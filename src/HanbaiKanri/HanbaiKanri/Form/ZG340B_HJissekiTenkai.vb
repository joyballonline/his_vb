'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���j�̔����яW�v�W�J�w��
'    �i�t�H�[��ID�jZG340B_HJissekiTorikomi
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ����        2010/10/26                 �V�K              
'�@(2)   ����        2011/01/26                 �ύX�@�ʌv����͖��o�^�`�F�b�N�ǉ�              
'�@(3)   ����        2014/06/04                 �ύX�@�ޗ��[�}�X�^�iMPESEKKEI�j�e�[�u���ύX�Ή�            
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Public Class ZG340B_HJissekiTenkai
    Inherits System.Windows.Forms.Form

    '-------------------------------------------------------------------------------
    '   �萔��`
    '-------------------------------------------------------------------------------
    Private Const RS As String = "RecSet"                       '�f�[�^�Z�b�g�e�[�u����
    Private Const N As String = ControlChars.NewLine            '���s����
    Private Const PGID As String = "ZG340B"
    Private Const IMP_LOG_NM As String = "�̔����яW�v�W�J�����o�͏��.txt" '2010.12.27 add by takagi #59

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
    Private Sub ZG330B_HJissekiTorikomi_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            sql = sql & N & "    ,KENNSU2 "
            sql = sql & N & "    ,KENNSU3 "
            sql = sql & N & "    ,KENNSU4 "
            sql = sql & N & "    ,KENNSU5 "
            sql = sql & N & "    ,NAME1 "
            sql = sql & N & "    ,NAME2 "
            sql = sql & N & "    from T91RIREKI "
            sql = sql & N & "    where PGID     = '" & PGID & "' "
            sql = sql & N & ") "
            sql = sql & N & "where RNUM = 1 "
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)
            If iRecCnt < 1 Then
                '�����Ȃ�
                lblZenkaiJikkouDate.Text = ZC110M_Menu.NON_EXECUTE
                lblZenkaiKensuPtn1.Text = ""
                lblZenkaiKensuPtn2.Text = ""
                lblZenkaiKensuPtn3.Text = ""
                lblZenkaiKensuPtn4.Text = ""
                lblZenkaiKensuPtn5.Text = ""
                lblZenkaiAnbunFrom.Text = ""
                lblZenkaiAnbunTo.Text = ""
            Else
                '��������
                lblZenkaiJikkouDate.Text = _db.rmNullDate(ds.Tables(RS).Rows(0)("SDATEEND"))
                lblZenkaiKensuPtn1.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU1")), "#,##0")
                lblZenkaiKensuPtn2.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU2")), "#,##0")
                lblZenkaiKensuPtn3.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU3")), "#,##0")
                lblZenkaiKensuPtn4.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU4")), "#,##0")
                lblZenkaiKensuPtn5.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU5")), "#,##0")
                Dim wk As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("NAME1"))
                lblZenkaiAnbunFrom.Text = wk.Substring(0, 4) & "/" & wk.Substring(4, 2)
                wk = _db.rmNullStr(ds.Tables(RS).Rows(0)("NAME2"))
                lblZenkaiAnbunTo.Text = wk.Substring(0, 4) & "/" & wk.Substring(4, 2)
            End If



            '������s���̕\��
            dteKonkaiAnbunFrom.Text = Format(DateAdd(DateInterval.Month, -12, CDate(lblSyoriDate.Text & "/01")), "yyyy/MM")
            dteKonkaiAnbunTo.Text = Format(DateAdd(DateInterval.Month, -1, CDate(lblSyoriDate.Text & "/01")), "yyyy/MM")
            lblKonkaiKensuPtn1.Text = getCntAnbunTarget()
            lblKonkaiKensuPtn2.Text = _db.rmNullInt(_db.selectDB("SELECT COUNT(*) CNT FROM T12HINMEIHANK WHERE TENKAIPTN = '2'", RS, iRecCnt).Tables(RS).Rows(0)("CNT"))
            lblKonkaiKensuPtn3.Text = getCntTarget(-3, -1, "3")
            lblKonkaiKensuPtn4.Text = getCntTarget(-12, -1, "4")
            lblKonkaiKensuPtn5.Text = getCntTarget(-12, -10, "5")

            '���s�{�^���g�p��
            btnJikkou.Enabled = _updFlg

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '�W�J�p�^�[���R�C�S�C�T�̑Ώی������擾����
    Private Function getCntTarget(ByVal prmFrom As Integer, ByVal prmTo As Integer, ByVal prmPtn As String) As String

        Dim prmFromYM As String = Format(DateAdd(DateInterval.Month, prmFrom, CDate(lblSyoriDate.Text & "/01")), "yyyyMM")
        Dim prmToYM As String = Format(DateAdd(DateInterval.Month, prmTo, CDate(lblSyoriDate.Text & "/01")), "yyyyMM")
        'W13
        Dim Sql As String = ""
        Sql = Sql & N & "SELECT COUNT(*) CNT FROM ( "
        Sql = Sql & N & "SELECT KHINMEICD "
        Sql = Sql & N & "FROM ( "
        Sql = Sql & N & "     SELECT "
        Sql = Sql & N & "      KHINMEICD "
        Sql = Sql & N & "     ,NENGETU "
        Sql = Sql & N & "     FROM ( "
        Sql = Sql & N & "          SELECT "
        Sql = Sql & N & "           M12.KHINMEICD "
        Sql = Sql & N & "          ,T10.NENGETU "
        Sql = Sql & N & "          FROM T10HINHANJ T10 "
        Sql = Sql & N & "          INNER JOIN M12SYUYAKU    M12 ON T10.HINMEICD  = M12.HINMEICD "
        Sql = Sql & N & "          INNER JOIN M11KEIKAKUHIN M11 ON M12.KHINMEICD = M11.TT_KHINMEICD "
        Sql = Sql & N & "          WHERE M11.TT_TENKAIPTN = '" & prmPtn & "' "
        Sql = Sql & N & "            AND T10.NENGETU >= '" & Format(DateAdd(DateInterval.Month, -12, CDate(lblSyoriDate.Text & "/01")), "yyyyMM") & "' "
        Sql = Sql & N & "            AND T10.NENGETU <= '" & Format(DateAdd(DateInterval.Month, -1, CDate(lblSyoriDate.Text & "/01")), "yyyyMM") & "' "
        Sql = Sql & N & "          ) "
        Sql = Sql & N & "     GROUP BY  KHINMEICD,NENGETU "
        Sql = Sql & N & "     ) "
        Sql = Sql & N & "WHERE NENGETU >= '" & prmFromYM & "' "
        Sql = Sql & N & "  AND NENGETU <= '" & prmToYM & "' "
        Sql = Sql & N & "GROUP BY KHINMEICD "
        Sql = Sql & N & ")"
        Return _db.rmNullInt(_db.selectDB(Sql, RS).Tables(RS).Rows(0)("CNT")).ToString

    End Function

    '�W�J�p�^�[���P�̑Ώی������擾���邪�A�}�X�^�Ƃ̓ˍ����ʂɂ���Ă͂���ȉ��ɂȂ邱�Ƃ�����
    Private Function getCntAnbunTarget() As String

        Dim Sql As String = ""
        Sql = Sql & N & "SELECT COUNT(*) CNT "
        Sql = Sql & N & "FROM ( "
        Sql = Sql & N & "     SELECT * "
        Sql = Sql & N & "     FROM ( "
        Sql = Sql & N & "          SELECT "
        Sql = Sql & N & "           KHINMEICD "
        Sql = Sql & N & "          ,NENGETU "
        Sql = Sql & N & "          FROM ( "
        Sql = Sql & N & "               SELECT "
        Sql = Sql & N & "                M12.KHINMEICD "
        Sql = Sql & N & "               ,T10.NENGETU "
        Sql = Sql & N & "               FROM T10HINHANJ T10 "
        Sql = Sql & N & "               INNER JOIN M12SYUYAKU    M12 ON T10.HINMEICD  = M12.HINMEICD "
        Sql = Sql & N & "               INNER JOIN M11KEIKAKUHIN M11 ON M12.KHINMEICD = M11.TT_KHINMEICD "
        Sql = Sql & N & "               WHERE M11.TT_TENKAIPTN = '1' "
        Sql = Sql & N & "                 AND T10.NENGETU >= '" & Format(DateAdd(DateInterval.Month, -12, CDate(lblSyoriDate.Text & "/01")), "yyyyMM") & "' "
        Sql = Sql & N & "                 AND T10.NENGETU <= '" & Format(DateAdd(DateInterval.Month, -1, CDate(lblSyoriDate.Text & "/01")), "yyyyMM") & "' "
        Sql = Sql & N & "               ) "
        Sql = Sql & N & "          GROUP BY  KHINMEICD,NENGETU "
        Sql = Sql & N & "          ) "
        Sql = Sql & N & "     WHERE NENGETU >= '" & dteKonkaiAnbunFrom.Text.Replace("/", "") & "' "
        Sql = Sql & N & "       AND NENGETU <= '" & dteKonkaiAnbunTo.Text.Replace("/", "") & "' "
        Sql = Sql & N & "     GROUP BY KHINMEICD "
        Sql = Sql & N & "     ) "
        Return _db.rmNullInt(_db.selectDB(Sql, RS).Tables(RS).Rows(0)("CNT")).ToString

    End Function

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

            '���̓`�F�b�N
            Call checkInput()

            '' 2011/01/26 ADD-S Sugano #94
            '�ʔ̔��v����͂̓o�^�σ`�F�b�N
            '���o�^�̃��R�[�h�������擾
            Dim sql = ""
            sql = sql & N & " SELECT COUNT(*) RECCNT FROM M11KEIKAKUHIN M11 "
            sql = sql & N & " LEFT JOIN T12HINMEIHANK T12 "
            sql = sql & N & " ON M11.TT_KHINMEICD = T12.KHINMEICD "
            sql = sql & N & " WHERE M11.TT_TENKAIPTN = '2' "
            sql = sql & N & " AND "
            sql = sql & N & " T12.KHINMEICD IS NULL "   'T12�Ƀ��R�[�h�����݂��Ȃ�����

            'SQL���s
            Dim ds As DataSet = _db.selectDB(sql, RS)
            Dim reccnt As Integer = 0
            reccnt = _db.rmNullInt(ds.Tables(RS).Rows(0)("RECCNT"))
            If reccnt <> 0 Then
                Throw New UsrDefException("�ʌv����͂����o�^�̃f�[�^������܂��B", _msgHd.getMSG("kobetsuMitouroku"), btnModoru)
            End If
            '' 2011/01/26 ADD-E Sugano #94

            '���s�m�F�i���s���܂��B��낵���ł����H�j
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmRun")
            If rtn = Windows.Forms.DialogResult.No Or rtn = Windows.Forms.DialogResult.Cancel Then Exit Sub

            '-->2010.12.27 add by takagi #59
            Dim cntNullMetsuke As Integer = 0
            '<--2010.12.27 add by takagi #59

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
                    pb.status = "�捞������" : pb.maxVal = 3
                    _db.executeDB("delete from ZG340B_W10 where UPDNAME = '" & UtilClass.getComputerName() & "'")
                    _db.executeDB("delete from ZG340B_W11 where UPDNAME = '" & UtilClass.getComputerName() & "'")
                    _db.executeDB("delete from ZG340B_W12 where UPDNAME = '" & UtilClass.getComputerName() & "'")
                    _db.executeDB("delete from ZG340B_W13 where UPDNAME = '" & UtilClass.getComputerName() & "'")
                    _db.executeDB("delete from ZG340B_W14 where UPDNAME = '" & UtilClass.getComputerName() & "'")

                    ' ''Call kobetsuInsert()                    '1-0 �ʌv��ݒ�

                    Call createHanbaiJisseki()              '2-0 �v��i��CD�⊮
                    Call createKeikakuhinHanbaiJisseki()    '2-1 �v��i�����W�v
                    Call createAnbunWk()                    '2-2 �����ԏW�v(�p�^�[���P)
                    Call createHanbaiJissekiWkByPtn(Format(DateAdd(DateInterval.Month, -3, CDate(lblSyoriDate.Text & "/01")), "yyyyMM"), _
                                                    Format(DateAdd(DateInterval.Month, -1, CDate(lblSyoriDate.Text & "/01")), "yyyyMM"), _
                                                    "3")    '2-3 ���߂R�J���W�v(�p�^�[���R)
                    Call createHanbaiJissekiWkByPtn(Format(DateAdd(DateInterval.Month, -12, CDate(lblSyoriDate.Text & "/01")), "yyyyMM"), _
                                                    Format(DateAdd(DateInterval.Month, -1, CDate(lblSyoriDate.Text & "/01")), "yyyyMM"), _
                                                    "4")    '2-4 �O�N�W�v(�p�^�[���S)
                    '-->2010.12.25 chg by takagi #49
                    'Call createHanbaiJissekiWkByPtn(Format(DateAdd(DateInterval.Month, -12, CDate(lblSyoriDate.Text & "/01")), "yyyyMM"), _
                    '                                Format(DateAdd(DateInterval.Month, -10, CDate(lblSyoriDate.Text & "/01")), "yyyyMM"), _
                    '                                "5")    '2-5 �O�N�R�J���W�v(�p�^�[���T)
                    Call createHanbaiJissekiWkByPtn(Format(DateAdd(DateInterval.Month, -13, CDate(lblSyoriDate.Text & "/01")), "yyyyMM"), _
                                                    Format(DateAdd(DateInterval.Month, -11, CDate(lblSyoriDate.Text & "/01")), "yyyyMM"), _
                                                    "5T")    '2-5 �O�N�R�J���W�v(�p�^�[���T-1)
                    Call createHanbaiJissekiWkByPtn(Format(DateAdd(DateInterval.Month, -12, CDate(lblSyoriDate.Text & "/01")), "yyyyMM"), _
                                                    Format(DateAdd(DateInterval.Month, -10, CDate(lblSyoriDate.Text & "/01")), "yyyyMM"), _
                                                    "5Y")    '2-5 �O�N�R�J���W�v(�p�^�[���T-2)
                    Call createHanbaiJissekiWkByPtn(Format(DateAdd(DateInterval.Month, -11, CDate(lblSyoriDate.Text & "/01")), "yyyyMM"), _
                                                    Format(DateAdd(DateInterval.Month, -9, CDate(lblSyoriDate.Text & "/01")), "yyyyMM"), _
                                                    "5YY")    '2-5 �O�N�R�J���W�v(�p�^�[���T-3)
                    '<--2010.12.25 chg by takagi #49
                    Call summaryHinshuValue()               '2-6 �i��ʍ��v����
                    Call updateHinshuSum()                  '2-7 �i��ʍ��v�⊮
                    Call updateAnbunRate()                  '2-8 �T�C�Y�ʈ����Z�o
                    _db.beginTran()
                    Try
                        _db.executeDB("delete from T13HANBAI ")
                        '-->2010.12.27 chg by takagi #59
                        'Call kobetsuInsert()                    '1-0 �ʌv��ݒ�
                        Call kobetsuInsert(cntNullMetsuke)
                        '<--2010.12.27 chg by takagi #59

                        Call insertAnbunRec()                   '2-9 ���l�v��ݒ�
                        Call insertAvgRec()                     '2-10 ���ϒl�v��ݒ�

                        pb.status = "�X�e�[�^�X�ύX���E�E�E"
                        ed = Now                    '�����I������
                        _parentForm.updateSeigyoTbl(PGID, True, st, ed) : pb.value += 1         '3-0

                        pb.status = "���s�����쐬"
                        insertRireki(st, ed) : pb.value += 1                                    '3-1 ���s�����i�[
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
            '-->2010.12.27 chg by takagi #59
            'Call _msgHd.dspMSG("completeRun")
            Dim optionMsg As String = ""
            Dim wk As String = ""
            Dim outFilePath As String = ""
            Call UtilClass.dividePathAndFile(StartUp.iniValue.LogFilePath, outFilePath, wk)
            outFilePath = outFilePath & "\" & IMP_LOG_NM
            If cntNullMetsuke > 0 Then
                optionMsg = "-----------------------------------------------------------------" & N & _
                            "�ڕt�̎擾���s���Ȃ��f�[�^��" & cntNullMetsuke & "�����݂��܂����B" & N & _
                            "�ڍׂȕi���R�[�h�̓��O���m�F���Ă��������B"

                optionMsg = optionMsg & N & N & outFilePath
            End If
            Call _msgHd.dspMSG("completeRun", optionMsg)
            If cntNullMetsuke > 0 Then
                '���O�\��
                Try
                    System.Diagnostics.Process.Start(outFilePath)   '�֘A�t�����A�v���ŋN��
                Catch ex As Exception
                End Try
            End If
            '<--2010.12.27 chg by takagi #59
            Call btnModoru_Click(Nothing, Nothing)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '2-10 ���ϒl�v��ݒ�
    Private Sub insertAvgRec()
        Dim sql As String = ""
        sql = sql & N & "INSERT INTO T13HANBAI "
        sql = sql & N & "( "
        sql = sql & N & " KHINMEICD "
        sql = sql & N & ",TENKAIPTN "
        sql = sql & N & ",THANBAIRYOU "
        sql = sql & N & ",YHANBAIRYOU "
        sql = sql & N & ",YYHANBAIRYOU "
        sql = sql & N & ",THANBAIRYOUH "
        sql = sql & N & ",YHANBAIRYOUH "
        sql = sql & N & ",YYHANBAIRYOUH "
        sql = sql & N & ",THANBAIRYOUHK "
        sql = sql & N & ",YHANBAIRYOUHK "
        sql = sql & N & ",YYHANBAIRYOUHK "
        sql = sql & N & ",UPDNAME "
        sql = sql & N & ",UPDDATE "
        '-->2010.12.12 add by takagi
        sql = sql & N & ",METSUKE "
        sql = sql & N & ",THANBAISU "
        sql = sql & N & ",YHANBAISU "
        sql = sql & N & ",YYHANBAISU "
        sql = sql & N & ",THANBAISUH "
        sql = sql & N & ",YHANBAISUH "
        sql = sql & N & ",YYHANBAISUH "
        sql = sql & N & ",THANBAISUHK "
        sql = sql & N & ",YHANBAISUHK "
        sql = sql & N & ",YYHANBAISUHK "
        '<--2010.12.12 add by takagi
        sql = sql & N & ") "
        '-->2010.12.17 chg by takagi #15
        ''-->2010.12.12 chg by takagi
        ''sql = sql & N & "SELECT  "
        ''sql = sql & N & " KHINMEICD "
        ''sql = sql & N & ",TENKAIPTN "
        ''sql = sql & N & ",ROUND(HJISSEKIRYOU/DECODE(TENKAIPTN,'4',12,3),1) "
        ''sql = sql & N & ",ROUND(HJISSEKIRYOU/DECODE(TENKAIPTN,'4',12,3),1) "
        ''sql = sql & N & ",ROUND(HJISSEKIRYOU/DECODE(TENKAIPTN,'4',12,3),1) "
        ''sql = sql & N & ",NULL "
        ''sql = sql & N & ",NULL "
        ''sql = sql & N & ",NULL "
        ''sql = sql & N & ",ROUND(HJISSEKIRYOU/DECODE(TENKAIPTN,'4',12,3),1) "
        ''sql = sql & N & ",ROUND(HJISSEKIRYOU/DECODE(TENKAIPTN,'4',12,3),1) "
        ''sql = sql & N & ",ROUND(HJISSEKIRYOU/DECODE(TENKAIPTN,'4',12,3),1) "
        ''sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        ''sql = sql & N & ",SYSDATE "
        ''sql = sql & N & "FROM ZG340B_W13 WK "
        ''sql = sql & N & "WHERE UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        'sql = sql & N & "SELECT  "
        'sql = sql & N & " WK.KHINMEICD "
        'sql = sql & N & ",WK.TENKAIPTN "
        'sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3),1) "
        'sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3),1) "
        'sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3),1) "
        'sql = sql & N & ",NULL "
        'sql = sql & N & ",NULL "
        'sql = sql & N & ",NULL "
        'sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3),1) "
        'sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3),1) "
        'sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3),1) "
        'sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        'sql = sql & N & ",SYSDATE "
        'sql = sql & N & ",T10.MYMETSUKE "
        'sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12,1) ELSE NULL END  "
        'sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12,1) ELSE NULL END  "
        'sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12,1) ELSE NULL END  "
        'sql = sql & N & ",NULL "
        'sql = sql & N & ",NULL "
        'sql = sql & N & ",NULL "
        'sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12,1) ELSE NULL END  "
        'sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12,1) ELSE NULL END  "
        'sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12,1) ELSE NULL END  "
        sql = sql & N & "SELECT  "
        sql = sql & N & " WK.KHINMEICD "
        sql = sql & N & ",WK.TENKAIPTN "
        '-->2011.01.16 chg by takagi #82
        'sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3)/1000,1) " 'Kg��t
        'sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3)/1000,1) "
        'sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3)/1000,1) "
        sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3)/1000,4) " 'Kg��t
        sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3)/1000,4) "
        sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3)/1000,4) "
        '<--2011.01.16 chg by takagi #82
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        '-->2011.01.16 chg by takagi #82
        'sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3)/1000,1) "
        'sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3)/1000,1) "
        'sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3)/1000,1) "
        sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3)/1000,4) "
        sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3)/1000,4) "
        sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3)/1000,4) "
        '<--2011.01.16 chg by takagi #82
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & ",SYSDATE "
        sql = sql & N & ",T10.MYMETSUKE "
        '-->2011.01.16 chg by takagi #82
        'sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12/1000,1) ELSE NULL END  " 'm��Km
        'sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12/1000,1) ELSE NULL END  "
        'sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12/1000,1) ELSE NULL END  "
        sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12/1000,4) ELSE NULL END  " 'm��Km
        sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12/1000,4) ELSE NULL END  "
        sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12/1000,4) ELSE NULL END  "
        '<--2011.01.16 chg by takagi #82
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        '-->2011.01.16 chg by takagi #82
        'sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12/1000,1) ELSE NULL END  "
        'sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12/1000,1) ELSE NULL END  "
        'sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12/1000,1) ELSE NULL END  "
        sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12/1000,4) ELSE NULL END  "
        sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12/1000,4) ELSE NULL END  "
        sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12/1000,4) ELSE NULL END  "
        '<--2011.01.16 chg by takagi #82
        ''<--2010.12.17 chg by takagi #15
        '-->2010.12.26 chg by takagi #49
        'sql = sql & N & "FROM ZG340B_W13 WK "
        sql = sql & N & "FROM (select * from ZG340B_W13 where tenkaiptn not in ('5T','5Y','5YY')) WK "
        '<--2010.12.26 chg by takagi #49
        sql = sql & N & "LEFT JOIN ( "
        sql = sql & N & "	SELECT "
        sql = sql & N & "	 M.KHINMEICD "
        sql = sql & N & "	,MIN(T.METSUKE) MYMETSUKE "
        sql = sql & N & "	FROM T10HINHANJ       T "
        sql = sql & N & "	INNER JOIN M12SYUYAKU M "
        sql = sql & N & "	ON T.HINMEICD = M.HINMEICD "
        sql = sql & N & "	WHERE T.METSUKE IS NOT NULL "
        sql = sql & N & "	GROUP BY M.KHINMEICD ) T10 ON WK.KHINMEICD = T10.KHINMEICD "
        '<--2010.12.12 chg by takagi
        sql = sql & N & "WHERE UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        _db.executeDB(sql)

        '-->2010.12.26 add by takagi #49
        sql = ""
        sql = sql & N & "INSERT INTO T13HANBAI "
        sql = sql & N & "( "
        sql = sql & N & " KHINMEICD "
        sql = sql & N & ",TENKAIPTN "
        sql = sql & N & ",THANBAIRYOU "
        sql = sql & N & ",YHANBAIRYOU "
        sql = sql & N & ",YYHANBAIRYOU "
        sql = sql & N & ",THANBAIRYOUH "
        sql = sql & N & ",YHANBAIRYOUH "
        sql = sql & N & ",YYHANBAIRYOUH "
        sql = sql & N & ",THANBAIRYOUHK "
        sql = sql & N & ",YHANBAIRYOUHK "
        sql = sql & N & ",YYHANBAIRYOUHK "
        sql = sql & N & ",UPDNAME "
        sql = sql & N & ",UPDDATE "
        sql = sql & N & ",METSUKE "
        sql = sql & N & ",THANBAISU "
        sql = sql & N & ",YHANBAISU "
        sql = sql & N & ",YYHANBAISU "
        sql = sql & N & ",THANBAISUH "
        sql = sql & N & ",YHANBAISUH "
        sql = sql & N & ",YYHANBAISUH "
        sql = sql & N & ",THANBAISUHK "
        sql = sql & N & ",YHANBAISUHK "
        sql = sql & N & ",YYHANBAISUHK "
        sql = sql & N & ") "
        sql = sql & N & "SELECT  "
        sql = sql & N & " WK.KHINMEICD "
        sql = sql & N & ",WK.MAXTENKAIPTN "
        '-->2011.01.16 chg by takagi #82
        'sql = sql & N & ",ROUND(WK.SUMHJISSEKIRYOU_T /3/1000,1) " 'Kg��t
        'sql = sql & N & ",ROUND(WK.SUMHJISSEKIRYOU_Y /3/1000,1) "
        'sql = sql & N & ",ROUND(WK.SUMHJISSEKIRYOU_YY/3/1000,1) "
        sql = sql & N & ",ROUND(WK.SUMHJISSEKIRYOU_T /3/1000,4) " 'Kg��t
        sql = sql & N & ",ROUND(WK.SUMHJISSEKIRYOU_Y /3/1000,4) "
        sql = sql & N & ",ROUND(WK.SUMHJISSEKIRYOU_YY/3/1000,4) "
        '<--2011.01.16 chg by takagi #82
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        '-->2011.01.16 chg by takagi #82
        'sql = sql & N & ",ROUND(WK.SUMHJISSEKIRYOU_T /3/1000,1) "
        'sql = sql & N & ",ROUND(WK.SUMHJISSEKIRYOU_Y /3/1000,1) "
        'sql = sql & N & ",ROUND(WK.SUMHJISSEKIRYOU_YY/3/1000,1) "
        sql = sql & N & ",ROUND(WK.SUMHJISSEKIRYOU_T /3/1000,4) "
        sql = sql & N & ",ROUND(WK.SUMHJISSEKIRYOU_Y /3/1000,4) "
        sql = sql & N & ",ROUND(WK.SUMHJISSEKIRYOU_YY/3/1000,4) "
        '<--2011.01.16 chg by takagi #82
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & ",SYSDATE "
        sql = sql & N & ",T10.MYMETSUKE "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & "FROM ( "
        sql = sql & N & "     SELECT "
        sql = sql & N & "       KHINMEICD "
        sql = sql & N & "      ,SUM(HJISSEKIRYOU_T)  SUMHJISSEKIRYOU_T "
        sql = sql & N & "      ,SUM(HJISSEKISU_T)    SUMHJISSEKISU_T "
        sql = sql & N & "      ,SUM(HJISSEKIRYOU_Y)  SUMHJISSEKIRYOU_Y"
        sql = sql & N & "      ,SUM(HJISSEKISU_Y)    SUMHJISSEKISU_Y "
        sql = sql & N & "      ,SUM(HJISSEKIRYOU_YY) SUMHJISSEKIRYOU_YY "
        sql = sql & N & "      ,SUM(HJISSEKISU_YY)   SUMHJISSEKISU_YY "
        sql = sql & N & "      ,'5'                  MAXTENKAIPTN"
        sql = sql & N & "      ,MAX(UPDNAME)         MAXUPDNAME"
        sql = sql & N & "      ,MAX(UPDDATE)         MAXUPDDATE "
        sql = sql & N & "     FROM ( "
        sql = sql & N & "          SELECT "
        sql = sql & N & "            KHINMEICD "
        sql = sql & N & "           ,HJISSEKIRYOU HJISSEKIRYOU_T "
        sql = sql & N & "           ,HJISSEKISU   HJISSEKISU_T "
        sql = sql & N & "           ,0            HJISSEKIRYOU_Y "
        sql = sql & N & "           ,0            HJISSEKISU_Y "
        sql = sql & N & "           ,0            HJISSEKIRYOU_YY "
        sql = sql & N & "           ,0            HJISSEKISU_YY "
        sql = sql & N & "           ,TENKAIPTN "
        sql = sql & N & "           ,UPDNAME "
        sql = sql & N & "           ,UPDDATE "
        sql = sql & N & "          FROM ZG340B_W13 "
        sql = sql & N & "          WHERE TENKAIPTN  = '5T' "
        sql = sql & N & "          UNION ALL "
        sql = sql & N & "          SELECT "
        sql = sql & N & "            KHINMEICD "
        sql = sql & N & "           ,0            HJISSEKIRYOU_T "
        sql = sql & N & "           ,0            HJISSEKISU_T "
        sql = sql & N & "           ,HJISSEKIRYOU HJISSEKIRYOU_Y "
        sql = sql & N & "           ,HJISSEKISU   HJISSEKISU_Y "
        sql = sql & N & "           ,0            HJISSEKIRYOU_YY "
        sql = sql & N & "           ,0            HJISSEKISU_YY "
        sql = sql & N & "           ,TENKAIPTN "
        sql = sql & N & "           ,UPDNAME "
        sql = sql & N & "           ,UPDDATE "
        sql = sql & N & "          FROM ZG340B_W13 "
        sql = sql & N & "          WHERE TENKAIPTN = '5Y' "
        sql = sql & N & "          UNION ALL "
        sql = sql & N & "          SELECT "
        sql = sql & N & "            KHINMEICD "
        sql = sql & N & "           ,0            HJISSEKIRYOU_T "
        sql = sql & N & "           ,0            HJISSEKISU_T "
        sql = sql & N & "           ,0            HJISSEKIRYOU_Y "
        sql = sql & N & "           ,0            HJISSEKISU_Y "
        sql = sql & N & "           ,HJISSEKIRYOU HJISSEKIRYOU_YY "
        sql = sql & N & "           ,HJISSEKISU   HJISSEKISU_YY "
        sql = sql & N & "           ,TENKAIPTN "
        sql = sql & N & "           ,UPDNAME "
        sql = sql & N & "           ,UPDDATE "
        sql = sql & N & "          FROM ZG340B_W13 "
        sql = sql & N & "          WHERE TENKAIPTN = '5YY' "
        sql = sql & N & "         ) "
        sql = sql & N & "     WHERE UPDNAME   = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & "     GROUP BY KHINMEICD "
        sql = sql & N & "     ) WK "
        sql = sql & N & "LEFT JOIN ( "
        sql = sql & N & "	SELECT "
        sql = sql & N & "	 M.KHINMEICD "
        sql = sql & N & "	,MIN(T.METSUKE) MYMETSUKE "
        sql = sql & N & "	FROM T10HINHANJ       T "
        sql = sql & N & "	INNER JOIN M12SYUYAKU M "
        sql = sql & N & "	ON T.HINMEICD = M.HINMEICD "
        sql = sql & N & "	WHERE T.METSUKE IS NOT NULL "
        sql = sql & N & "	GROUP BY M.KHINMEICD ) T10 ON WK.KHINMEICD = T10.KHINMEICD "
        _db.executeDB(sql)
        '<--2010.12.26 add by takagi #49

    End Sub

    '2-9 ���l�v��ݒ�
    Private Sub insertAnbunRec()

        Dim sql As String = ""
        sql = sql & N & "INSERT INTO T13HANBAI "
        Sql = Sql & N & "( "
        Sql = Sql & N & " KHINMEICD "
        Sql = Sql & N & ",TENKAIPTN "
        Sql = Sql & N & ",THANBAIRYOU "
        Sql = Sql & N & ",YHANBAIRYOU "
        Sql = Sql & N & ",YYHANBAIRYOU "
        Sql = Sql & N & ",THANBAIRYOUH "
        Sql = Sql & N & ",YHANBAIRYOUH "
        Sql = Sql & N & ",YYHANBAIRYOUH "
        Sql = Sql & N & ",THANBAIRYOUHK "
        Sql = Sql & N & ",YHANBAIRYOUHK "
        Sql = Sql & N & ",YYHANBAIRYOUHK "
        Sql = Sql & N & ",UPDNAME "
        Sql = Sql & N & ",UPDDATE "
        '-->2010.12.12 add by takagi
        sql = sql & N & ",METSUKE "
        sql = sql & N & ",THANBAISU "
        sql = sql & N & ",YHANBAISU "
        sql = sql & N & ",YYHANBAISU "
        sql = sql & N & ",THANBAISUH "
        sql = sql & N & ",YHANBAISUH "
        sql = sql & N & ",YYHANBAISUH "
        sql = sql & N & ",THANBAISUHK "
        sql = sql & N & ",YHANBAISUHK "
        sql = sql & N & ",YYHANBAISUHK "
        '<--2010.12.12 add by takagi
        sql = sql & N & ") "
        Sql = Sql & N & "SELECT  "
        Sql = Sql & N & " WK.KHINMEICD "
        Sql = Sql & N & ",WK.TENKAIPTN "
        '-->2011.01.16 chg by takagi #82
        'sql = sql & N & ",ROUND(SUB.THANBAIRYOU * WK.ALLOTMENTRATE/100,1) "
        'sql = sql & N & ",ROUND(SUB.YHANBAIRYOU * WK.ALLOTMENTRATE/100,1) "
        'sql = sql & N & ",ROUND(SUB.YYHANBAIRYOU * WK.ALLOTMENTRATE/100,1) "
        sql = sql & N & ",ROUND(SUB.THANBAIRYOU * WK.ALLOTMENTRATE/100,4) "
        sql = sql & N & ",ROUND(SUB.YHANBAIRYOU * WK.ALLOTMENTRATE/100,4) "
        sql = sql & N & ",ROUND(SUB.YYHANBAIRYOU * WK.ALLOTMENTRATE/100,4) "
        '<--2011.01.16 chg by takagi #82
        sql = sql & N & ",NULL "
        Sql = Sql & N & ",NULL "
        Sql = Sql & N & ",NULL "
        '-->2011.01.16 chg by takagi #82
        'sql = sql & N & ",ROUND(SUB.THANBAIRYOU * WK.ALLOTMENTRATE/100,1) "
        'sql = sql & N & ",ROUND(SUB.YHANBAIRYOU * WK.ALLOTMENTRATE/100,1) "
        'sql = sql & N & ",ROUND(SUB.YYHANBAIRYOU * WK.ALLOTMENTRATE/100,1) "
        sql = sql & N & ",ROUND(SUB.THANBAIRYOU * WK.ALLOTMENTRATE/100,4) "
        sql = sql & N & ",ROUND(SUB.YHANBAIRYOU * WK.ALLOTMENTRATE/100,4) "
        sql = sql & N & ",ROUND(SUB.YYHANBAIRYOU * WK.ALLOTMENTRATE/100,4) "
        '<--2011.01.16 chg by takagi #82
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        Sql = Sql & N & ",SYSDATE "
        '-->2010.12.12 add by takagi
        sql = sql & N & ",T10.MYMETSUKE "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        '<--2010.12.12 add by takagi
        sql = sql & N & "FROM ZG340B_W12 WK "
        Sql = Sql & N & "INNER JOIN ( "
        Sql = Sql & N & "	SELECT "
        sql = sql & N & "	 M11.TT_KHINMEICD "
        sql = sql & N & "	,T11.HINSYUKBN "
        sql = sql & N & "	,T11.JUYOUCD "
        sql = sql & N & "	,T11.THANBAIRYOU "
        sql = sql & N & "	,T11.YHANBAIRYOU "
        sql = sql & N & "	,T11.YYHANBAIRYOU "
        sql = sql & N & "	FROM T11HINSYUHANK T11 "
        sql = sql & N & "	INNER JOIN M11KEIKAKUHIN M11 ON T11.JUYOUCD = M11.TT_JUYOUCD AND T11.HINSYUKBN = M11.TT_HINSYUKBN "
        Sql = Sql & N & "	) SUB "
        sql = sql & N & "ON WK.KHINMEICD = SUB.TT_KHINMEICD "
        '-->2010.12.12 add by takagi
        sql = sql & N & "LEFT JOIN ( "
        sql = sql & N & "	SELECT "
        sql = sql & N & "	 M.KHINMEICD "
        sql = sql & N & "	,MIN(T.METSUKE) MYMETSUKE "
        sql = sql & N & "	FROM T10HINHANJ       T "
        sql = sql & N & "	INNER JOIN M12SYUYAKU M "
        sql = sql & N & "	ON T.HINMEICD = M.HINMEICD "
        sql = sql & N & "	WHERE T.METSUKE IS NOT NULL "
        sql = sql & N & "	GROUP BY M.KHINMEICD ) T10 ON WK.KHINMEICD = T10.KHINMEICD "
        '<--2010.12.12 add by takagi
        sql = sql & N & "WHERE UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        '-->2010.12.22 add by takagi #21
        sql = sql & N & " AND  TENKAIPTN = '1' "
        '<--2010.12.22 add by takagi #21
        _db.executeDB(sql)

    End Sub

    '2-8 �T�C�Y�ʈ����Z�o
    Private Sub updateAnbunRate()

        Dim sql As String = ""
        sql = sql & N & "UPDATE ZG340B_W12 "
        sql = sql & N & "SET ALLOTMENTRATE = DECODE(HJISSEKIRYOU_SUM,0,0,ROUND((HJISSEKIRYOU/HJISSEKIRYOU_SUM)*100,3)) "
        sql = sql & N & "WHERE UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        _db.executeDB(sql)

    End Sub

    '2-7 �i��ʍ��v�⊮
    Private Sub updateHinshuSum()

        Dim sql As String = ""
        sql = sql & N & "UPDATE ZG340B_W12 "
        sql = sql & N & "SET (HJISSEKIRYOU_SUM,HJISSEKISU_SUM) = ( "
        sql = sql & N & "	SELECT W14.HJISSEKIRYOU_SUM,W14.HJISSEKISU_SUM "
        sql = sql & N & "	FROM ZG340B_W14 W14 "
        sql = sql & N & "	INNER JOIN M11KEIKAKUHIN M11 ON W14.HINSYUKBN = M11.TT_HINSYUKBN AND W14.JUYOUCD = M11.TT_JUYOUCD "
        sql = sql & N & "	WHERE ZG340B_W12.KHINMEICD = M11.TT_KHINMEICD "
        sql = sql & N & "	  AND W14.UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & "	) "
        sql = sql & N & "WHERE ZG340B_W12.KHINMEICD IN ( "
        sql = sql & N & "	SELECT M11.TT_KHINMEICD "
        sql = sql & N & "	FROM ZG340B_W14 W14 "
        sql = sql & N & "	INNER JOIN M11KEIKAKUHIN M11 ON W14.HINSYUKBN = M11.TT_HINSYUKBN AND W14.JUYOUCD = M11.TT_JUYOUCD "
        sql = sql & N & "	WHERE ZG340B_W12.KHINMEICD = M11.TT_KHINMEICD "
        sql = sql & N & "	  AND W14.UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & "	) "
        sql = sql & N & "  AND ZG340B_W12.UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        _db.executeDB(sql)

    End Sub

    '2-6 �i��ʍ��v����
    Private Sub summaryHinshuValue()

        Dim sql As String = ""
        sql = sql & N & "INSERT INTO ZG340B_W14 "
        Sql = Sql & N & "( "
        sql = sql & N & " JUYOUCD "
        sql = sql & N & ",HINSYUKBN "
        sql = sql & N & ",HJISSEKIRYOU_SUM "
        Sql = Sql & N & ",HJISSEKISU_SUM "
        Sql = Sql & N & ",UPDNAME "
        Sql = Sql & N & ",UPDDATE "
        Sql = Sql & N & ") "
        Sql = Sql & N & "SELECT "
        sql = sql & N & " M.TT_JUYOUCD "
        sql = sql & N & ",M.TT_HINSYUKBN "
        sql = sql & N & ",SUM(W.HJISSEKIRYOU) "
        sql = sql & N & ",SUM(W.HJISSEKISU) "
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        Sql = Sql & N & ",SYSDATE "
        sql = sql & N & "FROM ZG340B_W12 W INNER JOIN M11KEIKAKUHIN M ON W.KHINMEICD = M.TT_KHINMEICD"
        sql = sql & N & "WHERE UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & "GROUP BY M.TT_JUYOUCD,M.TT_HINSYUKBN "
        _db.executeDB(sql)

    End Sub

    '-------------------------------------------------------------------------------
    '   ���̓`�F�b�N����
    '   �i�����T�v�j�����̓`�F�b�N�A�召�`�F�b�N�A�Ó����`�F�b�N(1�N�ȏ�O�̓��t)���s��
    '   �����̓p�����^  �F�Ȃ�
    '   ���o�̓p�����^  �F�Ȃ�
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub checkInput()
        '���̓`�F�b�N
        If "".Equals(dteKonkaiAnbunFrom.Text.Replace("/", "").Trim) Then                           '������
            Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput"), dteKonkaiAnbunFrom)
        ElseIf "".Equals(dteKonkaiAnbunTo.Text.Replace("/", "").Trim) Then                         '������
            Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput"), dteKonkaiAnbunTo)
        ElseIf (dteKonkaiAnbunFrom.Text) > (dteKonkaiAnbunTo.Text) Then                            '�召�s��
            Throw New UsrDefException("�召�֌W���s���ł��B", _msgHd.getMSG("ErrDaiSyoChk"), dteKonkaiAnbunTo)
        ElseIf DateAdd(DateInterval.Year, 1, CDate(dteKonkaiAnbunFrom.Text & "/01")) < CDate(lblSyoriDate.Text & "/01") Then
            Throw New UsrDefException("�����ȓ��t�����͂���Ă��܂��B", _msgHd.getMSG("ImputedInvalidDate"), dteKonkaiAnbunFrom)
        ElseIf DateAdd(DateInterval.Year, 1, CDate(dteKonkaiAnbunTo.Text & "/01")) < CDate(lblSyoriDate.Text & "/01") Then
            Throw New UsrDefException("�����ȓ��t�����͂���Ă��܂��B", _msgHd.getMSG("ImputedInvalidDate"), dteKonkaiAnbunTo)
        ElseIf CDate(dteKonkaiAnbunFrom.Text & "/01") >= CDate(lblSyoriDate.Text & "/01") Then
            Throw New UsrDefException("�����ȓ��t�����͂���Ă��܂��B", _msgHd.getMSG("ImputedInvalidDate"), dteKonkaiAnbunFrom)
        ElseIf CDate(dteKonkaiAnbunTo.Text & "/01") >= CDate(lblSyoriDate.Text & "/01") Then
            Throw New UsrDefException("�����ȓ��t�����͂���Ă��܂��B", _msgHd.getMSG("ImputedInvalidDate"), dteKonkaiAnbunTo)
        End If
    End Sub

    '-------------------------------------------------------------------------------
    '   �����ԏW�v
    '   �i�����T�v�j�W�J�p�^�[�����P(�i���)�̂��̂�������ʂŎw�肳�ꂽ�W�v���ԂŒ��o���A�v��i���P�ʂŏW�񂷂�
    '   �����̓p�����^  �F�Ȃ�
    '   ���o�̓p�����^  �F�Ȃ�
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub createAnbunWk()

        Dim sql As String = ""
        sql = sql & N & "INSERT INTO ZG340B_W12 "
        sql = sql & N & "( "
        sql = sql & N & " KHINMEICD "
        sql = sql & N & ",HJISSEKIRYOU "
        sql = sql & N & ",HJISSEKISU "
        sql = sql & N & ",TENKAIPTN "
        sql = sql & N & ",UPDNAME "
        sql = sql & N & ",UPDDATE "
        sql = sql & N & ") "
        sql = sql & N & "SELECT "
        sql = sql & N & " KHINMEICD "
        sql = sql & N & ",SUM(HJISSEKIRYOU) "
        sql = sql & N & ",SUM(HJISSEKISU) "
        sql = sql & N & ",MAX(TENKAIPTN) "
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & ",SYSDATE "
        sql = sql & N & "FROM ZG340B_W11 "
        sql = sql & N & "WHERE UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & "  AND NENGETU >= '" & dteKonkaiAnbunFrom.Text.Replace("/", "") & "' "
        sql = sql & N & "  AND NENGETU <= '" & dteKonkaiAnbunTo.Text.Replace("/", "") & "' "
        '-->2010.12.22 del by takagi #21
        'sql = sql & N & "  AND TENKAIPTN = '1' "
        '<--2010.12.22 del by takagi #21
        sql = sql & N & "GROUP BY  KHINMEICD "
        _db.executeDB(sql)

    End Sub

    '-------------------------------------------------------------------------------
    '   �w����ԏW�v
    '   �i�����T�v�j�����Ŏw�肳�ꂽ�W�v���Ԃ̃f�[�^���W�񂷂�
    '   �����̓p�����^  �FprmFromYM �W�v�J�n�N��
    '                   �FprmToYM   �W�v�I���N��
    '                   �FprmPtn    �W�J�p�^�[��
    '   ���o�̓p�����^  �F�Ȃ�
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub createHanbaiJissekiWkByPtn(ByVal prmFromYM As String, ByVal prmToYM As String, ByVal prmPtn As String)

        Dim sql As String = ""
        sql = sql & N & "INSERT INTO ZG340B_W13 "
        sql = sql & N & "( "
        sql = sql & N & " KHINMEICD "
        sql = sql & N & ",HJISSEKIRYOU "
        sql = sql & N & ",HJISSEKISU "
        sql = sql & N & ",TENKAIPTN "
        sql = sql & N & ",UPDNAME "
        sql = sql & N & ",UPDDATE "
        sql = sql & N & ") "
        sql = sql & N & "SELECT "
        sql = sql & N & " KHINMEICD "
        sql = sql & N & ",SUM(HJISSEKIRYOU) "
        sql = sql & N & ",SUM(HJISSEKISU) "
        '-->2010.12.26 chg by takagi #49
        'sql = sql & N & ",MAX(TENKAIPTN) "
        sql = sql & N & ",CASE WHEN '" & prmPtn & "' = '5T'  THEN '5T' "
        sql = sql & N & "      WHEN '" & prmPtn & "' = '5Y'  THEN '5Y' "
        sql = sql & N & "      WHEN '" & prmPtn & "' = '5YY' THEN '5YY' "
        sql = sql & N & "      ELSE MAX(TENKAIPTN) END "
        '<--2010.12.26 chg by takagi #49
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & ",SYSDATE "
        sql = sql & N & "FROM ZG340B_W11 "
        sql = sql & N & "WHERE UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & "  AND NENGETU >= '" & prmFromYM & "' "
        sql = sql & N & "  AND NENGETU <= '" & prmToYM & "' "
        '-->2010.12.26 chg by takagi #49
        'sql = sql & N & "  AND TENKAIPTN = '" & prmPtn & "' "
        If "5T".Equals(prmPtn) OrElse "5Y".Equals(prmPtn) OrElse "5YY".Equals(prmPtn) Then
            sql = sql & N & "  AND TENKAIPTN = '5' "
        Else
            sql = sql & N & "  AND TENKAIPTN = '" & prmPtn & "' "
        End If
        '<--2010.12.26 chg by takagi #49
        sql = sql & N & "GROUP BY  KHINMEICD "
        _db.executeDB(sql)

    End Sub

    '-------------------------------------------------------------------------------
    '   �v��i�����W�v
    '   �i�����T�v�j�v��i���ƔN���P�ʂŔ̔����т��W�v����
    '   �����̓p�����^  �F�Ȃ�
    '   ���o�̓p�����^  �F�Ȃ�
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub createKeikakuhinHanbaiJisseki()
        Dim sql As String = ""
        sql = sql & N & "INSERT INTO ZG340B_W11 "
        sql = sql & N & "( "
        sql = sql & N & " KHINMEICD "
        sql = sql & N & ",NENGETU "
        sql = sql & N & ",HJISSEKIRYOU "
        sql = sql & N & ",HJISSEKISU "
        sql = sql & N & ",TENKAIPTN "
        sql = sql & N & ",UPDNAME "
        sql = sql & N & ",UPDDATE "
        sql = sql & N & ") "
        sql = sql & N & "SELECT "
        sql = sql & N & " KHINMEICD "
        sql = sql & N & ",NENGETU "
        sql = sql & N & ",SUM(HJISSEKIRYOU) "
        sql = sql & N & ",SUM(HJISSEKISU) "
        sql = sql & N & ",MAX(TENKAIPTN) "
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & ",SYSDATE "
        sql = sql & N & "FROM ZG340B_W10 "
        sql = sql & N & "WHERE UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & "GROUP BY  KHINMEICD,NENGETU "
        _db.executeDB(sql)
    End Sub

    '-------------------------------------------------------------------------------
    '   �v��i��CD�⊮
    '   �i�����T�v�j�����N��-12�`�����N��-1�̃f�[�^��̔����т��璊�o���A�����ɓW�J�p�^�[���ƌv��i��CD��⊮����
    '   �����̓p�����^  �F�Ȃ�
    '   ���o�̓p�����^  �F�Ȃ�
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub createHanbaiJisseki()

        Dim sql As String = ""
        sql = sql & N & "INSERT INTO ZG340B_W10 "
        sql = sql & N & "( "
        sql = sql & N & " HINMEICD "
        sql = sql & N & ",KHINMEICD "
        sql = sql & N & ",NENGETU "
        sql = sql & N & ",HJISSEKIRYOU "
        sql = sql & N & ",HJISSEKISU "
        sql = sql & N & ",TENKAIPTN "
        sql = sql & N & ",UPDNAME "
        sql = sql & N & ",UPDDATE "
        sql = sql & N & ") "
        sql = sql & N & "SELECT "
        sql = sql & N & " T10.HINMEICD "
        sql = sql & N & ",M12.KHINMEICD "
        sql = sql & N & ",T10.NENGETU "
        sql = sql & N & ",T10.HJISSEKIRYOU "
        sql = sql & N & ",T10.HJISSEKISU "
        sql = sql & N & ",M11.TT_TENKAIPTN "
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & ",SYSDATE "
        sql = sql & N & "FROM T10HINHANJ T10 "
        sql = sql & N & "INNER JOIN M12SYUYAKU    M12 ON T10.HINMEICD  = M12.HINMEICD "
        sql = sql & N & "INNER JOIN M11KEIKAKUHIN M11 ON M12.KHINMEICD = M11.TT_KHINMEICD "
        sql = sql & N & "  AND M11.TT_SYUBETU = 1 " '�i1�F�݌Ɂj'������2011.01.19 add by takagi
        '-->2010.12.22 chg by takagi #21
        'sql = sql & N & "WHERE M11.TT_TENKAIPTN IN ('1','3','4','5') "
        sql = sql & N & "WHERE M11.TT_TENKAIPTN IN ('1','2','3','4','5') "
        '<--2010.12.22 chg by takagi #21
        '-->2010.12.22 chg by takagi #49
        'sql = sql & N & "  AND T10.NENGETU >= '" & Format(DateAdd(DateInterval.Month, -12, CDate(lblSyoriDate.Text & "/01")), "yyyyMM") & "' "
        sql = sql & N & "  AND T10.NENGETU >= '" & Format(DateAdd(DateInterval.Month, -13, CDate(lblSyoriDate.Text & "/01")), "yyyyMM") & "' "
        '<--2010.12.22 chg by takagi #49
        sql = sql & N & "  AND T10.NENGETU <= '" & Format(DateAdd(DateInterval.Month, -1, CDate(lblSyoriDate.Text & "/01")), "yyyyMM") & "' "
        _db.executeDB(sql)

    End Sub

    '-------------------------------------------------------------------------------
    '   �ʌv��ݒ�
    '   �i�����T�v�j�T�C�Y�ʓW�J�p�^�[�����Q�̃f�[�^���ʌv��(T12)����̔��v��(T13)�ɓ�������
    '   �����̓p�����^  �F�Ȃ�
    '   ���o�̓p�����^  �F�Ȃ�
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    '-->2010.12.27 add by takagi #59
    'Private Sub kobetsuInsert()
    Private Sub kobetsuInsert(ByRef prmRefNullMetsukeCnt As Integer)
        '-->2010.12.27 add by takagi #59

        _db.executeDB("delete from T13HANBAI where KHINMEICD in (select KHINMEICD FROM T12HINMEIHANK WHERE TENKAIPTN = '2')")

        Dim sql As String = ""
        sql = sql & N & "INSERT INTO T13HANBAI "
        sql = sql & N & "( "
        sql = sql & N & " KHINMEICD "
        sql = sql & N & ",TENKAIPTN "
        sql = sql & N & ",THANBAIRYOU "
        sql = sql & N & ",YHANBAIRYOU "
        sql = sql & N & ",YYHANBAIRYOU "
        sql = sql & N & ",THANBAIRYOUH "
        sql = sql & N & ",YHANBAIRYOUH "
        sql = sql & N & ",YYHANBAIRYOUH "
        sql = sql & N & ",THANBAIRYOUHK "
        sql = sql & N & ",YHANBAIRYOUHK "
        sql = sql & N & ",YYHANBAIRYOUHK "
        sql = sql & N & ",UPDNAME "
        sql = sql & N & ",UPDDATE "
        '-->2010.12.27 add by takagi #59
        sql = sql & N & ",METSUKE "
        '<--2010.12.27 add by takagi #59
        sql = sql & N & ") "
        '-->2010.12.02 upd by takagi #TM11���ς���Ă��邱�Ƃ��l�����āA12�̓W�J�p�^�[���͐M�p���Ȃ�
        'sql = sql & N & "SELECT "
        'sql = sql & N & " KHINMEICD "
        'sql = sql & N & ",TENKAIPTN "
        'sql = sql & N & ",THANBAIRYOU "
        'sql = sql & N & ",YHANBAIRYOU "
        'sql = sql & N & ",YYHANBAIRYOU "
        'sql = sql & N & ",NULL "
        'sql = sql & N & ",NULL "
        'sql = sql & N & ",NULL "
        'sql = sql & N & ",THANBAIRYOU "
        'sql = sql & N & ",YHANBAIRYOU "
        'sql = sql & N & ",YYHANBAIRYOU "
        'sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        'sql = sql & N & ",SYSDATE "
        'sql = sql & N & "FROM T12HINMEIHANK "
        'sql = sql & N & "WHERE TENKAIPTN = '2' "
        sql = sql & N & "SELECT "
        sql = sql & N & " T12.KHINMEICD "
        sql = sql & N & ",M11.TT_TENKAIPTN  "
        sql = sql & N & ",T12.THANBAIRYOU "
        sql = sql & N & ",T12.YHANBAIRYOU "
        sql = sql & N & ",T12.YYHANBAIRYOU "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",T12.THANBAIRYOU "
        sql = sql & N & ",T12.YHANBAIRYOU "
        sql = sql & N & ",T12.YYHANBAIRYOU "
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & ",SYSDATE "
        '-->2010.12.27 add by takagi #59
        sql = sql & N & ",M.METSUKE "       '�ڕt
        '<--2010.12.27 add by takagi #59
        sql = sql & N & "FROM T12HINMEIHANK T12 "
        sql = sql & N & "INNER JOIN M11KEIKAKUHIN M11 ON T12.KHINMEICD = M11.TT_KHINMEICD "
        '-->2010.12.27 add by takagi #59
        '2014/06/04 UPD-S Sugano
        'sql = sql & N & "LEFT JOIN "
        'sql = sql & N & "    ( "
        'sql = sql & N & "    SELECT "
        'sql = sql & N & "     (  SHIYO                         "
        'sql = sql & N & "     || LPAD(TO_CHAR(HINSYU)  ,3,'0') "
        'sql = sql & N & "     || LPAD(TO_CHAR(SENSHIN) ,3,'0') "
        'sql = sql & N & "     || LPAD(TO_CHAR(SAIZU)   ,2,'0') "
        'sql = sql & N & "     || LPAD(TO_CHAR(IRO)     ,3,'0'))            HINCD   "  '���i���R�[�h
        'sql = sql & N & "    ,SEISAN_KANSAN                                METSUKE "  '�ڕt
        'sql = sql & N & "    FROM MPESEKKEI "                    '�ޗ��[�}�X�^
        'sql = sql & N & "    WHERE SEKKEI_HUKA = 'A' "
        'sql = sql & N & "      AND SEQ_NO      = 1 "
        'sql = sql & N & "    ) M "
        sql = sql & N & " LEFT JOIN (SELECT "
        sql = sql & N & "             (M1.SHIYO || M1.HINSYU || M1.SENSHIN || M1.SAIZU || M1.IRO) HINCD "
        sql = sql & N & "             ,M1.SEISAN_KANSAN METSUKE "
        sql = sql & N & "             FROM MPESEKKEI1 M1 "
        sql = sql & N & "             INNER JOIN (SELECT "
        sql = sql & N & "                               SHIYO,HINSYU,SENSHIN,SAIZU,IRO,SEKKEI_FUKA,MAX(SEKKEI_KAITEI) KAITEI"
        sql = sql & N & "                         FROM  MPESEKKEI1 "
        sql = sql & N & "                         WHERE SEKKEI_FUKA = 'A' "
        sql = sql & N & "                         GROUP BY SHIYO,HINSYU,SENSHIN,SAIZU,IRO,SEKKEI_FUKA) M2 "
        sql = sql & N & "             ON  M1.SHIYO = M2.SHIYO "
        sql = sql & N & "             AND M1.HINSYU = M2.HINSYU "
        sql = sql & N & "             AND M1.SENSHIN = M2.SENSHIN "
        sql = sql & N & "             AND M1.SAIZU = M2.SAIZU "
        sql = sql & N & "             AND M1.IRO = M2.IRO "
        sql = sql & N & "             AND M1.SEKKEI_FUKA = M2.SEKKEI_FUKA "
        sql = sql & N & "             AND M1.SEKKEI_KAITEI = M2.KAITEI ) M"
        '2014/06/04 UPD-E Sugano
        sql = sql & N & " ON T12.KHINMEICD = M.HINCD "
        '<--2010.12.27 add by takagi #59
        sql = sql & N & "WHERE M11.TT_TENKAIPTN = '2' "
        '<--2010.12.02 upd by takagi #TM11���ς���Ă��邱�Ƃ��l�����āA12�̓W�J�p�^�[���͐M�p���Ȃ�
        _db.executeDB(sql)

        '-->2010.12.27 add by takagi #59
        '�ڕt�擾�s�ǂ̒��o
        sql = ""
        sql = sql & N & "SELECT KHINMEICD  "
        sql = sql & N & "FROM T13HANBAI "
        sql = sql & N & "WHERE TENKAIPTN = '2' "
        sql = sql & N & "  AND METSUKE IS NULL "
        sql = sql & N & "ORDER BY KHINMEICD "
        Dim ds As DataSet = _db.selectDB(sql, RS, prmRefNullMetsukeCnt)
        If prmRefNullMetsukeCnt > 0 Then

            Dim wk As String = ""
            Dim outFilePath As String = ""
            Call UtilClass.dividePathAndFile(StartUp.iniValue.LogFilePath, outFilePath, wk)
            outFilePath = outFilePath & "\" & IMP_LOG_NM

            Dim logBuf As System.Text.StringBuilder = New System.Text.StringBuilder
            logBuf.Append(Format(Now(), "yyyy/MM/dd HH:mm:ss") & "���s" & N)
            logBuf.Append("==========================================================" & N)
            logBuf.Append("���̔����яW�v�W�J�����o�͏��" & N)
            logBuf.Append("  �ޗ��[�}�X�^���o�^�i���R�[�h�i�ڕt�̎擾���s���Ȃ������i���R�[�h�j" & N)
            logBuf.Append("----------------------------------------------------------" & N)
            For i As Integer = 0 To prmRefNullMetsukeCnt - 1
                logBuf.Append(_db.rmNullStr(ds.Tables(RS).Rows(i)("KHINMEICD")) & N)
            Next
            logBuf.Append("==========================================================")
            Dim tw As UtilMDL.Text.UtilTextWriter = New UtilMDL.Text.UtilTextWriter(outFilePath)
            tw.open(False)
            Try
                tw.writeLine(logBuf.ToString)
            Finally
                tw.close()
            End Try

        End If
        '<--2010.12.27 add by takagi #59

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
        sql = sql & N & ",KENNSU1 "    '�p�^�[���P����
        sql = sql & N & ",KENNSU2 "    '�p�^�[���Q����
        sql = sql & N & ",KENNSU3 "    '�p�^�[���R����
        sql = sql & N & ",KENNSU4 "    '�p�^�[���S����
        sql = sql & N & ",KENNSU5 "    '�p�^�[���T����
        sql = sql & N & ",NAME1 "      '����FROM
        sql = sql & N & ",NAME2 "      '����TO
        sql = sql & N & ",UPDNAME "    '�ŏI�X�V��
        sql = sql & N & ",UPDDATE "    '�ŏI�X�V��
        sql = sql & N & ")values( "
        sql = sql & N & "  '" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "' "                             '�����N��
        sql = sql & N & ", '" & _db.rmSQ(lblKeikakuDate.Text.Replace("/", "")) & "' "                           '�v��N��
        sql = sql & N & ", '" & _db.rmSQ(PGID) & "' "                                                           '�@�\ID
        sql = sql & N & ",to_date('" & Format(prmStDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '�����J�n����
        sql = sql & N & ",to_date('" & Format(prmEdDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '�����I������
        sql = sql & N & ", (SELECT COUNT(*) FROM T13HANBAI WHERE TENKAIPTN = '1') "                             'Ptn1����
        sql = sql & N & ", (SELECT COUNT(*) FROM T13HANBAI WHERE TENKAIPTN = '2') "                             'Ptn2����
        sql = sql & N & ", (SELECT COUNT(*) FROM T13HANBAI WHERE TENKAIPTN = '3') "                             'Ptn3����
        sql = sql & N & ", (SELECT COUNT(*) FROM T13HANBAI WHERE TENKAIPTN = '4') "                             'Ptn4����
        sql = sql & N & ", (SELECT COUNT(*) FROM T13HANBAI WHERE TENKAIPTN = '5') "                             'Ptn5����
        sql = sql & N & ", " & dteKonkaiAnbunFrom.Text.Replace("/", "") & " "                                   '����FROM
        sql = sql & N & ", " & dteKonkaiAnbunTo.Text.Replace("/", "") & " "                                     '����TO
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "                                     '�ŏI�X�V��
        sql = sql & N & ",sysdate "                                                                             '�ŏI�X�V��
        sql = sql & N & ") "
        _db.executeDB(sql)

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�t�H�[�J�X�擾�C�x���g
    '------------------------------------------------------------------------------------------------------
    Private Sub txt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dteKonkaiAnbunFrom.GotFocus, dteKonkaiAnbunTo.GotFocus
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
    Private Sub txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dteKonkaiAnbunFrom.KeyPress, dteKonkaiAnbunTo.KeyPress
        Try
            UtilClass.moveNextFocus(Me, e)
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

End Class