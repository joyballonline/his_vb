'===============================================================================
'　 （システム名）      購買連携システム
'
'   （機能名）          ログイン
'   （クラス名）        frmKR11_Login
'   （処理機能名）      
'   （本MDL使用前提）   UtilMDLプロジェクトがソリューションに取り込まれていること
'   （備考）            
'
'===============================================================================
' 履歴  名前               日付       マーク    内容
'-------------------------------------------------------------------------------
'  (1)  Shigihara          2010/03/01           新規
'  (2)  Shigihara          2013/03/11 V1.2.0.1  担当者コード存在チェックの位置を変更
'-------------------------------------------------------------------------------
Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB

Imports System.Drawing.Printing
Imports System.Runtime.InteropServices

'===================================================================================
'フォーム
'===================================================================================
Public Class frmC01F10_Login

    Inherits System.Windows.Forms.Form

    '------------------------------------------------------------------------------------------------------
    'メンバー定数宣言
    '------------------------------------------------------------------------------------------------------
    'PG制御文字 
    Private Const N As String = ControlChars.NewLine                    '改行文字
    Private Const RS As String = "RecSet"                               'レコードセットテーブル

    '-------------------------------------------------------------------------------
    'メンバー変数宣言
    '-------------------------------------------------------------------------------
    Private _parentForm As Form
    Private _msgHd As UtilMsgHandler
    Private _langHd As UtilLangHandler
    Private _db As UtilDBIf
    Private _btnSybt As String

    'ログイン情報格納変数
    Private Shared _loginVal As StartUp.loginType

    '-------------------------------------------------------------------------------
    'プロパティ宣言
    '-------------------------------------------------------------------------------
    Public Shared ReadOnly Property loginValue() As StartUp.loginType                     'ログイン情報構造体を返却
        Get
            Return _loginVal
        End Get
    End Property

    '-------------------------------------------------------------------------------
    'コンストラクタ（Privateにして、外からは呼べないようにする）
    '-------------------------------------------------------------------------------
    Private Sub New()
        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()
        cmbCampany.SelectedIndex = 0
    End Sub

    '-------------------------------------------------------------------------------
    'コンストラクタ　StartUpから呼ばれる。
    '   ●入力パラメタ   ：prmRefMsgHd      MSGハンドラ
    '                      prmRefDbHd       DBハンドラ
    '   ●メソッド戻り値 ：インスタンス
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefLangHd As UtilLangHandler, ByRef prmRefDbHd As UtilDBIf)
        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                    'MSGハンドラの設定
        _langHd = prmRefLangHd                                                  'LANGハンドラの設定
        _db = prmRefDbHd                                                        'DBハンドラの設定
        StartPosition = FormStartPosition.CenterScreen                          '画面中央表示
        lblVer.Text = "Ver : " & UtilClass.getAppVersion(StartUp.assembly)      'ラベルへ、バージョン情報の表示
        If StartUp.BackUpServer Then
            'バックアップサーバ接続中
            lblBackup.Visible = True
        End If
        Dim test As String = _langHd.getLANG("title", "en")

    End Sub

    '-------------------------------------------------------------------------------
    '   終了ボタン
    '   （処理概要）フォームを閉じる
    '-------------------------------------------------------------------------------
    Private Sub btnEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnd.Click

        Dim intRet As Integer
        intRet = _msgHd.dspMSG("SystemExit")
        If intRet = vbOK Then
            Application.Exit()
        End If

        'メッセージはこのあたりのクラスをコールしなくてよいのだろうか？
        '該当メッセージがＸＭＬに見当たらないので、とりあえずこのまま。
        'Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", ""), txtTanto)

    End Sub

    '------------------------------------------------------------------------------------------------------
    'フォームロードイベント
    '------------------------------------------------------------------------------------------------------
    Private Sub frm_E11_Login_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            '描画関係の設定
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           'サイズが変更されたときに、コントロールがコントロール自体を再描画するかどうかを示す値を設定
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '描画はバッファで実行され、完了後に、結果が画面に出力されるよう設定
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  'コントロールは、画面に直接ではなく、まずバッファに描画されます。これにより、ちらつきを抑えることができます。
            Me.SetStyle(ControlStyles.UserPaint, True)              'コントロールは、オペレーティング システムによってではなく、独自に描画されるよう設定
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   'コントロールはウィンドウ メッセージ WM_ERASEBKGND を無視するように設定

            '初期化
            Call initForm()

        Catch ue As UsrDefException
            ue.dspMsg()                                                                                                     '握りつぶす
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))     '握りつぶす
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   ログインボタン
    '-------------------------------------------------------------------------------
    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click

        '_loginVal.BumonNM = cmbCampany.Text
        'If chkPasswd.Checked Then
        '    'パスワード変更チェックあり
        '    Dim openForm As Form = Nothing
        '    Dim strTitle As String = "[" & cmbCampany.Text & "][鴫原　牧人]"
        '    openForm = New frmC01F20_ChangePasswd(_msgHd, _db, Me, txtTanto.Text, strTitle)
        '    openForm.Show()

        'Else
        '    'パスワード変更チェックなし
        '    Dim openForm As Form = Nothing
        '    openForm = New frmC01F30_Menu(_msgHd, _db)
        '    openForm.Show()
        'End If

        Try

            '入力チェック---------------------------------------------------------------
            Try
                Call checkInput()
            Catch lex As UsrDefException
                lex.dspMsg()
                Exit Sub
            End Try

            Dim sql As String = ""
            '2)	パスワードチェック			
            '画面入力値をもとに、パスワードマスタとの整合性チェックを行う。			
            '・検索キー：　IF)会社コード、IF)ユーザID、画面)パスワード			
            '	   IF)世代番号 - 10 よりも大（過去10世代と重複しない）
            Try
                sql = sql & N & "SELECT "
                sql = sql & N & "    略名 "
                sql = sql & N & " FROM m02_user "
                sql = sql & N & " WHERE "
                sql = sql & N & "    会社コード = '" & _db.rmSQ(cmbCampany.SelectedValue) & "'"
                sql = sql & N & "    and ユーザＩＤ = '" & _db.rmSQ(txtTanto.Text) & "'"
                sql = sql & N & "    and 無効フラグ = 0 "
                Dim reccnt As Integer = 0
                Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

                If reccnt <= 0 Then
                    _msgHd.dspMSG("NonImputUserID")
                    'MsgBox("入力された「ユーザID」は存在しないか、無効になっています。", vbOK)
                    'Throw New UsrDefException("入力された「ユーザID」は存在しないか、無効になっています。", _msgHd.getMSG("NoTantoCD", ""), txtTanto)
                    Exit Sub
                End If

                '2)	パスワードチェック
                '	画面入力値をもとに、パスワードマスタとの整合性チェックを行う。
                '	・検索キー
                '	　　　画面)会社コード、画面)ユーザID、適用開始日≦システム日付≦適用終了日
                '	・取得項目：　パスワード、世代番号

                sql = ""
                sql = sql & N & "SELECT "
                sql = sql & N & "   パスワード "        'パスワード
                sql = sql & N & "  , 世代番号 "         '世代番号
                sql = sql & N & " FROM m03_pswd "
                sql = sql & N & " where 適用開始日 <= current_date "
                sql = sql & N & "   and 適用終了日 >= current_date "
                sql = sql & N & "   and 会社コード = '" & _db.rmSQ(cmbCampany.SelectedValue) & "'"
                sql = sql & N & "   and ユーザＩＤ = '" & _db.rmSQ(txtTanto.Text) & "'"
                Dim reccnt2 As Integer = 0
                Dim ds2 = _db.selectDB(sql, RS, reccnt2)

                '①　該当するレコードが存在しない場合	
                '入力された「ユーザID」のパスワード情報が存在しません。
                '→　入力状態に戻す（通常はありえない）
                If reccnt2 <= 0 Then
                    _msgHd.dspMSG("NonImputNoDataUserID")
                    'MsgBox("入力された「ユーザID」のパスワード情報が存在しません。", vbOK)
                    'Throw New UsrDefException("入力された「ユーザID」は存在しないか、無効になっています。", _msgHd.getMSG("NoTantoCD", ""), txtTanto)
                    Exit Sub
                End If

                '②　該当するレコードが存在する場合		
                '②-1　DB取得パスワードの復号化	
                '★当面保留★（暗号化なしのため、そのまま比較）

                '②-2　パスワード同一チェック	
                '画面)パスワードとDB)パスワードを比較
                If Not _db.rmNullStr(ds2.Tables(RS).Rows(0)("パスワード")).Equals(txtPasswd.Text) Then
                    'Throw New UsrDefException("パスワードが違います。", _msgHd.getMSG("Unmatch", ""), txtPasswd)
                    _msgHd.dspMSG("NonImputPassword")
                    'MsgBox("パスワードが違います。", vbOK)
                    Exit Sub
                End If

                '■グローバル変数にセット
                '3)	次処理起動	
                '下記要領にしたがって、次処理に遷移を移動する。	
                'ログイン画面から、次の項目を次処理に受け渡す。	
                '・受け渡し項目：会社コード、会社略称、ユーザID、社員略名、BKUPサーバ接続有無（バックアップサーバ接続時:"Y"、以外:"N"）、世代番号（パスワード）

                _loginVal.BumonCD = _db.rmNullStr(cmbCampany.SelectedValue)     '会社コード
                _loginVal.BumonNM = _db.rmNullStr(cmbCampany.Text)              '会社略称
                _loginVal.TantoCD = _db.rmNullStr(txtTanto.Text)                'ユーザＩＤ
                _loginVal.TantoNM = _db.rmNullStr(ds.Tables(RS).Rows(0)("略名"))                '社員略名
                _loginVal.Passwd = _db.rmNullStr(txtPasswd.Text)                'パスワード
                _loginVal.Generation = _db.rmNullStr(ds2.Tables(RS).Rows(0)("世代番号"))                '世代番号
                '未実装　BKUPサーバ接続有無（バックアップサーバ接続時:"Y"、以外:"N"）


            Catch lex As UsrDefException
                lex.dspMsg()
                Exit Sub
            End Try

            'パスワード変更チェックONの場合、パスワード変更画面を起動
            If chkPasswd.Checked = True Then
                'パスワード変更チェックあり
                Dim openForm As Form = Nothing
                openForm = New frmC01F20_ChangePasswd(_msgHd, _db, Me)
                openForm.ShowDialog()
                openForm.Dispose()

                '「パスワード変更」画面起動
                'Dim openForm12 As frmKR12_ChangePasswd = New frmKR12_ChangePasswd(_msgHd, _db, Me, txtTanto.Text)   'パラメタを起動画面へ渡す
                'StartUp.loginForm = Me
                'openForm12.ShowDialog()                                                 '画面表示
                'openForm12.Dispose()
                Exit Sub

            Else
                'パスワード変更チェックなし
                Dim openForm As Form = Nothing
                openForm = New frmC01F30_Menu(_msgHd, _db)
                openForm.Show()
                Me.Hide()                                                           '自分は隠れる

                ''「連携処理一覧」画面起動
                'Dim openForm13 As frmKR13_ProcList = New frmKR13_ProcList(_msgHd, _db, Me, _loginVal.TantoCD, _loginVal.TantoNM)      'パラメタを起動画面へ渡す
                'StartUp.loginForm = Me
                'openForm13.Show()                                                   '画面表示
                'Me.Hide()                                                           '自分は隠れる

            End If

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　キープレスイベント
    '-------------------------------------------------------------------------------
    Private Sub ctl_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
                                txtTanto.KeyPress, txtPasswd.KeyPress

        '押下キーがEnterの場合、次のコントロールへフォーカス移動
        Call UtilClass.moveNextFocus(Me, e)

    End Sub

    '-------------------------------------------------------------------------------
    '　フォーカス取得イベント
    '-------------------------------------------------------------------------------
    Private Sub ctl_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
                            txtTanto.GotFocus, txtPasswd.GotFocus

        'フォーカス取得時、入力パラメタのコントロールを全選択状態とする
        Call UtilClass.selAll(sender)

    End Sub

    '------------------------------------------------------------------------------------------------------
    '   フォーム初期化
    '------------------------------------------------------------------------------------------------------
    Private Sub initForm()
        Try

            'コントロール初期化
            Call clearControl()

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   コントロールのクリア
    '   （処理概要）　全コントロールの内容をクリアする
    '-------------------------------------------------------------------------------
    Private Sub clearControl()

        'システム名称表示
        lblTitle.Text = StartUp.iniValue.SystemCaption

        '担当者コード
        txtTanto.Text = ""
        'パスワード
        txtPasswd.Text = ""
        'パスワード変更チェックボックス
        chkPasswd.Checked = False

        '会社名コンボボックスセット
        clearCmbCampany()

    End Sub

    '-------------------------------------------------------------------------------
    '   会社名コンボボックスを初期化
    '   （処理概要）　会社マスタよりデータを取得し、コンボボックスにセットする
    '-------------------------------------------------------------------------------
    Private Sub clearCmbCampany()

        Dim strSql As String = ""
        '会社マスタをコンボボックスにセット
        Try
            strSql = "SELECT "
            strSql = strSql & "    会社コード, 会社略称 "
            strSql = strSql & " FROM m01_company "
            strSql = strSql & " order by 表示順 "
            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(strSql, RS, reccnt)

            cmbCampany.DataSource = ds.Tables(RS)
            cmbCampany.DisplayMember = "会社略称"
            cmbCampany.ValueMember = "会社コード"
        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '   入力チェック
    '------------------------------------------------------------------------------------------------------
    Private Sub checkInput()

        '担当者コード
        If "".Equals(txtTanto.Text) Then
            Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", ""), txtTanto)
        End If

        'パスワード
        If "".Equals(txtPasswd.Text) Then
            Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", ""), txtPasswd)
        End If

    End Sub

End Class