'===================================================================================
'　 （システム名）      在庫計画システム
'
'   （機能名）          スタートアップクラス（Sub Mainを含む）
'   （クラス名）        StartUp
'   （処理機能名）      アプリケーション開始時の処理
'   （備考）            初期チェック後にメニュー画面を表示する。
'
'===================================================================================
' 履歴  名前                日付       マーク       内容
'-----------------------------------------------------------------------------------
'  (1)  中澤                2010/07/16              新規
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
        Public UdlFilePath As String            '在庫計画 Oracle接続ファイルパス
        Public UdlFilePath_Nouhinsho As String  '納品書データ SQLSV接続ファイルパス
        Public UdlFilePath_Ukeharai As String   '受払売上統計 SQLSV接続ファイルパス
        Public BaseXlsPath As String            'EXCEl出力の雛形フォルダパス
        Public OutXlsPath As String             'EXCEl出力先フォルダパス
        Public ExcelZM610R1_Base As String      '生産能力マスタ一覧雛形ファイル名
        Public ExcelZM610R1_Out As String       '生産能力マスタ一覧出力ファイル名
        Public ExcelZG310R1_Base As String      '品種別販売計画雛形ファイル名
        Public ExcelZG310R1_Out As String       '品種別販売計画出力ファイル名
        Public ExcelZG320R1_Base As String      '品名別販売計画雛形ファイル名
        Public ExcelZG320R1_Out As String       '品名別販売計画出力ファイル名
        Public ExcelZG530R1_Base As String      '生産・販売・在庫計画雛形ファイル名
        Public ExcelZG530R1_Out As String       '生産・販売・在庫計画出力ファイル名
        Public ExcelZG530R2_Base As String      '品種別集計表雛形ファイル名
        Public ExcelZG530R2_Out As String       '品種別集計表出力ファイル名
        Public ExcelZG640R1_Base As String      '在庫補充品リスト雛形ファイル名
        Public ExcelZG640R1_Out As String       '在庫補充品リスト出力ファイル名
        Public ExcelZM120R1_Base As String      '計画対象品マスタ一覧雛形ファイル名
        Public ExcelZM120R1_Out As String       '計画対象品マスタ一覧出力ファイル名
        Public ExcelZM130R1_Base As String      '計画対象品一覧雛形ファイル名
        Public ExcelZM130R1_Out As String       '計画対象品一覧出力ファイル名
        Public ExcelZG220R1_Base As String      '生産量データ一覧雛形ファイル名
        Public ExcelZG220R1_Out As String       '生産量データ一覧出力ファイル名
        Public ExcelZG730R1_Base As String      '機械別負荷山積集計表雛形ファイル名
        Public ExcelZG730R1_Out As String       '機械別負荷山積集計表出力ファイル名
        Public ExcelZG731R1_Base As String      '機械別品名別負荷山積集計表雛形ファイル名
        Public ExcelZG731R1_Out As String       '機械別品名別負荷山積集計表出力ファイル名
        '-->2010.12.07 add by takagi
        Public ExcelZE110R1_Base As String      '販売実績雛形ファイル名
        Public ExcelZE110R1_Out As String       '販売実績出力ファイル名
        '<--2010.12.07 add by takagi
        '-->2010.12.09 add by takagi
        Public TableOwner As String             'テーブルオーナ名
        Public LinkSvForHanbai As String        'TMC101→KNDSV43へのリンクサーバ名
        Public ExcelZE210R1_Base As String      '在庫実績雛形ファイル名
        Public ExcelZE210R1_Out As String       '在庫実績出力ファイル名
        '<--2010.12.09 add by takagi
    End Structure

    'ログイン情報格納変数
    Public Structure loginType              'ログイン情報格納用
        Public TantoCD As String            '（未使用）
        Public Passwd As String             'ユーザ名
        Public Kengen As String             '権限
        Public TantoNM As String            '部門コード
        Public PcName As String             '端末名
        Public BumonCD As String            '部門コード
        Public TantoSign As String          '担当サイン
    End Structure
    '汎用マスタデータ格納用変数
    Public Structure hanyouM
        Public KoteiKey As String
        Public KahenKey As String
        Public Mei1 As String
        Public Mei2 As String
    End Structure

    '-------------------------------------------------------------------------------
    '定数宣言
    '-------------------------------------------------------------------------------
    '固定のファイル名やパス名
    Public Const INI_FILE As String = "HanbaiKanri.ini"                                'Iniファイル名

    'ＩＮＩファイルのセクション名称
    Private Const INIITEM1_LOGFILE As String = "Logging"                                'ログレベル/ログファイル情報
    Private Const INIITEM1_MSGFILE As String = "msg File"                               'メッセージファイル情報
    Private Const INIITEM1_DB As String = "DB"                                          'DB接続ファイルパス
    Private Const INIITEM1_ORACLE As String = "Oracle"                                  'DB接続情報
    Private Const INIITEM1_FILE As String = "File"                                      '各種ファイル
    Private Const INIITEM1_ID As String = "ID"                                          '端末ID
    Private Const INIITEM1_EXCEL As String = "Excel Objects"                            '各種EXCEL

    'ＩＮＩファイルのキー名称
    Private Const INIITEM2_LOGTYPE As String = "LogType"                                'ログレベル
    Private Const INIITEM2_LOGFILEPATH As String = "LogFilePath"                        'ログファイルパス(ファイル名も含む)
    Private Const INIITEM2_MSGFILENAME As String = "msgFileName"                        'メッセージファイル名
    Private Const INIITEM2_UDLFILEPATHORA As String = "UdlFilePath"                     'ORACLE接続ファイルパス
    Private Const INIITEM2_UDLFILEPATHNOHIN As String = "UdlFilePath_Nouhinsho"         '納品書データ接続ファイルパス
    Private Const INIITEM2_UDLFILEPATHUKE As String = "UdlFilePath_Ukeharai"            '受払売上統計接続ファイルパス
    Private Const INIITEM2_BASEXLSPATH As String = "BaseXLSPath"                        'EXCEL雛形ファイルパス
    Private Const INIITEM2_OUTXLSPATH As String = "OutXLSPath"                          'EXCEL出力先フォルダパス
    Private Const INIITEM2_ZM610R1_BASE As String = "ZM610R1_Base"                      '生産能力マスタメンテ雛形ファイル名
    Private Const INIITEM2_ZM610R1_OUT As String = "ZM610R1_Out"                        '生産能力マスタメンテ出力ファイル名
    Private Const INIITEM2_ZG310R1_BASE As String = "ZG310R1_Base"                      '品種別販売計画雛形ファイル名
    Private Const INIITEM2_ZG310R1_OUT As String = "ZG310R1_Out"                        '品種別販売計画出力ファイル名
    Private Const INIITEM2_ZG320R1_BASE As String = "ZG320R1_Base"                      '品名別販売計画雛形ファイル名
    Private Const INIITEM2_ZG320R1_OUT As String = "ZG320R1_Out"                        '品名別販売計画出力ファイル名
    Private Const INIITEM2_ZG530R1_BASE As String = "ZG530R1_Base"                      '生産・販売・在庫計画雛形ファイル名
    Private Const INIITEM2_ZG530R1_OUT As String = "ZG530R1_Out"                        '生産・販売・在庫計画出力ファイル名
    Private Const INIITEM2_ZG530R2_BASE As String = "ZG530R2_Base"                      '品種別集計表雛形ファイル名
    Private Const INIITEM2_ZG530R2_OUT As String = "ZG530R2_Out"                        '品種別集計表出力ファイル名
    Private Const INIITEM2_ZG640R1_Base As String = "ZG640R1_Base"                      '在庫補充品リスト雛形ファイル名
    Private Const INIITEM2_ZG640R1_OUT As String = "ZG640R1_Out"                        '在庫補充品リスト出力ファイル名
    Private Const INIITEM2_ZM120R1_BASE As String = "ZM120R1_Base"                       '計画対象品マスタ一覧雛形ファイル名
    Private Const INIITEM2_ZM120R1_OUT As String = "ZM120R1_Out"                         '計画対象品マスタ一覧出力ファイル名
    Private Const INIITEM2_ZM130R1_BASE As String = "ZM130R1_Base"                       '計画対象品一覧雛形ファイル名
    Private Const INIITEM2_ZM130R1_OUT As String = "ZM130R1_Out"                         '計画対象品一覧出力ファイル名
    Private Const INIITEM2_ZG220R1_Base As String = "ZG220R1_Base"                      '生産量データ一覧雛形ファイル名
    Private Const INIITEM2_ZG220R1_OUT As String = "ZG220R1_Out"                        '生産量データ一覧出力ファイル名
    Private Const INIITEM2_ZG730R1_Base As String = "ZG730R1_Base"                      '機械別負荷山積集計表雛形ファイル名
    Private Const INIITEM2_ZG730R1_OUT As String = "ZG730R1_Out"                        '機械別負荷山積集計表出力ファイル名
    Private Const INIITEM2_ZG731R1_Base As String = "ZG731R1_Base"                      '機械別品名別負荷山積集計表雛形ファイル名
    Private Const INIITEM2_ZG731R1_OUT As String = "ZG731R1_Out"                        '機械別品名別負荷山積集計表出力ファイル名
    '-->2010.12.07 add by takagi
    Private Const INIITEM2_ZE110R1_Base As String = "ZE110R1_Base"                      '販売実績雛形ファイル名
    Private Const INIITEM2_ZE110R1_OUT As String = "ZE110R1_Out"                        '販売実績出力ファイル名
    '<--2010.12.07 add by takagi
    '-->2010.12.09 add by takagi
    Private Const INIITEM2_TableOwner As String = "TableOwner"                          'テーブルオーナ名
    Private Const INIITEM2_LinkSvForHanbai As String = "LinkSvForHanbai"                'リンクサーバ名
    Private Const INIITEM2_ZE210R1_Base As String = "ZE210R1_Base"                      '在庫実績雛形ファイル名
    Private Const INIITEM2_ZE210R1_OUT As String = "ZE210R1_Out"                        '在庫実績出力ファイル名
    '<--2010.12.09 add by takagi

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

    '-------------------------------------------------------------------------------
    'メンバー変数宣言
    '-------------------------------------------------------------------------------
    Private Shared _assembly As System.Reflection.Assembly          'アセンブリ(アプリケーション情報)
    Private Shared _msgHd As UtilMsgHandler                         'メッセージハンドラ
    Private Shared _db As UtilDBIf                                  '営業受注 DBハンドラ
    Private Shared _debugMode As Boolean                            'デバッグモード(ログレベルがDEBUGの場合にTrue)

    'Iniファイル格納変数
    Private Shared _iniVal As iniType

    '汎用マスタ格納変数
    Private Shared _hanyou_tb As Hashtable = New Hashtable

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
            'UtilOleDBDebuggerはインスタンスを生成すると、DBコネクションまで生成してくれる
            Dim iniWk As iniType = StartUp._iniVal
            '_db = New UtilOleDBDebugger(iniWk.UdlFilePath, iniWk.LogFilePath, _debugMode)
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
                'openForm = New frmMZ01_01M_MainMenu(_msgHd, _db)
                'openForm = New ZC110M_Menu(_msgHd, _db)
                'openForm = New Sample_Chumon(_msgHd, _db)
                openForm = New frmKR11_Login(_msgHd, _db)
                'フォームオープン
                Application.Run(openForm)

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
                If _iniVal.LogFilePath IsNot Nothing AndAlso _
                   (Not _iniVal.LogFilePath.StartsWith("..") And _iniVal.LogFilePath.StartsWith(".")) Then
                    _iniVal.LogFilePath = Mid(_iniVal.LogFilePath, 2)
                End If
                If Not _iniVal.LogFilePath.StartsWith("\") Then
                    _iniVal.LogFilePath = "\" & _iniVal.LogFilePath
                End If
                _iniVal.LogFilePath = UtilClass.getAppPath(_assembly) & _iniVal.LogFilePath

                ''UDLファイル
                'errXMLstrkey = "NonDbUdlKey"                'ＤＢ接続ファイルの格納場所の取得に失敗しました。
                '_iniVal.UdlFilePath = ini.getIni(INIITEM1_DB, INIITEM2_UDLFILEPATHORA)
                'If _iniVal.UdlFilePath IsNot Nothing AndAlso _
                '   (Not _iniVal.UdlFilePath.StartsWith("..") And _iniVal.UdlFilePath.StartsWith(".")) Then
                '    _iniVal.UdlFilePath = Mid(_iniVal.UdlFilePath, 2)
                'End If
                'If Not _iniVal.UdlFilePath.StartsWith("\") Then
                '    _iniVal.UdlFilePath = "\" & _iniVal.UdlFilePath
                'End If
                '_iniVal.UdlFilePath = UtilClass.getAppPath(_assembly) & _iniVal.UdlFilePath

                ''UDLファイル
                'errXMLstrkey = "NonDbUdlKey"                'ＤＢ接続ファイルの格納場所の取得に失敗しました。
                '_iniVal.UdlFilePath_Nouhinsho = ini.getIni(INIITEM1_DB, INIITEM2_UDLFILEPATHNOHIN)
                'If _iniVal.UdlFilePath_Nouhinsho IsNot Nothing AndAlso _
                '   (Not _iniVal.UdlFilePath_Nouhinsho.StartsWith("..") And _iniVal.UdlFilePath_Nouhinsho.StartsWith(".")) Then
                '    _iniVal.UdlFilePath_Nouhinsho = Mid(_iniVal.UdlFilePath_Nouhinsho, 2)
                'End If
                'If Not _iniVal.UdlFilePath_Nouhinsho.StartsWith("\") Then
                '    _iniVal.UdlFilePath_Nouhinsho = "\" & _iniVal.UdlFilePath_Nouhinsho
                'End If
                '_iniVal.UdlFilePath_Nouhinsho = UtilClass.getAppPath(_assembly) & _iniVal.UdlFilePath_Nouhinsho

                ''UDLファイル
                'errXMLstrkey = "NonDbUdlKey"                'ＤＢ接続ファイルの格納場所の取得に失敗しました。
                '_iniVal.UdlFilePath_Ukeharai = ini.getIni(INIITEM1_DB, INIITEM2_UDLFILEPATHUKE)
                'If _iniVal.UdlFilePath_Ukeharai IsNot Nothing AndAlso _
                '   (Not _iniVal.UdlFilePath_Ukeharai.StartsWith("..") And _iniVal.UdlFilePath_Ukeharai.StartsWith(".")) Then
                '    _iniVal.UdlFilePath_Ukeharai = Mid(_iniVal.UdlFilePath_Ukeharai, 2)
                'End If
                'If Not _iniVal.UdlFilePath_Ukeharai.StartsWith("\") Then
                '    _iniVal.UdlFilePath_Ukeharai = "\" & _iniVal.UdlFilePath_Ukeharai
                'End If
                '_iniVal.UdlFilePath_Ukeharai = UtilClass.getAppPath(_assembly) & _iniVal.UdlFilePath_Ukeharai

                'EXCEl雛形フォルダパス
                errXMLstrkey = "NonXlsBaseDir"                'EXCEl雛形ファイルの格納場所の取得に失敗しました。
                _iniVal.BaseXlsPath = ini.getIni(INIITEM1_EXCEL, INIITEM2_BASEXLSPATH)
                If _iniVal.BaseXlsPath IsNot Nothing AndAlso _
                   (Not _iniVal.BaseXlsPath.StartsWith("..") And _iniVal.BaseXlsPath.StartsWith(".")) Then
                    _iniVal.BaseXlsPath = Mid(_iniVal.BaseXlsPath, 2)
                End If
                If Not _iniVal.BaseXlsPath.StartsWith("\") Then
                    _iniVal.BaseXlsPath = "\" & _iniVal.BaseXlsPath
                End If
                _iniVal.BaseXlsPath = UtilClass.getAppPath(_assembly) & _iniVal.BaseXlsPath

                'EXCEl出力先フォルダファイル
                errXMLstrkey = "NonXlsOutDir"                'EXCEl出力ファイルの格納場所の取得に失敗しました。
                _iniVal.OutXlsPath = ini.getIni(INIITEM1_EXCEL, INIITEM2_OUTXLSPATH)
                If _iniVal.ExcelZM610R1_Base IsNot Nothing AndAlso _
                   (Not _iniVal.OutXlsPath.StartsWith("..") And _iniVal.OutXlsPath.StartsWith(".")) Then
                    _iniVal.OutXlsPath = Mid(_iniVal.OutXlsPath, 2)
                End If
                If Not _iniVal.OutXlsPath.StartsWith("\") Then
                    _iniVal.OutXlsPath = "\" & _iniVal.OutXlsPath
                End If
                _iniVal.OutXlsPath = UtilClass.getAppPath(_assembly) & _iniVal.OutXlsPath

                ''生産能力マスタEXCEl雛形(ZM610R1)ファイル名
                'errXMLstrkey = "noZM610R1_Base"        '生産能力マスタ雛形ファイル名が設定されていません。
                '_iniVal.ExcelZM610R1_Base = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZM610R1_BASE)

                ''生産能力マスタEXCEl出力(ZM610R1)ファイル名
                'errXMLstrkey = "noZM610R1_Out"        '生産能力マスタ出力ファイル名が設定されていません。
                '_iniVal.ExcelZM610R1_Out = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZM610R1_OUT)

                ''品種別販売計画EXCEl雛形(ZG310R1)ファイル名
                'errXMLstrkey = "noZG310R1_Base"        '品種別販売計画雛形ファイル名が設定されていません。
                '_iniVal.ExcelZG310R1_Base = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG310R1_BASE)

                ''品種別販売計画EXCEl出力(ZG310R1)ファイル名
                'errXMLstrkey = "noZG310R1_Out"        '品種別販売計画出力ファイル名が設定されていません。
                '_iniVal.ExcelZG310R1_Out = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG310R1_OUT)

                ''品名別販売計画EXCEl雛形(ZG320R1)ファイル名
                'errXMLstrkey = "noZG320R1_Base"        '品名別販売計画雛形ファイル名が設定されていません。
                '_iniVal.ExcelZG320R1_Base = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG320R1_BASE)

                ''品名別販売計画EXCEl出力(ZG320R1)ファイル名
                'errXMLstrkey = "noZG320R1_Out"        '品名別販売計画出力ファイル名が設定されていません。
                '_iniVal.ExcelZG320R1_Out = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG320R1_OUT)

                ''生産・販売・在庫計画EXCEl雛形(ZG530R1)ファイル名
                'errXMLstrkey = "noZG530R1_Base"        '生産・販売・在庫計画雛形ファイル名が設定されていません。
                '_iniVal.ExcelZG530R1_Base = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG530R1_BASE)

                ''生産・販売・在庫計画EXCEl出力(ZG530R1)ファイル名
                'errXMLstrkey = "noZG530R1_Out"        '生産・販売・在庫計画出力ファイル名が設定されていません。
                '_iniVal.ExcelZG530R1_Out = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG530R1_OUT)

                ''品種別集計表EXCEl雛形(ZG530R2)ファイル名
                'errXMLstrkey = "noZG530R2_Base"        '品種別集計表雛形ファイル名が設定されていません。
                '_iniVal.ExcelZG530R2_Base = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG530R2_BASE)

                ''品種別集計表EXCEl出力(ZG530R2)ファイル名
                'errXMLstrkey = "noZG530R2_Out"        '品種別集計表出力ファイル名が設定されていません。
                '_iniVal.ExcelZG530R2_Out = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG530R2_OUT)

                ''在庫補充品リストEXCEl雛形(ZG640R1)ファイル名
                'errXMLstrkey = "noZG640R1_Base"        '在庫補充品リスト雛形ファイルが存在しません。
                '_iniVal.ExcelZG640R1_Base = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG640R1_Base)
                ''在庫補充品リストEXCEl出力(ZG640R1)ファイル名
                'errXMLstrkey = "noZG640R1_Out"        '在庫補充品リスト出力ファイル名が設定されていません。
                '_iniVal.ExcelZG640R1_Out = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG640R1_OUT)

                ''計画対象品マスタ一覧EXCEl雛形(ZM120R1)ファイル名
                'errXMLstrkey = "noZM120R1_Base"        '品種別集計表出力ファイル名が設定されていません。
                '_iniVal.ExcelZM120R1_Base = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZM120R1_BASE)

                ''計画対象品マスタ一覧EXCEl出力(ZM120R1)ファイル名
                'errXMLstrkey = "noZM120R1_Out"        '品種別集計表出力ファイル名が設定されていません。
                '_iniVal.ExcelZM120R1_Out = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZM120R1_OUT)

                ''計画対象品一覧EXCEl雛形(ZM130R1)ファイル名
                'errXMLstrkey = "noZM130R1_Base"        '品種別集計表出力ファイル名が設定されていません。
                '_iniVal.ExcelZM130R1_Base = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZM130R1_BASE)

                ''計画対象品一覧EXCEl出力(ZM130R1)ファイル名
                'errXMLstrkey = "noZM130R1_Out"        '品種別集計表出力ファイル名が設定されていません。
                '_iniVal.ExcelZM130R1_Out = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZM130R1_OUT)


                ''生産量データ一覧EXCEl雛形(ZG210R1)ファイル名
                'errXMLstrkey = "noZG220R1_Base"        '生産量データ一覧雛形ファイルが存在しません。
                '_iniVal.ExcelZG220R1_Base = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG220R1_Base)
                ''生産量データ一覧EXCEl出力(ZG210R1)ファイル名
                'errXMLstrkey = "noZG220R1_Out"        '在庫補充品リスト出力ファイル名が設定されていません。
                '_iniVal.ExcelZG220R1_Out = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG220R1_OUT)

                ''機械別負荷山積集計表EXCEl雛形(ZG730R1)ファイル名
                'errXMLstrkey = "noZG730R1_Base"        '機械別負荷山積集計表雛形ファイルが存在しません。
                '_iniVal.ExcelZG730R1_Base = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG730R1_Base)
                ''機械別負荷山積集計表EXCEl出力(ZG730R1)ファイル名
                'errXMLstrkey = "noZG730R1_Out"        '機械別負荷山積集計表出力ファイル名が設定されていません。
                '_iniVal.ExcelZG730R1_Out = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG730R1_OUT)

                ''機械別品名別負荷山積集計表EXCEl雛形(ZG731R1)ファイル名
                'errXMLstrkey = "noZG731R1_Base"        '機械別品名別負荷山積集計表雛形ファイルが存在しません。
                '_iniVal.ExcelZG731R1_Base = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG731R1_Base)
                ''機械別品名別負荷山積集計表EXCEl出力(ZG731R1)ファイル名
                'errXMLstrkey = "noZG731R1_Out"        '機械別品名別負荷山積集計表出力ファイル名が設定されていません。
                '_iniVal.ExcelZG731R1_Out = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG731R1_OUT)

                ''-->2010.12.07 add by takagi
                ''販売実績EXCEl雛形(ZE110R1)ファイル名
                'errXMLstrkey = "noZE110R1_Base"        '販売実績雛形ファイルが存在しません。
                '_iniVal.ExcelZE110R1_Base = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZE110R1_Base)
                ''販売実績EXCEl出力(ZE110R1)ファイル名
                'errXMLstrkey = "noZE110R1_Out"        '販売実績出力ファイル名が設定されていません。
                '_iniVal.ExcelZE110R1_Out = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZE110R1_OUT)
                ''<--2010.12.07 add by takagi

                ''-->2010.12.09 add by takagi
                ''テーブルオーナ名
                'errXMLstrkey = "noTableOwner"        'テーブルオーナ名が存在しません。
                '_iniVal.TableOwner = ini.getIni(INIITEM1_DB, INIITEM2_TableOwner)
                ''リンクサーバー名
                'errXMLstrkey = "noLinkSvForHanbai"        'リンクサーバー名が存在しません。
                '_iniVal.LinkSvForHanbai = ini.getIni(INIITEM1_DB, INIITEM2_LinkSvForHanbai)

                ''在庫実績EXCEl雛形(ZE210R1)ファイル名
                'errXMLstrkey = "noZE210R1_Base"        '販売実績雛形ファイルが存在しません。
                '_iniVal.ExcelZE210R1_Base = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZE210R1_Base)
                ''在庫実績EXCEl出力(ZE210R1)ファイル名
                'errXMLstrkey = "noZE210R1_Out"        '販売実績出力ファイル名が設定されていません。
                '_iniVal.ExcelZE210R1_Out = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZE210R1_OUT)
                ''<--2010.12.09 add by takagi

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

            ''生産能力マスタEXCEl雛形(ZM610R1)ファイル存在チェック
            'If Not UtilClass.isFileExists(_iniVal.BaseXlsPath & "\" & _iniVal.ExcelZM610R1_Base) Then
            '    Throw New UsrDefException("生産能力マスタ雛形ファイルが見つかりません。", _msgHd.getMSG("notExistZM610R1_Base"))
            'End If

            ''品種別販売計画EXCEl雛形(ZG310R1)ファイル存在チェック
            'If Not UtilClass.isFileExists(_iniVal.BaseXlsPath & "\" & _iniVal.ExcelZG310R1_Base) Then
            '    Throw New UsrDefException("品種別販売計画雛形ファイルが見つかりません。", _msgHd.getMSG("notExistZG310R1_Base"))
            'End If

            ''品名別販売計画EXCEl雛形(ZG320R1)ファイル存在チェック
            'If Not UtilClass.isFileExists(_iniVal.BaseXlsPath & "\" & _iniVal.ExcelZG310R1_Base) Then
            '    Throw New UsrDefException("品名別販売計画雛形ファイルが見つかりません。", _msgHd.getMSG("notExistZG320R1_Base"))
            'End If

            ''生産・販売・在庫計画EXCEl雛形(ZG530R1)ファイル存在チェック
            'If Not UtilClass.isFileExists(_iniVal.BaseXlsPath & "\" & _iniVal.ExcelZG530R1_Base) Then
            '    Throw New UsrDefException("生産・販売・在庫計画雛形ファイルが見つかりません。", _msgHd.getMSG("notExistZG530R1_Base"))
            'End If

            ''品種別集計表EXCEl雛形(ZG530R2)ファイル存在チェック
            'If Not UtilClass.isFileExists(_iniVal.BaseXlsPath & "\" & _iniVal.ExcelZG530R2_Base) Then
            '    Throw New UsrDefException("品種別集計表雛形ファイルが見つかりません。", _msgHd.getMSG("notExistZG530R2_Base"))
            'End If

            ''計画対象品マスタ一覧EXCEl雛形(ZM120R1)ファイル存在チェック
            'If Not UtilClass.isFileExists(_iniVal.BaseXlsPath & "\" & _iniVal.ExcelZM120R1_Base) Then
            '    Throw New UsrDefException("計画対象品マスタ一覧雛形ファイルが見つかりません。", _msgHd.getMSG("notExistZM120R1_Base"))
            'End If

            ''計画対象品一覧EXCEl雛形(ZM130R1)ファイル存在チェック
            'If Not UtilClass.isFileExists(_iniVal.BaseXlsPath & "\" & _iniVal.ExcelZM130R1_Base) Then
            '    Throw New UsrDefException("計画対象品一覧雛形ファイルが見つかりません。", _msgHd.getMSG("notExistZM130R1_Base"))
            'End If

            ''機械別負荷山積集計表EXCEl雛形(ZG730R1)ファイル存在チェック
            'If Not UtilClass.isFileExists(_iniVal.BaseXlsPath & "\" & _iniVal.ExcelZG730R1_Base) Then
            '    Throw New UsrDefException("機械別負荷山積集計表雛形ファイルが見つかりません。", _msgHd.getMSG("notExistZG730R1_Base"))
            'End If

            ''機械別品名別負荷山積集計表EXCEl雛形(ZG731R1)ファイル存在チェック
            'If Not UtilClass.isFileExists(_iniVal.BaseXlsPath & "\" & _iniVal.ExcelZG731R1_Base) Then
            '    Throw New UsrDefException("機械別品名別負荷山積集計表雛形ファイルが見つかりません。", _msgHd.getMSG("notExistZG731R1_Base"))
            'End If

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
