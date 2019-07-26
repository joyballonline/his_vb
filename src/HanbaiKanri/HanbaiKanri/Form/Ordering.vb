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
Imports System.Text.RegularExpressions
Imports System.IO

Public Class Ordering
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
    Private PurchaseNo As String = ""
    Private PurchaseSuffix As String = ""
    Private PurchaseStatus As String = ""
    Private PurchaseCount As String = ""

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
                   Optional ByRef prmRefSuffix As String = Nothing,
                   Optional ByRef prmRefStatus As String = Nothing)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm

        PurchaseNo = prmRefNo
        PurchaseSuffix = prmRefSuffix
        PurchaseStatus = prmRefStatus

        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True
        TxtInput.Text = frmC01F10_Login.loginValue.TantoNM
        CompanyCode = frmC01F10_Login.loginValue.BumonCD

    End Sub

    Public Class ComboBoxItem

        Private m_id As String = ""
        Private m_name As String = ""

        '実際の値
        '（ValueMemberに設定する文字列と同名にする）
        Public Property ID() As String
            Set(ByVal value As String)
                m_id = value
            End Set
            Get
                Return m_id
            End Get
        End Property

        '表示名称
        '（DisplayMemberに設定する文字列と同名にする）
        Public Property NAME() As String
            Set(ByVal value As String)
                m_name = value
            End Set
            Get
                Return m_name
            End Get
        End Property

    End Class

    '新規登録時の発注番号採番処理
    '
    Private Sub GetSiireNo_New()
        Dim reccnt As Integer = 0
        Dim dtNow As DateTime = DateTime.Now
        Dim strNow As String = UtilClass.formatDatetime(dtNow)
        Dim SqlSaiban As String = ""
        SqlSaiban += "SELECT "
        SqlSaiban += "会社コード, "
        SqlSaiban += "採番キー, "
        SqlSaiban += "最新値, "
        SqlSaiban += "最小値, "
        SqlSaiban += "最大値, "
        SqlSaiban += "接頭文字, "
        SqlSaiban += "連番桁数 "
        SqlSaiban += "FROM public.m80_saiban"
        SqlSaiban += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        SqlSaiban += " AND 採番キー = '30'"

        Dim Saiban As DataSet = _db.selectDB(SqlSaiban, RS, reccnt)

        PurchaseCount = Saiban.Tables(RS).Rows(0)(2)
        PurchaseNo = Saiban.Tables(RS).Rows(0)(5)
        PurchaseNo += dtNow.ToString("MMdd")
        PurchaseNo += PurchaseCount.PadLeft(Saiban.Tables(RS).Rows(0)(6), "0")

        PurchaseCount += 1
        Dim Saiban4 As String = ""
        Saiban4 += "UPDATE Public.m80_saiban "
        Saiban4 += "SET "
        Saiban4 += " 最新値 = '" & PurchaseCount.ToString & "'"
        Saiban4 += " , 更新者 = 'Admin'"
        Saiban4 += " , 更新日 = '" & strNow & "'"
        Saiban4 += " WHERE 会社コード ='" & frmC01F10_Login.loginValue.BumonCD & "'"
        Saiban4 += " AND 採番キー ='30' "
        _db.executeDB(Saiban4)

        TxtOrderingNo.Text = PurchaseNo
        TxtOrderingSuffix.Text = 1

    End Sub

    '画面表示時
    Private Sub Ordering_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim dtNow As DateTime = DateTime.Now
        Dim dtToday As String = formatDatetime(dtNow)

        delCellValueChanged()   'セル変更イベントを無効化


        '汎用マスタからリードタイム単位を取得
        Dim dsHanyo = getDsHanyoData(CommonConst.FIXED_KEY_READTIME)

        Dim dtReadtime As New DataTable("Table2")
        dtReadtime.Columns.Add("Display", GetType(String))
        dtReadtime.Columns.Add("Value", GetType(Integer))

        For i As Integer = 0 To dsHanyo.Tables(RS).Rows.Count - 1
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                dtReadtime.Rows.Add(dsHanyo.Tables(RS).Rows(i)("文字２"), dsHanyo.Tables(RS).Rows(i)("可変キー"))
            Else
                dtReadtime.Rows.Add(dsHanyo.Tables(RS).Rows(i)("文字１"), dsHanyo.Tables(RS).Rows(i)("可変キー"))
            End If
        Next

        Dim column2 As New DataGridViewComboBoxColumn()
        column2.DataSource = dtReadtime
        column2.ValueMember = "Value"
        column2.DisplayMember = "Display"
        column2.HeaderText = "リードタイム単位"
        column2.Name = "リードタイム単位"

        DgvItemList.Columns.Insert(21, column2)

        '汎用マスタから貿易条件を取得
        dsHanyo = getDsHanyoData(CommonConst.FIXED_KEY_TRADE_TERMS)

        Dim table3 As New DataTable("Table3")
        table3.Columns.Add("Display", GetType(String))
        table3.Columns.Add("Value", GetType(Integer))

        For index As Integer = 0 To dsHanyo.Tables(RS).Rows.Count - 1
            table3.Rows.Add(dsHanyo.Tables(RS).Rows(index)("文字１"), dsHanyo.Tables(RS).Rows(index)("可変キー"))
        Next

        Dim column3 As New DataGridViewComboBoxColumn()
        column3.DataSource = table3
        column3.ValueMember = "Value"
        column3.DisplayMember = "Display"
        column3.HeaderText = "貿易条件"
        column3.Name = "貿易条件"

        DgvItemList.Columns.Insert(22, column3)
        CbShippedBy.SelectedIndex = 0

        createWarehouseCombobox() '倉庫コンボボックス

        TxtIDRCurrency.Text = setBaseCurrency()

        'ヘッダ項目を中央寄せ
        DgvItemList.Columns("仕入単価").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("No").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("メーカー").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("品名").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("型式").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("数量").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("単位").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("仕入単価_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("仕入単価").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("仕入原価").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("関税率").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("関税額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("前払法人税率").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("前払法人税額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("輸送費率").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("輸送費額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("リードタイム").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("リードタイム単位").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("貿易条件").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("備考").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("仕入金額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("仕入金額_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter


        '翻訳
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblPurchaseNo.Text = "PurchaseOrderNo"
            LblPurchaseNo.Size = New Size(145, 23)
            TxtOrderingNo.Location = New Point(162, 12)
            Label2.Location = New Point(256, 13)
            TxtOrderingSuffix.Location = New Point(273, 13)

            LblQuoteNo.Text = "QuoteNo"
            LblQuoteNo.Size = New Size(145, 23)
            LblPurchaseNo.Size = New Size(145, 23)
            TxtQuoteNo.Location = New Point(162, 42)
            LblHyphen.Location = New Point(256, 43)
            TxtQuoteSuffix.Location = New Point(273, 43)

            LblCustomerPO.Text = "CustomerNo"
            LblCustomerPO.Location = New Point(308, 13)
            LblCustomerPO.Size = New Size(142, 23)
            TxtCustomerPO.Location = New Point(456, 13)
            LblPurchaseDate.Text = "OrderDate"
            LblPurchaseDate.Location = New Point(550, 13)
            DtpPurchaseDate.Location = New Point(668, 13)
            DtpPurchaseDate.Size = New Size(130, 22)
            LblRegistrationDate.Text = "OrderRegistrationDate"
            LblRegistrationDate.Size = New Size(158, 23)
            LblRegistrationDate.Location = New Point(802, 13)
            DtpRegistrationDate.Location = New Point(968, 13)
            DtpRegistrationDate.Size = New Size(130, 22)
            DtpRegistrationDate.Enabled = False

            LblSupplierName.Text = "SupplierName"
            LblAddress.Text = "Address"
            LblTel.Text = "PhoneNumber"
            LblFax.Text = "FAX"
            LblPerson.Text = "NameOfPIC"
            LblPosition.Text = "PositionPICSupplier"
            LblSales.Text = "SalesPersonInCharge"
            LblInput.Text = "PICForInputting"
            LblPaymentTerms.Text = "PaymentTerms"
            LblPurchaseRemarks.Text = "PurchaseRemarks"
            LblPurchaseRemarks.Font = New Font("MS UI Gothic", 9.0!, FontStyle.Regular, GraphicsUnit.Point, 128)
            LblRemarks.Font = New Font("MS UI Gothic", 9.0!, FontStyle.Regular, GraphicsUnit.Point, 128)
            LblRemarks.Text = "QuotationRemarks"
            LblItemCount.Text = "ItemCount"
            LblMethod.Text = "ShippingMethod"
            LblShipDate.Text = "ShipDate"
            LblWarehouse.Text = "Warehouse"

            lblPurchasecost.Text = "PurchaseCost（c）"      '仕入原価
            LblPurchaseAmount.Text = "PurchaseAmount（j）"  '仕入金額

            LblPurchaseAmount2.Text = "PurchaseAmount"  '仕入金額_外貨(原通貨)

            'LblPurchaseAmount.Size = New Size(180, 23)
            'LblPurchaseAmount.Location = New Point(923, 465)

            TxtSupplierCode.Size = New Point(62, 23)
            BtnCodeSearch.Text = "Search"
            BtnCodeSearch.Size = New Size(72, 23)
            BtnInsert.Text = "InsertLine"
            BtnUp.Text = "ShiftLineUp"
            BtnDown.Text = "ShiftLineDown"
            BtnRowsAdd.Text = "AddLine"
            BtnRowsDel.Text = "DeleteLine"
            BtnClone.Text = "LineDuplication"
            LblCurrency.Text = "PurchaseCurrency" '仕入通貨
            LblRate.Text = "Rate" 'レート

            LblIDRCurrency.Text = "Currency" '通貨
            LblChangeCurrency.Text = "Currency" '通貨

            BtnPurchase.Text = "IssuePurchaseOrder"
            BtnRegistration.Text = "Registrartion"
            BtnBack.Text = "Back"

            DgvItemList.Columns("メーカー").HeaderText = "Manufacturer"
            DgvItemList.Columns("品名").HeaderText = "ItemName"
            DgvItemList.Columns("型式").HeaderText = "Spec"
            DgvItemList.Columns("数量").HeaderText = "Quantity" & vbCrLf & "a"
            DgvItemList.Columns("単位").HeaderText = "Unit"

            DgvItemList.Columns("仕入単価_外貨").HeaderText = "PurchaseUnitPrice" & vbCrLf & "(OriginalCurrency)"
            DgvItemList.Columns("仕入単価").HeaderText = "PurchaseUnitPrice(" & setBaseCurrency() & ")" & vbCrLf & "b"

            DgvItemList.Columns("仕入金額").HeaderText = "PurchaseAmount" & vbCrLf & "j=a*(b+e+g+i)"
            DgvItemList.Columns("仕入金額_外貨").HeaderText = "PurchaseAmountForeignCurrency" & vbCrLf & "(OriginalCurrency)"
            DgvItemList.Columns("仕入原価").HeaderText = "PurchasingCost" & vbCrLf & "c=a*b"
            DgvItemList.Columns("関税率").HeaderText = "CustomsDutyRate" & vbCrLf & "d"
            DgvItemList.Columns("関税額").HeaderText = "CustomsDutyParUnit" & vbCrLf & "e=b*d"
            DgvItemList.Columns("前払法人税率").HeaderText = "PrepaidCorporateTaxRate" & vbCrLf & "f"
            DgvItemList.Columns("前払法人税額").HeaderText = "PrepaidCorporateTaxAmountParUnit" & vbCrLf & "g=(b+e)*f"
            DgvItemList.Columns("輸送費率").HeaderText = "TransportationCostRate" & vbCrLf & "h"
            DgvItemList.Columns("輸送費額").HeaderText = "TransportationCostParUnit" & vbCrLf & "i=b*h"
            DgvItemList.Columns("リードタイム").HeaderText = "LeadTime"
            DgvItemList.Columns("リードタイム単位").HeaderText = "LeadTimeUnit"
            DgvItemList.Columns("貿易条件").HeaderText = "TradeTerms"
            DgvItemList.Columns("備考").HeaderText = "Remarks"

        Else
            '日本語用見出し
            DgvItemList.Columns("数量").HeaderText = "数量" & vbCrLf & "a"
            DgvItemList.Columns("仕入単価_外貨").HeaderText = "仕入単価" & vbCrLf & "（原通貨）"
            DgvItemList.Columns("仕入単価").HeaderText = "仕入単価(" & setBaseCurrency() & ")" & vbCrLf & "b"
            DgvItemList.Columns("仕入原価").HeaderText = "仕入原価" & vbCrLf & "c=a*b"
            DgvItemList.Columns("関税率").HeaderText = "関税率" & vbCrLf & "d"
            DgvItemList.Columns("関税額").HeaderText = "単価当り関税額" & vbCrLf & "e=b*d"
            DgvItemList.Columns("前払法人税率").HeaderText = "前払法人税率" & vbCrLf & "f"
            DgvItemList.Columns("前払法人税額").HeaderText = "単価当り前払法人税額" & vbCrLf & "g=(b+e)*f"
            DgvItemList.Columns("輸送費率").HeaderText = "輸送費率" & vbCrLf & "h"
            DgvItemList.Columns("輸送費額").HeaderText = "単価当り輸送費額" & vbCrLf & "i=b*h"
            DgvItemList.Columns("仕入金額").HeaderText = "仕入金額" & vbCrLf & "j=a*(b+e+g+i)"

            DgvItemList.Columns("仕入金額_外貨").HeaderText = "仕入金額" & vbCrLf & "（原通貨）"

        End If

        '検索（Date）の初期値
        DtpRegistrationDate.Value = DateTime.Today
        DtpPurchaseDate.Value = DateTime.Today
        DtpShippedDate.Value = DateTime.Today

        'セルの内容に合わせて、行の高さが自動的に調節されるようにする
        DgvItemList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        '"Column1"列のセルのテキストを折り返して表示する
        DgvItemList.Columns("型式").DefaultCellStyle.WrapMode = DataGridViewTriState.True
        DgvItemList.Columns("備考").DefaultCellStyle.WrapMode = DataGridViewTriState.True

        Dim reccnt As Integer = 0

        '新規登録モード 伝票番号取得
        If PurchaseStatus = CommonConst.STATUS_ADD Then

            GetSiireNo_New()
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "AddNewMode"
            Else
                LblMode.Text = "新規登録モード"
            End If

            TxtSupplierCode.Enabled = True
            TxtSupplierName.Enabled = True
            TxtPostalCode.Enabled = True
            TxtAddress1.Enabled = True
            TxtTel.Enabled = True
            TxtFax.Enabled = True
            TxtPosition.Enabled = True
            TxtPerson.Enabled = True
            TxtSales.Enabled = True
            TxtPaymentTerms.Enabled = True

            '通貨・レート情報設定
            createCurrencyCombobox()
            setRate()

            TxtIDRCurrency.Text = setBaseCurrency() '通貨表示：ベースの設定
            setChangeCurrency() '通貨表示：変更後の設定

            '明細を１行デフォルト表示
            DgvItemList.Rows.Add()
            DgvItemList.Rows(DgvItemList.Rows.Count() - 1).Cells("リードタイム単位").Value = 1
            setSireTax(DgvItemList.Rows.Count() - 1)  '間接費率をセット
            DgvItemList.Rows(DgvItemList.Rows.Count() - 1).Cells("No").Value = "1"
            'リストの行数をセット
            TxtItemCount.Text = DgvItemList.Rows.Count()

            setCellValueChanged()
            Exit Sub

        ElseIf PurchaseStatus Is CommonConst.STATUS_VIEW Then
            '参照モード

            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If

            BtnRegistration.Visible = False
            TxtCustomerPO.Enabled = False
            DtpPurchaseDate.Enabled = False
            TxtSales.Enabled = False
            TxtPerson.Enabled = False
            TxtPosition.Enabled = False
            TxtPaymentTerms.Enabled = False
            TxtPurchaseRemark.Enabled = False
            CbShippedBy.Enabled = False
            DtpShippedDate.Enabled = False
            CmWarehouse.Enabled = False
            CmCurrency.Enabled = False

            BtnInsert.Visible = False
            BtnUp.Visible = False
            BtnDown.Visible = False
            BtnRowsAdd.Visible = False
            BtnRowsDel.Visible = False
            BtnClone.Visible = False
            DgvItemList.ReadOnly = True

        End If

        '発注基本情報
        Dim Sql As String = ""

        'ここで最大値の高いものを取得するSQLを実行する
        Sql = " AND 発注番号 = '" & PurchaseNo.ToString & "'"
        Sql += " AND 発注番号枝番 = '" & PurchaseSuffix.ToString & "'"

        Dim dsHattyu As DataSet = getDsData("t20_hattyu", Sql)
        CompanyCode = dsHattyu.Tables(RS).Rows(0)("会社コード")

        Sql = "SELECT t21.*"
        Sql += " FROM  public.t21_hattyu t21 "
        Sql += " INNER JOIN  t20_hattyu t20"
        Sql += " ON t21.会社コード = t20.会社コード"
        Sql += " And  t21.発注番号 = t20.発注番号"
        Sql += " And  t21.発注番号枝番 = t20.発注番号枝番"

        Sql += " WHERE t21.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND t21.発注番号 ILIKE '" & PurchaseNo.ToString & "'"
        Sql += " AND t21.発注番号枝番 ILIKE '" & PurchaseSuffix.ToString & "'"
        Sql += " ORDER BY t21.行番号 "

        Dim dsHattyudt As DataSet = _db.selectDB(Sql, RS, reccnt)

        '発注データから各項目を取得、表示
        If dsHattyu.Tables(RS).Rows(0)("発注番号") IsNot DBNull.Value Then
            TxtOrderingNo.Text = dsHattyu.Tables(RS).Rows(0)("発注番号")
        End If
        If dsHattyu.Tables(RS).Rows(0)("発注番号枝番") IsNot DBNull.Value Then
            TxtOrderingSuffix.Text = dsHattyu.Tables(RS).Rows(0)("発注番号枝番")
        End If
        If dsHattyu.Tables(RS).Rows(0)("見積番号") IsNot DBNull.Value Then
            TxtQuoteNo.Text = dsHattyu.Tables(RS).Rows(0)("見積番号")
        End If

        If dsHattyu.Tables(RS).Rows(0)("見積番号").ToString = "" Then
            LblQuoteNo.Visible = False
            TxtQuoteNo.Visible = False
            LblHyphen.Visible = False
            TxtQuoteSuffix.Visible = False
        End If

        If dsHattyu.Tables(RS).Rows(0)("見積番号枝番") IsNot DBNull.Value Then
            TxtQuoteSuffix.Text = dsHattyu.Tables(RS).Rows(0)("見積番号枝番")
        End If
        If dsHattyu.Tables(RS).Rows(0)("発注日") IsNot DBNull.Value Then
            If PurchaseStatus Is CommonConst.STATUS_VIEW Then
                DtpPurchaseDate.Value = dsHattyu.Tables(RS).Rows(0)("発注日")
            Else
                DtpPurchaseDate.Value = dtNow
            End If
        End If
        If dsHattyu.Tables(RS).Rows(0)("登録日") IsNot DBNull.Value Then
            If PurchaseStatus Is CommonConst.STATUS_VIEW Then
                DtpRegistrationDate.Value = dsHattyu.Tables(RS).Rows(0)("登録日")
            Else
                DtpPurchaseDate.Value = dtNow
            End If
        End If
        If dsHattyu.Tables(RS).Rows(0)("仕入先コード") IsNot DBNull.Value Then
            TxtSupplierCode.Text = dsHattyu.Tables(RS).Rows(0)("仕入先コード")
        End If
        If dsHattyu.Tables(RS).Rows(0)("仕入先名") IsNot DBNull.Value Then
            delSupplierNameChanged() '仕入先名変更イベントを無効化
            TxtSupplierName.Text = dsHattyu.Tables(RS).Rows(0)("仕入先名")
            setSupplierNameChanged() '仕入先名変更イベントを有効化
        End If
        If dsHattyu.Tables(RS).Rows(0)("仕入先担当者名") IsNot DBNull.Value Then
            TxtPerson.Text = dsHattyu.Tables(RS).Rows(0)("仕入先担当者名")
        End If
        If dsHattyu.Tables(RS).Rows(0)("仕入先担当者役職") IsNot DBNull.Value Then
            TxtPosition.Text = dsHattyu.Tables(RS).Rows(0)("仕入先担当者役職")
        End If
        If dsHattyu.Tables(RS).Rows(0)("仕入先郵便番号") IsNot DBNull.Value Then
            TxtPostalCode.Text = dsHattyu.Tables(RS).Rows(0)("仕入先郵便番号")
        End If
        If dsHattyu.Tables(RS).Rows(0)("仕入先住所") IsNot DBNull.Value Then
            TxtAddress1.Text = dsHattyu.Tables(RS).Rows(0)("仕入先住所")
        End If
        If dsHattyu.Tables(RS).Rows(0)("仕入先電話番号") IsNot DBNull.Value Then
            TxtTel.Text = dsHattyu.Tables(RS).Rows(0)("仕入先電話番号")
        End If
        If dsHattyu.Tables(RS).Rows(0)("仕入先ＦＡＸ") IsNot DBNull.Value Then
            TxtFax.Text = dsHattyu.Tables(RS).Rows(0)("仕入先ＦＡＸ")
        End If
        If dsHattyu.Tables(RS).Rows(0)("営業担当者コード") IsNot DBNull.Value Then
            TxtSales.Tag = dsHattyu.Tables(RS).Rows(0)("営業担当者コード")
        End If
        If dsHattyu.Tables(RS).Rows(0)("営業担当者") IsNot DBNull.Value Then
            TxtSales.Text = dsHattyu.Tables(RS).Rows(0)("営業担当者")
        End If
        If dsHattyu.Tables(RS).Rows(0)("入力担当者") IsNot DBNull.Value Then
            TxtInput.Text = dsHattyu.Tables(RS).Rows(0)("入力担当者")
        End If
        If dsHattyu.Tables(RS).Rows(0)("支払条件") IsNot DBNull.Value Then
            TxtPaymentTerms.Text = dsHattyu.Tables(RS).Rows(0)("支払条件")
        End If
        If dsHattyu.Tables(RS).Rows(0)("備考") IsNot DBNull.Value Then
            TxtPurchaseRemark.Text = dsHattyu.Tables(RS).Rows(0)("備考")
        End If
        If dsHattyu.Tables(RS).Rows(0)("仕入金額") IsNot DBNull.Value Then
            Dim tmp_cur As Decimal = dsHattyu.Tables(RS).Rows(0)("仕入金額")
            TxtPurchaseAmount.Text = Decimal.Parse(tmp_cur).ToString("N0")
        End If
        If dsHattyu.Tables(RS).Rows(0)("見積備考") IsNot DBNull.Value Then
            TxtQuoteRemarks.Text = dsHattyu.Tables(RS).Rows(0)("見積備考")
        End If
        If dsHattyu.Tables(RS).Rows(0)("客先番号") IsNot DBNull.Value Then
            TxtCustomerPO.Text = dsHattyu.Tables(RS).Rows(0)("客先番号")
        End If

        If dsHattyu.Tables(RS).Rows(0)("出荷方法") IsNot DBNull.Value Then
            CbShippedBy.SelectedIndex = dsHattyu.Tables(RS).Rows(0)("出荷方法")
        End If
        If dsHattyu.Tables(RS).Rows(0)("出荷日") IsNot DBNull.Value Then
            '出荷日の最小値を調べて出荷日が入るようにする
            If DtpShippedDate.MinDate > dsHattyu.Tables(RS).Rows(0)("出荷日") Then
                DtpShippedDate.MinDate = dsHattyu.Tables(RS).Rows(0)("出荷日")
            End If
            DtpShippedDate.Value = dsHattyu.Tables(RS).Rows(0)("出荷日")
        End If
        If dsHattyu.Tables(RS).Rows(0)("倉庫コード") IsNot DBNull.Value Then
            CmWarehouse.SelectedValue = dsHattyu.Tables(RS).Rows(0)("倉庫コード")
        End If
        If dsHattyu.Tables(RS).Rows(0)("通貨") IsNot DBNull.Value Then

            createCurrencyCombobox(dsHattyu.Tables(RS).Rows(0)("通貨").ToString)

        End If

        '通貨・レート情報設定
        If PurchaseStatus <> CommonConst.STATUS_ADD Then
            '通貨・レート情報設定
            createCurrencyCombobox(dsHattyudt.Tables(RS).Rows(0)("仕入通貨").ToString)
        End If

        setRate()
        '通貨表示：ベースの設定
        TxtIDRCurrency.Text = setBaseCurrency()
        setChangeCurrency()

        If dsHattyu.Tables(RS).Rows(0)("仕入金額") IsNot DBNull.Value Then
            Dim tmp_cur As Decimal = dsHattyu.Tables(RS).Rows(0)("仕入金額")
            TxtPurchaseAmount.Text = Decimal.Parse(tmp_cur).ToString("N0")
        End If

        If dsHattyu.Tables(RS).Rows(0)("仕入金額_外貨") IsNot DBNull.Value Then
            Dim tmp_cur As Decimal = dsHattyu.Tables(RS).Rows(0)("仕入金額_外貨")
            TxtPurchaseAmount2.Text = Decimal.Parse(tmp_cur).ToString("N0")
        End If

        For i As Integer = 0 To dsHattyudt.Tables(RS).Rows.Count - 1
            DgvItemList.Rows.Add()
            DgvItemList.Rows(i).Cells("仕入区分").Value = Integer.Parse(dsHattyudt.Tables(RS).Rows(i)("仕入区分"))
            DgvItemList.Rows(i).Cells("メーカー").Value = dsHattyudt.Tables(RS).Rows(i)("メーカー")
            DgvItemList.Rows(i).Cells("品名").Value = dsHattyudt.Tables(RS).Rows(i)("品名")
            DgvItemList.Rows(i).Cells("型式").Value = dsHattyudt.Tables(RS).Rows(i)("型式")
            DgvItemList.Rows(i).Cells("数量").Value = dsHattyudt.Tables(RS).Rows(i)("発注数量")
            DgvItemList.Rows(i).Cells("単位").Value = dsHattyudt.Tables(RS).Rows(i)("単位")
            DgvItemList.Rows(i).Cells("仕入単価").Value = Decimal.Parse(dsHattyudt.Tables(RS).Rows(i)("仕入値"))
            DgvItemList.Rows(i).Cells("仕入単価_外貨").Value = Decimal.Parse(dsHattyudt.Tables(RS).Rows(i)("仕入値_外貨"))
            DgvItemList.Rows(i).Cells("仕入原価").Value = Decimal.Parse(dsHattyudt.Tables(RS).Rows(i)("仕入値") * dsHattyudt.Tables(RS).Rows(i)("発注数量"))

            DgvItemList.Rows(i).Cells("関税率").Value = dsHattyudt.Tables(RS).Rows(i)("関税率")
            DgvItemList.Rows(i).Cells("関税額").Value = dsHattyudt.Tables(RS).Rows(i)("関税額")
            DgvItemList.Rows(i).Cells("前払法人税率").Value = dsHattyudt.Tables(RS).Rows(i)("前払法人税率")
            DgvItemList.Rows(i).Cells("前払法人税額").Value = dsHattyudt.Tables(RS).Rows(i)("前払法人税額")
            DgvItemList.Rows(i).Cells("仕入金額").Value = Decimal.Parse(dsHattyudt.Tables(RS).Rows(i)("仕入金額"))
            DgvItemList.Rows(i).Cells("仕入金額_外貨").Value = Decimal.Parse(dsHattyudt.Tables(RS).Rows(i)("仕入金額_外貨"))
            DgvItemList.Rows(i).Cells("輸送費率").Value = dsHattyudt.Tables(RS).Rows(i)("輸送費率")
            DgvItemList.Rows(i).Cells("輸送費額").Value = dsHattyudt.Tables(RS).Rows(i)("輸送費額")

            DgvItemList.Rows(i).Cells("間接費").Value = dsHattyudt.Tables(RS).Rows(i)("間接費")

            If dsHattyudt.Tables(RS).Rows(i)("リードタイム単位") IsNot DBNull.Value Then
                DgvItemList.Rows(i).Cells("リードタイム単位").Value = Integer.Parse(dsHattyudt.Tables(RS).Rows(i)("リードタイム単位"))
            End If

            If dsHattyudt.Tables(RS).Rows(i)("貿易条件") IsNot DBNull.Value Then
                DgvItemList.Rows(i).Cells("貿易条件").Value = Integer.Parse(dsHattyudt.Tables(RS).Rows(i)("貿易条件"))
            End If

            DgvItemList.Rows(i).Cells("備考").Value = dsHattyudt.Tables(RS).Rows(i)("備考")
            DgvItemList.Rows(i).Cells("入庫数").Value = dsHattyudt.Tables(RS).Rows(i)("入庫数")
            DgvItemList.Rows(i).Cells("未入庫数").Value = dsHattyudt.Tables(RS).Rows(i)("未入庫数")

        Next

        '行番号の振り直し
        Dim rowNo As Integer = DgvItemList.Rows.Count()
        Dim No As Integer = 1
        For c As Integer = 0 To rowNo - 1
            DgvItemList.Rows(c).Cells(0).Value = No
            No += 1
        Next c
        TxtItemCount.Text = DgvItemList.Rows.Count()

        '参照モード
        If PurchaseStatus = CommonConst.STATUS_VIEW Then

            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If

            DtpPurchaseDate.Enabled = False
            TxtPurchaseRemark.Enabled = False
            TxtCustomerPO.Enabled = False
            DgvItemList.ReadOnly = True
            BtnRegistration.Visible = False
            BtnPurchase.Visible = True
            BtnPurchase.Location = New Point(1004, 509)

            '複写モード
        ElseIf PurchaseStatus = CommonConst.STATUS_CLONE Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "NewCopyMode"
            Else
                LblMode.Text = "新規複写モード"
            End If

            TxtSupplierCode.Enabled = True
            TxtSupplierName.Enabled = True
            TxtPostalCode.Enabled = True
            TxtAddress1.Enabled = True
            TxtTel.Enabled = True
            TxtFax.Enabled = True
            TxtPosition.Enabled = True
            TxtPerson.Enabled = True
            TxtSales.Enabled = True
            TxtPaymentTerms.Enabled = True

            '発注番号を新規発行
            Dim PO As String = getSaiban("30", dtNow)
            TxtOrderingNo.Text = PO

            '枝番は1
            TxtOrderingSuffix.Text = 1

        ElseIf PurchaseStatus = CommonConst.STATUS_EDIT Then

            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "EditMode"
            Else
                LblMode.Text = "編集モード"
            End If


            '枝番の最大値を取得し、 +1 加算する

            Sql = " SELECT public.t20_hattyu.* "
            Sql += " FROM "
            Sql += "t20_hattyu"

            Sql += " INNER JOIN( "
            Sql += " SELECT "
            Sql += "発注番号"
            Sql += ", MAX(""発注番号枝番"") As max_val "
            Sql += " FROM t20_hattyu "
            Sql += " GROUP BY "
            Sql += " 発注番号 "
            Sql += " ) tmp "
            Sql += " ON "
            Sql += " t20_hattyu.""発注番号"" = tmp.""発注番号"""
            Sql += " AND "
            Sql += " t20_hattyu.""発注番号枝番"" = tmp.max_val"

            Sql += " where "
            Sql += " t20_hattyu.""会社コード"" = '" & frmC01F10_Login.loginValue.BumonCD & "'"

            Sql += " AND "
            Sql += " t20_hattyu.""発注番号"" "
            Sql += " ILIKE "
            Sql += "'"
            Sql += dsHattyu.Tables(RS).Rows(0)("発注番号")
            Sql += "'"
            Sql += " AND "
            Sql += " t20_hattyu.""取消区分"" = " & CommonConst.CANCEL_KBN_ENABLED


            Dim ds2 As DataSet = _db.selectDB(Sql, RS, reccnt)

            '枝番は+1
            TxtOrderingSuffix.Text = ds2.Tables(RS).Rows(0)("発注番号枝番") + 1

        End If

        setCellValueChanged()

    End Sub
    '前の画面に戻る
    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    '基準通貨の通貨コードを取得する
    Private Function setBaseCurrency() As String
        Dim Sql As String
        '通貨表示：ベースの設定
        Sql = " AND 採番キー = " & CommonConst.CURRENCY_CD_IDR.ToString
        Sql += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString

        Dim ds As DataSet = getDsData("m25_currency", Sql)
        'TxtIDRCurrency.Text = ds.Tables(RS).Rows(0)("通貨コード")
        setBaseCurrency = ds.Tables(RS).Rows(0)("通貨コード")

    End Function


    'セルの値が変更されたら
    Private Sub CellValueChanged(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DgvItemList.CellValueChanged

        Dim tmp As Decimal = 0
        Dim tmp1 As Decimal = 0
        Dim tmp2 As Decimal = 0
        Dim tmp3 As Decimal = 0
        Dim tmp4 As Decimal = 0

        '操作したカラム名を取得
        Dim currentColumn As String = DgvItemList.Columns(e.ColumnIndex).Name

        '1回だとイベントハンドラーを削除しきれなかったので、2回実行している
        delCellValueChanged()
        delCellValueChanged()


        If DgvItemList.Rows.Count = 0 Then  '明細がない場合は終了
            Exit Sub
        End If

        'ヘッダー以外だったら
        If e.RowIndex > -1 Then

            '発注金額をクリア
            TxtPurchaseAmount.Text = 0
            TxtPurchaseAmount2.Text = 0


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


            If currentColumn = "数量" Then  '数量の場合、カンマ処理
                Dim tmpCur As Decimal = DgvItemList.Rows(e.RowIndex).Cells("数量").Value
                DgvItemList("数量", e.RowIndex).Value = tmpCur
            End If

            If currentColumn = "仕入単価_外貨" Then  '仕入単価_外貨の場合、カンマ処理
                Dim tmpCur As Decimal = DgvItemList.Rows(e.RowIndex).Cells("仕入単価_外貨").Value
                DgvItemList("仕入単価_外貨", e.RowIndex).Value = tmpCur
            End If


            '仕入単価_外貨 / 仕入金額_外貨 が変更されたらそれぞれを更新する
            If currentColumn = "仕入単価" Or currentColumn = "仕入単価_外貨" Then
                If DgvItemList.Rows.Count > 0 Then
                    Select Case currentColumn
                        Case "仕入単価"
                            If TxtRate.Text <> "" Then
                                Dim tmpCur As Decimal = DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value
                                DgvItemList("仕入単価", e.RowIndex).Value = tmpCur
                                DgvItemList.Rows(e.RowIndex).Cells("仕入単価_外貨").Value = Math.Ceiling(DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value / TxtRate.Text)
                            End If
                        Case "仕入単価_外貨"
                            If DgvItemList("仕入単価_外貨", e.RowIndex).Value IsNot Nothing Then
                                '小数点表示にするため切り上げをコメントアウト
                                'DgvItemList("仕入単価", e.RowIndex).Value = Math.Ceiling(DgvItemList("仕入単価_外貨", e.RowIndex).Value / TxtRate.Text)
                                DgvItemList("仕入単価", e.RowIndex).Value = DgvItemList("仕入単価_外貨", e.RowIndex).Value / TxtRate.Text
                            End If
                    End Select

                End If
            End If


            '仕入単価 <> Nothing
            '--------------------------
            If DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value IsNot Nothing Then

                '数量 <> Nothing
                If DgvItemList.Rows(e.RowIndex).Cells("数量").Value IsNot Nothing Then
                    DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value = Math.Ceiling(DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value * DgvItemList.Rows(e.RowIndex).Cells("数量").Value)
                End If
                '関税率 <> Nothing
                If DgvItemList.Rows(e.RowIndex).Cells("関税率").Value IsNot Nothing Then
                    DgvItemList.Rows(e.RowIndex).Cells("関税額").Value = Math.Ceiling(DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value * DgvItemList.Rows(e.RowIndex).Cells("関税率").Value)
                End If
                '前払法人税率, 関税額 <> Nothing
                If DgvItemList.Rows(e.RowIndex).Cells("前払法人税率").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("関税額").Value IsNot Nothing Then
                    tmp = DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value + DgvItemList.Rows(e.RowIndex).Cells("関税額").Value
                    tmp1 = Math.Ceiling(tmp * DgvItemList.Rows(e.RowIndex).Cells("前払法人税率").Value)
                    tmp1 = Math.Ceiling(tmp1)
                    DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value = tmp1
                End If
                '輸送費率 <> Nothing
                If DgvItemList.Rows(e.RowIndex).Cells("輸送費率").Value IsNot Nothing Then
                    DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value = Math.Ceiling(DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value * DgvItemList.Rows(e.RowIndex).Cells("輸送費率").Value)
                End If

                '仕入原価 <> Nothing
                If DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value IsNot Nothing Then
                    DgvItemList.Rows(e.RowIndex).Cells("仕入金額").Value = DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value _
                                                                         + (DgvItemList.Rows(e.RowIndex).Cells("関税額").Value + DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value + DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value) _
                                                                         * DgvItemList.Rows(e.RowIndex).Cells("数量").Value

                    If (DgvItemList.Rows(e.RowIndex).Cells("関税額").Value Is Nothing Or DgvItemList.Rows(e.RowIndex).Cells("関税額").Value = 0) _
                       And (DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value Is Nothing Or DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value = 0) _
                       And (DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value Is Nothing Or DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value = 0) Then
                        DgvItemList.Rows(e.RowIndex).Cells("仕入金額_外貨").Value = Math.Ceiling(DgvItemList.Rows(e.RowIndex).Cells("仕入単価_外貨").Value * DgvItemList.Rows(e.RowIndex).Cells("数量").Value)
                    Else
                        DgvItemList.Rows(e.RowIndex).Cells("仕入金額_外貨").Value = Math.Ceiling(DgvItemList.Rows(e.RowIndex).Cells("仕入金額").Value * TxtRate.Text)
                    End If

                End If

                If currentColumn = "仕入単価" Then
                    '仕入単価_外貨
                    DgvItemList("仕入単価_外貨", e.RowIndex).Value = Math.Ceiling(DgvItemList("仕入単価", e.RowIndex).Value / TxtRate.Text)
                End If

            End If

            ''数量と仕入単価が入力されていたら
            'If DgvItemList.Rows(e.RowIndex).Cells("数量").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value IsNot Nothing Then
            '    '仕入金額 = 数量 * 仕入単価
            '    'DgvItemList.Rows(e.RowIndex).Cells("仕入金額").Value = DgvItemList.Rows(e.RowIndex).Cells("数量").Value * DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value
            '    DgvItemList.Rows(e.RowIndex).Cells("仕入金額_外貨").Value = DgvItemList.Rows(e.RowIndex).Cells("数量").Value * DgvItemList.Rows(e.RowIndex).Cells("仕入単価_外貨").Value
            'End If

            setCellValueChanged()
        End If


        'ヘッダー
        Call setCurrency()


    End Sub

    '行移動上
    Private Sub BtnUp_Click(sender As Object, e As EventArgs) Handles BtnUp.Click
        'グリッドに何もないときは処理しない
        If DgvItemList.CurrentCell Is Nothing Then
            Exit Sub
        End If

        If DgvItemList.CurrentCell.RowIndex > 0 Then
            DgvItemList.CurrentCell = DgvItemList(DgvItemList.CurrentCell.ColumnIndex, DgvItemList.CurrentCell.RowIndex - 1)
        End If
    End Sub

    '行移動下
    Private Sub BtnDown_Click(sender As Object, e As EventArgs) Handles BtnDown.Click
        'グリッドに何もないときは処理しない
        If DgvItemList.CurrentCell Is Nothing Then
            Exit Sub
        End If

        If DgvItemList.CurrentCell.RowIndex + 1 < DgvItemList.Rows.Count Then
            DgvItemList.CurrentCell = DgvItemList(DgvItemList.CurrentCell.ColumnIndex, DgvItemList.CurrentCell.RowIndex + 1)
        End If
    End Sub

    '任意の場所に行を挿入
    Private Sub BtnInsert_Click(sender As Object, e As EventArgs) Handles BtnInsert.Click

        If DgvItemList.Rows.Count > 0 Then
            Dim RowIdx As Integer = DgvItemList.CurrentCell.RowIndex
            '行を挿入
            DgvItemList.Rows.Insert(RowIdx + 1)
            DgvItemList.Rows(RowIdx + 1).Cells("リードタイム単位").Value = 1
            setSireTax(RowIdx + 1)  '間接費率をセット
        Else
            DgvItemList.Rows.Add()
            DgvItemList.Rows(0).Cells("リードタイム単位").Value = 1
            TxtItemCount.Text = DgvItemList.Rows.Count()
            setSireTax(0)  '間接費率をセット
        End If

        '最終行のインデックスを取得
        Dim index As Integer = DgvItemList.Rows.Count()
        '行番号の振り直し
        Dim No As Integer = 1
        For c As Integer = 0 To index - 1
            DgvItemList.Rows(c).Cells("No").Value = No
            No += 1
        Next c

        'リストの行数をセット
        TxtItemCount.Text = DgvItemList.Rows.Count()
    End Sub

    '行追加（DGVの最終行に追加）
    Private Sub BtnRowsAdd_Click(sender As Object, e As EventArgs) Handles BtnRowsAdd.Click
        DgvItemList.Rows.Add()
        DgvItemList.Rows(DgvItemList.Rows.Count() - 1).Cells("リードタイム単位").Value = 1
        setSireTax(DgvItemList.Rows.Count() - 1)  '間接費率をセット

        '行番号の振り直し
        Dim index As Integer = DgvItemList.Rows.Count()
        Dim No As Integer = 1
        For c As Integer = 0 To index - 1
            DgvItemList.Rows(c).Cells("No").Value = No
            No += 1
        Next c

        'リストの行数をセット
        TxtItemCount.Text = DgvItemList.Rows.Count()
    End Sub

    '選択行の削除（削除時に金額の再計算、Noの再採番）
    Private Sub BtnRowsDel_Click(sender As Object, e As EventArgs) Handles BtnRowsDel.Click

        For Each r As DataGridViewCell In DgvItemList.SelectedCells
            DgvItemList.Rows.RemoveAt(r.RowIndex)
        Next r

        txtPurchasecost.Clear()
        TxtPurchaseAmount.Clear()
        TxtPurchaseAmount2.Clear()

        Call resetListCurrency()

        '行番号の振り直し
        Dim index As Integer = DgvItemList.Rows.Count()
        Dim No As Integer = 1
        For c As Integer = 0 To index - 1
            DgvItemList.Rows(c).Cells(0).Value = No
            No += 1
        Next c
        TxtItemCount.Text = DgvItemList.Rows.Count()
    End Sub

    '行の複写（選択行の直下に複写）
    Private Sub BtnClone_Click(sender As Object, e As EventArgs) Handles BtnClone.Click
        Try
            'メニュー選択処理
            Dim RowIdx As Integer
            RowIdx = DgvItemList.CurrentCell.RowIndex

            Dim rowClone As DataGridViewRow = DgvItemList.Rows(RowIdx).Clone

            'columnの数分valueを複写
            For i As Integer = 0 To DgvItemList.ColumnCount - 1
                rowClone.Cells(i).Value = DgvItemList.Rows(RowIdx).Cells(i).Value
            Next

            '1行下に新規行作成及びclone内容を反映
            DgvItemList.Rows.Insert(RowIdx + 1, rowClone)

            '行番号の振り直し
            noReset()

            TxtItemCount.Text = DgvItemList.Rows.Count()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Sub noReset()
        Dim index As Integer = DgvItemList.Rows.Count()
        Dim No As Integer = 1
        For c As Integer = 0 To index - 1
            DgvItemList.Rows(c).Cells("No").Value = No
            No += 1
        Next c

    End Sub

    Private Sub TxtCustomerCode_DoubleClick(sender As Object, e As EventArgs) Handles TxtSupplierCode.DoubleClick
        Dim openForm As Form = Nothing
        Dim idx As Integer = 0
        Dim Status As String = CommonConst.STATUS_CLONE
        openForm = New SupplierSearch(_msgHd, _db, _langHd, idx, Me, Status)   '処理選択
        openForm.Show(Me)
        Me.Enabled = False
    End Sub

    Private Sub TxtSales_DoubleClick(sender As Object, e As EventArgs) Handles TxtSales.DoubleClick
        Dim openForm As Form = Nothing
        Dim Status As String = CommonConst.STATUS_CLONE
        openForm = New SalesSearch(_msgHd, _db, _langHd, Me, Status)   '処理選択
        openForm.Show(Me)
        Me.Enabled = False
    End Sub

    Private Sub DgvItemList_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvItemList.CellDoubleClick

        '行ヘッダークリック時は無効
        If e.RowIndex < 0 Then
            Exit Sub
        End If

        If PurchaseStatus Is CommonConst.STATUS_VIEW Then
            Exit Sub
        End If


        Dim Status As String = CommonConst.STATUS_CLONE

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
            openForm = New MakerSearch(_msgHd, _db, Me, e.RowIndex, e.ColumnIndex, Maker, Item, Model, selectColumn, Status)   '処理選択
            openForm.Show(Me)
            Me.Enabled = False
        End If

    End Sub

    '登録ボタン押下時
    Private Sub BtnRegistration_Click(sender As Object, e As EventArgs) Handles BtnRegistration.Click
        '項目チェック
        Dim strMessage As String = ""    'メッセージ本文
        Dim strMessageTitle As String = ""      'メッセージタイトル
        '仕入先は必須入力としましょう
        If TxtSupplierCode.Text = "" Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                strMessage = "Please enter Supplier Code. "
                strMessageTitle = "SupplierCode Error"
            Else
                strMessage = "仕入先を入力してください。"
                strMessageTitle = "仕入先入力エラー"
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
                (DgvItemList.Rows(i).Cells("メーカー").Value Is Nothing Or DgvItemList.Rows(i).Cells("メーカー").Value = vbNullString) Or
                (DgvItemList.Rows(i).Cells("品名").Value Is Nothing Or DgvItemList.Rows(i).Cells("品名").Value = vbNullString) Or
                (DgvItemList.Rows(i).Cells("型式").Value Is Nothing Or DgvItemList.Rows(i).Cells("型式").Value = vbNullString) Then

                '対象データがないメッセージを表示
                _msgHd.dspMSG("chkQuoteInputError", frmC01F10_Login.loginValue.Language)

                Exit Sub
            End If
        Next

        '数量と仕入単価がなかったらエラーで戻す
        For i As Integer = 0 To DgvItemList.RowCount - 1
            If DgvItemList.Rows(i).Cells("仕入単価").Value Is Nothing Or DgvItemList.Rows(i).Cells("数量").Value Is Nothing Then

                '対象データがないメッセージを表示
                _msgHd.dspMSG("chkOrderingInputError", frmC01F10_Login.loginValue.Language)

                Exit Sub
            Else
                If DgvItemList.Rows(i).Cells("数量").Value = 0 Then
                    '対象データがないメッセージを表示
                    _msgHd.dspMSG("chkQuantityError", frmC01F10_Login.loginValue.Language)

                    Exit Sub
                End If

            End If
        Next

        Dim reccnt As Integer = 0
        Dim dtNow As String = formatDatetime(DateTime.Now)
        Dim Sql As String = ""

        Try
            '複写か編集の時
            If PurchaseStatus = CommonConst.STATUS_CLONE Or PurchaseStatus = CommonConst.STATUS_EDIT Or PurchaseStatus = CommonConst.STATUS_ADD Then
                Dim dsCompany As DataSet
                Dim dsHattyuHd As DataSet
                Dim dsHattyuDt As DataSet

                'If PurchaseStatus <> CommonConst.STATUS_ADD And PurchaseStatus <> CommonConst.STATUS_CLONE Then

                Sql = " AND "
                Sql += " 見積番号 = '" & TxtQuoteNo.Text & "'"
                Sql += " AND "
                Sql += " 見積番号枝番 = '" & TxtQuoteSuffix.Text & "'"
                dsCompany = getDsData("t01_mithd", Sql)

                Sql = " AND 発注番号 = '" & PurchaseNo & "'"
                Sql += " AND 発注番号枝番 = '" & PurchaseSuffix & "'"
                dsHattyuHd = getDsData("t20_hattyu", Sql)

                Sql = " AND 発注番号 = '" & PurchaseNo & "'"
                Sql += " AND 発注番号枝番 = '" & PurchaseSuffix & "'"
                dsHattyuDt = getDsData("t21_hattyu", Sql)
                'Else
                'End If

                'レートの取得
                Dim strRate As Decimal = setRate(CmCurrency.SelectedValue.ToString)


                Sql = "INSERT INTO "
                Sql += "Public."
                Sql += "t20_hattyu("
                Sql += "会社コード, 発注番号, 発注番号枝番, 客先番号, 受注番号, 受注番号枝番, 見積番号, 見積番号枝番"
                Sql += ", 得意先コード, 得意先名, 得意先郵便番号, 得意先住所, 得意先電話番号, 得意先ＦＡＸ"
                Sql += ", 得意先担当者役職, 得意先担当者名, 仕入先コード, 仕入先名, 仕入先郵便番号, 仕入先住所"
                Sql += ", 仕入先電話番号, 仕入先ＦＡＸ, 仕入先担当者役職, 仕入先担当者名, 見積日, 見積有効期限"
                Sql += ", 支払条件, 見積金額,仕入金額, 粗利額, 営業担当者,入力担当者, 備考, 見積備考, ＶＡＴ, ＰＰＨ"
                Sql += ", 受注日, 発注日, 登録日, 更新日, 更新者, 取消区分, 出荷方法, 出荷日, 営業担当者コード"
                Sql += ", 入力担当者コード, 倉庫コード, 通貨, レート,仕入金額_外貨"

                If PurchaseStatus <> CommonConst.STATUS_ADD And dsHattyuHd.Tables(RS).Rows.Count > 0 Then
                    If dsHattyuHd.Tables(RS).Rows(0)("見積番号") <> "" Then
                        Sql += ", 見積金額_外貨"
                    End If
                End If


                Sql += ") VALUES ('"

                Sql += CompanyCode '会社コード
                Sql += "', '"
                Sql += TxtOrderingNo.Text '発注番号
                Sql += "', '"
                Sql += TxtOrderingSuffix.Text '発注番号枝番
                Sql += "', '"
                Sql += UtilClass.escapeSql(TxtCustomerPO.Text) '客先番号
                Sql += "', '"
                If dsHattyuHd.Tables(RS).Rows.Count > 0 Then
                    Sql += IIf(dsHattyuHd.Tables(RS).Rows(0)("受注番号") IsNot DBNull.Value, dsHattyuHd.Tables(RS).Rows(0)("受注番号"), "") '受注番号
                End If
                Sql += "', '"
                If dsHattyuHd.Tables(RS).Rows.Count > 0 Then
                    Sql += IIf(dsHattyuHd.Tables(RS).Rows(0)("受注番号枝番") IsNot DBNull.Value, dsHattyuHd.Tables(RS).Rows(0)("受注番号枝番"), "") '受注番号枝番
                End If
                Sql += "', '"
                Sql += IIf(TxtQuoteNo.Text <> "", TxtQuoteNo.Text, "") '見積番号
                Sql += "', '"
                Sql += IIf(TxtQuoteSuffix.Text <> "", TxtQuoteSuffix.Text, "") '見積番号枝番
                Sql += "', '"

                If dsCompany.Tables(RS).Rows.Count > 0 Then
                    Sql += IIf(dsCompany.Tables(RS).Rows(0)("得意先コード") IsNot DBNull.Value, dsCompany.Tables(RS).Rows(0)("得意先コード"), 0)
                End If

                Sql += "', '"

                If dsCompany.Tables(RS).Rows.Count > 0 Then
                    Sql += IIf(dsCompany.Tables(RS).Rows(0)("得意先名") IsNot DBNull.Value, dsCompany.Tables(RS).Rows(0)("得意先名"), 0)
                End If

                Sql += "', '"

                If dsCompany.Tables(RS).Rows.Count > 0 Then
                    Sql += IIf(dsCompany.Tables(RS).Rows(0)("得意先郵便番号") IsNot DBNull.Value, dsCompany.Tables(RS).Rows(0)("得意先郵便番号"), 0)
                End If

                Sql += "', '"

                If dsCompany.Tables(RS).Rows.Count > 0 Then
                    Sql += IIf(dsCompany.Tables(RS).Rows(0)("得意先住所") IsNot DBNull.Value, dsCompany.Tables(RS).Rows(0)("得意先住所"), 0)
                End If

                Sql += "', '"

                If dsCompany.Tables(RS).Rows.Count > 0 Then
                    Sql += IIf(dsCompany.Tables(RS).Rows(0)("得意先電話番号") IsNot DBNull.Value, dsCompany.Tables(RS).Rows(0)("得意先電話番号"), 0)
                End If

                Sql += "', '"

                If dsCompany.Tables(RS).Rows.Count > 0 Then
                    Sql += IIf(dsCompany.Tables(RS).Rows(0)("得意先ＦＡＸ") IsNot DBNull.Value, dsCompany.Tables(RS).Rows(0)("得意先ＦＡＸ"), 0)
                End If

                Sql += "', '"

                If dsCompany.Tables(RS).Rows.Count > 0 Then
                    Sql += IIf(dsCompany.Tables(RS).Rows(0)("得意先担当者役職") IsNot DBNull.Value, dsCompany.Tables(RS).Rows(0)("得意先担当者役職"), 0)
                End If

                Sql += "', '"

                If dsCompany.Tables(RS).Rows.Count > 0 Then
                    Sql += IIf(dsCompany.Tables(RS).Rows(0)("得意先担当者名") IsNot DBNull.Value, dsCompany.Tables(RS).Rows(0)("得意先担当者名"), 0)
                End If

                Sql += "', '"
                Sql += TxtSupplierCode.Text '仕入先コード
                Sql += "', '"
                Sql += TxtSupplierName.Text '仕入先名
                Sql += "', '"
                Sql += TxtPostalCode.Text '仕入先郵便番号
                Sql += "', '"
                Sql += TxtAddress1.Text '仕入先住所
                Sql += "', '"
                Sql += TxtTel.Text '仕入先電話番号
                Sql += "', '"
                Sql += TxtFax.Text '仕入先ＦＡＸ
                Sql += "', '"
                Sql += UtilClass.escapeSql(TxtPosition.Text) '仕入先担当者役職
                Sql += "', '"
                Sql += UtilClass.escapeSql(TxtPerson.Text) '仕入先担当者名

                Sql += "', "

                If dsCompany.Tables(RS).Rows.Count > 0 Then '見積日
                    Sql += IIf(dsCompany.Tables(RS).Rows(0)("見積日") IsNot DBNull.Value, "'" & UtilClass.strFormatDate(dsCompany.Tables(RS).Rows(0)("見積日")) & "'", 0)
                Else
                    Sql += "null"
                End If

                Sql += ", "

                If dsCompany.Tables(RS).Rows.Count > 0 Then '見積有効期限
                    Sql += IIf(dsCompany.Tables(RS).Rows(0)("見積有効期限") IsNot DBNull.Value, "'" & UtilClass.strFormatDate(dsCompany.Tables(RS).Rows(0)("見積有効期限")) & "'", 0)
                Else
                    Sql += "null"
                End If

                Sql += ", '"
                Sql += UtilClass.escapeSql(TxtPaymentTerms.Text) '支払条件
                Sql += "', '"
                Sql += "0" '見積金額
                Sql += "', '"
                Sql += formatNumber(TxtPurchaseAmount.Text) '仕入金額
                Sql += "', '"
                Sql += "0" '粗利額
                Sql += "', '"
                Sql += TxtSales.Text '営業担当者
                Sql += "', '"
                Sql += TxtInput.Text '入力担当者
                Sql += "', '"
                Sql += UtilClass.escapeSql(TxtPurchaseRemark.Text) '備考
                Sql += "', '"
                Sql += TxtQuoteRemarks.Text '見積備考
                Sql += "', "
                If dsCompany.Tables(RS).Rows.Count > 0 Then
                    Sql += IIf(dsCompany.Tables(RS).Rows(0)("ＶＡＴ") IsNot DBNull.Value, "'" & UtilClass.formatNumber(dsCompany.Tables(RS).Rows(0)("ＶＡＴ")) & "'", 0)
                Else
                    Sql += "0" 'ＶＡＴ
                End If
                Sql += ", '"
                Sql += "0" 'ＰＰＨ
                Sql += "', "

                If dsCompany.Tables(RS).Rows.Count > 0 Then
                    Sql += IIf(dsCompany.Tables(RS).Rows(0)("受注日") IsNot DBNull.Value, "'" & UtilClass.strFormatDate(dsCompany.Tables(RS).Rows(0)("受注日")) & "'", 0)
                Else
                    Sql += "null"
                End If

                Sql += ", '"
                Sql += strFormatDate(DtpPurchaseDate.Value) '発注日
                Sql += "', '"
                Sql += dtNow '登録日
                Sql += "', '"
                Sql += dtNow '更新日
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoNM '更新者
                Sql += "', "
                Sql += "0" '取消区分
                Sql += ", "
                Sql += CbShippedBy.SelectedIndex.ToString '出荷方法
                Sql += ", '"
                Sql += strFormatDate(DtpShippedDate.Value) '出荷日
                Sql += "', '"
                Sql += TxtSales.Tag '営業担当者コード
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoCD '入力担当者コード
                Sql += "', '"

                If CmWarehouse.SelectedIndex <> -1 Then
                    Sql += CmWarehouse.SelectedValue.ToString '倉庫コード
                Else
                    Sql += "" '倉庫コード
                End If

                Sql += "', '"
                Sql += CmCurrency.SelectedValue.ToString '通貨
                Sql += "', '"
                Sql += UtilClass.formatNumberF10(TxtRate.Text) 'レート
                Sql += "', '"
                Sql += formatNumber(TxtPurchaseAmount2.Text)  '仕入金額_外貨

                If PurchaseStatus <> CommonConst.STATUS_ADD And dsHattyuHd.Tables(RS).Rows.Count > 0 Then
                    If dsHattyuHd.Tables(RS).Rows(0)("見積番号") <> "" Then
                        Sql += "', '"
                        Sql += UtilClass.formatNumber(dsHattyuHd.Tables(RS).Rows(0)("見積金額_外貨")) '見積金額_外貨
                    End If
                End If

                Sql += "') "

                _db.executeDB(Sql)

                For i As Integer = 0 To DgvItemList.Rows.Count - 1

                    'レートの取得
                    strRate = setRate(CmCurrency.SelectedValue.ToString)


                    Sql = "INSERT INTO "
                    Sql += "Public."
                    Sql += "t21_hattyu("
                    Sql += "会社コード, 発注番号, 発注番号枝番, 行番号, 仕入区分, 仕入先名, メーカー, 品名, 型式, 単位, 仕入値"
                    Sql += ", 発注数量, 仕入数量, 発注残数, 間接費, 仕入金額, リードタイム, リードタイム単位, 入庫数"
                    Sql += ", 未入庫数, 備考, 更新者, 登録日, 更新日, 仕入単価_外貨, 仕入通貨, 仕入レート, 関税率, 関税額"
                    Sql += ", 前払法人税率, 前払法人税額, 輸送費率, 輸送費額, 仕入値_外貨, 仕入金額_外貨"
                    If PurchaseStatus <> CommonConst.STATUS_ADD And dsHattyuHd.Tables(RS).Rows.Count > 0 Then
                        If dsHattyuHd.Tables(RS).Rows(0)("見積番号") <> "" Then

                            Sql += ", 見積単価_外貨, 見積金額_外貨, 通貨, レート"
                        End If
                    End If

                    Sql += IIf(
                    DgvItemList.Rows(i).Cells("貿易条件").Value IsNot Nothing,
                    ", 貿易条件",
                    "")
                    Sql += " )VALUES('"
                    Sql += CompanyCode '会社コード
                    Sql += "', '"
                    Sql += TxtOrderingNo.Text '発注番号
                    Sql += "', '"
                    Sql += TxtOrderingSuffix.Text '発注番号枝番
                    Sql += "', '"
                    Sql += DgvItemList.Rows(i).Cells("No").Value.ToString '行番号
                    Sql += "', "

                    If DgvItemList.Rows(i).Cells("仕入区分").Value <> Nothing Then
                        Sql += DgvItemList.Rows(i).Cells("仕入区分").Value.ToString
                    Else
                        Sql += "2" '仕入区分
                    End If

                    Sql += ", '"
                    Sql += TxtSupplierName.Text '仕入先名
                    Sql += "', '"
                    Sql += DgvItemList.Rows(i).Cells("メーカー").Value.ToString 'メーカー
                    Sql += "', '"
                    Sql += DgvItemList.Rows(i).Cells("品名").Value.ToString '品名
                    Sql += "', '"
                    Sql += DgvItemList.Rows(i).Cells("型式").Value.ToString '型式
                    Sql += "', '"
                    Sql += UtilClass.escapeSql(DgvItemList.Rows(i).Cells("単位").Value) '単位
                    Sql += "', '"
                    Sql += formatNumber(DgvItemList.Rows(i).Cells("仕入単価").Value.ToString) '仕入値
                    Sql += "', '"
                    Sql += formatNumber(DgvItemList.Rows(i).Cells("数量").Value.ToString) '発注数量
                    Sql += "', '"
                    Sql += "0" '仕入数量
                    Sql += "', '"
                    Sql += formatNumber(DgvItemList.Rows(i).Cells("数量").Value.ToString) '発注残数
                    Sql += "', 0" '間接費
                    Sql += ", '"
                    Sql += formatNumber(DgvItemList.Rows(i).Cells("仕入金額").Value.ToString) '仕入金額
                    Sql += "', '"
                    Sql += IIf(
                                DgvItemList.Rows(i).Cells("リードタイム").Value IsNot Nothing,
                                DgvItemList.Rows(i).Cells("リードタイム").Value,
                                "") 'リードタイム
                    Sql += "', '"
                    Sql += DgvItemList.Rows(i).Cells("リードタイム単位").Value.ToString 'リードタイム単位
                    Sql += "', '"
                    Sql += "0" '入庫数
                    Sql += "', '"
                    Sql += DgvItemList.Rows(i).Cells("数量").Value.ToString '未入庫数
                    Sql += "', '"
                    Sql += UtilClass.escapeSql(DgvItemList.Rows(i).Cells("備考").Value) '備考
                    Sql += "', '"
                    Sql += frmC01F10_Login.loginValue.TantoNM '更新者
                    Sql += "', '"
                    Sql += dtNow '登録日
                    Sql += "', '"
                    Sql += dtNow '更新日
                    Sql += "', '"
                    Sql += formatNumber(DgvItemList.Rows(i).Cells("仕入単価_外貨").Value) '仕入単価_外貨
                    Sql += "', '"
                    Sql += CmCurrency.SelectedValue.ToString '仕入通貨
                    Sql += "', '"
                    Sql += UtilClass.formatNumberF10(strRate) '仕入レート  発注日で計算し直したデータを入れる
                    Sql += "', '"
                    Sql += UtilClass.formatNumber(DgvItemList.Rows(i).Cells("関税率").Value) '関税率
                    Sql += "', '"
                    Sql += UtilClass.formatNumber(DgvItemList.Rows(i).Cells("関税額").Value) '関税額
                    Sql += "', '"
                    Sql += UtilClass.formatNumber(DgvItemList.Rows(i).Cells("前払法人税率").Value) '前払法人税率
                    Sql += "', '"
                    Sql += UtilClass.formatNumber(DgvItemList.Rows(i).Cells("前払法人税額").Value) '前払法人税額
                    Sql += "', '"
                    Sql += UtilClass.formatNumber(DgvItemList.Rows(i).Cells("輸送費率").Value) '輸送費率
                    Sql += "', '"
                    Sql += UtilClass.formatNumber(DgvItemList.Rows(i).Cells("輸送費額").Value) '輸送費額

                    If PurchaseStatus <> CommonConst.STATUS_ADD And dsHattyuHd.Tables(RS).Rows.Count > 0 Then
                        If dsHattyuHd.Tables(RS).Rows(0)("見積番号") <> "" Then
                            Sql += "', '"
                            Sql += formatNumber(dsHattyuDt.Tables(RS).Rows(i)("見積単価_外貨")).ToString '見積単価_外貨
                            Sql += "', '"
                            Sql += formatNumber(dsHattyuDt.Tables(RS).Rows(i)("見積金額_外貨")).ToString '見積金額_外貨
                            Sql += "', '"
                            Sql += dsHattyuDt.Tables(RS).Rows(i)("通貨").ToString '通貨
                            Sql += "', '"
                            Sql += UtilClass.formatNumberF10(dsHattyuDt.Tables(RS).Rows(i)("レート")) 'レート
                        End If
                    End If

                    Sql += "'"

                    Sql += ", "
                    Sql += formatNumber(DgvItemList.Rows(i).Cells("仕入単価_外貨").Value)  '仕入値_外貨
                    Sql += ", "
                    Sql += formatNumber(DgvItemList.Rows(i).Cells("仕入金額_外貨").Value)  '仕入金額_外貨


                    Sql += IIf(
                                DgvItemList.Rows(i).Cells("貿易条件").Value IsNot Nothing,
                                ", '" & DgvItemList.Rows(i).Cells("貿易条件").Value & "'",
                                "") '貿易条件
                    Sql += " )"

                    _db.executeDB(Sql)
                Next

                '発注編集時に登録した場合、一つ前の枝番を取り消す
                If PurchaseStatus = CommonConst.STATUS_EDIT Then

                    Sql = "UPDATE t20_hattyu SET "
                    Sql += " 取消日 = '" & dtNow & "'"
                    Sql += " ,取消区分 = " & CommonConst.CANCEL_KBN_DISABLED.ToString
                    Sql += " WHERE "
                    Sql += " 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                    Sql += " AND "
                    Sql += " 発注番号 = '" & PurchaseNo & "'"
                    Sql += " AND "
                    Sql += " 発注番号枝番 = '" & PurchaseSuffix & "'"

                    _db.executeDB(Sql)

                End If

                '複写か編集か新規の時以外
            Else

                Sql = "UPDATE "
                Sql += "Public."
                Sql += "t20_hattyu "
                Sql += "SET "

                Sql += "備考"
                Sql += " = '"
                Sql += UtilClass.escapeSql(TxtPurchaseRemark.Text)
                Sql += "', "
                Sql += "受注日"
                Sql += " = '"
                Sql += DtpPurchaseDate.Value
                Sql += "', "
                Sql += "更新日"
                Sql += " = '"
                Sql += dtNow
                Sql += "', "
                Sql += "更新者"
                Sql += " = '"
                Sql += frmC01F10_Login.loginValue.TantoNM
                Sql += " ' "

                Sql += "WHERE"
                Sql += " 会社コード"
                Sql += "='"
                Sql += CompanyCode
                Sql += "'"
                Sql += " AND"
                Sql += " 発注番号"
                Sql += "='"
                Sql += PurchaseNo
                Sql += "' "
                Sql += " AND"
                Sql += " 発注番号枝番"
                Sql += "='"
                Sql += PurchaseSuffix
                Sql += "' "

                _db.executeDB(Sql)

            End If

            '登録完了メッセージ
            _msgHd.dspMSG("completeInsert", frmC01F10_Login.loginValue.Language)

            'Me.Close()
            _parentForm.Enabled = True
            _parentForm.Show()
            Me.Dispose()

        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '通貨の採番キーからレートを取得・設定
    '基準日が発注日「以前」の最新のもの
    Private Function setRate(ByVal strKey As Integer) As Decimal
        Dim Sql As String

        Sql = " AND 採番キー = " & strKey & ""
        Sql += " AND 基準日 < '" & UtilClass.strFormatDate(DtpPurchaseDate.Text) & "'"  '発注日
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


    '発注書発行のボタン押下時
    Private Sub BtnPurchase_Click(sender As Object, e As EventArgs) Handles BtnPurchase.Click
        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        Dim reccnt As Integer = 0
        Dim Sql As String = ""

        Sql = " AND 発注番号 = '" & PurchaseNo.ToString & "'"
        Sql += " AND 発注番号枝番 = '" & PurchaseSuffix.ToString & "'"
        Sql += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        Dim dsHattyuhd = getDsData("t20_hattyu", Sql)

        Sql = ""
        Sql += " AND 発注番号 = '" & PurchaseNo.ToString & "'"
        Sql += " AND 発注番号枝番 = '" & PurchaseSuffix.ToString & "'"

        Dim dsHattyudt = getDsData("t21_hattyu", Sql)

        Sql = " AND "
        Sql += " 仕入先コード =  '" & dsHattyuhd.Tables(RS).Rows(0)("仕入先コード") & "'"

        Dim supplierData = getDsData("m11_supplier", Sql)

        '====================================
        ' Excel作成
        '====================================
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
            sHinaFile = sHinaPath & "\" & "PurchaseOrder.xlsx"

            '出力先パス
            Dim sOutPath As String = ""
            sOutPath = StartUp._iniVal.OutXlsPath

            '出力ファイル名
            Dim sOutFile As String = ""
            sOutFile = sOutPath & "\" & dsHattyuhd.Tables(RS).Rows(0)("発注番号") & "-" & dsHattyuhd.Tables(RS).Rows(0)("発注番号枝番") & ".xlsx"

            app = New Excel.Application()
            book = app.Workbooks.Add(sHinaFile)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet)

            sheet.Range("C8").Value = dsHattyuhd.Tables(RS).Rows(0)("仕入先名") & vbLf & dsHattyuhd.Tables(RS).Rows(0)("仕入先郵便番号") & vbLf & dsHattyuhd.Tables(RS).Rows(0)("仕入先住所")
            sheet.Range("C14").Value = dsHattyuhd.Tables(RS).Rows(0)("仕入先担当者役職") & " " & dsHattyuhd.Tables(RS).Rows(0)("仕入先担当者名")
            sheet.Range("A15").Value = "Telp." & dsHattyuhd.Tables(RS).Rows(0)("仕入先電話番号") & "　Fax." & dsHattyuhd.Tables(RS).Rows(0)("仕入先ＦＡＸ")
            sheet.Range("T8").Value = dsHattyuhd.Tables(RS).Rows(0)("発注番号") & "-" & dsHattyuhd.Tables(RS).Rows(0)("発注番号枝番")
            sheet.Range("T9").Value = dsHattyuhd.Tables(RS).Rows(0)("発注日")
            If dsHattyuhd.Tables(RS).Rows(0)("出荷方法") Is DBNull.Value Then
                sheet.Range("T10").Value = ""
            Else
                Dim tmp = CbShippedBy.Items(dsHattyuhd.Tables(RS).Rows(0)("出荷方法"))
                sheet.Range("T10").Value = tmp
            End If

            sheet.Range("T11").Value = dsHattyuhd.Tables(RS).Rows(0)("出荷日")
            sheet.Range("T13").Value = dsHattyuhd.Tables(RS).Rows(0)("支払条件")

            sheet.Range("H27").Value = dsHattyuhd.Tables(RS).Rows(0)("備考")

            sheet.Range("R30").Value = dsHattyuhd.Tables(RS).Rows(0)("仕入先名")
            sheet.Range("R18").Value = "(" & getCurrency(dsHattyudt.Tables(RS).Rows(0)("仕入通貨")) & ")"


            Dim rowCnt As Integer = 0
            Dim lstRow As Integer = 21
            Dim addRowCnt As Integer = 0
            Dim currentCnt As Integer = 19
            Dim num As Integer = 1

            rowCnt = dsHattyudt.Tables(RS).Rows.Count - 1

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

            Dim totalPrice As Decimal = 0
            'Dim Sql As String = ""

            For i As Integer = 0 To DgvItemList.RowCount - 1
                sheet.Range("A" & currentCnt).Value = num
                sheet.Range("C" & currentCnt).Value = DgvItemList.Rows(i).Cells("メーカー").Value & vbLf & DgvItemList.Rows(i).Cells("品名").Value & vbLf & DgvItemList.Rows(i).Cells("型式").Value
                sheet.Range("L" & currentCnt).Value = DgvItemList.Rows(i).Cells("数量").Value & " " & DgvItemList.Rows(i).Cells("単位").Value

                Dim strValue As String = DgvItemList.Rows(i).Cells("貿易条件").Value
                If strValue = Nothing Then
                    sheet.Range("O" & currentCnt).Value = ""
                Else
                    Dim dsHanyoA = getDsHanyoData(CommonConst.FIXED_KEY_TRADE_TERMS, strValue)
                    sheet.Range("O" & currentCnt).Value = dsHanyoA.Tables(RS).Rows(0)("文字１")
                End If

                sheet.Range("R" & currentCnt).Value = DgvItemList.Rows(i).Cells("仕入単価_外貨").Value.ToString
                sheet.Range("W" & currentCnt).Value = DgvItemList.Rows(i).Cells("仕入単価_外貨").Value * DgvItemList.Rows(i).Cells("数量").Value

                totalPrice = totalPrice + DgvItemList.Rows(i).Cells("仕入単価_外貨").Value * DgvItemList.Rows(i).Cells("数量").Value

                currentCnt = currentCnt + 1
                num = num + 1
            Next

            'For i As Integer = 0 To dsHattyudt.Tables(RS).Rows.Count - 1
            '    Dim cell As String

            '    cell = "A" & currentCnt
            '    sheet.Range(cell).Value = num
            '    cell = "C" & currentCnt
            '    sheet.Range(cell).Value = dsHattyudt.Tables(RS).Rows(i)("メーカー") & vbLf & dsHattyudt.Tables(RS).Rows(i)("品名") & vbLf & dsHattyudt.Tables(RS).Rows(i)("型式")
            '    cell = "L" & currentCnt
            '    sheet.Range(cell).Value = dsHattyudt.Tables(RS).Rows(i)("発注数量") & " " & dsHattyudt.Tables(RS).Rows(i)("単位")

            '    Dim dsHanyo = getDsHanyoData(CommonConst.FIXED_KEY_TRADE_TERMS, dsHattyudt.Tables(RS).Rows(i)("貿易条件").ToString)

            '    cell = "O" & currentCnt
            '    If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            '        sheet.Range(cell).Value = dsHanyo.Tables(RS).Rows(0)("文字２")
            '    Else
            '        sheet.Range(cell).Value = dsHanyo.Tables(RS).Rows(0)("文字１")
            '    End If

            '    cell = "R" & currentCnt
            '    sheet.Range(cell).Value = dsHattyudt.Tables(RS).Rows(i)("仕入単価_外貨")
            '    cell = "W" & currentCnt
            '    sheet.Range(cell).Value = dsHattyudt.Tables(RS).Rows(i)("仕入単価_外貨") * dsHattyudt.Tables(RS).Rows(i)("発注数量")

            '    totalPrice = totalPrice + dsHattyudt.Tables(RS).Rows(i)("仕入金額")

            '    currentCnt = currentCnt + 1
            '    num = num + 1

            'Next

            sheet.Range("W" & lstRow + 1).Value = totalPrice 'Subtotal
            sheet.Range("W" & lstRow + 2).Value = IIf(supplierData.Tables(RS).Rows(0)("国内区分") = CommonConst.DD_KBN_OVERSEAS,
                                                      "",
                                                      Math.Ceiling(totalPrice * 10 * 0.01)) 'VAT
            sheet.Range("W" & lstRow + 3).Value = Math.Ceiling(totalPrice * 10 * 0.01) + totalPrice 'TOTAL
            sheet.Range("H" & lstRow + 5).Value = Math.Ceiling(totalPrice * 10 * 0.01) + totalPrice 'REMARKS ?

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

    '仕入先検索ボタン
    Private Sub BtnCodeSearch_Click(sender As Object, e As EventArgs) Handles BtnCodeSearch.Click
        Dim Sql As String = ""

        Sql = " AND "
        Sql += "仕入先コード"
        Sql += " ILIKE "
        Sql += "'"
        Sql += TxtSupplierCode.Text
        Sql += "'"

        Dim dsSupplier As DataSet = getDsData("m11_supplier", Sql)

        If dsSupplier.Tables(RS).Rows.Count > 0 Then
            TxtSupplierName.Text = dsSupplier.Tables(RS).Rows(0)("仕入先名").ToString
            TxtPostalCode.Text = dsSupplier.Tables(RS).Rows(0)("郵便番号").ToString
            TxtAddress1.Text = dsSupplier.Tables(RS).Rows(0)("住所１").ToString & " " & dsSupplier.Tables(RS).Rows(0)("住所２").ToString & " " & dsSupplier.Tables(RS).Rows(0)("住所３").ToString
            TxtTel.Text = dsSupplier.Tables(RS).Rows(0)("電話番号").ToString
            TxtFax.Text = dsSupplier.Tables(RS).Rows(0)("ＦＡＸ番号").ToString
            TxtPerson.Text = dsSupplier.Tables(RS).Rows(0)("担当者名").ToString
            TxtPosition.Text = dsSupplier.Tables(RS).Rows(0)("担当者役職").ToString

        End If
    End Sub

    '金額フォーマット（登録の際の小数点指定子）を日本の形式に合わせる
    '桁区切り記号は外す
    Private Function formatNumber(ByVal prmVal As Decimal) As String

        Dim nfi As NumberFormatInfo = New CultureInfo(CommonConst.CI_JP, False).NumberFormat

        '日本の形式に書き換える
        Return prmVal.ToString("F3", nfi) '売掛残高を増やす
    End Function

    'sqlで実行する文字列からシングルクォーテーションを文字コードにする
    Private Function escapeSql(ByVal prmSql As String) As String
        Dim sql As String = prmSql

        sql = sql.Replace("'"c, "''") 'シングルクォーテーションを置換

        Return Regex.Escape(sql)
        Return sql
    End Function

    'param1：String テーブル名
    'param2：String 詳細条件
    'Return: DataSet
    Private Function getDsData(ByVal tableName As String, Optional ByRef txtParam As String = "") As DataSet
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Sql += "SELECT * FROM public." & tableName
        Sql += " WHERE 会社コード =  '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += txtParam

        Return _db.selectDB(Sql, RS, reccnt)
    End Function

    '汎用マスタから固定キー、可変キーに応じた結果を返す
    'param1：String 固定キー
    'param2：String 可変キー
    'Return: DataSet
    Private Function getDsHanyoData(ByVal prmFixed As String, Optional ByRef prmVariable As String = "") As DataSet
        Dim Sql As String = ""

        Sql = " AND 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND 固定キー = '" & prmFixed & "'"

        If prmVariable IsNot "" Then
            Sql += " AND 可変キー = '" & prmVariable & "'"
        End If

        'リードタイムのリストを汎用マスタから取得
        Return getDsData("m90_hanyo", Sql)

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
            Sql = "SELECT * FROM public.m80_saiban"
            Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND 採番キー = '" & key & "'"

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

            Sql = "UPDATE Public.m80_saiban "
            Sql += "SET  最新値  = '" & keyNo.ToString & "'"
            Sql += " , 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
            Sql += " , 更新日 = '" & UtilClass.formatDatetime(today) & "'"
            Sql += "WHERE 会社コード ='" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND 採番キー = '" & key & "'"
            Console.WriteLine(Sql)
            _db.executeDB(Sql)

            Return saibanID
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Function

    'どんなカルチャーであっても、日本の形式に変換する
    Private Function strFormatDate(ByVal prmDate As String, Optional ByRef prmFormat As String = "yyyy/MM/dd") As String

        'PCのカルチャーを取得し、それに応じてStringからDatetimeを作成
        Dim ci As New System.Globalization.CultureInfo(CultureInfo.CurrentCulture.Name.ToString)
        Dim dateFormat As DateTime = DateTime.Parse(prmDate, ci, System.Globalization.DateTimeStyles.AssumeLocal)

        '日本の形式に書き換える
        Return dateFormat.ToString(prmFormat)
    End Function

    'どんなカルチャーであっても、日本の形式に変換する
    Private Function formatDatetime(ByVal prmDatetime As DateTime) As String

        'PCのカルチャーを取得し、それに応じてStringからDatetimeを作成
        Dim ciCurrent As New System.Globalization.CultureInfo(CultureInfo.CurrentCulture.Name.ToString)
        Dim dateFormat As DateTime = DateTime.Parse(prmDatetime.ToString, ciCurrent, System.Globalization.DateTimeStyles.AssumeLocal)

        Dim changeFormat As String = dateFormat.ToString("yyyy/MM/dd HH:mm:ss")

        Dim ciJP As New System.Globalization.CultureInfo(CommonConst.CI_JP)
        Dim rtnDatetime As DateTime = DateTime.Parse(changeFormat, ciJP, System.Globalization.DateTimeStyles.AssumeLocal)


        '日本の形式に書き換える
        Return changeFormat
    End Function

    '発注日変更時、出荷日のMinDate及びValueを変更
    Private Sub DtpPurchaseDate_ValueChanged(sender As Object, e As EventArgs) Handles DtpPurchaseDate.ValueChanged
        '出荷日が発注日より小さかったら
        If DtpPurchaseDate.Value.ToString("yyyyMMdd") > DtpShippedDate.Value.ToString("yyyyMMdd") Then
            'DtpShippedDate.MinDate = DtpPurchaseDate.Value  '過去日を入力できるようにするためコメントアウト
            DtpShippedDate.Value = DtpPurchaseDate.Value
        End If

    End Sub

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

        If prmVal IsNot "" Then
            CmWarehouse.SelectedValue = prmVal
        Else
            CmWarehouse.SelectedIndex = 0
        End If

    End Sub

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
    '基準日が発注日「以前」の最新のもの
    Private Sub setRate()
        Dim Sql As String

        Sql = " AND 採番キー = " & CmCurrency.SelectedValue.ToString & ""
        Sql += " AND 基準日 < '" & UtilClass.strFormatDate(DtpPurchaseDate.Text) & "'"  '発注日
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
        Dim PurchaseTotal As Decimal = 0
        Dim CurrencyTotal As Decimal = 0
        Dim Purchasecost As Decimal = 0

        For c As Integer = 0 To DgvItemList.Rows.Count - 1
            Purchasecost += DgvItemList.Rows(c).Cells("仕入原価").Value
            PurchaseTotal += DgvItemList.Rows(c).Cells("仕入金額").Value
            CurrencyTotal += DgvItemList.Rows(c).Cells("仕入金額_外貨").Value
        Next

        txtPurchasecost.Text = Purchasecost.ToString("N0")     '仕入原価
        TxtPurchaseAmount.Text = PurchaseTotal.ToString("N0")

        TxtPurchaseAmount2.Text = CurrencyTotal.ToString("N0")

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

                If DgvItemList.Rows(i).Cells("仕入単価_外貨").Value IsNot Nothing And DgvItemList.Rows(i).Cells("数量").Value IsNot Nothing Then

                    '小数点表示にするため切り上げをコメントアウト
                    'DgvItemList.Rows(i).Cells("仕入単価").Value = Math.Ceiling(DgvItemList.Rows(i).Cells("仕入単価_外貨").Value / TxtRate.Text)
                    DgvItemList.Rows(i).Cells("仕入単価").Value = DgvItemList.Rows(i).Cells("仕入単価_外貨").Value / TxtRate.Text
                    DgvItemList.Rows(i).Cells("仕入原価").Value = Math.Ceiling(DgvItemList.Rows(i).Cells("仕入単価").Value * DgvItemList.Rows(i).Cells("数量").Value)


                    '関税率
                    If DgvItemList.Rows(i).Cells("関税率").Value IsNot Nothing Then
                        DgvItemList.Rows(i).Cells("関税額").Value = Math.Ceiling(DgvItemList.Rows(i).Cells("仕入単価").Value * DgvItemList.Rows(i).Cells("関税率").Value)
                    End If

                    '前払法人税率, 関税額 <> Nothing
                    If DgvItemList.Rows(i).Cells("前払法人税率").Value IsNot Nothing And DgvItemList.Rows(i).Cells("関税額").Value IsNot Nothing Then
                        Dim tmp As Decimal = DgvItemList.Rows(i).Cells("仕入単価").Value + DgvItemList.Rows(i).Cells("関税額").Value
                        Dim tmp1 As Decimal = Math.Ceiling(tmp * DgvItemList.Rows(i).Cells("前払法人税率").Value)
                        tmp1 = Math.Ceiling(tmp1)
                        DgvItemList.Rows(i).Cells("前払法人税額").Value = tmp1
                    End If

                    '輸送費率
                    If DgvItemList.Rows(i).Cells("輸送費率").Value IsNot Nothing Then
                        DgvItemList.Rows(i).Cells("輸送費額").Value = Math.Ceiling(DgvItemList.Rows(i).Cells("仕入単価").Value * DgvItemList.Rows(i).Cells("輸送費率").Value)
                    End If


                    DgvItemList.Rows(i).Cells("仕入金額").Value = DgvItemList.Rows(i).Cells("仕入原価").Value _
                                                                     + (DgvItemList.Rows(i).Cells("関税額").Value + DgvItemList.Rows(i).Cells("前払法人税額").Value + DgvItemList.Rows(i).Cells("輸送費額").Value) _
                                                                     * DgvItemList.Rows(i).Cells("数量").Value

                    '仕入金額_外貨
                    If (DgvItemList.Rows(i).Cells("関税額").Value Is Nothing Or DgvItemList.Rows(i).Cells("関税額").Value = 0) _
                       And (DgvItemList.Rows(i).Cells("前払法人税額").Value Is Nothing Or DgvItemList.Rows(i).Cells("前払法人税額").Value = 0) _
                       And (DgvItemList.Rows(i).Cells("輸送費額").Value Is Nothing Or DgvItemList.Rows(i).Cells("輸送費額").Value = 0) Then

                        DgvItemList.Rows(i).Cells("仕入金額_外貨").Value = Math.Ceiling(DgvItemList.Rows(i).Cells("仕入単価_外貨").Value * DgvItemList.Rows(i).Cells("数量").Value)
                    Else
                        DgvItemList.Rows(i).Cells("仕入金額_外貨").Value = Math.Ceiling(DgvItemList.Rows(i).Cells("仕入金額").Value * TxtRate.Text)
                    End If

                End If

                setCellValueChanged()
            Next
        End If
    End Sub

    Private Sub CmCurrency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmCurrency.SelectedIndexChanged
        setRate()
        setChangeCurrency()
        resetListCurrency()  '明細
        setCurrency()        'ヘッダー
    End Sub

    Private Sub setSireTax(ByVal prmRowIndex As Integer)
        Dim Sql As String = ""
        Sql += " and 仕入先コード = '" & TxtSupplierCode.Text & "'"

        Dim ds As DataSet = getDsData("m11_supplier", Sql)

        If ds.Tables(RS).Rows.Count > 0 Then
            DgvItemList.Rows(prmRowIndex).Cells("関税率").Value = ds.Tables(RS).Rows(0)("関税率").ToString
            DgvItemList.Rows(prmRowIndex).Cells("前払法人税率").Value = ds.Tables(RS).Rows(0)("前払法人税率").ToString
            DgvItemList.Rows(prmRowIndex).Cells("輸送費率").Value = ds.Tables(RS).Rows(0)("輸送費率").ToString
        Else
            DgvItemList.Rows(prmRowIndex).Cells("関税率").Value = 0
            DgvItemList.Rows(prmRowIndex).Cells("前払法人税率").Value = 0
            DgvItemList.Rows(prmRowIndex).Cells("輸送費率").Value = 0

        End If

    End Sub

    Private Sub setSupplierNameChanged()

        AddHandler TxtSupplierName.TextChanged, AddressOf TxtSupplierName_TextChanged
    End Sub
    Private Sub delSupplierNameChanged()

        RemoveHandler TxtSupplierName.TextChanged, AddressOf TxtSupplierName_TextChanged
    End Sub


    Private Sub TxtSupplierName_TextChanged(sender As Object, e As EventArgs) Handles TxtSupplierName.TextChanged
        Dim Sql As String = ""
        Sql += " and 仕入先コード = '" & TxtSupplierCode.Text & "'"

        Dim ds As DataSet = getDsData("m11_supplier", Sql)

        For i As Integer = 0 To DgvItemList.Rows.Count - 1
            DgvItemList.Rows(i).Cells("関税率").Value = ds.Tables(RS).Rows(0)("関税率").ToString
            DgvItemList.Rows(i).Cells("前払法人税率").Value = ds.Tables(RS).Rows(0)("前払法人税率").ToString
            DgvItemList.Rows(i).Cells("輸送費率").Value = ds.Tables(RS).Rows(0)("輸送費率").ToString
        Next

    End Sub

    Private Function getCurrency(ByVal prmVal As Integer) As String
        Dim Sql As String = " AND 取消区分 = '" & CommonConst.FLAG_ENABLED & "'"
        Sql += " AND 採番キー =" & prmVal.ToString

        Dim ds As DataSet = getDsData("m25_currency", Sql)

        'リードタイム単位の多言語対応

        Return ds.Tables(RS).Rows(0)("通貨コード")

    End Function
End Class