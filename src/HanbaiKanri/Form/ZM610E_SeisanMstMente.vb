'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���j���Y�\�̓}�X�^�����e���
'    �i�t�H�[��ID�jZM610E_SeisanMstMente
'
'===============================================================================
'�@����     ���O        ���@�t          �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)      ���V        2010/09/30                  �V�K
'�@(2)      ���V        2010/11/22                  �L�[�ύX�ɂ����̓`�F�b�N�̕ύX�Ή�
'�@(3)      ����        2011/01/13                  (2)�Ή����ɔ��������s��̏C��
'-------------------------------------------------------------------------------
Option Explicit On
Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory

Public Class ZM610E_SeisanMstMente
    Inherits System.Windows.Forms.Form
    Implements IfRturnKahenKey

#Region "���e�����l��`"

    'PG���䕶�� 
    Private Const N As String = ControlChars.NewLine            '���s����
    Private Const RS As String = "RecSet"                       '���R�[�h�Z�b�g�e�[�u��

    '�ꗗ�o�C���hDetaSet��
    Private Const COLDT_SAKUJOCHK As String = "dtSakujoChk"     '�폜�`�F�b�N�{�b�N�X
    Private Const COLDT_KOUTEI As String = "dtKoutei"           '�H�����R�[�h
    Private Const COLDT_KOUTEIBTN As String = "dtKouteiBtn"     '�H���{�^��
    Private Const COLDT_KIKAINAME As String = "dtKikaiName"     '�@�B���L��
    Private Const COLDT_TJIKAN As String = "dtTjikan"           '�ʏ�ғ�����
    Private Const COLDT_DJIKAN As String = "dtDjikan"           '�y�j�ғ�����
    Private Const COLDT_NJIKAN As String = "dtNjikan"           '���j�ғ�����
    Private Const COLDT_FLG As String = "dtFlg"                 '�ύX�t���O
    Private Const COLDT_BKOUTEI As String = "dtBKoutei"         '�ύX�O�H��
    Private Const COLDT_BKIKAINAME As String = "dtBKikaiName"   '�ύX�O�@�B��

    '�ꗗ��
    Private Const COLCN_SAKUJOCHK As String = "cnSakujoChk"     '�폜�`�F�b�N�{�b�N�X
    Private Const COLCN_KOUTEI As String = "cnKoutei"           '�H�����R�[�h
    Private Const COLCN_KOUTEIBTN As String = "cnKouteiBtn"     '�H���{�^��
    Private Const COLCN_KIKAINAME As String = "cnKikaiName"     '�@�B���L��
    Private Const COLCN_TJIKAN As String = "cnTjikan"           '�ʏ�ғ�����
    Private Const COLCN_DJIKAN As String = "cnDjikan"           '�y�j�ғ�����
    Private Const COLCN_NJIKAN As String = "cnNjikan"           '���j�ғ�����
    Private Const COLCN_FLG As String = "cnFlg"                 '�ύX�t���O
    Private Const COLCN_BKOUTEI As String = "cnBKoutei"         '�ύX�O�H��
    Private Const COLCN_BKIKAINAME As String = "cnBKikaiName"   '�ύX�O�@�B��

    '�O���b�h��ԍ�
    Private Const COLNO_SAKUJOCHK As Integer = 0                '�폜�`�F�b�N�{�b�N�X
    Private Const COLNO_KOUTEI As Integer = 1                   '�H�����R�[�h
    Private Const COLNO_KOUTEIBTN As Integer = 2                '�H���{�^��
    Private Const COLNO_KIKAINAME As Integer = 3                '�@�B���L��
    Private Const COLNO_TJIKAN As Integer = 4                   '�ʏ�ғ�����
    Private Const COLNO_DJIKAN As Integer = 5                   '�y�j�ғ�����
    Private Const COLNO_NJIKAN As Integer = 6                   '���j�ғ�����

    'M01�ėp�}�X�^�Œ跰
    Private Const KOTEIKEY_KOUTEI As String = "12"              '�H��

    '�폜�t���O
    Private Const SAKUJO_FLG As String = "1"

    '�ύX�t���O
    Private Const HENKO_FLG As String = "1"

    '�v���O����ID�iT91���s�����e�[�u���o�^�p�j
    Private Const PGID As String = "ZM610E"

    'EXCEL�g���q
    Private Const EXT_XLS As String = ".xls"

#End Region

