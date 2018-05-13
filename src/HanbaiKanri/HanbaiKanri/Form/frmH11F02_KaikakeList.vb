'===============================================================================
'
'　カネキ吉田商店様
'　　（システム名）販売管理
'　　（処理機能名）買掛金一覧表出力指示
'    （フォームID）H11F02
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   狩野        2018/03/16                 新規              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView        'UtilDataGridViewHandler

Public Class frmH11F02_KaikakeList
    '-------------------------------------------------------------------------------
    '   定数定義
    '-------------------------------------------------------------------------------
    'PG制御文字 
    Private Const N As String = ControlChars.NewLine                    '改行文字
    Private Const RS As String = "RecSet"                               'レコードセットテーブル
    Private Const DATE_EMPTY As String = "    /"
    Private Const SORT_CODE As String = "コード順"
    Private Const SORT_KANA As String = "カナ順"

    '-------------------------------------------------------------------------------
    '   変数定義
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _companyCd As String
    Private _selectId As String
    Private _gyomuId As String
    Private _userId As String
    Private Shared _shoriId As String
    Private Shared _printKbn As String

    Private _comLogc As CommonLogic                         '共通処理用
    Private _frmOpen As Boolean                             '画面起動済フラグ
    Private _parentForm As Form                             '親フォーム


    '-------------------------------------------------------------------------------
    'プロパティ宣言
    '-------------------------------------------------------------------------------
    Public Shared ReadOnly Property shoriId() As String
        Get
            Return _shoriId
        End Get
    End Property
    Public Shared ReadOnly Property printKbn() As String
        Get
            Return _printKbn
        End Get
    End Property

#Region "コンストラクタ"
    '-------------------------------------------------------------------------------
    'デフォルトコンストラクタ（隠蔽）
    '-------------------------------------------------------------------------------
    Private Sub New()
        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()
    End Sub

    '-------------------------------------------------------------------------------
    'コンストラクタ　メニューから呼ばれる
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByRef prmSelectID As String, ByRef prmKidoShoriID As String, ByRef prmParentForm As Form)
        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint 'フォームタイトル表示
        _companyCd = frmC01F10_Login.loginValue.BumonCD                     '会社コード
        _selectId = prmSelectID                                             '選択処理ID
        _shoriId = prmSelectID                                              '起動処理ID
        _gyomuId = prmSelectID.Substring(0, 3)                              '業務ID
        _userId = frmC01F10_Login.loginValue.TantoCD                        'ユーザＩＤ
        _parentForm = prmParentForm

        ' 共通処理使用の準備
        _comLogc = New CommonLogic(_db, _msgHd)                             ' 共通処理用
        _frmOpen = False

    End Sub
#End Region

#Region "フォームロード"
    '-------------------------------------------------------------------------------
    '   画面ロード処理
    '   （処理概要） 画面が起動したときの処理を行う。
    '-------------------------------------------------------------------------------
    Private Sub frmH11F02_KaikakeList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            '操作履歴ログ作成
            Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, _shoriId, CommonConst.PROGRAM_START, CommonConst.STATUS_NORMAL,
                                                DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value,
                                                DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _userId)

            '画面初期化
            Call initForm()

            _frmOpen = True

        Catch ue As UsrDefException
            ue.dspMsg()
            cmdPreview.Enabled = False
            cmdPrint.Enabled = False
        Catch ex As Exception
            'システムエラー
            Call _msgHd.dspMSG("SystemErr", vbCrLf & "詳細：(" & Err.Number & ")" & vbCrLf & Err.Description & vbCrLf & "(Form_Load)")
        End Try

    End Sub
#End Region

#Region "戻るボタンクリック"
    '-------------------------------------------------------------------------------
    '　戻るボタン押下
    '-------------------------------------------------------------------------------
    Private Sub cmdBack_Click(sender As Object, e As EventArgs) Handles cmdBack.Click
        _parentForm.Show()                                              ' 前画面を表示
        _parentForm.Enabled = True                                      ' 前画面の使用を可能にする
        _parentForm.Activate()                                          ' 前画面をアクティブにする

        Me.Dispose()                                                    ' 自画面を閉じる
    End Sub
