'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���j���Y�ʃf�[�^�捞
'    �i�t�H�[��ID�jZG210E_SeisanHanei
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ���{        2010/10/26                �V�K              
'�@(2)   ����        2011/01/13                �ύX�@��������e�[�u���̍X�V�^�C�~���O��ύX           
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.Combo
Imports UtilMDL.DataGridView

Imports System.Runtime.InteropServices

Public Class ZG210E_SeisanHanei
    Inherits System.Windows.Forms.Form

#Region "���e�����l��`"
    '------------------------------------------------------------------------------------------------------
    '�����o�[�萔�錾
    '------------------------------------------------------------------------------------------------------
    'PG���䕶�� 
    Private Const N As String = ControlChars.NewLine                    '���s����
    Private Const RS As String = "RecSet"                               '���R�[�h�Z�b�g�e�[�u��
    
    '�ꗗ�f�[�^�o�C���h��
    Private Const COLDT_TAISYOGAI As String = "dtTaisyou"           '�Ώ�
    Private Const COLDT_TAISYOGAICOPY As String = "dtTaisyouCopy"   '�ΏۃR�s�[
    Private Const COLDT_UCHIIRE As String = "dtUchiire"             '����
    Private Const COLDT_TEHAIKBN As String = "dtTehaiKbn"           '��z
    Private Const COLDT_SYUTTAIBI As String = "dtKibou"             '��]�o����
    Private Const COLDT_YOTEI As String = "dtYotei"                 '�\��o����
    Private Const COLDT_TEHAINO As String = "dtTehaiNo"             '��z��
    Private Const COLDT_SEIBAN As String = "dtSeiban"               '����
    Private Const COLDT_HINMEI As String = "dtHinmei"               '�i��
    Private Const COLDT_TEHAISU As String = "dtTehaisu"             '��z��
    Private Const COLDT_TANTYOU As String = "dtTancyo"              '�P��
    Private Const COLDT_JOUSUU As String = "dtJosu"                 '��
    Private Const COLDT_SEISAN As String = "dtSeisan"               '���Y����
    Private Const COLDT_TUKI As String = "dtTuki"                   '�N��
    Private Const COLDT_HINCD As String = "dtHincd"                 '�����i�R�[�h
    Private Const COLDT_OYACD As String = "dtOyacd"                 '�v��i���R�[�h
    Private Const COLDT_JUYOSAKI As String = "dtJuyosaki"           '���v�於
    Private Const COLDT_HINSYU As String = "dtHinsyu"               '�i��敪
    Private Const COLDT_NEN As String = "dtNen"                     '�N�@�@�@�@(�B����)
    Private Const COLDT_RENBAN As String = "dtRenban"               '�A�ԁ@�@�@(�B����)
    Private Const COLDT_HENKOUFLG As String = "dtHenkouFlg"         '�ύX�t���O(�B����)

    '�ꗗ�O���b�h��
    Private Const COLCN_TAISYOGAI As String = "cnTaisyou"           '�Ώ�
    Private Const COLCN_TAISYOGAICOPY As String = "cnTaisyouCopy"   '�ΏۃR�s�[
    Private Const COLCN_UCHIIRE As String = "cnUchiire"             '����
    Private Const COLCN_TEHAIKBN As String = "cnTehaiKbn"           '��z
    Private Const COLCN_SYUTTAIBI As String = "cnKibou"             '��]�o����
    Private Const COLCN_YOTEI As String = "cnYotei"                 '�\��o����
    Private Const COLCN_TEHAINO As String = "cnTehaiNo"             '��z��
    Private Const COLCN_SEIBAN As String = "cnSeiban"               '����
    Private Const COLCN_HINMEI As String = "cnHinmei"               '�i��
    Private Const COLCN_TEHAISU As String = "cnTehaisu"             '��z��
    Private Const COLCN_TANTYOU As String = "cnTancyo"              '�P��
    Private Const COLCN_JOUSUU As String = "cnJosu"                 '��
    Private Const COLCN_SEISAN As String = "cnSeisan"               '���Y����
    Private Const COLCN_TUKI As String = "cnTuki"                   '�N��
    Private Const COLCN_HINCD As String = "cnHincd"                 '�����i�R�[�h
    Private Const COLCN_OYACD As String = "cnOyacd"                 '�v��i���R�[�h
    Private Const COLCN_JUYOSAKI As String = "cnJuyosaki"           '���v�於
    Private Const COLCN_HINSYU As String = "cnHinsyu"               '�i��敪
    Private Const COLCN_NEN As String = "cnNen"                     '�N�@�@�@�@(�B����)
    Private Const COLCN_RENBAN As String = "cnRenban"               '�A�ԁ@�@�@(�B����)
    Private Const COLCN_HENKOUFLG As String = "cnHenkouFlg"         '�ύX�t���O(�B����)

    '�ꗗ�O���b�h��
    Private Const COLNO_TAISYOGAI As Integer = 0                    '�Ώ�
    Private Const COLNO_TAISYOGAICOPY As Integer = 1                '�ΏۃR�s�[
    Private Const COLNO_UCHIIRE As Integer = 2                      '����
    Private Const COLNO_TEHAIKBN As Integer = 3                     '��z
    Private Const COLNO_SYUTTAIBI As Integer = 4                    '��]�o����
    Private Const COLNO_YOTEI As Integer = 5                        '�\��o����
    Private Const COLNO_TEHAINO As Integer = 6                      '��z��
    Private Const COLNO_SEIBAN As Integer = 7                       '����
    Private Const COLNO_HINMEI As Integer = 8                       '�i��
    Private Const COLNO_TEHAISU As Integer = 9                      '��z��
    Private Const COLNO_TANTYOU As Integer = 10                     '�P��
    Private Const COLNO_JOUSUU As Integer = 11                      '��
    Private Const COLNO_SEISAN As Integer = 12                      '���Y����
    Private Const COLNO_TUKI As Integer = 13                        '�N��
    Private Const COLNO_HINCD As Integer = 14                       '�����i�R�[�h
    Private Const COLNO_OYACD As Integer = 15                       '�v��i���R�[�h
    Private Const COLNO_JUYOSAKI As Integer = 16                    '���v�於
    Private Const COLNO_HINSYU As Integer = 17                      '�i��敪
    Private Const COLNO_NEN As Integer = 18                         '�N�@�@�@�@(�B����)
    Private Const COLNO_RENBAN As Integer = 19                      '�A�ԁ@�@�@(�B����)
    Private Const COLNO_HENKOUFLG As Integer = 20                   '�ύX�t���O(�B����)

    '�쐬�敪
    Public Const TEHAI As String = "0"                                 '��z�ς݃f�[�^
    Public Const NYUKO As String = "1"                                 '���ɍς݃f�[�^

    '���[�N�e�[�u���p
    Private Const RENBAN As Integer = 1                                 '�A��
    Private Const NYUKOFLG As String = "1"                              '�����t���O
    Private Const SAKUSEIKBN_TEHAI As String = "1"                      '�쐬�敪(��z�ς݃f�[�^)
    Private Const SAKUSEIKBN_NYUKO As String = "2"                      '�쐬�敪(���ɍς݃f�[�^)
    Private Const TAISYOFLG As String = "1"                             '�Ώۃt���O
    Private Const MINOUZAN As Integer = 0                               '���[�c

    'M01�ėp�}�X�^�Œ跰
    Private Const COTEI_JUYOU As String = "01"                          '���v�於
    Private Const COTEI_TEHAI As String = "02"                          '��z�敪��
    Private Const COTEI_NYUKO As String = "18"                          '�����t���O��

    '�Ώۃt���O
    Private Const TAISYO As String = "1"
    Private Const TAISYO_ARI As String = "True"
    Private Const TAISYO_GAI As String = "False"

    '�v���O����ID�iT91���s�����e�[�u���o�^�p�j
    Private Const PGID1 As String = "ZG210E1"
    Private Const PGID2 As String = "ZG210E2"

    '�ύX�t���O
    Private Const HENKO_FLG As String = "0"

#End Region

#Region "�����o�ϐ���`"
    '------------------------------------------------------------------------------------------------------
    '�����o�[�ϐ��錾
    '------------------------------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler                                    'MSG�n���h��
    Private _db As UtilDBIf                                             'DB�n���h��
    Private _parentForm As ZC110M_Menu

    Private _oldRowIndex As Integer = -1                                '�I���s�̔w�i�F��ύX���邽�߂̃t���O
    Private _colorCtlFlg As Boolean = False                             '�I���s�̔w�i�F��ύX���邽�߂̃t���O

    Private _SakuseiCD As String = ""                                   '�쐬�敪
    Private _tanmatuID As String = ""                                   '�[��ID
    Private _updateDate As Date = Now                                   '�X�V����

    Private _addcount As Long = 0                                       '�X�V����

    Private _errSet As UtilDataGridViewHandler.dgvErrSet                '�G���[�������Ƀt�H�[�J�X����Z���ʒu
    Private _nyuuryokuErrFlg As Boolean = False                         '���̓G���[�L���t���O

    Private _changeFlg As Boolean = False                               '�ꗗ�f�[�^�ύX�t���O
    Private _beforeChange As String = ""                                '�ꗗ�ύX�O�̃f�[�^
   
    Private _chkCellVO As UtilDgvChkCellVO                              '�ꗗ�̓��͐����p
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
        InitializeComponent()

    End Sub

    '-------------------------------------------------------------------------------
    '�R���X�g���N�^�@���j���[����Ă΂��
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmForm As ZC110M_Menu, ByVal prmSakuseiCD As String, ByVal prmUpdFlg As Boolean)
        Call Me.New()

        '��������
        _msgHd = prmRefMsgHd                                                'MSG�n���h���̐ݒ�
        _db = prmRefDbHd                                                    'DB�n���h���̐ݒ�
        _parentForm = prmForm                                               '�e�t�H�[��
        _SakuseiCD = prmSakuseiCD                                           '���j���[��ʂőI�����ꂽ�{�^��
        StartPosition = FormStartPosition.CenterScreen                      '��ʒ����\��
        _updFlg = prmUpdFlg
    End Sub

