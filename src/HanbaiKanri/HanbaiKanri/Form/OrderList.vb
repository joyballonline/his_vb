'2020.01.09 ロケ番号→出庫開始サインに名称変更

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

Public Class OrderList
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
    Private OrderStatus As String = ""


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
        OrderStatus = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True
        DgvCymnhd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells

        '受注参照以外では隠す
        LblItemName.Visible = False
        TxtItemName.Visible = False
        LblSpec.Visible = False
        TxtSpec.Visible = False

    End Sub

    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If OrderStatus = CommonConst.STATUS_SALES Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "SalesInputMode"
            Else
                LblMode.Text = "売上入力モード"
            End If

            BtnSales.Visible = True
            BtnSales.Location = New Point(997, 509)

            lblPurchaseSince.Visible = True
            TxtPurchaseSince.Visible = True

            'メーカー
            lblMaker.Location = New Point(584, 150)
            txtMaker.Location = New Point(760, 150)

            '客先番号
            lblCustomerPO.Location = New Point(584, 121)
            TxtCustomerPO.Location = New Point(760, 121)

            '発注番号
            lblPurchaseSince.Location = New Point(584, 93)
            TxtPurchaseSince.Location = New Point(760, 93)
            TxtPurchaseSince.TabIndex = 10

        ElseIf OrderStatus = CommonConst.STATUS_GOODS_ISSUE Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "GoodsDeliveryInputMode"
            Else
                LblMode.Text = "出庫入力モード"
            End If

            BtnGoodsIssue.Visible = True
            BtnGoodsIssue.Location = New Point(997, 509)

            lblPurchaseSince.Visible = True
            TxtPurchaseSince.Visible = True

            'メーカー
            lblMaker.Location = New Point(584, 150)
            txtMaker.Location = New Point(760, 150)

            '客先番号
            lblCustomerPO.Location = New Point(584, 121)
            TxtCustomerPO.Location = New Point(760, 121)

            '発注番号
            lblPurchaseSince.Location = New Point(584, 93)
            TxtPurchaseSince.Location = New Point(760, 93)
            TxtPurchaseSince.TabIndex = 10

        ElseIf OrderStatus = CommonConst.STATUS_EDIT Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "EditMode"
            Else
                LblMode.Text = "編集モード"
            End If

            BtnOrderEdit.Visible = True
            BtnOrderEdit.Location = New Point(997, 509)
        ElseIf OrderStatus = CommonConst.STATUS_VIEW Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If

            BtnOrderView.Visible = True
            BtnOrderView.Location = New Point(997, 509)

            LblItemName.Visible = True
            TxtItemName.Visible = True
            LblSpec.Visible = True
            TxtSpec.Visible = True

        ElseIf OrderStatus = CommonConst.STATUS_CANCEL Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "CancelMode"
            Else
                LblMode.Text = "取消モード"
            End If

            BtnOrderCancel.Visible = True
            BtnOrderCancel.Location = New Point(997, 509)
        ElseIf OrderStatus = CommonConst.STATUS_CLONE Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "NewCopyMode"
            Else
                LblMode.Text = "新規複写モード"
            End If

            BtnOrderClone.Visible = True
            BtnOrderClone.Location = New Point(997, 509)
        ElseIf OrderStatus = CommonConst.STATUS_BILL Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "BillingRegistrationMode"
            Else
                LblMode.Text = "請求登録モード"
            End If

            BtnBill.Visible = True
            BtnBill.Location = New Point(997, 509)
        End If

        '検索（Date）の初期値
        dtOrderDateSince.Value = DateAdd("d", CommonConst.SINCE_DEFAULT_DAY, DateTime.Today)
        dtOrderDateUntil.Value = DateTime.Today

        OrderListLoad() '一覧表示

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblConditions.Text = "ExtractionCondition"
            Label1.Text = "CustomerName"
            Label2.Text = "Address"
            Label3.Text = "PhoneNumber"
            Label4.Text = "CustomerCode"
            Label8.Text = "OrderDate"
            Label7.Text = "OrdernNumber"
            Label6.Text = "SalesPersonInCharge"
            lblCustomerPO.Text = "CustomerNumber"
            Label10.Text = "DisplayFormat"
            RbtnSlip.Text = "UnitOfVoucher"
            lblMaker.Text = "Maker"
            lblPurchaseSince.Text = "PurchaseOrderNo"

            RbtnDetails.Text = "LineItemUnit"
            RbtnDetails.Location = New Point(166, 202)

            ChkCancelData.Text = "IncludeCancelData"
            ChkCancelData.Location = New Point(556, 203)

            BtnOrderSearch.Text = "Search"
            BtnBill.Text = "BillingRegistration"
            BtnOrderCancel.Text = "CancelOfOrder"
            BtnSales.Text = "SalesRagistration"
            BtnGoodsIssue.Text = "GoodsIssueRegistration"
            BtnOrderClone.Text = "OrderCopy"
            BtnOrderView.Text = "OrderView"
            BtnOrderEdit.Text = "OrderEdit"
            BtnBack.Text = "Back"


            '受注参照時のみ表示される
            LblItemName.Text = "ItemName"
            LblSpec.Text = "Spec"
        End If
    End Sub

    '受注一覧を表示
    Private Sub OrderListLoad(Optional ByRef prmRefStatus As String = "")

        DgvCymnhd.Visible = False

        ' 行や列を追加したり、セルに値を設定するときは、自動サイズ設定しない。
        DgvCymnhd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        DgvCymnhd.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None
        DgvCymnhd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

        '一覧をクリア
        DgvCymnhd.Rows.Clear()
        DgvCymnhd.Columns.Clear()

        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        '抽出条件
        Dim customerName As String = escapeSql(TxtCustomerName.Text)
        Dim customerAddress As String = escapeSql(TxtAddress.Text)
        Dim customerTel As String = escapeSql(TxtTel.Text)
        Dim customerCode As String = escapeSql(TxtCustomerCode.Text)
        Dim sinceDate As String = UtilClass.strFormatDate(dtOrderDateSince.Text)
        Dim untilDate As String = UtilClass.strFormatDate(dtOrderDateUntil.Text)
        Dim sinceNum As String = escapeSql(TxtOrderSince.Text)
        Dim salesName As String = escapeSql(TxtSales.Text)
        Dim customerPO As String = escapeSql(TxtCustomerPO.Text)
        Dim itemName As String = escapeSql(TxtItemName.Text)
        Dim spec As String = escapeSql(TxtSpec.Text)
        Dim Maker As String = UtilClass.escapeSql(txtMaker.Text)
        Dim PurchaseSince As String = escapeSql(TxtPurchaseSince.Text)


        Try

            '受注参照時、品名と型式も検索
            '出庫登録時も一覧では特別な表示制限なし
            If OrderStatus = CommonConst.STATUS_VIEW Or OrderStatus = CommonConst.STATUS_GOODS_ISSUE Then

                '伝票単位の場合
                If RbtnSlip.Checked Then

                    Sql = " SELECT "
                    Sql += " t10.受注番号,t10.受注番号枝番,t10.受注日,t10.取消区分,t10.客先番号"
                    Sql += ",t10.見積番号,t10.見積番号枝番,t10.見積日,t10.見積有効期限"
                    Sql += ",t10.得意先コード,t10.得意先名,t10.得意先郵便番号,t10.得意先住所,t10.得意先電話番号"
                    Sql += ",t10.得意先ＦＡＸ,t10.得意先担当者名,t10.得意先担当者役職"
                    Sql += ",t10.ＶＡＴ,t10.通貨,t10.見積金額_外貨"
                    Sql += ",t10.見積金額,t10.仕入金額,t10.粗利額,t10.支払条件"
                    Sql += ",t10.営業担当者,t10.入力担当者,t10.備考,t10.登録日,t10.更新者,t10.更新日"


                    Sql += " FROM t10_cymnhd t10 "
                    Sql += " left join t11_cymndt t11 "
                    Sql += " on (t10.受注番号 = t11.受注番号 and t10.受注番号枝番 = t11.受注番号枝番)"


                    Sql += " WHERE t10.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' "

                    Sql += viewSearchConditions() '検索条件

                    Sql += " group by "
                    Sql += " t10.受注番号,t10.受注番号枝番,t10.受注日,t10.取消区分,t10.客先番号"
                    Sql += ",t10.見積番号,t10.見積番号枝番,t10.見積日,t10.見積有効期限"
                    Sql += ",t10.得意先コード,t10.得意先名,t10.得意先郵便番号,t10.得意先住所,t10.得意先電話番号"
                    Sql += ",t10.得意先ＦＡＸ,t10.得意先担当者名,t10.得意先担当者役職"
                    Sql += ",t10.ＶＡＴ,t10.通貨,t10.見積金額_外貨"
                    Sql += ",t10.見積金額,t10.仕入金額,t10.粗利額,t10.支払条件"
                    Sql += ",t10.営業担当者,t10.入力担当者,t10.備考,t10.登録日,t10.更新者,t10.更新日"


                    Sql += " ORDER BY t10.受注日 DESC ,t10.受注番号 DESC "

                    ds = _db.selectDB(Sql, RS, reccnt)

                    setRows(ds) '行をセット

                Else
                    '明細単位

                    Sql = "SELECT t11.*, t10.取消区分 "
                    Sql += " FROM t11_cymndt t11 "

                    Sql += " LEFT JOIN  t10_cymnhd t10 "
                    Sql += " ON t11.会社コード = t10.会社コード "
                    Sql += " AND t11.受注番号 = t10.受注番号 "
                    Sql += " AND t11.受注番号枝番 = t10.受注番号枝番 "

                    Sql += " WHERE t10.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' "

                    Sql += viewSearchConditions() '検索条件

                    Sql += " ORDER BY t11.登録日 DESC ,t10.受注番号 DESC"

                    ds = _db.selectDB(Sql, RS, reccnt)

                    setRows(ds) '行をセット

                End If
                DgvCymnhd.Visible = True

                Exit Sub

            End If

            '伝票単位の場合
            If RbtnSlip.Checked Then

                Sql = " SELECT *"

                Sql += " FROM t10_cymnhd t10 "
                'Sql += " left join t20_hattyu t20 "
                'Sql += " on (t10.受注番号 = t20.受注番号 and t10.受注番号枝番 = t20.受注番号枝番)"

                Sql += " WHERE t10.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' "
                Sql += viewSearchConditions() '検索条件

                Sql += " ORDER BY t10.更新日 DESC"

                ds = _db.selectDB(Sql, RS, reccnt)

                setRows(ds) '行をセット

            Else

                '明細単位の場合
                'joinするのでとりあえず直書き
                Sql = "SELECT t11.*, t10.取消区分 FROM  public.t11_cymndt t11 "
                Sql += " INNER JOIN  t10_cymnhd t10"
                Sql += " ON t11.会社コード = t10.会社コード"
                Sql += " AND  t11.受注番号 = t10.受注番号"
                Sql += " AND  t11.受注番号枝番 = t10.受注番号枝番"


                Sql += " WHERE t11.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

                If customerName <> Nothing Then
                    Sql += " AND t10.得意先名 ILIKE '%" & customerName & "%' "
                End If

                If customerAddress <> Nothing Then
                    Sql += " AND t10.得意先住所 ILIKE '%" & customerAddress & "%' "
                End If

                If customerTel <> Nothing Then
                    Sql += " AND t10.得意先電話番号 ILIKE '%" & customerTel & "%' "
                End If

                If customerCode <> Nothing Then
                    Sql += " AND t10.得意先コード ILIKE '%" & customerCode & "%' "
                End If

                If sinceDate <> Nothing Then
                    Sql += " AND t10.受注日 >= '" & sinceDate & "'"
                End If
                If untilDate <> Nothing Then
                    Sql += " AND t10.受注日 <= '" & untilDate & "'"
                End If

                If sinceNum <> Nothing Then
                    Sql += " AND t11.受注番号 >= '" & sinceNum & "' "
                End If

                If salesName <> Nothing Then
                    Sql += " AND t10.営業担当者 ILIKE '%" & salesName & "%' "
                End If

                If customerPO <> Nothing Then
                    Sql += " AND t10.客先番号 ILIKE '%" & customerPO & "%' "
                End If

                If Maker <> Nothing Then
                    Sql += " AND t11.メーカー ILIKE '%" & Maker & "%' "
                End If

                If itemName <> Nothing Then
                    Sql += " AND t11.品名 ILIKE '%" & itemName & "%' "
                End If

                If spec <> Nothing Then
                    Sql += " AND t11.型式 ILIKE '%" & spec & "%' "
                End If

                If PurchaseSince <> Nothing Then
                    Sql += "And t10.受注番号 In (Select 受注番号 From t20_hattyu t20 Where 発注番号 In('" & PurchaseSince & "'))"
                End If


                '取消データを含めない場合
                If ChkCancelData.Checked = False Then
                    Sql += " AND t10.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
                End If

                Sql += " ORDER BY t11.登録日 DESC"

                ds = _db.selectDB(Sql, RS, reccnt)

                setRows(ds) '行をセット

            End If
            '自動でサイズを設定するのは、行や列を追加したり、セルに値を設定した後にする。
            DgvCymnhd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            DgvCymnhd.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            DgvCymnhd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
            DgvCymnhd.Visible = True

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '検索内容をListにセット（行）
    Private Sub setRows(ByVal prmDataset As DataSet)
        Dim ds As DataSet = prmDataset


        '伝票単位の場合
        '------------------------------
        If RbtnSlip.Checked Then

            Dim curds As DataSet  'm25_currency
            Dim cur As String
            Dim Sql As String

            setHdColumns() '表示カラムの設定

            For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1

                If IsDBNull(ds.Tables(RS).Rows(i)("通貨")) Then
                    cur = vbNullString
                Else
                    Sql = " and 採番キー = " & ds.Tables(RS).Rows(i)("通貨")
                    curds = getDsData("m25_currency", Sql)

                    cur = curds.Tables(RS).Rows(0)("通貨コード")
                End If

                DgvCymnhd.Rows.Add()
                DgvCymnhd.Rows(i).Cells("取消").Value = getDelKbnTxt(ds.Tables(RS).Rows(i)("取消区分"))
                DgvCymnhd.Rows(i).Cells("受注番号").Value = ds.Tables(RS).Rows(i)("受注番号")
                DgvCymnhd.Rows(i).Cells("受注番号枝番").Value = ds.Tables(RS).Rows(i)("受注番号枝番")

                DgvCymnhd.Rows(i).Cells("客先番号").Value = ds.Tables(RS).Rows(i)("客先番号")
                DgvCymnhd.Rows(i).Cells("受注日").Value = ds.Tables(RS).Rows(i)("受注日")
                DgvCymnhd.Rows(i).Cells("見積番号").Value = ds.Tables(RS).Rows(i)("見積番号")
                DgvCymnhd.Rows(i).Cells("見積番号枝番").Value = ds.Tables(RS).Rows(i)("見積番号枝番")
                DgvCymnhd.Rows(i).Cells("見積日").Value = ds.Tables(RS).Rows(i)("見積日")
                DgvCymnhd.Rows(i).Cells("見積有効期限").Value = ds.Tables(RS).Rows(i)("見積有効期限")
                DgvCymnhd.Rows(i).Cells("得意先コード").Value = ds.Tables(RS).Rows(i)("得意先コード")
                DgvCymnhd.Rows(i).Cells("得意先名").Value = ds.Tables(RS).Rows(i)("得意先名")


                DgvCymnhd.Rows(i).Cells("通貨_外貨").Value = cur
                DgvCymnhd.Rows(i).Cells("受注金額_外貨").Value = ds.Tables(RS).Rows(i)("見積金額_外貨")

                DgvCymnhd.Rows(i).Cells("受注金額").Value = ds.Tables(RS).Rows(i)("見積金額")
                DgvCymnhd.Rows(i).Cells("ＶＡＴ").Value = ds.Tables(RS).Rows(i)("見積金額") * ds.Tables(RS).Rows(i)("ＶＡＴ") / 100

                DgvCymnhd.Rows(i).Cells("仕入金額").Value = ds.Tables(RS).Rows(i)("仕入金額")
                DgvCymnhd.Rows(i).Cells("粗利額").Value = ds.Tables(RS).Rows(i)("見積金額") - ds.Tables(RS).Rows(i)("仕入金額")

                If ds.Tables(RS).Rows(i)("見積金額") = 0 Or DgvCymnhd.Rows(i).Cells("粗利額").Value = 0 Then
                    DgvCymnhd.Rows(i).Cells("粗利率").Value = 0
                Else
                    DgvCymnhd.Rows(i).Cells("粗利率").Value = DgvCymnhd.Rows(i).Cells("粗利額").Value / ds.Tables(RS).Rows(i)("見積金額") * 100
                End If


                DgvCymnhd.Rows(i).Cells("得意先郵便番号").Value = ds.Tables(RS).Rows(i)("得意先郵便番号")
                DgvCymnhd.Rows(i).Cells("得意先住所").Value = ds.Tables(RS).Rows(i)("得意先住所")
                DgvCymnhd.Rows(i).Cells("得意先電話番号").Value = ds.Tables(RS).Rows(i)("得意先電話番号")
                DgvCymnhd.Rows(i).Cells("得意先ＦＡＸ").Value = ds.Tables(RS).Rows(i)("得意先ＦＡＸ")
                DgvCymnhd.Rows(i).Cells("得意先担当者名").Value = ds.Tables(RS).Rows(i)("得意先担当者名")
                DgvCymnhd.Rows(i).Cells("得意先担当者役職").Value = ds.Tables(RS).Rows(i)("得意先担当者役職")

                DgvCymnhd.Rows(i).Cells("支払条件").Value = ds.Tables(RS).Rows(i)("支払条件")
                DgvCymnhd.Rows(i).Cells("営業担当者").Value = ds.Tables(RS).Rows(i)("営業担当者")
                DgvCymnhd.Rows(i).Cells("入力担当者").Value = ds.Tables(RS).Rows(i)("入力担当者")
                DgvCymnhd.Rows(i).Cells("備考").Value = ds.Tables(RS).Rows(i)("備考")
                DgvCymnhd.Rows(i).Cells("登録日").Value = ds.Tables(RS).Rows(i)("登録日")
                DgvCymnhd.Rows(i).Cells("更新者").Value = ds.Tables(RS).Rows(i)("更新者")
                DgvCymnhd.Rows(i).Cells("更新日").Value = ds.Tables(RS).Rows(i)("更新日")

            Next

        Else

            '明細単位の場合
            '------------------------------
            Dim Sql As String = ""
            Dim tmp1 As String = ""

            setDtColumns() '表示カラムの設定

            For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1

                DgvCymnhd.Rows.Add()
                DgvCymnhd.Rows(i).Cells("取消").Value = getDelKbnTxt(ds.Tables(RS).Rows(i)("取消区分"))
                DgvCymnhd.Rows(i).Cells("受注番号").Value = ds.Tables(RS).Rows(i)("受注番号")
                DgvCymnhd.Rows(i).Cells("受注番号枝番").Value = ds.Tables(RS).Rows(i)("受注番号枝番")
                DgvCymnhd.Rows(i).Cells("行番号").Value = ds.Tables(RS).Rows(i)("行番号")

                '汎用マスタから仕入区分を取得
                Dim dsSireKbn As DataSet = getDsHanyoData(CommonConst.FIXED_KEY_PURCHASING_CLASS, ds.Tables(RS).Rows(i)("仕入区分").ToString)
                DgvCymnhd.Rows(i).Cells("仕入区分").Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                                        dsSireKbn.Tables(RS).Rows(0)("文字２"),
                                                        dsSireKbn.Tables(RS).Rows(0)("文字１"))

                DgvCymnhd.Rows(i).Cells("メーカー").Value = ds.Tables(RS).Rows(i)("メーカー")
                DgvCymnhd.Rows(i).Cells("品名").Value = ds.Tables(RS).Rows(i)("品名")
                DgvCymnhd.Rows(i).Cells("型式").Value = ds.Tables(RS).Rows(i)("型式")
                DgvCymnhd.Rows(i).Cells("仕入先名").Value = ds.Tables(RS).Rows(i)("仕入先名")
                DgvCymnhd.Rows(i).Cells("仕入値").Value = ds.Tables(RS).Rows(i)("仕入値")
                DgvCymnhd.Rows(i).Cells("受注数量").Value = ds.Tables(RS).Rows(i)("受注数量")
                DgvCymnhd.Rows(i).Cells("売上数量").Value = ds.Tables(RS).Rows(i)("売上数量")
                DgvCymnhd.Rows(i).Cells("受注残数").Value = ds.Tables(RS).Rows(i)("受注残数")
                DgvCymnhd.Rows(i).Cells("単位").Value = ds.Tables(RS).Rows(i)("単位")
                DgvCymnhd.Rows(i).Cells("間接費").Value = ds.Tables(RS).Rows(i)("間接費")
                DgvCymnhd.Rows(i).Cells("売単価").Value = ds.Tables(RS).Rows(i)("売単価")
                DgvCymnhd.Rows(i).Cells("売上金額").Value = ds.Tables(RS).Rows(i)("売上金額")
                DgvCymnhd.Rows(i).Cells("粗利額").Value = ds.Tables(RS).Rows(i)("粗利額")
                DgvCymnhd.Rows(i).Cells("粗利率").Value = ds.Tables(RS).Rows(i)("粗利率")

                If ds.Tables(RS).Rows(i)("リードタイム単位") Is DBNull.Value Then

                    'リードタイムが空だったらそのまま
                    DgvCymnhd.Rows(i).Cells("リードタイム").Value = ds.Tables(RS).Rows(i)("リードタイム")

                Else

                    'リードタイムが入っていたら汎用マスタから単位を取得して連結する
                    Sql = " AND 固定キー = '" & CommonConst.FIXED_KEY_READTIME & "'"
                    Sql += " AND 可変キー = '" & ds.Tables(RS).Rows(i)("リードタイム単位").ToString & "'"

                    Dim dsHanyo As DataSet = getDsData("m90_hanyo", Sql)

                    tmp1 = ""
                    tmp1 += ds.Tables(RS).Rows(i)("リードタイム")
                    tmp1 += dsHanyo.Tables(RS).Rows(0)("文字１")
                    DgvCymnhd.Rows(i).Cells("リードタイム").Value = tmp1
                End If

                DgvCymnhd.Rows(i).Cells("出庫数").Value = ds.Tables(RS).Rows(i)("出庫数")
                DgvCymnhd.Rows(i).Cells("未出庫数").Value = ds.Tables(RS).Rows(i)("未出庫数")
                DgvCymnhd.Rows(i).Cells("備考").Value = ds.Tables(RS).Rows(i)("備考")
                DgvCymnhd.Rows(i).Cells("更新者").Value = ds.Tables(RS).Rows(i)("更新者")
                DgvCymnhd.Rows(i).Cells("登録日").Value = ds.Tables(RS).Rows(i)("登録日")
            Next

        End If

    End Sub

    Private Sub mPurchaseCost(ByVal OrderNo As String, ByVal BranchNo As String _
                                   , ByRef decPurchase1 As Decimal, ByRef decPurchase2 As Decimal _
                                   , ByRef decPurchaseAmount1 As Decimal, ByRef decPurchaseAmount2 As Decimal)


        Dim reccnt As Integer = 0 'DB用（デフォルト
        Dim Sql As String
        Dim ds_t21 As DataSet

        Sql = "SELECT 仕入値,仕入値_外貨,発注数量,仕入金額,間接費,仕入金額_外貨"

        Sql += " FROM public.t21_hattyu"

        Sql += " WHERE 会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " and 発注番号 = '" & OrderNo & "'"
        Sql += " and 発注番号枝番 = '" & BranchNo & "'"

        ds_t21 = _db.selectDB(Sql, RS, reccnt)

        decPurchase1 = 0  '外貨
        decPurchase2 = 0
        decPurchaseAmount1 = 0
        decPurchaseAmount2 = 0

    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    '明細単位切替時で表示形式のイベントを取得
    Private Sub RbtnDetails_CheckedChanged(sender As Object, e As EventArgs) Handles RbtnDetails.CheckedChanged

        OrderListLoad() '一覧を再表示

    End Sub

    '検索実行
    Private Sub BtnQuoteSearch_Click(sender As Object, e As EventArgs) Handles BtnOrderSearch.Click

        OrderListLoad() '一覧を再表示

    End Sub

    '機能としては使用しない
    '受注修正
    Private Sub BtnOrderEdit_Click(sender As Object, e As EventArgs) Handles BtnOrderEdit.Click
        'グリッドに何もないときは次画面へ移動しない
        If Me.DgvCymnhd.RowCount = 0 Then
            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Exit Sub
        End If

        '取消済みデータは取消操作不可能
        If DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("取消").Value =
            IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_JPN, CommonConst.CANCEL_KBN_JPN_TXT, CommonConst.CANCEL_KBN_ENG_TXT) Then
            '取消データは選択できないアラートを出す
            _msgHd.dspMSG("cannotSelectTorikeshiData", frmC01F10_Login.loginValue.Language)
            Exit Sub
        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvCymnhd.CurrentCell.RowIndex
        Dim No As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号").Value
        Dim Suffix As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号枝番").Value
        Dim Status As String = CommonConst.STATUS_EDIT
        Dim openForm As Form = Nothing
        openForm = New Order(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
        openForm.ShowDialog(Me)

        OrderListLoad() '一覧表示
    End Sub

    '受注参照
    Private Sub BtnOrderView_Click(sender As Object, e As EventArgs) Handles BtnOrderView.Click
        'グリッドに何もないときは次画面へ移動しない
        If Me.DgvCymnhd.RowCount = 0 Then
            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Exit Sub
        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvCymnhd.CurrentCell.RowIndex
        Dim No As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号").Value
        Dim Suffix As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号枝番").Value
        Dim Status As String = CommonConst.STATUS_VIEW

        Dim openForm As Form = Nothing
        openForm = New Order(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
        openForm.Show(Me)
    End Sub

    '売上入力
    Private Sub BtnOrder_Click(sender As Object, e As EventArgs) Handles BtnSales.Click
        'グリッドに何もないときは次画面へ移動しない
        If Me.DgvCymnhd.RowCount = 0 Then
            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Exit Sub
        End If

        '取消済みデータは取消操作不可能
        If DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("取消").Value =
            IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_JPN, CommonConst.CANCEL_KBN_JPN_TXT, CommonConst.CANCEL_KBN_ENG_TXT) Then
            '取消データは選択できないアラートを出す
            _msgHd.dspMSG("cannotSelectTorikeshiData", frmC01F10_Login.loginValue.Language)
            Exit Sub
        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvCymnhd.CurrentCell.RowIndex
        Dim No As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号").Value
        Dim Suffix As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号枝番").Value
        Dim openForm As Form = Nothing
        openForm = New OrderManagement(_msgHd, _db, _langHd, Me, No, Suffix)   '処理選択
        openForm.Show(Me)
    End Sub

    '出庫入力
    Private Sub BtnReceipt_Click(sender As Object, e As EventArgs) Handles BtnGoodsIssue.Click
        'グリッドに何もないときは次画面へ移動しない
        If Me.DgvCymnhd.RowCount = 0 Then
            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Exit Sub
        End If

        '取消済みデータは取消操作不可能
        If DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("取消").Value =
            IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_JPN, CommonConst.CANCEL_KBN_JPN_TXT, CommonConst.CANCEL_KBN_ENG_TXT) Then
            '取消データは選択できないアラートを出す
            _msgHd.dspMSG("cannotSelectTorikeshiData", frmC01F10_Login.loginValue.Language)
            Exit Sub
        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvCymnhd.CurrentCell.RowIndex
        Dim No As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号").Value
        Dim Suffix As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号枝番").Value
        Dim openForm As Form = Nothing
        openForm = New GoodsIssue(_msgHd, _db, _langHd, No, Suffix)   '処理選択
        openForm.Show(Me)
    End Sub

    '受注取消
    Private Sub BtnOrderCancel_Click(sender As Object, e As EventArgs) Handles BtnOrderCancel.Click

        'グリッドに何もないときは次画面へ移動しない
        If Me.DgvCymnhd.RowCount = 0 Then
            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Exit Sub
        End If

        Dim dtNow As String = UtilClass.formatDatetime(DateTime.Now)
        Dim Sql1 As String = ""

        '明細表示時は取消操作不可能
        If RbtnDetails.Checked Then

            '操作できないアラートを出す
            _msgHd.dspMSG("chkDetailsCancel", frmC01F10_Login.loginValue.Language)
            Return

        End If

        '取消済みデータは取消操作不可能
        If DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("取消").Value =
            IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_JPN, CommonConst.CANCEL_KBN_JPN_TXT, CommonConst.CANCEL_KBN_ENG_TXT) Then
            '取消データは選択できないアラートを出す
            _msgHd.dspMSG("cannotSelectTorikeshiData", frmC01F10_Login.loginValue.Language)
            Return
        End If


        '入金済みのデータはエラー
        Dim blnFlg As Boolean = mCheckNyukin()
        If blnFlg = False Then
            '取消データは選択できないアラートを出す
            _msgHd.dspMSG("cannotSelectTorikeshiData_nyukin", frmC01F10_Login.loginValue.Language)
            Return
        End If


        '支払済みのデータはエラー
        blnFlg = mCheckShiharai()
        If blnFlg = False Then
            '取消データは選択できないアラートを出す
            _msgHd.dspMSG("cannotSelectTorikeshiData_shiharai", frmC01F10_Login.loginValue.Language)
            Return
        End If


        Try

            '受注番号を保持しておく
            Dim strJyutyuNo As String = DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("受注番号").Value
            Dim strEda As String = DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("受注番号枝番").Value

            Dim strMitumoriNo As String = DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("見積番号").Value
            Dim strMitumoriEda As String = DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("見積番号枝番").Value


            '取消確認のアラート
            Dim result As DialogResult = _msgHd.dspMSG("confirmCancel", frmC01F10_Login.loginValue.Language)

            If result = DialogResult.No Then
                Exit Sub
            End If


            '発注、出庫、売上、請求の取消
            blnFlg = mCheckAllCancel(strJyutyuNo, strEda)
            If blnFlg = False Then
                'キャンセルボタンの場合は終了
                Exit Sub
            End If


            '引当データを元に戻す
            '受注データの取消
            If mOutCancel(strJyutyuNo, strEda, strMitumoriNo, strMitumoriEda) = False Then
                Exit Sub
            End If


            OrderListLoad() 'データ更新

        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    Private Function mCheckAllCancel(ByVal strJyutyuNo As String, ByVal strEda As String) As Boolean

        Dim reccnt As Integer = 0
        Dim strMessage As String = vbNullString
        Dim blnFlg1 As Boolean = False
        Dim blnFlg2 As Boolean = False
        Dim blnFlg3 As Boolean = False
        Dim blnFlg4 As Boolean = False

        Dim strHatyu As String = vbNullString     '発注番号
        Dim strHatyuEda As String = vbNullString
        Dim strSyukoNo As String = vbNullString   '出庫番号
        Dim intShiireKubun As Integer = 0         '仕入区分
        Dim strUriageNo As String = vbNullString  '売上番号
        Dim strUriageEda As String = vbNullString
        Dim strSeikyuNo As String = vbNullString  '請求番号



