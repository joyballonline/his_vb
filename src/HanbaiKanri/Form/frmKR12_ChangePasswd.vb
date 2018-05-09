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
Imports UtilMDL.DB

Imports System.Drawing.Printing
Imports System.Runtime.InteropServices

'===================================================================================
'メニューフォーム
'===================================================================================
Public Class frmKR12_ChangePasswd
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
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, _
                   ByRef prmRefDbHd As UtilDBIf, _
                   ByRef prmRefForm As Form, _
                   ByVal prmValTantoCD As String)

        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _parentForm = prmRefForm                                            '親フォーム
        _parentForm.Enabled = False
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        lblTanto.Text = prmValTantoCD                                       '担当者コードを表示

    End Sub

    '-------------------------------------------------------------------------------
    '   戻るボタン
    '-------------------------------------------------------------------------------
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnback.Click

        Me.Close()
        _parentForm.Show()
        _parentForm.Enabled = True
        _parentForm.Activate()

    End Sub

    '-------------------------------------------------------------------------------
    'フォームクローズイベント
    '-------------------------------------------------------------------------------
    'Private Sub frmKR12_ChangePasswd_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    '    '親フォーム表示---------------------------------------------------------
    '    _parentForm.Show()
    '    _parentForm.Enabled = True
    '    _parentForm.Activate()

    'End Sub

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
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))     '握りつぶす
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   OKボタン押下時
    '-------------------------------------------------------------------------------
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try

            '入力チェック---------------------------------------------------------------
            Try
                Call checkInput()
            Catch lex As UsrDefException
                lex.dspMsg()
                Exit Sub
            End Try

            'Ｍ担当者に存在するか確認
            Dim sql As String = ""
            sql = sql & N & "SELECT "
            sql = sql & N & "    CTANTO_CD "
            sql = sql & N & " FROM M030_TANTO_CTL "
            sql = sql & N & " WHERE "
            sql = sql & N & "    CTANTO_CD = '" & _db.rmSQ(lblTanto.Text) & "'"
            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

            If reccnt <= 0 Then

                'レコード追加
                sql = ""
                sql = sql & N & "INSERT INTO M030_TANTO_CTL ( "
                sql = sql & N & "    CTANTO_CD "
                sql = sql & N & "  , CPASSWD "
                sql = sql & N & "  , CMNUSER "
                sql = sql & N & "  , DMNDATE "
                sql = sql & N & ") VALUES ( "
                sql = sql & N & "    '" & _db.rmSQ(lblTanto.Text) & "' "        '担当者コード
                sql = sql & N & "  , '" & _db.rmSQ(txtPasswd.Text) & "' "       '新パスワード
                sql = sql & N & "  , '" & _db.rmSQ(lblTanto.Text) & "' "        '更新者
                sql = sql & N & "  , SYSDATE "                                  '更新日
                sql = sql & N & ") "
                _db.executeDB(sql)

            Else

                'パスワードの更新
                sql = ""
                sql = sql & N & "UPDATE M030_TANTO_CTL SET "
                sql = sql & N & "    CPASSWD = '" & _db.rmSQ(txtPasswd.Text) & "'"      '新パスワード
                sql = sql & N & "  , CMNUSER = '" & _db.rmSQ(lblTanto.Text) & "'"       '更新者
                sql = sql & N & "  , DMNDATE = SYSDATE"                                 '更新日
                sql = sql & N & " WHERE "
                sql = sql & N & "    CTANTO_CD = '" & _db.rmSQ(lblTanto.Text) & "'"
                _db.executeDB(sql)

            End If

            '更新完了メッセージ
            _msgHd.dspMSG("completePWChanged")

            sql = ""
            sql = sql & N & "SELECT "
            sql = sql & N & "    V.氏名 "           '氏名
            sql = sql & N & " FROM ユーザ V "
            sql = sql & N & " LEFT JOIN M030_TANTO_CTL M "
            sql = sql & N & " ON V.ユーザーID = M.CTANTO_CD "
            sql = sql & N & " WHERE "
            sql = sql & N & "    V.ユーザーID = '" & _db.rmSQ(lblTanto.Text) & "'"
            ds = _db.selectDB(sql, RS, reccnt)

            '「連携処理一覧」画面起動
            Dim openForm13 As frmKR13_ProcList = New frmKR13_ProcList(_msgHd, _db, Me, lblTanto.Text, ds.Tables(RS).Rows(0)("氏名"))      'パラメタを起動画面へ渡す
            'StartUp.loginForm = Me
            openForm13.Show()                                                   '画面表示
            Me.Close()
            _parentForm.Hide()

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
            Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", ""), txtPasswd)
        End If

        '確認用
        If "".Equals(txtKakunin.Text) Then
            Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", ""), txtKakunin)
        End If

        '新パスワードと確認用パスワードの一致確認
        If Not txtPasswd.Text.Equals(txtKakunin.Text) Then
            Throw New UsrDefException("入力したパスワードが一致しておりません。", _msgHd.getMSG("UnmatchPasswd", ""), txtKakunin)
        End If

    End Sub

End Class
