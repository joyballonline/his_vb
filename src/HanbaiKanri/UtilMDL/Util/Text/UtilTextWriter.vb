Imports System.IO
Imports System.Text


Namespace Text
    '===============================================================================
    '
    '  ユーティリティクラス
    '    （クラス名）    UtilTextWriter
    '    （処理機能名）      テキストファイルを書き込む機能を提供
    '    （本MDL使用前提）   特に無し
    '    （備考）            
    '
    '===============================================================================
    '  履歴  名前          日  付      マーク      内容
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2006/05/14              新規
    '-------------------------------------------------------------------------------
    Public Class UtilTextWriter

        '===============================================================================
        'メンバー変数定義
        '===============================================================================
        Private _fileName As String         'ファイル名
        Private _sWriter As StreamWriter    'ストリームライター
        Private _openFlg As Boolean = False


        '===============================================================================
        'プロパティ(アクセサ)
        '===============================================================================
        Public ReadOnly Property isFileOpen() As Boolean
            Get
                Return _openFlg
            End Get
        End Property

        '===============================================================================
        ' コンストラクタ
        '   ●入力パラメタ   ：prmFileName    操作対象ファイル名
        '   ●備考           ：ファイルが存在しない場合はオープンしたタイミングでファイルを作成する。
        '===============================================================================
        ''' <summary>
        ''' コンストラクタ ファイルが存在しない場合はオープンしたタイミングでファイルを作成する。
        ''' </summary>
        ''' <param name="prmFileName"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal prmFileName As String)
            _fileName = prmFileName
        End Sub

        '===============================================================================
        ' デストラクタ
        '   ●入力パラメタ   ：なし
        '===============================================================================
        ''' <summary>
        ''' デストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Protected Overrides Sub Finalize()
            Try
                If _sWriter IsNot Nothing And _openFlg Then
                    _sWriter.Close()
                End If
            Catch lex As Exception
            Finally
                If _sWriter IsNot Nothing Then
                    Call _sWriter.Dispose()
                End If
                _sWriter = Nothing
                Call MyBase.Finalize()
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   ファイルオープン
        '   （処理概要）対象ファイルを開く
        '   ●入力パラメタ   ：prmAppendFlg   追加書き込みするかどうかのフラグ(Falseの場合、上書きする)
        '   ●メソッド戻り値 ：なし
        '   ●備考           ：openメソッドの呼出し後は必ずcloseメソッドの呼び出しを保障すること
        '                                               2006.05.14 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' オープン オープンしたら必ずcloseメソッドの呼出を保障すること
        ''' </summary>
        ''' <param name="prmAppendFlg">追加書き込みするかどうかのフラグ(Falseの場合、上書きする)</param>
        ''' <remarks></remarks>
        Public Sub open(Optional ByVal prmAppendFlg As Boolean = True)
            If _openFlg Then
                Throw New UsrDefException("ファイルは既に開いています。")
            End If
            _sWriter = New StreamWriter(_fileName, prmAppendFlg, Encoding.Default)
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
        ''' クローズ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub close()
            If Not _openFlg Then
                Throw New UsrDefException("ファイルが閉じています。")
            End If
            _sWriter.Close()
            _openFlg = False
        End Sub

        '-------------------------------------------------------------------------------
        '   書き込み
        '   （処理概要）指定された文字列を書き込む
        '   ●入力パラメタ   ：prmStr   書き込み文字列
        '   ●メソッド戻り値 ：なし
        '                                               2006.05.14 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 指定された文字列を書き込む
        ''' </summary>
        ''' <param name="prmStr">書き込み文字列</param>
        ''' <remarks></remarks>
        Public Sub write(ByVal prmStr As String)
            If Not _openFlg Then
                Throw New UsrDefException("ファイルが閉じています。")
            End If
            _sWriter.Write(prmStr)
        End Sub
        'Object型をオーバーロード 2010.11.14 Laevigata, Inc.
        Public Sub write(ByVal prmStr As Object)
            If Not _openFlg Then
                Throw New UsrDefException("ファイルが閉じています。")
            End If
            _sWriter.Write(prmStr)
        End Sub

        '-------------------------------------------------------------------------------
        '   書き込み
        '   （処理概要）指定された文字列を書き込み、最後に改行を出力する
        '   ●入力パラメタ   ：prmStr   書き込み文字列
        '   ●メソッド戻り値 ：なし
        '                                               2006.05.14 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 指定された文字列を書き込み、最後に改行を出力する
        ''' </summary>
        ''' <param name="prmStr">書き込み文字列</param>
        ''' <remarks></remarks>
        Public Sub writeLine(ByVal prmStr As String)
            If Not _openFlg Then
                Throw New UsrDefException("ファイルが閉じています。")
            End If
            _sWriter.WriteLine(prmStr)
        End Sub

        '-------------------------------------------------------------------------------
        '   改行
        '   （処理概要）改行を出力する
        '   ●入力パラメタ   ：なし
        '   ●メソッド戻り値 ：なし
        '                                               2006.05.14 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 改行
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub newLine()
            If Not _openFlg Then
                Throw New UsrDefException("ファイルが閉じています。")
            End If
            _sWriter.WriteLine()
        End Sub

    End Class
End Namespace
