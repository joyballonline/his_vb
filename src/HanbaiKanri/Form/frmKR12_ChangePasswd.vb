'===============================================================================
'�@ �i�V�X�e�����j      �w���A�g�V�X�e��
'
'   �i�@�\���j          �p�X���[�h�ύX
'   �i�N���X���j        frmKR12_ChangePasswd
'   �i�����@�\���j      
'   �i�{MDL�g�p�O��j   UtilMDL�v���W�F�N�g���\�����[�V�����Ɏ�荞�܂�Ă��邱��
'   �i���l�j            
'
'===============================================================================
' ����  ���O               ���t       �}�[�N    ���e
'-------------------------------------------------------------------------------
'  (1)  Shigihara          2012/03/01           �V�K
'-------------------------------------------------------------------------------
Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB

Imports System.Drawing.Printing
Imports System.Runtime.InteropServices

'===================================================================================
'���j���[�t�H�[��
'===================================================================================
Public Class frmKR12_ChangePasswd
    Inherits System.Windows.Forms.Form


    '-------------------------------------------------------------------------------
    '�����o�[�萔�錾
    '-------------------------------------------------------------------------------
    'PG���䕶�� 
    Private Const N As String = ControlChars.NewLine                    '���s����
    Private Const RS As String = "RecSet"                               '���R�[�h�Z�b�g�e�[�u��
    Private Const FORM_ID As String = "KR12"                            '���ID

    '-------------------------------------------------------------------------------
    '�����o�[�ϐ��錾
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As Form

    '-------------------------------------------------------------------------------
    '�`�o�h�C���|�[�g�@�u�~�v����{�^���𖳌������邽��
    '-------------------------------------------------------------------------------
    <DllImport("USER32.DLL")> _
    Private Shared Function _
    GetSystemMenu(ByVal hWnd As IntPtr, ByVal bRevert As Integer) As IntPtr
    End Function
    <DllImport("USER32.DLL")> _
    Private Shared Function _
    RemoveMenu(ByVal hMenu As IntPtr, ByVal nPosition As Integer, ByVal wFlags As Integer) As Integer
    End Function
    '-------------------------------------------------------------------------------
    '�R���X�g���N�^�iPrivate�ɂ��āA�O����͌ĂׂȂ��悤�ɂ���j
    '-------------------------------------------------------------------------------
    Private Sub New()
        ' ���̌Ăяo���́AWindows �t�H�[�� �f�U�C�i�ŕK�v�ł��B
        InitializeComponent()

        '�u�~�v�{�^���𖳌������邽�߂̒l
        Dim SC_CLOSE As Integer = &HF060
        Dim MF_BYCOMMAND As Integer = &H0
        ' �R���g���[���{�b�N�X�́m����n�{�^���̖�����
        Dim hMenu As IntPtr = GetSystemMenu(Me.Handle, 0)
        RemoveMenu(hMenu, SC_CLOSE, MF_BYCOMMAND)

    End Sub

    '-------------------------------------------------------------------------------
    '�R���X�g���N�^�@���O�C����ʂ���Ă΂��B
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, _
                   ByRef prmRefDbHd As UtilDBIf, _
                   ByRef prmRefForm As Form, _
                   ByVal prmValTantoCD As String)

        Call Me.New()

        '��������
        _msgHd = prmRefMsgHd                                                'MSG�n���h���̐ݒ�
        _db = prmRefDbHd                                                    'DB�n���h���̐ݒ�
        _parentForm = prmRefForm                                            '�e�t�H�[��
        _parentForm.Enabled = False
        StartPosition = FormStartPosition.CenterScreen                      '��ʒ����\��
        lblTanto.Text = prmValTantoCD                                       '�S���҃R�[�h��\��

    End Sub

    '-------------------------------------------------------------------------------
    '   �߂�{�^��
    '-------------------------------------------------------------------------------
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnback.Click

        Me.Close()
        _parentForm.Show()
        _parentForm.Enabled = True
        _parentForm.Activate()

    End Sub

    '-------------------------------------------------------------------------------
    '�t�H�[���N���[�Y�C�x���g
    '-------------------------------------------------------------------------------
    'Private Sub frmKR12_ChangePasswd_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    '    '�e�t�H�[���\��---------------------------------------------------------
    '    _parentForm.Show()
    '    _parentForm.Enabled = True
    '    _parentForm.Activate()

    'End Sub

    '------------------------------------------------------------------------------------------------------
    '�t�H�[�����[�h�C�x���g
    '------------------------------------------------------------------------------------------------------
    Private Sub frm_UserList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            '�`��֌W�̐ݒ�
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           '�T�C�Y���ύX���ꂽ�Ƃ��ɁA�R���g���[�����R���g���[�����̂��ĕ`�悷�邩�ǂ����������l��ݒ�
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '�`��̓o�b�t�@�Ŏ��s����A������ɁA���ʂ���ʂɏo�͂����悤�ݒ�
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  '�R���g���[���́A��ʂɒ��ڂł͂Ȃ��A�܂��o�b�t�@�ɕ`�悳��܂��B����ɂ��A�������}���邱�Ƃ��ł��܂��B
            Me.SetStyle(ControlStyles.UserPaint, True)              '�R���g���[���́A�I�y���[�e�B���O �V�X�e���ɂ���Ăł͂Ȃ��A�Ǝ��ɕ`�悳���悤�ݒ�
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   '�R���g���[���̓E�B���h�E ���b�Z�[�W WM_ERASEBKGND �𖳎�����悤�ɐݒ�

        Catch ue As UsrDefException
            ue.dspMsg()                                                                                                     '����Ԃ�
        Catch ex As Exception
            '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��V�X�e���G���[MSG�o��
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))     '����Ԃ�
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   OK�{�^��������
    '-------------------------------------------------------------------------------
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try

            '���̓`�F�b�N---------------------------------------------------------------
            Try
                Call checkInput()
            Catch lex As UsrDefException
                lex.dspMsg()
                Exit Sub
            End Try

            '�l�S���҂ɑ��݂��邩�m�F
            Dim sql As String = ""
            sql = sql & N & "SELECT "
            sql = sql & N & "    CTANTO_CD "
            sql = sql & N & " FROM M030_TANTO_CTL "
            sql = sql & N & " WHERE "
            sql = sql & N & "    CTANTO_CD = '" & _db.rmSQ(lblTanto.Text) & "'"
            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

            If reccnt <= 0 Then

                '���R�[�h�ǉ�
                sql = ""
                sql = sql & N & "INSERT INTO M030_TANTO_CTL ( "
                sql = sql & N & "    CTANTO_CD "
                sql = sql & N & "  , CPASSWD "
                sql = sql & N & "  , CMNUSER "
                sql = sql & N & "  , DMNDATE "
                sql = sql & N & ") VALUES ( "
                sql = sql & N & "    '" & _db.rmSQ(lblTanto.Text) & "' "        '�S���҃R�[�h
                sql = sql & N & "  , '" & _db.rmSQ(txtPasswd.Text) & "' "       '�V�p�X���[�h
                sql = sql & N & "  , '" & _db.rmSQ(lblTanto.Text) & "' "        '�X�V��
                sql = sql & N & "  , SYSDATE "                                  '�X�V��
                sql = sql & N & ") "
                _db.executeDB(sql)

            Else

                '�p�X���[�h�̍X�V
                sql = ""
                sql = sql & N & "UPDATE M030_TANTO_CTL SET "
                sql = sql & N & "    CPASSWD = '" & _db.rmSQ(txtPasswd.Text) & "'"      '�V�p�X���[�h
                sql = sql & N & "  , CMNUSER = '" & _db.rmSQ(lblTanto.Text) & "'"       '�X�V��
                sql = sql & N & "  , DMNDATE = SYSDATE"                                 '�X�V��
                sql = sql & N & " WHERE "
                sql = sql & N & "    CTANTO_CD = '" & _db.rmSQ(lblTanto.Text) & "'"
                _db.executeDB(sql)

            End If

            '�X�V�������b�Z�[�W
            _msgHd.dspMSG("completePWChanged")

            sql = ""
            sql = sql & N & "SELECT "
            sql = sql & N & "    V.���� "           '����
            sql = sql & N & " FROM ���[�U V "
            sql = sql & N & " LEFT JOIN M030_TANTO_CTL M "
            sql = sql & N & " ON V.���[�U�[ID = M.CTANTO_CD "
            sql = sql & N & " WHERE "
            sql = sql & N & "    V.���[�U�[ID = '" & _db.rmSQ(lblTanto.Text) & "'"
            ds = _db.selectDB(sql, RS, reccnt)

            '�u�A�g�����ꗗ�v��ʋN��
            Dim openForm13 As frmKR13_ProcList = New frmKR13_ProcList(_msgHd, _db, Me, lblTanto.Text, ds.Tables(RS).Rows(0)("����"))      '�p�����^���N����ʂ֓n��
            'StartUp.loginForm = Me
            openForm13.Show()                                                   '��ʕ\��
            Me.Close()
            _parentForm.Hide()

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
                                txtPasswd.KeyPress, txtKakunin.KeyPress

        '�����L�[��Enter�̏ꍇ�A���̃R���g���[���փt�H�[�J�X�ړ�
        Call UtilClass.moveNextFocus(Me, e)

    End Sub

    '-------------------------------------------------------------------------------
    '�@�t�H�[�J�X�擾�C�x���g
    '-------------------------------------------------------------------------------
    Private Sub ctl_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
                            txtPasswd.GotFocus, txtKakunin.GotFocus

        '�t�H�[�J�X�擾���A���̓p�����^�̃R���g���[����S�I����ԂƂ���
        Call UtilClass.selAll(sender)

    End Sub

    '------------------------------------------------------------------------------------------------------
    '   ���̓`�F�b�N
    '------------------------------------------------------------------------------------------------------
    Private Sub checkInput()

        '�V�p�X���[�h
        If "".Equals(txtPasswd.Text) Then
            Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", ""), txtPasswd)
        End If

        '�m�F�p
        If "".Equals(txtKakunin.Text) Then
            Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", ""), txtKakunin)
        End If

        '�V�p�X���[�h�Ɗm�F�p�p�X���[�h�̈�v�m�F
        If Not txtPasswd.Text.Equals(txtKakunin.Text) Then
            Throw New UsrDefException("���͂����p�X���[�h����v���Ă���܂���B", _msgHd.getMSG("UnmatchPasswd", ""), txtKakunin)
        End If

    End Sub

End Class
