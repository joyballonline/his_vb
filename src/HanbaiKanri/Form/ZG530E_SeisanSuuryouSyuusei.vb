'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���j���Y�v�搔�ʏC��
'    �i�t�H�[��ID�jZG530E_SeisanSuuryouSyuusei
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ���V        2010/09/01                 �V�K              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.Combo
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls

Public Class ZG530E_SeisanSuuryouSyuusei
    Inherits System.Windows.Forms.Form

#Region "���e�����l��`"
    '-------------------------------------------------------------------------------
    '   �萔��`
    '-------------------------------------------------------------------------------

    'PG���䕶�� 
    Private Const N As String = ControlChars.NewLine            '���s����
    Private Const RS As String = "RecSet"                       '���R�[�h�Z�b�g�e�[�u��

    Private Const PGID As String = "ZG530E"                     'T91�ɓo�^����PGID

    '�ꗗ�f�[�^�o�C���h��
    Private Const COLDT_JUYOUCD As String = "dtJuyouCD"             '���v��
    Private Const COLDT_JUYOUSAKI As String = "dtJuyousaki"         '���v�於
    Private Const COLDT_HINMEICD As String = "dtHinmeiCD"           '�i���R�[�h
    Private Const COLDT_HINMEI As String = "dtHinmei"               '�i��
    Private Const COLDT_HINSYUCD As String = "dtHinsyuCD"           '�i��R�[�h
    Private Const COLDT_HINSYUNM As String = "dtHinsyuNM"           '�i�햼
    Private Const COLDT_LOTTYO As String = "dtLottyou"              '���b�g��
    Private Const COLDT_ABC As String = "dtABC"                     'ABC
    Private Const COLDT_ZZZAIKOS As String = "dtZZZaikosu"          '�O�X�����݌ɐ�
    Private Const COLDT_ZZZAIKOR As String = "dtZZZaikoryou"        '�O�X�����݌ɗ�
    Private Const COLDT_ZSEISANS As String = "dtZSeisansu"          '�O�����Y���ѐ�
    Private Const COLDT_ZSEISANR As String = "dtZSeisanryou"        '�O�����Y���ї�
    Private Const COLDT_ZHANBAIS As String = "dtZHanbaisu"          '�O���̔����ѐ�
    Private Const COLDT_ZHANBAIR As String = "dtZHanbairyou"        '�O���̔����ї�
    Private Const COLDT_ZZAIKOS As String = "dtZZaikoS"             '�O�����݌ɐ�
    Private Const COLDT_ZZAIKOR As String = "dtZZaikoR"             '�O�����݌ɗ�
    Private Const COLDT_TSEISANS As String = "dtTSeisanS"           '�������Y�v�搔
    Private Const COLDT_TSEISANR As String = "dtTSeisanR"           '�������Y�v���
    Private Const COLDT_THANBAIS As String = "dtTHanbaiS"           '�����̔��v�搔
    Private Const COLDT_THANBAIR As String = "dtTHanbaiR"           '�����̔��v���
    Private Const COLDT_TZAIKOS As String = "dtTZaikoS"             '�������݌ɐ�
    Private Const COLDT_TZAIKOR As String = "dtTZaikoR"             '�������݌ɗ�
    Private Const COLDT_KURIKOSIS As String = "dtKurikosiS"         '�J�z��
    Private Const COLDT_KURIKOSIR As String = "dtKurikosiR"         '�J�z��
    Private Const COLDT_LOTSU As String = "dtLotsuu"                '���b�g��
    Private Const COLDT_YSEISANS As String = "dtYSeisanS"           '�������Y�v�搔
    Private Const COLDT_YSEISANR As String = "dtYSeisanR"           '�������Y�v���
    Private Const COLDT_YHANBAIS As String = "dtYHanbaiS"           '�����̔��v�搔
    Private Const COLDT_YHANBAIR As String = "dtYHanbaiR"           '�����̔��v���
    Private Const COLDT_YZAIKOS As String = "dtYZaikoS"             '�������݌ɐ�
    Private Const COLDT_YZAIKOR As String = "dtYZaikoR"             '�������݌ɗ�
    Private Const COLDT_ZAIKOTUKISU As String = "dtZaikoTukisu"     '�݌Ɍ���
    Private Const COLDT_YYHANBAIS As String = "dtYYHanbaiS"         '���X���̔��v�搔
    Private Const COLDT_YYHANBAIR As String = "dtYYHanbaiR"         '���X���̔��v���
    Private Const COLDT_KTUKISU As String = "dtKTukisu"             '�����
    Private Const COLDT_FZAIKOS As String = "dtFukkyuS"             '�����p�݌ɐ�
    Private Const COLDT_FZAIKOR As String = "dtFukkyuR"             '�����p�݌ɗ�
    Private Const COLDT_AZAIKOS As String = "dtAZaikoS"             '���S�݌ɐ�
    Private Const COLDT_AZAIKOR As String = "dtAZaikoR"             '���S�݌ɗ�
    Private Const COLDT_METUKE As String = "dtMetuke"               '�ڕt
    Private Const COLDT_SIYOCD As String = "dtSiyoCD"               '�d�l�R�[�h

    '�ꗗ�O���b�h��
    Private Const COLCN_JUYOUCD As String = "cnJuyouCD"             '���v��
    Private Const COLCN_JUYOUSAKI As String = "cnJuyousaki"         '���v�於
    Private Const COLCN_HINMEICD As String = "cnHinmeiCD"           '�i���R�[�h
    Private Const COLCN_HINMEI As String = "cnHinmei"               '�i��
    Private Const COLCN_HINSYUNM As String = "cnHinsyuNM"           '�i�햼
    Private Const COLCN_LOTTYO As String = "cnLottyou"              '���b�g��
    Private Const COLCN_ABC As String = "cnABC"                     'ABC
    Private Const COLCN_ZZAIKOS As String = "cnZZaikoS"             '�O�����݌ɐ�
    Private Const COLCN_ZZAIKOR As String = "cnZZaikoR"             '�O�����݌ɗ�
    Private Const COLCN_TSEISANS As String = "cnTSeisanS"           '�������Y�v�搔
    Private Const COLCN_TSEISANR As String = "cnTSeisanR"           '�������Y�v���
    Private Const COLCN_THANBAIS As String = "cnTHanbaiS"           '�����̔��v�搔
    Private Const COLCN_THANBAIR As String = "cnTHanbaiR"           '�����̔��v���
    Private Const COLCN_TZAIKOS As String = "cnTZaikoS"             '�������݌ɐ�
    Private Const COLCN_TZAIKOR As String = "cnTZaikoR"             '�������݌ɗ�
    Private Const COLCN_KURIKOSIS As String = "cnKurikosiS"         '�J�z��
    Private Const COLCN_KURIKOSIR As String = "cnKurikosiR"         '�J�z��
    Private Const COLCN_LOTSUU As String = "cnLotsuu"               '���b�g��
    Private Const COLCN_YSEISANS As String = "cnYSeisanS"           '�������Y�v�搔
    Private Const COLCN_YSEISANR As String = "cnYSeisanR"           '�������Y�v���
    Private Const COLCN_YHANBAIS As String = "cnYHanbaiS"           '�����̔��v�搔
    Private Const COLCN_YHANBAIR As String = "cnYHanbaiR"           '�����̔��v���
    Private Const COLCN_YZAIKOS As String = "cnYZaikoS"             '�������݌ɐ�
    Private Const COLCN_YZAIKOR As String = "cnYZaikoR"             '�������݌ɗ�
    Private Const COLCN_ZAIKOTUKISU As String = "cnZaikoTukisu"     '�݌Ɍ���
    Private Const COLCN_YYHANBAIS As String = "cnYYHanbaiS"         '���X���̔��v�搔
    Private Const COLCN_YYHANBAIR As String = "cnYYHanbaiR"         '���X���̔��v���
    Private Const COLCN_KTUKISU As String = "cnKTukisu"             '�����
    Private Const COLCN_FUKKYUS As String = "cnFukkyuS"             '�����p�݌ɐ�
    Private Const COLCN_FUKKYUR As String = "cnFukkyuR"             '�����p�݌ɗ�
    Private Const COLCN_AZAIKOS As String = "cnAZaikoS"             '���S�݌ɐ�
    Private Const COLCN_AZAIKOR As String = "cnAZaikoR"             '���S�݌ɗ�
    Private Const COLCN_METUKE As String = "cnMetuke"               '�ڕt

    '�ꗗ��ԍ�
    Private Const COLNO_JUYOUCD As Integer = 0          '���v��
    Private Const COLNO_JUYOUSAKI As Integer = 1        '���v�於
    Private Const COLNO_HINMEICD As Integer = 2         '�i���R�[�h
    Private Const COLNO_HINMEI As Integer = 3           '�i��
    Private Const COLNO_LOTTYO As Integer = 4           '���b�g��
    Private Const COLNO_ABC As Integer = 5              'ABC
    Private Const COLNO_ZZAIKOS As Integer = 6          '�O�����݌ɐ�
    Private Const COLNO_ZZAIKOR As Integer = 7          '�O�����݌ɗ�
    Private Const COLNO_TSEISANS As Integer = 8         '�������Y�v�搔
    Private Const COLNO_TSEISANR As Integer = 9         '�������Y�v���
    Private Const COLNO_THANBAIS As Integer = 10        '�����̔��v�搔
    Private Const COLNO_THANBAIR As Integer = 11        '�����̔��v���
    Private Const COLNO_TZAIKOS As Integer = 12         '�������݌ɐ�
    Private Const COLNO_TZAIKOR As Integer = 13         '�������݌ɗ�
    Private Const COLNO_KURIKOSIS As Integer = 14       '�J�z��
    Private Const COLNO_KURIKOSIR As Integer = 15       '�J�z��
    Private Const COLNO_LOTSUU As Integer = 16          '���b�g��
    Private Const COLNO_YSEISANS As Integer = 17        '�������Y�v�搔
    Private Const COLNO_YSEISANR As Integer = 18        '�������Y�v���
    Private Const COLNO_YHANBAIS As Integer = 19        '�����̔��v�搔
    Private Const COLNO_YHANBAIR As Integer = 20        '�����̔��v���
    Private Const COLNO_YZAIKOS As Integer = 21         '�������݌ɐ�
    Private Const COLNO_YZAIKOR As Integer = 22         '�������݌ɗ�
    Private Const COLNO_ZAIKOTUKISU As Integer = 23     '�݌Ɍ���
    Private Const COLNO_YYHANBAIS As Integer = 24       '���X���̔��v�搔
    Private Const COLNO_YYHANBAIR As Integer = 25       '���X���̔��v���
    Private Const COLNO_KTUKISU As Integer = 26         '�����
    Private Const COLNO_FUKKYUS As Integer = 27         '�����p�݌ɐ�
    Private Const COLNO_FUKKYUR As Integer = 28         '�����p�݌ɗ�
    Private Const COLNO_AZAIKOS As Integer = 29         '���S�݌ɐ�
    Private Const COLNO_AZAIKOR As Integer = 30         '���S�݌ɗ�
    Private Const COLNO_METUKE As Integer = 31          '�ڕt
    Private Const COLNO_HINSYUNM As Integer = 32        '�i�햼

    '���x�����������בւ��p���e����
    Private Const LBL_JUYO As String = "���v��"
    Private Const LBL_HINSYU As String = "�i��敪"
    Private Const LBL_HINMEICD As String = "�i������"
    Private Const LBL_HINMEI As String = "�i��"
    Private Const LBL_SYOJUN As String = "��"
    Private Const LBL_KOJUN As String = "��"

    '�\���P�ʐ؂�ւ������x���ύX�p���e����
    Private Const LBL_ZAIKO As String = "�݌�"
    Private Const LBL_SEISAN As String = "���Y"
    Private Const LBL_HANBAI As String = "�̔�"
    Private Const LBL_KURIKOSIS As String = "�J�z��"
    Private Const LBL_KURIKOSIR As String = "�J�z��"
    Private Const LBL_FUKKYU As String = "�����p"
    Private Const LBL_ANNZEN As String = "���S�݌�"
    Private Const LBL_KM As String = "Km"
    Private Const LBL_TON As String = "t"

    '�ꗗ�w�b�_�p���e����
    Private Const MONTH As String = "��"            '�ꗗ�w�b�_�ҏW�p

    '�ėp�}�X�^
    Private Const KOTEI_JUYOU As String = "01"              '���v��Œ�L�[
    Private Const HANYOU_NAME1 As String = "NAME1"          '���̂P
    Private Const HANYOU_KAHENKEY As String = "KAHENKEY"    '�σL�[

    'EXCEL
    Private Const START_PRINT As Integer = 10       'EXCEL�o�͊J�n�s��

    '�i��ʏW�v�\EXCEL�o�͗�ԍ�
    Private Const XLSCOL_H_HINSYUCD = 1     '�i��R�[�h
    Private Const XLSCOL_H_HINSYUNM = 2     '�i�햼
    Private Const XLSCOL_H_ZZZAIKO = 3      '�O�X�����݌ɗ�
    Private Const XLSCOL_H_ZSEISAN = 4      '�O�����Y���ї�
    Private Const XLSCOL_H_ZHANBAI = 5      '�O���̔����ї�
    Private Const XLSCOL_H_ZZAIKO = 6       '�O�����݌ɗ�
    Private Const XLSCOL_H_TSEISAN = 7      '�������Y�v���
    Private Const XLSCOL_H_THANBAI = 8      '�����̔��v���
    Private Const XLSCOL_H_TZAIKO = 9       '�������݌ɗ�
    Private Const XLSCOL_H_KURIKOSI = 10    '�J�z��
    Private Const XLSCOL_H_LOT = 11         '���b�g��
    Private Const XLSCOL_H_YSEISAN = 12     '�������Y��
    Private Const XLSCOL_H_YHANBAI = 13     '�����̔���
    Private Const XLSCOL_H_YZAIKO = 14      '�����݌ɗ�
    Private Const XLSCOL_H_YYHANBAI = 15    '���X���̔���

    '���Y�̔��݌Ɍv��EXCEL�o�͗�ԍ�
    Private Const XLSCOL_SIYOUCD As Integer = 1         '�d�l�R�[�h
    Private Const XLSCOL_HINMEI As Integer = 2          '�i��
    Private Const XLSCOL_LOTTYO As Integer = 3          '���b�g��
    Private Const XLSCOL_ABC As Integer = 4             'ABC�敪
    Private Const XLSCOL_ZZZAIKOS As Integer = 5        '�O�X�����݌ɐ�
    Private Const XLSCOL_ZSEISANS As Integer = 6        '�O�����Y���ѐ�
    Private Const XLSCOL_ZHANBAIS As Integer = 7        '�O���̔����ѐ�
    Private Const XLSCOL_ZZAIKOS As Integer = 8         '�O�����݌ɐ�
    Private Const XLSCOL_TSEISANS As Integer = 9        '�������Y�v�搔
    Private Const XLSCOL_TSEISANR As Integer = 10       '�������Y�v���
    Private Const XLSCOL_THANBAIS As Integer = 11       '�����̔��v�搔
    Private Const XLSCOL_THANBAIR As Integer = 12       '�����̔��v���
    Private Const XLSCOL_TZAIKOS As Integer = 13        '�������݌ɐ�
    Private Const XLSCOL_TZAIKOR As Integer = 14        '�������݌ɗ�
    Private Const XLSCOL_KURIKOSIS As Integer = 15      '�J�z��
    Private Const XLSCOL_LOTSU As Integer = 16          '���b�g��
    Private Const XLSCOL_YSEISANS As Integer = 17       '�������Y�v�搔
    Private Const XLSCOL_YSEISANR As Integer = 18       '�������Y�v���
    Private Const XLSCOL_YHANBAIS As Integer = 19       '�����̔��v�搔
    Private Const XLSCOL_YHANBAIR As Integer = 20       '�����̔��v���
    Private Const XLSCOL_YZAIKOS As Integer = 21        '�������݌ɐ�
    Private Const XLSCOL_YZAIKOR As Integer = 22        '�������݌ɗ�
    Private Const XLSCOL_YTUKISU As Integer = 23        '�݌Ɍ���
    Private Const XLSCOL_YYHANBAIS As Integer = 24      '���X���̔��v�搔
    Private Const XLSCOL_YYHANBAIR As Integer = 25      '���X���̔��v���
    Private Const XLSCOL_KIJUNTUKISU As Integer = 26    '�����
    Private Const XLSCOL_FUKKYUYO As Integer = 27       '�����p�݌ɐ�
    Private Const XLSCOL_ANZEN As Integer = 28          '���S�݌ɐ�
    Private Const XLSCOL_METUKE As Integer = 29         '�ڕt
    Private Const XLSCOL_ZZAIKOR As Integer = 30        '�O�X�����݌ɗ�

    '�o�͗�A���t�@�x�b�g
    Private Const XLSALP_ZZZAIKOS As String = "E"       '�O�X�����݌ɐ�
    Private Const XLSALP_ZSEISANS As String = "F"       '�O�����Y���ѐ�
    Private Const XLSALP_ZHANBAIS As String = "G"       '�O���̔����ѐ�
    Private Const XLSALP_ZZAIKOS As String = "H"        '�O�����݌ɐ�
    Private Const XLSALP_TSEISANS As String = "I"       '�������Y�v�搔
    Private Const XLSALP_TSEISANR As String = "J"       '�������Y�v���
    Private Const XLSALP_THANBAIS As String = "K"       '�����̔��v�搔
    Private Const XLSALP_THANBAIR As String = "L"       '�����̔��v���
    Private Const XLSALP_TZAIKOS As String = "M"        '�������݌ɐ�
    Private Const XLSALP_TZAIKOR As String = "N"        '�������݌ɗ�
    Private Const XLSALP_KURIKOSIS As String = "O"      '�J�z��
    Private Const XLSALP_LOTSU As String = "P"          '���b�g��
    Private Const XLSALP_YSEISANS As String = "Q"       '�������Y�v�搔
    Private Const XLSALP_YSEISANR As String = "R"       '�������Y�v���
    Private Const XLSALP_YHANBAIS As String = "S"       '�����̔��v�搔
    Private Const XLSALP_YHANBAIR As String = "T"       '�����̔��v���
    Private Const XLSALP_YZAIKOS As String = "U"        '�������݌ɐ�
    Private Const XLSALP_YZAIKOR As String = "V"        '�������݌ɗ�
    Private Const XLSALP_YTUKISU As String = "W"        '�݌Ɍ���
    Private Const XLSALP_YYHANBAIS As String = "X"      '���X���̔��v�搔
    Private Const XLSALP_YYHANBAIR As String = "Y"      '���X���̔��v���
    Private Const XLSALP_ZZAIKOR As String = "AD"       '�O�X�����݌ɗ�

    '�i��ʏW�v�\���`�V�[�g��
    Private Const XLSSHEETNM_HINSYU As String = "Ver1.0.00"

    Private Const CBONULLCODE As String = "NotSelect"   '�R���{�{�b�N�X���I�����̃R�[�h

