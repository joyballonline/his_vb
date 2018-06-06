'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���j�i��敪�}�X�^�����e���
'    �i�t�H�[��ID�jZM210E_HinsyuKbn
'
'===============================================================================
'�@�����@���O�@         ���@�t          �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ����           2010/09/22                  �V�K
'  (2)   ���V           2010/09/29                  �q��ʂɓn���p�����[�^�ǉ�
'  (3)   ���V           2010/11/01                  �q��ʂŕi��敪�}�X�^��\�����郍�W�b�N�ǉ�
'  (4)   ���V           2010/11/22                  �i��敪�q��ʌďo�{�^���폜�A�i��敪�����͉�
'-------------------------------------------------------------------------------
Option Explicit On
Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView

Public Class ZM210E_HinsyuKbn
    Inherits System.Windows.Forms.Form
    Implements IfRturnKahenKey
#Region "���e�����l��`"

    'PG���䕶�� 
    Private Const N As String = ControlChars.NewLine                    '���s����
    Private Const RS As String = "RecSet"                               '���R�[�h�Z�b�g�e�[�u��

    '�O���b�h��ԍ�
    Private Const COLNO_JUYOUSAKIBTN As Integer = 0
    '-->2010/11/22 del start nakazawa   
    Private Const COLNO_HINSYUKBNBTN As Integer = 1
    '<--2010/11/22 del end nakazawa   
    '-->2010/11/22 upd start nakazawa   
    'Private Const COLNO_SAKUJOCHK As Integer = 2
    'Private Const COLNO_JUYOUSAKIKBN As Integer = 3
    'Private Const COLNO_JUYOUSAKI As Integer = 4
    'Private Const COLNO_HINSYUCD As Integer = 5
    'Private Const COLNO_HINSYUKBN As Integer = 6
    Private Const COLNO_SAKUJOCHK As Integer = 1
    Private Const COLNO_JUYOUSAKIKBN As Integer = 2
    Private Const COLNO_JUYOUSAKI As Integer = 3
    Private Const COLNO_HINSYUCD As Integer = 4
    Private Const COLNO_HINSYUKBN As Integer = 5
    '<--2010/11/22 upd end nakazawa   

    'M01�ėp�}�X�^�Œ跰
    Private Const KOTEIKEY_JUYOUCD As String = "01"                     '���v��R�[�h
    
    'M02�i��敪�}�X�^
    Private Const M02_JUYOUSAKI As String = ""
    Private Const M02_HINSYUKBN As String = ""
    Private Const M02_HINSYUKBNNM As String = ""


    '�폜�t���O
    Private Const SAKUJO_FLG As String = "1"

    '�ύX�t���O
    Private Const HENKO_FLG As String = "1"

    '�v���O����ID�iT91���s�����e�[�u���o�^�p�j
    Private Const PGID As String = "ZM210E"

    '�ꗗ�o�C���hDetaSet��
    Private Const COLDT_SAKUJOCHK As String = "dtSakujoChk"             '�폜�`�F�b�N
    Private Const COLDT_JUYOUSAKI As String = "dtJuyousakiCD"           '���v��
    Private Const COLDT_JUYOUSAKINM As String = "dtJuyousaki"           '���v�於
    Private Const COLDT_HINSYUKBN As String = "dtCD"                    '�i��敪
    Private Const COLDT_HINSYUKBNNM As String = "dtHinsyu"              '�i��敪��
    Private Const COLDT_HENKOUFLG As String = "dtHenkouFlg"             '�ύX�t���O
    Private Const COLDT_HENKOMAEJUYOU As String = "dtHenkomaeJuyou"     '�ύX�O���v��
    Private Const COLDT_HENKOMAEHINSYU As String = "dtHenkomaeHinsyu"   '�ύX�O�i��敪

    '-->2010/11/22 add start nakazawa   
    '�ꗗ��
    Private Const COLCN_JUYOUSAKI As String = "cnJuyousakiCD"           '���v��
    Private Const COLCN_HINSYUKBN As String = "cnCD"                    '�i��敪
    '<--2010/11/22 add end nakazawa   

#End Region

