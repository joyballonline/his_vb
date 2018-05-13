'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���j���׎R�ϏW�v���ʊm�F(�H����)
'    �i�t�H�[��ID�jZG730Q_FukaYamadumiKoutei
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ���        2010/11/19                 �V�K              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.Combo
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory

Public Class ZG730Q_FukaYamadumiKoutei
    Inherits System.Windows.Forms.Form
    Implements IfRturnSeisanNouryoku

#Region "���e�����l��`"

    'PG���䕶��
    Private Const RS As String = "RecSet"                       '�f�[�^�Z�b�g�e�[�u����
    Private Const N As String = ControlChars.NewLine            '���s����
    Private Const T As String = ControlChars.Tab                '��ؕ���

    '�ꗗ�f�[�^�o�C���h��
    Private Const COLDT_KOUTEI As String = "dtKoutei"                   '�H��
    Private Const COLDT_MASHINENAME As String = "dtMashineName"         '�@�B���L��
    Private Const COLDT_SEISANNOURYOKU As String = "dtSeisanNouryoku"   'MCH���Y�\��
    Private Const COLDT_MCHGOUKEI As String = "dtMCHGoukei"             'MCH�R�ϕ�
    Private Const COLDT_MCHHAKKOU As String = "dtMCHHakkou"             'MCH����`�[���s��
    Private Const COLDT_MCHMIHAKKOU As String = "dtMCHMihakkou"         'MCH����`�[�����s��
    Private Const COLDT_MCHGETUJIZAIKO As String = "dtMCHGetujiZaiko"   'MCH�����݌ɕ�
    Private Const COLDT_OVERMCH As String = "dtOverMCH"                 'MCH�I�[�o�[��
    Private Const COLDT_MHGOUKEI As String = "dtMHGoukei"               'MH�R�ϕ�
    Private Const COLDT_MHHAKKOU As String = "dtMHHakkou"               'MH����`�[���s��
    Private Const COLDT_MHMIHAKKOU As String = "dtMHMihakkou"           'MH����`�[�����s��
    Private Const COLDT_MHGETUJIZAIKO As String = "dtMHGetujiZaiko"     'MH�����݌ɕ�

    '�ꗗ�O���b�h��
    Private Const COLCN_MEISAICHK As String = "cnMeisaiChk"             '���׃{�^��
    Private Const COLCN_KOUTEI As String = "cnKoutei"                   '�H��
    Private Const COLCN_MASHINENAME As String = "cnMashineName"         '�@�B���L��
    Private Const COLCN_SEISANNOURYOKU As String = "cnSeisanNouryoku"   'MCH���Y�\��
    Private Const COLCN_MCHGOUKEI As String = "cnMCHGoukei"             'MCH�R�ϕ�
    Private Const COLCN_MCHHAKKOU As String = "cnMCHHakkou"             'MCH����`�[���s��
    Private Const COLCN_MCHMIHAKKOU As String = "cnMCHMihakkou"         'MCH����`�[�����s��
    Private Const COLCN_MCHGETUJIZAIKO As String = "cnMCHGetujiZaiko"   'MCH�����݌ɕ�
    Private Const COLCN_OVERMCH As String = "cnOverMCH"                 'MCH�I�[�o�[��
    Private Const COLCN_MHGOUKEI As String = "cnMHGoukei"               'MH�R�ϕ�
    Private Const COLCN_MHHAKKOU As String = "cnMHHakkou"               'MH����`�[���s��
    Private Const COLCN_MIHAKKOU As String = "cnMihakkou"               'MH����`�[�����s��
    Private Const COLCN_MHGETUJIZAIKO As String = "cnMHGetujiZaiko"     'MH�����݌ɕ�

    Private Const KOUTEI As String = "12"                               '�H���̌Œ�L�[
    Private Const MAXSORT As Integer = 99999999                         '�\�[�g�ԍ��̍ő�l

    Private PGID As String = "ZG730Q"                                   'T02�ɓo�^���邽�߂̋@�\�h�c

#End Region

