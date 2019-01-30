Imports System.IO

Namespace FileDirectory

    '===============================================================================
    '
    '  ���[�e�B���e�B�N���X
    '    �i�N���X���j    UtilDirectory
    '    �i�����@�\���j      �f�B���N�g������@�\��񋟂���
    '    �i�{MDL�g�p�O��j   ���ɖ���
    '    �i���l�j            
    '
    '===============================================================================
    '  ����  ���O          ��  �t      �}�[�N      ���e
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2006/05/15              �V�K
    '-------------------------------------------------------------------------------
    Public Class UtilDirectory

        '===============================================================================
        '�����o�[�萔��`
        '===============================================================================
        '�Ȃ�

        '===============================================================================
        '�����o�[�ϐ���`
        '===============================================================================
        '�Ȃ�

        '===============================================================================
        '�v���p�e�B(�A�N�Z�T)
        '===============================================================================
        '�Ȃ�

        '===============================================================================
        ' �R���X�g���N�^
        '===============================================================================
        '�Ȃ�


        '-------------------------------------------------------------------------------
        '   �f�B���N�g���̑��݃`�F�b�N
        '   �i�����T�v�j�f�B���N�g�������݂��邩�ǂ����̃`�F�b�N���s��
        '   �����̓p�����^   �FprmDir  �f�B���N�g����
        '   �����\�b�h�߂�l �F����/�񑶍�
        '                                               2006.05.15 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �f�B���N�g���̑��݃`�F�b�N
        ''' </summary>
        ''' <param name="prmDir">�f�B���N�g����</param>
        ''' <returns>����/�񑶍�</returns>
        ''' <remarks></remarks>
        Public Function isDirExists(ByVal prmDir As String) As Boolean
            Return Directory.Exists(prmDir)
        End Function

        '-------------------------------------------------------------------------------
        '   �f�B���N�g���폜
        '   �i�����T�v�j�w�肵���f�B���N�g�����폜����
        '   �����̓p�����^   �FprmDir  �f�B���N�g����
        '   �����\�b�h�߂�l �F�Ȃ�
        '                                               2006.05.15 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �w�肵���f�B���N�g�����폜����
        ''' </summary>
        ''' <param name="prmDir">�f�B���N�g����</param>
        ''' <remarks></remarks>
        Public Sub delete(ByVal prmDir As String)
            Directory.Delete(prmDir, True)
        End Sub

        '-------------------------------------------------------------------------------
        '   �f�B���N�g���쐬
        '   �i�����T�v�j�w�肵���f�B���N�g�����쐬����
        '   �����̓p�����^   �FprmDir  �f�B���N�g����
        '   �����\�b�h�߂�l �F�Ȃ�
        '                                               2006.05.15 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �w�肵���f�B���N�g�����쐬����
        ''' </summary>
        ''' <param name="prmDir">�f�B���N�g����</param>
        ''' <remarks></remarks>
        Public Sub create(ByVal prmDir As String)
            Directory.CreateDirectory(prmDir)
        End Sub

        '-------------------------------------------------------------------------------
        '   �t�@�C�����擾
        '   �i�����T�v�j�w�肵���f�B���N�g���z���̃t�@�C����(����)���擾
        '   �����̓p�����^   �FprmDir  �f�B���N�g����
        '   �����\�b�h�߂�l �F�擾�t�@�C����(�z��)
        '                                               2006.05.15 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �w�肵���f�B���N�g���̃t�@�C����(����)���擾
        ''' </summary>
        ''' <param name="prmDir">�f�B���N�g����</param>
        ''' <returns>�擾�t�@�C����(�z��)</returns>
        ''' <remarks></remarks>
        Public Function getFiles(ByVal prmDir As String) As String()
            Dim strFiles() As String = Directory.GetFiles(prmDir)
            Return strFiles
        End Function

        '-------------------------------------------------------------------------------
        '   �t�H���_���擾
        '   �i�����T�v�j�w�肵���f�B���N�g���z���̃t�H���_��(����)���擾
        '   �����̓p�����^   �FprmDir  �f�B���N�g����
        '   �����\�b�h�߂�l �F�擾�t�H���_��(�z��)
        '                                               2006.05.15 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �w�肵���f�B���N�g���̃t�H���_��(����)���擾
        ''' </summary>
        ''' <param name="prmDir">�f�B���N�g����</param>
        ''' <returns>�擾�t�H���_��(�z��)</returns>
        ''' <remarks></remarks>
        Public Function getDirectories(ByVal prmDir As String) As String()
            Dim strDirectories() As String = Directory.GetDirectories(prmDir)
            Return strDirectories
        End Function

        '-------------------------------------------------------------------------------
        '   �T�u�f�B���N�g���擾
        '   �i�����T�v�j�w�肵���f�B���N�g���z���ɑ��݂���T�u�f�B���N�g����S�Ď擾����
        '   �����̓p�����^   �FprmDir  �f�B���N�g����
        '   �����\�b�h�߂�l �F�擾�f�B���N�g����(�z��)
        '                                               2006.05.15 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �w�肵���f�B���N�g���z���ɑ��݂���T�u�f�B���N�g����S�Ď擾����
        ''' </summary>
        ''' <param name="prmDir">�f�B���N�g����</param>
        ''' <returns>�擾�f�B���N�g����(�z��)</returns>
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
        '���������\�b�h
        Private Sub getSubDirectories(ByVal prmSearchDir As String, ByRef prmFindDirs As ArrayList)
            '�T�u�t�H���_���擾
            Dim dir As String
            For Each dir In Directory.GetDirectories(prmSearchDir)
                prmFindDirs.Add(dir)                    'ArrayList�Ƀt�H���_�ǉ�
                Me.getSubDirectories(dir, prmFindDirs)  '�ċA�Ăяo��
            Next
        End Sub

    End Class
End Namespace
