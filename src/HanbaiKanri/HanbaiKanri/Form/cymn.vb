Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class Cymn
    Inherits System.Windows.Forms.Form

    '------------------------------------------------------------------------------------------------------
    'メンバー定数宣言
    '------------------------------------------------------------------------------------------------------
    'PG制御文字 
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
    Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ
    Private _parentForm As Form
    Private CompanyCode As String = ""
    Private QuoteNo As String = ""
    Private QuoteSuffix As String = ""
    Private OrderCount As String = ""

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
                   ByRef prmRefForm As Form,
                   Optional ByRef prmRefNo As String = Nothing,
                   Optional ByRef prmRefSuffix As String = Nothing)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
        QuoteNo = prmRefNo
        QuoteSuffix = prmRefSuffix

        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    Private Sub Quote_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'DateTimePickerのフォーマットを指定
        DtpOrderRegistration.Text = DateAdd("m", 0, Now).ToString("yyyy/MM/dd")
        DtpOrderDate.Text = DateAdd("m", 0, Now).ToString("yyyy/MM/dd")
        DtpExpiration.Text = DateAdd("d", 7, Now).ToString("yyyy/MM/dd")
        DtpPurchaseDate.Text = DateAdd("m", 0, Now).ToString("yyyy/MM/dd")
        DtpQuoteDate.Text = DateAdd("m", 0, Now).ToString("yyyy/MM/dd")
        DtpQuoteRegistration.Text = DateAdd("d", 7, Now).ToString("yyyy/MM/dd")

        DgvItemList.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        'セルの内容に合わせて、行の高さが自動的に調節されるようにする
        DgvItemList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        '"Column1"列のセルのテキストを折り返して表示する
        DgvItemList.Columns("型式").DefaultCellStyle.WrapMode = DataGridViewTriState.True

        'ComboBoxに表示する項目のリストを作成する
        Dim table As New DataTable("Table")
        table.Columns.Add("Display", GetType(String))
        table.Columns.Add("Value", GetType(Integer))
        table.Rows.Add("仕入", 1)
        table.Rows.Add("在庫", 2)
        table.Rows.Add("サービス", 9)

        'DataGridViewComboBoxColumnを作成
        Dim column As New DataGridViewComboBoxColumn()
        'DataGridViewComboBoxColumnのDataSourceを設定
        column.DataSource = table
        '実際の値が"Value"列、表示するテキストが"Display"列とする
        column.ValueMember = "Value"
        column.DisplayMember = "Display"
        column.HeaderText = "仕入区分"
        column.Name = "仕入区分"
        'column.ValueMember = 1
        'DataGridView1に追加する
        DgvItemList.Columns.Insert(1, column)

        Dim reccnt As Integer = 0
        Dim Sql12 As String = ""

        Sql12 += "SELECT "
        Sql12 += "* "
        Sql12 += "FROM "
        Sql12 += "public"
        Sql12 += "."
        Sql12 += "m90_hanyo"
        Sql12 += " WHERE "
        Sql12 += "会社コード"
        Sql12 += " ILIKE "
        Sql12 += "'"
        Sql12 += frmC01F10_Login.loginValue.BumonNM
        Sql12 += "'"
        Sql12 += " AND "
        Sql12 += "固定キー"
        Sql12 += " ILIKE "
        Sql12 += "'"
        Sql12 += "4"
        Sql12 += "'"

        Dim ds12 As DataSet = _db.selectDB(Sql12, RS, reccnt)

        Dim table2 As New DataTable("Table")
        table2.Columns.Add("Display", GetType(String))
        table2.Columns.Add("Value", GetType(Integer))

        For index As Integer = 0 To ds12.Tables(RS).Rows.Count - 1
            table2.Rows.Add(ds12.Tables(RS).Rows(index)("文字１"), ds12.Tables(RS).Rows(index)("可変キー"))
        Next

        Dim column2 As New DataGridViewComboBoxColumn()
        column2.DataSource = table2
        column2.ValueMember = "Value"
        column2.DisplayMember = "Display"
        column2.HeaderText = "リードタイム単位"
        column2.Name = "リードタイム単位"

        DgvItemList.Columns.Insert(24, column2)


        Dim SqlSaiban As String = ""
        SqlSaiban += "SELECT "
        SqlSaiban += "会社コード, "
        SqlSaiban += "採番キー, "
        SqlSaiban += "最新値, "
        SqlSaiban += "最小値, "
        SqlSaiban += "最大値, "
        SqlSaiban += "接頭文字, "
        SqlSaiban += "連番桁数 "
        SqlSaiban += "FROM "
        SqlSaiban += "public"
        SqlSaiban += "."
        SqlSaiban += "m80_saiban"
        SqlSaiban += " WHERE "
        SqlSaiban += "採番キー"
        SqlSaiban += " ILIKE "
        SqlSaiban += "'20'"

        Dim dtNow As DateTime = DateTime.Now
        Dim Saiban1 As DataSet = _db.selectDB(SqlSaiban, RS, reccnt)



        Dim SqlSaiban2 As String = ""
        SqlSaiban2 += "SELECT "
        SqlSaiban2 += "会社コード, "
        SqlSaiban2 += "採番キー, "
        SqlSaiban2 += "最新値, "
        SqlSaiban2 += "最小値, "
        SqlSaiban2 += "最大値, "
        SqlSaiban2 += "接頭文字, "
        SqlSaiban2 += "連番桁数 "
        SqlSaiban2 += "FROM "
        SqlSaiban2 += "public"
        SqlSaiban2 += "."
        SqlSaiban2 += "m80_saiban"
        SqlSaiban2 += " WHERE "
        SqlSaiban2 += "採番キー"
        SqlSaiban2 += " ILIKE "
        SqlSaiban2 += "'30'"

        Dim Saiban2 As DataSet = _db.selectDB(SqlSaiban2, RS, reccnt)

        CompanyCode = Saiban1.Tables(RS).Rows(0)(0)
        Dim OrderNo As String = Saiban1.Tables(RS).Rows(0)(5)
        OrderNo += dtNow.ToString("MMdd")
        OrderCount = Saiban1.Tables(RS).Rows(0)(2)
        OrderNo += OrderCount.PadLeft(Saiban1.Tables(RS).Rows(0)(6), "0")

        OrderCount += 1
        Dim Saiban3 As String = ""
        Saiban3 += "UPDATE "
        Saiban3 += "Public."
        Saiban3 += "m80_saiban "
        Saiban3 += "SET "
        Saiban3 += " 最新値"
        Saiban3 += " = '"
        Saiban3 += OrderCount.ToString
        Saiban3 += "', "
        Saiban3 += "更新者"
        Saiban3 += " = '"
        Saiban3 += "Admin"
        Saiban3 += "', "
        Saiban3 += "更新日"
        Saiban3 += " = '"
        Saiban3 += dtNow
        Saiban3 += "' "
        Saiban3 += "WHERE"
        Saiban3 += " 会社コード"
        Saiban3 += "='"
        Saiban3 += CompanyCode.ToString
        Saiban3 += "'"
        Saiban3 += " AND"
        Saiban3 += " 採番キー"
        Saiban3 += "='"
        Saiban3 += "20"
        Saiban3 += "' "
        Saiban3 += "RETURNING 会社コード"
        Saiban3 += ", "
        Saiban3 += "採番キー"
        Saiban3 += ", "
        Saiban3 += "最新値"
        Saiban3 += ", "
        Saiban3 += "最小値"
        Saiban3 += ", "
        Saiban3 += "最大値"
        Saiban3 += ", "
        Saiban3 += "接頭文字"
        Saiban3 += ", "
        Saiban3 += "連番桁数"
        Saiban3 += ", "
        Saiban3 += "更新者"
        Saiban3 += ", "
        Saiban3 += "更新日"

        _db.executeDB(Saiban3)

        '見積基本情報

        Dim Sql1 As String = ""
        Sql1 += "SELECT"
        Sql1 += " * "
        Sql1 += "FROM "
        Sql1 += "public"
        Sql1 += "."
        Sql1 += "t01_mithd"
        Sql1 += " WHERE "
        Sql1 += "見積番号"
        Sql1 += " ILIKE "
        Sql1 += "'"
        Sql1 += QuoteNo.ToString
        Sql1 += "'"
        Sql1 += " AND "
        Sql1 += "見積番号枝番"
        Sql1 += " ILIKE "
        Sql1 += "'"
        Sql1 += QuoteSuffix.ToString
        Sql1 += "'"

        Dim ds1 = _db.selectDB(Sql1, RS, reccnt)

        CompanyCode = ds1.Tables(RS).Rows(0)("会社コード")
        TxtOrderNo.Text = OrderNo
        TxtOrderSuffix.Text = 1
        DtpOrderRegistration.Value = dtNow
        DtpOrderDate.Value = dtNow
        DtpPurchaseDate.Value = dtNow
        If ds1.Tables(RS).Rows(0)("見積番号") IsNot DBNull.Value Then
            TxtQuoteNo.Text = ds1.Tables(RS).Rows(0)("見積番号")
        End If
        If ds1.Tables(RS).Rows(0)("見積番号枝番") IsNot DBNull.Value Then
            TxtQuoteSuffix.Text = ds1.Tables(RS).Rows(0)("見積番号枝番")
        End If
        If ds1.Tables(RS).Rows(0)("登録日") IsNot DBNull.Value Then
            DtpQuoteRegistration.Value = ds1.Tables(RS).Rows(0)("登録日")
        End If
        If ds1.Tables(RS).Rows(0)("見積日") IsNot DBNull.Value Then
            DtpQuoteDate.Value = ds1.Tables(RS).Rows(0)("見積日")
        End If
        If ds1.Tables(RS).Rows(0)("見積有効期限") IsNot DBNull.Value Then
            DtpExpiration.Value = ds1.Tables(RS).Rows(0)("見積有効期限")
        End If
        If ds1.Tables(RS).Rows(0)("得意先コード") IsNot DBNull.Value Then
            TxtCustomerCode.Text = ds1.Tables(RS).Rows(0)("得意先コード")
        End If
        If ds1.Tables(RS).Rows(0)("得意先名") IsNot DBNull.Value Then
            TxtCustomerName.Text = ds1.Tables(RS).Rows(0)("得意先名")
        End If
        If ds1.Tables(RS).Rows(0)("得意先担当者名") IsNot DBNull.Value Then
            TxtPerson.Text = ds1.Tables(RS).Rows(0)("得意先担当者名")
        End If
        If ds1.Tables(RS).Rows(0)("得意先担当者役職") IsNot DBNull.Value Then
            TxtPosition.Text = ds1.Tables(RS).Rows(0)("得意先担当者役職")
        End If
        If ds1.Tables(RS).Rows(0)("得意先郵便番号") IsNot DBNull.Value Then
            TxtPostalCode.Text = ds1.Tables(RS).Rows(0)("得意先郵便番号")
        End If
        If ds1.Tables(RS).Rows(0)("得意先住所") IsNot DBNull.Value Then
            Dim Address As String = ds1.Tables(RS).Rows(0)("得意先住所")
            Dim delimiter As String = " "
            Dim parts As String() = Split(Address, delimiter, -1, CompareMethod.Text)
            TxtAddress1.Text = parts(0).ToString
            TxtAddress2.Text = parts(1).ToString
            TxtAddress3.Text = parts(2).ToString
        End If
        If ds1.Tables(RS).Rows(0)("得意先電話番号") IsNot DBNull.Value Then
            TxtTel.Text = ds1.Tables(RS).Rows(0)("得意先電話番号")
        End If
        If ds1.Tables(RS).Rows(0)("得意先ＦＡＸ") IsNot DBNull.Value Then
            TxtFax.Text = ds1.Tables(RS).Rows(0)("得意先ＦＡＸ")
        End If
        If ds1.Tables(RS).Rows(0)("営業担当者") IsNot DBNull.Value Then
            TxtSales.Text = ds1.Tables(RS).Rows(0)("営業担当者")
        End If
        If ds1.Tables(RS).Rows(0)("入力担当者") IsNot DBNull.Value Then
            TxtInput.Text = ds1.Tables(RS).Rows(0)("入力担当者")
        End If
        If ds1.Tables(RS).Rows(0)("支払条件") IsNot DBNull.Value Then
            TxtPaymentTerms.Text = ds1.Tables(RS).Rows(0)("支払条件")
        End If
        If ds1.Tables(RS).Rows(0)("備考") IsNot DBNull.Value Then
            TxtQuoteRemarks.Text = ds1.Tables(RS).Rows(0)("備考")
        End If
        If ds1.Tables(RS).Rows(0)("ＶＡＴ") IsNot DBNull.Value Then
            TxtVat.Text = ds1.Tables(RS).Rows(0)("ＶＡＴ")
        End If
        If ds1.Tables(RS).Rows(0)("見積金額") IsNot DBNull.Value Then
            TxtOrderAmount.Text = ds1.Tables(RS).Rows(0)("見積金額")
        End If
        If ds1.Tables(RS).Rows(0)("仕入金額") IsNot DBNull.Value Then
            TxtPurchaseAmount.Text = ds1.Tables(RS).Rows(0)("仕入金額")
        End If
        If ds1.Tables(RS).Rows(0)("粗利額") IsNot DBNull.Value Then
            TxtGrossProfit.Text = ds1.Tables(RS).Rows(0)("粗利額")
        End If

        ''見積明細情報
        Dim Sql3 As String = ""
        Sql3 += "SELECT"
        Sql3 += " * "
        Sql3 += "FROM "
        Sql3 += "public"
        Sql3 += "."
        Sql3 += "t02_mitdt"
        Sql3 += " WHERE "
        Sql3 += "見積番号"
        Sql3 += " ILIKE "
        Sql3 += "'"
        Sql3 += QuoteNo.ToString
        Sql3 += "'"
        Sql3 += " AND "
        Sql3 += "見積番号枝番"
        Sql3 += " ILIKE "
        Sql3 += "'"
        Sql3 += QuoteSuffix.ToString
        Sql3 += "'"
        Dim ds3 = _db.selectDB(Sql3, RS, reccnt)

        Dim Sql4 As String = ""
        Sql4 += "SELECT"
        Sql4 += " * "
        Sql4 += "FROM "
        Sql4 += "public"
        Sql4 += "."
        Sql4 += "m01_company"
        Sql4 += " WHERE "
        Sql4 += "会社コード"
        Sql4 += " ILIKE "
        Sql4 += "'"
        Sql4 += frmC01F10_Login.loginValue.BumonNM
        Sql4 += "'"

        Dim ds4 As DataSet = _db.selectDB(Sql4, RS, reccnt)

        Dim tmp As Integer
        Dim tmp2 As Double
        Dim tmp3 As Double
        Dim tmp4 As Double
        Dim NoOverHead As Integer = 0
        Dim Total As Double = 0
        Dim PPH As Double = ds4.Tables(RS).Rows(0)("前払法人税率")

        Dim tmp5 As Integer
        For index As Integer = 0 To ds3.Tables(RS).Rows.Count - 1
            DgvItemList.Rows.Add()
            tmp = ds3.Tables(RS).Rows(index)("仕入区分")
            DgvItemList(1, index).Value = tmp
            DgvItemList.Rows(index).Cells(2).Value = ds3.Tables(RS).Rows(index)("メーカー")
            DgvItemList.Rows(index).Cells(3).Value = ds3.Tables(RS).Rows(index)("品名")
            DgvItemList.Rows(index).Cells(4).Value = ds3.Tables(RS).Rows(index)("型式")
            DgvItemList.Rows(index).Cells(5).Value = ds3.Tables(RS).Rows(index)("数量")
            DgvItemList.Rows(index).Cells(6).Value = ds3.Tables(RS).Rows(index)("単位")
            DgvItemList.Rows(index).Cells(7).Value = ds3.Tables(RS).Rows(index)("仕入先名称")
            DgvItemList.Rows(index).Cells(8).Value = ds3.Tables(RS).Rows(index)("仕入単価")
            DgvItemList.Rows(index).Cells(9).Value = ds3.Tables(RS).Rows(index)("仕入原価")
            DgvItemList.Rows(index).Cells(10).Value = ds3.Tables(RS).Rows(index)("関税率")
            DgvItemList.Rows(index).Cells(11).Value = ds3.Tables(RS).Rows(index)("関税額")
            DgvItemList.Rows(index).Cells(12).Value = ds3.Tables(RS).Rows(index)("前払法人税率")
            DgvItemList.Rows(index).Cells(13).Value = ds3.Tables(RS).Rows(index)("前払法人税額")
            DgvItemList.Rows(index).Cells(14).Value = ds3.Tables(RS).Rows(index)("輸送費率")
            DgvItemList.Rows(index).Cells(15).Value = ds3.Tables(RS).Rows(index)("輸送費額")
            DgvItemList.Rows(index).Cells(16).Value = ds3.Tables(RS).Rows(index)("仕入金額")
            DgvItemList.Rows(index).Cells(17).Value = ds3.Tables(RS).Rows(index)("売単価")
            DgvItemList.Rows(index).Cells(18).Value = ds3.Tables(RS).Rows(index)("売上金額")
            DgvItemList.Rows(index).Cells(19).Value = ds3.Tables(RS).Rows(index)("見積単価")
            DgvItemList.Rows(index).Cells(20).Value = ds3.Tables(RS).Rows(index)("見積金額")
            DgvItemList.Rows(index).Cells(21).Value = ds3.Tables(RS).Rows(index)("粗利額")
            DgvItemList.Rows(index).Cells(22).Value = ds3.Tables(RS).Rows(index)("粗利率")
            DgvItemList.Rows(index).Cells(23).Value = ds3.Tables(RS).Rows(index)("リードタイム")
            tmp5 = ds3.Tables(RS).Rows(index)("リードタイム単位")
            DgvItemList.Rows(index).Cells(24).Value = tmp5
            DgvItemList.Rows(index).Cells(25).Value = ds3.Tables(RS).Rows(index)("備考")
            DgvItemList.Rows(index).Cells(26).Value = ""

            tmp2 = ds3.Tables(RS).Rows(index)("関税率")
            tmp2 = tmp2 / 100
            tmp3 = ds3.Tables(RS).Rows(index)("仕入原価")
            tmp3 = tmp3 * tmp2
            tmp4 = ds3.Tables(RS).Rows(index)("仕入原価")
            tmp4 = tmp4 + tmp3
            Total += tmp4 * PPH
        Next

        TxtPph.Text = Total

        '行番号の振り直し
        Dim i As Integer = DgvItemList.Rows.Count()
        Dim No As Integer = 1
        For c As Integer = 0 To i - 1
            DgvItemList.Rows(c).Cells(0).Value = No
            No += 1
        Next c
        TxtItemCount.Text = DgvItemList.Rows.Count()

        '翻訳
        If frmC01F10_Login.loginValue.Language = "ENG" Then
            LblOrderNo.Text = "JobOrderNo"
            LblCustmerPO.Text = "CustomerNumber"
            LblCustmerPO.Location = New Point(273, 12)
            LblCustmerPO.Size = New Size(152, 23)
            TxtCustomerPO.Location = New Point(431, 12)
            LblOrderDate.Text = "JobOrderDate" '受注日
            LblOrderDate.Location = New Point(525, 12)
            DtpOrderDate.Location = New Point(643, 12)
            DtpOrderDate.Size = New Size(130, 22)
            LblPurchaseDate.Text = "OrderDate" '発注日
            LblPurchaseDate.Size = New Size(138, 23)
            LblPurchaseDate.Location = New Point(779, 12)
            DtpPurchaseDate.Location = New Point(923, 12)
            DtpPurchaseDate.Size = New Size(130, 22)
            LblRegistration.Text = "Ordering/Purchasing" '受発注登録日
            LblRegistration.Location = New Point(1059, 12)
            LblRegistration.Size = New Size(145, 23)
            DtpOrderRegistration.Location = New Point(1210, 12)
            DtpOrderRegistration.Size = New Size(130, 22)
            LblQuoteNo.Text = "QuotationNumber" '見積番号
            LblQuoteNo.Location = New Point(11, 42)
            LblQuoteNo.Size = New Size(162, 23)
            TxtQuoteNo.Location = New Point(179, 42)
            LblHyphen.Location = New Point(273, 49)
            TxtQuoteSuffix.Location = New Point(290, 42)
            LblQuoteRegistration.Text = "QuotationRegistrationDate"
            LblQuoteRegistration.Location = New Point(325, 42)
            LblQuoteRegistration.Size = New Size(215, 23)
            DtpQuoteRegistration.Location = New Point(546, 42)
            LblQuoteDate.Text = "QuotationDate"
            LblQuoteDate.Location = New Point(702, 42)
            DtpQuoteDate.Location = New Point(820, 42)
            LblExpiration.Text = "QuotationExpirationDate"
            LblExpiration.Size = New Size(205, 23)
            LblExpiration.Location = New Point(976, 42)
            DtpExpiration.Location = New Point(1187, 42)
            LblCustomerName.Text = "CustomerName"
            LblAddress.Text = "Address"
            LblTel.Text = "PhoneNumber"
            LblFax.Text = "FAX"
            LblPerson.Text = "NameOfPIC"
            LblPosition.Text = "PositionPICCustomer"
            LblSales.Text = "SalesPersonInCharge"
            LblInput.Text = "PICForInputting"
            LblPaymentTerms.Text = "PaymentTerms"
            TxtPaymentTerms.Location = New Point(181, 187)
            LblPaymentTerms.Size = New Size(162, 23)
            LblRemarks.Text = "QuotationRemarks"
            TxtQuoteRemarks.Location = New Point(181, 216)
            LblRemarks.Size = New Size(162, 23)
            LblItemCount.Text = "ItemCount"
            LblOrderRemarks.Text = "OrderRemarks"
            LblOrderRemarks.Size = New Size(161, 23)
            LblOrderRemarks.Location = New Point(11, 422)
            TxtOrderRemark.Location = New Point(178, 422)
            LblPurchaseRemarks.Text = "PurchaseOrderRemarks"
            LblPurchaseRemarks.Size = New Size(161, 23)
            LblPurchaseRemarks.Location = New Point(11, 451)
            TxtPurchaseRemark.Location = New Point(178, 451)
            LblPph.Text = "PPH"
            TxtPph.Size = New Size(151, 23)
            LblVAT.Text = "VAT"
            TxtVat.Size = New Size(151, 23)
            LblOrderAmount.Text = "JobOrderAmount" '受注金額
            LblOrderAmount.Size = New Size(180, 23)
            LblOrderAmount.Location = New Point(923, 422)
            LblPurchaseAmount.Text = "PurchaseOrderAmount"
            LblPurchaseAmount.Size = New Size(180, 23)
            LblPurchaseAmount.Location = New Point(923, 451)
            LblGrossProfit.Text = "GrossMargin"
            LblGrossProfit.Size = New Size(180, 23)
            LblGrossProfit.Location = New Point(923, 480)

            BtnRegistration.Text = "Registrartion"
            BtnBack.Text = "Back"

            DgvItemList.Columns("仕入区分").HeaderText = "PurchasingClassification"
            DgvItemList.Columns("メーカー").HeaderText = "Manufacturer"
            DgvItemList.Columns("品名").HeaderText = "ItemName"
            DgvItemList.Columns("型式").HeaderText = "Spec"
            DgvItemList.Columns("数量").HeaderText = "Quantity"
            DgvItemList.Columns("単位").HeaderText = "Unit"
            DgvItemList.Columns("仕入先").HeaderText = "SupplierName"
            DgvItemList.Columns("仕入単価").HeaderText = "PurchaseUnitPrice"
            DgvItemList.Columns("仕入原価").HeaderText = "PurchsingCost"
            DgvItemList.Columns("関税率").HeaderText = "CustomsDutyRate"
            DgvItemList.Columns("関税額").HeaderText = "CustomsDuty"
            DgvItemList.Columns("前払法人税率").HeaderText = "PrepaidCorporateTaxRate"
            DgvItemList.Columns("前払法人税額").HeaderText = "PrepaidCorporateTaxAmount"
            DgvItemList.Columns("輸送費率").HeaderText = "TransportationCostRate"
            DgvItemList.Columns("輸送費額").HeaderText = "TransportationCost"
            DgvItemList.Columns("仕入金額").HeaderText = "PurchaseAmount"
            DgvItemList.Columns("売単価").HeaderText = "SellingPrice"
            DgvItemList.Columns("売上金額").HeaderText = "SalesAmount"
            DgvItemList.Columns("見積単価").HeaderText = "QuotetionPrice"
            DgvItemList.Columns("見積金額").HeaderText = "QuotetionAmount"
            DgvItemList.Columns("粗利額").HeaderText = "GrossProfit"
            DgvItemList.Columns("粗利率").HeaderText = "GrossMarginRate(%)"
            DgvItemList.Columns("リードタイム").HeaderText = "LeadTime"
            DgvItemList.Columns("リードタイム単位").HeaderText = "LeadTimeUnit"
            DgvItemList.Columns("備考").HeaderText = "Remarks"
        End If


    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    Private Sub BtnRegistration_Click(sender As Object, e As EventArgs) Handles BtnRegistration.Click
        Dim errList(DgvItemList.Rows.Count - 1) As Integer
        Dim Sql As String = ""
        Dim reccnt As Integer = 0
        Dim itemCount As Integer = 0
        Dim errFlg As Boolean = True
        Dim dtNow As DateTime = DateTime.Now

        If TxtCustomerCode.Text = "stock" Then
        Else
            For i As Integer = 0 To DgvItemList.Rows.Count - 1
                If DgvItemList.Rows(i).Cells("仕入区分").Value = 2 Then
                    itemCount = 0
                    Sql = ""
                    Sql += "Select"
                    Sql += " * "
                    Sql += "FROM "
                    Sql += "Public"
                    Sql += "."
                    Sql += "t43_nyukodt"
                    Sql += " WHERE "
                    Sql += "メーカー"
                    Sql += " ILIKE "
                    Sql += "'"
                    Sql += DgvItemList.Rows(i).Cells("メーカー").Value
                    Sql += "'"
                    Sql += " AND "
                    Sql += "品名"
                    Sql += " ILIKE "
                    Sql += "'"
                    Sql += DgvItemList.Rows(i).Cells("品名").Value
                    Sql += "'"
                    Sql += " AND "
                    Sql += "型式"
                    Sql += " ILIKE "
                    Sql += "'"
                    Sql += DgvItemList.Rows(i).Cells("型式").Value
                    Sql += "'"
                    Dim ds1 = _db.selectDB(Sql, RS, reccnt)

                    For y As Integer = 0 To ds1.Tables(RS).Rows.Count - 1
                        itemCount += ds1.Tables(RS).Rows(y)("入庫数量")
                    Next

                    Sql = ""
                    Sql += "SELECT"
                    Sql += " * "
                    Sql += "FROM "
                    Sql += "public"
                    Sql += "."
                    Sql += "t45_shukodt"
                    Sql += " WHERE "
                    Sql += "メーカー"
                    Sql += " ILIKE "
                    Sql += "'"
                    Sql += DgvItemList.Rows(i).Cells("メーカー").Value
                    Sql += "'"
                    Sql += " AND "
                    Sql += "品名"
                    Sql += " ILIKE "
                    Sql += "'"
                    Sql += DgvItemList.Rows(i).Cells("品名").Value
                    Sql += "'"
                    Sql += " AND "
                    Sql += "型式"
                    Sql += " ILIKE "
                    Sql += "'"
                    Sql += DgvItemList.Rows(i).Cells("型式").Value
                    Sql += "'"
                    Dim ds2 = _db.selectDB(Sql, RS, reccnt)

                    For y As Integer = 0 To ds2.Tables(RS).Rows.Count - 1
                        itemCount -= ds2.Tables(RS).Rows(y)("出庫数量")
                    Next
                    itemCount -= DgvItemList.Rows(i).Cells("数量").Value
                    If itemCount < 0 Then
                        errFlg = False
                        errList(i) = Math.Abs(itemCount)
                    End If
                End If
            Next
            If errFlg Then
                Dim Sql1 As String = ""
                Sql1 = ""
                Sql1 += "INSERT INTO "
                Sql1 += "Public."
                Sql1 += "t10_cymnhd("
                Sql1 += "会社コード, 受注番号, 受注番号枝番, 客先番号, 見積番号, 見積番号枝番, 得意先コード, 得意先名, 得意先郵便番号, 得意先住所, 得意先電話番号, 得意先ＦＡＸ, 得意先担当者役職, 得意先担当者名, 見積日, 見積有効期限, 支払条件, 見積金額,仕入金額, 粗利額, 営業担当者, 入力担当者, 備考, 見積備考, ＶＡＴ, 受注日, 登録日, 更新日, 更新者, 取消区分)"
                Sql1 += " VALUES('"
                Sql1 += CompanyCode
                Sql1 += "', '"
                Sql1 += TxtOrderNo.Text
                Sql1 += "', '"
                Sql1 += TxtOrderSuffix.Text
                Sql1 += "', '"
                Sql1 += TxtCustomerPO.Text
                Sql1 += "', '"
                Sql1 += TxtQuoteNo.Text
                Sql1 += "', '"
                Sql1 += TxtQuoteSuffix.Text
                Sql1 += "', '"
                Sql1 += TxtCustomerCode.Text
                Sql1 += "', '"
                Sql1 += TxtCustomerName.Text
                Sql1 += "', '"
                Sql1 += TxtPostalCode.Text
                Sql1 += "', '"
                Sql1 += TxtAddress1.Text
                Sql1 += " "
                Sql1 += TxtAddress2.Text
                Sql1 += " "
                Sql1 += TxtAddress3.Text
                Sql1 += "', '"
                Sql1 += TxtTel.Text
                Sql1 += "', '"
                Sql1 += TxtFax.Text
                Sql1 += "', '"
                Sql1 += TxtPosition.Text
                Sql1 += "', '"
                Sql1 += TxtPerson.Text
                Sql1 += "', '"
                Sql1 += DtpQuoteDate.Value
                Sql1 += "', '"
                Sql1 += DtpExpiration.Value
                Sql1 += "', '"
                Sql1 += TxtPaymentTerms.Text
                Sql1 += "', '"
                Sql1 += TxtOrderAmount.Text
                Sql1 += "', '"
                Sql1 += TxtPurchaseAmount.Text
                Sql1 += "', '"
                Sql1 += TxtGrossProfit.Text
                Sql1 += "', '"
                Sql1 += TxtSales.Text
                Sql1 += "', '"
                Sql1 += TxtInput.Text
                Sql1 += "', '"
                Sql1 += TxtOrderRemark.Text
                Sql1 += "', '"
                Sql1 += TxtQuoteRemarks.Text
                Sql1 += "', '"
                Sql1 += TxtVat.Text
                Sql1 += "', '"
                Sql1 += DtpOrderDate.Value
                Sql1 += "', '"
                Sql1 += DtpOrderRegistration.Value
                Sql1 += "', '"
                Sql1 += dtNow
                Sql1 += "', '"
                Sql1 += "zenbi01"
                Sql1 += "', '"
                Sql1 += "0"
                Sql1 += " ')"
                Sql1 += "RETURNING 会社コード"
                Sql1 += ", "
                Sql1 += "受注番号"
                Sql1 += ", "
                Sql1 += "受注番号枝番"
                Sql1 += ", "
                Sql1 += "客先番号"
                Sql1 += ", "
                Sql1 += "見積番号"
                Sql1 += ", "
                Sql1 += "見積番号枝番"
                Sql1 += ", "
                Sql1 += "得意先コード"
                Sql1 += ", "
                Sql1 += "得意先名"
                Sql1 += ", "
                Sql1 += "得意先郵便番号"
                Sql1 += ", "
                Sql1 += "得意先住所"
                Sql1 += ", "
                Sql1 += "得意先電話番号"
                Sql1 += ", "
                Sql1 += "得意先ＦＡＸ"
                Sql1 += ", "
                Sql1 += "得意先担当者役職"
                Sql1 += ", "
                Sql1 += "得意先担当者名"
                Sql1 += ", "
                Sql1 += "見積日"
                Sql1 += ", "
                Sql1 += "見積有効期限"
                Sql1 += ", "
                Sql1 += "支払条件"
                Sql1 += ", "
                Sql1 += "見積金額"
                Sql1 += ", "
                Sql1 += "仕入金額"
                Sql1 += ", "
                Sql1 += "粗利額"
                Sql1 += ", "
                Sql1 += "営業担当者"
                Sql1 += ", "
                Sql1 += "入力担当者"
                Sql1 += ", "
                Sql1 += "備考"
                Sql1 += ", "
                Sql1 += "見積備考"
                Sql1 += ", "
                Sql1 += "ＶＡＴ"
                Sql1 += ", "
                Sql1 += "受注日"
                Sql1 += ", "
                Sql1 += "登録日"
                Sql1 += ", "
                Sql1 += "更新日"
                Sql1 += ", "
                Sql1 += "更新者"
                Sql1 += ", "
                Sql1 += "取消区分"

                _db.executeDB(Sql1)

                Dim Sql2 As String = ""
                For cymnhdIdx As Integer = 0 To DgvItemList.Rows.Count - 1
                    Sql2 = ""
                    Sql2 += "INSERT INTO "
                    Sql2 += "Public."
                    Sql2 += "t11_cymndt("
                    Sql2 += "会社コード, 受注番号, 受注番号枝番, 行番号, 仕入区分, メーカー, 品名, 型式, 単位, 仕入先名, 仕入値, 受注数量, 売上数量, 受注残数, 売単価, 売上金額, 見積単価, 見積金額, 粗利額, 粗利率, 間接費, リードタイム, リードタイム単位, 出庫数, 未出庫数, 備考, 更新者, 登録日, 関税率, 関税額, 前払法人税率, 前払法人税額, 輸送費率, 輸送費額, 仕入原価, 仕入金額)"
                    Sql2 += " VALUES('"
                    Sql2 += CompanyCode
                    Sql2 += "', '"
                    Sql2 += TxtOrderNo.Text
                    Sql2 += "', '"
                    Sql2 += TxtOrderSuffix.Text
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("No").Value.ToString
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("仕入区分").Value.ToString
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("メーカー").Value.ToString
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("品名").Value.ToString
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("型式").Value.ToString
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("単位").Value.ToString
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("仕入先").Value.ToString
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("仕入単価").Value.ToString
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("数量").Value.ToString
                    Sql2 += "', '"
                    Sql2 += "0"
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("数量").Value.ToString
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("売単価").Value.ToString
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("売上金額").Value.ToString
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("見積単価").Value.ToString
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("見積金額").Value.ToString
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("粗利額").Value.ToString
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("粗利率").Value.ToString
                    Sql2 += "', '"
                    Dim overhead As Double = 0
                    overhead = DgvItemList.Rows(cymnhdIdx).Cells("仕入金額").Value - DgvItemList.Rows(cymnhdIdx).Cells("仕入原価").Value
                    Sql2 += overhead.ToString
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("リードタイム").Value.ToString
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("リードタイム単位").Value.ToString
                    Sql2 += "', '"
                    Sql2 += "0"
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("数量").Value.ToString
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("備考").Value.ToString
                    Sql2 += "', '"
                    Sql2 += "zenbi01"
                    Sql2 += "', '"
                    Sql2 += dtNow
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("関税率").Value.ToString
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("関税額").Value.ToString
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("前払法人税率").Value.ToString
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("前払法人税額").Value.ToString
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("輸送費率").Value.ToString
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("輸送費額").Value.ToString
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("仕入原価").Value.ToString
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("仕入金額").Value.ToString

                    Sql2 += " ')"
                    Sql2 += "RETURNING 会社コード"
                    Sql2 += ", "
                    Sql2 += "受注番号"
                    Sql2 += ", "
                    Sql2 += "受注番号枝番"
                    Sql2 += ", "
                    Sql2 += "行番号"
                    Sql2 += ", "
                    Sql2 += "仕入区分"
                    Sql2 += ", "
                    Sql2 += "メーカー"
                    Sql2 += ", "
                    Sql2 += "品名"
                    Sql2 += ", "
                    Sql2 += "型式"
                    Sql2 += ", "
                    Sql2 += "単位"
                    Sql2 += ", "
                    Sql2 += "仕入先名"
                    Sql2 += ", "
                    Sql2 += "仕入値"
                    Sql2 += ", "
                    Sql2 += "受注数量"
                    Sql2 += ", "
                    Sql2 += "売上数量"
                    Sql2 += ", "
                    Sql2 += "受注残数"
                    Sql2 += ", "
                    Sql2 += "売単価"
                    Sql2 += ", "
                    Sql2 += "売上金額"
                    Sql2 += ", "
                    Sql2 += "見積単価"
                    Sql2 += ", "
                    Sql2 += "見積金額"
                    Sql2 += ", "
                    Sql2 += "粗利額"
                    Sql2 += ", "
                    Sql2 += "粗利率"
                    Sql2 += ", "
                    Sql2 += "間接費"
                    Sql2 += ", "
                    Sql2 += "リードタイム"
                    Sql2 += ", "
                    Sql2 += "出庫数"
                    Sql2 += ", "
                    Sql2 += "未出庫数"
                    Sql2 += ", "
                    Sql2 += "備考"
                    Sql2 += ", "
                    Sql2 += "更新者"
                    Sql2 += ", "
                    Sql2 += "登録日"
                    Sql2 += ", "
                    Sql2 += "関税率"
                    Sql2 += ", "
                    Sql2 += "関税額"
                    Sql2 += ", "
                    Sql2 += "前払法人税率"
                    Sql2 += ", "
                    Sql2 += "前払法人税額"
                    Sql2 += ", "
                    Sql2 += "輸送費率"
                    Sql2 += ", "
                    Sql2 += "輸送費額"
                    Sql2 += ", "
                    Sql2 += "仕入原価"
                    Sql2 += ", "
                    Sql2 += "仕入金額"

                    _db.executeDB(Sql2)

                    Sql2 = ""
                Next
