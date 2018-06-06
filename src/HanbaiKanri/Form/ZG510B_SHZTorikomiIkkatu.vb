'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���j���Y�̔��݌Ɏ捞�E�ꊇ�Z�o
'    �i�t�H�[��ID�jZG510B_SHZTorikomiIkkatu
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ����        2010/11/09                 �V�K              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Public Class ZG510B_SHZTorikomiIkkatu
    Inherits System.Windows.Forms.Form

    '-------------------------------------------------------------------------------
    '   �萔��`
    '-------------------------------------------------------------------------------
    Private Const RS As String = "RecSet"                       '�f�[�^�Z�b�g�e�[�u����
    Private Const N As String = ControlChars.NewLine            '���s����
    Private Const PGID As String = "ZG510B"

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
    Private Sub ZG510B_SHZTorikomiIkkatu_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
                lblZenkaiJikkouDate.Text = ZC110M_Menu.NON_EXECUTE
                lblZenkaiKensu.Text = ""
                lblZSeisanryo.Text = ""
                lblZHanbai.Text = ""
                lblZZaiko.Text = ""
            Else
                '��������
                lblZenkaiJikkouDate.Text = _db.rmNullDate(ds.Tables(RS).Rows(0)("SDATEEND"))
                lblZenkaiKensu.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU4")), "#,##0")
                lblZSeisanryo.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU1")), "#,##0")
                lblZHanbai.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU2")), "#,##0")
                lblZZaiko.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU3")), "#,##0")
            End If

            '������s���̕\��
            lblKSeisanryo.Text = Format(_db.rmNullInt(_db.selectDB("select count(*) CNT from T21SEISANM ", RS).Tables(RS).Rows(0)("CNT")), "#,##0")
            lblKHanbai.Text = Format(_db.rmNullInt(_db.selectDB("select count(*) CNT from T13HANBAI ", RS).Tables(RS).Rows(0)("CNT")), "#,##0")
            lblKZaiko.Text = Format(_db.rmNullInt(_db.selectDB("select count(*) CNT from T31ZAIKOJ ", RS).Tables(RS).Rows(0)("CNT")), "#,##0")

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
                    pb.jobName = "�捞���������s���Ă��܂��B"
                    pb.oneStep = 1

                    _db.beginTran()
                    Try
                        pb.status = "�捞������" : pb.maxVal = 1
                        _db.executeDB("delete from T41SEISANK")

                        pb.status = "���Y�E�̔��E�݌Ƀf�[�^�擾��"
                        Call insertBaseRecord()                                                    '1-1 ���Y�̔��݌Ɏ捞


                        pb.status = "�X�e�[�^�X�ύX���E�E�E"
                        ed = Now                    '�����I������
                        _parentForm.updateSeigyoTbl(PGID, True, st, ed)                             '2-0 ���Y�ʊm��/����

                        pb.status = "���s�����쐬"
                        insertRireki(st, ed)                                  '2-1 ���s�����i�[
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
    Private Sub insertRireki(ByVal prmStDt As Date, ByVal prmEdDt As Date)

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
        sql = sql & N & ",KENNSU3 "    'T31���s����
        sql = sql & N & ",KENNSU4 "    'T41���s����
        sql = sql & N & ",UPDNAME "    '�ŏI�X�V��
        sql = sql & N & ",UPDDATE "    '�ŏI�X�V��
        sql = sql & N & ")values( "
        sql = sql & N & "  '" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "' "                             '�����N��
        sql = sql & N & ", '" & _db.rmSQ(lblKeikakuDate.Text.Replace("/", "")) & "' "                           '�v��N��
        sql = sql & N & ", '" & _db.rmSQ(PGID) & "' "                                                           '�@�\ID
        sql = sql & N & ",to_date('" & Format(prmStDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '�����J�n����
        sql = sql & N & ",to_date('" & Format(prmEdDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '�����I������
        sql = sql & N & ", " & lblKSeisanryo.Text & " "                                                         'T21���s����
        sql = sql & N & ", " & lblKHanbai.Text & " "                                                            'T13���s����
        sql = sql & N & ", " & lblKZaiko.Text & " "                                                             'T31���s����
        sql = sql & N & ", " & _db.rmNullInt(_db.selectDB("select count(*) CNT from T41SEISANK ", RS).Tables(RS).Rows(0)("CNT")) & " "                        '���s����
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "                                     '�ŏI�X�V��
        sql = sql & N & ",sysdate "                                                                             '�ŏI�X�V��
        sql = sql & N & ") "
        _db.executeDB(sql)

    End Sub

    '-------------------------------------------------------------------------------
    '   ���Y�̔��݌Ɏ捞
    '   �i�����T�v�jT21���Y�����AT13�̔��v��AT31�݌Ɏ��т���T41���Y�v��փf�[�^��������
    '   �����̓p�����^  �F�Ȃ�
    '   ���o�̓p�����^  �F�Ȃ�
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub insertBaseRecord()

        Dim sql As String = ""
        '�ʏ�ڕt��
        sql = sql & N & "INSERT INTO T41SEISANK "
        sql = sql & N & "( "
        sql = sql & N & " KHINMEICD        "  '�v��i���R�[�h 
        sql = sql & N & ",ZZZAIKOSU        "  '�O�X�����݌ɐ� 
        sql = sql & N & ",ZZZAIKORYOU      "  '�O�X�����݌ɗ� 
        sql = sql & N & ",ZSEISANSU        "  '�O�����Y���ѐ� 
        sql = sql & N & ",ZSEISANRYOU      "  '�O�����Y���ї� 
        sql = sql & N & ",ZHANBAISU        "  '�O���̔����ѐ� 
        sql = sql & N & ",ZHANBAIRYOU      "  '�O���̔����ї� 
        sql = sql & N & ",ZZAIKOSU         "  '�O�����݌ɐ� 
        sql = sql & N & ",ZZAIKORYOU       "  '�O�����݌ɗ� 
        sql = sql & N & ",TSEISANSU        "  '�������Y�v�搔 
        sql = sql & N & ",TSEISANRYOU      "  '�������Y�v��� 
        sql = sql & N & ",THANBAISU        "  '�����̔��v�搔 
        sql = sql & N & ",THANBAIRYOU      "  '�����̔��v��� 
        sql = sql & N & ",TZAIKOSU         "  '�������݌ɐ� 
        sql = sql & N & ",TZAIKORYOU       "  '�������݌ɗ� 
        sql = sql & N & ",KURIKOSISU       "  '�J�z�� 
        sql = sql & N & ",KURIKOSIRYOU     "  '�J�z�� 
        sql = sql & N & ",IKATULOTOSU      "  '�ꊇ�Z�o���b�g�� 
        sql = sql & N & ",LOTOSU           "  '���b�g�� 
        sql = sql & N & ",YSEISANSU        "  '�������Y�v�搔 
        sql = sql & N & ",YSEISANRYOU      "  '�������Y�v��� 
        sql = sql & N & ",YHANBAISU        "  '�����̔��v�搔 
        sql = sql & N & ",YHANBAIRYOU      "  '�����̔��v��� 
        sql = sql & N & ",YZAIKOSU         "  '�������݌ɐ� 
        sql = sql & N & ",YZAIKORYOU       "  '�������݌ɗ� 
        sql = sql & N & ",YZAIKOTUKISU     "  '�����݌Ɍ��� 
        sql = sql & N & ",YYHANBAISU       "  '���X���̔��v�搔 
        sql = sql & N & ",YYHANBAIRYOU     "  '���X���̔��v��� 
        sql = sql & N & ",KTUKISU          "  '����� 
        sql = sql & N & ",FZAIKOSU         "  '�����p�݌ɐ� 
        sql = sql & N & ",FZAIKORYOU       "  '�����p�݌ɗ� 
        sql = sql & N & ",AZAIKOSU         "  '���S�݌ɐ� 
        sql = sql & N & ",AZAIKORYOU       "  '���S�݌ɗ� 
        sql = sql & N & ",METSUKE          "  '�ڕt 
        sql = sql & N & ",UPDNAME          "  '�[��ID 
        sql = sql & N & ",UPDDATE          "  '�X�V���� 
        sql = sql & N & ") "

        ''sql = sql & N & "SELECT "
        ''sql = sql & N & " T13.KHINMEICD "                                                                                                                                                                                                                                 '�v��i���R�[�h 
        ''sql = sql & N & ",ROUND(NVL(T31.ZZZAIKOSU ,0) * 1000 ,1) "                                                                                                                                                                                                        '�O�X�����݌ɐ� 
        ''sql = sql & N & ",ROUND(NVL(T31.ZZZAIKORYOU,0) * 1000 ,1) "                                                                                                                                                                                                       '�O�X�����݌ɗ� 
        ''sql = sql & N & ",ROUND(NVL(T31.ZSEISANSU ,0) * 1000 ,1) "                                                                                                                                                                                                        '�O�����Y���ѐ� 
        ''sql = sql & N & ",ROUND(NVL(T31.ZSEISANRYOU,0) * 1000 ,1) "                                                                                                                                                                                                       '�O�����Y���ї� 
        ''sql = sql & N & ",ROUND(NVL(T31.ZHANBAISU ,0) * 1000 ,1) "                                                                                                                                                                                                        '�O���̔����ѐ� 
        ''sql = sql & N & ",ROUND(NVL(T31.ZHANBAIRYOU,0) * 1000 ,1) "                                                                                                                                                                                                       '�O���̔����ї� 
        ''sql = sql & N & ",ROUND(NVL(T31.ZZAIKOSU ,0) * 1000 ,1) "                                                                                                                                                                                                         '�O�����݌ɐ� 
        ''sql = sql & N & ",ROUND(NVL(T31.ZZAIKORYOU ,0) * 1000 ,1) "                                                                                                                                                                                                       '�O�����݌ɗ� 
        ''sql = sql & N & ",CASE WHEN T21.NENGETSU <= '" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "' THEN ROUND(NVL(T21.MYSMIKOMISU ,0) * 1000 ,1) ELSE NULL END "                                                                                                  '�������Y�v�搔 
        ''sql = sql & N & ",CASE WHEN T21.NENGETSU <= '" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "' THEN ROUND(NVL(T21.MYSMIKOMISU ,0) * 1000 * NVL(T10.MYMETSUKE,0) / 1000 ,1) ELSE NULL END "                                                                    '�������Y�v��� 
        ''sql = sql & N & ",ROUND(NVL(T13.THANBAIRYOUHK,0) * 1000 / T10.MYMETSUKE ,1) "                                                                                                                                                                                     '�����̔��v�搔 
        ''sql = sql & N & ",ROUND(NVL(T13.THANBAIRYOUHK,0) ,1) "                                                                                                                                                                                                            '�����̔��v��� 
        ''sql = sql & N & ",ROUND((NVL(T31.ZZAIKOSU  ,0) * 1000) + ( NVL(T21.MYSMIKOMISU,0) * 1000                               ) - ((T13.THANBAIRYOUHK * 1000) / T10.MYMETSUKE) ,1) "                                                                                     '�������݌ɐ� 
        ''sql = sql & N & ",ROUND((NVL(T31.ZZAIKORYOU,0) * 1000) + ((NVL(T21.MYSMIKOMISU,0) * 1000 * NVL(T10.MYMETSUKE,0)) / 1000) -   T13.THANBAIRYOUHK                          ,1) "                                                                                     '�������݌ɗ� 
        ''sql = sql & N & ",CASE WHEN T21.NENGETSU > '" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "' THEN ROUND(NVL(T21.MYSMIKOMISU ,0) * 1000,1) ELSE NULL END "                                                                                                    '�J�z�� 
        ''sql = sql & N & ",CASE WHEN T21.NENGETSU > '" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "' THEN ROUND((NVL(T21.MYSMIKOMISU ,0) * 1000 * NVL(T10.MYMETSUKE,0)) / 1000,1) ELSE NULL END "                                                                    '�J�z�� 
        ''sql = sql & N & ",0 "                                                                                                                                                                                                                                             '�ꊇ�Z�o���b�g�� 
        ''sql = sql & N & ",0 "                                                                                                                                                                                                                                             '���b�g�� 
        ''sql = sql & N & ",ROUND(NVL(T21.MYSMIKOMISU,0) * 1000,1) "                                                                                                                                                                                                        '�������Y�v�搔 
        ''sql = sql & N & ",ROUND((NVL(T21.MYSMIKOMISU,0) * 1000 * NVL(T10.MYMETSUKE,0)) / 1000,1) "                                                                                                                                                                        '�������Y�v��� 
        ''sql = sql & N & ",ROUND((T13.YHANBAIRYOUHK * 1000) / T10.MYMETSUKE,1) "                                                                                                                                                                                           '�����̔��v�搔 
        ''sql = sql & N & ",ROUND(T13.YHANBAIRYOUHK,1) "                                                                                                                                                                                                                    '�����̔��v��� 
        ''sql = sql & N & ",ROUND(((NVL(T31.ZZAIKOSU,0) * 1000) + (NVL(T21.MYSMIKOMISU,0) * 1000) - ((T13.THANBAIRYOUHK * 1000) / T10.MYMETSUKE)) + (NVL(T21.MYSMIKOMISU,0) * 1000) - ((T13.YHANBAIRYOUHK * 1000) / T10.MYMETSUKE),1) "                                     '�������݌ɐ� 
        ''sql = sql & N & ",ROUND(((NVL(T31.ZZAIKORYOU,0) * 1000) + ((NVL(T21.MYSMIKOMISU,0) * 1000 * NVL(T10.MYMETSUKE,0)) / 1000) - T13.THANBAIRYOUHK) + ((NVL(T21.MYSMIKOMISU,0) * 1000 * NVL(T10.MYMETSUKE,0)) / 1000) - T13.YHANBAIRYOUHK,1) "                         '�������݌ɗ� 
        ''sql = sql & N & ",ROUND((((NVL(T31.ZZAIKORYOU,0) * 1000) + ((NVL(T21.MYSMIKOMISU,0) * 1000 * NVL(T10.MYMETSUKE,0)) / 1000) - T13.THANBAIRYOUHK) + ((NVL(T21.MYSMIKOMISU,0) * 1000 * NVL(T10.MYMETSUKE,0)) / 1000) - T13.YHANBAIRYOUHK) / T13.YYHANBAIRYOUHK,1) "  '�����݌Ɍ��� 
        ''sql = sql & N & ",ROUND((T13.YYHANBAIRYOUHK * 1000) / T10.MYMETSUKE,1) "                                                                                                                                                                                          '���X���̔��v�搔 
        ''sql = sql & N & ",ROUND(T13.YYHANBAIRYOUHK ,1) "                                                                                                                                                                                                                  '���X���̔��v��� 
        ''sql = sql & N & ",ROUND(NVL(M11.TT_KZAIKOTUKISU,0),1) "                                                                                                                                                                                                           '����� 
        ''sql = sql & N & ",ROUND(NVL(M11.TT_SFUKKYUU,0) * 1000,1) "                                                                                                                                                                                                        '�����p�݌ɐ� 
        ''sql = sql & N & ",ROUND(NVL(M11.TT_SFUKKYUU,0) * NVL(T10.MYMETSUKE,0),1) "                                                                                                                                                                                        '�����p�݌ɗ� 
        ''sql = sql & N & ",ROUND(NVL(M11.TT_ANNZENZAIKO,0) * 1000,1) "                                                                                                                                                                                                     '���S�݌ɐ� 
        ''sql = sql & N & ",ROUND(NVL(M11.TT_ANNZENZAIKO,0) * NVL(T10.MYMETSUKE,0),1) "                                                                                                                                                                                     '���S�݌ɗ� 
        ''sql = sql & N & ",ROUND(NVL(T10.MYMETSUKE,0),3) "                                                                                                                                                                                                                 '�ڕt 
        ''sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "                                                                                                                                                                                               '�[��ID 
        ''sql = sql & N & ",SYSDATE "                                                                                                                                                                                                                                       '�X�V���� 
        ''sql = sql & N & "FROM T13HANBAI             T13 "
        ''sql = sql & N & "LEFT JOIN ( "
        ''sql = sql & N & "	SELECT "
        ''sql = sql & N & "	 KHINMEICD,NENGETSU "
        ''sql = sql & N & "	,MAX(SMIKOMISU) MYSMIKOMISU "
        ''sql = sql & N & "	FROM T21SEISANM "
        ''sql = sql & N & "   WHERE TAISYO_FLG = '1' "
        ''sql = sql & N & "	GROUP BY KHINMEICD,NENGETSU "
        ''sql = sql & N & "	)                       T21 ON T13.KHINMEICD = T21.KHINMEICD "

        '-->2010.12.02 chg by takagi
        'sql = sql & N & "SELECT "
        'sql = sql & N & " T13.KHINMEICD "                                                                                                                                                                                                                                 '�v��i���R�[�h 
        'sql = sql & N & ",ROUND(NVL(T31.ZZZAIKOSU ,0) / 1000 ,1) "                                                                                                                                                                                                        '�O�X�����݌ɐ� 
        'sql = sql & N & ",ROUND(NVL(T31.ZZZAIKORYOU,0) / 1000 ,1) "                                                                                                                                                                                                       '�O�X�����݌ɗ� 
        'sql = sql & N & ",ROUND(NVL(T31.ZSEISANSU ,0) / 1000 ,1) "                                                                                                                                                                                                        '�O�����Y���ѐ� 
        'sql = sql & N & ",ROUND(NVL(T31.ZSEISANRYOU,0) / 1000 ,1) "                                                                                                                                                                                                       '�O�����Y���ї� 
        'sql = sql & N & ",ROUND(NVL(T31.ZHANBAISU ,0) / 1000 ,1) "                                                                                                                                                                                                        '�O���̔����ѐ� 
        'sql = sql & N & ",ROUND(NVL(T31.ZHANBAIRYOU,0) / 1000 ,1) "                                                                                                                                                                                                       '�O���̔����ї� 
        'sql = sql & N & ",ROUND(NVL(T31.ZZAIKOSU ,0) / 1000 ,1) "                                                                                                                                                                                                         '�O�����݌ɐ� 
        'sql = sql & N & ",ROUND(NVL(T31.ZZAIKORYOU ,0) / 1000 ,1) "                                                                                                                                                                                                       '�O�����݌ɗ� 
        'sql = sql & N & ",ROUND(NVL(T21L.MYSMIKOMISU ,0) / 1000 ,1) "                                                                                                  '�������Y�v�搔 
        'sql = sql & N & ",ROUND(NVL(T21L.MYSMIKOMISU ,0) / 1000 * NVL(T10.MYMETSUKE,0) / 1000 ,1) "                                                                    '�������Y�v��� 
        'sql = sql & N & ",ROUND(NVL(T13.THANBAIRYOUHK,0) * 1000 / T10.MYMETSUKE ,1) "                                                                                                                                                                                     '�����̔��v�搔 
        'sql = sql & N & ",ROUND(NVL(T13.THANBAIRYOUHK,0) ,1) "                                                                                                                                                                                                            '�����̔��v��� 
        'sql = sql & N & ",ROUND((NVL(T31.ZZAIKOSU  ,0) / 1000) + ( NVL(T21L.MYSMIKOMISU,0) / 1000                               ) - ((T13.THANBAIRYOUHK * 1000) / T10.MYMETSUKE) ,1) "                                                                                     '�������݌ɐ� 
        'sql = sql & N & ",ROUND((NVL(T31.ZZAIKORYOU,0) / 1000) + ((NVL(T21L.MYSMIKOMISU,0) / 1000 * NVL(T10.MYMETSUKE,0)) / 1000) -   T13.THANBAIRYOUHK                          ,1) "                                                                                     '�������݌ɗ� 
        'sql = sql & N & ",ROUND(NVL(T21U.MYSMIKOMISU ,0) / 1000,1) "                                                                                                    '�J�z�� 
        'sql = sql & N & ",ROUND((NVL(T21U.MYSMIKOMISU ,0) / 1000 * NVL(T10.MYMETSUKE,0)) / 1000,1) "                                                                    '�J�z�� 
        'sql = sql & N & ",0 "                                                                                                                                                                                                                                             '�ꊇ�Z�o���b�g�� 
        'sql = sql & N & ",0 "                                                                                                                                                                                                                                             '���b�g�� 
        'sql = sql & N & ",ROUND(NVL(T21U.MYSMIKOMISU,0) / 1000,1) "                                                                                                                                                                                                        '�������Y�v�搔 
        'sql = sql & N & ",ROUND((NVL(T21U.MYSMIKOMISU,0) / 1000 * NVL(T10.MYMETSUKE,0)) / 1000,1) "                                                                                                                                                                        '�������Y�v��� 
        'sql = sql & N & ",ROUND((T13.YHANBAIRYOUHK * 1000) / T10.MYMETSUKE,1) "                                                                                                                                                                                           '�����̔��v�搔 
        'sql = sql & N & ",ROUND(T13.YHANBAIRYOUHK,1) "                                                                                                                                                                                                                    '�����̔��v��� 
        'sql = sql & N & ",ROUND(((NVL(T31.ZZAIKOSU,0) / 1000) + (NVL(T21L.MYSMIKOMISU,0) / 1000) - ((T13.THANBAIRYOUHK * 1000) / T10.MYMETSUKE)) + (NVL(T21U.MYSMIKOMISU,0) / 1000) - ((T13.YHANBAIRYOUHK * 1000) / T10.MYMETSUKE),1) "                                     '�������݌ɐ� 
        'sql = sql & N & ",ROUND(((NVL(T31.ZZAIKORYOU,0) / 1000) + ((NVL(T21L.MYSMIKOMISU,0) / 1000 * NVL(T10.MYMETSUKE,0)) / 1000) - T13.THANBAIRYOUHK) + ((NVL(T21U.MYSMIKOMISU,0) / 1000 * NVL(T10.MYMETSUKE,0)) / 1000) - T13.YHANBAIRYOUHK,1) "                         '�������݌ɗ� 
        ''-->2010.12.02 upd by takagi
        ''sql = sql & N & ",ROUND((((NVL(T31.ZZAIKORYOU,0) / 1000) + ((NVL(T21L.MYSMIKOMISU,0) / 1000 * NVL(T10.MYMETSUKE,0)) / 1000) - T13.THANBAIRYOUHK) + ((NVL(T21U.MYSMIKOMISU,0) / 1000 * NVL(T10.MYMETSUKE,0)) / 1000) - T13.YHANBAIRYOUHK) / T13.YYHANBAIRYOUHK,1) "  '�����݌Ɍ��� 
        'sql = sql & N & ",DECODE(T13.YYHANBAIRYOUHK,0,NULL,ROUND((((NVL(T31.ZZAIKORYOU,0) / 1000) + ((NVL(T21L.MYSMIKOMISU,0) / 1000 * NVL(T10.MYMETSUKE,0)) / 1000) - T13.THANBAIRYOUHK) + ((NVL(T21U.MYSMIKOMISU,0) / 1000 * NVL(T10.MYMETSUKE,0)) / 1000) - T13.YHANBAIRYOUHK) / T13.YYHANBAIRYOUHK,1)) "  '�����݌Ɍ��� 
        ''<--2010.12.02 upd by takagi
        'sql = sql & N & ",ROUND((T13.YYHANBAIRYOUHK * 1000) / T10.MYMETSUKE,1) "                                                                                                                                                                                          '���X���̔��v�搔 
        'sql = sql & N & ",ROUND(T13.YYHANBAIRYOUHK ,1) "                                                                                                                                                                                                                  '���X���̔��v��� 
        'sql = sql & N & ",ROUND(NVL(M11.TT_KZAIKOTUKISU,0),1) "                                                                                                                                                                                                           '����� 
        'sql = sql & N & ",ROUND(NVL(M11.TT_SFUKKYUU,0) / 1000,1) "                                                                                                                                                                                                        '�����p�݌ɐ� 
        'sql = sql & N & ",ROUND((NVL(M11.TT_SFUKKYUU,0) / 1000) * NVL(T10.MYMETSUKE,0) / 1000 ,1) "                                                                                                                                                                       '�����p�݌ɗ� 
        'sql = sql & N & ",ROUND(NVL(M11.TT_ANNZENZAIKO,0) / 1000,1) "                                                                                                                                                                                                     '���S�݌ɐ� 
        'sql = sql & N & ",ROUND((NVL(M11.TT_ANNZENZAIKO,0) / 1000) * NVL(T10.MYMETSUKE,0) / 1000 ,1) "                                                                                                                                                                    '���S�݌ɗ� 
        'sql = sql & N & ",ROUND(NVL(T10.MYMETSUKE,0),3) "                                                                                                                                                                                                                 '�ڕt 
        'sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "                                                                                                                                                                                               '�[��ID 
        'sql = sql & N & ",SYSDATE "                                                                                                                                                                                                                                       '�X�V���� 
        sql = sql & N & "SELECT "
        sql = sql & N & " T13.KHINMEICD "                                                                   '�v��i���R�[�h 
        sql = sql & N & ",ROUND(NVL(T31.ZZZAIKOSU    ,0) / 1000 ,1) "                                       '�O�X�����݌ɐ� 
        sql = sql & N & ",ROUND(NVL(T31.ZZZAIKORYOU  ,0) / 1000 ,1) "                                       '�O�X�����݌ɗ� 
        sql = sql & N & ",ROUND(NVL(T31.ZSEISANSU    ,0) / 1000 ,1) "                                       '�O�����Y���ѐ� 
        sql = sql & N & ",ROUND(NVL(T31.ZSEISANRYOU  ,0) / 1000 ,1) "                                       '�O�����Y���ї� 
        sql = sql & N & ",ROUND(NVL(T31.ZHANBAISU    ,0) / 1000 ,1) "                                       '�O���̔����ѐ� 
        sql = sql & N & ",ROUND(NVL(T31.ZHANBAIRYOU  ,0) / 1000 ,1) "                                       '�O���̔����ї� 
        sql = sql & N & ",ROUND(NVL(T31.ZZAIKOSU     ,0) / 1000 ,1) "                                       '�O�����݌ɐ� 
        sql = sql & N & ",ROUND(NVL(T31.ZZAIKORYOU   ,0) / 1000 ,1) "                                       '�O�����݌ɗ� 
        '-->2011.01.16 chg by takagi #82
        'sql = sql & N & ",ROUND(NVL(T21L.MYSMIKOMISU ,0) / 1000 ,1) "                                       '�������Y�v�搔 
        'sql = sql & N & ",ROUND(NVL(T21L.MYSMIKOMISU ,0) / 1000 * NVL(T13.METSUKE,0) / 1000 ,1) "           '�������Y�v��� 
        'sql = sql & N & ",ROUND(NVL(T13.THANBAIRYOUHK,0) * 1000 / T13.METSUKE ,1) "                         '�����̔��v�搔 
        'sql = sql & N & ",ROUND(NVL(T13.THANBAIRYOUHK,0) ,1) "                                              '�����̔��v��� 
        'sql = sql & N & ",ROUND(   ( NVL(T31.ZZAIKOSU    ,0)   / 1000        ) "
        'sql = sql & N & "        + ( NVL(T21L.MYSMIKOMISU,0)   / 1000        ) "
        'sql = sql & N & "        - ((T13.THANBAIRYOUHK * 1000) / T13.METSUKE )  , 1 ) "                     '�������݌ɐ� 
        'sql = sql & N & ",ROUND(   ( NVL(T31.ZZAIKORYOU  ,0) / 1000                             ) "
        'sql = sql & N & "        + ((NVL(T21L.MYSMIKOMISU,0) / 1000 * NVL(T13.METSUKE,0)) / 1000) "
        'sql = sql & N & "        - ( T13.THANBAIRYOUHK                                          )  ,1) "    '�������݌ɗ� 
        sql = sql & N & ",ROUND(NVL(T21L.MYSMIKOMISU ,0) / 1000 ,4) "                                       '�������Y�v�搔 
        sql = sql & N & ",ROUND(NVL(T21L.MYSMIKOMISU ,0) / 1000 * NVL(T13.METSUKE,0) / 1000 ,4) "           '�������Y�v��� 
        sql = sql & N & ",ROUND(NVL(T13.THANBAIRYOUHK,0) * 1000 / T13.METSUKE ,4) "                         '�����̔��v�搔 
        sql = sql & N & ",ROUND(NVL(T13.THANBAIRYOUHK,0) ,4) "                                              '�����̔��v��� 
        sql = sql & N & ",ROUND(   ( NVL(T31.ZZAIKOSU    ,0)   / 1000        ) "
        sql = sql & N & "        + ( NVL(T21L.MYSMIKOMISU,0)   / 1000        ) "
        sql = sql & N & "        - ((T13.THANBAIRYOUHK * 1000) / T13.METSUKE )  , 4 ) "                     '�������݌ɐ� 
        sql = sql & N & ",ROUND(   ( NVL(T31.ZZAIKORYOU  ,0) / 1000                             ) "
        sql = sql & N & "        + ((NVL(T21L.MYSMIKOMISU,0) / 1000 * NVL(T13.METSUKE,0)) / 1000) "
        sql = sql & N & "        - ( T13.THANBAIRYOUHK                                          )  ,4) "    '�������݌ɗ� 
        '<--2011.01.16 chg by takagi #82
        sql = sql & N & ",ROUND(NVL(T21U.MYSMIKOMISU ,0) / 1000,1) "                                        '�J�z�� 
        sql = sql & N & ",ROUND((NVL(T21U.MYSMIKOMISU ,0) / 1000 * NVL(T13.METSUKE,0)) / 1000,1) "          '�J�z�� 
        sql = sql & N & ",0 "                                                                               '�ꊇ�Z�o���b�g�� 
        sql = sql & N & ",0 "                                                                               '���b�g�� 
        '-->2011.01.16 chg by takagi #82
        'sql = sql & N & ",ROUND(NVL(T21U.MYSMIKOMISU,0) / 1000,1) "                                         '�������Y�v�搔 
        'sql = sql & N & ",ROUND((NVL(T21U.MYSMIKOMISU,0) / 1000 * NVL(T13.METSUKE,0)) / 1000,1) "           '�������Y�v��� 
        'sql = sql & N & ",ROUND((T13.YHANBAIRYOUHK * 1000) / T13.METSUKE,1) "                               '�����̔��v�搔 
        'sql = sql & N & ",ROUND(T13.YHANBAIRYOUHK,1) "                                                      '�����̔��v��� 
        'sql = sql & N & ",ROUND(    (  ( NVL(T31.ZZAIKOSU    ,0)    / 1000       ) "
        'sql = sql & N & "            + ( NVL(T21L.MYSMIKOMISU,0)    / 1000       ) "
        'sql = sql & N & "            - ( (T13.THANBAIRYOUHK * 1000) / T13.METSUKE) "
        'sql = sql & N & "           ) "
        'sql = sql & N & "         + ( NVL(T21U.MYSMIKOMISU,0   ) / 1000          ) "
        'sql = sql & N & "         - ( (T13.YHANBAIRYOUHK * 1000) / T13.METSUKE   )      ,1) "               '�������݌ɐ� 
        'sql = sql & N & ",ROUND(   (  (NVL(T31.ZZAIKORYOU  ,0) / 1000                            ) "
        'sql = sql & N & "           + (NVL(T21L.MYSMIKOMISU,0) / 1000 * NVL(T13.METSUKE,0) / 1000) "
        'sql = sql & N & "           -  T13.THANBAIRYOUHK "
        'sql = sql & N & "          ) "
        'sql = sql & N & "        + (NVL(T21U.MYSMIKOMISU,0) / 1000 * NVL(T13.METSUKE,0) / 1000) "
        'sql = sql & N & "        - T13.YHANBAIRYOUHK                                    ,1) "               '�������݌ɗ� 
        sql = sql & N & ",ROUND(NVL(T21U.MYSMIKOMISU,0) / 1000,4) "                                         '�������Y�v�搔 
        sql = sql & N & ",ROUND((NVL(T21U.MYSMIKOMISU,0) / 1000 * NVL(T13.METSUKE,0)) / 1000,4) "           '�������Y�v��� 
        sql = sql & N & ",ROUND((T13.YHANBAIRYOUHK * 1000) / T13.METSUKE,4) "                               '�����̔��v�搔 
        sql = sql & N & ",ROUND(T13.YHANBAIRYOUHK,4) "                                                      '�����̔��v��� 
        sql = sql & N & ",ROUND(    (  ( NVL(T31.ZZAIKOSU    ,0)    / 1000       ) "
        sql = sql & N & "            + ( NVL(T21L.MYSMIKOMISU,0)    / 1000       ) "
        sql = sql & N & "            - ( (T13.THANBAIRYOUHK * 1000) / T13.METSUKE) "
        sql = sql & N & "           ) "
        sql = sql & N & "         + ( NVL(T21U.MYSMIKOMISU,0   ) / 1000          ) "
        sql = sql & N & "         - ( (T13.YHANBAIRYOUHK * 1000) / T13.METSUKE   )      ,4) "               '�������݌ɐ� 
        sql = sql & N & ",ROUND(   (  (NVL(T31.ZZAIKORYOU  ,0) / 1000                            ) "
        sql = sql & N & "           + (NVL(T21L.MYSMIKOMISU,0) / 1000 * NVL(T13.METSUKE,0) / 1000) "
        sql = sql & N & "           -  T13.THANBAIRYOUHK "
        sql = sql & N & "          ) "
        sql = sql & N & "        + (NVL(T21U.MYSMIKOMISU,0) / 1000 * NVL(T13.METSUKE,0) / 1000) "
        sql = sql & N & "        - T13.YHANBAIRYOUHK                                    ,4) "               '�������݌ɗ� 
        '<--2011.01.16 chg by takagi #82

        '-->2011.01.18 chg by takagi #82
        ''sql = sql & N & ",DECODE( T13.YYHANBAIRYOUHK ,0 ,NULL "
        ''sql = sql & N & "        ,ROUND( (  (  (NVL(T31.ZZAIKORYOU  ,0) / 1000                            ) "
        ''sql = sql & N & "                    + (NVL(T21L.MYSMIKOMISU,0) / 1000 * NVL(T13.METSUKE,0) / 1000) "
        ''sql = sql & N & "                    - T13.THANBAIRYOUHK "
        ''sql = sql & N & "                   ) "
        ''sql = sql & N & "                 + (  (NVL(T21U.MYSMIKOMISU,0) / 1000 * NVL(T13.METSUKE,0)       ) / 1000) "
        ''sql = sql & N & "                 - T13.YHANBAIRYOUHK) / T13.YYHANBAIRYOUHK     ,1)) "              '�����݌Ɍ��� 
        sql = sql & N & ",DECODE(T13.YYHANBAIRYOUHK, 0, NULL, ROUND( "
        sql = sql & N & "     (   (  (NVL(T31.ZZAIKORYOU  ,0) / 1000                            ) "
        sql = sql & N & "               + (NVL(T21L.MYSMIKOMISU,0) / 1000 * NVL(T13.METSUKE,0) / 1000) "
        sql = sql & N & "               -  T13.THANBAIRYOUHK "
        sql = sql & N & "              ) "
        sql = sql & N & "            + (NVL(T21U.MYSMIKOMISU,0) / 1000 * NVL(T13.METSUKE,0) / 1000) "
        sql = sql & N & "            - T13.YHANBAIRYOUHK                                    ) "             '�������݌ɗ� 
        sql = sql & N & "     / "
        sql = sql & N & "     (T13.YYHANBAIRYOUHK ) "                                                       '���X���̔��v��� 
        sql = sql & N & "     ,4)) "
        '<--2011.01.18 chg by takagi #82

        '-->2011.01.16 chg by takagi #82
        'sql = sql & N & ",ROUND((T13.YYHANBAIRYOUHK * 1000) / T13.METSUKE,1) "                             '���X���̔��v�搔 
        'sql = sql & N & ",ROUND(T13.YYHANBAIRYOUHK ,1) "                                                   '���X���̔��v��� 
        sql = sql & N & ",ROUND((T13.YYHANBAIRYOUHK * 1000) / T13.METSUKE,4) "                              '���X���̔��v�搔 
        sql = sql & N & ",ROUND(T13.YYHANBAIRYOUHK ,4) "                                                    '���X���̔��v��� 
        '<--2011.01.16 chg by takagi #82
        sql = sql & N & ",ROUND(NVL(M11.TT_KZAIKOTUKISU,0),1) "                                             '����� 
        sql = sql & N & ",ROUND(NVL(M11.TT_SFUKKYUU,0) / 1000,1) "                                          '�����p�݌ɐ� 
        sql = sql & N & ",ROUND((NVL(M11.TT_SFUKKYUU,0) / 1000) * NVL(T13.METSUKE,0) / 1000 ,1) "           '�����p�݌ɗ� 
        sql = sql & N & ",ROUND(NVL(M11.TT_ANNZENZAIKO,0) / 1000,1) "                                       '���S�݌ɐ� 
        sql = sql & N & ",ROUND((NVL(M11.TT_ANNZENZAIKO,0) / 1000) * NVL(T13.METSUKE,0) / 1000 ,1) "        '���S�݌ɗ� 
        sql = sql & N & ",ROUND(NVL(T13.METSUKE,0),3) "                                                     '�ڕt 
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "                                 '�[��ID 
        sql = sql & N & ",SYSDATE "                                                                         '�X�V���� 
        '<--2010.12.02 chg by takagi
        sql = sql & N & "FROM T13HANBAI             T13 "
        sql = sql & N & "LEFT JOIN ( "
        sql = sql & N & "	SELECT "
        sql = sql & N & "	 KHINMEICD "
        sql = sql & N & "	,SUM(SMIKOMISU) MYSMIKOMISU "
        sql = sql & N & "	FROM T21SEISANM "
        sql = sql & N & "   WHERE TAISYO_FLG = '1' "
        sql = sql & N & "    AND  NENGETSU <= '" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "'"
        sql = sql & N & "	GROUP BY KHINMEICD "
        sql = sql & N & "	)                       T21L ON T13.KHINMEICD = T21L.KHINMEICD "
        sql = sql & N & "LEFT JOIN ( "
        sql = sql & N & "	SELECT "
        sql = sql & N & "	 KHINMEICD "
        sql = sql & N & "	,SUM(SMIKOMISU) MYSMIKOMISU "
        sql = sql & N & "	FROM T21SEISANM "
        sql = sql & N & "   WHERE TAISYO_FLG = '1' "
        sql = sql & N & "    AND  NENGETSU > '" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "'"
        sql = sql & N & "	GROUP BY KHINMEICD "
        sql = sql & N & "	)                       T21U ON T13.KHINMEICD = T21U.KHINMEICD "

        sql = sql & N & "LEFT JOIN T31ZAIKOJ        T31 ON T13.KHINMEICD = T31.KHINMEICD "
        sql = sql & N & "LEFT JOIN M11KEIKAKUHIN    M11 ON T13.KHINMEICD = M11.TT_KHINMEICD "
        '-->2010.12.12 chg by takagi
        ''-->2010.12.02 upd by takagi
        ''sql = sql & N & "LEFT JOIN ( "
        'sql = sql & N & "INNER JOIN ( "
        ''<--2010.12.02 upd by takagi
        'sql = sql & N & "	SELECT "
        'sql = sql & N & "	 M.KHINMEICD "
        'sql = sql & N & "	,MIN(T.METSUKE) MYMETSUKE "
        'sql = sql & N & "	FROM T10HINHANJ       T "
        'sql = sql & N & "	INNER JOIN M12SYUYAKU M "
        'sql = sql & N & "	ON T.HINMEICD = M.HINMEICD "
        ''-->2010.12.02 add by takagi
        'sql = sql & N & "	WHERE T.METSUKE IS NOT NULL "
        ''<--2010.12.02 add by takagi
        'sql = sql & N & "	GROUP BY M.KHINMEICD ) T10 ON T13.KHINMEICD = T10.KHINMEICD "
        ''-->2010.12.02 add by takagi
        'sql = sql & N & "	                           AND T10.MYMETSUKE != 0 "
        ''<--2010.12.02 add by takagi
        sql = sql & N & "WHERE T13.METSUKE IS NOT NULL AND T13.METSUKE != 0 "
        '<--2010.12.12 chg by takagi
        _db.executeDB(sql)

        '-->2010.12.12 add by takagi
        '�ڕt�O��
        sql = ""
        sql = sql & N & "INSERT INTO T41SEISANK "
        sql = sql & N & "( "
        sql = sql & N & " KHINMEICD        "  '�v��i���R�[�h 
        sql = sql & N & ",ZZZAIKOSU        "  '�O�X�����݌ɐ� 
        sql = sql & N & ",ZZZAIKORYOU      "  '�O�X�����݌ɗ� 
        sql = sql & N & ",ZSEISANSU        "  '�O�����Y���ѐ� 
        sql = sql & N & ",ZSEISANRYOU      "  '�O�����Y���ї� 
        sql = sql & N & ",ZHANBAISU        "  '�O���̔����ѐ� 
        sql = sql & N & ",ZHANBAIRYOU      "  '�O���̔����ї� 
        sql = sql & N & ",ZZAIKOSU         "  '�O�����݌ɐ� 
        sql = sql & N & ",ZZAIKORYOU       "  '�O�����݌ɗ� 
        sql = sql & N & ",TSEISANSU        "  '�������Y�v�搔 
        sql = sql & N & ",TSEISANRYOU      "  '�������Y�v��� 
        sql = sql & N & ",THANBAISU        "  '�����̔��v�搔 
        sql = sql & N & ",THANBAIRYOU      "  '�����̔��v��� 
        sql = sql & N & ",TZAIKOSU         "  '�������݌ɐ� 
        sql = sql & N & ",TZAIKORYOU       "  '�������݌ɗ� 
        sql = sql & N & ",KURIKOSISU       "  '�J�z�� 
        sql = sql & N & ",KURIKOSIRYOU     "  '�J�z�� 
        sql = sql & N & ",IKATULOTOSU      "  '�ꊇ�Z�o���b�g�� 
        sql = sql & N & ",LOTOSU           "  '���b�g�� 
        sql = sql & N & ",YSEISANSU        "  '�������Y�v�搔 
        sql = sql & N & ",YSEISANRYOU      "  '�������Y�v��� 
        sql = sql & N & ",YHANBAISU        "  '�����̔��v�搔 
        sql = sql & N & ",YHANBAIRYOU      "  '�����̔��v��� 
        sql = sql & N & ",YZAIKOSU         "  '�������݌ɐ� 
        sql = sql & N & ",YZAIKORYOU       "  '�������݌ɗ� 
        sql = sql & N & ",YZAIKOTUKISU     "  '�����݌Ɍ��� 
        sql = sql & N & ",YYHANBAISU       "  '���X���̔��v�搔 
        sql = sql & N & ",YYHANBAIRYOU     "  '���X���̔��v��� 
        sql = sql & N & ",KTUKISU          "  '����� 
        sql = sql & N & ",FZAIKOSU         "  '�����p�݌ɐ� 
        sql = sql & N & ",FZAIKORYOU       "  '�����p�݌ɗ� 
        sql = sql & N & ",AZAIKOSU         "  '���S�݌ɐ� 
        sql = sql & N & ",AZAIKORYOU       "  '���S�݌ɗ� 
        sql = sql & N & ",METSUKE          "  '�ڕt 
        sql = sql & N & ",UPDNAME          "  '�[��ID 
        sql = sql & N & ",UPDDATE          "  '�X�V���� 
        sql = sql & N & ") "
        sql = sql & N & "SELECT "
        sql = sql & N & " T13.KHINMEICD "                                                                   '�v��i���R�[�h 
        sql = sql & N & ",ROUND(NVL(T31.ZZZAIKOSU ,0) / 1000 ,1) "                                          '�O�X�����݌ɐ� 
        sql = sql & N & ",ROUND(NVL(T31.ZZZAIKORYOU,0) / 1000 ,1) "                                         '�O�X�����݌ɗ� 
        sql = sql & N & ",ROUND(NVL(T31.ZSEISANSU ,0) / 1000 ,1) "                                          '�O�����Y���ѐ� 
        sql = sql & N & ",ROUND(NVL(T31.ZSEISANRYOU,0) / 1000 ,1) "                                         '�O�����Y���ї� 
        sql = sql & N & ",ROUND(NVL(T31.ZHANBAISU ,0) / 1000 ,1) "                                          '�O���̔����ѐ� 
        sql = sql & N & ",ROUND(NVL(T31.ZHANBAIRYOU,0) / 1000 ,1) "                                         '�O���̔����ї� 
        sql = sql & N & ",ROUND(NVL(T31.ZZAIKOSU ,0) / 1000 ,1) "                                           '�O�����݌ɐ� 
        sql = sql & N & ",ROUND(NVL(T31.ZZAIKORYOU ,0) / 1000 ,1) "                                         '�O�����݌ɗ� 
        '-->2011.01.16 chg by takagi #82
        'sql = sql & N & ",ROUND(NVL(T21L.MYSMIKOMISU ,0) / 1000 ,1) "                                       '�������Y�v�搔 
        'sql = sql & N & ",ROUND(NVL(T21L.MYSMIKOMISU ,0) / 1000 * NVL(T13.METSUKE,0) / 1000 ,1) "           '�������Y�v��� 
        'sql = sql & N & ",ROUND(NVL(T13.THANBAISUHK,0) ,1) "                                                '�����̔��v�搔 
        'sql = sql & N & ",0 "                                                                               '�����̔��v��� 
        'sql = sql & N & ",ROUND(  (NVL(T31.ZZAIKOSU    ,0) / 1000) "
        'sql = sql & N & "       + (NVL(T21L.MYSMIKOMISU,0) / 1000) "
        'sql = sql & N & "       - T13.THANBAISUHK                      ,1) "                                '�������݌ɐ� 
        'sql = sql & N & ",0 "                                                                               '�������݌ɗ� 
        sql = sql & N & ",ROUND(NVL(T21L.MYSMIKOMISU ,0) / 1000 ,4) "                                       '�������Y�v�搔 
        sql = sql & N & ",ROUND(NVL(T21L.MYSMIKOMISU ,0) / 1000 * NVL(T13.METSUKE,0) / 1000 ,4) "           '�������Y�v��� 
        sql = sql & N & ",ROUND(NVL(T13.THANBAISUHK,0) ,4) "                                                '�����̔��v�搔 
        sql = sql & N & ",0 "                                                                               '�����̔��v��� 
        sql = sql & N & ",ROUND(  (NVL(T31.ZZAIKOSU    ,0) / 1000) "
        sql = sql & N & "       + (NVL(T21L.MYSMIKOMISU,0) / 1000) "
        sql = sql & N & "       - T13.THANBAISUHK                      ,4) "                                '�������݌ɐ� 
        sql = sql & N & ",0 "                                                                               '�������݌ɗ� 
        '<--2011.01.16 chg by takagi #82
        sql = sql & N & ",ROUND(NVL(T21U.MYSMIKOMISU ,0) / 1000,1) "                                        '�J�z�� 
        sql = sql & N & ",0 "                                                                               '�J�z�� 
        sql = sql & N & ",0 "                                                                               '�ꊇ�Z�o���b�g�� 
        sql = sql & N & ",0 "                                                                               '���b�g�� 
        '-->2011.01.16 chg by takagi #82
        'sql = sql & N & ",ROUND(NVL(T21U.MYSMIKOMISU,0) / 1000,1) "                                         '�������Y�v�搔 
        'sql = sql & N & ",ROUND((NVL(T21U.MYSMIKOMISU,0) / 1000 * NVL(T13.METSUKE,0)) / 1000,1) "           '�������Y�v��� 
        'sql = sql & N & ",ROUND(T13.YHANBAISUHK,1) "                                                        '�����̔��v�搔 
        'sql = sql & N & ",0 "                                                                               '�����̔��v��� 
        'sql = sql & N & ",ROUND( ( (NVL(T31.ZZAIKOSU    ,0) / 1000) "
        'sql = sql & N & "         +(NVL(T21L.MYSMIKOMISU,0) / 1000) "
        'sql = sql & N & "         - T13.THANBAISUHK"
        'sql = sql & N & "         ) "
        'sql = sql & N & "       + (NVL(T21U.MYSMIKOMISU,0) / 1000) "
        'sql = sql & N & "       - T13.YHANBAISUHK                      ,1) "                                '�������݌ɐ� 
        'sql = sql & N & ",0 "                                                                               '�������݌ɗ� 
        sql = sql & N & ",ROUND(NVL(T21U.MYSMIKOMISU,0) / 1000,4) "                                         '�������Y�v�搔 
        sql = sql & N & ",ROUND((NVL(T21U.MYSMIKOMISU,0) / 1000 * NVL(T13.METSUKE,0)) / 1000,4) "           '�������Y�v��� 
        sql = sql & N & ",ROUND(T13.YHANBAISUHK,4) "                                                        '�����̔��v�搔 
        sql = sql & N & ",0 "                                                                               '�����̔��v��� 
        sql = sql & N & ",ROUND( ( (NVL(T31.ZZAIKOSU    ,0) / 1000) "
        sql = sql & N & "         +(NVL(T21L.MYSMIKOMISU,0) / 1000) "
        sql = sql & N & "         - T13.THANBAISUHK"
        sql = sql & N & "         ) "
        sql = sql & N & "       + (NVL(T21U.MYSMIKOMISU,0) / 1000) "
        sql = sql & N & "       - T13.YHANBAISUHK                      ,4) "                                '�������݌ɐ� 
        sql = sql & N & ",0 "                                                                               '�������݌ɗ� 
        '<--2011.01.16 chg by takagi #82

        '-->2011.01.18 chg by takagi #82
        ''sql = sql & N & ",DECODE(T13.YYHANBAISUHK ,0 ,NULL, "
        ''sql = sql & N & "        ROUND( ( ( (NVL(T31.ZZAIKOSU    ,0) / 1000) "
        ''sql = sql & N & "                 + (NVL(T21L.MYSMIKOMISU,0) / 1000) "
        ''sql = sql & N & "                 - T13.THANBAISUHK "
        ''sql = sql & N & "                 ) "
        ''sql = sql & N & "               + (NVL(T21U.MYSMIKOMISU  ,0) / 1000) "
        ''sql = sql & N & "               - T13.YHANBAISUHK "
        ''sql = sql & N & "               ) / T13.YYHANBAISUHK           ,1)) "                               '�����݌Ɍ��� 
        sql = sql & N & ",DECODE(T13.YYHANBAISUHK ,0 ,NULL , "
        sql = sql & N & "        ROUND( "
        sql = sql & N & "             ( ( (NVL(T31.ZZAIKOSU    ,0) / 1000) "
        sql = sql & N & "                +(NVL(T21L.MYSMIKOMISU,0) / 1000) "
        sql = sql & N & "                - T13.THANBAISUHK"
        sql = sql & N & "                ) "
        sql = sql & N & "              + (NVL(T21U.MYSMIKOMISU,0) / 1000) "
        sql = sql & N & "              - T13.YHANBAISUHK                        ) "                                '�������݌ɐ� 
        sql = sql & N & "             / "
        sql = sql & N & "             (T13.YYHANBAISUHK  ) "                                                       '���X���̔��v�搔 
        sql = sql & N & "                              ,4)) "
        ''<--2011.01.18 chg by takagi #82

        '-->2011.01.16 chg by takagi #82
        'sql = sql & N & ",ROUND(T13.YYHANBAISUHK,1) "                                                       '���X���̔��v�搔 
        'sql = sql & N & ",0 "                                                                               '���X���̔��v��� 
        sql = sql & N & ",ROUND(T13.YYHANBAISUHK,4) "                                                       '���X���̔��v�搔 
        sql = sql & N & ",0 "                                                                               '���X���̔��v��� 
        '<--2011.01.16 chg by takagi #82
        sql = sql & N & ",ROUND(NVL(M11.TT_KZAIKOTUKISU,0),1) "                                             '����� 
        sql = sql & N & ",ROUND(NVL(M11.TT_SFUKKYUU,0) / 1000,1) "                                          '�����p�݌ɐ� 
        sql = sql & N & ",ROUND((NVL(M11.TT_SFUKKYUU,0) / 1000) * NVL(T13.METSUKE,0) / 1000 ,1) "           '�����p�݌ɗ� 
        sql = sql & N & ",ROUND(NVL(M11.TT_ANNZENZAIKO,0) / 1000,1) "                                       '���S�݌ɐ� 
        sql = sql & N & ",ROUND((NVL(M11.TT_ANNZENZAIKO,0) / 1000) * NVL(T13.METSUKE,0) / 1000 ,1) "        '���S�݌ɗ� 
        sql = sql & N & ",ROUND(NVL(T13.METSUKE,0),3) "                                                     '�ڕt 
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "                                 '�[��ID 
        sql = sql & N & ",SYSDATE "                                                                         '�X�V���� 
        sql = sql & N & "FROM T13HANBAI             T13 "
        sql = sql & N & "LEFT JOIN ( "
        sql = sql & N & "	SELECT "
        sql = sql & N & "	 KHINMEICD "
        sql = sql & N & "	,SUM(SMIKOMISU) MYSMIKOMISU "
        sql = sql & N & "	FROM T21SEISANM "
        sql = sql & N & "   WHERE TAISYO_FLG = '1' "
        sql = sql & N & "    AND  NENGETSU <= '" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "'"
        sql = sql & N & "	GROUP BY KHINMEICD "
        sql = sql & N & "	)                       T21L ON T13.KHINMEICD = T21L.KHINMEICD "
        sql = sql & N & "LEFT JOIN ( "
        sql = sql & N & "	SELECT "
        sql = sql & N & "	 KHINMEICD "
        sql = sql & N & "	,SUM(SMIKOMISU) MYSMIKOMISU "
        sql = sql & N & "	FROM T21SEISANM "
        sql = sql & N & "   WHERE TAISYO_FLG = '1' "
        sql = sql & N & "    AND  NENGETSU > '" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "'"
        sql = sql & N & "	GROUP BY KHINMEICD "
        sql = sql & N & "	)                       T21U ON T13.KHINMEICD = T21U.KHINMEICD "
        sql = sql & N & "LEFT JOIN T31ZAIKOJ        T31 ON T13.KHINMEICD = T31.KHINMEICD "
        sql = sql & N & "LEFT JOIN M11KEIKAKUHIN    M11 ON T13.KHINMEICD = M11.TT_KHINMEICD "
        sql = sql & N & "WHERE T13.METSUKE IS NOT NULL AND T13.METSUKE = 0 "
        _db.executeDB(sql)
        '<--2010.12.12 add by takagi

        sql = ""
        sql = sql & N & "UPDATE T41SEISANK "
        '-->2011.01.16 chg by takagi #72
        ''sql = sql & N & "SET IKATULOTOSU = CEIL(((YZAIKOTUKISU - KTUKISU) * YYHANBAIRYOU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) "
        ''sql = sql & N & " ,  LOTOSU      = CEIL(((YZAIKOTUKISU - KTUKISU) * YYHANBAIRYOU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) "
        ''sql = sql & N & " ,  YSEISANSU   = (CEIL(((YZAIKOTUKISU - KTUKISU) * YYHANBAIRYOU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSISU "
        ''sql = sql & N & " ,  YSEISANRYOU = (CEIL(((YZAIKOTUKISU - KTUKISU) * YYHANBAIRYOU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSIRYOU "
        ''sql = sql & N & " ,  YZAIKOSU    = TZAIKOSU   - YHANBAISU    + ((CEIL(((YZAIKOTUKISU - KTUKISU) * YYHANBAIRYOU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSISU) "
        ''sql = sql & N & " ,  YZAIKORYOU  = ZZAIKORYOU - YHANBAIRYOU  + ((CEIL(((YZAIKOTUKISU - KTUKISU) * YYHANBAIRYOU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSIRYOU) "
        ''sql = sql & N & " ,  YZAIKOTUKISU= (ZZAIKORYOU - YHANBAIRYOU  + ((CEIL(((YZAIKOTUKISU - KTUKISU) * YYHANBAIRYOU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSIRYOU)) / YYHANBAIRYOU "
        ''sql = sql & N & "SET IKATULOTOSU = CEIL(((KTUKISU - YZAIKOTUKISU) * YYHANBAIRYOU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) "
        'sql = sql & N & " ,  LOTOSU      = CEIL(((KTUKISU - YZAIKOTUKISU) * YYHANBAIRYOU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) "
        'sql = sql & N & " ,  YSEISANSU   = (CEIL(((KTUKISU - YZAIKOTUKISU) * YYHANBAIRYOU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSISU "
        'sql = sql & N & " ,  YSEISANRYOU = (CEIL(((KTUKISU - YZAIKOTUKISU) * YYHANBAIRYOU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) / 1000 * METSUKE / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSIRYOU "
        'sql = sql & N & " ,  YZAIKOSU    = TZAIKOSU   - YHANBAISU    + ((CEIL(((KTUKISU - YZAIKOTUKISU) * YYHANBAIRYOU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSISU) "
        'sql = sql & N & " ,  YZAIKORYOU  = TZAIKORYOU - YHANBAIRYOU  + ((CEIL(((KTUKISU - YZAIKOTUKISU) * YYHANBAIRYOU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) / 1000 * METSUKE / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSIRYOU) "
        'sql = sql & N & " ,  YZAIKOTUKISU= DECODE(YYHANBAIRYOU,0,0, (ZZAIKORYOU - YHANBAIRYOU  + ((CEIL(((KTUKISU - YZAIKOTUKISU) * YYHANBAIRYOU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) / 1000 * METSUKE / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSIRYOU)) / YYHANBAIRYOU ) "
        sql = sql & N & "SET IKATULOTOSU = CEIL( (YYHANBAIRYOU * KTUKISU - TZAIKORYOU + YHANBAIRYOU + KURIKOSIRYOU ) / ((SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD ) / 1000 * METSUKE / 1000) ) "
        sql = sql & N & " ,  LOTOSU      = CEIL( (YYHANBAIRYOU * KTUKISU - TZAIKORYOU + YHANBAIRYOU + KURIKOSIRYOU ) / ((SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD ) / 1000 * METSUKE / 1000) ) "
        sql = sql & N & " ,  YSEISANSU   = (CEIL( (YYHANBAIRYOU * KTUKISU - TZAIKORYOU + YHANBAIRYOU + KURIKOSIRYOU ) / ((SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD ) / 1000 * METSUKE / 1000) ) / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSISU "
        sql = sql & N & " ,  YSEISANRYOU = (CEIL( (YYHANBAIRYOU * KTUKISU - TZAIKORYOU + YHANBAIRYOU + KURIKOSIRYOU ) / ((SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD ) / 1000 * METSUKE / 1000) ) / 1000 * METSUKE / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSIRYOU "
        sql = sql & N & " ,  YZAIKOSU    = TZAIKOSU   - YHANBAISU    + ((CEIL( (YYHANBAIRYOU * KTUKISU - TZAIKORYOU + YHANBAIRYOU + KURIKOSIRYOU ) / ((SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD ) / 1000 * METSUKE / 1000) ) / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSISU) "
        sql = sql & N & " ,  YZAIKORYOU  = TZAIKORYOU - YHANBAIRYOU  + ((CEIL( (YYHANBAIRYOU * KTUKISU - TZAIKORYOU + YHANBAIRYOU + KURIKOSIRYOU ) / ((SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD ) / 1000 * METSUKE / 1000) ) / 1000 * METSUKE / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSIRYOU) "
        sql = sql & N & " ,  YZAIKOTUKISU= DECODE(YYHANBAIRYOU,0,0, (TZAIKORYOU - YHANBAIRYOU  + ((CEIL( (YYHANBAIRYOU * KTUKISU - TZAIKORYOU + YHANBAIRYOU + KURIKOSIRYOU ) / ((SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD ) / 1000 * METSUKE / 1000) ) / 1000 * METSUKE / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSIRYOU)) / YYHANBAIRYOU) "
        '<--2011.01.16 chg by takagi #72
        sql = sql & N & " ,  UPDNAME     = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & " ,  UPDDATE     = SYSDATE "
        sql = sql & N & "WHERE YZAIKOTUKISU < KTUKISU "
        '-->2010.12.12 add by takagi
        sql = sql & N & " AND METSUKE IS NOT NULL AND METSUKE != 0 "
        '<--2010.12.12 add by takagi
        _db.executeDB(sql)

        '-->2010.12.12 add by takagi
        sql = ""
        sql = sql & N & "UPDATE T41SEISANK "
        '-->2011.01.16 chg by takagi #72
        'sql = sql & N & "SET IKATULOTOSU = CEIL(((KTUKISU - YZAIKOTUKISU) * YYHANBAISU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) "
        'sql = sql & N & " ,  LOTOSU      = CEIL(((KTUKISU - YZAIKOTUKISU) * YYHANBAISU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) "
        'sql = sql & N & " ,  YSEISANSU   = (CEIL(((KTUKISU - YZAIKOTUKISU) * YYHANBAISU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSISU "
        'sql = sql & N & " ,  YSEISANRYOU = 0 "
        'sql = sql & N & " ,  YZAIKOSU    = TZAIKOSU   - YHANBAISU    + ((CEIL(((KTUKISU - YZAIKOTUKISU) * YYHANBAISU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSISU) "
        'sql = sql & N & " ,  YZAIKORYOU  = 0 "
        'sql = sql & N & " ,  YZAIKOTUKISU= DECODE(YYHANBAISU,0,0, (ZZAIKOSU - YHANBAISU  + ((CEIL(((KTUKISU - YZAIKOTUKISU) * YYHANBAISU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSISU)) / YYHANBAISU ) "
        sql = sql & N & "SET IKATULOTOSU = CEIL( (YYHANBAISU*KTUKISU-TZAIKOSU+YHANBAISU+KURIKOSISU) /( (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD ) / 1000 ) ) "
        sql = sql & N & " ,  LOTOSU      = CEIL( (YYHANBAISU*KTUKISU-TZAIKOSU+YHANBAISU+KURIKOSISU) /( (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD ) / 1000 ) ) "
        sql = sql & N & " ,  YSEISANSU   = (CEIL( (YYHANBAISU*KTUKISU-TZAIKOSU+YHANBAISU+KURIKOSISU) /( (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD ) / 1000 ) ) / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSISU "
        sql = sql & N & " ,  YSEISANRYOU = 0 "
        sql = sql & N & " ,  YZAIKOSU    = TZAIKOSU   - YHANBAISU    + ((CEIL( (YYHANBAISU*KTUKISU-TZAIKOSU+YHANBAISU+KURIKOSISU) /( (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD ) / 1000 ) ) / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSISU) "
        sql = sql & N & " ,  YZAIKORYOU  = 0 "
        sql = sql & N & " ,  YZAIKOTUKISU= DECODE(YYHANBAISU,0,0, (TZAIKOSU   - YHANBAISU    + ((CEIL( (YYHANBAISU*KTUKISU-TZAIKOSU+YHANBAISU+KURIKOSISU) /( (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD ) / 1000 ) ) / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSISU)) / YYHANBAISU ) "
        '<--2011.01.16 chg by takagi #72
        sql = sql & N & " ,  UPDNAME     = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & " ,  UPDDATE     = SYSDATE "
        sql = sql & N & "WHERE YZAIKOTUKISU < KTUKISU "
        sql = sql & N & " AND METSUKE IS NOT NULL AND METSUKE = 0 "
        _db.executeDB(sql)
        '<--2010.12.12 add by takagi

    End Sub

End Class