﻿Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Imports System.Globalization
Imports System.IO
Imports UtilMDL.Text

Public Class DataOutput
    Inherits System.Windows.Forms.Form

    '------------------------------------------------------------------------------------------------------
    'メンバー定数宣言
    '------------------------------------------------------------------------------------------------------
    'PG制御文字 
    Dim ds As DataSet
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
    'Private CompanyCode As String = ""
    'Private SalesNo As String()
    Private SalesStatus As String = ""
    Private _com As CommonLogic
    Private _vs As String = "2"
    Private _csv As UtilCsvHandler
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
                   ByRef prmRefStatus As String)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
        SalesStatus = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        _com = New CommonLogic(_db, _msgHd)
        _csv = New UtilCsvHandler(UtilClass.getAppPath(StartUp.assembly) & "\..\Setting\CSV.xml")
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text += _vs
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True
    End Sub

    '画面表示時
    Private Sub DataOutput_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            LblMode.Text = SalesStatus & " Mode"

            LblTarget.Text = "Target"
            LblPeriod.Text = "Period"
            RbtnQuotation.Text = "Quotation"
            RbtnJobOrder.Text = "JobOrder"
            RbtnSales.Text = "Invoiced"

            BtnCSVOutput.Text = "CSV Output"
            BtnBack.Text = "Back"

        End If

        DtpDateSince.Text = DateTime.Today
        DtpDateUntil.Text = DateTime.Today

        'getList() '一覧表示

    End Sub

    '一覧取得
    Private Sub getList()
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        If DtpDateSince.Text = "" And DtpDateUntil.Text = "" Then
            Return
        End If

        'リスト初期化
        DgvList.Columns.Clear()
        DgvList.Rows.Clear()

        'カーソルをビジー状態にする
        Cursor.Current = Cursors.WaitCursor

        If RbtnQuotation.Checked Then

            '見積選択時
            Sql = "SELECT "
            Sql += " t01.会社コード"
            Sql += ",t01.見積番号"
            Sql += ",t01.見積番号枝番"
            Sql += ",t01.得意先コード"
            Sql += ",t01.得意先名"
            Sql += ",t01.得意先担当者役職"
            Sql += ",t01.得意先担当者名"
            Sql += ",t01.見積日"
            Sql += ",t01.見積有効期限"
            Sql += ",t01.支払条件"
            Sql += ",t01.見積金額"
            Sql += ",t01.営業担当者"
            Sql += ",t01.入力担当者"
            Sql += ",t01.備考"
            Sql += ",t01.登録日"
            Sql += ",t01.仕入金額"
            Sql += ",t01.得意先住所"
            Sql += ",t01.得意先電話番号"
            Sql += ",t01.得意先ＦＡＸ"
            Sql += ",t01.粗利額"
            Sql += ",t01.受注日"
            Sql += ",t01.更新日"
            Sql += ",t01.更新者"
            Sql += ",t01.取消日"
            Sql += ",t01.取消区分"
            Sql += ",t01.得意先郵便番号"
            Sql += ",t01.ＶＡＴ"
            Sql += ",t01.営業担当者コード"
            Sql += ",t01.入力担当者コード"
            Sql += ",t02.行番号"
            Sql += ",t02.仕入区分"
            Sql += ",t02.メーカー"
            Sql += ",t02.品名"
            Sql += ",t02.型式"
            Sql += ",t02.仕入先コード"
            Sql += ",t02.仕入先名称"
            Sql += ",t02.仕入単価"
            Sql += ",t02.数量"
            Sql += ",t02.単位"
            Sql += ",t02.売単価"
            Sql += ",t02.売上金額"
            Sql += ",t02.間接費"
            Sql += ",t02.備考 as 明細備考"
            Sql += ",t02.リードタイム"
            Sql += ",t02.粗利額 As 明細粗利額"
            Sql += ",t02.粗利率"
            Sql += ",t02.仕入金額 as 明細仕入金額"
            Sql += ",t02.間接費率"
            Sql += ",t02.間接費無仕入金額"
            Sql += ",t02.仕入原価"
            Sql += ",t02.関税率"
            Sql += ",t02.関税額"
            Sql += ",t02.前払法人税率"
            Sql += ",t02.前払法人税額"
            Sql += ",t02.輸送費率"
            Sql += ",t02.輸送費額"
            Sql += ",t02.リードタイム単位"
            Sql += ",t02.見積単価"
            Sql += ",t02.見積金額 as 明細見積金額"
            Sql += " FROM t01_mithd t01 "
            Sql += " LEFT JOIN t02_mitdt t02 "
            Sql += " ON t01.会社コード = t02.会社コード "
            Sql += " AND t01.見積番号 = t02.見積番号 "
            Sql += " AND t01.見積番号枝番 = t02.見積番号枝番 "
            Sql += " WHERE t01.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND t01.取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'"
            Sql += " AND t01.見積日 >= '" & UtilClass.strFormatDate(DtpDateSince.Text) & "'"
            Sql += " AND t01.見積日 <= '" & UtilClass.strFormatDate(DtpDateUntil.Text) & "'"
            Sql += " AND t01.取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'"
            Sql += " ORDER BY t01.見積日, t01.見積番号, t01.見積番号枝番, t01.得意先コード "

        ElseIf RbtnJobOrder.Checked Then

            Dim n As Integer = _csv.load("JOBORDER")
            '受注選択時

            Sql = "SELECT "

            For i As Integer = 0 To n - 1
                Sql += _csv.get_column_str(i) & ","
            Next

            Sql = Sql.Remove(Sql.Length - 1)
            Sql += " FROM t10_cymnhd t10 "
            Sql += " LEFT JOIN t11_cymndt t11 "
            Sql += " ON t10.会社コード = t11.会社コード "
            Sql += " AND t10.受注番号 = t11.受注番号 "
            Sql += " AND t10.受注番号枝番 = t11.受注番号枝番 "
            Sql += " WHERE t10.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND t10.取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'"
            Sql += " AND t10.受注日 >= '" & UtilClass.strFormatDate(DtpDateSince.Text) & "'"
            Sql += " AND t10.受注日 <= '" & UtilClass.strFormatDate(DtpDateUntil.Text) & "'"
            Sql += " ORDER BY t10.受注日, t10.受注番号, t10.受注番号枝番 "

        ElseIf RbtnSales.Checked Then

            '売上選択時=Invoice
            Dim n As Integer = _csv.load("INVOICED")
            Sql = "SELECT "

            For i As Integer = 0 To n - 1
                Sql += _csv.get_column_str(i) & ","
            Next
            Sql = Sql.Remove(Sql.Length - 1)
            Sql += " FROM t23_skyuhd t23, v_t31_urigdt_2 t31 "
            Sql += " WHERE t23.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND t23.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED & ""
            Sql += " AND t23.請求日 >= '" & UtilClass.strFormatDate(DtpDateSince.Text) & "'"
            Sql += " AND t23.請求日 <= '" & UtilClass.strFormatDate(DtpDateUntil.Text) & "'"
            Sql += " AND t23.会社コード = t31.会社コード and t23.請求番号=t31.入金番号"
            Sql += " ORDER BY t23.請求日, t23.請求番号"
        ElseIf RbtnDelivered.Checked Then
            Dim n As Integer = _csv.load("DELIVERED")
            Sql = "SELECT "

            For i As Integer = 0 To n - 1
                Sql += _csv.get_column_str(i) & ","
            Next
            Sql = Sql.Remove(Sql.Length - 1)
            Sql += " FROM t30_urighd t30 "
            Sql += " LEFT JOIN t31_urigdt t31 "
            Sql += " ON t30.会社コード = t31.会社コード "
            Sql += " AND t30.売上番号 = t31.売上番号 "
            Sql += " AND t30.売上番号枝番 = t31.売上番号枝番 "
            Sql += " WHERE t30.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND t30.取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'"
            Sql += " AND t30.売上日 >= '" & UtilClass.strFormatDate(DtpDateSince.Text) & "'"
            Sql += " AND t30.売上日 <= '" & UtilClass.strFormatDate(DtpDateUntil.Text) & "'"
            Sql += " ORDER BY t30.得意先名, t30.売上日, t30.売上番号, t30.売上番号枝番 "
        End If

        Try
            ds = _db.selectDB(Sql, RS, reccnt)

            For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                If i = 0 Then
                    '列名
                    For y As Integer = 0 To ds.Tables(RS).Columns.Count - 1
                        Dim field As String = ds.Tables(RS).Columns(y).ColumnName
                        DgvList.Columns.Add(field, field)

                        '英語処理入れる
                    Next
                End If

                DgvList.Rows.Add()

                '基本
                For y As Integer = 0 To ds.Tables(RS).Columns.Count - 1
                    Dim fieldName As String = ds.Tables(RS).Columns(y).ColumnName

                    DgvList.Rows(i).Cells(fieldName).Value = ds.Tables(RS).Rows(i)(fieldName)

                Next
            Next

            '見出しの英訳・テキスト位置設定
            If RbtnQuotation.Checked Then
                quantityHd()
            ElseIf RbtnJobOrder.Checked Then
                JobOrderHd_new()
            ElseIf RbtnDelivered.Checked Then
                'DeliveryHd()
                JobOrderHd_new()
            Else
                'SalesHd()
                JobOrderHd_new()
            End If

            DgvList.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells) 'すべての列の幅を自動調整する

            'カーソルをビジー状態から元に戻す
            Cursor.Current = Cursors.Default

        Catch ue As UsrDefException
            ue.dspMsg()
            'Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            'Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    Private Sub quantityHd()
        If DgvList.Columns.Count = 0 Then
            Exit Sub
        End If

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            DgvList.Columns("会社コード").HeaderText = "CompanyCode"
            DgvList.Columns("見積番号").HeaderText = "QuotationNumber"
            DgvList.Columns("見積番号枝番").HeaderText = "QuotationSubNumber"
            DgvList.Columns("行番号").HeaderText = "LineNumber"
            DgvList.Columns("得意先コード").HeaderText = "CustomerCode"
            DgvList.Columns("得意先名").HeaderText = "CustomerName"
            DgvList.Columns("得意先担当者役職").HeaderText = "PositionPICCustomer"
            DgvList.Columns("得意先担当者名").HeaderText = "NameOfPIC"
            DgvList.Columns("得意先郵便番号").HeaderText = "CustomerPostalCode"
            DgvList.Columns("得意先住所").HeaderText = "CustomerAddress"
            DgvList.Columns("得意先電話番号").HeaderText = "CustomerPhoneNumber"
            DgvList.Columns("得意先ＦＡＸ").HeaderText = "CustomerFAX"
            DgvList.Columns("支払条件").HeaderText = "PaymentTermsAndConditon"
            DgvList.Columns("見積日").HeaderText = "QuotationDate"
            DgvList.Columns("見積有効期限").HeaderText = "QuotationExpirationDate"
            DgvList.Columns("営業担当者コード").HeaderText = "SalesPersonInChargeCode"
            DgvList.Columns("営業担当者").HeaderText = "SalesPersonInCharge"
            DgvList.Columns("入力担当者コード").HeaderText = "PICForInputtingCode"
            DgvList.Columns("入力担当者").HeaderText = "PICForInputting"
            DgvList.Columns("備考").HeaderText = "Remarks"
            DgvList.Columns("仕入金額").HeaderText = "PurchaseAmount"
            DgvList.Columns("ＶＡＴ").HeaderText = "ＶＡＴ"
            DgvList.Columns("見積金額").HeaderText = "QuotationAmount"
            DgvList.Columns("粗利額").HeaderText = "GrossMargin"
            DgvList.Columns("仕入区分").HeaderText = "PurchasingClassification"
            DgvList.Columns("仕入先コード").HeaderText = "SupplierCode"
            DgvList.Columns("仕入先名称").HeaderText = "SupplierName"
            DgvList.Columns("メーカー").HeaderText = "Manufacturer"
            DgvList.Columns("品名").HeaderText = "ItemName"
            DgvList.Columns("型式").HeaderText = "Spec"
            DgvList.Columns("数量").HeaderText = "Quantity"
            DgvList.Columns("単位").HeaderText = "Unit"
            DgvList.Columns("仕入単価").HeaderText = "PurchaseUnitPrice"
            DgvList.Columns("明細仕入金額").HeaderText = "DetailsPurchaseAmount"
            DgvList.Columns("売単価").HeaderText = "SellingPrice"
            DgvList.Columns("売上金額").HeaderText = "SalesAmount"
            DgvList.Columns("間接費").HeaderText = "Overhead"
            DgvList.Columns("更新者").HeaderText = "ModifiedBy"
            DgvList.Columns("登録日").HeaderText = "RegistrationDate"
            DgvList.Columns("粗利率").HeaderText = "GrossMarginRate"
            DgvList.Columns("明細粗利額").HeaderText = "DetailsGrossMargin"
            DgvList.Columns("間接費率").HeaderText = "OverheadRate"
            DgvList.Columns("間接費無仕入金額").HeaderText = "OverheadNoPurchasePrice"
            DgvList.Columns("仕入原価").HeaderText = "PurchasingCost"
            DgvList.Columns("関税率").HeaderText = "CustomsDutyRate"
            DgvList.Columns("関税額").HeaderText = "CustomsDuty"
            DgvList.Columns("前払法人税率").HeaderText = "PrepaidCorporateTaxRate"
            DgvList.Columns("前払法人税額").HeaderText = "PrepaidCorporateTaxAmount"
            DgvList.Columns("輸送費率").HeaderText = "TransportationCostRate"
            DgvList.Columns("輸送費額").HeaderText = "TransportationCost"
            DgvList.Columns("見積単価").HeaderText = "QuotationUnitPrice"
            DgvList.Columns("明細見積金額").HeaderText = "DetailsQuotationAmount"
            DgvList.Columns("リードタイム").HeaderText = "LeadTime"
            DgvList.Columns("リードタイム単位").HeaderText = "LeadTimUnit"
            DgvList.Columns("明細備考").HeaderText = "DetailsRemarks"
            DgvList.Columns("取消日").HeaderText = "CancelDate"
            DgvList.Columns("取消区分").HeaderText = "CancelClassification"
            DgvList.Columns("受注日").HeaderText = "JobOrderDate"
            DgvList.Columns("登録日").HeaderText = "RegistrationDate"
            DgvList.Columns("更新日").HeaderText = "UpdateDate"
            DgvList.Columns("更新者").HeaderText = "ModifiedBy"

        End If

        DgvList.Columns("見積金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("粗利額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("ＶＡＴ").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DgvList.Columns("仕入原価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("仕入単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("売単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("売上金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("間接費").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("粗利額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("粗利率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("間接費率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("間接費無仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("関税率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("関税額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("前払法人税率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("前払法人税額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("輸送費率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("輸送費額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("リードタイム").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("見積単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("見積金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("明細粗利額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("明細仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("明細見積金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    End Sub

    Private Sub JobOrderHd_del()
        If DgvList.Columns.Count = 0 Then
            Exit Sub
        End If

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            DgvList.Columns("会社コード").HeaderText = "CompanyCode"
            DgvList.Columns("受注番号").HeaderText = "JobOrderNumber"
            DgvList.Columns("受注番号枝番").HeaderText = "JobOrderSubNumber"
            DgvList.Columns("見積番号").HeaderText = "QuotationNumber"
            DgvList.Columns("見積番号枝番").HeaderText = "QuotationSubNumber"
            DgvList.Columns("行番号").HeaderText = "LineNumber"
            DgvList.Columns("得意先コード").HeaderText = "CustomerCode"
            DgvList.Columns("得意先名").HeaderText = "CustomerName"
            DgvList.Columns("得意先担当者役職").HeaderText = "PositionPICCustomer"
            DgvList.Columns("得意先担当者名").HeaderText = "NameOfPIC"
            DgvList.Columns("得意先郵便番号").HeaderText = "CustomerPostalCode"
            DgvList.Columns("得意先住所").HeaderText = "CustomerAddress"
            DgvList.Columns("得意先電話番号").HeaderText = "CustomerPhoneNumber"
            DgvList.Columns("得意先ＦＡＸ").HeaderText = "CustomerFAX"
            DgvList.Columns("支払条件").HeaderText = "PaymentTermsAndConditon"
            DgvList.Columns("見積日").HeaderText = "QuotationDate"
            DgvList.Columns("見積有効期限").HeaderText = "QuotationExpirationDate"
            DgvList.Columns("営業担当者コード").HeaderText = "SalesPersonInChargeCode"
            DgvList.Columns("営業担当者").HeaderText = "SalesPersonInCharge"
            DgvList.Columns("入力担当者コード").HeaderText = "PICForInputtingCode"
            DgvList.Columns("入力担当者").HeaderText = "PICForInputting"
            DgvList.Columns("見積備考").HeaderText = "QuotationRemarks"
            DgvList.Columns("備考").HeaderText = "Remarks"
            DgvList.Columns("仕入金額").HeaderText = "PurchaseAmount"
            DgvList.Columns("ＶＡＴ").HeaderText = "ＶＡＴ"
            DgvList.Columns("見積金額").HeaderText = "QuotationAmount"
            DgvList.Columns("粗利額").HeaderText = "GrossMargin"
            DgvList.Columns("仕入区分").HeaderText = "PurchasingClassification"
            DgvList.Columns("客先番号").HeaderText = "PO"
            DgvList.Columns("仕入先名").HeaderText = "SupplierName"
            DgvList.Columns("メーカー").HeaderText = "Manufacturer"
            DgvList.Columns("品名").HeaderText = "ItemName"
            DgvList.Columns("型式").HeaderText = "Spec"
            DgvList.Columns("受注数量").HeaderText = "JobOrderQuantity"
            DgvList.Columns("売上数量").HeaderText = "SalesQuantity"
            DgvList.Columns("受注残数").HeaderText = "OrderRemainingAmount"
            DgvList.Columns("出庫数").HeaderText = "GoodsDeliveryQuantity"
            DgvList.Columns("未出庫数").HeaderText = "GoodsDeliveryRemainingQuantity"
            DgvList.Columns("単位").HeaderText = "Unit"
            DgvList.Columns("仕入値").HeaderText = "PurchaseAmount"
            DgvList.Columns("明細仕入金額").HeaderText = "DetailsPurchaseAmount"
            DgvList.Columns("売単価").HeaderText = "SellingPrice"
            DgvList.Columns("売上金額").HeaderText = "SalesAmount"
            DgvList.Columns("間接費").HeaderText = "Overhead"
            DgvList.Columns("更新者").HeaderText = "ModifiedBy"
            DgvList.Columns("登録日").HeaderText = "RegistrationDate"
            DgvList.Columns("粗利率").HeaderText = "GrossMarginRate"
            DgvList.Columns("粗利額").HeaderText = "GrossMargin"
            DgvList.Columns("明細粗利額").HeaderText = "DetailsGrossMargin"
            DgvList.Columns("仕入原価").HeaderText = "PurchasingCost"
            DgvList.Columns("関税率").HeaderText = "CustomsDutyRate"
            DgvList.Columns("関税額").HeaderText = "CustomsDuty"
            DgvList.Columns("前払法人税率").HeaderText = "PrepaidCorporateTaxRate"
            DgvList.Columns("前払法人税額").HeaderText = "PrepaidCorporateTaxAmount"
            DgvList.Columns("輸送費率").HeaderText = "TransportationCostRate"
            DgvList.Columns("輸送費額").HeaderText = "TransportationCost"
            DgvList.Columns("見積単価").HeaderText = "QuotationUnitPrice"
            DgvList.Columns("明細見積金額").HeaderText = "DetailsQuotationAmount"
            DgvList.Columns("リードタイム").HeaderText = "LeadTime"
            DgvList.Columns("リードタイム単位").HeaderText = "LeadTimUnit"
            DgvList.Columns("明細備考").HeaderText = "DetailsRemarks"
            DgvList.Columns("取消日").HeaderText = "CancelDate"
            DgvList.Columns("取消区分").HeaderText = "CancelClassification"
            DgvList.Columns("受注日").HeaderText = "JobOrderDate"
            DgvList.Columns("登録日").HeaderText = "RegistrationDate"
            DgvList.Columns("更新日").HeaderText = "UpdateDate"
            DgvList.Columns("更新者").HeaderText = "ModifiedBy"

        End If

        DgvList.Columns("見積金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("粗利額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("ＶＡＴ").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("ＰＰＨ").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DgvList.Columns("仕入原価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("仕入値").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("受注数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("売上数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("受注残数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("出庫数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("未出庫数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("売単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("売上金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("間接費").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("粗利額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("粗利率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("関税率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("関税額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("前払法人税率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("前払法人税額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("輸送費率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("輸送費額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("リードタイム").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("見積単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("見積金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("明細粗利額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("明細仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("明細見積金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    End Sub

    Private Sub SalesHd_del()
        If DgvList.Columns.Count = 0 Then
            Exit Sub
        End If

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            DgvList.Columns("会社コード").HeaderText = "CompanyCode"
            DgvList.Columns("請求番号").HeaderText = "InvoiceNo"
            'DgvList.Columns("売上番号枝番").HeaderText = "SalesSubNumber"
            DgvList.Columns("受注番号").HeaderText = "JobOrderNumber"
            DgvList.Columns("受注番号枝番").HeaderText = "JobOrderVer"
            'DgvList.Columns("見積番号").HeaderText = "QuotationNumber"
            'DgvList.Columns("見積番号枝番").HeaderText = "QuotationSubNumber"
            'DgvList.Columns("行番号").HeaderText = "LineNumber"
            DgvList.Columns("得意先コード").HeaderText = "CustomerCode"
            DgvList.Columns("得意先名").HeaderText = "CustomerName"
            'DgvList.Columns("得意先担当者役職").HeaderText = "PositionPICCustomer"
            'DgvList.Columns("得意先担当者名").HeaderText = "NameOfPIC"
            'DgvList.Columns("得意先郵便番号").HeaderText = "CustomerPostalCode"
            'DgvList.Columns("得意先住所").HeaderText = "CustomerAddress"
            'DgvList.Columns("得意先電話番号").HeaderText = "CustomerPhoneNumber"
            'DgvList.Columns("得意先ＦＡＸ").HeaderText = "CustomerFAX"
            'DgvList.Columns("支払条件").HeaderText = "PaymentTermsAndConditon"
            'DgvList.Columns("見積有効期限").HeaderText = "QuotationExpirationDate"
            'DgvList.Columns("営業担当者コード").HeaderText = "SalesPersonInChargeCode"
            'DgvList.Columns("営業担当者").HeaderText = "SalesPersonInCharge"
            'DgvList.Columns("入力担当者コード").HeaderText = "PICForInputtingCode"
            'DgvList.Columns("入力担当者").HeaderText = "PICForInputting"
            'DgvList.Columns("見積備考").HeaderText = "QuotationRemarks"
            DgvList.Columns("備考1").HeaderText = "Remarks1"
            'DgvList.Columns("仕入金額").HeaderText = "PurchaseAmount"
            'DgvList.Columns("ＶＡＴ").HeaderText = "ＶＡＴ"
            'DgvList.Columns("見積金額").HeaderText = "QuotationAmount"
            'DgvList.Columns("売上金額").HeaderText = "SalesAmount"
            'DgvList.Columns("粗利額").HeaderText = "GrossMargin"
            DgvList.Columns("請求区分").HeaderText = "InvoiceClassification"
            DgvList.Columns("締処理日").HeaderText = "ClosingDate"
            DgvList.Columns("客先番号").HeaderText = "PO"
            'DgvList.Columns("仕入先名").HeaderText = "SupplierName"
            'DgvList.Columns("メーカー").HeaderText = "Manufacturer"
            'DgvList.Columns("品名").HeaderText = "ItemName"
            'DgvList.Columns("型式").HeaderText = "Spec"
            'DgvList.Columns("受注数量").HeaderText = "JobOrderQuantity"
            'DgvList.Columns("売上数量").HeaderText = "SalesQuantity"
            'DgvList.Columns("受注残数").HeaderText = "OrderRemainingAmount"
            'DgvList.Columns("出庫数").HeaderText = "GoodsDeliveryQuantity"
            'DgvList.Columns("未出庫数").HeaderText = "GoodsDeliveryRemainingQuantity"
            'DgvList.Columns("単位").HeaderText = "Unit"
            'DgvList.Columns("仕入値").HeaderText = "PurchaseAmount"
            'DgvList.Columns("明細仕入金額").HeaderText = "DetailsPurchaseAmount"
            'DgvList.Columns("売単価").HeaderText = "SellingPrice"
            'DgvList.Columns("間接費").HeaderText = "Overhead"
            DgvList.Columns("更新者").HeaderText = "ModifiedBy"
            DgvList.Columns("登録日").HeaderText = "RegistrationDate"
            'DgvList.Columns("粗利率").HeaderText = "GrossMarginRate"
            'DgvList.Columns("粗利額").HeaderText = "GrossMargin"
            'DgvList.Columns("明細売上金額").HeaderText = "DetailsSalesAmount"
            'DgvList.Columns("明細粗利額").HeaderText = "DetailsGrossMargin"
            'DgvList.Columns("仕入原価").HeaderText = "PurchasingCost"
            'DgvList.Columns("関税率").HeaderText = "CustomsDutyRate"
            'DgvList.Columns("関税額").HeaderText = "CustomsDuty"
            'DgvList.Columns("前払法人税率").HeaderText = "PrepaidCorporateTaxRate"
            'DgvList.Columns("前払法人税額").HeaderText = "PrepaidCorporateTaxAmount"
            'DgvList.Columns("輸送費率").HeaderText = "TransportationCostRate"
            'DgvList.Columns("輸送費額").HeaderText = "TransportationCost"
            'DgvList.Columns("見積単価").HeaderText = "QuotationUnitPrice"
            'DgvList.Columns("明細見積金額").HeaderText = "DetailsQuotationAmount"
            'DgvList.Columns("リードタイム").HeaderText = "LeadTime"
            'DgvList.Columns("リードタイム単位").HeaderText = "LeadTimUnit"
            'DgvList.Columns("入金有無").HeaderText = "PaymentConfirmation"
            DgvList.Columns("入金番号").HeaderText = "MoneyReceiptNumber"
            DgvList.Columns("備考2").HeaderText = "Remarks2"
            DgvList.Columns("取消日").HeaderText = "CancelDate"
            DgvList.Columns("取消区分").HeaderText = "CancelClassification"
            DgvList.Columns("請求日").HeaderText = "InvoiceDate"
            'DgvList.Columns("受注日").HeaderText = "JobOrderDate"
            'DgvList.Columns("売上日").HeaderText = "SalesDate"
            DgvList.Columns("入金予定日").HeaderText = "DepositDate"
            DgvList.Columns("入金完了日").HeaderText = "MoneyReceiptDate"
            'DgvList.Columns("登録日").HeaderText = "RegistrationDate"
            DgvList.Columns("更新日").HeaderText = "UpdateDate"
            'DgvList.Columns("更新者").HeaderText = "ModifiedBy"
            DgvList.Columns("通貨").HeaderText = "Currency"
            DgvList.Columns("レート").HeaderText = "Rate"
            DgvList.Columns("請求金額計_外貨").HeaderText = "TotalBillingAmount"
            DgvList.Columns("売掛残高_外貨").HeaderText = "BillingBalance"
            DgvList.Columns("請求消費税計").HeaderText = "VAT-OUT"

        End If

        DgvList.Columns("請求金額計_外貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("売掛残高_外貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("請求消費税計").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("レート").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("ＰＰＨ").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        'DgvList.Columns("仕入原価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("仕入値").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("受注数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("売上数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("受注残数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        'DgvList.Columns("売単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("間接費").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("粗利額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("粗利率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("関税率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("関税額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("前払法人税率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("前払法人税額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("輸送費率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("輸送費額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("リードタイム").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("見積単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("見積金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("明細売上金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("明細粗利額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("明細仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("明細見積金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    End Sub

    Private Sub DeliveryHd_del()
        If DgvList.Columns.Count = 0 Then
            Exit Sub
        End If

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            DgvList.Columns("会社コード").HeaderText = "CompanyCode"
            DgvList.Columns("売上番号").HeaderText = "SalesNumber"
            DgvList.Columns("売上番号枝番").HeaderText = "SalesSubNumber"
            DgvList.Columns("受注番号").HeaderText = "QuotationNumber"
            DgvList.Columns("受注番号枝番").HeaderText = "QuotationSubNumber"
            DgvList.Columns("見積番号").HeaderText = "QuotationNumber"
            DgvList.Columns("見積番号枝番").HeaderText = "QuotationSubNumber"
            DgvList.Columns("行番号").HeaderText = "LineNumber"
            DgvList.Columns("得意先コード").HeaderText = "CustomerCode"
            DgvList.Columns("得意先名").HeaderText = "CustomerName"
            DgvList.Columns("得意先担当者役職").HeaderText = "PositionPICCustomer"
            DgvList.Columns("得意先担当者名").HeaderText = "NameOfPIC"
            DgvList.Columns("得意先郵便番号").HeaderText = "CustomerPostalCode"
            DgvList.Columns("得意先住所").HeaderText = "CustomerAddress"
            DgvList.Columns("得意先電話番号").HeaderText = "CustomerPhoneNumber"
            DgvList.Columns("得意先ＦＡＸ").HeaderText = "CustomerFAX"
            DgvList.Columns("支払条件").HeaderText = "PaymentTermsAndConditon"
            DgvList.Columns("見積有効期限").HeaderText = "QuotationExpirationDate"
            DgvList.Columns("営業担当者コード").HeaderText = "SalesPersonInChargeCode"
            DgvList.Columns("営業担当者").HeaderText = "SalesPersonInCharge"
            DgvList.Columns("入力担当者コード").HeaderText = "PICForInputtingCode"
            DgvList.Columns("入力担当者").HeaderText = "PICForInputting"
            'DgvList.Columns("見積備考").HeaderText = "QuotationRemarks"
            DgvList.Columns("備考").HeaderText = "Remarks"
            DgvList.Columns("仕入金額").HeaderText = "PurchaseAmount"
            DgvList.Columns("ＶＡＴ").HeaderText = "ＶＡＴ"
            DgvList.Columns("見積金額").HeaderText = "QuotationAmount"
            DgvList.Columns("売上金額").HeaderText = "SalesAmount"
            DgvList.Columns("粗利額").HeaderText = "GrossMargin"
            DgvList.Columns("仕入区分").HeaderText = "PurchasingClassification"
            DgvList.Columns("締処理日").HeaderText = "ClosingDate"
            DgvList.Columns("客先番号").HeaderText = "PO"
            DgvList.Columns("仕入先名").HeaderText = "SupplierName"
            DgvList.Columns("メーカー").HeaderText = "Manufacturer"
            DgvList.Columns("品名").HeaderText = "ItemName"
            DgvList.Columns("型式").HeaderText = "Spec"
            DgvList.Columns("受注数量").HeaderText = "JobOrderQuantity"
            DgvList.Columns("売上数量").HeaderText = "SalesQuantity"
            DgvList.Columns("受注残数").HeaderText = "OrderRemainingAmount"
            'DgvList.Columns("出庫数").HeaderText = "GoodsDeliveryQuantity"
            'DgvList.Columns("未出庫数").HeaderText = "GoodsDeliveryRemainingQuantity"
            DgvList.Columns("単位").HeaderText = "Unit"
            DgvList.Columns("仕入値").HeaderText = "PurchaseAmount"
            DgvList.Columns("明細仕入金額").HeaderText = "DetailsPurchaseAmount"
            DgvList.Columns("売単価").HeaderText = "SellingPrice"
            DgvList.Columns("間接費").HeaderText = "Overhead"
            DgvList.Columns("更新者").HeaderText = "ModifiedBy"
            DgvList.Columns("登録日").HeaderText = "RegistrationDate"
            DgvList.Columns("粗利率").HeaderText = "GrossMarginRate"
            DgvList.Columns("粗利額").HeaderText = "GrossMargin"
            DgvList.Columns("明細売上金額").HeaderText = "DetailsSalesAmount"
            DgvList.Columns("明細粗利額").HeaderText = "DetailsGrossMargin"
            DgvList.Columns("仕入原価").HeaderText = "PurchasingCost"
            DgvList.Columns("関税率").HeaderText = "CustomsDutyRate"
            DgvList.Columns("関税額").HeaderText = "CustomsDuty"
            DgvList.Columns("前払法人税率").HeaderText = "PrepaidCorporateTaxRate"
            DgvList.Columns("前払法人税額").HeaderText = "PrepaidCorporateTaxAmount"
            DgvList.Columns("輸送費率").HeaderText = "TransportationCostRate"
            DgvList.Columns("輸送費額").HeaderText = "TransportationCost"
            DgvList.Columns("見積単価").HeaderText = "QuotationUnitPrice"
            DgvList.Columns("明細見積金額").HeaderText = "DetailsQuotationAmount"
            DgvList.Columns("リードタイム").HeaderText = "LeadTime"
            'DgvList.Columns("リードタイム単位").HeaderText = "LeadTimUnit"
            DgvList.Columns("入金有無").HeaderText = "PaymentConfirmation"
            DgvList.Columns("入金番号").HeaderText = "MoneyReceiptNumber"
            DgvList.Columns("明細備考").HeaderText = "DetailsRemarks"
            DgvList.Columns("取消日").HeaderText = "CancelDate"
            DgvList.Columns("取消区分").HeaderText = "CancelClassification"
            DgvList.Columns("見積日").HeaderText = "QuotationDate"
            DgvList.Columns("受注日").HeaderText = "JobOrderDate"
            DgvList.Columns("売上日").HeaderText = "SalesDate"
            DgvList.Columns("入金予定日").HeaderText = "DepositDate"
            DgvList.Columns("入金日").HeaderText = "MoneyReceiptDate"
            DgvList.Columns("登録日").HeaderText = "RegistrationDate"
            DgvList.Columns("更新日").HeaderText = "UpdateDate"
            DgvList.Columns("更新者").HeaderText = "ModifiedBy"

        End If

        DgvList.Columns("見積金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("売上金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("粗利額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("ＶＡＴ").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("ＰＰＨ").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DgvList.Columns("仕入原価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("仕入値").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("受注数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("売上数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("受注残数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DgvList.Columns("売単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("間接費").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("粗利額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("粗利率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("関税率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("関税額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("前払法人税率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("前払法人税額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("輸送費率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("輸送費額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("リードタイム").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("見積単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("見積金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("明細売上金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("明細粗利額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("明細仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("明細見積金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    End Sub

    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    Private Sub BtnCSVOutput_Click(sender As Object, e As EventArgs) Handles BtnCSVOutput.Click

        '対象データがない場合は取消操作不可能
        If DgvList.Rows.Count = 0 Then
            '該当データがないアラートを出す
            _msgHd.dspMSG("noTargetData", frmC01F10_Login.loginValue.Language)
            Return
        Else

            'カーソルをビジー状態にする
            Cursor.Current = Cursors.WaitCursor

            'Excel出力処理
            outputCSV()

            'カーソルをビジー状態から元に戻す
            Cursor.Current = Cursors.Default

            '完了アラート
            _msgHd.dspMSG("CreateCSV", frmC01F10_Login.loginValue.Language)

        End If

    End Sub

    'CSV出力処理
    Private Sub outputCSV()


        Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("UTF-8")
        Dim sp As Char = _csv.getDelimiter()
        '出力先パス
        Dim sOutPath As String = ""
        sOutPath = StartUp._iniVal.OutXlsPath

        '出力ファイル名
        Dim sOutFile As String = ""
        sOutFile = sOutPath & "\" & Name & "_" & DateTime.Now.ToString("yyyyMMddHHmm") & ".csv"

        '書き込むファイルを開く
        Dim sr As New System.IO.StreamWriter(sOutFile, False, enc)

        For i As Integer = 0 To DgvList.Rows.Count - 1
            If i = 0 Then
                '基本列名
                For y As Integer = 0 To DgvList.Columns.Count - 1
                    Dim field As String = DgvList.Columns(y).HeaderText
                    field = EncloseDoubleQuotesIfNeed(field)
                    sr.Write(field)
                    If y < DgvList.Columns.Count - 1 Then
                        sr.Write(sp)
                    End If
                Next
                sr.Write(vbCrLf)
            End If

            '基本
            For y As Integer = 0 To DgvList.Columns.Count - 1
                Dim fieldName As String = DgvList.Columns(y).Name
                Dim field As String = ""

                If _csv.get_(y, "FORMAT") Is Nothing Then
                    field = DgvList.Rows(i).Cells(fieldName).Value.ToString()
                Else
                    If _csv.get_(y, "FORMAT").Equals("d") Then
                        field = DgvList.Rows(i).Cells(fieldName).Value.ToShortDateString()
                    Else
                        field = DgvList.Rows(i).Cells(fieldName).Value.ToString()
                    End If
                End If
                field = EncloseDoubleQuotesIfNeed(field)
                sr.Write(field)
                If y < DgvList.Columns.Count - 1 Then
                    sr.Write(sp)
                End If
            Next
            sr.Write(vbCrLf)
        Next

        sr.Close()

    End Sub

    Private Function EncloseDoubleQuotes(field As String) As String
        If field.IndexOf(""""c) > -1 Then
            '"を""とする
            field = field.Replace("""", """""")
        End If
        Return """" & field & """"
    End Function

    Private Function EncloseDoubleQuotesIfNeed(field As String) As String
        If NeedEncloseDoubleQuotes(field) Then
            Return EncloseDoubleQuotes(field)
        End If
        Return field
    End Function

    Private Function NeedEncloseDoubleQuotes(field As String) As Boolean
        Return field.IndexOf(""""c) > -1 OrElse
        field.IndexOf(","c) > -1 OrElse
        field.IndexOf(ControlChars.Cr) > -1 OrElse
        field.IndexOf(ControlChars.Lf) > -1 OrElse
        field.StartsWith(" ") OrElse
        field.StartsWith(vbTab) OrElse
        field.EndsWith(" ") OrElse
        field.EndsWith(vbTab)
    End Function

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        getList()
    End Sub

    Private Sub JobOrderHd_new()
        If DgvList.Columns.Count = 0 Then
            Exit Sub
        End If

        For i As Integer = 0 To _csv.count - 1
            DgvList.Columns(_csv.get_(i, "DBCOL")).HeaderText = _csv.get_hd_text_str(i, frmC01F10_Login.loginValue.Language)
            If _csv.get_(i, "ALIGNMENT") Is Nothing Then
            Else
                DgvList.Columns(_csv.get_(i, "DBCOL")).DefaultCellStyle.Alignment = _csv.get_(i, "ALIGNMENT")
            End If
            If _csv.get_(i, "FORMAT") Is Nothing Then
            Else
                DgvList.Columns(_csv.get_(i, "DBCOL")).DefaultCellStyle.Format = _csv.get_(i, "FORMAT")
            End If
        Next

    End Sub

End Class