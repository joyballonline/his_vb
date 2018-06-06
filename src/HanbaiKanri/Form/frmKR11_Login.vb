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
Imports UtilMDL.DB

Imports System.Drawing.Printing
Imports System.Runtime.InteropServices

'===================================================================================
'フォーム
'===================================================================================
Public Class frmKR11_Login

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
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf)
        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                    'MSGハンドラの設定
        _db = prmRefDbHd                                                        'DBハンドラの設定
        StartPosition = FormStartPosition.CenterScreen                          '画面中央表示
        lblVer.Text = "Ver : " & UtilClass.getAppVersion(StartUp.assembly)      'ラベルへ、バージョン情報の表示

    End Sub

    '-------------------------------------------------------------------------------
    '   終了ボタン
    '   （処理概要）フォームを閉じる
    '-------------------------------------------------------------------------------
    Private Sub btnEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnd.Click

        Me.Close()

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
        Try

            '入力チェック---------------------------------------------------------------
            Try
                Call checkInput()
            Catch lex As UsrDefException
                lex.dspMsg()
                Exit Sub
            End Try

            Dim sql As String = ""
            '担当者コード存在チェック
            Try
                sql = sql & N & "SELECT "
                sql = sql & N & "    V.ユーザーID "
                sql = sql & N & " FROM ユーザ V "
                sql = sql & N & " WHERE "
                sql = sql & N & "    V.ユーザーID = '" & _db.rmSQ(txtTanto.Text) & "'"
                Dim reccnt As Integer = 0
                Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

                If reccnt <= 0 Then

                    Throw New UsrDefException("登録されていない担当者です。", _msgHd.getMSG("NoTantoCD", ""), txtTanto)

                End If

                'ユーザ情報の抽出
                sql = ""
                sql = sql & N & "SELECT "
                sql = sql & N & "    V.ユーザーID "     '担当者コード
                sql = sql & N & "  , M.CPASSWD "        'パスワード
                sql = sql & N & "  , V.氏名 "           '氏名
                sql = sql & N & "  , M.CMNUSER "        '更新者
                sql = sql & N & "  , M.DMNDATE "        '更新日
                sql = sql & N & " FROM ユーザ V "
                sql = sql & N & " LEFT JOIN M030_TANTO_CTL M "
                sql = sql & N & " ON V.ユーザーID = M.CTANTO_CD "
                sql = sql & N & " WHERE "
                sql = sql & N & "    V.ユーザーID = '" & _db.rmSQ(txtTanto.Text) & "'"
                Dim reccnt2 As Integer = 0
                Dim ds2 = _db.selectDB(sql, RS, reccnt2)

                Dim lIdx As Long = 0
                Dim bTantoFlg As Boolean = False
                With ds2.Tables(RS)
                    For Cnt As Integer = 0 To reccnt2 - 1

                        'パスワードが存在しているかチェック
                        If ds2.Tables(RS).Rows(Cnt)("CPASSWD") Is DBNull.Value Then

                            bTantoFlg = True
                            Exit For

                        End If

                        'パスワードが一致しているかチェック
                        If Not _db.rmNullStr(ds2.Tables(RS).Rows(Cnt)("CPASSWD")).Equals(txtPasswd.Text) Then

                            Throw New UsrDefException("担当者コードとパスワードが一致しません。", _msgHd.getMSG("Unmatch", ""), txtPasswd)

                        End If

                        lIdx = Cnt
                    Next Cnt
                End With
                If bTantoFlg Then
                    MessageBox.Show("パスワードが未設定です。パスワードを設定してください。", _
                                    "購買連携システム", _
                                    MessageBoxButtons.OK, _
                                    MessageBoxIcon.Information)

                    '「パスワード変更」画面起動
                    'Dim openForm12 As frmKR12_ChangePasswd = New frmKR12_ChangePasswd(_msgHd, _db, Me, txtTanto.Text)   'パラメタを起動画面へ渡す
                    'StartUp.loginForm = Me
                    'openForm12.ShowDialog()                                                 '画面表示
                    'openForm12.Dispose()
                    Exit Sub
                End If

                '■グローバル変数にセット
                _loginVal.TantoNM = _db.rmNullStr(ds2.Tables(RS).Rows(lIdx)("氏名"))
                _loginVal.TantoCD = _db.rmNullStr(ds2.Tables(RS).Rows(lIdx)("ユーザーID"))
                _loginVal.Passwd = _db.rmNullStr(ds2.Tables(RS).Rows(lIdx)("CPASSWD"))
                _loginVal.PcName = UtilClass.getComputerName

            Catch lex As UsrDefException
                lex.dspMsg()
                Exit Sub
            End Try

            'パスワード変更チェックONの場合、パスワード変更画面を起動
            If chkPasswd.Checked = True Then

                '「パスワード変更」画面起動
                'Dim openForm12 As frmKR12_ChangePasswd = New frmKR12_ChangePasswd(_msgHd, _db, Me, txtTanto.Text)   'パラメタを起動画面へ渡す
                'StartUp.loginForm = Me
                'openForm12.ShowDialog()                                                 '画面表示
                'openForm12.Dispose()
                Exit Sub

            Else

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

        '担当者コード
        txtTanto.Text = ""
        'パスワード
        txtPasswd.Text = ""
        'パスワード変更チェックボックス
        chkPasswd.Checked = False

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