#End Region


#Region "プレビューボタンクリック"
    '-------------------------------------------------------------------------------
    '　プレビューボタン押下
    '-------------------------------------------------------------------------------
    Private Sub cmdPreview_Click(sender As Object, e As EventArgs) Handles cmdPreview.Click

        Dim piRtn As Integer

        Try

            '入力チェック
            If check_Input() = False Then
                Exit Sub
            End If

            'データ存在チェック
            Dim sSql As String = ""

            'SQL編集
            sSql = makeSql()
            Dim rc As Integer = 0
            Dim ds As DataSet = _db.selectDB(sSql, RS, rc)

            '対象件数チェック
            If rc <= 0 Then
                piRtn = _msgHd.dspMSG("noTargetData", "")               '該当データがありません。
                txtSiireYm.Focus()
                Exit Sub
            End If
            '上限チェック 
            If rc >= StartUp._iniVal.PrintSelUpperCnt Then
                If _msgHd.dspMSG("MaxDataCnt", "") = vbCancel Then      '抽出したデータが上限を超えています。表示してよろしいですか？
                    txtSiireYm.Focus()
                    Exit Sub
                End If
            End If

            _printKbn = CommonConst.REPORT_PREVIEW

            '注文明細表
            Dim rSect As RepSectionIF = New H11R02_KaikakeList
            rSect.setData(ds, _db)
            rSect.runReport()

            '※操作履歴はレポートで出力

        Catch ue As UsrDefException
            'メッセージ表示
            ue.dspMsg()
        Catch ex As Exception
            'システムエラー
            Call _msgHd.dspMSG("SystemErr", vbCrLf & "詳細：(" & Err.Number & ")" & vbCrLf & Err.Description & vbCrLf & "(cmdPreview)")
        End Try
    End Sub
#End Region

#Region "印刷ボタンクリック"
    '-------------------------------------------------------------------------------
    '　印刷ボタン押下
    '-------------------------------------------------------------------------------
    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click

        Dim piRtn As Integer

        Try

            '入力チェック
            If check_Input() = False Then
                Exit Sub
            End If

            'データ存在チェック
            Dim sSql As String = ""

            'SQL編集
            sSql = makeSql()
            Dim rc As Integer = 0
            Dim ds As DataSet = _db.selectDB(sSql, RS, rc)

            '対象件数チェック
            If rc <= 0 Then
                piRtn = _msgHd.dspMSG("noTargetData", "")       '該当データがありません。
                txtSiireYm.Focus()
                Exit Sub
            End If
            '上限チェック 
            If rc >= StartUp._iniVal.PrintSelUpperCnt Then
                If _msgHd.dspMSG("MaxDataCnt", "") = vbCancel Then    '抽出したデータが上限を超えています。表示してよろしいですか？
                    txtSiireYm.Focus()
                    Exit Sub
                End If
            End If
            'ダイアログ表示（印刷時のみ）
            If _msgHd.dspMSG("confirmPrint") = vbNo Then        '印刷処理を実行します。よろしいですか？
                Exit Sub
            End If

            _printKbn = CommonConst.REPORT_DIRECT

            '注文明細表
            Dim rSect As RepSectionIF = New H11R02_KaikakeList
            rSect.setData(ds, _db)
            rSect.runReport()

            '※操作履歴はレポートで出力

            'ダイアログ表示（印刷時のみ）
            piRtn = _msgHd.dspMSG("printOK")                    '対象データの印刷が完了しました。

        Catch ue As UsrDefException
            'メッセージ表示
            ue.dspMsg()
        Catch ex As Exception
            'システムエラー
            Call _msgHd.dspMSG("SystemErr", vbCrLf & "詳細：(" & Err.Number & ")" & vbCrLf & Err.Description & vbCrLf & "(cmdPreview)")
        End Try
    End Sub
#End Region

