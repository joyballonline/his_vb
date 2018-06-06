Imports System.Windows.Forms
'===============================================================================
'
'  ���[�e�B���e�B�N���X
'    �i�N���X���j    UsrDefException
'    �i�����@�\���j      ���[�U�[��`��O
'    �i�{MDL�g�p�O��j   UtilMsgVO����荞�܂�Ă��邱��
'    �i���l�j            �v���O���}���Ӑ}�I�ɃG���[�𔭐�������Ƃ���Throw�����O
'
'===============================================================================
'  ����  ���O          ��  �t      �}�[�N      ���e
'-------------------------------------------------------------------------------
'  (1)   Jun.Takagi    2006/04/18             �V�K
'-------------------------------------------------------------------------------
Public Class UsrDefException
    Inherits Exception

    '===============================================================================
    '�����o�[�ϐ���`
    '===============================================================================
    Private _msgVO As UtilMDL.MSG.UtilMsgVO
    Private _DisplaiedMsgFlg As Boolean
    Private _targetCtl As Control
    Private _defaultIcon As MessageBoxIcon = MessageBoxIcon.Warning
    Private _colNm As String = ""
    Private _row As Integer = -1

    '===============================================================================
    '�v���p�e�B(�A�N�Z�T)
    '===============================================================================
    Public ReadOnly Property msgVO() As UtilMDL.MSG.UtilMsgVO
        Get
            Return _msgVO
        End Get
    End Property
    Public ReadOnly Property hasMsg() As Boolean
        Get
            Return (_msgVO IsNot Nothing)
        End Get
    End Property

    '===============================================================================
    ' �R���X�g���N�^
    '   �����̓p�����^   �F  prmExceptionMsg    Exception�̃��b�Z�[�W
    '                       <prmDspMessageVO>   �\���p���b�Z�[�W�r�[��
    '                       <prmException>      �V�X�e����O���烆�[�U�[��`��O�Ɏʂ����ۂ̃V�X�e����O
    '                       <prmErrCtl>         ���̓G���[�R���g���[��
    '===============================================================================
    '�@(���[�U�[��`��O�����p�R���X�g���N�^)
    ''' <summary>
    ''' �@(���[�U�[��`��O�����p�R���X�g���N�^)
    ''' </summary>
    ''' <param name="prmExceptionMsg">Exception�Ɋi�[����G���[���b�Z�[�W</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal prmExceptionMsg As String)
        MyBase.New(prmExceptionMsg)
        _DisplaiedMsgFlg = False
        Debug.WriteLine("==========��O���b�Z�[�W==========")
        Debug.WriteLine(prmExceptionMsg)
        Debug.WriteLine("==================================")
    End Sub
    '�A(���[�U�[��`��O�����p�R���X�g���N�^)
    ''' <summary>
    ''' �A(���[�U�[��`��O�����p�R���X�g���N�^)
    ''' </summary>
    ''' <param name="prmExceptionMsg">Exception�Ɋi�[����G���[���b�Z�[�W</param>
    ''' <param name="prmDspMessageVO">���[�U�[�ʒm�p���b�Z�[�W��MsgVO</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal prmExceptionMsg As String, ByVal prmDspMessageVO As UtilMDL.MSG.UtilMsgVO)
        Call Me.New(prmExceptionMsg)                        '�@�ɏ����ϑ�
        _msgVO = prmDspMessageVO
    End Sub
    '�B(�V�X�e����O����̃N���[�������p�R���X�g���N�^)
    ''' <summary>
    ''' �B(�V�X�e����O����̃N���[�������p�R���X�g���N�^)�@Catch���ꂽ�V�X�e����O����UsrDefException�ւ̋l�ߑւ���z��
    ''' </summary>
    ''' <param name="prmException">���������V�X�e����O</param>
    ''' <param name="prmDspMessageVO">���[�U�[�ʒm�p���b�Z�[�W��MsgVO</param>
    ''' <param name="prmSilentMode">���b�Z�[�W���o�����o���Ȃ������w��</param>
    ''' <param name="prmOutLogFile">MSG���o���Ȃ��ꍇ�ɑ���ɏo�͂���郍�O�t�@�C�������w��</param>
    ''' <remarks>�V�X�e����O��Cath����邱�Ƃ�z�肵�Ă���ׁA�C���X�^���X���������_�ŃG���[Msg���o�͂���</remarks>
    Public Sub New(ByVal prmException As Exception, _
                   ByVal prmDspMessageVO As UtilMDL.MSG.UtilMsgVO, _
                   Optional ByVal prmSilentMode As Boolean = False, _
                   Optional ByVal prmOutLogFile As String = "")
        Call Me.New(prmException.Message, prmDspMessageVO)  '�A�ɏ����ϑ�
        Debug.WriteLine(prmException.StackTrace)
        _defaultIcon = MessageBoxIcon.Error
        Call Me.dspMsg(prmSilentMode, prmOutLogFile)                                    '�V�X�e����O�͒����ɃG���[MSG�̕\�����s��
    End Sub
    '�C(���[�U�[��`��O�����p�R���X�g���N�^)
    ''' <summary>
    ''' �C(���[�U�[��`��O�����p�R���X�g���N�^)�@���̓`�F�b�N���Ȃǂ�Throw��z��
    ''' </summary>
    ''' <param name="prmExceptionMsg">Exception�Ɋi�[����G���[���b�Z�[�W</param>
    ''' <param name="prmDspMessageVO">���[�U�[�ʒm�p���b�Z�[�W��MsgVO</param>
    ''' <param name="prmErrCtl">�t�H�[�J�X��ݒ肷��R���g���[��</param>
    ''' <remarks>2006.11.06 Updated By Jun.Takagi</remarks>
    Public Sub New(ByVal prmExceptionMsg As String, ByVal prmDspMessageVO As UtilMDL.MSG.UtilMsgVO, ByVal prmErrCtl As Control)
        Call Me.New(prmExceptionMsg, prmDspMessageVO)       '�A�ɏ����ϑ�
        _targetCtl = prmErrCtl
        _colNm = ""
        _row = -1
    End Sub
    '�D(���[�U�[��`��O�����p�R���X�g���N�^)
    ''' <summary>
    ''' �D(���[�U�[��`��O�����p�R���X�g���N�^)�@DataGridView���̓`�F�b�N���Ȃǂ�Throw��z��
    ''' </summary>
    ''' <param name="prmExceptionMsg">Exception�Ɋi�[����G���[���b�Z�[�W</param>
    ''' <param name="prmDspMessageVO">���[�U�[�ʒm�p���b�Z�[�W��MsgVO</param>
    ''' <param name="prmErrDgv">�t�H�[�J�X��ݒ肷��DataGridView</param>
    ''' <param name="prmColName">�I�����������Z���̃O���b�h��̗�</param>
    ''' <param name="prmRow">�I�����������Z���̍s�ԍ�</param>
    ''' <remarks>2006.11.06 Created By Jun.Takagi</remarks>
    Public Sub New(ByVal prmExceptionMsg As String, _
                       ByVal prmDspMessageVO As UtilMDL.MSG.UtilMsgVO, _
                       ByVal prmErrDgv As Windows.Forms.DataGridView, _
                       ByVal prmColName As String, _
                       ByVal prmRow As Integer)
        Call Me.New(prmExceptionMsg, prmDspMessageVO)       '�A�ɏ����ϑ�
        _targetCtl = CType(prmErrDgv, Control)
        _colNm = prmColName
        _row = prmRow
    End Sub
    '-------------------------------------------------------------------------------
    '   ���b�Z�[�W�\��
    '   �i�����T�v�j�i�[�ς݂�MSG��\��(�����Exception�ɑ΂��Ă͈�x����MSG��\�����Ȃ�)���A
    '               �G���[�ΏۃR���g���[�������݂����ꍇ�̓t�H�[�J�X�̈ʒu�Â����s��
    '   �����̓p�����^   �FprmSilentMode    ���b�Z�[�W���o�����o���Ȃ������w��
    '                    �FprmOutLogFile    MSG���o���Ȃ��ꍇ�ɑ���ɏo�͂���郍�O�t�@�C�������w��
    '   �����\�b�h�߂�l �F�����{�^��(DialogResult)
    '   ��������O       �F�Ȃ�
    '                                               2006.11.06 Updated By Jun.Takagi
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' ���b�Z�[�W�\�� �i�[�ς݂�MSG��\��(�����Exception�ɑ΂��Ă͈�x����MSG��\�����Ȃ�)���A�G���[�ΏۃR���g���[�������݂����ꍇ�̓t�H�[�J�X�̈ʒu�Â����s��
    ''' </summary>
    ''' <param name="prmSilentMode">���b�Z�[�W���o�����o���Ȃ������w��</param>
    ''' <param name="prmOutLogFile">MSG���o���Ȃ��ꍇ�ɑ���ɏo�͂���郍�O�t�@�C�������w��</param>
    ''' <returns>�����{�^��(DialogResult)</returns>
    ''' <remarks></remarks>
    Public Function dspMsg(Optional ByVal prmSilentMode As Boolean = False, Optional ByVal prmOutLogFile As String = "") As DialogResult

        '���b�Z�[�W�\�����s��
        Dim ret As DialogResult
        If _DisplaiedMsgFlg Then
            '����Exception�̃G���[�͕\���ς݂Ȃ̂ŕ\�����Ȃ�
            ret = DialogResult.OK
        Else
            If Not Me.hasMsg Then
                'Message�����݂��Ȃ��ꍇ��ExceptionMessage��\������
                If Not prmSilentMode Then
                    ret = MessageBox.Show(MyBase.Message, "�G���[", MessageBoxButtons.OK, _defaultIcon, MessageBoxDefaultButton.Button1)
                Else
                    '�T�C�����g���[�h�̍ۂ�MSG���o�͂�����LOG�ɏo�͂���
                    'LogFileName����
                    Dim tmpLogFile As String
                    If Not "".Equals(prmOutLogFile) Then
                        tmpLogFile = prmOutLogFile
                    Else
                        '�o�̓��O�t�@�C�������w�肳��Ă��Ȃ��̂Ŏ��͂Ő���
                        tmpLogFile = System.IO.Path.GetDirectoryName( _
                                    System.Reflection.Assembly.GetExecutingAssembly().Location _
                                ) & "\" & _
                                System.Reflection.Assembly.GetExecutingAssembly.GetName().Name & "_" & _
                                Now.ToString("yyyyMMdd") & ".log"
                    End If

                    '���K�[����
                    Dim logger As UtilMDL.Log.UtilLogWriter = New UtilMDL.Log.UtilLogWriter(tmpLogFile)

                    '���O�o��
                    logger.writeLine(MyBase.Message)
                End If
            Else
                '���ɐ�������Ă���_msgVO.dspStr��\��
                If Not prmSilentMode Then
                    ret = MessageBox.Show(_msgVO.dspStr, _msgVO.title, _msgVO.button, _msgVO.icon, _msgVO.defaultButton)
                Else
                    '�T�C�����g���[�h�̍ۂ�MSG���o�͂�����LOG�ɏo�͂���
                    'LogFileName����
                    Dim tmpLogFile As String
                    If Not "".Equals(prmOutLogFile) Then
                        tmpLogFile = prmOutLogFile
                    Else
                        '�o�̓��O�t�@�C�������w�肳��Ă��Ȃ��̂Ŏ��͂Ő���
                        tmpLogFile = System.IO.Path.GetDirectoryName( _
                                    System.Reflection.Assembly.GetExecutingAssembly().Location _
                                ) & "\" & _
                                System.Reflection.Assembly.GetExecutingAssembly.GetName().Name & "_" & _
                                Now.ToString("yyyyMMdd") & ".log"
                    End If

                    '���K�[����
                    Dim logger As UtilMDL.Log.UtilLogWriter = New UtilMDL.Log.UtilLogWriter(tmpLogFile)

                    '���O�o��
                    logger.writeLine(_msgVO.dspStr)
                End If

            End If
        End If
        _DisplaiedMsgFlg = True 'MSG��\�������̂Ńt���O��|��
        _msgVO = Nothing

        '�G���[�R���g���[�������݂���ꍇ�̓t�H�[�J�X���ʒu�t����
        If _targetCtl IsNot Nothing Then
            Dim flg As Boolean = _targetCtl.Enabled
            _targetCtl.Enabled = True   '�g�p�s�ɔ����Ĉ�U�g�p�ɂ���
            _targetCtl.Focus()          '�t�H�[�J�X�ݒ�
            If (Not "".Equals(_colNm) And _row <> -1) Then
                '�f�[�^�O���b�h�r���[�Ȃ̂ōs����ݒ肷��
                CType(_targetCtl, Windows.Forms.DataGridView).CurrentCell _
                  = CType(_targetCtl, Windows.Forms.DataGridView).Rows(_row).Cells(_colNm)
            End If
            _targetCtl.Enabled = flg    '�g�p�s�������ꍇ�͎��̃R���g���[���փt�H�[�J�X�͈ړ�����
        End If
        Return ret

    End Function

End Class
