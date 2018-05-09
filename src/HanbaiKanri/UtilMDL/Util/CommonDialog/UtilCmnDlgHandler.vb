Namespace CommonDialog
    '===============================================================================
    '
    ' ユーティリティモジュール
    '   （モジュール名）   UtilCmnDlgHandler
    '   （処理機能名）     コモンダイアログ操作機能を提供する
    '   （本MDL使用前提）  特に無し
    '   （備考）           
    '
    '===============================================================================
    ' 履歴  名前          日  付      マーク      内容
    '-------------------------------------------------------------------------------
    ' (1)   高木   　     2006/05/11              新規
    '-------------------------------------------------------------------------------
    Public Class UtilCmnDlgHandler

        '===============================================================================
        '変数定義
        '===============================================================================

        '===============================================================================
        '定数定義
        '===============================================================================
        'コモンダイアログフィルター定数
        Public Const TXT As String = "テキストファイル(*.txt)" & "|" & "*.txt"  'テキストファイル
        Public Const CSV As String = "CSVファイル(*.csv)" & "|" & "*.csv"       'ＣＳＶファイル
        Public Const DAT As String = "データファイル(*.dat)" & "|" & "*.dat"    'データファイル
        Public Const ALL As String = "全てのファイル(*.*)" & "|" & "*.*"        '全てのファイル

        'コモンダイアログ初期表示ディレクトリ
        Private Const sDEFALT_DIR As String = "C:\"

        '-------------------------------------------------------------------------------
        '　 「ファイルを選択」コモンダイアログ表示
        '  （処理概要）「ファイルを選択」コモンダイアログを表示し、選択されたファイルのフルパスを返す
        '  ●入力パラメタ：i <prmDir>       ダイアログ初期表示ディレクトリ
        '                ：i <prmFileNm>    ダイアログ初期表示ファイル名
        '                ：i <prmFillter>   ダイアログ表示フィルター
        '                                      TXT/CSV/DAT/ALL                       
        '                ：i <prmTitle>     タイトル
        '                ：i <prmMultiSel>  複数選択可否
        '  ●関数戻り値　：選択されたファイルのフルパス(複数選択ありの場合は「,」で連結して返却/キャンセル時は""を返却)
        '  ●その他：呼出例 Call UtilCmnDlgHandler.openFileDialog("d:\work", "tmp.txt", UtilCmnDlgHandler.TXT & "|" & UtilCmnDlgHandler.ALL)
        '            prmDir優先度
        '                      prmDir > カレントDir > "C:\"
        '                                              2006.05.11 Createed By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 「ファイルを選択」コモンダイアログ表示
        ''' </summary>
        ''' <param name="prmDir">ダイアログ初期表示ディレクトリ</param>
        ''' <param name="prmFileNm">ダイアログ初期表示ファイル名</param>
        ''' <param name="prmFillter">ダイアログ表示フィルター TXT/CSV/DAT/ALL </param>
        ''' <param name="prmTitle">タイトル</param>
        ''' <param name="prmMultiSel">複数選択可否</param>
        ''' <returns>選択されたファイルのフルパス(複数選択ありの場合は「,」で連結して返却/キャンセル時は""を返却)</returns>
        ''' <remarks></remarks>
        Public Shared Function openFileDialog(Optional ByVal prmDir As String = "", _
                                          Optional ByVal prmFileNm As String = "", _
                                          Optional ByVal prmFillter As String = ALL, _
                                          Optional ByVal prmTitle As String = "ファイルを選択", _
                                          Optional ByVal prmMultiSel As Boolean = False) As String

            Try
                '初期表示のパス取得
                Dim initialDir As String       '初期表示するディレクトリ
                If prmDir.Equals("") Then
                    initialDir = sDEFALT_DIR       '初期値
                Else
                    initialDir = prmDir                '引数値
                End If

                'OpenFileDialog の新しいインスタンスを生成する 
                Dim fd As OpenFileDialog = New OpenFileDialog()
                Try
                    With fd
                        .Title = prmTitle               'ダイアログのタイトルを設定する
                        .InitialDirectory = initialDir  '初期表示するディレクトリを設定する
                        .FileName = prmFileNm           '初期表示するファイル名を設定する
                        .Filter = prmFillter            'ファイルのフィルタを設定する
                        .RestoreDirectory = True        'ダイアログボックスを閉じる前にカレントディレクトリを復元
                        .Multiselect = prmMultiSel      '複数のファイルを選択可能にする
                        .CheckFileExists = True         'ファイルの存在チェックを行う
                        If .ShowDialog() = DialogResult.OK Then
                            If Not prmMultiSel Then     '[OK]押下時はファイル名を設定
                                Return .FileName
                            Else
                                '複数選択ありの場合は「,」編集
                                Dim ret As String = ""
                                For Each name As String In fd.FileNames
                                    If ret.Equals("") Then
                                        ret = name
                                    Else
                                        ret = ret & "," & name
                                    End If
                                Next
                                Return ret
                            End If
                        Else
                            Return ""                   '[キャンセル]押下時は空文字を設定
                        End If
                    End With
                Catch le As Exception
                    Throw le
                Finally
                    fd.Dispose()                        'ダイアログの破棄
                End Try
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        '-------------------------------------------------------------------------------
        '　 「名前をつけて保存」コモンダイアログ表示
        '  （処理概要）「名前をつけて保存」コモンダイアログを表示し、選択されたファイルのフルパスを返す
        '  ●入力パラメタ：i <prmDir>       ダイアログ初期表示ディレクトリ
        '                ：i <prmFileNm>    ダイアログ初期表示ファイル名
        '                ：i <prmFillter>   ダイアログ表示フィルター
        '                                      TXT/CSV/DAT/ALL                       
        '                ：i <prmTitle>     タイトル
        '  ●関数戻り値　：選択されたファイルのフルパス(キャンセル時は""を返却)
        '  ●その他：呼出例 Call UtilCmnDlgHandler.saveFileDialog("d:\work", "tmp.txt", UtilCmnDlgHandler.TXT & "|" & UtilCmnDlgHandler.ALL)
        '            prmDir優先度
        '                      prmDir > カレントDir > "C:\"
        '                                              2006.05.11 Createed By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' 「名前をつけて保存」コモンダイアログ表示
        ''' </summary>
        ''' <param name="prmDir">ダイアログ初期表示ディレクトリ</param>
        ''' <param name="prmFileNm">ダイアログ初期表示ファイル名</param>
        ''' <param name="prmFillter">ダイアログ表示フィルター TXT/CSV/DAT/ALL</param>
        ''' <param name="prmTitle">タイトル</param>
        ''' <returns>選択されたファイルのフルパス(キャンセル時は""を返却)</returns>
        ''' <remarks></remarks>
        Public Shared Function saveFileDialog(Optional ByVal prmDir As String = "", _
                                          Optional ByVal prmFileNm As String = "", _
                                          Optional ByVal prmFillter As String = ALL, _
                                          Optional ByVal prmTitle As String = "名前をつけて保存") As String

            Try
                '初期表示のパス取得
                Dim initialDir As String       '初期表示するディレクトリ
                If prmDir.Equals("") Then
                    initialDir = sDEFALT_DIR       '初期値
                Else
                    initialDir = prmDir                '引数値
                End If

                'SaveFileDialogの新しいインスタンスを生成する 
                Dim fd As SaveFileDialog = New SaveFileDialog()
                Try
                    With fd
                        .Title = prmTitle               'ダイアログのタイトルを設定する
                        .InitialDirectory = initialDir  '初期表示するディレクトリを設定する
                        .FileName = prmFileNm           '初期表示するファイル名を設定する
                        .Filter = prmFillter            'ファイルのフィルタを設定する
                        .RestoreDirectory = True        'ダイアログボックスを閉じる前にカレントディレクトリを復元
                        .CheckFileExists = False        'ファイルの存在チェックを行わない
                        If .ShowDialog() = DialogResult.OK Then
                            Return .FileName            '[OK]押下時はファイル名を設定
                        Else
                            Return ""                   '[キャンセル]押下時は空文字を設定
                        End If
                    End With
                Catch le As Exception
                    Throw le
                Finally
                    fd.Dispose()                        'ダイアログの破棄
                End Try
            Catch ex As Exception
                Throw ex
            End Try
        End Function

    End Class
End Namespace