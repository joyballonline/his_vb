'===============================================================================
'
'　カネキ吉田商店様
'　　（システム名）販売管理
'　　（処理機能名）仕入先元帳出力指示
'    （フォームID）H11F03
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
Public Class frmH11F03_SiiresakiMotoList
    '-------------------------------------------------------------------------------
    '   定数定義
    '-------------------------------------------------------------------------------
    'PG制御文字 
    Private Const N As String = ControlChars.NewLine                    '改行文字
    Private Const RS As String = "RecSet"                               'レコードセットテーブル
    Private Const DATE_EMPTY As String = "    /"
    Private Const SORT_CODE As String = "コード順"
    Private Const SORT_KANA As String = "カナ順"
    Private Const STRING_KURIKOSIGAKU As String = "＊＊　繰越額　＊＊"

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
    Private Sub frmH11F03_SiiresakiMotoList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

            '繰越額の追加
            Dim ds2 As DataSet = add_Kurikosi(ds)


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
            Dim rSect As RepSectionIF = New H11R03_ShiiresakiMotoList
            rSect.setData(ds2, _db)
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
            Dim rSect As RepSectionIF = New H11R03_ShiiresakiMotoList
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
    Private Sub txtNyukinYm_Enter(sender As Object, e As EventArgs) Handles txtSiireYm.Enter
        txtSiireYm.Focus()
        txtSiireYm.SelectAll()
    End Sub
#End Region

#Region "CloseUp時"
    '-------------------------------------------------------------------------------
    '　CloseUp時、曜日設定
    '-------------------------------------------------------------------------------
    Private Sub dtShukkabiFrom_CloseUp(sender As Object, e As EventArgs) Handles dtYmdFrom.CloseUp
        Dim dDate As Date = Date.Parse(dtYmdFrom.Text)
        lblYmdWeekFrom.Text = Format(dDate, "ddd")
    End Sub
    Private Sub dtShukkabiTo_CloseUp(sender As Object, e As EventArgs) Handles dtYmdTo.CloseUp
        Dim dDate As Date = Date.Parse(dtYmdTo.Text)
        lblYmsWeekTo.Text = Format(dDate, "ddd")
    End Sub
    '-------------------------------------------------------------------------------
    '　Leave時、曜日設定
    '-------------------------------------------------------------------------------
    Private Sub dtShukkabiFrom_Leave(sender As Object, e As EventArgs) Handles dtYmdFrom.Leave
        Dim dDate As Date = Date.Parse(dtYmdFrom.Text)
        lblYmdWeekFrom.Text = Format(dDate, "ddd")
    End Sub

    Private Sub dtShukkabiTo_Leave(sender As Object, e As EventArgs) Handles dtYmdTo.Leave
        Dim dDate As Date = Date.Parse(dtYmdTo.Text)
        lblYmsWeekTo.Text = Format(dDate, "ddd")
    End Sub

#End Region

#Region "KeyPress時"
    '-------------------------------------------------------------------------------
    '　KeyPress時、次コントロール移動
    '-------------------------------------------------------------------------------
    Private Sub rbUriage_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSiireYm.KeyPress,
                                                                                    dtYmdFrom.KeyPress,
                                                                                    dtYmdTo.KeyPress,
                                                                                    rbSortCode.KeyPress,
                                                                                    rbSortKana.KeyPress,
                                                                                    dgvKouho.KeyPress,
                                                                                    dgvTaisho.KeyPress

        '押下キーがEnterの場合、次のコントロールへフォーカス移動
        Call UtilClass.moveNextFocus(Me, e)
    End Sub
#End Region

