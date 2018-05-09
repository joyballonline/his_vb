'===================================================================================
'�@ �i�V�X�e�����j      ������� �S���l�����@�̔��Ǘ��V�X�e��
'
'   �i�@�\���j          �X�^�[�g�A�b�v�N���X�iSub Main���܂ށj
'   �i�N���X���j        StartUp
'   �i�����@�\���j      �A�v���P�[�V�����J�n���̏���
'   �i���l�j            �����`�F�b�N��Ƀ��j���[��ʂ�\������B
'
'===================================================================================
' ����  ���O                ���t       �}�[�N       ���e
'-----------------------------------------------------------------------------------
'  (1)  ����                2018/01/05              �V�K
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
        Public SVAddr As String                 '�T�[�o���i���C���T�[�o�j
        Public PortNo As String                 '�|�[�g�ԍ��i���C���T�[�o�j
        Public DBName As String                 '�c�a�i���C���T�[�o�j
        Public UserId As String                 '���[�U�i���C���T�[�o�j
        Public Password As String               '�p�X���[�h�i���C���T�[�o�j
        Public SVAddr_stby As String            '�T�[�o���i�o�b�N�A�b�v�T�[�o�j
        Public PortNo_stby As String            '�|�[�g�ԍ��i�o�b�N�A�b�v�T�[�o�j
        Public DBName_stby As String            '�c�a�i�o�b�N�A�b�v�T�[�o�j
        Public UserId_stby As String            '���[�U�i�o�b�N�A�b�v�T�[�o�j
        Public Password_stby As String          '�p�X���[�h�i�o�b�N�A�b�v�T�[�o�j
        Public SystemCaption As String          '���O�C���\���V�X�e����
        Public GamenSelUpperCnt As Integer      '�e����͉�ʌ������A�x���\������������
        Public PrintSelUpperCnt As Integer      '���[�o�͎w����ʌ������A�x���\������������
        Public BaseXlsPath As String            'EXCEl�o�͂̐��`�t�H���_�p�X
        Public OutXlsPath As String             'EXCEl�o�͐�t�H���_�p�X
    End Structure

    '�ėp�}�X�^�f�[�^�i�[�p�ϐ�
    Public Structure hanyouM
        Public KoteiKey As String
        Public KahenKey As String
        Public Mei1 As String
        Public Mei2 As String
    End Structure

    '���O�C�����i�[�ϐ�
    Public Structure loginType              '���O�C�����i�[�p
        Public TantoCD As String            '���[�U�h�c
        Public Passwd As String             '�p�X���[�h
        Public Kengen As String             '����
        Public TantoNM As String            '�S���Җ�
        Public PcName As String             '�[����
        Public BumonCD As String            '��ЃR�[�h
        Public BumonNM As String            '��З���
        Public TantoSign As String          '�S���T�C��
        Public Generation As Integer        '����ԍ�
    End Structure

    '-------------------------------------------------------------------------------
    '�萔�錾
    '-------------------------------------------------------------------------------
    '�Œ�̃t�@�C������p�X��
    Public Const INI_FILE As String = "HanbaiKanri.ini"                                'Ini�t�@�C����

    '�h�m�h�t�@�C���̃Z�N�V��������
    Private Const INIITEM1_LOGFILE As String = "Logging"                                '���O���x��/���O�t�@�C�����
    Private Const INIITEM1_MSGFILE As String = "msg File"                               '���b�Z�[�W�t�@�C�����
    Private Const INIITEM1_DB As String = "DB"                                          '���C���T�[�oDB�ڑ����
    Private Const INIITEM1_DB_STBY As String = "DB_Standby"                             '�o�b�N�A�b�v�T�[�oDB�ڑ����
    Private Const INIITEM1_PRODUCT_INFO As String = "Product Info"                      '�V�X�e���K����
    Private Const INIITEM1_EXCEL As String = "Excel Objects"                            '�e��EXCEL

    '�h�m�h�t�@�C���̃L�[����
    Private Const INIITEM2_LOGTYPE As String = "LogType"                                '���O���x��
    Private Const INIITEM2_LOGFILEPATH As String = "LogFilePath"                        '���O�t�@�C���p�X(�t�@�C�������܂�)
    Private Const INIITEM2_MSGFILENAME As String = "msgFileName"                        '���b�Z�[�W�t�@�C����
    Private Const INIITEM2_SVADDR As String = "SVAddr"                                  '�T�[�o���i���C���T�[�o�j
    Private Const INIITEM2_PORTNO As String = "PortNo"                                  '�|�[�g�ԍ��i���C���T�[�o�j
    Private Const INIITEM2_DBNAME As String = "DBName"                                  '�c�a�i���C���T�[�o�j
    Private Const INIITEM2_USERID As String = "UserId"                                  '���[�U�i���C���T�[�o�j
    Private Const INIITEM2_PASSWORD As String = "Password"                              '�p�X���[�h�i���C���T�[�o�j
    Private Const INIITEM2_SVADDR_STBY As String = "SVAddr"                             '�T�[�o���i�o�b�N�A�b�v�T�[�o�j
    Private Const INIITEM2_PORTNO_STBY As String = "PortNo"                             '�|�[�g�ԍ��i�o�b�N�A�b�v�T�[�o�j
    Private Const INIITEM2_DBNAME_STBY As String = "DBName"                             '�c�a�i�o�b�N�A�b�v�T�[�o�j
    Private Const INIITEM2_USERID_STBY As String = "UserId"                             '���[�U�i�o�b�N�A�b�v�T�[�o�j
    Private Const INIITEM2_PASSWORD_STBY As String = "Password"                         '�p�X���[�h�i�o�b�N�A�b�v�T�[�o�j
    Private Const INIITEM2_SYSTEMCAPTION As String = "SystemCaption"                    '���O�C���\���V�X�e����
    Private Const INIITEM2_GAMENSELUPPERCNT As String = "GamenSelUpperCnt"              '�e����͉�ʌ����A���x���\������������
    Private Const INIITEM2_PRINTSELUPPERCNT As String = "PrintSelUpperCnt"              '���[�o�͎w����ʌ������A�x���\������������
    Private Const INIITEM2_BASEXLSPATH As String = "BaseXLSPath"                        'EXCEL���`�t�@�C���p�X
    Private Const INIITEM2_OUTXLSPATH As String = "OutXLSPath"                          'EXCEL�o�͐�t�H���_�p�X

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

    '�Ɩ��h�c
    Public Const GYOMU_H01 As String = "H01"           '����
    Public Const GYOMU_H10 As String = "H10"           '����
    Public Const GYOMU_H03 As String = "H03"           '�ϑ�����
    Public Const GYOMU_H04 As String = "H04"           '����
    Public Const GYOMU_H05 As String = "H05"           '����
    Public Const GYOMU_H06 As String = "H06"           '�d��
    Public Const GYOMU_H07 As String = "H07"           '�x��
    Public Const GYOMU_G01 As String = "G01"           '����
    Public Const GYOMU_M01 As String = "M01"           '�}�X�^�ێ�


    '-------------------------------------------------------------------------------
    '�����o�[�ϐ��錾
    '-------------------------------------------------------------------------------
    Private Shared _assembly As System.Reflection.Assembly          '�A�Z���u��(�A�v���P�[�V�������)
    Private Shared _msgHd As UtilMsgHandler                         '���b�Z�[�W�n���h��
    Private Shared _db As UtilDBIf                                  '�c�Ǝ� DB�n���h��
    Private Shared _debugMode As Boolean                            '�f�o�b�O���[�h(���O���x����DEBUG�̏ꍇ��True)

    'Ini�t�@�C���i�[�ϐ�
    Public Shared _iniVal As iniType

    '�ėp�}�X�^�i�[�ϐ�
    Private Shared _hanyou_tb As Hashtable = New Hashtable

    '�o�b�N�A�b�v�T�[�o�ڑ�����p�@True:���o�b�N�A�b�v�T�[�o�ڑ������@False:�o�b�N�A�b�v�T�[�o���ڑ�
    Private Shared _BackUpServer As Boolean = False

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
    '�o�b�N�A�b�v�T�[�o�ڑ���
    Shared ReadOnly Property BackUpServer() As Boolean
        Get
            Return _BackUpServer
        End Get
    End Property
    '�o�b�N�A�b�v�T�[�o�ڑ���(�t�H�[���^�C�g���\���p�j
    Shared ReadOnly Property BackUpServerPrint() As String
        Get
            If _BackUpServer Then
                Return CommonConst.BACKUPSERVER
            Else
                Return ""
            End If
        End Get
    End Property

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
            'UtilPostgresDebugger�̓C���X�^���X�𐶐�����ƁADB�R�l�N�V�����܂Ő������Ă����
            Dim _db As UtilDBIf
            Try
                _db = New UtilPostgresDebugger(_iniVal.SVAddr, _iniVal.PortNo, _iniVal.DBName, _iniVal.UserId, _iniVal.Password, _iniVal.LogFilePath, _debugMode)
            Catch ex As Exception
                '�m�F���b�Z�[�W��\������
                Dim piRtn As Integer
                piRtn = _msgHd.dspMSG("NonPriDb")  '�T�[�o�ɐڑ��ł��܂���B�o�b�N�A�b�v�T�[�o�ɐڑ����܂����H
                If piRtn = vbNo Then
                    '�E[������]�I���̏ꍇ	
                    '	�V�X�e�����I�����܂��B
                    GC.KeepAlive(m)
                    Exit Sub
                End If

                'DB�ڑ����s�@�o�b�N�A�b�v�T�[�o�֐ڑ�
                Try
                    _db = New UtilPostgresDebugger(_iniVal.SVAddr_stby, _iniVal.PortNo_stby, _iniVal.DBName_stby, _iniVal.UserId_stby, _iniVal.Password_stby, _iniVal.LogFilePath, _debugMode)
                    _BackUpServer = True
                Catch eex As Exception
                    piRtn = _msgHd.dspMSG("NonBackDb")  '�T�[�o�ɐڑ��ł��܂���B
                    GC.KeepAlive(m)
                    Exit Sub
                End Try

            End Try


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
                    openForm = New frmC01F10_Login(_msgHd, _db)

                    '�t�H�[���I�[�v��
                    Application.Run(openForm)
                    '���O�C����ʃI�[�v��
                    'openForm.ShowDialog()

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
                If _iniVal.LogFilePath IsNot Nothing AndAlso
                   (Not _iniVal.LogFilePath.StartsWith("..") And _iniVal.LogFilePath.StartsWith(".")) Then
                    _iniVal.LogFilePath = Mid(_iniVal.LogFilePath, 2)
                End If
                If Not _iniVal.LogFilePath.StartsWith("\") Then
                    _iniVal.LogFilePath = "\" & _iniVal.LogFilePath
                End If
                _iniVal.LogFilePath = UtilClass.getAppPath(_assembly) & _iniVal.LogFilePath

                '�c�a
                errXMLstrkey = "NonDbIniKey"
                _iniVal.SVAddr = ini.getIni(INIITEM1_DB, INIITEM2_SVADDR)
                errXMLstrkey = "NonDbIniKey"
                _iniVal.PortNo = ini.getIni(INIITEM1_DB, INIITEM2_PORTNO)
                errXMLstrkey = "NonDbIniKey"
                _iniVal.DBName = ini.getIni(INIITEM1_DB, INIITEM2_DBNAME)
                errXMLstrkey = "NonDbIniKey"
                _iniVal.UserId = ini.getIni(INIITEM1_DB, INIITEM2_USERID)
                errXMLstrkey = "NonDbIniKey"
                _iniVal.Password = ini.getIni(INIITEM1_DB, INIITEM2_PASSWORD)
                errXMLstrkey = "NonDbIniKey"
                _iniVal.SVAddr_stby = ini.getIni(INIITEM1_DB_STBY, INIITEM2_SVADDR_STBY)
                errXMLstrkey = "NonDbIniKey"
                _iniVal.PortNo_stby = ini.getIni(INIITEM1_DB_STBY, INIITEM2_PORTNO_STBY)
                errXMLstrkey = "NonDbIniKey"
                _iniVal.DBName_stby = ini.getIni(INIITEM1_DB_STBY, INIITEM2_DBNAME_STBY)
                errXMLstrkey = "NonDbIniKey"
                _iniVal.UserId_stby = ini.getIni(INIITEM1_DB_STBY, INIITEM2_USERID_STBY)
                errXMLstrkey = "NonDbIniKey"
                _iniVal.Password_stby = ini.getIni(INIITEM1_DB_STBY, INIITEM2_PASSWORD_STBY)

                '�V�X�e���K����
                errXMLstrkey = "NonProductInfoIniKey"
                _iniVal.SystemCaption = ini.getIni(INIITEM1_PRODUCT_INFO, INIITEM2_SYSTEMCAPTION)
                errXMLstrkey = "NonProductInfoIniKey"
                _iniVal.GamenSelUpperCnt = ini.getIni(INIITEM1_PRODUCT_INFO, INIITEM2_GAMENSELUPPERCNT)
                errXMLstrkey = "NonProductInfoIniKey"
                _iniVal.PrintSelUpperCnt = ini.getIni(INIITEM1_PRODUCT_INFO, INIITEM2_PRINTSELUPPERCNT)

                'EXCEl���`�t�H���_�p�X
                errXMLstrkey = "NonXlsBaseDir"                'EXCEL���`�t�@�C���̊i�[�ꏊ�̎擾�Ɏ��s���܂����B
                _iniVal.BaseXlsPath = ini.getIni(INIITEM1_EXCEL, INIITEM2_BASEXLSPATH)
                If _iniVal.BaseXlsPath IsNot Nothing AndAlso
                   (Not _iniVal.BaseXlsPath.StartsWith("..") And _iniVal.BaseXlsPath.StartsWith(".")) Then
                    _iniVal.BaseXlsPath = Mid(_iniVal.BaseXlsPath, 2)
                End If
                If Not _iniVal.BaseXlsPath.StartsWith("\") Then
                    _iniVal.BaseXlsPath = "\" & _iniVal.BaseXlsPath
                End If
                _iniVal.BaseXlsPath = UtilClass.getAppPath(_assembly) & _iniVal.BaseXlsPath

                'EXCEl�o�͐�t�H���_�t�@�C��
                errXMLstrkey = "NonXlsOutDir"                'EXCEL�o�̓t�@�C���̊i�[�ꏊ�̎擾�Ɏ��s���܂����B
                _iniVal.OutXlsPath = ini.getIni(INIITEM1_EXCEL, INIITEM2_OUTXLSPATH)
                If _iniVal.OutXlsPath IsNot Nothing AndAlso
                   (Not _iniVal.OutXlsPath.StartsWith("..") And _iniVal.OutXlsPath.StartsWith(".")) Then
                    _iniVal.OutXlsPath = Mid(_iniVal.OutXlsPath, 2)
                End If
                If Not _iniVal.OutXlsPath.StartsWith("\") Then
                    _iniVal.OutXlsPath = "\" & _iniVal.OutXlsPath
                End If
                _iniVal.OutXlsPath = UtilClass.getAppPath(_assembly) & _iniVal.OutXlsPath

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
