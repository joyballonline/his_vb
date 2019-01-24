Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices


Public Class Quote
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
    Private _parentForm As Form
    Private _init As Boolean                             '初期処理済フラグ
    Private count As Integer = 0
    Private CompanyCode As String = ""
    Private KeyNo As String = ""
    Private QuoteNo As String = ""
    Private QuoteNoMin As String = ""
    Private QuoteNoMax As String = ""
    Private EditNo As String = ""
    Private EditSuffix As String = ""
    Private LoadFlg As Boolean = False
    Private Status As String = ""
    Private Input As String = ""

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
                   ByRef prmRefLangHd As UtilLangHandler,
                   ByRef prmRefForm As Form,
                   Optional ByRef prmRefNo As String = Nothing,
                   Optional ByRef prmRefSuffix As String = Nothing,
                   Optional ByRef prmRefStatus As String = Nothing)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLangHd
        _parentForm = prmRefForm
        EditNo = prmRefNo
        EditSuffix = prmRefSuffix
        Status = prmRefStatus
        Input = frmC01F10_Login.loginValue.TantoNM
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True



    End Sub

    Private Sub Quote_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'DateTimePickerのフォーマットを指定
        DtpRegistration.Text = DateAdd("m", 0, Now).ToString("yyyy/MM/dd")
        DtpQuote.Text = DateAdd("m", 0, Now).ToString("yyyy/MM/dd")
        DtpExpiration.Text = DateAdd("d", 7, Now).ToString("yyyy/MM/dd")

        DgvItemList.Columns("No").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns("数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns("仕入単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns("間接費率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns("間接費無仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns("売単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns("売上金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns("粗利額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns("粗利率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns("リードタイム").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        'セルの内容に合わせて、行の高さが自動的に調節されるようにする
        DgvItemList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        '"Column1"列のセルのテキストを折り返して表示する
        DgvItemList.Columns("型式").DefaultCellStyle.WrapMode = DataGridViewTriState.True
        DgvItemList.Columns("備考").DefaultCellStyle.WrapMode = DataGridViewTriState.True

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

        For i As Integer = 0 To ds12.Tables(RS).Rows.Count - 1
            table2.Rows.Add(ds12.Tables(RS).Rows(i)("文字１"), ds12.Tables(RS).Rows(i)("可変キー"))
        Next

        Dim column2 As New DataGridViewComboBoxColumn()
        column2.DataSource = table2
        column2.ValueMember = "Value"
        column2.DisplayMember = "Display"
        column2.HeaderText = "リードタイム単位"
        column2.Name = "リードタイム単位"

        DgvItemList.Columns.Insert(25, column2)

        If frmC01F10_Login.loginValue.Language = "ENG" Then
            LblMode.Text = "Mode"
            LblQuoteNo.Text = "QuoteNo"
            LblRegistration.Text = "RegistrationDate"
            LblQuote.Text = "QuoteDate"
            LblExpiration.Text = "Expiration"
            LblCustomerName.Text = "CustomerName"
            LblAddress.Text = "Address"
            LblTel.Text = "Tel"
            LblFax.Text = "Fax"
            LblPerson.Text = "Person"
            LblPosition.Text = "Position"
            LblSales.Text = "Sales"
            LblInput.Text = "Input"
            LblPaymentTerms.Text = "PaymentTerms"
            LblRemarks.Text = "Remarks"
            LblItemCount.Text = "ItemCount"
            LblPurchaseAmount.Text = "PurchaseAmount"
            LblOrderAmount.Text = "OrderAmount"
            LblQuoteAmount.Text = "QuoteAmount"
            LblGrossProfit.Text = "GrossProfit"
            LblVat.Text = "Vat"

            RbtnGP.Text = "GrossProfitInput"
            RbtnUP.Text = "SellingPriceInput"
            RbtnQuote.Text = "QuotationPriceInput"

            BtnCodeSearch.Text = "Search"
            BtnInsert.Text = "Insert"
            BtnUp.Text = "Up"
            BtnDown.Text = "Down"
            BtnRowsAdd.Text = "Add"
            BtnRowsDel.Text = "Delete"
            BtnClone.Text = "Clone"
            BtnProof.Text = "Proof"
            BtnQuoteRequest.Text = "QuoteRequest"
            BtnQuote.Text = "Quote"
            BtnRegistration.Text = "Registration"
            BtnBack.Text = "Back"

            LblRegistration.Size = New Size(156, 23)
            LblRegistration.Location = New Point(274, 15)
            DtpRegistration.Location = New Point(436, 16)
            LblQuote.Location = New Point(592, 14)
            DtpQuote.Location = New Point(710, 16)
            LblExpiration.Location = New Point(866, 15)
            DtpExpiration.Location = New Point(982, 16)

            BtnCodeSearch.Size = New Size(108, 23)
            BtnCodeSearch.Location = New Point(212, 63)
            TxtCustomerName.Size = New Size(278, 23)
            TxtCustomerName.Location = New Point(326, 63)

            LblItemCount.Size = New Size(125, 23)
            LblItemCount.Location = New Point(1216, 179)
            TxtItemCount.Size = New Size(125, 23)
            TxtItemCount.Location = New Point(1216, 208)
            RbtnUP.Location = New Point(712, 210)
            RbtnGP.Location = New Point(879, 210)
            RbtnQuote.Location = New Point(1038, 210)

            LblVat.Location = New Point(629, 422)
            TxtVat.Location = New Point(735, 422)
            LblGrossProfit.Location = New Point(628, 451)
            TxtGrossProfit.Location = New Point(734, 451)
            LblPurchaseAmount.Location = New Point(972, 422)
            LblPurchaseAmount.Size = New Size(132, 23)
            LblOrderAmount.Location = New Point(972, 451)
            LblOrderAmount.Size = New Size(132, 23)
            LblQuoteAmount.Location = New Point(972, 480)
            LblQuoteAmount.Size = New Size(132, 23)

            DgvItemList.Columns("仕入区分").HeaderText = "PurchaseSection"
            DgvItemList.Columns("メーカー").HeaderText = "Maker"
            DgvItemList.Columns("品名").HeaderText = "Item"
            DgvItemList.Columns("型式").HeaderText = "Model"
            DgvItemList.Columns("数量").HeaderText = "Quantity"
            DgvItemList.Columns("単位").HeaderText = "Unit"
            DgvItemList.Columns("仕入先コード").HeaderText = "SupplierCode"
            DgvItemList.Columns("仕入先").HeaderText = "SupplierName"
            DgvItemList.Columns("仕入単価").HeaderText = "PurchasePrice"
            DgvItemList.Columns("仕入原価").HeaderText = "PurchsingCost"
            DgvItemList.Columns("関税率").HeaderText = "TariffRate"
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

        If EditNo IsNot Nothing Then    '見積編集時
            '見積基本情報
            Dim Sql1 As String = ""
            Sql1 += "SELECT "
            Sql1 += "* "
            Sql1 += "FROM "
            Sql1 += "public"
            Sql1 += "."
            Sql1 += "t01_mithd"
            Sql1 += " WHERE "
            Sql1 += "見積番号"
            Sql1 += " ILIKE "
            Sql1 += "'%"
            Sql1 += EditNo.ToString
            Sql1 += "%'"
            Sql1 += " AND "
            Sql1 += "見積番号枝番"
            Sql1 += " ILIKE "
            Sql1 += "'%"
            Sql1 += EditSuffix.ToString
            Sql1 += "%'"

            Dim ds1 = _db.selectDB(Sql1, RS, reccnt)

            Dim Sql2 As String = ""
            Sql2 += "SELECT "
            Sql2 += "見積番号枝番 "
            Sql2 += "FROM "
            Sql2 += "public"
            Sql2 += "."
            Sql2 += "t01_mithd"
            Sql2 += " WHERE "
            Sql2 += "見積番号"
            Sql2 += " ILIKE "
            Sql2 += "'%"
            Sql2 += EditNo.ToString
            Sql2 += "%'"

            Dim ds2 = _db.selectDB(Sql2, RS, reccnt)
            Dim SuffixMax As Integer = 0
            If Status Is "CLONE" Then
                Dim Sql As String = ""
                Sql += "SELECT "
                Sql += "会社コード, "
                Sql += "採番キー, "
                Sql += "最新値, "
                Sql += "最小値, "
                Sql += "最大値, "
                Sql += "接頭文字, "
                Sql += "連番桁数 "
                Sql += "FROM "
                Sql += "public"
                Sql += "."
                Sql += "m80_saiban"
                Sql += " WHERE "
                Sql += "採番キー"
                Sql += " ILIKE "
                Sql += "'10'"


                Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)
                Dim dtNow As DateTime = DateTime.Now
                ' 指定した書式で日付を文字列に変換する
                Dim QuoteDate As String = dtNow.ToString("MMdd")

                TxtQuoteNo.Text += ds.Tables(RS).Rows(0)(5)
                TxtQuoteNo.Text += QuoteDate
                CompanyCode = ds.Tables(RS).Rows(0)(0)
                KeyNo = ds.Tables(RS).Rows(0)(1)
                QuoteNo = ds.Tables(RS).Rows(0)(2)
                QuoteNoMin = ds.Tables(RS).Rows(0)(3)
                QuoteNoMax = ds.Tables(RS).Rows(0)(4)
                TxtQuoteNo.Text += QuoteNo.PadLeft(ds.Tables(RS).Rows(0)(6), "0")

                SaibanSave()
            Else
                TxtQuoteNo.Text = ds1.Tables(RS).Rows(0)(1)
                For index As Integer = 0 To ds2.Tables(RS).Rows.Count - 1
                    If SuffixMax <= ds2.Tables(RS).Rows(index)(0) Then
                        SuffixMax = ds2.Tables(RS).Rows(index)(0)
                    End If
                Next
            End If

            CompanyCode = ds1.Tables(RS).Rows(0)(0)

            If Status IsNot "PRICE" Then
                TxtSuffixNo.Text = SuffixMax + 1
            Else
                TxtSuffixNo.Text = ds1.Tables(RS).Rows(0)("見積番号枝番")
            End If

            DtpQuote.Value = ds1.Tables(RS).Rows(0)("見積日")
            DtpExpiration.Value = ds1.Tables(RS).Rows(0)("見積有効期限")
            TxtCustomerCode.Text = ds1.Tables(RS).Rows(0)("得意先コード")
            TxtCustomerName.Text = ds1.Tables(RS).Rows(0)("得意先名")
            TxtPerson.Text = ds1.Tables(RS).Rows(0)("得意先担当者名")
            TxtPosition.Text = ds1.Tables(RS).Rows(0)("得意先担当者役職")
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
                TxtRemarks.Text = ds1.Tables(RS).Rows(0)("備考")
            End If
            If ds1.Tables(RS).Rows(0)("ＶＡＴ") IsNot DBNull.Value Then
                TxtVat.Text = ds1.Tables(RS).Rows(0)("ＶＡＴ")
            End If

            '見積明細情報
            Dim Sql3 As String = ""
            Sql3 += "SELECT "
            Sql3 += "* "
            Sql3 += "FROM "
            Sql3 += "public"
            Sql3 += "."
            Sql3 += "t02_mitdt"
            Sql3 += " WHERE "
            Sql3 += "見積番号"
            Sql3 += " ILIKE "
            Sql3 += "'%"
            Sql3 += EditNo.ToString
            Sql3 += "%'"
            Sql3 += " AND "
            Sql3 += "見積番号枝番"
            Sql3 += " ILIKE "
            Sql3 += "'%"
            Sql3 += EditSuffix.ToString
            Sql3 += "%'"

            Dim ds3 = _db.selectDB(Sql3, RS, reccnt)

            For index As Integer = 0 To ds3.Tables(RS).Rows.Count - 1
                DgvItemList.Rows.Add()
                Dim tmp As Integer = ds3.Tables(RS).Rows(index)("仕入区分")
                DgvItemList(1, index).Value = tmp
                DgvItemList.Rows(index).Cells("メーカー").Value = ds3.Tables(RS).Rows(index)("メーカー")
                DgvItemList.Rows(index).Cells("品名").Value = ds3.Tables(RS).Rows(index)("品名")
                DgvItemList.Rows(index).Cells("型式").Value = ds3.Tables(RS).Rows(index)("型式")
                DgvItemList.Rows(index).Cells("数量").Value = ds3.Tables(RS).Rows(index)("数量")
                DgvItemList.Rows(index).Cells("単位").Value = ds3.Tables(RS).Rows(index)("単位")
                DgvItemList.Rows(index).Cells("仕入先").Value = ds3.Tables(RS).Rows(index)("仕入先名称")
                DgvItemList.Rows(index).Cells("仕入単価").Value = ds3.Tables(RS).Rows(index)("仕入単価")
                DgvItemList.Rows(index).Cells("仕入原価").Value = ds3.Tables(RS).Rows(index)("仕入原価")
                DgvItemList.Rows(index).Cells("関税率").Value = ds3.Tables(RS).Rows(index)("関税率")
                DgvItemList.Rows(index).Cells("関税額").Value = ds3.Tables(RS).Rows(index)("関税額")
                DgvItemList.Rows(index).Cells("前払法人税率").Value = ds3.Tables(RS).Rows(index)("前払法人税率")
                DgvItemList.Rows(index).Cells("前払法人税額").Value = ds3.Tables(RS).Rows(index)("前払法人税額")
                DgvItemList.Rows(index).Cells("輸送費率").Value = ds3.Tables(RS).Rows(index)("輸送費率")
                DgvItemList.Rows(index).Cells("輸送費額").Value = ds3.Tables(RS).Rows(index)("輸送費額")
                DgvItemList.Rows(index).Cells("仕入金額").Value = ds3.Tables(RS).Rows(index)("仕入金額")
                DgvItemList.Rows(index).Cells("売単価").Value = ds3.Tables(RS).Rows(index)("売単価")
                DgvItemList.Rows(index).Cells("売上金額").Value = ds3.Tables(RS).Rows(index)("売上金額")
                DgvItemList.Rows(index).Cells("粗利額").Value = ds3.Tables(RS).Rows(index)("粗利額")
                DgvItemList.Rows(index).Cells("粗利率").Value = ds3.Tables(RS).Rows(index)("粗利率")
                DgvItemList.Rows(index).Cells("見積単価").Value = ds3.Tables(RS).Rows(index)("見積単価")
                DgvItemList.Rows(index).Cells("見積金額").Value = ds3.Tables(RS).Rows(index)("見積金額")
                DgvItemList.Rows(index).Cells("リードタイム").Value = ds3.Tables(RS).Rows(index)("リードタイム")
                If ds3.Tables(RS).Rows(index)("リードタイム単位") Is DBNull.Value Then
                Else
                    Dim tmp2 As Integer = ds3.Tables(RS).Rows(index)("リードタイム単位")
                    DgvItemList("リードタイム単位", index).Value = tmp2
                End If

                DgvItemList.Rows(index).Cells("備考").Value = ds3.Tables(RS).Rows(index)("備考")
                DgvItemList.Rows(index).Cells("ステータス").Value = "EDIT"
            Next

            '金額計算
            Dim Total As Integer = 0
            Dim QuoteTotal As Integer = 0
            Dim PurchaseTotal As Integer = 0
            Dim GrossProfit As Decimal = 0

            For index As Integer = 0 To DgvItemList.Rows.Count - 1
                PurchaseTotal += DgvItemList.Rows(index).Cells("仕入金額").Value
                Total += DgvItemList.Rows(index).Cells("売上金額").Value
                QuoteTotal += DgvItemList.Rows(index).Cells("見積金額").Value
            Next

            TxtPurchaseTotal.Text = PurchaseTotal
            TxtTotal.Text = Total
            TxtQuoteTotal.Text = QuoteTotal
            TxtGrossProfit.Text = Total - PurchaseTotal

            '行番号の振り直し
            Dim i As Integer = DgvItemList.Rows.Count()
            Dim No As Integer = 1
            For c As Integer = 0 To i - 1
                DgvItemList.Rows(c).Cells("No").Value = No
                No += 1
            Next c
            TxtItemCount.Text = DgvItemList.Rows.Count()

        Else    '見積新規追加
            Dim dtNow As DateTime = DateTime.Now
            ' 指定した書式で日付を文字列に変換する
            Dim QuoteDate As String = dtNow.ToString("MMdd")

            TxtSuffixNo.Text = 1
            Dim Sql As String = ""
            Sql += "SELECT "
            Sql += "会社コード, "
            Sql += "採番キー, "
            Sql += "最新値, "
            Sql += "最小値, "
            Sql += "最大値, "
            Sql += "接頭文字, "
            Sql += "連番桁数 "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "m80_saiban"
            Sql += " WHERE "
            Sql += "採番キー"
            Sql += " ILIKE "
            Sql += "'10'"

            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            TxtQuoteNo.Text += ds.Tables(RS).Rows(0)(5)
            TxtQuoteNo.Text += QuoteDate
            CompanyCode = ds.Tables(RS).Rows(0)(0)
            KeyNo = ds.Tables(RS).Rows(0)(1)
            QuoteNo = ds.Tables(RS).Rows(0)(2)
            QuoteNoMin = ds.Tables(RS).Rows(0)(3)
            QuoteNoMax = ds.Tables(RS).Rows(0)(4)
            TxtQuoteNo.Text += QuoteNo.PadLeft(ds.Tables(RS).Rows(0)(6), "0")

            TxtInput.Text = Input

            SaibanSave()
        End If

        If Status Is "VIEW" Then
            If frmC01F10_Login.loginValue.Language = "ENG" Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If

            DtpQuote.Enabled = False
            DtpExpiration.Enabled = False
            TxtCustomerCode.Enabled = False
            TxtCustomerName.Enabled = False
            TxtPostalCode.Enabled = False
            TxtAddress1.Enabled = False
            TxtAddress2.Enabled = False
            TxtAddress3.Enabled = False
            TxtTel.Enabled = False
            TxtFax.Enabled = False
            TxtPerson.Enabled = False
            TxtPosition.Enabled = False
            TxtSales.Enabled = False
            TxtPaymentTerms.Enabled = False
            TxtRemarks.Enabled = False
            DgvItemList.ReadOnly = True
            BtnRegistration.Visible = False
            BtnRowsAdd.Visible = False
            BtnRowsDel.Visible = False
            BtnUp.Visible = False
            BtnDown.Visible = False
            BtnClone.Visible = False
            BtnInsert.Visible = False
            BtnProof.Visible = True
            BtnProof.Location = New Point(828, 509)
            BtnCodeSearch.Enabled = False

            Dim RequestFlg As Boolean = False
            For i As Integer = 0 To DgvItemList.Rows.Count() - 1
                If DgvItemList.Rows(i).Cells("仕入区分").Value = 1 And DgvItemList.Rows(i).Cells("仕入単価").Value = 0 Then
                    RequestFlg = True
                End If
            Next

            If RequestFlg Then
                BtnQuoteRequest.Visible = True
                BtnQuoteRequest.Location = New Point(1004, 509)
            Else
                BtnQuote.Visible = True
                BtnQuote.Location = New Point(1004, 509)
            End If

        ElseIf Status Is "PRICE" Then
            If frmC01F10_Login.loginValue.Language = "ENG" Then
                LblMode.Text = "PurchasePriceInputMode"
            Else
                LblMode.Text = "仕入単価入力モード"
            End If

            BtnRowsAdd.Visible = False
            BtnRowsDel.Visible = False
            BtnUp.Visible = False
            BtnDown.Visible = False
            BtnClone.Visible = False
            BtnInsert.Visible = False
            BtnQuote.Visible = False
        ElseIf Status Is "EDIT" Then
            If frmC01F10_Login.loginValue.Language = "ENG" Then
                LblMode.Text = "EditMode"
            Else
                LblMode.Text = "編集モード"
            End If

        ElseIf Status Is "ADD" Then
            If frmC01F10_Login.loginValue.Language = "ENG" Then
                LblMode.Text = "NewRegistrationMode"
            Else
                LblMode.Text = "新規登録モード"
            End If
        End If


        LoadFlg = True

    End Sub

    Private Sub SaibanSave()
        Dim dtToday As DateTime = DateTime.Now

        If QuoteNo = QuoteNoMax Then
            QuoteNo = QuoteNoMin
        Else
            QuoteNo = QuoteNo + 1
        End If
        Dim Sql As String = ""

        Sql = ""
        Sql += "UPDATE "
        Sql += "Public."
        Sql += "m80_saiban "
        Sql += "SET "
        Sql += " 最新値"
        Sql += " = '"
        Sql += QuoteNo.ToString
        Sql += "', "
        Sql += "更新者"
        Sql += " = '"
        Sql += Input
        Sql += "', "
        Sql += "更新日"
        Sql += " = '"
        Sql += dtToday
        Sql += "' "
        Sql += "WHERE"
        Sql += " 会社コード"
        Sql += "='"
        Sql += CompanyCode.ToString
        Sql += "'"
        Sql += " AND"
        Sql += " 採番キー"
        Sql += "='"
        Sql += KeyNo.ToString
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
    End Sub

    '金額自動計算
    Private Sub CellValueChanged(ByVal sender As Object,
    ByVal e As DataGridViewCellEventArgs) _
    Handles DgvItemList.CellValueChanged
        If LoadFlg Then
            TxtPurchaseTotal.Clear()
            TxtTotal.Clear()
            TxtQuoteTotal.Clear()
            TxtGrossProfit.Clear()

            Dim Total As Integer = 0
            Dim PurchaseTotal As Integer = 0
            Dim QuoteTotal As Integer = 0
            Dim GrossProfit As Decimal = 0
            Dim tmpPurchase As Integer = 0
            Dim tmp As Decimal = 0
            Dim tmp1 As Decimal = 0
            Dim tmp2 As Decimal = 0
            Dim tmp3 As Decimal = 0
            Dim tmp4 As Decimal = 0
            Dim Sql As String = ""

            If e.RowIndex > -1 Then
                If RbtnUP.Checked Or RbtnGP.Checked Then
                    If DgvItemList.Rows(e.RowIndex).Cells("数量").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value IsNot Nothing Then
                        DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value = DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value * DgvItemList.Rows(e.RowIndex).Cells("数量").Value
                    End If
                    If DgvItemList.Rows(e.RowIndex).Cells("関税率").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value IsNot Nothing Then
                        DgvItemList.Rows(e.RowIndex).Cells("関税額").Value = DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value * DgvItemList.Rows(e.RowIndex).Cells("関税率").Value
                    End If
                    If DgvItemList.Rows(e.RowIndex).Cells("前払法人税率").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("関税額").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value IsNot Nothing Then
                        tmp = DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value + DgvItemList.Rows(e.RowIndex).Cells("関税額").Value
                        tmp1 = tmp * DgvItemList.Rows(e.RowIndex).Cells("前払法人税率").Value
                        tmp1 = Math.Ceiling(tmp1)
                        DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value = tmp1
                    End If
                    If DgvItemList.Rows(e.RowIndex).Cells("輸送費率").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value IsNot Nothing Then
                        DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value = DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value * DgvItemList.Rows(e.RowIndex).Cells("輸送費率").Value
                    End If
                    If DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("関税額").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value IsNot Nothing Then
                        DgvItemList.Rows(e.RowIndex).Cells("仕入金額").Value = DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value + DgvItemList.Rows(e.RowIndex).Cells("関税額").Value + DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value + DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value
                    End If
                    If DgvItemList.Rows(e.RowIndex).Cells("売単価").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("関税額").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value IsNot Nothing Then
                        DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value = DgvItemList.Rows(e.RowIndex).Cells("売単価").Value + DgvItemList.Rows(e.RowIndex).Cells("関税額").Value + DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value + DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value
                    End If
                    If DgvItemList.Rows(e.RowIndex).Cells("数量").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value IsNot Nothing Then
                        DgvItemList.Rows(e.RowIndex).Cells("見積金額").Value = DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value * DgvItemList.Rows(e.RowIndex).Cells("数量").Value
                    End If
                End If

                If RbtnUP.Checked Then
                    If DgvItemList.Rows(e.RowIndex).Cells("数量").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("売単価").Value IsNot Nothing Then
                        DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value = DgvItemList.Rows(e.RowIndex).Cells("数量").Value * DgvItemList.Rows(e.RowIndex).Cells("売単価").Value
                        If DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value IsNot Nothing Then
                            DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value = DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value - DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value
                            DgvItemList.Rows(e.RowIndex).Cells("粗利率").Value = Format(DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value / DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value * 100, "0.0")
                        End If
                    End If
                ElseIf RbtnGP.Checked Then
                    If DgvItemList.Rows(e.RowIndex).Cells("数量").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("粗利率").Value IsNot Nothing Then
                        tmp2 = DgvItemList.Rows(e.RowIndex).Cells("粗利率").Value / 100
                        tmp3 = DgvItemList.Rows(e.RowIndex).Cells("数量").Value - tmp2 * DgvItemList.Rows(e.RowIndex).Cells("数量").Value
                        If DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value IsNot Nothing Then
                            DgvItemList.Rows(e.RowIndex).Cells("売単価").Value = DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value / tmp3
                            DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value = DgvItemList.Rows(e.RowIndex).Cells("売単価").Value * DgvItemList.Rows(e.RowIndex).Cells("数量").Value
                            DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value = DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value - DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value
                        End If
                    End If
                Else
                    If DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("売単価").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("関税額").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value IsNot Nothing Then
                        tmp4 = DgvItemList.Rows(e.RowIndex).Cells("関税額").Value + DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value + DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value
                        DgvItemList.Rows(e.RowIndex).Cells("売単価").Value = DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value - tmp4
                    End If
                End If

                If DgvItemList.Rows(e.RowIndex).Cells("仕入先コード").Value IsNot Nothing Then
                    Sql = ""
                    Sql += "SELECT "
                    Sql += "* "
                    Sql += "FROM "
                    Sql += "public"
                    Sql += "."
                    Sql += "m11_supplier"
                    Sql += " WHERE "
                    Sql += "仕入先コード"
                    Sql += " ILIKE "
                    Sql += "'"
                    Sql += DgvItemList.Rows(e.RowIndex).Cells("仕入先コード").Value.ToString
                    Sql += "'"

                    Dim reccnt As Integer = 0
                    Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

                    DgvItemList.Rows(e.RowIndex).Cells("仕入先").Value = ds.Tables(RS).Rows(0)("仕入先名").ToString
                    DgvItemList.Rows(e.RowIndex).Cells("間接費率").Value = ds.Tables(RS).Rows(0)("既定間接費率").ToString
                    DgvItemList.Rows(e.RowIndex).Cells("関税率").Value = ds.Tables(RS).Rows(0)("関税率").ToString
                    DgvItemList.Rows(e.RowIndex).Cells("前払法人税率").Value = ds.Tables(RS).Rows(0)("前払法人税率").ToString
                    DgvItemList.Rows(e.RowIndex).Cells("輸送費率").Value = ds.Tables(RS).Rows(0)("輸送費率").ToString
                End If

            End If

            For index As Integer = 0 To DgvItemList.Rows.Count - 1
                PurchaseTotal += DgvItemList.Rows(index).Cells("仕入金額").Value
                Total += DgvItemList.Rows(index).Cells("売上金額").Value
                QuoteTotal += DgvItemList.Rows(index).Cells("見積金額").Value
            Next
            TxtPurchaseTotal.Text = PurchaseTotal
            TxtTotal.Text = Total
            TxtQuoteTotal.Text = QuoteTotal
            TxtGrossProfit.Text = Total - PurchaseTotal
        End If
    End Sub

    Private Sub RbtnGP_CheckedChanged(sender As Object, e As EventArgs) Handles RbtnGP.CheckedChanged
        If RbtnUP.Checked Then
            DgvItemList.Columns("粗利率").ReadOnly = True
            DgvItemList.Columns("売単価").ReadOnly = False
            DgvItemList.Columns("見積単価").ReadOnly = True
            DgvItemList.Columns("粗利率").DefaultCellStyle.BackColor = Color.Gray
            DgvItemList.Columns("売単価").DefaultCellStyle.BackColor = Color.White
            DgvItemList.Columns("見積単価").DefaultCellStyle.BackColor = Color.Gray
        ElseIf RbtnGP.Checked Then
            DgvItemList.Columns("売単価").ReadOnly = True
            DgvItemList.Columns("粗利率").ReadOnly = False
            DgvItemList.Columns("見積単価").ReadOnly = True
            DgvItemList.Columns("売単価").DefaultCellStyle.BackColor = Color.Gray
            DgvItemList.Columns("粗利率").DefaultCellStyle.BackColor = Color.White
            DgvItemList.Columns("見積単価").DefaultCellStyle.BackColor = Color.Gray
        Else
            DgvItemList.Columns("売単価").ReadOnly = True
            DgvItemList.Columns("粗利率").ReadOnly = True
            DgvItemList.Columns("見積単価").ReadOnly = False
            DgvItemList.Columns("売単価").DefaultCellStyle.BackColor = Color.Gray
            DgvItemList.Columns("粗利率").DefaultCellStyle.BackColor = Color.Gray
            DgvItemList.Columns("見積単価").DefaultCellStyle.BackColor = Color.White

        End If

    End Sub

    '任意の場所に行を挿入
    Private Sub BtnInsert_Click(sender As Object, e As EventArgs) Handles BtnInsert.Click
        If DgvItemList.Rows.Count > 0 Then
            Dim RowIdx As Integer = DgvItemList.CurrentCell.RowIndex
            '行を挿入
            DgvItemList.Rows.Insert(RowIdx + 1)
            DgvItemList.Rows(RowIdx + 1).Cells("仕入区分").Value = 1
            DgvItemList.Rows(RowIdx + 1).Cells("リードタイム単位").Value = 1
            DgvItemList.Rows(RowIdx + 1).Cells("ステータス").Value = "ADD"
            '最終行のインデックスを取得
            Dim index As Integer = DgvItemList.Rows.Count()
            '行番号の振り直し
            Dim No As Integer = 1
            For c As Integer = 0 To index - 1
                DgvItemList.Rows(c).Cells("No").Value = No
                No += 1
            Next c
            TxtItemCount.Text = DgvItemList.Rows.Count()
        Else
            DgvItemList.Rows.Add()
            DgvItemList.Rows(0).Cells("仕入区分").Value = 1
            DgvItemList.Rows(0).Cells("リードタイム単位").Value = 1
            TxtItemCount.Text = DgvItemList.Rows.Count()
            DgvItemList.Rows(DgvItemList.Rows.Count() - 1).Cells("ステータス").Value = "ADD"
            '行番号の振り直し
            Dim index As Integer = DgvItemList.Rows.Count()
            Dim No As Integer = 1
            For c As Integer = 0 To index - 1
                DgvItemList.Rows(c).Cells("No").Value = No
                No += 1
            Next c
            TxtItemCount.Text = DgvItemList.Rows.Count()
        End If
    End Sub

    '行追加（DGVの最終行に追加）
    Private Sub BtnRowsAdd_Click(sender As Object, e As EventArgs) Handles BtnRowsAdd.Click
        DgvItemList.Rows.Add()
        DgvItemList.Rows(DgvItemList.Rows.Count() - 1).Cells("仕入区分").Value = 1
        DgvItemList.Rows(DgvItemList.Rows.Count() - 1).Cells("リードタイム単位").Value = 1
        DgvItemList.Rows(DgvItemList.Rows.Count() - 1).Cells("ステータス").Value = "ADD"
        '行番号の振り直し
        Dim index As Integer = DgvItemList.Rows.Count()
        Dim No As Integer = 1
        For c As Integer = 0 To index - 1
            DgvItemList.Rows(c).Cells("No").Value = No
            No += 1
        Next c
        TxtItemCount.Text = DgvItemList.Rows.Count()
    End Sub

    '選択行の削除（削除時に金額の再計算、Noの再採番）
    Private Sub BtnRowsDel_Click(sender As Object, e As EventArgs) Handles BtnRowsDel.Click
        LoadFlg = False

        For Each r As DataGridViewCell In DgvItemList.SelectedCells
            DgvItemList.Rows.RemoveAt(r.RowIndex)
        Next r

        TxtPurchaseTotal.Clear()
        TxtTotal.Clear()
        TxtQuoteTotal.Clear()
        TxtGrossProfit.Clear()

        Dim Total As Integer = 0
        Dim QuoteTotal As Integer = 0
        Dim PurchaseTotal As Integer = 0
        Dim GrossProfit As Decimal = 0

        For c As Integer = 0 To DgvItemList.Rows.Count - 1
            PurchaseTotal += DgvItemList.Rows(c).Cells("仕入金額").Value
            Total += DgvItemList.Rows(c).Cells("売上金額").Value
            QuoteTotal += DgvItemList.Rows(c).Cells("見積金額").Value
        Next
        TxtPurchaseTotal.Text = PurchaseTotal
        TxtTotal.Text = Total
        TxtQuoteTotal.Text = QuoteTotal
        TxtGrossProfit.Text = Total - PurchaseTotal

        '行番号の振り直し
        Dim index As Integer = DgvItemList.Rows.Count()
        Dim No As Integer = 1
        For c As Integer = 0 To index - 1
            DgvItemList.Rows(c).Cells("No").Value = No
            No += 1
        Next c
        TxtItemCount.Text = DgvItemList.Rows.Count()
        LoadFlg = True
    End Sub

    '行の複写（選択行の直下に複写）
    Private Sub BtnClone_Click(sender As Object, e As EventArgs) Handles BtnClone.Click
        Try
            'メニュー選択処理
            Dim RowIdx As Integer
            Dim Item(27) As String

            '一覧選択行インデックスの取得

            RowIdx = DgvItemList.CurrentCell.RowIndex


            '選択行の値を格納
            For c As Integer = 0 To 27
                Item(c) = DgvItemList.Rows(RowIdx).Cells(c).Value
            Next c

            '行を挿入
            DgvItemList.Rows.Insert(RowIdx + 1)
            DgvItemList.Rows(RowIdx + 1).Cells("ステータス").Value = "ADD"
            '追加した行に複製元の値を格納
            For c As Integer = 0 To 27
                If c = 1 Then
                    If Item(c) IsNot Nothing Then
                        Dim tmp As Integer = Item(c)
                        DgvItemList(1, RowIdx + 1).Value = tmp
                    End If
                ElseIf c = 25 Then
                    If Item(c) IsNot Nothing Then
                        Dim tmp As Integer = Item(c)
                        DgvItemList(25, RowIdx + 1).Value = tmp
                    End If
                Else
                    DgvItemList.Rows(RowIdx + 1).Cells(c).Value = Item(c)
                End If

            Next c

            '最終行のインデックスを取得
            Dim index As Integer = DgvItemList.Rows.Count()
            '行番号の振り直し
            Dim No As Integer = 1
            For c As Integer = 0 To index - 1
                DgvItemList.Rows(c).Cells("No").Value = No
                No += 1
            Next c
            TxtItemCount.Text = DgvItemList.Rows.Count()
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '得意先コードをダブルクリック時得意先マスタから得意先を取得
    Private Sub TxtCustomerCode_DoubleClick(sender As Object, e As EventArgs) Handles TxtCustomerCode.DoubleClick
        Dim openForm As Form = Nothing
        openForm = New CustomerSearch(_msgHd, _db, _langHd, Me)   '処理選択
        openForm.Show(Me)
        Me.Enabled = False
    End Sub

    Private Sub TxtSales_DoubleClick(sender As Object, e As EventArgs) Handles TxtSales.DoubleClick
        Dim openForm As Form = Nothing
        openForm = New SalesSearch(_msgHd, _db, _langHd, Me)   '処理選択
        openForm.Show(Me)
        Me.Enabled = False
    End Sub

    'Dgv内での検索
    Private Sub DgvItemList_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) _
     Handles DgvItemList.CellDoubleClick

        Dim ColIdx As Integer
        ColIdx = DgvItemList.CurrentCell.ColumnIndex
        Dim RowIdx As Integer
        RowIdx = DgvItemList.CurrentCell.RowIndex

        Dim Maker As String = DgvItemList.Rows(RowIdx).Cells("メーカー").Value
        Dim Item As String = DgvItemList.Rows(RowIdx).Cells("品名").Value
        Dim Model As String = DgvItemList.Rows(RowIdx).Cells("型式").Value

        If ColIdx = 2 Then                  'メーカー検索
            Dim openForm As Form = Nothing
            openForm = New MakerSearch(_msgHd, _db, Me, RowIdx, ColIdx, Maker, Item, Model)   '処理選択
            openForm.Show(Me)
            Me.Enabled = False
        End If

        If ColIdx = 3 Then              '品名検索
            If Maker IsNot Nothing Then
                Dim openForm As Form = Nothing
                openForm = New MakerSearch(_msgHd, _db, Me, RowIdx, ColIdx, Maker, Item, Model)   '処理選択
                openForm.Show(Me)
                Me.Enabled = False
            Else
                If frmC01F10_Login.loginValue.Language = "ENG" Then
                    MessageBox.Show("Please enter the maker.",
                "エラー",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
                Else
                    MessageBox.Show("メーカーを入力してください。",
                "エラー",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
                End If

            End If
        End If

        If ColIdx = 4 Then
            If Maker IsNot Nothing And Item IsNot Nothing Then
                Dim openForm As Form = Nothing
                openForm = New MakerSearch(_msgHd, _db, Me, RowIdx, ColIdx, Maker, Item, Model)
                openForm.Show(Me)
                Me.Enabled = False
            Else
                If frmC01F10_Login.loginValue.Language = "ENG" Then
                    MessageBox.Show("Please enter the maker and item.",
                "エラー",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
                Else
                    MessageBox.Show("メーカー、品名を入力してください。",
                "エラー",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
                End If
            End If
        End If

        If ColIdx = 8 Then
            Dim openForm As Form = Nothing
            openForm = New SupplierSearch(_msgHd, _db, _langHd, RowIdx, Me)
            openForm.Show(Me)
            Me.Enabled = False
        End If

        If ColIdx = 26 Then
            Dim openForm As Form = Nothing
            openForm = New RemarksInput(_msgHd, _db, _langHd, RowIdx, Me)
            openForm.Show(Me)
            Me.Enabled = False
        End If
    End Sub

    Private Sub BtnUp_Click(sender As Object, e As EventArgs) Handles BtnUp.Click
        If DgvItemList.CurrentCell.RowIndex > 0 Then
            DgvItemList.CurrentCell = DgvItemList(DgvItemList.CurrentCell.ColumnIndex, DgvItemList.CurrentCell.RowIndex - 1)
        End If
    End Sub

    Private Sub BtnDown_Click(sender As Object, e As EventArgs) Handles BtnDown.Click
        If DgvItemList.CurrentCell.RowIndex + 1 < DgvItemList.Rows.Count Then
            DgvItemList.CurrentCell = DgvItemList(DgvItemList.CurrentCell.ColumnIndex, DgvItemList.CurrentCell.RowIndex + 1)
        End If
    End Sub

    '前の画面に戻る
    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Show()
        _parentForm.Enabled = True
        Me.Dispose()
    End Sub

    Private Sub BtnRegistration_Click(sender As Object, e As EventArgs) Handles BtnRegistration.Click
        Dim dtToday As DateTime = DateTime.Now
        If Status Is "PRICE" Then
            Try
                Dim Sql1 As String = ""
                Sql1 = ""
                Sql1 += "UPDATE "
                Sql1 += "Public."
                Sql1 += "t01_mithd "
                Sql1 += "SET "

                Sql1 += "得意先コード"
                Sql1 += " = '"
                Sql1 += TxtCustomerCode.Text
                Sql1 += "', "
                Sql1 += "得意先名"
                Sql1 += " = '"
                Sql1 += TxtCustomerName.Text
                Sql1 += "', "
                Sql1 += "得意先郵便番号"
                Sql1 += " = '"
                Sql1 += TxtPostalCode.Text
                Sql1 += "', "
                Sql1 += "得意先住所"
                Sql1 += " = '"
                Sql1 += TxtAddress1.Text
                Sql1 += " "
                Sql1 += TxtAddress2.Text
                Sql1 += " "
                Sql1 += TxtAddress3.Text
                Sql1 += "', "
                Sql1 += "得意先電話番号"
                Sql1 += " = '"
                Sql1 += TxtTel.Text
                Sql1 += "', "
                Sql1 += "得意先ＦＡＸ"
                Sql1 += " = '"
                Sql1 += TxtFax.Text
                Sql1 += "', "
                Sql1 += "得意先担当者役職"
                Sql1 += " = '"
                Sql1 += TxtPosition.Text
                Sql1 += "', "
                Sql1 += "得意先担当者名"
                Sql1 += " = '"
                Sql1 += TxtPerson.Text
                Sql1 += "', "
                Sql1 += "見積日"
                Sql1 += " = '"
                Sql1 += DtpQuote.Text
                Sql1 += "', "
                Sql1 += "見積有効期限"
                Sql1 += " = '"
                Sql1 += DtpExpiration.Text
                Sql1 += "', "
                Sql1 += "支払条件"
                Sql1 += " = '"
                Sql1 += TxtPaymentTerms.Text
                Sql1 += "', "
                Sql1 += "見積金額"
                Sql1 += " = '"
                Sql1 += TxtTotal.Text
                Sql1 += "', "
                Sql1 += "仕入金額"
                Sql1 += " = '"
                Sql1 += TxtPurchaseTotal.Text
                Sql1 += "', "
                Sql1 += "粗利額"
                Sql1 += " = '"
                Sql1 += TxtGrossProfit.Text
                Sql1 += "', "
                Sql1 += "営業担当者"
                Sql1 += " = '"
                Sql1 += TxtSales.Text
                Sql1 += "', "
                Sql1 += "入力担当者"
                Sql1 += " = '"
                Sql1 += TxtInput.Text
                Sql1 += "', "
                Sql1 += "備考"
                Sql1 += " = '"
                Sql1 += TxtRemarks.Text
                Sql1 += "', "
                Sql1 += "ＶＡＴ"
                Sql1 += " = '"
                Sql1 += TxtVat.Text
                Sql1 += "', "
                Sql1 += "登録日"
                Sql1 += " = '"
                Sql1 += DtpRegistration.Text
                Sql1 += "', "
                Sql1 += "更新日"
                Sql1 += " = '"
                Sql1 += dtToday
                Sql1 += "', "
                Sql1 += "更新者"
                Sql1 += " = '"
                Sql1 += Input
                Sql1 += "' "
                Sql1 += "WHERE"
                Sql1 += " 会社コード"
                Sql1 += "='"
                Sql1 += CompanyCode
                Sql1 += "'"
                Sql1 += " AND"
                Sql1 += " 見積番号"
                Sql1 += "='"
                Sql1 += TxtQuoteNo.Text
                Sql1 += "' "
                Sql1 += " AND"
                Sql1 += " 見積番号枝番"
                Sql1 += "='"
                Sql1 += TxtSuffixNo.Text
                Sql1 += "' "
                Sql1 += "RETURNING 会社コード"
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
                Sql1 += "営業担当者"
                Sql1 += ", "
                Sql1 += "入力担当者"
                Sql1 += ", "
                Sql1 += "備考"
                Sql1 += ", "
                Sql1 += "ＶＡＴ"
                Sql1 += ", "
                Sql1 += "登録日"
                Sql1 += ", "
                Sql1 += "更新日"
                Sql1 += ", "
                Sql1 += "更新者"
                _db.executeDB(Sql1)

                Dim Sql2 As String = ""
                For index As Integer = 0 To DgvItemList.Rows.Count - 1
                    Sql2 = ""
                    Sql2 += "UPDATE "
                    Sql2 += "Public."
                    Sql2 += "t02_mitdt "
                    Sql2 += "SET "

                    Sql2 += "仕入区分"
                    Sql2 += " = '"
                    Sql2 += DgvItemList.Rows(index).Cells("仕入区分").Value.ToString
                    Sql2 += "', "
                    Sql2 += "メーカー"
                    Sql2 += " = '"
                    Sql2 += DgvItemList.Rows(index).Cells("メーカー").Value.ToString
                    Sql2 += "', "
                    Sql2 += "品名"
                    Sql2 += " = '"
                    Sql2 += DgvItemList.Rows(index).Cells("品名").Value.ToString
                    Sql2 += "', "
                    Sql2 += "型式"
                    Sql2 += " = '"
                    Sql2 += DgvItemList.Rows(index).Cells("型式").Value.ToString
                    Sql2 += "', "

                    If DgvItemList.Rows(index).Cells("数量").Value IsNot Nothing Then
                        Sql2 += "数量"
                        Sql2 += " = '"
                        Sql2 += DgvItemList.Rows(index).Cells("数量").Value.ToString
                        Sql2 += "', "
                    Else
                        Sql2 += "数量"
                        Sql2 += " = '"
                        Sql2 += "0"
                        Sql2 += "', "
                    End If
                    Sql2 += "単位"
                    Sql2 += " = '"
                    Sql2 += DgvItemList.Rows(index).Cells("単位").Value.ToString
                    Sql2 += "', "
                    Sql2 += "仕入先名称"
                    Sql2 += " = '"
                    Sql2 += DgvItemList.Rows(index).Cells("仕入先").Value.ToString
                    Sql2 += "', "
                    If DgvItemList.Rows(index).Cells("仕入単価").Value IsNot Nothing Then
                        Sql2 += "仕入単価"
                        Sql2 += " = '"
                        Sql2 += DgvItemList.Rows(index).Cells("仕入単価").Value.ToString
                        Sql2 += "', "
                    Else
                        Sql2 += "仕入単価"
                        Sql2 += " = '"
                        Sql2 += "0"
                        Sql2 += "', "
                    End If

                    If DgvItemList.Rows(index).Cells("仕入金額").Value IsNot Nothing Then
                        Sql2 += "仕入金額"
                        Sql2 += " = '"
                        Sql2 += DgvItemList.Rows(index).Cells("仕入金額").Value.ToString
                        Sql2 += "', "
                    Else
                        Sql2 += "仕入金額"
                        Sql2 += " = '"
                        Sql2 += "0"
                        Sql2 += "', "
                    End If
                    If DgvItemList.Rows(index).Cells("仕入原価").Value IsNot Nothing Then
                        Sql2 += "仕入原価"
                        Sql2 += " = '"
                        Sql2 += DgvItemList.Rows(index).Cells("仕入原価").Value.ToString
                        Sql2 += "', "
                    Else
                        Sql2 += "仕入原価"
                        Sql2 += " = '"
                        Sql2 += "0"
                        Sql2 += "', "
                    End If
                    If DgvItemList.Rows(index).Cells("関税率").Value IsNot Nothing Then
                        Sql2 += "関税率"
                        Sql2 += " = '"
                        Sql2 += DgvItemList.Rows(index).Cells("関税率").Value.ToString
                        Sql2 += "', "
                    Else
                        Sql2 += "関税率"
                        Sql2 += " = '"
                        Sql2 += "0"
                        Sql2 += "', "
                    End If
                    If DgvItemList.Rows(index).Cells("関税額").Value IsNot Nothing Then
                        Sql2 += "関税額"
                        Sql2 += " = '"
                        Sql2 += DgvItemList.Rows(index).Cells("関税額").Value.ToString
                        Sql2 += "', "
                    Else
                        Sql2 += "関税額"
                        Sql2 += " = '"
                        Sql2 += "0"
                        Sql2 += "', "
                    End If
                    If DgvItemList.Rows(index).Cells("前払法人税率").Value IsNot Nothing Then
                        Sql2 += "前払法人税率"
                        Sql2 += " = '"
                        Sql2 += DgvItemList.Rows(index).Cells("前払法人税率").Value.ToString
                        Sql2 += "', "
                    Else
                        Sql2 += "前払法人税率"
                        Sql2 += " = '"
                        Sql2 += "0"
                        Sql2 += "', "
                    End If
                    If DgvItemList.Rows(index).Cells("前払法人税額").Value IsNot Nothing Then
                        Sql2 += "前払法人税額"
                        Sql2 += " = '"
                        Sql2 += DgvItemList.Rows(index).Cells("前払法人税額").Value.ToString
                        Sql2 += "', "
                    Else
                        Sql2 += "前払法人税額"
                        Sql2 += " = '"
                        Sql2 += "0"
                        Sql2 += "', "
                    End If
                    If DgvItemList.Rows(index).Cells("輸送費率").Value IsNot Nothing Then
                        Sql2 += "輸送費率"
                        Sql2 += " = '"
                        Sql2 += DgvItemList.Rows(index).Cells("輸送費率").Value.ToString
                        Sql2 += "', "
                    Else
                        Sql2 += "輸送費率"
                        Sql2 += " = '"
                        Sql2 += "0"
                        Sql2 += "', "
                    End If
                    If DgvItemList.Rows(index).Cells("輸送費額").Value IsNot Nothing Then
                        Sql2 += "輸送費額"
                        Sql2 += " = '"
                        Sql2 += DgvItemList.Rows(index).Cells("輸送費額").Value.ToString
                        Sql2 += "', "
                    Else
                        Sql2 += "輸送費額"
                        Sql2 += " = '"
                        Sql2 += "0"
                        Sql2 += "', "
                    End If
                    If DgvItemList.Rows(index).Cells("売単価").Value IsNot Nothing Then
                        Sql2 += "売単価"
                        Sql2 += " = '"
                        Sql2 += DgvItemList.Rows(index).Cells("売単価").Value.ToString
                        Sql2 += "', "
                    Else
                        Sql2 += "売単価"
                        Sql2 += " = '"
                        Sql2 += "0"
                        Sql2 += "', "
                    End If
                    If DgvItemList.Rows(index).Cells("売上金額").Value IsNot Nothing Then
                        Sql2 += "売上金額"
                        Sql2 += " = '"
                        Sql2 += DgvItemList.Rows(index).Cells("売上金額").Value.ToString
                        Sql2 += "', "
                    Else
                        Sql2 += "売上金額"
                        Sql2 += " = '"
                        Sql2 += "0"
                        Sql2 += "', "
                    End If

                    If DgvItemList.Rows(index).Cells("見積単価").Value IsNot Nothing Then
                        Sql2 += "見積単価"
                        Sql2 += " = '"
                        Sql2 += DgvItemList.Rows(index).Cells("見積単価").Value.ToString
                        Sql2 += "', "
                    Else
                        Sql2 += "見積単価"
                        Sql2 += " = '"
                        Sql2 += "0"
                        Sql2 += "', "
                    End If

                    If DgvItemList.Rows(index).Cells("見積金額").Value IsNot Nothing Then
                        Sql2 += "見積金額"
                        Sql2 += " = '"
                        Sql2 += DgvItemList.Rows(index).Cells("見積金額").Value.ToString
                        Sql2 += "', "
                    Else
                        Sql2 += "見積金額"
                        Sql2 += " = '"
                        Sql2 += "0"
                        Sql2 += "', "
                    End If

                    If DgvItemList.Rows(index).Cells("粗利額").Value IsNot Nothing Then
                        Sql2 += "粗利額"
                        Sql2 += " = '"
                        Sql2 += DgvItemList.Rows(index).Cells("粗利額").Value.ToString
                        Sql2 += "', "
                    Else
                        Sql2 += "粗利額"
                        Sql2 += " = '"
                        Sql2 += "0"
                        Sql2 += "', "
                    End If
                    If DgvItemList.Rows(index).Cells("粗利率").Value IsNot Nothing Then
                        Sql2 += "粗利率"
                        Sql2 += " = '"
                        Sql2 += DgvItemList.Rows(index).Cells("粗利率").Value.ToString
                        Sql2 += "', "
                    Else
                        Sql2 += "粗利率"
                        Sql2 += " = '"
                        Sql2 += "0"
                        Sql2 += "', "
                    End If
                    If DgvItemList.Rows(index).Cells("リードタイム").Value IsNot Nothing Then
                        Sql2 += "リードタイム"
                        Sql2 += " = '"
                        Sql2 += DgvItemList.Rows(index).Cells("リードタイム").Value.ToString
                        Sql2 += "', "
                    Else
                        Sql2 += "リードタイム"
                        Sql2 += " = '"
                        Sql2 += "0"
                        Sql2 += "', "
                    End If

                    Sql2 += "リードタイム単位"
                    Sql2 += " = '"
                    Sql2 += DgvItemList.Rows(index).Cells("リードタイム単位").Value.ToString
                    Sql2 += "', "
                    Sql2 += "備考"
                    Sql2 += " = '"
                    Sql2 += DgvItemList.Rows(index).Cells("備考").Value.ToString
                    Sql2 += "', "
                    Sql2 += "更新者"
                    Sql2 += " = '"
                    Sql2 += Input
                    Sql2 += "', "
                    Sql2 += "登録日"
                    Sql2 += " = '"
                    Sql2 += DtpRegistration.Text
                    Sql2 += "' "
                    Sql2 += "WHERE"
                    Sql2 += " 会社コード"
                    Sql2 += "='"
                    Sql2 += CompanyCode
                    Sql2 += "'"
                    Sql2 += " AND"
                    Sql2 += " 見積番号"
                    Sql2 += "='"
                    Sql2 += TxtQuoteNo.Text
                    Sql2 += "' "
                    Sql2 += " AND"
                    Sql2 += " 見積番号枝番"
                    Sql2 += "='"
                    Sql2 += TxtSuffixNo.Text
                    Sql2 += "' "
                    Sql2 += " AND"
                    Sql2 += " 行番号"
                    Sql2 += "='"
                    Sql2 += DgvItemList.Rows(index).Cells("No").Value.ToString
                    Sql2 += "' "
                    Sql2 += "RETURNING 会社コード"
                    Sql2 += ", "
                    Sql2 += "見積番号"
                    Sql2 += ", "
                    Sql2 += "見積番号枝番"
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
                    Sql2 += "数量"
                    Sql2 += ", "
                    Sql2 += "単位"
                    Sql2 += ", "
                    Sql2 += "仕入先名称"
                    Sql2 += ", "
                    Sql2 += "仕入単価"
                    Sql2 += ", "
                    Sql2 += "仕入原価"
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
                    Sql2 += "仕入金額"
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
                    Sql2 += "リードタイム"
                    Sql2 += ", "
                    Sql2 += "リードタイム単位"
                    Sql2 += ", "
                    Sql2 += "備考"
                    Sql2 += ", "
                    Sql2 += "更新者"
                    Sql2 += ", "
                    Sql2 += "登録日"

                    _db.executeDB(Sql2)
                Next
            Catch ue As UsrDefException
                ue.dspMsg()
                Throw ue
            Catch ex As Exception
                'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
            End Try
        Else
            Try
                Dim Sql1 As String = ""
                Sql1 = ""
                Sql1 += "INSERT INTO "
                Sql1 += "Public."
                Sql1 += "t01_mithd("
                Sql1 += "会社コード, 見積番号, 見積番号枝番, 得意先コード, 得意先名, 得意先郵便番号, 得意先住所, 得意先電話番号, 得意先ＦＡＸ, 得意先担当者役職, 得意先担当者名, 見積日, 見積有効期限, 支払条件, 見積金額, 仕入金額, 粗利額, 営業担当者, 入力担当者, 備考, ＶＡＴ, 取消区分, 登録日, 更新日, 更新者)"
                Sql1 += " VALUES('"
                Sql1 += CompanyCode
                Sql1 += "', '"
                Sql1 += TxtQuoteNo.Text
                Sql1 += "', '"
                Sql1 += TxtSuffixNo.Text
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
                Sql1 += DtpQuote.Text
                Sql1 += "', '"
                Sql1 += DtpExpiration.Text
                Sql1 += "', '"
                Sql1 += TxtPaymentTerms.Text
                Sql1 += "', '"
                Sql1 += TxtTotal.Text
                Sql1 += "', '"
                Sql1 += TxtPurchaseTotal.Text
                Sql1 += "', '"
                Sql1 += TxtGrossProfit.Text
                Sql1 += "', '"
                Sql1 += TxtSales.Text
                Sql1 += "', '"
                Sql1 += TxtInput.Text
                Sql1 += "', '"
                Sql1 += TxtRemarks.Text
                Sql1 += "', '"
                If TxtVat.Text = Nothing Then
                    Sql1 += "0"
                Else
                    Sql1 += TxtVat.Text
                End If
                Sql1 += "', '"
                Sql1 += "0"
                Sql1 += "', '"
                Sql1 += DtpRegistration.Text
                Sql1 += "', '"
                Sql1 += dtToday
                Sql1 += "', '"
                Sql1 += Input
                Sql1 += " ')"
                Sql1 += "RETURNING 会社コード"
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
                Sql1 += "ＶＡＴ"
                Sql1 += ", "
                Sql1 += "取消区分"
                Sql1 += ", "
                Sql1 += "登録日"
                Sql1 += ", "
                Sql1 += "更新日"
                Sql1 += ", "
                Sql1 += "更新者"

                _db.executeDB(Sql1)

                Dim Sql2 As String = ""
                For index As Integer = 0 To DgvItemList.Rows.Count - 1
                    Sql2 = ""
                    Sql2 += "INSERT INTO "
                    Sql2 += "Public."
                    Sql2 += "t02_mitdt("
                    Sql2 += "会社コード, 見積番号, 見積番号枝番, 行番号, 仕入区分, メーカー, 品名, 型式, 数量, 単位, 仕入先名称, 仕入単価, 仕入原価, 関税率, 関税額, 前払法人税率, 前払法人税額, 輸送費率, 輸送費額, 仕入金額, 売単価, 売上金額, 見積単価, 見積金額, 粗利額, 粗利率, リードタイム, リードタイム単位, 備考, 更新者, 登録日)"
                    Sql2 += " VALUES('"
                    Sql2 += CompanyCode
                    Sql2 += "', '"
                    Sql2 += TxtQuoteNo.Text
                    Sql2 += "', '"
                    Sql2 += TxtSuffixNo.Text
                    Sql2 += "', '"
                    If DgvItemList.Rows(index).Cells("No").Value IsNot Nothing Then
                        Sql2 += DgvItemList.Rows(index).Cells("No").Value.ToString
                        Sql2 += "', '"
                    Else
                        Sql2 += ""
                        Sql2 += "', '"
                    End If
                    If DgvItemList.Rows(index).Cells("仕入区分").Value IsNot Nothing Then
                        Sql2 += DgvItemList.Rows(index).Cells("仕入区分").Value.ToString
                        Sql2 += "', '"
                    Else
                        Sql2 += ""
                        Sql2 += "', '"
                    End If
                    If DgvItemList.Rows(index).Cells("メーカー").Value IsNot Nothing Then
                        Sql2 += DgvItemList.Rows(index).Cells("メーカー").Value.ToString
                        Sql2 += "', '"
                    Else
                        Sql2 += ""
                        Sql2 += "', '"
                    End If
                    If DgvItemList.Rows(index).Cells("品名").Value IsNot Nothing Then
                        Sql2 += DgvItemList.Rows(index).Cells("品名").Value.ToString
                        Sql2 += "', '"
                    Else
                        Sql2 += ""
                        Sql2 += "', '"
                    End If
                    If DgvItemList.Rows(index).Cells("型式").Value IsNot Nothing Then
                        Sql2 += DgvItemList.Rows(index).Cells("型式").Value.ToString
                        Sql2 += "', '"
                    Else
                        Sql2 += ""
                        Sql2 += "', '"
                    End If

                    If DgvItemList.Rows(index).Cells("数量").Value IsNot Nothing Then
                        Sql2 += DgvItemList.Rows(index).Cells("数量").Value.ToString
                        Sql2 += "', '"
                    Else
                        Sql2 += "0"
                        Sql2 += "', '"
                    End If
                    If DgvItemList.Rows(index).Cells("単位").Value IsNot Nothing Then
                        Sql2 += DgvItemList.Rows(index).Cells("単位").Value.ToString
                        Sql2 += "', '"
                    Else
                        Sql2 += ""
                        Sql2 += "', '"
                    End If
                    If DgvItemList.Rows(index).Cells("仕入先").Value IsNot Nothing Then
                        Sql2 += DgvItemList.Rows(index).Cells("仕入先").Value.ToString
                        Sql2 += "', '"
                    Else
                        Sql2 += ""
                        Sql2 += "', '"
                    End If
                    If DgvItemList.Rows(index).Cells("仕入単価").Value IsNot Nothing Then
                        Sql2 += DgvItemList.Rows(index).Cells("仕入単価").Value.ToString
                        Sql2 += "', '"
                    Else
                        Sql2 += "0"
                        Sql2 += "', '"
                    End If
                    If DgvItemList.Rows(index).Cells("仕入原価").Value IsNot Nothing Then
                        Sql2 += DgvItemList.Rows(index).Cells("仕入原価").Value.ToString
                        Sql2 += "', '"
                    Else
                        Sql2 += "0"
                        Sql2 += "', '"
                    End If
                    If DgvItemList.Rows(index).Cells("関税率").Value IsNot Nothing Then
                        Sql2 += DgvItemList.Rows(index).Cells("関税率").Value.ToString
                        Sql2 += "', '"
                    Else
                        Sql2 += "0"
                        Sql2 += "', '"
                    End If
                    If DgvItemList.Rows(index).Cells("関税額").Value IsNot Nothing Then
                        Sql2 += DgvItemList.Rows(index).Cells("関税額").Value.ToString
                        Sql2 += "', '"
                    Else
                        Sql2 += "0"
                        Sql2 += "', '"
                    End If
                    If DgvItemList.Rows(index).Cells("前払法人税率").Value IsNot Nothing Then
                        Sql2 += DgvItemList.Rows(index).Cells("前払法人税率").Value.ToString
                        Sql2 += "', '"
                    Else
                        Sql2 += "0"
                        Sql2 += "', '"
                    End If
                    If DgvItemList.Rows(index).Cells("前払法人税額").Value IsNot Nothing Then
                        Sql2 += DgvItemList.Rows(index).Cells("前払法人税額").Value.ToString
                        Sql2 += "', '"
                    Else
                        Sql2 += "0"
                        Sql2 += "', '"
                    End If
                    If DgvItemList.Rows(index).Cells("輸送費率").Value IsNot Nothing Then
                        Sql2 += DgvItemList.Rows(index).Cells("輸送費率").Value.ToString
                        Sql2 += "', '"
                    Else
                        Sql2 += "0"
                        Sql2 += "', '"
                    End If
                    If DgvItemList.Rows(index).Cells("輸送費額").Value IsNot Nothing Then
                        Sql2 += DgvItemList.Rows(index).Cells("輸送費額").Value.ToString
                        Sql2 += "', '"
                    Else
                        Sql2 += "0"
                        Sql2 += "', '"
                    End If
                    If DgvItemList.Rows(index).Cells("仕入金額").Value IsNot Nothing Then
                        Sql2 += DgvItemList.Rows(index).Cells("仕入金額").Value.ToString
                        Sql2 += "', '"
                    Else
                        Sql2 += "0"
                        Sql2 += "', '"
                    End If
                    If DgvItemList.Rows(index).Cells("売単価").Value IsNot Nothing Then
                        Sql2 += DgvItemList.Rows(index).Cells("売単価").Value.ToString
                        Sql2 += "', '"
                    Else
                        Sql2 += "0"
                        Sql2 += "', '"
                    End If
                    If DgvItemList.Rows(index).Cells("売上金額").Value IsNot Nothing Then
                        Sql2 += DgvItemList.Rows(index).Cells("売上金額").Value.ToString
                        Sql2 += "', '"
                    Else
                        Sql2 += "0"
                        Sql2 += "', '"
                    End If

                    If DgvItemList.Rows(index).Cells("見積単価").Value IsNot Nothing Then
                        Sql2 += DgvItemList.Rows(index).Cells("見積単価").Value.ToString
                        Sql2 += "', '"
                    Else
                        Sql2 += "0"
                        Sql2 += "', '"
                    End If
                    If DgvItemList.Rows(index).Cells("見積金額").Value IsNot Nothing Then
                        Sql2 += DgvItemList.Rows(index).Cells("見積金額").Value.ToString
                        Sql2 += "', '"
                    Else
                        Sql2 += "0"
                        Sql2 += "', '"
                    End If

                    If DgvItemList.Rows(index).Cells("粗利額").Value IsNot Nothing Then
                        Sql2 += DgvItemList.Rows(index).Cells("粗利額").Value.ToString
                        Sql2 += "', '"
                    Else
                        Sql2 += "0"
                        Sql2 += "', '"
                    End If
                    If DgvItemList.Rows(index).Cells("粗利率").Value IsNot Nothing Then
                        Sql2 += DgvItemList.Rows(index).Cells("粗利率").Value.ToString
                        Sql2 += "', '"
                    Else
                        Sql2 += "0"
                        Sql2 += "', '"
                    End If
                    If DgvItemList.Rows(index).Cells("リードタイム").Value IsNot Nothing Then
                        Sql2 += DgvItemList.Rows(index).Cells("リードタイム").Value.ToString
                        Sql2 += "', '"
                    Else
                        Sql2 += ""
                        Sql2 += "', '"
                    End If
                    Sql2 += DgvItemList.Rows(index).Cells("リードタイム単位").Value.ToString
                    Sql2 += "', '"
                    If DgvItemList.Rows(index).Cells("備考").Value IsNot Nothing Then
                        Sql2 += DgvItemList.Rows(index).Cells("備考").Value.ToString
                        Sql2 += "', '"
                    Else
                        Sql2 += ""
                        Sql2 += "', '"
                    End If

                    Sql2 += Input
                    Sql2 += "', '"
                    Sql2 += DtpRegistration.Text
                    Sql2 += " ')"

                    Sql2 += "RETURNING 会社コード"
                    Sql2 += ", "
                    Sql2 += "見積番号"
                    Sql2 += ", "
                    Sql2 += "見積番号枝番"
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
                    Sql2 += "数量"
                    Sql2 += ", "
                    Sql2 += "単位"
                    Sql2 += ", "
                    Sql2 += "仕入先名称"
                    Sql2 += ", "
                    Sql2 += "仕入単価"
                    Sql2 += ", "
                    Sql2 += "仕入原価"
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
                    Sql2 += "仕入金額"
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
                    Sql2 += "リードタイム"
                    Sql2 += ", "
                    Sql2 += "リードタイム単位"
                    Sql2 += ", "
                    Sql2 += "備考"
                    Sql2 += ", "
                    Sql2 += "更新者"
                    Sql2 += ", "
                    Sql2 += "登録日"

                    _db.executeDB(Sql2)
                Next
            Catch ue As UsrDefException
                ue.dspMsg()
                Throw ue
            Catch ex As Exception
                'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
            End Try
        End If
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()

    End Sub

    Private Sub BtnQuote_Click(sender As Object, e As EventArgs) Handles BtnQuote.Click

        '見積基本情報
        Dim Sql1 As String = ""
        Sql1 += "SELECT "
        Sql1 += "会社コード, "
        Sql1 += "見積番号, "
        Sql1 += "見積番号枝番, "
        Sql1 += "見積日, "
        Sql1 += "見積有効期限, "
        Sql1 += "得意先コード, "
        Sql1 += "得意先名, "
        Sql1 += "得意先担当者名, "
        Sql1 += "得意先担当者役職, "
        Sql1 += "得意先郵便番号, "
        Sql1 += "得意先住所, "
        Sql1 += "得意先電話番号, "
        Sql1 += "得意先ＦＡＸ, "
        Sql1 += "営業担当者, "
        Sql1 += "入力担当者, "
        Sql1 += "支払条件, "
        Sql1 += "備考, "
        Sql1 += "登録日, "
        Sql1 += "更新日, "
        Sql1 += "更新者 "
        Sql1 += "FROM "
        Sql1 += "public"
        Sql1 += "."
        Sql1 += "t01_mithd"
        Sql1 += " WHERE "
        Sql1 += "見積番号"
        Sql1 += " ILIKE "
        Sql1 += "'%"
        Sql1 += EditNo.ToString
        Sql1 += "%'"
        Sql1 += " AND "
        Sql1 += "見積番号枝番"
        Sql1 += " ILIKE "
        Sql1 += "'%"
        Sql1 += EditSuffix.ToString
        Sql1 += "%'"
        Dim reccnt As Integer = 0
        Dim ds1 = _db.selectDB(Sql1, RS, reccnt)

        Dim Sql2 As String = ""
        Sql2 += "SELECT "
        Sql2 += "見積番号枝番 "
        Sql2 += "FROM "
        Sql2 += "public"
        Sql2 += "."
        Sql2 += "t01_mithd"
        Sql2 += " WHERE "
        Sql2 += "見積番号"
        Sql2 += " ILIKE "
        Sql2 += "'%"
        Sql2 += EditNo.ToString
        Sql2 += "%'"

        Dim ds2 = _db.selectDB(Sql2, RS, reccnt)
        Dim SuffixMax As Integer = 0


        CompanyCode = ds1.Tables(RS).Rows(0)("会社コード")

        Dim CmnData = ds1.Tables(RS).Rows(0)


        '見積明細情報
        Dim Sql3 As String = ""
        Sql3 += "SELECT "
        Sql3 += "仕入区分, "
        Sql3 += "メーカー, "
        Sql3 += "品名, "
        Sql3 += "型式, "
        Sql3 += "数量, "
        Sql3 += "単位, "
        Sql3 += "仕入先名称, "
        Sql3 += "仕入単価, "
        Sql3 += "間接費率, "
        Sql3 += "間接費, "
        Sql3 += "仕入金額, "
        Sql3 += "売単価, "
        Sql3 += "売上金額, "
        Sql3 += "粗利額, "
        Sql3 += "粗利率, "
        Sql3 += "リードタイム, "
        Sql3 += "リードタイム単位, "
        Sql3 += "備考, "
        Sql3 += "登録日 "
        Sql3 += "FROM "
        Sql3 += "public"
        Sql3 += "."
        Sql3 += "t02_mitdt"
        Sql3 += " WHERE "
        Sql3 += "見積番号"
        Sql3 += " ILIKE "
        Sql3 += "'%"
        Sql3 += EditNo.ToString
        Sql3 += "%'"
        Sql3 += " AND "
        Sql3 += "見積番号枝番"
        Sql3 += " ILIKE "
        Sql3 += "'%"
        Sql3 += EditSuffix.ToString
        Sql3 += "%'"

        Dim ds3 = _db.selectDB(Sql3, RS, reccnt)

        Dim Sql4 As String = ""
        Sql4 += "SELECT "
        Sql4 += "会社コード, "
        Sql4 += "会社名, "
        Sql4 += "会社略称, "
        Sql4 += "郵便番号, "
        Sql4 += "住所１, "
        Sql4 += "住所２, "
        Sql4 += "住所３, "
        Sql4 += "電話番号, "
        Sql4 += "ＦＡＸ番号, "
        Sql4 += "代表者役職, "
        Sql4 += "代表者名, "
        Sql4 += "表示順, "
        Sql4 += "備考, "
        Sql4 += "銀行コード, "
        Sql4 += "支店コード, "
        Sql4 += "預金種目, "
        Sql4 += "口座番号, "
        Sql4 += "口座名義, "
        Sql4 += "更新者, "
        Sql4 += "更新日 "
        Sql4 += "FROM "
        Sql4 += "public"
        Sql4 += "."
        Sql4 += "m01_company"

        Dim ds4 = _db.selectDB(Sql4, RS, reccnt)


        '定義
        Dim app As Excel.Application = Nothing
        Dim book As Excel.Workbook = Nothing
        Dim sheet As Excel.Worksheet = Nothing



        Try
            '雛形パス
            Dim sHinaPath As String = ""
            sHinaPath = StartUp._iniVal.BaseXlsPath

            '雛形ファイル名
            Dim sHinaFile As String = ""
            sHinaFile = sHinaPath & "\" & "Quotation.xlsx"

            '出力先パス
            Dim sOutPath As String = ""
            sOutPath = StartUp._iniVal.OutXlsPath

            '出力ファイル名
            Dim sOutFile As String = ""
            sOutFile = sOutPath & "\" & CmnData(1) & "-" & CmnData(2) & ".xlsx"



            app = New Excel.Application()
            book = app.Workbooks.Add(sHinaFile)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet)

            sheet.Range("D1").Value = ds4.Tables(RS).Rows(0)(1)
            sheet.Range("D2").Value = ds4.Tables(RS).Rows(0)(3) & " " & ds4.Tables(RS).Rows(0)(4)
            sheet.Range("D3").Value = ds4.Tables(RS).Rows(0)(5) & " " & ds4.Tables(RS).Rows(0)(6)
            sheet.Range("D4").Value = "telp. " & ds4.Tables(RS).Rows(0)(7) & " Fax." & ds4.Tables(RS).Rows(0)(8)

            sheet.Range("E8").Value = CmnData(6)                       '得意先名
            sheet.Range("E9").Value = CmnData(8) & " " & CmnData(7)    '得意先担当者
            sheet.Range("E11").Value = CmnData(11)                       '得意先名
            sheet.Range("E12").Value = CmnData(12)                       '得意先名

            sheet.Range("S8").Value = CmnData(1) & "-" & CmnData(2)    '見積番号
            sheet.Range("S9").Value = CmnData(3)                       '見積番号

            sheet.Range("H27").Value = CmnData(15)                       '見積番号
            sheet.Range("H28").Value = CmnData(10) & " " & CmnData(11)                        '見積番号
            sheet.Range("H29").Value = CmnData(4)                       '見積番号
            sheet.Range("H30").Value = CmnData(16)                       '見積番号

            sheet.Range("D34").Value = CmnData(13)                       '見積番号
            sheet.Range("D35").Value = CmnData(14)                       '見積番号

            Dim rowCnt As Integer = 0
            Dim lstRow As Integer = 22
            Dim addRowCnt As Integer = 0
            Dim currentCnt As Integer = 20
            Dim num As Integer = 1

            rowCnt = ds3.Tables(RS).Rows.Count - 1
            'rowCnt = 10

            Dim cellPos As String = lstRow & ":" & lstRow

            If rowCnt > 1 Then
                For addRow As Integer = 0 To rowCnt
                    Dim R As Object
                    cellPos = lstRow - 2 & ":" & lstRow - 2
                    R = sheet.Range(cellPos)
                    R.Copy()
                    R.Insert()
                    If Marshal.IsComObject(R) Then
                        Marshal.ReleaseComObject(R)
                    End If

                    lstRow = lstRow + 1
                Next
            End If

            Dim totalPrice As Integer = 0
            Dim Sql5 As String = ""
            Dim tmp1 As String = ""

            For index As Integer = 0 To ds3.Tables(RS).Rows.Count - 1
                Dim cell As String

                cell = "A" & currentCnt
                sheet.Range(cell).Value = num
                cell = "C" & currentCnt
                sheet.Range(cell).Value = ds3.Tables(RS).Rows(index)("メーカー") & "/" & ds3.Tables(RS).Rows(index)("品名") & "/" & ds3.Tables(RS).Rows(index)("型式") & vbCrLf & ds3.Tables(RS).Rows(index)("備考")
                cell = "L" & currentCnt
                sheet.Range(cell).Value = ds3.Tables(RS).Rows(index)("数量")
                cell = "O" & currentCnt
                sheet.Range(cell).Value = ds3.Tables(RS).Rows(index)("単位")
                cell = "R" & currentCnt
                sheet.Range(cell).Value = ds3.Tables(RS).Rows(index)("売単価")
                cell = "V" & currentCnt
                sheet.Range(cell).Value = ds3.Tables(RS).Rows(index)("売上金額")

                totalPrice = totalPrice + ds3.Tables(RS).Rows(index)("売上金額")
                Sql5 = ""
                Sql5 += "SELECT "
                Sql5 += "* "
                Sql5 += "FROM "
                Sql5 += "public"
                Sql5 += "."
                Sql5 += "m90_hanyo"
                Sql5 += " WHERE "
                Sql5 += "会社コード"
                Sql5 += " ILIKE "
                Sql5 += "'"
                Sql5 += frmC01F10_Login.loginValue.BumonNM
                Sql5 += "'"
                Sql5 += " AND "
                Sql5 += "固定キー"
                Sql5 += " ILIKE "
                Sql5 += "'"
                Sql5 += "4"
                Sql5 += "'"
                Sql5 += " AND "
                Sql5 += "可変キー"
                Sql5 += " ILIKE "
                Sql5 += "'"
                Sql5 += ds3.Tables(RS).Rows(index)("リードタイム単位").ToString
                Sql5 += "'"
                Dim ds5 = _db.selectDB(Sql5, RS, reccnt)

                cell = "Z" & currentCnt
                tmp1 = ""
                tmp1 += ds3.Tables(RS).Rows(index)("リードタイム")
                tmp1 += ds5.Tables(RS).Rows(0)("文字１")

                sheet.Range(cell).Value = tmp1

                'sheet.Rows(currentCnt & ":" & currentCnt)

                currentCnt = currentCnt + 1
                num = num + 1
            Next


            sheet.Range("S" & lstRow + 1).Value = totalPrice
            sheet.Range("S" & lstRow + 2).Value = totalPrice * 10 * 0.01
            sheet.Range("S" & lstRow + 3).Value = totalPrice * 10 * 0.01 + totalPrice
            sheet.Rows.AutoFit()
            book.SaveAs(sOutFile)
            app.Visible = True

            _msgHd.dspMSG("CreateExcel")

        Catch ex As Exception
            Throw ex

        Finally
            'app.Quit()
            'Marshal.ReleaseComObject(sheet)
            'Marshal.ReleaseComObject(book)
            'Marshal.ReleaseComObject(app)

        End Try
    End Sub

    Private Sub BtnQuoteRequest_Click(sender As Object, e As EventArgs) Handles BtnQuoteRequest.Click
        Dim createFlg = False

        '見積基本情報
        Dim Sql1 As String = ""
        Sql1 += "SELECT "
        Sql1 += "会社コード, "
        Sql1 += "見積番号, "
        Sql1 += "見積番号枝番, "
        Sql1 += "見積日, "
        Sql1 += "見積有効期限, "
        Sql1 += "得意先コード, "
        Sql1 += "得意先名, "
        Sql1 += "得意先担当者名, "
        Sql1 += "得意先担当者役職, "
        Sql1 += "得意先郵便番号, "
        Sql1 += "得意先住所, "
        Sql1 += "得意先電話番号, "
        Sql1 += "得意先ＦＡＸ, "
        Sql1 += "営業担当者, "
        Sql1 += "入力担当者, "
        Sql1 += "支払条件, "
        Sql1 += "備考, "
        Sql1 += "登録日, "
        Sql1 += "更新日, "
        Sql1 += "更新者 "
        Sql1 += "FROM "
        Sql1 += "public"
        Sql1 += "."
        Sql1 += "t01_mithd"
        Sql1 += " WHERE "
        Sql1 += "見積番号"
        Sql1 += " ILIKE "
        Sql1 += "'%"
        Sql1 += EditNo.ToString
        Sql1 += "%'"
        Sql1 += " AND "
        Sql1 += "見積番号枝番"
        Sql1 += " ILIKE "
        Sql1 += "'%"
        Sql1 += EditSuffix.ToString
        Sql1 += "%'"

        Dim reccnt As Integer = 0
        Dim ds1 = _db.selectDB(Sql1, RS, reccnt)

        Dim Sql2 As String = ""
        Sql2 += "SELECT "
        Sql2 += "見積番号枝番 "
        Sql2 += "FROM "
        Sql2 += "public"
        Sql2 += "."
        Sql2 += "t01_mithd"
        Sql2 += " WHERE "
        Sql2 += "見積番号"
        Sql2 += " ILIKE "
        Sql2 += "'%"
        Sql2 += EditNo.ToString
        Sql2 += "%'"


        Dim ds2 = _db.selectDB(Sql2, RS, reccnt)
        Dim SuffixMax As Integer = 0


        CompanyCode = ds1.Tables(RS).Rows(0)("会社コード")

        Dim CmnData = ds1.Tables(RS).Rows(0)


        '見積明細情報
        Dim Sql3 As String = ""
        Sql3 += "SELECT "
        Sql3 += "* "
        Sql3 += "FROM "
        Sql3 += "public"
        Sql3 += "."
        Sql3 += "t02_mitdt"
        Sql3 += " WHERE "
        Sql3 += "見積番号"
        Sql3 += " ILIKE "
        Sql3 += "'%"
        Sql3 += EditNo.ToString
        Sql3 += "%'"
        Sql3 += " AND "
        Sql3 += "見積番号枝番"
        Sql3 += " ILIKE "
        Sql3 += "'%"
        Sql3 += EditSuffix.ToString
        Sql3 += "%'"

        Dim ds3 = _db.selectDB(Sql3, RS, reccnt)


        Dim supplierlist As New List(Of String)(New String() {})
        Dim supplierChkList As New List(Of Boolean)
        For i As Integer = 0 To ds3.tables(RS).rows.count - 1
            If supplierlist.Contains(ds3.tables(RS).rows(i)("仕入先名称")) = False Then
                supplierlist.Add(ds3.tables(RS).rows(i)("仕入先名称"))
                supplierChkList.Add(False)
            End If
        Next

        Dim supplier

        For i As Integer = 0 To supplierlist.Count - 1
            supplier = supplierlist(i)
            For j As Integer = 0 To ds3.tables(RS).rows.count - 1
                If supplier Is ds3.tables(RS).rows(j)("仕入先名称") And ds3.tables(RS).rows(j)("仕入単価") <= 0 Then
                    supplierChkList(i) = True
                End If
            Next
        Next

        For i As Integer = 0 To supplierlist.Count - 1

            If supplierChkList(i) = True Then

                '定義
                Dim app As Excel.Application = Nothing
                Dim book As Excel.Workbook = Nothing
                Dim sheet As Excel.Worksheet = Nothing



                Try
                    '雛形パス
                    Dim sHinaPath As String = ""
                    sHinaPath = StartUp._iniVal.BaseXlsPath

                    '雛形ファイル名
                    Dim sHinaFile As String = ""
                    sHinaFile = sHinaPath & "\" & "QuotationRequest.xlsx"

                    '出力先パス
                    Dim sOutPath As String = ""
                    sOutPath = StartUp._iniVal.OutXlsPath

                    '出力ファイル名
                    Dim sOutFile As String = ""
                    sOutFile = sOutPath & "\" & CmnData("見積番号") & "-" & CmnData("見積番号枝番") & "_Request_" & supplierlist(i) & ".xlsx"



                    app = New Excel.Application()
                    book = app.Workbooks.Add(sHinaFile)  'テンプレート
                    sheet = CType(book.Worksheets(1), Excel.Worksheet)

                    sheet.Range("AA2").Value = CmnData("見積番号") & "-" & CmnData("見積番号枝番")
                    sheet.Range("AA3").Value = System.DateTime.Today
                    sheet.Range("A12").Value = supplierlist(i)
                    sheet.Range("V19").Value = CmnData("営業担当者")
                    sheet.Range("V20").Value = CmnData("入力担当者")


                    Dim rowCnt As Integer = 0
                    Dim lstRow As Integer = 23
                    'Dim addRowCnt As Integer = 0
                    'Dim currentCnt As Integer = 20
                    'Dim num As Integer = 1


                    For j As Integer = 0 To ds3.tables(RS).rows.count - 1
                        If supplierlist(i) Is ds3.tables(RS).rows(j)("仕入先名称") And ds3.tables(RS).rows(j)("仕入単価") <= 0 Then
                            If rowCnt = 0 Then
                                sheet.Range("A23").Value = ds3.tables(RS).rows(j)("メーカー") & vbLf & ds3.tables(RS).rows(j)("品名") & vbLf & ds3.tables(RS).rows(j)("型式")
                                sheet.Range("J23").Value = ds3.tables(RS).rows(j)("数量") & " " & ds3.tables(RS).rows(j)("単位")
                                'sheet.Rows(lstRow & ":" & lstRow).AutoFit
                            Else
                                Dim cellPos As String = lstRow & ":" & lstRow
                                Dim R As Object
                                cellPos = lstRow & ":" & lstRow
                                R = sheet.Range(cellPos)
                                R.Copy()
                                R.Insert()
                                If Marshal.IsComObject(R) Then
                                    Marshal.ReleaseComObject(R)
                                End If

                                lstRow = lstRow + 1

                                sheet.Range("A" & lstRow).Value = ds3.tables(RS).rows(j)("メーカー") & vbLf & ds3.tables(RS).rows(j)("品名") & vbLf & ds3.tables(RS).rows(j)("型式")
                                sheet.Range("J" & lstRow).Value = ds3.tables(RS).rows(j)("数量") & " " & ds3.tables(RS).rows(j)("単位")
                                'sheet.Rows(lstRow & ":" & lstRow).AutoFit

                            End If



                        End If
                    Next

                    book.SaveAs(sOutFile)
                    app.Visible = True

                    '_msgHd.dspMSG("CreateExcel")
                    createFlg = True

                Catch ex As Exception
                    Throw ex

                Finally
                    'app.Quit()
                    'Marshal.ReleaseComObject(sheet)
                    'Marshal.ReleaseComObject(book)
                    'Marshal.ReleaseComObject(app)

                End Try
            End If

            If (createFlg = True) Then
                _msgHd.dspMSG("CreateExcel")
            End If

        Next

    End Sub

    Private Sub BtnProof_Click(sender As Object, e As EventArgs) Handles BtnProof.Click
        Dim QuoteNo As String = TxtQuoteNo.Text
        Dim QuoteSuffix As String = TxtSuffixNo.Text
        Dim RegistrationDate As String = DtpRegistration.Text
        Dim QuoteDate As String = DtpQuote.Text
        Dim Expiration As String = DtpExpiration.Text
        Dim CustomerCode As String = TxtCustomerCode.Text
        Dim CustomerName As String = TxtCustomerName.Text
        Dim PostalCode As String = TxtPostalCode.Text
        Dim Address1 As String = TxtAddress1.Text
        Dim Address2 As String = TxtAddress2.Text
        Dim Address3 As String = TxtAddress3.Text
        Dim Tel As String = TxtTel.Text
        Dim Fax As String = TxtFax.Text
        Dim Person As String = TxtPerson.Text
        Dim Position As String = TxtPosition.Text
        Dim Sales As String = TxtSales.Text
        Dim Input As String = TxtInput.Text
        Dim PaymentTerms As String = TxtPaymentTerms.Text
        Dim QuoteRemarks As String = TxtRemarks.Text
        Dim ItemCount As String = TxtItemCount.Text
        Dim Vat As String = TxtVat.Text
        Dim PurchaseTotal As String = TxtPurchaseTotal.Text
        Dim QuoteAmount As String = TxtTotal.Text
        Dim GrossProfitAmount As String = TxtGrossProfit.Text

        Dim No(DgvItemList.Rows.Count - 1) As String
        Dim PurchaseCategory(DgvItemList.Rows.Count - 1) As String
        Dim Maker(DgvItemList.Rows.Count - 1) As String
        Dim ItemName(DgvItemList.Rows.Count - 1) As String
        Dim Model(DgvItemList.Rows.Count - 1) As String
        Dim Quantity(DgvItemList.Rows.Count - 1) As String
        Dim Unit(DgvItemList.Rows.Count - 1) As String
        Dim Supplier(DgvItemList.Rows.Count - 1) As String
        Dim PurchasePrice(DgvItemList.Rows.Count - 1) As String
        Dim OverHead(DgvItemList.Rows.Count - 1) As String
        Dim PurchaseAmount1(DgvItemList.Rows.Count - 1) As String
        Dim PurchaseAmount2(DgvItemList.Rows.Count - 1) As String
        Dim SellingPrice(DgvItemList.Rows.Count - 1) As String
        Dim SellingAmount(DgvItemList.Rows.Count - 1) As String
        Dim GrossProfit(DgvItemList.Rows.Count - 1) As String
        Dim GrossProfitRate(DgvItemList.Rows.Count - 1) As String
        Dim LeadTime(DgvItemList.Rows.Count - 1) As String
        Dim ItemRemarks(DgvItemList.Rows.Count - 1) As String
        Dim tmp As String = ""

        '定義
        Dim app As Excel.Application = Nothing
        Dim book As Excel.Workbook = Nothing
        Dim sheet As Excel.Worksheet = Nothing



        Try
            '雛形パス
            Dim sHinaPath As String = ""
            sHinaPath = StartUp._iniVal.BaseXlsPath

            '雛形ファイル名
            Dim sHinaFile As String = ""
            sHinaFile = sHinaPath & "\" & "Proof.xlsx"

            '出力先パス
            Dim sOutPath As String = ""
            sOutPath = StartUp._iniVal.OutXlsPath

            '出力ファイル名
            Dim sOutFile As String = ""
            sOutFile = sOutPath & "\Proof_" & QuoteNo & "-" & QuoteSuffix & ".xlsx"



            app = New Excel.Application()
            book = app.Workbooks.Add(sHinaFile)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet)

            Dim rowCnt As Integer = 0
            Dim currentRow As Integer = 10
            Dim lastRow As Integer = 12

            sheet.Range("B1").Value = QuoteNo & "-" & QuoteSuffix
            sheet.Range("I1").Value = QuoteDate & "(" & RegistrationDate & ")"
            sheet.Range("N1").Value = Expiration
            sheet.Range("B2").Value = CustomerName
            sheet.Range("B3").Value = PostalCode & " " & Address1
            sheet.Range("B4").Value = Address2
            sheet.Range("B5").Value = Address3
            sheet.Range("I2").Value = Tel
            sheet.Range("I3").Value = Fax
            sheet.Range("I4").Value = Person
            sheet.Range("I5").Value = Position
            sheet.Range("N2").Value = Sales
            sheet.Range("N3").Value = Input
            sheet.Range("B6").Value = PaymentTerms
            sheet.Range("B7").Value = QuoteRemarks
            sheet.Range("W14").Value = PurchaseTotal
            sheet.Range("W15").Value = QuoteAmount
            sheet.Range("W16").Value = GrossProfitAmount



            For index As Integer = 0 To DgvItemList.Rows.Count - 1
                If rowCnt > 3 Then
                    Dim R As Object
                    R = sheet.Range(lastRow - 2 & ":" & lastRow - 2)
                    R.Copy()
                    R.Insert()
                    If Marshal.IsComObject(R) Then
                        Marshal.ReleaseComObject(R)
                    End If
                    lastRow += 1
                End If

                sheet.Range("A" & currentRow).Value = DgvItemList.Rows(index).Cells("No").Value
                sheet.Range("B" & currentRow).Value = PurchaseCategory(index) = DgvItemList(1, index).Value
                sheet.Range("C" & currentRow).Value = DgvItemList.Rows(index).Cells("メーカー").Value
                sheet.Range("D" & currentRow).Value = DgvItemList.Rows(index).Cells("品名").Value
                sheet.Range("E" & currentRow).Value = DgvItemList.Rows(index).Cells("型式").Value
                sheet.Range("F" & currentRow).Value = DgvItemList.Rows(index).Cells("数量").Value
                sheet.Range("G" & currentRow).Value = DgvItemList.Rows(index).Cells("単位").Value
                sheet.Range("H" & currentRow).Value = DgvItemList.Rows(index).Cells("仕入先").Value
                sheet.Range("I" & currentRow).Value = DgvItemList.Rows(index).Cells("仕入単価").Value
                sheet.Range("J" & currentRow).Value = DgvItemList.Rows(index).Cells("仕入原価").Value
                sheet.Range("K" & currentRow).Value = DgvItemList.Rows(index).Cells("関税率").Value
                sheet.Range("L" & currentRow).Value = DgvItemList.Rows(index).Cells("関税額").Value
                sheet.Range("M" & currentRow).Value = DgvItemList.Rows(index).Cells("前払法人税率").Value
                sheet.Range("N" & currentRow).Value = DgvItemList.Rows(index).Cells("前払法人税額").Value
                sheet.Range("O" & currentRow).Value = DgvItemList.Rows(index).Cells("輸送費率").Value
                sheet.Range("P" & currentRow).Value = DgvItemList.Rows(index).Cells("輸送費額").Value
                sheet.Range("Q" & currentRow).Value = DgvItemList.Rows(index).Cells("仕入金額").Value
                sheet.Range("R" & currentRow).Value = DgvItemList.Rows(index).Cells("売単価").Value
                sheet.Range("S" & currentRow).Value = DgvItemList.Rows(index).Cells("売上金額").Value
                sheet.Range("T" & currentRow).Value = DgvItemList.Rows(index).Cells("粗利額").Value
                sheet.Range("U" & currentRow).Value = DgvItemList.Rows(index).Cells("粗利率").Value
                tmp = ""
                tmp += DgvItemList.Rows(index).Cells("リードタイム").Value
                tmp += DgvItemList.Item("リードタイム単位", index).FormattedValue
                sheet.Range("V" & currentRow).Value = tmp
                sheet.Range("W" & currentRow).Value = DgvItemList.Rows(index).Cells("備考").Value


                currentRow += 1
                rowCnt += 1

            Next

            book.SaveAs(sOutFile)
            app.Visible = True

            _msgHd.dspMSG("CreateExcel")

        Catch ex As Exception
            Throw ex

        Finally
            'app.Quit()
            'Marshal.ReleaseComObject(sheet)
            'Marshal.ReleaseComObject(book)
            'Marshal.ReleaseComObject(app)

        End Try

    End Sub

    Private Sub BtnCodeSearch_Click(sender As Object, e As EventArgs) Handles BtnCodeSearch.Click
        Dim SqlCode As String = ""
        SqlCode += "SELECT "
        SqlCode += "* "
        SqlCode += "FROM "
        SqlCode += "public"
        SqlCode += "."
        SqlCode += "m10_customer"
        SqlCode += " WHERE "
        SqlCode += "会社コード"
        SqlCode += " ILIKE "
        SqlCode += "'"
        SqlCode += CompanyCode
        SqlCode += "'"
        SqlCode += " AND "
        SqlCode += "得意先コード"
        SqlCode += " ILIKE "
        SqlCode += "'"
        SqlCode += TxtCustomerCode.Text
        SqlCode += "'"

        Dim reccnt As Integer = 0
        Dim dsCode = _db.selectDB(SqlCode, RS, reccnt)
        If dsCode.Tables(RS).Rows.Count > 0 Then
            TxtCustomerName.Text = dsCode.Tables(RS).Rows(0)("得意先名").ToString
            TxtPostalCode.Text = dsCode.Tables(RS).Rows(0)("郵便番号").ToString
            TxtAddress1.Text = dsCode.Tables(RS).Rows(0)("住所１").ToString
            TxtAddress2.Text = dsCode.Tables(RS).Rows(0)("住所２").ToString
            TxtAddress3.Text = dsCode.Tables(RS).Rows(0)("住所３").ToString
            TxtTel.Text = dsCode.Tables(RS).Rows(0)("電話番号").ToString
            TxtFax.Text = dsCode.Tables(RS).Rows(0)("ＦＡＸ番号").ToString
            TxtPerson.Text = dsCode.Tables(RS).Rows(0)("担当者名").ToString
            TxtPosition.Text = dsCode.Tables(RS).Rows(0)("担当者役職").ToString
            TxtPaymentTerms.Text = dsCode.Tables(RS).Rows(0)("既定支払条件").ToString
        End If
    End Sub
End Class
