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

        'セルの内容に合わせて、行の高さが自動的に調節されるようにする
        DgvItemList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        '"Column1"列のセルのテキストを折り返して表示する
        DgvItemList.Columns("型式").DefaultCellStyle.WrapMode = DataGridViewTriState.True

        'DataGridViewComboBoxColumnを作成
        Dim column As New DataGridViewComboBoxColumn()
        'DataGridViewComboBoxColumnのDataSourceを設定
        column.DataSource = getSireKbn()
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

        Sql12 += "SELECT * FROM public.m90_hanyo"
        Sql12 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql12 += " AND 固定キー = '" & CommonConst.FIXED_KEY_READTIME & "'"

        Dim ds12 As DataSet = _db.selectDB(Sql12, RS, reccnt)

        Dim table2 As New DataTable("Table")
        table2.Columns.Add("Display", GetType(String))
        table2.Columns.Add("Value", GetType(Integer))

        For index As Integer = 0 To ds12.Tables(RS).Rows.Count - 1
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                table2.Rows.Add(ds12.Tables(RS).Rows(index)("文字２"), ds12.Tables(RS).Rows(index)("可変キー"))
            Else
                table2.Rows.Add(ds12.Tables(RS).Rows(index)("文字１"), ds12.Tables(RS).Rows(index)("可変キー"))
            End If
        Next

        Dim column2 As New DataGridViewComboBoxColumn()
        column2.DataSource = table2
        column2.ValueMember = "Value"
        column2.DisplayMember = "Display"
        column2.HeaderText = "リードタイム単位"
        column2.Name = "リードタイム単位"

        DgvItemList.Columns.Insert(25, column2)

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            If OrderStatus = CommonConst.STATUS_ADD Then
                LblMode.Text = "NewRegistrationMode"
            ElseIf OrderStatus = CommonConst.STATUS_EDIT Then
                LblMode.Text = "EditMode"
            ElseIf OrderStatus = CommonConst.STATUS_CLONE Then
                LblMode.Text = "NewCopyMode"
            ElseIf OrderStatus = CommonConst.STATUS_VIEW Then
                LblMode.Text = "ViewMode"
            End If
        Else
            If OrderStatus = CommonConst.STATUS_ADD Then
                LblMode.Text = "新規登録モード"
            ElseIf OrderStatus = CommonConst.STATUS_EDIT Then
                LblMode.Text = "編集モード"
            ElseIf OrderStatus = CommonConst.STATUS_CLONE Then
                LblMode.Text = "新規複写モード"
            ElseIf OrderStatus = CommonConst.STATUS_VIEW Then
                LblMode.Text = "参照モード"
            End If
        End If

        Dim dtNow As DateTime = DateTime.Now

        '受注登録モード
        '現在未使用 201904
        '
        If OrderStatus = CommonConst.STATUS_ADD Then
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
            SqlSaiban += "FROM public.m80_saiban"
            SqlSaiban += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            SqlSaiban += " AND 採番キー = '20'"

            Dim Saiban1 As DataSet = _db.selectDB(SqlSaiban, RS, reccnt)

            Dim Sql1 As String = ""
            Sql1 += "SELECT * FROM public.t01_mithd"
            Sql1 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql1 += " AND 見積番号 = '" & OrderNo.ToString & "'"
            Sql1 += " AND 見積番号枝番 = '" & OrderSuffix.ToString & "'"

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
                TxtAddress1.Text = ds1.Tables(RS).Rows(0)("得意先住所")
            End If
            If ds1.Tables(RS).Rows(0)("得意先電話番号") IsNot DBNull.Value Then
                TxtTel.Text = ds1.Tables(RS).Rows(0)("得意先電話番号")
            End If
            If ds1.Tables(RS).Rows(0)("得意先ＦＡＸ") IsNot DBNull.Value Then
                TxtFax.Text = ds1.Tables(RS).Rows(0)("得意先ＦＡＸ")
            End If
            If ds1.Tables(RS).Rows(0)("営業担当者") IsNot DBNull.Value Then
                TxtSales.Tag = ds1.Tables(RS).Rows(0)("営業担当者コード")
                TxtSales.Text = ds1.Tables(RS).Rows(0)("営業担当者")
            End If
            If ds1.Tables(RS).Rows(0)("入力担当者") IsNot DBNull.Value Then
                TxtInput.Tag = ds1.Tables(RS).Rows(0)("入力担当者コード")
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

            '見積明細情報
            Dim Sql3 As String = ""
            Sql3 += "SELECT * FROM public.t02_mitdt"
            Sql3 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql3 += " AND 見積番号 = '" & OrderNo.ToString & "'"
            Sql3 += " AND 見積番号枝番 = '" & OrderSuffix.ToString & "'"
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

            '明細は操作させない
            DgvItemList.Columns("仕入区分").ReadOnly = True
            DgvItemList.Columns("メーカー").ReadOnly = True
            DgvItemList.Columns("品名").ReadOnly = True
            DgvItemList.Columns("型式").ReadOnly = True
            DgvItemList.Columns("数量").ReadOnly = True
            DgvItemList.Columns("単位").ReadOnly = True
            DgvItemList.Columns("仕入先").ReadOnly = True
            DgvItemList.Columns("仕入値").ReadOnly = True
            DgvItemList.Columns("間接費").ReadOnly = True
            DgvItemList.Columns("売単価").ReadOnly = True
            DgvItemList.Columns("売上金額").ReadOnly = True
            DgvItemList.Columns("粗利額").ReadOnly = True
            DgvItemList.Columns("粗利率").ReadOnly = True
            DgvItemList.Columns("見積単価").ReadOnly = True
            DgvItemList.Columns("見積金額").ReadOnly = True
            DgvItemList.Columns("関税率").ReadOnly = True
            DgvItemList.Columns("関税額").ReadOnly = True
            DgvItemList.Columns("前払法人税率").ReadOnly = True
            DgvItemList.Columns("前払法人税額").ReadOnly = True
            DgvItemList.Columns("輸送費率").ReadOnly = True
            DgvItemList.Columns("輸送費額").ReadOnly = True
            DgvItemList.Columns("仕入原価").ReadOnly = True

        Else
            '受注編集、受注参照（受注複写は未使用 201904）
            '
            '受注基本情報

            Dim Sql1 As String = ""
            Sql1 += "Select * FROM Public.t10_cymnhd"
            Sql1 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql1 += " AND 受注番号 = '" & OrderNo.ToString & "'"
            Sql1 += " AND 受注番号枝番 = '" & OrderSuffix.ToString & "'"

            Dim ds1 = _db.selectDB(Sql1, RS, reccnt)

            Dim Sql2 As String = ""
            Sql2 += "SELECT * FROM public.t10_cymnhd"
            Sql2 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql2 += " AND 受注番号 = '" & ds1.Tables(RS).Rows(0)("受注番号") & "'"
            Dim ds2 As DataSet = _db.selectDB(Sql2, RS, reccnt)

            Dim MaxSuffix As Integer = 0
            For index As Integer = 0 To ds2.Tables(RS).Rows.Count - 1
                If MaxSuffix < ds2.Tables(RS).Rows(index)("受注番号枝番") Then
                    MaxSuffix = ds2.Tables(RS).Rows(index)("受注番号枝番")
                End If
            Next

            CompanyCode = ds1.Tables(RS).Rows(0)("会社コード")
            If OrderStatus = CommonConst.STATUS_CLONE Then
                Dim SqlSaiban As String = ""
                SqlSaiban += "SELECT "
                SqlSaiban += "会社コード, "
                SqlSaiban += "採番キー, "
                SqlSaiban += "最新値, "
                SqlSaiban += "最小値, "
                SqlSaiban += "最大値, "
                SqlSaiban += "接頭文字, "
                SqlSaiban += "連番桁数 "
                SqlSaiban += "FROM public.m80_saiban"
                SqlSaiban += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                SqlSaiban += " AND 採番キー = '20'"

                Dim Saiban1 As DataSet = _db.selectDB(SqlSaiban, RS, reccnt)

                CompanyCode = Saiban1.Tables(RS).Rows(0)("会社コード")
                Dim NewOrderNo As String = Saiban1.Tables(RS).Rows(0)("接頭文字")
                NewOrderNo += dtNow.ToString("MMdd")
                OrderCount = Saiban1.Tables(RS).Rows(0)("最新値")
                Dim TmpOrderCount As String = Saiban1.Tables(RS).Rows(0)("最新値")
                NewOrderNo += TmpOrderCount.PadLeft(Saiban1.Tables(RS).Rows(0)("連番桁数"), "0")

                SaibanSave()

                TxtOrderNo.Text = NewOrderNo
            Else
                If ds1.Tables(RS).Rows(0)("受注番号") IsNot DBNull.Value Then
                    TxtOrderNo.Text = ds1.Tables(RS).Rows(0)("受注番号")
                End If
            End If

            '枝番設定
            If OrderStatus = CommonConst.STATUS_CLONE Then
                '複写モード
                TxtOrderSuffix.Text = 1
            ElseIf OrderStatus = CommonConst.STATUS_EDIT Then
                '編集モード
                TxtOrderSuffix.Text = MaxSuffix + 1
            Else
                '参照モード
                If ds1.Tables(RS).Rows(0)("受注番号枝番") IsNot DBNull.Value Then
                    TxtOrderSuffix.Text = ds1.Tables(RS).Rows(0)("受注番号枝番")
                End If
            End If

            '受注日設定
            If OrderStatus = CommonConst.STATUS_CLONE Then
                '複写モード
                DtpOrderDate.Value = dtNow
            Else
                '編集・参照モード
                If ds1.Tables(RS).Rows(0)("受注日") IsNot DBNull.Value Then
                    DtpOrderDate.Value = ds1.Tables(RS).Rows(0)("受注日")
                End If
            End If

            '登録日設定
            If OrderStatus = CommonConst.STATUS_CLONE Then
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
                TxtAddress1.Text = ds1.Tables(RS).Rows(0)("得意先住所")
            End If
            If ds1.Tables(RS).Rows(0)("得意先電話番号") IsNot DBNull.Value Then
                TxtTel.Text = ds1.Tables(RS).Rows(0)("得意先電話番号")
            End If
            If ds1.Tables(RS).Rows(0)("得意先ＦＡＸ") IsNot DBNull.Value Then
                TxtFax.Text = ds1.Tables(RS).Rows(0)("得意先ＦＡＸ")
            End If
            If ds1.Tables(RS).Rows(0)("営業担当者") IsNot DBNull.Value Then
                TxtSales.Tag = ds1.Tables(RS).Rows(0)("営業担当者コード")
                TxtSales.Text = ds1.Tables(RS).Rows(0)("営業担当者")
            End If
            If ds1.Tables(RS).Rows(0)("入力担当者") IsNot DBNull.Value Then
                TxtInput.Tag = ds1.Tables(RS).Rows(0)("入力担当者コード")
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

            '見積明細情報
            Dim Sql3 As String = ""
            Sql3 += "SELECT * FROM public.t11_cymndt"
            Sql3 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql3 += " AND 受注番号 = '" & OrderNo.ToString & "'"
            Sql3 += " AND 受注番号枝番 = '" & OrderSuffix.ToString & "'"
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
            If OrderStatus = CommonConst.STATUS_VIEW Then
                DtpOrderDate.Enabled = False
                TxtOrderRemark.Enabled = False
                DtpQuoteDate.Enabled = False
                DtpExpiration.Enabled = False
                DgvItemList.ReadOnly = True
                BtnRegistration.Visible = False
            End If
        End If

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
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
            LblTel.Text = "PhoneNumber"
            LblFax.Text = "FAX"
            LblPerson.Text = "NameOfPIC"
            LblPosition.Text = "PositionPICCustomer"
            LblSales.Text = "SalesPersonInCharge"
            LblInput.Text = "PICForInputting"
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
            LblOrderAmount.Text = "JobOrderAmount"
            LblOrderAmount.Size = New Size(180, 23)
            LblOrderAmount.Location = New Point(923, 422)
            LblPurchaseAmount.Text = "PurchaseOrderAmount"
            LblPurchaseAmount.Size = New Size(180, 23)
            LblPurchaseAmount.Location = New Point(923, 451)
            LblGrossProfit.Text = "GrossMargin"
            LblGrossProfit.Size = New Size(180, 23)
            LblGrossProfit.Location = New Point(923, 480)

            BtnRegistration.Text = "Registrartion"
            BtnBack.Text = "Back"

            DgvItemList.Columns("仕入区分").HeaderText = "PurchasingClassification"
            DgvItemList.Columns("メーカー").HeaderText = "Manufacturer"
            DgvItemList.Columns("品名").HeaderText = "ItemName"
            DgvItemList.Columns("型式").HeaderText = "Spec"
            DgvItemList.Columns("数量").HeaderText = "Quantity"
            DgvItemList.Columns("単位").HeaderText = "Unit"
            DgvItemList.Columns("仕入先").HeaderText = "SupplierName"
            DgvItemList.Columns("仕入値").HeaderText = "PurchaseAmount"
            DgvItemList.Columns("仕入原価").HeaderText = "PurchsingCost"
            DgvItemList.Columns("関税率").HeaderText = "CustomsDutyRate"
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
            DgvItemList.Columns("粗利額").HeaderText = "GrossMargin"
            DgvItemList.Columns("粗利率").HeaderText = "GrossMarginRate(%)"
            DgvItemList.Columns("リードタイム").HeaderText = "LeadTime"
            DgvItemList.Columns("リードタイム単位").HeaderText = "LeadTimeUnit"
            DgvItemList.Columns("備考").HeaderText = "Remarks"
        End If

        calcTotal() '合計値の計算

        AddHandler DgvItemList.CellValueChanged, AddressOf DgvItemList_CellValueChanged 'イベントハンドラーをセット
    End Sub

    Private Sub SaibanSave()
        Dim dtNow As String = UtilClass.formatDatetime(DateTime.Now)
        OrderCount += 1
        Dim Saiban3 As String = ""
        Saiban3 += "UPDATE Public.m80_saiban "
        Saiban3 += "SET  最新値 = '" & OrderCount.ToString & "'"
        Saiban3 += " , 更新者 = 'Admin'"
        Saiban3 += " , 更新日 = '" & dtNow & "'"
        Saiban3 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Saiban3 += " AND 会社コード ='" & CompanyCode.ToString & "'"
        Saiban3 += " AND 採番キー ='20' "

        _db.executeDB(Saiban3)
    End Sub

    '前の画面に戻る
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _ParentForm.Enabled = True
        _ParentForm.Show()
        Me.Dispose()
    End Sub

    '登録ボタン押下時
    Private Sub BtnRegistration_Click(sender As Object, e As EventArgs) Handles BtnRegistration.Click
        Dim reccnt As Integer = 0
        Dim dtNow As String = UtilClass.formatDatetime(DateTime.Now)

        '登録、編集、複写モードの時
        If OrderStatus = CommonConst.STATUS_ADD Or OrderStatus = CommonConst.STATUS_EDIT Or OrderStatus = CommonConst.STATUS_CLONE Then

            Try
                Dim Sql1 As String = ""
                Sql1 = ""
                Sql1 += "INSERT INTO "
                Sql1 += "Public."
                Sql1 += "t10_cymnhd("
                Sql1 += "会社コード, 受注番号, 受注番号枝番, 客先番号, 見積番号, 見積番号枝番, 得意先コード"
                Sql1 += ", 得意先名, 得意先郵便番号, 得意先住所, 得意先電話番号, 得意先ＦＡＸ, 得意先担当者役職"
                Sql1 += ", 得意先担当者名, 見積日, 見積有効期限, 支払条件, 見積金額, 仕入金額, 粗利額, 営業担当者"
                Sql1 += ", 営業担当者コード, 入力担当者, 入力担当者コード, 備考, 見積備考, ＶＡＴ, 受注日, 登録日, 更新日, 更新者, 取消区分)"
                Sql1 += " VALUES('"
                Sql1 += CompanyCode '会社コード
                Sql1 += "', '"
                Sql1 += TxtOrderNo.Text '受注番号
                Sql1 += "', '"
                Sql1 += TxtOrderSuffix.Text '受注番号枝番
                Sql1 += "', '"
                Sql1 += RevoveChars(TxtCustomerPO.Text) '客先番号
                Sql1 += "', '"
                Sql1 += TxtQuoteNo.Text '見積番号
                Sql1 += "', '"
                Sql1 += TxtQuoteSuffix.Text '見積番号枝番
                Sql1 += "', '"
                Sql1 += TxtCustomerCode.Text '得意先コード
                Sql1 += "', '"
                Sql1 += TxtCustomerName.Text '得意先名
                Sql1 += "', '"
                Sql1 += TxtPostalCode.Text '得意先郵便番号
                Sql1 += "', '"
                Sql1 += TxtAddress1.Text '得意先住所
                Sql1 += "', '"
                Sql1 += TxtTel.Text '得意先電話番号
                Sql1 += "', '"
                Sql1 += TxtFax.Text '得意先ＦＡＸ
                Sql1 += "', '"
                Sql1 += TxtPosition.Text '得意先担当者役職
                Sql1 += "', '"
                Sql1 += TxtPerson.Text '得意先担当者名
                Sql1 += "', '"
                Sql1 += UtilClass.strFormatDate(DtpQuoteDate.Value) '見積日
                Sql1 += "', '"
                Sql1 += UtilClass.strFormatDate(DtpExpiration.Value) '見積有効期限
                Sql1 += "', '"
                Sql1 += TxtPaymentTerms.Text '支払条件
                Sql1 += "', '"
                Sql1 += UtilClass.formatNumber(TxtOrderAmount.Text) '見積金額
                Sql1 += "', '"
                Sql1 += UtilClass.formatNumber(TxtPurchaseAmount.Text) '仕入金額
                Sql1 += "', '"
                Sql1 += UtilClass.formatNumber(TxtGrossProfit.Text) '粗利額
                Sql1 += "', '"
                Sql1 += TxtSales.Text '営業担当者
                Sql1 += "', '"
                Sql1 += TxtSales.Tag '営業担当者コード
                Sql1 += "', '"
                Sql1 += TxtInput.Text '入力担当者
                Sql1 += "', '"
                Sql1 += TxtInput.Tag '入力担当者コード
                Sql1 += "', '"
                Sql1 += RevoveChars(TxtOrderRemark.Text) '備考
                Sql1 += "', '"
                Sql1 += TxtQuoteRemarks.Text '見積備考
                Sql1 += "', '"
                Sql1 += UtilClass.formatNumber(TxtVat.Text) 'ＶＡＴ
                Sql1 += "', '"
                Sql1 += UtilClass.strFormatDate(DtpOrderDate.Value) '受注日
                Sql1 += "', '"
                Sql1 += UtilClass.strFormatDate(DtpOrderRegistration.Value) '登録日
                Sql1 += "', '"
                Sql1 += dtNow '更新日
                Sql1 += "', '"
                Sql1 += frmC01F10_Login.loginValue.TantoNM '更新者
                Sql1 += "', '"
                Sql1 += "0" '取消区分
                Sql1 += "')"

                _db.executeDB(Sql1)

                Dim Sql2 As String = ""
                For cymnhdIdx As Integer = 0 To DgvItemList.Rows.Count - 1
                    Sql2 = ""
                    Sql2 += "INSERT INTO "
                    Sql2 += "Public."
                    Sql2 += "t11_cymndt("
                    Sql2 += "会社コード, 受注番号, 受注番号枝番, 行番号, 仕入区分, メーカー, 品名, 型式, 単位"
                    Sql2 += ", 仕入先名, 仕入値, 受注数量, 売上数量, 受注残数, 間接費, 売単価, 売上金額, 粗利額"
                    Sql2 += ", 粗利率, リードタイム, リードタイム単位, 出庫数, 未出庫数, 備考, 更新者, 登録日"
                    Sql2 += ", 見積単価, 見積金額, 関税率, 関税額, 前払法人税率, 前払法人税額, 輸送費率, 輸送費額, 仕入原価, 仕入金額)"
                    Sql2 += " VALUES('"
                    Sql2 += CompanyCode '会社コード
                    Sql2 += "', '"
                    Sql2 += TxtOrderNo.Text '受注番号
                    Sql2 += "', '"
                    Sql2 += TxtOrderSuffix.Text '受注番号枝番
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("No").Value.ToString '行番号
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("仕入区分").Value.ToString '仕入区分
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("メーカー").Value.ToString 'メーカー
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("品名").Value.ToString '品名
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("型式").Value.ToString '型式
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("単位").Value.ToString '単位
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("仕入先").Value.ToString '仕入先名
                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(cymnhdIdx).Cells("仕入値").Value.ToString) '仕入値
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("数量").Value.ToString '受注数量
                    Sql2 += "', '"
                    Sql2 += "0" '売上数量
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("数量").Value.ToString '受注残数
                    Sql2 += "', '"

                    If DgvItemList.Rows(cymnhdIdx).Cells("間接費").Value.ToString = "" Then
                        Sql2 += "0" '間接費
                    Else
                        Sql2 += UtilClass.formatNumber(DgvItemList.Rows(cymnhdIdx).Cells("間接費").Value.ToString) '間接費
                    End If
                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(cymnhdIdx).Cells("売単価").Value.ToString) '売単価
                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(cymnhdIdx).Cells("売上金額").Value.ToString) '売上金額
                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(cymnhdIdx).Cells("粗利額").Value.ToString) '粗利額
                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(cymnhdIdx).Cells("粗利率").Value.ToString) '粗利率
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("リードタイム").Value.ToString 'リードタイム
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("リードタイム単位").Value.ToString 'リードタイム単位
                    Sql2 += "', '"
                    Sql2 += "0" '出庫数
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(cymnhdIdx).Cells("数量").Value.ToString '未出庫数
                    Sql2 += "', '"
                    Sql2 += RevoveChars(DgvItemList.Rows(cymnhdIdx).Cells("備考").Value.ToString) '備考
                    Sql2 += "', '"
                    Sql2 += frmC01F10_Login.loginValue.TantoNM '更新者
                    Sql2 += "', '"
                    Sql2 += dtNow '登録日

                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(cymnhdIdx).Cells("見積単価").Value.ToString) '見積単価
                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(cymnhdIdx).Cells("見積金額").Value.ToString) '見積金額
                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(cymnhdIdx).Cells("関税率").Value.ToString) '関税率
                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(cymnhdIdx).Cells("関税額").Value.ToString) '関税額
                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(cymnhdIdx).Cells("前払法人税率").Value.ToString) '前払法人税率
                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(cymnhdIdx).Cells("前払法人税額").Value.ToString) '前払法人税額
                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(cymnhdIdx).Cells("輸送費率").Value.ToString) '輸送費率
                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(cymnhdIdx).Cells("輸送費額").Value.ToString) '輸送費額
                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(cymnhdIdx).Cells("仕入原価").Value.ToString) '仕入原価
                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(cymnhdIdx).Cells("仕入金額").Value.ToString) '仕入金額
                    Sql2 += "')"

                    _db.executeDB(Sql2)

                    Sql2 = ""
                Next
                Dim Sql As String = ""
                '呼び出した見積に受注日を入れる
                Sql = ""
                Sql += "UPDATE Public.t01_mithd "
                Sql += "SET  受注日 = '" & UtilClass.strFormatDate(DtpOrderDate.Value) & "'"
                Sql += ", 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
                Sql += ", 更新日 = '" & dtNow & "'"
                Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " AND 見積番号 = '" & TxtQuoteNo.Text & "'"
                Sql += " AND 見積番号枝番 = '" & TxtQuoteSuffix.Text & "'"

                _db.executeDB(Sql)


                '受注編集時に登録した場合、一つ前の枝番を取り消す
                If OrderStatus = CommonConst.STATUS_EDIT Then

                    Sql = "UPDATE t10_cymnhd SET "
                    Sql += " 取消日 = '" & dtNow & "'"
                    Sql += " ,取消区分 = " & CommonConst.CANCEL_KBN_DISABLED.ToString
                    Sql += " WHERE "
                    Sql += " 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                    Sql += " AND "
                    Sql += " 受注番号 = '" & OrderNo & "'"
                    Sql += " AND "
                    Sql += " 受注番号枝番 = '" & OrderSuffix & "'"

                    _db.executeDB(Sql)

                End If

            Catch ue As UsrDefException
                ue.dspMsg()
                Throw ue
            Catch ex As Exception
                'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
            End Try

        Else
            Try

                Dim Sql1 As String = ""
                Sql1 = ""
                Sql1 += "UPDATE Public.t10_cymnhd "
                Sql1 += "SET 客先番号 = '" & RevoveChars(TxtCustomerPO.Text) & "'"
                Sql1 += " , 備考 = '" & RevoveChars(TxtOrderRemark.Text) & "'"
                Sql1 += " , 受注日 = '" & DtpOrderDate.Value & "'"
                Sql1 += " , 更新日 = '" & dtNow & "'"
                Sql1 += " , 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"

                Sql1 += " WHERE 会社コード ='" & CompanyCode & "'"
                Sql1 += " AND 受注番号 ='" & OrderNo & "'"
                Sql1 += " AND 受注番号枝番 ='" & OrderSuffix & "'"

                _db.executeDB(Sql1)

            Catch ue As UsrDefException
                ue.dspMsg()
                Throw ue
            Catch ex As Exception
                'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
            End Try
        End If

        _ParentForm.Enabled = True
        _ParentForm.Show()
        Me.Dispose()

    End Sub
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

    'Return: DataTable
    Private Function getSireKbn() As DataTable
        Dim Sql As String = ""
        Dim strViewText As String = ""

        Sql = " AND "
        Sql += "固定キー ILIKE '" & CommonConst.FIXED_KEY_PURCHASING_CLASS & "'"
        Sql += " ORDER BY 表示順"

        'リードタイムのリストを汎用マスタから取得
        Dim dsHanyo As DataSet = getDsData("m90_hanyo", Sql)

        'ComboBoxに表示する項目のリストを作成する
        Dim table As New DataTable("Table")
        table.Columns.Add("Display", GetType(String))
        table.Columns.Add("Value", GetType(Integer))

        strViewText = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "文字２", "文字１")

        For x As Integer = 0 To dsHanyo.Tables(RS).Rows.Count - 1
            table.Rows.Add(dsHanyo.Tables(RS).Rows(x)(strViewText), dsHanyo.Tables(RS).Rows(x)("可変キー"))
        Next

        Return table
    End Function

    '一覧内テキスト変更イベント
    Private Sub DgvItemList_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs)
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        '合計値リセット
        TxtPurchaseAmount.Clear()
        TxtOrderAmount.Clear()
        TxtGrossProfit.Clear()

        Dim tmp As Decimal = 0
        Dim tmp1 As Decimal = 0
        Dim tmp2 As Decimal = 0
        Dim tmp3 As Decimal = 0
        Dim tmp4 As Decimal = 0

        If DgvItemList.Rows.Count = 0 Then
            Exit Sub
        End If

        '各項目の属性チェック
        If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("数量").Value) And (DgvItemList.Rows(e.RowIndex).Cells("数量").Value IsNot Nothing) Then
            MessageBox.Show("Please enter with numeric value.", "Quantity Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            DgvItemList.Rows(e.RowIndex).Cells("数量").Value = Nothing
            Exit Sub
        End If
        If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("仕入値").Value) And (DgvItemList.Rows(e.RowIndex).Cells("仕入値").Value IsNot Nothing) Then
            MessageBox.Show("Please enter with numeric value.", "PurchaseUnitPrice Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            DgvItemList.Rows(e.RowIndex).Cells("仕入値").Value = Nothing
            Exit Sub
        End If
        If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value) And (DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value IsNot Nothing) Then
            MessageBox.Show("Please enter with numeric value.", "PurchasingCost Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value = Nothing
            Exit Sub
        End If
        If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("関税率").Value) And (DgvItemList.Rows(e.RowIndex).Cells("関税率").Value IsNot Nothing) Then
            MessageBox.Show("Please enter with numeric value.", "CustomsDutyRate Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            DgvItemList.Rows(e.RowIndex).Cells("関税率").Value = Nothing
            Exit Sub
        End If
        If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("関税額").Value) And (DgvItemList.Rows(e.RowIndex).Cells("関税額").Value IsNot Nothing) Then
            MessageBox.Show("Please enter with numeric value.", "CustomsDuty Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            DgvItemList.Rows(e.RowIndex).Cells("関税額").Value = Nothing
            Exit Sub
        End If
        If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("前払法人税率").Value) And (DgvItemList.Rows(e.RowIndex).Cells("前払法人税率").Value IsNot Nothing) Then
            MessageBox.Show("Please enter with numeric value.", "PrepaidCorporateTaxRate Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            DgvItemList.Rows(e.RowIndex).Cells("前払法人税率").Value = Nothing
            Exit Sub
        End If
        If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value) And (DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value IsNot Nothing) Then
            MessageBox.Show("Please enter with numeric value.", "PrepaidCorporateTaxAmount Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value = Nothing
            Exit Sub
        End If
        If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("輸送費率").Value) And (DgvItemList.Rows(e.RowIndex).Cells("輸送費率").Value IsNot Nothing) Then
            MessageBox.Show("Please enter with numeric value.", "TransportationCostRate Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            DgvItemList.Rows(e.RowIndex).Cells("輸送費率").Value = Nothing
            Exit Sub
        End If
        If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value) And (DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value IsNot Nothing) Then
            MessageBox.Show("Please enter with numeric value.", "TransportationCost Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value = Nothing
            Exit Sub
        End If
        If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("仕入金額").Value) And (DgvItemList.Rows(e.RowIndex).Cells("仕入金額").Value IsNot Nothing) Then
            MessageBox.Show("Please enter with numeric value.", "PurchaseAmount Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            DgvItemList.Rows(e.RowIndex).Cells("仕入金額").Value = Nothing
            Exit Sub
        End If
        If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("売単価").Value) And (DgvItemList.Rows(e.RowIndex).Cells("売単価").Value IsNot Nothing) Then
            MessageBox.Show("Please enter with numeric value.", "SellingPrice Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            DgvItemList.Rows(e.RowIndex).Cells("売単価").Value = Nothing
            Exit Sub
        End If
        If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value) And (DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value IsNot Nothing) Then
            MessageBox.Show("Please enter with numeric value.", "SalesAmount Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value = Nothing
            Exit Sub
        End If
        If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value) And (DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value IsNot Nothing) Then
            MessageBox.Show("Please enter with numeric value.", "QuotetionPrice Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value = Nothing
            Exit Sub
        End If
        If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("見積金額").Value) And (DgvItemList.Rows(e.RowIndex).Cells("見積金額").Value IsNot Nothing) Then
            MessageBox.Show("Please enter with numeric value.", "QuotetionAmount Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            DgvItemList.Rows(e.RowIndex).Cells("見積金額").Value = Nothing
            Exit Sub
        End If
        If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value) And (DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value IsNot Nothing) Then
            MessageBox.Show("Please enter with numeric value.", "GrossMargin Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value = Nothing
            Exit Sub
        End If
        If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("粗利率").Value) And (DgvItemList.Rows(e.RowIndex).Cells("粗利率").Value IsNot Nothing) Then
            MessageBox.Show("Please enter with numeric value.", "GrossMarginRate(%) Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            DgvItemList.Rows(e.RowIndex).Cells("粗利率").Value = Nothing
            Exit Sub
        End If

        Try
            '計算式を各行に適用
            If e.RowIndex > -1 Then

                '--------------------------
                '単価入力 or 粗利入力
                '--------------------------

                '操作したカラム名を取得
                Dim currentColumn As String = DgvItemList.Columns(e.ColumnIndex).Name

                '仕入値 <> Nothing
                '--------------------------
                If currentColumn = "仕入値" And DgvItemList.Rows(e.RowIndex).Cells("仕入値").Value IsNot Nothing Then

                    '数量 <> Nothing
                    If DgvItemList.Rows(e.RowIndex).Cells("数量").Value IsNot Nothing Then
                        DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value = DgvItemList.Rows(e.RowIndex).Cells("仕入値").Value * DgvItemList.Rows(e.RowIndex).Cells("数量").Value
                    End If
                    '関税率 <> Nothing
                    If DgvItemList.Rows(e.RowIndex).Cells("関税率").Value IsNot Nothing Then
                        DgvItemList.Rows(e.RowIndex).Cells("関税額").Value = DgvItemList.Rows(e.RowIndex).Cells("仕入値").Value * DgvItemList.Rows(e.RowIndex).Cells("関税率").Value
                    End If
                    '前払法人税率, 関税額 <> Nothing
                    If DgvItemList.Rows(e.RowIndex).Cells("前払法人税率").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("関税額").Value IsNot Nothing Then
                        tmp = DgvItemList.Rows(e.RowIndex).Cells("仕入値").Value + DgvItemList.Rows(e.RowIndex).Cells("関税額").Value
                        tmp1 = tmp * DgvItemList.Rows(e.RowIndex).Cells("前払法人税率").Value
                        tmp1 = Math.Ceiling(tmp1)
                        DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value = tmp1
                    End If
                    '輸送費率 <> Nothing
                    If DgvItemList.Rows(e.RowIndex).Cells("輸送費率").Value IsNot Nothing Then
                        DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value = DgvItemList.Rows(e.RowIndex).Cells("仕入値").Value * DgvItemList.Rows(e.RowIndex).Cells("輸送費率").Value
                    End If

                End If

                '関税額, 前払法人税額, 輸送費額 <> Nothing
                '--------------------------
                If DgvItemList.Rows(e.RowIndex).Cells("関税額").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value IsNot Nothing Then
                    '仕入原価 <> Nothing
                    If DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value IsNot Nothing Then
                        DgvItemList.Rows(e.RowIndex).Cells("仕入金額").Value = DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value + (DgvItemList.Rows(e.RowIndex).Cells("関税額").Value + DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value + DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value) * DgvItemList.Rows(e.RowIndex).Cells("数量").Value
                    End If
                    '売単価 <> Nothing
                    If DgvItemList.Rows(e.RowIndex).Cells("売単価").Value IsNot Nothing Then
                        DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value = Math.Ceiling(DgvItemList.Rows(e.RowIndex).Cells("売単価").Value + DgvItemList.Rows(e.RowIndex).Cells("関税額").Value + DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value + DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value)
                    End If
                End If


                '--------------------------
                '単価入力
                '--------------------------
                If currentColumn = "売単価" Or currentColumn = "数量" Then
                    '数量, 売単価 <> Nothing
                    '--------------------------
                    If DgvItemList.Rows(e.RowIndex).Cells("数量").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("売単価").Value IsNot Nothing Then
                        DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value = DgvItemList.Rows(e.RowIndex).Cells("数量").Value * DgvItemList.Rows(e.RowIndex).Cells("売単価").Value

                        '仕入原価 <> Nothing
                        '--------------------------
                        If DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value IsNot Nothing Then
                            DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value = Math.Ceiling(DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value - DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value)
                            '売上金額 <> 0
                            '--------------------------
                            If DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value <> 0 Then
                                DgvItemList.Rows(e.RowIndex).Cells("粗利率").Value = Format(DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value / DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value * 100, "0.0")
                            End If
                        End If

                    End If

                ElseIf currentColumn = "粗利率" Or currentColumn = "数量" Then
                    '--------------------------
                    '粗利入力
                    '--------------------------

                    '数量, 仕入値, 粗利率 <> Nothing
                    '--------------------------
                    If DgvItemList.Rows(e.RowIndex).Cells("数量").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("仕入値").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("粗利率").Value IsNot Nothing Then
                        tmp2 = DgvItemList.Rows(e.RowIndex).Cells("粗利率").Value / 100
                        tmp3 = DgvItemList.Rows(e.RowIndex).Cells("数量").Value - tmp2 * DgvItemList.Rows(e.RowIndex).Cells("数量").Value

                        '仕入原価 <> Nothing
                        '--------------------------
                        If DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value IsNot Nothing Then
                            DgvItemList.Rows(e.RowIndex).Cells("売単価").Value = DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value / tmp3
                            DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value = DgvItemList.Rows(e.RowIndex).Cells("売単価").Value * DgvItemList.Rows(e.RowIndex).Cells("数量").Value
                            DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value = Math.Ceiling(DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value - DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value)
                        End If

                    End If
                ElseIf currentColumn = "見積単価" Or currentColumn = "数量" Then
                    '--------------------------
                    '見積入力
                    '--------------------------

                    '見積単価, 売単価, 関税額, 前払法人税額, 輸送費額 <> Nothing
                    '--------------------------
                    If DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("関税額").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value IsNot Nothing Then
                        'If DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("売単価").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("関税額").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value IsNot Nothing Then
                        tmp4 = DgvItemList.Rows(e.RowIndex).Cells("関税額").Value + DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value + DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value
                        DgvItemList.Rows(e.RowIndex).Cells("売単価").Value = DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value - tmp4
                        DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value = DgvItemList.Rows(e.RowIndex).Cells("売単価").Value * DgvItemList.Rows(e.RowIndex).Cells("数量").Value
                        DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value = Math.Ceiling(DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value - DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value)
                        DgvItemList.Rows(e.RowIndex).Cells("粗利率").Value = Format(DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value / DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value * 100, "0.0")
                    End If
                End If

                '見積金額算出
                If DgvItemList.Rows(e.RowIndex).Cells("数量").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value IsNot Nothing Then
                    DgvItemList.Rows(e.RowIndex).Cells("見積金額").Value = DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value * DgvItemList.Rows(e.RowIndex).Cells("数量").Value
                End If

            End If

        Catch ex As OverflowException

            Throw ex

        End Try

        calcTotal() '合計値の計算

    End Sub

    Private Sub calcTotal()
        'Dim Total As Decimal = 0 '売上金額
        Dim PurchaseTotal As Decimal = 0 '仕入金額
        Dim QuoteTotal As Decimal = 0 '見積金額
        Dim GrossProfit As Decimal = 0 '粗利額

        For index As Integer = 0 To DgvItemList.Rows.Count - 1
            'Total += DgvItemList.Rows(index).Cells("売上金額").Value
            PurchaseTotal += DgvItemList.Rows(index).Cells("仕入原価").Value
            QuoteTotal += DgvItemList.Rows(index).Cells("見積金額").Value
            GrossProfit += DgvItemList.Rows(index).Cells("粗利額").Value
        Next

        TxtPurchaseAmount.Text = PurchaseTotal
        TxtOrderAmount.Text = QuoteTotal.ToString
        TxtGrossProfit.Text = GrossProfit

    End Sub
End Class