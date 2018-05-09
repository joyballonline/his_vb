'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���jABC���͎��s�w��
'    �i�t�H�[��ID�jZM410B_ABCBunseki
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ����        2010/10/25                 �V�K              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Public Class ZM410B_ABCBunseki
    Inherits System.Windows.Forms.Form

    '-------------------------------------------------------------------------------
    '   �萔��`
    '-------------------------------------------------------------------------------
    Private Const RS As String = "RecSet"                       '�f�[�^�Z�b�g�e�[�u����
    Private Const N As String = ControlChars.NewLine            '���s����
    Private Const PGID As String = "ZM410B"

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
        _updFlg = prmUpdFlg


    End Sub

    '-------------------------------------------------------------------------------
    '�@�t�H�[�����[�h�C�x���g
    '-------------------------------------------------------------------------------
    Private Sub ZM410B_ABCBunseki_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            '�O����s���̕\��
            Dim sql As String = ""
            Dim iRecCnt As Integer = 0
            sql = sql & N & "select *  "
            sql = sql & N & "from ( "
            sql = sql & N & "    select "
            sql = sql & N & "     ROW_NUMBER() OVER (ORDER BY RECORDID desc) RNUM "
            sql = sql & N & "    ,SDATEEND "
            sql = sql & N & "    ,KENNSU1 T10TARGET_CNT "
            sql = sql & N & "    ,KENNSU2 TARGET_ITEM_CNT "
            sql = sql & N & "    ,KENNSU3 A_CNT "
            sql = sql & N & "    ,KENNSU4 B_CNT "
            sql = sql & N & "    ,KENNSU5 C_CNT "
            sql = sql & N & "    ,KENNSU6 S_CNT "
            sql = sql & N & "    ,NAME1   YYYYMM_FROM "
            sql = sql & N & "    ,NAME2   YYYYMM_TO "
            sql = sql & N & "    from T91RIREKI "
            sql = sql & N & "    where PGID     = '" & PGID & "' "
            sql = sql & N & ") "
            sql = sql & N & "where RNUM = 1 "
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)
            If iRecCnt < 1 Then
                '�����Ȃ�
                lblZenkaiJikkouDate.Text = ZC110M_Menu.NON_EXECUTE
                lblZenkaiJissekiFrom.Text = ""
                lblZenkaiJissekiTo.Text = ""
                lblSum.Text = ""
                lblS.Text = ""
                lblA.Text = ""
                lblB.Text = ""
                lblC.Text = ""
            Else
                '��������
                lblZenkaiJikkouDate.Text = _db.rmNullDate(ds.Tables(RS).Rows(0)("SDATEEND"))
                Dim wk As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("YYYYMM_FROM"))
                lblZenkaiJissekiFrom.Text = wk.Substring(0, 4) & "/" & wk.Substring(4, 2)
                wk = _db.rmNullStr(ds.Tables(RS).Rows(0)("YYYYMM_TO"))
                lblZenkaiJissekiTo.Text = wk.Substring(0, 4) & "/" & wk.Substring(4, 2)
                lblSum.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("TARGET_ITEM_CNT")), "#,##0")
                lblS.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("S_CNT")), "#,##0")
                lblA.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("A_CNT")), "#,##0")
                lblB.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("B_CNT")), "#,##0")
                lblC.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("C_CNT")), "#,##0")
            End If

            '������s���̕\��
            sql = ""
            sql = sql & N & "SELECT "
            sql = sql & N & " DECODE(MAX(NENGETU),NULL,'', "
            sql = sql & N & "     TO_CHAR( "
            sql = sql & N & "         ADD_MONTHS( "
            sql = sql & N & "             TO_DATE( "
            sql = sql & N & "                 SUBSTR(MAX(NENGETU),1,4) || '/' || SUBSTR(MAX(NENGETU),5,2) || '/01' "
            sql = sql & N & "                ,'YYYY/MM/DD' "
            sql = sql & N & "                ) "
            sql = sql & N & "            ,-11 "
            sql = sql & N & "            ) "
            sql = sql & N & "        ,'YYYY/MM' "
            sql = sql & N & "        ) "
            sql = sql & N & "     )                       FROM_DT "
            sql = sql & N & ",DECODE(MAX(NENGETU),NULL,'', "
            sql = sql & N & "     TO_CHAR( "
            sql = sql & N & "         TO_DATE( "
            sql = sql & N & "             SUBSTR(MAX(NENGETU),1,4) || '/' || SUBSTR(MAX(NENGETU),5,2) || '/01' "
            sql = sql & N & "            ,'YYYY/MM/DD' "
            sql = sql & N & "            ) "
            sql = sql & N & "        ,'YYYY/MM' "
            sql = sql & N & "        ) "
            sql = sql & N & "     )                       TO_DT "
            sql = sql & N & "FROM T10HINHANJ "
            ds = _db.selectDB(sql, RS)
            dteKonkaiJissekiFrom.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("FROM_DT"))
            dteKonkaiJissekiTo.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("TO_DT"))

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
    '�@�e�L�X�g�t�H�[�J�X�擾�C�x���g
    '------------------------------------------------------------------------------------------------------
    Private Sub txt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dteKonkaiJissekiFrom.GotFocus, dteKonkaiJissekiTo.GotFocus
        UtilClass.selAll(sender)
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�e�L�X�g�L�[�����C�x���g
    '------------------------------------------------------------------------------------------------------
    Private Sub dteKonkaiJissekiFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dteKonkaiJissekiFrom.KeyPress, dteKonkaiJissekiTo.KeyPress
        UtilClass.moveNextFocus(Me, e)
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@���s�{�^���N�����C�x���g
    '------------------------------------------------------------------------------------------------------
    Private Sub btnJikkou_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJikkou.Click
        Try
            '���̓`�F�b�N
            Call checkInit()                                                                    '���юQ�Ɗ���


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
                    pb.jobName = "�捞���������s���Ă��܂��B"
                    pb.oneStep = 1

                    pb.status = "���͏�����" '0-0 WK������
                    _db.executeDB("delete from ZM410B_W10 where UPDNAME = '" & UtilClass.getComputerName() & "'")
                    _db.executeDB("delete from ZM410B_W11 where UPDNAME = '" & UtilClass.getComputerName() & "'")

                    Const CALC_VAL As String = "HJISSEKISU"

                    '1-0 �Ώەi�����o
                    pb.status = "�Ώەi�����o"
                    Dim sql As String = ""
                    sql = sql & N & "INSERT INTO ZM410B_W10 "
                    sql = sql & N & "( "
                    sql = sql & N & " HINMEICD "        '���i���R�[�h
                    sql = sql & N & ",KHINMEICD "       '�v��i���R�[�h
                    sql = sql & N & ",NENGETU "         '�N��
                    sql = sql & N & ",HJISSEKIRYOU "    '�̔����ї�
                    sql = sql & N & ",HJISSEKISU "      '�̔����ѐ�
                    sql = sql & N & ",UPDNAME "         '�[��ID
                    sql = sql & N & ",UPDDATE "         '�X�V����
                    sql = sql & N & ") "
                    sql = sql & N & "SELECT "
                    sql = sql & N & " T10.HINMEICD "                          '���i���R�[�h
                    sql = sql & N & ",M12.KHINMEICD "                         '�v��i���R�[�h
                    sql = sql & N & ",T10.NENGETU "                           '�N��
                    sql = sql & N & ",T10.HJISSEKIRYOU "                      '�̔����ї�
                    sql = sql & N & ",T10.HJISSEKISU "                        '�̔����ѐ�
                    sql = sql & N & ",'" & UtilClass.getComputerName() & "' " '�[��ID
                    sql = sql & N & ",SYSDATE "                               '�X�V����
                    sql = sql & N & "FROM T10HINHANJ       T10  "
                    sql = sql & N & "INNER JOIN M12SYUYAKU M12 "
                    sql = sql & N & " ON T10.HINMEICD = M12.HINMEICD "
                    sql = sql & N & "INNER JOIN (SELECT * FROM M11KEIKAKUHIN WHERE (TT_JUYOUCD IS NULL OR TT_JUYOUCD != '1') AND TT_SYUBETU = 1) M11 " '���v��CD��1���݌ɁE�J�ԁ�1
                    sql = sql & N & " ON M12.KHINMEICD = M11.TT_KHINMEICD "
                    sql = sql & N & "WHERE NENGETU >= '" & dteKonkaiJissekiFrom.Text.Replace("/", "") & "'"
                    sql = sql & N & "  AND NENGETU <= '" & dteKonkaiJissekiTo.Text.Replace("/", "") & "'"
                    _db.executeDB(sql)

                    '2-1 ���̔����ҏW
                    pb.status = "���̔����ҏW"
                    sql = ""
                    sql = sql & N & "INSERT INTO ZM410B_W11 "
                    sql = sql & N & "( "
                    sql = sql & N & " KHINMEICD "
                    sql = sql & N & ",HJISSEKIRYOU "
                    sql = sql & N & ",HJISSEKISU "
                    sql = sql & N & ",UPDNAME "
                    sql = sql & N & ",UPDDATE "
                    sql = sql & N & ")SELECT "
                    sql = sql & N & " KHINMEICD "
                    sql = sql & N & ",SUM(HJISSEKIRYOU) "
                    sql = sql & N & ",SUM(HJISSEKISU) "
                    sql = sql & N & ",'" & UtilClass.getComputerName() & "' "
                    sql = sql & N & ",SYSDATE "
                    sql = sql & N & "FROM ZM410B_W10 "
                    sql = sql & N & "GROUP BY KHINMEICD "
                    _db.executeDB(sql)

                    '2-2 �\���䗦�Z�o
                    pb.status = "�\���䗦�Z�o"
                    sql = ""
                    sql = sql & N & "UPDATE ZM410B_W11 "
                    sql = sql & N & "SET CONSISTSRATE = "
                    sql = sql & N & "( "
                    sql = sql & N & "SELECT ROUND((" & CALC_VAL & "/(SELECT SUM(" & CALC_VAL & ") FROM ZM410B_W11 WHERE UPDNAME='" & UtilClass.getComputerName() & "'))*100,5) "
                    sql = sql & N & "FROM ZM410B_W11 SUB  "
                    sql = sql & N & "WHERE ZM410B_W11.KHINMEICD = SUB.KHINMEICD "
                    sql = sql & N & "AND   SUB.UPDNAME='" & UtilClass.getComputerName() & "' "
                    sql = sql & N & ") "
                    sql = sql & N & "WHERE UPDNAME='" & UtilClass.getComputerName() & "' "
                    _db.executeDB(sql)

                    '2-3 �ݐύ\�����Ϗ�
                    pb.status = "�ݐύ\�����Ϗグ"
                    sql = ""
                    sql = sql & N & "SELECT "
                    sql = sql & N & " ROWID "
                    sql = sql & N & ",HJISSEKISU "
                    sql = sql & N & ",CONSISTSRATE "
                    sql = sql & N & "FROM ZM410B_W11 "
                    sql = sql & N & "WHERE UPDNAME = '" & UtilClass.getComputerName() & "' "
                    sql = sql & N & "ORDER BY CONSISTSRATE DESC ," & CALC_VAL & " DESC "
                    Dim ds As DataSet = _db.selectDB(sql, RS, pb.maxVal)
                    Dim sumValue As Decimal = 0.0
                    Dim sumRate As Decimal = 0.0
                    For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                        sumValue += _db.rmNullDouble(ds.Tables(RS).Rows(i)("HJISSEKISU"))
                        sumRate += _db.rmNullDouble(ds.Tables(RS).Rows(i)("CONSISTSRATE"))
                        sql = ""
                        sql = sql & N & "UPDATE ZM410B_W11"
                        sql = sql & N & "SET  RANK        = " & i + 1 & " "
                        sql = sql & N & "    ,AMOUNTVALUE = " & sumValue & " "
                        sql = sql & N & "    ,AMOUNTRATE  = " & sumRate & " "
                        sql = sql & N & "WHERE ROWID = '" & ds.Tables(RS).Rows(i)("ROWID") & "'"
                        _db.executeDB(sql)
                        pb.value = i + 1
                    Next

                    '3-0 ABC�敪�ݒ�
                    pb.status = "ABC�敪�ݒ�"
                    pb.value = 0
                    sql = ""
                    sql = sql & N & "UPDATE ZM410B_W11"
                    sql = sql & N & "SET ABC = "
                    sql = sql & N & " CASE WHEN AMOUNTRATE < (SELECT NUM1 FROM M01HANYO WHERE KOTEIKEY = '13' AND KAHENKEY = 'A') THEN 'A' "
                    sql = sql & N & "      WHEN AMOUNTRATE < (SELECT NUM1 FROM M01HANYO WHERE KOTEIKEY = '13' AND KAHENKEY = 'B') THEN 'B' "
                    sql = sql & N & "                                                                                             ELSE 'C' END "
                    sql = sql & N & "WHERE UPDNAME = '" & UtilClass.getComputerName() & "' "
                    _db.executeDB(sql)

                    _db.beginTran()
                    Try
                        '4-0 ABC�敪�X�V
                        pb.status = "ABC�敪�X�V"
                        pb.maxVal = 3
                        sql = ""
                        sql = sql & N & "UPDATE M11KEIKAKUHIN "
                        sql = sql & N & "SET TT_UPDNAME = '" & UtilClass.getComputerName() & "' "
                        sql = sql & N & "  , TT_DATE    = SYSDATE "
                        sql = sql & N & "  , TT_ABCKBN  =  "
                        sql = sql & N & "	( "
                        sql = sql & N & "	SELECT WK.ABC "
                        sql = sql & N & "	FROM (SELECT ABC,KHINMEICD FROM ZM410B_W11 WHERE  UPDNAME = '" & UtilClass.getComputerName() & "') WK "
                        sql = sql & N & "	WHERE M11KEIKAKUHIN.TT_KHINMEICD = WK.KHINMEICD "
                        sql = sql & N & "	) "
                        _db.executeDB(sql)
                        pb.value += 1
                        sql = ""
                        sql = sql & N & "UPDATE M11KEIKAKUHIN "
                        sql = sql & N & "SET TT_ABCKBN = 'C' "
                        sql = sql & N & "  , TT_UPDNAME = '" & UtilClass.getComputerName() & "' "
                        sql = sql & N & "  , TT_DATE    = SYSDATE "
                        sql = sql & N & "WHERE TT_ABCKBN IS NULL "
                        _db.executeDB(sql)
                        pb.value += 1
                        sql = ""
                        sql = sql & N & "UPDATE M11KEIKAKUHIN "
                        sql = sql & N & "SET TT_ABCKBN = 'S' "
                        sql = sql & N & "  , TT_UPDNAME = '" & UtilClass.getComputerName() & "' "
                        sql = sql & N & "  , TT_DATE    = SYSDATE "
                        sql = sql & N & "WHERE TT_JUYOUCD = '1' "
                        _db.executeDB(sql)
                        pb.value += 1


                        pb.status = "�X�e�[�^�X�ύX���E�E�E"
                        pb.value = 0
                        ed = Now                    '�����I������
                        _parentForm.updateSeigyoTbl(PGID, True, st, ed)                             '5-0 ���Y�ʊm��/����

                        pb.status = "���s�����쐬"
                        '5-1
                        sql = ""
                        sql = sql & N & "SELECT "
                        sql = sql & N & " SUM(ITEMCNT)   SUM_ITEMCNT "
                        sql = sql & N & ",SUM(A_CNT)     SUM_A_CNT "
                        sql = sql & N & ",SUM(B_CNT)     SUM_B_CNT "
                        sql = sql & N & ",SUM(C_CNT)     SUM_C_CNT "
                        sql = sql & N & ",SUM(S_CNT)     SUM_S_CNT  "
                        sql = sql & N & ",SUM(TARGETCNT) SUM_TARGETCNT "
                        sql = sql & N & "FROM "
                        sql = sql & N & "( "
                        sql = sql & N & "SELECT COUNT(*) ITEMCNT,0 A_CNT,0 B_CNT,0 C_CNT,0 S_CNT,0 TARGETCNT FROM M11KEIKAKUHIN  "
                        sql = sql & N & "UNION ALL "
                        sql = sql & N & "SELECT 0 ITEMCNT,COUNT(*) A_CNT,0 B_CNT,0 C_CNT,0 S_CNT,0 TARGETCNT FROM M11KEIKAKUHIN WHERE TT_ABCKBN = 'A' "
                        sql = sql & N & "UNION ALL "
                        sql = sql & N & "SELECT 0 ITEMCNT,0 A_CNT,COUNT(*) B_CNT,0 C_CNT,0 S_CNT,0 TARGETCNT FROM M11KEIKAKUHIN WHERE TT_ABCKBN = 'B' "
                        sql = sql & N & "UNION ALL "
                        sql = sql & N & "SELECT 0 ITEMCNT,0 A_CNT,0 B_CNT,COUNT(*) C_CNT,0 S_CNT,0 TARGETCNT FROM M11KEIKAKUHIN WHERE TT_ABCKBN = 'C' "
                        sql = sql & N & "UNION ALL "
                        sql = sql & N & "SELECT 0 ITEMCNT,0 A_CNT,0 B_CNT,0 C_CNT,COUNT(*) S_CNT,0 TARGETCNT FROM M11KEIKAKUHIN WHERE TT_ABCKBN = 'S' "
                        sql = sql & N & "UNION ALL "
                        sql = sql & N & "SELECT 0 ITEMCNT,0 A_CNT,0 B_CNT,0 C_CNT,0 S_CNT,COUNT(*) TARGETCNT FROM ZM410B_W10 WHERE UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
                        sql = sql & N & ") "
                        With _db.selectDB(sql, RS).Tables(RS)
                            insertRireki(_db.rmNullInt(.Rows(0)("SUM_TARGETCNT")), _
                                         _db.rmNullInt(.Rows(0)("SUM_ITEMCNT")), _
                                         _db.rmNullInt(.Rows(0)("SUM_A_CNT")), _
                                         _db.rmNullInt(.Rows(0)("SUM_B_CNT")), _
                                         _db.rmNullInt(.Rows(0)("SUM_C_CNT")), _
                                         _db.rmNullInt(.Rows(0)("SUM_S_CNT")), _
                                         st, ed)                                 '5-2 ���s�����i�[
                        End With


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
            Call initForm()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   ���̓`�F�b�N
    '   �i�����T�v�j���юQ�Ɗ��Ԃ̕K�{/�召/�ߋ����`�F�b�N���s��
    '   �����̓p�����^  �F�Ȃ�
    '   ���o�̓p�����^  �F�Ȃ�
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub checkInit()
        '���̓`�F�b�N
        If "/".Equals(dteKonkaiJissekiFrom.Text.Trim) Then                                                 '������
            Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput"), dteKonkaiJissekiFrom)
        ElseIf "/".Equals(dteKonkaiJissekiTo.Text.Trim) Then                                           '������
            Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput"), dteKonkaiJissekiTo)
        ElseIf CDate(dteKonkaiJissekiFrom.Text & "/01") >= CDate(dteKonkaiJissekiTo.Text & "/01") Then
            Throw New UsrDefException("�召�֌W���s���ł��B", _msgHd.getMSG("ErrDaiSyoChk"), dteKonkaiJissekiTo)
        ElseIf Format(CDate(dteKonkaiJissekiFrom.Text & "/01"), "yyyy/MM") >= Format(Now, "yyyy/MM") Then
            Throw New UsrDefException("�����̓��t�͓��͂ł��܂���B", _msgHd.getMSG("FutureDay"), dteKonkaiJissekiFrom)
        ElseIf Format(CDate(dteKonkaiJissekiTo.Text & "/01"), "yyyy/MM") >= Format(Now, "yyyy/MM") Then
            Throw New UsrDefException("�����̓��t�͓��͂ł��܂���B", _msgHd.getMSG("FutureDay"), dteKonkaiJissekiTo)
        End If
    End Sub

    '-------------------------------------------------------------------------------
    '   ���s�����쐬
    '   �i�����T�v�j�m�菈���p�̎��s�������쐬����
    '   �����̓p�����^  �FprmCntTarget  T10�Ώی���
    '                     prmCntItem    �Ώەi����(Item��)
    '                     prmCntA       A�敪����
    '                     prmCntB       B�敪����
    '                     prmCntC       C�敪����
    '                     prmCntS       S�敪����
    '                     prmStDt       �����J�n����
    '                     prmEdDt       �����I������
    '   ���o�̓p�����^  �F�Ȃ�
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub insertRireki(ByVal prmCntTarget As Integer, _
                             ByVal prmCntItem As Integer, _
                             ByVal prmCntA As Integer, _
                             ByVal prmCntB As Integer, _
                             ByVal prmCntC As Integer, _
                             ByVal prmCntS As Integer, ByVal prmStDt As Date, ByVal prmEdDt As Date)

        Dim d As DataSet = _db.selectDB("select SNENGETU,KNENGETU from T01KEIKANRI", RS)
        Dim syoriDate As String = _db.rmNullStr(d.Tables(RS).Rows(0)("SNENGETU"))
        Dim keikakuDate As String = _db.rmNullStr(d.Tables(RS).Rows(0)("KNENGETU"))

        Dim sql As String = ""
        sql = sql & N & "insert into T91RIREKI "
        sql = sql & N & "( "
        sql = sql & N & " SNENGETU "   '�����N��
        sql = sql & N & ",KNENGETU "   '�v��N��
        sql = sql & N & ",PGID "       '�@�\ID
        sql = sql & N & ",SDATESTART " '�����J�n����
        sql = sql & N & ",SDATEEND "   '�����I������
        sql = sql & N & ",KENNSU1 "    'T10�Ώی���
        sql = sql & N & ",KENNSU2 "    '�Ώەi����(Item��)
        sql = sql & N & ",KENNSU3 "    'A�敪����
        sql = sql & N & ",KENNSU4 "    'B�敪����
        sql = sql & N & ",KENNSU5 "    'C�敪����
        sql = sql & N & ",KENNSU6 "    'S�敪����
        sql = sql & N & ",NAME1 "      '���юQ�Ɗ���FROM
        sql = sql & N & ",NAME2 "      '���юQ�Ɗ���TO
        sql = sql & N & ",UPDNAME "    '�ŏI�X�V��
        sql = sql & N & ",UPDDATE "    '�ŏI�X�V��
        sql = sql & N & ")values( "
        sql = sql & N & "  '" & _db.rmSQ(syoriDate) & "' "                                                      '�����N��
        sql = sql & N & ", '" & _db.rmSQ(keikakuDate) & "' "                                                    '�v��N��
        sql = sql & N & ", '" & _db.rmSQ(PGID) & "' "                                                           '�@�\ID
        sql = sql & N & ",to_date('" & Format(prmStDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '�����J�n����
        sql = sql & N & ",to_date('" & Format(prmEdDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '�����I������
        sql = sql & N & "," & prmCntTarget & " "                                                                'T10�Ώی���
        sql = sql & N & "," & prmCntItem & " "                                                                  '�Ώەi����(Item��)
        sql = sql & N & "," & prmCntA & " "                                                                     'A�敪����
        sql = sql & N & "," & prmCntB & " "                                                                     'B�敪����
        sql = sql & N & "," & prmCntC & " "                                                                     'C�敪����
        sql = sql & N & "," & prmCntS & " "                                                                     'S�敪����
        sql = sql & N & ",'" & dteKonkaiJissekiFrom.Text.Replace("/", "") & "' "                                '���юQ�Ɗ���FROM
        sql = sql & N & ",'" & dteKonkaiJissekiTo.Text.Replace("/", "") & "' "                                  '���юQ�Ɗ���TO
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "                                     '�ŏI�X�V��
        sql = sql & N & ",sysdate "                                                                             '�ŏI�X�V��
        sql = sql & N & ") "
        _db.executeDB(sql)

    End Sub

End Class