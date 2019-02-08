Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB


Public Class QuoteList
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
    Private _status As String = ""
    'Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ
    Private CompanyCode As String = ""
    Private QuoteNo As String()
    Private dtToday As DateTime = DateTime.Now

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
                   ByRef prmRefStatus As String)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLangHd
        _parentForm = prmRefForm
        _status = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True
        DgvMithd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells
    End Sub

    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If _status = "EDIT" Then
            If frmC01F10_Login.loginValue.Language = "ENG" Then
                LblMode.Text = "EditMode"
            Else
                LblMode.Text = "編集モード"
            End If

            BtnQuoteEdit.Visible = True
            BtnQuoteEdit.Location = New Point(997, 509)
        ElseIf _status = "CLONE" Then
            If frmC01F10_Login.loginValue.Language = "ENG" Then
                LblMode.Text = "CopyMode"
            Else
                LblMode.Text = "複写モード"
            End If

            BtnQuoteClone.Visible = True
            BtnQuoteClone.Location = New Point(997, 509)
        ElseIf _status = "CANCEL" Then
            If frmC01F10_Login.loginValue.Language = "ENG" Then
                LblMode.Text = "CancelMode"
            Else
                LblMode.Text = "取消モード"
            End If

            BtnCancel.Visible = True
            BtnCancel.Location = New Point(997, 509)
        ElseIf _status = "VIEW" Then
            If frmC01F10_Login.loginValue.Language = "ENG" Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If

            BtnQuoteView.Visible = True
            BtnQuoteView.Location = New Point(997, 509)
        ElseIf _status = "PRICE" Then
            If frmC01F10_Login.loginValue.Language = "ENG" Then
                LblMode.Text = "PurchasePriceInputMode"
            Else
                LblMode.Text = "仕入単価入力モード"
            End If

            BtnUnitPrice.Visible = True
            BtnUnitPrice.Location = New Point(997, 509)
        ElseIf _status = "ORDER_NEW" Then
            If frmC01F10_Login.loginValue.Language = "ENG" Then
                LblMode.Text = "NewOrderRegistrationMode"
            Else
                LblMode.Text = "受注新規入力モード"
            End If

            BtnOrder.Visible = True
            BtnOrder.Location = New Point(997, 509)
        ElseIf _status = "PURCHASE_NEW" Then
            If frmC01F10_Login.loginValue.Language = "ENG" Then
                LblMode.Text = "NewPurchaseRegistrationMode"
            Else
                LblMode.Text = "仕入新規入力モード"
            End If

            BtnPurchase.Visible = True
            BtnPurchase.Location = New Point(997, 509)
        ElseIf _status = "ORDER_PURCHASE" Then
            If frmC01F10_Login.loginValue.Language = "ENG" Then
                LblMode.Text = "JobOrderingAndPurchasingMode"
            Else
                LblMode.Text = "受発注登録モード"
            End If

            BtnOrderPurchase.Visible = True
            BtnOrderPurchase.Location = New Point(997, 509)
        End If

        QuoteListLoad()

        If frmC01F10_Login.loginValue.Language = "ENG" Then
            LblConditions.Text = "TermsOfSelection"
            Label1.Text = "CustomerName"
            Label2.Text = "Address"
            Label3.Text = "PhoneNumber"
            Label4.Text = "CustomerCode"
            Label8.Text = "QuotationDate"
            Label7.Text = "QuotationNumber"
            Label6.Text = "SalesPersonInCharge"
            Label10.Text = "DisplayFormat"

            RbtnSlip.Text = "UnitOfVoucher" '伝票単位
            RbtnDetails.Text = "UnitOfDetailData" '明細単位
            RbtnDetails.Location = New Point(166, 202)

            ChkExpired.Text = "IncludeExpriedData" '有効期限の切れたデータを含める
            ChkExpired.Location = New Point(329, 203)
            ChkCancel.Text = "IncludeCancelData"
            ChkCancel.Location = New Point(556, 203)

            BtnQuoteSearch.Text = "Search"
            BtnOrderPurchase.Text = "Ordering"
            BtnQuoteAdd.Text = "QuotationRegistration"
            BtnUnitPrice.Text = "PurchasePriceInput"
            BtnQuoteEdit.Text = "QuotationEdit"
            BtnQuoteClone.Text = "QuotationCopy"
            BtnQuoteView.Text = "QuotationView"
            BtnCancel.Text = "QuotationCancel"
            BtnBack.Text = "Back"
        End If

        '見積日の範囲指定を初期設定
        TxtQuoteDate1.Value = DateAdd("d", -14, Now())
        TxtQuoteDate2.Value = Now()
        'グリッドの初期表示
        QuoteListLoad()

    End Sub

    Private Sub QuoteListLoad()
        Try
            'グリッド初期化
            RbtnSlip.Checked = True
            DgvMithd.Rows.Clear()
            DgvMithd.Columns.Clear()
            Dim Sql As String = ""
            Dim strWhere As String = ""     'Where句

            'データロード
            Sql += "SELECT * FROM public.t01_mithd "
            strWhere += "WHERE"
            strWhere += " 会社コード"
            strWhere += "='"
            strWhere += frmC01F10_Login.loginValue.BumonNM
            strWhere += "' "

            If TxtCustomerName.Text <> "" Then
                strWhere += " and "
                strWhere += "得意先名"
                strWhere += " ILIKE "
                strWhere += "'%"
                strWhere += TxtCustomerName.Text
                strWhere += "%'"
            End If
            If TxtAddress.Text <> "" Then
                strWhere += " and "
                strWhere += "得意先住所"
                strWhere += " ILIKE "
                strWhere += "'%"
                strWhere += TxtAddress.Text
                strWhere += "%'"
            End If
            If TxtTel.Text <> "" Then
                strWhere += " and "
                strWhere += "得意先電話番号"
                strWhere += " ILIKE "
                strWhere += "'%"
                strWhere += TxtTel.Text
                strWhere += "%'"
            End If
            If TxtCustomerCode.Text <> "" Then
                strWhere += " and "
                strWhere += "得意先コード"
                strWhere += " ILIKE "
                strWhere += "'%"
                strWhere += TxtCustomerCode.Text
                strWhere += "%'"
            End If
            If TxtQuoteDate1.Text <> "" Then
                strWhere += " and "
                strWhere += "見積日"
                strWhere += " >=  "
                strWhere += "'"
                strWhere += TxtQuoteDate1.Text
                strWhere += "'"
            End If
            If TxtQuoteDate2.Text <> "" Then
                strWhere += " and "
                strWhere += "見積日"
                strWhere += " <=  "
                strWhere += "'"
                strWhere += TxtQuoteDate2.Text
                strWhere += "'"
            End If
            If TxtQuoteNo1.Text <> "" Then
                strWhere += " and "
                strWhere += "見積番号"
                strWhere += " >=  "
                strWhere += "'"
                strWhere += TxtQuoteNo1.Text
                strWhere += "'"
            End If
            If TxtQuoteNo2.Text <> "" Then
                strWhere += " and "
                strWhere += "見積番号"
                strWhere += " <=  "
                strWhere += "'"
                strWhere += TxtQuoteNo2.Text
                strWhere += "'"
            End If
            If TxtSales.Text <> "" Then
                strWhere += " and "
                strWhere += "営業担当者"
                strWhere += " ILIKE "
                strWhere += "'%"
                strWhere += TxtSales.Text
                strWhere += "%'"
            End If
            If Not ChkExpired.Checked Then
                strWhere += " and "
                strWhere += "見積有効期限 >= '"
                strWhere += dtToday
                strWhere += "'"
            End If
            If Not ChkCancel.Checked Then
                strWhere += " and "
                strWhere += " 取消区分 = '0'"
            End If

            Sql += strWhere
            Sql += " ORDER BY 見積番号 DESC,見積番号枝番 DESC"

            Dim reccnt As Integer = 0
            ds = _db.selectDB(Sql, RS, reccnt)

            If frmC01F10_Login.loginValue.Language = "ENG" Then
                DgvMithd.Columns.Add("見積番号", "QuotationNumber")
                DgvMithd.Columns.Add("見積番号枝番", "BranchNumber")
                DgvMithd.Columns.Add("見積日", "QuotationDate")
                DgvMithd.Columns.Add("見積有効期限", "QuotationExpriedDate")
                DgvMithd.Columns.Add("得意先コード", "CustomerCode")
                DgvMithd.Columns.Add("得意先名", "CustomerName")
                DgvMithd.Columns.Add("得意先郵便番号", "PostalCode")
                DgvMithd.Columns.Add("得意先住所", "Address")
                DgvMithd.Columns.Add("得意先電話番号", "PhoneNumber")
                DgvMithd.Columns.Add("得意先ＦＡＸ", "FAX")
                DgvMithd.Columns.Add("見積金額", "QuotationAmount")
                DgvMithd.Columns.Add("仕入金額", "PurchaseAmount")
                DgvMithd.Columns.Add("ＶＡＴ", "VAT")
                DgvMithd.Columns.Add("粗利額", "GrossMargin")
                DgvMithd.Columns.Add("支払条件", "PeymentTerms")
                DgvMithd.Columns.Add("営業担当者", "SalesPersonInCharge")
                DgvMithd.Columns.Add("入力担当者", "PICForInputting")
                DgvMithd.Columns.Add("備考", "Remarks")
                DgvMithd.Columns.Add("登録日", "RegistrationDate")
                DgvMithd.Columns.Add("更新者", "ModifiedBy")
                DgvMithd.Columns.Add("更新日", "UpdateDate")
            Else
                DgvMithd.Columns.Add("見積番号", "見積番号")
                DgvMithd.Columns.Add("見積番号枝番", "見積番号枝番")
                DgvMithd.Columns.Add("見積日", "見積日")
                DgvMithd.Columns.Add("見積有効期限", "見積有効期限")
                DgvMithd.Columns.Add("得意先コード", "得意先コード")
                DgvMithd.Columns.Add("得意先名", "得意先名")
                DgvMithd.Columns.Add("得意先郵便番号", "得意先郵便番号")
                DgvMithd.Columns.Add("得意先住所", "得意先住所")
                DgvMithd.Columns.Add("得意先電話番号", "得意先電話番号")
                DgvMithd.Columns.Add("得意先ＦＡＸ", "得意先ＦＡＸ")
                DgvMithd.Columns.Add("見積金額", "見積金額")
                DgvMithd.Columns.Add("仕入金額", "仕入金額")
                DgvMithd.Columns.Add("ＶＡＴ", "ＶＡＴ")
                DgvMithd.Columns.Add("粗利額", "粗利額")
                DgvMithd.Columns.Add("支払条件", "支払条件")
                DgvMithd.Columns.Add("営業担当者", "営業担当者")
                DgvMithd.Columns.Add("入力担当者", "入力担当者")
                DgvMithd.Columns.Add("備考", "備考")
                DgvMithd.Columns.Add("登録日", "登録日")
                DgvMithd.Columns.Add("更新者", "更新者")
                DgvMithd.Columns.Add("更新日", "更新日")
            End If


            DgvMithd.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvMithd.Rows.Add()
                DgvMithd.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)("見積番号")
                DgvMithd.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)("見積番号枝番")
                DgvMithd.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)("見積日")
                DgvMithd.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)("見積有効期限")
                DgvMithd.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)("得意先コード")
                DgvMithd.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)("得意先名")
                DgvMithd.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)("得意先郵便番号")
                DgvMithd.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)("得意先住所")
                DgvMithd.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)("得意先電話番号")
                DgvMithd.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)("得意先ＦＡＸ")
                DgvMithd.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)("見積金額")
                DgvMithd.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)("仕入金額")
                DgvMithd.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)("ＶＡＴ")
                DgvMithd.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)("粗利額")
                DgvMithd.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)("支払条件")
                DgvMithd.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)("営業担当者")
                DgvMithd.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)("入力担当者")
                DgvMithd.Rows(index).Cells(17).Value = ds.Tables(RS).Rows(index)("備考")
                DgvMithd.Rows(index).Cells(18).Value = ds.Tables(RS).Rows(index)("登録日")
                DgvMithd.Rows(index).Cells(19).Value = ds.Tables(RS).Rows(index)("更新者")
                DgvMithd.Rows(index).Cells(20).Value = ds.Tables(RS).Rows(index)("更新日")
            Next
        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()

    End Sub

    Private Sub RbtnDetails_CheckedChanged(sender As Object, e As EventArgs) Handles RbtnDetails.CheckedChanged
        DgvMithd.Rows.Clear()
        DgvMithd.Columns.Clear()

        If RbtnSlip.Checked Then
            QuoteListLoad()
        Else
            Dim Sql As String = ""
            Dim strWhere As String = ""     'Where句

            Sql += "SELECT t02.* ,m901.文字１ as 仕入区分名 ,m902.文字１ as リードタイム単位名 "
            Sql += "FROM public.t01_mithd t01 ,public.t02_mitdt  t02  "
            Sql += "INNER JOIN public.m90_hanyo m901 "
            Sql += " ON m901.会社コード = t02.会社コード "
            Sql += "   and m901.固定キー = '1002' "
            Sql += "   and m901.可変キー = t02.仕入区分 "
            Sql += "LEFT JOIN public.m90_hanyo m902 "
            Sql += " ON m902.会社コード = t02.会社コード "
            Sql += "   and m902.固定キー = '4' "
            Sql += "   and m902.可変キー = to_char(t02.リードタイム単位,'FM9') "
            strWhere += "WHERE"
            strWhere += " t01.会社コード"
            strWhere += "='"
            strWhere += frmC01F10_Login.loginValue.BumonNM
            strWhere += "' "
            strWhere += " and t01.見積番号 = t02.見積番号 and t01.見積番号枝番 = t02.見積番号枝番"

            If TxtCustomerName.Text <> "" Then
                strWhere += " and "
                strWhere += "t01.得意先名"
                strWhere += " ILIKE "
                strWhere += "'%"
                strWhere += TxtCustomerName.Text
                strWhere += "%'"
            End If
            If TxtAddress.Text <> "" Then
                strWhere += " and "
                strWhere += "t01.得意先住所"
                strWhere += " ILIKE "
                strWhere += "'%"
                strWhere += TxtAddress.Text
                strWhere += "%'"
            End If
            If TxtTel.Text <> "" Then
                strWhere += " and "
                strWhere += "t01.得意先電話番号"
                strWhere += " ILIKE "
                strWhere += "'%"
                strWhere += TxtTel.Text
                strWhere += "%'"
            End If
            If TxtCustomerCode.Text <> "" Then
                strWhere += " and "
                strWhere += "t01.得意先コード"
                strWhere += " ILIKE "
                strWhere += "'%"
                strWhere += TxtCustomerCode.Text
                strWhere += "%'"
            End If
            If TxtQuoteDate1.Text <> "" Then
                strWhere += " and "
                strWhere += "t01.見積日"
                strWhere += " >=  "
                strWhere += "'"
                strWhere += TxtQuoteDate1.Text
                strWhere += "'"
            End If
            If TxtQuoteDate2.Text <> "" Then
                strWhere += " and "
                strWhere += "t01.見積日"
                strWhere += " <=  "
                strWhere += "'"
                strWhere += TxtQuoteDate2.Text
                strWhere += "'"
            End If
            If TxtQuoteNo1.Text <> "" Then
                strWhere += " and "
                strWhere += "t01.見積番号"
                strWhere += " >=  "
                strWhere += "'"
                strWhere += TxtQuoteNo1.Text
                strWhere += "'"
            End If
            If TxtQuoteNo2.Text <> "" Then
                strWhere += " and "
                strWhere += "t01.見積番号"
                strWhere += " <=  "
                strWhere += "'"
                strWhere += TxtQuoteNo2.Text
                strWhere += "'"
            End If
            If TxtSales.Text <> "" Then
                strWhere += " and "
                strWhere += "t01.営業担当者"
                strWhere += " ILIKE "
                strWhere += "'%"
                strWhere += TxtSales.Text
                strWhere += "%'"
            End If
            If Not ChkExpired.Checked Then
                strWhere += " and "
                strWhere += "t01.見積有効期限 >= '"
                strWhere += dtToday
                strWhere += "'"
            End If
            If Not ChkCancel.Checked Then
                strWhere += " and "
                strWhere += " t01.取消区分 = '0'"
            End If

            Sql += strWhere
            Sql += " ORDER BY t02.見積番号 DESC,t02.見積番号枝番 DESC ,t02.行番号"

            Dim reccnt As Integer = 0
            ds = _db.selectDB(Sql, RS, reccnt)

            If frmC01F10_Login.loginValue.Language = "ENG" Then
                DgvMithd.Columns.Add("見積番号", "QuotationNumber")
                DgvMithd.Columns.Add("見積番号枝番", "BranchNumber")
                DgvMithd.Columns.Add("仕入区分", "PurchasingClassification")
                DgvMithd.Columns.Add("メーカー", "Manufacturer")
                DgvMithd.Columns.Add("品名", "ItemName")
                DgvMithd.Columns.Add("型式", "Spec")
                DgvMithd.Columns.Add("数量", "Quantity")
                DgvMithd.Columns.Add("単位", "Unit")
                DgvMithd.Columns.Add("仕入先名称", "SupplierName")
                DgvMithd.Columns.Add("仕入単価", "PurchaseUnitPrice")
                DgvMithd.Columns.Add("間接費", "Overhead")
                DgvMithd.Columns.Add("仕入金額", "PurchaseAmount")
                DgvMithd.Columns.Add("売単価", "SellingPrice")
                DgvMithd.Columns.Add("売上金額", "SalesAmount")
                DgvMithd.Columns.Add("粗利額", "GrossMargin")
                DgvMithd.Columns.Add("粗利率", "GrossMarginRate")
                DgvMithd.Columns.Add("リードタイム", "LeadTime")
                DgvMithd.Columns.Add("備考", "Remarks")
                DgvMithd.Columns.Add("登録日", "RegistrationDate")
            Else
                DgvMithd.Columns.Add("見積番号", "見積番号")
                DgvMithd.Columns.Add("見積番号枝番", "見積番号枝番")
                DgvMithd.Columns.Add("仕入区分", "仕入区分")
                DgvMithd.Columns.Add("メーカー", "メーカー")
                DgvMithd.Columns.Add("品名", "品名")
                DgvMithd.Columns.Add("型式", "型式")
                DgvMithd.Columns.Add("数量", "数量")
                DgvMithd.Columns.Add("単位", "単位")
                DgvMithd.Columns.Add("仕入先名称", "仕入先名称")
                DgvMithd.Columns.Add("仕入単価", "仕入単価")
                DgvMithd.Columns.Add("間接費", "間接費")
                DgvMithd.Columns.Add("仕入金額", "仕入金額")
                DgvMithd.Columns.Add("売単価", "売単価")
                DgvMithd.Columns.Add("売上金額", "売上金額")
                DgvMithd.Columns.Add("粗利額", "粗利額")
                DgvMithd.Columns.Add("粗利率", "粗利率")
                DgvMithd.Columns.Add("リードタイム", "リードタイム")
                DgvMithd.Columns.Add("備考", "備考")
                DgvMithd.Columns.Add("登録日", "登録日")
            End If


            DgvMithd.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvMithd.Rows.Add()
                DgvMithd.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)("見積番号")
                DgvMithd.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)("見積番号枝番")
                DgvMithd.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)("仕入区分名")
                DgvMithd.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)("メーカー")
                DgvMithd.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)("品名")
                DgvMithd.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)("型式")
                DgvMithd.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)("数量")
                DgvMithd.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)("単位")
                DgvMithd.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)("仕入先名称")
                DgvMithd.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)("仕入単価")
                DgvMithd.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)("間接費")
                DgvMithd.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)("仕入金額")
                DgvMithd.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)("売単価")
                DgvMithd.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)("売上金額")
                DgvMithd.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)("粗利額")
                DgvMithd.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)("粗利率")
                DgvMithd.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)("リードタイム") & ds.Tables(RS).Rows(index)("リードタイム単位名")
                DgvMithd.Rows(index).Cells(17).Value = ds.Tables(RS).Rows(index)("備考")
                DgvMithd.Rows(index).Cells(18).Value = ds.Tables(RS).Rows(index)("登録日")
            Next
        End If
    End Sub

    Private Sub BtnQuoteAdd_Click(sender As Object, e As EventArgs) Handles BtnQuoteAdd.Click
        Dim Status As String = "ADD"
        Dim openForm As Form = Nothing
        openForm = New Quote(_msgHd, _db, _langHd, Me, , Status)
        Me.Hide()
        openForm.ShowDialog(Me)

        QuoteListLoad()

    End Sub

    Private Sub BtnQuoteEdit_Click(sender As Object, e As EventArgs) Handles BtnQuoteEdit.Click

        'グリッドにリストが存在しない場合は処理しない
        Try


            Dim RowIdx As Integer
            RowIdx = Me.DgvMithd.CurrentCell.RowIndex
            Dim No As String = DgvMithd.Rows(RowIdx).Cells(0).Value
            Dim Suffix As String = DgvMithd.Rows(RowIdx).Cells(1).Value

            Dim openForm As Form = Nothing
            openForm = New Quote(_msgHd, _db, _langHd, Me, No, Suffix)   '処理選択
            Me.Enabled = False
            Me.Hide()
            openForm.ShowDialog()

            QuoteListLoad()
        Catch
        End Try

    End Sub


    Private Sub BtnQuoteClone_Click(sender As Object, e As EventArgs) Handles BtnQuoteClone.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvMithd.CurrentCell.RowIndex
        Dim No As String = DgvMithd.Rows(RowIdx).Cells(0).Value
        Dim Suffix As String = DgvMithd.Rows(RowIdx).Cells(1).Value
        Dim Status As String = "CLONE"
        Dim openForm As Form = Nothing
        openForm = New Quote(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
        Me.Enabled = False
        Me.Hide()
        openForm.ShowDialog()

        QuoteListLoad()
    End Sub

    Private Sub BtnQuoteView_Click(sender As Object, e As EventArgs) Handles BtnQuoteView.Click

        'グリッドに何もないときは次画面へ移動しない
        Try
            Dim RowIdx As Integer
            RowIdx = Me.DgvMithd.CurrentCell.RowIndex
            Dim No As String = DgvMithd.Rows(RowIdx).Cells(0).Value
            Dim Suffix As String = DgvMithd.Rows(RowIdx).Cells(1).Value
            Dim Status As String = "VIEW"
            Dim openForm As Form = Nothing
            openForm = New Quote(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
            Me.Enabled = False
            Me.Hide()
            openForm.Show(Me)
        Catch

        End Try

    End Sub

    Private Sub BtnUnitPrice_Click(sender As Object, e As EventArgs) Handles BtnUnitPrice.Click

        'グリッドに何もないときは次画面へ移動しない
        Try
            Dim RowIdx As Integer
            RowIdx = Me.DgvMithd.CurrentCell.RowIndex
            Dim No As String = DgvMithd.Rows(RowIdx).Cells(0).Value
            Dim Suffix As String = DgvMithd.Rows(RowIdx).Cells(1).Value
            Dim Status As String = "PRICE"
            Dim openForm As Form = Nothing
            openForm = New Quote(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
            Me.Enabled = False
            Me.Hide()
            openForm.ShowDialog()

            QuoteListLoad()

        Catch

        End Try
    End Sub

    Private Sub BtnQuoteSearch_Click(sender As Object, e As EventArgs) Handles BtnQuoteSearch.Click
        QuoteListLoad()
    End Sub

    Private Sub BtnOrder_Click(sender As Object, e As EventArgs) Handles BtnOrderPurchase.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvMithd.CurrentCell.RowIndex
        Dim No As String = DgvMithd.Rows(RowIdx).Cells(0).Value
        Dim Suffix As String = DgvMithd.Rows(RowIdx).Cells(1).Value

        Dim openForm As Form = Nothing
        openForm = New Cymn(_msgHd, _db, _langHd, Me, No, Suffix)   '処理選択
        Me.Hide()
        openForm.Show(Me)
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Dim dtToday As DateTime = DateTime.Now

        Dim Sql1 As String = ""
        Sql1 = ""
        Sql1 += "UPDATE Public.t01_mithd "
        Sql1 += "SET 取消区分 = '1' "
        Sql1 += ",取消日 = '" & dtToday & "' "
        Sql1 += ",更新日 = '" & dtToday & "' "
        Sql1 += ",更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "' "
        Sql1 += "WHERE 会社コード ='" & frmC01F10_Login.loginValue.BumonNM & "'"
        Sql1 += " AND 見積番号 ='" & DgvMithd.Rows(DgvMithd.CurrentCell.RowIndex).Cells("見積番号").Value & "' "
        Sql1 += " AND 見積番号枝番 ='" & DgvMithd.Rows(DgvMithd.CurrentCell.RowIndex).Cells("見積番号枝番").Value & "' "
        Sql1 += "RETURNING 会社コード"
        Sql1 += ",見積番号 "
        Sql1 += ",見積番号枝番 "
        Sql1 += ",得意先コード "
        Sql1 += ",得意先名 "
        Sql1 += ",得意先郵便番号 "
        Sql1 += ",得意先住所 "
        Sql1 += ",得意先電話番号 "
        Sql1 += ",得意先ＦＡＸ "
        Sql1 += ",得意先担当者役職 "
        Sql1 += ",得意先担当者名 "
        Sql1 += ",見積日 "
        Sql1 += ",見積有効期限 "
        Sql1 += ",支払条件 "
        Sql1 += ",見積金額 "
        Sql1 += ",仕入金額 "
        Sql1 += ",営業担当者 "
        Sql1 += ",入力担当者 "
        Sql1 += ",備考 "
        Sql1 += ",ＶＡＴ "
        Sql1 += ",取消日 "
        Sql1 += ",取消区分 "
        Sql1 += ",登録日 "
        Sql1 += ",受注日 "
        Sql1 += ",更新日 "
        Sql1 += ",更新者"

        If frmC01F10_Login.loginValue.Language = "ENG" Then
            Dim result As DialogResult = MessageBox.Show("Would you like to cancel the Quotation？",
                                            "Question",
                                            MessageBoxButtons.YesNoCancel,
                                            MessageBoxIcon.Question,
                                            MessageBoxDefaultButton.Button2)

            If result = DialogResult.Yes Then
                _db.executeDB(Sql1)
                QuoteListLoad()
            ElseIf result = DialogResult.No Then

            ElseIf result = DialogResult.Cancel Then

            End If
        Else
            Dim result As DialogResult = MessageBox.Show("見積を取り消しますか？",
                                            "質問",
                                            MessageBoxButtons.YesNoCancel,
                                            MessageBoxIcon.Question,
                                            MessageBoxDefaultButton.Button2)

            If result = DialogResult.Yes Then
                _db.executeDB(Sql1)
                QuoteListLoad()
            ElseIf result = DialogResult.No Then

            ElseIf result = DialogResult.Cancel Then

            End If
        End If
    End Sub

    Private Sub ChkExpired_CheckedChanged(sender As Object, e As EventArgs) Handles ChkExpired.CheckedChanged

        QuoteListLoad()

    End Sub

    Private Sub BtnOrder_Click_1(sender As Object, e As EventArgs) Handles BtnOrder.Click
        Dim RowIdx As Integer
        RowIdx = DgvMithd.CurrentCell.RowIndex
        Dim No As String = DgvMithd.Rows(RowIdx).Cells(0).Value
        Dim Suffix As String = DgvMithd.Rows(RowIdx).Cells(1).Value
        Dim Status As String = "ADD"
        Dim openForm As Form = Nothing
        openForm = New Order(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
        openForm.Show(Me)
    End Sub

    Private Sub ChkCancel_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCancel.CheckedChanged

        QuoteListLoad()

    End Sub

End Class