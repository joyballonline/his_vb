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

    End Sub

    'ロード時
    Private Sub CustomerList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        CustomerListLoad()

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblCustomerName.Text = "CustomerName"
            BtnSearch.Text = "Search"
            BtnBillingCalculation.Text = "SelectInvoice"
            btnBack.Text = "Back"

            DgvCustomer.Columns("得意先名").HeaderText = "CustomerName"
            DgvCustomer.Columns("通貨_外貨").HeaderText = "Currency"
            DgvCustomer.Columns("受注金額計_外貨").HeaderText = "TotalJobOrderAmountForeignCurrency"
            DgvCustomer.Columns("請求金額計_外貨").HeaderText = "TotalBillingAmountForeignCurrency"
            DgvCustomer.Columns("請求残高_外貨").HeaderText = "BillingBalanceForeignCurrency"
            DgvCustomer.Columns("通貨").HeaderText = "Currency"
            DgvCustomer.Columns("受注金額計").HeaderText = "TotalJobOrderAmount"
            DgvCustomer.Columns("請求金額計").HeaderText = "TotalBillingAmount"
            DgvCustomer.Columns("請求残高").HeaderText = "BillingBalance"
        End If

        '数字形式
        DgvCustomer.Columns("受注金額計_外貨").DefaultCellStyle.Format = "N2"
        DgvCustomer.Columns("請求金額計_外貨").DefaultCellStyle.Format = "N2"
        DgvCustomer.Columns("請求残高_外貨").DefaultCellStyle.Format = "N2"
        DgvCustomer.Columns("受注金額計").DefaultCellStyle.Format = "N2"
        DgvCustomer.Columns("請求金額計").DefaultCellStyle.Format = "N2"
        DgvCustomer.Columns("請求残高").DefaultCellStyle.Format = "N2"

        '右寄せ
        DgvCustomer.Columns("受注金額計_外貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCustomer.Columns("請求金額計_外貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCustomer.Columns("請求残高_外貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCustomer.Columns("受注金額計").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCustomer.Columns("請求金額計").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCustomer.Columns("請求残高").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    End Sub

    '一覧表示処理
    Private Sub CustomerListLoad()
        Dim Sql As String = ""
        Dim reccnt As Integer = 0

        Dim curds As DataSet  'm25_currency
        Dim cur As String

        'リストクリア
        DgvCustomer.Rows.Clear()

        Sql += searchConditions()

        Dim dsCustomer As DataSet = getDsData("m10_customer", Sql)

        Dim Count As Integer = 0
        Dim CustomerCount As Integer = dsCustomer.Tables(RS).Rows.Count
        Dim CustomerOrderCount As Integer
        Dim CustomerBillingCount As Integer

        Dim CustomerBillingAmountFC As Decimal
        Dim CustomerOrderAmountFC As Decimal

        Dim CustomerBillingAmount As Decimal
        Dim CustomerOrderAmount As Decimal

        For i As Integer = 0 To dsCustomer.Tables(RS).Rows.Count - 1

            Sql = "SELECT "
            Sql += " SUM(見積金額_外貨) as 見積金額_外貨合計,SUM(見積金額) as 見積金額_合計,通貨"
            Sql += " FROM "

            Sql += "public.t10_cymnhd"
            Sql += " WHERE "
            Sql += "会社コード"
            Sql += " ILIKE  "
            Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"

            Sql += " AND "
            Sql += "得意先コード"
            Sql += " = "
            Sql += "'"
            Sql += dsCustomer.Tables(RS).Rows(i)("得意先コード")
            Sql += "'"
            Sql += " AND "
            Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

            Sql += " group by 通貨"

            '得意先ごとの受注基本を取得
            Dim dsCymnhd As DataSet = _db.selectDB(Sql, RS, reccnt)

            '受注件数を取得
            CustomerOrderCount = dsCymnhd.Tables(RS).Rows.Count.ToString

            For j As Integer = 0 To dsCymnhd.Tables(RS).Rows.Count - 1  't10_cymnhd

                If IsDBNull(dsCymnhd.Tables(RS).Rows(j)("見積金額_外貨合計")) Then
                    CustomerOrderAmountFC = 0
                Else
                    CustomerOrderAmountFC = dsCymnhd.Tables(RS).Rows(j)("見積金額_外貨合計")
                End If

                If IsDBNull(dsCymnhd.Tables(RS).Rows(j)("見積金額_合計")) Then
                    CustomerOrderAmount = 0
                Else
                    CustomerOrderAmount = dsCymnhd.Tables(RS).Rows(j)("見積金額_合計")
                End If

                Sql = "SELECT"
                Sql += " sum(請求金額計_外貨) as 請求金額計_外貨合計, sum(請求金額計) as 請求金額計_合計"
                Sql += " FROM "

                Sql += "public.t23_skyuhd"
                Sql += " WHERE "
                Sql += "会社コード"
                Sql += " ILIKE  "
                Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " AND "
                Sql += "得意先コード"
                Sql += " = "
                Sql += "'"
                Sql += dsCustomer.Tables(RS).Rows(i)("得意先コード")
                Sql += "'"
                Sql += " AND "
                Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

                If IsDBNull(dsCymnhd.Tables(RS).Rows(j)("通貨")) Then
                    Sql += " AND 通貨 is null "
                Else
                    Sql += " AND 通貨 = " & dsCymnhd.Tables(RS).Rows(j)("通貨")
                End If

                '得意先ごとの請求基本を取得
                Dim dsSkyuhd As DataSet = _db.selectDB(Sql, RS, reccnt)

                '請求件数を取得
                CustomerBillingCount = dsSkyuhd.Tables(RS).Rows.Count.ToString

                If IsDBNull(dsSkyuhd.Tables(RS).Rows(0)("請求金額計_外貨合計")) Then
                    CustomerBillingAmountFC = 0
                Else
                    CustomerBillingAmountFC = dsSkyuhd.Tables(RS).Rows(0)("請求金額計_外貨合計")
                End If

                If IsDBNull(dsSkyuhd.Tables(RS).Rows(0)("請求金額計_合計")) Then
                    CustomerBillingAmount = 0
                Else
                    CustomerBillingAmount = dsSkyuhd.Tables(RS).Rows(0)("請求金額計_合計")
                End If


                Dim idx = DgvCustomer.Rows.Count()

                If IsDBNull(dsCymnhd.Tables(RS).Rows(j)("通貨")) Then
                    cur = vbNullString
                Else
                    Sql = " and 採番キー = " & dsCymnhd.Tables(RS).Rows(j)("通貨")
                    curds = getDsData("m25_currency", Sql)

                    cur = curds.Tables(RS).Rows(0)("通貨コード")
                End If

                DgvCustomer.Rows.Add()
                DgvCustomer.Rows(idx).Cells("得意先名").Value = dsCustomer.Tables(RS).Rows(i)("得意先名")
                DgvCustomer.Rows(idx).Cells("通貨_外貨").Value = cur
                DgvCustomer.Rows(idx).Cells("受注金額計_外貨").Value = CustomerOrderAmountFC
                DgvCustomer.Rows(idx).Cells("請求金額計_外貨").Value = CustomerBillingAmountFC
                DgvCustomer.Rows(idx).Cells("請求残高_外貨").Value = CustomerOrderAmountFC - CustomerBillingAmountFC
                DgvCustomer.Rows(idx).Cells("通貨").Value = setBaseCurrency()
                DgvCustomer.Rows(idx).Cells("受注金額計").Value = CustomerOrderAmount
                DgvCustomer.Rows(idx).Cells("請求金額計").Value = CustomerBillingAmount
                DgvCustomer.Rows(idx).Cells("請求残高").Value = CustomerOrderAmount - CustomerBillingAmount
                DgvCustomer.Rows(idx).Cells("受注件数").Value = CustomerOrderCount
                DgvCustomer.Rows(idx).Cells("請求件数").Value = CustomerBillingCount
                DgvCustomer.Rows(idx).Cells("得意先コード").Value = dsCustomer.Tables(RS).Rows(i)("得意先コード")
                DgvCustomer.Rows(idx).Cells("会社コード").Value = dsCustomer.Tables(RS).Rows(i)("会社コード")

                DgvCustomer.Rows(idx).Cells("通貨_外貨コード").Value = dsCymnhd.Tables(RS).Rows(j)("通貨")

            Next
        Next
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
        Dim customerName As String = escapeSql(TxtSearch.Text)

        If customerName <> Nothing Then
            Sql += " AND "
            Sql += " 得意先名 ILIKE '%" & customerName & "%' "
        End If

        Return Sql

    End Function

    'sqlで実行する文字列からシングルクォーテーションを文字コードにする
    Private Function escapeSql(ByVal prmSql As String) As String
        Dim sql As String = prmSql

        sql = sql.Replace("'"c, "''") 'シングルクォーテーションを置換

        Return Regex.Escape(sql)
        Return sql
    End Function

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
        Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += txtParam

        Return _db.selectDB(Sql, RS, reccnt)
    End Function

    '基準通貨の通貨コードを取得する
    Private Function setBaseCurrency() As String
        Dim Sql As String
        '通貨表示：ベースの設定
        Sql = " AND 採番キー = " & CommonConst.CURRENCY_CD_IDR.ToString
        Sql += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString

        Dim ds As DataSet = getDsData("m25_currency", Sql)
        'TxtIDRCurrency.Text = ds.Tables(RS).Rows(0)("通貨コード")
        setBaseCurrency = ds.Tables(RS).Rows(0)("通貨コード")

    End Function

End Class