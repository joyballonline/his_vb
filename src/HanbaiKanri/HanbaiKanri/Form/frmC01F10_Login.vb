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
Imports System.Threading
Imports System.Globalization
Imports System.Net

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

    Private Const COMPANY_FILE As String = "company.txt"                                'txtファイル名

    '-------------------------------------------------------------------------------
    'メンバー変数宣言
    '-------------------------------------------------------------------------------
    Private Shared _assembly As System.Reflection.Assembly          'アセンブリ(アプリケーション情報)
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

    Public Shared ReadOnly Property assembly() As System.Reflection.Assembly    'アセンブリ
        Get
            Return _Assembly
        End Get
    End Property

    '-------------------------------------------------------------------------------
    'コンストラクタ（Privateにして、外からは呼べないようにする）
    '-------------------------------------------------------------------------------
    Private Sub New()
        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()
        'cmbCampany.SelectedIndex = 0
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

        _assembly = System.Reflection.Assembly.GetExecutingAssembly() 'アセンブリをメンバーに格納

    End Sub

    '-------------------------------------------------------------------------------
    '   終了ボタン
    '   （処理概要）フォームを閉じる
    '-------------------------------------------------------------------------------
    Private Sub btnEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnd.Click

        Dim intRet As Integer
        intRet = _msgHd.dspMSG("SystemExit", CommonConst.LANG_KBN_JPN)
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


        '描画関係の設定
        Me.SetStyle(ControlStyles.ResizeRedraw, True)           'サイズが変更されたときに、コントロールがコントロール自体を再描画するかどうかを示す値を設定
        Me.SetStyle(ControlStyles.DoubleBuffer, True)           '描画はバッファで実行され、完了後に、結果が画面に出力されるよう設定
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  'コントロールは、画面に直接ではなく、まずバッファに描画されます。これにより、ちらつきを抑えることができます。
        Me.SetStyle(ControlStyles.UserPaint, True)              'コントロールは、オペレーティング システムによってではなく、独自に描画されるよう設定
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   'コントロールはウィンドウ メッセージ WM_ERASEBKGND を無視するように設定

        Dim netChk As Boolean = networkCheck()
        If netChk Then
            '20190502　いったんコメントアウト
            'accountCheck()
            'useLimitCheck()
        Else
            _msgHd.dspMSG("chkNetworkError", CommonConst.LANG_KBN_JPN)
            Application.Exit()
        End If

        '初期化
        Call initForm()

    End Sub

    '-------------------------------------------------------------------------------
    '   ログインボタン
    '-------------------------------------------------------------------------------
    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click


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
                sql = "SELECT * FROM m02_user "
                sql += " WHERE "
                sql += "    会社コード = '" & _db.rmSQ(cmbCampany.SelectedValue) & "'"
                sql += "    and ユーザＩＤ = '" & _db.rmSQ(txtTanto.Text) & "'"
                sql += "    and 無効フラグ = 0 "
                Dim reccnt As Integer = 0
                Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

                If reccnt <= 0 Then
                    _msgHd.dspMSG("NonImputUserID", CommonConst.LANG_KBN_JPN)
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
                sql = sql & "SELECT "
                sql = sql & "   パスワード "        'パスワード
                sql = sql & "  , 世代番号 "         '世代番号
                sql = sql & " FROM m03_pswd "
                sql = sql & " where 適用開始日 <= current_date "
                sql = sql & "   and 適用終了日 >= current_date "
                sql = sql & "   and 会社コード = '" & _db.rmSQ(cmbCampany.SelectedValue) & "'"
                sql = sql & "   and ユーザＩＤ = '" & _db.rmSQ(txtTanto.Text) & "'"

                Dim reccnt2 As Integer = 0
                Dim ds2 = _db.selectDB(sql, RS, reccnt2)

                '①　該当するレコードが存在しない場合	
                '入力された「ユーザID」のパスワード情報が存在しません。
                '→　入力状態に戻す（通常はありえない）
                If reccnt2 <= 0 Then
                    _msgHd.dspMSG("NonImputNoDataUserID", CommonConst.LANG_KBN_JPN)
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
                    _msgHd.dspMSG("NonImputPassword", CommonConst.LANG_KBN_JPN)
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
                _loginVal.Language = _db.rmNullStr(ds.Tables(RS).Rows(0)("言語"))                '言語
                _loginVal.Auth = _db.rmNullStr(ds.Tables(RS).Rows(0)("権限"))               '権限
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

                ''「連携処理一覧」画面起動
                'Dim openForm13 As frmKR13_ProcList = New frmKR13_ProcList(_msgHd, _db, Me, _loginVal.TantoCD, _loginVal.TantoNM)      'パラメタを起動画面へ渡す
                'StartUp.loginForm = Me
                'openForm13.Show()                                                   '画面表示
                'Me.Hide()                                                           '自分は隠れる


                '前月末にレートの登録があるかチェック
                Dim Month As Integer = Now.Month
                Dim BaseDate As Date = DateSerial(Year(Now), Month, 1)
                BaseDate = DateAdd("d", -1, BaseDate)  '前月末

                sql = ""
                sql = sql & "SELECT count(*) as 件数"
                sql = sql & " FROM t71_exchangerate "
                sql = sql & " where 基準日 ='" & UtilClass.strFormatDate(BaseDate) & "'"
                sql = sql & "   and 会社コード = '" & _db.rmSQ(cmbCampany.SelectedValue) & "'"

                Dim dsRate = _db.selectDB(sql, RS, 0)

                If dsRate.Tables(RS).Rows(0)("件数") = 0 Then
                    _msgHd.dspMSG("LoginRate", frmC01F10_Login.loginValue.Language)
                End If


                'インフォメーション表示
                Dim openForm As Form = Nothing
                openForm = New Information(_msgHd, _db, _langHd, Me)
                openForm.Show()
                Me.Hide()

            End If

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
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   コントロールのクリア
    '   （処理概要）　全コントロールの内容をクリアする
    '-------------------------------------------------------------------------------
    Private Sub clearControl()

        'システム名称表示
        'lblTitle.Text = StartUp.iniValue.SystemCaption

        Label1.Text = "User ID"
        Label2.Text = "Password"
        Label5.Text = "Company"
        chkPasswd.Text = "Change Password"
        lblBackup.Text = "Connected to Backup Server"
        btnLogin.Text = "Login"
        btnEnd.Text = "Exit"

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
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '   入力チェック
    '------------------------------------------------------------------------------------------------------
    Private Sub checkInput()

        '担当者コード
        If "".Equals(txtTanto.Text) Then
            Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", frmC01F10_Login.loginValue.Language), txtTanto)
        End If

        'パスワード
        If "".Equals(txtPasswd.Text) Then
            Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", frmC01F10_Login.loginValue.Language), txtPasswd)
        End If

    End Sub

    Private Function networkCheck()
        'ネットワークに接続されているか調べる
        If System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function fileCheck()

        'メッセージ用Xml存在チェック
        Dim companyFileName As String
        companyFileName = UtilClass.getAppPath(_assembly)               'アプリケーション実行パスを取得
        If Not companyFileName.EndsWith("\") Then                       '"\"で終わっていないなら
            companyFileName = companyFileName & "\"
        End If
        Dim companyFile As String = UtilClass.getAppPath(_assembly) & "\..\Setting\" & COMPANY_FILE

        If UtilClass.isFileExists(companyFile) Then                 'ファイルが存在するなら


            Dim name As String = ""
            name = System.IO.File.ReadAllText(companyFile, System.Text.Encoding.Default)

            'Dim rs As New System.IO.StringReader(companyFile)
            ''ストリームの末端まで繰り返す
            'While rs.Peek() > -1
            '    '一行読み込んで表示する
            '    Console.WriteLine(rs.ReadLine())

            '    name = rs.ReadLine()
            'End While

            'rs.Close()

            If name = "" Then
                '情報が存在しないのでエラー
                _msgHd.dspMSG("chkAppUseSettingError", CommonConst.LANG_KBN_JPN, "No Setting")
                Application.Exit()

            End If

            Return name
        Else

            'ファイルが存在しないのでエラー
            _msgHd.dspMSG("chkAppUseSettingError", CommonConst.LANG_KBN_JPN, "No File")
            Application.Exit()

            Return ""
        End If

    End Function

    Private Sub accountCheck()
        Try

            Dim reccnt As Integer = 0
            Dim Sql = ""

            Sql = " SELECT "
            Sql += " テキスト "
            Sql += " FROM "
            Sql += " s01_config "
            Sql += " WHERE "
            Sql += " 項目 = '会社名'"
            Sql += " AND "
            Sql += " テキスト = '" & fileCheck() & "'"

            Dim companyName As DataSet = _db.selectDB(Sql, RS, reccnt)

            If companyName.Tables(RS).Rows.Count > 0 Then

                If companyName.Tables(RS).Rows(0)("テキスト").ToString IsNot Nothing Then

                    Dim url As String = CommonConst.CHECK_URL

                    Dim wc As New System.Net.WebClient
                    'NameValueCollectionの作成
                    Dim ps As New System.Collections.Specialized.NameValueCollection
                    '送信するデータ（フィールド名と値の組み合わせ）を追加
                    ps.Add("name", companyName.Tables(RS).Rows(0)("テキスト").ToString)
                    'データを送信し、また受信する
                    Dim resData As Byte() = wc.UploadValues(url, ps)
                    wc.Dispose()

                    '受信したデータを表示する
                    Dim resText As String = System.Text.Encoding.UTF8.GetString(resData)
                    'Console.WriteLine(resText)

                    If resText <> "success" Then
                        '有効なユーザでない場合、終了する
                        _msgHd.dspMSG("chkAppActiveUserError", CommonConst.LANG_KBN_JPN)
                        Application.Exit()

                    End If

                End If

            Else

                '設定がなかったら終了する
                _msgHd.dspMSG("chkAppUseSettingError", CommonConst.LANG_KBN_JPN)
                Application.Exit()

            End If

            'Catch ex As WebException

            '    'HTTPプロトコルエラーかどうか調べる
            '    If ex.Status = System.Net.WebExceptionStatus.ProtocolError Then
            '        'HttpWebResponseを取得
            '        Dim errors As System.Net.HttpWebResponse = CType(ex.Response, System.Net.HttpWebResponse)
            '        '応答したURIを表示する
            '        Console.WriteLine(errors.ResponseUri)
            '        '応答ステータスコードを表示する
            '        Console.WriteLine("{0}:{1}", errors.StatusCode, errors.StatusDescription)
            '    Else
            '        Console.WriteLine(ex.Message)
            '    End If

        Catch ue As UsrDefException
            ue.dspMsg()                                                                                                     '握りつぶす
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))     '握りつぶす
        End Try

    End Sub

    Private Sub useLimitCheck()

        Try
            '最初に使用可能ユーザーかどうかチェック
            Dim reccnt As Integer = 0

            Dim Sql = ""

            Sql = "SELECT "
            Sql += " マシン名, 初回アクセス日時 "
            Sql += " FROM l11_aclog "
            Sql += " WHERE "
            Sql += " マシン名 = '" & System.Environment.MachineName & "'"

            Dim dsACLog As DataSet = _db.selectDB(Sql, RS, reccnt)

            '該当マシンがあったら
            If dsACLog.Tables(RS).Rows.Count > 0 Then

                '初期化
                Call initForm()

            Else
                '使用数が上限に達しているかどうか

                Sql = "SELECT"
                Sql += " 数値 "
                Sql += " FROM s01_config "
                Sql += " WHERE "
                Sql += " 項目 = '" & "使用上限数" & "'" '使用上限数

                Dim dsConfig As DataSet = _db.selectDB(Sql, RS, reccnt)

                Sql = "SELECT "
                Sql += " マシン名, 初回アクセス日時 "
                Sql += " FROM l11_aclog "

                dsACLog = _db.selectDB(Sql, RS, reccnt)

                If dsConfig.Tables(RS).Rows.Count > 0 Then
                    '使用上限数未満だったらl11_aclogに新規追加
                    If dsACLog.Tables(RS).Rows.Count < dsConfig.Tables(RS).Rows(0)("数値") Then

                        Sql = "INSERT INTO l11_aclog ( "
                        Sql += " マシン名, 初回アクセス日時 "
                        Sql += " ) VALUES ( "
                        Sql += " '" & System.Environment.MachineName & "'"
                        Sql += " , '" & UtilClass.formatDatetime(DateTime.Now)
                        Sql += "' ) "

                        _db.executeDB(Sql) 'l11_aclogテーブル更新

                        '初期化
                        Call initForm()

                    Else

                        '上限を超えていたらアラートを表示後に終了
                        _msgHd.dspMSG("chkAppUseError", CommonConst.LANG_KBN_JPN)
                        Application.Exit()

                    End If
                Else

                    '設定がなかったら終了する
                    _msgHd.dspMSG("chkAppUseSettingError", CommonConst.LANG_KBN_JPN)
                    Application.Exit()

                End If

            End If

        Catch ue As UsrDefException
            ue.dspMsg()                                                                                                     '握りつぶす
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))     '握りつぶす
        End Try

    End Sub

    '開発者用
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        ' ホスト名を取得する
        Dim hostname As String = Dns.GetHostName()

        Dim comName As String = "ZENBI"
        Dim userPass As String = "zenbi01"
        'Dim comName As String = "demo"
        'Dim userPass As String = "demo"

        ' ホスト名からIPアドレスを取得する
        Dim adrList As IPAddress() = Dns.GetHostAddresses(hostname)
        For Each address As IPAddress In adrList
            Console.WriteLine(address.ToString())

            If address.ToString() = "172.21.12.72" Then
                Dim sql As String = ""
                '2)	パスワードチェック			
                '画面入力値をもとに、パスワードマスタとの整合性チェックを行う。			
                '・検索キー：　IF)会社コード、IF)ユーザID、画面)パスワード			
                '	   IF)世代番号 - 10 よりも大（過去10世代と重複しない）
                Try
                    sql = "SELECT * FROM m02_user "
                    sql += " WHERE "
                    sql += "    会社コード = '" & comName & "'"
                    sql += "    and ユーザＩＤ = '" & userPass & "'"
                    sql += "    and 無効フラグ = 0 "
                    Dim reccnt As Integer = 0
                    Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

                    If reccnt <= 0 Then
                        _msgHd.dspMSG("NonImputUserID", CommonConst.LANG_KBN_JPN)
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
                    sql = sql & "SELECT "
                    sql = sql & "   パスワード "        'パスワード
                    sql = sql & "  , 世代番号 "         '世代番号
                    sql = sql & " FROM m03_pswd "
                    sql = sql & " where 適用開始日 <= current_date "
                    sql = sql & "   and 適用終了日 >= current_date "
                    sql = sql & "   and 会社コード = '" & comName & "'"
                    sql = sql & "   and ユーザＩＤ = '" & userPass & "'"
                    Dim reccnt2 As Integer = 0
                    Dim ds2 = _db.selectDB(sql, RS, reccnt2)

                    '①　該当するレコードが存在しない場合	
                    '入力された「ユーザID」のパスワード情報が存在しません。
                    '→　入力状態に戻す（通常はありえない）
                    If reccnt2 <= 0 Then
                        _msgHd.dspMSG("NonImputNoDataUserID", CommonConst.LANG_KBN_JPN)
                        'MsgBox("入力された「ユーザID」のパスワード情報が存在しません。", vbOK)
                        'Throw New UsrDefException("入力された「ユーザID」は存在しないか、無効になっています。", _msgHd.getMSG("NoTantoCD", ""), txtTanto)
                        Exit Sub
                    End If

                    '②　該当するレコードが存在する場合		
                    '②-1　DB取得パスワードの復号化	
                    '★当面保留★（暗号化なしのため、そのまま比較）

                    '②-2　パスワード同一チェック	
                    '画面)パスワードとDB)パスワードを比較
                    If Not _db.rmNullStr(ds2.Tables(RS).Rows(0)("パスワード")).Equals(userPass) Then
                        'Throw New UsrDefException("パスワードが違います。", _msgHd.getMSG("Unmatch", ""), txtPasswd)
                        _msgHd.dspMSG("NonImputPassword", CommonConst.LANG_KBN_JPN)
                        'MsgBox("パスワードが違います。", vbOK)
                        Exit Sub
                    End If

                    '■グローバル変数にセット
                    '3)	次処理起動	
                    '下記要領にしたがって、次処理に遷移を移動する。	
                    'ログイン画面から、次の項目を次処理に受け渡す。	
                    '・受け渡し項目：会社コード、会社略称、ユーザID、社員略名、BKUPサーバ接続有無（バックアップサーバ接続時:"Y"、以外:"N"）、世代番号（パスワード）

                    _loginVal.BumonCD = _db.rmNullStr(comName)                                  '会社コード
                    _loginVal.BumonNM = _db.rmNullStr(comName)                                  '会社略称
                    _loginVal.TantoCD = _db.rmNullStr(userPass)                            'ユーザＩＤ
                    _loginVal.TantoNM = _db.rmNullStr(ds.Tables(RS).Rows(0)("略名"))            '社員略名
                    _loginVal.Passwd = _db.rmNullStr(userPass)                            'パスワード
                    _loginVal.Generation = _db.rmNullStr(ds2.Tables(RS).Rows(0)("世代番号"))    '世代番号
                    _loginVal.Language = _db.rmNullStr(ds.Tables(RS).Rows(0)("言語"))           '言語
                    _loginVal.Auth = _db.rmNullStr(ds.Tables(RS).Rows(0)("権限"))               '権限
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
                    Exit Sub

                Else
                    'パスワード変更チェックなし
                    'インフォメーション表示


                    Dim openForm As Form = Nothing
                    openForm = New Information(_msgHd, _db, _langHd, Me)
                    openForm.Show()
                    Me.Hide()

                End If
            End If
        Next
    End Sub
End Class