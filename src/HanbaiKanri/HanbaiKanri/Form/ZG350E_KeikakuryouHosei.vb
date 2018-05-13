'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���j�̔��v��ʕ␳
'    �i�t�H�[��ID�jZG350E_KeikakuryouHosei
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
Imports UtilMDL.Combo
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Public Class ZG350E_KeikakuryouHosei
    Inherits System.Windows.Forms.Form

#Region "���e�����l��`"
    '-------------------------------------------------------------------------------
    '   �萔��`
    '-------------------------------------------------------------------------------

    'PG���䕶�� 
    Private Const N As String = ControlChars.NewLine            '���s����
    Private Const RS As String = "RecSet"                       '���R�[�h�Z�b�g�e�[�u��

    Private Const PGID As String = "ZG350E"                     'T02�ɓo�^����PGID
    Private Const SQLPGID As String = "ZG340B"                  'T02������s�N�����擾���邽�߂�PGID

    '�ꗗ�f�[�^�o�C���h��
    Private Const COLDT_STENKAINM As String = "dtSTenkai"           '�T�C�Y�W�J
    Private Const COLDT_JUYOUCD As String = "dtJuyouCD"             '���v��
    Private Const COLDT_JUYOUNAME As String = "dtJuyouName"         '���v�於
    Private Const COLDT_HINSYUKBN As String = "dtHinsyuKbn"         '�i��敪
    Private Const COLDT_HINSYUKBNNM As String = "dtHinsyuKbnName"   '�i��敪��
    Private Const COLDT_HINMEICD As String = "dtHinmeiCD"           '�i���R�[�h
    Private Const COLDT_HINMEI As String = "dtHinmei"               '�i��
    Private Const COLDT_TANCYO As String = "dtTancyo"               '�P��
    Private Const COLDT_THANBAI As String = "dtTHanbai"             '�����̔��v��  
    Private Const COLDT_THANBAIHOSEI As String = "dtTHanbaiHosei"   '�����̔��v��␳��
    Private Const COLDT_THANBAIKEKKA As String = "dtTHanbaiKekka"   '�����̔��v��␳����
    Private Const COLDT_YHANBAI As String = "dtYHanbai"             '�����̔��v��
    Private Const COLDT_YHANBAIHOSEI As String = "dtYHanbaiHosei"   '�����̔��v��␳��
    Private Const COLDT_YHANBAIKEKKA As String = "dtYHanbaiKekka"   '�����̔��v��␳����
    Private Const COLDT_YYHANBAI As String = "dtYYHanbai"           '���X���̔��v��
    Private Const COLDT_YYHANBAIHOSEI As String = "dtYYHanbaiHosei" '���X���̔��v��␳��
    Private Const COLDT_YYHANBAIKEKKA As String = "dtYYHanbaiKekka" '���X���̔��v��␳����
    Private Const COLDT_JUYOUSORT As String = "dtJuyouSort"         '���v��\����
    Private Const COLDT_STENKAISORT As String = "dtSTenkaiSort"     '�T�C�Y�W�J�\����
    Private Const COLDT_UPDNAME As String = "dtUpdName"             '�[��ID
    '-->2010.12.14 add by takagi
    Private Const COLDT_METSUKE As String = "dtMETSUKE"
    Private Const COLDT_THANBAISU As String = "dtTHANBAISU"
    Private Const COLDT_YHANBAISU As String = "dtYHANBAISU"
    Private Const COLDT_YYHANBAISU As String = "dtYYHANBAISU"
    Private Const COLDT_THANBAISUH As String = "dtTHANBAISUH"
    Private Const COLDT_YHANBAISUH As String = "dtYHANBAISUH"
    Private Const COLDT_YYHANBAISUH As String = "dtYYHANBAISUH"
    Private Const COLDT_THANBAISUHK As String = "dtTHANBAISUHK"
    Private Const COLDT_YHANBAISUHK As String = "dtYHANBAISUHK"
    Private Const COLDT_YYHANBAISUHK As String = "dtYYHANBAISUHK"
    '<--2010.12.14 add by takagi

    '�ꗗ�O���b�h��
    Private Const COLCN_STENKAINM As String = "cnSTenkai"           '�T�C�Y�W�J
    Private Const COLCN_JUYOUCD As String = "cnJuyouCD"             '���v��
    Private Const COLCN_JUYOUNAME As String = "cnJuyouName"         '���v�於
    Private Const COLCN_HINSYUKBN As String = "cnHinsyuKbn"         '�i��敪
    Private Const COLCN_HINSYUKBNNM As String = "cnHinsyuKbnName"   '�i��敪��
    '' 2010/12/27 upd start sugano
    'Private Const COLCN_HINMEICD As String = "cnHinmei"             '�i���R�[�h
    Private Const COLCN_HINMEICD As String = "cnHinmeiCD"           '�i���R�[�h
    '' 2010/12/27 upd end sugano
    Private Const COLCN_HINMEI As String = "cnHinmei"               '�i��
    Private Const COLCN_TANCYO As String = "cnTancyo"               '�P��
    Private Const COLCN_THANBAI As String = "cnTHanbai"             '�����̔��v��  
    Private Const COLCN_THANBAIHOSEI As String = "cnTHanbaiHosei"   '�����̔��v��␳��
    Private Const COLCN_THANBAIKEKKA As String = "cnTHanbaiKekka"   '�����̔��v��␳����
    Private Const COLCN_YHANBAI As String = "cnYHanbai"             '�����̔��v��
    Private Const COLCN_YHANBAIHOSEI As String = "cnYHanbaiHosei"   '�����̔��v��␳��
    Private Const COLCN_YHANBAIKEKKA As String = "cnYHanbaiKekka"   '�����̔��v��␳����
    Private Const COLCN_YYHANBAI As String = "cnYYHanbai"           '���X���̔��v��
    Private Const COLCN_YYHANBAIHOSEI As String = "cnYYHanbaiHosei" '���X���̔��v��␳��
    Private Const COLCN_YYHANBAIKEKKA As String = "cnYYHanbaiKekka" '���X���̔��v��␳����
    Private Const COLCN_JUYOUSORT As String = "cnJuyouSort"         '���v��\����
    Private Const COLCN_STENKAISORT As String = "cnSTenkaiSort"     '�T�C�Y�W�J�\����
    Private Const COLCN_UPDNAME As String = "cnUpdName"             '�[��ID
    '-->2010.12.14 add by takagi
    Private Const COLCN_METSUKE As String = "cnMETSUKE"
    Private Const COLCN_THANBAISU As String = "cnTHANBAISU"
    Private Const COLCN_YHANBAISU As String = "cnYHANBAISU"
    Private Const COLCN_YYHANBAISU As String = "cnYYHANBAISU"
    Private Const COLCN_THANBAISUH As String = "cnTHANBAISUH"
    Private Const COLCN_YHANBAISUH As String = "cnYHANBAISUH"
    Private Const COLCN_YYHANBAISUH As String = "cnYYHANBAISUH"
    Private Const COLCN_THANBAISUHK As String = "cnTHANBAISUHK"
    Private Const COLCN_YHANBAISUHK As String = "cnYHANBAISUHK"
    Private Const COLCN_YYHANBAISUHK As String = "cnYYHANBAISUHK"
    '<--2010.12.14 add by takagi

    '�ꗗ��ԍ�
    Private Const COLNO_STENKAINM As Integer = 3        '�T�C�Y�W�J
    Private Const COLNO_JUYOUCD As Integer = 4          '���v��
    Private Const COLNO_JUYOUNAME As Integer = 5        '���v�於
    Private Const COLNO_HINSYUKBN As Integer = 6        '�i��敪
    Private Const COLNO_HINSYUKBNNM As Integer = 7      '�i��敪��
    Private Const COLNO_HINMEICD As Integer = 8         '�i���R�[�h
    Private Const COLNO_HINMEI As Integer = 9           '�i��
    Private Const COLNO_TANCYO As Integer = 10          '�P��
    Private Const COLNO_THANBAI As Integer = 11         '�����̔��v��  
    Private Const COLNO_THANBAIHOSEI As Integer = 12    '�����̔��v��␳��
    Private Const COLNO_YHANBAI As Integer = 13         '�����̔��v��
    Private Const COLNO_YHANBAIHOSEI As Integer = 14    '�����̔��v��␳��
    Private Const COLNO_YYHANBAI As Integer = 15        '���X���̔��v��
    Private Const COLNO_YYHANBAIHOSEI As Integer = 16   '���X���̔��v��␳��

    '���x�����������בւ��p���e����
    Private Const LBL_JUYO As String = "���v��"
    Private Const LBL_HINSYU As String = "�i��敪"
    Private Const LBL_HINMEICD As String = "�i���R�[�h"
    Private Const LBL_HINMEI As String = "�i��"
    Private Const LBL_SYOJUN As String = "��"
    Private Const LBL_KOJUN As String = "��"

    '�ėp�}�X�^�Œ�L�[
    Private Const COTEI_JUYOU As String = "01"                      '���v��
    Private Const COTEI_STENKAI As String = "11"                    '�T�C�Y�W�J�p�^�[��

    'EXCEL
    Private Const START_PRINT As Integer = 7                        'EXCEL�o�͊J�n�s��
    '�o�͗�ԍ�
    Private Const XLSCOL_STENKAI As Integer = 1
    Private Const XLSCOL_JUYOUCD As Integer = 2
    Private Const XLSCOL_JUYOUNM As Integer = 3
    Private Const XLSCOL_HINSYUKBN As Integer = 4
    Private Const XLSCOL_HINSYUKBNNM As Integer = 5
    Private Const XLSCOL_HINMEICD As Integer = 6
    Private Const XLSCOL_HINMEI As Integer = 7
    Private Const XLSCOL_TANCHO As Integer = 8
    Private Const XLSCOL_THANBAI As Integer = 9
    Private Const XLSCOL_YHANBAI As Integer = 10
    Private Const XLSCOL_YYHANBAI As Integer = 11
   
#End Region

#Region "�����o�[�ϐ��錾"

    '-------------------------------------------------------------------------------
    '   �ϐ���`
    '-------------------------------------------------------------------------------

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu

    Private _oldRowIndex As Integer = -1            '�I���s�̔w�i�F��ύX���邽�߂̕ϐ�
    Private _colorCtlFlg As Boolean = False         '�I���s�̔w�i�F��ύX���邽�߂̃t���O

    Private _tanmatuID As String = ""               '�[��ID

    Private _changeFlg As Boolean = False           '�ꗗ�f�[�^�ύX�t���O
    Private _beforeChange As Double = 0             '�ꗗ�ύX�O�̃f�[�^

    Private _chkCellVO As UtilDgvChkCellVO          '�ꗗ�̓��͐����p

    Private _errSet As UtilDataGridViewHandler.dgvErrSet    '�G���[�������Ƀt�H�[�J�X����Z���ʒu
    Private _nyuuryokuErrFlg As Boolean = False             '���̓G���[�L���t���O

    '���������i�[�ϐ�
    Private _serchSTenkai As String = ""            '�T�C�Y�ʓW�J�p�^�[��
    Private _serchJuyo As String = ""               '���v��
    Private _serchCdJuyo As String = ""             '���v��R���{�{�b�N�X�̃R�[�h
    Private _serchHinsyuKbn As String = ""          '�i��敪
    Private _serchSiyoCd As String = ""             '�d�l�R�[�h
    Private _serchHinsyuCd As String = ""           '�i��R�[�h
    Private _serchSensinCd As String = ""           '���S���R�[�h
    Private _serchSizeCd As String = ""             '�T�C�Y�R�[�h
    Private _serchColorCd As String = ""            '�F�R�[�h

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
    '�R���X�g���N�^�@���j���[����Ă΂��
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
    Private Sub ZG350E_KeikakuryouHosei_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            '�`��֌W�̐ݒ�
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           '�T�C�Y���ύX���ꂽ�Ƃ��ɁA�R���g���[�����R���g���[�����̂��ĕ`�悷�邩�ǂ����������l��ݒ�
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '�`��̓o�b�t�@�Ŏ��s����A������ɁA���ʂ���ʂɏo�͂����悤�ݒ�
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  '�R���g���[���́A��ʂɒ��ڂł͂Ȃ��A�܂��o�b�t�@�ɕ`�悳��܂��B����ɂ��A�������}���邱�Ƃ��ł��܂��B
            Me.SetStyle(ControlStyles.UserPaint, True)              '�R���g���[���́A�I�y���[�e�B���O �V�X�e���ɂ���Ăł͂Ȃ��A�Ǝ��ɕ`�悳���悤�ݒ�
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   '�R���g���[���̓E�B���h�E ���b�Z�[�W WM_ERASEBKGND �𖳎�����悤�ɐݒ�

            Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr '�^�C�g���I�v�V�����\��

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

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�����{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnKensaku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensaku.Click
        Try
            '�x�����b�Z�[�W
            If _changeFlg Then
                Dim rtn As DialogResult = _msgHd.dspMSG("confirmSrcEdit")   '�ҏW���̓��e���j������܂��B��낵���ł����H
                If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If

            '�ύX�t���O�����Z�b�g����
            _changeFlg = False

            '�}�E�X�J�[�\�������v
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor
            Try

                '���������̍쐬
                Dim sqlWhere As String = ""
                sqlWhere = createSerchStr()

                ' ''�g�����U�N�V�����J�n
                ''_db.beginTran()

                '���[�N�e�[�u���̍쐬
                Call delInsWK01(sqlWhere)
                Call createWK01()

                ' ''�g�����U�N�V�����I��
                ''_db.commitTran()

                '�ꗗ�s���F�t���O�𖳌��ɂ���
                _colorCtlFlg = False

                '�ꗗ�\��
                Call dispWK01()

                '�ꗗ�s���F�t���O��L���ɂ���
                _colorCtlFlg = True

                '�ꗗ�̍ŏ��̓��͉\�Z���փt�H�[�J�X����
                setForcusCol(COLNO_THANBAIHOSEI, 0)

                '�ꗗ���̍��v�\��
                Call dispTotal()

                '�����N���A�v��N���\��
                Call dispDate()

                '���בւ����x���̕\��������
                Call initLabel()

            Finally
                '�}�E�X�J�[�\�����
                Me.Cursor = cur
            End Try

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

            '�ύX�t���O�����Z�b�g����
            _changeFlg = False

            '�}�E�X�J�[�\�������v
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor
            Try

                '�ꗗ�̃f�[�^�����[�N�e�[�u���ɍX�V
                Call updateWK01()

                '�g�����U�N�V�����J�n
                _db.beginTran()

                'T13�֓o�^
                Call registT13()

                '�g�����U�N�V�����I��
                _db.commitTran()

                '�}�E�X�J�[�\�����
                Me.Cursor = Cursors.Arrow

                '���בւ����x���̕\��������
                Call initLabel()

                '�������b�Z�[�W
                _msgHd.dspMSG("completeInsert")

                '�ꗗ�̕ύX�t���O�𖳌��ɂ���
                _changeFlg = False

            Finally
                '�}�E�X�J�[�\�����
                Me.Cursor = cur
            End Try

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
            '�}�E�X�J�[�\�������v
            Me.Cursor = Cursors.WaitCursor

            'EXCEL�o��
            Call printExcel()

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
    '�@�v�Z�l�ɖ߂��{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnKeisanti_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKeisanti.Click
        Try
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmDelHoseiti")   '�ꗗ�̕␳�l���N���A���܂��B��낵���ł����H
            If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHanbaiHosei)

            For i As Integer = 0 To gh.getMaxRow - 1

                '�ꗗ�␳�l�̃N���A
                dgvHanbaiHosei(COLNO_THANBAIHOSEI, i).Value = DBNull.Value
                dgvHanbaiHosei(COLNO_YHANBAIHOSEI, i).Value = DBNull.Value
                dgvHanbaiHosei(COLNO_YYHANBAIHOSEI, i).Value = DBNull.Value
            Next
            '���v�l�̕\��
            Call dispTotal()

            '�ꗗ�ύX�t���O�𗧂Ă�
            _changeFlg = True

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

            '���`�t�@�C��(�i���ʔ̔��v��Ɠ������`)
            Dim openFilePath As String = StartUp.iniValue.BaseXlsPath & "\" & StartUp.iniValue.ExcelZG320R1_Base
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
            Dim wkEditFile As String = StartUp.iniValue.OutXlsPath & "\" & StartUp.iniValue.ExcelZG320R1_Out     '�R�s�[��t�@�C��

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
                eh.open()
                Try
                    Dim startPrintRow As Integer = START_PRINT          '�o�͊J�n�s��
                    Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHanbaiHosei)        'DGV�n���h���̐ݒ�
                    Dim rowCnt As Integer = gh.getMaxRow
                    Dim i As Integer
                    For i = 0 To rowCnt - 1

                        '���1�s�ǉ�
                        eh.copyRow(startPrintRow + i)
                        eh.insertPasteRow(startPrintRow + i)
                        '�ꗗ�f�[�^�o��
                        eh.setValue(_db.rmNullStr(dgvHanbaiHosei(COLCN_STENKAINM, i).Value), startPrintRow + i, XLSCOL_STENKAI)         '�T�C�Y�W�J�p�^�[��
                        eh.setValue(_db.rmNullStr(dgvHanbaiHosei(COLCN_JUYOUCD, i).Value), startPrintRow + i, XLSCOL_JUYOUCD)           '���v��R�[�h
                        eh.setValue(_db.rmNullStr(dgvHanbaiHosei(COLCN_JUYOUNAME, i).Value), startPrintRow + i, XLSCOL_JUYOUNM)         '���v�於
                        eh.setValue(_db.rmNullStr(dgvHanbaiHosei(COLCN_HINSYUKBN, i).Value), startPrintRow + i, XLSCOL_HINSYUKBN)       '�i��敪
                        eh.setValue(_db.rmNullStr(dgvHanbaiHosei(COLCN_HINSYUKBNNM, i).Value), startPrintRow + i, XLSCOL_HINSYUKBNNM)   '�i��敪��
                        eh.setValue(_db.rmNullStr(dgvHanbaiHosei(COLCN_HINMEICD, i).Value), startPrintRow + i, XLSCOL_HINMEICD)         '�i���R�[�h
                        eh.setValue(_db.rmNullStr(dgvHanbaiHosei(COLCN_HINMEI, i).Value), startPrintRow + i, XLSCOL_HINMEI)             '�i��   
                        eh.setValue(_db.rmNullStr(dgvHanbaiHosei(COLCN_TANCYO, i).Value), startPrintRow + i, XLSCOL_TANCHO)             '�P��
                        eh.setValue(backKeikakuOrHosei(COLNO_THANBAI, COLNO_THANBAIHOSEI, i), startPrintRow + i, XLSCOL_THANBAI)        '�����̔��v��
                        eh.setValue(backKeikakuOrHosei(COLNO_YHANBAI, COLNO_YHANBAIHOSEI, i), startPrintRow + i, XLSCOL_YHANBAI)        '�����̔��v��
                        eh.setValue(backKeikakuOrHosei(COLNO_YYHANBAI, COLNO_YYHANBAIHOSEI, i), startPrintRow + i, XLSCOL_YYHANBAI)     '���X���̔��v��
                    Next

                    '���v�s�\��
                    eh.setValue("���v", startPrintRow + i, 1)
                    eh.setValue("-", startPrintRow + i, 2)
                    eh.setValue("-", startPrintRow + i, 3)
                    eh.setValue("-", startPrintRow + i, 4)
                    eh.setValue("-", startPrintRow + i, 5)
                    eh.setValue("-", startPrintRow + i, 6)
                    eh.setValue("-", startPrintRow + i, 7)
                    eh.setValue("-", startPrintRow + i, 8)
                    eh.setValue(lblTKei.Text, startPrintRow + i, 9)         '�����̔��v��
                    eh.setValue(lblYKei.Text, startPrintRow + i, 10)        '�����̔��v��
                    eh.setValue(lblYYKei.Text, startPrintRow + i, 11)       '���X���̔��v��

                    '�r�����Đݒ�
                    Dim lineV As LineVO = New LineVO()
                    lineV.Bottom = LineVO.LineType.NomalL
                    eh.drawRuledLine(lineV, startPrintRow + i, 1, , 11)

                    '�쐬�����ҏW
                    Dim printDate As String = Now.ToString("yyyy/MM/dd HH:mm")
                    eh.setValue("�쐬���� �F " & printDate, 1, 11)   'K1

                    '�����N���A�v��N���ҏW
                    eh.setValue("�����N���F" & lblSyori.Text & "�@�@�v��N���F" & lblKeikaku.Text, 1, 6)    'F1

                    '�W�v��W�J���s�����\��
                    eh.setValue(lblSyuukei.Text & " �F " & lblJikkouDate.Text, 2, 11)    'K2

                    '�����ҏW
                    eh.setValue(rowCnt & "��", 3, 11)    'K3

                    '��������
                    eh.setValue("�T�C�Y�ʓW�J����݁F" & _serchSTenkai, 3, 1)        'A3
                    eh.setValue("�i��敪�F" & _serchHinsyuKbn, 3, 7)               'G3
                    eh.setValue("�i���R�[�h�F" & createHinmeiCd(), 3, 8)            'H3

                    '���v��̎擾�ƕ\��
                    If Not "".Equals(_serchJuyo) Then
                        Dim sql As String = ""
                        sql = sql & N & " SELECT NAME1  FROM M01HANYO WHERE KOTEIKEY = '01' "
                        sql = sql & N & "   AND KAHENKEY = '" & _serchCdJuyo & "'"
                        'SQL���s
                        Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
                        Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)
                        If iRecCnt = 1 Then
                            eh.setValue("���v��F" & _db.rmNullStr(ds.Tables(RS).Rows(0)(0)), 3, 5)    'E3
                        Else
                            eh.setValue("���v��F", 3, 5)       'E3
                        End If
                    Else
                        eh.setValue("���v��F", 3, 5)           'E3
                    End If

                    '�w�b�_�[�̔N���ҏW
                    eh.setValue(lblTHanbai.Text, 6, 9)          'I6
                    eh.setValue(lblYHanbai.Text, 6, 10)         'J6
                    eh.setValue(lblYYHanbai.Text, 6, 11)        'K6

                    '����̃Z���Ƀt�H�[�J�X���Ă�
                    eh.selectCell(7, 1)     'A7

                    Clipboard.Clear()       '�N���b�v�{�[�h�̏�����

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
    '�@�i���R�[�h�쐬
    '�@(�����T�v)EXCEL�ɏo�͂���i���R�[�h��ҏW���ĕԂ��B
    '�@�@I�@�F�@�Ȃ�
    '�@�@R�@�F�@createHinmeiCd      �ҏW�����i���R�[�h
    '-------------------------------------------------------------------------------
    Private Function createHinmeiCd() As String
        createHinmeiCd = ""

        '�d�l�R�[�h
        If _serchSiyoCd.Length = 2 Then
            createHinmeiCd = _serchSiyoCd & "-"
        ElseIf _serchSiyoCd.Length = 1 Then
            createHinmeiCd = _serchSiyoCd.Substring(0, 1) & "*-"
        Else
            createHinmeiCd = "**-"
        End If

        '�i��R�[�h
        If _serchHinsyuCd.Length = 3 Then
            createHinmeiCd = createHinmeiCd & _serchHinsyuCd & "-"
        ElseIf _serchHinsyuCd.Length = 2 Then
            createHinmeiCd = createHinmeiCd & _serchHinsyuCd.Substring(0, 2) & "*-"
        ElseIf _serchHinsyuCd.Length = 1 Then
            createHinmeiCd = createHinmeiCd & _serchHinsyuCd.Substring(0, 1) & "**-"
        Else
            createHinmeiCd = createHinmeiCd & "***-"
        End If

        '���S���R�[�h
        If _serchSensinCd.Length = 3 Then
            createHinmeiCd = createHinmeiCd & _serchSensinCd & "-"
        ElseIf _serchSensinCd.Length = 2 Then
            createHinmeiCd = createHinmeiCd & _serchSensinCd.Substring(0, 2) & "*-"
        ElseIf _serchSensinCd.Length = 1 Then
            createHinmeiCd = createHinmeiCd & _serchSensinCd.Substring(0, 1) & "**-"
        Else
            createHinmeiCd = createHinmeiCd & "***-"
        End If

        '�T�C�Y�R�[�h
        If _serchSizeCd.Length = 2 Then
            createHinmeiCd = createHinmeiCd & _serchSizeCd & "-"
        ElseIf _serchSizeCd.Length = 1 Then
            createHinmeiCd = createHinmeiCd & _serchSizeCd.Substring(0, 1) & "*-"
        Else
            createHinmeiCd = createHinmeiCd & "**-"
        End If

        '�F�R�[�h
        If _serchColorCd.Length = 3 Then
            createHinmeiCd = createHinmeiCd & _serchColorCd
        ElseIf _serchColorCd.Length = 2 Then
            createHinmeiCd = createHinmeiCd & _serchColorCd.Substring(0, 2) & "*"
        ElseIf _serchColorCd.Length = 1 Then
            createHinmeiCd = createHinmeiCd & _serchColorCd.Substring(0, 1) & "**"
        Else
            createHinmeiCd = createHinmeiCd & "***"
        End If

    End Function

