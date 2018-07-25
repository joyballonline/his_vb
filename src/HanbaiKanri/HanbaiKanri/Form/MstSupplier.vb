Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class MstSupplier
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

    End Sub

    Private Sub MstSupplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += "会社コード, "
            Sql += "仕入先コード, "
            Sql += "仕入先名, "
            Sql += "仕入先名略称, "
            Sql += "郵便番号, "
            Sql += "住所１, "
            Sql += "住所２, "
            Sql += "住所３, "
            Sql += "電話番号, "
            Sql += "電話番号検索用, "
            Sql += "ＦＡＸ番号, "
            Sql += "担当者名, "
            Sql += "既定間接費率, "
            Sql += "メモ, "
            Sql += "銀行コード, "
            Sql += "支店コード, "
            Sql += "預金種目, "
            Sql += "口座番号, "
            Sql += "口座名義, "
            Sql += "更新者, "
            Sql += "更新日 "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "m11_supplier"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                Dgv_Supplier.Rows.Add()
                Dgv_Supplier.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)(0)
                Dgv_Supplier.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)(1)
                Dgv_Supplier.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)(2)
                Dgv_Supplier.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)(3)
                Dgv_Supplier.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)(4)
                Dgv_Supplier.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)(5)
                Dgv_Supplier.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)(6)
                Dgv_Supplier.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)(7)
                Dgv_Supplier.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)(8)
                Dgv_Supplier.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)(9)
                Dgv_Supplier.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)(10)
                Dgv_Supplier.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)(11)
                Dgv_Supplier.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)(12)
                Dgv_Supplier.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)(13)
                Dgv_Supplier.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)(14)
                Dgv_Supplier.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)(15)
                Dgv_Supplier.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)(16)
                Dgv_Supplier.Rows(index).Cells(17).Value = ds.Tables(RS).Rows(index)(17)
                Dgv_Supplier.Rows(index).Cells(18).Value = ds.Tables(RS).Rows(index)(18)
                Dgv_Supplier.Rows(index).Cells(19).Value = ds.Tables(RS).Rows(index)(19)
                Dgv_Supplier.Rows(index).Cells(20).Value = ds.Tables(RS).Rows(index)(20)
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Sub btnSupplierAdd_Click(sender As Object, e As EventArgs) Handles btnSupplierAdd.Click
        Dim openForm As Form = Nothing
        Dim Status As String = "ADD"

        openForm = New Supplier(_msgHd, _db, _langHd, Status)   '処理選択
        openForm.Show()
        Me.Hide()   ' 自分は隠れる
    End Sub

    Private Sub btnSupplierEdit_Click(sender As Object, e As EventArgs) Handles btnSupplierEdit.Click
        Dim openForm As Form = Nothing
        Dim Status As String = "EDIT"
        Dim CompanyCode As String = Dgv_Supplier.Rows(Dgv_Supplier.CurrentCell.RowIndex).Cells("会社コード").Value
        Dim SupplierCode As String = Dgv_Supplier.Rows(Dgv_Supplier.CurrentCell.RowIndex).Cells("仕入先コード").Value
        openForm = New Supplier(_msgHd, _db, _langHd, Status, CompanyCode, SupplierCode)   '処理選択
        openForm.Show()
        Me.Hide()   ' 自分は隠れる
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        Dim frmMenu As frmC01F30_Menu
        frmMenu = New frmC01F30_Menu(_msgHd, _langHd, _db)
        frmMenu.Show()
        Me.Close()
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        Dgv_Supplier.Rows.Clear()

        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "m11_supplier"
            Sql += " WHERE "
            Sql += "会社コード"
            Sql += " ILIKE "
            Sql += "'%"
            Sql += Search.Text
            Sql += "%'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                Dgv_Supplier.Rows.Add()
                Dgv_Supplier.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)(0)
                Dgv_Supplier.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)(1)
                Dgv_Supplier.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)(2)
                Dgv_Supplier.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)(3)
                Dgv_Supplier.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)(4)
                Dgv_Supplier.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)(5)
                Dgv_Supplier.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)(6)
                Dgv_Supplier.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)(7)
                Dgv_Supplier.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)(8)
                Dgv_Supplier.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)(9)
                Dgv_Supplier.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)(10)
                Dgv_Supplier.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)(11)
                Dgv_Supplier.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)(12)
                Dgv_Supplier.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)(13)
                Dgv_Supplier.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)(14)
                Dgv_Supplier.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)(15)
                Dgv_Supplier.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)(16)
                Dgv_Supplier.Rows(index).Cells(17).Value = ds.Tables(RS).Rows(index)(17)
                Dgv_Supplier.Rows(index).Cells(18).Value = ds.Tables(RS).Rows(index)(18)
                Dgv_Supplier.Rows(index).Cells(19).Value = ds.Tables(RS).Rows(index)(19)
                Dgv_Supplier.Rows(index).Cells(20).Value = ds.Tables(RS).Rows(index)(20)
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub
End Class