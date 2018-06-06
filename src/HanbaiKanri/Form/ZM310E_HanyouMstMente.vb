'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���j�ėp�}�X�^�����e���
'    �i�t�H�[��ID�jZM310E_HanyouMstMente
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ���V        2010/09/03                 �V�K              
'�@(2)   ����        2011/01/13                 �C���@�s�ǉ����̕s��Ή�              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView

Public Class ZM310E_HanyouMstMente
    Inherits System.Windows.Forms.Form

#Region "���e�����l��`"

    'PG���䕶��
    Private Const RS As String = "RecSet"                       '�f�[�^�Z�b�g�e�[�u����
    Private Const RS2 As String = "RecSet2"                     '�f�[�^�Z�b�g�e�[�u����
    Private Const N As String = ControlChars.NewLine            '���s����

    '�}�X�^�o�^���ڈꗗ�\���p�̌Œ�L�[��������
    Private Const KOTEIKEY As String = "00"

    '�}�X�^�o�^���ڈꗗ(��)�O���b�h�o�C���h��
    Private Const COLDT_TOP_CD As String = "dtCD"
    Private Const COLDT_TOP_KAHENCD As String = "dtKahenCD"
    Private Const COLDT_TOP_MEISYOU As String = "dtMeisyou"
    Private Const COLDT_TOP_BIKO As String = "dtBiko"
    Private Const COLDT_TOP_UPDDATE As String = "dtUpdDate"

    '�}�X�^�ڍ׈ꗗ(��)�O���b�h�o�C���h��
    Private Const COLDT_UND_CHECK As String = "dtCheck"
    Private Const COLDT_UND_KOTEIKEY As String = "dtKoteiKey"
    Private Const COLDT_UND_KAHENKEY As String = "dtKahenKey"
    Private Const COLDT_UND_NAME1 As String = "dtMeisyou1"
    Private Const COLDT_UND_NAME2 As String = "dtMeisyou2"
    Private Const COLDT_UND_NAME3 As String = "dtMeisyou3"
    Private Const COLDT_UND_NAME4 As String = "dtMeisyou4"
    Private Const COLDT_UND_NAME5 As String = "dtMeisyou5"
    Private Const COLDT_UND_NUM1 As String = "dtSuuti1"
    Private Const COLDT_UND_NUM2 As String = "dtSuuti2"
    Private Const COLDT_UND_NUM3 As String = "dtSuuti3"
    Private Const COLDT_UND_NUM4 As String = "dtSuuti4"
    Private Const COLDT_UND_NUM5 As String = "dtSuuti5"
    Private Const COLDT_UND_SORT As String = "dtHyoujijun"

    '�}�X�^�o�^���ڈꗗ(��)�O���b�h��
    Private Const COLCN_TOP_KOTEIKEY As String = "cnCD"
    Private Const COLCN_TOP_MEISYOU As String = "cnMeisyou"
    Private Const COLCN_TOP_KAHENKEY As String = "cnKahenCD"

    '�}�X�^�ڍ׈ꗗ(��)�O���b�h�o�C���h��
    Private Const COLCN_UND_KAHENKEY As String = "cnKahenKey"

    '�}�X�^�ڍ׈ꗗ(��)�O���b�h��
    Private Const COLNO_UND_CHECK As Integer = 0
    Private Const COLNO_UND_KAHEN As Integer = 2
    Private Const COLNO_UND_NAME1 As Integer = 3
    Private Const COLNO_UND_NAME2 As Integer = 4
    Private Const COLNO_UND_NAME3 As Integer = 5
    Private Const COLNO_UND_NAME4 As Integer = 6
    Private Const COLNO_UND_NAME5 As Integer = 7
    Private Const COLNO_UND_NUM1 As Integer = 8
    Private Const COLNO_UND_NUM2 As Integer = 9
    Private Const COLNO_UND_NUM3 As Integer = 10
    Private Const COLNO_UND_NUM4 As Integer = 11
    Private Const COLNO_UND_NUM5 As Integer = 12
    Private Const COLNO_UND_SORT As Integer = 13

    Private Const PGID As String = "ZM310E"             'DB�o�^���Ɏg�p����@�\ID

#End Region

