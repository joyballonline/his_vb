'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���j��z�f�[�^�쐬�w���w��
'    �i�t�H�[��ID�jZG610B_TehaiDateSakusei
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
Public Class ZG610B_TehaiDateSakusei
    Inherits System.Windows.Forms.Form

    '-------------------------------------------------------------------------------
    '   �\���̒�`
    '-------------------------------------------------------------------------------
    Private Structure shuttaiBiType
        Dim shuttaiBi1 As String
        Dim shuttaiBi2 As String
        Dim shuttaiBi3 As String
        Dim shuttaiBi4 As String
        Dim shuttaiBi5 As String
        Dim shuttaiBi6 As String
    End Structure
    Private Structure abcType
        Dim nomal As shuttaiBiType
        Dim chozoHin As shuttaiBiType
    End Structure

    '-------------------------------------------------------------------------------
    '   �萔��`
    '-------------------------------------------------------------------------------
    Private Const RS As String = "RecSet"                       '�f�[�^�Z�b�g�e�[�u����
    Private Const N As String = ControlChars.NewLine            '���s����
    Private Const PGID As String = "ZG610B"

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
    Private Sub ZG610B_TehaiDateSakusei_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            sql = sql & N & "    from T91RIREKI "
            sql = sql & N & "    where PGID     = '" & PGID & "' "
            sql = sql & N & ") "
            sql = sql & N & "where RNUM = 1 "
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)
            If iRecCnt < 1 Then
                '�����Ȃ�
                lblJikkouDate.Text = ZC110M_Menu.NON_EXECUTE
                lblZNyuuryokuKensu.Text = ""
                lblZSyuturyokuKensu.Text = ""
            Else
                '��������
                lblJikkouDate.Text = _db.rmNullDate(ds.Tables(RS).Rows(0)("SDATEEND"))
                lblZNyuuryokuKensu.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU1")), "#,##0")
                lblZSyuturyokuKensu.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU2")), "#,##0")
            End If

            '������s���̕\��
            lblKonkaiKensu.Text = Format(_db.rmNullInt(_db.selectDB("select count(*) CNT from T41SEISANK ", RS).Tables(RS).Rows(0)("CNT")), "#,##0")

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
                    pb.jobName = "��z�쐬���������s���Ă��܂��B"
                    pb.oneStep = 1

                    pb.status = "������" : pb.maxVal = 1
                    _db.executeDB("delete from ZG610B_W10 where updname = '" & UtilClass.getComputerName() & "'")

                    pb.status = "�x�[�X�f�[�^�쐬��"
                    Dim outputCnt As Integer = 0
                    Call insertBaseRecord(outputCnt, pb)
                    pb.status = "��z�f�[�^�\�z��"
                    Call updateWkColumns(pb)

                    _db.beginTran()
                    Try
                        _db.executeDB("delete from T51TEHAI")

                        pb.status = "��z�f�[�^�쐬��"
                        Call insertTehaiRec(pb)

                        pb.status = "�X�e�[�^�X�ύX���E�E�E"
                        ed = Now                    '�����I������
                        _parentForm.updateSeigyoTbl(PGID, True, st, ed)

                        pb.status = "���s�����쐬"
                        Call insertRireki(outputCnt, st, ed)                                  '2-1 ���s�����i�[

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
    Private Sub insertRireki(ByRef prmRefOutputCnt As Integer, ByVal prmStDt As Date, ByVal prmEdDt As Date)

        Dim sql As String = ""
        sql = sql & N & "insert into T91RIREKI "
        sql = sql & N & "( "
        sql = sql & N & " SNENGETU "   '�����N��
        sql = sql & N & ",KNENGETU "   '�v��N��
        sql = sql & N & ",PGID "       '�@�\ID
        sql = sql & N & ",SDATESTART " '�����J�n����
        sql = sql & N & ",SDATEEND "   '�����I������
        sql = sql & N & ",KENNSU1 "    'T21���s����
        sql = sql & N & ",KENNSU2 "    'T13���s����
        sql = sql & N & ",UPDNAME "    '�ŏI�X�V��
        sql = sql & N & ",UPDDATE "    '�ŏI�X�V��
        sql = sql & N & ")values( "
        sql = sql & N & "  '" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "' "                             '�����N��
        sql = sql & N & ", '" & _db.rmSQ(lblKeikakuDate.Text.Replace("/", "")) & "' "                           '�v��N��
        sql = sql & N & ", '" & _db.rmSQ(PGID) & "' "                                                           '�@�\ID
        sql = sql & N & ",to_date('" & Format(prmStDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '�����J�n����
        sql = sql & N & ",to_date('" & Format(prmEdDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '�����I������
        sql = sql & N & ", " & CLng(lblKonkaiKensu.Text) & " "                                                  '���͌���
        sql = sql & N & ", " & prmRefOutputCnt & " "                                                            '�o�͌���
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "                                     '�ŏI�X�V��
        sql = sql & N & ",sysdate "                                                                             '�ŏI�X�V��
        sql = sql & N & ") "
        _db.executeDB(sql)

    End Sub

    '�x�[�X�f�[�^�쐬
    Private Sub insertBaseRecord(ByRef prmRefOutputCnt As Integer, ByRef prmRefBar As UtilProgressBar)

        '�W�J�p�^�[���ݒ�̃��[�h
        Dim iniFileName As String = UtilClass.getAppPath(StartUp.assembly)
        If Not iniFileName.EndsWith("\") Then iniFileName = iniFileName & "\"
        iniFileName = iniFileName & "..\Setting\" & StartUp.INI_FILE
        Dim ini As UtilMDL.API.UtilIniFileHandler = New UtilMDL.API.UtilIniFileHandler(iniFileName)
        Dim tenkaiPth As abcType
        tenkaiPth.chozoHin.shuttaiBi1 = ini.getIni("Lot Expand Rule", "S1")
        tenkaiPth.chozoHin.shuttaiBi2 = ini.getIni("Lot Expand Rule", "S2")
        tenkaiPth.chozoHin.shuttaiBi3 = ini.getIni("Lot Expand Rule", "S3")
        tenkaiPth.chozoHin.shuttaiBi4 = ini.getIni("Lot Expand Rule", "S4")
        tenkaiPth.chozoHin.shuttaiBi5 = ini.getIni("Lot Expand Rule", "S5")
        tenkaiPth.chozoHin.shuttaiBi6 = ini.getIni("Lot Expand Rule", "S6")
        tenkaiPth.nomal.shuttaiBi1 = ini.getIni("Lot Expand Rule", "ABC1")
        tenkaiPth.nomal.shuttaiBi2 = ini.getIni("Lot Expand Rule", "ABC2")
        tenkaiPth.nomal.shuttaiBi3 = ini.getIni("Lot Expand Rule", "ABC3")
        tenkaiPth.nomal.shuttaiBi4 = ini.getIni("Lot Expand Rule", "ABC4")
        tenkaiPth.nomal.shuttaiBi5 = ini.getIni("Lot Expand Rule", "ABC5")
        tenkaiPth.nomal.shuttaiBi6 = ini.getIni("Lot Expand Rule", "ABC6")

        '��]�o�����̎擾
        Dim recCnt As Integer = 0
        Dim sql As String = ""
        sql = sql & N & "SELECT 1 KBN,KIBOU1 KIBOU FROM T01KEIKANRI "
        sql = sql & N & "UNION ALL "
        sql = sql & N & "SELECT 2 KBN,KIBOU2 KIBOU FROM T01KEIKANRI "
        sql = sql & N & "UNION ALL "
        sql = sql & N & "SELECT 3 KBN,KIBOU3 KIBOU FROM T01KEIKANRI "
        sql = sql & N & "UNION ALL "
        sql = sql & N & "SELECT 4 KBN,KIBOU4 KIBOU FROM T01KEIKANRI "
        sql = sql & N & "UNION ALL "
        sql = sql & N & "SELECT 5 KBN,KIBOU5 KIBOU FROM T01KEIKANRI "
        sql = sql & N & "UNION ALL "
        sql = sql & N & "SELECT 6 KBN,KIBOU6 KIBOU FROM T01KEIKANRI "
        Dim kibouDs As DataSet = _db.selectDB(sql, RS, recCnt)
        Dim kibouHash As Hashtable = New Hashtable
        With kibouDs.Tables(RS)
            For i As Integer = 0 To recCnt - 1
                kibouHash.Add(_db.rmNullInt(.Rows(i)("KBN")), _db.rmNullStr(.Rows(i)("KIBOU")))
            Next
        End With

        'T41���Y�v��̎擾
        sql = ""
        sql = sql & N & "SELECT "
        sql = sql & N & " T.KHINMEICD "                        '�v��i��CD
        sql = sql & N & ",SUBSTR(T.KHINMEICD,1,2) SHIYOCD "    '�v��i��CD �d�l
        sql = sql & N & ",SUBSTR(T.KHINMEICD,3,3) HINSHUCD "   '�v��i��CD �i��
        sql = sql & N & ",SUBSTR(T.KHINMEICD,6,3) SENSHINCD "  '�v��i��CD ���S��
        sql = sql & N & ",SUBSTR(T.KHINMEICD,9,2) SIZECD "     '�v��i��CD �T�C�Y
        sql = sql & N & ",SUBSTR(T.KHINMEICD,11,3) IROCD "     '�v��i��CD �F
        sql = sql & N & ",M.TT_HINSYUNM "                      '�i�햼 
        sql = sql & N & ",M.TT_SIZENM "                        '�T�C�Y�� 
        sql = sql & N & ",M.TT_IRONM "                         '�F���� 
        sql = sql & N & ",1 LOTNUM "                           '���b�g�� 
        sql = sql & N & ",M.TT_LOT "                           '�W�����b�g�� 
        sql = sql & N & ",M.TT_TANCYO "                        '�P�� 
        sql = sql & N & ",(M.TT_LOT/M.TT_TANCYO) HONSU "       '�{�� 
        sql = sql & N & ",H.NAME3 CHUMONSAKI "                 '������ 
        sql = sql & N & ",T.LOTOSU "                           '���b�g��  �i�W�J�����j
        sql = sql & N & ",M.TT_ABCKBN "                        'ABC�敪   �i�W�J�����j
        sql = sql & N & "FROM T41SEISANK T "
        sql = sql & N & "INNER JOIN M11KEIKAKUHIN                                  M ON T.KHINMEICD  = M.TT_KHINMEICD "
        sql = sql & N & "LEFT  JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '01') H ON M.TT_JUYOUCD = H.KAHENKEY "
        sql = sql & N & "WHERE T.LOTOSU > 0 "
        Dim Ds As DataSet = _db.selectDB(sql, RS, recCnt)
        prmRefBar.maxVal = Ds.Tables(RS).Rows.Count - 1
        With Ds.Tables(RS)

            Dim nowdt As String = Format(Now, "yyyy/MM/dd HH:mm:ss")
            For i As Integer = 0 To recCnt - 1
                prmRefBar.value = i
                '��]�o�������Ƀ��b�g�W�J
                Dim shuttaibi() As String = getShuttaibi(tenkaiPth, kibouHash, _db.rmNullStr(.Rows(i)("TT_ABCKBN")), _db.rmNullInt(.Rows(i)("LOTOSU")))

                '��]�o��������WK�փ��R�[�h�}��
                For j As Integer = 0 To shuttaibi.Length - 1
                    sql = ""
                    sql = sql & N & "INSERT INTO ZG610B_W10"
                    sql = sql & N & "("
                    sql = sql & N & " ZW_KHINMEICD"    '�v��i��CD
                    sql = sql & N & ",ZW_H_SIYOU_CD"   '�d�l�R�[�h
                    sql = sql & N & ",ZW_H_HIN_CD"     '�i��R�[�h
                    sql = sql & N & ",ZW_H_SENSIN_CD"  '���S��
                    sql = sql & N & ",ZW_H_SIZE_CD"    '�T�C�Y
                    sql = sql & N & ",ZW_H_COLOR_CD"   '�F
                    sql = sql & N & ",ZW_HIN_DATA"     '�i���f�[�^
                    sql = sql & N & ",ZW_SIZE_DATA"    '�T�C�Y�f�[�^
                    sql = sql & N & ",ZW_COLOR_DATA"   '�F�f�[�^
                    sql = sql & N & ",ZW_TEHAI_SUU"    '��z����
                    sql = sql & N & ",ZW_TANCYO"       '����P��
                    sql = sql & N & ",ZW_JYO_SUU"      '��
                    sql = sql & N & ",ZW_KIBOU_DATE"   '��]�N����
                    sql = sql & N & ",ZW_TYUMONSAKI"   '������
                    sql = sql & N & ",ZW_NOUKI"        '�[��
                    sql = sql & N & ",ZW_LOT_SUU"      '���b�g��
                    sql = sql & N & ",ZW_LOT_CHO"      '���b�g��
                    sql = sql & N & ",UPDNAME "        '�X�V��
                    sql = sql & N & ",UPDDATE "        '�X�V��
                    sql = sql & N & ")VALUES("
                    sql = sql & N & " " & impDtForStr(_db.rmNullStr(.Rows(i)("KHINMEICD")))   '�v��i���R�[�h
                    sql = sql & N & "," & impDtForStr(_db.rmNullStr(.Rows(i)("SHIYOCD")))     '�d�l�R�[�h
                    sql = sql & N & "," & impDtForStr(_db.rmNullStr(.Rows(i)("HINSHUCD")))    '�i��R�[�h
                    sql = sql & N & "," & impDtForStr(_db.rmNullStr(.Rows(i)("SENSHINCD")))   '���S��
                    sql = sql & N & "," & impDtForStr(_db.rmNullStr(.Rows(i)("SIZECD")))      '�T�C�Y
                    sql = sql & N & "," & impDtForStr(_db.rmNullStr(.Rows(i)("IROCD")))       '�F
                    sql = sql & N & "," & impDtForStr(_db.rmNullStr(.Rows(i)("TT_HINSYUNM"))) '�i���f�[�^
                    sql = sql & N & "," & impDtForStr(_db.rmNullStr(.Rows(i)("TT_SIZENM")))   '�T�C�Y�f�[�^
                    sql = sql & N & "," & impDtForStr(_db.rmNullStr(.Rows(i)("TT_IRONM")))    '�F�f�[�^
                    sql = sql & N & "," & impDtForNum(_db.rmNullStr(.Rows(i)("TT_LOT")))      '��z����
                    sql = sql & N & "," & impDtForNum(_db.rmNullStr(.Rows(i)("TT_TANCYO")))   '����P��
                    sql = sql & N & "," & impDtForNum(_db.rmNullStr(.Rows(i)("HONSU")))       '��
                    sql = sql & N & "," & impDtForStr(shuttaibi(j))                           '��]�N����
                    sql = sql & N & "," & impDtForStr(_db.rmNullStr(.Rows(i)("CHUMONSAKI")))  '������
                    sql = sql & N & "," & impDtForStr(shuttaibi(j))                           '�[��
                    sql = sql & N & "," & impDtForNum(_db.rmNullStr(.Rows(i)("LOTNUM")))      '���b�g��
                    sql = sql & N & "," & impDtForNum(_db.rmNullStr(.Rows(i)("TT_LOT")))      '���b�g��
                    sql = sql & N & ",'" & UtilClass.getComputerName() & "'"                  '�X�V��
                    sql = sql & N & ",TO_DATE('" & nowdt & "','YYYY/MM/DD HH24:MI:SS') "      '�X�V��
                    sql = sql & N & ")"
                    _db.executeDB(sql)
                    '2011/02/01 add start Sugawara #97
                    prmRefOutputCnt = prmRefOutputCnt + 1
                    '2011/02/01 add end Sugawara #97
                Next

            Next

        End With

    End Sub

    '��z�f�[�^�\�z
    Public Sub updateWkColumns(ByRef prmRefBar As UtilProgressBar)

        Dim SQL As String = ""
        SQL = SQL & "SELECT "
        SQL = SQL & " TT_KHINMEICD,"
        SQL = SQL & " TT_H_SIYOU_CD,"       ' 0�F�i�i���R�[�h�j�d�l�R�[�h
        SQL = SQL & " TT_H_HIN_CD,"         ' 1�F�i�i���R�[�h�j�i��R�[�h
        SQL = SQL & " TT_H_SENSIN_CD,"      ' 2�F�i�i���R�[�h�j���S���R�[�h
        SQL = SQL & " TT_H_SIZE_CD,"        ' 3�F�i�i���R�[�h�j�T�C�Y�R�[�h
        SQL = SQL & " TT_H_COLOR_CD,"       ' 4�F�i�i���R�[�h�j�F�R�[�h
        SQL = SQL & " TT_TANCYO,"           ' 5�F����P��
        SQL = SQL & " TT_FUKA_CD,"          ' 6�F�t���L��
        SQL = SQL & " TT_HINMEI,"           ' 7�F�i��
        SQL = SQL & " TT_TEHAI_SUU,"        ' 8�F��z����
        SQL = SQL & " TT_SYORI_KBN,"        ' 9�F�����敪
        SQL = SQL & " TT_TEHAI_KBN,"        '10�F��z�敪
        SQL = SQL & " TT_SEISAKU_KBN,"      '11�F����敪
        SQL = SQL & " TT_TENKAI_KBN,"       '12�F�W�J�敪
        SQL = SQL & " TT_KOUTEI,"           '13�F�w��H��
        SQL = SQL & " TT_KEISAN_KBN,"       '14�F���H���v�Z
        SQL = SQL & " TT_TATIAI_UM,"        '15�F����L��
        SQL = SQL & " TT_TANCYO_KBN,"       '16�F�P���敪
        SQL = SQL & " TT_MAKI_CD,"          '17�F���g�R�[�h
        SQL = SQL & " TT_HOSO_KBN,"         '18�F��敪
        SQL = SQL & " TT_HINSITU_KBN,"      '19�F�i�������敪
        SQL = SQL & " TT_SIYOUSYO_NO,"      '20�F�d�l����
        SQL = SQL & " TT_SEIZOU_BMN,"       '21�F��������
        'SQL = SQL & " TT_KONYU_SIYOU,"      '22�F�w���i�p�d�l�R�[�h
        SQL = SQL & " TT_KAMOKU_CD,"        '23�F�ȖڃR�[�h
        SQL = SQL & " TT_N_SO_SUU,"         '24�F���ɖ{�� �S��
        SQL = SQL & " TT_N_K_SUU,"          '25�F���ɖ{�� �k���{�{��
        SQL = SQL & " TT_N_SH_SUU"          '26�F���ɖ{�� �Z�d�����{��
        SQL = SQL & " FROM"
        SQL = SQL & " M11KEIKAKUHIN TT"
        SQL = SQL & " WHERE EXISTS (SELECT * "
        SQL = SQL & "               FROM ZG610B_W10 zw "
        SQL = SQL & "               WHERE TT.TT_KHINMEICD = zw.ZW_KHINMEICD "
        SQL = SQL & "                AND  zw.UPDNAME      = '" & UtilClass.getComputerName() & "')"
        Dim ds As DataSet = _db.selectDB(SQL, RS)
        prmRefBar.maxVal = ds.Tables(RS).Rows.Count - 1
        With ds.Tables(RS)
            Dim nowdt As String = Format(Now, "yyyy/MM/dd HH:mm:ss")
            For i As Integer = 0 To .Rows.Count - 1
                prmRefBar.value = i
                SQL = ""
                SQL = SQL & N & "UPDATE ZG610B_W10 "
                SQL = SQL & N & "SET "
                SQL = SQL & N & " ZW_FUKA_CD     = " & impDtForStr(_db.rmNullStr(.Rows(i)("TT_FUKA_CD")))       '�t���L��
                SQL = SQL & N & ",ZW_HINMEI      = " & impDtForStr(_db.rmNullStr(.Rows(i)("TT_HINMEI")))        '�i��
                SQL = SQL & N & ",ZW_TEHAI_SUU   = " & impDtForNum(_db.rmNullStr(.Rows(i)("TT_TEHAI_SUU")))     '��z����
                SQL = SQL & N & ",ZW_SYORI_KBN   = " & impDtForNum(_db.rmNullStr(.Rows(i)("TT_SYORI_KBN")))     '�����敪
                SQL = SQL & N & ",ZW_TEHAI_KBN   = " & impDtForNum(_db.rmNullStr(.Rows(i)("TT_TEHAI_KBN")))     '��z�敪
                SQL = SQL & N & ",ZW_SEISAKU_KBN = " & impDtForNum(_db.rmNullStr(.Rows(i)("TT_SEISAKU_KBN")))   '����敪
                SQL = SQL & N & ",ZW_SEIZOU_BMN  = " & impDtForNum(_db.rmNullStr(.Rows(i)("TT_SEIZOU_BMN")))    '��������
                SQL = SQL & N & ",ZW_TANCYO_KBN  = " & impDtForNum(_db.rmNullStr(.Rows(i)("TT_TANCYO_KBN")))    '�P���敪
                SQL = SQL & N & ",ZW_MAKI_CD     = " & impDtForNum(_db.rmNullStr(.Rows(i)("TT_MAKI_CD")))
                SQL = SQL & N & ",ZW_HOSO_KBN    = " & impDtForStr(_db.rmNullStr(.Rows(i)("TT_HOSO_KBN")))      '��敪
                SQL = SQL & N & ",ZW_SIYOUSYO_NO = " & impDtForStr(_db.rmNullStr(.Rows(i)("TT_SIYOUSYO_NO")))   '�d�l���ԍ�
                SQL = SQL & N & ",ZW_TENKAI_KBN  = " & impDtForNum(_db.rmNullStr(.Rows(i)("TT_TENKAI_KBN")))    '�W�J�敪
                SQL = SQL & N & ",ZW_BBNKOUTEI   = " & impDtForStr(_db.rmNullStr(.Rows(i)("TT_KOUTEI")))        '�w��H��
                SQL = SQL & N & ",ZW_HINSITU_KBN = " & impDtForNum(_db.rmNullStr(.Rows(i)("TT_HINSITU_KBN")))   '�i�������敪
                SQL = SQL & N & ",ZW_KEISAN_KBN  = " & impDtForNum(_db.rmNullStr(.Rows(i)("TT_KEISAN_KBN")))    '���H���v�Z�敪
                SQL = SQL & N & ",ZW_TATIAI_UM   = " & impDtForNum(_db.rmNullStr(.Rows(i)("TT_TATIAI_UM")))     '����L��
                'SQL = SQL & N & ",ZW_KONYU_SIYOU = " & impDtForStr(_db.rmNullStr(.Rows(i)("TT_KONYU_SIYOU")))  '�w���i�p�d�l�R�[�h
                SQL = SQL & N & ",ZW_KAMOKU_CD   = " & impDtForNum(_db.rmNullStr(.Rows(i)("TT_KAMOKU_CD")))
                SQL = SQL & N & ",ZW_N_SO_SUU    = " & impDtForNum(_db.rmNullStr(.Rows(i)("TT_N_SO_SUU")))      '���ɖ{�� �S��
                SQL = SQL & N & ",ZW_N_K_SUU     = " & impDtForNum(_db.rmNullStr(.Rows(i)("TT_N_K_SUU")))       '���ɖ{�� �k���{�{��
                SQL = SQL & N & ",ZW_N_SH_SUU    = " & impDtForNum(_db.rmNullStr(.Rows(i)("TT_N_SH_SUU")))      '���ɖ{�� �Z�d�����{��
                SQL = SQL & N & ",ZW_HENKAN_FLG  = 1 "                                                          '�ϊ��c�a���݃t���O
                SQL = SQL & N & ",UPDDATE        = TO_DATE('" & nowdt & "','YYYY/MM/DD HH24:MI:SS') "                                '�X�V��
                SQL = SQL & N & "WHERE ZW_KHINMEICD = " & impDtForStr(_db.rmNullStr(.Rows(i)("TT_KHINMEICD")))  '�v��i���R�[�h
                SQL = SQL & N & " AND  UPDNAME      = '" & UtilClass.getComputerName() & "'"
                _db.executeDB(SQL)
            Next
        End With

    End Sub

    '��z�f�[�^�쐬
    Public Sub insertTehaiRec(ByRef prmRefBar As UtilProgressBar)

        Dim SQL As String = ""
        SQL = SQL & N & "SELECT"
        SQL = SQL & N & " ZW_KHINMEICD,"
        SQL = SQL & N & " ZW_H_SIYOU_CD,"   ' 0�F�i�i���R�[�h�j�d�l�R�[�h
        SQL = SQL & N & " ZW_H_HIN_CD,"     ' 1�F�i�i���R�[�h�j�i��R�[�h
        SQL = SQL & N & " ZW_H_SENSIN_CD,"  ' 2�F�i�i���R�[�h�j���S���R�[�h
        SQL = SQL & N & " ZW_H_SIZE_CD,"    ' 3�F�i�i���R�[�h�j�T�C�Y�R�[�h
        SQL = SQL & N & " ZW_H_COLOR_CD,"   ' 4�F�i�i���R�[�h�j�F�R�[�h
        SQL = SQL & N & " ZW_FUKA_CD,"      ' 5�F�݌v�t���L��
        SQL = SQL & N & " ZW_LOT_SUU,"      ' 6�F���b�g��
        SQL = SQL & N & " ZW_LOT_CHO,"      ' 7�F���b�g��
        SQL = SQL & N & " ZW_HIN_DATA,"     ' 8�F�i���f�[�^
        SQL = SQL & N & " ZW_SIZE_DATA,"    ' 9�F�T�C�Y�f�[�^
        SQL = SQL & N & " ZW_COLOR_DATA,"   '10�F�F�f�[�^
        SQL = SQL & N & " ZW_KIBOU_DATE,"   '11�F��]�N����
        SQL = SQL & N & " ZW_NOUKI,"        '12�F�[��
        SQL = SQL & N & " ZW_TYUMONSAKI,"   '13�F������
        SQL = SQL & N & " ZW_HINMEI,"       '14�F�i��
        SQL = SQL & N & " ZW_TEHAI_SUU,"    '15�F��z����(��z�ϊ��c�a)
        SQL = SQL & N & " ZW_TANCYO,"       '16�F����P��
        SQL = SQL & N & " ZW_JYO_SUU,"      '17�F��
        SQL = SQL & N & " ZW_SYORI_KBN,"    '18�F�����敪
        SQL = SQL & N & " ZW_TEHAI_KBN,"    '19�F��z�敪
        SQL = SQL & N & " ZW_SEISAKU_KBN,"  '20�F����敪
        SQL = SQL & N & " ZW_SEIZOU_BMN,"   '21�F��������
        SQL = SQL & N & " ZW_TANCYO_KBN,"   '22�F�P���敪
        SQL = SQL & N & " ZW_MAKI_CD,"      '23�F���g�R�[�h
        SQL = SQL & N & " ZW_HOSO_KBN,"     '24�F��敪
        SQL = SQL & N & " ZW_SIYOUSYO_NO,"  '25�F�d�l����
        SQL = SQL & N & " ZW_TENKAI_KBN,"   '26�F�W�J�敪
        SQL = SQL & N & " ZW_BBNKOUTEI,"    '27�F�w��H��
        SQL = SQL & N & " ZW_HINSITU_KBN,"  '28�F�i�������敪
        SQL = SQL & N & " ZW_KEISAN_KBN,"   '29�F���H���v�Z
        SQL = SQL & N & " ZW_TATIAI_UM,"    '30�F����L��
        SQL = SQL & N & " ZW_KONYU_SIYOU,"  '31�F�w���i�p�d�l�R�[�h
        SQL = SQL & N & " ZW_KAMOKU_CD,"    '32�F�ȖڃR�[�h
        SQL = SQL & N & " ZW_HENKAN_FLG,"   '33�F��z�ϊ��c�a���݃t���O
        SQL = SQL & N & " UPDDATE,"         '34�F�X�V��
        SQL = SQL & N & " ZW_N_SO_SUU,"     '35:���ɖ{�� �S��
        SQL = SQL & N & " ZW_N_K_SUU,"      '36:���ɖ{�� �k���{�{��
        SQL = SQL & N & " ZW_N_SH_SUU "     '37���ɖ{�� �Z�d�����{��
        '-->2010.12.22 add by takagi 
        SQL = SQL & N & ",TT_JUYOUCD"
        '<--2010.12.22 add by takagi 
        SQL = SQL & N & "FROM ZG610B_W10 "
        '-->2010.12.22 add by takagi 
        SQL = SQL & N & "LEFT JOIN M11KEIKAKUHIN ON ZG610B_W10.ZW_KHINMEICD = M11KEIKAKUHIN.TT_KHINMEICD"
        '<--2010.12.22 add by takagi 
        SQL = SQL & N & "WHERE ZW_HENKAN_FLG = 1 AND updname = '" & UtilClass.getComputerName() & "' "
        SQL = SQL & N & "ORDER BY"
        SQL = SQL & N & "  ZW_SEIZOU_BMN DESC,"
        SQL = SQL & N & "  ZW_H_HIN_CD,"
        SQL = SQL & N & "  ZW_H_SENSIN_CD,"
        SQL = SQL & N & "  ZW_H_SIZE_CD,"
        SQL = SQL & N & "  ZW_H_SIYOU_CD,"
        SQL = SQL & N & "  ZW_H_COLOR_CD,"
        SQL = SQL & N & "  ZW_TANCYO,"
        SQL = SQL & N & "  ZW_KIBOU_DATE"
        Dim ds As DataSet = _db.selectDB(SQL, RS)
        prmRefBar.maxVal = ds.Tables(RS).Rows.Count - 1

        '-->2010.12.22 chg by takagi #24
        'Dim sYear As String = lblSyoriDate.Text.Replace("/", "").Substring(0, 4)
        'Dim sMonth As String = lblSyoriDate.Text.Replace("/", "").Substring(4, 2)
        Dim sYear As String = lblKeikakuDate.Text.Replace("/", "").Substring(0, 4)
        Dim sMonth As String = lblKeikakuDate.Text.Replace("/", "").Substring(4, 2)
        '<--2010.12.22 chg by takagi #24

        '��zNo�J�E���^������
        '-->2010.12.22 chg by takagi #24
        'Dim lT_Cnt_K As Integer = lT_Cnt_K = 700      '�w���i��700�ԑ䂩��g�p����B
        'Dim lT_Cnt_O As Integer = lT_Cnt_O = 0        '�ȊO�͒ʏ�
        Dim lT_Cnt_K As Integer = 700      '�w���i��700�ԑ䂩��g�p����B
        Dim lT_Cnt_O As Integer = 0        '�ȊO�͒ʏ�
        '<--2010.12.22 chg by takagi #24

        'Const lH03_NAISAKU As Integer = 1      '����敪�F����
        Const lH03_GAICHUU As Integer = 2      '����敪�F�O��
        Const lH03_KOUNYUU As Integer = 3      '����敪�F�w��
        Const lZM03_DENPYO_KBN_OCR As Integer = 1     '�`�[�敪�FOCR�i���ьŒ�j

        Dim nowdt As String = Format(Now, "yyyy/MM/dd HH:mm:ss")

        With ds.Tables(RS)

            For i As Integer = 0 To .Rows.Count - 1
                prmRefBar.value = i

                '����敪��ۑ�
                Dim lSeiKbn As Integer = _db.rmNullInt(.Rows(i)("ZW_SEISAKU_KBN"))

                '��zNo�쐬
                Dim sTehaiNO As String = ""
                If lSeiKbn = lH03_KOUNYUU Then
                    '�w���i�̏ꍇ
                    lT_Cnt_K = lT_Cnt_K + 1
                    sTehaiNO = sMonth & Format(lT_Cnt_K, "000")
                Else
                    '�w���i�ȊO�̏ꍇ�i����E�O���j
                    lT_Cnt_O = lT_Cnt_O + 1
                    sTehaiNO = sMonth & Format(lT_Cnt_O, "000")
                End If

                Dim sTokki1 As String = "ƭ����ܹ "
                sTokki1 = sTokki1 & "K:" & _db.rmNullInt(.Rows(i)("ZW_N_K_SUU")) & " "
                sTokki1 = sTokki1 & "S:" & _db.rmNullInt(.Rows(i)("ZW_N_SH_SUU")) & " "
                sTokki1 = (sTokki1 & Space(22)).Substring(0, 22)

                '�o�^
                SQL = ""
                SQL = SQL & N & "INSERT INTO T51TEHAI"
                SQL = SQL & N & " (TEHAI_NO,"        '��z��
                SQL = SQL & N & " SYORI_YM,"         '�����N��
                SQL = SQL & N & " SYORI_KBN,"        '�����敪
                SQL = SQL & N & " KIBOU_DATE,"       '��]�N����
                'SQL = SQL & N & " NOUKI,"            '�[��
                SQL = SQL & N & " TEHAI_KBN,"        '��z�敪
                SQL = SQL & N & " SEISAKU_KBN,"      '����敪
                SQL = SQL & N & " SEIZOU_BMN,"       '��������
                SQL = SQL & N & " DENPYOK,"          '�`�[�敪
                SQL = SQL & N & " TYUMONSAKI,"       '������
                SQL = SQL & N & " HINMEI_CD, "       '�i��CD
                SQL = SQL & N & " H_SIYOU_CD,"       '�i�i���R�[�h�j�d�l�R�[�h
                SQL = SQL & N & " H_HIN_CD,"         '�i�i���R�[�h�j�i��R�[�h
                SQL = SQL & N & " H_SENSIN_CD,"      '�i�i���R�[�h�j���S���R�[�h
                SQL = SQL & N & " H_SIZE_CD,"        '�i�i���R�[�h�j�T�C�Y�R�[�h
                SQL = SQL & N & " H_COLOR_CD,"       '�i�i���R�[�h�j�F�R�[�h
                SQL = SQL & N & " FUKA_CD,"          '�݌v�t���L��
                SQL = SQL & N & " HINMEI,"           '�i��
                SQL = SQL & N & " HIN_DATA,"         '�i���f�[�^
                SQL = SQL & N & " SIZE_DATA,"        '�T�C�Y�f�[�^
                SQL = SQL & N & " COLOR_DATA,"       '�F�f�[�^
                SQL = SQL & N & " TEHAI_SUU,"        '��z����
                SQL = SQL & N & " TANCYO_KBN,"       '�P���敪
                SQL = SQL & N & " TANCYO,"           '����P��
                SQL = SQL & N & " JYOSU,"            '��
                SQL = SQL & N & " MAKI_CD,"          '���g�R�[�h
                SQL = SQL & N & " HOSO_KBN,"         '��敪
                SQL = SQL & N & " SIYOUSYO_NO,"      '�d�l����
                SQL = SQL & N & " TOKKI,"            '���L����
                SQL = SQL & N & " TENKAI_KBN,"       '�W�J�敪
                SQL = SQL & N & " BBNKOUTEI,"        '�w��H��
                SQL = SQL & N & " HINSITU_KBN,"      '�i�������敪
                SQL = SQL & N & " KEISAN_KBN,"       '���H���v�Z
                SQL = SQL & N & " TATIAI_UM,"        '����L��
                'SQL = SQL & N & " KONYU_SIYOU,"      '�w���i�p�d�l�R�[�h
                SQL = SQL & N & " KAMOKU_CD,"        '�ȖڃR�[�h
                SQL = SQL & N & " N_SO_SUU,"         '���ɖ{��
                SQL = SQL & N & " N_K_SUU,"          '�k���{�{��
                SQL = SQL & N & " N_SH_SUU,"         '�Z�d�����{��
                '-->2010.12.22 add by takagi 
                SQL = SQL & N & " JUYOUCD,"
                '<--2010.12.22 add by takagi 
                SQL = SQL & N & " UPDNAME,"          '�X�V��
                SQL = SQL & N & " UPDDATE)"          '�X�V��
                SQL = SQL & N & " VALUES"
                SQL = SQL & N & " ("
                SQL = SQL & N & "'" & sTehaiNO & "',"                                           '��z��
                SQL = SQL & N & "'" & sYear & sMonth & "',"                                     '�����N��
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_SYORI_KBN"))) & ","      '�����敪
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_KIBOU_DATE"))) & ","     '��]�N����
                'SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_NOUKI"))) & ","          '�[��
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_TEHAI_KBN"))) & ","      '��z�敪
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_SEISAKU_KBN"))) & ","    '����敪
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_SEIZOU_BMN"))) & ","     '��������
                SQL = SQL & N & lZM03_DENPYO_KBN_OCR & ","                                      '�`�[�敪
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_TYUMONSAKI"))) & ","     '������
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_KHINMEICD"))) & ","      '�i���R�[�h
                SQL = SQL & N & impDtForStr((_db.rmNullStr(.Rows(i)("ZW_H_SIYOU_CD")) & "  ").Substring(0, 2)) & ","     '�i�i���R�[�h�j�d�l�R�[�h
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_H_HIN_CD"))) & ","       '�i�i���R�[�h�j�i��R�[�h
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_H_SENSIN_CD"))) & ","    '�i�i���R�[�h�j���S���R�[�h
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_H_SIZE_CD"))) & ","      '�i�i���R�[�h�j�T�C�Y�R�[�h
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_H_COLOR_CD"))) & ","     '�i�i���R�[�h�j�F�R�[�h
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_FUKA_CD"))) & ","        '�݌v�t���L��
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_HINMEI"))) & ","         '�i��
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_HIN_DATA"))) & ","       '�i���f�[�^
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_SIZE_DATA"))) & ","      '�T�C�Y�f�[�^
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_COLOR_DATA"))) & ","     '�F�f�[�^
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_LOT_CHO"))) & ","        '��z����
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_TANCYO_KBN"))) & ","     '�P���敪
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_TANCYO"))) & ","         '����P��
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_JYO_SUU"))) & ","        '��
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_MAKI_CD"))) & ","
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_HOSO_KBN"))) & ","       '��敪
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_SIYOUSYO_NO"))) & ","    '�d�l����
                If (lSeiKbn = lH03_GAICHUU) Or (lSeiKbn = lH03_KOUNYUU) Then '����敪�F�O��(2)�w��(3)       '���L�����i����������j
                    '�O���E�w���i�̏ꍇ�A�ȖڃR�[�h��t���i�R�i�ځ^45�޲ā`�j
                    SQL = SQL & N & "'" & sTokki1 & Space(22) & Format(_db.rmNullStr(.Rows(i)("ZW_KAMOKU_CD")), "000000") & "',"
                Else
                    SQL = SQL & N & "'" & sTokki1 & "',"
                End If
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_TENKAI_KBN"))) & ","     '�W�J�敪
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_BBNKOUTEI"))) & ","      '�w��H��
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_HINSITU_KBN"))) & ","    '�i�������敪
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_KEISAN_KBN"))) & ","     '���H���v�Z
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_TATIAI_UM"))) & ","      '����L��
                'SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_KONYU_SIYOU"))) & ","    '�w���i�p�d�l�R�[�h
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_KAMOKU_CD"))) & ","
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_N_SO_SUU"))) & ","       '���ɖ{���S��
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_N_K_SUU"))) & ","        '�k���{�{��
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_N_SH_SUU"))) & ","       '�Z�d�����{��
                '-->2010.12.22 add by takagi 
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("TT_JUYOUCD"))) & ","
                '<--2010.12.22 add by takagi 
                SQL = SQL & N & "'" & UtilClass.getComputerName() & "',"
                SQL = SQL & N & "TO_DATE('" & nowdt & "','YYYY/MM/DD HH24:MI:SS'))"             '�X�V��
                _db.executeDB(SQL)
            Next
        End With

    End Sub

    'insert���p������ҏW(�����p)
    Private Function impDtForStr(ByVal prmVal As String) As String
        If "".Equals(prmVal) Then
            Return "NULL"
        Else
            Return "'" & prmVal & "'"
        End If
    End Function

    'insert���p������ҏW(���l�p)
    Private Function impDtForNum(ByVal prmVal As String) As String
        If "".Equals(prmVal) Then
            Return "NULL"
        Else
            Return "" & prmVal & ""
        End If
    End Function

    '�p�^�[���}�g���N�X�ɏ]�����o�����̎擾
    Private Function getShuttaibi(ByRef prmRefPattern As abcType, ByRef prmRefShuttaiHash As Hashtable, _
                                  ByRef prmRefABC As String, ByRef prmRefLotNum As Integer) As String()
        Dim ret() As String = {}
        Dim i As Integer = 0

        '�P�`�U���b�g�͊e�p�^�[���ɏ]���B
        '�U���̏ꍇ�͂U���b�g�P�ʂɏ��Z���A�U���b�g���̓p�^�[���U�̗v�̂œW�J����
        '���̗]�蕪�̓p�^�[���P�`�T�ɉ����ēW�J����

        Do

            If "S".Equals(prmRefABC) Then
                '�����i
                If (prmRefLotNum - (i * 6)) \ 6 > 0 Then
                    Call getShuttaibiAry(prmRefPattern.chozoHin.shuttaiBi6, prmRefShuttaiHash, ret)
                Else
                    Select Case (prmRefLotNum - (i * 6)) Mod 6
                        Case 1 : Call getShuttaibiAry(prmRefPattern.chozoHin.shuttaiBi1, prmRefShuttaiHash, ret)
                        Case 2 : Call getShuttaibiAry(prmRefPattern.chozoHin.shuttaiBi2, prmRefShuttaiHash, ret)
                        Case 3 : Call getShuttaibiAry(prmRefPattern.chozoHin.shuttaiBi3, prmRefShuttaiHash, ret)
                        Case 4 : Call getShuttaibiAry(prmRefPattern.chozoHin.shuttaiBi4, prmRefShuttaiHash, ret)
                        Case 5 : Call getShuttaibiAry(prmRefPattern.chozoHin.shuttaiBi5, prmRefShuttaiHash, ret)
                        Case 0 : Call getShuttaibiAry(prmRefPattern.chozoHin.shuttaiBi6, prmRefShuttaiHash, ret)
                    End Select
                End If
            Else
                '�ʏ�
                If (prmRefLotNum - (i * 6)) \ 6 > 0 Then
                    Call getShuttaibiAry(prmRefPattern.nomal.shuttaiBi6, prmRefShuttaiHash, ret)
                Else
                    Select Case (prmRefLotNum - (i * 6)) Mod 6
                        Case 1 : Call getShuttaibiAry(prmRefPattern.nomal.shuttaiBi1, prmRefShuttaiHash, ret)
                        Case 2 : Call getShuttaibiAry(prmRefPattern.nomal.shuttaiBi2, prmRefShuttaiHash, ret)
                        Case 3 : Call getShuttaibiAry(prmRefPattern.nomal.shuttaiBi3, prmRefShuttaiHash, ret)
                        Case 4 : Call getShuttaibiAry(prmRefPattern.nomal.shuttaiBi4, prmRefShuttaiHash, ret)
                        Case 5 : Call getShuttaibiAry(prmRefPattern.nomal.shuttaiBi5, prmRefShuttaiHash, ret)
                        Case 0 : Call getShuttaibiAry(prmRefPattern.nomal.shuttaiBi6, prmRefShuttaiHash, ret)
                    End Select
                End If
            End If
            i += 1

        Loop Until (prmRefLotNum <= 6) Or (i > IIf((prmRefLotNum Mod 6) <> 0, (prmRefLotNum \ 6), (prmRefLotNum \ 6) - 1))

        Return ret
    End Function

    '�o�����z��̎擾
    Private Sub getShuttaibiAry(ByVal prmBit As String, ByRef prmRefShuttaiHash As Hashtable, ByRef prmRefTarget() As String)

        For i As Integer = 0 To prmBit.Length - 1
            If "1".Equals(prmBit.Substring(i, 1)) Then
                ReDim Preserve prmRefTarget(UBound(prmRefTarget) + 1)
                prmRefTarget(UBound(prmRefTarget)) = prmRefShuttaiHash.Item(i + 1).ToString
            End If
        Next

    End Sub

End Class