#Region "発注"


        '受注と結び付いた発注データが存在するか検索する
        Dim Sql As String = "AND 受注番号 = '" & strJyutyuNo & "'"
        Sql += "AND 受注番号枝番 = '" & strEda & "'"
        Sql += " and 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        Dim dshattyu As DataSet = getDsData("t20_hattyu", Sql)


        If dshattyu.Tables(RS).Rows.Count = 0 Then
        Else
            '発注登録あり
            blnFlg4 = True
            mCheckAllCancel = True

            strHatyu = dshattyu.Tables(RS).Rows(0)("発注番号")
            strHatyuEda = dshattyu.Tables(RS).Rows(0)("発注番号枝番")
        End If


        dshattyu = Nothing

#End Region


#Region "出庫"


        '受注と結び付いた出庫データが存在するか検索する
        Sql = "SELECT t44.出庫番号,t45.仕入区分"

        Sql += " FROM t44_shukohd t44 left JOIN t45_shukodt t45 "
        Sql += " ON t44.出庫番号 = t45.出庫番号"

        Sql += " WHERE "
        Sql += "     t44.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " and t44.受注番号 = '" & strJyutyuNo & "'"
        Sql += " and t44.受注番号枝番 = '" & strEda & "'"
        Sql += " and t44.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
        Sql += " AND t45.出庫区分 = '" & CommonConst.SHUKO_KBN_NORMAL & "'" '通常出庫のものを取得

        Dim dsSyuko As DataSet = _db.selectDB(Sql, RS, reccnt)


        If dsSyuko.Tables(RS).Rows.Count = 0 Then
        Else
            '出庫登録あり
            blnFlg1 = True
            mCheckAllCancel = True

            strSyukoNo = dsSyuko.Tables(RS).Rows(0)("出庫番号")
            intShiireKubun = dsSyuko.Tables(RS).Rows(0)("仕入区分")
        End If


        dsSyuko = Nothing

