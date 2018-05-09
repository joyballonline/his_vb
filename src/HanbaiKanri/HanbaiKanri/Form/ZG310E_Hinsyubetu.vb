'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���j�i��ʔ̔��v�����
'    �i�t�H�[��ID�jZG310E_Hinsyubetu
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ���V        2010/09/02                 �V�K              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Public Class ZG310E_Hinsyubetu
    Inherits System.Windows.Forms.Form

#Region "���e�����l��`"
    '-------------------------------------------------------------------------------
    '   �萔��`
    '-------------------------------------------------------------------------------

    'PG���䕶�� 
    Private Const N As String = ControlChars.NewLine            '���s����
    Private Const RS As String = "RecSet"                       '���R�[�h�Z�b�g�e�[�u��

    Private Const PGID As String = "ZG310E"                     'T91�ɓo�^����PGID

    '�ꗗ�f�[�^�o�C���h��
    Private Const COLDT_JUYOCD As String = "dtJuyousakiCD"
    Private Const COLDT_JUYONM As String = "dtJuyousaki"
    Private Const COLDT_HINKBN As String = "dtHinsyuKbn"
    Private Const COLDT_HINKBNNM As String = "dtHinsyuKbnNm"
    Private Const COLDT_THANBAI As String = "dtTougetu"
    Private Const COLDT_YHANBAI As String = "dtYokugetu"
    Private Const COLDT_YYHANBAI As String = "dtYyokugetu"
    Private Const COLDT_JUYOSORT As String = "dtJuyoSort"
    Private Const COLDT_UPDNM As String = "dtUpdNm"

    '�ꗗ�O���b�h��
    Private Const COLCN_JUYOCD As String = "cnJuyousakiCD"
    Private Const COLCN_JUYONM As String = "cnJuyousaki"
    Private Const COLCN_HINKBN As String = "cnHinsyuKbn"
    Private Const COLCN_HINKBNNM As String = "cnHinsyuKbnNm"
    Private Const COLCN_THANBAI As String = "cnTougetu"
    Private Const COLCN_YHANBAI As String = "cnYokugetu"
    Private Const COLCN_YYHANBAI As String = "cnYyokugetu"
    Private Const COLCN_JUYOSORT As String = "cnJuyoSort"
    Private Const COLCN_UPDNM As String = "cnUpdNm"

    '�ꗗ��ԍ�
    Private Const COLNO_JUYOCD As Integer = 0
    Private Const COLNO_JUYONM As Integer = 1
    Private Const COLNO_HINKBN As Integer = 2
    Private Const COLNO_HINKBNNM As Integer = 3
    Private Const COLNO_THANBAI As Integer = 4
    Private Const COLNO_YHANBAI As Integer = 5
    Private Const COLNO_YYHANBAI As Integer = 6

    '���x�����������בւ��p���e����
    Private Const LBL_JUYO As String = "���v��"
    Private Const LBL_HINSYU As String = "�i��敪"
    Private Const LBL_SYOJUN As String = "��"
    Private Const LBL_KOJUN As String = "��"

    'EXCEL
    Private Const START_PRINT As Integer = 7        'EXCEL�o�͊J�n�s��

    'EXCEL��ԍ�
    Private Const XLSCOL_JUYOCD As Integer = 1      '���v�R�[�h
    Private Const XLSCOL_JUYONM As Integer = 2      '���v��
    Private Const XLSCOL_HINKBN As Integer = 3      '�i��敪   
    Private Const XLSCOL_HINKBNNM As Integer = 4    '�i��敪��
    Private Const XLSCOL_THANBAI As Integer = 5     '�����̔��v��
    Private Const XLSCOL_YHANBAI As Integer = 6     '�����̔��v��
    Private Const XLSCOL_YYHANBAI As Integer = 7    '���X���̔��v��

#End Region

