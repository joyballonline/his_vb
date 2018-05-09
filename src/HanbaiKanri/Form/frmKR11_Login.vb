'===============================================================================
'�@ �i�V�X�e�����j      �w���A�g�V�X�e��
'
'   �i�@�\���j          ���O�C��
'   �i�N���X���j        frmKR11_Login
'   �i�����@�\���j      
'   �i�{MDL�g�p�O��j   UtilMDL�v���W�F�N�g���\�����[�V�����Ɏ�荞�܂�Ă��邱��
'   �i���l�j            
'
'===============================================================================
' ����  ���O               ���t       �}�[�N    ���e
'-------------------------------------------------------------------------------
'  (1)  Shigihara          2010/03/01           �V�K
'  (2)  Shigihara          2013/03/11 V1.2.0.1  �S���҃R�[�h���݃`�F�b�N�̈ʒu��ύX
'-------------------------------------------------------------------------------
Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB

Imports System.Drawing.Printing
Imports System.Runtime.InteropServices

'===================================================================================
'�t�H�[��
'===================================================================================
Public Class frmKR11_Login

    Inherits System.Windows.Forms.Form

    '------------------------------------------------------------------------------------------------------
    '�����o�[�萔�錾
    '------------------------------------------------------------------------------------------------------
    'PG���䕶�� 
    Private Const N As String = ControlChars.NewLine                    '���s����
    Private Const RS As String = "RecSet"                               '���R�[�h�Z�b�g�e�[�u��

    '-------------------------------------------------------------------------------
    '�����o�[�ϐ��錾
    '-------------------------------------------------------------------------------
    Private _parentForm As Form
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _btnSybt As String

    '���O�C�����i�[�ϐ�
    Private Shared _loginVal As StartUp.loginType

    '-------------------------------------------------------------------------------
    '�v���p�e�B�錾
    '-------------------------------------------------------------------------------
    Public Shared ReadOnly Property loginValue() As StartUp.loginType                     '���O�C�����\���̂�ԋp
        Get
            Return _loginVal
        End Get
    End Property

    '-------------------------------------------------------------------------------
    '�R���X�g���N�^�iPrivate�ɂ��āA�O����͌ĂׂȂ��悤�ɂ���j
    '-------------------------------------------------------------------------------
    Private Sub New()
        ' ���̌Ăяo���́AWindows �t�H�[�� �f�U�C�i�ŕK�v�ł��B
        InitializeComponent()
        cmbCampany.SelectedIndex = 0
    End Sub

    '-------------------------------------------------------------------------------
    '�R���X�g���N�^�@StartUp����Ă΂��B
    '   �����̓p�����^   �FprmRefMsgHd      MSG�n���h��
    '                      prmRefDbHd       DB�n���h��
    '   �����\�b�h�߂�l �F�C���X�^���X
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf)
        Call Me.New()

        '��������
        _msgHd = prmRefMsgHd                                                    'MSG�n���h���̐ݒ�
        _db = prmRefDbHd                                                        'DB�n���h���̐ݒ�
        StartPosition = FormStartPosition.CenterScreen                          '��ʒ����\��
        lblVer.Text = "Ver : " & UtilClass.getAppVersion(StartUp.assembly)      '���x���ցA�o�[�W�������̕\��

    End Sub

    '-------------------------------------------------------------------------------
    '   �I���{�^��
    '   �i�����T�v�j�t�H�[�������
    '-------------------------------------------------------------------------------
    Private Sub btnEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnd.Click

        Me.Close()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�t�H�[�����[�h�C�x���g
    '------------------------------------------------------------------------------------------------------
    Private Sub frm_E11_Login_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            '�`��֌W�̐ݒ�
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           '�T�C�Y���ύX���ꂽ�Ƃ��ɁA�R���g���[�����R���g���[�����̂��ĕ`�悷�邩�ǂ����������l��ݒ�
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '�`��̓o�b�t�@�Ŏ��s����A������ɁA���ʂ���ʂɏo�͂����悤�ݒ�
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  '�R���g���[���́A��ʂɒ��ڂł͂Ȃ��A�܂��o�b�t�@�ɕ`�悳��܂��B����ɂ��A�������}���邱�Ƃ��ł��܂��B
            Me.SetStyle(ControlStyles.UserPaint, True)              '�R���g���[���́A�I�y���[�e�B���O �V�X�e���ɂ���Ăł͂Ȃ��A�Ǝ��ɕ`�悳���悤�ݒ�
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   '�R���g���[���̓E�B���h�E ���b�Z�[�W WM_ERASEBKGND �𖳎�����悤�ɐݒ�

            '������
            Call initForm()

        Catch ue As UsrDefException
            ue.dspMsg()                                                                                                     '����Ԃ�
        Catch ex As Exception
            '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��V�X�e���G���[MSG�o��
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))     '����Ԃ�
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   ���O�C���{�^��
    '-------------------------------------------------------------------------------
    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Try

            '���̓`�F�b�N---------------------------------------------------------------
            Try
                Call checkInput()
            Catch lex As UsrDefException
                lex.dspMsg()
                Exit Sub
            End Try

            Dim sql As String = ""
            '�S���҃R�[�h���݃`�F�b�N
            Try
                sql = sql & N & "SELECT "
                sql = sql & N & "    V.���[�U�[ID "
                sql = sql & N & " FROM ���[�U V "
                sql = sql & N & " WHERE "
                sql = sql & N & "    V.���[�U�[ID = '" & _db.rmSQ(txtTanto.Text) & "'"
                Dim reccnt As Integer = 0
                Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

                If reccnt <= 0 Then

                    Throw New UsrDefException("�o�^����Ă��Ȃ��S���҂ł��B", _msgHd.getMSG("NoTantoCD", ""), txtTanto)

                End If

                '���[�U���̒��o
                sql = ""
                sql = sql & N & "SELECT "
                sql = sql & N & "    V.���[�U�[ID "     '�S���҃R�[�h
                sql = sql & N & "  , M.CPASSWD "        '�p�X���[�h
                sql = sql & N & "  , V.���� "           '����
                sql = sql & N & "  , M.CMNUSER "        '�X�V��
                sql = sql & N & "  , M.DMNDATE "        '�X�V��
                sql = sql & N & " FROM ���[�U V "
                sql = sql & N & " LEFT JOIN M030_TANTO_CTL M "
                sql = sql & N & " ON V.���[�U�[ID = M.CTANTO_CD "
                sql = sql & N & " WHERE "
                sql = sql & N & "    V.���[�U�[ID = '" & _db.rmSQ(txtTanto.Text) & "'"
                Dim reccnt2 As Integer = 0
                Dim ds2 = _db.selectDB(sql, RS, reccnt2)

                Dim lIdx As Long = 0
                Dim bTantoFlg As Boolean = False
                With ds2.Tables(RS)
                    For Cnt As Integer = 0 To reccnt2 - 1

                        '�p�X���[�h�����݂��Ă��邩�`�F�b�N
                        If ds2.Tables(RS).Rows(Cnt)("CPASSWD") Is DBNull.Value Then

                            bTantoFlg = True
                            Exit For

                        End If

                        '�p�X���[�h����v���Ă��邩�`�F�b�N
                        If Not _db.rmNullStr(ds2.Tables(RS).Rows(Cnt)("CPASSWD")).Equals(txtPasswd.Text) Then

                            Throw New UsrDefException("�S���҃R�[�h�ƃp�X���[�h����v���܂���B", _msgHd.getMSG("Unmatch", ""), txtPasswd)

                        End If

                        lIdx = Cnt
                    Next Cnt
                End With
                If bTantoFlg Then
                    MessageBox.Show("�p�X���[�h�����ݒ�ł��B�p�X���[�h��ݒ肵�Ă��������B", _
                                    "�w���A�g�V�X�e��", _
                                    MessageBoxButtons.OK, _
                                    MessageBoxIcon.Information)

                    '�u�p�X���[�h�ύX�v��ʋN��
                    'Dim openForm12 As frmKR12_ChangePasswd = New frmKR12_ChangePasswd(_msgHd, _db, Me, txtTanto.Text)   '�p�����^���N����ʂ֓n��
                    'StartUp.loginForm = Me
                    'openForm12.ShowDialog()                                                 '��ʕ\��
                    'openForm12.Dispose()
                    Exit Sub
                End If

                '���O���[�o���ϐ��ɃZ�b�g
                _loginVal.TantoNM = _db.rmNullStr(ds2.Tables(RS).Rows(lIdx)("����"))
                _loginVal.TantoCD = _db.rmNullStr(ds2.Tables(RS).Rows(lIdx)("���[�U�[ID"))
                _loginVal.Passwd = _db.rmNullStr(ds2.Tables(RS).Rows(lIdx)("CPASSWD"))
                _loginVal.PcName = UtilClass.getComputerName

            Catch lex As UsrDefException
                lex.dspMsg()
                Exit Sub
            End Try

            '�p�X���[�h�ύX�`�F�b�NON�̏ꍇ�A�p�X���[�h�ύX��ʂ��N��
            If chkPasswd.Checked = True Then

                '�u�p�X���[�h�ύX�v��ʋN��
                'Dim openForm12 As frmKR12_ChangePasswd = New frmKR12_ChangePasswd(_msgHd, _db, Me, txtTanto.Text)   '�p�����^���N����ʂ֓n��
                'StartUp.loginForm = Me
                'openForm12.ShowDialog()                                                 '��ʕ\��
                'openForm12.Dispose()
                Exit Sub

            Else

                ''�u�A�g�����ꗗ�v��ʋN��
                'Dim openForm13 As frmKR13_ProcList = New frmKR13_ProcList(_msgHd, _db, Me, _loginVal.TantoCD, _loginVal.TantoNM)      '�p�����^���N����ʂ֓n��
                'StartUp.loginForm = Me
                'openForm13.Show()                                                   '��ʕ\��
                'Me.Hide()                                                           '�����͉B���

            End If

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��V�X�e���G���[MSG�o�͌�X���[
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@�L�[�v���X�C�x���g
    '-------------------------------------------------------------------------------
    Private Sub ctl_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
                                txtTanto.KeyPress, txtPasswd.KeyPress

        '�����L�[��Enter�̏ꍇ�A���̃R���g���[���փt�H�[�J�X�ړ�
        Call UtilClass.moveNextFocus(Me, e)

    End Sub

    '-------------------------------------------------------------------------------
    '�@�t�H�[�J�X�擾�C�x���g
    '-------------------------------------------------------------------------------
    Private Sub ctl_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
                            txtTanto.GotFocus, txtPasswd.GotFocus

        '�t�H�[�J�X�擾���A���̓p�����^�̃R���g���[����S�I����ԂƂ���
        Call UtilClass.selAll(sender)

    End Sub

    '------------------------------------------------------------------------------------------------------
    '   �t�H�[��������
    '------------------------------------------------------------------------------------------------------
    Private Sub initForm()
        Try

            '�R���g���[��������
            Call clearControl()

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��V�X�e���G���[MSG�o�͌�X���[
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   �R���g���[���̃N���A
    '   �i�����T�v�j�@�S�R���g���[���̓��e���N���A����
    '-------------------------------------------------------------------------------
    Private Sub clearControl()

        '�S���҃R�[�h
        txtTanto.Text = ""
        '�p�X���[�h
        txtPasswd.Text = ""
        '�p�X���[�h�ύX�`�F�b�N�{�b�N�X
        chkPasswd.Checked = False

    End Sub

    '------------------------------------------------------------------------------------------------------
    '   ���̓`�F�b�N
    '------------------------------------------------------------------------------------------------------
    Private Sub checkInput()

        '�S���҃R�[�h
        If "".Equals(txtTanto.Text) Then
            Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", ""), txtTanto)
        End If

        '�p�X���[�h
        If "".Equals(txtPasswd.Text) Then
            Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", ""), txtPasswd)
        End If

    End Sub

End Class