#End Region


#Region "売上"


        '受注と結び付いた売上データが存在するか検索する
        Sql = "SELECT 売上番号,売上番号枝番"

        Sql += " FROM t30_urighd "

        Sql += " WHERE "
        Sql += "     会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " and 受注番号 = '" & strJyutyuNo & "'"
        Sql += " and 受注番号枝番 = '" & strEda & "'"
        Sql += " and 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        Dim dsUriage As DataSet = _db.selectDB(Sql, RS, reccnt)

        If dsUriage.Tables(RS).Rows.Count = 0 Then
        Else
            '売上登録あり
            blnFlg2 = True
            mCheckAllCancel = True

            strUriageNo = dsUriage.Tables(RS).Rows(0)("売上番号")
            strUriageEda = dsUriage.Tables(RS).Rows(0)("売上番号枝番")
        End If


        dsUriage = Nothing

#End Region


#Region "請求"


        '受注と結び付いた請求データが存在するか検索する
        Sql = "SELECT 請求番号"

        Sql += " FROM t23_skyuhd "

        Sql += " WHERE "
        Sql += "     会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " and 受注番号 = '" & strJyutyuNo & "'"
        Sql += " and 受注番号枝番 = '" & strEda & "'"
        Sql += " and 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        Dim dsSeikyu As DataSet = _db.selectDB(Sql, RS, reccnt)

        If dsSeikyu.Tables(RS).Rows.Count = 0 Then
        Else
            '請求登録あり
            blnFlg3 = True
            mCheckAllCancel = True

            strSeikyuNo = dsSeikyu.Tables(RS).Rows(0)("請求番号")
        End If


        dsSeikyu = Nothing

#End Region


#Region "メッセージ確認"


        '発注、出庫、売上、請求に対象の受注番号がない場合は終了
        If mCheckAllCancel = False Then
            mCheckAllCancel = True
            Exit Function
        End If


        Dim strTitle1 As String = "Goods issue registration"
        Dim strTitle2 As String = "Sales registration"
        Dim strTitle3 As String = "Billing registration"
        Dim strTitle4 As String = "Order registration"



        '確認メッセージの作成
        If blnFlg4 = True Then  '発注
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then  '英語
                strMessage += strTitle4 & vbCrLf
            Else
                strMessage += "発注登録" & vbCrLf
            End If
        End If

        If blnFlg1 = True Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then  '英語
                strMessage += strTitle1 & vbCrLf
            Else
                strMessage += "出庫登録" & vbCrLf
            End If
        End If

        If blnFlg2 = True Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then  '英語
                strMessage += strTitle2 & vbCrLf
            Else
                strMessage += "売上登録" & vbCrLf
            End If
        End If

        If blnFlg3 = True Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then  '英語
                strMessage += strTitle3 & vbCrLf
            Else
                strMessage += "請求登録" & vbCrLf
            End If
        End If


        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then  '英語
            strMessage += "Has already been done.Together with order registration" & vbCrLf

            If blnFlg4 = True Then  '発注
                strMessage += strTitle4
            End If

            If blnFlg1 = True Then
                If blnFlg4 = True Then
                    strMessage += "・"
                End If
                strMessage += strTitle1
            End If

            If blnFlg2 = True Then
                If blnFlg4 = True Or blnFlg1 = True Then
                    strMessage += "・"
                End If
                strMessage += strTitle2
            End If

            If blnFlg3 = True Then
                If blnFlg4 = True Or blnFlg1 = True Or blnFlg2 = True Then
                    strMessage += "・"
                End If
                strMessage += strTitle3
            End If

        Else
            strMessage += "が既になされています。発注登録と合わせて" & vbCrLf

            If blnFlg4 = True Then  '発注
                strMessage += "発注登録"
            End If

            If blnFlg1 = True Then
                If blnFlg4 = True Then
                    strMessage += "・"
                End If
                strMessage += "出庫登録"
            End If

            If blnFlg2 = True Then
                If blnFlg4 = True Or blnFlg1 = True Then
                    strMessage += "・"
                End If
                strMessage += "売上登録"
            End If

            If blnFlg3 = True Then
                If blnFlg4 = True Or blnFlg1 = True Or blnFlg2 = True Then
                    strMessage += "・"
                End If
                strMessage += "請求登録"
            End If

        End If


        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then  '英語
            strMessage += " Do you also cancel?"
        Else
            strMessage += "も取り消しますか？"
        End If


        Dim result As DialogResult = MessageBox.Show(strMessage, CommonConst.AP_NAME, MessageBoxButtons.OKCancel,
                                                     MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)

        If result = DialogResult.Cancel Then
            'キャンセル場合は終了
            mCheckAllCancel = False
            Exit Function
        End If

