Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class CustomerOrderList
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
    'Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ
    Private CompanyCode As String = ""
    Private OrderingNo As String()
    Private CustomerCode As String = ""

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
                   ByRef prmRefCompany As String,
                   ByRef prmRefCustomer As String)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        CompanyCode = prmRefCompany
        CustomerCode = prmRefCustomer
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub
    Private Sub PurchaseListLoad(Optional ByRef Status As String = "")
        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t23_skyuhd"
            Sql += " WHERE "
            Sql += "取消区分"
            Sql += " = "
            Sql += "'"
            Sql += "0"
            Sql += "'"
            Sql += " AND "
            Sql += "会社コード"
            Sql += " = "
            Sql += "'"
            Sql += CompanyCode
            Sql += "'"
            Sql += " AND "
            Sql += "得意先コード"
            Sql += " = "
            Sql += "'"
            Sql += CustomerCode
            Sql += "'"

            Dim reccnt As Integer = 0
            ds = _db.selectDB(Sql, RS, reccnt)
            DgvBilling.Columns.Add("請求番号", "請求番号")
            DgvBilling.Columns.Add("請求区分", "請求区分")
            DgvBilling.Columns.Add("請求日", "請求日")
            DgvBilling.Columns.Add("受注番号", "受注番号")
            DgvBilling.Columns.Add("受注番号枝番", "受注番号枝番")
            DgvBilling.Columns.Add("得意先コード", "得意先コード")
            DgvBilling.Columns.Add("得意先名", "得意先名")
            DgvBilling.Columns.Add("請求金額計", "請求金額計")
            DgvBilling.Columns.Add("売掛残高", "売掛残高")
            DgvBilling.Columns.Add("備考1", "備考1")
            DgvBilling.Columns.Add("備考2", "備考2")
            DgvBilling.Columns.Add("登録日", "登録日")
            DgvBilling.Columns.Add("更新者", "更新者")

            DgvBilling.Columns("請求金額計").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvBilling.Columns("売掛残高").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvBilling.Rows.Add()
                DgvBilling.Rows(index).Cells("請求番号").Value = ds.Tables(RS).Rows(index)("請求番号")
                DgvBilling.Rows(index).Cells("請求区分").Value = ds.Tables(RS).Rows(index)("請求区分")
                DgvBilling.Rows(index).Cells("請求日").Value = ds.Tables(RS).Rows(index)("請求日")
                DgvBilling.Rows(index).Cells("受注番号").Value = ds.Tables(RS).Rows(index)("受注番号")
                DgvBilling.Rows(index).Cells("受注番号枝番").Value = ds.Tables(RS).Rows(index)("受注番号枝番")
                DgvBilling.Rows(index).Cells("得意先コード").Value = ds.Tables(RS).Rows(index)("得意先コード")
                DgvBilling.Rows(index).Cells("得意先名").Value = ds.Tables(RS).Rows(index)("得意先名")
                DgvBilling.Rows(index).Cells("請求金額計").Value = ds.Tables(RS).Rows(index)("請求金額計")
                DgvBilling.Rows(index).Cells("売掛残高").Value = ds.Tables(RS).Rows(index)("売掛残高")
                DgvBilling.Rows(index).Cells("備考1").Value = ds.Tables(RS).Rows(index)("備考1")
                DgvBilling.Rows(index).Cells("備考2").Value = ds.Tables(RS).Rows(index)("備考2")
                DgvBilling.Rows(index).Cells("登録日").Value = ds.Tables(RS).Rows(index)("登録日")
                DgvBilling.Rows(index).Cells("更新者").Value = ds.Tables(RS).Rows(index)("更新者")
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub
    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PurchaseListLoad()
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        Dim openForm As Form = Nothing
        openForm = New frmC01F30_Menu(_msgHd, _langHd, _db)
        openForm.Show()
        Me.Close()
    End Sub

    Private Sub BtnInvoice_Click(sender As Object, e As EventArgs) Handles BtnInvoice.Click '請求書発行
        Dim BillingNo As String = ""
        Dim BillingSuffix As String = ""
        Dim BillingSubTotal As Integer = 0
        Dim Sql1 As String = ""
        Dim Sql2 As String = ""
        Dim Sql3 As String = ""
        Dim reccnt As Integer = 0

        For Each r As DataGridViewRow In DgvBilling.SelectedRows
            BillingNo = DgvBilling.Rows(r.Index).Cells("受注番号").Value
            BillingSuffix = DgvBilling.Rows(r.Index).Cells("受注番号枝番").Value
            BillingSubTotal += DgvBilling.Rows(r.Index).Cells("請求金額計").Value

            Sql1 += "SELECT "
            Sql1 += "* "
            Sql1 += "FROM "
            Sql1 += "public"
            Sql1 += "."
            Sql1 += "t10_cymnhd"
            Sql1 += " WHERE "
            Sql1 += "受注番号"
            Sql1 += " = "
            Sql1 += "'"
            Sql1 += BillingNo
            Sql1 += "'"
            Sql1 += " AND "
            Sql1 += "受注番号枝番"
            Sql1 += " = "
            Sql1 += "'"
            Sql1 += BillingSuffix
            Sql1 += "'"

            Dim ds1 As DataSet = _db.selectDB(Sql1, RS, reccnt) '受注基本（請求債情報）

            Sql2 += "SELECT "
            Sql2 += "* "
            Sql2 += "FROM "
            Sql2 += "public"
            Sql2 += "."
            Sql2 += "t11_cymndt"
            Sql2 += " WHERE "
            Sql2 += "受注番号"
            Sql2 += " = "
            Sql2 += "'"
            Sql2 += BillingNo
            Sql2 += "'"
            Sql2 += " AND "
            Sql2 += "受注番号枝番"
            Sql2 += " = "
            Sql2 += "'"
            Sql2 += BillingSuffix
            Sql2 += "'"

            Dim ds2 As DataSet = _db.selectDB(Sql2, RS, reccnt)　'受注明細（商品情報）

            Sql1 = ""
            Sql2 = ""
        Next r

        Sql3 += "SELECT "
        Sql3 += "* "
        Sql3 += "FROM "
        Sql3 += "public"
        Sql3 += "."
        Sql3 += "m01_company"
        Sql3 += " WHERE "
        Sql3 += "会社コード"
        Sql3 += " = "
        Sql3 += "'"
        Sql3 += frmC01F10_Login.loginValue.BumonNM
        Sql3 += "'"

        Dim ds3 As DataSet = _db.selectDB(Sql3, RS, reccnt)　'会社情報（振込先情報）
    End Sub
End Class