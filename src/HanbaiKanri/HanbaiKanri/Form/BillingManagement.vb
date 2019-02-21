Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class BillingManagement
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
    Private CymnNo As String = ""
    Private Suffix As String = ""
    Private _parentForm As Form
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
        CymnNo = prmRefNo
        Suffix = prmRefSuffix
        _status = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        DtpBillingDate.Value = Date.Now
        _init = True

    End Sub

    Private Sub BillingManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'ComboBoxに表示する項目のリストを作成する
        Dim table As New DataTable("Table")
        table.Columns.Add("Display", GetType(String))
        table.Columns.Add("Value", GetType(Integer))
        table.Rows.Add("前受金請求", 1)
        table.Rows.Add("通常請求", 2)

        'DataGridViewComboBoxColumnを作成
        Dim column As New DataGridViewComboBoxColumn()
        'DataGridViewComboBoxColumnのDataSourceを設定
        column.DataSource = table
        '実際の値が"Value"列、表示するテキストが"Display"列とする
        column.ValueMember = "Value"
        column.DisplayMember = "Display"
        column.HeaderText = "請求区分"
        column.Name = "請求区分"
        'column.ValueMember = 1
        'DataGridView1に追加する
        DgvAdd.Columns.Insert(1, column)

        '一覧系取得
        BillLoad()

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblBillingDate.Text = "BillingDate"
            LblNo1.Text = "Record"
            LblNo1.Location = New Point(1272, 118)
            LblNo1.Size = New Size(66, 22)
            LblNo2.Text = "Record"
            LblNo2.Location = New Point(1272, 253)
            LblNo2.Size = New Size(66, 22)
            LblNo3.Text = "Record"
            LblNo3.Location = New Point(1272, 387)
            LblNo3.Size = New Size(66, 22)
            LblRemarks1.Text = "Remarks1"
            LblRemarks2.Text = "Remarks2"
            LblCymndt.Text = "JobOrderDetails"
            LblHistory.Text = "BillingHistoryData"
            LblAdd.Text = "InvoicingThisTime"

            TxtDgvCymndtCount.Location = New Point(1228, 118)
            TxtDgvHistoryCount.Location = New Point(1228, 253)
            TxtDgvAddCount.Location = New Point(1228, 387)

            BtnRegist.Text = "Registration"
            BtnBack.Text = "Back"

            DgvCymn.Columns("受注番号").HeaderText = "JobOrderNumber"
            DgvCymn.Columns("受注日").HeaderText = "OrderDate"
            DgvCymn.Columns("得意先").HeaderText = "CustomerName"
            DgvCymn.Columns("客先番号").HeaderText = "CustomerNumber"
            DgvCymn.Columns("受注金額").HeaderText = "JobOrderAmount"
            DgvCymn.Columns("請求金額計").HeaderText = "TotalBillingAmount"
            DgvCymn.Columns("請求残高").HeaderText = "BillingBalance"

            DgvCymndt.Columns("明細").HeaderText = "DetailData"
            DgvCymndt.Columns("メーカー").HeaderText = "Manufacturer"
            DgvCymndt.Columns("品名").HeaderText = "ItemName"
            DgvCymndt.Columns("型式").HeaderText = "Spec"
            DgvCymndt.Columns("受注個数").HeaderText = "JobOrderQuantity"
            DgvCymndt.Columns("単位").HeaderText = "Unit"
            DgvCymndt.Columns("売上数量").HeaderText = "SalesQuantity"
            DgvCymndt.Columns("売上単価").HeaderText = "SellingPrice"
            DgvCymndt.Columns("売上金額").HeaderText = "SalesAmount"

            DgvHistory.Columns("請求番号").HeaderText = "InvoiceNumber"
            DgvHistory.Columns("請求日").HeaderText = "BillingDate"
            DgvHistory.Columns("請求区分").HeaderText = "BillingClassification"
            DgvHistory.Columns("請求先").HeaderText = "BillingAddress"
            DgvHistory.Columns("請求金額").HeaderText = "BillingAmount"
            DgvHistory.Columns("備考1").HeaderText = "Remarks1"
            DgvHistory.Columns("備考2").HeaderText = "Remarks2"

            DgvAdd.Columns("請求区分").HeaderText = "BillingClassification"
            DgvAdd.Columns("今回請求先").HeaderText = "BillingAddress"
            DgvAdd.Columns("今回請求金額計").HeaderText = "TotalBillingAmount"
            DgvAdd.Columns("今回備考1").HeaderText = "Remarks1"
            DgvAdd.Columns("今回備考2").HeaderText = "Remarks2"

        End If
        If _status = "VIEW" Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If

            LblNo1.Visible = False
            LblNo2.Visible = False
            LblNo2.Visible = False
            LblCymndt.Visible = False
            LblAdd.Visible = False
            LblBillingDate.Visible = False
            DtpBillingDate.Visible = False
            TxtDgvCymndtCount.Visible = False
            TxtDgvHistoryCount.Visible = False
            TxtDgvAddCount.Visible = False
            DgvCymn.Visible = False
            DgvCymndt.Visible = False
            DgvAdd.Visible = False
            DgvHistory.ReadOnly = False

            LblHistory.Location = New Point(12, 82)
            DgvHistory.Location = New Point(12, 106)
            DgvHistory.Size = New Point(1326, 566)

            BtnRegist.Visible = False
        Else
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "BillingRegistrationMode"
            Else
                LblMode.Text = "請求登録モード"
            End If

        End If

    End Sub

    Private Sub BillLoad()
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Sql = " AND "
        Sql += "受注番号 ILIKE '" & CymnNo & "'"
        Sql += " AND "
        Sql += "受注番号枝番 ILIKE '" & Suffix & "'"
        Sql += " AND "
        Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        '受注基本取得
        Dim dsCymnhd As DataSet = getDsData("t10_cymnhd", Sql)

        'joinするのでとりあえず直書き
        Sql = "SELECT"
        Sql += " t11.行番号, t11.メーカー, t11.品名, t11.型式, t11.受注数量, t11.単位, t11.売上数量, t11.売単価, t11.売上金額"
        Sql += " FROM "
        Sql += " public.t11_cymndt t11 "

        Sql += " INNER JOIN "
        Sql += " t10_cymnhd t10"
        Sql += " ON "

        Sql += " t11.会社コード = t10.会社コード"
        Sql += " AND "
        Sql += " t11.受注番号 = t10.受注番号"
        Sql += " AND "
        Sql += " t11.受注番号 = t10.受注番号"

        Sql += " WHERE "
        Sql += " t11.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonNM & "'"
        Sql += " AND "
        Sql += " t11.受注番号 ILIKE '" & CymnNo & "'"
        Sql += " AND "
        Sql += " t11.受注番号枝番 ILIKE '" & Suffix & "'"
        Sql += " AND "
        Sql += "t10.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        '受注明細取得
        Dim dsCymndt As DataSet = _db.selectDB(Sql, RS, reccnt)

        Sql = " AND "
        Sql += "受注番号 ILIKE '" & CymnNo & "'"
        Sql += " AND "
        Sql += "受注番号枝番 ILIKE '" & Suffix & "'"
        Sql += " AND "
        Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        '請求基本取得
        Dim dsSkyuhd As DataSet = getDsData("t23_skyuhd", Sql)

        Dim BillingAmount As Long = 0

        '請求金額計を集計
        BillingAmount = IIf(
            dsSkyuhd.Tables(RS).Compute("SUM(請求金額計)", Nothing) IsNot DBNull.Value,
            dsSkyuhd.Tables(RS).Compute("SUM(請求金額計)", Nothing),
            0
        )

        '受注データ、見積データから対象の請求金額・請求残高を表示
        DgvCymn.Rows.Add()
        DgvCymn.Rows(0).Cells("受注番号").Value = dsCymnhd.Tables(RS).Rows(0)("受注番号")
        DgvCymn.Rows(0).Cells("受注日").Value = dsCymnhd.Tables(RS).Rows(0)("受注日")
        DgvCymn.Rows(0).Cells("得意先").Value = dsCymnhd.Tables(RS).Rows(0)("得意先名")
        DgvCymn.Rows(0).Cells("客先番号").Value = dsCymnhd.Tables(RS).Rows(0)("客先番号").ToString
        DgvCymn.Rows(0).Cells("受注金額").Value = dsCymnhd.Tables(RS).Rows(0)("見積金額")
        DgvCymn.Rows(0).Cells("請求金額計").Value = BillingAmount
        DgvCymn.Rows(0).Cells("請求残高").Value = dsCymnhd.Tables(RS).Rows(0)("見積金額") - BillingAmount

        '受注明細データから対象のデータの詳細を表示
        For i As Integer = 0 To dsCymndt.Tables(RS).Rows.Count - 1
            DgvCymndt.Rows.Add()
            DgvCymndt.Rows(i).Cells("明細").Value = dsCymndt.Tables(RS).Rows(i)("行番号")
            DgvCymndt.Rows(i).Cells("メーカー").Value = dsCymndt.Tables(RS).Rows(i)("メーカー")
            DgvCymndt.Rows(i).Cells("品名").Value = dsCymndt.Tables(RS).Rows(i)("品名")
            DgvCymndt.Rows(i).Cells("型式").Value = dsCymndt.Tables(RS).Rows(i)("型式")
            DgvCymndt.Rows(i).Cells("受注個数").Value = dsCymndt.Tables(RS).Rows(i)("受注数量")
            DgvCymndt.Rows(i).Cells("単位").Value = dsCymndt.Tables(RS).Rows(i)("単位")
            DgvCymndt.Rows(i).Cells("売上数量").Value = dsCymndt.Tables(RS).Rows(i)("売上数量")
            DgvCymndt.Rows(i).Cells("売上単価").Value = dsCymndt.Tables(RS).Rows(i)("売単価")
            DgvCymndt.Rows(i).Cells("売上金額").Value = dsCymndt.Tables(RS).Rows(i)("売上金額")
        Next

        '受注明細の件数カウント
        TxtDgvCymndtCount.Text = dsCymndt.Tables(RS).Rows.Count

        '請求データから請求済みデータ一覧を表示
        For i As Integer = 0 To dsSkyuhd.Tables(RS).Rows.Count - 1
            DgvHistory.Rows.Add()
            DgvHistory.Rows(i).Cells("No").Value = i + 1
            DgvHistory.Rows(i).Cells("請求番号").Value = dsSkyuhd.Tables(RS).Rows(i)("請求番号")
            DgvHistory.Rows(i).Cells("請求日").Value = dsSkyuhd.Tables(RS).Rows(i)("請求日")

            DgvHistory.Rows(i).Cells("請求区分").Value = IIf(
                DgvHistory.Rows(i).Cells("請求区分").Value,
                CommonConst.BILLING_KBN_DEPOSIT_TXT,
                CommonConst.BILLING_KBN_NORMAL_TXT
            )

            DgvHistory.Rows(i).Cells("請求先").Value = dsSkyuhd.Tables(RS).Rows(i)("得意先名")
            DgvHistory.Rows(i).Cells("請求金額").Value = dsSkyuhd.Tables(RS).Rows(i)("請求金額計")
            DgvHistory.Rows(i).Cells("備考1").Value = dsSkyuhd.Tables(RS).Rows(i)("備考1")
            DgvHistory.Rows(i).Cells("備考2").Value = dsSkyuhd.Tables(RS).Rows(i)("備考2")
            DgvHistory.Rows(i).Cells("請求済み受注番号").Value = dsSkyuhd.Tables(RS).Rows(i)("受注番号")
            DgvHistory.Rows(i).Cells("請求済み受注番号枝番").Value = dsSkyuhd.Tables(RS).Rows(i)("受注番号枝番")
        Next

        '請求済みデータ件数カウント
        TxtDgvHistoryCount.Text = dsSkyuhd.Tables(RS).Rows.Count

        '請求データの残高が0じゃなかったら入力欄を表示
        If DgvCymn.Rows(0).Cells("請求残高").Value <> 0 Then
            DgvAdd.Rows.Add()
            DgvAdd.Rows(0).Cells("AddNo").Value = 1
            DgvAdd(1, 0).Value = 2
            DgvAdd.Rows(0).Cells("今回請求先").Value = dsCymnhd.Tables(RS).Rows(0)("得意先名")
            DgvAdd.Rows(0).Cells("今回請求金額計").Value = 0

            '請求データ作成件数カウント
            TxtDgvAddCount.Text = 1
        End If
    End Sub

    '前の画面に戻る
    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    '複製ボタン押下時
    Private Sub BtnClone_Click(sender As Object, e As EventArgs) Handles BtnClone.Click
        'メニュー選択処理
        Dim RowIdx As Integer
        Dim Item(5) As String

        '一覧選択行インデックスの取得

        RowIdx = DgvAdd.CurrentCell.RowIndex


        '選択行の値を格納
        For c As Integer = 0 To 5
            Item(c) = DgvAdd.Rows(RowIdx).Cells(c).Value
        Next c

        '行を挿入
        DgvAdd.Rows.Insert(RowIdx + 1)

        '追加した行に複製元の値を格納
        For c As Integer = 0 To 5
            If c = 1 Then
                If Item(c) IsNot Nothing Then
                    Dim tmp As Integer = Item(c)
                    DgvAdd(1, RowIdx + 1).Value = tmp
                End If
            Else
                DgvAdd.Rows(RowIdx + 1).Cells(c).Value = Item(c)
            End If

        Next c

        '最終行のインデックスを取得
        Dim index As Integer = DgvAdd.Rows.Count()
        '行番号の振り直し
        Dim No As Integer = 1
        For c As Integer = 0 To index - 1
            DgvAdd.Rows(c).Cells(0).Value = No
            No += 1
        Next c
        TxtDgvAddCount.Text = DgvAdd.Rows.Count()
    End Sub

    '行削除ボタン押下時
    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        For Each r As DataGridViewCell In DgvAdd.SelectedCells
            DgvAdd.Rows.RemoveAt(r.RowIndex)
        Next r

        '行番号の振り直し
        Dim index As Integer = DgvAdd.Rows.Count()
        Dim No As Integer = 1
        For c As Integer = 0 To index - 1
            DgvAdd.Rows(c).Cells(0).Value = No
            No += 1
        Next c
        TxtDgvAddCount.Text = DgvAdd.Rows.Count()
    End Sub

    '請求登録
    Private Sub BtnRegist_Click(sender As Object, e As EventArgs) Handles BtnRegist.Click
        Dim errflg As Boolean = True
        Dim dtToday As DateTime = DateTime.Now
        Dim reccnt As Integer = 0
        Dim BillingAmount As Decimal = 0

        Dim Sql As String = ""

        If Decimal.Parse(DgvAdd.Rows(0).Cells("今回請求金額計").Value) > Decimal.Parse(DgvCymn.Rows(0).Cells("請求残高").Value) Then
            '請求残高より請求金額が大きい場合はアラート
            _msgHd.dspMSG("chkBillingBalanceError", frmC01F10_Login.loginValue.Language)

            Return
        End If


        Sql = " AND "
        Sql += "受注番号 ILIKE '" & CymnNo & "'"
        Sql += " AND "
        Sql += "受注番号枝番 ILIKE '" & Suffix & "'"
        Sql += " AND "
        Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        Dim dsCymnhd As DataSet = getDsData("t10_cymnhd", Sql)

        Sql = " AND "
        Sql += "受注番号 ILIKE '" & CymnNo & "'"
        Sql += " AND "
        Sql += "受注番号枝番 ILIKE '" & Suffix & "'"
        Sql += " AND "
        Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        Dim dsSkyuhd As DataSet = getDsData("t23_skyuhd", Sql)

        BillingAmount = IIf(
            dsSkyuhd.Tables(RS).Compute("SUM(請求金額計)", Nothing) IsNot DBNull.Value,
            dsSkyuhd.Tables(RS).Compute("SUM(請求金額計)", Nothing),
            0
        )

        Dim BillTotal As Decimal = DgvAdd.Rows(0).Cells("今回請求金額計").Value + BillingAmount
        Dim Balance As Decimal = dsCymnhd.Tables(RS).Rows(0)("見積金額") - BillTotal

        If Balance < 0 Then
            '対象データがないメッセージを表示
            _msgHd.dspMSG("chkBalanceDataError", frmC01F10_Login.loginValue.Language)

            Return
        End If

        If DgvAdd.Rows(0).Cells("今回請求金額計").Value = 0 Then
            '対象データがないメッセージを表示
            _msgHd.dspMSG("NonData", frmC01F10_Login.loginValue.Language)

            Return
        End If

        '採番データを取得・更新
        Dim DM As String = getSaiban("80", dtToday)

        Sql = "INSERT INTO "
        Sql += "Public."
        Sql += "t23_skyuhd("
        Sql += "会社コード, 請求番号, 請求区分, 請求日, 受注番号, 受注番号枝番, 客先番号, 得意先コード, 得意先名, 請求金額計, 売掛残高, 備考1, 備考2, 取消区分, 登録日, 更新者, 更新日)"
        Sql += " VALUES('"
        Sql += dsCymnhd.Tables(RS).Rows(0)("会社コード").ToString
        Sql += "', '"
        Sql += DM
        Sql += "', '"
        Sql += DgvAdd.Rows(0).Cells("請求区分").Value.ToString
        Sql += "', '"
        Sql += DtpBillingDate.Value
        Sql += "', '"
        Sql += dsCymnhd.Tables(RS).Rows(0)("受注番号").ToString
        Sql += "', '"
        Sql += dsCymnhd.Tables(RS).Rows(0)("受注番号枝番").ToString
        Sql += "', '"
        Sql += dsCymnhd.Tables(RS).Rows(0)("客先番号").ToString
        Sql += "', '"
        Sql += dsCymnhd.Tables(RS).Rows(0)("得意先コード").ToString
        Sql += "', '"
        Sql += dsCymnhd.Tables(RS).Rows(0)("得意先名").ToString
        Sql += "', '"
        Sql += DgvAdd.Rows(0).Cells("今回請求金額計").Value
        Sql += "', '"
        Sql += DgvAdd.Rows(0).Cells("今回請求金額計").Value
        Sql += "', '"
        Sql += DgvAdd.Rows(0).Cells("今回備考1").Value
        Sql += "', '"
        Sql += DgvAdd.Rows(0).Cells("今回備考2").Value
        Sql += "', '"
        Sql += "0"
        Sql += "', '"
        Sql += dtToday
        Sql += "', '"
        Sql += frmC01F10_Login.loginValue.TantoNM
        Sql += "', '"
        Sql += dtToday
        Sql += " ')"

        _db.executeDB(Sql)

        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub


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
        Sql += "'" & frmC01F10_Login.loginValue.BumonNM & "'"
        Sql += txtParam
        Return _db.selectDB(Sql, RS, reccnt)
    End Function

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
            Sql += "会社コード = '" & frmC01F10_Login.loginValue.BumonNM & "'"
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
            Sql += today
            Sql += "' "
            Sql += "WHERE"
            Sql += " 会社コード"
            Sql += "='"
            Sql += frmC01F10_Login.loginValue.BumonNM
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

End Class