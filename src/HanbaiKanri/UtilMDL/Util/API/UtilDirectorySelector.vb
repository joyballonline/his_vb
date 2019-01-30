Imports System.Text
Namespace API
    ''' <summary>
    ''' ���[�e�B���e�B���W���[��
    ''' </summary>
    ''' <remarks></remarks>
    Public Class UtilDirectorySelector
        '===============================================================================
        '
        '  ���[�e�B���e�B���W���[��
        '    �i���W���[�����j   UtilDirectorySelector
        '    �i�����@�\���j     �t�H���_�I���_�C�A���O�Ɋւ���@�\��񋟂���
        '    �i�{MDL�g�p�O��j  ���ɖ���
        '    �i���l�j           �ȉ�API�g�p
        '                           shell32.SHBrowseForFolder
        '                           shell32.SHGetPathFromIDList
        '                           shell32.#195
        '                           user32.SendMessageA
        '                           user32.GetDesktopWindow
        '
        '
        '===============================================================================
        '  ����  ���O          ��  �t      �}�[�N      ���e
        '-------------------------------------------------------------------------------
        '  (1)   Laevigata, Inc.    2006/05/11              �V�K
        '-------------------------------------------------------------------------------

        '===============================================================================
        'API��`
        '===============================================================================
        Private Declare Function SHBrowseForFolder Lib "shell32" (ByRef lpbi As BROWSEINFO) As Integer
        Private Declare Function SHGetPathFromIDList Lib "shell32" _
                (ByVal pidl As Integer, ByVal pszPath As String) As Integer
        Private Declare Function SendMessageStr Lib "user32" Alias "SendMessageA" _
                (ByVal hWnd As Integer, ByVal wMsg As Integer,
                 ByVal wParam As Integer, ByVal lParam As String) As Integer
        Private Declare Function SHFree Lib "shell32" Alias "#195" (ByVal pidl As Integer) As Integer
        Private Declare Function GetDesktopWindow Lib "user32" () As Integer
        'SHBrowseForFolder API �̃R�[���o�b�N�֐��p��Delegate
        Private Delegate Function CallbackDelegate(ByVal lngHWnd As Integer, ByVal lngUMsg As Integer,
                                    ByVal lngLParam As Integer, ByVal lngLpData As String) As Integer

        '===============================================================================
        '�萔��`
        '===============================================================================
        Private Const MAX_PATH As Integer = 260
        Private Const BFFM_SETSTATUSTEXTA As Integer = &H464&   ' �X�e�[�^�X�e�L�X�g
        Private Const BFFM_ENABLEOK As Integer = &H465&         ' OK �{�^���̎g�p��
        Private Const BFFM_SETSELECTIONA As Integer = &H466&    ' �A�C�e����I��
        Private Const BFFM_INITIALIZED As Integer = &H1&
        Private Const BFFM_SELCHANGED As Integer = &H2&

        '===============================================================================
        '���[�U�[��`�^��`
        '===============================================================================
        Private Structure RECT
            Public left As Integer    'Window��X���W
            Public top As Integer     'Window��Y���W
            Public right As Integer   'Window�̉E�[�̍��W
            Public bottom As Integer  'Window�̒�ɂ����镔���̍��W
        End Structure
        Private Structure BROWSEINFO
            Public hWndOwner As Integer         '�_�C�A���O�̐e�E�B���h�E�̃n���h��
            Public pidlRoot As Integer          '�f�B���N�g���c���[�̃��[�g
            Public pszDisplayName As String     'MAX_PATH
            Public lpszTitle As String          '�_�C�A���O�̐�����
            Public ulFlags As Integer           'FLG_FOLDER
            Public lpfn As CallbackDelegate              '�R�[���o�b�N�֐��ւ̃|�C���^
            Public lParam As String             '�R�[���o�b�N�֐��ւ̃p�����[�^
            Public iImage As Integer            '�t�H���_�[�A�C�R���̃V�X�e���C���[�W���X�g
        End Structure

        '===============================================================================
        '�񋓌^��`
        '===============================================================================
        Public Enum ROOT
            DESKTOP = &H0&                        ' �f�X�N�g�b�v
            INTERNET = &H1&                       ' �C���^�[�l�b�g
            PROGRAMS = &H2&                       ' Program Files
            CONTROLS = &H3&                       ' �R���g���[���p�l��
            PRINTERS = &H4&                       ' �v�����^
            PERSONAL = &H5&                       ' �h�L�������g�t�H���_�[
            FAVORITES = &H6&                      ' ���C�ɓ���
            STARTUP = &H7&                        ' �X�^�[�g�A�b�v
            RECENT = &H8&                         ' �ŋߎg�����t�@�C��
            SENDTO = &H9&                         ' ����
            BITBUCKET = &HA&                      ' ���ݔ�
            STARTMENU = &HB&                      ' �X�^�[�g���j���[
            DESKTOPDIRECTORY = &H10&              ' �f�X�N�g�b�v�t�H���_�[
            DRIVES = &H11&                        ' �}�C�R���s���[�^
            NETWORK = &H12&                       ' �l�b�g���[�N(�l�b�g���[�N�S�̂���)
            NETHOOD = &H13&                       ' NETHOOD �t�H���_�[
            FONTS = &H14&                         ' �t�H���g
            TEMPLATES = &H15&                     ' �e���v���[�g
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
            RETURNONLYFSDIRS = &H1&          ' �t�H���_�̂�
            DONTGOBELOWDOMAIN = &H2&         ' �l�b�g���[�N�R���s���[�^�[���\��
            STATUSTEXT = &H4&                ' �X�e�[�^�X�\��
            RETURNFSANCESTORS = &H8&
            BROWSEFORCOMPUTER = &H1000&      ' �l�b�g���[�N�R���s���[�^�[�̂�
            BROWSEFORPRINTER = &H2000&       ' �v�����^�[�̂�
            BROWSEINCLUDEFILES = &H4000&     ' �S�đI���\
        End Enum

        '-------------------------------------------------------------------------------
        '�@ �t�H���_�I���_�C�A���O�\��
        '   �i�����T�v�j�t�H���_�I���_�C�A���O��\�����A���[�U�[���͒l��ԋp����
        '   �����̓p�����^�F<prmDefaultDir> �f�t�H���g�t�H���_
        '                 �F<prmTitle>      �_�C�A���O�ɕ\�����������
        '                 �F<prmRoot>       ���[�g�ʒu(ROOT�̒萔/�����l=DESKTOP)
        '                 �F<prmFlg>        �\���t�H���_�I�v�V����(FLG_FOLDER�̒萔/�����l=RETURNONLYFSDIRS)
        '                 �F<prmWHwnd>      �_�C�A���O�̃I�[�i�[�E�B���h�E�n���h��
        '   ���֐��߂�l�@�F����I�����F�I���t�H���_�t���p�X / �L�����Z�����F""
        '   �����̑��@�@�@�F�ȉ��AForm�ɂ�����g�p��
        '                        Dim RtnDir As String = UtilDirectorySelector.choiceFolder("C:\WINDOWS", "�����̃t�H���_��I�����Ă��������B")
        '                        MsgBox RtnDir
        '                                               2006.05.11 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------'
        ''' <summary>
        ''' �t�H���_�I���_�C�A���O�\��
        ''' </summary>
        ''' <param name="prmDefaultDir">�f�t�H���g�t�H���_</param>
        ''' <param name="prmTitle">�_�C�A���O�ɕ\�����������</param>
        ''' <param name="prmRoot">���[�g�ʒu(ROOT�̒萔/�����l=DESKTOP)</param>
        ''' <param name="prmFlg">�\���t�H���_�I�v�V����(FLG_FOLDER�̒萔/�����l=RETURNONLYFSDIRS)</param>
        ''' <param name="prmWHwnd">�_�C�A���O�̃I�[�i�[�E�B���h�E�n���h��</param>
        ''' <returns>����I�����F�I���t�H���_�t���p�X / �L�����Z�����F""</returns>
        ''' <remarks>�ȉ��AForm�ɂ�����g�p��
        '''Dim RtnDir As String = UtilDirectorySelector.choiceFolder("C:\WINDOWS", "�����̃t�H���_��I�����Ă��������B")
        ''' MsgBox RtnDir</remarks>
        Public Shared Function choiceFolder(Optional ByRef prmDefaultDir As String = vbNullString, _
                                            Optional ByRef prmTitle As String = "�t�H���_��I�����Ă�������", _
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

        'SHBrowseForFolder API �̃R�[���o�b�N�֐���`
        ''' <summary>
        ''' SHBrowseForFolder API �̃R�[���o�b�N�֐���`
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
                    Dim source() As Byte                        '�ϊ����̃o�C�g�z��
                    Dim encoded() As Byte                       '�ϊ���̃o�C�g�z��
                    source = Encoding.Unicode.GetBytes(text)    '��������o�C�g�z��ɕϊ�
                    encoded = Encoding.Convert(Encoding.Unicode, _
                                               Encoding.GetEncoding("shift_jis"), _
                                               source)          '�R�[�h�y�[�W��Unicode����V�t�gJIS�ɕϊ�
                    Call SendMessageStr(lngHWnd, _
                                        BFFM_SETSELECTIONA, _
                                        1&, _
                                        Encoding.GetEncoding("shift_jis").GetString(encoded))
                Case BFFM_SELCHANGED
                    'ITEM���I�����ꂽ���ɏ������s�������ꍇ�����ɋL�q
            End Select
            BrowseCallbackProc = 0&
        End Function

    End Class
End Namespace