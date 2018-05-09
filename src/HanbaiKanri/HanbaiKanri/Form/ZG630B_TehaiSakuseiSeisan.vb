'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���j��z�f�[�^�쐬�w��(���Y�Ǘ�)
'    �i�t�H�[��ID�jZG630B_TehaiSakuseiSeisan
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ����        2010/11/10                 �V�K              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Public Class ZG630B_TehaiSakuseiSeisan
    Inherits System.Windows.Forms.Form

    '-------------------------------------------------------------------------------
    '   �\���̒�`
    '-------------------------------------------------------------------------------
    Private Structure fixedStringType
        Dim var As String
        Dim size As Integer
    End Structure
    'OCR��z�p�f�[�^(�s��1)��`�@(REC SIZE=82)
    Private Structure OCRrec1_def
        Dim T001 As fixedStringType '* 5       '�`������R�[�h
        Dim T006 As fixedStringType '* 1       '�s��
        Dim T007 As fixedStringType '* 1       '�����敪
        Dim T008 As fixedStringType '* 5       '��z��
        Dim T013 As fixedStringType '* 1       '��z�敪
        Dim T014 As fixedStringType '* 1       '����敪
        Dim T015 As fixedStringType '* 13      '�i���R�[�h
        Dim T028 As fixedStringType '* 1       '�݌v�t���L��
        Dim T029 As fixedStringType '* 4       '��]�o����
        Dim T033 As fixedStringType '* 1       '�W�J�敪
        Dim T034 As fixedStringType '* 6       '�����W�J�w��H��
        Dim T040 As fixedStringType '* 1       '���H���v�Z�敪
        Dim T041 As fixedStringType '* 1       '����L��
        Dim T042 As fixedStringType '* 4       '����\���
        Dim T046 As fixedStringType '* 1       '���я�
        Dim T047 As fixedStringType '* 1       '�P���敪
        Dim T048 As fixedStringType '* 7       '��z����
        Dim T055 As fixedStringType '* 6       '�[��
        Dim T061 As fixedStringType '* 1       '�i�������敪
        Dim T062 As fixedStringType '* 2       '�����]���i�P���j
        Dim T064 As fixedStringType '* 3       '�����]���i���b�g�j
        Dim T067 As fixedStringType '* 2       '����]���i�P���j
        Dim T069 As fixedStringType '* 3       '����]���i���b�g�j
        Dim T072 As fixedStringType '* 2       '�w��Ќ��]���i�P���j
        Dim T074 As fixedStringType '* 3       '�w��Ќ��]���i���b�g���j
        Dim T077 As fixedStringType '* 4       '��
        Dim T081 As fixedStringType '* 2       'CRLF
        Overrides Function ToString() As String
            Return T001.var & T006.var & T007.var & T008.var & T013.var & T014.var & T015.var & T028.var _
                    & T029.var & T033.var & T034.var & T040.var & T041.var & T042.var & T046.var & T047.var _
                    & T048.var & T055.var & T061.var & T062.var & T064.var & T067.var & T069.var & T072.var _
                    & T074.var & T077.var & T081.var
        End Function
    End Structure

    'OCR��z�p�f�[�^(�s��2)��`�@(REC SIZE=82)
    Private Structure OCRrec2_def
        Dim T001 As fixedStringType '* 5       '�`������R�[�h
        Dim T006 As fixedStringType '* 1       '�s��
        Dim T007 As fixedStringType '* 20      '�d�l���ԍ�
        Dim T027 As fixedStringType '* 20      '������
        Dim T047 As fixedStringType '* 34      '��
        Dim T081 As fixedStringType '* 2       'CRLF
        Overrides Function ToString() As String
            Return T001.var & T006.var & T007.var & T027.var & T047.var & T081.var
        End Function
    End Structure

    'OCR��z�p�f�[�^(�s��3)��`�@(REC SIZE=82)
    Private Structure OCRrec3_def
        Dim T001 As fixedStringType '* 5       '�`������R�[�h
        Dim T006 As fixedStringType '* 1       '�s��
        Dim T007 As fixedStringType '* 66      '���L����
        Dim T073 As fixedStringType '* 8       '��
        Dim T081 As fixedStringType '* 2       'CRLF
        Overrides Function ToString() As String
            Return T001.var & T006.var & T007.var & T073.var & T081.var
        End Function
    End Structure

    'OCR��z�p�f�[�^(�s��4)��`�@(REC SIZE=82)
    Private Structure OCRrec4_def
        Dim T001 As fixedStringType '* 5       '�`������R�[�h
        Dim T006 As fixedStringType '* 1       '�s��
        Dim T007 As fixedStringType '* 5       '�P��_1
        Dim T012 As fixedStringType '* 4       '��_1
        Dim T016 As fixedStringType '* 6       '���g�R�[�h_1
        Dim T022 As fixedStringType '* 2       '��敪_1
        Dim T024 As fixedStringType '* 5       '�P��_2
        Dim T029 As fixedStringType '* 4       '��_2
        Dim T033 As fixedStringType '* 6       '���g�R�[�h_2
        Dim T039 As fixedStringType '* 2       '��敪_2
        Dim T041 As fixedStringType '* 5       '�P��_3
        Dim T046 As fixedStringType '* 4       '��_3
        Dim T050 As fixedStringType '* 6       '���g�R�[�h_3
        Dim T056 As fixedStringType '* 2       '��敪_3
        Dim T058 As fixedStringType '* 5       '�P��_4
        Dim T063 As fixedStringType '* 4       '��_4
        Dim T067 As fixedStringType '* 6       '���g�R�[�h_4
        Dim T073 As fixedStringType '* 2       '��敪_4
        Dim T075 As fixedStringType '* 6       '��
        Dim T081 As fixedStringType '* 2       'CRLF
        Overrides Function ToString() As String
            Return T001.var & T006.var & T007.var & T012.var & T016.var & T022.var & T024.var & T029.var _
                    & T033.var & T039.var & T041.var & T046.var & T050.var & T056.var & T058.var & T063.var _
                    & T067.var & T073.var & T075.var & T081.var
        End Function
    End Structure

    'OCR��z�p�f�[�^(�s��5)��`�@(REC SIZE=82)
    Private Structure OCRrec5_def
        Dim T001 As fixedStringType '* 5       '�`������R�[�h
        Dim T006 As fixedStringType '* 1       '�s��
        Dim T007 As fixedStringType '* 5       '�P��_5
        Dim T012 As fixedStringType '* 4       '��_5
        Dim T016 As fixedStringType '* 6       '���g�R�[�h_5
        Dim T022 As fixedStringType '* 2       '��敪_5
        Dim T024 As fixedStringType '* 5       '�P��_6
        Dim T029 As fixedStringType '* 4       '��_6
        Dim T033 As fixedStringType '* 6       '���g�R�[�h_6
        Dim T039 As fixedStringType '* 2       '��敪_6
        Dim T041 As fixedStringType '* 5       '�P��_7
        Dim T046 As fixedStringType '* 4       '��_7
        Dim T050 As fixedStringType '* 6       '���g�R�[�h_7
        Dim T056 As fixedStringType '* 2       '��敪_7
        Dim T058 As fixedStringType '* 5       '�P��_8
        Dim T063 As fixedStringType '* 4       '��_8
        Dim T067 As fixedStringType '* 6       '���g�R�[�h_8
        Dim T073 As fixedStringType '* 2       '��敪_8
        Dim T075 As fixedStringType '* 6       '��
        Dim T081 As fixedStringType '* 2       'CRLF
        Overrides Function ToString() As String
            Return T001.var & T006.var & T007.var & T012.var & T016.var & T022.var & T024.var & T029.var _
                    & T033.var & T039.var & T041.var & T046.var & T050.var & T056.var & T058.var & T063.var _
                    & T067.var & T073.var & T075.var & T081.var
        End Function
    End Structure


    'OCR��z�p�f�[�^(�s��6)��`�@(REC SIZE=82)
    Private Structure OCRrec6_def
        Dim T001 As fixedStringType '* 5       '�`������R�[�h
        Dim T006 As fixedStringType '* 1       '�s��
        Dim T007 As fixedStringType '* 6       '�W���H���R�[�h
        Dim T013 As fixedStringType '* 6       '�i��ցj�H���R�[�h
        Dim T019 As fixedStringType '* 3       '�i��ցj�H������
        Dim T022 As fixedStringType '* 2       '�i��ցj���
        Dim T024 As fixedStringType '* 2       '�i��ցj�i��
        Dim T026 As fixedStringType '* 6       '�i��ցj��o����
        Dim T032 As fixedStringType '* 4       '����ذٗ]���E����
        Dim T036 As fixedStringType '* 4       '����ذٗ]���E׽�
        Dim T040 As fixedStringType '* 4       '׽�ذٗ]���E����
        Dim T044 As fixedStringType '* 4       '׽�ذٗ]���E׽�
        Dim T048 As fixedStringType '* 5       '�ő努�撷
        Dim T053 As fixedStringType '* 1       '�v�Z����
        Dim T054 As fixedStringType '* 27      '��
        Dim T081 As fixedStringType '* 2       'CRLF
        Overrides Function ToString() As String
            Return T001.var & T006.var & T007.var & T013.var & T019.var & T022.var & T024.var & T026.var _
                    & T032.var & T036.var & T040.var & T044.var & T048.var & T053.var & T054.var & T081.var
        End Function
    End Structure

    '-------------------------------------------------------------------------------
    '   �萔��`
    '-------------------------------------------------------------------------------
    Private Const RS As String = "RecSet"                       '�f�[�^�Z�b�g�e�[�u����
    Private Const N As String = ControlChars.NewLine            '���s����
    Private Const PGID As String = "ZG630B"

    '-------------------- << OCR��z�p�f�[�^��� >> ----------------------
    Private Const sSEND_CTRL_CD = "PB110"   '�`������R�[�h
    Private Const sGYO_1 = "1"              '�s��F1
    Private Const sGYO_2 = "2"              '�s��F2
    Private Const sGYO_3 = "3"              '�s��F3
    Private Const sGYO_4 = "4"              '�s��F4
    Private Const sGYO_5 = "5"              '�s��F5
    Private Const sGYO_6 = "6"              '�s��F6

    '-------------------------------------------------------------------------------
    '   �ϐ���`
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu
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
        _updFlg = prmUpdFlg                                                 '�X�V��
    End Sub

    '-------------------------------------------------------------------------------
    '�@�t�H�[�����[�h�C�x���g
    '-------------------------------------------------------------------------------
    Private Sub ZG630B_TehaiSakuseiSeisan_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            '�`��֌W�̐ݒ�
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           '�T�C�Y���ύX���ꂽ�Ƃ��ɁA�R���g���[�����R���g���[�����̂��ĕ`�悷�邩�ǂ����������l��ݒ�
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '�`��̓o�b�t�@�Ŏ��s����A������ɁA���ʂ���ʂɏo�͂����悤�ݒ�
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  '�R���g���[���́A��ʂɒ��ڂł͂Ȃ��A�܂��o�b�t�@�ɕ`�悳��܂��B����ɂ��A�������}���邱�Ƃ��ł��܂��B
            Me.SetStyle(ControlStyles.UserPaint, True)              '�R���g���[���́A�I�y���[�e�B���O �V�X�e���ɂ���Ăł͂Ȃ��A�Ǝ��ɕ`�悳���悤�ݒ�
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   '�R���g���[���̓E�B���h�E ���b�Z�[�W WM_ERASEBKGND �𖳎�����悤�ɐݒ�

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
    '   �i�����T�v�j��ʓ��̊e���ڂ������\������
    '   �����̓p�����^  �F�Ȃ�
    '   ���o�̓p�����^  �F�Ȃ�
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub initForm()
        Try

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
            sql = sql & N & "    ,(NAME1 || NAME2 || NAME3 || NAME4) mypath "
            sql = sql & N & "    from T91RIREKI "
            sql = sql & N & "    where PGID     = '" & PGID & "' "
            sql = sql & N & ") "
            sql = sql & N & "where RNUM = 1 "
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)
            If iRecCnt < 1 Then
                '�����Ȃ�
                lblJikkouDate.Text = ZC110M_Menu.NON_EXECUTE
                lblZenkaiKensu.Text = ""
                txtPastPass.Text = ""
            Else
                '��������
                lblJikkouDate.Text = _db.rmNullDate(ds.Tables(RS).Rows(0)("SDATEEND"))
                lblZenkaiKensu.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU1")), "#,##0")
                txtPastPass.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("mypath")).Trim
            End If

            '������s���̕\��
            '2011/01/28 chg start Sugawara #95
            'lblKonkaiKensu.Text = Format(_db.rmNullInt(_db.selectDB("select count(*) CNT from T51TEHAI ", RS).Tables(RS).Rows(0)("CNT")), "#,##0")
            lblKonkaiKensu.Text = Format(_db.rmNullInt(_db.selectDB("select count(*) CNT from T51TEHAI where GAI_FLG is null ", RS).Tables(RS).Rows(0)("CNT")), "#,##0")
            '2011/01/28 chg end Sugawara #95

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

    '------------------------------------------------------------------------------------------------------
    '�@���s�{�^�������C�x���g
    '------------------------------------------------------------------------------------------------------
    Private Sub btnJikkou_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJikkou.Click
        Try
            '���s�m�F�i���s���܂��B��낵���ł����H�j
            'Dim rtn As DialogResult = _msgHd.dspMSG("confirmRun")
            'If rtn = Windows.Forms.DialogResult.No Or rtn = Windows.Forms.DialogResult.Cancel Then Exit Sub

            Dim myPath As String = ""
            Dim myFile As String = ""
            Call UtilClass.dividePathAndFile(txtPastPass.Text, myPath, myFile)
            Dim executePath As String = UtilMDL.CommonDialog.UtilCmnDlgHandler.saveFileDialog(myPath, "DPB12000", , "OCR��z�f�[�^�쐬��")
            If String.IsNullOrEmpty(executePath) Then Exit Sub

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
                    pb.jobName = "�n�b�q��z�f�[�^(DPB12000)���쐬���Ă��܂��B"
                    pb.oneStep = 1

                    pb.status = "�쐬��"
                    Dim outputCnt As Integer = 0
                    Call outputOcrTehaiDate(executePath, outputCnt, pb)

                    _db.beginTran()
                    Try
                        pb.status = "�X�e�[�^�X�ύX���E�E�E"
                        ed = Now                    '�����I������
                        _parentForm.updateSeigyoTbl(PGID, True, st, ed)

                        pb.status = "���s�����쐬"
                        Call insertRireki(executePath, st, ed)                                  '2-1 ���s�����i�[
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
    '   ���s�����쐬
    '   �i�����T�v�j�m�菈���p�̎��s�������쐬����
    '   �����̓p�����^  �FprmStDt   �����J�n����
    '                     prmEdDt   �����I������
    '   ���o�̓p�����^  �F�Ȃ�
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub insertRireki(ByVal executePath As String, ByVal prmStDt As Date, ByVal prmEdDt As Date)

        Dim path1 As String = ""
        Dim path2 As String = ""
        Dim path3 As String = ""
        Dim path4 As String = ""
        Dim targetPath As String = executePath

        If targetPath.Length < 128 Then
            path1 = targetPath
        Else
            path1 = targetPath.Substring(0, 128)
            targetPath = targetPath.Substring(128)

            If targetPath.Length < 128 Then
                path2 = targetPath
            Else
                path2 = targetPath.Substring(0, 128)
                targetPath = targetPath.Substring(128)

                If targetPath.Length < 128 Then
                    path3 = targetPath
                Else
                    path3 = targetPath.Substring(0, 128)
                    targetPath = targetPath.Substring(128)

                    If targetPath.Length < 128 Then
                        path4 = targetPath
                    Else
                        path4 = targetPath.Substring(0, 128)
                        targetPath = targetPath.Substring(128)
                    End If
                End If
            End If
        End If

        Dim sql As String = ""
        sql = sql & N & "insert into T91RIREKI "
        sql = sql & N & "( "
        sql = sql & N & " SNENGETU "   '�����N��
        sql = sql & N & ",KNENGETU "   '�v��N��
        sql = sql & N & ",PGID "       '�@�\ID
        sql = sql & N & ",SDATESTART " '�����J�n����
        sql = sql & N & ",SDATEEND "   '�����I������
        sql = sql & N & ",KENNSU1 "    '���s����
        sql = sql & N & ",NAME1 "      '�p�X
        sql = sql & N & ",NAME2 "      '�p�X
        sql = sql & N & ",NAME3 "      '�p�X
        sql = sql & N & ",NAME4 "      '�p�X
        sql = sql & N & ",UPDNAME "    '�ŏI�X�V��
        sql = sql & N & ",UPDDATE "    '�ŏI�X�V��
        sql = sql & N & ")values( "
        sql = sql & N & "  '" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "' "                             '�����N��
        sql = sql & N & ", '" & _db.rmSQ(lblKeikakuDate.Text.Replace("/", "")) & "' "                           '�v��N��
        sql = sql & N & ", '" & _db.rmSQ(PGID) & "' "                                                           '�@�\ID
        sql = sql & N & ",to_date('" & Format(prmStDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '�����J�n����
        sql = sql & N & ",to_date('" & Format(prmEdDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '�����I������
        sql = sql & N & ", " & CLng(lblKonkaiKensu.Text) & " "                                                  '���͌���
        sql = sql & N & ", " & impDtForStr(path1) & " "                                                            '�p�X
        sql = sql & N & ", " & impDtForStr(path2) & " "                                                            '�p�X
        sql = sql & N & ", " & impDtForStr(path3) & " "                                                            '�p�X
        sql = sql & N & ", " & impDtForStr(path4) & " "                                                            '�p�X
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "                                     '�ŏI�X�V��
        sql = sql & N & ",sysdate "                                                                             '�ŏI�X�V��
        sql = sql & N & ") "
        _db.executeDB(sql)

    End Sub

    Private Function impDtForStr(ByVal prmVal As String) As String
        If "".Equals(prmVal) Then
            Return "NULL"
        Else
            Return "'" & prmVal & "'"
        End If
    End Function

    Private Function initialyzerOCRrec1() As OCRrec1_def
        Dim ret As OCRrec1_def = New OCRrec1_def
        ret.T001.size = 5       '�`������R�[�h
        ret.T006.size = 1       '�s��
        ret.T007.size = 1       '�����敪
        ret.T008.size = 5       '��z��
        ret.T013.size = 1       '��z�敪
        ret.T014.size = 1       '����敪
        ret.T015.size = 13      '�i���R�[�h
        ret.T028.size = 1       '�݌v�t���L��
        ret.T029.size = 4       '��]�o����
        ret.T033.size = 1       '�W�J�敪
        ret.T034.size = 6       '�����W�J�w��H��
        ret.T040.size = 1       '���H���v�Z�敪
        ret.T041.size = 1       '����L��
        ret.T042.size = 4       '����\���
        ret.T046.size = 1       '���я�
        ret.T047.size = 1       '�P���敪
        ret.T048.size = 7       '��z����
        ret.T055.size = 6       '�[��
        ret.T061.size = 1       '�i�������敪
        ret.T062.size = 2       '�����]���i�P���j
        ret.T064.size = 3       '�����]���i���b�g�j
        ret.T067.size = 2       '����]���i�P���j
        ret.T069.size = 3       '����]���i���b�g�j
        ret.T072.size = 2       '�w��Ќ��]���i�P���j
        ret.T074.size = 3       '�w��Ќ��]���i���b�g���j
        ret.T077.size = 4       '��
        ret.T081.size = 2       'CRLF
        Return ret
    End Function

    Private Function initialyzerOCRrec2() As OCRrec2_def
        Dim ret As OCRrec2_def = New OCRrec2_def
        ret.T001.size = 5       '�`������R�[�h
        ret.T006.size = 1       '�s��
        ret.T007.size = 20      '�d�l���ԍ�
        ret.T027.size = 20      '������
        ret.T047.size = 34      '��
        ret.T081.size = 2       'CRLF
        Return ret
    End Function

    Private Function initialyzerOCRrec3() As OCRrec3_def
        Dim ret As OCRrec3_def = New OCRrec3_def
        ret.T001.size = 5       '�`������R�[�h
        ret.T006.size = 1       '�s��
        ret.T007.size = 66      '���L����
        ret.T073.size = 8       '��
        ret.T081.size = 2       'CRLF
        Return ret
    End Function

    Private Function initialyzerOCRrec4() As OCRrec4_def
        Dim ret As OCRrec4_def = New OCRrec4_def
        ret.T001.size = 5       '�`������R�[�h
        ret.T006.size = 1       '�s��
        ret.T007.size = 5       '�P��_1
        ret.T012.size = 4       '��_1
        ret.T016.size = 6       '���g�R�[�h_1
        ret.T022.size = 2       '��敪_1
        ret.T024.size = 5       '�P��_2
        ret.T029.size = 4       '��_2
        ret.T033.size = 6       '���g�R�[�h_2
        ret.T039.size = 2       '��敪_2
        ret.T041.size = 5       '�P��_3
        ret.T046.size = 4       '��_3
        ret.T050.size = 6       '���g�R�[�h_3
        ret.T056.size = 2       '��敪_3
        ret.T058.size = 5       '�P��_4
        ret.T063.size = 4       '��_4
        ret.T067.size = 6       '���g�R�[�h_4
        ret.T073.size = 2       '��敪_4
        ret.T075.size = 6       '��
        ret.T081.size = 2       'CRLF
        Return ret
    End Function

    Private Function initialyzerOCRrec5() As OCRrec5_def
        Dim ret As OCRrec5_def = New OCRrec5_def
        ret.T001.size = 5       '�`������R�[�h
        ret.T006.size = 1       '�s��
        ret.T007.size = 5       '�P��_5
        ret.T012.size = 4       '��_5
        ret.T016.size = 6       '���g�R�[�h_5
        ret.T022.size = 2       '��敪_5
        ret.T024.size = 5       '�P��_6
        ret.T029.size = 4       '��_6
        ret.T033.size = 6       '���g�R�[�h_6
        ret.T039.size = 2       '��敪_6
        ret.T041.size = 5       '�P��_7
        ret.T046.size = 4       '��_7
        ret.T050.size = 6       '���g�R�[�h_7
        ret.T056.size = 2       '��敪_7
        ret.T058.size = 5       '�P��_8
        ret.T063.size = 4       '��_8
        ret.T067.size = 6       '���g�R�[�h_8
        ret.T073.size = 2       '��敪_8
        ret.T075.size = 6       '��
        ret.T081.size = 2       'CRLF
        Return ret
    End Function

    Private Function initialyzerOCRrec6() As OCRrec6_def
        Dim ret As OCRrec6_def = New OCRrec6_def
        ret.T001.size = 5       '�`������R�[�h
        ret.T006.size = 1       '�s��
        ret.T007.size = 6       '�W���H���R�[�h
        ret.T013.size = 6       '�i��ցj�H���R�[�h
        ret.T019.size = 3       '�i��ցj�H������
        ret.T022.size = 2       '�i��ցj���
        ret.T024.size = 2       '�i��ցj�i��
        ret.T026.size = 6       '�i��ցj��o����
        ret.T032.size = 4       '����ذٗ]���E����
        ret.T036.size = 4       '����ذٗ]���E׽�
        ret.T040.size = 4       '׽�ذٗ]���E����
        ret.T044.size = 4       '׽�ذٗ]���E׽�
        ret.T048.size = 5       '�ő努�撷
        ret.T053.size = 1       '�v�Z����
        ret.T054.size = 27      '��
        ret.T081.size = 2       'CRLF
        Return ret
    End Function
    Private Sub setOcrRecValue(ByRef prmRefTarget As fixedStringType, ByVal prmVal As String)
        prmRefTarget.var = LSet(prmVal, prmRefTarget.size)
    End Sub
    '-------------------------------------------------------------------------------
    '�@ OCR��z�p�f�[�^(TEXT)�쐬
    '   �i�����T�v�j��z���[�N�c�a���OCR��z�p�f�[�^���쐬����B
    '-------------------------------------------------------------------------------
    Private Sub outputOcrTehaiDate(ByVal prmExecutePath As String, ByRef prmRefOutputCnt As Integer, ByRef prmRefProgress As UtilProgressBar)

        Dim SQL As String = ""
        SQL = SQL & N & "SELECT "
        SQL = SQL & N & "  TEHAI_NO"         ' 0�F��z��
        SQL = SQL & N & " ,SYORI_YM"         ' 1�F�����N��
        SQL = SQL & N & " ,SYORI_KBN"        ' 2�F�����敪
        SQL = SQL & N & " ,KIBOU_DATE"       ' 3�F��]�N����
        'SQL = SQL & N & " ,NOUKI"            ' 4�F�[��
        SQL = SQL & N & " ,TEHAI_KBN"        ' 5�F��z�敪
        SQL = SQL & N & " ,SEISAKU_KBN"      ' 6�F����敪
        SQL = SQL & N & " ,SEIZOU_BMN"       ' 7�F��������
        SQL = SQL & N & " ,DENPYOK"          ' 8�F�`�[�敪
        SQL = SQL & N & " ,TYUMONSAKI"       ' 9�F������
        SQL = SQL & N & " ,H_SIYOU_CD"       '10�F�i�i���R�[�h�j�d�l�R�[�h
        SQL = SQL & N & " ,H_HIN_CD"         '11�F�i�i���R�[�h�j�i��R�[�h
        SQL = SQL & N & " ,H_SENSIN_CD"      '12�F�i�i���R�[�h�j���S���R�[�h
        SQL = SQL & N & " ,H_SIZE_CD"        '13�F�i�i���R�[�h�j�T�C�Y�R�[�h
        SQL = SQL & N & " ,H_COLOR_CD"       '14�F�i�i���R�[�h�j�F�R�[�h
        SQL = SQL & N & " ,FUKA_CD"          '15�F�݌v�t���L��
        SQL = SQL & N & " ,HINMEI"           '16�F�i��
        SQL = SQL & N & " ,TEHAI_SUU"        '17�F��z����
        SQL = SQL & N & " ,TANCYO_KBN"       '18�F�P���敪
        SQL = SQL & N & " ,TANCYO"           '19�F����P��
        SQL = SQL & N & " ,JYOSU"            '20�F��
        SQL = SQL & N & " ,MAKI_CD"          '21�F���g�R�[�h
        SQL = SQL & N & " ,HOSO_KBN"         '22�F��敪
        SQL = SQL & N & " ,SIYOUSYO_NO"      '23�F�d�l����
        SQL = SQL & N & " ,TOKKI"            '24�F���L����
        SQL = SQL & N & " ,BIKO"             '25�F���l
        SQL = SQL & N & " ,HENKO"            '26�F�ύX���e
        SQL = SQL & N & " ,TENKAI_KBN"       '27�F�W�J�敪
        SQL = SQL & N & " ,BBNKOUTEI"        '28�F�w��H��
        SQL = SQL & N & " ,HINSITU_KBN"      '29�F�i�������敪
        SQL = SQL & N & " ,KEISAN_KBN"       '30�F���H���v�Z
        SQL = SQL & N & " ,TATIAI_UM"        '31�F����L��
        SQL = SQL & N & " ,TACIAIBI"         '32�F����\���
        SQL = SQL & N & " ,SEISEKI"          '33�F���я�
        SQL = SQL & N & " ,MYTANCYO"         '34�F�����]���i�P�����j
        SQL = SQL & N & " ,MYLOT"            '35�F�����]���i���b�g���j
        SQL = SQL & N & " ,TYTANCYO"         '36�F����]���i�P�����j
        SQL = SQL & N & " ,TYLOT"            '37�F����]���i���b�g���j
        SQL = SQL & N & " ,SYTANCYO"         '38�F�w��Ќ��]���i�P�����j
        SQL = SQL & N & " ,SYLOT"            '39�F�w��Ќ��]���i���b�g���j
        'SQL = SQL & n & " ,HYOJYUNC_1"       '40�F�W���H���R�[�h_1
        'SQL = SQL & n & " ,KOUTEIC_1"        '41�F�H���R�[�h_1
        'SQL = SQL & n & " ,KOUTEIJ_1"        '42�F�H������_1
        'SQL = SQL & n & " ,TEIIN_1"          '43�F���_1
        'SQL = SQL & n & " ,DANDORI_1"        '44�F�i��_1
        'SQL = SQL & n & " ,KIJYUN_1"         '45�F��o����_1
        'SQL = SQL & n & " ,HYOJYUNC_2"       '46�F�W���H���R�[�h_2
        'SQL = SQL & n & " ,KOUTEIC_2"        '47�F�H���R�[�h_2
        'SQL = SQL & n & " ,KOUTEIJ_2"        '48�F�H������_2
        'SQL = SQL & n & " ,TEIIN_2"          '49�F���_2
        'SQL = SQL & n & " ,DANDORI_2"        '50�F�i��_2
        'SQL = SQL & n & " ,KIJYUN_2"         '51�F��o����_2
        'SQL = SQL & n & " ,HYOJYUNC_3"       '52�F�W���H���R�[�h_3
        'SQL = SQL & n & " ,KOUTEIC_3"        '53�F�H���R�[�h_3
        'SQL = SQL & n & " ,KOUTEIJ_3"        '54�F�H������_3
        'SQL = SQL & n & " ,TEIIN_3"          '55�F���_3
        'SQL = SQL & n & " ,DANDORI_3"        '56�F�i��_3
        'SQL = SQL & n & " ,KIJYUN_3"         '57�F��o����_3
        'SQL = SQL & n & " ,HYOJYUNC_4"       '58�F�W���H���R�[�h_4
        'SQL = SQL & n & " ,KOUTEIC_4"        '59�F�H���R�[�h_4
        'SQL = SQL & n & " ,KOUTEIJ_4"        '60�F�H������_4
        'SQL = SQL & n & " ,TEIIN_4"          '61�F���_4
        'SQL = SQL & n & " ,DANDORI_4"        '62�F�i��_4
        'SQL = SQL & n & " ,KIJYUN_4"         '63�F��o����_4
        'SQL = SQL & n & " ,HYOJYUNC_5"       '64�F�W���H���R�[�h_5
        'SQL = SQL & n & " ,KOUTEIC_5"        '65�F�H���R�[�h_5
        'SQL = SQL & n & " ,KOUTEIJ_5"        '66�F�H������_5
        'SQL = SQL & n & " ,TEIIN_5"          '67�F���_5
        'SQL = SQL & n & " ,DANDORI_5"        '68�F�i��_5
        'SQL = SQL & n & " ,KIJYUN_5"         '69�F��o����_5
        SQL = SQL & N & " ,UPDDATE"          '70�F�X�V��
        SQL = SQL & N & " FROM T51TEHAI"
        '2011/01/28 add start Sugawara #95
        SQL = SQL & N & " WHERE GAI_FLG IS NULL"   '(�����l�FNULL�A�ΏۊO�F1) �ΏۊO�f�[�^�������B
        '2011/01/28 add end Sugawara #95
        SQL = SQL & N & " ORDER BY TEHAI_NO"
        Dim ds As DataSet = _db.selectDB(SQL, RS)

        Dim tw As UtilMDL.Text.UtilTextWriter = New UtilMDL.Text.UtilTextWriter(prmExecutePath)
        '-->2011.02.15 upd start by takagi #104 �ǋL���[�h���㏑�����[�h
        'tw.open()
        Const NO_APPEND As Boolean = False
        tw.open(NO_APPEND)                                                  '�ǋL�ł͂Ȃ��A�㏑��
        '<--2011.02.15 upd end by takagi #104 �ǋL���[�h���㏑�����[�h
        Try
            For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1

                Dim OCRrec1 As OCRrec1_def = initialyzerOCRrec1()           'OCR��z�p�f�[�^�E�s��P
                Call Edit_OCRrec1(ds.Tables(RS).Rows(i), OCRrec1)
                tw.write(OCRrec1)

                Dim OCRrec2 As OCRrec2_def = initialyzerOCRrec2()           'OCR��z�p�f�[�^�E�s��Q
                Call Edit_OCRrec2(ds.Tables(RS).Rows(i), OCRrec2)
                tw.write(OCRrec2)

                Dim OCRrec3 As OCRrec3_def = initialyzerOCRrec3()           'OCR��z�p�f�[�^�E�s��R
                Call Edit_OCRrec3(ds.Tables(RS).Rows(i), OCRrec3)
                tw.write(OCRrec3)

                Dim OCRrec4 As OCRrec4_def = initialyzerOCRrec4()           'OCR��z�p�f�[�^�E�s��S
                Call Edit_OCRrec4(ds.Tables(RS).Rows(i), OCRrec4)
                tw.write(OCRrec4)

                Dim OCRrec5 As OCRrec5_def = initialyzerOCRrec5()           'OCR��z�p�f�[�^�E�s��T
                Call Edit_OCRrec5(ds.Tables(RS).Rows(i), OCRrec5)
                tw.write(OCRrec5)

                For j As Integer = 0 To 4
                    Dim OCRrec6 As OCRrec6_def = initialyzerOCRrec6()       'OCR��z�p�f�[�^�E�s��U
                    Call Edit_OCRrec6(ds.Tables(RS).Rows(i), OCRrec6, j)
                    If CLng(OCRrec6.T019.var) <> 0 Then
                        '�H�����ʂ��O�ȊO�̏ꍇ�A��֎w�背�R�[�h���o��
                        tw.write(OCRrec6)
                    End If
                Next

            Next

        Finally
            tw.close()
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@ OCR��z�p�f�[�^�E�s��P�ҏW
    '   �i�����T�v�jOCR��z�p�f�[�^�E�s��P��ҏW����B
    '-------------------------------------------------------------------------------
    Private Sub Edit_OCRrec1(ByVal prmRow As DataRow, ByRef prmRefOcrRec1 As OCRrec1_def)

        Dim sHinCD As String = ""
        sHinCD += UtilClass.setDataLen(_db.rmNullStr(prmRow("H_SIYOU_CD")), 2)
        sHinCD += UtilClass.setDataLen(_db.rmNullStr(prmRow("H_HIN_CD")), 3)
        sHinCD += UtilClass.setDataLen(_db.rmNullStr(prmRow("H_SENSIN_CD")), 3)
        sHinCD += UtilClass.setDataLen(_db.rmNullStr(prmRow("H_SIZE_CD")), 2)
        sHinCD += UtilClass.setDataLen(_db.rmNullStr(prmRow("H_COLOR_CD")), 3)

        'OCR��z�p�f�[�^�E�s��P�ҏW
        With prmRefOcrRec1
            setOcrRecValue(.T001, sSEND_CTRL_CD)                                            '�`������R�[�h
            setOcrRecValue(.T006, sGYO_1)                                                   '�s��
            setOcrRecValue(.T007, _db.rmNullInt(prmRow("SYORI_KBN")))                       '�����敪
            setOcrRecValue(.T008, Format(_db.rmNullInt(prmRow("TEHAI_NO")), "00000"))       '��z��
            setOcrRecValue(.T013, _db.rmNullInt(prmRow("TEHAI_KBN")))                       '��z�敪
            setOcrRecValue(.T014, _db.rmNullInt(prmRow("SEISAKU_KBN")))                     '����敪
            setOcrRecValue(.T015, sHinCD)                                                   '�i���R�[�h
            setOcrRecValue(.T028, _db.rmNullStr(prmRow("FUKA_CD")))                         '�݌v�t���L��
            setOcrRecValue(.T029, _db.rmNullStr(prmRow("KIBOU_DATE")).PadLeft(8, "0").Substring(4)) '��]�o����
            setOcrRecValue(.T033, _db.rmNullInt(prmRow("TENKAI_KBN")))                      '�W�J�敪
            setOcrRecValue(.T034, _db.rmNullStr(prmRow("BBNKOUTEI")))                       '�����W�J�w��H��
            setOcrRecValue(.T040, _db.rmNullInt(prmRow("KEISAN_KBN")))                      '���H�v�Z�敪
            setOcrRecValue(.T041, _db.rmNullInt(prmRow("TATIAI_UM")))                       '����L��
            setOcrRecValue(.T042, _db.rmNullStr(prmRow("TACIAIBI")).PadLeft(8, "0").Substring(4)) '����\���
            setOcrRecValue(.T046, _db.rmNullInt(prmRow("SEISEKI")))                         '���я�
            setOcrRecValue(.T047, _db.rmNullInt(prmRow("TANCYO_KBN")))                      '�P���敪
            setOcrRecValue(.T048, Format(_db.rmNullInt(prmRow("TEHAI_SUU")), "0000000"))    '��z����
            'setOcrRecValue(.T055, _db.rmNullStr(prmRow("NOUKI")).PadLeft(8, "0").Substring(2)) '�[��
            setOcrRecValue(.T055, "") '�[��
            setOcrRecValue(.T061, _db.rmNullInt(prmRow("HINSITU_KBN")))                     '�i�������敪
            setOcrRecValue(.T062, Format(_db.rmNullDouble(prmRow("MYTANCYO")) * 10, "00"))  '�����]���i�P���j
            setOcrRecValue(.T064, Format(_db.rmNullDouble(prmRow("MYLOT")) * 100, "0000"))  '�����]���i���b�g�j
            setOcrRecValue(.T067, Format(_db.rmNullDouble(prmRow("TYTANCYO")) * 10, "00"))  '����]���i�P���j
            setOcrRecValue(.T069, Format(_db.rmNullDouble(prmRow("TYLOT")) * 100, "0000"))  '����]���i���b�g�j
            setOcrRecValue(.T072, Format(_db.rmNullDouble(prmRow("SYTANCYO")) * 10, "00"))  '�w��Ќ��i�P���j
            setOcrRecValue(.T074, Format(_db.rmNullDouble(prmRow("SYLOT")) * 100, "0000"))  '�w��Ќ��i���b�g�j
            setOcrRecValue(.T077, Space(4))                                                 '��
            setOcrRecValue(.T081, ControlChars.CrLf)                                        'CRLF
        End With

    End Sub

    '-------------------------------------------------------------------------------
    '�@ OCR��z�p�f�[�^�E�s��Q�ҏW
    '   �i�����T�v�jOCR��z�p�f�[�^�E�s��Q��ҏW����B
    '-------------------------------------------------------------------------------
    Private Sub Edit_OCRrec2(ByVal prmRow As DataRow, ByRef prmRefOcrRec2 As OCRrec2_def)

        'OCR��z�p�f�[�^�E�s��Q�ҏW
        With prmRefOcrRec2
            setOcrRecValue(.T001, sSEND_CTRL_CD)                                        '�`������R�[�h
            setOcrRecValue(.T006, sGYO_2)                                               '�s��
            setOcrRecValue(.T007, _db.rmNullStr(prmRow("SIYOUSYO_NO")))                 '�d�l���ԍ�
            setOcrRecValue(.T027, _db.rmNullStr(prmRow("TYUMONSAKI")))                  '������
            setOcrRecValue(.T047, Space(34))                                            '��
            setOcrRecValue(.T081, ControlChars.CrLf)                                    'CRLF
        End With

    End Sub

    '-------------------------------------------------------------------------------
    '�@ OCR��z�p�f�[�^�E�s��R�ҏW
    '   �i�����T�v�jOCR��z�p�f�[�^�E�s��R��ҏW����B
    '-------------------------------------------------------------------------------
    Private Sub Edit_OCRrec3(ByVal prmRow As DataRow, ByRef prmRefOcrRec3 As OCRrec3_def)

        'OCR��z�p�f�[�^�E�s��R�ҏW
        With prmRefOcrRec3
            setOcrRecValue(.T001, sSEND_CTRL_CD)                                        '�`������R�[�h
            setOcrRecValue(.T006, sGYO_3)                                               '�s��
            setOcrRecValue(.T007, _db.rmNullStr(prmRow("TOKKI")))                       '���L����
            setOcrRecValue(.T073, Space(8))                                             '��
            setOcrRecValue(.T081, ControlChars.CrLf)                                    'CRLF
        End With

    End Sub

    '-------------------------------------------------------------------------------
    '�@ OCR��z�p�f�[�^�E�s��S�ҏW
    '   �i�����T�v�jOCR��z�p�f�[�^�E�s��S��ҏW����B
    '-------------------------------------------------------------------------------
    Private Sub Edit_OCRrec4(ByVal prmRow As DataRow, ByRef prmRefOcrRec4 As OCRrec4_def)

        'OCR��z�p�f�[�^�E�s��S�ҏW
        With prmRefOcrRec4
            setOcrRecValue(.T001, sSEND_CTRL_CD)                                        '�`������R�[�h
            setOcrRecValue(.T006, sGYO_4)                                               '�s��
            setOcrRecValue(.T007, Format(_db.rmNullInt(prmRow("TANCYO")), "00000"))     '�P��_1
            setOcrRecValue(.T012, Format(_db.rmNullInt(prmRow("JYOSU")), "0000"))       '��_1
            setOcrRecValue(.T016, Format(_db.rmNullInt(prmRow("MAKI_CD")), "000000"))   '���g�R�[�h_1
            setOcrRecValue(.T022, _db.rmNullStr(prmRow("HOSO_KBN")))                    '��敪_1
            setOcrRecValue(.T024, "00000")                                              '�P��_2
            setOcrRecValue(.T029, "0000")                                               '��_2
            setOcrRecValue(.T033, "000000")                                             '���g�R�[�h_2
            setOcrRecValue(.T039, Space(2))                                             '��敪_2
            setOcrRecValue(.T041, "00000")                                              '�P��_3
            setOcrRecValue(.T046, "0000")                                               '��_3
            setOcrRecValue(.T050, "000000")                                             '���g�R�[�h_3
            setOcrRecValue(.T056, Space(2))                                             '��敪_3
            setOcrRecValue(.T058, "00000")                                              '�P��_4
            setOcrRecValue(.T063, "0000")                                               '��_4
            setOcrRecValue(.T067, "000000")                                             '���g�R�[�h_4
            setOcrRecValue(.T073, Space(2))                                             '��敪_4
            setOcrRecValue(.T075, Space(6))                                             '��
            setOcrRecValue(.T081, ControlChars.CrLf)                                    'CRLF
        End With

    End Sub


    '-------------------------------------------------------------------------------
    '�@ OCR��z�p�f�[�^�E�s��T�ҏW
    '   �i�����T�v�jOCR��z�p�f�[�^�E�s��T��ҏW����B
    '-------------------------------------------------------------------------------
    Private Sub Edit_OCRrec5(ByVal prmRow As DataRow, ByRef prmRefOcrRec5 As OCRrec5_def)

        'OCR��z�p�f�[�^�E�s��T�ҏW
        With prmRefOcrRec5
            setOcrRecValue(.T001, sSEND_CTRL_CD)    '�`������R�[�h
            setOcrRecValue(.T006, sGYO_5)           '�s��
            setOcrRecValue(.T007, "00000")          '�P��_5
            setOcrRecValue(.T012, "0000")           '��_5
            setOcrRecValue(.T016, "000000")         '���g�R�[�h_5
            setOcrRecValue(.T022, Space(2))         '��敪_5
            setOcrRecValue(.T024, "00000")          '�P��_6
            setOcrRecValue(.T029, "0000")           '��_6
            setOcrRecValue(.T033, "000000")         '���g�R�[�h_6
            setOcrRecValue(.T039, Space(2))         '��敪_6
            setOcrRecValue(.T041, "00000")          '�P��_7
            setOcrRecValue(.T046, "0000")           '��_7
            setOcrRecValue(.T050, "000000")         '���g�R�[�h_7
            setOcrRecValue(.T056, Space(2))         '��敪_7
            setOcrRecValue(.T058, "00000")          '�P��_8
            setOcrRecValue(.T063, "0000")           '��_8
            setOcrRecValue(.T067, "000000")         '���g�R�[�h_8
            setOcrRecValue(.T073, Space(2))         '��敪_8
            setOcrRecValue(.T075, Space(6))         '��
            setOcrRecValue(.T081, ControlChars.CrLf) 'CRLF
        End With

    End Sub


    '-------------------------------------------------------------------------------
    '�@ OCR��z�p�f�[�^�E�s��U�ҏW
    '   �i�����T�v�jOCR��z�p�f�[�^�E�s��U��ҏW����B
    '-------------------------------------------------------------------------------
    Private Sub Edit_OCRrec6(ByVal prmRow As DataRow, ByRef prmRefOcrRec6 As OCRrec6_def, ByVal prmRowCnt As Long)
        With prmRefOcrRec6

            'OCR��z�p�f�[�^�E�s��U�ҏW
            setOcrRecValue(.T001, sSEND_CTRL_CD)                '�`������R�[�h
            setOcrRecValue(.T006, sGYO_6)                       '�s��

            ''setOcrRecValue(.T007, _db.rmNullStr(prmRow("HYOJYUNC_" & CStr(prmRowCnt + 1))))                      '�W���H���R�[�h
            ''setOcrRecValue(.T013, _db.rmNullStr(prmRow("KOUTEIC_" & CStr(prmRowCnt + 1))))                       '�i��ցj�H���R�[�h
            ''setOcrRecValue(.T019, Format(_db.rmNullInt(prmRow("KOUTEIJ_" & CStr(prmRowCnt + 1))), "000"))        '�i��ցj�H������
            ''setOcrRecValue(.T022, Format(_db.rmNullDouble(prmRow("TEIIN_" & CStr(prmRowCnt + 1))) * 10, "00"))   '�i��ցj���
            ''setOcrRecValue(.T024, Format(_db.rmNullDouble(prmRow("DANDORI_" & CStr(prmRowCnt + 1))) * 10, "00")) '�i��ցj�i��
            ''setOcrRecValue(.T026, Format(_db.rmNullInt(prmRow("KIJYUN_" & CStr(prmRowCnt + 1))), "000000"))      '�i��ցj��o����
            setOcrRecValue(.T007, "")                           '�W���H���R�[�h
            setOcrRecValue(.T013, "")                           '�i��ցj�H���R�[�h
            setOcrRecValue(.T019, Format(0, "000"))             '�i��ցj�H������
            setOcrRecValue(.T022, Format(0, "00"))              '�i��ցj���
            setOcrRecValue(.T024, Format(0, "00"))              '�i��ցj�i��
            setOcrRecValue(.T026, Format(0, "000000"))          '�i��ցj��o����

            setOcrRecValue(.T032, "0000")                       '����ذٗ]���E����
            setOcrRecValue(.T036, "0000")                       '����ذٗ]���E׽�
            setOcrRecValue(.T040, "0000")                       '׽�ذٗ]���E����
            setOcrRecValue(.T044, "0000")                       '׽�ذٗ]���E׽�
            setOcrRecValue(.T048, "00000")                      '�ő努�撷
            setOcrRecValue(.T053, "0")                          '�v�Z����
            setOcrRecValue(.T054, Space(27))                    '��
            setOcrRecValue(.T081, ControlChars.CrLf)            'CRLF
        End With
    End Sub
End Class