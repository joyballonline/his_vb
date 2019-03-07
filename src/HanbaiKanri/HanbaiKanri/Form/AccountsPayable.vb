Option Explicit On

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
        DtpPaymentDate.Value = Date.Now
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

        DtpAPDate.MinDate = dsHattyu.Tables(RS).Rows(0)("発注日").ToShortDateString()
        DtpPaymentDate.MinDate = dsHattyu.Tables(RS).Rows(0)("発注日").ToShortDateString()

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

        Dim dtToday As DateTime = DateTime.Now
        Dim strToday As String = formatDatetime(dtToday)
        Dim reccnt As Integer = 0
        Dim AccountsPayable As Decimal = 0

        If DgvAdd.Rows.Count() > 0 Then

            Dim Sql As String = ""

            Dim AP As String = getSaiban("100", dtToday)

            Sql = " AND "
            Sql += "発注番号 ILIKE '" & HattyuNo & "'"
            Sql += " AND "
            Sql += "発注番号枝番 ILIKE '" & Suffix & "'"
            Sql += " AND "
            Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

            Dim dsHattyu As DataSet = getDsData("t20_hattyu", Sql)


            Sql = " AND "
            Sql += "発注番号 ILIKE '" & HattyuNo & "'"
            Sql += " AND "
            Sql += "発注番号枝番 ILIKE '" & Suffix & "'"
            Sql += " AND "
            Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

            Dim dsKikehd As DataSet = getDsData("t46_kikehd", Sql)

            '買掛残高を集計
            AccountsPayable = IIf(
            dsKikehd.Tables(RS).Compute("SUM(買掛金額計)", Nothing) IsNot DBNull.Value,
            dsKikehd.Tables(RS).Compute("SUM(買掛金額計)", Nothing),
            0)

            Dim APTotal As Decimal = DgvAdd.Rows(0).Cells("今回買掛金額計").Value + AccountsPayable
            Dim Balance As Decimal = dsHattyu.Tables(RS).Rows(0)("仕入金額") - APTotal

            If Balance < 0 Then
                '対象データがないメッセージを表示
                _msgHd.dspMSG("chkAPDataError", frmC01F10_Login.loginValue.Language)

                Return
            End If

            If DgvAdd.Rows(0).Cells("今回買掛金額計").Value = 0 Then
                '対象データがないメッセージを表示
                _msgHd.dspMSG("NonData", frmC01F10_Login.loginValue.Language)

                Return
            End If

            Sql = "INSERT INTO "
            Sql += "Public."
            Sql += "t46_kikehd("
            Sql += "会社コード, 買掛番号, 買掛区分, 買掛日, 発注番号, 発注番号枝番, 客先番号, 仕入先コード, 仕入先名, 買掛金額計, 買掛残高"
            Sql += ", 備考1, 備考2, 取消区分, 登録日, 更新者, 更新日, 支払予定日)"
            Sql += " VALUES('"
            Sql += dsHattyu.Tables(RS).Rows(0)("会社コード").ToString '会社コード
            Sql += "', '"
            Sql += AP '買掛番号
            Sql += "', '"
            Sql += DgvAdd.Rows(0).Cells("買掛区分").Value.ToString '買掛区分
            Sql += "', '"
            Sql += strFormatDate(DtpAPDate.Value) '買掛日
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
            Sql += "', '"
            Sql += DgvAdd.Rows(0).Cells("今回買掛金額計").Value '買掛金額計
            Sql += "', '"
            Sql += DgvAdd.Rows(0).Cells("今回買掛金額計").Value '買掛残高
            Sql += "', '"
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
            Sql += strFormatDate(DtpPaymentDate.Value) '支払予定日
            Sql += " ')"

            _db.executeDB(Sql)

            _parentForm.Enabled = True
            _parentForm.Show()
            Me.Dispose()

        Else
            '登録するデータがなかったら
            _msgHd.dspMSG("NonAddData", frmC01F10_Login.loginValue.Language)
        End If


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
            Sql += formatDatetime(today)
            Sql += "' "
            Sql += "WHERE"
            Sql += " 会社コード"
            Sql += "='"
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "'"
            Sql += " AND"
            Sql += " 採番キー = '" & key & "'"
            Console.WriteLine(Sql)
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

End Class