#Region "�����o�ϐ���`"

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu

    Dim _dgv As UtilDataGridViewHandler         '�O���b�h�n���h���[

    Private _oldRowIndex As Integer = -1            '�I���s�̔w�i�F��ύX���邽�߂̕ϐ�
    Private _colorCtlFlg As Boolean = False         '�I���s�̔w�i�F��ύX���邽�߂̃t���O

    Private _formOpenFlg As Boolean                 '��ʋN�����ł��邩�𔻕ʂ��邽�߂̃t���O

    Private _errSet As UtilDataGridViewHandler.dgvErrSet
    Private _nyuuryokuErrFlg As Boolean = False

    Private _ZC910KahenKey As String                'ZC910S_CodeSentaku����󂯎��ėp�}�X�^�σL�[
    Private _ZC910Meisyo1 As String                 'ZC910S_CodeSentaku����󂯎��ėp�}�X�^����

    Private _chkCellVO As UtilDgvChkCellVO          '�ꗗ�̓��͐����p

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
    Private Sub ZM610E_SeisanMstMente_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
            _formOpenFlg = True

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
        Try

            Dim dt As DataTable = CType(dgvSeisanMst.DataSource, DataSet).Tables(RS)
            Dim newRow As DataRow = dt.NewRow

            '����DataTable�̍ŏI�s��VO��}��
            dt.Rows.InsertAt(newRow, dt.Rows.Count)

            _dgv = New UtilDataGridViewHandler(dgvSeisanMst)                       'DGV�n���h���̐ݒ�
            '�ǉ������s�̕ύX�t���O�𗧂Ă�B
            '���ύX�t���O�������Ă���f�[�^���o�^�ΏۂɂȂ邽��
            Dim lRowCnt As Long = dt.Rows.Count
            _dgv.setCellData(COLDT_FLG, lRowCnt - 1, HENKO_FLG)

            '�ǉ������s�̍폜�`�F�b�N������������iFalse�ɐݒ肷��j
            _dgv.setCellData(COLDT_SAKUJOCHK, lRowCnt - 1, False)

            '�����̕\��
            lblKensuu.Text = CStr(lRowCnt) & "��"

            '�{�^���̎g�p�ېݒ�i�g�p�j
            Call initBotton(True)

            '�t�H�[�J�X�̐ݒ�
            Call setForcusCol(COLNO_KOUTEI, CInt(lRowCnt - 1))

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

            '�ꗗ�̃f�[�^���̎擾
            _dgv = New UtilDataGridViewHandler(dgvSeisanMst)
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

                Call registDB()

                '�g�����U�N�V�����I��
                _db.commitTran()

            Finally
                '�}�E�X�J�[�\�����
                Me.Cursor = cur
            End Try

            '�������b�Z�[�W
            _msgHd.dspMSG("completeInsert")

            _colorCtlFlg = False

            '��ʍĕ\�����̔��ʗp�t���O�̏�����
            _formOpenFlg = True

            '�ꗗ�̍ĕ\��
            Call dispDGV()

            '��ʍĕ\�����̔��ʗp�t���O�̐ݒ�
            _formOpenFlg = False

            _dgvChangeFlg = False                '�ꗗ�ύX�t���O

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
    '�@EXCEL�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Try
            _dgv = New UtilDataGridViewHandler(dgvSeisanMst)

            '�ꗗ�̌�����0���Ȃ�A�����𒆎~����
            If _dgv.getMaxRow < 0 Then
                Throw New UsrDefException("�Y���f�[�^������܂���B", _msgHd.getMSG("noTargetData"))
            End If

            '�}�E�X�J�[�\�������v
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor
            Try

                Call printExcel()

            Finally
                '�}�E�X�J�[�\�����ɖ߂�
                Me.Cursor = cur
            End Try

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

    '------------------------------------------------------------------------------------------------------
    '�@�{�^���̎g�p�ېݒ�
    '------------------------------------------------------------------------------------------------------
    Private Sub initBotton(ByVal prmEnable As Boolean)

        '�o�^�{�^��
        btnTouroku.Enabled = prmEnable

    End Sub
    '-------------------------------------------------------------------------------
    ' �@�ėp�}�X�^�σL�[�̎󂯎��
    '   (�����T�v)�q��ʂőI�����ꂽ�σL�[�Ɩ��̂��󂯎��
    '�@�@I�@�F�@prmKahenKey     �σL�[
    '�@�@I�@�F�@prmMeisyo1      ����
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

