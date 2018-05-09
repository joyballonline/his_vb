'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）汎用マスタ　マスタ項目編集画面
'    （フォームID）ZM311S_HanyouMstHensyuu
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   中澤        2010/09/08                 新規              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB

Public Class ZM311S_HanyouMstHensyuu
    Inherits System.Windows.Forms.Form

#Region "リテラル値定義"

    'PG制御文字
    Private Const RS As String = "RecSet"                       'データセットテーブル名
    Private Const N As String = ControlChars.NewLine            '改行文字

    Private Const KOTEI As String = "00"                     '画面表示用固定キー

    Private Const PGID As String = "ZM311S"             'DB登録時に使用する機能ID

#End Region

#Region "メンバ変数定義"

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As Form

    Private _koteiKey As String
    '-->2010.12/12 add by takagi 
    '-------------------------------------------------------------------------------
    '   オーバーライドプロパティで×ボタンだけを無効にする(ControlBoxはTrueのまま使用可能)
    '-------------------------------------------------------------------------------
    Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
        Get
            Const CS_NOCLOSE As Integer = &H200

            Dim tmpCreateParams As System.Windows.Forms.CreateParams = MyBase.CreateParams
            tmpCreateParams.ClassStyle = tmpCreateParams.ClassStyle Or CS_NOCLOSE

            Return tmpCreateParams
        End Get
    End Property
    '<--2010.12/12 add by takagi 

#End Region

#Region "コンストラクタ"

    '-------------------------------------------------------------------------------
    'コンストラクタ（Privateにして、外からは呼べないようにする）
    '-------------------------------------------------------------------------------
    Private Sub New()
        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

    End Sub

    '-------------------------------------------------------------------------------
    'コンストラクタ　マスタメンテ画面から呼ばれる
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmForm As Form, ByVal prmKoteiKey As String)
        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _parentForm = prmForm                                               '親フォーム
        _koteiKey = prmKoteiKey                                             '親から受け取った固定キー
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
    End Sub

#End Region

#Region "Formイベント"

    '-------------------------------------------------------------------------------
    '　処理開始イベント
    '-------------------------------------------------------------------------------
    Private Sub ZM311S_HanyouMstHensyuu_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            '描画関係の設定
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           'サイズが変更されたときに、コントロールがコントロール自体を再描画するかどうかを示す値を設定
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '描画はバッファで実行され、完了後に、結果が画面に出力されるよう設定
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  'コントロールは、画面に直接ではなく、まずバッファに描画されます。これにより、ちらつきを抑えることができます。
            Me.SetStyle(ControlStyles.UserPaint, True)              'コントロールは、オペレーティング システムによってではなく、独自に描画されるよう設定
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   'コントロールはウィンドウ メッセージ WM_ERASEBKGND を無視するように設定

            Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr 'タイトルオプション表示

            '画面初期表示
            Call dispLbl()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

#End Region

#Region "ボタンイベント"

    '------------------------------------------------------------------------------------------------------
    '　登録ボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnTouroku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTouroku.Click
        Try

            '入力チェック
            Call check()

            '登録確認ダイアログ表示
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmInsert")   '登録します。よろしいですか？
            If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            'DB更新
            Call updateDB()

            _msgHd.dspMSG("completeInsert")     '登録が完了しました。

            Call Button1_Click(Nothing, Nothing)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　戻るボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModoru.Click

        '自画面を終了し、メニュー画面に戻る。
        _parentForm.Show()
        _parentForm.Activate()

        Me.Close()

    End Sub

#End Region

