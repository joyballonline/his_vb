Imports System.IO
Imports System.Text


Namespace Text
    '===============================================================================
    '
    '  ���[�e�B���e�B�N���X
    '    �i�N���X���j    UtilTextWriter
    '    �i�����@�\���j      �e�L�X�g�t�@�C�����������ދ@�\���
    '    �i�{MDL�g�p�O��j   ���ɖ���
    '    �i���l�j            
    '
    '===============================================================================
    '  ����  ���O          ��  �t      �}�[�N      ���e
    '-------------------------------------------------------------------------------
    '  (1)   Jun.Takagi    2006/05/14              �V�K
    '-------------------------------------------------------------------------------
    Public Class UtilTextWriter

        '===============================================================================
        '�����o�[�ϐ���`
        '===============================================================================
        Private _fileName As String         '�t�@�C����
        Private _sWriter As StreamWriter    '�X�g���[�����C�^�[
        Private _openFlg As Boolean = False


        '===============================================================================
        '�v���p�e�B(�A�N�Z�T)
        '===============================================================================
        Public ReadOnly Property isFileOpen() As Boolean
            Get
                Return _openFlg
            End Get
        End Property

        '===============================================================================
        ' �R���X�g���N�^
        '   �����̓p�����^   �FprmFileName    ����Ώۃt�@�C����
        '   �����l           �F�t�@�C�������݂��Ȃ��ꍇ�̓I�[�v�������^�C�~���O�Ńt�@�C�����쐬����B
        '===============================================================================
        ''' <summary>
        ''' �R���X�g���N�^ �t�@�C�������݂��Ȃ��ꍇ�̓I�[�v�������^�C�~���O�Ńt�@�C�����쐬����B
        ''' </summary>
        ''' <param name="prmFileName"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal prmFileName As String)
            _fileName = prmFileName
        End Sub

        '===============================================================================
        ' �f�X�g���N�^
        '   �����̓p�����^   �F�Ȃ�
        '===============================================================================
        ''' <summary>
        ''' �f�X�g���N�^
        ''' </summary>
        ''' <remarks></remarks>
        Protected Overrides Sub Finalize()
            Try
                If _sWriter IsNot Nothing And _openFlg Then
                    _sWriter.Close()
                End If
            Catch lex As Exception
            Finally
                If _sWriter IsNot Nothing Then
                    Call _sWriter.Dispose()
                End If
                _sWriter = Nothing
                Call MyBase.Finalize()
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   �t�@�C���I�[�v��
        '   �i�����T�v�j�Ώۃt�@�C�����J��
        '   �����̓p�����^   �FprmAppendFlg   �ǉ��������݂��邩�ǂ����̃t���O(False�̏ꍇ�A�㏑������)
        '   �����\�b�h�߂�l �F�Ȃ�
        '   �����l           �Fopen���\�b�h�̌ďo����͕K��close���\�b�h�̌Ăяo����ۏႷ�邱��
        '                                               2006.05.14 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �I�[�v�� �I�[�v��������K��close���\�b�h�̌ďo��ۏႷ�邱��
        ''' </summary>
        ''' <param name="prmAppendFlg">�ǉ��������݂��邩�ǂ����̃t���O(False�̏ꍇ�A�㏑������)</param>
        ''' <remarks></remarks>
        Public Sub open(Optional ByVal prmAppendFlg As Boolean = True)
            If _openFlg Then
                Throw New UsrDefException("�t�@�C���͊��ɊJ���Ă��܂��B")
            End If
            _sWriter = New StreamWriter(_fileName, prmAppendFlg, Encoding.Default)
            _openFlg = True
        End Sub

        '-------------------------------------------------------------------------------
        '   �t�@�C���N���[�Y
        '   �i�����T�v�j�Ώۃt�@�C�������
        '   �����̓p�����^   �F�Ȃ�
        '   �����\�b�h�߂�l �F�Ȃ�
        '   �����l           �Fopen���\�b�h�̌ďo����͕K��close���\�b�h�̌Ăяo����ۏႷ�邱��
        '                                               2006.05.14 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �N���[�Y
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub close()
            If Not _openFlg Then
                Throw New UsrDefException("�t�@�C�������Ă��܂��B")
            End If
            _sWriter.Close()
            _openFlg = False
        End Sub

        '-------------------------------------------------------------------------------
        '   ��������
        '   �i�����T�v�j�w�肳�ꂽ���������������
        '   �����̓p�����^   �FprmStr   �������ݕ�����
        '   �����\�b�h�߂�l �F�Ȃ�
        '                                               2006.05.14 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �w�肳�ꂽ���������������
        ''' </summary>
        ''' <param name="prmStr">�������ݕ�����</param>
        ''' <remarks></remarks>
        Public Sub write(ByVal prmStr As String)
            If Not _openFlg Then
                Throw New UsrDefException("�t�@�C�������Ă��܂��B")
            End If
            _sWriter.Write(prmStr)
        End Sub
        'Object�^���I�[�o�[���[�h 2010.11.14 Jun.Takagi
        Public Sub write(ByVal prmStr As Object)
            If Not _openFlg Then
                Throw New UsrDefException("�t�@�C�������Ă��܂��B")
            End If
            _sWriter.Write(prmStr)
        End Sub

        '-------------------------------------------------------------------------------
        '   ��������
        '   �i�����T�v�j�w�肳�ꂽ��������������݁A�Ō�ɉ��s���o�͂���
        '   �����̓p�����^   �FprmStr   �������ݕ�����
        '   �����\�b�h�߂�l �F�Ȃ�
        '                                               2006.05.14 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' �w�肳�ꂽ��������������݁A�Ō�ɉ��s���o�͂���
        ''' </summary>
        ''' <param name="prmStr">�������ݕ�����</param>
        ''' <remarks></remarks>
        Public Sub writeLine(ByVal prmStr As String)
            If Not _openFlg Then
                Throw New UsrDefException("�t�@�C�������Ă��܂��B")
            End If
            _sWriter.WriteLine(prmStr)
        End Sub

        '-------------------------------------------------------------------------------
        '   ���s
        '   �i�����T�v�j���s���o�͂���
        '   �����̓p�����^   �F�Ȃ�
        '   �����\�b�h�߂�l �F�Ȃ�
        '                                               2006.05.14 Created By Jun.Takagi
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' ���s
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub newLine()
            If Not _openFlg Then
                Throw New UsrDefException("�t�@�C�������Ă��܂��B")
            End If
            _sWriter.WriteLine()
        End Sub

    End Class
End Namespace
