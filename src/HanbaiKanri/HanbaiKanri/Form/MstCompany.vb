Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class MstCompany
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
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByRef prmRefLang As UtilLangHandler)
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

    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "m01_company"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                Dgv_Company.Rows.Add()
                Dgv_Company.Rows(index).Cells("会社コード").Value = ds.Tables(RS).Rows(index)("会社コード")
                Dgv_Company.Rows(index).Cells("会社名").Value = ds.Tables(RS).Rows(index)("会社名")
                Dgv_Company.Rows(index).Cells("会社略称").Value = ds.Tables(RS).Rows(index)("会社略称")
                Dgv_Company.Rows(index).Cells("郵便番号").Value = ds.Tables(RS).Rows(index)("郵便番号")
                Dgv_Company.Rows(index).Cells("住所１").Value = ds.Tables(RS).Rows(index)("住所１")
                Dgv_Company.Rows(index).Cells("住所２").Value = ds.Tables(RS).Rows(index)("住所２")
                Dgv_Company.Rows(index).Cells("住所３").Value = ds.Tables(RS).Rows(index)("住所３")
                Dgv_Company.Rows(index).Cells("電話番号").Value = ds.Tables(RS).Rows(index)("電話番号")
                Dgv_Company.Rows(index).Cells("FAX番号").Value = ds.Tables(RS).Rows(index)("ＦＡＸ番号")
                Dgv_Company.Rows(index).Cells("代表者役職").Value = ds.Tables(RS).Rows(index)("代表者役職")
                Dgv_Company.Rows(index).Cells("代表者名").Value = ds.Tables(RS).Rows(index)("代表者名")
                Dgv_Company.Rows(index).Cells("表示順").Value = ds.Tables(RS).Rows(index)("表示順")
                Dgv_Company.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
                Dgv_Company.Rows(index).Cells("更新者").Value = ds.Tables(RS).Rows(index)("更新者")
                Dgv_Company.Rows(index).Cells("更新日").Value = ds.Tables(RS).Rows(index)("更新日")
                Dgv_Company.Rows(index).Cells("銀行コード").Value = ds.Tables(RS).Rows(index)("銀行コード")
                Dgv_Company.Rows(index).Cells("支店コード").Value = ds.Tables(RS).Rows(index)("支店コード")
                Dgv_Company.Rows(index).Cells("預金種目").Value = ds.Tables(RS).Rows(index)("預金種目")
                Dgv_Company.Rows(index).Cells("口座番号").Value = ds.Tables(RS).Rows(index)("口座番号")
                Dgv_Company.Rows(index).Cells("口座名義").Value = ds.Tables(RS).Rows(index)("口座名義")
                Dgv_Company.Rows(index).Cells("銀行名").Value = ds.Tables(RS).Rows(index)("銀行名")
                Dgv_Company.Rows(index).Cells("支店名").Value = ds.Tables(RS).Rows(index)("支店名")
                Dgv_Company.Rows(index).Cells("前回締日").Value = ds.Tables(RS).Rows(index)("前回締日")
                Dgv_Company.Rows(index).Cells("今回締日").Value = ds.Tables(RS).Rows(index)("今回締日")
                Dgv_Company.Rows(index).Cells("次回締日").Value = ds.Tables(RS).Rows(index)("次回締日")
                Dgv_Company.Rows(index).Cells("在庫単価評価法").Value = ds.Tables(RS).Rows(index)("在庫単価評価法")
                Dgv_Company.Rows(index).Cells("前払法人税率").Value = ds.Tables(RS).Rows(index)("前払法人税率")

            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Sub btnCompanyrAdd_Click(sender As Object, e As EventArgs) Handles btnCompanyAdd.Click
        Dim openForm As Form = Nothing
        Dim Status As String = "ADD"
        openForm = New Company(_msgHd, _db, _langHd, Status)   '処理選択
        openForm.Show()
        Me.Hide()   ' 自分は隠れる
    End Sub

    Private Sub btnSelectCompany_Click(sender As Object, e As EventArgs) Handles btnSelectCompany.Click
        Dim openForm As Form = Nothing
        Dim Status As String = "EDIT"
        Dim CompanyCode As String = Dgv_Company.Rows(Dgv_Company.CurrentCell.RowIndex).Cells("会社コード").Value
        openForm = New Company(_msgHd, _db, _langHd, Status, CompanyCode)   '処理選択
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
        Dgv_Company.Rows.Clear()

        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "m01_company"
            Sql += " WHERE "
            Sql += "会社名"
            Sql += " ILIKE "
            Sql += "'%"
            Sql += Search.Text
            Sql += "%'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                Dgv_Company.Rows.Add()
                Dgv_Company.Rows(index).Cells("会社コード").Value = ds.Tables(RS).Rows(index)("会社コード")
                Dgv_Company.Rows(index).Cells("会社名").Value = ds.Tables(RS).Rows(index)("会社名")
                Dgv_Company.Rows(index).Cells("会社略称").Value = ds.Tables(RS).Rows(index)("会社略称")
                Dgv_Company.Rows(index).Cells("郵便番号").Value = ds.Tables(RS).Rows(index)("郵便番号")
                Dgv_Company.Rows(index).Cells("住所１").Value = ds.Tables(RS).Rows(index)("住所１")
                Dgv_Company.Rows(index).Cells("住所２").Value = ds.Tables(RS).Rows(index)("住所２")
                Dgv_Company.Rows(index).Cells("住所３").Value = ds.Tables(RS).Rows(index)("住所３")
                Dgv_Company.Rows(index).Cells("電話番号").Value = ds.Tables(RS).Rows(index)("電話番号")
                Dgv_Company.Rows(index).Cells("ＦＡＸ番号").Value = ds.Tables(RS).Rows(index)("ＦＡＸ番号")
                Dgv_Company.Rows(index).Cells("代表者役職").Value = ds.Tables(RS).Rows(index)("代表者役職")
                Dgv_Company.Rows(index).Cells("代表者名").Value = ds.Tables(RS).Rows(index)("代表者名")
                Dgv_Company.Rows(index).Cells("表示順").Value = ds.Tables(RS).Rows(index)("表示順")
                Dgv_Company.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
                Dgv_Company.Rows(index).Cells("更新者").Value = ds.Tables(RS).Rows(index)("更新者")
                Dgv_Company.Rows(index).Cells("更新日").Value = ds.Tables(RS).Rows(index)("更新日")
                Dgv_Company.Rows(index).Cells("銀行コード").Value = ds.Tables(RS).Rows(index)("銀行コード")
                Dgv_Company.Rows(index).Cells("支店コード").Value = ds.Tables(RS).Rows(index)("支店コード")
                Dgv_Company.Rows(index).Cells("預金種目").Value = ds.Tables(RS).Rows(index)("預金種目")
                Dgv_Company.Rows(index).Cells("口座番号").Value = ds.Tables(RS).Rows(index)("口座番号")
                Dgv_Company.Rows(index).Cells("口座名義").Value = ds.Tables(RS).Rows(index)("口座名義")
                Dgv_Company.Rows(index).Cells("銀行名").Value = ds.Tables(RS).Rows(index)("銀行名")
                Dgv_Company.Rows(index).Cells("支店名").Value = ds.Tables(RS).Rows(index)("支店名")
                Dgv_Company.Rows(index).Cells("前回締日").Value = ds.Tables(RS).Rows(index)("前回締日")
                Dgv_Company.Rows(index).Cells("今回締日").Value = ds.Tables(RS).Rows(index)("今回締日")
                Dgv_Company.Rows(index).Cells("次回締日").Value = ds.Tables(RS).Rows(index)("次回締日")
                Dgv_Company.Rows(index).Cells("在庫単価評価法").Value = ds.Tables(RS).Rows(index)("在庫単価評価法")
                Dgv_Company.Rows(index).Cells("前払法人税率").Value = ds.Tables(RS).Rows(index)("前払法人税率")
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