#Region "ユーザ定義関数:DB関連"

    '------------------------------------------------------------------------------------------------------
    '　画面初期表示
    '　(処理内容)渡された固定キーを元に汎用マスタの値を画面表示する
    '------------------------------------------------------------------------------------------------------
    Private Sub dispLbl()
        Try

            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & " KOTEIKEY " & "koteikey"
            sql = sql & N & " ,KAHENKEY " & "kahenkey"
            sql = sql & N & " ,NAME1 " & "name"
            sql = sql & N & " ,BIKO " & "biko"
            sql = sql & N & " FROM M01HANYO "
            sql = sql & N & " WHERE KOTEIKEY = '" & KOTEI & "'"
            sql = sql & N & " AND KAHENKEY = '" & _db.rmSQ(_koteiKey) & "'"

            'SQL発行
            Dim recCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, recCnt)

            If recCnt <= 0 Then                    '抽出レコードが１件もない場合
                Throw New UsrDefException("登録されていません。", _msgHd.getMSG("noData"))
            End If

            Dim biko As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("biko"))
            Dim kahenKey As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("kahenkey"))
            Dim name As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("name"))

            lblKoteiKey.Text = kahenKey
            txtKoumokumei.Text = name
            txtKoumokuSetumei.Text = biko

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　DB更新
    '------------------------------------------------------------------------------------------------------
    Private Sub updateDB()
        Try
            Dim updStartDate As Date = Now      '処理開始日時

            _db.beginTran()

            Dim sql As String = ""
            sql = "UPDATE "
            sql = sql & N & " M01HANYO SET "
            sql = sql & N & " NAME1 = '" & txtKoumokumei.Text & "'"                     '名称１
            sql = sql & N & " ,BIKO = '" & _db.rmNullStr(txtKoumokuSetumei.Text) & "'"  '備考
            sql = sql & N & " ,UPDDATE = SYSDATE"                                       '更新日
            sql = sql & N & " WHERE KOTEIKEY = '" & KOTEI & "'"
            sql = sql & N & " AND KAHENKEY = '" & _db.rmSQ(_koteiKey) & "'"

            'SQL発行
            Dim recCnt As Integer
            Call _db.executeDB(sql, recCnt)
            If recCnt <= 0 Then
                Call _db.rollbackTran()
                Throw New UsrDefException("登録に失敗しました。アプリケーションを終了します。", _msgHd.getMSG("failRegist"))
            End If

            Dim machineId As String = SystemInformation.ComputerName    '端末Id
            Dim updFinDate As Date = Now                                   '更新日時および処理終了日時

            Dim deleteCount As String = 0
            Dim insertCount As String = 1

            '実行履歴テーブル更新
            sql = ""
            sql = sql & N & " INSERT INTO T91RIREKI ("
            sql = sql & N & " PGID, "
            sql = sql & N & " SDATESTART, "
            sql = sql & N & " SDATEEND, "
            sql = sql & N & " KENNSU1, "
            sql = sql & N & " KENNSU2, "
            sql = sql & N & " NAME1, "
            sql = sql & N & " UPDNAME, "
            sql = sql & N & " UPDDATE) "
            sql = sql & N & " VALUEs ("
            sql = sql & N & "   '" & PGID & "', "
            sql = sql & N & "       TO_DATE('" & updStartDate & "',  'YYYY/MM/DD HH24:MI:SS'), "
            sql = sql & N & "       TO_DATE('" & updFinDate & "',  'YYYY/MM/DD HH24:MI:SS'), "
            sql = sql & N & "       " & deleteCount & " , "
            sql = sql & N & "       " & insertCount & " , "
            sql = sql & N & "       '" & KOTEI & "', "
            sql = sql & N & "   '" & machineId & "' ,"
            sql = sql & N & "   TO_DATE('" & updFinDate & "', 'YYYY/MM/DD HH24:MI:SS')) "

            _db.executeDB(sql, recCnt)
            If recCnt <= 0 Then
                Call _db.rollbackTran()
                Throw New UsrDefException("登録に失敗しました。アプリケーションを終了します。", _msgHd.getMSG("failRegist"))
            End If

            _db.commitTran()

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        Finally
            If _db.isTransactionOpen Then
                Call _db.rollbackTran()
            End If
        End Try
    End Sub

#End Region

#Region "ユーザ定義関数:チェック処理"


    '------------------------------------------------------------------------------------------------------
    '　入力チェック
    '------------------------------------------------------------------------------------------------------
    Private Sub check()
        Try

            If "".Equals(txtKoumokumei.Text) Then
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput"))
            End If

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

#End Region

End Class