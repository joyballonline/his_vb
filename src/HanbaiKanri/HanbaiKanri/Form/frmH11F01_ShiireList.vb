'===============================================================================
'
'　カネキ吉田商店様
'　　（システム名）販売管理
'　　（処理機能名）仕入明細表出力指示
'    （フォームID）H11F01
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   狩野        2018/03/15                 新規              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Public Class frmH11F01_ShiireList
    '-------------------------------------------------------------------------------
    '   定数定義
    '-------------------------------------------------------------------------------
    'PG制御文字 
    Private Const N As String = ControlChars.NewLine                    '改行文字
    Private Const RS As String = "RecSet"                               'レコードセットテーブル
    Private Const DATE_EMPTY As String = "    /  /"

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
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint  'フォームタイトル表示
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
    Private Sub frmH11F01_ShiireList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
                piRtn = _msgHd.dspMSG("noTargetData", "")       '該当データがありません。
                dtSiirebiFrom.Focus()
                Exit Sub
            End If
            '上限チェック 
            If rc >= StartUp._iniVal.PrintSelUpperCnt Then
                If _msgHd.dspMSG("MaxDataCnt", "") = vbCancel Then    '抽出したデータが上限を超えています。表示してよろしいですか？
                    dtSiirebiFrom.Focus()
                    Exit Sub
                End If
            End If

            _printKbn = CommonConst.REPORT_PREVIEW

            '注文明細表
            Dim rSect As RepSectionIF = New H11R01_ShiireList
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
                dtSiirebiFrom.Focus()
                Exit Sub
            End If
            '上限チェック 
            If rc >= StartUp._iniVal.PrintSelUpperCnt Then
                If _msgHd.dspMSG("MaxDataCnt", "") = vbCancel Then    '抽出したデータが上限を超えています。表示してよろしいですか？
                    dtSiirebiFrom.Focus()
                    Exit Sub
                End If
            End If
            'ダイアログ表示（印刷時のみ）
            If _msgHd.dspMSG("confirmPrint") = vbNo Then        '印刷処理を実行します。よろしいですか？
                Exit Sub
            End If

            _printKbn = CommonConst.REPORT_DIRECT

            '注文明細表
            Dim rSect As RepSectionIF = New H11R01_ShiireList
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
    Private Sub txtDenNoFrom_Enter(sender As Object, e As EventArgs) Handles txtDenNoFrom.Enter
        txtDenNoFrom.Focus()
        txtDenNoFrom.SelectAll()
    End Sub

    Private Sub txtDenNoTo_Enter(sender As Object, e As EventArgs) Handles txtDenNoTo.Enter
        txtDenNoTo.Focus()
        txtDenNoTo.SelectAll()
    End Sub
#End Region

#Region "CloseUp時"
    '-------------------------------------------------------------------------------
    '　CloseUp時、曜日設定
    '-------------------------------------------------------------------------------
    Private Sub dtSiirebiFrom_CloseUp(sender As Object, e As EventArgs) Handles dtSiirebiFrom.CloseUp
        Dim dDate As Date = Date.Parse(dtSiirebiFrom.Text)
        lblSiirebiWeekFrom.Text = Format(dDate, "ddd")
    End Sub
    Private Sub dtSiirebiTo_CloseUp(sender As Object, e As EventArgs) Handles dtSiirebiTo.CloseUp
        Dim dDate As Date = Date.Parse(dtSiirebiTo.Text)
        lblSiirebiWeekTo.Text = Format(dDate, "ddd")
    End Sub
    '-------------------------------------------------------------------------------
    '　Leave時、曜日設定
    '-------------------------------------------------------------------------------
    Private Sub dtSiirebiFrom_Leave(sender As Object, e As EventArgs) Handles dtSiirebiFrom.Leave
        Dim dDate As Date = Date.Parse(dtSiirebiFrom.Text)
        lblSiirebiWeekFrom.Text = Format(dDate, "ddd")
    End Sub

    Private Sub dtSiirebiTo_Leave(sender As Object, e As EventArgs) Handles dtSiirebiTo.Leave
        Dim dDate As Date = Date.Parse(dtSiirebiTo.Text)
        lblSiirebiWeekTo.Text = Format(dDate, "ddd")
    End Sub

#End Region

