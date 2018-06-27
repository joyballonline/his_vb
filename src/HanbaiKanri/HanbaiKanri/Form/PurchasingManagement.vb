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
                   ByRef prmRefNo As String)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        No = prmRefNo
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示

        _init = True

    End Sub

    Private Sub PurchaseManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Sql1 As String = ""
        Dim Sql2 As String = ""
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
            Sql2 += "t21_hattyu"
            Sql2 += " WHERE "
            Sql2 += "発注番号"
            Sql2 += " ILIKE "
            Sql2 += "'"
            Sql2 += No
            Sql2 += "'"

            Dim reccnt As Integer = 0
            Dim ds1 As DataSet = _db.selectDB(Sql1, RS, reccnt)
            Dim ds2 As DataSet = _db.selectDB(Sql2, RS, reccnt)

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

            For index As Integer = 0 To ds1.Tables(RS).Rows.Count - 1
                DgvPurchase.Rows.Add()
                DgvPurchase.Rows(index).Cells(1).Value = ds1.Tables(RS).Rows(index)("メーカー")
                DgvPurchase.Rows(index).Cells(2).Value = ds1.Tables(RS).Rows(index)("品名")
                DgvPurchase.Rows(index).Cells(3).Value = ds1.Tables(RS).Rows(index)("型式")
                DgvPurchase.Rows(index).Cells(4).Value = ds1.Tables(RS).Rows(index)("発注数量")
                DgvPurchase.Rows(index).Cells(5).Value = ds1.Tables(RS).Rows(index)("単位")
                DgvPurchase.Rows(index).Cells(6).Value = ds1.Tables(RS).Rows(index)("仕入数量")
                DgvPurchase.Rows(index).Cells(7).Value = ds1.Tables(RS).Rows(index)("仕入単価")
                DgvPurchase.Rows(index).Cells(8).Value = ds1.Tables(RS).Rows(index)("仕入金額")
                DgvPurchase.Rows(index).Cells(9).Value = ds1.Tables(RS).Rows(index)("発注残数")
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        Dim openForm As Form = Nothing
        openForm = New frmC01F30_Menu(_msgHd, _langHd, _db)
        openForm.Show()
        Me.Close()
    End Sub

End Class