#End Region

#Region "�����o�[�ϐ��錾"

    '-------------------------------------------------------------------------------
    '   �ϐ���`
    '-------------------------------------------------------------------------------

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu

    Private _oldRowIndex As Integer = -1            '�I���s�̔w�i�F��ύX���邽�߂̕ϐ�
    Private _colorCtlFlg As Boolean = False         '�I���s�̔w�i�F��ύX���邽�߂̃t���O

    Private _tanmatuID As String = ""               '�[��ID

    Private _changeFlg As Boolean = False           '�ꗗ�f�[�^�ύX�t���O
    Private _beforeChange As Double = 0             '�ꗗ�ύX�O�̃f�[�^

    Private _chkCellVO As UtilDgvChkCellVO          '�ꗗ�̓��͐����p

    '���������i�[�ϐ�
    Private _serchJuyo As String = ""               '���v��

    Private _startForm As Boolean = True            '��ʋN�������ǂ����̃t���O
    Private _saikeisanFlg As Boolean = False        '�Čv�Z�������ǂ����̃t���O

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
        _updFlg = prmUpdFlg                                                 '�X�V��

    End Sub
#End Region

#Region "Form�C�x���g"

    '-------------------------------------------------------------------------------
    '�@�����J�n�C�x���g
    '-------------------------------------------------------------------------------
    Private Sub ZG530E_SeisanSuuryouSyuusei_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            '�`��֌W�̐ݒ�
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           '�T�C�Y���ύX���ꂽ�Ƃ��ɁA�R���g���[�����R���g���[�����̂��ĕ`�悷�邩�ǂ����������l��ݒ�
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '�`��̓o�b�t�@�Ŏ��s����A������ɁA���ʂ���ʂɏo�͂����悤�ݒ�
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  '�R���g���[���́A��ʂɒ��ڂł͂Ȃ��A�܂��o�b�t�@�ɕ`�悳��܂��B����ɂ��A�������}���邱�Ƃ��ł��܂��B
            Me.SetStyle(ControlStyles.UserPaint, True)              '�R���g���[���́A�I�y���[�e�B���O �V�X�e���ɂ���Ăł͂Ȃ��A�Ǝ��ɕ`�悳���悤�ݒ�
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   '�R���g���[���̓E�B���h�E ���b�Z�[�W WM_ERASEBKGND �𖳎�����悤�ɐݒ�

            Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr '�^�C�g���I�v�V�����\��

            '��ʕ\��
            Call initForm()
            _startForm = False

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

#End Region

