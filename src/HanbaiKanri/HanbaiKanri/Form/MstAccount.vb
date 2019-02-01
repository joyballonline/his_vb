Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls

Public Class MstAccount

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

    Private Sub MstAccount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If frmC01F10_Login.loginValue.Language = "ENG" Then
            LblAccountName.Text = "AccountName"
            TxtSearch.Location = New Point(120, 6)
            BtnSearch.Text = "Search"
            BtnSearch.Location = New Point(226, 6)
            btnAccountAdd.Text = "Add"
            btnAccountEdit.Text = "Edit"
            BtnBack.Text = "Back"

            Dgv_Account.Columns("会社コード").HeaderText = "CompanyCode"
            Dgv_Account.Columns("勘定科目コード").HeaderText = "AccountCode"
            Dgv_Account.Columns("勘定科目名称１").HeaderText = "AccountName1"
            Dgv_Account.Columns("勘定科目名称２").HeaderText = "AccountName2"
            Dgv_Account.Columns("勘定科目名称３").HeaderText = "AccountName3"
            Dgv_Account.Columns("会計用勘定科目コード").HeaderText = "AccountingAccountCode"
            Dgv_Account.Columns("備考").HeaderText = "Remarks"
            Dgv_Account.Columns("有効区分").HeaderText = "EffectiveClassification"
            Dgv_Account.Columns("更新者").HeaderText = "ModifiedBy"
            Dgv_Account.Columns("更新日").HeaderText = "UpdateDate"

        End If
        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "m92_kanjo"
            Sql += " WHERE "
            Sql += "会社コード"
            Sql += " ILIKE "
            Sql += "'"
            Sql += frmC01F10_Login.loginValue.BumonNM
            Sql += "'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1

                Dgv_Account.Rows.Add()
                Dgv_Account.Rows(index).Cells("会社コード").Value = ds.Tables(RS).Rows(index)("会社コード")
                Dgv_Account.Rows(index).Cells("勘定科目コード").Value = ds.Tables(RS).Rows(index)("勘定科目コード")
                Dgv_Account.Rows(index).Cells("勘定科目名称１").Value = ds.Tables(RS).Rows(index)("勘定科目名称１")
                Dgv_Account.Rows(index).Cells("勘定科目名称２").Value = ds.Tables(RS).Rows(index)("勘定科目名称２")
                Dgv_Account.Rows(index).Cells("勘定科目名称３").Value = ds.Tables(RS).Rows(index)("勘定科目名称３")
                Dgv_Account.Rows(index).Cells("会計用勘定科目コード").Value = ds.Tables(RS).Rows(index)("会計用勘定科目コード")
                Dgv_Account.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
                Dgv_Account.Rows(index).Cells("有効区分").Value = ds.Tables(RS).Rows(index)("有効区分")
                Dgv_Account.Rows(index).Cells("更新者").Value = ds.Tables(RS).Rows(index)("更新者")
                Dgv_Account.Rows(index).Cells("更新日").Value = ds.Tables(RS).Rows(index)("更新日")

            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Sub btnCompanyrAdd_Click(sender As Object, e As EventArgs) Handles btnAccountAdd.Click
        Dim openForm As Form = Nothing
        Dim Status As String = "ADD"
        openForm = New Account(_msgHd, _db, _langHd, Status)   '処理選択
        openForm.Show()
        Me.Hide()   ' 自分は隠れる
    End Sub

    Private Sub btnSelectCompany_Click(sender As Object, e As EventArgs) Handles btnAccountEdit.Click
        If Dgv_Account.Rows.Count > 0 Then
            Dim openForm As Form = Nothing
            Dim Status As String = "EDIT"
            Dim CompanyCode As String = Dgv_Account.Rows(Dgv_Account.CurrentCell.RowIndex).Cells("会社コード").Value
            Dim AccountCode As String = Dgv_Account.Rows(Dgv_Account.CurrentCell.RowIndex).Cells("勘定科目コード").Value
            openForm = New Account(_msgHd, _db, _langHd, Status, CompanyCode, AccountCode)   '処理選択
            openForm.Show()
            Me.Hide()   ' 自分は隠れる
        Else
            '該当データがなかったら
            _msgHd.dspMSG("NonData")
        End If

    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        Dim frmMenu As frmC01F30_Menu
        frmMenu = New frmC01F30_Menu(_msgHd, _langHd, _db)
        frmMenu.Show()
        Me.Close()
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        Dgv_Account.Rows.Clear()

        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "m92_kanjo"
            Sql += " WHERE "
            Sql += "会社コード"
            Sql += " ILIKE "
            Sql += "'"
            Sql += frmC01F10_Login.loginValue.BumonNM
            Sql += "'"
            Sql += " AND "
            Sql += " ( 勘定科目名称１"
            Sql += " ILIKE "
            Sql += "'%"
            Sql += TxtSearch.Text
            Sql += "%'"
            Sql += " OR "
            Sql += "勘定科目名称２"
            Sql += " ILIKE "
            Sql += "'%"
            Sql += TxtSearch.Text
            Sql += "%'"
            Sql += " OR "
            Sql += "勘定科目名称３"
            Sql += " ILIKE "
            Sql += "'%"
            Sql += TxtSearch.Text
            Sql += "%')"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1

                Dgv_Account.Rows.Add()

                Dgv_Account.Rows(index).Cells("会社コード").Value = ds.Tables(RS).Rows(index)("会社コード")
                Dgv_Account.Rows(index).Cells("勘定科目コード").Value = ds.Tables(RS).Rows(index)("勘定科目コード")
                Dgv_Account.Rows(index).Cells("勘定科目名称１").Value = ds.Tables(RS).Rows(index)("勘定科目名称１")
                Dgv_Account.Rows(index).Cells("勘定科目名称２").Value = ds.Tables(RS).Rows(index)("勘定科目名称２")
                Dgv_Account.Rows(index).Cells("勘定科目名称３").Value = ds.Tables(RS).Rows(index)("勘定科目名称３")
                Dgv_Account.Rows(index).Cells("会計用勘定科目コード").Value = ds.Tables(RS).Rows(index)("会計用勘定科目コード")
                Dgv_Account.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
                Dgv_Account.Rows(index).Cells("有効区分").Value = ds.Tables(RS).Rows(index)("有効区分")
                Dgv_Account.Rows(index).Cells("更新者").Value = ds.Tables(RS).Rows(index)("更新者")
                Dgv_Account.Rows(index).Cells("更新日").Value = ds.Tables(RS).Rows(index)("更新日")
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