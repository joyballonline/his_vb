Imports System.IO

Namespace FileDirectory

    '===============================================================================
    '
    '  ユーティリティクラス
    '    （クラス名）    UtilDirectory
    '    （処理機能名）      ディレクトリ操作機能を提供する
    '    （本MDL使用前提）   特に無し
    '    （備考）            
    '
    '===============================================================================
    '  履歴  名前          日  付      マーク      内容
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2006/05/15              新規
    '-------------------------------------------------------------------------------
    Public Class UtilDirectory

        '===============================================================================
        'メンバー定数定義
        '===============================================================================
        'なし

        '===============================================================================
        'メンバー変数定義
        '===============================================================================
        'なし

        '===============================================================================
        'プロパティ(アクセサ)
        '===============================================================================
        'なし

        '===============================================================================
        ' コンストラクタ
        '===============================================================================
        'なし


        '-------------------------------------------------------------------------------
        '   ディレクトリの存在チェック
        '   （処理概要）ディレクトリが存在するかどうかのチェックを行う
        '   ●入力パラメタ   ：prmDir  ディレクトリ名
        '   ●メソッド戻り値 ：存在/非存在
        '                                               2006.05.15 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' ディレクトリの存在チェック
        ''' </summary>
        ''' <param name="prmDir">ディレクトリ名</param>
        ''' <returns>存在/非存在</returns>
        ''' <remarks></remarks>
        Public Function isDirExists(ByVal prmDir As String) As Boolean
            Return Directory.Exists(prmDir)
        End Function

        '-------------------------------------------------------------------------------
        '   ディレクトリ削除
        '   （処理概要）指定したディレクトリを削除する
        '   ●入力パラメタ   ：prmDir  ディレクトリ名
        '   ●メソッド戻り値 ：なし
        '                                               2006.05.15 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 指定したディレクトリを削除する
        ''' </summary>
        ''' <param name="prmDir">ディレクトリ名</param>
        ''' <remarks></remarks>
        Public Sub delete(ByVal prmDir As String)
            Directory.Delete(prmDir, True)
        End Sub

        '-------------------------------------------------------------------------------
        '   ディレクトリ作成
        '   （処理概要）指定したディレクトリを作成する
        '   ●入力パラメタ   ：prmDir  ディレクトリ名
        '   ●メソッド戻り値 ：なし
        '                                               2006.05.15 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 指定したディレクトリを作成する
        ''' </summary>
        ''' <param name="prmDir">ディレクトリ名</param>
        ''' <remarks></remarks>
        Public Sub create(ByVal prmDir As String)
            Directory.CreateDirectory(prmDir)
        End Sub

        '-------------------------------------------------------------------------------
        '   ファイル名取得
        '   （処理概要）指定したディレクトリ配下のファイル名(複数)を取得
        '   ●入力パラメタ   ：prmDir  ディレクトリ名
        '   ●メソッド戻り値 ：取得ファイル名(配列)
        '                                               2006.05.15 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 指定したディレクトリのファイル名(複数)を取得
        ''' </summary>
        ''' <param name="prmDir">ディレクトリ名</param>
        ''' <returns>取得ファイル名(配列)</returns>
        ''' <remarks></remarks>
        Public Function getFiles(ByVal prmDir As String) As String()
            Dim strFiles() As String = Directory.GetFiles(prmDir)
            Return strFiles
        End Function

        '-------------------------------------------------------------------------------
        '   フォルダ名取得
        '   （処理概要）指定したディレクトリ配下のフォルダ名(複数)を取得
        '   ●入力パラメタ   ：prmDir  ディレクトリ名
        '   ●メソッド戻り値 ：取得フォルダ名(配列)
        '                                               2006.05.15 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 指定したディレクトリのフォルダ名(複数)を取得
        ''' </summary>
        ''' <param name="prmDir">ディレクトリ名</param>
        ''' <returns>取得フォルダ名(配列)</returns>
        ''' <remarks></remarks>
        Public Function getDirectories(ByVal prmDir As String) As String()
            Dim strDirectories() As String = Directory.GetDirectories(prmDir)
            Return strDirectories
        End Function

        '-------------------------------------------------------------------------------
        '   サブディレクトリ取得
        '   （処理概要）指定したディレクトリ配下に存在するサブディレクトリを全て取得する
        '   ●入力パラメタ   ：prmDir  ディレクトリ名
        '   ●メソッド戻り値 ：取得ディレクトリ名(配列)
        '                                               2006.05.15 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 指定したディレクトリ配下に存在するサブディレクトリを全て取得する
        ''' </summary>
        ''' <param name="prmDir">ディレクトリ名</param>
        ''' <returns>取得ディレクトリ名(配列)</returns>
        ''' <remarks></remarks>
        Public Function getSugDirectories(ByVal prmDir As String) As String()
            Dim ret() As String
            Dim subDirsAry As New ArrayList

            Me.getSubDirectories(prmDir, subDirsAry)

            Erase ret
            If subDirsAry.Count > 0 Then
                ReDim ret(subDirsAry.Count - 1)
                For i As Short = 0 To subDirsAry.Count - 1
                    ret(i) = subDirsAry(i).ToString
                Next
            End If
            Return ret

        End Function
        '※内部メソッド
        Private Sub getSubDirectories(ByVal prmSearchDir As String, ByRef prmFindDirs As ArrayList)
            'サブフォルダを取得
            Dim dir As String
            For Each dir In Directory.GetDirectories(prmSearchDir)
                prmFindDirs.Add(dir)                    'ArrayListにフォルダ追加
                Me.getSubDirectories(dir, prmFindDirs)  '再帰呼び出し
            Next
        End Sub

    End Class
End Namespace
