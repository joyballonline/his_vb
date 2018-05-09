'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���j�ėp�}�X�^�@�}�X�^���ڕҏW���
'    �i�t�H�[��ID�jZM311S_HanyouMstHensyuu
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ���V        2010/09/08                 �V�K              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB

Public Class ZM311S_HanyouMstHensyuu
    Inherits System.Windows.Forms.Form

#Region "���e�����l��`"

    'PG���䕶��
    Private Const RS As String = "RecSet"                       '�f�[�^�Z�b�g�e�[�u����
    Private Const N As String = ControlChars.NewLine            '���s����

    Private Const KOTEI As String = "00"                     '��ʕ\���p�Œ�L�[

    Private Const PGID As String = "ZM311S"             'DB�o�^���Ɏg�p����@�\ID

#End Region

#Region "�����o�ϐ���`"

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As Form

    Private _koteiKey As String
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
    '�R���X�g���N�^�@�}�X�^�����e��ʂ���Ă΂��
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmForm As Form, ByVal prmKoteiKey As String)
        Call Me.New()

        '��������
        _msgHd = prmRefMsgHd                                                'MSG�n���h���̐ݒ�
        _db = prmRefDbHd                                                    'DB�n���h���̐ݒ�
        _parentForm = prmForm                                               '�e�t�H�[��
        _koteiKey = prmKoteiKey                                             '�e����󂯎�����Œ�L�[
        StartPosition = FormStartPosition.CenterScreen                      '��ʒ����\��
    End Sub

#End Region

#Region "Form�C�x���g"

    '-------------------------------------------------------------------------------
    '�@�����J�n�C�x���g
    '-------------------------------------------------------------------------------
    Private Sub ZM311S_HanyouMstHensyuu_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            '�`��֌W�̐ݒ�
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           '�T�C�Y���ύX���ꂽ�Ƃ��ɁA�R���g���[�����R���g���[�����̂��ĕ`�悷�邩�ǂ����������l��ݒ�
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '�`��̓o�b�t�@�Ŏ��s����A������ɁA���ʂ���ʂɏo�͂����悤�ݒ�
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  '�R���g���[���́A��ʂɒ��ڂł͂Ȃ��A�܂��o�b�t�@�ɕ`�悳��܂��B����ɂ��A�������}���邱�Ƃ��ł��܂��B
            Me.SetStyle(ControlStyles.UserPaint, True)              '�R���g���[���́A�I�y���[�e�B���O �V�X�e���ɂ���Ăł͂Ȃ��A�Ǝ��ɕ`�悳���悤�ݒ�
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   '�R���g���[���̓E�B���h�E ���b�Z�[�W WM_ERASEBKGND �𖳎�����悤�ɐݒ�

            Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr '�^�C�g���I�v�V�����\��

            '��ʏ����\��
            Call dispLbl()

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

            '���̓`�F�b�N
            Call check()

            '�o�^�m�F�_�C�A���O�\��
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmInsert")   '�o�^���܂��B��낵���ł����H
            If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            'DB�X�V
            Call updateDB()

            _msgHd.dspMSG("completeInsert")     '�o�^���������܂����B

            Call Button1_Click(Nothing, Nothing)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�߂�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModoru.Click

        '����ʂ��I�����A���j���[��ʂɖ߂�B
        _parentForm.Show()
        _parentForm.Activate()

        Me.Close()

    End Sub

#End Region

#Region "���[�U��`�֐�:DB�֘A"

    '------------------------------------------------------------------------------------------------------
    '�@��ʏ����\��
    '�@(�������e)�n���ꂽ�Œ�L�[�����ɔėp�}�X�^�̒l����ʕ\������
    '------------------------------------------------------------------------------------------------------
    Private Sub dispLbl()
        Try

            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & " KOTEIKEY " & "koteikey"
            sql = sql & N & " ,KAHENKEY " & "kahenkey"
            sql = sql & N & " ,NAME1 " & "name"
            sql = sql & N & " ,BIKO " & "biko"
            sql = sql & N & " FROM M01HANYO "
            sql = sql & N & " WHERE KOTEIKEY = '" & KOTEI & "'"
            sql = sql & N & " AND KAHENKEY = '" & _db.rmSQ(_koteiKey) & "'"

            'SQL���s
            Dim recCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, recCnt)

            If recCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                Throw New UsrDefException("�o�^����Ă��܂���B", _msgHd.getMSG("noData"))
            End If

            Dim biko As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("biko"))
            Dim kahenKey As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("kahenkey"))
            Dim name As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("name"))

            lblKoteiKey.Text = kahenKey
            txtKoumokumei.Text = name
            txtKoumokuSetumei.Text = biko

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@DB�X�V
    '------------------------------------------------------------------------------------------------------
    Private Sub updateDB()
        Try
            Dim updStartDate As Date = Now      '�����J�n����

            _db.beginTran()

            Dim sql As String = ""
            sql = "UPDATE "
            sql = sql & N & " M01HANYO SET "
            sql = sql & N & " NAME1 = '" & txtKoumokumei.Text & "'"                     '���̂P
            sql = sql & N & " ,BIKO = '" & _db.rmNullStr(txtKoumokuSetumei.Text) & "'"  '���l
            sql = sql & N & " ,UPDDATE = SYSDATE"                                       '�X�V��
            sql = sql & N & " WHERE KOTEIKEY = '" & KOTEI & "'"
            sql = sql & N & " AND KAHENKEY = '" & _db.rmSQ(_koteiKey) & "'"

            'SQL���s
            Dim recCnt As Integer
            Call _db.executeDB(sql, recCnt)
            If recCnt <= 0 Then
                Call _db.rollbackTran()
                Throw New UsrDefException("�o�^�Ɏ��s���܂����B�A�v���P�[�V�������I�����܂��B", _msgHd.getMSG("failRegist"))
            End If

            Dim machineId As String = SystemInformation.ComputerName    '�[��Id
            Dim updFinDate As Date = Now                                   '�X�V��������я����I������

            Dim deleteCount As String = 0
            Dim insertCount As String = 1

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
            sql = sql & N & "       " & deleteCount & " , "
            sql = sql & N & "       " & insertCount & " , "
            sql = sql & N & "       '" & KOTEI & "', "
            sql = sql & N & "   '" & machineId & "' ,"
            sql = sql & N & "   TO_DATE('" & updFinDate & "', 'YYYY/MM/DD HH24:MI:SS')) "

            _db.executeDB(sql, recCnt)
            If recCnt <= 0 Then
                Call _db.rollbackTran()
                Throw New UsrDefException("�o�^�Ɏ��s���܂����B�A�v���P�[�V�������I�����܂��B", _msgHd.getMSG("failRegist"))
            End If

            _db.commitTran()

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

#End Region

#Region "���[�U��`�֐�:�`�F�b�N����"


    '------------------------------------------------------------------------------------------------------
    '�@���̓`�F�b�N
    '------------------------------------------------------------------------------------------------------
    Private Sub check()
        Try

            If "".Equals(txtKoumokumei.Text) Then
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput"))
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