#Region "�����o�ϐ���`"

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu
    Private _updFlg As Boolean = False

    Private _selectedCbKoutei As String         '�������̍H�����R�[�h
    Private _selectedCbKikai As String          '�������̋@�B���L��
    Private _selectedCbCdKouitei As String      '�H�����R���{�{�b�N�X�̃R�[�h
    Private _selectedCbCdKikai As String        '�@�B���L���R���{�{�b�N�X�̃R�[�h

    Private _kensuu As Integer                  '�ꗗ�ɕ\������Ă��錏��

    Private _oldRowIndex As Integer = -1        '�I���s�̔w�i�F��ύX���邽�߂̃t���O��錾
    Private _colorCtlFlg As Boolean = False     '�I���s�̔w�i�F��ύX���邽�߂̃t���O��錾

    Private _startDate As Date                  'T02�ɓo�^���鏈���J�n����
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
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmForm As ZC110M_Menu, ByVal prmUpdFlg As Boolean)

        Call Me.New()

        '��������
        _msgHd = prmRefMsgHd                                    'MSG�n���h���̐ݒ�
        _db = prmRefDbHd                                        'DB�n���h���̐ݒ�
        _parentForm = prmForm                                   '�e�t�H�[��
        _kensuu = 0
        lblKensuu.Text = "0��"
        StartPosition = FormStartPosition.CenterScreen          '��ʒ����\��
        _updFlg = prmUpdFlg
        _startDate = Now                                        '�����J�n�����ێ�

    End Sub

#End Region

#Region "Form�C�x���g"


    '-------------------------------------------------------------------------------
    '�@�����J�n�C�x���g
    '-------------------------------------------------------------------------------
    Private Sub ZG730Q_FukaYamadumiKoutei_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            '�`��֌W�̐ݒ�
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           '�T�C�Y���ύX���ꂽ�Ƃ��ɁA�R���g���[�����R���g���[�����̂��ĕ`�悷�邩�ǂ����������l��ݒ�
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '�`��̓o�b�t�@�Ŏ��s����A������ɁA���ʂ���ʂɏo�͂����悤�ݒ�
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  '�R���g���[���́A��ʂɒ��ڂł͂Ȃ��A�܂��o�b�t�@�ɕ`�悳��܂��B����ɂ��A�������}���邱�Ƃ��ł��܂��B
            Me.SetStyle(ControlStyles.UserPaint, True)              '�R���g���[���́A�I�y���[�e�B���O �V�X�e���ɂ���Ăł͂Ȃ��A�Ǝ��ɕ`�悳���悤�ݒ�
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   '�R���g���[���̓E�B���h�E ���b�Z�[�W WM_ERASEBKGND �𖳎�����悤�ɐݒ�

            Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr '�^�C�g���I�v�V�����\��

            '������
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
    '�@�����{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnKensaku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensaku.Click

        Try
            '�񒅐F�t���O����
            _colorCtlFlg = False

            Call dispDGV()     '����������

            '�񒅐F�t���O�L��
            _colorCtlFlg = True

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�߂�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnModoru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModoru.Click

        If "�ݒ肠��".Equals(lblSeisanSettei.Text) Then
            'T02��������e�[�u���X�V
            _parentForm.updateSeigyoTbl(PGID, True, _startDate, Now)
        End If

        '����ʂ��I�����A���j���[��ʂɖ߂�B
        _parentForm.Show()
        _parentForm.Activate()

        Me.Close()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@���Y�\�͐ݒ�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnSeisan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeisan.Click

        Dim openForm As ZG710E_SeisanNouryoku = New ZG710E_SeisanNouryoku(_msgHd, _db, Me, _kensuu)      '��ʑJ��
        openForm.Show()    '��ʕ\��
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@EXCEL�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click

        Try
            ' ����Wait�J�[�\����ێ�
            Dim cur As Cursor = Me.Cursor
            ' �J�[�\����ҋ@�J�[�\���ɕύX
            Me.Cursor = Cursors.WaitCursor

            Try
                'EXCEL�o��
                Call printExcel()

            Finally
                ' �J�[�\�������ɖ߂�
                Me.Cursor = cur
            End Try

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

