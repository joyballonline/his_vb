'===============================================================================
'　 （システム名）      購買連携システム
'
'   （機能名）          パスワード変更
'   （クラス名）        frmKR12_ChangePasswd
'   （処理機能名）      
'   （本MDL使用前提）   UtilMDLプロジェクトがソリューションに取り込まれていること
'   （備考）            
'
'===============================================================================
' 履歴  名前               日付       マーク    内容
'-------------------------------------------------------------------------------
'  (1)  Shigihara          2012/03/01           新規
'-------------------------------------------------------------------------------
Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB

Imports System.Drawing.Printing
Imports System.Runtime.InteropServices

'===================================================================================
'メニューフォーム
'===================================================================================
Public Class frmC01F20_ChangePasswd
    Inherits System.Windows.Forms.Form


    '-------------------------------------------------------------------------------
    'メンバー定数宣言
    '-------------------------------------------------------------------------------
    'PG制御文字 
    Private Const N As String = ControlChars.NewLine                    '改行文字
    Private Const RS As String = "RecSet"                               'レコードセットテーブル
    Private Const FORM_ID As String = "KR12"                            '画面ID

    '-------------------------------------------------------------------------------
    'メンバー変数宣言
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _langHd As UtilLangHandler
    Private _db As UtilDBIf
    Private _parentForm As Form

    '-------------------------------------------------------------------------------
    'ＡＰＩインポート　「×」閉じるボタンを無効化するため
    '-------------------------------------------------------------------------------
    <DllImport("USER32.DLL")> _
    Private Shared Function _
    GetSystemMenu(ByVal hWnd As IntPtr, ByVal bRevert As Integer) As IntPtr
    End Function
    <DllImport("USER32.DLL")> _
    Private Shared Function _
    RemoveMenu(ByVal hMenu As IntPtr, ByVal nPosition As Integer, ByVal wFlags As Integer) As Integer
    End Function
    '-------------------------------------------------------------------------------
    'コンストラクタ（Privateにして、外からは呼べないようにする）
    '-------------------------------------------------------------------------------
    Private Sub New()
        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

        '「×」ボタンを無効化するための値
        Dim SC_CLOSE As Integer = &HF060
        Dim MF_BYCOMMAND As Integer = &H0
        ' コントロールボックスの［閉じる］ボタンの無効化
        Dim hMenu As IntPtr = GetSystemMenu(Me.Handle, 0)
        RemoveMenu(hMenu, SC_CLOSE, MF_BYCOMMAND)

    End Sub

    '-------------------------------------------------------------------------------
    'コンストラクタ　ログイン画面から呼ばれる。
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler,
                   ByRef prmRefDbHd As UtilDBIf,
                   ByRef prmRefForm As Form)

        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _parentForm = prmRefForm                                            '親フォーム
        _parentForm.Enabled = False
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        lblTanto.Text = frmC01F10_Login.loginValue.TantoCD                  '担当者コードを表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]"                                  'フォームタイトル表示

    End Sub

    '-------------------------------------------------------------------------------
    '   戻るボタン
    '-------------------------------------------------------------------------------
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnback.Click

        Dim intRet As Integer
        intRet = _msgHd.dspMSG("rejectPWEdit", frmC01F10_Login.loginValue.Language)
        If intRet = vbNo Then
            Exit Sub
        End If
        Me.Close()
        _parentForm.Show()
        _parentForm.Enabled = True
        _parentForm.Activate()

    End Sub

    '------------------------------------------------------------------------------------------------------
    'フォームロードイベント
    '------------------------------------------------------------------------------------------------------
    Private Sub frm_UserList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            '描画関係の設定
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           'サイズが変更されたときに、コントロールがコントロール自体を再描画するかどうかを示す値を設定
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '描画はバッファで実行され、完了後に、結果が画面に出力されるよう設定
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  'コントロールは、画面に直接ではなく、まずバッファに描画されます。これにより、ちらつきを抑えることができます。
            Me.SetStyle(ControlStyles.UserPaint, True)              'コントロールは、オペレーティング システムによってではなく、独自に描画されるよう設定
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   'コントロールはウィンドウ メッセージ WM_ERASEBKGND を無視するように設定


        Catch ue As UsrDefException
            ue.dspMsg()                                                                                                     '握りつぶす
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))     '握りつぶす
        End Try

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblTitle.Text = "ChangePassword"
            LblUserCd.Text = "UserCode"
            LblNewPassword.Text = "NewPassword"
            LblConfirmation.Text = "Confirmation"
            btnback.Text = "Back"
        End If

    End Sub

    '-------------------------------------------------------------------------------
    '   OKボタン押下時
    '-------------------------------------------------------------------------------
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click


        Try

            '入力チェック---------------------------------------------------------------
            '1)	パスワード入力チェック
            Try
                Call checkInput()
            Catch lex As UsrDefException
                lex.dspMsg()
                Exit Sub
            End Try

            '2)パスワードチェック
            '画面入力値をもとに、パスワードマスタとの整合性チェックを行う。
            '・検索キー：　IF)会社コード、IF)ユーザID、画面)パスワード
            '   IF)世代番号 - 10 よりも大（過去10世代と重複しない）
            Dim sql As String = ""
            sql = sql & "SELECT count(*) as 件数"
            sql = sql & " FROM m03_pswd "
            sql = sql & " WHERE "
            sql = sql & "    会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"
            sql = sql & "   and ユーザＩＤ = '" & _db.rmSQ(frmC01F10_Login.loginValue.TantoCD) & "'"
            sql = sql & "   and パスワード = '" & _db.rmSQ(txtPasswd.Text) & "'"
            sql = sql & "   and 世代番号 > " & _db.rmSQ(frmC01F10_Login.loginValue.Generation) - 10
            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

            '①　該当するレコードが存在する場合
            '以前に使用されたパスワードです。
            '→　入力状態に戻る
            If _db.rmNullInt(ds.Tables(RS).Rows(0)("件数")) > 0 Then
                _msgHd.dspMSG("ReusePasswd", CommonConst.LANG_KBN_JPN)
                txtPasswd.Focus()
                Exit Sub
            End If

            Dim currentdateCnt As Integer = 0

            '運用開始日=システム日付のデータ件数を取得
            'sql編集
            sql = ""
            sql = "SELECT count(*) 件数"
            sql = sql & " FROM"
            sql = sql & " m03_pswd"
            sql = sql & " WHERE"
            sql = sql & "       会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"
            sql = sql & "   AND ユーザＩＤ = '" & _db.rmSQ(frmC01F10_Login.loginValue.TantoCD) & "'"
            sql = sql & "   AND 適用開始日 = current_date "

            Dim iRecCnt As Integer = 0
            'sql発行
            Dim oDataSet As DataSet = _db.selectDB(sql, RS, iRecCnt)    '抽出結果をDSへ格納
            currentdateCnt = _db.rmNullInt(oDataSet.Tables(RS).Rows(0)("件数"))

            'パスワードマスタ更新
            '運用開始日=システム日付のデータ件数が存在していない場合
            If currentdateCnt = 0 Then
                '適用終了日を（システム日付-1）で更新
                sql = ""
                sql = "UPDATE m03_pswd"
                sql = sql & " SET"
                sql = sql & "  適用終了日 = current_date - 1"                                      '適用終了日
                sql = sql & " ,更新者 = '" & _db.rmSQ(lblTanto.Text) & "'"                         '更新者
                sql = sql & " ,更新日 = current_timestamp"                                         '更新日
                sql = sql & " WHERE 会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"     '会社コード
                sql = sql & "   AND ユーザＩＤ = '" & _db.rmSQ(frmC01F10_Login.loginValue.TantoCD) & "'"                      'ユーザＩＤ
                sql = sql & "   AND 適用終了日 = (SELECT max(適用終了日) FROM m03_pswd "
                sql = sql & "                     WHERE 会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"     '会社コード
                sql = sql & "                     AND ユーザＩＤ = '" & _db.rmSQ(frmC01F10_Login.loginValue.TantoCD) & "')"                      'ユーザＩＤ

                'sql発行
                _db.executeDB(sql)
                '運用開始日=システム日付のデータ件数が存在している場合
            Else
                '適用終了日をシステム日付で更新
                sql = ""
                sql = "UPDATE m03_pswd"
                sql = sql & " SET"
                sql = sql & "  適用終了日 = current_date"                                          '適用終了日
                sql = sql & " ,更新者 = '" & _db.rmSQ(lblTanto.Text) & "'"           '更新者
                sql = sql & " ,更新日 = current_timestamp"                                         '更新日
                sql = sql & " WHERE 会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"     '会社コード
                sql = sql & "   AND ユーザＩＤ = '" & _db.rmSQ(frmC01F10_Login.loginValue.TantoCD) & "'"                      'ユーザＩＤ
                sql = sql & "   AND 適用終了日 = (SELECT max(適用終了日) FROM m03_pswd "
                sql = sql & "                     WHERE 会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"     '会社コード
                sql = sql & "                     AND ユーザＩＤ = '" & _db.rmSQ(frmC01F10_Login.loginValue.TantoCD) & "')"                      'ユーザＩＤ

                'sql発行
                _db.executeDB(sql)

            End If

            'レコード追加
            sql = ""
            sql = sql & "INSERT INTO m03_pswd ( "
            sql = sql & "    会社コード "
            sql = sql & "  , ユーザＩＤ "
            sql = sql & "  , 適用開始日 "
            sql = sql & "  , 適用終了日 "
            sql = sql & "  , パスワード "
            sql = sql & "  , パスワード変更方法 "
            sql = sql & "  , 世代番号 "
            sql = sql & "  , 有効期限 "
            sql = sql & "  , 更新者 "
            sql = sql & "  , 更新日 "
            sql = sql & ") VALUES ( "
            sql = sql & "    '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '会社コード
            sql = sql & "  , '" & _db.rmSQ(frmC01F10_Login.loginValue.TantoCD) & "' "       'ユーザＩＤ
            sql = sql & "  , current_date "     '運用開始日
            sql = sql & "  , '2099-12-31' "     '運用終了日
            sql = sql & "  , '" & _db.rmSQ(txtPasswd.Text) & "' "       '新パスワード     ★暗号化予定★
            sql = sql & "  , 1 "                'パスワード変更方法　固定値"1"（画面変更）
            sql = sql & "  , " & _db.rmSQ(frmC01F10_Login.loginValue.Generation) + 1           '世代番号
            sql = sql & "  , '2099-12-31' "     '有効期限
            sql = sql & "  , '" & _db.rmSQ(lblTanto.Text) & "' "        '更新者
            sql = sql & "  , current_timestamp "                                  '更新日
            sql = sql & ") "
            _db.executeDB(sql)

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

    End Sub

    '-------------------------------------------------------------------------------
    '　キープレスイベント
    '-------------------------------------------------------------------------------
    Private Sub ctl_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
                                txtPasswd.KeyPress, txtKakunin.KeyPress

        '押下キーがEnterの場合、次のコントロールへフォーカス移動
        Call UtilClass.moveNextFocus(Me, e)

    End Sub

    '-------------------------------------------------------------------------------
    '　フォーカス取得イベント
    '-------------------------------------------------------------------------------
    Private Sub ctl_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
                            txtPasswd.GotFocus, txtKakunin.GotFocus

        'フォーカス取得時、入力パラメタのコントロールを全選択状態とする
        Call UtilClass.selAll(sender)

    End Sub

    '------------------------------------------------------------------------------------------------------
    '   入力チェック
    '------------------------------------------------------------------------------------------------------
    Private Sub checkInput()

        '新パスワード
        If "".Equals(txtPasswd.Text) Then

            '「新パスワード」を入力してください。
            Throw New UsrDefException(_msgHd.dspMSG("noInputNewPasswordError", frmC01F10_Login.loginValue.Language))

        End If

        '確認用
        If "".Equals(txtKakunin.Text) Then

            '「確認用パスワード」を入力してください。
            Throw New UsrDefException(_msgHd.dspMSG("noInputConfirmationError", frmC01F10_Login.loginValue.Language))

        End If

        '新パスワードと確認用パスワードの一致確認
        If Not txtPasswd.Text.Equals(txtKakunin.Text) Then
            '「新パスワード」と「確認用パスワード」が不一致です。
            Throw New UsrDefException(_msgHd.dspMSG("noInputPasswordVotEqualError", frmC01F10_Login.loginValue.Language))

        End If

    End Sub

End Class
