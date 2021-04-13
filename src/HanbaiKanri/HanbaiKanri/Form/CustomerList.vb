Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Text.RegularExpressions

Public Class CustomerList
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
    Private _langHd As UtilLangHandler
    Private _db As UtilDBIf
    Private _parentForm As Form
    'Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ
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
                   ByRef prmRefForm As Form)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True
        _com = New CommonLogic(_db, _msgHd)

    End Sub

    'ロード時
    Private Sub CustomerList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DtpFrom.Value = DateAdd(DateInterval.Month, -3, Now)
        DtpTo.Value = Now

        CustomerListLoad()

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblCustomerName.Text = "CustomerName"
            BtnSearch.Text = "Search"
            BtnBillingCalculation.Text = "SelectInvoice"
            btnBack.Text = "Back"

            DgvCustomer.Columns("得意先コード").HeaderText = "CustomerCode"
            DgvCustomer.Columns("得意先名").HeaderText = "CustomerName"

            DgvCustomer.Columns("通貨_外貨").HeaderText = "Currency"
            DgvCustomer.Columns("受注金額計_外貨").HeaderText = "TotalJobOrderAmount" '& vbCrLf & "a"
            DgvCustomer.Columns("VAT_OUT計_外貨").HeaderText = "TotalVAT-OUT" '& vbCrLf & "b"
            DgvCustomer.Columns("請求金額計_外貨").HeaderText = "TotalBillingAmount" '& vbCrLf & "c"
            DgvCustomer.Columns("請求残高_外貨").HeaderText = "BillingBalance" '& vbCrLf & "d=a+b-c"

            DgvCustomer.Columns("通貨").HeaderText = "Currency"
            DgvCustomer.Columns("受注金額計").HeaderText = "TotalJobOrderAmount"
            DgvCustomer.Columns("請求金額計").HeaderText = "TotalBillingAmount"
            DgvCustomer.Columns("請求残高").HeaderText = "BillingBalance"

        Else  '日本語

            DgvCustomer.Columns("受注金額計_外貨").HeaderText = "受注金額計" '& vbCrLf & "a"
            DgvCustomer.Columns("VAT_OUT計_外貨").HeaderText = "VAT-OUT計" '& vbCrLf & "b"
            DgvCustomer.Columns("請求金額計_外貨").HeaderText = "請求済金額計" '& vbCrLf & "c"
            DgvCustomer.Columns("請求残高_外貨").HeaderText = "未請求金額計" '& vbCrLf & "d=a+b-c"

        End If

        '数字形式
        DgvCustomer.Columns("受注金額計_外貨").DefaultCellStyle.Format = "N2"
        DgvCustomer.Columns("VAT_OUT計_外貨").DefaultCellStyle.Format = "N2"
        DgvCustomer.Columns("請求金額計_外貨").DefaultCellStyle.Format = "N2"
        DgvCustomer.Columns("請求残高_外貨").DefaultCellStyle.Format = "N2"
        DgvCustomer.Columns("受注金額計").DefaultCellStyle.Format = "N2"
        DgvCustomer.Columns("請求金額計").DefaultCellStyle.Format = "N2"
        DgvCustomer.Columns("請求残高").DefaultCellStyle.Format = "N2"

        '中央寄せ
        DgvCustomer.Columns("得意先コード").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCustomer.Columns("得意先名").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCustomer.Columns("通貨_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCustomer.Columns("受注金額計_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCustomer.Columns("VAT_OUT計_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCustomer.Columns("請求金額計_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCustomer.Columns("請求残高_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        '右寄せ
        DgvCustomer.Columns("受注金額計_外貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCustomer.Columns("VAT_OUT計_外貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCustomer.Columns("請求金額計_外貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCustomer.Columns("請求残高_外貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCustomer.Columns("受注金額計").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCustomer.Columns("請求金額計").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCustomer.Columns("請求残高").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DgvCustomer.Columns("受注金額計").Visible = False
        DgvCustomer.Columns("請求金額計").Visible = False
        DgvCustomer.Columns("請求残高").Visible = False

    End Sub

    '一覧表示処理
    Private Sub CustomerListLoad()
        Dim Sql As String = ""
        Dim reccnt As Integer = 0
        Me.Cursor = Cursors.WaitCursor

        'Dim curds As DataSet  'm25_currency
        'Dim cur As String

        'リストクリア
        'DgvCustomer.Rows.Clear()

        'Sql += searchConditions()

        'Dim dsCustomer As DataSet = getDsData("m10_customer", Sql)

        Dim Count As Integer = 0
        'Dim CustomerCount As Integer '= dsCustomer.Tables(RS).Rows.Count
        Dim CustomerOrderCount As Integer
        Dim CustomerBillingCount As Integer

        Dim CustomerBillingAmountFC As Decimal
        'Dim CustomerOrderAmountFC As Decimal
        Dim VATFC As Decimal

        Dim CustomerBillingAmount As Decimal
        'Dim CustomerOrderAmount As Decimal

        'For i As Integer = 0 To dsCustomer.Tables(RS).Rows.Count - 1

        Sql = "SELECT 会社コード,得意先コード,得意先名,通貨,sum(請求金額計_外貨) as 請求金額計_外貨合計,sum(請求金額計) as 請求金額計_合計, sum(請求消費税計) as 請求消費税計 FROM"
        Sql += " Public.t23_skyuhd WHERE 取消区分=" & CommonConst.CANCEL_KBN_ENABLED
        Sql += " and 会社コード='" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += searchConditions()
        Sql += " Group by 会社コード,得意先コード,得意先名,通貨 order by 1,2,3,4"

        '得意先ごとの受注基本を取得
        Dim dsSkyuhd As DataSet = _db.selectDB(Sql, RS, reccnt)

        '受注件数を取得
        CustomerOrderCount = dsSkyuhd.Tables(RS).Rows.Count

        For j As Integer = 0 To dsSkyuhd.Tables(RS).Rows.Count - 1

            'If IsDBNull(dsCymnhd.Tables(RS).Rows(j)("見積金額_外貨合計")) Then
            'CustomerOrderAmountFC = 0
            'VATFC = 0
            'Else
            'CustomerOrderAmountFC = dsCymnhd.Tables(RS).Rows(j)("見積金額_外貨合計")
            'VATFC = dsCymnhd.Tables(RS).Rows(j)("見積金額_外貨合計") * dsCymnhd.Tables(RS).Rows(j)("ＶＡＴ") / 100
            'End If

            'If IsDBNull(dsCymnhd.Tables(RS).Rows(j)("見積金額_合計")) Then
            'CustomerOrderAmount = 0
            'Else
            'CustomerOrderAmount = dsCymnhd.Tables(RS).Rows(j)("見積金額_合計")
            'End If

            'Sql = "SELECT"
            'Sql += " "
            'Sql += " FROM "

            'Sql += "public.t23_skyuhd"
            'Sql += " WHERE "
            'Sql += "会社コード"
            'Sql += " ILIKE  "
            'Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"
            'Sql += " AND "
            'Sql += "得意先コード"
            'Sql += " = "
            'Sql += "'"
            'Sql += dsCustomer.Tables(RS).Rows(i)("得意先コード")
            'Sql += "'"
            'Sql += " AND "
            'Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

            'If IsDBNull(dsCymnhd.Tables(RS).Rows(j)("通貨")) Then
            'Sql += " AND 通貨 is null "
            'Else
            'Sql += " AND 通貨 = " & dsCymnhd.Tables(RS).Rows(j)("通貨")
            'End If

            '得意先ごとの請求基本を取得
            'Dim dsSkyuhd As DataSet = _db.selectDB(Sql, RS, reccnt)

            '請求件数を取得
            'CustomerBillingCount = dsSkyuhd.Tables(RS).Rows.Count.ToString

            If IsDBNull(dsSkyuhd.Tables(RS).Rows(j)("請求金額計_外貨合計")) Then
                CustomerBillingAmountFC = 0
            Else
                CustomerBillingAmountFC = dsSkyuhd.Tables(RS).Rows(j)("請求金額計_外貨合計")
            End If

            If IsDBNull(dsSkyuhd.Tables(RS).Rows(j)("請求金額計_合計")) Then
                CustomerBillingAmount = 0
            Else
                CustomerBillingAmount = dsSkyuhd.Tables(RS).Rows(j)("請求金額計_合計")
            End If

            If IsDBNull(dsSkyuhd.Tables(RS).Rows(j)("請求消費税計")) Then
                VATFC = 0
            Else
                VATFC = dsSkyuhd.Tables(RS).Rows(j)("請求消費税計")
            End If

            Dim idx = DgvCustomer.Rows.Count()

            'If IsDBNull(dsCymnhd.Tables(RS).Rows(j)("通貨")) Then
            'cur = vbNullString
            'Else
            'Sql = " and 採番キー = " & dsCymnhd.Tables(RS).Rows(j)("通貨")
            'curds = getDsData("m25_currency", Sql)

            'cur = curds.Tables(RS).Rows(0)("通貨コード")
            'End If

            DgvCustomer.Rows.Add()
            DgvCustomer.Rows(idx).Cells("得意先コード").Value = dsSkyuhd.Tables(RS).Rows(j)("得意先コード")
            DgvCustomer.Rows(idx).Cells("得意先名").Value = dsSkyuhd.Tables(RS).Rows(j)("得意先名")

            DgvCustomer.Rows(idx).Cells("通貨_外貨").Value = _com.getCurrencyEx(dsSkyuhd.Tables(RS).Rows(j)("通貨"))
            'DgvCustomer.Rows(idx).Cells("受注金額計_外貨").Value = CustomerOrderAmountFC
            DgvCustomer.Rows(idx).Cells("VAT_OUT計_外貨").Value = VATFC
            DgvCustomer.Rows(idx).Cells("請求金額計_外貨").Value = CustomerBillingAmountFC
            'DgvCustomer.Rows(idx).Cells("請求残高_外貨").Value = CustomerOrderAmountFC + VATFC - CustomerBillingAmountFC

            DgvCustomer.Rows(idx).Cells("通貨").Value = _com.setBaseCurrency()
            'DgvCustomer.Rows(idx).Cells("受注金額計").Value = CustomerOrderAmount
            DgvCustomer.Rows(idx).Cells("請求金額計").Value = CustomerBillingAmount
            'DgvCustomer.Rows(idx).Cells("請求残高").Value = CustomerOrderAmount - CustomerBillingAmount
            DgvCustomer.Rows(idx).Cells("受注件数").Value = CustomerOrderCount
            DgvCustomer.Rows(idx).Cells("請求件数").Value = CustomerBillingCount
            DgvCustomer.Rows(idx).Cells("会社コード").Value = dsSkyuhd.Tables(RS).Rows(j)("会社コード")

            DgvCustomer.Rows(idx).Cells("通貨_外貨コード").Value = dsSkyuhd.Tables(RS).Rows(j)("通貨")

        Next

        Me.Cursor = DefaultCursor

    End Sub

    '戻るボタン押下時
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    '請求計算ボタン押下時
    Private Sub BtnBillingCalculation_Click(sender As Object, e As EventArgs) Handles BtnBillingCalculation.Click
        If DgvCustomer.Rows.Count = 0 Then
            '対象データがないメッセージを表示
            _msgHd.dspMSG("NonData", frmC01F10_Login.loginValue.Language)

            Return
        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvCustomer.CurrentCell.RowIndex
        Dim Company As String = DgvCustomer.Rows(RowIdx).Cells("会社コード").Value
        Dim Customer As String = DgvCustomer.Rows(RowIdx).Cells("得意先コード").Value
        Dim CurCode As Integer = 0
        If IsDBNull(DgvCustomer.Rows(RowIdx).Cells("通貨_外貨コード").Value) Then
        Else
            CurCode = DgvCustomer.Rows(RowIdx).Cells("通貨_外貨コード").Value
        End If
        Dim openForm As Form = Nothing
        openForm = New CustomerOrderList(_msgHd, _db, _langHd, Me, Company, Customer, CurCode)   '処理選択

        Me.Enabled = False
        Me.Hide()
        openForm.Show(Me)
    End Sub

    '検索ボタン押下時
    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        DgvCustomer.Rows.Clear()
        CustomerListLoad()
    End Sub

    '抽出条件取得
    Private Function searchConditions() As String
        Dim Sql As String = ""

        '抽出条件
        Dim customerName As String = UtilClass.escapeSql(TxtSearch.Text)

        If customerName <> Nothing Then
            Sql += " AND "
            Sql += " 得意先名 ILIKE '%" & customerName & "%' "
        End If
        Sql += "AND 請求日>'" & UtilClass.formatDatetime(DtpFrom.Value) & "' AND 請求日<'" & UtilClass.formatDatetime(DtpTo.Value) & "' "
        Sql += " AND 得意先コード in (select 得意先コード from m10_customer where 会社コード='" & frmC01F10_Login.loginValue.BumonCD & "' and is_active=0) "
        Return Sql

    End Function

End Class