#End Region

#Region "�f�[�^�O���b�h����"
    '-->2010.12.27 add by takagi 
    Private Sub dgvFukaKakunin_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvFukaKakunin.DoubleClick
        If dgvFukaKakunin.CurrentCellAddress.Y < 0 Then Exit Sub
        Dim ee As System.Windows.Forms.DataGridViewCellEventArgs = New System.Windows.Forms.DataGridViewCellEventArgs(0, dgvFukaKakunin.CurrentCellAddress.Y)
        Call dgvFukaKakunin_CellContentClick(sender, ee) '�{�^�������]��
    End Sub
    '<--2010.12.27 add by takagi 

    '------------------------------------------------------------------------------------------------------
    '�@�f�[�^�O���b�h����
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvFukaKakunin_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvFukaKakunin.CellContentClick

        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvFukaKakunin)
            Dim clickMeisaiBtn As Integer        '���׃{�^�����������ꂽ�s��
            Dim nowRow As Integer = e.RowIndex   '���݂̍s��

            '�����s�𐅐F�ɒ��F
            dgvFukaKakunin.CurrentCell = dgvFukaKakunin(COLCN_MEISAICHK, nowRow)
            gh.setSelectionRowColor(nowRow, _oldRowIndex, StartUp.lCOLOR_BLUE)

            '�{�^����̐F���V���o�[�ɖ߂�
            dgvFukaKakunin(COLCN_MEISAICHK, nowRow).Style.BackColor = SystemColors.Control

            _oldRowIndex = nowRow

            '�f�[�^�O���b�h�̃{�^�����������ꂽ�ꍇ�A���׉�ʂɑJ��
            If gh.getClickBtn(e, clickMeisaiBtn) = True Then

                Dim openForm As ZG731Q_FukaYamadumiMeisai = New ZG731Q_FukaYamadumiMeisai(_msgHd, _db, Me, _
                                                                        _db.rmNullStr(gh.getCellData(COLDT_MASHINENAME, clickMeisaiBtn)), _
                                                                        _db.rmNullStr(gh.getCellData(COLDT_SEISANNOURYOKU, clickMeisaiBtn)), _
                                                                        _db.rmNullStr(gh.getCellData(COLDT_MCHGOUKEI, clickMeisaiBtn)), _
                                                                        _db.rmNullStr(gh.getCellData(COLDT_OVERMCH, clickMeisaiBtn)), _
                                                                        _db.rmNullStr(gh.getCellData(COLDT_MHGOUKEI, clickMeisaiBtn)), _
                                                                        lblSyori.Text, lblKeikaku.Text)      '��ʑJ��
                openForm.Show() '��ʕ\��
                Me.Hide()

            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�@�I���s�ɒ��F���鏈��
    '�@�@(�����T�v�j�I���s�ɒ��F����B
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvFukaKakunin_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvFukaKakunin.CellEnter

        Try
            If _colorCtlFlg Then
                Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvFukaKakunin)
                '�w�i�F�̐ݒ�
                Call setBackcolor(dgvFukaKakunin.CurrentCellAddress.Y, _oldRowIndex)
            End If
            _oldRowIndex = dgvFukaKakunin.CurrentCellAddress.Y
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub


    '------------------------------------------------------------------------------------------------------
    '�@�w�i�F�̐ݒ菈��
    '�@(�����T�v)�s�̔w�i���ɂ��A�{�^���̗�����ɖ߂��B
    '�@�@I�@�F�@prmRowIndex     ���݃t�H�[�J�X������s��
    '�@�@I�@�F�@prmOldRowIndex  ���݂̍s�Ɉڂ�O�̍s��
    '------------------------------------------------------------------------------------------------------
    Private Sub setBackcolor(ByVal prmRowIndex As Integer, ByVal prmOldRowIndex As Integer)

        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvFukaKakunin)

            '�w�肵���s�̔w�i�F��ɂ���
            gh.setSelectionRowColor(prmRowIndex, prmOldRowIndex, StartUp.lCOLOR_BLUE)

            '�{�^����̐F���ς���Ă��܂��̂ŁA�߂�����
            dgvFukaKakunin(COLCN_MEISAICHK, prmRowIndex).Style.BackColor = SystemColors.Control

            _oldRowIndex = prmRowIndex

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

