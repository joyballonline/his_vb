'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���j��z�f�[�^�C��(�ڍ�)
'    �i�t�H�[��ID�jZG621E_TehaiSyuuseiSyousai
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ���{        2010/10/22                 �V�K    
'�@(2)   ���V        2010/11/17                 �ύX(���ځu�[���v�폜�Ή�)    
'�@(3)   ���V        2010/12/02                 �ύX(�ΏۊO���e��ʂɔ��f����Ȃ��o�O�C��)  
'                                               �ύX(��z���ʂ̎����v�Z�����ǉ�)
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.Combo

Public Class ZG621E_TehaiSyuuseiSyousai
    Inherits System.Windows.Forms.Form

#Region "���e�����l��`"

    'PG���䕶��
    Private Const RS As String = "RecSet"                       '�f�[�^�Z�b�g�e�[�u����
    Private Const N As String = ControlChars.NewLine            '���s����

    '�ꗗ�f�[�^�o�C���h��
    Private Const COLDT_TEHAINO As String = "dtTehaiNo"             '��z��
    Private Const COLDT_SYUTTAIBI As String = "dtSyuttaibi"         '��]�o����
    '2010/11/17 delete start Nakazawa
    'Private Const COLDT_NOUKI As String = "dtNouki"                 '�[��
    '2010/11/17 delete end Nakazawa
    Private Const COLDT_TEHAIKBN As String = "dtTehaikbn"           '��z�敪"
    Private Const COLDT_SEISAKU As String = "dtSeisaku"             '����敪
    Private Const COLDT_SEIZOUBMN As String = "dtSeizoubmn"         '��������
    Private Const COLDT_TYUMONSAKI As String = "dtTyumonsaki"       '������
    Private Const COLDT_HINMEICD As String = "dtHinmeiCD"           '�i���R�[�h
    Private Const COLDT_HINMEI As String = "dtHinmei"               '�����i��
    Private Const COLDT_TEHAISUURYOU As String = "dtTehaiSuuryou"   '��z������
    Private Const COLDT_SIYOUSYONO As String = "dtSiyousyoNo"       '�d�l���ԍ�
    Private Const COLDT_TANTYOU As String = "dtTantyou"             '�P��
    Private Const COLDT_JOUSUU As String = "dtJousuu"               '��
    Private Const COLDT_MAKIWAKU As String = "dtMakiwaku"           '���g�R�[�h
    Private Const COLDT_MAKIWAKUMEI As String = "dtMakiwakuMei"     '���g��
    Private Const COLDT_HOUSOU As String = "dtHousou"               '�/�\���敪
    Private Const COLDT_HOUSOUTYPE As String = "dtHoousoutype"      '�/�\�����
    Private Const COLDT_KSUU As String = "dtKsuu"                   'K�{��
    Private Const COLDT_SHSUU As String = "dtShsuu"                 'S�{��
    Private Const COLDT_GAICYUSAKI As String = "dtGaicyusaki"       '�O����
    Private Const COLDT_CYUMONBI As String = "dtCyumonbi"           '������
    Private Const COLDT_NYUKABI As String = "dtNyukabi"             '���ד�
    Private Const COLDT_KAMOKUCD As String = "dtKamokuCd"           '�ȖڃR�[�h
    Private Const COLDT_CYUMONNO As String = "dtcyumonNo"           '������
    Private Const COLDT_TOKKI As String = "dtTokki"                 '���L����
    Private Const COLDT_BIKO As String = "dtBiko"                   '���l
    Private Const COLDT_HENKO As String = "dtHenko"                 '�ύX���e
    Private Const COLDT_TENKAIKBN As String = "dtTenkaikbn"         '�W�J�敪
    Private Const COLDT_BBNKOUTEI As String = "dtBbnkoutei"         '�����W�J�w��H��
    Private Const COLDT_HINSITUKBN As String = "dtHinsitukbn"       '�i�������敪
    Private Const COLDT_KEISANKBN As String = "dtKeisankbn"         '���H���v�Z�敪
    Private Const COLDT_TATIAIUM As String = "dtTatiaium"           '����L��
    Private Const COLDT_TACIAIBI As String = "dtTaciaibi"           '����\���
    Private Const COLDT_TAISYOGAI As String = "dtTaisyogai"         '�ΏۊO

    '�Œ�L�[
    Private Const COTEI_TEHAI As String = "02"                  '��z�敪
    Private Const COTEI_SEISAKU As String = "03"                '�쐬�敪
    Private Const COTEI_TENKAI As String = "04"                 '�W�J�敪
    Private Const COTEI_KAKOU As String = "05"                  '���H���v�Z
    Private Const COTEI_TATIAI As String = "06"                 '����L��
    Private Const COTEI_HINSHITSU As String = "08"              '�i�������敪
    Private Const COTEI_SEIZOU As String = "09"                 '��������

    '�v���O����ID�iT91���s�����e�[�u���o�^�p�j
    Private Const PGID As String = "ZG620E"

    '�X�V����
    Private Const UPDATECOUNT As Integer = 1

    '�ΏۊO�t���O
    Private Const TAISYO_ARI As String = "1"
    Private Const TAISYO_GAI As String = ""

    '����敪
    Private Const SEISAKU_NAI As String = "1"    '����
    Private Const SEISAKU_GAI As String = "2"    '�O��

    '�W�J�敪
    Private Const TENKAI_ALL As String = "1"
    Private Const TENKAI_POINT As String = "2"

    '�i�������敪
    Private Const HINSITU_YOTYO As String = "0"
    Private Const HINSITU_LOT As String = "2"

    '����敪
    Private Const TACHIAI_NASI As String = "1"
    Private Const TACHIAI_ARI As String = "2"

    '���L��������
    Private Const TOKKI_WORD As String = "ƭ����ܹ"
    Private Const TOKKI_WORD_K As String = " K:"
    Private Const TOKKI_WORD_S As String = " S:"


#End Region

#Region "�����o�ϐ���`"

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As IfRturnUpDateData
    Private _menuForm As ZC110M_Menu

    Private _koteiKey As String
    Private _Syori As String
    Private _Keikaku As String

    Private _changeFlg As Boolean = False           '�ύX�t���O
    Private _beforeChange As String = ""            '�ύX�O�̃f�[�^
    Private _updFlg As Boolean = False

    '-->2010.12/12 add by takagi 
    '-------------------------------------------------------------------------------
    '   �I�[�o�[���C�h�v���p�e�B�Ł~�{�^�������𖳌��ɂ���(ControlBox��True�̂܂܎g�p�\)
    '-------------------------------------------------------------------------------
    Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
        Get
            Const CS_NOCLOSE As Integer = &H200

            Dim tmpCreateParams As System.Windows.Forms.CreateParams = MyBase.CreateParams
            tmpCreateParams.ClassStyle = tmpCreateParams.ClassStyle Or CS_NOCLOSE

            Return tmpCreateParams
        End Get
    End Property
    '<--2010.12/12 add by takagi 

#End Region

#Region "�R���X�g���N�^"

    '-------------------------------------------------------------------------------
    '�R���X�g���N�^�iPrivate�ɂ��āA�O����͌ĂׂȂ��悤�ɂ���j
    '-------------------------------------------------------------------------------
    Private Sub New()
        ' ���̌Ăяo���́AWindows �t�H�[�� �f�U�C�i�ŕK�v�ł��B
        InitializeComponent()

    End Sub

    '-------------------------------------------------------------------------------
    '�R���X�g���N�^�@���j���[����Ă΂��
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmForm As Form, ByVal prmKoteiKey As String, ByVal prmSyori As String, ByVal prmKeikaku As String, ByVal prmUpdFlg As Boolean, ByVal prmMenuForm As ZC110M_Menu)
        Call Me.New()

        '��������
        _msgHd = prmRefMsgHd                                                'MSG�n���h���̐ݒ�
        _db = prmRefDbHd                                                    'DB�n���h���̐ݒ�
        _parentForm = prmForm                                               '�e�t�H�[��
        _koteiKey = prmKoteiKey                                             '�e����󂯎�����Œ�L�[
        _Syori = prmSyori                                                   '�e����󂯎���������N��
        _Keikaku = prmKeikaku                                               '�e����󂯎�����v��N��
        StartPosition = FormStartPosition.CenterScreen                      '��ʒ����\��
        _updFlg = prmUpdFlg
        _menuForm = prmMenuForm
    End Sub

#End Region

#Region "Form�C�x���g"

    '-------------------------------------------------------------------------------
    '�@�����J�n�C�x���g
    '-------------------------------------------------------------------------------
    Private Sub ZG621E_TehaiSyuuseiSyousai_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            '�`��֌W�̐ݒ�
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           '�T�C�Y���ύX���ꂽ�Ƃ��ɁA�R���g���[�����R���g���[�����̂��ĕ`�悷�邩�ǂ����������l��ݒ�
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '�`��̓o�b�t�@�Ŏ��s����A������ɁA���ʂ���ʂɏo�͂����悤�ݒ�
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  '�R���g���[���́A��ʂɒ��ڂł͂Ȃ��A�܂��o�b�t�@�ɕ`�悳��܂��B����ɂ��A�������}���邱�Ƃ��ł��܂��B
            Me.SetStyle(ControlStyles.UserPaint, True)              '�R���g���[���́A�I�y���[�e�B���O �V�X�e���ɂ���Ăł͂Ȃ��A�Ǝ��ɕ`�悳���悤�ݒ�
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   '�R���g���[���̓E�B���h�E ���b�Z�[�W WM_ERASEBKGND �𖳎�����悤�ɐݒ�

            '�^�C�g���I�v�V�����\��
            Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr

            '��ʕ\��
            Call initForm()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

#End Region

#Region "�{�^���C�x���g"

    '------------------------------------------------------------------------------------------------------
    '�@�߂�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnModoru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModoru.Click
        Try
            '�x�����b�Z�[�W
            If _changeFlg Then
                Dim rtn As DialogResult = _msgHd.dspMSG("confirmDgvEdit")   '�ҏW���̓��e���j������܂��B��낵���ł����H
                If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If
            
            '���e�t�H�[���\��
            _parentForm.myShow()
            _parentForm.myActivate()
            Me.Close()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�X�V�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnTouroku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKousin.Click
        Try

            '�K�{���̓`�F�b�N
            Call checkTouroku()

            '�o�^�m�F���b�Z�[�W
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmInsert")    '�o�^���܂��B
            If rtn <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If

            ' ����Wait�J�[�\����ێ�
            Dim preCursor As Cursor = Me.Cursor
            ' �J�[�\����ҋ@�J�[�\���ɕύX
            Me.Cursor = Cursors.WaitCursor
            Try
                '�����J�n���Ԃƒ[��ID�̎擾
                Dim dStartSysdate As Date = Now()                           '�����J�n����
                Dim sPCName As String = UtilClass.getComputerName           '�[��ID
                Dim dtkibou As String = ""
                '2010/11/17 delete start Nakazawa
                'Dim dtnouki As String = ""
                '2010/11/17 delete end Nakazawa
                Dim dttehaisuuryou As String = ""
                Dim dttantyou As String = ""
                Dim dtjyusuu As String = ""
                Dim dtsiyousyono As String = ""

                '�g�����U�N�V�����J�n
                _db.beginTran()

                '��z�e�[�u���̍X�V����
                Call UpdateT51TEHAI(dStartSysdate, sPCName)

                '�����I�������̎擾
                Dim dFinishSysdate As Date = Now()

                '���s�����e�[�u���̍X�V����
                Call updT91Rireki(_koteiKey, sPCName, dStartSysdate, dFinishSysdate)

                'T02��������e�[�u���X�V
                _menuForm.updateSeigyoTbl(PGID, True, dStartSysdate, dFinishSysdate)

                '�g�����U�N�V�����I��
                _db.commitTran()

                '�X�V���e���f
                If Not "".Equals(Trim(Replace(txtKibou.Text, "/", ""))) Then
                    dtkibou = txtKibou.Text.Substring(2, 8) '��]�o����
                End If
                '2010/11/17 delete start Nakazawa
                'If Not "".Equals(Trim(Replace(txtNouki.Text, "/", ""))) Then
                '    dtnouki = txtNouki.Text.Substring(2, 8) '�[��
                'End If
                '2010/11/17 delete end Nakazawa
                dttehaisuuryou = txtTehaiSuuryou.Value      '��z����
                dttantyou = txtTantyou.Text                 '�P��
                dtjyusuu = txtJousuu.Value                  '��
                dtsiyousyono = txtSiyousyoNo.Text           '�d�l���ԍ�


                '2010/12/02 add start Nakazawa
                Dim taisyougaiFlg As Boolean = False        '�ΏۊO�`�F�b�N�p�t���O
                If chktaisyogai.Checked Then
                    taisyougaiFlg = True
                End If
                '2010/12/02 add end Nakazawa

                '�e�t�H�[���ɒl�n��
                '2010/12/02 update start Nakazawa---
                '2010/11/17 update start Nakazawa
                '_parentForm.setUpDateData(dtkibou, dtnouki, dttehaisuuryou, dttantyou, dtjyusuu, dtsiyousyono)
                '_parentForm.setUpDateData(dtkibou, dttehaisuuryou, dttantyou, dtjyusuu, dtsiyousyono)
                '2010/11/17 update end Nakazawa
                _parentForm.setUpDateData(dtkibou, dttehaisuuryou, dttantyou, dtjyusuu, dtsiyousyono, taisyougaiFlg)
                '2010/12/02 update end Nakazawa---

                '�������b�Z�[�W
                _msgHd.dspMSG("completeInsert")

                '�ύX�t���O�𖳌��ɂ���
                _changeFlg = False

                '���e�t�H�[���\��
                _parentForm.myShow()
                _parentForm.myActivate()
                Me.Close()

            Finally
                If _db.isTransactionOpen = True Then
                    _db.rollbackTran()                          '���[���o�b�N
                End If
                ' �J�[�\�������ɖ߂�
                Me.Cursor = preCursor
            End Try

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

#End Region

#Region "���[�U��`�֐�:��ʐ���"
    '-------------------------------------------------------------------------------
    '�@��ʋN����
    '-------------------------------------------------------------------------------
    Private Sub initForm()
        Try
            '�R���{�{�b�N�X
            Call setCbo(cboSeisakuKbn, COTEI_SEISAKU)           '�쐬�敪
            Call setCbo(cboTenkaiKbn, COTEI_TENKAI)             '�W�J�敪
            Call setCbo(cboHinsituKbn, COTEI_HINSHITSU)         '�i�������敪
            Call setCbo(cboTachiai, COTEI_TATIAI)               '����L��

            '��ʕ\��
            Call dispFrom(_koteiKey)

            btnKousin.Enabled = _updFlg

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�R���g���[���L�[�����C�x���g
    '�@(�����T�v)�G���^�[�{�^���������Ɏ��̃R���g���[���Ɉڂ�
    '-------------------------------------------------------------------------------
    '2010/11/17 update start Nakazawa
    'Private Sub txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtKibou.KeyPress, _
    '                                                                                                            txtNouki.KeyPress, _
    '                                                                                                            cboSeisakuKbn.KeyPress, _
    '                                                                                                            txtTehaiSuuryou.KeyPress, _
    '                                                                                                            txtSiyousyoNo.KeyPress, _
    '                                                                                                            txtTantyou.KeyPress, _
    '                                                                                                            txtJousuu.KeyPress, _
    '                                                                                                            txtMakiwaku.KeyPress, _
    '                                                                                                            txtHousou.KeyPress, _
    '                                                                                                            txtKHonsuu.KeyPress, _
    '                                                                                                            txtSHonsuu.KeyPress, _
    '                                                                                                            txtBikou.KeyPress, _
    '                                                                                                            txtGaicyusaki.KeyPress, _
    '                                                                                                            txtCyumonbi.KeyPress, _
    '                                                                                                            txtNyukabi.KeyPress, _
    '                                                                                                            txtKamoku.KeyPress, _
    '                                                                                                            txtCyumonno.KeyPress, _
    '                                                                                                            txtTokki1.KeyPress, _
    '                                                                                                            txtTokki2.KeyPress, _
    '                                                                                                            txtTokki3.KeyPress, _
    '                                                                                                            txtHenkouNaiyou.KeyPress, _
    '                                                                                                            cboTenkaiKbn.KeyPress, _
    '                                                                                                            txtBubunTenkai.KeyPress, _
    '                                                                                                            cboHinsituKbn.KeyPress, _
    '                                                                                                            cboTachiai.KeyPress, _
    '                                                                                                            chktaisyogai.KeyPress
    Private Sub txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtKibou.KeyPress, _
                                                                                                                cboSeisakuKbn.KeyPress, _
                                                                                                                txtTehaiSuuryou.KeyPress, _
                                                                                                                txtSiyousyoNo.KeyPress, _
                                                                                                                txtTantyou.KeyPress, _
                                                                                                                txtJousuu.KeyPress, _
                                                                                                                txtMakiwaku.KeyPress, _
                                                                                                                txtHousou.KeyPress, _
                                                                                                                txtKHonsuu.KeyPress, _
                                                                                                                txtSHonsuu.KeyPress, _
                                                                                                                txtBikou.KeyPress, _
                                                                                                                txtGaicyusaki.KeyPress, _
                                                                                                                txtCyumonbi.KeyPress, _
                                                                                                                txtNyukabi.KeyPress, _
                                                                                                                txtKamoku.KeyPress, _
                                                                                                                txtCyumonno.KeyPress, _
                                                                                                                txtTokki1.KeyPress, _
                                                                                                                txtTokki2.KeyPress, _
                                                                                                                txtTokki3.KeyPress, _
                                                                                                                txtHenkouNaiyou.KeyPress, _
                                                                                                                cboTenkaiKbn.KeyPress, _
                                                                                                                txtBubunTenkai.KeyPress, _
                                                                                                                cboHinsituKbn.KeyPress, _
                                                                                                                cboTachiai.KeyPress, _
                                                                                                                chktaisyogai.KeyPress
        '2010/11/17 update end Nakazawa

        Try
            '���̃R���g���[���ֈړ�����
            UtilClass.moveNextFocus(Me, e)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�R���g���[���L�[�t�H�[�J�X�擾�C�x���g
    '�@(�����T�v)�t�H�[�J�X�擾���A�S�I����Ԃɂ���
    '-------------------------------------------------------------------------------
    '2010/11/17 update start Nakazawa
    'Private Sub txt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtKibou.GotFocus, _
    '                                                                                      txtNouki.GotFocus, _
    '                                                                                      cboSeisakuKbn.GotFocus, _
    '                                                                                      txtTehaiSuuryou.GotFocus, _
    '                                                                                      txtSiyousyoNo.GotFocus, _
    '                                                                                      txtTantyou.GotFocus, _
    '                                                                                      txtJousuu.GotFocus, _
    '                                                                                      txtMakiwaku.GotFocus, _
    '                                                                                      txtHousou.GotFocus, _
    '                                                                                      txtKHonsuu.GotFocus, _
    '                                                                                      txtSHonsuu.GotFocus, _
    '                                                                                      txtBikou.GotFocus, _
    '                                                                                      txtGaicyusaki.GotFocus, _
    '                                                                                      txtCyumonbi.GotFocus, _
    '                                                                                      txtNyukabi.GotFocus, _
    '                                                                                      txtKamoku.GotFocus, _
    '                                                                                      txtCyumonno.GotFocus, _
    '                                                                                      txtTokki1.GotFocus, _
    '                                                                                      txtTokki2.GotFocus, _
    '                                                                                      txtTokki3.GotFocus, _
    '                                                                                      txtHenkouNaiyou.GotFocus, _
    '                                                                                      cboTenkaiKbn.GotFocus, _
    '                                                                                      txtBubunTenkai.GotFocus, _
    '                                                                                      cboHinsituKbn.GotFocus, _
    '                                                                                      cboTachiai.GotFocus
    Private Sub txt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtKibou.GotFocus, _
                                                                                          cboSeisakuKbn.GotFocus, _
                                                                                          txtTehaiSuuryou.GotFocus, _
                                                                                          txtSiyousyoNo.GotFocus, _
                                                                                          txtTantyou.GotFocus, _
                                                                                          txtJousuu.GotFocus, _
                                                                                          txtMakiwaku.GotFocus, _
                                                                                          txtHousou.GotFocus, _
                                                                                          txtKHonsuu.GotFocus, _
                                                                                          txtSHonsuu.GotFocus, _
                                                                                          txtBikou.GotFocus, _
                                                                                          txtGaicyusaki.GotFocus, _
                                                                                          txtCyumonbi.GotFocus, _
                                                                                          txtNyukabi.GotFocus, _
                                                                                          txtKamoku.GotFocus, _
                                                                                          txtCyumonno.GotFocus, _
                                                                                          txtTokki1.GotFocus, _
                                                                                          txtTokki2.GotFocus, _
                                                                                          txtTokki3.GotFocus, _
                                                                                          txtHenkouNaiyou.GotFocus, _
                                                                                          cboTenkaiKbn.GotFocus, _
                                                                                          txtBubunTenkai.GotFocus, _
                                                                                          cboHinsituKbn.GotFocus, _
                                                                                          cboTachiai.GotFocus
        '2010/11/17 update end Nakazawa

        Try
            '�S�I����Ԃɂ���
            UtilClass.selAll(sender)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�e�L�X�g�{�b�N�X�R���g���[���̃t�H�[�J�X�擾���C�x���g
    '�@(�����T�v)�e�L�X�g�{�b�N�X�R���g���[���̃t�H�[�J�X�擾���̏���
    '-------------------------------------------------------------------------------
    '2010/11/17 update start Nakazawa
    'Private Sub txt_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKibou.Enter, _
    '                                                                                      txtNouki.Enter, _
    '                                                                                      txtTehaiSuuryou.Enter, _
    '                                                                                      txtSiyousyoNo.Enter, _
    '                                                                                      txtTantyou.Enter, _
    '                                                                                      txtJousuu.Enter, _
    '                                                                                      txtMakiwaku.Enter, _
    '                                                                                      txtHousou.Enter, _
    '                                                                                      txtKHonsuu.Enter, _
    '                                                                                      txtSHonsuu.Enter, _
    '                                                                                      txtBikou.Enter, _
    '                                                                                      txtGaicyusaki.Enter, _
    '                                                                                      txtCyumonbi.Enter, _
    '                                                                                      txtNyukabi.Enter, _
    '                                                                                      txtKamoku.Enter, _
    '                                                                                      txtCyumonno.Enter, _
    '                                                                                      txtTokki2.Enter, _
    '                                                                                      txtTokki3.Enter, _
    '                                                                                      txtHenkouNaiyou.Enter, _
    '                                                                                      txtBubunTenkai.Enter
    Private Sub txt_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKibou.Enter, _
                                                                                        txtTehaiSuuryou.Enter, _
                                                                                        txtSiyousyoNo.Enter, _
                                                                                        txtTantyou.Enter, _
                                                                                        txtJousuu.Enter, _
                                                                                        txtMakiwaku.Enter, _
                                                                                        txtHousou.Enter, _
                                                                                        txtKHonsuu.Enter, _
                                                                                        txtSHonsuu.Enter, _
                                                                                        txtBikou.Enter, _
                                                                                        txtGaicyusaki.Enter, _
                                                                                        txtCyumonbi.Enter, _
                                                                                        txtNyukabi.Enter, _
                                                                                        txtKamoku.Enter, _
                                                                                        txtCyumonno.Enter, _
                                                                                        txtTokki2.Enter, _
                                                                                        txtTokki3.Enter, _
                                                                                        txtHenkouNaiyou.Enter, _
                                                                                        txtBubunTenkai.Enter
        '2010/11/17 update end Nakazawa
        Try
            '���ɕύX�t���O�������Ă���ꍇ�͉����s��Ȃ�
            If _changeFlg = False Then
                If sender.GetType Is GetType(TextBox) Then
                    Dim txt As TextBox = DirectCast(sender, TextBox)
                    _beforeChange = txt.Text
                ElseIf sender.GetType Is GetType(CustomControl.TextBoxNum) Then
                    Dim txtnum As CustomControl.TextBoxNum = DirectCast(sender, CustomControl.TextBoxNum)
                    _beforeChange = txtnum.Value
                ElseIf sender.GetType Is GetType(CustomControl.TextBoxDate) Then
                    Dim txtdate As CustomControl.TextBoxDate = DirectCast(sender, CustomControl.TextBoxDate)
                    _beforeChange = txtdate.Text
                End If
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�e�L�X�g�{�b�N�X�R���g���[���̃t�H�[�J�X�������C�x���g
    '�@(�����T�v)�e�L�X�g�R���g���[���̃t�H�[�J�X�������̏���
    '-------------------------------------------------------------------------------
    '2010/11/17 update start Nakazawa
    'Private Sub txt_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKibou.Leave, _
    '                                                                                          txtNouki.Leave, _
    '                                                                                          txtTehaiSuuryou.Leave, _
    '                                                                                          txtSiyousyoNo.Leave, _
    '                                                                                          txtTantyou.Leave, _
    '                                                                                          txtJousuu.Leave, _
    '                                                                                          txtMakiwaku.Leave, _
    '                                                                                          txtHousou.Leave, _
    '                                                                                          txtKHonsuu.Leave, _
    '                                                                                          txtSHonsuu.Leave, _
    '                                                                                          txtBikou.Leave, _
    '                                                                                          txtGaicyusaki.Leave, _
    '                                                                                          txtCyumonbi.Leave, _
    '                                                                                          txtNyukabi.Leave, _
    '                                                                                          txtKamoku.Leave, _
    '                                                                                          txtCyumonno.Leave, _
    '                                                                                          txtTokki2.Leave, _
    '                                                                                          txtTokki3.Leave, _
    '                                                                                          txtHenkouNaiyou.Leave, _
    '                                                                                          txtBubunTenkai.Leave
    Private Sub txt_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKibou.Leave, _
                                                                                              txtTehaiSuuryou.Leave, _
                                                                                              txtSiyousyoNo.Leave, _
                                                                                              txtTantyou.Leave, _
                                                                                              txtJousuu.Leave, _
                                                                                              txtMakiwaku.Leave, _
                                                                                              txtHousou.Leave, _
                                                                                              txtKHonsuu.Leave, _
                                                                                              txtSHonsuu.Leave, _
                                                                                              txtBikou.Leave, _
                                                                                              txtGaicyusaki.Leave, _
                                                                                              txtCyumonbi.Leave, _
                                                                                              txtNyukabi.Leave, _
                                                                                              txtKamoku.Leave, _
                                                                                              txtCyumonno.Leave, _
                                                                                              txtTokki2.Leave, _
                                                                                              txtTokki3.Leave, _
                                                                                              txtHenkouNaiyou.Leave, _
                                                                                              txtBubunTenkai.Leave
        '2010/11/17 update end Nakazawa

        Try

            '�ҏW�O�ƒl���ς���Ă����ꍇ�A�t���O�𗧂Ă�
            If sender.GetType Is GetType(TextBox) Then
                Dim txt As TextBox = DirectCast(sender, TextBox)
                If Not _beforeChange.Equals(txt.Text) Then
                    _changeFlg = True
                End If
                Select Case txt.Name
                    Case txtMakiwaku.Name
                        '���g�R�[�h�̏ꍇ
                        If "".Equals(txtMakiwaku.Text) Then
                            '���g���N���A
                            lblMakiwakumei.Text = ""
                        Else
                            '���g���\��
                            Call dispMakiwakuName(txtMakiwaku.Text)
                        End If

                    Case txtHousou.Name
                        '�/�\���敪�̏ꍇ
                        If "".Equals(txtHousou.Text) Then
                            '�/�\���敪��ރN���A
                            lblHousouSyurui.Text = ""
                        Else
                            '�/�\���敪��ޕ\��
                            Call dispHousouType(txtHousou.Text)
                        End If

                    Case txtGaicyusaki.Name
                    Case txtKamoku.Name
                    Case txtCyumonno.Name
                        '�O�����e�L�X�g�{�b�N�X�̏ꍇ
                        '���L�����Q�A�R�ҏW
                        txtTokki2.Text = txtGaicyusaki.Text.PadRight(10) & Strings.Right(Trim(Replace(txtCyumonbi.Text, "/", "")), 6) & Strings.Right(Trim(Replace(txtNyukabi.Text, "/", "")), 6)
                        txtTokki3.Text = txtKamoku.Text.PadRight(16) & txtCyumonno.Text.PadLeft(6)
                End Select
            ElseIf sender.GetType Is GetType(CustomControl.TextBoxNum) Then
                Dim txtnum As CustomControl.TextBoxNum = DirectCast(sender, CustomControl.TextBoxNum)
                If Not _beforeChange.Equals(txtnum.Text) Then
                    _changeFlg = True
                End If

                '���l�e�L�X�g�{�b�N�X�������͂̏ꍇ�A����������
                If "".Equals(txtnum.Value) Then
                    txtnum.Value = "0"
                End If

                Select Case txtnum.Name
                    Case txtJousuu.Name
                        '�𐔃e�L�X�g�{�b�N�X�̏ꍇ
                        'K�{���Čv�Z
                        txtKHonsuu.Value = (Integer.Parse(txtJousuu.Value) - Integer.Parse(txtSHonsuu.Value)).ToString

                    Case txtKHonsuu.Name
                        'K�{���e�L�X�g�{�b�N�X�̏ꍇ
                        'S�{���Čv�Z
                        txtSHonsuu.Value = (Integer.Parse(txtJousuu.Value) - Integer.Parse(txtKHonsuu.Value)).ToString
                        '���L�L���P�ҏW
                        If Integer.Parse(txtJousuu.Value) < Integer.Parse(txtKHonsuu.Value) + Integer.Parse(txtSHonsuu.Value) Then
                            txtTokki1.Text = ""
                        Else
                            txtTokki1.Text = TOKKI_WORD & TOKKI_WORD_K & txtKHonsuu.Value & TOKKI_WORD_S & txtSHonsuu.Value
                        End If

                    Case txtSHonsuu.Name
                        'S�{���e�L�X�g�{�b�N�X�̏ꍇ
                        'K�{���Čv�Z
                        txtKHonsuu.Value = (Integer.Parse(txtJousuu.Value) - Integer.Parse(txtSHonsuu.Value)).ToString
                        '���L�L���P�ҏW
                        If Integer.Parse(txtJousuu.Value) < Integer.Parse(txtKHonsuu.Value) + Integer.Parse(txtSHonsuu.Value) Then
                            txtTokki1.Text = ""
                        Else
                            txtTokki1.Text = TOKKI_WORD & TOKKI_WORD_K & txtKHonsuu.Value & TOKKI_WORD_S & txtSHonsuu.Value
                        End If
                End Select

            ElseIf sender.GetType Is GetType(CustomControl.TextBoxDate) Then
                Dim txtdate As CustomControl.TextBoxDate = DirectCast(sender, CustomControl.TextBoxDate)
                If Not _beforeChange.Equals(txtdate.Text) Then
                    _changeFlg = True
                End If
                Select Case txtdate.Name
                    Case txtCyumonbi.Name
                    Case txtNyukabi.Name
                        '�O�������t�e�L�X�g�{�b�N�X�̏ꍇ
                        '���L�����Q�A�R�ҏW
                        txtTokki2.Text = txtGaicyusaki.Text.PadRight(10) & Strings.Right(Trim(Replace(txtCyumonbi.Text, "/", "")), 6) & Strings.Right(Trim(Replace(txtNyukabi.Text, "/", "")), 6)
                        txtTokki3.Text = txtKamoku.Text.PadRight(16) & txtCyumonno.Text.PadLeft(6)
                End Select
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@����敪�R���{�{�b�N�X�ύX���̃C�x���g
    '�@(�����T�v)����敪�R���{�{�b�N�X�̑I����e�ɂ���ăR���g���[���̏�Ԃ�ύX����
    '-------------------------------------------------------------------------------
    Private Sub cboSeisakuKbn_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSeisakuKbn.SelectedIndexChanged
        Try
            Dim ch1 As UtilComboBoxHandler = New UtilComboBoxHandler(cboSeisakuKbn)
            Dim ch2 As UtilComboBoxHandler = New UtilComboBoxHandler(cboTenkaiKbn)
            Dim ch3 As UtilComboBoxHandler = New UtilComboBoxHandler(cboHinsituKbn)
            '-->2010.12.27 add by takagi #54
            txtGaicyusaki.Text = ""
            txtCyumonbi.Text = ""
            txtNyukabi.Text = ""
            txtKamoku.Text = ""
            txtCyumonno.Text = ""
            txtTokki1.Text = ""
            txtTokki2.Text = ""
            txtTokki3.Text = ""
            '<--2010.12.27 add by takagi #54
            If ch1.getCode.Equals(SEISAKU_NAI) Then
                '����敪��="1"(����)�̏ꍇ
                grpGaicyu.Enabled = False
                txtTokki1.Enabled = False
                '-->2010.12.27 chg by takagi 
                'txtTokki1.BackColor = Color.FromArgb(255, 255, 192)
                txtTokki1.BackColor = StartUp.lCOLOR_YELLOW
                '<--2010.12.27 chg by takagi 
                txtTokki2.Enabled = True
                '-->2010.12.27 chg by takagi 
                'txtTokki2.BackColor = Color.FromKnownColor(KnownColor.Window)
                txtTokki2.BackColor = StartUp.lCOLOR_WHITE
                '<--2010.12.27 chg by takagi 
                txtTokki3.Enabled = True
                '-->2010.12.27 chg by takagi 
                'txtTokki3.BackColor = Color.FromKnownColor(KnownColor.Window)
                txtTokki3.BackColor = StartUp.lCOLOR_WHITE
                '<--2010.12.27 chg by takagi 
                ch2.selectItem(TENKAI_ALL)
                ch3.selectItem(HINSITU_LOT)
            Else
                '����敪��="2"(�O��)�̏ꍇ
                grpGaicyu.Enabled = True
                txtTokki1.Enabled = True
                txtTokki1.BackColor = Color.FromKnownColor(KnownColor.Window)
                txtTokki2.Enabled = False
                '-->2010.12.27 chg by takagi 
                'txtTokki2.BackColor = Color.FromArgb(255, 255, 192)
                txtTokki2.BackColor = StartUp.lCOLOR_YELLOW
                '<--2010.12.27 chg by takagi 
                txtTokki3.Enabled = False
                '-->2010.12.27 chg by takagi 
                'txtTokki3.BackColor = Color.FromArgb(255, 255, 192)
                txtTokki3.BackColor = StartUp.lCOLOR_YELLOW
                '<--2010.12.27 chg by takagi 
                ch2.selectItem(TENKAI_POINT)
                ch3.selectItem(HINSITU_YOTYO)
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�W�J�敪�R���{�{�b�N�X�ύX���̃C�x���g
    '�@(�����T�v)�W�J�敪�R���{�{�b�N�X�̑I����e�ɂ���ăR���g���[���̏�Ԃ�ύX����
    '-------------------------------------------------------------------------------
    Private Sub cboTenkaiKbn_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTenkaiKbn.TextChanged
        Try
            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(cboTenkaiKbn)
            If ch.getCode.Equals(TENKAI_ALL) Then
                '�W�J�敪="1"(�S�W�J)�̏ꍇ
                txtBubunTenkai.Clear()
                txtBubunTenkai.Enabled = False
                txtBubunTenkai.BackColor = Color.FromArgb(255, 255, 192)
            Else
                '�W�J�敪="2"(�����W�J)�̏ꍇ
                txtBubunTenkai.Enabled = True
                txtBubunTenkai.BackColor = Color.FromKnownColor(KnownColor.Window)
            End If
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@����L���R���{�{�b�N�X�ύX���̃C�x���g
    '�@(�����T�v)����L���R���{�{�b�N�X�̑I����e�ɂ���ăR���g���[���̏�Ԃ�ύX����
    '-------------------------------------------------------------------------------
    Private Sub cboTachiai_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTachiai.SelectedIndexChanged
        Try
            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(cboTachiai)
            If ch.getCode.Equals(TACHIAI_NASI) Then
                '����L��="1"(�i�V)�̏ꍇ
                txtTachiaibi.Text = ""
                txtTachiaibi.Enabled = False
                txtTachiaibi.BackColor = Color.FromArgb(255, 255, 192)
            Else
                '����L��="2"(�A��)�̏ꍇ
                txtTachiaibi.Enabled = True
                txtTachiaibi.BackColor = Color.FromKnownColor(KnownColor.Window)
            End If
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�R���{�{�b�N�X�R���g���[���̃t�H�[�J�X�擾���C�x���g
    '�@(�����T�v)�R���{�{�b�N�X�R���g���[���̃t�H�[�J�X�擾���̏���
    '-------------------------------------------------------------------------------
    Private Sub cbo_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSeisakuKbn.Enter, _
                                                                                              cboTenkaiKbn.Enter, _
                                                                                              cboHinsituKbn.Enter, _
                                                                                              cboTachiai.Enter
        Try

            '���ɕύX�t���O�������Ă���ꍇ�͉����s��Ȃ�
            If _changeFlg = False Then
                Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(sender)
                _beforeChange = ch.getCode()
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�R���{�{�b�N�X�R���g���[���̃t�H�[�J�X�������C�x���g
    '�@(�����T�v)�R���{�{�b�N�X�R���g���[���̃t�H�[�J�X�������̏���
    '-------------------------------------------------------------------------------
    Private Sub cbo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSeisakuKbn.Leave, _
                                                                                              cboTenkaiKbn.Leave, _
                                                                                              cboHinsituKbn.Leave, _
                                                                                              cboTachiai.Leave
        Try
            If _changeFlg = False Then
                '�ҏW�O�ƒl���ς���Ă����ꍇ�A�t���O�𗧂Ă�
                Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(sender)
                If Not _beforeChange.Equals(ch.getCode()) Then
                    _changeFlg = True
                End If
            End If
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�`�F�b�N�{�b�N�X�R���g���[���̃t�H�[�J�X�擾���C�x���g
    '�@(�����T�v)�`�F�b�N�{�b�N�X�R���g���[���̃t�H�[�J�X�擾���̏���
    '-------------------------------------------------------------------------------
    Private Sub chk_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chktaisyogai.Enter
        Try

            Dim chk As CheckBox = DirectCast(sender, CheckBox)
            _beforeChange = chk.Checked.ToString

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�`�F�b�N�{�b�N�X�R���g���[���̃t�H�[�J�X�������C�x���g
    '�@(�����T�v)�`�F�b�N�{�b�N�X�R���g���[���̃t�H�[�J�X�������̏���
    '-------------------------------------------------------------------------------
    Private Sub chk_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chktaisyogai.Leave
        Try
            If _changeFlg = False Then
                '�ҏW�O�ƒl���ς���Ă����ꍇ�A�t���O�𗧂Ă�
                Dim chk As CheckBox = DirectCast(sender, CheckBox)
                If Not _beforeChange.Equals(chk.Checked.ToString) Then
                    _changeFlg = True
                End If
            End If
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�R���{�{�b�N�X�̑I��
    '�@(�����T�v)�R���{�{�b�N�X�̃��X�g��I������
    '�@�@I�@�F�@prmsender       �ΏۃI�u�W�F�N�g
    '�@�@I�@�F�@prmsetdata      �R���{�{�b�N�X�őI����������e
    '-------------------------------------------------------------------------------
    Private Sub selectCbo(ByVal prmsender As Object, ByVal prmsetdata As String)
        Try

            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(prmsender)
            '�R���{�{�b�N�X��I������
            ch.selectItem(prmsetdata)

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�R���{�{�b�N�X�̃R�[�h�擾
    '�@(�����T�v)���ݑI������Ă���R���{�{�b�N�X�̃R�[�h���擾����
    '�@�@I�@�F�@prmsender      �R���{�{�b�N�X�őI����������e
    '-------------------------------------------------------------------------------
    Private Function GetCboCode(ByVal prmsender As Object) As String
        Try

            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(prmsender)
            '�R�[�h�擾
            GetCboCode = ch.getCode()

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Function


#End Region

#Region "���[�U��`�֐�:DB�֘A"
    '-------------------------------------------------------------------------------
    '�@��ʕ\��
    '�@(�����T�v)��ʂ�\������
    '-------------------------------------------------------------------------------
    Private Sub dispFrom(ByVal prmSql As String)
        Try
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & " T51.TEHAI_NO " & COLDT_TEHAINO
            sql = sql & N & " ,TO_CHAR(TO_DATE(T51.KIBOU_DATE,'YYYYMMDD'),'yyyy/mm/dd') " & COLDT_SYUTTAIBI
            '2010/11/17 delete start Nakazawa
            'sql = sql & N & " ,TO_CHAR(TO_DATE(T51.NOUKI,'YYYYMMDD'),'yyyy/mm/dd') " & COLDT_NOUKI
            '2010/11/17 delete end Nakazawa
            sql = sql & N & " ,M011.NAME1 " & COLDT_TEHAIKBN
            sql = sql & N & " ,T51.SEISAKU_KBN " & COLDT_SEISAKU
            sql = sql & N & " ,M012.NAME1 " & COLDT_SEIZOUBMN
            sql = sql & N & " ,T51.TYUMONSAKI " & COLDT_TYUMONSAKI
            sql = sql & N & " ,T51.HINMEI_CD " & COLDT_HINMEICD
            sql = sql & N & " ,T51.HINMEI " & COLDT_HINMEI
            sql = sql & N & " ,T51.TEHAI_SUU " & COLDT_TEHAISUURYOU
            sql = sql & N & " ,T51.SIYOUSYO_NO " & COLDT_SIYOUSYONO
            sql = sql & N & " ,T51.TANCYO " & COLDT_TANTYOU
            sql = sql & N & " ,T51.JYOSU " & COLDT_JOUSUU
            sql = sql & N & " ,T51.MAKI_CD " & COLDT_MAKIWAKU
            sql = sql & N & " ,ZEA.ZE_NAME " & COLDT_MAKIWAKUMEI
            sql = sql & N & " ,T51.HOSO_KBN " & COLDT_HOUSOU
            sql = sql & N & " ,HOS.HN_NAME " & COLDT_HOUSOUTYPE
            sql = sql & N & " ,T51.N_K_SUU " & COLDT_KSUU
            sql = sql & N & " ,T51.N_SH_SUU " & COLDT_SHSUU
            sql = sql & N & " ,T51.GAICYUSAKI " & COLDT_GAICYUSAKI
            sql = sql & N & " ,TO_CHAR(TO_DATE(T51.CYUMONBI,'RRMMDD'),'yyyy/mm/dd') " & COLDT_CYUMONBI
            sql = sql & N & " ,TO_CHAR(TO_DATE(T51.NYUKABI,'RRMMDD'),'yyyy/mm/dd') " & COLDT_NYUKABI
            sql = sql & N & " ,T51.KAMOKU_CD " & COLDT_KAMOKUCD
            sql = sql & N & " ,T51.CYUMONNO " & COLDT_CYUMONNO
            sql = sql & N & " ,T51.TOKKI " & COLDT_TOKKI
            sql = sql & N & " ,T51.BIKO " & COLDT_BIKO
            sql = sql & N & " ,T51.HENKO " & COLDT_HENKO
            sql = sql & N & " ,T51.TENKAI_KBN " & COLDT_TENKAIKBN
            sql = sql & N & " ,T51.BBNKOUTEI " & COLDT_BBNKOUTEI
            sql = sql & N & " ,T51.HINSITU_KBN " & COLDT_HINSITUKBN
            sql = sql & N & " ,M013.NAME1 " & COLDT_KEISANKBN
            sql = sql & N & " ,T51.TATIAI_UM " & COLDT_TATIAIUM
            sql = sql & N & " ,TO_CHAR(TO_DATE(T51.TACIAIBI,'YYYYMMDD'),'yyyy/mm/dd') " & COLDT_TACIAIBI
            sql = sql & N & " ,T51.GAI_FLG " & COLDT_TAISYOGAI
            sql = sql & N & " FROM T51TEHAI T51 "
            sql = sql & N & "   LEFT OUTER JOIN M01HANYO M011 ON "
            sql = sql & N & "   T51.TEHAI_KBN = M011.KAHENKEY "
            sql = sql & N & "   AND M011.KOTEIKEY = '" & COTEI_TEHAI & "'"
            sql = sql & N & "   LEFT OUTER JOIN M01HANYO M012 ON "
            sql = sql & N & "   T51.SEIZOU_BMN = M012.KAHENKEY "
            sql = sql & N & "   AND M012.KOTEIKEY = '" & COTEI_SEIZOU & "'"
            sql = sql & N & "   LEFT OUTER JOIN M01HANYO M013 ON "
            sql = sql & N & "   T51.KEISAN_KBN = M013.KAHENKEY "
            sql = sql & N & "   AND M013.KOTEIKEY = '" & COTEI_KAKOU & "'"
            sql = sql & N & "   LEFT OUTER JOIN M01HANYO M014 ON "
            sql = sql & N & "   T51.TATIAI_UM = M014.KAHENKEY "
            sql = sql & N & "   AND M014.KOTEIKEY = '" & COTEI_TATIAI & "'"
            sql = sql & N & "   LEFT OUTER JOIN ZEASYCODE_TB ZEA ON "
            sql = sql & N & "   T51.MAKI_CD = ZEA.ZE_CODE "
            sql = sql & N & "   LEFT OUTER JOIN HOSONAME_TB HOS ON "
            sql = sql & N & "   T51.HOSO_KBN = HOS.HN_KUBUN "
            sql = sql & N & " WHERE T51.TEHAI_NO = '" & prmSql & "'"
            sql = sql & N & " ORDER BY T51.TEHAI_NO "

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                Throw New UsrDefException("�Ώۃf�[�^������܂���B", _msgHd.getMSG("NonData"))
            End If

            '�������ʕ\��
            lblTehaiNo.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TEHAINO))                 '��z��
            txtKibou.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_SYUTTAIBI))                 '��]�o����
            '2010/11/17 delete start Nakazawa
            'txtNouki.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_NOUKI))                     '�[��
            '2010/11/17 delete end Nakazawa
            lblTehaiKbn.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TEHAIKBN))               '��z�敪
            Call selectCbo(cboSeisakuKbn, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_SEISAKU)))    '����敪
            lblSeizouBmn.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_SEIZOUBMN))             '��������
            lblTyuumonsaki.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TYUMONSAKI))          '������
            lblHinmecd.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_HINMEICD))                '�i���R�[�h
            lblHinmei.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_HINMEI))                   '�����i��
            txtTehaiSuuryou.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TEHAISUURYOU))       '��z������
            txtSiyousyoNo.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_SIYOUSYONO))           '�d�l���ԍ�
            txtTantyou.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TANTYOU))                 '�P��
            txtJousuu.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_JOUSUU))                   '��
            txtMakiwaku.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_MAKIWAKU))               '���g�R�[�h
            lblMakiwakumei.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_MAKIWAKUMEI))         '���g��
            txtHousou.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_HOUSOU))                   '�/�\���敪
            lblHousouSyurui.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_HOUSOUTYPE))         '�/�\�����
            txtKHonsuu.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_KSUU))                    'K�{��
            txtSHonsuu.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_SHSUU))                   'S�{��
            txtGaicyusaki.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_GAICYUSAKI))           '�O����
            txtCyumonbi.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_CYUMONBI))               '������
            txtNyukabi.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_NYUKABI))                 '���ד�
            txtKamoku.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_KAMOKUCD))                 '�ȖڃR�[�h
            txtCyumonno.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_CYUMONNO))               '������
            '���L����
            Dim tokki As String = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TOKKI))
            If tokki.Length <= 22 Then
                txtTokki1.Text = Trim(tokki)
            ElseIf tokki.Length > 22 And tokki.Length <= 44 Then
                txtTokki1.Text = Trim(tokki.Substring(0, 22))
                txtTokki2.Text = Trim(tokki.Substring(22))
            Else
                txtTokki1.Text = Trim(tokki.Substring(0, 22))
                txtTokki2.Text = Trim(tokki.Substring(22, 22))
                txtTokki3.Text = Trim(tokki.Substring(44))
            End If
            txtBikou.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_BIKO))                      '���l
            txtHenkouNaiyou.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_HENKO))              '�ύX���e
            Call selectCbo(cboTenkaiKbn, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TENKAIKBN)))   '�W�J�敪
            txtBubunTenkai.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_BBNKOUTEI))           '�����W�J�w��
            Call selectCbo(cboHinsituKbn, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_HINSITUKBN))) '�i�������敪
            lblKakoutyouKbn.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_KEISANKBN))          '���H���v�Z��
            Call selectCbo(cboTachiai, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TATIAIUM)))      '����L��
            txtTachiaibi.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TACIAIBI))              '����\���
            '�ΏۊO
            If "1".Equals(_db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TAISYOGAI))) Then
                chktaisyogai.Checked = True
            Else
                chktaisyogai.Checked = False
            End If

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@��z�e�[�u���̍X�V����
    '�@(�����T�v)�ύX���ꂽ���e�ɂĎ�z�e�[�u�����X�V����
    '�@�@I�@�F�@prmSysdate       �����J�n����
    '�@�@I�@�F�@prmPCName      �@�[��ID
    '------------------------------------------------------------------------------------------------------
    Private Sub UpdateT51TEHAI(ByVal prmSysdate As Date, ByVal prmPCName As String)
        Try

            'SQL�����s
            Dim sql As String = ""
            sql = "UPDATE T51TEHAI SET"
            sql = sql & N & " KIBOU_DATE = '" & Trim(Replace(txtKibou.Text, "/", "")) & "'"
            '2010/11/17 delete start Nakazawa
            'sql = sql & N & " ,NOUKI = '" & Trim(Replace(txtNouki.Text, "/", "")) & "'"
            '2010/11/17 delete end Nakazawa
            sql = sql & N & " ,SEISAKU_KBN = '" & GetCboCode(cboSeisakuKbn) & "'"
            sql = sql & N & " ,TEHAI_SUU = " & txtTehaiSuuryou.Value
            sql = sql & N & " ,SIYOUSYO_NO = '" & txtSiyousyoNo.Text & "'"
            sql = sql & N & " ,TANCYO = " & txtTantyou.Value
            sql = sql & N & " ,JYOSU = " & txtJousuu.Value
            sql = sql & N & " ,MAKI_CD = '" & txtMakiwaku.Text & "'"
            sql = sql & N & " ,HOSO_KBN = '" & txtHousou.Text & "'"
            sql = sql & N & " ,N_K_SUU = " & txtKHonsuu.Value
            sql = sql & N & " ,N_SH_SUU = " & txtSHonsuu.Value
            sql = sql & N & " ,GAICYUSAKI = '" & txtGaicyusaki.Text & "'"
            '-->2010.12.27 chg by takagi #52
            'sql = sql & N & " ,CYUMONBI = '" & Trim(Replace(txtCyumonbi.Text, "/", "")) & "'"
            'sql = sql & N & " ,NYUKABI = '" & Trim(Replace(txtNyukabi.Text, "/", "")) & "'"
            Dim tmp As String = ""
            tmp = Trim(Replace(txtCyumonbi.Text, "/", ""))
            If tmp.Length > 0 Then tmp = tmp.Substring(2, 6) '���͂������YYMMDD��
            sql = sql & N & " ,CYUMONBI = '" & tmp & "'"
            tmp = Trim(Replace(txtNyukabi.Text, "/", ""))
            If tmp.Length > 0 Then tmp = tmp.Substring(2, 6) '���͂������YYMMDD��
            sql = sql & N & " ,NYUKABI = '" & tmp & "'"
            '<--2010.12.27 chg by takagi #52
            sql = sql & N & " ,KAMOKU_CD = '" & txtKamoku.Text & "'"
            sql = sql & N & " ,CYUMONNO = '" & txtCyumonno.Text & "'"
            sql = sql & N & " ,TOKKI = '" & txtTokki1.Text.PadRight(22) & txtTokki2.Text.PadRight(22) & txtTokki3.Text.PadRight(22) & "'"
            sql = sql & N & " ,BIKO = '" & txtBikou.Text & "'"
            sql = sql & N & " ,HENKO = '" & txtHenkouNaiyou.Text & "'"
            sql = sql & N & " ,TENKAI_KBN = '" & GetCboCode(cboTenkaiKbn) & "'"
            sql = sql & N & " ,BBNKOUTEI = '" & txtBubunTenkai.Text & "'"
            sql = sql & N & " ,HINSITU_KBN = '" & GetCboCode(cboHinsituKbn) & "'"
            sql = sql & N & " ,TATIAI_UM = '" & GetCboCode(cboTachiai) & "'"
            sql = sql & N & " ,TACIAIBI = '" & Trim(Replace(txtTachiaibi.Text, "/", "")) & "'"
            If chktaisyogai.Checked Then
                sql = sql & N & " ,GAI_FLG = '" & TAISYO_ARI & "'"
            Else
                sql = sql & N & " ,GAI_FLG = '" & TAISYO_GAI & "'"
            End If

            sql = sql & N & ", UPDNAME = '" & prmPCName & "'"                                         '�[��ID
            sql = sql & N & ", UPDDATE = TO_DATE('" & prmSysdate & "', 'YYYY/MM/DD HH24:MI:SS') "     '�X�V����
            sql = sql & N & " WHERE TEHAI_NO = '" & _koteiKey & "'"
            _db.executeDB(sql)

        Catch ex As Exception
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�R���{�{�b�N�X�̃Z�b�g
    '�@(�����T�v)M01�ėp�}�X�^����쐬�敪,�W�J�敪,�i�������敪,����L���𒊏o���ĕ\������B
    '�@�@I�@�F�@prmsender       �ݒ�ΏۃI�u�W�F�N�g
    '�@�@I�@�F�@prmWhere      �@��������
    '-------------------------------------------------------------------------------
    Private Sub setCbo(ByVal prmsender As Object, ByVal prmWhere As String)
        Try
            '�R���{�{�b�N�X
            Dim sql = ""
            sql = sql & N & " SELECT KAHENKEY "
            sql = sql & N & " ,NAME1 "
            sql = sql & N & " FROM M01HANYO "
            sql = sql & N & " WHERE KOTEIKEY = '" & prmWhere & "' "
            sql = sql & N & " ORDER BY KAHENKEY "

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                btnKousin.Enabled = False
                Throw New UsrDefException("�o�^����Ă��܂���B", _msgHd.getMSG("noData"))
            End If

            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(prmsender)

            '�������ʂ��R���{�{�b�N�X�ɐݒ�
            For i As Integer = 0 To iRecCnt - 1
                ch.addItem(New UtilCboVO(ds.Tables(RS).Rows(i)(0).ToString, ds.Tables(RS).Rows(i)(1).ToString))
            Next

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@���g���\��
    '�@(�����T�v)���g����\������
    '�@�@I�@�F�@prmWhere      �@��������
    '-------------------------------------------------------------------------------
    Private Sub dispMakiwakuName(ByVal prmWhere As String)
        Try
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & " ZE_NAME " & "NAME"          '���g��
            sql = sql & N & " FROM ZEASYCODE_TB "
            sql = sql & N & " WHERE ZE_CODE = '" & prmWhere & "'"

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                Throw New UsrDefException("���g�����}�X�^�ɓo�^����Ă��܂���B", _msgHd.getMSG("noMakiwakuName"), txtMakiwaku)
            End If

            '�������ʂ�\��
            lblMakiwakumei.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("NAME"))

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub
    '-------------------------------------------------------------------------------
    '�@�/�\���敪��ޕ\��
    '�@(�����T�v)�/�\���敪��ނ�\������
    '�@�@I�@�F�@prmWhere      �@��������
    '-------------------------------------------------------------------------------
    Private Sub dispHousouType(ByVal prmWhere As String)
        Try
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & " HN_NAME " & "NAME"          '�/�\�����
            sql = sql & N & " FROM HOSONAME_TB "
            sql = sql & N & " WHERE HN_KUBUN = '" & prmWhere & "'"

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                Throw New UsrDefException("�/�\����ނ��}�X�^�ɓo�^����Ă��܂���B", _msgHd.getMSG("noHousouType"), txtHousou)
            End If
            '�������ʂ�\��
            lblHousouSyurui.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("NAME"))

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@���s�����e�[�u���̍X�V����
    '�@(�����T�v)���s���e�[�u�����X�V����
    '�@�@I�@�F�@prmTehai       �@�@��z��
    '�@�@I�@�F�@prmPCName      �@�@�[��ID
    '�@�@I�@�F�@prmStartDate       �����J�n����
    '�@�@I�@�F�@prmFinishDate      �����I������
    '------------------------------------------------------------------------------------------------------
    Private Sub updT91Rireki(ByVal prmTehai As String, ByVal prmPCName As String, ByVal prmStartDate As Date, ByVal prmFinishDate As Date)
        Try
            '�o�^����
            Dim sql As String = ""
            sql = "INSERT INTO T91RIREKI ("
            sql = sql & N & "  SNENGETU"                                                    '�����N��
            sql = sql & N & ", KNENGETU"                                                    '�v��N��
            sql = sql & N & ", PGID"                                                        '�@�\ID
            sql = sql & N & ", SDATESTART"                                                  '�����J�n����
            sql = sql & N & ", SDATEEND"                                                    '�����I������
            sql = sql & N & ", KENNSU1"                                                     '�����P�i�폜�����j
            sql = sql & N & ", NAME1"                                                       '�����P�i��z���j
            sql = sql & N & ", UPDNAME"                                                     '�[��ID
            sql = sql & N & ", UPDDATE"                                                     '�X�V����
            sql = sql & N & ") VALUES ("
            sql = sql & N & "  '" & _Syori & "'"                                            '�����N��
            sql = sql & N & ", '" & _Keikaku & "'"                                          '�v��N��
            sql = sql & N & ", '" & PGID & "'"                                              '�@�\ID
            sql = sql & N & ", TO_DATE('" & prmStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "   '�����J�n����
            sql = sql & N & ", TO_DATE('" & prmFinishDate & "', 'YYYY/MM/DD HH24:MI:SS') "  '�����I������
            sql = sql & N & ", " & UPDATECOUNT                                              '�����P�i�X�V�����j
            sql = sql & N & ", '" & prmTehai & "'"                                          '�����P�i��z���j
            sql = sql & N & ", '" & prmPCName & "'"                                         '�[��ID
            sql = sql & N & ", TO_DATE('" & prmStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "   '�X�V����
            sql = sql & N & " )"
            _db.executeDB(sql)

        Catch ex As Exception
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '2010/12/02 add start nakazawa
    '2010/12/02 del start sugano
    ''-------------------------------------------------------------------------------
    ''�@�P��(��)�ҏW�㏈��
    ''�@(�����T�v)��z���ʂ̎����v�Z���s���B
    ''-------------------------------------------------------------------------------
    'Private Sub txtTantyou_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTantyou.LostFocus
    '    Try

    '        Call culcTehaiSuryo()

    '    Catch ue As UsrDefException
    '        ue.dspMsg()
    '    Catch ex As Exception
    '        Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
    '    End Try

    'End Sub

    ''-------------------------------------------------------------------------------
    ''�@�𐔕ҏW�㏈��
    ''�@(�����T�v)��z���ʂ̎����v�Z���s���B
    ''-------------------------------------------------------------------------------
    'Private Sub txtJousuu_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtJousuu.LostFocus
    '    Try

    '        Call culcTehaiSuryo()

    '    Catch ue As UsrDefException
    '        ue.dspMsg()
    '    Catch ex As Exception
    '        Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
    '    End Try

    'End Sub

    ''-------------------------------------------------------------------------------
    ''�@��z���ʂ̎����v�Z
    ''�@(�����T�v)�P���Ə𐔂����z���ʂ������v�Z����B
    ''-------------------------------------------------------------------------------
    'Private Sub culcTehaiSuryo()
    '    Try

    '        '�P���Ə𐔂��������͂���Ă���ꍇ�̂ݍs��
    '        If Not "".Equals(txtTantyou.Text) And Not "".Equals(txtJousuu.Text) Then

    '            txtTehaiSuuryou.Text = CStr(CInt(txtTantyou.Text) * CInt(txtJousuu.Text))

    '        End If

    '    Catch ue As UsrDefException         '���[�U�[��`��O
    '        Call ue.dspMsg()
    '        Throw ue                        '�L���b�`������O�����̂܂܃X���[
    '    Catch ex As Exception               '�V�X�e����O
    '        Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
    '    End Try

    'End Sub
    ''2010/12/02 del end sugano
    '2010/12/02 add end nakazawa

#End Region

#Region "���[�U��`�֐�:�`�F�b�N����"
    '------------------------------------------------------------------------------------------------------
    '  �o�^�`�F�b�N
    '�@(�����T�v)�e���ڂ̓��̓`�F�b�N���s��
    '------------------------------------------------------------------------------------------------------
    Private Sub checkTouroku()
        Try

            Dim ch1 As UtilComboBoxHandler = New UtilComboBoxHandler(cboSeisakuKbn)
            Dim ch2 As UtilComboBoxHandler = New UtilComboBoxHandler(cboTenkaiKbn)
            Dim ch3 As UtilComboBoxHandler = New UtilComboBoxHandler(cboHinsituKbn)

            '�K�{���̓`�F�b�N
            Call chekuHissu(txtKibou, Trim(Replace(txtKibou.Text, "/", "")), "��]�o����")
            '2010/11/17 delete start Nakazawa
            'Call chekuHissu(txtNouki, Trim(Replace(txtNouki.Text, "/", "")), "�[��")
            '2010/11/17 delete end Nakazawa
            Call chekuHissu(txtTehaiSuuryou, txtTehaiSuuryou.Value, "��z����")
            Call chekuHissu(txtSiyousyoNo, txtSiyousyoNo.Text, "�d�l���ԍ�")
            Call chekuHissu(txtTantyou, txtTantyou.Value, "�P��")
            Call chekuHissu(txtJousuu, txtJousuu.Value, "��")
            Call chekuHissu(txtKHonsuu, txtKHonsuu.Value, "K�{��")
            Call chekuHissu(txtSHonsuu, txtSHonsuu.Value, "S�{��")
            Call chekuHissu(cboSeisakuKbn, cboSeisakuKbn.Text, "����敪")
            Call chekuHissu(cboTenkaiKbn, cboTenkaiKbn.Text, "�W�J�敪")
            Call chekuHissu(cboHinsituKbn, cboHinsituKbn.Text, "�i�������敪")
            Call chekuHissu(txtMakiwaku, txtMakiwaku.Text, "���g�R�[�h")
            Call chekuHissu(txtHousou, txtHousou.Text, "�/�\���敪")
            If SEISAKU_GAI.Equals(ch1.getCode) Then
                Call chekuHissu(txtGaicyusaki, txtGaicyusaki.Text, "�O����")
                Call chekuHissu(txtCyumonbi, Trim(Replace(txtCyumonbi.Text, "/", "")), "������")
                Call chekuHissu(txtNyukabi, Trim(Replace(txtNyukabi.Text, "/", "")), "���ד�")
                Call chekuHissu(txtKamoku, txtKamoku.Text, "�ȖڃR�[�h")
                Call chekuHissu(txtCyumonno, txtCyumonno.Text, "�����ԍ�")
            End If

            '�}�C�i�X�l�`�F�b�N
            If Integer.Parse(txtKHonsuu.Value) < 0 Then
                '�G���[���b�Z�[�W�̕\��
                Throw New UsrDefException("�v���X�̒l����͂��Ă��������B", _msgHd.getMSG("NoPositiveNo", "�y K�{�� �z"), txtKHonsuu)
            End If
            If Integer.Parse(txtSHonsuu.Value) < 0 Then
                '�G���[���b�Z�[�W�̕\��
                Throw New UsrDefException("�v���X�̒l����͂��Ă��������B", _msgHd.getMSG("NoPositiveNo", "�y S�{�� �z"), txtSHonsuu)
            End If

            '�召�֌W�`�F�b�N
            If Integer.Parse(txtKHonsuu.Value) > Integer.Parse(txtJousuu.Value) Then
                '�G���[���b�Z�[�W�̕\��
                Throw New UsrDefException("�𐔂𒴂���l�����͂���Ă��܂��B", _msgHd.getMSG("JousuuOver", "�y K�{�� �z"), txtKHonsuu)
            End If
            If Integer.Parse(txtSHonsuu.Value) > Integer.Parse(txtJousuu.Value) Then
                '�G���[���b�Z�[�W�̕\��
                Throw New UsrDefException("�𐔂𒴂���l�����͂���Ă��܂��B", _msgHd.getMSG("JousuuOver", "�y S�{�� �z"), txtSHonsuu)
            End If

            '�����`�F�b�N
            If Not txtMakiwaku.Text.Length.Equals(6) Then
                '�G���[���b�Z�[�W�̕\��
                Throw New UsrDefException("���g�R�[�h�͂U���œ��͂��Ă��������B", _msgHd.getMSG("MakiwakuCDLength", "�y ���g�R�[�h �z"), txtMakiwaku)
            End If
            '-->2010.12.27 del by takagi #55
            'If SEISAKU_GAI.Equals(ch1.getCode) And Not txtKamoku.Text.Length.Equals(6) Then
            '    '�G���[���b�Z�[�W�̕\��
            '    Throw New UsrDefException("�ȖڃR�[�h�͂U���œ��͂��Ă��������B", _msgHd.getMSG("KamokuCDLength", "�y �ȖڃR�[�h �z"), txtKamoku)
            'End If
            '<--2010.12.27 del by takagi #55
            If Long.Parse(txtTantyou.Value) * Long.Parse(txtJousuu.Value) >= 10000000 Then
                '�G���[���b�Z�[�W�̕\��
                Throw New UsrDefException("�P���~�𐔂̒l���V�P�^�𒴂��Ă��܂��B", _msgHd.getMSG("InputLenOver", "�y �P���~�� �z"), txtTantyou)
            End If

            '�󗓃`�F�b�N
            If "".Equals(lblMakiwakumei.Text) Then
                '�G���[���b�Z�[�W�̕\��
                Throw New UsrDefException("���g�����}�X�^�ɓo�^����Ă��܂���B", _msgHd.getMSG("noMakiwakuName"))
            End If
            If "".Equals(lblHousouSyurui.Text) Then
                '�G���[���b�Z�[�W�̕\��
                Throw New UsrDefException("�/�\����ނ��}�X�^�ɓo�^����Ă��܂���B", _msgHd.getMSG("noHousouType"))
            End If

            '���p�����`�F�b�N
            If UtilClass.isOnlyNStr(txtSiyousyoNo.Text) = False Then
                Throw New UsrDefException("���p�p���̂ݓ��͉\�ł��B", _msgHd.getMSG("onlyAcceptHankakuEisu", "�y �d�l���ԍ� �z"), txtSiyousyoNo)
            End If
            If UtilClass.isOnlyNStr(txtGaicyusaki.Text) = False Then
                Throw New UsrDefException("���p�p���̂ݓ��͉\�ł��B", _msgHd.getMSG("onlyAcceptHankakuEisu", "�y �O���� �z"), txtGaicyusaki)
            End If
            If UtilClass.isOnlyNStr(txtKamoku.Text) = False Then
                Throw New UsrDefException("���p�p���̂ݓ��͉\�ł��B", _msgHd.getMSG("onlyAcceptHankakuEisu", "�y �ȖڃR�[�h �z"), txtKamoku)
            End If
            If UtilClass.isOnlyNStr(txtCyumonno.Text) = False Then
                Throw New UsrDefException("���p�p���̂ݓ��͉\�ł��B", _msgHd.getMSG("onlyAcceptHankakuEisu", "�y ������ �z"), txtCyumonno)
            End If
            If UtilClass.isOnlyNStr(txtTokki1.Text) = False Then
                Throw New UsrDefException("���p�p���̂ݓ��͉\�ł��B", _msgHd.getMSG("onlyAcceptHankakuEisu", "�y ���L�����P �z"), txtTokki1)
            End If
            If UtilClass.isOnlyNStr(txtTokki2.Text) = False Then
                Throw New UsrDefException("���p�p���̂ݓ��͉\�ł��B", _msgHd.getMSG("onlyAcceptHankakuEisu", "�y ���L�����Q �z"), txtTokki2)
            End If
            If UtilClass.isOnlyNStr(txtTokki3.Text) = False Then
                Throw New UsrDefException("���p�p���̂ݓ��͉\�ł��B", _msgHd.getMSG("onlyAcceptHankakuEisu", "�y ���L�����R �z"), txtTokki3)
            End If
            If UtilClass.isOnlyNStr(txtBubunTenkai.Text) = False Then
                Throw New UsrDefException("���p�p���̂ݓ��͉\�ł��B", _msgHd.getMSG("onlyAcceptHankakuEisu", "�y �����W�J�w��H�� �z"), txtBubunTenkai)
            End If

            '���͒l�`�F�b�N
            If Not Integer.Parse(txtJousuu.Value).Equals(Integer.Parse(txtKHonsuu.Value) + Integer.Parse(txtSHonsuu.Value)) Then
                '�G���[���b�Z�[�W�̕\��
                Throw New UsrDefException("K�{����S�{���̍��v���A�𐔂ƈ�v���Ă��܂���B", _msgHd.getMSG("notEqualKSSum", "�y �� �z"), txtJousuu)
            End If
            If Not Long.Parse(txtTehaiSuuryou.Value).Equals(Long.Parse(txtTantyou.Value) * Long.Parse(txtJousuu.Value)) Then
                '�G���[���b�Z�[�W�̕\��
                Throw New UsrDefException("��z���ʂ��P���~�𐔂ƈ�v���Ă��܂���B", _msgHd.getMSG("notEqualTehaiSuuryou", "�y ��z���� �z"), txtTehaiSuuryou)
            End If
            If SEISAKU_GAI.Equals(ch1.getCode) And Not TENKAI_POINT.Equals(ch2.getCode) Then
                '�G���[���b�Z�[�W�̕\��
                Throw New UsrDefException("����敪�u�O���v���͓W�J�敪�u�����W�J�v�ȊO�I���o���܂���B", _msgHd.getMSG("nonGaicyuSelect", "�y �W�J�敪 �z"), cboTenkaiKbn)
            End If
            If TENKAI_POINT.Equals(ch2.getCode) And "".Equals(txtBubunTenkai.Text) Then
                '�G���[���b�Z�[�W�̕\��
                Throw New UsrDefException("�W�J�敪�u�����W�J�v���͕����W�J�w��H���͏ȗ��ł��܂���B", _msgHd.getMSG("nonBubunOmit", "�y �����W�J�w��H�� �z"), txtBubunTenkai)
            End If
            If Not "".Equals(txtBubunTenkai.Text) Then
                If Not "1".Equals(txtBubunTenkai.Text.Substring(0, 1)) And Not "3".Equals(txtBubunTenkai.Text.Substring(0, 1)) Then
                    '�G���[���b�Z�[�W�̕\��
                    Throw New UsrDefException("�P�܂��͂R����n�܂�H������͂��Ă��������B", _msgHd.getMSG("ErrStartKoutei", "�y �����W�J�w��H�� �z"), txtBubunTenkai)
                End If
            End If


        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '  �K�{���̓`�F�b�N
    '�@(�����T�v)�e�L�X�g�����͂���Ă��邩�`�F�b�N����
    '�@�@I�@�F�@prmSender              �`�F�b�N����I�u�W�F�N�g
    '�@�@I�@�F�@prmControlName         �`�F�b�N����I�u�W�F�N�g��
    '------------------------------------------------------------------------------------------------------
    Private Sub chekuHissu(ByVal prmSender As System.Object, ByVal prmChktxt As String, ByVal prmControlName As String)
        Try
            '�K�{���̓`�F�b�N
            If "".Equals(prmChktxt) Then
                '�G���[���b�Z�[�W�̕\��
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y " & prmControlName & "�z"), prmSender)
            End If

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub
#End Region

    
End Class