#Region "Enter時"
    '-------------------------------------------------------------------------------
    '　Enter時、全選択状態にする
    '-------------------------------------------------------------------------------
    Private Sub txtSiireYm_Enter(sender As Object, e As EventArgs) Handles txtSiireYm.Enter
        txtSiireYm.Focus()
        txtSiireYm.SelectAll()
    End Sub
#End Region

#Region "KeyPress時"
    '-------------------------------------------------------------------------------
    '　KeyPress時、次コントロール移動
    '-------------------------------------------------------------------------------
    Private Sub rbUriage_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSiireYm.KeyPress,
                                                                                    rbSortCode.KeyPress,
                                                                                    rbSortKana.KeyPress,
                                                                                    dgvKouho.KeyPress,
                                                                                    dgvTaisho.KeyPress

        '押下キーがEnterの場合、次のコントロールへフォーカス移動
        Call UtilClass.moveNextFocus(Me, e)
    End Sub
#End Region

#Region "左右ボタンClick時"
    '-------------------------------------------------------------------------------
    '　＞＞ボタンClick時、全件■出力候補→■出力対象
    '-------------------------------------------------------------------------------
    Private Sub cmdAllRight_Click(sender As Object, e As EventArgs) Handles cmdAllRight.Click
        Dim r As Integer = 0
        For r = 0 To dgvKouho.RowCount - 1
            Dim taishoIdx As Integer = dgvTaisho.RowCount
            dgvTaisho.Rows.Add()
            dgvTaisho.Rows(taishoIdx).Cells(0).Value = dgvKouho.Rows(r).Cells(0).Value
            dgvTaisho.Rows(taishoIdx).Cells(1).Value = dgvKouho.Rows(r).Cells(1).Value
        Next
        dgvKouho.Rows.Clear()
    End Sub

    '-------------------------------------------------------------------------------
    '　＞ボタンClick時、選択行■出力候補→■出力対象
    '-------------------------------------------------------------------------------
    Private Sub cmdSelectRight_Click(sender As Object, e As EventArgs) Handles cmdSelectRight.Click
        '選択されている行の数
        If dgvKouho.Rows.GetRowCount(DataGridViewElementStates.Selected) <= 0 Then
            Exit Sub
        End If

        '選択されている行を出力対象に追加（上から下）
        Dim r As Integer = 0
        For r = 0 To dgvKouho.RowCount - 1
            If dgvKouho.Rows(r).Selected = True Then
                Dim taishoIdx As Integer = dgvTaisho.RowCount
                dgvTaisho.Rows.Add()
                dgvTaisho.Rows(taishoIdx).Cells(0).Value = dgvKouho.Rows(r).Cells(0).Value
                dgvTaisho.Rows(taishoIdx).Cells(1).Value = dgvKouho.Rows(r).Cells(1).Value
                taishoIdx = taishoIdx + 1
            End If
        Next
        '選択されている行を出力候補から削除（For Eachは下から上）
        For Each ro As DataGridViewRow In dgvKouho.SelectedRows
            dgvKouho.Rows.RemoveAt(ro.Index)
        Next ro
    End Sub

    '-------------------------------------------------------------------------------
    '　＜ボタンClick時、選択行■出力候補←■出力対象
    '-------------------------------------------------------------------------------
    Private Sub cmdSelectLeft_Click(sender As Object, e As EventArgs) Handles cmdSelectLeft.Click
        '選択されている行の数
        If dgvTaisho.Rows.GetRowCount(DataGridViewElementStates.Selected) <= 0 Then
            Exit Sub
        End If

        '選択されている行を出力候補に追加（上から下）
        Dim r As Integer = 0
        For r = 0 To dgvTaisho.RowCount - 1
            If dgvTaisho.Rows(r).Selected = True Then
                Dim kouhoIdx As Integer = dgvKouho.RowCount
                dgvKouho.Rows.Add()
                dgvKouho.Rows(kouhoIdx).Cells(0).Value = dgvTaisho.Rows(r).Cells(0).Value
                dgvKouho.Rows(kouhoIdx).Cells(1).Value = dgvTaisho.Rows(r).Cells(1).Value
                kouhoIdx = kouhoIdx + 1
            End If
        Next
        '選択されている行を出力候補から削除（For Eachは下から上）
        For Each ro As DataGridViewRow In dgvTaisho.SelectedRows
            dgvTaisho.Rows.RemoveAt(ro.Index)
        Next ro
    End Sub

    '-------------------------------------------------------------------------------
    '　＜＜ボタンClick時、全件■出力候補←■出力対象
    '-------------------------------------------------------------------------------
    Private Sub cmdAllLeft_Click(sender As Object, e As EventArgs) Handles cmdAllLeft.Click
        Dim r As Integer = 0
        For r = 0 To dgvTaisho.RowCount - 1
            Dim kouhoIdx As Integer = dgvKouho.RowCount
            dgvKouho.Rows.Add()
            dgvKouho.Rows(kouhoIdx).Cells(0).Value = dgvTaisho.Rows(r).Cells(0).Value
            dgvKouho.Rows(kouhoIdx).Cells(1).Value = dgvTaisho.Rows(r).Cells(1).Value
        Next
        dgvTaisho.Rows.Clear()
    End Sub
