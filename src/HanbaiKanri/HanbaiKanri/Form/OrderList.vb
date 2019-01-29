Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


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
                   Optional ByRef prmRefStatus As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        OrderStatus = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True
        DgvCymnhd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells
    End Sub
    Private Sub OrderListLoad(Optional ByRef prmRefStatus As String = "")
        Dim Status As String = prmRefStatus
        Dim Sql As String = ""

        If Status = "EXCLUSION" Then
            Try
                Sql += "SELECT "
                Sql += "* "
                Sql += "FROM "
                Sql += "public"
                Sql += "."
                Sql += "t10_cymnhd"
                Sql += " WHERE "
                Sql += "取消区分"
                Sql += " = "
                Sql += "'"
                Sql += "0"
                Sql += "'"
                Sql += " ORDER BY "
                Sql += "登録日 DESC"

                Dim reccnt As Integer = 0
                ds = _db.selectDB(Sql, RS, reccnt)
                If frmC01F10_Login.loginValue.Language = "ENG" Then
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
                    DgvCymnhd.Columns.Add("得意先郵便番号", "PostalCode")
                    DgvCymnhd.Columns.Add("得意先住所", "Address")
                    DgvCymnhd.Columns.Add("得意先電話番号", "PhoneNumber")
                    DgvCymnhd.Columns.Add("得意先ＦＡＸ", "FAX")
                    DgvCymnhd.Columns.Add("得意先担当者名", "NameOfPIC")
                    DgvCymnhd.Columns.Add("得意先担当者役職", "PositionPICCustomer")
                    DgvCymnhd.Columns.Add("ＶＡＴ", "VAT")
                    DgvCymnhd.Columns.Add("受注金額", "OrderAmount")
                    DgvCymnhd.Columns.Add("仕入金額", "PurchaseAmount")
                    DgvCymnhd.Columns.Add("粗利額", "GrossMargin")
                    DgvCymnhd.Columns.Add("支払条件", "PeymentTerms")
                    DgvCymnhd.Columns.Add("営業担当者", "SalesPersonInCharge")
                    DgvCymnhd.Columns.Add("入力担当者", "PICForInputting")
                    DgvCymnhd.Columns.Add("備考", "Remarks")
                    DgvCymnhd.Columns.Add("登録日", "RegistrationDate")
                Else
                    DgvCymnhd.Columns.Add("受注番号", "受注番号")
                    DgvCymnhd.Columns.Add("受注番号枝番", "受注番号枝番")
                    DgvCymnhd.Columns.Add("客先番号", "客先番号")
                    DgvCymnhd.Columns.Add("受注日", "受注日")
                    DgvCymnhd.Columns.Add("見積番号", "見積番号")
                    DgvCymnhd.Columns.Add("見積番号枝番", "見積番号枝番")
                    DgvCymnhd.Columns.Add("見積日", "見積日")
                    DgvCymnhd.Columns.Add("見積有効期限", "見積有効期限")
                    DgvCymnhd.Columns.Add("得意先コード", "得意先コード")
                    DgvCymnhd.Columns.Add("得意先名", "得意先名")
                    DgvCymnhd.Columns.Add("得意先郵便番号", "得意先郵便番号")
                    DgvCymnhd.Columns.Add("得意先住所", "得意先住所")
                    DgvCymnhd.Columns.Add("得意先電話番号", "得意先電話番号")
                    DgvCymnhd.Columns.Add("得意先ＦＡＸ", "得意先ＦＡＸ")
                    DgvCymnhd.Columns.Add("得意先担当者名", "得意先担当者名")
                    DgvCymnhd.Columns.Add("得意先担当者役職", "得意先担当者役職")
                    DgvCymnhd.Columns.Add("ＶＡＴ", "ＶＡＴ")
                    DgvCymnhd.Columns.Add("受注金額", "受注金額")
                    DgvCymnhd.Columns.Add("仕入金額", "仕入金額")
                    DgvCymnhd.Columns.Add("粗利額", "粗利額")
                    DgvCymnhd.Columns.Add("支払条件", "支払条件")
                    DgvCymnhd.Columns.Add("営業担当者", "営業担当者")
                    DgvCymnhd.Columns.Add("入力担当者", "入力担当者")
                    DgvCymnhd.Columns.Add("備考", "備考")
                    DgvCymnhd.Columns.Add("登録日", "登録日")
                End If

                DgvCymnhd.Columns("ＶＡＴ").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvCymnhd.Columns("受注金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvCymnhd.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvCymnhd.Columns("粗利額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                    DgvCymnhd.Rows.Add()
                    DgvCymnhd.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)("受注番号")
                    DgvCymnhd.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)("受注番号枝番")
                    DgvCymnhd.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)("客先番号")
                    DgvCymnhd.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)("受注日")
                    DgvCymnhd.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)("見積番号")
                    DgvCymnhd.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)("見積番号枝番")
                    DgvCymnhd.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)("見積日")
                    DgvCymnhd.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)("見積有効期限")
                    DgvCymnhd.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)("得意先コード")
                    DgvCymnhd.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)("得意先名")
                    DgvCymnhd.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)("得意先郵便番号")
                    DgvCymnhd.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)("得意先住所")
                    DgvCymnhd.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)("得意先電話番号")
                    DgvCymnhd.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)("得意先ＦＡＸ")
                    DgvCymnhd.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)("得意先担当者名")
                    DgvCymnhd.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)("得意先担当者役職")
                    DgvCymnhd.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)("ＶＡＴ")
                    DgvCymnhd.Rows(index).Cells(17).Value = ds.Tables(RS).Rows(index)("見積金額")
                    DgvCymnhd.Rows(index).Cells(18).Value = ds.Tables(RS).Rows(index)("仕入金額")
                    DgvCymnhd.Rows(index).Cells(19).Value = ds.Tables(RS).Rows(index)("粗利額")
                    DgvCymnhd.Rows(index).Cells(20).Value = ds.Tables(RS).Rows(index)("支払条件")
                    DgvCymnhd.Rows(index).Cells(21).Value = ds.Tables(RS).Rows(index)("営業担当者")
                    DgvCymnhd.Rows(index).Cells(22).Value = ds.Tables(RS).Rows(index)("入力担当者")
                    DgvCymnhd.Rows(index).Cells(23).Value = ds.Tables(RS).Rows(index)("備考")
                    DgvCymnhd.Rows(index).Cells(24).Value = ds.Tables(RS).Rows(index)("登録日")
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
                Sql += "SELECT "
                Sql += "*  "
                Sql += "FROM "
                Sql += "public"
                Sql += "."
                Sql += "t10_cymnhd"
                Sql += " ORDER BY "
                Sql += "登録日 DESC"

                Dim reccnt As Integer = 0
                ds = _db.selectDB(Sql, RS, reccnt)
                If frmC01F10_Login.loginValue.Language = "ENG" Then
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
                    DgvCymnhd.Columns.Add("得意先郵便番号", "PostalCode")
                    DgvCymnhd.Columns.Add("得意先住所", "Address")
                    DgvCymnhd.Columns.Add("得意先電話番号", "PhoneNumber")
                    DgvCymnhd.Columns.Add("得意先ＦＡＸ", "FAX")
                    DgvCymnhd.Columns.Add("得意先担当者名", "NameOfPIC")
                    DgvCymnhd.Columns.Add("得意先担当者役職", "PositionPICCustomer")
                    DgvCymnhd.Columns.Add("ＶＡＴ", "VAT")
                    DgvCymnhd.Columns.Add("受注金額", "OrderAmount")
                    DgvCymnhd.Columns.Add("仕入金額", "PurchaseAmount")
                    DgvCymnhd.Columns.Add("粗利額", "GrossMargin")
                    DgvCymnhd.Columns.Add("支払条件", "PeymentTerms")
                    DgvCymnhd.Columns.Add("営業担当者", "SalesPersonInCharge")
                    DgvCymnhd.Columns.Add("入力担当者", "PICForInputting")
                    DgvCymnhd.Columns.Add("備考", "Remarks")
                    DgvCymnhd.Columns.Add("登録日", "RegistrationDate")
                Else
                    DgvCymnhd.Columns.Add("受注番号", "受注番号")
                    DgvCymnhd.Columns.Add("受注番号枝番", "受注番号枝番")
                    DgvCymnhd.Columns.Add("客先番号", "客先番号")
                    DgvCymnhd.Columns.Add("受注日", "受注日")
                    DgvCymnhd.Columns.Add("見積番号", "見積番号")
                    DgvCymnhd.Columns.Add("見積番号枝番", "見積番号枝番")
                    DgvCymnhd.Columns.Add("見積日", "見積日")
                    DgvCymnhd.Columns.Add("見積有効期限", "見積有効期限")
                    DgvCymnhd.Columns.Add("得意先コード", "得意先コード")
                    DgvCymnhd.Columns.Add("得意先名", "得意先名")
                    DgvCymnhd.Columns.Add("得意先郵便番号", "得意先郵便番号")
                    DgvCymnhd.Columns.Add("得意先住所", "得意先住所")
                    DgvCymnhd.Columns.Add("得意先電話番号", "得意先電話番号")
                    DgvCymnhd.Columns.Add("得意先ＦＡＸ", "得意先ＦＡＸ")
                    DgvCymnhd.Columns.Add("得意先担当者名", "得意先担当者名")
                    DgvCymnhd.Columns.Add("得意先担当者役職", "得意先担当者役職")
                    DgvCymnhd.Columns.Add("ＶＡＴ", "ＶＡＴ")
                    DgvCymnhd.Columns.Add("受注金額", "受注金額")
                    DgvCymnhd.Columns.Add("仕入金額", "仕入金額")
                    DgvCymnhd.Columns.Add("粗利額", "粗利額")
                    DgvCymnhd.Columns.Add("支払条件", "支払条件")
                    DgvCymnhd.Columns.Add("営業担当者", "営業担当者")
                    DgvCymnhd.Columns.Add("入力担当者", "入力担当者")
                    DgvCymnhd.Columns.Add("備考", "備考")
                    DgvCymnhd.Columns.Add("登録日", "登録日")
                End If

                DgvCymnhd.Columns("ＶＡＴ").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvCymnhd.Columns("受注金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvCymnhd.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvCymnhd.Columns("粗利額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                    DgvCymnhd.Rows.Add()
                    DgvCymnhd.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)("受注番号")
                    DgvCymnhd.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)("受注番号枝番")
                    DgvCymnhd.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)("客先番号")
                    DgvCymnhd.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)("受注日")
                    DgvCymnhd.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)("見積番号")
                    DgvCymnhd.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)("見積番号枝番")
                    DgvCymnhd.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)("見積日")
                    DgvCymnhd.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)("見積有効期限")
                    DgvCymnhd.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)("得意先コード")
                    DgvCymnhd.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)("得意先名")
                    DgvCymnhd.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)("得意先郵便番号")
                    DgvCymnhd.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)("得意先住所")
                    DgvCymnhd.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)("得意先電話番号")
                    DgvCymnhd.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)("得意先ＦＡＸ")
                    DgvCymnhd.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)("得意先担当者名")
                    DgvCymnhd.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)("得意先担当者役職")
                    DgvCymnhd.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)("ＶＡＴ")
                    DgvCymnhd.Rows(index).Cells(17).Value = ds.Tables(RS).Rows(index)("見積金額")
                    DgvCymnhd.Rows(index).Cells(18).Value = ds.Tables(RS).Rows(index)("仕入金額")
                    DgvCymnhd.Rows(index).Cells(19).Value = ds.Tables(RS).Rows(index)("粗利額")
                    DgvCymnhd.Rows(index).Cells(20).Value = ds.Tables(RS).Rows(index)("支払条件")
                    DgvCymnhd.Rows(index).Cells(21).Value = ds.Tables(RS).Rows(index)("営業担当者")
                    DgvCymnhd.Rows(index).Cells(22).Value = ds.Tables(RS).Rows(index)("入力担当者")
                    DgvCymnhd.Rows(index).Cells(23).Value = ds.Tables(RS).Rows(index)("備考")
                    DgvCymnhd.Rows(index).Cells(24).Value = ds.Tables(RS).Rows(index)("登録日")
                Next

            Catch ue As UsrDefException
                ue.dspMsg()
                Throw ue
            Catch ex As Exception
                'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
            End Try
        End If


    End Sub

    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If OrderStatus = "SALES" Then
            If frmC01F10_Login.loginValue.Language = "ENG" Then
                LblMode.Text = "SalesInputMode"
            Else
                LblMode.Text = "売上入力モード"
            End If

            BtnSales.Visible = True
            BtnSales.Location = New Point(997, 509)
        ElseIf OrderStatus = "GOODS_ISSUE" Then
            If frmC01F10_Login.loginValue.Language = "ENG" Then
                LblMode.Text = "GoodsDeliveryInputMode"
            Else
                LblMode.Text = "出庫入力モード"
            End If

            BtnGoodsIssue.Visible = True
            BtnGoodsIssue.Location = New Point(997, 509)
        ElseIf OrderStatus = "EDIT" Then
            If frmC01F10_Login.loginValue.Language = "ENG" Then
                LblMode.Text = "EditMode"
            Else
                LblMode.Text = "編集モード"
            End If

            BtnOrderEdit.Visible = True
            BtnOrderEdit.Location = New Point(997, 509)
        ElseIf OrderStatus = "VIEW" Then
            If frmC01F10_Login.loginValue.Language = "ENG" Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If

            BtnOrderView.Visible = True
            BtnOrderView.Location = New Point(997, 509)
        ElseIf OrderStatus = "CANCEL" Then
            If frmC01F10_Login.loginValue.Language = "ENG" Then
                LblMode.Text = "CancelMode"
            Else
                LblMode.Text = "取消モード"
            End If

            BtnOrderCancel.Visible = True
            BtnOrderCancel.Location = New Point(997, 509)
        ElseIf OrderStatus = "CLONE" Then
            If frmC01F10_Login.loginValue.Language = "ENG" Then
                LblMode.Text = "NewCopyMode"
            Else
                LblMode.Text = "新規複写モード"
            End If

            BtnOrderClone.Visible = True
            BtnOrderClone.Location = New Point(997, 509)
        ElseIf OrderStatus = "BILL" Then
            If frmC01F10_Login.loginValue.Language = "ENG" Then
                LblMode.Text = "BillingRegistrationMode"
            Else
                LblMode.Text = "請求登録モード"
            End If

            BtnBill.Visible = True
            BtnBill.Location = New Point(997, 509)
        End If
        Dim Status As String = "EXCLUSION"
        OrderListLoad(Status)
        If frmC01F10_Login.loginValue.Language = "ENG" Then
            LblConditions.Text = "TermsOfSelection"
            Label1.Text = "CustomerName"
            Label2.Text = "Address"
            Label3.Text = "PhoneNumber"
            Label4.Text = "CustomerCode"
            Label8.Text = "OrderDate"
            Label7.Text = "OrdernNumber"
            Label6.Text = "SalesPersonInCharge"
            Label11.Text = "CustomerNumber"
            Label10.Text = "DisplayFormat"
            RbtnSlip.Text = "UnitOfSlip"

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
        End If
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        Dim openForm As Form = Nothing
        openForm = New frmC01F30_Menu(_msgHd, _langHd, _db)
        openForm.Show()
        Me.Close()
    End Sub

    Private Sub RbtnDetails_CheckedChanged(sender As Object, e As EventArgs) Handles RbtnDetails.CheckedChanged
        DgvCymnhd.Rows.Clear()
        DgvCymnhd.Columns.Clear()

        If RbtnSlip.Checked Then
            Dim Sql As String = ""
            Sql += "SELECT "
            Sql += " * "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t10_cymnhd"
            If OrderNo IsNot Nothing Then
                For i As Integer = 0 To OrderNo.Length - 1
                    If i = 0 Then
                        Sql += " WHERE "
                        Sql += "受注番号"
                        Sql += " ILIKE "
                        Sql += "'%"
                        Sql += OrderNo(i)
                        Sql += "%'"
                    Else
                        Sql += " OR "
                        Sql += "受注番号"
                        Sql += " ILIKE "
                        Sql += "'%"
                        Sql += OrderNo(i)
                        Sql += "%'"
                    End If
                Next
            End If
            Sql += " ORDER BY "
            Sql += "登録日 DESC"



            Dim reccnt As Integer = 0
            ds = _db.selectDB(Sql, RS, reccnt)

            If frmC01F10_Login.loginValue.Language = "ENG" Then
                DgvCymnhd.Columns.Add("受注番号", "OrderNumber")
                DgvCymnhd.Columns.Add("受注番号枝番", "JobOrderSubNumber")
                DgvCymnhd.Columns.Add("客先番号", "CustomerNumber")
                DgvCymnhd.Columns.Add("受注日", "OrderNumber")
                DgvCymnhd.Columns.Add("見積番号", "QuotationNumber")
                DgvCymnhd.Columns.Add("見積番号枝番", "BranchNumber")
                DgvCymnhd.Columns.Add("見積日", "QuotationDate")
                DgvCymnhd.Columns.Add("見積有効期限", "QuotationExpriedDate")
                DgvCymnhd.Columns.Add("得意先コード", "CustomerCode")
                DgvCymnhd.Columns.Add("得意先名", "CustomerName")
                DgvCymnhd.Columns.Add("得意先郵便番号", "PostalCode")
                DgvCymnhd.Columns.Add("得意先住所", "Address")
                DgvCymnhd.Columns.Add("得意先電話番号", "PhoneNumber")
                DgvCymnhd.Columns.Add("得意先ＦＡＸ", "FAX")
                DgvCymnhd.Columns.Add("得意先担当者名", "NameOfPIC")
                DgvCymnhd.Columns.Add("得意先担当者役職", "PositionPICCustomer")
                DgvCymnhd.Columns.Add("ＶＡＴ", "VAT")
                DgvCymnhd.Columns.Add("受注金額", "OrderAmount")
                DgvCymnhd.Columns.Add("仕入金額", "PurchaseAmount")
                DgvCymnhd.Columns.Add("粗利額", "GrossMargin")
                DgvCymnhd.Columns.Add("支払条件", "PeymentTerms")
                DgvCymnhd.Columns.Add("営業担当者", "SalesPersonInCharge")
                DgvCymnhd.Columns.Add("入力担当者", "PICForInputting")
                DgvCymnhd.Columns.Add("備考", "Remarks")
                DgvCymnhd.Columns.Add("登録日", "RegistrationDate")
            Else
                DgvCymnhd.Columns.Add("受注番号", "受注番号")
                DgvCymnhd.Columns.Add("受注番号枝番", "受注番号枝番")
                DgvCymnhd.Columns.Add("客先番号", "客先番号")
                DgvCymnhd.Columns.Add("受注日", "受注日")
                DgvCymnhd.Columns.Add("見積番号", "見積番号")
                DgvCymnhd.Columns.Add("見積番号枝番", "見積番号枝番")
                DgvCymnhd.Columns.Add("見積日", "見積日")
                DgvCymnhd.Columns.Add("見積有効期限", "見積有効期限")
                DgvCymnhd.Columns.Add("得意先コード", "得意先コード")
                DgvCymnhd.Columns.Add("得意先名", "得意先名")
                DgvCymnhd.Columns.Add("得意先郵便番号", "得意先郵便番号")
                DgvCymnhd.Columns.Add("得意先住所", "得意先住所")
                DgvCymnhd.Columns.Add("得意先電話番号", "得意先電話番号")
                DgvCymnhd.Columns.Add("得意先ＦＡＸ", "得意先ＦＡＸ")
                DgvCymnhd.Columns.Add("得意先担当者名", "得意先担当者名")
                DgvCymnhd.Columns.Add("得意先担当者役職", "得意先担当者役職")
                DgvCymnhd.Columns.Add("ＶＡＴ", "ＶＡＴ")
                DgvCymnhd.Columns.Add("受注金額", "受注金額")
                DgvCymnhd.Columns.Add("仕入金額", "仕入金額")
                DgvCymnhd.Columns.Add("粗利額", "粗利額")
                DgvCymnhd.Columns.Add("支払条件", "支払条件")
                DgvCymnhd.Columns.Add("営業担当者", "営業担当者")
                DgvCymnhd.Columns.Add("入力担当者", "入力担当者")
                DgvCymnhd.Columns.Add("備考", "備考")
                DgvCymnhd.Columns.Add("登録日", "登録日")
            End If

            DgvCymnhd.Columns("ＶＡＴ").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns("受注金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns("粗利額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvCymnhd.Rows.Add()
                DgvCymnhd.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)("受注番号")
                DgvCymnhd.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)("受注番号枝番")
                DgvCymnhd.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)("客先番号")
                DgvCymnhd.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)("受注日")
                DgvCymnhd.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)("見積番号")
                DgvCymnhd.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)("見積番号枝番")
                DgvCymnhd.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)("見積日")
                DgvCymnhd.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)("見積有効期限")
                DgvCymnhd.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)("得意先コード")
                DgvCymnhd.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)("得意先名")
                DgvCymnhd.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)("得意先郵便番号")
                DgvCymnhd.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)("得意先住所")
                DgvCymnhd.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)("得意先電話番号")
                DgvCymnhd.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)("得意先ＦＡＸ")
                DgvCymnhd.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)("得意先担当者名")
                DgvCymnhd.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)("得意先担当者役職")
                DgvCymnhd.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)("ＶＡＴ")
                DgvCymnhd.Rows(index).Cells(17).Value = ds.Tables(RS).Rows(index)("見積金額")
                DgvCymnhd.Rows(index).Cells(18).Value = ds.Tables(RS).Rows(index)("仕入金額")
                DgvCymnhd.Rows(index).Cells(19).Value = ds.Tables(RS).Rows(index)("粗利額")
                DgvCymnhd.Rows(index).Cells(20).Value = ds.Tables(RS).Rows(index)("支払条件")
                DgvCymnhd.Rows(index).Cells(21).Value = ds.Tables(RS).Rows(index)("営業担当者")
                DgvCymnhd.Rows(index).Cells(22).Value = ds.Tables(RS).Rows(index)("入力担当者")
                DgvCymnhd.Rows(index).Cells(23).Value = ds.Tables(RS).Rows(index)("備考")
                DgvCymnhd.Rows(index).Cells(24).Value = ds.Tables(RS).Rows(index)("登録日")
            Next
        Else
            Dim Sql As String = ""

            Sql += "SELECT "
            Sql += " * "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t11_cymndt"

            If OrderNo IsNot Nothing Then
                For i As Integer = 0 To OrderNo.Length - 1
                    If i = 0 Then
                        Sql += " WHERE "
                        Sql += "受注番号"
                        Sql += " ILIKE "
                        Sql += "'%"
                        Sql += OrderNo(i)
                        Sql += "%'"
                    Else
                        Sql += " OR "
                        Sql += "受注番号"
                        Sql += " ILIKE "
                        Sql += "'%"
                        Sql += OrderNo(i)
                        Sql += "%'"
                    End If
                Next
            End If
            Sql += " ORDER BY "
            Sql += "登録日 DESC"

            Dim reccnt As Integer = 0
            ds = _db.selectDB(Sql, RS, reccnt)
            If frmC01F10_Login.loginValue.Language = "ENG" Then
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


            DgvCymnhd.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            DgvCymnhd.Columns(19).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns(20).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Dim tmp1 As String = ""
            Dim Sql2 As String = ""
            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1

                DgvCymnhd.Rows.Add()
                DgvCymnhd.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)("受注番号")
                DgvCymnhd.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)("受注番号枝番")
                DgvCymnhd.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)("行番号")
                If ds.Tables(RS).Rows(index)("仕入区分") = 1 Then
                    DgvCymnhd.Rows(index).Cells(3).Value = "仕入"
                ElseIf ds.Tables(RS).Rows(index)("仕入区分") = 2 Then
                    DgvCymnhd.Rows(index).Cells(3).Value = "在庫"
                Else
                    DgvCymnhd.Rows(index).Cells(3).Value = "サービス"
                End If

                DgvCymnhd.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)("メーカー")
                DgvCymnhd.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)("品名")
                DgvCymnhd.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)("型式")
                DgvCymnhd.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)("仕入先名")
                DgvCymnhd.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)("仕入値")
                DgvCymnhd.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)("受注数量")
                DgvCymnhd.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)("売上数量")
                DgvCymnhd.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)("受注残数")
                DgvCymnhd.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)("単位")
                DgvCymnhd.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)("間接費")
                DgvCymnhd.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)("売単価")
                DgvCymnhd.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)("売上金額")
                DgvCymnhd.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)("粗利額")
                DgvCymnhd.Rows(index).Cells(17).Value = ds.Tables(RS).Rows(index)("粗利率")

                If ds.Tables(RS).Rows(index)("リードタイム単位") Is DBNull.Value Then
                    DgvCymnhd.Rows(index).Cells(18).Value = ds.Tables(RS).Rows(index)("リードタイム")
                Else
                    tmp1 = ""
                    Sql2 = ""
                    Sql2 += "SELECT "
                    Sql2 += "* "
                    Sql2 += "FROM "
                    Sql2 += "public"
                    Sql2 += "."
                    Sql2 += "m90_hanyo"
                    Sql2 += " WHERE "
                    Sql2 += "会社コード"
                    Sql2 += " ILIKE "
                    Sql2 += "'"
                    Sql2 += frmC01F10_Login.loginValue.BumonNM
                    Sql2 += "'"
                    Sql2 += " AND "
                    Sql2 += "固定キー"
                    Sql2 += " ILIKE "
                    Sql2 += "'"
                    Sql2 += "4"
                    Sql2 += "'"
                    Sql2 += " AND "
                    Sql2 += "可変キー"
                    Sql2 += " ILIKE "
                    Sql2 += "'"
                    Sql2 += ds.Tables(RS).Rows(index)("リードタイム単位").ToString
                    Sql2 += "'"

                    Dim ds2 As DataSet = _db.selectDB(Sql2, RS, reccnt)
                    tmp1 += ds.Tables(RS).Rows(index)("リードタイム")
                    tmp1 += ds2.Tables(RS).Rows(0)("文字１")
                    DgvCymnhd.Rows(index).Cells(18).Value = tmp1
                End If

                DgvCymnhd.Rows(index).Cells(19).Value = ds.Tables(RS).Rows(index)("出庫数")
                DgvCymnhd.Rows(index).Cells(20).Value = ds.Tables(RS).Rows(index)("未出庫数")
                DgvCymnhd.Rows(index).Cells(21).Value = ds.Tables(RS).Rows(index)("備考")
                DgvCymnhd.Rows(index).Cells(22).Value = ds.Tables(RS).Rows(index)("更新者")
                DgvCymnhd.Rows(index).Cells(23).Value = ds.Tables(RS).Rows(index)("登録日")
            Next
        End If
    End Sub

    Private Sub BtnQuoteSearch_Click(sender As Object, e As EventArgs) Handles BtnOrderSearch.Click
        DgvCymnhd.Rows.Clear()
        DgvCymnhd.Columns.Clear()

        Dim count As Integer = 0
        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t10_cymnhd"
            If TxtCustomerName.Text = "" Then
            Else
                Sql += " WHERE "
                Sql += "得意先名"
                Sql += " ILIKE "
                Sql += "'%"
                Sql += TxtCustomerName.Text
                Sql += "%'"
                count += 1
            End If
            If TxtAddress.Text = "" Then
            Else
                If count > 0 Then
                    Sql += " AND "
                    Sql += "得意先住所"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtAddress.Text
                    Sql += "%'"
                Else
                    Sql += " WHERE "
                    Sql += "得意先住所"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtAddress.Text
                    Sql += "%'"
                    count += 1
                End If
            End If
            If TxtTel.Text = "" Then
            Else
                If count > 0 Then
                    Sql += " AND "
                    Sql += "得意先電話番号"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtTel.Text
                    Sql += "%'"
                Else
                    Sql += " WHERE "
                    Sql += "得意先電話番号"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtTel.Text
                    Sql += "%'"
                    count += 1
                End If
            End If
            If TxtCustomerCode.Text = "" Then
            Else
                If count > 0 Then
                    Sql += " AND "
                    Sql += "得意先コード"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtCustomerCode.Text
                    Sql += "%'"
                Else
                    Sql += " WHERE "
                    Sql += "得意先コード"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtCustomerCode.Text
                    Sql += "%'"
                    count += 1
                End If
            End If
            If TxtOrderDate1.Text = "" Then
                If TxtOrderDate2.Text = "" Then
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "受注日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtOrderDate2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "受注日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtOrderDate2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            Else
                If TxtOrderDate2.Text = "" Then
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "受注日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtOrderDate1.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "受注日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtOrderDate1.Text
                        Sql += "'"
                        count += 1
                    End If
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "受注日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtOrderDate1.Text
                        Sql += "' "
                        Sql += "AND  "
                        Sql += "受注日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtOrderDate2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "受注日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtOrderDate1.Text
                        Sql += "' "
                        Sql += "AND  "
                        Sql += "受注日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtOrderDate2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            End If
            If TxtOrderNo1.Text = "" Then
                If TxtOrderNo2.Text = "" Then
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "受注番号"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtOrderNo2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "受注番号"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtOrderNo2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            Else
                If TxtOrderNo2.Text = "" Then
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "受注番号"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtOrderNo1.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "受注番号"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtOrderNo1.Text
                        Sql += "'"
                        count += 1
                    End If
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "受注番号"
                        Sql += " >= "
                        Sql += "'"
                        Sql += TxtOrderNo1.Text
                        Sql += "' "
                        Sql += "AND "
                        Sql += "受注番号"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtOrderNo2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "受注番号"
                        Sql += " >= "
                        Sql += "'"
                        Sql += TxtOrderNo1.Text
                        Sql += "' "
                        Sql += "AND  "
                        Sql += "受注番号"
                        Sql += " <= "
                        Sql += "'"
                        Sql += TxtOrderNo2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            End If
            If TxtSales.Text = "" Then
            Else
                If count > 0 Then
                    Sql += " AND "
                    Sql += "営業担当者"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtSales.Text
                    Sql += "%'"
                Else
                    Sql += " WHERE "
                    Sql += "営業担当者"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtSales.Text
                    Sql += "%'"
                    count += 1
                End If
            End If
            If TxtCustomerPO.Text = "" Then
            Else
                If count > 0 Then
                    Sql += " AND "
                    Sql += "客先番号"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtCustomerPO.Text
                    Sql += "%'"
                Else
                    Sql += " WHERE "
                    Sql += "客先番号"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtCustomerPO.Text
                    Sql += "%'"
                    count += 1
                End If
            End If
            Sql += " ORDER BY "
            Sql += "登録日 DESC"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            If frmC01F10_Login.loginValue.Language = "ENG" Then
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
                DgvCymnhd.Columns.Add("得意先郵便番号", "PostalCode")
                DgvCymnhd.Columns.Add("得意先住所", "Address")
                DgvCymnhd.Columns.Add("得意先電話番号", "PhoneNumber")
                DgvCymnhd.Columns.Add("得意先ＦＡＸ", "FAX")
                DgvCymnhd.Columns.Add("得意先担当者名", "NameOfPIC")
                DgvCymnhd.Columns.Add("得意先担当者役職", "PositionPICCustomer")
                DgvCymnhd.Columns.Add("ＶＡＴ", "VAT")
                DgvCymnhd.Columns.Add("受注金額", "OrderAmount")
                DgvCymnhd.Columns.Add("仕入金額", "PurchaseAmount")
                DgvCymnhd.Columns.Add("粗利額", "GrossMargin")
                DgvCymnhd.Columns.Add("支払条件", "PeymentTerms")
                DgvCymnhd.Columns.Add("営業担当者", "SalesPersonInCharge")
                DgvCymnhd.Columns.Add("入力担当者", "PICForInputting")
                DgvCymnhd.Columns.Add("備考", "Remarks")
                DgvCymnhd.Columns.Add("登録日", "RegistrationDate")
            Else
                DgvCymnhd.Columns.Add("受注番号", "受注番号")
                DgvCymnhd.Columns.Add("受注番号枝番", "受注番号枝番")
                DgvCymnhd.Columns.Add("客先番号", "客先番号")
                DgvCymnhd.Columns.Add("受注日", "受注日")
                DgvCymnhd.Columns.Add("見積番号", "見積番号")
                DgvCymnhd.Columns.Add("見積番号枝番", "見積番号枝番")
                DgvCymnhd.Columns.Add("見積日", "見積日")
                DgvCymnhd.Columns.Add("見積有効期限", "見積有効期限")
                DgvCymnhd.Columns.Add("得意先コード", "得意先コード")
                DgvCymnhd.Columns.Add("得意先名", "得意先名")
                DgvCymnhd.Columns.Add("得意先郵便番号", "得意先郵便番号")
                DgvCymnhd.Columns.Add("得意先住所", "得意先住所")
                DgvCymnhd.Columns.Add("得意先電話番号", "得意先電話番号")
                DgvCymnhd.Columns.Add("得意先ＦＡＸ", "得意先ＦＡＸ")
                DgvCymnhd.Columns.Add("得意先担当者名", "得意先担当者名")
                DgvCymnhd.Columns.Add("得意先担当者役職", "得意先担当者役職")
                DgvCymnhd.Columns.Add("ＶＡＴ", "ＶＡＴ")
                DgvCymnhd.Columns.Add("受注金額", "受注金額")
                DgvCymnhd.Columns.Add("仕入金額", "仕入金額")
                DgvCymnhd.Columns.Add("粗利額", "粗利額")
                DgvCymnhd.Columns.Add("支払条件", "支払条件")
                DgvCymnhd.Columns.Add("営業担当者", "営業担当者")
                DgvCymnhd.Columns.Add("入力担当者", "入力担当者")
                DgvCymnhd.Columns.Add("備考", "備考")
                DgvCymnhd.Columns.Add("登録日", "登録日")
            End If

            DgvCymnhd.Columns("ＶＡＴ").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns("受注金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns("粗利額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            Dim tmp As Integer = ds.Tables(RS).Rows.Count - 1
            ReDim OrderNo(tmp)

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvCymnhd.Rows.Add()
                OrderNo(index) = ds.Tables(RS).Rows(index)("受注番号")
                DgvCymnhd.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)("受注番号")
                DgvCymnhd.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)("受注番号枝番")
                DgvCymnhd.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)("客先番号")
                DgvCymnhd.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)("受注日")
                DgvCymnhd.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)("見積番号")
                DgvCymnhd.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)("見積番号枝番")
                DgvCymnhd.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)("見積日")
                DgvCymnhd.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)("見積有効期限")
                DgvCymnhd.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)("得意先コード")
                DgvCymnhd.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)("得意先名")
                DgvCymnhd.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)("得意先郵便番号")
                DgvCymnhd.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)("得意先住所")
                DgvCymnhd.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)("得意先電話番号")
                DgvCymnhd.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)("得意先ＦＡＸ")
                DgvCymnhd.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)("得意先担当者名")
                DgvCymnhd.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)("得意先担当者役職")
                DgvCymnhd.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)("ＶＡＴ")
                DgvCymnhd.Rows(index).Cells(17).Value = ds.Tables(RS).Rows(index)("見積金額")
                DgvCymnhd.Rows(index).Cells(18).Value = ds.Tables(RS).Rows(index)("仕入金額")
                DgvCymnhd.Rows(index).Cells(19).Value = ds.Tables(RS).Rows(index)("粗利額")
                DgvCymnhd.Rows(index).Cells(20).Value = ds.Tables(RS).Rows(index)("支払条件")
                DgvCymnhd.Rows(index).Cells(21).Value = ds.Tables(RS).Rows(index)("営業担当者")
                DgvCymnhd.Rows(index).Cells(22).Value = ds.Tables(RS).Rows(index)("入力担当者")
                DgvCymnhd.Rows(index).Cells(23).Value = ds.Tables(RS).Rows(index)("備考")
                DgvCymnhd.Rows(index).Cells(24).Value = ds.Tables(RS).Rows(index)("登録日")
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Sub BtnOrderEdit_Click(sender As Object, e As EventArgs) Handles BtnOrderEdit.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvCymnhd.CurrentCell.RowIndex
        Dim No As String = DgvCymnhd.Rows(RowIdx).Cells(0).Value
        Dim Suffix As String = DgvCymnhd.Rows(RowIdx).Cells(1).Value
        Dim Status As String = "EDIT"
        Dim openForm As Form = Nothing
        openForm = New Order(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
        openForm.ShowDialog(Me)
        DgvCymnhd.Rows.Clear()
        DgvCymnhd.Columns.Clear()
        Dim ListStatus As String = "EXCLUSION"
        OrderListLoad(ListStatus)
    End Sub

    Private Sub BtnOrderView_Click(sender As Object, e As EventArgs) Handles BtnOrderView.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvCymnhd.CurrentCell.RowIndex
        Dim No As String = DgvCymnhd.Rows(RowIdx).Cells(0).Value
        Dim Suffix As String = DgvCymnhd.Rows(RowIdx).Cells(1).Value
        Dim Status As String = "VIEW"

        Dim openForm As Form = Nothing
        openForm = New Order(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
        openForm.Show(Me)
    End Sub

    Private Sub BtnOrder_Click(sender As Object, e As EventArgs) Handles BtnSales.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvCymnhd.CurrentCell.RowIndex
        Dim No As String = DgvCymnhd.Rows(RowIdx).Cells(0).Value
        Dim Suffix As String = DgvCymnhd.Rows(RowIdx).Cells(1).Value
        Dim openForm As Form = Nothing
        openForm = New OrderManagement(_msgHd, _db, _langHd, No, Suffix)   '処理選択
        openForm.Show(Me)
    End Sub

    Private Sub BtnReceipt_Click(sender As Object, e As EventArgs) Handles BtnGoodsIssue.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvCymnhd.CurrentCell.RowIndex
        Dim No As String = DgvCymnhd.Rows(RowIdx).Cells(0).Value
        Dim Suffix As String = DgvCymnhd.Rows(RowIdx).Cells(1).Value
        Dim openForm As Form = Nothing
        openForm = New GoodsIssue(_msgHd, _db, _langHd, No, Suffix)   '処理選択
        openForm.Show(Me)
    End Sub

    Private Sub BtnOrderCancel_Click(sender As Object, e As EventArgs) Handles BtnOrderCancel.Click
        Dim dtNow As DateTime = DateTime.Now
        Dim Sql1 As String = ""
        Sql1 = ""
        Sql1 += "UPDATE "
        Sql1 += "Public."
        Sql1 += "t10_cymnhd "
        Sql1 += "SET "

        Sql1 += "取消区分"
        Sql1 += " = '"
        Sql1 += "1"
        Sql1 += "', "
        Sql1 += "取消日"
        Sql1 += " = '"
        Sql1 += dtNow
        Sql1 += "', "
        Sql1 += "更新日"
        Sql1 += " = '"
        Sql1 += dtNow
        Sql1 += "', "
        Sql1 += "更新者"
        Sql1 += " = '"
        Sql1 += frmC01F10_Login.loginValue.TantoNM
        Sql1 += " ' "

        Sql1 += "WHERE"
        Sql1 += " 会社コード"
        Sql1 += "='"
        Sql1 += frmC01F10_Login.loginValue.BumonNM
        Sql1 += "'"
        Sql1 += " AND"
        Sql1 += " 受注番号"
        Sql1 += "='"
        Sql1 += DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("受注番号").Value
        Sql1 += "' "
        Sql1 += " AND"
        Sql1 += " 受注番号枝番"
        Sql1 += "='"
        Sql1 += DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("受注番号枝番").Value
        Sql1 += "' "
        Sql1 += "RETURNING 会社コード"
        Sql1 += ", "
        Sql1 += "受注番号"
        Sql1 += ", "
        Sql1 += "受注番号枝番"
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
        Sql1 += "受注日"
        Sql1 += ", "
        Sql1 += "登録日"
        Sql1 += ", "
        Sql1 += "更新日"
        Sql1 += ", "
        Sql1 += "更新者"

        If frmC01F10_Login.loginValue.Language = "ENG" Then
            Dim result As DialogResult = MessageBox.Show("Would you like to cancel the order?",
                                             "Question",
                                             MessageBoxButtons.YesNoCancel,
                                             MessageBoxIcon.Exclamation,
                                             MessageBoxDefaultButton.Button2)
            If result = DialogResult.Yes Then
                _db.executeDB(Sql1)
                DgvCymnhd.Rows.Clear()
                DgvCymnhd.Columns.Clear()
                Dim Status As String = "EXCLUSION"
                OrderListLoad(Status)
            ElseIf result = DialogResult.No Then

            ElseIf result = DialogResult.Cancel Then

            End If
        Else
            Dim result As DialogResult = MessageBox.Show("受注を取り消しますか？",
                                             "質問",
                                             MessageBoxButtons.YesNoCancel,
                                             MessageBoxIcon.Exclamation,
                                             MessageBoxDefaultButton.Button2)
            If result = DialogResult.Yes Then
                _db.executeDB(Sql1)
                DgvCymnhd.Rows.Clear()
                DgvCymnhd.Columns.Clear()
                Dim Status As String = "EXCLUSION"
                OrderListLoad(Status)
            ElseIf result = DialogResult.No Then

            ElseIf result = DialogResult.Cancel Then

            End If
        End If




    End Sub

    Private Sub ChkCancelData_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCancelData.CheckedChanged
        DgvCymnhd.Rows.Clear()
        DgvCymnhd.Columns.Clear()

        If ChkCancelData.Checked = False Then
            Dim Status As String = "EXCLUSION"
            OrderListLoad(Status)
        Else
            OrderListLoad()
        End If
    End Sub

    Private Sub BtnOrderClone_Click(sender As Object, e As EventArgs) Handles BtnOrderClone.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvCymnhd.CurrentCell.RowIndex
        Dim No As String = DgvCymnhd.Rows(RowIdx).Cells(0).Value
        Dim Suffix As String = DgvCymnhd.Rows(RowIdx).Cells(1).Value
        Dim Status As String = "CLONE"
        Dim openForm As Form = Nothing
        openForm = New Order(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
        Me.Enabled = False
        Me.Hide()
        openForm.ShowDialog(Me)

        DgvCymnhd.Rows.Clear()
        DgvCymnhd.Columns.Clear()

        Dim ListStatus As String = "EXCLUSION"
        OrderListLoad(ListStatus)
    End Sub

    Private Sub BtnBill_Click(sender As Object, e As EventArgs) Handles BtnBill.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvCymnhd.CurrentCell.RowIndex
        Dim No As String = DgvCymnhd.Rows(RowIdx).Cells(0).Value
        Dim Suffix As String = DgvCymnhd.Rows(RowIdx).Cells(1).Value
        Dim openForm As Form = Nothing
        openForm = New BillingManagement(_msgHd, _db, _langHd, Me, No, Suffix)   '処理選択
        Me.Enabled = False
        Me.Hide()
        openForm.ShowDialog(Me)

        DgvCymnhd.Rows.Clear()
        DgvCymnhd.Columns.Clear()

        Dim ListStatus As String = "EXCLUSION"
        OrderListLoad(ListStatus)
    End Sub
End Class