#Region "�����o�[�ϐ��錾"

    '-------------------------------------------------------------------------------
    '   �ϐ���`
    '-------------------------------------------------------------------------------

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu

    Private _oldRowIndex As Integer = -1                    '�I���s�̔w�i�F��ύX���邽�߂̕ϐ�
    Private _colorCtlFlg As Boolean = False                 '�I���s�̔w�i�F��ύX���邽�߂̃t���O

    Private _tanmatuID As String = ""                       '�[��ID

    Private _chkCellVO As UtilDgvChkCellVO                  '�ꗗ�̓��͐����p

    Private _errSet As UtilDataGridViewHandler.dgvErrSet    '�G���[�������Ƀt�H�[�J�X����Z���ʒu
    Private _nyuuryokuErrFlg As Boolean = False             '���̓G���[�L���t���O

    Private _updFlg As Boolean = False                      '�X�V��

    Private _formOpenFlg As Boolean = True                  '��ʋN�����t���O
    Private _dgvChangeFlg As Boolean = False                '�ꗗ�ύX�t���O

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
        _msgHd = prmRefMsgHd                                                'MSG�n���h���̐ݒ�
        _db = prmRefDbHd                                                    'DB�n���h���̐ݒ�
        _parentForm = prmForm                                               '�e�t�H�[��
        StartPosition = FormStartPosition.CenterScreen                      '��ʒ����\��
        _updFlg = prmUpdFlg                                                 '�X�V��

    End Sub
#End Region

#Region "Form�C�x���g"

    '-------------------------------------------------------------------------------
    '�@�����J�n�C�x���g
    '-------------------------------------------------------------------------------
    Private Sub ZG310E_Hinsyubetu_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            '��ʋN�����t���O�L��
            _formOpenFlg = True

            '��ʕ\��
            Call initForm()

            '��ʋN�����t���O����
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
    '�@�o�^�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnTouroku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTouroku.Click
        Try

            '�K�{���̓`�F�b�N
            Call checkTouroku()

            '�o�^�m�F���b�Z�[�W
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmInsert")    '�o�^���܂��B
            If rtn <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If

            '�}�E�X�J�[�\�������v
            Me.Cursor = Cursors.WaitCursor

            '�ꗗ�̃f�[�^�����[�N�e�[�u���ɍX�V
            Call updateWK01()

            '�g�����U�N�V�����J�n
            _db.beginTran()

            'T11�֓o�^
            Call registT11()

            '�g�����U�N�V�����I��
            _db.commitTran()

            '�}�E�X�J�[�\�����
            Me.Cursor = Cursors.Arrow

            '�������b�Z�[�W
            _msgHd.dspMSG("completeInsert")

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
            '�}�E�X�J�[�\�����
            Me.Cursor = Cursors.Arrow
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@EXCEL�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHinsyu)

            '�ꗗ�̌�����0���Ȃ�A�����𒆎~����
            If gh.getMaxRow < 0 Then
                Throw New UsrDefException("�Y���f�[�^������܂���B", _msgHd.getMSG("noTargetData"))
            End If

            '�}�E�X�J�[�\�������v
            Me.Cursor = Cursors.WaitCursor

            'EXCEL�o��
            Call printExcel()

            '�}�E�X�J�[�\�����ɖ߂�
            Me.Cursor = Cursors.Arrow

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        Finally
            '�}�E�X�J�[�\�����ɖ߂�
            Me.Cursor = Cursors.Arrow
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

#Region "���[�U��`�֐�:��ʐ���"

    '-------------------------------------------------------------------------------
    '�@��ʋN����
    '-------------------------------------------------------------------------------
    Private Sub initForm()
        Try
            '�[��ID�̎擾
            _tanmatuID = UtilClass.getComputerName

            '�ꗗ�\��
            Call dispdgv()

            '�ꗗ�s���F�t���O��L���ɂ���
            _colorCtlFlg = True

            '�����N���A�v��N���\��
            Call dispDate()

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@���בւ����x��������
    '�@(�����T�v)�ꗗ����ёւ���
    '-------------------------------------------------------------------------------
    Private Sub lbl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblJuyo.Click, _
                                                                                            lblHinsyu.Click
        Try

            '�ꗗ�s���F�t���O�𖳌��ɂ���
            _colorCtlFlg = False

            '�ꗗ�̃f�[�^�����[�N�e�[�u���ɍX�V
            Call updateWK01()

            '�ꗗ�w�b�_�[���x���ҏW
            If sender.Equals(lblJuyo) Then
                '���v��
                If (LBL_JUYO & N & LBL_SYOJUN).Equals(lblJuyo.Text) Then
                    '���ɏ����ŕ��בւ����Ă���ꍇ
                    dispWK01("JUYOSORT DESC")
                    lblJuyo.Text = LBL_JUYO & N & LBL_KOJUN
                Else
                    '���񉟉����܂��͊��ɍ~���ŕ��בւ����Ă���ꍇ
                    dispWK01("JUYOSORT")
                    lblJuyo.Text = LBL_JUYO & N & LBL_SYOJUN
                End If
                '�i��敪�̃��x�������ɖ߂�
                lblHinsyu.Text = LBL_HINSYU
            Else
                '�i��敪
                If (LBL_HINSYU & N & LBL_SYOJUN).Equals(lblHinsyu.Text) Then
                    '���ɏ����ŕ��בւ����Ă���ꍇ
                    dispWK01("HINSYUKBN DESC")
                    lblHinsyu.Text = LBL_HINSYU & N & LBL_KOJUN
                Else
                    '���񉟉����܂��͊��ɍ~���ŕ��בւ����Ă���ꍇ
                    dispWK01("HINSYUKBN")
                    lblHinsyu.Text = LBL_HINSYU & N & LBL_SYOJUN
                End If
                '���v��̃��x�������ɖ߂�
                lblJuyo.Text = LBL_JUYO
            End If

            '�w�i�F�̐ݒ�
            Call setBackcolor(0, 0)

            '�ꗗ�s���F�t���O��L���ɂ���
            _colorCtlFlg = True

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