#End Region

#Region "Form�C�x���g"
    '------------------------------------------------------------------------------------------------------
    '�t�H�[�����[�h�C�x���g
    '------------------------------------------------------------------------------------------------------
    Private Sub frmSH51_ChumonList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

            '������
            Call initForm()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��V�X�e���G���[MSG�o��
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

#End Region

#Region "�{�^���C�x���g"
    '------------------------------------------------------------------------------------------------------
    '�߂�{�^���������C�x���g
    '------------------------------------------------------------------------------------------------------
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            '�`�F�b�N�{�b�N�X�̓��e�ύX�m�F
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)

            If gh.getMaxRow > 0 Then

                For i As Integer = gh.getMaxRow - 1 To 0 Step -1
                    If Not gh.getCellData(COLDT_TAISYOGAI, i).Equals(gh.getCellData(COLDT_TAISYOGAICOPY, i)) Then
                        _changeFlg = True
                        Exit For
                    End If
                Next

                '�x�����b�Z�[�W
                If _changeFlg Then
                    Dim rtn As DialogResult = _msgHd.dspMSG("confirmDgvEdit")   '�ҏW���̓��e���j������܂��B��낵���ł����H
                    If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                        Exit Sub
                    End If
                End If
            Else
                '' 2011/01/13 del start sugano #��������e�[�u���͓o�^�{�^���������̂ݍX�V����
                ''�Ώۃf�[�^���O���̏ꍇ�A��������e�[�u�����X�V����
                'Dim PGID As String = IIf(TEHAI.Equals(_SakuseiCD), PGID1, PGID2)
                '_parentForm.updateSeigyoTbl(PGID, True, Now(), Now())
                '' 2011/01/13 del end sugano
            End If

            '����ʂ��I�����A���j���[��ʂɖ߂�B
            _parentForm.Show()
            _parentForm.Activate()

            Me.Close()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�o�^�{�^���������C�x���g
    '------------------------------------------------------------------------------------------------------
    Private Sub btnEntry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEntry.Click
        Try
            Dim lCntIns As Long = 0
            Dim lCntDel As Long = 0

            '�o�^�`�F�b�N
            Call checkTouroku()

            '�o�^�m�F���b�Z�[�W
            Dim rtn As DialogResult
            Dim sql As String = ""
            sql = "SELECT COUNT(*)"
            sql = sql & N & " FROM T21SEISANM "
            If TEHAI.Equals(_SakuseiCD) Then
                sql = sql & N & " WHERE SAKUSEI_KBN = '" & SAKUSEIKBN_TEHAI & "'"
            Else
                sql = sql & N & " WHERE SAKUSEI_KBN = '" & SAKUSEIKBN_NYUKO & "'"
            End If

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If _db.rmNullStr(ds.Tables(RS).Rows(0)(0)) = 0 Then
                rtn = _msgHd.dspMSG("confirmInsert")         '�o�^���܂��B
            Else
                rtn = _msgHd.dspMSG("confirmNotTorikomi")    '�捞�σf�[�^�j��

                '�폜�����ҏW
                lCntDel = CInt(_db.rmNullStr(ds.Tables(RS).Rows(0)(0)))
            End If
            If rtn <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If

            ' ����Wait�J�[�\����ێ�
            Dim preCursor As Cursor = Me.Cursor
            ' �J�[�\����ҋ@�J�[�\���ɕύX
            Me.Cursor = Cursors.WaitCursor

            Try
                Dim dStartSysdate As Date = Now()                           '�����J�n����
                Dim sPCName As String = UtilClass.getComputerName           '�[��ID


                '�ꗗ�̃f�[�^�����[�N�e�[�u���ɍX�V
                Call updateWK10()

                '�g�����U�N�V�����J�n
                _db.beginTran()

                '���Y�����e�[�u���o�^
                Call torokuT21SEUSANM(dStartSysdate, sPCName)

                '�����I�������̎擾
                Dim dFinishSysdate As Date = Now()

                '�ǉ������ҏW
                lCntIns = _addcount

                '���s�����e�[�u���̍X�V����
                Call updT91Rireki(lCntIns, lCntDel, sPCName, dStartSysdate, dFinishSysdate)

                Dim PGID As String = IIf(TEHAI.Equals(_SakuseiCD), PGID1, PGID2)
                _parentForm.updateSeigyoTbl(PGID, True, dStartSysdate, dFinishSysdate)

                '�g�����U�N�V�����I��
                _db.commitTran()

                
                '�`�F�b�N�{�b�N�X���X�V����
                Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)
                For i As Integer = gh.getMaxRow - 1 To 0 Step -1
                    If Not gh.getCellData(COLDT_TAISYOGAI, i).Equals(gh.getCellData(COLDT_TAISYOGAICOPY, i)) Then
                        gh.setCellData(COLDT_TAISYOGAICOPY, i, gh.getCellData(COLDT_TAISYOGAI, i))
                    End If
                Next

                '�ύX�t���O�𖳌��ɂ���
                _changeFlg = False

                '�������b�Z�[�W
                _msgHd.dspMSG("completeInsert")

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

    '------------------------------------------------------------------------------------------------------
    '�S�I���{�^���������C�x���g
    '------------------------------------------------------------------------------------------------------
    Private Sub btnZenSentaku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZenSentaku.Click
        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)

            '���ڂ����ׂă`�F�b�N������
            For i As Integer = gh.getMaxRow - 1 To 0 Step -1
                gh.setCellData(COLDT_TAISYOGAI, i, True)
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�S�����{�^���������C�x���g
    '------------------------------------------------------------------------------------------------------
    Private Sub btnZenKaijo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZenKaijo.Click
        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)

            '�`�F�b�N����Ă��鍀�ڂ����ׂĉ�������
            For i As Integer = gh.getMaxRow - 1 To 0 Step -1
                gh.setCellData(COLDT_TAISYOGAI, i, False)
            Next

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
            '�쐬�敪�̕ҏW
            If TEHAI.Equals(_SakuseiCD) Then
                '��z�σf�[�^�捞�̏ꍇ
                rdoKubun1.Checked = True
                rdoKubun2.Enabled = False
            Else
                '���ɍσf�[�^�捞�̏ꍇ
                rdoKubun2.Checked = True
                rdoKubun1.Enabled = False
            End If

            '�����N���A�v��N���\��
            Call dispDate()

            '�[��ID�̎擾
            _tanmatuID = UtilClass.getComputerName

            '�ꗗ�\��
            Call dispdgv()

            '�ꗗ�s���F�t���O��L���ɂ���
            _colorCtlFlg = True

            '' 2011/01/13 del start sugano
            '#�o�^�{�^���̐���^�C�~���O��ύX
            'btnEntry.Enabled = _updFlg
            '' 2011/01/13 del end sugano

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��V�X�e���G���[MSG�o�͌�X���[
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�R���g���[���L�[�����C�x���g
    '�@(�����T�v)�G���^�[�{�^���������Ɏ��̃R���g���[���Ɉڂ�
    '-------------------------------------------------------------------------------
    Private Sub ctl_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles rdoKubun1.KeyPress, _
                                                                                                                rdoKubun2.KeyPress, _
                                                                                                                btnZenKaijo.KeyPress, _
                                                                                                                btnZenSentaku.KeyPress, _
                                                                                                                btnEntry.KeyPress, _
                                                                                                                btnBack.KeyPress
        '�����L�[��Enter�̏ꍇ�A���̃R���g���[���փt�H�[�J�X�ړ�
        Call UtilClass.moveNextFocus(Me, e)
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@��������SQL�쐬
    '�@(�����T�v)SQL���쐬����
    '�@�@I�@�F�@prmSakuseiCD�@�@�@'�쐬�敪
    '�@�@R�@�F�@createSerch       '��������
    '------------------------------------------------------------------------------------------------------
    Private Function createSerch(ByVal prmSakuseiCD As String) As String
        Try

            createSerch = ""
            createSerch = "INSERT INTO ZG210E_W10 ("
            createSerch = createSerch & N & " HINMEICD "        '���i���R�[�h
            createSerch = createSerch & N & " ,SIYOU_CD "       '�d�l�R�[�h
            createSerch = createSerch & N & " ,HIN_CD "         '�i��R�[�h
            createSerch = createSerch & N & " ,SENSIN_CD "      '���S���R�[�h
            createSerch = createSerch & N & " ,SIZE_CD "        '�T�C�Y�R�[�h
            createSerch = createSerch & N & " ,COLOR_CD "       '�F�R�[�h
            createSerch = createSerch & N & " ,HINMEI "         '�i��
            createSerch = createSerch & N & " ,KHINMEICD "      '�v��i���R�[�h
            createSerch = createSerch & N & " ,NEN "            '�N
            createSerch = createSerch & N & " ,TEHAINO "        '��z��
            createSerch = createSerch & N & " ,RENBAN "         '�A��
            createSerch = createSerch & N & " ,SEIBAN "         '����
            createSerch = createSerch & N & " ,TEHAI_KBN "      '��z�敪
            createSerch = createSerch & N & " ,TANCYO "         '�P��
            createSerch = createSerch & N & " ,JYOSU "          '��
            createSerch = createSerch & N & " ,TEHAISU "        '��z��
            createSerch = createSerch & N & " ,KYUTTAIBI "      '��]�o����
            createSerch = createSerch & N & " ,YSYUTTAIBI "     '�\��o����
            createSerch = createSerch & N & " ,NYUUKOSU "       '���ɐ�
            createSerch = createSerch & N & " ,MINOUZAN "       '���[�c
            createSerch = createSerch & N & " ,SMIKOMISU "      '���Y����
            createSerch = createSerch & N & " ,NENGETSU "       '�N��
            createSerch = createSerch & N & " ,NYUKO_FLG "      '�����t���O
            If NYUKO.Equals(prmSakuseiCD) Then
                createSerch = createSerch & N & " ,NYUKOBI "      '���ɓ�
            End If
            createSerch = createSerch & N & " ,SAKUSEI_KBN "    '�쐬�敪
            createSerch = createSerch & N & " ,TAISYO_FLG "     '�Ώۃt���O
            createSerch = createSerch & N & " ,UPDNAME "        '�[��ID
            createSerch = createSerch & N & " ,UPDDATE) "       '�X�V����
            createSerch = createSerch & N & " SELECT "
            createSerch = createSerch & N & "   RPAD(T03.SHIYOCD, 2) || T03.HINSYUCD || T03.SENSINSUCD || T03.SIZECD || T03.IROCD "
            createSerch = createSerch & N & "   , T03.SHIYOCD "
            createSerch = createSerch & N & "   , T03.HINSYUCD "
            createSerch = createSerch & N & "   , T03.SENSINSUCD "
            createSerch = createSerch & N & "   , T03.SIZECD "
            createSerch = createSerch & N & "   , T03.IROCD "
            createSerch = createSerch & N & "   , T03.HINNM "
            createSerch = createSerch & N & "   , M12.KHINMEICD "

            If NYUKO.Equals(prmSakuseiCD) Then
                createSerch = createSerch & N & "   , T04.NENDO "
                createSerch = createSerch & N & "   , T04.TEHAINO "
                createSerch = createSerch & N & "   , T04.NYUKONO "
            Else
                createSerch = createSerch & N & "   , T03.NENDO "
                createSerch = createSerch & N & "   , T03.TEHAINO "
                createSerch = createSerch & N & "   , " & RENBAN & " "
            End If

            createSerch = createSerch & N & "   , T03.SEIBAN "
            createSerch = createSerch & N & "   , T03.TEHAIKBN "
            createSerch = createSerch & N & "   , T03.TANTYO "
            createSerch = createSerch & N & "   , T03.INSU "
            createSerch = createSerch & N & "   , T03.TEHAISU "
            createSerch = createSerch & N & "   , T03.KIBOUDATE "
            createSerch = createSerch & N & "   , T03.YOTEIDATE "

            If NYUKO.Equals(prmSakuseiCD) Then
                createSerch = createSerch & N & "   , T04.NYUKOSUU "
                createSerch = createSerch & N & "   , " & MINOUZAN & " "
                createSerch = createSerch & N & "   , T04.NYUKOSUU "
            Else
                createSerch = createSerch & N & "   , T03.NYUKOSUM "
                createSerch = createSerch & N & "   , T03.MINOUZAN "
                createSerch = createSerch & N & "   , T03.MINOUZAN "
            End If

            createSerch = createSerch & N & "   , TO_CHAR(TO_DATE(T03.KIBOUDATE,'YYYYMMDD'),'yyyymm') "

            If NYUKO.Equals(prmSakuseiCD) Then
                createSerch = createSerch & N & "   , CASE WHEN T04.NYUKOSUU > 0 THEN '" & NYUKOFLG & "' END"
                createSerch = createSerch & N & "   , T04.NYUKODATE "
                createSerch = createSerch & N & "   , '" & SAKUSEIKBN_NYUKO & "'"
            Else
                createSerch = createSerch & N & "   , CASE WHEN T03.NYUKOSUM > 0 THEN '" & NYUKOFLG & "' END"
                createSerch = createSerch & N & "   , '" & SAKUSEIKBN_TEHAI & "'"
            End If

            createSerch = createSerch & N & "   , '" & TAISYOFLG & "'"
            createSerch = createSerch & N & "   , '" & _tanmatuID & "'"
            createSerch = createSerch & N & "   , TO_DATE('" & _updateDate & "', 'YYYY/MM/DD HH24:MI:SS')"
            createSerch = createSerch & N & " FROM  T03MINOU T03 "
            createSerch = createSerch & N & "   INNER JOIN M12SYUYAKU M12 "
            createSerch = createSerch & N & "   ON SUBSTR(T03.COSTCD,0,13) = M12.HINMEICD "
            createSerch = createSerch & N & "   INNER JOIN M11KEIKAKUHIN M11 "
            createSerch = createSerch & N & "   ON M11.TT_KHINMEICD = M12.KHINMEICD "

            If NYUKO.Equals(prmSakuseiCD) Then
                createSerch = createSerch & N & "   INNER JOIN T04NYUKO T04 "
                createSerch = createSerch & N & "   ON T03.NENDO = T04.NENDO "
                createSerch = createSerch & N & "   AND T03.TEHAINO = T04.TEHAINO "
                createSerch = createSerch & N & " WHERE TO_CHAR(TO_DATE(T04.NYUKODATE,'YYYYMMDD'),'yyyy/mm') = '" & lblSyori.Text & "'"
                createSerch = createSerch & N & " AND M11.TT_SYUBETU = '1'" '�݌�
            Else
                createSerch = createSerch & N & " WHERE T03.DELFLG IS NULL"
                createSerch = createSerch & N & " AND T03.NYUFLG = '0'"
                createSerch = createSerch & N & " AND M11.TT_SYUBETU = '1'" '�݌�
            End If

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Function

#End Region

#Region "���[�U��`�֐�:DGV�֘A"

    '------------------------------------------------------------------------------------------------------
    '�@�ꗗ�f�[�^�ҏW�O
    '�@(�����T�v)�ꗗ�̃f�[�^���ύX�����O�̒l��ێ�����
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvList_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles dgvList.CellBeginEdit
        '�ҏW�O�̒l��ۑ�
        _beforeChange = _db.rmNullStr(dgvList(e.ColumnIndex, e.RowIndex).Value.ToString)

        '�w�i�F�̐ݒ�
        '�O���b�h�̐擪��Ƀt�H�[�J�X���ړ������ꍇ�̂�SelectionChanged�C�x���g�Ńt�H�[�J�X������Z���̍s���擾
        '�o���Ȃ��ׁA�����ōēx�w�i�F��ύX
        Call setBackcolor(e.RowIndex, _oldRowIndex)

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�ꗗ�f�[�^�ҏW��
    '�@(�����T�v)�ꗗ�̃f�[�^���ύX���ꂽ�ꍇ�A�ύX�t���O�𗧂āA���v�̒l���ĕ\������
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvList_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvList.CellEndEdit
        Try
            Dim _dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)        'DGV�n���h���̐ݒ�
            Dim RowNo As Integer = dgvList.CurrentCell.RowIndex

            '�ҏW�O�ƒl���ς���Ă����ꍇ�A�t���O�𗧂Ă�
            If Not _beforeChange.Equals(_db.rmNullStr(dgvList(e.ColumnIndex, e.RowIndex).Value.ToString)) Then
                '�Ώۍs�ɕύX�t���O�𗧂Ă�
                dgvList(COLNO_HENKOUFLG, RowNo).Value = HENKO_FLG
                _changeFlg = True
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   �ꗗ�@�ҏW�`�F�b�N�iEditingControlShowing�C�x���g�j
    '   �i�����T�v�j���͂̐�����������
    '-------------------------------------------------------------------------------
    Private Sub dgvList_EditingControlShowing(ByVal sender As Object, _
                                              ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs _
                                              ) Handles dgvList.EditingControlShowing

        Try
            Dim _dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)        'DGV�n���h���̐ݒ�
            '�����Y�����̏ꍇ
            If dgvList.CurrentCell.ColumnIndex = COLNO_SEISAN Then
                '���O���b�h�ɁA���l���̓��[�h�̐�����������
                _chkCellVO = _dgv.chkCell(sender, e, UtilDataGridViewHandler.chkType.Num)
            Else
                If Not _chkCellVO Is Nothing Then
                    _dgv.AfterchkCell(_chkCellVO)
                End If
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '   �I���Z�����؃C�x���g�iDataError�C�x���g�j
    '   �i�����T�v�j���l���͗��ɐ��l�ȊO�����͂��ꂽ�ꍇ�̃G���[����
    '-------------------------------------------------------------------------------
    Private Sub dgvList_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvList.DataError

        Try
            e.Cancel = False                                   '�ҏW���[�h�I��

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)
            '�����Y�����̏ꍇ�A�O���b�h�ɂ͐��l���̓��[�h(0�`9)�̐����������Ă���̂ŁA�����̉���
            If dgvList.CurrentCell.ColumnIndex = COLNO_SEISAN Then
                gh.AfterchkCell(_chkCellVO)
            End If

            '���̓G���[�t���O�𗧂Ă�
            _nyuuryokuErrFlg = True

            Dim colName As String = ""
            colName = COLCN_SEISAN


            '�G���[�Z���Ƀt�H�[�J�X�����Ă�
            _errSet = gh.readyForErrSet(e.RowIndex, colName)

            '�G���[���b�Z�[�W�\��
            Throw New UsrDefException("���p�����̂ݓ��͉\�ł��B", _msgHd.getMSG("onlyAcceptNumeric"))

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�@�O���b�h�t�H�[�J�X�ݒ�y�ёI���s�ɒ��F���鏈��
    '�@�@(�����T�v�j�Z���ҏW��ɃG���[�ɂȂ����ꍇ�ɁA�G���[�Z���Ƀt�H�[�J�X��߂��B
    '               �I���s�ɒ��F����B
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvList_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvList.SelectionChanged
        Try

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)

            '���̓G���[���������ꍇ
            If _nyuuryokuErrFlg Then
                _nyuuryokuErrFlg = False
                gh.setCurrentCell(_errSet)
            End If

            If _colorCtlFlg Then
                '�w�i�F�̐ݒ�
                Call setBackcolor(dgvList.CurrentCellAddress.Y, _oldRowIndex)
            End If
            _oldRowIndex = dgvList.CurrentCellAddress.Y

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�w�i�F�̐ݒ菈��
    '�@(�����T�v)�s�̔w�i�F��ɒ��F����B
    '�@�@I�@�F�@prmRowIndex     ���݃t�H�[�J�X������s��
    '�@�@I�@�F�@prmOldRowIndex  ���݂̍s�Ɉڂ�O�̍s��
    '------------------------------------------------------------------------------------------------------
    Private Sub setBackcolor(ByVal prmRowIndex As Integer, ByVal prmOldRowIndex As Integer)

        Dim _dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)

        '�w�肵���s�̔w�i�F��ɂ���
        _dgv.setSelectionRowColor(prmRowIndex, prmOldRowIndex, StartUp.lCOLOR_BLUE)

        _oldRowIndex = prmRowIndex

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�w���ւ̃t�H�[�J�X�ݒ菈��
    '�@(�����T�v)�w�肳�ꂽ�Z���Ƀt�H�[�J�X����B
    '�@�@I�@�F�@prmCoIndex      �t�H�[�J�X������Z���̗�
    '�@�@I�@�F�@prmRowIndex     �t�H�[�J�X������Z���̍s��
    '------------------------------------------------------------------------------------------------------
    Private Sub setForcusCol(ByVal prmColIndex As Integer, ByVal prmRowIndex As Integer)

        '�t�H�[�J�X�����Ă�
        dgvList.Focus()
        dgvList.CurrentCell = dgvList(prmColIndex, prmRowIndex)

        '�w�i�F�̐ݒ�
        Call setBackcolor(prmRowIndex, _oldRowIndex)

    End Sub
#End Region

#Region "���[�U��`�֐�:DB�֘A"
    '-------------------------------------------------------------------------------
    '�@�����N���A�v��N���\��
    '�@(�����T�v)�����N���A�v��N����\������
    '-------------------------------------------------------------------------------
    Private Sub dispDate()
        Try
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & " SNENGETU " & "SYORI"          '�����N��
            sql = sql & N & " ,KNENGETU " & "KEIKAKU"       '�v��N��
            sql = sql & N & " FROM T01KEIKANRI "

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                Throw New UsrDefException("�o�^����Ă��܂���B", _msgHd.getMSG("noData"))
            End If

            Dim syoriDate As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("SYORI"))
            Dim keikakuDate As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("KEIKAKU"))

            '�uYYYY/MM�v�`���ŕ\��
            lblSyori.Text = syoriDate.Substring(0, 4) & "/" & syoriDate.Substring(4, 2)
            lblKeikaku.Text = keikakuDate.Substring(0, 4) & "/" & keikakuDate.Substring(4, 2)

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@�ꗗ�\��
    '�@(�����T�v)�ꗗ�\���f�[�^��WK01�ɕێ����A�ꗗ�ɕ\������
    '-------------------------------------------------------------------------------
    Private Sub dispdgv()
        Try

            '�g�����U�N�V�����J�n
            _db.beginTran()

            Dim sql As String = ""
            sql = " DELETE FROM ZG210E_W10 WHERE UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql)

            '�X�V�������擾
            _updateDate = Now

            If TEHAI.Equals(_SakuseiCD) Then
                'T03���[�c�e�[�u���AM12�W�񏤕i�}�X�^
                sql = createSerch(_SakuseiCD)
            Else
                'T03���[�c�e�[�u���AT04���ɏ󋵃e�[�u��
                sql = createSerch(_SakuseiCD)
            End If
            _db.executeDB(sql)


            'M11�v��Ώەi�}�X�^
            sql = ""
            sql = sql & N & "UPDATE ZG210E_W10 W10"
            sql = sql & N & "SET (JUYOU_CD, HINSYU_KBN) = ("
            sql = sql & N & " SELECT M11.TT_JUYOUCD, M11.TT_HINSYUKBN FROM M11KEIKAKUHIN M11 "
            sql = sql & N & " WHERE M11.TT_KHINMEICD = W10.KHINMEICD) "
            sql = sql & N & "WHERE W10.UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql)

            'M10�ėp�}�X�^
            '�����t���O��
            sql = ""
            sql = sql & N & "UPDATE ZG210E_W10 W10"
            sql = sql & N & "SET (NYUKO_FLGNM) = ("
            sql = sql & N & " SELECT M01.NAME1 FROM M01HANYO M01 "
            sql = sql & N & " WHERE M01.KOTEIKEY = '" & COTEI_NYUKO & "' "
            sql = sql & N & " AND M01.KAHENKEY = W10.NYUKO_FLG) "
            sql = sql & N & "WHERE W10.UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql)

            '��z�敪��
            sql = ""
            sql = sql & N & "UPDATE ZG210E_W10 W10"
            sql = sql & N & "SET (TEHAI_KBNNM) = ("
            sql = sql & N & " SELECT M01.NAME1 FROM M01HANYO M01 "
            sql = sql & N & " WHERE M01.KOTEIKEY = '" & COTEI_TEHAI & "' "
            sql = sql & N & " AND M01.KAHENKEY = W10.TEHAI_KBN) "
            sql = sql & N & "WHERE W10.UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql)

            '���v�於�A���v��\����
            sql = ""
            sql = sql & N & "UPDATE ZG210E_W10 W10"
            sql = sql & N & "SET (JUYOU_NM,JUYOU_SORT) = ("
            sql = sql & N & " SELECT M01.NAME2,M01.SORT FROM M01HANYO M01 "
            sql = sql & N & " WHERE M01.KOTEIKEY = '" & COTEI_JUYOU & "' "
            sql = sql & N & " AND M01.KAHENKEY = W10.JUYOU_CD) "
            sql = sql & N & "WHERE W10.UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql)

            '�g�����U�N�V�����I��
            _db.commitTran()

            '�ꗗ�\��
            Call dispWK10()

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        Finally
            If _db.isTransactionOpen = True Then
                _db.rollbackTran()                          '���[���o�b�N
            End If
            '�}�E�X�J�[�\�����
            Me.Cursor = Cursors.Arrow
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@���[�N�e�[�u���f�[�^�̈ꗗ�\��
    '�@(�����T�v)���[�N�e�[�u���̃f�[�^���ꗗ�ɕ\������
    '-------------------------------------------------------------------------------
    Private Sub dispWK10()
        Try
            '���[�N�̃f�[�^���ꗗ�ɕ\��
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)

            gh.clearRow()
            dgvList.Enabled = False

            Dim sql As String = ""
            sql = sql & N & " SELECT "
            sql = sql & N & "  CASE TAISYO_FLG WHEN '" & TAISYO & "' THEN '" & TAISYO_ARI & "' ELSE '" & TAISYO_GAI & "' END " & COLDT_TAISYOGAI       '�Ώ�
            sql = sql & N & " ,CASE TAISYO_FLG WHEN '" & TAISYO & "' THEN '" & TAISYO_ARI & "' ELSE '" & TAISYO_GAI & "' END " & COLDT_TAISYOGAICOPY   '�ΏۃR�s�[
            sql = sql & N & " ,NYUKO_FLGNM " & COLDT_UCHIIRE      '����
            sql = sql & N & " ,TEHAI_KBNNM " & COLDT_TEHAIKBN     '��z
            sql = sql & N & " ,TO_CHAR(TO_DATE(KYUTTAIBI,'YYYYMMDD'),'yy/mm/dd') " & COLDT_SYUTTAIBI      '��]�o����
            sql = sql & N & " ,TO_CHAR(TO_DATE(YSYUTTAIBI,'YYYYMMDD'),'yy/mm/dd') " & COLDT_YOTEI         '�\��o����
            sql = sql & N & " ,TEHAINO " & COLDT_TEHAINO          '��z��
            sql = sql & N & " ,SEIBAN " & COLDT_SEIBAN            '����
            sql = sql & N & " ,HINMEI " & COLDT_HINMEI            '�i��
            sql = sql & N & " ,TEHAISU " & COLDT_TEHAISU          '��z��
            sql = sql & N & " ,TANCYO " & COLDT_TANTYOU           '�P��
            sql = sql & N & " ,JYOSU " & COLDT_JOUSUU             '��
            sql = sql & N & " ,SMIKOMISU " & COLDT_SEISAN         '���Y����
            sql = sql & N & " ,CASE WHEN NENGETSU IS NULL THEN '' ELSE TO_CHAR(TO_DATE(NENGETSU,'YYYYMM'),'yyyy/mm') END " & COLDT_TUKI            '�N��
            sql = sql & N & " ,HINMEICD " & COLDT_HINCD           '�����i�R�[�h
            sql = sql & N & " ,CASE KHINMEICD WHEN HINMEICD THEN '' ELSE KHINMEICD END " & COLDT_OYACD          '�v��i���R�[�h
            sql = sql & N & " ,JUYOU_NM " & COLDT_JUYOSAKI        '���v�於
            sql = sql & N & " ,HINSYU_KBN " & COLDT_HINSYU        '�i��敪
            sql = sql & N & " ,NEN " & COLDT_NEN                  '�N�@�@�@�@(�B����)
            sql = sql & N & " ,RENBAN " & COLDT_RENBAN            '�A�ԁ@�@�@(�B����)
            sql = sql & N & " ,NULL " & COLDT_HENKOUFLG           '�ύX�t���O(�B����)
            sql = sql & N & " FROM ZG210E_W10 "
            sql = sql & N & "   WHERE UPDNAME = '" & _tanmatuID & "'"
            '-->2010.12.25 chg by takagi #44
            '�\�[�g�L�[�ύX�i�i�큨���S�����T�C�Y���d�l���F�j
            'sql = sql & N & " ORDER BY JUYOU_SORT, HINSYU_KBN, HIN_CD, SENSIN_CD, SIZE_CD, SIYOU_CD, COLOR_CD, TEHAINO"
            sql = sql & N & " ORDER BY HIN_CD, SENSIN_CD, SIZE_CD, SIYOU_CD, COLOR_CD, TEHAINO"
            '<--2010.12.25 chg by takagi #44

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            '' 2011/01/13 add start sugano
            '�o�^�{�^���͈ꗗ�̌����ɂ�����炸���䂷��
            btnEntry.Enabled = _updFlg
            '' 2011/01/13 add end sugano

            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                dgvList.Enabled = False         '�ꗗ�̎g�p�s��
                '' 2011/01/13 del start sugano
                'btnEntry.Enabled = False
                '' 2011/01/13 del end sugano
                btnZenSentaku.Enabled = False
                btnZenKaijo.Enabled = False

                '�ꗗ�̌�����\������
                txtCnt.Text = CStr(iRecCnt) & "��"
                '�\�������̕ۑ�
                _addcount = CLng(iRecCnt)

                Throw New UsrDefException("�Ώۃf�[�^������܂���B", _msgHd.getMSG("NonData"))
            Else                                    '���o�f�[�^������ꍇ�A�o�^�{�^���L��
                dgvList.Enabled = True          '�ꗗ�̎g�p�s��
                '' 2011/01/13 del start sugano
                'btnEntry.Enabled = _updFlg
                '' 2011/01/13 del end sugano
                btnZenSentaku.Enabled = True
                btnZenKaijo.Enabled = True
            End If

            '���o�f�[�^���ꗗ�ɕ\������
            dgvList.DataSource = ds
            dgvList.DataMember = RS

            '�ꗗ�̌�����\������
            txtCnt.Text = CStr(iRecCnt) & "��"
            '�\�������̕ۑ�
            _addcount = CLng(iRecCnt)

            '�ꗗ�擪�s�I��--------------------------------------------------------------
            gh.setSelectionRowColor(0, 0, StartUp.lCOLOR_BLUE)

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@���[�N�e�[�u���f�[�^�̍X�V
    '�@(�����T�v)�ꗗ�ɕ\������Ă���f�[�^�����[�N�e�[�u���ɍX�V����
    '-------------------------------------------------------------------------------
    Private Sub updateWK10()
        Try

            Dim sql As String = ""
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)

            '�g�����U�N�V�����J�n
            _db.beginTran()

            '�s�����������[�v
            For i As Integer = 0 To gh.getMaxRow - 1
                If HENKO_FLG.Equals(dgvList(COLNO_HENKOUFLG, i).Value.ToString) Or Not gh.getCellData(COLDT_TAISYOGAI, i).Equals(gh.getCellData(COLDT_TAISYOGAICOPY, i)) Then
                    sql = ""
                    sql = sql & N & " UPDATE ZG210E_W10 SET "
                    sql = sql & N & " SMIKOMISU = TO_NUMBER('" & _db.rmNullStr(dgvList(COLNO_SEISAN, i).Value) & "') "
                    sql = sql & N & " ,NENGETSU = '" & Trim(Replace(_db.rmNullStr(dgvList(COLNO_TUKI, i).Value), "/", "")) & "' "
                    If TAISYO_ARI.Equals(gh.getCellData(COLDT_TAISYOGAI, i)) Then
                        sql = sql & N & " ,TAISYO_FLG = '" & TAISYO & "' "
                    Else
                        sql = sql & N & " ,TAISYO_FLG = ''"
                    End If
                    sql = sql & N & "   WHERE UPDNAME = '" & _tanmatuID & "'"
                    sql = sql & N & "   AND NEN = '" & dgvList(COLNO_NEN, i).Value & "'"
                    sql = sql & N & "   AND TEHAINO = '" & dgvList(COLNO_TEHAINO, i).Value & "'"
                    sql = sql & N & "   AND RENBAN = '" & dgvList(COLNO_RENBAN, i).Value & "'"
                    _db.executeDB(sql)
                End If
            Next

            '�g�����U�N�V�����I��
            _db.commitTran()

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@���s�����e�[�u���̒ǉ�����
    '  (�����T�v)���s�����e�[�u���Ƀ��R�[�h��ǉ�����
    '�@�@I�@�F�@prmCntIns       �@ �o�^����
    '�@�@I�@�F�@prmCntDel        �@�폜����
    '�@�@I�@�F�@prmPCName      �@�@�[��ID
    '�@�@I�@�F�@prmStartDate       �����J�n����
    '�@�@I�@�F�@prmFinishDate      �����I������
    '------------------------------------------------------------------------------------------------------
    Private Sub updT91Rireki(ByVal prmCntIns As Long, ByVal prmCntDel As Long, ByVal prmPCName As String, _
                             ByVal prmStartDate As Date, ByVal prmFinishDate As Date)
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
            sql = sql & N & ", KENNSU2"                                                     '�����Q�i�o�^�����j
            sql = sql & N & ", UPDNAME"                                                     '�[��ID
            sql = sql & N & ", UPDDATE"                                                     '�X�V����
            sql = sql & N & ") VALUES ("

            sql = sql & N & "  '" & Trim(Replace(lblSyori.Text, "/", "")) & "'"             '�����N��
            sql = sql & N & ", '" & Trim(Replace(lblKeikaku.Text, "/", "")) & "'"           '�v��N��
            If TEHAI.Equals(_SakuseiCD) Then
                sql = sql & N & ",  '" & PGID1 & "'"                                        '�@�\ID(��z�σf�[�^)
            Else
                sql = sql & N & ",  '" & PGID2 & "'"                                        '�@�\ID(���ɍσf�[�^)
            End If

            sql = sql & N & ", TO_DATE('" & prmStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "   '�����J�n����
            sql = sql & N & ", TO_DATE('" & prmFinishDate & "', 'YYYY/MM/DD HH24:MI:SS') "  '�����I������
            sql = sql & N & ", " & prmCntDel                                                '�����P�i�폜�����j
            sql = sql & N & ", " & prmCntIns                                                '�����Q�i�o�^�����j
            sql = sql & N & ", '" & prmPCName & "'"                                         '�[��ID
            sql = sql & N & ", TO_DATE('" & prmFinishDate & "', 'YYYY/MM/DD HH24:MI:SS') "  '�X�V����
            sql = sql & N & " )"
            _db.executeDB(sql)

        Catch ex As Exception
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@���Y�����e�[�u���̓o�^����
    '�@�@I�@�F�@prmSysdate       �����J�n����
    '�@�@I�@�F�@prmPCName      �@�[��ID
    '------------------------------------------------------------------------------------------------------
    Private Sub torokuT21SEUSANM(ByVal prmSysdate As Date, ByVal prmPCName As String)
        Try
            '�폜����
            'SQL�����s
            Dim sql As String = ""
            sql = "DELETE FROM T21SEISANM"
            If TEHAI.Equals(_SakuseiCD) Then
                sql = sql & N & " WHERE SAKUSEI_KBN = '" & SAKUSEIKBN_TEHAI & "'"
            Else
                sql = sql & N & " WHERE SAKUSEI_KBN = '" & SAKUSEIKBN_NYUKO & "'"
            End If
            _db.executeDB(sql)

            '�o�^����
            'SQL�����s
            sql = ""
            sql = "INSERT INTO T21SEISANM ("
            sql = sql & N & "  HINMEICD"                                                  '���i���R�[�h
            sql = sql & N & ", SIYOU_CD"                                                  '�d�l�R�[�h
            sql = sql & N & ", HIN_CD"                                                    '�i��R�[�h
            sql = sql & N & ", SENSIN_CD"                                                 '���S���R�[�h
            sql = sql & N & ", SIZE_CD"                                                   '�T�C�Y�R�[�h
            sql = sql & N & ", COLOR_CD"                                                  '�F�R�[�h
            sql = sql & N & ", HINMEI"                                                    '�i��
            sql = sql & N & ", KHINMEICD"                                                 '�v��i���R�[�h
            sql = sql & N & ", NEN"                                                       '�N
            sql = sql & N & ", TEHAINO"                                                   '��z��
            sql = sql & N & ", RENBAN"                                                    '�A��
            sql = sql & N & ", SEIBAN"                                                    '����
            sql = sql & N & ", TEHAI_KBN"                                                 '��z�敪
            sql = sql & N & ", TANCYO"                                                    '�P��
            sql = sql & N & ", JYOSU"                                                     '��
            sql = sql & N & ", TEHAISU"                                                   '��z��
            sql = sql & N & ", KYUTTAIBI"                                                 '��]�o����
            sql = sql & N & ", YSYUTTAIBI"                                                '�\��o����
            sql = sql & N & ", NYUUKOSU"                                                  '���ɐ�
            sql = sql & N & ", MINOUZAN"                                                  '���[�c
            sql = sql & N & ", SMIKOMISU"                                                 '���Y������
            sql = sql & N & ", NENGETSU"                                                  '�N��
            sql = sql & N & ", JUYOU_CD"                                                  '���v��
            sql = sql & N & ", HINSYU_KBN"                                                '�i��敪
            sql = sql & N & ", NYUKO_FLG"                                                 '�����t���O
            sql = sql & N & ", TAISYO_FLG"                                                '�Ώۃt���O
            sql = sql & N & ", SAKUSEI_KBN"                                               '�쐬�敪
            sql = sql & N & ", NYUKOBI"                                                   '���ɓ�
            sql = sql & N & ", UPDNAME"                                                   '�[��ID
            sql = sql & N & ", UPDDATE )"                                                 '�X�V����
            sql = sql & N & " SELECT "
            sql = sql & N & "  HINMEICD"                                                  '���i���R�[�h
            sql = sql & N & ", SIYOU_CD"                                                  '�d�l�R�[�h
            sql = sql & N & ", HIN_CD"                                                    '�i��R�[�h
            sql = sql & N & ", SENSIN_CD"                                                 '���S���R�[�h
            sql = sql & N & ", SIZE_CD"                                                   '�T�C�Y�R�[�h
            sql = sql & N & ", COLOR_CD"                                                  '�F�R�[�h
            sql = sql & N & ", HINMEI"                                                    '�i��
            sql = sql & N & ", KHINMEICD"                                                 '�v��i���R�[�h
            sql = sql & N & ", NEN"                                                       '�N
            sql = sql & N & ", TEHAINO"                                                   '��z��
            sql = sql & N & ", RENBAN"                                                    '�A��
            sql = sql & N & ", SEIBAN"                                                    '����
            sql = sql & N & ", TEHAI_KBN"                                                 '��z�敪
            sql = sql & N & ", TANCYO"                                                    '�P��
            sql = sql & N & ", JYOSU"                                                     '��
            sql = sql & N & ", TEHAISU"                                                   '��z��
            sql = sql & N & ", KYUTTAIBI"                                                 '��]�o����
            sql = sql & N & ", YSYUTTAIBI"                                                '�\��o����
            sql = sql & N & ", NYUUKOSU"                                                  '���ɐ�
            sql = sql & N & ", MINOUZAN"                                                  '���[�c
            sql = sql & N & ", SMIKOMISU"                                                 '���Y������
            sql = sql & N & ", NENGETSU"                                                  '�N��
            sql = sql & N & ", JUYOU_CD"                                                  '���v��
            sql = sql & N & ", HINSYU_KBN"                                                '�i��敪
            sql = sql & N & ", NYUKO_FLG"                                                 '�����t���O
            sql = sql & N & ", TAISYO_FLG"                                                '�Ώۃt���O
            sql = sql & N & ", SAKUSEI_KBN"                                               '�쐬�敪
            sql = sql & N & ", NYUKOBI"                                                   '���ɓ�
            sql = sql & N & ", UPDNAME "                                                  '�[��ID
            sql = sql & N & ", TO_DATE('" & prmSysdate & "', 'YYYY/MM/DD HH24:MI:SS')"
            sql = sql & N & " FROM ZG210E_W10 "
            sql = sql & N & " WHERE UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql)


        Catch ex As Exception
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub
#End Region

#Region "���[�U��`�֐�:�`�F�b�N����"
    '------------------------------------------------------------------------------------------------------
    '  �o�^�`�F�b�N
    '�@(�����T�v)�e���ڂ̕K�{���ځA�`���`�F�b�N���s��
    '------------------------------------------------------------------------------------------------------
    Private Sub checkTouroku()
        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)

            For i As Integer = 0 To gh.getMaxRow - 1
                If TAISYO_ARI.Equals(gh.getCellData(COLDT_TAISYOGAI, i)) Then
                    '�K�{���̓`�F�b�N
                    '���Y����
                    Call checkHissu(COLDT_SEISAN, "���Y����", i, COLNO_SEISAN)
                    '�N��
                    Call checkHissu(COLDT_TUKI, "�N��", i, COLNO_TUKI)
                End If
                '�N�����󔒈ȊO�̏ꍇ
                If Not "".Equals(gh.getCellData(COLDT_TUKI, i)) Then
                    '���p�����`�F�b�N
                    If UtilClass.isOnlyNStr(gh.getCellData(COLDT_TUKI, i)) = False Then
                        Throw New UsrDefException("���p�p���̂ݓ��͉\�ł��B", _msgHd.getMSG("onlyAcceptHankakuEisu", "�y �N�� �z"))
                    End If

                    '�`���`�F�b�N
                    Call checkFormat(COLDT_TUKI, "�N��", i, COLNO_TUKI)
                End If
            Next

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '  �K�{���̓`�F�b�N
    '�@(�����T�v)�Z�������͂���Ă��邩
    '�@�@I�@�F�@prmColName              �`�F�b�N����Z���̗�
    '�@�@I�@�F�@prmColHeaderName        �G���[���ɕ\�������
    '�@�@I�@�F�@prmCnt                  �`�F�b�N����Z���̍s��
    '�@�@I�@�F�@prmColNo                �`�F�b�N����Z���̗�
    '------------------------------------------------------------------------------------------------------
    Private Sub checkHissu(ByVal prmColName As String, ByVal prmColHeaderName As String, ByVal prmCnt As Integer, ByVal prmColNo As Integer)
        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)

            '�K�{���̓`�F�b�N
            If "".Equals(gh.getCellData(prmColName, prmCnt).ToString) Then
                '�t�H�[�J�X�����Ă�
                Call setForcusCol(prmColNo, prmCnt)
                '�G���[���b�Z�[�W�̕\��
                '-->2010.12.17 chg by takagi #13
                'Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y '" & prmColHeaderName & "' �F" & prmCnt + 1 & "�s�ځz"))
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput"))
                '<--2010.12.17 chg by takagi #13
            End If

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '  �`���`�F�b�N
    '�@(�����T�v)�Z���ɓ��͂��ꂽ�l�̌`������������
    '�@�@I�@�F�@prmColName              �`�F�b�N����Z���̗�
    '�@�@I�@�F�@prmColHeaderName        �G���[���ɕ\�������
    '�@�@I�@�F�@prmCnt                  �`�F�b�N����Z���̍s��
    '�@�@I�@�F�@prmColNo                �`�F�b�N����Z���̗�
    '------------------------------------------------------------------------------------------------------
    Private Sub checkFormat(ByVal prmColName As String, ByVal prmColHeaderName As String, ByVal prmCnt As Integer, ByVal prmColNo As Integer)
        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)

            '�`���`�F�b�N
            If Not IsDigit(gh.getCellData(prmColName, prmCnt).ToString) Then
                '�t�H�[�J�X�����Ă�
                Call setForcusCol(prmColNo, prmCnt)
                '�G���[���b�Z�[�W�̕\��
                '-->2010.12.17 chg by takagi #13
                'Throw New UsrDefException("�N����YYYY/MM�`���œ��͂��Ă��������B", _msgHd.getMSG("ErrTukiFormat", "�y '" & prmColHeaderName & "' �F" & prmCnt + 1 & "�s�ځz"))
                Throw New UsrDefException("�N����YYYY/MM�`���œ��͂��Ă��������B", _msgHd.getMSG("ErrTukiFormat"))
                '<--2010.12.17 chg by takagi #13
            End If

            '���t�`�F�b�N
            If Not IsDate(gh.getCellData(prmColName, prmCnt).ToString & "/01") Then
                '�t�H�[�J�X�����Ă�
                Call setForcusCol(prmColNo, prmCnt)
                '�G���[���b�Z�[�W�̕\��
                '-->2010.12.17 chg by takagi #13
                'Throw New UsrDefException("�����ȓ��t�����͂���Ă��܂��B", _msgHd.getMSG("ImputedInvalidDate", "�y '" & prmColHeaderName & "' �F" & prmCnt + 1 & "�s�ځz"))
                Throw New UsrDefException("�����ȓ��t�����͂���Ă��܂��B", _msgHd.getMSG("ImputedInvalidDate"))
                '<--2010.12.17 chg by takagi #13
            End If


        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '  �`���`�F�b�N
    '�@(�����T�v)�Z���ɓ��͂��ꂽ�l�̌`������������
    '�@�@I�@�F�@prmColName              �`�F�b�N����Z���̓��e
    '------------------------------------------------------------------------------------------------------
    Private Function IsDigit(ByVal Value As String) As Boolean
        Dim K As Long

        If Len(Value) = 0 Then
            IsDigit = False
            Exit Function
        End If

        If Not Len(Value) = 7 Then
            IsDigit = False
            Exit Function
        End If

        For K = 1 To Len(Value)
            If K = 5 Then
                If Not Mid(Value, K, 1) Like "/" Then Exit Function
            Else
                If Not Mid(Value, K, 1) Like "[0-9]" Then Exit Function
            End If
        Next K

        IsDigit = True

    End Function

#End Region

End Class