#Region "出庫"
                Sql = ""
                Sql += "SELECT "
                Sql += "* "
                Sql += "FROM "
                Sql += "public"
                Sql += "."
                Sql += "m80_saiban"
                Sql += " WHERE "
                Sql += "採番キー"
                Sql += " ILIKE "
                Sql += "'"
                Sql += "70"
                Sql += "'"
                Dim dsSaiban As DataSet = _db.selectDB(Sql, RS, reccnt)

                Dim LS As String = dsSaiban.Tables(RS).Rows(0)("接頭文字")
                LS += dtNow.ToString("MMdd")
                LS += dsSaiban.Tables(RS).Rows(0)("最新値").ToString.PadLeft(dsSaiban.Tables(RS).Rows(0)("連番桁数"), "0")

                Sql = ""
                Sql += "INSERT INTO "
                Sql += "Public."
                Sql += "t44_shukohd("
                Sql += "会社コード, 出庫番号, 見積番号, 見積番号枝番, 受注番号, 受注番号枝番, 客先番号, 得意先コード, 得意先名, 得意先郵便番号, 得意先住所, 得意先電話番号, 得意先ＦＡＸ, 得意先担当者役職, 得意先担当者名, 営業担当者, 入力担当者, 備考, 取消日, 取消区分, 出庫日, 登録日, 更新日, 更新者)"
                Sql += " VALUES('"
                Sql += frmC01F10_Login.loginValue.BumonNM
                Sql += "', '"
                Sql += LS
                Sql += "', '"
                Sql += TxtQuoteNo.Text
                Sql += "', '"
                Sql += TxtQuoteSuffix.Text
                Sql += "', '"
                Sql += TxtOrderNo.Text
                Sql += "', '"
                Sql += TxtOrderSuffix.Text
                Sql += "', '"
                Sql += TxtCustomerPO.Text
                Sql += "', '"
                Sql += TxtCustomerCode.Text
                Sql += "', '"
                Sql += TxtCustomerName.Text
                Sql += "', '"
                Sql += TxtPostalCode.Text
                Sql += "', '"
                Sql += TxtAddress1.Text
                Sql += " "
                Sql += TxtAddress2.Text
                Sql += " "
                Sql += TxtAddress3.Text
                Sql += "', '"
                Sql += TxtTel.Text
                Sql += "', '"
                Sql += TxtFax.Text
                Sql += "', '"
                Sql += TxtPosition.Text
                Sql += "', '"
                Sql += TxtPerson.Text
                Sql += "', '"
                Sql += TxtSales.Text
                Sql += "', '"
                Sql += TxtInput.Text
                Sql += "', '"
                Sql += TxtOrderRemark.Text
                Sql += "', "
                Sql += "null"
                Sql += ", "
                Sql += "null"
                Sql += ", '"
                Sql += dtNow
                Sql += "', '"
                Sql += dtNow
                Sql += "', '"
                Sql += dtNow
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoNM
                Sql += " ')"
                Sql += "RETURNING 会社コード"
                Sql += ", "
                Sql += "出庫番号"
                Sql += ", "
                Sql += "見積番号"
                Sql += ", "
                Sql += "見積番号枝番"
                Sql += ", "
                Sql += "受注番号"
                Sql += ", "
                Sql += "受注番号枝番"
                Sql += ", "
                Sql += "客先番号"
                Sql += ", "
                Sql += "得意先コード"
                Sql += ", "
                Sql += "得意先名"
                Sql += ", "
                Sql += "得意先郵便番号"
                Sql += ", "
                Sql += "得意先住所"
                Sql += ", "
                Sql += "得意先電話番号"
                Sql += ", "
                Sql += "得意先ＦＡＸ"
                Sql += ", "
                Sql += "得意先担当者役職"
                Sql += ", "
                Sql += "得意先担当者名"
                Sql += ", "
                Sql += "営業担当者"
                Sql += ", "
                Sql += "入力担当者"
                Sql += ", "
                Sql += "備考"
                Sql += ", "
                Sql += "取消日"
                Sql += ", "
                Sql += "取消区分"
                Sql += ", "
                Sql += "出庫日"
                Sql += ", "
                Sql += "登録日"
                Sql += ", "
                Sql += "更新日"
                Sql += ", "
                Sql += "更新者"

                _db.executeDB(Sql)

                For index As Integer = 0 To DgvItemList.Rows.Count() - 1
                    Sql = ""
                    Sql += "INSERT INTO "
                    Sql += "Public."
                    Sql += "t45_shukodt("
                    Sql += "会社コード, 出庫番号, 受注番号, 受注番号枝番, 行番号, 仕入区分, メーカー, 品名, 型式, 仕入先名, 売単価, 出庫数量, 単位, 備考, 更新者, 更新日, 出庫区分)"
                    Sql += " VALUES('"
                    Sql += frmC01F10_Login.loginValue.BumonNM
                    Sql += "', '"
                    Sql += LS
                    Sql += "', '"
                    Sql += TxtOrderNo.Text
                    Sql += "', '"
                    Sql += TxtOrderSuffix.Text
                    Sql += "', '"
                    Sql += DgvItemList.Rows(index).Cells("No").Value.ToString
                    Sql += "', '"
                    Sql += DgvItemList.Rows(index).Cells("仕入区分").Value.ToString
                    Sql += "', '"
                    Sql += DgvItemList.Rows(index).Cells("メーカー").Value.ToString
                    Sql += "', '"
                    Sql += DgvItemList.Rows(index).Cells("品名").Value.ToString
                    Sql += "', '"
                    Sql += DgvItemList.Rows(index).Cells("型式").Value.ToString
                    Sql += "', '"
                    Sql += DgvItemList.Rows(index).Cells("仕入先").Value.ToString
                    Sql += "', '"
                    Sql += DgvItemList.Rows(index).Cells("売単価").Value.ToString
                    Sql += "', '"
                    Sql += DgvItemList.Rows(index).Cells("数量").Value.ToString
                    Sql += "', '"
                    Sql += DgvItemList.Rows(index).Cells("単位").Value.ToString
                    Sql += "', '"
                    Sql += DgvItemList.Rows(index).Cells("備考").Value.ToString
                    Sql += "', '"
                    Sql += frmC01F10_Login.loginValue.TantoNM
                    Sql += "', '"
                    Sql += dtNow
                    Sql += "', '"
                    Sql += "1"
                    Sql += " ')"
                    Sql += "RETURNING 会社コード"
                    Sql += ", "
                    Sql += "出庫番号"
                    Sql += ", "
                    Sql += "受注番号"
                    Sql += ", "
                    Sql += "受注番号枝番"
                    Sql += ", "
                    Sql += "行番号"
                    Sql += ", "
                    Sql += "仕入区分"
                    Sql += ", "
                    Sql += "メーカー"
                    Sql += ", "
                    Sql += "品名"
                    Sql += ", "
                    Sql += "型式"
                    Sql += ", "
                    Sql += "仕入先名"
                    Sql += ", "
                    Sql += "売単価"
                    Sql += ", "
                    Sql += "出庫数量"
                    Sql += ", "
                    Sql += "単位"
                    Sql += ", "
                    Sql += "備考"
                    Sql += ", "
                    Sql += "更新者"
                    Sql += ", "
                    Sql += "更新日"
                    Sql += ", "
                    Sql += "出庫区分"

                    _db.executeDB(Sql)

                Next

                Dim LSNo As Integer

                If dsSaiban.Tables(RS).Rows(0)("最新値") = dsSaiban.Tables(RS).Rows(0)("最大値") Then
                    LSNo = dsSaiban.Tables(RS).Rows(0)("最小値")
                Else
                    LSNo = dsSaiban.Tables(RS).Rows(0)("最新値") + 1
                End If

                Sql = ""
                Sql += "UPDATE "
                Sql += "Public."
                Sql += "m80_saiban "
                Sql += "SET "
                Sql += " 最新値"
                Sql += " = '"
                Sql += LSNo.ToString
                Sql += "', "
                Sql += "更新者"
                Sql += " = '"
                Sql += frmC01F10_Login.loginValue.TantoNM
                Sql += "', "
                Sql += "更新日"
                Sql += " = '"
                Sql += dtNow
                Sql += "' "
                Sql += "WHERE"
                Sql += " 会社コード"
                Sql += "='"
                Sql += frmC01F10_Login.loginValue.BumonNM
                Sql += "'"
                Sql += " AND"
                Sql += " 採番キー"
                Sql += "='"
                Sql += "70"
                Sql += "' "
                Sql += "RETURNING 会社コード"
                Sql += ", "
                Sql += "採番キー"
                Sql += ", "
                Sql += "最新値"
                Sql += ", "
                Sql += "最小値"
                Sql += ", "
                Sql += "最大値"
                Sql += ", "
                Sql += "接頭文字"
                Sql += ", "
                Sql += "連番桁数"
                Sql += ", "
                Sql += "更新者"
                Sql += ", "
                Sql += "更新日"

                _db.executeDB(Sql)
