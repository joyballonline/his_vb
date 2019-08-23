Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Text.RegularExpressions

Public Class DepositList
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

    Private Sub getNukinList()

        Dim Sql As String = ""
        Dim reccnt As Integer = 0

        Dim curds As DataSet  'm25_currency
        Dim cur As String

        '一覧をクリア
        DgvCustomer.Rows.Clear()

        '検索条件を取得
        Sql = searchConditions()

        Dim dsCustomer As DataSet = getDsData("m10_customer", Sql)

        Dim Count As Integer = 0
        Dim CustomerCount As Integer = dsCustomer.Tables(RS).Rows.Count

        Dim CustomerBillingAmountFC As Decimal  '請求金額_外貨
        Dim CustomerOrderAmountFC As Decimal    '見積金額_外貨
        Dim AccountsReceivableFC As Decimal  '売掛残高_外貨
        Dim AmountDeposited As Decimal  '入金額計_外貨

        Dim CustomerBillingAmount As Decimal  '請求金額
        Dim CustomerOrderAmount As Decimal    '見積金額
        Dim AccountsReceivable As Decimal  '売掛残高

        'Language=ENGの時
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            LblConditions.Text = "ExtractionCondition"
            Label1.Text = "CustomerName"
            Label2.Text = "Address"
            Label3.Text = "PhoneNumber"
            Label4.Text = "CustomerCode"
            LblMode.Text = "MoneyReceiptInputMode"
            BtnSerach.Text = "Search"
            BtnDeposit.Text = "MoneyReceiptInput"
            btnBack.Text = "Back"
            LblBillingDate.Text = "BillingDate"
            ChkZeroData.Text = "Include AccountsReceivable 0"

            DgvCustomer.Columns("得意先コード").HeaderText = "CustomerCode"
            DgvCustomer.Columns("得意先名").HeaderText = "CustomerName"
            DgvCustomer.Columns("通貨_外貨").HeaderText = "Currency"
            'DgvCustomer.Columns("請求金額残_外貨").HeaderText = "BillingBalanceForeignCurrency"
            DgvCustomer.Columns("売掛金額_外貨").HeaderText = "AccountsReceivableAmount" & vbCrLf & "a"
            DgvCustomer.Columns("既入金額_外貨").HeaderText = "depositAmount" & vbCrLf & "b"
            DgvCustomer.Columns("売掛残高_外貨").HeaderText = "AccountsReceivableBalance" & vbCrLf & "c=a-b"

            'DgvCustomer.Columns("通貨").HeaderText = "Currency"
            'DgvCustomer.Columns("請求金額残").HeaderText = "BillingBalance"
            'DgvCustomer.Columns("売掛残高").HeaderText = "AccountsReceivableBalance"

        Else  '日本語

            DgvCustomer.Columns("売掛金額_外貨").HeaderText = "売掛金額" & vbCrLf & "a"
            DgvCustomer.Columns("既入金額_外貨").HeaderText = "既入金額" & vbCrLf & "b"
            DgvCustomer.Columns("売掛残高_外貨").HeaderText = "売掛残高" & vbCrLf & "c=a-b"

        End If

        '中央寄せ
        DgvCustomer.Columns("得意先コード").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCustomer.Columns("得意先名").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCustomer.Columns("通貨_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCustomer.Columns("売掛金額_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCustomer.Columns("既入金額_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvCustomer.Columns("売掛残高_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        '右寄せ
        DgvCustomer.Columns("請求金額残_外貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCustomer.Columns("既入金額_外貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCustomer.Columns("売掛残高_外貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCustomer.Columns("請求金額残").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvCustomer.Columns("売掛残高").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        '数字形式
        DgvCustomer.Columns("請求金額残_外貨").DefaultCellStyle.Format = "N2"
        DgvCustomer.Columns("既入金額_外貨").DefaultCellStyle.Format = "N2"
        DgvCustomer.Columns("売掛残高_外貨").DefaultCellStyle.Format = "N2"
        DgvCustomer.Columns("請求金額残").DefaultCellStyle.Format = "N2"
        DgvCustomer.Columns("売掛残高").DefaultCellStyle.Format = "N2"

        '得意先の一覧を取得
        For i As Integer = 0 To dsCustomer.Tables(RS).Rows.Count - 1


            Sql = "SELECT "
            Sql += " SUM(見積金額_外貨) as 見積金額_外貨合計,SUM(見積金額) as 見積金額_合計,通貨"
            Sql += " FROM "

            Sql += "public.t10_cymnhd"
            Sql += " WHERE "
            Sql += "会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

            Sql += " AND 得意先コード ILIKE '%" & dsCustomer.Tables(RS).Rows(i)("得意先コード") & "%'"
            Sql += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
            Sql += " group by 通貨"

            '得意先ごとの受注基本を取得
            Dim dsCymnhd As DataSet = _db.selectDB(Sql, RS, reccnt)

            For j As Integer = 0 To dsCymnhd.Tables(RS).Rows.Count - 1

                '見積金額を集計
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
                Sql += " count(*) as 件数,sum(請求金額計_外貨) as 請求金額計_外貨合計, sum(請求金額計) as 請求金額計_合計"
                Sql += ",sum(売掛残高_外貨) as 売掛残高_外貨合計, sum(売掛残高) as 売掛残高_合計"
                Sql += ",sum(入金額計_外貨) as 入金額計_外貨合計"

                Sql += " FROM "

                Sql += "public.t23_skyuhd"
                Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " AND 得意先コード ILIKE '%" & dsCustomer.Tables(RS).Rows(i)("得意先コード") & "%'"
                Sql += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

                If IsDBNull(dsCymnhd.Tables(RS).Rows(j)("通貨")) Then
                    Sql += " AND 通貨 is null "
                Else
                    Sql += " AND 通貨 = " & dsCymnhd.Tables(RS).Rows(j)("通貨")
                End If


                'InvoiceDate
                If TxtBillingDateSince.Text <> "" Then
                    Sql += " And 請求日 >= '" & UtilClass.strFormatDate(RevoveChars(TxtBillingDateSince.Text)) & "'"
                End If
                If TxtBillingDateUntil.Text <> "" Then
                    Sql += " and 請求日 <= '" & UtilClass.strFormatDate(RevoveChars(TxtBillingDateUntil.Text)) & "'"
                End If

                '売掛残０を含める場合　チェック = true
                If ChkZeroData.Checked = True Then
                Else
                    Sql += " having  sum(売掛残高)<> 0"
                End If


                '得意先ごとの請求基本を取得
                Dim dsSkyuhd As DataSet = _db.selectDB(Sql, RS, reccnt)


                '件数を判定
                If dsSkyuhd.Tables(RS).Rows.Count > 0 Then  '件数あり


                    '請求金額を集計
                    If IsDBNull(dsSkyuhd.Tables(RS).Rows(0)("請求金額計_外貨合計")) Then
                        CustomerBillingAmountFC = 0
                    Else
                        CustomerBillingAmountFC = dsSkyuhd.Tables(RS).Rows(0)("請求金額計_外貨合計")
                    End If

                    'If IsDBNull(dsSkyuhd.Tables(RS).Rows(0)("請求金額計_合計")) Then
                    '    CustomerBillingAmount = 0
                    'Else
                    '    CustomerBillingAmount = dsSkyuhd.Tables(RS).Rows(0)("請求金額計_合計")
                    'End If

                    If IsDBNull(dsSkyuhd.Tables(RS).Rows(0)("売掛残高_外貨合計")) Then
                        AccountsReceivableFC = 0
                    Else
                        AccountsReceivableFC = dsSkyuhd.Tables(RS).Rows(0)("売掛残高_外貨合計")
                    End If

                    'If IsDBNull(dsSkyuhd.Tables(RS).Rows(0)("売掛残高_合計")) Then
                    '    AccountsReceivable = 0
                    'Else
                    '    AccountsReceivable = dsSkyuhd.Tables(RS).Rows(0)("売掛残高_合計")
                    'End If

                    If IsDBNull(dsSkyuhd.Tables(RS).Rows(0)("入金額計_外貨合計")) Then
                        AmountDeposited = 0
                    Else
                        AmountDeposited = dsSkyuhd.Tables(RS).Rows(0)("入金額計_外貨合計")
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
                    DgvCustomer.Rows(idx).Cells("得意先コード").Value = dsCustomer.Tables(RS).Rows(i)("得意先コード")
                    DgvCustomer.Rows(idx).Cells("得意先名").Value = dsCustomer.Tables(RS).Rows(i)("得意先名")

                    DgvCustomer.Rows(idx).Cells("通貨_外貨").Value = cur
                    DgvCustomer.Rows(idx).Cells("売掛金額_外貨").Value = CustomerBillingAmountFC
                    DgvCustomer.Rows(idx).Cells("既入金額_外貨").Value = AmountDeposited
                    DgvCustomer.Rows(idx).Cells("売掛残高_外貨").Value = AccountsReceivableFC
                    'DgvCustomer.Rows(idx).Cells("請求金額残_外貨").Value = CustomerOrderAmountFC - CustomerBillingAmountFC

                    'DgvCustomer.Rows(idx).Cells("通貨").Value = setBaseCurrency()
                    'DgvCustomer.Rows(idx).Cells("請求金額残").Value = CustomerOrderAmount - CustomerBillingAmount
                    'DgvCustomer.Rows(idx).Cells("売掛残高").Value = AccountsReceivable
                    DgvCustomer.Rows(idx).Cells("会社コード").Value = dsCustomer.Tables(RS).Rows(i)("会社コード")

                    DgvCustomer.Rows(idx).Cells("通貨_外貨コード").Value = dsCymnhd.Tables(RS).Rows(j)("通貨")

                End If
            Next
        Next
    End Sub

    '画面表示時
    Private Sub DepositList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'InvoiceDateの範囲指定を初期設定
        TxtBillingDateSince.Value = DateAdd("d", CommonConst.SINCE_DEFAULT_DAY, DateTime.Today)
        TxtBillingDateUntil.Value = DateTime.Today


        '一覧取得
        getNukinList()
    End Sub

    '戻るボタン押下時
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    '入金入力ボタン押下時
    Private Sub BtnDeposit_Click(sender As Object, e As EventArgs) Handles BtnDeposit.Click

        '対象データがない場合は取消操作不可能
        If DgvCustomer.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Return

        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvCustomer.CurrentCell.RowIndex
        Dim Company As String = DgvCustomer.Rows(RowIdx).Cells("会社コード").Value
        Dim Customer As String = DgvCustomer.Rows(RowIdx).Cells("得意先コード").Value
        Dim Name As String = DgvCustomer.Rows(RowIdx).Cells("得意先名").Value
        Dim CurCode As Integer = 0
        If IsDBNull(DgvCustomer.Rows(RowIdx).Cells("通貨_外貨コード").Value) Then
        Else
            CurCode = DgvCustomer.Rows(RowIdx).Cells("通貨_外貨コード").Value
        End If
        Dim openForm As Form = Nothing
        openForm = New DepositManagement(_msgHd, _db, _langHd, Me, Company, Customer, Name, CurCode)   '処理選択
        openForm.Show(Me)
        Me.Hide()
    End Sub

    '抽出条件取得
    Private Function searchConditions() As String
        Dim Sql As String = ""

        '抽出条件
        Dim customerName As String = escapeSql(TxtCustomerName.Text)
        Dim customerAddress As String = escapeSql(TxtAddress.Text)
        Dim customerTel As String = escapeSql(TxtTel.Text)
        Dim customerCode As String = escapeSql(TxtCustomerCode.Text)

        If customerName <> Nothing Then
            Sql += " AND "
            Sql += " 得意先名 ILIKE '%" & customerName & "%' "
        End If

        If customerAddress <> Nothing Then
            Sql += " AND "
            Sql += " (住所１ ILIKE '%" & customerAddress & "%' "
            Sql += " OR "
            Sql += " 住所２ ILIKE '%" & customerAddress & "%' "
            Sql += " OR "
            Sql += " 住所３ ILIKE '%" & customerAddress & "%' )"
        End If

        If customerTel <> Nothing Then
            Sql += " AND "
            Sql += " 電話番号検索用 ILIKE '%" & customerTel & "%' "
        End If

        If customerCode <> Nothing Then
            Sql += " AND "
            Sql += " 得意先コード ILIKE '%" & customerCode & "%' "
        End If

        Return Sql

    End Function

    'param1：String テーブル名
    'param2：String 詳細条件
    'Return: DataSet
    Private Function getDsData(ByVal tableName As String, Optional ByRef txtParam As String = "") As DataSet
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Sql += "SELECT * FROM "

        Sql += "public." & tableName
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += txtParam
        Return _db.selectDB(Sql, RS, reccnt)
    End Function

    '検索ボタン押下時
    Private Sub BtnSerach_Click(sender As Object, e As EventArgs) Handles BtnSerach.Click
        '一覧取得
        getNukinList()
    End Sub

    'sqlで実行する文字列からシングルクォーテーションを文字コードにする
    Private Function escapeSql(ByVal prmSql As String) As String
        Dim sql As String = prmSql

        sql = sql.Replace("'"c, "''") 'シングルクォーテーションを置換

        Return Regex.Escape(sql)
        Return sql
    End Function

    Private Sub DepositList_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        '一覧取得
        getNukinList()
    End Sub

    '基準通貨の通貨コードを取得する
    Private Function setBaseCurrency() As String
        Dim Sql As String
        '通貨表示：ベースの設定
        Sql = " AND 採番キー = " & CommonConst.CURRENCY_CD_IDR.ToString
        Sql += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString

        Dim ds As DataSet = getDsData("m25_currency", Sql)
        setBaseCurrency = ds.Tables(RS).Rows(0)("通貨コード")

    End Function

    ''' <summary>
    ''' 指定した文字列から指定した文字を全て削除する
    ''' </summary>
    ''' <param name="s">対象となる文字列。</param>
    ''' <returns>sに含まれている全てのcharacters文字が削除された文字列。</returns>
    Public Shared Function RevoveChars(s As String) As String
        Dim buf As New System.Text.StringBuilder(s)
        '削除する文字の配列
        Dim removeChars As Char() = New Char() {vbCr, vbLf, Chr(39)}

        For Each c As Char In removeChars
            buf.Replace(c.ToString(), "")
        Next
        Return buf.ToString()
    End Function

End Class