#End Region

#Region "���[�U��`�֐�:��ʐ���"

    '-------------------------------------------------------------------------------
    '�@��ʋN����
    '-------------------------------------------------------------------------------
    Private Sub initForm()
        Try

            '�o�^�{�^���EEXCEL�{�^���g�p�s��
            btnTouroku.Enabled = False
            btnExcel.Enabled = False

            '�[��ID�̎擾
            _tanmatuID = UtilClass.getComputerName

            '�}�E�X�J�[�\�������v
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor
            Try

                '�����N���A�v��N���\��
                Call dispDate()

                '�W�v�E�W�J���s�����̕\��
                Call dispTenkaiDate()

                '���v��R���{�{�b�N�X�E�T�C�Y�ʓW�J�R���{�{�b�N�X�̃Z�b�g
                Call setCbo()

                '�g�����U�N�V�����J�n
                _db.beginTran()

                '�ꗗ�f�[�^�쐬
                Call delInsWK01()
                Call createWK01()

                '�g�����U�N�V�����I��
                _db.commitTran()

                '�ꗗ�\��
                Call dispWK01()

                '�ꗗ���̍��v�\��
                Call dispTotal()

                '�ꗗ�s���F�t���O��L���ɂ���
                _colorCtlFlg = True

                '�w�i�F�̐ݒ�
                Call setBackcolor(0, 0)

            Finally
                '�}�E�X�J�[�\�����
                Me.Cursor = cur
            End Try

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        Finally
            If _db.isTransactionOpen = True Then
                _db.rollbackTran()                          '���[���o�b�N
            End If
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@���בւ����x��������
    '�@(�����T�v)�ꗗ����ёւ���
    '-------------------------------------------------------------------------------
    Private Sub lblJuyoSort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblJuyoSort.Click, _
                                                                                                    lblHinCDSort.Click, _
                                                                                                    lblHinsyuSort.Click, _
                                                                                                    lblHinmeiSort.Click
        Try
            '�}�E�X�J�[�\�������v
            Me.Cursor = Cursors.WaitCursor

            '�ꗗ�s���F�t���O�𖳌��ɂ���
            _colorCtlFlg = False

            '�ꗗ�̃f�[�^�����[�N�e�[�u���ɍX�V
            Call updateWK01()

            '�ꗗ�w�b�_�[���x���ҏW
            If sender.Equals(lblJuyoSort) Then
                '���v��
                If (LBL_JUYO & N & LBL_SYOJUN).Equals(lblJuyoSort.Text) Then
                    '���ɏ����ŕ��בւ����Ă���ꍇ
                    dispWK01("JUYOSORT DESC")
                    lblJuyoSort.Text = LBL_JUYO & N & LBL_KOJUN
                Else
                    '���񉟉����܂��͊��ɍ~���ŕ��בւ����Ă���ꍇ
                    dispWK01("JUYOSORT")
                    lblJuyoSort.Text = LBL_JUYO & N & LBL_SYOJUN
                End If
                '�i��敪�E�i���R�[�h�E�i���̃��x�������ɖ߂�
                lblHinsyuSort.Text = LBL_HINSYU
                lblHinCDSort.Text = LBL_HINMEICD
                lblHinmeiSort.Text = LBL_HINMEI
            ElseIf sender.Equals(lblHinsyuSort) Then
                '�i��敪
                If (LBL_HINSYU & N & LBL_SYOJUN).Equals(lblHinsyuSort.Text) Then
                    '���ɏ����ŕ��בւ����Ă���ꍇ
                    dispWK01("HINSYUKBN DESC")
                    lblHinsyuSort.Text = LBL_HINSYU & N & LBL_KOJUN
                Else
                    '���񉟉����܂��͊��ɍ~���ŕ��בւ����Ă���ꍇ
                    dispWK01("HINSYUKBN")
                    lblHinsyuSort.Text = LBL_HINSYU & N & LBL_SYOJUN
                End If
                '���v��E�i���R�[�h�E�i���̃��x�������ɖ߂�
                lblJuyoSort.Text = LBL_JUYO
                lblHinCDSort.Text = LBL_HINMEICD
                lblHinmeiSort.Text = LBL_HINMEI
            ElseIf sender.Equals(lblHinCDSort) Then
                '�i���R�[�h
                If (LBL_HINMEICD & N & LBL_SYOJUN).Equals(lblHinCDSort.Text) Then
                    '���ɏ����ŕ��בւ����Ă���ꍇ
                    dispWK01("KHINMEICD DESC")
                    lblHinCDSort.Text = LBL_HINMEICD & N & LBL_KOJUN
                Else
                    '���񉟉����܂��͊��ɍ~���ŕ��בւ����Ă���ꍇ
                    dispWK01("KHINMEICD")
                    lblHinCDSort.Text = LBL_HINMEICD & N & LBL_SYOJUN
                End If
                '���v��E�i��敪�E�i���̃��x�������ɖ߂�
                lblJuyoSort.Text = LBL_JUYO
                lblHinsyuSort.Text = LBL_HINSYU
                lblHinmeiSort.Text = LBL_HINMEI
            ElseIf sender.Equals(lblHinmeiSort) Then
                '�i��
                If (LBL_HINMEI & N & LBL_SYOJUN).Equals(lblHinmeiSort.Text) Then
                    '���ɏ����ŕ��בւ����Ă���ꍇ
                    dispWK01("HINMEI DESC")
                    lblHinmeiSort.Text = LBL_HINMEI & N & LBL_KOJUN
                Else
                    '���񉟉����܂��͊��ɍ~���ŕ��בւ����Ă���ꍇ
                    dispWK01("HINMEI")
                    lblHinmeiSort.Text = LBL_HINMEI & N & LBL_SYOJUN
                End If
                '���v��E�i��敪�E�i��R�[�h�̃��x�������ɖ߂�
                lblJuyoSort.Text = LBL_JUYO
                lblHinsyuSort.Text = LBL_HINSYU
                lblHinCDSort.Text = LBL_HINMEI

            Else
                Exit Sub
            End If

            '�w�i�F�̐ݒ�
            Call setBackcolor(0, 0)

            '�ꗗ�s���F�t���O��L���ɂ���
            _colorCtlFlg = True

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        Finally
            '�}�E�X�J�[�\�����ɖ߂�
            Me.Cursor = Cursors.Arrow
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@��������SQL�쐬
    '�@(�����T�v)��ʂɓ��͂��ꂽ����������SQL���ɂ���
    '-------------------------------------------------------------------------------
    Private Function createSerchStr() As String
        Try
            createSerchStr = ""

            Dim chSTenkai As UtilComboBoxHandler = New UtilComboBoxHandler(cboSTenkai)
            Dim chJuyo As UtilComboBoxHandler = New UtilComboBoxHandler(cboJuyou)

            '���������̕ێ�
            _serchSTenkai = _db.rmNullStr(chSTenkai.getName)
            _serchJuyo = _db.rmNullStr(chJuyo.getName)
            _serchCdJuyo = _db.rmNullStr(chJuyo.getCode)
            _serchHinsyuKbn = _db.rmSQ(txtHinsyuKbn.Text)
            _serchSiyoCd = txtSiyoCD.Text
            _serchHinsyuCd = txtHinsyuCD.Text
            _serchSensinCd = txtSensinsuu.Text
            _serchSizeCd = txtSize.Text
            _serchColorCd = txtColor.Text

            '�T�C�Y�ʓW�J�p�^�[��
            If Not "".Equals(_serchSTenkai) Then
                createSerchStr = createSerchStr & N & " TT_TENKAIPTN = '" & _db.rmSQ(chSTenkai.getCode) & "'"
            End If

            '���v��
            If Not "".Equals(_serchJuyo) Then
                If Not "".Equals(createSerchStr) Then
                    createSerchStr = createSerchStr & N & " AND "
                End If
                createSerchStr = createSerchStr & N & " TT_JUYOUCD = '" & _db.rmSQ(chJuyo.getCode) & "'"
            End If

            '�i��敪
            If Not "".Equals(_serchHinsyuKbn) Then
                If Not "".Equals(createSerchStr) Then
                    createSerchStr = createSerchStr & N & " AND "
                End If
                createSerchStr = createSerchStr & " TT_HINSYUKBN LIKE '" & _serchHinsyuKbn & "%'"
            End If

            '�i���R�[�h�d�l
            If Not "".Equals(_serchSiyoCd) Then
                If Not "".Equals(createSerchStr) Then
                    createSerchStr = createSerchStr & N & " AND "
                End If
                createSerchStr = createSerchStr & " TT_H_SIYOU_CD LIKE '" & _serchSiyoCd & "%'"
            End If

            '�i���R�[�h�i��
            If Not "".Equals(_serchHinsyuCd) Then
                If Not "".Equals(createSerchStr) Then
                    createSerchStr = createSerchStr & N & " AND "
                End If
                createSerchStr = createSerchStr & " TT_H_HIN_CD LIKE '" & _serchHinsyuCd & "%'"
            End If

            '�i���R�[�h���S��
            If Not "".Equals(_serchSensinCd) Then
                If Not "".Equals(createSerchStr) Then
                    createSerchStr = createSerchStr & N & " AND "
                End If
                createSerchStr = createSerchStr & " TT_H_SENSIN_CD LIKE '" & _serchSensinCd & "%'"
            End If

            '�i���R�[�h�T�C�Y
            If Not "".Equals(_serchSizeCd) Then
                If Not "".Equals(createSerchStr) Then
                    createSerchStr = createSerchStr & N & " AND "
                End If
                createSerchStr = createSerchStr & " TT_H_SIZE_CD LIKE '" & _serchSizeCd & "%'"
            End If

            '�i���R�[�h�i��
            If Not "".Equals(_serchColorCd) Then
                If Not "".Equals(createSerchStr) Then
                    createSerchStr = createSerchStr & N & " AND "
                End If
                createSerchStr = createSerchStr & " TT_H_COLOR_CD LIKE '" & _serchColorCd & "%'"
            End If
            
        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Function

    '-------------------------------------------------------------------------------
    '�@�R���g���[���L�[�����C�x���g
    '�@(�����T�v)�G���^�[�{�^���������Ɏ��̃R���g���[���Ɉڂ�
    '-------------------------------------------------------------------------------
    Private Sub txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboSTenkai.KeyPress, _
                                                                                                            cboJuyou.KeyPress, _
                                                                                                            txtHinsyuKbn.KeyPress, _
                                                                                                            txtHinsyuCD.KeyPress, _
                                                                                                            txtSiyoCD.KeyPress, _
                                                                                                            txtSensinsuu.KeyPress, _
                                                                                                            txtSize.KeyPress, _
                                                                                                            txtColor.KeyPress
        UtilClass.moveNextFocus(Me, e) '���̃R���g���[���ֈړ����� 

    End Sub

    '-------------------------------------------------------------------------------
    '�@�R���g���[���S�I��
    '�@(�����T�v)�R���g���[���ړ����ɑS�I����Ԃɂ���
    '-------------------------------------------------------------------------------
    Private Sub serch_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtHinsyuKbn.GotFocus, _
                                                                                            txtHinsyuCD.GotFocus, _
                                                                                            txtSiyoCD.GotFocus, _
                                                                                            txtSensinsuu.GotFocus, _
                                                                                            txtSize.GotFocus, _
                                                                                            txtColor.GotFocus
        UtilClass.selAll(sender)

    End Sub

    '-------------------------------------------------------------------------------
    '�@���בւ����x���̕\��������
    '�@(�����T�v)�����E�o�^�{�^���������A���בւ����x���̕\��������������
    '-------------------------------------------------------------------------------
    Private Sub initLabel()

        lblJuyoSort.Text = LBL_JUYO
        lblHinsyuSort.Text = LBL_HINSYU
        lblHinCDSort.Text = LBL_HINMEICD
        lblHinmeiSort.Text = LBL_HINMEI

    End Sub

