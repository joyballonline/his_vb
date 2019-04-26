Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class Information
    Inherits System.Windows.Forms.Form

    '------------------------------------------------------------------------------------------------------
    'メンバー定数宣言
    '------------------------------------------------------------------------------------------------------
    'PG制御文字 
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const RS As String = "RecSet"                               'レコードセットテーブル
    Private Const HAIFUN_ID As String = "H@@@@@"
    Private Const HAIFUN_GYOMU1 As String = "-----------"
    Private Const HAIFUN_SHORI As String = "----------------"
    Private Const HAIFUN_SETUMEI As String = "-------------------------------------------"
    Private Const HAIFUN_MYSOUSANICHIJI As String = "---------------"
    Private Const HAIFUN_SOUSA As String = "----------"
    Private Const HAIFUN_ZENKAI As String = "---------------"

    '-------------------------------------------------------------------------------
    '   変数定義
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _langHd As UtilLangHandler
    Private _parentForm As Form
    'Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ

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
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler,
                   ByRef prmRefDbHd As UtilDBIf,
                   ByRef prmRefLang As UtilLangHandler,
                   ByRef prmRefForm As Form
                   )
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm

        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    '初期表示
    Private Sub Hanyo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Hide()

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            BtnBack.Text = "Back"

        End If

        '受注残データ取得
        Dim orderRemainingClass = New OrderRemainingList(_msgHd, _db, _langHd, Me)
        Dim orderRemainingData As DataSet = orderRemainingClass.getJobOrderRemainingList()
        Dim orderRemainingCnt As Integer = orderRemainingData.Tables(RS).Rows.Count

        If orderRemainingCnt > 0 Then

            LbMessage.Text = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                    "There is " & orderRemainingCnt.ToString & " order backlog",
                                    "受注残が " & orderRemainingCnt.ToString & " 件あります")

        End If

        '発注残データ取得
        Dim jobOrderingRemainingData As DataSet = getJobOrderingRemainingList()
        Dim jobOrderingRemainingCnt As Integer = orderRemainingData.Tables(RS).Rows.Count

        If jobOrderingRemainingCnt > 0 Then
            LbMessage.Text += IIf(LbMessage.Text = "", "", N)
            LbMessage.Text += IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                    "There is " & orderRemainingCnt.ToString & " ordering backlog",
                                    "発注残が " & orderRemainingCnt.ToString & " 件あります")
        End If

        '請求残データ取得
        Dim ARClass = New CustomerARList(_msgHd, _db, _langHd, Me)
        Dim ARData As DataSet = ARClass.getARList()
        Dim ARCnt As Integer = ARData.Tables(RS).Rows.Count

        If ARCnt > 0 Then
            LbMessage.Text += IIf(LbMessage.Text = "", "", N)
            LbMessage.Text += IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                    "There is " & ARCnt.ToString & " unpaid",
                                    "未入金が " & ARCnt.ToString & " 件あります")
        End If

        '入金残データ取得
        Dim APClass = New SupplierAPList(_msgHd, _db, _langHd, Me)
        Dim APData As DataSet = APClass.getAPList()
        Dim APCnt As Integer = APData.Tables(RS).Rows.Count

        If APCnt > 0 Then

            LbMessage.Text += IIf(LbMessage.Text = "", "", N)
            LbMessage.Text += IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                    "There is " & APCnt.ToString & " payable",
                                    "未払金が " & APCnt.ToString & " 件あります")
        End If

        '出力するメッセージがない かつ ログインから来た場合はそのままメニューを開く
        If LbMessage.Text = "" And _parentForm.Name = "frmC01F10_Login" Then

            Dim openForm As Form = Nothing
            openForm = New frmC01F30_Menu(_msgHd, _langHd, _db)
            openForm.Show()
            Me.Dispose() '閉じる

        Else

            Me.Show() '表示する

            LbMessage.Text = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                 "There is no notification.",
                                 "お知らせはありません。")

        End If


    End Sub

    '戻るボタン押下
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click

        Dim openForm As Form = Nothing

        Console.WriteLine(_parentForm.Name)

        If _parentForm.Name = "frmC01F10_Login" Then

            openForm = New frmC01F30_Menu(_msgHd, _langHd, _db)
            openForm.Show()
            Me.Dispose()

        Else

            _parentForm.Enabled = True
            _parentForm.Show()
            Me.Dispose()

        End If

    End Sub


    '発注残数データ取得
    Public Function getJobOrderingRemainingList()

        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Sql = "SELECT t20.発注番号, t20.発注番号枝番, t21.行番号, t20.発注日, t20.仕入先名, t21.メーカー, t21.品名, t21.型式, t21.発注数量"
        Sql += ",t21.単位, t21.仕入単価, t20.ＶＡＴ, t21.仕入金額, t21.発注残数, t21.備考"
        Sql += " FROM  public.t21_hattyu t21 "
        Sql += " INNER JOIN  t20_hattyu t20"
        Sql += " ON t21.会社コード = t20.会社コード"
        Sql += " AND  t21.発注番号 = t20.発注番号"
        Sql += " AND  t21.発注番号枝番 = t20.発注番号枝番"

        Sql += " WHERE t21.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND "
        Sql += " t21.発注残数 <> 0 "
        Sql += " AND "
        Sql += " t20.取消区分 = 0 "
        Sql += " ORDER BY t20.発注日 DESC"

        Try

            Dim dsHattyudt As DataSet = _db.selectDB(Sql, RS, reccnt)

            Return dsHattyudt

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Function

End Class