'===================================================================================
'�@ �i�V�X�e�����j      �݌Ɍv��V�X�e��
'
'   �i�@�\���j          ���ʃC���^�[�t�F�[�X�i�t���i�j
'   �i�N���X���j        IfRturnUpDateData
'   �i�����@�\���j      
'   �i���l�j            �e��ʂƎq��ʊԂ̒l�n���̂��߂̃C���^�[�t�F�[�X
'
'===================================================================================
' ����  ���O                ���t        �}�[�N      ���e
'-----------------------------------------------------------------------------------
'  (1)  ���{     �@�@       2010/10/22              �V�K
'�@(2)  ���V                2010/11/17              �ύX(ZG620E�y��ZG621E�̍��ځu�[���v�폜�Ή�)    
'  (3)  ���V                2010/12/02              �ύX(�ΏۊO���e��ʂɔ��f����Ȃ��o�O�C��)    
'-----------------------------------------------------------------------------------

Public Interface IfRturnUpDateData

    '�C��������z�f�[�^��n��
    '2010/12/02 update start Nakazawa---
    '2010/11/17 update start Nakazawa
    'Sub setUpDateData(ByVal prmKibou As String, ByVal prmNouki As String, ByVal prmTehaiSuuryou As String, _
    '                 ByVal prmTantyou As String, ByVal prmJousuu As String, ByVal prmSiyousyoNo As String)
    'Sub setUpDateData(ByVal prmKibou As String, ByVal prmTehaiSuuryou As String, ByVal prmTantyou As String, _
    '                                                    ByVal prmJousuu As String, ByVal prmSiyousyoNo As String)
    '2010/11/17 update end Nakazawa
    Sub setUpDateData(ByVal prmKibou As String, ByVal prmTehaiSuuryou As String, ByVal prmTantyou As String, _
                        ByVal prmJousuu As String, ByVal prmSiyousyoNo As String, ByVal prmTaisyogaiFlg As Boolean)
    '2010/12/02 update end Nakazawa---

    Sub myActivate()

    Sub myShow()

End Interface