#Region "�����o�ϐ���`"

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu

    Private _dgv As UtilDataGridViewHandler         '�O���b�h�n���h���[

    Private _oldRowIndex As Integer = -1            '�I���s�̔w�i�F��ύX���邽�߂̕ϐ�
    Private _colorCtlFlg As Boolean = False         '�I���s�̔w�i�F��ύX���邽�߂̃t���O

    Private _formOpenFlg As Boolean                 '��ʋN�����ł��邩�𔻕ʂ��邽�߂̃t���O

    Private _ZC910KahenKey As String                'ZC910S_CodeSentaku����󂯎��ėp�}�X�^�σL�[
    Private _ZC910Meisyo1 As String                 'ZC910S_CodeSentaku����󂯎��ėp�}�X�^���̂P

    Private _updFlg As Boolean = False              '�X�V��
    Private _dgvChangeFlg As Boolean = False        '�ꗗ�ύX�t���O
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
    '�R���X�g���N�^�iPrivate�ɂ��āA�O����͌ĂׂȂ��悤�ɂ���j
    '------------------------------------------------------------------------------------------------------
    Private Sub New()

        '��ʋN�����̔��ʗp�t���O��������
        _formOpenFlg = True

        ' ���̌Ăяo���́AWindows �t�H�[�� �f�U�C�i�ŕK�v�ł��B
        InitializeComponent()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�R���X�g���N�^�@���j���[����Ă΂��
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

    '-------------------------------------------------------------------------------
    '�@�����J�n�C�x���g
    '-------------------------------------------------------------------------------
    Private Sub ZM210E_HinsyuKbn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            '�`��֌W�̐ݒ�
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           '�T�C�Y���ύX���ꂽ�Ƃ��ɁA�R���g���[�����R���g���[�����̂��ĕ`�悷�邩�ǂ����������l��ݒ�
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '�`��̓o�b�t�@�Ŏ��s����A������ɁA���ʂ���ʂɏo�͂����悤�ݒ�
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  '�R���g���[���́A��ʂɒ��ڂł͂Ȃ��A�܂��o�b�t�@�ɕ`�悳��܂��B����ɂ��A�������}���邱�Ƃ��ł��܂��B
            Me.SetStyle(ControlStyles.UserPaint, True)              '�R���g���[���́A�I�y���[�e�B���O �V�X�e���ɂ���Ăł͂Ȃ��A�Ǝ��ɕ`�悳���悤�ݒ�
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   '�R���g���[���̓E�B���h�E ���b�Z�[�W WM_ERASEBKGND �𖳎�����悤�ɐݒ�

            Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr '�^�C�g���I�v�V�����\��

            '�ꗗ�\��
            Call dispDGV()

            '��ʋN�����̔��ʗp�t���O�̐ݒ�
            _formOpenFlg = False

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

#End Region

#Region "�{�^���C�x���g"

    '------------------------------------------------------------------------------------------------------
    '�@�V�K�ǉ��{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnSinki_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSinki.Click

        Dim dt As DataTable = CType(dgvHinsyuMst.DataSource, DataSet).Tables(RS)
        Dim newRow As DataRow = dt.NewRow

        '����DataTable�̍ŏI�s��VO��}��
        dt.Rows.InsertAt(newRow, dt.Rows.Count)

        '�ǉ������s�̕ύX�t���O�𗧂Ă�B
        '���ύX�t���O�������Ă���f�[�^���o�^�ΏۂɂȂ邽��
        Dim lRowCnt As Long = dt.Rows.Count
        _dgv.setCellData(COLDT_HENKOUFLG, lRowCnt - 1, HENKO_FLG)

        '�ǉ������s�̍폜�`�F�b�N������������iFalse�ɐݒ肷��j
        _dgv.setCellData(COLDT_SAKUJOCHK, lRowCnt - 1, False)

        '�����̕\��
        lblKensuu.Text = CStr(lRowCnt) & "��"

        '�{�^���̎g�p�ېݒ�i�g�p�j
        Call initBotton(_updFlg)

        '�t�H�[�J�X�̐ݒ�
        Call setForcusCol(COLNO_JUYOUSAKIKBN, CInt(lRowCnt - 1))

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�o�^�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnTouroku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTouroku.Click
        Try
            '�ꗗ�̃f�[�^���̎擾
            _dgv = New UtilDataGridViewHandler(dgvHinsyuMst)
            Dim lMaxCnt As Long = _dgv.getMaxRow

            '�o�^�`�F�b�N
            Try
                Call checkTouroku(lMaxCnt)
            Catch lex As UsrDefException
                lex.dspMsg()
                Exit Sub
            End Try

            '�o�^�m�F���b�Z�[�W
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmInsert")    '�o�^���܂��B
            If rtn <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If

            '�}�E�X�J�[�\�������v
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor
            Try

                '�����J�n���Ԃƒ[��ID�̎擾
                Dim dStartSysdate As Date = Now()                           '�����J�n����
                Dim sPCName As String = UtilClass.getComputerName           '�[��ID

                '�g�����U�N�V�����J�n
                _db.beginTran()

                '�i��敪�}�X�^�̓o�^����
                Dim lCntIns As Long = 0
                Dim lCntDel As Long = 0
                Call torokuM02HinsyuKbnMst(lMaxCnt, dStartSysdate, sPCName, lCntIns, lCntDel)

                '�����I�������̎擾
                Dim dFinishSysdate As Date = Now()

                '���s�����e�[�u���A��������e�[�u���̍X�V����
                Call updRirekiAndSeigyo(lCntIns, lCntDel, sPCName, dStartSysdate, dFinishSysdate)

                '�g�����U�N�V�����I��
                _db.commitTran()

            Finally
                '�}�E�X�J�[�\�����ɖ߂�
                Me.Cursor = cur
            End Try

            '�������b�Z�[�W
            _msgHd.dspMSG("completeInsert")

            '��ʋN�����̔��ʗp�t���O�̏�����
            _formOpenFlg = True

            '�ꗗ�̍ĕ\��
            Call dispDGV()

            '��ʋN�����̔��ʗp�t���O�̐ݒ�
            _formOpenFlg = False

            '�ꗗ�ύX�t���O
            _dgvChangeFlg = False

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        Finally
            If _db.isTransactionOpen = True Then
                _db.rollbackTran()                          '���[���o�b�N
            End If
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�߂�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnModoru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModoru.Click

        '�x�����b�Z�[�W
        If _dgvChangeFlg Then
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

#Region "���[�U��`�֐�:DGV�֘A"

    '-------------------------------------------------------------------------------
    '   �V�[�g�R�}���h�{�^������
    '-------------------------------------------------------------------------------
    Private Sub dgvHinsyuMst_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvHinsyuMst.CellContentClick
        Try
            _dgv = New UtilDataGridViewHandler(dgvHinsyuMst)        'DGV�n���h���̐ݒ�

            If e.ColumnIndex <> COLNO_JUYOUSAKIBTN Then             '���v��I���{�^��
                Exit Sub
            End If

            '�ύX�����s���擾����
            _dgv = New UtilDataGridViewHandler(dgvHinsyuMst)
            Dim RowNo As Integer = dgvHinsyuMst.CurrentCell.RowIndex

            '-->2010/11/22 del start nakazawa  �i��敪�{�^���폜
            'Select Case e.ColumnIndex
            'Case COLNO_JUYOUSAKIBTN     '�@���v��{�^��
            '<--2010/11/22 del end nakazawa   �i��敪�{�^���폜

            '���v��R�[�h�̎擾
            Dim sJuyosaki As String = _dgv.getCellData(COLDT_JUYOUSAKI, RowNo)

            '-->2010/09/29 upd start nakazawa
            '�R�[�h�I����ʕ\��
            'Dim openForm As ZC910S_CodeSentaku = New ZC910S_CodeSentaku(_msgHd, _db, Me, KOTEIKEY_JUYOUCD)   '��ʑJ��
            Dim openForm As ZC910S_CodeSentaku = New ZC910S_CodeSentaku(_msgHd, _db, Me, KOTEIKEY_JUYOUCD, sJuyosaki)   '��ʑJ��
            '<--2010/09/29 upd end nakazawa
            openForm.ShowDialog(Me)                                 '��ʕ\��
            openForm.Dispose()

            '�I���������e�̕\��
            If Not "".Equals(_ZC910KahenKey) Then
                '�ꗗ�̓��e���ς�����ꍇ
                If Not _dgv.getCellData(COLDT_JUYOUSAKI, RowNo).Equals(_ZC910KahenKey) Then
                    _dgvChangeFlg = True        '�ꗗ�ύX�t���O
                End If
                _dgv.setCellData(COLDT_JUYOUSAKI, RowNo, _ZC910KahenKey)    '���v��
                _dgv.setCellData(COLDT_JUYOUSAKINM, RowNo, _ZC910Meisyo1)   '���v�於
            End If

            '-->2010/11/22 del start nakazawa  �i��敪�{�^���폜
            'Case COLNO_HINSYUKBNBTN     '�A�i��敪�{�^��
            ''-->2010/11/22 upd start nakazawa
            ''�i��敪�̎擾
            'Dim sHinsyuKbn As String = _dgv.getCellData(COLDT_HINSYUKBN, RowNo)

            ''�R�[�h�I����ʕ\��
            'Dim openForm As ZC910S_CodeSentaku = New ZC910S_CodeSentaku(_msgHd, _db, Me, _
            '        dgvHinsyuMst(COLNO_JUYOUSAKIKBN, e.RowIndex).Value, _
            '        dgvHinsyuMst(COLNO_HINSYUCD, e.RowIndex).Value, , True)   '��ʑJ��
            'openForm.ShowDialog(Me)                                 '��ʕ\��
            'openForm.Dispose()

            ''�I���������e�̕\��
            'If Not "".Equals(_ZC910KahenKey) Then
            '    _dgv.setCellData(COLDT_HINSYUKBN, RowNo, _ZC910KahenKey)    '�i��敪
            '    _dgv.setCellData(COLDT_HINSYUKBNNM, RowNo, _ZC910Meisyo1)   '�i��敪��
            'End If
            ''<--2010/11/01 upd end nakazawa

            'Case Else
            'Exit Sub
            'End Select
            '<--2010/11/22 del end nakazawa   �i��敪�{�^���폜

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '   �ꗗ�̃Z���l�ύX��
    '-------------------------------------------------------------------------------
    Private Sub dgvHinsyuMst_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvHinsyuMst.CellValueChanged
        Try
            '��ʋN�����͏������s��Ȃ�
            If _formOpenFlg = True Then
                Exit Sub
            End If

            '�ꗗ�ύX�t���O
            _dgvChangeFlg = True

            '�ύX�����s���擾����
            _dgv = New UtilDataGridViewHandler(dgvHinsyuMst)
            Dim RowNo As Integer = dgvHinsyuMst.CurrentCell.RowIndex

            '�Ώۍs�ɕύX�t���O�𗧂Ă�
            _dgv.setCellData(COLDT_HENKOUFLG, RowNo, HENKO_FLG)

            '�@�폜�`�F�b�N�̏ꍇ-----------------------------------------------------
            If e.ColumnIndex = COLNO_SAKUJOCHK Then
                If IIf((_dgv.getCellData(COLDT_SAKUJOCHK, RowNo) = ""), False, (_dgv.getCellData(COLDT_SAKUJOCHK, RowNo))) = True Then
                    '�폜�t���O�𗧂Ă�
                    _dgv.setCellData(COLDT_SAKUJOCHK, RowNo, True)
                Else
                    '�폜�t���O������
                    _dgv.setCellData(COLDT_SAKUJOCHK, RowNo, False)
                End If
            End If

            '�A���v��R�[�h��ύX�����ꍇ�i���v���\������j-------------------------
            If e.ColumnIndex = COLNO_JUYOUSAKIKBN Then
                '���v��R�[�h�̎擾
                Dim sJuyosaki As String = _dgv.getCellData(COLDT_JUYOUSAKI, RowNo)

                If Not "".Equals(sJuyosaki) Then
                    '���v�於�̎擾����
                    Call getJuyousakiName(sJuyosaki, RowNo)
                Else
                    '���v�於�̏������i�󗓂ɂ���j
                    _dgv.setCellData(COLDT_JUYOUSAKINM, RowNo, "")
                End If
            End If

            '�B�i��敪��ύX�����ꍇ�i�i��敪����\������j-------------------------
            If e.ColumnIndex = COLNO_HINSYUCD Then
                '�i��敪�̎擾
                Dim sHinsyuKbn As String = _dgv.getCellData(COLDT_HINSYUKBN, RowNo)

                If Not "".Equals(sHinsyuKbn) Then
                    '�i��敪���̎擾����
                    Call getHinsyuKbnName(sHinsyuKbn, RowNo)
                Else
                    '�i��敪���̏������i�󗓂ɂ���j
                    _dgv.setCellData(COLDT_HINSYUKBNNM, RowNo, "")
                End If
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�I���s�ɒ��F���鏈��
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvHinsyuMst_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvHinsyuMst.CellEnter

        If _colorCtlFlg Then
            '�w�i�F�̐ݒ�
            Call setBackcolor(dgvHinsyuMst.CurrentCellAddress.Y, _oldRowIndex)
        End If

        _oldRowIndex = dgvHinsyuMst.CurrentCellAddress.Y
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�w�i�F�̐ݒ菈��
    '------------------------------------------------------------------------------------------------------
    Private Sub setBackcolor(ByVal prmRowIndex As Integer, ByVal prmOldRowIndex As Integer)

        Dim _dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHinsyuMst)

        '�w�肵���s�̔w�i�F��ɂ���
        _dgv.setSelectionRowColor(prmRowIndex, prmOldRowIndex, StartUp.lCOLOR_BLUE)

        '�{�^����̐F���ς���Ă��܂��̂ŁA�߂�����
        Call colBtnColorSilver(prmRowIndex)

        _oldRowIndex = prmRowIndex

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�I���s�̃{�^���̐F�����ɖ߂�����
    '------------------------------------------------------------------------------------------------------
    Private Sub colBtnColorSilver(ByVal prmNewRowIdx As Integer)

        dgvHinsyuMst(COLNO_JUYOUSAKIBTN, prmNewRowIdx).Style.BackColor = SystemColors.Control   '���v��{�^��
        '-->2010/11/22 del start nakazawa
        'dgvHinsyuMst(COLNO_HINSYUKBNBTN, prmNewRowIdx).Style.BackColor = SystemColors.Control   '�i��敪�{�^��
        '<--2010/11/22 del end nakazawa

    End Sub

    '------------------------------------------------------------------------------------------------------
    '   �w���ւ̃t�H�[�J�X�ݒ菈��
    '------------------------------------------------------------------------------------------------------
    Private Sub setForcusCol(ByVal prmColIndex As Integer, ByVal prmRowIndex As Integer)

        '�t�H�[�J�X�����Ă�
        dgvHinsyuMst.Focus()
        dgvHinsyuMst.CurrentCell = dgvHinsyuMst(prmColIndex, prmRowIndex)

        '�w�i�F�̐ݒ�
        Call setBackcolor(prmRowIndex, _oldRowIndex)

    End Sub

#End Region

#Region "���[�U��`�֐�:��ʐ���"

    '------------------------------------------------------------------------------------------------------
    '�@�{�^���̎g�p�ېݒ�
    '------------------------------------------------------------------------------------------------------
    Private Sub initBotton(ByVal prmEnable As Boolean)

        '�o�^�{�^��
        btnTouroku.Enabled = prmEnable

    End Sub

    '-------------------------------------------------------------------------------
    ' �@�ėp�}�X�^�σL�[�̎󂯎��
    '-------------------------------------------------------------------------------
    Sub setKahenKey(ByVal prmKahenKey As String, ByVal prmMeisyo1 As String) Implements IfRturnKahenKey.setKahenKey
        Try

            _ZC910KahenKey = prmKahenKey
            _ZC910Meisyo1 = prmMeisyo1

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
    Public Sub myShow() Implements IfRturnKahenKey.myShow
        Me.Show()
    End Sub

    '-------------------------------------------------------------------------------
    '   myActivate���\�b�h
    '-------------------------------------------------------------------------------
    Public Sub myActivate() Implements IfRturnKahenKey.myActivate
        Me.Activate()
    End Sub

#End Region

#Region "���[�U��`�֐�:DB�֘A"

    '-------------------------------------------------------------------------------
    '�@�����J�n�C�x���g
    '-------------------------------------------------------------------------------
    Private Sub dispDGV()

        Try
            '�ꗗ�E�����N���A
            _colorCtlFlg = False
            _dgv = New UtilDataGridViewHandler(dgvHinsyuMst)
            _dgv.clearRow()
            dgvHinsyuMst.Enabled = False
            lblKensuu.Text = "0��"

            'M02�i��敪�}�X�^�̕\��
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & "   'False' " & COLDT_SAKUJOCHK
            sql = sql & N & " , M02.JUYOUCD " & COLDT_JUYOUSAKI
            sql = sql & N & " , M01.NAME1 " & COLDT_JUYOUSAKINM
            sql = sql & N & " , M02.HINSYUKBN " & COLDT_HINSYUKBN
            sql = sql & N & " , M02.HINSYUKBNNM " & COLDT_HINSYUKBNNM
            sql = sql & N & " , NULL " & COLDT_HENKOUFLG
            sql = sql & N & " , M02.JUYOUCD " & COLDT_HENKOMAEJUYOU
            sql = sql & N & " , M02.HINSYUKBN " & COLDT_HENKOMAEHINSYU
            sql = sql & N & " FROM M02HINSYUKBN M02"
            sql = sql & N & "   LEFT JOIN M01HANYO M01 ON M02.JUYOUCD = M01.KAHENKEY"
            sql = sql & N & " WHERE M01.KOTEIKEY = '" & KOTEIKEY_JUYOUCD & "'"
            sql = sql & N & " ORDER BY JUYOUCD, HINSYUKBN "

            'SQL���s
            Dim iRecCnt As Integer                  '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                '�{�^���̎g�p�ېݒ�i�g�p�s�j
                Call initBotton(False)
                '���b�Z�[�W�̕\��
                Throw New UsrDefException("�o�^����Ă��܂���B", _msgHd.getMSG("noData"))
            Else
                '�{�^���̎g�p�ېݒ�i�g�p�j
                Call initBotton(_updFlg)
            End If

            '���o�f�[�^���ꗗ�ɕ\������
            dgvHinsyuMst.DataSource = ds
            dgvHinsyuMst.DataMember = RS

            '������\������
            lblKensuu.Text = CStr(iRecCnt) & "��"

            _colorCtlFlg = True
            dgvHinsyuMst.Enabled = True

            '�ꗗ�擪�s�I��
            dgvHinsyuMst.Focus()
            '�w�i�F�̐ݒ�
            Call setBackcolor(0, 0)

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@���v�於�̎擾����
    '------------------------------------------------------------------------------------------------------
    Private Sub getJuyousakiName(ByVal prmJuyousaki As String, ByVal prmRowNo As Long)
        Try
            'M01�ėp�}�X�^����Y��������v�於���擾����
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & "  NAME1"
            sql = sql & N & " FROM M01HANYO"
            sql = sql & N & " WHERE KOTEIKEY = '" & KOTEIKEY_JUYOUCD & "'"
            sql = sql & N & "   AND KAHENKEY = '" & prmJuyousaki & "'"

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then
                '���v�於���󗓂ɂ���
                _dgv.setCellData(COLDT_JUYOUSAKINM, prmRowNo, "")
                '���b�Z�[�W�̕\��
                Throw New UsrDefException("�o�^����Ă��Ȃ����v��R�[�h�ł��B", _msgHd.getMSG("NoJuyousakiCD"), dgvHinsyuMst, COLCN_JUYOUSAKI, prmRowNo)
            Else
                '���v�於��\������
                _dgv.setCellData(COLDT_JUYOUSAKINM, prmRowNo, _db.rmNullStr(ds.Tables(RS).Rows(0)("NAME1")))
            End If

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�i��敪���̎擾����
    '------------------------------------------------------------------------------------------------------
    Private Sub getHinsyuKbnName(ByVal prmHinsyuKbn As String, ByVal prmRowNo As Long)
        Try

            ' ''M01�ėp�}�X�^����Y������i��敪�����擾����
            ''Dim sql As String = ""
            ''sql = "SELECT "
            ''sql = sql & N & "  NAME1"
            ''sql = sql & N & " FROM M01HANYO"
            ''sql = sql & N & " WHERE KOTEIKEY = '" & KOTEIKEY_JUYOUCD & "'"
            ''sql = sql & N & "   AND KAHENKEY = '" & prmHinsyuKbn & "'"

            ' ''SQL���s
            ''Dim iRecCnt As Integer			'�f�[�^�Z�b�g�̍s��
            ''Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            ''If iRecCnt <= 0 Then
            ''	'�i��敪�����󗓂ɂ���
            ''	_dgv.setCellData(COLDT_HINSYUKBNNM, prmRowNo, "")
            ''	'���b�Z�[�W�̕\��
            ''	Throw New UsrDefException("�o�^����Ă��Ȃ��i��敪�ł��B", _msgHd.getMSG("NoJuyousakiCD"))
            ''Else
            ''	'�i��敪����\������
            ''	_dgv.setCellData(COLDT_HINSYUKBNNM, prmRowNo, _db.rmNullStr(ds.Tables(RS).Rows(0)("NAME1")))
            ''End If

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�i��敪�}�X�^�̓o�^����
    '------------------------------------------------------------------------------------------------------
    Private Sub torokuM02HinsyuKbnMst(ByVal prmMaxCnt As Long, ByVal prmSysdate As Date, ByVal prmPCName As String, _
       ByRef rCntIns As Long, ByRef rCntDel As Long)
        Try
            Dim lCntHenkoFlg As Long        '�폜�������������i�X�V�o�^�̂��߂̍폜���܂ށj�̃J�E���g�p

            '�폜����
            lCntHenkoFlg = 0
            For i As Integer = 0 To prmMaxCnt - 1
                '�ύX�t���O�������Ă���f�[�^���폜����
                If HENKO_FLG.Equals(_dgv.getCellData(COLDT_HENKOUFLG, i).ToString) Then
                    'SQL�����s
                    Dim sql As String = ""
                    sql = "DELETE FROM M02HINSYUKBN"
                    sql = sql & N & " WHERE JUYOUCD = '" & _dgv.getCellData(COLDT_HENKOMAEJUYOU, i).ToString & "'"
                    sql = sql & N & "   AND HINSYUKBN = '" & _dgv.getCellData(COLDT_HENKOMAEHINSYU, i).ToString & "'"
                    _db.executeDB(sql)

                    '�폜�����̃J�E���g�A�b�v
                    lCntHenkoFlg = lCntHenkoFlg + 1
                End If
            Next

            '�o�^����
            For i As Integer = 0 To prmMaxCnt - 1
                '�ύX�t���O�������Ă���A���폜�`�F�b�N���Ȃ��f�[�^��o�^����
                If HENKO_FLG.Equals(_dgv.getCellData(COLDT_HENKOUFLG, i).ToString) _
                   And _dgv.getCellData(COLDT_SAKUJOCHK, i) = False Then
                    'SQL�����s
                    Dim sql As String = ""
                    sql = "INSERT INTO M02HINSYUKBN ("
                    sql = sql & N & "  JUYOUCD"                                                     '���v��R�[�h
                    sql = sql & N & ", HINSYUKBN"                                                   '�i��敪�R�[�h
                    sql = sql & N & ", HINSYUKBNNM"                                                 '�i��敪��
                    sql = sql & N & ", UPDNAME"                                                     '�[��ID
                    sql = sql & N & ", UPDDATE"                                                     '�X�V����
                    sql = sql & N & ") VALUES ("
                    sql = sql & N & "  '" & _dgv.getCellData(COLDT_JUYOUSAKI, i).ToString & "'"     '���v��R�[�h
                    sql = sql & N & ", '" & _dgv.getCellData(COLDT_HINSYUKBN, i).ToString & "'"     '�i��敪�R�[�h
                    sql = sql & N & ", '" & _dgv.getCellData(COLDT_HINSYUKBNNM, i).ToString & "'"   '�i��敪��
                    sql = sql & N & ", '" & prmPCName & "'"                                         '�[��ID
                    sql = sql & N & ", TO_DATE('" & prmSysdate & "', 'YYYY/MM/DD HH24:MI:SS') "     '�X�V����
                    sql = sql & N & " )"
                    _db.executeDB(sql)

                    '�o�^�����̃J�E���g�A�b�v
                    rCntIns = rCntIns + 1
                End If
            Next

            '�폜���������̎Z�o�i�폜�`�F�b�N�ō폜���������j
            rCntDel = lCntHenkoFlg - rCntIns

        Catch ex As Exception
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@���s�����e�[�u���A��������e�[�u���̍X�V����
    '�@�@�����̓p�����[�^�F prmCntIns       �o�^����
    '�@�@�@�@�@�@�@�@�@�@�F prmCntDel       �폜����
    '�@�@�@�@�@�@�@�@�@�@�F prmPCName       �[����
    '�@�@�@�@�@�@�@�@�@�@�F prmStartDate    �����J�n����
    '�@�@�@�@�@�@�@�@�@�@�F prmFinishDate   �����I������
    '�@�@���֐��߂�l�@�@�F�Ȃ�
    '------------------------------------------------------------------------------------------------------
    Private Sub updRirekiAndSeigyo(ByVal prmCntIns As Long, ByVal prmCntDel As Long, ByVal prmPCName As String, _
       ByVal prmStartDate As Date, ByVal prmFinishDate As Date)
        Try
            '�o�^����
            Dim sql As String = ""
            sql = "INSERT INTO T91RIREKI ("
            sql = sql & N & "  PGID"                                                        '�@�\ID
            sql = sql & N & ", SDATESTART"                                                  '�����J�n����
            sql = sql & N & ", SDATEEND"                                                    '�����I������
            sql = sql & N & ", KENNSU1"                                                     '�����P�i�폜�����j
            sql = sql & N & ", KENNSU2"                                                     '�����Q�i�o�^�����j
            sql = sql & N & ", UPDNAME"                                                     '�[��ID
            sql = sql & N & ", UPDDATE"                                                     '�X�V����
            sql = sql & N & ") VALUES ("
            sql = sql & N & "  '" & PGID & "'"                                              '�@�\ID
            sql = sql & N & ", TO_DATE('" & prmStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "   '�����J�n����
            sql = sql & N & ", TO_DATE('" & prmFinishDate & "', 'YYYY/MM/DD HH24:MI:SS') "  '�����I������
            sql = sql & N & ", " & prmCntDel                                                '�����P�i�폜�����j
            sql = sql & N & ", " & prmCntIns                                                '�����Q�i�o�^�����j
            sql = sql & N & ", '" & prmPCName & "'"                                         '�[��ID
            sql = sql & N & ", TO_DATE('" & prmFinishDate & "', 'YYYY/MM/DD HH24:MI:SS') "  '�X�V����
            sql = sql & N & " )"
            _db.executeDB(sql)

            'T02��������e�[�u���X�V
            _parentForm.updateSeigyoTbl(PGID, True, prmStartDate, prmFinishDate)

        Catch ex As Exception
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

#End Region

#Region "���[�U��`�֐�:�`�F�b�N����"

	'------------------------------------------------------------------------------------------------------
	'   �o�^�`�F�b�N
	'------------------------------------------------------------------------------------------------------
	Private Sub checkTouroku(ByRef prmMaxCnt As Long)
        Try

            Dim reccnt As Integer = 0

            For i As Integer = 0 To prmMaxCnt - 1

                '���v��R�[�h�K�{�`�F�b�N
                If "".Equals(_db.rmNullStr(_dgv.getCellData(COLDT_JUYOUSAKI, i)).ToString) Then
                    '�t�H�[�J�X�����Ă�
                    Call setForcusCol(COLNO_JUYOUSAKIKBN, i)
                    '�G���[���b�Z�[�W�̕\��
                    '-->2010.12.17 chg by takagi #13
                    'Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y���v��F" & i + 1 & "�s�ځz"))
                    Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput"))
                    '<--2010.12.17 chg by takagi #13
                End If

                '���v��R�[�h���݃`�F�b�N
                Call getJuyousakiName(_db.rmNullStr(_dgv.getCellData(COLDT_JUYOUSAKI, i)).ToString, i)

                '�i��敪�K�{�`�F�b�N
                If "".Equals(_db.rmNullStr(_dgv.getCellData(COLDT_HINSYUKBN, i)).ToString) Then
                    '�t�H�[�J�X�����Ă�
                    Call setForcusCol(COLNO_HINSYUCD, i)
                    '�G���[���b�Z�[�W�̕\��
                    '-->2010.12.17 chg by takagi #13
                    'Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y�i��敪�F" & i + 1 & "�s�ځz"))
                    Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput"))
                    '<--2010.12.17 chg by takagi #13
                End If

                '���v��R�[�h�ƕi��敪�̑g�ݍ��킹�d���`�F�b�N
                '�����v��܂��͕i��敪���ύX�O�ƕς���Ă���ꍇ�Ƀ`�F�b�N����B
                If Not _dgv.getCellData(COLDT_JUYOUSAKI, i).ToString.Equals(_dgv.getCellData(COLDT_HENKOMAEJUYOU, i)) _
                 Or Not _dgv.getCellData(COLDT_HINSYUKBN, i).ToString.Equals(_dgv.getCellData(COLDT_HENKOMAEHINSYU, i)) Then

                    '-->2010/11/22 del start nakazawa
                    'Dim sql As String = ""
                    'sql = sql & N & "SELECT "
                    'sql = sql & N & "   * "
                    'sql = sql & N & " FROM M02HINSYUKBN "
                    'sql = sql & N & " WHERE JUYOUCD = '" & _dgv.getCellData(COLDT_JUYOUSAKI, i).ToString & "'"
                    'sql = sql & N & "   AND HINSYUKBN = '" & _dgv.getCellData(COLDT_HINSYUKBN, i).ToString & "'"
                    '            ds = _db.selectDB(sql, RS, reccnt)

                    'If reccnt <> 0 Then
                    '    '�t�H�[�J�X�����Ă�
                    '    Call setForcusCol(COLNO_HINSYUCD, i)
                    '    '�G���[���b�Z�[�W�̕\��
                    '    Throw New UsrDefException("���͂��ꂽ�i��敪�͓o�^�ςł��B", _msgHd.getMSG("RepeatHinsyuCD", "�y" & i + 1 & "�s�ځz"))
                    'End If
                    '<--2010/11/22 del end nakazawa

                    '-->2010/11/22 add start nakazawa
                    For j As Integer = 0 To prmMaxCnt - 1
                        If Not j = i Then
                            If _dgv.getCellData(COLDT_HINSYUKBN, i).ToString.Equals(_dgv.getCellData(COLDT_HINSYUKBN, j).ToString) Then
                                If _dgv.getCellData(COLDT_JUYOUSAKI, i).ToString.Equals(_dgv.getCellData(COLDT_JUYOUSAKI, j).ToString) Then
                                    '�t�H�[�J�X�����Ă�
                                    Call setForcusCol(COLNO_HINSYUCD, i)
                                    '-->2010.12.17 chg by takagi #13
                                    'Throw New UsrDefException("�i��敪���d�����Ă܂��B", _
                                    '        _msgHd.getMSG("RepeatHinsyuCD", "�y" & i + 1 & "�s�ځz"))
                                    Throw New UsrDefException("�i��敪���d�����Ă܂��B", _
                                            _msgHd.getMSG("RepeatHinsyuCD"))
                                    '<--2010.12.17 chg by takagi #13
                                End If
                            End If
                        End If
                    Next
                    '<--2010/11/22 add end nakazawa
                End If

                ' ''�i��敪��
                ''If "".Equals(_dgv.getCellData(COLDT_HINSYUKBNNM, i).ToString) Then
                ''	'�t�H�[�J�X�����Ă�
                ''	Call setForcusCol(COLNO_HINSYUKBN, i)
                ''	'�G���[���b�Z�[�W�̕\��
                ''	Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y�i��敪���F" & i + 1 & "�s�ځz"))
                ''End If

            Next

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

	End Sub

#End Region

End Class

