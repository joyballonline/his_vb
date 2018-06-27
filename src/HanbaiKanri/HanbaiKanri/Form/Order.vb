Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class Order
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
    Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ

    Private CompanyCode As String = ""
    Private OrderNo As String = ""
    Private OrderSuffix As String = ""
    Private OrderCount As String = ""
    Private OrderStatus As String = ""

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
                   Optional ByRef prmRefNo As String = Nothing,
                   Optional ByRef prmRefSuffix As String = Nothing,
                   Optional ByRef prmRefStatus As String = Nothing)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        OrderNo = prmRefNo
        OrderSuffix = prmRefSuffix
        OrderStatus = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示

        _init = True

    End Sub

    Private Sub Quote_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'DateTimePickerのフォーマットを指定
        DtpOrderRegistration.Text = DateAdd("m", 0, Now).ToString("yyyy/MM/dd")
        DtpOrderDate.Text = DateAdd("m", 0, Now).ToString("yyyy/MM/dd")
        DtpExpiration.Text = DateAdd("d", 7, Now).ToString("yyyy/MM/dd")
        DtpQuoteDate.Text = DateAdd("m", 0, Now).ToString("yyyy/MM/dd")
        DtpQuoteRegistration.Text = DateAdd("m", 0, Now).ToString("yyyy/MM/dd")

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
        Dim dtNow As DateTime = DateTime.Now


        '受注基本情報

        Dim Sql1 As String = ""
        Sql1 += "SELECT"
        Sql1 += " * "
        Sql1 += "FROM "
        Sql1 += "public"
        Sql1 += "."
        Sql1 += "t10_cymnhd"
        Sql1 += " WHERE "
        Sql1 += "受注番号"
        Sql1 += " ILIKE "
        Sql1 += "'"
        Sql1 += OrderNo.ToString
        Sql1 += "'"
        Sql1 += " AND "
        Sql1 += "受注番号枝番"
        Sql1 += " ILIKE "
        Sql1 += "'"
        Sql1 += OrderSuffix.ToString
        Sql1 += "'"

        Dim ds1 = _db.selectDB(Sql1, RS, reccnt)

        CompanyCode = ds1.Tables(RS).Rows(0)("会社コード")

        If ds1.Tables(RS).Rows(0)("受注番号") IsNot DBNull.Value Then
            TxtOrderNo.Text = ds1.Tables(RS).Rows(0)("受注番号")
        End If
        If ds1.Tables(RS).Rows(0)("受注番号枝番") IsNot DBNull.Value Then
            TxtOrderSuffix.Text = ds1.Tables(RS).Rows(0)("受注番号枝番")
        End If
        If ds1.Tables(RS).Rows(0)("受注日") IsNot DBNull.Value Then
            DtpOrderDate.Value = ds1.Tables(RS).Rows(0)("受注日")
        End If
        If ds1.Tables(RS).Rows(0)("登録日") IsNot DBNull.Value Then
            DtpQuoteRegistration.Value = ds1.Tables(RS).Rows(0)("登録日")
        End If
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
            Dim PostalCode As String = ds1.Tables(RS).Rows(0)("得意先郵便番号")
            TxtPostalCode1.Text = PostalCode.Substring(0, 3)
            TxtPostalCode2.Text = PostalCode.Substring(3, 4)
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
            TxtOrderRemark.Text = ds1.Tables(RS).Rows(0)("備考")
        End If
        If ds1.Tables(RS).Rows(0)("ＶＡＴ") IsNot DBNull.Value Then
            TxtVat.Text = ds1.Tables(RS).Rows(0)("ＶＡＴ")
        End If
        If ds1.Tables(RS).Rows(0)("粗利額") IsNot DBNull.Value Then
            TxtGrossProfit.Text = ds1.Tables(RS).Rows(0)("粗利額")
        End If
        If ds1.Tables(RS).Rows(0)("見積金額") IsNot DBNull.Value Then
            TxtOrderAmount.Text = ds1.Tables(RS).Rows(0)("見積金額")
        End If
        If ds1.Tables(RS).Rows(0)("見積備考") IsNot DBNull.Value Then
            TxtQuoteRemarks.Text = ds1.Tables(RS).Rows(0)("見積備考")
        End If

        ''見積明細情報
        Dim Sql3 As String = ""
        Sql3 += "SELECT"
        Sql3 += " * "
        Sql3 += "FROM "
        Sql3 += "public"
        Sql3 += "."
        Sql3 += "t11_cymndt"
        Sql3 += " WHERE "
        Sql3 += "受注番号"
        Sql3 += " ILIKE "
        Sql3 += "'"
        Sql3 += OrderNo.ToString
        Sql3 += "'"
        Sql3 += " AND "
        Sql3 += "受注番号枝番"
        Sql3 += " ILIKE "
        Sql3 += "'"
        Sql3 += OrderSuffix.ToString
        Sql3 += "'"
        Dim ds3 = _db.selectDB(Sql3, RS, reccnt)

        For index As Integer = 0 To ds3.Tables(RS).Rows.Count - 1
            DgvItemList.Rows.Add()
            Dim tmp As Integer = ds3.Tables(RS).Rows(index)("仕入区分")
            DgvItemList(1, index).Value = tmp
            DgvItemList.Rows(index).Cells(2).Value = ds3.Tables(RS).Rows(index)("メーカー")
            DgvItemList.Rows(index).Cells(3).Value = ds3.Tables(RS).Rows(index)("品名")
            DgvItemList.Rows(index).Cells(4).Value = ds3.Tables(RS).Rows(index)("型式")
            DgvItemList.Rows(index).Cells(5).Value = ds3.Tables(RS).Rows(index)("受注数量")
            DgvItemList.Rows(index).Cells(6).Value = ds3.Tables(RS).Rows(index)("単位")
            DgvItemList.Rows(index).Cells(7).Value = ds3.Tables(RS).Rows(index)("仕入先名")
            DgvItemList.Rows(index).Cells(8).Value = ds3.Tables(RS).Rows(index)("仕入値")
            DgvItemList.Rows(index).Cells(9).Value = ds3.Tables(RS).Rows(index)("間接費")
            DgvItemList.Rows(index).Cells(10).Value = ds3.Tables(RS).Rows(index)("売単価")
            DgvItemList.Rows(index).Cells(11).Value = ds3.Tables(RS).Rows(index)("売上金額")
            DgvItemList.Rows(index).Cells(12).Value = ds3.Tables(RS).Rows(index)("粗利額")
            DgvItemList.Rows(index).Cells(13).Value = ds3.Tables(RS).Rows(index)("粗利率")
            DgvItemList.Rows(index).Cells(14).Value = ds3.Tables(RS).Rows(index)("リードタイム")
            DgvItemList.Rows(index).Cells(15).Value = ds3.Tables(RS).Rows(index)("備考")
            DgvItemList.Rows(index).Cells(16).Value = ds3.Tables(RS).Rows(index)("出庫数")
            DgvItemList.Rows(index).Cells(17).Value = ds3.Tables(RS).Rows(index)("未出庫数")
        Next

        ''金額計算
        'Dim Total As Integer = 0
        'Dim PurchaseTotal As Integer = 0
        'Dim GrossProfit As Decimal = 0

        'For index As Integer = 0 To DgvItemList.Rows.Count - 1
        '    PurchaseTotal += DgvItemList.Rows(index).Cells(11).Value
        '    Total += DgvItemList.Rows(index).Cells(13).Value
        'Next

        'TxtOrderAmount.Text = PurchaseTotal
        'TxtGrossProfit.Text = Total - PurchaseTotal

        '行番号の振り直し
        Dim i As Integer = DgvItemList.Rows.Count()
        Dim No As Integer = 1
        For c As Integer = 0 To i - 1
            DgvItemList.Rows(c).Cells(0).Value = No
            No += 1
        Next c
        TxtItemCount.Text = DgvItemList.Rows.Count()

        If OrderStatus = "VIEW" Then
            DtpOrderDate.Enabled = False
            TxtOrderRemark.Enabled = False
            DtpQuoteDate.Enabled = False
            DtpExpiration.Enabled = False
            DgvItemList.ReadOnly = True
            BtnRegistration.Visible = False
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

        Dim Sql1 As String = ""
        Sql1 = ""
        Sql1 += "UPDATE "
        Sql1 += "Public."
        Sql1 += "t10_cymnhd "
        Sql1 += "SET "

        Sql1 += "備考"
        Sql1 += " = '"
        Sql1 += TxtOrderRemark.Text
        Sql1 += "', "
        Sql1 += "受注日"
        Sql1 += " = '"
        Sql1 += DtpOrderDate.Value
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
        Sql1 += " 受注番号"
        Sql1 += "='"
        Sql1 += OrderNo
        Sql1 += "' "
        Sql1 += " AND"
        Sql1 += " 受注番号枝番"
        Sql1 += "='"
        Sql1 += OrderSuffix
        Sql1 += "' "
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
        Sql1 += "ＶＡＴ"
        Sql1 += ", "
        Sql1 += "受注日"
        Sql1 += ", "
        Sql1 += "登録日"
        Sql1 += ", "
        Sql1 += "更新日"
        Sql1 += ", "
        Sql1 += "更新者"

        _db.executeDB(Sql1)

        Me.Close()
    End Sub
End Class