Namespace Text
    '===============================================================================
    '
    '  ���[�e�B���e�B�N���X
    '    �i�N���X���j    UtilTextFunc
    '    �i�����@�\���j      �O��I/F�t�@�C���ɂ����ă��[�e�B���e�B�ƂȂ郁�\�b�h���
    '    �i�{MDL�g�p�O��j   ���ɖ���
    '    �i���l�j            
    '
    '===============================================================================
    '  ����  ���O          ��  �t      �}�[�N      ���e
    '-------------------------------------------------------------------------------
    '  (1)   Jun.Takagi    2006/05/14              �V�K
    '-------------------------------------------------------------------------------
    Public Class UtilTextFunc

        '-------------------------------------------------------------------------------
        '�@CSV�`��������̓ǂݍ���
        '   �i�����T�v�jCSV�`���̕����񂩂�e�t�B�[���h��String�I�u�W�F�N�g�̔z��Ƃ��Đ�����ԋp����
        '   �����̓p�����^�FprmSource       �����Ώە�����
        '                   <prmSeparator>  ��؂蕶��(�ȗ����F,)
        '                   <prmEncloser>   ���蕶��(�ȗ����F")
        '   ���֐��߂�l�@�F�������String�z��
        '                                               2006.05.14 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' CSV�`��������̓ǂݍ��� CSV�`���̕����񂩂�e�t�B�[���h��String�I�u�W�F�N�g�̔z��Ƃ��Đ�����ԋp����
        ''' </summary>
        ''' <param name="prmSource">�����Ώە�����</param>
        ''' <param name="prmSeparator">��؂蕶��(�ȗ����F,)</param>
        ''' <param name="prmEncloser">���蕶��(�ȗ����F")</param>
        ''' <returns>�������String�z��</returns>
        ''' <remarks></remarks>
        Public Shared Function splitCSV(ByVal prmSource As String, Optional ByVal prmSeparator As Char = ",", Optional ByVal prmEncloser As Char = """") As String()
            Dim ret() As String : Erase ret
            Dim onData As Boolean
            For index As Integer = 0 To prmSource.Length - 1
                '1�������o
                Dim tgt As String = prmSource.Substring(index, 1)

                '���蕶�����ǂ������f
                If prmEncloser.ToString.Equals(tgt) Then
                    If onData Then
                        onData = False
                    Else
                        onData = True
                    End If
                    Continue For '���蕶���̏ꍇ�͎��̕�����
                End If

                '��؂蕶�����ǂ������f
                If prmSeparator.ToString.Equals(tgt) Then
                    '���蕶���̒����ǂ������f
                    If onData Then
                        '���蕶���̒��Ȃ̂ł��̂܂܊i�[
                        ret(UBound(ret)) = ret(UBound(ret)) & tgt
                    Else
                        '���蕶���̊O�Ȃ̂Ŕz��g��
                        If ret Is Nothing Then
                            ReDim ret(0) : ret(0) = ""
                        Else
                            ReDim Preserve ret(UBound(ret) + 1)
                            ret(UBound(ret)) = "" '������
                        End If
                    End If
                    Continue For '��؂蕶���̏ꍇ�͎��̕�����
                End If

                '�ʏ핶���͔z��Ɋi�[
                If ret Is Nothing Then ReDim ret(0) : ret(0) = ""
                ret(UBound(ret)) = ret(UBound(ret)) & tgt
            Next

            Return ret
        End Function

        '-------------------------------------------------------------------------------
        '�@�Œ蒷������`���̓ǂݍ���
        '   �i�����T�v�j�Œ蒷������`���̃f�[�^���e�t�B�[���h�ɕ�����String�I�u�W�F�N�g�̔z��Ƃ��ĕԋp����
        '   �����̓p�����^�FprmSource         �����Ώە�����
        '                   prmSeparateCount  �e�t�B�[���h��Byte��
        '   ���֐��߂�l�@�F�������String�z��
        '   ���g�p��@�@�@�F     Dim rtn() as String
        '                        Dim sep() As Short = New Short() {3, 3, 4}
        '                        rtn = TextFunc.splitFixedString("12345�U890", sep)
        '   �����l  �@�@�@�F��؂�ʒu��2Byte���������݂����ꍇ�A�Y���������X�y�[�X�u�����ĕԋp
        '                                               2006.05.14 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �Œ蒷������`���̓ǂݍ��� �Œ蒷������`���̃f�[�^���e�t�B�[���h�ɕ�����String�I�u�W�F�N�g�̔z��Ƃ��ĕԋp����
        ''' </summary>
        ''' <param name="prmSource">�����Ώە�����</param>
        ''' <param name="prmSeparateCount">�e�t�B�[���h��Byte��</param>
        ''' <returns>�������String�z��</returns>
        ''' <remarks></remarks>
        Public Shared Function splitFixedString(ByVal prmSource As String, ByVal prmSeparateCount() As Short) As String()
            Dim retAry() As String
            Dim cnt As Short = UBound(prmSeparateCount)     '�t�B�[���h�̐����擾
            Erase retAry
            For i As Short = 0 To cnt                       '�t�B�[���h�̐�������indexLoop
                Dim CharCnt As Short = prmSeparateCount(i)  '�Ώۃt�B�[���h��Byte�����擾
                Dim ret As String = ""                      '�ԋp�p�t�B�[���h�̃o�b�t�@��������
                Dim wkLen As Short = prmSource.Length       '��̕�����̖����܂ł̕��������擾(�Ƃ肠�����Ō�܂ł�ݒ�)
                For j As Short = 0 To wkLen
                    '1���������₵�Ă����A�؂�o��Byte�ʒu�ɂȂ������𔻒�
                    If System.Text.Encoding.GetEncoding("shift_jis").GetByteCount(prmSource.Substring(0, j)) = CharCnt Then
                        '�؂�o��Byte���擾
                        ret = prmSource.Substring(0, j)
                        prmSource = prmSource.Substring(j)  '�؂�o�������ȍ~���Ċi�[
                        Exit For
                    ElseIf System.Text.Encoding.GetEncoding("shift_jis").GetByteCount(prmSource.Substring(0, j)) > CharCnt Then
                        '�؂�o��Byte�������傫���̂ŁA1Byte�폜���ăX�y�[�X�Œ���(��؂�Byte�ʒu��2Byte���������݂���P�[�X)
                        ret = prmSource.Substring(0, j - 1) & " "
                        prmSource = " " & prmSource.Substring(j) '�؂�o�������ȍ~���Ċi�[
                        Exit For
                    Else
                        ret = prmSource.Substring(0, j)
                    End If
                Next j
                If retAry Is Nothing Then
                    ReDim retAry(0)
                Else
                    ReDim Preserve retAry(UBound(retAry) + 1)
                End If
                retAry(UBound(retAry)) = ret
            Next i
            Return retAry
        End Function

    End Class
End Namespace
