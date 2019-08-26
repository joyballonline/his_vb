Option Explicit On

Imports System.Globalization
Imports UtilMDL
Imports UtilMDL.DataGridView
Imports UtilMDL.DB
Imports UtilMDL.LANG
Imports UtilMDL.MSG

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
    Private LoadFlg As Boolean = False

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
        Dim reccnt As Integer = 0

        delCellValueChanged() 'イベントハンドラー削除

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

        'リードタイム単位コンボボックス作成
        Dim column2 As New DataGridViewComboBoxColumn()
        column2.DataSource = getReadTime()
        column2.ValueMember = "Value"
        column2.DisplayMember = "Display"
        column2.HeaderText = "リードタイム単位"
        column2.Name = "リードタイム単位"

        DgvItemList.Columns.Insert(29, column2)

        '仕入通貨コンボボックス作成
        Dim column3 As New DataGridViewComboBoxColumn()
        column3.DataSource = getSireCurrency()
        column3.ValueMember = "Value"
        column3.DisplayMember = "Display"
        column3.HeaderText = "仕入通貨"
        column3.Name = "仕入通貨"

        DgvItemList.Columns.Insert(9, column3)

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
                TxtQuoteTotal.Text = ds1.Tables(RS).Rows(0)("見積金額")
            End If
            If ds1.Tables(RS).Rows(0)("仕入金額") IsNot DBNull.Value Then
                TxtPurchaseTotal.Text = ds1.Tables(RS).Rows(0)("仕入金額")
            End If
            If ds1.Tables(RS).Rows(0)("粗利額") IsNot DBNull.Value Then
                txtProfitmargin.Text = ds1.Tables(RS).Rows(0)("粗利額")
            End If
            If ds1.Tables(RS).Rows(0)("ＶＡＴ") IsNot DBNull.Value Then
                TxtVatAmount.Text = ds1.Tables(RS).Rows(0)("ＶＡＴ")
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
                DgvItemList.Rows(index).Cells("仕入レート").Value = setSireCurrency(ds3.Tables(RS).Rows(index)("仕入通貨"))
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
                DgvItemList.Rows(index).Cells("関税率").Value = ds3.Tables(RS).Rows(index)("関税率") * 100
                DgvItemList.Rows(index).Cells("関税額").Value = ds3.Tables(RS).Rows(index)("関税額")
                DgvItemList.Rows(index).Cells("前払法人税率").Value = ds3.Tables(RS).Rows(index)("前払法人税率") * 100
                DgvItemList.Rows(index).Cells("前払法人税額").Value = ds3.Tables(RS).Rows(index)("前払法人税額")
                DgvItemList.Rows(index).Cells("輸送費率").Value = ds3.Tables(RS).Rows(index)("輸送費率") * 100
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

            Dim Sql As String = ""
            Sql = " AND 見積番号 = '" & ds1.Tables(RS).Rows(0)("見積番号") & "'"
            Sql += " AND 見積番号枝番 = '" & ds1.Tables(RS).Rows(0)("見積番号枝番") & "'"
            Dim dsMithd As DataSet = getDsData("t01_mithd", Sql)

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
                txtProfitmargin.Text = ds1.Tables(RS).Rows(0)("粗利額")
            End If
            If ds1.Tables(RS).Rows(0)("見積金額") IsNot DBNull.Value Then
                TxtQuoteTotal.Text = ds1.Tables(RS).Rows(0)("見積金額")
            End If
            If ds1.Tables(RS).Rows(0)("仕入金額") IsNot DBNull.Value Then
                TxtPurchaseTotal.Text = ds1.Tables(RS).Rows(0)("仕入金額")
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
                DgvItemList.Rows(index).Cells("仕入レート").Value = setSireCurrency(ds3.Tables(RS).Rows(index)("仕入通貨"))
                If ds3.Tables(RS).Rows(index)("仕入通貨") Is DBNull.Value Then
                Else
                    Dim tmp2 As Integer = ds3.Tables(RS).Rows(index)("仕入通貨")
                    DgvItemList.Rows(index).Cells("仕入通貨").Value = tmp2
                End If
                DgvItemList.Rows(index).Cells("仕入単価_外貨").Value = ds3.Tables(RS).Rows(index)("仕入単価_外貨")
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
                DgvItemList.Rows(index).Cells("見積単価_外貨").Value = ds3.Tables(RS).Rows(index)("見積単価_外貨")
                DgvItemList.Rows(index).Cells("見積金額").Value = ds3.Tables(RS).Rows(index)("見積金額")
                DgvItemList.Rows(index).Cells("見積金額_外貨").Value = ds3.Tables(RS).Rows(index)("見積金額_外貨")
                DgvItemList.Rows(index).Cells("関税率").Value = ds3.Tables(RS).Rows(index)("関税率") * 100
                DgvItemList.Rows(index).Cells("関税額").Value = ds3.Tables(RS).Rows(index)("関税額")
                DgvItemList.Rows(index).Cells("前払法人税率").Value = ds3.Tables(RS).Rows(index)("前払法人税率") * 100
                DgvItemList.Rows(index).Cells("前払法人税額").Value = ds3.Tables(RS).Rows(index)("前払法人税額")
                DgvItemList.Rows(index).Cells("輸送費率").Value = ds3.Tables(RS).Rows(index)("輸送費率") * 100
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
                TxtCustomerPO.Enabled = False
                CmCurrency.Enabled = False
            End If

            '通貨・レート情報設定
            createCurrencyCombobox(ds1.Tables(RS).Rows(0)("通貨").ToString)
            setRate()

            '通貨表示：ベースの設定
            setBaseCurrency()
            setChangeCurrency()
        End If

        '言語判定　ヘッダーの設定
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblOrderNo.Text = "OrderNumber"
            LblCustomerPO.Text = "CustomerNumber"
            LblCustomerPO.Location = New Point(273, 12)
            LblCustomerPO.Size = New Size(145, 23)
            TxtCustomerPO.Location = New Point(425, 12)
            LblOrderDate.Text = "OrderDate"
            LblOrderDate.Location = New Point(610, 12)
            DtpOrderDate.Location = New Point(726, 12)
            DtpOrderDate.Size = New Size(130, 22)

            LblRegistration.Text = "OrderingDate"
            LblRegistration.Location = New Point(860, 12)
            DtpOrderRegistration.Location = New Point(975, 12)
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

            LblCurrency.Text = "Currency" '通貨
            LblRate.Text = "Rate" 'レート

            LblIDRCurrency.Text = "Currency" '通貨
            LblChangeCurrency.Text = "Currency" '通貨
            TxtVat.Size = New Size(151, 23)
            TxtCurrencyVatAmount.Size = New Size(151, 23)
            LblOrderCurrencyAmount.Text = "JobOrderAmount"
            LblOrderCurrencyAmount.Size = New Size(180, 23)
            LblOrderCurrencyAmount.Location = New Point(923, 480)

            BtnRegistration.Text = "Registrartion"
            BtnBack.Text = "Back"


            LblOrderAmount.Text = "SalesAmount（l）"      '売上金額
            lblPurchasecost.Text = "PurchaseCost（c）"    '仕入原価
            LblGrossProfit.Text = "GrossMargin（o）"      '粗利額
            lblGrossmargin.Text = "GrossMarginRate（p）"  '粗利率

            LblQuoteAmount.Text = "QuotationAmount（n）"    '見積金額
            LblPurchaseAmount.Text = "PurchaseAmount（j）"  '仕入金額
            lblProfitmargin.Text = "Profitmargin"           '利益
            lblProfitmarginRate.Text = "ProfitmarginRate"   '利益率


            '英語用見出し
            DgvItemList.Columns("仕入区分").HeaderText = "PurchasingClassification"
            DgvItemList.Columns("メーカー").HeaderText = "Manufacturer"
            DgvItemList.Columns("品名").HeaderText = "ItemName"
            DgvItemList.Columns("型式").HeaderText = "Spec"
            DgvItemList.Columns("数量").HeaderText = "Quantity" & vbCrLf & "a"
            DgvItemList.Columns("単位").HeaderText = "Unit"
            DgvItemList.Columns("仕入先").HeaderText = "SupplierName"
            DgvItemList.Columns("仕入通貨").HeaderText = "PurchaseCurrency"

            DgvItemList.Columns("仕入単価_外貨").HeaderText = "PurchaseUnitPrice" & vbCrLf & "(OriginalCurrency)"
            DgvItemList.Columns("仕入値").HeaderText = "PurchaseUnitPrice(" & TxtIDRCurrency.Text & ")" & vbCrLf & "b"

            DgvItemList.Columns("仕入原価").HeaderText = "PurchasingCost" & vbCrLf & "c=a*b"
            DgvItemList.Columns("関税率").HeaderText = "CustomsDutyRate" & vbCrLf & "d"
            DgvItemList.Columns("関税額").HeaderText = "CustomsDutyParUnit" & vbCrLf & "e=b*d"
            DgvItemList.Columns("前払法人税率").HeaderText = "PrepaidCorporateTaxRate" & vbCrLf & "f"
            DgvItemList.Columns("前払法人税額").HeaderText = "PrepaidCorporateTaxAmountParUnit" & vbCrLf & "g=(b+e)*f"
            DgvItemList.Columns("輸送費率").HeaderText = "TransportationCostRate" & vbCrLf & "h"
            DgvItemList.Columns("輸送費額").HeaderText = "TransportationCostParUnit" & vbCrLf & "i=b*h"
            DgvItemList.Columns("仕入金額").HeaderText = "PurchaseAmount" & vbCrLf & "j=a*(b+e+g+i)"
            DgvItemList.Columns("売単価_外貨").HeaderText = "SellingPrice" & vbCrLf & "(OriginalCurrency)"
            DgvItemList.Columns("売単価").HeaderText = "SellingPrice" & vbCrLf & "k"
            DgvItemList.Columns("売上金額").HeaderText = "SalesAmount" & vbCrLf & "l=a*k"
            DgvItemList.Columns("見積単価").HeaderText = "QuotetionPrice(" & TxtIDRCurrency.Text & ")" & vbCrLf & "m=k+e+g+i"
            DgvItemList.Columns("見積単価_外貨").HeaderText = "QuotetionPrice" & vbCrLf & "(OriginalCurrency)"
            DgvItemList.Columns("見積金額").HeaderText = "QuotetionAmount(" & TxtIDRCurrency.Text & ")" & vbCrLf & "n=a*m"
            DgvItemList.Columns("見積金額_外貨").HeaderText = "QuotetionAmount" & vbCrLf & "(OriginalCurrency)"
            DgvItemList.Columns("粗利額").HeaderText = "GrossMargin" & vbCrLf & "o=a*(k-b)"
            DgvItemList.Columns("粗利率").HeaderText = "GrossMarginRate(%)" & vbCrLf & "p=(1-(b/k))*100"
            DgvItemList.Columns("リードタイム").HeaderText = "LeadTime"
            DgvItemList.Columns("リードタイム単位").HeaderText = "LeadTimeUnit"
            DgvItemList.Columns("備考").HeaderText = "Remarks"

        Else  '日本語

            '日本語用見出し
            DgvItemList.Columns("数量").HeaderText = "数量" & vbCrLf & "a"
            DgvItemList.Columns("仕入単価_外貨").HeaderText = "仕入単価(原通貨)"
            DgvItemList.Columns("仕入値").HeaderText = "仕入単価(" & TxtIDRCurrency.Text & ")" & vbCrLf & "b"
            DgvItemList.Columns("仕入原価").HeaderText = "仕入原価" & vbCrLf & "c=a*b"
            DgvItemList.Columns("関税率").HeaderText = "関税率" & vbCrLf & "d"
            DgvItemList.Columns("関税額").HeaderText = "単価当り関税額" & vbCrLf & "e=b*d"
            DgvItemList.Columns("前払法人税率").HeaderText = "前払法人税率" & vbCrLf & "f"
            DgvItemList.Columns("前払法人税額").HeaderText = "単価当り前払法人税額" & vbCrLf & "g=(b+e)*f"
            DgvItemList.Columns("輸送費率").HeaderText = "輸送費率" & vbCrLf & "h"
            DgvItemList.Columns("輸送費額").HeaderText = "単価当り輸送費額" & vbCrLf & "i=b*h"
            DgvItemList.Columns("仕入金額").HeaderText = "仕入金額" & vbCrLf & "j=a*(b+e+g+i)"
            DgvItemList.Columns("売単価_外貨").HeaderText = "売単価(原通貨)"
            DgvItemList.Columns("売単価").HeaderText = "売単価(" & TxtIDRCurrency.Text & ")" & vbCrLf & "k"
            DgvItemList.Columns("売上金額").HeaderText = "売上金額" & vbCrLf & "l=a*k"
            DgvItemList.Columns("見積単価").HeaderText = "見積単価(" & TxtIDRCurrency.Text & ")" & vbCrLf & "m=k+e+g+i"
            DgvItemList.Columns("見積金額").HeaderText = "見積金額(" & TxtIDRCurrency.Text & ")" & vbCrLf & "n=a*m"
            DgvItemList.Columns("粗利額").HeaderText = "粗利額" & vbCrLf & "o=a*(k-b)"
            DgvItemList.Columns("粗利率").HeaderText = "粗利率(%)" & vbCrLf & "p=(1-(b/k))*100"


            DgvItemList.Columns("見積単価_外貨").HeaderText = "見積単価(原通貨)"
            DgvItemList.Columns("見積金額_外貨").HeaderText = "見積金額(原通貨)"

        End If


        'ヘッダ項目を中央寄せ
        DgvItemList.Columns("No").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("仕入区分").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("メーカー").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("品名").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("型式").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("数量").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("単位").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("仕入先").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("仕入通貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("仕入単価_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("仕入値").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("仕入原価").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("関税率").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("関税額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("前払法人税率").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("前払法人税額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("輸送費率").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("輸送費額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("仕入金額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("売単価_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("売単価").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("売上金額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("見積単価").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("見積単価_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("見積金額_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("見積金額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("粗利額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("粗利率").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("リードタイム").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvItemList.Columns("備考").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter


        LoadFlg = True

        calcTotal() '合計値の計算

        setCellValueChanged()   'セル変更イベントを無効化

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
                Sql1 += ", 営業担当者コード, 入力担当者, 入力担当者コード, 備考, 見積備考, ＶＡＴ, 受注日, 登録日, 更新日"
                Sql1 += ", 更新者, 取消区分, 見積金額_外貨, 通貨, レート)"
                Sql1 += " VALUES("
                Sql1 += "'" & CompanyCode & "'" '会社コード
                Sql1 += ", '"
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
                Sql1 += UtilClass.formatNumber(TxtQuoteTotal.Text) '見積金額
                Sql1 += "', '"
                Sql1 += UtilClass.formatNumber(TxtPurchaseTotal.Text) '仕入金額
                Sql1 += "', '"
                Sql1 += UtilClass.formatNumber(txtProfitmargin.Text) '粗利額
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
                Sql1 += "', current_timestamp " '更新日
                Sql1 += ", '"
                Sql1 += frmC01F10_Login.loginValue.TantoNM '更新者
                Sql1 += "', '"
                Sql1 += "0" '取消区分
                Sql1 += "', '"
                Sql1 += UtilClass.formatNumber(TxtOrderCurrencyAmount.Text) '見積金額_外貨
                Sql1 += "', '"
                Sql1 += CmCurrency.SelectedValue.ToString '通貨
                Sql1 += "', '"
                Sql1 += UtilClass.formatNumberF10(TxtRate.Text) 'レート
                Sql1 += "')"

                _db.executeDB(Sql1)

                Dim Sql2 As String = ""
                For i As Integer = 0 To DgvItemList.Rows.Count - 1
                    Sql2 = ""
                    Sql2 += "INSERT INTO "
                    Sql2 += "Public."
                    Sql2 += "t11_cymndt("
                    Sql2 += "会社コード, 受注番号, 受注番号枝番, 行番号, 仕入区分, メーカー, 品名, 型式, 単位"
                    Sql2 += ", 仕入先名, 仕入値, 受注数量, 売上数量, 受注残数, 間接費, 売単価, 売上金額, 粗利額"
                    Sql2 += ", 粗利率, リードタイム, リードタイム単位, 出庫数, 未出庫数, 備考, 更新者, 登録日"
                    Sql2 += ", 見積単価, 見積金額, 関税率, 関税額, 前払法人税率, 前払法人税額, 輸送費率, 輸送費額"
                    Sql2 += ", 仕入原価, 仕入金額, 見積単価_外貨, 見積金額_外貨, 通貨, レート, 仕入単価_外貨, 仕入通貨, 仕入レート)"
                    Sql2 += " VALUES('"
                    Sql2 += CompanyCode '会社コード
                    Sql2 += "', '"
                    Sql2 += TxtOrderNo.Text '受注番号
                    Sql2 += "', '"
                    Sql2 += TxtOrderSuffix.Text '受注番号枝番
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(i).Cells("No").Value.ToString '行番号
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(i).Cells("仕入区分").Value.ToString '仕入区分
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(i).Cells("メーカー").Value.ToString 'メーカー
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(i).Cells("品名").Value.ToString '品名
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(i).Cells("型式").Value.ToString '型式
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(i).Cells("単位").Value.ToString '単位
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(i).Cells("仕入先").Value.ToString '仕入先名
                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(i).Cells("仕入値").Value.ToString) '仕入値
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(i).Cells("数量").Value.ToString '受注数量
                    Sql2 += "', '"
                    Sql2 += "0" '売上数量
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(i).Cells("数量").Value.ToString '受注残数
                    Sql2 += "', '"

                    If DgvItemList.Rows(i).Cells("間接費").Value.ToString = "" Then
                        Sql2 += "0" '間接費
                    Else
                        Sql2 += UtilClass.formatNumber(DgvItemList.Rows(i).Cells("間接費").Value.ToString) '間接費
                    End If
                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(i).Cells("売単価").Value.ToString) '売単価
                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(i).Cells("売上金額").Value.ToString) '売上金額
                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(i).Cells("粗利額").Value.ToString) '粗利額
                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(i).Cells("粗利率").Value.ToString) '粗利率
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(i).Cells("リードタイム").Value.ToString 'リードタイム
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(i).Cells("リードタイム単位").Value.ToString 'リードタイム単位
                    Sql2 += "', '"
                    Sql2 += "0" '出庫数
                    Sql2 += "', '"
                    Sql2 += DgvItemList.Rows(i).Cells("数量").Value.ToString '未出庫数
                    Sql2 += "', '"
                    Sql2 += RevoveChars(DgvItemList.Rows(i).Cells("備考").Value.ToString) '備考
                    Sql2 += "', '"
                    Sql2 += frmC01F10_Login.loginValue.TantoNM '更新者
                    Sql2 += "', '"
                    Sql2 += dtNow '登録日

                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(i).Cells("見積単価").Value.ToString) '見積単価
                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(i).Cells("見積金額").Value.ToString) '見積金額
                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(i).Cells("関税率").Value.ToString / 100) '関税率
                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(i).Cells("関税額").Value.ToString) '関税額
                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(i).Cells("前払法人税率").Value.ToString / 100) '前払法人税率
                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(i).Cells("前払法人税額").Value.ToString) '前払法人税額
                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(i).Cells("輸送費率").Value.ToString / 100) '輸送費率
                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(i).Cells("輸送費額").Value.ToString) '輸送費額
                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(i).Cells("仕入原価").Value.ToString) '仕入原価
                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(i).Cells("仕入金額").Value.ToString) '仕入金額
                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(i).Cells("見積単価_外貨").Value.ToString) '見積単価_外貨
                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumber(DgvItemList.Rows(i).Cells("見積金額_外貨").Value.ToString) '見積金額_外貨
                    Sql2 += "', '"
                    Sql2 += CmCurrency.SelectedValue.ToString '通貨
                    Sql2 += "', '"
                    Sql2 += UtilClass.formatNumberF10(TxtRate.Text) 'レート
                    Sql2 += "' "

                    If DgvItemList.Rows(i).Cells("仕入単価_外貨").Value IsNot Nothing Then   '仕入単価_外貨
                        Sql2 += " ," & UtilClass.formatNumber(DgvItemList.Rows(i).Cells("仕入単価_外貨").Value.ToString)
                    Else
                        Sql2 += " ,0"
                    End If

                    Sql2 += "," & RevoveChars(DgvItemList.Rows(i).Cells("仕入通貨").Value.ToString)    '仕入通貨

                    If DgvItemList.Rows(i).Cells("仕入レート").Value IsNot Nothing Then
                        Sql2 += "," & UtilClass.formatNumberF10(DgvItemList.Rows(i).Cells("仕入レート").Value.ToString)    '仕入レート
                    Else
                        Sql2 += ",0"
                    End If

                    Sql2 += ")"

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
                Sql1 += " , 受注日 = '" & UtilClass.strFormatDate(DtpOrderDate.Value) & "'"
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

        Sql += "SELECT * FROM "

        Sql += "public." & tableName
        Sql += " WHERE "
        Sql += "会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += txtParam

        Return _db.selectDB(Sql, RS, reccnt)
    End Function

    'Return: DataTable
    Private Function getSireKbn() As DataTable
        Dim Sql As String = ""
        Dim strViewText As String = ""

        Sql = " AND "
        Sql += "固定キー = '" & CommonConst.FIXED_KEY_PURCHASING_CLASS & "'"
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
    Private Sub DgvItemList_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DgvItemList.CellValueChanged

        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        '操作したカラム名を取得
        Dim currentColumn As String = DgvItemList.Columns(e.ColumnIndex).Name

        '1回だとイベントハンドラーを削除しきれなかったので、2回実行している
        delCellValueChanged()
        delCellValueChanged()

        If LoadFlg Then
            '合計値リセット
            TxtPurchaseTotal.Clear()       '発注金額
            TxtQuoteTotal.Clear()          '受注金額
            txtProfitmargin.Clear()          '粗利額
            TxtVatAmount.Clear()            'VAT額
            TxtCurrencyVatAmount.Clear()    'VAT額_外貨
            TxtOrderCurrencyAmount.Clear()  '受注金額_外貨

            Dim Total As Decimal = 0 '売上金額
            Dim PurchaseTotal As Decimal = 0 '仕入金額
            Dim QuoteTotal As Decimal = 0 '見積金額
            Dim GrossProfit As Decimal = 0 '粗利額
            Dim tmpPurchase As Integer = 0
            Dim tmp As Decimal = 0
            Dim tmp1 As Decimal = 0
            Dim tmp2 As Decimal = 0
            Dim tmp3 As Decimal = 0
            Dim tmp4 As Decimal = 0

            If DgvItemList.Rows.Count = 0 Then
                Exit Sub
            End If

            '仕入通貨が変更されたら仕入レートを更新する
            If currentColumn = "仕入通貨" Then
                Dim sireCurrencyCd As String = DgvItemList("仕入通貨", e.RowIndex).Value

                Dim tmpCurrencyVal As Decimal = setSireCurrency(DgvItemList.Rows(e.RowIndex).Cells("仕入通貨").Value)
                DgvItemList("仕入レート", e.RowIndex).Value = tmpCurrencyVal

            End If

            '仕入通貨 / 仕入値 / 仕入単価_外貨 が変更されたらそれぞれを更新する
            If currentColumn = "仕入通貨" Or currentColumn = "仕入値" Or currentColumn = "仕入単価_外貨" Then

                If DgvItemList.Rows.Count > 0 Then
                    Select Case currentColumn
                        Case "仕入通貨"
                            If DgvItemList("仕入値", e.RowIndex).Value IsNot Nothing And DgvItemList("仕入レート", e.RowIndex).Value IsNot Nothing Then
                                DgvItemList("仕入単価_外貨", e.RowIndex).Value = DgvItemList("仕入値", e.RowIndex).Value * DgvItemList("仕入レート", e.RowIndex).Value
                            End If
                        Case "仕入値"
                            If DgvItemList("仕入レート", e.RowIndex).Value IsNot Nothing Then
                                DgvItemList("仕入単価_外貨", e.RowIndex).Value = DgvItemList("仕入値", e.RowIndex).Value * DgvItemList("仕入レート", e.RowIndex).Value
                            End If
                        Case "仕入単価_外貨"
                            If DgvItemList("仕入単価_外貨", e.RowIndex).Value IsNot Nothing And DgvItemList("仕入レート", e.RowIndex).Value IsNot Nothing Then
                                DgvItemList("仕入値", e.RowIndex).Value = DgvItemList("仕入単価_外貨", e.RowIndex).Value / DgvItemList("仕入レート", e.RowIndex).Value
                            End If
                    End Select

                End If

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
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("仕入単価_外貨").Value) And (DgvItemList.Rows(e.RowIndex).Cells("仕入単価_外貨").Value IsNot Nothing) Then
                MessageBox.Show("Please enter with numeric value.", "PurchaseUnitPrice Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                DgvItemList.Rows(e.RowIndex).Cells("仕入単価_外貨").Value = Nothing
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
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value) And (DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value IsNot Nothing) Then
                MessageBox.Show("Please enter with numeric value.", "QuotetionPrice Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value = Nothing
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("見積金額").Value) And (DgvItemList.Rows(e.RowIndex).Cells("見積金額").Value IsNot Nothing) Then
                MessageBox.Show("Please enter with numeric value.", "QuotetionAmount Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                DgvItemList.Rows(e.RowIndex).Cells("見積金額").Value = Nothing
                Exit Sub
            End If
            If Not IsNumeric(DgvItemList.Rows(e.RowIndex).Cells("見積金額_外貨").Value) And (DgvItemList.Rows(e.RowIndex).Cells("見積金額_外貨").Value IsNot Nothing) Then
                MessageBox.Show("Please enter with numeric value.", "QuotetionAmount Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                DgvItemList.Rows(e.RowIndex).Cells("見積金額_外貨").Value = Nothing
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

                    '仕入値 <> Nothing
                    '--------------------------
                    'If currentColumn = "仕入値" And DgvItemList.Rows(e.RowIndex).Cells("仕入値").Value IsNot Nothing Then
                    If DgvItemList.Rows(e.RowIndex).Cells("仕入値").Value IsNot Nothing Then

                        '数量 <> Nothing
                        If DgvItemList.Rows(e.RowIndex).Cells("数量").Value IsNot Nothing Then
                            DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value = DgvItemList.Rows(e.RowIndex).Cells("仕入値").Value * DgvItemList.Rows(e.RowIndex).Cells("数量").Value
                        End If
                        '関税率 <> Nothing
                        If DgvItemList.Rows(e.RowIndex).Cells("関税率").Value IsNot Nothing Then
                            DgvItemList.Rows(e.RowIndex).Cells("関税額").Value = DgvItemList.Rows(e.RowIndex).Cells("仕入値").Value * DgvItemList.Rows(e.RowIndex).Cells("関税率").Value / 100
                        End If
                        '前払法人税率, 関税額 <> Nothing
                        If DgvItemList.Rows(e.RowIndex).Cells("前払法人税率").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("関税額").Value IsNot Nothing Then
                            tmp = DgvItemList.Rows(e.RowIndex).Cells("仕入値").Value + DgvItemList.Rows(e.RowIndex).Cells("関税額").Value
                            tmp1 = tmp * DgvItemList.Rows(e.RowIndex).Cells("前払法人税率").Value / 100
                            tmp1 = tmp1
                            DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value = tmp1
                        End If
                        '輸送費率 <> Nothing
                        If DgvItemList.Rows(e.RowIndex).Cells("輸送費率").Value IsNot Nothing Then
                            DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value = DgvItemList.Rows(e.RowIndex).Cells("仕入値").Value * DgvItemList.Rows(e.RowIndex).Cells("輸送費率").Value / 100
                        End If

                        If DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value IsNot Nothing Then
                            DgvItemList.Rows(e.RowIndex).Cells("仕入金額").Value = DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value + (DgvItemList.Rows(e.RowIndex).Cells("関税額").Value + DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value + DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value) * DgvItemList.Rows(e.RowIndex).Cells("数量").Value
                        End If

                        '仕入単価_外貨 ここでは計算されない
                        'DgvItemList("仕入単価_外貨", e.RowIndex).Value = DgvItemList("仕入値", e.RowIndex).Value * DgvItemList("仕入レート", e.RowIndex).Value
                    End If


                    '売単価 <> Nothing and 見積単価を入力した場合は処理しない
                    If DgvItemList.Rows(e.RowIndex).Cells("売単価_外貨").Value IsNot Nothing And currentColumn <> "見積単価_外貨" Then
                        DgvItemList.Rows(e.RowIndex).Cells("売単価").Value = DgvItemList.Rows(e.RowIndex).Cells("売単価_外貨").Value / TxtRate.Text

                        DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value = DgvItemList.Rows(e.RowIndex).Cells("売単価").Value + DgvItemList.Rows(e.RowIndex).Cells("関税額").Value + DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value + DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value
                        DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value = Math.Truncate(DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value * 100) / 100

                        DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value = DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value * TxtRate.Text
                        DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value = Math.Truncate(DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value * 100) / 100

                    End If


                    '--------------------------
                    '単価入力
                    '--------------------------
                    If currentColumn = "売単価_外貨" Or currentColumn = "数量" Then
                        '数量, 売単価 <> Nothing
                        '--------------------------
                        If DgvItemList.Rows(e.RowIndex).Cells("数量").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("売単価").Value IsNot Nothing Then
                            DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value = DgvItemList.Rows(e.RowIndex).Cells("数量").Value * DgvItemList.Rows(e.RowIndex).Cells("売単価").Value

                            '仕入原価 <> Nothing
                            '--------------------------
                            If DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value IsNot Nothing Then
                                DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value = DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value - DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value
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
                                DgvItemList.Rows(e.RowIndex).Cells("売単価_外貨").Value = DgvItemList.Rows(e.RowIndex).Cells("売単価").Value * TxtRate.Text
                                DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value = DgvItemList.Rows(e.RowIndex).Cells("売単価").Value * DgvItemList.Rows(e.RowIndex).Cells("数量").Value
                                DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value = DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value - DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value
                            End If

                            '関税額, 前払法人税額, 輸送費額 <> Nothing
                            '--------------------------
                            If DgvItemList.Rows(e.RowIndex).Cells("関税額").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value IsNot Nothing Then
                                tmp4 = DgvItemList.Rows(e.RowIndex).Cells("関税額").Value + DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value + DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value

                                DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value = DgvItemList.Rows(e.RowIndex).Cells("売単価").Value + tmp4
                                DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value = Math.Truncate(DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value * 100) / 100

                                DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value = DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value * TxtRate.Text
                                DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value = Math.Truncate(DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value * 100) / 100
                            End If

                        End If
                    ElseIf currentColumn = "見積単価_外貨" Or currentColumn = "数量" Then
                        '--------------------------
                        '見積入力
                        '--------------------------

                        '見積単価, 売単価, 関税額, 前払法人税額, 輸送費額 <> Nothing
                        '--------------------------
                        If DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("関税額").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value IsNot Nothing Then

                            DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value = Math.Truncate(DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value * 100) / 100

                            DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value = DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value / TxtRate.Text
                            DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value = Math.Truncate(DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value * 100) / 100

                            tmp4 = DgvItemList.Rows(e.RowIndex).Cells("関税額").Value + DgvItemList.Rows(e.RowIndex).Cells("前払法人税額").Value + DgvItemList.Rows(e.RowIndex).Cells("輸送費額").Value
                            DgvItemList.Rows(e.RowIndex).Cells("売単価").Value = DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value - tmp4
                            DgvItemList.Rows(e.RowIndex).Cells("売単価_外貨").Value = DgvItemList.Rows(e.RowIndex).Cells("売単価").Value * TxtRate.Text
                            DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value = DgvItemList.Rows(e.RowIndex).Cells("売単価").Value * DgvItemList.Rows(e.RowIndex).Cells("数量").Value
                            DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value = DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value - DgvItemList.Rows(e.RowIndex).Cells("仕入原価").Value
                            DgvItemList.Rows(e.RowIndex).Cells("粗利率").Value = Format(DgvItemList.Rows(e.RowIndex).Cells("粗利額").Value / DgvItemList.Rows(e.RowIndex).Cells("売上金額").Value * 100, "0.0")
                        End If
                    End If

                    '見積金額算出
                    If DgvItemList.Rows(e.RowIndex).Cells("数量").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value IsNot Nothing Then
                        DgvItemList.Rows(e.RowIndex).Cells("見積金額").Value = DgvItemList.Rows(e.RowIndex).Cells("見積単価").Value * DgvItemList.Rows(e.RowIndex).Cells("数量").Value
                        DgvItemList.Rows(e.RowIndex).Cells("見積金額_外貨").Value = DgvItemList.Rows(e.RowIndex).Cells("見積単価_外貨").Value * DgvItemList.Rows(e.RowIndex).Cells("数量").Value
                    End If

                    setCellValueChanged()

                End If

                For index As Integer = 0 To DgvItemList.Rows.Count - 1
                    PurchaseTotal += DgvItemList.Rows(index).Cells("仕入金額").Value
                    'Total += DgvItemList.Rows(index).Cells("売上金額").Value
                    QuoteTotal += DgvItemList.Rows(index).Cells("見積金額").Value
                    GrossProfit += DgvItemList.Rows(index).Cells("粗利額").Value
                Next

                TxtPurchaseTotal.Text = PurchaseTotal '発注金額
                'TxtOrderAmount.Text = Total.ToString("F0") '受注金額
                TxtQuoteTotal.Text = QuoteTotal '受注金額
                txtProfitmargin.Text = GrossProfit
                TxtVatAmount.Text = ((QuoteTotal * TxtVat.Text) / 100).ToString("F0")
                setCurrency() '通貨に設定した内容に変更

            Catch ex As OverflowException

                Throw ex

            End Try

            calcTotal() '合計値の計算

        End If


    End Sub

    'Return: DataTable
    Private Function getReadTime() As DataTable

        Dim Sql As String = ""
        Sql += " AND 固定キー = '" & CommonConst.FIXED_KEY_READTIME & "'"

        Dim ds As DataSet = getDsData("m90_hanyo", Sql)

        Dim table As New DataTable("Table")
        table.Columns.Add("Display", GetType(String))
        table.Columns.Add("Value", GetType(Integer))

        'リードタイム単位の多言語対応
        For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                table.Rows.Add(ds.Tables(RS).Rows(i)("文字２"), ds.Tables(RS).Rows(i)("可変キー"))
            Else
                table.Rows.Add(ds.Tables(RS).Rows(i)("文字１"), ds.Tables(RS).Rows(i)("可変キー"))
            End If

        Next
        Return table
    End Function

    Private Function setSireCurrency(Optional ByRef prmCurrencyVal As Integer = CommonConst.CURRENCY_CD_IDR)
        Dim retVal As Decimal
        '通貨コードが1（IDR）の場合、
        If prmCurrencyVal = CommonConst.CURRENCY_CD_IDR Then
            ''currentが日本だったら
            'If CultureInfo.CurrentCulture.Name.ToString = CommonConst.CI_ID Then
            '    retVal = Decimal.Parse(CommonConst.BASE_RATE_IDR)
            'Else
            '    retVal = Decimal.Parse(CommonConst.BASE_RATE_JPY)
            'End If
            retVal = 1.ToString("F10")
        Else
            '基準日よりも古い最新のレートを取得
            Dim Sql As String = ""
            Sql = " AND 採番キー =" & prmCurrencyVal
            Sql += " AND 基準日 < '" & UtilClass.strFormatDate(DtpOrderDate.Text) & "'"
            Sql += " ORDER BY 基準日 DESC "

            Dim ds As DataSet = getDsData("t71_exchangerate", Sql)
            If ds.Tables(RS).Rows.Count > 0 Then
                retVal = ds.Tables(RS).Rows(0)("レート")
            End If
        End If
        Return retVal
    End Function

    'Return: DataTable
    Private Function getSireCurrency() As DataTable
        Dim Sql As String = " AND 取消区分 = '" & CommonConst.FLAG_ENABLED & "'"

        Dim ds As DataSet = getDsData("m25_currency", Sql)

        Dim table As New DataTable("Table")
        table.Columns.Add("Display", GetType(String))
        table.Columns.Add("Value", GetType(Integer))

        'リードタイム単位の多言語対応
        For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
            table.Rows.Add(ds.Tables(RS).Rows(i)("通貨コード"), ds.Tables(RS).Rows(i)("採番キー"))
        Next
        Return table
    End Function

    '通貨のコンボボックスを作成
    '編集モードの時は値を渡してセットさせる
    Private Sub createCurrencyCombobox(Optional ByRef prmVal As String = "")
        CmCurrency.DisplayMember = "Text"
        CmCurrency.ValueMember = "Value"

        Dim Sql As String = " AND 取消区分 = '" & CommonConst.FLAG_ENABLED & "'"

        Dim ds As DataSet = getDsData("m25_currency", Sql)

        Dim tb As New DataTable
        tb.Columns.Add("Text", GetType(String))
        tb.Columns.Add("Value", GetType(String))

        For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
            tb.Rows.Add(ds.Tables(RS).Rows(i)("通貨コード"), ds.Tables(RS).Rows(i)("採番キー"))

        Next

        CmCurrency.DataSource = tb

        If prmVal IsNot "" Then
            CmCurrency.SelectedValue = prmVal
        Else
            CmCurrency.SelectedIndex = 0
        End If
    End Sub

    '通貨の採番キーからレートを取得・設定
    '基準日が見積日「以前」の最新のもの
    Private Sub setRate()
        Dim Sql As String

        Sql = " AND 採番キー = " & CmCurrency.SelectedValue.ToString & ""
        Sql += " AND 基準日 < '" & UtilClass.strFormatDate(DtpQuoteDate.Text) & "'"
        Sql += " ORDER BY 基準日 DESC "

        Dim ds As DataSet = getDsData("t71_exchangerate", Sql)

        If ds.Tables(RS).Rows.Count > 0 Then
            TxtRate.Text = ds.Tables(RS).Rows(0)("レート")
        Else
            'If CultureInfo.CurrentCulture.Name.ToString = CommonConst.CI_ID Then
            '    TxtRate.Text = CommonConst.BASE_RATE_IDR
            'Else
            '    TxtRate.Text = CommonConst.BASE_RATE_JPY
            'End If
            TxtRate.Text = 1.ToString("F10")
        End If

    End Sub

    Private Sub setBaseCurrency()
        Dim Sql As String
        '通貨表示：ベースの設定
        Sql = " AND 採番キー = " & CommonConst.CURRENCY_CD_IDR.ToString
        Sql += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString

        Dim ds As DataSet = getDsData("m25_currency", Sql)
        TxtIDRCurrency.Text = ds.Tables(RS).Rows(0)("通貨コード")

    End Sub

    '通貨表示：通貨変更の設定
    Private Sub setChangeCurrency()
        Dim Sql As String
        Sql = " AND 採番キー = " & CmCurrency.SelectedValue.ToString
        Sql += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString

        Dim ds As DataSet = getDsData("m25_currency", Sql)
        TxtChangeCurrency.Text = ds.Tables(RS).Rows(0)("通貨コード")
    End Sub

    'Currencyに応じて変換
    Private Sub setCurrency()

        Dim currencyVal As Decimal = IIf(TxtRate.Text <> "", TxtRate.Text, 0)   'レート
        'Dim vatVal As Decimal = IIf(TxtVatAmount.Text <> "", TxtVatAmount.Text, 0)  'VAT
        Dim OrderCurrencyTotal As Decimal = 0       '見積金額_外貨

        For c As Integer = 0 To DgvItemList.Rows.Count - 1
            OrderCurrencyTotal += DgvItemList.Rows(c).Cells("見積金額_外貨").Value
        Next
        TxtOrderCurrencyAmount.Text = OrderCurrencyTotal.ToString("F0") '受注金額

        If IsNumeric(TxtVat.Text) Then
            If TxtVat.Text <> 0 Then
                TxtCurrencyVatAmount.Text = ((OrderCurrencyTotal.ToString("F0") * TxtVat.Text) / 100).ToString("F0")
            End If
        End If


    End Sub

    Private Sub calcTotal()

        Dim Total As Decimal = 0          '売上金額
        Dim PurchaseCost As Decimal = 0   '仕入原価
        Dim GrossProfit As Decimal = 0    '粗利額
        Dim QuoteTotal As Decimal = 0     '見積金額
        Dim PurchaseTotal As Decimal = 0  '仕入金額
        Dim ProfitMargin As Decimal = 0　 '利益額


        For index As Integer = 0 To DgvItemList.Rows.Count - 1

            Total += DgvItemList.Rows(index).Cells("売上金額").Value
            PurchaseCost += DgvItemList.Rows(index).Cells("仕入原価").Value
            GrossProfit += DgvItemList.Rows(index).Cells("粗利額").Value

            QuoteTotal += DgvItemList.Rows(index).Cells("見積金額").Value
            PurchaseTotal += DgvItemList.Rows(index).Cells("仕入金額").Value
        Next


        TxtTotal.Text = Total.ToString("N2")                '売上金額
        txtPurchasecost.Text = PurchaseCost.ToString("N2")  '仕入原価
        TxtGrossProfit.Text = GrossProfit.ToString("N2")    '粗利額

        '粗利率
        If TxtTotal.Text = 0 Or txtPurchasecost.Text = 0 Then
            txtGrossmarginRate.Text = 0
        Else
            txtGrossmarginRate.Text = ((GrossProfit / Total) * 100).ToString("N1")
        End If


        TxtQuoteTotal.Text = QuoteTotal.ToString("N2")        '見積金額
        TxtPurchaseTotal.Text = PurchaseTotal.ToString("N2")  '仕入金額

        '利益額
        If TxtQuoteTotal.Text = 0 Or TxtPurchaseTotal.Text = 0 Then
            txtProfitmargin.Text = 0
        Else
            ProfitMargin = QuoteTotal - PurchaseTotal
            txtProfitmargin.Text = ProfitMargin.ToString("N2")
        End If

        '利益率
        If TxtQuoteTotal.Text = 0 Or TxtPurchaseTotal.Text = 0 Then
            txtProfitmarginRate.Text = 0
        Else
            txtProfitmarginRate.Text = ((ProfitMargin / QuoteTotal) * 100).ToString("N1")
        End If

        TxtVatAmount.Text = ((QuoteTotal * TxtVat.Text) / 100).ToString("N2") 'VAT-OUT


        setCurrency() '通貨に設定した内容に変更

    End Sub

    Private Sub CmCurrency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmCurrency.SelectedIndexChanged
        setRate()
        setChangeCurrency()
        resetListCurrency()
        setCurrency()
    End Sub

    Private Sub setCellValueChanged()

        AddHandler DgvItemList.CellValueChanged, AddressOf DgvItemList_CellValueChanged

    End Sub
    Private Sub delCellValueChanged()

        RemoveHandler DgvItemList.CellValueChanged, AddressOf DgvItemList_CellValueChanged
    End Sub

    Private Sub resetListCurrency()
        If DgvItemList.Rows.Count > 0 Then
            For i As Integer = 0 To DgvItemList.Rows.Count - 1
                delCellValueChanged()
                delCellValueChanged()
                If DgvItemList.Rows(i).Cells("売単価").Value IsNot Nothing And DgvItemList.Rows(i).Cells("数量").Value IsNot Nothing Then
                    DgvItemList.Rows(i).Cells("売単価_外貨").Value = DgvItemList.Rows(i).Cells("売単価").Value * TxtRate.Text
                End If
                If TxtRate.Text <> "" And DgvItemList.Rows(i).Cells("見積単価").Value IsNot Nothing Then
                    DgvItemList.Rows(i).Cells("見積単価").Value = DgvItemList.Rows(i).Cells("見積単価").Value * TxtRate.Text

                    DgvItemList.Rows(i).Cells("見積単価_外貨").Value = DgvItemList.Rows(i).Cells("見積単価").Value * TxtRate.Text
                    DgvItemList.Rows(i).Cells("見積単価_外貨").Value = Math.Truncate(DgvItemList.Rows(i).Cells("見積単価_外貨").Value * 100) / 100

                    DgvItemList.Rows(i).Cells("見積金額_外貨").Value = DgvItemList.Rows(i).Cells("見積単価_外貨").Value * DgvItemList.Rows(i).Cells("数量").Value
                End If
                setCellValueChanged()
            Next
        End If
    End Sub

End Class