#Region "伝票Leave時"
    '-------------------------------------------------------------------------------
    '　伝票Leave時、前ゼロを設定
    '-------------------------------------------------------------------------------
    Private Sub txtDenNoFrom_Leave(sender As Object, e As EventArgs) Handles txtDenNoFrom.Leave
        If Not txtDenNoFrom.Text.Equals("") Then
            Dim lVal As Decimal = Decimal.Parse(txtDenNoFrom.Text)
            txtDenNoFrom.Text = Format(lVal, "000000")
        End If
    End Sub
    Private Sub txtDenNoTo_Leave(sender As Object, e As EventArgs) Handles txtDenNoTo.Leave
        If Not txtDenNoTo.Text.Equals("") Then
            Dim lVal As Decimal = Decimal.Parse(txtDenNoTo.Text)
            txtDenNoTo.Text = Format(lVal, "000000")
        End If
    End Sub
#End Region

#Region "KeyPress時"
    '-------------------------------------------------------------------------------
    '　KeyPress時、次コントロール移動
    '-------------------------------------------------------------------------------
    Private Sub rbUriage_KeyPress(sender As Object, e As KeyPressEventArgs) Handles dtSiirebiFrom.KeyPress,
                                                                                    dtSiirebiTo.KeyPress,
                                                                                    txtDenNoFrom.KeyPress,
                                                                                    txtDenNoTo.KeyPress

        '押下キーがEnterの場合、次のコントロールへフォーカス移動
        Call UtilClass.moveNextFocus(Me, e)
    End Sub
