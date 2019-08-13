Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Globalization

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

        Dim reccnt As Integer = 0

        'DateTimePickerのフォーマットを指定
        DtpOrderRegistration.Text = DateTime.Today '受発注登録日
        DtpOrderDate.Text = DateTime.Today '受注日
        DtpPurchaseDate.Text = DateTime.Today '発注日


        '-----受発注時に今ベースで入れることはないので、コメントアウト
        'DtpQuoteDate.Text = DateAdd("m", 0, Now).ToString("yyyy/MM/dd") '見積日
        'DtpExpiration.Text = DateAdd("d", 7, Now).ToString("yyyy/MM/dd") '見積有効期限
        'DtpQuoteRegistration.Text = DateAdd("d", 7, Now).ToString("yyyy/MM/dd") '見積登録日

        'セルの内容に合わせて、行の高さが自動的に調節されるようにする
        DgvItemList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        '"Column1"列のセルのテキストを折り返して表示する
        DgvItemList.Columns("型式").DefaultCellStyle.WrapMode = DataGridViewTriState.True

        'DataGridViewComboBoxColumnを作成
        Dim column As New DataGridViewComboBoxColumn()
        'DataGridViewComboBoxColumnのDataSourceを設定
        column.DataSource = getSireKbn()
        '実際の値が"Value"列、表示するテキストが"Display"列とする
        column.ValueMember = "Value"
        column.DisplayMember = "Display"
        column.HeaderText = "仕入区分"
        column.Name = "仕入区分"
        'column.ValueMember = 1
        'DataGridView1に追加する
        DgvItemList.Columns.Insert(1, column)

        'リードタイム単位コンボボックス作成
        Dim column2 As New DataGridViewComboBoxColumn()
        column2.DataSource = getReadTime()
        column2.ValueMember = "Value"
        column2.DisplayMember = "Display"
        column2.HeaderText = "リードタイム単位"
        column2.Name = "リードタイム単位"

        'DgvItemList.Columns.Insert(25, column2)
        DgvItemList.Columns.Insert(29, column2)

        '仕入通貨コンボボックス作成
        Dim column3 As New DataGridViewComboBoxColumn()
        column3.DataSource = getSireCurrency()
        column3.ValueMember = "Value"
        column3.DisplayMember = "Display"
        column3.HeaderText = "仕入通貨"
        column3.Name = "仕入通貨"

        DgvItemList.Columns.Insert(10, column3)

        createWarehouseCombobox() '倉庫コンボボックス

        Dim SqlSaiban As String = ""
        SqlSaiban += "SELECT "
        SqlSaiban += " 会社コード "
        SqlSaiban += ", 採番キー "
        SqlSaiban += ", 最新値 "
        SqlSaiban += ", 最小値 "
        SqlSaiban += ", 最大値 "
        SqlSaiban += ", 接頭文字 "
        SqlSaiban += ", 連番桁数 "
        SqlSaiban += "FROM public.m80_saiban"
        SqlSaiban += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        SqlSaiban += " AND 採番キー = '20'"

        Dim dtNow As DateTime = DateTime.Now
        Dim Saiban1 As DataSet = _db.selectDB(SqlSaiban, RS, reccnt)

        Dim SqlSaiban2 As String = ""
        SqlSaiban2 += "SELECT "
        SqlSaiban2 += " 会社コード "
        SqlSaiban2 += ", 採番キー "
        SqlSaiban2 += ", 最新値 "
        SqlSaiban2 += ", 最小値 "
        SqlSaiban2 += ", 最大値 "
        SqlSaiban2 += ", 接頭文字 "
        SqlSaiban2 += ", 連番桁数 "
        SqlSaiban2 += "FROM public.m80_saiban"
        SqlSaiban2 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        SqlSaiban2 += " AND 採番キー = '30'"

        Dim Saiban2 As DataSet = _db.selectDB(SqlSaiban2, RS, reccnt)

        CompanyCode = Saiban1.Tables(RS).Rows(0)(0)
        Dim OrderNo As String = Saiban1.Tables(RS).Rows(0)(5)
        OrderNo += dtNow.ToString("MMdd")
        OrderCount = Saiban1.Tables(RS).Rows(0)(2)
        OrderNo += OrderCount.PadLeft(Saiban1.Tables(RS).Rows(0)(6), "0")

        OrderCount += 1
        Dim Saiban3 As String = ""
        Saiban3 += "UPDATE Public.m80_saiban "
        Saiban3 += "SET "
        Saiban3 += " 最新値 = '" & OrderCount.ToString & "' "
        Saiban3 += ", 更新者 = 'Admin' "
        Saiban3 += ", 更新日 = '" & UtilClass.formatDatetime(dtNow) & "' "
        Saiban3 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Saiban3 += " AND 採番キー = '20'"

        _db.executeDB(Saiban3)

        '見積基本情報

        Dim Sql1 As String = ""
        Sql1 += "SELECT * FROM public.t01_mithd"
        Sql1 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql1 += " AND 見積番号 = '" & QuoteNo.ToString & "'"
        Sql1 += " AND 見積番号枝番 = '" & QuoteSuffix.ToString & "'"

        Dim ds1 = _db.selectDB(Sql1, RS, reccnt)

        CompanyCode = ds1.Tables(RS).Rows(0)("会社コード")
        TxtOrderNo.Text = OrderNo
        TxtOrderSuffix.Text = 1
        DtpOrderRegistration.Value = dtNow
        DtpOrderDate.Value = dtNow
        DtpPurchaseDate.Value = dtNow

        'たぶんこの記述だけでDBNull攻略できる（はず）
        '見積入力と同様
        'If ds1.Tables(RS).Rows(0)("見積番号") IsNot DBNull.Value Then
        '    TxtQuoteNo.Text = ds1.Tables(RS).Rows(0)("見積番号")
        'End If
        TxtQuoteNo.Text = ds1.Tables(RS).Rows(0)("見積番号").ToString
        TxtQuoteSuffix.Text = ds1.Tables(RS).Rows(0)("見積番号枝番").ToString
        If ds1.Tables(RS).Rows(0)("登録日") IsNot DBNull.Value Then
            DtpQuoteRegistration.Value = ds1.Tables(RS).Rows(0)("登録日")
        End If
        If ds1.Tables(RS).Rows(0)("見積日") IsNot DBNull.Value Then
            DtpQuoteDate.Value = ds1.Tables(RS).Rows(0)("見積日")
        End If
        If ds1.Tables(RS).Rows(0)("見積有効期限") IsNot DBNull.Value Then
            DtpExpiration.Value = ds1.Tables(RS).Rows(0)("見積有効期限")
        End If
        TxtCustomerCode.Text = ds1.Tables(RS).Rows(0)("得意先コード").ToString
        TxtCustomerName.Text = ds1.Tables(RS).Rows(0)("得意先名").ToString
        TxtPerson.Text = ds1.Tables(RS).Rows(0)("得意先担当者名").ToString
        TxtPosition.Text = ds1.Tables(RS).Rows(0)("得意先担当者役職").ToString
        TxtPostalCode.Text = ds1.Tables(RS).Rows(0)("得意先郵便番号").ToString
        TxtAddress1.Text = ds1.Tables(RS).Rows(0)("得意先住所").ToString
        TxtTel.Text = ds1.Tables(RS).Rows(0)("得意先電話番号").ToString
        TxtFax.Text = ds1.Tables(RS).Rows(0)("得意先ＦＡＸ").ToString
        TxtSales.Tag = ds1.Tables(RS).Rows(0)("営業担当者コード").ToString
        TxtSales.Text = ds1.Tables(RS).Rows(0)("営業担当者").ToString
        TxtInput.Tag = frmC01F10_Login.loginValue.TantoCD
        TxtInput.Text = frmC01F10_Login.loginValue.TantoNM
        TxtPaymentTerms.Text = ds1.Tables(RS).Rows(0)("支払条件").ToString
        TxtQuoteRemarks.Text = ds1.Tables(RS).Rows(0)("備考").ToString

        Dim decTmp As Decimal = ds1.Tables(RS).Rows(0)("ＶＡＴ").ToString
        TxtVat.Text = decTmp.ToString("N1")

        decTmp = ds1.Tables(RS).Rows(0)("見積金額").ToString
        TxtOrderAmount.Text = decTmp.ToString("N2")

        decTmp = ds1.Tables(RS).Rows(0)("仕入金額").ToString
        TxtPurchaseAmount.Text = decTmp.ToString("N2")

        decTmp = ds1.Tables(RS).Rows(0)("粗利額").ToString
        TxtGrossProfit.Text = decTmp.ToString("N2")

        TxtVatAmount.Text = ((TxtOrderAmount.Text * TxtVat.Text) / 100).ToString("N2")

        '通貨・レート情報設定
        createCurrencyCombobox(ds1.Tables(RS).Rows(0)("通貨").ToString)
        setRate()

        '見積明細情報
        Dim Sql3 As String = ""
        Sql3 += "SELECT * "
        Sql3 += "FROM public.t02_mitdt"
        Sql3 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql3 += " AND 見積番号 = '" & QuoteNo.ToString & "'"
        Sql3 += " AND 見積番号枝番 = '" & QuoteSuffix.ToString & "'"
        Sql3 += " ORDER BY 行番号"
        Dim ds3 = _db.selectDB(Sql3, RS, reccnt)

        Dim Sql4 As String = ""
        Sql4 += "SELECT * FROM public.m01_company"
        Sql4 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

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
            DgvItemList("仕入区分", index).Value = tmp
            DgvItemList.Rows(index).Cells("メーカー").Value = ds3.Tables(RS).Rows(index)("メーカー")
            DgvItemList.Rows(index).Cells("品名").Value = ds3.Tables(RS).Rows(index)("品名")
            DgvItemList.Rows(index).Cells("型式").Value = ds3.Tables(RS).Rows(index)("型式")
            DgvItemList.Rows(index).Cells("数量").Value = ds3.Tables(RS).Rows(index)("数量")
            DgvItemList.Rows(index).Cells("単位").Value = ds3.Tables(RS).Rows(index)("単位")
            DgvItemList.Rows(index).Cells("仕入先コード").Value = ds3.Tables(RS).Rows(index)("仕入先コード")
            DgvItemList.Rows(index).Cells("仕入先").Value = ds3.Tables(RS).Rows(index)("仕入先名称")
            DgvItemList.Rows(index).Cells("仕入単価").Value = ds3.Tables(RS).Rows(index)("仕入単価")
            If ds3.Tables(RS).Rows(index)("仕入通貨") Is DBNull.Value Then
            Else
                Dim tmp6 As Integer = ds3.Tables(RS).Rows(index)("仕入通貨")
                DgvItemList.Rows(index).Cells("仕入通貨").Value = tmp6
            End If
            DgvItemList.Rows(index).Cells("仕入レート").Value = ds3.Tables(RS).Rows(index)("仕入レート")
            DgvItemList.Rows(index).Cells("仕入単価_外貨").Value = ds3.Tables(RS).Rows(index)("仕入単価_外貨")
            DgvItemList.Rows(index).Cells("仕入原価").Value = ds3.Tables(RS).Rows(index)("仕入原価")
            DgvItemList.Rows(index).Cells("関税率").Value = ds3.Tables(RS).Rows(index)("関税率") * 100
            DgvItemList.Rows(index).Cells("関税額").Value = ds3.Tables(RS).Rows(index)("関税額")
            DgvItemList.Rows(index).Cells("前払法人税率").Value = ds3.Tables(RS).Rows(index)("前払法人税率") * 100
            DgvItemList.Rows(index).Cells("前払法人税額").Value = ds3.Tables(RS).Rows(index)("前払法人税額")
            DgvItemList.Rows(index).Cells("輸送費率").Value = ds3.Tables(RS).Rows(index)("輸送費率") * 100
            DgvItemList.Rows(index).Cells("輸送費額").Value = ds3.Tables(RS).Rows(index)("輸送費額")
            DgvItemList.Rows(index).Cells("仕入金額").Value = ds3.Tables(RS).Rows(index)("仕入金額")
            DgvItemList.Rows(index).Cells("売単価").Value = ds3.Tables(RS).Rows(index)("売単価")
            DgvItemList.Rows(index).Cells("売上金額").Value = ds3.Tables(RS).Rows(index)("売上金額")
            DgvItemList.Rows(index).Cells("見積単価").Value = ds3.Tables(RS).Rows(index)("見積単価")
            DgvItemList.Rows(index).Cells("見積単価_外貨").Value = ds3.Tables(RS).Rows(index)("見積単価_外貨")
            DgvItemList.Rows(index).Cells("見積金額").Value = ds3.Tables(RS).Rows(index)("見積金額")
            DgvItemList.Rows(index).Cells("見積金額_外貨").Value = ds3.Tables(RS).Rows(index)("見積金額_外貨")
            DgvItemList.Rows(index).Cells("粗利額").Value = ds3.Tables(RS).Rows(index)("粗利額")
            DgvItemList.Rows(index).Cells("粗利率").Value = ds3.Tables(RS).Rows(index)("粗利率")
            DgvItemList.Rows(index).Cells("リードタイム").Value = ds3.Tables(RS).Rows(index)("リードタイム")
            tmp5 = ds3.Tables(RS).Rows(index)("リードタイム単位")
            DgvItemList.Rows(index).Cells("リードタイム単位").Value = tmp5
            DgvItemList.Rows(index).Cells("備考").Value = ds3.Tables(RS).Rows(index)("備考")
            'DgvItemList.Rows(index).Cells(28).Value = "" 'ステータス？

            tmp2 = ds3.Tables(RS).Rows(index)("関税率")
            tmp2 = tmp2 / 100
            tmp3 = ds3.Tables(RS).Rows(index)("仕入原価")
            tmp3 = tmp3 * tmp2
            tmp4 = ds3.Tables(RS).Rows(index)("仕入原価")
            tmp4 = tmp4 + tmp3
            Total += tmp4 * PPH
        Next

        TxtPph.Text = Total.ToString("N2")

        '日付のMinDate設定
        '過去日付を許可する
        'DtpOrderDate.MinDate = DtpQuoteDate.Value
        'DtpPurchaseDate.MinDate = DtpQuoteDate.Value


        '行番号の振り直し
        Dim i As Integer = DgvItemList.Rows.Count()
        Dim No As Integer = 1
        For c As Integer = 0 To i - 1
            DgvItemList.Rows(c).Cells(0).Value = No
            No += 1
        Next c
        TxtItemCount.Text = DgvItemList.Rows.Count()

        '通貨表示：ベースの設定
        setBaseCurrency()

        '通貨表示：変更後の設定
        setChangeCurrency()

        setCurrency() '通貨に設定した内容に変更

        '翻訳
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
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
            TxtPaymentTerms.Size = New Size(420, 23)
            LblPaymentTerms.Size = New Size(162, 23)
            LblRemarks.Text = "QuotationRemarks"
            TxtQuoteRemarks.Location = New Point(181, 216)
            TxtQuoteRemarks.Size = New Size(420, 23)
            LblRemarks.Size = New Size(162, 23)
            LblItemCount.Text = "ItemCount"
            LblOrderRemarks.Text = "OrderRemarks"
            LblOrderRemarks.Size = New Size(161, 23)
            LblOrderRemarks.Location = New Point(11, 393)
            TxtOrderRemark.Location = New Point(179, 393)
            TxtOrderRemark.Size = New Size(280, 23)
            LblPurchaseRemarks.Text = "PurchaseOrderRemarks"
            LblPurchaseRemarks.Size = New Size(161, 23)
            LblPurchaseRemarks.Location = New Point(11, 422)
            TxtPurchaseRemark.Location = New Point(179, 422)
            TxtPurchaseRemark.Size = New Size(280, 23)
            LblPph.Text = "PPH"
            TxtPph.Size = New Size(151, 23)
            'LblVAT.Text = "VAT"
            TxtVat.Size = New Size(151, 23)
            LblOrderAmount.Text = "JobOrderAmount" '受注金額
            LblOrderAmount.Size = New Size(180, 23)
            LblOrderAmount.Location = New Point(923, 393)
            LblPurchaseAmount.Text = "PurchaseOrderAmount"
            LblPurchaseAmount.Size = New Size(180, 23)
            LblPurchaseAmount.Location = New Point(923, 422)
            LblGrossProfit.Text = "GrossMargin"
            LblGrossProfit.Size = New Size(180, 23)
            LblGrossProfit.Location = New Point(923, 451)
            LblCurrencyOrderAmount.Text = "JobOrderAmount"
            LblCurrencyOrderAmount.Size = New Size(180, 23)
            LblCurrencyOrderAmount.Location = New Point(923, 480)
            LblVatAmount.Text = "Currency"
            TxtVatAmount.Size = New Size(151, 23)
            LblCurrencyVatAmount.Text = "Currency"
            TxtCurrencyVatAmount.Size = New Size(151, 23)

            LblRate.Text = "Rate"
            LblWarehouse.Text = "Warehouse"
            LblCurrency.Text = "Currency"
            LblCurrency.Text = "Currency"
            LblIDRCurrency.Text = "Currency"
            LblChangeCurrency.Text = "Currency"

            BtnRegistration.Text = "Registrartion"
            BtnBack.Text = "Back"

            DgvItemList.Columns("仕入区分").HeaderText = "PurchasingClassification"
            DgvItemList.Columns("メーカー").HeaderText = "Manufacturer"
            DgvItemList.Columns("品名").HeaderText = "ItemName"
            DgvItemList.Columns("型式").HeaderText = "Spec"
            DgvItemList.Columns("数量").HeaderText = "Quantity"
            DgvItemList.Columns("単位").HeaderText = "Unit"
            DgvItemList.Columns("仕入先コード").HeaderText = "SupplierCode"
            DgvItemList.Columns("仕入先").HeaderText = "SupplierName"
            DgvItemList.Columns("仕入通貨").HeaderText = "PurchaseCurrency"
            DgvItemList.Columns("仕入単価_外貨").HeaderText = "PurchaseUnitPrice" & vbCrLf & "(OriginalCurrency)"
            DgvItemList.Columns("仕入単価").HeaderText = "PurchaseUnitPrice" & vbCrLf & "(" & TxtIDRCurrency.Text & ")"

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

            DgvItemList.Columns("見積単価_外貨").HeaderText = "QuotetionPriceForeignCurrency" & vbCrLf & "(OriginalCurrency)"
            DgvItemList.Columns("見積単価").HeaderText = "QuotetionPrice" & vbCrLf & "(" & TxtIDRCurrency.Text & ")"

            DgvItemList.Columns("見積金額_外貨").HeaderText = "QuotetionAmountForeignCurrency" & vbCrLf & "(OriginalCurrency)"
            DgvItemList.Columns("見積金額").HeaderText = "QuotetionAmount" & vbCrLf & "(" & TxtIDRCurrency.Text & ")"
            DgvItemList.Columns("粗利額").HeaderText = "GrossProfit"
            DgvItemList.Columns("粗利率").HeaderText = "GrossMarginRate(%)"
            DgvItemList.Columns("リードタイム").HeaderText = "LeadTime"
            DgvItemList.Columns("リードタイム単位").HeaderText = "LeadTimeUnit"
            DgvItemList.Columns("備考").HeaderText = "Remarks"

        Else
            DgvItemList.Columns("仕入単価_外貨").HeaderText = "仕入単価" & vbCrLf & "（原通貨）"
            DgvItemList.Columns("仕入単価").HeaderText = "仕入単価" & vbCrLf & "（" & TxtIDRCurrency.Text & "）"
            DgvItemList.Columns("見積単価_外貨").HeaderText = "見積単価" & vbCrLf & "（原通貨）"
            DgvItemList.Columns("見積単価").HeaderText = "見積単価" & vbCrLf & "（" & TxtIDRCurrency.Text & "）"
            DgvItemList.Columns("見積金額_外貨").HeaderText = "見積金額" & vbCrLf & "（原通貨）"
            DgvItemList.Columns("見積金額").HeaderText = "見積金額" & vbCrLf & "（" & TxtIDRCurrency.Text & "）"

        End If

        '中央寄せ
        DgvItemList.Columns("仕入区分").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("メーカー").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("品名").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("型式").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("数量").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("単位").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("仕入先コード").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("仕入先").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("仕入単価").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("仕入通貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("仕入単価_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("仕入原価").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("関税率").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("関税額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("前払法人税率").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("前払法人税額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("輸送費率").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("輸送費額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("仕入金額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("売単価").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("売上金額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("見積単価").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("見積単価_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("見積金額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("見積金額_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("粗利額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("粗利率").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("リードタイム").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("リードタイム単位").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("備考").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter


    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    '登録ボタン押下時
    Private Sub BtnRegistration_Click(sender As Object, e As EventArgs) Handles BtnRegistration.Click
        Dim errList(DgvItemList.Rows.Count - 1) As Integer
        Dim Sql As String = ""
        Dim reccnt As Integer = 0
        Dim itemCount As Integer = 0
        Dim errFlg As Boolean = True

        Dim dtNow As DateTime = DateTime.Now
        Dim strNow As String = UtilClass.formatDatetime(dtNow)

        '受発注登録時に受注金額が0の場合アラートで警告
        If TxtOrderAmount.Text = 0 Then
            '対象のデータではないことをアラートする
            Dim result = _msgHd.dspMSG("confirmOrderAmountZERO", frmC01F10_Login.loginValue.Language)
            If result = DialogResult.No Then
                Exit Sub
            End If
        End If

        If CmWarehouse.SelectedValue = "" Then
            '倉庫データがないことをアラートする
            Dim result = _msgHd.dspMSG("chkWarehouseError", frmC01F10_Login.loginValue.Language)
            Exit Sub
        End If

        '得意先が stockじゃなかったら
        If TxtCustomerCode.Text <> "stock" Then

#Region "得意先コード <> 'stock' AND 仕入区分 = 2"

            '登録する出庫データがあるかフラグでチェック
            Dim chkShukko As Boolean = False
            For i As Integer = 0 To DgvItemList.Rows.Count - 1
                '仕入区分が 2 だったら
                If DgvItemList.Rows(i).Cells("仕入区分").Value = CommonConst.Sire_KBN_Zaiko Then

                    chkShukko = True

                    If setZaikoQuantity(i) < Long.Parse(DgvItemList.Rows(i).Cells("数量").Value.ToString) Then
                        errFlg = False
                        errList(i) = Math.Abs(setZaikoQuantity(i) - Long.Parse(DgvItemList.Rows(i).Cells("数量").Value.ToString))
                    End If
                End If
            Next
#End Region

            '在庫数が0以上であるとき
            If errFlg Then

#Region "t10_cymnhd 受注基本登録"

                Dim Sql1 As String = ""

                Sql1 = "INSERT INTO "
                Sql1 += "Public."
                Sql1 += "t10_cymnhd("
                Sql1 += "会社コード, 受注番号, 受注番号枝番, 客先番号, 見積番号, 見積番号枝番, 得意先コード"
                Sql1 += ", 得意先名, 得意先郵便番号, 得意先住所, 得意先電話番号, 得意先ＦＡＸ, 得意先担当者役職"
                Sql1 += ", 得意先担当者名, 見積日, 見積有効期限, 支払条件, 見積金額,仕入金額, 粗利額"
                Sql1 += ", 営業担当者コード, 営業担当者, 入力担当者コード, 入力担当者, 備考, 見積備考, ＶＡＴ"
                Sql1 += ", 受注日, 登録日, 更新日, 更新者, 取消区分, 見積金額_外貨, 通貨, レート)"
                Sql1 += " VALUES('" & frmC01F10_Login.loginValue.BumonCD & "'"     '会社コード
                Sql1 += ", '" & TxtOrderNo.Text & "'"       '受注番号
                Sql1 += ", '" & TxtOrderSuffix.Text & "'"   '受注番号枝番
                Sql1 += ", '" & RevoveChars(TxtCustomerPO.Text) & "'"    '客先番号
                Sql1 += ", '" & TxtQuoteNo.Text & "'"       '見積番号
                Sql1 += ", '" & TxtQuoteSuffix.Text & "'"   '見積番号枝番
                Sql1 += ", '" & TxtCustomerCode.Text & "'"  '得意先コード
                Sql1 += ", '" & TxtCustomerName.Text & "'"  '得意先名
                Sql1 += ", '" & TxtPostalCode.Text & "'"    '得意先郵便番号
                Sql1 += ", '" & TxtAddress1.Text & "'"      '得意先住所
                Sql1 += ", '" & TxtTel.Text & "'"           '得意先電話番号
                Sql1 += ", '" & TxtFax.Text & "'"           '得意先ＦＡＸ
                Sql1 += ", '" & TxtPosition.Text & "'"      '得意先担当者役職
                Sql1 += ", '" & TxtPerson.Text & "'"        '得意先担当者名
                Sql1 += ", '" & UtilClass.strFormatDate(DtpQuoteDate.Value) & "'"    '見積日
                Sql1 += ", '" & UtilClass.strFormatDate(DtpExpiration.Value) & "'"   '見積有効期限
                Sql1 += ", '" & TxtPaymentTerms.Text & "'"  '支払条件
                Sql1 += ", " & formatStringToNumber(TxtOrderAmount.Text)          '見積金額
                Sql1 += ", " & formatStringToNumber(TxtPurchaseAmount.Text)       '仕入金額
                Sql1 += "," & formatStringToNumber(TxtGrossProfit.Text)           '粗利額
                Sql1 += ", '" & TxtSales.Tag & "'"         '営業担当者コード
                Sql1 += ", '" & TxtSales.Text & "'"         '営業担当者
                Sql1 += ", '" & TxtInput.Tag & "'"         '入力担当者コード
                Sql1 += ", '" & TxtInput.Text & "'"         '入力担当者
                Sql1 += ", '" & TxtOrderRemark.Text & "'"   '備考
                Sql1 += ", '" & RevoveChars(TxtQuoteRemarks.Text) & "'" '見積備考
                Sql1 += ", " & formatStringToNumber(TxtVat.Text)      'ＶＡＴ
                Sql1 += ", '" & UtilClass.strFormatDate(DtpOrderDate.Value) & "'"    '受注日
                Sql1 += ", '" & UtilClass.formatDatetime(DtpOrderRegistration.Value) & "'"     '登録日
                Sql1 += ", '" & strNow & "'"                 '更新日
                Sql1 += ", '" & frmC01F10_Login.loginValue.TantoNM & "'"
                Sql1 += ", 0"                               '取消区分
                Sql1 += ", " & formatStringToNumber(TxtCurrencyOrderTotal.Text)          '見積金額_外貨
                Sql1 += ", " & CmCurrency.SelectedValue.ToString                            '通貨
                Sql1 += ", " & UtilClass.formatNumberF10(TxtRate.Text)  'レート
                Sql1 += " )"

                _db.executeDB(Sql1)

#End Region

#Region "t11_cymndt 受注明細登録"

                Dim Sql2 As String = ""
                For i As Integer = 0 To DgvItemList.Rows.Count - 1

                    Sql2 = "INSERT INTO "
                    Sql2 += "Public."
                    Sql2 += "t11_cymndt("
                    Sql2 += "会社コード, 受注番号, 受注番号枝番, 行番号, 仕入区分, メーカー, 品名, 型式, 単位, 仕入先名, 仕入値, 受注数量"
                    Sql2 += ", 売上数量, 受注残数, 売単価, 売上金額, 見積単価, 見積金額, 粗利額, 粗利率, 間接費, リードタイム, リードタイム単位"
                    Sql2 += ", 出庫数, 未出庫数, 備考, 更新者, 登録日, 関税率, 関税額, 前払法人税率, 前払法人税額, 輸送費率, 輸送費額, 仕入原価"
                    Sql2 += ", 仕入金額, 見積単価_外貨, 見積金額_外貨, 通貨, レート, 仕入単価_外貨, 仕入通貨, 仕入レート)"
                    Sql2 += " VALUES("
                    Sql2 += " '" & frmC01F10_Login.loginValue.BumonCD & "'"  '会社コード
                    Sql2 += ", '" & TxtOrderNo.Text & "'"       '受注番号
                    Sql2 += ", '" & TxtOrderSuffix.Text & "'"   '受注番号枝番
                    Sql2 += ", " & DgvItemList.Rows(i).Cells("No").Value.ToString      '行番号
                    Sql2 += ", '" & DgvItemList.Rows(i).Cells("仕入区分").Value.ToString & "'"  '仕入区分
                    Sql2 += ", '" & DgvItemList.Rows(i).Cells("メーカー").Value.ToString & "'"  'メーカー
                    Sql2 += ", '" & DgvItemList.Rows(i).Cells("品名").Value.ToString & "'"      '品名
                    Sql2 += ", '" & DgvItemList.Rows(i).Cells("型式").Value.ToString & "'"    '型式
                    Sql2 += ", '" & DgvItemList.Rows(i).Cells("単位").Value.ToString & "'"    '単位
                    Sql2 += ", '" & DgvItemList.Rows(i).Cells("仕入先").Value.ToString & "'"   '仕入先名
                    Sql2 += ", " & formatStringToNumber(DgvItemList.Rows(i).Cells("仕入単価").Value.ToString)       '仕入値
                    Sql2 += ", " & DgvItemList.Rows(i).Cells("数量").Value.ToString           '受注数量
                    Sql2 += ", 0"   '売上数量
                    Sql2 += ", " & DgvItemList.Rows(i).Cells("数量").Value.ToString       '受注残数
                    Sql2 += ", " & formatStringToNumber(DgvItemList.Rows(i).Cells("売単価").Value.ToString)      '売単価
                    Sql2 += ", " & formatStringToNumber(DgvItemList.Rows(i).Cells("売上金額").Value.ToString)     '売上金額
                    Sql2 += ", " & formatStringToNumber(DgvItemList.Rows(i).Cells("見積単価").Value.ToString)     '見積単価
                    Sql2 += ", " & formatStringToNumber(DgvItemList.Rows(i).Cells("見積金額").Value.ToString)     '見積金額
                    Sql2 += ", " & formatStringToNumber(DgvItemList.Rows(i).Cells("粗利額").Value.ToString)      '粗利額
                    Sql2 += ", " & formatStringToNumber(DgvItemList.Rows(i).Cells("粗利率").Value.ToString)      '粗利率
                    Sql2 += ", " & formatStringToNumber(DgvItemList.Rows(i).Cells("仕入金額").Value - DgvItemList.Rows(i).Cells("仕入原価").Value)     '間接費
                    Sql2 += ", '" & DgvItemList.Rows(i).Cells("リードタイム").Value.ToString & "'"    'リードタイム
                    Sql2 += ", " & DgvItemList.Rows(i).Cells("リードタイム単位").Value.ToString     'リードタイム単位
                    Sql2 += ", 0"       '出庫数
                    Sql2 += ", " & DgvItemList.Rows(i).Cells("数量").Value.ToString       '未出庫数
                    Sql2 += ", '" & RevoveChars(DgvItemList.Rows(i).Cells("備考").Value.ToString) & "'"   '備考
                    Sql2 += ", '" & frmC01F10_Login.loginValue.TantoNM & "'"       '更新者
                    Sql2 += ", '" & UtilClass.formatDatetime(dtNow) & "'"      '登録日
                    Sql2 += ", " & formatStringToNumber(DgvItemList.Rows(i).Cells("関税率").Value.ToString / 100)             '関税率
                    Sql2 += ", " & formatStringToNumber(DgvItemList.Rows(i).Cells("関税額").Value.ToString)             '関税額
                    Sql2 += ", " & formatStringToNumber(DgvItemList.Rows(i).Cells("前払法人税率").Value.ToString / 100)       '前払法人税率
                    Sql2 += ", " & formatStringToNumber(DgvItemList.Rows(i).Cells("前払法人税額").Value.ToString)       '前払法人税額
                    Sql2 += ", " & formatStringToNumber(DgvItemList.Rows(i).Cells("輸送費率").Value.ToString / 100)         '輸送費率
                    Sql2 += ", " & formatStringToNumber(DgvItemList.Rows(i).Cells("輸送費額").Value.ToString)         '輸送費額
                    Sql2 += ", " & formatStringToNumber(DgvItemList.Rows(i).Cells("仕入原価").Value.ToString)         '仕入原価
                    Sql2 += ", " & formatStringToNumber(DgvItemList.Rows(i).Cells("仕入金額").Value.ToString)         '仕入金額
                    Sql2 += ", " & formatStringToNumber(DgvItemList.Rows(i).Cells("見積単価_外貨").Value.ToString)     '見積単価_外貨
                    Sql2 += ", " & formatStringToNumber(DgvItemList.Rows(i).Cells("見積金額_外貨").Value.ToString)     '見積金額_外貨
                    Sql2 += ", " & CmCurrency.SelectedValue.ToString                                   '通貨
                    Sql2 += ", " & UtilClass.formatNumberF10(TxtRate.Text)                                              'レート
                    Sql2 += ", " & formatStringToNumber(DgvItemList.Rows(i).Cells("仕入単価_外貨").Value.ToString)    '仕入単価_外貨
                    Sql2 += ", " & formatStringToNumber(DgvItemList.Rows(i).Cells("仕入通貨").Value.ToString)           '仕入通貨
                    Sql2 += ", " & formatStringToNumber(DgvItemList.Rows(i).Cells("仕入レート").Value.ToString)          '仕入レート
                    Sql2 += ")"
                    _db.executeDB(Sql2)

                    Sql2 = ""
                Next

#End Region

#Region "t44_shukohd 出庫登録"

                '1件も「仕入先 <>'stock' && 仕入区分 = 2 」の条件を満たすデータがなければスルー
                If chkShukko Then

                    Sql = "SELECT * FROM public.m80_saiban"
                    Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                    Sql += " AND 採番キー = '70'"
                    Dim dsSaiban As DataSet = _db.selectDB(Sql, RS, reccnt)

                    '採番データを取得・更新
                    '出庫登録データの伝票番号は基本的に LS で統一される（1商品で複数の在庫マスタをまたぐ場合を除く）
                    Dim MAIN_LS As String = getSaiban("70", dtNow.ToShortDateString())

                    For i As Integer = 0 To DgvItemList.Rows.Count() - 1

                        '受発注の段階で出庫登録を行うのは「仕入先 <>'stock' && 仕入区分 = 2 」のみ
                        If DgvItemList.Rows(i).Cells("仕入区分").Value = CommonConst.Sire_KBN_Zaiko Then

                            '該当する在庫データを取得・ループ
                            '対象の在庫がなくなるまでデータを作成する
                            Dim dsCurrentList As DataSet = getNukoList(i)

                            Dim totalShukkoVal As Long = Long.Parse(DgvItemList.Rows(i).Cells("数量").Value)
                            Dim currentVal As Long = 0
                            Dim currentLS As String = ""

                            '明細ループ
                            '------------------------
                            For x As Integer = 0 To dsCurrentList.Tables(RS).Rows.Count - 1

                                If totalShukkoVal = 0 Then
                                    Exit For
                                End If

                                currentVal = Long.Parse(dsCurrentList.Tables(RS).Rows(x)("現在庫数"))
                                '現在庫数より出庫数量の方が大きかった場合、現在庫数をそのまま出庫データとして作成
                                If currentVal < totalShukkoVal Then
                                    totalShukkoVal -= currentVal 'currentValをそのまま登録し、全体数からcurrentValを減算する
                                Else
                                    currentVal = totalShukkoVal '登録するのは残数分のみ
                                    totalShukkoVal -= currentVal
                                End If

                                '作成データが複数以上の場合、出庫番号を新規取得
                                currentLS = IIf(x = 0, MAIN_LS, getSaiban("70", dtNow.ToShortDateString()))

                                Sql = "INSERT INTO "
                                Sql += "Public."
                                Sql += "t45_shukodt("
                                Sql += "会社コード, 出庫番号, 受注番号, 受注番号枝番, 行番号, 仕入区分, メーカー, 品名, 型式"
                                Sql += ", 仕入先名, 売単価, 出庫数量, 単位, 備考, 更新者, 更新日, 出庫区分, 倉庫コード)"
                                Sql += " VALUES('" & frmC01F10_Login.loginValue.BumonCD & "'"   '会社コード
                                Sql += ", '" & currentLS & "'"     '出庫番号
                                Sql += ", '" & TxtOrderNo.Text & "'"    '受注番号
                                Sql += ", '" & TxtOrderSuffix.Text & "'"        '受注番号枝番
                                Sql += ", " & DgvItemList.Rows(i).Cells("No").Value.ToString    '行番号
                                Sql += ", '" & DgvItemList.Rows(i).Cells("仕入区分").Value.ToString & "'"   '仕入区分
                                Sql += ", '" & DgvItemList.Rows(i).Cells("メーカー").Value.ToString & "'"   'メーカー
                                Sql += ", '" & DgvItemList.Rows(i).Cells("品名").Value.ToString & "'"       '品名
                                Sql += ", '" & DgvItemList.Rows(i).Cells("型式").Value.ToString & "'"         '型式
                                Sql += ", '" & DgvItemList.Rows(i).Cells("仕入先").Value.ToString & "'"        '仕入先名
                                Sql += ", " & formatStringToNumber(DgvItemList.Rows(i).Cells("見積単価").Value.ToString)    '見積単価
                                'Sql += ", " & DgvItemList.Rows(i).Cells("数量").Value.ToString               '出庫数量
                                Sql += ", " & currentVal.ToString '数量
                                Sql += ", '" & DgvItemList.Rows(i).Cells("単位").Value.ToString & "'"         '単位
                                Sql += ", '" & DgvItemList.Rows(i).Cells("備考").Value.ToString & "'"         '備考
                                Sql += ", '" & frmC01F10_Login.loginValue.TantoNM & "'"                           '更新者
                                Sql += ", '" & UtilClass.formatDatetime(dtNow) & "'"                 '更新日
                                Sql += ", '" & CommonConst.SHUKO_KBN_TMP & "'" '仮出庫：2
                                Sql += ", '" & CmWarehouse.SelectedValue & "')" '倉庫コード

                                _db.executeDB(Sql)

                                't70_inout にデータ登録
                                Sql = "INSERT INTO "
                                Sql += "Public."
                                Sql += "t70_inout("
                                Sql += "会社コード, 入出庫区分, 倉庫コード, 伝票番号, 行番号, 入出庫種別"
                                Sql += ", 引当区分, メーカー, 品名, 型式, 数量, 単位, 備考, 入出庫日"
                                Sql += ", 取消区分, 更新者, 更新日, ロケ番号)"
                                Sql += " VALUES('"
                                Sql += frmC01F10_Login.loginValue.BumonCD '会社コード
                                Sql += "', '"
                                Sql += "2" '入出庫区分 1.入庫, 2.出庫
                                Sql += "', '"
                                Sql += CmWarehouse.SelectedValue '倉庫コード
                                Sql += "', '"
                                'Sql += dsCurrentList.Tables(RS).Rows(x)("伝票番号").ToString '伝票番号（入庫番号）
                                'Sql += "', '"
                                'Sql += dsCurrentList.Tables(RS).Rows(x)("行番号").ToString '行番号（入庫番号）

                                Sql += currentLS '伝票番号（出庫番号）
                                Sql += "', '"
                                Sql += DgvItemList.Rows(i).Cells("No").Value.ToString    '行番号
                                'Sql += dsCurrentList.Tables(RS).Rows(x)("行番号").ToString '行番号（入庫番号）

                                Sql += "', '"
                                Sql += CommonConst.INOUT_KBN_NORMAL '入出庫種別
                                Sql += "', '"
                                Sql += CommonConst.AC_KBN_ASSIGN.ToString '引当区分
                                Sql += "', '"
                                Sql += DgvItemList.Rows(i).Cells("メーカー").Value.ToString 'メーカー
                                Sql += "', '"
                                Sql += DgvItemList.Rows(i).Cells("品名").Value.ToString '品名
                                Sql += "', '"
                                Sql += DgvItemList.Rows(i).Cells("型式").Value.ToString '型式
                                Sql += "', '"
                                'Sql += DgvItemList.Rows(i).Cells("数量").Value.ToString '数量
                                Sql += currentVal.ToString '数量
                                Sql += "', '"
                                Sql += DgvItemList.Rows(i).Cells("単位").Value.ToString '単位
                                Sql += "', '"
                                Sql += DgvItemList.Rows(i).Cells("備考").Value.ToString '備考
                                Sql += "', '"
                                Sql += UtilClass.formatDatetime(dtNow) '入出庫日
                                Sql += "', '"
                                Sql += CommonConst.CANCEL_KBN_ENABLED.ToString '取消区分
                                Sql += "', '"
                                Sql += frmC01F10_Login.loginValue.TantoNM      '更新者
                                Sql += "', '"
                                Sql += UtilClass.formatDatetime(dtNow) '更新日
                                Sql += "', '"
                                'Sql += currentLS & DgvItemList.Rows(i).Cells("No").Value.ToString

                                Sql += dsCurrentList.Tables(RS).Rows(x)("伝票番号").ToString & dsCurrentList.Tables(RS).Rows(x)("行番号").ToString '入庫番号+行番号

                                Sql += "')"

                                _db.executeDB(Sql)

                                If MAIN_LS <> currentLS Then
                                    Sql = "INSERT INTO Public.t44_shukohd ("
                                    Sql += "会社コード, 出庫番号, 見積番号, 見積番号枝番, 受注番号, 受注番号枝番, 客先番号, 得意先コード, 得意先名"
                                    Sql += ", 得意先郵便番号, 得意先住所, 得意先電話番号, 得意先ＦＡＸ, 得意先担当者役職, 得意先担当者名, 営業担当者"
                                    Sql += ", 入力担当者, 備考, 取消日, 取消区分, 出庫日, 登録日, 更新日, 更新者, 営業担当者コード, 入力担当者コード)"
                                    Sql += " VALUES('" & frmC01F10_Login.loginValue.BumonCD & "'"       '会社コード
                                    Sql += ", '" & currentLS & "'"                 '出庫番号
                                    Sql += ", '" & TxtQuoteNo.Text & "'"    '見積番号
                                    Sql += ", '" & TxtQuoteSuffix.Text & "'"    '見積番号枝番
                                    Sql += ", '" & TxtOrderNo.Text & "'"        '受注番号
                                    Sql += ", '" & TxtOrderSuffix.Text & "'"    '受注番号枝番
                                    Sql += ", '" & RevoveChars(TxtCustomerPO.Text) & "'"     '客先番号
                                    Sql += ", '" & TxtCustomerCode.Text & "'"   '得意先コード
                                    Sql += ", '" & TxtCustomerName.Text & "'"   '得意先名
                                    Sql += ", '" & TxtPostalCode.Text & "'"     '得意先郵便番号
                                    Sql += ", '" & TxtAddress1.Text & "'"       '得意先住所
                                    Sql += ", '" & TxtTel.Text & "'"            '得意先電話番号
                                    Sql += ", '" & TxtFax.Text & "'"            '得意先ＦＡＸ
                                    Sql += ", '" & TxtPosition.Text & "'"       '得意先担当者役職
                                    Sql += ", '" & TxtPerson.Text & "'"         '得意先担当者名
                                    Sql += ", '" & TxtSales.Text & "'"          '営業担当者
                                    Sql += ", '" & TxtInput.Text & "'"          '入力担当者
                                    Sql += ", '" & TxtOrderRemark.Text & "'"    '備考
                                    Sql += ", null"     '取消日
                                    Sql += ", 0"     '取消区分
                                    Sql += ", current_date"     '出庫日
                                    Sql += ", '" & UtilClass.formatDatetime(dtNow) & "'"                 '登録日
                                    Sql += ", '" & UtilClass.formatDatetime(dtNow) & "'"                 '更新日
                                    Sql += ", '" & frmC01F10_Login.loginValue.TantoNM & "'"     '更新者
                                    Sql += ", '" & TxtSales.Tag & "'"    '営業担当者コード
                                    Sql += ", '" & frmC01F10_Login.loginValue.TantoCD & "'"    '入力担当者コード
                                    Sql += " )"

                                    _db.executeDB(Sql)
                                End If
                            Next

                        End If

                    Next

                    Sql = "INSERT INTO Public.t44_shukohd ("
                    Sql += "会社コード, 出庫番号, 見積番号, 見積番号枝番, 受注番号, 受注番号枝番, 客先番号, 得意先コード, 得意先名"
                    Sql += ", 得意先郵便番号, 得意先住所, 得意先電話番号, 得意先ＦＡＸ, 得意先担当者役職, 得意先担当者名, 営業担当者"
                    Sql += ", 入力担当者, 備考, 取消日, 取消区分, 出庫日, 登録日, 更新日, 更新者, 営業担当者コード, 入力担当者コード)"
                    Sql += " VALUES('" & frmC01F10_Login.loginValue.BumonCD & "'"       '会社コード
                    Sql += ", '" & MAIN_LS & "'"                 '出庫番号
                    Sql += ", '" & TxtQuoteNo.Text & "'"    '見積番号
                    Sql += ", '" & TxtQuoteSuffix.Text & "'"    '見積番号枝番
                    Sql += ", '" & TxtOrderNo.Text & "'"        '受注番号
                    Sql += ", '" & TxtOrderSuffix.Text & "'"    '受注番号枝番
                    Sql += ", '" & RevoveChars(TxtCustomerPO.Text) & "'"     '客先番号
                    Sql += ", '" & TxtCustomerCode.Text & "'"   '得意先コード
                    Sql += ", '" & TxtCustomerName.Text & "'"   '得意先名
                    Sql += ", '" & TxtPostalCode.Text & "'"     '得意先郵便番号
                    Sql += ", '" & TxtAddress1.Text & "'"       '得意先住所
                    Sql += ", '" & TxtTel.Text & "'"            '得意先電話番号
                    Sql += ", '" & TxtFax.Text & "'"            '得意先ＦＡＸ
                    Sql += ", '" & TxtPosition.Text & "'"       '得意先担当者役職
                    Sql += ", '" & TxtPerson.Text & "'"         '得意先担当者名
                    Sql += ", '" & TxtSales.Text & "'"          '営業担当者
                    Sql += ", '" & TxtInput.Text & "'"          '入力担当者
                    Sql += ", '" & TxtOrderRemark.Text & "'"    '備考
                    Sql += ", null"     '取消日
                    Sql += ", 0"     '取消区分
                    Sql += ", current_date"     '出庫日
                    Sql += ", '" & UtilClass.formatDatetime(dtNow) & "'"                 '登録日
                    Sql += ", '" & UtilClass.formatDatetime(dtNow) & "'"                 '更新日
                    Sql += ", '" & frmC01F10_Login.loginValue.TantoNM & "'"     '更新者
                    Sql += ", '" & TxtSales.Tag & "'"    '営業担当者コード
                    Sql += ", '" & frmC01F10_Login.loginValue.TantoCD & "'"    '入力担当者コード
                    Sql += " )"

                    _db.executeDB(Sql)

                End If

#End Region
            Else

                '在庫で不足分があればエラーを返す

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

            'Dim currencyList As New List(Of Integer)(New Integer() {})
            'For i As Integer = 0 To DgvItemList.Rows.Count - 1
            '    If currencyList.Contains(DgvItemList.Rows(i).Cells("仕入通貨").Value) = False And DgvItemList.Rows(i).Cells("仕入区分").Value = 1 Then
            '        currencyList.Add(DgvItemList.Rows(i).Cells("仕入通貨").Value)
            '    Else

            '    End If
            'Next

            Dim tbl As DataTable = New DataTable("table1")
            Dim row As DataRow
            Dim SupFlg As Boolean = True

            'カラム名
            For i As Integer = 0 To DgvItemList.ColumnCount - 1
                tbl.Columns.Add(DgvItemList.Columns(i).Name)
            Next

            For i As Integer = 0 To DgvItemList.Rows.Count - 1
                row = tbl.NewRow
                For x As Integer = 0 To DgvItemList.ColumnCount - 1
                    'カラム名
                    If DgvItemList.Columns(x).Name = "仕入区分" Or DgvItemList.Columns(x).Name = "リードタイム単位" Or DgvItemList.Columns(x).Name = "仕入通貨" Then
                        row(DgvItemList.Columns(x).Name) = DgvItemList.Rows(i).Cells(DgvItemList.Columns(x).Name).Value

                        '仕入区分を判定
                        If DgvItemList.Columns(x).Name = "仕入区分" Then
                            If DgvItemList.Rows(i).Cells(DgvItemList.Columns(x).Name).Value = 1 Then
                                SupFlg = True
                            Else
                                SupFlg = False  '在庫引当の場合は読み飛ばす
                            End If
                        End If

                    Else
                        row(DgvItemList.Columns(x).Name) = DgvItemList.Rows(i).Cells(DgvItemList.Columns(x).Name).Value
                    End If
                    row(DgvItemList.Columns(x).Name) = DgvItemList.Rows(i).Cells(DgvItemList.Columns(x).Name).Value
                Next

                If SupFlg = True Then
                    tbl.Rows.Add(row)
                End If
            Next

            ' 並び替える
            Dim dv = New DataView(tbl)
            dv.Sort = "仕入先コード , 仕入通貨, No" ' 昇順
            tbl = dv.ToTable ' 並び替え後のデータをDataTableに戻す

            'ループ条件チェック用
            '仕入先、通貨コード
            Dim sireCd As String = ""
            Dim currencyCd As String = ""
            Dim costMain As Long = 0 '仕入先 & 仕入通貨 毎に仕入原価を合算する（main po用）
            Dim tmpCuoteMain As Long = 0 '仕入先 & 仕入通貨 毎に見積金額を合算（main po用）
            Dim cost As Long = 0 '仕入先 & 仕入通貨 毎に仕入原価を合算する
            Dim tmpCuote As Long = 0 '仕入先 & 仕入通貨 毎に見積金額を合算
            Dim PurchaseNo As String
            Dim CurrentPurchaseNo As String = ""

            Dim dsSipper As DataSet '仕入先
            Dim strRate As Decimal 'レートの取得

            Dim lngCnt As Long = 0

            PurchaseNo = getSaiban(30, dtNow) '必ず発行される発注番号

            'DataTableをループしながらデータ登録
            For i As Integer = 0 To tbl.Rows.Count - 1

                '仕入区分 = 1（受発注）のみ発注登録を行う
                If tbl.Rows(i)("仕入区分") = CommonConst.Sire_KBN_Sire Then

                    '最初はそのまま
                    If CurrentPurchaseNo = "" Then
                        CurrentPurchaseNo = PurchaseNo
                        '新しい値をセットし、発注基本を登録する
                        sireCd = tbl.Rows(i)("仕入先コード")
                        currencyCd = tbl.Rows(i)("仕入通貨")

                    Else
                        '前回の明細データの仕入コード、仕入通貨と一致するかチェック
                        If (sireCd <> tbl.Rows(i)("仕入先コード") Or currencyCd <> tbl.Rows(i)("仕入通貨")) Then
                            CurrentPurchaseNo = getSaiban("30", dtNow)

                            hattyuHdInsert(PurchaseNo, dsSipper, cost, tmpCuote, dtNow, strRate)
                            'reset
                            PurchaseNo = CurrentPurchaseNo
                            sireCd = tbl.Rows(i)("仕入先コード")
                            currencyCd = tbl.Rows(i)("仕入通貨")
                            cost = 0
                            tmpCuote = 0

                        End If
                    End If

                    Sql = " AND 仕入先コード ILIKE '" & tbl.Rows(i)("仕入先コード") & "'"
                    dsSipper = getDsData("m11_supplier", Sql)

                    'レートの取得
                    strRate = setRate(tbl.Rows(i)("仕入通貨"))

                    '明細データ更新
                    Sql = ""
                    Sql += "INSERT INTO "
                    Sql += "Public."
                    Sql += "t21_hattyu("
                    Sql += "会社コード, 発注番号, 発注番号枝番, 行番号, 仕入区分, メーカー, 品名, 型式"
                    Sql += ", 単位, 仕入先名, 仕入値, 発注数量, 仕入数量, 発注残数, 仕入金額"
                    Sql += ", 間接費, リードタイム, リードタイム単位, 入庫数, 未入庫数, 備考, 更新者"
                    Sql += ", 登録日, 更新日, 見積単価_外貨, 見積金額_外貨, 通貨, レート, 仕入単価_外貨"
                    Sql += ", 仕入通貨, 仕入レート, 関税率, 関税額, 前払法人税率, 前払法人税額, 輸送費率, 輸送費額"
                    Sql += ",仕入値_外貨,仕入金額_外貨)"
                    Sql += " VALUES('"
                    Sql += CompanyCode '会社コード
                    Sql += "', '"
                    Sql += CurrentPurchaseNo '発注番号
                    Sql += "', '"
                    Sql += "1" '発注番号枝番
                    Sql += "', '"
                    Sql += tbl.Rows(i)("No") '行番号
                    Sql += "', '"
                    Sql += tbl.Rows(i)("仕入区分") '仕入区分
                    Sql += "', '"
                    Sql += tbl.Rows(i)("メーカー") 'メーカー
                    Sql += "', '"
                    Sql += tbl.Rows(i)("品名") '品名
                    Sql += "', '"
                    Sql += tbl.Rows(i)("型式") '型式
                    Sql += "', '"
                    Sql += tbl.Rows(i)("単位") '単位
                    Sql += "', '"
                    Sql += tbl.Rows(i)("仕入先") '仕入先名
                    Sql += "', '"
                    Sql += formatStringToNumber(tbl.Rows(i)("仕入単価")) '仕入値
                    Sql += "', '"
                    Sql += tbl.Rows(i)("数量") '発注数量
                    Sql += "', '"
                    Sql += "0" '仕入数量
                    Sql += "', '"
                    Sql += tbl.Rows(i)("数量") '発注残数
                    Sql += "', '"
                    Sql += formatStringToNumber(tbl.Rows(i)("仕入原価")) '仕入金額  ここでは原価を入れる
                    Sql += "', '"
                    Dim overhead As Double = 0
                    overhead = tbl.Rows(i)("仕入金額") - tbl.Rows(i)("仕入原価")
                    Sql += formatStringToNumber(overhead.ToString) '間接費
                    Sql += "', '"
                    Sql += tbl.Rows(i)("リードタイム") 'リードタイム
                    Sql += "', '"
                    Sql += tbl.Rows(i)("リードタイム単位") 'リードタイム単位
                    Sql += "', '"
                    Sql += "0" '入庫数
                    Sql += "', '"
                    Sql += tbl.Rows(i)("数量") '未入庫数
                    Sql += "', '"
                    Sql += tbl.Rows(i)("備考") '備考
                    Sql += "', '"
                    Sql += frmC01F10_Login.loginValue.TantoNM '更新者
                    Sql += "', '"
                    Sql += UtilClass.formatDatetime(dtNow) '登録日
                    Sql += "', '"
                    Sql += UtilClass.formatDatetime(dtNow) '更新日
                    Sql += "', '"
                    Sql += formatStringToNumber(tbl.Rows(i)("見積単価_外貨")) '見積単価_外貨
                    Sql += "', '"
                    Sql += formatStringToNumber(tbl.Rows(i)("見積金額_外貨")) '見積金額_外貨
                    Sql += "', "
                    Sql += CmCurrency.SelectedValue.ToString '通貨
                    Sql += ", '"
                    Sql += UtilClass.formatNumberF10(TxtRate.Text) 'レート
                    Sql += "', '"
                    Sql += formatStringToNumber(tbl.Rows(i)("仕入単価_外貨")) '仕入単価_外貨
                    Sql += "', '"
                    Sql += formatStringToNumber(tbl.Rows(i)("仕入通貨")) '仕入通貨
                    Sql += "', '"
                    Sql += UtilClass.formatNumberF10(strRate)  '仕入レート 受注日で計算し直したデータを入れる
                    Sql += "', " & formatStringToNumber(tbl.Rows(i)("関税率") / 100)             '関税率
                    Sql += ", " & formatStringToNumber(tbl.Rows(i)("関税額"))             '関税額
                    Sql += ", " & formatStringToNumber(tbl.Rows(i)("前払法人税率") / 100)       '前払法人税率
                    Sql += ", " & formatStringToNumber(tbl.Rows(i)("前払法人税額"))       '前払法人税額
                    Sql += ", " & formatStringToNumber(tbl.Rows(i)("輸送費率") / 100)         '輸送費率
                    Sql += ", " & formatStringToNumber(tbl.Rows(i)("輸送費額"))         '輸送費額


                    Sql += ", "
                    Sql += formatNumber(tbl.Rows(i)("仕入単価_外貨")) '仕入値_外貨

                    Sql += ", "
                    Sql += formatNumber(Math.Ceiling(tbl.Rows(i)("仕入金額") * strRate)) '仕入金額_外貨

                    Sql += ")"
                    _db.executeDB(Sql)

                    cost += tbl.Rows(i)("仕入原価")
                    tmpCuote += tbl.Rows(i)("見積金額_外貨")

                    lngCnt = i

                    '前回の明細データの仕入コード、仕入通貨と一致するかチェック
                    'If (sireCd <> tbl.Rows(i)("仕入先コード") Or currencyCd <> tbl.Rows(i)("仕入通貨")) Then
                    If PurchaseNo <> CurrentPurchaseNo Then

                        'If i = tbl.Rows.Count - 1 Then
                        If i < tbl.Rows.Count - 1 Then
                            If (tbl.Rows(i)("仕入先コード") <> tbl.Rows(i + 1)("仕入先コード") Or tbl.Rows(i)("仕入通貨") <> tbl.Rows(i + 1)("仕入通貨")) Then

                                '新しい値をセットし、発注基本を登録する
                                sireCd = tbl.Rows(i)("仕入先コード")
                                currencyCd = tbl.Rows(i)("仕入通貨")

                                'レートの取得
                                strRate = setRate(currencyCd)

                                hattyuHdInsert(CurrentPurchaseNo, dsSipper, cost, tmpCuote, dtNow, strRate) '発注基本更新

                                cost = 0 '伝票単位でリセット
                                tmpCuote = 0
                            Else
                                sireCd = tbl.Rows(i)("仕入先コード")
                                currencyCd = tbl.Rows(i)("仕入通貨")
                            End If
                        Else
                            hattyuHdInsert(CurrentPurchaseNo, dsSipper, cost, tmpCuote, dtNow, strRate) '発注基本更新
                        End If

                    Else

                        '新しい値をセットし、発注基本を登録する
                        costMain += tbl.Rows(i)("仕入原価")
                        tmpCuoteMain += tbl.Rows(i)("見積金額_外貨")

                    End If
                End If

            Next

            '在庫引当のみの場合、tblのデータなし
            If tbl.Rows.Count > 0 Then
                '最終レコードの処理
                '新しい値をセットし、発注基本を登録する
                sireCd = tbl.Rows(lngCnt)("仕入先コード")
                currencyCd = tbl.Rows(lngCnt)("仕入通貨")

                'レートの取得
                strRate = setRate(currencyCd)

                hattyuHdInsert(CurrentPurchaseNo, dsSipper, cost, tmpCuote, dtNow, strRate) '発注基本更新
            End If


#Region "t20_hattyu 発注基本登録"

            '最初に発行した発注番号の基本を作成
            '---------------------------------------------
            'Sql = " AND 仕入先コード ILIKE '" & tbl.Rows(0)("仕入先コード") & "'"
            'dsSipper = getDsData("m11_supplier", Sql) '仕入先情報の取得

            'strRate = setRate(tbl.Rows(0)("仕入通貨")) 'レートの取得

            'hattyuHdInsert(PurchaseNo, dsSipper, cost, tmpCuote, dtNow, strRate) '発注明細更新

#End Region

#Region "t01_mithd 見積基本 -受注日登録"

            '呼び出した見積に受注日を入れる
            Sql = ""
                Sql += "UPDATE Public.t01_mithd "
                Sql += "SET  受注日 = '" & UtilClass.strFormatDate(DtpOrderDate.Value) & "'"
                Sql += ", 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
                Sql += ", 更新日 = '" & UtilClass.formatDatetime(dtNow) & "'"
                Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " AND 見積番号 = '" & QuoteNo.ToString & "'"
                Sql += " AND 見積番号枝番 = '" & QuoteSuffix.ToString & "'"

                _db.executeDB(Sql)
#End Region

                '登録完了メッセージ
                _msgHd.dspMSG("completeInsert", frmC01F10_Login.loginValue.Language)

                '画面クローズ
                _parentForm.Enabled = True
                _parentForm.Show()
                Me.Dispose()
            End If

    End Sub

    Private Sub hattyuHdInsert(ByVal PurchaseNo As String, ByVal dsSipper As DataSet, ByVal cost As Long, ByVal tmpCuote As Long, ByVal dtNow As DateTime, ByVal strRate As Decimal)

        Dim Sql As String = ""

        '基本データ更新
        Sql = "INSERT INTO "
        Sql += "Public."
        Sql += "t20_hattyu("
        Sql += "会社コード, 発注番号, 発注番号枝番, 客先番号, 受注番号, 受注番号枝番, 見積番号, 見積番号枝番, 得意先コード, 得意先名"
        Sql += ", 得意先郵便番号, 得意先住所, 得意先電話番号, 得意先ＦＡＸ, 得意先担当者役職, 得意先担当者名, 仕入先コード, 仕入先名"
        Sql += ", 仕入先郵便番号, 仕入先住所, 仕入先電話番号, 仕入先ＦＡＸ, 仕入先担当者役職, 仕入先担当者名, 見積日, 見積有効期限"
        Sql += ", 支払条件, 見積金額,仕入金額, 粗利額, 営業担当者, 営業担当者コード, 入力担当者, 入力担当者コード, 備考, 見積備考"
        Sql += ", ＶＡＴ, ＰＰＨ, 受注日, 発注日, 登録日, 更新日, 更新者, 取消区分, 倉庫コード, 見積金額_外貨, 通貨, レート"
        Sql += ", 仕入金額_外貨)"

        Sql += " VALUES('"
        Sql += CompanyCode '会社コード
        Sql += "', '"
        Sql += PurchaseNo '発注番号
        Sql += "', '"
        Sql += "1" '発注番号枝番
        Sql += "', '"
        Sql += RevoveChars(TxtCustomerPO.Text) '客先番号
        Sql += "', '"
        Sql += TxtOrderNo.Text '受注番号
        Sql += "', '"
        Sql += TxtOrderSuffix.Text '受注番号枝番
        Sql += "', '"
        Sql += TxtQuoteNo.Text '見積番号
        Sql += "', '"
        Sql += TxtQuoteSuffix.Text '見積番号枝番
        Sql += "', '"
        Sql += TxtCustomerCode.Text '得意先コード
        Sql += "', '"
        Sql += TxtCustomerName.Text '得意先名
        Sql += "', '"
        Sql += TxtPostalCode.Text '得意先郵便番号
        Sql += "', '"
        Sql += TxtAddress1.Text '得意先住所
        Sql += "', '"
        Sql += TxtTel.Text '得意先電話番号
        Sql += "', '"
        Sql += TxtFax.Text '得意先ＦＡＸ
        Sql += "', '"
        Sql += TxtPosition.Text '得意先担当者役職
        Sql += "', '"
        Sql += TxtPerson.Text '得意先担当者名
        Sql += "', '"
        Sql += dsSipper.Tables(RS).Rows(0)("仕入先コード").ToString '仕入先コード
        Sql += "', '"
        Sql += dsSipper.Tables(RS).Rows(0)("仕入先名").ToString '仕入先名
        Sql += "', '"
        Sql += dsSipper.Tables(RS).Rows(0)("郵便番号").ToString '仕入先郵便番号
        Sql += "', '"
        Sql += dsSipper.Tables(RS).Rows(0)("住所１").ToString '仕入先住所
        Sql += " "
        Sql += dsSipper.Tables(RS).Rows(0)("住所２").ToString '仕入先住所
        Sql += " "
        Sql += dsSipper.Tables(RS).Rows(0)("住所３").ToString '仕入先住所
        Sql += "', '"
        Sql += dsSipper.Tables(RS).Rows(0)("電話番号").ToString '仕入先電話番号
        Sql += "', '"
        Sql += dsSipper.Tables(RS).Rows(0)("ＦＡＸ番号").ToString '仕入先ＦＡＸ
        Sql += "', '"
        Sql += dsSipper.Tables(RS).Rows(0)("担当者役職").ToString '仕入先担当者役職
        Sql += "', '"
        Sql += dsSipper.Tables(RS).Rows(0)("担当者名").ToString '仕入先担当者名
        Sql += "', '"
        Sql += UtilClass.strFormatDate(DtpQuoteDate.Value) '見積日
        Sql += "', '"
        Sql += UtilClass.strFormatDate(DtpExpiration.Value) '見積有効期限
        Sql += "', '"
        Sql += TxtPaymentTerms.Text '支払条件
        Sql += "', '"
        Sql += formatStringToNumber(TxtOrderAmount.Text) '見積金額
        Sql += "', '"
        Sql += formatStringToNumber(cost.ToString) '仕入金額　ここでは原価を入れる
        Sql += "', '"
        Sql += formatStringToNumber(TxtGrossProfit.Text) '粗利額
        Sql += "', '"
        Sql += TxtSales.Text '営業担当者
        Sql += "', '"
        Sql += TxtSales.Tag '営業担当者コード
        Sql += "', '"
        Sql += TxtInput.Text '入力担当者
        Sql += "', '"
        Sql += TxtInput.Tag '入力担当者コード
        Sql += "', '"
        Sql += TxtPurchaseRemark.Text '備考
        Sql += "', '"
        Sql += TxtQuoteRemarks.Text '見積備考
        Sql += "', '"
        Sql += formatStringToNumber(TxtVat.Text) 'ＶＡＴ
        Sql += "', '"
        Sql += formatStringToNumber(TxtPph.Text) 'ＰＰＨ
        Sql += "', '"
        Sql += UtilClass.strFormatDate(DtpOrderDate.Value) '受注日
        Sql += "', '"
        Sql += UtilClass.strFormatDate(DtpPurchaseDate.Value) '発注日
        Sql += "', '"
        Sql += UtilClass.strFormatDate(DtpOrderRegistration.Value) '登録日（受発注登録日）
        Sql += "', '"
        Sql += UtilClass.formatDatetime(dtNow) '更新日
        Sql += "', '"
        Sql += frmC01F10_Login.loginValue.BumonCD '更新者
        Sql += "', '"
        Sql += "0" '取消区分
        Sql += "', '"
        If CmWarehouse.SelectedIndex <> -1 Then
            Sql += CmWarehouse.SelectedValue.ToString '倉庫コード
        Else
            Sql += "" '倉庫コード
        End If
        Sql += "', '"
        Sql += formatStringToNumber(tmpCuote) '見積金額_外貨
        Sql += "', "
        Sql += CmCurrency.SelectedValue.ToString '通貨
        Sql += ", '"
        Sql += UtilClass.formatNumberF10(TxtRate.Text) 'レート
        Sql += "', '"
        Sql += formatNumber(Math.Ceiling(cost.ToString * strRate)) '仕入金額_外貨
        Sql += "')"


        _db.executeDB(Sql)

    End Sub


    ''' <summary>
    ''' 指定した文字列から指定した文字を全て削除する
    ''' </summary>
    ''' <param name="s">対象となる文字列。</param>
    ''' <returns>sに含まれている全てのcharacters文字が削除された文字列。</returns>
    Public Shared Function RevoveChars(s As String) As String
        Dim buf As New System.Text.StringBuilder(s)
        '削除する文字の配列
        Dim removeChars As Char() = New Char() {vbCr, vbLf, Chr(39)}

        For Each c As Char In removeChars
            buf.Replace(c.ToString(), "")
        Next
        Return buf.ToString()
    End Function

    '金額フォーマット（登録の際の小数点指定子）を日本の形式に合わせる
    '桁区切り記号は外す
    Private Function formatNumber(ByVal prmVal As Decimal) As String

        Dim nfi As NumberFormatInfo = New CultureInfo(CommonConst.CI_JP, False).NumberFormat

        '日本の形式に書き換える
        Return prmVal.ToString("F3", nfi)
    End Function

    '金額フォーマット（登録の際の小数点指定子）を日本の形式に合わせる
    '桁区切り記号は外す
    Private Function formatStringToNumber(ByVal prmVal As String) As String

        Dim decVal As Decimal = Decimal.Parse(prmVal)

        Dim nfi As NumberFormatInfo = New CultureInfo(CommonConst.CI_JP, False).NumberFormat

        '日本の形式に書き換える
        Return decVal.ToString("F3", nfi)
    End Function

    '受注日変更時
    Private Sub DtpOrderDate_ValueChanged(sender As Object, e As EventArgs) Handles DtpOrderDate.ValueChanged

        '発注日が受注日より小さかったら
        If DtpOrderDate.Value.ToString("yyyyMMdd") > DtpPurchaseDate.Value.ToString("yyyyMMdd") Then
            DtpPurchaseDate.Value = DtpOrderDate.Value
        End If

    End Sub

    '発注日変更時
    Private Sub DtpPurchaseDate_ValueChanged(sender As Object, e As EventArgs) Handles DtpPurchaseDate.ValueChanged
        '発注日が受注日より小さかったら
        If DtpOrderDate.Value.ToString("yyyyMMdd") > DtpPurchaseDate.Value.ToString("yyyyMMdd") Then
            DtpOrderDate.Value = DtpPurchaseDate.Value
        End If

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
        Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += txtParam

        Return _db.selectDB(Sql, RS, reccnt)
    End Function

    'Return: DataTable
    Private Function getSireKbn() As DataTable
        Dim Sql As String = ""
        Dim strViewText As String = ""

        Sql = " AND "
        Sql += "固定キー ILIKE '" & CommonConst.FIXED_KEY_PURCHASING_CLASS & "'"
        Sql += " ORDER BY 表示順"

        'リードタイムのリストを汎用マスタから取得
        Dim dsHanyo As DataSet = getDsData("m90_hanyo", Sql)

        'ComboBoxに表示する項目のリストを作成する
        Dim table As New DataTable("Table")
        table.Columns.Add("Display", GetType(String))
        table.Columns.Add("Value", GetType(Integer))

        strViewText = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "文字２", "文字１")

        For x As Integer = 0 To dsHanyo.Tables(RS).Rows.Count - 1
            table.Rows.Add(dsHanyo.Tables(RS).Rows(x)(strViewText), dsHanyo.Tables(RS).Rows(x)("可変キー"))
        Next

        Return table
    End Function

    '倉庫のコンボボックスを作成
    '編集モードの時は値を渡してセットさせる
    Private Sub createWarehouseCombobox(Optional ByRef prmVal As String = "")
        CmWarehouse.DisplayMember = "Text"
        CmWarehouse.ValueMember = "Value"

        Dim Sql As String = " AND 無効フラグ = '" & CommonConst.FLAG_ENABLED & "'"

        Dim ds As DataSet = getDsData("m20_warehouse", Sql)

        Dim tb As New DataTable
        tb.Columns.Add("Text", GetType(String))
        tb.Columns.Add("Value", GetType(String))

        For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
            tb.Rows.Add(ds.Tables(RS).Rows(i)("名称"), ds.Tables(RS).Rows(i)("倉庫コード"))

        Next

        CmWarehouse.DataSource = tb

        '倉庫データがあったら
        If ds.Tables(RS).Rows.Count > 0 Then
            If prmVal IsNot "" Then
                CmWarehouse.SelectedValue = prmVal
            Else
                CmWarehouse.SelectedIndex = 0
            End If
        End If

    End Sub

    '通貨のコンボボックスを作成
    '編集モードの時は値を渡してセットさせる
    Private Sub createCurrencyCombobox(Optional ByRef prmVal As String = "")
        CmCurrency.DisplayMember = "Text"
        CmCurrency.ValueMember = "Value"

        Dim Sql As String = " AND 取消区分 = '" & CommonConst.FLAG_ENABLED & "'"

        Dim ds As DataSet = getDsData("m25_currency", Sql)

        Dim tb As New DataTable
        tb.Columns.Add("Text", GetType(String))
        tb.Columns.Add("Value", GetType(String))

        For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
            tb.Rows.Add(ds.Tables(RS).Rows(i)("通貨コード"), ds.Tables(RS).Rows(i)("採番キー"))

        Next

        CmCurrency.DataSource = tb

        If prmVal IsNot "" Then
            CmCurrency.SelectedValue = prmVal
        Else
            CmCurrency.SelectedIndex = 0
        End If
    End Sub

    '通貨の採番キーからレートを取得・設定
    '基準日が見積日「以前」の最新のもの
    Private Sub setRate()
        Dim Sql As String

        Sql = " AND 採番キー = " & CmCurrency.SelectedValue.ToString & ""
        Sql += " AND 基準日 < '" & UtilClass.strFormatDate(DtpQuoteDate.Text) & "'"
        Sql += " ORDER BY 基準日 DESC "

        Dim ds As DataSet = getDsData("t71_exchangerate", Sql)

        If ds.Tables(RS).Rows.Count > 0 Then
            TxtRate.Text = ds.Tables(RS).Rows(0)("レート")
        Else
            If CultureInfo.CurrentCulture.Name.ToString = CommonConst.CI_ID Then
                TxtRate.Text = CommonConst.BASE_RATE_IDR
            Else
                TxtRate.Text = CommonConst.BASE_RATE_JPY
            End If
        End If

    End Sub

    'Return: DataTable
    Private Function getReadTime() As DataTable

        Dim Sql As String = ""
        Sql += " AND 固定キー = '" & CommonConst.FIXED_KEY_READTIME & "'"

        Dim ds As DataSet = getDsData("m90_hanyo", Sql)

        Dim table As New DataTable("Table")
        table.Columns.Add("Display", GetType(String))
        table.Columns.Add("Value", GetType(Integer))

        'リードタイム単位の多言語対応
        For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                table.Rows.Add(ds.Tables(RS).Rows(i)("文字２"), ds.Tables(RS).Rows(i)("可変キー"))
            Else
                table.Rows.Add(ds.Tables(RS).Rows(i)("文字１"), ds.Tables(RS).Rows(i)("可変キー"))
            End If

        Next
        Return table
    End Function

    'Return: DataTable
    Private Function getSireCurrency() As DataTable
        Dim Sql As String = " AND 取消区分 = '" & CommonConst.FLAG_ENABLED & "'"

        Dim ds As DataSet = getDsData("m25_currency", Sql)

        Dim table As New DataTable("Table")
        table.Columns.Add("Display", GetType(String))
        table.Columns.Add("Value", GetType(Integer))

        'リードタイム単位の多言語対応
        For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
            table.Rows.Add(ds.Tables(RS).Rows(i)("通貨コード"), ds.Tables(RS).Rows(i)("採番キー"))
        Next
        Return table
    End Function


    Private Function getCurrency(ByVal prmVal As Integer) As String
        Dim Sql As String = " AND 取消区分 = '" & CommonConst.FLAG_ENABLED & "'"
        Sql += " AND 採番キー =" & prmVal.ToString

        Dim ds As DataSet = getDsData("m25_currency", Sql)

        'リードタイム単位の多言語対応

        Return ds.Tables(RS).Rows(0)("通貨コード")

    End Function

    Private Sub setBaseCurrency()
        Dim Sql As String
        '通貨表示：ベースの設定
        Sql = " AND 採番キー = " & CommonConst.CURRENCY_CD_IDR.ToString
        Sql += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString

        Dim ds As DataSet = getDsData("m25_currency", Sql)
        TxtIDRCurrency.Text = ds.Tables(RS).Rows(0)("通貨コード")

    End Sub

    '通貨表示：通貨変更の設定
    Private Sub setChangeCurrency()
        Dim Sql As String
        Sql = " AND 採番キー = " & CmCurrency.SelectedValue.ToString
        Sql += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString

        Dim ds As DataSet = getDsData("m25_currency", Sql)
        TxtChangeCurrency.Text = ds.Tables(RS).Rows(0)("通貨コード")
    End Sub

    'Currencyに応じて変換
    Private Sub setCurrency()

        Dim currencyVal As Decimal = IIf(TxtRate.Text <> "", TxtRate.Text, 0)
        Dim vatVal As Decimal = IIf(TxtVatAmount.Text <> "", TxtVatAmount.Text, 0)
        Dim sumVal As Decimal = IIf(TxtOrderAmount.Text <> "", TxtOrderAmount.Text, 0)
        Dim QuoteCurrencyTotal As Decimal = 0       '見積金額_外貨

        TxtCurrencyVatAmount.Text = (vatVal * currencyVal).ToString("N2")
        'TxtCurrencyOrderTotal.Text = (sumVal * currencyVal).ToString("F0")

        For c As Integer = 0 To DgvItemList.Rows.Count - 1
            QuoteCurrencyTotal += DgvItemList.Rows(c).Cells("見積金額_外貨").Value
        Next
        TxtCurrencyOrderTotal.Text = QuoteCurrencyTotal.ToString("N2")
    End Sub

    '在庫マスタから現在庫数を取得
    Private Function setZaikoQuantity(ByVal rowIndex As Integer) As Long
        Dim Sql As String = ""
        Dim reccnt As Integer = 0 'DB用（デフォルト）

        Sql = "SELECT sum(現在庫数) as 在庫数量 from m21_zaiko"

        Sql += " WHERE 会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"

        Sql += " AND メーカー ILIKE '" & DgvItemList.Rows(rowIndex).Cells("メーカー").Value.ToString & "'"
        Sql += " AND 品名 ILIKE '" & DgvItemList.Rows(rowIndex).Cells("品名").Value.ToString & "'"
        Sql += " AND 型式 ILIKE '" & DgvItemList.Rows(rowIndex).Cells("型式").Value.ToString & "'"
        Sql += " AND 倉庫コード ILIKE '" & CmWarehouse.SelectedValue & "'"
        Sql += " AND 入出庫種別 <= '" & CommonConst.INOUT_KBN_NORMAL & "'"
        Sql += " AND 現在庫数 <> 0"

        'Sql += " GROUP BY 倉庫コード, 入出庫種別, 最終入庫日 "
        'Sql += " ORDER BY 最終入庫日 "

        '在庫マスタから現在庫数を取得
        Dim dsZaiko As DataSet = _db.selectDB(Sql, RS, reccnt)

        If dsZaiko.Tables(RS).Rows.Count > 0 And dsZaiko.Tables(RS).Rows(0)("在庫数量") IsNot DBNull.Value Then
            Return dsZaiko.Tables(RS).Rows(0)("在庫数量")
        Else
            Return 0
        End If

    End Function

    'param1：String 採番キー
    'param2：DateTime 登録日
    'Return: String 伝票番号
    '伝票番号を取得
    Private Function getSaiban(ByVal key As String, ByVal today As DateTime) As String
        Dim Sql As String = ""
        Dim saibanID As String = ""
        Dim reccnt As Integer = 0 'DB用（デフォルト）

        Try
            Sql = "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public.m80_saiban"
            Sql += " WHERE "
            Sql += "会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND "
            Sql += "採番キー = '" & key & "'"

            Dim dsSaiban As DataSet = _db.selectDB(Sql, RS, reccnt)

            saibanID = dsSaiban.Tables(RS).Rows(0)("接頭文字")
            saibanID += today.ToString("MMdd")
            saibanID += dsSaiban.Tables(RS).Rows(0)("最新値").ToString.PadLeft(dsSaiban.Tables(RS).Rows(0)("連番桁数"), "0")

            Dim keyNo As Integer

            If dsSaiban.Tables(RS).Rows(0)("最新値") = dsSaiban.Tables(RS).Rows(0)("最大値") Then
                '最新値が最大と同じ場合、最小値にリセット
                keyNo = dsSaiban.Tables(RS).Rows(0)("最小値")
            Else
                '最新値+1
                keyNo = dsSaiban.Tables(RS).Rows(0)("最新値") + 1
            End If

            Sql = "UPDATE "
            Sql += "Public.m80_saiban "
            Sql += "SET "
            Sql += " 最新値 "
            Sql += " = '"
            Sql += keyNo.ToString
            Sql += "', "
            Sql += "更新者"
            Sql += " = '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            Sql += "', "
            Sql += "更新日"
            Sql += " = '"
            Sql += UtilClass.formatDatetime(today)
            Sql += "' "
            Sql += "WHERE"
            Sql += " 会社コード"
            Sql += "='"
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "'"
            Sql += " AND"
            Sql += " 採番キー = '" & key & "'"

            _db.executeDB(Sql)

            Return saibanID
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Function

    '在庫マスタから現在庫数一覧を取得
    '仕入区分 2 の時
    Private Function getZaikoList(ByVal rowIndex As Integer) As DataSet
        Dim Sql As String = ""
        Dim reccnt As Integer = 0 'DB用（デフォルト）

        Sql = "SELECT sum(現在庫数) as 現在庫数, 入出庫種別, 伝票番号, 行番号 from m21_zaiko"

        Sql += " WHERE 会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"

        Sql += " AND  メーカー ILIKE '" & DgvItemList.Rows(rowIndex).Cells("メーカー").Value.ToString & "'"
        Sql += " AND  品名 ILIKE '" & DgvItemList.Rows(rowIndex).Cells("品名").Value.ToString & "'"
        Sql += " AND  型式 ILIKE '" & DgvItemList.Rows(rowIndex).Cells("型式").Value.ToString & "'"
        Sql += " AND  倉庫コード ILIKE '" & CmWarehouse.SelectedValue.ToString & "'"
        Sql += " AND  入出庫種別 ILIKE '" & CommonConst.INOUT_KBN_NORMAL & "'"
        Sql += " AND  無効フラグ = " & CommonConst.CANCEL_KBN_ENABLED
        Sql += " AND  現在庫数 <> 0"

        Sql += " GROUP BY 倉庫コード, 入出庫種別, 最終入庫日, 伝票番号, 行番号 "
        Sql += " ORDER BY 最終入庫日 "

        '在庫マスタから現在庫数を取得
        Dim dsZaiko As DataSet = _db.selectDB(Sql, RS, reccnt)

        Return dsZaiko

    End Function

    '入庫マスタから現在庫数一覧を取得
    '仕入区分 2 の時
    Private Function getNukoList(ByVal rowIndex As Integer) As DataSet
        Dim Sql As String = ""
        Dim reccnt As Integer = 0 'DB用（デフォルト）

        Sql = " SELECT t42.入庫番号 "
        Sql += " FROM t10_cymnhd t10  "

        Sql += " LEFT JOIN t20_hattyu t20 "
        Sql += " ON "
        Sql += " t10.会社コード = t20.会社コード "
        Sql += " AND "
        Sql += " t10.受注番号 = t20.受注番号 "
        Sql += " AND "
        Sql += " t10.受注番号枝番 = t20.受注番号枝番 "
        Sql += " AND "
        Sql += " t20.発注番号枝番 = (SELECT MAX(発注番号枝番) AS 発注番号枝番 FROM t20_hattyu) "

        Sql += " LEFT JOIN t42_nyukohd t42 "
        Sql += " ON "
        Sql += " t20.会社コード = t42.会社コード "
        Sql += " AND "
        Sql += " t20.発注番号 = t42.発注番号 "
        Sql += " AND "
        Sql += " t20.発注番号枝番 = t42.発注番号枝番 "
        Sql += " AND "
        Sql += " t20.仕入先コード = t42.仕入先コード "

        Sql += " LEFT JOIN t43_nyukodt t43 "
        Sql += " ON "
        Sql += " t42.会社コード = t43.会社コード "
        Sql += " AND "
        Sql += " t42.入庫番号 = t43.入庫番号 "

        Sql += " WHERE "
        Sql += " t10.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND "
        Sql += " t10.受注番号 = '" & TxtOrderNo.Text & "'"
        Sql += " AND "
        Sql += " t10.受注番号枝番 = '" & TxtOrderSuffix.Text & "'"

        Sql += " AND t43.メーカー ILIKE '" & DgvItemList.Rows(rowIndex).Cells("メーカー").Value.ToString & "'"
        Sql += " AND t43.品名 ILIKE '" & DgvItemList.Rows(rowIndex).Cells("品名").Value.ToString & "'"
        Sql += " AND t43.型式 ILIKE '" & DgvItemList.Rows(rowIndex).Cells("型式").Value.ToString & "'"

        Sql += " AND t42.倉庫コード ILIKE '" & CmWarehouse.SelectedValue.ToString & "'"

        Sql += " AND t43.仕入区分 = '" & DgvItemList.Rows(rowIndex).Cells("仕入区分").Value.ToString & "'"

        Dim dsNyukoList As DataSet = _db.selectDB(Sql, RS, reccnt)

        '取得した入庫番号一覧から現在庫数を取得
        Sql = "SELECT sum(現在庫数) as 現在庫数, 入出庫種別, 伝票番号, 行番号 from m21_zaiko"

        Sql += " WHERE 会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"

        Sql += " AND メーカー ILIKE '" & DgvItemList.Rows(rowIndex).Cells("メーカー").Value.ToString & "'"
        Sql += " AND 品名 ILIKE '" & DgvItemList.Rows(rowIndex).Cells("品名").Value.ToString & "'"
        Sql += " AND 型式 ILIKE '" & DgvItemList.Rows(rowIndex).Cells("型式").Value.ToString & "'"
        Sql += " AND 倉庫コード ILIKE '" & CmWarehouse.SelectedValue.ToString & "'"
        Sql += " AND 入出庫種別 ILIKE '" & CommonConst.INOUT_KBN_NORMAL & "'"
        Sql += " AND 無効フラグ = " & CommonConst.CANCEL_KBN_ENABLED
        Sql += " AND 現在庫数 <> 0"

        If dsNyukoList.Tables(RS).Rows.Count > 0 Then
            Sql += " AND ( "
            For i As Integer = 0 To dsNyukoList.Tables(RS).Rows.Count - 1
                Sql += IIf(i > 0, " OR ", "")
                Sql += " 伝票番号 ILIKE '" & dsNyukoList.Tables(RS).Rows(i)("入庫番号") & "'"
            Next
            Sql += " ) "
        End If

        Sql += " GROUP BY 倉庫コード, 入出庫種別, 最終入庫日, 伝票番号, 行番号 "
        Sql += " ORDER BY 最終入庫日 "

        '在庫マスタから現在庫数を取得
        Dim dsZaiko As DataSet = _db.selectDB(Sql, RS, reccnt)

        Return dsZaiko

    End Function

    '通貨の採番キーからレートを取得・設定
    '基準日が受注日「以前」の最新のもの
    Private Function setRate(ByVal strKey As Integer) As Decimal
        Dim Sql As String

        Sql = " AND 採番キー = " & strKey & ""
        Sql += " AND 基準日 < '" & UtilClass.strFormatDate(DtpOrderDate.Text) & "'"  '受注日
        Sql += " ORDER BY 基準日 DESC "

        Dim ds As DataSet = getDsData("t71_exchangerate", Sql)

        If ds.Tables(RS).Rows.Count > 0 Then
            setRate = ds.Tables(RS).Rows(0)("レート")
        Else
            If CultureInfo.CurrentCulture.Name.ToString = CommonConst.CI_ID Then
                setRate = CommonConst.BASE_RATE_IDR
            Else
                setRate = CommonConst.BASE_RATE_JPY
            End If
        End If

    End Function
End Class