#End Region

#Region "���[�U��`�֐�:EXCEL�֘A"

    '------------------------------------------------------------------------------------------------------
    '�@EXCEL�o�͏���
    '------------------------------------------------------------------------------------------------------
    Private Sub printExcel()
        Try

            '���`�t�@�C��
            Dim openFilePath As String = StartUp.iniValue.BaseXlsPath & "\" & StartUp.iniValue.ExcelZG310R1_Base
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
            Dim wkEditFile As String = StartUp.iniValue.OutXlsPath & "\" & StartUp.iniValue.ExcelZG310R1_Out     '�R�s�[��t�@�C��

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
            Try
                '�R�s�[��t�@�C���J��
                eh.open()
                Try
                    Dim startPrintRow As Integer = START_PRINT          '�o�͊J�n�s��
                    Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHinsyu)        'DGV�n���h���̐ݒ�
                    Dim rowCnt As Integer = gh.getMaxRow
                    Dim i As Integer
                    For i = 0 To rowCnt - 1

                        '���1�s�ǉ�
                        eh.copyRow(startPrintRow + i)
                        eh.insertPasteRow(startPrintRow + i)
                        '�ꗗ�f�[�^�o��
                        eh.setValue(_db.rmNullStr(dgvHinsyu(COLCN_JUYOCD, i).Value), startPrintRow + i, XLSCOL_JUYOCD)
                        eh.setValue(_db.rmNullStr(dgvHinsyu(COLCN_JUYONM, i).Value), startPrintRow + i, XLSCOL_JUYONM)
                        eh.setValue(_db.rmNullStr(dgvHinsyu(COLCN_HINKBN, i).Value), startPrintRow + i, XLSCOL_HINKBN)
                        eh.setValue(_db.rmNullStr(dgvHinsyu(COLCN_HINKBNNM, i).Value), startPrintRow + i, XLSCOL_HINKBNNM)
                        eh.setValue(_db.rmNullStr(dgvHinsyu(COLCN_THANBAI, i).Value), startPrintRow + i, XLSCOL_THANBAI)
                        eh.setValue(_db.rmNullStr(dgvHinsyu(COLCN_YHANBAI, i).Value), startPrintRow + i, XLSCOL_YHANBAI)
                        eh.setValue(_db.rmNullStr(dgvHinsyu(COLCN_YYHANBAI, i).Value), startPrintRow + i, XLSCOL_YYHANBAI)

                    Next

                    '�]���ȋ�s���폜
                    eh.deleteRow(startPrintRow + i)

                    '�쐬�����ҏW
                    Dim printDate As String = Now.ToString("yyyy/MM/dd HH:mm")
                    eh.setValue("�쐬���� �F " & printDate, 1, 7)   'G1

                    '�����N���A�v��N���ҏW
                    eh.setValue("�����N���F" & lblSyori.Text & "�@�@�v��N���F" & lblKeikaku.Text, 1, 4)    'D1

                    '�����ҏW
                    eh.setValue(rowCnt & "��", 3, 7)    'G3

                    '�w�b�_�[�̔N���ҏW
                    eh.setValue(lblTHanbai.Text, 6, 5)  'E6
                    eh.setValue(lblYHanbai.Text, 6, 6)  'F6
                    eh.setValue(lblYYHanbai.Text, 6, 7) 'G6

                    '����̃Z���Ƀt�H�[�J�X���Ă�
                    eh.selectCell(7, 1)     'G1

                    Clipboard.Clear()         '�N���b�v�{�[�h�̏�����

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

