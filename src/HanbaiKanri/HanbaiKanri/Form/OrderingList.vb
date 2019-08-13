Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Globalization
Imports System.Text.RegularExpressions

Public Class OrderingList
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
    Private CompanyCode As String = ""
    Private OrderNo As String()
    Private _status As String = ""
    Private List As New List(Of String)(New String() {})

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
                   Optional ByRef prmRefStatus As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
        _status = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True
        DgvHtyhd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells
    End Sub

    '画面表示時
    Private Sub OrderingList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _status = CommonConst.STATUS_ORDING Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "PurchasedInputMode"
            Else
                LblMode.Text = "仕入入力モード"
            End If

            BtnOrding.Visible = True
            BtnOrding.Location = New Point(997, 509)
        ElseIf _status = CommonConst.STATUS_RECEIPT Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "GoodsReceiptInputMode"
            Else
                LblMode.Text = "入庫入力モード"
            End If

            BtnReceipt.Visible = True
            BtnReceipt.Location = New Point(997, 509)
        ElseIf _status = CommonConst.STATUS_EDIT Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "EditMode"
            Else
                LblMode.Text = "編集モード"
            End If

            BtnPurchaseEdit.Visible = True
            BtnPurchaseEdit.Location = New Point(997, 509)
        ElseIf _status = CommonConst.STATUS_VIEW Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If

            BtnPurchaseView.Visible = True
            BtnPurchaseView.Location = New Point(997, 509)
        ElseIf _status = CommonConst.STATUS_CANCEL Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "CancelMode"
            Else
                LblMode.Text = "取消モード"
            End If

            BtnPurchaseCancel.Visible = True
            BtnPurchaseCancel.Location = New Point(997, 509)
        ElseIf _status = CommonConst.STATUS_CLONE Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "NewCopyMode"
            Else
                LblMode.Text = "新規複写モード"
            End If

            BtnPurchaseClone.Visible = True
            BtnPurchaseClone.Location = New Point(997, 509)
        ElseIf _status = CommonConst.STATUS_AP Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "AccountsPayableInputMode"
            Else
                LblMode.Text = "買掛入力モード"
            End If

            BtnAP.Visible = True
            BtnAP.Location = New Point(997, 509)
        End If

        '検索（Date）の初期値
        dtPurchaseDateSince.Value = DateAdd("d", CommonConst.SINCE_DEFAULT_DAY, DateTime.Today)
        dtPurchaseDateUntil.Value = DateTime.Today

        '一覧再表示
        getList()

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblConditions.Text = "TermsOfSelection" '抽出条件
            Label1.Text = "SupplierName"
            Label2.Text = "Address"
            Label3.Text = "PhoneNumber"
            Label4.Text = "SupplierCode"
            Label8.Text = "PurchaseDate"
            Label7.Text = "PurchaseNumber"
            Label6.Text = "SalesPersonInCharge"
            Label11.Text = "CustomerNumber"
            lblMaker.Text = "Maker"
            LblItemName.Text = "ItemName"
            LblSpec.Text = "Spec"
            Label10.Text = "DisplayFormat"
            RbtnSlip.Text = "UnitOfVoucher"

            RbtnDetails.Text = "LineItemUnit"
            RbtnDetails.Location = New Point(166, 196)

            ChkCancelData.Text = "IncludeCancelData"
            ChkCancelData.Location = New Point(556, 196)

            BtnPurchaseView.Text = "PurchaseView"
            BtnPurchaseSearch.Text = "Search"
            BtnPurchaseCancel.Text = "CancelOfPurchase"
            BtnPurchaseClone.Text = "PurchaseCopy"
            BtnBack.Text = "Back"
            BtnAP.Text = "AccountsPayable"
            BtnOrding.Text = "PurchaseRegistration"
            BtnReceipt.Text = "ReceiptRegistration"
            BtnPurchaseEdit.Text = "PurchaseEdit"

        End If
    End Sub

    Private Sub getList()
        '一覧クリア
        DgvHtyhd.Rows.Clear()
        DgvHtyhd.Columns.Clear()

        Dim ds As DataSet
        Dim Sql As String = ""
        Dim reccnt As Integer = 0 'DB用（デフォルト）

        Dim curds As DataSet  'm25_currency
        Dim cur As String

        Dim strBaseCur = setBaseCurrency()

        Try

            '伝票単位
            If RbtnSlip.Checked Then

                If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                    DgvHtyhd.Columns.Add("取消", "Cancel")
                    DgvHtyhd.Columns.Add("発注番号", "PurchaseNumber")
                    DgvHtyhd.Columns.Add("発注番号枝番", "PurchaseOrderVer.")
                    DgvHtyhd.Columns.Add("客先番号", "CustomerNumber")
                    DgvHtyhd.Columns.Add("発注日", "PurchaseDate")
                    DgvHtyhd.Columns.Add("得意先名", "CustomerName")
                    DgvHtyhd.Columns.Add("仕入先コード", "SupplierCode")
                    DgvHtyhd.Columns.Add("仕入先名", "SupplierName")

                    DgvHtyhd.Columns.Add("通貨_外貨", "Currency")
                    'DgvHtyhd.Columns.Add("通貨", "Currency")

                    DgvHtyhd.Columns.Add("仕入原価_外貨", "PurchasingCost" & vbCrLf & "(ForeignCurrency)")
                    DgvHtyhd.Columns.Add("仕入原価", "PurchasingCost" & vbCrLf & "(" & setBaseCurrency() & ")")


                    DgvHtyhd.Columns.Add("仕入金額_外貨", "PurchaseAmount" & vbCrLf & "(ForeignCurrency)")
                    DgvHtyhd.Columns.Add("仕入金額", "PurchaseAmount" & vbCrLf & "(" & setBaseCurrency() & ")")


                    DgvHtyhd.Columns.Add("仕入先郵便番号", "PostalCode")
                    DgvHtyhd.Columns.Add("仕入先住所", "Address")
                    DgvHtyhd.Columns.Add("仕入先電話番号", "PhoneNumber")
                    DgvHtyhd.Columns.Add("仕入先ＦＡＸ", "FAX")
                    DgvHtyhd.Columns.Add("仕入先担当者名", "NameOfPIC")
                    DgvHtyhd.Columns.Add("仕入先担当者役職", "PositionPICSupplier")
                    DgvHtyhd.Columns.Add("支払条件", "PaymentTerms")
                    DgvHtyhd.Columns.Add("営業担当者", "SalesPersonInCharge")
                    DgvHtyhd.Columns.Add("入力担当者", "PICForInputting")
                    DgvHtyhd.Columns.Add("備考", "Remarks")
                    DgvHtyhd.Columns.Add("登録日", "RegistrationDate")
                    DgvHtyhd.Columns.Add("更新日", "LastUpdateDate")
                Else
                    DgvHtyhd.Columns.Add("取消", "取消")
                    DgvHtyhd.Columns.Add("発注番号", "発注番号")
                    DgvHtyhd.Columns.Add("発注番号枝番", "発注Ver.")
                    DgvHtyhd.Columns.Add("客先番号", "客先番号")
                    DgvHtyhd.Columns.Add("発注日", "発注日")
                    DgvHtyhd.Columns.Add("得意先名", "得意先名")
                    DgvHtyhd.Columns.Add("仕入先コード", "仕入先コード")
                    DgvHtyhd.Columns.Add("仕入先名", "仕入先名")

                    DgvHtyhd.Columns.Add("通貨_外貨", "通貨")
                    'DgvHtyhd.Columns.Add("通貨", "通貨")
                    DgvHtyhd.Columns.Add("仕入原価_外貨", "仕入原価" & vbCrLf & "(原通貨)")
                    DgvHtyhd.Columns.Add("仕入原価", "仕入原価" & vbCrLf & "(" & setBaseCurrency() & ")")
                    DgvHtyhd.Columns.Add("仕入金額_外貨", "仕入金額" & vbCrLf & "(原通貨)")
                    DgvHtyhd.Columns.Add("仕入金額", "仕入金額" & vbCrLf & "(" & setBaseCurrency() & ")")

                    DgvHtyhd.Columns.Add("仕入先郵便番号", "仕入先郵便番号")
                    DgvHtyhd.Columns.Add("仕入先住所", "仕入先先住所")
                    DgvHtyhd.Columns.Add("仕入先電話番号", "仕入先電話番号")
                    DgvHtyhd.Columns.Add("仕入先ＦＡＸ", "仕入先ＦＡＸ")
                    DgvHtyhd.Columns.Add("仕入先担当者名", "仕入先担当者名")
                    DgvHtyhd.Columns.Add("仕入先担当者役職", "仕入先担当者役職")

                    DgvHtyhd.Columns.Add("支払条件", "支払条件")
                    DgvHtyhd.Columns.Add("営業担当者", "営業担当者")
                    DgvHtyhd.Columns.Add("入力担当者", "入力担当者")
                    DgvHtyhd.Columns.Add("備考", "備考")
                    DgvHtyhd.Columns.Add("登録日", "登録日")
                    DgvHtyhd.Columns.Add("更新日", "最終更新日")
                End If

                '数字形式
                DgvHtyhd.Columns("仕入原価_外貨").DefaultCellStyle.Format = "N2"
                DgvHtyhd.Columns("仕入原価").DefaultCellStyle.Format = "N2"

                DgvHtyhd.Columns("仕入金額_外貨").DefaultCellStyle.Format = "N2"
                DgvHtyhd.Columns("仕入金額").DefaultCellStyle.Format = "N2"


                '発注基本を取得
                Sql = "Select count(*) As 件数"
                Sql += " from Public.t20_hattyu t20 "
                Sql += " left join Public.t21_hattyu t21"
                Sql += "  On (t20.発注番号 = t21.発注番号 And t20.発注番号枝番 = t21.発注番号枝番)"

                Sql += " WHERE "
                Sql += " t20.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"

                Sql += viewSearchConditions() '抽出条件取得

                ds = _db.selectDB(Sql, RS, reccnt)

                If ds.Tables(RS).Rows(0)("件数") = 0 Then
                    Exit Sub
                End If

                '発注基本を取得
                Sql = "SELECT"
                Sql += " t20.発注番号,t20.発注番号枝番,t20.発注日,t20.取消区分"
                Sql += ",t20.得意先名,t20.客先番号,t20.仕入先コード,t20.仕入先名,t20.仕入先郵便番号,t20.仕入先住所"
                Sql += ",t20.仕入先電話番号,t20.仕入先ＦＡＸ,t20.仕入先担当者名,t20.仕入先担当者役職"
                Sql += ",t20.仕入金額_外貨,t20.仕入金額,t20.支払条件,t20.営業担当者,t20.入力担当者,t20.備考"
                Sql += ",t20.登録日,t20.更新日,t20.通貨"

                'Sql += ",sum(t21.仕入金額 + t21.間接費) as 仕入金額合計, sum(t21.仕入金額_外貨) as 仕入金額合計_外貨 "

                Sql += " FROM "
                Sql += " public.t20_hattyu t20 "
                Sql += " left join public.t21_hattyu t21"
                Sql += "  on (t20.発注番号 = t21.発注番号 and t20.発注番号枝番 = t21.発注番号枝番)"

                Sql += " WHERE "
                Sql += " t20.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"

                Sql += viewSearchConditions() '抽出条件取得


                Sql += " GROUP BY "
                Sql += " t20.発注番号,t20.発注番号枝番,t20.発注日,t20.取消区分"
                Sql += ",t20.得意先名,t20.客先番号,t20.仕入先コード,t20.仕入先名,t20.仕入先郵便番号,t20.仕入先住所"
                Sql += ",t20.仕入先電話番号,t20.仕入先ＦＡＸ,t20.仕入先担当者名,t20.仕入先担当者役職"
                Sql += ",t20.仕入金額_外貨,t20.仕入金額,t20.支払条件,t20.営業担当者,t20.入力担当者,t20.備考"
                Sql += ",t20.登録日,t20.更新日,t20.通貨"


                Sql += " ORDER BY "
                Sql += "t20.更新日 DESC, t20.発注番号, t20.発注番号枝番"

                ds = _db.selectDB(Sql, RS, reccnt)


                '伝票単位時のセル書式
                DgvHtyhd.Columns("仕入原価_外貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvHtyhd.Columns("仕入原価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvHtyhd.Columns("仕入金額_外貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvHtyhd.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1

                    If IsDBNull(ds.Tables(RS).Rows(i)("通貨")) Then
                        cur = vbNullString
                    Else
                        Sql = " and 採番キー = " & ds.Tables(RS).Rows(i)("通貨")
                        curds = getDsData("m25_currency", Sql)

                        cur = curds.Tables(RS).Rows(0)("通貨コード")
                    End If

                    DgvHtyhd.Rows.Add()
                    DgvHtyhd.Rows(i).Cells("取消").Value = getDelKbnTxt(ds.Tables(RS).Rows(i)("取消区分"))
                    DgvHtyhd.Rows(i).Cells("発注番号").Value = ds.Tables(RS).Rows(i)("発注番号")
                    DgvHtyhd.Rows(i).Cells("発注番号枝番").Value = ds.Tables(RS).Rows(i)("発注番号枝番")
                    DgvHtyhd.Rows(i).Cells("客先番号").Value = ds.Tables(RS).Rows(i)("客先番号")
                    DgvHtyhd.Rows(i).Cells("発注日").Value = ds.Tables(RS).Rows(i)("発注日").ToShortDateString
                    DgvHtyhd.Rows(i).Cells("得意先名").Value = ds.Tables(RS).Rows(i)("得意先名")
                    DgvHtyhd.Rows(i).Cells("仕入先コード").Value = ds.Tables(RS).Rows(i)("仕入先コード")
                    DgvHtyhd.Rows(i).Cells("仕入先名").Value = ds.Tables(RS).Rows(i)("仕入先名")

                    DgvHtyhd.Rows(i).Cells("通貨_外貨").Value = cur
                    'DgvHtyhd.Rows(i).Cells("通貨").Value = strBaseCur  '基準通貨

                    'DgvHtyhd.Rows(i).Cells("仕入原価_外貨").Value = rmNullDecimal(ds.Tables(RS).Rows(i)("仕入値合計_外貨")) * rmNullDecimal(ds.Tables(RS).Rows(i)("発注数量"))
                    'DgvHtyhd.Rows(i).Cells("仕入原価").Value = rmNullDecimal(ds.Tables(RS).Rows(i)("仕入値合計")) * rmNullDecimal(ds.Tables(RS).Rows(i)("発注数量"))
                    'DgvHtyhd.Rows(i).Cells("仕入金額_外貨").Value = ds.Tables(RS).Rows(i)("仕入金額_外貨")
                    'DgvHtyhd.Rows(i).Cells("仕入金額").Value = ds.Tables(RS).Rows(i)("仕入金額")

                    Dim decPurchase1 As Decimal = 0
                    Dim decPurchase2 As Decimal = 0
                    Dim decPurchaseAmount1 As Decimal = 0
                    Dim decPurchaseAmount2 As Decimal = 0

                    Call mPurchaseCost(ds.Tables(RS).Rows(i)("発注番号"), ds.Tables(RS).Rows(i)("発注番号枝番") _
                                       , decPurchase1, decPurchase2 _
                                       , decPurchaseAmount1, decPurchaseAmount2)

                    DgvHtyhd.Rows(i).Cells("仕入原価_外貨").Value = decPurchase1
                    DgvHtyhd.Rows(i).Cells("仕入原価").Value = decPurchase2
                    DgvHtyhd.Rows(i).Cells("仕入金額_外貨").Value = decPurchaseAmount1
                    DgvHtyhd.Rows(i).Cells("仕入金額").Value = decPurchaseAmount2

                    DgvHtyhd.Rows(i).Cells("仕入先郵便番号").Value = ds.Tables(RS).Rows(i)("仕入先郵便番号")
                    DgvHtyhd.Rows(i).Cells("仕入先住所").Value = ds.Tables(RS).Rows(i)("仕入先住所")
                    DgvHtyhd.Rows(i).Cells("仕入先電話番号").Value = ds.Tables(RS).Rows(i)("仕入先電話番号")
                    DgvHtyhd.Rows(i).Cells("仕入先ＦＡＸ").Value = ds.Tables(RS).Rows(i)("仕入先ＦＡＸ")
                    DgvHtyhd.Rows(i).Cells("仕入先担当者名").Value = ds.Tables(RS).Rows(i)("仕入先担当者名")
                    DgvHtyhd.Rows(i).Cells("仕入先担当者役職").Value = ds.Tables(RS).Rows(i)("仕入先担当者役職")

                    DgvHtyhd.Rows(i).Cells("支払条件").Value = ds.Tables(RS).Rows(i)("支払条件")
                    DgvHtyhd.Rows(i).Cells("営業担当者").Value = ds.Tables(RS).Rows(i)("営業担当者")
                    DgvHtyhd.Rows(i).Cells("入力担当者").Value = ds.Tables(RS).Rows(i)("入力担当者")
                    DgvHtyhd.Rows(i).Cells("備考").Value = ds.Tables(RS).Rows(i)("備考")
                    DgvHtyhd.Rows(i).Cells("登録日").Value = ds.Tables(RS).Rows(i)("登録日")
                    DgvHtyhd.Rows(i).Cells("更新日").Value = ds.Tables(RS).Rows(i)("更新日")
                Next

            Else '明細単位

                '発注基本を取得

                '抽出条件
                Dim supplierName As String = escapeSql(TxtSupplierName.Text)
                Dim supplierAddress As String = escapeSql(TxtAddress.Text)
                Dim supplierTel As String = escapeSql(TxtTel.Text)
                Dim supplierCode As String = escapeSql(TxtSupplierCode.Text)
                Dim sinceDate As String = UtilClass.strFormatDate(dtPurchaseDateSince.Text) '日付の書式を日本の形式に合わせる
                Dim untilDate As String = UtilClass.strFormatDate(dtPurchaseDateUntil.Text) '日付の書式を日本の形式に合わせる
                Dim sinceNum As String = escapeSql(TxtPurchaseSince.Text)
                'Dim untilNum As String = escapeSql(TxtPurchaseUntil.Text)
                Dim salesName As String = escapeSql(TxtSales.Text)
                Dim poCode As String = escapeSql(TxtCustomerPO.Text)


                Sql = "SELECT"
                Sql += " t21.*, t20.取消区分"
                Sql += " FROM "
                Sql += " public.t21_hattyu t21 "

                Sql += " INNER JOIN "
                Sql += " t20_hattyu t20"
                Sql += " ON "
                Sql += " t21.会社コード = t20.会社コード "
                Sql += " AND "
                Sql += " t21.発注番号 = t20.発注番号"
                Sql += " AND "
                Sql += " t21.発注番号枝番 = t20.発注番号枝番"

                Sql += " WHERE "
                Sql += " t21.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"

                Sql += viewSearchConditions() '抽出条件取得

                Sql += " ORDER BY "
                Sql += "t21.更新日 DESC, t21.発注番号, t21.発注番号枝番, t21.行番号"


                '得意先と一致する入金明細を取得
                ds = _db.selectDB(Sql, RS, reccnt)

                If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                    DgvHtyhd.Columns.Add("取消", "Cancel")
                    DgvHtyhd.Columns.Add("発注番号", "PurchaseNumber")
                    DgvHtyhd.Columns.Add("発注番号枝番", "PurchaseOrderVer.")
                    DgvHtyhd.Columns.Add("行番号", "LineNumber")
                    DgvHtyhd.Columns.Add("仕入区分", "PurchaseClassification")
                    DgvHtyhd.Columns.Add("メーカー", "Manufacturer")
                    DgvHtyhd.Columns.Add("品名", "ItemName")
                    DgvHtyhd.Columns.Add("型式", "Spec")
                    DgvHtyhd.Columns.Add("仕入先名", "SupplierName")
                    DgvHtyhd.Columns.Add("仕入値", "PurchaseAmount")
                    DgvHtyhd.Columns.Add("発注数量", "OrderQuantity")
                    DgvHtyhd.Columns.Add("仕入数量", "PurchasedQuantity")
                    DgvHtyhd.Columns.Add("発注残数", "NumberOfOrderRemaining ")
                    DgvHtyhd.Columns.Add("単位", "Unit")
                    DgvHtyhd.Columns.Add("間接費", "OverHead")
                    DgvHtyhd.Columns.Add("仕入金額", "PurchaseAmount")
                    DgvHtyhd.Columns.Add("リードタイム", "LeadTime")
                    DgvHtyhd.Columns.Add("貿易条件", "TradeTerms")
                    'DgvHtyhd.Columns.Add("入庫数", "GoodsReceiptQuantity")
                    'DgvHtyhd.Columns.Add("未入庫数", "NoGoodsReceiptQuantity")
                    DgvHtyhd.Columns.Add("備考", "Remarks")
                    DgvHtyhd.Columns.Add("更新者", "ModifiedBy")
                    DgvHtyhd.Columns.Add("登録日", "RegistrationDate")
                    DgvHtyhd.Columns.Add("更新日", "LastUpdateDate")
                Else
                    DgvHtyhd.Columns.Add("取消", "取消")
                    DgvHtyhd.Columns.Add("発注番号", "発注番号")
                    DgvHtyhd.Columns.Add("発注番号枝番", "発注Ver.")
                    DgvHtyhd.Columns.Add("行番号", "行No")
                    DgvHtyhd.Columns.Add("仕入区分", "仕入区分")
                    DgvHtyhd.Columns.Add("メーカー", "メーカー")
                    DgvHtyhd.Columns.Add("品名", "品名")
                    DgvHtyhd.Columns.Add("型式", "型式")
                    DgvHtyhd.Columns.Add("仕入先名", "仕入先名")
                    DgvHtyhd.Columns.Add("仕入値", "仕入値")
                    DgvHtyhd.Columns.Add("発注数量", "発注数量")
                    DgvHtyhd.Columns.Add("仕入数量", "仕入数量")
                    DgvHtyhd.Columns.Add("発注残数", "発注残数")
                    DgvHtyhd.Columns.Add("単位", "単位")
                    DgvHtyhd.Columns.Add("間接費", "間接費")
                    DgvHtyhd.Columns.Add("仕入金額", "仕入金額")
                    DgvHtyhd.Columns.Add("リードタイム", "リードタイム")
                    DgvHtyhd.Columns.Add("貿易条件", "貿易条件")
                    'DgvHtyhd.Columns.Add("入庫数", "入庫数")
                    'DgvHtyhd.Columns.Add("未入庫数", "未入庫数")
                    DgvHtyhd.Columns.Add("備考", "備考")
                    DgvHtyhd.Columns.Add("更新者", "更新者")
                    DgvHtyhd.Columns.Add("登録日", "登録日")
                    DgvHtyhd.Columns.Add("更新日", "最終更新日")
                End If

                '伝票単位時のセル書式
                DgvHtyhd.Columns("仕入値").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvHtyhd.Columns("発注数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvHtyhd.Columns("仕入数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvHtyhd.Columns("発注残数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvHtyhd.Columns("間接費").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvHtyhd.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                'DgvHtyhd.Columns("入庫数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                'DgvHtyhd.Columns("未入庫数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


                '数字形式
                DgvHtyhd.Columns("仕入値").DefaultCellStyle.Format = "N2"
                DgvHtyhd.Columns("発注数量").DefaultCellStyle.Format = "N2"
                DgvHtyhd.Columns("仕入数量").DefaultCellStyle.Format = "N2"
                DgvHtyhd.Columns("発注残数").DefaultCellStyle.Format = "N2"
                DgvHtyhd.Columns("間接費").DefaultCellStyle.Format = "N2"
                DgvHtyhd.Columns("仕入金額").DefaultCellStyle.Format = "N2"
                'DgvHtyhd.Columns("入庫数").DefaultCellStyle.Format = "N2"
                'DgvHtyhd.Columns("未入庫数").DefaultCellStyle.Format = "N2"


                Dim dsHanyou As DataSet

                '発注明細ぶん回し
                For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1

                    DgvHtyhd.Rows.Add()
                    DgvHtyhd.Rows(i).Cells("取消").Value = getDelKbnTxt(ds.Tables(RS).Rows(i)("取消区分"))
                    DgvHtyhd.Rows(i).Cells("発注番号").Value = ds.Tables(RS).Rows(i)("発注番号")
                    DgvHtyhd.Rows(i).Cells("発注番号枝番").Value = ds.Tables(RS).Rows(i)("発注番号枝番")
                    DgvHtyhd.Rows(i).Cells("行番号").Value = ds.Tables(RS).Rows(i)("行番号")

                    'リードタイムのリストを汎用マスタから取得
                    dsHanyou = getDsHanyoData(CommonConst.FIXED_KEY_PURCHASING_CLASS, ds.Tables(RS).Rows(i)("仕入区分"))
                    If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                        DgvHtyhd.Rows(i).Cells("仕入区分").Value = dsHanyou.Tables(RS).Rows(0)("文字２")
                    Else
                        DgvHtyhd.Rows(i).Cells("仕入区分").Value = dsHanyou.Tables(RS).Rows(0)("文字１")
                    End If

                    DgvHtyhd.Rows(i).Cells("メーカー").Value = ds.Tables(RS).Rows(i)("メーカー")
                    DgvHtyhd.Rows(i).Cells("品名").Value = ds.Tables(RS).Rows(i)("品名")
                    DgvHtyhd.Rows(i).Cells("型式").Value = ds.Tables(RS).Rows(i)("型式")
                    DgvHtyhd.Rows(i).Cells("仕入先名").Value = ds.Tables(RS).Rows(i)("仕入先名")
                    DgvHtyhd.Rows(i).Cells("仕入値").Value = ds.Tables(RS).Rows(i)("仕入値")
                    DgvHtyhd.Rows(i).Cells("発注数量").Value = ds.Tables(RS).Rows(i)("発注数量")
                    DgvHtyhd.Rows(i).Cells("仕入数量").Value = ds.Tables(RS).Rows(i)("仕入数量")
                    DgvHtyhd.Rows(i).Cells("発注残数").Value = ds.Tables(RS).Rows(i)("発注残数")
                    DgvHtyhd.Rows(i).Cells("単位").Value = ds.Tables(RS).Rows(i)("単位")
                    DgvHtyhd.Rows(i).Cells("間接費").Value = ds.Tables(RS).Rows(i)("間接費")
                    DgvHtyhd.Rows(i).Cells("仕入金額").Value = ds.Tables(RS).Rows(i)("仕入金額")
                    If ds.Tables(RS).Rows(i)("リードタイム") Is "" Then
                        DgvHtyhd.Rows(i).Cells("リードタイム").Value = ""
                    Else

                        'リードタイムのリストを汎用マスタから取得
                        dsHanyou = getDsHanyoData(CommonConst.FIXED_KEY_READTIME, ds.Tables(RS).Rows(i)("リードタイム単位").ToString)
                        DgvHtyhd.Rows(i).Cells("リードタイム").Value = ds.Tables(RS).Rows(i)("リードタイム") & dsHanyou.Tables(RS).Rows(0)("文字１").ToString

                    End If

                    If ds.Tables(RS).Rows(i)("貿易条件") IsNot DBNull.Value Then

                        '貿易条件のリストを汎用マスタから取得
                        dsHanyou = getDsHanyoData(CommonConst.FIXED_KEY_TRADE_TERMS, ds.Tables(RS).Rows(i)("貿易条件").ToString)
                        DgvHtyhd.Rows(i).Cells("貿易条件").Value = dsHanyou.Tables(RS).Rows(0)("文字１").ToString
                    End If
                    'DgvHtyhd.Rows(i).Cells("入庫数").Value = ds.Tables(RS).Rows(i)("入庫数")
                    'DgvHtyhd.Rows(i).Cells("未入庫数").Value = ds.Tables(RS).Rows(i)("未入庫数")
                    DgvHtyhd.Rows(i).Cells("備考").Value = ds.Tables(RS).Rows(i)("備考")
                    DgvHtyhd.Rows(i).Cells("更新者").Value = ds.Tables(RS).Rows(i)("更新者")
                    DgvHtyhd.Rows(i).Cells("登録日").Value = ds.Tables(RS).Rows(i)("登録日")
                    DgvHtyhd.Rows(i).Cells("更新日").Value = ds.Tables(RS).Rows(i)("更新日")
                Next

            End If

            'カラム幅をユーザーが変更可能にする。
            DgvHtyhd.AllowUserToResizeColumns = True

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    Private Sub mPurchaseCost(ByVal OrderNo As String, ByVal BranchNo As String _
                                   , ByRef decPurchase1 As Decimal, ByRef decPurchase2 As Decimal _
                                   , ByRef decPurchaseAmount1 As Decimal, ByRef decPurchaseAmount2 As Decimal)


        Dim reccnt As Integer = 0 'DB用（デフォルト
        Dim Sql As String
        Dim ds_t21 As DataSet

        Sql = "SELECT"
        Sql += " 仕入値,仕入値_外貨,発注数量,仕入金額,間接費,仕入金額_外貨"

        Sql += " FROM "
        Sql += " public.t21_hattyu"

        Sql += " WHERE "
        Sql += " 会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"

        Sql += " and 発注番号 = '" & OrderNo & "'"
        Sql += " and 発注番号枝番 = '" & BranchNo & "'"

        ds_t21 = _db.selectDB(Sql, RS, reccnt)

        decPurchase1 = 0  '外貨
        decPurchase2 = 0
        decPurchaseAmount1 = 0
        decPurchaseAmount2 = 0

        For i As Integer = 0 To ds_t21.Tables(RS).Rows.Count - 1

            decPurchase1 += rmNullDecimal(ds_t21.Tables(RS).Rows(i)("仕入値_外貨") * ds_t21.Tables(RS).Rows(i)("発注数量"))
            decPurchase2 += rmNullDecimal(ds_t21.Tables(RS).Rows(i)("仕入値") * ds_t21.Tables(RS).Rows(i)("発注数量"))

            decPurchaseAmount1 += rmNullDecimal(ds_t21.Tables(RS).Rows(i)("仕入金額_外貨"))
            decPurchaseAmount2 += rmNullDecimal(ds_t21.Tables(RS).Rows(i)("仕入金額") + ds_t21.Tables(RS).Rows(i)("間接費"))

        Next

    End Sub
    '
    'NothingをDecimalに置換
    Private Function rmNullDecimal(ByVal prmField As Object) As Decimal
        If prmField Is Nothing Then
            rmNullDecimal = 0
            Exit Function
        End If
        If prmField Is DBNull.Value Then
            rmNullDecimal = 0
            Exit Function
        End If

        If Not IsNumeric(prmField) Then
            rmNullDecimal = 0
            Exit Function
        End If

        rmNullDecimal = prmField

    End Function
    '検索ボタン押下時
    Private Sub BtnPurchaseSearch_Click(sender As Object, e As EventArgs) Handles BtnPurchaseSearch.Click
        '一覧再表示
        getList()
    End Sub

    '取消データチェックイベント
    Private Sub ChkCancelData_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCancelData.CheckedChanged
        '一覧再表示
        getList()
    End Sub

    'ラジオボタン変更時
    Private Sub RbtnDetails_CheckedChanged(sender As Object, e As EventArgs) Handles RbtnDetails.CheckedChanged
        '一覧再表示
        getList()
    End Sub

    '発注参照ボタン押下時
    Private Sub BtnPurchaseView_Click(sender As Object, e As EventArgs) Handles BtnPurchaseView.Click

        '明細表示時、または対象データがない場合は取消操作不可能
        If DgvHtyhd.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Return

        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvHtyhd.CurrentCell.RowIndex
        Dim No As String = DgvHtyhd.Rows(RowIdx).Cells("発注番号").Value
        Dim Suffix As String = DgvHtyhd.Rows(RowIdx).Cells("発注番号枝番").Value
        Dim Status As String = CommonConst.STATUS_VIEW
        Dim openForm As Form = Nothing
        openForm = New Ordering(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
        openForm.Show(Me)
    End Sub

    '発注複製ボタン押下時
    Private Sub BtnPurchaseClone_Click(sender As Object, e As EventArgs) Handles BtnPurchaseClone.Click
        '明細表示時、または対象データがない場合は取消操作不可能
        If DgvHtyhd.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Return

        End If


        Dim RowIdx As Integer
        RowIdx = Me.DgvHtyhd.CurrentCell.RowIndex
        Dim No As String = DgvHtyhd.Rows(RowIdx).Cells("発注番号").Value
        Dim Suffix As String = DgvHtyhd.Rows(RowIdx).Cells("発注番号枝番").Value
        Dim Status As String = CommonConst.STATUS_CLONE
        Dim openForm As Form = Nothing
        openForm = New Ordering(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
        openForm.ShowDialog(Me)

        '一覧再表示
        getList()

    End Sub

    '発注修正ボタン押下時
    Private Sub BtnPurchaseeEdit_Click(sender As Object, e As EventArgs) Handles BtnPurchaseEdit.Click

        '明細表示時、または対象データがない場合は取消操作不可能
        If DgvHtyhd.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Return

        End If

        '取消済みデータは取消操作不可能
        If DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("取消").Value = CommonConst.CANCEL_KBN_DISABLED_TXT Then
            '取消データは選択できないアラートを出す
            _msgHd.dspMSG("cannotSelectTorikeshiData", frmC01F10_Login.loginValue.Language)
            Return
        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvHtyhd.CurrentCell.RowIndex
        Dim No As String = DgvHtyhd.Rows(RowIdx).Cells("発注番号").Value
        Dim Suffix As String = DgvHtyhd.Rows(RowIdx).Cells("発注番号枝番").Value
        Dim status As String = CommonConst.STATUS_EDIT

        Dim openForm As Form = Nothing
        openForm = New Ordering(_msgHd, _db, _langHd, Me, No, Suffix, status)   '処理選択
        openForm.ShowDialog(Me)

        '一覧再表示
        getList()
    End Sub

    '発注取消ボタン押下時
    Private Sub BtnPurchaseCancel_Click(sender As Object, e As EventArgs) Handles BtnPurchaseCancel.Click

        '明細表示時、または対象データがない場合は取消操作不可能
        If RbtnDetails.Checked Or DgvHtyhd.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Return

        End If

        Dim dtNow As String = FormatDateTime(DateTime.Now)
        Dim Sql As String = ""

        '取消済みデータは取消操作不可能
        If DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("取消").Value = CommonConst.CANCEL_KBN_DISABLED_TXT Then
            '取消データは選択できないアラートを出す
            _msgHd.dspMSG("cannotSelectTorikeshiData", frmC01F10_Login.loginValue.Language)
            Return
        End If

        Try
            Sql = "UPDATE "
            Sql += "Public."
            Sql += "t20_hattyu "
            Sql += "SET "

            Sql += "取消区分 = " & CommonConst.CANCEL_KBN_DISABLED
            Sql += ", 取消日 = current_date"
            Sql += ", 更新日 = current_timestamp"
            Sql += ", 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "' "

            Sql += "WHERE"
            Sql += " 会社コード"
            Sql += "='"
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "'"
            Sql += " AND"
            Sql += " 発注番号"
            Sql += "='"
            Sql += DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("発注番号").Value
            Sql += "' "
            Sql += " AND"
            Sql += " 発注番号枝番"
            Sql += "='"
            Sql += DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("発注番号枝番").Value
            Sql += "' "


            '取消確認のアラート
            Dim result As DialogResult = _msgHd.dspMSG("confirmCancel", frmC01F10_Login.loginValue.Language)

            If result = DialogResult.Yes Then
                _db.executeDB(Sql)
                getList() 'データ更新
            End If

        Catch ex As Exception

            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))

        End Try

    End Sub

    '仕入入力ボタン押下時
    Private Sub BtnOrding_Click(sender As Object, e As EventArgs) Handles BtnOrding.Click

        '明細表示時、または対象データがない場合は取消操作不可能
        If DgvHtyhd.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Return

        End If

        '取消済みデータは取消操作不可能
        If DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("取消").Value = CommonConst.CANCEL_KBN_DISABLED_TXT Then
            '取消データは選択できないアラートを出す
            _msgHd.dspMSG("cannotSelectTorikeshiData", frmC01F10_Login.loginValue.Language)
            Return
        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvHtyhd.CurrentCell.RowIndex
        Dim No As String = DgvHtyhd.Rows(RowIdx).Cells("発注番号").Value
        Dim Suffix As String = DgvHtyhd.Rows(RowIdx).Cells("発注番号枝番").Value
        Dim openForm As Form = Nothing
        openForm = New PurchasingManagement(_msgHd, _db, _langHd, No, Suffix)   '処理選択
        openForm.Show(Me)

    End Sub

    '入庫入力ボタン押下時
    Private Sub BtnGoodsIssue_Click(sender As Object, e As EventArgs) Handles BtnReceipt.Click

        '明細表示時、または対象データがない場合は取消操作不可能
        If DgvHtyhd.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Return

        End If

        '取消済みデータは取消操作不可能
        If DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("取消").Value = CommonConst.CANCEL_KBN_DISABLED_TXT Then
            '取消データは選択できないアラートを出す
            _msgHd.dspMSG("cannotSelectTorikeshiData", frmC01F10_Login.loginValue.Language)
            Return
        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvHtyhd.CurrentCell.RowIndex
        Dim No As String = DgvHtyhd.Rows(RowIdx).Cells("発注番号").Value
        Dim Suffix As String = DgvHtyhd.Rows(RowIdx).Cells("発注番号枝番").Value
        Dim openForm As Form = Nothing
        openForm = New Receipt(_msgHd, _db, _langHd, No, Suffix)   '処理選択
        openForm.Show(Me)

    End Sub

    '買掛登録ボタン押下時
    Private Sub BtnAP_Click(sender As Object, e As EventArgs) Handles BtnAP.Click

        '明細表示時、または対象データがない場合は取消操作不可能
        If DgvHtyhd.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Return

        End If

        '取消済みデータは取消操作不可能
        If DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("取消").Value = CommonConst.CANCEL_KBN_DISABLED_TXT Then
            '取消データは選択できないアラートを出す
            _msgHd.dspMSG("cannotSelectTorikeshiData", frmC01F10_Login.loginValue.Language)
            Return
        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvHtyhd.CurrentCell.RowIndex
        Dim No As String = DgvHtyhd.Rows(RowIdx).Cells("発注番号").Value
        Dim Suffix As String = DgvHtyhd.Rows(RowIdx).Cells("発注番号枝番").Value
        Dim openForm As Form = Nothing
        openForm = New AccountsPayable(_msgHd, _db, _langHd, Me, No, Suffix)   '処理選択
        Me.Enabled = False
        Me.Hide()
        openForm.ShowDialog(Me)

        '一覧再表示
        getList()

    End Sub

    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    '抽出条件取得
    Private Function viewSearchConditions() As String
        Dim Sql As String = ""

        '抽出条件
        Dim supplierName As String = escapeSql(TxtSupplierName.Text)
        Dim supplierAddress As String = escapeSql(TxtAddress.Text)
        Dim supplierTel As String = escapeSql(TxtTel.Text)
        Dim supplierCode As String = escapeSql(TxtSupplierCode.Text)
        Dim sinceDate As String = UtilClass.strFormatDate(dtPurchaseDateSince.Text) '日付の書式を日本の形式に合わせる
        Dim untilDate As String = UtilClass.strFormatDate(dtPurchaseDateUntil.Text) '日付の書式を日本の形式に合わせる
        Dim sinceNum As String = escapeSql(TxtPurchaseSince.Text)
        Dim salesName As String = escapeSql(TxtSales.Text)
        Dim poCode As String = escapeSql(TxtCustomerPO.Text)
        Dim itemName As String = UtilClass.escapeSql(TxtItemName.Text)
        Dim spec As String = UtilClass.escapeSql(TxtSpec.Text)
        Dim Maker As String = UtilClass.escapeSql(txtMaker.Text)

        If supplierName <> Nothing Then
            Sql += " AND "
            Sql += " t20.仕入先名 ILIKE '%" & supplierName & "%' "
        End If

        If supplierAddress <> Nothing Then
            Sql += " AND "
            Sql += " t20.仕入先住所 ILIKE '%" & supplierAddress & "%' "
        End If

        If supplierTel <> Nothing Then
            Sql += " AND "
            Sql += " t20.仕入先電話番号 ILIKE '%" & supplierTel & "%' "
        End If

        If supplierCode <> Nothing Then
            Sql += " AND "
            Sql += " t20.仕入先コード ILIKE '%" & supplierCode & "%' "
        End If

        If sinceDate <> Nothing Then
            Sql += " AND "
            Sql += " t20.発注日 >= '" & sinceDate & "'"
        End If
        If untilDate <> Nothing Then
            Sql += " AND "
            Sql += " t20.発注日 <= '" & untilDate & "'"
        End If

        If sinceNum <> Nothing Then
            Sql += " AND "
            Sql += " t20.発注番号 ILIKE '%" & sinceNum & "%' "
        End If

        If salesName <> Nothing Then
            Sql += " AND "
            Sql += " t20.営業担当者 ILIKE '%" & salesName & "%' "
        End If

        If poCode <> Nothing Then
            Sql += " AND "
            Sql += " t20.客先番号 ILIKE '%" & poCode & "%' "
        End If

        If Maker <> Nothing Then
            Sql += " AND "
            Sql += " t21.メーカー ILIKE '%" & Maker & "%' "
        End If

        If itemName <> Nothing Then
            Sql += " AND "
            Sql += " t21.品名 ILIKE '%" & itemName & "%' "
        End If

        If spec <> Nothing Then
            Sql += " AND "
            Sql += " t21.型式 ILIKE '%" & spec & "%' "
        End If

        '取消データを含めない場合
        If ChkCancelData.Checked = False Then
            Sql += " AND "
            Sql += "t20.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
        End If

        Return Sql

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

    '汎用マスタから固定キー、可変キーに応じた結果を返す
    'param1：String 固定キー
    'param2：String 可変キー
    'Return: DataSet
    Private Function getDsHanyoData(ByVal prmFixed As String, ByVal prmVariable As String) As DataSet
        Dim Sql As String = ""

        Sql = " AND "
        Sql += "固定キー ILIKE '" & prmFixed & "'"
        Sql += " AND "
        Sql += "可変キー ILIKE '" & prmVariable & "'"

        'リードタイムのリストを汎用マスタから取得
        Return getDsData("m90_hanyo", Sql)

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

    Private Sub OrderingList_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        '一覧再表示
        getList()
    End Sub

    '基準通貨の取得
    Private Function setBaseCurrency() As String
        Dim Sql As String
        '通貨表示：ベースの設定
        Sql = " AND 採番キー = " & CommonConst.CURRENCY_CD_IDR.ToString
        Sql += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString

        Dim ds As DataSet = getDsData("m25_currency", Sql)
        setBaseCurrency = ds.Tables(RS).Rows(0)("通貨コード")

    End Function

End Class