#End Region

#Region "CellDoubleClick時"
    '-------------------------------------------------------------------------------
    '　CellDoubleClick、選択→■出力対象
    '-------------------------------------------------------------------------------
    Private Sub dgvKouho_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvKouho.CellDoubleClick

        'ヘッダー行ダブルクリックの場合、処理終了
        If DirectCast(e, DataGridViewCellEventArgs).RowIndex < 0 Then
            Exit Sub
        End If

        Call cmdSelectRight.PerformClick()

    End Sub

    '-------------------------------------------------------------------------------
    '　CellDoubleClick、■出力候補←選択
    '-------------------------------------------------------------------------------
    Private Sub dgvTaisho_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTaisho.CellDoubleClick

        'ヘッダー行ダブルクリックの場合、処理終了
        If DirectCast(e, DataGridViewCellEventArgs).RowIndex < 0 Then
            Exit Sub
        End If

        Call cmdSelectLeft.PerformClick()

    End Sub
#End Region

    '-------------------------------------------------------------------------------
    '　画面初期化
    '-------------------------------------------------------------------------------
    Private Sub initForm()

        '仕入支払年月
        Dim sSysDate As String = _comLogc.getSysDdate()     'システム日付
        Dim dDate As DateTime = Date.Parse(sSysDate)
        dDate = dDate.AddMonths(-1)
        txtSiireYm.Text = dDate.ToString("yyyy/MM")         '前月
        '出力順
        rbSortCode.Checked = True

        '一覧クリア
        dgvKouho.Rows.Clear()
        dgvTaisho.Rows.Clear()

        ' ユーザ操作による行追加を無効(禁止)
        dgvKouho.AllowUserToAddRows = False
        dgvTaisho.AllowUserToAddRows = False

        '列の幅をユーザー変更不可
        dgvKouho.AllowUserToResizeColumns = False
        dgvTaisho.AllowUserToResizeColumns = False

        '行の高さをユーザー変更不可
        dgvKouho.AllowUserToResizeRows = False
        dgvTaisho.AllowUserToResizeRows = False

        '列ヘッダーの高さ変更不可
        dgvKouho.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvTaisho.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

        '出力候補作成
        Call getKouhoList()

    End Sub

    '-------------------------------------------------------------------------------
    '　入力チェック
    '-------------------------------------------------------------------------------
    Private Function check_Input() As Boolean

        Dim piRtn As Integer

        check_Input = False

        '必須チェック
        If txtSiireYm.Text.Equals(DATE_EMPTY) Then
            piRtn = _msgHd.dspMSG("requiredImput", "")                               '必須入力項目です。
            txtSiireYm.Focus()
            Exit Function
        End If

        '■出力対象件数チェック
        If dgvTaisho.RowCount <= 0 Then
            piRtn = _msgHd.dspMSG("nonTaisho", "")                                   '出力対象を指定してください。
            dgvKouho.Focus()
            Exit Function
        End If

        check_Input = True

    End Function

    '-------------------------------------------------------------------------------
    '　出力候補作成
    '-------------------------------------------------------------------------------
    Private Sub getKouhoList(Optional ByVal getMenuSql As String = "")

        '取引先マスタから一覧にセット
        Try
            Dim sSql As String = ""
            sSql = "SELECT "
            sSql = sSql & "    取引先コード, 取引先名 "
            sSql = sSql & " FROM m10_customer "
            sSql = sSql & " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            sSql = sSql & " AND 支払先該当 = 1"
            sSql = sSql & " ORDER BY 取引先コード "

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(sSql, RS, reccnt)

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                dgvKouho.Rows.Add()
                dgvKouho.Rows(index).Cells(0).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("取引先コード"))
                dgvKouho.Rows(index).Cells(1).Value = _db.rmNullStr(ds.Tables(RS).Rows(index)("取引先名"))
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　ＳＱＬ編集
    '-------------------------------------------------------------------------------
    Private Function makeSql() As String
        makeSql = ""

        Dim sYm As String = Replace(txtSiireYm.Text, "/", "")
        Dim dDate As DateTime = Date.Parse(txtSiireYm.Text & "/01")
        dDate = dDate.AddMonths(-1)
        Dim sYmZen As String = dDate.ToString("yyyy/MM")     '前月
        sYmZen = Replace(sYmZen, "/", "")

        '請求先編集
        Dim sInStr As String = " IN("
        Dim sComma As String = ""
        Dim r As Integer = 0
        For r = 0 To dgvTaisho.RowCount - 1
            sInStr = sInStr & sComma & "'" & dgvTaisho.Rows(r).Cells(0).Value & "'"
            sComma = ","
        Next
        sInStr = sInStr & ")"

        Dim sSql As String = ""
        sSql = sSql & " SELECT"
        '検索条件
        sSql = sSql & "  '" & CommonConst.SHIIRE_KBN_NM_SHIIRE & "' 条件_区分"
        If txtSiireYm.Text.Equals(DATE_EMPTY) Then
            sSql = sSql & "     ,''                        条件_年月"
        Else
            sSql = sSql & "     ,'" & txtSiireYm.Text & "' 条件_年月"
        End If
        If rbSortCode.Checked = True Then
            sSql = sSql & "     ,'" & SORT_CODE & "' 条件_出力順"
        Else
            sSql = sSql & "     ,'" & SORT_KANA & "' 条件_出力順"
        End If
        'ヘッダ（なし）
        '明細
        sSql = sSql & "     ,AA.支払先コード"
        sSql = sSql & "     ,MAX(AA.取引先名カナ)                 取引先名カナ"
        sSql = sSql & "     ,MAX(AA.支払先名)                     支払先名"
        sSql = sSql & "     ,SUM(COALESCE(AA.前月残,0))           前月残"
        sSql = sSql & "     ,SUM(COALESCE(AA.仕入額,0))           仕入額"
        sSql = sSql & "     ,SUM(COALESCE(AA.仕入消費税,0))       仕入消費税"
        sSql = sSql & "     ,SUM(COALESCE(AA.共販手数料,0))       共販手数料"
        sSql = sSql & "     ,SUM(COALESCE(AA.共販手数料消費税,0)) 共販手数料消費税"
        sSql = sSql & "     ,SUM(COALESCE(AA.支払額,0))           支払額"
        sSql = sSql & "     ,SUM(COALESCE(AA.手数料,0))           手数料"
        'ページフッタ
        sSql = sSql & "     ,MAX(AA.会社名)   会社名"
        'レポートフッタ

        sSql = sSql & " FROM ("
        sSql = sSql & "       SELECT t50.支払先コード"
        sSql = sSql & "             ,t50.支払先名   支払先名"
        sSql = sSql & "             ,t50.当月残高   前月残"
        sSql = sSql & "             ,0              仕入額"
        sSql = sSql & "             ,0              仕入消費税"
        sSql = sSql & "             ,0              共販手数料"
        sSql = sSql & "             ,0              共販手数料消費税"
        sSql = sSql & "             ,0              支払額"
        sSql = sSql & "             ,0              手数料"
        sSql = sSql & "             ,m10.取引先名カナ"
        sSql = sSql & "             ,m01.会社名"
        sSql = sSql & "       FROM t50_simuzd t50"        '債務残高
        sSql = sSql & "       LEFT JOIN m10_customer AS m10  ON m10.会社コード   = t50.会社コード"
        sSql = sSql & "                                     AND m10.取引先コード = t50.支払先コード"
        sSql = sSql & "       LEFT JOIN m01_company AS  m01  ON m01.会社コード   = t50.会社コード"
        sSql = sSql & "       WHERE t50.会社コード   = '" & _companyCd & "'"
        sSql = sSql & "         AND t50.年月         = '" & sYmZen & "'"      '前月
        sSql = sSql & "         AND t50.支払先コード " & sInStr
        sSql = sSql & "     UNION ALL"
        sSql = sSql & "       SELECT t40.支払先コード"
        sSql = sSql & "             ,t40.支払先名   支払先名"
        sSql = sSql & "             ,0              前月残"
        sSql = sSql & "             ,t40.税抜額計   仕入額"
        sSql = sSql & "             ,t40.消費税計   仕入消費税"
        sSql = sSql & "             ,0              共販手数料"
        sSql = sSql & "             ,0              共販手数料消費税"
        sSql = sSql & "             ,0              支払額"
        sSql = sSql & "             ,0              手数料"
        sSql = sSql & "             ,m10.取引先名カナ"
        sSql = sSql & "             ,m01.会社名"
        sSql = sSql & "       FROM t40_sirehd t40"      '仕入基本
        sSql = sSql & "       LEFT JOIN m10_customer AS m10  ON m10.会社コード   = t40.会社コード"
        sSql = sSql & "                                     AND m10.取引先コード = t40.支払先コード"
        sSql = sSql & "       LEFT JOIN m01_company AS  m01  ON m01.会社コード   = t40.会社コード"
        sSql = sSql & "       WHERE t40.会社コード   = '" & _companyCd & "'"
        sSql = sSql & "         AND t40.取消区分     = 0"
        sSql = sSql & "         AND t40.支払先コード " & sInStr
        sSql = sSql & "         AND TO_CHAR(t40.仕入日,'yyyyMM') = '" & sYm & "'"
        sSql = sSql & "     UNION ALL"
        sSql = sSql & "       SELECT t45.支払先コード"
        sSql = sSql & "             ,t45.支払先名         支払先名"
        sSql = sSql & "             ,0                    前月残"
        sSql = sSql & "             ,0                    仕入額"
        sSql = sSql & "             ,0                    仕入消費税"
        sSql = sSql & "             ,t45.共販手数料       共販手数料"
        sSql = sSql & "             ,t45.共販手数料消費税 共販手数料消費税"
        sSql = sSql & "             ,t45.現金振込計       支払額"
        sSql = sSql & "             ,t45.手数料計         手数料"
        sSql = sSql & "             ,m10.取引先名カナ"
        sSql = sSql & "             ,m01.会社名"
        sSql = sSql & "       FROM t45_sraihd t45"      '支払基本
        sSql = sSql & "       LEFT JOIN m10_customer AS m10  ON m10.会社コード   = t45.会社コード"
        sSql = sSql & "                                     AND m10.取引先コード = t45.支払先コード"
        sSql = sSql & "       LEFT JOIN m01_company AS  m01  ON m01.会社コード   = t45.会社コード"
        sSql = sSql & "       WHERE t45.会社コード   = '" & _companyCd & "'"
        sSql = sSql & "         AND t45.取消区分     = 0"
        sSql = sSql & "         AND t45.支払先コード " & sInStr
        sSql = sSql & "         AND TO_CHAR(t45.支払日,'yyyyMM') = '" & sYm & "'"
        sSql = sSql & "      ) AS  AA "
        sSql = sSql & " GROUP BY AA.支払先コード"
        If rbSortCode.Checked = True Then
            sSql = sSql & " ORDER BY AA.支払先コード"
        Else
            sSql = sSql & " ORDER BY MAX(AA.取引先名カナ)"
        End If

        Return sSql

    End Function


End Class