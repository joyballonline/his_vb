Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Imports System.Globalization
Imports System.IO

Public Class DataOutput
    Inherits System.Windows.Forms.Form

    '------------------------------------------------------------------------------------------------------
    'メンバー定数宣言
    '------------------------------------------------------------------------------------------------------
    'PG制御文字 
    Dim ds As DataSet
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
    Private CompanyCode As String = ""
    Private SalesNo As String()
    Private SalesStatus As String = ""

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
                   ByRef prmRefStatus As String)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
        SalesStatus = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True
    End Sub

    '画面表示時
    Private Sub SalesProfitList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
        End If

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            LblMode.Text = SalesStatus & " Mode"

            'DgvList.Columns("仕入番号").HeaderText = "PurchaseNumber"
            'DgvList.Columns("仕入日").HeaderText = "PurchaseDate"
            'DgvList.Columns("仕入先名").HeaderText = "SupplierName"
            'DgvList.Columns("メーカー").HeaderText = "Manufacturer"
            'DgvList.Columns("品名").HeaderText = "ItemName"
            'DgvList.Columns("型式").HeaderText = "Spec"
            'DgvList.Columns("数量").HeaderText = "PurchasedQuantity"
            'DgvList.Columns("単位").HeaderText = "Unit"
            'DgvList.Columns("仕入単価").HeaderText = "PurchaseUnitPrice"
            'DgvList.Columns("ＶＡＴ").HeaderText = "ＶＡＴ"
            'DgvList.Columns("間接費").HeaderText = "Overhead"
            'DgvList.Columns("仕入計").HeaderText = "PurchaseAmount"

            LblTarget.Text = "Target"
            LblPeriod.Text = "Period"
            RbtnQuotation.Text = "Quotation"
            RbtnJobOrder.Text = "JobOrder"
            RbtnSales.Text = "Sales"

            BtnCSVOutput.Text = "CSV Output"
            BtnBack.Text = "Back"

        End If

        DtpDateSince.Text = DateTime.Today
        DtpDateUntil.Text = DateTime.Today

        getList() '一覧表示

    End Sub

    '一覧取得
    Private Sub getList()
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        If DtpDateSince.Text = "" And DtpDateUntil.Text = "" Then
            Return
        End If

        'リスト初期化
        DgvList.Columns.Clear()
        DgvList.Rows.Clear()

        If RbtnQuotation.Checked Then

            '見積選択時
            Sql = "SELECT "
            Sql += " t01.会社コード"
            Sql += ",t01.見積番号"
            Sql += ",t01.見積番号枝番"
            Sql += ",t01.得意先コード"
            Sql += ",t01.得意先名"
            Sql += ",t01.得意先担当者役職"
            Sql += ",t01.得意先担当者名"
            Sql += ",t01.見積日"
            Sql += ",t01.見積有効期限"
            Sql += ",t01.支払条件"
            Sql += ",t01.見積金額"
            Sql += ",t01.営業担当者"
            Sql += ",t01.入力担当者"
            Sql += ",t01.備考"
            Sql += ",t01.登録日"
            Sql += ",t01.仕入金額"
            Sql += ",t01.得意先住所"
            Sql += ",t01.得意先電話番号"
            Sql += ",t01.得意先ＦＡＸ"
            Sql += ",t01.粗利額"
            Sql += ",t01.受注日"
            Sql += ",t01.更新日"
            Sql += ",t01.更新者"
            Sql += ",t01.取消日"
            Sql += ",t01.取消区分"
            Sql += ",t01.得意先郵便番号"
            Sql += ",t01.ＶＡＴ"
            Sql += ",t01.営業担当者コード"
            Sql += ",t01.入力担当者コード"
            Sql += ",t02.行番号"
            Sql += ",t02.仕入区分"
            Sql += ",t02.メーカー"
            Sql += ",t02.品名"
            Sql += ",t02.型式"
            Sql += ",t02.仕入先コード"
            Sql += ",t02.仕入先名称"
            Sql += ",t02.仕入単価"
            Sql += ",t02.数量"
            Sql += ",t02.単位"
            Sql += ",t02.売単価"
            Sql += ",t02.売上金額"
            Sql += ",t02.間接費"
            'Sql += ",t02.更新者"
            'Sql += ",t02.登録日"
            Sql += ",t02.備考 as 明細備考"
            Sql += ",t02.リードタイム"
            Sql += ",t02.粗利額 As 明細粗利額"
            Sql += ",t02.粗利率"
            Sql += ",t02.仕入金額"
            Sql += ",t02.間接費率"
            Sql += ",t02.間接費無仕入金額"
            Sql += ",t02.仕入原価"
            Sql += ",t02.関税率"
            Sql += ",t02.関税額"
            Sql += ",t02.前払法人税率"
            Sql += ",t02.前払法人税額"
            Sql += ",t02.輸送費率"
            Sql += ",t02.輸送費額"
            Sql += ",t02.リードタイム単位"
            Sql += ",t02.見積単価"
            Sql += ",t02.見積金額 as 明細見積金額"
            Sql += " FROM t01_mithd t01 "
            Sql += " LEFT JOIN t02_mitdt t02 "
            Sql += " ON t01.会社コード = t02.会社コード "
            Sql += " AND t01.見積番号 = t02.見積番号 "
            Sql += " AND t01.見積番号枝番 = t02.見積番号枝番 "
            Sql += " WHERE t01.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND t01.取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'"
            Sql += " AND t01.見積日 >= '" & UtilClass.strFormatDate(DtpDateSince.Text) & "'"
            Sql += " AND t01.見積日 <= '" & UtilClass.strFormatDate(DtpDateUntil.Text) & "'"
            Sql += " AND t01.取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'"
            Sql += " ORDER BY t01.見積日, t01.見積番号, t01.見積番号枝番, t01.得意先コード "

        ElseIf RbtnJobOrder.Checked Then

            '受注選択時
            Sql = "SELECT "
            Sql += " t10.会社コード"
            Sql += ",t10.受注番号"
            Sql += ",t10.受注番号枝番"
            Sql += ",t10.見積番号"
            Sql += ",t10.見積番号枝番"
            Sql += ",t10.得意先コード"
            Sql += ",t10.得意先名"
            Sql += ",t10.得意先郵便番号"
            Sql += ",t10.得意先住所"
            Sql += ",t10.得意先電話番号"
            Sql += ",t10.得意先ＦＡＸ"
            Sql += ",t10.得意先担当者役職"
            Sql += ",t10.得意先担当者名"
            Sql += ",t10.見積日"
            Sql += ",t10.見積有効期限"
            Sql += ",t10.支払条件"
            Sql += ",t10.見積金額"
            Sql += ",t10.仕入金額"
            Sql += ",t10.粗利額"
            Sql += ",t10.営業担当者"
            Sql += ",t10.入力担当者"
            Sql += ",t10.備考"
            Sql += ",t10.取消日"
            Sql += ",t10.取消区分"
            Sql += ",t10.ＶＡＴ"
            Sql += ",t10.受注日"
            Sql += ",t10.登録日"
            Sql += ",t10.更新日"
            Sql += ",t10.更新者"
            Sql += ",t10.見積備考"
            Sql += ",t10.ＰＰＨ"
            Sql += ",t10.客先番号"
            Sql += ",t10.営業担当者コード"
            Sql += ",t10.入力担当者コード"
            Sql += ",t11.行番号"
            Sql += ",t11.仕入区分"
            Sql += ",t11.メーカー"
            Sql += ",t11.品名"
            Sql += ",t11.型式"
            Sql += ",t11.仕入先名"
            Sql += ",t11.仕入値"
            Sql += ",t11.受注数量"
            Sql += ",t11.売上数量"
            Sql += ",t11.受注残数"
            Sql += ",t11.間接費"
            Sql += ",t11.売単価"
            Sql += ",t11.売上金額"
            Sql += ",t11.粗利額 as 明細粗利額"
            Sql += ",t11.粗利率"
            Sql += ",t11.リードタイム"
            Sql += ",t11.出庫数"
            Sql += ",t11.未出庫数"
            Sql += ",t11.備考 as 明細備考"
            Sql += ",t11.単位"
            Sql += ",t11.リードタイム単位"
            Sql += ",t11.関税率"
            Sql += ",t11.関税額"
            Sql += ",t11.前払法人税率"
            Sql += ",t11.前払法人税額"
            Sql += ",t11.輸送費率"
            Sql += ",t11.輸送費額"
            Sql += ",t11.見積単価"
            Sql += ",t11.見積金額 as 明細見積金額"
            Sql += ",t11.仕入原価"
            Sql += ",t11.仕入金額 as 明細仕入金額"
            Sql += " FROM t10_cymnhd t10 "
            Sql += " LEFT JOIN t11_cymndt t11 "
            Sql += " ON t10.会社コード = t11.会社コード "
            Sql += " AND t10.受注番号 = t11.受注番号 "
            Sql += " AND t10.受注番号枝番 = t11.受注番号枝番 "
            Sql += " WHERE t10.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND t10.取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'"
            Sql += " AND t10.受注日 >= '" & UtilClass.strFormatDate(DtpDateSince.Text) & "'"
            Sql += " AND t10.受注日 <= '" & UtilClass.strFormatDate(DtpDateUntil.Text) & "'"
            Sql += " ORDER BY t10.受注日, t10.受注番号, t10.受注番号枝番, t10.得意先コード "

        Else

            '売上選択時
            Sql = "SELECT "
            Sql += "t30.会社コード"
            Sql += ",t30.売上番号"
            Sql += ",t30.売上番号枝番"
            Sql += ",t30.受注番号"
            Sql += ",t30.受注番号枝番"
            Sql += ",t30.見積番号"
            Sql += ",t30.見積番号枝番"
            Sql += ",t30.得意先コード"
            Sql += ",t30.得意先名"
            Sql += ",t30.得意先郵便番号"
            Sql += ",t30.得意先住所"
            Sql += ",t30.得意先電話番号"
            Sql += ",t30.得意先ＦＡＸ"
            Sql += ",t30.得意先担当者役職"
            Sql += ",t30.得意先担当者名"
            Sql += ",t30.見積日"
            Sql += ",t30.見積有効期限"
            Sql += ",t30.支払条件"
            Sql += ",t30.見積金額"
            Sql += ",t30.売上金額"
            Sql += ",t30.粗利額"
            Sql += ",t30.営業担当者"
            Sql += ",t30.入力担当者"
            Sql += ",t30.備考"
            Sql += ",t30.取消日"
            Sql += ",t30.取消区分"
            Sql += ",t30.ＶＡＴ"
            Sql += ",t30.ＰＰＨ"
            Sql += ",t30.受注日"
            Sql += ",t30.売上日"
            Sql += ",t30.入金予定日"
            Sql += ",t30.登録日"
            Sql += ",t30.更新日"
            Sql += ",t30.更新者"
            Sql += ",t30.客先番号"
            Sql += ",t30.締処理日"
            Sql += ",t30.仕入金額"
            Sql += ",t30.営業担当者コード"
            Sql += ",t30.入力担当者コード"
            Sql += ",t31.行番号"
            Sql += ",t31.仕入区分"
            Sql += ",t31.メーカー"
            Sql += ",t31.品名"
            Sql += ",t31.型式"
            Sql += ",t31.仕入先名"
            Sql += ",t31.仕入値"
            Sql += ",t31.受注数量"
            Sql += ",t31.売上数量"
            Sql += ",t31.受注残数"
            Sql += ",t31.単位"
            Sql += ",t31.間接費"
            Sql += ",t31.売単価"
            Sql += ",t31.売上金額"
            Sql += ",t31.粗利額 as 明細粗利額"
            Sql += ",t31.粗利率"
            Sql += ",t31.リードタイム"
            Sql += ",t31.入金有無"
            Sql += ",t31.入金番号"
            Sql += ",t31.入金日"
            Sql += ",t31.備考 as 明細備考"
            Sql += ",t31.仕入原価"
            Sql += ",t31.関税率"
            Sql += ",t31.関税額"
            Sql += ",t31.前払法人税率"
            Sql += ",t31.前払法人税額"
            Sql += ",t31.輸送費率"
            Sql += ",t31.輸送費額"
            Sql += ",t31.仕入金額 as 明細仕入金額"
            Sql += ",t31.見積単価"
            Sql += ",t31.見積金額 as 明細見積金額"
            Sql += " FROM t30_urighd t30 "
            Sql += " LEFT JOIN t31_urigdt t31 "
            Sql += " ON t30.会社コード = t31.会社コード "
            Sql += " AND t30.売上番号 = t31.売上番号 "
            Sql += " AND t30.売上番号枝番 = t31.売上番号枝番 "
            Sql += " WHERE t30.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND t30.取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'"
            Sql += " AND t30.売上日 >= '" & UtilClass.strFormatDate(DtpDateSince.Text) & "'"
            Sql += " AND t30.売上日 <= '" & UtilClass.strFormatDate(DtpDateUntil.Text) & "'"
            Sql += " ORDER BY t30.売上日, t30.売上番号, t30.売上番号枝番, t30.得意先コード "

        End If

        Try
            ds = _db.selectDB(Sql, RS, reccnt)

            For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                If i = 0 Then
                    '列名
                    For y As Integer = 0 To ds.Tables(RS).Columns.Count - 1
                        Dim field As String = ds.Tables(RS).Columns(y).ColumnName
                        DgvList.Columns.Add(field, field)

                        '英語処理入れる
                    Next
                End If

                DgvList.Rows.Add()

                '基本
                For y As Integer = 0 To ds.Tables(RS).Columns.Count - 1
                    Dim fieldName As String = ds.Tables(RS).Columns(y).ColumnName

                    DgvList.Rows(i).Cells(fieldName).Value = ds.Tables(RS).Rows(i)(fieldName)

                Next
            Next

            '見出し追加
            'If RbtnQuotation.Checked Then

            '    quontityHd()

            '    'データを回す
            '    For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1

            '        DgvList.Rows.Add()
            '        DgvList.Rows(i).Cells("会社コード").Value = ds.Tables(RS).Rows(i)("会社コード")
            '        DgvList.Rows(i).Cells("見積番号").Value = ds.Tables(RS).Rows(i)("見積番号")
            '        DgvList.Rows(i).Cells("見積番号枝番").Value = ds.Tables(RS).Rows(i)("見積番号枝番")
            '        DgvList.Rows(i).Cells("行番号").Value = ds.Tables(RS).Rows(i)("行番号")
            '        DgvList.Rows(i).Cells("得意先コード").Value = ds.Tables(RS).Rows(i)("得意先コード")
            '        DgvList.Rows(i).Cells("得意先名").Value = ds.Tables(RS).Rows(i)("得意先名")
            '        DgvList.Rows(i).Cells("得意先担当者役職").Value = ds.Tables(RS).Rows(i)("得意先担当者役職")
            '        DgvList.Rows(i).Cells("得意先担当者名").Value = ds.Tables(RS).Rows(i)("得意先担当者名")
            '        DgvList.Rows(i).Cells("得意先郵便番号").Value = ds.Tables(RS).Rows(i)("得意先郵便番号")
            '        DgvList.Rows(i).Cells("得意先住所").Value = ds.Tables(RS).Rows(i)("得意先住所")
            '        DgvList.Rows(i).Cells("得意先電話番号").Value = ds.Tables(RS).Rows(i)("得意先電話番号")
            '        DgvList.Rows(i).Cells("得意先ＦＡＸ").Value = ds.Tables(RS).Rows(i)("得意先ＦＡＸ")
            '        DgvList.Rows(i).Cells("支払条件").Value = ds.Tables(RS).Rows(i)("支払条件")
            '        DgvList.Rows(i).Cells("見積日").Value = ds.Tables(RS).Rows(i)("見積日")
            '        DgvList.Rows(i).Cells("見積有効期限").Value = ds.Tables(RS).Rows(i)("見積有効期限")
            '        DgvList.Rows(i).Cells("営業担当者コード").Value = ds.Tables(RS).Rows(i)("営業担当者コード")
            '        DgvList.Rows(i).Cells("営業担当者").Value = ds.Tables(RS).Rows(i)("営業担当者")
            '        DgvList.Rows(i).Cells("入力担当者コード").Value = ds.Tables(RS).Rows(i)("入力担当者コード")
            '        DgvList.Rows(i).Cells("入力担当者").Value = ds.Tables(RS).Rows(i)("入力担当者")
            '        DgvList.Rows(i).Cells("備考").Value = ds.Tables(RS).Rows(i)("備考")
            '        DgvList.Rows(i).Cells("仕入金額").Value = ds.Tables(RS).Rows(i)("仕入金額")
            '        DgvList.Rows(i).Cells("ＶＡＴ").Value = ds.Tables(RS).Rows(i)("ＶＡＴ")
            '        DgvList.Rows(i).Cells("見積金額").Value = ds.Tables(RS).Rows(i)("見積金額")
            '        DgvList.Rows(i).Cells("粗利額").Value = ds.Tables(RS).Rows(i)("粗利額")
            '        DgvList.Rows(i).Cells("仕入区分").Value = ds.Tables(RS).Rows(i)("仕入区分")
            '        DgvList.Rows(i).Cells("仕入先コード").Value = ds.Tables(RS).Rows(i)("仕入先コード")
            '        DgvList.Rows(i).Cells("仕入先名称").Value = ds.Tables(RS).Rows(i)("仕入先名称")
            '        DgvList.Rows(i).Cells("メーカー").Value = ds.Tables(RS).Rows(i)("メーカー")
            '        DgvList.Rows(i).Cells("品名").Value = ds.Tables(RS).Rows(i)("品名")
            '        DgvList.Rows(i).Cells("型式").Value = ds.Tables(RS).Rows(i)("型式")
            '        DgvList.Rows(i).Cells("受注数量").Value = ds.Tables(RS).Rows(i)("受注数量")
            '        DgvList.Rows(i).Cells("売上数量").Value = ds.Tables(RS).Rows(i)("売上数量")
            '        DgvList.Rows(i).Cells("受注残数").Value = ds.Tables(RS).Rows(i)("受注残数")
            '        DgvList.Rows(i).Cells("出庫数").Value = ds.Tables(RS).Rows(i)("出庫数")
            '        DgvList.Rows(i).Cells("出庫数").Value = ds.Tables(RS).Rows(i)("出庫数")
            '        DgvList.Rows(i).Cells("単位").Value = ds.Tables(RS).Rows(i)("単位")
            '        DgvList.Rows(i).Cells("仕入単価").Value = ds.Tables(RS).Rows(i)("仕入単価")
            '        DgvList.Rows(i).Cells("仕入金額").Value = ds.Tables(RS).Rows(i)("仕入金額")
            '        DgvList.Rows(i).Cells("売単価").Value = ds.Tables(RS).Rows(i)("売単価")
            '        DgvList.Rows(i).Cells("売上金額").Value = ds.Tables(RS).Rows(i)("売上金額")
            '        DgvList.Rows(i).Cells("間接費").Value = ds.Tables(RS).Rows(i)("間接費")
            '        DgvList.Rows(i).Cells("仕入先名称").Value = ds.Tables(RS).Rows(i)("仕入先名称")
            '        DgvList.Rows(i).Cells("粗利率").Value = ds.Tables(RS).Rows(i)("粗利率")
            '        DgvList.Rows(i).Cells("粗利額").Value = ds.Tables(RS).Rows(i)("粗利額")
            '        DgvList.Rows(i).Cells("間接費率").Value = ds.Tables(RS).Rows(i)("間接費率")
            '        DgvList.Rows(i).Cells("間接費無仕入金額").Value = ds.Tables(RS).Rows(i)("間接費無仕入金額")
            '        DgvList.Rows(i).Cells("仕入原価").Value = ds.Tables(RS).Rows(i)("仕入原価")
            '        DgvList.Rows(i).Cells("関税率").Value = ds.Tables(RS).Rows(i)("関税率")
            '        DgvList.Rows(i).Cells("関税額").Value = ds.Tables(RS).Rows(i)("関税額")
            '        DgvList.Rows(i).Cells("前払法人税率").Value = ds.Tables(RS).Rows(i)("前払法人税率")
            '        DgvList.Rows(i).Cells("前払法人税額").Value = ds.Tables(RS).Rows(i)("前払法人税額")
            '        DgvList.Rows(i).Cells("輸送費率").Value = ds.Tables(RS).Rows(i)("輸送費率")
            '        DgvList.Rows(i).Cells("輸送費額").Value = ds.Tables(RS).Rows(i)("輸送費額")
            '        DgvList.Rows(i).Cells("見積単価").Value = ds.Tables(RS).Rows(i)("見積単価")
            '        DgvList.Rows(i).Cells("見積金額").Value = ds.Tables(RS).Rows(i)("見積金額")
            '        DgvList.Rows(i).Cells("リードタイム").Value = ds.Tables(RS).Rows(i)("リードタイム")
            '        DgvList.Rows(i).Cells("リードタイム単位").Value = ds.Tables(RS).Rows(i)("リードタイム単位")
            '        DgvList.Rows(i).Cells("明細備考").Value = ds.Tables(RS).Rows(i)("備考")
            '        DgvList.Rows(i).Cells("取消日").Value = ds.Tables(RS).Rows(i)("取消日")
            '        DgvList.Rows(i).Cells("取消区分").Value = ds.Tables(RS).Rows(i)("取消区分")
            '        DgvList.Rows(i).Cells("受注日").Value = ds.Tables(RS).Rows(i)("受注日")
            '        DgvList.Rows(i).Cells("登録日").Value = ds.Tables(RS).Rows(i)("登録日")
            '        DgvList.Rows(i).Cells("更新日").Value = ds.Tables(RS).Rows(i)("更新日")
            '        DgvList.Rows(i).Cells("更新者").Value = ds.Tables(RS).Rows(i)("更新者")

            '    Next

            'ElseIf RbtnJobOrder.Checked Then
            '    JobOrderHd()

            '    'データを回す
            '    For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1

            '        DgvList.Rows.Add()
            '        DgvList.Rows(i).Cells("会社コード").Value = ds.Tables(RS).Rows(i)("会社コード")
            '        DgvList.Rows(i).Cells("受注番号").Value = ds.Tables(RS).Rows(i)("受注番号")
            '        DgvList.Rows(i).Cells("受注番号枝番").Value = ds.Tables(RS).Rows(i)("受注番号枝番")
            '        DgvList.Rows(i).Cells("見積番号").Value = ds.Tables(RS).Rows(i)("見積番号")
            '        DgvList.Rows(i).Cells("見積番号枝番").Value = ds.Tables(RS).Rows(i)("見積番号枝番")
            '        DgvList.Rows(i).Cells("行番号").Value = ds.Tables(RS).Rows(i)("行番号")
            '        DgvList.Rows(i).Cells("得意先コード").Value = ds.Tables(RS).Rows(i)("得意先コード")
            '        DgvList.Rows(i).Cells("得意先名").Value = ds.Tables(RS).Rows(i)("得意先名")
            '        DgvList.Rows(i).Cells("得意先担当者役職").Value = ds.Tables(RS).Rows(i)("得意先担当者役職")
            '        DgvList.Rows(i).Cells("得意先担当者名").Value = ds.Tables(RS).Rows(i)("得意先担当者名")
            '        DgvList.Rows(i).Cells("得意先郵便番号").Value = ds.Tables(RS).Rows(i)("得意先郵便番号")
            '        DgvList.Rows(i).Cells("得意先住所").Value = ds.Tables(RS).Rows(i)("得意先住所")
            '        DgvList.Rows(i).Cells("得意先電話番号").Value = ds.Tables(RS).Rows(i)("得意先電話番号")
            '        DgvList.Rows(i).Cells("得意先ＦＡＸ").Value = ds.Tables(RS).Rows(i)("得意先ＦＡＸ")
            '        DgvList.Rows(i).Cells("支払条件").Value = ds.Tables(RS).Rows(i)("支払条件")
            '        DgvList.Rows(i).Cells("見積日").Value = ds.Tables(RS).Rows(i)("見積日")
            '        DgvList.Rows(i).Cells("見積有効期限").Value = ds.Tables(RS).Rows(i)("見積有効期限")
            '        DgvList.Rows(i).Cells("営業担当者コード").Value = ds.Tables(RS).Rows(i)("営業担当者コード")
            '        DgvList.Rows(i).Cells("営業担当者").Value = ds.Tables(RS).Rows(i)("営業担当者")
            '        DgvList.Rows(i).Cells("入力担当者コード").Value = ds.Tables(RS).Rows(i)("入力担当者コード")
            '        DgvList.Rows(i).Cells("入力担当者").Value = ds.Tables(RS).Rows(i)("入力担当者")
            '        DgvList.Rows(i).Cells("備考").Value = ds.Tables(RS).Rows(i)("備考")
            '        DgvList.Rows(i).Cells("仕入金額").Value = ds.Tables(RS).Rows(i)("仕入金額")
            '        DgvList.Rows(i).Cells("ＶＡＴ").Value = ds.Tables(RS).Rows(i)("ＶＡＴ")
            '        DgvList.Rows(i).Cells("ＰＰＨ").Value = ds.Tables(RS).Rows(i)("ＰＰＨ")
            '        DgvList.Rows(i).Cells("客先番号").Value = ds.Tables(RS).Rows(i)("ＶＡＴ")
            '        DgvList.Rows(i).Cells("見積金額").Value = ds.Tables(RS).Rows(i)("客先番号")
            '        DgvList.Rows(i).Cells("粗利額").Value = ds.Tables(RS).Rows(i)("粗利額")
            '        DgvList.Rows(i).Cells("仕入区分").Value = ds.Tables(RS).Rows(i)("仕入区分")
            '        DgvList.Rows(i).Cells("仕入先コード").Value = ds.Tables(RS).Rows(i)("仕入先コード")
            '        DgvList.Rows(i).Cells("仕入先名").Value = ds.Tables(RS).Rows(i)("仕入先名")
            '        DgvList.Rows(i).Cells("メーカー").Value = ds.Tables(RS).Rows(i)("メーカー")
            '        DgvList.Rows(i).Cells("品名").Value = ds.Tables(RS).Rows(i)("品名")
            '        DgvList.Rows(i).Cells("型式").Value = ds.Tables(RS).Rows(i)("型式")
            '        DgvList.Rows(i).Cells("数量").Value = ds.Tables(RS).Rows(i)("数量")
            '        DgvList.Rows(i).Cells("単位").Value = ds.Tables(RS).Rows(i)("単位")
            '        DgvList.Rows(i).Cells("仕入値").Value = ds.Tables(RS).Rows(i)("仕入値")
            '        DgvList.Rows(i).Cells("仕入金額").Value = ds.Tables(RS).Rows(i)("仕入金額")
            '        DgvList.Rows(i).Cells("売単価").Value = ds.Tables(RS).Rows(i)("売単価")
            '        DgvList.Rows(i).Cells("売上金額").Value = ds.Tables(RS).Rows(i)("売上金額")
            '        DgvList.Rows(i).Cells("間接費").Value = ds.Tables(RS).Rows(i)("間接費")
            '        DgvList.Rows(i).Cells("仕入先名称").Value = ds.Tables(RS).Rows(i)("仕入先名称")
            '        DgvList.Rows(i).Cells("粗利率").Value = ds.Tables(RS).Rows(i)("粗利率")
            '        DgvList.Rows(i).Cells("粗利額").Value = ds.Tables(RS).Rows(i)("粗利額")
            '        DgvList.Rows(i).Cells("間接費率").Value = ds.Tables(RS).Rows(i)("間接費率")
            '        DgvList.Rows(i).Cells("間接費無仕入金額").Value = ds.Tables(RS).Rows(i)("間接費無仕入金額")
            '        DgvList.Rows(i).Cells("仕入原価").Value = ds.Tables(RS).Rows(i)("仕入原価")
            '        DgvList.Rows(i).Cells("関税率").Value = ds.Tables(RS).Rows(i)("関税率")
            '        DgvList.Rows(i).Cells("関税額").Value = ds.Tables(RS).Rows(i)("関税額")
            '        DgvList.Rows(i).Cells("前払法人税率").Value = ds.Tables(RS).Rows(i)("前払法人税率")
            '        DgvList.Rows(i).Cells("前払法人税額").Value = ds.Tables(RS).Rows(i)("前払法人税額")
            '        DgvList.Rows(i).Cells("輸送費率").Value = ds.Tables(RS).Rows(i)("輸送費率")
            '        DgvList.Rows(i).Cells("輸送費額").Value = ds.Tables(RS).Rows(i)("輸送費額")
            '        DgvList.Rows(i).Cells("見積単価").Value = ds.Tables(RS).Rows(i)("見積単価")
            '        DgvList.Rows(i).Cells("見積金額").Value = ds.Tables(RS).Rows(i)("見積金額")
            '        DgvList.Rows(i).Cells("リードタイム").Value = ds.Tables(RS).Rows(i)("リードタイム")
            '        DgvList.Rows(i).Cells("リードタイム単位").Value = ds.Tables(RS).Rows(i)("リードタイム単位")
            '        DgvList.Rows(i).Cells("明細備考").Value = ds.Tables(RS).Rows(i)("備考")
            '        DgvList.Rows(i).Cells("取消日").Value = ds.Tables(RS).Rows(i)("取消日")
            '        DgvList.Rows(i).Cells("取消区分").Value = ds.Tables(RS).Rows(i)("取消区分")
            '        DgvList.Rows(i).Cells("受注日").Value = ds.Tables(RS).Rows(i)("受注日")
            '        DgvList.Rows(i).Cells("登録日").Value = ds.Tables(RS).Rows(i)("登録日")
            '        DgvList.Rows(i).Cells("更新日").Value = ds.Tables(RS).Rows(i)("更新日")
            '        DgvList.Rows(i).Cells("更新者").Value = ds.Tables(RS).Rows(i)("更新者")

            '    Next

            'Else

            'End If


            'データを回す
            'For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1

            '    DgvList.Rows.Add()
            '    DgvList.Rows(i).Cells("会社コード").Value = ds.Tables(RS).Rows(i)("会社コード")
            '    DgvList.Rows(i).Cells("見積番号").Value = ds.Tables(RS).Rows(i)("見積番号")
            '    DgvList.Rows(i).Cells("見積番号枝番").Value = ds.Tables(RS).Rows(i)("見積番号枝番")
            '    DgvList.Rows(i).Cells("行番号").Value = ds.Tables(RS).Rows(i)("行番号")
            '    DgvList.Rows(i).Cells("得意先コード").Value = ds.Tables(RS).Rows(i)("得意先コード")
            '    DgvList.Rows(i).Cells("得意先名").Value = ds.Tables(RS).Rows(i)("得意先名")
            '    DgvList.Rows(i).Cells("得意先担当者役職").Value = ds.Tables(RS).Rows(i)("得意先担当者役職")
            '    DgvList.Rows(i).Cells("得意先担当者名").Value = ds.Tables(RS).Rows(i)("得意先担当者名")
            '    DgvList.Rows(i).Cells("得意先郵便番号").Value = ds.Tables(RS).Rows(i)("得意先郵便番号")
            '    DgvList.Rows(i).Cells("得意先住所").Value = ds.Tables(RS).Rows(i)("得意先住所")
            '    DgvList.Rows(i).Cells("得意先電話番号").Value = ds.Tables(RS).Rows(i)("得意先電話番号")
            '    DgvList.Rows(i).Cells("得意先ＦＡＸ").Value = ds.Tables(RS).Rows(i)("得意先ＦＡＸ")
            '    DgvList.Rows(i).Cells("支払条件").Value = ds.Tables(RS).Rows(i)("支払条件")
            '    DgvList.Rows(i).Cells("見積日").Value = ds.Tables(RS).Rows(i)("見積日")
            '    DgvList.Rows(i).Cells("見積有効期限").Value = ds.Tables(RS).Rows(i)("見積有効期限")
            '    DgvList.Rows(i).Cells("営業担当者コード").Value = ds.Tables(RS).Rows(i)("営業担当者コード")
            '    DgvList.Rows(i).Cells("営業担当者").Value = ds.Tables(RS).Rows(i)("営業担当者")
            '    DgvList.Rows(i).Cells("入力担当者コード").Value = ds.Tables(RS).Rows(i)("入力担当者コード")
            '    DgvList.Rows(i).Cells("入力担当者").Value = ds.Tables(RS).Rows(i)("入力担当者")
            '    DgvList.Rows(i).Cells("備考").Value = ds.Tables(RS).Rows(i)("備考")
            '    DgvList.Rows(i).Cells("仕入金額").Value = ds.Tables(RS).Rows(i)("仕入金額")
            '    DgvList.Rows(i).Cells("ＶＡＴ").Value = ds.Tables(RS).Rows(i)("ＶＡＴ")
            '    DgvList.Rows(i).Cells("見積金額").Value = ds.Tables(RS).Rows(i)("見積金額")
            '    DgvList.Rows(i).Cells("粗利額").Value = ds.Tables(RS).Rows(i)("粗利額")
            '    DgvList.Rows(i).Cells("仕入区分").Value = ds.Tables(RS).Rows(i)("仕入区分")
            '    DgvList.Rows(i).Cells("仕入先コード").Value = ds.Tables(RS).Rows(i)("仕入先コード")
            '    DgvList.Rows(i).Cells("仕入先名称").Value = ds.Tables(RS).Rows(i)("仕入先名称")
            '    DgvList.Rows(i).Cells("メーカー").Value = ds.Tables(RS).Rows(i)("メーカー")
            '    DgvList.Rows(i).Cells("品名").Value = ds.Tables(RS).Rows(i)("品名")
            '    DgvList.Rows(i).Cells("型式").Value = ds.Tables(RS).Rows(i)("型式")
            '    DgvList.Rows(i).Cells("数量").Value = ds.Tables(RS).Rows(i)("数量")
            '    DgvList.Rows(i).Cells("単位").Value = ds.Tables(RS).Rows(i)("単位")
            '    DgvList.Rows(i).Cells("仕入値").Value = ds.Tables(RS).Rows(i)("仕入値")
            '    DgvList.Rows(i).Cells("仕入金額").Value = ds.Tables(RS).Rows(i)("仕入金額")
            '    DgvList.Rows(i).Cells("売単価").Value = ds.Tables(RS).Rows(i)("売単価")
            '    DgvList.Rows(i).Cells("売上金額").Value = ds.Tables(RS).Rows(i)("売上金額")
            '    DgvList.Rows(i).Cells("間接費").Value = ds.Tables(RS).Rows(i)("間接費")
            '    DgvList.Rows(i).Cells("仕入先名称").Value = ds.Tables(RS).Rows(i)("仕入先名称")
            '    DgvList.Rows(i).Cells("粗利率").Value = ds.Tables(RS).Rows(i)("粗利率")
            '    DgvList.Rows(i).Cells("粗利額").Value = ds.Tables(RS).Rows(i)("粗利額")
            '    DgvList.Rows(i).Cells("間接費率").Value = ds.Tables(RS).Rows(i)("間接費率")
            '    DgvList.Rows(i).Cells("間接費無仕入金額").Value = ds.Tables(RS).Rows(i)("間接費無仕入金額")
            '    DgvList.Rows(i).Cells("仕入原価").Value = ds.Tables(RS).Rows(i)("仕入原価")
            '    DgvList.Rows(i).Cells("関税率").Value = ds.Tables(RS).Rows(i)("関税率")
            '    DgvList.Rows(i).Cells("関税額").Value = ds.Tables(RS).Rows(i)("関税額")
            '    DgvList.Rows(i).Cells("前払法人税率").Value = ds.Tables(RS).Rows(i)("前払法人税率")
            '    DgvList.Rows(i).Cells("前払法人税額").Value = ds.Tables(RS).Rows(i)("前払法人税額")
            '    DgvList.Rows(i).Cells("輸送費率").Value = ds.Tables(RS).Rows(i)("輸送費率")
            '    DgvList.Rows(i).Cells("輸送費額").Value = ds.Tables(RS).Rows(i)("輸送費額")
            '    DgvList.Rows(i).Cells("見積単価").Value = ds.Tables(RS).Rows(i)("見積単価")
            '    DgvList.Rows(i).Cells("見積金額").Value = ds.Tables(RS).Rows(i)("見積金額")
            '    DgvList.Rows(i).Cells("リードタイム").Value = ds.Tables(RS).Rows(i)("リードタイム")
            '    DgvList.Rows(i).Cells("リードタイム単位").Value = ds.Tables(RS).Rows(i)("リードタイム単位")
            '    DgvList.Rows(i).Cells("明細備考").Value = ds.Tables(RS).Rows(i)("備考")
            '    DgvList.Rows(i).Cells("取消日").Value = ds.Tables(RS).Rows(i)("取消日")
            '    DgvList.Rows(i).Cells("取消区分").Value = ds.Tables(RS).Rows(i)("取消区分")
            '    DgvList.Rows(i).Cells("受注日").Value = ds.Tables(RS).Rows(i)("受注日")
            '    DgvList.Rows(i).Cells("登録日").Value = ds.Tables(RS).Rows(i)("登録日")
            '    DgvList.Rows(i).Cells("更新日").Value = ds.Tables(RS).Rows(i)("更新日")
            '    DgvList.Rows(i).Cells("更新者").Value = ds.Tables(RS).Rows(i)("更新者")

            'Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    Private Sub quontityHd()

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            DgvList.Columns.Add("会社コード", "CompanyCode")
            DgvList.Columns.Add("見積番号", "QuotationNumber")
            DgvList.Columns.Add("見積番号枝番", "QuotationSubNumber")
            DgvList.Columns.Add("行番号", "LineNumber")
            DgvList.Columns.Add("得意先コード", "CustomerCode")
            DgvList.Columns.Add("得意先名", "CustomerName")
            DgvList.Columns.Add("得意先担当者役職", "PositionPICCustomer")
            DgvList.Columns.Add("得意先担当者名", "NameOfPIC")
            DgvList.Columns.Add("得意先郵便番号", "CustomerPostalCode")
            DgvList.Columns.Add("得意先住所", "CustomerAddress")
            DgvList.Columns.Add("得意先電話番号", "CustomerPhoneNumber")
            DgvList.Columns.Add("得意先ＦＡＸ", "CustomerFAX")
            DgvList.Columns.Add("支払条件", "PaymentTermsAndConditon")
            DgvList.Columns.Add("見積日", "QuotationDate")
            DgvList.Columns.Add("見積有効期限", "QuotationExpirationDate")
            DgvList.Columns.Add("営業担当者コード", "SalesPersonInChargeCode")
            DgvList.Columns.Add("営業担当者", "SalesPersonInCharge")
            DgvList.Columns.Add("入力担当者コード", "PICForInputtingCode")
            DgvList.Columns.Add("入力担当者", "PICForInputting")
            DgvList.Columns.Add("備考", "Remarks")
            DgvList.Columns.Add("仕入金額", "PurchaseAmount")
            DgvList.Columns.Add("ＶＡＴ", "ＶＡＴ")
            DgvList.Columns.Add("見積金額", "QuotationAmount")
            DgvList.Columns.Add("粗利額", "GrossMargin")

            DgvList.Columns.Add("仕入区分", "PurchasingClassification")
            DgvList.Columns.Add("仕入先コード", "SupplierCode")
            DgvList.Columns.Add("仕入先名称", "SupplierName")
            DgvList.Columns.Add("メーカー", "Manufacturer")
            DgvList.Columns.Add("品名", "ItemName")
            DgvList.Columns.Add("型式", "Spec")
            DgvList.Columns.Add("数量", "Quantity")
            DgvList.Columns.Add("単位", "Unit")
            DgvList.Columns.Add("仕入単価", "PurchaseUnitPrice")
            DgvList.Columns.Add("仕入金額", "PurchaseAmount")
            DgvList.Columns.Add("売単価", "SellingPrice")
            DgvList.Columns.Add("売上金額", "SalesAmount")
            DgvList.Columns.Add("間接費", "Overhead")
            'DgvList.Columns.Add("更新者", "ModifiedBy")
            'DgvList.Columns.Add("登録日", "ModifiedBy")
            DgvList.Columns.Add("粗利率", "GrossMarginRate")
            DgvList.Columns.Add("粗利額", "GrossMargin")
            DgvList.Columns.Add("間接費率", "OverheadRate")
            DgvList.Columns.Add("間接費無仕入金額", "OverheadNoPurchasePrice")
            DgvList.Columns.Add("仕入原価", "PurchasingCost")
            DgvList.Columns.Add("関税率", "CustomsDutyRate")
            DgvList.Columns.Add("関税額", "CustomsDuty")
            DgvList.Columns.Add("前払法人税率", "PrepaidCorporateTaxRate")
            DgvList.Columns.Add("前払法人税額", "PrepaidCorporateTaxAmount")
            DgvList.Columns.Add("輸送費率", "TransportationCostRate")
            DgvList.Columns.Add("輸送費額", "TransportationCost")
            DgvList.Columns.Add("見積単価", "QuotationUnitPrice")
            DgvList.Columns.Add("見積金額", "QuotationAmount")

            DgvList.Columns.Add("リードタイム", "LeadTime")
            DgvList.Columns.Add("リードタイム単位", "LeadTimUnit")
            DgvList.Columns.Add("明細備考", "DetailsRemarks")

            DgvList.Columns.Add("取消日", "CancelDate")
            DgvList.Columns.Add("取消区分", "CancelClassification")

            DgvList.Columns.Add("受注日", "JobOrderDate")
            DgvList.Columns.Add("登録日", "RegistrationDate")
            DgvList.Columns.Add("更新日", "UpdateDate")
            DgvList.Columns.Add("更新者", "ModifiedBy")

        Else

            DgvList.Columns.Add("会社コード", "会社コード")
            DgvList.Columns.Add("見積番号", "見積番号")
            DgvList.Columns.Add("見積番号枝番", "見積番号枝番")
            DgvList.Columns.Add("行番号", "行番号")
            DgvList.Columns.Add("得意先コード", "得意先コード")
            DgvList.Columns.Add("得意先名", "得意先名")
            DgvList.Columns.Add("得意先担当者役職", "得意先担当者役職")
            DgvList.Columns.Add("得意先担当者名", "得意先担当者名")
            DgvList.Columns.Add("得意先郵便番号", "得意先郵便番号")
            DgvList.Columns.Add("得意先住所", "得意先住所")
            DgvList.Columns.Add("得意先電話番号", "得意先電話番号")
            DgvList.Columns.Add("得意先ＦＡＸ", "得意先ＦＡＸ")
            DgvList.Columns.Add("支払条件", "支払条件")
            DgvList.Columns.Add("見積日", "見積日")
            DgvList.Columns.Add("見積有効期限", "見積有効期限")
            DgvList.Columns.Add("営業担当者コード", "営業担当者コード")
            DgvList.Columns.Add("営業担当者", "営業担当者")
            DgvList.Columns.Add("入力担当者コード", "入力担当者コード")
            DgvList.Columns.Add("入力担当者", "入力担当者")
            DgvList.Columns.Add("備考", "備考")
            DgvList.Columns.Add("仕入金額", "仕入金額")
            DgvList.Columns.Add("ＶＡＴ", "ＶＡＴ")
            DgvList.Columns.Add("見積金額", "見積金額")
            DgvList.Columns.Add("粗利額", "粗利額")

            DgvList.Columns.Add("仕入区分", "仕入区分")
            DgvList.Columns.Add("仕入先コード", "仕入先コード")
            DgvList.Columns.Add("仕入先名称", "仕入先名称")
            DgvList.Columns.Add("メーカー", "メーカー")
            DgvList.Columns.Add("品名", "品名")
            DgvList.Columns.Add("型式", "型式")
            DgvList.Columns.Add("数量", "数量")
            DgvList.Columns.Add("単位", "単位")
            DgvList.Columns.Add("仕入単価", "仕入単価")
            DgvList.Columns.Add("仕入金額", "仕入金額")
            DgvList.Columns.Add("売単価", "売単価")
            DgvList.Columns.Add("売上金額", "売上金額")
            DgvList.Columns.Add("間接費", "間接費")
            'DgvList.Columns.Add("更新者", "更新者")
            'DgvList.Columns.Add("登録日", "登録日")
            DgvList.Columns.Add("粗利率", "粗利率")
            DgvList.Columns.Add("粗利額", "粗利額")
            DgvList.Columns.Add("間接費率", "間接費率")
            DgvList.Columns.Add("間接費無仕入金額", "間接費無仕入金額")
            DgvList.Columns.Add("仕入原価", "仕入原価")
            DgvList.Columns.Add("関税率", "関税率")
            DgvList.Columns.Add("関税額", "関税額")
            DgvList.Columns.Add("前払法人税率", "前払法人税率")
            DgvList.Columns.Add("前払法人税額", "前払法人税額")
            DgvList.Columns.Add("輸送費率", "輸送費率")
            DgvList.Columns.Add("輸送費額", "輸送費額")
            DgvList.Columns.Add("見積単価", "見積単価")
            DgvList.Columns.Add("見積金額", "見積金額")

            DgvList.Columns.Add("リードタイム", "リードタイム")
            DgvList.Columns.Add("リードタイム単位", "リードタイム単位")
            DgvList.Columns.Add("明細備考", "明細備考")

            DgvList.Columns.Add("取消日", "取消日")
            DgvList.Columns.Add("取消区分", "取消区分")

            DgvList.Columns.Add("受注日", "受注日")
            DgvList.Columns.Add("登録日", "登録日")
            DgvList.Columns.Add("更新日", "更新日")
            DgvList.Columns.Add("更新者", "更新者")
        End If

        DgvList.Columns("見積金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("粗利額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DgvList.Columns("仕入単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("売単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("売上金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("間接費").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("粗利額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("粗利率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("間接費率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("間接費無仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("関税率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("関税額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("前払法人税率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("前払法人税額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("輸送費率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("輸送費額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("リードタイム").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("見積単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("見積金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    End Sub

    Private Sub JobOrderHd()

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            DgvList.Columns.Add("会社コード", "CompanyCode")
            DgvList.Columns.Add("受注番号", "QuotationNumber")
            DgvList.Columns.Add("受注番号枝番", "QuotationSubNumber")
            DgvList.Columns.Add("見積番号", "QuotationNumber")
            DgvList.Columns.Add("見積番号枝番", "QuotationSubNumber")

            DgvList.Columns.Add("行番号", "LineNumber")
            DgvList.Columns.Add("得意先コード", "CustomerCode")
            DgvList.Columns.Add("得意先名", "CustomerName")
            DgvList.Columns.Add("得意先担当者役職", "PositionPICCustomer")
            DgvList.Columns.Add("得意先担当者名", "NameOfPIC")
            DgvList.Columns.Add("得意先郵便番号", "CustomerPostalCode")
            DgvList.Columns.Add("得意先住所", "CustomerAddress")
            DgvList.Columns.Add("得意先電話番号", "CustomerPhoneNumber")
            DgvList.Columns.Add("得意先ＦＡＸ", "CustomerFAX")
            DgvList.Columns.Add("支払条件", "PaymentTermsAndConditon")
            DgvList.Columns.Add("見積日", "QuotationDate")
            DgvList.Columns.Add("見積有効期限", "QuotationExpirationDate")

            DgvList.Columns.Add("営業担当者コード", "SalesPersonInChargeCode")
            DgvList.Columns.Add("営業担当者", "SalesPersonInCharge")
            DgvList.Columns.Add("入力担当者コード", "PICForInputtingCode")
            DgvList.Columns.Add("入力担当者", "PICForInputting")
            DgvList.Columns.Add("備考", "Remarks")
            DgvList.Columns.Add("見積備考", "QuotationRemarks")
            DgvList.Columns.Add("仕入金額", "PurchaseAmount")
            DgvList.Columns.Add("ＶＡＴ", "ＶＡＴ")
            DgvList.Columns.Add("ＰＰＨ", "ＰＰＨ")
            DgvList.Columns.Add("客先番号", "PO")
            DgvList.Columns.Add("見積金額", "QuotationAmount")
            DgvList.Columns.Add("粗利額", "GrossMargin")

            DgvList.Columns.Add("仕入区分", "PurchasingClassification")
            'DgvList.Columns.Add("仕入先コード", "SupplierCode")
            DgvList.Columns.Add("仕入先名", "SupplierName")
            DgvList.Columns.Add("メーカー", "Manufacturer")
            DgvList.Columns.Add("品名", "ItemName")
            DgvList.Columns.Add("型式", "Spec")

            DgvList.Columns.Add("受注数量", "JobOrderQuantity")
            DgvList.Columns.Add("売上数量", "SalesQuantity")
            DgvList.Columns.Add("受注残数", "OrderRemainingAmount")
            DgvList.Columns.Add("出庫数", "GoodsDeliveryQuantity")
            DgvList.Columns.Add("未出庫数", "GoodsDeliveryRemainingQuantity")

            DgvList.Columns.Add("単位", "Unit")
            DgvList.Columns.Add("仕入値", "PurchaseUnitPrice")
            DgvList.Columns.Add("仕入金額", "PurchaseAmount")
            DgvList.Columns.Add("売単価", "SellingPrice")
            DgvList.Columns.Add("売上金額", "SalesAmount")
            DgvList.Columns.Add("間接費", "Overhead")
            'DgvList.Columns.Add("更新者", "ModifiedBy")
            'DgvList.Columns.Add("登録日", "ModifiedBy")
            DgvList.Columns.Add("粗利率", "GrossMarginRate")
            DgvList.Columns.Add("粗利額", "GrossMargin")
            DgvList.Columns.Add("間接費率", "OverheadRate")

            DgvList.Columns.Add("仕入原価", "PurchasingCost")
            DgvList.Columns.Add("関税率", "CustomsDutyRate")
            DgvList.Columns.Add("関税額", "CustomsDuty")
            DgvList.Columns.Add("前払法人税率", "PrepaidCorporateTaxRate")
            DgvList.Columns.Add("前払法人税額", "PrepaidCorporateTaxAmount")
            DgvList.Columns.Add("輸送費率", "TransportationCostRate")
            DgvList.Columns.Add("輸送費額", "TransportationCost")
            DgvList.Columns.Add("見積単価", "QuotationUnitPrice")
            DgvList.Columns.Add("見積金額", "QuotationAmount")

            DgvList.Columns.Add("リードタイム", "LeadTime")
            DgvList.Columns.Add("リードタイム単位", "LeadTimUnit")
            DgvList.Columns.Add("明細備考", "DetailsRemarks")

            DgvList.Columns.Add("取消日", "CancelDate")
            DgvList.Columns.Add("取消区分", "CancelClassification")

            DgvList.Columns.Add("受注日", "JobOrderDate")
            DgvList.Columns.Add("登録日", "RegistrationDate")
            DgvList.Columns.Add("更新日", "UpdateDate")
            DgvList.Columns.Add("更新者", "ModifiedBy")

        Else

            DgvList.Columns.Add("会社コード", "会社コード")
            DgvList.Columns.Add("受注番号", "受注番号")
            DgvList.Columns.Add("受注番号枝番", "受注番号枝番")
            DgvList.Columns.Add("見積番号", "見積番号")
            DgvList.Columns.Add("見積番号枝番", "見積番号枝番")
            DgvList.Columns.Add("行番号", "行番号")
            DgvList.Columns.Add("得意先コード", "得意先コード")
            DgvList.Columns.Add("得意先名", "得意先名")
            DgvList.Columns.Add("得意先担当者役職", "得意先担当者役職")
            DgvList.Columns.Add("得意先担当者名", "得意先担当者名")
            DgvList.Columns.Add("得意先郵便番号", "得意先郵便番号")
            DgvList.Columns.Add("得意先住所", "得意先住所")
            DgvList.Columns.Add("得意先電話番号", "得意先電話番号")
            DgvList.Columns.Add("得意先ＦＡＸ", "得意先ＦＡＸ")
            DgvList.Columns.Add("支払条件", "支払条件")
            DgvList.Columns.Add("見積日", "見積日")
            DgvList.Columns.Add("見積有効期限", "見積有効期限")
            DgvList.Columns.Add("営業担当者コード", "営業担当者コード")
            DgvList.Columns.Add("営業担当者", "営業担当者")
            DgvList.Columns.Add("入力担当者コード", "入力担当者コード")
            DgvList.Columns.Add("入力担当者", "入力担当者")
            DgvList.Columns.Add("備考", "備考")
            DgvList.Columns.Add("見積備考", "見積備考")
            DgvList.Columns.Add("仕入金額", "仕入金額")
            DgvList.Columns.Add("ＶＡＴ", "ＶＡＴ")
            DgvList.Columns.Add("ＰＰＨ", "ＰＰＨ")
            DgvList.Columns.Add("客先番号", "客先番号")
            DgvList.Columns.Add("見積金額", "見積金額")
            DgvList.Columns.Add("粗利額", "粗利額")

            DgvList.Columns.Add("仕入区分", "仕入区分")
            DgvList.Columns.Add("仕入先コード", "仕入先コード")
            DgvList.Columns.Add("仕入先名", "仕入先名")
            DgvList.Columns.Add("メーカー", "メーカー")
            DgvList.Columns.Add("品名", "品名")
            DgvList.Columns.Add("型式", "型式")
            DgvList.Columns.Add("受注数量", "受注数量")
            DgvList.Columns.Add("売上数量", "売上数量")
            DgvList.Columns.Add("受注残数", "受注残数")
            DgvList.Columns.Add("出庫数", "出庫数")
            DgvList.Columns.Add("未出庫数", "未出庫数")
            DgvList.Columns.Add("単位", "単位")
            DgvList.Columns.Add("仕入単価", "仕入単価")
            DgvList.Columns.Add("仕入金額", "仕入金額")
            DgvList.Columns.Add("売単価", "売単価")
            DgvList.Columns.Add("売上金額", "売上金額")
            DgvList.Columns.Add("間接費", "間接費")
            'DgvList.Columns.Add("更新者", "更新者")
            'DgvList.Columns.Add("登録日", "登録日")
            DgvList.Columns.Add("粗利率", "粗利率")
            DgvList.Columns.Add("粗利額", "粗利額")
            DgvList.Columns.Add("間接費率", "間接費率")
            DgvList.Columns.Add("仕入原価", "仕入原価")
            DgvList.Columns.Add("関税率", "関税率")
            DgvList.Columns.Add("関税額", "関税額")
            DgvList.Columns.Add("前払法人税率", "前払法人税率")
            DgvList.Columns.Add("前払法人税額", "前払法人税額")
            DgvList.Columns.Add("輸送費率", "輸送費率")
            DgvList.Columns.Add("輸送費額", "輸送費額")
            DgvList.Columns.Add("見積単価", "見積単価")
            DgvList.Columns.Add("見積金額", "見積金額")

            DgvList.Columns.Add("リードタイム", "リードタイム")
            DgvList.Columns.Add("リードタイム単位", "リードタイム単位")
            DgvList.Columns.Add("明細備考", "明細備考")

            DgvList.Columns.Add("取消日", "取消日")
            DgvList.Columns.Add("取消区分", "取消区分")

            DgvList.Columns.Add("受注日", "受注日")
            DgvList.Columns.Add("登録日", "登録日")
            DgvList.Columns.Add("更新日", "更新日")
            DgvList.Columns.Add("更新者", "更新者")
        End If

        'DgvList.Columns("見積金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("粗利額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        'DgvList.Columns("仕入単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("売単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("売上金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("間接費").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("粗利額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("粗利率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("間接費率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("間接費無仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("関税率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("関税額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("前払法人税率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("前払法人税額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("輸送費率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("輸送費額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("リードタイム").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("見積単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("見積金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    End Sub


    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    Private Sub BtnCSVOutput_Click(sender As Object, e As EventArgs) Handles BtnCSVOutput.Click

        '対象データがない場合は取消操作不可能
        If ds.Tables(RS).Rows.Count = 0 Then
            '該当データがないアラートを出す
            _msgHd.dspMSG("noTargetData", frmC01F10_Login.loginValue.Language)
            Return
        Else

            'Excel出力処理
            outputCSV(ds)
        End If

    End Sub

    'CSV出力処理
    Private Sub outputCSV(ByVal prmDataSet As DataSet)

        'カーソルをビジー状態にする
        Cursor.Current = Cursors.WaitCursor

        Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("UTF-8")
        '出力先パス
        Dim sOutPath As String = ""
        sOutPath = StartUp._iniVal.OutXlsPath

        '出力ファイル名
        Dim sOutFile As String = ""
        sOutFile = sOutPath & "\" & Name & "_" & DateTime.Now.ToString("yyyyMMddHHmm") & ".csv"

        '書き込むファイルを開く
        Dim sr As New System.IO.StreamWriter(sOutFile, False, enc)

        For i As Integer = 0 To DgvList.Rows.Count - 1
            If i = 0 Then
                '基本列名
                For y As Integer = 0 To DgvList.Columns.Count - 1
                    Dim field As String = DgvList.Columns(y).HeaderText
                    field = EncloseDoubleQuotesIfNeed(field)
                    sr.Write(field)
                    If y < DgvList.Columns.Count - 1 Then
                        sr.Write(","c)
                    End If
                Next
                sr.Write(vbCrLf)
            End If

            '基本
            For y As Integer = 0 To DgvList.Columns.Count - 1
                Dim fieldName As String = DgvList.Columns(y).HeaderText

                Dim field As String = DgvList.Rows(i).Cells(fieldName).Value.ToString()
                field = EncloseDoubleQuotesIfNeed(field)
                sr.Write(field)
                If y < DgvList.Columns.Count - 1 Then
                    sr.Write(","c)
                End If
            Next
            sr.Write(vbCrLf)
        Next

        sr.Close()

        'カーソルをビジー状態から元に戻す
        Cursor.Current = Cursors.Default

    End Sub

    Private Function EncloseDoubleQuotes(field As String) As String
        If field.IndexOf(""""c) > -1 Then
            '"を""とする
            field = field.Replace("""", """""")
        End If
        Return """" & field & """"
    End Function

    Private Function EncloseDoubleQuotesIfNeed(field As String) As String
        If NeedEncloseDoubleQuotes(field) Then
            Return EncloseDoubleQuotes(field)
        End If
        Return field
    End Function

    Private Function NeedEncloseDoubleQuotes(field As String) As Boolean
        Return field.IndexOf(""""c) > -1 OrElse
        field.IndexOf(","c) > -1 OrElse
        field.IndexOf(ControlChars.Cr) > -1 OrElse
        field.IndexOf(ControlChars.Lf) > -1 OrElse
        field.StartsWith(" ") OrElse
        field.StartsWith(vbTab) OrElse
        field.EndsWith(" ") OrElse
        field.EndsWith(vbTab)
    End Function

    Private Sub RbtnQuotation_CheckedChanged(sender As Object, e As EventArgs) Handles RbtnQuotation.CheckedChanged
        getList()
    End Sub

    Private Sub RbtnJobOrder_CheckedChanged(sender As Object, e As EventArgs) Handles RbtnJobOrder.CheckedChanged
        getList()
    End Sub

    Private Sub RbtnSales_CheckedChanged(sender As Object, e As EventArgs) Handles RbtnSales.CheckedChanged
        getList()
    End Sub

    Private Sub DtpDateSince_ValueChanged(sender As Object, e As EventArgs) Handles DtpDateSince.ValueChanged
        getList()
    End Sub

    Private Sub DtpDateUntil_ValueChanged(sender As Object, e As EventArgs) Handles DtpDateUntil.ValueChanged
        getList()
    End Sub

    'param1：String テーブル名
    'param2：String 詳細条件
    'Return: DataSet
    Private Function getDsData(ByVal tableName As String, Optional ByRef txtParam As String = "") As DataSet
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Sql += "SELECT * FROM public." & tableName
        Sql += " WHERE "
        Sql += "会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += txtParam
        Return _db.selectDB(Sql, RS, reccnt)
    End Function

    ''' ------------------------------------------------------------------------
    ''' <summary>
    '''     指定した精度の数値に切り捨てします。</summary>
    ''' <param name="dValue">
    '''     丸め対象の倍精度浮動小数点数。</param>
    ''' <param name="iDigits">
    '''     戻り値の有効桁数の精度。</param>
    ''' <returns>
    '''     iDigits に等しい精度の数値に切り捨てられた数値。</returns>
    ''' ------------------------------------------------------------------------
    Public Shared Function ToRoundDown(ByVal dValue As Double, ByVal iDigits As Integer) As Double
        Dim dCoef As Double = System.Math.Pow(10, iDigits)

        If dValue > 0 Then
            Return System.Math.Floor(dValue * dCoef) / dCoef
        Else
            Return System.Math.Ceiling(dValue * dCoef) / dCoef
        End If
    End Function

    ''' <summary>
    ''' コンボボックスに値を設定する
    ''' </summary>
    ''' <param name="combo">値を設定するコンボボックスコントロール</param>
    Private Sub setComboBox(ByVal combo As ComboBox)
        '=========================================
        '初期化
        '=========================================
        'コンボボックスの表示アイテムをクリア
        combo.Items.Clear()
        combo.DisplayMember = "Key" '表示値としてDataSourceの'Key'を利用
        combo.ValueMember = "Value" '値としてDataSourceの'Value'を利用

        '=========================================
        '設定するデータソースの準備
        '=========================================
        Dim dic As New Dictionary(Of String, Integer)

        Dim dtToday As DateTime = DateTime.Today

        If combo.Items.Count() = 0 Then
            If combo.Name = "cmbYear" Then
                Dim nowDate As Integer = Integer.Parse(dtToday.Year)
                For i As Integer = CommonConst.SINCE_DEFAULT_YEAR To nowDate
                    dic(i.ToString) = i  '表示値 = 値
                Next
            Else
                For i As Integer = 1 To 12
                    dic(i.ToString) = i  '表示値 = 値
                Next
            End If
        End If
        ''ダミー行が欲しい場合(未選択時の空白とか)は以下の様に入れとくと便利
        'dic("") = -1 '表示値:空白(未設定) => 値:-1

        '=========================================
        'データソースをコンボボックスへ設定
        '=========================================
        combo.DataSource = New BindingSource(dic, Nothing)
    End Sub

    'Excel出力する際のチェック
    Private Function excelOutput(ByVal prmFilePath As String)
        Dim fileChk As String = Dir(prmFilePath)
        '同名ファイルがあるかどうかチェック
        If fileChk <> "" Then
            Dim result = _msgHd.dspMSG("confirmFileExist", frmC01F10_Login.loginValue.Language, prmFilePath)
            If result = DialogResult.No Then
                Return False
            End If

            Try
                'ファイルが開けるかどうかチェック
                Dim sr As StreamReader = New StreamReader(prmFilePath)
                sr.Close() '処理が通ったら閉じる
            Catch ex As Exception
                '開けない場合はアラートを表示してリターンさせる
                MessageBox.Show(ex.Message, CommonConst.AP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try

            Return True
        End If
        Return True
    End Function

End Class