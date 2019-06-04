Imports System.IO
Imports System.Text


Namespace Log
    '===============================================================================
    '
    '  ユーティリティクラス
    '    （クラス名）    UtilLogWrier
    '    （処理機能名）      ログ出力機能を提供する
    '    （本MDL使用前提）   特になし
    '    （備考）            consoleWrite()をFalseに設定するとコンソール出力しなくなる
    '
    '===============================================================================
    '  履歴  名前          日  付      マーク      内容
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2006/04/17             新規
    '-------------------------------------------------------------------------------
    Public Class UtilLogWriter

        '===============================================================================
        'メンバー変数定義
        '===============================================================================
        Private _fileNm As String           'ログファイル名
        Private _consoleWrite As Boolean    'コンソール出力するかどうか

        '===============================================================================
        'プロパティ(アクセサ)
        '===============================================================================
        Public ReadOnly Property fileNm() As String
            'Geter--------
            Get
                fileNm = _fileNm
            End Get
            'Setter-------
            'なし
        End Property
        Public Property consoleWrite() As Boolean
            'Geter--------
            Get
                consoleWrite = _consoleWrite
            End Get
            'Setter-------
            Set(ByVal Value As Boolean)
                Dim wkVal As String
                If Value Then
                    wkVal = "コンソール出力を開始します。"
                Else
                    wkVal = "コンソール出力を停止します。"
                End If
                _consoleWrite = True
                writeLine(wkVal)
                _consoleWrite = Value
            End Set
        End Property

        '===============================================================================
        ' コンストラクタ
        '   ●入力パラメタ   ：  prmFileNm           Logファイル名(フルパス)
        '                       <prmConsoleWrite>   コンソール出力するかどうか
        '===============================================================================
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="prmFileNm">Logファイル名(フルパス)</param>
        ''' <param name="prmConsoleWrite">コンソール出力するかどうか</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal prmFileNm As String, Optional ByVal prmConsoleWrite As Boolean = False)
            _fileNm = prmFileNm
            _consoleWrite = prmConsoleWrite
        End Sub

        '-------------------------------------------------------------------------------
        '   ログ出力
        '   （処理概要）指定された文字列をログ出力する
        '               書式：１行目     YYYY/MM/DD HH:MM:DD   エラーコード＆エラーメッセージ
        '   　　　            ２行目     → SQL文など追加メッセージ(指定時のみ出力)
        '   ●入力パラメタ  ：prmOutPut      出力ログ
        '                   ：<prmOutPut2>   出力ログ２(SQL文などを想定)　改行後に出力
        '   ●メソッド戻り値 ：なし
        '   ●発生例外       ：Exception
        '                                               2006.04.17 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' ログ出力 指定された文字列をログ出力する
        ''' </summary>
        ''' <param name="prmOutPut">YYYY/MM/DD HH:MM:DD   エラーコード＆エラーメッセージ</param>
        ''' <param name="prmOutPut2">SQL文など追加メッセージ(指定時のみ出力)</param>
        ''' <remarks></remarks>
        Public Sub writeLine(ByVal prmOutPut As String, _
                             Optional ByVal prmOutPut2 As String = "")
            Dim log As StreamWriter
            Dim outStr As String
            Try
                'ログファイルオープン
                log = New StreamWriter(_fileNm, True, Encoding.UTF8)

                Try
                    '文字列編集
                    outStr = Now.ToString("G") & Space(3) & prmOutPut

                    '出力
                    log.WriteLine(outStr)
                    Debug.WriteLine(outStr)
                    If _consoleWrite Then
                        Console.WriteLine(outStr) 'コンソール出力
                    End If

                    'オプションパラメタが設定されている場合はそちらも出力
                    If (Not prmOutPut2.Equals("")) Then
                        outStr = prmOutPut2
                        log.WriteLine(outStr)
                        Debug.WriteLine(outStr)
                        If _consoleWrite Then
                            Console.WriteLine(outStr) 'コンソール出力
                        End If
                    End If

                Catch ex As Exception
                    Throw ex
                Finally
                    'ファイルクローズ
                    log.Close()
                End Try

            Catch ex2 As Exception
                Debug.WriteLine(ex2.Message)
                Debug.WriteLine(ex2.StackTrace)
                Throw ex2
            End Try

        End Sub
    End Class

End Namespace
