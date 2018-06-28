Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class PurchasingManagement
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
    Private _db As UtilDBIf
    'Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ
    Private CompanyCode As String = ""
    Private No As String = ""
    Private _langHd As UtilLangHandler

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
                   ByRef prmRefNo As String)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        No = prmRefNo
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示

        _init = True

    End Sub

    Private Sub PurchaseManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Sql1 As String = ""
        Dim Sql2 As String = ""
        Dim Sql3 As String = ""
        Try
            Sql1 += "SELECT "
            Sql1 += "* "
            Sql1 += "FROM "
            Sql1 += "public"
            Sql1 += "."
            Sql1 += "t20_hattyu"
            Sql1 += " WHERE "
            Sql1 += "発注番号"
            Sql1 += " ILIKE "
            Sql1 += "'"
            Sql1 += No
            Sql1 += "'"

            Sql2 += "SELECT "
            Sql2 += "* "
            Sql2 += "FROM "
            Sql2 += "public"
            Sql2 += "."
            Sql2 += "t41_siredt"
            Sql2 += " WHERE "
            Sql2 += "発注番号"
            Sql2 += " ILIKE "
            Sql2 += "'"
            Sql2 += No
            Sql2 += "'"

            Sql3 += "SELECT "
            Sql3 += "* "
            Sql3 += "FROM "
            Sql3 += "public"
            Sql3 += "."
            Sql3 += "t21_hattyu"
            Sql3 += " WHERE "
            Sql3 += "発注番号"
            Sql3 += " ILIKE "
            Sql3 += "'"
            Sql3 += No
            Sql3 += "'"

            Dim reccnt As Integer = 0
            Dim ds1 As DataSet = _db.selectDB(Sql1, RS, reccnt)
            Dim ds2 As DataSet = _db.selectDB(Sql2, RS, reccnt)
            Dim ds3 As DataSet = _db.selectDB(Sql3, RS, reccnt)

            DgvPurchase.Columns.Add("明細", "明細")
            DgvPurchase.Columns.Add("メーカー", "メーカー")
            DgvPurchase.Columns.Add("品名", "品名")
            DgvPurchase.Columns.Add("型式", "型式")
            DgvPurchase.Columns.Add("発注数量", "発注数量")
            DgvPurchase.Columns.Add("単位", "単位")
            DgvPurchase.Columns.Add("仕入数量", "仕入数量")
            DgvPurchase.Columns.Add("仕入単価", "仕入単価")
            DgvPurchase.Columns.Add("仕入金額", "仕入金額")
            DgvPurchase.Columns.Add("発注残数", "発注残数")

            DgvPurchase.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvPurchase.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvPurchase.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvPurchase.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvPurchase.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For index As Integer = 0 To ds3.Tables(RS).Rows.Count - 1
                DgvPurchase.Rows.Add()
                DgvPurchase.Rows(index).Cells(1).Value = ds3.Tables(RS).Rows(index)("メーカー")
                DgvPurchase.Rows(index).Cells(2).Value = ds3.Tables(RS).Rows(index)("品名")
                DgvPurchase.Rows(index).Cells(3).Value = ds3.Tables(RS).Rows(index)("型式")
                DgvPurchase.Rows(index).Cells(4).Value = ds3.Tables(RS).Rows(index)("発注数量")
                DgvPurchase.Rows(index).Cells(5).Value = ds3.Tables(RS).Rows(index)("単位")
                DgvPurchase.Rows(index).Cells(6).Value = ds3.Tables(RS).Rows(index)("仕入数量")
                DgvPurchase.Rows(index).Cells(7).Value = ds3.Tables(RS).Rows(index)("仕入単価")
                DgvPurchase.Rows(index).Cells(8).Value = ds3.Tables(RS).Rows(index)("仕入金額")
                DgvPurchase.Rows(index).Cells(9).Value = ds3.Tables(RS).Rows(index)("発注残数")
            Next

            DgvHistory.Columns.Add("No", "No")
            DgvHistory.Columns.Add("仕入番号", "仕入番号")
            DgvHistory.Columns.Add("行番号", "行番号")
            DgvHistory.Columns.Add("仕入区分", "仕入区分")
            DgvHistory.Columns.Add("メーカー", "メーカー")
            DgvHistory.Columns.Add("品名", "品名")
            DgvHistory.Columns.Add("型式", "型式")
            DgvHistory.Columns.Add("単位", "単位")
            DgvHistory.Columns.Add("仕入先", "仕入先")
            DgvHistory.Columns.Add("仕入値", "仕入値")
            DgvHistory.Columns.Add("仕入数量", "仕入数量")
            DgvHistory.Columns.Add("備考", "備考")

            DgvHistory.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvHistory.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For index As Integer = 0 To ds2.Tables(RS).Rows.Count - 1
                DgvHistory.Rows.Add()
                DgvHistory.Rows(index).Cells(1).Value = ds2.Tables(RS).Rows(index)("仕入番号")
                DgvHistory.Rows(index).Cells(1).Value = ds2.Tables(RS).Rows(index)("行番号")
                DgvHistory.Rows(index).Cells(1).Value = ds2.Tables(RS).Rows(index)("仕入区分")
                DgvHistory.Rows(index).Cells(1).Value = ds2.Tables(RS).Rows(index)("メーカー")
                DgvHistory.Rows(index).Cells(2).Value = ds2.Tables(RS).Rows(index)("品名")
                DgvHistory.Rows(index).Cells(3).Value = ds2.Tables(RS).Rows(index)("型式")
                DgvHistory.Rows(index).Cells(5).Value = ds2.Tables(RS).Rows(index)("単位")
                DgvHistory.Rows(index).Cells(6).Value = ds2.Tables(RS).Rows(index)("仕入先")
                DgvHistory.Rows(index).Cells(7).Value = ds2.Tables(RS).Rows(index)("仕入値")
                DgvHistory.Rows(index).Cells(8).Value = ds2.Tables(RS).Rows(index)("仕入数量")
                DgvHistory.Rows(index).Cells(9).Value = ds2.Tables(RS).Rows(index)("備考")
            Next

            DgvAdd.Columns.Add("No", "No")
            DgvAdd.Columns.Add("行番号", "行番号")
            DgvAdd.Columns.Add("仕入区分", "仕入区分")
            DgvAdd.Columns.Add("メーカー", "メーカー")
            DgvAdd.Columns.Add("品名", "品名")
            DgvAdd.Columns.Add("型式", "型式")
            DgvAdd.Columns.Add("単位", "単位")
            DgvAdd.Columns.Add("仕入先", "仕入先")
            DgvAdd.Columns.Add("仕入値", "仕入値")
            DgvAdd.Columns.Add("仕入数量", "仕入数量")
            DgvAdd.Columns.Add("備考", "備考")

            DgvAdd.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvAdd.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For index As Integer = 0 To ds3.Tables(RS).Rows.Count - 1
                DgvAdd.Rows.Add()
                DgvAdd.Rows(index).Cells(1).Value = ds3.Tables(RS).Rows(index)("行番号")
                DgvAdd.Rows(index).Cells(2).Value = ds3.Tables(RS).Rows(index)("仕入区分")
                DgvAdd.Rows(index).Cells(3).Value = ds3.Tables(RS).Rows(index)("メーカー")
                DgvAdd.Rows(index).Cells(4).Value = ds3.Tables(RS).Rows(index)("品名")
                DgvAdd.Rows(index).Cells(5).Value = ds3.Tables(RS).Rows(index)("型式")
                DgvAdd.Rows(index).Cells(6).Value = ds3.Tables(RS).Rows(index)("仕入先名")
                DgvAdd.Rows(index).Cells(7).Value = ds3.Tables(RS).Rows(index)("単位")
                DgvAdd.Rows(index).Cells(8).Value = ds3.Tables(RS).Rows(index)("仕入値")
                DgvAdd.Rows(index).Cells(9).Value = ds3.Tables(RS).Rows(index)("仕入数量")
                DgvAdd.Rows(index).Cells(10).Value = ds3.Tables(RS).Rows(index)("備考")
            Next

            '行番号の振り直し
            Dim i1 As Integer = DgvPurchase.Rows.Count()
            Dim No1 As Integer = 1
            For c As Integer = 0 To i1 - 1
                DgvPurchase.Rows(c).Cells(0).Value = No1
                No1 += 1
            Next c
            TxtCount1.Text = DgvPurchase.Rows.Count()

            Dim i2 As Integer = DgvHistory.Rows.Count()
            Dim No2 As Integer = 1
            For c As Integer = 0 To i2 - 1
                DgvHistory.Rows(c).Cells(0).Value = No2
                No2 += 1
            Next c
            TxtCount2.Text = DgvHistory.Rows.Count()

            Dim i3 As Integer = DgvAdd.Rows.Count()
            Dim No3 As Integer = 1
            For c As Integer = 0 To i3 - 1
                DgvAdd.Rows(c).Cells(0).Value = No3
                No3 += 1
            Next c
            TxtCount3.Text = DgvAdd.Rows.Count()

            TxtPurchaseNo.Text = ds1.Tables(RS).Rows(0)("発注番号")
            TxtOrdingDate.Text = ds1.Tables(RS).Rows(0)("発注日")
            TxtSupplierCode.Text = ds1.Tables(RS).Rows(0)("仕入先コード")
            TxtSupplierName.Text = ds1.Tables(RS).Rows(0)("仕入先名")

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        'Dim openForm As Form = Nothing
        'openForm = New frmC01F30_Menu(_msgHd, _langHd, _db)
        'openForm.Show()
        Me.Close()
    End Sub

    Private Sub BtnRegist_Click(sender As Object, e As EventArgs) Handles BtnRegist.Click

    End Sub
End Class