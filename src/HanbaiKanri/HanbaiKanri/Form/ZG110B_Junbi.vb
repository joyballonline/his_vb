'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���j��������
'    �i�t�H�[��ID�jZG110B_Junbi
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ����        2010/10/15                 �V�K              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB

Public Class ZG110B_Junbi
    Inherits System.Windows.Forms.Form

    '-------------------------------------------------------------------------------
    '   �萔��`
    '-------------------------------------------------------------------------------
    Private Const RS As String = "RecSet"                       '�f�[�^�Z�b�g�e�[�u����
    Private Const N As String = ControlChars.NewLine            '���s����

    Private Const PGID As String = "ZG110B"                     '+���[�h(1:��������/2:�X�V����)
    Public Const BOOTMODE_INIT As String = "1"                  '1:��������
    Public Const BOOTMODE_UPD As String = "2"                   '2:�X�V����

    '-------------------------------------------------------------------------------
    '   �ϐ���`
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu
    Private _bootMode As String = ""    '�N�����[�h
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

    '-------------------------------------------------------------------------------
    '�f�t�H���g�R���X�g���N�^�i�B���j
    '-------------------------------------------------------------------------------
    Private Sub New()
        ' ���̌Ăяo���́AWindows �t�H�[�� �f�U�C�i�ŕK�v�ł��B
        InitializeComponent()

    End Sub

    '-------------------------------------------------------------------------------
    '�R���X�g���N�^�@���j���[��ʂ���Ă΂��
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, _
                   ByRef prmRefDbHd As UtilDBIf, _
                   ByRef prmRefForm As ZC110M_Menu, _
                   ByVal prmBootMode As String, _
                   ByVal prmUpdFlg As Boolean)
        Call Me.New()

        '��������
        _msgHd = prmRefMsgHd                                                'MSG�n���h���̐ݒ�
        _db = prmRefDbHd                                                    'DB�n���h���̐ݒ�
        _parentForm = prmRefForm                                            '�e�t�H�[��
        StartPosition = FormStartPosition.CenterScreen                      '��ʒ����\��
        If BOOTMODE_INIT.Equals(prmBootMode) OrElse _
           BOOTMODE_UPD.Equals(prmBootMode) Then                            '�N�����[�h(��������/�X�V����)
            _bootMode = prmBootMode
        Else
            Throw New UsrDefException("�N�����[�h�Ɍ�肪����܂��B(" & BOOTMODE_INIT & ":��������/" & BOOTMODE_UPD & ":�X�V����)")
        End If
        _updFlg = prmUpdFlg                                                 '�X�V��
    End Sub

    '-------------------------------------------------------------------------------
    '�@�t�H�[�����[�h�C�x���g
    '-------------------------------------------------------------------------------
    Private Sub ZG110B_Junbi_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            '�`��֌W�̐ݒ�
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           '�T�C�Y���ύX���ꂽ�Ƃ��ɁA�R���g���[�����R���g���[�����̂��ĕ`�悷�邩�ǂ����������l��ݒ�
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '�`��̓o�b�t�@�Ŏ��s����A������ɁA���ʂ���ʂɏo�͂����悤�ݒ�
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  '�R���g���[���́A��ʂɒ��ڂł͂Ȃ��A�܂��o�b�t�@�ɕ`�悳��܂��B����ɂ��A�������}���邱�Ƃ��ł��܂��B
            Me.SetStyle(ControlStyles.UserPaint, True)              '�R���g���[���́A�I�y���[�e�B���O �V�X�e���ɂ���Ăł͂Ȃ��A�Ǝ��ɕ`�悳���悤�ݒ�
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   '�R���g���[���̓E�B���h�E ���b�Z�[�W WM_ERASEBKGND �𖳎�����悤�ɐݒ�

            '��ʃ^�C�g���ݒ�
            Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr '�^�C�g���I�v�V�����\��

            '��ʏ�����
            Call initForm()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   ��ʏ�����
    '   �i�����T�v�j��ʂ̊e���ڂ�����������
    '   �����̓p�����^  �F�Ȃ�
    '   ���o�̓p�����^  �F�Ȃ�
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub initForm()
        Try
            '�O����s���̕\��
            Dim sql As String = ""
            Dim iRecCnt As Integer = 0
            sql = sql & N & "select *  "
            sql = sql & N & "from ( "
            sql = sql & N & "    select "
            sql = sql & N & "     RECORDID "
            sql = sql & N & "    ,ROW_NUMBER() OVER (ORDER BY RECORDID desc) RNUM "
            sql = sql & N & "    ,SNENGETU "
            sql = sql & N & "    ,KNENGETU "
            sql = sql & N & "    ,PGID "
            sql = sql & N & "    ,SDATESTART "
            sql = sql & N & "    ,SDATEEND "
            sql = sql & N & "    from T91RIREKI "
            sql = sql & N & "    where PGID = '" & PGID & BOOTMODE_INIT & "' "
            sql = sql & N & ") "
            sql = sql & N & "where RNUM = 1 "
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)
            If iRecCnt < 1 Then
                '�����Ȃ�
                lblPreviousRunDt.Text = ZC110M_Menu.NON_EXECUTE                                                     '�O����s����
                lblPreviousRun_SyoriYM.Text = ""                                                                    '�O����s�������N��
                lblPreviousRun_KeikakuYM.Text = ""                                                                  '�O����s���v��N��
                dteSyoriDate.Text = Format(Now, "yyyy/MM")                                                          '�����N��
                dteKeikakuDate.Text = Format(DateAdd(DateInterval.Month, 1, Now), "yyyy/MM")                        '�v��N��
            Else
                '��������
                lblPreviousRunDt.Text = _db.rmNullDate(ds.Tables(RS).Rows(0)("SDATEEND"))                           '�O����s����
                lblPreviousRun_SyoriYM.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("SNENGETU")).Substring(0, 4) & "/" & _
                                              _db.rmNullStr(ds.Tables(RS).Rows(0)("SNENGETU")).Substring(4, 2)      '�O����s�������N��
                lblPreviousRun_KeikakuYM.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("KNENGETU")).Substring(0, 4) & "/" & _
                                                _db.rmNullStr(ds.Tables(RS).Rows(0)("KNENGETU")).Substring(4, 2)    '�O����s���v��N��
                If _bootMode.Equals(BOOTMODE_INIT) Then
                    '��������
                    dteSyoriDate.Text = Format(DateAdd(DateInterval.Month, 1, CDate(lblPreviousRun_SyoriYM.Text & "/01")), "yyyy/MM")   '�����N��
                    dteKeikakuDate.Text = Format(DateAdd(DateInterval.Month, 1, CDate(dteSyoriDate.Text & "/01")), "yyyy/MM")           '�v��N��
                ElseIf _bootMode.Equals(BOOTMODE_UPD) Then
                    '�X�V����
                    dteSyoriDate.Text = lblPreviousRun_SyoriYM.Text                                                                     '�����N��
                    dteKeikakuDate.Text = Format(DateAdd(DateInterval.Month, 1, CDate(dteSyoriDate.Text & "/01")), "yyyy/MM")           '�v��N��
                End If
            End If

            '�N�����[�h����
            If _bootMode.Equals(BOOTMODE_INIT) Then
                '��������
                numSyuttaibi1.Text = ""                                                                         '��]�o�����P
                numSyuttaibi2.Text = ""                                                                         '��]�o�����Q
                numSyuttaibi3.Text = ""                                                                         '��]�o�����R
                numSyuttaibi4.Text = ""                                                                         '��]�o�����S
                numSyuttaibi5.Text = ""                                                                         '��]�o�����T
                numSyuttaibi6.Text = ""                                                                         '��]�o�����U
                lblSyuttaibiUpdDt.Text = ZC110M_Menu.NON_EXECUTE                                                '�O��X�V����
                btnKousin.Enabled = False                                                                       '�X�V�{�^���g�p�s��
                btnJikkou.Enabled = _updFlg                                                                     '���s�{�^���͍X�V�ۃt���O�Ɉˑ�
            ElseIf _bootMode.Equals(BOOTMODE_UPD) Then
                '�X�V����
                Dim d As DataSet = _db.selectDB("select KIBOU1,KIBOU2,KIBOU3,KIBOU4,KIBOU5,KIBOU6,UPDDATE from T01KEIKANRI", RS, iRecCnt)
                If iRecCnt <> 1 Then Throw New UsrDefException("�v��Ǘ�TBL�̃��R�[�h�\�����s���ł��B")
                With d.Tables(RS).Rows(0)
                    numSyuttaibi1.Text = _db.rmNullStr(.Item("KIBOU1")).PadLeft(8, " "c).Substring(6, 2).Trim   '��]�o�����P
                    numSyuttaibi2.Text = _db.rmNullStr(.Item("KIBOU2")).PadLeft(8, " "c).Substring(6, 2).Trim   '��]�o�����Q
                    numSyuttaibi3.Text = _db.rmNullStr(.Item("KIBOU3")).PadLeft(8, " "c).Substring(6, 2).Trim   '��]�o�����R
                    numSyuttaibi4.Text = _db.rmNullStr(.Item("KIBOU4")).PadLeft(8, " "c).Substring(6, 2).Trim   '��]�o�����S
                    numSyuttaibi5.Text = _db.rmNullStr(.Item("KIBOU5")).PadLeft(8, " "c).Substring(6, 2).Trim   '��]�o�����T
                    numSyuttaibi6.Text = _db.rmNullStr(.Item("KIBOU6")).PadLeft(8, " "c).Substring(6, 2).Trim   '��]�o�����U
                    lblSyuttaibiUpdDt.Text = _db.rmNullDate(.Item("UPDDATE"))                                   '�O��X�V����
                End With
                btnJikkou.Enabled = False                                                                       '���s�{�^���g�p�s��
                btnKousin.Enabled = _updFlg                                                                     '�X�V�{�^���͍X�V�ۃt���O�Ɉˑ�
                dteSyoriDate.Enabled = False                                                                    '�����N���g�p�s��
                dteKeikakuDate.Enabled = False                                                                  '�v��N���g�p�s��
            Else
                Throw New UsrDefException("�N�����[�h�̐����������܂���B���ݒl�F" & _bootMode)
            End If

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�߂�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnModoru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModoru.Click

        '����ʂ��I�����A���j���[��ʂɖ߂�B
        _parentForm.Show()
        _parentForm.Activate()

        Me.Close()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�t�H�[�J�X�擾�C�x���g
    '------------------------------------------------------------------------------------------------------
    Private Sub txt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dteSyoriDate.GotFocus, dteKeikakuDate.GotFocus, numSyuttaibi1.GotFocus, numSyuttaibi2.GotFocus, numSyuttaibi3.GotFocus, numSyuttaibi4.GotFocus, numSyuttaibi5.GotFocus, numSyuttaibi6.GotFocus
        Try
            UtilClass.selAll(sender)
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�L�[�v���X�C�x���g
    '------------------------------------------------------------------------------------------------------
    Private Sub txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dteSyoriDate.KeyPress, dteKeikakuDate.KeyPress, numSyuttaibi1.KeyPress, numSyuttaibi2.KeyPress, numSyuttaibi3.KeyPress, numSyuttaibi4.KeyPress, numSyuttaibi5.KeyPress, numSyuttaibi6.KeyPress
        Try
            UtilClass.moveNextFocus(Me, e)
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@���s�{�^�������C�x���g
    '------------------------------------------------------------------------------------------------------
    Private Sub btnJikkou_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJikkou.Click
        Try
            '���̓`�F�b�N
            Call checkInit()                                                                    '�����N��/�v��N��
            Call checkUpdate()                                                                  '��]�o����

            '���s�m�F�i���s���܂��B��낵���ł����H�j
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmRun")
            If rtn = Windows.Forms.DialogResult.No Or rtn = Windows.Forms.DialogResult.Cancel Then Exit Sub

            '�|�C���^�ύX
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor
            Try
                '�y�o�b�`�����J�n�z
                Dim pb As UtilProgressBar = New UtilProgressBar(Me)                             '�v���O���X�o�[��ʂ�\��
                pb.Show()
                Try
                    Dim st As Date = Now                                                        '�����J�n����
                    Dim ed As Date = Nothing                                                    '�����I������

                    '�v���O���X�o�[�ݒ�
                    pb.jobName = "�������������s���Ă��܂��B"
                    pb.oneStep = 1
                    pb.maxVal = 29 '(�o�b�N�A�b�v/�N���A�Ώ�12TBL�{������4)

                    _db.beginTran()
                    Try
                        pb.status = "�v��Ǘ�TBL�X�V"
                        Call updateKeikakuKanri() : pb.value += 1                                   '1-0 �v��Ǘ�TBL�X�V

                        pb.status = "�f�[�^�o�b�N�A�b�v���E�E�E"
                        Call backUpTrnTbl(pb)                                                       '2-0 TRN�o�b�N�A�b�v

                        pb.status = "�O���f�[�^�������E�E�E"
                        Call deleteTrnTbl(pb)                                                       '3-0 TRN������

                        pb.status = "�X�e�[�^�X�ύX���E�E�E"
                        Call updateSeigyoInit() : pb.value += 1                                     '4-0 �������䏉����
                        ed = Now                                                                    '�����I������
                        _parentForm.updateSeigyoTbl(PGID & _bootMode, True, st, ed) : pb.value += 1 '4-1 �v����ݒ�

                        pb.status = "���s�����쐬"
                        insertRireki(st, ed) : pb.value += 1                                        '5-0 ���s�����i�[

                    Catch ex As Exception
                        _db.rollbackTran()
                        Throw ex
                    Finally
                        If _db.isTransactionOpen Then _db.commitTran()
                    End Try

                Finally
                    pb.Close()                                                                  '�v���O���X�o�[��ʏ���
                End Try

            Finally
                Me.Cursor = cur
            End Try

            '�I��MSG
            Call _msgHd.dspMSG("completeRun")
            Call btnModoru_Click(Nothing, Nothing)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�X�V�{�^�������C�x���g
    '------------------------------------------------------------------------------------------------------
    Private Sub btnKousin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKousin.Click
        Try
            '���̓`�F�b�N
            Call checkUpdate()                                                                  '��]�o����

            '���s�m�F�i�X�V���܂��B��낵���ł����H�j
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmUpdate")
            If rtn = Windows.Forms.DialogResult.No Or rtn = Windows.Forms.DialogResult.Cancel Then Exit Sub

            '�|�C���^�ύX
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor
            Try
                '�y�o�b�`�����J�n�z
                Dim pb As UtilProgressBar = New UtilProgressBar(Me)                             '�v���O���X�o�[��ʂ�\��
                pb.Show()
                Try

                    Dim st As Date = Now                                                        '�����J�n����
                    Dim ed As Date = Nothing                                                    '�����I������

                    '�v���O���X�o�[�ݒ�
                    pb.jobName = "�X�V���������s���Ă��܂��B"
                    pb.oneStep = 1
                    pb.maxVal = 3 '(������3)

                    _db.beginTran()
                    Try
                        pb.status = "�v��Ǘ�TBL�X�V"
                        Call updateKeikakuKanri(True) : pb.value += 1                               '1-0 �v��Ǘ�TBL�X�V

                        pb.status = "�X�e�[�^�X�ύX���E�E�E"
                        ed = Now                                                                    '�����I������
                        _parentForm.updateSeigyoTbl(PGID & _bootMode, True, st, ed) : pb.value += 1 '4-1 �v����ݒ�

                        pb.status = "���s�����쐬"
                        insertRireki(st, ed) : pb.value += 1                                        '5-0 ���s�����i�[

                    Catch ex As Exception
                        _db.rollbackTran()
                        Throw ex
                    Finally
                        If _db.isTransactionOpen Then _db.commitTran()
                    End Try

                Finally
                    pb.Close()                                                                  '�v���O���X�o�[��ʏ���
                End Try

                Finally
                    Me.Cursor = cur
                End Try

            '�I��MSG
            Call _msgHd.dspMSG("completeRun")
            Call btnModoru_Click(Nothing, Nothing)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '   ���������p���̓`�F�b�N
    '   �i�����T�v�j�����N��/�v��N���̕K�{/�召�`�F�b�N���s��
    '   �����̓p�����^  �F�Ȃ�
    '   ���o�̓p�����^  �F�Ȃ�
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub checkInit()
        '���̓`�F�b�N
        If "/".Equals(dteSyoriDate.Text.Trim) Then                                                 '������
            Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput"), dteSyoriDate)
        ElseIf "/".Equals(dteKeikakuDate.Text.Trim) Then                                           '������
            Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput"), dteKeikakuDate)
        ElseIf CDate(dteSyoriDate.Text & "/01") >= CDate(dteKeikakuDate.Text & "/01") Then
            Throw New UsrDefException("�召�֌W���s���ł��B", _msgHd.getMSG("ErrDaiSyoChk"), dteKeikakuDate)
        ElseIf DateAdd(DateInterval.Month, 1, CDate(dteSyoriDate.Text & "/01")) <> CDate(dteKeikakuDate.Text & "/01") Then
            Throw New UsrDefException("�����ȓ��t�����͂���Ă��܂��B", _msgHd.getMSG("ImputedInvalidDate"), dteKeikakuDate)
        End If
    End Sub

    '-------------------------------------------------------------------------------
    '   �X�V�����p���̓`�F�b�N
    '   �i�����T�v�j�o�����P�`�U�̖�����/����/�召�`�F�b�N���s��
    '   �����̓p�����^  �F�Ȃ�
    '   ���o�̓p�����^  �F�Ȃ�
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub checkUpdate()

        '���̓`�F�b�N
        If "".Equals(numSyuttaibi1.Text) Then                                                       '������
            Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput"), numSyuttaibi1)
        ElseIf "".Equals(numSyuttaibi2.Text) Then                                                   '������
            Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput"), numSyuttaibi2)
        ElseIf "".Equals(numSyuttaibi3.Text) Then                                                   '������
            Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput"), numSyuttaibi3)
        ElseIf "".Equals(numSyuttaibi4.Text) Then                                                   '������
            Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput"), numSyuttaibi4)
        ElseIf "".Equals(numSyuttaibi5.Text) Then                                                   '������
            Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput"), numSyuttaibi5)
        ElseIf "".Equals(numSyuttaibi6.Text) Then                                                   '������
            Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput"), numSyuttaibi6)
        ElseIf IsDate(dteKeikakuDate.Text & "/" & numSyuttaibi1.Text.PadLeft(2, "0"c)) = False Then '�����s��
            Throw New UsrDefException("���t���s���ł��B", _msgHd.getMSG("ImputedInvalidDate"), numSyuttaibi1)
        ElseIf IsDate(dteKeikakuDate.Text & "/" & numSyuttaibi2.Text.PadLeft(2, "0"c)) = False Then '�����s��
            Throw New UsrDefException("���t���s���ł��B", _msgHd.getMSG("ImputedInvalidDate"), numSyuttaibi2)
        ElseIf IsDate(dteKeikakuDate.Text & "/" & numSyuttaibi3.Text.PadLeft(2, "0"c)) = False Then '�����s��
            Throw New UsrDefException("���t���s���ł��B", _msgHd.getMSG("ImputedInvalidDate"), numSyuttaibi3)
        ElseIf IsDate(dteKeikakuDate.Text & "/" & numSyuttaibi4.Text.PadLeft(2, "0"c)) = False Then '�����s��
            Throw New UsrDefException("���t���s���ł��B", _msgHd.getMSG("ImputedInvalidDate"), numSyuttaibi4)
        ElseIf IsDate(dteKeikakuDate.Text & "/" & numSyuttaibi5.Text.PadLeft(2, "0"c)) = False Then '�����s��
            Throw New UsrDefException("���t���s���ł��B", _msgHd.getMSG("ImputedInvalidDate"), numSyuttaibi5)
        ElseIf IsDate(dteKeikakuDate.Text & "/" & numSyuttaibi6.Text.PadLeft(2, "0"c)) = False Then '�����s��
            Throw New UsrDefException("���t���s���ł��B", _msgHd.getMSG("ImputedInvalidDate"), numSyuttaibi6)
        ElseIf CInt(numSyuttaibi1.Text) >= CInt(numSyuttaibi2.Text) Then                            '�召�s��
            Throw New UsrDefException("�召�֌W���s���ł��B", _msgHd.getMSG("ErrDaiSyoChk"), numSyuttaibi2)
        ElseIf CInt(numSyuttaibi2.Text) >= CInt(numSyuttaibi3.Text) Then                            '�召�s��
            Throw New UsrDefException("�召�֌W���s���ł��B", _msgHd.getMSG("ErrDaiSyoChk"), numSyuttaibi3)
        ElseIf CInt(numSyuttaibi3.Text) >= CInt(numSyuttaibi4.Text) Then                            '�召�s��
            Throw New UsrDefException("�召�֌W���s���ł��B", _msgHd.getMSG("ErrDaiSyoChk"), numSyuttaibi4)
        ElseIf CInt(numSyuttaibi4.Text) >= CInt(numSyuttaibi5.Text) Then                            '�召�s��
            Throw New UsrDefException("�召�֌W���s���ł��B", _msgHd.getMSG("ErrDaiSyoChk"), numSyuttaibi5)
        ElseIf CInt(numSyuttaibi5.Text) >= CInt(numSyuttaibi6.Text) Then                            '�召�s��
            Throw New UsrDefException("�召�֌W���s���ł��B", _msgHd.getMSG("ErrDaiSyoChk"), numSyuttaibi6)
        End If

    End Sub

    '-------------------------------------------------------------------------------
    '   �v��Ǘ�TBL�X�V
    '   �i�����T�v�j�����N��/�v��N���A�Ȃ�тɊ�]�o�����P�`�U���X�V����
    '   �����̓p�����^  �F[prmOnlyUpdate]   ��]�o�����݂̂̍X�V�Ƃ��邩�ۂ�
    '   ���o�̓p�����^  �F�Ȃ�
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub updateKeikakuKanri(Optional ByVal prmOnlySyuttaiUpdate As Boolean = False)

        Dim sql As String = ""
        sql = sql & N & "update T01KEIKANRI set "
        If prmOnlySyuttaiUpdate = False Then                                                        '�o�����̂ݍX�V���H
            sql = sql & N & " SNENGETU = '" & _db.rmSQ(dteSyoriDate.Text.Replace("/", "")) & "', "
            sql = sql & N & " KNENGETU = '" & _db.rmSQ(dteKeikakuDate.Text.Replace("/", "")) & "', "
        End If
        sql = sql & N & " KIBOU1   =  " & _db.rmSQ(dteKeikakuDate.Text.Replace("/", "") & numSyuttaibi1.Text.PadLeft(2, "0"c)) & ", "
        sql = sql & N & " KIBOU2   =  " & _db.rmSQ(dteKeikakuDate.Text.Replace("/", "") & numSyuttaibi2.Text.PadLeft(2, "0"c)) & ", "
        sql = sql & N & " KIBOU3   =  " & _db.rmSQ(dteKeikakuDate.Text.Replace("/", "") & numSyuttaibi3.Text.PadLeft(2, "0"c)) & ", "
        sql = sql & N & " KIBOU4   =  " & _db.rmSQ(dteKeikakuDate.Text.Replace("/", "") & numSyuttaibi4.Text.PadLeft(2, "0"c)) & ", "
        sql = sql & N & " KIBOU5   =  " & _db.rmSQ(dteKeikakuDate.Text.Replace("/", "") & numSyuttaibi5.Text.PadLeft(2, "0"c)) & ", "
        sql = sql & N & " KIBOU6   =  " & _db.rmSQ(dteKeikakuDate.Text.Replace("/", "") & numSyuttaibi6.Text.PadLeft(2, "0"c)) & ", "
        sql = sql & N & " UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "', "
        sql = sql & N & " UPDDATE = TO_DATE('" & Format(Now, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "
        _db.executeDB(sql)

    End Sub

    '-------------------------------------------------------------------------------
    '   ���s�����쐬
    '   �i�����T�v�j���������p�̎��s�������쐬����
    '   �����̓p�����^  �FprmStDt   �����J�n����
    '                     prmEdDt   �����I������
    '   ���o�̓p�����^  �F�Ȃ�
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub insertRireki(ByVal prmStDt As Date, ByVal prmEdDt As Date)

        Dim sql As String = ""
        sql = sql & N & "insert into T91RIREKI "
        sql = sql & N & "( "
        sql = sql & N & " SNENGETU "   '�����N��
        sql = sql & N & ",KNENGETU "   '�v��N��
        sql = sql & N & ",PGID "       '�@�\ID
        sql = sql & N & ",SDATESTART " '�����J�n����
        sql = sql & N & ",SDATEEND "   '�����I������
        sql = sql & N & ",SUUTI1 "     '��]�o����1
        sql = sql & N & ",SUUTI2 "     '��]�o����2
        sql = sql & N & ",SUUTI3 "     '��]�o����3
        sql = sql & N & ",SUUTI4 "     '��]�o����4
        sql = sql & N & ",SUUTI5 "     '��]�o����5
        sql = sql & N & ",SUUTI6 "     '��]�o����6
        sql = sql & N & ",UPDNAME "    '�ŏI�X�V��
        sql = sql & N & ",UPDDATE "    '�ŏI�X�V��
        sql = sql & N & ")values( "
        sql = sql & N & "  '" & _db.rmSQ(dteSyoriDate.Text.Replace("/", "")) & "' "                                        '�����N��
        sql = sql & N & ", '" & _db.rmSQ(dteKeikakuDate.Text.Replace("/", "")) & "' "                                      '�v��N��
        sql = sql & N & ", '" & _db.rmSQ(PGID & _bootMode) & "' "                                                          '�@�\ID
        sql = sql & N & ",to_date('" & Format(prmStDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "              '�����J�n����
        sql = sql & N & ",to_date('" & Format(prmEdDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "              '�����I������
        sql = sql & N & ", " & _db.rmSQ(dteKeikakuDate.Text.Replace("/", "") & numSyuttaibi1.Text.PadLeft(2, "0"c)) & " "  '��]�o����1
        sql = sql & N & ", " & _db.rmSQ(dteKeikakuDate.Text.Replace("/", "") & numSyuttaibi2.Text.PadLeft(2, "0"c)) & " "  '��]�o����2
        sql = sql & N & ", " & _db.rmSQ(dteKeikakuDate.Text.Replace("/", "") & numSyuttaibi3.Text.PadLeft(2, "0"c)) & " "  '��]�o����3
        sql = sql & N & ", " & _db.rmSQ(dteKeikakuDate.Text.Replace("/", "") & numSyuttaibi4.Text.PadLeft(2, "0"c)) & " "  '��]�o����4
        sql = sql & N & ", " & _db.rmSQ(dteKeikakuDate.Text.Replace("/", "") & numSyuttaibi5.Text.PadLeft(2, "0"c)) & " "  '��]�o����5
        sql = sql & N & ", " & _db.rmSQ(dteKeikakuDate.Text.Replace("/", "") & numSyuttaibi6.Text.PadLeft(2, "0"c)) & " "  '��]�o����6
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "                                                '�ŏI�X�V��
        sql = sql & N & ",sysdate "                                                                                        '�ŏI�X�V��
        sql = sql & N & ") "
        _db.executeDB(sql)

    End Sub

    '-------------------------------------------------------------------------------
    '   �������䏉����
    '   �i�����T�v�j�}�X�����n�������e�����̏������������������A�����̏������J�n����
    '   �����̓p�����^  �F�Ȃ�
    '   ���o�̓p�����^  �F�Ȃ�
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub updateSeigyoInit()

        Dim sql As String = ""
        sql = sql & N & "UPDATE T02SEIGYO SET "
        sql = sql & N & "SDATESTART = NULL, "
        sql = sql & N & "SDATEEND   = NULL, "
        sql = sql & N & "UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "', "
        sql = sql & N & "UPDDATE = TO_DATE('" & Format(Now, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "
        sql = sql & N & "WHERE PGID in ('ZG110B1','ZG110B2','ZG210E1','ZG210E2','ZG220E','ZG230B','ZG310E','ZG320E','ZG330B','ZG340B','ZG350E','ZG360B','ZG410B','ZG510B','ZG530E','ZG540B','ZG610B','ZG620E','ZG630B','ZG640B','ZG720B','ZG730Q')  "
        _db.executeDB(sql)

    End Sub

    '-------------------------------------------------------------------------------
    '   �g�����U�N�V�����o�b�N�A�b�v
    '   �i�����T�v�j�e��g�����U�N�V����TBL���o�b�N�A�b�v�p�X�L�[�}�̑ޔ�TBL�փo�b�N�A�b�v����
    '   �����̓p�����^  �FprmPb �v���O���X�o�[�t�H�[��
    '   ���o�̓p�����^  �FprmPb �v���O���X�o�[�t�H�[��
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub backUpTrnTbl(ByRef prmPb As UtilProgressBar)

        Dim sql As String = ""
        Dim sysdate As String = "to_date('" & Format(Now, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS')"

        '�i���ʔ̔�����
        sql = ""
        sql = sql & N & "insert into B10HINHANJ  "
        sql = sql & N & "select " & sysdate & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "',T.* "
        sql = sql & N & "from T10HINHANJ T"
        _db.executeDB(sql) : prmPb.value += 1

        '�i��ʔ̔��v��e�[�u��
        sql = ""
        sql = sql & N & "insert into B11HINSYUHANK  "
        sql = sql & N & "select " & sysdate & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "',T.* "
        sql = sql & N & "from T11HINSYUHANK T"
        _db.executeDB(sql) : prmPb.value += 1

        '�i���ʔ̔��v��e�[�u��
        sql = ""
        sql = sql & N & "insert into B12HINMEIHANK  "
        sql = sql & N & "select " & sysdate & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "',T.* "
        sql = sql & N & "from T12HINMEIHANK T"
        _db.executeDB(sql) : prmPb.value += 1

        '�̔��v��e�[�u��
        sql = ""
        sql = sql & N & "insert into B13HANBAI  "
        sql = sql & N & "select " & sysdate & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "',T.* "
        sql = sql & N & "from T13HANBAI T"
        _db.executeDB(sql) : prmPb.value += 1

        '���Y�����e�[�u��
        sql = ""
        sql = sql & N & "insert into B21SEISANM  "
        sql = sql & N & "select " & sysdate & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "',T.* "
        sql = sql & N & "from T21SEISANM T"
        _db.executeDB(sql) : prmPb.value += 1

        '�݌Ɏ��уe�[�u��
        sql = ""
        sql = sql & N & "insert into B31ZAIKOJ  "
        sql = sql & N & "select " & sysdate & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "',T.* "
        sql = sql & N & "from T31ZAIKOJ T"
        _db.executeDB(sql) : prmPb.value += 1

        '���Y�v��e�[�u��
        sql = ""
        sql = sql & N & "insert into B41SEISANK  "
        sql = sql & N & "select " & sysdate & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "',T.* "
        sql = sql & N & "from T41SEISANK T"
        _db.executeDB(sql) : prmPb.value += 1

        '��z�e�[�u��
        sql = ""
        sql = sql & N & "insert into B51TEHAI "
        sql = sql & N & "select " & sysdate & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "',T.* "
        sql = sql & N & "from T51TEHAI T"
        _db.executeDB(sql) : prmPb.value += 1

        '���׎R�σe�[�u��
        sql = ""
        sql = sql & N & "insert into B61FUKA  "
        sql = sql & N & "select " & sysdate & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "',T.* "
        sql = sql & N & "from T61FUKA T"
        _db.executeDB(sql) : prmPb.value += 1

        '���׎R�ϖ��׃e�[�u��
        sql = ""
        sql = sql & N & "insert into B62FUKAMEISAI "
        sql = sql & N & "select " & sysdate & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "',T.* "
        sql = sql & N & "from T62FUKAMEISAI T"
        _db.executeDB(sql) : prmPb.value += 1

        '�ғ������e�[�u��
        sql = ""
        sql = sql & N & "insert into B63KADOUBI  "
        sql = sql & N & "select " & sysdate & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "',T.* "
        sql = sql & N & "from T63KADOUBI T"
        _db.executeDB(sql) : prmPb.value += 1

        '����MCH�e�[�u��
        sql = ""
        sql = sql & N & "insert into B64MCH  "
        sql = sql & N & "select " & sysdate & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "',T.* "
        sql = sql & N & "from T64MCH T"
        _db.executeDB(sql) : prmPb.value += 1

        '-->2010.12.07 add by takagi 
        '�̔����яƉ�e�[�u��
        sql = ""
        sql = sql & N & "insert into B71HANBAIS  "
        sql = sql & N & "select " & sysdate & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "',T.* "
        sql = sql & N & "from T71HANBAIS T"
        _db.executeDB(sql) : prmPb.value += 1
        '<--2010.12.07 add by takagi 

        '-->2010.12.09 add by takagi 
        '�݌Ɏ��яƉ�e�[�u��
        sql = ""
        sql = sql & N & "insert into B72ZAIKOS  "
        sql = sql & N & "select " & sysdate & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "',T.* "
        sql = sql & N & "from T72ZAIKOS T"
        _db.executeDB(sql) : prmPb.value += 1
        '<--2010.12.09 add by takagi 
    End Sub

    '-------------------------------------------------------------------------------
    '   �g�����U�N�V�����폜
    '   �i�����T�v�j�e��g�����U�N�V����TBL������������
    '   �����̓p�����^  �FprmPb �v���O���X�o�[�t�H�[��
    '   ���o�̓p�����^  �FprmPb �v���O���X�o�[�t�H�[��
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub deleteTrnTbl(ByRef prmPb As UtilProgressBar)

        '�i���ʔ̔�����(T10HINHANJ)�͍폜���Ȃ�

        _db.executeDB("delete from  T11HINSYUHANK ") : prmPb.value += 1     '�i��ʔ̔��v��e�[�u��
        _db.executeDB("delete from  T12HINMEIHANK") : prmPb.value += 1      '�i���ʔ̔��v��e�[�u��
        _db.executeDB("delete from  T13HANBAI ") : prmPb.value += 1         '�̔��v��e�[�u��
        _db.executeDB("delete from  T21SEISANM ") : prmPb.value += 1        '���Y�����e�[�u��
        _db.executeDB("delete from  T31ZAIKOJ ") : prmPb.value += 1         '�݌Ɏ��уe�[�u��
        _db.executeDB("delete from  T41SEISANK ") : prmPb.value += 1        '���Y�v��e�[�u��
        _db.executeDB("delete from  T51TEHAI ") : prmPb.value += 1          '��z�e�[�u��
        _db.executeDB("delete from  T61FUKA ") : prmPb.value += 1           '���׎R�σe�[�u��
        _db.executeDB("delete from  T62FUKAMEISAI ") : prmPb.value += 1     '���׎R�ϖ��׃e�[�u��
        _db.executeDB("delete from  T63KADOUBI ") : prmPb.value += 1        '�ғ������e�[�u��
        _db.executeDB("delete from  T64MCH ") : prmPb.value += 1            '����MCH�e�[�u��

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�����N���t�H�[�J�X�r���C�x���g
    '------------------------------------------------------------------------------------------------------
    Private Sub dteSyoriDate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dteSyoriDate.LostFocus
        Try
            If (Not "".Equals(dteSyoriDate.Text.Replace("/", "").Trim)) AndAlso "".Equals(dteKeikakuDate.Text.Replace("/", "").Trim) Then
                dteKeikakuDate.Text = Format(DateAdd(DateInterval.Month, 1, CDate(dteSyoriDate.Text & "/01")), "yyyy/MM")
            End If
        Catch ex As Exception

        End Try
    End Sub

End Class