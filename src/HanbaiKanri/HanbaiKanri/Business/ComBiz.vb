'===================================================================================
'�@ �i�V�X�e�����j      �݌Ɍv��V�X�e��
'
'   �i�@�\���j          ���ʃr�W�l�X���W�b�N
'   �i�N���X���j        ComBiz
'   �i�����@�\���j      ���ʌn�̃r�W�l�X���W�b�N���i�[����
'   �i���l�j            
'
'===================================================================================
' ����  ���O               ���t         �}�[�N      ���e
'-----------------------------------------------------------------------------------
'  (1)  ���V            2010/09/13                  �V�K
'  (2)  ����            2014/06/04                  �ύX�@�ޗ��[�}�X�^(MPESEKKEI)�e�[�u���ύX�Ή�
'-----------------------------------------------------------------------------------
Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.Combo
Imports UtilMDL.Text
Imports System.Reflection
Imports System.Reflection.Assembly
Imports System.Drawing.Printing
Imports UtilMDL.FileDirectory

Public Class ComBiz
    '-------------------------------------------------------------------------------
    '�����o�[�萔�錾
    '-------------------------------------------------------------------------------
    '�R���{�p
    Public Const CBO_ALLN As String = "���ׂ�"                  '�\��
    Public Const CBO_ALLC_MEISYO As String = "ALL________"      '�R�[�h�i���̃R���{�p�j
    '�R���{�p�i�擪�s���󗓂ɂ���ꍇ�j
    Public Const CBO_BLANC As String = "�@�@�@"                 '�\��
    Public Const CBO_BLANC_MEISYO As String = "BLANC______"     '�R�[�h�i���̃R���{�p�j

    'PG���䕶��
    Private Const N As String = ControlChars.NewLine            '���s����
    Private Const RS As String = "RecSet"                       '���R�[�h�Z�b�g�e�[�u��

    '-------------------------------------------------------------------------------
    '�����o�[�ϐ��錾
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf

    '-------------------------------------------------------------------------------
    '   �R���X�g���N�^
    '   �i�����T�v�j�@�������������s��
    '   �����̓p�����^   �FprmRefDbHd   DB�n���h��
    '                      prmMsgHd     MSG�n���h��
    '   �����\�b�h�߂�l �F�C���X�^���X
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefDbHd As UtilDBIf, ByVal prmMsgHd As UtilMsgHandler)
        _db = prmRefDbHd
        _msgHd = prmMsgHd
    End Sub

    '-------------------------------------------------------------------------------
    '   �t�H�[���^�C�g�� �I�v�V����������擾
    '   �i�����T�v�jSystemInfo���擾��������擾����
    '   �����̓p�����^   �FprmRefDbHd   DB�n���h��
    '                      prmMsgHd     MSG�n���h��
    '   �����\�b�h�߂�l �F�擾������(STRINGVALUE1�`3�𕶎��񌋍�)
    '-------------------------------------------------------------------------------
    Public Shared Function getFormTitleOption(ByRef prmRefDbHd As UtilDBIf, ByVal prmMsgHd As UtilMsgHandler) As String
        Dim ret As String = ""
        Try
            'SystemInfo�擾
            Dim sysinfoRec As UtilDBIf.sysinfoRec = prmRefDbHd.getSystemInfo("FormTitle", "OptionValue")

            '�ԋp�����\�z
            If Not "".Equals(sysinfoRec.stringValue1) Then
                ret = sysinfoRec.stringValue1
            End If
            If Not "".Equals(sysinfoRec.stringValue2) AndAlso "".Equals(ret) Then
                ret = sysinfoRec.stringValue2
            ElseIf Not "".Equals(sysinfoRec.stringValue2) Then
                ret = ret & " : " & sysinfoRec.stringValue2
            End If
            If Not "".Equals(sysinfoRec.stringValue3) AndAlso "".Equals(ret) Then
                ret = sysinfoRec.stringValue3
            ElseIf Not "".Equals(sysinfoRec.stringValue3) Then
                ret = ret & " : " & sysinfoRec.stringValue3
            End If

        Catch ex As Exception
            '��������
        End Try
        Return ret
    End Function

    '-------------------------------------------------------------------------------
    '   DB�T�[�o�V�X�e�����t�擾
    '   �i�����T�v�j�@DB�T�[�o���V�X�e�����t�̎擾���s��
    '   �����̓p�����^   �FprmFormat  ���t����
    '   �����\�b�h�߂�l �F�擾���t
    '-------------------------------------------------------------------------------
    Public Function getSysDate(Optional ByVal prmFormat As String = "yyyy/MM/dd HH:mm:ss") As String
        Dim reccnt As Integer
        Dim ds As DataSet = _db.selectDB("select sysdate dt from dual", RS, reccnt)
        If reccnt <= 0 Then
            Throw New UsrDefException("�V�X�e�����t�̎擾�Ɏ��s���܂����B")
        Else
            Return _db.rmNullDate(ds.Tables(RS).Rows(0)("dt"), prmFormat)
        End If
    End Function

    '-------------------------------------------------------------------------------
    '   �i���f�[�^�擾
    '   �i�����T�v�j�@�ޗ��\�}�X�^���i�����擾���A�ҏW���ĕԂ�
    '   �����̓p�����^      �FprmSiyoCD         �d�l�R�[�h
    '                       �FprmHinsyuCD       �i��R�[�h
    '                       �FprmSensinsuCD     ���S���R�[�h
    '                       �FprmSizeCD         �T�C�Y�R�[�h
    '                       �FprmIroCD          �F�R�[�h
    '   ���o�̓p�����^      �FprmRefHinmei      �ҏW��̕i��
    '                       �FprmRefHinsyuNM    �i�햼
    '                       �FprmRefSizeNM      �T�C�Y��
    '                       �FprmRefIroNM       �F��
    '-------------------------------------------------------------------------------
    Public Sub getHinmeiFromZairyoMst(ByVal prmSiyoCD As String, ByVal prmHinsyuCD As String, ByVal prmSensinsuCD As String, _
                ByVal prmSizeCD As String, ByVal prmIroCD As String, ByRef prmRefHinmei As String, ByRef prmRefHinsyuNM As String, _
                                                            ByRef prmRefSizeNM As String, ByRef prmRefIroNM As String)
        Try

            '�f�[�^�Z�b�g������o�����߂̗�
            Dim MPE_HINSYUMEI As String = "HINSYUMEI"
            Dim MPE_SAIZUMEI As String = "SAIZUMEI"
            Dim MPE_IROMEI As String = "IROMEI"

            '�ޗ��[����i������
            Dim sql As String = ""
            sql = sql & N & " SELECT "
            sql = sql & N & "  HINSYU_MEI " & MPE_HINSYUMEI
            sql = sql & N & " ,SAIZU_MEI " & MPE_SAIZUMEI
            sql = sql & N & " ,IRO_MEI " & MPE_IROMEI
            '2014/06/04 UPD-S Sugano 
            'sql = sql & N & " FROM MPESEKKEI "
            'sql = sql & N & "   WHERE SHIYO = '" & _db.rmSQ(prmSiyoCD) & "' "
            'sql = sql & N & "   AND LPAD(TO_CHAR(HINSYU)  ,3,'0') = '" & _db.rmSQ(prmHinsyuCD) & "' "
            'sql = sql & N & "   AND LPAD(TO_CHAR(SENSHIN)  ,3,'0') = '" & _db.rmSQ(prmSensinsuCD) & "' "
            'sql = sql & N & "   AND LPAD(TO_CHAR(SAIZU)  ,2,'0') = '" & _db.rmSQ(prmSizeCD) & "' "
            'sql = sql & N & "   AND LPAD(TO_CHAR(IRO)  ,3,'0') = '" & _db.rmSQ(prmIroCD) & "' "
            'sql = sql & N & "   AND SEQ_NO = 1 "
            'sql = sql & N & "   AND SEKKEI_HUKA = "
            'sql = sql & N & "           (SELECT MAX(SEKKEI_HUKA) FROM MPESEKKEI "
            'sql = sql & N & "               WHERE SHIYO = '" & _db.rmSQ(prmSiyoCD) & "' "
            'sql = sql & N & "               AND HINSYU = '" & _db.rmSQ(prmHinsyuCD) & "' "
            'sql = sql & N & "               AND SENSHIN = '" & _db.rmSQ(prmSensinsuCD) & "' "
            'sql = sql & N & "               AND SAIZU = '" & _db.rmSQ(prmSizeCD) & "' "
            'sql = sql & N & "               AND IRO = '" & _db.rmSQ(prmIroCD) & "') "
            sql = sql & N & " FROM MPESEKKEI1 "
            sql = sql & N & "   WHERE SHIYO = '" & _db.rmSQ(prmSiyoCD) & "' "
            sql = sql & N & "   AND HINSYU = '" & _db.rmSQ(prmHinsyuCD) & "' "
            sql = sql & N & "   AND SENSHIN = '" & _db.rmSQ(prmSensinsuCD) & "' "
            sql = sql & N & "   AND SAIZU = '" & _db.rmSQ(prmSizeCD) & "' "
            sql = sql & N & "   AND IRO = '" & _db.rmSQ(prmIroCD) & "' "
            sql = sql & N & "   AND SEKKEI_FUKA = 'A'"
            sql = sql & N & "   AND SEKKEI_KAITEI = (SELECT MAX(SEKKEI_KAITEI) FROM MPESEKKEI1 "
            sql = sql & N & "               WHERE SHIYO = '" & _db.rmSQ(prmSiyoCD) & "' "
            sql = sql & N & "               AND HINSYU = '" & _db.rmSQ(prmHinsyuCD) & "' "
            sql = sql & N & "               AND SENSHIN = '" & _db.rmSQ(prmSensinsuCD) & "' "
            sql = sql & N & "               AND SAIZU = '" & _db.rmSQ(prmSizeCD) & "' "
            sql = sql & N & "               AND IRO = '" & _db.rmSQ(prmIroCD) & "' "
            sql = sql & N & "               AND SEKKEI_FUKA = 'A') "
            '2014/06/04 UPD-E Sugano 

            'SQL���s
            Dim recCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, recCnt)

            If recCnt = 0 Then             '���o���R�[�h���P�����Ȃ��ꍇ
                '�󕶎���Ԃ��B�G���[�����͌ďo���ōs���B
                prmRefHinmei = ""
                prmRefHinsyuNM = ""
                prmRefSizeNM = ""
                prmRefIroNM = ""
                Exit Sub
            End If

            '�i����"M="�������A2�����ȏ�̃X�y�[�X��1�����X�y�[�X�ɕϊ�����
            prmRefHinmei = ds.Tables(RS).Rows(0)(MPE_HINSYUMEI) & ds.Tables(RS).Rows(0)(MPE_SAIZUMEI) & _
                                                                        ds.Tables(RS).Rows(0)(MPE_IROMEI)

            prmRefHinmei = deleteSome(prmRefHinmei)

            '���o�f�[�^���ďo���ɕԂ�
            '�i�햼
            prmRefHinsyuNM = ds.Tables(RS).Rows(0)(MPE_HINSYUMEI)
            '�T�C�Y��
            prmRefSizeNM = ds.Tables(RS).Rows(0)(MPE_SAIZUMEI)
            '�F��
            prmRefIroNM = ds.Tables(RS).Rows(0)(MPE_IROMEI)

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��V�X�e���G���[MSG�o�͌�X���[
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   �i���ҏW
    '   �i�����T�v�j�@�n���ꂽ�i���̗]�蕔�����������ĕԋp
    '   �����̓p�����^   �F prmHinmei     �i��  
    '   �����\�b�h�߂�l �F �@�ƇA������̕i��
    '                         �@�uM=�v������
    '                         �A2�����ȏ�̃X�y�[�X��1�����X�y�[�X�ɕϊ�   
    '-------------------------------------------------------------------------------
    Private Function deleteSome(ByVal prmHinmei As String) As String
        Try

            '"M="������
            deleteSome = prmHinmei.Replace("M=", "")
            '������̒����擾
            Dim cnt As Integer
            '�X�y�[�X2�����ȏ���X�y�[�X1�����ɕϊ�
            cnt = deleteSome.Length
            For i As Integer = 1 To cnt - 1
                deleteSome = deleteSome.Replace("  ", " ")
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��V�X�e���G���[MSG�o�͌�X���[
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Function

End Class
