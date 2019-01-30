Imports System.IO

Namespace FileDirectory
    '===============================================================================
    '
    '  ���[�e�B���e�B�N���X
    '    �i�N���X���j    UtilFile
    '    �i�����@�\���j      �t�@�C������@�\��񋟂���
    '    �i�{MDL�g�p�O��j   ���ɖ���
    '    �i���l�j            
    '
    '===============================================================================
    '  ����  ���O          ��  �t      �}�[�N      ���e
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2006/05/15              �V�K
    '  (2)   Laevigata, Inc. 2010/03/31              �ǉ��@getWriteTimeStamp
    '-------------------------------------------------------------------------------
    Public Class UtilFile

        '===============================================================================
        '�����o�[�萔��`
        '===============================================================================
        '�t�@�C���T�C�Y�񋓌^
        Public Enum sizeTypeEnum
            B = 1
            KB = 2
            MB = 3
            GB = 4
            TB = 5
        End Enum

        '===============================================================================
        '�����o�[�ϐ���`
        '===============================================================================
        Private _sizeType As sizeTypeEnum

        '===============================================================================
        '�v���p�e�B(�A�N�Z�T)
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
        ' �R���X�g���N�^
        '===============================================================================
        ''' <summary>
        ''' �R���X�g���N�^
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            _sizeType = UtilFile.sizeTypeEnum.KB    '�f�t�H���g�ݒ�
        End Sub

        '-------------------------------------------------------------------------------
        '   �t�@�C���̑��݃`�F�b�N
        '   �i�����T�v�j�t�@�C�������݂��邩�ǂ����̃`�F�b�N���s��
        '   �����̓p�����^   �FprmFile  �t�@�C����
        '   �����\�b�h�߂�l �F����/�񑶍�
        '                                               2006.05.15 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �t�@�C�������݂��邩�ǂ����̃`�F�b�N���s��
        ''' </summary>
        ''' <param name="prmFile">�t�@�C����</param>
        ''' <returns>����/�񑶍�</returns>
        ''' <remarks></remarks>
        Public Function isFileExists(ByVal prmFile As String) As Boolean
            Return File.Exists(prmFile)
        End Function

        '-------------------------------------------------------------------------------
        '   �t�@�C���폜
        '   �i�����T�v�j�w�肵���t�@�C�����폜����
        '   �����̓p�����^   �FprmFile  �t�@�C����
        '   �����\�b�h�߂�l �F�Ȃ�
        '                                               2006.05.15 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �w�肵���t�@�C�����폜����
        ''' </summary>
        ''' <param name="prmFile">�t�@�C����</param>
        ''' <remarks></remarks>
        Public Sub delete(ByVal prmFile As String)
            File.Delete(prmFile)
        End Sub

        '-------------------------------------------------------------------------------
        '   �t�@�C���R�s�[
        '   �i�����T�v�j�w�肵���t�@�C�����R�s�[����
        '   �����̓p�����^   �FprmSourceFile    �R�s�[���t�@�C����
        '                    �FprmDestFile      �R�s�[��t�@�C����
        '                    �FprmOverWrite     �㏑���m�F
        '   �����\�b�h�߂�l �F�Ȃ�
        '                                               2006.05.15 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �w�肵���t�@�C�����R�s�[����
        ''' </summary>
        ''' <param name="prmSourceFile">�R�s�[���t�@�C����</param>
        ''' <param name="prmDestFile">�R�s�[��t�@�C����</param>
        ''' <param name="prmOverWrite">�㏑���m�F</param>
        ''' <remarks></remarks>
        Public Sub copy(ByVal prmSourceFile As String, ByVal prmDestFile As String, Optional ByVal prmOverWrite As Boolean = False)
            File.Copy(prmSourceFile, prmDestFile, prmOverWrite)
        End Sub

        '-------------------------------------------------------------------------------
        '   �t�@�C���ړ�
        '   �i�����T�v�j�w�肵���t�@�C�����ړ�����
        '   �����̓p�����^   �FprmSourceFile    �ړ����t�@�C����
        '                    �FprmDestFile      �ړ���t�@�C����
        '   �����\�b�h�߂�l �F�Ȃ�
        '   �����l           �F�����f�B���N�g�����w�肵���ꍇ��ReName�ƂȂ�
        '                                               2006.05.15 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �w�肵���t�@�C�����ړ����� �������f�B���N�g�����w�肵���ꍇ�AReName�ƂȂ�
        ''' </summary>
        ''' <param name="prmSourceFile">�ړ����t�@�C����</param>
        ''' <param name="prmDestFile">�ړ���t�@�C����</param>
        ''' <remarks></remarks>
        Public Sub move(ByVal prmSourceFile As String, ByVal prmDestFile As String)
            File.Move(prmSourceFile, prmDestFile)
        End Sub

        '-------------------------------------------------------------------------------
        '   �^�C���X�^���v�擾
        '   �i�����T�v�j�w�肵���t�@�C���̃^�C���X�^���v���擾����
        '   �����̓p�����^   �FprmFile    �t�@�C����
        '   �����\�b�h�߂�l �F�^�C���X�^���v
        '                                               2006.05.15 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �w�肵���t�@�C���̃^�C���X�^���v���擾����
        ''' </summary>
        ''' <param name="prmFile">�t�@�C����</param>
        ''' <returns>�^�C���X�^���v</returns>
        ''' <remarks></remarks>
        Public Function getTimeStamp(ByVal prmFile As String) As Date
            Return File.GetCreationTime(prmFile)
        End Function

        '-------------------------------------------------------------------------------
        '   �t�@�C���T�C�Y���擾
        '   �i�����T�v�j�w�肵���t�@�C���̃t�@�C���T�C�Y���擾����
        '   �����̓p�����^   �FprmFile    �t�@�C����
        '   �����\�b�h�߂�l �F�t�@�C���T�C�Y
        '                                               2006.05.15 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �w�肵���t�@�C���̃t�@�C���T�C�Y���擾����
        ''' </summary>
        ''' <param name="prmFile">�t�@�C����</param>
        ''' <returns>�t�@�C���폜</returns>
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
        '   �f�B���N�g��/�t�@�C��������
        '   �i�����T�v�j�w�肳�ꂽ�t���p�X���f�B���N�g���ƃt�@�C�����ɕ�������
        '   �����̓p�����^   �FprmFullPath  �Ώۃt���p�X
        '                    �FprmRefPath   �擾�p�X
        '                    �FprmRefFile   �擾�t�@�C����
        '   �����\�b�h�߂�l �F�t�@�C���T�C�Y
        '                                               2006.05.15 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �w�肳�ꂽ�t���p�X���f�B���N�g���ƃt�@�C�����ɕ�������
        ''' </summary>
        ''' <param name="prmFullPath">�Ώۃt���p�X</param>
        ''' <param name="prmRefPath">�擾�p�X</param>
        ''' <param name="prmRefFile">�擾�t�@�C����</param>
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
        '   �t�@�C���X�V���擾
        '   �i�����T�v�j�w�肵���t�@�C���̍X�V�����擾����
        '   �����̓p�����^   �FprmFile    �t�@�C����
        '   �����\�b�h�߂�l �F�X�V��
        '                                               2010.03.31 Created By sugano
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �w�肵���t�@�C���̍X�V�����擾����
        ''' </summary>
        ''' <param name="prmFile">�t�@�C����</param>
        ''' <returns>�X�V��</returns>
        ''' <remarks></remarks>
        Public Function getWriteTimeStamp(ByVal prmFile As String) As Date
            Return File.GetLastWriteTime(prmFile)
        End Function
    End Class
End Namespace
