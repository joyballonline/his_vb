Imports System.IO
Imports System.Text


Namespace Text

    '===============================================================================
    '
    '  ���[�e�B���e�B�N���X
    '    �i�N���X���j    UtilTextReader
    '    �i�����@�\���j      �e�L�X�g�t�@�C����ǂݍ��ދ@�\���
    '    �i�{MDL�g�p�O��j   ���ɖ���
    '    �i���l�j            
    '
    '===============================================================================
    '  ����  ���O          ��  �t      �}�[�N      ���e
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2006/05/14              �V�K
    '-------------------------------------------------------------------------------
    Public Class UtilTextReader

        '===============================================================================
        '�����o�[�ϐ���`
        '===============================================================================
        Private _fileName As String         '�t�@�C����
        Private _sReader As StreamReader    '�X�g���[�����[�_�[
        Private _openFlg As Boolean = False

        '===============================================================================
        '�v���p�e�B(�A�N�Z�T)
        '===============================================================================
        Public ReadOnly Property EOF() As Boolean
            Get
                Dim ret As Boolean
                If _sReader.Peek = -1 Then
                    ret = True
                Else
                    ret = False
                End If
                Return ret
            End Get
        End Property
        Public ReadOnly Property isFileOpen() As Boolean
            Get
                Return _openFlg
            End Get
        End Property

        '===============================================================================
        ' �R���X�g���N�^
        '   �����̓p�����^   �F  prmFileName    ����Ώۃt�@�C����
        '===============================================================================
        ''' <summary> 
        ''' �R���X�g���N�^
        ''' </summary>
        ''' <param name="prmFileName">�t�@�C����</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal prmFileName As String)
            _fileName = prmFileName
        End Sub

        '===============================================================================
        ' �f�X�g���N�^
        '   �����̓p�����^   �F  �Ȃ�
        '===============================================================================
        ''' <summary>
        ''' �f�X�g���N�^
        ''' </summary>
        ''' <remarks></remarks>
        Protected Overrides Sub Finalize()

            Try
                If _sReader IsNot Nothing And _openFlg Then
                    _sReader.Close()
                End If
            Catch lex As Exception
            Finally
                If _sReader IsNot Nothing Then
                    Call _sReader.Dispose()
                End If
                _sReader = Nothing
                MyBase.Finalize()
            End Try
        End Sub


        '-------------------------------------------------------------------------------
        '   �t�@�C���I�[�v��
        '   �i�����T�v�j�Ώۃt�@�C�����J��
        '   �����̓p�����^   �F�Ȃ�
        '   �����\�b�h�߂�l �F�Ȃ�
        '   �����l           �Fopen���\�b�h�̌ďo����͕K��close���\�b�h�̌Ăяo����ۏႷ�邱��
        '                                               2006.05.14 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �t�@�C���I�[�v���@�I�[�v��������K��close���\�b�h�̌ďo��ۏႷ�邱��
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub open()
            If _openFlg Then
                Throw New UsrDefException("�t�@�C���͊��ɊJ���Ă��܂��B")
            End If
            _sReader = New StreamReader(_fileName, Encoding.Default)
            _openFlg = True
        End Sub

        '-------------------------------------------------------------------------------
        '   �t�@�C���N���[�Y
        '   �i�����T�v�j�Ώۃt�@�C�������
        '   �����̓p�����^   �F�Ȃ�
        '   �����\�b�h�߂�l �F�Ȃ�
        '   �����l           �Fopen���\�b�h�̌ďo����͕K��close���\�b�h�̌Ăяo����ۏႷ�邱��
        '                                               2006.05.14 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �t�@�C���N���[�Y
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub close()
            If Not _openFlg Then
                Throw New UsrDefException("�t�@�C�������Ă��܂��B")
            End If
            _sReader.Close()
            _openFlg = False
        End Sub

        '-------------------------------------------------------------------------------
        '   ���[�h���C��
        '   �i�����T�v�j�J�����g�s�̕������ǂݍ���
        '   �����̓p�����^   �F�Ȃ�
        '   �����\�b�h�߂�l �F�ǂݍ��ݕ�����
        '                                               2006.05.14 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        '���[�h���C��
        ''' <summary>
        ''' ���[�h���C��
        ''' </summary>
        ''' <returns>�ǂݍ��ݕ�����</returns>
        ''' <remarks></remarks>
        Public Function readLine() As String
            If Not _openFlg Then
                Throw New UsrDefException("�t�@�C�������Ă��܂��B")
            End If
            Dim retLine As String = _sReader.ReadLine
            If retLine IsNot Nothing Then
                Return retLine
            Else
                Return ""
            End If
        End Function

        '-------------------------------------------------------------------------------
        '   �S�ǂݍ���
        '   �i�����T�v�j�J�����g�s�ȍ~�̑S�������ǂݍ���
        '   �����̓p�����^   �F�Ȃ�
        '   �����\�b�h�߂�l �F�ǂݍ��ݕ�����
        '                                               2006.05.14 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �S�ǂݍ���(�J�����g�s�ȍ~)
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function readToEnd() As String
            If Not _openFlg Then
                Throw New UsrDefException("�t�@�C�������Ă��܂��B")
            End If
            Return _sReader.ReadToEnd
        End Function

    End Class
End Namespace
