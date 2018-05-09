'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���j�����z�c�a�o�^
'    �i�t�H�[��ID�jZG640B_SeisakuTehaiDB
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
Public Class ZG640B_SeisakuTehaiDB
    Inherits System.Windows.Forms.Form

    '-------------------------------------------------------------------------------
    '   �\���̒�`
    '-------------------------------------------------------------------------------

    '-------------------------------------------------------------------------------
    '   �萔��`
    '-------------------------------------------------------------------------------
    Private Const RS As String = "RecSet"                       '�f�[�^�Z�b�g�e�[�u����
    Private Const N As String = ControlChars.NewLine            '���s����
    Private Const PGID As String = "ZG640B"


    Private Const lZM05_SEISAKU_IRAI_NO As Integer = 0     '����˗����F�O�i���ьŒ�j
    Private Const lMZ05_SENDFLG_OFF As Integer = 0         '���M�σt���O�F�����M
    Private Const lMZ05_SENDFLG_ON As Integer = 1          '���M�σt���O�F���M��
    Private Const lMZ05_KOUJYOK_NOTHING As Integer = 0     '�H��敪�F�Ȃ�
    Private Const lMZ05_KOUJYOK_DENRYOKU  As Integer = 1    '�H��敪�F�d��
    Private Const lMZ05_KOUJYOK_JYOUHOU As Integer = 2     '�H��敪�F���
    Private Const lMZ05_SEKKEI_MST_EXIST As Integer = 0    '�݌v�}�X�^�F�L��i���ьŒ�j
    Private Const lMZ05_ZAIRYO_MST_EXIST As Integer = 1    '�ޗ��}�X�^�F�L��i���ьŒ�j
    Private Const lMZ05_KOUTEI_MST_EXIST As Integer = 1    '�H���}�X�^�F�L��i���ьŒ�j
    Private Const lMZ05_TOROKU_KBN_TOROKU As Integer = 1   '�o�^�敪�F�o�^�i���ьŒ�j
    Private Const sMZ05_TEHAI_HAKKOU_SYA As String = "�݌Ɏ�z"   '��z���s�ҁF�݌Ɏ�z�i���ьŒ�j
    Private Const sMZ05_SEKKEI_KAKUNIN_SYA As String = "�݌Ɏ�z" '�݌v�[�m�F�ҁF�݌Ɏ�z�i���ьŒ�j
    Private Const sMZ05_ZAIRYO_KAKUNIN_SYA As String = "�݌Ɏ�z" '�ޗ��[�m�F�ҁF�݌Ɏ�z�i���ьŒ�j
    Private Const sMZ05_KOUTEI_KAKUNIN_SYA As String = "�݌Ɏ�z" '�H���[�m�F�ҁF�݌Ɏ�z�i���ьŒ�j
    Private Const sMZ05_TEHAI_TOUROKU_SYA As String = "�݌Ɏ�z"  '��z�o�^�ҁF�݌Ɏ�z�i���ьŒ�j

    '-------------------------------------------------------------------------------
    '   �ϐ���`
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu
    Private _updFlg As Boolean = False  '�X�V��

    '-------------------------------------------------------------------------------
    '�R���X�g���N�^�iPrivate�ɂ��āA�O����͌ĂׂȂ��悤�ɂ���j
    '-------------------------------------------------------------------------------
    Private Sub New()
        ' ���̌Ăяo���́AWindows �t�H�[�� �f�U�C�i�ŕK�v�ł��B
        InitializeComponent()

    End Sub

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
    Private Sub ZG640B_SeisakuTehaiDB_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            sql = sql & N & "    from T91RIREKI "
            sql = sql & N & "    where PGID     = '" & PGID & "' "
            sql = sql & N & ") "
            sql = sql & N & "where RNUM = 1 "
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)
            If iRecCnt < 1 Then
                '�����Ȃ�
                lblJikkouDate.Text = ZC110M_Menu.NON_EXECUTE
                lblZTehaiData.Text = ""
                lblKihon.Text = ""
                lblTantyo.Text = ""
                lblDaitai.Text = ""
            Else
                '��������
                lblJikkouDate.Text = _db.rmNullDate(ds.Tables(RS).Rows(0)("SDATEEND"))
                lblZTehaiData.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU1")), "#,##0")
                lblKihon.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU2")), "#,##0")
                lblTantyo.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU3")), "#,##0")
                lblDaitai.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU4")), "#,##0")
            End If

            '������s���̕\��
            '2011/01/28 chg start Sugawara #95
            'lblKTehaiData.Text = Format(_db.rmNullInt(_db.selectDB("select count(*) CNT from T51TEHAI ", RS).Tables(RS).Rows(0)("CNT")), "#,##0")
            lblKTehaiData.Text = Format(_db.rmNullInt(_db.selectDB("select count(*) CNT from T51TEHAI where GAI_FLG is null ", RS).Tables(RS).Rows(0)("CNT")), "#,##0")
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
                    pb.jobName = "�����z�f�[�^���쐬���Ă��܂��B"
                    pb.oneStep = 1

                    pb.status = "�쐬��"
                    Dim outputCnt As Integer = 0
                    Dim kihonCnt As Integer = 0
                    Dim danchoCnt As Integer = 0
                    Dim daitaiCnt As Integer = 0

                    _db.beginTran()
                    Try
                        Call insertSeisakuDB(kihonCnt, danchoCnt, daitaiCnt, pb)

                        pb.status = "�X�e�[�^�X�ύX���E�E�E"
                        ed = Now                    '�����I������
                        _parentForm.updateSeigyoTbl(PGID, True, st, ed)

                        pb.status = "���s�����쐬"
                        Call insertRireki(kihonCnt, danchoCnt, daitaiCnt, st, ed)                                  '2-1 ���s�����i�[
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
    Private Sub insertRireki(ByVal prmKihonCnt As Integer, ByVal prmTanchoCnt As Integer, ByVal prmDaitaiCnt As Integer, ByVal prmStDt As Date, ByVal prmEdDt As Date)

        Dim sql As String = ""
        sql = sql & N & "insert into T91RIREKI "
        sql = sql & N & "( "
        sql = sql & N & " SNENGETU "   '�����N��
        sql = sql & N & ",KNENGETU "   '�v��N��
        sql = sql & N & ",PGID "       '�@�\ID
        sql = sql & N & ",SDATESTART " '�����J�n����
        sql = sql & N & ",SDATEEND "   '�����I������
        sql = sql & N & ",KENNSU1 "    '���s����
        sql = sql & N & ",KENNSU2 "    '��{���s����
        sql = sql & N & ",KENNSU3 "    '�P�����s����
        sql = sql & N & ",KENNSU4 "    '��֎��s����
        sql = sql & N & ",UPDNAME "    '�ŏI�X�V��
        sql = sql & N & ",UPDDATE "    '�ŏI�X�V��
        sql = sql & N & ")values( "
        sql = sql & N & "  '" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "' "                             '�����N��
        sql = sql & N & ", '" & _db.rmSQ(lblKeikakuDate.Text.Replace("/", "")) & "' "                           '�v��N��
        sql = sql & N & ", '" & _db.rmSQ(PGID) & "' "                                                           '�@�\ID
        sql = sql & N & ",to_date('" & Format(prmStDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '�����J�n����
        sql = sql & N & ",to_date('" & Format(prmEdDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '�����I������
        sql = sql & N & ", " & CLng(lblKTehaiData.Text) & " "                                                  '���͌���
        sql = sql & N & ", " & prmKihonCnt & " "                                                  '���͌���
        sql = sql & N & ", " & prmTanchoCnt & " "                                                  '���͌���
        sql = sql & N & ", " & prmDaitaiCnt & " "                                                  '���͌���
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "                                     '�ŏI�X�V��
        sql = sql & N & ",sysdate "                                                                             '�ŏI�X�V��
        sql = sql & N & ") "
        _db.executeDB(sql)

    End Sub

    '-------------------------------------------------------------------------------
    '�@ �����z�c�a�o�^
    '   �i�����T�v�j��z���[�N�c�a�̃f�[�^�𐻍��z�c�a�ɓo�^����B
    '              [�ΏۊO�t���O�n�m(1)�̃f�[�^�͏����B]
    '-------------------------------------------------------------------------------
    Public Sub insertSeisakuDB(ByRef prmKihonCnt As Integer, ByRef prmTanchoCnt As Integer, ByRef prmDaitaiCnt As Integer, ByRef prmRefBar As UtilProgressBar)

        Dim SQL As String = ""
        SQL = SQL & N & "SELECT "
        SQL = SQL & N & " TEHAI_NO"         ' 0�F��z��
        SQL = SQL & N & ",SYORI_YM"         ' 1�F�����N��
        SQL = SQL & N & ",SYORI_KBN"        ' 2�F�����敪
        SQL = SQL & N & ",KIBOU_DATE"       ' 3�F��]�N����
        'SQL = SQL & N & ",NOUKI"            ' 4�F�[��
        SQL = SQL & N & ",TEHAI_KBN"        ' 5�F��z�敪
        SQL = SQL & N & ",SEISAKU_KBN"      ' 6�F����敪
        SQL = SQL & N & ",SEIZOU_BMN"       ' 7�F��������
        SQL = SQL & N & ",DENPYOK"          ' 8�F�`�[�敪
        SQL = SQL & N & ",TYUMONSAKI"       ' 9�F������
        SQL = SQL & N & ",H_SIYOU_CD"       '10�F�i�i���R�[�h�j�d�l�R�[�h
        SQL = SQL & N & ",H_HIN_CD"         '11�F�i�i���R�[�h�j�i��R�[�h
        SQL = SQL & N & ",H_SENSIN_CD"      '12�F�i�i���R�[�h�j���S���R�[�h
        SQL = SQL & N & ",H_SIZE_CD"        '13�F�i�i���R�[�h�j�T�C�Y�R�[�h
        SQL = SQL & N & ",H_COLOR_CD"       '14�F�i�i���R�[�h�j�F�R�[�h
        SQL = SQL & N & ",FUKA_CD"          '15�F�݌v�t���L��
        SQL = SQL & N & ",HINMEI"           '16�F�i��
        SQL = SQL & N & ",TEHAI_SUU"        '17�F��z����
        SQL = SQL & N & ",TANCYO_KBN"       '18�F�P���敪
        SQL = SQL & N & ",TANCYO"           '19�F����P��
        SQL = SQL & N & ",JYOSU"            '20�F��
        SQL = SQL & N & ",MAKI_CD"          '21�F���g�R�[�h
        SQL = SQL & N & ",HOSO_KBN"         '22�F��敪
        SQL = SQL & N & ",SIYOUSYO_NO"      '23�F�d�l����
        SQL = SQL & N & ",TOKKI"            '24�F���L����
        SQL = SQL & N & ",BIKO"             '25�F���l
        SQL = SQL & N & ",HENKO"            '26�F�ύX���e
        SQL = SQL & N & ",TENKAI_KBN"       '27�F�W�J�敪
        SQL = SQL & N & ",BBNKOUTEI"        '28�F�w��H��
        SQL = SQL & N & ",HINSITU_KBN"      '29�F�i�������敪
        SQL = SQL & N & ",KEISAN_KBN"       '30�F���H���v�Z
        SQL = SQL & N & ",TATIAI_UM"        '31�F����L��
        SQL = SQL & N & ",TACIAIBI"         '32�F����\���
        SQL = SQL & N & ",SEISEKI"          '33�F���я�
        SQL = SQL & N & ",MYTANCYO"         '34�F�����]���i�P�����j
        SQL = SQL & N & ",MYLOT"            '35�F�����]���i���b�g���j
        SQL = SQL & N & ",TYTANCYO"         '36�F����]���i�P�����j
        SQL = SQL & N & ",TYLOT"            '37�F����]���i���b�g���j
        SQL = SQL & N & ",SYTANCYO"         '38�F�w��Ќ��]���i�P�����j
        SQL = SQL & N & ",SYLOT"            '39�F�w��Ќ��]���i���b�g���j
        'SQL = SQL & N & ",HYOJYUNC_1"       '40�F�W���H���R�[�h_1
        'SQL = SQL & N & ",KOUTEIC_1"        '41�F�H���R�[�h_1
        'SQL = SQL & N & ",KOUTEIJ_1"        '42�F�H������_1
        'SQL = SQL & N & ",TEIIN_1"          '43�F���_1
        'SQL = SQL & N & ",DANDORI_1"        '44�F�i��_1
        'SQL = SQL & N & ",KIJYUN_1"         '45�F��o����_1
        'SQL = SQL & N & ",HYOJYUNC_2"       '46�F�W���H���R�[�h_2
        'SQL = SQL & N & ",KOUTEIC_2"        '47�F�H���R�[�h_2
        'SQL = SQL & N & ",KOUTEIJ_2"        '48�F�H������_2
        'SQL = SQL & N & ",TEIIN_2"          '49�F���_2
        'SQL = SQL & N & ",DANDORI_2"        '50�F�i��_2
        'SQL = SQL & N & ",KIJYUN_2"         '51�F��o����_2
        'SQL = SQL & N & ",HYOJYUNC_3"       '52�F�W���H���R�[�h_3
        'SQL = SQL & N & ",KOUTEIC_3"        '53�F�H���R�[�h_3
        'SQL = SQL & N & ",KOUTEIJ_3"        '54�F�H������_3
        'SQL = SQL & N & ",TEIIN_3"          '55�F���_3
        'SQL = SQL & N & ",DANDORI_3"        '56�F�i��_3
        'SQL = SQL & N & ",KIJYUN_3"         '57�F��o����_3
        'SQL = SQL & N & ",HYOJYUNC_4"       '58�F�W���H���R�[�h_4
        'SQL = SQL & N & ",KOUTEIC_4"        '59�F�H���R�[�h_4
        'SQL = SQL & N & ",KOUTEIJ_4"        '60�F�H������_4
        'SQL = SQL & N & ",TEIIN_4"          '61�F���_4
        'SQL = SQL & N & ",DANDORI_4"        '62�F�i��_4
        'SQL = SQL & N & ",KIJYUN_4"         '63�F��o����_4
        'SQL = SQL & N & ",HYOJYUNC_5"       '64�F�W���H���R�[�h_5
        'SQL = SQL & N & ",KOUTEIC_5"        '65�F�H���R�[�h_5
        'SQL = SQL & N & ",KOUTEIJ_5"        '66�F�H������_5
        'SQL = SQL & N & ",TEIIN_5"          '67�F���_5
        'SQL = SQL & N & ",DANDORI_5"        '68�F�i��_5
        'SQL = SQL & N & ",KIJYUN_5"         '69�F��o����_5
        SQL = SQL & N & ",HIN_DATA"         '70�F�i���f�[�^
        SQL = SQL & N & ",SIZE_DATA"        '71�F�T�C�Y�f�[�^
        SQL = SQL & N & ",COLOR_DATA"       '72�F�F�f�[�^
        SQL = SQL & N & ",UPDDATE "         '73�F�X�V��
        SQL = SQL & N & "FROM T51TEHAI "
        '2011/01/28 add start Sugawara #95
        SQL = SQL & N & "WHERE GAI_FLG IS NULL " '(�����l�FNULL�A�ΏۊO�F1) �ΏۊO�f�[�^�������B
        '2011/01/28 add end Sugawara #95
        SQL = SQL & N & "ORDER BY TEHAI_NO"
        Dim ds As DataSet = _db.selectDB(SQL, RS)

        For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1

            '�����z�c�a�i��{���j�X�V������쐬
            Call EditSQL_Tehai1(ds.Tables(RS).Rows(i))
            '2011/01/28 add start Sugawara #96
            prmKihonCnt = prmKihonCnt + 1
            '2011/01/28 add end Sugawara #96

            '�����z�c�a�i�P�����j�X�V������쐬
            Call insertTehai2(ds.Tables(RS).Rows(i))
            '2011/01/28 add start Sugawara #96
            prmTanchoCnt = prmTanchoCnt + 1
            '2011/01/28 add end Sugawara #96

            'For lDCnt As Integer = 1 To 5
            '    If _db.rmNullInt(ds.Tables(RS).Rows(i)("KOUTEIJ_" & lDCnt)) <> 0 Then
            '        '�����z�c�a�i��֕��j�X�V������쐬
            '        Call insertTehai3(ds.Tables(RS).Rows(i), lDCnt)
            '        2011/01/28 add start Sugawara #96
            '        prmDaitaiCnt = prmDaitaiCnt + 1
            '        2011/01/28 add end Sugawara #96
            '    End If
            'Next
        Next

    End Sub

    '-------------------------------------------------------------------------------
    '�@ �����z�c�a�ǉ��r�p�k���쐬�i��{���j
    '   �i�����T�v�j�����z�c�a�i��{���j�ɒǉ�����r�p�k����ҏW����B
    '-------------------------------------------------------------------------------
    Private Sub EditSQL_Tehai1(ByVal prmRow As DataRow)

        '�i���R�[�h�ҏW
        Dim sHinCD As String = ""
        sHinCD += UtilClass.setDataLen(_db.rmNullStr(prmRow("H_SIYOU_CD")), 2)
        sHinCD += UtilClass.setDataLen(_db.rmNullStr(prmRow("H_HIN_CD")), 3)
        sHinCD += UtilClass.setDataLen(_db.rmNullStr(prmRow("H_SENSIN_CD")), 3)
        sHinCD += UtilClass.setDataLen(_db.rmNullStr(prmRow("H_SIZE_CD")), 2)
        sHinCD += UtilClass.setDataLen(_db.rmNullStr(prmRow("H_COLOR_CD")), 3)

        '�����i���ҏW
        Dim sOdrHinmei As String = ""
        sOdrHinmei += UtilClass.setDataLen(_db.rmNullStr(prmRow("HIN_DATA")), 13)
        sOdrHinmei += UtilClass.setDataLen(_db.rmNullStr(prmRow("SIZE_DATA")), 8)
        sOdrHinmei += UtilClass.setDataLen(_db.rmNullStr(prmRow("COLOR_DATA")), 2)

        '�������偨�H��敪�ϊ�
        Dim lFctKBN As Long
        Select Case _db.rmNullInt(prmRow("SEIZOU_BMN"))
            Case 1 : lFctKBN = lMZ05_KOUJYOK_JYOUHOU '�ʐM
            Case 3 : lFctKBN = lMZ05_KOUJYOK_DENRYOKU '�d��
            Case Else : lFctKBN = lMZ05_KOUJYOK_NOTHING '���̑��i�G���[�j
        End Select

        '�V�X�e�����擾
        Dim sEntDate As String = CStr(Format(Now, "yyyyMMdd"))

        Dim SQL As String = ""
        SQL = SQL & N & "INSERT INTO IN_TEHAI1_TB"
        SQL = SQL & N & "("
        SQL = SQL & N & " IT1_TEHAINO,"     ' 1�F��z��
        SQL = SQL & N & " IT1_IRAINO,"      ' 2�F����˗���
        SQL = SQL & N & " IT1_SYORIK,"      ' 3�F�����敪
        SQL = SQL & N & " IT1_TEHAIK,"      ' 4�F��z�敪
        SQL = SQL & N & " IT1_SEISAKK,"     ' 5�F����敪
        SQL = SQL & N & " IT1_KOUJYOK,"     ' 6�F�H��敪
        SQL = SQL & N & " IT1_DENPYOK,"     ' 7�F�`�[�敪
        SQL = SQL & N & " IT1_DEKIBI,"      ' 8�F��]�o����
        SQL = SQL & N & " IT1_NOUKI,"       ' 9�F�[��
        SQL = SQL & N & " IT1_KYAKSAKI,"    '10�F������
        SQL = SQL & N & " IT1_HINMEIC,"     '11�F�i���R�[�h
        SQL = SQL & N & " IT1_FUKAC,"       '12�F�݌v�t���L��
        SQL = SQL & N & " IT1_KNAME,"       '13�F�����i��
        SQL = SQL & N & " IT1_SNAME,"       '14�F�݌v�[�i��
        SQL = SQL & N & " IT1_ZNAME,"       '15�F�ޗ��[�i��
        SQL = SQL & N & " IT1_SIYOSYONO,"   '16�F�d�l���ԍ�
        SQL = SQL & N & " IT1_SURYO,"       '17�F��z����
        SQL = SQL & N & " IT1_TANCYOK,"     '18�F�P���敪
        SQL = SQL & N & " IT1_TENKAIK,"     '19�F�W�J�敪
        SQL = SQL & N & " IT1_BBNKOTEI,"    '20�F�����W�J�w��H��
        SQL = SQL & N & " IT1_HINSITUK,"    '21�F�i�������敪
        SQL = SQL & N & " IT1_KEISANK,"     '22�F���H���v�Z�敪
        SQL = SQL & N & " IT1_TACIAIUM,"    '23�F����L��
        SQL = SQL & N & " IT1_TACIAIBI,"    '24�F����\���
        SQL = SQL & N & " IT1_SEISEKI,"     '25�F���я�
        SQL = SQL & N & " IT1_MYTANCYO,"    '26�F�����]���i�P�����j
        SQL = SQL & N & " IT1_MYLOT,"       '27�F�����]���i���b�g���j
        SQL = SQL & N & " IT1_TYTANCYO,"    '28�F����]���i�P�����j
        SQL = SQL & N & " IT1_TYLOT,"       '29�F����]���i���b�g���j
        SQL = SQL & N & " IT1_SYTANCYO,"    '30�F�w��Ќ����i�P�����j
        SQL = SQL & N & " IT1_SYLOT,"       '31�F�w��Ќ����i���b�g���j
        SQL = SQL & N & " IT1_TOKKI,"       '32�F���L����
        SQL = SQL & N & " IT1_BIKO,"        '33�F���l
        SQL = SQL & N & " IT1_HENKO,"       '34�F�ύX���e
        SQL = SQL & N & " IT1_SMASTUM,"     '35�F�݌v�}�X�^�L��
        SQL = SQL & N & " IT1_ZMASTUM,"     '36�F�ޗ��}�X�^�L��
        SQL = SQL & N & " IT1_KMASTUM,"     '37�F�H���}�X�^�L��
        SQL = SQL & N & " IT1_TOROKK,"      '38�F�o�^�敪
        SQL = SQL & N & " IT1_HAKKOSYA,"    '39�F��z���s��
        SQL = SQL & N & " IT1_HAKKOBI,"     '40�F��z���s��
        SQL = SQL & N & " IT1_SKNSYA,"      '41�F�݌v�[�m�F��
        SQL = SQL & N & " IT1_SKNBI,"       '42�F�݌v�[�m�F��
        SQL = SQL & N & " IT1_ZKNSYA,"      '43�F�ޗ��[�m�F��
        SQL = SQL & N & " IT1_ZKNBI,"       '44�F�ޗ��[�m�F��
        SQL = SQL & N & " IT1_KKNSYA,"      '45�F�H���[�m�F��
        SQL = SQL & N & " IT1_KKNBI,"       '46�F�H���[�m�F��
        SQL = SQL & N & " IT1_TOROKSYA,"    '47�F��z�o�^��
        SQL = SQL & N & " IT1_TOROKBI,"     '48�F��z�o�^��
        SQL = SQL & N & " IT1_SENDFLG"      '49�F���M�σt���O
        SQL = SQL & N & ")"
        SQL = SQL & N & " VALUES("
        SQL = SQL & N & _db.rmNullInt(prmRow("TEHAI_NO")) & ","                 ' 1�F��z��
        SQL = SQL & N & lZM05_SEISAKU_IRAI_NO & ","                             ' 2�F����˗���
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("SYORI_KBN"))) & ","   ' 3�F�����敪
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("TEHAI_KBN"))) & ","   ' 4�F��z�敪
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("SEISAKU_KBN"))) & "," ' 5�F����敪
        SQL = SQL & N & lFctKBN & ","                                           ' 6�F�H��敪
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("DENPYOK"))) & ","     ' 7�F�`�[�敪
        SQL = SQL & N & impDtForStr(_db.rmNullStr(prmRow("KIBOU_DATE"))) & ","  ' 8�F��]�o����
        'SQL = SQL & N & impDtForStr(_db.rmNullStr(prmRow("NOUKI"))) & ","       ' 9�F�[��
        SQL = SQL & N & impDtForStr("") & ","       ' 9�F�[��
        SQL = SQL & N & impDtForStr(_db.rmNullStr(prmRow("TYUMONSAKI"))) & ","  '10�F������
        SQL = SQL & N & impDtForStr(sHinCD) & ","                               '11�F�i���R�[�h
        SQL = SQL & N & impDtForStr(_db.rmNullStr(prmRow("FUKA_CD"))) & ","     '12�F�݌v�t���L��
        SQL = SQL & N & impDtForStr(sOdrHinmei) & ","                           '13�F�����i��
        SQL = SQL & N & impDtForStr(_db.rmNullStr(prmRow("HINMEI"))) & ","      '14�F�݌v�[�i��
        SQL = SQL & N & impDtForStr(_db.rmNullStr(prmRow("HINMEI"))) & ","      '15�F�ޗ��[�i��
        SQL = SQL & N & impDtForStr(_db.rmNullStr(prmRow("SIYOUSYO_NO"))) & "," '16�F�d�l���ԍ�
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("TEHAI_SUU"))) & ","   '17�F��z����
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("TANCYO_KBN"))) & ","  '18�F�P���敪
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("TENKAI_KBN"))) & ","  '19�F�W�J�敪
        SQL = SQL & N & impDtForStr(_db.rmNullStr(prmRow("BBNKOUTEI"))) & ","   '20�F�����W�J�w��H��
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("HINSITU_KBN"))) & "," '21�F�i�������敪
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("KEISAN_KBN"))) & ","  '22�F���H���v�Z�敪
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("TATIAI_UM"))) & ","   '23�F����L��
        SQL = SQL & N & "" & "NULL" & ","                                       '24�F����\���
        SQL = SQL & N & "" & "NULL" & ","                                       '25�F���я�
        SQL = SQL & N & "" & "NULL" & ","                                       '26�F�����]���i�P�����j
        SQL = SQL & N & "" & "NULL" & ","                                       '27�F�����]���i���b�g���j
        SQL = SQL & N & "" & "NULL" & ","                                       '28�F����]���i�P�����j
        SQL = SQL & N & "" & "NULL" & ","                                       '29�F����]���i���b�g���j
        SQL = SQL & N & "" & "NULL" & ","                                       '30�F�w��Ќ����i�P�����j
        SQL = SQL & N & "" & "NULL" & ","                                       '31�F�w��Ќ����i���b�g���j
        SQL = SQL & N & impDtForStr(_db.rmNullStr(prmRow("TOKKI"))) & ","       '32�F���L����
        SQL = SQL & N & impDtForStr(_db.rmNullStr(prmRow("BIKO"))) & ","        '33�F���l
        SQL = SQL & N & impDtForStr(_db.rmNullStr(prmRow("HENKO"))) & ","       '34�F�ύX���e
        SQL = SQL & N & "" & lMZ05_SEKKEI_MST_EXIST & ","                       '35�F�݌v�}�X�^�L��
        SQL = SQL & N & "" & lMZ05_ZAIRYO_MST_EXIST & ","                       '36�F�ޗ��}�X�^�L��
        SQL = SQL & N & "" & lMZ05_KOUTEI_MST_EXIST & ","                       '37�F�H���}�X�^�L��
        SQL = SQL & N & "" & lMZ05_TOROKU_KBN_TOROKU & ","                      '38�F�o�^�敪
        SQL = SQL & N & "'" & sMZ05_TEHAI_HAKKOU_SYA & "',"                     '39�F��z���s��
        SQL = SQL & N & "'" & sEntDate & "',"                                   '40�F��z���s��
        SQL = SQL & N & "'" & sMZ05_SEKKEI_KAKUNIN_SYA & "',"                   '41�F�݌v�[�m�F��
        SQL = SQL & N & "'" & sEntDate & "',"                                   '42�F�݌v�[�m�F��
        SQL = SQL & N & "'" & sMZ05_ZAIRYO_KAKUNIN_SYA & "',"                   '43�F�ޗ��[�m�F��
        SQL = SQL & N & "'" & sEntDate & "',"                                   '44�F�ޗ��[�m�F��
        SQL = SQL & N & "'" & sMZ05_KOUTEI_KAKUNIN_SYA & "',"                   '45�F�H���[�m�F��
        SQL = SQL & N & "'" & sEntDate & "',"                                   '46�F�H���[�m�F��
        SQL = SQL & N & "'" & sMZ05_TEHAI_TOUROKU_SYA & "',"                    '47�F��z�o�^��
        SQL = SQL & N & "'" & sEntDate & "',"                                   '48�F��z�o�^��
        SQL = SQL & N & "" & lMZ05_SENDFLG_ON & ""                              '49�F���M�σt���O
        SQL = SQL & N & ")"
        _db.executeDB(SQL)

    End Sub

    '-------------------------------------------------------------------------------
    '�@ �����z�c�a�ǉ��r�p�k���쐬�i�P�����j
    '   �i�����T�v�j�����z�c�a�i�P���j�ɒǉ�����r�p�k����ҏW����B
    '-------------------------------------------------------------------------------
    Private Function insertTehai2(ByVal prmRow As DataRow) As Boolean

        Dim SQL As String = ""
        SQL = SQL & N & "INSERT INTO IN_TEHAI2_TB"
        SQL = SQL & N & "("
        SQL = SQL & N & " IT2_TEHAINO,"     ' 1�F��z��
        SQL = SQL & N & " IT2_RENBAN,"      ' 2�F�A��
        SQL = SQL & N & " IT2_TANCYO,"      ' 3�F�P��
        SQL = SQL & N & " IT2_JYOSU,"       ' 4�F��
        SQL = SQL & N & " IT2_MAKIWAKC,"    ' 5�F���g�R�[�h
        SQL = SQL & N & " IT2_HOSOK"        ' 6�F��^�\���敪
        SQL = SQL & N & ")"
        SQL = SQL & N & " VALUES("
        SQL = SQL & N & _db.rmNullInt(prmRow("TEHAI_NO")) & ","             ' 1�F��z��
        SQL = SQL & N & " 1,"                                               ' 2�F�A��
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("TANCYO"))) & ","  ' 3�F�P��
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("JYOSU"))) & ","   ' 4�F��
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("MAKI_CD"))) & "," ' 5�F���g�R�[�h
        SQL = SQL & N & impDtForStr(_db.rmNullStr(prmRow("HOSO_KBN")))      ' 6�F��^�\���敪
        SQL = SQL & N & ")"
        _db.executeDB(SQL)

    End Function

    '-------------------------------------------------------------------------------
    '�@ �����z�c�a�ǉ��r�p�k���쐬�i��֕��j
    '   �i�����T�v�j�����z�c�a�i��֕��j�ɒǉ�����r�p�k����ҏW����B
    '-------------------------------------------------------------------------------
    Private Function insertTehai3(ByVal prmRow As DataRow, ByVal lPrmRen As Long) As Boolean
        Dim SQL As String = ""
        SQL = SQL & N & "INSERT INTO IN_TEHAI3_TB"
        SQL = SQL & N & "("
        SQL = SQL & N & " IT3_TEHAINO,"     ' 1�F��z��
        SQL = SQL & N & " IT3_RENBAN,"      ' 2�F�A��
        SQL = SQL & N & " IT3_HYOJYNC,"     ' 3�F�W���H���R�[�h
        SQL = SQL & N & " IT3_KOTEIC,"      ' 4�F�H���R�[�h
        SQL = SQL & N & " IT3_KOTEIJ,"      ' 5�F�H������
        SQL = SQL & N & " IT3_TEIIN,"       ' 6�F���
        SQL = SQL & N & " IT3_DANDORI,"     ' 7�F�i��
        SQL = SQL & N & " IT3_KIJYN,"       ' 8�F��o����
        SQL = SQL & N & " IT3_STARTS,"      ' 9�F����ذٗ]���E����
        SQL = SQL & N & " IT3_STARTL,"      '10�F����ذٗ]���E׽�
        SQL = SQL & N & " IT3_LASTS,"       '11�F׽�ذٗ]���E����
        SQL = SQL & N & " IT3_LASTL,"       '12�F׽�ذٗ]���E׽�
        SQL = SQL & N & " IT3_LENGTH,"      '13�F�ő努�撷
        SQL = SQL & N & " IT3_CONTROL"      '14�F�v�Z����
        SQL = SQL & N & ")"
        SQL = SQL & N & " VALUES("
        SQL = SQL & N & _db.rmNullInt(prmRow("TEHAI_NO")) & ","                         ' 1�F��z��
        SQL = SQL & N & lPrmRen & ","                                                   ' 2�F�A��
        SQL = SQL & N & impDtForStr(_db.rmNullStr(prmRow("HYOJYUNC_" & lPrmRen))) & "," ' 3�F�W���H���R�[�h
        SQL = SQL & N & impDtForStr(_db.rmNullStr(prmRow("KOUTEIC_" & lPrmRen))) & ","  ' 4�F�H���R�[�h
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("KOUTEIJ_" & lPrmRen))) & ","  ' 5�F�H������
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("TEIIN_" & lPrmRen))) & ","    ' 6�F���
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("DANDORI_" & lPrmRen))) & ","  ' 7�F�i��
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("DKIJYUN_" & lPrmRen))) & ","  ' 8�F��o����
        SQL = SQL & N & "NULL,"                                                         ' 9�F����ذٗ]���E����
        SQL = SQL & N & "NULL,"                                                         '10�F����ذٗ]���E׽�
        SQL = SQL & N & "NULL,"                                                         '11�F׽�ذٗ]���E����
        SQL = SQL & N & "NULL,"                                                         '12�F׽�ذٗ]���E׽�
        SQL = SQL & N & "NULL,"                                                         '13�F�ő努�撷
        SQL = SQL & N & "NULL"                                                          '14�F�v�Z����
        SQL = SQL & N & ")"
        _db.executeDB(SQL)

    End Function

    'SQL������ҏW(�����p)
    Private Function impDtForStr(ByVal prmVal As String) As String
        If "".Equals(prmVal) Then
            Return "NULL"
        Else
            Return "'" & prmVal & "'"
        End If
    End Function

    'SQL������ҏW(���l�p)
    Private Function impDtForNum(ByVal prmVal As String) As String
        If "".Equals(prmVal) Then
            Return "NULL"
        Else
            Return "" & prmVal & ""
        End If
    End Function

End Class