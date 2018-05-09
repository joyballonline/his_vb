'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���j���j���[���
'    �i�t�H�[��ID�jZC110M_Menu
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ����        2010/10/15                 �V�K              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls

Public Class ZC110M_Menu
    Inherits System.Windows.Forms.Form

    '-------------------------------------------------------------------------------
    '   �\���̒�`
    '-------------------------------------------------------------------------------
    Private Structure UpdatableType
        Public updFlgSyokisettei As Boolean         '�����ݒ�
        Public updFlgSetteitisyuusei As Boolean     '��]�o�����C��
        Public updFlgTDTorikomi As Boolean          '��z���ް��捞
        Public updFlgNDTorikomi As Boolean          '���ɍ��ް��捞
        Public updFlgSDSyuusei As Boolean           '���Y���ް��C��
        Public updFlgSeisanKakutei As Boolean       '���Y�ʊm��
        Public updFlgHKNyuryoku As Boolean          '�i��ʌv�����
        Public updFlgKKNyuroku As Boolean           '�ʌv�����
        Public updFlgHJTorikomi As Boolean          '�̔����ю捞
        Public updFlgSyuukeiTenkai As Boolean       '�̔��v��W�v�W�J
        Public updFlgHKSyuusei As Boolean           '�̔��v��ʏC��
        Public updFlgSKakutei As Boolean            '�̔��v��m��
        Public updFlgZaikoTorikomi As Boolean       '�݌Ɏ��ю捞
        Public updFlgSHZTorikomi As Boolean         '���Y�̔��݌Ɏ捞
        Public updFlgSKSyuusei As Boolean           '���Y�v�搔�ʏC��
        Public updFlgKakutei As Boolean             '���Y�v��m��
        Public updFlgTDSakusei As Boolean           '��z�ް��쐬
        Public updFlgTDSyuusei As Boolean           '��z�ް��C���E�o��
        Public updFlgTDSousin As Boolean            '��z�ް��쐬(���Y�Ǘ����ё��M�p)
        Public updFlgFYamadumi As Boolean           '���׎R�σf�[�^�捞
        Public updFlgKKakunin As Boolean            '���׎R�ϏW�v���ʊm�F
        Public updFlgSTDB As Boolean                '�����zDB�o�^
        Public updFlgSinki As Boolean               '�V�K�o�^
        Public updFlgSyuusei As Boolean             '�C���EEXCEL�o��
        Public updFlgSakujo As Boolean              '�폜
        Public updFlgKExcel As Boolean              '�v��Ώەi�ꗗ�\���
        Public updFlgABC As Boolean                 'ABC����
        Public updFlgHMstMente As Boolean           '�i��敪�}�X�^�����e
        Public updFlgHanyoMst As Boolean            '�ėp�}�X�^�����e
        Public updFlgSNouryokuMst As Boolean        '���Y�\�̓}�X�^�����e
        Public updFlgGRenkei As Boolean             '�O���V�X�e���A�g
    End Structure

    '-------------------------------------------------------------------------------
    '   �萔��`
    '-------------------------------------------------------------------------------
    Private Const N As String = ControlChars.NewLine            '���s����
    Private Const RS As String = "RecSet"                       '���R�[�h�Z�b�g�e�[�u��
    Public Const NON_EXECUTE As String = "- - -"

    '-------------------------------------------------------------------------------
    '�v��Ώەi�ꗗ�o�͗p�萔
    '-------------------------------------------------------------------------------
    Private Const RS2 As String = "RecSetM12ForxLS"             '���R�[�h�Z�b�g�e�[�u��

    '�ėp�}�X�^�Œ�L�[
    Private Const M01KOTEI_JUYOUSAKI As String = "01"           '���v��

    '�ėp�}�X�^�σL�[
    Private Const M01KAHEN_KURIKAESI As String = "9"            '���J�ԕi

    'M11�G�C���A�X
    Private Const COLDT_HINMEICD As String = "dtHinmeiCD"       '�i���R�[�h
    Private Const COLDT_HINMEI As String = "dtHinmei"           '�i��
    Private Const COLDT_LOTTYOU As String = "dtLottyou"         '�W�����b�g��
    Private Const COLDT_TANTYOU As String = "dtSeisakuTantyou"  '�P��
    Private Const COLDT_JOSU As String = "dtJosu"               '��
    Private Const COLDT_KIJUNTUKISU As String = "dtKijunTuki"   '�����
    Private Const COLDT_ABC As String = "dtABC"                 'ABC�敪

    'M12�G�C���A�X
    Private Const COLDT_M12KHINMEICD As String = "KHINMEICD"    '�v��i���R�[�h
    Private Const COLDT_M12HINMEICD As String = "HINMEICD"      '���i���R�[�h

    'EXCEL
    Private Const START_PRINT_ROW As Integer = 7                'EXCEL�o�͊J�n�s��
    Private Const START_PRINT_COL As Integer = 1                'EXCEL�o�͊J�n��
    Private Const XLSSHEETNM_HINSYU As String = "Ver01-00"      '�v��Ώ��i�ꗗ���`�V�[�g��
    Private Const XLS_TITLE As String = "�v��Ώەi�ꗗ�\"      'EXCEL�^�C�g��

    '�v��Ώەi�ꗗ�\��PGID
    Private Const ZM130P_PGID As String = "ZM130P"

    '-------------------------------------------------------------------------------
    '   �ϐ���`
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private updFlg As UpdatableType

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
    '�R���X�g���N�^�@���j���[����Ă΂��
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf)
        Call Me.New()

        '��������
        _msgHd = prmRefMsgHd                                                'MSG�n���h���̐ݒ�
        _db = prmRefDbHd                                                    'DB�n���h���̐ݒ�
        StartPosition = FormStartPosition.CenterScreen                      '��ʒ����\��

    End Sub

    '-------------------------------------------------------------------------------
    '�@�t�H�[�����[�h�C�x���g
    '-------------------------------------------------------------------------------
    Private Sub ZC110M_Menu_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            '�`��֌W�̐ݒ�
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           '�T�C�Y���ύX���ꂽ�Ƃ��ɁA�R���g���[�����R���g���[�����̂��ĕ`�悷�邩�ǂ����������l��ݒ�
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '�`��̓o�b�t�@�Ŏ��s����A������ɁA���ʂ���ʂɏo�͂����悤�ݒ�
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  '�R���g���[���́A��ʂɒ��ڂł͂Ȃ��A�܂��o�b�t�@�ɕ`�悳��܂��B����ɂ��A�������}���邱�Ƃ��ł��܂��B
            Me.SetStyle(ControlStyles.UserPaint, True)              '�R���g���[���́A�I�y���[�e�B���O �V�X�e���ɂ���Ăł͂Ȃ��A�Ǝ��ɕ`�悳���悤�ݒ�
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   '�R���g���[���̓E�B���h�E ���b�Z�[�W WM_ERASEBKGND �𖳎�����悤�ɐݒ�

            '��ʃ^�C�g���ݒ�
            Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr '�^�C�g���I�v�V�����\��

            '�o�[�W�����\�L
            lblVersion.Text = UtilClass.getAppVersion(StartUp.assembly)

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
    '   �i�����T�v�j��ʍ��ڂ������ݒ肷��
    '   �����̓p�����^  �F�Ȃ�
    '   ���o�̓p�����^  �F�Ȃ�
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub initForm()

        '�v��N��/�����N���\��
        Call getKeikakuKanriTblRec(lblKeikaku.Text, lblSyori.Text)

        '���s�����\��
        With updFlg
            '�P�V�[�g��-----
            Call getExecuteDt(lblSyokisettei, btnSyokisettei, .updFlgSyokisettei)              '�����ݒ�
            Call getExecuteDt(lblSetteitisyuusei, btnSetteitisyuusei, .updFlgSetteitisyuusei)  '��]�o�����C��
            Call getExecuteDt(lblTDTorikomi, btnTDTorikomi, .updFlgTDTorikomi)                 '��z���ް��捞
            Call getExecuteDt(lblNDTorikomi, btnNDTorikomi, .updFlgNDTorikomi)                 '���ɍ��ް��捞
            Call getExecuteDt(lblSDSyuusei, btnSDSyuusei, .updFlgSDSyuusei)                    '���Y���ް��C��
            Call getExecuteDt(lblSeisanKakutei, btnSeisanKakutei, .updFlgSeisanKakutei)        '���Y�ʊm��
            Call getExecuteDt(lblHKNyuryoku, btnHKNyuryoku, .updFlgHKNyuryoku)                 '�i��ʌv�����
            Call getExecuteDt(lblKKNyuroku, btnKKNyuroku, .updFlgKKNyuroku)                    '�ʌv�����
            Call getExecuteDt(lblHJTorikomi, btnHJTorikomi, .updFlgHJTorikomi)                 '�̔����ю捞
            Call getExecuteDt(lblSyuukeiTenkai, btnSyuukeiTenkai, .updFlgSyuukeiTenkai)        '�̔��v��W�v�W�J
            Call getExecuteDt(lblHKSyuusei, btnHKSyuusei, .updFlgHKSyuusei)                    '�̔��v��ʏC��
            Call getExecuteDt(lblSKakutei, btnSKakutei, .updFlgSKakutei)                       '�̔��v��m��
            Call getExecuteDt(lblZaikoTorikomi, btnZaikoTorikomi, .updFlgZaikoTorikomi)        '�݌Ɏ��ю捞
            Call getExecuteDt(lblSHZTorikomi, btnSHZTorikomi, .updFlgSHZTorikomi)              '���Y�̔��݌Ɏ捞
            Call getExecuteDt(lblSKSyuusei, btnSKSyuusei, .updFlgSKSyuusei)                    '���Y�v�搔�ʏC��
            Call getExecuteDt(lblKakutei, btnKakutei, .updFlgKakutei)                          '���Y�v��m��
            Call getExecuteDt(lblTDSakusei, btnTDSakusei, .updFlgTDSakusei)                    '��z�ް��쐬
            Call getExecuteDt(lblTDSyuusei, btnTDSyuusei, .updFlgTDSyuusei)                    '��z�ް��C���E�o��
            Call getExecuteDt(lblTDSousin, btnTDSousin, .updFlgTDSousin)                       '��z�ް��쐬(���Y�Ǘ����ё��M�p								)
            Call getExecuteDt(lblFYamadumi, btnFYamadumi, .updFlgFYamadumi)                    '���׎R�σf�[�^�捞
            Call getExecuteDt(lblKKakunin, btnKKakunin, .updFlgKKakunin)                       '���׎R�ϏW�v���ʊm�F
            Call getExecuteDt(lblSTDB, btnSTDB, .updFlgSTDB)                                   '�����zDB�o�^

            '-->2010.12.17 add by takagi #16
            '����^�u���ł̍ŐV���t���擾����
            Dim latestDt As String = ""
            For Each ctl As Control In tabGeturei.Controls                               '����^�u���̃R���g���[�������[�v
                Dim l As System.Windows.Forms.Label = TryCast(ctl, System.Windows.Forms.Label)
                If l IsNot Nothing Then                                                         '�擾�R���g���[����Label�����f
                    If IsDate(l.Text) Then                                                      '���t�ȊO�̃R���g���[���̓X�L�b�v
                        If "".Equals(latestDt) Then latestDt = l.Text '                          ���񃋁[�v���͖������i�[
                        If CDate(latestDt) < CDate(l.Text) Then                                 '�ێ����Ă���ŐV���t�����V�������H
                            latestDt = l.Text                                                   '���̏ꍇ�͂��V�������t��ێ�
                        End If
                    End If
                End If
            Next
            For Each ctl As Control In tabGeturei.Controls                               '����^�u���̃R���g���[�������[�v
                Dim l As System.Windows.Forms.Label = TryCast(ctl, System.Windows.Forms.Label)
                If l IsNot Nothing Then                                                         '�擾�R���g���[����Label�����f
                    '���x��
                    If NON_EXECUTE.Equals(l.Text) OrElse IsDate(l.Text) Then
                        '���t���x��
                        Select Case True
                            Case NON_EXECUTE.Equals(l.Text) : l.ForeColor = Color.Black
                            Case CDate(latestDt) <= CDate(l.Text) : l.ForeColor = Color.Red
                            Case Else : l.ForeColor = Color.Blue
                        End Select
                    End If
                End If
            Next
            '<--2010.12.17 add by takagi #16

            '�Q�V�[�g��-----                                            
            '-->2010.12.02 upd by takagi
            'Call getExecuteDt(lblTSakujo, btnSinki, .updFlgSinki)                              '�V�K�o�^
            Call getExecuteDt(lblShinki, btnSinki, .updFlgSinki)                              '�V�K�o�^
            '<--2010.12.02 upd by takagi
            Call getExecuteDt(lblSyuusei, btnSyuusei, .updFlgSyuusei)                          '�C���EEXCEL�o��
            '-->2010.12.02 upd by takagi
            'Call getExecuteDt(lblTSakujo, btnSakujo, .updFlgSakujo)                            '�폜
            Call getExecuteDt(lblM11Del, btnSakujo, .updFlgSakujo)                            '�폜
            '<--2010.12.02 upd by takagi
            Call getExecuteDt(lblKExcel, btnKExcel, .updFlgKExcel)                             '�v��Ώەi�ꗗ�\���
            Call getExecuteDt(lblABC, btnABC, .updFlgABC)                                      'ABC����
            Call getExecuteDt(lblHMstMente, btnHMstMente, .updFlgHMstMente)                    '�i��敪�}�X�^�����e
            Call getExecuteDt(lblHanyoMst, btnHanyoMst, .updFlgHanyoMst)                       '�ėp�}�X�^�����e
            Call getExecuteDt(lblSNouryokuMst, btnSNouryokuMst, .updFlgSNouryokuMst)           '���Y�\�̓}�X�^�����e
            Call getExecuteDt(lblGRenkei, btnGRenkei, .updFlgGRenkei)                          '�O���V�X�e���A�g

        End With

    End Sub

    '-------------------------------------------------------------------------------
    '   �v��/�����N���̎擾
    '   �i�����T�v�j�v��Ǘ��s�a�k����v��N���Ə����N�����擾����
    '   �����̓p�����^  �F�Ȃ�
    '   ���o�̓p�����^  �FprmRefSyoriYM     �擾�Ϗ����N��
    '                     prmRefKeikakuYM   �擾�όv��N��
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub getKeikakuKanriTblRec(ByRef prmRefSyoriYM As String, ByRef prmRefKeikakuYM As String)
        Try
            '������
            prmRefSyoriYM = ""              '�����N��
            prmRefKeikakuYM = ""            '�v��N��

            '�v��Ǘ�TBL����
            Dim sql As String = ""
            Dim iRecCnt As Integer = 0
            sql = sql & N & " SELECT "
            sql = sql & N & "  SNENGETU "
            sql = sql & N & " ,KNENGETU "
            sql = sql & N & " FROM T01KEIKANRI "
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)
            If iRecCnt <> 1 Then Throw New UsrDefException("�v��Ǘ��s�a�k�̃��R�[�h�\�����s���ł��B(" & iRecCnt & "��)")

            '�ԋp�l�ҏW
            prmRefSyoriYM = _db.rmNullStr(ds.Tables(RS).Rows(0)("SNENGETU"))
            If Not "".Equals(prmRefSyoriYM) Then prmRefSyoriYM = prmRefSyoriYM.Substring(0, 4) & "/" & prmRefSyoriYM.Substring(4)
            prmRefKeikakuYM = _db.rmNullStr(ds.Tables(RS).Rows(0)("KNENGETU"))
            If Not "".Equals(prmRefKeikakuYM) Then prmRefKeikakuYM = prmRefKeikakuYM.Substring(0, 4) & "/" & prmRefKeikakuYM.Substring(4)

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   �v��/�����N���̎擾
    '   �i�����T�v�j��������TBL����@�\ID�̏����I���������擾����Ƌ��ɁA���̋@�\�̎g�p�ۂƍX�V�ۂ𔻒肷��
    '   �����̓p�����^  �FprmExecBtn        �����{�^��(Tag�v���p�e�B�ɊY������@�\ID��ݒ肵�Ă��邱��)
    '   ���o�̓p�����^  �FprmRefUpdatable   �X�V��(�Y���{�^�����N�������@�\���X�V������ۗL���邩�ۂ�
    '   �����\�b�h�߂�l�F�����I������
    '-------------------------------------------------------------------------------
    Private Sub getExecuteDt(ByRef prmRefLabel As Label, ByVal prmExecBtn As Button, ByRef prmRefUpdatable As Boolean)
        Dim ret As String = ""
        Try
            '�p�����^�`�F�b�N
            If "".Equals(prmExecBtn.Tag) Then Throw New UsrDefException("�����{�^����Tag�v���p�e�B�����ݒ�ł��B" & N & "Tag�v���p�e�B�ɋ@�\ID�𐳂����ݒ肵�Ă��������B")

            '������
            prmExecBtn.Enabled = False
            prmRefUpdatable = False

            '�����I�������̎擾-----
            Dim iRecCnt As Integer = 0
            Dim ds As DataSet = _db.selectDB("SELECT SDATEEND FROM T02SEIGYO WHERE PGID = '" & _db.rmSQ(prmExecBtn.Tag) & "'", RS, iRecCnt)
            If iRecCnt <> 1 Then Throw New UsrDefException("��������s�a�k�ɊY���@�\�̃��R�[�h��������܂���B(" & prmExecBtn.Tag & ")")

            '-->2010.12.17 chg by takagi #16
            'ret = _db.rmNullDate(ds.Tables(RS).Rows(0)("SDATEEND"), "yyyy/MM/dd HH:mm")
            ret = _db.rmNullDate(ds.Tables(RS).Rows(0)("SDATEEND"), "yyyy/MM/dd HH:mm:ss")
            '<--2010.12.17 chg by takagi #16
            If "".Equals(ret) Then ret = NON_EXECUTE


            '��s�W���u�̔���-------
            ds = _db.selectDB("SELECT BEFOREJOB_ID FROM M81BEFOREJOB WHERE PGID = '" & _db.rmSQ(prmExecBtn.Tag) & "'", RS, iRecCnt)
            If iRecCnt <= 0 Then
                '��s�W���u��`�Ȃ����N���\
                prmExecBtn.Enabled = True
            Else
                '��s�W���u��`����
                Dim wkCnt As Integer = 0
                Dim wkDs As DataSet = Nothing
                Dim wkPgId As String = ""
                Dim execCnt As Integer = 0
                For i As Integer = 0 To iRecCnt - 1
                    '��s�W���u���ƂɎ��s�ς�����
                    wkPgId = _db.rmNullStr(ds.Tables(RS).Rows(i)("BEFOREJOB_ID"))
                    wkDs = _db.selectDB("SELECT SDATEEND FROM T02SEIGYO WHERE PGID = '" & _db.rmSQ(wkPgId) & "'", RS, wkCnt)
                    If Not "".Equals(_db.rmNullDate(wkDs.Tables(RS).Rows(0)("SDATEEND"))) Then
                        execCnt += 1                            '������
                    End If
                Next
                If execCnt = iRecCnt Then
                    prmExecBtn.Enabled = True                   '�S�ď����ρ��N���\
                End If
            End If

            '�㑱�W���u�̔���-------
            If prmExecBtn.Enabled Then                          '�N���\�̏ꍇ�̂ݍX�V�ۂ𔻒f����
                ds = _db.selectDB("SELECT AFTERJOB_ID FROM M82AFTERJOB WHERE PGID = '" & _db.rmSQ(prmExecBtn.Tag) & "'", RS, iRecCnt)
                If iRecCnt <= 0 Then
                    '�㑱�W���u��`�Ȃ����X�V�\
                    prmRefUpdatable = True
                Else
                    '�㑱�W���u��`����
                    prmRefUpdatable = True
                    Dim wkCnt As Integer = 0
                    Dim wkDs As DataSet = Nothing
                    Dim wkPgId As String = ""
                    Dim execCnt As Integer = 0
                    For i As Integer = 0 To iRecCnt - 1
                        '�㑱�W���u���ƂɎ��s�ς�����
                        wkPgId = _db.rmNullStr(ds.Tables(RS).Rows(i)("AFTERJOB_ID"))
                        wkDs = _db.selectDB("SELECT SDATEEND FROM T02SEIGYO WHERE PGID = '" & _db.rmSQ(wkPgId) & "'", RS, wkCnt)
                        If Not "".Equals(_db.rmNullDate(wkDs.Tables(RS).Rows(0)("SDATEEND"))) Then
                            prmRefUpdatable = False             '������
                            Exit For                            '��ł������ς�����΂��̎��_�ōX�V�s��
                        End If
                    Next
                End If
            End If

            '�f�o�b�O�p�ɍX�V�������{�^���e�L�X�g�ɕ\��
            If StartUp.DebugMode Then
                lblDebugDsp.Visible = True
                prmExecBtn.Text = System.Text.RegularExpressions.Regex.Replace(prmExecBtn.Text, "\[.*\]", "") & "[" & prmRefUpdatable & "]"
            End If

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
        prmRefLabel.Text = ret
        '-->2010.12.17 add by takagi #16
        'If Not prmExecBtn.Enabled Then prmRefLabel.Text = NON_EXECUTE
        '<--2010.12.17 add by takagi #16
        If Not NON_EXECUTE.Equals(prmRefLabel.Text) Then prmRefLabel.ForeColor = Color.Blue

    End Sub

    '-------------------------------------------------------------------------------
    '   ��������TBL�X�V
    '   �i�����T�v�j��������TBL(T02SEIGYO)�̏������s������ݒ肷��
    '   �����̓p�����^  �FprmPgId       ��������TBL�̃��R�[�h����Ɏg�p����@�\ID
    '                     prmRunFlg     ���s���L�����Z�����������t���O(�L�����Z���F�m��������Ɏg�p)
    '                     [prmStartDt]  �����J�n����(�L�����Z�����͖��g�p)
    '                     [prmEndDt]    �����I������(�L�����Z�����͖��g�p)
    '   ���o�̓p�����^  �F�Ȃ�
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    Public Sub updateSeigyoTbl(ByVal prmPgId As String, ByVal prmRunFlg As Boolean, Optional ByVal prmStartDt As Date = Nothing, Optional ByVal prmEndDt As Date = Nothing)
        Try
            '�p�����^�`�F�b�N
            If prmRunFlg AndAlso (prmStartDt = #12:00:00 AM# OrElse prmEndDt = #12:00:00 AM#) Then
                Throw New UsrDefException("���s����(prmRunFlg=True)�̏ꍇ�͏����J�n����(prmStartDt)�E�����I������(prmEndDt)���K�{�ł��B")
            End If

            '����e�[�u���X�V
            Dim sql As String = ""
            Dim affectedRows As Integer = 0
            sql = sql & N & "UPDATE T02SEIGYO SET "
            If prmRunFlg Then
                sql = sql & N & "SDATESTART = TO_DATE('" & Format(prmStartDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS'), "
                sql = sql & N & "SDATEEND   = TO_DATE('" & Format(prmEndDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS'), "
            Else
                sql = sql & N & "SDATESTART = NULL, "
                sql = sql & N & "SDATEEND   = NULL, "
            End If
            sql = sql & N & "UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "', "
            sql = sql & N & "UPDDATE = TO_DATE('" & Format(Now, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "
            sql = sql & N & "WHERE PGID = '" & _db.rmSQ(prmPgId) & "' "
            _db.executeDB(sql, affectedRows)
            If affectedRows <= 0 Then
                Throw New UsrDefException("����TBL�̃��R�[�h�\�����s���ł��B(" & prmPgId & "�񑶍�)")
            End If

            '���j���[��ʍĕ`��
            Call initForm()

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�I���{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnd.Click

        '��ʃN���[�Y
        Me.Close()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�����ݒ�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnSyokisettei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSyokisettei.Click

        Dim openForm As ZG110B_Junbi = New ZG110B_Junbi(_msgHd, _db, Me, ZG110B_Junbi.BOOTMODE_INIT, updFlg.updFlgSyokisettei)      '��ʑJ��
        openForm.Show()                                                             '��ʕ\��
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@��]�o�����{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnSetteitisyuusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetteitisyuusei.Click

        Dim openForm As ZG110B_Junbi = New ZG110B_Junbi(_msgHd, _db, Me, ZG110B_Junbi.BOOTMODE_UPD, updFlg.updFlgSetteitisyuusei)      '��ʑJ��
        openForm.Show()                                                             '��ʕ\��
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@��z�σf�[�^�o�^�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnTDTorikomi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTDTorikomi.Click

        Dim openForm As ZG210E_SeisanHanei = New ZG210E_SeisanHanei(_msgHd, _db, Me, ZG210E_SeisanHanei.TEHAI, updFlg.updFlgTDTorikomi)      '��ʑJ��
        openForm.Show()                                                             '��ʕ\��
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@���ɍσf�[�^�o�^�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnNDTorikomi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNDTorikomi.Click

        Dim openForm As ZG210E_SeisanHanei = New ZG210E_SeisanHanei(_msgHd, _db, Me, ZG210E_SeisanHanei.NYUKO, updFlg.updFlgNDTorikomi)      '��ʑJ��
        openForm.Show()                                                             '��ʕ\��
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@���Y�ʃf�[�^�C���{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnSDSyuusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSDSyuusei.Click

        Dim openForm As ZG220E_SeisanSyusei = New ZG220E_SeisanSyusei(_msgHd, _db, Me, updFlg.updFlgSDSyuusei)      '��ʑJ��
        openForm.Show()                                                             '��ʕ\��
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@���Y�ʊm��{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnSeisanKakutei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeisanKakutei.Click

        Dim openForm As ZG230B_SeisanryouKakutei = New ZG230B_SeisanryouKakutei(_msgHd, _db, Me, updFlg.updFlgSeisanKakutei)      '��ʑJ��
        openForm.Show()                                                             '��ʕ\��
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�i��ʌv����̓{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnHKNyuryoku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHKNyuryoku.Click

        Dim openForm As ZG310E_Hinsyubetu = New ZG310E_Hinsyubetu(_msgHd, _db, Me, updFlg.updFlgHKNyuryoku)      '��ʑJ��
        openForm.Show()                                                             '��ʕ\��
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�ʌv����̓{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnKKNyuroku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKKNyuroku.Click

        Dim openForm As ZG320E_KobetuNyuuroku = New ZG320E_KobetuNyuuroku(_msgHd, _db, Me, updFlg.updFlgKKNyuroku)      '��ʑJ��
        openForm.Show()                                                             '��ʕ\��
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�̔����ю捞�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnHJTorikomi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHJTorikomi.Click

        Dim openForm As ZG330B_HJissekiTorikomi = New ZG330B_HJissekiTorikomi(_msgHd, _db, Me, updFlg.updFlgHJTorikomi)      '��ʑJ��
        openForm.Show()                                                             '��ʕ\��
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�̔��v��W�v�W�J�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnSyuukeiTenkai_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSyuukeiTenkai.Click

        Dim openForm As ZG340B_HJissekiTenkai = New ZG340B_HJissekiTenkai(_msgHd, _db, Me, updFlg.updFlgSyuukeiTenkai)      '��ʑJ��
        openForm.Show()                                                             '��ʕ\��
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�̔��v��ʏC���{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnHKSyuusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHKSyuusei.Click

        Dim openForm As ZG350E_KeikakuryouHosei = New ZG350E_KeikakuryouHosei(_msgHd, _db, Me, updFlg.updFlgHKSyuusei)      '��ʑJ��
        openForm.Show()                                                             '��ʕ\��
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�̔��v��ʊm��{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnSKakutei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSKakutei.Click

        Dim openForm As ZG360B_HKeikakuKakutei = New ZG360B_HKeikakuKakutei(_msgHd, _db, Me, updFlg.updFlgSKakutei)      '��ʑJ��
        openForm.Show()                                                             '��ʕ\��
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�݌Ɏ��ю捞�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnZaikoTorikomi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZaikoTorikomi.Click

        Dim openForm As ZG410B_ZJissekiTorikomi = New ZG410B_ZJissekiTorikomi(_msgHd, _db, Me, updFlg.updFlgZaikoTorikomi)      '��ʑJ��
        openForm.Show()                                                             '��ʕ\��
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@���Y�̔��݌Ɏ捞�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnSHZTorikomi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSHZTorikomi.Click

        Dim openForm As ZG510B_SHZTorikomiIkkatu = New ZG510B_SHZTorikomiIkkatu(_msgHd, _db, Me, updFlg.updFlgSHZTorikomi)      '��ʑJ��
        openForm.Show()                                                             '��ʕ\��
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@���Y�v�搔�ʏC���{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnSKSyuusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSKSyuusei.Click

        Dim openForm As ZG530E_SeisanSuuryouSyuusei = New ZG530E_SeisanSuuryouSyuusei(_msgHd, _db, Me, updFlg.updFlgSKSyuusei)      '��ʑJ��
        openForm.Show()                                                             '��ʕ\��
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@���Y�v��m��{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnKakutei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKakutei.Click

        Dim openForm As ZG540B_SKeikakuKakutei = New ZG540B_SKeikakuKakutei(_msgHd, _db, Me, updFlg.updFlgKakutei)      '��ʑJ��
        openForm.Show()                                                             '��ʕ\��
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@��z�f�[�^�쐬�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnTDSakusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTDSakusei.Click

        Dim openForm As ZG610B_TehaiDateSakusei = New ZG610B_TehaiDateSakusei(_msgHd, _db, Me, updFlg.updFlgTDSakusei)      '��ʑJ��
        openForm.Show()                                                             '��ʕ\��
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@��z�f�[�^�C���E�o�̓{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnTDSyuusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTDSyuusei.Click

        Dim openForm As ZG620E_TehaiSyuuseiItiran = New ZG620E_TehaiSyuuseiItiran(_msgHd, _db, Me, updFlg.updFlgTDSyuusei)      '��ʑJ��
        openForm.Show()                                                             '��ʕ\��
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@��z�f�[�^�쐬(���Y�Ǘ��V�X�e�����M�p)�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnTDSousin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTDSousin.Click

        Dim openForm As ZG630B_TehaiSakuseiSeisan = New ZG630B_TehaiSakuseiSeisan(_msgHd, _db, Me, updFlg.updFlgTDSousin)      '��ʑJ��
        openForm.Show()                                                             '��ʕ\��
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@���׎R�σf�[�^�捞�{�^������
    '------------------------------------------------------------------------------------------------------
     Private Sub btnFYamadumi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFYamadumi.Click

        Dim openForm As ZG720B_FukaYamadumiTorikomi = New ZG720B_FukaYamadumiTorikomi(_msgHd, _db, Me, updFlg.updFlgFYamadumi)      '��ʑJ��
        openForm.Show()                                                             '��ʕ\��
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@���׎R�ϏW�v���ʊm�F�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnKKakunin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKKakunin.Click

        Dim openForm As ZG730Q_FukaYamadumiKoutei = New ZG730Q_FukaYamadumiKoutei(_msgHd, _db, Me, updFlg.updFlgKKakunin)      '��ʑJ��
        openForm.Show()                                                             '��ʕ\��
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�����zDB�o�^�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnSTDB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSTDB.Click

        Dim openForm As ZG640B_SeisakuTehaiDB = New ZG640B_SeisakuTehaiDB(_msgHd, _db, Me, updFlg.updFlgSTDB)      '��ʑJ��
        openForm.Show()                                                             '��ʕ\��
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�V�K�o�^�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnTSakujo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSinki.Click

        '-->2010.12.02 add by takagi
        'Dim openForm As ZM110E_Sinki = New ZM110E_Sinki(_msgHd, _db, Me, updFlg.updFlgSinki, True)      '�p�����^��J�ڐ��ʂ֓n��
        Dim openForm As ZM110E_Sinki = New ZM110E_Sinki(_msgHd, _db, Me, updFlg.updFlgSinki, True, btnSinki.Tag)      '�p�����^��J�ڐ��ʂ֓n��
        '<--2010.12.02 add by takagi
        openForm.Show()                                                             '��ʕ\��
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�C���EEXCEL�o�͉���
    '------------------------------------------------------------------------------------------------------
    Private Sub btnSyuusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSyuusei.Click

        Dim openForm As ZM120E_Syuusei = New ZM120E_Syuusei(_msgHd, _db, Me, updFlg.updFlgSyuusei)      '�p�����^��J�ڐ��ʂ֓n��
        openForm.Show()                                                             '��ʕ\��
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�폜�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnSakujo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSakujo.Click

        '-->2010.12.02 add by takagi
        'Dim openForm As ZM110E_Sinki = New ZM110E_Sinki(_msgHd, _db, Me, updFlg.updFlgSinki, False)      '�p�����^��J�ڐ��ʂ֓n��
        Dim openForm As ZM110E_Sinki = New ZM110E_Sinki(_msgHd, _db, Me, updFlg.updFlgSinki, False, btnSakujo.Tag)      '�p�����^��J�ڐ��ʂ֓n��
        '<--2010.12.02 add by takagi
        openForm.Show()                                                             '��ʕ\��
        Me.Hide()

    End Sub

#Region "�v��Ώەi�ꗗ�\����{�^�������@�v��Ώ��i�ꗗ���"
    '------------------------------------------------------------------------------------------------------
    '�@�v��Ώەi�ꗗ�\����{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnKExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKExcel.Click
        Try
            '�}�E�X�J�[�\�������v
            Me.Cursor = Cursors.WaitCursor

            '���
            Dim startPrintTime As Date = Now

            'EXCEL�o��
            Call printExcel()

            Dim endPrintTime As Date = Now

            '����e�[�u���X�V
            Call updateSeigyoTbl(ZM130P_PGID, True, startPrintTime, endPrintTime)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        Finally
            '�}�E�X�J�[�\�����ɖ߂�
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�v��Ώەi�ꗗ�o��
    '------------------------------------------------------------------------------------------------------
    Private Sub printExcel()
        Try
            Dim pb As UtilProgressBar = New UtilProgressBar(Me)
            Try
                pb.Show()

                '�v���O���X�o�[�ݒ�
                pb.jobName = "�o�͂��������Ă��܂��B"
                pb.status = "���������D�D�D"

                '���`�t�@�C��(�i���ʔ̔��v��Ɠ������`)
                Dim openFilePath As String = StartUp.iniValue.BaseXlsPath & "\" & StartUp.iniValue.ExcelZM130R1_Base
                '���`�t�@�C�����J����Ă��Ȃ����`�F�b�N
                Dim fh As UtilFile = New UtilFile()
                Try
                    fh.move(openFilePath, openFilePath & 1)
                    fh.move(openFilePath & 1, openFilePath)
                Catch ioe As System.IO.IOException
                    Throw New UsrDefException("�t�@�C�����J����Ă��܂��B�t�@�C������Ă���Ď��s���Ă��������B", _
                                              _msgHd.getMSG("fileOpenErr", "�y�t�@�C�����z�F" & openFilePath))
                End Try

                '�o�͗p�t�@�C��
                '�t�@�C�����擾-----------------------------------------------------
                Dim wkEditFile As String = StartUp.iniValue.OutXlsPath & "\" & StartUp.iniValue.ExcelZM130R1_Out     '�R�s�[��t�@�C��

                '�R�s�[��t�@�C�������݂���ꍇ�A�R�s�[��t�@�C�����폜----------------
                If UtilClass.isFileExists(wkEditFile) Then
                    Try
                        fh.delete(wkEditFile)
                    Catch ioe As System.IO.IOException
                        Throw New UsrDefException("�t�@�C�����J����Ă��܂��B�t�@�C������Ă���Ď��s���Ă��������B", _
                                                  _msgHd.getMSG("fileOpenErr", "�y�t�@�C�����z�F" & wkEditFile))
                    End Try
                End If

                Try
                    '�o�͗p�t�@�C���֐��^�t�@�C���R�s�[
                    FileCopy(openFilePath, wkEditFile)
                Catch ioe As System.IO.IOException
                    Throw New UsrDefException(ioe, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ioe)))
                End Try

                Dim eh As xls.UtilExcelHandler = New xls.UtilExcelHandler(wkEditFile)
                Try
                    eh.open()
                    Try
                        '�ėp�}�X�^������v������擾
                        Dim sql As String = ""
                        sql = sql & N & " SELECT KAHENKEY, NAME1 FROM M01HANYO "
                        sql = sql & N & " WHERE KOTEIKEY = '" & M01KOTEI_JUYOUSAKI & "'"
                        sql = sql & N & " ORDER BY KAHENKEY "
                        'SQL���s
                        Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
                        Dim dsHanyo As DataSet = _db.selectDB(sql, RS, iRecCnt)

                        If iRecCnt <= 0 Then                    'M01�ėp�}�X�^���o���R�[�h���P�����Ȃ��ꍇ
                            Throw New UsrDefException("�o�^����Ă��܂���B", _msgHd.getMSG("noData"))
                        End If

                        For i As Integer = 0 To iRecCnt - 1

                           'M11�̒l���f�[�^�Z�b�g�ɕێ�
                            Dim dsM11 As DataSet = Nothing
                            Dim rowCntM11 As Integer = 0
                            '���v�悲�Ƃ�M11�̃f�[�^�𒊏o
                            Call getM11DataForXls(_db.rmNullStr(dsHanyo.Tables(RS).Rows(i)("KAHENKEY")), dsM11, rowCntM11)

                            'M12�̒l���f�[�^�Z�b�g�ɕێ�
                            Dim dsM12 As DataSet = Nothing
                            Dim rowCntM12 As Integer = 0
                            '���v�悲�Ƃ�M12�̃f�[�^�𒊏o
                            Call getM12DataForXls(_db.rmNullStr(dsHanyo.Tables(RS).Rows(i)("KAHENKEY")), dsM12, rowCntM12)

                            If rowCntM11 > 0 Then

                                '�V�[�g(���`)�𕡐��ۑ�
                                Dim baseName As String = XLSSHEETNM_HINSYU  '���`�V�[�g��
                                Dim newName As String = _db.rmNullStr(dsHanyo.Tables(RS).Rows(i)("NAME1"))    '�V���ɍ쐬����V�[�g
                                Try
                                    eh.targetSheet = baseName               '���`�V�[�g�I��
                                    eh.copySheetOnLast(newName)             '���`�V�[�g�R�s�[
                                Catch ex As Exception
                                    Throw New UsrDefException("�V�[�g�̕����Ɏ��s���܂����B", _msgHd.getMSG("failCopySheet"))
                                End Try

                                '�v���O���X�o�[�ݒ�
                                pb.jobName = newName & "�o�͒��D�D�D"
                                pb.status = ""

                                '�R�s�[�����V�[�g�ɏo��
                                eh.targetSheet = newName

                                '�쐬�����ҏW
                                Dim printDate As String = Now.ToString("yyyy/MM/dd HH:mm")
                                eh.setValue("�쐬���� �F " & printDate, 1, 8)   'H1

                                '�^�C�g���E���v�i���ҏW
                                eh.setValue(XLS_TITLE & "      (" & _db.rmNullStr(dsHanyo.Tables(RS).Rows(i)("NAME1")) & ")", 3, 1)      'A3

                                Dim startPrintRow As Integer = START_PRINT_ROW          '�o�͊J�n�s��

                                Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder
                                pb.maxVal = rowCntM11

                                Dim k As Integer = 0        'M11���[�v�J�E���^�[
                                Dim m As Integer = 0        'M12�̃��R�[�h���J�E���^�[
                                Dim xlsRow As Integer = 0
                                For k = 0 To rowCntM11 - 1

                                    pb.status = (k + 1) & "/" & rowCntM11 & "��"
                                    pb.oneStep = 10
                                    pb.value = k + 1

                                    xlsRow = startPrintRow + k

                                    '�s��1�s�ǉ�
                                    eh.copyRow(xlsRow)
                                    eh.insertPasteRow(xlsRow)

                                    '�ꗗ�f�[�^�o��
                                    With dsM11.Tables(RS)
                                        Dim sHinmeiCD As String = ""        '�o�͂���v��i���R�[�h��ێ�����ϐ�
                                        For n As Integer = m To rowCntM12 - 1
                                            '���i���R�[�h���������ꍇ
                                            If _db.rmNullStr(.Rows(k)(COLDT_HINMEICD)).Equals _
                                                            (_db.rmNullStr(dsM12.Tables(RS2).Rows(n)(COLDT_M12KHINMEICD))) Then
                                                If "".Equals(sHinmeiCD) Then
                                                    sHinmeiCD = _db.rmNullStr(dsM12.Tables(RS2).Rows(n)(COLDT_M12HINMEICD))
                                                Else
                                                    '�Y��������i���R�[�h���J���}��؂�łȂ���
                                                    sHinmeiCD = sHinmeiCD & "," & _db.rmNullStr(dsM12.Tables(RS2).Rows(n)(COLDT_M12HINMEICD))
                                                End If
                                                m = n + 1
                                            Else
                                                Exit For
                                            End If
                                        Next

                                        sb.Append(_db.rmNullStr(.Rows(k)(COLDT_HINMEICD)) & ControlChars.Tab)       '�i���R�[�h
                                        sb.Append(_db.rmNullStr(.Rows(k)(COLDT_HINMEI)) & ControlChars.Tab)         '�i��
                                        sb.Append(_db.rmNullStr(.Rows(k)(COLDT_LOTTYOU)) & ControlChars.Tab)        '���b�g��
                                        sb.Append(_db.rmNullStr(.Rows(k)(COLDT_TANTYOU)) & ControlChars.Tab)        '�P��
                                        sb.Append(_db.rmNullStr(.Rows(k)(COLDT_JOSU)) & ControlChars.Tab)           '��
                                        sb.Append(_db.rmNullStr(.Rows(k)(COLDT_KIJUNTUKISU)) & ControlChars.Tab)    '�����
                                        sb.Append(_db.rmNullStr(.Rows(k)(COLDT_ABC)) & ControlChars.Tab)            'ABC�敪
                                        sb.Append(sHinmeiCD)                                                        '�W�v�i����

                                        sb.Append(ControlChars.CrLf)
                                    End With
                                Next

                                Clipboard.SetText(sb.ToString)
                                eh.paste(START_PRINT_ROW, START_PRINT_COL) '�ꊇ�\��t��

                                '�r�����Đݒ�
                                Dim lineV As LineVO = New LineVO()
                                lineV.Bottom = LineVO.LineType.NomalL
                                eh.drawRuledLine(lineV, xlsRow, START_PRINT_COL, , 8)

                                eh.deleteRow(xlsRow + 1)    '�]���ȍs���폜
                            End If
                        Next

                        eh.deleteSheet(XLSSHEETNM_HINSYU)   '�]���ȃV�[�g���폜

                        '-->2010.12.25 add by takagi #43
                        '�擪�V�[�g��I��
                        eh.targetSheetByIdx = 1
                        eh.selectSheet(eh.targetSheet)
                        eh.selectCell(1, 1)
                        '<--2010.12.25 add by takagi #43
                    Finally
                        eh.close()
                    End Try

                    'EXCEL�t�@�C���J��
                    eh.display()

                Catch ue As UsrDefException
                    ue.dspMsg()
                    Throw ue
                Catch ex As Exception
                    '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��V�X�e���G���[MSG�o�͌�X���[
                    Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)), , StartUp.iniValue.LogFilePath)
                Finally
                    eh.endUse()
                    eh = Nothing
                End Try
            Finally
                '��ʏ���
                pb.Close()
            End Try
        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '   �G�N�Z���o�͗p�f�[�^���o
    '�@�i�����T�v�j�G�N�Z���o�͗p�̃f�[�^��M11���璊�o����B
    '   �����̓p�����^  �FprmJJuyousaki     ���v��̒l
    '   ���o�̓p�����^  �FprmDs             ���o���ʂ̃f�[�^�Z�b�g
    '   ���o�̓p�����^  �FprmRecCnt         ���o���ʌ���
    '-------------------------------------------------------------------------------
    Private Sub getM11DataForXls(ByVal prmJuyousaki As String, ByRef prmDs As DataSet, ByRef prmRecCnt As Integer)
        Try

            'EXCEL�p�̃f�[�^�擾
            Dim SQL As String = ""
            SQL = "SELECT "
            SQL = SQL & N & " (TT_H_SIYOU_CD "
            SQL = SQL & N & "  || TT_H_HIN_CD "
            SQL = SQL & N & "  || TT_H_SENSIN_CD "
            SQL = SQL & N & "  || TT_H_SIZE_CD "
            SQL = SQL & N & "  || TT_H_COLOR_CD)   " & COLDT_HINMEICD       '�i���R�[�h
            SQL = SQL & N & " ,TT_HINMEI           " & COLDT_HINMEI         '�i��
            SQL = SQL & N & " ,TT_LOT              " & COLDT_LOTTYOU        '�W�����b�g��
            SQL = SQL & N & " ,TT_TANCYO           " & COLDT_TANTYOU        '����P��
            SQL = SQL & N & " ,TT_JYOSU            " & COLDT_JOSU           '���ɖ{�� �S��
            SQL = SQL & N & " ,TT_KZAIKOTUKISU     " & COLDT_KIJUNTUKISU    '�����
            SQL = SQL & N & " ,TT_ABCKBN           " & COLDT_ABC            'ABC�敪
            SQL = SQL & N & " FROM M11KEIKAKUHIN "
            SQL = SQL & N & "   WHERE "
            '���v��
            SQL = SQL & "   TT_JUYOUCD = '" & _db.rmSQ(prmJuyousaki) & "'"
            SQL = SQL & "   ORDER BY TT_JUYOUCD, TT_H_HIN_CD, TT_H_SENSIN_CD,  "
            SQL = SQL & "   TT_H_SIZE_CD, TT_H_SIYOU_CD, TT_H_COLOR_CD, TT_TEHAI_KBN "

            'SQL���s
            prmDs = _db.selectDB(SQL, RS, prmRecCnt)

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '   �G�N�Z���o�͗p�f�[�^���o
    '�@�i�����T�v�j�G�N�Z���o�͗p�̃f�[�^��M12���璊�o����B
    '   �����̓p�����^  �FprmJuyousaki      ���v��̒l
    '   ���o�̓p�����^  �FprmDs             ���o���ʂ̃f�[�^�Z�b�g
    '   ���o�̓p�����^  �FprmRecCnt         ���o���ʌ���
    '-------------------------------------------------------------------------------
    Private Sub getM12DataForXls(ByVal prmJuyousaki As String, ByRef prmDs As DataSet, ByRef prmRecCnt As Integer)
        Try

            'EXCEL�p�̃f�[�^�擾
            Dim SQL As String = ""
            SQL = "SELECT "
            SQL = SQL & N & "  M12.HINMEICD " & COLDT_M12HINMEICD       '���i���R�[�h
            SQL = SQL & N & "  ,M12.KHINMEICD " & COLDT_M12KHINMEICD    '�v��i���R�[�h
            SQL = SQL & N & " FROM  M12SYUYAKU M12 "
            SQL = SQL & N & "   LEFT JOIN  M11KEIKAKUHIN M11 "
            SQL = SQL & N & "   ON M11.TT_KHINMEICD = M12.KHINMEICD "
            SQL = SQL & N & "   WHERE "
            SQL = SQL & N & "   NOT M12.KHINMEICD = M12.HINMEICD "

            '���v��
            SQL = SQL & N & "   AND "
            SQL = SQL & "   M11.TT_JUYOUCD = '" & _db.rmSQ(prmJuyousaki) & "'"

            SQL = SQL & "   ORDER BY TT_JUYOUCD, TT_H_HIN_CD, TT_H_SENSIN_CD,  "
            SQL = SQL & "   TT_H_SIZE_CD, TT_H_SIYOU_CD, TT_H_COLOR_CD, TT_TEHAI_KBN "

            'SQL���s
            prmDs = _db.selectDB(SQL, RS2, prmRecCnt)


        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

#End Region

    '------------------------------------------------------------------------------------------------------
    '�@ABC���̓{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnABC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnABC.Click

        Dim openForm As ZM410B_ABCBunseki = New ZM410B_ABCBunseki(_msgHd, _db, Me, updFlg.updFlgABC)      '��ʑJ��
        openForm.Show()                                                             '��ʕ\��
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�i��敪�}�X�^�����e�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnHMstMente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHMstMente.Click

        Dim openForm As ZM210E_HinsyuKbn = New ZM210E_HinsyuKbn(_msgHd, _db, Me, updFlg.updFlgHMstMente)      '�p�����^��J�ڐ��ʂ֓n��
        openForm.Show()                                                             '��ʕ\��
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�ėp�}�X�^�����e�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnHanyoMst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHanyoMst.Click

        Me.Hide()
        Dim openForm As ZM310E_HanyouMstMente = New ZM310E_HanyouMstMente(_msgHd, _db, Me, updFlg.updFlgHanyoMst)      '��ʑJ��
        openForm.Show()                                                             '��ʕ\��

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@���Y�\�̓}�X�^�����e�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnSNouryokuMst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSNouryokuMst.Click

        Dim openForm As ZM610E_SeisanMstMente = New ZM610E_SeisanMstMente(_msgHd, _db, Me, updFlg.updFlgSNouryokuMst)      '��ʑJ��
        openForm.Show()                                                             '��ʕ\��
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�O���V�X�e���A�g�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnGRenkei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGRenkei.Click

        Dim openForm As ZM510B_GaibuSystem = New ZM510B_GaibuSystem(_msgHd, _db, Me)      '��ʑJ��
        openForm.Show()                                                             '��ʕ\��
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�̔����яƉ�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnHSyoukai_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHSyoukai.Click

        Dim openForm As ZE110Q_HanbaiJisseki = New ZE110Q_HanbaiJisseki(_msgHd, _db, Me)      '��ʑJ��
        openForm.Show()                                                             '��ʕ\��
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�݌Ɏ��яƉ�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnZSyoukai_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZSyoukai.Click

        Dim openForm As ZE210Q_ZaikoJisseki = New ZE210Q_ZaikoJisseki(_msgHd, _db, Me)      '��ʑJ��
        openForm.Show()                                                             '��ʕ\��
        Me.Hide()

    End Sub

    Private Sub tabGeturei_Click(sender As Object, e As EventArgs) Handles tabGeturei.Click

    End Sub
End Class