#Region "売上入金年月Leave時"
    '-------------------------------------------------------------------------------
    '　売上入金年月Leave時、日付設定
    '-------------------------------------------------------------------------------
    Private Sub txtNyukinYm_Leave(sender As Object, e As EventArgs) Handles txtSiireYm.Leave

        If _frmOpen = False Then Exit Sub                            '起動中は実行しない

        If txtSiireYm.Text = DATE_EMPTY Then Exit Sub

        Dim dDate As DateTime = Date.Parse(txtSiireYm.Text & "/01")
        Dim dGetumatu As DateTime = dDate
        dGetumatu = (dGetumatu.AddMonths(1)).AddDays(-1)
        txtSiireYm.Text = dDate.ToString("yyyy/MM")     '前月
        '日付
        dtYmdFrom.Text = dDate.ToString("yyyy/MM/dd")
        lblYmdWeekFrom.Text = dDate.ToString("ddd")
        dtYmdTo.Text = dGetumatu.ToString("yyyy/MM/dd")
        lblYmsWeekTo.Text = dGetumatu.ToString("ddd")
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

        '売上入金日
        Dim sDate As String = _comLogc.getSysDdate().Substring(0, 7) & "/01"     'システム日付
        Dim dDate As DateTime = Date.Parse(sDate)
        dDate = dDate.AddMonths(-1)
        Dim dGetumatu As DateTime = dDate
        dGetumatu = (dGetumatu.AddMonths(1)).AddDays(-1)
        txtSiireYm.Text = dDate.ToString("yyyy/MM")     '前月
        '日付
        dtYmdFrom.Text = dDate.ToString("yyyy/MM/dd")
        lblYmdWeekFrom.Text = dDate.ToString("ddd")
        dtYmdTo.Text = dGetumatu.ToString("yyyy/MM/dd")
        lblYmsWeekTo.Text = dGetumatu.ToString("ddd")

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
    '　入力チェック
    '-------------------------------------------------------------------------------
    Private Function check_Input() As Boolean

        Dim piRtn As Integer

        check_Input = False

        '必須チェック
        If txtSiireYm.Text = DATE_EMPTY Then
            piRtn = _msgHd.dspMSG("requiredImput", "")                               '必須入力項目です。
            txtSiireYm.Focus()
            Exit Function
        End If

        '大小チェック
        Select Case String.CompareOrdinal(dtYmdFrom.Text, dtYmdTo.Text)
            Case 0
                '同じ
            Case Is > 0
                '>
                piRtn = _msgHd.dspMSG("ErrDaiSyoChk", "")                            '大小関係が正しくありません。
                dtYmdFrom.Focus()
                Exit Function
            Case Is < 0
                '<
        End Select

        '年月チェック
        If Not txtSiireYm.Text.Equals(dtYmdFrom.Text.Substring(0, 7)) Then
            piRtn = _msgHd.dspMSG("notSiireYm", "")                                 '仕入支払年月と違います。
            dtYmdFrom.Focus()
            Exit Function
        End If
        If Not txtSiireYm.Text.Equals(dtYmdTo.Text.Substring(0, 7)) Then
            piRtn = _msgHd.dspMSG("notSiireYm", "")                                 '仕入支払年月と違います。
            dtYmdTo.Focus()
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
    '　ＳＱＬ編集
    '-------------------------------------------------------------------------------
    Private Function makeSql() As String
        makeSql = ""

        Dim sYm As String = Replace(txtSiireYm.Text, "/", "")
        Dim dDate As DateTime = Date.Parse(txtSiireYm.Text & "/01")
        dDate = dDate.AddMonths(-1)
        Dim sYmZen As String = dDate.ToString("yyyy/MM")     '前月
        sYmZen = Replace(sYmZen, "/", "")

        '支払先編集
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
        sSql = sSql & "     ,'" & txtSiireYm.Text & "' 条件_年月"
        sSql = sSql & "     ,'" & dtYmdFrom.Text & " ～ " & dtYmdTo.Text & "' 条件_日付"
        sSql = sSql & "     ,'" & dtYmdFrom.Text & "'                         条件_日付From"
        sSql = sSql & "     ,'" & dtYmdTo.Text & "'                           条件_日付To"
        If rbSortCode.Checked = True Then
            sSql = sSql & "     ,'" & SORT_CODE & "' 条件_出力順"
        Else
            sSql = sSql & "     ,'" & SORT_KANA & "' 条件_出力順"
        End If
        'ヘッダ（なし）
        '明細
        sSql = sSql & "     ,AA.取得元テーブル"
        sSql = sSql & "     ,AA.支払先コード"
        sSql = sSql & "     ,AA.支払先名"
        sSql = sSql & "     ,AA.仕入日"
        sSql = sSql & "     ,AA.仕入伝番"
        sSql = sSql & "     ,AA.行番"
        sSql = sSql & "     ,AA.区分"
        sSql = sSql & "     ,AA.仕入先"
        sSql = sSql & "     ,AA.商品仕入摘要"
        sSql = sSql & "     ,AA.数量"
        sSql = sSql & "     ,AA.単位"
        sSql = sSql & "     ,AA.単価"
        sSql = sSql & "     ,AA.仕入額"
        sSql = sSql & "     ,AA.消費税"
        sSql = sSql & "     ,AA.支払額"
        sSql = sSql & "     ,AA.残高"
        sSql = sSql & "     ,AA.取引先名カナ"
        'ページフッタ
        sSql = sSql & "     ,AA.会社名"
        'レポートフッタ

        sSql = sSql & " FROM ("
        sSql = sSql & "       SELECT 2                 取得元テーブル"
        sSql = sSql & "             ,t40.支払先コード  支払先コード"
        sSql = sSql & "             ,t40.支払先名      支払先名"
        sSql = sSql & "             ,t40.仕入日        仕入日"
        sSql = sSql & "             ,t40.仕入伝番      仕入伝番"
        sSql = sSql & "             ,t41.行番          行番"
        sSql = sSql & "             ,t40.仕入区分      区分"
        sSql = sSql & "             ,t40.仕入先名      仕入先"
        sSql = sSql & "             ,t41.商品名 || COALESCE(t41.商品詳細,'') 商品仕入摘要"       'NVL
        sSql = sSql & "             ,t41.仕入数量      数量"
        sSql = sSql & "             ,t41.単位          単位"
        sSql = sSql & "             ,t41.仕入単価      単価"
        sSql = sSql & "             ,t41.仕入金額      仕入額"
        sSql = sSql & "             ,CASE WHEN t41.行番 = 1"
        sSql = sSql & "                   THEN t40.消費税計"
        sSql = sSql & "                   ELSE NULL"
        sSql = sSql & "              END               消費税"
        sSql = sSql & "             ,NULL              支払額"
        sSql = sSql & "             ,0                 残高"
        sSql = sSql & "             ,m10.取引先名カナ"
        sSql = sSql & "             ,m01.会社名"
        sSql = sSql & "       FROM t40_sirehd t40"      '仕入基本
        sSql = sSql & "       LEFT JOIN t41_siredt As t41    On t41.会社コード   = t40.会社コード"
        sSql = sSql & "                                     And t41.仕入伝番     = t40.仕入伝番"
        sSql = sSql & "       LEFT JOIN m10_customer As m10  On m10.会社コード   = t40.会社コード"
        sSql = sSql & "                                     And m10.取引先コード = t40.支払先コード"
        sSql = sSql & "       LEFT JOIN m01_company As  m01  On m01.会社コード   = t40.会社コード"
        sSql = sSql & "       WHERE t40.会社コード   = '" & _companyCd & "'"
        sSql = sSql & "         AND t40.取消区分     = 0"
        sSql = sSql & "         AND t40.支払先コード " & sInStr
        sSql = sSql & "         AND TO_CHAR(t40.仕入日,'YYYY/MM/DD') >= '" & dtYmdFrom.Text & "'"
        sSql = sSql & "         AND TO_CHAR(t40.仕入日,'YYYY/MM/DD') <= '" & dtYmdTo.Text & "'"
        sSql = sSql & "     UNION ALL"
        sSql = sSql & "       SELECT 3                 取得元テーブル"
        sSql = sSql & "             ,t45.支払先コード  支払先コード"
        sSql = sSql & "             ,t45.支払先名      支払先名"
        sSql = sSql & "             ,t45.支払日        仕入日"
        sSql = sSql & "             ,t45.支払伝番      仕入伝番"
        sSql = sSql & "             ,t46.行番          行番"
        sSql = sSql & "             ,'支払'            区分"
        sSql = sSql & "             ,COALESCE( t46.支払種別名,'')              仕入先"
        sSql = sSql & "             ,CASE WHEN t46.行番 = 1"
        sSql = sSql & "                   THEN  COALESCE( t45.備考１,'')"
        sSql = sSql & "                   ELSE '' "
        sSql = sSql & "              END               商品仕入摘要"
        sSql = sSql & "             ,NULL              数量"
        sSql = sSql & "             ,NULL              単位"
        sSql = sSql & "             ,NULL              単価"
        sSql = sSql & "             ,NULL              仕入額"
        sSql = sSql & "             ,NULL              消費税"
        sSql = sSql & "             ,t46.金額          支払額"
        sSql = sSql & "             ,0                 残高"
        sSql = sSql & "             ,m10.取引先名カナ"
        sSql = sSql & "             ,m01.会社名"
        sSql = sSql & "       FROM t45_sraihd t45"      '支払基本
        sSql = sSql & "       LEFT JOIN t46_sraidt As t46    On t46.会社コード   = t45.会社コード"
        sSql = sSql & "                                     And t46.支払伝番     = t45.支払伝番"
        sSql = sSql & "       LEFT JOIN m10_customer As m10  On m10.会社コード   = t45.会社コード"
        sSql = sSql & "                                     And m10.取引先コード = t45.支払先コード"
        sSql = sSql & "       LEFT JOIN m01_company As  m01  On m01.会社コード   = t45.会社コード"
        sSql = sSql & "       WHERE t45.会社コード   = '" & _companyCd & "'"
        sSql = sSql & "         AND t45.取消区分     = 0"
        sSql = sSql & "         AND t45.支払先コード " & sInStr
        sSql = sSql & "         AND TO_CHAR(t45.支払日,'YYYY/MM/DD') >= '" & dtYmdFrom.Text & "'"
        sSql = sSql & "         AND TO_CHAR(t45.支払日,'YYYY/MM/DD') <= '" & dtYmdTo.Text & "'"
        sSql = sSql & "     UNION ALL"
        sSql = sSql & "       SELECT 1                取得元テーブル"
        sSql = sSql & "             ,t50.支払先コード"
        sSql = sSql & "             ,t50.支払先名"
        sSql = sSql & "             ,TO_DATE(年月 || '01', 'YYYYMMDD') 仕入日"
        sSql = sSql & "             ,NULL             仕入伝番"
        sSql = sSql & "             ,NULL             行番"
        sSql = sSql & "             ,NULL             区分"
        sSql = sSql & "             ,'" & STRING_KURIKOSIGAKU & "'     仕入先"
        sSql = sSql & "             ,NULL             商品仕入摘要"
        sSql = sSql & "             ,NULL             数量"
        sSql = sSql & "             ,NULL             単位"
        sSql = sSql & "             ,NULL             単価"
        sSql = sSql & "             ,NULL             仕入額"
        sSql = sSql & "             ,NULL             消費税"
        sSql = sSql & "             ,NULL             支払額"
        sSql = sSql & "             ,t50.当月残高     残高"
        sSql = sSql & "             ,m10.取引先名カナ"
        sSql = sSql & "             ,m01.会社名"
        sSql = sSql & "       FROM t50_simuzd t50"        '債務残高
        sSql = sSql & "       LEFT JOIN m10_customer AS m10  ON m10.会社コード   = t50.会社コード"
        sSql = sSql & "                                     AND m10.取引先コード = t50.支払先コード"
        sSql = sSql & "       LEFT JOIN m01_company AS  m01  ON m01.会社コード   = t50.会社コード"
        sSql = sSql & "       WHERE t50.会社コード   = '" & _companyCd & "'"
        sSql = sSql & "         AND t50.年月         = '" & sYmZen & "'"      '前月
        sSql = sSql & "         AND t50.支払先コード " & sInStr
        sSql = sSql & "      ) AS  AA "
        If rbSortCode.Checked = True Then
            sSql = sSql & " ORDER BY AA.支払先コード, AA.仕入日, AA.取得元テーブル, AA.仕入伝番, AA.行番"
        Else
            sSql = sSql & " ORDER BY AA.取引先名カナ, AA.支払先コード, AA.仕入日, AA.取得元テーブル, AA.仕入伝番, AA.行番"
        End If

        Return sSql

    End Function


    '-------------------------------------------------------------------------------
    '　前月の繰越レコードが存在しない場合、繰越レコードを追加する
    '-------------------------------------------------------------------------------
    Private Function add_Kurikosi(ByVal paraDs As DataSet) As DataSet

        Dim ds As New DataSet
        Dim dsIdx As Integer = 0
        Dim dt As DataTable = New DataTable(RS)

        '列追加
        dt.Columns.Add("条件_区分", GetType(String))
        dt.Columns.Add("条件_年月", GetType(String))
        dt.Columns.Add("条件_日付", GetType(String))
        dt.Columns.Add("条件_日付From", GetType(String))
        dt.Columns.Add("条件_日付To", GetType(String))
        dt.Columns.Add("条件_出力順", GetType(String))

        dt.Columns.Add("取得元テーブル", GetType(String))
        dt.Columns.Add("支払先コード", GetType(String))
        dt.Columns.Add("支払先名", GetType(String))
        dt.Columns.Add("仕入日", GetType(Date))
        dt.Columns.Add("仕入伝番", GetType(String))
        dt.Columns.Add("行番", GetType(Int32))
        dt.Columns.Add("区分", GetType(String))
        dt.Columns.Add("仕入先", GetType(String))
        dt.Columns.Add("商品仕入摘要", GetType(String))
        dt.Columns.Add("数量", GetType(Decimal))
        dt.Columns.Add("単位", GetType(String))
        dt.Columns.Add("単価", GetType(Decimal))
        dt.Columns.Add("仕入額", GetType(Decimal))
        dt.Columns.Add("消費税", GetType(Decimal))
        dt.Columns.Add("支払額", GetType(Decimal))
        dt.Columns.Add("残高", GetType(Decimal))
        dt.Columns.Add("取引先名カナ", GetType(String))
        dt.Columns.Add("会社名", GetType(String))

        Dim sSiharaisakiCd As String = ""
        For index As Integer = 0 To paraDs.Tables(RS).Rows.Count - 1
            Dim newRow As DataRow = dt.NewRow

            '請求先コードがkeyブレイクで債権残高(t30)レコードがない場合
            If Not sSiharaisakiCd.Equals(_db.rmNullStr(paraDs.Tables(RS).Rows(index)("支払先コード"))) AndAlso
              Not _db.rmNullStr(paraDs.Tables(RS).Rows(index)("取得元テーブル")).Equals("1") Then

                newRow("条件_区分") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("条件_区分"))
                newRow("条件_年月") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("条件_年月"))
                newRow("条件_日付") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("条件_日付"))
                newRow("条件_日付From") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("条件_日付From"))
                newRow("条件_日付To") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("条件_日付To"))
                newRow("条件_出力順") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("条件_出力順"))

                newRow("取得元テーブル") = "1"
                newRow("支払先コード") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("支払先コード"))
                newRow("支払先名") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("支払先名"))
                Dim dShiirebi As Date = DateTime.Parse(_db.rmNullStr(paraDs.Tables(RS).Rows(index)("仕入日")).Substring(0, 7) & "/01")
                dShiirebi = dShiirebi.AddDays(-1)
                newRow("仕入日") = dShiirebi.ToShortDateString
                newRow("仕入伝番") = ""
                newRow("行番") = 0
                newRow("区分") = ""
                newRow("仕入先") = STRING_KURIKOSIGAKU
                newRow("商品仕入摘要") = ""
                newRow("数量") = 0
                newRow("単位") = ""
                newRow("単価") = 0
                newRow("仕入額") = 0
                newRow("消費税") = 0
                newRow("支払額") = 0
                newRow("残高") = 0
                newRow("取引先名カナ") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("取引先名カナ"))
                newRow("会社名") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("会社名"))
                '追加
                dt.Rows.Add(newRow)
                newRow = dt.NewRow
            End If
            'keyブレイク用
            sSiharaisakiCd = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("支払先コード"))

            newRow("条件_区分") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("条件_区分"))
            newRow("条件_年月") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("条件_年月"))
            newRow("条件_日付") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("条件_日付"))
            newRow("条件_日付From") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("条件_日付From"))
            newRow("条件_日付To") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("条件_日付To"))
            newRow("条件_出力順") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("条件_出力順"))

            newRow("取得元テーブル") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("取得元テーブル"))
            newRow("支払先コード") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("支払先コード"))
            newRow("支払先名") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("支払先名"))
            newRow("仕入日") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("仕入日"))
            newRow("仕入伝番") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("仕入伝番"))
            newRow("行番") = _db.rmNullDouble(paraDs.Tables(RS).Rows(index)("行番"))
            newRow("区分") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("区分"))
            newRow("仕入先") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("仕入先"))
            newRow("商品仕入摘要") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("商品仕入摘要"))
            newRow("数量") = _db.rmNullDouble(paraDs.Tables(RS).Rows(index)("数量"))
            newRow("単位") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("単位"))
            newRow("単価") = _db.rmNullDouble(paraDs.Tables(RS).Rows(index)("単価"))
            newRow("仕入額") = _db.rmNullDouble(paraDs.Tables(RS).Rows(index)("仕入額"))
            newRow("消費税") = _db.rmNullDouble(paraDs.Tables(RS).Rows(index)("消費税"))
            newRow("支払額") = _db.rmNullDouble(paraDs.Tables(RS).Rows(index)("支払額"))
            newRow("残高") = _db.rmNullDouble(paraDs.Tables(RS).Rows(index)("残高"))
            newRow("取引先名カナ") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("取引先名カナ"))
            newRow("会社名") = _db.rmNullStr(paraDs.Tables(RS).Rows(index)("会社名"))

            dt.Rows.Add(newRow)
        Next

        'DataSetにdtを追加
        ds.Tables.Add(dt)

        Return ds

    End Function
End Class