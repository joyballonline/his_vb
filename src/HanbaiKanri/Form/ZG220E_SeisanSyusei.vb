'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���j���Y�ʃf�[�^�C��
'    �i�t�H�[��ID�jZG220E_SeisanSyusei
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ���{        2010/10/26                 �V�K              
'�@(2)   ����        2011/01/13                 �ύX�@��������e�[�u���̍X�V�^�C�~���O��ύX              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.Combo
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory

Imports System.Runtime.InteropServices

Public Class ZG220E_SeisanSyusei
    Inherits System.Windows.Forms.Form
    Implements IfRturnUpDDate

#Region "���e�����l��`"
    '------------------------------------------------------------------------------------------------------
    '�����o�[�萔�錾
    '------------------------------------------------------------------------------------------------------

    'PG���䕶�� 
    Private Const N As String = ControlChars.NewLine                    '���s����
    Private Const RS As String = "RecSet"                               '���R�[�h�Z�b�g�e�[�u��

    '�ꗗ�f�[�^�o�C���h��
    Private Const COLDT_TAISYOU As String = "dtTaisyou"         '�Ώ�
    Private Const COLDT_TAISYOUCOPY As String = "dtTaisyouCopy" '�ΏۃR�s�[
    Private Const COLDT_KUBUN As String = "dtKubun"             '�敪
    Private Const COLDT_UCHIIRE As String = "dtUchiire"         '����
    Private Const COLDT_TEHAIKBN As String = "dtTehaiKbn"       '��z
    Private Const COLDT_KIBOU As String = "dtKibou"             '��]
    Private Const COLDT_YOTEI As String = "dtYotei"             '�\��
    Private Const COLDT_TEHAINO As String = "dtTehaino"         '��z��
    Private Const COLDT_SEIBAN As String = "dtSeiban"           '����
    Private Const COLDT_HINMEI As String = "dtHinmei"           '�i��
    Private Const COLDT_TEHAISU As String = "dtTehaisu"         '��z��
    Private Const COLDT_TANCYO As String = "dtTancyo"           '�P��
    Private Const COLDT_JOSU As String = "dtJosu"               '��
    Private Const COLDT_SEISAN As String = "dtSeisan"           '���Y����
    Private Const COLDT_TUKI As String = "dtTuki"               '�N��
    Private Const COLDT_HINCD As String = "dtHincd"             '�i���R�[�h
    Private Const COLDT_OYACD As String = "dtOyacd"             '�e�i���R�[�h
    Private Const COLDT_JUYOSAKI As String = "dtJuyosaki"       '���v��
    Private Const COLDT_HINSYU As String = "dtHinsyu"           '�i��敪
    Private Const COLDT_RECORDID As String = "dtRecordid"       '���R�[�hID    (�B����)
    Private Const COLDT_JUYOSORT As String = "dtJuyoSort"       '���v��\����  (�B����)
    Private Const COLDT_SAKUSEISORT As String = "dtSakuseiSort" '�쐬�敪�\����(�B����)
    Private Const COLDT_HENKOUFLG As String = "dtHenkouflg"     '�ύX�t���O    (�B����)

    '�ꗗ�O���b�h��
    Private Const COLCN_TAISYOU As String = "cnTaisyou"         '�Ώ�
    Private Const COLCN_TAISYOUCOPY As String = "cnTaisyouCopy" '�ΏۃR�s�[
    Private Const COLCN_KUBUN As String = "cnKubun"             '�敪
    Private Const COLCN_UCHIIRE As String = "cnUchiire"         '����
    Private Const COLCN_TEHAIKBN As String = "cnTehaiKbn"       '��z
    Private Const COLCN_KIBOU As String = "cnKibou"             '��]
    Private Const COLCN_YOTEI As String = "cnYotei"             '�\��
    Private Const COLCN_TEHAINO As String = "cnTehaino"         '��z��
    Private Const COLCN_SEIBAN As String = "cnSeiban"           '����
    Private Const COLCN_HINMEI As String = "cnHinmei"           '�i��
    Private Const COLCN_TEHAISU As String = "cnTehaisu"         '��z��
    Private Const COLCN_TANCYO As String = "cnTancyo"           '�P��
    Private Const COLCN_JOSU As String = "cnJosu"               '��
    Private Const COLCN_SEISAN As String = "cnSeisan"           '���Y����
    Private Const COLCN_TUKI As String = "cnTuki"               '�N��
    Private Const COLCN_HINCD As String = "cnHincd"             '�i���R�[�h
    Private Const COLCN_OYACD As String = "cnOyacd"             '�e�i���R�[�h
    Private Const COLCN_JUYOSAKI As String = "cnJuyosaki"       '���v��
    Private Const COLCN_HINSYU As String = "cnHinsyu"           '�i��敪
    Private Const COLCN_RECORDID As String = "cnRecordid"       '���R�[�hID�@�@(�B����)
    Private Const COLCN_JUYOSORT As String = "cnJuyoSort"       '���v��\�����@(�B����)
    Private Const COLCN_SAKUSEISORT As String = "cnSakuseiSort" '�쐬�敪�\����(�B����)
    Private Const COLCN_HENKOUFLG As String = "cnHenkouflg"     '�ύX�t���O�@�@(�B����)

    '�ꗗ��ԍ�
    Private Const COLNO_TAISYOU As Integer = 0        '�Ώ�
    Private Const COLNO_TAISYOUCOPY As Integer = 1    '�ΏۃR�s�[
    Private Const COLNO_KUBUN As Integer = 2          '�敪
    Private Const COLNO_UCHIIRE As Integer = 3        '����
    Private Const COLNO_TEHAIKBN As Integer = 4       '��z
    Private Const COLNO_KIBOU As Integer = 5          '��]
    Private Const COLNO_YOTEI As Integer = 6          '�\��
    Private Const COLNO_TEHAINO As Integer = 7        '��z��
    Private Const COLNO_SEIBAN As Integer = 8         '����
    Private Const COLNO_HINMEI As Integer = 9         '�i��
    Private Const COLNO_TEHAISU As Integer = 10       '��z��
    Private Const COLNO_TANCYO As Integer = 11        '�P��
    Private Const COLNO_JOSU As Integer = 12          '��
    Private Const COLNO_SEISAN As Integer = 13        '���Y����
    Private Const COLNO_TUKI As Integer = 14          '�N��
    Private Const COLNO_HINCD As Integer = 15         '�i���R�[�h
    Private Const COLNO_OYACD As Integer = 16         '�e�i���R�[�h
    Private Const COLNO_JUYOSAKI As Integer = 17      '���v��
    Private Const COLNO_HINSYU As Integer = 18        '�i��敪
    Private Const COLNO_RECORDID As Integer = 19      '���R�[�hID�@�@(�B����)
    Private Const COLNO_JUYOSORT As Integer = 20      '���v��\�����@(�B����)
    Private Const COLNO_SAKUSEISORT As Integer = 21   '�쐬�敪�\����(�B����)
    Private Const COLNO_HENKOUFLG As Integer = 22     '�ύX�t���O�@�@(�B����)

    'M01�ėp�}�X�^�Œ跰
    Private Const COTEI_JUYOU As String = "01"                      '���v�於
    Private Const COTEI_TEHAI As String = "02"                      '��z�敪
    Private Const COTEI_SAKUSEI As String = "15"                    '�쐬�敪
    Private Const COTEI_NYUKO As String = "18"                      '�����t���O

    'EXCEL
    Private Const START_PRINT As Integer = 7        'EXCEL�o�͊J�n�s��

    'EXCEL��ԍ�
    Private Const XLSCOL_TAISYOU As Integer = 1        '�Ώ�
    Private Const XLSCOL_KUBUN As Integer = 2          '�敪
    Private Const XLSCOL_UCHIIRE As Integer = 3        '����
    Private Const XLSCOL_TEHAIKBN As Integer = 4       '��z
    Private Const XLSCOL_KIBOU As Integer = 5          '��]
    Private Const XLSCOL_YOTEI As Integer = 6          '�\��
    Private Const XLSCOL_TEHAINO As Integer = 7        '��z��
    Private Const XLSCOL_SEIBAN As Integer = 8         '����
    Private Const XLSCOL_HINMEI As Integer = 9         '�i��
    Private Const XLSCOL_TEHAISU As Integer = 10       '��z��
    Private Const XLSCOL_TANCYO As Integer = 11        '�P��
    Private Const XLSCOL_JOSU As Integer = 12          '��
    Private Const XLSCOL_SEISAN As Integer = 13        '���Y����
    Private Const XLSCOL_TUKI As Integer = 14          '�N��
    Private Const XLSCOL_HINCD As Integer = 15         '�i���R�[�h
    Private Const XLSCOL_OYACD As Integer = 16         '�e�i���R�[�h
    Private Const XLSCOL_JUYOSAKI As Integer = 17      '���v��
    Private Const XLSCOL_HINSYU As Integer = 18        '�i��敪

    'EXCEL�Œ蕶��
    Private Const HEADER_TAISYOU As String = "�Ώۂ̂ݕ\��"
    Private Const LIST_TAISYOU As String = "��"

    '�ύX�t���O
    Private Const HENKO_FLG As String = "1"

    '�v���O����ID�iT91���s�����e�[�u���o�^�p�j
    Private Const PGID As String = "ZG220E"

    '���s�����e�[�u���o�^�p
    Private Const TOUROKU = "2"

    '���Y�����e�[�u���X�V�p
    Private Const TAISYO_ARI = "1"
    Private Const TAISYO_GAI = ""

    '�Ώۃt���O
    Private Const TAISYO As String = "1"
    '-->2010/12/12 chg by takagi #�f�U�C�i�Ř_���l�͐ݒ�ł��Ȃ�
    'Private Const CHECK As String = "True"
    'Private Const NON_CHECK As String = "False"
    Private Const CHECK As String = "1"
    Private Const NON_CHECK As String = "0"
    '<--2010/12/12 chg by takagi #�f�U�C�i�Ř_���l�͐ݒ�ł��Ȃ�

#End Region

#Region "�����o�ϐ���`"
    '------------------------------------------------------------------------------------------------------
    '�����o�[�ϐ��錾
    '------------------------------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler                       'MSG�n���h��
    Private _db As UtilDBIf                                'DB�n���h��
    Private _parentForm As ZC110M_Menu

    Private _oldRowIndex As Integer = -1                   '�I���s�̔w�i�F��ύX���邽�߂̃t���O
    Private _colorCtlFlg As Boolean = False                '�I���s�̔w�i�F��ύX���邽�߂̃t���O

    Private _errSet As UtilDataGridViewHandler.dgvErrSet   '�G���[�������Ƀt�H�[�J�X����Z���ʒu
    Private _nyuuryokuErrFlg As Boolean = False            '���̓G���[�L���t���O

    Private _changeFlg As Boolean = False                  '�ꗗ�f�[�^�ύX�t���O
    Private _beforeChange As String = ""                   '�ꗗ�ύX�O�̃f�[�^

    Private _chkCellVO As UtilDgvChkCellVO                 '�ꗗ�̓��͐����p

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

    '------------------------------------------------------------------------------------------------------
    '�R���X�g���N�^
    '------------------------------------------------------------------------------------------------------
    Private Sub New()
        InitializeComponent()
    End Sub

    '------------------------------------------------------------------------------------------------------
    '   �R���X�g���N�^
    '   �i�����T�v�j�@���j���[��ʂ���Ă΂��B
    '   �����̓p�����^   �FprmRefMsgHd      MSG�n���h��
    '                      prmRefDbHd       DB�n���h��
    '                      prmBumonCd       ����R�[�h
    '                   �@ prmTantoSign     �S���T�C��
    '                      prmDataShubetsu  �f�[�^���(�󒍃f�[�^�E�e���v���[�g)
    '                      prmDataKbn       �f�[�^�敪(�쐬���E���M��)
    '                      prmShohinBunrui  ���i��������(�S�āE�d���E�t���i)
    '                      prmDefaultHyoji  �f�t�H���g�\���iTrue�Efalse�j	
    '   �����\�b�h�߂�l �F�C���X�^���X
    '------------------------------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmForm As ZC110M_Menu, ByVal prmUpdFlg As Boolean)
        Call Me.New()

        '��������
        _msgHd = prmRefMsgHd                                                'MSG�n���h���̐ݒ�
        _db = prmRefDbHd                                                    'DB�n���h���̐ݒ�
        _parentForm = prmForm                                               '�e�t�H�[��
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
            For i As Integer = gh.getMaxRow - 1 To 0 Step -1
                If Not gh.getCellData(COLDT_TAISYOU, i).Equals(gh.getCellData(COLDT_TAISYOUCOPY, i)) Then
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
    '�����{�^���������C�x���g
    '------------------------------------------------------------------------------------------------------
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            '�`�F�b�N�{�b�N�X�̓��e�ύX�m�F
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)
            For i As Integer = gh.getMaxRow - 1 To 0 Step -1
                If Not gh.getCellData(COLDT_TAISYOU, i).Equals(gh.getCellData(COLDT_TAISYOUCOPY, i)) Then
                    _changeFlg = True
                    Exit For
                End If
            Next

            '�x�����b�Z�[�W
            If _changeFlg Then
                Dim rtn As DialogResult = _msgHd.dspMSG("DellDgvData")   '�ҏW���̓��e���j������܂��B��낵���ł����H
                If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If

            Call dispDGV(True, False)     '����������

            '�ύX�t���O�𖳌��ɂ���
            _changeFlg = False

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
                Dim dStartSysdate As Date = Now()                           '�����J�n����
                Dim sPCName As String = UtilClass.getComputerName           '�[��ID
                Dim lCntIns As Long = 0

                '�g�����U�N�V�����J�n
                _db.beginTran()

                '���Y�����e�[�u���X�V
                Call UpdateT21Seisanm(sPCName, dStartSysdate, lCntIns)

                '�����I�������̎擾
                Dim dFinishSysdate As Date = Now()

                '���s�����e�[�u���̍X�V����
                Call updT91Rireki(lCntIns, sPCName, dStartSysdate, dFinishSysdate)

                _parentForm.updateSeigyoTbl(PGID, True, dStartSysdate, dFinishSysdate)

                '�g�����U�N�V�����I��
                _db.commitTran()

                '�`�F�b�N�{�b�N�X���X�V����
                Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)
                For i As Integer = gh.getMaxRow - 1 To 0 Step -1
                    If Not gh.getCellData(COLDT_TAISYOU, i).Equals(gh.getCellData(COLDT_TAISYOUCOPY, i)) Then
                        gh.setCellData(COLDT_TAISYOUCOPY, i, gh.getCellData(COLDT_TAISYOU, i))
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
    'Excel�{�^���������C�x���g
    '------------------------------------------------------------------------------------------------------
    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Try
            ' ����Wait�J�[�\����ێ�
            Dim preCursor As Cursor = Me.Cursor
            ' �J�[�\����ҋ@�J�[�\���ɕύX
            Me.Cursor = Cursors.WaitCursor
            Try

                'EXCEL�o��
                Call printExcel()

            Finally
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
    '�ǉ���z�o�^�{�^���������C�x���g
    '------------------------------------------------------------------------------------------------------
    Private Sub btnTsuika_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTsuika.Click
        Try

            Dim syori As String = Trim(Replace(lblSyori.Text, "/", ""))       '�����N��
            Dim keikaku As String = Trim(Replace(lblKeikaku.Text, "/", ""))   '�v��N��

            Dim openForm As ZG221S_TuikaNyuuryoku = New ZG221S_TuikaNyuuryoku(_msgHd, _db, Me, syori, keikaku)      '��ʑJ��
            openForm.ShowDialog(Me)                                                                                 '��ʕ\��
            openForm.Dispose()

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
                gh.setCellData(COLDT_TAISYOU, i, True)
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
                gh.setCellData(COLDT_TAISYOU, i, False)
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
            '�����N���A�v��N���\��
            Call dispDate()

            '�R���{�{�b�N�X
            Call setCbo(cboKubun, COTEI_SAKUSEI) '�쐬�敪
            '�`�F�b�N�{�b�N�X
            chkTaisyo.Checked = True

            Call dispDGV(False, False)               '��������

            '�ꗗ�s���F�t���O��L���ɂ���
            _colorCtlFlg = True

            btnEntry.Enabled = _updFlg

            '' 2011/01/13 del start sugano #��������e�[�u���͓o�^�{�^���������̂ݍX�V����
            ' '' 2010/12/22 add start sugano
            ''�o�^�{�^���g�p�̏ꍇ�i�m��ρj�̏ꍇ�A��������e�[�u���͍X�V���Ȃ�
            'If btnEntry.Enabled Then
            '    '' 2010/12/22 add  end  sugano
            '    '�o�^�{�^���������Ȃ��P�[�X���l�����A���̎��_�ŏ�������e�[�u�����X�V����
            '    _parentForm.updateSeigyoTbl(PGID, True, Now, Now)
            '    '' 2010/12/22 add start sugano
            'End If
            ' '' 2010/12/22 add  end  sugano
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
    ' �@�X�V�f�[�^�̎󂯎��
    '   (�����T�v)�q��ʂōX�V���ꂽ�f�[�^���󂯎��
    '�@�@I�@�F�@prmUpDDate     �@�@ �X�V����
    '-------------------------------------------------------------------------------
    Sub setUpDDate(ByVal prmUpDDate As Date) Implements IfRturnUpDDate.setUpDDate
        Try
            '�ꗗ�\��
            Call dispDGV(False, True, prmUpDDate)

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub
    '-------------------------------------------------------------------------------
    '   myShow���\�b�h
    '-------------------------------------------------------------------------------
    Public Sub myShow() Implements IfRturnUpDDate.myShow
        Me.Show()
    End Sub

    '-------------------------------------------------------------------------------
    '   myActivate���\�b�h
    '-------------------------------------------------------------------------------
    Public Sub myActivate() Implements IfRturnUpDDate.myActivate
        Me.Activate()
    End Sub

    '-------------------------------------------------------------------------------
    '�@�R���{�{�b�N�X�̃Z�b�g
    '�@(�����N��)M01�ėp�}�X�^����Ώۃ��R�[�h�𒊏o���ĕ\������B
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
                Throw New UsrDefException("�o�^����Ă��܂���B", _msgHd.getMSG("noData"))
            End If

            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(prmsender)

            '�������������p���X�g��ǉ�
            ch.addItem(New UtilCboVO("", ""))

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
    '�@�R���g���[���L�[�����C�x���g
    '�@(�����T�v)�G���^�[�{�^���������Ɏ��̃R���g���[���Ɉڂ�
    '-------------------------------------------------------------------------------
    Private Sub ctl_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboKubun.KeyPress, _
                                                                                                                chkTaisyo.KeyPress, _
                                                                                                                btnSearch.KeyPress, _
                                                                                                                btnZenKaijo.KeyPress, _
                                                                                                                btnZenSentaku.KeyPress, _
                                                                                                                btnTsuika.KeyPress, _
                                                                                                                btnExcel.KeyPress, _
                                                                                                                btnEntry.KeyPress, _
                                                                                                                btnBack.KeyPress
        '�����L�[��Enter�̏ꍇ�A���̃R���g���[���փt�H�[�J�X�ړ�
        Call UtilClass.moveNextFocus(Me, e)
    End Sub

#End Region

#Region "���[�U��`�֐�:EXCEL�֘A"
    '------------------------------------------------------------------------------------------------------
    '�@EXCEL�o�͏���
    '------------------------------------------------------------------------------------------------------
    Private Sub printExcel()
        Try
            '�v���O���X�o�[�\��
            Dim pb As UtilProgressBar = New UtilProgressBar(Me)
            pb.Show()
            Try

                '�v���O���X�o�[�ݒ�
                pb.jobName = "�o�͂��������Ă��܂��B"
                pb.status = "���������D�D�D"

                '���`�t�@�C��
                Dim openFilePath As String = StartUp.iniValue.BaseXlsPath & "\" & StartUp.iniValue.ExcelZG220R1_Base
                '���`�t�@�C�����J����Ă��Ȃ����`�F�b�N
                Dim fh As UtilFile = New UtilFile()
                Try
                    fh.move(openFilePath, openFilePath & 1)
                    fh.move(openFilePath & 1, openFilePath)
                Catch ioe As System.IO.IOException
                    Throw New UsrDefException("�t�@�C�����J����Ă��܂��B�t�@�C������Ă���Ď��s���Ă��������B", _
                                              _msgHd.getMSG("fileOpenErr", "�y�t�@�C�����z�F" & openFilePath))
                End Try

                '�o�͗p�t�@�C��
                '�t�@�C�����擾-----------------------------------------------------
                Dim wkEditFile As String = StartUp.iniValue.OutXlsPath & "\" & StartUp.iniValue.ExcelZG220R1_Out     '�R�s�[��t�@�C��

                '�R�s�[��t�@�C�������݂���ꍇ�A�R�s�[��t�@�C�����폜----------------
                If UtilClass.isFileExists(wkEditFile) Then
                    Try
                        fh.delete(wkEditFile)
                    Catch ioe As System.IO.IOException
                        Throw New UsrDefException("�t�@�C�����J����Ă��܂��B�t�@�C������Ă���Ď��s���Ă��������B", _
                                                  _msgHd.getMSG("fileOpenErr", "�y�t�@�C�����z�F" & wkEditFile))
                    End Try
                End If

                Try
                    '�o�͗p�t�@�C���֐��^�t�@�C���R�s�[
                    FileCopy(openFilePath, wkEditFile)
                Catch ioe As System.IO.IOException
                    Throw New UsrDefException(ioe, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ioe)))
                End Try

                Dim eh As xls.UtilExcelHandler = New xls.UtilExcelHandler(wkEditFile)
                Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder
                Try
                    '�R�s�[��t�@�C���J��
                    eh.open()

                    '�v���O���X�o�[�ݒ�
                    pb.jobName = "�o�͒��D�D�D"
                    pb.status = ""

                    Try
                        Dim startPrintRow As Integer = START_PRINT          '�o�͊J�n�s��
                        Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)        'DGV�n���h���̐ݒ�
                        Dim rowCnt As Integer = gh.getMaxRow

                        '�v���O���X�o�[�@�ő�l�ݒ�
                        pb.maxVal = gh.getMaxRow

                        '���׍s���s�����R�s�[����
                        eh.copyRow(startPrintRow)
                        eh.insertPasteRow(startPrintRow + 1, startPrintRow + gh.getMaxRow)

                        '-->2010.12.12 chg by takagi #�`�F�b�NOFF���ɕK���G���[�ɂȂ�(UtilDataGridViewHandler���g�p���Ă��Ȃ�����/�����Č����������{)
                        'Dim i As Integer
                        'For i = 0 To rowCnt - 1

                        '    '�v���O���X�o�[�J�E���g�A�b�v
                        '    pb.status = (i) & "/" & gh.getMaxRow & "��"
                        '    pb.oneStep = 10
                        '    pb.value = i

                        '    '�ꗗ�f�[�^�o��
                        '    If dgvList(COLCN_TAISYOU, i).Value Then
                        '        sb.Append(LIST_TAISYOU & ControlChars.Tab)
                        '    Else
                        '        sb.Append("" & ControlChars.Tab)
                        '    End If
                        '    sb.Append(dgvList(COLCN_KUBUN, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_UCHIIRE, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_TEHAIKBN, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_KIBOU, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_YOTEI, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_TEHAINO, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_SEIBAN, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_HINMEI, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_TEHAISU, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_TANCYO, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_JOSU, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_SEISAN, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_TUKI, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_HINCD, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_OYACD, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_JUYOSAKI, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_HINSYU, i).Value & ControlChars.Tab)
                        '    sb.Append(ControlChars.CrLf)

                        'Next
                        Dim i As Integer = 0
                        With sb
                            For i = 0 To rowCnt - 1

                                '�v���O���X�o�[�J�E���g�A�b�v
                                pb.status = (i) & "/" & gh.getMaxRow & "��"
                                pb.oneStep = 10
                                pb.value = i

                                '�ꗗ�f�[�^�o��
                                If CHECK.Equals(gh.getCellData(COLDT_TAISYOU, i)) Then
                                    sb.Append(LIST_TAISYOU & ControlChars.Tab)
                                Else
                                    sb.Append("" & ControlChars.Tab)
                                End If
                                .Append(gh.getCellData(COLDT_KUBUN, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_UCHIIRE, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_TEHAIKBN, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_KIBOU, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_YOTEI, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_TEHAINO, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_SEIBAN, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_HINMEI, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_TEHAISU, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_TANCYO, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_JOSU, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_SEISAN, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_TUKI, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_HINCD, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_OYACD, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_JUYOSAKI, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_HINSYU, i) & ControlChars.Tab)
                                .Append(ControlChars.CrLf)

                            Next
                        End With
                        '<--2010.12.12 chg by takagi #�`�F�b�NOFF���ɕK���G���[�ɂȂ�(UtilDataGridViewHandler���g�p���Ă��Ȃ�����/�����Č����������{)

                        Clipboard.SetText(sb.ToString)
                        eh.paste(startPrintRow, XLSCOL_TAISYOU) '�ꊇ�\��t��

                        '�]���ȋ�s���폜
                        eh.deleteRow(startPrintRow + i) '���`�̃R�s�[���ƂȂ�s
                        ''2011/01/20 add start sugano
                        eh.deleteRow(startPrintRow + i) '���^�̈���͈͂Ɋ܂܂���s
                        ''2011/01/20 add end sugano

                        '�쐬�����ҏW
                        Dim printDate As String = Now.ToString("yyyy/MM/dd HH:mm")
                        eh.setValue("�쐬���� �F " & printDate, 1, 18)   'R1

                        '�����N���A�v��N���ҏW
                        eh.setValue("�����N���F" & lblSyori.Text & "�@�@�v��N���F" & lblKeikaku.Text, 1, 8)    'H1

                        '�����ҏW
                        eh.setValue(rowCnt & "��", 3, 18)    'R3

                        '�w�b�_�[�̌��������ҏW
                        eh.setValue("�敪�F" & cboKubun.Text, 3, 1)  'A3
                        If chkTaisyo.Checked Then
                            eh.setValue(HEADER_TAISYOU, 3, 5)  'E3
                        Else
                            eh.setValue("", 3, 5)
                        End If

                        '����̃Z���Ƀt�H�[�J�X���Ă�
                        eh.selectCell(7, 1)     'A7

                        Clipboard.Clear()         '�N���b�v�{�[�h�̏�����

                    Finally
                        'EXCEL�����
                        eh.close()
                    End Try

                    'EXCEL�t�@�C���J��
                    eh.display()

                Catch ue As UsrDefException
                    ue.dspMsg()
                    Throw ue
                Catch ex As Exception
                    '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��V�X�e���G���[MSG�o�͌�X���[
                    Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)), , StartUp.iniValue.LogFilePath)
                Finally
                    eh.endUse()
                    eh = Nothing
                End Try

                '-->2010.12.12 �v���O���X�o�[���m���ɕ����Ȃ��̂ŁATry�`Finally��V�݂��A�Đݒ�
            Finally
                '�v���O���X�o�[��ʏ���
                pb.Close()
            End Try
            '<--2010.12.12 �v���O���X�o�[���m���ɕ����Ȃ��̂ŁATry�`Finally��V�݂��A�Đݒ�

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub
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

    '------------------------------------------------------------------------------------------------------
    '�@���s�����e�[�u���̒ǉ�����
    '  (�����T�v)���s�����e�[�u���Ƀ��R�[�h��ǉ�����
    '�@�@I�@�F�@prmCntIns       �@ �o�^����
    '�@�@I�@�F�@prmPCName      �@�@�[��ID
    '�@�@I�@�F�@prmStartDate       �����J�n����
    '�@�@I�@�F�@prmFinishDate      �����I������
    '------------------------------------------------------------------------------------------------------
    Private Sub updT91Rireki(ByVal prmCntIns As Long, ByVal prmPCName As String, ByVal prmStartDate As Date, ByVal prmFinishDate As Date)
        Try
            '�o�^����
            Dim sql As String = ""
            sql = "INSERT INTO T91RIREKI ("
            sql = sql & N & "  SNENGETU"                                                    '�����N��
            sql = sql & N & ", KNENGETU"                                                    '�v��N��
            sql = sql & N & ", PGID"                                                        '�@�\ID
            sql = sql & N & ", SDATESTART"                                                  '�����J�n����
            sql = sql & N & ", SDATEEND"                                                    '�����I������
            sql = sql & N & ", KENNSU1"                                                     '�����P�i�X�V�����j
            sql = sql & N & ", NAME1"                                                       '���̂P
            sql = sql & N & ", UPDNAME"                                                     '�[��ID
            sql = sql & N & ", UPDDATE"                                                     '�X�V����
            sql = sql & N & ") VALUES ("
            sql = sql & N & "  '" & Trim(Replace(lblSyori.Text, "/", "")) & "'"             '�����N��
            sql = sql & N & ", '" & Trim(Replace(lblKeikaku.Text, "/", "")) & "'"           '�v��N��
            sql = sql & N & ", '" & PGID & "'"                                              '�@�\ID
            sql = sql & N & ", TO_DATE('" & prmStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "   '�����J�n����
            sql = sql & N & ", TO_DATE('" & prmFinishDate & "', 'YYYY/MM/DD HH24:MI:SS') "  '�����I������
            sql = sql & N & ", " & prmCntIns                                                '�����P�i�X�V�����j
            sql = sql & N & ", " & TOUROKU                                                  '���̂P
            sql = sql & N & ", '" & prmPCName & "'"                                         '�[��ID
            sql = sql & N & ", TO_DATE('" & Now() & "', 'YYYY/MM/DD HH24:MI:SS') "          '�X�V����
            sql = sql & N & " )"
            _db.executeDB(sql)

        Catch ex As Exception
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@���Y�����e�[�u���̍X�V����
    '  (�����T�v)���Y�����e�[�u����ύX���e�ɂčX�V����
    '�@�@I�@�F�@prmPCName      �@�@�[��ID
    '�@�@I�@�F�@prmStartDate       �����J�n����
    '�@�@R�@�F�@rCntUp       �@�@�@�X�V����
    '------------------------------------------------------------------------------------------------------
    Private Sub UpdateT21Seisanm(ByVal prmPCName As String, ByVal prmStartDate As Date, ByRef rCntUp As Long)
        Try

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)

            For i As Integer = 0 To gh.getMaxRow - 1
                '�ύX�t���O�������Ă���f�[�^��ΏۂƂ���
                If HENKO_FLG.Equals(gh.getCellData(COLDT_HENKOUFLG, i).ToString) Or Not gh.getCellData(COLDT_TAISYOU, i).Equals(gh.getCellData(COLDT_TAISYOUCOPY, i)) Then
                    '�ύX�f�[�^�̍X�V����
                    Dim sql As String = ""
                    sql = ""
                    sql = sql & N & " UPDATE T21SEISANM SET "
                    If Not "".Equals(gh.getCellData(COLDT_SEISAN, i).ToString) Then
                        sql = sql & N & "   SMIKOMISU = " & gh.getCellData(COLDT_SEISAN, i).ToString
                    Else
                        sql = sql & N & "   SMIKOMISU = '' "
                    End If
                    If Not "".Equals(gh.getCellData(COLDT_TUKI, i).ToString) Then
                        sql = sql & N & ", NENGETSU = '" & Trim(Replace(gh.getCellData(COLDT_TUKI, i).ToString, "/", "")) & "'"
                    Else
                        sql = sql & N & ", NENGETSU = ''"
                    End If
                    If CHECK.Equals(gh.getCellData(COLDT_TAISYOU, i)) Then
                        sql = sql & N & ", TAISYO_FLG = '" & TAISYO_ARI & "'"
                    Else
                        sql = sql & N & ", TAISYO_FLG = '" & TAISYO_GAI & "'"
                    End If
                    sql = sql & N & ", UPDNAME = '" & prmPCName & "'"
                    sql = sql & N & ", UPDDATE = " & " TO_DATE('" & prmStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "
                    sql = sql & N & " WHERE RECORDID = '" & gh.getCellData(COLDT_RECORDID, i).ToString & "'"
                    _db.executeDB(sql)

                    '�X�V�����̃J�E���g�A�b�v
                    rCntUp = rCntUp + 1
                End If
            Next

        Catch ex As Exception
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '��������
    '   �i�����T�v�j�@�����������s�Ȃ��A�ꗗ�Ƀf�[�^��\������B
    '   �����̓p�����^   �FprmActSearchBtnFlg           �����{�^�������� �ďo����t���O
    '                                                   True = �����{�^���������AFalse = �����{�^���������ȊO
    '                    �FprmActCheck_InsertDataFlg    �f�[�^�ǉ����s����t���O
    '                                                   True = ���s����AFalse = ���s���Ȃ�
    '                    �FprmUpDDate�@�@�@�@�@�@�@�@�@ �X�V����
    '   �����\�b�h�߂�l �F�Ȃ�
    '------------------------------------------------------------------------------------------------------
    Private Sub dispDGV(Optional ByVal prmActSearchBtnFlg As Boolean = False, Optional ByVal prmActCheck_InsertDataFlg As Boolean = False, Optional ByVal prmUpDDate As Date = Nothing)
        Dim c As Cursor = Me.Cursor
        Me.Cursor = Cursors.WaitCursor
        Try
            '�ꗗ�E�����N���A
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)
            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(cboKubun)

            '�f�[�^�ǉ������ȊO�̏ꍇ�A�ꗗ�̏�����
            If Not prmActCheck_InsertDataFlg Then
                gh.clearRow()
                dgvList.Enabled = False
                txtCnt.Text = "0��"
            End If

            Dim sql As String = ""
            Dim sqlAdd As String = ""
            sql = "SELECT "
            sql = sql & N & "  CASE T21.TAISYO_FLG WHEN '" & TAISYO & "' THEN '" & CHECK & "' ELSE '" & NON_CHECK & "' END " & COLDT_TAISYOU
            sql = sql & N & " ,CASE T21.TAISYO_FLG WHEN '" & TAISYO & "' THEN '" & CHECK & "' ELSE '" & NON_CHECK & "' END " & COLDT_TAISYOUCOPY
            sql = sql & N & " ,M11.NAME1 " & COLDT_KUBUN
            sql = sql & N & " ,M12.NAME1 " & COLDT_UCHIIRE
            sql = sql & N & " ,M13.NAME1 " & COLDT_TEHAIKBN
            sql = sql & N & " ,TO_CHAR(TO_DATE(T21.KYUTTAIBI,'YYYYMMDD'),'yy/mm/dd') " & COLDT_KIBOU
            sql = sql & N & " ,TO_CHAR(TO_DATE(T21.YSYUTTAIBI,'YYYYMMDD'),'yy/mm/dd') " & COLDT_YOTEI
            sql = sql & N & " ,T21.TEHAINO " & COLDT_TEHAINO
            sql = sql & N & " ,T21.SEIBAN " & COLDT_SEIBAN
            sql = sql & N & " ,T21.HINMEI " & COLDT_HINMEI
            sql = sql & N & " ,T21.TEHAISU " & COLDT_TEHAISU
            sql = sql & N & " ,T21.TANCYO " & COLDT_TANCYO
            sql = sql & N & " ,T21.JYOSU " & COLDT_JOSU
            sql = sql & N & " ,T21.SMIKOMISU " & COLDT_SEISAN
            sql = sql & N & " ,CASE WHEN T21.NENGETSU IS NULL THEN '' ELSE TO_CHAR(TO_DATE(T21.NENGETSU ,'YYYYMM'),'yyyy/mm') END " & COLDT_TUKI
            sql = sql & N & " ,T21.HINMEICD " & COLDT_HINCD
            sql = sql & N & " ,CASE T21.HINMEICD WHEN T21.KHINMEICD THEN '' ELSE T21.KHINMEICD END " & COLDT_OYACD
            sql = sql & N & " ,M14.NAME2 " & COLDT_JUYOSAKI
            sql = sql & N & " ,T21.HINSYU_KBN " & COLDT_HINSYU
            sql = sql & N & " ,T21.RECORDID " & COLDT_RECORDID
            sql = sql & N & " ,M14.SORT " & COLDT_JUYOSORT
            sql = sql & N & " ,M11.SORT " & COLDT_SAKUSEISORT
            sql = sql & N & " ,NULL " & COLDT_HENKOUFLG
            sql = sql & N & " FROM T21SEISANM T21 "
            sql = sql & N & "   LEFT OUTER JOIN M01HANYO M11 ON "
            sql = sql & N & "   T21.SAKUSEI_KBN = M11.KAHENKEY "
            sql = sql & N & "   AND M11.KOTEIKEY = '" & COTEI_SAKUSEI & "'"
            sql = sql & N & "   LEFT OUTER JOIN M01HANYO M12 ON "
            sql = sql & N & "   T21.NYUKO_FLG = M12.KAHENKEY "
            sql = sql & N & "   AND M12.KOTEIKEY = '" & COTEI_NYUKO & "'"
            sql = sql & N & "   LEFT OUTER JOIN M01HANYO M13 ON "
            sql = sql & N & "   T21.TEHAI_KBN = M13.KAHENKEY "
            sql = sql & N & "   AND M13.KOTEIKEY = '" & COTEI_TEHAI & "'"
            sql = sql & N & "   LEFT OUTER JOIN M01HANYO M14 ON "
            sql = sql & N & "   T21.JUYOU_CD = M14.KAHENKEY "
            sql = sql & N & "   AND M14.KOTEIKEY = '" & COTEI_JUYOU & "'"
            If prmActCheck_InsertDataFlg Then
                sql = sql & N & " WHERE T21.UPDNAME = '" & UtilClass.getComputerName & "'"
                sql = sql & N & " AND T21.UPDDATE = TO_DATE('" & prmUpDDate & "', 'YYYY/MM/DD HH24:MI:SS') "
            ElseIf prmActSearchBtnFlg Then
                If Not "".Equals(ch.getCode()) Then
                    If Not "".Equals(sqlAdd) Then
                        sqlAdd = sqlAdd & N & " AND "
                    End If
                    sqlAdd = sqlAdd & "T21.SAKUSEI_KBN = '" & ch.getCode & "'"
                End If
                If chkTaisyo.Checked Then
                    If Not "".Equals(sqlAdd) Then
                        sqlAdd = sqlAdd & N & " AND "
                    End If
                    sqlAdd = sqlAdd & "T21.TAISYO_FLG = '" & TAISYO & "'"
                End If
                If Not "".Equals(sqlAdd) Then
                    sql = sql & N & " WHERE " & sqlAdd
                End If
            Else
                sql = sql & N & " WHERE T21.TAISYO_FLG = '" & TAISYO & "'"
            End If
            '-->2010.12.25 chg by takagi #46
            'sql = sql & N & " ORDER BY M14.SORT ,HINSYU_KBN ,HIN_CD ,SENSIN_CD ,SIZE_CD ,SIYOU_CD ,COLOR_CD ,KYUTTAIBI ,TEHAINO ,M11.SORT"
            sql = sql & N & " ORDER BY HIN_CD ,SENSIN_CD ,SIZE_CD ,SIYOU_CD ,COLOR_CD ,KYUTTAIBI ,TEHAINO ,M11.SORT"
            '<--2010.12.25 chg by takagi #46

            'SQL���s
            Dim iRecCnt As Integer                  '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If prmActCheck_InsertDataFlg Then
                Dim dt As DataTable = CType(dgvList.DataSource, DataSet).Tables(RS)
                Dim newRow As DataRow = dt.NewRow

                '����DataTable�̍ŏI�s��VO��}��
                dt.Rows.InsertAt(newRow, dt.Rows.Count)

                Dim lRowCnt As Long = dt.Rows.Count
                gh.setCellData(COLDT_TAISYOU, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TAISYOU)))        '�Ώ�
                gh.setCellData(COLDT_KUBUN, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_KUBUN)))             '�敪
                gh.setCellData(COLDT_UCHIIRE, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_UCHIIRE)))         '����
                gh.setCellData(COLDT_TEHAIKBN, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TEHAIKBN)))       '��z
                gh.setCellData(COLDT_KIBOU, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_KIBOU)))             '��]
                gh.setCellData(COLDT_YOTEI, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_YOTEI)))             '�\��
                gh.setCellData(COLDT_TEHAINO, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TEHAINO)))         '��z��
                gh.setCellData(COLDT_SEIBAN, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_SEIBAN)))           '����
                gh.setCellData(COLDT_HINMEI, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_HINMEI)))           '�i��
                gh.setCellData(COLDT_TEHAISU, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TEHAISU)))         '��z��
                gh.setCellData(COLDT_TANCYO, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TANCYO)))           '�P��
                gh.setCellData(COLDT_JOSU, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_JOSU)))               '��
                gh.setCellData(COLDT_SEISAN, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_SEISAN)))           '���Y����
                gh.setCellData(COLDT_TUKI, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TUKI)))               '�N��
                gh.setCellData(COLDT_HINCD, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_HINCD)))             '�i���R�[�h
                gh.setCellData(COLDT_OYACD, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_OYACD)))             '�e�i���R�[�h
                gh.setCellData(COLDT_JUYOSAKI, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_JUYOSAKI)))       '���v��
                gh.setCellData(COLDT_HINSYU, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_HINSYU)))           '�i��敪
                gh.setCellData(COLDT_RECORDID, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_RECORDID)))       '���R�[�hID    (�B����)
                gh.setCellData(COLDT_JUYOSORT, lRowCnt - 1, _db.rmNullInt(ds.Tables(RS).Rows(0)(COLDT_JUYOSORT)))       '���v��\����  (�B����)
                gh.setCellData(COLDT_SAKUSEISORT, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_SAKUSEISORT))) '�쐬�敪�\����(�B����)

                '�ǉ������s�̕ύX�t���O�𗧂Ă�B
                '���ύX�t���O�������Ă���f�[�^���o�^�ΏۂɂȂ邽��
                gh.setCellData(COLDT_HENKOUFLG, lRowCnt - 1, HENKO_FLG)

                '�����̕\��
                txtCnt.Text = CStr(lRowCnt) & "��"
            Else
                '���o�f�[�^���ꗗ�ɕ\������
                dgvList.DataSource = ds
                dgvList.DataMember = RS

                '�ꗗ�����\��
                txtCnt.Text = dgvList.RowCount.ToString & "��"

                '�{�^������------------------------------------------------------------
                If dgvList.RowCount <= 0 Then
                    dgvList.Enabled = False         '�ꗗ�̎g�p�s��
                    btnEntry.Enabled = False        '�o�^�{�^���̎g�p�s��
                    btnExcel.Enabled = False        '�G�N�Z���{�^���̎g�p�s��
                    btnZenSentaku.Enabled = False   '�S�I���{�^���̎g�p�s��
                    btnZenKaijo.Enabled = False     '�S�����{�^���̎g�p�s��
                    '���b�Z�[�W�̕\��
                    Throw New UsrDefException("�Ώۃf�[�^������܂���B", _msgHd.getMSG("NonData"))
                Else
                    dgvList.Enabled = True          '�ꗗ�̎g�p�s��
                    btnEntry.Enabled = _updFlg      '�o�^�{�^���̎g�p��
                    btnExcel.Enabled = True         '�G�N�Z���{�^���̎g�p��
                    btnZenSentaku.Enabled = True    '�S�I���{�^���̎g�p��
                    btnZenKaijo.Enabled = True      '�S�����{�^���̎g�p��
                    '�ꗗ�擪�s�I��--------------------------------------------------------------
                    dgvList.Focus()
                    gh.setSelectionRowColor(0, 0, StartUp.lCOLOR_BLUE)
                End If
            End If


        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��V�X�e���G���[MSG�o�͌�X���[
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        Finally
            Me.Cursor = c
        End Try
    End Sub
#End Region

#Region "���[�U��`�֐�:�`�F�b�N����"
    '------------------------------------------------------------------------------------------------------
    '  �o�^�`�F�b�N
    '�@(�����T�v)�e���ڂ̕K�{���ڃ`�F�b�N���s��
    '------------------------------------------------------------------------------------------------------
    Private Sub checkTouroku()
        Try

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)

            For i As Integer = 0 To gh.getMaxRow - 1
                If CHECK.Equals(gh.getCellData(COLDT_TAISYOU, i)) Then
                    '�Ώۃ`�F�b�N���`�F�b�N����̏ꍇ�A�K�{���̓`�F�b�N
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
