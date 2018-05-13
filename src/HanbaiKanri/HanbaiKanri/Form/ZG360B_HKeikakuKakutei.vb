'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���j�̔��v��m��w��
'    �i�t�H�[��ID�jZG360B_HKeikakuKakutei
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ����        2010/10/19                 �V�K              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Public Class ZG360B_HKeikakuKakutei
    Inherits System.Windows.Forms.Form

    '-------------------------------------------------------------------------------
    '   �萔��`
    '-------------------------------------------------------------------------------
    Private Const RS As String = "RecSet"                       '�f�[�^�Z�b�g�e�[�u����
    Private Const N As String = ControlChars.NewLine            '���s����
    Private Const FORMTITLE_TEXT_FIX As String = "�̔��v��m��w��"
    Private Const FORMTITLE_TEXT_UNFIX As String = "�̔��v��m������w��"
    Private Const MODELABEL_TEXT_FIX As String = "�m��"
    Private Const MODELABEL_TEXT_UNFIX As String = "�m�����"
    Private Const PGID As String = "ZG360B"
    Private Const FIX_VAL As Integer = 1                        '�m��
    Private Const UNFIX_VAL As Integer = 0                      '����

    '-------------------------------------------------------------------------------
    '   �ϐ���`
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu
    Private _FixMode As Boolean = False '�N�����[�h
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
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmForm As ZC110M_Menu, ByVal prmUpdFlg As Boolean)
        Call Me.New()

        '��������
        _msgHd = prmRefMsgHd                                                'MSG�n���h���̐ݒ�
        _db = prmRefDbHd                                                    'DB�n���h���̐ݒ�
        _parentForm = prmForm                                               '�e�t�H�[��
        StartPosition = FormStartPosition.CenterScreen                      '��ʒ����\��
        _updFlg = prmUpdFlg                                                 '�X�V��

    End Sub

    '-------------------------------------------------------------------------------
    '�@�t�H�[�����[�h�C�x���g
    '-------------------------------------------------------------------------------
    Private Sub ZG360B_HKeikakuKakutei_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            '�`��֌W�̐ݒ�
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           '�T�C�Y���ύX���ꂽ�Ƃ��ɁA�R���g���[�����R���g���[�����̂��ĕ`�悷�邩�ǂ����������l��ݒ�
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '�`��̓o�b�t�@�Ŏ��s����A������ɁA���ʂ���ʂɏo�͂����悤�ݒ�
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  '�R���g���[���́A��ʂɒ��ڂł͂Ȃ��A�܂��o�b�t�@�ɕ`�悳��܂��B����ɂ��A�������}���邱�Ƃ��ł��܂��B
            Me.SetStyle(ControlStyles.UserPaint, True)              '�R���g���[���́A�I�y���[�e�B���O �V�X�e���ɂ���Ăł͂Ȃ��A�Ǝ��ɕ`�悳���悤�ݒ�
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   '�R���g���[���̓E�B���h�E ���b�Z�[�W WM_ERASEBKGND �𖳎�����悤�ɐݒ�

            '�t�H�[���^�C�g���ݒ�
            Call setFormTitle()                                     '�m��/�m�����
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
    '   �t�H�[���^�C�g���ݒ�
    '   �i�����T�v�j�m�胂�[�h���m��������[�h���𔻒肵�A�t�H�[���^�C�g����ύX����Ƌ��ɓ����t�B�[���h�̃t���O�ݒ���s��
    '   �����̓p�����^  �F�Ȃ�
    '   ���o�̓p�����^  �F�Ȃ�
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub setFormTitle()
        Try
            '�������[�h����
            Dim sql As String = ""
            sql = sql & N & "select SDATEEND "
            sql = sql & N & "from T02SEIGYO "
            sql = sql & N & "where PGID = '" & PGID & "' "
            Dim ds As DataSet = _db.selectDB(sql, RS)
            If "".Equals(_db.rmNullDate(ds.Tables(RS).Rows(0)("SDATEEND"))) Then
                '���m�聨�m�胂�[�h
                Me.Text = FORMTITLE_TEXT_FIX
                _FixMode = True
            Else
                '�m��ς݁��������[�h
                Me.Text = FORMTITLE_TEXT_UNFIX
                _FixMode = False
            End If

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '   ��ʏ�����
    '   �i�����T�v�j��ʓ��̊e���ڂ������\������
    '   �����̓p�����^  �F�Ȃ�
    '   ���o�̓p�����^  �F�Ȃ�
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub initForm()
        Try
            '���[�h�ݒ�
            If _FixMode Then
                lblMode.Text = MODELABEL_TEXT_FIX
            Else
                lblMode.Text = MODELABEL_TEXT_UNFIX
                'lblMode.ForeColor = Color.Red
            End If

            '�����N��/�v��N���̕\��
            Dim d As DataSet = _db.selectDB("select SNENGETU,KNENGETU from T01KEIKANRI", RS)
            Dim syoriDate As String = _db.rmNullStr(d.Tables(RS).Rows(0)("SNENGETU"))
            Dim keikakuDate As String = _db.rmNullStr(d.Tables(RS).Rows(0)("KNENGETU"))
            lblSyoriDate.Text = syoriDate.Substring(0, 4) & "/" & syoriDate.Substring(4)
            lblKeikakuDate.Text = keikakuDate.Substring(0, 4) & "/" & keikakuDate.Substring(4)

            '�O����s���̕\��
            Dim sql As String = ""
            Dim iRecCnt As Integer = 0
            sql = sql & N & "select *  "
            sql = sql & N & "from ( "
            sql = sql & N & "    select "
            sql = sql & N & "     ROW_NUMBER() OVER (ORDER BY RECORDID desc) RNUM "
            sql = sql & N & "    ,SDATEEND "
            sql = sql & N & "    ,KENNSU1 "
            sql = sql & N & "    ,SUUTI1 "
            sql = sql & N & "    from T91RIREKI "
            sql = sql & N & "    where PGID     = '" & PGID & "' "
            sql = sql & N & ") "
            sql = sql & N & "where RNUM = 1 "
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)
            If iRecCnt < 1 Then
                '�����Ȃ�
                lblJikkouDate.Text = ZC110M_Menu.NON_EXECUTE
                lblZenkaiKensu.Text = ""
                lblZenkaiMode.Text = ""
            Else
                '��������
                lblJikkouDate.Text = _db.rmNullDate(ds.Tables(RS).Rows(0)("SDATEEND"))
                lblZenkaiKensu.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU1")), "#,##0")
                lblZenkaiMode.Text = IIf(_db.rmNullInt(ds.Tables(RS).Rows(0)("SUUTI1")) = FIX_VAL, MODELABEL_TEXT_FIX, MODELABEL_TEXT_UNFIX)
            End If

            '������s���̕\��
            lblKonkaiKensu.Text = Format(_db.rmNullInt(_db.selectDB("select count(*) CNT from T13HANBAI", RS).Tables(RS).Rows(0)("CNT")), "#,##0")

            '���s�{�^���g�p��
            btnJikkou.Enabled = _updFlg

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

    '-------------------------------------------------------------------------------
    '   ���s�{�^�������C�x���g
    '-------------------------------------------------------------------------------
    Private Sub btnJikkou_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJikkou.Click
        Try

            '���s�m�F�i���s���܂��B��낵���ł����H�j
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmRun")
            If rtn = Windows.Forms.DialogResult.No Or rtn = Windows.Forms.DialogResult.Cancel Then Exit Sub

            '�|�C���^�ύX
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor
            Try
                '�y�o�b�`�����J�n�z
                Dim pb As UtilProgressBar = New UtilProgressBar(Me)                         '�v���O���X�o�[��ʂ�\��
                pb.Show()
                Try
                    Dim st As Date = Now                                                    '�����J�n����
                    Dim ed As Date = Nothing    '�����I������

                    '�v���O���X�o�[�ݒ�
                    pb.jobName = "�m�菈�������s���Ă��܂��B"
                    pb.oneStep = 1
                    pb.maxVal = 2 '(������2)

                    _db.beginTran()
                    Try
                        pb.status = "�X�e�[�^�X�ύX���E�E�E"
                        ed = Now                    '�����I������
                        _parentForm.updateSeigyoTbl(PGID, _FixMode, st, ed) : pb.value += 1     '2-0 ���Y�ʊm��/����

                        pb.status = "���s�����쐬"
                        insertRireki(st, ed) : pb.value += 1                                    '3-0 ���s�����i�[

                    Catch ex As Exception
                        _db.rollbackTran()
                        Throw ex
                    Finally
                        If _db.isTransactionOpen Then _db.commitTran()
                    End Try
                Finally
                    pb.Close()                                                              '�v���O���X�o�[��ʏ���
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
    '   ���s�����쐬
    '   �i�����T�v�j�m�菈���p�̎��s�������쐬����
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
        sql = sql & N & ",KENNSU1 "    '���s����
        sql = sql & N & ",SUUTI1 "     '�������[�h(1:�m��/0:����)
        sql = sql & N & ",UPDNAME "    '�ŏI�X�V��
        sql = sql & N & ",UPDDATE "    '�ŏI�X�V��
        sql = sql & N & ")values( "
        sql = sql & N & "  '" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "' "                             '�����N��
        sql = sql & N & ", '" & _db.rmSQ(lblKeikakuDate.Text.Replace("/", "")) & "' "                           '�v��N��
        sql = sql & N & ", '" & _db.rmSQ(PGID) & "' "                                                           '�@�\ID
        sql = sql & N & ",to_date('" & Format(prmStDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '�����J�n����
        sql = sql & N & ",to_date('" & Format(prmEdDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '�����I������
        sql = sql & N & ", " & CInt(lblKonkaiKensu.Text) & " "                                                  '���s����
        sql = sql & N & ", " & IIf(_FixMode, FIX_VAL, UNFIX_VAL) & " "                                          '�������[�h(1:�m��/0:����)
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "                                     '�ŏI�X�V��
        sql = sql & N & ",sysdate "                                                                             '�ŏI�X�V��
        sql = sql & N & ") "
        _db.executeDB(sql)

    End Sub
End Class