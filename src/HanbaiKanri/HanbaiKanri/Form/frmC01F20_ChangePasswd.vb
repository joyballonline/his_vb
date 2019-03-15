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
Imports UtilMDL.LANG
Imports UtilMDL.DB

Imports System.Drawing.Printing
Imports System.Runtime.InteropServices

'===================================================================================
'���j���[�t�H�[��
'===================================================================================
Public Class frmC01F20_ChangePasswd
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
    Private _langHd As UtilLangHandler
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
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler,
                   ByRef prmRefDbHd As UtilDBIf,
                   ByRef prmRefForm As Form)

        Call Me.New()

        '��������
        _msgHd = prmRefMsgHd                                                'MSG�n���h���̐ݒ�
        _db = prmRefDbHd                                                    'DB�n���h���̐ݒ�
        _parentForm = prmRefForm                                            '�e�t�H�[��
        _parentForm.Enabled = False
        StartPosition = FormStartPosition.CenterScreen                      '��ʒ����\��
        lblTanto.Text = frmC01F10_Login.loginValue.TantoCD                  '�S���҃R�[�h��\��
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]"                                  '�t�H�[���^�C�g���\��

    End Sub

    '-------------------------------------------------------------------------------
    '   �߂�{�^��
    '-------------------------------------------------------------------------------
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnback.Click

        Dim intRet As Integer
        intRet = _msgHd.dspMSG("rejectPWEdit", frmC01F10_Login.loginValue.Language)
        If intRet = vbNo Then
            Exit Sub
        End If
        Me.Close()
        _parentForm.Show()
        _parentForm.Enabled = True
        _parentForm.Activate()

    End Sub

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
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))     '����Ԃ�
        End Try

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblTitle.Text = "ChangePassword"
            LblUserCd.Text = "UserCode"
            LblNewPassword.Text = "NewPassword"
            LblConfirmation.Text = "Confirmation"
            btnback.Text = "Back"
        End If

    End Sub

    '-------------------------------------------------------------------------------
    '   OK�{�^��������
    '-------------------------------------------------------------------------------
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click


        Try

            '���̓`�F�b�N---------------------------------------------------------------
            '1)	�p�X���[�h���̓`�F�b�N
            Try
                Call checkInput()
            Catch lex As UsrDefException
                lex.dspMsg()
                Exit Sub
            End Try

            '2)�p�X���[�h�`�F�b�N
            '��ʓ��͒l�����ƂɁA�p�X���[�h�}�X�^�Ƃ̐������`�F�b�N���s���B
            '�E�����L�[�F�@IF)��ЃR�[�h�AIF)���[�UID�A���)�p�X���[�h
            '   IF)����ԍ� - 10 ������i�ߋ�10����Əd�����Ȃ��j
            Dim sql As String = ""
            sql = sql & "SELECT count(*) as ����"
            sql = sql & " FROM m03_pswd "
            sql = sql & " WHERE "
            sql = sql & "    ��ЃR�[�h = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"
            sql = sql & "   and ���[�U�h�c = '" & _db.rmSQ(frmC01F10_Login.loginValue.TantoCD) & "'"
            sql = sql & "   and �p�X���[�h = '" & _db.rmSQ(txtPasswd.Text) & "'"
            sql = sql & "   and ����ԍ� > " & _db.rmSQ(frmC01F10_Login.loginValue.Generation) - 10
            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

            '�@�@�Y�����郌�R�[�h�����݂���ꍇ
            '�ȑO�Ɏg�p���ꂽ�p�X���[�h�ł��B
            '���@���͏�Ԃɖ߂�
            If _db.rmNullInt(ds.Tables(RS).Rows(0)("����")) > 0 Then
                _msgHd.dspMSG("ReusePasswd", CommonConst.LANG_KBN_JPN)
                txtPasswd.Focus()
                Exit Sub
            End If

            Dim currentdateCnt As Integer = 0

            '�^�p�J�n��=�V�X�e�����t�̃f�[�^�������擾
            'sql�ҏW
            sql = ""
            sql = "SELECT count(*) ����"
            sql = sql & " FROM"
            sql = sql & " m03_pswd"
            sql = sql & " WHERE"
            sql = sql & "       ��ЃR�[�h = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"
            sql = sql & "   AND ���[�U�h�c = '" & _db.rmSQ(frmC01F10_Login.loginValue.TantoCD) & "'"
            sql = sql & "   AND �K�p�J�n�� = current_date "

            Dim iRecCnt As Integer = 0
            'sql���s
            Dim oDataSet As DataSet = _db.selectDB(sql, RS, iRecCnt)    '���o���ʂ�DS�֊i�[
            currentdateCnt = _db.rmNullInt(oDataSet.Tables(RS).Rows(0)("����"))

            '�p�X���[�h�}�X�^�X�V
            '�^�p�J�n��=�V�X�e�����t�̃f�[�^���������݂��Ă��Ȃ��ꍇ
            If currentdateCnt = 0 Then
                '�K�p�I�������i�V�X�e�����t-1�j�ōX�V
                sql = ""
                sql = "UPDATE m03_pswd"
                sql = sql & " SET"
                sql = sql & "  �K�p�I���� = current_date - 1"                                      '�K�p�I����
                sql = sql & " ,�X�V�� = '" & _db.rmSQ(lblTanto.Text) & "'"                         '�X�V��
                sql = sql & " ,�X�V�� = current_timestamp"                                         '�X�V��
                sql = sql & " WHERE ��ЃR�[�h = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"     '��ЃR�[�h
                sql = sql & "   AND ���[�U�h�c = '" & _db.rmSQ(frmC01F10_Login.loginValue.TantoCD) & "'"                      '���[�U�h�c
                sql = sql & "   AND �K�p�I���� = (SELECT max(�K�p�I����) FROM m03_pswd "
                sql = sql & "                     WHERE ��ЃR�[�h = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"     '��ЃR�[�h
                sql = sql & "                     AND ���[�U�h�c = '" & _db.rmSQ(frmC01F10_Login.loginValue.TantoCD) & "')"                      '���[�U�h�c

                'sql���s
                _db.executeDB(sql)
                '�^�p�J�n��=�V�X�e�����t�̃f�[�^���������݂��Ă���ꍇ
            Else
                '�K�p�I�������V�X�e�����t�ōX�V
                sql = ""
                sql = "UPDATE m03_pswd"
                sql = sql & " SET"
                sql = sql & "  �K�p�I���� = current_date"                                          '�K�p�I����
                sql = sql & " ,�X�V�� = '" & _db.rmSQ(lblTanto.Text) & "'"           '�X�V��
                sql = sql & " ,�X�V�� = current_timestamp"                                         '�X�V��
                sql = sql & " WHERE ��ЃR�[�h = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"     '��ЃR�[�h
                sql = sql & "   AND ���[�U�h�c = '" & _db.rmSQ(frmC01F10_Login.loginValue.TantoCD) & "'"                      '���[�U�h�c
                sql = sql & "   AND �K�p�I���� = (SELECT max(�K�p�I����) FROM m03_pswd "
                sql = sql & "                     WHERE ��ЃR�[�h = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"     '��ЃR�[�h
                sql = sql & "                     AND ���[�U�h�c = '" & _db.rmSQ(frmC01F10_Login.loginValue.TantoCD) & "')"                      '���[�U�h�c

                'sql���s
                _db.executeDB(sql)

            End If

            '���R�[�h�ǉ�
            sql = ""
            sql = sql & "INSERT INTO m03_pswd ( "
            sql = sql & "    ��ЃR�[�h "
            sql = sql & "  , ���[�U�h�c "
            sql = sql & "  , �K�p�J�n�� "
            sql = sql & "  , �K�p�I���� "
            sql = sql & "  , �p�X���[�h "
            sql = sql & "  , �p�X���[�h�ύX���@ "
            sql = sql & "  , ����ԍ� "
            sql = sql & "  , �L������ "
            sql = sql & "  , �X�V�� "
            sql = sql & "  , �X�V�� "
            sql = sql & ") VALUES ( "
            sql = sql & "    '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "       '��ЃR�[�h
            sql = sql & "  , '" & _db.rmSQ(frmC01F10_Login.loginValue.TantoCD) & "' "       '���[�U�h�c
            sql = sql & "  , current_date "     '�^�p�J�n��
            sql = sql & "  , '2099-12-31' "     '�^�p�I����
            sql = sql & "  , '" & _db.rmSQ(txtPasswd.Text) & "' "       '�V�p�X���[�h     ���Í����\�聚
            sql = sql & "  , 1 "                '�p�X���[�h�ύX���@�@�Œ�l"1"�i��ʕύX�j
            sql = sql & "  , " & _db.rmSQ(frmC01F10_Login.loginValue.Generation) + 1           '����ԍ�
            sql = sql & "  , '2099-12-31' "     '�L������
            sql = sql & "  , '" & _db.rmSQ(lblTanto.Text) & "' "        '�X�V��
            sql = sql & "  , current_timestamp "                                  '�X�V��
            sql = sql & ") "
            _db.executeDB(sql)

            '�X�V�������b�Z�[�W
            _msgHd.dspMSG("completePWChanged", CommonConst.LANG_KBN_JPN)


            ''�u�A�g�����ꗗ�v��ʋN��
            'Dim openForm As Form = Nothing
            'openForm = New frmC01F30_Menu(_msgHd, _langHd, _db)
            'openForm.Show()
            Me.Close()
            _parentForm.Enabled = True

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��V�X�e���G���[MSG�o�͌�X���[
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
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

            '�u�V�p�X���[�h�v����͂��Ă��������B
            Throw New UsrDefException(_msgHd.dspMSG("noInputNewPasswordError", frmC01F10_Login.loginValue.Language))

        End If

        '�m�F�p
        If "".Equals(txtKakunin.Text) Then

            '�u�m�F�p�p�X���[�h�v����͂��Ă��������B
            Throw New UsrDefException(_msgHd.dspMSG("noInputConfirmationError", frmC01F10_Login.loginValue.Language))

        End If

        '�V�p�X���[�h�Ɗm�F�p�p�X���[�h�̈�v�m�F
        If Not txtPasswd.Text.Equals(txtKakunin.Text) Then
            '�u�V�p�X���[�h�v�Ɓu�m�F�p�p�X���[�h�v���s��v�ł��B
            Throw New UsrDefException(_msgHd.dspMSG("noInputPasswordVotEqualError", frmC01F10_Login.loginValue.Language))

        End If

    End Sub

End Class
