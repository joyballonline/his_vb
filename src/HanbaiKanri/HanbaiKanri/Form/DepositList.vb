Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class DepositList
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
    Private _langHd As UtilLangHandler
    Private _db As UtilDBIf
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
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByRef prmRefLang As UtilLangHandler)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub


    Private Sub DepositList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Sql As String = ""
        Dim reccnt As Integer = 0

        Dim dsCustomer As DataSet = getDsData("m10_customer")

        Dim Count As Integer = 0
        Dim CustomerCount As Integer = dsCustomer.Tables(RS).Rows.Count

        Dim CustomerOrderCount As Integer
        Dim CustomerBillingCount As Integer
        Dim CustomerBillingAmount As Integer
        Dim CustomerOrderAmount As Integer
        Dim AccountsReceivable As Integer


        '得意先の一覧を取得
        For i As Integer = 0 To dsCustomer.Tables(RS).Rows.Count - 1

            Sql = " AND "
            Sql += "得意先コード"
            Sql += " ILIKE "
            Sql += "'%"
            Sql += dsCustomer.Tables(RS).Rows(i)("得意先コード")
            Sql += "%'"

            '得意先と一致する請求基本を取得
            Dim dsSkyuhd As DataSet = getDsData("t23_skyuhd", Sql)

            '得意先の請求データ数を取得
            CustomerBillingCount = dsSkyuhd.Tables(RS).Rows.Count.ToString

            Sql = " AND "
            Sql += "得意先コード"
            Sql += " ILIKE "
            Sql += "'%"
            Sql += dsCustomer.Tables(RS).Rows(i)("得意先コード")
            Sql += "%'"

            '得意先と一致する受注基本を取得
            Dim dsCymnhd As DataSet = getDsData("t10_cymnhd", Sql)

            '受注データ数
            CustomerOrderCount = dsCymnhd.Tables(RS).Rows.Count.ToString

            '請求金額を集計
            'CustomerBillingAmount = dsSkyuhd.Tables(RS).Compute("SUM(請求金額計)", Nothing)
            CustomerBillingAmount = IIf(
                dsSkyuhd.Tables(RS).Compute("SUM(請求金額計)", Nothing) IsNot DBNull.Value,
                dsSkyuhd.Tables(RS).Compute("SUM(請求金額計)", Nothing),
                0
            )

            '見積金額を集計
            'CustomerOrderAmount = dsCymnhd.Tables(RS).Compute("SUM(見積金額)", Nothing)
            CustomerOrderAmount = IIf(
                dsCymnhd.Tables(RS).Compute("SUM(見積金額)", Nothing) IsNot DBNull.Value,
                dsCymnhd.Tables(RS).Compute("SUM(見積金額)", Nothing),
                0
            )
            '売掛残高を集計
            'AccountsReceivable = dsSkyuhd.Tables(RS).Compute("SUM(売掛残高)", Nothing)
            AccountsReceivable = IIf(
                dsSkyuhd.Tables(RS).Compute("SUM(売掛残高)", Nothing) IsNot DBNull.Value,
                dsSkyuhd.Tables(RS).Compute("SUM(売掛残高)", Nothing),
                0
            )
            '表示エリアにデータを追加
            If CustomerOrderCount > 0 Then
                DgvCustomer.Rows.Add()
                DgvCustomer.Rows(Count).Cells("得意先名").Value = dsCustomer.Tables(RS).Rows(i)("得意先名")
                DgvCustomer.Rows(Count).Cells("請求金額残").Value = CustomerOrderAmount - CustomerBillingAmount
                DgvCustomer.Rows(Count).Cells("売掛残高").Value = AccountsReceivable
                DgvCustomer.Rows(Count).Cells("得意先コード").Value = dsCustomer.Tables(RS).Rows(i)("得意先コード")
                DgvCustomer.Rows(Count).Cells("会社コード").Value = dsCustomer.Tables(RS).Rows(i)("会社コード")

                Count += 1 'カウントアップ
            End If
        Next

        'Language=ENGの時
        If frmC01F10_Login.loginValue.Language = "ENG" Then
            LblConditions.Text = "TermsOfSelection"
            Label1.Text = "CustomerName"
            Label2.Text = "Address"
            Label3.Text = "PhoneNumber"
            Label4.Text = "CustomerCode"
            LblMode.Text = "MoneyReceiptInputMode"
            BtnSerach.Text = "Search"
            BtnDeposit.Text = "MoneyReceiptInput"
            btnBack.Text = "Back"
            DgvCustomer.Columns("得意先名").HeaderText = "CustomerName"
            DgvCustomer.Columns("請求金額残").HeaderText = "BillingBalance"
            DgvCustomer.Columns("売掛残高").HeaderText = "AccountsReceivableBalance"

        End If
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim frmMenu As frmC01F30_Menu
        frmMenu = New frmC01F30_Menu(_msgHd, _langHd, _db)
        frmMenu.Show()
        Me.Close()
    End Sub

    Private Sub BtnDeposit_Click(sender As Object, e As EventArgs) Handles BtnDeposit.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvCustomer.CurrentCell.RowIndex
        Dim Company As String = DgvCustomer.Rows(RowIdx).Cells("会社コード").Value
        Dim Customer As String = DgvCustomer.Rows(RowIdx).Cells("得意先コード").Value
        Dim Name As String = DgvCustomer.Rows(RowIdx).Cells("得意先名").Value
        Dim openForm As Form = Nothing
        openForm = New DepositManagement(_msgHd, _db, _langHd, Me, Company, Customer, Name)   '処理選択
        openForm.ShowDialog(Me)
        Me.Close()
    End Sub

    'param1：String テーブル名
    'param2：String 詳細条件
    'Return: DataSet
    Private Function getDsData(ByVal tableName As String, Optional ByRef txtParam As String = "") As DataSet
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Sql += "SELECT"
        Sql += " *"
        Sql += " FROM "

        Sql += "public." & tableName
        Sql += " WHERE "
        Sql += "会社コード"
        Sql += " ILIKE  "
        Sql += "'" & frmC01F10_Login.loginValue.BumonNM & "'"
        Sql += txtParam
        Return _db.selectDB(Sql, RS, reccnt)
    End Function

End Class