#End Region

#Region "���[�U��`�֐�:DGV�֘A"

    '------------------------------------------------------------------------------------------------------
    '�@�ꗗ�f�[�^�ҏW�O
    '�@(�����T�v)�ꗗ�̃f�[�^���ύX�����O�̒l��ێ�����
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvHanbaiHosei_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles dgvHanbaiHosei.CellBeginEdit

        '���ɕύX�t���O�������Ă���ꍇ�͉����s��Ȃ�
        If _changeFlg = False Then
            _beforeChange = _db.rmNullDouble(dgvHanbaiHosei(e.ColumnIndex, e.RowIndex).Value)
        End If
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�ꗗ�f�[�^�ҏW��
    '�@(�����T�v)�ꗗ�̃f�[�^���ύX���ꂽ�ꍇ�A�ύX�t���O�𗧂āA���v�̒l���ĕ\������
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvHanbaiHosei_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvHanbaiHosei.CellEndEdit
        Try
            If _changeFlg = False Then
                '�ҏW�O�ƒl���ς���Ă����ꍇ�A�t���O�𗧂Ă�
                If Not _beforeChange.Equals(_db.rmNullDouble(dgvHanbaiHosei(e.ColumnIndex, e.RowIndex).Value)) Then
                    _changeFlg = True
                Else
                    Exit Sub
                End If
            End If

            '���v�̍Čv�Z
            Call dispTotal()

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
    Private Sub dgvHanbaiHosei_EditingControlShowing(ByVal sender As Object, _
                                              ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs _
                                              ) Handles dgvHanbaiHosei.EditingControlShowing

        Try
            Dim _dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHanbaiHosei)        'DGV�n���h���̐ݒ�
            '�������̔��v��A�����̔��v��A���X���̔��v��̏ꍇ
            If dgvHanbaiHosei.CurrentCell.ColumnIndex = COLNO_THANBAIHOSEI Or _
                            dgvHanbaiHosei.CurrentCell.ColumnIndex = COLNO_YHANBAIHOSEI Or _
                            dgvHanbaiHosei.CurrentCell.ColumnIndex = COLNO_YYHANBAIHOSEI Then

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
    '   �I���Z�����؃C�x���g�iDataError�C�x���g�j
    '   �i�����T�v�j���l���͗��ɐ��l�ȊO�����͂��ꂽ�ꍇ�̃G���[����
    '-------------------------------------------------------------------------------
    Private Sub dgvHanbaiHosei_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvHanbaiHosei.DataError

        Try
            e.Cancel = False                                   '�ҏW���[�h�I��

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHanbaiHosei)
            '�������̔��v��A�����̔��v��A���X���̔��v��̏ꍇ�A�O���b�h�ɂ͐��l���̓��[�h(0�`9)�̐����������Ă���̂ŁA�����̉���
            If dgvHanbaiHosei.CurrentCell.ColumnIndex = COLNO_THANBAIHOSEI Or _
                            dgvHanbaiHosei.CurrentCell.ColumnIndex = COLNO_YHANBAIHOSEI Or _
                            dgvHanbaiHosei.CurrentCell.ColumnIndex = COLNO_YYHANBAIHOSEI Then

                gh.AfterchkCell(_chkCellVO)
            End If

            '���̓G���[�t���O�𗧂Ă�
            _nyuuryokuErrFlg = True

            Dim colName As String = ""
            Select Case e.ColumnIndex
                Case COLNO_THANBAIHOSEI
                    colName = COLCN_THANBAIHOSEI
                Case COLNO_YHANBAIHOSEI
                    colName = COLCN_YHANBAIHOSEI
                Case COLNO_YYHANBAIHOSEI
                    colName = COLCN_YYHANBAIHOSEI
                Case Else
                    colName = COLCN_THANBAIHOSEI
            End Select

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

    '-------------------------------------------------------------------------------
    '�@�ꗗ�����v�\��
    '�@(�����T�v)�ꗗ�̉��̍��v��\������B
    '-------------------------------------------------------------------------------
    Private Sub dispTotal()
        Try

            Dim tTotal As Double = 0       '�����̔��v��̍��v
            Dim yTotal As Double = 0       '�����̔��v��̍��v
            Dim yyTotal As Double = 0      '���X�̔��v��̍��v

            Dim _dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHanbaiHosei)

            '�񂲂Ƃɍ��v���Z�o����
            For i As Integer = 0 To _dgv.getMaxRow - 1
                tTotal = tTotal + backKeikakuOrHosei(COLNO_THANBAI, COLNO_THANBAIHOSEI, i)
                yTotal = yTotal + backKeikakuOrHosei(COLNO_YHANBAI, COLNO_YHANBAIHOSEI, i)
                yyTotal = yyTotal + backKeikakuOrHosei(COLNO_YYHANBAI, COLNO_YYHANBAIHOSEI, i)
            Next

            '�J���}�ҏW���ĕ\��
            '-->2011.01.16 chg by takagi #81
            'lblTKei.Text = CStr(tTotal.ToString("N1"))
            'lblYKei.Text = CStr(yTotal.ToString("N1"))
            'lblYYKei.Text = CStr(yyTotal.ToString("N1"))
            lblTKei.Text = CStr(tTotal.ToString("N2"))
            lblYKei.Text = CStr(yTotal.ToString("N2"))
            lblYYKei.Text = CStr(yyTotal.ToString("N2"))
            '<--2011.01.16 chg by takagi #81

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@�v��lor�␳�l��Ԃ�
    '�@(�����T�v)�␳�l�����͂���Ă�����␳�l�A�ȊO�͌v��l��Ԃ��B
    '�@�@I�@�@�F�@�@prmKeikaku�@�@�@�v��l�̗�ԍ�
    '�@�@I�@�@�F�@�@prmHosei�@�@�@�@�␳�l�̗�ԍ�
    '�@�@I�@�@�F�@�@prmCnt�@�@�@�@�@�s��
    '�@�@R�@�@�F�@�@calcTotal�@�@�@ �␳�l���͂���Ă����Ȃ�␳�l�A�ȊO�͌v��l
    '-------------------------------------------------------------------------------
    Private Function backKeikakuOrHosei(ByVal prmKeikaku As Integer, ByVal prmHosei As Integer, ByVal prmCnt As Integer) As Double
        Try
            backKeikakuOrHosei = 0

            If "".Equals(_db.rmNullStr(dgvHanbaiHosei(prmHosei, prmCnt).Value)) Then
                backKeikakuOrHosei = _db.rmNullDouble(dgvHanbaiHosei(prmKeikaku, prmCnt).Value)
            Else
                backKeikakuOrHosei = _db.rmNullDouble(dgvHanbaiHosei(prmHosei, prmCnt).Value)
            End If

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Function
    Private Function backKeikakuOrHosei(ByVal prmKeikaku As String, ByVal prmHosei As String, ByVal prmCnt As Integer) As Double
        Try
            backKeikakuOrHosei = 0
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHanbaiHosei)

            If "".Equals(_db.rmNullStr(gh.getCellData(prmHosei, prmCnt))) Then
                backKeikakuOrHosei = _db.rmNullDouble(gh.getCellData(prmKeikaku, prmCnt))
            Else
                backKeikakuOrHosei = _db.rmNullDouble(gh.getCellData(prmHosei, prmCnt))
            End If

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Function
    '------------------------------------------------------------------------------------------------------
    '�@�@�O���b�h�t�H�[�J�X�ݒ�y�ёI���s�ɒ��F���鏈��
    '�@�@(�����T�v�j�Z���ҏW��ɃG���[�ɂȂ����ꍇ�ɁA�G���[�Z���Ƀt�H�[�J�X��߂��B
    '               �I���s�ɒ��F����B
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvHanbaiHosei_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvHanbaiHosei.SelectionChanged
        Try

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHanbaiHosei)

            '���̓G���[���������ꍇ
            If _nyuuryokuErrFlg Then
                _nyuuryokuErrFlg = False
                gh.setCurrentCell(_errSet)
            End If

            If _colorCtlFlg Then
                '�w�i�F�̐ݒ�
                Call setBackcolor(dgvHanbaiHosei.CurrentCellAddress.Y, _oldRowIndex)

            End If
            _oldRowIndex = dgvHanbaiHosei.CurrentCellAddress.Y

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

        Dim _dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHanbaiHosei)

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
        dgvHanbaiHosei.Focus()
        dgvHanbaiHosei.CurrentCell = dgvHanbaiHosei(prmColIndex, prmRowIndex)

        '�w�i�F�̐ݒ�
        Call setBackcolor(prmRowIndex, _oldRowIndex)

    End Sub

