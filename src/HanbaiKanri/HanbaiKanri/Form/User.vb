Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls

Public Class User
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
    Private _parentForm As Form
    'Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ
    Private _status As String = ""
    Private _companyCode As String = ""
    Private _userId As String = ""

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
                   ByRef prmRefForm As Form,
                   ByRef prmRefStatus As String,
                   Optional ByRef prmRefCompany As String = "",
                   Optional ByRef prmRefUser As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
        _status = prmRefStatus
        _companyCode = prmRefCompany
        _userId = prmRefUser
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles BtnRegistration.Click
        '項目チェック
        Dim strMessage As String = ""    'メッセージ本文
        Dim strMessageTitle As String = ""      'メッセージタイトル
        ''ユーザＩＤは必須
        If TxtUserId.Text = "" Then
            If frmC01F10_Login.loginValue.Language = "ENG" Then
                strMessage = "Please enter User ID. "
                strMessageTitle = "User ID Error"
            Else
                strMessage = "ユーザＩＤを入力してください。"
                strMessageTitle = "ユーザＩＤ入力エラー"
            End If
            Dim result As DialogResult = MessageBox.Show(strMessage, strMessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        '登録処理ここから
        Dim dtToday As String = UtilClass.formatDatetime(DateTime.Now)
        Try
            If _status = CommonConst.STATUS_ADD Then
                Dim Sql As String = ""

                Sql += "INSERT INTO Public.m02_user("
                Sql += "会社コード, ユーザＩＤ, 氏名, 略名, 備考, 無効フラグ, 権限, 言語, 更新者, 更新日)"
                Sql += "VALUES("
                Sql += " '" & frmC01F10_Login.loginValue.BumonCD & "'"  '会社コード
                Sql += ", '" & TxtUserId.Text & "'"         'ユーザＩＤ
                Sql += ", '" & TxtName.Text & "'"           '氏名
                Sql += ", '" & TxtShortName.Text & "'"      '略名
                Sql += ", '" & TxtRemarks.Text & "'"        '備考
                Sql += ", " & cmbInvalidFlag.SelectedValue '無効フラグ
                Sql += ", " & cmAuthority.SelectedValue '権限
                Sql += ", '" & cmLangage.SelectedValue.ToString & "'"        '言語
                Sql += ", '" & frmC01F10_Login.loginValue.TantoNM & "'"     '更新者
                Sql += ", '" & dtToday & "'"                '更新日
                Sql += " )"

                _db.executeDB(Sql)

                Sql = ""
                Sql += "INSERT INTO Public.m03_pswd"
                Sql += "(会社コード, ユーザＩＤ, 世代番号, 適用開始日, 適用終了日, パスワード, パスワード変更方法, 有効期限, 更新者, 更新日)"
                Sql += "VALUES("
                Sql += " '" & frmC01F10_Login.loginValue.BumonCD & "'"      '会社コード
                Sql += ", '" & TxtUserId.Text & "'"         'ユーザＩＤ
                Sql += ", '1'"                              '世代番号
                Sql += ", '" & dtToday & "'"                '適用開始日
                Sql += ", '2099-12-31'"                     '適用終了日
                Sql += ", '" & TxtPassword.Text & "'"       'パスワード
                Sql += ", 1"                                'パスワード変更方法
                Sql += ", '2099-12-31'"                     '有効期限
                Sql += ", '" & frmC01F10_Login.loginValue.TantoNM & "'"     '更新者
                Sql += ", '" & dtToday & "'"                '更新日
                Sql += " )"

                _db.executeDB(Sql)

            Else
                Dim Sql As String = ""

                Sql = "UPDATE Public.m02_user "
                Sql += "SET "
                Sql += " ユーザＩＤ = '" & TxtUserId.Text & "'"
                Sql += " , 氏名 = '" & TxtName.Text & "'"
                Sql += " , 略名 = '" & TxtShortName.Text & "'"
                Sql += " , 備考 = '" & TxtRemarks.Text & "'"
                Sql += " , 無効フラグ = " & cmbInvalidFlag.SelectedValue.ToString
                Sql += " , 権限 = " & cmAuthority.SelectedValue
                Sql += " , 言語 = '" & cmLangage.SelectedValue.ToString & "'"
                Sql += " , 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
                Sql += " , 更新日 = '" & dtToday & "'"
                Sql += "WHERE 会社コード ='" & _companyCode & "'"
                Sql += " AND ユーザＩＤ ='" & _userId & "' "
                _db.executeDB(Sql)
            End If


            _parentForm.Enabled = True
            _parentForm.Show()
            Me.Dispose()

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    Private Sub User_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        createCombobox()
        customsLangKbnCombobox()
        createAuthCombobox()

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            Label2.Text = "UserID"
            Label3.Text = "Name"
            Label4.Text = "ShortName"
            Label5.Text = "Remarks"
            Label6.Text = "Disable"
            Label7.Text = "Authority"
            Label8.Text = "Language"
            LblPassword.Text = "Password"
            Label26.Text = "(Non-Overlapping string)"
            Label27.Text = "(0:Valid 1:Disable)"
            Label9.Text = "(0:Common 1:Management)"
            Label1.Text = "(JPN:Japanese ENG:English)"

            BtnRegistration.Text = "Registration"
            BtnBack.Text = "Back"

        End If

        If _status = CommonConst.STATUS_EDIT Then
            LblPassword.Visible = False
            TxtPassword.Visible = False

            Dim Sql As String = ""

            Sql += "SELECT 会社コード, ユーザＩＤ, 氏名, 略名, 備考, 無効フラグ, 権限, 言語, 更新者, 更新日 "
            Sql += "FROM public.m02_user "
            Sql += "WHERE 会社コード ='" & _companyCode & "'"
            Sql += " AND ユーザＩＤ ='" & _userId & "'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            If ds.Tables(RS).Rows(0)("会社コード") Is DBNull.Value Then
            Else
                TxtCompanyCode.Text = ds.Tables(RS).Rows(0)("会社コード")
            End If


            If ds.Tables(RS).Rows(0)("ユーザＩＤ") Is DBNull.Value Then
            Else
                TxtUserId.Text = ds.Tables(RS).Rows(0)("ユーザＩＤ")
            End If

            If ds.Tables(RS).Rows(0)("氏名") Is DBNull.Value Then
            Else
                TxtName.Text = ds.Tables(RS).Rows(0)("氏名")
            End If

            If ds.Tables(RS).Rows(0)("略名") Is DBNull.Value Then
            Else
                TxtShortName.Text = ds.Tables(RS).Rows(0)("略名")
            End If

            If ds.Tables(RS).Rows(0)("備考") Is DBNull.Value Then
            Else
                TxtRemarks.Text = ds.Tables(RS).Rows(0)("備考")
            End If

            If ds.Tables(RS).Rows(0)("無効フラグ") Is DBNull.Value Then
            Else
                createCombobox(ds.Tables(RS).Rows(0)("無効フラグ"))
            End If

            If ds.Tables(RS).Rows(0)("権限") Is DBNull.Value Then
            Else
                createAuthCombobox(ds.Tables(RS).Rows(0)("権限"))
            End If

            If ds.Tables(RS).Rows(0)("言語") Is DBNull.Value Then
            Else
                customsLangKbnCombobox(ds.Tables(RS).Rows(0)("言語"))
            End If

        End If
    End Sub

    '有効無効のコンボボックスを作成
    '編集モードの時は値を渡してセットさせる
    Private Sub createCombobox(Optional ByRef prmVal As String = "")
        cmbInvalidFlag.DisplayMember = "Text"
        cmbInvalidFlag.ValueMember = "Value"

        Dim tb As New DataTable
        tb.Columns.Add("Text", GetType(String))
        tb.Columns.Add("Value", GetType(Integer))
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            tb.Rows.Add(CommonConst.FLAG_ENABLED_TXT_ENG, CommonConst.FLAG_ENABLED)
            tb.Rows.Add(CommonConst.FLAG_DISABLED_TXT_ENG, CommonConst.FLAG_DISABLED)

        Else
            tb.Rows.Add(CommonConst.FLAG_ENABLED_TXT, CommonConst.FLAG_ENABLED)
            tb.Rows.Add(CommonConst.FLAG_DISABLED_TXT, CommonConst.FLAG_DISABLED)

        End If

        cmbInvalidFlag.DataSource = tb

        If prmVal IsNot "" Then
            cmbInvalidFlag.SelectedValue = prmVal
        Else
            cmbInvalidFlag.SelectedValue = CommonConst.FLAG_ENABLED
        End If

    End Sub


    '言語のコンボボックスを作成
    '編集モードの時は値を渡してセットさせる
    Private Sub customsLangKbnCombobox(Optional ByRef prmVal As String = "")
        cmLangage.DisplayMember = "Text"
        cmLangage.ValueMember = "Value"

        Dim tb As New DataTable
        tb.Columns.Add("Text", GetType(String))
        tb.Columns.Add("Value", GetType(String))

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            tb.Rows.Add(CommonConst.LANG_KBN_JPN, CommonConst.LANG_KBN_JPN)
            tb.Rows.Add(CommonConst.LANG_KBN_ENG, CommonConst.LANG_KBN_ENG)

        Else
            tb.Rows.Add(CommonConst.LANG_KBN_JPN_TXT, CommonConst.LANG_KBN_JPN)
            tb.Rows.Add(CommonConst.LANG_KBN_ENG_TXT, CommonConst.LANG_KBN_ENG)

        End If

        cmLangage.DataSource = tb

        If prmVal IsNot "" Then
            cmLangage.SelectedValue = prmVal
        Else
            cmLangage.SelectedValue = CommonConst.LANG_KBN_JPN
        End If

    End Sub

    '有効無効のコンボボックスを作成
    '編集モードの時は値を渡してセットさせる
    Private Sub createAuthCombobox(Optional ByRef prmVal As String = "")
        cmAuthority.DisplayMember = "Text"
        cmAuthority.ValueMember = "Value"

        Dim tb As New DataTable
        tb.Columns.Add("Text", GetType(String))
        tb.Columns.Add("Value", GetType(Integer))
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            tb.Rows.Add(CommonConst.Auth_KBN_GENERAL_TXT_ENG, CommonConst.Auth_KBN_GENERAL)
            tb.Rows.Add(CommonConst.Auth_KBN_ADMIN_TXT_ENG, CommonConst.Auth_KBN_ADMIN)

        Else
            tb.Rows.Add(CommonConst.Auth_KBN_GENERAL_TXT, CommonConst.Auth_KBN_GENERAL)
            tb.Rows.Add(CommonConst.Auth_KBN_ADMIN_TXT, CommonConst.Auth_KBN_ADMIN)

        End If

        cmAuthority.DataSource = tb

        If prmVal IsNot "" Then
            cmAuthority.SelectedValue = prmVal
        Else
            cmAuthority.SelectedValue = CommonConst.Auth_KBN_GENERAL
        End If

    End Sub

End Class