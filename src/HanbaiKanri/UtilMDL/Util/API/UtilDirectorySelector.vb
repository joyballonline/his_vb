Imports System.Text
Namespace API
    ''' <summary>
    ''' ユーティリティモジュール
    ''' </summary>
    ''' <remarks></remarks>
    Public Class UtilDirectorySelector
        '===============================================================================
        '
        '  ユーティリティモジュール
        '    （モジュール名）   UtilDirectorySelector
        '    （処理機能名）     フォルダ選択ダイアログに関する機能を提供する
        '    （本MDL使用前提）  特に無し
        '    （備考）           以下API使用
        '                           shell32.SHBrowseForFolder
        '                           shell32.SHGetPathFromIDList
        '                           shell32.#195
        '                           user32.SendMessageA
        '                           user32.GetDesktopWindow
        '
        '
        '===============================================================================
        '  履歴  名前          日  付      マーク      内容
        '-------------------------------------------------------------------------------
        '  (1)   Laevigata, Inc.    2006/05/11              新規
        '-------------------------------------------------------------------------------

        '===============================================================================
        'API定義
        '===============================================================================
        Private Declare Function SHBrowseForFolder Lib "shell32" (ByRef lpbi As BROWSEINFO) As Integer
        Private Declare Function SHGetPathFromIDList Lib "shell32" _
                (ByVal pidl As Integer, ByVal pszPath As String) As Integer
        Private Declare Function SendMessageStr Lib "user32" Alias "SendMessageA" _
                (ByVal hWnd As Integer, ByVal wMsg As Integer,
                 ByVal wParam As Integer, ByVal lParam As String) As Integer
        Private Declare Function SHFree Lib "shell32" Alias "#195" (ByVal pidl As Integer) As Integer
        Private Declare Function GetDesktopWindow Lib "user32" () As Integer
        'SHBrowseForFolder API のコールバック関数用のDelegate
        Private Delegate Function CallbackDelegate(ByVal lngHWnd As Integer, ByVal lngUMsg As Integer,
                                    ByVal lngLParam As Integer, ByVal lngLpData As String) As Integer

        '===============================================================================
        '定数定義
        '===============================================================================
        Private Const MAX_PATH As Integer = 260
        Private Const BFFM_SETSTATUSTEXTA As Integer = &H464&   ' ステータステキスト
        Private Const BFFM_ENABLEOK As Integer = &H465&         ' OK ボタンの使用可否
        Private Const BFFM_SETSELECTIONA As Integer = &H466&    ' アイテムを選択
        Private Const BFFM_INITIALIZED As Integer = &H1&
        Private Const BFFM_SELCHANGED As Integer = &H2&

        '===============================================================================
        'ユーザー定義型定義
        '===============================================================================
        Private Structure RECT
            Public left As Integer    'WindowのX座標
            Public top As Integer     'WindowのY座標
            Public right As Integer   'Windowの右端の座標
            Public bottom As Integer  'Windowの底にあたる部分の座標
        End Structure
        Private Structure BROWSEINFO
            Public hWndOwner As Integer         'ダイアログの親ウィンドウのハンドル
            Public pidlRoot As Integer          'ディレクトリツリーのルート
            Public pszDisplayName As String     'MAX_PATH
            Public lpszTitle As String          'ダイアログの説明文
            Public ulFlags As Integer           'FLG_FOLDER
            Public lpfn As CallbackDelegate              'コールバック関数へのポインタ
            Public lParam As String             'コールバック関数へのパラメータ
            Public iImage As Integer            'フォルダーアイコンのシステムイメージリスト
        End Structure

        '===============================================================================
        '列挙型定義
        '===============================================================================
        Public Enum ROOT
            DESKTOP = &H0&                        ' デスクトップ
            INTERNET = &H1&                       ' インターネット
            PROGRAMS = &H2&                       ' Program Files
            CONTROLS = &H3&                       ' コントロールパネル
            PRINTERS = &H4&                       ' プリンタ
            PERSONAL = &H5&                       ' ドキュメントフォルダー
            FAVORITES = &H6&                      ' お気に入り
            STARTUP = &H7&                        ' スタートアップ
            RECENT = &H8&                         ' 最近使ったファイル
            SENDTO = &H9&                         ' 送る
            BITBUCKET = &HA&                      ' ごみ箱
            STARTMENU = &HB&                      ' スタートメニュー
            DESKTOPDIRECTORY = &H10&              ' デスクトップフォルダー
            DRIVES = &H11&                        ' マイコンピュータ
            NETWORK = &H12&                       ' ネットワーク(ネットワーク全体あり)
            NETHOOD = &H13&                       ' NETHOOD フォルダー
            FONTS = &H14&                         ' フォント
            TEMPLATES = &H15&                     ' テンプレート
            COMMON_STARTMENU = &H16&              '
            COMMON_PROGRAMS = &H17&               '
            COMMON_STARTUP = &H18&                '
            COMMON_DESKTOPDIRECTORY = &H19&       '
            APPDATA = &H1A&                       '
            PRINTHOOD = &H1B&                     '
            ALTSTARTUP = &H1D&                    '
            COMMON_ALTSTARTUP = &H1E&             '
            COMMON_FAVORITES = &H1F&              '
            INTERNET_CACHE = &H20&                '
            COOKIES = &H21&                       '
            HISTORY = &H22&                       '
        End Enum
        Public Enum FLG_FOLDER
            RETURNONLYFSDIRS = &H1&          ' フォルダのみ
            DONTGOBELOWDOMAIN = &H2&         ' ネットワークコンピューターを非表示
            STATUSTEXT = &H4&                ' ステータス表示
            RETURNFSANCESTORS = &H8&
            BROWSEFORCOMPUTER = &H1000&      ' ネットワークコンピューターのみ
            BROWSEFORPRINTER = &H2000&       ' プリンターのみ
            BROWSEINCLUDEFILES = &H4000&     ' 全て選択可能
        End Enum

        '-------------------------------------------------------------------------------
        '　 フォルダ選択ダイアログ表示
        '   （処理概要）フォルダ選択ダイアログを表示し、ユーザー入力値を返却する
        '   ●入力パラメタ：<prmDefaultDir> デフォルトフォルダ
        '                 ：<prmTitle>      ダイアログに表示する説明文
        '                 ：<prmRoot>       ルート位置(ROOTの定数/初期値=DESKTOP)
        '                 ：<prmFlg>        表示フォルダオプション(FLG_FOLDERの定数/初期値=RETURNONLYFSDIRS)
        '                 ：<prmWHwnd>      ダイアログのオーナーウィンドウハンドル
        '   ●関数戻り値　：正常終了時：選択フォルダフルパス / キャンセル時：""
        '   ●その他　　　：以下、Formにおける使用例
        '                        Dim RtnDir As String = UtilDirectorySelector.choiceFolder("C:\WINDOWS", "○○のフォルダを選択してください。")
        '                        MsgBox RtnDir
        '                                               2006.05.11 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------'
        ''' <summary>
        ''' フォルダ選択ダイアログ表示
        ''' </summary>
        ''' <param name="prmDefaultDir">デフォルトフォルダ</param>
        ''' <param name="prmTitle">ダイアログに表示する説明文</param>
        ''' <param name="prmRoot">ルート位置(ROOTの定数/初期値=DESKTOP)</param>
        ''' <param name="prmFlg">表示フォルダオプション(FLG_FOLDERの定数/初期値=RETURNONLYFSDIRS)</param>
        ''' <param name="prmWHwnd">ダイアログのオーナーウィンドウハンドル</param>
        ''' <returns>正常終了時：選択フォルダフルパス / キャンセル時：""</returns>
        ''' <remarks>以下、Formにおける使用例
        '''Dim RtnDir As String = UtilDirectorySelector.choiceFolder("C:\WINDOWS", "○○のフォルダを選択してください。")
        ''' MsgBox RtnDir</remarks>
        Public Shared Function choiceFolder(Optional ByRef prmDefaultDir As String = vbNullString, _
                                            Optional ByRef prmTitle As String = "フォルダを選択してください", _
                                            Optional ByVal prmRoot As ROOT = ROOT.DESKTOP, _
                                            Optional ByVal prmFlg As FLG_FOLDER = FLG_FOLDER.RETURNONLYFSDIRS, _
                                            Optional ByVal prmWHwnd As Integer = 0) As String

            Try
                Dim pidl As Integer
                If prmWHwnd = 0& Then
                    prmWHwnd = GetDesktopWindow()
                End If

                Dim path As String = ""
                For index As Short = 0 To MAX_PATH - 1
                    If path.Equals("") Then
                        path = vbNullChar
                    Else
                        path = path & vbNullChar
                    End If
                Next

                Dim biParam As BROWSEINFO = Nothing
                With biParam
                    .hWndOwner = prmWHwnd
                    .pidlRoot = prmRoot
                    .pszDisplayName = path
                    .lpszTitle = prmTitle & vbNullChar
                    .ulFlags = prmFlg
                    If Len(prmDefaultDir) > 0& Then
                        .lpfn = AddressOf BrowseCallbackProc
                        .lParam = prmDefaultDir & vbNullChar
                    End If
                End With

                pidl = SHBrowseForFolder(biParam)
                If biParam.ulFlags And FLG_FOLDER.BROWSEFORCOMPUTER Then
                    path = biParam.pszDisplayName
                    path = Left$(path, InStr(path, vbNullChar) - 1&)
                Else
                    If pidl = 0& Then
                        path = vbNullString
                    Else
                        If SHGetPathFromIDList(pidl, path) <> 0& Then
                            path = Left$(path, InStr(path, vbNullChar) - 1&)
                        Else
                            path = vbNullString
                        End If
                    End If
                End If

                Call SHFree(pidl)
                choiceFolder = path

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        'SHBrowseForFolder API のコールバック関数定義
        ''' <summary>
        ''' SHBrowseForFolder API のコールバック関数定義
        ''' </summary>
        ''' <param name="lngHWnd"></param>
        ''' <param name="lngUMsg"></param>
        ''' <param name="lngLParam"></param>
        ''' <param name="lngLpData"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function BrowseCallbackProc(ByVal lngHWnd As Integer, ByVal lngUMsg As Integer, _
                                    ByVal lngLParam As Integer, ByVal lngLpData As String) As Integer
            Select Case lngUMsg
                Case BFFM_INITIALIZED
                    Dim text As String = lngLpData
                    Dim source() As Byte                        '変換元のバイト配列
                    Dim encoded() As Byte                       '変換後のバイト配列
                    source = Encoding.Unicode.GetBytes(text)    '文字列をバイト配列に変換
                    encoded = Encoding.Convert(Encoding.Unicode, _
                                               Encoding.GetEncoding("shift_jis"), _
                                               source)          'コードページをUnicodeからシフトJISに変換
                    Call SendMessageStr(lngHWnd, _
                                        BFFM_SETSELECTIONA, _
                                        1&, _
                                        Encoding.GetEncoding("shift_jis").GetString(encoded))
                Case BFFM_SELCHANGED
                    'ITEMが選択された時に処理を行いたい場合ここに記述
            End Select
            BrowseCallbackProc = 0&
        End Function

    End Class
End Namespace