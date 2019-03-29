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

    '画面表示時
    Private Sub User_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim dtToday As String = UtilClass.formatDatetime(DateTime.Now)

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
            LblConfirmPassword.Text = "ConfirmPassword"

            BtnRegistration.Text = "Registration"
            BtnBack.Text = "Back"

        End If

        If _status = CommonConst.STATUS_EDIT Then

            '一般権限ユーザーは以下操作不可
            If frmC01F10_Login.loginValue.Auth = CommonConst.Auth_KBN_GENERAL Then
                LblPassword.Visible = False
                TxtPassword.Visible = False
                LblConfirmPassword.Visible = False
                TxtConfirmPassword.Visible = False

                cmbInvalidFlag.Enabled = False
                cmAuthority.Enabled = False
            End If

            TxtUserId.Enabled = False

            Dim Sql As String = ""

            Sql = "SELECT 会社コード, ユーザＩＤ, 氏名, 略名, 備考, 無効フラグ, 権限, 言語, 更新者, 更新日 "
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

            Sql = "SELECT "
            Sql += "   パスワード "        'パスワード
            Sql += "  , 世代番号 "         '世代番号
            Sql += " FROM m03_pswd "
            Sql += " where 適用開始日 <= '" & dtToday & "'"
            Sql += "   and 適用終了日 >= '" & dtToday & "'"
            Sql += "   and 会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"
            Sql += "   and ユーザＩＤ = '" & _db.rmSQ(_userId) & "'"
            Sql += "   AND 世代番号 = (SELECT max(世代番号) FROM m03_pswd "
            Sql += "                     WHERE 会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"     '会社コード
            Sql += "                     AND ユーザＩＤ = '" & _db.rmSQ(_userId) & "')"                      'ユーザＩＤ
            Dim ds2 = _db.selectDB(Sql, RS, reccnt)

            If ds2.Tables(RS).Rows.Count > 0 Then

                If ds2.Tables(RS).Rows(0)("世代番号") IsNot DBNull.Value Then
                    TxtGeneration.Text = ds2.Tables(RS).Rows(0)("世代番号")
                End If
            End If

        End If
    End Sub

    '登録ボタン押下時
    Private Sub BtnRegistration_Click(sender As Object, e As EventArgs) Handles BtnRegistration.Click

        '項目チェック
        Dim strMessage As String = ""    'メッセージ本文
        Dim strMessageTitle As String = ""      'メッセージタイトル

        Dim Sql As String = ""

        ''ユーザＩＤは必須
        If TxtUserId.Text = "" Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                strMessage = "Please enter User ID. "
                strMessageTitle = "User ID Error"
            Else
                strMessage = "ユーザＩＤを入力してください。"
                strMessageTitle = "ユーザＩＤ入力エラー"
            End If
            Dim result As DialogResult = MessageBox.Show(strMessage, strMessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub

        Else

            If _status = CommonConst.STATUS_ADD Then

                Sql = " AND ユーザＩＤ ILIKE '" & TxtUserId.Text & "'"

                Dim ds As DataSet = getDsData("m02_user", Sql)

                If ds.Tables(RS).Rows.Count > 0 Then
                    _msgHd.dspMSG("chkUserIdError", frmC01F10_Login.loginValue.Language)

                    Exit Sub
                End If

            End If

        End If

        '登録処理ここから
        Dim dtToday As String = UtilClass.formatDatetime(DateTime.Now)
        Try

            '登録モード
            If _status = CommonConst.STATUS_ADD Then

                Sql = "INSERT INTO Public.m02_user("
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

                Sql = "INSERT INTO Public.m03_pswd"
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
                '編集モード

                Sql = "UPDATE Public.m02_user "
                Sql += "SET "
                Sql += " ユーザＩＤ = '" & _userId & "'"
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


                If TxtPassword.Text IsNot "" Then
                    Try

                        '入力チェック---------------------------------------------------------------
                        '1)	パスワード入力チェック
                        Try
                            Call checkInput()
                        Catch lex As UsrDefException
                            lex.dspMsg()
                            Exit Sub
                        End Try

                        '以前使用したものかどうかはチェックしない
                        '
                        ''2)パスワードチェック
                        ''画面入力値をもとに、パスワードマスタとの整合性チェックを行う。
                        ''・検索キー：　IF)会社コード、IF)ユーザID、画面)パスワード
                        '''   IF)世代番号 - 10 よりも大（過去10世代と重複しない）
                        'Sql = "SELECT count(*) as 件数"
                        'Sql += " FROM m03_pswd "
                        'Sql += " WHERE "
                        'Sql += "    会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"
                        'Sql += "   and ユーザＩＤ = '" & _db.rmSQ(_userId) & "'"
                        'Sql += "   and パスワード = '" & _db.rmSQ(TxtPassword.Text) & "'"
                        'Sql += "   and 世代番号 > " & _db.rmSQ(TxtGeneration.Text) - 10
                        'Dim reccnt As Integer = 0
                        'Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

                        ''①　該当するレコードが存在する場合
                        ''以前に使用されたパスワードです。
                        ''→　入力状態に戻る
                        'If _db.rmNullInt(ds.Tables(RS).Rows(0)("件数")) > 0 Then
                        '    _msgHd.dspMSG("ReusePasswd", CommonConst.LANG_KBN_JPN)
                        '    TxtPassword.Focus()
                        '    Exit Sub
                        'End If

                        Dim currentdateCnt As Integer = 0

                        '運用開始日=システム日付のデータ件数を取得
                        'sql編集
                        Sql = "SELECT count(*) 件数"
                        Sql += " FROM"
                        Sql += " m03_pswd"
                        Sql += " WHERE"
                        Sql += "       会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"
                        Sql += "   AND ユーザＩＤ = '" & _db.rmSQ(_userId) & "'"
                        Sql += "   AND 適用開始日 = current_date "

                        Dim iRecCnt As Integer = 0
                        'sql発行
                        Dim oDataSet As DataSet = _db.selectDB(Sql, RS, iRecCnt)    '抽出結果をDSへ格納
                        currentdateCnt = _db.rmNullInt(oDataSet.Tables(RS).Rows(0)("件数"))

                        'パスワードマスタ更新
                        '運用開始日=システム日付のデータ件数が存在していない場合
                        If currentdateCnt = 0 Then
                            '適用終了日を（システム日付-1）で更新
                            Sql = "UPDATE m03_pswd"
                            Sql += " SET"
                            Sql += "  適用終了日 = current_date - 1"                                      '適用終了日
                            Sql += " ,更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"                         '更新者
                            Sql += " ,更新日 = current_timestamp"                                         '更新日
                            Sql += " WHERE 会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"     '会社コード
                            Sql += "   AND ユーザＩＤ = '" & _db.rmSQ(_userId) & "'"                      'ユーザＩＤ
                            Sql += "   AND 適用終了日 = (SELECT max(適用終了日) FROM m03_pswd "
                            Sql += "                     WHERE 会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"     '会社コード
                            Sql += "                     AND ユーザＩＤ = '" & _db.rmSQ(_userId) & "')"                      'ユーザＩＤ

                            'sql発行
                            _db.executeDB(Sql)
                            '運用開始日=システム日付のデータ件数が存在している場合
                        Else
                            '適用終了日をシステム日付で更新
                            Sql = "UPDATE m03_pswd"
                            Sql += " SET"
                            Sql += "  適用終了日 = current_date"                                          '適用終了日
                            Sql += " ,更新者 = '" & _db.rmSQ(frmC01F10_Login.loginValue.TantoNM) & "'"           '更新者
                            Sql += " ,更新日 = current_timestamp"                                         '更新日
                            Sql += " WHERE 会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"     '会社コード
                            Sql += "   AND ユーザＩＤ = '" & _db.rmSQ(_userId) & "'"                      'ユーザＩＤ
                            Sql += "   AND 適用終了日 = (SELECT max(適用終了日) FROM m03_pswd "
                            Sql += "                     WHERE 会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"     '会社コード
                            Sql += "                     AND ユーザＩＤ = '" & _db.rmSQ(_userId) & "')"                      'ユーザＩＤ

                            'sql発行
                            _db.executeDB(Sql)

                        End If

                        'レコード追加
                        Sql = "INSERT INTO m03_pswd ( "
                        Sql += "    会社コード "
                        Sql += "  , ユーザＩＤ "
                        Sql += "  , 適用開始日 "
                        Sql += "  , 適用終了日 "
                        Sql += "  , パスワード "
                        Sql += "  , パスワード変更方法 "
                        Sql += "  , 世代番号 "
                        Sql += "  , 有効期限 "
                        Sql += "  , 更新者 "
                        Sql += "  , 更新日 "
                        Sql += ") VALUES ( "
                        Sql += "    '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
                        Sql += "  , '" & _db.rmSQ(_userId) & "' "       'ユーザＩＤ
                        Sql += "  , current_date "     '運用開始日
                        Sql += "  , '2099-12-31' "     '運用終了日
                        Sql += "  , '" & _db.rmSQ(TxtPassword.Text) & "' "       '新パスワード     ★暗号化予定★
                        Sql += "  , 1 "                'パスワード変更方法　固定値"1"（画面変更）
                        Sql += "  , " & TxtGeneration.Text + 1           '世代番号
                        Sql += "  , '2099-12-31' "     '有効期限
                        Sql += "  , '" & _db.rmSQ(frmC01F10_Login.loginValue.TantoNM) & "' "        '更新者
                        Sql += "  , current_timestamp "                                  '更新日
                        Sql += ") "
                        _db.executeDB(Sql)

                        '更新完了メッセージ
                        _msgHd.dspMSG("completePWChanged", CommonConst.LANG_KBN_JPN)


                        ''「連携処理一覧」画面起動
                        'Dim openForm As Form = Nothing
                        'openForm = New frmC01F30_Menu(_msgHd, _langHd, _db)
                        'openForm.Show()
                        Me.Close()
                        _parentForm.Enabled = True

                    Catch ue As UsrDefException
                        ue.dspMsg()
                        Throw ue
                    Catch ex As Exception
                        'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                        Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
                    End Try
                End If



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

    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
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

    '------------------------------------------------------------------------------------------------------
    '   入力チェック
    '------------------------------------------------------------------------------------------------------
    Private Sub checkInput()

        '新パスワード
        If "".Equals(TxtPassword.Text) Then

            '「新パスワード」を入力してください。
            Throw New UsrDefException(_msgHd.dspMSG("noInputNewPasswordError", frmC01F10_Login.loginValue.Language))

        End If

        '確認用
        If "".Equals(TxtConfirmPassword.Text) Then

            '「確認用パスワード」を入力してください。
            Throw New UsrDefException(_msgHd.dspMSG("noInputConfirmationError", frmC01F10_Login.loginValue.Language))

        End If

        '新パスワードと確認用パスワードの一致確認
        If Not TxtPassword.Text.Equals(TxtConfirmPassword.Text) Then
            '「新パスワード」と「確認用パスワード」が不一致です。
            Throw New UsrDefException(_msgHd.dspMSG("noInputPasswordVotEqualError", frmC01F10_Login.loginValue.Language))

        End If

    End Sub

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

End Class