#End Region


#Region "発注取消"

        '発注に対象の受注番号がある場合
        If blnFlg4 = True Then

            '共通関数を呼び出し
            '入庫、仕入、買掛を取消
            Dim frm As New OrderingList(_msgHd, _db, _langHd, Me, CommonConst.STATUS_VIEW)
            Dim blnFlg As Boolean = frm.gCheckShiire(1, strHatyu, strHatyuEda)  '発注取消
            If blnFlg = False Then
                Exit Function
            End If

            '発注を取消
            blnFlg = frm.gHatyuCancel(strHatyu, strHatyuEda)
            If blnFlg = False Then
                Exit Function
            End If

        End If


#End Region


#Region "出庫取消"

        '出庫に対象の受注番号がある場合
        If blnFlg1 = True Then
            Dim blnFlg As Boolean = updateData(strJyutyuNo, strEda, strSyukoNo, intShiireKubun)  '出庫取消
            If blnFlg = False Then
                Exit Function
            End If
        End If


#End Region


#Region "売上取消"

        '売上に対象の発注番号がある場合
        If blnFlg2 = True Then
            Dim blnFlg As Boolean = updateUriage(strJyutyuNo, strEda, strUriageNo, strUriageEda)  '売上取消
            If blnFlg = False Then
                Exit Function
            End If
        End If

#End Region


#Region "請求取消"

        '請求に対象の発注番号がある場合
        If blnFlg3 = True Then
            Dim blnFlg As Boolean = updateSeikyu(strSeikyuNo)  '請求取消
            If blnFlg = False Then
                Exit Function
            End If
        End If

#End Region


        mCheckAllCancel = True

    End Function

    Private Function updateSeikyu(ByVal strSeikyuNo As String) As Boolean

        Dim dtNow As String = FormatDateTime(DateTime.Now)
        Dim Sql As String = ""
        Dim ds As DataSet


        Sql = " AND 請求番号 ='" & strSeikyuNo & "'"

        '画面を開いた時から対象データに対して更新がされていないかどうか確認
        ds = getDsData("t23_skyuhd", Sql)


        Sql = "UPDATE Public.t23_skyuhd "
        Sql += "SET "

        Sql += "取消区分 = '" & CommonConst.CANCEL_KBN_DISABLED & "'"
        Sql += ", 取消日 = current_date"
        Sql += ", 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
        Sql += ", 更新日 = current_timestamp"

        Sql += " WHERE 会社コード ='" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND 請求番号 = '" & strSeikyuNo & "'"

        '請求基本を更新
        _db.executeDB(Sql)


        updateSeikyu = True

    End Function

    Private Function updateUriage(ByVal strJyutyuNo As String, ByVal strEda As String, ByVal strUriageNo As String, ByVal strUriageEda As String) As Boolean

        Dim dtNow As DateTime = DateTime.Now
        Dim strNow As String = UtilClass.formatDatetime(dtNow)
        Dim reccnt As Integer = 0
        Dim Sql As String = ""

        '画面を開いた時から対象データに対して更新がされていないかどうか確認
        Sql = " AND "
        Sql += " 売上番号 ILIKE '" & strUriageNo & "'"
        Sql += " AND "
        Sql += " 売上番号枝番 ILIKE '" & strUriageEda & "'"
        Sql += " AND "
        Sql += " 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString

        ds = getDsData("t30_urighd", Sql)


        Sql = "SELECT"
        Sql += " t11.*, t10.取消区分"
        Sql += " FROM "
        Sql += " public.t11_cymndt t11 "

        Sql += " INNER JOIN "
        Sql += " t10_cymnhd t10"
        Sql += " ON "

        Sql += " t11.会社コード = t10.会社コード"
        Sql += " AND "
        Sql += " t11.受注番号 = t10.受注番号"
        Sql += " AND "
        Sql += " t11.受注番号枝番 = t10.受注番号枝番"

        Sql += " WHERE "
        Sql += " t11.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND "
        Sql += " t11.受注番号 ILIKE '" & strJyutyuNo & "'"
        Sql += " AND "
        Sql += " t11.受注番号枝番 ILIKE '" & strEda & "'"
        Sql += " AND "
        Sql += " t10.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString

        Dim dsCyminDt As DataSet = _db.selectDB(Sql, RS, reccnt)


        Sql = "SELECT"
        Sql += " t31.*, t30.取消区分"
        Sql += " FROM "
        Sql += " public.t31_urigdt t31 "

        Sql += " INNER JOIN "
        Sql += " t30_urighd t30"
        Sql += " ON "

        Sql += " t31.会社コード = t30.会社コード"
        Sql += " AND "
        Sql += " t31.売上番号 = t30.売上番号"
        Sql += " AND "
        Sql += " t31.売上番号枝番 = t30.売上番号枝番"

        Sql += " WHERE "
        Sql += " t31.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND "
        Sql += " t31.売上番号 ILIKE '" & strUriageNo & "'"
        Sql += " AND "
        Sql += " t31.売上番号枝番 ILIKE '" & strUriageEda & "'"
        Sql += " AND "
        Sql += " t30.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString

        Dim dsUrigDt As DataSet = _db.selectDB(Sql, RS, reccnt)

        '取消す売上データをループさせながら、受注データから減算していく
        For i As Integer = 0 To dsUrigDt.Tables(RS).Rows.Count() - 1

            For x As Integer = 0 To dsCyminDt.Tables(RS).Rows.Count() - 1

                '行番号が一致したら
                If dsUrigDt.Tables(RS).Rows(i)("行番号") = dsCyminDt.Tables(RS).Rows(x)("行番号") Then

                    Try
                        '受注明細から取消す売上データ明細の数を減算
                        Sql = "UPDATE "
                        Sql += "Public."
                        Sql += "t11_cymndt "
                        Sql += "SET "
                        Sql += "売上数量"
                        Sql += " = '"
                        Sql += (dsCyminDt.Tables(RS).Rows(x)("売上数量") - dsUrigDt.Tables(RS).Rows(i)("売上数量")).ToString
                        Sql += "', "
                        Sql += " 受注残数"
                        Sql += " = '"
                        Sql += (dsCyminDt.Tables(RS).Rows(x)("受注残数") + dsUrigDt.Tables(RS).Rows(i)("売上数量")).ToString
                        Sql += "', "
                        Sql += "更新者"
                        Sql += " = '"
                        Sql += frmC01F10_Login.loginValue.TantoNM
                        Sql += "' "
                        Sql += "WHERE"
                        Sql += " 会社コード"
                        Sql += "='"
                        Sql += frmC01F10_Login.loginValue.BumonCD
                        Sql += "'"
                        Sql += " AND"
                        Sql += " 受注番号"
                        Sql += "='"
                        Sql += dsCyminDt.Tables(RS).Rows(x)("受注番号")
                        Sql += "'"
                        Sql += " AND"
                        Sql += " 受注番号枝番"
                        Sql += "='"
                        Sql += dsCyminDt.Tables(RS).Rows(x)("受注番号枝番")
                        Sql += "'"
                        Sql += " AND"
                        Sql += " 行番号"
                        Sql += "='"
                        Sql += dsCyminDt.Tables(RS).Rows(x)("行番号").ToString
                        Sql += "' "

                        _db.executeDB(Sql)

                    Catch ue As UsrDefException
                        ue.dspMsg()
                        Throw ue
                    Catch ex As Exception
                        'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                        Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
                    End Try

                End If

            Next

        Next

        Try
            '受注基本データを更新
            Sql = "UPDATE "
            Sql += "Public."
            Sql += "t10_cymnhd "
            Sql += "SET "
            Sql += "更新日"
            Sql += " = '"
            Sql += strNow
            Sql += "', "
            Sql += "更新者"
            Sql += " = '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            Sql += "' "
            Sql += "WHERE"
            Sql += " 会社コード"
            Sql += "='"
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "'"
            Sql += " AND"
            Sql += " 受注番号"
            Sql += "='"
            Sql += strJyutyuNo
            Sql += "'"
            Sql += " AND"
            Sql += " 受注番号枝番"
            Sql += "='"
            Sql += strEda
            Sql += "'"

            _db.executeDB(Sql)

            Sql = "UPDATE "
            Sql += "Public."
            Sql += "t30_urighd "
            Sql += "SET "

            Sql += "取消区分 = " & CommonConst.CANCEL_KBN_DISABLED.ToString
            Sql += ", "
            Sql += "取消日"
            Sql += " = '"
            Sql += strNow
            Sql += "', "
            Sql += "更新日"
            Sql += " = '"
            Sql += strNow
            Sql += "', "
            Sql += "更新者"
            Sql += " = '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            Sql += " ' "

            Sql += "WHERE"
            Sql += " 会社コード"
            Sql += "='"
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "'"
            Sql += " AND"
            Sql += " 売上番号"
            Sql += "='"
            Sql += strUriageNo
            Sql += "' "
            Sql += " AND"
            Sql += " 売上番号枝番"
            Sql += "='"
            Sql += strUriageEda
            Sql += "' "

            _db.executeDB(Sql)

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try


        updateUriage = True

    End Function


    Private Function updateData(ByVal strJyutyuNo As String, ByVal strEda As String, ByVal strSyukoNo As String, ByVal intKubun As Integer) As Boolean

        Dim dtNow As String = UtilClass.formatDatetime(DateTime.Now)
        Dim dtNow_insert As DateTime = DateTime.Now
        Dim reccnt As Integer = 0
        Dim Sql As String = ""

        Try


