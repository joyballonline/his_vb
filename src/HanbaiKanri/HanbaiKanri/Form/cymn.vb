Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class Cymn
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
    Private _parentForm As Form
    Private CompanyCode As String = ""
    Private QuoteNo As String = ""
    Private QuoteSuffix As String = ""
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
                   ByRef prmRefForm As Form,
                   Optional ByRef prmRefNo As String = Nothing,
                   Optional ByRef prmRefSuffix As String = Nothing)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
        QuoteNo = prmRefNo
        QuoteSuffix = prmRefSuffix

        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    Private Sub Quote_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'DateTimePickerのフォーマットを指定
        DtpOrderRegistration.Text = DateAdd("m", 0, Now).ToString("yyyy/MM/dd")
        DtpOrderDate.Text = DateAdd("m", 0, Now).ToString("yyyy/MM/dd")
        DtpExpiration.Text = DateAdd("d", 7, Now).ToString("yyyy/MM/dd")
        DtpPurchaseDate.Text = DateAdd("m", 0, Now).ToString("yyyy/MM/dd")
        DtpQuoteDate.Text = DateAdd("m", 0, Now).ToString("yyyy/MM/dd")
        DtpQuoteRegistration.Text = DateAdd("d", 7, Now).ToString("yyyy/MM/dd")

        DgvItemList.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

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
        SqlSaiban += "'%20%'"

        Dim dtNow As DateTime = DateTime.Now

        Dim Saiban1 As DataSet = _db.selectDB(SqlSaiban, RS, reccnt)

        Dim SqlSaiban2 As String = ""
        SqlSaiban2 += "SELECT "
        SqlSaiban2 += "会社コード, "
        SqlSaiban2 += "採番キー, "
        SqlSaiban2 += "最新値, "
        SqlSaiban2 += "最小値, "
        SqlSaiban2 += "最大値, "
        SqlSaiban2 += "接頭文字, "
        SqlSaiban2 += "連番桁数 "
        SqlSaiban2 += "FROM "
        SqlSaiban2 += "public"
        SqlSaiban2 += "."
        SqlSaiban2 += "m80_saiban"
        SqlSaiban2 += " WHERE "
        SqlSaiban2 += "採番キー"
        SqlSaiban2 += " ILIKE "
        SqlSaiban2 += "'%30%'"

        Dim Saiban2 As DataSet = _db.selectDB(SqlSaiban2, RS, reccnt)

        CompanyCode = Saiban1.Tables(RS).Rows(0)(0)
        Dim OrderNo As String = Saiban1.Tables(RS).Rows(0)(5)
        OrderNo += dtNow.ToString("MMdd")
        OrderCount = Saiban1.Tables(RS).Rows(0)(2)
        OrderNo += OrderCount.PadLeft(Saiban1.Tables(RS).Rows(0)(6), "0")

        '見積基本情報

        Dim Sql1 As String = ""
        Sql1 += "SELECT"
        Sql1 += " * "
        Sql1 += "FROM "
        Sql1 += "public"
        Sql1 += "."
        Sql1 += "t01_mithd"
        Sql1 += " WHERE "
        Sql1 += "見積番号"
        Sql1 += " ILIKE "
        Sql1 += "'"
        Sql1 += QuoteNo.ToString
        Sql1 += "'"
        Sql1 += " AND "
        Sql1 += "見積番号枝番"
        Sql1 += " ILIKE "
        Sql1 += "'"
        Sql1 += QuoteSuffix.ToString
        Sql1 += "'"

        Dim ds1 = _db.selectDB(Sql1, RS, reccnt)

        CompanyCode = ds1.Tables(RS).Rows(0)("会社コード")
        TxtOrderNo.Text = OrderNo
        TxtOrderSuffix.Text = 1
        DtpOrderRegistration.Value = dtNow
        DtpOrderDate.Value = dtNow
        DtpPurchaseDate.Value = dtNow
        If ds1.Tables(RS).Rows(0)("見積番号") IsNot DBNull.Value Then
            TxtQuoteNo.Text = ds1.Tables(RS).Rows(0)("見積番号")
        End If
        If ds1.Tables(RS).Rows(0)("見積番号枝番") IsNot DBNull.Value Then
            TxtQuoteSuffix.Text = ds1.Tables(RS).Rows(0)("見積番号枝番")
        End If
        If ds1.Tables(RS).Rows(0)("登録日") IsNot DBNull.Value Then
            DtpQuoteRegistration.Value = ds1.Tables(RS).Rows(0)("登録日")
        End If
        If ds1.Tables(RS).Rows(0)("見積日") IsNot DBNull.Value Then
            DtpQuoteDate.Value = ds1.Tables(RS).Rows(0)("見積日")
        End If
        If ds1.Tables(RS).Rows(0)("見積有効期限") IsNot DBNull.Value Then
            DtpExpiration.Value = ds1.Tables(RS).Rows(0)("見積有効期限")
        End If
        If ds1.Tables(RS).Rows(0)("得意先コード") IsNot DBNull.Value Then
            TxtCustomerCode.Text = ds1.Tables(RS).Rows(0)("得意先コード")
        End If
        If ds1.Tables(RS).Rows(0)("得意先名") IsNot DBNull.Value Then
            TxtCustomerName.Text = ds1.Tables(RS).Rows(0)("得意先名")
        End If
        If ds1.Tables(RS).Rows(0)("得意先担当者名") IsNot DBNull.Value Then
            TxtPerson.Text = ds1.Tables(RS).Rows(0)("得意先担当者名")
        End If
        If ds1.Tables(RS).Rows(0)("得意先担当者役職") IsNot DBNull.Value Then
            TxtPosition.Text = ds1.Tables(RS).Rows(0)("得意先担当者役職")
        End If
        If ds1.Tables(RS).Rows(0)("得意先郵便番号") IsNot DBNull.Value Then
            TxtPostalCode.Text = ds1.Tables(RS).Rows(0)("得意先郵便番号")
        End If
        If ds1.Tables(RS).Rows(0)("得意先住所") IsNot DBNull.Value Then
            Dim Address As String = ds1.Tables(RS).Rows(0)("得意先住所")
            Dim delimiter As String = " "
            Dim parts As String() = Split(Address, delimiter, -1, CompareMethod.Text)
            TxtAddress1.Text = parts(0).ToString
            TxtAddress2.Text = parts(1).ToString
            TxtAddress3.Text = parts(2).ToString
        End If
        If ds1.Tables(RS).Rows(0)("得意先電話番号") IsNot DBNull.Value Then
            TxtTel.Text = ds1.Tables(RS).Rows(0)("得意先電話番号")
        End If
        If ds1.Tables(RS).Rows(0)("得意先ＦＡＸ") IsNot DBNull.Value Then
            TxtFax.Text = ds1.Tables(RS).Rows(0)("得意先ＦＡＸ")
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
            TxtQuoteRemarks.Text = ds1.Tables(RS).Rows(0)("備考")
        End If
        If ds1.Tables(RS).Rows(0)("ＶＡＴ") IsNot DBNull.Value Then
            TxtVat.Text = ds1.Tables(RS).Rows(0)("ＶＡＴ")
        End If

        ''見積明細情報
        Dim Sql3 As String = ""
        Sql3 += "SELECT"
        Sql3 += " * "
        Sql3 += "FROM "
        Sql3 += "public"
        Sql3 += "."
        Sql3 += "t02_mitdt"
        Sql3 += " WHERE "
        Sql3 += "見積番号"
        Sql3 += " ILIKE "
        Sql3 += "'"
        Sql3 += QuoteNo.ToString
        Sql3 += "'"
        Sql3 += " AND "
        Sql3 += "見積番号枝番"
        Sql3 += " ILIKE "
        Sql3 += "'"
        Sql3 += QuoteSuffix.ToString
        Sql3 += "'"
        Dim ds3 = _db.selectDB(Sql3, RS, reccnt)

        For index As Integer = 0 To ds3.Tables(RS).Rows.Count - 1
            DgvItemList.Rows.Add()
            Dim tmp As Integer = ds3.Tables(RS).Rows(index)("仕入区分")
            DgvItemList(1, index).Value = tmp
            DgvItemList.Rows(index).Cells(2).Value = ds3.Tables(RS).Rows(index)("メーカー")
            DgvItemList.Rows(index).Cells(3).Value = ds3.Tables(RS).Rows(index)("品名")
            DgvItemList.Rows(index).Cells(4).Value = ds3.Tables(RS).Rows(index)("型式")
            DgvItemList.Rows(index).Cells(5).Value = ds3.Tables(RS).Rows(index)("数量")
            DgvItemList.Rows(index).Cells(6).Value = ds3.Tables(RS).Rows(index)("単位")
            DgvItemList.Rows(index).Cells(7).Value = ds3.Tables(RS).Rows(index)("仕入先名称")
            DgvItemList.Rows(index).Cells(8).Value = ds3.Tables(RS).Rows(index)("仕入単価")
            DgvItemList.Rows(index).Cells(9).Value = ds3.Tables(RS).Rows(index)("間接費率")
            DgvItemList.Rows(index).Cells(10).Value = ds3.Tables(RS).Rows(index)("間接費")
            DgvItemList.Rows(index).Cells(11).Value = ds3.Tables(RS).Rows(index)("仕入金額")
            DgvItemList.Rows(index).Cells(12).Value = ds3.Tables(RS).Rows(index)("売単価")
            DgvItemList.Rows(index).Cells(13).Value = ds3.Tables(RS).Rows(index)("売上金額")
            DgvItemList.Rows(index).Cells(14).Value = ds3.Tables(RS).Rows(index)("粗利額")
            DgvItemList.Rows(index).Cells(15).Value = ds3.Tables(RS).Rows(index)("粗利率")
            DgvItemList.Rows(index).Cells(16).Value = ds3.Tables(RS).Rows(index)("リードタイム")
            DgvItemList.Rows(index).Cells(17).Value = ds3.Tables(RS).Rows(index)("備考")
            DgvItemList.Rows(index).Cells(18).Value = ""
        Next

        '金額計算
        Dim Total As Integer = 0
        Dim PurchaseTotal As Integer = 0
        Dim GrossProfit As Decimal = 0

        For index As Integer = 0 To DgvItemList.Rows.Count - 1
            PurchaseTotal += DgvItemList.Rows(index).Cells(11).Value
            Total += DgvItemList.Rows(index).Cells(13).Value
        Next

        TxtOrderAmount.Text = PurchaseTotal
        TxtPurchaseAmount.Text = Total
        TxtGrossProfit.Text = Total - PurchaseTotal




        '行番号の振り直し
        Dim i As Integer = DgvItemList.Rows.Count()
        Dim No As Integer = 1
        For c As Integer = 0 To i - 1
            DgvItemList.Rows(c).Cells(0).Value = No
            No += 1
        Next c
        TxtItemCount.Text = DgvItemList.Rows.Count()


    End Sub



    '金額自動計算
    'Private Sub CellValueChanged(ByVal sender As Object,
    'ByVal e As DataGridViewCellEventArgs) _
    'Handles DgvItemList.CellValueChanged
    '    If LoadFlg Then
    '        TxtPurchaseTotal.Clear()
    '        TxtTotal.Clear()
    '        TxtGrossProfit.Clear()

    '        Dim Total As Integer = 0
    '        Dim PurchaseTotal As Integer = 0
    '        Dim GrossProfit As Decimal = 0

    '        If e.RowIndex > -1 Then
    '            If DgvItemList.Rows(e.RowIndex).Cells(8).Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells(9).Value IsNot Nothing Then
    '                DgvItemList.Rows(e.RowIndex).Cells(10).Value = DgvItemList.Rows(e.RowIndex).Cells(8).Value * DgvItemList.Rows(e.RowIndex).Cells(9).Value
    '                If DgvItemList.Rows(e.RowIndex).Cells(5).Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells(8).Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells(10).Value IsNot Nothing Then
    '                    DgvItemList.Rows(e.RowIndex).Cells(11).Value = DgvItemList.Rows(e.RowIndex).Cells(5).Value * DgvItemList.Rows(e.RowIndex).Cells(8).Value + DgvItemList.Rows(e.RowIndex).Cells(10).Value
    '                End If
    '            End If
    '            If DgvItemList.Rows(e.RowIndex).Cells(5).Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells(12).Value IsNot Nothing Then
    '                DgvItemList.Rows(e.RowIndex).Cells(13).Value = DgvItemList.Rows(e.RowIndex).Cells(5).Value * DgvItemList.Rows(e.RowIndex).Cells(12).Value
    '                If DgvItemList.Rows(e.RowIndex).Cells(11).Value IsNot Nothing Then
    '                    DgvItemList.Rows(e.RowIndex).Cells(14).Value = DgvItemList.Rows(e.RowIndex).Cells(13).Value - DgvItemList.Rows(e.RowIndex).Cells(11).Value
    '                    DgvItemList.Rows(e.RowIndex).Cells(15).Value = Format(DgvItemList.Rows(e.RowIndex).Cells(14).Value / DgvItemList.Rows(e.RowIndex).Cells(13).Value * 100, "0.000")
    '                End If
    '            End If
    '        End If

    '        For index As Integer = 0 To DgvItemList.Rows.Count - 1
    '            PurchaseTotal += DgvItemList.Rows(index).Cells(11).Value
    '            Total += DgvItemList.Rows(index).Cells(13).Value
    '        Next
    '        TxtPurchaseTotal.Text = PurchaseTotal
    '        TxtTotal.Text = Total
    '        TxtGrossProfit.Text = Total - PurchaseTotal
    '    End If
    'End Sub


    '前の画面に戻る
    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    Private Sub BtnRegistration_Click(sender As Object, e As EventArgs) Handles BtnRegistration.Click

        Dim reccnt As Integer = 0

        Dim dtNow As DateTime = DateTime.Now

        Dim Sql1 As String = ""
        Sql1 = ""
        Sql1 += "INSERT INTO "
        Sql1 += "Public."
        Sql1 += "t10_cymnhd("
        Sql1 += "会社コード, 受注番号, 受注番号枝番, 見積番号, 見積番号枝番, 得意先コード, 得意先名, 得意先郵便番号, 得意先住所, 得意先電話番号, 得意先ＦＡＸ, 得意先担当者役職, 得意先担当者名, 見積日, 見積有効期限, 支払条件, 見積金額,仕入金額, 粗利額, 営業担当者, 入力担当者, 備考, 見積備考, ＶＡＴ, 受注日, 登録日, 更新日, 更新者, 取消区分)"
        Sql1 += " VALUES('"
        Sql1 += CompanyCode
        Sql1 += "', '"
        Sql1 += TxtOrderNo.Text
        Sql1 += "', '"
        Sql1 += TxtOrderSuffix.Text
        Sql1 += "', '"
        Sql1 += TxtQuoteNo.Text
        Sql1 += "', '"
        Sql1 += TxtQuoteSuffix.Text
        Sql1 += "', '"
        Sql1 += TxtCustomerCode.Text
        Sql1 += "', '"
        Sql1 += TxtCustomerName.Text
        Sql1 += "', '"
        Sql1 += TxtPostalCode.Text
        Sql1 += "', '"
        Sql1 += TxtAddress1.Text
        Sql1 += " "
        Sql1 += TxtAddress2.Text
        Sql1 += " "
        Sql1 += TxtAddress3.Text
        Sql1 += "', '"
        Sql1 += TxtTel.Text
        Sql1 += "', '"
        Sql1 += TxtFax.Text
        Sql1 += "', '"
        Sql1 += TxtPosition.Text
        Sql1 += "', '"
        Sql1 += TxtPerson.Text
        Sql1 += "', '"
        Sql1 += DtpQuoteDate.Value
        Sql1 += "', '"
        Sql1 += DtpExpiration.Value
        Sql1 += "', '"
        Sql1 += TxtPaymentTerms.Text
        Sql1 += "', '"
        Sql1 += TxtOrderAmount.Text
        Sql1 += "', '"
        Sql1 += TxtPurchaseAmount.Text
        Sql1 += "', '"
        Sql1 += TxtGrossProfit.Text
        Sql1 += "', '"
        Sql1 += TxtSales.Text
        Sql1 += "', '"
        Sql1 += TxtInput.Text
        Sql1 += "', '"
        Sql1 += TxtOrderRemark.Text
        Sql1 += "', '"
        Sql1 += TxtQuoteRemarks.Text
        Sql1 += "', '"
        Sql1 += TxtVat.Text
        Sql1 += "', '"
        Sql1 += DtpOrderDate.Value
        Sql1 += "', '"
        Sql1 += DtpOrderRegistration.Value
        Sql1 += "', '"
        Sql1 += dtNow
        Sql1 += "', '"
        Sql1 += "zenbi01"
        Sql1 += "', '"
        Sql1 += "0"
        Sql1 += " ')"
        Sql1 += "RETURNING 会社コード"
        Sql1 += ", "
        Sql1 += "受注番号"
        Sql1 += ", "
        Sql1 += "受注番号枝番"
        Sql1 += ", "
        Sql1 += "見積番号"
        Sql1 += ", "
        Sql1 += "見積番号枝番"
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
        Sql1 += ", "
        Sql1 += "見積日"
        Sql1 += ", "
        Sql1 += "見積有効期限"
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
        Sql1 += "見積備考"
        Sql1 += ", "
        Sql1 += "ＶＡＴ"
        Sql1 += ", "
        Sql1 += "受注日"
        Sql1 += ", "
        Sql1 += "登録日"
        Sql1 += ", "
        Sql1 += "更新日"
        Sql1 += ", "
        Sql1 += "更新者"
        Sql1 += ", "
        Sql1 += "取消区分"

        _db.executeDB(Sql1)

        Dim Sql2 As String = ""
        For cymnhdIdx As Integer = 0 To DgvItemList.Rows.Count - 1
            Sql2 = ""
            Sql2 += "INSERT INTO "
            Sql2 += "Public."
            Sql2 += "t11_cymndt("
            Sql2 += "会社コード, 受注番号, 受注番号枝番, 行番号, 仕入区分, メーカー, 品名, 型式, 単位, 仕入先名, 仕入値, 受注数量, 売上数量, 受注残数, 間接費, 売単価, 売上金額, 粗利額, 粗利率, リードタイム, 出庫数, 未出庫数, 備考, 更新者, 登録日)"
            Sql2 += " VALUES('"
            Sql2 += CompanyCode
            Sql2 += "', '"
            Sql2 += TxtOrderNo.Text
            Sql2 += "', '"
            Sql2 += TxtOrderSuffix.Text
            Sql2 += "', '"
            Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("No").Value.ToString
            Sql2 += "', '"
            Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("仕入区分").Value.ToString
            Sql2 += "', '"
            Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("メーカー").Value.ToString
            Sql2 += "', '"
            Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("品名").Value.ToString
            Sql2 += "', '"
            Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("型式").Value.ToString
            Sql2 += "', '"
            Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("単位").Value.ToString
            Sql2 += "', '"
            Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("仕入先").Value.ToString
            Sql2 += "', '"
            Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("仕入単価").Value.ToString
            Sql2 += "', '"
            Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("数量").Value.ToString
            Sql2 += "', '"
            Sql2 += "0"
            Sql2 += "', '"
            Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("数量").Value.ToString
            Sql2 += "', '"
            Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("間接費").Value.ToString
            Sql2 += "', '"
            Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("売単価").Value.ToString
            Sql2 += "', '"
            Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("売上金額").Value.ToString
            Sql2 += "', '"
            Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("粗利額").Value.ToString
            Sql2 += "', '"
            Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("粗利率").Value.ToString
            Sql2 += "', '"
            Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("リードタイム").Value.ToString
            Sql2 += "', '"
            Sql2 += "0"
            Sql2 += "', '"
            Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("数量").Value.ToString
            Sql2 += "', '"
            Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("備考").Value.ToString
            Sql2 += "', '"
            Sql2 += "zenbi01"
            Sql2 += "', '"
            Sql2 += dtNow
            Sql2 += " ')"
            Sql2 += "RETURNING 会社コード"
            Sql2 += ", "
            Sql2 += "受注番号"
            Sql2 += ", "
            Sql2 += "受注番号枝番"
            Sql2 += ", "
            Sql2 += "行番号"
            Sql2 += ", "
            Sql2 += "仕入区分"
            Sql2 += ", "
            Sql2 += "メーカー"
            Sql2 += ", "
            Sql2 += "品名"
            Sql2 += ", "
            Sql2 += "型式"
            Sql2 += ", "
            Sql2 += "単位"
            Sql2 += ", "
            Sql2 += "仕入先名"
            Sql2 += ", "
            Sql2 += "仕入値"
            Sql2 += ", "
            Sql2 += "受注数量"
            Sql2 += ", "
            Sql2 += "売上数量"
            Sql2 += ", "
            Sql2 += "受注残数"
            Sql2 += ", "
            Sql2 += "間接費"
            Sql2 += ", "
            Sql2 += "売単価"
            Sql2 += ", "
            Sql2 += "売上金額"
            Sql2 += ", "
            Sql2 += "粗利額"
            Sql2 += ", "
            Sql2 += "粗利率"
            Sql2 += ", "
            Sql2 += "リードタイム"
            Sql2 += ", "
            Sql2 += "出庫数"
            Sql2 += ", "
            Sql2 += "未出庫数"
            Sql2 += ", "
            Sql2 += "備考"
            Sql2 += ", "
            Sql2 += "更新者"
            Sql2 += ", "
            Sql2 += "登録日"

            _db.executeDB(Sql2)

            Sql2 = ""
        Next

        Dim SupplierList As New List(Of String)(New String() {})
        For i As Integer = 0 To DgvItemList.Rows.Count - 1
            If SupplierList.Contains(DgvItemList.Rows(i).Cells("仕入先").Value) = False Then
                SupplierList.Add(DgvItemList.Rows(i).Cells("仕入先").Value)
            Else

            End If
        Next

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
        Dim supplier As String = ""

        For s As Integer = 0 To SupplierList.Count - 1
            supplier = SupplierList(s)

            Dim Sql As String = ""
            Sql += "SELECT"
            Sql += " * "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "m11_supplier"
            Sql += " WHERE "
            Sql += "仕入先名"
            Sql += " ILIKE "
            Sql += "'%"
            Sql += supplier
            Sql += "%'"
            Dim ds1 As DataSet = _db.selectDB(Sql, RS, reccnt)

            Dim PurchaseNo As String = Saiban.Tables(RS).Rows(0)(5)
            PurchaseNo += dtNow.ToString("MMdd")
            PurchaseNo += PurchaseCount.PadLeft(Saiban.Tables(RS).Rows(0)(6), "0")

            Dim Sql3 As String = ""
            Sql3 = ""
            Sql3 += "INSERT INTO "
            Sql3 += "Public."
            Sql3 += "t20_hattyu("
            Sql3 += "会社コード, 発注番号, 発注番号枝番, 受注番号, 受注番号枝番, 見積番号, 見積番号枝番, 得意先コード, 得意先名, 得意先郵便番号, 得意先住所, 得意先電話番号, 得意先ＦＡＸ, 得意先担当者役職, 得意先担当者名, 仕入先コード, 仕入先名, 仕入先郵便番号, 仕入先住所, 仕入先電話番号, 仕入先ＦＡＸ, 仕入先担当者役職, 仕入先担当者名, 見積日, 見積有効期限, 支払条件, 見積金額,仕入金額, 粗利額, 営業担当者, 入力担当者, 備考, 見積備考, ＶＡＴ, ＰＰＨ, 受注日, 発注日, 登録日, 更新日, 更新者, 取消区分)"
            Sql3 += " VALUES('"
            Sql3 += CompanyCode
            Sql3 += "', '"
            Sql3 += PurchaseNo
            Sql3 += "', '"
            Sql3 += "1"
            Sql3 += "', '"
            Sql3 += TxtOrderNo.Text
            Sql3 += "', '"
            Sql3 += TxtOrderSuffix.Text
            Sql3 += "', '"
            Sql3 += TxtQuoteNo.Text
            Sql3 += "', '"
            Sql3 += TxtQuoteSuffix.Text
            Sql3 += "', '"
            Sql3 += TxtCustomerCode.Text
            Sql3 += "', '"
            Sql3 += TxtCustomerName.Text
            Sql3 += "', '"
            Sql3 += TxtPostalCode.Text
            Sql3 += "', '"
            Sql3 += TxtAddress1.Text
            Sql3 += " "
            Sql3 += TxtAddress2.Text
            Sql3 += " "
            Sql3 += TxtAddress3.Text
            Sql3 += "', '"
            Sql3 += TxtTel.Text
            Sql3 += "', '"
            Sql3 += TxtFax.Text
            Sql3 += "', '"
            Sql3 += TxtPosition.Text
            Sql3 += "', '"
            Sql3 += TxtPerson.Text
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("仕入先コード").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("仕入先名").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("郵便番号").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("住所１").ToString
            Sql3 += " "
            Sql3 += ds1.Tables(RS).Rows(0)("住所２").ToString
            Sql3 += " "
            Sql3 += ds1.Tables(RS).Rows(0)("住所３").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("電話番号").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("ＦＡＸ番号").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("担当者役職").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("担当者名").ToString
            Sql3 += "', '"
            Sql3 += DtpQuoteDate.Value
            Sql3 += "', '"
            Sql3 += DtpExpiration.Value
            Sql3 += "', '"
            Sql3 += TxtPaymentTerms.Text
            Sql3 += "', '"
            Sql3 += TxtOrderAmount.Text
            Sql3 += "', '"
            Sql3 += TxtPurchaseAmount.Text
            Sql3 += "', '"
            Sql3 += TxtGrossProfit.Text
            Sql3 += "', '"
            Sql3 += TxtSales.Text
            Sql3 += "', '"
            Sql3 += TxtInput.Text
            Sql3 += "', '"
            Sql3 += TxtPurchaseRemark.Text
            Sql3 += "', '"
            Sql3 += TxtQuoteRemarks.Text
            Sql3 += "', '"
            Sql3 += TxtVat.Text
            Sql3 += "', '"
            Sql3 += TxtPph.Text
            Sql3 += "', '"
            Sql3 += DtpOrderDate.Value
            Sql3 += "', '"
            Sql3 += DtpPurchaseDate.Value
            Sql3 += "', '"
            Sql3 += DtpOrderRegistration.Value
            Sql3 += "', '"
            Sql3 += dtNow
            Sql3 += "', '"
            Sql3 += "zenbi01"
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
            Sql3 += ", "
            Sql3 += "取消区分"

            _db.executeDB(Sql3)
            Sql3 = ""
            Dim Sql4 As String = ""
            Dim test As String = ""
            For hattyuIdx As Integer = 0 To DgvItemList.Rows.Count - 1
                If DgvItemList.Rows(hattyuIdx).Cells("仕入先").Value = supplier Then
                    Sql4 = ""
                    Sql4 += "INSERT INTO "
                    Sql4 += "Public."
                    Sql4 += "t21_hattyu("
                    Sql4 += "会社コード, 発注番号, 発注番号枝番, 行番号, 仕入区分, メーカー, 品名, 型式, 単位, 仕入先名, 仕入値, 発注数量, 仕入数量, 発注残数, 間接費, 仕入単価, 仕入金額, リードタイム, 入庫数, 未入庫数, 備考, 更新者, 登録日)"
                    Sql4 += " VALUES('"
                    Sql4 += CompanyCode
                    Sql4 += "', '"
                    Sql4 += PurchaseNo
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
                    Sql4 += DgvItemList.Rows(hattyuIdx).Cells("売単価").Value.ToString
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
                    Sql4 += "zenbi01"
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
                End If
            Next
            PurchaseCount += 1
        Next

        OrderCount += 1
        Dim Saiban3 As String = ""
        Saiban3 += "UPDATE "
        Saiban3 += "Public."
        Saiban3 += "m80_saiban "
        Saiban3 += "SET "
        Saiban3 += " 最新値"
        Saiban3 += " = '"
        Saiban3 += OrderCount.ToString
        Saiban3 += "', "
        Saiban3 += "更新者"
        Saiban3 += " = '"
        Saiban3 += "Admin"
        Saiban3 += "', "
        Saiban3 += "更新日"
        Saiban3 += " = '"
        Saiban3 += dtNow
        Saiban3 += "' "
        Saiban3 += "WHERE"
        Saiban3 += " 会社コード"
        Saiban3 += "='"
        Saiban3 += CompanyCode.ToString
        Saiban3 += "'"
        Saiban3 += " AND"
        Saiban3 += " 採番キー"
        Saiban3 += "='"
        Saiban3 += "20"
        Saiban3 += "' "
        Saiban3 += "RETURNING 会社コード"
        Saiban3 += ", "
        Saiban3 += "採番キー"
        Saiban3 += ", "
        Saiban3 += "最新値"
        Saiban3 += ", "
        Saiban3 += "最小値"
        Saiban3 += ", "
        Saiban3 += "最大値"
        Saiban3 += ", "
        Saiban3 += "接頭文字"
        Saiban3 += ", "
        Saiban3 += "連番桁数"
        Saiban3 += ", "
        Saiban3 += "更新者"
        Saiban3 += ", "
        Saiban3 += "更新日"

        _db.executeDB(Saiban3)

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
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub
End Class