#End Region

#Region "���[�U��`�֐�:DGV�֘A"

    '-------------------------------------------------------------------------------
    '   �ꗗ�̃Z���l�ύX��
    '   �i�����T�v�j�ύX���������s�̕ύX�t���O�𗧂Ă�
    '-------------------------------------------------------------------------------
    Private Sub dgvHinsyu_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvHinsyu.CellValueChanged
        Try

            '��ʋN�����͏������s��Ȃ�
            If _formOpenFlg Then
                Exit Sub
            End If

            '�ꗗ�ύX�t���O
            _dgvChangeFlg = True

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
    Private Sub dgvHinsyu_EditingControlShowing(ByVal sender As Object, _
                                              ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs _
                                              ) Handles dgvHinsyu.EditingControlShowing

        Try
            Dim _dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHinsyu)        'DGV�n���h���̐ݒ�
            '�������̔��v��A�����̔��v��A���X���̔��v��̏ꍇ
            If dgvHinsyu.CurrentCell.ColumnIndex = COLNO_THANBAI Or _
                            dgvHinsyu.CurrentCell.ColumnIndex = COLNO_YHANBAI Or _
                            dgvHinsyu.CurrentCell.ColumnIndex = COLNO_YYHANBAI Then

                '���O���b�h�ɁA���l���̓��[�h�̐�����������
                _chkCellVO = _dgv.chkCell(sender, e, UtilDataGridViewHandler.chkType.Num)

            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '   �T�C�Y�ʈꗗ�@�I���Z�����؃C�x���g�iDataError�C�x���g�j
    '   �i�����T�v�j���l���͗��ɐ��l�ȊO�����͂��ꂽ�ꍇ�̃G���[����
    '-------------------------------------------------------------------------------
    Private Sub dgvHinsyu_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvHinsyu.DataError

        Try
            e.Cancel = False                                   '�ҏW���[�h�I��

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHinsyu)
            '�������̔��v��A�����̔��v��A���X���̔��v��̏ꍇ�A�O���b�h�ɂ͐��l���̓��[�h(0�`9)�̐����������Ă���̂ŁA�����̉���
            If dgvHinsyu.CurrentCell.ColumnIndex = COLNO_THANBAI Or _
                            dgvHinsyu.CurrentCell.ColumnIndex = COLNO_YHANBAI Or _
                            dgvHinsyu.CurrentCell.ColumnIndex = COLNO_YYHANBAI Then

                gh.AfterchkCell(_chkCellVO)
            End If

            '���̓G���[�t���O�𗧂Ă�
            _nyuuryokuErrFlg = True

            Dim colName As String = ""
            Select Case e.ColumnIndex
                Case COLNO_THANBAI
                    colName = COLDT_THANBAI
                Case COLNO_YHANBAI
                    colName = COLDT_YHANBAI
                Case COLNO_YYHANBAI
                    colName = COLDT_YYHANBAI
                Case Else
                    colName = COLDT_THANBAI
            End Select

            '�������͂��ꂽ��Z������ɂ���
            gh.setCellData(colName, e.RowIndex, System.DBNull.Value)

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
    Private Sub dgvHinsyu_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvHinsyu.SelectionChanged
        Try

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHinsyu)

            '���̓G���[���������ꍇ
            If _nyuuryokuErrFlg Then
                _nyuuryokuErrFlg = False
                gh.setCurrentCell(_errSet)
            End If

            If _colorCtlFlg Then
                '�w�i�F�̐ݒ�
                Call setBackcolor(dgvHinsyu.CurrentCellAddress.Y, _oldRowIndex)

            End If
            _oldRowIndex = dgvHinsyu.CurrentCellAddress.Y

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

        Dim _dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHinsyu)

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
        dgvHinsyu.Focus()
        dgvHinsyu.CurrentCell = dgvHinsyu(prmColIndex, prmRowIndex)

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
                lblSyori.Text = ""
                lblKeikaku.Text = ""
                lblTHanbai.Text = ""
                lblYHanbai.Text = ""
                lblYYHanbai.Text = ""
            Else
                Dim syoriDate As String = ds.Tables(RS).Rows(0)("SYORI")
                Dim keikakuDate As String = ds.Tables(RS).Rows(0)("KEIKAKU")

                '�uYYYY/MM�v�`���ŕ\��
                lblSyori.Text = syoriDate.Substring(0, 4) & "/" & syoriDate.Substring(4, 2)
                lblKeikaku.Text = keikakuDate.Substring(0, 4) & "/" & keikakuDate.Substring(4, 2)

                '�ꗗ�w�b�_�[�\��
                lblTHanbai.Text = syoriDate.Substring(0, 4) & "/" & syoriDate.Substring(4, 2)
                lblYHanbai.Text = keikakuDate.Substring(0, 4) & "/" & keikakuDate.Substring(4, 2)

                '���X���̓��t���쐬
                Dim yyhanbai As String = keikakuDate & "01"     '���t�ɕϊ����邽�߂ɓ���t������
                Dim yyDate As DateTime = Date.ParseExact(yyhanbai, "yyyyMMdd", Nothing)
                yyDate = yyDate.AddMonths(1)        '1��������

                '�uYYYY/MM�v�`���ŕ\��
                lblYYHanbai.Text = CStr(yyDate).Substring(0, 7)
            End If

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
            sql = " DELETE FROM ZG310E_W10 WHERE UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql)

            '�X�V�������擾
            Dim updateDate As Date = Now

            'M02�i��敪�}�X�^
            sql = ""
            sql = "INSERT INTO ZG310E_W10 ("
            sql = sql & N & " JUYOUCD "                     '���v��
            sql = sql & N & " ,HINSYUKBN "                  '�i��敪
            sql = sql & N & " ,HINSYUKBNNM "                '�i��敪��
            sql = sql & N & " ,UPDNAME "                    '�[��ID
            sql = sql & N & " ,UPDDATE) "                   '�����J�n�N����
            sql = sql & N & " SELECT "
            sql = sql & N & "   JUYOUCD "
            sql = sql & N & "   , HINSYUKBN "
            sql = sql & N & "   , HINSYUKBNNM "
            sql = sql & N & "   , '" & _tanmatuID & "'"
            sql = sql & N & "   , TO_DATE('" & updateDate & "', 'YYYY/MM/DD HH24:MI:SS')"
            sql = sql & N & " FROM  M02HINSYUKBN "
            _db.executeDB(sql)

            'M10�ėp�}�X�^
            sql = ""
            sql = sql & N & "UPDATE ZG310E_W10 "
            sql = sql & N & "SET (JUYOUNM, JUYOSORT) = ("
            sql = sql & N & " SELECT M.NAME1, M.SORT FROM M01HANYO M "
            sql = sql & N & " WHERE M.KAHENKEY = ZG310E_W10.JUYOUCD "
            sql = sql & N & " AND M.KOTEIKEY = '01')"
            sql = sql & N & "WHERE ZG310E_W10.JUYOUCD = ("
            sql = sql & N & " SELECT M.KAHENKEY FROM M01HANYO M"
            sql = sql & N & " WHERE M.KAHENKEY = ZG310E_W10.JUYOUCD"
            sql = sql & N & " AND M.KOTEIKEY = '01')"
            _db.executeDB(sql)

            'T11�i��ʔ̔��v��
            sql = ""
            sql = sql & N & "UPDATE ZG310E_W10 "
            sql = sql & N & "SET ("
            sql = sql & N & "THANBAIRYOU, "
            sql = sql & N & "YHANBAIRYOU, "
            sql = sql & N & "YYHANBAIRYOU) = ("
            sql = sql & N & " SELECT T.THANBAIRYOU,"
            sql = sql & N & " T.YHANBAIRYOU, "
            sql = sql & N & " T.YYHANBAIRYOU "
            sql = sql & N & " FROM T11HINSYUHANK T"
            sql = sql & N & " WHERE ZG310E_W10.JUYOUCD = T.JUYOUCD"
            sql = sql & N & "  AND ZG310E_W10.HINSYUKBN = T.HINSYUKBN)"
            sql = sql & N & "WHERE "
            sql = sql & N & "(JUYOUCD, "
            sql = sql & N & "HINSYUKBN) IN ("
            sql = sql & N & " SELECT T.JUYOUCD,"
            sql = sql & N & "  T.HINSYUKBN"
            sql = sql & N & " FROM T11HINSYUHANK T"
            sql = sql & N & " WHERE ZG310E_W10.JUYOUCD = T.JUYOUCD"
            sql = sql & N & " AND ZG310E_W10.HINSYUKBN = T.HINSYUKBN)"
            _db.executeDB(sql)

            '�g�����U�N�V�����I��
            _db.commitTran()

            '�ꗗ�\��
            Call dispWK01()

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
    '�@�@I�@�F�@prmSort     �\�[�g��(��ʋN�����͉����󂯎��Ȃ�)
    '-------------------------------------------------------------------------------
    Private Sub dispWK01(Optional ByVal prmSort As String = "")
        Try

            Dim sql As String = ""
            '���[�N�̃f�[�^���ꗗ�ɕ\��
            sql = sql & N & " SELECT "
            sql = sql & N & "  JUYOUCD " & COLDT_JUYOCD             '���v��
            sql = sql & N & " ,JUYOUNM " & COLDT_JUYONM             '���v�於
            sql = sql & N & " ,HINSYUKBN " & COLDT_HINKBN           '�i��敪
            sql = sql & N & " ,HINSYUKBNNM " & COLDT_HINKBNNM       '�i��敪��
            sql = sql & N & " ,THANBAIRYOU " & COLDT_THANBAI        '�����v��̔���
            sql = sql & N & " ,YHANBAIRYOU " & COLDT_YHANBAI        '�����̔����f��
            sql = sql & N & " ,YYHANBAIRYOU " & COLDT_YYHANBAI      '���X���̔����f��
            sql = sql & N & " ,JUYOSORT " & COLDT_JUYOSORT          '�X�VIOd
            sql = sql & N & " ,UPDNAME " & COLDT_UPDNM              '�X�V����
            sql = sql & N & " FROM ZG310E_W10 "
            sql = sql & N & "   WHERE UPDNAME = '" & _tanmatuID & "'"
            sql = sql & N & " ORDER BY "
            If "".Equals(prmSort) Then
                sql = sql & N & " JUYOSORT, HINSYUKBN"
            Else
                sql = sql & N & " " & prmSort
            End If

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                btnTouroku.Enabled = False
                Throw New UsrDefException("�Ώۃf�[�^������܂���B", _msgHd.getMSG("NonData"))
            Else                                    '���o�f�[�^������ꍇ�A�o�^�{�^���L��
                btnTouroku.Enabled = _updFlg
            End If

            '���o�f�[�^���ꗗ�ɕ\������
            dgvHinsyu.DataSource = ds
            dgvHinsyu.DataMember = RS

            '�ꗗ�̌�����\������
            lblKensu.Text = CStr(iRecCnt) & "��"

            '�w�i�F�̐ݒ�
            Call setBackcolor(0, 0)

            '�ꗗ�̍ŏ��̓��͉\�Z���փt�H�[�J�X����
            setForcusCol(COLNO_THANBAI, 0)

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
    Private Sub updateWK01()
        Try

            Dim sql As String = ""
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHinsyu)

            '�g�����U�N�V�����J�n
            _db.beginTran()

            '�s�����������[�v
            For i As Integer = 0 To gh.getMaxRow - 1
                sql = ""
                sql = sql & N & " UPDATE ZG310E_W10 SET "
                '-->2010.12.17 chg by takagi #5
                'sql = sql & N & " THANBAIRYOU = TO_NUMBER('" & _db.rmNullStr(dgvHinsyu(COLNO_THANBAI, i).Value) & "') "
                'sql = sql & N & " ,YHANBAIRYOU = TO_NUMBER('" & _db.rmNullStr(dgvHinsyu(COLNO_YHANBAI, i).Value) & "') "
                'sql = sql & N & " ,YYHANBAIRYOU = TO_NUMBER('" & _db.rmNullStr(dgvHinsyu(COLNO_YYHANBAI, i).Value) & "') "
                sql = sql & N & " THANBAIRYOU = TO_NUMBER('" & _db.rmNullDouble(dgvHinsyu(COLNO_THANBAI, i).Value) & "') "
                sql = sql & N & " ,YHANBAIRYOU = TO_NUMBER('" & _db.rmNullDouble(dgvHinsyu(COLNO_YHANBAI, i).Value) & "') "
                sql = sql & N & " ,YYHANBAIRYOU = TO_NUMBER('" & _db.rmNullDouble(dgvHinsyu(COLNO_YYHANBAI, i).Value) & "') "
                '<--2010.12.17 chg by takagi #5
                sql = sql & N & "   WHERE UPDNAME = '" & _tanmatuID & "'"
                sql = sql & N & "   AND JUYOUCD = '" & dgvHinsyu(COLNO_JUYOCD, i).Value & "'"
                sql = sql & N & "   AND HINSYUKBN = '" & dgvHinsyu(COLNO_HINKBN, i).Value & "'"
                _db.executeDB(sql)

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

    '-------------------------------------------------------------------------------
    '�@DB�X�V
    '�@(�����T�v)���[�N�e�[�u���̒l��T11�ɓo�^����
    '-------------------------------------------------------------------------------
    Private Sub registT11()
        Try

            'T11�폜
            Dim delCnt As Integer = 0           '�폜���R�[�h��
            Dim sql As String = ""
            sql = sql & N & " DELETE FROM T11HINSYUHANK "
            sql = sql & N & " WHERE UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql, delCnt)

            '�X�V�������擾
            Dim updStartDate As Date = Now

            '���[�N�e�[�u���̒l��T11�ɓo�^
            sql = ""
            sql = sql & N & " INSERT INTO T11HINSYUHANK ( "
            sql = sql & N & "       JUYOUCD "           '���v��
            sql = sql & N & "      ,HINSYUKBN "         '�i��敪
            sql = sql & N & "      ,THANBAIRYOU "       '�����̔��v��
            sql = sql & N & "      ,YHANBAIRYOU "       '�����̔��v��
            sql = sql & N & "      ,YYHANBAIRYOU "      '���X���̔��v��
            sql = sql & N & "      ,UPDNAME "           '�[��ID
            sql = sql & N & "      ,UPDDATE )"          '�X�V����
            sql = sql & N & " SELECT "
            sql = sql & N & "       JUYOUCD "           '���v��
            sql = sql & N & "      ,HINSYUKBN "         '�i��敪
            sql = sql & N & "      ,THANBAIRYOU "       '�����̔��v��
            sql = sql & N & "      ,YHANBAIRYOU "       '�����̔��v��
            sql = sql & N & "      ,YYHANBAIRYOU "      '���X���̔��v��
            sql = sql & N & "      ,UPDNAME "           '�[��ID
            sql = sql & N & "      ,TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS')"
            sql = sql & N & "   FROM ZG310E_W10 "
            sql = sql & N & "       WHERE UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql)


            '�X�V�I���������擾
            Dim updFinDate As Date = Now

            '�X�V�����̎擾
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHinsyu)        'DGV�n���h���̐ݒ�
            Dim updCnt As Integer = gh.getMaxRow

            '���������E�v������擾
            Dim syoriDate As String = lblSyori.Text.Substring(0, 4) & lblSyori.Text.Substring(5, 2)
            Dim keikakuDate As String = lblKeikaku.Text.Substring(0, 4) & lblKeikaku.Text.Substring(5, 2)

            'T91���s����o�^����
            sql = ""
            sql = "INSERT INTO T91RIREKI ("
            sql = sql & N & "  PGID"                                                        '�@�\ID
            sql = sql & N & ", SNENGETU"                                                    '��������
            sql = sql & N & ", KNENGETU"                                                    '�v�����
            sql = sql & N & ", SDATESTART"                                                  '�����J�n����
            sql = sql & N & ", SDATEEND"                                                    '�����I������
            sql = sql & N & ", KENNSU1"                                                     '�����P�i�폜�����j
            sql = sql & N & ", KENNSU2"                                                     '�����Q�i�o�^�����j
            sql = sql & N & ", UPDNAME"                                                     '�[��ID
            sql = sql & N & ", UPDDATE"                                                     '�X�V����
            sql = sql & N & ") VALUES ("
            sql = sql & N & "  '" & PGID & "'"                                              '�@�\ID
            sql = sql & N & ", '" & syoriDate & "'"
            sql = sql & N & ", '" & keikakuDate & "'"
            sql = sql & N & ", TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "   '�����J�n����
            sql = sql & N & ", TO_DATE('" & updFinDate & "', 'YYYY/MM/DD HH24:MI:SS') "     '�����I������
            sql = sql & N & ", " & delCnt                                                   '�����P�i�폜�����j
            sql = sql & N & ", " & updCnt                                                   '�����Q�i�o�^�����j
            sql = sql & N & ", '" & _tanmatuID & "'"                                        '�[��ID
            sql = sql & N & ", TO_DATE('" & updFinDate & "', 'YYYY/MM/DD HH24:MI:SS') "     '�X�V����
            sql = sql & N & " )"
            _db.executeDB(sql)

            'T02��������e�[�u���X�V
            _parentForm.updateSeigyoTbl(PGID, True, updStartDate, updFinDate)

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

