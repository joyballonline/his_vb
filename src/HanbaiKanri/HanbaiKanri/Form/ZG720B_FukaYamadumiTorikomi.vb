'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）負荷山積データ取込
'    （フォームID）ZG720B_FukaYamadumiTorikomi
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   鈴木        2010/11/17                 新規              
'　(2)   菅野        2011/01/25                 変更　項目変更（製作区分→手配区分）#91              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.Text.UtilTextReader
Imports UtilMDL.FileDirectory.UtilFile
Imports UtilMDL.CommonDialog.UtilCmnDlgHandler
Public Class ZG720B_FukaYamadumiTorikomi
    Inherits System.Windows.Forms.Form

#Region "リテラル値定義"

    'PG制御文字
    Private Const RS As String = "RecSet"                       'データセットテーブル名
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const PGID As String = "ZG720B"                     'プログラムID
    Private Const IMP_LOG_NM As String = "負荷山積データ取込処理出力情報.txt" '取得できない機械略記号を出力するファイル名

    '取込ファイル列番号
    Private Const FL1COL_KOUTEI As Integer = 1              '工程コード（４桁）
    Private Const FL1COL_FUKAKUBUN As Integer = 2           '負荷区分
    Private Const FL1COL_KIBOUSYUTTAIBI As Integer = 3      '希望出来日
    Private Const FL1COL_KOUTEITYAKUSYUBI As Integer = 4    '工程着手日
    Private Const FL1COL_MCH As Integer = 5                 'MCH
    Private Const FL1COL_MH As Integer = 6                  'MH

    Private Const FL2COL_KOUTEI As Integer = 1              '工程コード（４桁）
    Private Const FL2COL_FUKAKUBUN As Integer = 2           '負荷区分
    '' 2011/01/25 CHG-S Sugano #91
    'Private Const FL2COL_SEISAKUKUBUN As Integer = 3        '製作区分
    Private Const FL2COL_TEHAIKUBUN As Integer = 3          '手配区分
    '' 2011/01/25 CHG-E Sugano #91
    Private Const FL2COL_SEIBAN As Integer = 4              '製番
    Private Const FL2COL_HINMEI As Integer = 5              '品名
    Private Const FL2COL_KIBOUSYUTTAIBI As Integer = 6      '希望出来日
    Private Const FL2COL_KOUTEITYAKUSYUBI As Integer = 7    '工程着手日
    Private Const FL2COL_MCH As Integer = 8                 'MCH
    Private Const FL2COL_MH As Integer = 9                  'MH
    Private Const FL2COL_TEHAINO As Integer = 10            '手配No

#End Region

#Region "メンバ変数定義"

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu
    Private _updFlg As Boolean = False
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
    'コンストラクタ　メニュー画面から呼ばれる
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmForm As ZC110M_Menu, ByVal prmUpdFlg As Boolean)

        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _parentForm = prmForm                                               '親フォーム
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        _updFlg = prmUpdFlg

    End Sub

#End Region

#Region "Formイベント"

    '-------------------------------------------------------------------------------
    '　処理開始イベント
    '-------------------------------------------------------------------------------
    Private Sub ZG720B_FukaYamadumiTorikomi_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            '描画関係の設定
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           'サイズが変更されたときに、コントロールがコントロール自体を再描画するかどうかを示す値を設定
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '描画はバッファで実行され、完了後に、結果が画面に出力されるよう設定
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  'コントロールは、画面に直接ではなく、まずバッファに描画されます。これにより、ちらつきを抑えることができます。
            Me.SetStyle(ControlStyles.UserPaint, True)              'コントロールは、オペレーティング システムによってではなく、独自に描画されるよう設定
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   'コントロールはウィンドウ メッセージ WM_ERASEBKGND を無視するように設定

            Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr 'タイトルオプション表示

            '初期化
            Call initForm()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub



#End Region