#Region "�����o�ϐ���`"

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu

    Dim _dgv As UtilDataGridViewHandler         '�O���b�h�n���h���[

    Private _oldRowIndexUnder As Integer = 0                '�}�X�^�ڍ׈ꗗ(��)�I���s�̃J�[�\���ړ��O�̍s�ԍ�
    Private _oldColNameUnder As String = ""                 '�}�X�^�ڍ׈ꗗ(��)�I���s�̃J�[�\���ړ��O�̗�
    Private _oldRowIndexTop As Integer = -1                 '�}�X�^�o�^���ڈꗗ(��)�I���s�̃J�[�\���ړ��O�̍s�ԍ�
    Private _colorCtlFlgUnder As Boolean = False            '�}�X�^�ڍ׈ꗗ(��)�I���s�̔w�i�F��ύX���邽�߂̃t���O��錾
    
    Private _updateDgvUnderFlg As Boolean = False           '�}�X�^�ڍ׈ꗗ(��)�Z�����e�ҏW�t���O
    Private _dispDgvUnderFirstFlg As Boolean = False        '��ʋN�����Ƀ}�X�^�ڍ׈ꗗ(��)��\�������Ȃ����߂̃t���O

    Private _beforeCellData As String = ""                  '�}�X�^�ڍ׈ꗗ(��)�̒l�̕ύX�O�l

    Private _chkCellVO As UtilDgvChkCellVO                  '�}�X�^�ڍ׈ꗗ(��)���l�̂ݓ��͂̂��߂̕ϐ�

    Private _hankakuSuutiErrorFlg As Boolean = False        '���p�����`�F�b�N�ŃG���[���N�����ꍇ�̃t���O
    Private _hankakuSuutiErrorRowNo As Integer              '�}�X�^�ڍ׈ꗗ(��)���̓G���[���N�������Z���̍s��
    Private _hankakuSuutiErrorColNo As Integer              '�}�X�^�ڍ׈ꗗ(��)���̓G���[���N�������Z���̗�
    Private _kahenKeyErrorFlg As Boolean = True             '�σL�[�������͂܂��͏d�������ꍇ�̍s���F�t���O

    Private _updFlg As Boolean = False  '�X�V��
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
    '�R���X�g���N�^�@frmMZ02_01M����Ă΂��
    '-------------------------------------------------------------------------------
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

    '-------------------------------------------------------------------------------
    '�@�����J�n�C�x���g
    '-------------------------------------------------------------------------------
    Private Sub ZC910S_CodeSentaku_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            '�`��֌W�̐ݒ�
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           '�T�C�Y���ύX���ꂽ�Ƃ��ɁA�R���g���[�����R���g���[�����̂��ĕ`�悷�邩�ǂ����������l��ݒ�
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '�`��̓o�b�t�@�Ŏ��s����A������ɁA���ʂ���ʂɏo�͂����悤�ݒ�
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  '�R���g���[���́A��ʂɒ��ڂł͂Ȃ��A�܂��o�b�t�@�ɕ`�悳��܂��B����ɂ��A�������}���邱�Ƃ��ł��܂��B
            Me.SetStyle(ControlStyles.UserPaint, True)              '�R���g���[���́A�I�y���[�e�B���O �V�X�e���ɂ���Ăł͂Ȃ��A�Ǝ��ɕ`�悳���悤�ݒ�
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   '�R���g���[���̓E�B���h�E ���b�Z�[�W WM_ERASEBKGND �𖳎�����悤�ɐݒ�

            Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr '�^�C�g���I�v�V�����\��

            _dispDgvUnderFirstFlg = True
            '�R���g���[���ݒ�
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
    '�@�}�X�^�o�^���ڂ̕ҏW�{�^������
    '�@(�������e)�}�X�^�o�^���ڂ̕ҏW�q��ʂ��J��
    '------------------------------------------------------------------------------------------------------
    Private Sub btnHensyuu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHensyuu.Click
        Try

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvJuyousaki)
            Dim rowcnt As Integer = dgvJuyousaki.CurrentCellAddress.Y

            '���݃J�[�\��������s�̉σL�[���擾
            Dim kahenKey As String = gh.getCellData(COLDT_TOP_KAHENCD, rowcnt)
            '�q��ʕ\��
            Dim openForm As ZM311S_HanyouMstHensyuu = New ZM311S_HanyouMstHensyuu(_msgHd, _db, Me, kahenKey)      '�p�����^��J�ڐ��ʂ֓n��
            openForm.ShowDialog(Me)                                                             '��ʕ\��
            openForm.Dispose()

            '�}�X�^�o�^���ڈꗗ(��)�̍ĕ\��
            Call dispDgvTop()

            '���l�A�X�V�����̎擾
            Dim biko As String = ""
            Dim kousinDate As String = ""
            biko = gh.getCellData(COLDT_TOP_BIKO, rowcnt)
            kousinDate = CStr(gh.getCellData(COLDT_TOP_UPDDATE, rowcnt))

            '���l�ƍX�V�������x���̕\��
            lblKoumokuSetumei.Text = biko

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�s�ǉ��{�^������
    '�@(�������e)�ڍ׈ꗗ(��)�̍ŏI�s�ɋ�s��ǉ�����
    '------------------------------------------------------------------------------------------------------
    Private Sub btnTuika_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTuika.Click
        Try

            Dim dt As DataTable = CType(dgvHanyouMst.DataSource, DataSet).Tables(RS2)

            Dim newRow As DataRow = dt.NewRow

            '����DataTable�̍ŏI�s��VO��}��
            dt.Rows.InsertAt(newRow, dt.Rows.Count)

            '' 2011/01/13 add start sugano
            '�Œ�L�[�̎擾
            Dim rowcnt As Integer
            rowcnt = dgvJuyousaki.CurrentCellAddress.Y
            Dim gh_hd As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvJuyousaki)
            Dim koteikey As String = ""
            koteikey = gh_hd.getCellData(COLDT_TOP_KAHENCD, rowcnt)

            '�ǉ��s�̔�\����ɌŒ�L�[���Z�b�g
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHanyouMst)
            gh.setCellData(COLDT_UND_KOTEIKEY, dt.Rows.Count - 1, koteikey)
            '' 2011/01/13 add end sugano

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�o�^�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnTouroku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTouroku.Click
        Try

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHanyouMst)
            Dim rowcnt As Integer = gh.getMaxRow

            '���̓`�F�b�N
            Call checkTourokuBtn(rowcnt)

            '�o�^�m�F�_�C�A���O�\��
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmInsert")   '�o�^���܂��B��낵���ł����H
            If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            '�}�E�X�J�[�\�������v
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor
            Try

                Call registDB()

            Finally
                '�}�E�X�J�[�\�����
                Me.Cursor = cur
            End Try

            Call _msgHd.dspMSG("completeRun")        '�������������܂���

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        Finally
            '�}�E�X�J�[�\�����
            Me.Cursor = Cursors.Arrow
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�߂�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnModoru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModoru.Click

        '�x�����b�Z�[�W
        If _updateDgvUnderFlg Then
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmDgvEdit")   '�ҏW���̓��e���j������܂��B��낵���ł����H
            If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        '����ʂ��I�����A���j���[��ʂɖ߂�B
        _parentForm.Show()
        _parentForm.Activate()

        Me.Close()

    End Sub

