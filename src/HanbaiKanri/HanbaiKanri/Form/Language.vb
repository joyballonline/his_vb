Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls

Public Class Language
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
    Private _langHd As UtilLangHandler
    'Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ
    Private _status As String = ""
    Private _companyCode As String = ""
    Private _langCode As String = ""

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
                   ByRef prmRefStatsu As String,
                   Optional ByRef prmRefCompany As String = "",
                   Optional ByRef prmRefLangCode As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _status = prmRefStatsu
        _companyCode = prmRefCompany
        _langCode = prmRefLangCode
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BtnRegistration.Click
        Dim dtToday As DateTime = DateTime.Now
        Try
            If _status = "ADD" Then
                Dim Sql As String = ""

                Sql = ""
                Sql += "INSERT INTO "
                Sql += "Public."
                Sql += "m05_language("
                Sql += "会社コード, 言語コード, 言語名称, 言語略称, 備考, 無効フラグ, 更新者, 更新日)"
                Sql += "VALUES('"
                Sql += frmC01F10_Login.loginValue.BumonCD
                Sql += "', '"
                Sql += TxtLanguage.Text
                Sql += "', '"
                Sql += TxtName.Text
                Sql += "', '"
                Sql += TxtShortName.Text
                Sql += "', '"
                Sql += TxtRemarks.Text
                Sql += "', '"
                If TxtFlg.Text = "" Then
                    Sql += "0"
                Else
                    Sql += TxtFlg.Text
                End If
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoNM
                Sql += "', '"
                Sql += dtToday
                Sql += " ')"
                Sql += "RETURNING 会社コード"
                Sql += ", "
                Sql += "言語コード"
                Sql += ", "
                Sql += "言語名称"
                Sql += ", "
                Sql += "言語略称"
                Sql += ", "
                Sql += "備考"
                Sql += ", "
                Sql += "無効フラグ"
                Sql += ", "
                Sql += "更新者"
                Sql += ", "
                Sql += "更新日"

                _db.executeDB(Sql)
            Else
                Dim Sql As String = ""

                Sql = ""
                Sql += "UPDATE "
                Sql += "Public."
                Sql += "m05_language "
                Sql += "SET "
                Sql += " 会社コード"
                Sql += " = '"
                Sql += TxtCompanyCode.Text
                Sql += "', "
                Sql += "言語コード"
                Sql += " = '"
                Sql += TxtLanguage.Text
                Sql += "', "
                Sql += "言語名称"
                Sql += " = '"
                Sql += TxtName.Text
                Sql += "', "
                Sql += "言語略称"
                Sql += " = '"
                Sql += TxtShortName.Text
                Sql += "', "
                Sql += "備考"
                Sql += " = '"
                Sql += TxtRemarks.Text
                Sql += "', "
                Sql += "無効フラグ"
                Sql += " = '"
                Sql += TxtFlg.Text
                Sql += "', "
                Sql += "更新者"
                Sql += " = '"
                Sql += frmC01F10_Login.loginValue.TantoNM
                Sql += "', "
                Sql += "更新日"
                Sql += " = '"
                Sql += dtToday
                Sql += "' "
                Sql += "WHERE"
                Sql += " 会社コード"
                Sql += "='"
                Sql += _companyCode
                Sql += "'"
                Sql += " AND"
                Sql += " 言語コード"
                Sql += "='"
                Sql += _langCode
                Sql += "' "
                Sql += "RETURNING 会社コード"
                Sql += ", "
                Sql += "言語コード"
                Sql += ", "
                Sql += "言語名称"
                Sql += ", "
                Sql += "言語略称"
                Sql += ", "
                Sql += "備考"
                Sql += ", "
                Sql += "無効フラグ"
                Sql += ", "
                Sql += "更新者"
                Sql += ", "
                Sql += "更新日"

                _db.executeDB(Sql)
            End If


            Dim frmUM As MstLanguage
            frmUM = New MstLanguage(_msgHd, _db, _langHd)
            frmUM.Show()
            Me.Close()

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        Dim MstLanguage As MstLanguage
        MstLanguage = New MstLanguage(_msgHd, _db, _langHd)
        MstLanguage.Show()
        Me.Close()
    End Sub

    Private Sub User_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If frmC01F10_Login.loginValue.Language = "ENG" Then
            Label2.Text = "LanguageCode"
            Label3.Text = "LanguageName"
            Label4.Text = "ShortName"
            Label5.Text = "Remarks"
            Label6.Text = "Disabled"
            Label20.Text = "(Non-Overlapping string)"
            Label8.Text = "(Example:JAPANESE)"
            Label1.Text = "(Example:JPN)"
            Label27.Text = "(0:Valid 1:Disabled)"

            BtnRegistration.Text = "Registration"
            BtnBack.Text = "Back"
        End If
        If _status = "EDIT" Then
            Dim Sql As String = ""

            Sql += "SELECT "
            Sql += "会社コード, "
            Sql += "言語コード, "
            Sql += "言語名称, "
            Sql += "言語略称, "
            Sql += "備考, "
            Sql += "無効フラグ, "
            Sql += "更新者, "
            Sql += "更新日 "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "m05_language"
            Sql += " WHERE"
            Sql += " 会社コード"
            Sql += "='"
            Sql += _companyCode
            Sql += "'"
            Sql += " AND"
            Sql += " 言語コード"
            Sql += "='"
            Sql += _langCode
            Sql += "' "

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            If ds.Tables(RS).Rows(0)("会社コード") Is DBNull.Value Then
            Else
                TxtCompanyCode.Text = ds.Tables(RS).Rows(0)("会社コード")
            End If

            If ds.Tables(RS).Rows(0)("言語コード") Is DBNull.Value Then
            Else
                TxtLanguage.Text = ds.Tables(RS).Rows(0)("言語コード")
            End If

            If ds.Tables(RS).Rows(0)("言語名称") Is DBNull.Value Then
            Else
                TxtName.Text = ds.Tables(RS).Rows(0)("言語名称")
            End If

            If ds.Tables(RS).Rows(0)("言語略称") Is DBNull.Value Then
            Else
                TxtShortName.Text = ds.Tables(RS).Rows(0)("言語略称")
            End If

            If ds.Tables(RS).Rows(0)("備考") Is DBNull.Value Then
            Else
                TxtRemarks.Text = ds.Tables(RS).Rows(0)("備考")
            End If

            If ds.Tables(RS).Rows(0)("無効フラグ") Is DBNull.Value Then
            Else
                TxtFlg.Text = ds.Tables(RS).Rows(0)("無効フラグ")
            End If
        End If
    End Sub
End Class