'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���j�̔����ю捞�w��
'    �i�t�H�[��ID�jZG330B_HJissekiTorikomi
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ����        2010/10/19                 �V�K              
'�@(2)   ����        2014/06/04                 �ύX�@�ޗ��[�}�X�^�iMPESEKKEI�j�e�[�u���ύX�Ή�            
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Public Class ZG330B_HJissekiTorikomi
    Inherits System.Windows.Forms.Form

    '-------------------------------------------------------------------------------
    '   �萔��`
    '-------------------------------------------------------------------------------
    Private Const RS As String = "RecSet"                       '�f�[�^�Z�b�g�e�[�u����
    Private Const N As String = ControlChars.NewLine            '���s����
    Private Const PGID As String = "ZG330B"
    Private Const IMP_LOG_NM As String = "�̔����ю捞�����o�͏��.txt"

    '-------------------------------------------------------------------------------
    '   �ϐ���`
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu
    Private _updFlg As Boolean = False  '�X�V��
    Private _nouhinshoDb As UtilDBIf

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

        '�[�i���f�[�^�p�R�l�N�V�����̐���
        Dim iniWk As StartUp.iniType = StartUp.iniValue
        _nouhinshoDb = New UtilOleDBDebugger(iniWk.UdlFilePath_Nouhinsho, iniWk.LogFilePath, StartUp.DebugMode)

    End Sub

    '-------------------------------------------------------------------------------
    '�@�t�H�[���N���[�Y�C�x���g
    '-------------------------------------------------------------------------------
    Private Sub ZG330B_HJissekiTorikomi_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            '�[�i���f�[�^�p�R�l�N�V�����̔j��
            _nouhinshoDb.close()
        Catch ex As Exception
        End Try

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
            Dim targetYYYY As String = lblKonkaiYM.Text.Substring(0, 4)
            If 1 <= CInt(lblKonkaiYM.Text.Substring(5, 2)) And CInt(lblKonkaiYM.Text.Substring(5, 2)) <= 3 Then
                targetYYYY = CInt(targetYYYY) - 1
            End If
            sql = ""
            sql = sql & N & "select count(*) CNT from T_�[�i���f�[�^_" & targetYYYY & "�݌v "
            sql = sql & N & "where  substring(convert(varchar,[�o�ד�]),1,6)='" & lblKonkaiYM.Text.Replace("/", "") & "'"
            lblKonkaiKensu.Text = Format(_db.rmNullInt(_nouhinshoDb.selectDB(sql, RS).Tables(RS).Rows(0)("CNT")), "#,##0")

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

            Dim cntNullMetsuke As Integer = 0

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
                    _db.executeDB("delete from ZG330B_W10 where UPDNAME = '" & UtilClass.getComputerName() & "'")
                    pb.value += 1                                                               '0-0 WK������


                    pb.status = "�[�i���f�[�^�擾��"
                    pb.value = 0 : pb.maxVal = CInt(lblKonkaiKensu.Text)
                    Call importNohinsho(pb)                                                     '1-1 ���ѓ]��

                    _db.beginTran()
                    Try
                        pb.status = "�Y���N���f�[�^�폜���E�E�E"
                        _db.executeDB("delete from T10HINHANJ where NENGETU = '" & lblKonkaiYM.Text.Replace("/", "") & "'")
                        _db.executeDB("delete from T71HANBAIS where SUBSTR(TO_CHAR(SYUKKABI),1,6) = '" & lblKonkaiYM.Text.Replace("/", "") & "'")

                        pb.status = "�̔����уf�[�^�\�z���E�E�E"
                        Call insertHanbaiJisseki(cntNullMetsuke)                                    '1-2 �̔����ю捞
                        pb.status = "�̔����яƉ�f�[�^�\�z���E�E�E"
                        Call insertHanbaiJissekiDB()                                                '1-3 �̔����ю捞

                        pb.status = "�X�e�[�^�X�ύX���E�E�E"
                        ed = Now                    '�����I������
                        _parentForm.updateSeigyoTbl(PGID, True, st, ed)                             '2-0 ���Y�ʊm��/����

                        pb.status = "���s�����쐬"
                        insertRireki(st, ed)                                                        '2-1 ���s�����i�[
                    Catch ex As Exception
                        _db.rollbackTran()
                        Throw ex
                    Finally
                        If _db.isTransactionOpen Then _db.commitTran()
                    End Try


                    '������2011.01.19 del by takagi
                    ''-->2011.01.16 add by takagi #���捞���эĎ擾
                    ''-->2011.01.18 del by takagi #82
                    ''_db.executeDB("delete from ZG330B_W10 where UPDNAME = '" & UtilClass.getComputerName() & "'")
                    ''<--2011.01.18 del by takagi #82

                    'pb.status = "�V�K�ǉ��v��Ώەi�̉ߋ��[�i���f�[�^�擾��"
                    'pb.value = 0
                    'Dim wkKey As String = getNonImportHinmei()
                    'If Not "".Equals(wkKey) Then
                    '    '-->2011.01.18 add by takagi #82
                    '    _db.executeDB("delete from ZG330B_W10 where UPDNAME = '" & UtilClass.getComputerName() & "'")
                    '    '<--2011.01.18 add by takagi #82
                    '    Call importNohinsho(pb, wkKey)                                                     '1-1 ���ѓ]��
                    '    _db.beginTran()
                    '    Try

                    '        pb.status = "�ߋ����̔����уf�[�^�\�z���E�E�E"
                    '        Dim wkCnt As Integer
                    '        '-->2011.01.18 chg by takagi #82
                    '        'Call insertHanbaiJisseki(wkCnt)                                    '1-2 �̔����ю捞
                    '        Call insertHanbaiJisseki(wkCnt, True)                                    '1-2 �̔����ю捞
                    '        cntNullMetsuke = cntNullMetsuke + wkCnt
                    '        '<--2011.01.18 chg by takagi #82
                    '        pb.status = "�ߋ����̔����яƉ�f�[�^�\�z���E�E�E"
                    '        Call insertHanbaiJissekiDB()                                                '1-3 �̔����ю捞

                    '    Catch ex As Exception
                    '        _db.rollbackTran()
                    '        Throw ex
                    '    Finally
                    '        If _db.isTransactionOpen Then _db.commitTran()
                    '    End Try
                    'End If
                    ''<--2011,01.16 add by takagi #���捞���эĎ擾
                    ''������2011.01.19 del by takagi

                Finally
                    pb.Close()                                                                  '�v���O���X�o�[��ʏ���
                End Try

            Finally
                Me.Cursor = cur
            End Try

            '�I��MSG
            Dim optionMsg As String = ""
            '-->2010.12.02 add by takagi 
            Dim wk As String = ""
            Dim outFilePath As String = ""
            Call UtilClass.dividePathAndFile(StartUp.iniValue.LogFilePath, outFilePath, wk)
            outFilePath = outFilePath & "\" & IMP_LOG_NM
            '<--2010.12.02 add by takagi
            If cntNullMetsuke > 0 Then
                optionMsg = "-----------------------------------------------------------------" & N & _
                            "�ڕt�̎擾���s���Ȃ��f�[�^��" & cntNullMetsuke & "�����݂��܂����B" & N & _
                            "�ڍׂȕi���R�[�h�̓��O���m�F���Ă��������B"

                '-->2010.12.02 add by takagi 
                optionMsg = optionMsg & N & N & outFilePath
                '<--2010.12.02 add by takagi
            End If
            Call _msgHd.dspMSG("completeRun", optionMsg)
            If cntNullMetsuke > 0 Then
                '���O�\��
                Try
                    '-->2010.12.02 add by takagi 
                    'System.Diagnostics.Process.Start(UtilClass.getAppPath(StartUp.assembly) & "\" & IMP_LOG_NM)   '�֘A�t�����A�v���ŋN��
                    System.Diagnostics.Process.Start(outFilePath)   '�֘A�t�����A�v���ŋN��
                    '<--2010.12.02 add by takagi
                Catch ex As Exception
                End Try
            End If
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
        sql = sql & N & ",NAME1 "      '�Ώ۔N��
        sql = sql & N & ",UPDNAME "    '�ŏI�X�V��
        sql = sql & N & ",UPDDATE "    '�ŏI�X�V��
        sql = sql & N & ")values( "
        sql = sql & N & "  '" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "' "                             '�����N��
        sql = sql & N & ", '" & _db.rmSQ(lblKeikakuDate.Text.Replace("/", "")) & "' "                           '�v��N��
        sql = sql & N & ", '" & _db.rmSQ(PGID) & "' "                                                           '�@�\ID
        sql = sql & N & ",to_date('" & Format(prmStDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '�����J�n����
        sql = sql & N & ",to_date('" & Format(prmEdDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '�����I������
        sql = sql & N & ", " & CInt(lblKonkaiKensu.Text) & " "                                                  '���s����
        sql = sql & N & ", '" & lblKonkaiYM.Text.Replace("/", "") & "' "                                        '�Ώ۔N��
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "                                     '�ŏI�X�V��
        sql = sql & N & ",sysdate "                                                                             '�ŏI�X�V��
        sql = sql & N & ") "
        _db.executeDB(sql)

    End Sub

    '-------------------------------------------------------------------------------
    '   �̔����э쐬
    '   �i�����T�v�j���[�N���̔����тփf�[�^��������
    '   �����̓p�����^  �F�Ȃ�
    '   ���o�̓p�����^  �F�ڕt�擾�s�ǌ���
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    '-->2011.01.18 chg by takagi #82
    'Private Sub insertHanbaiJisseki(ByRef prmRefNullMetsukeCnt As Integer)
    Private Sub insertHanbaiJisseki(ByRef prmRefNullMetsukeCnt As Integer, Optional ByVal prmAppendFlg As Boolean = False)
        '<--2011.01.18 chg by takagi #82

        Dim sql As String = ""
        sql = sql & N & "INSERT INTO T10HINHANJ "
        sql = sql & N & "( "
        sql = sql & N & " HINMEICD "        '���i���R�[�h
        sql = sql & N & ",NENGETU "         '�N��
        sql = sql & N & ",HJISSEKIRYOU "    '�̔����ї�
        sql = sql & N & ",HJISSEKISU "      '�̔����ѐ�
        sql = sql & N & ",METSUKE "         '�ڕt
        sql = sql & N & ",UPDNAME "         '�[��ID
        sql = sql & N & ",UPDDATE "         '�X�V����
        sql = sql & N & ") "
        sql = sql & N & "SELECT "
        sql = sql & N & " SUB.HINCD "       '���i���R�[�h
        sql = sql & N & ",SUB.YM "          '�N��
        sql = sql & N & ",SUB.SUMD "        '�̔����ї�
        sql = sql & N & ",SUB.SUMS "        '�̔����ѐ�
        sql = sql & N & ",M.METSUKE "       '�ڕt
        sql = sql & N & ",SUB.PCID "        '�[��ID
        sql = sql & N & ",SUB.DT "          '�X�V����
        sql = sql & N & "FROM  "
        sql = sql & N & "    ( "
        sql = sql & N & "    SELECT  "
        sql = sql & N & "        RPAD(SIYO    ,2,' ') "
        sql = sql & N & "     || LPAD(HINSYU  ,3,'0') "
        sql = sql & N & "     || LPAD(SENSHIN ,3,'0') "
        sql = sql & N & "     || LPAD(SIZECD  ,2,'0') "
        sql = sql & N & "     || LPAD(IRO     ,3,'0')                      HINCD "    '���i���R�[�h
        '-->2011.01.16 chg by takagi #���捞���эĎ擾
        'sql = sql & N & "    ,'" & lblKonkaiYM.Text.Replace("/", "") & "'  YM    "    '�N��
        sql = sql & N & "    ,substr(SYUKKABI,1,6)  YM    "    '�N��
        '-->2011.01.16 chg by takagi #���捞���эĎ擾
        sql = sql & N & "    ,SUM(DOURYOU)                                 SUMD  "    '�̔����ї�
        sql = sql & N & "    ,SUM(SYUKANUM)                                SUMS  "    '�̔����ѐ�
        sql = sql & N & "    ,'" & UtilClass.getComputerName() & "'        PCID  "    '�[��ID
        sql = sql & N & "    ,SYSDATE                                      DT    "    '�X�V����
        sql = sql & N & "    FROM ZG330B_W10  "                  '�̔�����WK
        sql = sql & N & "    WHERE  UPDNAME = '" & UtilClass.getComputerName() & "' "
        sql = sql & N & "      AND (TO_CHAR(TORIHIKIKBN) LIKE '11%' or TO_CHAR(TORIHIKIKBN) LIKE '12%')  "  '����f�[�^�敪
        sql = sql & N & "      AND SYOHINKBN = 1  "                                                         '���i�敪
        sql = sql & N & "      AND TYOUBOKBN = 1  "                                                         '����敪
        sql = sql & N & "      AND (HINMEIKBN2 LIKE '01%' or HINMEIKBN2 LIKE '02%') "                       '�i���敪2
        sql = sql & N & "    GROUP BY    RPAD(SIYO    ,2,' ') "                       '���i���R�[�h
        sql = sql & N & "             || LPAD(HINSYU  ,3,'0') "
        sql = sql & N & "             || LPAD(SENSHIN ,3,'0') "
        sql = sql & N & "             || LPAD(SIZECD  ,2,'0') "
        sql = sql & N & "             || LPAD(IRO     ,3,'0') "
        '-->2011.01.16 add by takagi #���捞���эĎ擾
        sql = sql & N & "            ,substr(SYUKKABI,1,6) "    '�N��
        '-->2011.01.16 add by takagi #���捞���эĎ擾
        sql = sql & N & "    ) SUB "
        '������2011.01.19 del by takagi
        ''sql = sql & N & "INNER JOIN M12SYUYAKU M12"
        ''sql = sql & N & " ON   SUB.HINCD = M12.HINMEICD "
        ''sql = sql & N & "INNER JOIN M11KEIKAKUHIN M11"
        ''sql = sql & N & " ON   M12.KHINMEICD = M11.TT_KHINMEICD "
        ''sql = sql & N & "  AND M11.TT_SYUBETU = 1 " '�i1�F�݌Ɂj
        '������2011.01.19 del by takagi
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
        sql = sql & N & " ON SUB.HINCD = M.HINCD "
        sql = sql & N & "ORDER BY SUB.HINCD "
        Try
            _db.executeDB(sql)
        Catch ex As Exception
            If InStr(ex.Message, "ORA-00001") > 0 Then
                '��Ӑ���ᔽ
                Throw New UsrDefException("�ޗ��[�}�X�^(MPESEKKEI)�ɏd������i���R�[�h���o�^����Ă��܂��B�p�������ł��Ȃ����߁A���s�𒆎~���܂��B", _msgHd.getMSG("NonUniqueMPESEKKEI", "�d�����������A�ēx���������s���Ă��������B"))
            End If
            Throw ex
        End Try

        sql = ""
        '������2011.01.19 chg by takagi
        ''sql = sql & N & "SELECT HINMEICD  "
        ''sql = sql & N & "FROM T10HINHANJ "
        ''sql = sql & N & "WHERE NENGETU = '" & lblKonkaiYM.Text.Replace("/", "") & "' "
        ''sql = sql & N & "  AND METSUKE IS NULL "
        ''sql = sql & N & "ORDER BY HINMEICD "
        sql = sql & N & "SELECT T10.HINMEICD  "
        sql = sql & N & "FROM T10HINHANJ T10 "
        sql = sql & N & "INNER JOIN M12SYUYAKU M12"
        sql = sql & N & " ON   T10.HINMEICD = M12.HINMEICD "
        sql = sql & N & "INNER JOIN M11KEIKAKUHIN M11"
        sql = sql & N & " ON   M12.KHINMEICD = M11.TT_KHINMEICD "
        sql = sql & N & "  AND M11.TT_SYUBETU = 1 " '�i1�F�݌Ɂj
        sql = sql & N & "WHERE T10.NENGETU = '" & lblKonkaiYM.Text.Replace("/", "") & "' "
        sql = sql & N & "  AND T10.METSUKE IS NULL "
        sql = sql & N & "ORDER BY T10.HINMEICD "
        '������2011.01.19 chg by takagi
        Dim ds As DataSet = _db.selectDB(sql, RS, prmRefNullMetsukeCnt)
        If prmRefNullMetsukeCnt > 0 Then

            '-->2010.12.02 add by takagi 
            Dim wk As String = ""
            Dim outFilePath As String = ""
            Call UtilClass.dividePathAndFile(StartUp.iniValue.LogFilePath, outFilePath, wk)
            outFilePath = outFilePath & "\" & IMP_LOG_NM
            '<--2010.12.02 add by takagi

            Dim logBuf As System.Text.StringBuilder = New System.Text.StringBuilder
            logBuf.Append(Format(Now(), "yyyy/MM/dd HH:mm:ss") & "���s" & N)
            logBuf.Append("==========================================================" & N)
            logBuf.Append("���̔����ю捞�����o�͏��" & N)
            logBuf.Append("  �ޗ��[�}�X�^���o�^�i���R�[�h�i�ڕt�̎擾���s���Ȃ������i���R�[�h�j" & N)
            logBuf.Append("----------------------------------------------------------" & N)
            For i As Integer = 0 To prmRefNullMetsukeCnt - 1
                logBuf.Append(_db.rmNullStr(ds.Tables(RS).Rows(i)("HINMEICD")) & N)
            Next
            logBuf.Append("==========================================================")
            '-->2010.12.02 upd by takagi 
            'Dim tw As UtilMDL.Text.UtilTextWriter = New UtilMDL.Text.UtilTextWriter(UtilClass.getAppPath(StartUp.assembly) & "\" & IMP_LOG_NM)
            Dim tw As UtilMDL.Text.UtilTextWriter = New UtilMDL.Text.UtilTextWriter(outFilePath)
            '<--2010.12.02 upd by takagi
            '-->2011.01.18 chg by takagi #82
            'tw.open(False)
            tw.open(prmAppendFlg)
            '<--2011.01.18 chg by takagi #82
            Try
                tw.writeLine(logBuf.ToString)
            Finally
                tw.close()
            End Try

        End If

    End Sub


    '-------------------------------------------------------------------------------
    '   �̔�����DB�쐬
    '   �i�����T�v�j���[�N���̔�����DB(T71)�փf�[�^��������
    '   �����̓p�����^  �F�Ȃ�
    '   ���o�̓p�����^  �F�Ȃ�
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub insertHanbaiJissekiDB()

        Dim sqlBuff As System.Text.StringBuilder = New System.Text.StringBuilder
        sqlBufF.Append(N & "INSERT INTO T71HANBAIS ")
        sqlBufF.Append(N & "( ")
        sqlBufF.Append(N & " HINMEIKBN1  ")                           '�i���敪�P
        sqlBufF.Append(N & ",HINMEIKBN2  ")                           '�i���敪�Q
        sqlBufF.Append(N & ",HINMEIKBN3  ")                           '�i���敪�R
        sqlBufF.Append(N & ",HINMEIKBN4  ")                           '�i���敪�S
        sqlBufF.Append(N & ",HINMEIKBN5  ")                           '�i���敪�T
        sqlBufF.Append(N & ",HINMEIKBN6  ")                           '�i���敪�U
        sqlBufF.Append(N & ",HINMEIKBN7  ")                           '�i���敪�V
        sqlBufF.Append(N & ",HINMEIKBN8  ")                           '�i���敪�W
        sqlBufF.Append(N & ",TORIHIKISAKIKBN1  ")                     '�����敪�P
        sqlBufF.Append(N & ",TORIHIKISAKIKBN2  ")                     '�����敪�Q
        sqlBufF.Append(N & ",TORIHIKISAKIKBN3  ")                     '�����敪�R
        sqlBufF.Append(N & ",TORIHIKISAKIKBN4  ")                     '�����敪�S
        sqlBufF.Append(N & ",TORIHIKISAKIKBN5  ")                     '�����敪�T
        sqlBufF.Append(N & ",TORIHIKISAKIKBN6  ")                     '�����敪�U
        sqlBufF.Append(N & ",TO_SONEKI_KBN  ")                        '���Ӑ�ʑ��v�Z�o�p���o�敪
        sqlBufF.Append(N & ",TORIHIKIKBN  ")                          '����f�[�^�敪
        sqlBufF.Append(N & ",NYUKO  ")                                '���ɑq��
        sqlBufF.Append(N & ",SYUKO  ")                                '�o�ɑq��
        sqlBufF.Append(N & ",SYOHINKBN  ")                            '���i�敪
        sqlBufF.Append(N & ",SIYO  ")                                 '�d�l
        sqlBufF.Append(N & ",HINSYU  ")                               '�i��
        sqlBufF.Append(N & ",SENSHIN  ")                              '���S��
        sqlBufF.Append(N & ",SIZECD  ")                               '�T�C�Y
        sqlBufF.Append(N & ",IRO  ")                                  '�F
        sqlBufF.Append(N & ",YOBI1  ")                                '�\���P
        sqlBufF.Append(N & ",HINSYUMEI  ")                            '�i�햼
        sqlBufF.Append(N & ",SIZEMEI  ")                              '�T�C�Y��
        sqlBufF.Append(N & ",IROMEI  ")                               '�F�x������
        sqlBufF.Append(N & ",SYUKANUM  ")                             '�o�א�
        sqlBufF.Append(N & ",UNIT  ")                                 '�P��
        sqlBufF.Append(N & ",DOUTAIKBN  ")                            '���̋敪
        sqlBufF.Append(N & ",TANKA  ")                                '�P��
        sqlBufF.Append(N & ",KINGAKU  ")                              '���z
        sqlBufF.Append(N & ",TORIHIKISAKIKBN  ")                      '�����敪
        sqlBufF.Append(N & ",TORIHIKISAKI  ")                         '�����
        sqlBufF.Append(N & ",SHITENCD  ")                             '�x�X�R�[�h
        sqlBufF.Append(N & ",TORIHIKISAKIMEI  ")                      '����於��
        sqlBufF.Append(N & ",NOUSHOKBN  ")                            '�[���敪
        sqlBufF.Append(N & ",NOUSHO  ")                               '�[��
        sqlBufF.Append(N & ",NOUHINCD  ")                             '�[���R�[�h
        sqlBufF.Append(N & ",NOUSYOMEI  ")                            '�[������
        sqlBufF.Append(N & ",DNPYOUNO  ")                             '�`�[�m�n
        sqlBufF.Append(N & ",DENPYOUGYONO  ")                         '�`�[�s�m�n
        sqlBufF.Append(N & ",SYORIBI  ")                              '������
        sqlBufF.Append(N & ",SETSUDANMOTO  ")                         '�ؒf�𒷌�
        sqlBufF.Append(N & ",JYOCHO  ")                               '��
        sqlBufF.Append(N & ",KOSU  ")                                 '��
        sqlBufF.Append(N & ",TYOUBOKBN  ")                            '����敪
        sqlBufF.Append(N & ",ZAIMUGENKAKBN  ")                        '���������敪
        sqlBufF.Append(N & ",ZAIKOKANRIKBN  ")                        '�݌ɊǗ��敪
        sqlBufF.Append(N & ",CHOTATUKBN  ")                           '���B�敪
        sqlBufF.Append(N & ",KANRYOKBN  ")                            '�����敪
        sqlBufF.Append(N & ",TEIHAKBN  ")                             '��[�敪
        sqlBufF.Append(N & ",SYUKKABI  ")                             '�o�ד�
        sqlBufF.Append(N & ",KONPOUTYPE  ")                           '����^�C�v�R�[�h
        sqlBufF.Append(N & ",KONPONUM  ")                             '���
        sqlBufF.Append(N & ",UNSOKBN  ")                              '�^���敪
        sqlBufF.Append(N & ",UNSOKAISHAKBN  ")                        '�^����ЃR�[�h
        sqlBufF.Append(N & ",UNCHINKBN  ")                            '�^���敪
        sqlBufF.Append(N & ",KARIKENSHUKINGAKU  ")                    '���������z
        sqlBufF.Append(N & ",BUMON  ")                                '����
        sqlBufF.Append(N & ",JYUCHUDENPYOUNO  ")                      '�󒍓`�[�m�n
        sqlBufF.Append(N & ",JYUCHGYONO  ")                           '�󒍍s�m�n
        sqlBufF.Append(N & ",JYUCHUGAPPI  ")                          '�󒍌���
        sqlBufF.Append(N & ",NOUKI  ")                                '�[��
        sqlBufF.Append(N & ",HIKIATEKBN  ")                           '�����敪
        sqlBufF.Append(N & ",KENMEI  ")                               '����
        sqlBufF.Append(N & ",SHUYOTORIHIKISAKI  ")                    '��v�����R�[�h
        sqlBufF.Append(N & ",JYUYOBUMON  ")                           '���v����
        sqlBufF.Append(N & ",DOUBASE  ")                              '���x�[�X
        sqlBufF.Append(N & ",RIEKI  ")                                '���v�z
        sqlBufF.Append(N & ",CHUMONNO  ")                             '�����m�n
        sqlBufF.Append(N & ",KEIYAKUKBN  ")                           '�_��敪
        sqlBufF.Append(N & ",KEIYAKUTSUKI  ")                         '�_��
        sqlBufF.Append(N & ",SHIHARAIKBN  ")                          '�x���敪
        sqlBufF.Append(N & ",HANBAITESURYORITSU  ")                   '�̔��萔����
        sqlBufF.Append(N & ",MOME  ")                                 '�ЊO������
        sqlBufF.Append(N & ",WAKUNO  ")                               '�g�m�n
        sqlBufF.Append(N & ",HIKIATENO  ")                            '�����m�n
        sqlBufF.Append(N & ",GAISANJYURYO  ")                         '�T�Z�d��
        sqlBufF.Append(N & ",KADOUSAIN  ")                            '�ړ��T�C��
        sqlBufF.Append(N & ",TEIREIRINJIKBN  ")                       '���Վ��敪
        sqlBufF.Append(N & ",HAKKOUTANTO  ")                          '���s�S��
        sqlBufF.Append(N & ",TANMATSUNO  ")                           '�[���@�m�n
        sqlBufF.Append(N & ",JIGYOKBN  ")                             '���ƕ��敪
        sqlBufF.Append(N & ",KAMOKUKBN  ")                            '�Ȗڋ敪
        sqlBufF.Append(N & ",YOBI2  ")                                '�\���Q
        sqlBufF.Append(N & ",CUBASE_ST  ")                            '�b�t�x�[�X�W��
        sqlBufF.Append(N & ",CUBASE_TEKIYO  ")                        '�b�t�x�[�X�K�p
        sqlBufF.Append(N & ",SEIZOCOST  ")                            '�����R�X�g�v
        sqlBufF.Append(N & ",SYUZAIRYOHI  ")                          '��ޗ���
        sqlBufF.Append(N & ",HUKUZAIRYOHI  ")                         '�����ޔ�
        sqlBufF.Append(N & ",KAKOUHI  ")                              '���H��
        sqlBufF.Append(N & ",BASESAGAKU  ")                           '�x�[�X���z
        sqlBufF.Append(N & ",HUKUSHIZAIHOSEI_RATE  ")                 '�����ޕ␳�W��
        sqlBufF.Append(N & ",HUKUSHIZAIHOSEI_GAKU  ")                 '�����ޕ␳�z
        sqlBufF.Append(N & ",DOURYOU  ")                              '����
        sqlBufF.Append(N & ",HIKARI_CORE  ")                          '���R�A��
        sqlBufF.Append(N & ",HIKARI_CONECTOR  ")                      '���R�l�N�^��
        sqlBufF.Append(N & ",SHITENMEI  ")                            '�x�X��
        sqlBufF.Append(N & ",KITANIHONKINGAKU  ")                     '�k���{���z
        sqlBufF.Append(N & ",KITANIHONTANKA  ")                       '�k���{�P��
        sqlBufF.Append(N & ",KITANIHONRIEKI  ")                       '�k���{���v
        sqlBufF.Append(N & ",KITANIHONKOUNYUTANKA  ")                 '�k���{�w���P��
        sqlBufF.Append(N & ",HENDOUHANBAIHIRITSU  ")                  '�ϓ��̔��
        sqlBufF.Append(N & ",HENDOUHANBAIHISAGAKU  ")                 '�ϓ��̔���z
        sqlBufF.Append(N & ",BUMONCD  ")                              '����R�[�h
        sqlBufF.Append(N & ",TOHIKISAKICD  ")                         '�����R�[�h
        sqlBufF.Append(N & ",TOUKEICD  ")                             '���v�R�[�h
        sqlBufF.Append(N & ",SDCRIEKI  ")                             '�r�c�b���v
        sqlBufF.Append(N & ",COSTSURAIDO  ")                          '�R�X�g�X���C�h�z
        sqlBufF.Append(N & ",SDCTANKA  ")                             '�r�c�b�w���P��
        sqlBufF.Append(N & ",YOBI3  ")                                '�\���R
        sqlBufF.Append(N & ",TORIHIKIKBN7  ")                         '�����敪�V
        sqlBufF.Append(N & ",TORIHIKIKBN8  ")                         '�����敪�W
        sqlBufF.Append(N & ",UPDNAME   ")                             '�[��ID
        sqlBufF.Append(N & ",UPDDATE  ")                              '�X�V����
        sqlBufF.Append(N & ") ")
        sqlBufF.Append(N & "SELECT ")
        sqlBuff.Append(N & " HINMEIKBN1  ")                           '�i���敪�P
        sqlBuff.Append(N & ",HINMEIKBN2  ")                           '�i���敪�Q
        sqlBuff.Append(N & ",HINMEIKBN3  ")                           '�i���敪�R
        sqlBuff.Append(N & ",HINMEIKBN4  ")                           '�i���敪�S
        sqlBuff.Append(N & ",HINMEIKBN5  ")                           '�i���敪�T
        sqlBuff.Append(N & ",HINMEIKBN6  ")                           '�i���敪�U
        sqlBuff.Append(N & ",HINMEIKBN7  ")                           '�i���敪�V
        sqlBuff.Append(N & ",HINMEIKBN8  ")                           '�i���敪�W
        sqlBuff.Append(N & ",TORIHIKISAKIKBN1  ")                     '�����敪�P
        sqlBuff.Append(N & ",TORIHIKISAKIKBN2  ")                     '�����敪�Q
        sqlBuff.Append(N & ",TORIHIKISAKIKBN3  ")                     '�����敪�R
        sqlBuff.Append(N & ",TORIHIKISAKIKBN4  ")                     '�����敪�S
        sqlBuff.Append(N & ",TORIHIKISAKIKBN5  ")                     '�����敪�T
        sqlBuff.Append(N & ",TORIHIKISAKIKBN6  ")                     '�����敪�U
        sqlBuff.Append(N & ",TO_SONEKI_KBN  ")                        '���Ӑ�ʑ��v�Z�o�p���o�敪
        sqlBuff.Append(N & ",TORIHIKIKBN  ")                          '����f�[�^�敪
        sqlBuff.Append(N & ",NYUKO  ")                                '���ɑq��
        sqlBuff.Append(N & ",SYUKO  ")                                '�o�ɑq��
        sqlBuff.Append(N & ",SYOHINKBN  ")                            '���i�敪
        sqlBuff.Append(N & ",SIYO  ")                                 '�d�l
        sqlBuff.Append(N & ",HINSYU  ")                               '�i��
        sqlBuff.Append(N & ",SENSHIN  ")                              '���S��
        sqlBuff.Append(N & ",SIZECD  ")                               '�T�C�Y
        sqlBuff.Append(N & ",IRO  ")                                  '�F
        sqlBuff.Append(N & ",YOBI1  ")                                '�\���P
        sqlBuff.Append(N & ",HINSYUMEI  ")                            '�i�햼
        sqlBuff.Append(N & ",SIZEMEI  ")                              '�T�C�Y��
        sqlBuff.Append(N & ",IROMEI  ")                               '�F�x������
        sqlBuff.Append(N & ",SYUKANUM  ")                             '�o�א�
        sqlBuff.Append(N & ",UNIT  ")                                 '�P��
        sqlBuff.Append(N & ",DOUTAIKBN  ")                            '���̋敪
        sqlBuff.Append(N & ",TANKA  ")                                '�P��
        sqlBuff.Append(N & ",KINGAKU  ")                              '���z
        sqlBuff.Append(N & ",TORIHIKISAKIKBN  ")                      '�����敪
        sqlBuff.Append(N & ",TORIHIKISAKI  ")                         '�����
        sqlBuff.Append(N & ",SHITENCD  ")                             '�x�X�R�[�h
        sqlBuff.Append(N & ",TORIHIKISAKIMEI  ")                      '����於��
        sqlBuff.Append(N & ",NOUSHOKBN  ")                            '�[���敪
        sqlBuff.Append(N & ",NOUSHO  ")                               '�[��
        sqlBuff.Append(N & ",NOUHINCD  ")                             '�[���R�[�h
        sqlBuff.Append(N & ",NOUSYOMEI  ")                            '�[������
        sqlBuff.Append(N & ",DNPYOUNO  ")                             '�`�[�m�n
        sqlBuff.Append(N & ",DENPYOUGYONO  ")                         '�`�[�s�m�n
        sqlBuff.Append(N & ",SYORIBI  ")                              '������
        sqlBuff.Append(N & ",SETSUDANMOTO  ")                         '�ؒf�𒷌�
        sqlBuff.Append(N & ",JYOCHO  ")                               '��
        sqlBuff.Append(N & ",KOSU  ")                                 '��
        sqlBuff.Append(N & ",TYOUBOKBN  ")                            '����敪
        sqlBuff.Append(N & ",ZAIMUGENKAKBN  ")                        '���������敪
        sqlBuff.Append(N & ",ZAIKOKANRIKBN  ")                        '�݌ɊǗ��敪
        sqlBuff.Append(N & ",CHOTATUKBN  ")                           '���B�敪
        sqlBuff.Append(N & ",KANRYOKBN  ")                            '�����敪
        sqlBuff.Append(N & ",TEIHAKBN  ")                             '��[�敪
        sqlBuff.Append(N & ",SYUKKABI  ")                             '�o�ד�
        sqlBuff.Append(N & ",KONPOUTYPE  ")                           '����^�C�v�R�[�h
        sqlBuff.Append(N & ",KONPONUM  ")                             '���
        sqlBuff.Append(N & ",UNSOKBN  ")                              '�^���敪
        sqlBuff.Append(N & ",UNSOKAISHAKBN  ")                        '�^����ЃR�[�h
        sqlBuff.Append(N & ",UNCHINKBN  ")                            '�^���敪
        sqlBuff.Append(N & ",KARIKENSHUKINGAKU  ")                    '���������z
        sqlBuff.Append(N & ",BUMON  ")                                '����
        sqlBuff.Append(N & ",JYUCHUDENPYOUNO  ")                      '�󒍓`�[�m�n
        sqlBuff.Append(N & ",JYUCHGYONO  ")                           '�󒍍s�m�n
        sqlBuff.Append(N & ",JYUCHUGAPPI  ")                          '�󒍌���
        sqlBuff.Append(N & ",NOUKI  ")                                '�[��
        sqlBuff.Append(N & ",HIKIATEKBN  ")                           '�����敪
        sqlBuff.Append(N & ",KENMEI  ")                               '����
        sqlBuff.Append(N & ",SHUYOTORIHIKISAKI  ")                    '��v�����R�[�h
        sqlBuff.Append(N & ",JYUYOBUMON  ")                           '���v����
        sqlBuff.Append(N & ",DOUBASE  ")                              '���x�[�X
        sqlBuff.Append(N & ",RIEKI  ")                                '���v�z
        sqlBuff.Append(N & ",CHUMONNO  ")                             '�����m�n
        sqlBuff.Append(N & ",KEIYAKUKBN  ")                           '�_��敪
        sqlBuff.Append(N & ",KEIYAKUTSUKI  ")                         '�_��
        sqlBuff.Append(N & ",SHIHARAIKBN  ")                          '�x���敪
        sqlBuff.Append(N & ",HANBAITESURYORITSU  ")                   '�̔��萔����
        sqlBuff.Append(N & ",MOME  ")                                 '�ЊO������
        sqlBuff.Append(N & ",WAKUNO  ")                               '�g�m�n
        sqlBuff.Append(N & ",HIKIATENO  ")                            '�����m�n
        sqlBuff.Append(N & ",GAISANJYURYO  ")                         '�T�Z�d��
        sqlBuff.Append(N & ",KADOUSAIN  ")                            '�ړ��T�C��
        sqlBuff.Append(N & ",TEIREIRINJIKBN  ")                       '���Վ��敪
        sqlBuff.Append(N & ",HAKKOUTANTO  ")                          '���s�S��
        sqlBuff.Append(N & ",TANMATSUNO  ")                           '�[���@�m�n
        sqlBuff.Append(N & ",JIGYOKBN  ")                             '���ƕ��敪
        sqlBuff.Append(N & ",KAMOKUKBN  ")                            '�Ȗڋ敪
        sqlBuff.Append(N & ",YOBI2  ")                                '�\���Q
        sqlBuff.Append(N & ",CUBASE_ST  ")                            '�b�t�x�[�X�W��
        sqlBuff.Append(N & ",CUBASE_TEKIYO  ")                        '�b�t�x�[�X�K�p
        sqlBuff.Append(N & ",SEIZOCOST  ")                            '�����R�X�g�v
        sqlBuff.Append(N & ",SYUZAIRYOHI  ")                          '��ޗ���
        sqlBuff.Append(N & ",HUKUZAIRYOHI  ")                         '�����ޔ�
        sqlBuff.Append(N & ",KAKOUHI  ")                              '���H��
        sqlBuff.Append(N & ",BASESAGAKU  ")                           '�x�[�X���z
        sqlBuff.Append(N & ",HUKUSHIZAIHOSEI_RATE  ")                 '�����ޕ␳�W��
        sqlBuff.Append(N & ",HUKUSHIZAIHOSEI_GAKU  ")                 '�����ޕ␳�z
        sqlBuff.Append(N & ",DOURYOU  ")                              '����
        sqlBuff.Append(N & ",HIKARI_CORE  ")                          '���R�A��
        sqlBuff.Append(N & ",HIKARI_CONECTOR  ")                      '���R�l�N�^��
        sqlBuff.Append(N & ",SHITENMEI  ")                            '�x�X��
        sqlBuff.Append(N & ",KITANIHONKINGAKU  ")                     '�k���{���z
        sqlBuff.Append(N & ",KITANIHONTANKA  ")                       '�k���{�P��
        sqlBuff.Append(N & ",KITANIHONRIEKI  ")                       '�k���{���v
        sqlBuff.Append(N & ",KITANIHONKOUNYUTANKA  ")                 '�k���{�w���P��
        sqlBuff.Append(N & ",HENDOUHANBAIHIRITSU  ")                  '�ϓ��̔��
        sqlBuff.Append(N & ",HENDOUHANBAIHISAGAKU  ")                 '�ϓ��̔���z
        sqlBuff.Append(N & ",BUMONCD  ")                              '����R�[�h
        sqlBuff.Append(N & ",TOHIKISAKICD  ")                         '�����R�[�h
        sqlBuff.Append(N & ",TOUKEICD  ")                             '���v�R�[�h
        sqlBuff.Append(N & ",SDCRIEKI  ")                             '�r�c�b���v
        sqlBuff.Append(N & ",COSTSURAIDO  ")                          '�R�X�g�X���C�h�z
        sqlBuff.Append(N & ",SDCTANKA  ")                             '�r�c�b�w���P��
        sqlBuff.Append(N & ",YOBI3  ")                                '�\���R
        sqlBuff.Append(N & ",TORIHIKIKBN7  ")                         '�����敪�V
        sqlBuff.Append(N & ",TORIHIKIKBN8  ")                         '�����敪�W
        sqlBuff.Append(N & ",'" & UtilClass.getComputerName() & "'   ") '�[��ID
        sqlBuff.Append(N & ",SYSDATE  ")                              '�X�V����
        sqlBuff.Append(N & "FROM  ZG330B_W10  ")                  '�̔�����WK
        sqlBuff.Append(N & "WHERE UPDNAME = '" & UtilClass.getComputerName() & "'")
        sqlBuff.Append(N & "  AND (TO_CHAR(TORIHIKIKBN) LIKE '11%' or TO_CHAR(TORIHIKIKBN) LIKE '12%')  ")  '����f�[�^�敪
        sqlBuff.Append(N & "  AND SYOHINKBN = 1  ")                                                         '���i�敪
        sqlBuff.Append(N & "  AND TYOUBOKBN = 1  ")                                                         '����敪
        sqlBuff.Append(N & "  AND (HINMEIKBN2 LIKE '01%' or HINMEIKBN2 LIKE '02%') ")                       '�i���敪2
        _db.executeDB(sqlBuff.ToString)

    End Sub

    '-->2011.01.16 add by takagi #�V�K�v��i�̔̔����эĎ捞�Ή�
    '-------------------------------------------------------------------------------
    '   �[�i���f�[�^���捞�i���R�[�h���o
    '-------------------------------------------------------------------------------
    Private Function getNonImportHinmei() As String
        Dim buf As System.Text.StringBuilder = New System.Text.StringBuilder

        Dim sql As String = ""
        Dim iRecCnt As Integer = 0
        sql = ""
        sql = sql & N & "SELECT "
        sql = sql & N & " SUB.HINMEICD"
        sql = sql & N & "FROM ("
        sql = sql & N & "	SELECT DISTINCT M12.HINMEICD"
        sql = sql & N & "	FROM M11KEIKAKUHIN M11 "
        sql = sql & N & "	INNER JOIN M12SYUYAKU M12 "
        sql = sql & N & "	ON M11.TT_KHINMEICD = M12.KHINMEICD"
        sql = sql & N & "	) SUB "
        sql = sql & N & "LEFT JOIN ("
        sql = sql & N & "	SELECT DISTINCT HINMEICD "
        sql = sql & N & "	FROM T10HINHANJ"
        sql = sql & N & "	) T10"
        sql = sql & N & "ON SUB.HINMEICD = T10.HINMEICD"
        sql = sql & N & "WHERE T10.HINMEICD IS NULL"
        sql = sql & N & "ORDER BY SUB.HINMEICD"
        Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)     '�v��Ώەi�ł���Ȃ���A�̔����т����݂��Ȃ��i��CD�𒊏o
        With ds.Tables(RS)
            For i As Integer = 0 To iRecCnt - 1
                If buf.Length > 0 Then buf.Append(",")
                buf.Append(convNullStr(.Rows(i)("HINMEICD")))
            Next
        End With
        Return buf.tostring()

    End Function
    '<--2011.01.16 add by takagi #�V�K�v��i�̔̔����эĎ捞�Ή�

    '-------------------------------------------------------------------------------
    '   �[�i���f�[�^�捞
    '   �i�����T�v�jSQLSV(�[�i���f�[�^)����OracleWK�փf�[�^����荞��
    '   �����̓p�����^  �F�v���O���X�o�[
    '   ���o�̓p�����^  �F�Ȃ�
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    '-->2011.01.16 add by takagi #���捞���эĎ擾
    'Private Sub importNohinsho(ByRef prmRefPb As UtilProgressBar)
    Private Sub importNohinsho(ByRef prmRefPb As UtilProgressBar, Optional ByVal prmNonImportsHinmeiCDs As String = "")
        '<--2011.01.16 add by takagi #���捞���эĎ擾

        Dim sysdate As String = "to_date('" & Format(Now, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS')"

        Dim targetYYYY As String = lblKonkaiYM.Text.Substring(0, 4)
        If 1 <= CInt(lblKonkaiYM.Text.Substring(5, 2)) And CInt(lblKonkaiYM.Text.Substring(5, 2)) <= 3 Then
            targetYYYY = CInt(targetYYYY) - 1
        End If

        'SQLSV��蒊�o
        Dim sql As String = ""
        Dim iRecCnt As Integer = 0
        sql = ""
        sql = sql & N & "select "
        sql = sql & N & " [�i���敪�P] "
        sql = sql & N & ",[�i���敪�Q] "
        sql = sql & N & ",[�i���敪�R] "
        sql = sql & N & ",[�i���敪�S] "
        sql = sql & N & ",[�i���敪�T] "
        sql = sql & N & ",[�i���敪�U] "
        sql = sql & N & ",[�i���敪�V] "
        sql = sql & N & ",[�i���敪�W] "
        sql = sql & N & ",[�����敪�P] "
        sql = sql & N & ",[�����敪�Q] "
        sql = sql & N & ",[�����敪�R] "
        sql = sql & N & ",[�����敪�S] "
        sql = sql & N & ",[�����敪�T] "
        sql = sql & N & ",[�����敪�U] "
        sql = sql & N & ",[���Ӑ�ʑ��v�Z�o�p���o�敪] "
        sql = sql & N & ",[����f�[�^�敪] "
        sql = sql & N & ",[���ɑq��] "
        sql = sql & N & ",[�o�ɑq��] "
        sql = sql & N & ",[���i�敪] "
        sql = sql & N & ",[�d�l] "
        sql = sql & N & ",[�i��] "
        sql = sql & N & ",[���S��] "
        sql = sql & N & ",[�T�C�Y] "
        sql = sql & N & ",[�F] "
        sql = sql & N & ",[�\���P] "
        sql = sql & N & ",[�i�햼] "
        sql = sql & N & ",[�T�C�Y��] "
        sql = sql & N & ",[�F�x������] "
        sql = sql & N & ",[�o�א�] "
        sql = sql & N & ",[�P��] "
        sql = sql & N & ",[���̋敪] "
        sql = sql & N & ",[�P��] "
        sql = sql & N & ",[���z] "
        sql = sql & N & ",[�����敪] "
        sql = sql & N & ",[�����] "
        sql = sql & N & ",[�x�X�R�[�h] "
        sql = sql & N & ",[����於��] "
        sql = sql & N & ",[�[���敪] "
        sql = sql & N & ",[�[��] "
        sql = sql & N & ",[�[���R�[�h] "
        sql = sql & N & ",[�[������] "
        sql = sql & N & ",[�`�[�m�n] "
        sql = sql & N & ",[�`�[�s�m�n] "
        sql = sql & N & ",[������] "
        sql = sql & N & ",[�ؒf�𒷌�] "
        sql = sql & N & ",[��] "
        sql = sql & N & ",[��] "
        sql = sql & N & ",[����敪] "
        sql = sql & N & ",[���������敪] "
        sql = sql & N & ",[�݌ɊǗ��敪] "
        sql = sql & N & ",[���B�敪] "
        sql = sql & N & ",[�����敪] "
        sql = sql & N & ",[��[�敪] "
        sql = sql & N & ",[�o�ד�] "
        sql = sql & N & ",[����^�C�v�R�[�h] "
        sql = sql & N & ",[���] "
        sql = sql & N & ",[�^���敪] "
        sql = sql & N & ",[�^����ЃR�[�h] "
        sql = sql & N & ",[�^���敪] "
        sql = sql & N & ",[���������z] "
        sql = sql & N & ",[����] "
        sql = sql & N & ",[�󒍓`�[�m�n] "
        sql = sql & N & ",[�󒍍s�m�n] "
        sql = sql & N & ",[�󒍌���] "
        sql = sql & N & ",[�[��] "
        sql = sql & N & ",[�����敪] "
        sql = sql & N & ",[����] "
        sql = sql & N & ",[��v�����R�[�h] "
        sql = sql & N & ",[���v����] "
        sql = sql & N & ",[���x�[�X] "
        sql = sql & N & ",[���v�z] "
        sql = sql & N & ",[�����m�n] "
        sql = sql & N & ",[�_��敪] "
        sql = sql & N & ",[�_��] "
        sql = sql & N & ",[�x���敪] "
        sql = sql & N & ",[�̔��萔����] "
        sql = sql & N & ",[�ЊO������] "
        sql = sql & N & ",[�g�m�n] "
        sql = sql & N & ",[�����m�n] "
        sql = sql & N & ",[�T�Z�d��] "
        sql = sql & N & ",[�ړ��T�C��] "
        sql = sql & N & ",[���Վ��敪] "
        sql = sql & N & ",[���s�S��] "
        sql = sql & N & ",[�[���@�m�n] "
        sql = sql & N & ",[���ƕ��敪] "
        sql = sql & N & ",[�Ȗڋ敪] "
        sql = sql & N & ",[�\���Q] "
        sql = sql & N & ",[�b�t�x�[�X�W��] "
        sql = sql & N & ",[�b�t�x�[�X�K�p] "
        sql = sql & N & ",[�����R�X�g�v] "
        sql = sql & N & ",[��ޗ���] "
        sql = sql & N & ",[�����ޔ�] "
        sql = sql & N & ",[���H��] "
        sql = sql & N & ",[�x�[�X���z] "
        sql = sql & N & ",[�����ޕ␳�W��] "
        sql = sql & N & ",[�����ޕ␳�z] "
        sql = sql & N & ",[����] "
        sql = sql & N & ",[���R�A��] "
        sql = sql & N & ",[���R�l�N�^��] "
        sql = sql & N & ",[�x�X��] "
        sql = sql & N & ",[�k���{���z] "
        sql = sql & N & ",[�k���{�P��] "
        sql = sql & N & ",[�k���{���v] "
        sql = sql & N & ",[�k���{�w���P��] "
        sql = sql & N & ",[�ϓ��̔��] "
        sql = sql & N & ",[�ϓ��̔���z] "
        sql = sql & N & ",[����R�[�h] "
        sql = sql & N & ",[�����R�[�h] "
        sql = sql & N & ",[���v�R�[�h] "
        sql = sql & N & ",[�r�c�b���v] "
        sql = sql & N & ",[�R�X�g�X���C�h�z] "
        sql = sql & N & ",[�r�c�b�w���P��] "
        sql = sql & N & ",[�\���R] "
        sql = sql & N & ",[�����敪�V] "
        sql = sql & N & ",[�����敪�W] "
        '-->2011.01.16 add by takagi #���捞���эĎ擾
        'sql = sql & N & "from T_�[�i���f�[�^_" & targetYYYY & "�݌v "
        'sql = sql & N & "where substring(convert(varchar,[�o�ד�]),1,6)='" & lblKonkaiYM.Text.Replace("/", "") & "' "
        If "".Equals(prmNonImportsHinmeiCDs) Then
            sql = sql & N & "from T_�[�i���f�[�^_" & targetYYYY & "�݌v "
            sql = sql & N & "where substring(convert(varchar,[�o�ד�]),1,6)='" & lblKonkaiYM.Text.Replace("/", "") & "' "
        Else
            sql = sql & N & "from (select * from T_�[�i���f�[�^_" & targetYYYY & "�݌v union all select * from T_�[�i���f�[�^_" & (CInt(targetYYYY) - 1) & "�݌v) as uniTbl "
            sql = sql & N & "where (substring((�d�l+' '),1,2) + �i�� + ���S�� + �T�C�Y + �F) in (" & prmNonImportsHinmeiCDs & ")"
        End If
        '<--2011.01.16 add by takagi #���捞���эĎ擾
        Dim ds As DataSet = _nouhinshoDb.selectDB(sql, RS, iRecCnt)     '�[�i���f�[�^�p�R�l�N�V������Select���s

        '-->2011.01.16 add by takagi #���捞���эĎ擾
        If Not "".Equals(prmNonImportsHinmeiCDs) Then
            prmRefPb.maxVal = iRecCnt
        End If
        '<--2011.01.16 add by takagi #���捞���эĎ擾

        'ORACLE�փC���T�[�g
        Dim sqlBuf As System.Text.StringBuilder = New System.Text.StringBuilder
        Dim sqlBufF As System.Text.StringBuilder = New System.Text.StringBuilder
        sqlBufF.Append(N & "insert into ZG330B_W10 ")
        sqlBufF.Append(N & "( ")
        sqlBufF.Append(N & " HINMEIKBN1  ")                           '�i���敪�P
        sqlBufF.Append(N & ",HINMEIKBN2  ")                           '�i���敪�Q
        sqlBufF.Append(N & ",HINMEIKBN3  ")                           '�i���敪�R
        sqlBufF.Append(N & ",HINMEIKBN4  ")                           '�i���敪�S
        sqlBufF.Append(N & ",HINMEIKBN5  ")                           '�i���敪�T
        sqlBufF.Append(N & ",HINMEIKBN6  ")                           '�i���敪�U
        sqlBufF.Append(N & ",HINMEIKBN7  ")                           '�i���敪�V
        sqlBufF.Append(N & ",HINMEIKBN8  ")                           '�i���敪�W
        sqlBufF.Append(N & ",TORIHIKISAKIKBN1  ")                     '�����敪�P
        sqlBufF.Append(N & ",TORIHIKISAKIKBN2  ")                     '�����敪�Q
        sqlBufF.Append(N & ",TORIHIKISAKIKBN3  ")                     '�����敪�R
        sqlBufF.Append(N & ",TORIHIKISAKIKBN4  ")                     '�����敪�S
        sqlBufF.Append(N & ",TORIHIKISAKIKBN5  ")                     '�����敪�T
        sqlBufF.Append(N & ",TORIHIKISAKIKBN6  ")                     '�����敪�U
        sqlBufF.Append(N & ",TO_SONEKI_KBN  ")                        '���Ӑ�ʑ��v�Z�o�p���o�敪
        sqlBufF.Append(N & ",TORIHIKIKBN  ")                          '����f�[�^�敪
        sqlBufF.Append(N & ",NYUKO  ")                                '���ɑq��
        sqlBufF.Append(N & ",SYUKO  ")                                '�o�ɑq��
        sqlBufF.Append(N & ",SYOHINKBN  ")                            '���i�敪
        sqlBufF.Append(N & ",SIYO  ")                                 '�d�l
        sqlBufF.Append(N & ",HINSYU  ")                               '�i��
        sqlBufF.Append(N & ",SENSHIN  ")                              '���S��
        sqlBufF.Append(N & ",SIZECD  ")                               '�T�C�Y
        sqlBufF.Append(N & ",IRO  ")                                  '�F
        sqlBufF.Append(N & ",YOBI1  ")                                '�\���P
        sqlBufF.Append(N & ",HINSYUMEI  ")                            '�i�햼
        sqlBufF.Append(N & ",SIZEMEI  ")                              '�T�C�Y��
        sqlBufF.Append(N & ",IROMEI  ")                               '�F�x������
        sqlBufF.Append(N & ",SYUKANUM  ")                             '�o�א�
        sqlBufF.Append(N & ",UNIT  ")                                 '�P��
        sqlBufF.Append(N & ",DOUTAIKBN  ")                            '���̋敪
        sqlBufF.Append(N & ",TANKA  ")                                '�P��
        sqlBufF.Append(N & ",KINGAKU  ")                              '���z
        sqlBufF.Append(N & ",TORIHIKISAKIKBN  ")                      '�����敪
        sqlBufF.Append(N & ",TORIHIKISAKI  ")                         '�����
        sqlBufF.Append(N & ",SHITENCD  ")                             '�x�X�R�[�h
        sqlBufF.Append(N & ",TORIHIKISAKIMEI  ")                      '����於��
        sqlBufF.Append(N & ",NOUSHOKBN  ")                            '�[���敪
        sqlBufF.Append(N & ",NOUSHO  ")                               '�[��
        sqlBufF.Append(N & ",NOUHINCD  ")                             '�[���R�[�h
        sqlBufF.Append(N & ",NOUSYOMEI  ")                            '�[������
        sqlBufF.Append(N & ",DNPYOUNO  ")                             '�`�[�m�n
        sqlBufF.Append(N & ",DENPYOUGYONO  ")                         '�`�[�s�m�n
        sqlBufF.Append(N & ",SYORIBI  ")                              '������
        sqlBufF.Append(N & ",SETSUDANMOTO  ")                         '�ؒf�𒷌�
        sqlBufF.Append(N & ",JYOCHO  ")                               '��
        sqlBufF.Append(N & ",KOSU  ")                                 '��
        sqlBufF.Append(N & ",TYOUBOKBN  ")                            '����敪
        sqlBufF.Append(N & ",ZAIMUGENKAKBN  ")                        '���������敪
        sqlBufF.Append(N & ",ZAIKOKANRIKBN  ")                        '�݌ɊǗ��敪
        sqlBufF.Append(N & ",CHOTATUKBN  ")                           '���B�敪
        sqlBufF.Append(N & ",KANRYOKBN  ")                            '�����敪
        sqlBufF.Append(N & ",TEIHAKBN  ")                             '��[�敪
        sqlBufF.Append(N & ",SYUKKABI  ")                             '�o�ד�
        sqlBufF.Append(N & ",KONPOUTYPE  ")                           '����^�C�v�R�[�h
        sqlBufF.Append(N & ",KONPONUM  ")                             '���
        sqlBufF.Append(N & ",UNSOKBN  ")                              '�^���敪
        sqlBufF.Append(N & ",UNSOKAISHAKBN  ")                        '�^����ЃR�[�h
        sqlBufF.Append(N & ",UNCHINKBN  ")                            '�^���敪
        sqlBufF.Append(N & ",KARIKENSHUKINGAKU  ")                    '���������z
        sqlBufF.Append(N & ",BUMON  ")                                '����
        sqlBufF.Append(N & ",JYUCHUDENPYOUNO  ")                      '�󒍓`�[�m�n
        sqlBufF.Append(N & ",JYUCHGYONO  ")                           '�󒍍s�m�n
        sqlBufF.Append(N & ",JYUCHUGAPPI  ")                          '�󒍌���
        sqlBufF.Append(N & ",NOUKI  ")                                '�[��
        sqlBufF.Append(N & ",HIKIATEKBN  ")                           '�����敪
        sqlBufF.Append(N & ",KENMEI  ")                               '����
        sqlBufF.Append(N & ",SHUYOTORIHIKISAKI  ")                    '��v�����R�[�h
        sqlBufF.Append(N & ",JYUYOBUMON  ")                           '���v����
        sqlBufF.Append(N & ",DOUBASE  ")                              '���x�[�X
        sqlBufF.Append(N & ",RIEKI  ")                                '���v�z
        sqlBufF.Append(N & ",CHUMONNO  ")                             '�����m�n
        sqlBufF.Append(N & ",KEIYAKUKBN  ")                           '�_��敪
        sqlBufF.Append(N & ",KEIYAKUTSUKI  ")                         '�_��
        sqlBufF.Append(N & ",SHIHARAIKBN  ")                          '�x���敪
        sqlBufF.Append(N & ",HANBAITESURYORITSU  ")                   '�̔��萔����
        sqlBufF.Append(N & ",MOME  ")                                 '�ЊO������
        sqlBufF.Append(N & ",WAKUNO  ")                               '�g�m�n
        sqlBufF.Append(N & ",HIKIATENO  ")                            '�����m�n
        sqlBufF.Append(N & ",GAISANJYURYO  ")                         '�T�Z�d��
        sqlBufF.Append(N & ",KADOUSAIN  ")                            '�ړ��T�C��
        sqlBufF.Append(N & ",TEIREIRINJIKBN  ")                       '���Վ��敪
        sqlBufF.Append(N & ",HAKKOUTANTO  ")                          '���s�S��
        sqlBufF.Append(N & ",TANMATSUNO  ")                           '�[���@�m�n
        sqlBufF.Append(N & ",JIGYOKBN  ")                             '���ƕ��敪
        sqlBufF.Append(N & ",KAMOKUKBN  ")                            '�Ȗڋ敪
        sqlBufF.Append(N & ",YOBI2  ")                                '�\���Q
        sqlBufF.Append(N & ",CUBASE_ST  ")                            '�b�t�x�[�X�W��
        sqlBufF.Append(N & ",CUBASE_TEKIYO  ")                        '�b�t�x�[�X�K�p
        sqlBufF.Append(N & ",SEIZOCOST  ")                            '�����R�X�g�v
        sqlBufF.Append(N & ",SYUZAIRYOHI  ")                          '��ޗ���
        sqlBufF.Append(N & ",HUKUZAIRYOHI  ")                         '�����ޔ�
        sqlBufF.Append(N & ",KAKOUHI  ")                              '���H��
        sqlBufF.Append(N & ",BASESAGAKU  ")                           '�x�[�X���z
        sqlBufF.Append(N & ",HUKUSHIZAIHOSEI_RATE  ")                 '�����ޕ␳�W��
        sqlBufF.Append(N & ",HUKUSHIZAIHOSEI_GAKU  ")                 '�����ޕ␳�z
        sqlBufF.Append(N & ",DOURYOU  ")                              '����
        sqlBufF.Append(N & ",HIKARI_CORE  ")                          '���R�A��
        sqlBufF.Append(N & ",HIKARI_CONECTOR  ")                      '���R�l�N�^��
        sqlBufF.Append(N & ",SHITENMEI  ")                            '�x�X��
        sqlBufF.Append(N & ",KITANIHONKINGAKU  ")                     '�k���{���z
        sqlBufF.Append(N & ",KITANIHONTANKA  ")                       '�k���{�P��
        sqlBufF.Append(N & ",KITANIHONRIEKI  ")                       '�k���{���v
        sqlBufF.Append(N & ",KITANIHONKOUNYUTANKA  ")                 '�k���{�w���P��
        sqlBufF.Append(N & ",HENDOUHANBAIHIRITSU  ")                  '�ϓ��̔��
        sqlBufF.Append(N & ",HENDOUHANBAIHISAGAKU  ")                 '�ϓ��̔���z
        sqlBufF.Append(N & ",BUMONCD  ")                              '����R�[�h
        sqlBufF.Append(N & ",TOHIKISAKICD  ")                         '�����R�[�h
        sqlBufF.Append(N & ",TOUKEICD  ")                             '���v�R�[�h
        sqlBufF.Append(N & ",SDCRIEKI  ")                             '�r�c�b���v
        sqlBufF.Append(N & ",COSTSURAIDO  ")                          '�R�X�g�X���C�h�z
        sqlBufF.Append(N & ",SDCTANKA  ")                             '�r�c�b�w���P��
        sqlBufF.Append(N & ",YOBI3  ")                                '�\���R
        sqlBufF.Append(N & ",TORIHIKIKBN7  ")                         '�����敪�V
        sqlBufF.Append(N & ",TORIHIKIKBN8  ")                         '�����敪�W
        sqlBufF.Append(N & ",UPDNAME   ")                             '�[��ID
        sqlBufF.Append(N & ",UPDDATE  ")                              '�X�V����
        sqlBufF.Append(N & ")values( ")
        With ds.Tables(RS)
            For i As Integer = 0 To iRecCnt - 1
                sqlBuf.Remove(0, sqlBuf.Length)
                sqlBuf.Append(N & "  " & convNullStr(.Rows(i)("�i���敪�P")) & " ")                  'HINMEIKBN1 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�i���敪�Q")) & " ")                  'HINMEIKBN2 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�i���敪�R")) & " ")                  'HINMEIKBN3 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�i���敪�S")) & " ")                  'HINMEIKBN4 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�i���敪�T")) & " ")                  'HINMEIKBN5 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�i���敪�U")) & " ")                  'HINMEIKBN6 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�i���敪�V")) & " ")                  'HINMEIKBN7 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�i���敪�W")) & " ")                  'HINMEIKBN8 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�����敪�P")) & " ")                'TORIHIKISAKIKBN1 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�����敪�Q")) & " ")                'TORIHIKISAKIKBN2 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�����敪�R")) & " ")                'TORIHIKISAKIKBN3 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�����敪�S")) & " ")                'TORIHIKISAKIKBN4 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�����敪�T")) & " ")                'TORIHIKISAKIKBN5 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�����敪�U")) & " ")                'TORIHIKISAKIKBN6 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("���Ӑ�ʑ��v�Z�o�p���o�敪")) & " ")  'TO_SONEKI_KBN 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("����f�[�^�敪")) & " ")              'TORIHIKIKBN 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("���ɑq��")) & " ")                    'NYUKO 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�o�ɑq��")) & " ")                    'SYUKO 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("���i�敪")) & " ")                    'SYOHINKBN 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�d�l")) & " ")                        'SIYO 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�i��")) & " ")                        'HINSYU 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("���S��")) & " ")                      'SENSHIN 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�T�C�Y")) & " ")                      'SIZECD 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�F")) & " ")                          'IRO 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�\���P")) & " ")                      'YOBI1 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�i�햼")) & " ")                      'HINSYUMEI 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�T�C�Y��")) & " ")                    'SIZEMEI 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�F�x������")) & " ")                  'IROMEI 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�o�א�")) & " ")                      'SYUKANUM 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�P��")) & " ")                        'UNIT 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("���̋敪")) & " ")                    'DOUTAIKBN 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�P��")) & " ")                        'TANKA 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("���z")) & " ")                        'KINGAKU 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�����敪")) & " ")                  'TORIHIKISAKIKBN 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�����")) & " ")                      'TORIHIKISAKI 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�x�X�R�[�h")) & " ")                  'SHITENCD 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("����於��")) & " ")                  'TORIHIKISAKIMEI 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�[���敪")) & " ")                    'NOUSHOKBN 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�[��")) & " ")                        'NOUSHO 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�[���R�[�h")) & " ")                  'NOUHINCD 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�[������")) & " ")                    'NOUSYOMEI 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�`�[�m�n")) & " ")                    'DNPYOUNO 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�`�[�s�m�n")) & " ")                  'DENPYOUGYONO 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("������")) & " ")                      'SYORIBI 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�ؒf�𒷌�")) & " ")                  'SETSUDANMOTO 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("��")) & " ")                        'JYOCHO 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("��")) & " ")                        'KOSU 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("����敪")) & " ")                    'TYOUBOKBN 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("���������敪")) & " ")                'ZAIMUGENKAKBN 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�݌ɊǗ��敪")) & " ")                'ZAIKOKANRIKBN 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("���B�敪")) & " ")                    'CHOTATUKBN 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�����敪")) & " ")                    'KANRYOKBN 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("��[�敪")) & " ")                    'TEIHAKBN 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�o�ד�")) & " ")                      'SYUKKABI 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("����^�C�v�R�[�h")) & " ")            'KONPOUTYPE 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("���")) & " ")                      'KONPONUM 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�^���敪")) & " ")                    'UNSOKBN 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�^����ЃR�[�h")) & " ")              'UNSOKAISHAKBN 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�^���敪")) & " ")                    'UNCHINKBN 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("���������z")) & " ")                  'KARIKENSHUKINGAKU 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("����")) & " ")                        'BUMON 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�󒍓`�[�m�n")) & " ")                'JYUCHUDENPYOUNO 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�󒍍s�m�n")) & " ")                  'JYUCHGYONO 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�󒍌���")) & " ")                    'JYUCHUGAPPI 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�[��")) & " ")                        'NOUKI 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�����敪")) & " ")                    'HIKIATEKBN 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("����")) & " ")                        'KENMEI 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("��v�����R�[�h")) & " ")            'SHUYOTORIHIKISAKI 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("���v����")) & " ")                    'JYUYOBUMON 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("���x�[�X")) & " ")                    'DOUBASE 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("���v�z")) & " ")                      'RIEKI 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�����m�n")) & " ")                    'CHUMONNO 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�_��敪")) & " ")                    'KEIYAKUKBN 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�_��")) & " ")                      'KEIYAKUTSUKI 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�x���敪")) & " ")                    'SHIHARAIKBN 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�̔��萔����")) & " ")                'HANBAITESURYORITSU 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�ЊO������")) & " ")                  'MOME 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�g�m�n")) & " ")                      'WAKUNO 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�����m�n")) & " ")                    'HIKIATENO 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�T�Z�d��")) & " ")                    'GAISANJYURYO 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�ړ��T�C��")) & " ")                  'KADOUSAIN 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("���Վ��敪")) & " ")                'TEIREIRINJIKBN 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("���s�S��")) & " ")                    'HAKKOUTANTO 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�[���@�m�n")) & " ")                  'TANMATSUNO 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("���ƕ��敪")) & " ")                  'JIGYOKBN 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�Ȗڋ敪")) & " ")                    'KAMOKUKBN 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�\���Q")) & " ")                      'YOBI2 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�b�t�x�[�X�W��")) & " ")              'CUBASE_ST 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�b�t�x�[�X�K�p")) & " ")              'CUBASE_TEKIYO 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�����R�X�g�v")) & " ")                'SEIZOCOST 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("��ޗ���")) & " ")                    'SYUZAIRYOHI 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�����ޔ�")) & " ")                    'HUKUZAIRYOHI 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("���H��")) & " ")                      'KAKOUHI 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�x�[�X���z")) & " ")                  'BASESAGAKU 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�����ޕ␳�W��")) & " ")              'HUKUSHIZAIHOSEI_RATE 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�����ޕ␳�z")) & " ")                'HUKUSHIZAIHOSEI_GAKU 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("����")) & " ")                        'DOURYOU 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("���R�A��")) & " ")                    'HIKARI_CORE 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("���R�l�N�^��")) & " ")                'HIKARI_CONECTOR 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�x�X��")) & " ")                      'SHITENMEI 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�k���{���z")) & " ")                  'KITANIHONKINGAKU 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�k���{�P��")) & " ")                  'KITANIHONTANKA 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�k���{���v")) & " ")                  'KITANIHONRIEKI 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�k���{�w���P��")) & " ")              'KITANIHONKOUNYUTANKA 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�ϓ��̔��")) & " ")                'HENDOUHANBAIHIRITSU 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�ϓ��̔���z")) & " ")                'HENDOUHANBAIHISAGAKU 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("����R�[�h")) & " ")                  'BUMONCD 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�����R�[�h")) & " ")                'TOHIKISAKICD 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("���v�R�[�h")) & " ")                  'TOUKEICD 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�r�c�b���v")) & " ")                  'SDCRIEKI 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�R�X�g�X���C�h�z")) & " ")            'COSTSURAIDO 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("�r�c�b�w���P��")) & " ")              'SDCTANKA 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�\���R")) & " ")                      'YOBI3 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�����敪�V")) & " ")                'TORIHIKIKBN7 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("�����敪�W")) & " ")                'TORIHIKIKBN8 
                sqlBuf.Append(N & ", " & convNullStr(UtilClass.getComputerName()) & " ")             'UPDNAME  
                sqlBuf.Append(N & ", " & sysdate & " ")                                              'UPDDATE 
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