﻿Option Explicit On

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

        Dim reccnt As Integer = 0
        Dim Sql12 As String = ""

        Sql12 += "SELECT "
        Sql12 += "* "
        Sql12 += "FROM "
        Sql12 += "public"
        Sql12 += "."
        Sql12 += "m90_hanyo"
        Sql12 += " WHERE "
        Sql12 += "会社コード"
        Sql12 += " ILIKE "
        Sql12 += "'"
        Sql12 += frmC01F10_Login.loginValue.BumonNM
        Sql12 += "'"
        Sql12 += " AND "
        Sql12 += "固定キー"
        Sql12 += " ILIKE "
        Sql12 += "'"
        Sql12 += "4"
        Sql12 += "'"

        Dim ds12 As DataSet = _db.selectDB(Sql12, RS, reccnt)

        Dim table2 As New DataTable("Table")
        table2.Columns.Add("Display", GetType(String))
        table2.Columns.Add("Value", GetType(Integer))

        For index As Integer = 0 To ds12.Tables(RS).Rows.Count - 1
            table2.Rows.Add(ds12.Tables(RS).Rows(index)("文字１"), ds12.Tables(RS).Rows(index)("可変キー"))
        Next

        Dim column2 As New DataGridViewComboBoxColumn()
        column2.DataSource = table2
        column2.ValueMember = "Value"
        column2.DisplayMember = "Display"
        column2.HeaderText = "リードタイム単位"
        column2.Name = "リードタイム単位"

        DgvItemList.Columns.Insert(25, column2)

        If OrderStatus = "ADD" Then
            LblMode.Text = "新規登録モード"
        ElseIf OrderStatus = "EDIT" Then
            LblMode.Text = "編集モード"
        ElseIf OrderStatus = "CLONE" Then
            LblMode.Text = "新規複写モード"
        ElseIf OrderStatus = "VIEW" Then
            LblMode.Text = "参照モード"
        End If


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
                If ds3.Tables(RS).Rows(index)("リードタイム単位") Is DBNull.Value Then
                Else
                    Dim tmp2 As Integer = ds3.Tables(RS).Rows(index)("リードタイム単位")
                    DgvItemList("リードタイム単位", index).Value = tmp2
                End If
                DgvItemList.Rows(index).Cells("備考").Value = ds3.Tables(RS).Rows(index)("備考")
                DgvItemList.Rows(index).Cells("見積単価").Value = ds3.Tables(RS).Rows(index)("見積単価")
                DgvItemList.Rows(index).Cells("見積金額").Value = ds3.Tables(RS).Rows(index)("見積金額")
                DgvItemList.Rows(index).Cells("関税率").Value = ds3.Tables(RS).Rows(index)("関税率")
                DgvItemList.Rows(index).Cells("関税額").Value = ds3.Tables(RS).Rows(index)("関税額")
                DgvItemList.Rows(index).Cells("前払法人税率").Value = ds3.Tables(RS).Rows(index)("前払法人税率")
                DgvItemList.Rows(index).Cells("前払法人税額").Value = ds3.Tables(RS).Rows(index)("前払法人税額")
                DgvItemList.Rows(index).Cells("輸送費率").Value = ds3.Tables(RS).Rows(index)("輸送費率")
                DgvItemList.Rows(index).Cells("輸送費額").Value = ds3.Tables(RS).Rows(index)("輸送費額")
                DgvItemList.Rows(index).Cells("仕入原価").Value = ds3.Tables(RS).Rows(index)("仕入原価")
                DgvItemList.Rows(index).Cells("仕入金額").Value = ds3.Tables(RS).Rows(index)("仕入金額")

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
                DgvItemList.Rows(index).Cells("メーカー").Value = ds3.Tables(RS).Rows(index)("メーカー")
                DgvItemList.Rows(index).Cells("品名").Value = ds3.Tables(RS).Rows(index)("品名")
                DgvItemList.Rows(index).Cells("型式").Value = ds3.Tables(RS).Rows(index)("型式")
                DgvItemList.Rows(index).Cells("数量").Value = ds3.Tables(RS).Rows(index)("受注数量")
                DgvItemList.Rows(index).Cells("単位").Value = ds3.Tables(RS).Rows(index)("単位")
                DgvItemList.Rows(index).Cells("仕入先").Value = ds3.Tables(RS).Rows(index)("仕入先名")
                DgvItemList.Rows(index).Cells("仕入値").Value = ds3.Tables(RS).Rows(index)("仕入値")
                DgvItemList.Rows(index).Cells("間接費").Value = ds3.Tables(RS).Rows(index)("間接費")
                DgvItemList.Rows(index).Cells("売単価").Value = ds3.Tables(RS).Rows(index)("売単価")
                DgvItemList.Rows(index).Cells("売上金額").Value = ds3.Tables(RS).Rows(index)("売上金額")
                DgvItemList.Rows(index).Cells("粗利額").Value = ds3.Tables(RS).Rows(index)("粗利額")
                DgvItemList.Rows(index).Cells("粗利率").Value = ds3.Tables(RS).Rows(index)("粗利率")
                DgvItemList.Rows(index).Cells("リードタイム").Value = ds3.Tables(RS).Rows(index)("リードタイム")
                If ds3.Tables(RS).Rows(index)("リードタイム単位") Is DBNull.Value Then
                Else
                    Dim tmp2 As Integer = ds3.Tables(RS).Rows(index)("リードタイム単位")
                    DgvItemList("リードタイム単位", index).Value = tmp2
                End If
                DgvItemList.Rows(index).Cells("備考").Value = ds3.Tables(RS).Rows(index)("備考")
                DgvItemList.Rows(index).Cells("見積単価").Value = ds3.Tables(RS).Rows(index)("見積単価")
                DgvItemList.Rows(index).Cells("見積金額").Value = ds3.Tables(RS).Rows(index)("見積金額")
                DgvItemList.Rows(index).Cells("関税率").Value = ds3.Tables(RS).Rows(index)("関税率")
                DgvItemList.Rows(index).Cells("関税額").Value = ds3.Tables(RS).Rows(index)("関税額")
                DgvItemList.Rows(index).Cells("前払法人税率").Value = ds3.Tables(RS).Rows(index)("前払法人税率")
                DgvItemList.Rows(index).Cells("前払法人税額").Value = ds3.Tables(RS).Rows(index)("前払法人税額")
                DgvItemList.Rows(index).Cells("輸送費率").Value = ds3.Tables(RS).Rows(index)("輸送費率")
                DgvItemList.Rows(index).Cells("輸送費額").Value = ds3.Tables(RS).Rows(index)("輸送費額")
                DgvItemList.Rows(index).Cells("仕入原価").Value = ds3.Tables(RS).Rows(index)("仕入原価")
                DgvItemList.Rows(index).Cells("仕入金額").Value = ds3.Tables(RS).Rows(index)("仕入金額")
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

        If frmC01F10_Login.loginValue.Language = "ENG" Then
            LblOrderNo.Text = "OrderNumber"
            LblCustomerPO.Text = "CustomerNumber"
            LblCustomerPO.Location = New Point(273, 12)
            LblCustomerPO.Size = New Size(152, 23)
            TxtCustomerPO.Location = New Point(431, 12)
            LblOrderDate.Text = "OrderDate"
            LblOrderDate.Location = New Point(525, 12)
            DtpOrderDate.Location = New Point(643, 12)
            DtpOrderDate.Size = New Size(130, 22)

            LblRegistration.Text = "OrderingDate"
            LblRegistration.Location = New Point(779, 12)
            DtpOrderRegistration.Location = New Point(895, 12)
            DtpOrderRegistration.Size = New Size(130, 22)
            LblQuoteNo.Text = "QuotationNumber"
            LblQuoteNo.Location = New Point(11, 42)
            LblQuoteNo.Size = New Size(162, 23)
            TxtQuoteNo.Location = New Point(179, 42)
            LblHyphen.Location = New Point(273, 49)
            TxtQuoteSuffix.Location = New Point(290, 42)
            LblQuoteRegistration.Text = "QuotationRegistrationDate"
            LblQuoteRegistration.Location = New Point(325, 42)
            LblQuoteRegistration.Size = New Size(215, 23)
            DtpQuoteRegistration.Location = New Point(546, 42)
            LblQuoteDate.Text = "QuotationDate"
            LblQuoteDate.Location = New Point(702, 42)
            DtpQuoteDate.Location = New Point(820, 42)
            LblExpiration.Text = "QuotationExpirationDate"
            LblExpiration.Size = New Size(205, 23)
            LblExpiration.Location = New Point(976, 42)
            DtpExpiration.Location = New Point(1187, 42)
            LblCustomerName.Text = "CustomerName"
            LblAddress.Text = "Address"
            LblTel.Text = "TEL"
            LblFax.Text = "FAX"
            LblPerson.Text = "ContactPersonName"
            LblPosition.Text = "Position"
            LblSales.Text = "SalesPerson"
            LblInput.Text = "InputPerson"
            LblPaymentTerms.Text = "PaymentTerms"
            TxtPaymentTerms.Location = New Point(181, 187)
            LblPaymentTerms.Size = New Size(162, 23)
            LblRemarks.Text = "QuotationRemarks"
            TxtQuoteRemarks.Location = New Point(181, 216)
            LblRemarks.Size = New Size(162, 23)
            LblItemCount.Text = "ItemCount"
            LblOrderRemarks.Text = "OrderRemarks"
            LblOrderRemarks.Size = New Size(161, 23)
            LblOrderRemarks.Location = New Point(11, 422)
            TxtOrderRemark.Location = New Point(178, 422)
            LblVat.Text = "VAT"
            TxtVat.Size = New Size(151, 23)
            LblOrderAmount.Text = "OrderAmount"
            LblOrderAmount.Size = New Size(180, 23)
            LblOrderAmount.Location = New Point(923, 422)
            LblPurchaseAmount.Text = "PurchaseOrderAmount"
            LblPurchaseAmount.Size = New Size(180, 23)
            LblPurchaseAmount.Location = New Point(923, 451)
            LblGrossProfit.Text = "GrossProfit"
            LblGrossProfit.Size = New Size(180, 23)
            LblGrossProfit.Location = New Point(923, 480)

            BtnRegistration.Text = "Registrartion"
            BtnBack.Text = "Back"

            DgvItemList.Columns("仕入区分").HeaderText = "PurchaseSection"
            DgvItemList.Columns("メーカー").HeaderText = "Maker"
            DgvItemList.Columns("品名").HeaderText = "Item"
            DgvItemList.Columns("型式").HeaderText = "Model"
            DgvItemList.Columns("数量").HeaderText = "Quantity"
            DgvItemList.Columns("単位").HeaderText = "Unit"
            DgvItemList.Columns("仕入先").HeaderText = "SupplierName"
            DgvItemList.Columns("仕入値").HeaderText = "PurchasePrice"
            DgvItemList.Columns("仕入原価").HeaderText = "PurchsingCost"
            DgvItemList.Columns("関税率").HeaderText = "TariffRate"
            DgvItemList.Columns("関税額").HeaderText = "CustomsDuty"
            DgvItemList.Columns("前払法人税率").HeaderText = "PrepaidCorporateTaxRate"
            DgvItemList.Columns("前払法人税額").HeaderText = "PrepaidCorporateTaxAmount"
            DgvItemList.Columns("輸送費率").HeaderText = "TransportationCostRate"
            DgvItemList.Columns("輸送費額").HeaderText = "TransportationCost"
            DgvItemList.Columns("仕入金額").HeaderText = "PurchaseAmount"
            DgvItemList.Columns("売単価").HeaderText = "SellingPrice"
            DgvItemList.Columns("売上金額").HeaderText = "SalesAmount"
            DgvItemList.Columns("見積単価").HeaderText = "QuotetionPrice"
            DgvItemList.Columns("見積金額").HeaderText = "QuotetionAmount"
            DgvItemList.Columns("粗利額").HeaderText = "GrossProfit"
            DgvItemList.Columns("粗利率").HeaderText = "GrossMarginRate(%)"
            DgvItemList.Columns("リードタイム").HeaderText = "LeadTime"
            DgvItemList.Columns("リードタイム単位").HeaderText = "LeadTimeUnit"
            DgvItemList.Columns("備考").HeaderText = "Remarks"
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
                Sql2 += "会社コード, 受注番号, 受注番号枝番, 行番号, 仕入区分, メーカー, 品名, 型式, 単位, 仕入先名, 仕入値, 受注数量, 売上数量, 受注残数, 間接費, 売単価, 売上金額, 粗利額, 粗利率, リードタイム, リードタイム単位, 出庫数, 未出庫数, 備考, 更新者, 登録日, 見積単価, 見積金額, 関税率, 関税額, 前払法人税率, 前払法人税額, 輸送費率, 輸送費額, 仕入原価, 仕入金額)"
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
                Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("リードタイム単位").Value.ToString
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

                Sql2 += "', '"
                Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("見積単価").Value.ToString
                Sql2 += "', '"
                Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("見積金額").Value.ToString
                Sql2 += "', '"
                Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("関税率").Value.ToString
                Sql2 += "', '"
                Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("関税額").Value.ToString
                Sql2 += "', '"
                Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("前払法人税率").Value.ToString
                Sql2 += "', '"
                Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("前払法人税額").Value.ToString
                Sql2 += "', '"
                Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("輸送費率").Value.ToString
                Sql2 += "', '"
                Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("輸送費額").Value.ToString
                Sql2 += "', '"
                Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("仕入原価").Value.ToString
                Sql2 += "', '"
                Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("仕入金額").Value.ToString
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
                Sql2 += ", "
                Sql2 += "見積単価"
                Sql2 += ", "
                Sql2 += "見積金額"
                Sql2 += ", "
                Sql2 += "関税率"
                Sql2 += ", "
                Sql2 += "関税額"
                Sql2 += ", "
                Sql2 += "前払法人税率"
                Sql2 += ", "
                Sql2 += "前払法人税額"
                Sql2 += ", "
                Sql2 += "輸送費率"
                Sql2 += ", "
                Sql2 += "輸送費額"
                Sql2 += ", "
                Sql2 += "仕入原価"
                Sql2 += ", "
                Sql2 += "仕入金額"

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