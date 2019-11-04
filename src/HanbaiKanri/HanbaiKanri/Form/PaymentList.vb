Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Text.RegularExpressions

Public Class PaymentList
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
        DgvSupplier.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells
    End Sub

    '画面表示時
    Private Sub MstSuppliere_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'InvoiceDateの範囲指定を初期設定
        TxtInvoiceDateSince.Value = DateAdd("d", CommonConst.SINCE_DEFAULT_DAY, DateTime.Today)
        TxtInvoiceDateUntil.Value = DateTime.Today

        '一覧取得
        getSiharaiList()

    End Sub

    '抽出条件を含め、一覧取得
    Private Sub getSiharaiList()

        Dim Sql As String = ""
        Dim reccnt As Integer = 0
        Dim curds As DataSet  'm25_currency
        Dim cur As String


        Dim SupplierOrderAmount As Decimal    '仕入金額
        Dim SupplierOrderAmountFC As Decimal  '仕入金額_外貨

        Dim PaymentAmountFC As Decimal  '支払金額_外貨

        Dim AccountsReceivable As Decimal     '買掛残高
        Dim AccountsReceivableFC As Decimal   '買掛残高_外貨


        '一覧をクリア
        DgvSupplier.Rows.Clear()

        '検索条件を取得
        Sql = searchConditions()

        'Language=ENGの時
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            LblConditions.Text = "ExtractionCondition"
            Label1.Text = "SupplierName"
            Label2.Text = "Address"
            Label3.Text = "PhoneNumber"
            Label4.Text = "SupplierCode"
            LblMode.Text = "PaymentRegistrationMode"

            BtnPayment.Text = "PaymentInput"
            BtnSerach.Text = "Search"
            btnBack.Text = "Back"

            ChkZeroData.Text = "Include Payback 0"

            DgvSupplier.Columns("仕入先コード").HeaderText = "SupplierCode"
            DgvSupplier.Columns("仕入先名").HeaderText = "SupplierName"

            DgvSupplier.Columns("通貨_外貨").HeaderText = "Currency"
            DgvSupplier.Columns("仕入金額計_外貨").HeaderText = "AccountsPayableAmount" & vbCrLf & "a"  '買掛金額
            DgvSupplier.Columns("買掛金額計_外貨").HeaderText = "AlreadyPaid" & vbCrLf & "b"            '既支払額
            DgvSupplier.Columns("支払残高_外貨").HeaderText = "AccountsPayable" & vbCrLf & "c=a-b"　 　 '買掛残高

            DgvSupplier.Columns("通貨").HeaderText = "Currency"
            DgvSupplier.Columns("仕入金額計").HeaderText = "TotalPurchaseAmount"
            DgvSupplier.Columns("支払残高").HeaderText = "PaymentAmount"

        Else  '日本語

            DgvSupplier.Columns("仕入金額計_外貨").HeaderText = "買掛金額" & vbCrLf & "a"
            DgvSupplier.Columns("買掛金額計_外貨").HeaderText = "既支払額" & vbCrLf & "b"
            DgvSupplier.Columns("支払残高_外貨").HeaderText = "買掛残高" & vbCrLf & "c=a-b"

        End If

        '中央寄せ
        DgvSupplier.Columns("仕入先コード").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvSupplier.Columns("仕入先名").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvSupplier.Columns("通貨_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvSupplier.Columns("仕入金額計_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvSupplier.Columns("買掛金額計_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvSupplier.Columns("支払残高_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        '数字形式
        DgvSupplier.Columns("仕入金額計_外貨").DefaultCellStyle.Format = "N2"
        DgvSupplier.Columns("買掛金額計_外貨").DefaultCellStyle.Format = "N2"
        DgvSupplier.Columns("支払残高_外貨").DefaultCellStyle.Format = "N2"
        DgvSupplier.Columns("仕入金額計").DefaultCellStyle.Format = "N2"
        DgvSupplier.Columns("支払残高").DefaultCellStyle.Format = "N2"


        '仕入先リストの取得
        Dim dsSupplier As DataSet = getDsData("m11_supplier", Sql)


        '仕入先の一覧から、支払一覧を作成
        For i As Integer = 0 To dsSupplier.Tables(RS).Rows.Count - 1    'm11_supplier


            '仕入先と一致する発注基本を取得
            Sql = "SELECT "
            Sql += " SUM(仕入金額_外貨) as 仕入金額_外貨合計,SUM(仕入金額) as 仕入金額_合計,通貨"
            Sql += " FROM "

            Sql += "public.t20_hattyu"
            Sql += " WHERE "
            Sql += "会社コード"
            Sql += " ILIKE  "
            Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"

            Sql += " AND "
            Sql += "仕入先コード"
            Sql += " ILIKE "
            Sql += "'%"
            Sql += dsSupplier.Tables(RS).Rows(i)("仕入先コード")
            Sql += "%'"
            Sql += " AND "
            Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

            Sql += " group by 通貨"

            Dim dsHattyu As DataSet = _db.selectDB(Sql, RS, reccnt)


            '仕入先の一覧から、支払一覧を作成
            For j As Integer = 0 To dsHattyu.Tables(RS).Rows.Count - 1    't20_hattyu

                '仕入先と一致する買掛基本を取得
                Sql = "SELECT"
                Sql += " count(*) as 件数"
                Sql += ",sum(買掛金額計_外貨) as 買掛金額計_外貨合計,sum(買掛残高_外貨) as 買掛残高_外貨合計,sum(支払金額計_外貨) as 支払金額計_外貨合計"
                Sql += ",sum(買掛残高) as 買掛残高_合計"

                Sql += " FROM "

                Sql += "public.t46_kikehd"
                Sql += " WHERE "
                Sql += "会社コード"
                Sql += " ILIKE  "
                Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " AND "
                Sql += "仕入先コード"
                Sql += " ILIKE "
                Sql += "'%"
                Sql += dsSupplier.Tables(RS).Rows(i)("仕入先コード")
                Sql += "%'"
                Sql += " AND "
                Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

                If IsDBNull(dsHattyu.Tables(RS).Rows(j)("通貨")) Then
                    Sql += " AND 通貨 is null "
                Else
                    Sql += " AND 通貨 = " & dsHattyu.Tables(RS).Rows(j)("通貨")
                End If


                'InvoiceDate
                If TxtInvoiceDateSince.Text <> "" Then
                    Sql += " And "
                    Sql += " 買掛日"
                    Sql += " >=  "
                    Sql += "'"
                    Sql += UtilClass.strFormatDate(RevoveChars(TxtInvoiceDateSince.Text))
                    Sql += "'"
                End If
                If TxtInvoiceDateUntil.Text <> "" Then
                    Sql += " and "
                    Sql += " 買掛日"
                    Sql += " <=  "
                    Sql += "'"
                    Sql += UtilClass.strFormatDate(RevoveChars(TxtInvoiceDateUntil.Text))
                    Sql += "'"
                End If


                '支払残０を含める場合　チェック = true
                If ChkZeroData.Checked = True Then
                Else
                    Sql += " having  sum(買掛残高_外貨)<> 0"
                End If

                Dim dsKikehd As DataSet = _db.selectDB(Sql, RS, reccnt)

                '件数を判定
                If dsKikehd.Tables(RS).Rows.Count > 0 Then  '件数あり


                    'If IsDBNull(dsHattyu.Tables(RS).Rows(j)("仕入金額_外貨合計")) Then
                    '    SupplierOrderAmountFC = 0
                    'Else
                    '    SupplierOrderAmountFC = dsHattyu.Tables(RS).Rows(j)("仕入金額_外貨合計")
                    'End If
                    If IsDBNull(dsKikehd.Tables(RS).Rows(0)("買掛金額計_外貨合計")) Then
                        SupplierOrderAmountFC = 0
                    Else
                        SupplierOrderAmountFC = dsKikehd.Tables(RS).Rows(0)("買掛金額計_外貨合計")
                    End If

                    If IsDBNull(dsHattyu.Tables(RS).Rows(j)("仕入金額_合計")) Then
                        SupplierOrderAmount = 0
                    Else
                        SupplierOrderAmount = dsHattyu.Tables(RS).Rows(j)("仕入金額_合計")
                    End If


                    '支払金額を集計
                    If IsDBNull(dsKikehd.Tables(RS).Rows(0)("支払金額計_外貨合計")) Then
                        PaymentAmountFC = 0
                    Else
                        PaymentAmountFC = dsKikehd.Tables(RS).Rows(0)("支払金額計_外貨合計")
                    End If



                    '買掛残高を集計
                    If IsDBNull(dsKikehd.Tables(RS).Rows(0)("買掛残高_外貨合計")) Then
                        AccountsReceivableFC = 0
                    Else
                        AccountsReceivableFC = dsKikehd.Tables(RS).Rows(0)("買掛残高_外貨合計")
                    End If

                    If IsDBNull(dsKikehd.Tables(RS).Rows(0)("買掛残高_合計")) Then
                        AccountsReceivable = 0
                    Else
                        AccountsReceivable = dsKikehd.Tables(RS).Rows(0)("買掛残高_合計")
                    End If


                    Dim idx = DgvSupplier.Rows.Count()  '一覧の列数を取得


                    If IsDBNull(dsHattyu.Tables(RS).Rows(j)("通貨")) Then
                        cur = vbNullString
                    Else
                        Sql = " and 採番キー = " & dsHattyu.Tables(RS).Rows(j)("通貨")
                        curds = getDsData("m25_currency", Sql)

                        cur = curds.Tables(RS).Rows(0)("通貨コード")
                    End If


                    '表示エリアにデータを追加
                    DgvSupplier.Rows.Add()

                    DgvSupplier.Rows(idx).Cells("仕入先コード").Value = dsSupplier.Tables(RS).Rows(i)("仕入先コード")
                    DgvSupplier.Rows(idx).Cells("仕入先名").Value = dsSupplier.Tables(RS).Rows(i)("仕入先名")

                    DgvSupplier.Rows(idx).Cells("通貨_外貨").Value = cur
                    DgvSupplier.Rows(idx).Cells("仕入金額計_外貨").Value = SupplierOrderAmountFC  '買掛金額
                    DgvSupplier.Rows(idx).Cells("買掛金額計_外貨").Value = PaymentAmountFC        '既支払額
                    DgvSupplier.Rows(idx).Cells("支払残高_外貨").Value = AccountsReceivableFC     '買掛残高


                    DgvSupplier.Rows(idx).Cells("通貨").Value = setBaseCurrency()
                    DgvSupplier.Rows(idx).Cells("仕入金額計").Value = SupplierOrderAmount
                    DgvSupplier.Rows(idx).Cells("支払残高").Value = AccountsReceivable

                    DgvSupplier.Rows(idx).Cells("会社コード").Value = dsSupplier.Tables(RS).Rows(i)("会社コード")
                    DgvSupplier.Rows(idx).Cells("通貨_外貨コード").Value = dsHattyu.Tables(RS).Rows(j)("通貨")

                End If

            Next
        Next

    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    Private Sub BtnDeposit_Click(sender As Object, e As EventArgs) Handles BtnPayment.Click

        '対象データがない場合は取消操作不可能
        If DgvSupplier.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Return

        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvSupplier.CurrentCell.RowIndex
        Dim Company As String = DgvSupplier.Rows(RowIdx).Cells("会社コード").Value
        Dim Supplier As String = DgvSupplier.Rows(RowIdx).Cells("仕入先コード").Value
        Dim Name As String = DgvSupplier.Rows(RowIdx).Cells("仕入先名").Value
        Dim CurCode As Integer = 0
        If IsDBNull(DgvSupplier.Rows(RowIdx).Cells("通貨_外貨コード").Value) Then
        Else
            CurCode = DgvSupplier.Rows(RowIdx).Cells("通貨_外貨コード").Value
        End If
        Dim openForm As Form = Nothing
        openForm = New Payment(_msgHd, _db, _langHd, Me, Company, Supplier, Name, CurCode)   '処理選択
        openForm.Show(Me)
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
            Sql += " 仕入先名 ILIKE '%" & customerName & "%' "
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
            Sql += " 仕入先コード ILIKE '%" & customerCode & "%' "
        End If

        Return Sql

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

    '検索ボタン押下時
    Private Sub BtnSerach_Click(sender As Object, e As EventArgs) Handles BtnSerach.Click
        '一覧取得
        getSiharaiList()
    End Sub

    'sqlで実行する文字列からシングルクォーテーションを文字コードにする
    Private Function escapeSql(ByVal prmSql As String) As String
        Dim sql As String = prmSql

        sql = sql.Replace("'"c, "''") 'シングルクォーテーションを置換

        Return Regex.Escape(sql)
        Return sql
    End Function

    Private Sub PaymentList_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        '一覧取得
        getSiharaiList()
    End Sub

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