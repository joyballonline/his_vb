Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class PaymentList
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
    Private _langHd As UtilLangHandler
    Private _db As UtilDBIf
    'Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ

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
                   ByRef prmRefLang As UtilLangHandler)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True
        DgvSupplier.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells
    End Sub

    Private Sub MstSuppliere_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Sql1 As String = ""
        Dim Sql2 As String = ""
        Dim Sql3 As String = ""
        Dim reccnt As Integer = 0

        Sql1 += "SELECT "
        Sql1 += "* "
        Sql1 += "FROM "
        Sql1 += "public"
        Sql1 += "."
        Sql1 += "m11_supplier"

        Dim ds1 As DataSet = _db.selectDB(Sql1, RS, reccnt)
        Dim Count As Integer = 0
        Dim SupplierCount As Integer = ds1.Tables(RS).Rows.Count
        Dim SupplierOrderCount(SupplierCount) As Integer
        Dim SupplierBillingCount(SupplierCount) As Integer
        Dim SupplierBillingAmount(SupplierCount) As Integer
        Dim SupplierOrderAmount(SupplierCount) As Integer
        Dim AccountsReceivable(SupplierCount) As Integer

        For index1 As Integer = 0 To ds1.Tables(RS).Rows.Count - 1
            Sql2 += "SELECT "
            Sql2 += "* "
            Sql2 += "FROM "
            Sql2 += "public"
            Sql2 += "."
            Sql2 += "t46_kikehd"
            Sql2 += " WHERE "
            Sql2 += "会社コード"
            Sql2 += " = "
            Sql2 += "'"
            Sql2 += ds1.Tables(RS).Rows(index1)("会社コード")
            Sql2 += "'"
            Sql2 += " AND "
            Sql2 += "仕入先コード"
            Sql2 += " ILIKE "
            Sql2 += "'%"
            Sql2 += ds1.Tables(RS).Rows(index1)("仕入先コード")
            Sql2 += "%'"
            Dim ds2 As DataSet = _db.selectDB(Sql2, RS, reccnt)
            SupplierBillingCount(index1) = ds2.Tables(RS).Rows.Count.ToString

            Sql3 += "SELECT "
            Sql3 += "* "
            Sql3 += "FROM "
            Sql3 += "public"
            Sql3 += "."
            Sql3 += "t20_hattyu"
            Sql3 += " WHERE "
            Sql3 += "会社コード"
            Sql3 += " = "
            Sql3 += "'"
            Sql3 += ds1.Tables(RS).Rows(index1)("会社コード")
            Sql3 += "'"
            Sql3 += " AND "
            Sql3 += "仕入先コード"
            Sql3 += " ILIKE "
            Sql3 += "'%"
            Sql3 += ds1.Tables(RS).Rows(index1)("仕入先コード")
            Sql3 += "%'"
            Dim ds3 As DataSet = _db.selectDB(Sql3, RS, reccnt)

            SupplierOrderCount(index1) = ds3.Tables(RS).Rows.Count.ToString

            For index2 As Integer = 0 To ds2.Tables(RS).Rows.Count - 1
                SupplierBillingAmount(index1) += ds2.Tables(RS).Rows(index2)("買掛金額計")
            Next

            For index3 As Integer = 0 To ds3.Tables(RS).Rows.Count - 1
                SupplierOrderAmount(index1) += ds3.Tables(RS).Rows(index3)("仕入金額")
            Next

            For index4 As Integer = 0 To ds2.Tables(RS).Rows.Count - 1
                AccountsReceivable(index1) += ds2.Tables(RS).Rows(index4)("買掛残高")
            Next

            If SupplierOrderCount(index1) > 0 Then
                DgvSupplier.Rows.Add()
                DgvSupplier.Rows(Count).Cells("仕入先名").Value = ds1.Tables(RS).Rows(index1)("仕入先名")
                DgvSupplier.Rows(Count).Cells("仕入金額計").Value = SupplierOrderAmount(index1)
                DgvSupplier.Rows(Count).Cells("支払残高").Value = SupplierOrderAmount(index1) - SupplierBillingAmount(index1)
                DgvSupplier.Rows(Count).Cells("仕入先コード").Value = ds1.Tables(RS).Rows(index1)("仕入先コード")
                DgvSupplier.Rows(Count).Cells("会社コード").Value = ds1.Tables(RS).Rows(index1)("会社コード")

                Count += 1
            End If
            Sql2 = ""
            Sql3 = ""
        Next
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim frmMenu As frmC01F30_Menu
        frmMenu = New frmC01F30_Menu(_msgHd, _langHd, _db)
        frmMenu.Show()
        Me.Close()
    End Sub

    Private Sub BtnDeposit_Click(sender As Object, e As EventArgs) Handles BtnPayment.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvSupplier.CurrentCell.RowIndex
        Dim Company As String = DgvSupplier.Rows(RowIdx).Cells("会社コード").Value
        Dim Supplier As String = DgvSupplier.Rows(RowIdx).Cells("仕入先コード").Value
        Dim Name As String = DgvSupplier.Rows(RowIdx).Cells("仕入先名").Value
        Dim openForm As Form = Nothing
        openForm = New Payment(_msgHd, _db, _langHd, Me, Company, Supplier, Name)   '処理選択
        openForm.Show(Me)
    End Sub
End Class