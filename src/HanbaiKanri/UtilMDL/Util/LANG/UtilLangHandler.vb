Imports System.Xml

Namespace LANG
    '===============================================================================
    '
    '  ���[�e�B���e�B�N���X
    '    �i�N���X���j    UtilMsgHandler
    '    �i�����@�\���j      xml��`�̃��b�Z�[�W�{�b�N�X��\������
    '    �i�{MDL�g�p�O��j   UtilMsgVO����荞�܂�Ă��邱��
    '    �i���l�j            Message.xml�`����`�t�@�C����z��
    '
    '===============================================================================
    '  ����  ���O          ��  �t      �}�[�N      ���e
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2006/04/17             �V�K
    '-------------------------------------------------------------------------------
    Public Class UtilLangHandler

        '===============================================================================
        '�����o�[�ϐ���`
        '===============================================================================
        Private _xmlDoc As XmlDocument

        '===============================================================================
        '�v���p�e�B(�A�N�Z�T)
        '===============================================================================
        Public ReadOnly Property xmlDoc() As XmlDocument
            'Geter--------
            Get
                xmlDoc = _xmlDoc
            End Get
            'Setter-------
            '�Ȃ�
        End Property

        '===============================================================================
        ' �R���X�g���N�^
        '   �����̓p�����^   �FprmFileName    �t���p�X���b�Z�[�W�t�@�C����
        '===============================================================================
        ''' <summary>
        ''' �R���X�g���N�^
        ''' </summary>
        ''' <param name="prmFileName">�t���p�X���b�Z�[�W�t�@�C����</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal prmFileName As String)
            Try
                _xmlDoc = New XmlDocument()  '_xmlDocument�I�u�W�F�N�g���쐬    
                _xmlDoc.Load(prmFileName)
            Catch ex As XmlException
                Dim lex As UsrDefException = New UsrDefException("�����`�t�@�C���Ǎ��G���[" & ControlChars.NewLine &
                                                     "�����`�t�@�C���̑��݁E�p�X���m�F���Ă��������B")
                Debug.WriteLine(lex.Message)
                Throw lex
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   ���b�Z�[�W�擾
        '   �i�����T�v�j�ʒm���ꂽ���b�Z�[�WID�ɑΉ�����MSG��ҏW���ĕԋp����
        '   �����̓p�����^   �FprmMsgId         ���b�Z�[�WID
        '                   �FprmOptionalMsg   �ǉ����b�Z�[�W
        '   �����\�b�h�߂�l �F�������ꂽ���b�Z�[�W�r�[��(ValueObject)
        '   ��������O       �FException,UsrDefException
        '                                               2006.05.07 Created By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' ���b�Z�[�W�擾 �ʒm���ꂽ���b�Z�[�WID�ɑΉ�����MSG��ҏW���ĕԋp����
        ''' </summary>
        ''' <param name="prmLangId">���b�Z�[�WID</param>
        ''' <returns>�������ꂽ���b�Z�[�W�r�[��(ValueObject)</returns>
        ''' <remarks>������O       �FException,UsrDefException</remarks>
        Public Function getLANG(ByVal prmLangText As String, ByVal prmLangId As String) As String
            Try
                Dim langDef As XmlElement = _xmlDoc.DocumentElement
                Dim elemList As XmlNodeList = langDef.GetElementsByTagName("LANG_DATA")
                Dim i As Integer
                For i = 0 To elemList.Count - 1
                    If elemList.ItemOf(i).Item("ID").InnerText = prmLangText Then
                        '���b�Z�[�WID����v����Ȃ�
                        Dim textWk As String = "err"
                        textWk = elemList.ItemOf(i).Item(prmLangId).InnerText

                        'MSG�\��
                        Return textWk
                    End If
                Next
                Return prmLangText
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Debug.WriteLine(ex.StackTrace)
                Throw ex
            End Try

        End Function
    End Class
End Namespace