#End Region

#Region "���[�U��`�֐�:��ʐ���"

    '-------------------------------------------------------------------------------
    '�@�R���g���[�������ݒ�
    '-------------------------------------------------------------------------------
    Private Sub initForm()

        '�����l�ݒ�
        Call dispDgvTop()

        '�{�^���g�p�ېݒ�
        btnHensyuu.Enabled = True
        btnTouroku.Enabled = True
        btnTuika.Enabled = True

    End Sub

#End Region

#Region "���[�U��`�֐�:DGV�֘A"

    '------------------------------------------------------------------------------------------------------
    '�I���s�ɒ��F���鏈��(�}�X�^�o�^���ڈꗗ�E��)����у��x���E�ڍ׈ꗗ(��)�̕\���A�{�^���̉ېݒ�
    '------------------------------------------------------------------------------------------------------
    Private _NonSelectionChangedBySetCurrentCell As Boolean = False
    Private _NonSelectionChangedBydispDGV As Boolean = False            '�q��ʂ���߂��Ă����ꍇ�ɁA�}�X�^�ڍ׈ꗗ(��)�ɍĕ\�������Ȃ����߂̃t���O
    Private Sub dgvJuyousaki_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvJuyousaki.SelectionChanged
        If _NonSelectionChangedBySetCurrentCell Or _
           _NonSelectionChangedBydispDGV Then Exit Sub

        dgvJuyousaki.Columns(COLCN_TOP_KAHENKEY).DefaultCellStyle.SelectionBackColor = StartUp.lCOLOR_BLUE
        dgvJuyousaki.Columns(COLCN_TOP_MEISYOU).DefaultCellStyle.SelectionBackColor = StartUp.lCOLOR_BLUE

        Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvJuyousaki)

        Try
            '�ڍ׈ꗗ�̓��e���ύX����Ă���ꍇ�A���e��j�����Ă悢���m�F����B
            If _updateDgvUnderFlg Then
                '�o�^�m�F�_�C�A���O�\��
                Dim rtn As DialogResult = _msgHd.dspMSG("confirmDgvEdit")   '�ҏW���̓��e���j������܂��B��낵���ł����H
                If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                    _updateDgvUnderFlg = False
                    gh.setSelectionRowColor(_oldRowIndexTop, dgvJuyousaki.CurrentCellAddress.Y, StartUp.lCOLOR_BLUE)
                    _dispDgvUnderFirstFlg = True

                    _NonSelectionChangedBySetCurrentCell = True
                    Try
                        gh.setCurrentCell(COLCN_TOP_MEISYOU, _oldRowIndexTop)
                    Finally
                        _NonSelectionChangedBySetCurrentCell = False
                    End Try
                    _updateDgvUnderFlg = True
                    Exit Sub
                End If
            End If
            _updateDgvUnderFlg = False
            _oldRowIndexTop = dgvJuyousaki.CurrentCellAddress.Y

            '���݂̍s�ԍ����擾
            Dim rowcnt As Integer
            rowcnt = dgvJuyousaki.CurrentCellAddress.Y 'e.RowIndex

            '���l�A�X�V�����̎擾
            Dim biko As String = ""
            Dim kousinDate As String = ""
            biko = gh.getCellData(COLDT_TOP_BIKO, rowcnt)
            kousinDate = CStr(gh.getCellData(COLDT_TOP_UPDDATE, rowcnt))

            '���l�ƍX�V�������x���̕\��
            lblKoumokuSetumei.Text = biko
            lblKousinDate.Text = kousinDate

            '�σL�[�̎擾
            Dim kahen As String = ""
            kahen = gh.getCellData(COLDT_TOP_KAHENCD, rowcnt)
            '���F�t���O��|���A���F�ϐ������Z�b�g����
            _colorCtlFlgUnder = False
            _oldRowIndexUnder = 0
            '�}�X�^�ڍ׈ꗗ(��)�̕\��
            Call dispMstMeisai(kahen)
            _colorCtlFlgUnder = True
            dgvJuyousaki.Focus()

            '�{�^���̎g�p�ېݒ�
            btnHensyuu.Enabled = True
            btnTouroku.Enabled = True
            btnTuika.Enabled = True

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�}�X�^�o�^���ڈꗗ�Z���ҏW�J�n������
    '�@(�������e)�@�ҏW�O�̒l���擾����
    '�@�@�@�@�@�@�@����̗�ɐ��l���̓��[�h�̐�����������
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvHanyouMst_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvHanyouMst.EditingControlShowing
        '�ҏW�O�̒l��ێ�
        _beforeCellData = CStr(_db.rmNullStr(dgvHanyouMst.CurrentCell.Value))

        Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHanyouMst)

        '�����l�P�A���l�Q�A���l�R�A�\�����̏ꍇ�A�O���b�h�ɐ��l���̓��[�h�̐�����������
        If dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_NUM1 Or _
                dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_NUM2 Or _
                dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_NUM3 Or _
                dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_NUM4 Or _
                dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_NUM5 Or _
                dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_SORT Then

            _chkCellVO = gh.chkCell(sender, e, UtilDataGridViewHandler.chkType.Num)
        End If
    End Sub

    '-------------------------------------------------------------------------------
    '   �T�C�Y�ʈꗗ�@�I���Z�����؃C�x���g�iDataError�C�x���g�j
    '   �i�����T�v�j���l���͗��ɐ��l�ȊO�����͂��ꂽ�ꍇ�̃G���[����
    '-------------------------------------------------------------------------------
    Private Sub dgvHanyouMst_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvHanyouMst.DataError

        Try
            e.Cancel = False                                   '�ҏW���[�h�I��

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHanyouMst)
            '�����l�P�A���l�Q�A���l�R�A�\�����̏ꍇ�A�O���b�h�ɂ͐��l���̓��[�h(0�`9)�̐����������Ă���̂ŁA�����̉���
            If dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_NUM1 Or _
                dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_NUM2 Or _
                dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_NUM3 Or _
                dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_NUM4 Or _
                dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_NUM5 Or _
                dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_SORT Then
                gh.AfterchkCell(_chkCellVO)
            End If

            '�G���[�t���O�𗧂Ă�
            _hankakuSuutiErrorFlg = True

            Dim colName As String = ""
            Select Case e.ColumnIndex
                Case COLNO_UND_NUM1
                    colName = COLDT_UND_NUM1
                Case COLNO_UND_NUM2
                    colName = COLDT_UND_NUM2
                Case COLNO_UND_NUM3
                    colName = COLDT_UND_NUM3
                Case COLNO_UND_NUM4
                    colName = COLDT_UND_NUM4
                Case COLNO_UND_NUM5
                    colName = COLDT_UND_NUM5
                Case Else
                    colName = COLDT_UND_NUM1
            End Select

            '�������͂��ꂽ��Z������ɂ���
            gh.setCellData(colName, e.RowIndex, DBNull.Value)

            Throw New UsrDefException("���p�����̂ݓ��͉\�ł��B", _msgHd.getMSG("onlyAcceptNumeric"))

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@�I���s�ɒ��F���鏈��(�}�X�^�ڍׁE��)
    '-------------------------------------------------------------------------------
    Private Sub dgvHanyouMst_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvHanyouMst.CellEnter

        Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHanyouMst)

        '�I���s���F
        '�σL�[�̖����́E�d���`�F�b�N�ŃG���[���������ꍇ�͏������s��Ȃ�
        If _colorCtlFlgUnder And _kahenKeyErrorFlg Then
            gh.setSelectionRowColor(dgvHanyouMst.CurrentCellAddress.Y, _oldRowIndexUnder, StartUp.lCOLOR_BLUE)
        Else
            _kahenKeyErrorFlg = True
            Exit Sub
        End If
        _oldRowIndexUnder = dgvHanyouMst.CurrentCellAddress.Y
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�}�X�^�o�^���ڈꗗ�Z���ҏW�I��������
    '(�������e)�@�J�n���ƏI�����ɃZ���̓��e���ς���Ă����ꍇ�A�ύX�ς݃t���O�𗧂Ă�
    '�@�@�@�@�@�@�σL�[���ύX���ꂽ�ꍇ�͓��̓`�F�b�N���s��
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvHanyouMst_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvHanyouMst.CellEndEdit
        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHanyouMst)

            '�ύX��̒l�̎擾
            Dim afterCellData As String = ""
            afterCellData = CStr(_db.rmNullStr(dgvHanyouMst.CurrentCell.Value))

            '���̓G���[���������ꍇ�͏������s��Ȃ�
            If _hankakuSuutiErrorFlg Then
                '�ύX���ꂽ�ꍇ�̓t���O�𗧂Ă�
                If Not _beforeCellData.Equals(afterCellData) Then
                    _updateDgvUnderFlg = True
                Else
                    _updateDgvUnderFlg = False
                End If
                Exit Sub
            End If

            '�����l�P�A���l�Q�A���l�R�A�\�����̏ꍇ�A�O���b�h�ɂ͐��l���̓��[�h(0�`9)�̐����������Ă���̂ŁA�����̉���
            If dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_NUM1 Or _
                dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_NUM2 Or _
                dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_NUM3 Or _
                dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_NUM4 Or _
                dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_NUM5 Or _
                dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_SORT Then
                gh.AfterchkCell(_chkCellVO)
            End If

            '�σL�[���ύX���ꂽ�ꍇ�͓��̓`�F�b�N���Ăяo��
            If e.ColumnIndex = COLNO_UND_KAHEN Then
                Call checkKahenKey(afterCellData)
            End If

            '�ύX���ꂽ�ꍇ�̓t���O�𗧂Ă�
            If Not _beforeCellData.Equals(afterCellData) Then
                _updateDgvUnderFlg = True
            Else
                _updateDgvUnderFlg = False
            End If

            _beforeCellData = ""

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