#Region "在庫引当_Insert"

            '在庫引当の場合
            '新しい出庫データと履歴を作成する
            '出庫番号を最新、受注番号などは変更しない
            If intKubun = CommonConst.Sire_KBN_Zaiko Then

                '採番データを取得・更新
                '出庫登録データの伝票番号は基本的に LS で統一される（1商品で複数の在庫マスタをまたぐ場合を除く）
                Dim frm As New GoodsIssueList(_msgHd, _db, _langHd, Me, CommonConst.STATUS_VIEW)
                Dim MAIN_LS As String = frm.getSaiban("70", dtNow_insert.ToShortDateString())

                't44_shukohd
                Sql = " and 出庫番号 ='" & strSyukoNo & "'"
                Dim dsShukohd As DataSet = getDsData("t44_shukohd", Sql)


                't44_shukohd
                Sql = "INSERT INTO Public.t44_shukohd ("
                Sql += "会社コード, 出庫番号, 見積番号, 見積番号枝番, 受注番号, 受注番号枝番, 客先番号, 得意先コード, 得意先名"
                Sql += ", 得意先郵便番号, 得意先住所, 得意先電話番号, 得意先ＦＡＸ, 得意先担当者役職, 得意先担当者名, 営業担当者"
                Sql += ", 入力担当者, 備考, 取消日, 取消区分, 出庫日, 登録日, 更新日, 更新者, 営業担当者コード, 入力担当者コード)"
                Sql += " VALUES('" & frmC01F10_Login.loginValue.BumonCD & "'"       '会社コード
                Sql += ", '" & MAIN_LS & "'"                 '出庫番号
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("見積番号") & "'"        '見積番号
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("見積番号枝番") & "'"    '見積番号枝番
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("受注番号") & "'"        '受注番号
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("受注番号枝番") & "'"    '受注番号枝番
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("客先番号") & "'"        '客先番号
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("得意先コード") & "'"    '得意先コード
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("得意先名") & "'"        '得意先名
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("得意先郵便番号") & "'"     '得意先郵便番号
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("得意先住所") & "'"       '得意先住所
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("得意先電話番号") & "'"   '得意先電話番号
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("得意先ＦＡＸ") & "'"      '得意先ＦＡＸ
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("得意先担当者役職") & "'"  '得意先担当者役職
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("得意先担当者名") & "'"    '得意先担当者名
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("営業担当者") & "'"          '営業担当者
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("入力担当者") & "'"          '入力担当者
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("備考") & "'"                '備考
                Sql += ", null"     '取消日
                Sql += ", 0"     '取消区分
                Sql += ", '" & UtilClass.formatDatetime(dsShukohd.Tables(RS).Rows(0)("出庫日")) & "'"     '出庫日
                Sql += ", '" & UtilClass.formatDatetime(dtNow) & "'"                 '登録日
                Sql += ", '" & UtilClass.formatDatetime(dtNow) & "'"                 '更新日
                Sql += ", '" & frmC01F10_Login.loginValue.TantoNM & "'"     '更新者
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("営業担当者コード") & "'"    '営業担当者コード
                Sql += ", '" & frmC01F10_Login.loginValue.TantoCD & "'"    '入力担当者コード
                Sql += " )"

                _db.executeDB(Sql)



                't45_shukodt
                Sql = " and 出庫番号 ='" & strSyukoNo & "'"
                Dim dsShuko As DataSet = getDsData("t45_shukodt", Sql)

                For i As Integer = 0 To dsShuko.Tables(RS).Rows.Count - 1

                    Sql = "INSERT INTO "
                    Sql += "Public."
                    Sql += "t45_shukodt("
                    Sql += "会社コード, 出庫番号, 受注番号, 受注番号枝番, 行番号, 仕入区分, メーカー, 品名, 型式"
                    Sql += ", 仕入先名, 売単価, 出庫数量, 単位, 備考, 更新者, 更新日, 出庫区分, 倉庫コード)"
                    Sql += " VALUES('" & frmC01F10_Login.loginValue.BumonCD & "'"   '会社コード
                    Sql += ", '" & MAIN_LS & "'"     '出庫番号
                    Sql += ", '" & dsShuko.Tables(RS).Rows(i)("受注番号") & "'"            '受注番号
                    Sql += ", '" & dsShuko.Tables(RS).Rows(i)("受注番号枝番") & "'"        '受注番号枝番
                    Sql += ", " & dsShuko.Tables(RS).Rows(i)("行番号")                     '行番号
                    Sql += ", '" & dsShuko.Tables(RS).Rows(i)("仕入区分") & "'"            '仕入区分
                    Sql += ", '" & dsShuko.Tables(RS).Rows(i)("メーカー") & "'"            'メーカー
                    Sql += ", '" & dsShuko.Tables(RS).Rows(i)("品名") & "'"                '品名
                    Sql += ", '" & dsShuko.Tables(RS).Rows(i)("型式") & "'"                '型式
                    Sql += ", '" & dsShuko.Tables(RS).Rows(i)("仕入先名") & "'"            '仕入先名
                    Sql += ", " & UtilClass.formatNumber(dsShuko.Tables(RS).Rows(i)("売単価"))    '売単価
                    Sql += ", " & UtilClass.formatNumber(dsShuko.Tables(RS).Rows(i)("出庫数量"))        '数量
                    Sql += ", '" & dsShuko.Tables(RS).Rows(i)("単位") & "'"                         '単位
                    Sql += ", '" & dsShuko.Tables(RS).Rows(i)("備考") & "'"                          '備考
                    Sql += ", '" & frmC01F10_Login.loginValue.TantoNM & "'"                           '更新者
                    Sql += ", '" & UtilClass.formatDatetime(dtNow) & "'"                              '更新日
                    Sql += ", '" & CommonConst.SHUKO_KBN_TMP & "'" '仮出庫：2
                    Sql += ", '" & dsShuko.Tables(RS).Rows(i)("倉庫コード") & "')" '倉庫コード

                    _db.executeDB(Sql)


                    '在庫データの伝票番号と行番号を呼び出す（重要）
                    'inoutの出庫開始サイン（旧：ロケ番号）へ挿入
                    ''Sql = "SELECT ロケ番号 "                      '2020.01.09 DEL
                    Sql = "SELECT 出庫開始サイン "                  '2020.01.09 ADD
                    Sql += " from t70_inout t70 "

                    Sql += " WHERE 会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"

                    Sql += "   AND 伝票番号 = '" & dsShuko.Tables(RS).Rows(i)("出庫番号") & "'"
                    Sql += "   AND   行番号 = '" & dsShuko.Tables(RS).Rows(i)("行番号") & "'"

                    '在庫マスタから現在庫数を取得
                    Dim dsZaiko As DataSet = _db.selectDB(Sql, RS, reccnt)


                    't70_inout にデータ登録
                    Sql = "INSERT INTO "
                    Sql += "Public."
                    Sql += "t70_inout("
                    Sql += "会社コード, 入出庫区分, 倉庫コード, 伝票番号, 行番号, 入出庫種別, 引当区分"
                    Sql += ", メーカー, 品名, 型式, 数量, 単位, 備考, 入出庫日"
                    ''Sql += ", 取消区分, 更新者, 更新日, ロケ番号"               '2020.01.09 DEL
                    Sql += ", 取消区分, 更新者, 更新日, 出庫開始サイン"           '2020.01.09 ADD   
                    Sql += " )VALUES('"
                    Sql += frmC01F10_Login.loginValue.BumonCD '会社コード
                    Sql += "', '"
                    Sql += "2" '入出庫区分 1.入庫, 2.出庫
                    Sql += "', '"
                    Sql += dsShuko.Tables(RS).Rows(i)("倉庫コード")  '倉庫コード
                    Sql += "', '"
                    Sql += MAIN_LS '伝票番号
                    Sql += "', '"
                    Sql += UtilClass.formatNumber(dsShuko.Tables(RS).Rows(i)("行番号"))  '行番号
                    Sql += "', '"
                    'Sql += DgvAdd.Rows(i).Cells("入出庫種別").Value.ToString '入出庫種別
                    Sql += CommonConst.INOUT_KBN_NORMAL.ToString '入出庫種別(0：通常）
                    Sql += "', '"
                    Sql += CommonConst.AC_KBN_ASSIGN.ToString '引当区分 1
                    Sql += "', '"
                    Sql += dsShuko.Tables(RS).Rows(i)("メーカー") 'メーカー
                    Sql += "', '"
                    Sql += dsShuko.Tables(RS).Rows(i)("品名") '品名
                    Sql += "', '"
                    Sql += dsShuko.Tables(RS).Rows(i)("型式") '型式
                    Sql += "', '"
                    Sql += UtilClass.formatNumber(dsShuko.Tables(RS).Rows(i)("出庫数量")) '数量
                    Sql += "', '"
                    Sql += dsShuko.Tables(RS).Rows(i)("単位")  '単位
                    Sql += "', '"
                    Sql += dsShuko.Tables(RS).Rows(i)("備考") '備考
                    Sql += "', '"
                    Sql += UtilClass.formatDatetime(dsShukohd.Tables(RS).Rows(0)("出庫日")) '入出庫日
                    Sql += "', '"
                    Sql += CommonConst.CANCEL_KBN_ENABLED.ToString '取消区分
                    Sql += "', '"
                    Sql += frmC01F10_Login.loginValue.TantoNM '更新者
                    Sql += "', '"
                    Sql += UtilClass.formatDatetime(dtNow) '更新日
                    Sql += "', '"
                    'Sql += dsZaiko.Tables(RS).Rows(i)("伝票番号") & dsZaiko.Tables(RS).Rows(i)("行番号")
                    ''Sql += dsZaiko.Tables(RS).Rows(0)("ロケ番号")             '2020.01.09 DEL
                    Sql += dsZaiko.Tables(RS).Rows(0)("出庫開始サイン")         '2020.01.09 ADD   

                    'Sql += "WH082601801"

                    'Sql += " ', '"
                    'Sql += dsShuko.Tables(RS).Rows(i)("仕入区分") '仕入区分

                    Sql += "')"

                    _db.executeDB(Sql)

                Next

            End If

#End Region

#Region "出庫取消"


            Sql = "UPDATE "
            Sql += "Public."
            Sql += "t44_shukohd "
            Sql += "SET "

            Sql += "取消区分 = " & CommonConst.CANCEL_KBN_DISABLED '取消区分：1
            Sql += ", "
            Sql += "取消日"
            Sql += " = '"
            Sql += dtNow
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
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "'"
            Sql += " AND"
            Sql += " 出庫番号"
            Sql += "='"
            Sql += strSyukoNo
            Sql += "'"

            _db.executeDB(Sql)

            Sql = "UPDATE "
            Sql += "Public."
            Sql += "t45_shukodt "
            Sql += "SET "

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
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "'"
            Sql += " AND"
            Sql += " 出庫番号"
            Sql += "='"
            Sql += strSyukoNo
            Sql += "' "

            _db.executeDB(Sql)

#End Region


#Region "受注データを更新する"

            '受注データ
            Sql = " AND "
            Sql += "受注番号 ILIKE '" & strJyutyuNo & "'"
            Sql += " AND "
            Sql += "受注番号枝番 ILIKE '" & strEda & "'"

            Dim dsCymndt As DataSet = getDsData("t11_cymndt", Sql)

            '出庫データ
            Sql = " AND"
            Sql += " 出庫番号"
            Sql += "='"
            Sql += strSyukoNo
            Sql += "'"

            Dim dsShukodt As DataSet = getDsData("t45_shukodt", Sql)


            '受注データを更新する
            For i As Integer = 0 To dsCymndt.Tables(RS).Rows.Count - 1
                For x As Integer = 0 To dsShukodt.Tables(RS).Rows.Count - 1

                    '行番号が一致したら
                    If dsCymndt.Tables(RS).Rows(i)("行番号") = dsShukodt.Tables(RS).Rows(x)("行番号") Then
                        Dim calShukko As Integer = dsCymndt.Tables(RS).Rows(i)("出庫数") - dsShukodt.Tables(RS).Rows(x)("出庫数量")
                        Dim calUnShukko As Integer = dsCymndt.Tables(RS).Rows(i)("未出庫数") + dsShukodt.Tables(RS).Rows(x)("出庫数量")

                        Sql = "update t11_cymndt set "
                        Sql += "出庫数 = '" & calShukko & "'"
                        Sql += ",未出庫数 = '" & calUnShukko & "'"
                        Sql += ",更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
                        Sql += " where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                        Sql += " AND "
                        Sql += "受注番号 ILIKE '" & strJyutyuNo & "'"
                        Sql += " AND "
                        Sql += "受注番号枝番 ILIKE '" & strEda & "'"
                        Sql += " AND "
                        Sql += "行番号 = '" & dsCymndt.Tables(RS).Rows(i)("行番号") & "'"

                        _db.executeDB(Sql)

                        Sql = "update t10_cymnhd set "
                        Sql += "更新日 = '" & dtNow & "'"
                        Sql += ",更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
                        Sql += " where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                        Sql += " AND "
                        Sql += "受注番号 ILIKE '" & strJyutyuNo & "'"
                        Sql += " AND "
                        Sql += "受注番号枝番 ILIKE '" & strEda & "'"
                        Sql += " AND "
                        Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '取消区分=0

                        _db.executeDB(Sql)

                    End If

                Next

            Next