#End Region

#Region "���[�U��`�֐�:�`�F�b�N����"

    '------------------------------------------------------------------------------------------------------
    '  �o�^�`�F�b�N
    '�@(�����T�v)�e���ڂ̕K�{�E���͌����`�F�b�N���s��
    '------------------------------------------------------------------------------------------------------
    Private Sub checkTouroku()
        Try

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHinsyu)

            For i As Integer = 0 To gh.getMaxRow - 1

                '-->2010.12.17 add by takagi #5
                If (Not "".Equals(gh.getCellData(COLDT_THANBAI, i))) OrElse _
                   (Not "".Equals(gh.getCellData(COLDT_YHANBAI, i))) OrElse _
                   (Not "".Equals(gh.getCellData(COLDT_YYHANBAI, i))) Then
                    '<--2010.12.17 add by takagi #5

                    '�K�{���̓`�F�b�N
                    '�H���R�[�h
                    Call chekuHissuKeta(COLDT_THANBAI, "�����̔��v��", i, COLNO_THANBAI)
                    '�@�B��
                    Call chekuHissuKeta(COLDT_YHANBAI, "�����̔��v��", i, COLNO_YHANBAI)
                    '�ʏ�ғ�����
                    Call chekuHissuKeta(COLDT_YYHANBAI, "���X���̔��v��", i, COLNO_YYHANBAI)

                    '-->2010.12.17 add by takagi #4
                End If
                '<--2010.12.17 add by takagi #4
                
            Next

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '  �K�{�E���͌����`�F�b�N
    '�@(�����T�v)�Z�������͂���Ă��邩�E��������4���܂ł��`�F�N����
    '�@�@I�@�F�@prmColName              �`�F�b�N����Z���̗�
    '�@�@I�@�F�@prmColHeaderName        �G���[���ɕ\�������
    '�@�@I�@�F�@prmCnt                  �`�F�b�N����Z���̍s��
    '�@�@I�@�F�@prmColNo                �`�F�b�N����Z���̗�
    '------------------------------------------------------------------------------------------------------
    Private Sub chekuHissuKeta(ByVal prmColName As String, ByVal prmColHeaderName As String, ByVal prmCnt As Integer, ByVal prmColNo As Integer)
        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHinsyu)

            If "".Equals(gh.getCellData(prmColName, prmCnt).ToString) Then
                '�t�H�[�J�X�����Ă�
                Call setForcusCol(prmColNo, prmCnt)
                '�G���[���b�Z�[�W�̕\��
                '-->2010.12.17 chg by takagi #13
                'Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y '" & prmColHeaderName & "' �F" & prmCnt + 1 & "�s�ځz"))
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput"))
                '<--2010.12.17 chg by takagi #13
            End If

            '�����`�F�b�N�`�F�b�N
            If CInt(gh.getCellData(prmColName, prmCnt).ToString) >= 10000 Then
                '�t�H�[�J�X�����Ă�
                Call setForcusCol(prmColNo, prmCnt)
                '�G���[���b�Z�[�W�̕\��
                '-->2010.12.17 chg by takagi #13
                'Throw New UsrDefException("�������͂S���ȓ��œ��͂��ĉ������B", _msgHd.getMSG("over4Keta", "�y '" & prmColHeaderName & "' �F" & prmCnt + 1 & "�s�ځz"))
                Throw New UsrDefException("�������͂S���ȓ��œ��͂��ĉ������B", _msgHd.getMSG("over4Keta"))
                '<--2010.12.17 chg by takagi #13
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