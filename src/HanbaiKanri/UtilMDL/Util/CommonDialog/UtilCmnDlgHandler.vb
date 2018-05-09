Namespace CommonDialog
    '===============================================================================
    '
    ' ���[�e�B���e�B���W���[��
    '   �i���W���[�����j   UtilCmnDlgHandler
    '   �i�����@�\���j     �R�����_�C�A���O����@�\��񋟂���
    '   �i�{MDL�g�p�O��j  ���ɖ���
    '   �i���l�j           
    '
    '===============================================================================
    ' ����  ���O          ��  �t      �}�[�N      ���e
    '-------------------------------------------------------------------------------
    ' (1)   ����   �@     2006/05/11              �V�K
    '-------------------------------------------------------------------------------
    Public Class UtilCmnDlgHandler

        '===============================================================================
        '�ϐ���`
        '===============================================================================

        '===============================================================================
        '�萔��`
        '===============================================================================
        '�R�����_�C�A���O�t�B���^�[�萔
        Public Const TXT As String = "�e�L�X�g�t�@�C��(*.txt)" & "|" & "*.txt"  '�e�L�X�g�t�@�C��
        Public Const CSV As String = "CSV�t�@�C��(*.csv)" & "|" & "*.csv"       '�b�r�u�t�@�C��
        Public Const DAT As String = "�f�[�^�t�@�C��(*.dat)" & "|" & "*.dat"    '�f�[�^�t�@�C��
        Public Const ALL As String = "�S�Ẵt�@�C��(*.*)" & "|" & "*.*"        '�S�Ẵt�@�C��

        '�R�����_�C�A���O�����\���f�B���N�g��
        Private Const sDEFALT_DIR As String = "C:\"

        '-------------------------------------------------------------------------------
        '�@ �u�t�@�C����I���v�R�����_�C�A���O�\��
        '  �i�����T�v�j�u�t�@�C����I���v�R�����_�C�A���O��\�����A�I�����ꂽ�t�@�C���̃t���p�X��Ԃ�
        '  �����̓p�����^�Fi <prmDir>       �_�C�A���O�����\���f�B���N�g��
        '                �Fi <prmFileNm>    �_�C�A���O�����\���t�@�C����
        '                �Fi <prmFillter>   �_�C�A���O�\���t�B���^�[
        '                                      TXT/CSV/DAT/ALL                       
        '                �Fi <prmTitle>     �^�C�g��
        '                �Fi <prmMultiSel>  �����I����
        '  ���֐��߂�l�@�F�I�����ꂽ�t�@�C���̃t���p�X(�����I������̏ꍇ�́u,�v�ŘA�����ĕԋp/�L�����Z������""��ԋp)
        '  �����̑��F�ďo�� Call UtilCmnDlgHandler.openFileDialog("d:\work", "tmp.txt", UtilCmnDlgHandler.TXT & "|" & UtilCmnDlgHandler.ALL)
        '            prmDir�D��x
        '                      prmDir > �J�����gDir > "C:\"
        '                                              2006.05.11 Createed By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �u�t�@�C����I���v�R�����_�C�A���O�\��
        ''' </summary>
        ''' <param name="prmDir">�_�C�A���O�����\���f�B���N�g��</param>
        ''' <param name="prmFileNm">�_�C�A���O�����\���t�@�C����</param>
        ''' <param name="prmFillter">�_�C�A���O�\���t�B���^�[ TXT/CSV/DAT/ALL </param>
        ''' <param name="prmTitle">�^�C�g��</param>
        ''' <param name="prmMultiSel">�����I����</param>
        ''' <returns>�I�����ꂽ�t�@�C���̃t���p�X(�����I������̏ꍇ�́u,�v�ŘA�����ĕԋp/�L�����Z������""��ԋp)</returns>
        ''' <remarks></remarks>
        Public Shared Function openFileDialog(Optional ByVal prmDir As String = "", _
                                          Optional ByVal prmFileNm As String = "", _
                                          Optional ByVal prmFillter As String = ALL, _
                                          Optional ByVal prmTitle As String = "�t�@�C����I��", _
                                          Optional ByVal prmMultiSel As Boolean = False) As String

            Try
                '�����\���̃p�X�擾
                Dim initialDir As String       '�����\������f�B���N�g��
                If prmDir.Equals("") Then
                    initialDir = sDEFALT_DIR       '�����l
                Else
                    initialDir = prmDir                '�����l
                End If

                'OpenFileDialog �̐V�����C���X�^���X�𐶐����� 
                Dim fd As OpenFileDialog = New OpenFileDialog()
                Try
                    With fd
                        .Title = prmTitle               '�_�C�A���O�̃^�C�g����ݒ肷��
                        .InitialDirectory = initialDir  '�����\������f�B���N�g����ݒ肷��
                        .FileName = prmFileNm           '�����\������t�@�C������ݒ肷��
                        .Filter = prmFillter            '�t�@�C���̃t�B���^��ݒ肷��
                        .RestoreDirectory = True        '�_�C�A���O�{�b�N�X�����O�ɃJ�����g�f�B���N�g���𕜌�
                        .Multiselect = prmMultiSel      '�����̃t�@�C����I���\�ɂ���
                        .CheckFileExists = True         '�t�@�C���̑��݃`�F�b�N���s��
                        If .ShowDialog() = DialogResult.OK Then
                            If Not prmMultiSel Then     '[OK]�������̓t�@�C������ݒ�
                                Return .FileName
                            Else
                                '�����I������̏ꍇ�́u,�v�ҏW
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
                            Return ""                   '[�L�����Z��]�������͋󕶎���ݒ�
                        End If
                    End With
                Catch le As Exception
                    Throw le
                Finally
                    fd.Dispose()                        '�_�C�A���O�̔j��
                End Try
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        '-------------------------------------------------------------------------------
        '�@ �u���O�����ĕۑ��v�R�����_�C�A���O�\��
        '  �i�����T�v�j�u���O�����ĕۑ��v�R�����_�C�A���O��\�����A�I�����ꂽ�t�@�C���̃t���p�X��Ԃ�
        '  �����̓p�����^�Fi <prmDir>       �_�C�A���O�����\���f�B���N�g��
        '                �Fi <prmFileNm>    �_�C�A���O�����\���t�@�C����
        '                �Fi <prmFillter>   �_�C�A���O�\���t�B���^�[
        '                                      TXT/CSV/DAT/ALL                       
        '                �Fi <prmTitle>     �^�C�g��
        '  ���֐��߂�l�@�F�I�����ꂽ�t�@�C���̃t���p�X(�L�����Z������""��ԋp)
        '  �����̑��F�ďo�� Call UtilCmnDlgHandler.saveFileDialog("d:\work", "tmp.txt", UtilCmnDlgHandler.TXT & "|" & UtilCmnDlgHandler.ALL)
        '            prmDir�D��x
        '                      prmDir > �J�����gDir > "C:\"
        '                                              2006.05.11 Createed By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �u���O�����ĕۑ��v�R�����_�C�A���O�\��
        ''' </summary>
        ''' <param name="prmDir">�_�C�A���O�����\���f�B���N�g��</param>
        ''' <param name="prmFileNm">�_�C�A���O�����\���t�@�C����</param>
        ''' <param name="prmFillter">�_�C�A���O�\���t�B���^�[ TXT/CSV/DAT/ALL</param>
        ''' <param name="prmTitle">�^�C�g��</param>
        ''' <returns>�I�����ꂽ�t�@�C���̃t���p�X(�L�����Z������""��ԋp)</returns>
        ''' <remarks></remarks>
        Public Shared Function saveFileDialog(Optional ByVal prmDir As String = "", _
                                          Optional ByVal prmFileNm As String = "", _
                                          Optional ByVal prmFillter As String = ALL, _
                                          Optional ByVal prmTitle As String = "���O�����ĕۑ�") As String

            Try
                '�����\���̃p�X�擾
                Dim initialDir As String       '�����\������f�B���N�g��
                If prmDir.Equals("") Then
                    initialDir = sDEFALT_DIR       '�����l
                Else
                    initialDir = prmDir                '�����l
                End If

                'SaveFileDialog�̐V�����C���X�^���X�𐶐����� 
                Dim fd As SaveFileDialog = New SaveFileDialog()
                Try
                    With fd
                        .Title = prmTitle               '�_�C�A���O�̃^�C�g����ݒ肷��
                        .InitialDirectory = initialDir  '�����\������f�B���N�g����ݒ肷��
                        .FileName = prmFileNm           '�����\������t�@�C������ݒ肷��
                        .Filter = prmFillter            '�t�@�C���̃t�B���^��ݒ肷��
                        .RestoreDirectory = True        '�_�C�A���O�{�b�N�X�����O�ɃJ�����g�f�B���N�g���𕜌�
                        .CheckFileExists = False        '�t�@�C���̑��݃`�F�b�N���s��Ȃ�
                        If .ShowDialog() = DialogResult.OK Then
                            Return .FileName            '[OK]�������̓t�@�C������ݒ�
                        Else
                            Return ""                   '[�L�����Z��]�������͋󕶎���ݒ�
                        End If
                    End With
                Catch le As Exception
                    Throw le
                Finally
                    fd.Dispose()                        '�_�C�A���O�̔j��
                End Try
            Catch ex As Exception
                Throw ex
            End Try
        End Function

    End Class
End Namespace