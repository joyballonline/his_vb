Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Text.RegularExpressions

Public Class MstHanyou
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
    Private _parentForm As Form
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
                   ByRef prmRefLang As UtilLangHandler,
                   ByRef prmRefForm As Form)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    '画面表示時
    Private Sub MstHanyou_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            Label2.Text = "String"
            Label3.Text = "Number"

            BtnSearch.Text = "Search"
            BtnAdd.Text = "Add"
            BtnEdit.Text = "Edit"
            BtnBack.Text = "Back"

            Dgv_Hanyo.Columns("固定キー").HeaderText = "FixedKey"
            Dgv_Hanyo.Columns("可変キー").HeaderText = "VariableKey"
            Dgv_Hanyo.Columns("表示順").HeaderText = "DisplayOrder"
            Dgv_Hanyo.Columns("文字１").HeaderText = "Charcter1"
            Dgv_Hanyo.Columns("文字２").HeaderText = "Charcter2"
            Dgv_Hanyo.Columns("文字３").HeaderText = "Charcter3"
            Dgv_Hanyo.Columns("文字４").HeaderText = "Charcter4"
            Dgv_Hanyo.Columns("文字５").HeaderText = "Charcter5"
            Dgv_Hanyo.Columns("文字６").HeaderText = "Charcter6"
            Dgv_Hanyo.Columns("数値１").HeaderText = "Number1"
            Dgv_Hanyo.Columns("数値２").HeaderText = "Number2"
            Dgv_Hanyo.Columns("数値３").HeaderText = "Number3"
            Dgv_Hanyo.Columns("数値４").HeaderText = "Number4"
            Dgv_Hanyo.Columns("数値５").HeaderText = "Number5"
            Dgv_Hanyo.Columns("数値６").HeaderText = "Number6"
            Dgv_Hanyo.Columns("メモ").HeaderText = "Memo"
            Dgv_Hanyo.Columns("更新者").HeaderText = "ModifiedBy"
            Dgv_Hanyo.Columns("更新日").HeaderText = "UpDateDate"

        End If

        '一覧を取得
        getList()

    End Sub

    '編集ボタン押下時
    Private Sub BtnEdit_Click(sender As Object, e As EventArgs) Handles BtnEdit.Click
        Dim openForm As Form = Nothing
        Dim Status As String = CommonConst.STATUS_EDIT
        Dim Code As String = frmC01F10_Login.loginValue.BumonCD
        Dim Key1 As String = Dgv_Hanyo.Rows(Dgv_Hanyo.CurrentCell.RowIndex).Cells("固定キー").Value
        Dim Key2 As String = Dgv_Hanyo.Rows(Dgv_Hanyo.CurrentCell.RowIndex).Cells("可変キー").Value

        openForm = New Hanyo(_msgHd, _db, _langHd, Me, Status, Code, Key1, Key2)   '処理選択
        openForm.Show()
        Me.Hide()   ' 自分は隠れる
    End Sub

    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    '追加ボタン押下時
    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        Dim openForm As Form = Nothing
        Dim Status As String = CommonConst.STATUS_ADD
        openForm = New Hanyo(_msgHd, _db, _langHd, Me, Status)   '処理選択
        openForm.Show()
        Me.Hide()   ' 自分は隠れる
    End Sub

    '検索ボタン押下時
    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        '一覧を取得
        getList()
    End Sub

    '一覧表示
    Public Sub getList()
        Dim Sql As String = ""

        If IsNumeric(TxtSearchNumber.Text) <> True And TxtSearchNumber.Text IsNot "" Then
            '数値以外が入っていたらアラート
            _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)

            Return
        End If

        '一覧クリア
        Dgv_Hanyo.Rows.Clear()

        Sql = searchConditions()

        Try
            Dim ds As DataSet = getDsData("m90_hanyo", Sql)

            For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                Dgv_Hanyo.Rows.Add()
                Dgv_Hanyo.Rows(i).Cells("固定キー").Value = ds.Tables(RS).Rows(i)("固定キー")        '固定キー
                Dgv_Hanyo.Rows(i).Cells("可変キー").Value = ds.Tables(RS).Rows(i)("可変キー")        '可変キー
                Dgv_Hanyo.Rows(i).Cells("表示順").Value = ds.Tables(RS).Rows(i)("表示順")         '表示順
                Dgv_Hanyo.Rows(i).Cells("文字１").Value = ds.Tables(RS).Rows(i)("文字１")         '文字１
                Dgv_Hanyo.Rows(i).Cells("文字２").Value = ds.Tables(RS).Rows(i)("文字２").ToString         '文字２
                Dgv_Hanyo.Rows(i).Cells("文字３").Value = ds.Tables(RS).Rows(i)("文字３").ToString         '文字３
                Dgv_Hanyo.Rows(i).Cells("文字４").Value = ds.Tables(RS).Rows(i)("文字４").ToString         '文字４
                Dgv_Hanyo.Rows(i).Cells("文字５").Value = ds.Tables(RS).Rows(i)("文字５").ToString         '文字５
                Dgv_Hanyo.Rows(i).Cells("文字６").Value = ds.Tables(RS).Rows(i)("文字６").ToString         '文字６
                Dgv_Hanyo.Rows(i).Cells("数値１").Value = ds.Tables(RS).Rows(i)("数値１")        '数値１
                Dgv_Hanyo.Rows(i).Cells("数値２").Value = ds.Tables(RS).Rows(i)("数値２")        '数値２
                Dgv_Hanyo.Rows(i).Cells("数値３").Value = ds.Tables(RS).Rows(i)("数値３")        '数値３
                Dgv_Hanyo.Rows(i).Cells("数値４").Value = ds.Tables(RS).Rows(i)("数値４")        '数値４
                Dgv_Hanyo.Rows(i).Cells("数値５").Value = ds.Tables(RS).Rows(i)("数値５")        '数値５
                Dgv_Hanyo.Rows(i).Cells("数値６").Value = ds.Tables(RS).Rows(i)("数値６")        '数値６
                Dgv_Hanyo.Rows(i).Cells("メモ").Value = ds.Tables(RS).Rows(i)("メモ").ToString         'メモ
                Dgv_Hanyo.Rows(i).Cells("更新者").Value = ds.Tables(RS).Rows(i)("更新者").ToString        '更新者
                Dgv_Hanyo.Rows(i).Cells("更新日").Value = ds.Tables(RS).Rows(i)("更新日").ToString        '更新日
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '抽出条件取得
    Private Function searchConditions() As String
        Dim Sql As String = ""

        '抽出条件
        Dim searchString As String = escapeSql(TxtSearchString.Text)
        Dim searchNumber As String = escapeSql(TxtSearchNumber.Text)

        If searchString <> Nothing Then

            Sql += " AND ("
            Sql += " 固定キー ILIKE '%" & searchString & "%' "
            Sql += " OR "
            Sql += " 文字１ ILIKE '%" & searchString & "%' "
            Sql += " OR "
            Sql += " 文字２ ILIKE '%" & searchString & "%' "
            Sql += " OR "
            Sql += " 文字３ ILIKE '%" & searchString & "%' "
            Sql += " OR "
            Sql += " 文字４ ILIKE '%" & searchString & "%' "
            Sql += " OR "
            Sql += " 文字５ ILIKE '%" & searchString & "%' "
            Sql += " OR "
            Sql += " 文字６ ILIKE '%" & searchString & "%' "
            Sql += " OR "
            Sql += " メモ ILIKE '%" & searchString & "%' "
            Sql += " )"

        End If

        If searchNumber <> Nothing Then

            Sql += " AND ("
            Sql += " 数値１ = " & searchNumber
            Sql += " OR "
            Sql += " 数値２ = " & searchNumber
            Sql += " OR "
            Sql += " 数値３ = " & searchNumber
            Sql += " OR "
            Sql += " 数値４ = " & searchNumber
            Sql += " OR "
            Sql += " 数値５ = " & searchNumber
            Sql += " OR "
            Sql += " 数値６ = " & searchNumber
            Sql += " )"

        End If

        Sql += " ORDER BY 固定キー, 表示順"

        Return Sql

    End Function

    'sqlで実行する文字列からシングルクォーテーションを文字コードにする
    Private Function escapeSql(ByVal prmSql As String) As String
        Dim sql As String = prmSql

        sql = sql.Replace("'"c, "''") 'シングルクォーテーションを置換

        Return Regex.Escape(sql)
        Return sql
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

    Private Sub MstHanyou_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        '一覧を取得
        getList()
    End Sub
End Class