#Region "�{�^���C�x���g"

    '------------------------------------------------------------------------------------------------------
    '�@�o�^�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnTouroku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTouroku.Click
        Try

            '�Čv�Z����Ă��邩�`�F�b�N
            If _saikeisanFlg = False Then
                Throw New UsrDefException("�o�^�̑O�ɍČv�Z���s���Ă��������B", _msgHd.getMSG("doSaikeisan"), btnSaikeisan)
            End If

            '�K�{���̓`�F�b�N
            Call checkTouroku()

            '�o�^�m�F���b�Z�[�W
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmInsert")    '�o�^���܂��B
            If rtn <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If

            '�ύX�t���O�����Z�b�g����
            _changeFlg = False

            '�}�E�X�J�[�\�������v
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor
            Try

                '�ꗗ�̃f�[�^�����[�N�e�[�u���ɍX�V
                Call updateWK10()

                '�g�����U�N�V�����J�n
                _db.beginTran()

                'T41�֓o�^
                Call registT41()

                '�g�����U�N�V�����I��
                _db.commitTran()

                '�}�E�X�J�[�\�����
                Me.Cursor = Cursors.Arrow

                '���בւ����x���̕\��������
                Call initLabel()

                '�������b�Z�[�W
                _msgHd.dspMSG("completeInsert")

            Finally
                '�}�E�X�J�[�\�����
                Me.Cursor = cur
            End Try

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        Finally
            If _db.isTransactionOpen = True Then
                _db.rollbackTran()                          '���[���o�b�N
            End If
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�߂�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnModoru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModoru.Click

        '�x�����b�Z�[�W
        If _changeFlg Then
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmDgvEdit")   '�ҏW���̓��e���j������܂��B��낵���ł����H
            If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        '����ʂ��I�����A���j���[��ʂɖ߂�B
        _parentForm.Show()
        _parentForm.Activate()

        Me.Close()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�����{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnKensaku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensaku.Click

        Try
            '�x�����b�Z�[�W
            If _changeFlg Then
                Dim rtn As DialogResult = _msgHd.dspMSG("confirmSrcEdit")   '�ҏW���̓��e���j������܂��B��낵���ł����H
                If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If

            '�ύX�t���O�����Z�b�g����
            _changeFlg = False

            '�}�E�X�J�[�\�������v
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor
            Try

                '���������̍쐬
                Dim sqlWhere As String = ""
                sqlWhere = createSerchStr()

                '�g�����U�N�V�����J�n
                _db.beginTran()

                '���[�N�e�[�u���̍쐬
                Call delInsWK10(sqlWhere)
                Call createWK10()

                '�g�����U�N�V�����I��
                _db.commitTran()

                '�ꗗ�s���F�t���O�𖳌��ɂ���
                _colorCtlFlg = False

                '�ꗗ�\��
                Call dispWK10()

                '�Čv�Z�ς݂ɂ���
                _saikeisanFlg = True

                '�ꗗ�s���F�t���O��L���ɂ���
                _colorCtlFlg = True

                '���b�g����̈�ԏ�̃Z���փt�H�[�J�X����
                setForcusCol(COLNO_LOTSUU, 0)

                '���בւ����x���̕\��������
                Call initLabel()

            Finally
                '�}�E�X�J�[�\�����
                Me.Cursor = cur
            End Try

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        Finally
            If _db.isTransactionOpen = True Then
                _db.rollbackTran()                          '���[���o�b�N
            End If
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�\���P�ʕύX�I�v�V�����{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub rdoKm_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoKm.CheckedChanged
        Try

            '��ʋN�����͏������Ȃ�
            If _startForm Then
                Exit Sub
            End If

            '�Čv�Z�{�^���������̏����𑖂点��(�\���ؑւ������ɑ���)
            Call btnSaikeisan_Click(Nothing, Nothing)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�Čv�Z�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnSaikeisan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaikeisan.Click
        Try

            '�}�E�X�J�[�\�������v
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor
            Try

                '�ꗗ�̃f�[�^�����[�N�e�[�u���ɍX�V
                Call updateWK10()

                '�v�搔�E�v��ʂ̍Čv�Z
                Call culcUpdateWK10()

                _colorCtlFlg = False

                '�ꗗ�ĕ\��
                Call dispWK10()

                _colorCtlFlg = True

                '���b�g����̈�ԏ�̃Z���փt�H�[�J�X����
                setForcusCol(COLNO_LOTSUU, 0)

                '���בւ����x���̕\��������
                Call initLabel()

                '�I������Ă���P�ʂɏ]���ĉ�ʕ\��
                Call changeDisp()

                '�Čv�Z�ς݂ɂ���
                _saikeisanFlg = True

            Finally
                '�}�E�X�J�[�\�����
                Me.Cursor = cur
            End Try

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        Finally
            If _db.isTransactionOpen = True Then
                _db.rollbackTran()                          '���[���o�b�N
            End If
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�i��ʏW�v�\�o�̓{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnHinsyuPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHinsyuPrint.Click
        '-->2010.12.12 add by takagi
        Dim cur As Cursor = Me.Cursor
        Me.Cursor = Cursors.WaitCursor
        Try
            '<--2010.12.12 add by takagi

            '-->2010.12.27 add by takagi #������
            Dim pb As UtilProgresBarCancelable = New UtilProgresBarCancelable(Me)
            pb.Show()
            Try
                Application.DoEvents()
                pb.Cursor = Cursors.WaitCursor
                pb.Refresh()
                Application.DoEvents()

                '�v���O���X�o�[�ݒ�
                pb.jobName = "�o�͂��������Ă��܂��B"
                pb.status = "�f�[�^�x�[�X�⍇�����D�D�D"
                '<--2010.12.27 add by takagi #������

                '�ꗗ�̃f�[�^�����[�N�e�[�u���ɍX�V
                Call updateWK10()

                '�v�搔�E�v��ʂ̍Čv�Z
                Call culcUpdateWK10()

                pb.jobName = "�o�͂��Ă��܂��B" '2010.12.27 add by takagi #������
                pb.status = ""                  '2010.12.27 add by takagi #������

                'EXCEL�o��
                '-->2010.12.27 chg by takagi #������
                'Call printHinsyuExcel()
                Call printHinsyuExcel(pb)
                '<--2010.12.27 chg by takagi #������

                '-->2010.12.27 add by takagi #������
            Catch pbe As UtilProgressBarCancelEx
                '�L�����Z�������������Ȃ�
            Finally
                '��ʏ���
                pb.Close()
            End Try
            '<--2010.12.27 add by takagi #������

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
            '-->2010.12.12 add by takagi
        Finally
            Me.Cursor = cur
            '<--2010.12.12 add by takagi
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@���Y�̔��݌Ɍv��o�̓{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnSeisanPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeisanPrint.Click
        '-->2010.12.12 add by takagi
        Dim cur As Cursor = Me.Cursor
        Me.Cursor = Cursors.WaitCursor
        Try
            '<--2010.12.12 add by takagi

            '-->2010.12.27 add by takagi #������
            Dim pb As UtilProgresBarCancelable = New UtilProgresBarCancelable(Me)
            pb.Show()
            Try
                Application.DoEvents()
                pb.Cursor = Cursors.WaitCursor
                pb.Refresh()
                Application.DoEvents()

                '�v���O���X�o�[�ݒ�
                pb.jobName = "�o�͂��������Ă��܂��B"
                pb.status = "�f�[�^�x�[�X�⍇�����D�D�D"
                '<--2010.12.27 add by takagi #������


                '�ꗗ�̃f�[�^�����[�N�e�[�u���ɍX�V
                Call updateWK10()

                '�v�搔�E�v��ʂ̍Čv�Z
                Call culcUpdateWK10()

                pb.jobName = "�o�͂��Ă��܂��B" '2010.12.27 add by takagi #������
                pb.status = ""                  '2010.12.27 add by takagi #������

                'EXCEL�o��
                '-->2010.12.27 chg by takagi #������
                'Call printSeisanExcel()
                Call printSeisanExcel(pb)
                '<--2010.12.27 chg by takagi #������

                '-->2010.12.27 add by takagi #������
            Catch pbe As UtilProgressBarCancelEx
                '�L�����Z�������������Ȃ�
            Finally
                '��ʏ���
                pb.Close()
            End Try
            '<--2010.12.27 add by takagi #������

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
            '-->2010.12.12 add by takagi
        Finally
            Me.Cursor = cur
            '<--2010.12.12 add by takagi
        End Try

    End Sub

#End Region

#Region "���[�U��`�֐�:EXCEL�֘A"

    '------------------------------------------------------------------------------------------------------
    '�@�i��ʏW�v�\EXCEL�o�͏���
    '------------------------------------------------------------------------------------------------------
    '-->2010.12.27 chg by takagi #������
    'Private Sub printHinsyuExcel()
    Private Sub printHinsyuExcel(ByVal prmPb As UtilProgresBarCancelable)
        '<--2010.12.27 chg by takagi #������
        Try

            '���`�t�@�C��(�i���ʔ̔��v��Ɠ������`)
            Dim openFilePath As String = StartUp.iniValue.BaseXlsPath & "\" & StartUp.iniValue.ExcelZG530R2_Base
            '���`�t�@�C�����J����Ă��Ȃ����`�F�b�N
            Dim fh As UtilFile = New UtilFile()
            Try
                fh.move(openFilePath, openFilePath & 1)
                fh.move(openFilePath & 1, openFilePath)
                '-->2010.12.27 add by takagi #������
            Catch pbe As UtilProgressBarCancelEx
                Throw pbe '��ʓ]��
                '<--2010.12.27 add by takagi #������
            Catch ioe As System.IO.IOException
                Throw New UsrDefException("�t�@�C�����J����Ă��܂��B�t�@�C������Ă���Ď��s���Ă��������B", _
                                          _msgHd.getMSG("fileOpenErr", "�y�t�@�C�����z�F" & openFilePath))
            End Try

            '�o�͗p�t�@�C��
            '�t�@�C�����擾-----------------------------------------------------
            Dim wkEditFile As String = StartUp.iniValue.OutXlsPath & "\" & StartUp.iniValue.ExcelZG530R2_Out     '�R�s�[��t�@�C��

            '�R�s�[��t�@�C�������݂���ꍇ�A�R�s�[��t�@�C�����폜----------------
            If UtilClass.isFileExists(wkEditFile) Then
                Try
                    fh.delete(wkEditFile)
                    '-->2010.12.27 add by takagi #������
                Catch pbe As UtilProgressBarCancelEx
                    Throw pbe '��ʓ]��
                    '<--2010.12.27 add by takagi #������
                Catch ioe As System.IO.IOException
                    Throw New UsrDefException("�t�@�C�����J����Ă��܂��B�t�@�C������Ă���Ď��s���Ă��������B", _
                                              _msgHd.getMSG("fileOpenErr", "�y�t�@�C�����z�F" & wkEditFile))
                End Try
            End If

            Try
                '�o�͗p�t�@�C���֐��^�t�@�C���R�s�[
                FileCopy(openFilePath, wkEditFile)
                '-->2010.12.27 add by takagi #������
            Catch pbe As UtilProgressBarCancelEx
                Throw pbe '��ʓ]��
                '<--2010.12.27 add by takagi #������
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
                    sql = sql & N & " WHERE KOTEIKEY = '" & KOTEI_JUYOU & "'"
                    'SQL���s
                    Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
                    Dim dsHanyo As DataSet = _db.selectDB(sql, RS, iRecCnt)

                    If iRecCnt <= 0 Then                    'M01�ėp�}�X�^���o���R�[�h���P�����Ȃ��ꍇ
                        Throw New UsrDefException("�o�^����Ă��܂���B", _msgHd.getMSG("noData"))
                    End If

                    '���v��̐��������[�v
                    For hanyoCnt As Integer = 0 To iRecCnt - 1
                        '���[�N�e�[�u���̒l���f�[�^�Z�b�g�ɕێ�
                        Dim ds As DataSet = Nothing
                        Dim rowCnt As Integer = 0
                        '���v�悲�ƂɃ��[�N�̃f�[�^�𒊏o
                        Call getDataForHinsyuXls(_db.rmNullStr(dsHanyo.Tables(RS).Rows(hanyoCnt)(HANYOU_KAHENKEY)), ds, rowCnt)

                        If rowCnt > 0 Then

                            '���V�[�g(���`)�𕡐��ۑ�
                            Dim baseName As String = XLSSHEETNM_HINSYU  '���`�V�[�g��
                            Dim newName As String = _db.rmNullStr(dsHanyo.Tables(RS).Rows(hanyoCnt)(HANYOU_NAME1))     '�V���ɍ쐬����V�[�g
                            Try
                                eh.targetSheet = baseName               '���`�V�[�g�I��
                                eh.copySheetOnLast(newName)
                                '-->2010.12.27 add by takagi #������
                            Catch pbe As UtilProgressBarCancelEx
                                Throw pbe '��ʓ]��
                                '<--2010.12.27 add by takagi #������
                            Catch ex As Exception
                                Throw New UsrDefException("�V�[�g�̕����Ɏ��s���܂����B", _msgHd.getMSG("failCopySheet"))
                            End Try

                            '-->2010.12.27 chg by takagi #������
                            prmPb.status = newName & "�o�͒�"
                            '<--2010.12.27 chg by takagi #������

                            eh.targetSheet = newName

                            Dim startPrintRow As Integer = START_PRINT          '�o�͊J�n�s��

                            '���v�ێ��p�̕ϐ�
                            Dim zzZaiko As Double = 0           '�O�X�����݌ɗ�
                            Dim zSeisanryou As Double = 0       '�O�����Y���ї�
                            Dim zHanbairyou As Double = 0       '�O���̔����ї�
                            Dim zZaikoryou As Double = 0        '�O�����݌ɗ�
                            Dim tSeisanryou As Double = 0       '�������Y�v���
                            Dim tHanbairyou As Double = 0       '�����̔��v���
                            Dim tZaikoryou As Double = 0        '�������݌ɗ�
                            Dim kurikosiryou As Double = 0      '�J�z��
                            Dim lot As Double = 0               '���b�g��
                            Dim ySeisanryou As Double = 0       '�������Y��
                            Dim yHanbairyou As Double = 0       '�����̔���
                            Dim yZaikoryou As Double = 0        '�����݌ɗ�
                            Dim yyHanbairyou As Double = 0      '���X���̔���
                            With ds.Tables(RS)
                                Dim i As Integer
                                prmPb.maxVal = rowCnt '2010.12.27 chg by takagi #������
                                For i = 0 To rowCnt - 1

                                    prmPb.value = i + 1 '2010.12.27 chg by takagi #������


                                    '���1�s�ǉ�
                                    eh.copyRow(startPrintRow + i)
                                    eh.insertPasteRow(startPrintRow + i)

                                    '�ꗗ�f�[�^�o��
                                    eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_HINSYUCD)), startPrintRow + i, XLSCOL_H_HINSYUCD)      '�i��R�[�h
                                    eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_HINSYUNM)), startPrintRow + i, XLSCOL_H_HINSYUNM)      '�i�햼
                                    eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ZZZAIKOR)), startPrintRow + i, XLSCOL_H_ZZZAIKO)       '�O�X�����݌ɗ�
                                    eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ZSEISANR)), startPrintRow + i, XLSCOL_H_ZSEISAN)       '�O�����Y���ї�
                                    eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ZHANBAIR)), startPrintRow + i, XLSCOL_H_ZHANBAI)       '�O���̔����ї�
                                    eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ZZAIKOR)), startPrintRow + i, XLSCOL_H_ZZAIKO)         '�O�����݌ɗ�
                                    eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_TSEISANR)), startPrintRow + i, XLSCOL_H_TSEISAN)       '�������Y�v���
                                    eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_THANBAIR)), startPrintRow + i, XLSCOL_H_THANBAI)       '�����̔��v���
                                    eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_TZAIKOR)), startPrintRow + i, XLSCOL_H_TZAIKO)         '�������݌ɗ�
                                    eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_KURIKOSIR)), startPrintRow + i, XLSCOL_H_KURIKOSI)     '�J�z��
                                    eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_LOTSU)), startPrintRow + i, XLSCOL_H_LOT)              '���b�g��
                                    eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YSEISANR)), startPrintRow + i, XLSCOL_H_YSEISAN)       '�������Y��
                                    eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YHANBAIR)), startPrintRow + i, XLSCOL_H_YHANBAI)       '�����̔���
                                    eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YZAIKOR)), startPrintRow + i, XLSCOL_H_YZAIKO)         '�����݌ɗ�
                                    eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YYHANBAIR)), startPrintRow + i, XLSCOL_H_YYHANBAI)     '���X���̔���

                                    '���v�l�v�Z
                                    zzZaiko = zzZaiko + _db.rmNullDouble(.Rows(i)(COLDT_ZZZAIKOR))            '�O�X�����݌ɗ�
                                    zSeisanryou = zSeisanryou + _db.rmNullDouble(.Rows(i)(COLDT_ZSEISANR))    '�O�����Y���ї�
                                    zHanbairyou = zHanbairyou + _db.rmNullDouble(.Rows(i)(COLDT_ZHANBAIR))    '�O���̔����ї�
                                    zZaikoryou = zZaikoryou + _db.rmNullDouble(.Rows(i)(COLDT_ZZAIKOR))       '�O�����݌ɗ�
                                    tSeisanryou = tSeisanryou + _db.rmNullDouble(.Rows(i)(COLDT_TSEISANR))    '�������Y�v���
                                    tHanbairyou = tHanbairyou + _db.rmNullDouble(.Rows(i)(COLDT_THANBAIR))    '�����̔��v���
                                    tZaikoryou = tZaikoryou + _db.rmNullDouble(.Rows(i)(COLDT_TZAIKOR))       '�������݌ɗ�
                                    kurikosiryou = kurikosiryou + _db.rmNullDouble(.Rows(i)(COLDT_KURIKOSIR)) '�J�z��
                                    lot = lot + _db.rmNullDouble(.Rows(i)(COLDT_LOTSU))                       '���b�g��
                                    ySeisanryou = ySeisanryou + _db.rmNullDouble(.Rows(i)(COLDT_YSEISANR))    '�������Y��
                                    yHanbairyou = yHanbairyou + _db.rmNullDouble(.Rows(i)(COLDT_YHANBAIR))    '�����̔���
                                    yZaikoryou = yZaikoryou + _db.rmNullDouble(.Rows(i)(COLDT_YZAIKOR))       '�����݌ɗ�
                                    yyHanbairyou = yyHanbairyou + _db.rmNullDouble(.Rows(i)(COLDT_YYHANBAIR)) '���X���̔���
                                Next

                                '�]���ȋ�s���폜
                                eh.deleteRow(startPrintRow + i)

                                '���v�s�\��
                                eh.setValue(zzZaiko, startPrintRow + i, XLSCOL_H_ZZZAIKO)         '�O�X�����݌ɗ�
                                eh.setValue(zSeisanryou, startPrintRow + i, XLSCOL_H_ZSEISAN)     '�O�����Y���ї�
                                eh.setValue(zHanbairyou, startPrintRow + i, XLSCOL_H_ZHANBAI)     '�O���̔����ї�
                                eh.setValue(zZaikoryou, startPrintRow + i, XLSCOL_H_ZZAIKO)       '�O�����݌ɗ�
                                eh.setValue(tSeisanryou, startPrintRow + i, XLSCOL_H_TSEISAN)     '�������Y�v���
                                eh.setValue(tHanbairyou, startPrintRow + i, XLSCOL_H_THANBAI)     '�����̔��v���
                                eh.setValue(tZaikoryou, startPrintRow + i, XLSCOL_H_TZAIKO)       '�������݌ɗ�
                                eh.setValue(kurikosiryou, startPrintRow + i, XLSCOL_H_KURIKOSI)   '�J�z��
                                eh.setValue(lot, startPrintRow + i, XLSCOL_H_LOT)                 '���b�g��
                                eh.setValue(ySeisanryou, startPrintRow + i, XLSCOL_H_YSEISAN)     '�������Y��
                                eh.setValue(yHanbairyou, startPrintRow + i, XLSCOL_H_YHANBAI)     '�����̔���
                                eh.setValue(yZaikoryou, startPrintRow + i, XLSCOL_H_YZAIKO)       '�����݌ɗ�
                                eh.setValue(yyHanbairyou, startPrintRow + i, XLSCOL_H_YYHANBAI)   '���X���̔���
                            End With
                            '�쐬�����ҏW
                            Dim printDate As String = Now.ToString("yyyy/MM/dd HH:mm")
                            eh.setValue("�쐬���� �F " & printDate, 1, 15)   'O1

                            '���v�i���ҏW
                            eh.setValue("(" & _db.rmNullStr(dsHanyo.Tables(RS).Rows(hanyoCnt)(HANYOU_NAME1)) & ")", 2, 3)      'C2

                            '�����N���A�v��N���ҏW
                            eh.setValue("�����N���F" & lblSyori.Text & "�@�@�v��N���F" & lblKeikaku.Text, 2, 6)    'F2

                            '��������
                            eh.setValue("���v��F" & _serchJuyo, 4, 1)        'A4

                            '�w�b�_�[�̔N���ҏW
                            eh.setValue(lblZengetu.Text, 7, 5)      'E7�@�O��
                            eh.setValue(lblTogetu.Text, 7, 8)       'H7�@����
                            eh.setValue(lblYokugetu.Text, 7, 12)    'L7�@����
                            eh.setValue(lblYygetu.Text, 7, 15)      'O7�@���X��
                            '�O�X���̌����v�Z
                            '-->2011.01.15 chg by takagi #69
                            'Dim zzgetu As Integer = CInt(lblSyori.Text.Substring(5, 2)) - 2
                            'If zzgetu = 0 Then
                            '    zzgetu = 12
                            'End If
                            Dim zzgetu As Integer = Format(DateAdd(DateInterval.Month, -2, CDate(lblSyori.Text & "/01")), "MM")
                            '<--2011.01.15 chg by takagi #69
                            eh.setValue(zzgetu & "��", 7, 3)               'C7�@�O�X��

                            '����̃Z���Ƀt�H�[�J�X���Ă�
                            eh.selectCell(START_PRINT, XLSCOL_H_HINSYUCD)     'A10

                            Clipboard.Clear()       '�N���b�v�{�[�h�̏�����
                        End If
                    Next
                    eh.deleteSheet("Ver1.0.00")

                    '' 2011/01/20 add start sugano
                    '�擪�V�[�g��I��
                    eh.targetSheetByIdx = 1
                    eh.selectSheet(eh.targetSheet)
                    eh.selectCell(1, 1)
                    '' 2011/01/20 add end sugano

                Finally
                    eh.close()
                End Try

                'EXCEL�t�@�C���J��
                eh.display()

                '-->2010.12.27 add by takagi #������
            Catch pbe As UtilProgressBarCancelEx
                Throw pbe '��ʓ]��
                '<--2010.12.27 add by takagi #������
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

            '-->2010.12.27 add by takagi #������
        Catch pbe As UtilProgressBarCancelEx
            Throw pbe '��ʓ]��
            '<--2010.12.27 add by takagi #������
        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@���Y�̔��݌Ɍv��EXCEL�o�͏���
    '------------------------------------------------------------------------------------------------------
    '-->2010.12.27 chg by takagi #������
    'Private Sub printSeisanExcel()
    Private Sub printSeisanExcel(ByVal prmPb As UtilProgresBarCancelable)
        '<--2010.12.27 chg by takagi #������
        Try


            '���`�t�@�C��(�i���ʔ̔��v��Ɠ������`)
            Dim openFilePath As String = StartUp.iniValue.BaseXlsPath & "\" & StartUp.iniValue.ExcelZG530R1_Base
            '���`�t�@�C�����J����Ă��Ȃ����`�F�b�N
            Dim fh As UtilFile = New UtilFile()
            Try
                fh.move(openFilePath, openFilePath & 1)
                fh.move(openFilePath & 1, openFilePath)

                '-->2010.12.27 add by takagi #������
            Catch pbe As UtilProgressBarCancelEx
                Throw pbe '��ʓ]��
                '<--2010.12.27 add by takagi #������
            Catch ioe As System.IO.IOException
                Throw New UsrDefException("�t�@�C�����J����Ă��܂��B�t�@�C������Ă���Ď��s���Ă��������B", _
                                          _msgHd.getMSG("fileOpenErr", "�y�t�@�C�����z�F" & openFilePath))
            End Try

            '�o�͗p�t�@�C��
            '�t�@�C�����擾-----------------------------------------------------
            Dim wkEditFile As String = StartUp.iniValue.OutXlsPath & "\" & StartUp.iniValue.ExcelZG530R1_Out     '�R�s�[��t�@�C��

            '�R�s�[��t�@�C�������݂���ꍇ�A�R�s�[��t�@�C�����폜----------------
            If UtilClass.isFileExists(wkEditFile) Then
                Try
                    fh.delete(wkEditFile)
                    '-->2010.12.27 add by takagi #������
                Catch pbe As UtilProgressBarCancelEx
                    Throw pbe '��ʓ]��
                    '<--2010.12.27 add by takagi #������
                Catch ioe As System.IO.IOException
                    Throw New UsrDefException("�t�@�C�����J����Ă��܂��B�t�@�C������Ă���Ď��s���Ă��������B", _
                                              _msgHd.getMSG("fileOpenErr", "�y�t�@�C�����z�F" & wkEditFile))
                End Try
            End If

            Try
                '�o�͗p�t�@�C���֐��^�t�@�C���R�s�[
                FileCopy(openFilePath, wkEditFile)
                '-->2010.12.27 add by takagi #������
            Catch pbe As UtilProgressBarCancelEx
                Throw pbe '��ʓ]��
                '<--2010.12.27 add by takagi #������
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
                    sql = sql & N & " WHERE KOTEIKEY = '" & KOTEI_JUYOU & "'"
                    'SQL���s
                    Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
                    Dim dsHanyo As DataSet = _db.selectDB(sql, RS, iRecCnt)

                    If iRecCnt <= 0 Then                    'M01�ėp�}�X�^���o���R�[�h���P�����Ȃ��ꍇ
                        Throw New UsrDefException("�o�^����Ă��܂���B", _msgHd.getMSG("noData"))
                    End If

                    '���v��̐��������[�v
                    For hanyoCnt As Integer = 0 To iRecCnt - 1
                        '���[�N�e�[�u���̒l���f�[�^�Z�b�g�ɕێ�
                        Dim ds As DataSet = Nothing
                        Dim rowCnt As Integer = 0
                        '���v�悲�ƂɃ��[�N�̃f�[�^�𒊏o
                        Call getDataForSeisanXls(_db.rmNullStr(dsHanyo.Tables(RS).Rows(hanyoCnt)(HANYOU_KAHENKEY)), ds, rowCnt)

                        If rowCnt > 0 Then

                            '���V�[�g(���`)�𕡐��ۑ�
                            Dim baseName As String = XLSSHEETNM_HINSYU  '���`�V�[�g��
                            Dim newName As String = _db.rmNullStr(dsHanyo.Tables(RS).Rows(hanyoCnt)(HANYOU_NAME1))     '�V���ɍ쐬����V�[�g
                            Try
                                eh.targetSheet = baseName               '���`�V�[�g�I��
                                eh.copySheetOnLast(newName)
                                '-->2010.12.27 add by takagi #������
                            Catch pbe As UtilProgressBarCancelEx
                                Throw pbe '��ʓ]��
                                '<--2010.12.27 add by takagi #������
                            Catch ex As Exception
                                Throw New UsrDefException("�V�[�g�̕����Ɏ��s���܂����B", _msgHd.getMSG("failCopySheet"))
                            End Try

                            '-->2010.12.27 chg by takagi #������
                            prmPb.status = newName & "�o�͒�"
                            '<--2010.12.27 chg by takagi #������

                            eh.targetSheet = newName

                            Dim startPrintRow As Integer = START_PRINT          '�o�͊J�n�s��

                            '�ŏ��̕i��R�[�h������
                            Dim startHinsyu As String = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_HINSYUCD))

                            Dim startTotalRow As Integer = startPrintRow    '�u�i��v�v�o�͂��邽�߂ɍ��v����ŏ��̗�
                            Dim totalRow As Integer = 0         '�u�i��v�v�̗���o�͂�����

                            '�|���ݒ�p
                            Dim lineV As LineVO = New LineVO()

                            Dim i As Integer                    '���[�v�J�E���^�[
                            Dim xlsRow As Integer = startPrintRow + i + totalRow

                            '-->2011.03.23 add by takagi #�݌Ɍ����͓����݌Ɂ������̔��ŋ��߂�
                            Dim existsMetsukeFlg As Boolean = False
                            '<--2011.03.23 add by takagi #�݌Ɍ����͓����݌Ɂ������̔��ŋ��߂�

                            '-->2010.12.27 add by takagi #������
                            Dim buf As System.Text.StringBuilder = New System.Text.StringBuilder
                            With ds.Tables(RS)
                                prmPb.maxVal = rowCnt
                                '<--2010.12.27 add by takagi #������

                                For i = 0 To rowCnt - 1
                                    xlsRow = startPrintRow + i + totalRow
                                    'With ds.Tables(RS)         '2010.12.27 del by takagi #������
                                    If startHinsyu.Equals(_db.rmNullStr(ds.Tables(RS).Rows(i)(COLDT_HINSYUCD))) Then

                                        '�s��1�s�ǉ�
                                        eh.copyRow(xlsRow)
                                        eh.insertPasteRow(xlsRow)
                                        '�ꗗ�f�[�^�o��
                                        '-->2010.12.27 chg by takagi #������
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_SIYOCD)), xlsRow, XLSCOL_SIYOUCD)        '�d�l�R�[�h                                    
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_HINMEI)), xlsRow, XLSCOL_HINMEI)         '�i�햼
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_LOTTYO)), xlsRow, XLSCOL_LOTTYO)         '���b�g��
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ABC)), xlsRow, XLSCOL_ABC)               'ABC
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ZZZAIKOS)), xlsRow, XLSCOL_ZZZAIKOS)     '�O�X���݌ɐ�
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ZSEISANS)), xlsRow, XLSCOL_ZSEISANS)     '�O�����Y���ѐ�
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ZHANBAIS)), xlsRow, XLSCOL_ZHANBAIS)     '�O���̔����ѐ�
                                        'eh.setValue("=(" & XLSALP_ZZZAIKOS & CStr(xlsRow) & " + " & XLSALP_ZSEISANS & CStr(xlsRow) & " - " & XLSALP_ZHANBAIS & CStr(xlsRow) & ")", xlsRow, XLSCOL_ZZAIKOS)  '�O�����݌ɐ�
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ZZAIKOR)), xlsRow, XLSCOL_ZZAIKOR)       '�O�����݌ɗ�
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_TSEISANS)), xlsRow, XLSCOL_TSEISANS)     '�������Y�v�搔
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_TSEISANR)), xlsRow, XLSCOL_TSEISANR)     '�������Y�v���
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_THANBAIS)), xlsRow, XLSCOL_THANBAIS)     '�����̔��v�搔
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_THANBAIR)), xlsRow, XLSCOL_THANBAIR)     '�����̔��v���
                                        'eh.setValue("=(" & XLSALP_ZZAIKOS & CStr(xlsRow) & " + " & XLSALP_TSEISANS & CStr(xlsRow) & " - " & XLSALP_THANBAIS & CStr(xlsRow) & ")", xlsRow, XLSCOL_TZAIKOS)   '�������݌ɐ�
                                        'eh.setValue("=(" & XLSALP_ZZAIKOR & CStr(xlsRow) & " + " & XLSALP_TSEISANR & CStr(xlsRow) & " - " & XLSALP_THANBAIR & CStr(xlsRow) & ")", xlsRow, XLSCOL_TZAIKOR)   '�������݌ɗ�
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_KURIKOSIS)), xlsRow, XLSCOL_KURIKOSIS)   '�J�z��
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_LOTSU)), xlsRow, XLSCOL_LOTSU)           '���b�g��
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YSEISANS)), xlsRow, XLSCOL_YSEISANS)     '�������Y�v�搔
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YSEISANR)), xlsRow, XLSCOL_YSEISANR)     '�������Y�v���
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YHANBAIS)), xlsRow, XLSCOL_YHANBAIS)     '�����̔��v�搔
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YHANBAIR)), xlsRow, XLSCOL_YHANBAIR)     '�����̔��v���
                                        'eh.setValue("=(" & XLSALP_TZAIKOS & CStr(xlsRow) & " + " & XLSALP_YSEISANS & CStr(xlsRow) & " - " & XLSALP_YHANBAIS & CStr(xlsRow) & ")", xlsRow, XLSCOL_YZAIKOS)   '�������݌ɐ�
                                        'eh.setValue("=(" & XLSALP_TZAIKOR & CStr(xlsRow) & " + " & XLSALP_YSEISANR & CStr(xlsRow) & " - " & XLSALP_YHANBAIR & CStr(xlsRow) & ")", xlsRow, XLSCOL_YZAIKOR)   '�������݌ɗ�
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ZAIKOTUKISU)), xlsRow, XLSCOL_YTUKISU)   '�݌Ɍ���
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YYHANBAIS)), xlsRow, XLSCOL_YYHANBAIS)   '���X���̔��v�搔
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YYHANBAIR)), xlsRow, XLSCOL_YYHANBAIR)   '���X���̔��v���
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_KTUKISU)), xlsRow, XLSCOL_KIJUNTUKISU)   '�����
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_FZAIKOS)), xlsRow, XLSCOL_FUKKYUYO)      '�����p�݌�
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_AZAIKOS)), xlsRow, XLSCOL_ANZEN)         '���S�݌�
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_METUKE)), xlsRow, XLSCOL_METUKE)         '�ڕt
                                        prmPb.value = i + 1
                                        buf.Remove(0, buf.Length)
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_SIYOCD)) & ControlChars.Tab)       '�d�l�R�[�h                                    
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_HINMEI)) & ControlChars.Tab)       '�i�햼
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_LOTTYO)) & ControlChars.Tab)       '���b�g��
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_ABC)) & ControlChars.Tab)          'ABC
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_ZZZAIKOS)) & ControlChars.Tab)     '�O�X���݌ɐ�
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_ZSEISANS)) & ControlChars.Tab)     '�O�����Y���ѐ�
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_ZHANBAIS)) & ControlChars.Tab)     '�O���̔����ѐ�
                                        buf.Append("=(" & XLSALP_ZZZAIKOS & CStr(xlsRow) & " + " & XLSALP_ZSEISANS & CStr(xlsRow) & " - " & XLSALP_ZHANBAIS & CStr(xlsRow) & ")" & ControlChars.Tab)  '�O�����݌ɐ�
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_TSEISANS)) & ControlChars.Tab)     '�������Y�v�搔
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_TSEISANR)) & ControlChars.Tab)     '�������Y�v���
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_THANBAIS)) & ControlChars.Tab)     '�����̔��v�搔
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_THANBAIR)) & ControlChars.Tab)     '�����̔��v���
                                        buf.Append("=(" & XLSALP_ZZAIKOS & CStr(xlsRow) & " + " & XLSALP_TSEISANS & CStr(xlsRow) & " - " & XLSALP_THANBAIS & CStr(xlsRow) & ")" & ControlChars.Tab)   '�������݌ɐ�
                                        buf.Append("=(" & XLSALP_ZZAIKOR & CStr(xlsRow) & " + " & XLSALP_TSEISANR & CStr(xlsRow) & " - " & XLSALP_THANBAIR & CStr(xlsRow) & ")" & ControlChars.Tab)   '�������݌ɗ�
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_KURIKOSIS)) & ControlChars.Tab)    '�J�z��
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_LOTSU)) & ControlChars.Tab)        '���b�g��
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_YSEISANS)) & ControlChars.Tab)     '�������Y�v�搔
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_YSEISANR)) & ControlChars.Tab)     '�������Y�v���
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_YHANBAIS)) & ControlChars.Tab)     '�����̔��v�搔
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_YHANBAIR)) & ControlChars.Tab)     '�����̔��v���
                                        buf.Append("=(" & XLSALP_TZAIKOS & CStr(xlsRow) & " + " & XLSALP_YSEISANS & CStr(xlsRow) & " - " & XLSALP_YHANBAIS & CStr(xlsRow) & ")" & ControlChars.Tab)   '�������݌ɐ�
                                        buf.Append("=(" & XLSALP_TZAIKOR & CStr(xlsRow) & " + " & XLSALP_YSEISANR & CStr(xlsRow) & " - " & XLSALP_YHANBAIR & CStr(xlsRow) & ")" & ControlChars.Tab)   '�������݌ɗ�
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_ZAIKOTUKISU)) & ControlChars.Tab)  '�݌Ɍ���
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_YYHANBAIS)) & ControlChars.Tab)    '���X���̔��v�搔
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_YYHANBAIR)) & ControlChars.Tab)    '���X���̔��v���
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_KTUKISU)) & ControlChars.Tab)      '�����
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_FZAIKOS)) & ControlChars.Tab)      '�����p�݌�
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_AZAIKOS)) & ControlChars.Tab)      '���S�݌�
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_METUKE)) & ControlChars.Tab)       '�ڕt
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_ZZAIKOR)) & ControlChars.Tab)      '�O�����݌ɗ�
                                        Clipboard.SetText(buf.ToString)
                                        eh.paste(xlsRow, XLSCOL_SIYOUCD)
                                        Clipboard.Clear()
                                        '<--2010.12.27 chg by takagi #������

                                        '-->2011.03.23 add by takagi #�݌Ɍ����͓����݌Ɂ������̔��ŋ��߂�
                                        '�ڕt�����݂���΃t���O�𗧂Ă�
                                        If "".Equals(_db.rmNullStr(.Rows(i)(COLDT_METUKE))) Or _
                                           _db.rmNullDouble(.Rows(i)(COLDT_METUKE)) = 0 Then
                                            existsMetsukeFlg = False
                                        Else
                                            existsMetsukeFlg = True
                                        End If
                                        '<--2011.03.23 add by takagi #�݌Ɍ����͓����݌Ɂ������̔��ŋ��߂�
                                    Else

                                        '�s��1�s�ǉ�
                                        eh.copyRow(xlsRow)
                                        eh.insertPasteRow(xlsRow)

                                        '�i��v�s�o��
                                        '-->2010.12.27 chg by takagi #������
                                        'eh.setValue("�i �� �v", xlsRow, XLSCOL_HINMEI)
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_ZZZAIKOS & startTotalRow & ":" & XLSALP_ZZZAIKOS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_ZZZAIKOS)     '�O�X�����݌ɐ�
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_ZSEISANS & startTotalRow & ":" & XLSALP_ZSEISANS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_ZSEISANS)     '�O�����Y���ѐ�
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_ZHANBAIS & startTotalRow & ":" & XLSALP_ZHANBAIS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_ZHANBAIS)     '�O���̔����ѐ�
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_ZZAIKOS & startTotalRow & ":" & XLSALP_ZZAIKOS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_ZZAIKOS)        '�O�����݌ɐ�
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_TSEISANS & startTotalRow & ":" & XLSALP_TSEISANS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_TSEISANS)     '�������Y�v�搔
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_TSEISANR & startTotalRow & ":" & XLSALP_TSEISANR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_TSEISANR)     '�������Y�v���
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_THANBAIS & startTotalRow & ":" & XLSALP_THANBAIS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_THANBAIS)     '�����̔��v�搔
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_THANBAIR & startTotalRow & ":" & XLSALP_THANBAIR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_THANBAIR)     '�����̔��v���
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_TZAIKOS & startTotalRow & ":" & XLSALP_TZAIKOS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_TZAIKOS)        '�������݌ɐ�
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_TZAIKOR & startTotalRow & ":" & XLSALP_TZAIKOR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_TZAIKOR)        '�������݌ɗ�
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_KURIKOSIS & startTotalRow & ":" & XLSALP_KURIKOSIS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_KURIKOSIS)  '�J�z��
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_LOTSU & startTotalRow & ":" & XLSALP_LOTSU & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_LOTSU)              '���b�g��
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_YSEISANS & startTotalRow & ":" & XLSALP_YSEISANS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YSEISANS)     '�������Y�v�搔
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_YSEISANR & startTotalRow & ":" & XLSALP_YSEISANR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YSEISANR)     '�������Y�v���
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_YHANBAIS & startTotalRow & ":" & XLSALP_YHANBAIS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YHANBAIS)     '�����̔��v�搔
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_YHANBAIR & startTotalRow & ":" & XLSALP_YHANBAIR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YHANBAIR)     '�����̔��v���
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_YZAIKOS & startTotalRow & ":" & XLSALP_YZAIKOS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YZAIKOS)        '�������݌ɐ�
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_YZAIKOR & startTotalRow & ":" & XLSALP_YZAIKOR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YZAIKOR)        '�������݌ɗ�
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_YTUKISU & startTotalRow & ":" & XLSALP_YTUKISU & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YTUKISU)        '����
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_YYHANBAIS & startTotalRow & ":" & XLSALP_YYHANBAIS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YYHANBAIS)  '���X���̔��v�搔
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_YYHANBAIR & startTotalRow & ":" & XLSALP_YYHANBAIR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YYHANBAIR)  '���X���̔��v���
                                        buf.Remove(0, buf.Length)
                                        buf.Append("�i �� �v" & ControlChars.Tab & ControlChars.Tab & ControlChars.Tab)
                                        buf.Append("=SUBTOTAL(9," & XLSALP_ZZZAIKOS & startTotalRow & ":" & XLSALP_ZZZAIKOS & CStr(xlsRow - 1) & ")" & ControlChars.Tab)     '�O�X�����݌ɐ�
                                        buf.Append("=SUBTOTAL(9," & XLSALP_ZSEISANS & startTotalRow & ":" & XLSALP_ZSEISANS & CStr(xlsRow - 1) & ")" & ControlChars.Tab)     '�O�����Y���ѐ�
                                        buf.Append("=SUBTOTAL(9," & XLSALP_ZHANBAIS & startTotalRow & ":" & XLSALP_ZHANBAIS & CStr(xlsRow - 1) & ")" & ControlChars.Tab)     '�O���̔����ѐ�
                                        buf.Append("=SUBTOTAL(9," & XLSALP_ZZAIKOS & startTotalRow & ":" & XLSALP_ZZAIKOS & CStr(xlsRow - 1) & ")" & ControlChars.Tab)        '�O�����݌ɐ�
                                        buf.Append("=SUBTOTAL(9," & XLSALP_TSEISANS & startTotalRow & ":" & XLSALP_TSEISANS & CStr(xlsRow - 1) & ")" & ControlChars.Tab)     '�������Y�v�搔
                                        buf.Append("=SUBTOTAL(9," & XLSALP_TSEISANR & startTotalRow & ":" & XLSALP_TSEISANR & CStr(xlsRow - 1) & ")" & ControlChars.Tab)     '�������Y�v���
                                        buf.Append("=SUBTOTAL(9," & XLSALP_THANBAIS & startTotalRow & ":" & XLSALP_THANBAIS & CStr(xlsRow - 1) & ")" & ControlChars.Tab)     '�����̔��v�搔
                                        buf.Append("=SUBTOTAL(9," & XLSALP_THANBAIR & startTotalRow & ":" & XLSALP_THANBAIR & CStr(xlsRow - 1) & ")" & ControlChars.Tab)     '�����̔��v���
                                        buf.Append("=SUBTOTAL(9," & XLSALP_TZAIKOS & startTotalRow & ":" & XLSALP_TZAIKOS & CStr(xlsRow - 1) & ")" & ControlChars.Tab)        '�������݌ɐ�
                                        buf.Append("=SUBTOTAL(9," & XLSALP_TZAIKOR & startTotalRow & ":" & XLSALP_TZAIKOR & CStr(xlsRow - 1) & ")" & ControlChars.Tab)        '�������݌ɗ�
                                        buf.Append("=SUBTOTAL(9," & XLSALP_KURIKOSIS & startTotalRow & ":" & XLSALP_KURIKOSIS & CStr(xlsRow - 1) & ")" & ControlChars.Tab)  '�J�z��
                                        buf.Append("=SUBTOTAL(9," & XLSALP_LOTSU & startTotalRow & ":" & XLSALP_LOTSU & CStr(xlsRow - 1) & ")" & ControlChars.Tab)              '���b�g��
                                        buf.Append("=SUBTOTAL(9," & XLSALP_YSEISANS & startTotalRow & ":" & XLSALP_YSEISANS & CStr(xlsRow - 1) & ")" & ControlChars.Tab)     '�������Y�v�搔
                                        buf.Append("=SUBTOTAL(9," & XLSALP_YSEISANR & startTotalRow & ":" & XLSALP_YSEISANR & CStr(xlsRow - 1) & ")" & ControlChars.Tab)     '�������Y�v���
                                        buf.Append("=SUBTOTAL(9," & XLSALP_YHANBAIS & startTotalRow & ":" & XLSALP_YHANBAIS & CStr(xlsRow - 1) & ")" & ControlChars.Tab)     '�����̔��v�搔
                                        buf.Append("=SUBTOTAL(9," & XLSALP_YHANBAIR & startTotalRow & ":" & XLSALP_YHANBAIR & CStr(xlsRow - 1) & ")" & ControlChars.Tab)     '�����̔��v���
                                        buf.Append("=SUBTOTAL(9," & XLSALP_YZAIKOS & startTotalRow & ":" & XLSALP_YZAIKOS & CStr(xlsRow - 1) & ")" & ControlChars.Tab)        '�������݌ɐ�
                                        buf.Append("=SUBTOTAL(9," & XLSALP_YZAIKOR & startTotalRow & ":" & XLSALP_YZAIKOR & CStr(xlsRow - 1) & ")" & ControlChars.Tab)        '�������݌ɗ�
                                        '-->2011.03.23 chg by takagi #�݌Ɍ����͓����݌Ɂ������̔��ŋ��߂�
                                        'buf.Append("=SUBTOTAL(9," & XLSALP_YTUKISU & startTotalRow & ":" & XLSALP_YTUKISU & CStr(xlsRow - 1) & ")" & ControlChars.Tab)        '����
                                        If existsMetsukeFlg Then
                                            '�ڕt���聨�d��(t)
                                            buf.Append("=" & XLSALP_YZAIKOR & CStr(xlsRow) & "/" & XLSALP_YYHANBAIR & CStr(xlsRow) & ControlChars.Tab)                         '����(t)
                                        Else
                                            '�ڕt�Ȃ����d��(Km)
                                            buf.Append("=" & XLSALP_YZAIKOS & CStr(xlsRow) & "/" & XLSALP_YYHANBAIS & CStr(xlsRow) & ControlChars.Tab)                         '����(Km)
                                        End If
                                        '<--2011.03.23 chg by takagi #�݌Ɍ����͓����݌Ɂ������̔��ŋ��߂�
                                        buf.Append("=SUBTOTAL(9," & XLSALP_YYHANBAIS & startTotalRow & ":" & XLSALP_YYHANBAIS & CStr(xlsRow - 1) & ")" & ControlChars.Tab)  '���X���̔��v�搔
                                        buf.Append("=SUBTOTAL(9," & XLSALP_YYHANBAIR & startTotalRow & ":" & XLSALP_YYHANBAIR & CStr(xlsRow - 1) & ")" & ControlChars.Tab)  '���X���̔��v���
                                        Clipboard.SetText(buf.ToString)
                                        eh.paste(xlsRow, XLSCOL_HINMEI)
                                        Clipboard.Clear()
                                        '<--2010.12.27 chg by takagi #������

                                        '�r�����Đݒ�
                                        lineV.Bottom = LineVO.LineType.BrokenL
                                        lineV.Top = LineVO.LineType.BrokenL
                                        eh.drawRuledLine(lineV, xlsRow, 1, , 29)

                                        startTotalRow = xlsRow + 1

                                        '���̕i��R�[�h��ێ�
                                        startHinsyu = _db.rmNullStr(ds.Tables(RS).Rows(i)(COLDT_HINSYUCD))

                                        '�o�͂���s�����̍s�Ɉڂ�
                                        totalRow = totalRow + 1
                                        xlsRow = startPrintRow + i + totalRow

                                        '�s��1�s�ǉ�
                                        eh.copyRow(xlsRow)
                                        eh.insertPasteRow(xlsRow)
                                        '�ꗗ�f�[�^�o��
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_SIYOCD)), xlsRow, XLSCOL_SIYOUCD)        '�i��R�[�h
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_HINMEI)), xlsRow, XLSCOL_HINMEI)         '�i�햼
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_LOTTYO)), xlsRow, XLSCOL_LOTTYO)         '�O�X�����݌ɗ�
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ABC)), xlsRow, XLSCOL_ABC)               '�O�����Y���ї�
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ZZZAIKOS)), xlsRow, XLSCOL_ZZZAIKOS)     '�O���̔����ї�
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ZSEISANS)), xlsRow, XLSCOL_ZSEISANS)     '�O�����݌ɗ�
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ZHANBAIS)), xlsRow, XLSCOL_ZHANBAIS)     '�������Y�v���
                                        'eh.setValue("=(" & XLSALP_ZZZAIKOS & CStr(xlsRow) & " + " & XLSALP_ZSEISANS & CStr(xlsRow) & " - " & XLSALP_ZHANBAIS & CStr(xlsRow) & ")", xlsRow, XLSCOL_ZZAIKOS)
                                        ''-->2010.12.27 add by takagi #50
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ZZAIKOR)), xlsRow, XLSCOL_ZZAIKOR)       '�O�����݌ɗ�
                                        ''<--2010.12.27 add by takagi #50
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_TSEISANS)), xlsRow, XLSCOL_TSEISANS)     '�����̔��v���
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_TSEISANR)), xlsRow, XLSCOL_TSEISANR)     '�������݌ɗ�
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_THANBAIS)), xlsRow, XLSCOL_THANBAIS)     '�J�z��
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_THANBAIR)), xlsRow, XLSCOL_THANBAIR)     '���b�g��
                                        'eh.setValue("=(" & XLSALP_ZZAIKOS & CStr(xlsRow) & " + " & XLSALP_TSEISANS & CStr(xlsRow) & " - " & XLSALP_THANBAIS & CStr(xlsRow) & ")", xlsRow, XLSCOL_TZAIKOS)
                                        'eh.setValue("=(" & XLSALP_ZZAIKOR & CStr(xlsRow) & " + " & XLSALP_TSEISANR & CStr(xlsRow) & " - " & XLSALP_THANBAIR & CStr(xlsRow) & ")", xlsRow, XLSCOL_TZAIKOR)   '�������݌ɗ�
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_KURIKOSIS)), xlsRow, XLSCOL_KURIKOSIS)   '�������Y��
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_LOTSU)), xlsRow, XLSCOL_LOTSU)           '�����̔���
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YSEISANS)), xlsRow, XLSCOL_YSEISANS)     '�����݌ɗ�
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YSEISANR)), xlsRow, XLSCOL_YSEISANR)     '���X���̔���
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YHANBAIS)), xlsRow, XLSCOL_YHANBAIS)     '�����̔��v�搔
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YHANBAIR)), xlsRow, XLSCOL_YHANBAIR)     '�����̔��v���
                                        'eh.setValue("=(" & XLSALP_TZAIKOS & CStr(xlsRow) & " + " & XLSALP_YSEISANS & CStr(xlsRow) & " - " & XLSALP_YHANBAIS & CStr(xlsRow) & ")", xlsRow, XLSCOL_YZAIKOS)   '�������݌ɐ�
                                        'eh.setValue("=(" & XLSALP_TZAIKOR & CStr(xlsRow) & " + " & XLSALP_YSEISANR & CStr(xlsRow) & " - " & XLSALP_YHANBAIR & CStr(xlsRow) & ")", xlsRow, XLSCOL_YZAIKOR)   '�������݌ɗ�
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ZAIKOTUKISU)), xlsRow, XLSCOL_YTUKISU)   '����
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YYHANBAIS)), xlsRow, XLSCOL_YYHANBAIS)   '���X���̔��v�搔
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YYHANBAIR)), xlsRow, XLSCOL_YYHANBAIR)   '���X���̔��v���
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_KTUKISU)), xlsRow, XLSCOL_KIJUNTUKISU)   '�����
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_FZAIKOS)), xlsRow, XLSCOL_FUKKYUYO)      '�����p�݌�
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_AZAIKOS)), xlsRow, XLSCOL_ANZEN)         '���S�݌�
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_METUKE)), xlsRow, XLSCOL_METUKE)         '�ڕt
                                        prmPb.value = i + 1
                                        buf.Remove(0, buf.Length)
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_SIYOCD)) & ControlChars.Tab)       '�i��R�[�h
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_HINMEI)) & ControlChars.Tab)       '�i�햼
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_LOTTYO)) & ControlChars.Tab)       '�O�X�����݌ɗ�
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_ABC)) & ControlChars.Tab)          '�O�����Y���ї�
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_ZZZAIKOS)) & ControlChars.Tab)     '�O���̔����ї�
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_ZSEISANS)) & ControlChars.Tab)     '�O�����݌ɗ�
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_ZHANBAIS)) & ControlChars.Tab)     '�������Y�v���
                                        buf.Append("=(" & XLSALP_ZZZAIKOS & CStr(xlsRow) & " + " & XLSALP_ZSEISANS & CStr(xlsRow) & " - " & XLSALP_ZHANBAIS & CStr(xlsRow) & ")" & ControlChars.Tab)
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_TSEISANS)) & ControlChars.Tab)     '�����̔��v���
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_TSEISANR)) & ControlChars.Tab)     '�������݌ɗ�
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_THANBAIS)) & ControlChars.Tab)     '�J�z��
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_THANBAIR)) & ControlChars.Tab)     '���b�g��
                                        buf.Append("=(" & XLSALP_ZZAIKOS & CStr(xlsRow) & " + " & XLSALP_TSEISANS & CStr(xlsRow) & " - " & XLSALP_THANBAIS & CStr(xlsRow) & ")" & ControlChars.Tab)
                                        buf.Append("=(" & XLSALP_ZZAIKOR & CStr(xlsRow) & " + " & XLSALP_TSEISANR & CStr(xlsRow) & " - " & XLSALP_THANBAIR & CStr(xlsRow) & ")" & ControlChars.Tab)   '�������݌ɗ�
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_KURIKOSIS)) & ControlChars.Tab)    '�������Y��
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_LOTSU)) & ControlChars.Tab)        '�����̔���
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_YSEISANS)) & ControlChars.Tab)     '�����݌ɗ�
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_YSEISANR)) & ControlChars.Tab)     '���X���̔���
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_YHANBAIS)) & ControlChars.Tab)     '�����̔��v�搔
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_YHANBAIR)) & ControlChars.Tab)     '�����̔��v���
                                        buf.Append("=(" & XLSALP_TZAIKOS & CStr(xlsRow) & " + " & XLSALP_YSEISANS & CStr(xlsRow) & " - " & XLSALP_YHANBAIS & CStr(xlsRow) & ")" & ControlChars.Tab)   '�������݌ɐ�
                                        buf.Append("=(" & XLSALP_TZAIKOR & CStr(xlsRow) & " + " & XLSALP_YSEISANR & CStr(xlsRow) & " - " & XLSALP_YHANBAIR & CStr(xlsRow) & ")" & ControlChars.Tab)   '�������݌ɗ�
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_ZAIKOTUKISU)) & ControlChars.Tab)  '����
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_YYHANBAIS)) & ControlChars.Tab)    '���X���̔��v�搔
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_YYHANBAIR)) & ControlChars.Tab)    '���X���̔��v���
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_KTUKISU)) & ControlChars.Tab)      '�����
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_FZAIKOS)) & ControlChars.Tab)      '�����p�݌�
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_AZAIKOS)) & ControlChars.Tab)      '���S�݌�
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_METUKE)) & ControlChars.Tab)       '�ڕt
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_ZZAIKOR)) & ControlChars.Tab)      '�O�����݌ɗ�
                                        Clipboard.SetText(buf.ToString)
                                        eh.paste(xlsRow, XLSCOL_SIYOUCD)
                                        Clipboard.Clear()
                                        '<--2010.12.27 chg by takagi #������

                                        '-->2011.03.23 add by takagi #�݌Ɍ����͓����݌Ɂ������̔��ŋ��߂�
                                        '�ڕt�����݂���΃t���O�𗧂Ă�
                                        If "".Equals(_db.rmNullStr(.Rows(i)(COLDT_METUKE))) Or _
                                           _db.rmNullDouble(.Rows(i)(COLDT_METUKE)) = 0 Then
                                            existsMetsukeFlg = False
                                        Else
                                            existsMetsukeFlg = True
                                        End If
                                        '<--2011.03.23 add by takagi #�݌Ɍ����͓����݌Ɂ������̔��ŋ��߂�

                                    End If
                                    'End With               '2010.12.27 del by takagi #������
                                Next
                            End With                        '2010.12.27 add by takagi #������

                            '�o�͂���s�����̍s�Ɉڂ�
                            xlsRow = xlsRow + 1

                            '���1�s�ǉ�
                            eh.copyRow(xlsRow)
                            eh.insertPasteRow(xlsRow)

                            '�i��v�s�o��
                            eh.setValue("�i �� �v", xlsRow, XLSCOL_HINMEI)
                            eh.setValue("=SUBTOTAL(9," & XLSALP_ZZZAIKOS & startTotalRow & ":" & XLSALP_ZZZAIKOS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_ZZZAIKOS)     '�O�X�����݌ɐ�
                            eh.setValue("=SUBTOTAL(9," & XLSALP_ZSEISANS & startTotalRow & ":" & XLSALP_ZSEISANS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_ZSEISANS)     '�O�����Y���ѐ�
                            eh.setValue("=SUBTOTAL(9," & XLSALP_ZHANBAIS & startTotalRow & ":" & XLSALP_ZHANBAIS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_ZHANBAIS)     '�O���̔����ѐ�
                            eh.setValue("=SUBTOTAL(9," & XLSALP_ZZAIKOS & startTotalRow & ":" & XLSALP_ZZAIKOS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_ZZAIKOS)        '�O�����݌ɐ�
                            eh.setValue("=SUBTOTAL(9," & XLSALP_TSEISANS & startTotalRow & ":" & XLSALP_TSEISANS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_TSEISANS)     '�������Y�v�搔
                            eh.setValue("=SUBTOTAL(9," & XLSALP_TSEISANR & startTotalRow & ":" & XLSALP_TSEISANR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_TSEISANR)     '�������Y�v���
                            eh.setValue("=SUBTOTAL(9," & XLSALP_THANBAIS & startTotalRow & ":" & XLSALP_THANBAIS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_THANBAIS)     '�����̔��v�搔
                            eh.setValue("=SUBTOTAL(9," & XLSALP_THANBAIR & startTotalRow & ":" & XLSALP_THANBAIR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_THANBAIR)     '�����̔��v���
                            eh.setValue("=SUBTOTAL(9," & XLSALP_TZAIKOS & startTotalRow & ":" & XLSALP_TZAIKOS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_TZAIKOS)        '�������݌ɐ�
                            eh.setValue("=SUBTOTAL(9," & XLSALP_TZAIKOR & startTotalRow & ":" & XLSALP_TZAIKOR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_TZAIKOR)        '�������݌ɗ�
                            eh.setValue("=SUBTOTAL(9," & XLSALP_KURIKOSIS & startTotalRow & ":" & XLSALP_KURIKOSIS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_KURIKOSIS)  '�J�z��
                            eh.setValue("=SUBTOTAL(9," & XLSALP_LOTSU & startTotalRow & ":" & XLSALP_LOTSU & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_LOTSU)              '���b�g��
                            eh.setValue("=SUBTOTAL(9," & XLSALP_YSEISANS & startTotalRow & ":" & XLSALP_YSEISANS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YSEISANS)     '�������Y�v�搔
                            eh.setValue("=SUBTOTAL(9," & XLSALP_YSEISANR & startTotalRow & ":" & XLSALP_YSEISANR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YSEISANR)     '�������Y�v���
                            eh.setValue("=SUBTOTAL(9," & XLSALP_YHANBAIS & startTotalRow & ":" & XLSALP_YHANBAIS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YHANBAIS)     '�����̔��v�搔
                            eh.setValue("=SUBTOTAL(9," & XLSALP_YHANBAIR & startTotalRow & ":" & XLSALP_YHANBAIR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YHANBAIR)     '�����̔��v���
                            eh.setValue("=SUBTOTAL(9," & XLSALP_YZAIKOS & startTotalRow & ":" & XLSALP_YZAIKOS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YZAIKOS)        '�������݌ɐ�
                            eh.setValue("=SUBTOTAL(9," & XLSALP_YZAIKOR & startTotalRow & ":" & XLSALP_YZAIKOR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YZAIKOR)        '�������݌ɗ�
                            '-->2011.03.23 chg by takagi #�݌Ɍ����͓����݌Ɂ������̔��ŋ��߂�
                            'eh.setValue("=SUBTOTAL(9," & XLSALP_YTUKISU & startTotalRow & ":" & XLSALP_YTUKISU & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YTUKISU)        '����
                            If existsMetsukeFlg Then
                                '�ڕt���聨�d��(t)
                                eh.setValue("=" & XLSALP_YZAIKOR & CStr(xlsRow) & "/" & XLSALP_YYHANBAIR & CStr(xlsRow), xlsRow, XLSCOL_YTUKISU)                         '����(t)
                            Else
                                '�ڕt�Ȃ����d��(Km)
                                eh.setValue("=" & XLSALP_YZAIKOS & CStr(xlsRow) & "/" & XLSALP_YYHANBAIS & CStr(xlsRow), xlsRow, XLSCOL_YTUKISU)                         '����(Km)
                            End If
                            '<--2011.03.23 chg by takagi #�݌Ɍ����͓����݌Ɂ������̔��ŋ��߂�
                            eh.setValue("=SUBTOTAL(9," & XLSALP_YYHANBAIS & startTotalRow & ":" & XLSALP_YYHANBAIS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YYHANBAIS)  '���X���̔��v�搔
                            eh.setValue("=SUBTOTAL(9," & XLSALP_YYHANBAIR & startTotalRow & ":" & XLSALP_YYHANBAIR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YYHANBAIR)  '���X���̔��v���

                            '�r�����Đݒ�
                            lineV.Bottom = LineVO.LineType.BrokenL
                            lineV.Top = LineVO.LineType.BrokenL
                            eh.drawRuledLine(lineV, xlsRow, 1, , 29)

                            '�ŏI���v�s�o��
                            xlsRow = xlsRow + 1
                            eh.deleteRow(xlsRow)
                            '-->2011.01.15 del by takagi
                            'eh.setValue("MAX", xlsRow, XLSCOL_SIYOUCD)
                            '<--2011.01.15 del by takagi
                            eh.setValue("�� �� �v", xlsRow, XLSCOL_HINMEI)
                            eh.setValue("=SUBTOTAL(9," & XLSALP_ZZZAIKOS & startPrintRow & ":" & XLSALP_ZZZAIKOS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_ZZZAIKOS)     '�O�X�����݌ɐ�
                            eh.setValue("-", xlsRow, XLSCOL_LOTTYO)
                            eh.setValue("-", xlsRow, XLSCOL_ZZZAIKOS)
                            eh.setValue("-", xlsRow, XLSCOL_ZSEISANS)
                            eh.setValue("-", xlsRow, XLSCOL_ZHANBAIS)
                            eh.setValue("-", xlsRow, XLSCOL_ZZAIKOS)
                            eh.setValue("-", xlsRow, XLSCOL_TSEISANS)
                            eh.setValue("=SUBTOTAL(9," & XLSALP_TSEISANR & startPrintRow & ":" & XLSALP_TSEISANR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_TSEISANR)     '�������Y�v���
                            eh.setValue("-", xlsRow, XLSCOL_THANBAIS)
                            eh.setValue("=SUBTOTAL(9," & XLSALP_THANBAIR & startPrintRow & ":" & XLSALP_THANBAIR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_THANBAIR)     '�����̔��v���
                            eh.setValue("-", xlsRow, XLSCOL_TZAIKOS)
                            eh.setValue("=SUBTOTAL(9," & XLSALP_TZAIKOR & startPrintRow & ":" & XLSALP_TZAIKOR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_TZAIKOR)        '�������݌ɗ�
                            eh.setValue("-", xlsRow, XLSCOL_KURIKOSIS)
                            eh.setValue("-", xlsRow, XLSCOL_LOTSU)
                            eh.setValue("-", xlsRow, XLSCOL_YSEISANS)
                            eh.setValue("=SUBTOTAL(9," & XLSALP_YSEISANR & startPrintRow & ":" & XLSALP_YSEISANR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YSEISANR)     '�������Y�v���
                            eh.setValue("-", xlsRow, XLSCOL_YHANBAIS)
                            eh.setValue("=SUBTOTAL(9," & XLSALP_YHANBAIR & startPrintRow & ":" & XLSALP_YHANBAIR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YHANBAIR)     '�����̔��v���
                            eh.setValue("-", xlsRow, XLSCOL_YZAIKOS)
                            eh.setValue("=SUBTOTAL(9," & XLSALP_YZAIKOR & startPrintRow & ":" & XLSALP_YZAIKOR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YZAIKOR)        '�������݌ɗ�
                            '-->2011.03.23 add by takagi #�݌Ɍ����͓����݌Ɂ������̔��ŋ��߂�
                            'eh.setValue("=SUBTOTAL(9," & XLSALP_YTUKISU & startPrintRow & ":" & XLSALP_YTUKISU & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YTUKISU)        '����
                            eh.setValue("-", xlsRow, XLSCOL_YTUKISU)        '����
                            '<--2011.03.23 add by takagi #�݌Ɍ����͓����݌Ɂ������̔��ŋ��߂�
                            eh.setValue("-", xlsRow, XLSCOL_YYHANBAIS)
                            eh.setValue("=SUBTOTAL(9," & XLSALP_YYHANBAIR & startPrintRow & ":" & XLSALP_YYHANBAIR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YYHANBAIR)  '���X���̔��v���
                            eh.setValue("-", xlsRow, XLSCOL_KIJUNTUKISU)
                            eh.setValue("-", xlsRow, XLSCOL_FUKKYUYO)
                            eh.setValue("-", xlsRow, XLSCOL_ANZEN)

                            '�r�����Đݒ�
                            lineV.Bottom = LineVO.LineType.NomalL
                            lineV.Top = LineVO.LineType.NomalL
                            eh.drawRuledLine(lineV, xlsRow, 1, , 29)


                            '�쐬�����ҏW
                            Dim printDate As String = Now.ToString("yyyy/MM/dd HH:mm")
                            '' 2010/12/22 upd start sugano
                            'eh.setValue("�쐬���� �F " & printDate, 1, 29)   'AC1
                            eh.setValue("�쐬���� �F " & printDate, 1, 28)   'AB1
                            '' 2010/12/22 upd end sugano

                            '���v�i���ҏW
                            eh.setValue("(" & _db.rmNullStr(dsHanyo.Tables(RS).Rows(hanyoCnt)(HANYOU_NAME1)) & ")", 2, 6)      'F2

                            '�����N���A�v��N���ҏW
                            eh.setValue("�����N���F" & lblSyori.Text & "�@�@�v��N���F" & lblKeikaku.Text, 2, 10)    'J2

                            '��������
                            eh.setValue("���v��F" & _serchJuyo, 4, 1)        'A4

                            '�w�b�_�[�̔N���ҏW
                            eh.setValue(lblZengetu.Text, 7, 7)      'G7�@�O��
                            eh.setValue(lblTogetu.Text, 7, 11)      'K7�@����
                            eh.setValue(lblYokugetu.Text, 7, 19)    'S7�@����
                            eh.setValue(lblYygetu.Text, 7, 24)      'X7�@���X��
                            '�O�X���̌����v�Z
                            '-->2011.01.15 chg by takagi #69
                            'Dim zzgetu As Integer = CInt(lblSyori.Text.Substring(5, 2)) - 2
                            'If zzgetu = 0 Then
                            '    zzgetu = 12
                            'End If
                            Dim zzgetu As Integer = Format(DateAdd(DateInterval.Month, -2, CDate(lblSyori.Text & "/01")), "MM")
                            '<--2011.01.15 chg by takagi #69
                            eh.setValue(zzgetu & "��", 7, 5)        'E7�@�O�X��

                            '����̃Z���Ƀt�H�[�J�X���Ă�
                            eh.selectCell(START_PRINT, XLSCOL_SIYOUCD)  'A10

                            Clipboard.Clear()       '�N���b�v�{�[�h�̏�����
                        End If
                    Next
                    eh.deleteSheet("Ver1.0.00")

                    '-->2010.12.27 add by takagi 
                    '�擪�V�[�g��I��
                    eh.targetSheetByIdx = 1
                    eh.selectSheet(eh.targetSheet)
                    eh.selectCell(1, 1)
                    '<--2010.12.27 add by takagi

                Finally
                    eh.close()
                End Try

                'EXCEL�t�@�C���J��
                eh.display()

                '-->2010.12.27 add by takagi #������
            Catch pbe As UtilProgressBarCancelEx
                Throw pbe '��ʓ]��
                '<--2010.12.27 add by takagi #������
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

            '-->2010.12.27 add by takagi #������
        Catch pbe As UtilProgressBarCancelEx
            Throw pbe '��ʓ]��
            '<--2010.12.27 add by takagi #������
        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