#Region "���[�U��`�֐�:EXCEL�֘A"

    '------------------------------------------------------------------------------------------------------
    '�@EXCEL�o�͏���
    '------------------------------------------------------------------------------------------------------
    Private Sub printExcel()
        Try
            Dim openFilePath As String = StartUp.iniValue.BaseXlsPath & "\" & StartUp.iniValue.ExcelZM610R1_Base

            '���`�t�@�C�����J����Ă��Ȃ����`�F�b�N
            Dim fh As UtilFile = New UtilFile()
            Try
                fh.move(openFilePath, openFilePath & 1)
                fh.move(openFilePath & 1, openFilePath)
            Catch ioe As System.IO.IOException
                Throw New UsrDefException("�t�@�C�����J����Ă��܂��B�t�@�C������Ă���Ď��s���Ă��������B", _
                                          _msgHd.getMSG("fileOpenErr", "�y�t�@�C�����z�F" & openFilePath))
            End Try

            '�ҏW�p�t�@�C���փR�s�[
            '�t�@�C�����擾-----------------------------------------------------
            Dim wkEditFile As String = StartUp.iniValue.OutXlsPath & "\" & StartUp.iniValue.ExcelZM610R1_Out     '�R�s�[��t�@�C��

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
                
                '���^�t�@�C���R�s�[
                FileCopy(openFilePath, wkEditFile)

            Catch ioe As System.IO.IOException
                Throw New UsrDefException(ioe, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ioe)))
            End Try

            Dim eh As xls.UtilExcelHandler = New xls.UtilExcelHandler(wkEditFile)
           
            Try
                '�R�s�[��t�@�C���J��
                eh.open()
                Try
                    Dim startPrintRow As Integer = 5                    '�o�͊J�n�s��
                    _dgv = New UtilDataGridViewHandler(dgvSeisanMst)
                    Dim rowCnt As Integer = _dgv.getMaxRow
                    Dim i As Integer
                    For i = 0 To rowCnt - 1

                        '���1�s�ǉ�
                        eh.copyRow(startPrintRow + i)
                        eh.insertPasteRow(startPrintRow + i)
                        '�ꗗ�f�[�^�o��
                        eh.setValue(_db.rmNullStr(dgvSeisanMst(COLCN_KOUTEI, i).Value), startPrintRow + i, 1)
                        eh.setValue(_db.rmNullStr(dgvSeisanMst(COLCN_KIKAINAME, i).Value), startPrintRow + i, 2)
                        eh.setValue(_db.rmNullStr(dgvSeisanMst(COLCN_TJIKAN, i).Value), startPrintRow + i, 3)
                        eh.setValue(_db.rmNullStr(dgvSeisanMst(COLCN_DJIKAN, i).Value), startPrintRow + i, 4)
                        eh.setValue(_db.rmNullStr(dgvSeisanMst(COLCN_NJIKAN, i).Value), startPrintRow + i, 5)

                    Next

                    '�]���ȋ�s���폜
                    eh.deleteRow(startPrintRow + i)

                    '�쐬�����ҏW
                    Dim printDate As String = Now.ToString("yyyy/MM/dd HH:mm")
                    eh.setValue("�쐬���� �F " & printDate, 1, 5)
                    eh.selectCell(1, 1)

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
    '   �ꗗ�@�ҏW�`�F�b�N�iEditingControlShowing�C�x���g�j
    '   �i�����T�v�j���͂̐�����������
    '-------------------------------------------------------------------------------
    Private Sub dgvkousin_EditingControlShowing(ByVal sender As Object, _
                                              ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs _
                                              ) Handles dgvSeisanMst.EditingControlShowing

        Try
            _dgv = New UtilDataGridViewHandler(dgvSeisanMst)                       'DGV�n���h���̐ݒ�
            '���ʏ�ғ����ԁA�y�j�ғ����ԁA���j�ғ����Ԃ̏ꍇ
            If dgvSeisanMst.CurrentCell.ColumnIndex = COLNO_TJIKAN Or _
                            dgvSeisanMst.CurrentCell.ColumnIndex = COLNO_DJIKAN Or _
                            dgvSeisanMst.CurrentCell.ColumnIndex = COLNO_NJIKAN Then

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
    Private Sub dgvSeisanMst_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvSeisanMst.DataError

        Try
            e.Cancel = False                                   '�ҏW���[�h�I��

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanMst)
            '���ʏ�ғ����ԁA�y�j�ғ����ԁA���j�ғ����Ԃ̏ꍇ�A�O���b�h�ɂ͐��l���̓��[�h(0�`9)�̐����������Ă���̂ŁA�����̉���
            If dgvSeisanMst.CurrentCell.ColumnIndex = COLNO_TJIKAN Or _
                            dgvSeisanMst.CurrentCell.ColumnIndex = COLNO_DJIKAN Or _
                            dgvSeisanMst.CurrentCell.ColumnIndex = COLNO_NJIKAN Then

                gh.AfterchkCell(_chkCellVO)
            End If

            '���̓G���[�t���O�𗧂Ă�
            _nyuuryokuErrFlg = True

            Dim colName As String = ""
            Select Case e.ColumnIndex
                Case COLNO_TJIKAN
                    colName = COLDT_TJIKAN
                Case COLNO_DJIKAN
                    colName = COLDT_DJIKAN
                Case COLNO_NJIKAN
                    colName = COLDT_NJIKAN
                Case Else
                    colName = COLDT_TJIKAN
            End Select

            '�������͂��ꂽ��Z������ɂ���
            gh.setCellData(colname, e.RowIndex, System.DBNull.Value)

            '�G���[���b�Z�[�W�\��
            Throw New UsrDefException("���p�����̂ݓ��͉\�ł��B", _msgHd.getMSG("onlyAcceptNumeric"))

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   �V�[�g�R�}���h�{�^������
    '   (�����T�v)�ꗗ�̃{�^�����������ꂽ�ꍇ�A�q��ʂ𗧂��グ��
    '-------------------------------------------------------------------------------
    Private Sub dgvHinsyuMst_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSeisanMst.CellContentClick
        Try

            If e.ColumnIndex <> COLNO_KOUTEIBTN Then  '�I���{�^��
                Exit Sub
            End If

            _dgv = New UtilDataGridViewHandler(dgvSeisanMst)                       'DGV�n���h���̐ݒ�
            Dim kahenKey As String = _dgv.getCellData(COLDT_KOUTEI, e.RowIndex)

            '�R�[�h�I����ʕ\��
            Dim openForm As ZC910S_CodeSentaku = New ZC910S_CodeSentaku(_msgHd, _db, Me, KOTEIKEY_KOUTEI, kahenKey)    '��ʑJ��
            openForm.ShowDialog(Me)                                                         '��ʕ\��
            openForm.Dispose()

            '���݂̒l�̕ێ�
            Dim beforeKoutei As String = _dgv.getCellData(COLDT_KOUTEI, e.RowIndex)

            '�I���������e�̕\��
            If Not "".Equals(_ZC910KahenKey) Then
                _dgv.setCellData(COLDT_KOUTEI, e.RowIndex, _ZC910KahenKey)      '�H�����R�[�h
                If Not beforeKoutei.Equals(_ZC910KahenKey) Then
                    '�Ώۍs�ɕύX�t���O�𗧂Ă�
                    _dgv.setCellData(COLDT_FLG, e.RowIndex, HENKO_FLG)
                End If
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   �ꗗ�̃Z���l�ύX��
    '-------------------------------------------------------------------------------
    Private Sub dgvSeisanMst_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSeisanMst.CellEndEdit
        Try
            '��ʋN�����͏������s��Ȃ�
            If _formOpenFlg = True Then
                _formOpenFlg = False
                Exit Sub
            End If


            '�ꗗ�ύX�t���O
            _dgvChangeFlg = True

            _dgv = New UtilDataGridViewHandler(dgvSeisanMst)                       'DGV�n���h���̐ݒ�

            '���l���̓��[�h(0�`9)�̐������������Ă���ꍇ�́A�����̉���
            If dgvSeisanMst.CurrentCell.ColumnIndex = COLNO_TJIKAN Or _
                            dgvSeisanMst.CurrentCell.ColumnIndex = COLNO_DJIKAN Or _
                            dgvSeisanMst.CurrentCell.ColumnIndex = COLNO_NJIKAN Then

                _dgv.AfterchkCell(_chkCellVO)
            End If

            '�ύX�����s���擾����
            Dim RowNo As Integer = dgvSeisanMst.CurrentCell.RowIndex

            '�Ώۍs�ɕύX�t���O�𗧂Ă�
            _dgv.setCellData(COLDT_FLG, RowNo, HENKO_FLG)

            '�{�^���̎g�p�ېݒ�i�g�p�j
            Call initBotton(True)

            '�폜�`�F�b�N
            If e.ColumnIndex = COLNO_SAKUJOCHK Then
                If IIf(("".Equals(_dgv.getCellData(COLDT_SAKUJOCHK, RowNo))), False, (_dgv.getCellData(COLDT_SAKUJOCHK, RowNo))) = True Then
                    '�폜�t���O�𗧂Ă�
                    _dgv.setCellData(COLDT_SAKUJOCHK, RowNo, True)
                Else
                    '�폜�t���O������
                    _dgv.setCellData(COLDT_SAKUJOCHK, RowNo, False)
                End If
            End If

            '�H�����R�[�h�̑��݃`�F�b�N
            If e.ColumnIndex = COLNO_KOUTEI Then

                If Not checkKoutei(_db.rmNullStr(dgvSeisanMst.CurrentCell.Value)) > 0 Then
                    dgvSeisanMst.CurrentCell.Value = _dgv.getCellData(COLDT_BKOUTEI, e.RowIndex)
                    '�G���[���N�����Z���̈ʒu����n��
                    _nyuuryokuErrFlg = True
                    _errSet = _dgv.readyForErrSet(e.RowIndex, COLCN_KOUTEI)
                    Throw New UsrDefException("�o�^����Ă��Ȃ��H�����R�[�h�ł��B", _msgHd.getMSG("NonKoutei"))
                End If
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�@�O���b�h�t�H�[�J�X�ݒ�y
    '�@�@(�����T�v�j�Z���ҏW��ɃG���[�ɂȂ����ꍇ�ɁA�G���[�Z���Ƀt�H�[�J�X��߂��B
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvHinsyuMst_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvSeisanMst.SelectionChanged
        Try

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanMst)

            '���̓G���[���������ꍇ
            If _nyuuryokuErrFlg Then
                _nyuuryokuErrFlg = False
                '�t�H�[�J�X����̓G���[�Z���Ɉڂ�
                gh.setCurrentCell(_errSet)
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
    Private Sub dgvSeisanMst_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSeisanMst.CellEnter
        Try

            If _colorCtlFlg Then
                Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanMst)
                '�w�i�F�̐ݒ�
                Call setBackcolor(dgvSeisanMst.CurrentCellAddress.Y, _oldRowIndex)

            End If
            _oldRowIndex = dgvSeisanMst.CurrentCellAddress.Y

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

        Dim _dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanMst)

        '�w�肵���s�̔w�i�F��ɂ���
        _dgv.setSelectionRowColor(prmRowIndex, prmOldRowIndex, StartUp.lCOLOR_BLUE)

        '�{�^����̐F���ς���Ă��܂��̂ŁA�߂�����
        Call colBtnColorSilver(prmRowIndex)

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
        dgvSeisanMst.Focus()
        dgvSeisanMst.CurrentCell = dgvSeisanMst(prmColIndex, prmRowIndex)

        '�w�i�F�̐ݒ�
        Call setBackcolor(prmRowIndex, _oldRowIndex)

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�I���s�̃{�^���̐F�����ɖ߂�����
    '�@(�����T�v)�{�^����̔w�i�F�����ɖ߂��B
    '�@�@I�@�F�@prmNewRowIndex      ���݂̍s��
    '------------------------------------------------------------------------------------------------------
    Private Sub colBtnColorSilver(ByVal prmNewRowIdx As Integer)

        dgvSeisanMst(COLNO_KOUTEIBTN, prmNewRowIdx).Style.BackColor = SystemColors.Control

    End Sub

#End Region

#Region "���[�U��`�֐�:DB�֘A"

    '-------------------------------------------------------------------------------
    '�@�ꗗ�\��
    '-------------------------------------------------------------------------------
    Private Sub dispDGV()

        Try

            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & "  'False' " & COLDT_SAKUJOCHK
            sql = sql & N & " ,M21.KOUTEI " & COLDT_KOUTEI
            sql = sql & N & " ,'' " & COLDT_KOUTEIBTN
            sql = sql & N & " ,M21.KIKAIMEI " & COLDT_KIKAINAME
            sql = sql & N & " ,M21.TUUJOUH " & COLDT_TJIKAN
            sql = sql & N & " ,M21.DOYOUH " & COLDT_DJIKAN
            sql = sql & N & " ,M21.NITIYOUH " & COLDT_NJIKAN
            sql = sql & N & " ,'' " & COLDT_FLG
            sql = sql & N & " ,M21.KOUTEI " & COLDT_BKOUTEI
            sql = sql & N & " ,M21.KIKAIMEI " & COLDT_BKIKAINAME
            sql = sql & N & " FROM M21SEISAN M21 "
            sql = sql & N & "   INNER JOIN M01HANYO M01 ON "
            sql = sql & N & "   M21.KOUTEI = M01.KAHENKEY "
            sql = sql & N & " WHERE M01.KOTEIKEY = '" & KOTEIKEY_KOUTEI & "'"
            sql = sql & N & " ORDER BY M01.SORT, M21.KIKAIMEI "

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                Throw New UsrDefException("�o�^����Ă��܂���B", _msgHd.getMSG("noData"))
            End If

            '���o�f�[�^���ꗗ�ɕ\������
            dgvSeisanMst.DataSource = ds
            dgvSeisanMst.DataMember = RS

            lblKensuu.Text = CStr(iRecCnt) & "��"

            '���o�f�[�^������ꍇ�A�o�^�{�^���L��
            If iRecCnt > 0 Then
                initBotton(True)
            End If

            _colorCtlFlg = True

            '�w�i�F�̐ݒ�
            Call setBackcolor(0, 0)

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@�H�����R�[�h���݃`�F�b�N
    '�@(�����T�v)��ʂɓ��͂��ꂽ�H�����R�[�h���ėp�}�X�^�ɑ��݂��邩�`�F�b�N����
    '�@�@I�@�F�@prmKoutei       ��ʂɓ��͂��ꂽ�H�����R�[�h
    '�@�@R�@�F  checkKoutei     SQL���ʂ̌����i0�Ȃ�G���[�j
    '-------------------------------------------------------------------------------
    Private Function checkKoutei(ByVal prmKoutei As String) As Integer
        Try
            checkKoutei = 0

            Dim Sql As String = ""
            Sql = Sql & N & " SELECT KAHENKEY FROM M01HANYO "
            Sql = Sql & N & " WHERE KOTEIKEY = '" & KOTEIKEY_KOUTEI & "'"
            '-->2010/11/25 del start nakazawa
            'Sql = Sql & N & " AND KAHENKEY = '" & prmKoutei & "'"
            '<--2010/11/25 del end nakazawa
            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(Sql, RS, iRecCnt)

            checkKoutei = iRecCnt

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Function

    '-->2010/11/22 del start nakazawa
    '-------------------------------------------------------------------------------
    '�@�H�����R�[�h�Ƌ@�B���L���̑g�����`�F�b�N
    '�@(�����T�v)��ʂɓ��͂��ꂽ�H���Ƌ@�B���̑g���������Y�\�̓}�X�^�ɑ��݂��邩�`�F�b�N����
    '�@�@I�@�F�@prmKoutei           �H��
    '�@�@I�@�F�@prmKikaiName        �@�B��
    '�@�@R�@�F�@checkCombination    SQL���ʂ̌����i0�ȊO�Ȃ�G���[�j
    '-------------------------------------------------------------------------------
    'Private Function checkCombination(ByVal prmKoutei As String, ByVal prmKikaiName As String) As Integer
    '    Try

    '        checkCombination = 0

    '        Dim ds As DataSet
    '        Dim recCnt As Integer = 0

    '        Dim sql As String = ""
    '        sql = sql & N & " SELECT "
    '        sql = sql & N & " KOUTEI "
    '        sql = sql & N & " FROM M21SEISAN "
    '        sql = sql & N & " WHERE KOUTEI = '" & prmKoutei & "'"
    '        sql = sql & N & "   AND KIKAIMEI = '" & prmKikaiName & "'"
    '        ds = _db.selectDB(sql, RS, recCnt)

    '        checkCombination = recCnt

    '    Catch ue As UsrDefException         '���[�U�[��`��O
    '        Call ue.dspMsg()
    '        Throw ue                        '�L���b�`������O�����̂܂܃X���[
    '    Catch ex As Exception               '�V�X�e����O
    '        Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
    '    End Try

    'End Function
    '<--2010/11/22 del end nakazawa

    '------------------------------------------------------------------------------------------------------
    '�@���Y�\�̓}�X�^�̓o�^����
    '�@(�����T�v)�ύX���e��DB�ɔ��f������
    '------------------------------------------------------------------------------------------------------
    Private Sub registDB()
        Try
            _dgv = New UtilDataGridViewHandler(dgvSeisanMst)
            Dim sql As String = ""

            '�����J�n���Ԃƒ[��ID�̎擾
            Dim updStartDate As Date = Now()                           '�����J�n����
            Dim sPCName As String = UtilClass.getComputerName           '�[��ID
            Dim lCntHenkoFlg As Long        '�폜�������������i�X�V�o�^�̂��߂̍폜���܂ށj�̃J�E���g�p

            '�폜����
            lCntHenkoFlg = 0
            For i As Integer = 0 To _dgv.getMaxRow - 1
                '�ύX�t���O�������Ă���f�[�^���폜����
                If HENKO_FLG.Equals(_dgv.getCellData(COLDT_FLG, i).ToString) Then

                    'SQL�����s
                    sql = ""
                    sql = "DELETE FROM M21SEISAN"
                    '' 2011/01/13 upd start sugano
                    'sql = sql & N & " WHERE KOUTEI = '" & _dgv.getCellData(COLDT_BKOUTEI, i).ToString & "'"
                    sql = sql & N & " WHERE KIKAIMEI = '" & _dgv.getCellData(COLDT_BKIKAINAME, i).ToString & "'"
                    '' 2011/01/13 upd end sugano
                    '-->2010/11/25 del start nakazawa
                    'sql = sql & N & "   AND KIKAIMEI = '" & _dgv.getCellData(COLDT_BKIKAINAME, i).ToString & "'"
                    '<--2010/11/25 del end nakazawa
                    _db.executeDB(sql)

                    '�폜�����̃J�E���g�A�b�v
                    lCntHenkoFlg = lCntHenkoFlg + 1
                End If
            Next

            '�o�^������������
            Dim rCntIns As Integer = 0
            '�o�^����
            For i As Integer = 0 To _dgv.getMaxRow - 1
                '�ύX�t���O�������Ă���A���폜�`�F�b�N���Ȃ��f�[�^��o�^����
                If HENKO_FLG.Equals(_dgv.getCellData(COLDT_FLG, i).ToString) _
                   And _dgv.getCellData(COLDT_SAKUJOCHK, i) = False Then
                    'SQL�����s
                    sql = ""
                    sql = "INSERT INTO M21SEISAN ("
                    sql = sql & N & "  KOUTEI "                                                     '�H�����R�[�h
                    sql = sql & N & ", KIKAIMEI "                                                   '�@�B���L��
                    sql = sql & N & ", TUUJOUH "                                                    '�ʏ�ғ�����
                    sql = sql & N & ", DOYOUH "                                                     '�y�j�ғ�����
                    sql = sql & N & ", NITIYOUH "                                                   '���j�ғ�����
                    sql = sql & N & ", UPDNAME "                                                    '�[��ID
                    sql = sql & N & ", UPDDATE "                                                    '�X�V����
                    sql = sql & N & ") VALUES ("
                    sql = sql & N & "  '" & _dgv.getCellData(COLDT_KOUTEI, i).ToString & "'"        '�H�����R�[�h
                    sql = sql & N & ", '" & _dgv.getCellData(COLDT_KIKAINAME, i).ToString & "'"     '�@�B���L��
                    sql = sql & N & ", '" & _dgv.getCellData(COLDT_TJIKAN, i).ToString & "'"        '�ʏ�ғ�����
                    sql = sql & N & ", '" & _dgv.getCellData(COLDT_DJIKAN, i).ToString & "'"        '�y�j�ғ�����
                    sql = sql & N & ", '" & _dgv.getCellData(COLDT_NJIKAN, i).ToString & "'"        '���j�ғ�����
                    sql = sql & N & ", '" & sPCName & "'"                                           '�[��ID
                    sql = sql & N & ", TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "  '�X�V����
                    sql = sql & N & " )"
                    _db.executeDB(sql)

                    '�o�^�����̃J�E���g�A�b�v
                    rCntIns = rCntIns + 1
                End If
            Next

            '�����I���������擾
            Dim updFinDate As Date
            updFinDate = Now

            '�폜���������̎Z�o�i�폜�`�F�b�N�ō폜���������j
            lCntHenkoFlg = lCntHenkoFlg - rCntIns

            Dim cnt As Integer = 0

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
            sql = sql & N & "       " & NS(lCntHenkoFlg) & " , "
            sql = sql & N & "       " & NS(rCntIns) & " , "
            sql = sql & N & "       '" & KOTEIKEY_KOUTEI & "', "
            sql = sql & N & "   '" & sPCName & "' ,"
            sql = sql & N & "   TO_DATE('" & updFinDate & "', 'YYYY/MM/DD HH24:MI:SS')) "
            _db.executeDB(sql, cnt)

            'T02��������e�[�u���X�V
            _parentForm.updateSeigyoTbl(PGID, True, updStartDate, updFinDate)

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@NULL����
    '�@(�������e)�Z���̓��e��NULL�Ȃ�"NULL"��Ԃ�
    '�@�@I�@�F�@prmStr      DB�ɓo�^����NUMBER�^�̒l
    '�@�@R�@�F�@NS          prmStr����Ȃ�uNULL�v�A����ȊO�Ȃ�uprmStr�v
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

#Region "���[�U��`�֐�:�`�F�b�N����"

    '------------------------------------------------------------------------------------------------------
    '  �o�^�`�F�b�N
    '�@(�����T�v)�e���ڂ̕K�{���ڃ`�F�b�N�A�H�����R�[�h�Ƌ@�B���L���̑g�ݍ��킹�d���`�F�b�N
    '------------------------------------------------------------------------------------------------------
    Private Sub checkTouroku(ByRef prmMaxCnt As Long)
        Try

            _dgv = New UtilDataGridViewHandler(dgvSeisanMst)

            For i As Integer = 0 To prmMaxCnt - 1
                If _dgv.getCellData(COLDT_SAKUJOCHK, i) Then
                    '�폜�Ƀ`�F�b�N�������Ă���ꍇ�͓��̓`�F�b�N�͍s��Ȃ�
                Else
                    '�K�{���̓`�F�b�N
                    '�H�����R�[�h
                    Call chekuHissu(COLDT_KOUTEI, "�H�����R�[�h", i, COLNO_KOUTEI)
                    '�@�B���L��
                    Call chekuHissu(COLDT_KIKAINAME, "�@�B���L��", i, COLNO_KIKAINAME)
                    '�ʏ�ғ�����
                    Call chekuHissu(COLDT_TJIKAN, "�ʏ�ғ�����", i, COLNO_TJIKAN)
                    '�y�j�ғ�����
                    Call chekuHissu(COLDT_DJIKAN, "�y�j�ғ�����", i, COLNO_DJIKAN)
                    '���j�ғ�����
                    Call chekuHissu(COLDT_NJIKAN, "���j�ғ�����", i, COLNO_NJIKAN)

                    '-->2010/11/22 upd start nakazawa
                    '�H���Ƌ@�B���̑g�ݍ��킹�d���`�F�b�N
                    '���H���܂��͋@�B�����ύX�O�ƕς���Ă���ꍇ�Ƀ`�F�b�N����B
                    'If Not _dgv.getCellData(COLDT_KOUTEI, i).Equals(_dgv.getCellData(COLDT_BKOUTEI, i)) _
                    ' Or Not _dgv.getCellData(COLDT_KIKAINAME, i).Equals(_dgv.getCellData(COLDT_BKIKAINAME, i)) Then

                    '    If checkCombination(_dgv.getCellData(COLDT_KOUTEI, i).ToString, _dgv.getCellData(COLDT_KIKAINAME, i).ToString) > 0 Then

                    '        '�t�H�[�J�X�����Ă�
                    '        Call setForcusCol(COLNO_KOUTEI, i)
                    '        '�G���[���b�Z�[�W�̕\��
                    '        Throw New UsrDefException("�L�[���d�����Ă��܂��B", _msgHd.getMSG("NotUniqueKey", "�y" & i + 1 & "�s�ځz"))
                    '    End If
                    'End If
                    '�@�B���L���̏d���`�F�b�N
                    If _dgv.getCellData(COLDT_KIKAINAME, i).Equals(_dgv.getCellData(COLDT_BKIKAINAME, i)) Then

                        For j As Integer = i + 1 To prmMaxCnt - 1
                            If _dgv.getCellData(COLDT_KIKAINAME, i).Equals(_dgv.getCellData(COLDT_KIKAINAME, j)) Then

                                '�t�H�[�J�X�����Ă�
                                Call setForcusCol(COLNO_KIKAINAME, j)
                                '-->2010.12.17 chg by takagi #13
                                'Throw New UsrDefException("�@�B���L�����d�����Ă��܂��B", _
                                '        _msgHd.getMSG("NotUniqueKikaiRyaku", "�y" & j + 1 & "�s�ځz"))
                                Throw New UsrDefException("�@�B���L�����d�����Ă��܂��B", _
                                        _msgHd.getMSG("NotUniqueKikaiRyaku"))
                                '<--2010.12.17 chg by takagi #13
                            End If
                        Next
                    End If
                    '�H�����R�[�h�Ƌ@�B���L����1���ڃ`�F�b�N
                    If Not _dgv.getCellData(COLDT_KOUTEI, i).Equals(_dgv.getCellData(COLDT_BKOUTEI, i)) _
                        Or Not _dgv.getCellData(COLDT_KIKAINAME, i).Equals(_dgv.getCellData(COLDT_BKIKAINAME, i)) Then

                        If Not _dgv.getCellData(COLDT_KOUTEI, i).Substring(0, 1).Equals(_dgv.getCellData(COLDT_KIKAINAME, i).Substring(0, 1)) Then

                            '�t�H�[�J�X�����Ă�
                            Call setForcusCol(COLNO_KIKAINAME, i)
                            '-->2010.12.17 chg by takagi #13
                            'Throw New UsrDefException("�H�����R�[�h��1���ڂƁA�@�B���L����1���ڂ���v���Ă��܂���B", _
                            '        _msgHd.getMSG("notEqualKouteiKikaiRyaku", "�y" & i + 1 & "�s�ځz"))
                            Throw New UsrDefException("�H�����R�[�h��1���ڂƁA�@�B���L����1���ڂ���v���Ă��܂���B", _
                                    _msgHd.getMSG("notEqualKouteiKikaiRyaku"))
                            '<--2010.12.17 chg by takagi #13
                        End If
                    End If
                    '<--2010/11/22 upd end nakazawa

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
    '�@�@I�@�F�@prmColName              �`�F�b�N����Z���̗�
    '�@�@I�@�F�@prmColHeaderName        �G���[���ɕ\�������
    '�@�@I�@�F�@prmCnt                  �`�F�b�N����Z���̍s��
    '�@�@I�@�F�@prmColNo                �`�F�b�N����Z���̗�
    '------------------------------------------------------------------------------------------------------
    Private Sub chekuHissu(ByVal prmColName As String, ByVal prmColHeaderName As String, ByVal prmCnt As Integer, ByVal prmColNo As Integer)

        Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanMst)

        If "".Equals(gh.getCellData(prmColName, prmCnt).ToString) Then
            '�t�H�[�J�X�����Ă�
            Call setForcusCol(prmColNo, prmCnt)
            '�G���[���b�Z�[�W�̕\��
            '-->2010.12.17 chg by takagi #13
            'Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y '" & prmColHeaderName & "' �F" & prmCnt + 1 & "�s�ځz"))
            Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput"))
            '<--2010.12.17 chg by takagi #13
        End If

    End Sub

#End Region

End Class

