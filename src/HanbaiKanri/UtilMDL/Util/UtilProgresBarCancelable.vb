'===============================================================================
'
'  ���[�e�B���e�B�N���X
'    �i�N���X���j    UtilProgresBarCancelable
'    �i�����@�\���j      �r���L�����Z���\�ȃv���O���X�o�[��ʋ@�\��񋟂���
'    �i�{MDL�g�p�O��j   �v���W�F�N�g��UtilProgressBar���捞�܂�Ă��邱��
'    �i���l�j            
'    �i�g�p���@�j       '�v���O���X�o�[��ʂ�\��
'                       Dim pb As UtilProgressBar = New UtilProgressBar(Me)
'                       pb.Show()
'
'                       '�v���O���X�o�[�ݒ�
'                       pb.jobName = "�o�̓f�[�^�𐶐����Ă��܂��B"
'                       pb.status = "���������D�D�D"
'
'                       '��������
'
'                       '�v���O���X�o�[�l�ݒ�
'                       pb.status = "�o�͒��D�D�D"
'                       pb.oneStep = 10
'                       pb.maxVal = rtnCnt
'                       For i As Integer = 0 To rtnCnt - 1
'                           pb.value = i '�v���O���X�o�[�l�ݒ�
'
'                           '��������
'                       
'                       Next
'                       
'                       '��ʏ���
'                       pb.Close()
'
'===============================================================================
'  ����  ���O          ��  �t      �}�[�N      ���e
'-------------------------------------------------------------------------------
'  (1)   Laevigata, Inc.    2010/12/07              �V�K
'-------------------------------------------------------------------------------
'Public Class UtilProgresBarCancelable

'    '===============================================================================
'    '�����o�[�ϐ���`
'    '===============================================================================
'    Private _pb As myProgressBar = Nothing

'    '===============================================================================
'    '�v���p�e�B(�A�N�Z�T)
'    '===============================================================================
'    'Window�^�C�g��
'    Public Property windowTitle() As String
'        Get
'            Return _pb.windowTitle
'        End Get
'        Set(ByVal value As String)
'            _pb.windowTitle = value
'        End Set
'    End Property
'    '������
'    Public Property jobName() As String
'        Get
'            Return _pb.jobName
'        End Get
'        Set(ByVal value As String)
'            _pb.jobName = value
'        End Set
'    End Property
'    '�������
'    Public Property status() As String
'        Get
'            Return _pb.status
'        End Get
'        Set(ByVal value As String)
'            _pb.status = value
'        End Set
'    End Property
'    '�ő�l
'    Public Property maxVal() As Integer
'        Get
'            Return _pb.maxVal
'        End Get
'        Set(ByVal value As Integer)
'            _pb.maxVal = value
'        End Set
'    End Property
'    '�ŏ��l
'    Public Property minVal() As Integer
'        Get
'            Return _pb.minVal
'        End Get
'        Set(ByVal value As Integer)
'            _pb.minVal = value
'        End Set
'    End Property
'    '�X�e�b�v��
'    Public Property oneStep() As Integer
'        Get
'            Return _pb.oneStep
'        End Get
'        Set(ByVal value As Integer)
'            _pb.oneStep = value
'        End Set
'    End Property
'    '�i�����
'    Public Property value() As Integer
'        Get
'            Return _pb.value
'        End Get
'        Set(ByVal value As Integer)

'            'If _flgCancel Then
'            '    Throw New UsrDefException("�L�����Z������܂����B")
'            'End If

'            _pb.value = value
'        End Set
'    End Property


'    'Private _flgCancel As Boolean = False
'    'Friend Property noticeCancel() As Boolean
'    '    Get
'    '        Return _flgCancel
'    '    End Get
'    '    Set(ByVal value As Boolean)
'    '        _flgCancel = value
'    '    End Set
'    'End Property

'    '-------------------------------------------------------------------------------
'    '�R���X�g���N�^
'    '-------------------------------------------------------------------------------
'    Public Sub New(ByRef prmRefParentForm As Form)
'        _pb = New myProgressBar(prmRefParentForm, Me)
'    End Sub

'    '-------------------------------------------------------------------------------
'    '��ʕ\��
'    '-------------------------------------------------------------------------------
'    Public Sub Show()
'        _pb.Show()
'    End Sub

'    '-------------------------------------------------------------------------------
'    '��ʏ���
'    '-------------------------------------------------------------------------------
'    Public Sub Close()
'        _pb.Close()
'        _pb = Nothing
'    End Sub

'End Class


'===============================================================================
'
'  ���[�e�B���e�B�N���X
'    �i�N���X���j    myProgressBar
'    �i�����@�\���j      �L�����Z���{�^����ێ������v���O���X�o�[���
'    �i�{MDL�g�p�O��j   UtilProgresBarCancelable�ƃZ�b�g�Ŏg�p����
'    �i���l�j            
'===============================================================================
'  ����  ���O          ��  �t      �}�[�N      ���e
'-------------------------------------------------------------------------------
'  (1)   Laevigata, Inc.    2010/12/07              �V�K
'-------------------------------------------------------------------------------
'Public Class myProgressBar
Public Class UtilProgresBarCancelable
    Inherits UtilProgressBar
    Friend WithEvents btnCancel As System.Windows.Forms.Button

    '   Dim _hd As UtilProgresBarCancelable = Nothing


    Public Sub New(ByRef prmRefParentForm As Form) ', ByRef parentHandler As UtilProgresBarCancelable)
        MyBase.New(prmRefParentForm)

        Application.DoEvents()
        Me.Cursor = Cursors.WaitCursor
        Me.Refresh()

        '        _hd = parentHandler

        '�L�����Z���{�^���̒ǉ�
        btnCancel = New System.Windows.Forms.Button
        btnCancel.Location = New System.Drawing.Point(367, 117)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New System.Drawing.Size(101, 23)
        btnCancel.TabIndex = 1
        btnCancel.Text = "�L�����Z��(&C)"
        btnCancel.UseVisualStyleBackColor = True
        btnCancel.Cursor = Cursors.Default
        Me.Controls.Add(btnCancel)

        Application.DoEvents()
        Me.Refresh()

    End Sub

    Public Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        '_hd.noticeCancel = True
        Throw New UtilProgressBarCancelEx("�L�����Z�����s")
    End Sub

    'Private Sub btnCancel_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.MouseHover
    '    btnCancel.Cursor = Cursors.Default
    'End Sub
    'Private Sub btnCancel_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.MouseLeave
    '    Me.Cursor
    'End Sub
End Class

Public Class UtilProgressBarCancelEx
    Inherits Exception

    Public Sub New(ByVal prmMessage As String)
        MyBase.New(prmMessage)
    End Sub

End Class