#End Region

#Region "���[�U��`�֐�"

    '-------------------------------------------------------------------------------
    '�@��ʋN����
    '-------------------------------------------------------------------------------
    Private Sub initForm()
        Try
            '�����N���A�v��N���\��
            Call dispDate()

            '�R���{�{�b�N�X�쐬
            Call setCbo()

            '���Y�\�͐ݒ胉�x���쐬
            Call dispSeisan()

            '�G�N�Z���{�^���̎g�p�s��
            btnExcel.Enabled = False

            '���Y�\�͐ݒ�{�^���g�p��
            btnSeisan.Enabled = _updFlg

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��V�X�e���G���[MSG�o�͌�X���[
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

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

            Dim syoriDate As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("SYORI"))     '�����N��
            Dim keikakuDate As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("KEIKAKU")) '�v��N��

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
    '�@�H�����R���{�{�b�N�X�E�@�B���L���̃Z�b�g
    '�@(�����T�v)M01�ėp�}�X�^����H�����E�@�B���L���𒊏o���ĕ\������B
    '-------------------------------------------------------------------------------
    Private Sub setCbo()

        Try
            '�H���R���{�{�b�N�X�쐬�pSQL
            Dim sql = ""
            sql = sql & N & " SELECT DISTINCT "
            sql = sql & N & " M21.KOUTEI KOUTEI, M01.KAHENKEY KAHEN, M01.SORT "
            sql = sql & N & " FROM M21SEISAN M21 "
            sql = sql & N & " LEFT OUTER JOIN (SELECT KAHENKEY,SORT FROM M01HANYO WHERE KOTEIKEY = '" & KOUTEI & "') M01 "
            sql = sql & N & " ON M01.KAHENKEY = M21.KOUTEI  "
            sql = sql & N & " ORDER BY NVL(M01.SORT, " & MAXSORT & ")"

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            Dim ckoutei As UtilComboBoxHandler = New UtilComboBoxHandler(cboKoutei) '�R���{�{�b�N�X�n���h��

            '�������������p���X�g��ǉ�
            ckoutei.addItem(New UtilCboVO("", ""))

            '���[�v�����ăR���{�{�b�N�X�ɃZ�b�g
            For i As Integer = 0 To iRecCnt - 1
                ckoutei.addItem(New UtilCboVO(_db.rmNullStr(ds.Tables(RS).Rows(i)("KAHEN")), _db.rmNullStr(ds.Tables(RS).Rows(i)("KOUTEI"))))
            Next

            '�@�B���L���R���{�{�b�N�X�쐬�pSQL
            sql = ""
            sql = sql & N & " SELECT "
            sql = sql & N & " M21.KIKAIMEI KIKAIMEI, M01.SORT "
            sql = sql & N & " FROM M21SEISAN M21 "
            sql = sql & N & " LEFT OUTER JOIN (SELECT KAHENKEY,SORT FROM M01HANYO WHERE KOTEIKEY = '" & KOUTEI & "') M01 "
            sql = sql & N & " ON M01.KAHENKEY = M21.KOUTEI "
            sql = sql & N & " ORDER BY NVL(M01.SORT, " & MAXSORT & "), M21.KIKAIMEI "

            'SQL���s
            Dim ds2 As DataSet = _db.selectDB(sql, RS, iRecCnt)

            Dim ckikai As UtilComboBoxHandler = New UtilComboBoxHandler(cboKikai) '�R���{�{�b�N�X�n���h��

            '���[�v�����ăR���{�{�b�N�X�ɃZ�b�g
            ckikai.addItem(New UtilCboVO("", ""))
            For i As Integer = 0 To iRecCnt - 1
                ckikai.addItem(New UtilCboVO(_db.rmNullStr(ds2.Tables(RS).Rows(i)("KIKAIMEI")), _db.rmNullStr(ds2.Tables(RS).Rows(i)("KIKAIMEI"))))
            Next

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@���Y�\�͐ݒ胉�x���\��
    '�@(�����T�v)���Y�\�͐ݒ胉�x����\������
    '-------------------------------------------------------------------------------
    Private Sub dispSeisan()

        Try
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & " TO_CHAR(MAX(UPDDATE), 'YYYY/MM/DD HH24:MI:SS') " & "UPDDATE "          '�X�V�N��
            sql = sql & N & " FROM T64MCH "

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)
            Dim seisanDate As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("UPDDATE"))  '���Y�\�͐ݒ����

            If String.Empty.Equals(seisanDate) Then      '���o���R�[�h����̏ꍇ
                lblSeisanSettei.Text = "�ݒ�Ȃ�"
                lblSeisanDate.Text = "- - -"
                Exit Sub
            End If

            lblSeisanSettei.Text = "�ݒ肠��"
            lblSeisanDate.Text = seisanDate.Substring(0, 16)        'yyyy/mm/dd hh:mm�`���ŕ\��

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '��������
    '   �i�����T�v�j�@�����������s�Ȃ��A�ꗗ�Ƀf�[�^��\������B
    '------------------------------------------------------------------------------------------------------
    Private Sub dispDGV()

        Dim c As Cursor = Me.Cursor
        Me.Cursor = Cursors.WaitCursor

        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvFukaKakunin)
            Dim ckoutei As UtilComboBoxHandler = New UtilComboBoxHandler(cboKoutei)
            Dim ckikai As UtilComboBoxHandler = New UtilComboBoxHandler(cboKikai)

            '�ꗗ�̏�����
            gh.clearRow()
            dgvFukaKakunin.Enabled = False
            lblKensuu.Text = "0��"

            '���������̎擾
            _selectedCbKoutei = _db.rmNullStr(ckoutei.getName)
            _selectedCbKikai = _db.rmNullStr(ckikai.getName)
            _selectedCbCdKouitei = _db.rmNullStr(ckoutei.getCode)
            _selectedCbCdKikai = _db.rmNullStr(ckikai.getCode)

            'SQL
            'T61�����C���Ƃ��āAT61�AT64����f�[�^�擾
            '�\�[�g���ɂ��Ă�M01���Q��(�H���� = NULL���l��)
            Dim sql As String = ""
            Dim sqlAdd As String = ""
            sql = "SELECT "
            sql = sql & N & " T61.KOUTEI " & COLDT_KOUTEI
            sql = sql & N & " ,T61.KIKAIMEI " & COLDT_MASHINENAME
            sql = sql & N & " ,NVL(T64.MSTMCH,'') " & COLDT_SEISANNOURYOKU
            sql = sql & N & " ,T61.YAMADUMIMCH " & COLDT_MCHGOUKEI
            sql = sql & N & " ,T61.DHAKKOUMCH " & COLDT_MCHHAKKOU
            sql = sql & N & " ,T61.DMIHAKKOUMCH " & COLDT_MCHMIHAKKOU
            sql = sql & N & " ,T61.GZAIKOMCH " & COLDT_MCHGETUJIZAIKO
            sql = sql & N & " ,NVL(T64.MSTMCH,0) - T61.YAMADUMIMCH " & COLDT_OVERMCH
            sql = sql & N & " ,T61.YAMADUMIMH " & COLDT_MHGOUKEI
            sql = sql & N & " ,T61.DHAKKOUMH " & COLDT_MHHAKKOU
            sql = sql & N & " ,T61.DMIHAKKOUMH " & COLDT_MHMIHAKKOU
            sql = sql & N & " ,T61.GZAIKOMH " & COLDT_MHGETUJIZAIKO
            sql = sql & N & " FROM T61FUKA T61 "
            sql = sql & N & " LEFT OUTER JOIN T64MCH T64 ON T64.NAME = T61.KIKAIMEI "
            sql = sql & N & " LEFT OUTER JOIN (SELECT KAHENKEY,SORT FROM M01HANYO WHERE KOTEIKEY = '" & KOUTEI & "') M01 ON M01.KAHENKEY = T61.KOUTEI "
            '�R���{�{�b�N�X�̓��e���ݒ肳��Ă���ꍇ�AWHERE�������
            If (String.Empty.Equals(_selectedCbKoutei) And String.Empty.Equals(_selectedCbKikai)) = False Then
                sql = sql & N & " WHERE "
                If String.Empty.Equals(_selectedCbKoutei) = False Then   '�H�����R���{�ɑI��l�����݂���ꍇ�A���������ɕt��
                    sql = sql & N & " T61.KOUTEI = '" & _selectedCbCdKouitei & "'"
                End If
                If String.Empty.Equals(_selectedCbKikai) = False Then   '�@�B���R���{�ɑI��l�����݂���ꍇ�A���������ɕt��
                    '�H�����R���{���ݒ肳��Ă����ꍇ�AAND�������
                    If String.Empty.Equals(_selectedCbKoutei) = False Then
                        sql = sql & N & " AND "
                    End If
                    sql = sql & N & " T61.KIKAIMEI = '" & _selectedCbCdKikai & "'"
                End If
            End If
            sql = sql & N & " ORDER BY NVL(M01.SORT," & MAXSORT & "), T61.KIKAIMEI "

            'SQL���s
            Dim iRecCnt As Integer                  '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then            '���o���R�[�h���P�����Ȃ��ꍇ
                Throw New UsrDefException("�Ώۃf�[�^������܂���B", _msgHd.getMSG("NonData"))
            End If

            '���o�f�[�^���ꗗ�ɕ\������
            dgvFukaKakunin.DataSource = ds
            dgvFukaKakunin.DataMember = RS

            '������\��
            lblKensuu.Text = CStr(iRecCnt) & "��"
            _kensuu = iRecCnt

            '�{�^������
            If dgvFukaKakunin.RowCount <= 0 Then
                dgvFukaKakunin.Enabled = False  '�ꗗ�̎g�p�s��
                btnExcel.Enabled = False        '�G�N�Z���{�^���̎g�p�s��
            Else
                dgvFukaKakunin.Enabled = True   '�ꗗ�̎g�p�s��
                btnExcel.Enabled = True         '�G�N�Z���{�^���̎g�p��
                '�ꗗ�擪�s�I��
                dgvFukaKakunin.Focus()
                gh.setSelectionRowColor(0, 0, StartUp.lCOLOR_BLUE)
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

    '------------------------------------------------------------------------------------------------------
    '�@EXCEL�o�͏���
    '   �i�����T�v�j�@�ŏI�������ʂ���@�B�ʕ��׎R�ϏW�v�\���o�͂���B
    '------------------------------------------------------------------------------------------------------
    Private Sub printExcel()

        Try
            '���`�t�@�C��
            Dim openFilePath As String = StartUp.iniValue.BaseXlsPath & "\" & StartUp.iniValue.ExcelZG730R1_Base
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
            '�t�@�C�����擾
            Dim wkEditFile As String = StartUp.iniValue.OutXlsPath & "\" & StartUp.iniValue.ExcelZG730R1_Out     '�R�s�[��t�@�C��

            '�R�s�[��t�@�C�������݂���ꍇ�A�R�s�[��t�@�C�����폜
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
            Try
                '�R�s�[��t�@�C���J��
                eh.open()
                Try
                    Dim startPrintRow As Integer = 9        '�o�͊J�n�s��
                    Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvFukaKakunin)        'DGV�n���h���̐ݒ�
                    Dim rowCnt As Integer = gh.getMaxRow    '�f�[�^�O���b�h�̍ő�s
                    Dim xlsi As Integer = 0                 '�G�N�Z���t�@�C���̏������ݍs
                    Dim sLine As Integer = startPrintRow    '�W�v�J�n�s
                    Dim eLine As Integer = startPrintRow    '�W�v�I���s
                    Dim syuukeiFlg As Boolean = False   '�W�v������s���t���O
                    Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder

                    For i As Integer = 0 To rowCnt - 1

                        If i = rowCnt - 1 Then
                            '�ŏI�s�̏ꍇ�W�v�t���OON
                            syuukeiFlg = True
                        ElseIf _db.rmNullStr(dgvFukaKakunin(COLCN_KOUTEI, i).Value).Equals(_db.rmNullStr(dgvFukaKakunin(COLCN_KOUTEI, i + 1).Value)) = False Then
                            '���H���Ǝ��H�����قȂ�ꍇ���W�v�t���OON
                            syuukeiFlg = True
                        End If

                        '�ꗗ�f�[�^�o��
                        sb.Append(_db.rmNullStr(dgvFukaKakunin(COLCN_KOUTEI, i).Value) & T)          '�H�����R�[�h
                        sb.Append(_db.rmNullStr(dgvFukaKakunin(COLCN_MASHINENAME, i).Value) & T)     '�@�B���L��
                        sb.Append(_db.rmNullStr(dgvFukaKakunin(COLCN_SEISANNOURYOKU, i).Value) & T)  '�iMCH�j���Y�\��
                        sb.Append(_db.rmNullStr(dgvFukaKakunin(COLCN_MCHGOUKEI, i).Value) & T)       '�iMCH�j�R�ϕ�
                        sb.Append(_db.rmNullStr(dgvFukaKakunin(COLCN_MCHHAKKOU, i).Value) & T)       '�iMCH�j����`�[���s��
                        sb.Append(_db.rmNullStr(dgvFukaKakunin(COLCN_MCHMIHAKKOU, i).Value) & T)     '�iMCH�j����`�[�����s��
                        sb.Append(_db.rmNullStr(dgvFukaKakunin(COLCN_MCHGETUJIZAIKO, i).Value) & T)  '�iMCH�j�����݌ɕ�
                        sb.Append(_db.rmNullStr(dgvFukaKakunin(COLCN_OVERMCH, i).Value) & T)         '�iMCH�j�I�[�o�[��
                        sb.Append(_db.rmNullStr(dgvFukaKakunin(COLCN_MHGOUKEI, i).Value) & T)        '�iMH�j�R�ϕ�
                        sb.Append(_db.rmNullStr(dgvFukaKakunin(COLCN_MHHAKKOU, i).Value) & T)        '�iMH�j����`�[���s��
                        sb.Append(_db.rmNullStr(dgvFukaKakunin(COLCN_MIHAKKOU, i).Value) & T)        '�iMH�j����`�[�����s��
                        sb.Append(_db.rmNullStr(dgvFukaKakunin(COLCN_MHGETUJIZAIKO, i).Value) & N)   '�iMH�j�����݌ɕ�

                        If syuukeiFlg = True Then
                            '�W�v�I���s�̐ݒ�
                            eLine = startPrintRow + xlsi

                            '�s��1�s�ǉ�
                            xlsi += 1

                            '�W�v�s�̑}��
                            sb.Append(_db.rmNullStr(dgvFukaKakunin(COLCN_KOUTEI, i).Value) & T)  '�H�����R�[�h
                            sb.Append("���v" & T)                                                '�@�B���L��
                            sb.Append("=if(C" & eLine & "="""","""",subtotal(9,C" & sLine & ":C" & eLine & "))" & T) '�iMCH�j���Y�\��
                            sb.Append("=subtotal(9,D" & sLine & ":D" & eLine & ")" & T)                 '�iMCH�j�R�ϕ�
                            sb.Append("=subtotal(9,E" & sLine & ":E" & eLine & ")" & T)                 '�iMCH�j����`�[���s��
                            sb.Append("=subtotal(9,F" & sLine & ":F" & eLine & ")" & T)                 '�iMCH�j����`�[�����s��
                            sb.Append("=subtotal(9,G" & sLine & ":G" & eLine & ")" & T)                 '�iMCH�j�����݌ɕ�
                            sb.Append("=subtotal(9,H" & sLine & ":H" & eLine & ")" & T)                 '�iMCH�j�I�[�o�[��
                            sb.Append("=subtotal(9,I" & sLine & ":I" & eLine & ")" & T)                 '�iMH�j�R�ϕ�
                            sb.Append("=subtotal(9,J" & sLine & ":J" & eLine & ")" & T)                 '�iMH�j����`�[���s��
                            sb.Append("=subtotal(9,K" & sLine & ":K" & eLine & ")" & T)                 '�iMH�j����`�[�����s��
                            sb.Append("=subtotal(9,L" & sLine & ":L" & eLine & ")" & N)                 '�iMH�j�����݌ɕ�

                            '�s��1�s�ǉ�
                            xlsi += 1
                            sb.Append(N)
                            syuukeiFlg = False

                            '�W�v�J�n�s�̐ݒ�
                            sLine = startPrintRow + xlsi + 1
                        End If

                        xlsi += 1

                    Next

                    '�s��ǉ�
                    eh.copyRow(startPrintRow)
                    eh.insertPasteRow(startPrintRow, startPrintRow + xlsi)

                    '���`�Ɉꊇ�\��t��
                    Clipboard.SetText(sb.ToString)
                    eh.paste(startPrintRow, 1)

                    '�]���ȋ�s���폜
                    eh.deleteRow(startPrintRow + xlsi - 1, startPrintRow + xlsi + 1)

                    '�쐬�����ҏW
                    Dim printDate As String = Now.ToString("yyyy/MM/dd HH:mm")
                    eh.setValue("�쐬���� �F " & printDate, 1, 12)   'L1:

                    '�����N���A�v��N���ҏW
                    eh.setValue("�����N���F" & lblSyori.Text & "�@�@�v��N���F" & lblKeikaku.Text, 1, 6)    'F1

                    '�w�b�_�[�̌��������ҏW
                    eh.setValue("�H�����R�[�h�F" & _selectedCbKoutei, 3, 1) 'A3
                    eh.setValue("�@�B���L���F" & _selectedCbKikai, 3, 4)    'D3

                    '����̃Z���Ƀt�H�[�J�X���Ă�
                    eh.selectCell(9, 1)     'A7

                    '�N���b�v�{�[�h�̏�����
                    Clipboard.Clear()

                Finally
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
    Private Sub txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboKoutei.KeyPress, _
                                                                                                                cboKikai.KeyPress
        Try
            '���̃R���g���[���ֈړ�����
            UtilClass.moveNextFocus(Me, e)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

