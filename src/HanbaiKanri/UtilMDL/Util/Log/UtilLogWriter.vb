Imports System.IO
Imports System.Text


Namespace Log
    '===============================================================================
    '
    '  ���[�e�B���e�B�N���X
    '    �i�N���X���j    UtilLogWrier
    '    �i�����@�\���j      ���O�o�͋@�\��񋟂���
    '    �i�{MDL�g�p�O��j   ���ɂȂ�
    '    �i���l�j            consoleWrite()��False�ɐݒ肷��ƃR���\�[���o�͂��Ȃ��Ȃ�
    '
    '===============================================================================
    '  ����  ���O          ��  �t      �}�[�N      ���e
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2006/04/17             �V�K
    '-------------------------------------------------------------------------------
    Public Class UtilLogWriter

        '===============================================================================
        '�����o�[�ϐ���`
        '===============================================================================
        Private _fileNm As String           '���O�t�@�C����
        Private _consoleWrite As Boolean    '�R���\�[���o�͂��邩�ǂ���

        '===============================================================================
        '�v���p�e�B(�A�N�Z�T)
        '===============================================================================
        Public ReadOnly Property fileNm() As String
            'Geter--------
            Get
                fileNm = _fileNm
            End Get
            'Setter-------
            '�Ȃ�
        End Property
        Public Property consoleWrite() As Boolean
            'Geter--------
            Get
                consoleWrite = _consoleWrite
            End Get
            'Setter-------
            Set(ByVal Value As Boolean)
                Dim wkVal As String
                If Value Then
                    wkVal = "�R���\�[���o�͂��J�n���܂��B"
                Else
                    wkVal = "�R���\�[���o�͂��~���܂��B"
                End If
                _consoleWrite = True
                writeLine(wkVal)
                _consoleWrite = Value
            End Set
        End Property

        '===============================================================================
        ' �R���X�g���N�^
        '   �����̓p�����^   �F  prmFileNm           Log�t�@�C����(�t���p�X)
        '                       <prmConsoleWrite>   �R���\�[���o�͂��邩�ǂ���
        '===============================================================================
        ''' <summary>
        ''' �R���X�g���N�^
        ''' </summary>
        ''' <param name="prmFileNm">Log�t�@�C����(�t���p�X)</param>
        ''' <param name="prmConsoleWrite">�R���\�[���o�͂��邩�ǂ���</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal prmFileNm As String, Optional ByVal prmConsoleWrite As Boolean = False)
            _fileNm = prmFileNm
            _consoleWrite = prmConsoleWrite
        End Sub

        '-------------------------------------------------------------------------------
        '   ���O�o��
        '   �i�����T�v�j�w�肳�ꂽ����������O�o�͂���
        '               �����F�P�s��     YYYY/MM/DD HH:MM:DD   �G���[�R�[�h���G���[���b�Z�[�W
        '   �@�@�@            �Q�s��     �� SQL���Ȃǒǉ����b�Z�[�W(�w�莞�̂ݏo��)
        '   �����̓p�����^  �FprmOutPut      �o�̓��O
        '                   �F<prmOutPut2>   �o�̓��O�Q(SQL���Ȃǂ�z��)�@���s��ɏo��
        '   �����\�b�h�߂�l �F�Ȃ�
        '   ��������O       �FException
        '                                               2006.04.17 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' ���O�o�� �w�肳�ꂽ����������O�o�͂���
        ''' </summary>
        ''' <param name="prmOutPut">YYYY/MM/DD HH:MM:DD   �G���[�R�[�h���G���[���b�Z�[�W</param>
        ''' <param name="prmOutPut2">SQL���Ȃǒǉ����b�Z�[�W(�w�莞�̂ݏo��)</param>
        ''' <remarks></remarks>
        Public Sub writeLine(ByVal prmOutPut As String, _
                             Optional ByVal prmOutPut2 As String = "")
            Dim log As StreamWriter
            Dim outStr As String
            Try
                '���O�t�@�C���I�[�v��
                log = New StreamWriter(_fileNm, True, Encoding.UTF8)

                Try
                    '������ҏW
                    outStr = Now.ToString("G") & Space(3) & prmOutPut

                    '�o��
                    log.WriteLine(outStr)
                    Debug.WriteLine(outStr)
                    If _consoleWrite Then
                        Console.WriteLine(outStr) '�R���\�[���o��
                    End If

                    '�I�v�V�����p�����^���ݒ肳��Ă���ꍇ�͂�������o��
                    If (Not prmOutPut2.Equals("")) Then
                        outStr = prmOutPut2
                        log.WriteLine(outStr)
                        Debug.WriteLine(outStr)
                        If _consoleWrite Then
                            Console.WriteLine(outStr) '�R���\�[���o��
                        End If
                    End If

                Catch ex As Exception
                    Throw ex
                Finally
                    '�t�@�C���N���[�Y
                    log.Close()
                End Try

            Catch ex2 As Exception
                Debug.WriteLine(ex2.Message)
                Debug.WriteLine(ex2.StackTrace)
                Throw ex2
            End Try

        End Sub
    End Class

End Namespace
