Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports System.Globalization

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
    Private strToday As String = UtilClass.formatDatetime(dtToday)

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

        If _status = CommonConst.STATUS_EDIT Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "EditMode"
            Else
                LblMode.Text = "編集モード"
            End If

            BtnQuoteEdit.Visible = True
            BtnQuoteEdit.Location = New Point(997, 509)
        ElseIf _status = CommonConst.STATUS_CLONE Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "CopyMode"
            Else
                LblMode.Text = "複写モード"
            End If

            BtnQuoteClone.Visible = True
            BtnQuoteClone.Location = New Point(997, 509)
        ElseIf _status = CommonConst.STATUS_CANCEL Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "CancelMode"
            Else
                LblMode.Text = "取消モード"
            End If

            BtnCancel.Visible = True
            BtnCancel.Location = New Point(997, 509)
        ElseIf _status = CommonConst.STATUS_VIEW Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If

            BtnQuoteView.Visible = True
            BtnQuoteView.Location = New Point(997, 509)
        ElseIf _status = CommonConst.STATUS_PRICE Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "PurchasePriceInputMode"
            Else
                LblMode.Text = "仕入単価入力モード"
            End If

            BtnUnitPrice.Visible = True
            BtnUnitPrice.Location = New Point(997, 509)
        ElseIf _status = CommonConst.STATUS_ORDER_NEW Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "NewOrderRegistrationMode"
            Else
                LblMode.Text = "受注新規入力モード"
            End If

            BtnOrder.Visible = True
            BtnOrder.Location = New Point(997, 509)
            '使用していない
            'ElseIf _status = "PURCHASE_NEW" Then
            '    If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            '        LblMode.Text = "NewPurchaseRegistrationMode"
            '    Else
            '        LblMode.Text = "仕入新規入力モード"
            '    End If

            '    BtnPurchase.Visible = True
            '    BtnPurchase.Location = New Point(997, 509)
        ElseIf _status = CommonConst.STATUS_ORDER_PURCHASE Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "JobOrderingAndPurchasingMode"
            Else
                LblMode.Text = "受発注登録モード"
            End If

            BtnOrderPurchase.Visible = True
            BtnOrderPurchase.Location = New Point(997, 509)
        End If

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblConditions.Text = "ExtractionCondition"
            LblCustomerName.Text = "CustomerName"
            LblAddress.Text = "Address"
            LblTel.Text = "PhoneNumber"
            LblCustomerCode.Text = "CustomerCode"
            LblQuoteDate.Text = "QuotationDate"
            LblQuoteNo.Text = "QuotationNumber"
            LblSales.Text = "SalesPersonInCharge"
            Label10.Text = "DisplayFormat"
            LblManufacturer.Text = "Manufacturer"
            LblItemName.Text = "ItemName"
            LblSpec.Text = "Spec"

            RbtnSlip.Text = "UnitOfVoucher" '伝票単位
            RbtnDetails.Text = "UnitOfDetailData" '明細単位
            ChkExpired.Text = "IncludeExpriedData" '有効期限の切れたデータを含める
            ChkCancel.Text = "IncludeCancelData" '取消データを含める

            BtnQuoteSearch.Text = "Search"
            BtnOrderPurchase.Text = "Ordering"
            BtnQuoteAdd.Text = "QuotationRegistration"
            BtnUnitPrice.Text = "PurchasePriceInput"
            BtnQuoteEdit.Text = "QuotationEdit"
            BtnQuoteClone.Text = "QuotationCopy"
            BtnQuoteView.Text = "QuotationView"
            BtnOrder.Text = "JobOrderRegistration"
            BtnCancel.Text = "QuotationCancel"
            BtnBack.Text = "Back"
        End If

        '見積日の範囲指定を初期設定
        TxtQuoteDateSince.Value = DateAdd("d", CommonConst.SINCE_DEFAULT_DAY, DateTime.Today)
        TxtQuoteDateUntil.Value = DateTime.Today

        'グリッドの初期表示
        QuoteListLoad()

    End Sub

    '一覧作成
    Private Sub QuoteListLoad()

        Try

            '一覧クリア
            DgvMithd.Rows.Clear()
            DgvMithd.Columns.Clear()

            Dim reccnt As Integer = 0
            Dim Sql As String = ""
            'Dim strWhere As String = ""     'Where句

            '伝票形式
            If RbtnSlip.Checked Then

                Sql = " SELECT "

                Sql += " t01.会社コード"
                Sql += ",t01.取消区分"
                Sql += ",t01.見積番号"
                Sql += ",t01.見積番号枝番"
                Sql += ",t01.見積日"
                Sql += ",t01.見積有効期限"
                Sql += ",t01.得意先コード"
                Sql += ",t01.得意先名"
                Sql += ",t01.得意先郵便番号"
                Sql += ",t01.得意先住所"
                Sql += ",t01.得意先電話番号"
                Sql += ",t01.得意先ＦＡＸ"
                Sql += ",t01.見積金額"
                Sql += ",t01.仕入金額"
                Sql += ",t01.ＶＡＴ"
                Sql += ",t01.粗利額"
                Sql += ",t01.支払条件"
                Sql += ",t01.営業担当者"
                Sql += ",t01.入力担当者"
                Sql += ",t01.備考"
                Sql += ",t01.登録日"
                Sql += ",t01.更新者"
                Sql += ",t01.更新日"

                Sql += " FROM t01_mithd t01 "

                Sql += " WHERE "
                Sql += "  t01.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

                '抽出条件
                If TxtCustomerName.Text <> "" Then
                    Sql += " and "
                    Sql += "t01.得意先名"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += RevoveChars(TxtCustomerName.Text)
                    Sql += "%'"
                End If
                If TxtAddress.Text <> "" Then
                    Sql += " and "
                    Sql += "t01.得意先住所"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += RevoveChars(TxtAddress.Text)
                    Sql += "%'"
                End If
                If TxtTel.Text <> "" Then
                    Sql += " and "
                    Sql += "t01.得意先電話番号"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += RevoveChars(TxtTel.Text)
                    Sql += "%'"
                End If
                If TxtCustomerCode.Text <> "" Then
                    Sql += " and "
                    Sql += "t01.得意先コード"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += RevoveChars(TxtCustomerCode.Text)
                    Sql += "%'"
                End If
                If TxtQuoteDateSince.Text <> "" Then
                    Sql += " and "
                    Sql += "t01.見積日"
                    Sql += " >=  "
                    Sql += "'"
                    Sql += UtilClass.strFormatDate(RevoveChars(TxtQuoteDateSince.Text))
                    Sql += "'"
                End If
                If TxtQuoteDateUntil.Text <> "" Then
                    Sql += " and "
                    Sql += "t01.見積日"
                    Sql += " <=  "
                    Sql += "'"
                    Sql += UtilClass.strFormatDate(RevoveChars(TxtQuoteDateUntil.Text))
                    Sql += "'"
                End If
                If TxtQuoteNoSince.Text <> "" Then
                    Sql += " and "
                    Sql += "t01.見積番号"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += RevoveChars(TxtQuoteNoSince.Text)
                    Sql += "%'"
                End If
                If TxtSales.Text <> "" Then
                    Sql += " and "
                    Sql += "t01.営業担当者"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += RevoveChars(TxtSales.Text)
                    Sql += "%'"
                End If
                If Not ChkExpired.Checked Then
                    Sql += " and "
                    Sql += "t01.見積有効期限 >= '"
                    Sql += UtilClass.strFormatDate(strToday)
                    Sql += "'"
                End If
                If TxtManufacturer.Text <> "" Then
                    Sql += " and "
                    Sql += "t02.メーカー"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += RevoveChars(TxtManufacturer.Text)
                    Sql += "%'"
                End If
                If TxtItemName.Text <> "" Then
                    Sql += " and "
                    Sql += "t02.品名"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += RevoveChars(TxtItemName.Text)
                    Sql += "%'"
                End If
                If TxtSpec.Text <> "" Then
                    Sql += " and "
                    Sql += "t02.型式"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += RevoveChars(TxtSpec.Text)
                    Sql += "%'"
                End If
                If Not ChkCancel.Checked Then
                    Sql += " and "
                    Sql += " t01.取消区分 = '0'"
                End If

                '受注済みの見積は　編集、取消、仕入単価入力　できない
                '受発注登録の時も受注済みは表示しない
                If (_status = CommonConst.STATUS_CANCEL) Or (_status = CommonConst.STATUS_PRICE) Or (_status = CommonConst.STATUS_EDIT) Or
                        (_status = CommonConst.STATUS_ORDER_NEW) Or (_status = CommonConst.STATUS_ORDER_PURCHASE) Then
                    Sql += " and t01.受注日 is null"
                End If
                '受発注登録の時は有効期限切れは表示しない
                If (_status = CommonConst.STATUS_ORDER_NEW) Or (_status = CommonConst.STATUS_ORDER_PURCHASE) Then
                    Sql += " and t01.見積有効期限 >= '" & UtilClass.strFormatDate(DateTime.Now.ToShortDateString) & "'"
                End If

                '取消データを含めない場合
                If ChkCancel.Checked = False Then
                    Sql += " AND "
                    Sql += "t01.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
                End If

                '有効期限の切れたデータを含めない場合
                If ChkExpired.Checked = False Or _status = CommonConst.STATUS_ORDER_NEW Then
                    Sql += " AND "
                    Sql += "t01.見積有効期限 >= '" & UtilClass.strFormatDate(strToday) & "'"
                End If

                Sql += " ORDER BY t01.見積番号 DESC,t01.見積番号枝番 DESC"

                ds = _db.selectDB(Sql, RS, reccnt)

                setListHd() '見出し行セット

                For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                    DgvMithd.Rows.Add()
                    DgvMithd.Rows(i).Cells("取消").Value = getDelKbnTxt(ds.Tables(RS).Rows(i)("取消区分"))
                    DgvMithd.Rows(i).Cells("見積番号").Value = ds.Tables(RS).Rows(i)("見積番号")
                    DgvMithd.Rows(i).Cells("見積番号枝番").Value = ds.Tables(RS).Rows(i)("見積番号枝番")
                    DgvMithd.Rows(i).Cells("見積日").Value = ds.Tables(RS).Rows(i)("見積日").ToShortDateString
                    DgvMithd.Rows(i).Cells("見積有効期限").Value = ds.Tables(RS).Rows(i)("見積有効期限")
                    DgvMithd.Rows(i).Cells("得意先コード").Value = ds.Tables(RS).Rows(i)("得意先コード")
                    DgvMithd.Rows(i).Cells("得意先名").Value = ds.Tables(RS).Rows(i)("得意先名")
                    DgvMithd.Rows(i).Cells("得意先郵便番号").Value = ds.Tables(RS).Rows(i)("得意先郵便番号")
                    DgvMithd.Rows(i).Cells("得意先住所").Value = ds.Tables(RS).Rows(i)("得意先住所")
                    DgvMithd.Rows(i).Cells("得意先電話番号").Value = ds.Tables(RS).Rows(i)("得意先電話番号")
                    DgvMithd.Rows(i).Cells("得意先ＦＡＸ").Value = ds.Tables(RS).Rows(i)("得意先ＦＡＸ")
                    DgvMithd.Rows(i).Cells("見積金額").Value = ds.Tables(RS).Rows(i)("見積金額")
                    DgvMithd.Rows(i).Cells("仕入金額").Value = ds.Tables(RS).Rows(i)("仕入金額")
                    DgvMithd.Rows(i).Cells("ＶＡＴ").Value = ds.Tables(RS).Rows(i)("ＶＡＴ")
                    DgvMithd.Rows(i).Cells("粗利額").Value = ds.Tables(RS).Rows(i)("粗利額")
                    DgvMithd.Rows(i).Cells("支払条件").Value = ds.Tables(RS).Rows(i)("支払条件")
                    DgvMithd.Rows(i).Cells("営業担当者").Value = ds.Tables(RS).Rows(i)("営業担当者")
                    DgvMithd.Rows(i).Cells("入力担当者").Value = ds.Tables(RS).Rows(i)("入力担当者")
                    DgvMithd.Rows(i).Cells("備考").Value = ds.Tables(RS).Rows(i)("備考")
                    DgvMithd.Rows(i).Cells("登録日").Value = ds.Tables(RS).Rows(i)("登録日")
                    DgvMithd.Rows(i).Cells("更新者").Value = ds.Tables(RS).Rows(i)("更新者")
                    DgvMithd.Rows(i).Cells("更新日").Value = ds.Tables(RS).Rows(i)("更新日")
                Next

            Else
                '明細形式表示時

                setListDt() '見出し行セット
                If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                    Sql = "SELECT t02.* ,m901.文字２ as 仕入区分名 ,m902.文字２ as リードタイム単位名, t01.取消区分 "
                Else
                    Sql = "SELECT t02.* ,m901.文字１ as 仕入区分名 ,m902.文字１ as リードタイム単位名, t01.取消区分 "
                End If

                Sql += "FROM public.t01_mithd t01 ,public.t02_mitdt  t02  "
                Sql += "INNER JOIN public.m90_hanyo m901 "
                Sql += " ON m901.会社コード = t02.会社コード "
                Sql += "   and m901.固定キー = '" & CommonConst.FIXED_KEY_PURCHASING_CLASS & "' "
                Sql += "   and m901.可変キー = t02.仕入区分 "
                Sql += "LEFT JOIN public.m90_hanyo m902 "
                Sql += " ON m902.会社コード = t02.会社コード "
                Sql += "   and m902.固定キー = '" & CommonConst.FIXED_KEY_READTIME & "' "
                Sql += "   and m902.可変キー = to_char(t02.リードタイム単位,'FM9') "
                Sql += "WHERE"
                Sql += " t01.会社コード"
                Sql += "='"
                Sql += frmC01F10_Login.loginValue.BumonCD
                Sql += "' "
                Sql += " and t01.見積番号 = t02.見積番号 and t01.見積番号枝番 = t02.見積番号枝番"

                If TxtCustomerName.Text <> "" Then
                    Sql += " and "
                    Sql += "t01.得意先名"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += RevoveChars(TxtCustomerName.Text)
                    Sql += "%'"
                End If
                If TxtAddress.Text <> "" Then
                    Sql += " and "
                    Sql += "t01.得意先住所"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += RevoveChars(TxtAddress.Text)
                    Sql += "%'"
                End If
                If TxtTel.Text <> "" Then
                    Sql += " and "
                    Sql += "t01.得意先電話番号"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += RevoveChars(TxtTel.Text)
                    Sql += "%'"
                End If
                If TxtCustomerCode.Text <> "" Then
                    Sql += " and "
                    Sql += "t01.得意先コード"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += RevoveChars(TxtCustomerCode.Text)
                    Sql += "%'"
                End If
                If TxtQuoteDateSince.Text <> "" Then
                    Sql += " and "
                    Sql += "t01.見積日"
                    Sql += " >=  "
                    Sql += "'"
                    Sql += UtilClass.strFormatDate(TxtQuoteDateSince.Text)
                    Sql += "'"
                End If
                If TxtQuoteDateUntil.Text <> "" Then
                    Sql += " and "
                    Sql += "t01.見積日"
                    Sql += " <=  "
                    Sql += "'"
                    Sql += UtilClass.strFormatDate(TxtQuoteDateUntil.Text)
                    Sql += "'"
                End If
                If TxtQuoteNoSince.Text <> "" Then
                    Sql += " and "
                    Sql += "t01.見積番号"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += RevoveChars(TxtQuoteNoSince.Text)
                    Sql += "%'"
                End If
                If TxtSales.Text <> "" Then
                    Sql += " and "
                    Sql += "t01.営業担当者"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += RevoveChars(TxtSales.Text)
                    Sql += "%'"
                End If
                If TxtManufacturer.Text <> "" Then
                    Sql += " and "
                    Sql += "t02.メーカー"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += RevoveChars(TxtManufacturer.Text)
                    Sql += "%'"
                End If
                If TxtItemName.Text <> "" Then
                    Sql += " and "
                    Sql += "t02.品名"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += RevoveChars(TxtItemName.Text)
                    Sql += "%'"
                End If
                If TxtSpec.Text <> "" Then
                    Sql += " and "
                    Sql += "t02.型式"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += RevoveChars(TxtSpec.Text)
                    Sql += "%'"
                End If
                If Not ChkCancel.Checked Then
                    Sql += " and "
                    Sql += " t01.取消区分 = '0'"
                End If

                '受注済みの見積は取消できない
                '受注済みの見積は仕入単価入力もできない
                '受発注登録の時も受注済みは表示しない
                If (_status = CommonConst.STATUS_CANCEL) Or (_status = CommonConst.STATUS_PRICE) Or (_status = CommonConst.STATUS_ORDER_NEW) Or (_status = CommonConst.STATUS_ORDER_PURCHASE) Then
                    Sql += " and t01.受注日 is null"
                End If


                If ChkExpired.Checked = False Then
                    Sql += " and t01.見積有効期限 >= '" & UtilClass.strFormatDate(DateTime.Today.ToShortDateString) & "'"
                End If


                Sql += " ORDER BY t02.見積番号 DESC,t02.見積番号枝番 DESC ,t02.行番号"

                ds = _db.selectDB(Sql, RS, reccnt)

                For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                    DgvMithd.Rows.Add()
                    DgvMithd.Rows(i).Cells("取消").Value = getDelKbnTxt(ds.Tables(RS).Rows(i)("取消区分"))
                    DgvMithd.Rows(i).Cells("見積番号").Value = ds.Tables(RS).Rows(i)("見積番号")
                    DgvMithd.Rows(i).Cells("見積番号枝番").Value = ds.Tables(RS).Rows(i)("見積番号枝番")
                    DgvMithd.Rows(i).Cells("仕入区分").Value = ds.Tables(RS).Rows(i)("仕入区分名")
                    DgvMithd.Rows(i).Cells("メーカー").Value = ds.Tables(RS).Rows(i)("メーカー")
                    DgvMithd.Rows(i).Cells("品名").Value = ds.Tables(RS).Rows(i)("品名")
                    DgvMithd.Rows(i).Cells("型式").Value = ds.Tables(RS).Rows(i)("型式")
                    DgvMithd.Rows(i).Cells("数量").Value = ds.Tables(RS).Rows(i)("数量")
                    DgvMithd.Rows(i).Cells("単位").Value = ds.Tables(RS).Rows(i)("単位")
                    DgvMithd.Rows(i).Cells("仕入先名称").Value = ds.Tables(RS).Rows(i)("仕入先名称")
                    DgvMithd.Rows(i).Cells("仕入単価").Value = ds.Tables(RS).Rows(i)("仕入単価")
                    DgvMithd.Rows(i).Cells("間接費").Value = ds.Tables(RS).Rows(i)("間接費")
                    DgvMithd.Rows(i).Cells("仕入金額").Value = ds.Tables(RS).Rows(i)("仕入金額")
                    DgvMithd.Rows(i).Cells("売単価").Value = ds.Tables(RS).Rows(i)("売単価")
                    DgvMithd.Rows(i).Cells("売上金額").Value = ds.Tables(RS).Rows(i)("売上金額")
                    DgvMithd.Rows(i).Cells("粗利額").Value = ds.Tables(RS).Rows(i)("粗利額")
                    DgvMithd.Rows(i).Cells("粗利率").Value = ds.Tables(RS).Rows(i)("粗利率")
                    DgvMithd.Rows(i).Cells("リードタイム").Value = ds.Tables(RS).Rows(i)("リードタイム") & ds.Tables(RS).Rows(i)("リードタイム単位名")
                    DgvMithd.Rows(i).Cells("備考").Value = ds.Tables(RS).Rows(i)("備考")
                    DgvMithd.Rows(i).Cells("登録日").Value = ds.Tables(RS).Rows(i)("登録日")
                Next

            End If


        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '伝票表示形式選択時の見出しセット
    Private Sub setListHd()
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            DgvMithd.Columns.Add("取消", "Cancel")
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
            DgvMithd.Columns.Add("取消", "取消")
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

        DgvMithd.Columns("見積金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvMithd.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvMithd.Columns("ＶＡＴ").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvMithd.Columns("粗利額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        '数字形式
        DgvMithd.Columns("見積金額").DefaultCellStyle.Format = "N2"
        DgvMithd.Columns("仕入金額").DefaultCellStyle.Format = "N2"
        DgvMithd.Columns("ＶＡＴ").DefaultCellStyle.Format = "N2"
        DgvMithd.Columns("粗利額").DefaultCellStyle.Format = "N2"

    End Sub

    '明細表示形式選択時の見出しセット
    Private Sub setListDt()
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            DgvMithd.Columns.Add("取消", "Cancel")
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
            DgvMithd.Columns.Add("取消", "取消")
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

        DgvMithd.Columns("数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvMithd.Columns("仕入単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvMithd.Columns("間接費").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvMithd.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvMithd.Columns("売単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvMithd.Columns("売上金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvMithd.Columns("粗利額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvMithd.Columns("粗利率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvMithd.Columns("リードタイム").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


        '数字形式
        DgvMithd.Columns("数量").DefaultCellStyle.Format = "N2"
        DgvMithd.Columns("仕入単価").DefaultCellStyle.Format = "N2"
        DgvMithd.Columns("間接費").DefaultCellStyle.Format = "N2"
        DgvMithd.Columns("仕入金額").DefaultCellStyle.Format = "N2"
        DgvMithd.Columns("売単価").DefaultCellStyle.Format = "N2"
        DgvMithd.Columns("売上金額").DefaultCellStyle.Format = "N2"
        DgvMithd.Columns("粗利額").DefaultCellStyle.Format = "N2"
        DgvMithd.Columns("粗利率").DefaultCellStyle.Format = "N1"

    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()

    End Sub

    '明細形式選択時
    Private Sub RbtnDetails_CheckedChanged(sender As Object, e As EventArgs) Handles RbtnDetails.CheckedChanged
        QuoteListLoad() '一覧取得
    End Sub

    '見積登録ボタン押下時
    Private Sub BtnQuoteAdd_Click(sender As Object, e As EventArgs) Handles BtnQuoteAdd.Click
        Dim Status As String = CommonConst.STATUS_ADD
        Dim openForm As Form = Nothing
        openForm = New Quote(_msgHd, _db, _langHd, Me, , Status)
        Me.Hide()
        openForm.ShowDialog(Me)

        QuoteListLoad() '一覧取得
    End Sub

    '見積編集ボタン押下時
    Private Sub BtnQuoteEdit_Click(sender As Object, e As EventArgs) Handles BtnQuoteEdit.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvMithd.CurrentCell.RowIndex

        'グリッドにリストが存在しない場合は処理しない
        If actionChk() = False Then
            Return
        End If

        If DgvMithd.Rows(RowIdx).Cells("取消").Value = CommonConst.CANCEL_KBN_DISABLED_TXT Then
            '対象のデータではないことをアラートする
            _msgHd.dspMSG("cannotSelectTorikeshiData", frmC01F10_Login.loginValue.Language)

            Return

        End If

        Try

            Dim No As String = DgvMithd.Rows(RowIdx).Cells("見積番号").Value
            Dim Suffix As String = DgvMithd.Rows(RowIdx).Cells("見積番号枝番").Value

            Dim openForm As Form = Nothing
            openForm = New Quote(_msgHd, _db, _langHd, Me, No, Suffix)   '処理選択
            Me.Enabled = False
            Me.Hide()
            openForm.ShowDialog()

            QuoteListLoad() '一覧取得

        Catch ex As Exception

            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))

        End Try

    End Sub

    '見積複製ボタン押下時
    Private Sub BtnQuoteClone_Click(sender As Object, e As EventArgs) Handles BtnQuoteClone.Click

        'グリッドにリストが存在しない場合は処理しない
        If actionChk() = False Then
            Return
        End If

        Try

            Dim RowIdx As Integer
            RowIdx = Me.DgvMithd.CurrentCell.RowIndex
            Dim No As String = DgvMithd.Rows(RowIdx).Cells("見積番号").Value
            Dim Suffix As String = DgvMithd.Rows(RowIdx).Cells("見積番号枝番").Value
            Dim Status As String = CommonConst.STATUS_CLONE
            Dim openForm As Form = Nothing
            openForm = New Quote(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
            Me.Enabled = False
            Me.Hide()
            openForm.ShowDialog()

            QuoteListLoad() '一覧取得

        Catch ex As Exception

            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))

        End Try

    End Sub

    '見積参照ボタン押下時
    Private Sub BtnQuoteView_Click(sender As Object, e As EventArgs) Handles BtnQuoteView.Click

        'グリッドに何もないときは次画面へ移動しない
        If actionChk() = False Then
            Return
        End If

        Try
            Dim RowIdx As Integer
            RowIdx = Me.DgvMithd.CurrentCell.RowIndex
            Dim No As String = DgvMithd.Rows(RowIdx).Cells("見積番号").Value
            Dim Suffix As String = DgvMithd.Rows(RowIdx).Cells("見積番号枝番").Value
            Dim Status As String = CommonConst.STATUS_VIEW
            Dim openForm As Form = Nothing
            openForm = New Quote(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
            Me.Enabled = False
            Me.Hide()
            openForm.Show(Me)

        Catch ex As Exception

            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))

        End Try

    End Sub

    '単価入力
    '
    Private Sub BtnUnitPrice_Click(sender As Object, e As EventArgs) Handles BtnUnitPrice.Click
        'グリッドに何もないときは次画面へ移動しない
        If actionChk() = False Then
            Return
        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvMithd.CurrentCell.RowIndex

        'グリッドに何もないときは次画面へ移動しない
        If actionChk() = False Then
            Return
        End If

        If DgvMithd.Rows(RowIdx).Cells("取消").Value = CommonConst.CANCEL_KBN_DISABLED_TXT Then
            '対象のデータではないことをアラートする
            _msgHd.dspMSG("cannotSelectTorikeshiData", frmC01F10_Login.loginValue.Language)

            Return

        End If

        Try
            Dim No As String = DgvMithd.Rows(RowIdx).Cells("見積番号").Value
            Dim Suffix As String = DgvMithd.Rows(RowIdx).Cells("見積番号枝番").Value
            Dim Status As String = CommonConst.STATUS_PRICE
            Dim openForm As Form = Nothing
            openForm = New Quote(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
            Me.Enabled = False
            Me.Hide()
            openForm.ShowDialog()

            QuoteListLoad() '一覧取得

        Catch ex As Exception

            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))

        End Try

    End Sub

    '検索ボタン押下時
    Private Sub BtnQuoteSearch_Click(sender As Object, e As EventArgs) Handles BtnQuoteSearch.Click
        QuoteListLoad() '一覧再表示
    End Sub

    '受発注ボタン押下時
    Private Sub BtnOrder_Click(sender As Object, e As EventArgs) Handles BtnOrderPurchase.Click
        'グリッドに何もないときは次画面へ移動しない
        If actionChk() = False Then
            Return
        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvMithd.CurrentCell.RowIndex

        If DgvMithd.Rows(RowIdx).Cells("取消").Value = CommonConst.CANCEL_KBN_DISABLED_TXT Then
            '対象のデータではないことをアラートする
            _msgHd.dspMSG("cannotSelectTorikeshiData", frmC01F10_Login.loginValue.Language)

            Return

        End If

        Try

            Dim No As String = DgvMithd.Rows(RowIdx).Cells("見積番号").Value
            Dim Suffix As String = DgvMithd.Rows(RowIdx).Cells("見積番号枝番").Value

            Dim openForm As Form = Nothing
            openForm = New Cymn(_msgHd, _db, _langHd, Me, No, Suffix)   '処理選択

            Me.Hide()
            openForm.Show(Me)

        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))

        End Try
    End Sub

    '見積取消
    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        'グリッドに何もないときは次画面へ移動しない
        If actionChk() = False Then
            Return
        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvMithd.CurrentCell.RowIndex


        '明細表示時は取消操作不可能
        If RbtnDetails.Checked Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Return

        End If

        If DgvMithd.Rows(RowIdx).Cells("取消").Value = CommonConst.CANCEL_KBN_DISABLED_TXT Then
            '対象のデータではないことをアラートする
            _msgHd.dspMSG("cannotSelectTorikeshiData", frmC01F10_Login.loginValue.Language)

            Return

        End If

        Dim dtToday As DateTime = DateTime.Now
        Dim strToday As String = UtilClass.formatDatetime(dtToday)
        Dim Sql1 As String = ""

        Try
            Sql1 = "UPDATE Public.t01_mithd "
            Sql1 += "SET 取消区分 = '1' "
            Sql1 += ",取消日 = '" & strToday & "' "
            Sql1 += ",更新日 = '" & strToday & "' "
            Sql1 += ",更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "' "
            Sql1 += "WHERE 会社コード ='" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql1 += " AND 見積番号 ='" & DgvMithd.Rows(DgvMithd.CurrentCell.RowIndex).Cells("見積番号").Value & "' "
            Sql1 += " AND 見積番号枝番 ='" & DgvMithd.Rows(DgvMithd.CurrentCell.RowIndex).Cells("見積番号枝番").Value & "' "

            Dim result As DialogResult = _msgHd.dspMSG("confirmCancel", frmC01F10_Login.loginValue.Language)
            If result = DialogResult.Yes Then

                _db.executeDB(Sql1)

                QuoteListLoad() '一覧取得
            End If

        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))

        End Try

    End Sub

    '有効期限の切れたデータを含めるチェックイベント
    Private Sub ChkExpired_CheckedChanged(sender As Object, e As EventArgs) Handles ChkExpired.CheckedChanged

        QuoteListLoad() '一覧取得

    End Sub

    '受注登録ボタンクリック時
    '
    Private Sub BtnOrder_Click_1(sender As Object, e As EventArgs) Handles BtnOrder.Click
        Dim RowIdx As Integer
        RowIdx = DgvMithd.CurrentCell.RowIndex

        'グリッドに何もないときは次画面へ移動しない
        If actionChk() = False Then
            Return
        End If

        If DgvMithd.Rows(RowIdx).Cells("取消").Value = CommonConst.CANCEL_KBN_DISABLED_TXT Then
            '対象のデータではないことをアラートする
            _msgHd.dspMSG("cannotSelectTorikeshiData", frmC01F10_Login.loginValue.Language)

            Return

        End If

        Try
            Dim No As String = DgvMithd.Rows(RowIdx).Cells("見積番号").Value
            Dim Suffix As String = DgvMithd.Rows(RowIdx).Cells("見積番号枝番").Value
            Dim Status As String = CommonConst.STATUS_ADD
            Dim openForm As Form = Nothing
            openForm = New Order(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
            openForm.Show(Me)

        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))

        End Try
    End Sub

    '取消データを含めるチェックイベント
    Private Sub ChkCancel_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCancel.CheckedChanged

        QuoteListLoad() '一覧取得

    End Sub

    '画面がアクティブになるときのイベント
    Private Sub QuoteList_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        QuoteListLoad()
    End Sub

    '表示形式条件
    Private Function viewFormat() As String
        Dim Sql As String = ""

        '取消データを含めない場合
        If ChkCancel.Checked = False Then
            Sql += " AND "
            Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
        End If

        '有効期限の切れたデータを含めない場合
        If ChkExpired.Checked = False Or _status = CommonConst.STATUS_ORDER_NEW Then
            Sql += " AND "
            Sql += "見積有効期限 >= '" & UtilClass.strFormatDate(strToday) & "'"
        End If

        Return Sql

    End Function

    Private Function actionChk() As Boolean
        '対象データがない場合は取消操作不可能
        If DgvMithd.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)

            Return False

        Else

            Return True

        End If
    End Function

    'param1：String テーブル名
    'param2：String 詳細条件
    'Return: DataSet
    Private Function getDsData(ByVal tableName As String, Optional ByRef txtParam As String = "") As DataSet
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Sql = "SELECT"
        Sql += " *"
        Sql += " FROM "

        Sql += "public." & tableName
        Sql += " WHERE "
        Sql += "会社コード"
        Sql += " =  "
        Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += txtParam
        Return _db.selectDB(Sql, RS, reccnt)
    End Function

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

    '取消区分の表示テキストを返す
    'param1：String テーブル名
    'param2：String 詳細条件
    'Return: DataSet
    Public Function getDelKbnTxt(ByVal delKbn As String) As String
        '区分の値を取得し、使用言語に応じて値を返却

        Dim reDelKbn As String = IIf(delKbn = CommonConst.CANCEL_KBN_DISABLED,
                                    IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_JPN, CommonConst.CANCEL_KBN_JPN_TXT, CommonConst.CANCEL_KBN_ENG_TXT),
                                    "")
        Return reDelKbn
    End Function

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles LblManufacturer.Click

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TxtManufacturer.TextChanged

    End Sub

    Private Sub TxtQuoteNoSince_TextChanged(sender As Object, e As EventArgs) Handles TxtQuoteNoSince.TextChanged

    End Sub
End Class