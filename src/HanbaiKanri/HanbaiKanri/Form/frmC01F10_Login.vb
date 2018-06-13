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
Imports UtilMDL.LANG
Imports UtilMDL.DB

Imports System.Drawing.Printing
Imports System.Runtime.InteropServices

'===================================================================================
'�t�H�[��
'===================================================================================
Public Class frmC01F10_Login

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
    Private _langHd As UtilLangHandler
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
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefLangHd As UtilLangHandler, ByRef prmRefDbHd As UtilDBIf)
        Call Me.New()

        '��������
        _msgHd = prmRefMsgHd                                                    'MSG�n���h���̐ݒ�
        _langHd = prmRefLangHd                                                  'LANG�n���h���̐ݒ�
        _db = prmRefDbHd                                                        'DB�n���h���̐ݒ�
        StartPosition = FormStartPosition.CenterScreen                          '��ʒ����\��
        lblVer.Text = "Ver : " & UtilClass.getAppVersion(StartUp.assembly)      '���x���ցA�o�[�W�������̕\��
        If StartUp.BackUpServer Then
            '�o�b�N�A�b�v�T�[�o�ڑ���
            lblBackup.Visible = True
        End If
        Dim test As String = _langHd.getLANG("title", "en")

    End Sub

    '-------------------------------------------------------------------------------
    '   �I���{�^��
    '   �i�����T�v�j�t�H�[�������
    '-------------------------------------------------------------------------------
    Private Sub btnEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnd.Click

        Dim intRet As Integer
        intRet = _msgHd.dspMSG("SystemExit")
        If intRet = vbOK Then
            Application.Exit()
        End If

        '���b�Z�[�W�͂��̂�����̃N���X���R�[�����Ȃ��Ă悢�̂��낤���H
        '�Y�����b�Z�[�W���w�l�k�Ɍ�������Ȃ��̂ŁA�Ƃ肠�������̂܂܁B
        'Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", ""), txtTanto)

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

        '_loginVal.BumonNM = cmbCampany.Text
        'If chkPasswd.Checked Then
        '    '�p�X���[�h�ύX�`�F�b�N����
        '    Dim openForm As Form = Nothing
        '    Dim strTitle As String = "[" & cmbCampany.Text & "][�����@�q�l]"
        '    openForm = New frmC01F20_ChangePasswd(_msgHd, _db, Me, txtTanto.Text, strTitle)
        '    openForm.Show()

        'Else
        '    '�p�X���[�h�ύX�`�F�b�N�Ȃ�
        '    Dim openForm As Form = Nothing
        '    openForm = New frmC01F30_Menu(_msgHd, _db)
        '    openForm.Show()
        'End If

        Try

            '���̓`�F�b�N---------------------------------------------------------------
            Try
                Call checkInput()
            Catch lex As UsrDefException
                lex.dspMsg()
                Exit Sub
            End Try

            Dim sql As String = ""
            '2)	�p�X���[�h�`�F�b�N			
            '��ʓ��͒l�����ƂɁA�p�X���[�h�}�X�^�Ƃ̐������`�F�b�N���s���B			
            '�E�����L�[�F�@IF)��ЃR�[�h�AIF)���[�UID�A���)�p�X���[�h			
            '	   IF)����ԍ� - 10 ������i�ߋ�10����Əd�����Ȃ��j
            Try
                sql = sql & N & "SELECT "
                sql = sql & N & "    ���� "
                sql = sql & N & " FROM m02_user "
                sql = sql & N & " WHERE "
                sql = sql & N & "    ��ЃR�[�h = '" & _db.rmSQ(cmbCampany.SelectedValue) & "'"
                sql = sql & N & "    and ���[�U�h�c = '" & _db.rmSQ(txtTanto.Text) & "'"
                sql = sql & N & "    and �����t���O = 0 "
                Dim reccnt As Integer = 0
                Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

                If reccnt <= 0 Then
                    _msgHd.dspMSG("NonImputUserID")
                    'MsgBox("���͂��ꂽ�u���[�UID�v�͑��݂��Ȃ����A�����ɂȂ��Ă��܂��B", vbOK)
                    'Throw New UsrDefException("���͂��ꂽ�u���[�UID�v�͑��݂��Ȃ����A�����ɂȂ��Ă��܂��B", _msgHd.getMSG("NoTantoCD", ""), txtTanto)
                    Exit Sub
                End If

                '2)	�p�X���[�h�`�F�b�N
                '	��ʓ��͒l�����ƂɁA�p�X���[�h�}�X�^�Ƃ̐������`�F�b�N���s���B
                '	�E�����L�[
                '	�@�@�@���)��ЃR�[�h�A���)���[�UID�A�K�p�J�n�����V�X�e�����t���K�p�I����
                '	�E�擾���ځF�@�p�X���[�h�A����ԍ�

                sql = ""
                sql = sql & N & "SELECT "
                sql = sql & N & "   �p�X���[�h "        '�p�X���[�h
                sql = sql & N & "  , ����ԍ� "         '����ԍ�
                sql = sql & N & " FROM m03_pswd "
                sql = sql & N & " where �K�p�J�n�� <= current_date "
                sql = sql & N & "   and �K�p�I���� >= current_date "
                sql = sql & N & "   and ��ЃR�[�h = '" & _db.rmSQ(cmbCampany.SelectedValue) & "'"
                sql = sql & N & "   and ���[�U�h�c = '" & _db.rmSQ(txtTanto.Text) & "'"
                Dim reccnt2 As Integer = 0
                Dim ds2 = _db.selectDB(sql, RS, reccnt2)

                '�@�@�Y�����郌�R�[�h�����݂��Ȃ��ꍇ	
                '���͂��ꂽ�u���[�UID�v�̃p�X���[�h��񂪑��݂��܂���B
                '���@���͏�Ԃɖ߂��i�ʏ�͂��肦�Ȃ��j
                If reccnt2 <= 0 Then
                    _msgHd.dspMSG("NonImputNoDataUserID")
                    'MsgBox("���͂��ꂽ�u���[�UID�v�̃p�X���[�h��񂪑��݂��܂���B", vbOK)
                    'Throw New UsrDefException("���͂��ꂽ�u���[�UID�v�͑��݂��Ȃ����A�����ɂȂ��Ă��܂��B", _msgHd.getMSG("NoTantoCD", ""), txtTanto)
                    Exit Sub
                End If

                '�A�@�Y�����郌�R�[�h�����݂���ꍇ		
                '�A-1�@DB�擾�p�X���[�h�̕�����	
                '�����ʕۗ����i�Í����Ȃ��̂��߁A���̂܂ܔ�r�j

                '�A-2�@�p�X���[�h����`�F�b�N	
                '���)�p�X���[�h��DB)�p�X���[�h���r
                If Not _db.rmNullStr(ds2.Tables(RS).Rows(0)("�p�X���[�h")).Equals(txtPasswd.Text) Then
                    'Throw New UsrDefException("�p�X���[�h���Ⴂ�܂��B", _msgHd.getMSG("Unmatch", ""), txtPasswd)
                    _msgHd.dspMSG("NonImputPassword")
                    'MsgBox("�p�X���[�h���Ⴂ�܂��B", vbOK)
                    Exit Sub
                End If

                '���O���[�o���ϐ��ɃZ�b�g
                '3)	�������N��	
                '���L�v�̂ɂ��������āA�������ɑJ�ڂ��ړ�����B	
                '���O�C����ʂ���A���̍��ڂ��������Ɏ󂯓n���B	
                '�E�󂯓n�����ځF��ЃR�[�h�A��З��́A���[�UID�A�Ј������ABKUP�T�[�o�ڑ��L���i�o�b�N�A�b�v�T�[�o�ڑ���:"Y"�A�ȊO:"N"�j�A����ԍ��i�p�X���[�h�j

                _loginVal.BumonCD = _db.rmNullStr(cmbCampany.SelectedValue)     '��ЃR�[�h
                _loginVal.BumonNM = _db.rmNullStr(cmbCampany.Text)              '��З���
                _loginVal.TantoCD = _db.rmNullStr(txtTanto.Text)                '���[�U�h�c
                _loginVal.TantoNM = _db.rmNullStr(ds.Tables(RS).Rows(0)("����"))                '�Ј�����
                _loginVal.Passwd = _db.rmNullStr(txtPasswd.Text)                '�p�X���[�h
                _loginVal.Generation = _db.rmNullStr(ds2.Tables(RS).Rows(0)("����ԍ�"))                '����ԍ�
                '�������@BKUP�T�[�o�ڑ��L���i�o�b�N�A�b�v�T�[�o�ڑ���:"Y"�A�ȊO:"N"�j


            Catch lex As UsrDefException
                lex.dspMsg()
                Exit Sub
            End Try

            '�p�X���[�h�ύX�`�F�b�NON�̏ꍇ�A�p�X���[�h�ύX��ʂ��N��
            If chkPasswd.Checked = True Then
                '�p�X���[�h�ύX�`�F�b�N����
                Dim openForm As Form = Nothing
                openForm = New frmC01F20_ChangePasswd(_msgHd, _db, Me)
                openForm.ShowDialog()
                openForm.Dispose()

                '�u�p�X���[�h�ύX�v��ʋN��
                'Dim openForm12 As frmKR12_ChangePasswd = New frmKR12_ChangePasswd(_msgHd, _db, Me, txtTanto.Text)   '�p�����^���N����ʂ֓n��
                'StartUp.loginForm = Me
                'openForm12.ShowDialog()                                                 '��ʕ\��
                'openForm12.Dispose()
                Exit Sub

            Else
                '�p�X���[�h�ύX�`�F�b�N�Ȃ�
                Dim openForm As Form = Nothing
                openForm = New frmC01F30_Menu(_msgHd, _db)
                openForm.Show()
                Me.Hide()                                                           '�����͉B���

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

        '�V�X�e�����̕\��
        lblTitle.Text = StartUp.iniValue.SystemCaption

        '�S���҃R�[�h
        txtTanto.Text = ""
        '�p�X���[�h
        txtPasswd.Text = ""
        '�p�X���[�h�ύX�`�F�b�N�{�b�N�X
        chkPasswd.Checked = False

        '��Ж��R���{�{�b�N�X�Z�b�g
        clearCmbCampany()

    End Sub

    '-------------------------------------------------------------------------------
    '   ��Ж��R���{�{�b�N�X��������
    '   �i�����T�v�j�@��Ѓ}�X�^���f�[�^���擾���A�R���{�{�b�N�X�ɃZ�b�g����
    '-------------------------------------------------------------------------------
    Private Sub clearCmbCampany()

        Dim strSql As String = ""
        '��Ѓ}�X�^���R���{�{�b�N�X�ɃZ�b�g
        Try
            strSql = "SELECT "
            strSql = strSql & "    ��ЃR�[�h, ��З��� "
            strSql = strSql & " FROM m01_company "
            strSql = strSql & " order by �\���� "
            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(strSql, RS, reccnt)

            cmbCampany.DataSource = ds.Tables(RS)
            cmbCampany.DisplayMember = "��З���"
            cmbCampany.ValueMember = "��ЃR�[�h"
        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��V�X�e���G���[MSG�o�͌�X���[
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

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