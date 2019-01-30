Imports System.Windows.Forms
'===============================================================================
'
'  ���[�e�B���e�B�N���X
'    �i�N���X���j    UtilProgressBar
'    �i�����@�\���j      �v���O���X�o�[��ʋ@�\��񋟂���
'    �i�{MDL�g�p�O��j   ���ɂȂ�
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
'  (1)   Laevigata, Inc.    2006/06/02              �V�K
'-------------------------------------------------------------------------------
Public Class UtilProgressBar
    Inherits System.Windows.Forms.Form

    '===============================================================================
    '�����o�[�ϐ���`
    '===============================================================================
    Private _parentForm As Form
    Private _cur As Cursor = Cursors.Default
    Private _windowTitle As String = ""                 'WindowTitle
    Private _jobName As String = ""                     '��������
    Private _status As String = ""                      '�������
    Private _maxVal As Integer = 100                    '�ő又���X�e�b�v
    Private _minVal As Integer = 0                      '�ŏ������X�e�b�v
    Private _oneStep As Integer = 2                     '�ǂ̒P�ʂŃX�e�b�v�A�b�v���邩
    Private _value As Integer = 0                       '���݂̏����X�e�b�v

    '===============================================================================
    '�v���p�e�B(�A�N�Z�T)
    '===============================================================================
    'Window�^�C�g��
    Public Property windowTitle() As String
        Get
            Return _windowTitle
        End Get
        Set(ByVal value As String)
            _windowTitle = value
            Me.Text = _windowTitle
            Application.DoEvents()
            Me.Refresh()
        End Set
    End Property
    '������
    Public Property jobName() As String
        Get
            Return _jobName
        End Get
        Set(ByVal value As String)
            _jobName = value
            lblJobName.Text = _jobName
            pgbBar.Value = 0
            Application.DoEvents()
            Me.Refresh()
        End Set
    End Property
    '�������
    Public Property status() As String
        Get
            Return _status
        End Get
        Set(ByVal value As String)
            _status = value
            lblStatus.Text = _status
            Application.DoEvents()
            Me.Refresh()
        End Set
    End Property
    '�ő�l
    Public Property maxVal() As Integer
        Get
            Return _maxVal
        End Get
        Set(ByVal value As Integer)
            _maxVal = value
            pgbBar.Maximum = _maxVal
            Application.DoEvents()
            Me.Refresh()
        End Set
    End Property
    '�ŏ��l
    Public Property minVal() As Integer
        Get
            Return _minVal
        End Get
        Set(ByVal value As Integer)
            _minVal = value
            pgbBar.Minimum = _minVal
            Application.DoEvents()
            Me.Refresh()
        End Set
    End Property
    '�X�e�b�v��
    Public Property oneStep() As Integer
        Get
            Return _oneStep
        End Get
        Set(ByVal value As Integer)
            _oneStep = value
            pgbBar.Step = _oneStep
            Application.DoEvents()
            Me.Refresh()
        End Set
    End Property
    '�i�����
    Public Property value() As Integer
        Get
            Return _value
        End Get
        Set(ByVal value As Integer)
            _value = value
            pgbBar.Value = _value
            Application.DoEvents()
            Me.Refresh()
        End Set
    End Property


    '-------------------------------------------------------------------------------
    '�R���X�g���N�^
    '-------------------------------------------------------------------------------
    Private Sub New()
        InitializeComponent()
    End Sub
    Public Sub New(ByRef prmRefParentForm As Form)
        Me.New()

        _windowTitle = Me.Text
        _jobName = ""
        Me.lblJobName.Text = _jobName
        _status = ""
        Me.lblStatus.Text = _status

        _parentForm = prmRefParentForm
        _parentForm.Enabled = False

        Application.DoEvents()
        StartPosition = FormStartPosition.CenterScreen                      '��ʒ����\��
    End Sub

    '-------------------------------------------------------------------------------
    '�t�H�[�����[�h
    '-------------------------------------------------------------------------------
    Private Sub UtilProgressBar_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '�`��֌W�̐ݒ�
        Me.SetStyle(ControlStyles.ResizeRedraw, True)           '�T�C�Y���ύX���ꂽ�Ƃ��ɁA�R���g���[�����R���g���[�����̂��ĕ`�悷�邩�ǂ����������l��ݒ�
        Me.SetStyle(ControlStyles.DoubleBuffer, True)           '�`��̓o�b�t�@�Ŏ��s����A������ɁA���ʂ���ʂɏo�͂����悤�ݒ�
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  '�R���g���[���́A��ʂɒ��ڂł͂Ȃ��A�܂��o�b�t�@�ɕ`�悳��܂��B����ɂ��A�������}���邱�Ƃ��ł��܂��B
        Me.SetStyle(ControlStyles.UserPaint, True)              '�R���g���[���́A�I�y���[�e�B���O �V�X�e���ɂ���Ăł͂Ȃ��A�Ǝ��ɕ`�悳���悤�ݒ�
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   '�R���g���[���̓E�B���h�E ���b�Z�[�W WM_ERASEBKGND �𖳎�����悤�ɐݒ�

        _cur = Me.Cursor
        Application.DoEvents()
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()

        Me.Refresh()
        Application.DoEvents()

    End Sub

    '-------------------------------------------------------------------------------
    '�t�H�[���N���[�Y
    '-------------------------------------------------------------------------------
    Private Sub UtilProgressBar_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Cursor = _cur
        _parentForm.Enabled = True
    End Sub

End Class
