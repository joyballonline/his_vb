Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


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

    Private CompanyCode As String = ""
    Private HattyuNo As String = ""
    Private Suffix As String = ""
    Private _parentForm As Form
    Private _status As String = ""
    Private checkAdd As Integer

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
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        DtpAPDate.Value = Date.Now
        _init = True

    End Sub

    '画面表示時
    Private Sub AccountsPayable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'ComboBoxに表示する項目のリストを作成する
        Dim table As New DataTable("Table")
        table.Columns.Add("Display", GetType(String))
        table.Columns.Add("Value", GetType(Integer))
        table.Rows.Add("前払金買掛", 1)
        table.Rows.Add("通常買掛", 2)

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

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            LblAccountsPayableDate.Text = "AccountsPayableDate"
            LblAccountsPayableDate.Location = New Point(237, 379)
            LblAccountsPayableDate.Size = New Size(205, 22)
            DtpAPDate.Location = New Point(458, 379)
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
            LblHistory.Text = "AccountsPayableHistoryData"
            LblAdd.Text = "AcceptAccountPayableThisTime"

            TxtHattyudtCount.Location = New Point(1228, 111)
            TxtKikehdCount.Location = New Point(1228, 245)
            TxtCount3.Location = New Point(1228, 387)

            BtnRegist.Text = "Registration"
            BtnBack.Text = "Back"

            DgvCymn.Columns("発注番号").HeaderText = "PurchaseOrderNumber"
            DgvCymn.Columns("発注番号枝番").HeaderText = "PurchaseOrderSubNumber"
            DgvCymn.Columns("発注日").HeaderText = "OrderDate"
            DgvCymn.Columns("仕入先").HeaderText = "SupplierName"
            DgvCymn.Columns("客先番号").HeaderText = "CustomerNumber"
            DgvCymn.Columns("発注金額").HeaderText = "PurchaseOrderAmount"
            DgvCymn.Columns("買掛金額計").HeaderText = "TotalAccountsPayable"
            DgvCymn.Columns("買掛残高").HeaderText = "AccountsPayableBalance"

            DgvCymndt.Columns("明細").HeaderText = "Purchase order details"
            DgvCymndt.Columns("メーカー").HeaderText = "Manufacturer"
            DgvCymndt.Columns("品名").HeaderText = "ItemName"
            DgvCymndt.Columns("型式").HeaderText = "Spec"
            DgvCymndt.Columns("発注個数").HeaderText = "OrderQuantity"
            DgvCymndt.Columns("単位").HeaderText = "Unit"
            DgvCymndt.Columns("仕入数量").HeaderText = "PurchaseQuantity"
            DgvCymndt.Columns("仕入単価").HeaderText = "PurchaseUnitPrice"
            DgvCymndt.Columns("仕入金額").HeaderText = "PurchaseAmount"

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

        End If

        If _status = "VIEW" Then

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

    End Sub

    '発注基本
    Private Sub BillLoad()

        Dim reccnt As Integer = 0
        Dim Sql As String = ""
        Dim AccountsPayable As Integer = 0 '買掛残高を集計

        Sql = " AND "
        Sql += "発注番号 ILIKE '" & HattyuNo & "'"
        Sql += " AND "
        Sql += "発注番号枝番 ILIKE '" & Suffix & "'"
        Sql += " AND "
        Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
        Sql += " ORDER BY 更新日 DESC "

        Dim dsHattyu As DataSet = getDsData("t20_hattyu", Sql)

        Sql = " SELECT t21.* "
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
        Sql += " ORDER BY 行番号 DESC "

        Dim dsHattyudt As DataSet = _db.selectDB(Sql, RS, reccnt)

        Sql = " AND "
        Sql += "発注番号 ILIKE '" & HattyuNo & "'"
        Sql += " AND "
        Sql += "発注番号枝番 ILIKE '" & Suffix & "'"
        Sql += " AND "
        Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
        Sql += " ORDER BY 更新日 DESC "

        Dim dsKikehd As DataSet = getDsData("t46_kikehd", Sql)

        '買掛残高を集計
        AccountsPayable = IIf(
            dsKikehd.Tables(RS).Compute("SUM(買掛金額計)", Nothing) IsNot DBNull.Value,
            dsKikehd.Tables(RS).Compute("SUM(買掛金額計)", Nothing),
            0
        )

        DgvCymn.Rows.Add()
        DgvCymn.Rows(0).Cells("発注番号").Value = dsHattyu.Tables(RS).Rows(0)("発注番号")
        DgvCymn.Rows(0).Cells("発注番号枝番").Value = dsHattyu.Tables(RS).Rows(0)("発注番号枝番")
        DgvCymn.Rows(0).Cells("発注日").Value = dsHattyu.Tables(RS).Rows(0)("発注日").ToShortDateString()
        DgvCymn.Rows(0).Cells("仕入先").Value = dsHattyu.Tables(RS).Rows(0)("仕入先名")
        DgvCymn.Rows(0).Cells("客先番号").Value = dsHattyu.Tables(RS).Rows(0)("客先番号").ToString
        DgvCymn.Rows(0).Cells("発注金額").Value = dsHattyu.Tables(RS).Rows(0)("仕入金額")
        DgvCymn.Rows(0).Cells("買掛金額計").Value = AccountsPayable
        DgvCymn.Rows(0).Cells("買掛残高").Value = dsHattyu.Tables(RS).Rows(0)("仕入金額") - AccountsPayable

        checkAdd = DgvCymn.Rows(0).Cells("買掛残高").Value

        For i As Integer = 0 To dsHattyudt.Tables(RS).Rows.Count - 1
            DgvCymndt.Rows.Add()
            DgvCymndt.Rows(i).Cells("明細").Value = dsHattyudt.Tables(RS).Rows(i)("行番号")
            DgvCymndt.Rows(i).Cells("メーカー").Value = dsHattyudt.Tables(RS).Rows(i)("メーカー")
            DgvCymndt.Rows(i).Cells("品名").Value = dsHattyudt.Tables(RS).Rows(i)("品名")
            DgvCymndt.Rows(i).Cells("型式").Value = dsHattyudt.Tables(RS).Rows(i)("型式")
            DgvCymndt.Rows(i).Cells("発注個数").Value = dsHattyudt.Tables(RS).Rows(i)("発注数量")
            DgvCymndt.Rows(i).Cells("単位").Value = dsHattyudt.Tables(RS).Rows(i)("単位")
            DgvCymndt.Rows(i).Cells("仕入数量").Value = dsHattyudt.Tables(RS).Rows(i)("仕入数量")
            DgvCymndt.Rows(i).Cells("仕入単価").Value = dsHattyudt.Tables(RS).Rows(i)("仕入値")
            DgvCymndt.Rows(i).Cells("仕入金額").Value = dsHattyudt.Tables(RS).Rows(i)("仕入金額")
        Next

        TxtHattyudtCount.Text = dsHattyudt.Tables(RS).Rows.Count

        For i As Integer = 0 To dsKikehd.Tables(RS).Rows.Count - 1
            DgvHistory.Rows.Add()
            DgvHistory.Rows(i).Cells("No").Value = i + 1
            DgvHistory.Rows(i).Cells("買掛番号").Value = dsKikehd.Tables(RS).Rows(i)("買掛番号")
            DgvHistory.Rows(i).Cells("買掛日").Value = dsKikehd.Tables(RS).Rows(i)("買掛日").ToShortDateString()
            DgvHistory.Rows(i).Cells("買掛区分").Value = IIf(dsKikehd.Tables(RS).Rows(i)("買掛区分") = CommonConst.APC_KBN_DEPOSIT.ToString,
                                                                                                    CommonConst.APC_KBN_DEPOSIT_TXT,
                                                                                                    CommonConst.APC_KBN_NORMAL_TXT)
            DgvHistory.Rows(i).Cells("支払先").Value = dsKikehd.Tables(RS).Rows(i)("仕入先名")
            DgvHistory.Rows(i).Cells("買掛金額").Value = dsKikehd.Tables(RS).Rows(i)("買掛金額計")
            DgvHistory.Rows(i).Cells("備考1").Value = dsKikehd.Tables(RS).Rows(i)("備考1")
            DgvHistory.Rows(i).Cells("備考2").Value = dsKikehd.Tables(RS).Rows(i)("備考2")
            DgvHistory.Rows(i).Cells("買掛済み発注番号").Value = dsKikehd.Tables(RS).Rows(i)("発注番号")
            DgvHistory.Rows(i).Cells("買掛済み発注番号枝番").Value = dsKikehd.Tables(RS).Rows(i)("発注番号枝番")
        Next

        TxtKikehdCount.Text = dsKikehd.Tables(RS).Rows.Count

        If DgvCymn.Rows(0).Cells("買掛残高").Value = 0 Then
        Else
            DgvAdd.Rows.Add()
            DgvAdd.Rows(0).Cells("AddNo").Value = 1
            DgvAdd(1, 0).Value = 2
            DgvAdd.Rows(0).Cells("今回支払先").Value = dsHattyu.Tables(RS).Rows(0)("仕入先名")
            DgvAdd.Rows(0).Cells("今回買掛金額計").Value = 0

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
        Dim errflg As Boolean = True
        Dim dtToday As DateTime = DateTime.Now
        Dim reccnt As Integer = 0
        Dim AccountsPayable As Integer = 0

        If DgvAdd.Rows.Count() > 0 Then

            Dim Saiban1 As String = ""
            Dim Sql1 As String = ""
            Dim Sql2 As String = ""
            Dim Sql3 As String = ""
            Dim Sql4 As String = ""

            Saiban1 += "SELECT "
            Saiban1 += "* "
            Saiban1 += "FROM "
            Saiban1 += "public"
            Saiban1 += "."
            Saiban1 += "m80_saiban"
            Saiban1 += " WHERE "
            Saiban1 += "採番キー"
            Saiban1 += " ILIKE "
            Saiban1 += "'"
            Saiban1 += "100"
            Saiban1 += "'"

            Dim dsSaiban1 As DataSet = _db.selectDB(Saiban1, RS, reccnt)

            Dim AP As String = dsSaiban1.Tables(RS).Rows(0)("接頭文字")
            AP += dtToday.ToString("MMdd")
            AP += dsSaiban1.Tables(RS).Rows(0)("最新値").ToString.PadLeft(dsSaiban1.Tables(RS).Rows(0)("連番桁数"), "0")

            Sql1 += "SELECT "
            Sql1 += "* "
            Sql1 += "FROM "
            Sql1 += "public"
            Sql1 += "."
            Sql1 += "t20_hattyu"
            Sql1 += " WHERE "
            Sql1 += "発注番号"
            Sql1 += " ILIKE "
            Sql1 += "'"
            Sql1 += HattyuNo
            Sql1 += "'"
            Sql1 += " AND "
            Sql1 += "発注番号枝番"
            Sql1 += " ILIKE "
            Sql1 += "'"
            Sql1 += Suffix
            Sql1 += "'"

            Dim ds1 As DataSet = _db.selectDB(Sql1, RS, reccnt)

            Sql2 += "SELECT "
            Sql2 += "* "
            Sql2 += "FROM "
            Sql2 += "public"
            Sql2 += "."
            Sql2 += "t46_kikehd"
            Sql2 += " WHERE "
            Sql2 += "発注番号"
            Sql2 += " ILIKE "
            Sql2 += "'"
            Sql2 += HattyuNo
            Sql2 += "'"
            Sql2 += " AND "
            Sql2 += "発注番号枝番"
            Sql2 += " ILIKE "
            Sql2 += "'"
            Sql2 += Suffix
            Sql2 += "'"

            Dim ds2 As DataSet = _db.selectDB(Sql2, RS, reccnt)

            For index As Integer = 0 To ds2.Tables(RS).Rows.Count - 1
                AccountsPayable += ds2.Tables(RS).Rows(index)("買掛金額計")
            Next


            Dim APTotal As Integer = DgvAdd.Rows(0).Cells("今回買掛金額計").Value + AccountsPayable
            Dim Balance As Integer = ds1.Tables(RS).Rows(0)("仕入金額") - APTotal

            If Balance < 0 Then
                If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                    MessageBox.Show("Total accounts payable amount exceeds purchase order amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    MessageBox.Show("買掛金額計が発注金額を超えています。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

                errflg = False
            End If

            If DgvAdd.Rows(0).Cells("今回買掛金額計").Value = 0 Then
                If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                    MessageBox.Show("Total accounts payable amount is 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    MessageBox.Show("買掛金額計が0になっています。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

                errflg = False
            End If

            If errflg Then
                Sql3 = ""
                Sql3 += "INSERT INTO "
                Sql3 += "Public."
                Sql3 += "t46_kikehd("
                Sql3 += "会社コード, 買掛番号, 買掛区分, 買掛日, 発注番号, 発注番号枝番, 客先番号, 仕入先コード, 仕入先名, 買掛金額計, 買掛残高, 備考1, 備考2, 取消区分, 登録日, 更新者, 更新日)"
                Sql3 += " VALUES('"
                Sql3 += ds1.Tables(RS).Rows(0)("会社コード").ToString
                Sql3 += "', '"
                Sql3 += AP
                Sql3 += "', '"
                Sql3 += DgvAdd.Rows(0).Cells("買掛区分").Value.ToString
                Sql3 += "', '"
                Sql3 += DtpAPDate.Value
                Sql3 += "', '"
                Sql3 += ds1.Tables(RS).Rows(0)("発注番号").ToString
                Sql3 += "', '"
                Sql3 += ds1.Tables(RS).Rows(0)("発注番号枝番").ToString
                Sql3 += "', '"
                Sql3 += ds1.Tables(RS).Rows(0)("客先番号").ToString
                Sql3 += "', '"
                Sql3 += ds1.Tables(RS).Rows(0)("仕入先コード").ToString
                Sql3 += "', '"
                Sql3 += ds1.Tables(RS).Rows(0)("仕入先名").ToString
                Sql3 += "', '"
                Sql3 += DgvAdd.Rows(0).Cells("今回買掛金額計").Value
                Sql3 += "', '"
                Sql3 += DgvAdd.Rows(0).Cells("今回買掛金額計").Value
                Sql3 += "', '"
                Sql3 += DgvAdd.Rows(0).Cells("今回備考1").Value
                Sql3 += "', '"
                Sql3 += DgvAdd.Rows(0).Cells("今回備考2").Value
                Sql3 += "', '"
                Sql3 += "0"
                Sql3 += "', '"
                Sql3 += dtToday
                Sql3 += "', '"
                Sql3 += frmC01F10_Login.loginValue.TantoNM
                Sql3 += "', '"
                Sql3 += dtToday
                Sql3 += " ')"
                Sql3 += "RETURNING 会社コード"
                Sql3 += ", "
                Sql3 += "買掛番号"
                Sql3 += ", "
                Sql3 += "買掛区分"
                Sql3 += ", "
                Sql3 += "買掛日"
                Sql3 += ", "
                Sql3 += "発注番号"
                Sql3 += ", "
                Sql3 += "発注番号枝番"
                Sql3 += ", "
                Sql3 += "客先番号"
                Sql3 += ", "
                Sql3 += "仕入先コード"
                Sql3 += ", "
                Sql3 += "仕入先名"
                Sql3 += ", "
                Sql3 += "買掛金額計"
                Sql3 += ", "
                Sql3 += "買掛残高"
                Sql3 += ", "
                Sql3 += "備考1"
                Sql3 += ", "
                Sql3 += "備考2"
                Sql3 += ", "
                Sql3 += "取消区分"
                Sql3 += ", "
                Sql3 += "登録日"
                Sql3 += ", "
                Sql3 += "更新者"

                _db.executeDB(Sql3)

                Dim APNo As Integer

                If dsSaiban1.Tables(RS).Rows(0)("最新値") = dsSaiban1.Tables(RS).Rows(0)("最大値") Then
                    APNo = dsSaiban1.Tables(RS).Rows(0)("最小値")
                Else
                    APNo = dsSaiban1.Tables(RS).Rows(0)("最新値") + 1
                End If

                Sql4 = ""
                Sql4 += "UPDATE "
                Sql4 += "Public."
                Sql4 += "m80_saiban "
                Sql4 += "SET "
                Sql4 += " 最新値"
                Sql4 += " = '"
                Sql4 += APNo.ToString
                Sql4 += "', "
                Sql4 += "更新者"
                Sql4 += " = '"
                Sql4 += frmC01F10_Login.loginValue.TantoNM
                Sql4 += "', "
                Sql4 += "更新日"
                Sql4 += " = '"
                Sql4 += dtToday
                Sql4 += "' "
                Sql4 += "WHERE"
                Sql4 += " 会社コード"
                Sql4 += "='"
                Sql4 += ds1.Tables(RS).Rows(0)("会社コード").ToString
                Sql4 += "'"
                Sql4 += " AND"
                Sql4 += " 採番キー"
                Sql4 += "='"
                Sql4 += "100"
                Sql4 += "' "
                Sql4 += "RETURNING 会社コード"
                Sql4 += ", "
                Sql4 += "採番キー"
                Sql4 += ", "
                Sql4 += "最新値"
                Sql4 += ", "
                Sql4 += "最小値"
                Sql4 += ", "
                Sql4 += "最大値"
                Sql4 += ", "
                Sql4 += "接頭文字"
                Sql4 += ", "
                Sql4 += "連番桁数"
                Sql4 += ", "
                Sql4 += "更新者"
                Sql4 += ", "
                Sql4 += "更新日"

                _db.executeDB(Sql4)

                _parentForm.Enabled = True
                _parentForm.Show()
                Me.Dispose()
            End If

        Else
            '登録するデータがなかったら
            _msgHd.dspMSG("NonAddData", frmC01F10_Login.loginValue.Language)
        End If


    End Sub

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

End Class