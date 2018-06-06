'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���j�v��Ώەi�}�X�^�����e�C�����
'    �i�t�H�[��ID�jZM120E_Syuusei
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ���V        2010/11/02                 �V�K              
'�@(2)   ����        2010/12/17                 �ύX�@�\�[�g����ύX             
'�@(3)   ����        2011/01/13                 �ύX�@�ȖڃR�[�h�����`�F�b�N�ύX
'�@(4)   ����        2011/02/10                 �C���@�X�V�{�^���������̕s��Ή�
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.Combo
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls

Public Class ZM120E_Syuusei
    Implements IfRturnKahenKey

#Region "���e�����l��`"
    '-------------------------------------------------------------------------------
    '   �萔��`
    '-------------------------------------------------------------------------------
    '�w�b�_�s��
    Private Const MZ0203_ROW_HEADER As Integer = -1             '�w�b�_�s��

    'PG���䕶�� 
    Private Const N As String = ControlChars.NewLine            '���s����
    Private Const RS As String = "RecSet"                       '���R�[�h�Z�b�g�e�[�u��
    Private Const RS2 As String = "RecSetM12ForxLS"             '���R�[�h�Z�b�g�e�[�u��
    Private Const PGID As String = "ZM120E"                     'T91�ɓo�^����PGID

    '�ꗗ��
    Private Const COLCN_HINMEICD As String = "cnHinmeiCD"               '�i���R�[�h
    Private Const COLCN_HINMEI As String = "cnHinmei"                   '�i��
    Private Const COLCN_SEISAKUKBN As String = "cnSeisakuKbn"           '����敪
    Private Const COLCN_LOTTYOU As String = "cnLottyou"                 '�W�����b�g��
    Private Const COLCN_TANTYOU As String = "cnSeisakuTantyou"          '����P��
    Private Const COLCN_JOSU As String = "cnJosu"                       '��
    Private Const COLCN_KND As String = "cnKHonsuu"                     'KND����
    Private Const COLCN_HST As String = "cnHSTHonsuu"                   'HST����
    Private Const COLCN_ZAIKO As String = "cnZKurikaesi"                '�݌ɌJ��
    Private Const COLCN_ZAIKOBTN As String = "cnZKurikaesiBtn"          '�݌ɌJ�ԃ{�^��
    Private Const COLCN_ZAIKONM As String = "cnZKurikaesinm"            '�݌ɌJ�Ԗ�
    Private Const COLCN_CHUMONSAKI As String = "cnChumonsaki"           '������
    Private Const COLCN_JUYOUSAKINAME As String = "cnJuyousakiName"     '���v�於
    Private Const COLCN_JUYOUSAKI As String = "cnJuyousaki"             '���v��
    Private Const COLCN_JUYOUSAKIBTN As String = "cnJuyousakiBtn"       '���v��{�^��
    Private Const COLCN_SIZETENKAI As String = "cnSizeTenkai"           '�T�C�Y�W�J
    Private Const COLCN_SIZETENKAIBTN As String = "cnSizeTenkaiBtn"     '�T�C�Y�W�J�{�^��
    Private Const COLCN_HINSYUKBN As String = "cnHinsyuKbn"             '�i��敪
    Private Const COLCN_KIJUNTUKISU As String = "cnKijunTuki"           '�����
    Private Const COLCN_SAIGAI As String = "cnSaigai"                   '�ЊQ�����p�݌�
    Private Const COLCN_ANZENZAIKO As String = "cnAnzenZaiko"           '���S�݌�
    Private Const COLCN_SHINMEI As String = "cnSHinmei"                 '�W�v�i��
    Private Const COLCN_SHINMEIBTN As String = "cnSHinmeiBtn"           '�W�v�i���{�^��
    Private Const COLCN_KAMOKUCD As String = "cnKamokuCD"               '�ȖڃR�[�h
    Private Const COLCN_MAKIWAKU As String = "cnMakiwaku"               '���g�R�[�h
    Private Const COLCN_HOUSOU As String = "cnHousou"                   '��^�\���敪
    Private Const COLCN_SIYOUSYONO As String = "cnSiyousyoNo"           '�d�l��No
    Private Const COLCN_SEIZOUBUMON As String = "cnSeizouBumon"         '��������R�[�h
    Private Const COLCN_SEIZOUBUMONBTN As String = "cnSeizouBumonBtn"   '��������R�[�h�{�^��
    Private Const COLCN_SEIZOUBUMONNM As String = "cnSeizouBumonnm"     '�������喼
    Private Const COLCN_TENKAIKBN As String = "cnTenkaiKbn"             '�W�J�敪
    Private Const COLCN_TENKAIKBNBTN As String = "cnTenkaiKbnBtn"       '�W�J�敪�{�^��
    Private Const COLCN_TENKAIKBNNM As String = "cnTenkaiKbnnm"         '�W�J�敪��
    Private Const COLCN_BUBUNKOUTEI As String = "cnBubunKoutei"         '�����H��
    Private Const COLCN_HINSITU As String = "cnHinsitu"                 '�i������
    Private Const COLCN_HINSITUBTN As String = "cnHinsituBtn"           '�i�������{�^��
    Private Const COLCN_TATIAI As String = "cnTatiai"                   '����L��
    Private Const COLCN_TATIAIBTN As String = "cnTatiaiBtn"             '����L���{�^��
    Private Const COLCN_CHANGEFLG As String = "cnChangeFlg"             '�ύX�t���O

    '�ꗗ�o�C���hDetaSet��
    Private Const COLDT_HINMEICD As String = "dtHinmeiCD"               '�i���R�[�h
    Private Const COLDT_HINMEI As String = "dtHinmei"                   '�i��
    Private Const COLDT_SEISAKUKUBUN As String = "dtSeisakuKbn"         '����敪
    Private Const COLDT_LOTTYOU As String = "dtLottyou"                 '�W�����b�g��
    Private Const COLDT_TANTYOU As String = "dtSeisakuTantyou"          '�P��
    Private Const COLDT_JOSU As String = "dtJosu"                       '��
    Private Const COLDT_KND As String = "dtKHonsuu"                     'KND����
    Private Const COLDT_HST As String = "dtHSTHonsuu"                   'HST����
    Private Const COLDT_ZAIKO As String = "dtZKurikaesi"                '�݌ɌJ��
    Private Const COLDT_ZAIKOBTN As String = "dtZKurikaesiBtn"          '�݌ɌJ�ԃ{�^��
    Private Const COLDT_ZAIKONM As String = "dtZKurikaesinm"            '�݌ɌJ�Ԗ�
    Private Const COLDT_CHUMONSAKI As String = "dtChumonsaki"           '������
    Private Const COLDT_JUYOUSAKINAME As String = "dtJuyousakiName"     '���v�於
    Private Const COLDT_JUYOUSAKI As String = "dtJuyousaki"             '���v��
    Private Const COLDT_JUYOUSAKIBTN As String = "dtJuyousakiBtn"       '���v��{�^��
    Private Const COLDT_SIZETENKAI As String = "dtSizeTenkai"           '�T�C�Y�W�J
    Private Const COLDT_SIZETENKAIBTN As String = "dtSizeTenkaiBtn"     '�T�C�Y�W�J�{�^��
    Private Const COLDT_HINSYUKBN As String = "dtHinsyuKbn"             '�i��敪
    Private Const COLDT_KIJUNTUKISU As String = "dtKijunTuki"           '�����
    Private Const COLDT_SAIGAI As String = "dtSaigai"                   '�ЊQ�����p�݌�
    Private Const COLDT_ANZENZAIKO As String = "dtAnzenZaiko"           '���S�݌�
    Private Const COLDT_SHINMEI As String = "dtSHinmei"                 '�W�v�i��
    Private Const COLDT_SHINMEIBTN As String = "dtSHinmeiBtn"           '�W�v�i���{�^��
    Private Const COLDT_KAMOKUCD As String = "dtKamokuCD"               '�ȖڃR�[�h
    Private Const COLDT_MAKIWAKU As String = "dtMakiwaku"               '���g�R�[�h
    Private Const COLDT_HOUSOU As String = "dtHousou"                   '��^�\���敪
    Private Const COLDT_SIYOUSYONO As String = "dtSiyousyoNo"           '�d�l���ԍ�
    Private Const COLDT_SEIZOUBUMON As String = "dtSeizouBumon"         '��������
    Private Const COLDT_SEIZOUBUMONBTN As String = "dtSeizouBumonBtn"   '��������{�^��
    Private Const COLDT_SEIZOUBUMONNM As String = "dtSeizouBumonnm"     '�������喼
    Private Const COLDT_TENKAIKBN As String = "dtTenkaiKbn"             '�W�J�敪
    Private Const COLDT_TENKAIKBNBTN As String = "dtTenkaiKbnBtn"       '�W�J�敪�{�^��
    Private Const COLDT_TENKAIKBNNM As String = "dtTenkaiKbnnm"         '�W�J�敪��
    Private Const COLDT_BUBUNKOUTEI As String = "dtBubunKoutei"         '�����H��
    Private Const COLDT_HINSITU As String = "dtHinsitu"                 '�i������
    Private Const COLDT_HINSITUBTN As String = "dtHinsituBtn"           '�i�������{�^��
    Private Const COLDT_TATIAI As String = "dtTatiai"                   '����L��
    Private Const COLDT_TATIAIBTN As String = "dtTatiaiBtn"             '����L���{�^��
    Private Const COLDT_CHANGEFLG As String = "dtChangeFlg"             '�ύX�t���O

    Private Const COLDT_M12KHINMEICD As String = "KHINMEICD"
    Private Const COLDT_M12HINMEICD As String = "HINMEICD"

    '�ꗗ��ԍ�
    Private Const COLNO_HINMEICD As Integer = 0             '�i���R�[�h
    Private Const COLNO_HINMEI As Integer = 1               '�i��
    Private Const COLNO_LOTTYOU As Integer = 2              '�W�����b�g��
    Private Const COLNO_TANTYOU As Integer = 3              '�P��
    Private Const COLNO_JOSU As Integer = 4                 '��
    Private Const COLNO_KND As Integer = 5                  '���ɖ{�� �k���{�{��
    Private Const COLNO_HST As Integer = 6                  '���ɖ{�� HST
    Private Const COLNO_MAKIWAKU As Integer = 7             '���g�R�[�h
    Private Const COLNO_HOUSOU As Integer = 8               '��敪
    Private Const COLNO_SIYOUSYONO As Integer = 9           '�d�l���ԍ�
    Private Const COLNO_SEISAKUKBN As Integer = 10          '����敪
    Private Const COLNO_ZAIKO As Integer = 11               '�݌ɌJ��
    Private Const COLNO_ZAIKOBTN As Integer = 12            '�݌ɌJ�ԃ{�^��
    Private Const COLNO_ZAIKONM As Integer = 13             '�݌ɌJ�Ԗ�
    Private Const COLNO_CHUMONSAKI As Integer = 14          '������
    Private Const COLNO_JUYOUSAKI As Integer = 15           '���v��
    Private Const COLNO_JUYOUSAKIBTN As Integer = 16        '���v��{�^��
    Private Const COLNO_JUYOUSAKINAME As Integer = 17       '���v�於
    Private Const COLNO_SIZETENKAI As Integer = 18          '�T�C�Y�W�J
    Private Const COLNO_SIZETENKAIBTN As Integer = 19       '�T�C�Y�W�J�{�^��
    Private Const COLNO_HINSYUKBN As Integer = 20           '�i��敪
    Private Const COLNO_KIJUNTUKISUU As Integer = 21        '�����
    Private Const COLNO_SAIGAI As Integer = 22              '�ЊQ�����݌�
    Private Const COLNO_ANNZENNZAIKO As Integer = 23        '���S�݌�
    Private Const COLNO_SYUUKEIHINMEI As Integer = 24       '�W�v�i��
    Private Const COLNO_SYUUKEIHINMEIBTN As Integer = 25    '�W�v�i���{�^��
    Private Const COLNO_KAMOKUCD As Integer = 26            '�ȖڃR�[�h
    Private Const COLNO_SEIZOUBUMON As Integer = 27         '��������
    Private Const COLNO_SEIZOUBUMONBTN As Integer = 28      '��������{�^��
    Private Const COLNO_SEIZOUBUMONNM As Integer = 29       '�������喼
    Private Const COLNO_TENKAIKBN As Integer = 30           '�W�J�敪
    Private Const COLNO_TENKAIKBNBTN As Integer = 31        '�W�J�敪�{�^��
    Private Const COLNO_TENKAIKBNNM As Integer = 32         '�W�J�敪��
    Private Const COLNO_BUBUNKOUTEI As Integer = 33         '�����H��
    Private Const COLNO_HINSITU As Integer = 34             '�i������
    Private Const COLNO_HINSITUBTN As Integer = 35          '�i�������{�^��
    Private Const COLNO_TATIAI As Integer = 36              '����L��
    Private Const COLNO_TATIAIBTN As Integer = 37           '����L���{�^��
    Private Const COLNO_CHANGEFLG As Integer = 38           '�ύX�t���O


    '�ėp�}�X�^�Œ�L�[
    Private Const M01KOTEI_SEISAKUKBN As String = "03"          '����敪
    Private Const M01KOTEI_ZAIKO As String = "10"               '�݌ɌJ��
    Private Const M01KOTEI_JUYOUSAKI As String = "01"           '���v��
    Private Const M01KOTEI_SEIZOBMN As String = "09"            '��������
    Private Const M01KOTEI_TENKAI As String = "04"              '�W�J�敪
    Private Const M01KOTEI_HINSITU As String = "08"             '�i�������敪
    Private Const M01KOTEI_TATIAI As String = "06"              '����L��
    Private Const M01KOTEI_SIZETENKAI As String = "11"          '�T�C�Y�W�J

    '�ėp�}�X�^�σL�[
    'Private Const lH09_TUSIN As String = "1"        '��������R�[�h�F�ʐM
    'Private Const lH09_DENRYOKU As String = "3"     '��������R�[�h�F�d��
    'Private Const lH03_NAISAKU As String = "1"      '����敪�F����
    'Private Const lH03_GAICHUU As String = "2"      '����敪�F�O��
    Private Const lH03_KOUNYUU As Integer = 3       '����敪�F�w��
    Private Const M01KAHEN_ZAIKO_ZAIKO As Integer = 1   '�݌ɌJ�ԁF�݌ɑΏ�
    Private Const M01KAHEN_ZAIKO_JUTYU As Integer = 2   '�݌ɌJ�ԁF�J�Ԃ���
    Private Const M01KAHEN_TENKAI_BUBUN As Integer = 2  '�W�J�敪�F�����W�J

    '�ėp�}�X�^������
    Private Const M01NAME2_SEISAKUKBN As String = "�O"
    Private Const HANYOU_NAME1 As String = "NAME1"          '���̂P

    '�V�[�g��ԍ�
    Private Const XLSNO_HINMEICD As Integer = 1             '�i���R�[�h
    Private Const XLSNO_HINMEI As Integer = 2               '�i��
    Private Const XLSNO_LOTTYOU As Integer = 3              '�W�����b�g��
    Private Const XLSNO_TANTYOU As Integer = 4              '�P��
    Private Const XLSNO_JOSU As Integer = 5                '��
    Private Const XLSNO_KND As Integer = 6                  '���ɖ{�� �k���{�{��
    Private Const XLSNO_HST As Integer = 7                  '���ɖ{�� HST
    Private Const XLSNO_MAKIWAKU As Integer = 8             '���g�R�[�h
    Private Const XLSNO_HOUSOU As Integer = 9               '��敪
    Private Const XLSNO_SIYOUSYONO As Integer = 10           '�d�l���ԍ�
    Private Const XLSNO_SEISAKUKBN As Integer = 11          '����敪
    Private Const XLSNO_ZAIKO As Integer = 12               '�݌ɌJ��
    Private Const XLSNO_CHUMONSAKI As Integer = 13          '������
    Private Const XLSNO_JUYOUSAKI As Integer = 14          '���v��
    Private Const XLSNO_HINSYUKBN As Integer = 15           '�i��敪
    Private Const XLSNO_SIZETENKAI As Integer = 16          '�T�C�Y�W�J
    Private Const XLSNO_SHINMEICD As Integer = 17           '�W��Ώەi���R�[�h
    Private Const XLSNO_KIJUNTUKISUU As Integer = 18        '�����
    Private Const XLSNO_SAIGAI As Integer = 19              '�ЊQ�����݌�
    Private Const XLSNO_ANNZENNZAIKO As Integer = 20        '���S�݌�
    Private Const XLSNO_KAMOKUCD As Integer = 21            '�ȖڃR�[�h
    Private Const XLSNO_SEIZOUBUMON As Integer = 22         '��������
    Private Const XLSNO_TENKAIKBN As Integer = 23           '�W�J�敪
    Private Const XLSNO_BUBUNKOUTEI As Integer = 24         '�����H��
    Private Const XLSNO_HINSITU As Integer = 25             '�i������
    Private Const XLSNO_TATIAI As Integer = 26              '����L��
    
    '�ύX�t���O���e
    Private Const MZ0203_UPDFLG_OFF As Integer = 5     '�ύX�Ȃ�
    Private Const ZM120E_UPDFLG_ON As Integer = 1      '�ύX����

    '�ޗ��[DB�����p
    Private Const DT1_HINSYU As String = "HINSYU"
    Private Const DT1_LINE As String = "LINE"
    Private Const DT1_COLOR As String = "COLOR"

    '�ύX�t���O
    Private Const HENKO_FLG As String = "1"

    'EXCEL
    Private Const START_PRINT As Integer = 9               'EXCEL�o�͊J�n�s��
    '�v��Ώ��i�}�X�^�ꗗ���`�V�[�g��
    Private Const XLSSHEETNM_HINSYU As String = "Ver1.0.00"

#End Region

#Region "�����o�[�ϐ��錾"

    '-------------------------------------------------------------------------------
    '   �ϐ���`
    '-------------------------------------------------------------------------------

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu
    Private _chkCellVO As UtilDgvChkCellVO          '�ꗗ�̓��͐����p
    Private _ctlText As Control

    Dim _dgv As UtilDataGridViewHandler         '�O���b�h�n���h���[

    Private _mz02KahenKey As String             '�R�[�h�I���q��ʂ���󂯎��ėp�}�X�^�σL�[

    Dim _copyPositionRow As Integer = -1        '�R�s�[�J�n�ʒu�{�^���������̍s�ԍ�
    Dim _copyPositionCol As Integer = -1        '�R�s�[�J�n�ʒu�{�^���������̗�ԍ�

    Private _errSet As UtilDataGridViewHandler.dgvErrSet
    Private _nyuuryokuErrFlg As Boolean = False

    Private _oldRowIndex As Integer = -1            '�I���s�̔w�i�F��ύX���邽�߂̕ϐ�
    Private _colorCtlFlg As Boolean = False         '�I���s�̔w�i�F��ύX���邽�߂̃t���O

    Private _updFlg As Boolean = False              '�X�V��

    Private _formOpenFlg As Boolean = True          '��ʋN�����t���O
    Private _dgvChangeFlg As Boolean = False        '�ꗗ�ύX�t���O

    Private _tanmatuID As String = ""               '�[��ID

    '���������i�[�ϐ�
    Private _serchSiyoCd As String = ""             '�d�l�R�[�h
    Private _serchHinsyuCd As String = ""           '�i��R�[�h
    Private _serchSensinCd As String = ""           '���S���R�[�h
    Private _serchSizeCd As String = ""             '�T�C�Y�R�[�h
    Private _serchColorCd As String = ""            '�F�R�[�h
    Private _serchSeizouBmn As String = ""          '��������
    Private _serchSeisakuKbn As String = ""         '����敪
    Private _serchJuyosaki As String = ""           '���v��
    Private _serchCdSeizouBmn As String = ""        '��������R�[�h
    Private _serchCdSeisakuKbn As String = ""       '����敪�R�[�h
    Private _serchCdJuyosaki As String = ""         '���v��R�[�h

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
    '�R���X�g���N�^�@���j���[����Ă΂��
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
    Private Sub ZM120E_Syuusei_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            '�`��֌W�̐ݒ�
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           '�T�C�Y���ύX���ꂽ�Ƃ��ɁA�R���g���[�����R���g���[�����̂��ĕ`�悷�邩�ǂ����������l��ݒ�
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '�`��̓o�b�t�@�Ŏ��s����A������ɁA���ʂ���ʂɏo�͂����悤�ݒ�
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  '�R���g���[���́A��ʂɒ��ڂł͂Ȃ��A�܂��o�b�t�@�ɕ`�悳��܂��B����ɂ��A�������}���邱�Ƃ��ł��܂��B
            Me.SetStyle(ControlStyles.UserPaint, True)              '�R���g���[���́A�I�y���[�e�B���O �V�X�e���ɂ���Ăł͂Ȃ��A�Ǝ��ɕ`�悳���悤�ݒ�
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   '�R���g���[���̓E�B���h�E ���b�Z�[�W WM_ERASEBKGND �𖳎�����悤�ɐݒ�

            Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr '�^�C�g���I�v�V�����\��

            '�[��ID�̎擾
            _tanmatuID = UtilClass.getComputerName

            '��ʋN�����t���O�L��
            _formOpenFlg = True

            '�����l�ݒ�
            Call initForm()

            '��ʋN�����t���O����
            _formOpenFlg = False

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@�t�H�[���A�����[�h�C�x���g
    '-------------------------------------------------------------------------------
    Private Sub frmMZ02_03M_TehaiKousin_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        '�g�����U�N�V�����L���Ȃ��������
        If _db.isTransactionOpen Then
            Call _db.rollbackTran()
        End If

    End Sub

#End Region

#Region "�{�^���C�x���g"

    '-------------------------------------------------------------------------------
    '�@�@�����{�^�������C�x���g
    '-------------------------------------------------------------------------------
    Private Sub btnKensaku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensaku.Click
        Try

            '�x�����b�Z�[�W
            If _dgvChangeFlg Then
                Dim rtn As DialogResult = _msgHd.dspMSG("confirmDgvEdit")   '�ҏW���̓��e���j������܂��B��낵���ł����H
                If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If

            '�}�E�X�J�[�\�������v
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor
            Try

                Dim chSeizou As UtilComboBoxHandler = New UtilComboBoxHandler(cboSeizouBmn)
                Dim chSeisaku As UtilComboBoxHandler = New UtilComboBoxHandler(cboSeisakuKbn)
                Dim chJuyo As UtilComboBoxHandler = New UtilComboBoxHandler(cboJuyosaki)
                '���������̕ێ�
                _serchSiyoCd = txtSiyou.Text
                _serchHinsyuCd = txtHinsyu.Text
                _serchSensinCd = txtSensin.Text
                _serchSizeCd = txtSize.Text
                _serchColorCd = txtColor.Text
                _serchSeizouBmn = _db.rmNullStr(chSeizou.getName)
                _serchSeisakuKbn = _db.rmNullStr(chSeisaku.getName)
                _serchJuyosaki = _db.rmNullStr(chJuyo.getName)
                _serchCdSeizouBmn = _db.rmNullStr(chSeizou.getCode)
                _serchCdSeisakuKbn = _db.rmNullStr(chSeisaku.getCode)
                _serchCdJuyosaki = _db.rmNullStr(chJuyo.getCode)

                '�񒅐F�t���O����
                _colorCtlFlg = False

                '�ꗗ�\��
                Call dispData()

                '�񒅐F�t���O�L��
                _colorCtlFlg = True

                '�X�V�A�R�s�[�J�n�ʒu�A�R�s�[�{�^���A�v��Ώەi�}�X�^�ꗗ�{�^���g�p��
                If "0".Equals(lblKensuu.Text.Substring(0, 1)) Then

                    Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvKousin)
                    gh.clearRow()

                    btnKousin.Enabled = False
                    btnCopyPosition.Enabled = False
                    btnCopy.Enabled = False
                    btnInsatu.Enabled = False
                    Throw New UsrDefException("�Ώۃf�[�^������܂���B", _msgHd.getMSG("NonData"), cboSeizouBmn)
                Else
                    '�X�V�A�R�s�[�J�n�ʒu�A�R�s�[�{�^���A�v��Ώەi�}�X�^�ꗗ�{�^���g�p��
                    btnKousin.Enabled = True
                    btnCopyPosition.Enabled = True
                    btnCopy.Enabled = True
                    btnInsatu.Enabled = True
                    dgvKousin.Focus()

                    dgvKousin.CurrentCell = dgvKousin(COLNO_LOTTYOU, 0)
                End If

                '�ꗗ�ύX�t���O
                _dgvChangeFlg = False

            Finally
                '�}�E�X�J�[�\�����ɖ߂�
                Me.Cursor = cur
            End Try

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@�R�s�[�J�n�ʒu�{�^������
    '�@�i�����T�v�j���݂̃Z���̍s���ԍ���ێ����A�Z���ɒ��F����B
    '-------------------------------------------------------------------------------
    Private Sub btnCopyPosition_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyPosition.Click
        Try

            '���͉\�ȗ�̂ݏ������s��
            If dgvKousin.CurrentCell.ColumnIndex = COLNO_HINMEI Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_HINMEICD Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_JOSU Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_SEISAKUKBN Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_ZAIKOBTN Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_ZAIKONM Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_JUYOUSAKIBTN Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_JUYOUSAKINAME Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_SIZETENKAIBTN Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_SYUUKEIHINMEI Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_SYUUKEIHINMEIBTN Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_SEIZOUBUMON Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_SEIZOUBUMONBTN Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_SEIZOUBUMONNM Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_TENKAIKBNBTN Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_TENKAIKBNNM Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_HINSITUBTN Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_TATIAIBTN Then
                Exit Sub
            End If

            '���ɃR�s�[�J�n�ʒu���ݒ肳��Ă���ꍇ�́A�Z���̐F�����ɖ߂�
            If _copyPositionRow <> -1 Then
                dgvKousin(_copyPositionCol, _copyPositionRow).Style.BackColor = StartUp.lCOLOR_WHITE
            End If

            '���݂̃Z���̍s�E��ԍ���ێ�
            _copyPositionRow = dgvKousin.CurrentCell.RowIndex
            _copyPositionCol = dgvKousin.CurrentCell.ColumnIndex

            '�R�s�[�J�n�ʒu�̒��F
            dgvKousin(_copyPositionCol, _copyPositionRow).Style.BackColor = StartUp.lCOLOR_PINK

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@�R�s�[�{�^������
    '�@�i�����T�v�j�R�s�[�J�n�ʒu�̃Z���l�����݂̃Z���܂ŃR�s�[����B
    '-------------------------------------------------------------------------------
    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Try
            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGV�n���h���̐ݒ�

            '�R�s�[�J�n�ʒu�ƌ��݂̃Z���̗񂪈Ⴄ�ꍇ�͏������s��Ȃ��B
            If _copyPositionCol <> dgvKousin.CurrentCell.ColumnIndex Then
                Exit Sub
            End If
            Try
                '�R�s�[�J�n�ʒu�̒l�����݂̃Z���܂œ\��t����B
                '�J�n�ʒu�ƃR�s�[�{�^���������̈ʒu�̂����A�s�������������̃Z���l���R�s�[����B
                Dim copyFrom As Integer
                Dim copyTo As Integer
                If _copyPositionRow < dgvKousin.CurrentCell.RowIndex Then
                    copyFrom = _copyPositionRow
                    copyTo = dgvKousin.CurrentCell.RowIndex
                Else
                    copyFrom = dgvKousin.CurrentCell.RowIndex
                    copyTo = _copyPositionRow
                End If
                '�\��t����O�ƌ�Œl���ς��ꍇ�́A�ύX�t���O�𗧂Ă�B
                For i As Integer = copyFrom + 1 To copyTo
                    If Not dgvKousin(_copyPositionCol, i).Value.Equals(dgvKousin(_copyPositionCol, copyFrom).Value) Then
                        '�ύX�t���O�n�m
                        _dgv.setCellData(COLDT_CHANGEFLG, i, HENKO_FLG)
                    End If
                    dgvKousin(_copyPositionCol, i).Value = dgvKousin(_copyPositionCol, copyFrom).Value
                Next
            Finally
                '�R�s�[�J�n�ʒu�̃Z�������̐F�ɖ߂��A�ێ����Ă����J�n�ʒu�̍s�E��ԍ������Z�b�g����B
                dgvKousin(_copyPositionCol, _copyPositionRow).Style.BackColor = StartUp.lCOLOR_WHITE
                _copyPositionCol = -1
                _copyPositionRow = -1
            End Try
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '   �X�V�{�^������
    '-------------------------------------------------------------------------------
    Private Sub btnKousin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKousin.Click
        Try
            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGV�n���h���̐ݒ�

            '�ꗗ���ύX����Ă��Ȃ��ꍇ
            If _dgvChangeFlg = False Then
                Throw New UsrDefException("�ꗗ���X�V������Ă��܂���B", _msgHd.getMSG("noUpdDataGridView"))
            End If

            '���̓`�F�b�N
            Call chkBeforeUpdate()

            '���͓��e�`�F�b�N
            Call chkInputValue()

            '�o�^�m�F�_�C�A���O�\��
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmInsert")   '�o�^���܂��B��낵���ł����H
            If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            '�}�E�X�|�C���^��Ԃ̕ۑ�
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor
            Try

                'DB�X�V
                Call updateDB()

            Finally
                '�}�E�X�|�C���^��Ԃ̕���
                Me.Cursor = cur
            End Try

            '�������b�Z�[�W
            Call _msgHd.dspMSG("completeInsert")          '�o�^���������܂����B
            '��ʏ����ݒ�(�R���g���[���̏�����)
            'Call initForm()

            '�ꗗ�ύX�t���O
            _dgvChangeFlg = False

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�v��Ώەi�}�X�^�ꗗ�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnInsatu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsatu.Click
        Try

            '2�x�����h�~�̂��ߎg�p�s�Ƃ���
            btnInsatu.Enabled = False

            '�}�E�X�|�C���^��Ԃ̕ۑ�
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor
            Try

                'EXCEL�o��
                Call printExcel()

            Finally
                '�}�E�X�|�C���^��Ԃ̕���
                Me.Cursor = cur
            End Try

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        Finally
            '�G�N�Z���o�̓{�^���g�p��
            btnInsatu.Enabled = True
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�߂�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnModoru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModoru.Click

        '�x�����b�Z�[�W
        If _dgvChangeFlg Then
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmDgvEdit")   '�ҏW���̓��e���j������܂��B��낵���ł����H
            If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        '�g�����U�N�V�����L���Ȃ��������
        If _db.isTransactionOpen Then
            Call _db.rollbackTran()
        End If

        '���e�t�H�[���\��
        _parentForm.Show()
        _parentForm.Activate()
        Me.Close()

    End Sub

#End Region

#Region "���[�U��`�֐�:EXCEL�֘A"

    '------------------------------------------------------------------------------------------------------
    '�@�v��Ώەi�}�X�^�ꗗ�o��
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
                Dim openFilePath As String = StartUp.iniValue.BaseXlsPath & "\" & StartUp.iniValue.ExcelZM120R1_Base
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
                Dim wkEditFile As String = StartUp.iniValue.OutXlsPath & "\" & StartUp.iniValue.ExcelZM120R1_Out     '�R�s�[��t�@�C��

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
                        '�ėp�}�X�^���琻����������擾
                        Dim sql As String = ""
                        sql = sql & N & " SELECT KAHENKEY, NAME1 FROM M01HANYO "
                        sql = sql & N & " WHERE KOTEIKEY = '" & M01KOTEI_SEIZOBMN & "'"
                        If Not "".Equals(_serchSeizouBmn) Then
                            sql = sql & N & " AND KAHENKEY = '" & _db.rmSQ(_serchCdSeizouBmn) & "'"
                        End If
                        sql = sql & N & " ORDER BY KAHENKEY "
                        'SQL���s
                        Dim iRecCntSeizo As Integer          '�f�[�^�Z�b�g�̍s��
                        Dim dsHanyoSeizo As DataSet = _db.selectDB(sql, RS, iRecCntSeizo)

                        If iRecCntSeizo <= 0 Then                    'M01�ėp�}�X�^���o���R�[�h���P�����Ȃ��ꍇ
                            Throw New UsrDefException("�Ώۃf�[�^������܂���B", _msgHd.getMSG("NonData"))
                        End If

                        '�ėp�}�X�^���琻��敪�����擾
                        sql = ""
                        sql = sql & N & " SELECT KAHENKEY, NAME1 FROM M01HANYO "
                        sql = sql & N & " WHERE KOTEIKEY = '" & M01KOTEI_SEISAKUKBN & "'"
                        If Not "".Equals(_serchSeisakuKbn) Then
                            sql = sql & N & " AND KAHENKEY = '" & _db.rmSQ(_serchCdSeisakuKbn) & "'"
                        End If
                        sql = sql & N & " ORDER BY KAHENKEY "
                        'SQL���s
                        Dim iRecCntSeisaku As Integer          '�f�[�^�Z�b�g�̍s��
                        Dim dsHanyoSeisaku As DataSet = _db.selectDB(sql, RS, iRecCntSeisaku)

                        If iRecCntSeizo <= 0 Then                    'M01�ėp�}�X�^���o���R�[�h���P�����Ȃ��ꍇ
                            Throw New UsrDefException("�Ώۃf�[�^������܂���B", _msgHd.getMSG("NonData"))
                        End If

                        For i As Integer = 0 To iRecCntSeizo - 1

                            For j As Integer = 0 To iRecCntSeisaku - 1

                                'M11�̒l���f�[�^�Z�b�g�ɕێ�
                                Dim dsM11 As DataSet = Nothing
                                Dim rowCntM11 As Integer = 0
                                '��������A����敪���Ƃ�M11�̃f�[�^�𒊏o
                                Call getM11DataForXls(_db.rmNullStr(dsHanyoSeizo.Tables(RS).Rows(i)("KAHENKEY")), _
                                                _db.rmNullStr(dsHanyoSeisaku.Tables(RS).Rows(j)("KAHENKEY")), dsM11, rowCntM11)

                                'M12�̒l���f�[�^�Z�b�g�ɕێ�
                                Dim dsM12 As DataSet = Nothing
                                Dim rowCntM12 As Integer = 0
                                '��������A����敪���Ƃ�M12�̃f�[�^�𒊏o
                                Call getM12DataForXls(_db.rmNullStr(dsHanyoSeizo.Tables(RS).Rows(i)("KAHENKEY")), _
                                                _db.rmNullStr(dsHanyoSeisaku.Tables(RS).Rows(j)("KAHENKEY")), dsM12, rowCntM12)

                                If rowCntM11 > 0 Then

                                    '�V�[�g(���`)�𕡐��ۑ�
                                    Dim baseName As String = XLSSHEETNM_HINSYU  '���`�V�[�g��
                                    Dim newName As String = _db.rmNullStr(dsHanyoSeizo.Tables(RS).Rows(i)(HANYOU_NAME1)) & "�E" & _
                                                _db.rmNullStr(dsHanyoSeisaku.Tables(RS).Rows(j)(HANYOU_NAME1))  '�V���ɍ쐬����V�[�g
                                    Try
                                        eh.targetSheet = baseName               '���`�V�[�g�I��
                                        eh.copySheetOnLast(newName)
                                    Catch ex As Exception
                                        Throw New UsrDefException("�V�[�g�̕����Ɏ��s���܂����B", _msgHd.getMSG("failCopySheet"))
                                    End Try

                                    '�v���O���X�o�[�ݒ�
                                    pb.jobName = newName & "�o�͒��D�D�D"
                                    pb.status = ""

                                    eh.targetSheet = newName

                                    '�쐬�����ҏW
                                    Dim printDate As String = Now.ToString("yyyy/MM/dd HH:mm")
                                    eh.setValue("�쐬���� �F " & printDate, 1, 20)   'T1

                                    '���v�i�E�i���ҏW
                                    Dim kensakuStr As String = "���v��F"
                                    '���v��擾
                                    If Not "".Equals(cboJuyosaki.Text) Then
                                        kensakuStr = kensakuStr & cboJuyosaki.Text
                                    End If
                                    '�i���擾
                                    kensakuStr = kensakuStr & "   �i���R�[�h�F" & createHinmeiCd()
                                    eh.setValue(kensakuStr, 5, 1)      'A5

                                    '��������ҏW
                                    eh.setValue(_db.rmNullStr(dsHanyoSeizo.Tables(RS).Rows(i)("NAME1")), 4, 3)    'C4

                                    '����敪�ҏW
                                    eh.setValue(_db.rmNullStr(dsHanyoSeisaku.Tables(RS).Rows(j)("NAME1")), 4, 6)    'F4
                                    
                                    Dim startPrintRow As Integer = START_PRINT          '�o�͊J�n�s��

                                    Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder
                                    pb.maxVal = rowCntM11


                                    Dim k As Integer = 0        '���[�v�J�E���^�[
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
                                                If _db.rmNullStr(_db.rmNullStr(dsM11.Tables(RS).Rows(k)(COLDT_HINMEICD)).Equals _
                                                                (_db.rmNullStr(dsM12.Tables(RS2).Rows(n)(COLDT_M12KHINMEICD)))) Then
                                                    If "".Equals(sHinmeiCD) Then
                                                        sHinmeiCD = _db.rmNullStr(dsM12.Tables(RS2).Rows(n)(COLDT_M12HINMEICD))
                                                    Else
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
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_KND)) & ControlChars.Tab)            '�k���{��
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_HST)) & ControlChars.Tab)            '�Z�d������
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_MAKIWAKU)) & ControlChars.Tab)       '���g�R�[�h
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_HOUSOU)) & ControlChars.Tab)         '��敪
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_SIYOUSYONO)) & ControlChars.Tab)     '�d�l���ԍ�
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_SEISAKUKUBUN)) & ControlChars.Tab)   '����敪
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_ZAIKO)) & ControlChars.Tab)          '�݌ɌJ��
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_CHUMONSAKI)) & ControlChars.Tab)     '������
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_JUYOUSAKI)) & ControlChars.Tab)      '���v��
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_HINSYUKBN)) & ControlChars.Tab)      '�i��敪
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_SIZETENKAI)) & ControlChars.Tab)     '�T�C�Y�W�J
                                            sb.Append(sHinmeiCD & ControlChars.Tab)                                     '�W�v�i����
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_KIJUNTUKISU)) & ControlChars.Tab)    '�����
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_SAIGAI)) & ControlChars.Tab)         '�ЊQ�����݌�
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_ANZENZAIKO)) & ControlChars.Tab)     '���S�݌�
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_KAMOKUCD)) & ControlChars.Tab)       '�ȖڃR�[�h
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_SEIZOUBUMON)) & ControlChars.Tab)    '��������
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_TENKAIKBN)) & ControlChars.Tab)      '�W�J�敪
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_BUBUNKOUTEI)) & ControlChars.Tab)    '�����W�J
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_HINSITU)) & ControlChars.Tab)        '�i������
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_TATIAI)) & ControlChars.Tab)         '����L��

                                            sb.Append(ControlChars.CrLf)
                                        End With
                                    Next

                                    Clipboard.SetText(sb.ToString)
                                    eh.paste(startPrintRow, XLSNO_HINMEICD) '�ꊇ�\��t��

                                    eh.deleteRow(xlsRow + 1)
                                End If

                            Next
                        Next

                        eh.deleteSheet(XLSSHEETNM_HINSYU)
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
    '   �����̓p�����^  �FprmSeizo          �ėp�}�X�^��������̒l
    '   �����̓p�����^  �FprmSeisaku        �ėp�}�X�^����敪�̒l
    '   ���o�̓p�����^  �FprmDs             ���o���ʂ̃f�[�^�Z�b�g
    '   ���o�̓p�����^  �FprmRecCnt         ���o���ʌ���
    '-------------------------------------------------------------------------------
    Private Sub getM11DataForXls(ByVal prmSeizo As String, ByVal prmSeisaku As String, ByRef prmDs As DataSet, ByRef prmRecCnt As Integer)
        Try

            'EXCEL�p�̃f�[�^�擾
            Dim SQL As String = ""
            SQL = "SELECT "
            SQL = SQL & N & " (RPAD(TT_H_SIYOU_CD, 2) "
            SQL = SQL & N & "  || TT_H_HIN_CD "
            SQL = SQL & N & "  || TT_H_SENSIN_CD "
            SQL = SQL & N & "  || TT_H_SIZE_CD "
            SQL = SQL & N & "  || TT_H_COLOR_CD)   " & COLDT_HINMEICD       '�i���R�[�h
            SQL = SQL & N & " ,TT_HINMEI           " & COLDT_HINMEI         '�i��
            SQL = SQL & N & " ,TT_LOT              " & COLDT_LOTTYOU        '�W�����b�g��
            SQL = SQL & N & " ,TT_TANCYO           " & COLDT_TANTYOU        '����P��
            SQL = SQL & N & " ,TT_JYOSU            " & COLDT_JOSU           '���ɖ{�� �S��
            SQL = SQL & N & " ,TT_N_K_SUU          " & COLDT_KND            '���ɖ{�� �k���{�{��
            SQL = SQL & N & " ,TT_N_SH_SUU         " & COLDT_HST            '���ɖ{�� �Z�d�����{��
            SQL = SQL & N & " ,TT_MAKI_CD          " & COLDT_MAKIWAKU       '���g�R�[�h
            SQL = SQL & N & " ,TT_HOSO_KBN         " & COLDT_HOUSOU         '��敪
            SQL = SQL & N & " ,TT_SIYOUSYO_NO      " & COLDT_SIYOUSYONO     '�d�l����
            SQL = SQL & N & " ,M02.NAME1           " & COLDT_SEISAKUKUBUN   '����敪
            SQL = SQL & N & " ,M03.NAME3           " & COLDT_ZAIKO          '�݌ɌJ��
            SQL = SQL & N & " ,TT_KYAKSAKI         " & COLDT_CHUMONSAKI     '�����於
            SQL = SQL & N & " ,M04.NAME2           " & COLDT_JUYOUSAKI      '���v��
            SQL = SQL & N & " ,TT_TENKAIPTN        " & COLDT_SIZETENKAI     '�T�C�Y�W�J
            SQL = SQL & N & " ,TT_HINSYUKBN        " & COLDT_HINSYUKBN      '�i��敪
            SQL = SQL & N & " ,TT_KZAIKOTUKISU     " & COLDT_KIJUNTUKISU    '�����
            SQL = SQL & N & " ,TT_SFUKKYUU         " & COLDT_SAIGAI         '�ЊQ����
            SQL = SQL & N & " ,TT_ANNZENZAIKO      " & COLDT_ANZENZAIKO     '���S�݌�
            SQL = SQL & N & " ,TT_KAMOKU_CD        " & COLDT_KAMOKUCD       '�ȖڃR�[�h
            SQL = SQL & N & " ,M05.NAME1           " & COLDT_SEIZOUBUMON    '�������喼
            SQL = SQL & N & " ,M06.NAME2           " & COLDT_TENKAIKBN      '�W�J�敪
            SQL = SQL & N & " ,TT_KOUTEI           " & COLDT_BUBUNKOUTEI    '�����H��
            SQL = SQL & N & " ,M07.NAME1    �@     " & COLDT_HINSITU        '�i������
            SQL = SQL & N & " ,M08.NAME1           " & COLDT_TATIAI         '����L��
            SQL = SQL & N & " FROM M11KEIKAKUHIN "
            SQL = SQL & N & "   LEFT JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '" & M01KOTEI_SEISAKUKBN & "') M02 "
            SQL = SQL & N & "   ON TT_SEISAKU_KBN =  M02.KAHENKEY "
            SQL = SQL & N & "   LEFT JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '" & M01KOTEI_ZAIKO & "') M03 "
            SQL = SQL & N & "   ON TT_SYUBETU =  M03.KAHENKEY "
            SQL = SQL & N & "   LEFT JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '" & M01KOTEI_JUYOUSAKI & "') M04 "
            SQL = SQL & N & "   ON TT_JUYOUCD =  M04.KAHENKEY "
            SQL = SQL & N & "   LEFT JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '" & M01KOTEI_SEIZOBMN & "') M05 "
            SQL = SQL & N & "   ON TT_SEIZOU_BMN =  M05.KAHENKEY "
            SQL = SQL & N & "   LEFT JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '" & M01KOTEI_TENKAI & "') M06 "
            SQL = SQL & N & "   ON TT_TENKAI_KBN =  M06.KAHENKEY "
            SQL = SQL & N & "   LEFT JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '" & M01KOTEI_HINSITU & "') M07 "
            SQL = SQL & N & "   ON TT_HINSITU_KBN =  M07.KAHENKEY "
            SQL = SQL & N & "   LEFT JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '" & M01KOTEI_TATIAI & "') M08 "
            SQL = SQL & N & "   ON TT_TATIAI_UM =  M08.KAHENKEY "

            '��������
            SQL = SQL & N & "   WHERE "
            SQL = SQL & "   M05.KAHENKEY = '" & _db.rmNullStr(prmSeizo) & "'"

            '����敪
            SQL = SQL & N & "   AND "
            SQL = SQL & "   M02.KAHENKEY = '" & _db.rmNullStr(prmSeisaku) & "'"

            '���v��
            If Not "".Equals(_serchJuyosaki) Then
                SQL = SQL & N & "   AND "
                SQL = SQL & "   M04.KAHENKEY = '" & _db.rmNullStr(_serchCdJuyosaki) & "'"
            End If

            '�d�l�R�[�h
            If Not "".Equals(_serchSiyoCd) Then
                SQL = SQL & N & "   AND "

                'SQL = SQL & "   TT_H_SIYOU_CD LIKE '" & _db.rmSQ(Trim(_serchSiyoCd)) & "%'"
                SQL = SQL & "   TT_H_SIYOU_CD LIKE '" & _db.rmSQ(Trim(_serchSiyoCd.PadRight(2, " "))) & "%'"

            End If

            '�i��R�[�h
            If Not "".Equals(_serchHinsyuCd) Then
                SQL = SQL & N & "   AND "
                SQL = SQL & "   TT_H_HIN_CD LIKE '" & _db.rmSQ(Trim(_serchHinsyuCd)) & "%'"
            End If

            '���S���R�[�h
            If Not "".Equals(_serchSensinCd) Then
                SQL = SQL & N & "   AND "
                SQL = SQL & "   TT_H_SENSIN_CD LIKE '" & _db.rmSQ(Trim(_serchSensinCd)) & "%'"
            End If

            '�T�C�Y�R�[�h
            If Not "".Equals(_serchSizeCd) Then
                SQL = SQL & N & "   AND "
                SQL = SQL & "   TT_H_SIZE_CD LIKE '" & _db.rmSQ(Trim(_serchSizeCd)) & "%'"
            End If

            '�F�R�[�h
            If Not "".Equals(_serchColorCd) Then
                SQL = SQL & N & "   AND "
                SQL = SQL & "   TT_H_COLOR_CD LIKE '" & _db.rmSQ(Trim(_serchColorCd)) & "%'"
            End If

            '' 2010/12/17 upd start sugano
            'SQL = SQL & "   ORDER BY TT_SEIZOU_BMN, TT_SEISAKU_KBN, TT_JUYOUCD, "
            SQL = SQL & "   ORDER BY TT_SEIZOU_BMN, TT_SEISAKU_KBN, "
            '' 2010/12/17 upd end sugano

            SQL = SQL & "   TT_H_HIN_CD, TT_H_SENSIN_CD, TT_H_SIZE_CD, TT_H_SIYOU_CD, TT_H_COLOR_CD "

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
    '   �����̓p�����^  �FprmSeizo          �ėp�}�X�^��������̒l
    '   �����̓p�����^  �FprmSeisaku        �ėp�}�X�^����敪�̒l
    '   ���o�̓p�����^  �FprmDs             ���o���ʂ̃f�[�^�Z�b�g
    '   ���o�̓p�����^  �FprmRecCnt         ���o���ʌ���
    '-------------------------------------------------------------------------------
    Private Sub getM12DataForXls(ByVal prmSeizo As String, ByVal prmSeisaku As String, ByRef prmDs As DataSet, ByRef prmRecCnt As Integer)
        Try

            'EXCEL�p�̃f�[�^�擾
            Dim SQL As String = ""
            SQL = "SELECT "
            SQL = SQL & N & "  M12.HINMEICD " & COLDT_M12HINMEICD       '���i���R�[�h
            SQL = SQL & N & "  ,M12.KHINMEICD " & COLDT_M12KHINMEICD    '�v��i���R�[�h
            SQL = SQL & N & " FROM  M12SYUYAKU M12 "
            SQL = SQL & N & "   LEFT JOIN  M11KEIKAKUHIN M11 "
            SQL = SQL & N & "   ON M11.TT_KHINMEICD = M12.KHINMEICD "
            SQL = SQL & N & "   LEFT JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '" & M01KOTEI_SEIZOBMN & "') M05 "
            SQL = SQL & N & "   ON TT_SEIZOU_BMN =  M05.KAHENKEY "
            SQL = SQL & N & "   LEFT JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '" & M01KOTEI_SEISAKUKBN & "') M02 "
            SQL = SQL & N & "   ON TT_SEISAKU_KBN =  M02.KAHENKEY "
            SQL = SQL & N & "   LEFT JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '" & M01KOTEI_JUYOUSAKI & "') M04 "
            SQL = SQL & N & "   ON TT_JUYOUCD =  M04.KAHENKEY "
            SQL = SQL & N & "   WHERE "
            SQL = SQL & N & "   NOT M12.KHINMEICD = M12.HINMEICD "
            '��������
            SQL = SQL & N & "   AND "
            SQL = SQL & "   M05.KAHENKEY = '" & _db.rmNullStr(prmSeizo) & "'"

            '����敪
            SQL = SQL & N & "   AND "
            SQL = SQL & "   M02.KAHENKEY = '" & _db.rmNullStr(prmSeisaku) & "'"

            '���v��
            If Not "".Equals(_serchJuyosaki) Then
                SQL = SQL & N & "   AND "
                SQL = SQL & "   M04.NAME1 = '" & _db.rmNullStr(_serchCdJuyosaki) & "'"
            End If

            '�d�l�R�[�h
            If Not "".Equals(_serchSiyoCd) Then
                SQL = SQL & N & "   AND "

                'SQL = SQL & "   TT_H_SIYOU_CD LIKE '" & _db.rmSQ(Trim(_serchSiyoCd)) & "%'"
                SQL = SQL & "   TT_H_SIYOU_CD LIKE '" & _db.rmSQ(Trim(_serchSiyoCd.PadRight(2, " "))) & "%'"

            End If

            '�i��R�[�h
            If Not "".Equals(_serchHinsyuCd) Then
                SQL = SQL & N & "   AND "
                SQL = SQL & "   TT_H_HIN_CD LIKE '" & _db.rmSQ(Trim(_serchHinsyuCd)) & "%'"
            End If

            '���S���R�[�h
            If Not "".Equals(_serchSensinCd) Then
                SQL = SQL & N & "   AND "
                SQL = SQL & "   TT_H_SENSIN_CD LIKE '" & _db.rmSQ(Trim(_serchSensinCd)) & "%'"
            End If

            '�T�C�Y�R�[�h
            If Not "".Equals(_serchSizeCd) Then
                SQL = SQL & N & "   AND "
                SQL = SQL & "   TT_H_SIZE_CD LIKE '" & _db.rmSQ(Trim(_serchSizeCd)) & "%'"
            End If

            '�F�R�[�h
            If Not "".Equals(_serchColorCd) Then
                SQL = SQL & N & "   AND "
                SQL = SQL & "   TT_H_COLOR_CD LIKE '" & _db.rmSQ(Trim(_serchColorCd)) & "%'"
            End If

            SQL = SQL & "   ORDER BY TT_SEIZOU_BMN, TT_SEISAKU_KBN, TT_JUYOUCD, "
            SQL = SQL & "   TT_H_HIN_CD, TT_H_SENSIN_CD, TT_H_SIZE_CD, TT_H_SIYOU_CD, TT_H_COLOR_CD "

            'SQL���s
            prmDs = _db.selectDB(SQL, RS2, prmRecCnt)


        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�i���R�[�h�쐬
    '�@(�����T�v)EXCEL�ɏo�͂���i���R�[�h��ҏW���ĕԂ��B
    '�@�@I�@�F�@�Ȃ�
    '�@�@R�@�F�@createHinmeiCd      '�ҏW�����i���R�[�h
    '-------------------------------------------------------------------------------
    Private Function createHinmeiCd() As String
        Try
            createHinmeiCd = ""

            '�d�l�R�[�h
            If _serchSiyoCd.Length = 2 Then
                createHinmeiCd = _serchSiyoCd & "-"
            ElseIf _serchSiyoCd.Length = 1 Then
                createHinmeiCd = _serchSiyoCd.Substring(0, 1) & "*-"
            Else
                createHinmeiCd = "**-"
            End If

            '�i��R�[�h
            If _serchHinsyuCd.Length = 3 Then
                createHinmeiCd = createHinmeiCd & _serchHinsyuCd & "-"
            ElseIf _serchHinsyuCd.Length = 2 Then
                createHinmeiCd = createHinmeiCd & _serchHinsyuCd.Substring(0, 2) & "*-"
            ElseIf _serchHinsyuCd.Length = 1 Then
                createHinmeiCd = createHinmeiCd & _serchHinsyuCd.Substring(0, 1) & "**-"
            Else
                createHinmeiCd = createHinmeiCd & "***-"
            End If

            '���S���R�[�h
            If _serchSensinCd.Length = 3 Then
                createHinmeiCd = createHinmeiCd & _serchSensinCd & "-"
            ElseIf _serchSensinCd.Length = 2 Then
                createHinmeiCd = createHinmeiCd & _serchSensinCd.Substring(0, 2) & "*-"
            ElseIf _serchSensinCd.Length = 1 Then
                createHinmeiCd = createHinmeiCd & _serchSensinCd.Substring(0, 1) & "**-"
            Else
                createHinmeiCd = createHinmeiCd & "***-"
            End If

            '�T�C�Y�R�[�h
            If _serchSizeCd.Length = 2 Then
                createHinmeiCd = createHinmeiCd & _serchSizeCd & "-"
            ElseIf _serchSizeCd.Length = 1 Then
                createHinmeiCd = createHinmeiCd & _serchSizeCd.Substring(0, 1) & "*-"
            Else
                createHinmeiCd = createHinmeiCd & "**-"
            End If

            '�F�R�[�h
            If _serchColorCd.Length = 3 Then
                createHinmeiCd = createHinmeiCd & _serchColorCd
            ElseIf _serchColorCd.Length = 2 Then
                createHinmeiCd = createHinmeiCd & _serchColorCd.Substring(0, 2) & "*"
            ElseIf _serchColorCd.Length = 1 Then
                createHinmeiCd = createHinmeiCd & _serchColorCd.Substring(0, 1) & "**"
            Else
                createHinmeiCd = createHinmeiCd & "***"
            End If

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Function
#End Region

#Region "���[�U��`�֐�:��ʐ���"

    '-------------------------------------------------------------------------------
    '   ��ʏ����ݒ�
    '-------------------------------------------------------------------------------
    Private Sub initForm()
        Try

            '�{�^������
            btnKousin.Enabled = False           '�X�V�{�^��
            btnCopyPosition.Enabled = False     '�R�s�[�J�n�ʒu�{�^��
            btnCopy.Enabled = False             '�R�s�[�{�^��
            btnInsatu.Enabled = False           '�v��Ώەi�}�X�^�ꗗ�{�^��
            txtSiyou.Text = ""                  '�d�l�R�[�h
            txtHinsyu.Text = ""                 '�i��R�[�h
            txtSensin.Text = ""                 '���S���R�[�h
            txtSize.Text = ""                   '�T�C�Y�R�[�h
            txtColor.Text = ""                  '�F�R�[�h

            '�R���{�{�b�N�X�Z�b�g
            Call setCboJuyosaki()               '���v��
            Call setCboSeisakuKbn()             '����敪
            Call setCboSeizoBmn()               '��������

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    ' �@�t�H�[�J�X�ړ�
    '�@�i�����T�v�j�������ڂ̃e�L�X�g�{�b�N�X�ŃG���^�[�L�[�������͎��̃R���g���[���ֈړ�����B
    '-------------------------------------------------------------------------------
    Private Sub txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSiyou.KeyPress, _
                                                                                                    txtHinsyu.KeyPress, _
                                                                                                    txtSensin.KeyPress, _
                                                                                                    txtSize.KeyPress, _
                                                                                                    txtColor.KeyPress, _
                                                                                                    cboJuyosaki.KeyPress, _
                                                                                                    cboSeisakuKbn.KeyPress, _
                                                                                                    cboSeizouBmn.KeyPress

        UtilClass.moveNextFocus(Me, e)  '���̃R���g���[���ֈړ�����

    End Sub

    '-------------------------------------------------------------------------------
    '�@�R���g���[���S�I��
    '�@(�����T�v)�R���g���[���ړ����ɑS�I����Ԃɂ���
    '-------------------------------------------------------------------------------
    Private Sub serch_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSiyou.GotFocus, _
                                                                                            txtHinsyu.GotFocus, _
                                                                                            txtSensin.GotFocus, _
                                                                                            txtSize.GotFocus, _
                                                                                            txtColor.GotFocus, _
                                                                                            cboJuyosaki.GotFocus, _
                                                                                            cboSeisakuKbn.GotFocus, _
                                                                                            cboSeizouBmn.GotFocus
        UtilClass.selAll(sender)

    End Sub

    '-------------------------------------------------------------------------------
    ' �@�ėp�}�X�^�σL�[�̎󂯎��
    '-------------------------------------------------------------------------------
    Public Sub setKahenKey(ByVal prmKahenKey As String, ByVal prmMeisyo1 As String) Implements IfRturnKahenKey.setKahenKey
        Try

            _mz02KahenKey = prmKahenKey

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   myShow���\�b�h
    '-------------------------------------------------------------------------------
    Public Sub myShow() Implements IfRturnKahenKey.myShow
        Me.Show()
    End Sub

    '-------------------------------------------------------------------------------
    '   myActivate���\�b�h
    '-------------------------------------------------------------------------------
    Public Sub myActivate() Implements IfRturnKahenKey.myActivate
        Me.Activate()
    End Sub

#End Region

#Region "���[�U��`�֐�:DGV�֘A"

    '-------------------------------------------------------------------------------
    '   �ꗗ�̃Z���l�ύX��
    '   �i�����T�v�j�ύX���������s�̕ύX�t���O�𗧂Ă�
    '-------------------------------------------------------------------------------
    Private Sub dgvKousin_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvKousin.CellValueChanged
        Try

            '��ʋN�����͏������s��Ȃ�
            If _formOpenFlg Then
                Exit Sub
            End If

            _dgv = New UtilDataGridViewHandler(dgvKousin)
            
            '�ύX�t���O�n�m
            _dgv.setCellData(COLDT_CHANGEFLG, e.RowIndex, ZM120E_UPDFLG_ON)

            '�ꗗ�ύX�t���O
            _dgvChangeFlg = True

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '   �ꗗ�@�ҏW�`�F�b�N�iEditingControlShowing�C�x���g�j
    '   �i�����T�v�j���͂̐�����������
    '-------------------------------------------------------------------------------
    Private Sub dgvkousin_EditingControlShowing(ByVal sender As Object, _
                                              ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs _
                                              ) Handles dgvKousin.EditingControlShowing

        Try
            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGV�n���h���̐ݒ�

            If dgvKousin.CurrentCell.ColumnIndex = COLNO_LOTTYOU Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_TANTYOU Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_KND Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_HST Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_MAKIWAKU Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_ZAIKO Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_JUYOUSAKI Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_SIZETENKAI Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_KIJUNTUKISUU Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_SAIGAI Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_ANNZENNZAIKO Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_KAMOKUCD Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_SEIZOUBUMON Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_TENKAIKBN Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_HINSITU Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_TATIAI Then

                '���O���b�h�ɁA���l���̓��[�h�̐�����������
                _chkCellVO = _dgv.chkCell(sender, e, UtilDataGridViewHandler.chkType.Num)

            End If

            Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '   �I���Z�����؃C�x���g�iDataError�C�x���g�j
    '   �i�����T�v�j���l���͗��ɐ��l�ȊO�����͂��ꂽ�ꍇ�̃G���[����
    '-------------------------------------------------------------------------------
    Private Sub dgvKousin_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvKousin.DataError
        Try
            e.Cancel = False                                   '�ҏW���[�h�I��

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvKousin)
            '�O���b�h�ɂ͐��l���̓��[�h(0�`9)�̐����������Ă���̂ŁA�����̉���
            If dgvKousin.CurrentCell.ColumnIndex = COLNO_LOTTYOU Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_TANTYOU Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_KND Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_HST Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_MAKIWAKU Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_ZAIKO Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_JUYOUSAKI Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_SIZETENKAI Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_KIJUNTUKISUU Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_SAIGAI Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_ANNZENNZAIKO Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_KAMOKUCD Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_SEIZOUBUMON Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_TENKAIKBN Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_HINSITU Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_TATIAI Then
                gh.AfterchkCell(_chkCellVO)
            End If

            Dim colName As String = ""
            Select Case e.ColumnIndex
                Case COLNO_LOTTYOU
                    colName = COLDT_LOTTYOU
                Case COLNO_TANTYOU
                    colName = COLDT_TANTYOU
                Case COLNO_KND
                    colName = COLDT_KND
                Case COLNO_HST
                    colName = COLDT_HST
                Case COLNO_MAKIWAKU
                    colName = COLDT_MAKIWAKU
                Case COLNO_ZAIKO
                    colName = COLDT_ZAIKO
                Case COLNO_JUYOUSAKI
                    colName = COLDT_JUYOUSAKI
                Case COLNO_SIZETENKAI
                    colName = COLDT_SIZETENKAI
                Case COLNO_KIJUNTUKISUU
                    colName = COLDT_KIJUNTUKISU
                Case COLNO_SAIGAI
                    colName = COLDT_SAIGAI
                Case COLNO_ANNZENNZAIKO
                    colName = COLDT_ANZENZAIKO
                Case COLNO_KAMOKUCD
                    colName = COLDT_KAMOKUCD
                Case COLNO_SEIZOUBUMON
                    colName = COLDT_SEIZOUBUMON
                Case COLNO_TENKAIKBN
                    colName = COLDT_TENKAIKBN
                Case COLNO_HINSITU
                    colName = COLDT_HINSITU
                Case COLNO_TATIAI
                    colName = COLDT_TATIAI
                Case Else
                    Exit Sub
            End Select

            '���̓G���[�t���O�𗧂Ă�
            _nyuuryokuErrFlg = True

            '�G���[�Z���Ƀt�H�[�J�X�����Ă�
            Call _dgv.setCellData(colName, e.RowIndex, DBNull.Value)

            '�G���[���b�Z�[�W�\��
            Throw New UsrDefException("���p�����̂ݓ��͉\�ł��B", _msgHd.getMSG("onlyAcceptNumeric"))

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '   �Z���ҏW�㏈��
    '-------------------------------------------------------------------------------
    Private Sub dgvKousin_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvKousin.CellEndEdit

        If _nyuuryokuErrFlg Then
            dgvKousin.CancelEdit()
            Exit Sub
        End If

        Try
            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGV�n���h���̐ݒ�

            '���l���̓��[�h(0�`9)�̐������������Ă���ꍇ�́A�����̉���
            If dgvKousin.CurrentCell.ColumnIndex = COLNO_LOTTYOU Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_TANTYOU Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_KND Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_HST Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_MAKIWAKU Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_ZAIKO Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_JUYOUSAKI Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_SIZETENKAI Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_KIJUNTUKISUU Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_SAIGAI Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_ANNZENNZAIKO Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_KAMOKUCD Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_SEIZOUBUMON Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_TENKAIKBN Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_HINSITU Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_TATIAI Then
                _dgv.AfterchkCell(_chkCellVO)
            End If
            '�w�b�_�s�̏ꍇ������
            If e.RowIndex <= MZ0203_ROW_HEADER Then Exit Sub

            '�𐔂̎����v�Z
            If dgvKousin.CurrentCell.ColumnIndex = COLNO_LOTTYOU Or dgvKousin.CurrentCell.ColumnIndex = COLNO_TANTYOU Then
                Call calcJosu(e)
            End If

            '�{���̎����v�Z
            If dgvKousin.CurrentCell.ColumnIndex = COLNO_KND Or dgvKousin.CurrentCell.ColumnIndex = COLNO_HST Then
                Call calcHonsu(e)
            End If

            '���g�R�[�h�̃`�F�b�N
            If dgvKousin.CurrentCell.ColumnIndex = COLNO_MAKIWAKU Then
                If checkMakiwaku(dgvKousin.CurrentCellAddress.Y) = False Then
                    Throw New UsrDefException("���g�����}�X�^�ɓo�^����Ă��܂���B", _
                                    _msgHd.getMSG("noExistMakiwaku", "�y���g�R�[�h�z"), dgvKousin, COLCN_MAKIWAKU, dgvKousin.CurrentCellAddress.Y)
                End If
            End If

            '��\���敪�̃`�F�b�N
            If dgvKousin.CurrentCell.ColumnIndex = COLNO_HOUSOU Then
                If checkHousou(dgvKousin.CurrentCellAddress.Y) = False Then
                    Throw New UsrDefException("��^�\����ނ��}�X�^�ɓo�^����Ă��܂���B", _
                            _msgHd.getMSG("noExistHousou", "�y��\���敪�z"), dgvKousin, COLCN_HOUSOU, dgvKousin.CurrentCellAddress.Y)
                End If
            End If

            '�݌ɌJ�ԕύX��
            If dgvKousin.CurrentCell.ColumnIndex = COLNO_ZAIKO Then
                If checkZaiko(dgvKousin.CurrentCellAddress.Y) = False Then
                    Throw New UsrDefException("�ėp�}�X�^�ɓo�^����Ă��Ȃ��l�ł��B", _
                                _msgHd.getMSG("noExistHanyouMst", "�y�݌ɌJ�ԁz"), dgvKousin, COLCN_ZAIKO, dgvKousin.CurrentCellAddress.Y)
                End If
            End If

            '�݌ɌJ�� = �u1�v�̂Ƃ��݈̂ȉ��̃`�F�b�N���s���B
            If _db.rmNullInt(dgvKousin(COLNO_ZAIKO, dgvKousin.CurrentCell.RowIndex).Value) = M01KAHEN_ZAIKO_ZAIKO Then

                '���v����͎�
                If dgvKousin.CurrentCell.ColumnIndex = COLNO_JUYOUSAKI Then
                    If checkJuyo(dgvKousin.CurrentCellAddress.Y) = False Then
                        Throw New UsrDefException("�ėp�}�X�^�ɓo�^����Ă��Ȃ��l�ł��B", _
                                _msgHd.getMSG("noExistHanyouMst", "�y���v��z"), dgvKousin, COLCN_JUYOUSAKI, dgvKousin.CurrentCellAddress.Y)
                    End If
                End If

                '�T�C�Y�W�J���͎�
                If dgvKousin.CurrentCell.ColumnIndex = COLNO_SIZETENKAI Then
                    If checkSizeTenkai(dgvKousin.CurrentCellAddress.Y) = False Then
                        Throw New UsrDefException("�ėp�}�X�^�ɓo�^����Ă��Ȃ��l�ł��B", _
                                _msgHd.getMSG("noExistHanyouMst", "�y�T�C�Y�W�J�z"), dgvKousin, COLCN_SIZETENKAI, dgvKousin.CurrentCellAddress.Y)
                    End If
                End If

                '�i��敪���͎�
                If dgvKousin.CurrentCell.ColumnIndex = COLNO_HINSYUKBN Then
                    If checkHinsyuKbn(dgvKousin.CurrentCellAddress.Y) = False Then
                        Throw New UsrDefException("�i��敪���}�X�^�ɓo�^����Ă��܂���B", _
                                        _msgHd.getMSG("noHinsyuKbn", "�y�i��敪�z"), dgvKousin, COLCN_HINSYUKBN, dgvKousin.CurrentCellAddress.Y)
                    End If
                End If
            End If

            '����������͎�
            If dgvKousin.CurrentCell.ColumnIndex = COLNO_SEIZOUBUMON Then
                If checkSeizouBmn(dgvKousin.CurrentCellAddress.Y) = False Then
                    Throw New UsrDefException("�ėp�}�X�^�ɓo�^����Ă��Ȃ��l�ł��B", _msgHd.getMSG("noExistHanyouMst", "�y��������z"))
                End If
            End If

            '�W�J�敪���͎�
            If dgvKousin.CurrentCell.ColumnIndex = COLNO_TENKAIKBN Then
                If checkTenkaiKbn(dgvKousin.CurrentCellAddress.Y) = 1 Then
                    Throw New UsrDefException("�ėp�}�X�^�ɓo�^����Ă��Ȃ��l�ł��B", _
                            _msgHd.getMSG("noExistHanyouMst", "�y�W�J�敪�z"), dgvKousin, COLCN_TENKAIKBN, dgvKousin.CurrentCellAddress.Y)
                ElseIf checkTenkaiKbn(dgvKousin.CurrentCellAddress.Y) = 2 Then
                    Throw New UsrDefException("����敪�u�O���v���͓W�J�敪�u�����W�J�v�ȊO�I���ł��܂���B", _
                            _msgHd.getMSG("nonGaicyuSelect"), dgvKousin, COLCN_TENKAIKBN, dgvKousin.CurrentCellAddress.Y)
                End If
            End If

            '�i�������敪���͎�
            If dgvKousin.CurrentCell.ColumnIndex = COLNO_HINSITU Then
                If checkHinsitu(dgvKousin.CurrentCellAddress.Y) = False Then
                    Throw New UsrDefException("�ėp�}�X�^�ɓo�^����Ă��Ȃ��l�ł��B", _
                                _msgHd.getMSG("noExistHanyouMst", "�y�i����������z"), dgvKousin, COLCN_HINSITU, dgvKousin.CurrentCellAddress.Y)
                End If
            End If

            '����L�����͎�
            If dgvKousin.CurrentCell.ColumnIndex = COLNO_TATIAI Then
                If checkTatiai(dgvKousin.CurrentCellAddress.Y) = False Then
                    Throw New UsrDefException("�ėp�}�X�^�ɓo�^����Ă��Ȃ��l�ł��B", _
                                _msgHd.getMSG("noExistHanyouMst", "�y����L���z"), dgvKousin, COLCN_TATIAI, dgvKousin.CurrentCellAddress.Y)
                End If
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    ' �@�O���b�h�t�H�[�J�X�ݒ�
    '�@�i�����T�v�j�Z���ҏW��ɃG���[�ɂȂ����ꍇ�ɁA�G���[�Z���Ƀt�H�[�J�X��߂��B
    '-------------------------------------------------------------------------------
    Private Sub dgvKousin_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvKousin.SelectionChanged
        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvKousin)

            '���̓G���[���������ꍇ
            If _nyuuryokuErrFlg Then
                _nyuuryokuErrFlg = False
                '�t�H�[�J�X����̓G���[�Z���Ɉڂ�
                gh.setCurrentCell(_errSet)
                Else
                End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�@�I���s�ɒ��F���鏈��
    '�@�@(�����T�v�j�I���s�ɒ��F����B
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvSeisanMst_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvKousin.CellEnter
        Try

            If _colorCtlFlg Then
                Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvKousin)
                '�w�i�F�̐ݒ�
                Call setBackcolor(dgvKousin.CurrentCellAddress.Y, _oldRowIndex)

            End If
            _oldRowIndex = dgvKousin.CurrentCellAddress.Y

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�w�i�F�̐ݒ菈��
    '�@(�����T�v)�s�̔w�i���ɂ��A�{�^���̗�����ɖ߂��B
    '�@�@I�@�F�@prmRowIndex     ���݃t�H�[�J�X������s��
    '�@�@I�@�F�@prmOldRowIndex  ���݂̍s�Ɉڂ�O�̍s��
    '------------------------------------------------------------------------------------------------------
    Private Sub setBackcolor(ByVal prmRowIndex As Integer, ByVal prmOldRowIndex As Integer)
        Try
            Dim _dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvKousin)

            '�w�肵���s�̔w�i�F��ɂ���
            _dgv.setSelectionRowColor(prmRowIndex, prmOldRowIndex, StartUp.lCOLOR_BLUE)

            '�{�^����̐F���ς���Ă��܂��̂ŁA�߂�����
            Call colBtnColorSilver(prmRowIndex)

            _oldRowIndex = prmRowIndex

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    ' �@�{�^�����F
    '�@�i�����T�v�j�u���[�ɒ��F���ꂽ�{�^�������ɖ߂�
    '-------------------------------------------------------------------------------
    Private Sub colBtnColorSilver(ByVal prmNewRowIdx As Integer)

        dgvKousin(COLNO_ZAIKOBTN, prmNewRowIdx).Style.BackColor = SystemColors.Control
        dgvKousin(COLNO_JUYOUSAKIBTN, prmNewRowIdx).Style.BackColor = SystemColors.Control
        dgvKousin(COLNO_SIZETENKAIBTN, prmNewRowIdx).Style.BackColor = SystemColors.Control
        dgvKousin(COLNO_SYUUKEIHINMEIBTN, prmNewRowIdx).Style.BackColor = SystemColors.Control
        dgvKousin(COLNO_TENKAIKBNBTN, prmNewRowIdx).Style.BackColor = SystemColors.Control
        dgvKousin(COLNO_HINSITUBTN, prmNewRowIdx).Style.BackColor = SystemColors.Control
        dgvKousin(COLNO_TATIAIBTN, prmNewRowIdx).Style.BackColor = SystemColors.Control
        dgvKousin(COLNO_SEIZOUBUMONBTN, prmNewRowIdx).Style.BackColor = SystemColors.Control

    End Sub

    '-------------------------------------------------------------------------------
    '   �V�[�g�R�}���h�{�^������
    '-------------------------------------------------------------------------------
    Private Sub dgvKousin_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvKousin.CellContentClick
        Try
            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGV�n���h���̐ݒ�

            Dim nowRow As Integer = e.RowIndex   '���݂̍s��

            If Not e.ColumnIndex.Equals(COLNO_SYUUKEIHINMEIBTN) Then
                '�R�[�h�I���q���
                Dim koteiKey As String = ""         '�R�[�h�I���q��ʂɓn���Œ�L�[
                Dim kahenKey As String = ""         '�R�[�h�I���q��ʂɓn���σL�[
                Dim colName As String = ""          '��
                Dim selectCol As String = "NAME1"   '�R�[�h�I���q��ʂɕ\������ėp�}�X�^�̗�

                Select Case e.ColumnIndex
                    Case COLNO_ZAIKOBTN            '�݌ɌJ�ԑI���{�^��
                        koteiKey = M01KOTEI_ZAIKO
                        kahenKey = _db.rmNullStr(dgvKousin(COLNO_ZAIKO, e.RowIndex).Value)
                        colName = COLDT_ZAIKO
                    Case COLNO_JUYOUSAKIBTN             '���v��R�[�h�I���{�^��
                        koteiKey = M01KOTEI_JUYOUSAKI
                        kahenKey = _db.rmNullStr(dgvKousin(COLNO_JUYOUSAKI, e.RowIndex).Value)
                        colName = COLDT_JUYOUSAKI
                        selectCol = "NAME2"
                    Case COLNO_SIZETENKAIBTN           '�T�C�Y�W�J�R�[�h�I���{�^��
                        koteiKey = M01KOTEI_SIZETENKAI
                        kahenKey = _db.rmNullStr(dgvKousin(COLNO_SIZETENKAI, e.RowIndex).Value)
                        colName = COLDT_SIZETENKAI
                    Case COLNO_SEIZOUBUMONBTN           '��������R�[�h�I���{�^��
                        koteiKey = M01KOTEI_SEIZOBMN
                        kahenKey = _db.rmNullStr(dgvKousin(COLNO_SEIZOUBUMON, e.RowIndex).Value)
                        colName = COLDT_SEIZOUBUMON
                    Case COLNO_TENKAIKBNBTN             '�W�J�敪�R�[�h�I���{�^��
                        koteiKey = M01KOTEI_TENKAI
                        kahenKey = _db.rmNullStr(dgvKousin(COLNO_TENKAIKBN, e.RowIndex).Value)
                        colName = COLDT_TENKAIKBN
                    Case COLNO_HINSITUBTN          '�i�������敪�I���{�^��
                        koteiKey = M01KOTEI_HINSITU
                        kahenKey = _db.rmNullStr(dgvKousin(COLNO_HINSITU, e.RowIndex).Value)
                        colName = COLDT_HINSITU
                    Case COLNO_TATIAIBTN                '����L���I���{�^��
                        koteiKey = M01KOTEI_TATIAI
                        kahenKey = _db.rmNullStr(dgvKousin(COLNO_TATIAI, e.RowIndex).Value)
                        colName = COLDT_TATIAI
                    Case Else
                        Exit Sub
                End Select

                '�R�[�h�I����ʕ\��
                Dim openForm As ZC910S_CodeSentaku = New ZC910S_CodeSentaku(_msgHd, _db, Me, koteiKey, kahenKey, selectCol, )      '�p�����^��J�ڐ��ʂ֓n��
                openForm.ShowDialog(Me)                                                             '��ʕ\��
                openForm.Dispose()

                If Not "".Equals(_mz02KahenKey) Then
                    '�R�[�h�I����ʂőI�������敪��\��
                    _dgv.setCellData(colName, e.RowIndex, _mz02KahenKey)
                    '�ύX�t���O�n�m
                    _dgv.setCellData(COLDT_CHANGEFLG, e.RowIndex, ZM120E_UPDFLG_ON)
                    _dgvChangeFlg = True
                    '�R�[�h�ɑΉ����郉�x���̍ĕ\��
                    If colName.Equals(COLDT_JUYOUSAKI) Then         '���v��
                        Call checkJuyo(e.RowIndex)
                    ElseIf colName.Equals(COLDT_SEIZOUBUMON) Then   '��������
                        Call checkSeizouBmn(e.RowIndex)
                    ElseIf colName.Equals(COLDT_TENKAIKBN) Then     '�W�J�敪
                        Call checkTenkaiKbn(e.RowIndex)
                    End If
                End If

                '�݌ɌJ�Ԃ��u1�v�ȊO�̏ꍇ
                If e.ColumnIndex.Equals(COLNO_ZAIKOBTN) Then
                    Call checkZaiko(e.RowIndex)
                End If
            Else
                '�W�v�Ώەi���o�^���
                '�R�[�h�I����ʕ\��
                Dim openForm As ZM121S_SyuukeiTouroku = New ZM121S_SyuukeiTouroku(_msgHd, _db, Me, _dgv.getCellData(COLDT_HINMEICD, e.RowIndex).ToString)      '�p�����^��J�ڐ��ʂ֓n��
                openForm.ShowDialog(Me)                                                             '��ʕ\��
                openForm.Dispose()

                '�W�v�i�����̍ĕ\��
                Call reDispSyukeiHinmei(e.RowIndex)

                dgvKousin.CurrentCell = dgvKousin(COLNO_SYUUKEIHINMEIBTN, nowRow)
                _dgv.setSelectionRowColor(nowRow, _oldRowIndex, StartUp.lCOLOR_BLUE)

                '�{�^����̐F���V���o�[�ɖ߂�
                Call colBtnColorSilver(nowRow)

                _oldRowIndex = nowRow

            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   �𐔎����v�Z
    '�@�i�����T�v�j�W�����b�g���E�P���̗��������͂���Ă���ꍇ�A�𐔂������v�Z����B
    '   �����̓p�����^  �FprmE      DGV�C�x���g
    '-------------------------------------------------------------------------------
    Private Sub calcJosu(ByVal prmE As System.Windows.Forms.DataGridViewCellEventArgs)
        Try
            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGV�n���h���̐ݒ�

            Dim lottyo As String = _dgv.getCellData(COLDT_LOTTYOU, prmE.RowIndex)
            Dim tantyo As String = _dgv.getCellData(COLDT_TANTYOU, prmE.RowIndex)
            Dim josu As String = ""

            If Not "".Equals(lottyo) And Not "".Equals(tantyo) Then
                '�W�����b�g���E����P�����͒l�`�F�b�N(�W�����b�g��������P��=����)
                If CInt(lottyo) Mod CInt(tantyo) <> 0 Then
                    _dgv.setCellData(COLDT_JOSU, prmE.RowIndex, 0)
                    Exit Sub
                Else
                    josu = CStr(CInt(lottyo) / CInt(tantyo))
                    _dgv.setCellData(COLDT_JOSU, prmE.RowIndex, josu)
                    '�𐔕ύX�ɔ����{���̕ύX
                    If Not "".Equals(_dgv.getCellData(COLDT_KND, prmE.RowIndex)) Then
                        _dgv.setCellData(COLDT_HST, prmE.RowIndex, CStr(josu - CInt(_dgv.getCellData(COLDT_KND, prmE.RowIndex))))
                    ElseIf Not "".Equals(_dgv.getCellData(COLDT_HST, prmE.RowIndex)) Then
                        _dgv.setCellData(COLDT_KND, prmE.RowIndex, CStr(josu - CInt(_dgv.getCellData(COLDT_HST, prmE.RowIndex))))
                    End If

                End If
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '   �{�������v�Z
    '�@�i�����T�v�j�𐔂Ɩk���{���܂��͏Z�d�����������͂���Ă���ꍇ�A�{���������v�Z����B
    '   �����̓p�����^  �FprmE      DGV�C�x���g
    '-------------------------------------------------------------------------------
    Private Sub calcHonsu(ByVal prmE As System.Windows.Forms.DataGridViewCellEventArgs)
        Try

            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGV�n���h���̐ݒ�

            Dim josu As String = _dgv.getCellData(COLDT_JOSU, prmE.RowIndex)

            If "".Equals(josu) Then Exit Sub

            Dim sumi As String = _dgv.getCellData(COLDT_HST, prmE.RowIndex)
            Dim knd As String = _dgv.getCellData(COLDT_KND, prmE.RowIndex)

            If prmE.ColumnIndex = COLNO_KND And Not "".Equals(knd) Then
                _dgv.setCellData(COLDT_HST, prmE.RowIndex, CStr(CInt(josu) - CInt(knd)))
            ElseIf prmE.ColumnIndex = COLNO_HST And Not "".Equals(sumi) Then
                _dgv.setCellData(COLDT_KND, prmE.RowIndex, CStr(CInt(josu) - CInt(sumi)))
            Else
                Exit Sub
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '   ���g�R�[�h���݃`�F�b�N
    '�@�i�����T�v�j���͂��ꂽ���g�����g�R�[�h�e�[�u���ɑ��݂��Ă��邩�`�F�b�N����B
    '   �����̓p�����^  �FprmRowIndex       DGV��ԍ�
    '-------------------------------------------------------------------------------
    Private Function checkMakiwaku(ByVal prmRowCnt As Integer) As Boolean
        Try
            checkMakiwaku = True

            '��̂Ƃ��̓`�F�b�N���Ȃ�
            If DBNull.Value.Equals(dgvKousin(COLNO_MAKIWAKU, prmRowCnt).Value) Then
                Exit Function
            End If

            '-->2010.12.25 add by takagi #48
            '999999�̂Ƃ����`�F�b�N���Ȃ�
            Dim tmpVal As String = CStr(dgvKousin(COLNO_MAKIWAKU, prmRowCnt).Value)
            If "999999".Equals(tmpVal) Then
                Exit Function
            End If
            '<--2010.12.25 add by takagi #48

            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGV�n���h���̐ݒ�

            '���g�R�[�h���݃`�F�b�N
            If Not serchMakiwaku(CStr(dgvKousin(COLNO_MAKIWAKU, prmRowCnt).Value)) Then
                checkMakiwaku = False
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Function

    '-------------------------------------------------------------------------------
    '   ��\���敪�R�[�h���݃`�F�b�N
    '�@�i�����T�v�j���͂��ꂽ��\���敪����e�[�u���ɑ��݂��Ă��邩�`�F�b�N����B
    '   �����̓p�����^  �FprmRowIndex       DGV��ԍ�
    '-------------------------------------------------------------------------------
    Private Function checkHousou(ByVal prmRowCnt As Integer) As Boolean
        Try
            checkHousou = True

            '��̂Ƃ��̓`�F�b�N���Ȃ�
            If "".Equals(_db.rmNullStr(dgvKousin(COLNO_HOUSOU, prmRowCnt).Value)) Then
                'checkHousou = False
                Exit Function
            End If

            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGV�n���h���̐ݒ�

            '��\���敪���݃`�F�b�N
            If Not serchHousou(CStr(dgvKousin(COLNO_HOUSOU, prmRowCnt).Value)) Then
                checkHousou = False
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Function

    '-------------------------------------------------------------------------------
    '   �݌ɌJ�ԓ��͎�����
    '�@�i�����T�v�j�@�݌ɌJ�Ԗ����ėp�}�X�^�ɂ��邩�`�F�b�N����B
    '�@�@�@�@�@�@�@�A�݌ɌJ�Ԃ��u1�v�ȊO�̏ꍇ�́A�ȉ��̍��ڂ��N���A����B
    '                   1.���v��
    '                   2.���v�於
    '                   3.�T�C�Y�W�J
    '                   4.�i��敪
    '                   5.�����
    '                   6.�ЊQ�����p�݌ɗ�
    '                   7.���S�݌ɗ�
    '                   8.�W�v�i����
    '   �����̓p�����^  �FprmRowIndex       DGV��ԍ�
    '-------------------------------------------------------------------------------
    Private Function checkZaiko(ByVal prmRowCnt As Integer) As Boolean
        Try

            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGV�n���h���̐ݒ�

            '�݌ɌJ�ԑ��݃`�F�b�N
            Dim name As String = backNameFromM01(M01KOTEI_ZAIKO, _db.rmNullStr(dgvKousin(COLNO_ZAIKO, prmRowCnt).Value))
            If "".Equals(name) Then

                Return False

            Else

                '�߂�l���݌ɌJ�Ԗ��ɕ\��
                _dgv.setCellData(COLDT_ZAIKONM, prmRowCnt, name)

                '�݌ɌJ�Ԃ��u1�v�ȊO�̏ꍇ�͏�L�R���g���[���N���A
                If Not CStr(M01KAHEN_ZAIKO_ZAIKO).Equals(_db.rmNullStr(dgvKousin(COLNO_ZAIKO, prmRowCnt).Value)) Then

                    _dgv.setCellData(COLDT_JUYOUSAKI, prmRowCnt, DBNull.Value)          '���v��
                    _dgv.setCellData(COLDT_JUYOUSAKINAME, prmRowCnt, DBNull.Value)      '���v�於
                    _dgv.setCellData(COLDT_SIZETENKAI, prmRowCnt, DBNull.Value)         '�T�C�Y�W�J
                    _dgv.setCellData(COLDT_HINSYUKBN, prmRowCnt, DBNull.Value)          '�i��敪
                    _dgv.setCellData(COLDT_KIJUNTUKISU, prmRowCnt, DBNull.Value)        '�����
                    _dgv.setCellData(COLDT_SAIGAI, prmRowCnt, DBNull.Value)             '�ЊQ�����p�݌ɐ�
                    _dgv.setCellData(COLDT_ANZENZAIKO, prmRowCnt, DBNull.Value)         '���S�݌ɐ�
                    _dgv.setCellData(COLDT_SHINMEI, prmRowCnt, DBNull.Value)            '�W�v�i����

                    '�ύX�t���O�n�m
                    _dgv.setCellData(COLDT_CHANGEFLG, prmRowCnt, ZM120E_UPDFLG_ON)

                    '�ꗗ�ύX�t���O
                    _dgvChangeFlg = True

                End If
                Return True
            End If
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Function

    '-------------------------------------------------------------------------------
    '   ���v����͎�����
    '�@�i�����T�v�j�@���v�於���ėp�}�X�^�ɂ��邩�`�F�b�N����B
    '   �����̓p�����^  �FprmRowIndex       DGV��ԍ�
    '-------------------------------------------------------------------------------
    Private Function checkJuyo(ByVal prmRowCnt As Integer) As Boolean
        Try
            checkJuyo = True
            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGV�n���h���̐ݒ�

            '���v�摶�݃`�F�b�N
            Dim name As String = backNameFromM01(M01KOTEI_JUYOUSAKI, _db.rmNullStr(dgvKousin(COLNO_JUYOUSAKI, prmRowCnt).Value))
            If "".Equals(name) Then

                checkJuyo = False

            Else

                '�߂�l�����v�於�ɕ\��
                _dgv.setCellData(COLDT_JUYOUSAKINAME, prmRowCnt, name)

                '�i��敪���N���A
                '_dgv.setCellData(COLDT_HINSYUKBN, prmRowCnt, DBNull.Value)

            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Function

    '-------------------------------------------------------------------------------
    '   �T�C�Y�W�J���͎�����
    '�@�i�����T�v�j�T�C�Y�W�J���ėp�}�X�^�ɂ��邩�`�F�b�N����B
    '   �����̓p�����^  �FprmRowIndex       DGV��ԍ�
    '-------------------------------------------------------------------------------
    Private Function checkSizeTenkai(ByVal prmRowCnt As Integer) As Boolean
        Try
            checkSizeTenkai = True
            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGV�n���h���̐ݒ�

            '�T�C�Y�W�J���݃`�F�b�N
            Dim name As String = backNameFromM01(M01KOTEI_SIZETENKAI, _db.rmNullStr(dgvKousin(COLNO_SIZETENKAI, prmRowCnt).Value))
            If "".Equals(Name) Then

                checkSizeTenkai = False

            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Function

    '-------------------------------------------------------------------------------
    '   �i��敪���͎�����
    '�@�i�����T�v�j�i��敪�Ǝ��v��̑g�ݍ��킹�����������`�F�b�N����B
    '   �����̓p�����^  �FprmRowIndex       DGV��ԍ�
    '-------------------------------------------------------------------------------
    Private Function checkHinsyuKbn(ByVal prmRowIndex As Integer) As Boolean

        Try
            checkHinsyuKbn = True

            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGV�n���h���̐ݒ�

            '�i��敪�`�F�b�N
            If Not hinsyuKbnExist(_db.rmNullStr(dgvKousin(COLNO_HINSYUKBN, prmRowIndex).Value), _db.rmNullStr(dgvKousin(COLNO_JUYOUSAKI, prmRowIndex).Value)) Then
                checkHinsyuKbn = False
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Function

    '-------------------------------------------------------------------------------
    '   ����������͎�����
    '�@�i�����T�v�j�������喼���ėp�}�X�^�ɂ��邩�`�F�b�N����B
    '   �����̓p�����^  �FprmRowIndex       DGV��ԍ�
    '-------------------------------------------------------------------------------
    Private Function checkSeizouBmn(ByVal prmRowIndex As Integer) As Boolean
        Try
            checkSeizouBmn = True
            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGV�n���h���̐ݒ�

            '�������呶�݃`�F�b�N
            Dim name As String = backNameFromM01(M01KOTEI_SEIZOBMN, _db.rmNullStr(dgvKousin(COLNO_SEIZOUBUMON, prmRowIndex).Value))
            If "".Equals(name) Then

                checkSeizouBmn = False

            Else

                '�߂�l�𐻑����喼�ɕ\��
                _dgv.setCellData(COLDT_SEIZOUBUMONNM, prmRowIndex, name)

            End If
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Function

    '-------------------------------------------------------------------------------
    '   �W�J�敪���͎�����
    '�@�i�����T�v�j�W�J�敪���ėp�}�X�^�ɂ��邩�`�F�b�N����B
    '   �����̓p�����^  �FprmRowIndex       DGV��ԍ�
    '-------------------------------------------------------------------------------
    Private Function checkTenkaiKbn(ByVal prmRowIndex As Integer) As Integer
        Try
            checkTenkaiKbn = 0
            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGV�n���h���̐ݒ�

            '�����͂Ȃ珈�����s��Ȃ�
            If "".Equals(_db.rmNullStr(dgvKousin(COLNO_TENKAIKBN, prmRowIndex).Value)) Then Exit Function

            '�W�J�敪���݃`�F�b�N
            Dim name As String = backNameFromM01(M01KOTEI_TENKAI, _db.rmNullStr(dgvKousin(COLNO_TENKAIKBN, prmRowIndex).Value))
            If "".Equals(name) Then

                checkTenkaiKbn = 1
                
            ElseIf M01NAME2_SEISAKUKBN.Equals(_db.rmNullStr(dgvKousin(COLNO_SEISAKUKBN, prmRowIndex).Value)) And _
                        Not "2".Equals(_db.rmNullStr(dgvKousin(COLNO_TENKAIKBN, prmRowIndex).Value)) Then

                checkTenkaiKbn = 2

            Else

                checkTenkaiKbn = 0
                '�߂�l�𐻑����喼�ɕ\��
                _dgv.setCellData(COLDT_TENKAIKBNNM, prmRowIndex, name)

            End If
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Function

    '-------------------------------------------------------------------------------
    '   �i�������敪���͎�����
    '�@�i�����T�v�j�i�������敪���ėp�}�X�^�ɂ��邩�`�F�b�N����B
    '   �����̓p�����^  �FprmRowIndex       DGV��ԍ�
    '-------------------------------------------------------------------------------
    Private Function checkHinsitu(ByVal prmRowIndex As Integer) As Boolean
        Try
            checkHinsitu = True
            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGV�n���h���̐ݒ�

            '�i�������敪���݃`�F�b�N
            Dim name As String = backNameFromM01(M01KOTEI_HINSITU, _db.rmNullStr(dgvKousin(COLNO_HINSITU, prmRowIndex).Value))
            If "".Equals(name) Then

                checkHinsitu = False
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Function

    '-------------------------------------------------------------------------------
    '   ����L�����͎�����
    '�@�i�����T�v�j����L�����ėp�}�X�^�ɂ��邩�`�F�b�N����B
    '   �����̓p�����^  �FprmRowIndex       DGV��ԍ�
    '-------------------------------------------------------------------------------
    Private Function checkTatiai(ByVal prmRowIndex As Integer) As Boolean
        Try
            checkTatiai = True
            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGV�n���h���̐ݒ�

            '����L�����݃`�F�b�N
            Dim name As String = backNameFromM01(M01KOTEI_TATIAI, _db.rmNullStr(dgvKousin(COLNO_TATIAI, prmRowIndex).Value))
            If "".Equals(name) Then

                checkTatiai = False
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Function

