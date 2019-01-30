Imports System.IO
Imports System.Text


Namespace Log
    '===============================================================================
    '
    '  ユーティリティクラス
    '    （クラス名）    UtilLogDebugger
    '    （処理機能名）      ログ出力拡張機能を提供する
    '    （本MDL使用前提）   UtilLogWriterがプロジェクトに取り込まれていること
    '    （備考）            UtilLogWriterを継承
    '
    '===============================================================================
    '  履歴  名前          日  付      マーク      内容
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2006/04/18             新規
    '-------------------------------------------------------------------------------
    Public Class UtilLogDebugger
        Inherits UtilLogWriter

        '===============================================================================
        'メンバー定数定義
        '===============================================================================
        Public Const LOG_DEBUG As Short = 1   'ログ出力タイプ＝デバッグ
        Public Const LOG_INFO As Short = 2    'ログ出力タイプ＝インフォメーション
        Public Const LOG_ERR As Short = 3     'ログ出力タイプ＝エラー

        '===============================================================================
        'メンバー変数定義
        '===============================================================================
        Private _debugFlg As Boolean                'デバッグモード

        '===============================================================================
        'プロパティ(アクセサ)
        '===============================================================================
        Public Property debugFlg() As Boolean
            'Geter--------
            Get
                debugFlg = _debugFlg
            End Get
            'Setter-------
            Set(ByVal Value As Boolean)
                _debugFlg = Value
                Call MyBase.writeLine("デバッグモードを[" & _debugFlg.ToString & "]に変更します。")
            End Set
        End Property

        '===============================================================================
        ' コンストラクタ
        '   ●入力パラメタ   ：  prmFileNm           Logファイル名(フルパス)
        '                       prmDebugFlg         デバッグモード
        '                       <prmConsoleWrite>   コンソール出力するかどうか
        '===============================================================================
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="prmFileNm">Logファイル名(フルパス)</param>
        ''' <param name="prmDebugFlg">デバッグモード</param>
        ''' <param name="prmConsoleWrite">コンソール出力するかどうか</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal prmFileNm As String, _
                       ByVal prmDebugFlg As Boolean, _
                       Optional ByVal prmConsoleWrite As Boolean = True)
            MyBase.New(prmFileNm, prmConsoleWrite)
            _debugFlg = prmDebugFlg
        End Sub

        '-------------------------------------------------------------------------------
        '   拡張ログ出力
        '   （処理概要）ログ出力タイプがデバッグのものはデバッグモード=trneの場合のみ出力する
        '   ●入力パラメタ：iLogType ログ出力タイプ(LOG_DEBUG/LOG_INFO/LOG_ERR)
        '                 ：他 mybase.writeline参照
        '   ●メソッド戻り値 ：なし
        '   ●発生例外       ：Exception
        '                                               2006.04.18 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 拡張ログ出力 ログ出力タイプがデバッグのものはデバッグモード=trneの場合のみ出力する
        ''' </summary>
        ''' <param name="prmLogType">ログ出力タイプ(LOG_DEBUG/LOG_INFO/LOG_ERR)</param>
        ''' <param name="prmOutPut">YYYY/MM/DD HH:MM:DD   エラーコード＆エラーメッセージ</param>
        ''' <param name="prmOutPut2">SQL文など追加メッセージ(指定時のみ出力)</param>
        ''' <remarks></remarks>
        Public Shadows Sub writeLine(ByVal prmLogType As Short, _
                                     ByVal prmOutPut As String, _
                                     Optional ByVal prmOutPut2 As String = "")

            If prmLogType = LOG_DEBUG And _debugFlg Then
                Call MyBase.writeLine("DEBUG   " & Space(1) & prmOutPut, prmOutPut2)
            ElseIf prmLogType = LOG_INFO Then
                Call MyBase.writeLine("INFO    " & Space(1) & prmOutPut, prmOutPut2)
            ElseIf prmLogType = LOG_ERR Then
                Call MyBase.writeLine("ERR*****" & Space(1) & prmOutPut, prmOutPut2)
            End If


        End Sub
    End Class
End Namespace