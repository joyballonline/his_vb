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
    Private Suffix2 As String = ""  '売上行番号
    Private _parentForm As Form
    Private _status As String = ""
    Private _com As CommonLogic
    Private _hash As Dictionary(Of String, String)

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
                   ByRef prmRefSuffix2 As String,
                   Optional ByRef prmRefStatus As String = "",
                   Optional ByRef prmHash As Dictionary(Of String, String) = Nothing)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
        CymnNo = prmRefNo
        Suffix = prmRefSuffix
        Suffix2 = prmRefSuffix2
        _status = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        _com = New CommonLogic(_db, _msgHd)
        _hash = prmHash
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
            LblCymndt.Text = "SalesDetails"
            LblHistory.Text = "BillingHistoryData"
            LblAdd.Text = "InvoicingThisTime"
            LblIDRCurrency.Text = "Currency"  '通貨ラベル

            TxtDgvCymndtCount.Location = New Point(1228, 118)
            TxtDgvHistoryCount.Location = New Point(1228, 253)
            TxtDgvAddCount.Location = New Point(1228, 383)

            BtnRegist.Text = "Registration"
            BtnBack.Text = "Back"

            DgvCymn.Columns("受注番号").HeaderText = "JobOrderNo"
            DgvCymn.Columns("受注番号枝番").HeaderText = "JobOrderVer"
            DgvCymn.Columns("受注日").HeaderText = "OrderDate"
            DgvCymn.Columns("得意先").HeaderText = "CustomerName"
            DgvCymn.Columns("得意先コード").HeaderText = "CustomerCode"
            DgvCymn.Columns("客先番号").HeaderText = "CustomerNumber"
            DgvCymn.Columns("受注金額").HeaderText = "JobOrderAmount"
            DgvCymn.Columns("売上番号").HeaderText = "SalesNumber"
            DgvCymn.Columns("売上番号枝番").HeaderText = "SalesVer"
            DgvCymn.Columns("行番号").HeaderText = "LineNo"
            DgvCymn.Columns("売上金額ヘッダ").HeaderText = "SalesAmount"
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
            DgvCymndt.Columns("売上番号2").HeaderText = "SalesNumber"
            DgvCymndt.Columns("SalesVer2").HeaderText = "SalesVer"
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
            DgvAdd.Columns("SALESAMT").HeaderText = "TotalSalesAmount"
            DgvAdd.Columns("VATAMT").HeaderText = "TotVAT-OUT"

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
        Dim iCnt As Integer = 0
        Dim iCymndtRowcount As Integer = 0
        Dim iHistoryRowcount As Integer = 0

        For Each yx As KeyValuePair(Of String, String) In _hash

            CymnNo = yx.Key
            Suffix = yx.Value

            Sql = "SELECT"

            Sql += " t30.受注番号,t30.受注番号枝番"
            Sql += ",t30.得意先名,t30.得意先コード"
            Sql += ",t30.客先番号"
            Sql += ",t30.通貨"
            Sql += ",t30.ＶＡＴ"
            Sql += ",t30.売上金額_外貨 as 売上金額ヘッダ_外貨"    '売上合計金額  20200316
            Sql += ",t31.*, t10.受注日"

            Sql += " FROM t30_urighd t30, t10_cymnhd t10, t31_urigdt t31"
            Sql += " WHERE t30.受注番号=t10.受注番号 and t30.受注番号枝番=t10.受注番号枝番 and t30.会社コード=t10.会社コード"
            Sql += ""
            Sql += " AND t30.売上番号=t31.売上番号 and t30.売上番号枝番=t31.売上番号枝番 and t30.会社コード=t31.会社コード"

            Sql += " AND t30.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND t30.売上番号 = '" & CymnNo & "'"
            Sql += " AND t30.売上番号枝番 = '" & Suffix & "'"
            Sql += " AND t30.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

            Dim dsUrihd As DataTable = _db.selectDB(Sql, RS, reccnt).Tables(0)

            If dsUrihd.Rows.Count > 0 Then
                TxtIDRCurrency.Text = _com.getCurrencyEx(dsUrihd.Rows(0)("通貨"))
            Else
                TxtIDRCurrency.Text = ""
            End If

            '請求基本取得
            Dim sql2 As String = ""

            For ixi As Integer = 0 To dsUrihd.Rows.Count - 1
                If dsUrihd.Rows(ixi)("入金番号") Is DBNull.Value Then
                Else
                    sql2 += dsUrihd.Rows(ixi)("入金番号") & "','"
                End If
            Next

            Sql = "  AND 請求番号 in ('" & sql2 & "')"
            Sql += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

                Dim dsSkyuhd As DataTable = _com.getDsData("t23_skyuhd", Sql).Tables(0)

                Dim BillingAmount As Decimal = 0
                Dim billedvat As Decimal = 0
                Dim total As Decimal = 0
                Dim totvat As Decimal = 0

                '売上データから売上金額、請求金額
                DgvCymn.Rows.Add()
                DgvCymn.Rows(iCnt).Cells("受注番号").Value = dsUrihd.Rows(0)("受注番号")
                DgvCymn.Rows(iCnt).Cells("受注日").Value = dsUrihd.Rows(0)("受注日").ToShortDateString()
                DgvCymn.Rows(iCnt).Cells("得意先").Value = dsUrihd.Rows(0)("得意先名")
                DgvCymn.Rows(iCnt).Cells("客先番号").Value = dsUrihd.Rows(0)("客先番号").ToString
                DgvCymn.Rows(iCnt).Cells("売上番号").Value = dsUrihd.Rows(0)("売上番号").ToString          '売上情報の追加 2020.02.23
                DgvCymn.Rows(iCnt).Cells("売上番号枝番").Value = dsUrihd.Rows(0)("売上番号枝番").ToString
                DgvCymn.Rows(iCnt).Cells("行番号").Value = dsUrihd.Rows(0)("行番号").ToString
                DgvCymn.Rows(iCnt).Cells("得意先コード").Value = dsUrihd.Rows(0)("得意先コード").ToString
                DgvCymn.Rows(iCnt).Cells("受注番号枝番").Value = dsUrihd.Rows(0)("受注番号枝番").ToString

                '数字形式
                DgvCymn.Columns("受注金額").DefaultCellStyle.Format = "N2"
                DgvCymn.Columns("売上金額ヘッダ").DefaultCellStyle.Format = "N2"
                DgvCymn.Columns("請求金額計").DefaultCellStyle.Format = "N2"
                DgvCymn.Columns("請求残高").DefaultCellStyle.Format = "N2"



                '売上明細データから対象のデータの詳細を表示
                Dim j As Integer = DgvCymndt.RowCount
                For i As Integer = 0 To dsUrihd.Rows.Count - 1
                    DgvCymndt.Rows.Add()
                    DgvCymndt.Rows(j + i).Cells("売上番号2").Value = dsUrihd.Rows(i)("売上番号")
                    DgvCymndt.Rows(j + i).Cells("SalesVer2").Value = dsUrihd.Rows(i)("売上番号枝番")
                    DgvCymndt.Rows(j + i).Cells("明細").Value = dsUrihd.Rows(i)("行番号")
                    DgvCymndt.Rows(j + i).Cells("メーカー").Value = dsUrihd.Rows(i)("メーカー")
                    DgvCymndt.Rows(j + i).Cells("品名").Value = dsUrihd.Rows(i)("品名")
                    DgvCymndt.Rows(j + i).Cells("型式").Value = dsUrihd.Rows(i)("型式")
                    'DgvCymndt.Rows(i).Cells("受注個数").Value = dsUrihd.Rows(i)("受注数量")
                    DgvCymndt.Rows(j + i).Cells("単位").Value = dsUrihd.Rows(i)("単位")
                    DgvCymndt.Rows(j + i).Cells("売上数量").Value = dsUrihd.Rows(i)("売上数量")
                    DgvCymndt.Rows(j + i).Cells("売上単価").Value = dsUrihd.Rows(i)("売上単価_外貨")

                    DgvCymndt.Rows(j + i).Cells("売上金額").Value = UtilClass.rmNullDecimal(dsUrihd.Rows(i)("売上金額_外貨"))
                    If dsUrihd.Rows(i)("仕入区分") = CommonConst.Sire_KBN_DELIVERY Then
                        DgvCymndt.Rows(j + i).Cells("VAT").Value = 0
                    Else
                        DgvCymndt.Rows(j + i).Cells("VAT").Value = UtilClass.VAT_round_AP(dsUrihd.Rows(0)("ＶＡＴ"), dsUrihd.Rows(i)("売上金額_外貨"))
                    End If
                    'DgvCymndt.Rows(j + i).Cells("VAT").Value = UtilClass.rmNullDecimal(dsUrihd.Rows(0)("ＶＡＴ")) * UtilClass.rmNullDecimal(dsUrihd.Rows(i)("売上金額_外貨")) / 100 'UtilClass.VAT_round_AP(dsUrihd.Rows(0)("ＶＡＴ"), dsUrihd.Rows(i)("売上金額_外貨"))
                    DgvCymndt.Rows(j + i).Cells("売上金額計").Value = DgvCymndt.Rows(j + i).Cells("売上金額").Value + DgvCymndt.Rows(j + i).Cells("VAT").Value

                    total += DgvCymndt.Rows(j + i).Cells("売上金額").Value
                    totvat += DgvCymndt.Rows(j + i).Cells("VAT").Value

                    If Not (dsUrihd.Rows(i)("入金番号") Is DBNull.Value) Then  '請求残高が０の場合
                        'チェックボックス
                        DgvCymndt.Rows(j + i).Cells("請求").ReadOnly = True
                        DgvCymndt.Rows(j + i).Cells("請求").Value = False
                    Else
                        'チェックボックス
                        DgvCymndt.Rows(j + i).Cells("請求").ReadOnly = False
                        DgvCymndt.Rows(j + i).Cells("請求").Value = True
                    End If

                Next

                '数字形式
                DgvCymndt.Columns("受注個数").DefaultCellStyle.Format = "N2"
                DgvCymndt.Columns("売上数量").DefaultCellStyle.Format = "N2"
                DgvCymndt.Columns("売上単価").DefaultCellStyle.Format = "N2"
                DgvCymndt.Columns("売上金額").DefaultCellStyle.Format = "N2"
                DgvCymndt.Columns("VAT").DefaultCellStyle.Format = "N2"
                DgvCymndt.Columns("売上金額計").DefaultCellStyle.Format = "N2"


                '受注明細の件数カウント
                TxtDgvCymndtCount.Text = j + dsUrihd.Rows.Count

                j = DgvHistory.RowCount
                '請求データから請求済みデータ一覧を表示
                For i As Integer = 0 To dsSkyuhd.Rows.Count - 1
                    DgvHistory.Rows.Add()
                    DgvHistory.Rows(j + i).Cells("No").Value = i + 1
                    DgvHistory.Rows(j + i).Cells("請求番号").Value = dsSkyuhd.Rows(i)("請求番号")
                    DgvHistory.Rows(j + i).Cells("請求日").Value = dsSkyuhd.Rows(i)("請求日").ToShortDateString()
                    If frmC01F10_Login.loginValue.Language = "ENG" Then
                        DgvHistory.Rows(j + i).Cells("請求区分").Value = IIf(
                        dsSkyuhd.Rows(i)("請求区分") = CommonConst.BILLING_KBN_DEPOSIT,
                        CommonConst.BILLING_KBN_DEPOSIT_TXT_E,
                        CommonConst.BILLING_KBN_NORMAL_TXT_E
                    )
                    Else
                        DgvHistory.Rows(j + i).Cells("請求区分").Value = IIf(
                        dsSkyuhd.Rows(i)("請求区分") = CommonConst.BILLING_KBN_DEPOSIT,
                        CommonConst.BILLING_KBN_DEPOSIT_TXT,
                        CommonConst.BILLING_KBN_NORMAL_TXT
                    )
                    End If

                    DgvHistory.Rows(j + i).Cells("請求先").Value = dsSkyuhd.Rows(i)("得意先名")
                    DgvHistory.Rows(j + i).Cells("請求金額").Value = dsSkyuhd.Rows(i)("請求金額計_外貨")
                    DgvHistory.Rows(j + i).Cells("備考1").Value = dsSkyuhd.Rows(i)("備考1")
                    DgvHistory.Rows(j + i).Cells("備考2").Value = dsSkyuhd.Rows(i)("備考2")
                    DgvHistory.Rows(j + i).Cells("請求済み受注番号").Value = dsSkyuhd.Rows(i)("受注番号")
                    DgvHistory.Rows(j + i).Cells("請求済み受注番号枝番").Value = dsSkyuhd.Rows(i)("受注番号枝番")
                    DgvHistory.Rows(j + i).Cells("VAT2").Value = dsSkyuhd.Rows(i)("請求消費税計")
                    billedvat += DgvHistory.Rows(j + i).Cells("VAT2").Value
                    BillingAmount += DgvHistory.Rows(j + i).Cells("請求金額").Value

                Next

                '数字形式
                DgvHistory.Columns("請求金額").DefaultCellStyle.Format = "N2"
                DgvHistory.Columns("VAT2").DefaultCellStyle.Format = "N2"

            totvat = Math.Floor(totvat)
            DgvCymn.Rows(iCnt).Cells("受注金額").Value = total + totvat
            DgvCymn.Rows(iCnt).Cells("売上金額ヘッダ").Value = total + totvat
            DgvCymn.Rows(iCnt).Cells("請求金額計").Value = BillingAmount
            DgvCymn.Rows(iCnt).Cells("請求残高").Value = (total + totvat - BillingAmount)

            '請求済みデータ件数カウント
            TxtDgvHistoryCount.Text = j + dsSkyuhd.Rows.Count

            '請求データの残高が0じゃなかったら入力欄を表示
            Dim k As Integer = 0
                If DgvCymn.Rows(iCnt).Cells("請求残高").Value <> 0 Then
                    If DgvAdd.RowCount = 0 Then
                        DgvAdd.Rows.Add()
                    End If
                    DgvAdd.Rows(k).Cells("AddNo").Value = 1
                    DgvAdd(1, k).Value = 2
                    DgvAdd.Rows(k).Cells("今回請求先").Value = dsUrihd.Rows(0)("得意先名")

                    '今回請求金額に請求残高をセットする
                    DgvAdd.Rows(k).Cells("今回請求金額計").Value += (total + totvat - BillingAmount)
                    DgvAdd.Rows(k).Cells("VATAMT").Value += (totvat - billedvat)
                    DgvAdd.Rows(k).Cells("SALESAMT").Value += (total - (BillingAmount - billedvat))
                    DgvAdd.Rows(k).Cells("BILLEDAMT").Value += (BillingAmount)
                    DgvAdd.Rows(k).Cells("BILLEDVAT").Value += (billedvat)
                    DgvAdd.Rows(k).Cells("BILLEDSALES").Value += (BillingAmount - billedvat)

                End If

                '請求データ作成件数カウント
                TxtDgvAddCount.Text = DgvAdd.RowCount


                '数字形式
                DgvAdd.Columns("今回請求金額計").DefaultCellStyle.Format = "N2"
                DgvAdd.Columns("VATAMT").DefaultCellStyle.Format = "N2"
                DgvAdd.Columns("SALESAMT").DefaultCellStyle.Format = "N2"

                iCnt += 1

            Next

            If DgvAdd.RowCount > 0 Then
            calcSum()
        End If

        Dim sCC As String = ""
        For q2 As Integer = 0 To DgvCymn.RowCount - 1
            If q2 = 0 Then
                sCC = DgvCymn.Rows(q2).Cells("得意先コード").Value
            Else
                If sCC.Equals(DgvCymn.Rows(q2).Cells("得意先コード").Value) Then
                Else
                    _msgHd.dspMSG("chkNotSameCust", frmC01F10_Login.loginValue.Language)
                    Return
                End If
            End If
        Next
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
        Dim BalanceTot As Decimal = 0
        Dim Amount As Decimal = 0      '今回請求金額計
        Dim AmountFC As Decimal = 0  '今回請求金額計_外貨
        Dim AccountsReceivable As Decimal = 0    '売掛残高
        Dim AccountsReceivableFC As Decimal = 0 '売掛残高_外貨

        Dim Sql As String = ""

        'グリッドに何もないときは次画面へ移動しない
        If DgvAdd.RowCount = 0 Then
            '操作できるデータではないことをアラートする
            _msgHd.dspMSG("chkActionPropriety", frmC01F10_Login.loginValue.Language)
            Exit Sub
        End If

        For q As Integer = 0 To DgvCymn.RowCount - 1
            BalanceTot += Decimal.Parse(DgvCymn.Rows(q).Cells("請求残高").Value)
        Next

        '入金額計
        AmountFC = Decimal.Parse(DgvAdd.Rows(0).Cells("今回請求金額計").Value)
        AccountsReceivableFC = UtilClass.Round_2(BalanceTot)

        If AmountFC - AccountsReceivableFC > 9 Then
            '請求残高より請求金額が大きい場合はアラート
            _msgHd.dspMSG("chkBillingBalanceError", frmC01F10_Login.loginValue.Language)

            Return
        End If
        If AmountFC = 0 Then 'DgvAdd.Rows(0).Cells("今回請求金額計").Value = 0 Then
            '対象データがないメッセージを表示
            _msgHd.dspMSG("NonData", frmC01F10_Login.loginValue.Language)

            Return
        End If

        'レートの取得
        Dim strRate As Decimal = _com.setRate(_com.getCurkeyByISO(TxtIDRCurrency.Text), DtpBillingDate.Text) 'setRate(dsUrihd.Rows(0)("通貨").ToString())
        '入金額計
        If strRate = 1 Then
            Amount = AmountFC
        Else
            Amount = UtilClass.convertFC2IDR(AmountFC, strRate)  '画面の金額をIDRに変換　切り上げ
        End If
        '売掛残高
        '売掛残高の意味：今回登録した売掛金額から後工程の入金額を減額したもの
        'なので初期登録時は今回請求金額計と同一のものが入る
        'AccountsReceivableFC = DgvCymn.Rows(0).Cells("請求残高").Value - DgvAdd.Rows(0).Cells("今回請求金額計").Value
        If strRate = 1 Then
            AccountsReceivable = AccountsReceivableFC
        Else
            AccountsReceivable = UtilClass.convertFC2IDR(AccountsReceivableFC, strRate)  '画面の金額をIDRに変換　切り上げ
        End If

        '採番データを取得・更新
        Dim DM As String = _com.getSaiban("80", dtToday)

        Sql = "INSERT INTO "
        Sql += "Public."
        Sql += "t23_skyuhd("
        Sql += "会社コード, 請求番号, 請求区分, 請求日, 受注番号, 受注番号枝番, 客先番号, 得意先コード, 得意先名"
        Sql += ", 請求金額計, 入金額計, 売掛残高, 備考1, 備考2, 取消区分, 入金予定日, 登録日, 更新者, 更新日"
        Sql += ", 請求金額計_外貨, 入金額計_外貨, 売掛残高_外貨, 通貨, レート, 請求消費税計)"
        Sql += " VALUES("
        Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"   '会社コード
        Sql += ", '" & DM & "'"     '請求番号
        Sql += ", '" & DgvAdd.Rows(0).Cells("請求区分").Value.ToString & "'"    '請求区分
        Sql += ", '" & UtilClass.strFormatDate(DtpBillingDate.Value) & "'"      '請求日
        If DgvCymn.RowCount = 1 Then
            Sql += ", '" & DgvCymn.Rows(0).Cells("受注番号").Value.ToString & "'"      '受注番号
            Sql += ", '" & DgvCymn.Rows(0).Cells("受注番号枝番").Value.ToString & "'"  '受注番号枝番
        Else
            Sql += ", '" & CommonConst.COLINV & "', ''"
        End If
        Sql += ", '" & DgvCymn.Rows(0).Cells("客先番号").Value.ToString & "'"   '客先番号
        Sql += ", '" & DgvCymn.Rows(0).Cells("得意先コード").Value.ToString & "'"     '得意先コード
        Sql += ", '" & DgvCymn.Rows(0).Cells("得意先").Value.ToString & "'"   '得意先名
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
        Sql += ", " & _com.getCurkeyByISO(TxtIDRCurrency.Text).ToString & ""       '通貨
        Sql += ", " & UtilClass.formatNumberF10(strRate)        'レート
        Sql += ", " & UtilClass.formatNumber(DgvAdd.Rows(0).Cells("VATAMT").Value)

        Sql += ")"

        _db.executeDB(Sql)

        For k As Integer = 0 To DgvCymndt.Rows.Count - 1
            If DgvCymndt.Rows(k).Cells("請求").Value = True Then
                Sql = "update t31_urigdt set 入金番号 = '" & DM & "' where 売上番号 = '" & DgvCymndt.Rows(k).Cells("売上番号2").Value
                Sql += "' and 会社コード='" & frmC01F10_Login.loginValue.BumonCD & "' and 売上番号枝番 = '" & DgvCymndt.Rows(k).Cells("SalesVer2").Value
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
            DgvAdd.Rows(e.RowIndex).Cells("今回請求金額計").Value = UtilClass.rmNullDecimal(DgvAdd.Rows(e.RowIndex).Cells("VATAMT").Value) + UtilClass.rmNullDecimal(DgvAdd.Rows(e.RowIndex).Cells("SALESAMT").Value)

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

                Call calcSum()

            End If
        End If

    End Sub
    Private Sub calcSum()

        Dim decUriSum As Decimal = 0
        Dim dvat As Decimal = 0
        Dim du As Decimal = 0

        For i As Integer = 0 To DgvCymndt.Rows.Count - 1  '受注明細をループ

            'チェックがあるデータの売上金額を合計
            If DgvCymndt.Rows(i).Cells("請求").Value = True Then
                decUriSum += DgvCymndt.Rows(i).Cells("売上金額計").Value
                dvat += DgvCymndt.Rows(i).Cells("VAT").Value
                du += DgvCymndt.Rows(i).Cells("売上金額").Value
            Else
            End If
        Next

        DgvAdd.Rows(0).Cells("今回請求金額計").Value = Math.Floor(dvat) + du 'decUriSum  '今回請求額
        DgvAdd.Rows(0).Cells("VATAMT").Value = Math.Floor(dvat)
        DgvAdd.Rows(0).Cells("SALESAMT").Value = du
    End Sub

End Class