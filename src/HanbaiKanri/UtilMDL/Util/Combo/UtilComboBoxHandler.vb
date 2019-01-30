Imports System.Windows.Forms
Namespace Combo
    '===============================================================================
    '
    '  ���[�e�B���e�B�N���X
    '    �i�N���X���j    UtilComboBoxHandler
    '    �i�����@�\���j      �R���{�{�b�N�X�̐���@�\���
    '    �i�{MDL�g�p�O��j   ���ɂȂ�
    '    �i���l�j            
    '
    '===============================================================================
    '  ����  ���O          ��  �t      �}�[�N      ���e
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2006/04/22             �V�K
    '  (2)   Laevigata, Inc.    2006/06/15             getRelationObj��ǉ�
    '-------------------------------------------------------------------------------
    Public Class UtilComboBoxHandler

        '===============================================================================
        '�����o�[�ϐ���`
        '===============================================================================
        Private _target As ComboBox     '�ΏۃR���{�{�b�N�X

        '===============================================================================
        ' �R���X�g���N�^
        '   �����̓p�����^   �FprmTarget    �ΏۃR���{�{�b�N�X
        '===============================================================================
        ''' <summary>
        ''' �R���{�{�b�N�X�n���h���̃C���X�^���X�𐶐�����
        ''' </summary>
        ''' <param name="prmTarget">����ΏۂƂȂ�R���{�{�b�N�X</param>
        ''' <remarks></remarks>
        Public Sub New(ByRef prmTarget As ComboBox)
            If prmTarget Is Nothing Then
                Throw (New UsrDefException("�R���{�{�b�N�X�̃C���X�^���X���ݒ肳��Ă��܂���"))
            End If
            _target = prmTarget '�����o�[�փR���{�{�b�N�X��ݒ�
        End Sub

        '-------------------------------------------------------------------------------
        '   �`���~
        '   �i�����T�v�j�R���{�{�b�N�X���ڒǉ����̏�����������ړI�Ƃ��A���ڒǉ��O�ɌĂяo��
        '   �����̓p�����^   �F�Ȃ�
        '   �����\�b�h�߂�l �F�Ȃ�
        '   ��������O       �F�Ȃ�
        '                                               2006.04.22 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �`���~ �R���{�{�b�N�X���ڒǉ����̏�����������ړI�Ƃ��A���ڒǉ��O�ɌĂяo��
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub beginUpdate()
            _target.BeginUpdate()
        End Sub

        '-------------------------------------------------------------------------------
        '   �`��J�n
        '   �i�����T�v�j�R���{�{�b�N�X���ڒǉ����̏�����������ړI�Ƃ��A���ڒǉ���ɌĂяo��
        '   �����̓p�����^   �F�Ȃ�
        '   �����\�b�h�߂�l �F�Ȃ�
        '   ��������O       �F�Ȃ�
        '                                               2006.04.22 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �`��J�n �R���{�{�b�N�X���ڒǉ����̏�����������ړI�Ƃ��A���ڒǉ���ɌĂяo��
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub endUpdate()
            _target.EndUpdate()
        End Sub

        '-------------------------------------------------------------------------------
        '   ���ڒǉ�
        '   �i�����T�v�j�R���{�{�b�N�X���ڒǉ�
        '   �����̓p�����^   �FprmData   UtilCboVO�̃C���X�^���X
        '   �����\�b�h�߂�l �F�Ȃ�
        '   ��������O       �FUsrDefException
        '                                               2006.04.22 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' ���ڒǉ� �R���{�{�b�N�X���ڒǉ�
        ''' </summary>
        ''' <param name="prmData">UtilCboVO�̃C���X�^���X</param>
        ''' <remarks></remarks>
        Public Sub addItem(ByRef prmData As UtilCboVO)
            If prmData Is Nothing Then
                Throw (New UsrDefException("UtilCboVO�̃C���X�^���X���ݒ肳��Ă��܂���"))
            End If
            Call _target.Items.Add(prmData)
        End Sub

        '-------------------------------------------------------------------------------
        '   ���ڑI��
        '   �i�����T�v�j�R���{�{�b�N�X�̍��ڂ�I��������
        '   �����̓p�����^   �FprmCode   �R���{�{�b�N�X�Ɋi�[����Ă���f�[�^�̓��A�I�������������ڂ̃R�[�h
        '   �����\�b�h�߂�l �F�Ȃ�
        '   ��������O       �F�Ȃ�
        '   �����l           �F�n���ꂽ�R�[�h��������Ȃ��ꍇ�A���I���Ƃ���
        '                                               2006.04.22 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' ���ڑI�� �R���{�{�b�N�X�̍��ڂ�I��������
        ''' </summary>
        ''' <param name="prmCode">�R���{�{�b�N�X�Ɋi�[����Ă���f�[�^�̓��A�I�������������ڂ̃R�[�h</param>
        ''' <remarks>�n���ꂽ�R�[�h��������Ȃ��ꍇ�A���I���Ƃ���</remarks>
        Public Sub selectItem(ByVal prmCode As String)

            Dim i As Short
            Dim hitFlg As Boolean
            For i = 0 To _target.Items.Count - 1
                If _target.Items.Item(i).code.Equals(prmCode) Then
                    hitFlg = True
                    _target.SelectedIndex = i
                    Exit For
                End If
            Next
            If Not hitFlg Then
                _target.SelectedIndex = -1
            End If

        End Sub

        '-------------------------------------------------------------------------------
        '   �\�����擾
        '   �i�����T�v�j���ݑI������Ă��鍀�ڂ̕\�����̂��擾����
        '   �����̓p�����^   �F�Ȃ�
        '   �����\�b�h�߂�l �F�I��l(�\������)
        '   ��������O       �F�Ȃ�
        '   �����l           �F���I���̂΂����A""��ԋp
        '                                               2006.04.22 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �\�����擾 ���ݑI������Ă��鍀�ڂ̕\�����̂��擾����
        ''' </summary>
        ''' <returns>�I��l(�\������)</returns>
        ''' <remarks>���I���̂΂����A""��ԋp</remarks>
        Public Function getName() As String
            getName = ""
            Try
                getName = _target.Items.Item(_target.SelectedIndex).name()
            Catch ex As Exception
            End Try
            Return getName
        End Function

        '-------------------------------------------------------------------------------
        '   �R�[�h�擾
        '   �i�����T�v�j���ݑI������Ă��鍀�ڂ̍���(�R�[�h)���擾����
        '   �����̓p�����^   �F�Ȃ�
        '   �����\�b�h�߂�l �F�I��l(�R�[�h)
        '   ��������O       �F�Ȃ�
        '   �����l           �F���I���̂΂����A""��ԋp
        '                                               2006.04.22 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �R�[�h�擾 ���ݑI������Ă��鍀�ڂ̍���(�R�[�h)���擾����
        ''' </summary>
        ''' <returns>�I��l(�R�[�h)</returns>
        ''' <remarks>���I���̂΂����A""��ԋp</remarks>
        Public Function getCode() As String
            getCode = ""
            Try
                getCode = _target.Items.Item(_target.SelectedIndex).code()
            Catch ex As Exception
            End Try
            Return getCode
        End Function

        '-------------------------------------------------------------------------------
        '   �֘A�t���I�u�W�F�N�g�擾
        '   �i�����T�v�j���ݑI������Ă��鍀�ڂ̊֘A�t���I�u�W�F�N�g���擾����
        '   �����̓p�����^   �F�Ȃ�
        '   �����\�b�h�߂�l �F�I��l(�֘A�t���I�u�W�F�N�g)
        '   �����l           �F���I���̂΂����ANothing��ԋp
        '                                               2006.06.15 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �R�[�h�擾 ���ݑI������Ă��鍀�ڂ̍���(�R�[�h)���擾����
        ''' </summary>
        ''' <returns>�I��l(�R�[�h)</returns>
        ''' <remarks>���I���̂΂����A""��ԋp</remarks>
        Public Function getRelationObj() As Object
            getRelationObj = Nothing
            Try
                getRelationObj = _target.Items.Item(_target.SelectedIndex).obj
            Catch ex As Exception
            End Try
            Return getRelationObj
        End Function

    End Class

    '===============================================================================
    '
    '  ���[�e�B���e�B�N���X
    '    �i�N���X���j    UtilCboVO
    '    �i�����@�\���j      UtilComboBoxHandler�ɓn���R���{�{�b�N�X�f�[�^�̘g���(Beans)
    '    �i�{MDL�g�p�O��j   UtilComboBoxHandler�Ƒ΂Ŏg�p����
    '    �i���l�j            ��L�g�p�O����UtilComboBoxHandler�Ɠ���SRC��ɒ�`
    '
    '===============================================================================
    '  ����  ���O          ��  �t      �}�[�N      ���e
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2006/04/22             �V�K
    '  (2)   Laevigata, Inc.    2006/06/15             getRelationObj�p�Ƀ����o�[��ǉ�
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' UtilComboBoxHandler�ɓn���R���{�{�b�N�X�f�[�^�̘g���(Beans)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class UtilCboVO
        '===============================================================================
        '�����o�[�ϐ���`
        '===============================================================================
        Private _code As String
        Private _name As String
        Private _obj As Object  '2006.06.15 add by Laevigata, Inc.

        '===============================================================================
        '�v���p�e�B(�A�N�Z�T)
        '===============================================================================
        ''' <summary>
        ''' �R���{�{�b�N�X�̊e�s�ɐݒ肳���R�[�h
        ''' </summary>
        ''' <value>�R�[�h</value>
        ''' <returns>�R�[�h</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property code() As String
            'Geter--------
            Get
                code = _code
            End Get
        End Property
        ''' <summary>
        ''' �R���{�{�b�N�X�̊e�s�ɐݒ肳���\������
        ''' </summary>
        ''' <value>�\������</value>
        ''' <returns>�\������</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property name() As String
            'Geter--------
            Get
                name = _name
            End Get
        End Property

        '-->2006.06.15 add start by Laevigata, Inc.
        ''' <summary>
        ''' �R���{�{�b�N�X�̊e�s�ɐݒ肳���֘A�t���I�u�W�F�N�g
        ''' </summary>
        ''' <value>�֘A�t���I�u�W�F�N�g</value>
        ''' <returns>�֘A�t���I�u�W�F�N�g</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property obj() As Object
            'Geter--------
            Get
                obj = _obj
            End Get
        End Property

        '===============================================================================
        ' �R���X�g���N�^
        '   �����̓p�����^  �FprmCode           ���̃C���X�^���X���\�����ڂ̃R�[�h
        '                   �FprmName           ���̃C���X�^���X���\�����ڂ̕\������
        '                   �FprmRelationObj    ���̃C���X�^���X���\�����ڂ̊֘A�t���I�u�W�F�N�g
        '===============================================================================
        ''' <summary>
        ''' �R���{�{�b�N�X�n���h���ւ̎󂯓n���f�[�^���C���X�^���X������
        ''' </summary>
        ''' <param name="prmCode">�R�[�h</param>
        ''' <param name="prmName">����</param>
        ''' <param name="prmRelationObj">�֘A�t���I�u�W�F�N�g</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal prmCode As String, ByVal prmName As String, ByVal prmRelationObj As Object)
            Me.New(prmCode, prmName)
            _obj = prmRelationObj
        End Sub
        '<--2006.06.15 add end by Laevigata, Inc.

        '===============================================================================
        ' �R���X�g���N�^
        '   �����̓p�����^   �FprmCode    ���̃C���X�^���X���\�����ڂ̃R�[�h
        '                   �FprmName    ���̃C���X�^���X���\�����ڂ̕\������
        '===============================================================================
        ''' <summary>
        ''' �R���{�{�b�N�X�n���h���ւ̎󂯓n���f�[�^���C���X�^���X������
        ''' </summary>
        ''' <param name="prmCode">�R�[�h</param>
        ''' <param name="prmName">����</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal prmCode As String, ByVal prmName As String)
            _code = prmCode
            _name = prmName
        End Sub

        '===============================================================================
        ' �I�[�o�[���C�h���\�b�h
        '   �i�����T�v�j�R���{�{�b�N�X�ɕ\������J�������w��
        '===============================================================================
        ''' <summary>
        ''' �R���{�{�b�N�X�ɕ\�����镶���������킷
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides Function ToString() As String
            ToString = _name '�\�����̂�ԋp
        End Function

    End Class
End Namespace