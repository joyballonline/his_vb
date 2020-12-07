Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Globalization

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
    Private _com As CommonLogic

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
        _com = New CommonLogic(_db, _msgHd)
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        DtpBillingDate.Value = Date.Now
        DtpDepositDate.Value = Date.Now
        _init = True

    End Sub

    Private Sub BillingManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'ComboBoxに表示する項目のリストを作成する
        Dim table As New DataTable("Table")
        table.Columns.Add("Display", GetType(String))
        table.Columns.Add("Value", GetType(Integer))
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            table.Rows.Add(CommonConst.BILLING_KBN_DEPOSIT_TXT_E, 1)              '前受金請求
            table.Rows.Add(CommonConst.BILLING_KBN_NORMAL_TXT_E, 2)                   '通常請求
        Else
            table.Rows.Add(CommonConst.BILLING_KBN_DEPOSIT_TXT, 1)              '前受金請求
            table.Rows.Add(CommonConst.BILLING_KBN_NORMAL_TXT, 2)                   '通常請求
        End If

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
            'LblBillingDate.Text = "BillingDate"
            LblDepositDate.Text = "DepositDate"
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
            LblIDRCurrency.Text = "Currency"  '通貨ラベル

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

            DgvCymndt.Columns("明細").HeaderText = "LineNo"
            DgvCymndt.Columns("メーカー").HeaderText = "Manufacturer"
            DgvCymndt.Columns("品名").HeaderText = "ItemName"
            DgvCymndt.Columns("型式").HeaderText = "Spec"
            DgvCymndt.Columns("受注個数").HeaderText = "JobOrderQuantity"
            DgvCymndt.Columns("単位").HeaderText = "Unit"
            DgvCymndt.Columns("売上数量").HeaderText = "SalesQuantity"
            DgvCymndt.Columns("売上単価").HeaderText = "SellingPrice"
            DgvCymndt.Columns("売上金額").HeaderText = "SalesAmount"
            DgvCymndt.Columns("VAT").HeaderText = "VAT-OUT"
            DgvCymndt.Columns("売上金額計").HeaderText = "SalesAmountSum"
            DgvCymndt.Columns("売上番号").HeaderText = "SalesNumber"
            DgvCymndt.Columns("売上番号枝番").HeaderText = "SalesVer"
            DgvCymndt.Columns("請求").HeaderText = "Billing"

            'DgvHistory.Columns("請求番号").HeaderText = "InvoiceNumber"
            DgvHistory.Columns("請求番号").HeaderText = "SalesInvoiceNo"
            'DgvHistory.Columns("請求日").HeaderText = "BillingDate"
            DgvHistory.Columns("請求日").HeaderText = "SalesInvoiceDate"
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
            DgvAdd.Columns("SALESAMT").HeaderText = "SalesAmount"

        End If
        If _status = CommonConst.STATUS_VIEW Then
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

        '今回請求の初期カーソル位置
        If DgvAdd.Rows.Count > 0 Then
            DgvAdd.CurrentCell = DgvAdd(3, 0)
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
        Dim dsCymnhd As DataSet = _com.getDsData("t10_cymnhd", Sql)

        'joinするのでとりあえず直書き
        Sql = "SELECT"
        Sql += " t11.行番号, t11.メーカー, t11.品名, t11.型式, t11.受注数量, t11.単位"
        Sql += " , t11.売上数量, t11.売単価, t11.売上金額, t11.見積単価_外貨, t11.見積金額_外貨 ,t10.ＶＡＴ ,t11.通貨"
        Sql += " FROM"
        Sql += " t11_cymndt t11, t10_cymnhd t10"
        Sql += " WHERE"
        Sql += " t11.会社コード = t10.会社コード"
        Sql += " AND "
        Sql += " t11.受注番号 = t10.受注番号"
        Sql += " AND "
        Sql += " t11.受注番号枝番 = t10.受注番号枝番"
        Sql += " AND "
        Sql += " t11.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND "
        Sql += " t11.受注番号 = '" & CymnNo & "'"
        Sql += " AND "
        Sql += " t11.受注番号枝番 = '" & Suffix & "'"
        Sql += " AND "
        Sql += "t10.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        '受注明細取得
        Dim dsCymndt As DataSet = _db.selectDB(Sql, RS, reccnt)

        TxtIDRCurrency.Text = _com.getCurrencyEx(dsCymndt.Tables(RS).Rows(0)("通貨"))

        Sql = " AND "
        Sql += "受注番号 = '" & CymnNo & "'"
        Sql += " AND "
        Sql += "受注番号枝番 = '" & Suffix & "'"
        Sql += " AND "
        Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        '請求基本取得
        Dim dsSkyuhd As DataSet = _com.getDsData("t23_skyuhd", Sql)
        Dim dsUri As DataSet = _com.getDsData("t30_urighd", Sql)

        Dim BillingAmount As Decimal = 0

        '請求金額計を集計
        BillingAmount = IIf(
            dsSkyuhd.Tables(RS).Compute("SUM(請求金額計_外貨)", Nothing) IsNot DBNull.Value,
            dsSkyuhd.Tables(RS).Compute("SUM(請求金額計_外貨)", Nothing),
            0
        )

        '受注額にVAT額を加算
        Dim total As Decimal = 0    '見積金額+VAT=受注金額
        total = UtilClass.rmNullDecimal(dsCymnhd.Tables(RS).Rows(0)("見積金額_外貨")) + (UtilClass.rmNullDecimal(dsCymnhd.Tables(RS).Rows(0)("見積金額_外貨")) * dsCymnhd.Tables(RS).Rows(0)("ＶＡＴ") / 100)

        '受注データ、見積データから対象の請求金額・請求残高を表示
        DgvCymn.Rows.Add()
        DgvCymn.Rows(0).Cells("受注番号").Value = dsCymnhd.Tables(RS).Rows(0)("受注番号")
        DgvCymn.Rows(0).Cells("受注日").Value = dsCymnhd.Tables(RS).Rows(0)("受注日").ToShortDateString()
        DgvCymn.Rows(0).Cells("得意先").Value = dsCymnhd.Tables(RS).Rows(0)("得意先名")
        DgvCymn.Rows(0).Cells("客先番号").Value = dsCymnhd.Tables(RS).Rows(0)("客先番号").ToString
        DgvCymn.Rows(0).Cells("受注金額").Value = total.ToString("N2")
        DgvCymn.Rows(0).Cells("請求金額計").Value = BillingAmount
        DgvCymn.Rows(0).Cells("請求残高").Value = (total - BillingAmount).ToString("N2")



        '数字形式
        DgvCymn.Columns("受注金額").DefaultCellStyle.Format = "N2"
        DgvCymn.Columns("請求金額計").DefaultCellStyle.Format = "N2"
        DgvCymn.Columns("請求残高").DefaultCellStyle.Format = "N2"

        '#633 のためコメントアウト
        'DtpBillingDate.MinDate = dsCymnhd.Tables(RS).Rows(0)("受注日").ToShortDateString()
        'DtpDepositDate.MinDate = dsCymnhd.Tables(RS).Rows(0)("受注日").ToShortDateString()


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
            DgvCymndt.Rows(i).Cells("売上単価").Value = dsCymndt.Tables(RS).Rows(i)("見積単価_外貨")

            DgvCymndt.Rows(i).Cells("売上金額").Value = dsCymndt.Tables(RS).Rows(i)("売上数量") * dsCymndt.Tables(RS).Rows(i)("見積単価_外貨") 'dsCymndt.Tables(RS).Rows(i)("見積金額_外貨")
            DgvCymndt.Rows(i).Cells("VAT").Value = UtilClass.VAT_round_AP(dsCymnhd.Tables(RS).Rows(0)("ＶＡＴ"), DgvCymndt.Rows(i).Cells("売上金額").Value)
            DgvCymndt.Rows(i).Cells("売上金額計").Value = DgvCymndt.Rows(i).Cells("売上金額").Value + DgvCymndt.Rows(i).Cells("VAT").Value

            If dsUri.Tables(RS).Rows.Count > 0 Then
                DgvCymndt.Rows(i).Cells("売上番号").Value = dsUri.Tables(RS).Rows(0)("売上番号")
                DgvCymndt.Rows(i).Cells("売上番号枝番").Value = dsUri.Tables(RS).Rows(0)("売上番号枝番")
            End If

            'チェックボックス
            DgvCymndt.Rows(i).Cells("請求").Value = False

        Next

        '数字形式
        DgvCymndt.Columns("受注個数").DefaultCellStyle.Format = "N2"
        DgvCymndt.Columns("売上数量").DefaultCellStyle.Format = "N2"
        DgvCymndt.Columns("売上単価").DefaultCellStyle.Format = "N2"
        DgvCymndt.Columns("売上金額").DefaultCellStyle.Format = "N2"
        DgvCymndt.Columns("VAT").DefaultCellStyle.Format = "N2"
        DgvCymndt.Columns("売上金額計").DefaultCellStyle.Format = "N2"


        '受注明細の件数カウント
        TxtDgvCymndtCount.Text = dsCymndt.Tables(RS).Rows.Count

        '請求データから請求済みデータ一覧を表示
        For i As Integer = 0 To dsSkyuhd.Tables(RS).Rows.Count - 1
            DgvHistory.Rows.Add()
            DgvHistory.Rows(i).Cells("No").Value = i + 1
            DgvHistory.Rows(i).Cells("請求番号").Value = dsSkyuhd.Tables(RS).Rows(i)("請求番号")
            DgvHistory.Rows(i).Cells("請求日").Value = dsSkyuhd.Tables(RS).Rows(i)("請求日").ToShortDateString()
            If frmC01F10_Login.loginValue.Language = "ENG" Then
                DgvHistory.Rows(i).Cells("請求区分").Value = IIf(
                dsSkyuhd.Tables(RS).Rows(i)("請求区分") = CommonConst.BILLING_KBN_DEPOSIT,
                CommonConst.BILLING_KBN_DEPOSIT_TXT_E,
                CommonConst.BILLING_KBN_NORMAL_TXT_E
            )
            Else
                DgvHistory.Rows(i).Cells("請求区分").Value = IIf(
                dsSkyuhd.Tables(RS).Rows(i)("請求区分") = CommonConst.BILLING_KBN_DEPOSIT,
                CommonConst.BILLING_KBN_DEPOSIT_TXT,
                CommonConst.BILLING_KBN_NORMAL_TXT
            )
            End If


            DgvHistory.Rows(i).Cells("請求先").Value = dsSkyuhd.Tables(RS).Rows(i)("得意先名")
            DgvHistory.Rows(i).Cells("請求金額").Value = dsSkyuhd.Tables(RS).Rows(i)("請求金額計_外貨")
            DgvHistory.Rows(i).Cells("備考1").Value = dsSkyuhd.Tables(RS).Rows(i)("備考1")
            DgvHistory.Rows(i).Cells("備考2").Value = dsSkyuhd.Tables(RS).Rows(i)("備考2")
            DgvHistory.Rows(i).Cells("請求済み受注番号").Value = dsSkyuhd.Tables(RS).Rows(i)("受注番号")
            DgvHistory.Rows(i).Cells("請求済み受注番号枝番").Value = dsSkyuhd.Tables(RS).Rows(i)("受注番号枝番")
        Next

        '数字形式
        DgvHistory.Columns("請求金額").DefaultCellStyle.Format = "N2"


        '請求済みデータ件数カウント
        TxtDgvHistoryCount.Text = dsSkyuhd.Tables(RS).Rows.Count

        '請求データの残高が0じゃなかったら入力欄を表示
        If DgvCymn.Rows(0).Cells("請求残高").Value <> 0 Then
            DgvAdd.Rows.Add()
            DgvAdd.Rows(0).Cells("AddNo").Value = 1
            DgvAdd(1, 0).Value = 2
            DgvAdd.Rows(0).Cells("今回請求先").Value = dsCymnhd.Tables(RS).Rows(0)("得意先名")

            '今回請求金額に請求残高をセットする
            DgvAdd.Rows(0).Cells("今回請求金額計").Value = (total - BillingAmount).ToString("N2")

            '請求データ作成件数カウント
            TxtDgvAddCount.Text = 1
        End If

        '数字形式
        DgvAdd.Columns("今回請求金額計").DefaultCellStyle.Format = "N2"
        DgvAdd.Columns("VATAMT").DefaultCellStyle.Format = "N2"
        DgvAdd.Columns("SALESAMT").DefaultCellStyle.Format = "N2"

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
        'Dim errflg As Boolean = True
        Dim dtToday As DateTime = DateTime.Now
        Dim strToday As String = UtilClass.strFormatDate(dtToday)
        Dim reccnt As Integer = 0
        Dim BillingAmount As Decimal = 0
        Dim Amount As Decimal = 0      '今回請求金額計
        Dim AmountFC As Decimal = 0  '今回請求金額計_外貨
        Dim AccountsReceivable As Decimal    '売掛残高
        Dim AccountsReceivableFC As Decimal  '売掛残高_外貨

        Dim Sql As String = ""

        'グリッドに何もないときは次画面へ移動しない
        If DgvAdd.RowCount = 0 Then
            '操作できるデータではないことをアラートする
            _msgHd.dspMSG("chkActionPropriety", frmC01F10_Login.loginValue.Language)
            Exit Sub
        End If

        If Decimal.Parse(DgvAdd.Rows(0).Cells("今回請求金額計").Value) > Decimal.Parse(DgvCymn.Rows(0).Cells("請求残高").Value) Then
            '請求残高より請求金額が大きい場合はアラート
            _msgHd.dspMSG("chkBillingBalanceError", frmC01F10_Login.loginValue.Language)

            Return
        End If


        Sql = " AND 受注番号 = '" & CymnNo & "'"
        Sql += " AND 受注番号枝番 = '" & Suffix & "'"
        Sql += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        Dim dsCymnhd As DataSet = _com.getDsData("t10_cymnhd", Sql)

        Sql = " AND 受注番号 = '" & CymnNo & "'"
        Sql += " AND 受注番号枝番 = '" & Suffix & "'"
        Sql += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        Dim dsSkyuhd As DataSet = _com.getDsData("t23_skyuhd", Sql)

        BillingAmount = IIf(
            dsSkyuhd.Tables(RS).Compute("SUM(請求金額計_外貨)", Nothing) IsNot DBNull.Value,
            dsSkyuhd.Tables(RS).Compute("SUM(請求金額計_外貨)", Nothing),
            0
        )
        '受注額にVAT額を加算
        Dim total As Decimal = 0    '見積金額+VAT=受注金額
        total = dsCymnhd.Tables(RS).Rows(0)("見積金額") + dsCymnhd.Tables(RS).Rows(0)("見積金額") * dsCymnhd.Tables(RS).Rows(0)("ＶＡＴ") / 100

        Dim BillTotal As Decimal = DgvAdd.Rows(0).Cells("今回請求金額計").Value + BillingAmount
        Dim Balance As Decimal = Math.Round(total, 2, MidpointRounding.AwayFromZero) - BillTotal

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

        'レートの取得
        Dim strRate As Decimal = setRate(dsCymnhd.Tables(RS).Rows(0)("通貨").ToString())

        '入金額計
        AmountFC = DgvAdd.Rows(0).Cells("今回請求金額計").Value
        Amount = Math.Ceiling(AmountFC / strRate)  '画面の金額をIDRに変換　切り上げ

        '売掛残高
        '売掛残高の意味：今回登録した売掛金額から後工程の入金額を減額したもの
        'なので初期登録時は今回請求金額計と同一のものが入る
        'AccountsReceivableFC = DgvCymn.Rows(0).Cells("請求残高").Value - DgvAdd.Rows(0).Cells("今回請求金額計").Value

        AccountsReceivableFC = DgvAdd.Rows(0).Cells("今回請求金額計").Value
        AccountsReceivable = Math.Ceiling(AccountsReceivableFC / strRate)  '画面の金額をIDRに変換　切り上げ


        '採番データを取得・更新
        Dim DM As String = _com.getSaiban("80", dtToday)

        Sql = "INSERT INTO "
        Sql += "Public."
        Sql += "t23_skyuhd("
        Sql += "会社コード, 請求番号, 請求区分, 請求日, 受注番号, 受注番号枝番, 客先番号, 得意先コード, 得意先名"
        Sql += ", 請求金額計, 入金額計, 売掛残高, 備考1, 備考2, 取消区分, 入金予定日, 登録日, 更新者, 更新日"
        Sql += ", 請求金額計_外貨, 入金額計_外貨, 売掛残高_外貨, 通貨, レート, 請求消費税計)"
        Sql += " VALUES("
        Sql += "'" & dsCymnhd.Tables(RS).Rows(0)("会社コード").ToString & "'"   '会社コード
        Sql += ", '" & DM & "'"     '請求番号
        Sql += ", '" & DgvAdd.Rows(0).Cells("請求区分").Value.ToString & "'"    '請求区分
        Sql += ", '" & UtilClass.strFormatDate(DtpBillingDate.Value) & "'"      '請求日
        Sql += ", '" & dsCymnhd.Tables(RS).Rows(0)("受注番号").ToString & "'"   '受注番号
        Sql += ", '" & dsCymnhd.Tables(RS).Rows(0)("受注番号枝番").ToString & "'"     '受注番号枝番
        Sql += ", '" & dsCymnhd.Tables(RS).Rows(0)("客先番号").ToString & "'"   '客先番号
        Sql += ", '" & dsCymnhd.Tables(RS).Rows(0)("得意先コード").ToString & "'"     '得意先コード
        Sql += ", '" & dsCymnhd.Tables(RS).Rows(0)("得意先名").ToString & "'"   '得意先名
        Sql += ", " & UtilClass.formatNumber(Amount)                              '請求金額計  
        Sql += ", 0"                                                            '入金額計を0で設定
        Sql += ", " & UtilClass.formatNumber(AccountsReceivable)                  '売掛残高
        Sql += ", '" & DgvAdd.Rows(0).Cells("今回備考1").Value & "'"            '備考1
        Sql += ", '" & DgvAdd.Rows(0).Cells("今回備考2").Value & "'"            '備考2
        Sql += ", 0"                                                            '取消区分
        Sql += ", '" & UtilClass.strFormatDate(DtpDepositDate.Value) & "'"      '入金予定日
        Sql += ", current_timestamp"                                            '登録日
        Sql += ", '" & frmC01F10_Login.loginValue.TantoNM & "'"                 '更新者
        Sql += ", current_timestamp"                                            '更新日

        Sql += ", " & UtilClass.formatNumber(AmountFC)    '請求金額計_外貨
        Sql += ", 0"                                    '入金額計_外貨を0で設定
        Sql += ", " & UtilClass.formatNumber(AccountsReceivableFC)  '売掛残高_外貨
        Sql += ", '" & dsCymnhd.Tables(RS).Rows(0)("通貨").ToString() & "'"       '通貨
        Sql += ", " & UtilClass.formatNumberF10(strRate)        'レート
        Sql += ", " & UtilClass.formatNumber(DgvAdd.Rows(0).Cells("VATAMT").Value)

        Sql += ")"

        _db.executeDB(Sql)

        For k As Integer = 0 To DgvCymndt.Rows.Count - 1
            If DgvCymndt.Rows(k).Cells("請求").Value = True Then
                Sql = "update t31_urigdt set 入金番号 = '" & DM & "' where 売上番号 = '" & DgvCymndt.Rows(k).Cells("売上番号").Value
                Sql += "' and 会社コード='" & frmC01F10_Login.loginValue.BumonCD & "' and 売上番号枝番 = '" & DgvCymndt.Rows(k).Cells("売上番号枝番").Value
                Sql += "' and 行番号 = '" & DgvCymndt.Rows(k).Cells("明細").Value & "'"
                _db.executeDB(Sql)
            End If
        Next

        '登録完了メッセージ
        _msgHd.dspMSG("completeInsert", frmC01F10_Login.loginValue.Language)

        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    '通貨の採番キーからレートを取得・設定
    '基準日が請求日「以前」の最新のもの
    Private Function setRate(ByVal strKey As Integer) As Decimal
        Dim Sql As String

        Sql = " AND 採番キー = " & strKey & ""
        Sql += " AND 基準日 < '" & UtilClass.strFormatDate(DtpBillingDate.Text) & "'"  '請求日
        Sql += " ORDER BY 基準日 DESC "

        Dim ds As DataSet = _com.getDsData("t71_exchangerate", Sql)

        If ds.Tables(RS).Rows.Count > 0 Then
            setRate = ds.Tables(RS).Rows(0)("レート")
        Else
            'If CultureInfo.CurrentCulture.Name.ToString = CommonConst.CI_ID Then
            '    setRate = CommonConst.BASE_RATE_IDR
            'Else
            '    setRate = CommonConst.BASE_RATE_JPY
            'End If
            setRate = 1.ToString("F10")
        End If

    End Function

    'DGV内で指定列名(プルダウン)は一度のクリックで開く
    'それ以外は一回で入力状態にする
    Private Sub DgvAdd_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DgvAdd.CellEnter
        If DgvAdd.Columns(e.ColumnIndex).Name = "請求区分" Then
            SendKeys.Send("{F4}")
            'Else
            '    SendKeys.Send("{F2}")
        End If
    End Sub

    Private Sub DgvAdd_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DgvAdd.CellValueChanged

        'ヘッダー以外だったら
        If e.RowIndex > -1 Then

            '操作したカラム名を取得
            Dim currentColumn As String = DgvAdd.Columns(e.ColumnIndex).Name

            If currentColumn = "今回請求金額計" Then  'Cellが今回請求金額計の場合
                '各項目の属性チェック
                If Not IsNumeric(DgvAdd.Rows(e.RowIndex).Cells("今回請求金額計").Value) And (DgvAdd.Rows(e.RowIndex).Cells("今回請求金額計").Value IsNot Nothing) Then
                    _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
                    DgvAdd.Rows(e.RowIndex).Cells("今回請求金額計").Value = 0
                    Exit Sub
                End If
                'Dim decTmp As Decimal = DgvAdd.Rows(e.RowIndex).Cells("今回請求金額計").Value
                'DgvAdd.Rows(e.RowIndex).Cells("今回請求金額計").Value = decTmp.ToString("N2")
            End If
            DgvAdd.Rows(e.RowIndex).Cells("今回請求金額計").Value = DgvAdd.Rows(e.RowIndex).Cells("VATAMT").Value + DgvAdd.Rows(e.RowIndex).Cells("SALESAMT").Value

        End If
    End Sub


    'CurrentCellDirtyStateChangedイベントハンドラ
    Private Sub DataGridView1_CurrentCellDirtyStateChanged(
        ByVal sender As Object, ByVal e As EventArgs) _
        Handles DgvCymndt.CurrentCellDirtyStateChanged

        If DgvCymndt.CurrentCellAddress.X = 11 AndAlso  '請求フラグが更新であれば「DgvCymndt_CellValueChanged」へ
        DgvCymndt.IsCurrentCellDirty Then
            'コミットする
            DgvCymndt.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub


    Private Sub DgvCymndt_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DgvCymndt.CellValueChanged

        'ヘッダー以外だったら
        If e.RowIndex > -1 Then

            '請求データがない場合は終了
            If DgvAdd.Rows.Count = 0 Then
                Exit Sub
            End If


            '操作したカラム名を取得
            Dim currentColumn As String = DgvCymndt.Columns(e.ColumnIndex).Name

            If currentColumn = "請求" Then  '請求フラグの更新の場合

                Dim decUriSum As Decimal = 0
                Dim vatamt As Decimal = 0
                Dim decSales As Decimal = 0

                For i As Integer = 0 To DgvCymndt.Rows.Count - 1  '受注明細をループ

                    'チェックがあるデータの売上金額を合計
                    If DgvCymndt.Rows(i).Cells("請求").Value = True Then
                        decUriSum += DgvCymndt.Rows(i).Cells("売上金額計").Value
                        vatamt += DgvCymndt.Rows(i).Cells("VAT").Value
                        decSales += DgvCymndt.Rows(i).Cells("売上金額").Value
                    Else
                    End If
                Next

                DgvAdd.Rows(0).Cells("今回請求金額計").Value = vatamt + decSales 'decUriSum  '今回請求額
                DgvAdd.Rows(0).Cells("VATAMT").Value = vatamt
                DgvAdd.Rows(0).Cells("SALESAMT").Value = decSales


            End If
        End If

    End Sub

End Class