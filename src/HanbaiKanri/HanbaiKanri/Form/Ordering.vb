Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices


Public Class Ordering
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
    Private PurchaseNo As String = ""
    Private PurchaseSuffix As String = ""
    Private PurchaseStatus As String = ""
    Private OrderCount As String = ""

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
                   Optional ByRef prmRefNo As String = Nothing,
                   Optional ByRef prmRefSuffix As String = Nothing,
                   Optional ByRef prmRefStatus As String = Nothing)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        PurchaseNo = prmRefNo
        PurchaseSuffix = prmRefSuffix
        PurchaseStatus = prmRefStatus

        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示

        _init = True

    End Sub

    Private Sub Quote_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'DateTimePickerのフォーマットを指定
        DtpPurchaseRegistration.Text = DateAdd("m", 0, Now).ToString("yyyy/MM/dd")
        DtpPurchaseDate.Text = DateAdd("m", 0, Now).ToString("yyyy/MM/dd")

        'DgvItemList.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvItemList.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvItemList.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvItemList.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvItemList.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvItemList.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvItemList.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvItemList.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvItemList.Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvItemList.Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvItemList.Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        'セルの内容に合わせて、行の高さが自動的に調節されるようにする
        DgvItemList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        '"Column1"列のセルのテキストを折り返して表示する
        DgvItemList.Columns("型式").DefaultCellStyle.WrapMode = DataGridViewTriState.True

        'ComboBoxに表示する項目のリストを作成する
        Dim table As New DataTable("Table")
        table.Columns.Add("Display", GetType(String))
        table.Columns.Add("Value", GetType(Integer))
        table.Rows.Add("仕入", 1)
        table.Rows.Add("在庫", 2)
        table.Rows.Add("サービス", 9)

        'DataGridViewComboBoxColumnを作成
        Dim column As New DataGridViewComboBoxColumn()
        'DataGridViewComboBoxColumnのDataSourceを設定
        column.DataSource = table
        '実際の値が"Value"列、表示するテキストが"Display"列とする
        column.ValueMember = "Value"
        column.DisplayMember = "Display"
        column.HeaderText = "仕入区分"
        column.Name = "仕入区分"
        'column.ValueMember = 1
        'DataGridView1に追加する
        DgvItemList.Columns.Insert(1, column)

        Dim reccnt As Integer = 0
        Dim dtNow As DateTime = DateTime.Now


        '受注基本情報

        Dim Sql1 As String = ""
        Sql1 += "SELECT"
        Sql1 += " * "
        Sql1 += "FROM "
        Sql1 += "public"
        Sql1 += "."
        Sql1 += "t20_hattyu"
        Sql1 += " WHERE "
        Sql1 += "発注番号"
        Sql1 += " ILIKE "
        Sql1 += "'"
        Sql1 += PurchaseNo.ToString
        Sql1 += "'"
        Sql1 += " AND "
        Sql1 += "発注番号枝番"
        Sql1 += " ILIKE "
        Sql1 += "'"
        Sql1 += PurchaseSuffix.ToString
        Sql1 += "'"

        Dim ds1 = _db.selectDB(Sql1, RS, reccnt)

        CompanyCode = ds1.Tables(RS).Rows(0)("会社コード")

        If ds1.Tables(RS).Rows(0)("発注番号") IsNot DBNull.Value Then
            TxtOrderNo.Text = ds1.Tables(RS).Rows(0)("発注番号")
        End If
        If ds1.Tables(RS).Rows(0)("発注番号枝番") IsNot DBNull.Value Then
            TxtOrderSuffix.Text = ds1.Tables(RS).Rows(0)("発注番号枝番")
        End If
        If ds1.Tables(RS).Rows(0)("発注日") IsNot DBNull.Value Then
            DtpPurchaseDate.Value = ds1.Tables(RS).Rows(0)("発注日")
        End If
        If ds1.Tables(RS).Rows(0)("登録日") IsNot DBNull.Value Then
            DtpPurchaseRegistration.Value = ds1.Tables(RS).Rows(0)("登録日")
        End If
        If ds1.Tables(RS).Rows(0)("仕入先コード") IsNot DBNull.Value Then
            TxtCustomerCode.Text = ds1.Tables(RS).Rows(0)("仕入先コード")
        End If
        If ds1.Tables(RS).Rows(0)("仕入先名") IsNot DBNull.Value Then
            TxtCustomerName.Text = ds1.Tables(RS).Rows(0)("仕入先名")
        End If
        If ds1.Tables(RS).Rows(0)("仕入先担当者名") IsNot DBNull.Value Then
            TxtPerson.Text = ds1.Tables(RS).Rows(0)("仕入先担当者名")
        End If
        If ds1.Tables(RS).Rows(0)("仕入先担当者役職") IsNot DBNull.Value Then
            TxtPosition.Text = ds1.Tables(RS).Rows(0)("仕入先担当者役職")
        End If
        If ds1.Tables(RS).Rows(0)("仕入先郵便番号") IsNot DBNull.Value Then
            Dim PostalCode As String = ds1.Tables(RS).Rows(0)("仕入先郵便番号")
            TxtPostalCode1.Text = PostalCode.Substring(0, 3)
            TxtPostalCode2.Text = PostalCode.Substring(3, 4)
        End If
        If ds1.Tables(RS).Rows(0)("仕入先住所") IsNot DBNull.Value Then
            Dim Address As String = ds1.Tables(RS).Rows(0)("仕入先住所")
            Dim delimiter As String = " "
            Dim parts As String() = Split(Address, delimiter, -1, CompareMethod.Text)
            TxtAddress1.Text = parts(0).ToString
            TxtAddress2.Text = parts(1).ToString
            TxtAddress3.Text = parts(2).ToString
        End If
        If ds1.Tables(RS).Rows(0)("仕入先電話番号") IsNot DBNull.Value Then
            TxtTel.Text = ds1.Tables(RS).Rows(0)("仕入先電話番号")
        End If
        If ds1.Tables(RS).Rows(0)("仕入先ＦＡＸ") IsNot DBNull.Value Then
            TxtFax.Text = ds1.Tables(RS).Rows(0)("仕入先ＦＡＸ")
        End If
        If ds1.Tables(RS).Rows(0)("営業担当者") IsNot DBNull.Value Then
            TxtSales.Text = ds1.Tables(RS).Rows(0)("営業担当者")
        End If
        If ds1.Tables(RS).Rows(0)("入力担当者") IsNot DBNull.Value Then
            TxtInput.Text = ds1.Tables(RS).Rows(0)("入力担当者")
        End If
        If ds1.Tables(RS).Rows(0)("支払条件") IsNot DBNull.Value Then
            TxtPaymentTerms.Text = ds1.Tables(RS).Rows(0)("支払条件")
        End If
        If ds1.Tables(RS).Rows(0)("備考") IsNot DBNull.Value Then
            TxtPurchaseRemark.Text = ds1.Tables(RS).Rows(0)("備考")
        End If
        If ds1.Tables(RS).Rows(0)("仕入金額") IsNot DBNull.Value Then
            TxtOrderAmount.Text = ds1.Tables(RS).Rows(0)("仕入金額")
        End If
        If ds1.Tables(RS).Rows(0)("見積備考") IsNot DBNull.Value Then
            TxtQuoteRemarks.Text = ds1.Tables(RS).Rows(0)("見積備考")
        End If
        If ds1.Tables(RS).Rows(0)("粗利額") IsNot DBNull.Value Then
            TxtGrossProfit.Text = ds1.Tables(RS).Rows(0)("粗利額")
        End If

        ''見積明細情報
        Dim Sql3 As String = ""
        Sql3 += "SELECT"
        Sql3 += " * "
        Sql3 += "FROM "
        Sql3 += "public"
        Sql3 += "."
        Sql3 += "t21_hattyu"
        Sql3 += " WHERE "
        Sql3 += "発注番号"
        Sql3 += " ILIKE "
        Sql3 += "'"
        Sql3 += PurchaseNo.ToString
        Sql3 += "'"
        Sql3 += " AND "
        Sql3 += "発注番号枝番"
        Sql3 += " ILIKE "
        Sql3 += "'"
        Sql3 += PurchaseSuffix.ToString
        Sql3 += "'"
        Dim ds3 = _db.selectDB(Sql3, RS, reccnt)

        For index As Integer = 0 To ds3.Tables(RS).Rows.Count - 1
            DgvItemList.Rows.Add()
            Dim tmp As Integer = ds3.Tables(RS).Rows(index)("仕入区分")
            DgvItemList(1, index).Value = tmp
            DgvItemList.Rows(index).Cells(2).Value = ds3.Tables(RS).Rows(index)("メーカー")
            DgvItemList.Rows(index).Cells(3).Value = ds3.Tables(RS).Rows(index)("品名")
            DgvItemList.Rows(index).Cells(4).Value = ds3.Tables(RS).Rows(index)("型式")
            DgvItemList.Rows(index).Cells(5).Value = ds3.Tables(RS).Rows(index)("発注数量")
            DgvItemList.Rows(index).Cells(6).Value = ds3.Tables(RS).Rows(index)("単位")
            DgvItemList.Rows(index).Cells(7).Value = ds3.Tables(RS).Rows(index)("仕入先名")
            DgvItemList.Rows(index).Cells(8).Value = ds3.Tables(RS).Rows(index)("仕入単価")
            DgvItemList.Rows(index).Cells(9).Value = ds3.Tables(RS).Rows(index)("間接費")
            DgvItemList.Rows(index).Cells(10).Value = ds3.Tables(RS).Rows(index)("仕入金額")
            DgvItemList.Rows(index).Cells(11).Value = ds3.Tables(RS).Rows(index)("リードタイム")
            DgvItemList.Rows(index).Cells(12).Value = ds3.Tables(RS).Rows(index)("備考")
            DgvItemList.Rows(index).Cells(13).Value = ds3.Tables(RS).Rows(index)("入庫数")
            DgvItemList.Rows(index).Cells(14).Value = ds3.Tables(RS).Rows(index)("未入庫数")
        Next

        '行番号の振り直し
        Dim i As Integer = DgvItemList.Rows.Count()
        Dim No As Integer = 1
        For c As Integer = 0 To i - 1
            DgvItemList.Rows(c).Cells(0).Value = No
            No += 1
        Next c
        TxtItemCount.Text = DgvItemList.Rows.Count()

        If PurchaseStatus = "VIEW" Then
            DtpPurchaseDate.Enabled = False
            TxtPurchaseRemark.Enabled = False
            DgvItemList.ReadOnly = True
            BtnRegistration.Visible = False
            BtnPurchase.Visible = True
            BtnPurchase.Location = New Point(1004, 677)
        End If

    End Sub

    '前の画面に戻る
    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        'Dim QuoteList As QuoteList
        'QuoteList = New QuoteList(_msgHd, _db)
        'QuoteList.Show()
        Me.Close()
    End Sub

    Private Sub BtnRegistration_Click(sender As Object, e As EventArgs) Handles BtnRegistration.Click
        Dim reccnt As Integer = 0
        Dim dtNow As DateTime = DateTime.Now

        If PurchaseStatus = "CLONE" Then
            Dim SqlSaiban As String = ""
            SqlSaiban += "SELECT "
            SqlSaiban += "会社コード, "
            SqlSaiban += "採番キー, "
            SqlSaiban += "最新値, "
            SqlSaiban += "最小値, "
            SqlSaiban += "最大値, "
            SqlSaiban += "接頭文字, "
            SqlSaiban += "連番桁数 "
            SqlSaiban += "FROM "
            SqlSaiban += "public"
            SqlSaiban += "."
            SqlSaiban += "m80_saiban"
            SqlSaiban += " WHERE "
            SqlSaiban += "採番キー"
            SqlSaiban += " ILIKE "
            SqlSaiban += "'%30%'"

            Dim Saiban As DataSet = _db.selectDB(SqlSaiban, RS, reccnt)

            Dim PurchaseCount As String = Saiban.Tables(RS).Rows(0)(2)
            Dim NewPurchaseNo As String = Saiban.Tables(RS).Rows(0)(5)
            NewPurchaseNo += dtNow.ToString("MMdd")
            NewPurchaseNo += PurchaseCount.PadLeft(Saiban.Tables(RS).Rows(0)(6), "0")


            Dim Sql1 As String = ""
            Sql1 += "SELECT"
            Sql1 += " * "
            Sql1 += "FROM "
            Sql1 += "public"
            Sql1 += "."
            Sql1 += "t20_hattyu"
            Sql1 += " WHERE "
            Sql1 += "発注番号"
            Sql1 += " ILIKE "
            Sql1 += "'"
            Sql1 += PurchaseNo.ToString
            Sql1 += "'"
            Sql1 += " AND "
            Sql1 += "発注番号枝番"
            Sql1 += " ILIKE "
            Sql1 += "'"
            Sql1 += PurchaseSuffix.ToString
            Sql1 += "'"
            Dim ds1 = _db.selectDB(Sql1, RS, reccnt)

            Dim Sql2 As String = ""
            Sql2 += "SELECT"
            Sql2 += " * "
            Sql2 += "FROM "
            Sql2 += "public"
            Sql2 += "."
            Sql2 += "t21_hattyu"
            Sql2 += " WHERE "
            Sql2 += "発注番号"
            Sql2 += " ILIKE "
            Sql2 += "'"
            Sql2 += PurchaseNo.ToString
            Sql2 += "'"
            Sql2 += " AND "
            Sql2 += "発注番号枝番"
            Sql2 += " ILIKE "
            Sql2 += "'"
            Sql2 += PurchaseSuffix.ToString
            Sql2 += "'"
            Dim ds2 = _db.selectDB(Sql2, RS, reccnt)

            Dim Sql3 As String = ""
            Sql3 = ""
            Sql3 += "INSERT INTO "
            Sql3 += "Public."
            Sql3 += "t20_hattyu("
            Sql3 += "会社コード, 発注番号, 発注番号枝番, 受注番号, 受注番号枝番, 見積番号, 見積番号枝番, 得意先コード, 得意先名, 得意先郵便番号, 得意先住所, 得意先電話番号, 得意先ＦＡＸ, 得意先担当者役職, 得意先担当者名, 仕入先コード, 仕入先名, 仕入先郵便番号, 仕入先住所, 仕入先電話番号, 仕入先ＦＡＸ, 仕入先担当者役職, 仕入先担当者名, 見積日, 見積有効期限, 支払条件, 見積金額,仕入金額, 粗利額, 営業担当者, 入力担当者, 備考, 見積備考, ＶＡＴ, ＰＰＨ, 受注日, 発注日, 登録日, 更新日, 更新者, 取消区分)"
            Sql3 += " VALUES('"
            Sql3 += ds1.Tables(RS).Rows(0)("会社コード").ToString
            Sql3 += "', '"
            Sql3 += NewPurchaseNo
            Sql3 += "', '"
            Sql3 += "1"
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("受注番号").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("受注番号枝番").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("見積番号").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("見積番号枝番").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("得意先コード").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("得意先名").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("得意先郵便番号").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("得意先住所").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("得意先電話番号").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("得意先ＦＡＸ").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("得意先担当者役職").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("得意先担当者名").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("仕入先コード").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("仕入先名").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("仕入先郵便番号").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("仕入先住所").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("仕入先電話番号").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("仕入先ＦＡＸ").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("仕入先担当者役職").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("仕入先担当者名").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("見積日").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("見積有効期限").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("支払条件").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("見積金額").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("仕入金額").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("粗利額").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("営業担当者").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("入力担当者").ToString
            Sql3 += "', '"
            Sql3 += TxtPurchaseRemark.Text
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("備考").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("ＶＡＴ").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("ＰＰＨ").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("受注日").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("発注日").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("登録日").ToString
            Sql3 += "', '"
            Sql3 += dtNow
            Sql3 += "', '"
            Sql3 += frmC01F10_Login.loginValue.TantoNM
            Sql3 += "', '"
            Sql3 += "0"
            Sql3 += " ')"
            Sql3 += "RETURNING 会社コード"
            Sql3 += ", "
            Sql3 += "発注番号"
            Sql3 += ", "
            Sql3 += "発注番号枝番"
            Sql3 += ", "
            Sql3 += "受注番号"
            Sql3 += ", "
            Sql3 += "受注番号枝番"
            Sql3 += ", "
            Sql3 += "見積番号"
            Sql3 += ", "
            Sql3 += "見積番号枝番"
            Sql3 += ", "
            Sql3 += "得意先コード"
            Sql3 += ", "
            Sql3 += "得意先名"
            Sql3 += ", "
            Sql3 += "得意先郵便番号"
            Sql3 += ", "
            Sql3 += "得意先住所"
            Sql3 += ", "
            Sql3 += "得意先電話番号"
            Sql3 += ", "
            Sql3 += "得意先ＦＡＸ"
            Sql3 += ", "
            Sql3 += "得意先担当者役職"
            Sql3 += ", "
            Sql3 += "得意先担当者名"
            Sql3 += ", "
            Sql3 += "仕入先コード"
            Sql3 += ", "
            Sql3 += "仕入先名"
            Sql3 += ", "
            Sql3 += "仕入先郵便番号"
            Sql3 += ", "
            Sql3 += "仕入先住所"
            Sql3 += ", "
            Sql3 += "仕入先電話番号"
            Sql3 += ", "
            Sql3 += "仕入先ＦＡＸ"
            Sql3 += ", "
            Sql3 += "仕入先担当者役職"
            Sql3 += ", "
            Sql3 += "仕入先担当者名"
            Sql3 += ", "
            Sql3 += "見積日"
            Sql3 += ", "
            Sql3 += "見積有効期限"
            Sql3 += ", "
            Sql3 += "支払条件"
            Sql3 += ", "
            Sql3 += "見積金額"
            Sql3 += ", "
            Sql3 += "仕入金額"
            Sql3 += ", "
            Sql3 += "粗利額"
            Sql3 += ", "
            Sql3 += "営業担当者"
            Sql3 += ", "
            Sql3 += "入力担当者"
            Sql3 += ", "
            Sql3 += "備考"
            Sql3 += ", "
            Sql3 += "見積備考"
            Sql3 += ", "
            Sql3 += "ＶＡＴ"
            Sql3 += ", "
            Sql3 += "ＰＰＨ"
            Sql3 += ", "
            Sql3 += "受注日"
            Sql3 += ", "
            Sql3 += "発注日"
            Sql3 += ", "
            Sql3 += "登録日"
            Sql3 += ", "
            Sql3 += "更新日"
            Sql3 += ", "
            Sql3 += "更新者"

            _db.executeDB(Sql3)

            Dim Sql4 As String = ""
            For hattyuIdx As Integer = 0 To DgvItemList.Rows.Count - 1
                Sql4 = ""
                Sql4 += "INSERT INTO "
                Sql4 += "Public."
                Sql4 += "t21_hattyu("
                Sql4 += "会社コード, 発注番号, 発注番号枝番, 行番号, 仕入区分, メーカー, 品名, 型式, 単位, 仕入先名, 仕入値, 発注数量, 仕入数量, 発注残数, 間接費, 仕入単価, 仕入金額, リードタイム, 入庫数, 未入庫数, 備考, 更新者, 登録日)"
                Sql4 += " VALUES('"
                Sql4 += ds1.Tables(RS).Rows(0)("会社コード").ToString
                Sql4 += "', '"
                Sql4 += NewPurchaseNo
                Sql4 += "', '"
                Sql4 += "1"
                Sql4 += "', '"
                Sql4 += DgvItemList.Rows(hattyuIdx).Cells("No").Value.ToString
                Sql4 += "', '"
                Sql4 += DgvItemList.Rows(hattyuIdx).Cells("仕入区分").Value.ToString
                Sql4 += "', '"
                Sql4 += DgvItemList.Rows(hattyuIdx).Cells("メーカー").Value.ToString
                Sql4 += "', '"
                Sql4 += DgvItemList.Rows(hattyuIdx).Cells("品名").Value.ToString
                Sql4 += "', '"
                Sql4 += DgvItemList.Rows(hattyuIdx).Cells("型式").Value.ToString
                Sql4 += "', '"
                Sql4 += DgvItemList.Rows(hattyuIdx).Cells("単位").Value.ToString
                Sql4 += "', '"
                Sql4 += DgvItemList.Rows(hattyuIdx).Cells("仕入先").Value.ToString
                Sql4 += "', '"
                Sql4 += DgvItemList.Rows(hattyuIdx).Cells("仕入単価").Value.ToString
                Sql4 += "', '"
                Sql4 += DgvItemList.Rows(hattyuIdx).Cells("数量").Value.ToString
                Sql4 += "', '"
                Sql4 += "0"
                Sql4 += "', '"
                Sql4 += DgvItemList.Rows(hattyuIdx).Cells("数量").Value.ToString
                Sql4 += "', '"
                Sql4 += DgvItemList.Rows(hattyuIdx).Cells("間接費").Value.ToString
                Sql4 += "', '"
                Sql4 += DgvItemList.Rows(hattyuIdx).Cells("仕入単価").Value.ToString
                Sql4 += "', '"
                Sql4 += DgvItemList.Rows(hattyuIdx).Cells("仕入金額").Value.ToString
                Sql4 += "', '"
                Sql4 += DgvItemList.Rows(hattyuIdx).Cells("リードタイム").Value.ToString
                Sql4 += "', '"
                Sql4 += "0"
                Sql4 += "', '"
                Sql4 += DgvItemList.Rows(hattyuIdx).Cells("数量").Value.ToString
                Sql4 += "', '"
                Sql4 += DgvItemList.Rows(hattyuIdx).Cells("備考").Value.ToString
                Sql4 += "', '"
                Sql4 += frmC01F10_Login.loginValue.TantoNM
                Sql4 += "', '"
                Sql4 += dtNow
                Sql4 += " ')"
                Sql4 += "RETURNING 会社コード"
                Sql4 += ", "
                Sql4 += "発注番号"
                Sql4 += ", "
                Sql4 += "発注番号枝番"
                Sql4 += ", "
                Sql4 += "行番号"
                Sql4 += ", "
                Sql4 += "仕入区分"
                Sql4 += ", "
                Sql4 += "メーカー"
                Sql4 += ", "
                Sql4 += "品名"
                Sql4 += ", "
                Sql4 += "型式"
                Sql4 += ", "
                Sql4 += "単位"
                Sql4 += ", "
                Sql4 += "仕入先名"
                Sql4 += ", "
                Sql4 += "仕入値"
                Sql4 += ", "
                Sql4 += "発注数量"
                Sql4 += ", "
                Sql4 += "仕入数量"
                Sql4 += ", "
                Sql4 += "発注残数"
                Sql4 += ", "
                Sql4 += "間接費"
                Sql4 += ", "
                Sql4 += "仕入単価"
                Sql4 += ", "
                Sql4 += "仕入金額"
                Sql4 += ", "
                Sql4 += "リードタイム"
                Sql4 += ", "
                Sql4 += "入庫数"
                Sql4 += ", "
                Sql4 += "未入庫数"
                Sql4 += ", "
                Sql4 += "備考"
                Sql4 += ", "
                Sql4 += "更新者"
                Sql4 += ", "
                Sql4 += "登録日"

                _db.executeDB(Sql4)
            Next
            PurchaseCount += 1
            Dim Saiban4 As String = ""
            Saiban4 += "UPDATE "
            Saiban4 += "Public."
            Saiban4 += "m80_saiban "
            Saiban4 += "SET "
            Saiban4 += " 最新値"
            Saiban4 += " = '"
            Saiban4 += PurchaseCount.ToString
            Saiban4 += "', "
            Saiban4 += "更新者"
            Saiban4 += " = '"
            Saiban4 += "Admin"
            Saiban4 += "', "
            Saiban4 += "更新日"
            Saiban4 += " = '"
            Saiban4 += dtNow
            Saiban4 += "' "
            Saiban4 += "WHERE"
            Saiban4 += " 会社コード"
            Saiban4 += "='"
            Saiban4 += CompanyCode.ToString
            Saiban4 += "'"
            Saiban4 += " AND"
            Saiban4 += " 採番キー"
            Saiban4 += "='"
            Saiban4 += "30"
            Saiban4 += "' "
            Saiban4 += "RETURNING 会社コード"
            Saiban4 += ", "
            Saiban4 += "採番キー"
            Saiban4 += ", "
            Saiban4 += "最新値"
            Saiban4 += ", "
            Saiban4 += "最小値"
            Saiban4 += ", "
            Saiban4 += "最大値"
            Saiban4 += ", "
            Saiban4 += "接頭文字"
            Saiban4 += ", "
            Saiban4 += "連番桁数"
            Saiban4 += ", "
            Saiban4 += "更新者"
            Saiban4 += ", "
            Saiban4 += "更新日"
            _db.executeDB(Saiban4)
            '_parentForm.Enabled = True
            '_parentForm.Show()
            'Me.Dispose()
        Else
            Dim Sql1 As String = ""
            Sql1 = ""
            Sql1 += "UPDATE "
            Sql1 += "Public."
            Sql1 += "t20_hattyu "
            Sql1 += "SET "

            Sql1 += "備考"
            Sql1 += " = '"
            Sql1 += TxtPurchaseRemark.Text
            Sql1 += "', "
            Sql1 += "受注日"
            Sql1 += " = '"
            Sql1 += DtpPurchaseDate.Value
            Sql1 += "', "
            Sql1 += "更新日"
            Sql1 += " = '"
            Sql1 += dtNow
            Sql1 += "', "
            Sql1 += "更新者"
            Sql1 += " = '"
            Sql1 += "zenbi01"
            Sql1 += " ' "

            Sql1 += "WHERE"
            Sql1 += " 会社コード"
            Sql1 += "='"
            Sql1 += CompanyCode
            Sql1 += "'"
            Sql1 += " AND"
            Sql1 += " 発注番号"
            Sql1 += "='"
            Sql1 += PurchaseNo
            Sql1 += "' "
            Sql1 += " AND"
            Sql1 += " 発注番号枝番"
            Sql1 += "='"
            Sql1 += PurchaseSuffix
            Sql1 += "' "
            Sql1 += "RETURNING 会社コード"
            Sql1 += ", "
            Sql1 += "発注番号"
            Sql1 += ", "
            Sql1 += "発注番号枝番"
            Sql1 += ", "
            Sql1 += "受注番号"
            Sql1 += ", "
            Sql1 += "受注番号枝番"
            Sql1 += ", "
            Sql1 += "見積番号"
            Sql1 += ", "
            Sql1 += "見積番号枝番"
            Sql1 += ", "
            Sql1 += "仕入先コード"
            Sql1 += ", "
            Sql1 += "仕入先名"
            Sql1 += ", "
            Sql1 += "仕入先郵便番号"
            Sql1 += ", "
            Sql1 += "仕入先住所"
            Sql1 += ", "
            Sql1 += "仕入先電話番号"
            Sql1 += ", "
            Sql1 += "仕入先ＦＡＸ"
            Sql1 += ", "
            Sql1 += "仕入先担当者役職"
            Sql1 += ", "
            Sql1 += "仕入先担当者名"
            Sql1 += ", "
            Sql1 += "見積日"
            Sql1 += ", "
            Sql1 += "見積有効期限"
            Sql1 += ", "
            Sql1 += "インボイス日"
            Sql1 += ", "
            Sql1 += "検品完了日"
            Sql1 += ", "
            Sql1 += "支払条件"
            Sql1 += ", "
            Sql1 += "見積金額"
            Sql1 += ", "
            Sql1 += "仕入金額"
            Sql1 += ", "
            Sql1 += "粗利額"
            Sql1 += ", "
            Sql1 += "営業担当者"
            Sql1 += ", "
            Sql1 += "入力担当者"
            Sql1 += ", "
            Sql1 += "備考"
            Sql1 += ", "
            Sql1 += "取消日"
            Sql1 += ", "
            Sql1 += "取消区分"
            Sql1 += ", "
            Sql1 += "ＶＡＴ"
            Sql1 += ", "
            Sql1 += "ＰＰＨ"
            Sql1 += ", "
            Sql1 += "受注日"
            Sql1 += ", "
            Sql1 += "発注日"
            Sql1 += ", "
            Sql1 += "登録日"
            Sql1 += ", "
            Sql1 += "更新日"
            Sql1 += ", "
            Sql1 += "更新者"
            Sql1 += ", "
            Sql1 += "得意先コード"
            Sql1 += ", "
            Sql1 += "得意先名"
            Sql1 += ", "
            Sql1 += "得意先郵便番号"
            Sql1 += ", "
            Sql1 += "得意先住所"
            Sql1 += ", "
            Sql1 += "得意先電話番号"
            Sql1 += ", "
            Sql1 += "得意先ＦＡＸ"
            Sql1 += ", "
            Sql1 += "得意先担当者役職"
            Sql1 += ", "
            Sql1 += "得意先担当者名"

            _db.executeDB(Sql1)
        End If
        Me.Close()
    End Sub

    Private Sub BtnPurchase_Click(sender As Object, e As EventArgs) Handles BtnPurchase.Click
        Dim reccnt As Integer = 0
        Dim Sql1 As String = ""
        Sql1 += "SELECT"
        Sql1 += " * "
        Sql1 += "FROM "
        Sql1 += "public"
        Sql1 += "."
        Sql1 += "t20_hattyu"
        Sql1 += " WHERE "
        Sql1 += "発注番号"
        Sql1 += " ILIKE "
        Sql1 += "'"
        Sql1 += PurchaseNo.ToString
        Sql1 += "'"
        Sql1 += " AND "
        Sql1 += "発注番号枝番"
        Sql1 += " ILIKE "
        Sql1 += "'"
        Sql1 += PurchaseSuffix.ToString
        Sql1 += "'"
        Dim ds1 = _db.selectDB(Sql1, RS, reccnt)

        Dim Sql2 As String = ""
        Sql2 += "SELECT"
        Sql2 += " * "
        Sql2 += "FROM "
        Sql2 += "public"
        Sql2 += "."
        Sql2 += "t21_hattyu"
        Sql2 += " WHERE "
        Sql2 += "発注番号"
        Sql2 += " ILIKE "
        Sql2 += "'"
        Sql2 += PurchaseNo.ToString
        Sql2 += "'"
        Sql2 += " AND "
        Sql2 += "発注番号枝番"
        Sql2 += " ILIKE "
        Sql2 += "'"
        Sql2 += PurchaseSuffix.ToString
        Sql2 += "'"
        Dim ds2 = _db.selectDB(Sql2, RS, reccnt)


        '定義
        Dim app As Excel.Application = Nothing
        Dim book As Excel.Workbook = Nothing
        Dim sheet As Excel.Worksheet = Nothing



        Try
            '雛形パス
            Dim sHinaPath As String = ""
            sHinaPath = StartUp._iniVal.BaseXlsPath

            '雛形ファイル名
            Dim sHinaFile As String = ""
            sHinaFile = sHinaPath & "\" & "PurchaseOrder.xlsx"

            '出力先パス
            Dim sOutPath As String = ""
            sOutPath = StartUp._iniVal.OutXlsPath

            '出力ファイル名
            Dim sOutFile As String = ""
            sOutFile = sOutPath & "\" & ds1.Tables(RS).Rows(0)("発注番号") & "-" & ds1.Tables(RS).Rows(0)("発注番号枝番") & ".xlsx"



            app = New Excel.Application()
            book = app.Workbooks.Add(sHinaFile)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet)

            sheet.Range("C8").Value = ds1.Tables(RS).Rows(0)("仕入先名") & vbLf & ds1.Tables(RS).Rows(0)("仕入先郵便番号") & vbLf & ds1.Tables(RS).Rows(0)("仕入先住所")
            sheet.Range("C14").Value = ds1.Tables(RS).Rows(0)("仕入先担当者役職") & " " & ds1.Tables(RS).Rows(0)("仕入先担当者名")
            sheet.Range("A15").Value = "Telp." & ds1.Tables(RS).Rows(0)("仕入先電話番号") & "　Fax." & ds1.Tables(RS).Rows(0)("仕入先ＦＡＸ")
            sheet.Range("T8").Value = ds1.Tables(RS).Rows(0)("発注番号") & "-" & ds1.Tables(RS).Rows(0)("発注番号枝番")
            sheet.Range("T9").Value = ds1.Tables(RS).Rows(0)("発注日")
            sheet.Range("T12").Value = ds1.Tables(RS).Rows(0)("得意先名")
            sheet.Range("T13").Value = ds1.Tables(RS).Rows(0)("受注番号") & "-" & ds1.Tables(RS).Rows(0)("受注番号枝番")
            sheet.Range("T14").Value = ds1.Tables(RS).Rows(0)("支払条件")

            sheet.Range("H26").Value = ds1.Tables(RS).Rows(0)("仕入金額")
            sheet.Range("H27").Value = ds1.Tables(RS).Rows(0)("備考")

            sheet.Range("A34").Value = ds1.Tables(RS).Rows(0)("営業担当者")
            sheet.Range("A35").Value = ds1.Tables(RS).Rows(0)("入力担当者")


            Dim rowCnt As Integer = 0
            Dim lstRow As Integer = 20
            Dim addRowCnt As Integer = 0
            Dim currentCnt As Integer = 19
            Dim num As Integer = 1

            rowCnt = ds2.Tables(RS).Rows.Count - 1
            'rowCnt = 10

            Dim cellPos As String = lstRow & ":" & lstRow

            If rowCnt > 1 Then
                For addRow As Integer = 0 To rowCnt
                    Dim R As Object
                    cellPos = lstRow - 2 & ":" & lstRow - 2
                    R = sheet.Range(cellPos)
                    R.Copy()
                    R.Insert()
                    If Marshal.IsComObject(R) Then
                        Marshal.ReleaseComObject(R)
                    End If

                    lstRow = lstRow + 1
                Next
            End If

            Dim totalPrice As Integer = 0

            For index As Integer = 0 To ds2.Tables(RS).Rows.Count - 1
                Dim cell As String

                cell = "A" & currentCnt
                sheet.Range(cell).Value = num
                cell = "C" & currentCnt
                sheet.Range(cell).Value = ds2.Tables(RS).Rows(index)("メーカー") & "/" & ds2.Tables(RS).Rows(index)("品名") & "/" & ds2.Tables(RS).Rows(index)("型式")
                cell = "L" & currentCnt
                sheet.Range(cell).Value = ds2.Tables(RS).Rows(index)("発注数量") & " " & ds2.Tables(RS).Rows(index)("単位")
                cell = "O" & currentCnt
                sheet.Range(cell).Value = ds2.Tables(RS).Rows(index)("備考")
                cell = "R" & currentCnt
                sheet.Range(cell).Value = ds2.Tables(RS).Rows(index)("仕入単価")
                cell = "W" & currentCnt
                sheet.Range(cell).Value = ds2.Tables(RS).Rows(index)("仕入金額")

                totalPrice = totalPrice + ds2.Tables(RS).Rows(index)("仕入金額")

                sheet.Rows(currentCnt & ":" & currentCnt).AutoFit

                currentCnt = currentCnt + 1
                num = num + 1
            Next


            sheet.Range("W" & lstRow + 3).Value = totalPrice
            sheet.Range("W" & lstRow + 4).Value = totalPrice * 10 * 0.01
            sheet.Range("W" & lstRow + 5).Value = totalPrice * 10 * 0.01 + totalPrice

            book.SaveAs(sOutFile)
            app.Visible = True

            _msgHd.dspMSG("CreateExcel")

        Catch ex As Exception
            Throw ex

        Finally
            app.Quit()
            Marshal.ReleaseComObject(sheet)
            Marshal.ReleaseComObject(book)
            Marshal.ReleaseComObject(app)

        End Try
    End Sub
End Class