#End Region


            Sql = "UPDATE "
            Sql += "Public."
            Sql += "t70_inout "
            Sql += "SET "

            Sql += "取消日"
            Sql += " = '"
            Sql += dtNow
            Sql += "', "
            Sql += "取消区分"
            Sql += " = '"
            Sql += CommonConst.CANCEL_KBN_DISABLED.ToString
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
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "'"
            Sql += " AND"
            Sql += " 伝票番号"
            Sql += "='"
            Sql += strSyukoNo
            Sql += "' "

            _db.executeDB(Sql)



        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try


        updateData = True

    End Function


    Private Function mCheckShiharai() As Boolean

        Dim reccnt As Integer = 0


        '受注番号で発注データを検索
        Dim strJyutyuNo As String = DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("受注番号").Value
        Dim strEda As String = DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("受注番号枝番").Value


        Dim Sql As String = "SELECT 発注番号,発注番号枝番"

        Sql += " FROM t20_hattyu"

        Sql += " WHERE "
        Sql += "     会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " and 受注番号 = '" & strJyutyuNo & "'"
        Sql += " and 受注番号枝番 = '" & strEda & "'"
        Sql += " and 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        Dim dshattyu As DataSet = _db.selectDB(Sql, RS, reccnt)


        If dshattyu.Tables(RS).Rows.Count = 0 Then
            '対象の発注データがない場合は正常終了
            mCheckShiharai = True
            Exit Function
        End If


        Dim strHatyuNo As String = dshattyu.Tables(RS).Rows(0)("発注番号")
        Dim strHatyuEda As String = dshattyu.Tables(RS).Rows(0)("発注番号枝番")


        '共通関数を呼び出し
        '発注と結び付いた支払データが存在するか検索する
        '発注番号で買掛データを検索 → 買掛番号で取消されていない支払データを検索
        Dim frm As New OrderingList(_msgHd, _db, _langHd, Me, CommonConst.STATUS_VIEW)
        Dim blnFlg As Boolean = frm.gCheckShiharai(strHatyuNo, strHatyuEda)
        If blnFlg = False Then
            mCheckShiharai = False
        Else
            '支払データがない場合はチェックなし
            mCheckShiharai = True
        End If


        dshattyu = Nothing

    End Function


    Private Function mCheckNyukin() As Boolean

        Dim reccnt As Integer = 0


        '受注と結び付いた入金データが存在するか検索する
        '受注番号で請求データを検索 → 請求番号で取消されていない入金データを検索
        Dim strJyutyuNo As String = DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("受注番号").Value
        Dim strEda As String = DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("受注番号枝番").Value


        Dim Sql As String = "SELECT t23.請求番号 as 請求1, t27.請求番号 as 請求2"

        Sql += " FROM t23_skyuhd t23 left join t27_nkinkshihd t27"
        Sql += " on t23.請求番号 = t27.請求番号"

        Sql += " WHERE "
        Sql += "     t23.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " and t23.受注番号 = '" & strJyutyuNo & "'"
        Sql += " and t23.受注番号枝番 = '" & strEda & "'"
        Sql += " and t27.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        Dim dsNyukin As DataSet = _db.selectDB(Sql, RS, reccnt)

        If dsNyukin.Tables(RS).Rows.Count = 0 Then
            '対象の入金データがない場合は正常終了
            mCheckNyukin = True
        Else
            Dim strMoji = Convert.ToString(dsNyukin.Tables(RS).Rows(0)("請求2"))
            If String.IsNullOrEmpty(strMoji) Then
                '対象の入金データがない場合は正常終了
                mCheckNyukin = True
            Else
                '対象の入金データがあった場合は受注取消ができない
                mCheckNyukin = False
            End If
        End If


        dsNyukin = Nothing

    End Function


    '引当データを元に戻す
    't44_shukohd
    't45_shukodt
    't70_inout
    't44_shukohd
    't45_shukodt
    't01_mithd
    Private Function mOutCancel(ByVal strJyutyuNo As String, ByVal strEda As String _
                                     , ByVal strMitumoriNo As String, ByVal strMitumoriEda As String) As Boolean

        Dim dtNow As String = UtilClass.formatDatetime(DateTime.Now)
        Dim Sql1 As String = ""
        Dim reccnt As Integer = 0 'DB用（デフォルト）

        mOutCancel = False

        Try


            '出庫データ登録前に、「在庫引当」の商品があるかどうかチェック
            'Sql1 = "AND 受注番号 = '" & DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("受注番号").Value & "'"
            'Sql1 += "AND 受注番号枝番 = '" & DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("受注番号枝番").Value & "'"

            Sql1 = "AND 受注番号 = '" & strJyutyuNo & "'"
            Sql1 += "AND 受注番号枝番 = '" & strEda & "'"

            Dim dsCymndt As DataSet = getDsData("t11_cymndt", Sql1)


            For i As Integer = 0 To dsCymndt.Tables(RS).Rows.Count() - 1

                '仕入区分が2（在庫引当）の場合、作成済みの仮出庫データを「取消区分=0, 取消日=Datetime.Date」でUPDATEする
                If dsCymndt.Tables(RS).Rows(i)("仕入区分").ToString = CommonConst.Sire_KBN_Zaiko Then

                    '出庫データ select
                    'Sql1 = " AND 受注番号 = '" & DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("受注番号").Value & "'"
                    'Sql1 += " AND 受注番号枝番 = '" & DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("受注番号枝番").Value & "'"
                    'Sql1 += " AND 行番号 = '" & dsCymndt.Tables(RS).Rows(i)("行番号").ToString & "'"

                    'Dim dsShukodt As DataSet = getDsData("t45_shukodt", Sql1)


                    Dim strJyutyuNo2 = dsCymndt.Tables(RS).Rows(i)("受注番号")
                    Dim strEda2 = dsCymndt.Tables(RS).Rows(i)("受注番号枝番")

                    Sql1 = " SELECT t44.出庫番号,t45.行番号 "

                    Sql1 += " FROM t44_shukohd t44 INNER JOIN t45_shukodt t45 "
                    Sql1 += " on t44.出庫番号 = t45.出庫番号 "

                    Sql1 += " WHERE "
                    Sql1 += " t44.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                    Sql1 += " AND t44.受注番号 = '" & strJyutyuNo2 & "'"
                    Sql1 += " AND t44.受注番号枝番 = '" & strEda2 & "'"
                    Sql1 += " AND t45.出庫区分 = '" & CommonConst.SHUKO_KBN_TMP & "'" '仮出庫のものを取得
                    Sql1 += " AND t44.取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'" '見取消のもの

                    Dim dsShukodt As DataSet = _db.selectDB(Sql1, RS, reccnt)


                    '該当データがあったら
                    'If shukkoTmpData.Tables(RS).Rows.Count > 0 Then
                    For x As Integer = 0 To dsShukodt.Tables(RS).Rows.Count - 1

                        Dim strSyukoNo As String = dsShukodt.Tables(RS).Rows(x)("出庫番号").ToString
                        Dim strGyo As String = dsShukodt.Tables(RS).Rows(x)("行番号").ToString


                        't45_shukodt
                        Sql1 = "UPDATE t45_shukodt"

                        Sql1 += " SET "
                        Sql1 += " 更新日 = '" & UtilClass.formatDatetime(dtNow) & "'"
                        Sql1 += ",更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"

                        Sql1 += " WHERE "
                        Sql1 += "     会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                        Sql1 += " AND 出庫番号 ='" & strSyukoNo & "'"
                        Sql1 += " AND 行番号 ='" & strGyo & "'"

                        _db.executeDB(Sql1)


#Region "inout"

                        't70_inout
                        Sql1 = "UPDATE t70_inout "
                        Sql1 += " SET "
                        Sql1 += " 取消区分 = " & CommonConst.CANCEL_KBN_DISABLED.ToString
                        Sql1 += ",取消日 = '" & UtilClass.formatDatetime(dtNow) & "'"
                        Sql1 += ",更新日 = '" & UtilClass.formatDatetime(dtNow) & "'"
                        Sql1 += ",更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"

                        Sql1 += " WHERE "
                        Sql1 += "     会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                        Sql1 += " AND 伝票番号 ='" & strSyukoNo & "'"
                        Sql1 += " AND 行番号 ='" & strGyo & "'"


                        _db.executeDB(Sql1)

#End Region

                    Next



#Region "t44_shukohd"

                    't44_shukohd
                    Sql1 = "UPDATE "
                    Sql1 += " t44_shukohd "
                    Sql1 += " SET "
                    Sql1 += " 取消区分 = " & CommonConst.CANCEL_KBN_DISABLED.ToString
                    Sql1 += ",取消日 = '" & UtilClass.formatDatetime(dtNow) & "'"
                    Sql1 += ",更新日 = '" & UtilClass.formatDatetime(dtNow) & "'"
                    Sql1 += ",更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"

                    Sql1 += " WHERE "
                    Sql1 += " 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                    Sql1 += " AND "
                    Sql1 += " 受注番号 = '" & strJyutyuNo2 & "'"
                    Sql1 += " AND "
                    Sql1 += " 受注番号枝番 = '" & strEda2 & "'"
                    Sql1 += " AND 取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'" '見取消のもの

                    _db.executeDB(Sql1)

#End Region

                End If
            Next


#Region "cymn"

            '受発注、在庫引当全てのデータを取消にする
            Sql1 = "UPDATE "
            Sql1 += " Public.t10_cymnhd "
            Sql1 += " SET "
            Sql1 += " 取消区分 = " & CommonConst.CANCEL_KBN_DISABLED.ToString
            Sql1 += ", 取消日 = '" & UtilClass.formatDatetime(dtNow) & "'"
            Sql1 += ", 更新日 = '" & UtilClass.formatDatetime(dtNow) & "'"
            Sql1 += ", 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"

            Sql1 += " WHERE 会社コード ='" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql1 += " AND 受注番号 ='" & strJyutyuNo & "'"
            Sql1 += " AND 受注番号枝番 ='" & strEda & "'"

            _db.executeDB(Sql1)


            Sql1 = "UPDATE "
            Sql1 += " Public.t11_cymndt "
            Sql1 += " SET "
            'Sql1 += "  更新日 = '" & UtilClass.formatDatetime(dtNow) & "'"
            Sql1 += " 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"

            Sql1 += " WHERE 会社コード ='" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql1 += " AND 受注番号 ='" & strJyutyuNo & "'"
            Sql1 += " AND 受注番号枝番 ='" & strEda & "'"

            _db.executeDB(Sql1)

#End Region



#Region "t01_mithd"

            't01_mithd　受注日をクリア
            Sql1 = "UPDATE "
            Sql1 += " Public.t01_mithd "
            Sql1 += " SET "
            Sql1 += " 受注日 = null "
            Sql1 += ",更新日 = '" & UtilClass.formatDatetime(dtNow) & "'"
            Sql1 += ",更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"

            Sql1 += " WHERE 会社コード ='" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql1 += " AND 見積番号 ='" & strMitumoriNo & "'"
            Sql1 += " AND 見積番号枝番 ='" & strMitumoriEda & "'"

            _db.executeDB(Sql1)

