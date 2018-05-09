'===================================================================================
'　 （システム名）      株式会社 全備様向け　販売管理システム
'
'   （機能名）          スタートアップクラス（Sub Mainを含む）
'   （クラス名）        StartUp
'   （処理機能名）      アプリケーション開始時の処理
'   （備考）            初期チェック後にメニュー画面を表示する。
'
'===================================================================================
' 履歴  名前                日付       マーク       内容
'-----------------------------------------------------------------------------------
'  (1)  鴫原                2018/01/05              新規
'-----------------------------------------------------------------------------------
Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.API
Imports UtilMDL.DB

'===================================================================================
'スタートアップクラス
'===================================================================================
Public Class StartUp

    '-------------------------------------------------------------------------------
    '構造体宣言
    '-------------------------------------------------------------------------------
    Public Structure iniType                    'INIファイル格納用
        Public LogType As String                'ログレベル
        Public LogFilePath As String            'ログファイルパス(ファイル名も含む)
        Public MsgFileName As String            'メッセージファイル名
        Public SVAddr As String                 'サーバ名（メインサーバ）
        Public PortNo As String                 'ポート番号（メインサーバ）
        Public DBName As String                 'ＤＢ（メインサーバ）
        Public UserId As String                 'ユーザ（メインサーバ）
        Public Password As String               'パスワード（メインサーバ）
        Public SVAddr_stby As String            'サーバ名（バックアップサーバ）
        Public PortNo_stby As String            'ポート番号（バックアップサーバ）
        Public DBName_stby As String            'ＤＢ（バックアップサーバ）
        Public UserId_stby As String            'ユーザ（バックアップサーバ）
        Public Password_stby As String          'パスワード（バックアップサーバ）
        Public SystemCaption As String          'ログイン表示システム名
        Public GamenSelUpperCnt As Integer      '各種入力画面検索時、警告表示する上限件数
        Public PrintSelUpperCnt As Integer      '帳票出力指示画面検索時、警告表示する上限件数
        Public BaseXlsPath As String            'EXCEl出力の雛形フォルダパス
        Public OutXlsPath As String             'EXCEl出力先フォルダパス
    End Structure

    '汎用マスタデータ格納用変数
    Public Structure hanyouM
        Public KoteiKey As String
        Public KahenKey As String
        Public Mei1 As String
        Public Mei2 As String
    End Structure

    'ログイン情報格納変数
    Public Structure loginType              'ログイン情報格納用
        Public TantoCD As String            'ユーザＩＤ
        Public Passwd As String             'パスワード
        Public Kengen As String             '権限
        Public TantoNM As String            '担当者名
        Public PcName As String             '端末名
        Public BumonCD As String            '会社コード
        Public BumonNM As String            '会社略称
        Public TantoSign As String          '担当サイン
        Public Generation As Integer        '世代番号
    End Structure

    '-------------------------------------------------------------------------------
    '定数宣言
    '-------------------------------------------------------------------------------
    '固定のファイル名やパス名
    Public Const INI_FILE As String = "HanbaiKanri.ini"                                'Iniファイル名

    'ＩＮＩファイルのセクション名称
    Private Const INIITEM1_LOGFILE As String = "Logging"                                'ログレベル/ログファイル情報
    Private Const INIITEM1_MSGFILE As String = "msg File"                               'メッセージファイル情報
    Private Const INIITEM1_DB As String = "DB"                                          'メインサーバDB接続情報
    Private Const INIITEM1_DB_STBY As String = "DB_Standby"                             'バックアップサーバDB接続情報
    Private Const INIITEM1_PRODUCT_INFO As String = "Product Info"                      'システム規定情報
    Private Const INIITEM1_EXCEL As String = "Excel Objects"                            '各種EXCEL

    'ＩＮＩファイルのキー名称
    Private Const INIITEM2_LOGTYPE As String = "LogType"                                'ログレベル
    Private Const INIITEM2_LOGFILEPATH As String = "LogFilePath"                        'ログファイルパス(ファイル名も含む)
    Private Const INIITEM2_MSGFILENAME As String = "msgFileName"                        'メッセージファイル名
    Private Const INIITEM2_SVADDR As String = "SVAddr"                                  'サーバ名（メインサーバ）
    Private Const INIITEM2_PORTNO As String = "PortNo"                                  'ポート番号（メインサーバ）
    Private Const INIITEM2_DBNAME As String = "DBName"                                  'ＤＢ（メインサーバ）
    Private Const INIITEM2_USERID As String = "UserId"                                  'ユーザ（メインサーバ）
    Private Const INIITEM2_PASSWORD As String = "Password"                              'パスワード（メインサーバ）
    Private Const INIITEM2_SVADDR_STBY As String = "SVAddr"                             'サーバ名（バックアップサーバ）
    Private Const INIITEM2_PORTNO_STBY As String = "PortNo"                             'ポート番号（バックアップサーバ）
    Private Const INIITEM2_DBNAME_STBY As String = "DBName"                             'ＤＢ（バックアップサーバ）
    Private Const INIITEM2_USERID_STBY As String = "UserId"                             'ユーザ（バックアップサーバ）
    Private Const INIITEM2_PASSWORD_STBY As String = "Password"                         'パスワード（バックアップサーバ）
    Private Const INIITEM2_SYSTEMCAPTION As String = "SystemCaption"                    'ログイン表示システム名
    Private Const INIITEM2_GAMENSELUPPERCNT As String = "GamenSelUpperCnt"              '各種入力画面検索、時警告表示する上限件数
    Private Const INIITEM2_PRINTSELUPPERCNT As String = "PrintSelUpperCnt"              '帳票出力指示画面検索時、警告表示する上限件数
    Private Const INIITEM2_BASEXLSPATH As String = "BaseXLSPath"                        'EXCEL雛形ファイルパス
    Private Const INIITEM2_OUTXLSPATH As String = "OutXLSPath"                          'EXCEL出力先フォルダパス

    Private Const RS As String = "RecSet"                           'レコードセットテーブル

    '汎用マスタ取得用列名
    Private Const HYM_KOTEIKEY As String = "clHYM_KOTEIKEY"         '汎用マスタ　固定キー
    Private Const HYM_KAHENKEY As String = "clHYM_KAHENKEY"         '汎用マスタ　可変キー
    Private Const HYM_MEI1 As String = "clHYM_MEI1"                 '汎用マスタ　名称１
    Private Const HYM_MEI2 As String = "clHYM_MEI2"                 '汎用マスタ　名称２

    'メッセージID
    Private Const SYSERR As String = "SystemErr"                'システムエラー
    Private Const NOHANYOUMST As String = "noHanyouMst"         '汎用マスタの値の取得に失敗しました。

    Private Const SETUZOKUSTR As String = "_,_"                 '汎用マスタの値を構造体から取得するための文字列の一部

    'コード選択共通子画面から返してもらう値
    Public Const HANYO_BACK_NAME1 As String = "NAME1"           '名称1
    Public Const HANYO_BACK_NAME2 As String = "NAME2"           '名称2
    Public Const HANYO_BACK_NAME3 As String = "NAME3"           '名称3
    Public Const HANYO_BACK_NAME4 As String = "NAME4"           '名称4
    Public Const HANYO_BACK_NAME5 As String = "NAME5"           '名称5

    '業務ＩＤ
    Public Const GYOMU_H01 As String = "H01"           '注文
    Public Const GYOMU_H10 As String = "H10"           '注文
    Public Const GYOMU_H03 As String = "H03"           '委託売上
    Public Const GYOMU_H04 As String = "H04"           '請求
    Public Const GYOMU_H05 As String = "H05"           '入金
    Public Const GYOMU_H06 As String = "H06"           '仕入
    Public Const GYOMU_H07 As String = "H07"           '支払
    Public Const GYOMU_G01 As String = "G01"           '原価
    Public Const GYOMU_M01 As String = "M01"           'マスタ保守


    '-------------------------------------------------------------------------------
    'メンバー変数宣言
    '-------------------------------------------------------------------------------
    Private Shared _assembly As System.Reflection.Assembly          'アセンブリ(アプリケーション情報)
    Private Shared _msgHd As UtilMsgHandler                         'メッセージハンドラ
    Private Shared _db As UtilDBIf                                  '営業受注 DBハンドラ
    Private Shared _debugMode As Boolean                            'デバッグモード(ログレベルがDEBUGの場合にTrue)

    'Iniファイル格納変数
    Public Shared _iniVal As iniType

    '汎用マスタ格納変数
    Private Shared _hanyou_tb As Hashtable = New Hashtable

    'バックアップサーバ接続判定用　True:★バックアップサーバ接続中★　False:バックアップサーバ未接続
    Private Shared _BackUpServer As Boolean = False

    '-------------------------------------------------------------------------------
    'プロパティ宣言
    '-------------------------------------------------------------------------------   
    Public Shared ReadOnly Property assembly() As System.Reflection.Assembly    'アセンブリ
        Get
            Return _assembly
        End Get
    End Property
    Public Shared ReadOnly Property iniValue() As iniType                       'ｉｎｉファイル構造体を返却
        Get
            Return _iniVal
        End Get
    End Property
    Public Shared ReadOnly Property DebugMode() As Boolean                      'デバッグモードを返却
        Get
            Return _debugMode
        End Get
    End Property
    Public Shared ReadOnly Property lCOLOR_YELLOW() As Color                    '画面表示色を返却
        Get
            Return ColorTranslator.FromWin32(12648447)
        End Get
    End Property
    Public Shared ReadOnly Property lCOLOR_BLUE() As Color                      '画面表示色を返却
        Get
            Return ColorTranslator.FromWin32(16777152)
        End Get
    End Property
    Public Shared ReadOnly Property lCOLOR_WHITE() As Color                     '画面表示色を返却
        Get
            Return Color.White
        End Get
    End Property
    '    Public Shared ReadOnly Property lCOLOR_GREEN() As Color                     '画面表示色を返却
    '        Get
    '            Return ColorTranslator.FromWin32(&H80FF80)
    '        End Get
    '    End Property
    '    Public Shared ReadOnly Property lCOLOR_GLAY() As Color                      '画面表示色を返却
    '        Get
    '            Return ColorTranslator.FromWin32(12632256)
    '        End Get
    '    End Property
    Public Shared ReadOnly Property lCOLOR_PINK() As Color                      '画面表示色を返却
        Get
            Return ColorTranslator.FromWin32(&HFFC0FF)
        End Get
    End Property
    Public Shared ReadOnly Property lCOLOR_RED() As Color                       '画面表示色を返却
        Get
            Return ColorTranslator.FromWin32(&HFF&)
        End Get
    End Property
    '    Public Shared ReadOnly Property lCOLOR_SPR_TANCHO() As Color                '画面表示色を返却
    '        Get
    '            Return ColorTranslator.FromWin32(11528071)
    '        End Get
    '    End Property
    '    Public Shared ReadOnly Property lCOLOR_SPR_HON() As Color                   '画面表示色を返却
    '        Get
    '            Return ColorTranslator.FromWin32(14942152)
    '        End Get
    '    End Property
    '    Public Shared ReadOnly Property lCOLOR_SPR_YELLOW() As Color                '画面表示色を返却
    '        Get
    '            Return ColorTranslator.FromWin32(8454143)
    '        End Get
    '    End Property
    '    Public Shared ReadOnly Property batMode() As String                         'バッチ起動モード
    '        Get
    '            Return _batMode
    '        End Get
    '    End Property
    'バックアップサーバ接続中
    Shared ReadOnly Property BackUpServer() As Boolean
        Get
            Return _BackUpServer
        End Get
    End Property
    'バックアップサーバ接続中(フォームタイトル表示用）
    Shared ReadOnly Property BackUpServerPrint() As String
        Get
            If _BackUpServer Then
                Return CommonConst.BACKUPSERVER
            Else
                Return ""
            End If
        End Get
    End Property

    '-------------------------------------------------------------------------------
    '   初めに起動するメソッド
    '   （処理概要）アプリケーション開始時の処理を行う。
    '               各種チェック後に、メニューフォームの表示を行う。
    '   ●入力パラメタ   ：なし
    '   ●メソッド戻り値 ：なし
    '   ●発生例外       ：なし(これより上位へ例外は戻せない)
    '-------------------------------------------------------------------------------
    Shared Sub main(ByVal cmds() As String)

        Dim m As System.Threading.Mutex = Nothing
        Try

            _iniVal.LogFilePath = ""

            '二重起動チェック
            m = New System.Threading.Mutex(False, "HanbaiKanri")
            '==>「システム終了時」にGC.KeepAlive(m)をCallすること
            If Not m.WaitOne(0, False) Then
                MessageBox.Show("既に同一のアプリケーションが起動しています。",
                                    System.Reflection.Assembly.GetExecutingAssembly.GetName().Name,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information)
                Exit Sub
            End If

            '自分自身のインスタンス生成 自分自身のインスタンスを生成しないとSharedメソッド以外は呼び出せない
            Dim _instance As StartUp        '自分自身
            _instance = New StartUp()
            _assembly = System.Reflection.Assembly.GetExecutingAssembly() 'アセンブリをメンバーに格納

            '-----------------------------------------------------------------------
            'ＩＮＩファイル読み込み。（ファイルの有無、ＩＮＩファイルのセクション等の各種チェックも行う）
            '-----------------------------------------------------------------------
            Try

                'メッセージ用Xmlファイルの存在チェック
                Call _instance.checkMesXmlFile()

                '初期設定ファイル読込
                Call _instance.setIniVal()

                '初期設定ファイルチェック
                Call _instance.checkIniFile()

            Catch ue As UsrDefException             'ユーザー定義例外(だけキャッチする)。他の例外は親ブロックに任せる。
                Call ue.dspMsg()                    'エラー出力
                Exit Sub                            '読み込み/チェックエラー(ユーザー定義例外の場合、エラー処理済なので終了)
            End Try

            '-----------------------------------------------------------------------
            'DBハンドラの取得およびコネクション接続
            '接続に失敗した場合、「データベースに接続できませんでした」を出力して終了する
            '-----------------------------------------------------------------------
            'UtilPostgresDebuggerはインスタンスを生成すると、DBコネクションまで生成してくれる
            Dim _db As UtilDBIf
            Try
                _db = New UtilPostgresDebugger(_iniVal.SVAddr, _iniVal.PortNo, _iniVal.DBName, _iniVal.UserId, _iniVal.Password, _iniVal.LogFilePath, _debugMode)
            Catch ex As Exception
                '確認メッセージを表示する
                Dim piRtn As Integer
                piRtn = _msgHd.dspMSG("NonPriDb")  'サーバに接続できません。バックアップサーバに接続しますか？
                If piRtn = vbNo Then
                    '・[いいえ]選択の場合	
                    '	システムを終了します。
                    GC.KeepAlive(m)
                    Exit Sub
                End If

                'DB接続失敗　バックアップサーバへ接続
                Try
                    _db = New UtilPostgresDebugger(_iniVal.SVAddr_stby, _iniVal.PortNo_stby, _iniVal.DBName_stby, _iniVal.UserId_stby, _iniVal.Password_stby, _iniVal.LogFilePath, _debugMode)
                    _BackUpServer = True
                Catch eex As Exception
                    piRtn = _msgHd.dspMSG("NonBackDb")  'サーバに接続できません。
                    GC.KeepAlive(m)
                    Exit Sub
                End Try

            End Try


                Try

                    Try
                    '汎用マスタ内容を構造体に格納
                    'Call _instance.getHanyouMST()

                Catch ex As UsrDefException             'ユーザー定義例外(だけキャッチする)。他の例外は親ブロックに任せる。
                        Call ex.dspMsg()                    'エラー出力
                        Exit Sub                            '読み込み/チェックエラー(ユーザー定義例外の場合、エラー処理済なので終了)
                    End Try

                    '-----------------------------------------------------------------------
                    '　画面起動
                    '-----------------------------------------------------------------------

                    'メニュー画面
                    Dim openForm As Form = Nothing
                    openForm = New frmC01F10_Login(_msgHd, _db)

                    'フォームオープン
                    Application.Run(openForm)
                    'ログイン画面オープン
                    'openForm.ShowDialog()

                Finally                                                                         '必ず通過する部分で後処理を行う
                    '_db.close()
                End Try

            Catch ex As Exception                       'システム例外
                'システムエラーMSG出力
                Dim tmp As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
            Finally
            Try
                '二重起動チェック用のインスタンス開放を許可する
                GC.KeepAlive(m)
            Catch ex As Exception
            End Try

        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   メッセージ用Xmlファイルの存在チェック
    '   （処理概要）ファイルが存在しない場合、アプリケーションを終了する
    '   ●入力パラメタ   ：なし
    '   ●メソッド戻り値 ：なし
    '   ●発生例外       ：なし
    '-------------------------------------------------------------------------------
    Private Sub checkMesXmlFile()
        Try

            'メッセージ用Xml存在チェック
            Dim msgFileName As String
            msgFileName = UtilClass.getAppPath(_assembly)               'アプリケーション実行パスを取得
            If Not msgFileName.EndsWith("\") Then                       '"\"で終わっていないなら
                msgFileName = msgFileName & "\"
            End If

            'メッセージファイル名を結合
            Dim ini As String = UtilClass.getAppPath(_assembly) & "\..\Setting\" & INI_FILE
            Dim fileWk As String = (New UtilIniFileHandler(ini)).getIni(INIITEM1_MSGFILE, INIITEM2_MSGFILENAME)
            msgFileName = msgFileName & "..\Setting\" & fileWk

            If UtilClass.isFileExists(msgFileName) Then                 'ファイルが存在するなら
                'メッセージハンドラを生成
                _msgHd = New UtilMsgHandler(msgFileName)                'これ以降_msgHdを使用して
            Else
                '存在しないのでエラー
                Throw New UsrDefException(fileWk & "が有りません。" & ControlChars.NewLine & _
                                          "システムの起動を中止します。" & ControlChars.NewLine & _
                                          "メッセージファイル取得　エラー")
            End If
        Catch ex As Exception
            'MSGが出力できない
            Dim t As UsrDefException = New UsrDefException("システムエラーが発生しました。" & ControlChars.NewLine & _
                                      "システムの起動を中止します。" & ControlChars.NewLine & ControlChars.NewLine & _
                                      UtilClass.getErrDetail(ex) & _
                                      ex.Message & ControlChars.NewLine & _
                                      ex.StackTrace)
            t.dspMsg()
            Throw t
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '   ＩＮＩファイル取得
    '   （処理概要）ＩＮＩファイルの設定情報を取得する。
    '   ●入力パラメタ   ：なし
    '   ●メソッド戻り値 ：なし
    '   ●発生例外       ：UsrDefException
    '-------------------------------------------------------------------------------
    Private Sub setIniVal()
        Try
            '-----------------------------------------------------------------------
            'INIファイル（フルパス）作成
            '-----------------------------------------------------------------------
            Dim iniFileName As String
            iniFileName = UtilClass.getAppPath(_assembly)               'アプリケーション実行パスを取得
            If Not iniFileName.EndsWith("\") Then                       '"\"で終わっていないなら
                iniFileName = iniFileName & "\"
            End If

            'INIファイル名を結合
            iniFileName = iniFileName & "..\Setting\" & INI_FILE

            '-----------------------------------------------------------------------
            'INIファイル存在チェック
            '-----------------------------------------------------------------------
            If Not UtilClass.isFileExists(iniFileName) Then
                Throw New UsrDefException("INIファイル存在チェックエラー", _msgHd.getMSG("nonIniFile"))
            End If

            '-----------------------------------------------------------------------
            'Iniファイルハンドラを生成する
            '-----------------------------------------------------------------------
            Dim ini As UtilIniFileHandler = New UtilIniFileHandler(iniFileName)

            '-----------------------------------------------------------------------
            'Iniファイル設定値格納
            '-----------------------------------------------------------------------
            Dim errXMLstrkey As String = ""      'キャッチした場合にエラーとなるXMLのKey

            Try
                errXMLstrkey = "NonIniKey"
                _iniVal.LogType = ini.getIni(INIITEM1_LOGFILE, INIITEM2_LOGTYPE)            'ログタイプの読み込みとデバッグモードの設定

                'デバッグモードを設定
                If _iniVal.LogType.ToUpper.Equals("DEBUG") Then
                    _debugMode = True
                Else
                    _debugMode = False
                End If

                'メッセージファイル名の読み込み
                _iniVal.MsgFileName = ini.getIni(INIITEM1_MSGFILE, INIITEM2_MSGFILENAME)

                'Logファイル
                errXMLstrkey = "NonLogDirKey"               'Logファイル格納場所の取得に失敗しました。
                _iniVal.LogFilePath = ini.getIni(INIITEM1_LOGFILE, INIITEM2_LOGFILEPATH)
                If _iniVal.LogFilePath IsNot Nothing AndAlso
                   (Not _iniVal.LogFilePath.StartsWith("..") And _iniVal.LogFilePath.StartsWith(".")) Then
                    _iniVal.LogFilePath = Mid(_iniVal.LogFilePath, 2)
                End If
                If Not _iniVal.LogFilePath.StartsWith("\") Then
                    _iniVal.LogFilePath = "\" & _iniVal.LogFilePath
                End If
                _iniVal.LogFilePath = UtilClass.getAppPath(_assembly) & _iniVal.LogFilePath

                'ＤＢ
                errXMLstrkey = "NonDbIniKey"
                _iniVal.SVAddr = ini.getIni(INIITEM1_DB, INIITEM2_SVADDR)
                errXMLstrkey = "NonDbIniKey"
                _iniVal.PortNo = ini.getIni(INIITEM1_DB, INIITEM2_PORTNO)
                errXMLstrkey = "NonDbIniKey"
                _iniVal.DBName = ini.getIni(INIITEM1_DB, INIITEM2_DBNAME)
                errXMLstrkey = "NonDbIniKey"
                _iniVal.UserId = ini.getIni(INIITEM1_DB, INIITEM2_USERID)
                errXMLstrkey = "NonDbIniKey"
                _iniVal.Password = ini.getIni(INIITEM1_DB, INIITEM2_PASSWORD)
                errXMLstrkey = "NonDbIniKey"
                _iniVal.SVAddr_stby = ini.getIni(INIITEM1_DB_STBY, INIITEM2_SVADDR_STBY)
                errXMLstrkey = "NonDbIniKey"
                _iniVal.PortNo_stby = ini.getIni(INIITEM1_DB_STBY, INIITEM2_PORTNO_STBY)
                errXMLstrkey = "NonDbIniKey"
                _iniVal.DBName_stby = ini.getIni(INIITEM1_DB_STBY, INIITEM2_DBNAME_STBY)
                errXMLstrkey = "NonDbIniKey"
                _iniVal.UserId_stby = ini.getIni(INIITEM1_DB_STBY, INIITEM2_USERID_STBY)
                errXMLstrkey = "NonDbIniKey"
                _iniVal.Password_stby = ini.getIni(INIITEM1_DB_STBY, INIITEM2_PASSWORD_STBY)

                'システム規定情報
                errXMLstrkey = "NonProductInfoIniKey"
                _iniVal.SystemCaption = ini.getIni(INIITEM1_PRODUCT_INFO, INIITEM2_SYSTEMCAPTION)
                errXMLstrkey = "NonProductInfoIniKey"
                _iniVal.GamenSelUpperCnt = ini.getIni(INIITEM1_PRODUCT_INFO, INIITEM2_GAMENSELUPPERCNT)
                errXMLstrkey = "NonProductInfoIniKey"
                _iniVal.PrintSelUpperCnt = ini.getIni(INIITEM1_PRODUCT_INFO, INIITEM2_PRINTSELUPPERCNT)

                'EXCEl雛形フォルダパス
                errXMLstrkey = "NonXlsBaseDir"                'EXCEL雛形ファイルの格納場所の取得に失敗しました。
                _iniVal.BaseXlsPath = ini.getIni(INIITEM1_EXCEL, INIITEM2_BASEXLSPATH)
                If _iniVal.BaseXlsPath IsNot Nothing AndAlso
                   (Not _iniVal.BaseXlsPath.StartsWith("..") And _iniVal.BaseXlsPath.StartsWith(".")) Then
                    _iniVal.BaseXlsPath = Mid(_iniVal.BaseXlsPath, 2)
                End If
                If Not _iniVal.BaseXlsPath.StartsWith("\") Then
                    _iniVal.BaseXlsPath = "\" & _iniVal.BaseXlsPath
                End If
                _iniVal.BaseXlsPath = UtilClass.getAppPath(_assembly) & _iniVal.BaseXlsPath

                'EXCEl出力先フォルダファイル
                errXMLstrkey = "NonXlsOutDir"                'EXCEL出力ファイルの格納場所の取得に失敗しました。
                _iniVal.OutXlsPath = ini.getIni(INIITEM1_EXCEL, INIITEM2_OUTXLSPATH)
                If _iniVal.OutXlsPath IsNot Nothing AndAlso
                   (Not _iniVal.OutXlsPath.StartsWith("..") And _iniVal.OutXlsPath.StartsWith(".")) Then
                    _iniVal.OutXlsPath = Mid(_iniVal.OutXlsPath, 2)
                End If
                If Not _iniVal.OutXlsPath.StartsWith("\") Then
                    _iniVal.OutXlsPath = "\" & _iniVal.OutXlsPath
                End If
                _iniVal.OutXlsPath = UtilClass.getAppPath(_assembly) & _iniVal.OutXlsPath

            Catch ex As Exception
                '変数（errXMLstrkey）をメッセージＩＤとして、エラーを出力します。
                Throw New UsrDefException("項目未設定エラー", _msgHd.getMSG(errXMLstrkey))
            End Try
        Catch ue As UsrDefException 'ユーザー定義例外(記述しない場合、Exceptionでキャッチされるため)
            Call ue.dspMsg()
            Throw ue                'キャッチした例外をそのままスロー(呼び出し元でエラー処理)
        Catch ex As Exception       'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex))) 'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub
    '-------------------------------------------------------------------------------
    '   ＩＮＩファイル設定内容チェック
    '   （処理概要）ＩＮＩファイルの設定内容をチェックする。
    '   ●入力パラメタ   ：なし
    '   ●メソッド戻り値 ：なし
    '   ●発生例外       ：UsrDefException
    '-------------------------------------------------------------------------------
    Private Sub checkIniFile()
        Try

            'ログファイルの出力先パスの存在チェック
            Dim fullPath As String = _iniVal.LogFilePath
            Dim pathName As String = ""
            Dim fileName As String = ""
            UtilClass.dividePathAndFile(fullPath, pathName, fileName)
            If Not UtilClass.isDirExists(pathName) Then
                _iniVal.LogFilePath = "" 'ログファイルが間違っている場合にログ出力すると実行エラーが出る為、初期化しておく
                Throw New UsrDefException("ログファイルの出力先が存在しないエラー", _msgHd.getMSG("NonLogDir"))
            End If

            '雛形フォルダ存在チェック
            If Not UtilClass.isDirExists(_iniVal.BaseXlsPath) Then
                Throw New UsrDefException("雛形ファイル格納フォルダがありません。", _msgHd.getMSG("noHinaDir"))
            End If

            'EXCEL出力先フォルダ存在チェック
            If Not UtilClass.isDirExists(_iniVal.OutXlsPath) Then
                Throw New UsrDefException("EXCELファイル出力先フォルダがありません。", _msgHd.getMSG("noOutExcelDir"))
            End If

        Catch ue As UsrDefException 'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                'キャッチした例外をそのままスロー
        Catch ex As Exception       'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex))) 'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　汎用マスタデータ取得
    '   （処理概要）汎用マスタのデータを読み込み、内部テーブルに格納する
    '-------------------------------------------------------------------------------
    Private Sub getHanyouMST()
        Try

            Dim SQL As String = ""               'SQL保持用変数
            SQL = "SELECT "
            SQL = SQL & "  HYM_KOTEIKEY         " & HYM_KOTEIKEY
            SQL = SQL & " ,HYM_KAHENKEY         " & HYM_KAHENKEY
            SQL = SQL & " ,HYM_MEI1             " & HYM_MEI1
            SQL = SQL & " ,HYM_MEI2             " & HYM_MEI2
            SQL = SQL & " FROM MZ09_HANYOU_MST "

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(SQL, RS, iRecCnt)

            If iRecCnt <= 0 Then             '抽出レコードが１件もない場合
                Throw New UsrDefException("汎用マスタの値の取得に失敗しました。", _msgHd.getMSG("noHanyouMst"))
            End If

            '検索結果を構造体に格納
            With ds.Tables(RS)
                For iCnt As Integer = 0 To iRecCnt - 1

                    Dim recKey As String = _db.rmNullStr(.Rows(iCnt)(HYM_KOTEIKEY)) & SETUZOKUSTR & _db.rmNullStr(.Rows(iCnt)(HYM_KAHENKEY))
                    Dim rec As hanyouM = New hanyouM
                    rec.KoteiKey = _db.rmNullStr(.Rows(iCnt)(HYM_KOTEIKEY))      '固定キー
                    rec.KahenKey = _db.rmNullStr(.Rows(iCnt)(HYM_KAHENKEY))      '可変キー
                    rec.Mei1 = _db.rmNullStr(.Rows(iCnt)(HYM_MEI1))              '名称１
                    rec.Mei2 = _db.rmNullStr(.Rows(iCnt)(HYM_MEI2))              '名称２
                    _hanyou_tb.Add(recKey, rec)

                Next

            End With

            ds = Nothing

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG(SYSERR, UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　汎用マスタ名称返却
    '   （処理概要）渡された固定･可変キーを元に、汎用マスタの名称１・２を返却する
    '-------------------------------------------------------------------------------
    Public Shared Function cnvKeyToName(ByVal sPrmKoteiKey As String, ByVal sPrmKahenKey As String, _
                                        Optional ByVal sPrmName2 As String = "") As String
        Try

            Dim rec As hanyouM = CType(_hanyou_tb.Item(sPrmKoteiKey & SETUZOKUSTR & sPrmKahenKey), hanyouM)
            sPrmName2 = rec.Mei2
            Return rec.Mei1

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG(SYSERR, UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Function

End Class
