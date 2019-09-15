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
        Dim reccnt As Integer = 0

        delCellValueChanged()   'セル変更イベントを無効化

        TxtVat.Text = CommonConst.TAX_VAT * 100  'VATの率

        '仕入単価(基準通貨)の制御
        DgvItemList.Columns("仕入単価").ReadOnly = True
        DgvItemList.Columns("仕入単価").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
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
        '売単価(基準通貨)の制御
        DgvItemList.Columns("売単価").ReadOnly = True
        DgvItemList.Columns("売単価").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
        '見積単価(基準通貨)の制御
        DgvItemList.Columns("見積単価").ReadOnly = True
        DgvItemList.Columns("見積単価").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)

        DgvItemList.Columns("見積単価_外貨").DefaultCellStyle.BackColor = Color.LightGray


        'ヘッダ項目を中央寄せ
        DgvItemList.Columns("No").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("メーカー").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("品名").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("型式").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("数量").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("単位").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("仕入先コード").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("仕入先").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("仕入単価_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("仕入単価").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("仕入原価").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("関税率").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("関税額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("前払法人税率").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("前払法人税額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("輸送費率").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("輸送費額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("仕入金額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("売単価_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("売単価").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("売上金額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("見積単価").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("見積単価_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("見積金額_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("見積金額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("粗利額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("粗利率").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("リードタイム").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("備考").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        'セルの内容に合わせて、行の高さが自動的に調節されるようにする
        DgvItemList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        '"Column1"列のセルのテキストを折り返して表示する
        DgvItemList.Columns("型式").DefaultCellStyle.WrapMode = DataGridViewTriState.True
        DgvItemList.Columns("備考").DefaultCellStyle.WrapMode = DataGridViewTriState.True

        DgvItemList.Columns("見積単価_外貨").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)

        '仕入区分コンボボックス作成
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
        DgvItemList.Columns("仕入区分").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        'リードタイム単位コンボボックス作成
        Dim column2 As New DataGridViewComboBoxColumn()
        column2.DataSource = getReadTime()
        column2.ValueMember = "Value"
        column2.DisplayMember = "Display"
        column2.HeaderText = "リードタイム単位"
        column2.Name = "リードタイム単位"

        DgvItemList.Columns.Insert(29, column2)
        DgvItemList.Columns("リードタイム単位").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        '仕入通貨コンボボックス作成
        Dim column3 As New DataGridViewComboBoxColumn()
        column3.DataSource = getSireCurrency()
        column3.ValueMember = "Value"
        column3.DisplayMember = "Display"
        column3.HeaderText = "仕入通貨"
        column3.Name = "仕入通貨"

        DgvItemList.Columns.Insert(10, column3)
        DgvItemList.Columns("仕入通貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        TxtIDRCurrency.Text = setBaseCurrency()

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblMode.Text = "Mode"
            LblQuoteNo.Text = "QuotationNo" '見積番号
            LblRegistration.Text = "RegistrationDate" '登録日
            LblQuote.Text = "QuotationDate" '見積日
            LblExpiration.Text = "ExpirationDate" '見積有効期限
            LblCustomerName.Text = "Customer" '得意先名称
            LblAddress.Text = "Address" '住所
            LblTel.Text = "PhoneNumber" '電話番号
            LblFax.Text = "FAX" 'FAX
            LblPerson.Text = "NameOfPIC" '得意先担当者
            LblPosition.Text = "PositionPICCustomer" '得意先担当者役職
            LblSales.Text = "SalesPersonInCharge" '営業担当者
            LblInput.Text = "PICForInputting" '入力担当者
            LblCurrency.Text = "SalesCurrency" '販売通貨
            LblRate.Text = "Rate" 'レート
            LblPaymentTerms.Text = "PaymentTerms" '支払条件
            LblRemarks.Text = "Remarks" '備考
            LblItemCount.Text = "ItemCount" '明細数

            LblVat.Text = "Vat(%)" 'VAT(%)

            LblCurrencyQuoteAmount.Text = "QuotationAmount" '見積金額
            LblIDRCurrency.Text = "Currency" '通貨
            LblChangeCurrency.Text = "Currency" '通貨

            RbtnGP.Text = "GrossMarginInput" '粗利入力
            RbtnUP.Text = "InputUnitPrice " '単価入力
            RbtnQuote.Text = "QuotationPriceInput" '見積単価入力

            BtnCodeSearch.Text = "Search" '検索
            BtnInsert.Text = "InsertLine" '行挿入
            BtnUp.Text = "ShiftLineUp" '行移動↑
            BtnDown.Text = "ShiftLineDown" '行移動↓
            BtnRowsAdd.Text = "AddLine" '行追加
            BtnRowsDel.Text = "DeleteLine" '行削除
            BtnClone.Text = "LineDuplication" '行複製
            BtnProof.Text = "Proof" 'プルーフ発行
            BtnQuoteRequest.Text = "QuotationRequest" '見積依頼書発行
            BtnQuote.Text = "Quotation" '見積書発行
            BtnRegistration.Text = "Registration" '登録
            BtnBack.Text = "Back" '戻る

            LblOrderAmount.Text = "SalesAmount（l）"      '売上金額
            lblPurchasecost.Text = "PurchaseCost（c）"    '仕入原価
            LblGrossProfit.Text = "GrossMargin（o）"      '粗利額
            lblGrossmargin.Text = "GrossMarginRate(%)（p）"  '粗利率

            LblQuoteAmount.Text = "QuotationAmount（n）"    '見積金額
            LblPurchaseAmount.Text = "PurchaseAmount（j）"  '仕入金額
            lblProfitmargin.Text = "Profitmargin"           '利益
            lblProfitmarginRate.Text = "ProfitmarginRate(%)"   '利益率


            LblRegistration.Size = New Size(156, 23)
            LblRegistration.Location = New Point(274, 10)
            DtpRegistration.Location = New Point(436, 11)
            LblQuote.Location = New Point(592, 10)
            DtpQuote.Location = New Point(710, 11)
            LblExpiration.Location = New Point(866, 10)
            DtpExpiration.Location = New Point(982, 11)

            BtnCodeSearch.Size = New Size(108, 23)
            BtnCodeSearch.Location = New Point(212, 46)
            TxtCustomerName.Size = New Size(278, 23)
            TxtCustomerName.Location = New Point(326, 46)

            LblItemCount.Size = New Size(125, 23)
            LblItemCount.Location = New Point(1216, 162)
            TxtItemCount.Size = New Size(125, 23)
            TxtItemCount.Location = New Point(1216, 187)
            RbtnUP.Location = New Point(712, 193)
            RbtnGP.Location = New Point(879, 193)
            RbtnQuote.Location = New Point(1038, 193)

            'LblVat.Location = New Point(629, 422)
            'TxtVat.Location = New Point(735, 422)
            'LblGrossProfit.Location = New Point(628, 451)
            'TxtGrossProfit.Location = New Point(734, 451)
            'LblPurchaseAmount.Location = New Point(972, 422)
            'LblPurchaseAmount.Size = New Size(132, 23)
            'LblOrderAmount.Location = New Point(972, 451)
            'LblOrderAmount.Size = New Size(132, 23)
            'LblQuoteAmount.Location = New Point(972, 480)
            'LblQuoteAmount.Size = New Size(132, 23)

            '英語用見出し
            DgvItemList.Columns("仕入区分").HeaderText = "PurchasingClassification"
            DgvItemList.Columns("メーカー").HeaderText = "Manufacturer"
            DgvItemList.Columns("品名").HeaderText = "ItemName"
            DgvItemList.Columns("型式").HeaderText = "Spec"
            DgvItemList.Columns("数量").HeaderText = "Quantity" & vbCrLf & "a"
            DgvItemList.Columns("単位").HeaderText = "Unit"
            DgvItemList.Columns("仕入先コード").HeaderText = "SupplierCode"
            DgvItemList.Columns("仕入先").HeaderText = "SupplierName"
            DgvItemList.Columns("仕入通貨").HeaderText = "PurchaseCurrency"

            DgvItemList.Columns("仕入単価_外貨").HeaderText = "PurchaseUnitPrice" & vbCrLf & "(OriginalCurrency)"
            DgvItemList.Columns("仕入単価").HeaderText = "PurchaseUnitPrice(" & setBaseCurrency() & ")" & vbCrLf & "b"

            DgvItemList.Columns("仕入原価").HeaderText = "PurchasingCost" & vbCrLf & "c=a*b"
            DgvItemList.Columns("関税率").HeaderText = "CustomsDutyRate" & vbCrLf & "(%)d"
            DgvItemList.Columns("関税額").HeaderText = "CustomsDutyParUnit" & vbCrLf & "e=b*d"
            DgvItemList.Columns("前払法人税率").HeaderText = "PrepaidCorporateTaxRate" & vbCrLf & "(%)f"
            DgvItemList.Columns("前払法人税額").HeaderText = "PrepaidCorporateTaxAmountParUnit" & vbCrLf & "g=(b+e)*f"
            DgvItemList.Columns("輸送費率").HeaderText = "TransportationCostRate" & vbCrLf & "(%)h"
            DgvItemList.Columns("輸送費額").HeaderText = "TransportationCostParUnit" & vbCrLf & "i=b*h"
            DgvItemList.Columns("仕入金額").HeaderText = "PurchaseAmount" & vbCrLf & "j=a*(b+e+g+i)"
            DgvItemList.Columns("売単価_外貨").HeaderText = "SellingPrice" & vbCrLf & "(OriginalCurrency)"
            DgvItemList.Columns("売単価").HeaderText = "SellingPrice" & vbCrLf & "k"
            DgvItemList.Columns("売上金額").HeaderText = "SalesAmount" & vbCrLf & "l=a*k"
            DgvItemList.Columns("見積単価").HeaderText = "QuotetionPrice(" & setBaseCurrency() & ")" & vbCrLf & "m=k+e+g+i"
            DgvItemList.Columns("見積単価_外貨").HeaderText = "QuotetionPrice" & vbCrLf & "(OriginalCurrency)"
            DgvItemList.Columns("見積金額").HeaderText = "QuotetionAmount(" & setBaseCurrency() & ")" & vbCrLf & "n=a*m"
            DgvItemList.Columns("見積金額_外貨").HeaderText = "QuotetionAmount" & vbCrLf & "(OriginalCurrency)"
            DgvItemList.Columns("粗利額").HeaderText = "GrossMargin" & vbCrLf & "o=a*(k-b)"
            DgvItemList.Columns("粗利率").HeaderText = "GrossMarginRate(%)" & vbCrLf & "(%)p=(1-(b/k))*100"
            DgvItemList.Columns("リードタイム").HeaderText = "LeadTime"
            DgvItemList.Columns("リードタイム単位").HeaderText = "LeadTimeUnit"
            DgvItemList.Columns("備考").HeaderText = "Remarks"
        Else
            '日本語用見出し
            DgvItemList.Columns("数量").HeaderText = "数量" & vbCrLf & "a"

            DgvItemList.Columns("仕入単価_外貨").HeaderText = "仕入単価(原通貨)"
            DgvItemList.Columns("仕入単価").HeaderText = "仕入単価(" & setBaseCurrency() & ")" & vbCrLf & "b"

            DgvItemList.Columns("仕入原価").HeaderText = "仕入原価" & vbCrLf & "c=a*b"
            DgvItemList.Columns("関税率").HeaderText = "関税率" & vbCrLf & "(%)d"
            DgvItemList.Columns("関税額").HeaderText = "単価当り関税額" & vbCrLf & "e=b*d"
            DgvItemList.Columns("前払法人税率").HeaderText = "前払法人税率" & vbCrLf & "(%)f"
            DgvItemList.Columns("前払法人税額").HeaderText = "単価当り前払法人税額" & vbCrLf & "g=(b+e)*f"
            DgvItemList.Columns("輸送費率").HeaderText = "輸送費率" & vbCrLf & "(%)h"
            DgvItemList.Columns("輸送費額").HeaderText = "単価当り輸送費額" & vbCrLf & "i=b*h"
            DgvItemList.Columns("仕入金額").HeaderText = "仕入金額" & vbCrLf & "j=a*(b+e+g+i)"

            DgvItemList.Columns("売単価_外貨").HeaderText = "売単価(原通貨)"
            DgvItemList.Columns("売単価").HeaderText = "売単価(" & setBaseCurrency() & ")" & vbCrLf & "k"

            DgvItemList.Columns("売上金額").HeaderText = "売上金額" & vbCrLf & "l=a*k"
            DgvItemList.Columns("見積単価").HeaderText = "見積単価(" & setBaseCurrency() & ")" & vbCrLf & "m=k+e+g+i"
            DgvItemList.Columns("見積金額").HeaderText = "見積金額(" & setBaseCurrency() & ")" & vbCrLf & "n=a*m"
            DgvItemList.Columns("粗利額").HeaderText = "粗利額" & vbCrLf & "o=a*(k-b)"
            DgvItemList.Columns("粗利率").HeaderText = "粗利率(%)" & vbCrLf & "p=(1-(b/k))*100"


            DgvItemList.Columns("見積単価_外貨").HeaderText = "見積単価" & vbCrLf & "（原通貨）"
            DgvItemList.Columns("見積金額_外貨").HeaderText = "見積金額" & vbCrLf & "（原通貨）"

        End If

        Dim Sql As String = ""
        Dim ds As DataSet

        '=======================
        '   新規登録モード
        '=======================
        If Status Is CommonConst.STATUS_ADD Then
            '明細を１行デフォルト表示
            DgvItemList.Rows.Add()
            DgvItemList.Rows(DgvItemList.Rows.Count() - 1).Cells("仕入区分").Value = 1
            DgvItemList.Rows(DgvItemList.Rows.Count() - 1).Cells("リードタイム単位").Value = 1
            DgvItemList.Rows(DgvItemList.Rows.Count() - 1).Cells("仕入通貨").Value = 1

            Dim tmpCurrencyVal As Decimal = setSireCurrency(DgvItemList.Rows(0).Cells("仕入通貨").Value)
            DgvItemList.Rows(DgvItemList.Rows.Count() - 1).Cells("仕入レート").Value = tmpCurrencyVal
            DgvItemList.Rows(DgvItemList.Rows.Count() - 1).Cells("ステータス").Value = "ADD"
            DgvItemList.Rows(DgvItemList.Rows.Count() - 1).Cells("No").Value = "1"
            '行数表示
            TxtItemCount.Text = DgvItemList.Rows.Count()
        End If

        '
        '新規登録モード以外
        '
        If EditNo IsNot Nothing Then


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

                ds = _db.selectDB(Sql, RS, reccnt)

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
                    DtpRegistration.Value = ds1.Tables(RS).Rows(0)("登録日")
                Case CommonConst.STATUS_VIEW
                    TxtSuffixNo.Text = ds1.Tables(RS).Rows(0)("見積番号枝番")
                    DtpRegistration.Value = ds1.Tables(RS).Rows(0)("登録日")
                Case Else
                    TxtSuffixNo.Text = SuffixMax + 1
                    DtpRegistration.Value = DateTime.Now
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

            '呼び出し時にVATが0ならば国外
            If TxtVat.Text = 0 Then
                '国外
                txtDomesticArea.Text = 1
            Else
                '国内
                txtDomesticArea.Text = 0
            End If
            '通貨・レート情報設定
            createCurrencyCombobox(ds1.Tables(RS).Rows(0)("通貨").ToString)
            setRate()

            '通貨表示：ベースの設定
            TxtIDRCurrency.Text = setBaseCurrency()


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
                If ds3.Tables(RS).Rows(index)("仕入通貨") Is DBNull.Value Then
                Else
                    Dim tmp2 As Integer = ds3.Tables(RS).Rows(index)("仕入通貨")
                    DgvItemList.Rows(index).Cells("仕入通貨").Value = tmp2
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
                DgvItemList.Rows(index).Cells("売単価_外貨").Value = (Decimal.Parse(ds3.Tables(RS).Rows(index)("売単価")) * Decimal.Parse(TxtRate.Text)).ToString("N2")
                DgvItemList.Rows(index).Cells("売単価").Value = ds3.Tables(RS).Rows(index)("売単価")
                DgvItemList.Rows(index).Cells("売上金額").Value = ds3.Tables(RS).Rows(index)("売上金額")
                DgvItemList.Rows(index).Cells("粗利額").Value = ds3.Tables(RS).Rows(index)("粗利額")
                DgvItemList.Rows(index).Cells("粗利率").Value = ds3.Tables(RS).Rows(index)("粗利率")
                DgvItemList.Rows(index).Cells("見積単価").Value = ds3.Tables(RS).Rows(index)("見積単価")
                DgvItemList.Rows(index).Cells("見積単価_外貨").Value = ds3.Tables(RS).Rows(index)("見積単価_外貨")
                DgvItemList.Rows(index).Cells("見積金額").Value = ds3.Tables(RS).Rows(index)("見積金額")
                DgvItemList.Rows(index).Cells("見積金額_外貨").Value = ds3.Tables(RS).Rows(index)("見積金額_外貨")
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
                PurchaseTotal += Math.Ceiling(DgvItemList.Rows(index).Cells("仕入金額").Value)
                Total += DgvItemList.Rows(index).Cells("売上金額").Value
                QuoteTotal += DgvItemList.Rows(index).Cells("見積金額").Value
                GrossProfit += DgvItemList.Rows(index).Cells("粗利額").Value
            Next

            TxtPurchaseTotal.Text = PurchaseTotal.ToString("N2")
            TxtTotal.Text = Total.ToString("N2")
            TxtQuoteTotal.Text = QuoteTotal.ToString("N2")
            TxtGrossProfit.Text = GrossProfit.ToString("N2")

            'TxtVatAmount.Text = ((QuoteTotal.ToString("N0") * TxtVat.Text) / 100).ToString("N2")
            Call mCalVat_Out(QuoteTotal)
            setCurrency() '通貨に設定した内容に変更

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

            DtpRegistration.Value = DateTime.Now '登録日

            ' 指定した書式で日付を文字列に変換する
            Dim QuoteDate As String = dtNow.ToString("MMdd")

            TxtSuffixNo.Text = 1

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

            ds = _db.selectDB(Sql, RS, reccnt)

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


            '通貨・レート情報設定
            createCurrencyCombobox()
            setRate()

            '通貨表示：ベースの設定
            TxtIDRCurrency.Text = setBaseCurrency()


            '通貨表示：変更後の設定
            setChangeCurrency()

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
            CmCurrency.Enabled = False

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
            CmCurrency.Enabled = False
            'グリッドの入力項目を制限
            DgvItemList.Columns("仕入区分").ReadOnly = True
            DgvItemList.Columns("仕入区分").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvItemList.Columns("メーカー").ReadOnly = True
            DgvItemList.Columns("メーカー").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvItemList.Columns("品名").ReadOnly = True
            DgvItemList.Columns("品名").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvItemList.Columns("型式").ReadOnly = True
            DgvItemList.Columns("型式").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvItemList.Columns("数量").ReadOnly = True
            DgvItemList.Columns("数量").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvItemList.Columns("単位").ReadOnly = True
            DgvItemList.Columns("単位").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvItemList.Columns("仕入先コード").ReadOnly = True
            DgvItemList.Columns("仕入先コード").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvItemList.Columns("仕入先").ReadOnly = True
            DgvItemList.Columns("仕入先").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)

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

        '入力タイプのイベントハンドラーをセット
        setAddHandler()

        setCellValueChanged()   'セル変更イベントを無効化

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
    '
    'NothingをDecimalに置換
    Private Function rmNullDecimal(ByVal prmField As Object) As Decimal
        If prmField Is Nothing Then
            rmNullDecimal = 0
            Exit Function
        End If

        If Not IsNumeric(prmField) Then
            rmNullDecimal = 0
            Exit Function
        End If

        rmNullDecimal = prmField

    End Function
    '金額自動計算
    Private Sub CellValueChanged(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DgvItemList.CellValueChanged

        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        '操作したカラム名を取得
        Dim currentColumn As String = DgvItemList.Columns(e.ColumnIndex).Name

        '1回だとイベントハンドラーを削除しきれなかったので、2回実行している
        delCellValueChanged()
        delCellValueChanged()


        If LoadFlg Then  '画面の呼び出しが終了した場合

            TxtTotal.Clear()
            TxtPurchaseTotal.Clear()
            TxtQuoteTotal.Clear()
            TxtGrossProfit.Clear()
            TxtVatAmount.Clear()
            TxtCurrencyVatAmount.Clear()
            TxtCurrencyQuoteTotal.Clear()

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

            If DgvItemList.Rows.Count = 0 Then  '明細がない場合は終了
                Exit Sub
            End If


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
                            Sql += " t41.* ,t20.通貨,t20.レート"
                            Sql += " FROM t41_siredt t41 "
                            Sql += " INNER JOIN t40_sirehd t40 "
                            Sql += " ON "
                            Sql += " t41.会社コード = t40.会社コード "
                            Sql += " AND "
                            Sql += " t41.仕入番号 = t40.仕入番号 "

                            Sql += " INNER JOIN t20_hattyu t20 "
                            Sql += " on t41.発注番号 = t20.発注番号 and t41.発注番号枝番 = t20.発注番号枝番"

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

                                Dim intCur As Integer = ds.Tables(RS).Rows(0)("通貨")
                                DgvItemList.Rows(e.RowIndex).Cells("仕入通貨").Value = intCur
                                DgvItemList("仕入レート", e.RowIndex).Value = ds.Tables(RS).Rows(0)("レート")
                                DgvItemList("仕入単価_外貨", e.RowIndex).Value = ds.Tables(RS).Rows(0)("仕入値")
                                DgvItemList("仕入単価", e.RowIndex).Value = ds.Tables(RS).Rows(0)("仕入値").ToString() / ds.Tables(RS).Rows(0)("レート")

                            End If

                        End If

                    End If

                End If

            End If



            '各項目の属性チェック
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("数量").Value) And (DgvItemList.Rows(e.RowIndex).Cells("数量").Value IsNot Nothing) Then
                _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
                DgvItemList.Rows(e.RowIndex).Cells("数量").Value = Nothing
                setCellValueChanged()
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value) And (DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value IsNot Nothing) Then
                _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
                DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value = Nothing
                setCellValueChanged()
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("仕入単価_外貨").Value) And (DgvItemList.Rows(e.RowIndex).Cells("仕入単価_外貨").Value IsNot Nothing) Then
                _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
                DgvItemList.Rows(e.RowIndex).Cells("仕入単価_外貨").Value = Nothing
                setCellValueChanged()
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value) And (DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value IsNot Nothing) Then
                _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
                DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value = Nothing
                setCellValueChanged()
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("関税率").Value) And (DgvItemList.Rows(e.RowIndex).Cells("関税率").Value IsNot Nothing) Then
                _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
                DgvItemList.Rows(e.RowIndex).Cells("関税率").Value = Nothing
                setCellValueChanged()
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("関税額").Value) And (DgvItemList.Rows(e.RowIndex).Cells("関税額").Value IsNot Nothing) Then
                _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
                DgvItemList.Rows(e.RowIndex).Cells("関税額").Value = Nothing
                setCellValueChanged()
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("前払法人税率").Value) And (DgvItemList.Rows(e.RowIndex).Cells("前払法人税率").Value IsNot Nothing) Then
                _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
                DgvItemList.Rows(e.RowIndex).Cells("前払法人税率").Value = Nothing
                setCellValueChanged()
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value) And (DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value IsNot Nothing) Then
                _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
                DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value = Nothing
                setCellValueChanged()
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("輸送費率").Value) And (DgvItemList.Rows(e.RowIndex).Cells("輸送費率").Value IsNot Nothing) Then
                _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
                DgvItemList.Rows(e.RowIndex).Cells("輸送費率").Value = Nothing
                setCellValueChanged()
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value) And (DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value IsNot Nothing) Then
                _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
                DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value = Nothing
                setCellValueChanged()
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("仕入金額").Value) And (DgvItemList.Rows(e.RowIndex).Cells("仕入金額").Value IsNot Nothing) Then
                _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
                DgvItemList.Rows(e.RowIndex).Cells("仕入金額").Value = Nothing
                setCellValueChanged()
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("売単価").Value) And (DgvItemList.Rows(e.RowIndex).Cells("売単価").Value IsNot Nothing) Then
                _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
                DgvItemList.Rows(e.RowIndex).Cells("売単価").Value = Nothing
                setCellValueChanged()
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("売単価_外貨").Value) And (DgvItemList.Rows(e.RowIndex).Cells("売単価_外貨").Value IsNot Nothing) Then
                _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
                DgvItemList.Rows(e.RowIndex).Cells("売単価_外貨").Value = Nothing
                setCellValueChanged()
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value) And (DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value IsNot Nothing) Then
                _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
                DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value = Nothing
                setCellValueChanged()
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value) And (DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value IsNot Nothing) Then
                _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
                DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value = Nothing
                setCellValueChanged()
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value) And (DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value IsNot Nothing) Then
                _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
                DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value = Nothing
                setCellValueChanged()
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("見積金額").Value) And (DgvItemList.Rows(e.RowIndex).Cells("見積金額").Value IsNot Nothing) Then
                _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
                DgvItemList.Rows(e.RowIndex).Cells("見積金額").Value = Nothing
                setCellValueChanged()
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("見積金額_外貨").Value) And (DgvItemList.Rows(e.RowIndex).Cells("見積金額_外貨").Value IsNot Nothing) Then
                _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
                DgvItemList.Rows(e.RowIndex).Cells("見積金額_外貨").Value = Nothing
                setCellValueChanged()
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value) And (DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value IsNot Nothing) Then
                _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
                DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value = Nothing
                setCellValueChanged()
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("粗利率").Value) And (DgvItemList.Rows(e.RowIndex).Cells("粗利率").Value IsNot Nothing) Then
                _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
                DgvItemList.Rows(e.RowIndex).Cells("粗利率").Value = Nothing
                setCellValueChanged()
                Exit Sub
            End If


            If currentColumn = "数量" Then  '数量の場合、カンマ処理
                Dim tmpCur As Decimal = DgvItemList.Rows(e.RowIndex).Cells("数量").Value
                DgvItemList("数量", e.RowIndex).Value = tmpCur
            End If

            If currentColumn = "仕入単価_外貨" Then  '仕入単価_外貨の場合、カンマ処理
                Dim tmpCur As Decimal = DgvItemList.Rows(e.RowIndex).Cells("仕入単価_外貨").Value
                DgvItemList("仕入単価_外貨", e.RowIndex).Value = tmpCur
            End If

            If currentColumn = "売単価_外貨" Then  '売単価_外貨の場合、カンマ処理
                Dim tmpCur As Decimal = DgvItemList.Rows(e.RowIndex).Cells("売単価_外貨").Value
                DgvItemList("売単価_外貨", e.RowIndex).Value = tmpCur
            End If

            If currentColumn = "見積単価_外貨" Then  '見積単価_外貨の場合、カンマ処理
                Dim tmpCur As Decimal = DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value
                DgvItemList("見積単価_外貨", e.RowIndex).Value = tmpCur
            End If


            '仕入通貨が変更されたら仕入レートを更新する
            If currentColumn = "仕入通貨" Then
                Dim sireCurrencyCd As String = DgvItemList("仕入通貨", e.RowIndex).Value

                Dim tmpCurrencyVal As Decimal = setSireCurrency(DgvItemList.Rows(e.RowIndex).Cells("仕入通貨").Value)
                DgvItemList("仕入レート", e.RowIndex).Value = tmpCurrencyVal

                'Dim ds As DataSet = setSireCurrency(sireCurrencyCd)
                'If ds.Tables(RS).Rows.Count > 0 Then
                '    DgvItemList("仕入レート", e.RowIndex).Value = ds.Tables(RS).Rows(0)("レート")
                'End If
            End If

            '仕入通貨 / 仕入単価 / 仕入単価_外貨 が変更されたらそれぞれを更新する
            If currentColumn = "仕入通貨" Or currentColumn = "仕入単価" Or currentColumn = "仕入単価_外貨" Then
                'delCellValueChanged()

                If DgvItemList.Rows.Count > 0 Then
                    Select Case currentColumn
                        Case "仕入通貨"
                            If DgvItemList("仕入単価", e.RowIndex).Value IsNot Nothing And DgvItemList("仕入レート", e.RowIndex).Value IsNot Nothing Then
                                '小数点表示にするため切り上げをコメントアウト
                                'DgvItemList("仕入単価", e.RowIndex).Value = Math.Ceiling(DgvItemList("仕入単価_外貨", e.RowIndex).Value / DgvItemList("仕入レート", e.RowIndex).Value)

                                Dim dotAlign As Decimal = DgvItemList("仕入単価_外貨", e.RowIndex).Value / DgvItemList("仕入レート", e.RowIndex).Value
                                dotAlign *= 100
                                dotAlign = Math.Ceiling(dotAlign)
                                dotAlign /= 100

                                DgvItemList("仕入単価", e.RowIndex).Value = dotAlign
                            End If
                        Case "仕入単価"
                            If DgvItemList("仕入レート", e.RowIndex).Value IsNot Nothing Then
                                Dim tmpCur As Decimal = DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value
                                DgvItemList("仕入単価", e.RowIndex).Value = tmpCur
                                DgvItemList("仕入単価_外貨", e.RowIndex).Value = Math.Ceiling(DgvItemList("仕入単価", e.RowIndex).Value * DgvItemList("仕入レート", e.RowIndex).Value)
                            End If
                        Case "仕入単価_外貨"
                            If DgvItemList("仕入単価_外貨", e.RowIndex).Value IsNot Nothing And DgvItemList("仕入レート", e.RowIndex).Value IsNot Nothing Then
                                '小数点表示にするため切り上げをコメントアウト
                                'DgvItemList("仕入単価", e.RowIndex).Value = Math.Ceiling(DgvItemList("仕入単価_外貨", e.RowIndex).Value / DgvItemList("仕入レート", e.RowIndex).Value)
                                Dim dotAlign As Decimal = DgvItemList("仕入単価_外貨", e.RowIndex).Value / DgvItemList("仕入レート", e.RowIndex).Value
                                dotAlign *= 100
                                dotAlign = Math.Ceiling(dotAlign)
                                dotAlign /= 100
                                DgvItemList("仕入単価", e.RowIndex).Value = dotAlign
                            End If
                    End Select

                End If
                'setCellValueChanged()
            End If


            Try
                '計算式を各行に適用
                If e.RowIndex > -1 Then

                    '仕入単価 <> Nothing
                    '--------------------------
                    If DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value IsNot Nothing Then

                        '数量 <> Nothing
                        If DgvItemList.Rows(e.RowIndex).Cells("数量").Value IsNot Nothing Then
                            DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value = DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value * DgvItemList.Rows(e.RowIndex).Cells("数量").Value
                        End If
                        '関税率 <> Nothing
                        If DgvItemList.Rows(e.RowIndex).Cells("関税率").Value IsNot Nothing Then
                            DgvItemList.Rows(e.RowIndex).Cells("関税額").Value = DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value * DgvItemList.Rows(e.RowIndex).Cells("関税率").Value / 100
                        End If
                        '前払法人税率, 関税額 <> Nothing
                        If DgvItemList.Rows(e.RowIndex).Cells("前払法人税率").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("関税額").Value IsNot Nothing Then
                            tmp = DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value + DgvItemList.Rows(e.RowIndex).Cells("関税額").Value
                            tmp1 = tmp * DgvItemList.Rows(e.RowIndex).Cells("前払法人税率").Value / 100
                            'tmp1 = Math.Ceiling(tmp1)
                            DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value = tmp1
                        End If
                        '輸送費率 <> Nothing
                        If DgvItemList.Rows(e.RowIndex).Cells("輸送費率").Value IsNot Nothing Then
                            '小数点２位表示
                            DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value = DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value * DgvItemList.Rows(e.RowIndex).Cells("輸送費率").Value / 100
                        End If

                        If currentColumn = "仕入単価" Then
                            '仕入単価_外貨
                            DgvItemList("仕入単価_外貨", e.RowIndex).Value = DgvItemList("仕入単価", e.RowIndex).Value / DgvItemList("仕入レート", e.RowIndex).Value
                        End If

                    End If


                    '関税額, 前払法人税額, 輸送費額 <> Nothing
                    '--------------------------
                    'If DgvItemList.Rows(e.RowIndex).Cells("関税額").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value IsNot Nothing Then
                    '仕入原価 <> Nothing
                    If DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value IsNot Nothing Then
                        DgvItemList.Rows(e.RowIndex).Cells("仕入金額").Value = DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value _
                                                                                 + (DgvItemList.Rows(e.RowIndex).Cells("関税額").Value + DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value + DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value) _
                                                                                 * DgvItemList.Rows(e.RowIndex).Cells("数量").Value
                    End If

                    '--------------------------
                    '単価入力 or 粗利入力
                    '--------------------------
                    If RbtnUP.Checked Or RbtnGP.Checked Then

                        '売単価 <> Nothing
                        If DgvItemList.Rows(e.RowIndex).Cells("売単価").Value IsNot Nothing Then
                            DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value = Decimal.Parse(
                                                                                   rmNullDecimal(DgvItemList.Rows(e.RowIndex).Cells("売単価").Value) + rmNullDecimal(DgvItemList.Rows(e.RowIndex).Cells("関税額").Value) + rmNullDecimal(DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value) + rmNullDecimal(DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value))
                            '端数処理
                            DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value = Math.Truncate(DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value * 100) / 100
                            If (DgvItemList.Rows(e.RowIndex).Cells("関税額").Value Is Nothing Or DgvItemList.Rows(e.RowIndex).Cells("関税額").Value = 0) _
                                And (DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value Is Nothing Or DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value = 0) _
                                And (DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value Is Nothing Or DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value = 0) Then

                                DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value = DgvItemList.Rows(e.RowIndex).Cells("売単価_外貨").Value
                                '端数処理
                                DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value = Math.Truncate(DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value * 100) / 100
                            Else
                                DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value = DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value * TxtRate.Text
                                '端数処理
                                DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value = Math.Truncate(DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value * 100) / 100
                            End If
                        End If
                        'End If

                        '売単価_外貨(原通貨)
                        If DgvItemList.Rows(e.RowIndex).Cells("売単価_外貨").Value IsNot Nothing Then
                            DgvItemList.Rows(e.RowIndex).Cells("売単価").Value = DgvItemList.Rows(e.RowIndex).Cells("売単価_外貨").Value / TxtRate.Text

                            DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value = Decimal.Parse(
                                                                                   rmNullDecimal(DgvItemList.Rows(e.RowIndex).Cells("売単価").Value) + rmNullDecimal(DgvItemList.Rows(e.RowIndex).Cells("関税額").Value) + rmNullDecimal(DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value) + rmNullDecimal(DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value))
                            '端数処理
                            DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value = Math.Truncate(DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value * 100) / 100

                            If (DgvItemList.Rows(e.RowIndex).Cells("関税額").Value Is Nothing Or DgvItemList.Rows(e.RowIndex).Cells("関税額").Value = 0) _
                                And (DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value Is Nothing Or DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value = 0) _
                                And (DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value Is Nothing Or DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value = 0) Then

                                DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value = DgvItemList.Rows(e.RowIndex).Cells("売単価_外貨").Value
                                '端数処理
                                DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value = Math.Truncate(DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value * 100) / 100
                            Else
                                DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value = DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value * TxtRate.Text
                                '端数処理
                                DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value = Math.Truncate(DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value * 100) / 100
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
                                DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value = DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value - DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value
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
                                DgvItemList.Rows(e.RowIndex).Cells("売単価_外貨").Value = DgvItemList.Rows(e.RowIndex).Cells("売単価").Value * TxtRate.Text

                                DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value = DgvItemList.Rows(e.RowIndex).Cells("売単価").Value * DgvItemList.Rows(e.RowIndex).Cells("数量").Value
                                DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value = DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value - DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value

                                DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value = Decimal.Parse(rmNullDecimal(DgvItemList.Rows(e.RowIndex).Cells("売単価").Value) + rmNullDecimal(DgvItemList.Rows(e.RowIndex).Cells("関税額").Value) + rmNullDecimal(DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value) + rmNullDecimal(DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value))
                                '端数処理
                                DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value = Math.Truncate(DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value * 100) / 100

                                If (DgvItemList.Rows(e.RowIndex).Cells("関税額").Value Is Nothing Or DgvItemList.Rows(e.RowIndex).Cells("関税額").Value = 0) _
                                    And (DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value Is Nothing Or DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value = 0) _
                                    And (DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value Is Nothing Or DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value = 0) Then

                                    DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value = DgvItemList.Rows(e.RowIndex).Cells("売単価_外貨").Value
                                    '端数処理
                                    DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value = Math.Truncate(DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value * 100) / 100
                                Else
                                    DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value = DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value * TxtRate.Text
                                    '端数処理
                                    DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value = Math.Truncate(DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value * 100) / 100
                                End If
                            End If
                        End If
                    Else

                        '--------------------------
                        '見積入力
                        '--------------------------

                        '見積単価, 売単価, 関税額, 前払法人税額, 輸送費額 <> Nothing
                        '--------------------------

                        If DgvItemList.Columns(e.ColumnIndex).Name = "見積単価" Then
                            If DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("関税額").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value IsNot Nothing Then
                                '端数処理
                                DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value = Math.Truncate(DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value * 100) / 100
                                DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value = DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value * TxtRate.Text
                                '端数処理
                                DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value = Math.Truncate(DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value * 100) / 100
                                tmp4 = DgvItemList.Rows(e.RowIndex).Cells("関税額").Value + DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value + DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value
                                DgvItemList.Rows(e.RowIndex).Cells("売単価").Value = DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value - tmp4
                                DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value = DgvItemList.Rows(e.RowIndex).Cells("売単価").Value * DgvItemList.Rows(e.RowIndex).Cells("数量").Value
                                DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value = DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value - DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value
                                DgvItemList.Rows(e.RowIndex).Cells("粗利率").Value = Format(DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value / DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value * 100, "0.0")
                            End If
                        ElseIf DgvItemList.Columns(e.ColumnIndex).Name = "見積単価_外貨" Then

                            DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value = DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value / TxtRate.Text
                            '端数処理
                            DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value = Math.Truncate(DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value * 100) / 100
                            tmp4 = DgvItemList.Rows(e.RowIndex).Cells("関税額").Value + DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value + DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value

                            DgvItemList.Rows(e.RowIndex).Cells("売単価").Value = DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value - tmp4
                            DgvItemList.Rows(e.RowIndex).Cells("売単価_外貨").Value = DgvItemList.Rows(e.RowIndex).Cells("売単価").Value * TxtRate.Text
                            DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value = DgvItemList.Rows(e.RowIndex).Cells("売単価").Value * DgvItemList.Rows(e.RowIndex).Cells("数量").Value

                            DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value = DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value - DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value
                            DgvItemList.Rows(e.RowIndex).Cells("粗利率").Value = Format(DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value / DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value * 100, "0.0")
                            'End If
                        End If

                    End If


                    '見積金額算出
                    If DgvItemList.Rows(e.RowIndex).Cells("数量").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value IsNot Nothing Then
                        DgvItemList.Rows(e.RowIndex).Cells("見積金額").Value = DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value * DgvItemList.Rows(e.RowIndex).Cells("数量").Value
                        DgvItemList.Rows(e.RowIndex).Cells("見積金額_外貨").Value = DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value * DgvItemList.Rows(e.RowIndex).Cells("数量").Value
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
                                DgvItemList.Rows(e.RowIndex).Cells("関税率").Value = ds.Tables(RS).Rows(0)("関税率").ToString * 100
                                DgvItemList.Rows(e.RowIndex).Cells("前払法人税率").Value = ds.Tables(RS).Rows(0)("前払法人税率").ToString * 100
                                DgvItemList.Rows(e.RowIndex).Cells("輸送費率").Value = ds.Tables(RS).Rows(0)("輸送費率").ToString * 100
                            Else
                                DgvItemList.Rows(e.RowIndex).Cells("仕入先").Value = ""
                                DgvItemList.Rows(e.RowIndex).Cells("間接費率").Value = 0
                                DgvItemList.Rows(e.RowIndex).Cells("関税率").Value = 0
                                DgvItemList.Rows(e.RowIndex).Cells("前払法人税率").Value = 0
                                DgvItemList.Rows(e.RowIndex).Cells("輸送費率").Value = 0

                            End If
                        End If
                    End If

                    setCellValueChanged()

                End If


                'ヘッダ部
                setCurrency()

            Catch ex As OverflowException

                Throw ex

            End Try

        End If

    End Sub

    Private Sub RbtnGP_CheckedChanged(sender As Object, e As EventArgs)

        If RbtnUP.Checked Then
            '単価入力選択時
            DgvItemList.Columns("粗利率").ReadOnly = True
            DgvItemList.Columns("売単価_外貨").ReadOnly = False
            DgvItemList.Columns("見積単価_外貨").ReadOnly = True
            DgvItemList.Columns("粗利率").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvItemList.Columns("売単価_外貨").DefaultCellStyle.BackColor = Color.White
            DgvItemList.Columns("見積単価_外貨").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
        ElseIf RbtnGP.Checked Then
            '粗利入力
            DgvItemList.Columns("売単価_外貨").ReadOnly = True
            DgvItemList.Columns("粗利率").ReadOnly = False
            DgvItemList.Columns("見積単価_外貨").ReadOnly = True
            DgvItemList.Columns("売単価_外貨").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvItemList.Columns("粗利率").DefaultCellStyle.BackColor = Color.White
            DgvItemList.Columns("見積単価_外貨").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
        Else
            '見積単価入力
            DgvItemList.Columns("売単価_外貨").ReadOnly = True
            DgvItemList.Columns("粗利率").ReadOnly = True
            DgvItemList.Columns("見積単価_外貨").ReadOnly = False
            DgvItemList.Columns("売単価_外貨").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvItemList.Columns("粗利率").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvItemList.Columns("見積単価_外貨").DefaultCellStyle.BackColor = Color.White
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
            DgvItemList.Rows(RowIdx + 1).Cells("仕入通貨").Value = 1
            Dim tmpCurrencyVal As Decimal = setSireCurrency(DgvItemList.Rows(0).Cells("仕入通貨").Value)
            DgvItemList.Rows(RowIdx + 1).Cells("仕入レート").Value = tmpCurrencyVal
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

            'フォーカス
            DgvItemList.Focus()
            If RowIdx = 0 Then
                DgvItemList.CurrentCell = DgvItemList(2, 1)
            Else
                DgvItemList.CurrentCell = DgvItemList(2, RowIdx + 1)
            End If
        Else
            DgvItemList.Rows.Add()
            DgvItemList.Rows(0).Cells("仕入区分").Value = 1
            DgvItemList.Rows(0).Cells("リードタイム単位").Value = 1
            DgvItemList.Rows(0).Cells("仕入通貨").Value = 1
            Dim tmpCurrencyVal As Decimal = setSireCurrency(DgvItemList.Rows(0).Cells("仕入通貨").Value)
            DgvItemList.Rows(0).Cells("仕入レート").Value = tmpCurrencyVal
            'Dim ds As DataSet = setSireCurrency(DgvItemList.Rows(0).Cells("仕入通貨").Value)
            'If ds.Tables(RS).Rows.Count > 0 Then
            '    DgvItemList.Rows(0).Cells("仕入レート").Value = ds.Tables(RS).Rows(0)("レート")
            'End If
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

            'フォーカス
            DgvItemList.Focus()
            DgvItemList.CurrentCell = DgvItemList(2, 0)
        End If
    End Sub

    '行追加（DGVの最終行に追加）
    Private Sub BtnRowsAdd_Click(sender As Object, e As EventArgs) Handles BtnRowsAdd.Click
        DgvItemList.Rows.Add()
        DgvItemList.Rows(DgvItemList.Rows.Count() - 1).Cells("仕入区分").Value = 1
        DgvItemList.Rows(DgvItemList.Rows.Count() - 1).Cells("リードタイム単位").Value = 1
        DgvItemList.Rows(DgvItemList.Rows.Count() - 1).Cells("仕入通貨").Value = 1

        Dim tmpCurrencyVal As Decimal = setSireCurrency(DgvItemList.Rows(0).Cells("仕入通貨").Value)
        DgvItemList.Rows(DgvItemList.Rows.Count() - 1).Cells("仕入レート").Value = tmpCurrencyVal
        'Dim ds As DataSet = setSireCurrency(DgvItemList.Rows(DgvItemList.Rows.Count() - 1).Cells("仕入通貨").Value)
        'If ds.Tables(RS).Rows.Count > 0 Then
        '    DgvItemList.Rows(DgvItemList.Rows.Count() - 1).Cells("仕入レート").Value = ds.Tables(RS).Rows(0)("レート")
        'End If
        DgvItemList.Rows(DgvItemList.Rows.Count() - 1).Cells("ステータス").Value = "ADD"
        '行番号の振り直し
        Dim index As Integer = DgvItemList.Rows.Count()
        Dim No As Integer = 1
        For c As Integer = 0 To index - 1
            DgvItemList.Rows(c).Cells("No").Value = No
            No += 1
        Next c
        TxtItemCount.Text = DgvItemList.Rows.Count()

        'フォーカス
        DgvItemList.Focus()
        DgvItemList.CurrentCell = DgvItemList(2, index - 1)

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
        TxtCurrencyVatAmount.Clear()

        Dim Total As Integer = 0
        Dim QuoteTotal As Integer = 0
        Dim PurchaseTotal As Integer = 0
        Dim GrossProfit As Decimal = 0

        For c As Integer = 0 To DgvItemList.Rows.Count - 1
            PurchaseTotal += Math.Ceiling(DgvItemList.Rows(c).Cells("仕入金額").Value)
            Total += DgvItemList.Rows(c).Cells("売上金額").Value
            QuoteTotal += DgvItemList.Rows(c).Cells("見積金額").Value
            GrossProfit += DgvItemList.Rows(c).Cells("粗利額").Value
        Next
        TxtPurchaseTotal.Text = PurchaseTotal.ToString("N2")
        TxtTotal.Text = Total.ToString("N2")
        TxtQuoteTotal.Text = QuoteTotal.ToString("N2")
        TxtGrossProfit.Text = GrossProfit.ToString("N2")

        'TxtVatAmount.Text = ((QuoteTotal * TxtVat.Text) / 100).ToString("N2")
        Call mCalVat_Out(QuoteTotal)

        setCurrency() '通貨に設定した内容に変更




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
            Dim Item(30) As String

            '一覧選択行インデックスの取得
            'グリッドに何もないときは処理しない
            If DgvItemList.CurrentCell Is Nothing Then
                Exit Sub
            End If

            '現在位置の列を取得
            RowIdx = DgvItemList.CurrentCell.RowIndex

            '対象列のクローンを作成
            Dim rowClone As DataGridViewRow = DgvItemList.Rows(RowIdx).Clone

            'columnの数分valueを複写
            For i As Integer = 0 To DgvItemList.ColumnCount - 1
                rowClone.Cells(i).Value = DgvItemList.Rows(RowIdx).Cells(i).Value
            Next

            '1行下に新規行作成及びclone内容を反映
            DgvItemList.Rows.Insert(RowIdx + 1, rowClone)
            DgvItemList.Rows(RowIdx + 1).Cells("ステータス").Value = "ADD"


            '選択行の値を格納		Item(c)	"1"	String

            'For c As Integer = 0 To 30
            '    Item(c) = DgvItemList.Rows(RowIdx).Cells(c).Value
            'Next c

            '行を挿入
            'DgvItemList.Rows.Insert(RowIdx + 1)
            'DgvItemList.Rows(RowIdx + 1).Cells("ステータス").Value = "ADD"

            ''追加した行に複製元の値を格納
            'For c As Integer = 0 To 30
            '    If c = 1 Then
            '        If Item(c) IsNot Nothing Then
            '            Dim tmp As Integer = Item(c)
            '            DgvItemList(1, RowIdx + 1).Value = tmp
            '        End If
            '    ElseIf c = 10 Then '通貨
            '        If Item(c) IsNot Nothing Then
            '            Dim tmp As Integer = Item(c)
            '            DgvItemList(10, RowIdx + 1).Value = tmp
            '        End If
            '    ElseIf c = 30 Then 'リードタイム単価
            '        If Item(c) IsNot Nothing Then
            '            Dim tmp As Integer = Item(c)
            '            DgvItemList(30, RowIdx + 1).Value = tmp
            '        End If
            '    Else
            '        DgvItemList.Rows(RowIdx + 1).Cells(c).Value = Item(c)
            '    End If

            'Next c

            '最終行のインデックスを取得
            Dim index As Integer = DgvItemList.Rows.Count()
            '行番号の振り直し
            Dim No As Integer = 1
            For c As Integer = 0 To index - 1
                DgvItemList.Rows(c).Cells("No").Value = No
                No += 1
            Next c
            TxtItemCount.Text = DgvItemList.Rows.Count()

            'フォーカス
            DgvItemList.Focus()
            DgvItemList.CurrentCell = DgvItemList(2, RowIdx + 1)


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

    Private Sub noReset()
        Dim index As Integer = DgvItemList.Rows.Count()
        Dim No As Integer = 1
        For c As Integer = 0 To index - 1
            DgvItemList.Rows(c).Cells("No").Value = No
            No += 1
        Next c

    End Sub

    '行移動実行
    Private Sub setRowClone(ByVal rowIndex As Integer, ByVal status As String)
        'DgvItemList.CurrentCell = DgvItemList(DgvItemList.CurrentCell.ColumnIndex, DgvItemList.CurrentCell.RowIndex)
        Dim nextRowIndex As Integer
        Dim delRowIndex As Integer
        If status = "UP" Then
            nextRowIndex = rowIndex - 1
            delRowIndex = rowIndex + 1
        Else
            nextRowIndex = rowIndex + 2
            delRowIndex = rowIndex
        End If

        Dim rowClone As DataGridViewRow = DgvItemList.Rows(rowIndex).Clone

        'columnの数分valueを複写
        For i As Integer = 0 To DgvItemList.ColumnCount - 1
            rowClone.Cells(i).Value = DgvItemList.Rows(rowIndex).Cells(i).Value
        Next

        '2行上に新規行作成及びclone内容を反映
        DgvItemList.Rows.Insert(nextRowIndex, rowClone)
        'カレントだった行を削除
        DgvItemList.Rows.RemoveAt(delRowIndex)

        '行番号の振り直し
        noReset()

        If status = "UP" Then
            DgvItemList.CurrentCell = DgvItemList(DgvItemList.CurrentCell.ColumnIndex, nextRowIndex)
        Else
            DgvItemList.CurrentCell = DgvItemList(DgvItemList.CurrentCell.ColumnIndex, nextRowIndex - 1)
        End If

    End Sub

    '行移動↑
    Private Sub BtnUp_Click(sender As Object, e As EventArgs) Handles BtnUp.Click
        'グリッドに何もないときは処理しない
        If DgvItemList.CurrentCell Is Nothing Then
            Exit Sub
        End If

        If DgvItemList.CurrentCell.RowIndex > 0 Then
            setRowClone(DgvItemList.CurrentCell.RowIndex, "UP")
        End If
    End Sub

    '行移動↓
    Private Sub BtnDown_Click(sender As Object, e As EventArgs) Handles BtnDown.Click

        'グリッドに何もないときは処理しない
        If DgvItemList.CurrentCell Is Nothing Then
            Exit Sub
        End If

        If DgvItemList.CurrentCell.RowIndex < DgvItemList.Rows.Count - 1 Then
            setRowClone(DgvItemList.CurrentCell.RowIndex, "DOWN")
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


        '仕入区分が「在庫引当」以外かつ品名が選択されていない場合
        For i As Integer = 0 To DgvItemList.RowCount - 1
            If DgvItemList.Rows(i).Cells("仕入区分").Value <> CommonConst.Sire_KBN_Zaiko And
                (DgvItemList.Rows(i).Cells("メーカー").Value Is Nothing Or DgvItemList.Rows(i).Cells("メーカー").Value = vbNullString) Or
                (DgvItemList.Rows(i).Cells("品名").Value Is Nothing Or DgvItemList.Rows(i).Cells("品名").Value = vbNullString) Or
                (DgvItemList.Rows(i).Cells("型式").Value Is Nothing Or DgvItemList.Rows(i).Cells("型式").Value = vbNullString) Then

                '対象データがないメッセージを表示
                _msgHd.dspMSG("chkQuoteInputError", frmC01F10_Login.loginValue.Language)

                Exit Sub
            End If
        Next


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
                Sql1 += ",見積金額 = " & UtilClass.formatNumber(TxtQuoteTotal.Text)
                Sql1 += ",見積金額_外貨 = " & UtilClass.formatNumber(TxtCurrencyQuoteTotal.Text)
                Sql1 += ",仕入金額 = " & UtilClass.formatNumber(TxtPurchaseTotal.Text)
                Sql1 += ",粗利額 = " & UtilClass.formatNumber(TxtGrossProfit.Text)
                Sql1 += ",営業担当者コード = '" & TxtSales.Tag & "' "
                Sql1 += ",営業担当者 = '" & TxtSales.Text & "' "
                Sql1 += ",入力担当者コード = '" & frmC01F10_Login.loginValue.TantoCD & "' "
                Sql1 += ",入力担当者 = '" & TxtInput.Text & "' "
                Sql1 += ",備考 = '" & RevoveChars(TxtRemarks.Text) & "' "
                '国内の判定
                If txtDomesticArea.Text = CommonConst.DD_KBN_DOMESTIC Then  '国内
                    Sql1 += ",ＶＡＴ = " & UtilClass.formatNumber(TxtVat.Text)
                Else  '国外
                    Sql1 += ",ＶＡＴ = " & UtilClass.formatNumber(0)
                End If

                Sql1 += ",登録日 = '" & UtilClass.strFormatDate(DtpRegistration.Text) & "' "
                Sql1 += ",更新日 = '" & strToday & "' "
                Sql1 += ",更新者 = '" & Input & "' "
                Sql1 += ",通貨 = " & CmCurrency.SelectedValue.ToString
                Sql1 += ",レート = " & UtilClass.formatNumberF10(TxtRate.Text)
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
                        Sql2 += ",仕入単価 = " & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("仕入単価").Value.ToString)
                    Else
                        Sql2 += ",仕入単価 = 0"
                    End If

                    If DgvItemList.Rows(index).Cells("仕入通貨").Value IsNot Nothing And Not IsNumeric(DgvItemList.Rows(index).Cells("仕入通貨").Value) Then
                        Sql2 += ",仕入通貨 = " & DgvItemList.Rows(index).Cells("仕入通貨").Value.ToString
                    Else
                        Sql2 += ",仕入通貨 = 1"
                    End If
                    If DgvItemList.Rows(index).Cells("仕入単価_外貨").Value IsNot Nothing Then
                        Sql2 += ",仕入単価_外貨 = " & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("仕入単価_外貨").Value.ToString)
                    Else
                        Sql2 += ",仕入単価_外貨 = 0"
                    End If
                    If DgvItemList.Rows(index).Cells("仕入レート").Value IsNot Nothing Then
                        Sql2 += ",仕入レート = " & UtilClass.formatNumberF10(DgvItemList.Rows(index).Cells("仕入レート").Value.ToString)
                    Else
                        Sql2 += ",仕入レート = 0"
                    End If
                    If DgvItemList.Rows(index).Cells("仕入金額").Value IsNot Nothing Then
                        Sql2 += ",仕入金額 = " & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("仕入金額").Value.ToString)
                    Else
                        Sql2 += ",仕入金額 = 0"
                    End If
                    If DgvItemList.Rows(index).Cells("仕入原価").Value IsNot Nothing Then
                        Sql2 += ",仕入原価 = " & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("仕入原価").Value.ToString)
                    Else
                        Sql2 += ",仕入原価 = 0"
                    End If
                    If DgvItemList.Rows(index).Cells("関税率").Value IsNot Nothing Then
                        Sql2 += ",関税率 = " & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("関税率").Value.ToString) / 100
                    Else
                        Sql2 += ",関税率 = 0"
                    End If
                    If DgvItemList.Rows(index).Cells("関税額").Value IsNot Nothing Then
                        Sql2 += ",関税額 = " & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("関税額").Value.ToString)
                    Else
                        Sql2 += ",関税額 = 0"
                    End If
                    If DgvItemList.Rows(index).Cells("前払法人税率").Value IsNot Nothing Then
                        Sql2 += ",前払法人税率 = " & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("前払法人税率").Value.ToString) / 100
                    Else
                        Sql2 += ",前払法人税率 = 0"
                    End If
                    If DgvItemList.Rows(index).Cells("前払法人税額").Value IsNot Nothing Then
                        Sql2 += ",前払法人税額 = " & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("前払法人税額").Value.ToString)
                    Else
                        Sql2 += ",前払法人税額 = 0"
                    End If
                    If DgvItemList.Rows(index).Cells("輸送費率").Value IsNot Nothing Then
                        Sql2 += ",輸送費率 = " & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("輸送費率").Value.ToString) / 100
                    Else
                        Sql2 += ",輸送費率 = 0"
                    End If
                    If DgvItemList.Rows(index).Cells("輸送費額").Value IsNot Nothing Then
                        Sql2 += ",輸送費額 = " & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("輸送費額").Value.ToString)
                    Else
                        Sql2 += ",輸送費額 = 0"
                    End If
                    If DgvItemList.Rows(index).Cells("売単価").Value IsNot Nothing Then
                        Sql2 += ",売単価 = " & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("売単価").Value.ToString)
                    Else
                        Sql2 += ",売単価 = 0"
                    End If
                    If DgvItemList.Rows(index).Cells("売上金額").Value IsNot Nothing Then
                        Sql2 += ",売上金額 = " & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("売上金額").Value.ToString)
                    Else
                        Sql2 += ",売上金額 = 0"
                    End If

                    If DgvItemList.Rows(index).Cells("見積単価").Value IsNot Nothing Then
                        Sql2 += ",見積単価 = " & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("見積単価").Value.ToString)
                    Else
                        Sql2 += ",見積単価 = 0"
                    End If

                    If DgvItemList.Rows(index).Cells("見積単価_外貨").Value IsNot Nothing Then
                        Sql2 += ",見積単価_外貨 = " & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("見積単価_外貨").Value.ToString)
                    Else
                        Sql2 += ",見積単価_外貨 = 0"
                    End If

                    If DgvItemList.Rows(index).Cells("見積金額").Value IsNot Nothing Then
                        Sql2 += ",見積金額 = " & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("見積金額").Value.ToString)
                    Else
                        Sql2 += ",見積金額 = 0"
                    End If

                    If DgvItemList.Rows(index).Cells("見積金額_外貨").Value IsNot Nothing Then
                        Sql2 += ",見積金額_外貨 = " & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("見積金額_外貨").Value.ToString)
                    Else
                        Sql2 += ",見積金額_外貨 = 0"
                    End If

                    If DgvItemList.Rows(index).Cells("粗利額").Value IsNot Nothing Then
                        Sql2 += ",粗利額 = " & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("粗利額").Value.ToString)
                    Else
                        Sql2 += ",粗利額 = 0"
                    End If
                    If DgvItemList.Rows(index).Cells("粗利率").Value IsNot Nothing Then
                        Sql2 += ",粗利率 = " & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("粗利率").Value.ToString)
                    Else
                        Sql2 += ",粗利率 = 0"
                    End If
                    Sql2 += ",リードタイム = '" & RevoveChars(DgvItemList.Rows(index).Cells("リードタイム").Value.ToString) & "' "
                    If DgvItemList.Rows(index).Cells("リードタイム単位").Value IsNot Nothing And IsNumeric(DgvItemList.Rows(index).Cells("リードタイム単位").Value) Then
                        Sql2 += ",リードタイム単位 = " & DgvItemList.Rows(index).Cells("リードタイム単位").Value.ToString
                    Else
                        Sql2 += ",リードタイム単位 = 1"
                    End If

                    Sql2 += ",備考 = '" & RevoveChars(DgvItemList.Rows(index).Cells("備考").Value.ToString) & "' "
                    Sql2 += ",更新者 = '" & Input & "' "
                    Sql2 += ",登録日 = '" & UtilClass.strFormatDate(DtpRegistration.Text) & "' "
                    Sql2 += ",通貨 = " & CmCurrency.SelectedValue.ToString
                    Sql2 += ",レート = " & UtilClass.formatNumberF10(TxtRate.Text)
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
                Sql1 += ", 入力担当者, 備考, ＶＡＴ, 取消区分, 登録日, 更新日, 更新者, 見積金額_外貨, 通貨, レート)"
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
                    '国内の判定
                    If txtDomesticArea.Text = CommonConst.DD_KBN_DOMESTIC Then  '国内
                        Sql1 += UtilClass.formatNumber(TxtVat.Text)
                    Else  '国外
                        Sql1 += UtilClass.formatNumber(0)
                    End If
                End If

                Sql1 += "',0"                                                    '取消区分
                Sql1 += ", '" & UtilClass.strFormatDate(DtpRegistration.Text) & "'"       '登録日
                Sql1 += ", '" & strToday & "'"                                   '更新日
                Sql1 += ", '" & Input & "'"                                    '更新者
                Sql1 += ", " & UtilClass.formatNumber(TxtCurrencyQuoteTotal.Text)     '見積金額_外貨
                Sql1 += ", " & CmCurrency.SelectedValue.ToString                '通貨
                Sql1 += ", " & UtilClass.formatNumberF10(TxtRate.Text)             'レート
                Sql1 += ")"

                _db.executeDB(Sql1)

                Dim Sql2 As String = ""
                For index As Integer = 0 To DgvItemList.Rows.Count - 1
                    Sql2 = ""
                    Sql2 += "INSERT INTO Public.t02_mitdt("
                    Sql2 += "会社コード, 見積番号, 見積番号枝番, 行番号, 仕入区分, メーカー, 品名, 型式, 数量, 単位"
                    Sql2 += ", 仕入先コード, 仕入先名称, 仕入単価, 仕入原価, 関税率, 関税額, 前払法人税率, 前払法人税額"
                    Sql2 += ", 輸送費率, 輸送費額, 仕入金額, 売単価, 売上金額, 見積単価, 見積単価_外貨, 見積金額, 見積金額_外貨"
                    Sql2 += ", 粗利額, 粗利率, リードタイム, リードタイム単位, 備考, 更新者, 登録日, 通貨, レート"
                    Sql2 += ", 仕入単価_外貨, 仕入通貨, 仕入レート)"
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
                        Sql2 += " ," & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("数量").Value.ToString)
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
                        Sql2 += " ," & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("関税率").Value.ToString / 100)
                    Else
                        Sql2 += " ,0"
                    End If
                    If DgvItemList.Rows(index).Cells("関税額").Value IsNot Nothing Then    '関税額
                        Sql2 += " ," & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("関税額").Value.ToString)
                    Else
                        Sql2 += " ,0"
                    End If
                    If DgvItemList.Rows(index).Cells("前払法人税率").Value IsNot Nothing Then     '前払法人税率
                        Sql2 += " ," & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("前払法人税率").Value.ToString / 100)
                    Else
                        Sql2 += " ,0"
                    End If
                    If DgvItemList.Rows(index).Cells("前払法人税額").Value IsNot Nothing Then     '前払法人税額
                        Sql2 += " ," & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("前払法人税額").Value.ToString)
                    Else
                        Sql2 += " ,0"
                    End If
                    If DgvItemList.Rows(index).Cells("輸送費率").Value IsNot Nothing Then       '輸送費率
                        Sql2 += " ," & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("輸送費率").Value.ToString / 100)
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
                    If DgvItemList.Rows(index).Cells("見積単価_外貨").Value IsNot Nothing Then       '見積単価_外貨
                        Sql2 += " ," & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("見積単価_外貨").Value.ToString)
                    Else
                        Sql2 += " ,0"
                    End If
                    If DgvItemList.Rows(index).Cells("見積金額").Value IsNot Nothing Then       '見積金額
                        Sql2 += " ," & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("見積金額").Value.ToString)
                    Else
                        Sql2 += " ,0"
                    End If
                    If DgvItemList.Rows(index).Cells("見積金額_外貨").Value IsNot Nothing Then       '見積金額_外貨
                        Sql2 += " ," & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("見積金額_外貨").Value.ToString)
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
                    Sql2 += ", " & CmCurrency.SelectedValue.ToString                '通貨
                    Sql2 += ", " & UtilClass.formatNumberF10(TxtRate.Text)             'レート

                    If DgvItemList.Rows(index).Cells("仕入単価_外貨").Value IsNot Nothing Then   '仕入単価_外貨
                        Sql2 += " ," & UtilClass.formatNumber(DgvItemList.Rows(index).Cells("仕入単価_外貨").Value.ToString)
                    Else
                        Sql2 += " ,0"
                    End If

                    Sql2 += " ," & RevoveChars(DgvItemList.Rows(index).Cells("仕入通貨").Value.ToString)    '仕入通貨

                    If DgvItemList.Rows(index).Cells("仕入レート").Value IsNot Nothing Then
                        Sql2 += "," & UtilClass.formatNumberF10(DgvItemList.Rows(index).Cells("仕入レート").Value.ToString)    '仕入レート
                    Else
                        Sql2 += ",0"
                    End If

                    Sql2 += " )"

                    _db.executeDB(Sql2)
                Next

                '登録完了メッセージ
                _msgHd.dspMSG("completeInsert", frmC01F10_Login.loginValue.Language)

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
        'Sql1 += ", 見積金額 "
        Sql1 += ", 見積金額_外貨 "
        Sql1 += ", ＶＡＴ "
        Sql1 += ", 登録日 "
        Sql1 += ", 更新日 "
        Sql1 += ", 更新者 "
        Sql1 += ", 通貨 "
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
        'Sql3 += ", 見積単価 "
        Sql3 += ", 見積単価_外貨 "
        'Sql3 += ", 見積金額 "
        Sql3 += ", 見積金額_外貨 "
        Sql3 += ", 備考 "
        Sql3 += ", 登録日 "
        Sql3 += ", 仕入単価_外貨 "
        Sql3 += ", 仕入通貨 "
        Sql3 += ", 仕入レート "
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

            sheet.Range("P19").Value = "Unit Price(" & getCurrency(CmnData("通貨")) & ")" '通貨

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
                sheet.Range(cell).Value = ds3.Tables(RS).Rows(index)("見積単価_外貨")
                cell = "T" & currentCnt
                sheet.Range(cell).Value = ds3.Tables(RS).Rows(index)("見積金額_外貨")

                totalPrice = totalPrice + ds3.Tables(RS).Rows(index)("見積金額_外貨")

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
        Sql3 += " AND (仕入単価 is null or 仕入単価 = 0)"
        Sql3 += " ORDER BY 行番号"

        Dim ds3 = _db.selectDB(Sql3, RS, reccnt)

        If ds3.tables(RS).rows.count = 0 Then
            '対象データがないメッセージを表示
            _msgHd.dspMSG("NonData", frmC01F10_Login.loginValue.Language)

            Exit Sub
        End If

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

                sheet.Range("F2").Value = CmnData("見積番号") & "-" & CmnData("見積番号枝番")
                sheet.Range("F3").Value = System.DateTime.Now.ToShortDateString()
                sheet.Range("A12").Value = supplierlist(i)
                sheet.Range("A14").Value = sheet.Range("A14").Value & DateAdd("d", 5, System.DateTime.Today).ToShortDateString() & "."
                sheet.Range("F19").Value = CmnData("営業担当者")
                sheet.Range("F20").Value = CmnData("入力担当者")

                Dim rowCnt As Integer = 0
                Dim lstRow As Integer = 23

                For j As Integer = 0 To ds3.tables(RS).rows.count - 1

                    If supplierlist(i) = ds3.tables(RS).rows(j)("仕入先名称").ToString() And ds3.tables(RS).rows(j)("仕入単価") <= 0 Then
                        If lstRow = 23 Then
                            sheet.Range("A" & lstRow).Value = (j + 1).ToString
                            sheet.Range("B" & lstRow).Value = ds3.tables(RS).rows(j)("メーカー") & vbLf & ds3.tables(RS).rows(j)("品名") & vbLf & ds3.tables(RS).rows(j)("型式")
                            sheet.Range("C" & lstRow).Value = ds3.tables(RS).rows(j)("数量") & " " & ds3.tables(RS).rows(j)("単位")
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

                            sheet.Range("A" & lstRow & ":F" & lstRow).Borders(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous
                            sheet.Range("A" & lstRow & ":F" & lstRow).Borders(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous
                            sheet.Range("A" & lstRow & ":F" & lstRow).Borders(Excel.XlBordersIndex.xlEdgeLeft).LineStyle = Excel.XlLineStyle.xlContinuous
                            sheet.Range("A" & lstRow & ":F" & lstRow).Borders(Excel.XlBordersIndex.xlEdgeRight).LineStyle = Excel.XlLineStyle.xlContinuous
                            sheet.Range("A" & lstRow & ":F" & lstRow).Font.Size = 9
                            sheet.Range("A" & lstRow).Borders(Excel.XlBordersIndex.xlEdgeRight).LineStyle = Excel.XlLineStyle.xlContinuous
                            sheet.Range("B" & lstRow).Borders(Excel.XlBordersIndex.xlEdgeRight).LineStyle = Excel.XlLineStyle.xlContinuous
                            sheet.Range("C" & lstRow).Borders(Excel.XlBordersIndex.xlEdgeRight).LineStyle = Excel.XlLineStyle.xlContinuous
                            sheet.Range("D" & lstRow).Borders(Excel.XlBordersIndex.xlEdgeRight).LineStyle = Excel.XlLineStyle.xlContinuous
                            sheet.Range("E" & lstRow).Borders(Excel.XlBordersIndex.xlEdgeRight).LineStyle = Excel.XlLineStyle.xlContinuous
                            sheet.Range("F" & lstRow).Borders(Excel.XlBordersIndex.xlEdgeRight).LineStyle = Excel.XlLineStyle.xlContinuous

                            sheet.Range("A" & lstRow).Value = (j + 1).ToString
                            sheet.Range("B" & lstRow).Value = ds3.tables(RS).rows(j)("メーカー") & vbLf & ds3.tables(RS).rows(j)("品名") & vbLf & ds3.tables(RS).rows(j)("型式")
                            sheet.Range("C" & lstRow).Value = ds3.tables(RS).rows(j)("数量") & " " & ds3.tables(RS).rows(j)("単位")
                            sheet.Range("C" & lstRow).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter

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

            '言語の判定
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then  '英語

                sheet.Range("A1").Value = "QuotationNo"　 '見積番号
                sheet.Range("A2").Value = "Customer"　    '得意先名称
                sheet.Range("A3").Value = "Address"       '住所
                sheet.Range("A6").Value = "PaymentTerms"　'支払条件
                sheet.Range("A7").Value = "Remarks"　     '備考

                sheet.Range("I1").Value = "QuotationDate(RegistrationDate)"　           '見積日
                sheet.Range("I2").Value = "PhoneNumber"        　'電話番号
                sheet.Range("I3").Value = "FAX"                　'FAX番号
                sheet.Range("I4").Value = "NameOfPIC"　          '得意先担当者名
                sheet.Range("I5").Value = "PositionPICCustomer"　'得意先担当者役職
                sheet.Range("I6").Value = "CustomsDutyRate"    　'関税率
                sheet.Range("I7").Value = "PrepaidCorporateTaxRate"　'前払法人税率
                sheet.Range("I8").Value = "TransportationCostRate"　 '輸送費率

                sheet.Range("N1").Value = "ExpirationDate"     　'見積有効期限
                sheet.Range("N2").Value = "SalesPersonInCharge"　'営業担当者
                sheet.Range("N3").Value = "PICForInputting"      '入力担当者

                sheet.Range("A10").Value = "No"　                                'No
                sheet.Range("B10").Value = "PurchasingClassification"        　'仕入区分
                sheet.Range("C10").Value = "Manufacturer"                    　'メーカー
                sheet.Range("D10").Value = "ItemName"　                        '品名
                sheet.Range("E10").Value = "Spec"                            　'型式
                sheet.Range("F10").Value = "Quantity"　                        '数量
                sheet.Range("G10").Value = "Unit"　                            '単位
                sheet.Range("H10").Value = "SupplierName"　                    '仕入先
                sheet.Range("I10").Value = "PurchaseUnitPrice"               　'仕入単価
                sheet.Range("J10").Value = "PurchaseCost"　                    '仕入原価
                sheet.Range("K10").Value = "CustomsDutyParUnit"              　'関税額
                sheet.Range("L10").Value = "PrepaidCorporateTaxAmountParUnit"　'前払法人税額
                sheet.Range("M10").Value = "TransportationCostParUnit"　       '輸送費額
                sheet.Range("N10").Value = "PurchaseAmount"　                 '仕入金額
                sheet.Range("O10").Value = "SellingPrice"　                   '売単価
                sheet.Range("P10").Value = "SalesAmount"　                    '売上金額
                sheet.Range("Q10").Value = "QuotationPrice"　                 '見積単価
                sheet.Range("R10").Value = "QuotationAmount"　                '見積金額
                sheet.Range("S10").Value = "GrossMargin"                      '粗利額
                sheet.Range("T10").Value = "GrossMarginRate"                    　'粗利率
                sheet.Range("U10").Value = "LeadTime"　                       'リードタイム

            End If

            sheet.Range("B1").Value = QuoteNo & "-" & QuoteSuffix
            sheet.Range("J1").Value = QuoteDate & "(" & RegistrationDate & ")"
            sheet.Range("O1").Value = Expiration
            sheet.Range("B2").Value = CustomerName
            sheet.Range("B3").Value = PostalCode & " " & Address1
            sheet.Range("J2").Value = Tel
            sheet.Range("J3").Value = Fax
            sheet.Range("J4").Value = Person
            sheet.Range("J5").Value = Position
            sheet.Range("O2").Value = Sales
            sheet.Range("O3").Value = Input
            sheet.Range("B6").Value = PaymentTerms
            sheet.Range("B7").Value = QuoteRemarks
            sheet.Range("U15").Value = PurchaseTotal        '仕入
            sheet.Range("U16").Value = Total                '売上
            sheet.Range("U17").Value = QuoteAmount          '見積
            sheet.Range("U18").Value = GrossProfitAmount    '粗利



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
                    sheet.Range("J6").Value = DgvItemList.Rows(index).Cells("関税率").Value
                End If
                If DgvItemList.Rows(index).Cells("前払法人税率").Value <> 0 Then
                    sheet.Range("J7").Value = DgvItemList.Rows(index).Cells("前払法人税率").Value
                End If
                If DgvItemList.Rows(index).Cells("輸送費率").Value <> 0 Then
                    sheet.Range("J8").Value = DgvItemList.Rows(index).Cells("輸送費率").Value
                End If


                sheet.Range("A" & currentRow).Value = DgvItemList.Rows(index).Cells("No").Value
                sheet.Range("B" & currentRow).Value = getSireKbnName(DgvItemList.Rows(index).Cells("仕入区分").Value)
                sheet.Range("C" & currentRow).Value = DgvItemList.Rows(index).Cells("メーカー").Value
                sheet.Range("D" & currentRow).Value = DgvItemList.Rows(index).Cells("品名").Value
                sheet.Range("E" & currentRow).Value = DgvItemList.Rows(index).Cells("型式").Value
                sheet.Range("F" & currentRow).Value = DgvItemList.Rows(index).Cells("数量").Value
                sheet.Range("G" & currentRow).Value = DgvItemList.Rows(index).Cells("単位").Value
                sheet.Range("H" & currentRow).Value = DgvItemList.Rows(index).Cells("仕入先").Value
                sheet.Range("I" & currentRow).Value = DgvItemList.Rows(index).Cells("仕入単価").Value
                sheet.Range("J" & currentRow).Value = DgvItemList.Rows(index).Cells("仕入原価").Value
                sheet.Range("K" & currentRow).Value = DgvItemList.Rows(index).Cells("関税額").Value
                sheet.Range("L" & currentRow).Value = DgvItemList.Rows(index).Cells("前払法人税額").Value
                sheet.Range("M" & currentRow).Value = DgvItemList.Rows(index).Cells("輸送費額").Value
                sheet.Range("N" & currentRow).Value = DgvItemList.Rows(index).Cells("仕入金額").Value
                sheet.Range("O" & currentRow).Value = DgvItemList.Rows(index).Cells("売単価").Value
                sheet.Range("P" & currentRow).Value = DgvItemList.Rows(index).Cells("売上金額").Value
                sheet.Range("Q" & currentRow).Value = DgvItemList.Rows(index).Cells("見積単価").Value
                sheet.Range("R" & currentRow).Value = DgvItemList.Rows(index).Cells("見積金額").Value
                sheet.Range("S" & currentRow).Value = DgvItemList.Rows(index).Cells("粗利額").Value
                sheet.Range("T" & currentRow).Value = DgvItemList.Rows(index).Cells("粗利率").Value
                tmp = ""
                tmp += DgvItemList.Rows(index).Cells("リードタイム").Value
                tmp += DgvItemList.Item("リードタイム単位", index).FormattedValue
                sheet.Range("U" & currentRow).Value = tmp

                currentRow += 1

            Next

            currentRow += 2

            '言語の判定
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then  '英語

                sheet.Range("T" & currentRow).Value = "PurchaseAmount"     '仕入金額
                sheet.Range("T" & currentRow + 1).Value = "SalesAmount"    '売上金額
                sheet.Range("T" & currentRow + 2).Value = "QuotationAmount" '見積金額
                sheet.Range("T" & currentRow + 3).Value = "GrossMargin"     '粗利額
            End If


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

    '得意先検索ボタンクリック
    '
    Private Sub BtnCodeSearch_Click(sender As Object, e As EventArgs) Handles BtnCodeSearch.Click
        '得意先マスタ読み込み
        Read_Customer()

    End Sub

    'キーイベント取得
    Private Sub DgvItemList_KeyDown(sender As Object, e As KeyEventArgs) Handles DgvItemList.KeyDown
        'F4キー押下
        If e.KeyData <> Keys.F4 Then
            Exit Sub
        End If

        Dim currentColumn As String = DgvItemList.Columns(DgvItemList.CurrentCell.ColumnIndex).Name
        Dim sireKbn As Integer = DgvItemList("仕入区分", DgvItemList.CurrentCell.RowIndex).Value

        Dim manufactuer As String = DgvItemList("メーカー", DgvItemList.CurrentCell.RowIndex).Value
        Dim itemName As String = DgvItemList("品名", DgvItemList.CurrentCell.RowIndex).Value
        Dim spec As String = DgvItemList("型式", DgvItemList.CurrentCell.RowIndex).Value

        'メーカー、品名、型式にいた場合
        If currentColumn = "メーカー" Or currentColumn = "品名" Or currentColumn = "型式" Then
            '各項目チェック
            If currentColumn = "型式" And (manufactuer Is Nothing And itemName Is Nothing) Then
                'メーカー、品名を入力してください。
                _msgHd.dspMSG("chkManufacturerItemNameError", frmC01F10_Login.loginValue.Language)
                Return

            ElseIf currentColumn = "品名" And (manufactuer Is Nothing) Then
                'メーカーを入力してください。
                _msgHd.dspMSG("chkManufacturerError", frmC01F10_Login.loginValue.Language)
                Return
            End If

            Dim openForm As Form = Nothing
            openForm = New MakerSearch(_msgHd, _db, Me, DgvItemList.CurrentCell.RowIndex, DgvItemList.CurrentCell.ColumnIndex, manufactuer, itemName, spec, currentColumn, CommonConst.STATUS_ADD)   '処理選択
            openForm.Show(Me)
            Me.Enabled = False
        End If

        If currentColumn = "仕入先" Then '仕入先検索
            Dim openForm As Form = Nothing
            openForm = New SupplierSearch(_msgHd, _db, _langHd, DgvItemList.CurrentCell.RowIndex, Me)
            openForm.Show(Me)
            Me.Enabled = False
        End If


        '仕入区分[ 在庫引当 ] + 数量にいた場合
        If sireKbn = CommonConst.Sire_KBN_Zaiko And currentColumn = "数量" Then
            manufactuer = IIf(manufactuer <> Nothing, manufactuer, "")
            itemName = IIf(itemName <> Nothing, itemName, "")
            spec = IIf(spec <> Nothing, spec, "")

            Dim openForm As Form = Nothing
            openForm = New StockSearch(_msgHd, _db, _langHd, Me, manufactuer, itemName, spec, "Normal")
            openForm.Show()
            Me.Enabled = False

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
            txtDomesticArea.Text = dsCode.Tables(RS).Rows(0)("国内区分").ToString      '非表示
        End If

        'VAT再計算
        Call mCalVat_Out(TxtQuoteTotal.Text)

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

    'param1：String テーブル名
    'param2：String 詳細条件
    'Return: DataSet
    Private Function getDsData(ByVal tableName As String, Optional ByRef txtParam As String = "") As DataSet
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Sql += "SELECT * FROM "

        Sql += "public." & tableName
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' "
        Sql += txtParam

        Return _db.selectDB(Sql, RS, reccnt)
    End Function

    'Return: String
    Private Function getSireKbnName(argSiireKBN) As String
        Dim Sql As String = ""
        Dim strViewText As String = ""

        Sql = " AND 固定キー = '" & CommonConst.FIXED_KEY_PURCHASING_CLASS & "'"
        Sql += " AND 可変キー = '" & argSiireKBN & "'"

        '汎用マスタから取得
        Dim dsHanyo As DataSet = getDsData("m90_hanyo", Sql)
        Dim retString As String = ""

        strViewText = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "文字２", "文字１")
        retString = dsHanyo.Tables(RS).Rows(0)(strViewText).ToString
        Return retString

    End Function


    'Return: DataTable
    Private Function getSireKbn() As DataTable
        Dim Sql As String = ""
        Dim strViewText As String = ""

        Sql = " AND 固定キー = '" & CommonConst.FIXED_KEY_PURCHASING_CLASS & "'"
        Sql += " AND 可変キー <> '" & CommonConst.Sire_KBN_Move & "'" '「移動」以外
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
        Sql += " AND 基準日 < '" & UtilClass.strFormatDate(DtpQuote.Text) & "'"
        Sql += " ORDER BY 基準日 DESC "

        Dim ds As DataSet = getDsData("t71_exchangerate", Sql)

        If ds.Tables(RS).Rows.Count > 0 Then
            TxtRate.Text = ds.Tables(RS).Rows(0)("レート")
        Else
            TxtRate.Text = 1.ToString("F10")
        End If

    End Sub

    '基準通貨の通貨コードを取得する
    Private Function setBaseCurrency() As String
        Dim Sql As String
        '通貨表示：ベースの設定
        Sql = " AND 採番キー = " & CommonConst.CURRENCY_CD_IDR.ToString
        Sql += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString

        Dim ds As DataSet = getDsData("m25_currency", Sql)
        setBaseCurrency = ds.Tables(RS).Rows(0)("通貨コード")

    End Function

    '通貨表示：通貨変更の設定
    Private Sub setChangeCurrency()
        Dim Sql As String
        Sql = " AND 採番キー = " & CmCurrency.SelectedValue.ToString
        Sql += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString

        Dim ds As DataSet = getDsData("m25_currency", Sql)
        TxtChangeCurrency.Text = ds.Tables(RS).Rows(0)("通貨コード")
    End Sub

    '見積日が変更されたらレートを更新する
    Private Sub DtpQuote_Validated(sender As Object, e As EventArgs) Handles DtpQuote.Validated
        setRate()
    End Sub

    '通貨コンボボックスを変更したら
    Private Sub CmCurrency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmCurrency.SelectedIndexChanged
        setRate()
        setChangeCurrency()
        resetListCurrency()  '明細
        setCurrency()        'フッター
    End Sub

    'Currencyに応じて変換
    Private Sub setCurrency()

        Dim currencyVal As Decimal = IIf(TxtRate.Text <> "", TxtRate.Text, 0)

        Dim vatVal As Decimal = IIf(TxtVatAmount.Text <> "", TxtVatAmount.Text, 0)

        Dim QuoteCurrencyTotal As Decimal = 0       '見積金額_外貨

        Dim Total As Decimal = 0          '売上金額
        Dim PurchaseCost As Decimal = 0   '仕入原価
        Dim GrossProfit As Decimal = 0    '粗利額
        Dim QuoteTotal As Decimal = 0     '見積金額
        Dim PurchaseTotal As Decimal = 0  '仕入金額
        Dim ProfitMargin As Decimal = 0　 '利益額


        TxtCurrencyVatAmount.Text = (vatVal * currencyVal).ToString("N2")

        For c As Integer = 0 To DgvItemList.Rows.Count - 1
            If Not DgvItemList.Rows(c).Cells("見積金額_外貨").Value Is DBNull.Value Then
                QuoteCurrencyTotal += CDec(DgvItemList.Rows(c).Cells("見積金額_外貨").Value)
            End If

            Total += DgvItemList.Rows(c).Cells("売上金額").Value
            PurchaseCost += DgvItemList.Rows(c).Cells("仕入原価").Value
            GrossProfit += DgvItemList.Rows(c).Cells("粗利額").Value

            QuoteTotal += DgvItemList.Rows(c).Cells("見積金額").Value
            PurchaseTotal += DgvItemList.Rows(c).Cells("仕入金額").Value
        Next

        TxtCurrencyQuoteTotal.Text = QuoteCurrencyTotal.ToString("N2")
        TxtCurrencyVatAmount.Text = ((QuoteCurrencyTotal.ToString("N0") * TxtVat.Text) / 100).ToString("N2")


        TxtTotal.Text = Total.ToString("N2")                '売上金額
        txtPurchasecost.Text = PurchaseCost.ToString("N2")  '仕入原価
        TxtGrossProfit.Text = GrossProfit.ToString("N2")    '粗利額

        '粗利率
        If TxtTotal.Text = 0 Or txtPurchasecost.Text = 0 Then
            txtGrossmarginRate.Text = 0.ToString("N1")
        Else
            txtGrossmarginRate.Text = ((GrossProfit / Total) * 100).ToString("N1")
        End If


        TxtQuoteTotal.Text = QuoteTotal.ToString("N2")        '見積金額
        TxtPurchaseTotal.Text = PurchaseTotal.ToString("N2")  '仕入金額

        '利益額
        If TxtQuoteTotal.Text = 0 Or TxtPurchaseTotal.Text = 0 Then
            txtProfitmargin.Text = 0.ToString("N2")
        Else
            ProfitMargin = QuoteTotal - PurchaseTotal
            txtProfitmargin.Text = ProfitMargin.ToString("N2")
        End If

        '利益率
        If TxtQuoteTotal.Text = 0 Or TxtPurchaseTotal.Text = 0 Then
            txtProfitmarginRate.Text = 0.ToString("N1")
        Else
            txtProfitmarginRate.Text = ((ProfitMargin / QuoteTotal) * 100).ToString("N1")
        End If

        'VAT
        Call mCalVat_Out(QuoteTotal)

    End Sub

    Private Sub mCalVat_Out(ByVal QuoteTotal As Decimal)

        '国内区分を判定
        '国内のみVATを計算
        If txtDomesticArea.Text = CommonConst.DD_KBN_DOMESTIC Then  '国内
            TxtVatAmount.Text = ((QuoteTotal * TxtVat.Text) / 100).ToString("N2") 'VAT-OUT
        Else  '国外
            TxtVatAmount.Text = "0.00"
        End If

    End Sub

    Private Sub setAddHandler()
        '入力タイプのイベントハンドラーをセット
        AddHandler RbtnGP.CheckedChanged, AddressOf RbtnGP_CheckedChanged
        AddHandler RbtnUP.CheckedChanged, AddressOf RbtnGP_CheckedChanged
        AddHandler RbtnQuote.CheckedChanged, AddressOf RbtnGP_CheckedChanged
    End Sub


    Private Sub setCellValueChanged()

        AddHandler DgvItemList.CellValueChanged, AddressOf CellValueChanged
    End Sub
    Private Sub delCellValueChanged()

        RemoveHandler DgvItemList.CellValueChanged, AddressOf CellValueChanged
    End Sub

    Private Sub resetListCurrency()
        If DgvItemList.Rows.Count > 0 Then
            For i As Integer = 0 To DgvItemList.Rows.Count - 1
                delCellValueChanged()
                delCellValueChanged()

                '売単価_外貨
                If DgvItemList.Rows(i).Cells("売単価_外貨").Value IsNot Nothing And DgvItemList.Rows(i).Cells("数量").Value IsNot Nothing Then
                    '小数点表示にするため切り上げをコメントアウト
                    'DgvItemList.Rows(i).Cells("売単価").Value = Math.Ceiling(DgvItemList.Rows(i).Cells("売単価_外貨").Value / TxtRate.Text)
                    DgvItemList.Rows(i).Cells("売単価").Value = DgvItemList.Rows(i).Cells("売単価_外貨").Value / TxtRate.Text
                    DgvItemList.Rows(i).Cells("売上金額").Value = Math.Truncate(DgvItemList.Rows(i).Cells("売単価").Value * DgvItemList.Rows(i).Cells("数量").Value)
                End If


                '見積単価_外貨
                If DgvItemList.Rows(i).Cells("見積単価_外貨").Value IsNot Nothing And DgvItemList.Rows(i).Cells("数量").Value IsNot Nothing Then

                    '小数点表示にするため切り上げをコメントアウト
                    'DgvItemList.Rows(i).Cells("見積単価").Value = Math.Ceiling(
                    DgvItemList.Rows(i).Cells("見積単価").Value = Decimal.Parse(
                                                                  rmNullDecimal(DgvItemList.Rows(i).Cells("売単価").Value) + rmNullDecimal(DgvItemList.Rows(i).Cells("関税額").Value) + rmNullDecimal(DgvItemList.Rows(i).Cells("前払法人税額").Value) + rmNullDecimal(DgvItemList.Rows(i).Cells("輸送費額").Value))

                    If (DgvItemList.Rows(i).Cells("関税額").Value Is Nothing Or DgvItemList.Rows(i).Cells("関税額").Value = 0) _
                                And (DgvItemList.Rows(i).Cells("前払法人税額").Value Is Nothing Or DgvItemList.Rows(i).Cells("前払法人税額").Value = 0) _
                                And (DgvItemList.Rows(i).Cells("輸送費額").Value Is Nothing Or DgvItemList.Rows(i).Cells("輸送費額").Value = 0) Then

                        DgvItemList.Rows(i).Cells("見積単価_外貨").Value = Math.Truncate(DgvItemList.Rows(i).Cells("売単価_外貨").Value * 100) / 100
                    Else
                        '小数点表示にするため切り上げをコメントアウト
                        'DgvItemList.Rows(i).Cells("見積単価_外貨").Value = Math.Ceiling(DgvItemList.Rows(i).Cells("見積単価").Value * TxtRate.Text)
                        DgvItemList.Rows(i).Cells("見積単価_外貨").Value = Math.Truncate(DgvItemList.Rows(i).Cells("見積単価").Value * TxtRate.Text * 100) / 100

                    End If

                    DgvItemList.Rows(i).Cells("見積金額").Value = Math.Truncate(DgvItemList.Rows(i).Cells("見積単価").Value * DgvItemList.Rows(i).Cells("数量").Value）
                    DgvItemList.Rows(i).Cells("見積金額_外貨").Value = Math.Round(DgvItemList.Rows(i).Cells("見積単価_外貨").Value * DgvItemList.Rows(i).Cells("数量").Value * 100） / 100
                End If
                setCellValueChanged()
            Next
        End If
    End Sub

    Private Function setSireCurrency(Optional ByRef prmCurrencyVal As Integer = CommonConst.CURRENCY_CD_IDR)
        Dim retVal As Decimal
        '通貨コードが1（IDR）の場合、
        If prmCurrencyVal = CommonConst.CURRENCY_CD_IDR Then
            'currentが日本だったら
            'If CultureInfo.CurrentCulture.Name.ToString = CommonConst.CI_ID Then
            '    retVal = Decimal.Parse(CommonConst.BASE_RATE_IDR)
            'Else
            '    retVal = Decimal.Parse(CommonConst.BASE_RATE_JPY)
            'End If
            retVal = 1.ToString("F10")
        Else
            '基準日よりも古い最新のレートを取得
            Dim Sql As String = ""
            Sql = " AND 採番キー =" & prmCurrencyVal
            Sql += " AND 基準日 < '" & UtilClass.strFormatDate(DtpQuote.Text) & "'"
            Sql += " ORDER BY 基準日 DESC "

            Dim ds As DataSet = getDsData("t71_exchangerate", Sql)
            If ds.Tables(RS).Rows.Count > 0 Then
                retVal = ds.Tables(RS).Rows(0)("レート")
            End If
        End If
        Return retVal
    End Function

    Private Function getCurrency(ByVal prmVal As Integer) As String
        Dim Sql As String = " AND 取消区分 = '" & CommonConst.FLAG_ENABLED & "'"
        Sql += " AND 採番キー =" & prmVal.ToString

        Dim ds As DataSet = getDsData("m25_currency", Sql)

        'リードタイム単位の多言語対応

        Return ds.Tables(RS).Rows(0)("通貨コード")

    End Function

    Private Sub TxtCustomerCode_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtCustomerCode.KeyDown

        '得意先コード欄でＦ４キーを押下した時は検索画面を表示
        If e.KeyCode <> Keys.F4 Then
            Exit Sub
        End If


        Dim openForm As Form = Nothing
        openForm = New CustomerSearch(_msgHd, _db, _langHd, Me) '処理選択
        openForm.ShowDialog(Me)
        openForm.Dispose()

        Read_Customer() '得意先マスタ読み込み

    End Sub


    Private Sub TxtSales_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtSales.KeyDown
        '営業担当者欄でＦ４キーを押下した時は検索画面を表示
        If e.KeyCode <> Keys.F4 Then
            Exit Sub
        End If

        Dim openForm As Form = Nothing
        openForm = New SalesSearch(_msgHd, _db, _langHd, Me) '処理選択
        openForm.Show(Me)
        Me.Enabled = False

    End Sub

    Private Sub DgvItemList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvItemList.CellContentClick

    End Sub
End Class