#End Region

            mOutCancel = True

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Function


    '受注データに対応する発注データを取り消す
    Private Function mOrderCancel() As Boolean

        Dim dtNow As String = UtilClass.formatDatetime(DateTime.Now)
        Dim Sql1 As String = ""

        mOrderCancel = False

        Try

            Sql1 = "UPDATE Public.t20_hattyu "
            Sql1 += "SET "

            Sql1 += " 取消区分 = " & CommonConst.CANCEL_KBN_DISABLED
            Sql1 += ", 取消日 = '" & UtilClass.formatDatetime(dtNow) & "'"
            Sql1 += ", 更新日 = '" & UtilClass.formatDatetime(dtNow) & "'"
            Sql1 += ", 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "' "

            Sql1 += "WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql1 += " AND 受注番号 = '" & DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("受注番号").Value & "'"
            Sql1 += " AND 受注番号枝番 = '" & DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("受注番号枝番").Value & "'"

            _db.executeDB(Sql1)


            mOrderCancel = True

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Function


    '取消データを含めるイベントの取得
    Private Sub ChkCancelData_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCancelData.CheckedChanged

        OrderListLoad() '一覧を再表示

    End Sub

    '受注複写
    Private Sub BtnOrderClone_Click(sender As Object, e As EventArgs) Handles BtnOrderClone.Click
        'グリッドに何もないときは次画面へ移動しない
        If Me.DgvCymnhd.RowCount = 0 Then
            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Exit Sub
        End If

        '取消済みデータは取消操作不可能
        If DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("取消").Value =
            IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_JPN, CommonConst.CANCEL_KBN_JPN_TXT, CommonConst.CANCEL_KBN_ENG_TXT) Then
            '取消データは選択できないアラートを出す
            _msgHd.dspMSG("cannotSelectTorikeshiData", frmC01F10_Login.loginValue.Language)
            Exit Sub
        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvCymnhd.CurrentCell.RowIndex
        Dim No As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号").Value
        Dim Suffix As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号枝番").Value
        Dim Status As String = CommonConst.STATUS_CLONE
        Dim openForm As Form = Nothing
        openForm = New Order(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
        Me.Enabled = False
        Me.Hide()
        openForm.ShowDialog(Me)

        OrderListLoad() '一覧を再表示
    End Sub

    '請求登録
    Private Sub BtnBill_Click(sender As Object, e As EventArgs) Handles BtnBill.Click
        'グリッドに何もないときは次画面へ移動しない
        If Me.DgvCymnhd.RowCount = 0 Then
            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Exit Sub
        End If

        '取消済みデータは取消操作不可能
        If DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("取消").Value =
            IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_JPN, CommonConst.CANCEL_KBN_JPN_TXT, CommonConst.CANCEL_KBN_ENG_TXT) Then
            '取消データは選択できないアラートを出す
            _msgHd.dspMSG("cannotSelectTorikeshiData", frmC01F10_Login.loginValue.Language)
            Exit Sub
        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvCymnhd.CurrentCell.RowIndex
        Dim No As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号").Value
        Dim Suffix As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号枝番").Value
        Dim openForm As Form = Nothing
        openForm = New BillingManagement(_msgHd, _db, _langHd, Me, No, Suffix, "")   '処理選択
        Me.Enabled = False
        Me.Hide()
        openForm.ShowDialog(Me)

        OrderListLoad() '一覧表示
    End Sub

    '使用言語に合わせて受注基本見出しを切替
    Private Sub setHdColumns()
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            DgvCymnhd.Columns.Add("取消", "Cancel")
            DgvCymnhd.Columns.Add("受注番号", "OrderNumber")
            DgvCymnhd.Columns.Add("受注番号枝番", "JobOrderSubNumber")

            DgvCymnhd.Columns.Add("客先番号", "CustomerNumber")
            DgvCymnhd.Columns.Add("受注日", "JobOrderDate")
            DgvCymnhd.Columns.Add("見積番号", "QuotationNumber")
            DgvCymnhd.Columns.Add("見積番号枝番", "BranchNumber")
            DgvCymnhd.Columns.Add("見積日", "QuotationDate")
            DgvCymnhd.Columns.Add("見積有効期限", "QuotationExpriedDate")
            DgvCymnhd.Columns.Add("得意先コード", "CustomerCode")
            DgvCymnhd.Columns.Add("得意先名", "CustomerName")

            DgvCymnhd.Columns.Add("通貨_外貨", "Currency")
            DgvCymnhd.Columns.Add("受注金額_外貨", "OrderAmount" & vbCrLf & "(OrignalCurrency)")

            DgvCymnhd.Columns.Add("受注金額", "OrderAmount" & vbCrLf & "(" & setBaseCurrency() & ")")
            DgvCymnhd.Columns.Add("ＶＡＴ", "VAT-OUT")

            DgvCymnhd.Columns.Add("仕入金額", "PurchaseAmount" & vbCrLf & "(" & setBaseCurrency() & ")")
            DgvCymnhd.Columns.Add("粗利額", "Profitmargin" & vbCrLf & "(" & setBaseCurrency() & ")")
            DgvCymnhd.Columns.Add("粗利率", "ProfitmarginRate(%)")


            DgvCymnhd.Columns.Add("得意先郵便番号", "PostalCode")
            DgvCymnhd.Columns.Add("得意先住所", "Address")
            DgvCymnhd.Columns.Add("得意先電話番号", "PhoneNumber")
            DgvCymnhd.Columns.Add("得意先ＦＡＸ", "FAX")
            DgvCymnhd.Columns.Add("得意先担当者名", "NameOfPIC")
            DgvCymnhd.Columns.Add("得意先担当者役職", "PositionPICCustomer")

            DgvCymnhd.Columns.Add("支払条件", "PeymentTerms")
            DgvCymnhd.Columns.Add("営業担当者", "SalesPersonInCharge")
            DgvCymnhd.Columns.Add("入力担当者", "PICForInputting")
            DgvCymnhd.Columns.Add("備考", "Remarks")
            DgvCymnhd.Columns.Add("登録日", "RegistrationDate")
            DgvCymnhd.Columns.Add("更新日", "UpdateDate")
            DgvCymnhd.Columns.Add("更新者", "ModifiedBy")

        Else

            DgvCymnhd.Columns.Add("取消", "取消")
            DgvCymnhd.Columns.Add("受注番号", "受注番号")
            DgvCymnhd.Columns.Add("受注番号枝番", "受注Ver")

            DgvCymnhd.Columns.Add("客先番号", "客先番号")
            DgvCymnhd.Columns.Add("受注日", "受注日")
            DgvCymnhd.Columns.Add("見積番号", "見積番号")
            DgvCymnhd.Columns.Add("見積番号枝番", "見積番号枝番")
            DgvCymnhd.Columns.Add("見積日", "見積日")
            DgvCymnhd.Columns.Add("見積有効期限", "見積有効期限")
            DgvCymnhd.Columns.Add("得意先コード", "得意先コード")
            DgvCymnhd.Columns.Add("得意先名", "得意先名")

            DgvCymnhd.Columns.Add("通貨_外貨", "販売通貨")
            DgvCymnhd.Columns.Add("受注金額_外貨", "受注金額" & vbCrLf & "(原通貨)")

            DgvCymnhd.Columns.Add("受注金額", "受注金額" & vbCrLf & "(" & setBaseCurrency() & ")")
            DgvCymnhd.Columns.Add("ＶＡＴ", "VAT-OUT")

            DgvCymnhd.Columns.Add("仕入金額", "仕入金額" & vbCrLf & "(" & setBaseCurrency() & ")")
            DgvCymnhd.Columns.Add("粗利額", "利益" & vbCrLf & "(" & setBaseCurrency() & ")")
            DgvCymnhd.Columns.Add("粗利率", "利益率(%)")


            DgvCymnhd.Columns.Add("得意先郵便番号", "得意先郵便番号")
            DgvCymnhd.Columns.Add("得意先住所", "得意先住所")
            DgvCymnhd.Columns.Add("得意先電話番号", "得意先電話番号")
            DgvCymnhd.Columns.Add("得意先ＦＡＸ", "得意先ＦＡＸ")
            DgvCymnhd.Columns.Add("得意先担当者名", "得意先担当者名")
            DgvCymnhd.Columns.Add("得意先担当者役職", "得意先担当者役職")

            DgvCymnhd.Columns.Add("支払条件", "支払条件")
            DgvCymnhd.Columns.Add("営業担当者", "営業担当者")
            DgvCymnhd.Columns.Add("入力担当者", "入力担当者")
            DgvCymnhd.Columns.Add("備考", "備考")
            DgvCymnhd.Columns.Add("登録日", "登録日")
            DgvCymnhd.Columns.Add("更新日", "最終更新日")
            DgvCymnhd.Columns.Add("更新者", "更新者")
        End If

        DgvCymnhd.Columns("受注金額_外貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("受注金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("粗利額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("粗利率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DgvCymnhd.Columns("ＶＡＴ").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


        '数字形式
        DgvCymnhd.Columns("受注金額_外貨").DefaultCellStyle.Format = "N2"
        DgvCymnhd.Columns("受注金額").DefaultCellStyle.Format = "N2"
        DgvCymnhd.Columns("仕入金額").DefaultCellStyle.Format = "N2"
        DgvCymnhd.Columns("粗利額").DefaultCellStyle.Format = "N2"
        DgvCymnhd.Columns("粗利率").DefaultCellStyle.Format = "N1"

        DgvCymnhd.Columns("ＶＡＴ").DefaultCellStyle.Format = "N2"


        DgvCymnhd.Columns("見積有効期限").Visible = False
        DgvCymnhd.Columns("更新者").Visible = False

        '日付表示
        DgvCymnhd.Columns("受注日").DefaultCellStyle.Format = "d"
        DgvCymnhd.Columns("見積日").DefaultCellStyle.Format = "d"
        DgvCymnhd.Columns("見積有効期限").DefaultCellStyle.Format = "d"

        '見出しの文字位置
        DgvCymnhd.ReadOnly = False
        'DataGridViewの列幅
        DgvCymnhd.AllowUserToResizeColumns = True
        'DgvCymnhd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.AllCells
        'DgvCymnhd.AllowUserToResizeRows = True
        '列ヘッダー高さ
        'DgvCymnhd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        'DgvCymnhd.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        'DgvCymnhd.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        'DgvCymnhd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize

        DgvCymnhd.Columns("取消").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("取消").ReadOnly = True
        DgvCymnhd.Columns("受注番号").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("受注番号").ReadOnly = True
        DgvCymnhd.Columns("受注番号枝番").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("受注番号枝番").ReadOnly = True
        DgvCymnhd.Columns("客先番号").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("客先番号").ReadOnly = True
        DgvCymnhd.Columns("受注日").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("受注日").ReadOnly = True
        DgvCymnhd.Columns("見積番号").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("見積番号").ReadOnly = True
        DgvCymnhd.Columns("見積番号枝番").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("見積番号枝番").ReadOnly = True
        DgvCymnhd.Columns("見積日").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("見積日").ReadOnly = True
        DgvCymnhd.Columns("見積有効期限").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("見積有効期限").ReadOnly = True
        DgvCymnhd.Columns("得意先コード").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("得意先コード").ReadOnly = True
        DgvCymnhd.Columns("得意先名").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("得意先名").ReadOnly = True
        DgvCymnhd.Columns("通貨_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("通貨_外貨").ReadOnly = True
        DgvCymnhd.Columns("受注金額_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("受注金額_外貨").ReadOnly = True
        DgvCymnhd.Columns("受注金額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("受注金額").ReadOnly = True
        DgvCymnhd.Columns("ＶＡＴ").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("ＶＡＴ").ReadOnly = True
        DgvCymnhd.Columns("仕入金額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("仕入金額").ReadOnly = True
        DgvCymnhd.Columns("粗利額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("粗利額").ReadOnly = True
        DgvCymnhd.Columns("粗利率").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("粗利率").ReadOnly = True
        DgvCymnhd.Columns("得意先郵便番号").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("得意先郵便番号").ReadOnly = True
        DgvCymnhd.Columns("得意先住所").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("得意先住所").ReadOnly = True
        DgvCymnhd.Columns("得意先電話番号").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("得意先電話番号").ReadOnly = True
        DgvCymnhd.Columns("得意先ＦＡＸ").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("得意先ＦＡＸ").ReadOnly = True
        DgvCymnhd.Columns("得意先担当者名").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("得意先担当者名").ReadOnly = True
        DgvCymnhd.Columns("得意先担当者役職").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("得意先担当者役職").ReadOnly = True
        DgvCymnhd.Columns("支払条件").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("支払条件").ReadOnly = True
        DgvCymnhd.Columns("営業担当者").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("営業担当者").ReadOnly = True
        DgvCymnhd.Columns("入力担当者").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("入力担当者").ReadOnly = True
        DgvCymnhd.Columns("備考").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("備考").ReadOnly = True
        DgvCymnhd.Columns("登録日").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("登録日").ReadOnly = True
        DgvCymnhd.Columns("更新日").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("更新日").ReadOnly = True
        DgvCymnhd.Columns("更新者").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("更新者").ReadOnly = True

        'DataGridView1のすべての列の幅を自動調整する
        'DgvCymnhd.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
        'DataGridView1のすべての行の高さを自動調整する
        'DgvCymnhd.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)


    End Sub

    '使用言語に合わせて受注明細見出しを切替
    Private Sub setDtColumns()

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            DgvCymnhd.Columns.Add("取消", "Cancel")
            DgvCymnhd.Columns.Add("受注番号", "OrderNumber")
            DgvCymnhd.Columns.Add("受注番号枝番", "OrderSuffixNumber")
            DgvCymnhd.Columns.Add("行番号", "LineNumber")

            DgvCymnhd.Columns.Add("仕入区分", "PurchasingClassification")
            DgvCymnhd.Columns.Add("メーカー", "Manufacturer")
            DgvCymnhd.Columns.Add("品名", "ItemName")
            DgvCymnhd.Columns.Add("型式", "Spec")
            DgvCymnhd.Columns.Add("仕入先名", "SupplierName")
            DgvCymnhd.Columns.Add("仕入値", "PurchaseAmount")
            DgvCymnhd.Columns.Add("受注数量", "OrderQuantity")
            DgvCymnhd.Columns.Add("売上数量", "SalesQuantity")
            DgvCymnhd.Columns.Add("受注残数", "OrderRemaining")
            DgvCymnhd.Columns.Add("単位", "Unit")
            DgvCymnhd.Columns.Add("間接費", "OverHead")
            DgvCymnhd.Columns.Add("売単価", "SellingPrice")
            DgvCymnhd.Columns.Add("売上金額", "SalesAmount")
            DgvCymnhd.Columns.Add("粗利額", "GrossProfit")
            DgvCymnhd.Columns.Add("粗利率", "GrossMarginRate")
            DgvCymnhd.Columns.Add("リードタイム", "LeadTime")
            DgvCymnhd.Columns.Add("出庫数", "GoodsDeliveryQuantity")
            DgvCymnhd.Columns.Add("未出庫数", "NoGoodsDeliveryQuantity")
            DgvCymnhd.Columns.Add("備考", "Remarks")
            DgvCymnhd.Columns.Add("更新者", "ModifiedBy")
            DgvCymnhd.Columns.Add("登録日", "Registration")
        Else
            DgvCymnhd.Columns.Add("取消", "取消")
            DgvCymnhd.Columns.Add("受注番号", "受注番号")
            DgvCymnhd.Columns.Add("受注番号枝番", "受注番号枝番")
            DgvCymnhd.Columns.Add("行番号", "行番号")

            DgvCymnhd.Columns.Add("仕入区分", "仕入区分")
            DgvCymnhd.Columns.Add("メーカー", "メーカー")
            DgvCymnhd.Columns.Add("品名", "品名")
            DgvCymnhd.Columns.Add("型式", "型式")
            DgvCymnhd.Columns.Add("仕入先名", "仕入先名")
            DgvCymnhd.Columns.Add("仕入値", "仕入値")
            DgvCymnhd.Columns.Add("受注数量", "受注数量")
            DgvCymnhd.Columns.Add("売上数量", "売上数量")
            DgvCymnhd.Columns.Add("受注残数", "受注残数")
            DgvCymnhd.Columns.Add("単位", "単位")
            DgvCymnhd.Columns.Add("間接費", "間接費")
            DgvCymnhd.Columns.Add("売単価", "売単価")
            DgvCymnhd.Columns.Add("売上金額", "売上金額")
            DgvCymnhd.Columns.Add("粗利額", "粗利額")
            DgvCymnhd.Columns.Add("粗利率", "粗利率")
            DgvCymnhd.Columns.Add("リードタイム", "リードタイム")
            DgvCymnhd.Columns.Add("出庫数", "出庫数")
            DgvCymnhd.Columns.Add("未出庫数", "未出庫数")
            DgvCymnhd.Columns.Add("備考", "備考")
            DgvCymnhd.Columns.Add("更新者", "更新者")
            DgvCymnhd.Columns.Add("登録日", "登録日")
        End If


        DgvCymnhd.Columns("行番号").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("仕入値").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("受注数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("売上数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("受注残数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("間接費").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("売単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("売上金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("粗利額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("粗利率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DgvCymnhd.Columns("出庫数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCymnhd.Columns("未出庫数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        '数字形式
        DgvCymnhd.Columns("仕入値").DefaultCellStyle.Format = "N2"
        DgvCymnhd.Columns("受注数量").DefaultCellStyle.Format = "N2"
        DgvCymnhd.Columns("売上数量").DefaultCellStyle.Format = "N2"
        DgvCymnhd.Columns("受注残数").DefaultCellStyle.Format = "N2"
        DgvCymnhd.Columns("間接費").DefaultCellStyle.Format = "N2"
        DgvCymnhd.Columns("売単価").DefaultCellStyle.Format = "N2"
        DgvCymnhd.Columns("売上金額").DefaultCellStyle.Format = "N2"
        DgvCymnhd.Columns("粗利額").DefaultCellStyle.Format = "N2"
        DgvCymnhd.Columns("粗利率").DefaultCellStyle.Format = "N1"

        DgvCymnhd.Columns("出庫数").DefaultCellStyle.Format = "N2"
        DgvCymnhd.Columns("未出庫数").DefaultCellStyle.Format = "N2"

        '見出しの文字位置
        DgvCymnhd.ReadOnly = False
        'DataGridViewの列幅
        'DgvCymnhd.AllowUserToResizeColumns = True
        'DgvCymnhd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.AllCells
        'DgvCymnhd.AllowUserToResizeRows = True
        '列ヘッダー高さ
        'DgvCymnhd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        'DgvCymnhd.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        'DgvCymnhd.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        'DgvCymnhd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize

        DgvCymnhd.Columns("取消").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("取消").ReadOnly = True
        DgvCymnhd.Columns("受注番号").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("受注番号").ReadOnly = True
        DgvCymnhd.Columns("受注番号枝番").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("受注番号枝番").ReadOnly = True
        DgvCymnhd.Columns("行番号").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("行番号").ReadOnly = True
        DgvCymnhd.Columns("仕入区分").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("仕入区分").ReadOnly = True
        DgvCymnhd.Columns("メーカー").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("メーカー").ReadOnly = True
        DgvCymnhd.Columns("品名").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("品名").ReadOnly = True
        DgvCymnhd.Columns("型式").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("型式").ReadOnly = True
        DgvCymnhd.Columns("仕入先名").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("仕入先名").ReadOnly = True
        DgvCymnhd.Columns("仕入値").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("仕入値").ReadOnly = True
        DgvCymnhd.Columns("受注数量").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("受注数量").ReadOnly = True
        DgvCymnhd.Columns("売上数量").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("売上数量").ReadOnly = True
        DgvCymnhd.Columns("受注残数").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("受注残数").ReadOnly = True
        DgvCymnhd.Columns("単位").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("単位").ReadOnly = True
        DgvCymnhd.Columns("間接費").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("間接費").ReadOnly = True
        DgvCymnhd.Columns("売単価").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("売単価").ReadOnly = True
        DgvCymnhd.Columns("売上金額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("売上金額").ReadOnly = True
        DgvCymnhd.Columns("粗利額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("粗利額").ReadOnly = True
        DgvCymnhd.Columns("粗利率").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("粗利率").ReadOnly = True
        DgvCymnhd.Columns("リードタイム").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("リードタイム").ReadOnly = True
        DgvCymnhd.Columns("出庫数").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("出庫数").ReadOnly = True
        DgvCymnhd.Columns("未出庫数").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("未出庫数").ReadOnly = True
        DgvCymnhd.Columns("備考").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("備考").ReadOnly = True
        DgvCymnhd.Columns("更新者").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("更新者").ReadOnly = True
        DgvCymnhd.Columns("登録日").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCymnhd.Columns("登録日").ReadOnly = True

        'DataGridView1のすべての列の幅を自動調整する
        'DgvCymnhd.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
        'DataGridView1のすべての行の高さを自動調整する
        'DgvCymnhd.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)

    End Sub

    '抽出条件取得
    Private Function searchConditions() As String

        Dim Sql As String = ""

        '抽出条件
        Dim customerName As String = escapeSql(TxtCustomerName.Text)
        Dim customerAddress As String = escapeSql(TxtAddress.Text)
        Dim customerTel As String = escapeSql(TxtTel.Text)
        Dim customerCode As String = escapeSql(TxtCustomerCode.Text)
        Dim sinceDate As String = UtilClass.strFormatDate(dtOrderDateSince.Text)
        Dim untilDate As String = UtilClass.strFormatDate(dtOrderDateUntil.Text)
        Dim sinceNum As String = escapeSql(TxtOrderSince.Text)
        Dim salesName As String = escapeSql(TxtSales.Text)
        Dim customerPO As String = escapeSql(TxtCustomerPO.Text)

        If customerName <> Nothing Then
            Sql += " And 得意先名 ILIKE '%" & customerName & "%' "
        End If

        If customerAddress <> Nothing Then
            Sql += " AND 得意先住所 ILIKE '%" & customerAddress & "%' "
        End If

        If customerTel <> Nothing Then
            Sql += " AND 得意先電話番号 ILIKE '%" & customerTel & "%' "
        End If

        If customerCode <> Nothing Then
            Sql += " AND 得意先コード ILIKE '%" & customerCode & "%' "
        End If

        If sinceDate <> Nothing Then
            Sql += " AND 受注日 >= '" & sinceDate & "'"
        End If
        If untilDate <> Nothing Then
            Sql += " AND 受注日 <= '" & untilDate & "'"
        End If

        If sinceNum <> Nothing Then
            Sql += " AND 受注番号 ILIKE '%" & sinceNum & "%' "
        End If

        If salesName <> Nothing Then
            Sql += " AND 営業担当者 ILIKE '%" & salesName & "%' "
        End If

        If customerPO <> Nothing Then
            Sql += " AND 客先番号 ILIKE '%" & customerPO & "%' "
        End If

        Return Sql

    End Function

    Private Function viewSearchConditions() As String
        Dim Sql As String = ""

        '抽出条件
        Dim customerName As String = UtilClass.escapeSql(TxtCustomerName.Text)
        Dim customerAddress As String = UtilClass.escapeSql(TxtAddress.Text)
        Dim customerTel As String = UtilClass.escapeSql(TxtTel.Text)
        Dim customerCode As String = UtilClass.escapeSql(TxtCustomerCode.Text)
        Dim sinceDate As String = UtilClass.strFormatDate(dtOrderDateSince.Text)
        Dim untilDate As String = UtilClass.strFormatDate(dtOrderDateUntil.Text)
        Dim sinceNum As String = UtilClass.escapeSql(TxtOrderSince.Text)
        Dim salesName As String = UtilClass.escapeSql(TxtSales.Text)
        Dim customerPO As String = UtilClass.escapeSql(TxtCustomerPO.Text)
        Dim itemName As String = UtilClass.escapeSql(TxtItemName.Text)
        Dim spec As String = UtilClass.escapeSql(TxtSpec.Text)
        Dim Maker As String = UtilClass.escapeSql(txtMaker.Text)
        Dim PurchaseSince As String = UtilClass.escapeSql(txtPurchaseSince.Text)


        If customerName <> Nothing Then
            Sql += " AND t10.得意先名 ILIKE '%" & customerName & "%' "
        End If

        If customerAddress <> Nothing Then
            Sql += " AND t10.得意先住所 ILIKE '%" & customerAddress & "%' "
        End If

        If customerTel <> Nothing Then
            Sql += " AND t10.得意先電話番号 ILIKE '%" & customerTel & "%' "
        End If

        If customerCode <> Nothing Then
            Sql += " AND t10.得意先コード ILIKE '%" & customerCode & "%' "
        End If

        If sinceDate <> Nothing Then
            Sql += " AND t10.受注日 >= '" & sinceDate & "'"
        End If
        If untilDate <> Nothing Then
            Sql += " AND t10.受注日 <= '" & untilDate & "'"
        End If

        If sinceNum <> Nothing Then
            Sql += " AND t10.受注番号 ILIKE '%" & sinceNum & "%' "
        End If

        If salesName <> Nothing Then
            Sql += " AND t10.営業担当者 ILIKE '%" & salesName & "%' "
        End If

        If customerPO <> Nothing Then
            Sql += " AND t10.客先番号 ILIKE '%" & customerPO & "%' "
        End If

        If Maker <> Nothing Then
            Sql += " AND t11.メーカー ILIKE '%" & Maker & "%' "
        End If

        If itemName <> Nothing Then
            Sql += " AND t11.品名 ILIKE '%" & itemName & "%' "
        End If

        If spec <> Nothing Then
            Sql += " AND t11.型式 ILIKE '%" & spec & "%' "
        End If

        If PurchaseSince <> Nothing Then
            'Sql += " AND t20.発注番号 ILIKE '%" & PurchaseSince & "%' "
            Sql += "And t10.受注番号 In (Select 受注番号 From t20_hattyu t20 Where 発注番号 In('" & PurchaseSince & "'))"
        End If

        '取消データを含めない場合
        If ChkCancelData.Checked = False Then
            Sql += " AND t10.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
        End If

        Return Sql
    End Function

    Private Function actionChk() As Boolean
        '対象データがない場合は取消操作不可能
        If DgvCymnhd.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)

            Return False

        Else

            Return True

        End If
    End Function

    '表示形式条件
    Private Function viewFormat() As String
        Dim Sql As String = ""

        '取消データを含めない場合
        If ChkCancelData.Checked = False Then
            Sql += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
        End If

        Return Sql

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

    'param1：String テーブル名
    'param2：String 詳細条件
    'Return: DataSet
    Private Function getDsData(ByVal tableName As String, Optional ByRef txtParam As String = "") As DataSet
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Sql += "SELECT * FROM public." & tableName
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' "
        Sql += txtParam
        Return _db.selectDB(Sql, RS, reccnt)
    End Function

    'sqlで実行する文字列からシングルクォーテーションを文字コードにする
    Private Function escapeSql(ByVal prmSql As String) As String
        Dim sql As String = prmSql

        sql = sql.Replace("'"c, "''") 'シングルクォーテーションを置換

        Return Regex.Escape(sql)
        Return sql
    End Function

    '汎用マスタから固定キー、可変キーに応じた結果を返す
    'param1：String 固定キー
    'param2：String 可変キー
    'Return: DataSet
    Private Function getDsHanyoData(ByVal prmFixed As String, Optional ByVal prmVariable As String = "") As DataSet
        Dim Sql As String = ""

        Sql = " AND 固定キー = '" & prmFixed & "'"

        If prmVariable IsNot "" Then
            Sql += " AND 可変キー = '" & prmVariable & "'"
        End If

        'リードタイムのリストを汎用マスタから取得
        Return getDsData("m90_hanyo", Sql)

    End Function

    Private Sub OrderList_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        'OrderListLoad() '一覧を再表示
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
End Class