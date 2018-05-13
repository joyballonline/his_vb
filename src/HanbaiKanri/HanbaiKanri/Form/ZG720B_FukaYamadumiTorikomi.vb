'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���j���׎R�σf�[�^�捞
'    �i�t�H�[��ID�jZG720B_FukaYamadumiTorikomi
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ���        2010/11/17                 �V�K              
'�@(2)   ����        2011/01/25                 �ύX�@���ڕύX�i����敪����z�敪�j#91              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.Text.UtilTextReader
Imports UtilMDL.FileDirectory.UtilFile
Imports UtilMDL.CommonDialog.UtilCmnDlgHandler
Public Class ZG720B_FukaYamadumiTorikomi
    Inherits System.Windows.Forms.Form

#Region "���e�����l��`"

    'PG���䕶��
    Private Const RS As String = "RecSet"                       '�f�[�^�Z�b�g�e�[�u����
    Private Const N As String = ControlChars.NewLine            '���s����
    Private Const PGID As String = "ZG720B"                     '�v���O����ID
    Private Const IMP_LOG_NM As String = "���׎R�σf�[�^�捞�����o�͏��.txt" '�擾�ł��Ȃ��@�B���L�����o�͂���t�@�C����

    '�捞�t�@�C����ԍ�
    Private Const FL1COL_KOUTEI As Integer = 1              '�H���R�[�h�i�S���j
    Private Const FL1COL_FUKAKUBUN As Integer = 2           '���׋敪
    Private Const FL1COL_KIBOUSYUTTAIBI As Integer = 3      '��]�o����
    Private Const FL1COL_KOUTEITYAKUSYUBI As Integer = 4    '�H�������
    Private Const FL1COL_MCH As Integer = 5                 'MCH
    Private Const FL1COL_MH As Integer = 6                  'MH

    Private Const FL2COL_KOUTEI As Integer = 1              '�H���R�[�h�i�S���j
    Private Const FL2COL_FUKAKUBUN As Integer = 2           '���׋敪
    '' 2011/01/25 CHG-S Sugano #91
    'Private Const FL2COL_SEISAKUKUBUN As Integer = 3        '����敪
    Private Const FL2COL_TEHAIKUBUN As Integer = 3          '��z�敪
    '' 2011/01/25 CHG-E Sugano #91
    Private Const FL2COL_SEIBAN As Integer = 4              '����
    Private Const FL2COL_HINMEI As Integer = 5              '�i��
    Private Const FL2COL_KIBOUSYUTTAIBI As Integer = 6      '��]�o����
    Private Const FL2COL_KOUTEITYAKUSYUBI As Integer = 7    '�H�������
    Private Const FL2COL_MCH As Integer = 8                 'MCH
    Private Const FL2COL_MH As Integer = 9                  'MH
    Private Const FL2COL_TEHAINO As Integer = 10            '��zNo

#End Region

#Region "�����o�ϐ���`"

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu
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

    '-------------------------------------------------------------------------------
    '�R���X�g���N�^�iPrivate�ɂ��āA�O����͌ĂׂȂ��悤�ɂ���j
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
        _updFlg = prmUpdFlg

    End Sub

#End Region

