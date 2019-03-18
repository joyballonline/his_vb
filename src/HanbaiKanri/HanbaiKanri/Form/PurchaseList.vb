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

Public Class PurchaseList
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
    Private OrderingNo As String()
    Private _status As String = ""

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

    '仕入一覧を表示
    Private Sub PurchaseListLoad(Optional ByRef Status As String = "")

        '一覧をクリア
        DgvHtyhd.Rows.Clear()
        DgvHtyhd.Columns.Clear()

        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""


        Try
            '伝票単位の場合
            If RbtnSlip.Checked Then
                Sql = searchConditions() '抽出条件取得
                Sql += viewFormat() '表示形式条件

                Sql += " ORDER BY "
                Sql += "登録日 DESC"

                ds = getDsData("t40_sirehd", Sql)


                setHdColumns() '表示カラムの設定

                For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                    DgvHtyhd.Rows.Add()
                    DgvHtyhd.Rows(i).Cells("取消").Value = getDelKbnTxt(ds.Tables(RS).Rows(i)("取消区分"))
                    DgvHtyhd.Rows(i).Cells("仕入番号").Value = ds.Tables(RS).Rows(i)("仕入番号")
                    DgvHtyhd.Rows(i).Cells("客先番号").Value = ds.Tables(RS).Rows(i)("客先番号")
                    DgvHtyhd.Rows(i).Cells("発注番号").Value = ds.Tables(RS).Rows(i)("発注番号")
                    DgvHtyhd.Rows(i).Cells("発注番号枝番").Value = ds.Tables(RS).Rows(i)("発注番号枝番")
                    DgvHtyhd.Rows(i).Cells("仕入日").Value = ds.Tables(RS).Rows(i)("仕入日")
                    DgvHtyhd.Rows(i).Cells("仕入先コード").Value = ds.Tables(RS).Rows(i)("仕入先コード")
                    DgvHtyhd.Rows(i).Cells("仕入先名").Value = ds.Tables(RS).Rows(i)("仕入先名")
                    DgvHtyhd.Rows(i).Cells("仕入先郵便番号").Value = ds.Tables(RS).Rows(i)("仕入先郵便番号")
                    DgvHtyhd.Rows(i).Cells("仕入先住所").Value = ds.Tables(RS).Rows(i)("仕入先住所")
                    DgvHtyhd.Rows(i).Cells("仕入先電話番号").Value = ds.Tables(RS).Rows(i)("仕入先電話番号")
                    DgvHtyhd.Rows(i).Cells("仕入先ＦＡＸ").Value = ds.Tables(RS).Rows(i)("仕入先ＦＡＸ")
                    DgvHtyhd.Rows(i).Cells("仕入先担当者名").Value = ds.Tables(RS).Rows(i)("仕入先担当者名")
                    DgvHtyhd.Rows(i).Cells("仕入先担当者役職").Value = ds.Tables(RS).Rows(i)("仕入先担当者役職")
                    DgvHtyhd.Rows(i).Cells("仕入金額").Value = ds.Tables(RS).Rows(i)("仕入金額")
                    DgvHtyhd.Rows(i).Cells("支払条件").Value = ds.Tables(RS).Rows(i)("支払条件")
                    DgvHtyhd.Rows(i).Cells("営業担当者").Value = ds.Tables(RS).Rows(i)("営業担当者")
                    DgvHtyhd.Rows(i).Cells("入力担当者").Value = ds.Tables(RS).Rows(i)("入力担当者")
                    DgvHtyhd.Rows(i).Cells("備考").Value = ds.Tables(RS).Rows(i)("備考")
                    DgvHtyhd.Rows(i).Cells("登録日").Value = ds.Tables(RS).Rows(i)("登録日")
                Next

            Else

                '明細単位の場合

                '抽出条件
                Dim customerName As String = escapeSql(TxtSupplierName.Text)
                Dim customerAddress As String = escapeSql(TxtAddress.Text)
                Dim customerTel As String = escapeSql(TxtTel.Text)
                Dim customerCode As String = escapeSql(TxtSupplierCode.Text)
                Dim sinceDate As String = strFormatDate(dtPurchaseDateSince.Text)
                Dim untilDate As String = strFormatDate(dtPurchaseDateUntil.Text)
                Dim sinceNum As String = escapeSql(TxtPurchaseSince.Text)
                Dim untilNum As String = escapeSql(TxtPurchaseUntil.Text)
                Dim salesName As String = escapeSql(TxtSales.Text)
                Dim customerPO As String = escapeSql(TxtCustomerPO.Text)

                'joinするのでとりあえず直書き
                Sql = "SELECT * FROM  public.t41_siredt t41 "
                Sql += " INNER JOIN  t40_sirehd t40 "
                Sql += " ON t41.会社コード = t40.会社コード"
                Sql += " AND  t41.仕入番号 = t40.仕入番号"
                Sql += " WHERE t41.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

                If customerName <> Nothing Then
                    Sql += " AND "
                    Sql += " t41.仕入先名 ILIKE '%" & customerName & "%' "
                End If

                If customerAddress <> Nothing Then
                    Sql += " AND "
                    Sql += " t41.仕入先住所 ILIKE '%" & customerAddress & "%' "
                End If

                If customerTel <> Nothing Then
                    Sql += " AND "
                    Sql += " t41.仕入先電話番号 ILIKE '%" & customerTel & "%' "
                End If

                If customerCode <> Nothing Then
                    Sql += " AND "
                    Sql += " t41.仕入先コード ILIKE '%" & customerCode & "%' "
                End If

                If sinceDate <> Nothing Then
                    Sql += " AND "
                    Sql += " t41.仕入日 >= '" & sinceDate & "'"
                End If
                If untilDate <> Nothing Then
                    Sql += " AND "
                    Sql += " t41.仕入日 <= '" & untilDate & "'"
                End If

                If sinceNum <> Nothing Then
                    Sql += " AND "
                    Sql += " t41.仕入番号 >= '" & sinceNum & "' "
                End If
                If untilNum <> Nothing Then
                    Sql += " AND "
                    Sql += " t41.仕入番号 <= '" & untilNum & "' "
                End If

                If salesName <> Nothing Then
                    Sql += " AND "
                    Sql += " t40.営業担当者 ILIKE '%" & salesName & "%' "
                End If

                If customerPO <> Nothing Then
                    Sql += " AND "
                    Sql += " t40.客先番号 ILIKE '%" & customerPO & "%' "
                End If

                '取消データを含めない場合
                If ChkCancelData.Checked = False Then
                    Sql += " AND "
                    Sql += "t40.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
                End If

                Sql += " ORDER BY "
                Sql += "t41.更新日 DESC"

                ds = _db.selectDB(Sql, RS, reccnt)

                setDtColumns() '表示カラムの設定

                For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                    DgvHtyhd.Rows.Add()
                    DgvHtyhd.Rows(i).Cells("仕入番号").Value = ds.Tables(RS).Rows(i)("仕入番号")
                    DgvHtyhd.Rows(i).Cells("行番号").Value = ds.Tables(RS).Rows(i)("行番号")
                    DgvHtyhd.Rows(i).Cells("仕入区分").Value = ds.Tables(RS).Rows(i)("仕入区分")

                    'リードタイムのリストを汎用マスタから取得
                    Dim dsHanyou As DataSet = getDsHanyoData(CommonConst.FIXED_KEY_PURCHASING_CLASS, ds.Tables(RS).Rows(i)("仕入区分"))
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
                    DgvHtyhd.Rows(i).Cells("リードタイム").Value = ds.Tables(RS).Rows(i)("リードタイム")
                    DgvHtyhd.Rows(i).Cells("備考").Value = ds.Tables(RS).Rows(i)("備考")
                    DgvHtyhd.Rows(i).Cells("更新者").Value = ds.Tables(RS).Rows(i)("更新者")
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

    '使用言語に合わせて仕入基本見出しを切替
    Private Sub setHdColumns()

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            DgvHtyhd.Columns.Add("取消", "Cancel")
            DgvHtyhd.Columns.Add("仕入番号", "PurchaseNumber")
            DgvHtyhd.Columns.Add("客先番号", "CustomerNumber")
            DgvHtyhd.Columns.Add("仕入日", "PurchaseDate")
            DgvHtyhd.Columns.Add("発注番号", "PurchaseOrderNumber")
            DgvHtyhd.Columns.Add("発注番号枝番", "PurchaseOrderSubNumber")
            DgvHtyhd.Columns.Add("仕入先コード", "SupplierCode")
            DgvHtyhd.Columns.Add("仕入先名", "SupplierName")
            DgvHtyhd.Columns.Add("仕入先郵便番号", "PostalCode")
            DgvHtyhd.Columns.Add("仕入先住所", "Address")
            DgvHtyhd.Columns.Add("仕入先電話番号", "PhoneNumber")
            DgvHtyhd.Columns.Add("仕入先ＦＡＸ", "FAX")
            DgvHtyhd.Columns.Add("仕入先担当者名", "NameOfPIC")
            DgvHtyhd.Columns.Add("仕入先担当者役職", "PositionPICSupplier")
            DgvHtyhd.Columns.Add("仕入金額", "PurchaseAmount")
            DgvHtyhd.Columns.Add("支払条件", "PaymentTerms")
            DgvHtyhd.Columns.Add("営業担当者", "SalesPersonInCharge")
            DgvHtyhd.Columns.Add("入力担当者", "PICForInputting")
            DgvHtyhd.Columns.Add("備考", "Remarks")
            DgvHtyhd.Columns.Add("登録日", "RegistrationDate")
        Else
            DgvHtyhd.Columns.Add("取消", "取消")
            DgvHtyhd.Columns.Add("仕入番号", "仕入番号")
            DgvHtyhd.Columns.Add("客先番号", "客先番号")
            DgvHtyhd.Columns.Add("仕入日", "仕入日")
            DgvHtyhd.Columns.Add("発注番号", "発注番号")
            DgvHtyhd.Columns.Add("発注番号枝番", "発注番号枝番")
            DgvHtyhd.Columns.Add("仕入先コード", "仕入先コード")
            DgvHtyhd.Columns.Add("仕入先名", "仕入先名")
            DgvHtyhd.Columns.Add("仕入先郵便番号", "仕入先郵便番号")
            DgvHtyhd.Columns.Add("仕入先住所", "仕入先住所")
            DgvHtyhd.Columns.Add("仕入先電話番号", "仕入先電話番号")
            DgvHtyhd.Columns.Add("仕入先ＦＡＸ", "仕入先ＦＡＸ")
            DgvHtyhd.Columns.Add("仕入先担当者名", "仕入先担当者名")
            DgvHtyhd.Columns.Add("仕入先担当者役職", "仕入先担当者役職")
            DgvHtyhd.Columns.Add("仕入金額", "仕入金額")
            DgvHtyhd.Columns.Add("支払条件", "支払条件")
            DgvHtyhd.Columns.Add("営業担当者", "営業担当者")
            DgvHtyhd.Columns.Add("入力担当者", "入力担当者")
            DgvHtyhd.Columns.Add("備考", "備考")
            DgvHtyhd.Columns.Add("登録日", "登録日")

        End If

        DgvHtyhd.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    End Sub

    '使用言語に合わせて仕入明細見出しを切替
    Private Sub setDtColumns()

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            DgvHtyhd.Columns.Add("取消", "Cancel")
            DgvHtyhd.Columns.Add("仕入番号", "PurchaseNumber")
            DgvHtyhd.Columns.Add("行番号", "LineNumber")
            DgvHtyhd.Columns.Add("仕入区分", "PurchasingClassification")
            DgvHtyhd.Columns.Add("メーカー", "Manufacturer")
            DgvHtyhd.Columns.Add("品名", "ItemName")
            DgvHtyhd.Columns.Add("型式", "Spec")
            DgvHtyhd.Columns.Add("仕入先名", "SupplierName")
            DgvHtyhd.Columns.Add("仕入値", "PurchaseAmount")
            DgvHtyhd.Columns.Add("発注数量", "OrderQuantity")
            DgvHtyhd.Columns.Add("仕入数量", "PurchaseQuantity")
            DgvHtyhd.Columns.Add("発注残数", "PurchasedQuantity")
            DgvHtyhd.Columns.Add("単位", "Unit")
            DgvHtyhd.Columns.Add("間接費", "OverHead")
            DgvHtyhd.Columns.Add("仕入金額", "PurchaseAmount")
            DgvHtyhd.Columns.Add("リードタイム", "LeadTime")
            DgvHtyhd.Columns.Add("備考", "Remarks")
            DgvHtyhd.Columns.Add("更新者", "ModifiedBy")
        Else
            DgvHtyhd.Columns.Add("取消", "取消")
            DgvHtyhd.Columns.Add("仕入番号", "仕入番号")
            DgvHtyhd.Columns.Add("行番号", "行番号")
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
            DgvHtyhd.Columns.Add("備考", "備考")
            DgvHtyhd.Columns.Add("更新者", "更新者")
        End If

        DgvHtyhd.Columns("仕入値").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvHtyhd.Columns("発注数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvHtyhd.Columns("仕入数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvHtyhd.Columns("発注残数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvHtyhd.Columns("間接費").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvHtyhd.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvHtyhd.Columns("リードタイム").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    End Sub

    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _status = CommonConst.STATUS_VIEW Then
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
        End If

        '検索（Date）の初期値
        dtPurchaseDateSince.Value = DateAdd("d", CommonConst.SINCE_DEFAULT_DAY, DateTime.Today)
        dtPurchaseDateUntil.Value = DateTime.Today

        PurchaseListLoad() '一覧欄を表示

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            LblConditions.Text = "ExtractionCondition"
            Label1.Text = "SupplierName"
            Label2.Text = "Address"
            Label3.Text = "PhoneNumber"
            Label4.Text = "SupplierCode"
            Label8.Text = "PurchaseDate"
            Label7.Text = "PurchaseNumber"
            Label6.Text = "SalesPersonInCharge"
            Label11.Text = "CustomerNumber"
            Label10.Text = "DisplayFormat"
            RbtnSlip.Text = "UnitOfVoucher"

            RbtnDetails.Text = "UnitOfDetailData"
            RbtnDetails.Location = New Point(166, 196)

            ChkCancelData.Text = "IncludeCancelData"
            ChkCancelData.Location = New Point(556, 196)

            BtnPurchaseSearch.Text = "Search"
            BtnPurchaseCancel.Text = "CancelOfPurchase"
            BtnPurchaseView.Text = "PurchaseDataView"
            BtnBack.Text = "Back"

        End If
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    Private Sub RbtnDetails_CheckedChanged(sender As Object, e As EventArgs) Handles RbtnDetails.CheckedChanged
        PurchaseListLoad() '一覧欄を表示
    End Sub

    Private Sub BtnPurchaseSearch_Click(sender As Object, e As EventArgs) Handles BtnPurchaseSearch.Click
        PurchaseListLoad() '一覧欄を表示
    End Sub

    Private Sub BtnPurchaseView_Click(sender As Object, e As EventArgs) Handles BtnPurchaseView.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvHtyhd.CurrentCell.RowIndex
        Dim No As String = DgvHtyhd.Rows(RowIdx).Cells("発注番号").Value
        Dim Suffix As String = DgvHtyhd.Rows(RowIdx).Cells("発注番号枝番").Value
        Dim Status As String = CommonConst.STATUS_VIEW
        Dim openForm As Form = Nothing
        openForm = New PurchasingManagement(_msgHd, _db, _langHd, No, Suffix, Status)   '処理選択
        openForm.Show(Me)
    End Sub

    Private Sub ChkCancelData_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCancelData.CheckedChanged
        PurchaseListLoad() '一覧欄を表示
    End Sub

    '仕入取消
    Private Sub BtnPurchaseCancel_Click(sender As Object, e As EventArgs) Handles BtnPurchaseCancel.Click
        'グリッドに何もないときは次画面へ移動しない
        If DgvHtyhd.RowCount = 0 Then
            Exit Sub
        End If

        '実行できるデータがあるかチェック
        If actionChk() = False Then
            Return
        End If


        Dim dtNow As String = formatDatetime(DateTime.Now)
        Dim reccnt As Integer = 0

        Dim Sql As String = ""

        Sql = "SELECT "
        Sql += " * "
        Sql += "FROM "
        Sql += "public"
        Sql += "."
        Sql += "t21_hattyu "
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

        Dim ds1 As DataSet = _db.selectDB(Sql, RS, reccnt)

        Dim Sql2 As String = ""
        Sql2 += "SELECT "
        Sql2 += " * "
        Sql2 += "FROM "
        Sql2 += "public"
        Sql2 += "."
        Sql2 += "t41_siredt "
        Sql2 += "WHERE"
        Sql2 += " 会社コード"
        Sql2 += "='"
        Sql2 += frmC01F10_Login.loginValue.BumonCD
        Sql2 += "'"
        Sql2 += " AND"
        Sql2 += " 仕入番号"
        Sql2 += "='"
        Sql2 += DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("仕入番号").Value
        Sql2 += "' "

        Dim ds2 As DataSet = _db.selectDB(Sql2, RS, reccnt)

        Dim Sql3 As String = ""
        Sql3 = ""
        Sql3 += "UPDATE "
        Sql3 += "Public."
        Sql3 += "t40_sirehd "
        Sql3 += "SET "

        Sql3 += "取消区分"
        Sql3 += " = '"
        Sql3 += "1"
        Sql3 += "', "
        Sql3 += "取消日"
        Sql3 += " = '"
        Sql3 += dtNow
        Sql3 += "', "
        Sql3 += "更新日"
        Sql3 += " = '"
        Sql3 += dtNow
        Sql3 += "', "
        Sql3 += "更新者"
        Sql3 += " = '"
        Sql3 += frmC01F10_Login.loginValue.TantoNM
        Sql3 += " ' "

        Sql3 += "WHERE"
        Sql3 += " 会社コード"
        Sql3 += "='"
        Sql3 += frmC01F10_Login.loginValue.BumonCD
        Sql3 += "'"
        Sql3 += " AND"
        Sql3 += " 仕入番号"
        Sql3 += "='"
        Sql3 += DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("仕入番号").Value
        Sql3 += "' "

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            Dim result As DialogResult = MessageBox.Show("Would you like to cancel the purchase？",
                                             "Question",
                                             MessageBoxButtons.YesNoCancel,
                                             MessageBoxIcon.Exclamation,
                                             MessageBoxDefaultButton.Button2)

            If result = DialogResult.Yes Then
                _db.executeDB(Sql3)


                Dim Sql4 As String = ""
                Dim PurchaseNum As Integer = 0
                Dim OrderingNum As Integer = 0

                For index1 As Integer = 0 To ds1.Tables(RS).Rows.Count() - 1
                    For index2 As Integer = 0 To ds2.Tables(RS).Rows.Count() - 1
                        If ds1.Tables(RS).Rows(index1)("行番号") = ds2.Tables(RS).Rows(index2)("行番号") Then
                            Sql4 = ""
                            Sql4 += "UPDATE "
                            Sql4 += "Public."
                            Sql4 += "t21_hattyu "
                            Sql4 += "SET "
                            Sql4 += "仕入数量"
                            Sql4 += " = '"
                            PurchaseNum = ds1.Tables(RS).Rows(index1)("仕入数量") - ds2.Tables(RS).Rows(index1)("仕入数量")
                            Sql4 += PurchaseNum.ToString
                            Sql4 += "', "
                            Sql4 += " 発注残数"
                            Sql4 += " = '"
                            OrderingNum = ds1.Tables(RS).Rows(index1)("発注残数") + ds2.Tables(RS).Rows(index2)("仕入数量")
                            Sql4 += OrderingNum.ToString
                            Sql4 += "', "
                            Sql4 += "更新者"
                            Sql4 += " = '"
                            Sql4 += frmC01F10_Login.loginValue.TantoNM
                            Sql4 += "' "
                            Sql4 += "WHERE"
                            Sql4 += " 会社コード"
                            Sql4 += "='"
                            Sql4 += ds1.Tables(RS).Rows(index1)("会社コード")
                            Sql4 += "'"
                            Sql4 += " AND"
                            Sql4 += " 発注番号"
                            Sql4 += "='"
                            Sql4 += ds1.Tables(RS).Rows(index1)("発注番号")
                            Sql4 += "'"
                            Sql4 += " AND"
                            Sql4 += " 発注番号枝番"
                            Sql4 += "='"
                            Sql4 += ds1.Tables(RS).Rows(index1)("発注番号枝番")
                            Sql4 += "'"
                            Sql4 += " AND"
                            Sql4 += " 行番号"
                            Sql4 += "='"
                            Sql4 += ds1.Tables(RS).Rows(index1)("行番号").ToString
                            Sql4 += "' "

                            _db.executeDB(Sql4)

                            Sql4 = ""
                            PurchaseNum = 0
                            OrderingNum = 0
                        End If
                    Next
                Next
                DgvHtyhd.Rows.Clear()
                DgvHtyhd.Columns.Clear()
                Dim Status As String = "EXCLUSION"
                PurchaseListLoad(Status)
            End If
        Else
            Dim result As DialogResult = MessageBox.Show("仕入を取り消しますか？",
                                             "質問",
                                             MessageBoxButtons.YesNoCancel,
                                             MessageBoxIcon.Exclamation,
                                             MessageBoxDefaultButton.Button2)

            If result = DialogResult.Yes Then
                _db.executeDB(Sql3)


                Dim Sql4 As String = ""
                Dim PurchaseNum As Integer = 0
                Dim OrderingNum As Integer = 0

                For index1 As Integer = 0 To ds1.Tables(RS).Rows.Count() - 1
                    For index2 As Integer = 0 To ds2.Tables(RS).Rows.Count() - 1
                        If ds1.Tables(RS).Rows(index1)("行番号") = ds2.Tables(RS).Rows(index2)("行番号") Then
                            Sql4 = ""
                            Sql4 += "UPDATE "
                            Sql4 += "Public."
                            Sql4 += "t21_hattyu "
                            Sql4 += "SET "
                            Sql4 += "仕入数量"
                            Sql4 += " = '"
                            PurchaseNum = ds1.Tables(RS).Rows(index1)("仕入数量") - ds2.Tables(RS).Rows(index1)("仕入数量")
                            Sql4 += PurchaseNum.ToString
                            Sql4 += "', "
                            Sql4 += " 発注残数"
                            Sql4 += " = '"
                            OrderingNum = ds1.Tables(RS).Rows(index1)("発注残数") + ds2.Tables(RS).Rows(index2)("仕入数量")
                            Sql4 += OrderingNum.ToString
                            Sql4 += "', "
                            Sql4 += "更新者"
                            Sql4 += " = '"
                            Sql4 += frmC01F10_Login.loginValue.TantoNM
                            Sql4 += "' "
                            Sql4 += "WHERE"
                            Sql4 += " 会社コード"
                            Sql4 += "='"
                            Sql4 += ds1.Tables(RS).Rows(index1)("会社コード")
                            Sql4 += "'"
                            Sql4 += " AND"
                            Sql4 += " 発注番号"
                            Sql4 += "='"
                            Sql4 += ds1.Tables(RS).Rows(index1)("発注番号")
                            Sql4 += "'"
                            Sql4 += " AND"
                            Sql4 += " 発注番号枝番"
                            Sql4 += "='"
                            Sql4 += ds1.Tables(RS).Rows(index1)("発注番号枝番")
                            Sql4 += "'"
                            Sql4 += " AND"
                            Sql4 += " 行番号"
                            Sql4 += "='"
                            Sql4 += ds1.Tables(RS).Rows(index1)("行番号").ToString
                            Sql4 += "' "

                            _db.executeDB(Sql4)

                            Sql4 = ""
                            PurchaseNum = 0
                            OrderingNum = 0
                        End If
                    Next
                Next
                DgvHtyhd.Rows.Clear()
                DgvHtyhd.Columns.Clear()
                Dim Status As String = "EXCLUSION"
                PurchaseListLoad(Status)
            End If
        End If


    End Sub

    '抽出条件取得
    Private Function searchConditions() As String

        Dim Sql As String = ""

        '抽出条件
        Dim customerName As String = escapeSql(TxtSupplierName.Text)
        Dim customerAddress As String = escapeSql(TxtAddress.Text)
        Dim customerTel As String = escapeSql(TxtTel.Text)
        Dim customerCode As String = escapeSql(TxtSupplierCode.Text)
        Dim sinceDate As String = strFormatDate(dtPurchaseDateSince.Text)
        Dim untilDate As String = strFormatDate(dtPurchaseDateUntil.Text)
        Dim sinceNum As String = escapeSql(TxtPurchaseSince.Text)
        Dim untilNum As String = escapeSql(TxtPurchaseUntil.Text)
        Dim salesName As String = escapeSql(TxtSales.Text)
        Dim customerPO As String = escapeSql(TxtCustomerPO.Text)

        If customerName <> Nothing Then
            Sql += " AND "
            Sql += " 仕入先名 ILIKE '%" & customerName & "%' "
        End If

        If customerAddress <> Nothing Then
            Sql += " AND "
            Sql += " 仕入先住所 ILIKE '%" & customerAddress & "%' "
        End If

        If customerTel <> Nothing Then
            Sql += " AND "
            Sql += " 仕入先電話番号 ILIKE '%" & customerTel & "%' "
        End If

        If customerCode <> Nothing Then
            Sql += " AND "
            Sql += " 仕入先コード ILIKE '%" & customerCode & "%' "
        End If

        If sinceDate <> Nothing Then
            Sql += " AND "
            Sql += " 仕入日 >= '" & sinceDate & "'"
        End If
        If untilDate <> Nothing Then
            Sql += " AND "
            Sql += " 仕入日 <= '" & untilDate & "'"
        End If

        If sinceNum <> Nothing Then
            Sql += " AND "
            Sql += " 仕入番号 >= '" & sinceNum & "' "
        End If
        If untilNum <> Nothing Then
            Sql += " AND "
            Sql += " 仕入番号 <= '" & untilNum & "' "
        End If

        If salesName <> Nothing Then
            Sql += " AND "
            Sql += " 営業担当者 ILIKE '%" & salesName & "%' "
        End If

        If customerPO <> Nothing Then
            Sql += " AND "
            Sql += " 客先番号 ILIKE '%" & customerPO & "%' "
        End If

        Return Sql

    End Function

    Private Function actionChk() As Boolean
        '対象データがない場合は取消操作不可能
        If DgvHtyhd.Rows.Count = 0 Then

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
            Sql += " AND "
            Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
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
        Sql += " WHERE "
        Sql += "会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
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

End Class