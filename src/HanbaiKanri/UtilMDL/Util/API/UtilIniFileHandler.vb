Imports System.Text


Namespace API
    '===============================================================================
    '
    '  ���[�e�B���e�B�N���X
    '    �i�N���X���j    UtilIniFileHandler
    '    �i�����@�\���j      Ini�t�@�C���̍��ڒl��ǂݍ���
    '    �i�{MDL�g�p�O��j   ���ɂȂ�
    '    �i���l�j            API(Kernel32.GetPrivateProfileStringA)�g�p�̂���
    '                       �ȉ��Ɏ���ini�t�@�C���`���ɑ��鎖
    '                           [�Z�N�V������]
    '                           �L�[�� = �ݒ�l
    '
    '===============================================================================
    '  ����  ���O          ��  �t      �}�[�N      ���e
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2006/04/09             �V�K
    '-------------------------------------------------------------------------------
    Public Class UtilIniFileHandler

        '===============================================================================
        'API��`
        '===============================================================================
        <System.Security.SuppressUnmanagedCodeSecurity()>
        Private Declare Function GetPrivateProfileString Lib "KERNEL32.DLL" Alias "GetPrivateProfileStringA" (
            ByVal lpAppName As String,
            ByVal lpKeyName As String, ByVal lpDefault As String,
            ByVal lpReturnedString As StringBuilder, ByVal nSize As Integer,
            ByVal lpFileName As String) As Integer

        '===============================================================================
        '�����o�[�ϐ���`
        '===============================================================================
        Private _iniFilePath As String

        '===============================================================================
        '�v���p�e�B(�A�N�Z�T)
        '===============================================================================
        ''' <summary>
        ''' �n���h�����Ă���Ini�t�@�C����
        ''' </summary>
        ''' <value>Ini�t�@�C����</value>
        ''' <returns>Ini�t�@�C����</returns>
        ''' <remarks></remarks>
        Public Property fileName() As String
            'Geter--------
            Get
                fileName = _iniFilePath
            End Get
            'Setter-------
            Set(ByVal Value As String)
                _iniFilePath = Value
            End Set
        End Property

        '===============================================================================
        ' �R���X�g���N�^
        '   �����̓p�����^   �FprmFileName    �t���p�XIni�t�@�C����
        '===============================================================================
        ''' <summary>
        ''' Ini�t�@�C���n���h���𐶐�����
        ''' </summary>
        ''' <param name="prmFileName">�ΏۂƂ���Ini�t�@�C����(�t���p�X)</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal prmFileName As String)
            _iniFilePath = prmFileName '�����o�[��Ini�t�@�C������ݒ�
        End Sub

        '-------------------------------------------------------------------------------
        '   ���ڎ擾
        '   �i�����T�v�j�ʒm���ꂽ�Z�N�V������/���ږ��ɑΉ������ݒ�l���擾����
        '   �����̓p�����^   �FsAppName    �Z�N�V������
        '                   �FsKeyName    ���ږ�
        '   �����\�b�h�߂�l �F�擾�l
        '   ��������O       �FException,UsrDefException
        '                                               2006.04.09 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' ���ڎ擾
        ''' </summary>
        ''' <param name="prmAppName">�Z�N�V������</param>
        ''' <param name="prmKeyName">���ږ�</param>
        ''' <returns>�擾�l</returns>
        ''' <remarks>�i�����T�v�j�ʒm���ꂽ�Z�N�V������/���ږ��ɑΉ������ݒ�l���擾����</remarks>
        Public Function getIni(ByVal prmAppName As String, ByVal prmKeyName As String) As String

            Dim sb As StringBuilder
            sb = New StringBuilder(1024)
            Const DEFAULTVALUE As String = "Initial File Read Error"
            Dim rtnCode As Integer
            Dim rtnStr As String = ""
            Dim ini As String = _iniFilePath
            'API�R�[��
            rtnCode = GetPrivateProfileString(prmAppName, prmKeyName, DEFAULTVALUE, sb, sb.Capacity, ini)
            rtnStr = sb.ToString()                                                 '�Ǎ����e�擾

            If rtnCode < 1 Or rtnStr = DEFAULTVALUE Then                          '�Ǎ����e�`�F�b�N
                Dim tex As UsrDefException = New UsrDefException("INI�t�@�C���ǂݍ��݃G���[" & ControlChars.NewLine & _
                                                     "Ini�t�@�C���E�Z�N�V�����E�L�[�̑��݂��m�F���Ă��������B")
                Debug.WriteLine(tex.Message)
                Throw tex
            Else
                Return rtnStr                                                      '�Ǎ����e�ԋp
            End If

        End Function
    End Class
End Namespace