#Region "Form�C�x���g"

    '-------------------------------------------------------------------------------
    '�@�����J�n�C�x���g
    '-------------------------------------------------------------------------------
    Private Sub ZG720B_FukaYamadumiTorikomi_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
    '�@�߂�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnModoru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModoru.Click

        '����ʂ��I�����A���j���[��ʂɖ߂�B
        _parentForm.Show()
        _parentForm.Activate()

        Me.Close()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�t�@�C���I���{�^���i�@�B�ʁj����
    '------------------------------------------------------------------------------------------------------
    Private Sub btnKikaibetu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKikaibetu.Click
        Try

            Dim retPath As String   'openFileDialog�̖߂�l
            Dim openPath As String  '�_�C�A���O�����l���

            '�_�C�A���O�̏����l���擾
            '���񕪂̓��͂�����Ȃ獡�񕪂��A���񕪂��Ȃ��Ȃ�O�񕪂������l�ɂ���
            If String.Empty.Equals(txtPass1.Text) Then
                openPath = txtPastPass1.Text
            Else
                openPath = txtPass1.Text
            End If

            '�f�B���N�g���̑��ݗL�����`�F�b�N
            Dim pathName As String = ""     '�_�C�A���O�����l���̃f�B���N�g��
            Dim fileName As String = ""     '�_�C�A���O�����l���̃t�@�C��
            UtilClass.dividePathAndFile(openPath, pathName, fileName)

            If UtilClass.isDirExists(pathName) Then
                '�f�B���N�g����
                retPath = openFileDialog(pathName)
            Else
                '�f�B���N�g������
                retPath = openFileDialog()
            End If

            If Not retPath.Equals(String.Empty) Then
                txtPass1.Text = retPath
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�t�@�C���I���{�^���i���ׁj����
    '------------------------------------------------------------------------------------------------------
    Private Sub btnYamadumi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYamadumi.Click
        Try

            Dim retPath As String   'openFileDialog�̖߂�l
            Dim openPath As String  '�_�C�A���O�����l���

            '�_�C�A���O�̏����l���擾
            '���񕪂̓��͂�����Ȃ獡�񕪂��A���񕪂��Ȃ��Ȃ�O�񕪂������l�ɂ���
            If String.Empty.Equals(txtPass2.Text) Then
                openPath = txtPastPass2.Text
            Else
                openPath = txtPass2.Text
            End If

            '�f�B���N�g���̑��ݗL�����`�F�b�N
            Dim pathName As String = ""     '�_�C�A���O�����l���̃f�B���N�g��
            Dim fileName As String = ""     '�_�C�A���O�����l���̃t�@�C��
            UtilClass.dividePathAndFile(openPath, pathName, fileName)

            If UtilClass.isDirExists(pathName) Then
                '�f�B���N�g����
                retPath = openFileDialog(pathName)
            Else
                '�f�B���N�g������
                retPath = openFileDialog()
            End If

            If Not retPath.Equals(String.Empty) Then
                txtPass2.Text = retPath
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@���s�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnJikkou_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJikkou.Click

        Try
            Dim pass1 As String = txtPass1.Text     '�t�@�C���捞�p�X�i�@�B�ʁj
            Dim pass2 As String = txtPass2.Text     '�t�@�C���捞�p�X�i���ׁj

            '�捞�t�@�C���̃`�F�b�N
            Call checkFile(pass1, pass2)

            Dim pb As UtilProgressBar               '�v���O���X�o�[
            Dim t61InsertCount As Integer           'T61�o�^����
            Dim t62InsertCount As Integer           'T62�o�^����
            Dim notGetKikaimeiCount As Integer      'M21����擾�ł��Ȃ������@�B������

            '�|�C���^�ύX
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor

            Try
                pb = New UtilProgressBar(Me)     '�v���O���X�o�[��ʂ�\��
                pb.Show()
                Try
                    Dim st As Date = Now    '�����J�n����
                    Dim procEndDate As Date '�����I������

                    '�v���O���X�o�[�ݒ�
                    pb.jobName = "���׎R�σf�[�^���쐬���Ă��܂�"
                    pb.status = "���׎R�σe�[�u���쐬��"
                    pb.value = 0

                    '�g�����U�N�V�����J�n
                    _db.beginTran()
                    Try
                        '���׎R�σe�[�u���쐬
                        Call insertHukayamazumiTable(st, pb, pass1, t61InsertCount)

                        pb.status = "���׎R�ϖ��׃e�[�u���쐬��"
                        pb.value = 0
                        '���׎R�ϖ��׃e�[�u���쐬
                        Call insertHukayamazumiMeisaiTable(pb, pass2, t62InsertCount)

                        'M21����擾�ł��Ȃ��@�B���L�����t�@�C���o�͂���
                        Call printNotGetKikaimei(notGetKikaimeiCount)

                        '�����I�����Ԃ�ێ�
                        procEndDate = Now

                        pb.status = "�X�e�[�^�X�ύX��"
                        pb.oneStep = 1
                        pb.maxVal = 1
                        pb.value = 0
                        '��������e�[�u���X�V
                        _parentForm.updateSeigyoTbl(PGID, True, st, procEndDate)
                        pb.value = 1

                        pb.status = "���s�����쐬��"
                        pb.maxVal = 1
                        pb.value = 0
                        '���s�����쐬
                        Call insertRireki(st, procEndDate, t61InsertCount, t62InsertCount, pass1, pass2)
                        pb.value = 1

                        '�g�����U�N�V�����I��
                        _db.commitTran()

                    Finally
                        If _db.isTransactionOpen = True Then
                            _db.rollbackTran()   '���[���o�b�N
                        End If
                    End Try

                Finally
                    pb.Close()
                End Try

                '�I��MSG
                If notGetKikaimeiCount > 0 Then
                    '�擾�ł��Ȃ������@�B���L�������݂���ꍇ
                    Dim optionMsg As String = ""
                    optionMsg = "-----------------------------------------------------------------" & N & _
                                "�H���̎擾���s���Ȃ��f�[�^��" & notGetKikaimeiCount & "�����݂��܂����B" & N & _
                                "�ڍׂȋ@�B���L���̓��O���m�F���Ă��������B"
                    Call _msgHd.dspMSG("completeInsert", optionMsg)
                ElseIf t61InsertCount = 0 And t62InsertCount = 0 Then
                    '�o�^�Ώۂ����݂��Ȃ������ꍇ
                    Call _msgHd.dspMSG("noInsertData")
                Else
                    '�ʏ�I��
                    Call _msgHd.dspMSG("completeInsert")
                End If

                If notGetKikaimeiCount > 0 Then
                    '�擾�ł��Ȃ������@�B���L�������݂���ꍇ�̓��O��\������
                    Try
                        System.Diagnostics.Process.Start(UtilClass.getAppPath(StartUp.assembly) & "\" & IMP_LOG_NM)   '�֘A�t�����A�v���ŋN��
                    Catch ex As Exception
                    End Try
                End If

                '���s�I����̓��j���[�ɖ߂�
                Call btnModoru_Click(Nothing, Nothing)

            Finally
                Me.Cursor = cur
            End Try

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

            '���s�����A�捞�����A�捞�p�X�\��
            Call dispRireki()

            '���s�{�^���g�p��
            btnJikkou.Enabled = _updFlg

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

            If iRecCnt <= 0 Then            '���o���R�[�h���P�����Ȃ��ꍇ
                Throw New UsrDefException("�o�^����Ă��܂���B", _msgHd.getMSG("noData"))
            End If

            Dim syoriDate As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("SYORI"))     '��������
            Dim keikakuDate As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("KEIKAKU")) '�v�����

            '�uYYYY/MM�v�`���ŕ\��
            lblSyoriDate.Text = syoriDate.Substring(0, 4) & "/" & syoriDate.Substring(4, 2)
            lblKeikakuDate.Text = keikakuDate.Substring(0, 4) & "/" & keikakuDate.Substring(4, 2)

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@'���s�����A�捞�����A�捞�p�X�\��
    '�@(�����T�v)���s�����A�捞�����A�捞�p�X��\������
    '-------------------------------------------------------------------------------
    Private Sub dispRireki()

        Try
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & " SDATEEND " & "JIKKOUDATE"   '���s����
            sql = sql & N & " ,KENNSU1 " & "KIKAIBETU"    '�@�B�ʎ捞����
            sql = sql & N & " ,KENNSU2 " & "MEISAI"       '���׎捞����
            sql = sql & N & " ,NAME1 " & "PASTPASS1"      '�@�B�ʃp�X
            sql = sql & N & " ,NAME2 " & "PASTPASS2"      '���׃p�X
            sql = sql & N & " FROM T91RIREKI "
            sql = sql & N & " WHERE PGID = '" & PGID & "' "
            sql = sql & N & " AND RECORDID = ("
            sql = sql & N & " SELECT "
            sql = sql & N & " MAX(RECORDID) "             '���R�[�hID�̍ő�l
            sql = sql & N & " FROM T91RIREKI "
            sql = sql & N & " WHERE PGID = '" & PGID & "')"

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                lblJikkouDate.Text = "- - -"        '���s������"- - -"
                lblKikaibetu.Text = String.Empty    '�@�B�ʎ捞�����ɂ͉����\�����Ȃ�
                lblMeisai.Text = String.Empty       '���׎捞�����ɂ͉����\�����Ȃ�
                txtPastPass1.Text = String.Empty    '�@�B�ʃp�X�ɂ͉����\�����Ȃ�
                txtPastPass2.Text = String.Empty    '���׃p�X�ɂ͉����\�����Ȃ�
                Exit Sub
            End If

            Dim jikkouDate As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("JIKKOUDATE")) '���s����
            Dim kikaibetu As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("KIKAIBETU"))   '�@�B�ʎ捞����
            Dim meisai As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("MEISAI"))         '���׎捞����
            Dim pastpass1 As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("PASTPASS1"))   '�@�B�ʃp�X
            Dim pastpass2 As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("PASTPASS2"))   '���׃p�X

            lblJikkouDate.Text = jikkouDate
            lblKikaibetu.Text = kikaibetu
            lblMeisai.Text = meisai
            txtPastPass1.Text = pastpass1
            txtPastPass2.Text = pastpass2

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@�捞�t�@�C���`�F�b�N
    '�@�@I�@�F�@prmPass1   �t�@�C���捞�p�X�i�@�B�ʁj
    '�@�@I�@�F�@prmPass2   �t�@�C���捞�p�X�i���ׁj
    '-------------------------------------------------------------------------------
    Private Sub checkFile(ByVal prmPass1 As String, ByVal prmPass2 As String)

        Try
            '�捞�t�@�C���̃`�F�b�N

            '�p�X�̖����̓`�F�b�N
            If String.Empty.Equals(prmPass1) Then
                '�p�X������MSG
                Throw New UsrDefException("��荞�ރt�@�C����I�����Ă��������B", _msgHd.getMSG("selectedImportFile"), txtPass1)
            End If
            If String.Empty.Equals(prmPass2) Then
                '�p�X������MSG
                Throw New UsrDefException("��荞�ރt�@�C����I�����Ă��������B", _msgHd.getMSG("selectedImportFile"), txtPass2)
            End If

            '�t�@�C���̑��ݗL�����`�F�b�N
            Dim pathName As String = ""     '�I���t�@�C���̃f�B���N�g��
            Dim fileName As String = ""     '�I���t�@�C���̃t�@�C��
            UtilClass.dividePathAndFile(prmPass1, pathName, fileName)
            If UtilClass.isFileExists(prmPass1) = False Then
                '�t�@�C������MSG
                Throw New UsrDefException("�t�@�C���̃p�X�����݂��܂���B", _msgHd.getMSG("notExistsFilePath"), txtPass1)
            End If
            UtilClass.dividePathAndFile(prmPass2, pathName, fileName)
            If UtilClass.isFileExists(prmPass2) = False Then
                '�t�@�C������MSG
                Throw New UsrDefException("�t�@�C���̃p�X�����݂��܂���B", _msgHd.getMSG("notExistsFilePath"), txtPass2)
            End If

            '�t�@�C���̓��e���݃`�F�b�N����уt�B�[���h���̃`�F�b�N
            '�@�B�ʂ̃t�@�C�����J��
            Dim tr1 As UtilMDL.Text.UtilTextReader = New UtilMDL.Text.UtilTextReader(prmPass1)
            tr1.open()
            Try
                If tr1.EOF = True Then
                    '�t�@�C���̓��e���݃`�F�b�NMSG
                    Throw New UsrDefException("�t�@�C���̓��e������܂���B", _msgHd.getMSG("emptyFile"), txtPass1)
                Else
                    If tr1.readLine().Split(",").Length <> 7 Then
                        '�t�B�[���h���ႢMSG
                        Throw New UsrDefException("�t�@�C��������������܂���B", _msgHd.getMSG("irregularFile"), txtPass1)
                    End If
                End If
            Finally
                tr1.close()
            End Try

            '���ׂ̃t�@�C�����J��
            Dim tr2 As UtilMDL.Text.UtilTextReader = New UtilMDL.Text.UtilTextReader(prmPass2)
            tr2.open()
            Try
                If tr2.EOF = True Then
                    '�t�@�C���̓��e���݃`�F�b�NMSG
                    Throw New UsrDefException("�t�@�C���̓��e������܂���B", _msgHd.getMSG("emptyFile"), txtPass2)
                Else
                    If tr2.readLine().Split(",").Length <> 11 Then
                        '�t�B�[���h���ႢMSG
                        Throw New UsrDefException("�t�@�C��������������܂���B", _msgHd.getMSG("irregularFile"), txtPass2)
                    End If
                End If
            Finally
                tr2.close()
            End Try

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   ���׎R�σe�[�u���쐬
    '   �i�����T�v�j�t�@�C�����烏�[�N�e�[�u���쐬��A���׎R�σe�[�u�����쐬����
    '�@�@I�@�F�@prmStDt   �����J�n����
    '�@�@I�@�F�@prmPb     �v���O���X�o�[
    '�@�@I�@�F�@prmPass1  �捞�t�@�C���p�X�i�@�B�ʁj
    '�@�@O�@�F�@prmT61InsertCount     T61�o�^����
    '-------------------------------------------------------------------------------
    Private Sub insertHukayamazumiTable(ByVal prmStDt As Date, ByVal prmPb As UtilProgressBar, ByVal prmPass1 As String, ByRef prmT61InsertCount As Integer)

        Try
            Dim ht As Hashtable = New Hashtable()   'M21�i�[�p�n�b�V���e�[�u��
            Dim col() As String                     '�捞�t�@�C���̍s�����i�[����z��
            Dim cols() As String                    '�捞�t�@�C�����i�[����z��
            Dim hukakubun1 As Integer = 1           '���׋敪=1
            Dim hukakubun2 As Integer = 2           '���׋敪=2
            Dim hukakubun3 As Integer = 3           '���׋敪=3
            Dim lineCount As Integer = 0            '�捞�t�@�C�����̏����Ώۍs�i�J�E���^�[�j
            Dim lineTotal As Integer = 0            '�捞�t�@�C�����̏����Ώۍs�i�ő�l�j

            prmT61InsertCount = 0

            '-->2010.12.22 chg by takagi # 35
            ''�����N���̌����擾
            'Dim syoriDateTuki As String
            'syoriDateTuki = Trim(lblSyoriDate.Text.Split("/")(1))
            '�v��N���̌����擾
            Dim keikakuDateTuki As String = Trim(lblKeikakuDate.Text.Split("/")(1))
            '<--2010.12.22 chg by takagi #35

            '�n�b�V���e�[�u���ɐ��Y�\�̓}�X�^���i�[
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & " KIKAIMEI "    '�@�B��
            sql = sql & N & " ,KOUTEI "     '�H��
            sql = sql & N & " FROM M21SEISAN "

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            With ds.Tables(RS)
                For i As Integer = 0 To iRecCnt - 1
                    ht.Add(_db.rmNullStr(.Rows(i)("KIKAIMEI")), .Rows(i))
                Next
            End With

            '���[�N�e�[�u���̏�����
            sql = "DELETE FROM ZG720B_W10 WHERE UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "'"
            _db.executeDB(sql)

            '�@�B�ʂ̃t�@�C�����J��
            Dim tr1 As UtilMDL.Text.UtilTextReader = New UtilMDL.Text.UtilTextReader(prmPass1)
            tr1.open()

            Try
                '�捞�t�@�C����z��ɃZ�b�g
                Dim dic As Char() = {vbCr, vbLf}
                cols = tr1.readToEnd.TrimEnd(dic).Split(N)
                lineTotal = cols.Length

                '�v���O���X�o�[�ݒ�
                prmPb.oneStep = 10
                prmPb.maxVal = lineTotal

                Dim existsFlg As Boolean = False
                For i As Integer = 0 To lineTotal - 1
                    existsFlg = True
                    '�捞�t�@�C����1�s��z��Ɋi�[
                    col = Trim(cols(i)).Split(",")

                    Dim r As DataRow = TryCast(ht.Item(col(FL1COL_KOUTEI)), DataRow)

                    '���[�N�e�[�u���Ɏ捞�t�@�C����o�^
                    '-->2010.12.22 chg by takagi #35
                    'If _db.rmNullStr(col(FL1COL_KIBOUSYUTTAIBI)).Substring(0, 2).Equals(syoriDateTuki) Then
                    If _db.rmNullStr(col(FL1COL_KIBOUSYUTTAIBI)).Substring(0, 2).Equals(keikakuDateTuki) Then
                        '<--2010.12.22 chg by takagi #35

                        '��]�o�����̌��������N���̌��Ɠ����ꍇ�̂ݑΏۂƂ���
                        sql = "INSERT "
                        sql = sql & N & "INTO ZG720B_W10( "
                        sql = sql & N & " KOUTEI "         '�H��
                        sql = sql & N & ",KIKAIMEI "       '�@�B��
                        sql = sql & N & ",YAMADUMIMCH "    '�R�ύ��vMCH
                        sql = sql & N & ",DHAKKOUMCH "     '����`�[���s��MCH
                        sql = sql & N & ",DMIHAKKOUMCH "   '����`�[�����s��MCH
                        sql = sql & N & ",GZAIKOMCH "      '�����݌ɕ�MCH
                        sql = sql & N & ",YAMADUMIMH "     '�R��MH
                        sql = sql & N & ",DHAKKOUMH "      '����`�[���s��MH
                        sql = sql & N & ",DMIHAKKOUMH "    '����`�[�����s��MH
                        sql = sql & N & ",GZAIKOMH "       '�����݌ɕ�MH
                        sql = sql & N & ",UPDNAME "        '�[��ID
                        sql = sql & N & ",UPDDATE "        '�ŏI�X�V��
                        sql = sql & N & ")VALUES"
                        If r Is Nothing Then
                            sql = sql & N & "(NULL "
                        Else
                            sql = sql & N & "('" & _db.rmSQ(_db.rmNullStr(r("KOUTEI"))) & "' "
                        End If
                        sql = sql & N & ",'" & _db.rmSQ(col(FL1COL_KOUTEI)) & "' "
                        sql = sql & N & "," & CDec(col(FL1COL_MCH)) & " "
                        sql = sql & N & ",DECODE(" & col(FL1COL_FUKAKUBUN) & ", " & hukakubun1 & "," & CDec(col(FL1COL_MCH)) & ",0) "
                        sql = sql & N & ",DECODE(" & col(FL1COL_FUKAKUBUN) & ", " & hukakubun2 & "," & CDec(col(FL1COL_MCH)) & ",0) "
                        sql = sql & N & ",DECODE(" & col(FL1COL_FUKAKUBUN) & ", " & hukakubun3 & "," & CDec(col(FL1COL_MCH)) & ",0) "
                        sql = sql & N & "," & CDec(Trim(col(FL1COL_MH))) & " "
                        sql = sql & N & ",DECODE(" & col(FL1COL_FUKAKUBUN) & ", " & hukakubun1 & "," & CDec(col(FL1COL_MH)) & ",0) "
                        sql = sql & N & ",DECODE(" & col(FL1COL_FUKAKUBUN) & ", " & hukakubun2 & "," & CDec(col(FL1COL_MH)) & ",0) "
                        sql = sql & N & ",DECODE(" & col(FL1COL_FUKAKUBUN) & ", " & hukakubun3 & "," & CDec(col(FL1COL_MH)) & ",0) "
                        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "
                        sql = sql & N & ",to_date('" & Format(prmStDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS')) "
                        _db.executeDB(sql)
                    End If
                    prmPb.status = "���׎R�σe�[�u���쐬��  " & (lineCount + 1) & "/" & lineTotal & "��"
                    lineCount += 1
                    prmPb.value = lineCount
                Next

                '�Ώۂ����݂��Ȃ������ꍇ
                If existsFlg = False Then
                    Exit Sub
                End If

            Finally
                tr1.close()
            End Try

            'T61�̏�����
            sql = "DELETE FROM T61FUKA"
            _db.executeDB(sql)

            '���[�N�e�[�u������T61�ɓo�^
            sql = "INSERT INTO T61FUKA "
            sql = sql & N & " SELECT "
            sql = sql & N & "  KOUTEI "
            sql = sql & N & " ,KIKAIMEI "
            sql = sql & N & " ,SUM(YAMADUMIMCH) "
            sql = sql & N & " ,SUM(DHAKKOUMCH) "
            sql = sql & N & " ,SUM(DMIHAKKOUMCH) "
            sql = sql & N & " ,SUM(GZAIKOMCH) "
            sql = sql & N & " ,SUM(YAMADUMIMH) "
            sql = sql & N & " ,SUM(DHAKKOUMH) "
            sql = sql & N & " ,SUM(DMIHAKKOUMH) "
            sql = sql & N & " ,SUM(GZAIKOMH) "
            sql = sql & N & " ,UPDNAME "
            sql = sql & N & " ,UPDDATE "
            sql = sql & N & " FROM ZG720B_W10 "
            sql = sql & N & " WHERE UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
            sql = sql & N & " GROUP BY KOUTEI, KIKAIMEI,UPDNAME,UPDDATE "

            Dim prmRefAffectedRows As Integer 'DB�o�^����
            _db.executeDB(sql, prmRefAffectedRows)

            '�o�^������ێ�
            prmT61InsertCount = prmRefAffectedRows

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   ���׎R�ϖ��׃e�[�u���쐬
    '   �i�����T�v�j�t�@�C�����畉�׎R�ϖ��׃e�[�u�����쐬����
    '�@�@I�@�F�@prmPb     �v���O���X�o�[
    '�@�@I�@�F�@prmPass2  �捞�t�@�C���p�X�i���ׁj
    '�@�@O�@�F�@prmT62InsertCount     T62�o�^����
    '-------------------------------------------------------------------------------
    Private Sub insertHukayamazumiMeisaiTable(ByVal prmPb As UtilProgressBar, ByVal prmPass2 As String, ByRef prmT62InsertCount As Integer)

        Try
            Dim ht As Hashtable = New Hashtable()   'M21�i�[�p�n�b�V���e�[�u��
            Dim col() As String                     '�捞�t�@�C���̍s�����i�[����z��
            Dim cols() As String                    '�捞�t�@�C�����i�[����z��
            Dim lineCount As Integer = 0            '�捞�t�@�C�����̏����Ώۍs�i�J�E���^�[�j
            Dim lineTotal As Integer = 0            '�捞�t�@�C�����̏����Ώۍs�i�ő�l�j

            prmT62InsertCount = 0

            '-->2010.12.27 chg by takagi #56
            '�����N���̔N�A�����擾
            'Dim syoriDateNen As String
            'Dim syoriDateTuki As String
            'syoriDateNen = Trim(lblSyoriDate.Text.Split("/")(0))
            'syoriDateTuki = Trim(lblSyoriDate.Text.Split("/")(1))
            '�v��N���̔N�A�����擾
            Dim keikakuDateNen As String
            Dim keikakuDateTuki As String
            keikakuDateNen = Trim(lblKeikakuDate.Text.Split("/")(0))
            keikakuDateTuki = Trim(lblKeikakuDate.Text.Split("/")(1))
            '<--2010.12.27 chg by takagi #56

            '�n�b�V���e�[�u���ɐ��Y�\�̓}�X�^���i�[
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & " KIKAIMEI "    '�@�B��
            sql = sql & N & " ,KOUTEI "     '�H��
            sql = sql & N & " FROM M21SEISAN "

            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            With ds.Tables(RS)
                For i As Integer = 0 To iRecCnt - 1
                    ht.Add(_db.rmNullStr(.Rows(i)("KIKAIMEI")), .Rows(i))
                Next
            End With

            'T62�̏�����
            sql = "DELETE FROM T62FUKAMEISAI"
            _db.executeDB(sql)

            '���ׂ̃t�@�C�����J��
            Dim tr2 As UtilMDL.Text.UtilTextReader = New UtilMDL.Text.UtilTextReader(prmPass2)
            tr2.open()

            Try
                '�捞�t�@�C����z��ɃZ�b�g
                Dim dic As Char() = {vbCr, vbLf}
                cols = tr2.readToEnd.TrimEnd(dic).Split(N)
                lineTotal = cols.Length

                '�v���O���X�o�[�ݒ�
                prmPb.oneStep = 10
                prmPb.maxVal = lineTotal

                'T62�Ɏ捞�t�@�C����o�^
                For i As Integer = 0 To lineTotal - 1

                    '�捞�t�@�C����1�s��z��Ɋi�[
                    col = Trim(cols(i)).Split(",")
                    Dim r As DataRow = TryCast(ht.Item(col(FL2COL_KOUTEI)), DataRow)

                    '-->2010.12.27 chg by takagi #56
                    'If _db.rmNullStr(col(FL2COL_KIBOUSYUTTAIBI)).Substring(0, 2).Equals(syoriDateTuki) Then
                    If _db.rmNullStr(col(FL2COL_KIBOUSYUTTAIBI)).Substring(0, 2).Equals(keikakuDateTuki) Then
                        '<--2010.12.27 chg by takagi #56
                        '��]�o�����̌��������N���̌��Ɠ����ꍇ�̂ݑΏۂƂ���
                        sql = "INSERT "
                        sql = sql & N & "INTO T62FUKAMEISAI( "
                        '' 2011/01/25 CHG-S Sugano #91
                        'sql = sql & N & " KIKAIMEI "       '�H��
                        'sql = sql & N & ",NAME "           '�@�B��
                        sql = sql & N & " KOUTEI "         '�H��
                        sql = sql & N & ",KIKAIMEI "       '�@�B��
                        '' 2011/01/25 CHG-E Sugano #91
                        sql = sql & N & ",FUKAKBN "        '���׋敪
                        '' 2011/01/25 CHG-S Sugano #91
                        'sql = sql & N & ",SEISAKUKBN "     '����敪
                        sql = sql & N & ",TEHAIKBN "       '��z�敪
                        '' 2011/01/25 CHG-E Sugano #91
                        sql = sql & N & ",SEIBAN "         '����
                        sql = sql & N & ",HINMEI "         '�i��
                        sql = sql & N & ",SYUTTAIBI "      '��]�o����
                        sql = sql & N & ",TYAKUSYUBI "     '�H�������
                        sql = sql & N & ",MCH "            'MCH
                        sql = sql & N & ",MH "             'MH
                        sql = sql & N & ",TEHAINO "        '��zNO
                        sql = sql & N & ",UPDNAME "        '�[��ID
                        sql = sql & N & ",UPDDATE "        '�ŏI�X�V��
                        sql = sql & N & ")VALUES"
                        If r Is Nothing Then
                            sql = sql & N & "(NULL "
                        Else
                            sql = sql & N & "('" & _db.rmSQ(_db.rmNullStr(r("KOUTEI"))) & "' "
                        End If
                        sql = sql & N & ",'" & _db.rmSQ(_db.rmNullStr(col(FL2COL_KOUTEI))) & "' "
                        sql = sql & N & ",'" & _db.rmNullStr(col(FL2COL_FUKAKUBUN)) & "' "
                        '' 2011/01/25 CHG-S Sugano #91
                        'sql = sql & N & ",'" & _db.rmSQ(_db.rmNullStr(col(FL2COL_SEISAKUKUBUN))) & "' "
                        sql = sql & N & ",'" & _db.rmSQ(_db.rmNullStr(col(FL2COL_TEHAIKUBUN))) & "' "
                        '' 2011/01/25 CHG-E Sugano #91
                        sql = sql & N & ",'" & _db.rmSQ(_db.rmNullStr(col(FL2COL_SEIBAN))) & "' "
                        sql = sql & N & ",'" & _db.rmSQ(Trim(_db.rmNullStr(col(FL2COL_HINMEI)).Replace(""""c, ""))) & "' "
                        '-->2010.12.27 chg by takagi #61
                        ''-->2010.12.27 chg by takagi #56
                        ''sql = sql & N & ",'" & syoriDateNen & _db.rmNullStr(col(FL2COL_KIBOUSYUTTAIBI)) & "' "
                        'sql = sql & N & ",'" & keikakuDateNen & _db.rmNullStr(col(FL2COL_KIBOUSYUTTAIBI)) & "' "
                        ''<--2010.12.27 chg by takagi #56
                        'If String.Empty.Equals(_db.rmNullStr(col(FL2COL_KOUTEITYAKUSYUBI))) Then
                        '    '�H�����������̏ꍇ�A��œo�^
                        '    sql = sql & N & ",'' "
                        'Else
                        '    '�H�����������ł͂Ȃ��ꍇ�A��]�o�����̔N�Ɣ�r���ĔN��⊮���o�^
                        '    '-->2010.12.27 chg by takagi #56
                        '    'If _db.rmNullStr(col(FL2COL_KIBOUSYUTTAIBI)).Substring(0, 2) >= _db.rmNullStr(col(FL2COL_KOUTEITYAKUSYUBI)).Substring(0, 2) Then
                        '    '    sql = sql & N & ",'" & syoriDateNen & _db.rmNullStr(col(FL2COL_KOUTEITYAKUSYUBI)) & "' "
                        '    'Else
                        '    '    sql = sql & N & ",'" & CStr(CInt(syoriDateNen) - 1) & _db.rmNullStr(col(FL2COL_KOUTEITYAKUSYUBI)) & "' "
                        '    'End If
                        '    If _db.rmNullStr(col(FL2COL_KIBOUSYUTTAIBI)).Substring(0, 2) >= _db.rmNullStr(col(FL2COL_KOUTEITYAKUSYUBI)).Substring(0, 2) Then
                        '        sql = sql & N & ",'" & keikakuDateNen & _db.rmNullStr(col(FL2COL_KOUTEITYAKUSYUBI)) & "' "
                        '    Else
                        '        sql = sql & N & ",'" & CStr(CInt(keikakuDateNen) - 1) & _db.rmNullStr(col(FL2COL_KOUTEITYAKUSYUBI)) & "' "
                        '    End If
                        '    '<--2010.12.27 chg by takagi #56
                        'End If
                        If "0000".Equals(_db.rmNullStr(col(FL2COL_KIBOUSYUTTAIBI))) Then
                            sql = sql & N & ",null "
                        Else
                            sql = sql & N & ",'" & keikakuDateNen & _db.rmNullStr(col(FL2COL_KIBOUSYUTTAIBI)) & "' "
                        End If
                        If String.Empty.Equals(_db.rmNullStr(col(FL2COL_KOUTEITYAKUSYUBI))) Or _
                           "0000".Equals(_db.rmNullStr(col(FL2COL_KOUTEITYAKUSYUBI))) Then
                            '�H�����������̏ꍇ�A��œo�^
                            sql = sql & N & ",'' "
                        Else
                            '�H�����������ł͂Ȃ��ꍇ�A��]�o�����͕K�������Ă���O��A���̔N�Ɣ�r���ĔN��⊮���o�^()
                            If _db.rmNullStr(col(FL2COL_KIBOUSYUTTAIBI)).Substring(0, 2) >= _db.rmNullStr(col(FL2COL_KOUTEITYAKUSYUBI)).Substring(0, 2) Then
                                sql = sql & N & ",'" & keikakuDateNen & _db.rmNullStr(col(FL2COL_KOUTEITYAKUSYUBI)) & "' "
                            Else
                                sql = sql & N & ",'" & CStr(CInt(keikakuDateNen) - 1) & _db.rmNullStr(col(FL2COL_KOUTEITYAKUSYUBI)) & "' "
                            End If
                        End If
                        '<--2010.12.27 chg by takagi #61
                        sql = sql & N & "," & CDec(_db.rmNullStr(col(FL2COL_MCH)))
                        sql = sql & N & "," & CDec(_db.rmNullStr(col(FL2COL_MH)))
                        sql = sql & N & ",'" & Trim(_db.rmNullStr(col(FL2COL_TEHAINO))) & "' "
                        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "
                        sql = sql & N & ",sysdate) "

                        _db.executeDB(sql)
                        prmT62InsertCount += 1

                    End If
                    prmPb.status = "���׎R�ϖ��׃e�[�u���쐬��  " & (lineCount + 1) & "/" & lineTotal & "��"
                    lineCount += 1
                    prmPb.value = lineCount
                Next

                '�Ώۂ����݂��Ȃ������ꍇ
                If prmT62InsertCount = 0 Then
                    Exit Sub
                End If

            Finally
                tr2.close()
            End Try

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   ���s�����쐬
    '   �i�����T�v�j�m�菈���p�̎��s�������쐬����
    '�@�@I�@�F�@prmStDt             �����J�n����
    '�@�@I�@�F�@prmFinDt            �����I������
    '�@�@I�@�F�@prmT61InsertCount   T61�o�^����
    '�@�@I�@�F�@prmT62InsertCount   T62�o�^����
    '�@�@I�@�F�@prmPass1   �t�@�C���捞�p�X�i�@�B�ʁj
    '�@�@I�@�F�@prmPass2   �t�@�C���捞�p�X�i���ׁj
    '-------------------------------------------------------------------------------
    Private Sub insertRireki(ByVal prmStDt As Date, ByVal prmFinDt As Date, ByVal prmT61InsertCount As Integer, _
                            ByVal prmT62InsertCount As Integer, ByVal prmPass1 As String, ByVal prmPass2 As String)

        Try
            Dim sql As String = ""
            sql = sql & N & "INSERT INTO T91RIREKI "
            sql = sql & N & "( "
            sql = sql & N & " PGID "       '�@�\ID
            sql = sql & N & ",SNENGETU "   '�����N��
            sql = sql & N & ",KNENGETU "   '�v��N��
            sql = sql & N & ",SDATESTART " '�����J�n����
            sql = sql & N & ",SDATEEND "   '�����I������
            sql = sql & N & ",KENNSU1 "    '����1
            sql = sql & N & ",KENNSU2 "    '����2
            sql = sql & N & ",NAME1 "      '����1
            sql = sql & N & ",NAME2 "      '����2
            sql = sql & N & ",UPDNAME "    '�[��ID
            sql = sql & N & ",UPDDATE "    '�ŏI�X�V��
            sql = sql & N & ")VALUES( "
            sql = sql & N & " '" & PGID & "' "                                                                      '�@�\ID
            sql = sql & N & ",'" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "' "                              '�����N��
            sql = sql & N & ",'" & _db.rmSQ(lblKeikakuDate.Text.Replace("/", "")) & "' "                            '�v��N��
            sql = sql & N & ",TO_DATE('" & Format(prmStDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '�����J�n����
            sql = sql & N & ",TO_DATE('" & Format(prmFinDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "  '�����I������
            sql = sql & N & ", " & prmT61InsertCount & " "                                                          '����1
            sql = sql & N & ", " & prmT62InsertCount & " "                                                          '����2
            sql = sql & N & ",'" & _db.rmSQ(prmPass1) & "' "                                                        '����1
            sql = sql & N & ",'" & _db.rmSQ(prmPass2) & "' "                                                        '����2
            sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "                                     '�[��ID
            sql = sql & N & ",TO_DATE('" & Format(prmFinDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "  '�ŏI�X�V��
            sql = sql & N & ") "
            _db.executeDB(sql)

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
    Private Sub txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPass1.KeyPress, _
                                                                                                                txtPass2.KeyPress
        Try
            '���̃R���g���[���ֈړ�����
            UtilClass.moveNextFocus(Me, e)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�R���g���[���L�[�t�H�[�J�X�擾�C�x���g
    '�@(�����T�v)�t�H�[�J�X�擾���A�S�I����Ԃɂ���
    '-------------------------------------------------------------------------------
    Private Sub txt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPass1.GotFocus, _
                                                                                          txtPass2.GotFocus
        Try
            '�S�I����Ԃɂ���
            UtilClass.selAll(sender)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�H�����擾���̃e�L�X�g�o��
    '�@(�����T�v)M21����擾�ł��Ȃ��@�B���L�����e�L�X�g�o�͂���
    '�@�@O�@�F�@prmNotGetKikaimeiCount          M21����擾�ł��Ȃ������@�B���L������
    '-------------------------------------------------------------------------------
    Private Sub printNotGetKikaimei(ByRef prmNotGetKikaimeiCount As Integer)

        Try
            prmNotGetKikaimeiCount = 0

            Dim sql As String = ""
            sql = ""
            sql = "SELECT KIKAIMEI "
            sql = sql & N & "FROM (SELECT KIKAIMEI "
            sql = sql & N & "FROM T61FUKA "
            sql = sql & N & "WHERE KOUTEI IS NULL "
            sql = sql & N & "GROUP BY KIKAIMEI "
            sql = sql & N & "UNION "
            '' 2011/01/25 CHG-S Sugano #91
            'sql = sql & N & "SELECT NAME KIKAIMEI "
            'sql = sql & N & "FROM T62FUKAMEISAI "
            'sql = sql & N & "WHERE KIKAIMEI IS NULL "
            'sql = sql & N & "GROUP BY NAME) A "
            sql = sql & N & "SELECT KIKAIMEI "
            sql = sql & N & "FROM T62FUKAMEISAI "
            sql = sql & N & "WHERE KOUTEI IS NULL "
            sql = sql & N & "GROUP BY KIKAIMEI) A "
            '' 2011/01/25 CHG-E Sugano #91
            sql = sql & N & "ORDER BY KIKAIMEI "
            Dim ds As DataSet = _db.selectDB(sql, RS, prmNotGetKikaimeiCount)

            If prmNotGetKikaimeiCount > 0 Then
                Dim logBuf As System.Text.StringBuilder = New System.Text.StringBuilder
                logBuf.Append(Format(Now(), "yyyy/MM/dd HH:mm:ss") & "���s" & N)
                logBuf.Append("==========================================================" & N)
                logBuf.Append("�����׎R�σf�[�^�捞�����o�͏��" & N)
                logBuf.Append("  ���Y�\�̓}�X�^���o�^�@�B���L���i�H���̎擾���s���Ȃ������@�B���L���j" & N)
                logBuf.Append("----------------------------------------------------------" & N)
                For i As Integer = 0 To prmNotGetKikaimeiCount - 1
                    logBuf.Append(_db.rmNullStr(ds.Tables(RS).Rows(i)("KIKAIMEI")) & N)
                Next
                logBuf.Append("==========================================================")
                Dim tw As UtilMDL.Text.UtilTextWriter = New UtilMDL.Text.UtilTextWriter(UtilClass.getAppPath(StartUp.assembly) & "\" & IMP_LOG_NM)
                tw.open(False)
                Try
                    tw.writeLine(logBuf.ToString)
                Finally
                    tw.close()
                End Try

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