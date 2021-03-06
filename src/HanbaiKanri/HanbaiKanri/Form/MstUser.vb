﻿Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class MstUser
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
    Private _langHd As UtilLangHandler
    Private _db As UtilDBIf
    Private _parentForm As Form
    Private _gh As UtilDataGridViewHandler
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

    Private Sub UserMaintenance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If frmC01F10_Login.loginValue.Language = "ENG" Then
            Label1.Text = "UserID"
            BtnBack.Text = "Back"
            BtnSearch.Text = "Search"
            BtnAdd.Text = "Add"
            BtnEdit.Text = "Edit"

            Dgv_User.Columns("会社コード").HeaderText = "CompanyCode"
            Dgv_User.Columns("ユーザID").HeaderText = "UserID"
            Dgv_User.Columns("氏名").HeaderText = "Name"
            Dgv_User.Columns("略名").HeaderText = "ShortName"
            Dgv_User.Columns("備考").HeaderText = "Remarks"
            Dgv_User.Columns("無効フラグ").HeaderText = "InvalidFlag"
            Dgv_User.Columns("権限").HeaderText = "Authority"
            Dgv_User.Columns("言語").HeaderText = "Language"
            Dgv_User.Columns("更新者").HeaderText = "ModifiedBy"
            Dgv_User.Columns("更新日").HeaderText = "UpdateDate"
        End If

        setList() '一覧取得
    End Sub

    Private Sub setList()
        Dgv_User.Rows.Clear()

        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "m02_user"
            Sql += " WHERE "
            Sql += "会社コード"
            Sql += " ILIKE "
            Sql += "'"
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "'"
            Sql += " AND "
            Sql += "ユーザＩＤ"
            Sql += " ILIKE "
            Sql += "'%"
            Sql += Search.Text
            Sql += "%'"
            Sql += " order by 会社コード, ユーザＩＤ "

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                Dgv_User.Rows.Add()
                Dgv_User.Rows(i).Cells("会社コード").Value = ds.Tables(RS).Rows(i)("会社コード")        '会社コード
                Dgv_User.Rows(i).Cells("ユーザID").Value = ds.Tables(RS).Rows(i)("ユーザＩＤ")        '言語コード
                Dgv_User.Rows(i).Cells("氏名").Value = ds.Tables(RS).Rows(i)("氏名")        '氏名
                Dgv_User.Rows(i).Cells("略名").Value = ds.Tables(RS).Rows(i)("略名")      '略名
                Dgv_User.Rows(i).Cells("備考").Value = ds.Tables(RS).Rows(i)("備考")      '備考
                Dgv_User.Rows(i).Cells("無効フラグ").Value = setEnabledText(ds.Tables(RS).Rows(i)("無効フラグ"))      '無効フラグ
                Dgv_User.Rows(i).Cells("権限").Value = setAuthText(ds.Tables(RS).Rows(i)("権限"))      '権限
                Dgv_User.Rows(i).Cells("言語").Value = setLangText(ds.Tables(RS).Rows(i)("言語"))      '備考
                Dgv_User.Rows(i).Cells("更新者").Value = ds.Tables(RS).Rows(i)("更新者")      '更新者
                Dgv_User.Rows(i).Cells("更新日").Value = ds.Tables(RS).Rows(i)("更新日")      '更新日
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    Private Sub btn_userAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        Dim openForm As Form = Nothing
        Dim Status As String = CommonConst.STATUS_ADD
        openForm = New User(_msgHd, _db, _langHd, Me, Status)   '処理選択
        openForm.Show()
        Me.Hide()   ' 自分は隠れる
    End Sub

    Private Sub btn_selectedRow_Click(sender As Object, e As EventArgs) Handles BtnEdit.Click
        Dim openForm As Form = Nothing
        Dim Status As String = CommonConst.STATUS_EDIT
        Dim CompanyCode As String = Dgv_User.Rows(Dgv_User.CurrentCell.RowIndex).Cells("会社コード").Value
        Dim UserId As String = Dgv_User.Rows(Dgv_User.CurrentCell.RowIndex).Cells("ユーザID").Value
        openForm = New User(_msgHd, _db, _langHd, Me, Status, CompanyCode, UserId)   '処理選択
        openForm.Show()
        Me.Hide()   ' 自分は隠れる
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        setList()
    End Sub

    Private Sub MstUser_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        setList()
    End Sub

    '無効フラグ
    Private Function setEnabledText(ByVal prmVal As String) As String
        Dim reVal As String

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            reVal = IIf(Integer.Parse(prmVal) = CommonConst.FLAG_ENABLED, CommonConst.FLAG_ENABLED_TXT_ENG, CommonConst.FLAG_DISABLED_TXT_ENG)
        Else
            reVal = IIf(Integer.Parse(prmVal) = CommonConst.FLAG_ENABLED, CommonConst.FLAG_ENABLED_TXT, CommonConst.FLAG_DISABLED_TXT)
        End If

        Return reVal
    End Function

    '言語
    Private Function setLangText(ByVal prmVal As String) As String
        Dim reVal As String

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            reVal = IIf(prmVal = CommonConst.LANG_KBN_JPN, CommonConst.LANG_KBN_JPN, CommonConst.LANG_KBN_ENG)
        Else
            reVal = IIf(prmVal = CommonConst.LANG_KBN_JPN, CommonConst.LANG_KBN_JPN_TXT, CommonConst.LANG_KBN_ENG_TXT)
        End If

        Return reVal
    End Function

    '権限
    Private Function setAuthText(ByVal prmVal As String) As String
        Dim reVal As String

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            reVal = IIf(Integer.Parse(prmVal) = CommonConst.Auth_KBN_GENERAL, CommonConst.Auth_KBN_GENERAL_TXT_ENG, CommonConst.Auth_KBN_ADMIN_TXT_ENG)
        Else
            reVal = IIf(Integer.Parse(prmVal) = CommonConst.Auth_KBN_GENERAL, CommonConst.Auth_KBN_GENERAL_TXT, CommonConst.Auth_KBN_ADMIN_TXT)
        End If

        Return reVal
    End Function
End Class