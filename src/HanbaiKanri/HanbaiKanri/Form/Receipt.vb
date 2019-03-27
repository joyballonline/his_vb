Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Globalization

Public Class Receipt
    Inherits System.Windows.Forms.Form

    '------------------------------------------------------------------------------------------------------
    'メンバー定数宣言
    '------------------------------------------------------------------------------------------------------
    'PG制御文字 

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
    Private _db As UtilDBIf
    'Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ
    Private CompanyCode As String = ""
    Private No As String = ""
    Private Suffix As String = ""
    Private _status As String = ""
    Private _langHd As UtilLangHandler
    Private Input As String = frmC01F10_Login.loginValue.TantoNM

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
                   ByRef prmRefNo As String,
                   ByRef prmRefSuffix As String,
                   Optional ByRef prmRefStatus As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        No = prmRefNo
        Suffix = prmRefSuffix
        _status = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    '画面表示時
    Private Sub Receipt_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblPurchaseNo.Text = "PurchaseOrderNumber"
            LblCustomerNo.Text = "CustomerNumber"
            LblPurchaseDate.Text = "OrderDate"
            LblSupplier.Text = "SupplierName"
            LblPurchase.Text = "PurchaseOrder"
            LblHistory.Text = "ReceiptHistory"
            LblAdd.Text = "GoodsReceiptThisTime"
            LblReceiptDate.Text = "GoodsReceiptDate"
            LblWarehouse.Text = "Warehouse"

            LblRemarks.Text = "Remarks"
            LblCount1.Text = "Record"
            LblCount1.Location = New Point(1272, 82)
            LblCount1.Size = New Size(66, 22)
            LblCount2.Text = "Record"
            LblCount2.Location = New Point(1272, 212)
            LblCount2.Size = New Size(66, 22)
            LblCount3.Text = "Record"
            LblCount3.Location = New Point(1272, 343)
            LblCount3.Size = New Size(66, 22)

            TxtCount1.Location = New Point(1228, 82)
            TxtCount2.Location = New Point(1228, 212)
            TxtCount3.Location = New Point(1228, 343)

            BtnRegist.Text = "Registration"
            BtnBack.Text = "Back"

        End If

        LblMode.Text = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "GoodsReceiptInputMode", "入庫入力モード")

        '参照モード時
        If _status = CommonConst.STATUS_VIEW Then

            LblMode.Text = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "ViewMode", "参照モード")

            LblCount1.Visible = False
            LblCount2.Visible = False
            LblCount3.Visible = False
            LblPurchase.Visible = False
            LblAdd.Visible = False
            'LblReceiptDate.Visible = False
            LblRemarks.Visible = False
            'DtpReceiptDate.Visible = False
            TxtCount1.Visible = False
            TxtCount2.Visible = False
            TxtCount3.Visible = False
            TxtRemarks.Visible = False
            DgvPurchase.Visible = False
            DgvAdd.Visible = False
            DgvHistory.ReadOnly = False

            LblHistory.Location = New Point(12, 82)
            DgvHistory.Location = New Point(12, 106)
            DgvHistory.Size = New Point(1326, 400)

            LblReceiptDate.Visible = False
            DtpReceiptDate.Visible = False
            LblWarehouse.Visible = False
            CmWarehouse.Visible = False

            BtnRegist.Visible = False

        End If

        '発注エリア
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            DgvPurchase.Columns.Add("明細", "DetailData")
            DgvPurchase.Columns.Add("メーカー", "Manufacturer")
            DgvPurchase.Columns.Add("品名", "ItemName")
            DgvPurchase.Columns.Add("型式", "Spec")
            DgvPurchase.Columns.Add("発注数量", "OrderQuantity")
            DgvPurchase.Columns.Add("単位", "Unit")
            DgvPurchase.Columns.Add("仕入数量", "PurchasedQuantity")
            DgvPurchase.Columns.Add("仕入単価", "PurchaseUnitPrice")
            DgvPurchase.Columns.Add("仕入金額", "PurchaseAmount")
            DgvPurchase.Columns.Add("発注残数", "NumberOfOrderRemaining")
            DgvPurchase.Columns.Add("未入庫数", "NoGoodsReceiptQntity")
            DgvPurchase.Columns.Add("更新日", "UpdateDate")
        Else
            DgvPurchase.Columns.Add("明細", "明細")
            DgvPurchase.Columns.Add("メーカー", "メーカー")
            DgvPurchase.Columns.Add("品名", "品名")
            DgvPurchase.Columns.Add("型式", "型式")
            DgvPurchase.Columns.Add("発注数量", "発注数量")
            DgvPurchase.Columns.Add("単位", "単位")
            DgvPurchase.Columns.Add("仕入数量", "仕入数量")
            DgvPurchase.Columns.Add("仕入単価", "仕入単価")
            DgvPurchase.Columns.Add("仕入金額", "仕入金額")
            DgvPurchase.Columns.Add("発注残数", "発注残数")
            DgvPurchase.Columns.Add("未入庫数", "未入庫数")
            DgvPurchase.Columns.Add("更新日", "更新日")
        End If

        DgvPurchase.Columns("発注数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvPurchase.Columns("仕入数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvPurchase.Columns("仕入単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvPurchase.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvPurchase.Columns("発注残数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvPurchase.Columns("未入庫数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvPurchase.Columns("更新日").Visible = False

        '入庫済みエリア
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            DgvHistory.Columns.Add("No", "No")
            DgvHistory.Columns.Add("入庫番号", "GoodsReceiptNumber")
            DgvHistory.Columns.Add("行番号", "LineNumber")
            DgvHistory.Columns.Add("仕入区分", "PurchasingClassification")
            DgvHistory.Columns.Add("メーカー", "Manufacturer")
            DgvHistory.Columns.Add("品名", "ItemName")
            DgvHistory.Columns.Add("型式", "Spec")
            DgvHistory.Columns.Add("単位", "Unit")
            DgvHistory.Columns.Add("仕入先", "SupplierName")
            DgvHistory.Columns.Add("仕入値", "PurchaseAmount")
            DgvHistory.Columns.Add("入庫数量", "GoodsReceiptQuantity")
            DgvHistory.Columns.Add("倉庫", "Warehouse")
            DgvHistory.Columns.Add("入出庫種別", "StorageType")
            DgvHistory.Columns.Add("引当区分", "AssignClassification")
            DgvHistory.Columns.Add("入庫日", "GoodsReceiptDate")
            DgvHistory.Columns.Add("備考", "Remarks")

        Else
            DgvHistory.Columns.Add("No", "No")
            DgvHistory.Columns.Add("入庫番号", "入庫番号")
            DgvHistory.Columns.Add("行番号", "行番号")
            DgvHistory.Columns.Add("仕入区分", "仕入区分")
            DgvHistory.Columns.Add("メーカー", "メーカー")
            DgvHistory.Columns.Add("品名", "品名")
            DgvHistory.Columns.Add("型式", "型式")
            DgvHistory.Columns.Add("単位", "単位")
            DgvHistory.Columns.Add("仕入先", "仕入先")
            DgvHistory.Columns.Add("仕入値", "仕入値")
            DgvHistory.Columns.Add("入庫数量", "入庫数量")
            DgvHistory.Columns.Add("倉庫", "倉庫")
            DgvHistory.Columns.Add("入出庫種別", "入出庫種別")
            DgvHistory.Columns.Add("引当区分", "引当区分")
            DgvHistory.Columns.Add("入庫日", "入庫日")
            DgvHistory.Columns.Add("備考", "備考")
        End If

        DgvHistory.Columns("仕入値").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvHistory.Columns("入庫数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        '今回入庫エリア
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            DgvAdd.Columns.Add("No", "No")
            DgvAdd.Columns.Add("行番号", "LineNumber")
            DgvAdd.Columns.Add("仕入区分", "PurchasingClassification")
            DgvAdd.Columns.Add("仕入区分値", "仕入区分値")
            DgvAdd.Columns.Add("メーカー", "Manufacturer")
            DgvAdd.Columns.Add("品名", "ItemName")
            DgvAdd.Columns.Add("型式", "Spec")
            DgvAdd.Columns.Add("単位", "Unit")
            DgvAdd.Columns.Add("仕入先", "SupplierName")
            DgvAdd.Columns.Add("仕入値", "PurchaseAmount")
            DgvAdd.Columns.Add("入庫数量", "GoodsReceiptQuantity")
            DgvAdd.Columns.Add("備考", "Remarks")

        Else
            DgvAdd.Columns.Add("No", "No")
            DgvAdd.Columns.Add("行番号", "行番号")
            DgvAdd.Columns.Add("仕入区分", "仕入区分")
            DgvAdd.Columns.Add("仕入区分値", "仕入区分値")
            DgvAdd.Columns.Add("メーカー", "メーカー")
            DgvAdd.Columns.Add("品名", "品名")
            DgvAdd.Columns.Add("型式", "型式")
            DgvAdd.Columns.Add("単位", "単位")
            DgvAdd.Columns.Add("仕入先", "仕入先")
            DgvAdd.Columns.Add("仕入値", "仕入値")
            DgvAdd.Columns.Add("入庫数量", "入庫数量")
            DgvAdd.Columns.Add("備考", "備考")
        End If

        DgvAdd.Columns("仕入値").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvAdd.Columns("入庫数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DgvAdd.Columns("仕入区分値").Visible = False

        createWarehouseCombobox() '倉庫コンボボックス

        Dim reccnt As Integer = 0
        Dim Sql As String = ""
        Dim sireKbn As DataSet

        Try

            Sql = " AND "
            Sql += "発注番号"
            Sql += " ILIKE '" & No & "'"
            Sql += " AND "
            Sql += "発注番号枝番"
            Sql += " ILIKE '" & Suffix & "'"
            Sql += " AND "
            Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

            '発注基本取得
            Dim dsHattyu As DataSet = getDsData("t20_hattyu", Sql)

            Sql = " SELECT t43.*, t42.取消区分, t42.入庫日, t70.倉庫コード, t70.入出庫種別, t70.引当区分 "
            Sql += " FROM t43_nyukodt t43"

            Sql += " INNER JOIN t42_nyukohd t42 ON "
            Sql += " t43.""会社コード"" = t42.""会社コード"""
            Sql += " AND "
            Sql += " t43.""入庫番号"" = t42.""入庫番号"""

            Sql += " LEFT JOIN  t70_inout t70"
            Sql += " ON t70.会社コード = t42.会社コード"
            Sql += " AND  t70.入出庫区分 = '1'"
            'Sql += " AND  t70.倉庫コード = t42.倉庫コード"
            Sql += " AND  t70.伝票番号 = t42.入庫番号"
            Sql += " AND  t70.行番号 = t43.行番号"

            Sql += " where "
            Sql += " t43.""会社コード"" = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND "
            Sql += "t43.発注番号"
            Sql += " ILIKE '" & No & "'"
            Sql += " AND "
            Sql += "t43.発注番号枝番"
            Sql += " ILIKE '" & Suffix & "'"
            Sql += " AND "
            Sql += "t42.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
            Sql += " ORDER BY t43.行番号"

            '入庫明細取得
            Dim dsNyukodt As DataSet = _db.selectDB(Sql, RS, reccnt)

            Sql = " SELECT t21.メーカー, t21.品名, t21.型式, t21.発注数量, t21.単位, t21.仕入数量, t21.仕入値, t21.仕入金額"
            Sql += ", t21.発注残数, t21.未入庫数, t21.行番号, t21.仕入区分, t21.仕入先名, t21.備考, t20.取消区分, t20.更新日 "
            Sql += " FROM "
            Sql += " t21_hattyu t21"
            Sql += " INNER JOIN t20_hattyu t20 ON "

            Sql += " t21.""会社コード"" = t20.""会社コード"""
            Sql += " AND "
            Sql += " t21.""発注番号"" = t20.""発注番号"""
            Sql += " AND "
            Sql += " t21.""発注番号枝番"" = t20.""発注番号枝番"""

            Sql += " where "
            Sql += " t21.""会社コード"" = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND "
            Sql += "t21.発注番号"
            Sql += " ILIKE '" & No & "'"
            Sql += " AND "
            Sql += "t21.発注番号枝番"
            Sql += " ILIKE '" & Suffix & "'"
            Sql += " AND "
            Sql += "t20.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

            '発注明細取得
            Dim dsHattyudt As DataSet = _db.selectDB(Sql, RS, reccnt)

            For i As Integer = 0 To dsHattyudt.Tables(RS).Rows.Count - 1
                DgvPurchase.Rows.Add()

                DgvPurchase.Rows(i).Cells("メーカー").Value = dsHattyudt.Tables(RS).Rows(i)("メーカー")
                DgvPurchase.Rows(i).Cells("品名").Value = dsHattyudt.Tables(RS).Rows(i)("品名")
                DgvPurchase.Rows(i).Cells("型式").Value = dsHattyudt.Tables(RS).Rows(i)("型式")
                DgvPurchase.Rows(i).Cells("発注数量").Value = dsHattyudt.Tables(RS).Rows(i)("発注数量")
                DgvPurchase.Rows(i).Cells("単位").Value = dsHattyudt.Tables(RS).Rows(i)("単位")
                DgvPurchase.Rows(i).Cells("仕入数量").Value = dsHattyudt.Tables(RS).Rows(i)("仕入数量")
                DgvPurchase.Rows(i).Cells("仕入単価").Value = dsHattyudt.Tables(RS).Rows(i)("仕入値")
                DgvPurchase.Rows(i).Cells("仕入金額").Value = dsHattyudt.Tables(RS).Rows(i)("仕入金額")
                DgvPurchase.Rows(i).Cells("発注残数").Value = dsHattyudt.Tables(RS).Rows(i)("発注残数")
                DgvPurchase.Rows(i).Cells("未入庫数").Value = dsHattyudt.Tables(RS).Rows(i)("未入庫数")
                DgvPurchase.Rows(i).Cells("更新日").Value = dsHattyudt.Tables(RS).Rows(i)("更新日")
            Next

            For i As Integer = 0 To dsNyukodt.Tables(RS).Rows.Count - 1
                '汎用マスタから仕入区分名称取得
                sireKbn = getDsHanyoData(CommonConst.FIXED_KEY_PURCHASING_CLASS, dsNyukodt.Tables(RS).Rows(i)("仕入区分").ToString)

                DgvHistory.Rows.Add()
                DgvHistory.Rows(i).Cells("入庫番号").Value = dsNyukodt.Tables(RS).Rows(i)("入庫番号")
                DgvHistory.Rows(i).Cells("行番号").Value = dsNyukodt.Tables(RS).Rows(i)("行番号")
                DgvHistory.Rows(i).Cells("仕入区分").Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                                                sireKbn.Tables(RS).Rows(0)("文字２"),
                                                                sireKbn.Tables(RS).Rows(0)("文字１"))
                DgvHistory.Rows(i).Cells("メーカー").Value = dsNyukodt.Tables(RS).Rows(i)("メーカー")
                DgvHistory.Rows(i).Cells("品名").Value = dsNyukodt.Tables(RS).Rows(i)("品名")
                DgvHistory.Rows(i).Cells("型式").Value = dsNyukodt.Tables(RS).Rows(i)("型式")
                DgvHistory.Rows(i).Cells("単位").Value = dsNyukodt.Tables(RS).Rows(i)("単位")
                DgvHistory.Rows(i).Cells("仕入先").Value = dsNyukodt.Tables(RS).Rows(i)("仕入先名")
                DgvHistory.Rows(i).Cells("仕入値").Value = dsNyukodt.Tables(RS).Rows(i)("仕入値")
                DgvHistory.Rows(i).Cells("入庫数量").Value = dsNyukodt.Tables(RS).Rows(i)("入庫数量")

                If dsNyukodt.Tables(RS).Rows(i).IsNull(("倉庫コード")) = False Then
                    DgvHistory.Rows(i).Cells("倉庫").Value = getWarehouseName(dsNyukodt.Tables(RS).Rows(i)("倉庫コード"))
                End If
                If dsNyukodt.Tables(RS).Rows(i).IsNull(("入出庫種別")) = False Then
                    DgvHistory.Rows(i).Cells("入出庫種別").Value = getInOutName(dsNyukodt.Tables(RS).Rows(i)("入出庫種別"))
                End If
                If dsNyukodt.Tables(RS).Rows(i).IsNull(("引当区分")) = False Then
                    DgvHistory.Rows(i).Cells("引当区分").Value = getAssignName(dsNyukodt.Tables(RS).Rows(i)("引当区分"))
                End If

                DgvHistory.Rows(i).Cells("入庫日").Value = dsNyukodt.Tables(RS).Rows(i)("入庫日").ToShortDateString
                DgvHistory.Rows(i).Cells("備考").Value = dsNyukodt.Tables(RS).Rows(i)("備考")
            Next

            DgvAdd.Columns("No").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("No").ReadOnly = True
            DgvAdd.Columns("行番号").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("行番号").ReadOnly = True
            DgvAdd.Columns("仕入区分").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("仕入区分").ReadOnly = True
            DgvAdd.Columns("メーカー").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("メーカー").ReadOnly = True
            DgvAdd.Columns("品名").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("品名").ReadOnly = True
            DgvAdd.Columns("型式").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("型式").ReadOnly = True
            DgvAdd.Columns("単位").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("単位").ReadOnly = True
            DgvAdd.Columns("仕入先").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("仕入先").ReadOnly = True
            DgvAdd.Columns("仕入値").ReadOnly = True

            '入出庫種別コンボボックス作成
            Dim cmInOutKbn As New DataGridViewComboBoxColumn()
            cmInOutKbn.DataSource = getInOutKbn()
            '実際の値が"Value"列、表示するテキストが"Display"列とする
            cmInOutKbn.ValueMember = "Value"
            cmInOutKbn.DisplayMember = "Display"
            cmInOutKbn.HeaderText = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                        "StorageType",
                                        "入出庫種別")
            cmInOutKbn.Name = "入出庫種別"
            'If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            '    cmInOutKbn.HeaderText = "StorageType"
            'End If

            DgvAdd.Columns.Insert(10, cmInOutKbn)

            '引当区分コンボボックス作成
            Dim cmAllocationKbn As New DataGridViewComboBoxColumn()
            cmAllocationKbn.DataSource = getAssignKbn()
            '実際の値が"Value"列、表示するテキストが"Display"列とする
            cmAllocationKbn.ValueMember = "Value"
            cmAllocationKbn.DisplayMember = "Display"
            cmAllocationKbn.HeaderText = "引当区分"
            cmAllocationKbn.Name = "引当区分"
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                cmAllocationKbn.HeaderText = "AssignClassification"
            End If

            DgvAdd.Columns.Insert(11, cmAllocationKbn)

            'リードタイムのリストを汎用マスタから取得
            Dim dsHanyo As DataSet = getDsHanyoData(CommonConst.INOUT_CLASS)

            For index As Integer = 0 To dsHattyudt.Tables(RS).Rows.Count - 1
                If dsHattyudt.Tables(RS).Rows(index)("発注残数") <> 0 Then

                    '汎用マスタから仕入区分名称取得
                    sireKbn = getDsHanyoData(CommonConst.FIXED_KEY_PURCHASING_CLASS, dsHattyudt.Tables(RS).Rows(index)("仕入区分").ToString)

                    DgvAdd.Rows.Add()
                    DgvAdd.Rows(index).Cells("行番号").Value = dsHattyudt.Tables(RS).Rows(index)("行番号")
                    DgvAdd.Rows(index).Cells("仕入区分").Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                                                sireKbn.Tables(RS).Rows(0)("文字２"),
                                                                sireKbn.Tables(RS).Rows(0)("文字１"))
                    DgvAdd.Rows(index).Cells("仕入区分値").Value = dsHattyudt.Tables(RS).Rows(index)("仕入区分")
                    DgvAdd.Rows(index).Cells("メーカー").Value = dsHattyudt.Tables(RS).Rows(index)("メーカー")
                    DgvAdd.Rows(index).Cells("品名").Value = dsHattyudt.Tables(RS).Rows(index)("品名")
                    DgvAdd.Rows(index).Cells("型式").Value = dsHattyudt.Tables(RS).Rows(index)("型式")
                    DgvAdd.Rows(index).Cells("仕入先").Value = dsHattyudt.Tables(RS).Rows(index)("仕入先名")
                    DgvAdd.Rows(index).Cells("単位").Value = dsHattyudt.Tables(RS).Rows(index)("単位")
                    DgvAdd.Rows(index).Cells("仕入値").Value = dsHattyudt.Tables(RS).Rows(index)("仕入値")
                    DgvAdd.Rows(index).Cells("入庫数量").Value = 0
                    DgvAdd.Rows(index).Cells("入出庫種別").Value = dsHanyo.Tables(RS).Rows(0)("可変キー")
                    DgvAdd.Rows(index).Cells("引当区分").Value = CommonConst.AC_KBN_NORMAL
                    'DgvAdd.Rows(index).Cells("備考").Value = dsHattyudt.Tables(RS).Rows(index)("備考")
                End If
            Next

            '行番号の振り直し
            Dim i1 As Integer = DgvPurchase.Rows.Count()
            Dim No1 As Integer = 1
            For c As Integer = 0 To i1 - 1
                DgvPurchase.Rows(c).Cells(0).Value = No1
                No1 += 1
            Next c
            TxtCount1.Text = DgvPurchase.Rows.Count()

            Dim i2 As Integer = DgvHistory.Rows.Count()
            Dim No2 As Integer = 1
            For c As Integer = 0 To i2 - 1
                DgvHistory.Rows(c).Cells(0).Value = No2
                No2 += 1
            Next c
            TxtCount2.Text = DgvHistory.Rows.Count()

            Dim i3 As Integer = DgvAdd.Rows.Count()
            Dim No3 As Integer = 1
            For c As Integer = 0 To i3 - 1
                DgvAdd.Rows(c).Cells(0).Value = No3
                No3 += 1
            Next c
            TxtCount3.Text = DgvAdd.Rows.Count()

            TxtPurchaseNo.Text = dsHattyu.Tables(RS).Rows(0)("発注番号")
            TxtSuffixNo.Text = dsHattyu.Tables(RS).Rows(0)("発注番号枝番")
            TxtCustomerPO.Text = dsHattyu.Tables(RS).Rows(0)("客先番号")
            TxtOrdingDate.Text = dsHattyu.Tables(RS).Rows(0)("発注日").ToShortDateString()
            TxtSupplierCode.Text = dsHattyu.Tables(RS).Rows(0)("仕入先コード")
            TxtSupplierName.Text = dsHattyu.Tables(RS).Rows(0)("仕入先名")
            DtpReceiptDate.Value = Date.Now

            '入庫日の選択最小日を発注日にする
            DtpReceiptDate.MinDate = dsHattyu.Tables(RS).Rows(0)("発注日").ToShortDateString()

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    'セルの値が変更されたら
    Private Sub CellValueChanged(ByVal sender As Object,
    ByVal e As DataGridViewCellEventArgs) Handles DgvAdd.CellValueChanged

        Dim PurchaseTotal As Integer = 0

        'ヘッダー以外だったら
        If e.RowIndex > -1 Then

            '各項目の属性チェック
            If Not IsNumeric(DgvAdd.Rows(e.RowIndex).Cells("入庫数量").Value) And (DgvAdd.Rows(e.RowIndex).Cells("入庫数量").Value IsNot Nothing) Then
                _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
                DgvAdd.Rows(e.RowIndex).Cells("入庫数量").Value = 0
                Exit Sub
            End If
        End If

    End Sub

    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        'Dim openForm As Form = Nothing
        'openForm = New frmC01F30_Menu(_msgHd, _langHd, _db)
        'openForm.Show()
        Me.Close()
    End Sub

    '登録ボタン押下時
    Private Sub BtnRegist_Click(sender As Object, e As EventArgs) Handles BtnRegist.Click

        Dim dtToday As String = DateTime.Now
        Dim strToday As String = UtilClass.formatDatetime(dtToday)
        Dim reccnt As Integer = 0

        Dim Sql As String = ""

        Sql = " AND "
        Sql += "発注番号 ILIKE '" & No & "'"
        Sql += " AND "
        Sql += "発注番号枝番 ILIKE '" & Suffix & "'"
        Sql += " AND "
        Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '取消区分=0

        Dim dsHattyu As DataSet = getDsData("t20_hattyu", Sql)

        Sql = "SELECT t21.行番号, t21.入庫数, t21.未入庫数 FROM "
        Sql += " t21_hattyu t21"

        Sql += " INNER JOIN "
        Sql += " t20_hattyu t20"
        Sql += " ON "

        Sql += " t21.会社コード = t20.会社コード"
        Sql += " AND "
        Sql += " t21.発注番号 = t20.発注番号"
        Sql += " AND "
        Sql += " t21.発注番号枝番 = t20.発注番号枝番"

        Sql += " where t20.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND "
        Sql += "t20.発注番号 ILIKE '" & No & "'"
        Sql += " AND "
        Sql += "t20.発注番号枝番 ILIKE '" & Suffix & "'"
        Sql += " AND "
        Sql += "t20.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '取消区分=0

        Dim dsHattyudt As DataSet = _db.selectDB(Sql, RS, reccnt)

        Dim chkReceiptAmount As Integer = 0 '入庫データがあるか合算する用

        '発注明細の仕入数量と今回入庫数の合算が発注数量を超えたら
        'なぜ入庫時だけ発注まで見ているのか（仕入時も入庫をみないといけないのでは）

        '対象データがなかったらメッセージを表示
        If DgvAdd.RowCount = 0 Then
            '操作できないメッセージを表示
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)

            Return
        End If

        '最初に今回入庫に入力がなかったらエラーで返す
        For i As Integer = 0 To DgvAdd.RowCount - 1
            chkReceiptAmount += DgvAdd.Rows(i).Cells("入庫数量").Value
        Next
        If chkReceiptAmount <= 0 Then
            '対象データがないメッセージを表示
            _msgHd.dspMSG("chkReceiptAddError", frmC01F10_Login.loginValue.Language)

            Return
        End If


        If DgvPurchase.Rows(0).Cells("更新日").Value <> dsHattyu.Tables(RS).Rows(0)("更新日") Then
            '画面を開いたときの日時とデータの日時が異なっていた場合
            'データが誰かに変更された旨を伝える
            _msgHd.dspMSG("chkData", frmC01F10_Login.loginValue.Language)

            Return
        End If

        '発注明細をループさせながら
        '入庫数量が発注残数を超えない範囲か確認
        For i As Integer = 0 To DgvPurchase.RowCount - 1
            If (DgvPurchase.Rows(i).Cells("発注残数").Value < Integer.Parse(DgvAdd.Rows(i).Cells("入庫数量").Value)) Then

                '対象データがないメッセージを表示
                _msgHd.dspMSG("chkGRBalanceError", frmC01F10_Login.loginValue.Language)

                Return
            End If

        Next

        'ここからは登録データがある前提
        'それぞれ伝票番号を取得
        Dim PC As String = getSaiban("50", dtToday)
        Dim WH As String = getSaiban("60", dtToday)

        Try

            '入庫基本に発注基本の内容を登録
            Sql = "INSERT INTO "
            Sql += "Public."
            Sql += "t42_nyukohd("
            Sql += "会社コード, 入庫番号, 発注番号, 発注番号枝番, 客先番号, 仕入先コード, 仕入先名, 仕入先郵便番号"
            Sql += ", 仕入先住所, 仕入先電話番号, 仕入先ＦＡＸ, 仕入先担当者役職, 仕入先担当者名, 支払条件, 仕入金額"
            Sql += ", 粗利額, 営業担当者, 入力担当者, 備考, 取消区分, ＶＡＴ, ＰＰＨ, 入庫日, 登録日, 更新日, 更新者"
            Sql += ", 営業担当者コード, 入力担当者コード, 倉庫コード"
            Sql += ") VALUES ("
            Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += ", '"
            Sql += WH
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("発注番号").ToString
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("発注番号枝番").ToString
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("客先番号").ToString
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("仕入先コード").ToString
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("仕入先名").ToString
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("仕入先郵便番号").ToString
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("仕入先住所").ToString
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("仕入先電話番号").ToString
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("仕入先ＦＡＸ").ToString
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("仕入先担当者役職").ToString
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("仕入先担当者名").ToString
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("支払条件").ToString
            Sql += "', '"
            Sql += UtilClass.formatNumber(dsHattyu.Tables(RS).Rows(0)("仕入金額"))
            Sql += "', '"
            Sql += UtilClass.formatNumber(dsHattyu.Tables(RS).Rows(0)("粗利額"))
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("営業担当者").ToString
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("入力担当者").ToString
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("備考").ToString
            Sql += "', '"
            Sql += CommonConst.CANCEL_KBN_ENABLED.ToString
            Sql += "', '"
            Sql += UtilClass.formatNumber(dsHattyu.Tables(RS).Rows(0)("ＶＡＴ"))
            Sql += "', '"
            Sql += UtilClass.formatNumber(dsHattyu.Tables(RS).Rows(0)("ＰＰＨ"))
            Sql += "', '"
            Sql += UtilClass.strFormatDate(DtpReceiptDate.Text)
            Sql += "', '"
            Sql += strToday
            Sql += "', '"
            Sql += strToday
            Sql += "', '"
            Sql += Input
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("営業担当者コード").ToString
            Sql += "', '"
            Sql += frmC01F10_Login.loginValue.TantoCD
            Sql += "', '"
            Sql += CmWarehouse.SelectedValue.ToString
            Sql += " ')"

            _db.executeDB(Sql)

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try


        '発注明細をループさせながら
        For i As Integer = 0 To DgvPurchase.RowCount - 1

            '入庫数量が0以外だったら
            If Integer.Parse(DgvAdd.Rows(i).Cells("入庫数量").Value) <> 0 Then

                Try

                    '明細部分を登録する
                    Sql = "INSERT INTO "
                    Sql += "Public."
                    Sql += "t43_nyukodt("
                    Sql += "会社コード, 入庫番号, 発注番号, 発注番号枝番, 行番号, 仕入区分, メーカー"
                    Sql += ", 品名, 型式, 仕入先名, 仕入値, 入庫数量, 単位, 備考, 更新者, 更新日"
                    Sql += " )VALUES( "
                    Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"
                    Sql += ", '"
                    Sql += WH
                    Sql += "', '"
                    Sql += dsHattyu.Tables(RS).Rows(0)("発注番号").ToString
                    Sql += "', '"
                    Sql += dsHattyu.Tables(RS).Rows(0)("発注番号枝番").ToString
                    Sql += "', '"
                    Sql += DgvAdd.Rows(i).Cells("No").Value.ToString
                    Sql += "', '"
                    Sql += DgvAdd.Rows(i).Cells("仕入区分値").Value.ToString
                    Sql += "', '"
                    Sql += DgvAdd.Rows(i).Cells("メーカー").Value.ToString
                    Sql += "', '"
                    Sql += DgvAdd.Rows(i).Cells("品名").Value.ToString
                    Sql += "', '"
                    Sql += DgvAdd.Rows(i).Cells("型式").Value.ToString
                    Sql += "', '"
                    Sql += DgvAdd.Rows(i).Cells("仕入先").Value.ToString
                    Sql += "', '"
                    Sql += UtilClass.formatNumber(DgvAdd.Rows(i).Cells("仕入値").Value.ToString)
                    Sql += "', '"
                    Sql += UtilClass.formatNumber(DgvAdd.Rows(i).Cells("入庫数量").Value.ToString)
                    Sql += "', '"
                    Sql += DgvAdd.Rows(i).Cells("単位").Value.ToString
                    Sql += "', '"
                    Sql += DgvAdd.Rows(i).Cells("備考").Value
                    Sql += "', '"
                    Sql += Input
                    Sql += "', '"
                    Sql += strToday
                    Sql += " ')"

                    _db.executeDB(Sql)


                    '発注明細の「未入庫数」「入庫数」を更新
                    For x As Integer = 0 To dsHattyudt.Tables(RS).Rows.Count - 1
                        '行番号が一致したら発注明細更新
                        If DgvAdd.Rows(i).Cells("行番号").Value = dsHattyudt.Tables(RS).Rows(x)("行番号") Then
                            Dim calShukko As Integer = dsHattyudt.Tables(RS).Rows(x)("入庫数") + DgvAdd.Rows(i).Cells("入庫数量").Value
                            Dim calUnShukko As Integer = dsHattyudt.Tables(RS).Rows(x)("未入庫数") - DgvAdd.Rows(i).Cells("入庫数量").Value

                            Sql = "update t21_hattyu set "
                            Sql += "入庫数 = '" & calShukko & "'"
                            Sql += ",未入庫数 = '" & calUnShukko & "'"
                            Sql += ",更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
                            Sql += " where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                            Sql += " AND "
                            Sql += "発注番号 ILIKE '" & No & "'"
                            Sql += " AND "
                            Sql += "発注番号枝番 ILIKE '" & Suffix & "'"
                            Sql += " AND "
                            Sql += "行番号 = '" & DgvAdd.Rows(i).Cells("行番号").Value & "'"

                            _db.executeDB(Sql)

                            Sql = "update t20_hattyu set "
                            Sql += "更新日 = '" & strToday & "'"
                            Sql += ",更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
                            Sql += " where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                            Sql += " AND "
                            Sql += "発注番号 ILIKE '" & No & "'"
                            Sql += " AND "
                            Sql += "発注番号枝番 ILIKE '" & Suffix & "'"
                            Sql += " AND "
                            Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '取消区分=0

                            _db.executeDB(Sql)

                        End If

                    Next



                    't70_inout にデータ登録
                    Sql = "INSERT INTO "
                    Sql += "Public."
                    Sql += "t70_inout("
                    Sql += "会社コード, 入出庫区分, 倉庫コード, 伝票番号, 行番号, 入出庫種別"
                    Sql += ", 引当区分, メーカー, 品名, 型式, 数量, 単位, 備考, 入出庫日"
                    Sql += ", 取消区分, 更新者, 更新日)"
                    Sql += " VALUES('"
                    Sql += frmC01F10_Login.loginValue.BumonCD '会社コード
                    Sql += "', '"
                    Sql += "1" '入出庫区分 1.入庫, 2.出庫
                    Sql += "', '"
                    Sql += CmWarehouse.SelectedValue.ToString '倉庫コード
                    Sql += "', '"
                    Sql += WH '伝票番号
                    Sql += "', '"
                    Sql += DgvAdd.Rows(i).Cells("行番号").Value.ToString '行番号
                    Sql += "', '"
                    Sql += DgvAdd.Rows(i).Cells("入出庫種別").Value.ToString '入出庫種別
                    Sql += "', '"
                    Sql += DgvAdd.Rows(i).Cells("引当区分").Value.ToString '引当区分
                    Sql += "', '"
                    Sql += DgvAdd.Rows(i).Cells("メーカー").Value.ToString 'メーカー
                    Sql += "', '"
                    Sql += DgvAdd.Rows(i).Cells("品名").Value.ToString '品名
                    Sql += "', '"
                    Sql += DgvAdd.Rows(i).Cells("型式").Value.ToString '型式
                    Sql += "', '"
                    Sql += DgvAdd.Rows(i).Cells("入庫数量").Value.ToString '数量
                    Sql += "', '"
                    Sql += DgvAdd.Rows(i).Cells("単位").Value.ToString '単位
                    Sql += "', '"
                    Sql += DgvAdd.Rows(i).Cells("備考").Value '単位
                    Sql += "', '"
                    Sql += UtilClass.formatDatetime(DtpReceiptDate.Text) '入出庫日
                    Sql += "', '"
                    Sql += CommonConst.CANCEL_KBN_ENABLED.ToString '取消区分
                    Sql += "', '"
                    Sql += Input '更新者
                    Sql += "', '"
                    Sql += UtilClass.formatDatetime(dtToday) '更新日
                    Sql += "')"

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

        'Dim openForm As Form = Nothing
        'openForm = New OrderingList(_msgHd, _db, _langHd, Me, CommonConst.STATUS_RECEIPT)
        'openForm.Show()
        'Me.Close()

        Me.Dispose()
    End Sub

    'param1：String 採番キー
    'param2：DateTime 登録日
    'Return: String 伝票番号
    '伝票番号を取得
    Private Function getSaiban(ByVal key As String, ByVal today As DateTime) As String
        Dim Sql As String = ""
        Dim saibanID As String = ""
        Dim reccnt As Integer = 0 'DB用（デフォルト）

        Try
            Sql = "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public.m80_saiban"
            Sql += " WHERE "
            Sql += "会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND "
            Sql += "採番キー = '" & key & "'"

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

            Sql = "UPDATE "
            Sql += "Public.m80_saiban "
            Sql += "SET "
            Sql += " 最新値 "
            Sql += " = '"
            Sql += keyNo.ToString
            Sql += "', "
            Sql += "更新者"
            Sql += " = '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            Sql += "', "
            Sql += "更新日"
            Sql += " = '"
            Sql += UtilClass.formatDatetime(today)
            Sql += "' "
            Sql += "WHERE"
            Sql += " 会社コード"
            Sql += "='"
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "'"
            Sql += " AND"
            Sql += " 採番キー = '" & key & "'"

            _db.executeDB(Sql)

            Return saibanID
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

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
            CmWarehouse.SelectedIndex = prmVal
        Else
            CmWarehouse.SelectedValue = 1
        End If

    End Sub

    '汎用マスタから固定キー、可変キーに応じた結果を返す
    'param1：String 固定キー
    'param2：String 可変キー
    'Return: DataSet
    Private Function getDsHanyoData(ByVal prmFixed As String, Optional ByVal prmVariable As String = "") As DataSet
        Dim Sql As String = ""

        Sql = " AND "
        Sql += "固定キー ILIKE '" & prmFixed & "'"

        If prmVariable IsNot "" Then
            Sql += " AND "
            Sql += "可変キー ILIKE '" & prmVariable & "'"
        End If

        'リードタイムのリストを汎用マスタから取得
        Return getDsData("m90_hanyo", Sql)
    End Function


    Private Function getWarehouseName(ByVal prmString As String) As String
        Dim val As String = ""

        If val IsNot Nothing Then
            Dim Sql As String = " AND 倉庫コード ILIKE '" & prmString & "'"
            Dim dsWarehouse As DataSet = getDsData("m20_warehouse", Sql)

            If dsWarehouse.Tables(RS).Rows.Count <> 0 Then
                val = dsWarehouse.Tables(RS).Rows(0)("名称")
            End If
        End If

        Return val
    End Function

    'Return: DataTable
    Private Function getInOutKbn() As DataTable
        Dim Sql As String = ""
        Dim strViewText As String = ""

        Sql = " AND "
        Sql += "固定キー ILIKE '" & CommonConst.INOUT_CLASS & "'"
        Sql += " ORDER BY 表示順"

        'リードタイムのリストを汎用マスタから取得
        Dim dsHanyo As DataSet = getDsData("m90_hanyo", Sql)

        'ComboBoxに表示する項目のリストを作成する
        Dim table As New DataTable("Table")
        table.Columns.Add("Display", GetType(String))
        table.Columns.Add("Value", GetType(String))

        strViewText = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "文字２", "文字１")

        For x As Integer = 0 To dsHanyo.Tables(RS).Rows.Count - 1
            table.Rows.Add(dsHanyo.Tables(RS).Rows(x)(strViewText), dsHanyo.Tables(RS).Rows(x)("可変キー"))
        Next

        Return table
    End Function

    Private Function getInOutName(ByVal prmString As String) As String
        Dim Sql As String = " AND 固定キー ILIKE '" & CommonConst.INOUT_CLASS & "'"

        'Dim ds As DataSet = _db.selectDB("m90_hanyo", Sql)

        Dim ds As DataSet = getDsHanyoData(CommonConst.INOUT_CLASS, prmString)

        'Dim table2 As New DataTable("Table")
        'table2.Columns.Add("Display", GetType(String))
        'table2.Columns.Add("Value", GetType(Integer))

        Dim displayTxt As String = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "文字２", "文字１")

        Return ds.Tables(RS).Rows(0)(displayTxt)
    End Function


    '引当区分　Return: DataTable
    Private Function getAssignKbn() As DataTable
        Dim Sql As String = ""
        Dim strViewText As String = ""

        'ComboBoxに表示する項目のリストを作成する
        Dim table As New DataTable("Table")
        table.Columns.Add("Display", GetType(String))
        table.Columns.Add("Value", GetType(Integer))

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            table.Rows.Add(CommonConst.AC_KBN_NORMAL_TXT_ENG, CommonConst.AC_KBN_NORMAL)
            table.Rows.Add(CommonConst.AC_KBN_ASSIGN_TXT_ENG, CommonConst.AC_KBN_ASSIGN)
        Else
            table.Rows.Add(CommonConst.AC_KBN_NORMAL_TXT, CommonConst.AC_KBN_NORMAL)
            table.Rows.Add(CommonConst.AC_KBN_ASSIGN_TXT, CommonConst.AC_KBN_ASSIGN)
        End If

        Return table
    End Function


    Private Function getAssignName(ByVal prmString As String) As String
        Dim reString As String = ""

        If prmString = CommonConst.AC_KBN_NORMAL Then
            reString = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                           CommonConst.AC_KBN_NORMAL_TXT_ENG,
                           CommonConst.AC_KBN_NORMAL_TXT)
        Else
            reString = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                           CommonConst.AC_KBN_ASSIGN_TXT_ENG,
                           CommonConst.AC_KBN_ASSIGN_TXT)
        End If

        Return reString

    End Function

    'DGV内で指定列名(プルダウン)は一度のクリックで開く
    'それ以外は一回で入力状態にする
    Private Sub DgvAdd_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DgvAdd.CellEnter
        If DgvAdd.Columns(e.ColumnIndex).Name = "入出庫種別" Or DgvAdd.Columns(e.ColumnIndex).Name = "引当区分" Then
            SendKeys.Send("{F4}")
            'Else
            '    SendKeys.Send("{F2}")
        End If
    End Sub

End Class