Imports System.IO

Namespace FileDirectory
    '===============================================================================
    '
    '  ユーティリティクラス
    '    （クラス名）    UtilFile
    '    （処理機能名）      ファイル操作機能を提供する
    '    （本MDL使用前提）   特に無し
    '    （備考）            
    '
    '===============================================================================
    '  履歴  名前          日  付      マーク      内容
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2006/05/15              新規
    '  (2)   Laevigata, Inc. 2010/03/31              追加　getWriteTimeStamp
    '-------------------------------------------------------------------------------
    Public Class UtilFile

        '===============================================================================
        'メンバー定数定義
        '===============================================================================
        'ファイルサイズ列挙型
        Public Enum sizeTypeEnum
            B = 1
            KB = 2
            MB = 3
            GB = 4
            TB = 5
        End Enum

        '===============================================================================
        'メンバー変数定義
        '===============================================================================
        Private _sizeType As sizeTypeEnum

        '===============================================================================
        'プロパティ(アクセサ)
        '===============================================================================
        Public Property sizeType() As sizeTypeEnum
            Get
                Return _sizeType
            End Get
            Set(ByVal value As sizeTypeEnum)
                _sizeType = value
            End Set
        End Property

        '===============================================================================
        ' コンストラクタ
        '===============================================================================
        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            _sizeType = UtilFile.sizeTypeEnum.KB    'デフォルト設定
        End Sub

        '-------------------------------------------------------------------------------
        '   ファイルの存在チェック
        '   （処理概要）ファイルが存在するかどうかのチェックを行う
        '   ●入力パラメタ   ：prmFile  ファイル名
        '   ●メソッド戻り値 ：存在/非存在
        '                                               2006.05.15 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' ファイルが存在するかどうかのチェックを行う
        ''' </summary>
        ''' <param name="prmFile">ファイル名</param>
        ''' <returns>存在/非存在</returns>
        ''' <remarks></remarks>
        Public Function isFileExists(ByVal prmFile As String) As Boolean
            Return File.Exists(prmFile)
        End Function

        '-------------------------------------------------------------------------------
        '   ファイル削除
        '   （処理概要）指定したファイルを削除する
        '   ●入力パラメタ   ：prmFile  ファイル名
        '   ●メソッド戻り値 ：なし
        '                                               2006.05.15 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 指定したファイルを削除する
        ''' </summary>
        ''' <param name="prmFile">ファイル名</param>
        ''' <remarks></remarks>
        Public Sub delete(ByVal prmFile As String)
            File.Delete(prmFile)
        End Sub

        '-------------------------------------------------------------------------------
        '   ファイルコピー
        '   （処理概要）指定したファイルをコピーする
        '   ●入力パラメタ   ：prmSourceFile    コピー元ファイル名
        '                    ：prmDestFile      コピー先ファイル名
        '                    ：prmOverWrite     上書き確認
        '   ●メソッド戻り値 ：なし
        '                                               2006.05.15 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 指定したファイルをコピーする
        ''' </summary>
        ''' <param name="prmSourceFile">コピー元ファイル名</param>
        ''' <param name="prmDestFile">コピー先ファイル名</param>
        ''' <param name="prmOverWrite">上書き確認</param>
        ''' <remarks></remarks>
        Public Sub copy(ByVal prmSourceFile As String, ByVal prmDestFile As String, Optional ByVal prmOverWrite As Boolean = False)
            File.Copy(prmSourceFile, prmDestFile, prmOverWrite)
        End Sub

        '-------------------------------------------------------------------------------
        '   ファイル移動
        '   （処理概要）指定したファイルを移動する
        '   ●入力パラメタ   ：prmSourceFile    移動元ファイル名
        '                    ：prmDestFile      移動先ファイル名
        '   ●メソッド戻り値 ：なし
        '   ●備考           ：同じディレクトリを指定した場合はReNameとなる
        '                                               2006.05.15 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 指定したファイルを移動する ※同じディレクトリを指定した場合、ReNameとなる
        ''' </summary>
        ''' <param name="prmSourceFile">移動元ファイル名</param>
        ''' <param name="prmDestFile">移動先ファイル名</param>
        ''' <remarks></remarks>
        Public Sub move(ByVal prmSourceFile As String, ByVal prmDestFile As String)
            File.Move(prmSourceFile, prmDestFile)
        End Sub

        '-------------------------------------------------------------------------------
        '   タイムスタンプ取得
        '   （処理概要）指定したファイルのタイムスタンプを取得する
        '   ●入力パラメタ   ：prmFile    ファイル名
        '   ●メソッド戻り値 ：タイムスタンプ
        '                                               2006.05.15 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 指定したファイルのタイムスタンプを取得する
        ''' </summary>
        ''' <param name="prmFile">ファイル名</param>
        ''' <returns>タイムスタンプ</returns>
        ''' <remarks></remarks>
        Public Function getTimeStamp(ByVal prmFile As String) As Date
            Return File.GetCreationTime(prmFile)
        End Function

        '-------------------------------------------------------------------------------
        '   ファイルサイズを取得
        '   （処理概要）指定したファイルのファイルサイズを取得する
        '   ●入力パラメタ   ：prmFile    ファイル名
        '   ●メソッド戻り値 ：ファイルサイズ
        '                                               2006.05.15 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 指定したファイルのファイルサイズを取得する
        ''' </summary>
        ''' <param name="prmFile">ファイル名</param>
        ''' <returns>ファイル削除</returns>
        ''' <remarks></remarks>
        Public Function getSize(ByVal prmFile As String) As Integer
            Dim f As FileInfo = New FileInfo(prmFile)
            Dim ret As Integer
            Select Case _sizeType
                Case UtilFile.sizeTypeEnum.B
                    ret = CInt(f.Length)
                Case UtilFile.sizeTypeEnum.KB
                    ret = CInt(f.Length / 1024)
                Case UtilFile.sizeTypeEnum.MB
                    ret = CInt((f.Length / 1024) / 1024)
                Case UtilFile.sizeTypeEnum.GB
                    ret = CInt(((f.Length / 1024) / 1024) / 1024)
                Case UtilFile.sizeTypeEnum.TB
                    ret = CInt((((f.Length / 1024) / 1024) / 1024) / 1024)
            End Select
            Return ret
        End Function

        '-------------------------------------------------------------------------------
        '   ディレクトリ/ファイル名分割
        '   （処理概要）指定されたフルパスをディレクトリとファイル名に分割する
        '   ●入力パラメタ   ：prmFullPath  対象フルパス
        '                    ：prmRefPath   取得パス
        '                    ：prmRefFile   取得ファイル名
        '   ●メソッド戻り値 ：ファイルサイズ
        '                                               2006.05.15 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 指定されたフルパスをディレクトリとファイル名に分割する
        ''' </summary>
        ''' <param name="prmFullPath">対象フルパス</param>
        ''' <param name="prmRefPath">取得パス</param>
        ''' <param name="prmRefFile">取得ファイル名</param>
        ''' <remarks></remarks>
        Public Sub dividePathAndFile(ByVal prmFullPath As String, ByRef prmRefPath As String, ByRef prmRefFile As String)
            Dim devPos As Integer
            devPos = InStrRev(prmFullPath.Replace("/", "\"), "\")

            If devPos <= 0 Then
                prmRefFile = prmFullPath
            Else
                prmRefFile = prmFullPath.Substring(devPos)
                prmRefPath = prmFullPath.Substring(0, devPos - 1)
            End If
        End Sub

        '-------------------------------------------------------------------------------
        '   ファイル更新日取得
        '   （処理概要）指定したファイルの更新日を取得する
        '   ●入力パラメタ   ：prmFile    ファイル名
        '   ●メソッド戻り値 ：更新日
        '                                               2010.03.31 Created By sugano
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 指定したファイルの更新日を取得する
        ''' </summary>
        ''' <param name="prmFile">ファイル名</param>
        ''' <returns>更新日</returns>
        ''' <remarks></remarks>
        Public Function getWriteTimeStamp(ByVal prmFile As String) As Date
            Return File.GetLastWriteTime(prmFile)
        End Function
    End Class
End Namespace