#End Region

#Region "�q��ʂ�����s�����֐�"

    '-------------------------------------------------------------------------------
    ' �@�X�V�f�[�^�̎󂯎��
    '   (�����T�v)�q��ʂł̓o�^�������󂯂Č������s��
    '�@�@I�@�F�@prmRegist     �@�@ �o�^�����ꍇTrue
    '-------------------------------------------------------------------------------
    Sub setRegist(ByVal prmRegist As Boolean) Implements IfRturnSeisanNouryoku.setRegist

        Try
            If prmRegist = True Then
                Call dispSeisan() '�q��ʂœo�^�������s�����ꍇ���Y�\�͐ݒ胉�x�����X�V

                '�e��ʂ̈ꗗ�\��������0�����傫���ꍇ�͍Č������s��
                If _kensuu > 0 Then
                    _colorCtlFlg = False
                    Call dispDGV()     '����������
                    _colorCtlFlg = True
                    '�X�N���[���o�[�̕s�����Ή�
                    dgvFukaKakunin.VirtualMode = True
                    dgvFukaKakunin.VirtualMode = False
                End If
            End If

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
    Public Sub myShow() Implements IfRturnSeisanNouryoku.myShow
        Me.Show()
    End Sub

    '-------------------------------------------------------------------------------
    '   myActivate���\�b�h
    '-------------------------------------------------------------------------------
    Public Sub myActivate() Implements IfRturnSeisanNouryoku.myActivate
        Me.Activate()
    End Sub

#End Region

End Class