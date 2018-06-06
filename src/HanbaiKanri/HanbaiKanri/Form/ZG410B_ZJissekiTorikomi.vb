'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���j�݌Ɏ��ю捞�w��
'    �i�t�H�[��ID�jZG410B_ZJissekiTorikomi
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
Public Class ZG410B_ZJissekiTorikomi
    Inherits System.Windows.Forms.Form

    '-------------------------------------------------------------------------------
    '   �萔��`
    '-------------------------------------------------------------------------------
    Private Const RS As String = "RecSet"                       '�f�[�^�Z�b�g�e�[�u����
    Private Const N As String = ControlChars.NewLine            '���s����
    Private Const PGID As String = "ZG410B"
    Private Const IMP_LOG_NM As String = "�݌Ɏ��ю捞�����o�͏��.txt"

    '-------------------------------------------------------------------------------
    '   �ϐ���`
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu
    Private _updFlg As Boolean = False  '�X�V��
    Private _ukeharaiDb As UtilDBIf     '�d���݌Ɏ擾�p�R�l�N�V����

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

        '�d���݌Ƀ}�X�^�p�R�l�N�V�����̐���
        Dim iniWk As StartUp.iniType = StartUp.iniValue
        _ukeharaiDb = New UtilOleDBDebugger(iniWk.UdlFilePath_Ukeharai, iniWk.LogFilePath, StartUp.DebugMode)
    End Sub

    '-------------------------------------------------------------------------------
    '�@�t�H�[���N���[�Y�C�x���g
    '-------------------------------------------------------------------------------
    Private Sub ZG410B_ZJissekiTorikomi_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            '�d���݌Ƀ}�X�^�p�R�l�N�V�����̔j��
            _ukeharaiDb.close()
        Catch ex As Exception
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@�t�H�[�����[�h�C�x���g
    '-------------------------------------------------------------------------------
    Private Sub ZG410B_ZJissekiTorikomi_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            sql = sql & N & "    ,NAME1 "
            sql = sql & N & "    from T91RIREKI "
            sql = sql & N & "    where PGID     = '" & PGID & "' "
            sql = sql & N & ") "
            sql = sql & N & "where RNUM = 1 "
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)
            If iRecCnt < 1 Then
                '�����Ȃ�
                lblZenkaiJikkouDate.Text = ZC110M_Menu.NON_EXECUTE
                lblZenkaiKensu.Text = ""
                lblZenkaiYM.Text = ""
            Else
                '��������
                lblZenkaiJikkouDate.Text = _db.rmNullDate(ds.Tables(RS).Rows(0)("SDATEEND"))
                lblZenkaiKensu.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU1")), "#,##0")
                Dim wk As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("NAME1"))
                lblZenkaiYM.Text = wk.Substring(0, 4) & "/" & wk.Substring(4, 2)
            End If

            '������s���̕\��
            lblKonkaiYM.Text = Format(DateAdd(DateInterval.Month, -1, CDate(lblSyoriDate.Text & "/01")), "yyyy/MM")
            sql = ""
            sql = sql & N & "select count(*) CNT from �d���݌Ƀ}�X�^ "
            sql = sql & N & "where �N�� = '" & lblKonkaiYM.Text.Replace("/", "") & "'"
            lblKonkaiKensu.Text = Format(_db.rmNullInt(_ukeharaiDb.selectDB(sql, RS).Tables(RS).Rows(0)("CNT")), "#,##0")

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
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmRun")
            If rtn = Windows.Forms.DialogResult.No Or rtn = Windows.Forms.DialogResult.Cancel Then Exit Sub

            Dim cntNullKeikakuHinmei As Integer = 0

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
                    pb.jobName = "�捞���������s���Ă��܂��B"
                    pb.oneStep = 1

                    pb.status = "�捞������" : pb.maxVal = 1
                    _db.executeDB("delete from ZG410B_W10 where UPDNAME = '" & UtilClass.getComputerName() & "'")
                    _db.executeDB("delete from ZG410B_W11 where UPDNAME = '" & UtilClass.getComputerName() & "'")
                    pb.value += 1                                                               '0-0 WK������


                    pb.status = "�݌Ƀf�[�^�擾��"
                    pb.value = 0 : pb.maxVal = CInt(lblKonkaiKensu.Text)
                    Call importZaikoMast(pb)                                                    '1-1 ���ѓ]��

                    pb.status = "�݌Ɏ��уf�[�^�\�z���E�E�E"
                    Call insertZaikoJisseki(cntNullKeikakuHinmei)                               '1-2 �݌Ɏ��ю捞
                    _db.beginTran()
                    Try
                        _db.executeDB("delete from T31ZAIKOJ")
                        _db.executeDB("delete from T72ZAIKOS where NENTETSU = '" & lblKonkaiYM.Text.Replace("/", "") & "'")

                        Dim insCnt As Integer
                        Call insertZaikoJisseki2(insCnt)                                        '1-2 �݌Ɏ��ю捞
                        Call insertZaikoJissekiDB()                                             '1-3 �݌Ɏ��ю捞

                        pb.status = "�X�e�[�^�X�ύX���E�E�E"
                        ed = Now                    '�����I������
                        _parentForm.updateSeigyoTbl(PGID, True, st, ed)                         '2-0 ���Y�ʊm��/����

                        pb.status = "���s�����쐬"
                        insertRireki(insCnt, st, ed)                                            '2-1 ���s�����i�[
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
            Dim optionMsg As String = ""
            ''If cntNullKeikakuHinmei > 0 Then
            ''    optionMsg = "-----------------------------------------------------------------" & N & _
            ''                "�v��i���R�[�h���ƍ��ł��Ȃ����i���R�[�h��" & cntNullKeikakuHinmei & "�����݂��܂����B" & N & _
            ''                "���Y�f�[�^�͎捞����Ă��܂���B" & N & N & _
            ''                "�ڍׂȎ��i���R�[�h�̓��O���m�F���Ă��������B"
            ''End If
            Call _msgHd.dspMSG("completeRun", optionMsg)
            ''If cntNullKeikakuHinmei > 0 Then
            ''    '���O�\��
            ''    Try
            ''        System.Diagnostics.Process.Start(UtilClass.getAppPath(StartUp.assembly) & "\" & IMP_LOG_NM)   '�֘A�t�����A�v���ŋN��
            ''    Catch ex As Exception
            ''    End Try
            ''End If
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
    Private Sub insertRireki(ByVal prmInsCnt As Integer, ByVal prmStDt As Date, ByVal prmEdDt As Date)

        Dim sql As String = ""
        sql = sql & N & "insert into T91RIREKI "
        sql = sql & N & "( "
        sql = sql & N & " SNENGETU "   '�����N��
        sql = sql & N & ",KNENGETU "   '�v��N��
        sql = sql & N & ",PGID "       '�@�\ID
        sql = sql & N & ",SDATESTART " '�����J�n����
        sql = sql & N & ",SDATEEND "   '�����I������
        sql = sql & N & ",KENNSU1 "    '���s����
        sql = sql & N & ",NAME1 "      '�Ώ۔N��
        sql = sql & N & ",UPDNAME "    '�ŏI�X�V��
        sql = sql & N & ",UPDDATE "    '�ŏI�X�V��
        sql = sql & N & ")values( "
        sql = sql & N & "  '" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "' "                             '�����N��
        sql = sql & N & ", '" & _db.rmSQ(lblKeikakuDate.Text.Replace("/", "")) & "' "                           '�v��N��
        sql = sql & N & ", '" & _db.rmSQ(PGID) & "' "                                                           '�@�\ID
        sql = sql & N & ",to_date('" & Format(prmStDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '�����J�n����
        sql = sql & N & ",to_date('" & Format(prmEdDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '�����I������
        'sql = sql & N & ", " & prmInsCnt & " "                                                                  '���s����
        sql = sql & N & ", " & lblKonkaiKensu.Text & " "                                                        '���s����
        sql = sql & N & ", '" & lblKonkaiYM.Text.Replace("/", "") & "' "                                        '�Ώ۔N��
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "                                     '�ŏI�X�V��
        sql = sql & N & ",sysdate "                                                                             '�ŏI�X�V��
        sql = sql & N & ") "
        _db.executeDB(sql)

    End Sub

    '-------------------------------------------------------------------------------
    '   �݌Ɏ��э쐬
    '   �i�����T�v�j���[�N���݌Ɏ��тփf�[�^��������
    '   �����̓p�����^  �F�Ȃ�
    '   ���o�̓p�����^  �F�Ȃ�
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub insertZaikoJisseki(ByRef prmRefNullMetsukeCnt As Integer)

        Dim sql As String = ""
        sql = sql & N & "INSERT INTO ZG410B_W11 "
        sql = sql & N & "( "
        sql = sql & N & " HINMEICD         --���i���R�[�h "
        sql = sql & N & ",KHINMEICD        --�v��i���R�[�h "
        sql = sql & N & ",ZZZAIKOSU        --�O�X�����݌Ɏ��ѐ� "
        sql = sql & N & ",ZZZAIKORYOU      --�O�X�����݌Ɏ��ї� "
        sql = sql & N & ",ZSEISANSU        --�O�����Y���ѐ� "
        sql = sql & N & ",ZSEISANRYOU      --�O�����Y���ї� "
        sql = sql & N & ",ZHANBAISU        --�O���̔����ѐ� "
        sql = sql & N & ",ZHANBAIRYOU      --�O���̔����ї� "
        sql = sql & N & ",ZZAIKOSU         --�O�����݌Ɏ��ѐ� "
        sql = sql & N & ",ZZAIKORYOU       --�O�����݌Ɏ��ї� "
        sql = sql & N & ",UPDNAME          --�[��ID "
        sql = sql & N & ",UPDDATE          --�X�V���� "
        sql = sql & N & ") "
        sql = sql & N & "select "
        sql = sql & N & " S.HINMEICD         --���i���R�[�h "
        sql = sql & N & ",M.KHINMEICD        --�v��i���R�[�h "
        sql = sql & N & ",S.ZZZAIKOSU        --�O�X�����݌Ɏ��ѐ� "
        sql = sql & N & ",S.ZZZAIKORYOU      --�O�X�����݌Ɏ��ї� "
        sql = sql & N & ",S.ZSEISANSU        --�O�����Y���ѐ� "
        sql = sql & N & ",S.ZSEISANRYOU      --�O�����Y���ї� "
        sql = sql & N & ",S.ZHANBAISU        --�O���̔����ѐ� "
        sql = sql & N & ",S.ZHANBAIRYOU      --�O���̔����ї� "
        sql = sql & N & ",S.ZZAIKOSU         --�O�����݌Ɏ��ѐ� "
        sql = sql & N & ",S.ZZAIKORYOU       --�O�����݌Ɏ��ї� "
        sql = sql & N & ",S.UPDNAME          --�[��ID "
        sql = sql & N & ",S.UPDDATE          --�X�V���� "
        sql = sql & N & "FROM ( "
        sql = sql & N & "     SELECT "
        sql = sql & N & "      (W.SHIYO || W.HINSHU || W.SENSHIN || W.SAIZU || W.IRO) HINMEICD --���i���R�[�h "
        sql = sql & N & "     ,SUM(NVL(W.KAGI_ZEN_SU        ,0) + NVL(W.FUNA_ZEN_SU        ,0) + NVL(W.SAPO_ZEN_SU        ,0) + NVL(W.HEN_ZEN_SU        ,0) + NVL(W.KARI_ZEN_SU  ,0) + NVL(W.YU_ZEN_SU   ,0)) ZZZAIKOSU    --�O�X�����݌Ɏ��ѐ� "
        sql = sql & N & "     ,SUM(NVL(W.KAGI_ZEN_DOU       ,0) + NVL(W.FUNA_ZEN_DOU       ,0) + NVL(W.SAPO_ZEN_DOU       ,0) + NVL(W.HEN_ZEN_DOU       ,0) + NVL(W.KARI_ZEN_DOU ,0) + NVL(W.YU_ZEN_DOU  ,0)) ZZZAIKORYOU  --�O�X�����݌Ɏ��ї� "
        sql = sql & N & "     ,SUM(NVL(W.KAGI_TOU_NYUKO_SU  ,0) + NVL(W.FUNA_TOU_NYUKO_SU  ,0) + NVL(W.SAPO_TOU_NYUKO_SU  ,0)                                                                               ) ZSEISANSU    --�O�����Y���ѐ� "
        sql = sql & N & "     ,SUM(NVL(W.KAGI_TOU_NYUKO_DOU ,0) + NVL(W.FUNA_TOU_NYUKO_DOU ,0) + NVL(W.SAPO_TOU_NYUKO_DOU ,0)                                                                               ) ZSEISANRYOU  --�O�����Y���ї� "
        sql = sql & N & "     ,SUM(NVL(W.KAGI_TOU_SHUKO_SU  ,0) + NVL(W.FUNA_TOU_SHUKO_SU  ,0) + NVL(W.SAPO_TOU_SHUKO_SU  ,0) + NVL(W.HEN_TOU_SHUKO_SU  ,0) + NVL(W.KARI_TOU_SU  ,0) + NVL(W.YU_TOU_SU   ,0)) ZHANBAISU    --�O���̔����ѐ� "
        sql = sql & N & "     ,SUM(NVL(W.KAGI_TOU_DHUKO_DOU ,0) + NVL(W.FUNA_TOU_SHUKO_DOU ,0) + NVL(W.SAPO_TOU_SHUKO_DOU ,0) + NVL(W.HEN_TOU_SHUKO_DOU ,0) + NVL(W.KARI_TOU_DOU ,0) + NVL(W.YU_TOU_DOU  ,0)) ZHANBAIRYOU  --�O���̔����ї� "
        sql = sql & N & "     ,SUM( "
        sql = sql & N & "        ( "
        sql = sql & N & "          NVL(W.KAGI_ZEN_SU        ,0) + NVL(W.FUNA_ZEN_SU        ,0) + NVL(W.SAPO_ZEN_SU        ,0) + NVL(W.HEN_ZEN_SU        ,0) + NVL(W.KARI_ZEN_SU  ,0) + NVL(W.YU_ZEN_SU   ,0) --(�O�X�����݌Ɏ��ѐ�) "
        sql = sql & N & "          + "
        sql = sql & N & "          NVL(W.KAGI_TOU_NYUKO_SU  ,0) + NVL(W.FUNA_TOU_NYUKO_SU  ,0) + NVL(W.SAPO_TOU_NYUKO_SU  ,0)                                                                                --(�O�����Y���ѐ�) "
        sql = sql & N & "        ) "
        sql = sql & N & "        - "
        sql = sql & N & "        ( "
        sql = sql & N & "          NVL(W.KAGI_TOU_SHUKO_SU  ,0) + NVL(W.FUNA_TOU_SHUKO_SU  ,0) + NVL(W.SAPO_TOU_SHUKO_SU  ,0) + NVL(W.HEN_TOU_SHUKO_SU  ,0) + NVL(W.KARI_TOU_SU  ,0) + NVL(W.YU_TOU_SU   ,0) --(�O���̔����ѐ�) "
        sql = sql & N & "        ) "
        sql = sql & N & "      )                                                                                                                                                                              ZZAIKOSU     --�O�����݌Ɏ��ѐ� "
        sql = sql & N & "     ,SUM( "
        sql = sql & N & "        ( "
        sql = sql & N & "          NVL(W.KAGI_ZEN_DOU       ,0) + NVL(W.FUNA_ZEN_DOU       ,0) + NVL(W.SAPO_ZEN_DOU       ,0) + NVL(W.HEN_ZEN_DOU       ,0) + NVL(W.KARI_ZEN_DOU ,0) + NVL(W.YU_ZEN_DOU  ,0) --(�O�X�����݌Ɏ��ї�) "
        sql = sql & N & "          + "
        sql = sql & N & "          NVL(W.KAGI_TOU_NYUKO_DOU ,0) + NVL(W.FUNA_TOU_NYUKO_DOU ,0) + NVL(W.SAPO_TOU_NYUKO_DOU ,0)                                                                                --(�O�����Y���ї�) "
        sql = sql & N & "        ) "
        sql = sql & N & "        - "
        sql = sql & N & "        ( "
        sql = sql & N & "          NVL(W.KAGI_TOU_DHUKO_DOU ,0) + NVL(W.FUNA_TOU_SHUKO_DOU ,0) + NVL(W.SAPO_TOU_SHUKO_DOU ,0) + NVL(W.HEN_TOU_SHUKO_DOU ,0) + NVL(W.KARI_TOU_DOU ,0) + NVL(W.YU_TOU_DOU  ,0) --(�O���̔����ї�) "
        sql = sql & N & "        ) "
        sql = sql & N & "      )                                                                                                                                                                              ZZAIKORYOU   --�O�����݌Ɏ��ї� "
        sql = sql & N & "     ,'" & UtilClass.getComputerName() & "'                                                                                                                                          UPDNAME      --�[��ID "
        sql = sql & N & "     ,SYSDATE                                                                                                                                                                        UPDDATE      --�X�V���� "
        sql = sql & N & "     FROM ZG410B_W10 W "
        sql = sql & N & "     WHERE UPDNAME = '" & UtilClass.getComputerName() & "' "
        sql = sql & N & "     GROUP BY (W.SHIYO || W.HINSHU || W.SENSHIN || W.SAIZU || W.IRO) "
        sql = sql & N & "     ) S "
        sql = sql & N & "LEFT JOIN M12SYUYAKU M ON S.HINMEICD = M.HINMEICD "
        _db.executeDB(sql)
        
        sql = ""
        sql = sql & N & "SELECT HINMEICD  "
        sql = sql & N & "FROM ZG410B_W11 "
        sql = sql & N & "WHERE UPDNAME = '" & UtilClass.getComputerName() & "' "
        sql = sql & N & "  AND KHINMEICD IS NULL "
        sql = sql & N & "ORDER BY HINMEICD "
        Dim ds As DataSet = _db.selectDB(sql, RS, prmRefNullMetsukeCnt)
        If prmRefNullMetsukeCnt > 0 Then

            Dim logBuf As System.Text.StringBuilder = New System.Text.StringBuilder
            logBuf.Append(Format(Now(), "yyyy/MM/dd HH:mm:ss") & "���s" & N)
            logBuf.Append("==========================================================" & N)
            logBuf.Append("���݌Ɏ��ю捞�����o�͏��" & N)
            logBuf.Append("  �W��i���}�X�^(M12)���o�^�i���R�[�h�i�v��i���R�[�h�ɕR�t���邱�Ƃ��o���Ȃ��������i���R�[�h�j" & N)
            logBuf.Append("----------------------------------------------------------" & N)
            For i As Integer = 0 To prmRefNullMetsukeCnt - 1
                logBuf.Append(_db.rmNullStr(ds.Tables(RS).Rows(i)("HINMEICD")) & N)
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
    End Sub

    Private Sub insertZaikoJisseki2(ByRef prmRefInsCnt As Integer)

        Dim Sql As String = ""
        Sql = Sql & N & "INSERT INTO T31ZAIKOJ "
        Sql = Sql & N & "( "
        Sql = Sql & N & " KHINMEICD "
        Sql = Sql & N & ",ZZZAIKOSU "
        Sql = Sql & N & ",ZZZAIKORYOU "
        Sql = Sql & N & ",ZSEISANSU "
        Sql = Sql & N & ",ZSEISANRYOU "
        Sql = Sql & N & ",ZHANBAISU "
        Sql = Sql & N & ",ZHANBAIRYOU "
        Sql = Sql & N & ",ZZAIKOSU "
        Sql = Sql & N & ",ZZAIKORYOU "
        Sql = Sql & N & ",UPDNAME "
        Sql = Sql & N & ",UPDDATE "
        Sql = Sql & N & ") "
        Sql = Sql & N & "SELECT  "
        Sql = Sql & N & " SUB.KHINMEICD "
        Sql = Sql & N & ",SUB.SUM_ZZZAIKOSU "
        Sql = Sql & N & ",SUB.SUM_ZZZAIKORYOU "
        Sql = Sql & N & ",SUB.SUM_ZSEISANSU "
        Sql = Sql & N & ",SUB.SUM_ZSEISANRYOU "
        Sql = Sql & N & ",SUB.SUM_ZHANBAISU "
        Sql = Sql & N & ",SUB.SUM_ZHANBAIRYOU "
        Sql = Sql & N & ",SUB.SUM_ZZAIKOSU "
        Sql = Sql & N & ",SUB.SUM_ZZAIKORYOU "
        Sql = Sql & N & ",SUB.MAX_UPDNAME "
        Sql = Sql & N & ",SUB.SYSDT "
        Sql = Sql & N & "FROM ( "
        Sql = Sql & N & "     SELECT  "
        Sql = Sql & N & "      KHINMEICD        KHINMEICD "
        Sql = Sql & N & "     ,SUM(ZZZAIKOSU)   SUM_ZZZAIKOSU "
        Sql = Sql & N & "     ,SUM(ZZZAIKORYOU) SUM_ZZZAIKORYOU "
        Sql = Sql & N & "     ,SUM(ZSEISANSU)   SUM_ZSEISANSU "
        Sql = Sql & N & "     ,SUM(ZSEISANRYOU) SUM_ZSEISANRYOU "
        Sql = Sql & N & "     ,SUM(ZHANBAISU)   SUM_ZHANBAISU "
        Sql = Sql & N & "     ,SUM(ZHANBAIRYOU) SUM_ZHANBAIRYOU "
        Sql = Sql & N & "     ,SUM(ZZAIKOSU)    SUM_ZZAIKOSU "
        Sql = Sql & N & "     ,SUM(ZZAIKORYOU)  SUM_ZZAIKORYOU "
        Sql = Sql & N & "     ,MAX(UPDNAME)     MAX_UPDNAME "
        Sql = Sql & N & "     ,SYSDATE          SYSDT "
        Sql = Sql & N & "     FROM ZG410B_W11 "
        Sql = Sql & N & "     WHERE KHINMEICD IS NOT NULL "
        Sql = Sql & N & "       AND UPDNAME   = '" & UtilClass.getComputerName() & "' "
        Sql = Sql & N & "     GROUP BY KHINMEICD "
        Sql = Sql & N & "     ) SUB "
        Sql = Sql & N & "INNER JOIN M11KEIKAKUHIN M11"
        Sql = Sql & N & " ON   SUB.KHINMEICD = M11.TT_KHINMEICD "
        Sql = Sql & N & "  AND M11.TT_SYUBETU = 1 " '�i1�F�݌Ɂj
        _db.executeDB(Sql, prmRefInsCnt)

    End Sub

    '-------------------------------------------------------------------------------
    '   �݌Ɏ���DB�쐬
    '   �i�����T�v�j���[�N���݌Ɏ���DB(T72)�փf�[�^��������
    '   �����̓p�����^  �F�Ȃ�
    '   ���o�̓p�����^  �F�Ȃ�
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub insertZaikoJissekiDB()

        Dim sql As String = ""
        Sql = Sql & N & "INSERT INTO T72ZAIKOS "
        Sql = Sql & N & "( "
        sql = sql & N & " NENTETSU                       --�N�� "
        sql = sql & N & ",SHIYO                          --�i��CD�d�l "
        sql = sql & N & ",HINSHU                         --�i��CD�i�� "
        sql = sql & N & ",SENSHIN                        --�i��CD���S�� "
        sql = sql & N & ",SAIZU                          --�i��CD�T�C�Y "
        sql = sql & N & ",IRO                            --�i��CD�F "
        sql = sql & N & ",FUKA                           --�i��CD�݌v�t���L�� "
        sql = sql & N & ",KAITEI                         --�i��CD�݌v�����L�� "
        '-->2010.12.10 add by takagi
        sql = sql & N & ",HINKBN2 "
        '<--2010.12.10 add by takagi
        sql = sql & N & ",HINSHUMEI                      --�i�햼 "
        sql = sql & N & ",SAIZUMEI                       --�T�C�Y�� "
        sql = sql & N & ",IROMEI                         --�F�� "
        sql = sql & N & ",TANI                           --�P�� "
        sql = sql & N & ",KAGI_ZEN_SU                    --�b��O���݌ɐ� "
        sql = sql & N & ",KAGI_ZEN_KINGAKU               --�b��O���݌ɋ��z "
        sql = sql & N & ",KAGI_ZEN_DOU                   --�b��O���݌ɓ��� "
        sql = sql & N & ",KAGI_TOU_NYUKO_SU              --�b�擖�����ɐ� "
        sql = sql & N & ",KAGI_TOU_NYUKO_KINGAKU         --�b�擖�����ɋ��z "
        sql = sql & N & ",KAGI_TOU_NYUKO_DOU             --�b�擖�����ɓ��� "
        sql = sql & N & ",KAGI_TOU_SHUKO_SU              --�b�擖���o�ɐ� "
        sql = sql & N & ",KAGI_TOU_SHUKO_KINGAKU         --�b�擖���o�ɋ��z "
        sql = sql & N & ",KAGI_TOU_DHUKO_DOU             --�b�擖���o�ɓ��� "
        sql = sql & N & ",FUNA_ZEN_SU                    --�D���O���݌ɐ� "
        sql = sql & N & ",FUNA_ZEN_KINGAKU               --�D���O���݌ɋ��z "
        sql = sql & N & ",FUNA_ZEN_DOU                   --�D���O���݌ɓ��� "
        sql = sql & N & ",FUNA_TOU_NYUKO_SU              --�D���������ɐ� "
        sql = sql & N & ",FUNA_TOU_NYUKO_KINGAKU         --�D���������ɋ��z "
        sql = sql & N & ",FUNA_TOU_NYUKO_DOU             --�D���������ɓ��� "
        sql = sql & N & ",FUNA_TOU_SHUKO_SU              --�D�������o�ɐ� "
        sql = sql & N & ",FUNA_TOU_SHUKO_KINGAKU         --�D�������o�ɋ��z "
        sql = sql & N & ",FUNA_TOU_SHUKO_DOU             --�D�������o�ɓ��� "
        sql = sql & N & ",SAPO_ZEN_SU                    --�D�y�O���݌ɐ� "
        sql = sql & N & ",SAPO_ZEN_KINGAKU               --�D�y�O���݌ɋ��z "
        sql = sql & N & ",SAPO_ZEN_DOU                   --�D�y�O���݌ɓ��� "
        sql = sql & N & ",SAPO_TOU_NYUKO_SU              --�D�y�������ɐ� "
        sql = sql & N & ",SAPO_TOU_NYUKO_KINGAKU         --�D�y�������ɋ��z "
        sql = sql & N & ",SAPO_TOU_NYUKO_DOU             --�D�y�������ɓ��� "
        sql = sql & N & ",SAPO_TOU_SHUKO_SU              --�D�y�����o�ɐ� "
        sql = sql & N & ",SAPO_TOU_SHUKO_KINGAKU         --�D�y�����o�ɋ��z "
        sql = sql & N & ",SAPO_TOU_SHUKO_DOU             --�D�y�����o�ɓ��� "
        sql = sql & N & ",HEN_ZEN_SU                     --�ԕi�O���݌ɐ� "
        sql = sql & N & ",HEN_ZEN_KINGAKU                --�ԕi�O���݌ɋ��z "
        sql = sql & N & ",HEN_ZEN_DOU                    --�ԕi�O���݌ɓ��� "
        sql = sql & N & ",HEN_TOU_SHUKO_SU               --�ԕi�����o�ɐ� "
        sql = sql & N & ",HEN_TOU_SHUKO_KINGAKU          --�ԕi�����o�ɋ��z "
        sql = sql & N & ",HEN_TOU_SHUKO_DOU              --�ԕi�����o�ɓ��� "
        sql = sql & N & ",KARI_ZEN_SU                    --���o�בO���݌ɐ� "
        sql = sql & N & ",KARI_ZEN_KINGAKU               --���o�בO���݌ɋ��z "
        sql = sql & N & ",KARI_ZEN_DOU                   --���o�בO���݌ɓ��� "
        sql = sql & N & ",KARI_TOU_SU                    --���o�ד����o�ɐ� "
        sql = sql & N & ",KARI_TOU_KINGAKU               --���o�ד����o�ɋ��z "
        sql = sql & N & ",KARI_TOU_DOU                   --���o�ד����o�ɓ��� "
        sql = sql & N & ",COST                           --�����R�X�g "
        sql = sql & N & ",YU_ZEN_SU                      --�A�����O���݌ɐ� "
        sql = sql & N & ",YU_ZEN_KINGAKU                 --�A�����O���݌ɋ��z "
        sql = sql & N & ",YU_ZEN_DOU                     --�A�����O���݌ɓ��� "
        sql = sql & N & ",YU_TOU_SU                      --�A���������o�ɐ� "
        sql = sql & N & ",YU_TOU_KNGAKU                  --�A���������o�ɋ��z "
        sql = sql & N & ",YU_TOU_DOU                     --�A���������o�ɓ��� "
        sql = sql & N & ",CREATEDT                       --�쐬���� "
        sql = sql & N & ",UPDDT                          --�X�V���� "
        sql = sql & N & ") "
        Sql = Sql & N & "SELECT "
        sql = sql & N & " NENTETSU                       --�N�� "
        sql = sql & N & ",SHIYO                          --�i��CD�d�l "
        sql = sql & N & ",HINSHU                         --�i��CD�i�� "
        sql = sql & N & ",SENSHIN                        --�i��CD���S�� "
        sql = sql & N & ",SAIZU                          --�i��CD�T�C�Y "
        sql = sql & N & ",IRO                            --�i��CD�F "
        sql = sql & N & ",FUKA                           --�i��CD�݌v�t���L�� "
        sql = sql & N & ",KAITEI                         --�i��CD�݌v�����L�� "
        '-->2010.12.10 add by takagi
        sql = sql & N & ",HINKBN2 "
        '<--2010.12.10 add by takagi
        sql = sql & N & ",HINSHUMEI                      --�i�햼 "
        sql = sql & N & ",SAIZUMEI                       --�T�C�Y�� "
        sql = sql & N & ",IROMEI                         --�F�� "
        sql = sql & N & ",TANI                           --�P�� "
        sql = sql & N & ",KAGI_ZEN_SU                    --�b��O���݌ɐ� "
        sql = sql & N & ",KAGI_ZEN_KINGAKU               --�b��O���݌ɋ��z "
        sql = sql & N & ",KAGI_ZEN_DOU                   --�b��O���݌ɓ��� "
        sql = sql & N & ",KAGI_TOU_NYUKO_SU              --�b�擖�����ɐ� "
        sql = sql & N & ",KAGI_TOU_NYUKO_KINGAKU         --�b�擖�����ɋ��z "
        sql = sql & N & ",KAGI_TOU_NYUKO_DOU             --�b�擖�����ɓ��� "
        sql = sql & N & ",KAGI_TOU_SHUKO_SU              --�b�擖���o�ɐ� "
        sql = sql & N & ",KAGI_TOU_SHUKO_KINGAKU         --�b�擖���o�ɋ��z "
        sql = sql & N & ",KAGI_TOU_DHUKO_DOU             --�b�擖���o�ɓ��� "
        sql = sql & N & ",FUNA_ZEN_SU                    --�D���O���݌ɐ� "
        sql = sql & N & ",FUNA_ZEN_KINGAKU               --�D���O���݌ɋ��z "
        sql = sql & N & ",FUNA_ZEN_DOU                   --�D���O���݌ɓ��� "
        sql = sql & N & ",FUNA_TOU_NYUKO_SU              --�D���������ɐ� "
        sql = sql & N & ",FUNA_TOU_NYUKO_KINGAKU         --�D���������ɋ��z "
        sql = sql & N & ",FUNA_TOU_NYUKO_DOU             --�D���������ɓ��� "
        sql = sql & N & ",FUNA_TOU_SHUKO_SU              --�D�������o�ɐ� "
        sql = sql & N & ",FUNA_TOU_SHUKO_KINGAKU         --�D�������o�ɋ��z "
        sql = sql & N & ",FUNA_TOU_SHUKO_DOU             --�D�������o�ɓ��� "
        sql = sql & N & ",SAPO_ZEN_SU                    --�D�y�O���݌ɐ� "
        sql = sql & N & ",SAPO_ZEN_KINGAKU               --�D�y�O���݌ɋ��z "
        sql = sql & N & ",SAPO_ZEN_DOU                   --�D�y�O���݌ɓ��� "
        sql = sql & N & ",SAPO_TOU_NYUKO_SU              --�D�y�������ɐ� "
        sql = sql & N & ",SAPO_TOU_NYUKO_KINGAKU         --�D�y�������ɋ��z "
        sql = sql & N & ",SAPO_TOU_NYUKO_DOU             --�D�y�������ɓ��� "
        sql = sql & N & ",SAPO_TOU_SHUKO_SU              --�D�y�����o�ɐ� "
        sql = sql & N & ",SAPO_TOU_SHUKO_KINGAKU         --�D�y�����o�ɋ��z "
        sql = sql & N & ",SAPO_TOU_SHUKO_DOU             --�D�y�����o�ɓ��� "
        sql = sql & N & ",HEN_ZEN_SU                     --�ԕi�O���݌ɐ� "
        sql = sql & N & ",HEN_ZEN_KINGAKU                --�ԕi�O���݌ɋ��z "
        sql = sql & N & ",HEN_ZEN_DOU                    --�ԕi�O���݌ɓ��� "
        sql = sql & N & ",HEN_TOU_SHUKO_SU               --�ԕi�����o�ɐ� "
        sql = sql & N & ",HEN_TOU_SHUKO_KINGAKU          --�ԕi�����o�ɋ��z "
        sql = sql & N & ",HEN_TOU_SHUKO_DOU              --�ԕi�����o�ɓ��� "
        sql = sql & N & ",KARI_ZEN_SU                    --���o�בO���݌ɐ� "
        sql = sql & N & ",KARI_ZEN_KINGAKU               --���o�בO���݌ɋ��z "
        sql = sql & N & ",KARI_ZEN_DOU                   --���o�בO���݌ɓ��� "
        sql = sql & N & ",KARI_TOU_SU                    --���o�ד����o�ɐ� "
        sql = sql & N & ",KARI_TOU_KINGAKU               --���o�ד����o�ɋ��z "
        sql = sql & N & ",KARI_TOU_DOU                   --���o�ד����o�ɓ��� "
        sql = sql & N & ",COST                           --�����R�X�g "
        sql = sql & N & ",YU_ZEN_SU                      --�A�����O���݌ɐ� "
        sql = sql & N & ",YU_ZEN_KINGAKU                 --�A�����O���݌ɋ��z "
        sql = sql & N & ",YU_ZEN_DOU                     --�A�����O���݌ɓ��� "
        sql = sql & N & ",YU_TOU_SU                      --�A���������o�ɐ� "
        sql = sql & N & ",YU_TOU_KNGAKU                  --�A���������o�ɋ��z "
        sql = sql & N & ",YU_TOU_DOU                     --�A���������o�ɓ��� "
        sql = sql & N & ",ORG_CREATEDT                   --�I���W�i���쐬���� "
        sql = sql & N & ",ORG_UPDDT                      --�I���W�i���X�V���� "
        sql = sql & N & "FROM  ZG410B_W10  "                  '�݌Ɏ���WK
        sql = sql & N & "WHERE UPDNAME = '" & UtilClass.getComputerName() & "' "
        _db.executeDB(Sql)

    End Sub

    '-------------------------------------------------------------------------------
    '   �d���݌Ƀf�[�^�捞
    '   �i�����T�v�jSQLSV(�d���݌Ƀ}�X�^)����OracleWK�փf�[�^����荞��
    '   �����̓p�����^  �F�v���O���X�o�[
    '   ���o�̓p�����^  �F�Ȃ�
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub importZaikoMast(ByRef prmRefPb As UtilProgressBar)

        Dim sysdate As String = "to_date('" & Format(Now, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS')"

        'SQLSV��蒊�o
        Dim sql As String = ""
        Dim iRecCnt As Integer = 0
        sql = ""
        sql = sql & N & "SELECT "
        sql = sql & N & " [�N��] "
        sql = sql & N & ",[�i��CD�d�l] "
        sql = sql & N & ",[�i��CD�i��] "
        sql = sql & N & ",[�i��CD���S��] "
        sql = sql & N & ",[�i��CD�T�C�Y] "
        sql = sql & N & ",[�i��CD�F] "
        sql = sql & N & ",[�i��CD�݌v�t���L��] "
        sql = sql & N & ",[�i��CD�݌v�����L��] "
        sql = sql & N & ",[�i�햼] "
        sql = sql & N & ",[�T�C�Y��] "
        sql = sql & N & ",[�F��] "
        sql = sql & N & ",[�P��] "
        sql = sql & N & ",[�b��O���݌ɐ�] "
        sql = sql & N & ",[�b��O���݌ɋ��z] "
        sql = sql & N & ",[�b��O���݌ɓ���] "
        sql = sql & N & ",[�b�擖�����ɐ�] "
        sql = sql & N & ",[�b�擖�����ɋ��z] "
        sql = sql & N & ",[�b�擖�����ɓ���] "
        sql = sql & N & ",[�b�擖���o�ɐ�] "
        sql = sql & N & ",[�b�擖���o�ɋ��z] "
        sql = sql & N & ",[�b�擖���o�ɓ���] "
        sql = sql & N & ",[�D���O���݌ɐ�] "
        sql = sql & N & ",[�D���O���݌ɋ��z] "
        sql = sql & N & ",[�D���O���݌ɓ���] "
        sql = sql & N & ",[�D���������ɐ�] "
        sql = sql & N & ",[�D���������ɋ��z] "
        sql = sql & N & ",[�D���������ɓ���] "
        sql = sql & N & ",[�D�������o�ɐ�] "
        sql = sql & N & ",[�D�������o�ɋ��z] "
        sql = sql & N & ",[�D�������o�ɓ���] "
        sql = sql & N & ",[�D�y�O���݌ɐ�] "
        sql = sql & N & ",[�D�y�O���݌ɋ��z] "
        sql = sql & N & ",[�D�y�O���݌ɓ���] "
        sql = sql & N & ",[�D�y�������ɐ�] "
        sql = sql & N & ",[�D�y�������ɋ��z] "
        sql = sql & N & ",[�D�y�������ɓ���] "
        sql = sql & N & ",[�D�y�����o�ɐ�] "
        sql = sql & N & ",[�D�y�����o�ɋ��z] "
        sql = sql & N & ",[�D�y�����o�ɓ���] "
        sql = sql & N & ",[�ԕi�O���݌ɐ�] "
        sql = sql & N & ",[�ԕi�O���݌ɋ��z] "
        sql = sql & N & ",[�ԕi�O���݌ɓ���] "
        sql = sql & N & ",[�ԕi�����o�ɐ�] "
        sql = sql & N & ",[�ԕi�����o�ɋ��z] "
        sql = sql & N & ",[�ԕi�����o�ɓ���] "
        sql = sql & N & ",[���o�בO���݌ɐ�] "
        sql = sql & N & ",[���o�בO���݌ɋ��z] "
        sql = sql & N & ",[���o�בO���݌ɓ���] "
        sql = sql & N & ",[���o�ד����o�ɐ�] "
        sql = sql & N & ",[���o�ד����o�ɋ��z] "
        sql = sql & N & ",[���o�ד����o�ɓ���] "
        sql = sql & N & ",[�����R�X�g] "
        sql = sql & N & ",[�A�����O���݌ɐ�] "
        sql = sql & N & ",[�A�����O���݌ɋ��z] "
        sql = sql & N & ",[�A�����O���݌ɓ���] "
        sql = sql & N & ",[�A���������o�ɐ�] "
        sql = sql & N & ",[�A���������o�ɋ��z] "
        sql = sql & N & ",[�A���������o�ɓ���] "
        sql = sql & N & ",[�쐬����] "
        sql = sql & N & ",[�X�V����] "
        '-->2010.12.10 add by takagi
        sql = sql & N & ",M.[�i���敪�Q] "
        '<--2010.12.10 add by takagi
        sql = sql & N & "  FROM �d���݌Ƀ}�X�^ "
        '-->2010.12.10 add by takagi
        sql = sql & N & " INNER JOIN " & StartUp.iniValue.LinkSvForHanbai & ".�̔�." & StartUp.iniValue.TableOwner & ".[T_�i���敪�}�X�^�i�d�����i�j] M "
        sql = sql & N & " ON �d���݌Ƀ}�X�^.[�i��CD�i��] = M.�i��R�[�h"
        '<--2010.12.10 add by takagi
        sql = sql & N & "where �N�� = '" & lblKonkaiYM.Text.Replace("/", "") & "'"
        Dim ds As DataSet = _ukeharaiDb.selectDB(sql, RS, iRecCnt)     '�d���݌Ƀf�[�^�p�R�l�N�V������Select���s

        'ORACLE�փC���T�[�g
        Dim sqlBuf As System.Text.StringBuilder = New System.Text.StringBuilder
        Dim sqlBufF As System.Text.StringBuilder = New System.Text.StringBuilder
        sqlBufF.Append(N & "insert into ZG410B_W10 ")
        sqlBufF.Append(N & "( ")
        sqlBufF.Append(N & " NENTETSU ")                    '�N��
        sqlBufF.Append(N & ",SHIYO ")                       '�i��CD�d�l
        sqlBufF.Append(N & ",HINSHU ")                      '�i��CD�i��
        sqlBufF.Append(N & ",SENSHIN ")                     '�i��CD���S��
        sqlBufF.Append(N & ",SAIZU ")                       '�i��CD�T�C�Y
        sqlBufF.Append(N & ",IRO ")                         '�i��CD�F
        sqlBufF.Append(N & ",FUKA ")                        '�i��CD�݌v�t���L��
        sqlBufF.Append(N & ",KAITEI ")                      '�i��CD�݌v�����L��
        sqlBufF.Append(N & ",HINSHUMEI ")                   '�i�햼
        sqlBufF.Append(N & ",SAIZUMEI ")                    '�T�C�Y��
        sqlBufF.Append(N & ",IROMEI ")                      '�F��
        sqlBufF.Append(N & ",TANI ")                        '�P��
        sqlBufF.Append(N & ",KAGI_ZEN_SU ")                 '�b��O���݌ɐ�
        sqlBufF.Append(N & ",KAGI_ZEN_KINGAKU ")            '�b��O���݌ɋ��z
        sqlBufF.Append(N & ",KAGI_ZEN_DOU ")                '�b��O���݌ɓ���
        sqlBufF.Append(N & ",KAGI_TOU_NYUKO_SU ")           '�b�擖�����ɐ�
        sqlBufF.Append(N & ",KAGI_TOU_NYUKO_KINGAKU ")      '�b�擖�����ɋ��z
        sqlBufF.Append(N & ",KAGI_TOU_NYUKO_DOU ")          '�b�擖�����ɓ���
        sqlBufF.Append(N & ",KAGI_TOU_SHUKO_SU ")           '�b�擖���o�ɐ�
        sqlBufF.Append(N & ",KAGI_TOU_SHUKO_KINGAKU ")      '�b�擖���o�ɋ��z
        sqlBufF.Append(N & ",KAGI_TOU_DHUKO_DOU ")          '�b�擖���o�ɓ���
        sqlBufF.Append(N & ",FUNA_ZEN_SU ")                 '�D���O���݌ɐ�
        sqlBufF.Append(N & ",FUNA_ZEN_KINGAKU ")            '�D���O���݌ɋ��z
        sqlBufF.Append(N & ",FUNA_ZEN_DOU ")                '�D���O���݌ɓ���
        sqlBufF.Append(N & ",FUNA_TOU_NYUKO_SU ")           '�D���������ɐ�
        sqlBufF.Append(N & ",FUNA_TOU_NYUKO_KINGAKU ")      '�D���������ɋ��z
        sqlBufF.Append(N & ",FUNA_TOU_NYUKO_DOU ")          '�D���������ɓ���
        sqlBufF.Append(N & ",FUNA_TOU_SHUKO_SU ")           '�D�������o�ɐ�
        sqlBufF.Append(N & ",FUNA_TOU_SHUKO_KINGAKU ")      '�D�������o�ɋ��z
        sqlBufF.Append(N & ",FUNA_TOU_SHUKO_DOU ")          '�D�������o�ɓ���
        sqlBufF.Append(N & ",SAPO_ZEN_SU ")                 '�D�y�O���݌ɐ�
        sqlBufF.Append(N & ",SAPO_ZEN_KINGAKU ")            '�D�y�O���݌ɋ��z
        sqlBufF.Append(N & ",SAPO_ZEN_DOU ")                '�D�y�O���݌ɓ���
        sqlBufF.Append(N & ",SAPO_TOU_NYUKO_SU ")           '�D�y�������ɐ�
        sqlBufF.Append(N & ",SAPO_TOU_NYUKO_KINGAKU ")      '�D�y�������ɋ��z
        sqlBufF.Append(N & ",SAPO_TOU_NYUKO_DOU ")          '�D�y�������ɓ���
        sqlBufF.Append(N & ",SAPO_TOU_SHUKO_SU ")           '�D�y�����o�ɐ�
        sqlBufF.Append(N & ",SAPO_TOU_SHUKO_KINGAKU ")      '�D�y�����o�ɋ��z
        sqlBufF.Append(N & ",SAPO_TOU_SHUKO_DOU ")          '�D�y�����o�ɓ���
        sqlBufF.Append(N & ",HEN_ZEN_SU ")                  '�ԕi�O���݌ɐ�
        sqlBufF.Append(N & ",HEN_ZEN_KINGAKU ")             '�ԕi�O���݌ɋ��z
        sqlBufF.Append(N & ",HEN_ZEN_DOU ")                 '�ԕi�O���݌ɓ���
        sqlBufF.Append(N & ",HEN_TOU_SHUKO_SU ")            '�ԕi�����o�ɐ�
        sqlBufF.Append(N & ",HEN_TOU_SHUKO_KINGAKU ")       '�ԕi�����o�ɋ��z
        sqlBufF.Append(N & ",HEN_TOU_SHUKO_DOU ")           '�ԕi�����o�ɓ���
        sqlBufF.Append(N & ",KARI_ZEN_SU ")                 '���o�בO���݌ɐ�
        sqlBufF.Append(N & ",KARI_ZEN_KINGAKU ")            '���o�בO���݌ɋ��z
        sqlBufF.Append(N & ",KARI_ZEN_DOU ")                '���o�בO���݌ɓ���
        sqlBufF.Append(N & ",KARI_TOU_SU ")                 '���o�ד����o�ɐ�
        sqlBufF.Append(N & ",KARI_TOU_KINGAKU ")            '���o�ד����o�ɋ��z
        sqlBufF.Append(N & ",KARI_TOU_DOU ")                '���o�ד����o�ɓ���
        sqlBufF.Append(N & ",COST ")                        '�����R�X�g
        sqlBufF.Append(N & ",YU_ZEN_SU ")                   '�A�����O���݌ɐ�
        sqlBufF.Append(N & ",YU_ZEN_KINGAKU ")              '�A�����O���݌ɋ��z
        sqlBufF.Append(N & ",YU_ZEN_DOU ")                  '�A�����O���݌ɓ���
        sqlBufF.Append(N & ",YU_TOU_SU ")                   '�A���������o�ɐ�
        sqlBufF.Append(N & ",YU_TOU_KNGAKU ")               '�A���������o�ɋ��z
        sqlBufF.Append(N & ",YU_TOU_DOU ")                  '�A���������o�ɓ���
        sqlBufF.Append(N & ",ORG_CREATEDT ")                '�I���W�i���쐬����
        sqlBufF.Append(N & ",ORG_UPDDT ")                   '�I���W�i���X�V����
        sqlBufF.Append(N & ",UPDNAME ")                     '�[��ID
        sqlBufF.Append(N & ",UPDDATE ")                     '�X�V����
        '-->2010.12.10 add by takagi
        sqlBufF.Append(N & ",HINKBN2")
        '<--2010.12.10 add by takagi
        sqlBufF.Append(N & ")values( ")
        With ds.Tables(RS)
            For i As Integer = 0 To iRecCnt - 1
                sqlBuf.Remove(0, sqlBuf.Length)
                sqlBuf.Append(N & "  " & convNullStr(.Rows(i)("�N��")))                                  'NENTETSU
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�i��CD�d�l")))                            'SHIYO
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�i��CD�i��")))                            'HINSHU
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�i��CD���S��")))                          'SENSHIN
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�i��CD�T�C�Y")))                          'SAIZU
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�i��CD�F")))                              'IRO
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�i��CD�݌v�t���L��")))                    'FUKA
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�i��CD�݌v�����L��")))                    'KAITEI
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�i�햼")))                                'HINSHUMEI
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�T�C�Y��")))                              'SAIZUMEI
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�F��")))                                  'IROMEI
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�P��")))                                  'TANI
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�b��O���݌ɐ�")))                        'KAGI_ZEN_SU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�b��O���݌ɋ��z")))                      'KAGI_ZEN_KINGAKU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�b��O���݌ɓ���")))                      'KAGI_ZEN_DOU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�b�擖�����ɐ�")))                        'KAGI_TOU_NYUKO_SU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�b�擖�����ɋ��z")))                      'KAGI_TOU_NYUKO_KINGAKU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�b�擖�����ɓ���")))                      'KAGI_TOU_NYUKO_DOU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�b�擖���o�ɐ�")))                        'KAGI_TOU_SHUKO_SU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�b�擖���o�ɋ��z")))                      'KAGI_TOU_SHUKO_KINGAKU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�b�擖���o�ɓ���")))                      'KAGI_TOU_DHUKO_DOU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�D���O���݌ɐ�")))                        'FUNA_ZEN_SU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�D���O���݌ɋ��z")))                      'FUNA_ZEN_KINGAKU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�D���O���݌ɓ���")))                      'FUNA_ZEN_DOU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�D���������ɐ�")))                        'FUNA_TOU_NYUKO_SU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�D���������ɋ��z")))                      'FUNA_TOU_NYUKO_KINGAKU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�D���������ɓ���")))                      'FUNA_TOU_NYUKO_DOU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�D�������o�ɐ�")))                        'FUNA_TOU_SHUKO_SU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�D�������o�ɋ��z")))                      'FUNA_TOU_SHUKO_KINGAKU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�D�������o�ɓ���")))                      'FUNA_TOU_SHUKO_DOU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�D�y�O���݌ɐ�")))                        'SAPO_ZEN_SU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�D�y�O���݌ɋ��z")))                      'SAPO_ZEN_KINGAKU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�D�y�O���݌ɓ���")))                      'SAPO_ZEN_DOU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�D�y�������ɐ�")))                        'SAPO_TOU_NYUKO_SU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�D�y�������ɋ��z")))                      'SAPO_TOU_NYUKO_KINGAKU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�D�y�������ɓ���")))                      'SAPO_TOU_NYUKO_DOU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�D�y�����o�ɐ�")))                        'SAPO_TOU_SHUKO_SU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�D�y�����o�ɋ��z")))                      'SAPO_TOU_SHUKO_KINGAKU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�D�y�����o�ɓ���")))                      'SAPO_TOU_SHUKO_DOU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�ԕi�O���݌ɐ�")))                        'HEN_ZEN_SU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�ԕi�O���݌ɋ��z")))                      'HEN_ZEN_KINGAKU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�ԕi�O���݌ɓ���")))                      'HEN_ZEN_DOU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�ԕi�����o�ɐ�")))                        'HEN_TOU_SHUKO_SU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�ԕi�����o�ɋ��z")))                      'HEN_TOU_SHUKO_KINGAKU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�ԕi�����o�ɓ���")))                      'HEN_TOU_SHUKO_DOU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("���o�בO���݌ɐ�")))                      'KARI_ZEN_SU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("���o�בO���݌ɋ��z")))                    'KARI_ZEN_KINGAKU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("���o�בO���݌ɓ���")))                    'KARI_ZEN_DOU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("���o�ד����o�ɐ�")))                      'KARI_TOU_SU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("���o�ד����o�ɋ��z")))                    'KARI_TOU_KINGAKU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("���o�ד����o�ɓ���")))                    'KARI_TOU_DOU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�����R�X�g")))                            'COST
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�A�����O���݌ɐ�")))                      'YU_ZEN_SU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�A�����O���݌ɋ��z")))                    'YU_ZEN_KINGAKU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�A�����O���݌ɓ���")))                    'YU_ZEN_DOU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�A���������o�ɐ�")))                      'YU_TOU_SU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�A���������o�ɋ��z")))                    'YU_TOU_KNGAKU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�A���������o�ɓ���")))                    'YU_TOU_DOU
                sqlBuf.Append(N & ", to_date(" & convNullStr(_db.rmNullDate(.Rows(i)("�쐬����"))) & ",'YYYY/MM/DD HH24:MI:SS') ") 'ORG_CREATEDT
                sqlBuf.Append(N & ", to_date(" & convNullStr(_db.rmNullDate(.Rows(i)("�X�V����"))) & ",'YYYY/MM/DD HH24:MI:SS') ") 'ORG_UPDDT
                sqlBuf.Append(N & ", " & convNullStr(UtilClass.getComputerName()) & " ")                 'UPDNAME  
                sqlBuf.Append(N & ", " & sysdate & " ")                                                  'UPDDATE 
                '-->2010.12.10 add by takagi
                sqlBuf.Append(N & "," & convNullStr(.Rows(i)("�i���敪�Q")))
                '<--2010.12.10 add by takagi
                sqlBuf.Append(N & ") ")
                _db.executeDB(sqlBufF.ToString & sqlBuf.ToString)

                prmRefPb.value += 1
                If prmRefPb.value Mod 10 = 0 Then
                    prmRefPb.status = "�[�i���f�[�^�捞��... (" & prmRefPb.value & "/" & prmRefPb.maxVal & ")"
                End If
            Next
        End With

    End Sub

    '-------------------------------------------------------------------------------
    '   �����^��p�@NULL��"NULL"�u���^NOT NULL��'���̂܂�'
    '   �i�����T�v�jSQL���s�p�̕�����u�����s��
    '   �����̓p�����^  �F�t�B�[���h�f�[�^(NULL��)
    '   ���o�̓p�����^  �F�Ȃ�
    '   �����\�b�h�߂�l�F�ϊ��㕶����
    '-------------------------------------------------------------------------------
    Private Function convNullStr(ByVal prmField As Object) As String
        If IsDBNull(prmField) Then
            Return "NULL"
        Else
            Return "'" & _db.rmSQ(CStr(prmField)) & "'"
        End If
    End Function

    '-------------------------------------------------------------------------------
    '   ���l�^��p�@NULL��"NULL"�u���^NOT NULL�����̂܂�
    '   �i�����T�v�jSQL���s�p�̕�����u�����s��
    '   �����̓p�����^  �F�t�B�[���h�f�[�^(NULL��)
    '   ���o�̓p�����^  �F�Ȃ�
    '   �����\�b�h�߂�l�F�ϊ��㕶����
    '-------------------------------------------------------------------------------
    Private Function convNullNUm(ByVal prmField As Object) As String
        If IsDBNull(prmField) Then
            Return "NULL"
        Else
            Return CStr(prmField)
        End If
    End Function
End Class