#End Region

#Region "���[�U��`�֐�:DB�֘A"

    '-------------------------------------------------------------------------------
    '   �ꗗ�\��
    '   (�����T�v)���͂��ꂽ�������������ƂɁA�ꗗ�Ƀf�[�^��\������B
    '-------------------------------------------------------------------------------
    Private Sub dispData()
        Try

            Dim SQL As String = ""
            SQL = "SELECT "
            SQL = SQL & N & " (MAX(RPAD(TT_H_SIYOU_CD,2)) "
            SQL = SQL & N & "  || MAX(TT_H_HIN_CD) "
            SQL = SQL & N & "  || MAX(TT_H_SENSIN_CD) "
            SQL = SQL & N & "  || MAX(TT_H_SIZE_CD) "
            SQL = SQL & N & "  || MAX(TT_H_COLOR_CD) )  " & COLDT_HINMEICD          '�i���R�[�h
            SQL = SQL & N & " ,MAX(TT_HINMEI)           " & COLDT_HINMEI            '�i��
            SQL = SQL & N & " ,MAX(TT_LOT)              " & COLDT_LOTTYOU           '�W�����b�g��
            SQL = SQL & N & " ,MAX(TT_TANCYO)           " & COLDT_TANTYOU    '����P��
            SQL = SQL & N & " ,MAX(TT_JYOSU)            " & COLDT_JOSU              '���ɖ{�� �S��
            SQL = SQL & N & " ,MAX(TT_N_K_SUU)          " & COLDT_KND           '���ɖ{�� �k���{�{��
            SQL = SQL & N & " ,MAX(TT_N_SH_SUU)         " & COLDT_HST         '���ɖ{�� �Z�d�����{��
            SQL = SQL & N & " ,MAX(TT_MAKI_CD)          " & COLDT_MAKIWAKU          '���g�R�[�h
            SQL = SQL & N & " ,MAX(TT_HOSO_KBN)         " & COLDT_HOUSOU            '��敪
            SQL = SQL & N & " ,MAX(TT_SIYOUSYO_NO)      " & COLDT_SIYOUSYONO        '�d�l����
            SQL = SQL & N & " ,MAX(M02.NAME2)           " & COLDT_SEISAKUKUBUN      '����敪
            SQL = SQL & N & " ,MAX(TT_SYUBETU)          " & COLDT_ZAIKO        '�݌ɌJ��
            SQL = SQL & N & " ,''                       " & COLDT_ZAIKOBTN     '�݌ɌJ�ԃ{�^��
            SQL = SQL & N & " ,MAX(M03.NAME2)           " & COLDT_ZAIKONM      '�݌ɌJ�Ԗ�
            SQL = SQL & N & " ,MAX(TT_KYAKSAKI)         " & COLDT_CHUMONSAKI        '�����於
            SQL = SQL & N & " ,MAX(TT_JUYOUCD)          " & COLDT_JUYOUSAKI         '���v��
            SQL = SQL & N & " ,''                       " & COLDT_JUYOUSAKIBTN      '���v��{�^��
            SQL = SQL & N & " ,MAX(M04.NAME2)           " & COLDT_JUYOUSAKINAME     '���v�於
            SQL = SQL & N & " ,MAX(TT_TENKAIPTN)        " & COLDT_SIZETENKAI        '�T�C�Y�W�J
            SQL = SQL & N & " ,''                       " & COLDT_SIZETENKAIBTN     '�T�C�Y�W�J�{�^��
            SQL = SQL & N & " ,MAX(TT_HINSYUKBN)        " & COLDT_HINSYUKBN         '�i��敪
            SQL = SQL & N & " ,MAX(TT_KZAIKOTUKISU)     " & COLDT_KIJUNTUKISU       '�����
            SQL = SQL & N & " ,MAX(TT_SFUKKYUU)         " & COLDT_SAIGAI            '�ЊQ����
            SQL = SQL & N & " ,MAX(TT_ANNZENZAIKO)      " & COLDT_ANZENZAIKO        '���S�݌�
            SQL = SQL & N & " ,COUNT(M12.HINMEICD) - 1  " & COLDT_SHINMEI           '�W�v�i����
            SQL = SQL & N & " ,''                       " & COLDT_SHINMEIBTN        '�W�v�i���{�^��
            SQL = SQL & N & " ,MAX(TT_KAMOKU_CD)        " & COLDT_KAMOKUCD          '�ȖڃR�[�h
            SQL = SQL & N & " ,MAX(TT_SEIZOU_BMN)       " & COLDT_SEIZOUBUMON       '��������R�[�h
            SQL = SQL & N & " ,''                       " & COLDT_SEIZOUBUMONBTN    '��������R�[�h�{�^��
            SQL = SQL & N & " ,MAX(M05.NAME2)           " & COLDT_SEIZOUBUMONNM     '�������喼
            SQL = SQL & N & " ,MAX(TT_TENKAI_KBN)       " & COLDT_TENKAIKBN         '�W�J�敪
            SQL = SQL & N & " ,''                       " & COLDT_TENKAIKBNBTN      '�W�J�敪�{�^��
            SQL = SQL & N & " ,MAX(M06.NAME2)           " & COLDT_TENKAIKBNNM       '�W�J�敪��
            SQL = SQL & N & " ,MAX(TT_KOUTEI)           " & COLDT_BUBUNKOUTEI       '�����H��
            SQL = SQL & N & " ,MAX(TT_HINSITU_KBN)      " & COLDT_HINSITU           '�i������
            SQL = SQL & N & " ,''                       " & COLDT_HINSITUBTN        '�i�������{�^��
            SQL = SQL & N & " ,MAX(TT_TATIAI_UM)        " & COLDT_TATIAI            '����L��
            SQL = SQL & N & " ,''                       " & COLDT_TATIAIBTN         '����L���{�^��
            SQL = SQL & N & " ,'0'                      " & COLDT_CHANGEFLG         '�ύX�t���O
            SQL = SQL & N & " FROM M11KEIKAKUHIN "
            SQL = SQL & N & "   LEFT JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '" & M01KOTEI_SEISAKUKBN & "') M02 "
            SQL = SQL & N & "   ON TT_SEISAKU_KBN =  M02.KAHENKEY "
            SQL = SQL & N & "   LEFT JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '" & M01KOTEI_ZAIKO & "') M03 "
            SQL = SQL & N & "   ON TT_SYUBETU =  M03.KAHENKEY "
            SQL = SQL & N & "   LEFT JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '" & M01KOTEI_JUYOUSAKI & "') M04 "
            SQL = SQL & N & "   ON TT_JUYOUCD =  M04.KAHENKEY "
            SQL = SQL & N & "   LEFT JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '" & M01KOTEI_SEIZOBMN & "') M05 "
            SQL = SQL & N & "   ON TT_SEIZOU_BMN =  M05.KAHENKEY "
            SQL = SQL & N & "   LEFT JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '" & M01KOTEI_TENKAI & "') M06 "
            SQL = SQL & N & "   ON TT_TENKAI_KBN =  M06.KAHENKEY "
            SQL = SQL & N & "   LEFT JOIN M12SYUYAKU M12 "
            SQL = SQL & N & "   ON TT_KHINMEICD = M12.KHINMEICD "

            Dim sqlWhere As Boolean = False             '�uAND�v��t����t���O
            '��������
            If Not "".Equals(_serchSeizouBmn) Then
                SQL = SQL & N & "   WHERE "
                SQL = SQL & "   M05.KAHENKEY = '" & _db.rmSQ(_serchCdSeizouBmn) & "'"
                sqlWhere = True
            End If
            '����敪
            If Not "".Equals(_serchSeisakuKbn) Then
                If sqlWhere Then
                    SQL = SQL & N & "   AND "
                Else
                    SQL = SQL & N & "   WHERE "
                End If
                SQL = SQL & "   M02.KAHENKEY = '" & _db.rmSQ(_serchCdSeisakuKbn) & "'"
                sqlWhere = True
            End If
            '���v��
            If Not "".Equals(_serchJuyosaki) Then
                If sqlWhere Then
                    SQL = SQL & N & "   AND "
                Else
                    SQL = SQL & N & "   WHERE "
                End If
                SQL = SQL & "   M04.KAHENKEY = '" & _db.rmSQ(_serchCdJuyosaki) & "'"
                sqlWhere = True
            End If
            '�d�l�R�[�h
            If Not "".Equals(txtSiyou.Text) Then
                If sqlWhere Then
                    SQL = SQL & N & "   AND "
                Else
                    SQL = SQL & N & "   WHERE "
                End If

                'SQL = SQL & "   TT_H_SIYOU_CD LIKE '" & _db.rmSQ(Trim(txtSiyou.Text)) & "%'"
                SQL = SQL & "   TT_H_SIYOU_CD LIKE '" & _db.rmSQ(Trim(txtSiyou.Text.PadRight(2, " "))) & "%'"

                sqlWhere = True
            End If
            '�i��R�[�h
            If Not "".Equals(txtHinsyu.Text) Then
                If sqlWhere Then
                    SQL = SQL & N & "   AND "
                Else
                    SQL = SQL & N & "   WHERE "
                End If
                SQL = SQL & "   TT_H_HIN_CD LIKE '" & _db.rmSQ(Trim(txtHinsyu.Text)) & "%'"
                sqlWhere = True
            End If
            '���S���R�[�h
            If Not "".Equals(txtSensin.Text) Then
                If sqlWhere Then
                    SQL = SQL & N & "   AND "
                Else
                    SQL = SQL & N & "   WHERE "
                End If
                SQL = SQL & "   TT_H_SENSIN_CD LIKE '" & _db.rmSQ(Trim(txtSensin.Text)) & "%'"
                sqlWhere = True
            End If
            '�T�C�Y�R�[�h
            If Not "".Equals(txtSize.Text) Then
                If sqlWhere Then
                    SQL = SQL & N & "   AND "
                Else
                    SQL = SQL & N & "   WHERE "
                End If
                SQL = SQL & "   TT_H_SIZE_CD LIKE '" & _db.rmSQ(Trim(txtSize.Text)) & "%'"
                sqlWhere = True
            End If
            '�F�R�[�h
            If Not "".Equals(txtColor.Text) Then
                If sqlWhere Then
                    SQL = SQL & N & "   AND "
                Else
                    SQL = SQL & N & "   WHERE "
                End If
                SQL = SQL & "   TT_H_COLOR_CD LIKE '" & _db.rmSQ(Trim(txtColor.Text)) & "%'"
            End If

            SQL = SQL & N & "   GROUP BY TT_KHINMEICD "

            '' 2010/12/17 upd start sugano
            'SQL = SQL & "   ORDER BY MAX(TT_SEIZOU_BMN), MAX(TT_SEISAKU_KBN), MAX(TT_JUYOUCD), "
            SQL = SQL & "   ORDER BY "
            '' 2010/12/17 upd end sugano
            SQL = SQL & "   MAX(TT_H_HIN_CD), MAX(TT_H_SENSIN_CD), MAX(TT_H_SIZE_CD), MAX(TT_H_SIYOU_CD), MAX(TT_H_COLOR_CD) "

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(SQL, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                lblKensuu.Text = "0��"
            Else
                '���o�f�[�^���ꗗ�ɕ\������
                dgvKousin.DataSource = ds
                dgvKousin.DataMember = RS

                lblKensuu.Text = iRecCnt & "��"
            End If

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@�f�[�^�X�V
    '�@�i�����T�v�j�ύX���ꂽ�f�[�^��DB�ɍX�V����
    '   �����̓p�����^�F�Ȃ�
    '   ���֐��߂�l�@�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub updateDB()
        Try
            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGV�n���h���̐ݒ�

            '�X�V�J�n�������擾
            Dim updStartDate As Date = Now

            '�g�����U�N�V�����J�n
            _db.beginTran()

            Dim SQL As String = ""
            Dim updCnt As Integer = 0       '�X�V��������

            '�S�����[�v
            For i As Integer = 0 To _dgv.getMaxRow - 1

                '�ύX�t���O���L���ȍs�����X�V����
                If HENKO_FLG.Equals(_dgv.getCellData(COLDT_CHANGEFLG, i)) Then
                    'M11�v��Ώەi�}�X�^
                    SQL = ""
                    SQL = SQL & N & " UPDATE M11KEIKAKUHIN SET "
                    SQL = SQL & N & "    TT_LOT = " & _db.rmSQ(_dgv.getCellData(COLDT_LOTTYOU, i))                      '�W�����b�g��
                    SQL = SQL & N & "   ,TT_TANCYO = " & _db.rmSQ(_dgv.getCellData(COLDT_TANTYOU, i))                   '�P��
                    SQL = SQL & N & "   ,TT_JYOSU = " & _db.rmSQ(_dgv.getCellData(COLDT_JOSU, i))                       '��
                    SQL = SQL & N & "   ,TT_N_SO_SUU = " & _db.rmSQ(_dgv.getCellData(COLDT_JOSU, i))                    '���ɖ{��
                    SQL = SQL & N & "   ,TT_N_K_SUU = " & _db.rmSQ(_dgv.getCellData(COLDT_KND, i))                      '�k���{��
                    SQL = SQL & N & "   ,TT_N_SH_SUU = " & _db.rmSQ(_dgv.getCellData(COLDT_HST, i))                     '�Z�d������
                    SQL = SQL & N & "   ,TT_MAKI_CD = " & CInt(_db.rmSQ(_dgv.getCellData(COLDT_MAKIWAKU, i)))           '���g�R�[�h
                    SQL = SQL & N & "   ,TT_HOSO_KBN = '" & _db.rmSQ(_dgv.getCellData(COLDT_HOUSOU, i)) & "'"           '��\���敪
                    SQL = SQL & N & "   ,TT_SIYOUSYO_NO = '" & _db.rmSQ(_dgv.getCellData(COLDT_SIYOUSYONO, i)) & "'"    '�d�l���ԍ�
                    SQL = SQL & N & "   ,TT_SYUBETU = " & _db.rmSQ(_dgv.getCellData(COLDT_ZAIKO, i))                    '�݌ɌJ��
                    SQL = SQL & N & "   ,TT_KYAKSAKI = '" & _db.rmSQ(_dgv.getCellData(COLDT_CHUMONSAKI, i)) & "'"       '������
                    SQL = SQL & N & "   ,TT_JUYOUCD = '" & _db.rmSQ(_dgv.getCellData(COLDT_JUYOUSAKI, i)) & "'"         '���v��
                    SQL = SQL & N & "   ,TT_TENKAIPTN = '" & _db.rmSQ(_dgv.getCellData(COLDT_SIZETENKAI, i)) & "'"      '�T�C�Y�W�J�p�^�[��
                    SQL = SQL & N & "   ,TT_HINSYUKBN = '" & _db.rmSQ(_dgv.getCellData(COLDT_HINSYUKBN, i)) & "'"       '�i��敪
                    SQL = SQL & N & "   ,TT_KZAIKOTUKISU = " & NS(_db.rmSQ(_dgv.getCellData(COLDT_KIJUNTUKISU, i)))     '�����
                    SQL = SQL & N & "   ,TT_SFUKKYUU = " & NS(_db.rmSQ(_dgv.getCellData(COLDT_SAIGAI, i)))              '�ЊQ�����݌�
                    SQL = SQL & N & "   ,TT_ANNZENZAIKO = " & NS(_db.rmSQ(_dgv.getCellData(COLDT_ANZENZAIKO, i)))       '���S�݌�
                    SQL = SQL & N & "   ,TT_KAMOKU_CD = " & NS(_db.rmSQ(_dgv.getCellData(COLDT_KAMOKUCD, i)))           '�ȖڃR�[�h
                    SQL = SQL & N & "   ,TT_SEIZOU_BMN = " & _db.rmSQ(_dgv.getCellData(COLDT_SEIZOUBUMON, i))           '��������
                    SQL = SQL & N & "   ,TT_TENKAI_KBN = " & _db.rmSQ(_dgv.getCellData(COLDT_TENKAIKBN, i))             '�W�J�敪
                    SQL = SQL & N & "   ,TT_KOUTEI = '" & _db.rmSQ(_dgv.getCellData(COLDT_BUBUNKOUTEI, i)) & "'"        '�����W�J�w��H��
                    SQL = SQL & N & "   ,TT_HINSITU_KBN = " & _db.rmSQ(_dgv.getCellData(COLDT_HINSITU, i))              '�i���敪
                    SQL = SQL & N & "   ,TT_TATIAI_UM = " & _db.rmSQ(_dgv.getCellData(COLDT_TATIAI, i))                 '����L��
                    SQL = SQL & N & "   ,TT_UPDNAME = '" & _tanmatuID & "'"                                             '�[��ID
                    SQL = SQL & N & "   ,TT_DATE = TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "           '�X�V����
                    '' 2011/02/10 ADD-S sugano
                    SQL = SQL & N & "   ,TT_TEHAI_SUU = " & _db.rmSQ(_dgv.getCellData(COLDT_LOTTYOU, i))                '��z���ʁi���W�����b�g���j
                    SQL = SQL & N & "   ,TT_TEHAI_KBN = " & _db.rmSQ(_dgv.getCellData(COLDT_ZAIKO, i))                  '��z�敪�i���݌ɌJ�ԁj
                    '' 2011/02/10 ADD-E sugano
                    SQL = SQL & N & " WHERE TT_KHINMEICD = '" & _dgv.getCellData(COLDT_HINMEICD, i) & "'"

                    'SQL���s
                    Dim recCnt As Integer
                    '�X�V�Ɏ��s���Ă����Ƀ��b�Z�[�W�͏o���Ȃ��B
                    '�X�V�����͐��m�Ɏ擾���āA�����e�[�u���ɓo�^����B
                    Call _db.executeDB(SQL, recCnt)

                    '�X�V�����ێ�
                    updCnt = updCnt + 1

                    '�ύX�t���O������
                    _dgv.setCellData(COLDT_CHANGEFLG, i, "0")

                End If
            Next

            '�X�V�I���������擾
            Dim updFinDate As Date = Now

            'T91���s����o�^����
            SQL = ""
            SQL = "INSERT INTO T91RIREKI ("
            SQL = SQL & N & "  PGID"                                                        '�@�\ID
            SQL = SQL & N & ", SNENGETU"                                                    '��������
            SQL = SQL & N & ", KNENGETU"                                                    '�v�����
            SQL = SQL & N & ", SDATESTART"                                                  '�����J�n����
            SQL = SQL & N & ", SDATEEND"                                                    '�����I������
            SQL = SQL & N & ", KENNSU1"                                                     '�����P�i�폜�����j
            SQL = SQL & N & ", UPDNAME"                                                     '�[��ID
            SQL = SQL & N & ", UPDDATE"                                                     '�X�V����
            SQL = SQL & N & ") VALUES ("
            SQL = SQL & N & "  '" & PGID & "'"                                              '�@�\ID
            SQL = SQL & N & ", NULL "
            SQL = SQL & N & ", NULL "
            SQL = SQL & N & ", TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "   '�����J�n����
            SQL = SQL & N & ", TO_DATE('" & updFinDate & "', 'YYYY/MM/DD HH24:MI:SS') "     '�����I������
            SQL = SQL & N & ", " & updCnt                                                   '�����P�i�X�V�����j
            SQL = SQL & N & ", '" & _tanmatuID & "'"                                        '�[��ID
            SQL = SQL & N & ", TO_DATE('" & updFinDate & "', 'YYYY/MM/DD HH24:MI:SS') "     '�X�V����
            SQL = SQL & N & " )"
            _db.executeDB(SQL)

            'T02��������e�[�u���X�V
            _parentForm.updateSeigyoTbl(PGID, True, updStartDate, updFinDate)

            '�g�����U�N�V�����I��
            _db.commitTran()

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��V�X�e���G���[MSG�o�͌�X���[
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        Finally
            If _db.isTransactionOpen = True Then
                _db.rollbackTran()                          '���[���o�b�N
            End If
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@��������R���{�{�b�N�X�쐬
    '-------------------------------------------------------------------------------
    Private Sub setCboSeizoBmn()
        Try

            Dim sql As String = ""
            sql = sql & N & " SELECT KAHENKEY KAHEN, NAME1 NAME FROM M01HANYO "
            sql = sql & N & "   WHERE KOTEIKEY = '" & M01KOTEI_SEIZOBMN & "'"

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                Exit Sub
            End If

            '�R���{�{�b�N�X�N���A
            Me.cboSeizouBmn.Items.Clear()
            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(cboSeizouBmn)

            '�擪�ɋ�s
            ch.addItem(New UtilCboVO("", ""))

            '���[�v�����ăR���{�{�b�N�X�ɃZ�b�g
            For i As Integer = 0 To iRecCnt - 1
                ch.addItem(New UtilCboVO(_db.rmNullStr(ds.Tables(RS).Rows(i)("KAHEN")), _db.rmNullStr(ds.Tables(RS).Rows(i)("NAME"))))
            Next

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@����敪�R���{�{�b�N�X�쐬
    '-------------------------------------------------------------------------------
    Private Sub setCboSeisakuKbn()
        Try

            Dim sql As String = ""
            sql = sql & N & " SELECT KAHENKEY KAHEN, NAME1 NAME FROM M01HANYO "
            sql = sql & N & "   WHERE KOTEIKEY = '" & M01KOTEI_SEISAKUKBN & "'"

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                Exit Sub
            End If

            '�R���{�{�b�N�X�N���A
            Me.cboSeisakuKbn.Items.Clear()
            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(cboSeisakuKbn)

            '�擪�ɋ�s
            ch.addItem(New UtilCboVO("", ""))

            '���[�v�����ăR���{�{�b�N�X�ɃZ�b�g
            For i As Integer = 0 To iRecCnt - 1
                ch.addItem(New UtilCboVO(_db.rmNullStr(ds.Tables(RS).Rows(i)("KAHEN")), _db.rmNullStr(ds.Tables(RS).Rows(i)("NAME"))))
            Next

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@���v��R���{�{�b�N�X�쐬
    '-------------------------------------------------------------------------------
    Private Sub setCboJuyosaki()
        Try

            Dim sql As String = ""
            sql = sql & N & " SELECT KAHENKEY KAHEN, NAME2 NAME FROM M01HANYO "
            sql = sql & N & "   WHERE KOTEIKEY = '" & M01KOTEI_JUYOUSAKI & "'"

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                Exit Sub
            End If

            '�R���{�{�b�N�X�N���A
            Me.cboJuyosaki.Items.Clear()
            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(cboJuyosaki)

            '�擪�ɋ�s
            ch.addItem(New UtilCboVO("", ""))

            '���[�v�����ăR���{�{�b�N�X�ɃZ�b�g
            For i As Integer = 0 To iRecCnt - 1
                ch.addItem(New UtilCboVO(_db.rmNullStr(ds.Tables(RS).Rows(i)("KAHEN")), _db.rmNullStr(ds.Tables(RS).Rows(i)("NAME"))))
            Next

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@���g�R�[�h���݃`�F�b�N
    '�@�i�����T�v�j�n���ꂽ���g�R�[�h�����ƂɊ��g�R�[�h�e�[�u������������
    '   �����̓p�����^�FprmMakiwakuCD   ���g�R�[�h���͒l
    '   ���֐��߂�l�@�FTRUE(����)/FALSE(���݂���)
    '-------------------------------------------------------------------------------
    Private Function serchMakiwaku(ByVal prmMakiwakuCD As String)
        Try
            Dim sql = ""
            sql = sql & N & " SELECT "
            sql = sql & N & " ZE_NAME "
            sql = sql & N & " FROM ZEASYCODE_TB "
            sql = sql & N & "   WHERE ZE_CODE = '" & prmMakiwakuCD & "'"

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then            '���o���R�[�h���P�����Ȃ��ꍇ
                Return False
            End If

            Return True

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Function

    '-------------------------------------------------------------------------------
    '�@��\���敪���݃`�F�b�N
    '�@�i�����T�v�j�n���ꂽ��\���敪�����Ƃɕ�e�[�u������������
    '   �����̓p�����^�FprmHousouCD   ��\���敪���͒l
    '   ���֐��߂�l�@�FTRUE(����)/FALSE(���݂���)
    '-------------------------------------------------------------------------------
    Private Function serchHousou(ByVal prmHousou As String)
        Try
            Dim sql = ""
            sql = sql & N & " SELECT "
            sql = sql & N & " HN_NAME "
            sql = sql & N & " FROM HOSONAME_TB "
            sql = sql & N & "   WHERE HN_KUBUN = '" & prmHousou & "'"

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then            '���o���R�[�h���P�����Ȃ��ꍇ
                Return False
            End If

            Return True

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Function

    '-------------------------------------------------------------------------------
    '�@�ėp�}�X�^����2���o
    '�@�i�����T�v�j�n���ꂽ�Œ�L�[����щσL�[�����Ƃɖ���2���������ĕԂ�
    '   �����̓p�����^�FprmKoteiKey     �Œ�L�[
    '                 �FprmKahenKey     �σL�[
    '   ���֐��߂�l�@�F                ���o��������
    '-------------------------------------------------------------------------------
    Private Function backNameFromM01(ByVal prmKoteiKey As String, ByVal prmKahenKey As String)
        Try

            Dim dtCol1 As String = "NAME1"    '����1�G�C���A�X
            Dim dtCol2 As String = "NAME2"    '����2�G�C���A�X

            Dim sql As String = ""
            sql = sql & N & " SELECT NAME1 " & dtCol1 & " , NAME2 " & dtCol2
            sql = sql & N & " FROM M01HANYO "
            sql = sql & N & "   WHERE KOTEIKEY = '" & prmKoteiKey & "'"
            sql = sql & N & "   AND KAHENKEY = '" & prmKahenKey & "'"

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then            '���o���R�[�h���P�����Ȃ��ꍇ
                Return ""
            Else
                '�i�������敪�y�ї���L���͉�ʕ\�����Ȃ��̂ŁA�Ƃ肠��������1��Ԃ�
                If prmKoteiKey.Equals(M01KOTEI_HINSITU) Or prmKoteiKey.Equals(M01KOTEI_TATIAI) Then
                    Return _db.rmNullStr(ds.Tables(RS).Rows(0)(dtCol1))
                End If
                '���̑��͖���2��Ԃ�
                Return _db.rmNullStr(ds.Tables(RS).Rows(0)(dtCol2))
            End If

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Function

    '-------------------------------------------------------------------------------
    '�@�i��敪
    '�@�i�����T�v�j���͂��ꂽ���v��ƕi��敪�̑g�ݍ��킹�����݂��邩�`�F�b�N����
    '   �����̓p�����^�FprmHinsyuKbnCD      �i��敪�R�[�h
    '                 �FprmJuyosakiCD       ���v��R�[�h
    '   ���֐��߂�l�@�FTRUE(����)/FALSE(���݂���)
    '-------------------------------------------------------------------------------
    Private Function hinsyuKbnExist(ByVal prmHinsyuKbnCD As String, ByVal prmJuyosakiCD As String)
        Try

            Dim sql = ""
            sql = sql & N & " SELECT "
            sql = sql & N & " HINSYUKBNNM "
            sql = sql & N & " FROM M02HINSYUKBN "
            sql = sql & N & "   WHERE JUYOUCD = '" & prmJuyosakiCD & "'"
            sql = sql & N & "   AND HINSYUKBN = '" & prmHinsyuKbnCD & "'"

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                Return False
            End If
            Return True

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Function

    '-------------------------------------------------------------------------------
    '�@�W�v�i�����ĕ\��
    '�@�i�����T�v�j�W�v�Ώەi���o�^��ʂŕҏW��̃��R�[�h�����ꗗ�ɔ��f������
    '   �����̓p�����^�FprmRowIndex         �ꗗ�̍s�ԍ�
    '   ���֐��߂�l�@�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub reDispSyukeiHinmei(ByVal prmRowIndex As Integer)
        Try
            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGV�n���h���̐ݒ�

            Dim sql = ""
            sql = sql & N & " SELECT "
            sql = sql & N & " COUNT(M12.HINMEICD) - 1 CNT "
            sql = sql & N & " FROM M12SYUYAKU M12 "
            sql = sql & N & " WHERE M12.KHINMEICD = '" & _dgv.getCellData(COLDT_HINMEICD, prmRowIndex) & "'"

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                _dgv.setCellData(COLDT_SHINMEI, prmRowIndex, "0")
            Else
                _dgv.setCellData(COLDT_SHINMEI, prmRowIndex, CStr(_db.rmNullInt(ds.Tables(RS).Rows(0)("CNT"))))
            End If

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@NULL����
    '�@(�����T�v)�Z���̓��e��NULL�Ȃ�"NULL"��Ԃ�
    '�@�@I�@�F�@prmStr      DB�ɓo�^����DOUBLE�^�̒l
    '�@�@R�@�F�@NS          prmStr����Ȃ�uNULL�v�A����ȊO�Ȃ�uprmStr�v
    '------------------------------------------------------------------------------------------------------
    Private Function NS(ByVal prmStr As String) As String
        Dim ret As String = ""
        If "".Equals(prmStr) Then
            ret = "NULL"
        Else
            ret = prmStr
        End If
        Return ret
    End Function

#End Region

#Region "���[�U��`�֐�:�`�F�b�N����"

    '-------------------------------------------------------------------------------
    '�@���͒l�`�F�b�N
    '�@�i�����T�v�j���͓��e�̑Ó������`�F�b�N����
    '-------------------------------------------------------------------------------
    Private Sub chkInputValue()
        Try

            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGV�n���h���̐ݒ�

            '�S�s���[�v
            For rowCnt As Integer = 0 To dgvKousin.RowCount - 1
                '�ύX�t���O���L���ȍs�̂݃`�F�b�N���s���B
                If HENKO_FLG.Equals(_dgv.getCellData(COLDT_CHANGEFLG, rowCnt)) Then

                    '���g�R�[�h�̃`�F�b�N
                    If checkMakiwaku(rowCnt) = False Then
                        Throw New UsrDefException("���g�����}�X�^�ɓo�^����Ă��܂���B", _
                                _msgHd.getMSG("noExistMakiwaku", "�y���g�R�[�h�z"), dgvKousin, COLCN_MAKIWAKU, rowCnt)
                    End If

                    '��\���敪�̃`�F�b�N
                    If checkHousou(rowCnt) = False Then
                        Throw New UsrDefException("��^�\����ނ��}�X�^�ɓo�^����Ă��܂���B", _
                                _msgHd.getMSG("noExistHousou", "�y��\���敪�z"), dgvKousin, COLCN_HOUSOU, rowCnt)
                    End If

                    '�݌ɌJ�ԕύX��
                    If checkZaiko(rowCnt) = False Then
                        Throw New UsrDefException("�ėp�}�X�^�ɓo�^����Ă��Ȃ��l�ł��B", _
                                    _msgHd.getMSG("noExistHanyouMst", "�y�݌ɌJ�ԁz"), dgvKousin, COLCN_ZAIKO, rowCnt)
                    End If

                    '�݌ɌJ�� = �u1�v�̂Ƃ��݈̂ȉ��̃`�F�b�N���s���B
                    If _db.rmNullInt(dgvKousin(COLNO_ZAIKO, rowCnt).Value) = M01KAHEN_ZAIKO_ZAIKO Then

                        '���v����͎�
                        If checkJuyo(rowCnt) = False Then
                            Throw New UsrDefException("�ėp�}�X�^�ɓo�^����Ă��Ȃ��l�ł��B", _
                                    _msgHd.getMSG("noExistHanyouMst", "�y���v��z"), dgvKousin, COLCN_JUYOUSAKI, rowCnt)
                        End If

                        '�T�C�Y�W�J���͎�
                        If checkSizeTenkai(rowCnt) = False Then
                            Throw New UsrDefException("�ėp�}�X�^�ɓo�^����Ă��Ȃ��l�ł��B", _
                                    _msgHd.getMSG("noExistHanyouMst", "�y�T�C�Y�W�J�z"), dgvKousin, COLCN_SIZETENKAI, rowCnt)
                        End If

                        '�i��敪
                        If checkHinsyuKbn(rowCnt) = False Then
                            Throw New UsrDefException("�i��敪���}�X�^�ɓo�^����Ă��܂���B", _
                                            _msgHd.getMSG("noHinsyuKbn", "�y�i��敪�z"), dgvKousin, COLCN_HINSYUKBN, rowCnt)
                        End If
                    End If

                    '����������͎�
                    If checkSeizouBmn(rowCnt) = False Then
                        Throw New UsrDefException("�ėp�}�X�^�ɓo�^����Ă��Ȃ��l�ł��B", _
                                    _msgHd.getMSG("noExistHanyouMst", "�y��������z"), dgvKousin, COLCN_SEIZOUBUMON, rowCnt)
                    End If

                    '�W�J�敪���͎�
                    If checkTenkaiKbn(rowCnt) = 1 Then
                        Throw New UsrDefException("�ėp�}�X�^�ɓo�^����Ă��Ȃ��l�ł��B", _
                                    _msgHd.getMSG("noExistHanyouMst", "�y�W�J�敪�z"), dgvKousin, COLCN_TENKAIKBN, rowCnt)
                    ElseIf checkTenkaiKbn(rowCnt) = 2 Then
                        Throw New UsrDefException("����敪�u�O���v���͓W�J�敪�u�����W�J�v�ȊO�I���ł��܂���B", _
                                    _msgHd.getMSG("nonGaicyuSelect"), dgvKousin, COLCN_TENKAIKBN, rowCnt)
                    End If

                    '�i�������敪���͎�
                    If checkHinsitu(rowCnt) = False Then
                        Throw New UsrDefException("�ėp�}�X�^�ɓo�^����Ă��Ȃ��l�ł��B", _
                                    _msgHd.getMSG("noExistHanyouMst", "�y�i����������z"), dgvKousin, COLCN_HINSITU, rowCnt)
                    End If

                    '����L�����͎�
                    If checkTatiai(rowCnt) = False Then
                        Throw New UsrDefException("�ėp�}�X�^�ɓo�^����Ă��Ȃ��l�ł��B", _
                                    _msgHd.getMSG("noExistHanyouMst", "�y����L���z"), dgvKousin, COLCN_TATIAI, rowCnt)
                    End If

                End If
            Next

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@���̓`�F�b�N�����i�X�V�{�^���������j
    '   �i�����T�v�j�e���͍��ڂ̃`�F�b�N���s���B
    '-------------------------------------------------------------------------------
    Private Sub chkBeforeUpdate()
        Try
            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGV�n���h���̐ݒ�

            '�S�s���[�v
            For rowCnt As Integer = 0 To dgvKousin.RowCount - 1

                '�ύX�t���O���L���ȍs�̂݃`�F�b�N���s���B
                If HENKO_FLG.Equals(_dgv.getCellData(COLDT_CHANGEFLG, rowCnt)) Then

                    '�W�����b�g���K�{�`�F�b�N
                    If "".Equals(_db.rmNullStr(dgvKousin(COLNO_LOTTYOU, rowCnt).Value)) Then
                        Throw New UsrDefException("�K�{���͍��ڂł��B", _
                                    _msgHd.getMSG("requiredImput", "�y�W�����b�g���z"), dgvKousin, COLCN_LOTTYOU, rowCnt)
                    End If

                    '�P���K�{�`�F�b�N
                    If "".Equals(_db.rmNullStr(dgvKousin(COLNO_TANTYOU, rowCnt).Value)) Then
                        Throw New UsrDefException("�K�{���͍��ڂł��B", _
                                        _msgHd.getMSG("requiredImput", "�y�P���z"), dgvKousin, COLCN_TANTYOU, rowCnt)
                    End If

                    '�𐔐��l�`�F�b�N
                    If _db.rmNullInt(dgvKousin(COLNO_LOTTYOU, rowCnt).Value) Mod _db.rmNullInt(dgvKousin(COLNO_TANTYOU, rowCnt).Value) <> 0 Then
                        Throw New UsrDefException("�W�����b�g���܂��͐���P�����Ⴂ�܂��B", _
                                        _msgHd.getMSG("failLotOrTantyou"), dgvKousin, COLCN_LOTTYOU, rowCnt)
                    End If

                    '�P���~�𐔂��u9999999�v�𒴂���ꍇ
                    If dgvKousin(COLNO_TANTYOU, rowCnt).Value * dgvKousin(COLNO_JOSU, rowCnt).Value > 9999999 Then
                        Throw New UsrDefException("�P���~�𐔂̒l��7���𒴂��Ă��܂��B", _
                                                    _msgHd.getMSG("over7Keta"), dgvKousin, COLCN_TANTYOU, rowCnt)
                    End If

                    '�k���{���K�{�`�F�b�N
                    If "".Equals(_db.rmNullStr(dgvKousin(COLNO_KND, rowCnt).Value)) Then
                        Throw New UsrDefException("�K�{���͍��ڂł��B", _
                                            _msgHd.getMSG("requiredImput", "�y����KND�{���z"), dgvKousin, COLCN_KND, rowCnt)
                    End If

                    '�k���{�����l�`�F�b�N
                    If _db.rmNullInt(dgvKousin(COLNO_KND, rowCnt).Value) > _db.rmNullInt(dgvKousin(COLNO_JOSU, rowCnt).Value) Then
                        Throw New UsrDefException("�͈͊O�̒l�����͂���܂����B", _
                                        _msgHd.getMSG("errOutOfRange", "�y����KND�{���z"), dgvKousin, COLCN_KND, rowCnt)
                    End If

                    '�Z�d�������K�{�`�F�b�N
                    If "".Equals(_db.rmNullStr(dgvKousin(COLNO_HST, rowCnt).Value)) Then
                        Throw New UsrDefException("�K�{���͍��ڂł��B", _
                                        _msgHd.getMSG("requiredImput", "�y����HST�{���z"), dgvKousin, COLCN_HST, rowCnt)
                    End If

                    '�Z�d���������l�`�F�b�N
                    If _db.rmNullInt(dgvKousin(COLNO_HST, rowCnt).Value) > _db.rmNullInt(dgvKousin(COLNO_JOSU, rowCnt).Value) Then
                        Throw New UsrDefException("�͈͊O�̒l�����͂���܂����B", _
                                            _msgHd.getMSG("errOutOfRange", "�y����HST�{���z"), dgvKousin, COLCN_HST, rowCnt)
                    End If

                    '���g�R�[�h�K�{�`�F�b�N
                    If "".Equals(_db.rmNullStr(dgvKousin(COLNO_MAKIWAKU, rowCnt).Value)) Then
                        Throw New UsrDefException("�K�{���͍��ڂł��B", _
                                    _msgHd.getMSG("requiredImput", "�y���g�R�[�h�z"), dgvKousin, COLCN_MAKIWAKU, rowCnt)
                    End If

                    '��\���敪�R�[�h�K�{�`�F�b�N
                    If "".Equals(_db.rmNullStr(dgvKousin(COLNO_HOUSOU, rowCnt).Value)) Then
                        Throw New UsrDefException("�K�{���͍��ڂł��B", _
                                    _msgHd.getMSG("requiredImput", "�y��\���敪�z"), dgvKousin, COLCN_HOUSOU, rowCnt)
                    End If

                    '�d�l���ԍ��K�{�`�F�b�N
                    If "".Equals(_db.rmNullStr(dgvKousin(COLNO_SIYOUSYONO, rowCnt).Value)) Then
                        Throw New UsrDefException("�K�{���͍��ڂł��B", _
                                _msgHd.getMSG("requiredImput", "�y�d�l���ԍ��z"), dgvKousin, COLCN_SIYOUSYONO, rowCnt)
                    End If

                    '�݌ɌJ�ԃR�[�h�K�{�`�F�b�N
                    If "".Equals(_db.rmNullStr(dgvKousin(COLNO_ZAIKO, rowCnt).Value)) Then
                        Throw New UsrDefException("�K�{���͍��ڂł��B", _
                                    _msgHd.getMSG("requiredImput", "�y�݌ɌJ�ԁz"), dgvKousin, COLCN_ZAIKO, rowCnt)
                    End If

                    '�݌ɌJ�� =�u2�v�̂Ƃ��̂ݒ�����͕K�{����
                    If dgvKousin(COLNO_ZAIKO, rowCnt).Value = M01KAHEN_ZAIKO_JUTYU Then
                        '������R�[�h���̓`�F�b�N
                        If "".Equals(_db.rmNullStr(dgvKousin(COLNO_CHUMONSAKI, rowCnt).Value)) Then
                            Throw New UsrDefException("�K�{���͍��ڂł��B", _
                                    _msgHd.getMSG("requiredImput", "�y������z"), dgvKousin, COLCN_CHUMONSAKI, rowCnt)
                        End If
                    End If

                    '�݌ɌJ�� = �u1�v�̂Ƃ��݈̂ȉ��̃`�F�b�N���s���B
                    If _db.rmNullInt(dgvKousin(COLNO_ZAIKO, rowCnt).Value) = M01KAHEN_ZAIKO_ZAIKO Then

                        '���v��R�[�h�K�{�`�F�b�N
                        If "".Equals(_db.rmNullStr(dgvKousin(COLNO_JUYOUSAKI, rowCnt).Value)) Then
                            Throw New UsrDefException("�K�{���͍��ڂł��B", _
                                    _msgHd.getMSG("requiredImput", "�y���v��z"), dgvKousin, COLCN_JUYOUSAKI, rowCnt)
                        End If

                        '�T�C�Y�W�J�K�{�`�F�b�N
                        If "".Equals(_db.rmNullStr(dgvKousin(COLNO_SIZETENKAI, rowCnt).Value)) Then
                            Throw New UsrDefException("�K�{���͍��ڂł��B", _
                                    _msgHd.getMSG("requiredImput", "�y�T�C�Y�W�J�z"), dgvKousin, COLCN_SIZETENKAI, rowCnt)
                        End If

                        '�i��敪�K�{�`�F�b�N
                        If "".Equals(_db.rmNullStr(dgvKousin(COLNO_HINSYUKBN, rowCnt).Value)) Then
                            Throw New UsrDefException("�K�{���͍��ڂł��B", _
                                     _msgHd.getMSG("requiredImput", "�y�i��敪�z"), dgvKousin, COLCN_HINSYUKBN, rowCnt)
                        End If

                        '������K�{�`�F�b�N
                        If "".Equals(_db.rmNullStr(dgvKousin(COLNO_KIJUNTUKISUU, rowCnt).Value)) Then
                            Throw New UsrDefException("�K�{���͍��ڂł��B", _
                                    _msgHd.getMSG("requiredImput", "�y������z"), dgvKousin, COLCN_KIJUNTUKISU, rowCnt)
                        End If
                    End If

                    '����敪 = �u2�v�̏ꍇ�̂݃`�F�b�N
                    If M01NAME2_SEISAKUKBN.Equals(_db.rmNullStr(dgvKousin(COLNO_SEISAKUKBN, rowCnt).Value)) Then
                        '�ȖڃR�[�h�K�{�`�F�b�N
                        If "".Equals(_db.rmNullStr(dgvKousin(COLNO_KAMOKUCD, rowCnt).Value)) Then
                            Throw New UsrDefException("�K�{���͍��ڂł��B", _
                                    _msgHd.getMSG("requiredImput", "�y�ȖڃR�[�h�z"), dgvKousin, COLCN_KAMOKUCD, rowCnt)
                        End If

                        '�ȖڃR�[�h�����`�F�b�N
                        '' 2011/01/13 upd start sugano
                        'If _db.rmNullStr(_dgv.getCellData(COLDT_KAMOKUCD, rowCnt)).Length <> 6 Then
                        '    Throw New UsrDefException("�ȖڃR�[�h�͂U���œ��͂��ĉ������B", _
                        '                _msgHd.getMSG("notKamokuCD6Keta", "�y�ȖڃR�[�h�z"), dgvKousin, COLCN_KAMOKUCD, rowCnt)
                        'End If
                        If _db.rmNullStr(_dgv.getCellData(COLDT_KAMOKUCD, rowCnt)).Length <> 5 Then
                            Throw New UsrDefException("�ȖڃR�[�h�͂T���œ��͂��ĉ������B", _
                                        _msgHd.getMSG("notKamokuCDKeta", "�y�ȖڃR�[�h�z"), dgvKousin, COLCN_KAMOKUCD, rowCnt)
                        End If
                        '' 2011/01/13 upd end sugano
                    End If

                    '��������K�{�`�F�b�N
                    If "".Equals(_db.rmNullStr(dgvKousin(COLNO_SEIZOUBUMON, rowCnt).Value)) Then
                        Throw New UsrDefException("�K�{���͍��ڂł��B", _
                            _msgHd.getMSG("requiredImput", "�y��������z"), dgvKousin, COLCN_SEIZOUBUMON, rowCnt)
                    End If

                    '�W�J�敪�K�{�`�F�b�N
                    If "".Equals(_db.rmNullStr(dgvKousin(COLNO_TENKAIKBN, rowCnt).Value)) Then
                        Throw New UsrDefException("�K�{���͍��ڂł��B", _
                                _msgHd.getMSG("requiredImput", "�y�W�J�敪�z"), dgvKousin, COLCN_TENKAIKBN, rowCnt)
                    End If

                    '�W�J�敪 = �u2�v�Ȃ�`�F�b�N
                    If dgvKousin(COLNO_TENKAIKBN, rowCnt).Value = M01KAHEN_TENKAI_BUBUN Then
                        '�����W�J�H���K�{�`�F�b�N
                        If "".Equals(_db.rmNullStr(dgvKousin(COLNO_BUBUNKOUTEI, rowCnt).Value)) Then
                            Throw New UsrDefException("�K�{���͍��ڂł��B", _
                                    _msgHd.getMSG("requiredImput", "�y�����W�J�H���z"), dgvKousin, COLCN_BUBUNKOUTEI, rowCnt)
                        End If

                        '�����W�J�H�����͓��e�`�F�b�N
                        If Not CStr(dgvKousin(COLNO_BUBUNKOUTEI, rowCnt).Value).Substring(0, 1) = "1" And _
                                Not CStr(dgvKousin(COLNO_BUBUNKOUTEI, rowCnt).Value).Substring(0, 1) = "3" Then

                            Throw New UsrDefException("1�܂���3����n�܂�H������͂��Ă��������B", _
                                    _msgHd.getMSG("notBKouteiStart1Or3", "�y�����W�J�H���z"), dgvKousin, COLCN_BUBUNKOUTEI, rowCnt)
                        End If
                    End If

                    '�i�������K�{�`�F�b�N
                    If "".Equals(_db.rmNullStr(dgvKousin(COLNO_HINSITU, rowCnt).Value)) Then
                        Throw New UsrDefException("�K�{���͍��ڂł��B", _
                                _msgHd.getMSG("requiredImput", "�y�i�������z"), dgvKousin, COLCN_HINSITU, rowCnt)
                    End If

                    '����L���`�F�b�N
                    If "".Equals(_db.rmNullStr(dgvKousin(COLNO_TATIAI, rowCnt).Value)) Then
                        Throw New UsrDefException("�K�{���͍��ڂł��B", _
                                _msgHd.getMSG("requiredImput", "�y����L���z"), dgvKousin, COLCN_TATIAI, rowCnt)
                    End If

                End If
            Next

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

#End Region

End Class