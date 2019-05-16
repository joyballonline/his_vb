Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Text.RegularExpressions

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
                   ByRef prmRefForm As Form)
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

    Private Sub getNukinList()

        Dim Sql As String = ""

        '一覧をクリア
        DgvCustomer.Rows.Clear()

        '検索条件を取得
        Sql = searchConditions()

        Dim dsCustomer As DataSet = getDsData("m10_customer", Sql)

        Dim Count As Integer = 0
        Dim CustomerCount As Integer = dsCustomer.Tables(RS).Rows.Count

        Dim CustomerOrderCount As Integer
        Dim CustomerBillingCount As Integer
        Dim CustomerBillingAmount As Long
        Dim CustomerOrderAmount As Long
        Dim AccountsReceivable As Integer

        'Language=ENGの時
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

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

        '得意先の一覧を取得
        For i As Integer = 0 To dsCustomer.Tables(RS).Rows.Count - 1

            Sql = " AND "
            Sql += "得意先コード"
            Sql += " ILIKE "
            Sql += "'%"
            Sql += dsCustomer.Tables(RS).Rows(i)("得意先コード")
            Sql += "%'"
            Sql += " AND "
            Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

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
            Sql += " AND "
            Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

            '得意先と一致する受注基本を取得
            Dim dsCymnhd As DataSet = getDsData("t10_cymnhd", Sql)

            '受注データ数
            CustomerOrderCount = dsCymnhd.Tables(RS).Rows.Count.ToString

            '請求金額を集計
            CustomerBillingAmount = IIf(
                dsSkyuhd.Tables(RS).Compute("SUM(請求金額計)", Nothing) IsNot DBNull.Value,
                dsSkyuhd.Tables(RS).Compute("SUM(請求金額計)", Nothing),
                0
            )

            '見積金額を集計
            CustomerOrderAmount = IIf(
                dsCymnhd.Tables(RS).Compute("SUM(見積金額)", Nothing) IsNot DBNull.Value,
                dsCymnhd.Tables(RS).Compute("SUM(見積金額)", Nothing),
                0
            )
            '売掛残高を集計
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

    End Sub

    '画面表示時
    Private Sub DepositList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '一覧取得
        getNukinList()
    End Sub

    '戻るボタン押下時
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    '入金入力ボタン押下時
    Private Sub BtnDeposit_Click(sender As Object, e As EventArgs) Handles BtnDeposit.Click

        '対象データがない場合は取消操作不可能
        If DgvCustomer.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Return

        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvCustomer.CurrentCell.RowIndex
        Dim Company As String = DgvCustomer.Rows(RowIdx).Cells("会社コード").Value
        Dim Customer As String = DgvCustomer.Rows(RowIdx).Cells("得意先コード").Value
        Dim Name As String = DgvCustomer.Rows(RowIdx).Cells("得意先名").Value
        Dim openForm As Form = Nothing
        openForm = New DepositManagement(_msgHd, _db, _langHd, Me, Company, Customer, Name)   '処理選択
        openForm.Show(Me)
        Me.Hide()
    End Sub

    '抽出条件取得
    Private Function searchConditions() As String
        Dim Sql As String = ""

        '抽出条件
        Dim customerName As String = escapeSql(TxtCustomerName.Text)
        Dim customerAddress As String = escapeSql(TxtAddress.Text)
        Dim customerTel As String = escapeSql(TxtTel.Text)
        Dim customerCode As String = escapeSql(TxtCustomerCode.Text)

        If customerName <> Nothing Then
            Sql += " AND "
            Sql += " 得意先名 ILIKE '%" & customerName & "%' "
        End If

        If customerAddress <> Nothing Then
            Sql += " AND "
            Sql += " (住所１ ILIKE '%" & customerAddress & "%' "
            Sql += " OR "
            Sql += " 住所２ ILIKE '%" & customerAddress & "%' "
            Sql += " OR "
            Sql += " 住所３ ILIKE '%" & customerAddress & "%' )"
        End If

        If customerTel <> Nothing Then
            Sql += " AND "
            Sql += " 電話番号検索用 ILIKE '%" & customerTel & "%' "
        End If

        If customerCode <> Nothing Then
            Sql += " AND "
            Sql += " 得意先コード ILIKE '%" & customerCode & "%' "
        End If

        Return Sql

    End Function

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
        Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += txtParam
        Return _db.selectDB(Sql, RS, reccnt)
    End Function

    '検索ボタン押下時
    Private Sub BtnSerach_Click(sender As Object, e As EventArgs) Handles BtnSerach.Click
        '一覧取得
        getNukinList()
    End Sub

    'sqlで実行する文字列からシングルクォーテーションを文字コードにする
    Private Function escapeSql(ByVal prmSql As String) As String
        Dim sql As String = prmSql

        sql = sql.Replace("'"c, "''") 'シングルクォーテーションを置換

        Return Regex.Escape(sql)
        Return sql
    End Function

    Private Sub DepositList_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        '一覧取得
        getNukinList()
    End Sub
End Class