#End Region

#Region "���[�U��`�֐�:DB�֘A"

    '-------------------------------------------------------------------------------
    '�@�}�X�^�o�^���ڈꗗ�\��(��̈ꗗ)
    '-------------------------------------------------------------------------------
    Private Sub dispDgvTop()

        Try

            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & " KOTEIKEY " & COLDT_TOP_CD
            sql = sql & N & " ,NAME1 " & COLDT_TOP_MEISYOU
            sql = sql & N & " ,KAHENKEY " & COLDT_TOP_KAHENCD
            sql = sql & N & " ,BIKO " & COLDT_TOP_BIKO
            sql = sql & N & " ,UPDDATE " & COLDT_TOP_UPDDATE
            sql = sql & N & " FROM M01HANYO "
            sql = sql & N & " WHERE KOTEIKEY = " & KOTEIKEY
            sql = sql & N & " ORDER BY SORT "

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                Throw New UsrDefException("�o�^����Ă��܂���B", _msgHd.getMSG("noData"))
            End If


            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(Me.dgvJuyousaki)
            Dim saveKeyType As String = ""
            '�q��ʂ���߂��Ă������ɁA�}�X�^���ڈꗗ(��)�t�H�[�J�X�����̈ʒu�ɖ߂����߉σL�[��ێ�
            If (dgvJuyousaki.DataSource IsNot Nothing) AndAlso (gh.getMaxRow > 0) Then
                saveKeyType = gh.getCellData(COLDT_TOP_KAHENCD, dgvJuyousaki.CurrentCellAddress.Y)
            End If

            '���o�f�[�^���ꗗ�ɕ\������
            If _dispDgvUnderFirstFlg Then       '��ʋN�����͈ꗗ(��)��\������B
                _dispDgvUnderFirstFlg = False
            Else
                _NonSelectionChangedBydispDGV = True        '�q��ʂ���߂��Ă����ꍇ�ɁA�}�X�^�ڍ׈ꗗ(��)�ɍĕ\�������Ȃ�
            End If
            Try
                dgvJuyousaki.DataSource = ds
                dgvJuyousaki.DataMember = RS
            Finally
                _NonSelectionChangedBydispDGV = False
            End Try

            '�}�X�^���ڈꗗ(��)�̃t�H�[�J�X���q��ʌďo�O�ɖ߂�
            If Not "".Equals(saveKeyType) Then
                For i As Integer = 0 To gh.getMaxRow - 1
                    If saveKeyType.Equals(gh.getCellData(COLDT_TOP_KAHENCD, i)) Then
                        _NonSelectionChangedBydispDGV = True
                        Try
                            Call gh.setCurrentCell(COLCN_TOP_KAHENKEY, i)
                        Finally
                            _NonSelectionChangedBydispDGV = False
                        End Try
                        Exit For
                    End If
                Next
            End If

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�}�X�^�ڍ׈ꗗ�\��(���̈ꗗ)
    '------------------------------------------------------------------------------------------------------
    Private Sub dispMstMeisai(ByVal prmKahenKey As String)
        Try

            '�}�X�^�ڍׂ��ꗗ�\��
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & " KOTEIKEY " & COLDT_UND_KOTEIKEY
            sql = sql & N & " ,KAHENKEY " & COLDT_UND_KAHENKEY
            sql = sql & N & " ,NAME1 " & COLDT_UND_NAME1
            sql = sql & N & " ,NAME2 " & COLDT_UND_NAME2
            sql = sql & N & " ,NAME3 " & COLDT_UND_NAME3
            sql = sql & N & " ,NAME4 " & COLDT_UND_NAME4
            sql = sql & N & " ,NAME5 " & COLDT_UND_NAME5
            sql = sql & N & " ,NUM1 " & COLDT_UND_NUM1
            sql = sql & N & " ,NUM2 " & COLDT_UND_NUM2
            sql = sql & N & " ,NUM3 " & COLDT_UND_NUM3
            sql = sql & N & " ,NUM4 " & COLDT_UND_NUM4
            sql = sql & N & " ,NUM5 " & COLDT_UND_NUM5
            sql = sql & N & " ,SORT " & COLDT_UND_SORT
            sql = sql & N & " FROM M01HANYO"
            sql = sql & N & " WHERE KOTEIKEY = '" & _db.rmSQ(prmKahenKey) & "'"
            sql = sql & N & " ORDER BY SORT "

            'SQL���s
            Dim recCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS2, recCnt)

            If recCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                Throw New UsrDefException("�o�^����Ă��܂���B", _msgHd.getMSG("noData"))
            End If

            '���o�f�[�^���ꗗ�ɕ\������
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(Me.dgvHanyouMst)
            gh.clearRow()
            dgvHanyouMst.DataSource = ds
            dgvHanyouMst.DataMember = RS2

            '�����\��
            lblKensuu.Text = CStr(recCnt) & "��"

            '�}�X�^�ڍ׈ꗗ(��)�̐擪�s�𒅐F
            gh.setSelectionRowColor(0, 0, StartUp.lCOLOR_BLUE)

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@DB�o�^
    '�@(�������e)�o�^�{�^����������DB�o�^���s��
    '------------------------------------------------------------------------------------------------------
    Private Sub registDB()
        Try
            Dim updStartDate As Date = Now      '�����J�n����

            Dim sql As String = ""
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(Me.dgvHanyouMst)

            _db.beginTran()
            Dim koteiKey As String = gh.getCellData(COLDT_UND_KOTEIKEY, 0)

            Dim cnt As Integer = 0

            sql = " DELETE FROM "
            sql = sql & N & " M01HANYO "
            sql = sql & N & " WHERE KOTEIKEY = '" & koteiKey & "'"
            _db.executeDB(sql, cnt)


            Dim machineId As String = SystemInformation.ComputerName    '�[��Id
            Dim updDate As Date = Now                                   '�X�V��������я����I������
            Dim insertCount As Integer = 0                              '�C���T�[�g����

            '�ėp�}�X�^�X�V
            Dim rowCnt As Integer = gh.getMaxRow
            For i As Integer = 0 To rowCnt - 1
                Dim checkBoxValue As Object = dgvHanyouMst(COLNO_UND_CHECK, i).Value
                If Not checkBoxValue Then
                    sql = ""
                    sql = "INSERT INTO M01HANYO ("
                    sql = sql & N & " KOTEIKEY, "
                    sql = sql & N & " KAHENKEY, "
                    sql = sql & N & " NAME1, "
                    sql = sql & N & " NAME2, "
                    sql = sql & N & " NAME3, "
                    sql = sql & N & " NAME4, "
                    sql = sql & N & " NAME5, "
                    sql = sql & N & " NUM1, "
                    sql = sql & N & " NUM2, "
                    sql = sql & N & " NUM3, "
                    sql = sql & N & " NUM4, "
                    sql = sql & N & " NUM5, "
                    sql = sql & N & " SORT, "
                    sql = sql & N & " BIKO, "
                    sql = sql & N & " UPDNAME, "
                    sql = sql & N & " UPDDATE) "
                    sql = sql & N & "   VALUES ("
                    sql = sql & N & "       '" & _db.rmNullStr(gh.getCellData(COLDT_UND_KOTEIKEY, i)) & "', "
                    sql = sql & N & "       '" & _db.rmNullStr(_db.rmSQ(gh.getCellData(COLDT_UND_KAHENKEY, i))) & "', "
                    sql = sql & N & "       '" & _db.rmNullStr(_db.rmSQ(gh.getCellData(COLDT_UND_NAME1, i))) & "', "
                    sql = sql & N & "       '" & _db.rmNullStr(_db.rmSQ(gh.getCellData(COLDT_UND_NAME2, i))) & "', "
                    sql = sql & N & "       '" & _db.rmNullStr(_db.rmSQ(gh.getCellData(COLDT_UND_NAME3, i))) & "', "
                    sql = sql & N & "       '" & _db.rmNullStr(_db.rmSQ(gh.getCellData(COLDT_UND_NAME4, i))) & "', "
                    sql = sql & N & "       '" & _db.rmNullStr(_db.rmSQ(gh.getCellData(COLDT_UND_NAME5, i))) & "', "
                    sql = sql & N & "       " & NS(gh.getCellData(COLDT_UND_NUM1, i)) & " , "
                    sql = sql & N & "       " & NS(gh.getCellData(COLDT_UND_NUM2, i)) & " , "
                    sql = sql & N & "       " & NS(gh.getCellData(COLDT_UND_NUM3, i)) & " , "
                    sql = sql & N & "       " & NS(gh.getCellData(COLDT_UND_NUM4, i)) & " , "
                    sql = sql & N & "       " & NS(gh.getCellData(COLDT_UND_NUM5, i)) & " , "
                    sql = sql & N & "       " & NS(gh.getCellData(COLDT_UND_SORT, i)) & " , "
                    sql = sql & N & "       '', "
                    sql = sql & N & "       '" & machineId & "', "
                    sql = sql & N & "       TO_DATE('" & updDate & "', 'YYYY/MM/DD HH24:MI:SS')) "

                    _db.executeDB(sql, cnt)
                    insertCount = insertCount + 1
                End If
            Next

            Dim deleteCount As Integer = rowCnt - insertCount     'DELETE����

            '�X�V�I���������擾
            Dim updFinDate As Date = Now

            '�v������擾
            Dim keikakuDate As String = lblKousinDate.Text.Substring(0, 4) & lblKousinDate.Text.Substring(5, 2)

            '���s�����e�[�u���X�V
            sql = ""
            sql = sql & N & " INSERT INTO T91RIREKI ("
            sql = sql & N & " PGID, "
            sql = sql & N & " SDATESTART, "
            sql = sql & N & " SDATEEND, "
            sql = sql & N & " KENNSU1, "
            sql = sql & N & " KENNSU2, "
            sql = sql & N & " NAME1, "
            sql = sql & N & " UPDNAME, "
            sql = sql & N & " UPDDATE) "
            sql = sql & N & " VALUEs ("
            sql = sql & N & "   '" & PGID & "', "
            sql = sql & N & "       TO_DATE('" & updStartDate & "',  'YYYY/MM/DD HH24:MI:SS'), "
            sql = sql & N & "       TO_DATE('" & updFinDate & "',  'YYYY/MM/DD HH24:MI:SS'), "
            sql = sql & N & "       " & NS(deleteCount) & " , "
            sql = sql & N & "       " & NS(insertCount) & " , "
            sql = sql & N & "       '" & _db.rmNullStr(gh.getCellData(COLDT_UND_KOTEIKEY, 0)) & "', "
            sql = sql & N & "   '" & machineId & "' ,"
            sql = sql & N & "   TO_DATE('" & updFinDate & "', 'YYYY/MM/DD HH24:MI:SS')) "
            _db.executeDB(sql, cnt)

            'T02��������e�[�u���X�V
            _parentForm.updateSeigyoTbl(PGID, True, updStartDate, updFinDate)

            _db.commitTran()

            _updateDgvUnderFlg = False
            lblKousinDate.Text = updDate.ToString("yyyy/MM/dd HH:mm:ss")

            _colorCtlFlgUnder = False
            Call dispMstMeisai(gh.getCellData(COLDT_UND_KOTEIKEY, 0))
            _colorCtlFlgUnder = True

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        Finally
            If _db.isTransactionOpen Then
                Call _db.rollbackTran()
            End If
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@NULL����
    '�@(�������e)�Z���̓��e��NULL�Ȃ�"NULL"��Ԃ�
    '------------------------------------------------------------------------------------------------------
    Private Function NS(ByVal prmStr As String) As String
        Dim ret As String = ""
        If "".Equals(prmStr) Then
            ret = "NULL"
        Else
            ret = prmStr
        End If
        Return ret
    End Function
