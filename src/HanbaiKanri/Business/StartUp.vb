'===================================================================================
'�@ �i�V�X�e�����j      �݌Ɍv��V�X�e��
'
'   �i�@�\���j          �X�^�[�g�A�b�v�N���X�iSub Main���܂ށj
'   �i�N���X���j        StartUp
'   �i�����@�\���j      �A�v���P�[�V�����J�n���̏���
'   �i���l�j            �����`�F�b�N��Ƀ��j���[��ʂ�\������B
'
'===================================================================================
' ����  ���O                ���t       �}�[�N       ���e
'-----------------------------------------------------------------------------------
'  (1)  ���V                2010/07/16              �V�K
'-----------------------------------------------------------------------------------
Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.API
Imports UtilMDL.DB

'===================================================================================
'�X�^�[�g�A�b�v�N���X
'===================================================================================
Public Class StartUp

    '-------------------------------------------------------------------------------
    '�\���̐錾
    '-------------------------------------------------------------------------------
    Public Structure iniType                    'INI�t�@�C���i�[�p
        Public LogType As String                '���O���x��
        Public LogFilePath As String            '���O�t�@�C���p�X(�t�@�C�������܂�)
        Public MsgFileName As String            '���b�Z�[�W�t�@�C����
        Public UdlFilePath As String            '�݌Ɍv�� Oracle�ڑ��t�@�C���p�X
        Public UdlFilePath_Nouhinsho As String  '�[�i���f�[�^ SQLSV�ڑ��t�@�C���p�X
        Public UdlFilePath_Ukeharai As String   '�󕥔��㓝�v SQLSV�ڑ��t�@�C���p�X
        Public BaseXlsPath As String            'EXCEl�o�͂̐��`�t�H���_�p�X
        Public OutXlsPath As String             'EXCEl�o�͐�t�H���_�p�X
        Public ExcelZM610R1_Base As String      '���Y�\�̓}�X�^�ꗗ���`�t�@�C����
        Public ExcelZM610R1_Out As String       '���Y�\�̓}�X�^�ꗗ�o�̓t�@�C����
        Public ExcelZG310R1_Base As String      '�i��ʔ̔��v�搗�`�t�@�C����
        Public ExcelZG310R1_Out As String       '�i��ʔ̔��v��o�̓t�@�C����
        Public ExcelZG320R1_Base As String      '�i���ʔ̔��v�搗�`�t�@�C����
        Public ExcelZG320R1_Out As String       '�i���ʔ̔��v��o�̓t�@�C����
        Public ExcelZG530R1_Base As String      '���Y�E�̔��E�݌Ɍv�搗�`�t�@�C����
        Public ExcelZG530R1_Out As String       '���Y�E�̔��E�݌Ɍv��o�̓t�@�C����
        Public ExcelZG530R2_Base As String      '�i��ʏW�v�\���`�t�@�C����
        Public ExcelZG530R2_Out As String       '�i��ʏW�v�\�o�̓t�@�C����
        Public ExcelZG640R1_Base As String      '�݌ɕ�[�i���X�g���`�t�@�C����
        Public ExcelZG640R1_Out As String       '�݌ɕ�[�i���X�g�o�̓t�@�C����
        Public ExcelZM120R1_Base As String      '�v��Ώەi�}�X�^�ꗗ���`�t�@�C����
        Public ExcelZM120R1_Out As String       '�v��Ώەi�}�X�^�ꗗ�o�̓t�@�C����
        Public ExcelZM130R1_Base As String      '�v��Ώەi�ꗗ���`�t�@�C����
        Public ExcelZM130R1_Out As String       '�v��Ώەi�ꗗ�o�̓t�@�C����
        Public ExcelZG220R1_Base As String      '���Y�ʃf�[�^�ꗗ���`�t�@�C����
        Public ExcelZG220R1_Out As String       '���Y�ʃf�[�^�ꗗ�o�̓t�@�C����
        Public ExcelZG730R1_Base As String      '�@�B�ʕ��׎R�ϏW�v�\���`�t�@�C����
        Public ExcelZG730R1_Out As String       '�@�B�ʕ��׎R�ϏW�v�\�o�̓t�@�C����
        Public ExcelZG731R1_Base As String      '�@�B�ʕi���ʕ��׎R�ϏW�v�\���`�t�@�C����
        Public ExcelZG731R1_Out As String       '�@�B�ʕi���ʕ��׎R�ϏW�v�\�o�̓t�@�C����
        '-->2010.12.07 add by takagi
        Public ExcelZE110R1_Base As String      '�̔����ѐ��`�t�@�C����
        Public ExcelZE110R1_Out As String       '�̔����яo�̓t�@�C����
        '<--2010.12.07 add by takagi
        '-->2010.12.09 add by takagi
        Public TableOwner As String             '�e�[�u���I�[�i��
        Public LinkSvForHanbai As String        'TMC101��KNDSV43�ւ̃����N�T�[�o��
        Public ExcelZE210R1_Base As String      '�݌Ɏ��ѐ��`�t�@�C����
        Public ExcelZE210R1_Out As String       '�݌Ɏ��яo�̓t�@�C����
        '<--2010.12.09 add by takagi
    End Structure

    '���O�C�����i�[�ϐ�
    Public Structure loginType              '���O�C�����i�[�p
        Public TantoCD As String            '�i���g�p�j
        Public Passwd As String             '���[�U��
        Public Kengen As String             '����
        Public TantoNM As String            '����R�[�h
        Public PcName As String             '�[����
        Public BumonCD As String            '����R�[�h
        Public TantoSign As String          '�S���T�C��
    End Structure
    '�ėp�}�X�^�f�[�^�i�[�p�ϐ�
    Public Structure hanyouM
        Public KoteiKey As String
        Public KahenKey As String
        Public Mei1 As String
        Public Mei2 As String
    End Structure

    '-------------------------------------------------------------------------------
    '�萔�錾
    '-------------------------------------------------------------------------------
    '�Œ�̃t�@�C������p�X��
    Public Const INI_FILE As String = "HanbaiKanri.ini"                                'Ini�t�@�C����

    '�h�m�h�t�@�C���̃Z�N�V��������
    Private Const INIITEM1_LOGFILE As String = "Logging"                                '���O���x��/���O�t�@�C�����
    Private Const INIITEM1_MSGFILE As String = "msg File"                               '���b�Z�[�W�t�@�C�����
    Private Const INIITEM1_DB As String = "DB"                                          'DB�ڑ��t�@�C���p�X
    Private Const INIITEM1_ORACLE As String = "Oracle"                                  'DB�ڑ����
    Private Const INIITEM1_FILE As String = "File"                                      '�e��t�@�C��
    Private Const INIITEM1_ID As String = "ID"                                          '�[��ID
    Private Const INIITEM1_EXCEL As String = "Excel Objects"                            '�e��EXCEL

    '�h�m�h�t�@�C���̃L�[����
    Private Const INIITEM2_LOGTYPE As String = "LogType"                                '���O���x��
    Private Const INIITEM2_LOGFILEPATH As String = "LogFilePath"                        '���O�t�@�C���p�X(�t�@�C�������܂�)
    Private Const INIITEM2_MSGFILENAME As String = "msgFileName"                        '���b�Z�[�W�t�@�C����
    Private Const INIITEM2_UDLFILEPATHORA As String = "UdlFilePath"                     'ORACLE�ڑ��t�@�C���p�X
    Private Const INIITEM2_UDLFILEPATHNOHIN As String = "UdlFilePath_Nouhinsho"         '�[�i���f�[�^�ڑ��t�@�C���p�X
    Private Const INIITEM2_UDLFILEPATHUKE As String = "UdlFilePath_Ukeharai"            '�󕥔��㓝�v�ڑ��t�@�C���p�X
    Private Const INIITEM2_BASEXLSPATH As String = "BaseXLSPath"                        'EXCEL���`�t�@�C���p�X
    Private Const INIITEM2_OUTXLSPATH As String = "OutXLSPath"                          'EXCEL�o�͐�t�H���_�p�X
    Private Const INIITEM2_ZM610R1_BASE As String = "ZM610R1_Base"                      '���Y�\�̓}�X�^�����e���`�t�@�C����
    Private Const INIITEM2_ZM610R1_OUT As String = "ZM610R1_Out"                        '���Y�\�̓}�X�^�����e�o�̓t�@�C����
    Private Const INIITEM2_ZG310R1_BASE As String = "ZG310R1_Base"                      '�i��ʔ̔��v�搗�`�t�@�C����
    Private Const INIITEM2_ZG310R1_OUT As String = "ZG310R1_Out"                        '�i��ʔ̔��v��o�̓t�@�C����
    Private Const INIITEM2_ZG320R1_BASE As String = "ZG320R1_Base"                      '�i���ʔ̔��v�搗�`�t�@�C����
    Private Const INIITEM2_ZG320R1_OUT As String = "ZG320R1_Out"                        '�i���ʔ̔��v��o�̓t�@�C����
    Private Const INIITEM2_ZG530R1_BASE As String = "ZG530R1_Base"                      '���Y�E�̔��E�݌Ɍv�搗�`�t�@�C����
    Private Const INIITEM2_ZG530R1_OUT As String = "ZG530R1_Out"                        '���Y�E�̔��E�݌Ɍv��o�̓t�@�C����
    Private Const INIITEM2_ZG530R2_BASE As String = "ZG530R2_Base"                      '�i��ʏW�v�\���`�t�@�C����
    Private Const INIITEM2_ZG530R2_OUT As String = "ZG530R2_Out"                        '�i��ʏW�v�\�o�̓t�@�C����
    Private Const INIITEM2_ZG640R1_Base As String = "ZG640R1_Base"                      '�݌ɕ�[�i���X�g���`�t�@�C����
    Private Const INIITEM2_ZG640R1_OUT As String = "ZG640R1_Out"                        '�݌ɕ�[�i���X�g�o�̓t�@�C����
    Private Const INIITEM2_ZM120R1_BASE As String = "ZM120R1_Base"                       '�v��Ώەi�}�X�^�ꗗ���`�t�@�C����
    Private Const INIITEM2_ZM120R1_OUT As String = "ZM120R1_Out"                         '�v��Ώەi�}�X�^�ꗗ�o�̓t�@�C����
    Private Const INIITEM2_ZM130R1_BASE As String = "ZM130R1_Base"                       '�v��Ώەi�ꗗ���`�t�@�C����
    Private Const INIITEM2_ZM130R1_OUT As String = "ZM130R1_Out"                         '�v��Ώەi�ꗗ�o�̓t�@�C����
    Private Const INIITEM2_ZG220R1_Base As String = "ZG220R1_Base"                      '���Y�ʃf�[�^�ꗗ���`�t�@�C����
    Private Const INIITEM2_ZG220R1_OUT As String = "ZG220R1_Out"                        '���Y�ʃf�[�^�ꗗ�o�̓t�@�C����
    Private Const INIITEM2_ZG730R1_Base As String = "ZG730R1_Base"                      '�@�B�ʕ��׎R�ϏW�v�\���`�t�@�C����
    Private Const INIITEM2_ZG730R1_OUT As String = "ZG730R1_Out"                        '�@�B�ʕ��׎R�ϏW�v�\�o�̓t�@�C����
    Private Const INIITEM2_ZG731R1_Base As String = "ZG731R1_Base"                      '�@�B�ʕi���ʕ��׎R�ϏW�v�\���`�t�@�C����
    Private Const INIITEM2_ZG731R1_OUT As String = "ZG731R1_Out"                        '�@�B�ʕi���ʕ��׎R�ϏW�v�\�o�̓t�@�C����
    '-->2010.12.07 add by takagi
    Private Const INIITEM2_ZE110R1_Base As String = "ZE110R1_Base"                      '�̔����ѐ��`�t�@�C����
    Private Const INIITEM2_ZE110R1_OUT As String = "ZE110R1_Out"                        '�̔����яo�̓t�@�C����
    '<--2010.12.07 add by takagi
    '-->2010.12.09 add by takagi
    Private Const INIITEM2_TableOwner As String = "TableOwner"                          '�e�[�u���I�[�i��
    Private Const INIITEM2_LinkSvForHanbai As String = "LinkSvForHanbai"                '�����N�T�[�o��
    Private Const INIITEM2_ZE210R1_Base As String = "ZE210R1_Base"                      '�݌Ɏ��ѐ��`�t�@�C����
    Private Const INIITEM2_ZE210R1_OUT As String = "ZE210R1_Out"                        '�݌Ɏ��яo�̓t�@�C����
    '<--2010.12.09 add by takagi

    Private Const RS As String = "RecSet"                           '���R�[�h�Z�b�g�e�[�u��

    '�ėp�}�X�^�擾�p��
    Private Const HYM_KOTEIKEY As String = "clHYM_KOTEIKEY"         '�ėp�}�X�^�@�Œ�L�[
    Private Const HYM_KAHENKEY As String = "clHYM_KAHENKEY"         '�ėp�}�X�^�@�σL�[
    Private Const HYM_MEI1 As String = "clHYM_MEI1"                 '�ėp�}�X�^�@���̂P
    Private Const HYM_MEI2 As String = "clHYM_MEI2"                 '�ėp�}�X�^�@���̂Q

    '���b�Z�[�WID
    Private Const SYSERR As String = "SystemErr"                '�V�X�e���G���[
    Private Const NOHANYOUMST As String = "noHanyouMst"         '�ėp�}�X�^�̒l�̎擾�Ɏ��s���܂����B

    Private Const SETUZOKUSTR As String = "_,_"                 '�ėp�}�X�^�̒l���\���̂���擾���邽�߂̕�����̈ꕔ

    '�R�[�h�I�����ʎq��ʂ���Ԃ��Ă��炤�l
    Public Const HANYO_BACK_NAME1 As String = "NAME1"           '����1
    Public Const HANYO_BACK_NAME2 As String = "NAME2"           '����2
    Public Const HANYO_BACK_NAME3 As String = "NAME3"           '����3
    Public Const HANYO_BACK_NAME4 As String = "NAME4"           '����4
    Public Const HANYO_BACK_NAME5 As String = "NAME5"           '����5

    '-------------------------------------------------------------------------------
    '�����o�[�ϐ��錾
    '-------------------------------------------------------------------------------
    Private Shared _assembly As System.Reflection.Assembly          '�A�Z���u��(�A�v���P�[�V�������)
    Private Shared _msgHd As UtilMsgHandler                         '���b�Z�[�W�n���h��
    Private Shared _db As UtilDBIf                                  '�c�Ǝ� DB�n���h��
    Private Shared _debugMode As Boolean                            '�f�o�b�O���[�h(���O���x����DEBUG�̏ꍇ��True)

    'Ini�t�@�C���i�[�ϐ�
    Private Shared _iniVal As iniType

    '�ėp�}�X�^�i�[�ϐ�
    Private Shared _hanyou_tb As Hashtable = New Hashtable

    '-------------------------------------------------------------------------------
    '�v���p�e�B�錾
    '-------------------------------------------------------------------------------   
    Public Shared ReadOnly Property assembly() As System.Reflection.Assembly    '�A�Z���u��
        Get
            Return _assembly
        End Get
    End Property
    Public Shared ReadOnly Property iniValue() As iniType                       '�������t�@�C���\���̂�ԋp
        Get
            Return _iniVal
        End Get
    End Property
    Public Shared ReadOnly Property DebugMode() As Boolean                      '�f�o�b�O���[�h��ԋp
        Get
            Return _debugMode
        End Get
    End Property
    Public Shared ReadOnly Property lCOLOR_YELLOW() As Color                    '��ʕ\���F��ԋp
        Get
            Return ColorTranslator.FromWin32(12648447)
        End Get
    End Property
    Public Shared ReadOnly Property lCOLOR_BLUE() As Color                      '��ʕ\���F��ԋp
        Get
            Return ColorTranslator.FromWin32(16777152)
        End Get
    End Property
    Public Shared ReadOnly Property lCOLOR_WHITE() As Color                     '��ʕ\���F��ԋp
        Get
            Return Color.White
        End Get
    End Property
    '    Public Shared ReadOnly Property lCOLOR_GREEN() As Color                     '��ʕ\���F��ԋp
    '        Get
    '            Return ColorTranslator.FromWin32(&H80FF80)
    '        End Get
    '    End Property
    '    Public Shared ReadOnly Property lCOLOR_GLAY() As Color                      '��ʕ\���F��ԋp
    '        Get
    '            Return ColorTranslator.FromWin32(12632256)
    '        End Get
    '    End Property
    Public Shared ReadOnly Property lCOLOR_PINK() As Color                      '��ʕ\���F��ԋp
        Get
            Return ColorTranslator.FromWin32(&HFFC0FF)
        End Get
    End Property
    Public Shared ReadOnly Property lCOLOR_RED() As Color                       '��ʕ\���F��ԋp
        Get
            Return ColorTranslator.FromWin32(&HFF&)
        End Get
    End Property
    '    Public Shared ReadOnly Property lCOLOR_SPR_TANCHO() As Color                '��ʕ\���F��ԋp
    '        Get
    '            Return ColorTranslator.FromWin32(11528071)
    '        End Get
    '    End Property
    '    Public Shared ReadOnly Property lCOLOR_SPR_HON() As Color                   '��ʕ\���F��ԋp
    '        Get
    '            Return ColorTranslator.FromWin32(14942152)
    '        End Get
    '    End Property
    '    Public Shared ReadOnly Property lCOLOR_SPR_YELLOW() As Color                '��ʕ\���F��ԋp
    '        Get
    '            Return ColorTranslator.FromWin32(8454143)
    '        End Get
    '    End Property
    '    Public Shared ReadOnly Property batMode() As String                         '�o�b�`�N�����[�h
    '        Get
    '            Return _batMode
    '        End Get
    '    End Property

    '-------------------------------------------------------------------------------
    '   ���߂ɋN�����郁�\�b�h
    '   �i�����T�v�j�A�v���P�[�V�����J�n���̏������s���B
    '               �e��`�F�b�N��ɁA���j���[�t�H�[���̕\�����s���B
    '   �����̓p�����^   �F�Ȃ�
    '   �����\�b�h�߂�l �F�Ȃ�
    '   ��������O       �F�Ȃ�(�������ʂ֗�O�͖߂��Ȃ�)
    '-------------------------------------------------------------------------------
    Shared Sub main(ByVal cmds() As String)

        Dim m As System.Threading.Mutex = Nothing
        Try

            _iniVal.LogFilePath = ""

            '��d�N���`�F�b�N
            m = New System.Threading.Mutex(False, "HanbaiKanri")
            '==>�u�V�X�e���I�����v��GC.KeepAlive(m)��Call���邱��
            If Not m.WaitOne(0, False) Then
                MessageBox.Show("���ɓ���̃A�v���P�[�V�������N�����Ă��܂��B",
                                    System.Reflection.Assembly.GetExecutingAssembly.GetName().Name,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information)
                Exit Sub
            End If

            '�������g�̃C���X�^���X���� �������g�̃C���X�^���X�𐶐����Ȃ���Shared���\�b�h�ȊO�͌Ăяo���Ȃ�
            Dim _instance As StartUp        '�������g
            _instance = New StartUp()
            _assembly = System.Reflection.Assembly.GetExecutingAssembly() '�A�Z���u���������o�[�Ɋi�[

            '-----------------------------------------------------------------------
            '�h�m�h�t�@�C���ǂݍ��݁B�i�t�@�C���̗L���A�h�m�h�t�@�C���̃Z�N�V�������̊e��`�F�b�N���s���j
            '-----------------------------------------------------------------------
            Try

                '���b�Z�[�W�pXml�t�@�C���̑��݃`�F�b�N
                Call _instance.checkMesXmlFile()

                '�����ݒ�t�@�C���Ǎ�
                Call _instance.setIniVal()

                '�����ݒ�t�@�C���`�F�b�N
                Call _instance.checkIniFile()

            Catch ue As UsrDefException             '���[�U�[��`��O(�����L���b�`����)�B���̗�O�͐e�u���b�N�ɔC����B
                Call ue.dspMsg()                    '�G���[�o��
                Exit Sub                            '�ǂݍ���/�`�F�b�N�G���[(���[�U�[��`��O�̏ꍇ�A�G���[�����ςȂ̂ŏI��)
            End Try

            '-----------------------------------------------------------------------
            'DB�n���h���̎擾����уR�l�N�V�����ڑ�
            '�ڑ��Ɏ��s�����ꍇ�A�u�f�[�^�x�[�X�ɐڑ��ł��܂���ł����v���o�͂��ďI������
            '-----------------------------------------------------------------------
            'UtilOleDBDebugger�̓C���X�^���X�𐶐�����ƁADB�R�l�N�V�����܂Ő������Ă����
            Dim iniWk As iniType = StartUp._iniVal
            '_db = New UtilOleDBDebugger(iniWk.UdlFilePath, iniWk.LogFilePath, _debugMode)
            Try

                Try
                    '�ėp�}�X�^���e���\���̂Ɋi�[
                    'Call _instance.getHanyouMST()

                Catch ex As UsrDefException             '���[�U�[��`��O(�����L���b�`����)�B���̗�O�͐e�u���b�N�ɔC����B
                    Call ex.dspMsg()                    '�G���[�o��
                    Exit Sub                            '�ǂݍ���/�`�F�b�N�G���[(���[�U�[��`��O�̏ꍇ�A�G���[�����ςȂ̂ŏI��)
                End Try

                '-----------------------------------------------------------------------
                '�@��ʋN��
                '-----------------------------------------------------------------------

                '���j���[���
                Dim openForm As Form = Nothing
                'openForm = New frmMZ01_01M_MainMenu(_msgHd, _db)
                'openForm = New ZC110M_Menu(_msgHd, _db)
                'openForm = New Sample_Chumon(_msgHd, _db)
                openForm = New frmKR11_Login(_msgHd, _db)
                '�t�H�[���I�[�v��
                Application.Run(openForm)

            Finally                                                                         '�K���ʉ߂��镔���Ō㏈�����s��
                '_db.close()
            End Try

        Catch ex As Exception                       '�V�X�e����O
            '�V�X�e���G���[MSG�o��
            Dim tmp As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        Finally
            Try
                '��d�N���`�F�b�N�p�̃C���X�^���X�J����������
                GC.KeepAlive(m)
            Catch ex As Exception
            End Try

        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   ���b�Z�[�W�pXml�t�@�C���̑��݃`�F�b�N
    '   �i�����T�v�j�t�@�C�������݂��Ȃ��ꍇ�A�A�v���P�[�V�������I������
    '   �����̓p�����^   �F�Ȃ�
    '   �����\�b�h�߂�l �F�Ȃ�
    '   ��������O       �F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub checkMesXmlFile()
        Try

            '���b�Z�[�W�pXml���݃`�F�b�N
            Dim msgFileName As String
            msgFileName = UtilClass.getAppPath(_assembly)               '�A�v���P�[�V�������s�p�X���擾
            If Not msgFileName.EndsWith("\") Then                       '"\"�ŏI����Ă��Ȃ��Ȃ�
                msgFileName = msgFileName & "\"
            End If

            '���b�Z�[�W�t�@�C����������
            Dim ini As String = UtilClass.getAppPath(_assembly) & "\..\Setting\" & INI_FILE
            Dim fileWk As String = (New UtilIniFileHandler(ini)).getIni(INIITEM1_MSGFILE, INIITEM2_MSGFILENAME)
            msgFileName = msgFileName & "..\Setting\" & fileWk

            If UtilClass.isFileExists(msgFileName) Then                 '�t�@�C�������݂���Ȃ�
                '���b�Z�[�W�n���h���𐶐�
                _msgHd = New UtilMsgHandler(msgFileName)                '����ȍ~_msgHd���g�p����
            Else
                '���݂��Ȃ��̂ŃG���[
                Throw New UsrDefException(fileWk & "���L��܂���B" & ControlChars.NewLine & _
                                          "�V�X�e���̋N���𒆎~���܂��B" & ControlChars.NewLine & _
                                          "���b�Z�[�W�t�@�C���擾�@�G���[")
            End If
        Catch ex As Exception
            'MSG���o�͂ł��Ȃ�
            Dim t As UsrDefException = New UsrDefException("�V�X�e���G���[���������܂����B" & ControlChars.NewLine & _
                                      "�V�X�e���̋N���𒆎~���܂��B" & ControlChars.NewLine & ControlChars.NewLine & _
                                      UtilClass.getErrDetail(ex) & _
                                      ex.Message & ControlChars.NewLine & _
                                      ex.StackTrace)
            t.dspMsg()
            Throw t
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '   �h�m�h�t�@�C���擾
    '   �i�����T�v�j�h�m�h�t�@�C���̐ݒ�����擾����B
    '   �����̓p�����^   �F�Ȃ�
    '   �����\�b�h�߂�l �F�Ȃ�
    '   ��������O       �FUsrDefException
    '-------------------------------------------------------------------------------
    Private Sub setIniVal()
        Try
            '-----------------------------------------------------------------------
            'INI�t�@�C���i�t���p�X�j�쐬
            '-----------------------------------------------------------------------
            Dim iniFileName As String
            iniFileName = UtilClass.getAppPath(_assembly)               '�A�v���P�[�V�������s�p�X���擾
            If Not iniFileName.EndsWith("\") Then                       '"\"�ŏI����Ă��Ȃ��Ȃ�
                iniFileName = iniFileName & "\"
            End If

            'INI�t�@�C����������
            iniFileName = iniFileName & "..\Setting\" & INI_FILE

            '-----------------------------------------------------------------------
            'INI�t�@�C�����݃`�F�b�N
            '-----------------------------------------------------------------------
            If Not UtilClass.isFileExists(iniFileName) Then
                Throw New UsrDefException("INI�t�@�C�����݃`�F�b�N�G���[", _msgHd.getMSG("nonIniFile"))
            End If

            '-----------------------------------------------------------------------
            'Ini�t�@�C���n���h���𐶐�����
            '-----------------------------------------------------------------------
            Dim ini As UtilIniFileHandler = New UtilIniFileHandler(iniFileName)

            '-----------------------------------------------------------------------
            'Ini�t�@�C���ݒ�l�i�[
            '-----------------------------------------------------------------------
            Dim errXMLstrkey As String = ""      '�L���b�`�����ꍇ�ɃG���[�ƂȂ�XML��Key

            Try
                errXMLstrkey = "NonIniKey"
                _iniVal.LogType = ini.getIni(INIITEM1_LOGFILE, INIITEM2_LOGTYPE)            '���O�^�C�v�̓ǂݍ��݂ƃf�o�b�O���[�h�̐ݒ�

                '�f�o�b�O���[�h��ݒ�
                If _iniVal.LogType.ToUpper.Equals("DEBUG") Then
                    _debugMode = True
                Else
                    _debugMode = False
                End If

                '���b�Z�[�W�t�@�C�����̓ǂݍ���
                _iniVal.MsgFileName = ini.getIni(INIITEM1_MSGFILE, INIITEM2_MSGFILENAME)

                'Log�t�@�C��
                errXMLstrkey = "NonLogDirKey"               'Log�t�@�C���i�[�ꏊ�̎擾�Ɏ��s���܂����B
                _iniVal.LogFilePath = ini.getIni(INIITEM1_LOGFILE, INIITEM2_LOGFILEPATH)
                If _iniVal.LogFilePath IsNot Nothing AndAlso _
                   (Not _iniVal.LogFilePath.StartsWith("..") And _iniVal.LogFilePath.StartsWith(".")) Then
                    _iniVal.LogFilePath = Mid(_iniVal.LogFilePath, 2)
                End If
                If Not _iniVal.LogFilePath.StartsWith("\") Then
                    _iniVal.LogFilePath = "\" & _iniVal.LogFilePath
                End If
                _iniVal.LogFilePath = UtilClass.getAppPath(_assembly) & _iniVal.LogFilePath

                ''UDL�t�@�C��
                'errXMLstrkey = "NonDbUdlKey"                '�c�a�ڑ��t�@�C���̊i�[�ꏊ�̎擾�Ɏ��s���܂����B
                '_iniVal.UdlFilePath = ini.getIni(INIITEM1_DB, INIITEM2_UDLFILEPATHORA)
                'If _iniVal.UdlFilePath IsNot Nothing AndAlso _
                '   (Not _iniVal.UdlFilePath.StartsWith("..") And _iniVal.UdlFilePath.StartsWith(".")) Then
                '    _iniVal.UdlFilePath = Mid(_iniVal.UdlFilePath, 2)
                'End If
                'If Not _iniVal.UdlFilePath.StartsWith("\") Then
                '    _iniVal.UdlFilePath = "\" & _iniVal.UdlFilePath
                'End If
                '_iniVal.UdlFilePath = UtilClass.getAppPath(_assembly) & _iniVal.UdlFilePath

                ''UDL�t�@�C��
                'errXMLstrkey = "NonDbUdlKey"                '�c�a�ڑ��t�@�C���̊i�[�ꏊ�̎擾�Ɏ��s���܂����B
                '_iniVal.UdlFilePath_Nouhinsho = ini.getIni(INIITEM1_DB, INIITEM2_UDLFILEPATHNOHIN)
                'If _iniVal.UdlFilePath_Nouhinsho IsNot Nothing AndAlso _
                '   (Not _iniVal.UdlFilePath_Nouhinsho.StartsWith("..") And _iniVal.UdlFilePath_Nouhinsho.StartsWith(".")) Then
                '    _iniVal.UdlFilePath_Nouhinsho = Mid(_iniVal.UdlFilePath_Nouhinsho, 2)
                'End If
                'If Not _iniVal.UdlFilePath_Nouhinsho.StartsWith("\") Then
                '    _iniVal.UdlFilePath_Nouhinsho = "\" & _iniVal.UdlFilePath_Nouhinsho
                'End If
                '_iniVal.UdlFilePath_Nouhinsho = UtilClass.getAppPath(_assembly) & _iniVal.UdlFilePath_Nouhinsho

                ''UDL�t�@�C��
                'errXMLstrkey = "NonDbUdlKey"                '�c�a�ڑ��t�@�C���̊i�[�ꏊ�̎擾�Ɏ��s���܂����B
                '_iniVal.UdlFilePath_Ukeharai = ini.getIni(INIITEM1_DB, INIITEM2_UDLFILEPATHUKE)
                'If _iniVal.UdlFilePath_Ukeharai IsNot Nothing AndAlso _
                '   (Not _iniVal.UdlFilePath_Ukeharai.StartsWith("..") And _iniVal.UdlFilePath_Ukeharai.StartsWith(".")) Then
                '    _iniVal.UdlFilePath_Ukeharai = Mid(_iniVal.UdlFilePath_Ukeharai, 2)
                'End If
                'If Not _iniVal.UdlFilePath_Ukeharai.StartsWith("\") Then
                '    _iniVal.UdlFilePath_Ukeharai = "\" & _iniVal.UdlFilePath_Ukeharai
                'End If
                '_iniVal.UdlFilePath_Ukeharai = UtilClass.getAppPath(_assembly) & _iniVal.UdlFilePath_Ukeharai

                'EXCEl���`�t�H���_�p�X
                errXMLstrkey = "NonXlsBaseDir"                'EXCEl���`�t�@�C���̊i�[�ꏊ�̎擾�Ɏ��s���܂����B
                _iniVal.BaseXlsPath = ini.getIni(INIITEM1_EXCEL, INIITEM2_BASEXLSPATH)
                If _iniVal.BaseXlsPath IsNot Nothing AndAlso _
                   (Not _iniVal.BaseXlsPath.StartsWith("..") And _iniVal.BaseXlsPath.StartsWith(".")) Then
                    _iniVal.BaseXlsPath = Mid(_iniVal.BaseXlsPath, 2)
                End If
                If Not _iniVal.BaseXlsPath.StartsWith("\") Then
                    _iniVal.BaseXlsPath = "\" & _iniVal.BaseXlsPath
                End If
                _iniVal.BaseXlsPath = UtilClass.getAppPath(_assembly) & _iniVal.BaseXlsPath

                'EXCEl�o�͐�t�H���_�t�@�C��
                errXMLstrkey = "NonXlsOutDir"                'EXCEl�o�̓t�@�C���̊i�[�ꏊ�̎擾�Ɏ��s���܂����B
                _iniVal.OutXlsPath = ini.getIni(INIITEM1_EXCEL, INIITEM2_OUTXLSPATH)
                If _iniVal.ExcelZM610R1_Base IsNot Nothing AndAlso _
                   (Not _iniVal.OutXlsPath.StartsWith("..") And _iniVal.OutXlsPath.StartsWith(".")) Then
                    _iniVal.OutXlsPath = Mid(_iniVal.OutXlsPath, 2)
                End If
                If Not _iniVal.OutXlsPath.StartsWith("\") Then
                    _iniVal.OutXlsPath = "\" & _iniVal.OutXlsPath
                End If
                _iniVal.OutXlsPath = UtilClass.getAppPath(_assembly) & _iniVal.OutXlsPath

                ''���Y�\�̓}�X�^EXCEl���`(ZM610R1)�t�@�C����
                'errXMLstrkey = "noZM610R1_Base"        '���Y�\�̓}�X�^���`�t�@�C�������ݒ肳��Ă��܂���B
                '_iniVal.ExcelZM610R1_Base = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZM610R1_BASE)

                ''���Y�\�̓}�X�^EXCEl�o��(ZM610R1)�t�@�C����
                'errXMLstrkey = "noZM610R1_Out"        '���Y�\�̓}�X�^�o�̓t�@�C�������ݒ肳��Ă��܂���B
                '_iniVal.ExcelZM610R1_Out = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZM610R1_OUT)

                ''�i��ʔ̔��v��EXCEl���`(ZG310R1)�t�@�C����
                'errXMLstrkey = "noZG310R1_Base"        '�i��ʔ̔��v�搗�`�t�@�C�������ݒ肳��Ă��܂���B
                '_iniVal.ExcelZG310R1_Base = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG310R1_BASE)

                ''�i��ʔ̔��v��EXCEl�o��(ZG310R1)�t�@�C����
                'errXMLstrkey = "noZG310R1_Out"        '�i��ʔ̔��v��o�̓t�@�C�������ݒ肳��Ă��܂���B
                '_iniVal.ExcelZG310R1_Out = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG310R1_OUT)

                ''�i���ʔ̔��v��EXCEl���`(ZG320R1)�t�@�C����
                'errXMLstrkey = "noZG320R1_Base"        '�i���ʔ̔��v�搗�`�t�@�C�������ݒ肳��Ă��܂���B
                '_iniVal.ExcelZG320R1_Base = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG320R1_BASE)

                ''�i���ʔ̔��v��EXCEl�o��(ZG320R1)�t�@�C����
                'errXMLstrkey = "noZG320R1_Out"        '�i���ʔ̔��v��o�̓t�@�C�������ݒ肳��Ă��܂���B
                '_iniVal.ExcelZG320R1_Out = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG320R1_OUT)

                ''���Y�E�̔��E�݌Ɍv��EXCEl���`(ZG530R1)�t�@�C����
                'errXMLstrkey = "noZG530R1_Base"        '���Y�E�̔��E�݌Ɍv�搗�`�t�@�C�������ݒ肳��Ă��܂���B
                '_iniVal.ExcelZG530R1_Base = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG530R1_BASE)

                ''���Y�E�̔��E�݌Ɍv��EXCEl�o��(ZG530R1)�t�@�C����
                'errXMLstrkey = "noZG530R1_Out"        '���Y�E�̔��E�݌Ɍv��o�̓t�@�C�������ݒ肳��Ă��܂���B
                '_iniVal.ExcelZG530R1_Out = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG530R1_OUT)

                ''�i��ʏW�v�\EXCEl���`(ZG530R2)�t�@�C����
                'errXMLstrkey = "noZG530R2_Base"        '�i��ʏW�v�\���`�t�@�C�������ݒ肳��Ă��܂���B
                '_iniVal.ExcelZG530R2_Base = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG530R2_BASE)

                ''�i��ʏW�v�\EXCEl�o��(ZG530R2)�t�@�C����
                'errXMLstrkey = "noZG530R2_Out"        '�i��ʏW�v�\�o�̓t�@�C�������ݒ肳��Ă��܂���B
                '_iniVal.ExcelZG530R2_Out = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG530R2_OUT)

                ''�݌ɕ�[�i���X�gEXCEl���`(ZG640R1)�t�@�C����
                'errXMLstrkey = "noZG640R1_Base"        '�݌ɕ�[�i���X�g���`�t�@�C�������݂��܂���B
                '_iniVal.ExcelZG640R1_Base = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG640R1_Base)
                ''�݌ɕ�[�i���X�gEXCEl�o��(ZG640R1)�t�@�C����
                'errXMLstrkey = "noZG640R1_Out"        '�݌ɕ�[�i���X�g�o�̓t�@�C�������ݒ肳��Ă��܂���B
                '_iniVal.ExcelZG640R1_Out = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG640R1_OUT)

                ''�v��Ώەi�}�X�^�ꗗEXCEl���`(ZM120R1)�t�@�C����
                'errXMLstrkey = "noZM120R1_Base"        '�i��ʏW�v�\�o�̓t�@�C�������ݒ肳��Ă��܂���B
                '_iniVal.ExcelZM120R1_Base = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZM120R1_BASE)

                ''�v��Ώەi�}�X�^�ꗗEXCEl�o��(ZM120R1)�t�@�C����
                'errXMLstrkey = "noZM120R1_Out"        '�i��ʏW�v�\�o�̓t�@�C�������ݒ肳��Ă��܂���B
                '_iniVal.ExcelZM120R1_Out = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZM120R1_OUT)

                ''�v��Ώەi�ꗗEXCEl���`(ZM130R1)�t�@�C����
                'errXMLstrkey = "noZM130R1_Base"        '�i��ʏW�v�\�o�̓t�@�C�������ݒ肳��Ă��܂���B
                '_iniVal.ExcelZM130R1_Base = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZM130R1_BASE)

                ''�v��Ώەi�ꗗEXCEl�o��(ZM130R1)�t�@�C����
                'errXMLstrkey = "noZM130R1_Out"        '�i��ʏW�v�\�o�̓t�@�C�������ݒ肳��Ă��܂���B
                '_iniVal.ExcelZM130R1_Out = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZM130R1_OUT)


                ''���Y�ʃf�[�^�ꗗEXCEl���`(ZG210R1)�t�@�C����
                'errXMLstrkey = "noZG220R1_Base"        '���Y�ʃf�[�^�ꗗ���`�t�@�C�������݂��܂���B
                '_iniVal.ExcelZG220R1_Base = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG220R1_Base)
                ''���Y�ʃf�[�^�ꗗEXCEl�o��(ZG210R1)�t�@�C����
                'errXMLstrkey = "noZG220R1_Out"        '�݌ɕ�[�i���X�g�o�̓t�@�C�������ݒ肳��Ă��܂���B
                '_iniVal.ExcelZG220R1_Out = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG220R1_OUT)

                ''�@�B�ʕ��׎R�ϏW�v�\EXCEl���`(ZG730R1)�t�@�C����
                'errXMLstrkey = "noZG730R1_Base"        '�@�B�ʕ��׎R�ϏW�v�\���`�t�@�C�������݂��܂���B
                '_iniVal.ExcelZG730R1_Base = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG730R1_Base)
                ''�@�B�ʕ��׎R�ϏW�v�\EXCEl�o��(ZG730R1)�t�@�C����
                'errXMLstrkey = "noZG730R1_Out"        '�@�B�ʕ��׎R�ϏW�v�\�o�̓t�@�C�������ݒ肳��Ă��܂���B
                '_iniVal.ExcelZG730R1_Out = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG730R1_OUT)

                ''�@�B�ʕi���ʕ��׎R�ϏW�v�\EXCEl���`(ZG731R1)�t�@�C����
                'errXMLstrkey = "noZG731R1_Base"        '�@�B�ʕi���ʕ��׎R�ϏW�v�\���`�t�@�C�������݂��܂���B
                '_iniVal.ExcelZG731R1_Base = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG731R1_Base)
                ''�@�B�ʕi���ʕ��׎R�ϏW�v�\EXCEl�o��(ZG731R1)�t�@�C����
                'errXMLstrkey = "noZG731R1_Out"        '�@�B�ʕi���ʕ��׎R�ϏW�v�\�o�̓t�@�C�������ݒ肳��Ă��܂���B
                '_iniVal.ExcelZG731R1_Out = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZG731R1_OUT)

                ''-->2010.12.07 add by takagi
                ''�̔�����EXCEl���`(ZE110R1)�t�@�C����
                'errXMLstrkey = "noZE110R1_Base"        '�̔����ѐ��`�t�@�C�������݂��܂���B
                '_iniVal.ExcelZE110R1_Base = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZE110R1_Base)
                ''�̔�����EXCEl�o��(ZE110R1)�t�@�C����
                'errXMLstrkey = "noZE110R1_Out"        '�̔����яo�̓t�@�C�������ݒ肳��Ă��܂���B
                '_iniVal.ExcelZE110R1_Out = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZE110R1_OUT)
                ''<--2010.12.07 add by takagi

                ''-->2010.12.09 add by takagi
                ''�e�[�u���I�[�i��
                'errXMLstrkey = "noTableOwner"        '�e�[�u���I�[�i�������݂��܂���B
                '_iniVal.TableOwner = ini.getIni(INIITEM1_DB, INIITEM2_TableOwner)
                ''�����N�T�[�o�[��
                'errXMLstrkey = "noLinkSvForHanbai"        '�����N�T�[�o�[�������݂��܂���B
                '_iniVal.LinkSvForHanbai = ini.getIni(INIITEM1_DB, INIITEM2_LinkSvForHanbai)

                ''�݌Ɏ���EXCEl���`(ZE210R1)�t�@�C����
                'errXMLstrkey = "noZE210R1_Base"        '�̔����ѐ��`�t�@�C�������݂��܂���B
                '_iniVal.ExcelZE210R1_Base = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZE210R1_Base)
                ''�݌Ɏ���EXCEl�o��(ZE210R1)�t�@�C����
                'errXMLstrkey = "noZE210R1_Out"        '�̔����яo�̓t�@�C�������ݒ肳��Ă��܂���B
                '_iniVal.ExcelZE210R1_Out = ini.getIni(INIITEM1_EXCEL, INIITEM2_ZE210R1_OUT)
                ''<--2010.12.09 add by takagi

            Catch ex As Exception
                '�ϐ��ierrXMLstrkey�j�����b�Z�[�W�h�c�Ƃ��āA�G���[���o�͂��܂��B
                Throw New UsrDefException("���ږ��ݒ�G���[", _msgHd.getMSG(errXMLstrkey))
            End Try
        Catch ue As UsrDefException '���[�U�[��`��O(�L�q���Ȃ��ꍇ�AException�ŃL���b�`����邽��)
            Call ue.dspMsg()
            Throw ue                '�L���b�`������O�����̂܂܃X���[(�Ăяo�����ŃG���[����)
        Catch ex As Exception       '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex))) '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub
    '-------------------------------------------------------------------------------
    '   �h�m�h�t�@�C���ݒ���e�`�F�b�N
    '   �i�����T�v�j�h�m�h�t�@�C���̐ݒ���e���`�F�b�N����B
    '   �����̓p�����^   �F�Ȃ�
    '   �����\�b�h�߂�l �F�Ȃ�
    '   ��������O       �FUsrDefException
    '-------------------------------------------------------------------------------
    Private Sub checkIniFile()
        Try

            '���O�t�@�C���̏o�͐�p�X�̑��݃`�F�b�N
            Dim fullPath As String = _iniVal.LogFilePath
            Dim pathName As String = ""
            Dim fileName As String = ""
            UtilClass.dividePathAndFile(fullPath, pathName, fileName)
            If Not UtilClass.isDirExists(pathName) Then
                _iniVal.LogFilePath = "" '���O�t�@�C�����Ԉ���Ă���ꍇ�Ƀ��O�o�͂���Ǝ��s�G���[���o��ׁA���������Ă���
                Throw New UsrDefException("���O�t�@�C���̏o�͐悪���݂��Ȃ��G���[", _msgHd.getMSG("NonLogDir"))
            End If

            '���`�t�H���_���݃`�F�b�N
            If Not UtilClass.isDirExists(_iniVal.BaseXlsPath) Then
                Throw New UsrDefException("���`�t�@�C���i�[�t�H���_������܂���B", _msgHd.getMSG("noHinaDir"))
            End If

            'EXCEL�o�͐�t�H���_���݃`�F�b�N
            If Not UtilClass.isDirExists(_iniVal.OutXlsPath) Then
                Throw New UsrDefException("EXCEL�t�@�C���o�͐�t�H���_������܂���B", _msgHd.getMSG("noOutExcelDir"))
            End If

            ''���Y�\�̓}�X�^EXCEl���`(ZM610R1)�t�@�C�����݃`�F�b�N
            'If Not UtilClass.isFileExists(_iniVal.BaseXlsPath & "\" & _iniVal.ExcelZM610R1_Base) Then
            '    Throw New UsrDefException("���Y�\�̓}�X�^���`�t�@�C����������܂���B", _msgHd.getMSG("notExistZM610R1_Base"))
            'End If

            ''�i��ʔ̔��v��EXCEl���`(ZG310R1)�t�@�C�����݃`�F�b�N
            'If Not UtilClass.isFileExists(_iniVal.BaseXlsPath & "\" & _iniVal.ExcelZG310R1_Base) Then
            '    Throw New UsrDefException("�i��ʔ̔��v�搗�`�t�@�C����������܂���B", _msgHd.getMSG("notExistZG310R1_Base"))
            'End If

            ''�i���ʔ̔��v��EXCEl���`(ZG320R1)�t�@�C�����݃`�F�b�N
            'If Not UtilClass.isFileExists(_iniVal.BaseXlsPath & "\" & _iniVal.ExcelZG310R1_Base) Then
            '    Throw New UsrDefException("�i���ʔ̔��v�搗�`�t�@�C����������܂���B", _msgHd.getMSG("notExistZG320R1_Base"))
            'End If

            ''���Y�E�̔��E�݌Ɍv��EXCEl���`(ZG530R1)�t�@�C�����݃`�F�b�N
            'If Not UtilClass.isFileExists(_iniVal.BaseXlsPath & "\" & _iniVal.ExcelZG530R1_Base) Then
            '    Throw New UsrDefException("���Y�E�̔��E�݌Ɍv�搗�`�t�@�C����������܂���B", _msgHd.getMSG("notExistZG530R1_Base"))
            'End If

            ''�i��ʏW�v�\EXCEl���`(ZG530R2)�t�@�C�����݃`�F�b�N
            'If Not UtilClass.isFileExists(_iniVal.BaseXlsPath & "\" & _iniVal.ExcelZG530R2_Base) Then
            '    Throw New UsrDefException("�i��ʏW�v�\���`�t�@�C����������܂���B", _msgHd.getMSG("notExistZG530R2_Base"))
            'End If

            ''�v��Ώەi�}�X�^�ꗗEXCEl���`(ZM120R1)�t�@�C�����݃`�F�b�N
            'If Not UtilClass.isFileExists(_iniVal.BaseXlsPath & "\" & _iniVal.ExcelZM120R1_Base) Then
            '    Throw New UsrDefException("�v��Ώەi�}�X�^�ꗗ���`�t�@�C����������܂���B", _msgHd.getMSG("notExistZM120R1_Base"))
            'End If

            ''�v��Ώەi�ꗗEXCEl���`(ZM130R1)�t�@�C�����݃`�F�b�N
            'If Not UtilClass.isFileExists(_iniVal.BaseXlsPath & "\" & _iniVal.ExcelZM130R1_Base) Then
            '    Throw New UsrDefException("�v��Ώەi�ꗗ���`�t�@�C����������܂���B", _msgHd.getMSG("notExistZM130R1_Base"))
            'End If

            ''�@�B�ʕ��׎R�ϏW�v�\EXCEl���`(ZG730R1)�t�@�C�����݃`�F�b�N
            'If Not UtilClass.isFileExists(_iniVal.BaseXlsPath & "\" & _iniVal.ExcelZG730R1_Base) Then
            '    Throw New UsrDefException("�@�B�ʕ��׎R�ϏW�v�\���`�t�@�C����������܂���B", _msgHd.getMSG("notExistZG730R1_Base"))
            'End If

            ''�@�B�ʕi���ʕ��׎R�ϏW�v�\EXCEl���`(ZG731R1)�t�@�C�����݃`�F�b�N
            'If Not UtilClass.isFileExists(_iniVal.BaseXlsPath & "\" & _iniVal.ExcelZG731R1_Base) Then
            '    Throw New UsrDefException("�@�B�ʕi���ʕ��׎R�ϏW�v�\���`�t�@�C����������܂���B", _msgHd.getMSG("notExistZG731R1_Base"))
            'End If

        Catch ue As UsrDefException '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception       '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex))) '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�ėp�}�X�^�f�[�^�擾
    '   �i�����T�v�j�ėp�}�X�^�̃f�[�^��ǂݍ��݁A�����e�[�u���Ɋi�[����
    '-------------------------------------------------------------------------------
    Private Sub getHanyouMST()
        Try

            Dim SQL As String = ""               'SQL�ێ��p�ϐ�
            SQL = "SELECT "
            SQL = SQL & "  HYM_KOTEIKEY         " & HYM_KOTEIKEY
            SQL = SQL & " ,HYM_KAHENKEY         " & HYM_KAHENKEY
            SQL = SQL & " ,HYM_MEI1             " & HYM_MEI1
            SQL = SQL & " ,HYM_MEI2             " & HYM_MEI2
            SQL = SQL & " FROM MZ09_HANYOU_MST "

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(SQL, RS, iRecCnt)

            If iRecCnt <= 0 Then             '���o���R�[�h���P�����Ȃ��ꍇ
                Throw New UsrDefException("�ėp�}�X�^�̒l�̎擾�Ɏ��s���܂����B", _msgHd.getMSG("noHanyouMst"))
            End If

            '�������ʂ��\���̂Ɋi�[
            With ds.Tables(RS)
                For iCnt As Integer = 0 To iRecCnt - 1

                    Dim recKey As String = _db.rmNullStr(.Rows(iCnt)(HYM_KOTEIKEY)) & SETUZOKUSTR & _db.rmNullStr(.Rows(iCnt)(HYM_KAHENKEY))
                    Dim rec As hanyouM = New hanyouM
                    rec.KoteiKey = _db.rmNullStr(.Rows(iCnt)(HYM_KOTEIKEY))      '�Œ�L�[
                    rec.KahenKey = _db.rmNullStr(.Rows(iCnt)(HYM_KAHENKEY))      '�σL�[
                    rec.Mei1 = _db.rmNullStr(.Rows(iCnt)(HYM_MEI1))              '���̂P
                    rec.Mei2 = _db.rmNullStr(.Rows(iCnt)(HYM_MEI2))              '���̂Q
                    _hanyou_tb.Add(recKey, rec)

                Next

            End With

            ds = Nothing

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG(SYSERR, UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@�ėp�}�X�^���̕ԋp
    '   �i�����T�v�j�n���ꂽ�Œ襉σL�[�����ɁA�ėp�}�X�^�̖��̂P�E�Q��ԋp����
    '-------------------------------------------------------------------------------
    Public Shared Function cnvKeyToName(ByVal sPrmKoteiKey As String, ByVal sPrmKahenKey As String, _
                                        Optional ByVal sPrmName2 As String = "") As String
        Try

            Dim rec As hanyouM = CType(_hanyou_tb.Item(sPrmKoteiKey & SETUZOKUSTR & sPrmKahenKey), hanyouM)
            sPrmName2 = rec.Mei2
            Return rec.Mei1

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG(SYSERR, UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Function

End Class