#End Region

    '-------------------------------------------------------------------------------
    '　画面初期化
    '-------------------------------------------------------------------------------
    Private Sub initForm()

        '仕入日
        Dim sSysDate As String = _comLogc.getSysDdate()     'システム日付
        dtSiirebiFrom.Text = sSysDate
        dtSiirebiTo.Text = sSysDate
        Dim sWeek As String = _comLogc.getSysWeek()         'システム日付の曜日取得
        lblSiirebiWeekFrom.Text = sWeek
        lblSiirebiWeekTo.Text = sWeek
        '伝票番号
        lblDenStrFrom.Text = CommonConst.CONNECT_DEN_SHIIRE
        txtDenNoFrom.Text = ""
        lblDenStrTo.Text = CommonConst.CONNECT_DEN_SHIIRE
        txtDenNoTo.Text = ""

    End Sub

    '-------------------------------------------------------------------------------
    '　入力チェック
    '-------------------------------------------------------------------------------
    Private Function check_Input() As Boolean

        Dim piRtn As Integer

        check_Input = False

        '大小チェック
        Select Case String.CompareOrdinal(dtSiirebiFrom.Text, dtSiirebiTo.Text)
            Case 0
                '同じ
            Case Is > 0
                '>
                piRtn = _msgHd.dspMSG("ErrDaiSyoChk", "")                                   '大小関係が正しくありません。
                dtSiirebiFrom.Focus()
                Exit Function
            Case Is < 0
                '<
        End Select

        '両方入力された時のみエラーチェック
        If Not txtDenNoFrom.Text.Equals("") And Not txtDenNoTo.Text.Equals("") Then
            Select Case String.CompareOrdinal(txtDenNoFrom.Text, txtDenNoTo.Text)

                Case 0
                '同じ
                Case Is > 0
                    '>
                    piRtn = _msgHd.dspMSG("ErrDaiSyoChk", "")                               '大小関係が正しくありません。
                    txtDenNoFrom.Focus()
                    Exit Function
                Case Is < 0
                    '<
            End Select
        End If

        check_Input = True

    End Function

    '-------------------------------------------------------------------------------
    '　ＳＱＬ編集
    '-------------------------------------------------------------------------------
    Private Function makeSql() As String
        makeSql = ""

        Dim sSql As String = ""
        sSql = sSql & " SELECT"
        '検索条件
        sSql = sSql & "  '" & CommonConst.SHIIRE_KBN_NM_SHIIRE & "' 条件_区分"
        sSql = sSql & "     ,'" & dtSiirebiFrom.Text & " ～ " & dtSiirebiTo.Text & "' 条件_仕入日"
        If Not txtDenNoFrom.Text.Equals("") And Not txtDenNoTo.Text.Equals("") Then
            sSql = sSql & "     ,'" & lblDenStrFrom.Text & txtDenNoFrom.Text & " ～ " & lblDenStrTo.Text & txtDenNoTo.Text & "' 条件_伝票番号"
        ElseIf Not txtDenNoFrom.Text.Equals("") Then
            sSql = sSql & "     ,'" & lblDenStrFrom.Text & txtDenNoFrom.Text & "' 条件_伝票番号"
        ElseIf Not txtDenNoTo.Text.Equals("") Then
            sSql = sSql & "     ,'" & lblDenStrTo.Text & txtDenNoTo.Text & "' 条件_伝票番号"
        Else
            sSql = sSql & "     ,'' 条件_伝票番号"
        End If
        'ヘッダ
        sSql = sSql & "     ,t40.仕入伝番"
        sSql = sSql & "     ,t40.仕入日"
        sSql = sSql & "     ,t40.仕入先名"
        sSql = sSql & "     ,t40.仕入先名"
        sSql = sSql & "     ,CASE WHEN t40.仕入先コード = t40.支払先コード"
        sSql = sSql & "           THEN ''"
        sSql = sSql & "           ELSE '[支払先]'"
        sSql = sSql & "      END 支払先ID"
        sSql = sSql & "     ,CASE WHEN t40.仕入先コード = t40.支払先コード"
        sSql = sSql & "           THEN ''"
        sSql = sSql & "           ELSE t40.支払先名"
        sSql = sSql & "      END 支払先名"
        '明細
        sSql = sSql & "     ,t41.商品名 || COALESCE(t41.商品詳細,'') 商品名"
        sSql = sSql & "     ,m90.文字１ 課税区分"
        sSql = sSql & "     ,t41.入数"
        sSql = sSql & "     ,t41.個数"
        sSql = sSql & "     ,t41.仕入数量"
        sSql = sSql & "     ,t41.単位"
        sSql = sSql & "     ,t41.仕入単価"
        sSql = sSql & "     ,t41.仕入金額"
        sSql = sSql & "     ,t41.仕入明細備考"
        'フッタ
        sSql = sSql & "     ,t40.仕入金額計"
        sSql = sSql & "     ,t40.消費税計"
        sSql = sSql & "     ,t40.税込額計"
        'ページフッタ
        sSql = sSql & "     ,m01.会社名"
        'レポートフッタ
        sSql = sSql & " FROM t40_sirehd t40"
        sSql = sSql & "      LEFT JOIN t41_siredt AS t41    ON t41.会社コード   = t40.会社コード"
        sSql = sSql & "                                    AND t41.仕入伝番     = t40.仕入伝番"
        sSql = sSql & "      LEFT JOIN m90_hanyo AS m90     ON m90.会社コード  = t40.会社コード"
        sSql = sSql & "                                    AND m90.固定キー    = '" & CommonConst.HANYO_KAZEI_KBN & "'"
        sSql = sSql & "                                    AND m90.可変キー    = t41.課税区分"
        sSql = sSql & "      LEFT JOIN m01_company AS  m01  ON m01.会社コード   = t40.会社コード"
        sSql = sSql & " WHERE t40.会社コード = '" & _companyCd & "'"
        sSql = sSql & "   AND t40.取消区分   = 0"
        '仕入日
        sSql = sSql & "   AND t40.仕入日 BETWEEN '" & dtSiirebiFrom.Text & "'" & " AND '" & dtSiirebiTo.Text & "'"
        '伝票番号
        If Not txtDenNoFrom.Text.Equals("") And Not txtDenNoTo.Text.Equals("") Then
            sSql = sSql & "   AND t40.仕入伝番   >= '" & lblDenStrFrom.Text & txtDenNoFrom.Text & "'"
            sSql = sSql & "   AND t40.仕入伝番   <= '" & lblDenStrTo.Text & txtDenNoTo.Text & "'"
        ElseIf Not txtDenNoFrom.Text.Equals("") Then
            sSql = sSql & "   AND t40.仕入伝番   = '" & lblDenStrFrom.Text & txtDenNoFrom.Text & "'"
        ElseIf Not txtDenNoFrom.Text.Equals("") Then
            sSql = sSql & "   AND t40.仕入伝番   = '" & lblDenStrTo.Text & txtDenNoTo.Text & "'"
        End If

        sSql = sSql & " ORDER BY t40.仕入伝番, t41.行番"

        Return sSql

    End Function




End Class