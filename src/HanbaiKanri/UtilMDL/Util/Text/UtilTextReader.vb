Imports System.IO
Imports System.Text


Namespace Text

    '===============================================================================
    '
    '  ユーティリティクラス
    '    （クラス名）    UtilTextReader
    '    （処理機能名）      テキストファイルを読み込む機能を提供
    '    （本MDL使用前提）   特に無し
    '    （備考）            
    '
    '===============================================================================
    '  履歴  名前          日  付      マーク      内容
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2006/05/14              新規
    '-------------------------------------------------------------------------------
    Public Class UtilTextReader

        '===============================================================================
        'メンバー変数定義
        '===============================================================================
        Private _fileName As String         'ファイル名
        Private _sReader As StreamReader    'ストリームリーダー
        Private _openFlg As Boolean = False

        '===============================================================================
        'プロパティ(アクセサ)
        '===============================================================================
        Public ReadOnly Property EOF() As Boolean
            Get
                Dim ret As Boolean
                If _sReader.Peek = -1 Then
                    ret = True
                Else
                    ret = False
                End If
                Return ret
            End Get
        End Property
        Public ReadOnly Property isFileOpen() As Boolean
            Get
                Return _openFlg
            End Get
        End Property

        '===============================================================================
        ' コンストラクタ
        '   ●入力パラメタ   ：  prmFileName    操作対象ファイル名
        '===============================================================================
        ''' <summary> 
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="prmFileName">ファイル名</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal prmFileName As String)
            _fileName = prmFileName
        End Sub

        '===============================================================================
        ' デストラクタ
        '   ●入力パラメタ   ：  なし
        '===============================================================================
        ''' <summary>
        ''' デストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Protected Overrides Sub Finalize()

            Try
                If _sReader IsNot Nothing And _openFlg Then
                    _sReader.Close()
                End If
            Catch lex As Exception
            Finally
                If _sReader IsNot Nothing Then
                    Call _sReader.Dispose()
                End If
                _sReader = Nothing
                MyBase.Finalize()
            End Try
        End Sub


        '-------------------------------------------------------------------------------
        '   ファイルオープン
        '   （処理概要）対象ファイルを開く
        '   ●入力パラメタ   ：なし
        '   ●メソッド戻り値 ：なし
        '   ●備考           ：openメソッドの呼出し後は必ずcloseメソッドの呼び出しを保障すること
        '                                               2006.05.14 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' ファイルオープン　オープンしたら必ずcloseメソッドの呼出を保障すること
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub open()
            If _openFlg Then
                Throw New UsrDefException("ファイルは既に開いています。")
            End If
            _sReader = New StreamReader(_fileName, Encoding.Default)
            _openFlg = True
        End Sub

        '-------------------------------------------------------------------------------
        '   ファイルクローズ
        '   （処理概要）対象ファイルを閉じる
        '   ●入力パラメタ   ：なし
        '   ●メソッド戻り値 ：なし
        '   ●備考           ：openメソッドの呼出し後は必ずcloseメソッドの呼び出しを保障すること
        '                                               2006.05.14 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' ファイルクローズ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub close()
            If Not _openFlg Then
                Throw New UsrDefException("ファイルが閉じています。")
            End If
            _sReader.Close()
            _openFlg = False
        End Sub

        '-------------------------------------------------------------------------------
        '   リードライン
        '   （処理概要）カレント行の文字列を読み込む
        '   ●入力パラメタ   ：なし
        '   ●メソッド戻り値 ：読み込み文字列
        '                                               2006.05.14 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        'リードライン
        ''' <summary>
        ''' リードライン
        ''' </summary>
        ''' <returns>読み込み文字列</returns>
        ''' <remarks></remarks>
        Public Function readLine() As String
            If Not _openFlg Then
                Throw New UsrDefException("ファイルが閉じています。")
            End If
            Dim retLine As String = _sReader.ReadLine
            If retLine IsNot Nothing Then
                Return retLine
            Else
                Return ""
            End If
        End Function

        '-------------------------------------------------------------------------------
        '   全読み込み
        '   （処理概要）カレント行以降の全文字列を読み込む
        '   ●入力パラメタ   ：なし
        '   ●メソッド戻り値 ：読み込み文字列
        '                                               2006.05.14 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 全読み込み(カレント行以降)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function readToEnd() As String
            If Not _openFlg Then
                Throw New UsrDefException("ファイルが閉じています。")
            End If
            Return _sReader.ReadToEnd
        End Function

    End Class
End Namespace