#End Region

#Region "���[�U��`�֐�:��ʐ���"

    '-------------------------------------------------------------------------------
    '�@��ʋN����
    '-------------------------------------------------------------------------------
    Private Sub initForm()
        Try

            '�o�^�{�^���E�Čv�Z�{�^���EEXCEL�o�̓{�^���E�I�v�V�����{�^���g�p�s��
            btnTouroku.Enabled = False
            btnSaikeisan.Enabled = False
            btnSeisanPrint.Enabled = False
            btnHinsyuPrint.Enabled = False
            optTanni.Enabled = False

            '�[��ID�̎擾
            _tanmatuID = UtilClass.getComputerName

            '�}�E�X�J�[�\�������v
            Me.Cursor = Cursors.WaitCursor

            '�����N���A�v��N���\��
            Call dispDate()

            '���v��R���{�{�b�N�X�̃Z�b�g
            Call setCbo()

            '�g�����U�N�V�����J�n
            _db.beginTran()

            '�ꗗ�f�[�^�쐬
            Call delInsWK10()
            Call createWK10()

            '�g�����U�N�V�����I��
            _db.commitTran()

            '�ꗗ�\��
            Call dispWK10()

            '�ꗗ�s���F�t���O��L���ɂ���
            _colorCtlFlg = True

            '�w�i�F�̐ݒ�
            Call setBackcolor(0, 0)

            '�Čv�Z�ς݂ɂ���
            _saikeisanFlg = True

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        Finally
            If _db.isTransactionOpen = True Then
                _db.rollbackTran()                          '���[���o�b�N
            End If
            '�}�E�X�J�[�\�����
            Me.Cursor = Cursors.Arrow
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@���בւ����x��������
    '�@(�����T�v)�ꗗ����ёւ���
    '-------------------------------------------------------------------------------
    Private Sub lblJuyoSort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblJuyoSort.Click, _
                                                                                                        lblHinCDSort.Click, _
                                                                                                        lblHinmeiSort.Click
        Try
            '�}�E�X�J�[�\�������v
            Me.Cursor = Cursors.WaitCursor

            '�ꗗ�s���F�t���O�𖳌��ɂ���
            _colorCtlFlg = False

            '�ꗗ�̃f�[�^�����[�N�e�[�u���ɍX�V
            Call updateWK10()

            '�v�搔�E�v��ʂ̍Čv�Z
            Call culcUpdateWK10()

            '�Čv�Z�ς݂ɂ���
            _saikeisanFlg = True

            '�ꗗ�w�b�_�[���x���ҏW
            If sender.Equals(lblJuyoSort) Then
                '���v��
                If (LBL_JUYO & N & LBL_SYOJUN).Equals(lblJuyoSort.Text) Then
                    '���ɏ����ŕ��בւ����Ă���ꍇ
                    dispWK10("JUYOSORT DESC")
                    lblJuyoSort.Text = LBL_JUYO & N & LBL_KOJUN
                Else
                    '���񉟉����܂��͊��ɍ~���ŕ��בւ����Ă���ꍇ
                    dispWK10("JUYOSORT")
                    lblJuyoSort.Text = LBL_JUYO & N & LBL_SYOJUN
                End If
                '�i���R�[�h�E�i���̃��x�������ɖ߂�
                lblHinCDSort.Text = LBL_HINMEICD
                lblHinmeiSort.Text = LBL_HINMEI

            ElseIf sender.Equals(lblHinCDSort) Then
                '�i���R�[�h
                If (LBL_HINMEICD & N & LBL_SYOJUN).Equals(lblHinCDSort.Text) Then
                    '���ɏ����ŕ��בւ����Ă���ꍇ
                    dispWK10("KHINMEICD DESC")
                    lblHinCDSort.Text = LBL_HINMEICD & N & LBL_KOJUN
                Else
                    '���񉟉����܂��͊��ɍ~���ŕ��בւ����Ă���ꍇ
                    dispWK10("KHINMEICD")
                    lblHinCDSort.Text = LBL_HINMEICD & N & LBL_SYOJUN
                End If
                '���v��E�i���̃��x�������ɖ߂�
                lblJuyoSort.Text = LBL_JUYO
                lblHinmeiSort.Text = LBL_HINMEI

            ElseIf sender.Equals(lblHinmeiSort) Then
                '�i��
                If (LBL_HINMEI & N & LBL_SYOJUN).Equals(lblHinmeiSort.Text) Then
                    '���ɏ����ŕ��בւ����Ă���ꍇ
                    dispWK10("HINMEI DESC")
                    lblHinmeiSort.Text = LBL_HINMEI & N & LBL_KOJUN
                Else
                    '���񉟉����܂��͊��ɍ~���ŕ��בւ����Ă���ꍇ
                    dispWK10("HINMEI")
                    lblHinmeiSort.Text = LBL_HINMEI & N & LBL_SYOJUN
                End If
                '���v��E�i��R�[�h�̃��x�������ɖ߂�
                lblJuyoSort.Text = LBL_JUYO
                lblHinCDSort.Text = LBL_HINMEICD

            Else
                Exit Sub
            End If

            '�w�i�F�̐ݒ�
            Call setBackcolor(0, 0)

            '�ꗗ�s���F�t���O��L���ɂ���
            _colorCtlFlg = True

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        Finally
            '�}�E�X�J�[�\�����ɖ߂�
            Me.Cursor = Cursors.Arrow
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�I�v�V�����{�^��������̕\����̐؂�ւ�
    '�@(�����T�v)�����km��̕\����؂�ւ���
    '-------------------------------------------------------------------------------
    Private Sub changeDisp()
        Try

            Dim kazuFlg As Boolean = False
            Dim ryouFlg As Boolean = False
            Dim tanni As String = ""

            If rdoKm.Checked Then   'km�\��
                kazuFlg = True
                ryouFlg = False
                tanni = LBL_KM
                lblKurikosi.Text = LBL_KURIKOSIS & LBL_KM
            Else                    't�\��
                kazuFlg = False
                ryouFlg = True
                tanni = LBL_TON
                lblKurikosi.Text = LBL_KURIKOSIR & LBL_TON
            End If

            '�e��̕\���ݒ�
            dgvSeisanSyuusei.Columns(COLNO_ZZAIKOS).Visible = kazuFlg           '�O�����݌ɐ�		
            dgvSeisanSyuusei.Columns(COLNO_ZZAIKOR).Visible = ryouFlg           '�O�����݌ɗ�		
            dgvSeisanSyuusei.Columns(COLNO_TSEISANS).Visible = kazuFlg          '�������Y�v�搔		
            dgvSeisanSyuusei.Columns(COLNO_TSEISANR).Visible = ryouFlg          '�������Y�v���		
            dgvSeisanSyuusei.Columns(COLNO_THANBAIS).Visible = kazuFlg          '�����̔��v�搔		
            dgvSeisanSyuusei.Columns(COLNO_THANBAIR).Visible = ryouFlg          '�����̔��v���		
            dgvSeisanSyuusei.Columns(COLNO_TZAIKOS).Visible = kazuFlg           '�������݌ɐ�		
            dgvSeisanSyuusei.Columns(COLNO_TZAIKOR).Visible = ryouFlg           '�������݌ɗ�		
            dgvSeisanSyuusei.Columns(COLNO_KURIKOSIS).Visible = kazuFlg         '�J�z��
            dgvSeisanSyuusei.Columns(COLNO_KURIKOSIR).Visible = ryouFlg         '�J�z��
            dgvSeisanSyuusei.Columns(COLNO_YSEISANS).Visible = kazuFlg          '�������Y�v�搔		
            dgvSeisanSyuusei.Columns(COLNO_YSEISANR).Visible = ryouFlg          '�������Y�v���		
            dgvSeisanSyuusei.Columns(COLNO_YHANBAIS).Visible = kazuFlg          '�����̔��v�搔		
            dgvSeisanSyuusei.Columns(COLNO_YHANBAIR).Visible = ryouFlg          '�����̔��v���		
            dgvSeisanSyuusei.Columns(COLNO_YZAIKOS).Visible = kazuFlg           '�������݌ɐ�		
            dgvSeisanSyuusei.Columns(COLNO_YZAIKOR).Visible = ryouFlg           '�������݌ɗ�		
            dgvSeisanSyuusei.Columns(COLNO_YYHANBAIS).Visible = kazuFlg         '���X���̔��v�搔	
            dgvSeisanSyuusei.Columns(COLNO_YYHANBAIR).Visible = ryouFlg         '���X���̔��v���	
            dgvSeisanSyuusei.Columns(COLNO_FUKKYUS).Visible = kazuFlg           '�����p�݌ɐ�		
            dgvSeisanSyuusei.Columns(COLNO_FUKKYUR).Visible = ryouFlg           '�����p�݌ɗ�		
            dgvSeisanSyuusei.Columns(COLNO_AZAIKOS).Visible = kazuFlg           '���S�݌ɐ�			
            dgvSeisanSyuusei.Columns(COLNO_AZAIKOR).Visible = ryouFlg           '���S�݌ɗ�	

            '���o�����x���̕\���ݒ�
            lblZZaiko.Text = LBL_ZAIKO & N & tanni          '�O�����݌�
            lblTSeisan.Text = LBL_SEISAN & N & tanni        '�������Y�v��
            lblTHanbai.Text = LBL_HANBAI & N & tanni        '�����̔��v��
            lblTZaiko.Text = LBL_ZAIKO & N & tanni          '�������݌�
            lblYSeisan.Text = LBL_SEISAN & N & tanni        '�������Y�v��
            lblYHanbai.Text = LBL_HANBAI & N & tanni        '�����̔��v��
            lblYZaiko.Text = LBL_ZAIKO & N & tanni          '�������݌�
            lblYYHanbai.Text = LBL_HANBAI & N & tanni       '���X���̔��v��
            lblFZaiko.Text = LBL_FUKKYU & N & tanni         '�����p�݌�
            lblAZaiko.Text = LBL_ANNZEN & N & tanni         '���S�݌�

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@��������SQL�쐬
    '�@(�����T�v)��ʂɓ��͂��ꂽ����������SQL���ɂ���
    '-------------------------------------------------------------------------------
    Private Function createSerchStr() As String
        Try
            createSerchStr = ""

            '���������̕ێ�
            _serchJuyo = cboJuyou.Text

            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(cboJuyou)
            If ch.getCode.Equals(CBONULLCODE) Then
                createSerchStr = ""
            Else
                createSerchStr = ch.getCode

            End If

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Function

    '-------------------------------------------------------------------------------
    '�@�R���g���[���L�[�����C�x���g
    '�@(�����T�v)�G���^�[�{�^���������Ɏ��̃R���g���[���Ɉڂ�
    '-------------------------------------------------------------------------------
    Private Sub txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboJuyou.KeyPress
        UtilClass.moveNextFocus(Me, e) '���̃R���g���[���ֈړ����� 

    End Sub

    '-------------------------------------------------------------------------------
    '�@���בւ����x���̕\��������
    '�@(�����T�v)�����E�o�^�{�^���������A���בւ����x���̕\��������������
    '-------------------------------------------------------------------------------
    Private Sub initLabel()

        lblJuyoSort.Text = LBL_JUYO
        lblHinCDSort.Text = LBL_HINMEICD
        lblHinmeiSort.Text = LBL_HINMEI

    End Sub

#End Region

#Region "���[�U��`�֐�:DGV�֘A"

    '-------------------------------------------------------------------------------
    '   �ꗗ�@�ҏW�`�F�b�N�iEditingControlShowing�C�x���g�j
    '   �i�����T�v�j���͂̐�����������
    '-------------------------------------------------------------------------------
    Private Sub dgvSeisanSyuusei_EditingControlShowing(ByVal sender As Object, _
                                              ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs _
                                              ) Handles dgvSeisanSyuusei.EditingControlShowing

        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanSyuusei)
            '�����b�g���̏ꍇ
            If dgvSeisanSyuusei.CurrentCell.ColumnIndex = COLNO_LOTSUU Then

                '���O���b�h�ɁA���l���̓��[�h�̐�����������
                _chkCellVO = gh.chkCell(sender, e, UtilDataGridViewHandler.chkType.Num)

            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '   �T�C�Y�ʈꗗ�@�I���Z�����؃C�x���g�iDataError�C�x���g�j
    '   �i�����T�v�j���l���͗��ɐ��l�ȊO�����͂��ꂽ�ꍇ�̃G���[����
    '-------------------------------------------------------------------------------
    Private Sub dgvSeisanSyuusei_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvSeisanSyuusei.DataError

        Try
            e.Cancel = False                                   '�ҏW���[�h�I��

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanSyuusei)
            '�����b�g���O���b�h�ɂ͐��l���̓��[�h(0�`9)�̐����������Ă���̂ŁA�����̉���
            If dgvSeisanSyuusei.CurrentCellAddress.X = COLNO_LOTSUU Then
                gh.AfterchkCell(_chkCellVO)
            End If

            '�������͂��ꂽ��Z������ɂ���
            gh.setCellData(COLDT_LOTSU, e.RowIndex, System.DBNull.Value)

            '�G���[���b�Z�[�W�\��
            Throw New UsrDefException("���p�����̂ݓ��͉\�ł��B", _msgHd.getMSG("onlyAcceptNumeric"))

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�ꗗ�f�[�^�ҏW�O
    '�@(�����T�v)�ꗗ�̃f�[�^���ύX�����O�̒l��ێ�����
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvSeisanSyuusei_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles dgvSeisanSyuusei.CellBeginEdit

        '���ɕύX�t���O�������Ă���ꍇ�͉����s��Ȃ�
        If _changeFlg = False Then
            _beforeChange = _db.rmNullDouble(dgvSeisanSyuusei(e.ColumnIndex, e.RowIndex).Value)
        End If
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�ꗗ�f�[�^�ҏW��
    '�@(�����T�v)�ꗗ�̃f�[�^���ύX���ꂽ�ꍇ�A�ύX�t���O�𗧂Ă�
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvSeisanSyuusei_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSeisanSyuusei.CellEndEdit
        Try

            '�Čv�Z���Ă��Ȃ���Ԃɂ���
            _saikeisanFlg = False

            If _changeFlg = False Then
                '�ҏW�O�ƒl���ς���Ă����ꍇ�A�t���O�𗧂Ă�
                If Not _beforeChange.Equals(_db.rmNullDouble(dgvSeisanSyuusei(e.ColumnIndex, e.RowIndex).Value)) Then
                    _changeFlg = True

                Else
                    Exit Sub
                End If
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�I���s�ɒ��F���鏈��
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvSeisanSyuusei_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvSeisanSyuusei.SelectionChanged
        If _colorCtlFlg Then
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanSyuusei)
            gh.setSelectionRowColor(dgvSeisanSyuusei.CurrentCellAddress.Y, _oldRowIndex, StartUp.lCOLOR_BLUE)
        End If
        _oldRowIndex = dgvSeisanSyuusei.CurrentCellAddress.Y
        Debug.Print("�񐔁@�F�@" & dgvSeisanSyuusei.CurrentCellAddress.X)
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�w�i�F�̐ݒ菈��
    '�@(�����T�v)�s�̔w�i�F��ɒ��F����B
    '�@�@I�@�F�@prmRowIndex     ���݃t�H�[�J�X������s��
    '�@�@I�@�F�@prmOldRowIndex  ���݂̍s�Ɉڂ�O�̍s��
    '------------------------------------------------------------------------------------------------------
    Private Sub setBackcolor(ByVal prmRowIndex As Integer, ByVal prmOldRowIndex As Integer)

        Dim _dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanSyuusei)

        '�w�肵���s�̔w�i�F��ɂ���
        _dgv.setSelectionRowColor(prmRowIndex, prmOldRowIndex, StartUp.lCOLOR_BLUE)

        _oldRowIndex = prmRowIndex

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�w���ւ̃t�H�[�J�X�ݒ菈��
    '�@(�����T�v)�w�肳�ꂽ�Z���Ƀt�H�[�J�X����B
    '�@�@I�@�F�@prmCoIndex      �t�H�[�J�X������Z���̗�
    '�@�@I�@�F�@prmRowIndex     �t�H�[�J�X������Z���̍s��
    '------------------------------------------------------------------------------------------------------
    Private Sub setForcusCol(ByVal prmColIndex As Integer, ByVal prmRowIndex As Integer)

        '�t�H�[�J�X�����Ă�
        dgvSeisanSyuusei.Focus()
        dgvSeisanSyuusei.CurrentCell = dgvSeisanSyuusei(prmColIndex, prmRowIndex)

        '�w�i�F�̐ݒ�
        Call setBackcolor(prmRowIndex, _oldRowIndex)

    End Sub

#End Region

#Region "���[�U��`�֐�:DB�֘A"

    '-------------------------------------------------------------------------------
    '�@���[�N�e�[�u���f�[�^�̍쐬
    '�@(�����T�v)���[�N�e�[�u���̃f�[�^���쐬(delete & insert)
    '�@�@I�@�F�@prmSql     ��������(��ʋN�����͉����󂯎��Ȃ�)
    '-------------------------------------------------------------------------------
    Private Sub delInsWK10(Optional ByVal prmSql As String = "")
        Try

            Dim sql As String = ""
            sql = " DELETE FROM ZG530E_W10 WHERE UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql)

            '�X�V�������擾
            Dim updateDate As Date = Now

            'T41���Y�v��
            sql = ""
            sql = sql & N & " INSERT INTO ZG530E_W10 ("
            sql = sql & N & "    JUYOUCD "          '���v��R�[�h
            sql = sql & N & "   ,KHINMEICD "        '�v��i���R�[�h
            sql = sql & N & "   ,HINMEI "           '�i��
            sql = sql & N & "   ,HINSYUNM"          '�i�햼
            sql = sql & N & "   ,SIYOUCD "          '�d�l�R�[�h
            sql = sql & N & "   ,HINSYUCD "         '�i��R�[�h
            sql = sql & N & "   ,SENSINCD "         '���S���R�[�h
            sql = sql & N & "   ,SIZECD "           '�T�C�Y�R�[�h
            sql = sql & N & "   ,COLORCD "          '�F�R�[�h
            sql = sql & N & "   ,LOT "              '�W�����b�g��
            sql = sql & N & "   ,ABCKBN "           'ABC�敪
            sql = sql & N & "   ,ZZZAIKOSU "        '�O�X�����݌ɐ�
            sql = sql & N & "   ,ZZZAIKORYOU "      '�O�X�����݌ɗ�
            sql = sql & N & "   ,ZSEISANSU "        '�O�����Y���ѐ�
            sql = sql & N & "   ,ZSEISANRYOU"       '�O�����Y���ї�
            sql = sql & N & "   ,ZHANBAISU "        '�O���̔����ѐ�
            sql = sql & N & "   ,ZHANBAIRYOU "      '�O���̔����ї�
            sql = sql & N & "   ,ZZAIKOSU "         '�O�����݌ɐ�
            sql = sql & N & "   ,ZZAIKORYOU "       '�O�����݌ɗ�
            sql = sql & N & "   ,TSEISANSU "        '�������Y�v�搔
            sql = sql & N & "   ,TSEISANRYOU "      '�������Y�v���
            sql = sql & N & "   ,THANBAISU "        '�����̔��v�搔
            sql = sql & N & "   ,THANBAIRYOU "      '�����̔��v���
            sql = sql & N & "   ,TZAIKOSU "         '�������݌ɐ�
            sql = sql & N & "   ,TZAIKORYOU "       '�������݌ɗ�
            sql = sql & N & "   ,KURIKOSISU "       '�J�z��
            sql = sql & N & "   ,KURIKOSIRYOU "     '�J�z��
            sql = sql & N & "   ,IKATULOTOSU "      '�ꊇ�Z�o���b�g��
            sql = sql & N & "   ,LOTOSU "           '���b�g��
            sql = sql & N & "   ,YSEISANSU "        '�������Y�v�搔
            sql = sql & N & "   ,YSEISANRYOU "      '�������Y�v���
            sql = sql & N & "   ,YHANBAISU "        '�����̔��v�搔
            sql = sql & N & "   ,YHANBAIRYOU "      '�����̔��v���
            sql = sql & N & "   ,YZAIKOSU "         '�������݌ɐ�
            sql = sql & N & "   ,YZAIKORYOU "       '�������݌ɗ�
            sql = sql & N & "   ,YZAIKOTUKISU "     '�����݌Ɍ���
            sql = sql & N & "   ,YYHANBAISU "       '���X���̔��v�搔
            sql = sql & N & "   ,YYHANBAIRYOU "     '���X���̔��v���
            sql = sql & N & "   ,KTUKISU "          '�����
            sql = sql & N & "   ,FZAIKOSU "         '�����p�݌ɐ�
            sql = sql & N & "   ,FZAIKORYOU "       '�����p�݌ɗ�
            sql = sql & N & "   ,AZAIKOSU "         '���S�݌ɐ�
            sql = sql & N & "   ,AZAIKORYOU "       '���S�݌ɗ�
            sql = sql & N & "   ,METSUKE "          '�ڕt
            sql = sql & N & "   ,UPDNAME "          '�[��ID
            sql = sql & N & "   ,UPDDATE) "         '�X�V����
            sql = sql & N & " SELECT "
            sql = sql & N & "    M.TT_JUYOUCD "     '���v��R�[�h
            sql = sql & N & "   ,M.TT_KHINMEICD "   '�v��i���R�[�h
            sql = sql & N & "   ,M.TT_HINMEI "      '�i��
            sql = sql & N & "   ,M.TT_HINSYUNM "    '�i�햼
            sql = sql & N & "   ,M.TT_H_SIYOU_CD "  '�d�l�R�[�h
            sql = sql & N & "   ,M.TT_H_HIN_CD "    '�i��R�[�h
            sql = sql & N & "   ,M.TT_H_SENSIN_CD " '���S���R�[�h
            sql = sql & N & "   ,M.TT_H_SIZE_CD "   '�T�C�Y�R�[�h
            sql = sql & N & "   ,M.TT_H_COLOR_CD "  '�F�R�[�h
            sql = sql & N & "   ,M.TT_LOT / 1000 "  '�W�����b�g��(���[�g���P�ʂɕϊ�)
            sql = sql & N & "   ,M.TT_ABCKBN "      'ABC�敪
            sql = sql & N & "   ,K.ZZZAIKOSU "      '�O�X�����݌ɐ�
            sql = sql & N & "   ,K.ZZZAIKORYOU "    '�O�X�����݌ɗ�
            sql = sql & N & "   ,K.ZSEISANSU "      '�O�����Y���ѐ�
            sql = sql & N & "   ,K.ZSEISANRYOU "    '�O�����Y���ї�
            sql = sql & N & "   ,K.ZHANBAISU "      '�O���̔����ѐ�
            sql = sql & N & "   ,K.ZHANBAIRYOU "    '�O���̔����ї�
            sql = sql & N & "   ,K.ZZAIKOSU "       '�O�����݌ɐ�
            sql = sql & N & "   ,K.ZZAIKORYOU "     '�O�����݌ɗ�
            sql = sql & N & "   ,K.TSEISANSU "      '�������Y�v�搔
            sql = sql & N & "   ,K.TSEISANRYOU "    '�������Y�v���
            sql = sql & N & "   ,K.THANBAISU "      '�����̔��v�搔
            sql = sql & N & "   ,K.THANBAIRYOU "    '�����̔��v���
            sql = sql & N & "   ,K.TZAIKOSU "       '�������݌ɐ�
            sql = sql & N & "   ,K.TZAIKORYOU "     '�������݌ɗ�
            sql = sql & N & "   ,K.KURIKOSISU "     '�J�z��
            sql = sql & N & "   ,K.KURIKOSIRYOU "   '�J�z��
            sql = sql & N & "   ,K.IKATULOTOSU "    '�ꊇ�Z�o���b�g��
            sql = sql & N & "   ,K.LOTOSU "         '���b�g��
            sql = sql & N & "   ,K.YSEISANSU "      '�������Y�v�搔
            sql = sql & N & "   ,K.YSEISANRYOU "    '�������Y�v���
            sql = sql & N & "   ,K.YHANBAISU "      '�����̔��v�搔
            sql = sql & N & "   ,K.YHANBAIRYOU "    '�����̔��v���
            sql = sql & N & "   ,K.YZAIKOSU "       '�������݌ɐ�
            sql = sql & N & "   ,K.YZAIKORYOU "     '�������݌ɗ�
            sql = sql & N & "   ,K.YZAIKOTUKISU  "  '�����݌Ɍ���
            sql = sql & N & "   ,K.YYHANBAISU "     '���X���̔��v�搔
            sql = sql & N & "   ,K.YYHANBAIRYOU "   '���X���̔��v���
            sql = sql & N & "   ,K.KTUKISU "        '�����
            sql = sql & N & "   ,K.FZAIKOSU "       '�����p�݌ɐ�
            sql = sql & N & "   ,K.FZAIKORYOU "     '�����p�݌ɗ�
            sql = sql & N & "   ,K.AZAIKOSU "       '���S�݌ɐ�
            sql = sql & N & "   ,K.AZAIKORYOU "     '���S�݌ɗ�
            sql = sql & N & "   ,K.METSUKE "        '�ڕt
            sql = sql & N & "   ,'" & _tanmatuID & "'"          '�[��ID
            sql = sql & N & "   ,TO_DATE('" & updateDate & "', 'YYYY/MM/DD HH24:MI:SS') "       '�X�V����
            sql = sql & N & "    FROM T41SEISANK K INNER JOIN "
            sql = sql & N & "       (SELECT * FROM M11KEIKAKUHIN "
            If Not "".Equals(prmSql) Then
                sql = sql & N & " WHERE TT_JUYOUCD = '" & prmSql & "'"
            End If
            sql = sql & N & "       ) M ON "
            sql = sql & N & "       K.KHINMEICD = M.TT_KHINMEICD "
            _db.executeDB(sql)

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@���[�N�e�[�u���쐬
    '�@(�����T�v)���[�N�e�[�u�����쐬(update)
    '------------------------------------------------------------------------------------------------------
    Private Sub createWK10()
        Try

            'M01�ėp�}�X�^
            Dim sql As String = ""
            sql = sql & N & " UPDATE ZG530E_W10 ZG SET ( "
            sql = sql & N & "    JUYOUNM "
            sql = sql & N & "   ,JUYOSORT) = ("
            sql = sql & N & "       SELECT  "
            sql = sql & N & "           NAME2, "
            sql = sql & N & "           SORT"
            sql = sql & N & "           FROM M01HANYO M "
            sql = sql & N & "           WHERE ZG.JUYOUCD = M.KAHENKEY "
            sql = sql & N & "           AND M.KOTEIKEY = '" & KOTEI_JUYOU & "') "
            sql = sql & N & "   WHERE (JUYOUCD) = (SELECT"
            sql = sql & N & "           KAHENKEY "
            sql = sql & N & "           FROM M01HANYO M "
            sql = sql & N & "           WHERE ZG.JUYOUCD = M.KAHENKEY "
            sql = sql & N & "           AND M.KOTEIKEY = '" & KOTEI_JUYOU & "'"
            sql = sql & N & "           AND ZG.UPDNAME = '" & _tanmatuID & "')"

            _db.executeDB(sql)

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@���[�N�e�[�u���f�[�^�̈ꗗ�\��
    '�@(�����T�v)���[�N�e�[�u���̃f�[�^���ꗗ�ɕ\������
    '�@�@I�@�F�@prmSort     �\�[�g��(��ʋN�����͉����󂯎��Ȃ�)
    '-------------------------------------------------------------------------------
    Private Sub dispWK10(Optional ByVal prmSort As String = "")
        Try

            Dim sql As String = ""
            '���[�N�̃f�[�^���ꗗ�ɕ\��
            sql = sql & N & " SELECT "
            sql = sql & N & "    JUYOUCD " & COLDT_JUYOUCD              '���v��
            sql = sql & N & "   ,JUYOUNM " & COLDT_JUYOUSAKI            '���v�於
            sql = sql & N & "   ,KHINMEICD " & COLDT_HINMEICD           '�i���R�[�h
            sql = sql & N & "   ,HINMEI " & COLDT_HINMEI                '�i��
            sql = sql & N & "   ,LOT " & COLDT_LOTTYO                   '���b�g��
            sql = sql & N & "   ,ABCKBN " & COLDT_ABC                   'ABC
            sql = sql & N & "   ,ZZAIKOSU " & COLDT_ZZAIKOS             '�O�����݌ɐ�
            sql = sql & N & "   ,ZZAIKORYOU " & COLDT_ZZAIKOR           '�O�����݌ɗ�
            sql = sql & N & "   ,TSEISANSU " & COLDT_TSEISANS           '�������Y�v�搔
            sql = sql & N & "   ,TSEISANRYOU " & COLDT_TSEISANR         '�������Y�v���
            sql = sql & N & "   ,THANBAISU " & COLDT_THANBAIS           '�����̔��v�搔
            sql = sql & N & "   ,THANBAIRYOU " & COLDT_THANBAIR         '�����̔��v���
            sql = sql & N & "   ,TZAIKOSU " & COLDT_TZAIKOS             '�������݌ɐ�
            sql = sql & N & "   ,TZAIKORYOU " & COLDT_TZAIKOR           '�������݌ɗ�
            sql = sql & N & "   ,KURIKOSISU " & COLDT_KURIKOSIS         '�J�z��
            sql = sql & N & "   ,KURIKOSIRYOU " & COLDT_KURIKOSIR       '�J�z��
            sql = sql & N & "   ,LOTOSU " & COLDT_LOTSU                 '���b�g��
            sql = sql & N & "   ,YSEISANSU " & COLDT_YSEISANS           '�������Y�v�搔
            sql = sql & N & "   ,YSEISANRYOU " & COLDT_YSEISANR         '�������Y�v���
            sql = sql & N & "   ,YHANBAISU " & COLDT_YHANBAIS           '�����̔��v�搔
            sql = sql & N & "   ,YHANBAIRYOU " & COLDT_YHANBAIR         '�����̔��v���
            sql = sql & N & "   ,YZAIKOSU " & COLDT_YZAIKOS             '�������݌ɐ�
            sql = sql & N & "   ,YZAIKORYOU " & COLDT_YZAIKOR           '�������݌ɗ�
            sql = sql & N & "   ,YZAIKOTUKISU " & COLDT_ZAIKOTUKISU     '�݌Ɍ���       
            sql = sql & N & "   ,YYHANBAISU " & COLDT_YYHANBAIS         '���X���̔��v�搔
            sql = sql & N & "   ,YYHANBAIRYOU " & COLDT_YYHANBAIR       '���X���̔��v���
            sql = sql & N & "   ,KTUKISU " & COLDT_KTUKISU              '�����
            sql = sql & N & "   ,FZAIKOSU " & COLDT_FZAIKOS             '�����p�݌ɐ�
            sql = sql & N & "   ,FZAIKORYOU " & COLDT_FZAIKOR           '�����p�݌ɗ�
            sql = sql & N & "   ,AZAIKOSU " & COLDT_AZAIKOS             '���S�݌ɐ�
            sql = sql & N & "   ,AZAIKORYOU " & COLDT_AZAIKOR           '���S�݌ɗ�
            sql = sql & N & "   ,METSUKE " & COLDT_METUKE               '�ڕt
            sql = sql & N & " FROM ZG530E_W10 "
            sql = sql & N & "   WHERE UPDNAME = '" & _tanmatuID & "'"
            sql = sql & N & " ORDER BY "
            If "".Equals(prmSort) Then
                '' 2010/12/17 upd start sugano
                'sql = sql & N & " JUYOSORT, HINSYUCD, SENSINCD, SIZECD, SIYOUCD, COLORCD "
                sql = sql & N & " HINSYUCD, SENSINCD, SIZECD, SIYOUCD, COLORCD "
                '' 2010/12/17 upd end sugano
            Else
                sql = sql & N & " " & prmSort
            End If

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                '�ꗗ�̃N���A
                Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanSyuusei)
                gh.clearRow()

                '�����̃N���A
                lblKensu.Text = "0��"

                '�o�^�E�Čv�Z�EEXCEL�o�̓{�^������ђP�ʃI�v�V�����{�^���g�p�s��
                btnTouroku.Enabled = False
                btnSaikeisan.Enabled = False
                btnSeisanPrint.Enabled = False
                btnHinsyuPrint.Enabled = False
                optTanni.Enabled = False

                Throw New UsrDefException("�Ώۃf�[�^������܂���B", _msgHd.getMSG("NonData"))
            Else
                '���o�f�[�^������ꍇ�A�o�^�{�^���EEXCEL�{�^����L���ɂ���
                btnTouroku.Enabled = _updFlg
                btnSaikeisan.Enabled = True
                btnSeisanPrint.Enabled = True
                btnHinsyuPrint.Enabled = True
                optTanni.Enabled = True
            End If

            '���o�f�[�^���ꗗ�ɕ\������
            dgvSeisanSyuusei.DataSource = ds
            dgvSeisanSyuusei.DataMember = RS

            '�ꗗ�̌�����\������
            lblKensu.Text = CStr(iRecCnt) & "��"

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@�����N���A�v��N���\��
    '�@(�����T�v)�����N���A�v��N����\������
    '-------------------------------------------------------------------------------
    Private Sub dispDate()
        Try
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & " SNENGETU " & "SYORI"          '�����N��
            sql = sql & N & " ,KNENGETU " & "KEIKAKU"       '�v��N��
            sql = sql & N & " FROM T01KEIKANRI "

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                lblSyori.Text = ""
                lblKeikaku.Text = ""
                lblTogetu.Text = ""
                lblYokugetu.Text = ""
                lblYygetu.Text = ""
                lblZengetu.Text = ""
            Else
                Dim syoriDate As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("SYORI"))
                Dim keikakuDate As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("KEIKAKU"))

                '�uYYYY/MM�v�`���ŕ\��
                lblSyori.Text = syoriDate.Substring(0, 4) & "/" & syoriDate.Substring(4, 2)
                lblKeikaku.Text = keikakuDate.Substring(0, 4) & "/" & keikakuDate.Substring(4, 2)

                '�ꗗ�w�b�_�[�\��
                If CInt(syoriDate.Substring(4, 2)) < 10 Then
                    lblTogetu.Text = syoriDate.Substring(5, 1) & MONTH
                Else
                    lblTogetu.Text = syoriDate.Substring(4, 2) & MONTH
                End If

                If CInt(keikakuDate.Substring(4, 2)) < 10 Then
                    lblYokugetu.Text = keikakuDate.Substring(5, 1) & MONTH
                Else
                    lblYokugetu.Text = keikakuDate.Substring(4, 2) & MONTH
                End If
                '���X���̓��t���쐬
                Dim yyhanbai As String = keikakuDate & "01"     '���t�ɕϊ����邽�߂ɓ���t������
                Dim yyDate As DateTime = Date.ParseExact(yyhanbai, "yyyyMMdd", Nothing)
                yyDate = yyDate.AddMonths(1)        '1��������

                If CInt(CStr(yyDate).Substring(5, 2)) < 10 Then
                    '����1���̏ꍇ
                    lblYygetu.Text = CStr(yyDate).Substring(6, 1) & MONTH
                Else
                    lblYygetu.Text = CStr(yyDate).Substring(5, 2) & MONTH
                End If

                '�O���̓��t���쐬
                Dim zDate As DateTime = yyDate.AddMonths(-3)
                'If CInt(CStr(zDate).Substring(5, 2)) Then
                If CInt(CStr(zDate).Substring(5, 2)) < 10 Then
                    '����1���̏ꍇ
                    lblZengetu.Text = CStr(zDate).Substring(6, 1) & MONTH
                Else
                    lblZengetu.Text = CStr(zDate).Substring(5, 2) & MONTH
                End If
            End If


        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@���v��R���{�{�b�N�X�̃Z�b�g
    '�@(�����T�v)M01�ėp�}�X�^������v�於�𒊏o���ĕ\������B
    '-------------------------------------------------------------------------------
    Private Sub setCbo()
        Try

            '�R���{�{�b�N�X
            Dim sql = ""
            sql = sql & N & " SELECT KAHENKEY KAHEN, "
            sql = sql & N & " NAME2 JUYOUSAKI "
            sql = sql & N & " FROM M01HANYO "
            sql = sql & N & " WHERE KOTEIKEY = '" & KOTEI_JUYOU & "' "
            sql = sql & N & " ORDER BY KAHENKEY "


            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                btnTouroku.Enabled = False
                btnSaikeisan.Enabled = False
                btnSeisanPrint.Enabled = False
                btnHinsyuPrint.Enabled = False
                optTanni.Enabled = False
                Throw New UsrDefException("�ėp�}�X�^�̒l�̎擾�Ɏ��s���܂����B", _msgHd.getMSG("noHanyouMst"))
            End If

            '�R���{�{�b�N�X�N���A
            Me.cboJuyou.Items.Clear()
            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(cboJuyou)

            '�擪�ɋ�s
            ch.addItem(New UtilCboVO(CBONULLCODE, ""))

            '���[�v�����ăR���{�{�b�N�X�ɃZ�b�g
            For i As Integer = 0 To iRecCnt - 1
                ch.addItem(New UtilCboVO(_db.rmNullStr(ds.Tables(RS).Rows(i)("KAHEN")), _db.rmNullStr(ds.Tables(RS).Rows(i)("JUYOUSAKI"))))
            Next

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@���[�N�e�[�u���f�[�^�̍X�V
    '�@(�����T�v)�ꗗ�ɕ\������Ă���f�[�^�����[�N�e�[�u���ɍX�V����
    '-------------------------------------------------------------------------------
    Private Sub updateWK10()
        Try

            Dim sql As String = ""
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanSyuusei)

            '�g�����U�N�V�����J�n
            _db.beginTran()

            '�s�����������[�v
            For i As Integer = 0 To gh.getMaxRow - 1
                sql = ""
                sql = sql & N & " UPDATE ZG530E_W10 SET "
                sql = sql & N & " LOTOSU = " & NS(_db.rmNullStr(dgvSeisanSyuusei(COLNO_LOTSUU, i).Value))     '���b�g��
                sql = sql & N & "   WHERE UPDNAME = '" & _tanmatuID & "'"
                sql = sql & N & "   AND KHINMEICD = '" & dgvSeisanSyuusei(COLNO_HINMEICD, i).Value & "'"
                _db.executeDB(sql)
            Next

            '�g�����U�N�V�����I��
            _db.commitTran()

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        Finally
            If _db.isTransactionOpen = True Then
                _db.rollbackTran()                          '���[���o�b�N
            End If
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

    '-------------------------------------------------------------------------------
    '�@�Čv�Z����
    '�@(�����T�v)���b�g�������Ƀ��[�N�e�[�u���̌v��l���Čv�Z����
    '-------------------------------------------------------------------------------
    Private Sub culcUpdateWK10()
        Try

            '�������Y�v�搔
            Dim sql As String = ""
            sql = sql & N & " UPDATE ZG530E_W10 SET "
            sql = sql & N & " YSEISANSU = LOTOSU * LOT + KURIKOSISU "
            sql = sql & N & " WHERE UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql)

            '�������݌ɐ�
            sql = ""
            sql = sql & N & " UPDATE ZG530E_W10 SET "
            sql = sql & N & " YZAIKOSU = TZAIKOSU + YSEISANSU - YHANBAISU "
            sql = sql & N & " WHERE UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql)

            '�����݌Ɍ���
            sql = ""
            sql = sql & N & " UPDATE ZG530E_W10 SET "
            sql = sql & N & " YZAIKOTUKISU = YZAIKOSU / YYHANBAISU "
            sql = sql & N & " WHERE UPDNAME = '" & _tanmatuID & "'"
            '-->2010.12.02 add by takagi 
            sql = sql & N & "  AND YYHANBAISU != 0"
            '<--2010.12.02 add by takagi 
            _db.executeDB(sql)

            '�������Y�v���
            sql = ""
            sql = sql & N & " UPDATE ZG530E_W10 SET "
            sql = sql & N & " YSEISANRYOU = YSEISANSU * METSUKE / 1000 "
            sql = sql & N & " WHERE UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql)

            '�������݌ɗ�
            sql = ""
            sql = sql & N & " UPDATE ZG530E_W10 SET "
            sql = sql & N & " YZAIKORYOU = YZAIKOSU * METSUKE / 1000 "
            sql = sql & N & " WHERE UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql)

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@DB�X�V
    '�@(�����T�v)���[�N�e�[�u���̒l��T41�ɓo�^����
    '-------------------------------------------------------------------------------
    Private Sub registT41()
        Try

            'T41�폜
            Dim delCnt As Integer = 0           '�폜���R�[�h��
            Dim sql As String = ""
            sql = " DELETE FROM T41SEISANK T41 "
            sql = sql & N & " WHERE EXISTS "
            sql = sql & N & "   (SELECT * FROM ZG530E_W10 WK "
            sql = sql & N & "       WHERE T41.KHINMEICD = WK.KHINMEICD "
            sql = sql & N & " AND UPDNAME = '" & _tanmatuID & "')"
            _db.executeDB(sql, delCnt)

            '�X�V�J�n�������擾
            Dim updStartDate As Date = Now

            '���[�N�e�[�u���̒l��T41�ɓo�^
            sql = ""
            sql = sql & N & " INSERT INTO T41SEISANK ( "
            sql = sql & N & "    KHINMEICD "            '�v��i���R�[�h
            sql = sql & N & "   ,ZZZAIKOSU "            '�O�X�����݌ɐ�
            sql = sql & N & "   ,ZZZAIKORYOU "          '�O�X�����݌ɗ�
            sql = sql & N & "   ,ZSEISANSU "            '�O�����Y���ѐ�
            sql = sql & N & "   ,ZSEISANRYOU "          '�O�����Y���ї�
            sql = sql & N & "   ,ZHANBAISU "            '�O���̔����ѐ�
            sql = sql & N & "   ,ZHANBAIRYOU "          '�O���̔����ї�
            sql = sql & N & "   ,ZZAIKOSU "             '�O�����݌ɐ�
            sql = sql & N & "   ,ZZAIKORYOU "           '�O�����݌ɗ�
            sql = sql & N & "   ,TSEISANSU "            '�������Y�v�搔
            sql = sql & N & "   ,TSEISANRYOU "          '�������Y�v���
            sql = sql & N & "   ,THANBAISU "            '�����̔��v�搔
            sql = sql & N & "   ,THANBAIRYOU "          '�����̔��v���
            sql = sql & N & "   ,TZAIKOSU "             '�������݌ɐ�
            sql = sql & N & "   ,TZAIKORYOU "           '�������݌ɗ�
            sql = sql & N & "   ,KURIKOSISU "           '�J�z��
            sql = sql & N & "   ,KURIKOSIRYOU "         '�J�z��
            sql = sql & N & "   ,IKATULOTOSU "          '�ꊇ�Z�o���b�g��
            sql = sql & N & "   ,LOTOSU "               '���b�g��
            sql = sql & N & "   ,YSEISANSU "            '�������Y�v�搔
            sql = sql & N & "   ,YSEISANRYOU "          '�������Y�v���
            sql = sql & N & "   ,YHANBAISU "            '�����̔��v�搔
            sql = sql & N & "   ,YHANBAIRYOU "          '�����̔��v���
            sql = sql & N & "   ,YZAIKOSU "             '�������݌ɐ�
            sql = sql & N & "   ,YZAIKORYOU "           '�������݌ɗ�
            sql = sql & N & "   ,YZAIKOTUKISU  "        '�����݌Ɍ���
            sql = sql & N & "   ,YYHANBAISU "           '���X���̔��v�搔
            sql = sql & N & "   ,YYHANBAIRYOU "         '���X���̔��v���
            sql = sql & N & "   ,KTUKISU "              '�����
            sql = sql & N & "   ,FZAIKOSU "             '�����p�݌ɐ�
            sql = sql & N & "   ,FZAIKORYOU "           '�����p�݌ɗ�
            sql = sql & N & "   ,AZAIKOSU "             '���S�݌ɐ�
            sql = sql & N & "   ,AZAIKORYOU "           '���S�݌ɗ�
            sql = sql & N & "   ,METSUKE "              '�ڕt
            sql = sql & N & "   ,UPDNAME "              '�[��ID
            sql = sql & N & "   ,UPDDATE )"             '�X�V����
            sql = sql & N & " SELECT "
            sql = sql & N & "    KHINMEICD "            '�v��i���R�[�h
            sql = sql & N & "   ,ZZZAIKOSU "            '�O�X�����݌ɐ�
            sql = sql & N & "   ,ZZZAIKORYOU "          '�O�X�����݌ɗ�
            sql = sql & N & "   ,ZSEISANSU "            '�O�����Y���ѐ�
            sql = sql & N & "   ,ZSEISANRYOU "          '�O�����Y���ї�
            sql = sql & N & "   ,ZHANBAISU "            '�O���̔����ѐ�
            sql = sql & N & "   ,ZHANBAIRYOU "          '�O���̔����ї�
            sql = sql & N & "   ,ZZAIKOSU "             '�O�����݌ɐ�
            sql = sql & N & "   ,ZZAIKORYOU "           '�O�����݌ɗ�
            sql = sql & N & "   ,TSEISANSU "            '�������Y�v�搔
            sql = sql & N & "   ,TSEISANRYOU "          '�������Y�v���
            sql = sql & N & "   ,THANBAISU "            '�����̔��v�搔
            sql = sql & N & "   ,THANBAIRYOU "          '�����̔��v���
            sql = sql & N & "   ,TZAIKOSU "             '�������݌ɐ�
            sql = sql & N & "   ,TZAIKORYOU "           '�������݌ɗ�
            sql = sql & N & "   ,KURIKOSISU "           '�J�z��
            sql = sql & N & "   ,KURIKOSIRYOU "         '�J�z��
            sql = sql & N & "   ,IKATULOTOSU "          '�ꊇ�Z�o���b�g��
            sql = sql & N & "   ,LOTOSU "               '���b�g��
            sql = sql & N & "   ,YSEISANSU "            '�������Y�v�搔
            sql = sql & N & "   ,YSEISANRYOU "          '�������Y�v���
            sql = sql & N & "   ,YHANBAISU "            '�����̔��v�搔
            sql = sql & N & "   ,YHANBAIRYOU "          '�����̔��v���
            sql = sql & N & "   ,YZAIKOSU "             '�������݌ɐ�
            sql = sql & N & "   ,YZAIKORYOU "           '�������݌ɗ�
            sql = sql & N & "   ,YZAIKOTUKISU  "        '�����݌Ɍ���
            sql = sql & N & "   ,YYHANBAISU "           '���X���̔��v�搔
            sql = sql & N & "   ,YYHANBAIRYOU "         '���X���̔��v���
            sql = sql & N & "   ,KTUKISU "              '�����
            sql = sql & N & "   ,FZAIKOSU "             '�����p�݌ɐ�
            sql = sql & N & "   ,FZAIKORYOU "           '�����p�݌ɗ�
            sql = sql & N & "   ,AZAIKOSU "             '���S�݌ɐ�
            sql = sql & N & "   ,AZAIKORYOU "           '���S�݌ɗ�
            sql = sql & N & "   ,METSUKE "              '�ڕt
            sql = sql & N & "   ,UPDNAME "              '�[��ID
            sql = sql & N & "   ,TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS')"
            sql = sql & N & "   FROM ZG530E_W10 "
            sql = sql & N & "       WHERE UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql)

            '�X�V�I���������擾
            Dim updFinDate As Date = Now

            '�X�V�����̎擾
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanSyuusei)        'DGV�n���h���̐ݒ�
            Dim updCnt As Integer = gh.getMaxRow

            '���������E�v������擾
            Dim syoriDate As String = lblSyori.Text.Substring(0, 4) & lblSyori.Text.Substring(5, 2)
            Dim keikakuDate As String = lblKeikaku.Text.Substring(0, 4) & lblKeikaku.Text.Substring(5, 2)

            '���s����o�^����
            sql = ""
            sql = "INSERT INTO T91RIREKI ("
            sql = sql & N & "  PGID"                                                        '�@�\ID
            sql = sql & N & ", SNENGETU"                                                    '��������
            sql = sql & N & ", KNENGETU"                                                    '�v�����
            sql = sql & N & ", SDATESTART"                                                  '�����J�n����
            sql = sql & N & ", SDATEEND"                                                    '�����I������
            sql = sql & N & ", KENNSU1"                                                     '�����P�i�폜�����j
            sql = sql & N & ", KENNSU2"                                                     '�����Q�i�o�^�����j
            sql = sql & N & ", UPDNAME"                                                     '�[��ID
            sql = sql & N & ", UPDDATE"                                                     '�X�V����
            sql = sql & N & ") VALUES ("
            sql = sql & N & "  '" & PGID & "'"                                              '�@�\ID
            sql = sql & N & ", '" & syoriDate & "'"
            sql = sql & N & ", '" & keikakuDate & "'"
            sql = sql & N & ", TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "   '�����J�n����
            sql = sql & N & ", TO_DATE('" & updFinDate & "', 'YYYY/MM/DD HH24:MI:SS') "     '�����I������
            sql = sql & N & ", " & delCnt                                                   '�����P�i�폜�����j
            sql = sql & N & ", " & updCnt                                                   '�����Q�i�o�^�����j
            sql = sql & N & ", '" & _tanmatuID & "'"                                        '�[��ID
            sql = sql & N & ", TO_DATE('" & updFinDate & "', 'YYYY/MM/DD HH24:MI:SS') "     '�X�V����
            sql = sql & N & " )"
            _db.executeDB(sql)

            'T02��������e�[�u���X�V
            _parentForm.updateSeigyoTbl(PGID, True, updStartDate, updFinDate)

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@�i��ʏW�v�\�o�͗p�f�[�^���o
    '�@(�����T�v)�i��ʏW�v�\�o�͂̂��߁A���[�N�e�[�u���̃f�[�^���f�[�^�Z�b�g�ɕێ����ĕԂ��B
    '   �����̓p�����^  �F���v��R�[�h
    '   ���o�̓p�����^  �F�������ʂ̃f�[�^�Z�b�g
    '                   �F�f�[�^�Z�b�g�̌���
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub getDataForHinsyuXls(ByVal prmJuyoCD As String, ByRef prmDs As DataSet, ByRef prmRecCnt As Integer)
        Try

            Dim sql As String = ""
            '���[�N�̃f�[�^���ꗗ�ɕ\��
            sql = sql & N & " SELECT "
            sql = sql & N & "    HINSYUCD " & COLDT_HINSYUCD                '�i��R�[�h

            '' 2011/01/24 upd start sugano
            'sql = sql & N & "   ,HINSYUNM " & COLDT_HINSYUNM                '�i�햼
            sql = sql & N & "   ,MAX(HINSYUNM) " & COLDT_HINSYUNM           '�i�햼
            '' 2011/01/24 upd end sugano

            sql = sql & N & "   ,SUM(ZZZAIKORYOU) " & COLDT_ZZZAIKOR        '�O�X�����݌ɗ�
            sql = sql & N & "   ,SUM(ZSEISANRYOU) " & COLDT_ZSEISANR        '�O�����Y���ї�
            sql = sql & N & "   ,SUM(ZHANBAIRYOU) " & COLDT_ZHANBAIR        '�O���̔����ї�

            '' 2011/01/20 upd start sugano
            'sql = sql & N & "   ,SUM(ZZAIKORYOU) " & COLDT_ZZAIKOR          '�O�����݌ɗ�
            sql = sql & N & "   ,SUM(ZZAIKORYOU) " & COLDT_ZZAIKOR          '�O�����݌ɗ�
            '' 2011/01/20 upd end sugano

            sql = sql & N & "   ,SUM(TSEISANRYOU) " & COLDT_TSEISANR        '�������Y�v���
            sql = sql & N & "   ,SUM(THANBAIRYOU) " & COLDT_THANBAIR        '�����̔��v���

            '' 2011/01/20 upd start sugano
            'sql = sql & N & "   ,SUM(TZAIKORYOU) " & COLDT_TZAIKOR          '�������݌ɗ�
            sql = sql & N & "   ,SUM(ZZAIKORYOU+TSEISANRYOU-THANBAIRYOU) " & COLDT_TZAIKOR          '�������݌ɗ�
            '' 2011/01/20 upd end sugano

            sql = sql & N & "   ,SUM(KURIKOSIRYOU) " & COLDT_KURIKOSIR      '�J�z��
            sql = sql & N & "   ,SUM(LOTOSU) " & COLDT_LOTSU                '���b�g��
            sql = sql & N & "   ,SUM(YSEISANRYOU) " & COLDT_YSEISANR        '�������Y�v���
            sql = sql & N & "   ,SUM(YHANBAIRYOU) " & COLDT_YHANBAIR        '�����̔��v���

            '' 2011/01/20 upd start sugano
            'sql = sql & N & "   ,SUM(YZAIKORYOU) " & COLDT_YZAIKOR          '�������݌ɗ�   
            sql = sql & N & "   ,SUM(ZZAIKORYOU+TSEISANRYOU-THANBAIRYOU+YSEISANRYOU-YHANBAIRYOU) " & COLDT_YZAIKOR          '�������݌ɗ�   
            '' 2011/01/20 upd end sugano

            sql = sql & N & "   ,SUM(YYHANBAIRYOU) " & COLDT_YYHANBAIR      '���X���̔��v���
            sql = sql & N & " FROM ZG530E_W10 "
            sql = sql & N & "   WHERE UPDNAME = '" & _tanmatuID & "' AND JUYOUCD = '" & prmJuyoCD & "'"

            '' 2011/01/24 upd start sugano
            'sql = sql & N & "   GROUP BY HINSYUCD, HINSYUNM "
            sql = sql & N & "   GROUP BY HINSYUCD "
            '' 2011/01/24 upd end sugano

            sql = sql & N & "   ORDER BY HINSYUCD "

            'SQL���s
            prmDs = _db.selectDB(sql, RS, prmRecCnt)

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@���Y�̔��݌Ɍv��o�͗p�f�[�^���o
    '�@(�����T�v)���Y�̔��݌Ɍv��o�͂̂��߁A���[�N�e�[�u���̃f�[�^���f�[�^�Z�b�g�ɕێ����ĕԂ��B
    '   �����̓p�����^  �F���v��R�[�h
    '   ���o�̓p�����^  �F�������ʂ̃f�[�^�Z�b�g
    '                   �F�f�[�^�Z�b�g�̌���
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub getDataForSeisanXls(ByVal prmJuyoCD As String, ByRef prmDs As DataSet, ByRef prmRecCnt As Integer)
        Try

            Dim sql As String = ""
            '���[�N�̃f�[�^���ꗗ�ɕ\��
            sql = sql & N & " SELECT "
            sql = sql & N & "    HINSYUCD " & COLDT_HINSYUCD        '�i��R�[�h
            sql = sql & N & "   ,SIYOUCD " & COLDT_SIYOCD           '�d�l�R�[�h
            sql = sql & N & "   ,HINMEI " & COLDT_HINMEI            '�i��
            sql = sql & N & "   ,LOT " & COLDT_LOTTYO               '�W�����b�g��
            sql = sql & N & "   ,ABCKBN " & COLDT_ABC               'ABC�敪
            sql = sql & N & "   ,ZZZAIKOSU " & COLDT_ZZZAIKOS       '�O�X�����݌ɐ�
            sql = sql & N & "   ,ZSEISANSU " & COLDT_ZSEISANS       '�O�����Y���ѐ�
            sql = sql & N & "   ,ZHANBAISU " & COLDT_ZHANBAIS       '�O���̔����ѐ�
            sql = sql & N & "   ,TSEISANSU " & COLDT_TSEISANS       '�������Y�v�搔
            sql = sql & N & "   ,TSEISANRYOU " & COLDT_TSEISANR     '�������Y�v���
            sql = sql & N & "   ,THANBAISU " & COLDT_THANBAIS       '�����̔��v�搔
            sql = sql & N & "   ,THANBAIRYOU " & COLDT_THANBAIR     '�����̔��v���
            sql = sql & N & "   ,ZZAIKORYOU " & COLDT_ZZAIKOR       '�O�����݌ɗ�
            sql = sql & N & "   ,KURIKOSISU " & COLDT_KURIKOSIS     '�J�z��
            sql = sql & N & "   ,LOTOSU " & COLDT_LOTSU             '���b�g��
            sql = sql & N & "   ,YSEISANSU " & COLDT_YSEISANS       '�������Y�v�搔
            sql = sql & N & "   ,YSEISANRYOU " & COLDT_YSEISANR     '�������Y�v���
            sql = sql & N & "   ,YHANBAISU " & COLDT_YHANBAIS       '�����̔��v�搔
            sql = sql & N & "   ,YHANBAIRYOU " & COLDT_YHANBAIR     '�����̔��v���
            sql = sql & N & "   ,YZAIKOTUKISU " & COLDT_ZAIKOTUKISU '�����݌Ɍ���
            sql = sql & N & "   ,YYHANBAISU " & COLDT_YYHANBAIS     '���X���̔��v�搔
            sql = sql & N & "   ,YYHANBAIRYOU " & COLDT_YYHANBAIR   '���X���̔��v���
            sql = sql & N & "   ,KTUKISU " & COLDT_KTUKISU          '�����
            '' 2011/01/20 upd start sugano
            'sql = sql & N & "   ,FZAIKOSU " & COLDT_FZAIKOS         '�����p�݌ɐ�
            'sql = sql & N & "   ,AZAIKOSU " & COLDT_AZAIKOS         '���S�݌ɐ�
            sql = sql & N & "   ,DECODE(FZAIKOSU,0,'',FZAIKOSU) " & COLDT_FZAIKOS         '�����p�݌ɐ�
            sql = sql & N & "   ,DECODE(AZAIKOSU,0,'',AZAIKOSU) " & COLDT_AZAIKOS         '���S�݌ɐ�
            '' 2011/01/20 upd end sugano
            sql = sql & N & "   ,METSUKE " & COLDT_METUKE           '�ڕt
            sql = sql & N & " FROM ZG530E_W10 "
            sql = sql & N & "   WHERE UPDNAME = '" & _tanmatuID & "' AND JUYOUCD = '" & prmJuyoCD & "'"
            sql = sql & N & "   ORDER BY JUYOSORT, HINSYUCD, SENSINCD, SIZECD, SIYOUCD, COLORCD "


            'SQL���s
            prmDs = _db.selectDB(sql, RS, prmRecCnt)

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

#End Region

#Region "���[�U��`�֐�:�`�F�b�N����"

    '------------------------------------------------------------------------------------------------------
    '  �o�^�`�F�b�N
    '�@(�����T�v)�K�{���ڃ`�F�b�N���s��
    '------------------------------------------------------------------------------------------------------
    Private Sub checkTouroku()
        Try

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanSyuusei)

            For i As Integer = 0 To gh.getMaxRow - 1

                '���͌����`�F�b�N
                '���b�g��
                Call checkKeta(COLDT_LOTSU, "���b�g��", i, COLNO_LOTSUU)

            Next

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '  �K�{���̓`�F�b�N
    '�@(�����T�v)�Z�������͂���Ă��邩�`�F�b�N����
    '�@�@I�@�F�@prmColName              �`�F�b�N����Z���̗�
    '�@�@I�@�F�@prmColHeaderName        �G���[���ɕ\�������
    '�@�@I�@�F�@prmCnt                  �`�F�b�N����Z���̍s��
    '�@�@I�@�F�@prmColNo                �`�F�b�N����Z���̗�
    '------------------------------------------------------------------------------------------------------
    Private Sub checkKeta(ByVal prmColName As String, ByVal prmColHeaderName As String, ByVal prmCnt As Integer, ByVal prmColNo As Integer)
        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanSyuusei)

            '�K�{���̓`�F�b�N
            If "".Equals(gh.getCellData(prmColName, prmCnt).ToString) Then
                '�t�H�[�J�X�����Ă�
                Call setForcusCol(prmColNo, prmCnt)
                '�G���[���b�Z�[�W�̕\��
                '-->2010.12.17 chg by takagi #13
                'Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y '" & prmColHeaderName & "' �F" & prmCnt + 1 & "�s�ځz"))
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput"))
                '<--2010.12.17 chg by takagi #13
            End If

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

#End Region

End Class