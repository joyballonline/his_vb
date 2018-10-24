Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
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
    Private _langHd As UtilLangHandler
    Private _ParentForm As Form
    Private CompanyCode As String = ""
    Private OrderNo As String = ""
    Private OrderSuffix As String = ""
    Private OrderCount As Integer
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
                   ByRef prmRefLang As UtilLangHandler,
                   ByRef prmRefForm As Form,
                   Optional ByRef prmRefNo As String = Nothing,
                   Optional ByRef prmRefSuffix As String = Nothing,
                   Optional ByRef prmRefStatus As String = Nothing)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _ParentForm = prmRefForm
        OrderNo = prmRefNo
        OrderSuffix = prmRefSuffix
        OrderStatus = prmRefStatus
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

        If OrderStatus = "ADD" Then
            LblMode.Text = "新規登録モード"
        ElseIf OrderStatus = "EDIT" Then
            LblMode.Text = "編集モード"
        ElseIf OrderStatus = "CLONE" Then
            LblMode.Text = "新規複写モード"
        ElseIf OrderStatus = "VIEW" Then
            LblMode.Text = "参照モード"
        End If

        Dim reccnt As Integer = 0
        Dim dtNow As DateTime = DateTime.Now

        If OrderStatus = "ADD" Then
            '見積基本情報
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
            SqlSaiban += "'20'"

            Dim Saiban1 As DataSet = _db.selectDB(SqlSaiban, RS, reccnt)

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
            Sql1 += OrderNo.ToString
            Sql1 += "'"
            Sql1 += " AND "
            Sql1 += "見積番号枝番"
            Sql1 += " ILIKE "
            Sql1 += "'"
            Sql1 += OrderSuffix.ToString
            Sql1 += "'"

            Dim ds1 = _db.selectDB(Sql1, RS, reccnt)
            CompanyCode = Saiban1.Tables(RS).Rows(0)(0)
            Dim NewOrderNo As String = Saiban1.Tables(RS).Rows(0)(5)
            NewOrderNo += dtNow.ToString("MMdd")
            OrderCount = Saiban1.Tables(RS).Rows(0)(2)
            Dim TmpOrderCount As String = Saiban1.Tables(RS).Rows(0)(2)
            NewOrderNo += TmpOrderCount.PadLeft(Saiban1.Tables(RS).Rows(0)(6), "0")

            SaibanSave()

            CompanyCode = ds1.Tables(RS).Rows(0)("会社コード")
            TxtOrderNo.Text = NewOrderNo
            TxtOrderSuffix.Text = 1
            DtpOrderRegistration.Value = dtNow
            DtpOrderDate.Value = dtNow

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
            If ds1.Tables(RS).Rows(0)("見積金額") IsNot DBNull.Value Then
                TxtOrderAmount.Text = ds1.Tables(RS).Rows(0)("見積金額")
            End If
            If ds1.Tables(RS).Rows(0)("仕入金額") IsNot DBNull.Value Then
                TxtPurchaseAmount.Text = ds1.Tables(RS).Rows(0)("仕入金額")
            End If
            If ds1.Tables(RS).Rows(0)("粗利額") IsNot DBNull.Value Then
                TxtGrossProfit.Text = ds1.Tables(RS).Rows(0)("粗利額")
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
            Sql3 += OrderNo.ToString
            Sql3 += "'"
            Sql3 += " AND "
            Sql3 += "見積番号枝番"
            Sql3 += " ILIKE "
            Sql3 += "'"
            Sql3 += OrderSuffix.ToString
            Sql3 += "'"
            Dim ds3 = _db.selectDB(Sql3, RS, reccnt)

            For index As Integer = 0 To ds3.Tables(RS).Rows.Count - 1
                DgvItemList.Rows.Add()
                Dim tmp As Integer = ds3.Tables(RS).Rows(index)("仕入区分")
                DgvItemList(1, index).Value = tmp
                DgvItemList.Rows(index).Cells("メーカー").Value = ds3.Tables(RS).Rows(index)("メーカー")
                DgvItemList.Rows(index).Cells("品名").Value = ds3.Tables(RS).Rows(index)("品名")
                DgvItemList.Rows(index).Cells("型式").Value = ds3.Tables(RS).Rows(index)("型式")
                DgvItemList.Rows(index).Cells("数量").Value = ds3.Tables(RS).Rows(index)("数量")
                DgvItemList.Rows(index).Cells("単位").Value = ds3.Tables(RS).Rows(index)("単位")
                DgvItemList.Rows(index).Cells("仕入先").Value = ds3.Tables(RS).Rows(index)("仕入先名称")
                DgvItemList.Rows(index).Cells("仕入値").Value = ds3.Tables(RS).Rows(index)("仕入単価")
                DgvItemList.Rows(index).Cells("間接費").Value = ds3.Tables(RS).Rows(index)("間接費")
                DgvItemList.Rows(index).Cells("売単価").Value = ds3.Tables(RS).Rows(index)("売単価")
                DgvItemList.Rows(index).Cells("売上金額").Value = ds3.Tables(RS).Rows(index)("売上金額")
                DgvItemList.Rows(index).Cells("粗利額").Value = ds3.Tables(RS).Rows(index)("粗利額")
                DgvItemList.Rows(index).Cells("粗利率").Value = ds3.Tables(RS).Rows(index)("粗利率")
                DgvItemList.Rows(index).Cells("リードタイム").Value = ds3.Tables(RS).Rows(index)("リードタイム")
                DgvItemList.Rows(index).Cells("備考").Value = ds3.Tables(RS).Rows(index)("備考")

            Next

            '行番号の振り直し
            Dim i As Integer = DgvItemList.Rows.Count()
            Dim No As Integer = 1
            For c As Integer = 0 To i - 1
                DgvItemList.Rows(c).Cells(0).Value = No
                No += 1
            Next c
            TxtItemCount.Text = DgvItemList.Rows.Count()
        Else
            '受注基本情報

            Dim Sql1 As String = ""
            Sql1 += "Select"
            Sql1 += " * "
            Sql1 += "FROM "
            Sql1 += "Public"
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

            Dim Sql2 As String = ""
            Sql2 += "SELECT"
            Sql2 += " * "
            Sql2 += "FROM "
            Sql2 += "public"
            Sql2 += "."
            Sql2 += "t10_cymnhd"
            Sql2 += " WHERE "
            Sql2 += "受注番号"
            Sql2 += " ILIKE "
            Sql2 += "'"
            Sql2 += ds1.Tables(RS).Rows(0)("受注番号")
            Sql2 += "'"
            Dim ds2 As DataSet = _db.selectDB(Sql2, RS, reccnt)

            Dim MaxSuffix As Integer = 0
            For index As Integer = 0 To ds2.Tables(RS).Rows.Count - 1
                If MaxSuffix < ds2.Tables(RS).Rows(index)("受注番号枝番") Then
                    MaxSuffix = ds2.Tables(RS).Rows(index)("受注番号枝番")
                End If
            Next

            CompanyCode = ds1.Tables(RS).Rows(0)("会社コード")
            If OrderStatus = "CLONE" Then
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
                SqlSaiban += "'20'"

                Dim Saiban1 As DataSet = _db.selectDB(SqlSaiban, RS, reccnt)

                CompanyCode = Saiban1.Tables(RS).Rows(0)(0)
                Dim NewOrderNo As String = Saiban1.Tables(RS).Rows(0)(5)
                NewOrderNo += dtNow.ToString("MMdd")
                OrderCount = Saiban1.Tables(RS).Rows(0)(2)
                Dim TmpOrderCount As String = Saiban1.Tables(RS).Rows(0)(2)
                NewOrderNo += TmpOrderCount.PadLeft(Saiban1.Tables(RS).Rows(0)(6), "0")

                SaibanSave()

                TxtOrderNo.Text = NewOrderNo
            Else
                If ds1.Tables(RS).Rows(0)("受注番号") IsNot DBNull.Value Then
                    TxtOrderNo.Text = ds1.Tables(RS).Rows(0)("受注番号")
                End If
            End If

            If OrderStatus = "CLONE" Then
                TxtOrderSuffix.Text = 1
            ElseIf OrderStatus = "EDIT" Then
                TxtOrderSuffix.Text = MaxSuffix + 1
            Else
                If ds1.Tables(RS).Rows(0)("受注番号枝番") IsNot DBNull.Value Then
                    TxtOrderSuffix.Text = ds1.Tables(RS).Rows(0)("受注番号枝番")
                End If
            End If

            If OrderStatus = "CLONE" Then
                DtpOrderDate.Value = dtNow
            Else
                If ds1.Tables(RS).Rows(0)("受注日") IsNot DBNull.Value Then
                    DtpOrderDate.Value = ds1.Tables(RS).Rows(0)("受注日")
                End If
            End If

            If OrderStatus = "CLONE" Then
                If ds1.Tables(RS).Rows(0)("登録日") IsNot DBNull.Value Then
                    DtpQuoteRegistration.Value = ds1.Tables(RS).Rows(0)("登録日")
                End If
            Else
                DtpOrderRegistration.Value = dtNow
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
            If ds1.Tables(RS).Rows(0)("仕入金額") IsNot DBNull.Value Then
                TxtPurchaseAmount.Text = ds1.Tables(RS).Rows(0)("仕入金額")
            End If
            If ds1.Tables(RS).Rows(0)("見積備考") IsNot DBNull.Value Then
                TxtQuoteRemarks.Text = ds1.Tables(RS).Rows(0)("見積備考")
            End If
            If ds1.Tables(RS).Rows(0)("客先番号") IsNot DBNull.Value Then
                TxtCustomerPO.Text = ds1.Tables(RS).Rows(0)("客先番号")
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
        End If
    End Sub

    Private Sub SaibanSave()
        Dim dtNow As DateTime = DateTime.Now
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
    End Sub

    '前の画面に戻る
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _ParentForm.Enabled = True
        _ParentForm.Show()
        Me.Dispose()
    End Sub

    Private Sub BtnRegistration_Click(sender As Object, e As EventArgs) Handles BtnRegistration.Click
        Dim reccnt As Integer = 0
        Dim dtNow As DateTime = DateTime.Now

        If OrderStatus = "ADD" Or OrderStatus = "EDIT" Or OrderStatus = "CLONE" Then
            Dim Sql1 As String = ""
            Sql1 = ""
            Sql1 += "INSERT INTO "
            Sql1 += "Public."
            Sql1 += "t10_cymnhd("
            Sql1 += "会社コード, 受注番号, 受注番号枝番, 客先番号, 見積番号, 見積番号枝番, 得意先コード, 得意先名, 得意先郵便番号, 得意先住所, 得意先電話番号, 得意先ＦＡＸ, 得意先担当者役職, 得意先担当者名, 見積日, 見積有効期限, 支払条件, 見積金額,仕入金額, 粗利額, 営業担当者, 入力担当者, 備考, 見積備考, ＶＡＴ, 受注日, 登録日, 更新日, 更新者, 取消区分)"
            Sql1 += " VALUES('"
            Sql1 += CompanyCode
            Sql1 += "', '"
            Sql1 += TxtOrderNo.Text
            Sql1 += "', '"
            Sql1 += TxtOrderSuffix.Text
            Sql1 += "', '"
            Sql1 += TxtCustomerPO.Text
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
            Sql1 += "客先番号"
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
                Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("仕入値").Value.ToString
                Sql2 += "', '"
                Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("数量").Value.ToString
                Sql2 += "', '"
                Sql2 += "0"
                Sql2 += "', '"
                Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("数量").Value.ToString
                Sql2 += "', '"

                If DgvItemList.Rows(cymnhdIdx).Cells("間接費").Value.ToString = "" Then
                    Sql2 += "0"
                Else
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("間接費").Value.ToString
                End If
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
        Else
            Dim Sql1 As String = ""
            Sql1 = ""
            Sql1 += "UPDATE "
            Sql1 += "Public."
            Sql1 += "t10_cymnhd "
            Sql1 += "SET "

            Sql1 += "客先番号"
            Sql1 += " = '"
            Sql1 += TxtCustomerPO.Text
            Sql1 += "', "
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
            Sql1 += "客先番号"
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
        End If

        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()

    End Sub
End Class