#End Region

#Region "���[�U��`�֐�:DB�֘A"

    '-------------------------------------------------------------------------------
    '�@���[�N�e�[�u���f�[�^�̍쐬
    '�@(�����T�v)���[�N�e�[�u���̃f�[�^���쐬(delete & insert)
    '�@�@I�@�F�@prmSql     ��������(��ʋN�����͉����󂯎��Ȃ�)
    '-------------------------------------------------------------------------------
    Private Sub delInsWK01(Optional ByVal prmSql As String = "")
        Try

            Dim sql As String = ""
            sql = " DELETE FROM ZG350E_W10 WHERE UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql)
            
            '�X�V�������擾
            Dim updateDate As Date = Now

            'M01�v��Ώەi
            sql = ""
            sql = sql & N & " INSERT INTO ZG350E_W10 ("
            sql = sql & N & "    STENKAIPTN "       '�T�C�Y�W�J�p�^�[��
            sql = sql & N & "   ,JUYOUCD "          '���v��R�[�h
            sql = sql & N & "   ,HINSYUKBN "        '�i��敪
            sql = sql & N & "   ,KHINMEICD "        '�v��i���R�[�h
            sql = sql & N & "   ,HINMEI "           '�i��
            sql = sql & N & "   ,TANCYO"            '�P��
            sql = sql & N & "   ,SIYOUCD "          '�d�l�R�[�h
            sql = sql & N & "   ,HINSYUCD "         '�i��R�[�h
            sql = sql & N & "   ,SENSINCD "         '���S���R�[�h
            sql = sql & N & "   ,SIZECD "           '�T�C�Y�R�[�h
            sql = sql & N & "   ,COLORCD "          '�F�R�[�h
            sql = sql & N & "   ,THANBAIRYOU "      '�����̔��v��       
            sql = sql & N & "   ,YHANBAIRYOU "      '�����̔��v�� 
            sql = sql & N & "   ,YYHANBAIRYOU "     '���X���̔��v�� 
            sql = sql & N & "   ,THANBAIRYOUH "     '�����̔��v��␳��  
            sql = sql & N & "   ,YHANBAIRYOUH "     '�����̔��v��␳�� 
            sql = sql & N & "   ,YYHANBAIRYOUH "    '���X���̔��v��␳��
            sql = sql & N & "   ,UPDNAME "          '�[��ID
            '-->2010.12.14 chg by takagi
            'sql = sql & N & "   ,UPDDATE) "         '�X�V����
            sql = sql & N & "   ,UPDDATE "         '�X�V����
            sql = sql & N & "   ,METSUKE       "
            sql = sql & N & "   ,THANBAISU     "
            sql = sql & N & "   ,YHANBAISU     "
            sql = sql & N & "   ,YYHANBAISU    "
            sql = sql & N & "   ,THANBAISUH    "
            sql = sql & N & "   ,YHANBAISUH    "
            sql = sql & N & "   ,YYHANBAISUH   "
            sql = sql & N & "   /*,THANBAISUHK   "
            sql = sql & N & "   ,YHANBAISUHK   "
            sql = sql & N & "   ,YYHANBAISUHK*/)  "
            '<--2010.12.14 chg by takagi
            sql = sql & N & " SELECT "
            sql = sql & N & "    M.TT_TENKAIPTN "
            sql = sql & N & "   ,M.TT_JUYOUCD "
            sql = sql & N & "   ,M.TT_HINSYUKBN "
            sql = sql & N & "   ,M.TT_KHINMEICD "
            sql = sql & N & "   ,M.TT_HINMEI "
            sql = sql & N & "   ,M.TT_TANCYO "
            sql = sql & N & "   ,M.TT_H_SIYOU_CD "
            sql = sql & N & "   ,M.TT_H_HIN_CD "
            sql = sql & N & "   ,M.TT_H_SENSIN_CD "
            sql = sql & N & "   ,M.TT_H_SIZE_CD "
            sql = sql & N & "   ,M.TT_H_COLOR_CD "
            sql = sql & N & "   ,H.THANBAIRYOU "
            sql = sql & N & "   ,H.YHANBAIRYOU "
            sql = sql & N & "   ,H.YYHANBAIRYOU "
            sql = sql & N & "   ,H.THANBAIRYOUH "
            sql = sql & N & "   ,H.YHANBAIRYOUH "
            sql = sql & N & "   ,H.YYHANBAIRYOUH "
            sql = sql & N & "   ,'" & _tanmatuID & "'"
            sql = sql & N & "   ,TO_DATE('" & updateDate & "', 'YYYY/MM/DD HH24:MI:SS') "
            '-->2010.12.14 add by takagi
            sql = sql & N & "   ,H.METSUKE       "
            sql = sql & N & "   ,H.THANBAISU     "
            sql = sql & N & "   ,H.YHANBAISU     "
            sql = sql & N & "   ,H.YYHANBAISU    "
            sql = sql & N & "   ,H.THANBAISUH    "
            sql = sql & N & "   ,H.YHANBAISUH    "
            sql = sql & N & "   ,H.YYHANBAISUH   "
            sql = sql & N & "   /*,H.THANBAISUHK   "
            sql = sql & N & "   ,H.YHANBAISUHK   "
            sql = sql & N & "   ,H.YYHANBAISUHK*/  "
            '<--2010.12.14 add by takagi
            sql = sql & N & "    FROM T13HANBAI H INNER JOIN "
            sql = sql & N & "       (SELECT * FROM M11KEIKAKUHIN "
            If Not "".Equals(prmSql) Then
                sql = sql & N & " WHERE " & prmSql
            End If
            sql = sql & N & "       ) M ON "
            sql = sql & N & "       H.KHINMEICD = M.TT_KHINMEICD "
            _db.executeDB(sql)

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@���[�N�e�[�u���쐬
    '�@(�����T�v)���[�N�e�[�u�����쐬(update)
    '------------------------------------------------------------------------------------------------------
    Private Sub createWK01()
        Try

            'M01�ėp�}�X�^
            Dim sql As String = ""
            sql = sql & N & " UPDATE ZG350E_W10 ZG SET ( "
            sql = sql & N & "    JUYOUNM "
            sql = sql & N & "   ,JUYOSORT) = ("
            sql = sql & N & "       SELECT  "
            sql = sql & N & "           NAME2, "
            sql = sql & N & "           SORT"
            sql = sql & N & "           FROM M01HANYO M "
            sql = sql & N & "           WHERE ZG.JUYOUCD = M.KAHENKEY "
            sql = sql & N & "           AND M.KOTEIKEY = '" & COTEI_JUYOU & "') "
            sql = sql & N & "   WHERE (JUYOUCD) = (SELECT"
            sql = sql & N & "           KAHENKEY "
            sql = sql & N & "           FROM M01HANYO M "
            sql = sql & N & "           WHERE ZG.JUYOUCD = M.KAHENKEY "
            sql = sql & N & "           AND M.KOTEIKEY = '" & COTEI_JUYOU & "'"
            sql = sql & N & "           AND ZG.UPDNAME = '" & _tanmatuID & "')"
            _db.executeDB(sql)

            'M01�ėp�}�X�^
            sql = ""
            sql = sql & N & " UPDATE ZG350E_W10 ZG SET ( "
            sql = sql & N & "    STENKAINM "
            sql = sql & N & "   ,STENKAISORT) = ( "
            sql = sql & N & "       SELECT "
            sql = sql & N & "           NAME1, "
            sql = sql & N & "           SORT "
            sql = sql & N & "           FROM M01HANYO M "
            sql = sql & N & "           WHERE ZG.STENKAIPTN = M.KAHENKEY "
            sql = sql & N & "           AND M.KOTEIKEY = '" & COTEI_STENKAI & "') "
            sql = sql & N & "   WHERE STENKAIPTN = (SELECT "
            sql = sql & N & "           KAHENKEY "
            sql = sql & N & "           FROM M01HANYO M "
            sql = sql & N & "           WHERE ZG.STENKAIPTN = M.KAHENKEY "
            sql = sql & N & "           AND M.KOTEIKEY = '" & COTEI_STENKAI & "'"
            sql = sql & N & "           AND ZG.UPDNAME = '" & _tanmatuID & "')"
            _db.executeDB(sql)

            'M02�i��敪�}�X�^
            sql = ""
            sql = sql & N & " UPDATE ZG350E_W10 ZG SET "
            sql = sql & N & "   ZG.HINSYUKBNNM = ( "
            sql = sql & N & "       SELECT "
            sql = sql & N & "           M.HINSYUKBNNM "
            sql = sql & N & "           FROM M02HINSYUKBN M "
            sql = sql & N & "           WHERE ZG.HINSYUKBN = M.HINSYUKBN "
            sql = sql & N & "           AND ZG.JUYOUCD = M.JUYOUCD) "
            sql = sql & N & "   WHERE (ZG.HINSYUKBN, ZG.JUYOUCD) = (SELECT "
            sql = sql & N & "           HINSYUKBN,"
            sql = sql & N & "           JUYOUCD "
            sql = sql & N & "           FROM M02HINSYUKBN M "
            sql = sql & N & "           WHERE ZG.HINSYUKBN = M.HINSYUKBN "
            sql = sql & N & "           AND ZG.JUYOUCD = M.JUYOUCD "
            sql = sql & N & "           AND ZG.UPDNAME = '" & _tanmatuID & "')"
            _db.executeDB(sql)

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
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
            sql = sql & N & "    STENKAINM " & COLDT_STENKAINM          '�T�C�Y�W�J
            sql = sql & N & "   ,JUYOUCD " & COLDT_JUYOUCD              '���v��
            sql = sql & N & "   ,JUYOUNM " & COLDT_JUYOUNAME            '���v�於
            sql = sql & N & "   ,HINSYUKBN " & COLDT_HINSYUKBN          '�i��敪
            sql = sql & N & "   ,HINSYUKBNNM " & COLDT_HINSYUKBNNM      '�i��敪��
            sql = sql & N & "   ,KHINMEICD " & COLDT_HINMEICD           '�i���R�[�h
            sql = sql & N & "   ,HINMEI " & COLDT_HINMEI                '�i��
            sql = sql & N & "   ,TANCYO " & COLDT_TANCYO                '�P��
            sql = sql & N & "   ,THANBAIRYOU " & COLDT_THANBAI          '�����̔��v��   
            sql = sql & N & "   ,THANBAIRYOUH " & COLDT_THANBAIHOSEI    '�����̔��v��␳��
            sql = sql & N & "   ,YHANBAIRYOU " & COLDT_YHANBAI          '�����̔��v��
            sql = sql & N & "   ,YHANBAIRYOUH " & COLDT_YHANBAIHOSEI    '�����̔��v��␳��
            sql = sql & N & "   ,YYHANBAIRYOU " & COLDT_YYHANBAI        '���X���̔��v��
            sql = sql & N & "   ,YYHANBAIRYOUH " & COLDT_YYHANBAIHOSEI  '���X���̔��v��␳��
            sql = sql & N & "   ,JUYOSORT " & COLDT_JUYOUSORT           '���v��\����
            sql = sql & N & "   ,STENKAISORT " & COLDT_STENKAISORT      '�T�C�Y�W�J�\����
            sql = sql & N & "   ,UPDNAME " & COLDT_UPDNAME              '�[��ID
            '-->2010.12.14 add by takagi
            sql = sql & N & "   ,METSUKE       " & coldt_METSUKE
            sql = sql & N & "   ,THANBAISU     " & coldt_THANBAISU
            sql = sql & N & "   ,YHANBAISU     " & coldt_YHANBAISU
            sql = sql & N & "   ,YYHANBAISU    " & coldt_YYHANBAISU
            sql = sql & N & "   ,THANBAISUH    " & coldt_THANBAISUH
            sql = sql & N & "   ,YHANBAISUH    " & coldt_YHANBAISUH
            sql = sql & N & "   ,YYHANBAISUH   " & coldt_YYHANBAISUH
            'sql = sql & N & "   ,THANBAISUHK   " & coldt_THANBAISUHK
            'sql = sql & N & "   ,YHANBAISUHK   " & coldt_YHANBAISUHK
            'sql = sql & N & "   ,YYHANBAISUHK  " & coldt_YYHANBAISUHK
            '<--2010.12.14 add by takagi
            sql = sql & N & " FROM ZG350E_W10 "
            sql = sql & N & "   WHERE UPDNAME = '" & _tanmatuID & "'"
            sql = sql & N & " ORDER BY "
            If "".Equals(prmSort) Then
                ''2010/12/17 upd start sugano
                'sql = sql & N & " STENKAISORT , JUYOSORT, HINSYUKBN, HINSYUCD, SENSINCD, SIZECD, COLORCD, SIYOUCD "
                sql = sql & N & " HINSYUCD, SENSINCD, SIZECD, COLORCD, SIYOUCD "
                ''2010/12/17 upd end sugano
            Else
                sql = sql & N & " " & prmSort
            End If

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                '�ꗗ�̃N���A
                Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHanbaiHosei)
                gh.clearRow()

                '���v�̃N���A
                lblTKei.Text = ""
                lblYKei.Text = ""
                lblYYKei.Text = ""

                '�����̃N���A
                lblKensuu.Text = "0��"

                '�o�^�EEXCEL�E�v�Z�l�ɖ߂��{�^���g�p�s��
                btnTouroku.Enabled = False
                btnExcel.Enabled = False
                btnKeisanti.Enabled = False
                Throw New UsrDefException("�Ώۃf�[�^������܂���B", _msgHd.getMSG("NonData"))
            Else
                '���o�f�[�^������ꍇ�A�o�^�{�^���EEXCEL�{�^����L���ɂ���
                btnTouroku.Enabled = _updFlg
                '-->2010.12.22 chg by takagi
                'btnExcel.Enabled = _updFlg
                'btnKeisanti.Enabled = _updFlg
                btnExcel.Enabled = True
                btnKeisanti.Enabled = True
                '<--2010.12.22 chg by takagi
            End If

            '���o�f�[�^���ꗗ�ɕ\������
            dgvHanbaiHosei.DataSource = ds
            dgvHanbaiHosei.DataMember = RS

            '�ꗗ�̌�����\������
            lblKensuu.Text = CStr(iRecCnt) & "��"

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
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
    '�@�W�v�E�W�J���s�����\��
    '�@(�����T�v)�W�v�E�W�J���s������\������
    '-------------------------------------------------------------------------------
    Private Sub dispTenkaiDate()
        Try
            Dim sql As String = ""
            sql = " SELECT SDATESTART SDATE FROM T02SEIGYO WHERE PGID = '" & SQLPGID & "'"

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                lblJikkouDate.Text = ""
            Else
                '�ϐ��ɕێ�
                Dim jikkouDate As String = CStr(_db.rmNullStr(ds.Tables(RS).Rows(0)("SDATE")))
                If Not "".Equals(jikkouDate) Then
                    lblJikkouDate.Text = jikkouDate.Substring(0, 16)        'yyyy/mm/dd hh:ss�`���ŕ\��
                Else
                    lblJikkouDate.Text = ""
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
    '�@���v��R���{�{�b�N�X�E�T�C�Y�ʓW�J�̃Z�b�g
    '�@(�����T�v)M01�ėp�}�X�^������v�於�E�T�C�Y�ʓW�J�p�^�[�����𒊏o���ĕ\������B
    '-------------------------------------------------------------------------------
    Private Sub setCbo()
        Try

            '�R���{�{�b�N�X
            Dim sql = ""
            sql = sql & N & " SELECT KAHENKEY KAHEN, NAME2 JUYOUSAKI "
            sql = sql & N & " FROM M01HANYO "
            sql = sql & N & " WHERE KOTEIKEY = '" & COTEI_JUYOU & "' "
            sql = sql & N & " ORDER BY KAHENKEY "

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                btnTouroku.Enabled = False
                btnExcel.Enabled = False
                btnKeisanti.Enabled = False
                Throw New UsrDefException("�ėp�}�X�^�̒l�̎擾�Ɏ��s���܂����B", _msgHd.getMSG("noHanyouMst"))
            End If

            '�R���{�{�b�N�X�N���A
            Me.cboJuyou.Items.Clear()
            Dim chJuyo As UtilComboBoxHandler = New UtilComboBoxHandler(cboJuyou)

            '�擪�ɋ�s
            chJuyo.addItem(New UtilCboVO("", ""))

            '���[�v�����ăR���{�{�b�N�X�ɃZ�b�g
            For i As Integer = 0 To iRecCnt - 1
                chJuyo.addItem(New UtilCboVO(ds.Tables(RS).Rows(i)("KAHEN").ToString, ds.Tables(RS).Rows(i)("JUYOUSAKI").ToString))
            Next

            '�T�C�Y�ʓW�J
            sql = ""
            sql = sql & N & " SELECT KAHENKEY KAHEN, NAME1 STENKAI "
            sql = sql & N & " FROM M01HANYO "
            sql = sql & N & " WHERE KOTEIKEY = '" & COTEI_STENKAI & "'"
            sql = sql & N & " ORDER BY KAHENKEY "

            'SQL���s
            Dim ds2 As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                btnTouroku.Enabled = False
                Throw New UsrDefException("�o�^����Ă��܂���B", _msgHd.getMSG("noData"))
            End If

            '�R���{�{�b�N�X�N���A
            Me.cboSTenkai.Items.Clear()
            Dim chSTenkai As UtilComboBoxHandler = New UtilComboBoxHandler(cboSTenkai)

            '�擪�ɋ�s
            chSTenkai.addItem(New UtilCboVO(0, ""))

            '���[�v�����ăR���{�{�b�N�X�ɃZ�b�g
            For int As Integer = 0 To iRecCnt - 1
                chSTenkai.addItem(New UtilCboVO(ds2.Tables(RS).Rows(int)("KAHEN").ToString, ds2.Tables(RS).Rows(int)("STENKAI").ToString))
            Next

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
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHanbaiHosei)

            '�g�����U�N�V�����J�n
            _db.beginTran()

            '�s�����������[�v
            For i As Integer = 0 To gh.getMaxRow - 1
                sql = ""
                sql = sql & N & " UPDATE ZG350E_W10 SET "
                '-->2010.12.15 add by takagi
                'sql = sql & N & " THANBAIRYOUH = " & NS(_db.rmNullStr(dgvHanbaiHosei(COLNO_THANBAIHOSEI, i).Value))     '�����̔��v��␳��
                'sql = sql & N & " ,YHANBAIRYOUH = " & NS(_db.rmNullStr(dgvHanbaiHosei(COLNO_YHANBAIHOSEI, i).Value))    '�����̔��v��␳��
                'sql = sql & N & " ,YYHANBAIRYOUH = " & NS(_db.rmNullStr(dgvHanbaiHosei(COLNO_YYHANBAIHOSEI, i).Value))  '���X���̔��v��␳��
                'sql = sql & N & " ,THANBAIRYOUHK = " & backKeikakuOrHosei(COLNO_THANBAI, COLNO_THANBAIHOSEI, i)                  '�����̔��v��␳����
                'sql = sql & N & " ,YHANBAIRYOUHK = " & backKeikakuOrHosei(COLNO_YHANBAI, COLNO_YHANBAIHOSEI, i)                  '�����̔��v��␳����
                'sql = sql & N & " ,YYHANBAIRYOUHK = " & backKeikakuOrHosei(COLNO_YYHANBAI, COLNO_YYHANBAIHOSEI, i)               '���X���̔��v��␳����
                sql = sql & N & " THANBAIRYOUH = " & NS(_db.rmNullStr(gh.getCellData(COLDT_THANBAIHOSEI, i)))     '�����̔��v��␳��
                sql = sql & N & " ,YHANBAIRYOUH = " & NS(_db.rmNullStr(gh.getCellData(COLDT_YHANBAIHOSEI, i)))    '�����̔��v��␳��
                sql = sql & N & " ,YYHANBAIRYOUH = " & NS(_db.rmNullStr(gh.getCellData(COLDT_YYHANBAIHOSEI, i)))  '���X���̔��v��␳��
                sql = sql & N & " ,THANBAIRYOUHK = " & backKeikakuOrHosei(COLDT_THANBAI, COLDT_THANBAIHOSEI, i)                  '�����̔��v��␳����
                sql = sql & N & " ,YHANBAIRYOUHK = " & backKeikakuOrHosei(COLDT_YHANBAI, COLDT_YHANBAIHOSEI, i)                  '�����̔��v��␳����
                sql = sql & N & " ,YYHANBAIRYOUHK = " & backKeikakuOrHosei(COLDT_YYHANBAI, COLDT_YYHANBAIHOSEI, i)               '���X���̔��v��␳����
                sql = sql & N & " ,THANBAISUH = " & NS(_db.rmNullStr(gh.getCellData(COLDT_THANBAISUH, i)))     '�����̔��v��␳��
                sql = sql & N & " ,YHANBAISUH = " & NS(_db.rmNullStr(gh.getCellData(COLDT_YHANBAISUH, i)))    '�����̔��v��␳��
                sql = sql & N & " ,YYHANBAISUH = " & NS(_db.rmNullStr(gh.getCellData(COLDT_YYHANBAISUH, i)))  '���X���̔��v��␳��
                sql = sql & N & " ,THANBAISUHK = " & backKeikakuOrHosei(COLDT_THANBAISU, COLDT_THANBAISUH, i)                  '�����̔��v��␳����
                sql = sql & N & " ,YHANBAISUHK = " & backKeikakuOrHosei(COLDT_YHANBAISU, COLDT_YHANBAISUH, i)                  '�����̔��v��␳����
                sql = sql & N & " ,YYHANBAISUHK = " & backKeikakuOrHosei(COLDT_YYHANBAISU, COLDT_YYHANBAISUH, i)               '���X���̔��v��␳����
                '<--2010.12.15 add by takagi
                sql = sql & N & "   WHERE UPDNAME = '" & _tanmatuID & "'"
                sql = sql & N & "   AND KHINMEICD = '" & dgvHanbaiHosei(COLNO_HINMEICD, i).Value & "'"
                _db.executeDB(sql)
            Next

            '�g�����U�N�V�����I��
            _db.commitTran()

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        Finally
            If _db.isTransactionOpen = True Then
                _db.rollbackTran()                          '���[���o�b�N
            End If
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@NULL����
    '�@(�������e)�Z���̓��e��NULL�Ȃ�"NULL"��Ԃ�
    '�@�@I�@�F�@prmStr      DB�ɓo�^����DOUBLE�^�̒l
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

    '-------------------------------------------------------------------------------
    '�@DB�X�V
    '�@(�����T�v)���[�N�e�[�u���̒l��T13�ɓo�^����
    '-------------------------------------------------------------------------------
    Private Sub registT13()
        Try

            'T13�폜
            Dim delCnt As Integer = 0           '�폜���R�[�h��
            Dim sql As String = ""
            sql = " DELETE FROM T13HANBAI T13 "
            sql = sql & N & " WHERE EXISTS "
            sql = sql & N & "   (SELECT * FROM ZG350E_W10 WK "
            sql = sql & N & "       WHERE T13.KHINMEICD = WK.KHINMEICD "
            sql = sql & N & " AND UPDNAME = '" & _tanmatuID & "')"
            _db.executeDB(sql, delCnt)

            '�X�V�J�n�������擾
            Dim updStartDate As Date = Now

            '���[�N�e�[�u���̒l��T13�ɓo�^
            sql = ""
            sql = sql & N & " INSERT INTO T13HANBAI ( "
            sql = sql & N & "       KHINMEICD "         '�i���R�[�h
            sql = sql & N & "      ,TENKAIPTN "         '�T�C�Y�W�J�p�^�[��
            sql = sql & N & "      ,THANBAIRYOU "       '�����̔��v��
            sql = sql & N & "      ,YHANBAIRYOU "       '�����̔��v��
            sql = sql & N & "      ,YYHANBAIRYOU "      '���X���̔��v��
            sql = sql & N & "      ,THANBAIRYOUH "      '�����̔��v��␳��
            sql = sql & N & "      ,YHANBAIRYOUH "      '�����̔��v��␳��
            sql = sql & N & "      ,YYHANBAIRYOUH "     '���X���̔��v��␳��
            sql = sql & N & "      ,THANBAIRYOUHK "     '�����̔��v��␳����
            sql = sql & N & "      ,YHANBAIRYOUHK "     '�����̔��v��␳����
            sql = sql & N & "      ,YYHANBAIRYOUHK"     '���X���̔��v��␳����
            sql = sql & N & "      ,UPDNAME "           '�[��ID
            '-->2010.12.14 chg by takagi
            'sql = sql & N & "      ,UPDDATE )"          '�X�V����
            sql = sql & N & "      ,UPDDATE "          '�X�V����
            sql = sql & N & "      ,METSUKE      "
            sql = sql & N & "      ,THANBAISU    "
            sql = sql & N & "      ,YHANBAISU    "
            sql = sql & N & "      ,YYHANBAISU   "
            sql = sql & N & "      ,THANBAISUH   "
            sql = sql & N & "      ,YHANBAISUH   "
            sql = sql & N & "      ,YYHANBAISUH  "
            sql = sql & N & "      ,THANBAISUHK  "
            sql = sql & N & "      ,YHANBAISUHK  "
            sql = sql & N & "      ,YYHANBAISUHK) "
            '<--2010.12.14 chg by takagi
            sql = sql & N & " SELECT "
            sql = sql & N & "       KHINMEICD "         '�i���R�[�h
            sql = sql & N & "      ,STENKAIPTN "        '�T�C�Y�W�J�p�^�[��
            sql = sql & N & "      ,THANBAIRYOU "       '�����̔��v��
            sql = sql & N & "      ,YHANBAIRYOU "       '�����̔��v��
            sql = sql & N & "      ,YYHANBAIRYOU "      '���X���̔��v��
            sql = sql & N & "      ,THANBAIRYOUH "      '�����̔��v��␳��
            sql = sql & N & "      ,YHANBAIRYOUH "      '�����̔��v��␳��
            sql = sql & N & "      ,YYHANBAIRYOUH "     '���X���̔��v��␳��
            sql = sql & N & "      ,THANBAIRYOUHK "     '�����̔��v��␳����
            sql = sql & N & "      ,YHANBAIRYOUHK "     '�����̔��v��␳����
            sql = sql & N & "      ,YYHANBAIRYOUHK"     '���X���̔��v��␳����
            sql = sql & N & "      ,UPDNAME "           '�[��ID
            sql = sql & N & "      ,TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS')"
            '-->2010.12.14 chg by takagi
            sql = sql & N & "      ,METSUKE      "
            sql = sql & N & "      ,THANBAISU    "
            sql = sql & N & "      ,YHANBAISU    "
            sql = sql & N & "      ,YYHANBAISU   "
            sql = sql & N & "      ,THANBAISUH   "
            sql = sql & N & "      ,YHANBAISUH   "
            sql = sql & N & "      ,YYHANBAISUH  "
            sql = sql & N & "      ,THANBAISUHK  "
            sql = sql & N & "      ,YHANBAISUHK  "
            sql = sql & N & "      ,YYHANBAISUHK "
            '<--2010.12.14 chg by takagi
            sql = sql & N & "   FROM ZG350E_W10 "
            sql = sql & N & "       WHERE UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql)

            '�X�V�I���������擾
            Dim updFinDate As Date = Now

            '�X�V�����̎擾
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHanbaiHosei)        'DGV�n���h���̐ݒ�
            Dim updCnt As Integer = gh.getMaxRow

            '���������E�v������擾
            Dim syoriDate As String = lblSyori.Text.Substring(0, 4) & lblSyori.Text.Substring(5, 2)
            Dim keikakuDate As String = lblKeikaku.Text.Substring(0, 4) & lblKeikaku.Text.Substring(5, 2)

            '���s����o�^����
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
    '�@(�����T�v)�e���ڂ̓��͌����`�F�b�N���s��
    '------------------------------------------------------------------------------------------------------
    Private Sub checkTouroku()
        Try

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHanbaiHosei)

            For i As Integer = 0 To gh.getMaxRow - 1

                '���͌����`�F�b�N
                '�H���R�[�h
                Call checkKeta(COLDT_THANBAIHOSEI, "�����̔��v��", i, COLNO_THANBAIHOSEI)

                '�@�B��
                Call checkKeta(COLDT_YHANBAIHOSEI, "�����̔��v��", i, COLNO_YHANBAIHOSEI)
                
                '�ʏ�ғ�����
                Call checkKeta(COLDT_YYHANBAIHOSEI, "���X���̔��v��", i, COLNO_YYHANBAIHOSEI)

            Next

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '  ���͌��`�F�b�N
    '�@(�����T�v)��������4���܂ł��`�F�b�N����
    '�@�@I�@�F�@prmColName              �`�F�b�N����Z���̗�
    '�@�@I�@�F�@prmColHeaderName        �G���[���ɕ\�������
    '�@�@I�@�F�@prmCnt                  �`�F�b�N����Z���̍s��
    '�@�@I�@�F�@prmColNo                �`�F�b�N����Z���̗�
    '------------------------------------------------------------------------------------------------------
    Private Sub checkKeta(ByVal prmColName As String, ByVal prmColHeaderName As String, ByVal prmCnt As Integer, ByVal prmColNo As Integer)
        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHanbaiHosei)

            '�����`�F�b�N�`�F�b�N 
            If CDbl(_db.rmNullDouble(gh.getCellData(prmColName, prmCnt))) > 10000 Then      '��������4���܂œo�^��
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