#End Region
            Else
                For i As Integer = 0 To DgvItemList.Rows.Count - 1
                    Dim itemNum As Integer = 0
                    If errList(i) = Nothing Then
                    Else
                        itemNum = i + 1
                        If frmC01F10_Login.loginValue.Language = "ENG" Then
                            MessageBox.Show("Item is missing" + errList(i).ToString + "at line " + itemNum.ToString + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Else
                            MessageBox.Show(itemNum.ToString + "行目の商品在庫数量が" + errList(i).ToString + "不足しています。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If

                    End If

                Next

            End If
        End If

        If errFlg Then
            Dim SupplierList As New List(Of String)(New String() {})
            For i As Integer = 0 To DgvItemList.Rows.Count - 1
                If SupplierList.Contains(DgvItemList.Rows(i).Cells("仕入先").Value) = False And DgvItemList.Rows(i).Cells("仕入区分").Value = 1 Then
                    SupplierList.Add(DgvItemList.Rows(i).Cells("仕入先").Value)
                Else

                End If
            Next

            Dim SqlSaiban As String = ""
            SqlSaiban += "SELECT "
            SqlSaiban += "会社コード, "
            SqlSaiban += "採番キー, "
            SqlSaiban += "最新値, "
            SqlSaiban += "最小値, "
            SqlSaiban += "最大値, "
            SqlSaiban += "接頭文字, "
            SqlSaiban += "連番桁数 "
            SqlSaiban += "FROM "
            SqlSaiban += "public"
            SqlSaiban += "."
            SqlSaiban += "m80_saiban"
            SqlSaiban += " WHERE "
            SqlSaiban += "採番キー"
            SqlSaiban += " ILIKE "
            SqlSaiban += "'30'"

            Dim Saiban As DataSet = _db.selectDB(SqlSaiban, RS, reccnt)

            Dim PurchaseCount As String = Saiban.Tables(RS).Rows(0)(2)
            Dim supplier As String = ""

            For s As Integer = 0 To SupplierList.Count - 1
                supplier = SupplierList(s)

                Sql = ""
                Sql += "SELECT"
                Sql += " * "
                Sql += "FROM "
                Sql += "public"
                Sql += "."
                Sql += "m11_supplier"
                Sql += " WHERE "
                Sql += "仕入先名"
                Sql += " ILIKE "
                Sql += "'"
                Sql += supplier
                Sql += "'"
                Dim ds1 As DataSet = _db.selectDB(Sql, RS, reccnt)

                Dim cost As Integer = 0
                For i As Integer = 0 To DgvItemList.Rows.Count - 1
                    If DgvItemList.Rows(i).Cells("仕入先").Value = supplier Then
                        cost += DgvItemList.Rows(i).Cells("仕入原価").Value
                    End If
                Next

                Dim PurchaseNo As String = Saiban.Tables(RS).Rows(0)(5)
                PurchaseNo += dtNow.ToString("MMdd")
                PurchaseNo += PurchaseCount.PadLeft(Saiban.Tables(RS).Rows(0)(6), "0")

                Dim Sql3 As String = ""
                Sql3 = ""
                Sql3 += "INSERT INTO "
                Sql3 += "Public."
                Sql3 += "t20_hattyu("
                Sql3 += "会社コード, 発注番号, 発注番号枝番, 客先番号, 受注番号, 受注番号枝番, 見積番号, 見積番号枝番, 得意先コード, 得意先名, 得意先郵便番号, 得意先住所, 得意先電話番号, 得意先ＦＡＸ, 得意先担当者役職, 得意先担当者名, 仕入先コード, 仕入先名, 仕入先郵便番号, 仕入先住所, 仕入先電話番号, 仕入先ＦＡＸ, 仕入先担当者役職, 仕入先担当者名, 見積日, 見積有効期限, 支払条件, 見積金額,仕入金額, 粗利額, 営業担当者, 入力担当者, 備考, 見積備考, ＶＡＴ, ＰＰＨ, 受注日, 発注日, 登録日, 更新日, 更新者, 取消区分)"
                Sql3 += " VALUES('"
                Sql3 += CompanyCode
                Sql3 += "', '"
                Sql3 += PurchaseNo
                Sql3 += "', '"
                Sql3 += "1"
                Sql3 += "', '"
                Sql3 += TxtCustomerPO.Text
                Sql3 += "', '"
                Sql3 += TxtOrderNo.Text
                Sql3 += "', '"
                Sql3 += TxtOrderSuffix.Text
                Sql3 += "', '"
                Sql3 += TxtQuoteNo.Text
                Sql3 += "', '"
                Sql3 += TxtQuoteSuffix.Text
                Sql3 += "', '"
                Sql3 += TxtCustomerCode.Text
                Sql3 += "', '"
                Sql3 += TxtCustomerName.Text
                Sql3 += "', '"
                Sql3 += TxtPostalCode.Text
                Sql3 += "', '"
                Sql3 += TxtAddress1.Text
                Sql3 += " "
                Sql3 += TxtAddress2.Text
                Sql3 += " "
                Sql3 += TxtAddress3.Text
                Sql3 += "', '"
                Sql3 += TxtTel.Text
                Sql3 += "', '"
                Sql3 += TxtFax.Text
                Sql3 += "', '"
                Sql3 += TxtPosition.Text
                Sql3 += "', '"
                Sql3 += TxtPerson.Text
                Sql3 += "', '"
                Sql3 += ds1.Tables(RS).Rows(0)("仕入先コード").ToString
                Sql3 += "', '"
                Sql3 += ds1.Tables(RS).Rows(0)("仕入先名").ToString
                Sql3 += "', '"
                Sql3 += ds1.Tables(RS).Rows(0)("郵便番号").ToString
                Sql3 += "', '"
                Sql3 += ds1.Tables(RS).Rows(0)("住所１").ToString
                Sql3 += " "
                Sql3 += ds1.Tables(RS).Rows(0)("住所２").ToString
                Sql3 += " "
                Sql3 += ds1.Tables(RS).Rows(0)("住所３").ToString
                Sql3 += "', '"
                Sql3 += ds1.Tables(RS).Rows(0)("電話番号").ToString
                Sql3 += "', '"
                Sql3 += ds1.Tables(RS).Rows(0)("ＦＡＸ番号").ToString
                Sql3 += "', '"
                Sql3 += ds1.Tables(RS).Rows(0)("担当者役職").ToString
                Sql3 += "', '"
                Sql3 += ds1.Tables(RS).Rows(0)("担当者名").ToString
                Sql3 += "', '"
                Sql3 += DtpQuoteDate.Value
                Sql3 += "', '"
                Sql3 += DtpExpiration.Value
                Sql3 += "', '"
                Sql3 += TxtPaymentTerms.Text
                Sql3 += "', '"
                Sql3 += TxtOrderAmount.Text
                Sql3 += "', '"
                Sql3 += cost.ToString
                Sql3 += "', '"
                Sql3 += TxtGrossProfit.Text
                Sql3 += "', '"
                Sql3 += TxtSales.Text
                Sql3 += "', '"
                Sql3 += TxtInput.Text
                Sql3 += "', '"
                Sql3 += TxtPurchaseRemark.Text
                Sql3 += "', '"
                Sql3 += TxtQuoteRemarks.Text
                Sql3 += "', '"
                Sql3 += TxtVat.Text
                Sql3 += "', '"
                Sql3 += TxtPph.Text
                Sql3 += "', '"
                Sql3 += DtpOrderDate.Value
                Sql3 += "', '"
                Sql3 += DtpPurchaseDate.Value
                Sql3 += "', '"
                Sql3 += DtpOrderRegistration.Value
                Sql3 += "', '"
                Sql3 += dtNow
                Sql3 += "', '"
                Sql3 += "zenbi01"
                Sql3 += "', '"
                Sql3 += "0"
                Sql3 += " ')"
                Sql3 += "RETURNING 会社コード"
                Sql3 += ", "
                Sql3 += "発注番号"
                Sql3 += ", "
                Sql3 += "発注番号枝番"
                Sql3 += ", "
                Sql3 += "客先番号"
                Sql3 += ", "
                Sql3 += "受注番号"
                Sql3 += ", "
                Sql3 += "受注番号枝番"
                Sql3 += ", "
                Sql3 += "見積番号"
                Sql3 += ", "
                Sql3 += "見積番号枝番"
                Sql3 += ", "
                Sql3 += "得意先コード"
                Sql3 += ", "
                Sql3 += "得意先名"
                Sql3 += ", "
                Sql3 += "得意先郵便番号"
                Sql3 += ", "
                Sql3 += "得意先住所"
                Sql3 += ", "
                Sql3 += "得意先電話番号"
                Sql3 += ", "
                Sql3 += "得意先ＦＡＸ"
                Sql3 += ", "
                Sql3 += "得意先担当者役職"
                Sql3 += ", "
                Sql3 += "得意先担当者名"
                Sql3 += ", "
                Sql3 += "仕入先コード"
                Sql3 += ", "
                Sql3 += "仕入先名"
                Sql3 += ", "
                Sql3 += "仕入先郵便番号"
                Sql3 += ", "
                Sql3 += "仕入先住所"
                Sql3 += ", "
                Sql3 += "仕入先電話番号"
                Sql3 += ", "
                Sql3 += "仕入先ＦＡＸ"
                Sql3 += ", "
                Sql3 += "仕入先担当者役職"
                Sql3 += ", "
                Sql3 += "仕入先担当者名"
                Sql3 += ", "
                Sql3 += "見積日"
                Sql3 += ", "
                Sql3 += "見積有効期限"
                Sql3 += ", "
                Sql3 += "支払条件"
                Sql3 += ", "
                Sql3 += "見積金額"
                Sql3 += ", "
                Sql3 += "仕入金額"
                Sql3 += ", "
                Sql3 += "粗利額"
                Sql3 += ", "
                Sql3 += "営業担当者"
                Sql3 += ", "
                Sql3 += "入力担当者"
                Sql3 += ", "
                Sql3 += "備考"
                Sql3 += ", "
                Sql3 += "見積備考"
                Sql3 += ", "
                Sql3 += "ＶＡＴ"
                Sql3 += ", "
                Sql3 += "ＰＰＨ"
                Sql3 += ", "
                Sql3 += "受注日"
                Sql3 += ", "
                Sql3 += "発注日"
                Sql3 += ", "
                Sql3 += "登録日"
                Sql3 += ", "
                Sql3 += "更新日"
                Sql3 += ", "
                Sql3 += "更新者"
                Sql3 += ", "
                Sql3 += "取消区分"

                _db.executeDB(Sql3)
                Sql3 = ""
                Dim Sql4 As String = ""
                Dim test As String = ""
                For hattyuIdx As Integer = 0 To DgvItemList.Rows.Count - 1
                    If DgvItemList.Rows(hattyuIdx).Cells("仕入先").Value = supplier And DgvItemList.Rows(hattyuIdx).Cells("仕入区分").Value = 1 Then
                        Sql4 = ""
                        Sql4 += "INSERT INTO "
                        Sql4 += "Public."
                        Sql4 += "t21_hattyu("
                        Sql4 += "会社コード, 発注番号, 発注番号枝番, 行番号, 仕入区分, メーカー, 品名, 型式, 単位, 仕入先名, 仕入値, 発注数量, 仕入数量, 発注残数, 仕入単価, 仕入金額, 間接費, リードタイム, リードタイム単位, 入庫数, 未入庫数, 備考, 更新者, 登録日)"
                        Sql4 += " VALUES('"
                        Sql4 += CompanyCode
                        Sql4 += "', '"
                        Sql4 += PurchaseNo
                        Sql4 += "', '"
                        Sql4 += "1"
                        Sql4 += "', '"
                        Sql4 += DgvItemList.Rows(hattyuIdx).Cells("No").Value.ToString
                        Sql4 += "', '"
                        Sql4 += DgvItemList.Rows(hattyuIdx).Cells("仕入区分").Value.ToString
                        Sql4 += "', '"
                        Sql4 += DgvItemList.Rows(hattyuIdx).Cells("メーカー").Value.ToString
                        Sql4 += "', '"
                        Sql4 += DgvItemList.Rows(hattyuIdx).Cells("品名").Value.ToString
                        Sql4 += "', '"
                        Sql4 += DgvItemList.Rows(hattyuIdx).Cells("型式").Value.ToString
                        Sql4 += "', '"
                        Sql4 += DgvItemList.Rows(hattyuIdx).Cells("単位").Value.ToString
                        Sql4 += "', '"
                        Sql4 += DgvItemList.Rows(hattyuIdx).Cells("仕入先").Value.ToString
                        Sql4 += "', '"
                        Sql4 += DgvItemList.Rows(hattyuIdx).Cells("仕入単価").Value.ToString
                        Sql4 += "', '"
                        Sql4 += DgvItemList.Rows(hattyuIdx).Cells("数量").Value.ToString
                        Sql4 += "', '"
                        Sql4 += "0"
                        Sql4 += "', '"
                        Sql4 += DgvItemList.Rows(hattyuIdx).Cells("数量").Value.ToString
                        Sql4 += "', '"
                        Sql4 += DgvItemList.Rows(hattyuIdx).Cells("売単価").Value.ToString
                        Sql4 += "', '"
                        Sql4 += DgvItemList.Rows(hattyuIdx).Cells("仕入原価").Value.ToString
                        Sql4 += "', '"
                        Dim overhead As Double = 0
                        overhead = DgvItemList.Rows(hattyuIdx).Cells("仕入金額").Value - DgvItemList.Rows(hattyuIdx).Cells("仕入原価").Value
                        Sql4 += overhead.ToString
                        Sql4 += "', '"
                        Sql4 += DgvItemList.Rows(hattyuIdx).Cells("リードタイム").Value.ToString
                        Sql4 += "', '"
                        Sql4 += DgvItemList.Rows(hattyuIdx).Cells("リードタイム単位").Value.ToString
                        'Sql4 += DgvItemList.Item("リードタイム単位", hattyuIdx).FormattedValue.ToString
                        Sql4 += "', '"
                        Sql4 += "0"
                        Sql4 += "', '"
                        Sql4 += DgvItemList.Rows(hattyuIdx).Cells("数量").Value.ToString
                        Sql4 += "', '"
                        Sql4 += DgvItemList.Rows(hattyuIdx).Cells("備考").Value.ToString
                        Sql4 += "', '"
                        Sql4 += "zenbi01"
                        Sql4 += "', '"
                        Sql4 += dtNow
                        Sql4 += " ')"
                        Sql4 += "RETURNING 会社コード"
                        Sql4 += ", "
                        Sql4 += "発注番号"
                        Sql4 += ", "
                        Sql4 += "発注番号枝番"
                        Sql4 += ", "
                        Sql4 += "行番号"
                        Sql4 += ", "
                        Sql4 += "仕入区分"
                        Sql4 += ", "
                        Sql4 += "メーカー"
                        Sql4 += ", "
                        Sql4 += "品名"
                        Sql4 += ", "
                        Sql4 += "型式"
                        Sql4 += ", "
                        Sql4 += "単位"
                        Sql4 += ", "
                        Sql4 += "仕入先名"
                        Sql4 += ", "
                        Sql4 += "仕入値"
                        Sql4 += ", "
                        Sql4 += "発注数量"
                        Sql4 += ", "
                        Sql4 += "仕入数量"
                        Sql4 += ", "
                        Sql4 += "発注残数"
                        Sql4 += ", "
                        Sql4 += "仕入単価"
                        Sql4 += ", "
                        Sql4 += "仕入金額"
                        Sql4 += ", "
                        Sql4 += "リードタイム"
                        Sql4 += ", "
                        Sql4 += "リードタイム単位"
                        Sql4 += ", "
                        Sql4 += "入庫数"
                        Sql4 += ", "
                        Sql4 += "未入庫数"
                        Sql4 += ", "
                        Sql4 += "備考"
                        Sql4 += ", "
                        Sql4 += "更新者"
                        Sql4 += ", "
                        Sql4 += "登録日"

                        _db.executeDB(Sql4)
                    End If
                Next
                PurchaseCount += 1
            Next

            PurchaseCount += 1
            Dim Saiban4 As String = ""
            Saiban4 += "UPDATE "
            Saiban4 += "Public."
            Saiban4 += "m80_saiban "
            Saiban4 += "SET "
            Saiban4 += " 最新値"
            Saiban4 += " = '"
            Saiban4 += PurchaseCount.ToString
            Saiban4 += "', "
            Saiban4 += "更新者"
            Saiban4 += " = '"
            Saiban4 += "Admin"
            Saiban4 += "', "
            Saiban4 += "更新日"
            Saiban4 += " = '"
            Saiban4 += dtNow
            Saiban4 += "' "
            Saiban4 += "WHERE"
            Saiban4 += " 会社コード"
            Saiban4 += "='"
            Saiban4 += CompanyCode.ToString
            Saiban4 += "'"
            Saiban4 += " AND"
            Saiban4 += " 採番キー"
            Saiban4 += "='"
            Saiban4 += "30"
            Saiban4 += "' "
            Saiban4 += "RETURNING 会社コード"
            Saiban4 += ", "
            Saiban4 += "採番キー"
            Saiban4 += ", "
            Saiban4 += "最新値"
            Saiban4 += ", "
            Saiban4 += "最小値"
            Saiban4 += ", "
            Saiban4 += "最大値"
            Saiban4 += ", "
            Saiban4 += "接頭文字"
            Saiban4 += ", "
            Saiban4 += "連番桁数"
            Saiban4 += ", "
            Saiban4 += "更新者"
            Saiban4 += ", "
            Saiban4 += "更新日"
            _db.executeDB(Saiban4)
            _parentForm.Enabled = True
            _parentForm.Show()
            Me.Dispose()
        End If

    End Sub
End Class