Imports System.IO
Imports System.Text


Namespace Log
    '===============================================================================
    '
    '  ���[�e�B���e�B�N���X
    '    �i�N���X���j    UtilLogDebugger
    '    �i�����@�\���j      ���O�o�͊g���@�\��񋟂���
    '    �i�{MDL�g�p�O��j   UtilLogWriter���v���W�F�N�g�Ɏ�荞�܂�Ă��邱��
    '    �i���l�j            UtilLogWriter���p��
    '
    '===============================================================================
    '  ����  ���O          ��  �t      �}�[�N      ���e
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2006/04/18             �V�K
    '-------------------------------------------------------------------------------
    Public Class UtilLogDebugger
        Inherits UtilLogWriter

        '===============================================================================
        '�����o�[�萔��`
        '===============================================================================
        Public Const LOG_DEBUG As Short = 1   '���O�o�̓^�C�v���f�o�b�O
        Public Const LOG_INFO As Short = 2    '���O�o�̓^�C�v���C���t�H���[�V����
        Public Const LOG_ERR As Short = 3     '���O�o�̓^�C�v���G���[

        '===============================================================================
        '�����o�[�ϐ���`
        '===============================================================================
        Private _debugFlg As Boolean                '�f�o�b�O���[�h

        '===============================================================================
        '�v���p�e�B(�A�N�Z�T)
        '===============================================================================
        Public Property debugFlg() As Boolean
            'Geter--------
            Get
                debugFlg = _debugFlg
            End Get
            'Setter-------
            Set(ByVal Value As Boolean)
                _debugFlg = Value
                Call MyBase.writeLine("�f�o�b�O���[�h��[" & _debugFlg.ToString & "]�ɕύX���܂��B")
            End Set
        End Property

        '===============================================================================
        ' �R���X�g���N�^
        '   �����̓p�����^   �F  prmFileNm           Log�t�@�C����(�t���p�X)
        '                       prmDebugFlg         �f�o�b�O���[�h
        '                       <prmConsoleWrite>   �R���\�[���o�͂��邩�ǂ���
        '===============================================================================
        ''' <summary>
        ''' �R���X�g���N�^
        ''' </summary>
        ''' <param name="prmFileNm">Log�t�@�C����(�t���p�X)</param>
        ''' <param name="prmDebugFlg">�f�o�b�O���[�h</param>
        ''' <param name="prmConsoleWrite">�R���\�[���o�͂��邩�ǂ���</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal prmFileNm As String, _
                       ByVal prmDebugFlg As Boolean, _
                       Optional ByVal prmConsoleWrite As Boolean = True)
            MyBase.New(prmFileNm, prmConsoleWrite)
            _debugFlg = prmDebugFlg
        End Sub

        '-------------------------------------------------------------------------------
        '   �g�����O�o��
        '   �i�����T�v�j���O�o�̓^�C�v���f�o�b�O�̂��̂̓f�o�b�O���[�h=trne�̏ꍇ�̂ݏo�͂���
        '   �����̓p�����^�FiLogType ���O�o�̓^�C�v(LOG_DEBUG/LOG_INFO/LOG_ERR)
        '                 �F�� mybase.writeline�Q��
        '   �����\�b�h�߂�l �F�Ȃ�
        '   ��������O       �FException
        '                                               2006.04.18 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �g�����O�o�� ���O�o�̓^�C�v���f�o�b�O�̂��̂̓f�o�b�O���[�h=trne�̏ꍇ�̂ݏo�͂���
        ''' </summary>
        ''' <param name="prmLogType">���O�o�̓^�C�v(LOG_DEBUG/LOG_INFO/LOG_ERR)</param>
        ''' <param name="prmOutPut">YYYY/MM/DD HH:MM:DD   �G���[�R�[�h���G���[���b�Z�[�W</param>
        ''' <param name="prmOutPut2">SQL���Ȃǒǉ����b�Z�[�W(�w�莞�̂ݏo��)</param>
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