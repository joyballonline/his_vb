﻿Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Globalization

Public Class AccountsPayable
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

    'Private CompanyCode As String = ""
    Private HattyuNo As String = ""
    Private Suffix As String = ""
    Private _parentForm As Form
    Private _status As String = ""
    'Private checkAdd As Integer
    Private _com As CommonLogic
    Private _vs As String = "1"


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
                   ByRef prmRefNo As String,
                   ByRef prmRefSuffix As String,
                   Optional ByRef prmRefStatus As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
        HattyuNo = prmRefNo
        Suffix = prmRefSuffix
        _status = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text += _vs
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        DtpAPDate.Value = Date.Now
        DtpPaymentDate.Value = Date.Now
        _com = New CommonLogic(_db, _msgHd)
        _init = True

    End Sub

    '画面表示時
    Private Sub AccountsPayable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'ComboBoxに表示する項目のリストを作成する
        Dim table As New DataTable("Table")
        table.Columns.Add("Display", GetType(String))
        table.Columns.Add("Value", GetType(Integer))

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            table.Rows.Add(CommonConst.APC_KBN_DEPOSIT_TXT_E, 1)
            table.Rows.Add(CommonConst.APC_KBN_NORMAL_TXT_E, 2)

        Else

            table.Rows.Add(CommonConst.APC_KBN_DEPOSIT_TXT, 1)
            table.Rows.Add(CommonConst.APC_KBN_NORMAL_TXT, 2)

        End If


        'DataGridViewComboBoxColumnを作成
        Dim column As New DataGridViewComboBoxColumn()
        'DataGridViewComboBoxColumnのDataSourceを設定
        column.DataSource = table
        '実際の値が"Value"列、表示するテキストが"Display"列とする
        column.ValueMember = "Value"
        column.DisplayMember = "Display"
        column.HeaderText = "買掛区分"
        column.Name = "買掛区分"

        'DataGridView1に追加する
        DgvAdd.Columns.Insert(1, column)

        BillLoad()

        '言語の判定
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then  '英語 

            LblAccountsPayableDate.Text = "AccountsPayableDate"
            LblAccountsPayableDate.Location = New Point(237, 379)
            LblAccountsPayableDate.Size = New Size(205, 22)
            DtpAPDate.Location = New Point(458, 379)

            lblVendorInvoiceNumber.Location = New Point(236, 424)
            lblVendorInvoiceNumber.Size = New Size(235, 22)
            VendorInvoiceNumber.Location = New Point(478, 424)

            LblNo1.Text = "Record"
            LblNo1.Location = New Point(1272, 111)
            LblNo1.Size = New Size(66, 22)
            LblNo2.Text = "Record"
            LblNo2.Location = New Point(1272, 245)
            LblNo2.Size = New Size(66, 22)
            LblNo3.Text = "Record"
            LblNo3.Location = New Point(1272, 387)
            LblNo3.Size = New Size(66, 22)
            LblPurchaseOrder.Text = "PurchaseOrderDetails"
            LblPaymentDate.Text = "PaymentDueDate"
            LblHistory.Text = "AccountsPayableHistoryData"
            LblAdd.Text = "AcceptAccountPayableThisTime"

            lblVendorInvoiceNumber.Text = "SupplierInvoice"

            TxtHattyudtCount.Location = New Point(1228, 111)
            TxtKikehdCount.Location = New Point(1228, 245)
            TxtCount3.Location = New Point(1228, 387)


            BtnRegist.Text = "Registration"
            BtnBack.Text = "Back"


            DgvCymn.Height = 67
            Label1.Text = "PurchaseOrder"
            LblIDRCurrency.Text = "Currency"

            DgvCymn.Columns("発注番号").HeaderText = _langHd.getLANG("発注番号", frmC01F10_Login.loginValue.Language)
            DgvCymn.Columns("発注番号枝番").HeaderText = _langHd.getLANG("発注番号枝番", frmC01F10_Login.loginValue.Language)
            DgvCymn.Columns("発注日").HeaderText = "OrderDate"

            DgvCymn.Columns("仕入先コード").HeaderText = "SupplierCode"
            DgvCymn.Columns("仕入先").HeaderText = "SupplierName"
            DgvCymn.Columns("客先番号").HeaderText = _langHd.getLANG("客先番号", frmC01F10_Login.loginValue.Language)

            DgvCymn.Columns("仕入原価").HeaderText = "PurchaseCost" & vbCrLf & "a"
            DgvCymn.Columns("VAT_IN").HeaderText = "VAT-IN" & vbCrLf & "b"

            DgvCymn.Columns("発注金額").HeaderText = "PurchaseOrderAmount" & vbCrLf & "c=a+b"
            DgvCymn.Columns("買掛金額計").HeaderText = "TotalAccountsPayable" & vbCrLf & "d"
            DgvCymn.Columns("買掛残高").HeaderText = "AccountsPayableBalance" & vbCrLf & "e=c-d"

            DgvCymndt.Columns("明細").HeaderText = "Purchase order details"
            DgvCymndt.Columns("メーカー").HeaderText = "Manufacturer"
            DgvCymndt.Columns("品名").HeaderText = "ItemName"
            DgvCymndt.Columns("型式").HeaderText = "Spec"
            'DgvCymndt.Columns("発注個数").HeaderText = "OrderQuantity"
            DgvCymndt.Columns("単位").HeaderText = "Unit"
            DgvCymndt.Columns("仕入数量").HeaderText = "PurchaseQuantity" & vbCrLf & "f"
            DgvCymndt.Columns("仕入単価").HeaderText = "PurchaseUnitPrice" & vbCrLf & "g"
            DgvCymndt.Columns("仕入金額").HeaderText = "PurchaseCost" & vbCrLf & "h=f*g"
            DgvCymndt.Columns("VAT").HeaderText = "VAT-IN" & vbCrLf & "i"
            DgvCymndt.Columns("仕入金額計").HeaderText = "PurchaseCostSum" & vbCrLf & "j=h+i"

            DgvCymndt.Columns("買掛").HeaderText = "Payable"


            DgvHistory.Columns("買掛番号").HeaderText = "AccountsPayableNumber"
            DgvHistory.Columns("買掛日").HeaderText = "AccountsPayableDate"
            DgvHistory.Columns("買掛区分").HeaderText = "AccountsPayableClassification"
            DgvHistory.Columns("支払先").HeaderText = "PaymentDestination"
            DgvHistory.Columns("買掛金額").HeaderText = "TotalAccountsPayable"
            DgvHistory.Columns("備考1").HeaderText = "Remarks1"
            DgvHistory.Columns("備考2").HeaderText = "Remarks2"

            DgvAdd.Columns("買掛区分").HeaderText = "AccountsPayableClassification"
            DgvAdd.Columns("今回支払先").HeaderText = "PaymentDestination"
            DgvAdd.Columns("今回買掛金額計").HeaderText = "TotalAccountsPayable"
            DgvAdd.Columns("今回備考1").HeaderText = "Remarks1"
            DgvAdd.Columns("今回備考2").HeaderText = "Remarks2"

        Else  '日本語

            DgvCymn.Columns("仕入原価").HeaderText = "仕入原価" & vbCrLf & "a"
            DgvCymn.Columns("VAT_IN").HeaderText = "VAT-IN" & vbCrLf & "b"
            DgvCymn.Columns("発注金額").HeaderText = "買掛金額" & vbCrLf & "c=a+b"
            DgvCymn.Columns("買掛金額計").HeaderText = "既登録額" & vbCrLf & "d"
            DgvCymn.Columns("買掛残高").HeaderText = "未登録額" & vbCrLf & "e=c-d"


            DgvCymndt.Columns("仕入数量").HeaderText = "数量" & vbCrLf & "f"
            DgvCymndt.Columns("仕入単価").HeaderText = "仕入単価" & vbCrLf & "g"
            DgvCymndt.Columns("仕入金額").HeaderText = "仕入原価" & vbCrLf & "h=f*g"

        End If

        If _status = CommonConst.STATUS_VIEW Then

            LblMode.Text = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                            "ViewMode",
                            "参照モード")

            LblNo1.Visible = False
            LblNo2.Visible = False
            LblNo2.Visible = False
            LblPurchaseOrder.Visible = False
            LblAdd.Visible = False
            LblAccountsPayableDate.Visible = False
            DtpAPDate.Visible = False
            DtpPaymentDate.Visible = False
            TxtHattyudtCount.Visible = False
            TxtKikehdCount.Visible = False
            TxtCount3.Visible = False
            DgvCymn.Visible = False
            DgvCymndt.Visible = False
            DgvAdd.Visible = False
            DgvHistory.ReadOnly = False

            LblHistory.Location = New Point(12, 82)
            DgvHistory.Location = New Point(12, 106)
            DgvHistory.Size = New Point(1326, 566)

            BtnRegist.Visible = False
        Else

            LblMode.Text = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                            "AccountsPayableInputMode",
                            "買掛入力モード")
        End If

        '中央寄せ
        DgvCymn.Columns("発注番号").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymn.Columns("発注番号枝番").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymn.Columns("発注日").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymn.Columns("仕入先コード").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymn.Columns("仕入先").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymn.Columns("客先番号").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        DgvCymn.Columns("仕入原価").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymn.Columns("VAT_IN").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        DgvCymn.Columns("発注金額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymn.Columns("買掛金額計").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymn.Columns("買掛残高").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        DgvCymndt.Columns("明細").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymndt.Columns("メーカー").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymndt.Columns("品名").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymndt.Columns("型式").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymndt.Columns("発注個数").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymndt.Columns("単位").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymndt.Columns("仕入数量").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymndt.Columns("仕入単価").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymndt.Columns("仕入金額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        'DgvCymndt.Columns("VAT").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        'DgvCymndt.Columns("仕入金額計").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        DgvHistory.Columns("買掛番号").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvHistory.Columns("買掛日").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvHistory.Columns("買掛区分").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvHistory.Columns("支払先").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvHistory.Columns("備考1").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvHistory.Columns("備考2").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        DgvAdd.Columns("買掛区分").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvAdd.Columns("今回支払先").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvAdd.Columns("今回買掛金額計").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvAdd.Columns("今回備考1").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvAdd.Columns("今回備考2").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter


        '数字形式
        DgvCymn.Columns("仕入原価").DefaultCellStyle.Format = "N2"
        DgvCymn.Columns("VAT_IN").DefaultCellStyle.Format = "N2"

        DgvCymn.Columns("発注金額").DefaultCellStyle.Format = "N2"
        DgvCymn.Columns("買掛金額計").DefaultCellStyle.Format = "N2"
        DgvCymn.Columns("買掛残高").DefaultCellStyle.Format = "N2"

        'DgvCymndt.Columns("発注個数").DefaultCellStyle.Format = "N2"
        DgvCymndt.Columns("仕入数量").DefaultCellStyle.Format = "N2"
        DgvCymndt.Columns("仕入単価").DefaultCellStyle.Format = "N2"
        DgvCymndt.Columns("仕入金額").DefaultCellStyle.Format = "N2"
        DgvCymndt.Columns("VAT").DefaultCellStyle.Format = "N2"
        DgvCymndt.Columns("仕入金額計").DefaultCellStyle.Format = "N2"

        DgvHistory.Columns("買掛金額").DefaultCellStyle.Format = "N2"
        DgvHistory.Columns("VAT2").DefaultCellStyle.Format = "N2"

        DgvAdd.Columns("今回買掛金額計").DefaultCellStyle.Format = "N2"
        DgvAdd.Columns("VAT3").DefaultCellStyle.Format = "N2"

        '右寄せ
        DgvCymn.Columns("仕入原価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymn.Columns("VAT_IN").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DgvCymndt.Columns("VAT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymndt.Columns("仕入金額計").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


        '今回買掛の初期カーソル位置
        If DgvAdd.Rows.Count > 0 Then
            DgvAdd.CurrentCell = DgvAdd(3, 0)
        End If

    End Sub

    '発注基本
    Private Sub BillLoad()

        Dim reccnt As Integer = 0
        Dim Sql As String = ""
        Dim AccountsPayable As Decimal = 0 '買掛残高を集計
        Dim PurchaseCostFC As Decimal = 0  '仕入原価_外貨
        Dim PurchaseAmountFC As Decimal = 0  '仕入金額_外貨
        Dim VAT_FC As Decimal = 0

        Sql = " AND "
        Sql += "発注番号 ILIKE '" & HattyuNo & "'"
        Sql += " AND "
        Sql += "発注番号枝番 ILIKE '" & Suffix & "'"
        Sql += " AND "
        Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
        Sql += " ORDER BY 更新日 DESC "

        Dim dsHattyu As DataSet = getDsData("t20_hattyu", Sql)

        Sql = " SELECT t21.* ,t20.仕入先コード ,t20.ＶＡＴ"
        Sql += " FROM "
        Sql += " t20_hattyu t20"
        Sql += " INNER JOIN t21_hattyu t21 ON "

        Sql += " t20.""発注番号"" = t21.""発注番号"""
        Sql += " AND "
        Sql += " t20.""発注番号枝番"" = t21.""発注番号枝番"""

        Sql += " where "
        Sql += " t20.""会社コード"" = '" & frmC01F10_Login.loginValue.BumonCD & "'"

        Sql += " AND "
        Sql += " t20.""発注番号"" ILIKE '" & dsHattyu.Tables(RS).Rows(0)("発注番号") & "'"
        Sql += " AND "
        Sql += " t20.""発注番号枝番"" ILIKE '" & Suffix & "'"
        Sql += " AND "
        Sql += " t20.""取消区分"" = " & CommonConst.CANCEL_KBN_ENABLED
        Sql += " ORDER BY 行番号 ASC "

        Dim dsHattyudt As DataSet = _db.selectDB(Sql, RS, reccnt)

        Sql = " AND "
        Sql += "発注番号 ILIKE '" & HattyuNo & "'"
        Sql += " AND "
        Sql += "発注番号枝番 ILIKE '" & Suffix & "'"
        Sql += " AND "
        Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
        Sql += " ORDER BY 更新日 DESC "

        Dim dsKikehd As DataSet = getDsData("t46_kikehd", Sql)

        TxtIDRCurrency.Text = _com.getCurrencyEx(dsHattyu.Tables(RS).Rows(0)("通貨"))

        If dsKikehd.Tables(RS).Rows.Count > 0 Then
            '買掛データがある場合
            VendorInvoiceNumber.Text = dsKikehd.Tables(RS).Rows(0)("仕入先請求番号").ToString  '仕入先請求番号 
        End If

        '買掛残高を集計
        AccountsPayable = IIf(
            dsKikehd.Tables(RS).Compute("SUM(買掛金額計_外貨)", Nothing) IsNot DBNull.Value,
            dsKikehd.Tables(RS).Compute("SUM(買掛金額計_外貨)", Nothing),
            0
        )

        '発注明細データより仕入金額を算出
        For i As Integer = 0 To dsHattyudt.Tables(RS).Rows.Count - 1
            PurchaseCostFC = PurchaseCostFC + (dsHattyudt.Tables(RS).Rows(i)("仕入値_外貨") * dsHattyudt.Tables(RS).Rows(i)("発注数量"))
        Next

        VAT_FC = UtilClass.VAT_round_AP(dsHattyu.Tables(RS).Rows(0)("ＶＡＴ"), PurchaseCostFC) 'Math.Round(PurchaseCostFC * dsHattyu.Tables(RS).Rows(0)("ＶＡＴ").ToString / 100, 2)
        PurchaseAmountFC = PurchaseCostFC + VAT_FC

        DgvCymn.Rows.Add()
        DgvCymn.Rows(0).Cells("発注番号").Value = dsHattyu.Tables(RS).Rows(0)("発注番号")
        DgvCymn.Rows(0).Cells("発注番号枝番").Value = dsHattyu.Tables(RS).Rows(0)("発注番号枝番")
        DgvCymn.Rows(0).Cells("発注日").Value = dsHattyu.Tables(RS).Rows(0)("発注日").ToShortDateString()
        DgvCymn.Rows(0).Cells("仕入先コード").Value = dsHattyu.Tables(RS).Rows(0)("仕入先コード")
        DgvCymn.Rows(0).Cells("仕入先").Value = dsHattyu.Tables(RS).Rows(0)("仕入先名")
        DgvCymn.Rows(0).Cells("客先番号").Value = dsHattyu.Tables(RS).Rows(0)("客先番号").ToString

        DgvCymn.Rows(0).Cells("仕入原価").Value = PurchaseCostFC
        DgvCymn.Rows(0).Cells("VAT_IN").Value = VAT_FC.ToString("N2")

        'PurchaseAmountFC += dsHattyu.Tables(RS).Rows(0)("ＶＡＴ").ToString

        DgvCymn.Rows(0).Cells("発注金額").Value = PurchaseAmountFC
        DgvCymn.Rows(0).Cells("買掛金額計").Value = AccountsPayable
        DgvCymn.Rows(0).Cells("買掛残高").Value = PurchaseAmountFC - AccountsPayable

        '#633 のためコメントアウト
        'DtpAPDate.MinDate = dsHattyu.Tables(RS).Rows(0)("発注日").ToShortDateString()
        'DtpPaymentDate.MinDate = dsHattyu.Tables(RS).Rows(0)("発注日").ToShortDateString()

        'checkAdd = DgvCymn.Rows(0).Cells("買掛残高").Value

        For i As Integer = 0 To dsHattyudt.Tables(RS).Rows.Count - 1
            DgvCymndt.Rows.Add()
            DgvCymndt.Rows(i).Cells("明細").Value = dsHattyudt.Tables(RS).Rows(i)("行番号")
            DgvCymndt.Rows(i).Cells("メーカー").Value = dsHattyudt.Tables(RS).Rows(i)("メーカー")
            DgvCymndt.Rows(i).Cells("品名").Value = dsHattyudt.Tables(RS).Rows(i)("品名")
            DgvCymndt.Rows(i).Cells("型式").Value = dsHattyudt.Tables(RS).Rows(i)("型式")
            DgvCymndt.Rows(i).Cells("発注個数").Value = dsHattyudt.Tables(RS).Rows(i)("発注数量")
            DgvCymndt.Rows(i).Cells("単位").Value = dsHattyudt.Tables(RS).Rows(i)("単位")
            DgvCymndt.Rows(i).Cells("仕入数量").Value = dsHattyudt.Tables(RS).Rows(i)("発注数量")
            DgvCymndt.Rows(i).Cells("仕入単価").Value = dsHattyudt.Tables(RS).Rows(i)("仕入値_外貨")
            DgvCymndt.Rows(i).Cells("仕入金額").Value = dsHattyudt.Tables(RS).Rows(i)("仕入値_外貨") * dsHattyudt.Tables(RS).Rows(i)("発注数量")
            DgvCymndt.Rows(i).Cells("VAT").Value = UtilClass.VAT_round_AP(dsHattyu.Tables(RS).Rows(0)("VAT"), DgvCymndt.Rows(i).Cells("仕入金額").Value) ' * dsHattyu.Tables(RS).Rows(0)("VAT") / 100)
            DgvCymndt.Rows(i).Cells("仕入金額計").Value = DgvCymndt.Rows(i).Cells("仕入金額").Value + DgvCymndt.Rows(i).Cells("VAT").Value

            DgvCymndt.Rows(i).Cells("買掛").Value = False
        Next

        TxtHattyudtCount.Text = dsHattyudt.Tables(RS).Rows.Count

        For i As Integer = 0 To dsKikehd.Tables(RS).Rows.Count - 1
            DgvHistory.Rows.Add()
            DgvHistory.Rows(i).Cells("No").Value = i + 1
            DgvHistory.Rows(i).Cells("買掛番号").Value = dsKikehd.Tables(RS).Rows(i)("買掛番号")
            DgvHistory.Rows(i).Cells("買掛区分").Value = IIf(dsKikehd.Tables(RS).Rows(i)("買掛区分") = CommonConst.APC_KBN_DEPOSIT.ToString,
                                                                                                    CommonConst.APC_KBN_DEPOSIT_TXT,
                                                                                                    CommonConst.APC_KBN_NORMAL_TXT)
            DgvHistory.Rows(i).Cells("買掛日").Value = dsKikehd.Tables(RS).Rows(i)("買掛日").ToShortDateString()
            DgvHistory.Rows(i).Cells("仕入先請求番号").Value = dsKikehd.Tables(RS).Rows(i)("仕入先請求番号")


            If frmC01F10_Login.loginValue.Language = "ENG" Then
                DgvHistory.Rows(i).Cells("買掛区分").Value = IIf(
                dsKikehd.Tables(RS).Rows(i)("買掛区分") = CommonConst.APC_KBN_DEPOSIT,
                CommonConst.APC_KBN_DEPOSIT_TXT_E,
                CommonConst.APC_KBN_NORMAL_TXT_E
            )
            Else
                DgvHistory.Rows(i).Cells("買掛区分").Value = IIf(
                dsKikehd.Tables(RS).Rows(i)("買掛区分") = CommonConst.APC_KBN_DEPOSIT,
                CommonConst.APC_KBN_DEPOSIT_TXT,
                CommonConst.APC_KBN_NORMAL_TXT
            )
            End If


            DgvHistory.Rows(i).Cells("支払先").Value = dsKikehd.Tables(RS).Rows(i)("仕入先名")
            DgvHistory.Rows(i).Cells("買掛金額").Value = dsKikehd.Tables(RS).Rows(i)("買掛金額計_外貨")
            DgvHistory.Rows(i).Cells("備考1").Value = dsKikehd.Tables(RS).Rows(i)("備考1")
            DgvHistory.Rows(i).Cells("備考2").Value = dsKikehd.Tables(RS).Rows(i)("備考2")
            DgvHistory.Rows(i).Cells("買掛済み発注番号").Value = dsKikehd.Tables(RS).Rows(i)("発注番号")
            DgvHistory.Rows(i).Cells("買掛済み発注番号枝番").Value = dsKikehd.Tables(RS).Rows(i)("発注番号枝番")
            DgvHistory.Rows(i).Cells("VAT2").Value = dsKikehd.Tables(RS).Rows(i)("買掛消費税計")
        Next

        TxtKikehdCount.Text = dsKikehd.Tables(RS).Rows.Count

        If DgvCymn.Rows(0).Cells("買掛残高").Value > 0 Then 'dsHattyu.Tables(RS).Rows(0)("仕入金額") - AccountsPayable <> 0 Then
            DgvAdd.Rows.Add()
            DgvAdd.Rows(0).Cells("AddNo").Value = 1
            DgvAdd(1, 0).Value = 2
            DgvAdd.Rows(0).Cells("今回支払先").Value = dsHattyu.Tables(RS).Rows(0)("仕入先名")

            '自動で買掛残高をセットする
            DgvAdd.Rows(0).Cells("今回買掛金額計").Value = DgvCymn.Rows(0).Cells("買掛残高").Value
            DgvAdd.Rows(0).Cells("VAT3").Value = UtilClass.intax(DgvAdd.Rows(0).Cells("今回買掛金額計").Value, dsHattyu.Tables(RS).Rows(0)("ＶＡＴ"))

            TxtCount3.Text = 1
        End If
    End Sub

    '前の画面に戻る
    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    '登録ボタン押下時
    Private Sub BtnRegist_Click(sender As Object, e As EventArgs) Handles BtnRegist.Click

        Dim dtToday As DateTime = DateTime.Now
        Dim strToday As String = UtilClass.formatDatetime(dtToday)
        Dim reccnt As Integer = 0
        Dim AccountsPayable As Decimal = 0    '買掛残高
        Dim AccountsPayableFC As Decimal = 0  '買掛残高_外貨

        Dim BuyToHangAmount As Decimal = 0    '今回買掛金額計
        Dim BuyToHangAmountFC As Decimal = 0  '今回買掛金額計_外貨

        If Decimal.Parse(DgvAdd.Rows(0).Cells("今回買掛金額計").Value) > Decimal.Parse(DgvCymn.Rows(0).Cells("買掛残高").Value) Then

            '買掛残高より買掛金額が大きい場合はアラート
            _msgHd.dspMSG("chkAccountsPayableError", frmC01F10_Login.loginValue.Language)

            Return
        End If

        If DgvAdd.Rows.Count() > 0 Then

            Dim Sql As String = ""

            'Dim AP As String = getSaiban("100", dtToday)

            Sql = " AND 発注番号 = '" & HattyuNo & "'"
            Sql += " AND 発注番号枝番 = '" & Suffix & "'"
            Sql += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

            Dim dsHattyu As DataSet = getDsData("t20_hattyu", Sql)


            Sql = " AND 発注番号 = '" & HattyuNo & "'"
            Sql += " AND 発注番号枝番 = '" & Suffix & "'"
            Sql += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

            Dim dsKikehd As DataSet = getDsData("t46_kikehd", Sql)

            '買掛残高を集計
            AccountsPayable = IIf(
            dsKikehd.Tables(RS).Compute("SUM(買掛金額計_外貨)", Nothing) IsNot DBNull.Value,
            dsKikehd.Tables(RS).Compute("SUM(買掛金額計_外貨)", Nothing),
            0)

            'Dim APTotal As Decimal = DgvAdd.Rows(0).Cells("今回買掛金額計").Value + AccountsPayable
            'Dim Balance As Decimal = dsHattyu.Tables(RS).Rows(0)("仕入金額") - APTotal

            'If Balance < 0 Then
            '    '対象データがないメッセージを表示
            '    _msgHd.dspMSG("chkAPDataError", frmC01F10_Login.loginValue.Language)

            '    Return
            'End If

            If DgvAdd.Rows(0).Cells("今回買掛金額計").Value = 0 Then
                '対象データがないメッセージを表示
                _msgHd.dspMSG("NonData", frmC01F10_Login.loginValue.Language)

                Return
            End If

            'レートの取得
            Dim strRate As Decimal = _com.setRate(dsHattyu.Tables(RS).Rows(0)("通貨"), UtilClass.strFormatDate(DtpAPDate.Text))


            '今回買掛金額計
            BuyToHangAmountFC = DgvAdd.Rows(0).Cells("今回買掛金額計").Value
            BuyToHangAmount = Math.Ceiling(BuyToHangAmountFC / strRate)  '画面の金額をIDRに変換　切り上げ

            '買掛残高
            '買掛残高の意味：今回登録した買掛金額から後工程の入金額を減額したもの
            'なので初期登録時は今回買掛金額計と同一のものが入る
            'AccountsPayableFC = DgvCymn.Rows(0).Cells("買掛残高").Value - DgvAdd.Rows(0).Cells("今回買掛金額計").Value
            'AccountsPayable = Math.Ceiling(AccountsPayableFC / strRate)  '画面の金額をIDRに変換　切り上げ
            AccountsPayableFC = DgvAdd.Rows(0).Cells("今回買掛金額計").Value
            AccountsPayable = Math.Ceiling(AccountsPayableFC / strRate)  '画面の金額をIDRに変換　切り上げ

            Dim AP As String = _com.getSaiban("100", dtToday)

            Sql = "INSERT INTO "
            Sql += "Public."
            Sql += "t46_kikehd("
            Sql += "会社コード, 買掛番号, 買掛区分, 買掛日, 発注番号, 発注番号枝番, 客先番号, 仕入先コード, 仕入先名, 買掛金額計, 買掛残高"
            Sql += ", 備考1, 備考2, 取消区分, 登録日, 更新者, 更新日, 支払予定日"
            Sql += ", 買掛金額計_外貨, 買掛残高_外貨, 通貨, レート, 仕入先請求番号, 買掛消費税計)"

            Sql += " VALUES('"
            Sql += dsHattyu.Tables(RS).Rows(0)("会社コード").ToString '会社コード
            Sql += "', '"
            Sql += AP '買掛番号
            Sql += "', '"
            Sql += DgvAdd.Rows(0).Cells("買掛区分").Value.ToString '買掛区分
            Sql += "', '"
            Sql += UtilClass.strFormatDate(DtpAPDate.Value) '買掛日
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("発注番号").ToString '発注番号
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("発注番号枝番").ToString '発注番号枝番
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("客先番号").ToString '客先番号
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("仕入先コード").ToString '仕入先コード
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("仕入先名").ToString '仕入先名
            Sql += "', "
            Sql += UtilClass.formatNumber(BuyToHangAmount) '買掛金額計
            Sql += ", "
            Sql += UtilClass.formatNumber(AccountsPayable) '買掛残高
            Sql += ", '"
            Sql += DgvAdd.Rows(0).Cells("今回備考1").Value '備考1
            Sql += "', '"
            Sql += DgvAdd.Rows(0).Cells("今回備考2").Value '備考2
            Sql += "', '"
            Sql += "0" '取消区分
            Sql += "', '"
            Sql += strToday '登録日
            Sql += "', '"
            Sql += frmC01F10_Login.loginValue.TantoNM '更新者
            Sql += "', '"
            Sql += strToday '更新日
            Sql += "', '"
            Sql += UtilClass.strFormatDate(DtpPaymentDate.Value) '支払予定日

            Sql += "',"
            Sql += UtilClass.formatNumber(BuyToHangAmountFC) '買掛金額計_外貨
            Sql += ","
            Sql += UtilClass.formatNumber(AccountsPayableFC) '買掛残高_外貨

            Sql += ","
            Sql += dsHattyu.Tables(RS).Rows(0)("通貨").ToString() '通貨
            Sql += ",'"
            Sql += UtilClass.formatNumberF10(strRate) 'レート

            Sql += "','"
            Sql += VendorInvoiceNumber.Text  '仕入先請求番号
            Sql += "',"
            Sql += UtilClass.formatNumber(DgvAdd.Rows(0).Cells("VAT3").Value)
            Sql += ")"

            _db.executeDB(Sql)

            '登録完了メッセージ
            _msgHd.dspMSG("completeInsert", frmC01F10_Login.loginValue.Language)

            _parentForm.Enabled = True
            _parentForm.Show()
            Me.Dispose()

        Else
            '登録するデータがなかったら
            _msgHd.dspMSG("NonAddData", frmC01F10_Login.loginValue.Language)
        End If


    End Sub

    '支払入力セルの値が変更されたら
    Private Sub DgvAddCellValueChanged(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DgvAdd.CellValueChanged

        Dim PurchaseTotal As Decimal = 0

        'ヘッダー以外だったら
        If e.RowIndex > -1 Then

            '操作したカラム名を取得
            Dim currentColumn As String = DgvAdd.Columns(e.ColumnIndex).Name

            If currentColumn = "今回買掛金額計" Then  'Cellが今回買掛金額計の場合

                '各項目の属性チェック
                If Not IsNumeric(DgvAdd.Rows(e.RowIndex).Cells("今回買掛金額計").Value) And (DgvAdd.Rows(e.RowIndex).Cells("今回買掛金額計").Value IsNot Nothing) Then
                    _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
                    DgvAdd.Rows(e.RowIndex).Cells("今回買掛金額計").Value = 0
                    Exit Sub
                End If

                Dim decTmp As Decimal = DgvAdd.Rows(e.RowIndex).Cells("今回買掛金額計").Value
                DgvAdd.Rows(e.RowIndex).Cells("今回買掛金額計").Value = decTmp
            End If

            'DgvAdd.Rows(e.RowIndex).Cells("今回備考1").Value = VendorInvoiceNumber.Text

        End If

    End Sub

    'param1：String テーブル名
    'param2：String 詳細条件
    'Return: DataSet
    Private Function getDsData(ByVal tableName As String, Optional ByRef txtParam As String = "") As DataSet
        Return _com.getDsData(tableName, txtParam) 'b.selectDB(Sql, RS, reccnt)
    End Function

    'DGV内で指定列名(プルダウン)は一度のクリックで開く
    'それ以外は一回で入力状態にする
    Private Sub DgvAdd_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DgvAdd.CellEnter
        If DgvAdd.Columns(e.ColumnIndex).Name = "買掛区分" Then
            SendKeys.Send("{F4}")
            'Else
            '    SendKeys.Send("{F2}")
        End If
    End Sub

    Private Sub AccountsPayable_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        '初期表示に選択状態じゃなくする
        DgvCymn.CurrentCell = Nothing
        DgvCymndt.CurrentCell = Nothing
    End Sub


    'CurrentCellDirtyStateChangedイベントハンドラ
    Private Sub DataGridView1_CurrentCellDirtyStateChanged(
        ByVal sender As Object, ByVal e As EventArgs) _
        Handles DgvCymndt.CurrentCellDirtyStateChanged

        If DgvCymndt.CurrentCellAddress.X = 10 AndAlso  '買掛フラグが更新であれば「DgvCymndt_CellValueChanged」へ
        DgvCymndt.IsCurrentCellDirty Then
            'コミットする
            DgvCymndt.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub


    Private Sub DgvCymndt_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DgvCymndt.CellValueChanged

        'ヘッダー以外だったら
        If e.RowIndex > -1 Then

            '買掛データがない場合は終了
            If DgvAdd.Rows.Count = 0 Then
                Exit Sub
            End If


            '操作したカラム名を取得
            Dim currentColumn As String = DgvCymndt.Columns(e.ColumnIndex).Name

            If currentColumn = "買掛" Then  '買掛フラグの更新の場合

                Dim decShiireSum As Decimal = 0

                For i As Integer = 0 To DgvCymndt.Rows.Count - 1  '発注明細をループ

                    'チェックがあるデータの仕入原価（仕入金額）を合計
                    If DgvCymndt.Rows(i).Cells("買掛").Value = True Then
                        decShiireSum += DgvCymndt.Rows(i).Cells("仕入金額計").Value
                    Else
                    End If
                Next

                DgvAdd.Rows(0).Cells("今回買掛金額計").Value = decShiireSum  '今回登録額

            End If
        End If
    End Sub

    Private Sub VendorInvoiceNumber_Validated(sender As Object, e As EventArgs) Handles VendorInvoiceNumber.Validated
        DgvAdd.Rows(0).Cells("今回備考1").Value = VendorInvoiceNumber.Text
    End Sub

    Private Sub BtnVat_Click(sender As Object, e As EventArgs) Handles BtnVat.Click

        Dim Sql As String = " AND "
        Sql += "発注番号 ILIKE '" & HattyuNo & "'"
        Sql += " AND "
        Sql += "発注番号枝番 ILIKE '" & Suffix & "'"
        Sql += " AND "
        Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
        Sql += " ORDER BY 更新日 DESC "
        Dim dsHattyu As DataSet = getDsData("t20_hattyu", Sql)
        Dim x As String = dsHattyu.Tables(RS).Rows(0)("仕入先コード")

        Sql = " AND "
        Sql += "仕入先コード = '" & x & "'"
        Dim dsSup As DataSet = getDsData("m11_supplier", Sql)
        Dim y As String = dsSup.Tables(RS).Rows(0)("国内区分")
        Dim s As String = "10"
        If y = "1" Then
            s = "0"
        End If

        Sql = "UPDATE public.t20_hattyu "
        Sql += "SET "
        Sql += " ＶＡＴ  = " & s & ""
        Sql += ", 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
        Sql += ", 更新日 = '" & UtilClass.formatDatetime(Today) & "'"
        Sql += " WHERE 会社コード ='" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND "
        Sql += "発注番号 = '" & HattyuNo & "'"
        Sql += " AND "
        Sql += "発注番号枝番 = '" & Suffix & "'"
        Sql += " AND "
        Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        _db.executeDB(Sql)
    End Sub
End Class