#Region "ボタンイベント"

    '------------------------------------------------------------------------------------------------------
    '　戻るボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnModoru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModoru.Click

        '自画面を終了し、メニュー画面に戻る。
        _parentForm.Show()
        _parentForm.Activate()

        Me.Close()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　ファイル選択ボタン（機械別）押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnKikaibetu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKikaibetu.Click
        Try

            Dim retPath As String   'openFileDialogの戻り値
            Dim openPath As String  'ダイアログ初期値候補

            'ダイアログの初期値を取得
            '今回分の入力があるなら今回分を、今回分がないなら前回分を初期値にする
            If String.Empty.Equals(txtPass1.Text) Then
                openPath = txtPastPass1.Text
            Else
                openPath = txtPass1.Text
            End If

            'ディレクトリの存在有無をチェック
            Dim pathName As String = ""     'ダイアログ初期値候補のディレクトリ
            Dim fileName As String = ""     'ダイアログ初期値候補のファイル
            UtilClass.dividePathAndFile(openPath, pathName, fileName)

            If UtilClass.isDirExists(pathName) Then
                'ディレクトリ在
                retPath = openFileDialog(pathName)
            Else
                'ディレクトリ未在
                retPath = openFileDialog()
            End If

            If Not retPath.Equals(String.Empty) Then
                txtPass1.Text = retPath
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　ファイル選択ボタン（明細）押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnYamadumi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYamadumi.Click
        Try

            Dim retPath As String   'openFileDialogの戻り値
            Dim openPath As String  'ダイアログ初期値候補

            'ダイアログの初期値を取得
            '今回分の入力があるなら今回分を、今回分がないなら前回分を初期値にする
            If String.Empty.Equals(txtPass2.Text) Then
                openPath = txtPastPass2.Text
            Else
                openPath = txtPass2.Text
            End If

            'ディレクトリの存在有無をチェック
            Dim pathName As String = ""     'ダイアログ初期値候補のディレクトリ
            Dim fileName As String = ""     'ダイアログ初期値候補のファイル
            UtilClass.dividePathAndFile(openPath, pathName, fileName)

            If UtilClass.isDirExists(pathName) Then
                'ディレクトリ在
                retPath = openFileDialog(pathName)
            Else
                'ディレクトリ未在
                retPath = openFileDialog()
            End If

            If Not retPath.Equals(String.Empty) Then
                txtPass2.Text = retPath
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　実行ボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnJikkou_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJikkou.Click

        Try
            Dim pass1 As String = txtPass1.Text     'ファイル取込パス（機械別）
            Dim pass2 As String = txtPass2.Text     'ファイル取込パス（明細）

            '取込ファイルのチェック
            Call checkFile(pass1, pass2)

            Dim pb As UtilProgressBar               'プログレスバー
            Dim t61InsertCount As Integer           'T61登録件数
            Dim t62InsertCount As Integer           'T62登録件数
            Dim notGetKikaimeiCount As Integer      'M21から取得できなかった機械名件数

            'ポインタ変更
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor

            Try
                pb = New UtilProgressBar(Me)     'プログレスバー画面を表示
                pb.Show()
                Try
                    Dim st As Date = Now    '処理開始日時
                    Dim procEndDate As Date '処理終了日時

                    'プログレスバー設定
                    pb.jobName = "負荷山積データを作成しています"
                    pb.status = "負荷山積テーブル作成中"
                    pb.value = 0

                    'トランザクション開始
                    _db.beginTran()
                    Try
                        '負荷山積テーブル作成
                        Call insertHukayamazumiTable(st, pb, pass1, t61InsertCount)

                        pb.status = "負荷山積明細テーブル作成中"
                        pb.value = 0
                        '負荷山積明細テーブル作成
                        Call insertHukayamazumiMeisaiTable(pb, pass2, t62InsertCount)

                        'M21から取得できない機械略記号をファイル出力する
                        Call printNotGetKikaimei(notGetKikaimeiCount)

                        '処理終了時間を保持
                        procEndDate = Now

                        pb.status = "ステータス変更中"
                        pb.oneStep = 1
                        pb.maxVal = 1
                        pb.value = 0
                        '処理制御テーブル更新
                        _parentForm.updateSeigyoTbl(PGID, True, st, procEndDate)
                        pb.value = 1

                        pb.status = "実行履歴作成中"
                        pb.maxVal = 1
                        pb.value = 0
                        '実行履歴作成
                        Call insertRireki(st, procEndDate, t61InsertCount, t62InsertCount, pass1, pass2)
                        pb.value = 1

                        'トランザクション終了
                        _db.commitTran()

                    Finally
                        If _db.isTransactionOpen = True Then
                            _db.rollbackTran()   'ロールバック
                        End If
                    End Try

                Finally
                    pb.Close()
                End Try

                '終了MSG
                If notGetKikaimeiCount > 0 Then
                    '取得できなかった機械略記号が存在する場合
                    Dim optionMsg As String = ""
                    optionMsg = "-----------------------------------------------------------------" & N & _
                                "工程の取得が行えないデータが" & notGetKikaimeiCount & "件存在しました。" & N & _
                                "詳細な機械略記号はログを確認してください。"
                    Call _msgHd.dspMSG("completeInsert", optionMsg)
                ElseIf t61InsertCount = 0 And t62InsertCount = 0 Then
                    '登録対象が存在しなかった場合
                    Call _msgHd.dspMSG("noInsertData")
                Else
                    '通常終了
                    Call _msgHd.dspMSG("completeInsert")
                End If

                If notGetKikaimeiCount > 0 Then
                    '取得できなかった機械略記号が存在する場合はログを表示する
                    Try
                        System.Diagnostics.Process.Start(UtilClass.getAppPath(StartUp.assembly) & "\" & IMP_LOG_NM)   '関連付いたアプリで起動
                    Catch ex As Exception
                    End Try
                End If

                '実行終了後はメニューに戻る
                Call btnModoru_Click(Nothing, Nothing)

            Finally
                Me.Cursor = cur
            End Try

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

#End Region

#Region "ユーザ定義関数"

    '-------------------------------------------------------------------------------
    '　画面起動時
    '-------------------------------------------------------------------------------
    Private Sub initForm()

        Try
            '処理年月、計画年月表示
            Call dispDate()

            '実行日時、取込件数、取込パス表示
            Call dispRireki()

            '実行ボタン使用可否
            btnJikkou.Enabled = _updFlg

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　処理年月、計画年月表示
    '　(処理概要)処理年月、計画年月を表示する
    '-------------------------------------------------------------------------------
    Private Sub dispDate()

        Try
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & " SNENGETU " & "SYORI"          '処理年月
            sql = sql & N & " ,KNENGETU " & "KEIKAKU"       '計画年月
            sql = sql & N & " FROM T01KEIKANRI "

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then            '抽出レコードが１件もない場合
                Throw New UsrDefException("登録されていません。", _msgHd.getMSG("noData"))
            End If

            Dim syoriDate As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("SYORI"))     '処理日時
            Dim keikakuDate As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("KEIKAKU")) '計画日時

            '「YYYY/MM」形式で表示
            lblSyoriDate.Text = syoriDate.Substring(0, 4) & "/" & syoriDate.Substring(4, 2)
            lblKeikakuDate.Text = keikakuDate.Substring(0, 4) & "/" & keikakuDate.Substring(4, 2)

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　'実行日時、取込件数、取込パス表示
    '　(処理概要)実行日時、取込件数、取込パスを表示する
    '-------------------------------------------------------------------------------
    Private Sub dispRireki()

        Try
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & " SDATEEND " & "JIKKOUDATE"   '実行日時
            sql = sql & N & " ,KENNSU1 " & "KIKAIBETU"    '機械別取込件数
            sql = sql & N & " ,KENNSU2 " & "MEISAI"       '明細取込件数
            sql = sql & N & " ,NAME1 " & "PASTPASS1"      '機械別パス
            sql = sql & N & " ,NAME2 " & "PASTPASS2"      '明細パス
            sql = sql & N & " FROM T91RIREKI "
            sql = sql & N & " WHERE PGID = '" & PGID & "' "
            sql = sql & N & " AND RECORDID = ("
            sql = sql & N & " SELECT "
            sql = sql & N & " MAX(RECORDID) "             'レコードIDの最大値
            sql = sql & N & " FROM T91RIREKI "
            sql = sql & N & " WHERE PGID = '" & PGID & "')"

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                lblJikkouDate.Text = "- - -"        '実行日時は"- - -"
                lblKikaibetu.Text = String.Empty    '機械別取込件数には何も表示しない
                lblMeisai.Text = String.Empty       '明細取込件数には何も表示しない
                txtPastPass1.Text = String.Empty    '機械別パスには何も表示しない
                txtPastPass2.Text = String.Empty    '明細パスには何も表示しない
                Exit Sub
            End If

            Dim jikkouDate As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("JIKKOUDATE")) '実行日時
            Dim kikaibetu As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("KIKAIBETU"))   '機械別取込件数
            Dim meisai As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("MEISAI"))         '明細取込件数
            Dim pastpass1 As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("PASTPASS1"))   '機械別パス
            Dim pastpass2 As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("PASTPASS2"))   '明細パス

            lblJikkouDate.Text = jikkouDate
            lblKikaibetu.Text = kikaibetu
            lblMeisai.Text = meisai
            txtPastPass1.Text = pastpass1
            txtPastPass2.Text = pastpass2

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　取込ファイルチェック
    '　　I　：　prmPass1   ファイル取込パス（機械別）
    '　　I　：　prmPass2   ファイル取込パス（明細）
    '-------------------------------------------------------------------------------
    Private Sub checkFile(ByVal prmPass1 As String, ByVal prmPass2 As String)

        Try
            '取込ファイルのチェック

            'パスの未入力チェック
            If String.Empty.Equals(prmPass1) Then
                'パス未入力MSG
                Throw New UsrDefException("取り込むファイルを選択してください。", _msgHd.getMSG("selectedImportFile"), txtPass1)
            End If
            If String.Empty.Equals(prmPass2) Then
                'パス未入力MSG
                Throw New UsrDefException("取り込むファイルを選択してください。", _msgHd.getMSG("selectedImportFile"), txtPass2)
            End If

            'ファイルの存在有無をチェック
            Dim pathName As String = ""     '選択ファイルのディレクトリ
            Dim fileName As String = ""     '選択ファイルのファイル
            UtilClass.dividePathAndFile(prmPass1, pathName, fileName)
            If UtilClass.isFileExists(prmPass1) = False Then
                'ファイル未在MSG
                Throw New UsrDefException("ファイルのパスが存在しません。", _msgHd.getMSG("notExistsFilePath"), txtPass1)
            End If
            UtilClass.dividePathAndFile(prmPass2, pathName, fileName)
            If UtilClass.isFileExists(prmPass2) = False Then
                'ファイル未在MSG
                Throw New UsrDefException("ファイルのパスが存在しません。", _msgHd.getMSG("notExistsFilePath"), txtPass2)
            End If

            'ファイルの内容存在チェックおよびフィールド数のチェック
            '機械別のファイルを開く
            Dim tr1 As UtilMDL.Text.UtilTextReader = New UtilMDL.Text.UtilTextReader(prmPass1)
            tr1.open()
            Try
                If tr1.EOF = True Then
                    'ファイルの内容存在チェックMSG
                    Throw New UsrDefException("ファイルの内容がありません。", _msgHd.getMSG("emptyFile"), txtPass1)
                Else
                    If tr1.readLine().Split(",").Length <> 7 Then
                        'フィールド数違いMSG
                        Throw New UsrDefException("ファイルが正しくありません。", _msgHd.getMSG("irregularFile"), txtPass1)
                    End If
                End If
            Finally
                tr1.close()
            End Try

            '明細のファイルを開く
            Dim tr2 As UtilMDL.Text.UtilTextReader = New UtilMDL.Text.UtilTextReader(prmPass2)
            tr2.open()
            Try
                If tr2.EOF = True Then
                    'ファイルの内容存在チェックMSG
                    Throw New UsrDefException("ファイルの内容がありません。", _msgHd.getMSG("emptyFile"), txtPass2)
                Else
                    If tr2.readLine().Split(",").Length <> 11 Then
                        'フィールド数違いMSG
                        Throw New UsrDefException("ファイルが正しくありません。", _msgHd.getMSG("irregularFile"), txtPass2)
                    End If
                End If
            Finally
                tr2.close()
            End Try

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   負荷山積テーブル作成
    '   （処理概要）ファイルからワークテーブル作成後、負荷山積テーブルを作成する
    '　　I　：　prmStDt   処理開始日時
    '　　I　：　prmPb     プログレスバー
    '　　I　：　prmPass1  取込ファイルパス（機械別）
    '　　O　：　prmT61InsertCount     T61登録件数
    '-------------------------------------------------------------------------------
    Private Sub insertHukayamazumiTable(ByVal prmStDt As Date, ByVal prmPb As UtilProgressBar, ByVal prmPass1 As String, ByRef prmT61InsertCount As Integer)

        Try
            Dim ht As Hashtable = New Hashtable()   'M21格納用ハッシュテーブル
            Dim col() As String                     '取込ファイルの行情報を格納する配列
            Dim cols() As String                    '取込ファイルを格納する配列
            Dim hukakubun1 As Integer = 1           '負荷区分=1
            Dim hukakubun2 As Integer = 2           '負荷区分=2
            Dim hukakubun3 As Integer = 3           '負荷区分=3
            Dim lineCount As Integer = 0            '取込ファイル中の処理対象行（カウンター）
            Dim lineTotal As Integer = 0            '取込ファイル中の処理対象行（最大値）

            prmT61InsertCount = 0

            '-->2010.12.22 chg by takagi # 35
            ''処理年月の月を取得
            'Dim syoriDateTuki As String
            'syoriDateTuki = Trim(lblSyoriDate.Text.Split("/")(1))
            '計画年月の月を取得
            Dim keikakuDateTuki As String = Trim(lblKeikakuDate.Text.Split("/")(1))
            '<--2010.12.22 chg by takagi #35

            'ハッシュテーブルに生産能力マスタを格納
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & " KIKAIMEI "    '機械名
            sql = sql & N & " ,KOUTEI "     '工程
            sql = sql & N & " FROM M21SEISAN "

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            With ds.Tables(RS)
                For i As Integer = 0 To iRecCnt - 1
                    ht.Add(_db.rmNullStr(.Rows(i)("KIKAIMEI")), .Rows(i))
                Next
            End With

            'ワークテーブルの初期化
            sql = "DELETE FROM ZG720B_W10 WHERE UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "'"
            _db.executeDB(sql)

            '機械別のファイルを開く
            Dim tr1 As UtilMDL.Text.UtilTextReader = New UtilMDL.Text.UtilTextReader(prmPass1)
            tr1.open()

            Try
                '取込ファイルを配列にセット
                Dim dic As Char() = {vbCr, vbLf}
                cols = tr1.readToEnd.TrimEnd(dic).Split(N)
                lineTotal = cols.Length

                'プログレスバー設定
                prmPb.oneStep = 10
                prmPb.maxVal = lineTotal

                Dim existsFlg As Boolean = False
                For i As Integer = 0 To lineTotal - 1
                    existsFlg = True
                    '取込ファイルの1行を配列に格納
                    col = Trim(cols(i)).Split(",")

                    Dim r As DataRow = TryCast(ht.Item(col(FL1COL_KOUTEI)), DataRow)

                    'ワークテーブルに取込ファイルを登録
                    '-->2010.12.22 chg by takagi #35
                    'If _db.rmNullStr(col(FL1COL_KIBOUSYUTTAIBI)).Substring(0, 2).Equals(syoriDateTuki) Then
                    If _db.rmNullStr(col(FL1COL_KIBOUSYUTTAIBI)).Substring(0, 2).Equals(keikakuDateTuki) Then
                        '<--2010.12.22 chg by takagi #35

                        '希望出来日の月が処理年月の月と同じ場合のみ対象とする
                        sql = "INSERT "
                        sql = sql & N & "INTO ZG720B_W10( "
                        sql = sql & N & " KOUTEI "         '工程
                        sql = sql & N & ",KIKAIMEI "       '機械名
                        sql = sql & N & ",YAMADUMIMCH "    '山積合計MCH
                        sql = sql & N & ",DHAKKOUMCH "     '製作伝票発行分MCH
                        sql = sql & N & ",DMIHAKKOUMCH "   '製作伝票未発行分MCH
                        sql = sql & N & ",GZAIKOMCH "      '月次在庫分MCH
                        sql = sql & N & ",YAMADUMIMH "     '山積MH
                        sql = sql & N & ",DHAKKOUMH "      '製作伝票発行分MH
                        sql = sql & N & ",DMIHAKKOUMH "    '製作伝票未発行分MH
                        sql = sql & N & ",GZAIKOMH "       '月次在庫分MH
                        sql = sql & N & ",UPDNAME "        '端末ID
                        sql = sql & N & ",UPDDATE "        '最終更新日
                        sql = sql & N & ")VALUES"
                        If r Is Nothing Then
                            sql = sql & N & "(NULL "
                        Else
                            sql = sql & N & "('" & _db.rmSQ(_db.rmNullStr(r("KOUTEI"))) & "' "
                        End If
                        sql = sql & N & ",'" & _db.rmSQ(col(FL1COL_KOUTEI)) & "' "
                        sql = sql & N & "," & CDec(col(FL1COL_MCH)) & " "
                        sql = sql & N & ",DECODE(" & col(FL1COL_FUKAKUBUN) & ", " & hukakubun1 & "," & CDec(col(FL1COL_MCH)) & ",0) "
                        sql = sql & N & ",DECODE(" & col(FL1COL_FUKAKUBUN) & ", " & hukakubun2 & "," & CDec(col(FL1COL_MCH)) & ",0) "
                        sql = sql & N & ",DECODE(" & col(FL1COL_FUKAKUBUN) & ", " & hukakubun3 & "," & CDec(col(FL1COL_MCH)) & ",0) "
                        sql = sql & N & "," & CDec(Trim(col(FL1COL_MH))) & " "
                        sql = sql & N & ",DECODE(" & col(FL1COL_FUKAKUBUN) & ", " & hukakubun1 & "," & CDec(col(FL1COL_MH)) & ",0) "
                        sql = sql & N & ",DECODE(" & col(FL1COL_FUKAKUBUN) & ", " & hukakubun2 & "," & CDec(col(FL1COL_MH)) & ",0) "
                        sql = sql & N & ",DECODE(" & col(FL1COL_FUKAKUBUN) & ", " & hukakubun3 & "," & CDec(col(FL1COL_MH)) & ",0) "
                        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "
                        sql = sql & N & ",to_date('" & Format(prmStDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS')) "
                        _db.executeDB(sql)
                    End If
                    prmPb.status = "負荷山積テーブル作成中  " & (lineCount + 1) & "/" & lineTotal & "件"
                    lineCount += 1
                    prmPb.value = lineCount
                Next

                '対象が存在しなかった場合
                If existsFlg = False Then
                    Exit Sub
                End If

            Finally
                tr1.close()
            End Try

            'T61の初期化
            sql = "DELETE FROM T61FUKA"
            _db.executeDB(sql)

            'ワークテーブルからT61に登録
            sql = "INSERT INTO T61FUKA "
            sql = sql & N & " SELECT "
            sql = sql & N & "  KOUTEI "
            sql = sql & N & " ,KIKAIMEI "
            sql = sql & N & " ,SUM(YAMADUMIMCH) "
            sql = sql & N & " ,SUM(DHAKKOUMCH) "
            sql = sql & N & " ,SUM(DMIHAKKOUMCH) "
            sql = sql & N & " ,SUM(GZAIKOMCH) "
            sql = sql & N & " ,SUM(YAMADUMIMH) "
            sql = sql & N & " ,SUM(DHAKKOUMH) "
            sql = sql & N & " ,SUM(DMIHAKKOUMH) "
            sql = sql & N & " ,SUM(GZAIKOMH) "
            sql = sql & N & " ,UPDNAME "
            sql = sql & N & " ,UPDDATE "
            sql = sql & N & " FROM ZG720B_W10 "
            sql = sql & N & " WHERE UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
            sql = sql & N & " GROUP BY KOUTEI, KIKAIMEI,UPDNAME,UPDDATE "

            Dim prmRefAffectedRows As Integer 'DB登録件数
            _db.executeDB(sql, prmRefAffectedRows)

            '登録件数を保持
            prmT61InsertCount = prmRefAffectedRows

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   負荷山積明細テーブル作成
    '   （処理概要）ファイルから負荷山積明細テーブルを作成する
    '　　I　：　prmPb     プログレスバー
    '　　I　：　prmPass2  取込ファイルパス（明細）
    '　　O　：　prmT62InsertCount     T62登録件数
    '-------------------------------------------------------------------------------
    Private Sub insertHukayamazumiMeisaiTable(ByVal prmPb As UtilProgressBar, ByVal prmPass2 As String, ByRef prmT62InsertCount As Integer)

        Try
            Dim ht As Hashtable = New Hashtable()   'M21格納用ハッシュテーブル
            Dim col() As String                     '取込ファイルの行情報を格納する配列
            Dim cols() As String                    '取込ファイルを格納する配列
            Dim lineCount As Integer = 0            '取込ファイル中の処理対象行（カウンター）
            Dim lineTotal As Integer = 0            '取込ファイル中の処理対象行（最大値）

            prmT62InsertCount = 0

            '-->2010.12.27 chg by takagi #56
            '処理年月の年、月を取得
            'Dim syoriDateNen As String
            'Dim syoriDateTuki As String
            'syoriDateNen = Trim(lblSyoriDate.Text.Split("/")(0))
            'syoriDateTuki = Trim(lblSyoriDate.Text.Split("/")(1))
            '計画年月の年、月を取得
            Dim keikakuDateNen As String
            Dim keikakuDateTuki As String
            keikakuDateNen = Trim(lblKeikakuDate.Text.Split("/")(0))
            keikakuDateTuki = Trim(lblKeikakuDate.Text.Split("/")(1))
            '<--2010.12.27 chg by takagi #56

            'ハッシュテーブルに生産能力マスタを格納
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & " KIKAIMEI "    '機械名
            sql = sql & N & " ,KOUTEI "     '工程
            sql = sql & N & " FROM M21SEISAN "

            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            With ds.Tables(RS)
                For i As Integer = 0 To iRecCnt - 1
                    ht.Add(_db.rmNullStr(.Rows(i)("KIKAIMEI")), .Rows(i))
                Next
            End With

            'T62の初期化
            sql = "DELETE FROM T62FUKAMEISAI"
            _db.executeDB(sql)

            '明細のファイルを開く
            Dim tr2 As UtilMDL.Text.UtilTextReader = New UtilMDL.Text.UtilTextReader(prmPass2)
            tr2.open()

            Try
                '取込ファイルを配列にセット
                Dim dic As Char() = {vbCr, vbLf}
                cols = tr2.readToEnd.TrimEnd(dic).Split(N)
                lineTotal = cols.Length

                'プログレスバー設定
                prmPb.oneStep = 10
                prmPb.maxVal = lineTotal

                'T62に取込ファイルを登録
                For i As Integer = 0 To lineTotal - 1

                    '取込ファイルの1行を配列に格納
                    col = Trim(cols(i)).Split(",")
                    Dim r As DataRow = TryCast(ht.Item(col(FL2COL_KOUTEI)), DataRow)

                    '-->2010.12.27 chg by takagi #56
                    'If _db.rmNullStr(col(FL2COL_KIBOUSYUTTAIBI)).Substring(0, 2).Equals(syoriDateTuki) Then
                    If _db.rmNullStr(col(FL2COL_KIBOUSYUTTAIBI)).Substring(0, 2).Equals(keikakuDateTuki) Then
                        '<--2010.12.27 chg by takagi #56
                        '希望出来日の月が処理年月の月と同じ場合のみ対象とする
                        sql = "INSERT "
                        sql = sql & N & "INTO T62FUKAMEISAI( "
                        '' 2011/01/25 CHG-S Sugano #91
                        'sql = sql & N & " KIKAIMEI "       '工程
                        'sql = sql & N & ",NAME "           '機械名
                        sql = sql & N & " KOUTEI "         '工程
                        sql = sql & N & ",KIKAIMEI "       '機械名
                        '' 2011/01/25 CHG-E Sugano #91
                        sql = sql & N & ",FUKAKBN "        '負荷区分
                        '' 2011/01/25 CHG-S Sugano #91
                        'sql = sql & N & ",SEISAKUKBN "     '製作区分
                        sql = sql & N & ",TEHAIKBN "       '手配区分
                        '' 2011/01/25 CHG-E Sugano #91
                        sql = sql & N & ",SEIBAN "         '製番
                        sql = sql & N & ",HINMEI "         '品名
                        sql = sql & N & ",SYUTTAIBI "      '希望出来日
                        sql = sql & N & ",TYAKUSYUBI "     '工程着手日
                        sql = sql & N & ",MCH "            'MCH
                        sql = sql & N & ",MH "             'MH
                        sql = sql & N & ",TEHAINO "        '手配NO
                        sql = sql & N & ",UPDNAME "        '端末ID
                        sql = sql & N & ",UPDDATE "        '最終更新日
                        sql = sql & N & ")VALUES"
                        If r Is Nothing Then
                            sql = sql & N & "(NULL "
                        Else
                            sql = sql & N & "('" & _db.rmSQ(_db.rmNullStr(r("KOUTEI"))) & "' "
                        End If
                        sql = sql & N & ",'" & _db.rmSQ(_db.rmNullStr(col(FL2COL_KOUTEI))) & "' "
                        sql = sql & N & ",'" & _db.rmNullStr(col(FL2COL_FUKAKUBUN)) & "' "
                        '' 2011/01/25 CHG-S Sugano #91
                        'sql = sql & N & ",'" & _db.rmSQ(_db.rmNullStr(col(FL2COL_SEISAKUKUBUN))) & "' "
                        sql = sql & N & ",'" & _db.rmSQ(_db.rmNullStr(col(FL2COL_TEHAIKUBUN))) & "' "
                        '' 2011/01/25 CHG-E Sugano #91
                        sql = sql & N & ",'" & _db.rmSQ(_db.rmNullStr(col(FL2COL_SEIBAN))) & "' "
                        sql = sql & N & ",'" & _db.rmSQ(Trim(_db.rmNullStr(col(FL2COL_HINMEI)).Replace(""""c, ""))) & "' "
                        '-->2010.12.27 chg by takagi #61
                        ''-->2010.12.27 chg by takagi #56
                        ''sql = sql & N & ",'" & syoriDateNen & _db.rmNullStr(col(FL2COL_KIBOUSYUTTAIBI)) & "' "
                        'sql = sql & N & ",'" & keikakuDateNen & _db.rmNullStr(col(FL2COL_KIBOUSYUTTAIBI)) & "' "
                        ''<--2010.12.27 chg by takagi #56
                        'If String.Empty.Equals(_db.rmNullStr(col(FL2COL_KOUTEITYAKUSYUBI))) Then
                        '    '工程着手日が空の場合、空で登録
                        '    sql = sql & N & ",'' "
                        'Else
                        '    '工程着手日が空ではない場合、希望出来日の年と比較して年を補完し登録
                        '    '-->2010.12.27 chg by takagi #56
                        '    'If _db.rmNullStr(col(FL2COL_KIBOUSYUTTAIBI)).Substring(0, 2) >= _db.rmNullStr(col(FL2COL_KOUTEITYAKUSYUBI)).Substring(0, 2) Then
                        '    '    sql = sql & N & ",'" & syoriDateNen & _db.rmNullStr(col(FL2COL_KOUTEITYAKUSYUBI)) & "' "
                        '    'Else
                        '    '    sql = sql & N & ",'" & CStr(CInt(syoriDateNen) - 1) & _db.rmNullStr(col(FL2COL_KOUTEITYAKUSYUBI)) & "' "
                        '    'End If
                        '    If _db.rmNullStr(col(FL2COL_KIBOUSYUTTAIBI)).Substring(0, 2) >= _db.rmNullStr(col(FL2COL_KOUTEITYAKUSYUBI)).Substring(0, 2) Then
                        '        sql = sql & N & ",'" & keikakuDateNen & _db.rmNullStr(col(FL2COL_KOUTEITYAKUSYUBI)) & "' "
                        '    Else
                        '        sql = sql & N & ",'" & CStr(CInt(keikakuDateNen) - 1) & _db.rmNullStr(col(FL2COL_KOUTEITYAKUSYUBI)) & "' "
                        '    End If
                        '    '<--2010.12.27 chg by takagi #56
                        'End If
                        If "0000".Equals(_db.rmNullStr(col(FL2COL_KIBOUSYUTTAIBI))) Then
                            sql = sql & N & ",null "
                        Else
                            sql = sql & N & ",'" & keikakuDateNen & _db.rmNullStr(col(FL2COL_KIBOUSYUTTAIBI)) & "' "
                        End If
                        If String.Empty.Equals(_db.rmNullStr(col(FL2COL_KOUTEITYAKUSYUBI))) Or _
                           "0000".Equals(_db.rmNullStr(col(FL2COL_KOUTEITYAKUSYUBI))) Then
                            '工程着手日が空の場合、空で登録
                            sql = sql & N & ",'' "
                        Else
                            '工程着手日が空ではない場合、希望出来日は必ず入っている前提、その年と比較して年を補完し登録()
                            If _db.rmNullStr(col(FL2COL_KIBOUSYUTTAIBI)).Substring(0, 2) >= _db.rmNullStr(col(FL2COL_KOUTEITYAKUSYUBI)).Substring(0, 2) Then
                                sql = sql & N & ",'" & keikakuDateNen & _db.rmNullStr(col(FL2COL_KOUTEITYAKUSYUBI)) & "' "
                            Else
                                sql = sql & N & ",'" & CStr(CInt(keikakuDateNen) - 1) & _db.rmNullStr(col(FL2COL_KOUTEITYAKUSYUBI)) & "' "
                            End If
                        End If
                        '<--2010.12.27 chg by takagi #61
                        sql = sql & N & "," & CDec(_db.rmNullStr(col(FL2COL_MCH)))
                        sql = sql & N & "," & CDec(_db.rmNullStr(col(FL2COL_MH)))
                        sql = sql & N & ",'" & Trim(_db.rmNullStr(col(FL2COL_TEHAINO))) & "' "
                        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "
                        sql = sql & N & ",sysdate) "

                        _db.executeDB(sql)
                        prmT62InsertCount += 1

                    End If
                    prmPb.status = "負荷山積明細テーブル作成中  " & (lineCount + 1) & "/" & lineTotal & "件"
                    lineCount += 1
                    prmPb.value = lineCount
                Next

                '対象が存在しなかった場合
                If prmT62InsertCount = 0 Then
                    Exit Sub
                End If

            Finally
                tr2.close()
            End Try

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   実行履歴作成
    '   （処理概要）確定処理用の実行履歴を作成する
    '　　I　：　prmStDt             処理開始日時
    '　　I　：　prmFinDt            処理終了日時
    '　　I　：　prmT61InsertCount   T61登録件数
    '　　I　：　prmT62InsertCount   T62登録件数
    '　　I　：　prmPass1   ファイル取込パス（機械別）
    '　　I　：　prmPass2   ファイル取込パス（明細）
    '-------------------------------------------------------------------------------
    Private Sub insertRireki(ByVal prmStDt As Date, ByVal prmFinDt As Date, ByVal prmT61InsertCount As Integer, _
                            ByVal prmT62InsertCount As Integer, ByVal prmPass1 As String, ByVal prmPass2 As String)

        Try
            Dim sql As String = ""
            sql = sql & N & "INSERT INTO T91RIREKI "
            sql = sql & N & "( "
            sql = sql & N & " PGID "       '機能ID
            sql = sql & N & ",SNENGETU "   '処理年月
            sql = sql & N & ",KNENGETU "   '計画年月
            sql = sql & N & ",SDATESTART " '処理開始日時
            sql = sql & N & ",SDATEEND "   '処理終了日時
            sql = sql & N & ",KENNSU1 "    '件数1
            sql = sql & N & ",KENNSU2 "    '件数2
            sql = sql & N & ",NAME1 "      '名称1
            sql = sql & N & ",NAME2 "      '名称2
            sql = sql & N & ",UPDNAME "    '端末ID
            sql = sql & N & ",UPDDATE "    '最終更新日
            sql = sql & N & ")VALUES( "
            sql = sql & N & " '" & PGID & "' "                                                                      '機能ID
            sql = sql & N & ",'" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "' "                              '処理年月
            sql = sql & N & ",'" & _db.rmSQ(lblKeikakuDate.Text.Replace("/", "")) & "' "                            '計画年月
            sql = sql & N & ",TO_DATE('" & Format(prmStDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '処理開始日時
            sql = sql & N & ",TO_DATE('" & Format(prmFinDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "  '処理終了日時
            sql = sql & N & ", " & prmT61InsertCount & " "                                                          '件数1
            sql = sql & N & ", " & prmT62InsertCount & " "                                                          '件数2
            sql = sql & N & ",'" & _db.rmSQ(prmPass1) & "' "                                                        '名称1
            sql = sql & N & ",'" & _db.rmSQ(prmPass2) & "' "                                                        '名称2
            sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "                                     '端末ID
            sql = sql & N & ",TO_DATE('" & Format(prmFinDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "  '最終更新日
            sql = sql & N & ") "
            _db.executeDB(sql)

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　コントロールキー押下イベント
    '　(処理概要)エンターボタン押下時に次のコントロールに移る
    '-------------------------------------------------------------------------------
    Private Sub txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPass1.KeyPress, _
                                                                                                                txtPass2.KeyPress
        Try
            '次のコントロールへ移動する
            UtilClass.moveNextFocus(Me, e)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　コントロールキーフォーカス取得イベント
    '　(処理概要)フォーカス取得時、全選択状態にする
    '-------------------------------------------------------------------------------
    Private Sub txt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPass1.GotFocus, _
                                                                                          txtPass2.GotFocus
        Try
            '全選択状態にする
            UtilClass.selAll(sender)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　工程未取得時のテキスト出力
    '　(処理概要)M21から取得できない機械略記号をテキスト出力する
    '　　O　：　prmNotGetKikaimeiCount          M21から取得できなかった機械略記号件数
    '-------------------------------------------------------------------------------
    Private Sub printNotGetKikaimei(ByRef prmNotGetKikaimeiCount As Integer)

        Try
            prmNotGetKikaimeiCount = 0

            Dim sql As String = ""
            sql = ""
            sql = "SELECT KIKAIMEI "
            sql = sql & N & "FROM (SELECT KIKAIMEI "
            sql = sql & N & "FROM T61FUKA "
            sql = sql & N & "WHERE KOUTEI IS NULL "
            sql = sql & N & "GROUP BY KIKAIMEI "
            sql = sql & N & "UNION "
            '' 2011/01/25 CHG-S Sugano #91
            'sql = sql & N & "SELECT NAME KIKAIMEI "
            'sql = sql & N & "FROM T62FUKAMEISAI "
            'sql = sql & N & "WHERE KIKAIMEI IS NULL "
            'sql = sql & N & "GROUP BY NAME) A "
            sql = sql & N & "SELECT KIKAIMEI "
            sql = sql & N & "FROM T62FUKAMEISAI "
            sql = sql & N & "WHERE KOUTEI IS NULL "
            sql = sql & N & "GROUP BY KIKAIMEI) A "
            '' 2011/01/25 CHG-E Sugano #91
            sql = sql & N & "ORDER BY KIKAIMEI "
            Dim ds As DataSet = _db.selectDB(sql, RS, prmNotGetKikaimeiCount)

            If prmNotGetKikaimeiCount > 0 Then
                Dim logBuf As System.Text.StringBuilder = New System.Text.StringBuilder
                logBuf.Append(Format(Now(), "yyyy/MM/dd HH:mm:ss") & "実行" & N)
                logBuf.Append("==========================================================" & N)
                logBuf.Append("■負荷山積データ取込処理出力情報■" & N)
                logBuf.Append("  生産能力マスタ未登録機械略記号（工程の取得が行えなかった機械略記号）" & N)
                logBuf.Append("----------------------------------------------------------" & N)
                For i As Integer = 0 To prmNotGetKikaimeiCount - 1
                    logBuf.Append(_db.rmNullStr(ds.Tables(RS).Rows(i)("KIKAIMEI")) & N)
                Next
                logBuf.Append("==========================================================")
                Dim tw As UtilMDL.Text.UtilTextWriter = New UtilMDL.Text.UtilTextWriter(UtilClass.getAppPath(StartUp.assembly) & "\" & IMP_LOG_NM)
                tw.open(False)
                Try
                    tw.writeLine(logBuf.ToString)
                Finally
                    tw.close()
                End Try

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