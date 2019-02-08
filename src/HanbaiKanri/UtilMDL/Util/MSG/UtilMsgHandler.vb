Imports System.Xml
Imports System.ComponentModel
Imports System.Windows.Forms

Namespace MSG
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
    Public Class UtilMsgHandler

        '===============================================================================
        '�����o�[�ϐ���`
        '===============================================================================
        Private _xmlDoc As XmlDocument
        Private _defTitle As String = ""                      'MSGBOX�ւ̃f�t�H���g�p�����^
        Private _defButton As MessageBoxButtons               'MSGBOX�ւ̃f�t�H���g�p�����^
        Private _defIcon As MessageBoxIcon                    'MSGBOX�ւ̃f�t�H���g�p�����^
        Private _defDefaultButton As MessageBoxDefaultButton  'MSGBOX�ւ̃f�t�H���g�p�����^

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
        Public ReadOnly Property defTitle() As String
            'Geter--------
            Get
                defTitle = _defTitle
            End Get
            'Setter-------
            '�Ȃ�
        End Property
        Public ReadOnly Property defButton() As String
            'Geter--------
            Get
                defButton = _defButton
            End Get
            'Setter-------
            '�Ȃ�
        End Property
        Public ReadOnly Property defIcon() As String
            'Geter--------
            Get
                defIcon = _defIcon
            End Get
            'Setter-------
            '�Ȃ�
        End Property
        Public ReadOnly Property defDefaultButton() As String
            'Geter--------
            Get
                defDefaultButton = _defDefaultButton
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
                Dim lex As UsrDefException = New UsrDefException("���b�Z�[�W��`�t�@�C���Ǎ��G���[" & ControlChars.NewLine &
                                                     "���b�Z�[�W��`�t�@�C���̑��݁E�p�X���m�F���Ă��������B")
                Debug.WriteLine(lex.Message)
                Throw lex
            End Try
            Try
                '�f�t�H���g��`�̓Ǎ�
                Dim msgDefault As XmlElement = _xmlDoc.DocumentElement
                Dim elemList As XmlNodeList = msgDefault.GetElementsByTagName("DEFAULT_MSG")
                If elemList.Count <> 1 Then
                    Dim lex As UsrDefException = New UsrDefException("DEFAULT_MSG�̒�`�����݂��Ȃ����A������`����Ă��܂��B")
                    Debug.WriteLine(lex.Message)
                    Throw lex
                End If
                '�^�C�g���ҏW
                Try
                    _defTitle = elemList.ItemOf(0).Item("TITLE").InnerText()
                Catch ex As Exception
                    Dim lex As UsrDefException = New UsrDefException("DEFAULT_MSG��TITLE��`�����݂��܂���B")
                    Debug.WriteLine(lex.Message)
                    Throw lex
                End Try
                '�{�^���ҏW
                Dim button As String
                Try
                    button = elemList.ItemOf(0).Item("BUTTON_TYPE").InnerText
                Catch ex As Exception : button = "" : End Try '�\�L�ȗ���
                Select Case button.ToLower
                    Case "abortretryignore"
                        _defButton = MessageBoxButtons.AbortRetryIgnore
                    Case "ok"
                        _defButton = MessageBoxButtons.OK
                    Case "okcancel"
                        _defButton = MessageBoxButtons.OKCancel
                    Case "retrycancel"
                        _defButton = MessageBoxButtons.RetryCancel
                    Case "yesno"
                        _defButton = MessageBoxButtons.YesNo
                    Case "yesnocancel"
                        _defButton = MessageBoxButtons.YesNoCancel
                    Case ""
                        Dim lex As UsrDefException = New UsrDefException("DEFAULT_MSG��BUTTON_TYPE��`�����݂��܂���B")
                        Debug.WriteLine(lex.Message)
                        Throw lex
                    Case Else
                        Dim lex As UsrDefException = New UsrDefException("DEFAULT_MSG��BUTTON_TYPE��`������Ă��܂��B")
                        Debug.WriteLine(lex.Message)
                        Throw lex
                End Select
                '�A�C�R���ҏW
                Dim icon As String
                Try
                    icon = elemList.ItemOf(0).Item("ICONT_TYPE").InnerText
                Catch ex As Exception : icon = "" : End Try '�\�L�ȗ���
                Select Case icon.ToLower
                    Case "asterisk"
                        _defIcon = MessageBoxIcon.Asterisk
                    Case "error"
                        _defIcon = MessageBoxIcon.Error
                    Case "exclamation"
                        _defIcon = MessageBoxIcon.Exclamation
                    Case "hand"
                        _defIcon = MessageBoxIcon.Hand
                    Case "information"
                        _defIcon = MessageBoxIcon.Information
                    Case "none"
                        _defIcon = MessageBoxIcon.None
                    Case "question"
                        _defIcon = MessageBoxIcon.Question
                    Case "stop"
                        _defIcon = MessageBoxIcon.Stop
                    Case "warning"
                        _defIcon = MessageBoxIcon.Warning
                    Case ""
                        Dim lex As UsrDefException = New UsrDefException("DEFAULT_MSG��ICONT_TYPE��`�����݂��܂���B")
                        Debug.WriteLine(lex.Message)
                        Throw lex
                    Case Else
                        Dim lex As UsrDefException = New UsrDefException("ICONT_TYPE�̒�`������Ă��܂��B")
                        Debug.WriteLine(lex.Message)
                        Throw lex
                End Select
                '�f�t�H���g�{�^���ҏW
                Dim defaultButton As String
                Try
                    defaultButton = elemList.ItemOf(0).Item("DEFAULT_BUTTON").InnerText
                Catch ex As Exception : defaultButton = "" : End Try '�\�L�ȗ���
                Select Case defaultButton.ToLower
                    Case "button1"
                        _defDefaultButton = MessageBoxDefaultButton.Button1
                    Case "button2"
                        _defDefaultButton = MessageBoxDefaultButton.Button2
                    Case "button3"
                        _defDefaultButton = MessageBoxDefaultButton.Button3
                    Case ""
                        Dim lex As UsrDefException = New UsrDefException("DEFAULT_MSG��DEFAULT_BUTTON��`�����݂��܂���B")
                        Debug.WriteLine(lex.Message)
                        Throw lex
                    Case Else
                        Dim lex As UsrDefException = New UsrDefException("DEFAULT_BUTTON�̒�`������Ă��܂��B")
                        Debug.WriteLine(lex.Message)
                        Throw lex
                End Select

            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Debug.WriteLine(ex.StackTrace)
                Throw ex
            End Try
        End Sub

        '-------------------------------------------------------------------------------
        '   ���b�Z�[�W�\��
        '   �i�����T�v�j�ʒm���ꂽ���b�Z�[�WID�ɑΉ�����MSG��ҏW���ĕ\������
        '   �����̓p�����^   �FprmMsgId         ���b�Z�[�WID
        '                   �FprmOptionalMsg   �ǉ����b�Z�[�W
        '   �����\�b�h�߂�l �F�����{�^��(DialogResult)
        '   ��������O       �FException,UsrDefException
        '                                               2006.05.07 Updated By Laevigata, Inc.
        '-------------------------------------------------------------------------------
        ''' <summary>
        ''' ���b�Z�[�W�\�� �ʒm���ꂽ���b�Z�[�WID�ɑΉ�����MSG��ҏW���ĕ\������
        ''' </summary>
        ''' <param name="prmMsgId">���b�Z�[�WID</param>
        ''' <param name="prmOptionalMsg">�ǉ����b�Z�[�W</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function dspMSG(ByVal prmMsgId As String, ByVal prmLang As String, Optional ByVal prmOptionalMsg As String = "") As DialogResult

            Dim mv As UtilMsgVO = Me.getMSG(prmMsgId, prmLang, prmOptionalMsg)
            Return MessageBox.Show(mv.dspStr, mv.title, mv.button, mv.icon, mv.defaultButton)

        End Function

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
        ''' <param name="prmMsgId">���b�Z�[�WID</param>
        ''' <param name="prmOptionalMsg">�ǉ����b�Z�[�W</param>
        ''' <returns>�������ꂽ���b�Z�[�W�r�[��(ValueObject)</returns>
        ''' <remarks>������O       �FException,UsrDefException</remarks>
        Public Function getMSG(ByVal prmMsgId As String, ByVal prmLang As String, Optional ByVal prmOptionalMsg As String = "") As UtilMsgVO
            Try
                Dim msgDef As XmlElement = _xmlDoc.DocumentElement
                Dim elemList As XmlNodeList = msgDef.GetElementsByTagName("MSG_DATA")
                Dim i As Integer
                Dim buttonWk As MessageBoxButtons
                Dim iconWk As MessageBoxIcon
                Dim defaultButtonWk As MessageBoxDefaultButton
                For i = 0 To elemList.Count - 1
                    If elemList.ItemOf(i).Item("ID").InnerText = prmMsgId Then
                        '���b�Z�[�WID����v����Ȃ�
                        '�^�C�g���ҏW
                        Dim titleWk As String
                        Try
                            titleWk = elemList.ItemOf(i).Item("TITLE").InnerText
                        Catch ex As Exception
                            titleWk = _defTitle
                        End Try
                        '���b�Z�[�W�ҏW
                        Dim msg1 As String = ""
                        Dim msg2 As String = ""

                        If prmLang = "JPN" AndAlso elemList.ItemOf(i).Item("MSG1_EN") IsNot Nothing Then
                            Try : msg1 = elemList.ItemOf(i).Item("MSG1").InnerText : Catch ex As Exception : End Try
                        Else
                            Try : msg1 = elemList.ItemOf(i).Item("MSG1_EN").InnerText : Catch ex As Exception : End Try
                        End If

                        If prmLang = "JPN" AndAlso elemList.ItemOf(i).Item("MSG2_EN") IsNot Nothing Then
                            Try : msg2 = elemList.ItemOf(i).Item("MSG2").InnerText : Catch ex As Exception : End Try
                        Else
                            Try : msg2 = elemList.ItemOf(i).Item("MSG2_EN").InnerText : Catch ex As Exception : End Try
                        End If

                        Dim dspStrWk As String
                        If msg2 <> "" Then
                            dspStrWk = msg1 & ControlChars.NewLine & msg2
                        Else
                            dspStrWk = msg1
                        End If
                        If prmOptionalMsg <> "" Then
                            dspStrWk = dspStrWk & ControlChars.NewLine & ControlChars.NewLine & prmOptionalMsg
                        End If
                        '�{�^���ҏW
                        Dim button As String
                        Try
                            button = elemList.ItemOf(i).Item("BUTTON_TYPE").InnerText
                        Catch ex As Exception : button = "" : End Try '�\�L�ȗ���
                        Select Case button.ToLower
                            Case "abortretryignore"
                                buttonWk = MessageBoxButtons.AbortRetryIgnore
                            Case "ok"
                                buttonWk = MessageBoxButtons.OK
                            Case "okcancel"
                                buttonWk = MessageBoxButtons.OKCancel
                            Case "retrycancel"
                                buttonWk = MessageBoxButtons.RetryCancel
                            Case "yesno"
                                buttonWk = MessageBoxButtons.YesNo
                            Case "yesnocancel"
                                buttonWk = MessageBoxButtons.YesNoCancel
                            Case ""
                                buttonWk = _defButton
                            Case Else
                                Dim lex As UsrDefException = New UsrDefException("BUTTON_TYPE�̒�`������Ă��܂��B")
                                Debug.WriteLine(lex.Message)
                                Throw lex
                        End Select
                        '�A�C�R���ҏW
                        Dim icon As String
                        Try
                            icon = elemList.ItemOf(i).Item("ICONT_TYPE").InnerText
                        Catch ex As Exception : icon = "" : End Try '�\�L�ȗ���
                        Select Case icon.ToLower
                            Case "asterisk"
                                iconWk = MessageBoxIcon.Asterisk
                            Case "error"
                                iconWk = MessageBoxIcon.Error
                            Case "exclamation"
                                iconWk = MessageBoxIcon.Exclamation
                            Case "hand"
                                iconWk = MessageBoxIcon.Hand
                            Case "information"
                                iconWk = MessageBoxIcon.Information
                            Case "none"
                                iconWk = MessageBoxIcon.None
                            Case "question"
                                iconWk = MessageBoxIcon.Question
                            Case "stop"
                                iconWk = MessageBoxIcon.Stop
                            Case "warning"
                                iconWk = MessageBoxIcon.Warning
                            Case ""
                                iconWk = _defIcon
                            Case Else
                                Dim lex As UsrDefException = New UsrDefException("ICONT_TYPE�̒�`������Ă��܂��B")
                                Debug.WriteLine(lex.Message)
                                Throw lex
                        End Select
                        '�f�t�H���g�{�^���ҏW
                        Dim defaultButton As String
                        Try
                            defaultButton = elemList.ItemOf(i).Item("DEFAULT_BUTTON").InnerText
                        Catch ex As Exception : defaultButton = "" : End Try '�\�L�ȗ���
                        Select Case defaultButton.ToLower
                            Case "button1"
                                defaultButtonWk = MessageBoxDefaultButton.Button1
                            Case "button2"
                                defaultButtonWk = MessageBoxDefaultButton.Button2
                            Case "button3"
                                defaultButtonWk = MessageBoxDefaultButton.Button3
                            Case ""
                                defaultButtonWk = _defDefaultButton
                            Case Else
                                Dim lex As UsrDefException = New UsrDefException("DEFAULT_BUTTON�̒�`������Ă��܂��B")
                                Debug.WriteLine(lex.Message)
                                Throw lex
                        End Select

                        'MSG�\��
                        Return New UtilMsgVO(dspStrWk, titleWk, buttonWk, iconWk, defaultButtonWk)
                    End If
                Next
                Return New UtilMsgVO("���b�Z�[�WID��������܂���B(" & prmMsgId & ")", "�V�X�e���G���[", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Debug.WriteLine(ex.StackTrace)
                Throw ex
            End Try

        End Function
    End Class

    '===============================================================================
    '
    '  ���[�e�B���e�B�N���X
    '    �i�N���X���j    UtilMsgVO
    '    �i�����@�\���j      xml��`�̃��b�Z�[�W�{�b�N�X�r�[��
    '    �i�{MDL�g�p�O��j   UtilMsgHandler����荞�܂�Ă��邱��
    '    �i���l�j            
    '
    '===============================================================================
    '  ����  ���O          ��  �t      �}�[�N      ���e
    '-------------------------------------------------------------------------------
    '  (1)   Laevigata, Inc.    2006/05/07              �V�K
    '-------------------------------------------------------------------------------
    Public Class UtilMsgVO

        '===============================================================================
        '�����o�[�ϐ���`
        '===============================================================================
        Private _dspStr As String
        Private _title As String
        Private _button As MessageBoxButtons
        Private _icon As MessageBoxIcon
        Private _defaultButton As MessageBoxDefaultButton

        '===============================================================================
        '�v���p�e�B(�A�N�Z�T)
        '===============================================================================
        Public ReadOnly Property dspStr() As String
            Get
                Return _dspStr
            End Get
        End Property
        Public ReadOnly Property title() As String
            Get
                Return _title
            End Get
        End Property
        Public ReadOnly Property button() As MessageBoxButtons
            Get
                Return _button
            End Get
        End Property
        Public ReadOnly Property icon() As MessageBoxIcon
            Get
                Return _icon
            End Get
        End Property
        Public ReadOnly Property defaultButton() As MessageBoxDefaultButton
            Get
                Return _defaultButton
            End Get
        End Property

        '===============================================================================
        ' �R���X�g���N�^
        '   �����̓p�����^   �F�e��MessageBox�p�����^
        '===============================================================================
        ''' <summary>
        ''' �R���X�g���N�^
        ''' </summary>
        ''' <param name="prmDspStr">�o�͂���e�L�X�g</param>
        ''' <param name="prmTitle">�^�C�g��</param>
        ''' <param name="prmButton">�{�^���̎��</param>
        ''' <param name="prmIcon">�A�C�R��</param>
        ''' <param name="prmDefaultButton">����̃{�^��</param>
        ''' <remarks></remarks>
        Friend Sub New(ByVal prmDspStr As String, ByVal prmTitle As String, ByVal prmButton As MessageBoxButtons, ByVal prmIcon As MessageBoxIcon, ByVal prmDefaultButton As MessageBoxDefaultButton)
            _dspStr = prmDspStr
            _title = prmTitle
            _button = prmButton
            _icon = prmIcon
            _defaultButton = prmDefaultButton
        End Sub

    End Class
End Namespace