#End Region

#Region "���[�U��`�֐�:���̓`�F�b�N"
    '------------------------------------------------------------------------------------------------------
    '�@�o�^�{�^�����������̓`�F�b�N
    '�@(�������e)���l�A�\�����̓��̓`�F�b�N���s��
    '------------------------------------------------------------------------------------------------------
    Private Sub checkTourokuBtn(ByVal prmRowCnt As Integer)
        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHanyouMst)

            '�σL�[�̕K�{�`�F�b�N
            For rowCntForHissu As Integer = 0 To prmRowCnt - 1
                If "".Equals(_db.rmNullStr(gh.getCellData(COLDT_UND_KAHENKEY, rowCntForHissu))) Then
                    Dim checkBoxValue As Object = dgvHanyouMst(COLNO_UND_CHECK, rowCntForHissu).Value
                    If Not checkBoxValue Then
                        '���݂̍s�̒��F��߂��A�G���[�̂������s�𒅐F
                        dgvHanyouMst.Rows(dgvHanyouMst.CurrentCellAddress.Y).DefaultCellStyle.BackColor = StartUp.lCOLOR_WHITE
                        dgvHanyouMst.Rows(rowCntForHissu).DefaultCellStyle.BackColor = StartUp.lCOLOR_BLUE
                        _oldRowIndexUnder = rowCntForHissu
                        _kahenKeyErrorFlg = False

                        '���b�Z�[�W��\�����A�G���[�Z���Ƀt�H�[�J�X����
                        Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput"), dgvHanyouMst, COLCN_UND_KAHENKEY, rowCntForHissu)
                    End If
                End If
            Next

            '�σL�[�̏d���`�F�b�N
            For rowCntForTyofuku As Integer = 0 To prmRowCnt - 1

                Dim checkedKahenKey As String = gh.getCellData(COLDT_UND_KAHENKEY, rowCntForTyofuku)    '�`�F�b�N����σL�[
                If Not "".Equals(checkedKahenKey) Then
                    Dim tyofukuErrorFlg As Boolean = False         '�σL�[�d������t���O
                    Dim errorRowNo As Integer = 0

                    For i As Integer = 0 To prmRowCnt - 1
                        Dim loopKahenKey As String = gh.getCellData(COLDT_UND_KAHENKEY, i)                  '�S�ẲσL�[
                        If checkedKahenKey.Equals(loopKahenKey) Then
                            '�d����2�񂠂����ꍇ�G���[
                            If tyofukuErrorFlg = True Then

                                '���݂̍s�̒��F��߂��A�G���[�̂������s�𒅐F
                                dgvHanyouMst.Rows(dgvHanyouMst.CurrentCellAddress.Y).DefaultCellStyle.BackColor = StartUp.lCOLOR_WHITE
                                dgvHanyouMst.Rows(errorRowNo).DefaultCellStyle.BackColor = StartUp.lCOLOR_BLUE
                                _oldRowIndexUnder = errorRowNo
                                _kahenKeyErrorFlg = False

                                '���b�Z�[�W��\�����A�G���[�Z���Ƀt�H�[�J�X����
                                Throw New UsrDefException("�σL�[���d�����Ă��܂��B", _msgHd.getMSG("NotUniqueKahenKey"), dgvHanyouMst, COLCN_UND_KAHENKEY, errorRowNo)
                            Else
                                '�d�����������s�ԍ���ێ����A�G���[�t���O�𗧂Ă�
                                errorRowNo = i
                                tyofukuErrorFlg = True
                            End If
                        End If
                    Next

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
    '�@���p���l�`�F�b�N
    '�@(�������e)�ꗗ�̓��͓��e�̔��p���l�`�F�b�N
    '------------------------------------------------------------------------------------------------------
    Private Sub checkHankakuEisu(ByVal prmRowCnt As Integer, ByVal prmColDataName As String, ByVal prmGh As UtilDataGridViewHandler)
        Try
            For i As Integer = 0 To prmRowCnt
                Dim checkCellData As String = ""
                checkCellData = prmGh.getCellData(prmColDataName, i)
                '���p�`�F�b�N
                If UtilClass.isOnlyNStr(checkCellData) = False Then
                    Throw New UsrDefException("���p�����̂ݓ��͉\�ł��B", _msgHd.getMSG("onlyAcceptNumeric"))
                End If
                '���l�`�F�b�N
                If Not IsNumeric(checkCellData) Then
                    Throw New UsrDefException("���p�����̂ݓ��͉\�ł��B", _msgHd.getMSG("onlyAcceptNumeric"))
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
    '�@�σL�[���̓`�F�b�N
    '�@(�������e)�σL�[�̔��p�`�F�b�N�A�d���`�F�b�N���s��
    '------------------------------------------------------------------------------------------------------
    Private Sub checkKahenKey(ByVal prmAfterCellData As String)
        Try

            '���p�`�F�b�N
            If UtilClass.isOnlyNStr(prmAfterCellData) = False Then
                Throw New UsrDefException("���p�p���̂ݓ��͉\�ł��B", _msgHd.getMSG("onlyAcceptHankakuEisu"))
            End If

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

#End Region

    Private Sub dgvJuyousaki_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvJuyousaki.CellContentClick

    End Sub
End Class