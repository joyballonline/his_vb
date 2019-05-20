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
Imports System.Globalization
Imports System.IO

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
        Dim dtNow As DateTime = DateTime.Now

        '仕入原価の制御
        DgvItemList.Columns("仕入原価").ReadOnly = True
        DgvItemList.Columns("仕入原価").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
        '関税額の制御
        DgvItemList.Columns("関税額").ReadOnly = True
        DgvItemList.Columns("関税額").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
        '前払法人税額の制御
        DgvItemList.Columns("前払法人税額").ReadOnly = True
        DgvItemList.Columns("前払法人税額").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
        '輸送費額の制御
        DgvItemList.Columns("輸送費額").ReadOnly = True
        DgvItemList.Columns("輸送費額").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)

        'セルの内容に合わせて、行の高さが自動的に調節されるようにする
        DgvItemList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        '"Column1"列のセルのテキストを折り返して表示する
        DgvItemList.Columns("型式").DefaultCellStyle.WrapMode = DataGridViewTriState.True
        DgvItemList.Columns("備考").DefaultCellStyle.WrapMode = DataGridViewTriState.True


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

        Dim reccnt As Integer = 0

        Dim Sql12 As String = ""

        Sql12 += "SELECT * FROM public.m90_hanyo"
        Sql12 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql12 += " AND 固定キー = '" & CommonConst.FIXED_KEY_READTIME & "'"
        Dim ds12 As DataSet = _db.selectDB(Sql12, RS, reccnt)

        Dim table2 As New DataTable("Table")
        table2.Columns.Add("Display", GetType(String))
        table2.Columns.Add("Value", GetType(Integer))

        'リードタイム単位の多言語対応
        For i As Integer = 0 To ds12.Tables(RS).Rows.Count - 1
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                table2.Rows.Add(ds12.Tables(RS).Rows(i)("文字２"), ds12.Tables(RS).Rows(i)("可変キー"))
            Else
                table2.Rows.Add(ds12.Tables(RS).Rows(i)("文字１"), ds12.Tables(RS).Rows(i)("可変キー"))
            End If

        Next

        Dim column2 As New DataGridViewComboBoxColumn()
        column2.DataSource = table2
        column2.ValueMember = "Value"
        column2.DisplayMember = "Display"
        column2.HeaderText = "リードタイム単位"
        column2.Name = "リードタイム単位"

        DgvItemList.Columns.Insert(25, column2)

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblMode.Text = "Mode"
            LblQuoteNo.Text = "QuotationNo"
            LblRegistration.Text = "RegistrationDate"
            LblQuote.Text = "QuotationDate"
            LblExpiration.Text = "ExpirationDate"
            LblCustomerName.Text = "CustomerName"
            LblAddress.Text = "Address"
            LblTel.Text = "PhoneNumber"
            LblFax.Text = "FAX"
            LblPerson.Text = "NameOfPIC"
            LblPosition.Text = "PositionPICCustomer"
            LblSales.Text = "SalesPersonInCharge"
            LblInput.Text = "PICForInputting"
            LblPaymentTerms.Text = "PaymentTerms" '支払条件
            LblRemarks.Text = "Remarks"
            LblItemCount.Text = "ItemCount" '明細数
            LblPurchaseAmount.Text = "PurchaseAmount"
            LblOrderAmount.Text = "SalesAmount"
            LblQuoteAmount.Text = "QuotationAmount"
            LblGrossProfit.Text = "GrossMargin"
            LblVat.Text = "Vat(%)"

            RbtnGP.Text = "GrossMarginInput" '粗利入力
            RbtnUP.Text = "InputUnitPrice " '単価入力
            RbtnQuote.Text = "QuotationPriceInput" '見積単価入力

            BtnCodeSearch.Text = "Search"
            BtnInsert.Text = "InsertLine"
            BtnUp.Text = "ShiftLineUp"
            BtnDown.Text = "ShiftLineDown"
            BtnRowsAdd.Text = "AddLine"
            BtnRowsDel.Text = "DeleteLine"
            BtnClone.Text = "LineDuplication"
            BtnProof.Text = "Proof"
            BtnQuoteRequest.Text = "QuotationRequest"
            BtnQuote.Text = "Quotation"
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

            LblItemCount.Size = New Size(125, 45)
            LblItemCount.Location = New Point(1216, 157)
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

            '英語用見出し
            DgvItemList.Columns("仕入区分").HeaderText = "PurchasingClassification"
            DgvItemList.Columns("メーカー").HeaderText = "Manufacturer"
            DgvItemList.Columns("品名").HeaderText = "ItemName"
            DgvItemList.Columns("型式").HeaderText = "Spec"
            DgvItemList.Columns("数量").HeaderText = "Quantity:a"
            DgvItemList.Columns("単位").HeaderText = "Unit"
            DgvItemList.Columns("仕入先コード").HeaderText = "SupplierCode"
            DgvItemList.Columns("仕入先").HeaderText = "SupplierName"
            DgvItemList.Columns("仕入単価").HeaderText = "PurchaseUnitPrice:b"
            DgvItemList.Columns("仕入原価").HeaderText = "PurchasingCost:c=a*b"
            DgvItemList.Columns("関税率").HeaderText = "CustomsDutyRate:d"
            DgvItemList.Columns("関税額").HeaderText = "CustomsDuty:e=b*d"
            DgvItemList.Columns("前払法人税率").HeaderText = "PrepaidCorporateTaxRate:f"
            DgvItemList.Columns("前払法人税額").HeaderText = "PrepaidCorporateTaxAmount:g=(b+e)*f"
            DgvItemList.Columns("輸送費率").HeaderText = "TransportationCostRate:h"
            DgvItemList.Columns("輸送費額").HeaderText = "TransportationCost:i=b*h"
            DgvItemList.Columns("仕入金額").HeaderText = "PurchaseAmount:j=(b+e+g+i)*a"
            DgvItemList.Columns("売単価").HeaderText = "SellingPrice:k"
            DgvItemList.Columns("売上金額").HeaderText = "SalesAmount:l=a*k"
            DgvItemList.Columns("見積単価").HeaderText = "QuotetionPrice:m=k+e+g+i"
            DgvItemList.Columns("見積金額").HeaderText = "QuotetionAmount:n=m*a"
            DgvItemList.Columns("粗利額").HeaderText = "GrossMargin:o=(k-b)*a"
            DgvItemList.Columns("粗利率").HeaderText = "GrossMarginRate(%):p=(1-(b/k))*100"
            DgvItemList.Columns("リードタイム").HeaderText = "LeadTime"
            DgvItemList.Columns("リードタイム単位").HeaderText = "LeadTimeUnit"
            DgvItemList.Columns("備考").HeaderText = "Remarks"
        Else
            '日本語用見出し
            DgvItemList.Columns("数量").HeaderText = "数量:a"
            DgvItemList.Columns("仕入単価").HeaderText = "仕入単価:b"
            DgvItemList.Columns("仕入原価").HeaderText = "仕入原価:c=a*b"
            DgvItemList.Columns("関税率").HeaderText = "関税率:d"
            DgvItemList.Columns("関税額").HeaderText = "関税額:e=b*d"
            DgvItemList.Columns("前払法人税率").HeaderText = "前払法人税率:f"
            DgvItemList.Columns("前払法人税額").HeaderText = "前払法人税額:g=(b+e)*f"
            DgvItemList.Columns("輸送費率").HeaderText = "輸送費率:h"
            DgvItemList.Columns("輸送費額").HeaderText = "輸送費額:i=b*h"
            DgvItemList.Columns("仕入金額").HeaderText = "仕入金額:j=(b+e+g+i)*a"
            DgvItemList.Columns("売単価").HeaderText = "売単価:k"
            DgvItemList.Columns("売上金額").HeaderText = "売上金額:l=a*k"
            DgvItemList.Columns("見積単価").HeaderText = "見積単価:m=k+e+g+i"
            DgvItemList.Columns("見積金額").HeaderText = "見積金額:n=m*a"
            DgvItemList.Columns("粗利額").HeaderText = "粗利額:o=(k-b)*a"
            DgvItemList.Columns("粗利率").HeaderText = "粗利率(%):p=(1-(b/k))*100"

        End If

        '
        '新規登録モード以外
        '
        If EditNo IsNot Nothing Then

            Dim Sql As String = ""

            '見積基本情報取得
            Sql = "SELECT * FROM public.t01_mithd"
            Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " and 見積番号 = '" & EditNo.ToString & "'"
            Sql += " AND 見積番号枝番 = '" & EditSuffix.ToString & "'"

            Dim ds1 = _db.selectDB(Sql, RS, reccnt)

            '見積番号の最新の枝番を取ろうとしているっぽい
            Sql = "SELECT 見積番号枝番 FROM public.t01_mithd"
            Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " and 見積番号 =  '" & EditNo.ToString & "'"

            Dim ds2 = _db.selectDB(Sql, RS, reccnt)
            Dim SuffixMax As Integer = 0

            '=======================
            '   複写モード
            '=======================
            If Status Is CommonConst.STATUS_CLONE Then

                Sql = "SELECT "
                Sql += "会社コード, "
                Sql += "採番キー, "
                Sql += "最新値, "
                Sql += "最小値, "
                Sql += "最大値, "
                Sql += "接頭文字, "
                Sql += "連番桁数 "
                Sql += "FROM public.m80_saiban"
                Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " and 採番キー = '10'"

                Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

                ' 指定した書式で日付を文字列に変換する
                Dim QuoteDate As String = dtNow.ToString("MMdd")

                TxtQuoteNo.Text += ds.Tables(RS).Rows(0)("接頭文字")
                TxtQuoteNo.Text += QuoteDate
                CompanyCode = ds.Tables(RS).Rows(0)("会社コード")
                KeyNo = ds.Tables(RS).Rows(0)("採番キー")
                QuoteNo = ds.Tables(RS).Rows(0)("最新値")
                QuoteNoMin = ds.Tables(RS).Rows(0)("最小値")
                QuoteNoMax = ds.Tables(RS).Rows(0)("最大値")
                TxtQuoteNo.Text += QuoteNo.PadLeft(ds.Tables(RS).Rows(0)("連番桁数"), "0")

                SaibanSave()

                DtpQuote.Value = dtNow.ToShortDateString                                                '見積日
                DtpExpiration.Text = DateAdd("d", CommonConst.DEADLINE_DEFAULT_DAY, DtpQuote.Value)     '見積有効期限

            Else
                '=======================
                '   複写モード以外
                '=======================
                TxtQuoteNo.Text = ds1.Tables(RS).Rows(0)("見積番号")
                For index As Integer = 0 To ds2.Tables(RS).Rows.Count - 1
                    If SuffixMax <= ds2.Tables(RS).Rows(index)("見積番号枝番") Then
                        SuffixMax = ds2.Tables(RS).Rows(index)("見積番号枝番")
                    End If
                Next

                Select Case Status
                    Case CommonConst.STATUS_VIEW
                        DtpQuote.Value = ds1.Tables(RS).Rows(0)("見積日")                                       '見積日
                        DtpExpiration.Text = DateAdd("d", CommonConst.DEADLINE_DEFAULT_DAY, DtpQuote.Value)     '見積有効期限
                    Case Else
                        DtpQuote.Value = dtNow.ToShortDateString                                                '見積日
                        DtpExpiration.Text = DateAdd("d", CommonConst.DEADLINE_DEFAULT_DAY, DtpQuote.Value)     '見積有効期限
                End Select


            End If

            CompanyCode = ds1.Tables(RS).Rows(0)("会社コード")

            Select Case Status
                Case CommonConst.STATUS_PRICE
                    TxtSuffixNo.Text = ds1.Tables(RS).Rows(0)("見積番号枝番")
                Case CommonConst.STATUS_VIEW
                    TxtSuffixNo.Text = ds1.Tables(RS).Rows(0)("見積番号枝番")
                Case Else
                    TxtSuffixNo.Text = SuffixMax + 1
            End Select

            TxtCustomerCode.Text = ds1.Tables(RS).Rows(0)("得意先コード").ToString
            TxtCustomerName.Text = ds1.Tables(RS).Rows(0)("得意先名").ToString
            TxtPerson.Text = ds1.Tables(RS).Rows(0)("得意先担当者名").ToString
            TxtPosition.Text = ds1.Tables(RS).Rows(0)("得意先担当者役職").ToString
            TxtPostalCode.Text = ds1.Tables(RS).Rows(0)("得意先郵便番号").ToString
            TxtAddress1.Text = ds1.Tables(RS).Rows(0)("得意先住所").ToString
            TxtTel.Text = ds1.Tables(RS).Rows(0)("得意先電話番号").ToString
            TxtFax.Text = ds1.Tables(RS).Rows(0)("得意先ＦＡＸ").ToString
            TxtSales.Text = ds1.Tables(RS).Rows(0)("営業担当者").ToString
            TxtInput.Text = ds1.Tables(RS).Rows(0)("入力担当者").ToString
            TxtPaymentTerms.Text = ds1.Tables(RS).Rows(0)("支払条件").ToString
            TxtRemarks.Text = ds1.Tables(RS).Rows(0)("備考").ToString
            TxtVat.Text = ds1.Tables(RS).Rows(0)("ＶＡＴ").ToString

            '見積明細情報
            Dim Sql3 As String = ""
            Sql3 += "SELECT * FROM public.t02_mitdt"
            Sql3 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql3 += " AND 見積番号 = '" & EditNo.ToString & "'"
            Sql3 += " AND 見積番号枝番 = '" & EditSuffix.ToString & "'"
            Sql3 += " ORDER BY 行番号"

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
                DgvItemList.Rows(index).Cells("仕入先コード").Value = ds3.Tables(RS).Rows(index)("仕入先コード")
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
                    DgvItemList.Rows(index).Cells("リードタイム単位").Value = tmp2
                End If

                DgvItemList.Rows(index).Cells("備考").Value = ds3.Tables(RS).Rows(index)("備考")
                DgvItemList.Rows(index).Cells("ステータス").Value = "EDIT"
            Next

            '金額計算
            Dim Total As Decimal = 0            '売上金額
            Dim QuoteTotal As Decimal = 0       '見積金額
            Dim PurchaseTotal As Decimal = 0    '仕入金額
            Dim GrossProfit As Decimal = 0      '粗利

            For index As Integer = 0 To DgvItemList.Rows.Count - 1
                PurchaseTotal += DgvItemList.Rows(index).Cells("仕入金額").Value
                Total += DgvItemList.Rows(index).Cells("売上金額").Value
                QuoteTotal += DgvItemList.Rows(index).Cells("見積金額").Value
                GrossProfit += DgvItemList.Rows(index).Cells("粗利額").Value
            Next

            TxtPurchaseTotal.Text = PurchaseTotal.ToString("F0")
            TxtTotal.Text = Total.ToString("F0")
            TxtQuoteTotal.Text = QuoteTotal.ToString("F0")
            TxtGrossProfit.Text = GrossProfit.ToString("F0")
            TxtVatAmount.Text = ((QuoteTotal.ToString("F0") * TxtVat.Text) / 100).ToString("F0")

            '行番号の振り直し
            Dim i As Integer = DgvItemList.Rows.Count()
            Dim No As Integer = 1
            For c As Integer = 0 To i - 1
                DgvItemList.Rows(c).Cells("No").Value = No
                No += 1
            Next c
            TxtItemCount.Text = DgvItemList.Rows.Count()

        Else

            '=======================
            '   見積新規登録
            '=======================

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
            Sql += "FROM public.m80_saiban"
            Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " and 採番キー = '10'"

            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            TxtQuoteNo.Text += ds.Tables(RS).Rows(0)("接頭文字")
            TxtQuoteNo.Text += QuoteDate
            CompanyCode = ds.Tables(RS).Rows(0)("会社コード")
            KeyNo = ds.Tables(RS).Rows(0)("採番キー")
            QuoteNo = ds.Tables(RS).Rows(0)("最新値")
            QuoteNoMin = ds.Tables(RS).Rows(0)("最小値")
            QuoteNoMax = ds.Tables(RS).Rows(0)("最大値")
            TxtQuoteNo.Text += QuoteNo.PadLeft(ds.Tables(RS).Rows(0)("連番桁数"), "0")

            DtpQuote.Value = dtNow.ToShortDateString                                                '見積日
            DtpExpiration.Text = DateAdd("d", CommonConst.DEADLINE_DEFAULT_DAY, DtpQuote.Value)     '見積有効期限

            TxtInput.Text = Input

            SaibanSave()
        End If

        '=======================
        '   参照モード
        '=======================
        If Status Is CommonConst.STATUS_VIEW Then

            LblMode.Text = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                               "ViewMode",
                               "参照モード")

            DtpQuote.Enabled = False
            DtpExpiration.Enabled = False
            TxtCustomerCode.Enabled = False
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
            BtnProof.Location = New Point(657, 509)
            BtnCodeSearch.Enabled = False

            BtnQuoteRequest.Visible = True
            BtnQuoteRequest.Location = New Point(828, 509)
            BtnQuote.Visible = True
            BtnQuote.Location = New Point(1004, 509)

        ElseIf Status Is CommonConst.STATUS_PRICE Then
            '=======================
            '   仕入単価入力モード
            '=======================
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
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
            'グリッドの入力項目を制限
            DgvItemList.Columns("仕入区分").ReadOnly = True
            DgvItemList.Columns("メーカー").ReadOnly = True
            DgvItemList.Columns("品名").ReadOnly = True
            DgvItemList.Columns("型式").ReadOnly = True
            DgvItemList.Columns("数量").ReadOnly = True
            DgvItemList.Columns("単位").ReadOnly = True
            DgvItemList.Columns("仕入先コード").ReadOnly = True
            DgvItemList.Columns("仕入先").ReadOnly = True

        ElseIf Status Is CommonConst.STATUS_EDIT Then
            '=======================
            '   編集モード
            '=======================
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "EditMode"
            Else
                LblMode.Text = "編集モード"
            End If

        ElseIf Status Is CommonConst.STATUS_ADD Then
            '=======================
            '   新規登録モード
            '=======================
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "NewRegistrationMode"
            Else
                LblMode.Text = "新規登録モード"
            End If
        End If


        LoadFlg = True

    End Sub

    Private Sub SaibanSave()
        Dim dtToday As String = UtilClass.formatDatetime(DateTime.Now)

        If QuoteNo = QuoteNoMax Then
            QuoteNo = QuoteNoMin
        Else
            QuoteNo = QuoteNo + 1
        End If
        Dim Sql As String = ""

        Sql = ""
        Sql += "UPDATE Public.m80_saiban "
        Sql += "SET "
        Sql += " 最新値 = '" & QuoteNo.ToString & "' "
        Sql += ",更新者 = '" & Input & "' "
        Sql += ",更新日 = '" & dtToday & "' "
        Sql += "WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' "
        Sql += " AND 採番キー = '" & KeyNo.ToString & "' "

        _db.executeDB(Sql)
    End Sub

    '金額自動計算
    Private Sub CellValueChanged(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DgvItemList.CellValueChanged
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        '仕入区分「在庫引当」時処理
        Dim currentColumn As String = DgvItemList.Columns(e.ColumnIndex).Name

        If currentColumn = "仕入区分" Or currentColumn = "メーカー" Or currentColumn = "品名" Or currentColumn = "型式" Then

            '仕入区分が「在庫引当」の場合
            If e.RowIndex >= 0 Then
                If DgvItemList("仕入区分", e.RowIndex).Value = CommonConst.Sire_KBN_Zaiko Then

                    Dim manufactuer As String = DgvItemList("メーカー", e.RowIndex).Value
                    Dim itemName As String = DgvItemList("品名", e.RowIndex).Value
                    Dim spec As String = DgvItemList("型式", e.RowIndex).Value

                    manufactuer = IIf(manufactuer <> Nothing, manufactuer, "")
                    itemName = IIf(itemName <> Nothing, itemName, "")
                    spec = IIf(spec <> Nothing, spec, "")

                    'メーカー、品名、型式があったら
                    If manufactuer <> "" And itemName <> "" And spec <> "" Then

                        Sql = " SELECT "
                        Sql += " t41.* "
                        Sql += " FROM t41_siredt t41 "
                        Sql += " INNER JOIN t40_sirehd t40 "
                        Sql += " ON "
                        Sql += " t41.会社コード = t40.会社コード "
                        Sql += " AND "
                        Sql += " t41.仕入番号 = t40.仕入番号 "
                        Sql += " WHERE "
                        Sql += " t41.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                        Sql += " AND "
                        Sql += " t40.取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'"
                        Sql += " AND "
                        Sql += " t41.メーカー ILIKE '" & manufactuer & "'"
                        Sql += " AND "
                        Sql += " t41.品名 ILIKE '" & itemName & "'"
                        Sql += " AND "
                        Sql += " t41.型式 ILIKE '" & spec & "'"
                        Sql += " ORDER BY 仕入日 DESC "

                        Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

                        If ds.Tables(RS).Rows.Count > 0 Then

                            DgvItemList("仕入単価", e.RowIndex).Value = ds.Tables(RS).Rows(0)("仕入値").ToString()

                        End If

                    End If

                End If

            End If

        End If

        If LoadFlg Then

            TxtTotal.Clear()
            TxtPurchaseTotal.Clear()
            TxtQuoteTotal.Clear()
            TxtGrossProfit.Clear()
            TxtVatAmount.Clear()

            Dim Total As Decimal = 0 '売上金額
            Dim PurchaseTotal As Decimal = 0 '仕入金額
            Dim QuoteTotal As Decimal = 0 '見積金額
            Dim GrossProfit As Decimal = 0 '粗利額
            Dim tmpPurchase As Integer = 0
            Dim tmp As Decimal = 0
            Dim tmp1 As Decimal = 0
            Dim tmp2 As Decimal = 0
            Dim tmp3 As Decimal = 0
            Dim tmp4 As Decimal = 0

            '各項目の属性チェック
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("数量").Value) And (DgvItemList.Rows(e.RowIndex).Cells("数量").Value IsNot Nothing) Then
                MessageBox.Show("Please enter with numeric value.", "Quantity Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                DgvItemList.Rows(e.RowIndex).Cells("数量").Value = Nothing
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value) And (DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value IsNot Nothing) Then
                MessageBox.Show("Please enter with numeric value.", "PurchaseUnitPrice Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value = Nothing
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value) And (DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value IsNot Nothing) Then
                MessageBox.Show("Please enter with numeric value.", "PurchasingCost Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value = Nothing
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("関税率").Value) And (DgvItemList.Rows(e.RowIndex).Cells("関税率").Value IsNot Nothing) Then
                MessageBox.Show("Please enter with numeric value.", "CustomsDutyRate Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                DgvItemList.Rows(e.RowIndex).Cells("関税率").Value = Nothing
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("関税額").Value) And (DgvItemList.Rows(e.RowIndex).Cells("関税額").Value IsNot Nothing) Then
                MessageBox.Show("Please enter with numeric value.", "CustomsDuty Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                DgvItemList.Rows(e.RowIndex).Cells("関税額").Value = Nothing
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("前払法人税率").Value) And (DgvItemList.Rows(e.RowIndex).Cells("前払法人税率").Value IsNot Nothing) Then
                MessageBox.Show("Please enter with numeric value.", "PrepaidCorporateTaxRate Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                DgvItemList.Rows(e.RowIndex).Cells("前払法人税率").Value = Nothing
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value) And (DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value IsNot Nothing) Then
                MessageBox.Show("Please enter with numeric value.", "PrepaidCorporateTaxAmount Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value = Nothing
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("輸送費率").Value) And (DgvItemList.Rows(e.RowIndex).Cells("輸送費率").Value IsNot Nothing) Then
                MessageBox.Show("Please enter with numeric value.", "TransportationCostRate Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                DgvItemList.Rows(e.RowIndex).Cells("輸送費率").Value = Nothing
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value) And (DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value IsNot Nothing) Then
                MessageBox.Show("Please enter with numeric value.", "TransportationCost Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value = Nothing
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("仕入金額").Value) And (DgvItemList.Rows(e.RowIndex).Cells("仕入金額").Value IsNot Nothing) Then
                MessageBox.Show("Please enter with numeric value.", "PurchaseAmount Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                DgvItemList.Rows(e.RowIndex).Cells("仕入金額").Value = Nothing
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("売単価").Value) And (DgvItemList.Rows(e.RowIndex).Cells("売単価").Value IsNot Nothing) Then
                MessageBox.Show("Please enter with numeric value.", "SellingPrice Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                DgvItemList.Rows(e.RowIndex).Cells("売単価").Value = Nothing
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value) And (DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value IsNot Nothing) Then
                MessageBox.Show("Please enter with numeric value.", "SalesAmount Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value = Nothing
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value) And (DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value IsNot Nothing) Then
                MessageBox.Show("Please enter with numeric value.", "QuotetionPrice Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value = Nothing
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("見積金額").Value) And (DgvItemList.Rows(e.RowIndex).Cells("見積金額").Value IsNot Nothing) Then
                MessageBox.Show("Please enter with numeric value.", "QuotetionAmount Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                DgvItemList.Rows(e.RowIndex).Cells("見積金額").Value = Nothing
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value) And (DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value IsNot Nothing) Then
                MessageBox.Show("Please enter with numeric value.", "GrossMargin Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value = Nothing
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("粗利率").Value) And (DgvItemList.Rows(e.RowIndex).Cells("粗利率").Value IsNot Nothing) Then
                MessageBox.Show("Please enter with numeric value.", "GrossMarginRate(%) Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                DgvItemList.Rows(e.RowIndex).Cells("粗利率").Value = Nothing
                Exit Sub
            End If

            Try
                '計算式を各行に適用
                If e.RowIndex > -1 Then

                    '--------------------------
                    '単価入力 or 粗利入力
                    '--------------------------
                    If RbtnUP.Checked Or RbtnGP.Checked Then

                        '仕入単価 <> Nothing
                        '--------------------------
                        If DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value IsNot Nothing Then

                            '数量 <> Nothing
                            If DgvItemList.Rows(e.RowIndex).Cells("数量").Value IsNot Nothing Then
                                DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value = DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value * DgvItemList.Rows(e.RowIndex).Cells("数量").Value
                            End If
                            '関税率 <> Nothing
                            If DgvItemList.Rows(e.RowIndex).Cells("関税率").Value IsNot Nothing Then
                                DgvItemList.Rows(e.RowIndex).Cells("関税額").Value = DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value * DgvItemList.Rows(e.RowIndex).Cells("関税率").Value
                            End If
                            '前払法人税率, 関税額 <> Nothing
                            If DgvItemList.Rows(e.RowIndex).Cells("前払法人税率").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("関税額").Value IsNot Nothing Then
                                tmp = DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value + DgvItemList.Rows(e.RowIndex).Cells("関税額").Value
                                tmp1 = tmp * DgvItemList.Rows(e.RowIndex).Cells("前払法人税率").Value
                                tmp1 = Math.Ceiling(tmp1)
                                DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value = tmp1
                            End If
                            '輸送費率 <> Nothing
                            If DgvItemList.Rows(e.RowIndex).Cells("輸送費率").Value IsNot Nothing Then
                                DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value = DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value * DgvItemList.Rows(e.RowIndex).Cells("輸送費率").Value
                            End If

                        End If

                        '関税額, 前払法人税額, 輸送費額 <> Nothing
                        '--------------------------
                        If DgvItemList.Rows(e.RowIndex).Cells("関税額").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value IsNot Nothing Then
                            '仕入原価 <> Nothing
                            If DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value IsNot Nothing Then
                                DgvItemList.Rows(e.RowIndex).Cells("仕入金額").Value = DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value + (DgvItemList.Rows(e.RowIndex).Cells("関税額").Value + DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value + DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value) * DgvItemList.Rows(e.RowIndex).Cells("数量").Value
                            End If
                            '売単価 <> Nothing
                            If DgvItemList.Rows(e.RowIndex).Cells("売単価").Value IsNot Nothing Then
                                DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value = Math.Ceiling(DgvItemList.Rows(e.RowIndex).Cells("売単価").Value + DgvItemList.Rows(e.RowIndex).Cells("関税額").Value + DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value + DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value)
                            End If
                        End If

                    End If

                    '--------------------------
                    '単価入力
                    '--------------------------
                    If RbtnUP.Checked Then
                        '数量, 売単価 <> Nothing
                        '--------------------------
                        If DgvItemList.Rows(e.RowIndex).Cells("数量").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("売単価").Value IsNot Nothing Then
                            DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value = DgvItemList.Rows(e.RowIndex).Cells("数量").Value * DgvItemList.Rows(e.RowIndex).Cells("売単価").Value

                            '仕入原価 <> Nothing
                            '--------------------------
                            If DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value IsNot Nothing Then
                                DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value = Math.Ceiling(DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value - DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value)
                                '売上金額 <> 0
                                '--------------------------
                                If DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value <> 0 Then
                                    DgvItemList.Rows(e.RowIndex).Cells("粗利率").Value = Format(DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value / DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value * 100, "0.0")
                                End If
                            End If

                        End If
                    ElseIf RbtnGP.Checked Then
                        '--------------------------
                        '粗利入力
                        '--------------------------

                        '数量, 仕入単価, 粗利率 <> Nothing
                        '--------------------------
                        If DgvItemList.Rows(e.RowIndex).Cells("数量").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("粗利率").Value IsNot Nothing Then
                            tmp2 = DgvItemList.Rows(e.RowIndex).Cells("粗利率").Value / 100
                            tmp3 = DgvItemList.Rows(e.RowIndex).Cells("数量").Value - tmp2 * DgvItemList.Rows(e.RowIndex).Cells("数量").Value

                            '仕入原価 <> Nothing
                            '--------------------------
                            If DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value IsNot Nothing Then
                                DgvItemList.Rows(e.RowIndex).Cells("売単価").Value = DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value / tmp3
                                DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value = DgvItemList.Rows(e.RowIndex).Cells("売単価").Value * DgvItemList.Rows(e.RowIndex).Cells("数量").Value
                                DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value = Math.Ceiling(DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value - DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value)
                            End If

                        End If
                    Else
                        '--------------------------
                        '見積入力
                        '--------------------------

                        '見積単価, 売単価, 関税額, 前払法人税額, 輸送費額 <> Nothing
                        '--------------------------
                        If DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("関税額").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value IsNot Nothing Then
                            'If DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("売単価").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("関税額").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value IsNot Nothing Then
                            tmp4 = DgvItemList.Rows(e.RowIndex).Cells("関税額").Value + DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value + DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value
                            DgvItemList.Rows(e.RowIndex).Cells("売単価").Value = DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value - tmp4
                            DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value = DgvItemList.Rows(e.RowIndex).Cells("売単価").Value * DgvItemList.Rows(e.RowIndex).Cells("数量").Value
                            DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value = Math.Ceiling(DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value - DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value)
                            DgvItemList.Rows(e.RowIndex).Cells("粗利率").Value = Format(DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value / DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value * 100, "0.0")
                        End If
                    End If

                    '見積金額算出
                    If DgvItemList.Rows(e.RowIndex).Cells("数量").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value IsNot Nothing Then
                        DgvItemList.Rows(e.RowIndex).Cells("見積金額").Value = DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value * DgvItemList.Rows(e.RowIndex).Cells("数量").Value
                    End If


                    '仕入先コード入力時、仕入先マスタより各項目を抽出
                    If DgvItemList.Rows(e.RowIndex).Cells("仕入先コード").Value IsNot Nothing Then
                        Sql = ""
                        Sql += "SELECT * FROM public.m11_supplier"
                        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                        Sql += " and 仕入先コード = '" & DgvItemList.Rows(e.RowIndex).Cells("仕入先コード").Value.ToString & "'"

                        Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)
                        '仕入先コードにカーソルがあるときだけ各率を再表示
                        If DgvItemList.Columns(e.ColumnIndex).Name = "仕入先コード" Then
                            If reccnt > 0 Then
                                DgvItemList.Rows(e.RowIndex).Cells("仕入先").Value = ds.Tables(RS).Rows(0)("仕入先名").ToString
                                DgvItemList.Rows(e.RowIndex).Cells("間接費率").Value = ds.Tables(RS).Rows(0)("既定間接費率").ToString
                                DgvItemList.Rows(e.RowIndex).Cells("関税率").Value = ds.Tables(RS).Rows(0)("関税率").ToString
                                DgvItemList.Rows(e.RowIndex).Cells("前払法人税率").Value = ds.Tables(RS).Rows(0)("前払法人税率").ToString
                                DgvItemList.Rows(e.RowIndex).Cells("輸送費率").Value = ds.Tables(RS).Rows(0)("輸送費率").ToString
                            Else
                                DgvItemList.Rows(e.RowIndex).Cells("仕入先").Value = ""
                                DgvItemList.Rows(e.RowIndex).Cells("間接費率").Value = 0
                                DgvItemList.Rows(e.RowIndex).Cells("関税率").Value = 0
                                DgvItemList.Rows(e.RowIndex).Cells("前払法人税率").Value = 0
                                DgvItemList.Rows(e.RowIndex).Cells("輸送費率").Value = 0

                            End If
                        End If

                    End If

                End If

                For index As Integer = 0 To DgvItemList.Rows.Count - 1
                    PurchaseTotal += DgvItemList.Rows(index).Cells("仕入金額").Value
                    Total += DgvItemList.Rows(index).Cells("売上金額").Value
                    QuoteTotal += DgvItemList.Rows(index).Cells("見積金額").Value
                    GrossProfit += DgvItemList.Rows(index).Cells("粗利額").Value
                Next

                TxtPurchaseTotal.Text = PurchaseTotal
                TxtTotal.Text = Total.ToString("F0")
                TxtQuoteTotal.Text = QuoteTotal
                TxtGrossProfit.Text = GrossProfit
                TxtVatAmount.Text = ((QuoteTotal * TxtVat.Text) / 100).ToString("F0")


            Catch ex As OverflowException

                Throw ex

            End Try

        End If
    End Sub

    Private Sub RbtnGP_CheckedChanged(sender As Object, e As EventArgs) Handles RbtnGP.CheckedChanged, RbtnUP.CheckedChanged, RbtnQuote.CheckedChanged

        If RbtnUP.Checked Then
            '単価入力選択時
            DgvItemList.Columns("粗利率").ReadOnly = True
            DgvItemList.Columns("売単価").ReadOnly = False
            DgvItemList.Columns("見積単価").ReadOnly = True
            DgvItemList.Columns("粗利率").DefaultCellStyle.BackColor = Color.LightGray
            DgvItemList.Columns("売単価").DefaultCellStyle.BackColor = Color.White
            DgvItemList.Columns("見積単価").DefaultCellStyle.BackColor = Color.LightGray
        ElseIf RbtnGP.Checked Then
            '粗利入力
            DgvItemList.Columns("売単価").ReadOnly = True
            DgvItemList.Columns("粗利率").ReadOnly = False
            DgvItemList.Columns("見積単価").ReadOnly = True
            DgvItemList.Columns("売単価").DefaultCellStyle.BackColor = Color.LightGray
            DgvItemList.Columns("粗利率").DefaultCellStyle.BackColor = Color.White
            DgvItemList.Columns("見積単価").DefaultCellStyle.BackColor = Color.LightGray
        Else
            '見積単価入力
            DgvItemList.Columns("売単価").ReadOnly = True
            DgvItemList.Columns("粗利率").ReadOnly = True
            DgvItemList.Columns("見積単価").ReadOnly = False
            DgvItemList.Columns("売単価").DefaultCellStyle.BackColor = Color.LightGray
            DgvItemList.Columns("粗利率").DefaultCellStyle.BackColor = Color.LightGray
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
        TxtVatAmount.Clear()

        Dim Total As Integer = 0
        Dim QuoteTotal As Integer = 0
        Dim PurchaseTotal As Integer = 0
        Dim GrossProfit As Decimal = 0

        For c As Integer = 0 To DgvItemList.Rows.Count - 1
            PurchaseTotal += DgvItemList.Rows(c).Cells("仕入金額").Value
            Total += DgvItemList.Rows(c).Cells("売上金額").Value
            QuoteTotal += DgvItemList.Rows(c).Cells("見積金額").Value
            GrossProfit += DgvItemList.Rows(c).Cells("粗利額").Value
        Next
        TxtPurchaseTotal.Text = PurchaseTotal
        TxtTotal.Text = Total
        TxtQuoteTotal.Text = QuoteTotal
        TxtGrossProfit.Text = GrossProfit
        TxtVatAmount.Text = ((QuoteTotal * TxtVat.Text) / 100).ToString("F0")


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
            'グリッドに何もないときは処理しない
            If DgvItemList.CurrentCell Is Nothing Then
                Exit Sub
            End If

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
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '得意先コードをダブルクリック時得意先マスタから得意先を取得
    Private Sub TxtCustomerCode_DoubleClick(sender As Object, e As EventArgs) Handles TxtCustomerCode.DoubleClick
        Dim openForm As Form = Nothing
        openForm = New CustomerSearch(_msgHd, _db, _langHd, Me) '処理選択
        openForm.ShowDialog(Me)
        openForm.Dispose()

        Read_Customer() '得意先マスタ読み込み

    End Sub

    '営業担当者コードをダブルクリック時ユーザマスタからユーザID、氏名を取得
    Private Sub TxtSales_DoubleClick(sender As Object, e As EventArgs) Handles TxtSales.DoubleClick
        Dim openForm As Form = Nothing
        openForm = New SalesSearch(_msgHd, _db, _langHd, Me) '処理選択
        openForm.Show(Me)
        Me.Enabled = False
    End Sub

    'Dgv内での検索
    Private Sub DgvItemList_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) _
     Handles DgvItemList.CellDoubleClick
        '行ヘッダークリック時は無効
        If e.RowIndex < 0 Then
            Exit Sub
        End If

        '仕入単価入力時は各項目変更しないので検索も起動しない
        If Status Is CommonConst.STATUS_PRICE Then
            Exit Sub
        End If

        Dim selectColumn As String = DgvItemList.Columns(e.ColumnIndex).Name

        Dim Maker As String = DgvItemList("メーカー", e.RowIndex).Value
        Dim Item As String = DgvItemList("品名", e.RowIndex).Value
        Dim Model As String = DgvItemList("型式", e.RowIndex).Value

        If selectColumn = "メーカー" Or selectColumn = "品名" Or selectColumn = "型式" Then
            '各項目チェック
            If selectColumn = "型式" And (Maker Is Nothing And Item Is Nothing) Then
                'メーカー、品名を入力してください。
                _msgHd.dspMSG("chkManufacturerItemNameError", frmC01F10_Login.loginValue.Language)
                Return

            ElseIf selectColumn = "品名" And (Maker Is Nothing) Then
                'メーカーを入力してください。
                _msgHd.dspMSG("chkManufacturerError", frmC01F10_Login.loginValue.Language)
                Return
            End If

            Dim openForm As Form = Nothing
            openForm = New MakerSearch(_msgHd, _db, Me, e.RowIndex, e.ColumnIndex, Maker, Item, Model, selectColumn, CommonConst.STATUS_ADD)   '処理選択
            openForm.Show(Me)
            Me.Enabled = False
        End If


        If selectColumn = "仕入先" Then '仕入先検索
            Dim openForm As Form = Nothing
            openForm = New SupplierSearch(_msgHd, _db, _langHd, e.RowIndex, Me)
            openForm.Show(Me)
            Me.Enabled = False
        End If

    End Sub

    Private Sub BtnUp_Click(sender As Object, e As EventArgs) Handles BtnUp.Click
        'グリッドに何もないときは処理しない
        If DgvItemList.CurrentCell Is Nothing Then
            Exit Sub
        End If

        If DgvItemList.CurrentCell.RowIndex > 0 Then
            DgvItemList.CurrentCell = DgvItemList(DgvItemList.CurrentCell.ColumnIndex, DgvItemList.CurrentCell.RowIndex - 1)
        End If
    End Sub

    Private Sub BtnDown_Click(sender As Object, e As EventArgs) Handles BtnDown.Click
        'グリッドに何もないときは処理しない
        If DgvItemList.CurrentCell Is Nothing Then
            Exit Sub
        End If

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

    '登録ボタン押下時
    Private Sub BtnRegistration_Click(sender As Object, e As EventArgs) Handles BtnRegistration.Click

        '項目チェック
        Dim strMessage As String = ""    'メッセージ本文
        Dim strMessageTitle As String = ""      'メッセージタイトル
        ''得意先は必須入力としましょう
        If TxtCustomerCode.Text = "" Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                strMessage = "Please enter Customer Code. "
                strMessageTitle = "CustomerCode Error"
            Else
                strMessage = "得意先を入力してください。"
                strMessageTitle = "得意先入力エラー"
            End If
            Dim result As DialogResult = MessageBox.Show(strMessage, strMessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        '明細行がゼロ件の場合はエラーとする
        If DgvItemList.Rows.Count = 0 Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                strMessage = "Please enter the details. "
                strMessageTitle = "details Error"
            Else
                strMessage = "明細を入力してください。"
                strMessageTitle = "明細入力エラー"
            End If
            Dim result As DialogResult = MessageBox.Show(strMessage, strMessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        '仕入区分が「在庫引当」以外かつ仕入先と数量がなかったらエラーで戻す
        For i As Integer = 0 To DgvItemList.RowCount - 1
            If DgvItemList.Rows(i).Cells("仕入区分").Value <> CommonConst.Sire_KBN_Zaiko And
                DgvItemList.Rows(i).Cells("仕入先コード").Value Is Nothing Or
                DgvItemList.Rows(i).Cells("数量").Value Is Nothing Then

                '対象データがないメッセージを表示
                _msgHd.dspMSG("chkQuoteInputError", frmC01F10_Login.loginValue.Language)

                Exit Sub
            Else
                If DgvItemList.Rows(i).Cells("数量").Value = 0 Then
                    '対象データがないメッセージを表示
                    _msgHd.dspMSG("chkQuantityError", frmC01F10_Login.loginValue.Language)

                    Exit Sub
                End If

            End If
        Next

        'TxtVatの属性チェック
        If Not IsNumeric(TxtVat.Text) Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                strMessage = "Please enter with 0<= VAT <100. "
                strMessageTitle = "VAT Error"
            Else
                strMessage = "0<= VAT <100 の範囲で入力してください。"
                strMessageTitle = "ＶＡＴ入力エラー"
            End If
            Dim result As DialogResult = MessageBox.Show(strMessage, strMessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If CDec(TxtVat.Text) < 0 Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                strMessage = "Please enter with 0<= VAT <100. "
                strMessageTitle = "VAT Error"
            Else
                strMessage = "0<= VAT <100 の範囲で入力してください。"
                strMessageTitle = "ＶＡＴ入力エラー"
            End If
            Dim result As DialogResult = MessageBox.Show(strMessage, strMessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If CDec(TxtVat.Text) >= 100 Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                strMessage = "Please enter with 0<= VAT <100. "
                strMessageTitle = "VAT Error"
            Else
                strMessage = "0<= VAT <100 の範囲で入力してください。"
                strMessageTitle = "ＶＡＴ入力エラー"
            End If
            Dim result As DialogResult = MessageBox.Show(strMessage, strMessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        '有効期限として指定できる日付は見積日以降
        If DtpQuote.Value > DtpExpiration.Value Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                strMessage = "Please enter with QuoteDate <= ExpirationDate. "
                strMessageTitle = "ExpirationDate Error"
            Else
                strMessage = "見積有効期限は見積日以降で入力してください。"
                strMessageTitle = "見積有効期限入力エラー"
            End If
            Dim result As DialogResult = MessageBox.Show(strMessage, strMessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        '数値チェック（DBの桁数を超えたらエラーで返す
        If TxtQuoteTotal.Text > CommonConst.DIGIT_CHECK Or TxtPurchaseTotal.Text > CommonConst.DIGIT_CHECK Or TxtTotal.Text > CommonConst.DIGIT_CHECK Then
            _msgHd.dspMSG("chkPriceError", frmC01F10_Login.loginValue.Language)
            Exit Sub
        End If

        Dim dtToday As DateTime = DateTime.Now
        Dim strToday As String = UtilClass.formatDatetime(dtToday)

        '仕入単価入力
        If Status Is CommonConst.STATUS_PRICE Then
            Try
                Dim Sql1 As String = ""
                Sql1 = ""
                Sql1 += "UPDATE Public.t01_mithd "
                Sql1 += "SET "
                Sql1 += "得意先コード = '" & TxtCustomerCode.Text & "' "
                Sql1 += ",得意先名 = '" & TxtCustomerName.Text & "' "
                Sql1 += ",得意先郵便番号 = '" & TxtPostalCode.Text & "' "
                Sql1 += ",得意先住所 = '" & TxtAddress1.Text & "' "
                Sql1 += ",得意先電話番号 = '" & TxtTel.Text & "' "
                Sql1 += ",得意先ＦＡＸ = '" & TxtFax.Text & "' "
                Sql1 += ",得意先担当者役職 = '" & TxtPosition.Text & "' "
                Sql1 += ",得意先担当者名 = '" & TxtPerson.Text & "' "
                Sql1 += ",見積日 = '" & UtilClass.strFormatDate(DtpQuote.Text) & "' "
                Sql1 += ",見積有効期限 = '" & UtilClass.strFormatDate(DtpExpiration.Text) & "' "
                Sql1 += ",支払条件 = '" & TxtPaymentTerms.Text & "' "
                Sql1 += ",見積金額 = " & formatStringToNumber(TxtQuoteTotal.Text)
                Sql1 += ",仕入金額 = " & formatStringToNumber(TxtPurchaseTotal.Text)
                Sql1 += ",粗利額 = " & formatStringToNumber(TxtGrossProfit.Text)
                Sql1 += ",営業担当者コード = '" & TxtSales.Tag & "' "
                Sql1 += ",営業担当者 = '" & TxtSales.Text & "' "
                Sql1 += ",入力担当者コード = '" & frmC01F10_Login.loginValue.TantoCD & "' "
                Sql1 += ",入力担当者 = '" & TxtInput.Text & "' "
                Sql1 += ",備考 = '" & RevoveChars(TxtRemarks.Text) & "' "
                Sql1 += ",ＶＡＴ = " & formatStringToNumber(TxtVat.Text)
                Sql1 += ",登録日 = '" & UtilClass.strFormatDate(DtpRegistration.Text) & "' "
                Sql1 += ",更新日 = '" & strToday & "' "
                Sql1 += ",更新者 = '" & Input & "' "
                Sql1 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql1 += " AND 見積番号 = '" & TxtQuoteNo.Text & "' "
                Sql1 += " AND 見積番号枝番 = '" & TxtSuffixNo.Text & "' "
                _db.executeDB(Sql1)

                Dim Sql2 As String = ""
                For index As Integer = 0 To DgvItemList.Rows.Count - 1
                    Sql2 = ""
                    Sql2 += "UPDATE Public.t02_mitdt "
                    Sql2 += "SET "
                    Sql2 += "仕入区分 = '" & DgvItemList.Rows(index).Cells("仕入区分").Value.ToString & "' "
                    Sql2 += ",メーカー = '" & DgvItemList.Rows(index).Cells("メーカー").Value.ToString & "' "
                    Sql2 += ",品名 = '" & DgvItemList.Rows(index).Cells("品名").Value.ToString & "' "
                    Sql2 += ",型式 = '" & DgvItemList.Rows(index).Cells("型式").Value.ToString & "' "
                    If DgvItemList.Rows(index).Cells("数量").Value IsNot Nothing Then
                        Sql2 += ",数量 = " & DgvItemList.Rows(index).Cells("数量").Value.ToString
                    Else
                        Sql2 += ",数量 = 0 "
                    End If
                    Sql2 += ",単位 = '" & DgvItemList.Rows(index).Cells("単位").Value.ToString & "' "
                    Sql2 += ",仕入先コード = '" & DgvItemList.Rows(index).Cells("仕入先コード").Value.ToString & "' "
                    Sql2 += ",仕入先名称 = '" & DgvItemList.Rows(index).Cells("仕入先").Value.ToString & "' "
                    If DgvItemList.Rows(index).Cells("仕入単価").Value IsNot Nothing Then
                        Sql2 += ",仕入単価 = " & formatStringToNumber(DgvItemList.Rows(index).Cells("仕入単価").Value.ToString)
                    Else
                        Sql2 += ",仕入単価 = 0"
                    End If

                    If DgvItemList.Rows(index).Cells("仕入金額").Value IsNot Nothing Then
                        Sql2 += ",仕入金額 = " & formatStringToNumber(DgvItemList.Rows(index).Cells("仕入金額").Value.ToString)
                    Else
                        Sql2 += ",仕入金額 = 0"
                    End If
                    If DgvItemList.Rows(index).Cells("仕入原価").Value IsNot Nothing Then
                        Sql2 += ",仕入原価 = " & formatStringToNumber(DgvItemList.Rows(index).Cells("仕入原価").Value.ToString)
                    Else
                        Sql2 += ",仕入原価 = 0"
                    End If
                    If DgvItemList.Rows(index).Cells("関税率").Value IsNot Nothing Then
                        Sql2 += ",関税率 = " & formatStringToNumber(DgvItemList.Rows(index).Cells("関税率").Value.ToString)
                    Else
                        Sql2 += ",関税率 = 0"
                    End If
                    If DgvItemList.Rows(index).Cells("関税額").Value IsNot Nothing Then
                        Sql2 += ",関税額 = " & formatStringToNumber(DgvItemList.Rows(index).Cells("関税額").Value.ToString)
                    Else
                        Sql2 += ",関税額 = 0"
                    End If
                    If DgvItemList.Rows(index).Cells("前払法人税率").Value IsNot Nothing Then
                        Sql2 += ",前払法人税率 = " & formatStringToNumber(DgvItemList.Rows(index).Cells("前払法人税率").Value.ToString)
                    Else
                        Sql2 += ",前払法人税率 = 0"
                    End If
                    If DgvItemList.Rows(index).Cells("前払法人税額").Value IsNot Nothing Then
                        Sql2 += ",前払法人税額 = " & formatStringToNumber(DgvItemList.Rows(index).Cells("前払法人税額").Value.ToString)
                    Else
                        Sql2 += ",前払法人税額 = 0"
                    End If
                    If DgvItemList.Rows(index).Cells("輸送費率").Value IsNot Nothing Then
                        Sql2 += ",輸送費率 = " & formatStringToNumber(DgvItemList.Rows(index).Cells("輸送費率").Value.ToString)
                    Else
                        Sql2 += ",輸送費率 = 0"
                    End If
                    If DgvItemList.Rows(index).Cells("輸送費額").Value IsNot Nothing Then
                        Sql2 += ",輸送費額 = " & formatStringToNumber(DgvItemList.Rows(index).Cells("輸送費額").Value.ToString)
                    Else
                        Sql2 += ",輸送費額 = 0"
                    End If
                    If DgvItemList.Rows(index).Cells("売単価").Value IsNot Nothing Then
                        Sql2 += ",売単価 = " & formatStringToNumber(DgvItemList.Rows(index).Cells("売単価").Value.ToString)
                    Else
                        Sql2 += ",売単価 = 0"
                    End If
                    If DgvItemList.Rows(index).Cells("売上金額").Value IsNot Nothing Then
                        Sql2 += ",売上金額 = " & formatStringToNumber(DgvItemList.Rows(index).Cells("売上金額").Value.ToString)
                    Else
                        Sql2 += ",売上金額 = 0"
                    End If

                    If DgvItemList.Rows(index).Cells("見積単価").Value IsNot Nothing Then
                        Sql2 += ",見積単価 = " & formatStringToNumber(DgvItemList.Rows(index).Cells("見積単価").Value.ToString)
                    Else
                        Sql2 += ",見積単価 = 0"
                    End If

                    If DgvItemList.Rows(index).Cells("見積金額").Value IsNot Nothing Then
                        Sql2 += ",見積金額 = " & formatStringToNumber(DgvItemList.Rows(index).Cells("見積金額").Value.ToString)
                    Else
                        Sql2 += ",見積金額 = 0"
                    End If

                    If DgvItemList.Rows(index).Cells("粗利額").Value IsNot Nothing Then
                        Sql2 += ",粗利額 = " & formatStringToNumber(DgvItemList.Rows(index).Cells("粗利額").Value.ToString)
                    Else
                        Sql2 += ",粗利額 = 0"
                    End If
                    If DgvItemList.Rows(index).Cells("粗利率").Value IsNot Nothing Then
                        Sql2 += ",粗利率 = " & formatStringToNumber(DgvItemList.Rows(index).Cells("粗利率").Value.ToString)
                    Else
                        Sql2 += ",粗利率 = 0"
                    End If
                    Sql2 += ",リードタイム = '" & RevoveChars(DgvItemList.Rows(index).Cells("リードタイム").Value.ToString) & "' "
                    If DgvItemList.Rows(index).Cells("リードタイム単位").Value IsNot Nothing And Not IsNumeric(DgvItemList.Rows(index).Cells("リードタイム単位").Value) Then
                        Sql2 += ",リードタイム単位 = " & DgvItemList.Rows(index).Cells("リードタイム単位").Value.ToString
                    Else
                        Sql2 += ",リードタイム単位 = 1"
                    End If

                    Sql2 += ",備考 = '" & RevoveChars(DgvItemList.Rows(index).Cells("備考").Value.ToString) & "' "
                    Sql2 += ",更新者 = '" & Input & "' "
                    Sql2 += ",登録日 = '" & UtilClass.strFormatDate(DtpRegistration.Text) & "' "
                    Sql2 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                    Sql2 += " AND 見積番号 = '" & TxtQuoteNo.Text & "' "
                    Sql2 += " AND 見積番号枝番 = '" & TxtSuffixNo.Text & "' "
                    Sql2 += " AND 行番号 =" & DgvItemList.Rows(index).Cells("No").Value.ToString

                    _db.executeDB(Sql2)
                Next
            Catch ue As UsrDefException
                ue.dspMsg()
                Throw ue
            Catch ex As Exception
                'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
            End Try

        Else
            '仕入単価入力以外

            Try
                Dim Sql1 As String = ""
                Sql1 = ""
                Sql1 += "INSERT INTO "
                Sql1 += "Public."
                Sql1 += "t01_mithd("
                Sql1 += "会社コード, 見積番号, 見積番号枝番, 得意先コード, 得意先名, 得意先郵便番号, 得意先住所"
                Sql1 += ", 得意先電話番号, 得意先ＦＡＸ, 得意先担当者役職, 得意先担当者名, 見積日, 見積有効期限"
                Sql1 += ", 支払条件, 見積金額, 仕入金額, 粗利額, 営業担当者コード, 営業担当者, 入力担当者コード"
                Sql1 += ", 入力担当者, 備考, ＶＡＴ, 取消区分, 登録日, 更新日, 更新者)"
                Sql1 += " VALUES('" & frmC01F10_Login.loginValue.BumonCD & "'"  '会社コード
                Sql1 += ",'" & TxtQuoteNo.Text & "'"                            '見積番号
                Sql1 += ", '" & TxtSuffixNo.Text & "'"                          '見積番号枝番
                Sql1 += ", '" & TxtCustomerCode.Text & "'"                      '得意先コード
                Sql1 += ", '" & TxtCustomerName.Text & "'"                      '得意先名
                Sql1 += ", '" & TxtPostalCode.Text & "'"                        '得意先郵便番号
                Sql1 += ", '" & TxtAddress1.Text & "'"                          '得意先住所
                Sql1 += ", '" & TxtTel.Text & "'"                               '得意先電話番号
                Sql1 += ", '" & TxtFax.Text & "'"                               '得意先ＦＡＸ
                Sql1 += ", '" & TxtPosition.Text & "'"                          '得意先担当者役職
                Sql1 += ", '" & TxtPerson.Text & "'"                            '得意先担当者名
                Sql1 += ", '" & UtilClass.strFormatDate(DtpQuote.Text) & "'"              '見積日
                Sql1 += ", '" & UtilClass.strFormatDate(DtpExpiration.Text) & "'"         '見積有効期限
                Sql1 += ", '" & TxtPaymentTerms.Text & "'"                      '支払条件
                Sql1 += ", " & UtilClass.formatNumber(TxtQuoteTotal.Text)         '見積金額
                Sql1 += ", " & UtilClass.formatNumber(TxtPurchaseTotal.Text)      '仕入金額
                Sql1 += ", " & UtilClass.formatNumber(TxtGrossProfit.Text)        '粗利額
                Sql1 += ", '" & TxtSales.Tag & "'"                              '営業担当者コード
                Sql1 += ", '" & TxtSales.Text & "'"                             '営業担当者
                Sql1 += ", '" & frmC01F10_Login.loginValue.TantoCD & "' "       '入力担当者コード
                Sql1 += ", '" & TxtInput.Text & "'"                             '入力担当者
                Sql1 += ", '" & RevoveChars(TxtRemarks.Text) & "'"              '備考
                Sql1 += ",'"                                                     'ＶＡＴ
                If TxtVat.Text = Nothing Then
                    Sql1 += "0"
                Else
                    Sql1 += UtilClass.formatNumber(TxtVat.Text)
                End If
                Sql1 += "',0"                                                    '取消区分
                Sql1 += ", '" & UtilClass.strFormatDate(DtpRegistration.Text) & "'"       '登録日
                Sql1 += ", '" & strToday & "'"                                   '更新日
                Sql1 += ", '" & Input & "'"                                    '更新者
                Sql1 += ")"

                _db.executeDB(Sql1)

                Dim Sql2 As String = ""
                For index As Integer = 0 To DgvItemList.Rows.Count - 1
                    Sql2 = ""
                    Sql2 += "INSERT INTO Public.t02_mitdt("
                    Sql2 += "会社コード, 見積番号, 見積番号枝番, 行番号, 仕入区分, メーカー, 品名, 型式, 数量, 単位, 仕入先コード, 仕入先名称, 仕入単価, 仕入原価, 関税率, 関税額, 前払法人税率, 前払法人税額, 輸送費率, 輸送費額, 仕入金額, 売単価, 売上金額, 見積単価, 見積金額, 粗利額, 粗利率, リードタイム, リードタイム単位, 備考, 更新者, 登録日)"
                    Sql2 += " VALUES("
                    Sql2 += " '" & frmC01F10_Login.loginValue.BumonCD & "'"     '会社コード
                    Sql2 += " , '" & TxtQuoteNo.Text & "'"                      '見積番号
                    Sql2 += " , '" & TxtSuffixNo.Text & "'"                     '見積番号枝番
                    If DgvItemList.Rows(index).Cells("No").Value IsNot Nothing Then '行番号
                        Sql2 += " ," & DgvItemList.Rows(index).Cells("No").Value.ToString
                    Else
                        Sql2 += " ,''"
                    End If
                    If DgvItemList.Rows(index).Cells("仕入区分").Value IsNot Nothing Then   '仕入区分
                        Sql2 += " ,'" & DgvItemList.Rows(index).Cells("仕入区分").Value.ToString & "'"
                    Else
                        Sql2 += " ,''"
                    End If
                    If DgvItemList.Rows(index).Cells("メーカー").Value IsNot Nothing Then   'メーカー
                        Sql2 += " ,'" & DgvItemList.Rows(index).Cells("メーカー").Value.ToString & "'"
                    Else
                        Sql2 += " ,''"
                    End If
                    If DgvItemList.Rows(index).Cells("品名").Value IsNot Nothing Then       '品名
                        Sql2 += " ,'" & DgvItemList.Rows(index).Cells("品名").Value.ToString & "'"
                    Else
                        Sql2 += " ,''"
                    End If
                    If DgvItemList.Rows(index).Cells("型式").Value IsNot Nothing Then     '型式
                        Sql2 += " ,'" & DgvItemList.Rows(index).Cells("型式").Value.ToString & "'"
                    Else
                        Sql2 += " ,''"
                    End If
                    If DgvItemList.Rows(index).Cells("数量").Value IsNot Nothing Then     '数量
                        Sql2 += " ," & formatStringToNumber(DgvItemList.Rows(index).Cells("数量").Value.ToString)
                    Else
                        Sql2 += " ,0"
                    End If
                    If DgvItemList.Rows(index).Cells("単位").Value IsNot Nothing Then     '単位
                        Sql2 += " ,'" & DgvItemList.Rows(index).Cells("単位").Value.ToString & "'"
                    Else
                        Sql2 += " ,''"
                    End If
                    If DgvItemList.Rows(index).Cells("仕入先コード").Value IsNot Nothing Then    '仕入先コード
                        Sql2 += " ,'" & DgvItemList.Rows(index).Cells("仕入先コード").Value.ToString & "'"
                    Else
                        Sql2 += " ,''"
                    End If
                    If DgvItemList.Rows(index).Cells("仕入先").Value IsNot Nothing Then    '仕入先名称
                        Sql2 += " ,'" & DgvItemList.Rows(index).Cells("仕入先").Value.ToString & "'"
                    Else
                        Sql2 += " ,''"
                    End If
                    If DgvItemList.Rows(index).Cells("仕入単価").Value IsNot Nothing Then   '仕入単価
                        Sql2 += " ," & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("仕入単価").Value.ToString)
                    Else
                        Sql2 += " ,0"
                    End If
                    If DgvItemList.Rows(index).Cells("仕入原価").Value IsNot Nothing Then   '仕入原価
                        Sql2 += " ," & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("仕入原価").Value.ToString)
                    Else
                        Sql2 += " ,0"
                    End If
                    If DgvItemList.Rows(index).Cells("関税率").Value IsNot Nothing Then    '関税率
                        Sql2 += " ," & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("関税率").Value.ToString)
                    Else
                        Sql2 += " ,0"
                    End If
                    If DgvItemList.Rows(index).Cells("関税額").Value IsNot Nothing Then    '関税額
                        Sql2 += " ," & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("関税額").Value.ToString)
                    Else
                        Sql2 += " ,0"
                    End If
                    If DgvItemList.Rows(index).Cells("前払法人税率").Value IsNot Nothing Then     '前払法人税率
                        Sql2 += " ," & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("前払法人税率").Value.ToString)
                    Else
                        Sql2 += " ,0"
                    End If
                    If DgvItemList.Rows(index).Cells("前払法人税額").Value IsNot Nothing Then     '前払法人税額
                        Sql2 += " ," & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("前払法人税額").Value.ToString)
                    Else
                        Sql2 += " ,0"
                    End If
                    If DgvItemList.Rows(index).Cells("輸送費率").Value IsNot Nothing Then       '輸送費率
                        Sql2 += " ," & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("輸送費率").Value.ToString)
                    Else
                        Sql2 += " ,0"
                    End If
                    If DgvItemList.Rows(index).Cells("輸送費額").Value IsNot Nothing Then       '輸送費額
                        Sql2 += " ," & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("輸送費額").Value.ToString)
                    Else
                        Sql2 += " ,0"
                    End If
                    If DgvItemList.Rows(index).Cells("仕入金額").Value IsNot Nothing Then       '仕入金額
                        Sql2 += " ," & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("仕入金額").Value.ToString)
                    Else
                        Sql2 += " ,0"
                    End If
                    If DgvItemList.Rows(index).Cells("売単価").Value IsNot Nothing Then        '売単価
                        Sql2 += " ," & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("売単価").Value.ToString)
                    Else
                        Sql2 += " ,0"
                    End If
                    If DgvItemList.Rows(index).Cells("売上金額").Value IsNot Nothing Then       '売上金額
                        Sql2 += " ," & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("売上金額").Value.ToString)
                    Else
                        Sql2 += " ,0"
                    End If
                    If DgvItemList.Rows(index).Cells("見積単価").Value IsNot Nothing Then       '見積単価
                        Sql2 += " ," & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("見積単価").Value.ToString)
                    Else
                        Sql2 += " ,0"
                    End If
                    If DgvItemList.Rows(index).Cells("見積金額").Value IsNot Nothing Then       '見積金額
                        Sql2 += " ," & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("見積金額").Value.ToString)
                    Else
                        Sql2 += " ,0"
                    End If
                    If DgvItemList.Rows(index).Cells("粗利額").Value IsNot Nothing Then        '粗利額
                        Sql2 += " ," & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("粗利額").Value.ToString)
                    Else
                        Sql2 += " ,0"
                    End If
                    If DgvItemList.Rows(index).Cells("粗利率").Value IsNot Nothing Then        '粗利率
                        Sql2 += " ," & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("粗利率").Value.ToString)
                    Else
                        Sql2 += " ,0"
                    End If
                    If DgvItemList.Rows(index).Cells("リードタイム").Value IsNot Nothing Then 'リードタイム
                        Sql2 += " ,'" & RevoveChars(DgvItemList.Rows(index).Cells("リードタイム").Value.ToString) & "'"
                    Else
                        Sql2 += " ,''"
                    End If
                    Sql2 += " ," & RevoveChars(DgvItemList.Rows(index).Cells("リードタイム単位").Value.ToString)    'リードタイム単位
                    If DgvItemList.Rows(index).Cells("備考").Value IsNot Nothing Then         '備考
                        Sql2 += " ,'" & RevoveChars(DgvItemList.Rows(index).Cells("備考").Value.ToString) & "'"
                    Else
                        Sql2 += " ,''"
                    End If

                    Sql2 += " ,'" & Input & "'"                   '更新者
                    Sql2 += " ,'" & UtilClass.strFormatDate(DtpRegistration.Text) & "'"    '登録日
                    Sql2 += " )"

                    _db.executeDB(Sql2)
                Next
            Catch ue As UsrDefException
                ue.dspMsg()
                Throw ue
            Catch ex As Exception
                'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
            End Try
        End If
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()

    End Sub

    '見積書印刷
    '
    Private Sub BtnQuote_Click(sender As Object, e As EventArgs) Handles BtnQuote.Click
        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        '見積基本情報
        Dim Sql1 As String = ""
        Sql1 += "SELECT "
        Sql1 += "会社コード"
        Sql1 += ", 見積番号 "
        Sql1 += ", 見積番号枝番 "
        Sql1 += ", 見積日 "
        Sql1 += ", 見積有効期限 "
        Sql1 += ", 得意先コード "
        Sql1 += ", 得意先名 "
        Sql1 += ", 得意先担当者名 "
        Sql1 += ", 得意先担当者役職 "
        Sql1 += ", 得意先郵便番号 "
        Sql1 += ", 得意先住所 "
        Sql1 += ", 得意先電話番号 "
        Sql1 += ", 得意先ＦＡＸ "
        Sql1 += ", 営業担当者 "
        Sql1 += ", 入力担当者 "
        Sql1 += ", 支払条件 "
        Sql1 += ", 備考 "
        Sql1 += ", 見積金額 "
        Sql1 += ", ＶＡＴ "
        Sql1 += ", 登録日 "
        Sql1 += ", 更新日 "
        Sql1 += ", 更新者 "
        Sql1 += "FROM public.t01_mithd"
        Sql1 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql1 += " AND 見積番号 = '" & EditNo.ToString & "'"
        Sql1 += " AND 見積番号枝番 ='" & EditSuffix.ToString & "'"
        Dim reccnt As Integer = 0
        Dim ds1 = _db.selectDB(Sql1, RS, reccnt)

        Dim SuffixMax As Integer = 0

        CompanyCode = ds1.Tables(RS).Rows(0)("会社コード")

        Dim CmnData = ds1.Tables(RS).Rows(0)


        '見積明細情報
        Dim Sql3 As String = ""
        Sql3 += "SELECT "
        Sql3 += "仕入区分 "
        Sql3 += ", メーカー "
        Sql3 += ", 品名 "
        Sql3 += ", 型式 "
        Sql3 += ", 数量 "
        Sql3 += ", 単位 "
        Sql3 += ", 仕入先コード "
        Sql3 += ", 仕入先名称 "
        Sql3 += ", 仕入単価 "
        Sql3 += ", 間接費率 "
        Sql3 += ", 間接費 "
        Sql3 += ", 仕入金額 "
        Sql3 += ", 売単価 "
        Sql3 += ", 売上金額 "
        Sql3 += ", 粗利額 "
        Sql3 += ", 粗利率 "
        Sql3 += ", リードタイム "
        Sql3 += ", リードタイム単位 "
        Sql3 += ", 見積単価 "
        Sql3 += ", 見積金額 "
        Sql3 += ", 備考 "
        Sql3 += ", 登録日 "
        Sql3 += "FROM public.t02_mitdt"
        Sql3 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql3 += " AND 見積番号 = '" & EditNo.ToString & "'"
        Sql3 += " AND 見積番号枝番 ='" & EditSuffix.ToString & "'"
        Sql3 += " ORDER BY 行番号"

        Dim ds3 = _db.selectDB(Sql3, RS, reccnt)

        Dim Sql4 As String = ""
        Sql4 += "SELECT "
        Sql4 += "会社コード "
        Sql4 += ", 会社名 "
        Sql4 += ", 会社略称 "
        Sql4 += ", 郵便番号 "
        Sql4 += ", 住所１ "
        Sql4 += ", 住所２ "
        Sql4 += ", 住所３ "
        Sql4 += ", 電話番号 "
        Sql4 += ", ＦＡＸ番号 "
        Sql4 += ", 代表者役職 "
        Sql4 += ", 代表者名 "
        Sql4 += ", 表示順 "
        Sql4 += ", 備考 "
        Sql4 += ", 銀行コード "
        Sql4 += ", 支店コード "
        Sql4 += ", 預金種目 "
        Sql4 += ", 口座番号 "
        Sql4 += ", 口座名義 "
        Sql4 += ", 更新者 "
        Sql4 += ", 更新日 "
        Sql4 += "FROM public.m01_company"
        Sql4 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

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
            sOutFile = sOutPath & "\" & CmnData("見積番号") & "-" & CmnData("見積番号枝番") & ".xlsx"



            app = New Excel.Application()
            book = app.Workbooks.Add(sHinaFile)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet)

            '↓この辺マジックナンバーが多いので、後できっちり見直す必要あり
            sheet.Range("D1").Value = ds4.Tables(RS).Rows(0)("会社名")
            sheet.Range("D2").Value = ds4.Tables(RS).Rows(0)("住所１") & " " & ds4.Tables(RS).Rows(0)("郵便番号")
            sheet.Range("D3").Value = ds4.Tables(RS).Rows(0)("住所２") & " " & ds4.Tables(RS).Rows(0)("住所３")
            sheet.Range("D4").Value = "telp. " & ds4.Tables(RS).Rows(0)("電話番号") & " Fax." & ds4.Tables(RS).Rows(0)("ＦＡＸ番号")

            sheet.Range("E8").Value = CmnData("得意先名") '得意先名
            sheet.Range("E9").Value = CmnData("得意先担当者役職") & " " & CmnData("得意先担当者名") '得意先担当者
            sheet.Range("E11").Value = CmnData("得意先電話番号") '電話番号
            sheet.Range("E12").Value = CmnData("得意先ＦＡＸ") 'FAX

            sheet.Range("S8").Value = CmnData("見積番号") & "-" & CmnData("見積番号枝番")    '見積番号
            sheet.Range("S9").Value = CmnData("見積日").ToShortDateString() '見積日

            sheet.Range("H27").Value = CmnData("支払条件") '支払条件
            sheet.Range("H28").Value = CmnData("得意先名")                       '納品先（得意先名
            sheet.Range("H29").Value = CmnData("見積有効期限").ToShortDateString() '見積有効期限
            sheet.Range("H30").Value = CmnData("備考") '備考

            sheet.Range("S11").Value = CmnData("営業担当者") '営業担当者
            sheet.Range("S12").Value = CmnData("入力担当者") '入力担当者

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

            Dim totalPrice As Long = 0
            Dim Sql5 As String = ""
            Dim tmp1 As String = ""
            Dim cell As String

            For index As Integer = 0 To ds3.Tables(RS).Rows.Count - 1


                cell = "A" & currentCnt
                sheet.Range(cell).Value = num
                cell = "C" & currentCnt
                sheet.Range(cell).Value = ds3.Tables(RS).Rows(index)("メーカー") & vbLf & ds3.Tables(RS).Rows(index)("品名") & vbLf & ds3.Tables(RS).Rows(index)("型式") & vbLf & ds3.Tables(RS).Rows(index)("備考")
                cell = "L" & currentCnt
                sheet.Range(cell).Value = ds3.Tables(RS).Rows(index)("数量")
                cell = "N" & currentCnt
                sheet.Range(cell).Value = ds3.Tables(RS).Rows(index)("単位")
                cell = "P" & currentCnt
                sheet.Range(cell).Value = ds3.Tables(RS).Rows(index)("見積単価")
                cell = "T" & currentCnt
                sheet.Range(cell).Value = ds3.Tables(RS).Rows(index)("見積金額")

                totalPrice = totalPrice + ds3.Tables(RS).Rows(index)("見積金額")

                '↑のSQL文でINNER JOIN使えばここで呼び出す必要はない
                Sql5 = ""
                Sql5 += "SELECT * FROM public.m90_hanyo"
                Sql5 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql5 += " AND 固定キー = '" & CommonConst.FIXED_KEY_READTIME & "'"
                Sql5 += " AND 可変キー = '" & ds3.Tables(RS).Rows(index)("リードタイム単位").ToString & "'"
                Dim ds5 = _db.selectDB(Sql5, RS, reccnt)

                cell = "X" & currentCnt
                tmp1 = ""
                tmp1 += "ABOUT" & Environment.NewLine & ds3.Tables(RS).Rows(index)("リードタイム")
                If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                    tmp1 += ds5.Tables(RS).Rows(0)("文字２")
                Else
                    tmp1 += ds5.Tables(RS).Rows(0)("文字１")
                End If
                sheet.Range(cell).Value = tmp1



                currentCnt = currentCnt + 1
                num = num + 1
            Next

            'CmnData(18) = VAT
            cell = "T" & lstRow + 1
            sheet.Range(cell).Value = totalPrice
            cell = "T" & lstRow + 2
            sheet.Range(cell).Value = totalPrice * CmnData("ＶＡＴ") * 0.01
            cell = "T" & lstRow + 3
            sheet.Range(cell).Value = totalPrice * CmnData("ＶＡＴ") * 0.01 + totalPrice
            'sheet.Rows.AutoFit()

            app.DisplayAlerts = False 'Microsoft Excelのアラート一旦無効化

            Dim excelChk As Boolean = excelOutput(sOutFile)
            If excelChk = False Then
                Exit Sub
            End If
            book.SaveAs(sOutFile) '書き込み実行

            app.DisplayAlerts = True 'アラート無効化を解除

            app.Visible = True
            'カーソルを砂時計から元に戻す
            Cursor.Current = Cursors.Default

            _msgHd.dspMSG("CreateExcel", frmC01F10_Login.loginValue.Language)

        Catch ex As Exception
            Throw ex

        Finally

        End Try
    End Sub

    '見積依頼書発行
    '
    Private Sub BtnQuoteRequest_Click(sender As Object, e As EventArgs) Handles BtnQuoteRequest.Click
        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        Dim createFlg = False

        '見積基本情報
        Dim Sql1 As String = ""
        Sql1 += "SELECT "
        Sql1 += "会社コード "
        Sql1 += ", 見積番号 "
        Sql1 += ", 見積番号枝番 "
        Sql1 += ", 見積日 "
        Sql1 += ", 見積有効期限 "
        Sql1 += ", 得意先コード "
        Sql1 += ", 得意先名 "
        Sql1 += ", 得意先担当者名 "
        Sql1 += ", 得意先担当者役職 "
        Sql1 += ", 得意先郵便番号 "
        Sql1 += ", 得意先住所 "
        Sql1 += ", 得意先電話番号 "
        Sql1 += ", 得意先ＦＡＸ "
        Sql1 += ", 営業担当者 "
        Sql1 += ", 入力担当者 "
        Sql1 += ", 支払条件 "
        Sql1 += ", 備考 "
        Sql1 += ", 登録日 "
        Sql1 += ", 更新日 "
        Sql1 += ", 更新者 "
        Sql1 += "FROM public.t01_mithd"
        Sql1 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql1 += " AND 見積番号 = '" & EditNo.ToString & "'"
        Sql1 += " AND 見積番号枝番 ='" & EditSuffix.ToString & "'"

        Dim reccnt As Integer = 0
        Dim ds1 = _db.selectDB(Sql1, RS, reccnt)

        CompanyCode = ds1.Tables(RS).Rows(0)("会社コード")
        Dim CmnData = ds1.Tables(RS).Rows(0)


        '見積明細情報
        Dim Sql3 As String = ""
        Sql3 += "SELECT * "
        Sql3 += "FROM public.t02_mitdt"
        Sql3 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql3 += " AND 見積番号 = '" & EditNo.ToString & "'"
        Sql3 += " AND 見積番号枝番 ='" & EditSuffix.ToString & "'"
        Sql3 += " ORDER BY 行番号"

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

            'If supplierChkList(i) = True Then

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

                    sheet.Range("E2").Value = CmnData("見積番号") & "-" & CmnData("見積番号枝番")
                    sheet.Range("E3").Value = System.DateTime.Now.ToShortDateString()
                    sheet.Range("A12").Value = supplierlist(i)
                    sheet.Range("A14").Value = sheet.Range("A14").Value & DateAdd("d", 5, System.DateTime.Today).ToShortDateString() & "."
                    sheet.Range("E19").Value = CmnData("営業担当者")
                    sheet.Range("E20").Value = CmnData("入力担当者")

                    Dim rowCnt As Integer = 0
                    Dim lstRow As Integer = 23

                    For j As Integer = 0 To ds3.tables(RS).rows.count - 1

                        If supplierlist(i) = ds3.tables(RS).rows(j)("仕入先名称").ToString() And ds3.tables(RS).rows(j)("仕入単価") <= 0 Then
                            If lstRow = 23 Then
                                sheet.Range("A" & lstRow).Value = ds3.tables(RS).rows(j)("メーカー") & vbLf & ds3.tables(RS).rows(j)("品名") & vbLf & ds3.tables(RS).rows(j)("型式")
                                sheet.Range("B" & lstRow).Value = ds3.tables(RS).rows(j)("数量") & " " & ds3.tables(RS).rows(j)("単位")
                                'sheet.Rows(lstRow & ":" & lstRow).AutoFit
                            Else
                                Dim cellPos As String = lstRow & ":" & lstRow
                                Dim R As Object
                                cellPos = lstRow & ":" & lstRow
                                R = sheet.Rows(lstRow)
                                R.Copy()
                                R.Insert()

                                If Marshal.IsComObject(R) Then
                                    Marshal.ReleaseComObject(R)
                                End If

                                sheet.Range("A" & lstRow & ":F" & lstRow).Style = sheet.Range("A23:F23").Style

                                sheet.Range("A" & lstRow & ":E" & lstRow).Borders(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous
                                sheet.Range("A" & lstRow & ":E" & lstRow).Borders(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous
                                sheet.Range("A" & lstRow & ":E" & lstRow).Borders(Excel.XlBordersIndex.xlEdgeLeft).LineStyle = Excel.XlLineStyle.xlContinuous
                                sheet.Range("A" & lstRow & ":E" & lstRow).Borders(Excel.XlBordersIndex.xlEdgeRight).LineStyle = Excel.XlLineStyle.xlContinuous
                                sheet.Range("A" & lstRow & ":E" & lstRow).Font.Size = 9
                                sheet.Range("A" & lstRow).Borders(Excel.XlBordersIndex.xlEdgeRight).LineStyle = Excel.XlLineStyle.xlContinuous
                                sheet.Range("B" & lstRow).Borders(Excel.XlBordersIndex.xlEdgeRight).LineStyle = Excel.XlLineStyle.xlContinuous
                                sheet.Range("C" & lstRow).Borders(Excel.XlBordersIndex.xlEdgeRight).LineStyle = Excel.XlLineStyle.xlContinuous
                                sheet.Range("D" & lstRow).Borders(Excel.XlBordersIndex.xlEdgeRight).LineStyle = Excel.XlLineStyle.xlContinuous
                                sheet.Range("E" & lstRow).Borders(Excel.XlBordersIndex.xlEdgeRight).LineStyle = Excel.XlLineStyle.xlContinuous

                                sheet.Range("A" & lstRow).Value = ds3.tables(RS).rows(j)("メーカー") & vbLf & ds3.tables(RS).rows(j)("品名") & vbLf & ds3.tables(RS).rows(j)("型式")
                                sheet.Range("B" & lstRow).Value = ds3.tables(RS).rows(j)("数量") & " " & ds3.tables(RS).rows(j)("単位")
                                sheet.Range("B" & lstRow).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter

                                'sheet.Rows(lstRow & ":" & lstRow).AutoFit

                            End If
                            lstRow = lstRow + 1

                        End If
                    Next

                    app.DisplayAlerts = False 'Microsoft Excelのアラート一旦無効化

                    Dim excelChk As Boolean = excelOutput(sOutFile)
                    If excelChk = False Then
                        Exit Sub
                    End If
                    book.SaveAs(sOutFile) '書き込み実行

                    app.DisplayAlerts = True 'アラート無効化を解除

                    app.Visible = True
                    'カーソルを砂時計から元に戻す
                    Cursor.Current = Cursors.Default

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
            'End If

            If (createFlg = True) Then
                _msgHd.dspMSG("CreateExcel", frmC01F10_Login.loginValue.Language)
            End If

        Next

    End Sub

    'プルーフ発行処理
    '
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
        Dim PurchaseTotal As String = TxtPurchaseTotal.Text     '仕入
        Dim Total As String = TxtTotal.Text                     '売上
        Dim QuoteAmount As String = TxtQuoteTotal.Text          '見積
        Dim GrossProfitAmount As String = TxtGrossProfit.Text   '粗利

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
            'カーソルを砂時計にする
            Cursor.Current = Cursors.WaitCursor
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
            Dim currentRow As Integer = 11
            Dim lastRow As Integer = 13

            sheet.Range("B1").Value = QuoteNo & "-" & QuoteSuffix
            sheet.Range("I1").Value = QuoteDate & "(" & RegistrationDate & ")"
            sheet.Range("N1").Value = Expiration
            sheet.Range("B2").Value = CustomerName
            sheet.Range("B3").Value = PostalCode & " " & Address1
            sheet.Range("I2").Value = Tel
            sheet.Range("I3").Value = Fax
            sheet.Range("I4").Value = Person
            sheet.Range("I5").Value = Position
            sheet.Range("N2").Value = Sales
            sheet.Range("N3").Value = Input
            sheet.Range("B6").Value = PaymentTerms
            sheet.Range("B7").Value = QuoteRemarks
            sheet.Range("T15").Value = PurchaseTotal        '仕入
            sheet.Range("T16").Value = Total                '売上
            sheet.Range("T17").Value = QuoteAmount          '見積
            sheet.Range("T18").Value = GrossProfitAmount    '粗利



            For index As Integer = 0 To DgvItemList.Rows.Count - 1
                If rowCnt >= 3 Then
                    Dim R As Object
                    R = sheet.Range(lastRow - 2 & ":" & lastRow - 2)
                    R.Copy()
                    R.Insert()
                    If Marshal.IsComObject(R) Then
                        Marshal.ReleaseComObject(R)
                    End If
                    lastRow += 1
                End If
                rowCnt += 1
            Next

            For index As Integer = 0 To DgvItemList.Rows.Count - 1
                '明細からヘッダへ移動
                If DgvItemList.Rows(index).Cells("関税率").Value <> 0 Then
                    sheet.Range("I6").Value = DgvItemList.Rows(index).Cells("関税率").Value
                End If
                If DgvItemList.Rows(index).Cells("前払法人税率").Value <> 0 Then
                    sheet.Range("I7").Value = DgvItemList.Rows(index).Cells("前払法人税率").Value
                End If
                If DgvItemList.Rows(index).Cells("輸送費率").Value <> 0 Then
                    sheet.Range("I8").Value = DgvItemList.Rows(index).Cells("輸送費率").Value
                End If


                sheet.Range("A" & currentRow).Value = DgvItemList.Rows(index).Cells("No").Value

                sheet.Range("B" & currentRow).Value = DgvItemList.Rows(index).Cells("メーカー").Value
                sheet.Range("C" & currentRow).Value = DgvItemList.Rows(index).Cells("品名").Value
                sheet.Range("D" & currentRow).Value = DgvItemList.Rows(index).Cells("型式").Value
                sheet.Range("E" & currentRow).Value = DgvItemList.Rows(index).Cells("数量").Value
                sheet.Range("F" & currentRow).Value = DgvItemList.Rows(index).Cells("単位").Value
                sheet.Range("G" & currentRow).Value = DgvItemList.Rows(index).Cells("仕入先").Value
                sheet.Range("H" & currentRow).Value = DgvItemList.Rows(index).Cells("仕入単価").Value
                sheet.Range("I" & currentRow).Value = DgvItemList.Rows(index).Cells("仕入原価").Value
                sheet.Range("J" & currentRow).Value = Math.Ceiling(DgvItemList.Rows(index).Cells("関税額").Value)
                sheet.Range("K" & currentRow).Value = Math.Ceiling(DgvItemList.Rows(index).Cells("前払法人税額").Value)
                sheet.Range("L" & currentRow).Value = Math.Ceiling(DgvItemList.Rows(index).Cells("輸送費額").Value)
                sheet.Range("M" & currentRow).Value = DgvItemList.Rows(index).Cells("仕入金額").Value
                sheet.Range("N" & currentRow).Value = DgvItemList.Rows(index).Cells("売単価").Value
                sheet.Range("O" & currentRow).Value = DgvItemList.Rows(index).Cells("売上金額").Value
                sheet.Range("P" & currentRow).Value = DgvItemList.Rows(index).Cells("見積単価").Value
                sheet.Range("Q" & currentRow).Value = DgvItemList.Rows(index).Cells("見積金額").Value
                sheet.Range("R" & currentRow).Value = DgvItemList.Rows(index).Cells("粗利額").Value
                sheet.Range("S" & currentRow).Value = DgvItemList.Rows(index).Cells("粗利率").Value
                tmp = ""
                tmp += DgvItemList.Rows(index).Cells("リードタイム").Value
                tmp += DgvItemList.Item("リードタイム単位", index).FormattedValue
                sheet.Range("T" & currentRow).Value = tmp

                currentRow += 1

            Next

            app.DisplayAlerts = False 'Microsoft Excelのアラート一旦無効化

            Dim excelChk As Boolean = excelOutput(sOutFile)
            If excelChk = False Then
                Exit Sub
            End If
            book.SaveAs(sOutFile) '書き込み実行

            app.DisplayAlerts = True 'アラート無効化を解除
            app.Visible = True

            'カーソルを砂時計から元に戻す
            Cursor.Current = Cursors.Default

            _msgHd.dspMSG("CreateExcel", frmC01F10_Login.loginValue.Language)

        Catch ex As Exception
            Throw ex

        Finally
            'app.Quit()
            'Marshal.ReleaseComObject(sheet)
            'Marshal.ReleaseComObject(book)
            'Marshal.ReleaseComObject(app)

        End Try

    End Sub

    '得意先検索ボタンクリック
    '
    Private Sub BtnCodeSearch_Click(sender As Object, e As EventArgs) Handles BtnCodeSearch.Click
        '得意先マスタ読み込み
        Read_Customer()

    End Sub

    'キーイベント取得
    Private Sub DgvItemList_KeyDown(sender As Object, e As KeyEventArgs) Handles DgvItemList.KeyDown
        'F4キー押下
        Dim currentColumn As String = DgvItemList.Columns(DgvItemList.CurrentCell.ColumnIndex).Name
        Dim sireKbn As Integer = DgvItemList("仕入区分", DgvItemList.CurrentCell.RowIndex).Value

        Dim manufactuer As String = DgvItemList("メーカー", DgvItemList.CurrentCell.RowIndex).Value
        Dim itemName As String = DgvItemList("品名", DgvItemList.CurrentCell.RowIndex).Value
        Dim spec As String = DgvItemList("型式", DgvItemList.CurrentCell.RowIndex).Value

        '仕入区分[ 在庫引当 ] + 数量にいた場合
        If e.KeyData = Keys.F4 Then

            If sireKbn = CommonConst.Sire_KBN_Zaiko And currentColumn = "数量" Then
                manufactuer = IIf(manufactuer <> Nothing, manufactuer, "")
                itemName = IIf(itemName <> Nothing, itemName, "")
                spec = IIf(spec <> Nothing, spec, "")

                Dim openForm As Form = Nothing
                openForm = New StockSearch(_msgHd, _db, _langHd, Me, manufactuer, itemName, spec)
                openForm.Show()
                Me.Enabled = False

            End If

        End If
    End Sub


    '得意先マスタより得意先情報を読み込む
    Private Sub Read_Customer()
        Dim SqlCode As String = ""
        SqlCode += "SELECT * "
        SqlCode += "FROM public.m10_customer"
        SqlCode += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        SqlCode += " AND 得意先コード = '" & TxtCustomerCode.Text & "'"

        Dim reccnt As Integer = 0
        Dim dsCode = _db.selectDB(SqlCode, RS, reccnt)
        If dsCode.Tables(RS).Rows.Count > 0 Then
            TxtCustomerName.Text = dsCode.Tables(RS).Rows(0)("得意先名").ToString
            TxtPostalCode.Text = dsCode.Tables(RS).Rows(0)("郵便番号").ToString
            TxtAddress1.Text = dsCode.Tables(RS).Rows(0)("住所１").ToString & " " & dsCode.Tables(RS).Rows(0)("住所２").ToString & " " & dsCode.Tables(RS).Rows(0)("住所３").ToString
            TxtTel.Text = dsCode.Tables(RS).Rows(0)("電話番号").ToString
            TxtFax.Text = dsCode.Tables(RS).Rows(0)("ＦＡＸ番号").ToString
            TxtPerson.Text = dsCode.Tables(RS).Rows(0)("担当者名").ToString
            TxtPosition.Text = dsCode.Tables(RS).Rows(0)("担当者役職").ToString
            TxtPaymentTerms.Text = dsCode.Tables(RS).Rows(0)("既定支払条件").ToString

        End If

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
        Sql += " AND "
        Sql += "可変キー <> '" & CommonConst.Sire_KBN_Move & "'" '「移動」以外
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

    'Excel出力する際のチェック
    Private Function excelOutput(ByVal prmFilePath As String)
        Dim fileChk As String = Dir(prmFilePath)
        '同名ファイルがあるかどうかチェック
        If fileChk <> "" Then
            Dim result = _msgHd.dspMSG("confirmFileExist", frmC01F10_Login.loginValue.Language, prmFilePath)
            If result = DialogResult.No Then
                Return False
            End If

            Try
                'ファイルが開けるかどうかチェック
                Dim sr As StreamReader = New StreamReader(prmFilePath)
                sr.Close() '処理が通ったら閉じる
            Catch ex As Exception
                '開けない場合はアラートを表示してリターンさせる
                MessageBox.Show(ex.Message, CommonConst.AP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try

            Return True
        End If
        Return True
    End Function

End Class
