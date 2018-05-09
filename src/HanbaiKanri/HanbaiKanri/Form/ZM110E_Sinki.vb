'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���j�v��Ώەi�}�X�^�����e�V�K�o�^���
'    �i�t�H�[��ID�jZM110E_Sinki
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ���V        2010/10/25                 �V�K
'�@(2)   ����        2011/01/13                 �ύX�@�ȖڃR�[�h�����`�F�b�N�ǉ�
'�@(3)   ����        2014/06/04                 �ύX�@�ޗ��[�}�X�^�iMPESEKKEI�j�e�[�u���ύX�Ή�            
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.Combo
Imports UtilMDL.DataGridView

Public Class ZM110E_Sinki
    Implements IfRturnKahenKey

#Region "���e�����l��`"

    'PG���䕶�� 
    Private Const N As String = ControlChars.NewLine            '���s����
    Private Const RS As String = "RecSet"                       '���R�[�h�Z�b�g�e�[�u��
    Private Const RS2 As String = "RecSet2"                     '���R�[�h�Z�b�g�e�[�u��
    Private Const NUMBER As String = "999999D9"                 'SQL�ŃJ���}��؂�̐��l��������J���}�����̐��l�ɕϊ����邽�߂̌`��

    '-->2010.12.02 upd by takagi
    'Private Const PGID As String = "ZM110E1"                     'T91�ɓo�^����PGID
    Private _pgId As String = ""                     'T91�ɓo�^����PGID
    '<--2010.12.02 upd by takagi

    '��ʃ��[�h
    Private Const SINKITOUROKU As String = "�V�K�o�^"
    Private Const SAKUJO As String = "�폜"

    '�e���͍��ڏ����l
    Private Const SYOKI_SEISAKUKBN As String = "1"              '����敪��1(����)
    Private Const SYOKI_ZAIKO As String = "1"                   '�݌ɁE�J�ԁ�1(�݌ɑΏ�)
    Private Const SYOKI_TENKAIKBN As String = "1"               '�W�J�敪��1(�S�W�J)
    Private Const SYOKI_HINSHITSUKBN As String = "0"            '�i�������敪��0(�]���s�v)
    Private Const SYOKI_TACHIAIKBN As String = "1"              '����L����1(�i�V)

    Private Const SYOKI_SYORIKBN As String = "1"                '�����敪��1(�V�K)
    Private Const SYOKI_KAKOUKBN As String = "0"                '���H���v�Z�敪��0(�v�Z�s�v)
    Private Const SYOKI_TANTYOKBN As String = "2"               '�P���敪��2(�W���O)

    Private Const SYOKI_SEISEKI As String = "0"                '���я�
    Private Const SYOKI_MTANTYO As String = "0"                '�����]���P����
    Private Const SYOKI_MLOT As String = "0"                   '�����]�����b�g��
    Private Const SYOKI_TTANTYO As String = "0"                '����]���P����
    Private Const SYOKI_TLOT As String = "0"                   '����]�����b�g��
    Private Const SYOKI_STANTYO As String = "0"                '�w��Ќ��P����
    Private Const SYOKI_SLOT As String = "0"                   '�w��Ќ����b�g��
    Private Const SYOKI_TOKKI As String = ""                    '���L����
    Private Const SYOKI_BIKOU As String = ""                    '���l
    Private Const SYOKI_NYUUKO As String = ""                   '���ɖ{��
    Private Const SYOKI_TOUROKUFLG As String = ""               '�o�^�t���O
    Private Const SYOKI_HENKOU As String = ""                   '�ύX���e

    '����敪�R�[�h
    Private Const SEISAKUKBNCD_NAISAKU As String = "1"
    Private Const SEISAKUKBNCD_GAITYU As String = "2"

    '�݌ɁE�J�ԃR�[�h
    Private Const ZAIKOCD_ZAIKO As String = "1"
    Private Const ZAIKOCD_KURIKAESI As String = "2"

    '�W�J�敪�R�[�h
    Private Const TENKAIKBNCD_ZEN As String = "1"
    Private Const TENKAIKBNCD_BUBUN As String = "2"

    '�i�������敪�R�[�h
    Private Const HINSITUCD_YOTYOUFUYO As String = "0"
    Private Const HINSITUCD_LOTKANRI As String = "2"

    '�ޗ��[DB�����p
    Private Const HINSYU As String = "HINSYU"
    Private Const LINE As String = "LINE"
    Private Const COLOR As String = "COLOR"

    'M11�����p���e����
    Private Const DB_SEISAKU As String = "seisakukubn"      '����敪
    Private Const DB_HINMEI As String = "hinmei"            '�i��
    Private Const DB_LOTTYO As String = "lottyo"            '�W�����b�g��
    Private Const DB_TANTYO As String = "tantyo"            '�P��
    Private Const DB_JOSU As String = "josu"                '��
    Private Const DB_KND As String = "knd"                  '�k���{��
    Private Const DB_SUMIDEN As String = "sumiden"          '�Z�d������
    Private Const DB_ZAIKO As String = "zaiko"              '�݌ɁE�J��
    Private Const DB_TYUMONSAKI As String = "tyumonsaki"    '������
    Private Const DB_JUYOSAKI As String = "juyousaki"       '���v��
    Private Const DB_ABC As String = "abc"                  'ABC
    Private Const DB_SIZETENKAI As String = "sizetenkai"    '�T�C�Y�W�J
    Private Const DB_HINSYUKBN As String = "hinsyukbn"      '�i��敪
    Private Const DB_KIJUNTUKI As String = "kijuntukisu"    '�����
    Private Const DB_SAIGAI As String = "saigai"            '�ЊQ�����p�݌ɗ�
    Private Const DB_ANZEN As String = "anzen"              '���S�݌ɗ�
    Private Const DB_KAMOKU As String = "kamoku"            '�ȖڃR�[�h
    Private Const DB_MAKIWAKU As String = "makiwaku"        '���g�R�[�h
    Private Const DB_HOUSOU As String = "housou"            '��^�\���敪
    Private Const DB_SIYOUSYO As String = "siyousyo"        '�d�l���ԍ�
    Private Const DB_SEIZOUBMN As String = "seizobumon"     '��������
    Private Const DB_TEHAIKBN As String = "tehaikbn"        '��z�敪
    Private Const DB_TENKAIKBN As String = "tenkai"         '�W�J�敪
    Private Const DB_BUBUNTENKAI As String = "bubuntenkai"  '�����W�J�H��
    Private Const DB_HINSITU As String = "hinsitu"          '�i�������敪
    Private Const DB_TATIAI As String = "tatiai"            '����L��
    Private Const DB_SYORIKBN As String = "syorikbn"        '�����敪
    Private Const DB_KAKOU As String = "kakou"              '���H���v�Z�敪
    Private Const DB_TANTYOKBN As String = "tantyokbn"      '�P���敪
    Private Const DB_SEISEKI As String = "seiseki"          '���я�
    Private Const DB_MTANTYO As String = "mtantyo"          '�����]���P����
    Private Const DB_MLOT As String = "mlot"                '�����]�����b�g��
    Private Const DB_TTANTYO As String = "ttantyo"          '����]���P����
    Private Const DB_TLOT As String = "tlot"                '����]�����b�g��
    Private Const DB_STANTYO As String = "stantyo"          '�w��Ќ��P����
    Private Const DB_SLOT As String = "slot"                '�w��Ќ����b�g��
    Private Const DB_TOKKI As String = "tokki"              '���L����
    Private Const DB_BIKOU As String = "bikou"              '���l
    Private Const DB_NYUUKO As String = "nyuuko"            '���ɖ{��
    Private Const DB_TOUROKU As String = "touroku"          '�o�^�t���O
    Private Const DB_HENKOU As String = "henkou"            '�ύX���e

    'M12�����p���e����
    Private Const COLDT_KHINMEICD As String = "dtSHinmeiCD"
    Private Const COLDT_KHINMEI As String = "dtSHinmei"
    Private Const COLDT_HINSYUNM As String = "dtHinsyuNM"
    Private Const COLDT_SIZENM As String = "dtSizeNM"
    Private Const COLDT_IRONM As String = "dtIroNM"

    '�ޗ��[�����p���e����
    Private Const INT_SEQNO As Integer = 1

    '�ėp�}�X�^�Œ�L�[
    Private Const HKOTEIKEY_JUYOSAKI As String = "01"               '���v��
    Private Const HKOTEIKEY_TEHAI_KBN As String = "02"              '��z�敪
    Private Const HKOTEIKEY_SEISAKU_KBN As String = "03"            '����敪
    Private Const HKOTEIKEY_TENKAI_KBN As String = "04"             '�W�J�敪
    Private Const HKOTEIKEY_KAKOUCHO_KBN As String = "05"           '���H���v�Z�敪
    Private Const HKOTEIKEY_TACHIAI_UM As String = "06"             '����L��
    Private Const HKOTEIKEY_TANCHO_KBN As String = "07"             '�P���敪
    Private Const HKOTEIKEY_HINSHITU_KBN As String = "08"           '�i�������敪
    Private Const HKOTEIKEY_SEIZO_BMN As String = "09"              '��������
    Private Const HKOTEIKEY_ZAIKO_KBN As String = "10"              '�݌ɁE�J��
    Private Const HKOTEIKEY_SIZETENKAI_KBN As String = "11"         '�T�C�Y�W�J
    Private Const HKOTEIKEY_SYORI_KBN As String = "19"              '�����敪

    '���̓`�F�b�N�p���e����
    Private Const TENKAIKBN_BUBUN As String = "2"                   '�W�J�敪=2(�����W�J) 
    Private Const SEISAKUKBN_GAITYU As String = "2"                 '����敪=2(�O��)

    '�W�v�Ώەi���R�[�h�ꗗ�f�[�^�o�C���h��
    Private Const COLDT_SCODE As String = "dtSHinmeiCD"             '�W�v�Ώەi���R�[�h
    Private Const COLDT_SNAME As String = "dtSHinmei"               '�W�v�Ώەi��

    Private Enum IniFormType As Short
        ''' <summary>
        ''' �L�[���ڊ�
        ''' </summary>
        ''' <remarks></remarks>
        IncludeKey = 1
        ''' <summary>
        ''' �L�[���ڏ���
        ''' </summary>
        ''' <remarks></remarks>
        ExecludeKey = 0

    End Enum

#End Region

#Region "�����o�[�ϐ��錾"

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu
    Private _ZC910KahenKey As String                'ZC910S_CodeSentaku����󂯎��ėp�}�X�^�σL�[
    Private _ZC910Meisyo As String                  'ZC910S_CodeSentaku����󂯎��ėp�}�X�^����

    Private _oldRowIndex As Integer = -1            '�I���s�̔w�i�F��ύX���邽�߂̃t���O��錾
    Private _colorCtlFlg As Boolean = False         '�I���s�̔w�i�F��ύX���邽�߂̃t���O��錾

    Private _updFlg As Boolean = False              '�X�V��
    Private _sinkiFlg As Boolean = False            '���[�h����t���O
    Private _itiranDispFirstFlg As Boolean = True   '�ꗗ�Ƀf�[�^���\������Ă��邩�ǂ����̃t���O

    Private _tanmatuID As String = ""               '�[��ID
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
    '�R���X�g���N�^�@   ���j���[�܂��͏C����ʂ���Ă΂��
    '-------------------------------------------------------------------------------
    '-->2010.12.02 add by takagi
    'Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmForm As ZC110M_Menu, _
    '                                         ByVal prmUpdFlg As Boolean, ByVal prmSinkiFlg As Boolean)
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmForm As ZC110M_Menu, _
                                     ByVal prmUpdFlg As Boolean, ByVal prmSinkiFlg As Boolean, ByVal prmPgId As String)
        '<--2010.12.02 add by takagi
        Call Me.New()

        '��������
        _msgHd = prmRefMsgHd                                                'MSG�n���h���̐ݒ�
        _db = prmRefDbHd                                                    'DB�n���h���̐ݒ�
        _parentForm = prmForm                                               '�e�t�H�[��
        StartPosition = FormStartPosition.CenterScreen                      '��ʒ����\��
        _updFlg = prmUpdFlg
        _sinkiFlg = prmSinkiFlg
        '-->2010.12.02 add by takagi
        _pgId = prmPgId
        '<--2010.12.02 add by takagi

    End Sub
#End Region

#Region "Form�C�x���g"
    '-------------------------------------------------------------------------------
    '�@�����J�n�C�x���g
    '-------------------------------------------------------------------------------
    Private Sub ZM110E_Sinki_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            '��ʕ\��
            Call initForm()

            If _sinkiFlg Then
                '�W�v�Ώەi���ꗗ�o�C���h�p�f�[�^�e�[�u���̗��`
                Dim dt As DataTable = New DataTable(RS)
                dt.Columns.Add(COLDT_SCODE, Type.GetType("System.String")) 'String�^
                dt.Columns.Add(COLDT_SNAME, Type.GetType("System.String")) 'String�^

                '�f�[�^�Z�b�g�Ƀf�[�^�e�[�u�����Z�b�g
                Dim ds As DataSet = New DataSet
                ds.Tables.Add(dt)

                '�O���b�h�Ƀf�[�^�Z�b�g���o�C���h
                dgvSTaisyou.DataSource = ds
                dgvSTaisyou.DataMember = RS
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

#End Region

#Region "�{�^���C�x���g"

    '-------------------------------------------------------------------------------
    '�@�����{�^�������C�x���g
    '�@(�����T�v)���͒l���ꂽ�i���R�[�h�����ƂɃf�[�^����ʕ\������B
    '�@�@�@�@�@�@����������́A�L�����Z���{�^�������܂Ō����s�Ƃ���B
    '-------------------------------------------------------------------------------
    Private Sub btnKensaku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensaku.Click
        Try

            '���������̃`�F�b�N
            '�i���R�[�h���S�ē��͂���Ă���ꍇ�͌������s���B
            Call checkInputHinmeiCD()

            '��ʂ̃N���A
            clearCtrDelMode()

            '�f�[�^���������ĉ�ʕ\��
            Call dispData()

            '�e���͍��ڂ����ƂɃ��x����\������B
            Call txtSeisaku_Leave(Nothing, Nothing)                                     '����敪
            Call txtZaiko_Leave(Nothing, Nothing)                                       '�݌ɥ�J��
            Call txtJuyousaki_Leave(Nothing, Nothing)                                   '���v��
            Call txtInput_Leave(txtSizeTenkai, Nothing)                                 '�T�C�Y�W�J
            Call txtInput_Leave(txtSeizouBmn, Nothing)                                  '��������
            Call txtTenkaiKbn_Leave(Nothing, Nothing)                                   '�W�J�敪
            Call txtInput_Leave(txtHinsitu, Nothing)                                    '�i�������敪
            Call txtInput_Leave(txtTatiai, Nothing)                                     '����L��
            Call dispMakiwakuLabel()                                                    '���g�R�[�h
            Call dispHousouLabel()                                                      '��^�\���敪
            lblTehaiKbnNM.Text = serchHanyoMst(HKOTEIKEY_TEHAI_KBN, lblTehaiKbn.Text)   '��z�敪
            lblSyoriKbnNM.Text = serchHanyoMst(HKOTEIKEY_SYORI_KBN, lblSyoriKbn.Text)   '�����敪
            lblKakoNM.Text = serchHanyoMst(HKOTEIKEY_KAKOUCHO_KBN, lblKako.Text)        '���H���v�Z�敪
            lblTantyoNM.Text = serchHanyoMst(HKOTEIKEY_TANCHO_KBN, lblTantyo.Text)      '�P���敪
            Call dispCboLabel()                                                         '�i��敪

            btnKensaku.Enabled = False      '�����{�^���g�p�s��
            btnTouroku.Enabled = True       '�폜�{�^���g�p��
            btnCancel.Enabled = True        '�L�����Z���{�^���g�p��

            '�L�����Z���{�^�������܂ōČ����s��
            txtSiyo.ReadOnly = True                     '�d�l�R�[�h
            txtSiyo.BackColor = StartUp.lCOLOR_YELLOW
            txtHinsyu.ReadOnly = True                   '�i��R�[�h
            txtHinsyu.BackColor = StartUp.lCOLOR_YELLOW
            txtSensin.ReadOnly = True                   '���S���R�[�h
            txtSensin.BackColor = StartUp.lCOLOR_YELLOW
            txtSize.ReadOnly = True                     '�T�C�Y�R�[�h
            txtSize.BackColor = StartUp.lCOLOR_YELLOW
            txtColor.ReadOnly = True                    '�F�R�[�h
            txtColor.BackColor = StartUp.lCOLOR_YELLOW

            '�L�����Z���{�^���Ƀt�H�[�J�X
            btnCancel.Focus()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@�L�����Z���{�^�������C�x���g
    '�@(�����T�v)��ʂ��N���A���A�����{�^�����g�p�Ƃ���B
    '-------------------------------------------------------------------------------
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            '��ʃN���A
            Call clearCtrDelMode()

            '�i���R�[�h���͉E�e�L�X�g�{�b�N�X�̐F��߂�
            txtSiyo.Focus()                             '�d�l�R�[�h
            txtSiyo.Text = ""
            txtSiyo.BackColor = StartUp.lCOLOR_WHITE
            txtSiyo.ReadOnly = False
            txtSiyo.Enabled = True
            txtHinsyu.Text = ""                         '�i��R�[�h
            txtHinsyu.BackColor = StartUp.lCOLOR_WHITE
            txtHinsyu.ReadOnly = False
            txtHinsyu.Enabled = True
            txtSensin.Text = ""                         '���S���R�[�h
            txtSensin.BackColor = StartUp.lCOLOR_WHITE
            txtSensin.ReadOnly = False
            txtSensin.Enabled = True
            txtSize.Text = ""                           '�T�C�Y�R�[�h
            txtSize.BackColor = StartUp.lCOLOR_WHITE
            txtSize.ReadOnly = False
            txtSize.Enabled = True
            txtColor.Text = ""                          '�F�R�[�h
            txtColor.BackColor = StartUp.lCOLOR_WHITE
            txtColor.ReadOnly = False
            txtColor.Enabled = True

            '�{�^������
            btnKensaku.Enabled = True       '�����{�^���g�p��
            btnTouroku.Enabled = False      '�폜�{�^���g�p�s��
            btnCancel.Enabled = False       '�L�����Z���{�^���g�p�s��
            btnHinmeiHyouji.Enabled = True

            '' 2010/12/27 add start sugano #�L�����Z�����A�e���ڂɏ����l���ݒ肳��Ȃ��s����C��
            Call initForm()
            '' 2010/12/27 add end sugano

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�i���\���{�^�������C�x���g
    '�@(�����T�v)���͒l���ꂽ�i���R�[�h�����Ƃɕi�������x���ɕ\������B
    '-------------------------------------------------------------------------------
    Private Sub btnHinmeiHyouji_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHinmeiHyouji.Click
        Try

            '���������̃`�F�b�N
            '�i���R�[�h���S�ē��͂���Ă���ꍇ�͌������s���B
            Call checkInputHinmeiCD()

            '�i�����������ĕ\��
            Call dispHinmei()

            '�o�^�{�^���g�p��
            btnTouroku.Enabled = True

            '�L�����Z���{�^���g�p��
            btnCancel.Enabled = True
            btnCancel.Visible = True

            '�i���R�[�h���͕s�E�i���\���s��
            txtSiyo.ReadOnly = True                     '�d�l�R�[�h
            txtSiyo.BackColor = StartUp.lCOLOR_YELLOW
            txtHinsyu.ReadOnly = True                   '�i��R�[�h
            txtHinsyu.BackColor = StartUp.lCOLOR_YELLOW
            txtSensin.ReadOnly = True                   '���S���R�[�h
            txtSensin.BackColor = StartUp.lCOLOR_YELLOW
            txtSize.ReadOnly = True                     '�T�C�Y�R�[�h
            txtSize.BackColor = StartUp.lCOLOR_YELLOW
            txtColor.ReadOnly = True                    '�F�R�[�h
            txtColor.BackColor = StartUp.lCOLOR_YELLOW
            btnHinmeiHyouji.Enabled = False             '�i���\���{�^��

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@�q��ʋN���e�{�^�������C�x���g
    '�@(�����T�v)�q��ʂ��N�����A�q��ʂőI�����ꂽ�f�[�^��e��ʂɕ\������
    '-------------------------------------------------------------------------------
    Private Sub clickSubBtn(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeisaku.Click, _
                                                                                                btnJuyousaki.Click, _
                                                                                                btnZaiko.Click, _
                                                                                                btnSizeTenkai.Click, _
                                                                                                btnSeizouBmn.Click, _
                                                                                                btnTenkaiKbn.Click, _
                                                                                                btnHinsitu.Click, _
                                                                                                btnTatiai.Click



        Dim koteiKey As String = ""     '�q��ʂɓn���Œ�L�[
        Dim targetTxtBox As TextBox     '���������{�^���ɑΉ�����e�L�X�g�{�b�N�X
        Dim targetLbl As Label

        Try

            Dim buttonName As String = ""
            buttonName = CType(sender, Button).Name

            Select Case True
                Case btnSeisaku.Name.Equals(buttonName)
                    '�u����敪�v�{�^�������C�x���g
                    koteiKey = HKOTEIKEY_SEISAKU_KBN
                    targetTxtBox = txtSeisaku
                    targetLbl = lblSeisaku
                Case btnJuyousaki.Name.Equals(buttonName)
                    '�u���v��v�{�^�������C�x���g
                    koteiKey = HKOTEIKEY_JUYOSAKI
                    targetTxtBox = txtJuyousaki
                    targetLbl = lblJuyousaki
                Case btnZaiko.Name.Equals(buttonName)
                    '�u�݌ɁE�J�ԁv�{�^�������C�x���g
                    koteiKey = HKOTEIKEY_ZAIKO_KBN
                    targetTxtBox = txtZaiko
                    targetLbl = lblZaiko
                Case btnSizeTenkai.Name.Equals(buttonName)
                    '�u�T�C�Y�W�J�v�{�^�������C�x���g
                    koteiKey = HKOTEIKEY_SIZETENKAI_KBN
                    targetTxtBox = txtSizeTenkai
                    targetLbl = lblSizeTenkai
                Case btnSeizouBmn.Name.Equals(buttonName)
                    '�u��������v�{�^�������C�x���g
                    koteiKey = HKOTEIKEY_SEIZO_BMN
                    targetTxtBox = txtSeizouBmn
                    targetLbl = lblSeizoBmn
                Case btnTenkaiKbn.Name.Equals(buttonName)
                    '�u�W�J�敪�v�{�^�������C�x���g
                    koteiKey = HKOTEIKEY_TENKAI_KBN
                    targetTxtBox = txtTenkaiKbn
                    targetLbl = lblTenkaiKbn
                Case btnHinsitu.Name.Equals(buttonName)
                    '�u�i�������敪�v�{�^�������C�x���g
                    koteiKey = HKOTEIKEY_HINSHITU_KBN
                    targetTxtBox = txtHinsitu
                    targetLbl = lblHinmei
                Case btnTatiai.Name.Equals(buttonName)
                    '�u����L���v�{�^�������C�x���g
                    koteiKey = HKOTEIKEY_TACHIAI_UM
                    targetTxtBox = txtTatiai
                    targetLbl = lblTatiai
                Case Else
                    Exit Sub
            End Select

            Dim kahenKey As String = ""
            If Not "".Equals(targetTxtBox.Text) Then
                kahenKey = targetTxtBox.Text
            End If

            '�q��ʂ̋N��
            Dim openForm As ZC910S_CodeSentaku
            If targetTxtBox.Equals(txtJuyousaki) Then
                '���v��̏ꍇ�͖���2��Ԃ�
                openForm = New ZC910S_CodeSentaku(_msgHd, _db, Me, koteiKey, kahenKey, StartUp.HANYO_BACK_NAME2)      '�p�����^��J�ڐ��ʂ֓n��
            Else
                '����ȊO�͖���1��Ԃ�
                openForm = New ZC910S_CodeSentaku(_msgHd, _db, Me, koteiKey, kahenKey, StartUp.HANYO_BACK_NAME1)      '�p�����^��J�ڐ��ʂ֓n��
            End If
            openForm.ShowDialog(Me)                                                             '��ʕ\��
            openForm.Dispose()
            If Not "".Equals(_ZC910KahenKey) Then                    'getKahenKey���\�b�h�Ŏq��ʂ���σL�[���󂯎���Ă���
                '�������ꂽ�{�^���ɑΉ�����e�L�X�g�{�b�N�X�E���x���ɕ\��
                targetTxtBox.Text = _ZC910KahenKey
                targetLbl.Text = _ZC910Meisyo

                '�����\��
                If targetTxtBox.Equals(txtSeisaku) Then
                    '�W�J�敪�E�i�������敪�����\���A�ȖڃR�[�h�̎g�p�ېݒ�
                    Call txtSeisakuChange()
                ElseIf targetTxtBox.Equals(txtZaiko) Then
                    '��z�敪�E�����掩���\��
                    Call txtZaikoChange()
                ElseIf targetTxtBox.Equals(txtJuyousaki) Then
                    '�i��敪�R���{�̍č쐬�ƃ��x���̏�����
                    Call createCbo()
                ElseIf targetTxtBox.Equals(txtTenkaiKbn) Then
                    '�����W�J�w��H�������ݒ�
                    Call txtTenkaiKbnChange()
                End If
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@�W�v�Ώەi���R�[�h�ǉ��{�^�������C�x���g
    '�@(�����T�v)���͂��ꂽ�W�v�Ώەi���R�[�h�ƕi�����ꗗ�ɕ\������B
    '-------------------------------------------------------------------------------
    Private Sub btnTuika_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTuika.Click
        Try
            '�W�v�Ώەi���R�[�h���̓`�F�b�N
            Call checkInputSHinmeiCD()

            '�W�v�Ώەi���R�[�h�d���`�F�b�N
            Call checkKHinmeiCDRepeat(txtSSiyo.Text, txtSHinsyu.Text, txtSSensin.Text, txtSSize.Text, txtSColor.Text)

            '�ꗗ�\��
            Call dispDgv()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�W�v�Ώەi���R�[�h�폜�{�^�������C�x���g
    '�@(�����T�v)�ꗗ�őI�����ꂽ�s���폜����B
    '-------------------------------------------------------------------------------
    Private Sub btnSakujo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSakujo.Click
        Try
            '�폜�m�F�_�C�A���O�\��
            Dim rtn As DialogResult = _msgHd.dspMSG("confDeleteSTaisyohin")   '�I���s���ꗗ����폜���܂��B��낵���ł����H
            If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            '�ꗗ�I���s�폜
            Call deleteRowDgv()

            '�������b�Z�[�W
            Call _msgHd.dspMSG("completeDelete")          '�폜���������܂����B

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�o�^�{�^��(�폜�{�^��)�����C�x���g
    '�@(�����T�v)�V�K���[�h�̏ꍇ�͓o�^�A�폜���[�h�̏ꍇ�͍폜���s���B
    '-------------------------------------------------------------------------------
    Private Sub btnTouroku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTouroku.Click
        Try

            If _sinkiFlg Then
                '�V�K�o�^
                Try

                    '�i���R�[�h���̓`�F�b�N
                    Call checkInputHinmeiCD()

                    '�d���L�[�`�F�b�N
                    Call checkHinmeiCDRepeat()

                    '�L�[���ړ��̓`�F�b�N
                    Call checkInputKey()

                Catch ue As UsrDefException
                    ue.dspMsg()
                    Exit Sub
                End Try

                '�o�^�m�F�_�C�A���O�\��
                Dim rtn As DialogResult = _msgHd.dspMSG("confirmInsert")   '�o�^���܂��B��낵���ł����H
                If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If

                '�}�E�X�J�[�\�������v
                Dim cur As Cursor = Me.Cursor
                Me.Cursor = Cursors.WaitCursor
                Try

                    '�f�[�^�ǉ�
                    Call insertDB()

                Finally
                    '�}�E�X�J�[�\�����
                    Me.Cursor = cur
                End Try

                '�������b�Z�[�W
                Call _msgHd.dspMSG("completeInsert")          '�o�^���������܂����B

            Else
                '�폜
                Try
                    '�i���R�[�h���̓`�F�b�N
                    Call checkInputHinmeiCD()
                Catch ue As UsrDefException
                    ue.dspMsg()
                    Exit Sub
                End Try

                '�폜�m�F�_�C�A���O�\��
                Dim rtn As DialogResult = _msgHd.dspMSG("confirmDelete")   '�폜���܂��B��낵���ł����H
                If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If

                '-->2010.12.02 del by takagi
                ''�}�E�X�J�[�\�������v
                'Me.Cursor = Cursors.WaitCursor
                '
                ''�f�[�^�폜
                'Call deleteDB()
                '<--2010.12.02 del by takagi

                '�}�E�X�J�[�\�����
                Dim cur As Cursor = Me.Cursor
                Me.Cursor = Cursors.WaitCursor
                Try

                    '-->2010.12.02 add by takagi
                    '�f�[�^�폜
                    Call deleteDB()
                    '<--2010.12.02 add by takagi

                    '�������b�Z�[�W
                    Call _msgHd.dspMSG("completeDelete")          '�폜���������܂����B

                    '��ʏ�����
                    Call clearCtrDelMode()

                    '�����������̓R���g���[���������E�g�p��
                    txtSiyo.Focus()             '�d�l�R�[�h           
                    txtSiyo.Text = ""
                    txtSiyo.BackColor = StartUp.lCOLOR_WHITE
                    txtSiyo.ReadOnly = False
                    txtHinsyu.Text = ""         '�i��R�[�h
                    txtHinsyu.BackColor = StartUp.lCOLOR_WHITE
                    txtHinsyu.ReadOnly = False
                    txtSensin.Text = ""         '���S���R�[�h
                    txtSensin.BackColor = StartUp.lCOLOR_WHITE
                    txtSensin.ReadOnly = False
                    txtSize.Text = ""           '�T�C�Y�R�[�h
                    txtSize.BackColor = StartUp.lCOLOR_WHITE
                    txtSize.ReadOnly = False
                    txtColor.Text = ""          '�F�R�[�h
                    txtColor.ReadOnly = False
                    txtColor.BackColor = StartUp.lCOLOR_WHITE

                    btnKensaku.Enabled = True   '�����{�^���g�p��
                    btnCancel.Enabled = False   '�L�����Z���{�^���g�p�s��
                    btnTouroku.Enabled = False  '�폜�{�^���g�p�s��

                Finally
                    '�}�E�X�J�[�\�����
                    Me.Cursor = cur
                End Try

            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�߂�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnModoru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModoru.Click

        '���e�t�H�[���\��
        _parentForm.Show()
        _parentForm.Activate()
        Me.Close()

    End Sub

#End Region

#Region "���[�U��`�֐�:��ʐ���"

    '-------------------------------------------------------------------------------
    '�@��ʏ����ݒ�
    '-------------------------------------------------------------------------------
    Private Sub initForm()
        Try

            If _sinkiFlg Then
                '�V�K�o�^�̏ꍇ�̏���

                '�����l��ݒ肷��
                '' 2010/12/27 upd start sugano
                'lblSeisaku.Text = SYOKI_SEISEKI         '���я�
                lblSeiseki.Text = SYOKI_SEISEKI         '���я�
                '' 2010/12/27 upd end sugano
                lblMTantyo.Text = SYOKI_MTANTYO         '�����]���P����
                lblMLot.Text = SYOKI_MLOT               '�����]�����b�g��
                lblTTantyo.Text = SYOKI_TTANTYO         '����]���P����
                lblTLot.Text = SYOKI_TLOT               '����]�����b�g��
                lblSTantyo.Text = SYOKI_STANTYO         '�w��Ќ��]���P����
                lblSLot.Text = SYOKI_SLOT               '�w��Ќ��]�����b�g��
                lblTokki.Text = SYOKI_TOKKI             '���L����
                lblBikou.Text = SYOKI_BIKOU             '���l
                lblNyuko.Text = SYOKI_NYUUKO            '���ɖ{��
                lblTourokuFlg.Text = SYOKI_TOUROKUFLG   '�o�^�t���O
                lblHenko.Text = SYOKI_HENKOU            '�ύX���e

                '�q��ʂ��Ăׂ鍀�ڂ̏����l�̕\���ƁA����ɔ��������\���̐ݒ�
                Call setSyokiti()

                '�V�K�o�^���[�h���͌����{�^����\��
                btnKensaku.Visible = False

                '�L�����Z���{�^���E�o�^�{�^���͕i���\������܂Ŏg�p�s��
                btnCancel.Enabled = False
                btnTouroku.Enabled = False

                '��ʃ��[�h�\��
                lblJoutai.Text = SINKITOUROKU       '�V�K�o�^

                '�R���{�{�b�N�X�쐬
                Call createCbo()

            Else
                '�폜�̏ꍇ�̏���

                '�u�V�K�{�^���v�̖��O���u�폜�{�^���v�ɕύX����B
                btnTouroku.Text = "�폜(&E)"

                '�폜���[�h���͕i���\���{�^���g�p�s��
                btnHinmeiHyouji.Visible = False

                '�����E�߂�{�^���ȊO�̑S�Ă̋@�\���g�p�s�Ƃ���B
                Call deleteMode()

            End If

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@�폜���[�h�̉�ʕ\��
    '�@(�����T�v)�����p�R���g���[���ȊO���g�p�s�ɂ���B�e�L�X�g�{�b�N�X�͉��F�ɒ��F�B
    '-------------------------------------------------------------------------------
    Private Sub deleteMode()

        btnHinmeiHyouji.Enabled = False     '�i���\���{�^��
        txtSeisaku.ReadOnly = True          '����敪
        txtSeisaku.BackColor = StartUp.lCOLOR_YELLOW
        btnSeisaku.Enabled = False          '����敪�{�^��
        txtHyoujunLot.ReadOnly = True       '�W�����b�g��
        txtHyoujunLot.BackColor = StartUp.lCOLOR_YELLOW
        txtTantyou.ReadOnly = True          '�P��
        txtTantyou.BackColor = StartUp.lCOLOR_YELLOW
        txtSumiHonsu.ReadOnly = True        '�Z�d������
        txtSumiHonsu.BackColor = StartUp.lCOLOR_YELLOW
        txtZaiko.ReadOnly = True            '�݌ɁE�J��
        txtZaiko.BackColor = StartUp.lCOLOR_YELLOW
        btnZaiko.Enabled = False            '�݌ɁE�J�ԃ{�^��
        txtChumonsaki.Enabled = True        '������
        txtChumonsaki.ReadOnly = True
        txtJuyousaki.ReadOnly = True        '���v��
        txtJuyousaki.BackColor = StartUp.lCOLOR_YELLOW
        btnJuyousaki.Enabled = False        '���v��{�^��
        txtSizeTenkai.ReadOnly = True       '�T�C�Y�W�J
        txtSizeTenkai.BackColor = StartUp.lCOLOR_YELLOW
        btnSizeTenkai.Enabled = False       '�T�C�Y�W�J�{�^��
        cboHinsyuKbn.Enabled = False        '�i��敪
        cboHinsyuKbn.BackColor = StartUp.lCOLOR_YELLOW
        txtKijunTuki.ReadOnly = True        '��݌Ɍ���
        txtKijunTuki.BackColor = StartUp.lCOLOR_YELLOW
        txtSaigai.ReadOnly = True           '�ЊQ�����p�݌ɗ�
        txtSaigai.BackColor = StartUp.lCOLOR_YELLOW
        txtAnzenZ.ReadOnly = True           '���S�݌ɗ�
        txtAnzenZ.BackColor = StartUp.lCOLOR_YELLOW
        txtSSiyo.ReadOnly = True            '�W�v�Ώەi�@�d�l
        txtSSiyo.BackColor = StartUp.lCOLOR_YELLOW
        txtSHinsyu.ReadOnly = True          '�W�v�Ώەi�@�i��
        txtSHinsyu.BackColor = StartUp.lCOLOR_YELLOW
        txtSSensin.ReadOnly = True          '�W�v�Ώەi�@���S��
        txtSSensin.BackColor = StartUp.lCOLOR_YELLOW
        txtSSize.ReadOnly = True            '�W�v�Ώەi�@�T�C�Y
        txtSSize.BackColor = StartUp.lCOLOR_YELLOW
        txtSColor.ReadOnly = True           '�W�v�Ώەi�@�F
        txtSColor.BackColor = StartUp.lCOLOR_YELLOW
        btnTuika.Enabled = False            '�W�v�Ώەi���ǉ��{�^��
        btnSakujo.Enabled = False           '�W�v�Ώەi���폜�{�^��
        dgvSTaisyou.ReadOnly = True         '�ꗗ
        dgvSTaisyou.Enabled = False
        txtKamoku.Enabled = False           '�ȖڃR�[�h
        txtKamoku.BackColor = StartUp.lCOLOR_YELLOW
        txtMakiwaku.ReadOnly = True         '���g�R�[�h
        txtMakiwaku.BackColor = StartUp.lCOLOR_YELLOW
        txtHousou.ReadOnly = True           '��^�\���敪
        txtHousou.BackColor = StartUp.lCOLOR_YELLOW
        txtSiyousyo.ReadOnly = True         '�d�l���ԍ�
        txtSiyousyo.BackColor = StartUp.lCOLOR_YELLOW
        txtSeizouBmn.ReadOnly = True        '��������
        txtSeizouBmn.BackColor = StartUp.lCOLOR_YELLOW
        btnSeizouBmn.Enabled = False        '��������{�^��
        txtTenkaiKbn.ReadOnly = True        '�W�J�敪
        txtTenkaiKbn.BackColor = StartUp.lCOLOR_YELLOW
        btnTenkaiKbn.Enabled = False        '�W�J�敪�{�^��
        txtBTenkai.ReadOnly = True          '�����W�J�H��
        txtBTenkai.Enabled = True
        txtBTenkai.BackColor = StartUp.lCOLOR_YELLOW
        btnTenkaiKbn.Enabled = False        '�����W�J�H���{�^��
        txtHinsitu.ReadOnly = True          '�i�������敪
        txtHinsitu.BackColor = StartUp.lCOLOR_YELLOW
        btnHinsitu.Enabled = False          '�i�������敪�{�^��
        txtTatiai.ReadOnly = True           '����L��
        txtTatiai.BackColor = StartUp.lCOLOR_YELLOW
        btnTatiai.Enabled = False           '����L���{�^��
        btnTouroku.Enabled = False          '�폜�{�^��
        btnCancel.Visible = True            '�L�����Z���{�^��
        btnCancel.Enabled = False

        '��ʃ��[�h�\��
        lblJoutai.Text = SAKUJO             '�폜

    End Sub

    '-------------------------------------------------------------------------------
    '�@�e�R���g���[���̒l�N���A
    '�@(�����T�v)�����Ɏ��s�E��������蒼���ꍇ�ɁA�e�R���g���[���̒l���N���A����B
    '-------------------------------------------------------------------------------
    Private Sub clearCtrDelMode()
        Try
            txtSeisaku.Text = ""        '����敪
            lblSeisaku.Text = ""        '����敪���x��
            lblHinmei.Text = ""         '�i��
            txtHyoujunLot.Text = ""     '�W�����b�g��
            txtTantyou.Text = ""        '�P��
            lblJosu.Text = ""           '��
            lblKNDHonsu.Text = ""       '�k���{��
            txtSumiHonsu.Text = ""      '�Z�d������
            txtZaiko.Text = ""          '�݌ɁE�J��
            lblZaiko.Text = ""          '�݌ɁE�J�ԃ��x��
            txtChumonsaki.Text = ""     '�����惉�x��
            txtJuyousaki.Text = ""      '���v��
            lblJuyousaki.Text = ""      '���v�惉�x��
            lblABC.Text = ""            'ABC���x��
            txtSizeTenkai.Text = ""     '�T�C�Y�W�J
            lblSizeTenkai.Text = ""     '�T�C�Y�W�J���x��
            cboHinsyuKbn.Text = ""      '�i��敪
            lblHinsyuKbn.Text = ""      '�i��敪���x��
            txtKijunTuki.Text = ""      '��݌Ɍ���
            txtSaigai.Text = ""         '�ЊQ�����p�݌ɗ�
            txtAnzenZ.Text = ""         '���S�݌ɗ�
            txtKamoku.Text = ""         '�ȖڃR�[�h
            txtSSiyo.Text = ""          '�W�v�Ώەi�d�l�R�[�h
            txtSHinsyu.Text = ""        '�W�v�Ώەi�i��R�[�h
            txtSSensin.Text = ""        '�W�v�Ώەi���S���R�[�h
            txtSSize.Text = ""          '�W�v�Ώەi�T�C�Y�R�[�h
            txtSColor.Text = ""         '�W�v�Ώەi�F�R�[�h
            txtMakiwaku.Text = ""       '���g�R�[�h
            lblMakiwaku.Text = ""       '���g�R�[�h���x��
            txtHousou.Text = ""         '��^�\���敪
            lblHousou.Text = ""         '��^�\���敪���x��
            txtSiyousyo.Text = ""       '�d�l���ԍ�
            txtSeizouBmn.Text = ""      '��������
            lblSeizoBmn.Text = ""       '�������僉�x��
            lblTehaiKbn.Text = ""       '��z�敪
            lblTehaiKbnNM.Text = ""     '��z�敪���x��
            txtTenkaiKbn.Text = ""      '�W�J�敪
            lblTenkaiKbn.Text = ""      '�W�J�敪���x��
            txtBTenkai.Text = ""        '�����W�J�H�����x��
            txtHinsitu.Text = ""        '�i�������敪
            lblHinsitu.Text = ""        '�i�������敪���x��
            txtTatiai.Text = ""         '����L��
            lblTatiai.Text = ""         '����L�����x��
            lblSyoriKbn.Text = ""       '�����敪
            lblSyoriKbnNM.Text = ""     '�����敪��
            lblKako.Text = ""           '���H���v�Z�敪
            lblKakoNM.Text = ""         '���H���v�Z�敪��
            lblTantyo.Text = ""         '�P���敪
            lblTantyoNM.Text = ""       '�P���敪��
            lblSeiseki.Text = ""        '���я�
            lblMTantyo.Text = ""        '�����]���P����
            lblMLot.Text = ""           '�����]�����b�g��
            lblTTantyo.Text = ""        '����]���P����
            lblTLot.Text = ""           '����]�����b�g��
            lblSTantyo.Text = ""        '�w��Ќ��]���P����
            lblSLot.Text = ""           '�w��Ќ��]�����b�g��
            lblTokki.Text = ""          '���L����
            lblBikou.Text = ""          '���l
            lblNyuko.Text = ""          '���ɖ{��
            lblTourokuFlg.Text = ""     '�o�^�t���O
            lblHenko.Text = ""          '�ύX���e

            '�R���{�{�b�N�X�N���A
            Me.cboHinsyuKbn.Items.Clear()

            '�ꗗ�̃N���A
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSTaisyou)
            gh.clearRow()

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@����敪���͎�
    '�@(�����T�v)���͒l�ɑΉ����閼�̂����x���ɕ\�����A�W�J�敪�E�i�������敪�̒l�������\������B
    '-------------------------------------------------------------------------------
    Private Sub txtSeisaku_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSeisaku.Leave
        Try

            If "".Equals(txtSeisaku.Text) Then
                lblSeisaku.Text = ""
                Exit Sub
            End If

            '���x���\��
            Dim lblStr As String = serchName(txtSeisaku.Text, HKOTEIKEY_SEISAKU_KBN, lblSeisaku)
            If "".Equals(lblStr) Then
                If _sinkiFlg Then txtSeisaku.Focus()
                '-->2010.12.22 chg by takagi #38
                'Throw New UsrDefException("�ėp�}�X�^�ɓo�^����Ă��Ȃ��l�ł��B", _msgHd.getMSG("noExistHanyouMst", "�y����敪�z"))
                If _sinkiFlg Then Throw New UsrDefException("�ėp�}�X�^�ɓo�^����Ă��Ȃ��l�ł��B", _msgHd.getMSG("noExistHanyouMst", "�y����敪�z"))
                '<--2010.12.22 chg by takagi #38
            Else
                lblSeisaku.Text = lblStr
            End If

            If _sinkiFlg Then
                '�W�J�敪�E�i�������敪�����\���A�ȖڃR�[�h�̎g�p�ېݒ�
                Call txtSeisakuChange()
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�W�J�敪�E�i�������敪�̎����\���A�ȖڃR�[�h�̎g�p�ېݒ�
    '�@(�����T�v)���͒l�����ƂɓW�J�敪�E�i�������敪�̒l�������\������B
    '            �ȖڃR�[�h�̎g�p�ېݒ���s���B
    '-------------------------------------------------------------------------------
    Private Sub txtSeisakuChange()
        Try
            '����敪�̓��͓��e�ɂ���Ĉȉ��̃R���g���[���𐧌�
            If SEISAKUKBNCD_NAISAKU.Equals(txtSeisaku.Text) Then
                '�W�J�敪
                txtTenkaiKbn.Text = TENKAIKBNCD_ZEN
                '�i�������敪
                txtHinsitu.Text = HINSITUCD_LOTKANRI
                '�ȖڃR�[�h�@�g�p�s��
                txtKamoku.Text = ""
                txtKamoku.ReadOnly = True
                txtKamoku.Enabled = False
                txtKamoku.BackColor = StartUp.lCOLOR_YELLOW
            ElseIf SEISAKUKBNCD_GAITYU.Equals(txtSeisaku.Text) Then
                '�W�J�敪
                txtTenkaiKbn.Text = TENKAIKBNCD_BUBUN
                '�i�������敪
                txtHinsitu.Text = HINSITUCD_YOTYOUFUYO
                '�ȖڃR�[�h�@�g�p��
                txtKamoku.ReadOnly = False
                txtKamoku.Enabled = True
                txtKamoku.BackColor = StartUp.lCOLOR_WHITE
            Else
                Exit Sub
            End If

            '�����W�J�w��H�������ݒ�
            Call txtTenkaiKbn_Leave(txtTenkaiKbn, Nothing)

            '�i�������敪�����ݒ�
            Call txtInput_Leave(txtHinsitu, Nothing)

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@�݌ɁE�J�ԓ��͎�
    '�@(�����T�v)���͒l�ɑΉ����閼�̂����x���ɕ\�����A��z�敪�̒l�������\������B
    '-------------------------------------------------------------------------------
    Private Sub txtZaiko_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtZaiko.Leave
        Try

            If "".Equals(txtZaiko.Text) Then
                lblZaiko.Text = ""
                Exit Sub
            End If

            '���x���\��
            Dim lblStr As String = serchName(txtZaiko.Text, HKOTEIKEY_ZAIKO_KBN, lblZaiko)
            If "".Equals(lblStr) Then
                If _sinkiFlg Then txtZaiko.Focus()
                '-->2010.12.22 chg by takagi #38
                'Throw New UsrDefException("�ėp�}�X�^�ɓo�^����Ă��Ȃ��l�ł��B", _msgHd.getMSG("noExistHanyouMst", "�y�݌ɁE�J�ԁz"))
                If _sinkiFlg Then Throw New UsrDefException("�ėp�}�X�^�ɓo�^����Ă��Ȃ��l�ł��B", _msgHd.getMSG("noExistHanyouMst", "�y�݌ɁE�J�ԁz"))
                '<--2010.12.22 chg by takagi #38
            Else
                lblZaiko.Text = lblStr
            End If

            If ZAIKOCD_ZAIKO.Equals(txtZaiko.Text) Then
                '�v������͉�
                Call notEnableKeikakujouhou(True)
            Else
                '�v������͕s��
                Call notEnableKeikakujouhou(False)
            End If
            If _sinkiFlg Then
                '��z�敪�E�����掩���\��
                Call txtZaikoChange()
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�v������͕s��
    '�@(�����T�v)�݌ɁE�J�Ԃ��u1�v�ȊO�̏ꍇ�́A�v�������͕s�Ƃ���
    '-------------------------------------------------------------------------------
    Private Sub notEnableKeikakujouhou(ByVal prmZaikoInputFlg As Boolean)
        Try

            If _sinkiFlg Then
                Dim zaikoEnabled As Boolean = False         'Enabled�t���O
                Dim zaikoReadOnly As Boolean = False        'ReadOnly�t���O
                Dim backColor As Color                      '�w�i�F

                If prmZaikoInputFlg Then                    '�v������͉�
                    zaikoEnabled = True
                    zaikoReadOnly = False
                    backColor = StartUp.lCOLOR_WHITE
                Else                                        '�v������͕s��
                    zaikoEnabled = False
                    zaikoReadOnly = True
                    backColor = StartUp.lCOLOR_YELLOW
                End If

                txtJuyousaki.Enabled = zaikoEnabled         '���v�R�[�h
                txtJuyousaki.ReadOnly = zaikoReadOnly
                txtJuyousaki.BackColor = backColor
                btnJuyousaki.Enabled = zaikoEnabled         '���v�R�[�h�{�^��
                txtSizeTenkai.Enabled = zaikoEnabled        '�T�C�Y�W�J
                txtSizeTenkai.ReadOnly = zaikoReadOnly
                txtSizeTenkai.BackColor = backColor
                btnSizeTenkai.Enabled = zaikoEnabled        '�T�C�Y�W�J�{�^��
                cboHinsyuKbn.Enabled = zaikoEnabled         '�i��敪�R���{
                cboHinsyuKbn.BackColor = backColor
                txtKijunTuki.Enabled = zaikoEnabled         '�����
                txtKijunTuki.ReadOnly = zaikoReadOnly
                txtKijunTuki.BackColor = backColor
                txtSaigai.Enabled = zaikoEnabled            '�ЊQ�����p�݌�
                txtSaigai.ReadOnly = zaikoReadOnly
                txtSaigai.BackColor = backColor
                txtAnzenZ.Enabled = zaikoEnabled            '���S�݌ɗ�
                txtAnzenZ.ReadOnly = zaikoReadOnly
                txtAnzenZ.BackColor = backColor
                txtSSiyo.Enabled = zaikoEnabled             '�W�v�Ώەi�d�l�R�[�h
                txtSSiyo.ReadOnly = zaikoReadOnly
                txtSSiyo.BackColor = backColor
                txtSHinsyu.Enabled = zaikoEnabled           '�W�v�Ώەi�i��R�[�h
                txtSHinsyu.ReadOnly = zaikoReadOnly
                txtSHinsyu.BackColor = backColor
                txtSSensin.Enabled = zaikoEnabled           '�W�v�Ώەi���S���R�[�h
                txtSSensin.ReadOnly = zaikoReadOnly
                txtSSensin.BackColor = backColor
                txtSSize.Enabled = zaikoEnabled             '�W�v�Ώەi�T�C�Y�R�[�h
                txtSSize.ReadOnly = zaikoReadOnly
                txtSSize.BackColor = backColor
                txtSColor.Enabled = zaikoEnabled            '�W�v�Ώەi�F�R�[�h
                txtSColor.ReadOnly = zaikoReadOnly
                txtSColor.BackColor = backColor
                btnTuika.Enabled = zaikoEnabled             '�ꗗ�ǉ��{�^��
                btnSakujo.Enabled = zaikoEnabled            '�ꗗ�폜�{�^��
                dgvSTaisyou.Enabled = zaikoEnabled          '�W�v�Ώەi�ꗗ
                dgvSTaisyou.ReadOnly = zaikoReadOnly
                lblKensuu.Text = "0��"                      '�W�v�Ώەi�ꗗ����

                '���͕s�̏ꍇ�͓��͒l���N���A
                If Not prmZaikoInputFlg Then
                    txtJuyousaki.Text = ""              '���v�R�[�h
                    lblJuyousaki.Text = ""              '���v�R�[�h���x��
                    lblABC.Text = ""                    'ABC�敪
                    txtSizeTenkai.Text = ""             '�T�C�Y�W�J
                    lblSizeTenkai.Text = ""             '�T�C�Y�W�J���x��
                    Me.cboHinsyuKbn.Items.Clear()       '�i��敪�R���{
                    lblHinsyuKbn.Text = ""              '�i��敪���x��
                    txtKijunTuki.Text = ""              '�����
                    txtSaigai.Text = ""                 '�ЊQ�����p�݌�
                    txtAnzenZ.Text = ""                 '���S�݌ɗ�
                    txtSSiyo.Text = ""                  '�W�v�Ώەi�d�l�R�[�h
                    txtSHinsyu.Text = ""                '�W�v�Ώەi�i��R�[�h
                    txtSSensin.Text = ""                '�W�v�Ώەi���S���R�[�h
                    txtSSize.Text = ""                  '�W�v�Ώەi�T�C�Y�R�[�h
                    txtSColor.Text = ""                 '�W�v�Ώەi�F�R�[�h
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
    '�@��z�敪�E������̎����ݒ�
    '�@(�����T�v)���͒l�����ƂɎ�z�敪�E������������ݒ肷��B
    '-------------------------------------------------------------------------------
    Private Sub txtZaikoChange()
        Try

            If ZAIKOCD_ZAIKO.Equals(txtZaiko.Text) Then
                '��z�敪
                lblTehaiKbn.Text = "1"
                lblTehaiKbnNM.Text = "�݌�"

                '������
                txtChumonsaki.Text = ""
                txtChumonsaki.BackColor = StartUp.lCOLOR_YELLOW
                txtChumonsaki.ReadOnly = True
                txtChumonsaki.Enabled = False

                '�v������͉�
                Call notEnableKeikakujouhou(True)

            ElseIf ZAIKOCD_KURIKAESI.Equals(txtZaiko.Text) Then
                '��z�敪
                lblTehaiKbn.Text = "2"
                lblTehaiKbnNM.Text = "��"

                '������
                txtChumonsaki.BackColor = StartUp.lCOLOR_WHITE
                txtChumonsaki.ReadOnly = False
                txtChumonsaki.Enabled = True

                '�v������͕s��
                Call notEnableKeikakujouhou(False)
            Else
                Exit Sub
            End If

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@���v����͎�
    '�@(�����T�v)���͒l�ɑΉ����閼�̂����x���ɕ\�����A�i��敪�̃R���{�{�b�N�X���č쐬����B
    '-------------------------------------------------------------------------------
    Private Sub txtJuyousaki_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtJuyousaki.Leave
        Try

            If "".Equals(txtJuyousaki.Text) Then
                lblJuyousaki.Text = ""
                Exit Sub
            End If

            '���x���\��
            Dim lblStr As String = serchName(txtJuyousaki.Text, HKOTEIKEY_JUYOSAKI, lblJuyousaki)
            If "".Equals(lblStr) Then
                If _sinkiFlg Then txtJuyousaki.Focus()
                '-->2010.12.22 chg by takagi
                'Throw New UsrDefException("�ėp�}�X�^�ɓo�^����Ă��Ȃ��l�ł��B", _msgHd.getMSG("noExistHanyouMst", "�y���v��z"))
                If _sinkiFlg Then Throw New UsrDefException("�ėp�}�X�^�ɓo�^����Ă��Ȃ��l�ł��B", _msgHd.getMSG("noExistHanyouMst", "�y���v��z"))
                '<--2010.12.22 chg by takagi
            Else
                lblJuyousaki.Text = lblStr
            End If

            If _sinkiFlg Then
                '�i��敪�R���{�̍č쐬�ƃ��x���̏�����
                Call createCbo()
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�W�J�敪���͎�
    '�@(�����T�v)���͒l�ɑΉ����閼�̂����x���ɕ\�����A�����W�J�敪�������ݒ肷��B
    '-------------------------------------------------------------------------------
    Private Sub txtTenkaiKbn_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTenkaiKbn.Leave
        Try

            '��̏ꍇ�̓��x�����N���A���A�����W�J�敪���g�p�s�Ƃ���B
            If "".Equals(txtTenkaiKbn.Text) Then
                lblTenkaiKbn.Text = ""
                txtBTenkai.Text = ""
                txtBTenkai.Enabled = False
                txtBTenkai.BackColor = StartUp.lCOLOR_YELLOW
                Exit Sub
            End If

            '���x���\��
            Dim lblStr As String = serchName(txtTenkaiKbn.Text, HKOTEIKEY_TENKAI_KBN, lblTenkaiKbn)
            If "".Equals(lblStr) Then

                If _sinkiFlg Then txtTenkaiKbn.Focus()
                '-->2010.12.22 chg by takagi
                'Throw New UsrDefException("�ėp�}�X�^�ɓo�^����Ă��Ȃ��l�ł��B", _msgHd.getMSG("noExistHanyouMst", "�y�W�J�敪�z"))
                If _sinkiFlg Then Throw New UsrDefException("�ėp�}�X�^�ɓo�^����Ă��Ȃ��l�ł��B", _msgHd.getMSG("noExistHanyouMst", "�y�W�J�敪�z"))
                '<--2010.12.22 chg by takagi

            Else
                lblTenkaiKbn.Text = lblStr
            End If

            If _sinkiFlg Then
                '�����W�J�w��H�������ݒ�
                Call txtTenkaiKbnChange()
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�����W�J�w��H���̎����ݒ�
    '�@(�����T�v)���͒l�����Ƃɕ����W�J�w��H���������ݒ肷��B
    '-------------------------------------------------------------------------------
    Private Sub txtTenkaiKbnChange()
        Try
            If TENKAIKBNCD_ZEN.Equals(txtTenkaiKbn.Text) Then
                txtBTenkai.ReadOnly = True
                txtBTenkai.Enabled = False
                txtBTenkai.Text = ""
                txtBTenkai.BackColor = StartUp.lCOLOR_YELLOW
            ElseIf TENKAIKBNCD_BUBUN.Equals(txtTenkaiKbn.Text) Then
                txtBTenkai.ReadOnly = False
                txtBTenkai.BackColor = StartUp.lCOLOR_WHITE
                txtBTenkai.Enabled = True
            Else
                Exit Sub
            End If

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@���x�������\��(�V�K���[�h�p)
    '�@(�����T�v)�T�C�Y�W�J�E��������E�i�������敪�E����敪�̃e�L�X�g�{�b�N�X���͒l�ɑΉ�����
    '�@�@�@�@�@�@���̂����x���Ɏ����\������B
    '-------------------------------------------------------------------------------
    Private Sub txtInput_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSizeTenkai.Leave, _
                                                                                            txtSeizouBmn.Leave, _
                                                                                            txtHinsitu.Leave, _
                                                                                            txtTatiai.Leave
        Try

            '���͂���Ă��Ȃ��ꍇ�́A�Ή����郉�x�����N���A���ď����𔲂���
            If "".Equals(sender.Text) Then
                Select Case True
                    Case txtSizeTenkai.Equals(sender)
                        '�T�C�Y�W�J
                        lblSizeTenkai.Text = ""
                    Case txtSeizouBmn.Equals(sender)
                        '��������
                        lblSeizoBmn.Text = ""
                    Case txtHinsitu.Equals(sender)
                        '�i�������敪
                        lblHinsitu.Text = ""
                    Case txtTatiai.Equals(sender)
                        '����L��
                        lblTatiai.Text = ""
                End Select

                Exit Sub
            End If

            Dim targetTextBox As TextBox = sender
            Dim targetText As String = ""
            Dim koteikey As String = ""
            Dim targetLabel As Label = Nothing
            Dim errorMsg As String = ""

            '���x���\�������ɓn���ϐ���ݒ�
            Select Case True
                Case txtSizeTenkai.Equals(sender)
                    '�T�C�Y�W�J
                    targetText = txtSizeTenkai.Text
                    koteikey = HKOTEIKEY_SIZETENKAI_KBN
                    targetLabel = lblSizeTenkai
                    errorMsg = "�T�C�Y�W�J"

                    '��������
                Case txtSeizouBmn.Equals(sender)
                    targetText = txtSeizouBmn.Text
                    koteikey = HKOTEIKEY_SEIZO_BMN
                    targetLabel = lblSeizoBmn
                    errorMsg = "��������"

                    '�i�������敪
                Case txtHinsitu.Equals(sender)
                    targetText = txtHinsitu.Text
                    koteikey = HKOTEIKEY_HINSHITU_KBN
                    targetLabel = lblHinsitu
                    errorMsg = "�i�������敪"

                    '����L��
                Case txtTatiai.Equals(sender)
                    targetText = txtTatiai.Text
                    koteikey = HKOTEIKEY_TACHIAI_UM
                    targetLabel = lblTatiai
                    errorMsg = "����L��"
            End Select

            '���x���\��
            Dim lblStr As String = serchName(targetText, koteikey, targetLabel)
            If "".Equals(lblStr) Then
                If _sinkiFlg Then targetTextBox.Focus()
                '-->2010.12.22 chg by takagi
                'Throw New UsrDefException("�ėp�}�X�^�ɓo�^����Ă��Ȃ��l�ł��B", _msgHd.getMSG("noExistHanyouMst", "�y" & errorMsg & "�z"))
                If _sinkiFlg Then Throw New UsrDefException("�ėp�}�X�^�ɓo�^����Ă��Ȃ��l�ł��B", _msgHd.getMSG("noExistHanyouMst", "�y" & errorMsg & "�z"))
                '<--2010.12.22 chg by takagi
            Else
                targetLabel.Text = lblStr
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�i��敪���͎�
    '�@(�����T�v)���͒l�ɑΉ����閼�̂����x���ɕ\������B
    '-------------------------------------------------------------------------------
    Private Sub cboHinsyuKbn_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboHinsyuKbn.Leave
        Try
            '���x���\��
            Call dispCboLabel()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@���g�R�[�h���͎�
    '�@(�����T�v)���͒l�ɑΉ����閼�̂����x���ɕ\������B
    '-------------------------------------------------------------------------------
    Private Sub txtMakiwaku_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMakiwaku.Leave
        Try

            '���x���\��
            Call dispMakiwakuLabel()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@��^�\���敪���͎�
    '�@(�����T�v)���͒l�ɑΉ����閼�̂����x���ɕ\������B
    '-------------------------------------------------------------------------------
    Private Sub txtHousou_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtHousou.Leave
        Try

            '���x���\��
            Call dispHousouLabel()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    ' �@���ɖ{�� �Z�d�������ύX    
    '�@(�����T�v)�Z�d�������ɓ��͂��ꂽ�l�����ɁA�k���{�����Čv�Z���ĕ\������B
    '-------------------------------------------------------------------------------
    Private Sub txtSumiHonsu_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSumiHonsu.LostFocus
        Try

            '�𐔂�����ꍇ
            If Not "".Equals(lblJosu.Text) Then
                '�Z�d���������𐔂�菭�Ȃ��ꍇ
                If _db.rmNullInt(txtSumiHonsu.Text) <= _db.rmNullInt(lblJosu.Text) Then
                    '�k���{���������v�Z
                    lblKNDHonsu.Text = _db.rmNullInt(lblJosu.Text) - _db.rmNullInt(txtSumiHonsu.Text)
                End If

                '�Z�d�������������͂̏ꍇ�A0��\��
                If "".Equals(txtSumiHonsu.Text) Then
                    txtSumiHonsu.Text = "0"
                End If
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   �𐔂̎����v�Z
    ' �@(�����T�v)�W�����b�g���܂��͒P�����ύX���ꂽ�ꍇ�A�𐔂̎����v�Z���s���B
    '-------------------------------------------------------------------------------
    Private Sub numLotLen_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHyoujunLot.LostFocus, _
                                                                                                        txtTantyou.LostFocus
        Try

            '�W�����b�g���y�ђP���̗��������͂���Ă���ꍇ�̂ݏ������s���B
            If "".Equals(txtHyoujunLot.Text) Or "".Equals(txtTantyou.Text) Then
                Exit Sub
            End If

            '�𐔂̌v�Z
            If CInt(txtHyoujunLot.Text) Mod CInt(txtTantyou.Text) <> 0 Then
                '����؂�Ȃ��ꍇ�́u0�v�ɂ��Ă���
                lblJosu.Text = "0"
                Exit Sub
            End If
            lblJosu.Text = Format(CInt(txtHyoujunLot.Text) / CInt(txtTantyou.Text), "#,##0")

            '���ɖ{���ɏ𐔂̒l���Z�b�g����
            lblNyuko.Text = lblJosu.Text

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    ' �@�ėp�}�X�^�σL�[�̎󂯎��
    '-------------------------------------------------------------------------------
    Sub setKahenKey(ByVal prmKahenKey As String, ByVal prmMeisyo As String) Implements IfRturnKahenKey.setKahenKey
        Try

            _ZC910KahenKey = prmKahenKey
            _ZC910Meisyo = prmMeisyo

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

    '-------------------------------------------------------------------------------
    '�@�R���g���[���L�[�����C�x���g
    '�@(�����T�v)�G���^�[�{�^���������Ɏ��̃R���g���[���Ɉڂ�
    '-------------------------------------------------------------------------------
    Private Sub txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSiyo.KeyPress, _
                                                                                            txtHinsyu.KeyPress, _
                                                                                            txtSensin.KeyPress, _
                                                                                            txtSize.KeyPress, _
                                                                                            txtColor.KeyPress, _
                                                                                            txtSeisaku.KeyPress, _
                                                                                            txtHyoujunLot.KeyPress, _
                                                                                            txtTantyou.KeyPress, _
                                                                                            txtSumiHonsu.KeyPress, _
                                                                                            txtZaiko.KeyPress, _
                                                                                            txtJuyousaki.KeyPress, _
                                                                                            txtSizeTenkai.KeyPress, _
                                                                                            txtKijunTuki.KeyPress, _
                                                                                            txtSaigai.KeyPress, _
                                                                                            txtAnzenZ.KeyPress, _
                                                                                            txtSSiyo.KeyPress, _
                                                                                            txtSHinsyu.KeyPress, _
                                                                                            txtSSensin.KeyPress, _
                                                                                            txtSSize.KeyPress, _
                                                                                            txtSColor.KeyPress, _
                                                                                            txtKamoku.KeyPress, _
                                                                                            txtMakiwaku.KeyPress, _
                                                                                            txtHousou.KeyPress, _
                                                                                            txtSiyousyo.KeyPress, _
                                                                                            txtSeizouBmn.KeyPress, _
                                                                                            txtTenkaiKbn.KeyPress, _
                                                                                            txtBTenkai.KeyPress, _
                                                                                            txtHinsitu.KeyPress, _
                                                                                            txtTatiai.KeyPress, _
                                                                                            txtChumonsaki.KeyPress, _
                                                                                            cboHinsyuKbn.KeyPress

        UtilClass.moveNextFocus(Me, e) '���̃R���g���[���ֈړ����� 

    End Sub

    '-------------------------------------------------------------------------------
    '�@�R���g���[���S�I��
    '�@(�����T�v)�R���g���[���ړ����ɑS�I����Ԃɂ���
    '-------------------------------------------------------------------------------
    Private Sub serch_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSiyo.GotFocus, _
                                                                                            txtHinsyu.GotFocus, _
                                                                                            txtSensin.GotFocus, _
                                                                                            txtSize.GotFocus, _
                                                                                            txtColor.GotFocus, _
                                                                                            txtSeisaku.GotFocus, _
                                                                                            txtHyoujunLot.GotFocus, _
                                                                                            txtTantyou.GotFocus, _
                                                                                            txtSumiHonsu.GotFocus, _
                                                                                            txtZaiko.GotFocus, _
                                                                                            txtJuyousaki.GotFocus, _
                                                                                            txtSizeTenkai.GotFocus, _
                                                                                            txtKijunTuki.GotFocus, _
                                                                                            txtSaigai.GotFocus, _
                                                                                            txtAnzenZ.GotFocus, _
                                                                                            txtSSiyo.GotFocus, _
                                                                                            txtSHinsyu.GotFocus, _
                                                                                            txtSSensin.GotFocus, _
                                                                                            txtSSize.GotFocus, _
                                                                                            txtSColor.GotFocus, _
                                                                                            txtKamoku.GotFocus, _
                                                                                            txtMakiwaku.GotFocus, _
                                                                                            txtHousou.GotFocus, _
                                                                                            txtSiyousyo.GotFocus, _
                                                                                            txtSeizouBmn.GotFocus, _
                                                                                            txtTenkaiKbn.GotFocus, _
                                                                                            txtBTenkai.GotFocus, _
                                                                                            txtHinsitu.GotFocus, _
                                                                                            txtTatiai.GotFocus, _
                                                                                            cboHinsyuKbn.GotFocus


        UtilClass.selAll(sender)

    End Sub


#End Region

#Region "���[�U��`�֐�:DGV�֘A"

    '------------------------------------------------------------------------------------------------------
    '�I���s�ɒ��F���鏈��
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvSHinmei_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvSTaisyou.SelectionChanged
        If _colorCtlFlg Then
            Dim gh As UtilMDL.DataGridView.UtilDataGridViewHandler = New UtilMDL.DataGridView.UtilDataGridViewHandler(dgvSTaisyou)
            gh.setSelectionRowColor(dgvSTaisyou.CurrentCellAddress.Y, _oldRowIndex, StartUp.lCOLOR_BLUE)
        End If
        _oldRowIndex = dgvSTaisyou.CurrentCellAddress.Y
    End Sub

#End Region

#Region "���[�U��`�֐�:DB�֘A"

    '-------------------------------------------------------------------------------
    '�@ �폜�f�[�^�\��
    '   �i�����T�v�j�i���R�[�h�����Ƃ�M11�����M12�̃f�[�^����ʂɕ\������B
    '-------------------------------------------------------------------------------
    Private Sub dispData()
        Try

            '�d�l�R�[�h��1���̏ꍇ�́A���p�X�y�[�X��������2���ɂ���
            Dim siyoCd As String = _db.rmSQ(txtSiyo.Text)
            If _db.rmSQ(txtSiyo.Text).Length = 1 Then
                siyoCd = siyoCd & " "
            End If

            'M11
            Dim sql As String = ""
            sql = sql & N & " SELECT "
            sql = sql & N & "  TT_TANCYO " & DB_TANTYO              '�P��
            sql = sql & N & " ,TT_HINMEI " & DB_HINMEI              '�i��
            sql = sql & N & " ,TT_SYORI_KBN " & DB_SYORIKBN         '�����敪
            sql = sql & N & " ,TT_TEHAI_KBN " & DB_TEHAIKBN         '��z�敪
            sql = sql & N & " ,TT_SEISAKU_KBN " & DB_SEISAKU        '����敪
            sql = sql & N & " ,TT_KYAKSAKI " & DB_TYUMONSAKI        '������
            sql = sql & N & " ,TT_TENKAI_KBN " & DB_TENKAIKBN       '�W�J�敪
            sql = sql & N & " ,TT_KOUTEI " & DB_BUBUNTENKAI         '�����W�J�H��
            sql = sql & N & " ,TT_KEISAN_KBN " & DB_KAKOU           '���H���v�Z����
            sql = sql & N & " ,TT_TATIAI_UM " & DB_TATIAI           '����L��
            sql = sql & N & " ,TT_TANCYO_KBN " & DB_TANTYOKBN       '�P���敪
            sql = sql & N & " ,TT_MAKI_CD " & DB_MAKIWAKU           '���g
            sql = sql & N & " ,TT_HOSO_KBN " & DB_HOUSOU            '��^�\���敪
            sql = sql & N & " ,TT_HINSITU_KBN " & DB_HINSITU        '�i�������敪
            sql = sql & N & " ,TT_SIYOUSYO_NO " & DB_SIYOUSYO       '�d�l���ԍ�
            sql = sql & N & " ,TT_SEIZOU_BMN " & DB_SEIZOUBMN       '��������
            sql = sql & N & " ,TT_KAMOKU_CD " & DB_KAMOKU           '�ȖڃR�[�h
            sql = sql & N & " ,TT_N_SO_SUU " & DB_NYUUKO            '���ɖ{��
            sql = sql & N & " ,TT_N_K_SUU " & DB_KND                '�k���{��
            sql = sql & N & " ,TT_N_SH_SUU " & DB_SUMIDEN           '�Z�d������
            sql = sql & N & " ,TT_SEISEKI " & DB_SEISEKI            '���я�
            sql = sql & N & " ,TT_MYTANCYO " & DB_MTANTYO           '�����]���P����
            sql = sql & N & " ,TT_MYLOT " & DB_MLOT                 '�����]�����b�g��
            sql = sql & N & " ,TT_TYTANCYO " & DB_TTANTYO           '����]���P����
            sql = sql & N & " ,TT_TYLOT " & DB_TLOT                 '����]�����b�g��
            sql = sql & N & " ,TT_SYTANCYO " & DB_STANTYO           '�w��Ќ��]���P����
            sql = sql & N & " ,TT_SYLOT " & DB_SLOT                 '�w��Ќ��]�����b�g��
            sql = sql & N & " ,TT_TOKKI " & DB_TOKKI                '���L����
            sql = sql & N & " ,TT_BIKO " & DB_BIKOU                 '���l
            sql = sql & N & " ,TT_HENKO " & DB_HENKOU               '�ύX���e
            sql = sql & N & " ,TT_JYOSU " & DB_JOSU                 '��
            sql = sql & N & " ,TT_INSFLG " & DB_TOUROKU             '�o�^�t���O
            sql = sql & N & " ,TT_SYUBETU " & DB_ZAIKO              '�݌ɁE�J��
            sql = sql & N & " ,TT_LOT " & DB_LOTTYO                 '�W�����b�g��
            sql = sql & N & " ,TT_JUYOUCD " & DB_JUYOSAKI           '���v��
            sql = sql & N & " ,TT_ABCKBN " & DB_ABC                 'ABC
            sql = sql & N & " ,TT_HINSYUKBN " & DB_HINSYUKBN        '�i��敪
            sql = sql & N & " ,TT_TENKAIPTN " & DB_SIZETENKAI       '�T�C�Y�W�J
            sql = sql & N & " ,TT_KZAIKOTUKISU " & DB_KIJUNTUKI     '�����
            sql = sql & N & " ,TT_SFUKKYUU " & DB_SAIGAI            '�ЊQ�����p�݌ɗ�
            sql = sql & N & " ,TT_ANNZENZAIKO " & DB_ANZEN          '���S�݌ɗ�
            sql = sql & N & " FROM M11KEIKAKUHIN "
            sql = sql & N & " WHERE "

            'sql = sql & N & "      TT_H_SIYOU_CD = '" & siyoCd & "' AND "
            sql = sql & N & "      TT_H_SIYOU_CD = '" & siyoCd.PadRight(2, " ") & "' AND "

            sql = sql & N & "      TT_H_HIN_CD = '" & _db.rmSQ(txtHinsyu.Text) & "' AND "
            sql = sql & N & "      TT_H_SENSIN_CD = '" & _db.rmSQ(txtSensin.Text) & "' AND "
            sql = sql & N & "      TT_H_SIZE_CD = '" & _db.rmSQ(txtSize.Text) & "' AND "
            sql = sql & N & "      TT_H_COLOR_CD = '" & _db.rmSQ(txtColor.Text) & "'"

            'SQL���s
            Dim iRecCntM11 As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCntM11)

            If iRecCntM11 <= 0 Then         '���o���R�[�h���P�����Ȃ��ꍇ
                txtSiyo.Focus()
                Call clearCtrDelMode()      '�e�R���g���[���̃N���A
                Throw New UsrDefException("�i���R�[�h���v��Ώەi�}�X�^�ɓo�^����Ă��܂���B", _msgHd.getMSG("NonKTaisyohinMst"))
            End If

            'M12
            sql = ""
            sql = sql & N & "SELECT "
            sql = sql & N & " M12.HINMEICD " & COLDT_KHINMEICD
            sql = sql & N & ",(MPE.HINSYU_MEI "
            sql = sql & N & "		|| MPE.SAIZU_MEI"
            sql = sql & N & "		|| MPE.IRO_MEI) " & COLDT_KHINMEI
            sql = sql & N & " FROM M12SYUYAKU M12 "
            '2014/06/04 UPD-S Sugano
            'sql = sql & N & "	 INNER JOIN MPESEKKEI MPE ON"
            'sql = sql & N & "	 M12.KHINMEICD = "
            'sql = sql & N & "		(MPE.SHIYO "
            'sql = sql & N & "		|| LPAD(TO_CHAR(MPE.HINSYU)  ,3,'0')"
            'sql = sql & N & "		|| LPAD(TO_CHAR(MPE.SENSHIN)  ,3,'0')"
            'sql = sql & N & "		|| LPAD(TO_CHAR(MPE.SAIZU)  ,2,'0')"
            'sql = sql & N & "		|| LPAD(TO_CHAR(MPE.IRO)  ,3,'0'))"
            'sql = sql & N & " WHERE MPE.SHIYO = '" & siyoCd & "' AND "
            'sql = sql & N & "       MPE.HINSYU = '" & _db.rmSQ(txtHinsyu.Text) & "' AND "
            'sql = sql & N & "       MPE.SENSHIN = '" & _db.rmSQ(txtSensin.Text) & "' AND "
            'sql = sql & N & "       MPE.SAIZU = '" & _db.rmSQ(txtSize.Text) & "' AND "
            'sql = sql & N & "       MPE.IRO = '" & _db.rmSQ(txtColor.Text) & "' "
            'sql = sql & N & " AND MPE.SEQ_NO = " & INT_SEQNO
            'sql = sql & N & " AND MPE.SEKKEI_HUKA = "
            'sql = sql & N & "           (SELECT MAX(SEKKEI_HUKA) FROM MPESEKKEI "
            'sql = sql & N & "               WHERE SHIYO = '" & siyoCd & "' AND "
            'sql = sql & N & "                   HINSYU = '" & _db.rmSQ(txtHinsyu.Text) & "' AND "
            'sql = sql & N & "                   SENSHIN = '" & _db.rmSQ(txtSensin.Text) & "' AND "
            'sql = sql & N & "                   SAIZU = '" & _db.rmSQ(txtSize.Text) & "' AND "
            'sql = sql & N & "                   IRO = '" & _db.rmSQ(txtColor.Text) & "') AND "
            'sql = sql & N & " WHERE NOT M12.KHINMEICD = M12.HINMEICD "
            sql = sql & N & "	 INNER JOIN (SELECT M1.* FROM MPESEKKEI1 M1 "
            sql = sql & N & "	             INNER JOIN (SELECT SHIYO,HINSYU,SENSHIN,SAIZU,IRO,SEKKEI_FUKA,MAX(SEKKEI_KAITEI) KAITEI FROM MPESEKKEI1 WHERE SEKKEI_FUKA = 'A' "
            sql = sql & N & "	                         GROUP BY SHIYO,HINSYU,SENSHIN,SAIZU,IRO,SEKKEI_FUKA) M2 "
            sql = sql & N & "	             ON  M1.SHIYO = M2.SHIYO "
            sql = sql & N & "	             AND M1.HINSYU = M2.HINSYU "
            sql = sql & N & "	             AND M1.SENSHIN = M2.SENSHIN "
            sql = sql & N & "	             AND M1.SAIZU = M2.SAIZU "
            sql = sql & N & "	             AND M1.IRO = M2.IRO "
            sql = sql & N & "	             AND M1.SEKKEI_FUKA = M2.SEKKEI_FUKA "
            sql = sql & N & "	             AND M1.SEKKEI_KAITEI = M2.KAITEI ) MPE"
            sql = sql & N & " ON M12.HINMEICD = MPE.SHIYO || MPE.HINSYU || MPE.SENSHIN || MPE.SAIZU || MPE.IRO "
            sql = sql & N & " WHERE NOT M12.KHINMEICD = M12.HINMEICD "
            sql = sql & N & " AND M12.KHINMEICD = '" & siyoCd & _db.rmSQ(txtHinsyu.Text) & _db.rmSQ(txtSensin.Text) & _db.rmSQ(txtSize.Text) & _db.rmSQ(txtColor.Text) & "'"
            '2014/06/04 UPD-E Sugano

            'SQL���s
            Dim iRecCntM12 As Integer
            Dim dsM12 As DataSet = _db.selectDB(sql, RS, iRecCntM12)

            'M11�̃f�[�^���e�R���g���[���ɕ\��
            txtSeisaku.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_SEISAKU))             '����敪
            lblHinmei.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_HINMEI))               '�i��
            txtHyoujunLot.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_LOTTYO))           '�W�����b�g��
            txtTantyou.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_TANTYO))              '�P��
            lblJosu.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_JOSU))                   '��
            lblKNDHonsu.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_KND))                '�k���{��
            txtSumiHonsu.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_SUMIDEN))           '�Z�d������
            txtZaiko.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_ZAIKO))                 '�݌ɁE�J��
            txtChumonsaki.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_TYUMONSAKI))       '������
            txtJuyousaki.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_JUYOSAKI))          '���v��
            lblABC.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_ABC))                     'ABC
            txtSizeTenkai.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_SIZETENKAI))       '�T�C�Y�W�J

            Me.cboHinsyuKbn.Items.Clear()                                                  '�i��敪�R���{
            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(cboHinsyuKbn)

            '�R���{�{�b�N�X�ɃZ�b�g
            ch.addItem(New UtilCboVO(0, _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_HINSYUKBN))))
            ch.selectItem(0)

            txtKijunTuki.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_KIJUNTUKI))         '�����
            txtSaigai.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_SAIGAI))               '�ЊQ�����p�݌ɗ�
            txtAnzenZ.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_ANZEN))                '���S�݌ɗ�
            txtKamoku.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_KAMOKU))               '�ȖڃR�[�h
            txtMakiwaku.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_MAKIWAKU))           '���g�R�[�h
            txtHousou.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_HOUSOU))               '��^�\���敪
            txtSiyousyo.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_SIYOUSYO))           '�d�l���ԍ�
            txtSeizouBmn.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_SEIZOUBMN))         '��������
            lblTehaiKbn.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_TEHAIKBN))           '��z�敪
            txtBTenkai.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_BUBUNTENKAI))         '�W�J�敪
            txtTenkaiKbn.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_TENKAIKBN))         '�����W�J�H��
            txtHinsitu.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_HINSITU))             '�i�������敪
            txtTatiai.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_TATIAI))               '����L��
            lblSyoriKbn.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_SYORIKBN))           '�����敪
            lblKako.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_KAKOU))                  '���H���v�Z�敪
            lblTantyo.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_TANTYOKBN))            '�P���敪
            lblSeiseki.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_SEISEKI))             '���я�
            lblMTantyo.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_MTANTYO))             '�����]���P����
            lblMLot.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_MLOT))                   '�����]�����b�g��
            lblTTantyo.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_TTANTYO))             '����]���P����
            lblTLot.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_TLOT))                   '����]�����b�g��
            lblSTantyo.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_STANTYO))             '�w��Ќ��P����
            lblSLot.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_SLOT))                   '�w��Ќ����b�g��
            lblTokki.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_TOKKI))                 '���L����
            lblBikou.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_BIKOU))                 '���l
            lblNyuko.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_NYUUKO))                '���ɖ{��
            lblTourokuFlg.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_TOUROKU))          '�o�^�t���O
            lblHenko.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_HENKOU))                '�ύX���e

            '���o�f�[�^���ꗗ�ɕ\������
            dgvSTaisyou.DataSource = dsM12
            dgvSTaisyou.DataMember = RS

            '�ꗗ�E��Ɍ�����\��
            lblKensuu.Text = CStr(iRecCntM12) & "��"

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@ �i���\��
    '   �i�����T�v�j���͂��ꂽ�i���R�[�h�����Ƃɍޗ��[����i�����擾���ĉ�ʂɕ\������B
    '-------------------------------------------------------------------------------
    Private Sub dispHinmei()
        Try

            '�i���Ȃǂ��󂯎�邽�߂̕ϐ�
            Dim hinmei As String = ""
            Dim hinsyuNM As String = ""
            Dim sizeNM As String = ""
            Dim colorNM As String = ""
            Dim instance As ComBiz = New ComBiz(_db, _msgHd)
            '�ޗ��[����i������
            Call instance.getHinmeiFromZairyoMst(txtSiyo.Text, txtHinsyu.Text, txtSensin.Text, txtSize.Text, txtColor.Text, _
                                                                                       hinmei, hinsyuNM, sizeNM, colorNM)

            '�i�������Ȃ������ꍇ
            If "".Equals(hinmei) Then
                If _sinkiFlg Then
                    txtSiyo.Focus()
                    lblHinmei.Text = ""     '�i���̃N���A
                End If
                Throw New UsrDefException("�i���R�[�h���ޗ��[�}�X�^�ɓo�^����Ă��܂���B", _msgHd.getMSG("notExistZairyouMst"))
            End If

            'M11�ɓo�^����Ă���i���R�[�h�Əd�����Ă��Ȃ����`�F�b�N
            Call checkHinmeiCDRepeat()

            'M12�ɓo�^����Ă�����i���R�[�h�Əd�����Ă��Ȃ����`�F�b�N
            Call checkKHinmeiCDRepeat(txtSiyo.Text, txtHinsyu.Text, txtSensin.Text, txtSize.Text, txtColor.Text)

            '�擾�����f�[�^����ʂ̉B���R���g���[���Ɋi�[
            lblHinsyuNMHide.Text = hinsyuNM
            lblSizeNMHide.Text = sizeNM
            lblIroNMHide.Text = colorNM

            '�i�����x���ɕi����\��
            lblHinmei.Text = hinmei

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@ �i��敪�R���{�{�b�N�X�쐬
    '   �i�����T�v�j���v��ɓ��͂���Ă���l�����ƂɃR���{�{�b�N�X���쐬����B
    '-------------------------------------------------------------------------------
    Private Sub createCbo()
        Try

            '���v�悪��Ȃ珈�����s��Ȃ��B
            If "".Equals(txtJuyousaki.Text) Then
                Exit Sub
            End If

            Dim sql As String = ""
            sql = sql & N & " SELECT HINSYUKBN FROM M02HINSYUKBN "
            sql = sql & N & "   WHERE JUYOUCD = '" & _db.rmNullInt(txtJuyousaki.Text) & "'"

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                Exit Sub
            End If

            '�R���{�{�b�N�X�N���A
            Me.cboHinsyuKbn.Items.Clear()
            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(cboHinsyuKbn)

            '���[�v�����ăR���{�{�b�N�X�ɃZ�b�g
            For i As Integer = 0 To iRecCnt - 1
                ch.addItem(New UtilCboVO(i, _db.rmNullStr(ds.Tables(RS).Rows(i)("HINSYUKBN"))))
            Next

            '���x���̏�����
            lblHinsyuKbn.Text = ""

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@���x���̎����\��
    '�@(�����T�v)�R�[�h�I���q��ʑΉ��̃e�L�X�g�{�b�N�X�ɒl�����͂��ꂽ�ꍇ�A
    '�@�@�@�@�@�@���̒l�����ƂɃ��x���ɖ��̂�\������B
    '   �����̓p�����^  �FprmKahenkey   �ėp�}�X�^�σL�[
    '                     prmKoteikey   �ėp�}�X�^�Œ�L�[
    '                     prmLbl�@�@�@�@���ʕ\�����x��
    '   ���o�̓p�����^  �F�Ȃ�
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Function serchName(ByVal prmKahenkey As String, ByVal prmKoteikey As String, ByVal prmLbl As Label) As String

        Try

            Dim dtCol As String = "NAME"        '�G�C���A�X
            Dim col As String = "NAME1"         '�擾�����
            If prmKoteikey.Equals(HKOTEIKEY_JUYOSAKI) Then
                col = "NAME2"                   '���v��̂ݖ���2���擾����
            End If

            Dim sql As String = ""
            sql = sql & N & " SELECT " & col & " " & dtCol
            sql = sql & N & " FROM M01HANYO "
            sql = sql & N & "   WHERE KOTEIKEY = '" & prmKoteikey & "'"
            sql = sql & N & "   AND KAHENKEY = '" & prmKahenkey & "'"

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                Return ""
            Else
                Return _db.rmNullStr(ds.Tables(RS).Rows(0)(dtCol))
            End If

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Function

    '-------------------------------------------------------------------------------
    '�@�V�K�o�^���[�h�����l�ݒ�
    '�@�i�����T�v�j�V�K�o�^���[�h�ł̏����l��ėp�}�X�^����擾���A�e�R���g���[���ɕ\������B
    '�@�@�@�@�@�@�@�ΏۃR���g���[�� �F����敪(�W�J�敪�E�i�������敪�������\��)
    '                               �F���H���v�Z�敪
    '                               �F����敪
    '                               �F�P���敪
    '                               �F�݌ɋ敪(��z�敪�������\��)
    '                               �F�����敪
    '�@�@�����̓p�����[�^�F�Ȃ�
    '�@�@���֐��߂�l�@�@�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub setSyokiti()
        Try
            '�擾���郌�R�[�h�̌Œ�L�[���Ȃ��Č����p������ɂ���B
            Dim koteikey As String = ""
            koteikey = "('" & HKOTEIKEY_SEISAKU_KBN & "', '" & HKOTEIKEY_KAKOUCHO_KBN & "','" & HKOTEIKEY_TACHIAI_UM & "','" & _
                    HKOTEIKEY_TANCHO_KBN & "','" & HKOTEIKEY_ZAIKO_KBN & "','" & HKOTEIKEY_SYORI_KBN & "'"

            Dim sql As String = ""
            sql = sql & N & " SELECT "
            sql = sql & N & " KOTEIKEY, "
            sql = sql & N & " KAHENKEY, "
            sql = sql & N & " NAME1 "
            sql = sql & N & " FROM M01HANYO "
            sql = sql & N & " WHERE KOTEIKEY IN " & koteikey & ")"

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ

                Throw New UsrDefException("�ėp�}�X�^�ɓo�^����Ă��Ȃ��l�ł��B", _msgHd.getMSG("noExistHanyouMst"))
            End If

            '�e�R���g���[���ɏ����l��ݒ肵�Ă���
            For i As Integer = 0 To iRecCnt - 1
                '����敪
                If HKOTEIKEY_SEISAKU_KBN.Equals(_db.rmNullStr(ds.Tables(RS).Rows(i)("KOTEIKEY"))) And _
                        SYOKI_SEISAKUKBN.Equals(_db.rmNullStr(ds.Tables(RS).Rows(i)("KAHENKEY"))) Then
                    txtSeisaku.Text = _db.rmNullStr(ds.Tables(RS).Rows(i)("KAHENKEY"))
                    lblSeisaku.Text = _db.rmNullStr(ds.Tables(RS).Rows(i)("NAME1"))
                    '�W�J�敪�E�i�������敪�����\���A�ȖڃR�[�h�̎g�p�ېݒ�
                    Call txtSeisakuChange()

                    '���H���v�Z�敪
                ElseIf HKOTEIKEY_KAKOUCHO_KBN.Equals(_db.rmNullStr(ds.Tables(RS).Rows(i)("KOTEIKEY"))) And _
                        SYOKI_KAKOUKBN.Equals(ds.Tables(RS).Rows(i)("KAHENKEY")) Then
                    lblKako.Text = ds.Tables(RS).Rows(i)("KAHENKEY")
                    lblKakoNM.Text = ds.Tables(RS).Rows(i)("NAME1")

                    '����敪
                    ''2010/12/27 upd start sugano
                    'ElseIf HKOTEIKEY_TACHIAI_UM.Equals(_db.rmNullStr(_db.rmNullStr(ds.Tables(RS).Rows(i)("KOTEIKEY"))) And _
                    '        SYOKI_TACHIAIKBN.Equals(_db.rmNullStr(ds.Tables(RS).Rows(i)("KAHENKEY")))) Then
                ElseIf HKOTEIKEY_TACHIAI_UM.Equals(_db.rmNullStr(ds.Tables(RS).Rows(i)("KOTEIKEY"))) And _
                        SYOKI_TACHIAIKBN.Equals(_db.rmNullStr(ds.Tables(RS).Rows(i)("KAHENKEY"))) Then
                    ''2010/12/27 upd end sugano
                    txtTatiai.Text = ds.Tables(RS).Rows(i)("KAHENKEY")
                    lblTatiai.Text = ds.Tables(RS).Rows(i)("NAME1")

                    '�P���敪
                ElseIf HKOTEIKEY_TANCHO_KBN.Equals(_db.rmNullStr(ds.Tables(RS).Rows(i)("KOTEIKEY"))) And _
                        SYOKI_TANTYOKBN.Equals(_db.rmNullStr(ds.Tables(RS).Rows(i)("KAHENKEY"))) Then
                    lblTantyo.Text = _db.rmNullStr(ds.Tables(RS).Rows(i)("KAHENKEY"))
                    lblTantyoNM.Text = _db.rmNullStr(ds.Tables(RS).Rows(i)("NAME1"))

                    '�݌ɋ敪
                ElseIf HKOTEIKEY_ZAIKO_KBN.Equals(_db.rmNullStr(_db.rmNullStr(ds.Tables(RS).Rows(i)("KOTEIKEY"))) And _
                        SYOKI_ZAIKO.Equals(_db.rmNullStr(ds.Tables(RS).Rows(i)("KAHENKEY")))) Then
                    txtZaiko.Text = _db.rmNullStr(ds.Tables(RS).Rows(i)("KAHENKEY"))
                    lblZaiko.Text = _db.rmNullStr(ds.Tables(RS).Rows(i)("NAME1"))
                    '��z�敪�E�����掩���\��
                    Call txtZaikoChange()

                    '�����敪
                ElseIf HKOTEIKEY_SYORI_KBN.Equals(_db.rmNullStr(ds.Tables(RS).Rows(i)("KOTEIKEY"))) And _
                        SYOKI_SYORIKBN.Equals(_db.rmNullStr(ds.Tables(RS).Rows(i)("KAHENKEY"))) Then
                    lblSyoriKbn.Text = _db.rmNullStr(ds.Tables(RS).Rows(i)("KAHENKEY"))
                    lblSyoriKbnNM.Text = _db.rmNullStr(ds.Tables(RS).Rows(i)("NAME1"))
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
    '�@�i��敪���x���\��
    '�@�i�����T�v�j�i��敪�R���{�őI�����ꂽ�R�[�h�ɑΉ����閼�̂����x���ɕ\������B
    '�@�@�����̓p�����[�^�F�Ȃ�
    '�@�@���֐��߂�l�@�@�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub dispCboLabel()
        Try
            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(cboHinsyuKbn)

            '���v�您��уR���{�{�b�N�X�̗��������͂���Ă���ꍇ�̂ݏ������s���B
            If "".Equals(ch.getName) Or "".Equals(txtJuyousaki.Text) Then Exit Sub

            Dim sql = ""
            sql = sql & N & " SELECT "
            sql = sql & N & " HINSYUKBNNM "
            sql = sql & N & " FROM M02HINSYUKBN "
            sql = sql & N & "   WHERE JUYOUCD = '" & txtJuyousaki.Text & "'"
            sql = sql & N & "   AND HINSYUKBN = '" & ch.getName & "'"

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                '-->2010.12.22 chg by takagi
                'Throw New UsrDefException("�ėp�}�X�^�ɓo�^����Ă��Ȃ��l�ł��B", _msgHd.getMSG("noExistHanyouMst", "�y�i��敪�z"))
                If _sinkiFlg Then
                    Throw New UsrDefException("�ėp�}�X�^�ɓo�^����Ă��Ȃ��l�ł��B", _msgHd.getMSG("noExistHanyouMst", "�y�i��敪�z"))
                Else
                    lblHinsyuKbn.Text = ""
                    Exit Sub
                End If
                '<--2010.12.22 chg by takagi

            End If

            '���x���\��
            lblHinsyuKbn.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("HINSYUKBNNM"))

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@���g�R�[�h���x���\��
    '�@�i�����T�v�j���͂��ꂽ���g�R�[�h�ɑΉ����閼�̂����x���ɕ\������B
    '�@�@�����̓p�����[�^�F�Ȃ�
    '�@�@���֐��߂�l�@�@�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub dispMakiwakuLabel()
        Try

            '-->2010.12.24 chg by takagi
            'If "".Equals(txtMakiwaku.Text) Then Exit Sub
            If "".Equals(txtMakiwaku.Text) Then
                lblMakiwaku.Text = ""
                Exit Sub
            End If
            '<--2010.12.24 chg by takagi

            '-->2010.12.24 add by takagi
            If "999999".Equals(txtMakiwaku.Text) Then
                lblMakiwaku.Text = ""
                Exit Sub
            End If
            '<--2010.12.24 add by takagi

            Dim sql = ""
            sql = sql & N & " SELECT "
            sql = sql & N & " ZE_NAME "
            sql = sql & N & " FROM ZEASYCODE_TB "
            sql = sql & N & "   WHERE ZE_CODE = '" & txtMakiwaku.Text & "'"

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then            '���o���R�[�h���P�����Ȃ��ꍇ
                If _sinkiFlg Then
                    lblMakiwaku.Text = ""       '���x���N���A
                    txtMakiwaku.Focus()         '�t�H�[�J�X�ݒ�
                End If
                '-->2010.12.22 chg by takagi
                'Throw New UsrDefException("���g�����}�X�^�ɓo�^����Ă��܂���B", _msgHd.getMSG("noExistMakiwaku", "�y���g�R�[�h�z"))
                If _sinkiFlg Then
                    Throw New UsrDefException("���g�����}�X�^�ɓo�^����Ă��܂���B", _msgHd.getMSG("noExistMakiwaku", "�y���g�R�[�h�z"))
                Else
                    lblMakiwaku.Text = ""
                    Exit Sub
                End If
                '<--2010.12.22 chg by takagi

            End If

            '���x���\��
            lblMakiwaku.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("ZE_NAME"))

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@��^�\���敪���x���\��
    '�@�i�����T�v�j���͂��ꂽ���g�R�[�h�ɑΉ����閼�̂����x���ɕ\������B
    '�@�@�����̓p�����[�^�F�Ȃ�
    '�@�@���֐��߂�l�@�@�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub dispHousouLabel()
        Try

            '-->2010.12.24 chg by takagi
            'If "".Equals(txtHousou.Text) Then Exit Sub
            If "".Equals(txtHousou.Text) Then
                lblHousou.Text = ""
                Exit Sub
            End If
            '<--2010.12.24 chg by takagi

            Dim sql = ""
            sql = sql & N & " SELECT "
            sql = sql & N & " HN_NAME "
            sql = sql & N & " FROM HOSONAME_TB "
            sql = sql & N & "   WHERE HN_KUBUN = '" & txtHousou.Text & "'"

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then            '���o���R�[�h���P�����Ȃ��ꍇ
                If _sinkiFlg Then
                    lblHousou.Text = ""         '���x���N���A
                    txtHousou.Focus()           '�t�H�[�J�X�ݒ�
                End If
                '-->2010.12.22 chg by takagi
                'Throw New UsrDefException("��^�\����ނ��}�X�^�ɓo�^����Ă��܂���B", _msgHd.getMSG("noExistHousou", "�y��^�\����ށz"))
                If _sinkiFlg Then
                    Throw New UsrDefException("��^�\����ނ��}�X�^�ɓo�^����Ă��܂���B", _msgHd.getMSG("noExistHousou", "�y��^�\����ށz"))
                Else
                    lblHousou.Text = ""
                    Exit Sub
                End If
                '<--2010.12.22 chg by takagi

            End If

            '���x���\��
            lblHousou.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("HN_NAME"))


        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�ꗗ�\��
    '�@�i�����T�v�j���͂��ꂽ�W�v�Ώەi���R�[�h�Ƃ���ɑΉ����閼�̂��ꗗ�ɕ\������B
    '�@�@�����̓p�����[�^�F�Ȃ�
    '�@�@���֐��߂�l�@�@�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub dispDgv()
        Try

            '�W�v�Ώەi���R�[�h�̍쐬
            Dim sSiyo As String = txtSSiyo.Text
            If sSiyo.Length = 1 Then
                sSiyo = sSiyo & " "
            End If

            Dim sHinmeiCD As String = ""
            sHinmeiCD = sSiyo & txtSHinsyu.Text & txtSSensin.Text & txtSSize.Text & txtSColor.Text

            Dim SQL As String = ""
            SQL = SQL & N & " SELECT "
            SQL = SQL & N & " '" & sHinmeiCD & "'" & COLDT_SCODE
            SQL = SQL & N & " ,HINSYU_MEI || SAIZU_MEI || IRO_MEI " & COLDT_SNAME
            '2014/06/04 UPD-S Sugano
            'SQL = SQL & N & " FROM MPESEKKEI "
            SQL = SQL & N & " FROM MPESEKKEI1 "
            '2014/06/04 UPD-E Sugano
            SQL = SQL & N & "   WHERE SHIYO = '" & _db.rmSQ(txtSSiyo.Text) & "'  "
            SQL = SQL & N & "   AND HINSYU = '" & _db.rmSQ(txtSHinsyu.Text) & "'  "
            SQL = SQL & N & "   AND SENSHIN = '" & _db.rmSQ(txtSSensin.Text) & "'  "
            SQL = SQL & N & "   AND SAIZU = '" & _db.rmSQ(txtSSize.Text) & "'  "
            SQL = SQL & N & "   AND IRO = '" & _db.rmSQ(txtSColor.Text) & "' "
            '2014/06/04 UPD-S Sugano
            'SQL = SQL & N & "   AND SEQ_NO = " & INT_SEQNO
            'SQL = SQL & N & "   AND SEKKEI_HUKA = "
            'SQL = SQL & N & "           (SELECT MAX(SEKKEI_HUKA) FROM MPESEKKEI "
            SQL = SQL & N & "   AND SEKKEI_FUKA = 'A'"
            SQL = SQL & N & "   AND SEKKEI_KAITEI = (SELECT MAX(SEKKEI_KAITEI) FROM MPESEKKEI1 "
            '2014/06/04 UPD-E Sugano
            SQL = SQL & N & "               WHERE SHIYO = '" & _db.rmSQ(txtSSiyo.Text) & "'  "
            SQL = SQL & N & "               AND HINSYU = '" & _db.rmSQ(txtSHinsyu.Text) & "'  "
            SQL = SQL & N & "               AND SENSHIN = '" & _db.rmSQ(txtSSensin.Text) & "'  "
            SQL = SQL & N & "               AND SAIZU = '" & _db.rmSQ(txtSSize.Text) & "'  "
            '2014/06/04 UPD-S Sugano
            'SQL = SQL & N & "               AND IRO = '" & _db.rmSQ(txtSColor.Text) & "')  "
            SQL = SQL & N & "               AND IRO = '" & _db.rmSQ(txtSColor.Text) & "'  "
            SQL = SQL & N & "               AND SEKKEI_FUKA = 'A')"
            '2014/06/04 UPD-E Sugano

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(SQL, RS2, iRecCnt)

            If iRecCnt <= 0 Then            '���o���R�[�h���P�����Ȃ��ꍇ
                txtSSiyo.Focus()            '�t�H�[�J�X�ݒ�
                Throw New UsrDefException("�W�v�Ώەi���R�[�h���ޗ��[�}�X�^�ɓo�^����Ă��܂���B", _
                                                                _msgHd.getMSG("notExistSyukeiZairyouMst"))
            End If

            '�ǉ��s����
            Dim rowDt As Object() = {_db.rmNullStr(ds.Tables(RS2).Rows(0)(COLDT_SCODE)), _
                                            _db.rmNullStr(ds.Tables(RS2).Rows(0)(COLDT_SNAME))}

            '�ǉ�����R�[�h�ƈꗗ�ɕ\������Ă���R�[�h�̏d���`�F�b�N
            For i As Integer = 0 To dgvSTaisyou.RowCount - 1
                If _db.rmNullStr(ds.Tables(RS2).Rows(0)(COLDT_SCODE)).Equals(_db.rmNullStr(dgvSTaisyou(0, i).Value)) Then
                    txtSSiyo.Focus()
                    Throw New UsrDefException("���łɈꗗ�ɒǉ����ꂽ�W�v�Ώەi���R�[�h�ł��B", _
                                                            _msgHd.getMSG("alreadyAddSyukeiItiran"))
                End If
            Next

            '�O���b�h�Ƀo�C���h���ꂽ�f�[�^�Z�b�g�擾
            Dim wkDs As DataSet = dgvSTaisyou.DataSource

            '���̃f�[�^�Z�b�g�ɍs�ǉ�
            wkDs.Tables(RS).Rows.Add(rowDt)

            '�\�[�g����
            Dim dtTmp As DataTable = wkDs.Tables(RS).Clone()

            '�\�[�g���ꂽ�f�[�^�r���[�̍쐬
            Dim dv As DataView = New DataView(wkDs.Tables(RS))
            dv.Sort = COLDT_SCODE

            '�\�[�g���ꂽ���R�[�h�̃R�s�[
            For Each drv As DataRowView In dv
                dtTmp.ImportRow(drv.Row)
            Next

            '�o�C���h�p�f�[�^�Z�b�g����
            Dim bindDs As DataSet = New DataSet
            bindDs.Tables.Add(dtTmp)

            '�ăo�C���h
            dgvSTaisyou.DataSource = bindDs
            dgvSTaisyou.DataMember = RS

            '�ꗗ�̌�����\������
            lblKensuu.Text = CStr(dgvSTaisyou.RowCount) & "��"

            '�ǉ������s�Ƀt�H�[�J�X
            For c As Integer = 0 To dgvSTaisyou.RowCount - 1
                If _db.rmNullStr(ds.Tables(RS2).Rows(0)(COLDT_SCODE)).Equals(_db.rmNullStr(dgvSTaisyou(0, c).Value)) Then
                    Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSTaisyou)
                    dgvSTaisyou.Focus()
                    dgvSTaisyou.CurrentCell = dgvSTaisyou(0, c)
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
    '�@�ꗗ�s�폜
    '�@�i�����T�v�j�I�����ꂽ�s���ꗗ����폜����B
    '�@�@�����̓p�����[�^�F�Ȃ�
    '�@�@���֐��߂�l�@�@�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub deleteRowDgv()
        Try

            '�O���b�h�Ƀo�C���h���ꂽ�f�[�^�Z�b�g�擾
            Dim wkDs As DataSet = dgvSTaisyou.DataSource

            '�I���s�폜
            wkDs.Tables(RS).Rows.RemoveAt(dgvSTaisyou.CurrentCellAddress.Y)

            '�ăo�C���h
            dgvSTaisyou.DataSource = wkDs
            dgvSTaisyou.DataMember = RS

            '�ꗗ�̌�����\������
            lblKensuu.Text = CStr(dgvSTaisyou.RowCount) & "��"

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�o�^����
    '�@�i�����T�v�j���͂��ꂽ�l��DB�ɓo�^����B
    '�@�@�����̓p�����[�^�F�Ȃ�
    '�@�@���֐��߂�l�@�@�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub insertDB()
        Try
            '�d�l�R�[�h��1���̏ꍇ�́A���p�X�y�[�X��������
            Dim siyoCD As String = ""
            If _db.rmSQ(txtSiyo.Text).Length = 1 Then
                siyoCD = siyoCD & _db.rmSQ(txtSiyo.Text) & " "
            Else
                siyoCD = _db.rmSQ(txtSiyo.Text)
            End If

            '�v��i���R�[�h�쐬
            Dim kHinmei As String = ""
            kHinmei = siyoCD & _db.rmSQ(txtHinsyu.Text) & _db.rmSQ(txtSensin.Text) & _db.rmSQ(txtSize.Text) & _db.rmSQ(txtColor.Text)

            '�X�V�J�n�������擾
            Dim updStartDate As Date = Now

            '�g�����U�N�V�����J�n
            _db.beginTran()

            Dim sql As String = ""
            sql = sql & N & " INSERT INTO M11KEIKAKUHIN ("
            sql = sql & N & " TT_H_SIYOU_CD "            '�d�l�R�[�h
            sql = sql & N & " ,TT_H_HIN_CD "             '�i��R�[�h
            sql = sql & N & " ,TT_H_SENSIN_CD "          '���V���R�[�h
            sql = sql & N & " ,TT_H_SIZE_CD "            '�T�C�Y�R�[�h
            sql = sql & N & " ,TT_H_COLOR_CD "           '�F�R�[�h
            sql = sql & N & " ,TT_TANCYO "               '�P��
            sql = sql & N & " ,TT_FUKA_CD "              '�t���L��
            sql = sql & N & " ,TT_HINMEI "               '�i��
            sql = sql & N & " ,TT_TEHAI_SUU "            '��z����
            sql = sql & N & " ,TT_SYORI_KBN "            '�����敪
            sql = sql & N & " ,TT_TEHAI_KBN "            '��z�敪
            sql = sql & N & " ,TT_SEISAKU_KBN "          '����敪
            sql = sql & N & " ,TT_KYAKSAKI "             '������
            sql = sql & N & " ,TT_TENKAI_KBN "           '�W�J�敪
            sql = sql & N & " ,TT_KOUTEI "               '�����W�J�w��H��
            sql = sql & N & " ,TT_KEISAN_KBN "           '���H���v�Z�敪
            sql = sql & N & " ,TT_TATIAI_UM "            '����L��
            sql = sql & N & " ,TT_TANCYO_KBN "           '�P���敪
            sql = sql & N & " ,TT_MAKI_CD "              '���g�R�[�h
            sql = sql & N & " ,TT_HOSO_KBN "             '��^�\���敪
            sql = sql & N & " ,TT_HINSITU_KBN "          '�i�������敪
            sql = sql & N & " ,TT_SIYOUSYO_NO "          '�d�l���ԍ�
            sql = sql & N & " ,TT_SEIZOU_BMN "           '��������
            sql = sql & N & " ,TT_KAMOKU_CD "            '�ȖڃR�[�h
            sql = sql & N & " ,TT_N_SO_SUU "             '���ɖ{��
            sql = sql & N & " ,TT_N_K_SUU "              '�k���{�{��
            sql = sql & N & " ,TT_N_SH_SUU "             '�Z�d�����{��
            sql = sql & N & " ,TT_SEISEKI "              '���я�
            sql = sql & N & " ,TT_MYTANCYO "             '�����]���P����
            sql = sql & N & " ,TT_MYLOT "                '�����]�����b�g��
            sql = sql & N & " ,TT_TYTANCYO "             '����]���P����
            sql = sql & N & " ,TT_TYLOT "                '����]�����b�g��
            sql = sql & N & " ,TT_SYTANCYO "             '�w��Ќ��]���P����
            sql = sql & N & " ,TT_SYLOT "                '�w��Ќ��]�����b�g��
            sql = sql & N & " ,TT_TOKKI "                '���L����
            sql = sql & N & " ,TT_BIKO "                 '���l
            sql = sql & N & " ,TT_HENKO "                '�ύX���e
            sql = sql & N & " ,TT_JYOSU "                '��
            sql = sql & N & " ,TT_INSFLG "               '�o�^�t���O
            sql = sql & N & " ,TT_SYUBETU "              '�݌ɌJ��
            sql = sql & N & " ,TT_KHINMEICD "            '�v��i���R�[�h
            sql = sql & N & " ,TT_HINSYUNM "             '�i�햼
            sql = sql & N & " ,TT_SIZENM "               '�T�C�Y��
            sql = sql & N & " ,TT_IRONM "                '�F�x������
            sql = sql & N & " ,TT_LOT "                  '�W�����b�g��
            '�݌ɁE�J�Ԃ��u1�v�̏ꍇ�̂ݓo�^
            If txtZaiko.Text = ZAIKOCD_ZAIKO Then
                sql = sql & N & " ,TT_JUYOUCD "              '���v��
                sql = sql & N & " ,TT_ABCKBN "               '�`�a�b�敪
                sql = sql & N & " ,TT_HINSYUKBN "            '�i��敪
                sql = sql & N & " ,TT_TENKAIPTN "            '�T�C�Y�W�J�p�^�[��
                sql = sql & N & " ,TT_KZAIKOTUKISU "         '��݌Ɍ���
                sql = sql & N & " ,TT_SFUKKYUU "             '�ЊQ�����p�݌ɗ�
                sql = sql & N & " ,TT_ANNZENZAIKO "          '���S�݌ɗ�
            End If
            sql = sql & N & " ,TT_UPDNAME "              '�[��ID
            sql = sql & N & " ,TT_DATE )"                '�X�V����
            sql = sql & N & " VALUES ( "

            'sql = sql & N & " '" & siyoCD & "', "                                                   '�d�l�R�[�h
            sql = sql & N & " '" & siyoCD.PadRight(2, " ") & "', "                                                   '�d�l�R�[�h

            sql = sql & N & " '" & _db.rmSQ(txtHinsyu.Text) & "', "                                 '�i��R�[�h
            sql = sql & N & " '" & _db.rmSQ(txtSensin.Text) & "', "                                 '���V���R�[�h
            sql = sql & N & " '" & _db.rmSQ(txtSize.Text) & "', "                                   '�T�C�Y�R�[�h
            sql = sql & N & " '" & _db.rmSQ(txtColor.Text) & "', "                                  '�F�R�[�h
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(txtTantyou.Text) & "', '" & NUMBER & "'), "   '�P��
            sql = sql & N & " '" & _db.rmSQ(lblFuka.Text) & "', "                                   '�t���L��
            sql = sql & N & " '" & _db.rmSQ(lblHinmei.Text) & "', "                                 '�i��
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(txtTantyou.Text) & "', '" & NUMBER & _
                        "') * TO_NUMBER('" & _db.rmSQ(lblJosu.Text) & "', '" & NUMBER & "'), "      '�W�����b�g���~�P��
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(lblSyoriKbn.Text) & "'), "                    '�����敪
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(lblTehaiKbn.Text) & "'), "                    '��z�敪
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(txtSeisaku.Text) & "'), "                     '����敪
            sql = sql & N & " " & returnNull(txtChumonsaki) & ", "                                  '������
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(txtTenkaiKbn.Text) & "'), "                   '�W�J�敪
            sql = sql & N & " " & returnNull(txtBTenkai) & ", "                                     '�����W�J�w��H��
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(lblKako.Text) & "'), "                        '���H���v�Z�敪
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(txtTatiai.Text) & "'), "                      '����L��
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(lblTantyo.Text) & "'), "                      '�P���敪
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(txtMakiwaku.Text) & "'), "                    '���g�R�[�h
            sql = sql & N & " '" & _db.rmSQ(txtHousou.Text) & "', "                                 '��^�\���敪
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(txtHinsitu.Text) & "'), "                     '�i�������敪
            sql = sql & N & " '" & _db.rmSQ(txtSiyousyo.Text) & "', "                               '�d�l���ԍ�
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(txtSeizouBmn.Text) & "'), "                   '��������
            sql = sql & N & " TO_NUMBER(" & returnNull(txtKamoku) & "), "                           '�ȖڃR�[�h
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(lblNyuko.Text) & "', '" & NUMBER & "'), "     '���ɖ{��
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(lblKNDHonsu.Text) & "', '" & NUMBER & "'), "  '�k���{�{��
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(txtSumiHonsu.Text) & "', '" & NUMBER & "'), " '�Z�d�����{��
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(lblSeiseki.Text) & "'), "                     '���я�
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(lblMTantyo.Text) & "', '" & NUMBER & "'), "   '�����]���P����
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(lblMLot.Text) & "', '" & NUMBER & "'), "      '�����]�����b�g��
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(lblTTantyo.Text) & "', '" & NUMBER & "'), "   '����]���P����
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(lblTLot.Text) & "', '" & NUMBER & "'), "      '����]�����b�g��
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(lblSTantyo.Text) & "', '" & NUMBER & "'), "   '�w��Ќ��]���P����
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(lblSLot.Text) & "', '" & NUMBER & "'), "      '�w��Ќ��]�����b�g��
            sql = sql & N & " " & returnNull(lblTokki) & ", "                                       '���L����
            sql = sql & N & " " & returnNull(lblBikou) & ", "                                       '���l
            sql = sql & N & " " & returnNull(lblHenko) & ", "                                       '�ύX���e
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(lblJosu.Text) & "'), "                        '��
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(lblTourokuFlg.Text) & "'), "                  '�o�^�t���O
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(txtZaiko.Text) & "'), "                       '�݌ɌJ��
            sql = sql & N & " '" & kHinmei & "', "                                                  '�v��i���R�[�h
            sql = sql & N & " '" & _db.rmSQ(lblHinsyuNMHide.Text) & "', "                           '�i�햼
            sql = sql & N & " '" & _db.rmSQ(lblSizeNMHide.Text) & "', "                             '�T�C�Y��
            sql = sql & N & " '" & _db.rmSQ(lblIroNMHide.Text) & "', "                              '�F�x������
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(txtHyoujunLot.Text) & "','" & NUMBER & "'), " '�W�����b�g��
            '�݌ɁE�J�Ԃ��u1�v�̏ꍇ�̂ݓo�^
            If txtZaiko.Text = ZAIKOCD_ZAIKO Then
                sql = sql & N & " " & returnNull(txtJuyousaki) & ", "                                   '���v��
                sql = sql & N & " " & returnNull(lblABC) & ", "                                         '�`�a�b�敪
                sql = sql & N & " '" & cboHinsyuKbn.Text & "', "                                        '�i��敪
                sql = sql & N & " '" & txtSizeTenkai.Text & "', "                                       '�T�C�Y�W�J�p�^�[��
                sql = sql & N & " TO_NUMBER('" & _db.rmSQ(txtKijunTuki.Text) & "', '" & NUMBER & "'), " '��݌Ɍ���
                sql = sql & N & " TO_NUMBER('" & _db.rmSQ(txtSaigai.Text) & "', '" & NUMBER & "'), "    '�ЊQ�����p�݌ɗ�
                sql = sql & N & " TO_NUMBER('" & _db.rmSQ(txtAnzenZ.Text) & "', '" & NUMBER & "'), "    '���S�݌ɗ�            
            End If
            sql = sql & N & " '" & _tanmatuID & "', "                                               '�[��ID
            sql = sql & N & " TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS')) "           '�X�V����

            Dim updCnt As Integer = 0
            _db.executeDB(sql, updCnt)

            'M12 �e�R�[�h�̓o�^
            sql = ""
            sql = sql & N & " INSERT INTO M12SYUYAKU ("
            sql = sql & N & "  HINMEICD "
            sql = sql & N & " ,KHINMEICD "
            sql = sql & N & " ,UPDNAME "
            sql = sql & N & " ,UPDDATE) "
            sql = sql & N & " VALUES ( "
            sql = sql & N & "   '" & kHinmei & "', "
            sql = sql & N & "   '" & kHinmei & "', "
            sql = sql & N & " '" & _tanmatuID & "', "                                       '�[��ID
            sql = sql & N & " TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS')) "   '�X�V����
            _db.executeDB(sql)

            'M12 �ꗗ�ɕ\������Ă���v��i���R�[�h�̓o�^
            For loopCnt As Integer = 0 To dgvSTaisyou.RowCount - 1

                sql = ""
                sql = sql & N & " INSERT INTO M12SYUYAKU ("
                sql = sql & N & "  HINMEICD "
                sql = sql & N & " ,KHINMEICD "
                sql = sql & N & " ,UPDNAME "
                sql = sql & N & " ,UPDDATE) "
                sql = sql & N & " VALUES ( "
                sql = sql & N & "   '" & _db.rmSQ(dgvSTaisyou(0, loopCnt).Value) & "', "
                sql = sql & N & "   '" & kHinmei & "', "
                sql = sql & N & " '" & _tanmatuID & "', "                                       '�[��ID
                sql = sql & N & " TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS')) "   '�X�V����
                _db.executeDB(sql)
            Next

            '�X�V�I���������擾
            Dim updFinDate As Date = Now

            'T91���s����o�^����
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
            '-->2010.12.02 upd by takagi
            'sql = sql & N & "  '" & PGID & "'"                                              '�@�\ID
            sql = sql & N & "  '" & _pgId & "'"                                              '�@�\ID
            '<--2010.12.02 upd by takagi
            sql = sql & N & ", NULL "
            sql = sql & N & ", NULL "
            sql = sql & N & ", TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "   '�����J�n����
            sql = sql & N & ", TO_DATE('" & updFinDate & "', 'YYYY/MM/DD HH24:MI:SS') "     '�����I������
            sql = sql & N & ", 0 "                                                          '�����P�i�폜�����j"
            sql = sql & N & ", " & updCnt                                                   '�����Q�i�o�^�����j
            sql = sql & N & ", '" & _tanmatuID & "'"                                        '�[��ID
            sql = sql & N & ", TO_DATE('" & updFinDate & "', 'YYYY/MM/DD HH24:MI:SS') "     '�X�V����
            sql = sql & N & " )"
            _db.executeDB(sql)

            'T02��������e�[�u���X�V
            '-->2010.12.02 upd by takagi
            '_parentForm.updateSeigyoTbl(PGID, True, updStartDate, updFinDate)
            _parentForm.updateSeigyoTbl(_pgId, True, updStartDate, updFinDate)
            '<--2010.12.02 upd by takagi

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

    '-------------------------------------------------------------------------------
    '�@�폜����
    '�@�i�����T�v�j���͂��ꂽ�i���R�[�h�����Ƃ�DB�̃��R�[�h���폜����B
    '�@�@�����̓p�����[�^�F�Ȃ�
    '�@�@���֐��߂�l�@�@�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub deleteDB()
        Try

            '�X�V�J�n�������擾
            Dim updStartDate As Date = Now

            '�d�l�R�[�h��1���̏ꍇ�́A���p�X�y�[�X��������2���ɂ���
            Dim siyoCd As String = _db.rmSQ(txtSiyo.Text)
            If _db.rmSQ(txtSiyo.Text).Length = 1 Then
                siyoCd = siyoCd & " "
            End If

            '�g�����U�N�V�����J�n
            _db.beginTran()

            'M11
            Dim sql As String = ""
            sql = sql & N & " DELETE FROM M11KEIKAKUHIN "
            sql = sql & N & "   WHERE "

            'sql = sql & N & "       TT_H_SIYOU_CD = '" & _db.rmSQ(siyoCd) & "'"       '�d�l�R�[�h
            sql = sql & N & "       TT_H_SIYOU_CD = '" & _db.rmSQ(siyoCd.PadRight(2, " ")) & "'"       '�d�l�R�[�h

            sql = sql & N & "   AND TT_H_HIN_CD = '" & _db.rmSQ(txtHinsyu.Text) & "'"       '�i��R�[�h
            sql = sql & N & "   AND TT_H_SENSIN_CD = '" & _db.rmSQ(txtSensin.Text) & "'"    '���V���R�[�h
            sql = sql & N & "   AND TT_H_SIZE_CD = '" & _db.rmSQ(txtSize.Text) & "'"        '�T�C�Y�R�[�h
            sql = sql & N & "   AND TT_H_COLOR_CD = '" & _db.rmSQ(txtColor.Text) & "'"      '�F�R�[�h

            '�폜�����ێ��ϐ�
            Dim delCnt As Integer = 0
            _db.executeDB(sql, delCnt)

            '�i���R�[�h������
            Dim kHinmeiCD As String = ""
            kHinmeiCD = siyoCd & _db.rmSQ(txtHinsyu.Text) & _db.rmSQ(txtSensin.Text) & _
                                                _db.rmSQ(txtSize.Text) & _db.rmSQ(txtColor.Text)

            'M12
            sql = ""
            sql = sql & N & " DELETE FROM M12SYUYAKU "
            sql = sql & N & "   WHERE "
            sql = sql & N & "       KHINMEICD = '" & kHinmeiCD & "'"

            _db.executeDB(sql, delCnt)

            '�X�V�I���������擾
            Dim updFinDate As Date = Now

            'T91���s����o�^����
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
            '-->2010.12.02 upd by takagi
            'sql = sql & N & "  '" & PGID & "'"                                              '�@�\ID
            sql = sql & N & "  '" & _pgId & "'"                                              '�@�\ID
            '<--2010.12.02 upd by takagi
            sql = sql & N & ", NULL "
            sql = sql & N & ", NULL "
            sql = sql & N & ", TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "   '�����J�n����
            sql = sql & N & ", TO_DATE('" & updFinDate & "', 'YYYY/MM/DD HH24:MI:SS') "     '�����I������
            sql = sql & N & ", " & delCnt                                                   '�����P�i�폜�����j"
            sql = sql & N & ", 0 "                                                          '�����Q�i�o�^�����j
            sql = sql & N & ", '" & _tanmatuID & "'"                                        '�[��ID
            sql = sql & N & ", TO_DATE('" & updFinDate & "', 'YYYY/MM/DD HH24:MI:SS') "     '�X�V����
            sql = sql & N & " )"
            _db.executeDB(sql)

            'T02��������e�[�u���X�V
            '-->2010.12.02 upd by takagi
            '_parentForm.updateSeigyoTbl(PGID, True, updStartDate, updFinDate)
            _parentForm.updateSeigyoTbl(_pgId, True, updStartDate, updFinDate)
            '<--2010.12.02 upd by takagi

            '�g�����U�N�V�����I��
            _db.commitTran()

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�R���g���[�������͔���
    '�@�i�����T�v�j�n���ꂽ�R���g���[���������͂��ǂ����𔻒肷��B
    '�@�@�����̓p�����[�^�F�e�R���g���[��
    '�@�@���֐��߂�l�@�@�FSQL��
    '-------------------------------------------------------------------------------
    Private Function returnNull(ByVal prmControl As Object) As String
        Try

            returnNull = " " & "NULL"

            If TypeOf prmControl Is TextBox Then
                '�e�L�X�g�{�b�N�X�p
                If "".Equals(CType(prmControl, TextBox).Text) Then
                    returnNull = " " & "NULL"
                Else
                    returnNull = " '" & _db.rmSQ(prmControl.Text) & "' "
                End If
            ElseIf TypeOf prmControl Is Label Then
                '���x���p
                If "".Equals(CType(prmControl, Label).Text) Then
                    returnNull = " " & "NULL"
                Else
                    returnNull = " '" & _db.rmSQ(prmControl.Text) & "' "
                End If
            End If

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Function

    '-------------------------------------------------------------------------------
    '�@�ėp�}�X�^���̎擾(�폜���[�h�p)
    '�@�i�����T�v�j�����{�^��������A�n���ꂽ�l�����Ƃɔėp�}�X�^���疼��1���擾����B
    '�@�@�����̓p�����[�^�F prmKoteiKey�@�Œ�L�[
    '                    �F prmKahenkey�@�σL�[
    '�@�@���֐��߂�l�@�@�F ����1
    '-------------------------------------------------------------------------------
    Private Function serchHanyoMst(ByVal prmKoteiKey As String, ByVal prmKahenkey As String)
        Try

            Dim ret As String = ""

            Dim sql As String = ""
            sql = sql & N & " SELECT "
            sql = sql & N & " NAME1 "
            sql = sql & N & " FROM M01HANYO "
            sql = sql & N & " WHERE KOTEIKEY = '" & _db.rmSQ(prmKoteiKey) & "'"
            sql = sql & N & " AND KAHENKEY = '" & _db.rmSQ(prmKahenkey) & "'"

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then            '���o���R�[�h���P�����Ȃ��ꍇ
                Return ret
                Exit Function
            End If

            ret = _db.rmNullStr(ds.Tables(RS).Rows(0)("NAME1"))
            Return ret

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Function

#End Region

#Region "���[�U��`�֐�:�`�F�b�N����"

    '-------------------------------------------------------------------------------
    '�@ �i���R�[�h�`�F�b�N
    '   �i�����T�v�j�i���R�[�h�����͂���Ă��邩�`�F�b�N����
    '-------------------------------------------------------------------------------
    Private Sub checkInputHinmeiCD()
        Try

            If "".Equals(txtSiyo.Text) Then
                txtSiyo.Focus()
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y�d�l�R�[�h�z"))
            ElseIf "".Equals(txtHinsyu.Text) Then
                txtHinsyu.Focus()
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y�i��R�[�h�z"))
            ElseIf "".Equals(txtSensin.Text) Then
                txtSensin.Focus()
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y���S���R�[�h�z"))
            ElseIf "".Equals(txtSize.Text) Then
                txtSize.Focus()
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y�T�C�Y�R�[�h�z"))
            ElseIf "".Equals(txtColor.Text) Then
                txtColor.Focus()
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y�F�R�[�h�z"))
            End If

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@ �W�v�Ώەi���R�[�h�`�F�b�N
    '   �i�����T�v�j�W�v�Ώەi���R�[�h�����͂���Ă��邩�`�F�b�N����
    '-------------------------------------------------------------------------------
    Private Sub checkInputSHinmeiCD()
        Try

            If "".Equals(txtSSiyo.Text) Then
                txtSSiyo.Focus()
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y�W�v�Ώەi�d�l�R�[�h�z"))
            ElseIf "".Equals(txtSHinsyu.Text) Then
                txtSHinsyu.Focus()
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y�W�v�Ώەi�i��R�[�h�z"))
            ElseIf "".Equals(txtSSensin.Text) Then
                txtSSensin.Focus()
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y�W�v�Ώەi���S���R�[�h�z"))
            ElseIf "".Equals(txtSSize.Text) Then
                txtSSize.Focus()
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y�W�v�Ώەi�T�C�Y�R�[�h�z"))
            ElseIf "".Equals(txtSColor.Text) Then
                txtSColor.Focus()
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y�W�v�Ώەi�F�R�[�h�z"))
            End If

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@ �i���d���`�F�b�N
    '   �i�����T�v�j�v��Ώەi�}�X�^���������A�i���R�[�h�̏d�����Ȃ����`�F�b�N����
    '-------------------------------------------------------------------------------
    Private Sub checkHinmeiCDRepeat()
        Try

            '�d�l�R�[�h��1���̏ꍇ�͌�ɔ��p�X�y�[�X��������
            Dim siyoCD As String = txtSiyo.Text
            If txtSiyo.Text.Length <= 1 Then
                siyoCD = siyoCD & " "
            End If

            'M11
            Dim sql As String = ""
            sql = ""
            sql = sql & N & " SELECT "
            sql = sql & N & " * "
            sql = sql & N & " FROM M11KEIKAKUHIN "

            'sql = sql & N & "   WHERE TT_H_SIYOU_CD = '" & _db.rmSQ(siyoCD) & "' "
            sql = sql & N & "   WHERE TT_H_SIYOU_CD = '" & _db.rmSQ(siyoCD.PadRight(2, " ")) & "' "

            sql = sql & N & "   AND TT_H_HIN_CD = '" & _db.rmSQ(txtHinsyu.Text) & "' "
            sql = sql & N & "   AND TT_H_SENSIN_CD = '" & _db.rmSQ(txtSensin.Text) & "' "
            sql = sql & N & "   AND TT_H_SIZE_CD = '" & _db.rmSQ(txtSize.Text) & "' "
            sql = sql & N & "   AND TT_H_COLOR_CD = '" & _db.rmSQ(txtColor.Text) & "' "

            Dim iRecCnt As Integer
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If Not iRecCnt = 0 Then     '���o���R�[�h������0���ȊO�̏ꍇ
                txtSiyo.Focus()
                lblHinmei.Text = ""     '�i���̃N���A
                Throw New UsrDefException("�i���R�[�h�͊��ɓo�^����Ă��܂��B", _msgHd.getMSG("alreadyHinmei"))
            End If


        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@ �i���d���`�F�b�N
    '   �i�����T�v�j�W�v�i���}�X�^���������A�i���R�[�h�̏d�����Ȃ����`�F�b�N����
    '�@�@�����̓p�����[�^�F prmSiyo�@   �d�l�R�[�h�܂��͏W�v�Ώەi�d�l�R�[�h
    '                    �F prmHinsyu   �i��R�[�h�܂��͏W�v�Ώەi�i��R�[�h
    '                    �F prmSensin�@ ���S���R�[�h�܂��͏W�v�Ώەi���S���R�[�h
    '                    �F prmSize�@   �T�C�Y�R�[�h�܂��͏W�v�Ώەi�T�C�Y�R�[�h
    '                    �F prmColor�@  �F�R�[�h�܂��͏W�v�Ώەi�F�R�[�h
    '�@�@���֐��߂�l�@�@�F �Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub checkKHinmeiCDRepeat(ByVal prmSiyo As String, ByVal prmHinsyu As String, ByVal prmSensin As String, _
                                                            ByVal prmSize As String, ByVal prmColor As String)
        Try
            '�i���������p�ɂȂ���
            '�d�l�R�[�h��1���̏ꍇ�́A���p�X�y�[�X��������
            Dim siyoCD As String = ""
            If _db.rmSQ(prmSiyo).Length = 1 Then
                siyoCD = siyoCD & _db.rmSQ(prmSiyo) & " "
            Else
                siyoCD = _db.rmSQ(prmSiyo)
            End If

            '�v��i���R�[�h�쐬
            Dim kHinmei As String = ""
            kHinmei = siyoCD & _db.rmSQ(prmHinsyu) & _db.rmSQ(prmSensin) & _db.rmSQ(prmSize) & _db.rmSQ(prmColor)

            'M12
            Dim sql As String = ""
            sql = ""
            sql = sql & N & " SELECT "
            sql = sql & N & " KHINMEICD "
            sql = sql & N & " FROM M12SYUYAKU "
            sql = sql & N & "   WHERE HINMEICD = '" & kHinmei & "' "

            Dim iRecCnt As Integer
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If Not iRecCnt = 0 Then     '���o���R�[�h������0���ȊO�̏ꍇ
                If prmSiyo.Equals(txtSiyo.Text) Then
                    txtSiyo.Focus()
                Else
                    txtSSiyo.Focus()
                End If
                Throw New UsrDefException("���͂��ꂽ�R�[�h�͈ȉ��̃R�[�h�̎��i���R�[�h�Ƃ��ēo�^����Ă��܂��B", _
                    _msgHd.getMSG("alreakyAddJituhinmeiCD", "�v��i���R�[�h�@�F�@" & _db.rmNullStr(ds.Tables(RS).Rows(0)("KHINMEICD"))))
            End If

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@ �L�[���ړ��̓`�F�b�N
    '   �i�����T�v�j�e���ڂ̖����̓`�F�b�N�E���͒l�Ó����`�F�b�N���s�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub checkInputKey()
        Try

            '�i���\���`�F�b�N
            If "".Equals(lblHinmei.Text) Then
                Throw New UsrDefException("�K�{���͂ł��B", _msgHd.getMSG("requiredImput", "�y�i���z"), txtSeisaku)
            End If

            '����敪�����̓`�F�b�N
            If "".Equals(txtSeisaku.Text) Then
                Throw New UsrDefException("�K�{���͂ł��B", _msgHd.getMSG("requiredImput", "�y����敪�z"), txtSeisaku)
            End If

            '�W�����b�g�������̓`�F�b�N
            If "".Equals(txtHyoujunLot.Text) Then
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y�W�����b�g���z"), txtHyoujunLot)
            End If

            '�P�������̓`�F�b�N
            If "".Equals(txtTantyou.Text) Then
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y�P���z"), txtTantyou)
            End If

            '�W�����b�g���E����P�����͒l�`�F�b�N(�W�����b�g��������P��=����)
            If CInt(txtHyoujunLot.Text) Mod CInt(txtTantyou.Text) <> 0 Then
                Throw New UsrDefException("�W�����b�g���܂��͐���P�����Ⴂ�܂��B", _msgHd.getMSG("failLotOrTantyou"), txtHyoujunLot)
            End If

            '�P���~�𐔂��u9999999�v�𒴂���ꍇ
            If CInt(txtTantyou.Text) * CInt(lblJosu.Text) > 9999999 Then
                Throw New UsrDefException("�P���~�𐔂̒l��7���𒴂��Ă��܂��B", _msgHd.getMSG("over7Keta"), txtTantyou)
            End If

            '�𐔂�4���𒴂���ꍇ
            If CInt(lblJosu.Text).ToString.Length > 4 Then
                Throw New UsrDefException("�𐔂̒l��4���𒴂��Ă��܂��B", _msgHd.getMSG("over4KetaJosu"), txtHyoujunLot)
            End If

            '�Z�d���������̓`�F�b�N
            If "".Equals(txtSumiHonsu.Text) Then
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y�Z�d�������z"), txtSumiHonsu)
            End If

            '�Z�d�����{���͈̓`�F�b�N
            If (CInt(txtSumiHonsu.Text) < 0) Or (CInt(txtSumiHonsu.Text) > CInt(lblJosu.Text)) Then
                Throw New UsrDefException("�͈͊O�̒l�����͂���܂����B", _msgHd.getMSG("errOutOfRange", "�y�Z�d�������z"), txtSumiHonsu)
            End If

            '�𐔑Ó����`�F�b�N
            If Not CInt(lblJosu.Text) = CInt(lblKNDHonsu.Text) + CInt(txtSumiHonsu.Text) Then
                Throw New UsrDefException("�k���{���ƏZ�d�������̍��v���𐔂ƈ�v���Ă��܂���B", _msgHd.getMSG("notJosuSumKndAndSumi"), txtSumiHonsu)
            End If

            '�݌ɁE�J��
            If "".Equals(txtZaiko.Text) Then
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y�݌ɁE�J�ԁz"), txtZaiko)
            End If

            If txtZaiko.Text = ZAIKOCD_ZAIKO Then
                '���v��
                If "".Equals(txtJuyousaki.Text) Then
                    Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y���v��z"), txtJuyousaki)
                End If

                '�T�C�Y�W�J
                If "".Equals(txtSizeTenkai.Text) Then
                    Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y�T�C�Y�W�J�z"), txtSizeTenkai)
                End If

                '�i��敪
                If "".Equals(cboHinsyuKbn.Text) Then
                    Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y�i��敪�z"), cboHinsyuKbn)
                End If

                '�����
                If "".Equals(txtKijunTuki.Text) Then
                    Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y������z"), txtKijunTuki)
                End If
            Else
                '�����撷���`�F�b�N
                If System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(txtChumonsaki.Text) > 20 Then
                    Throw New UsrDefException("20�o�C�g�܂łœ��͂��Ă��������B", _msgHd.getMSG("over20bite", "�y������z"), txtChumonsaki)
                End If
            End If

            '�ȖڃR�[�h�����̓`�F�b�N�i������敪=2�F�O�� �̏ꍇ�̂݁j
            If SEISAKUKBN_GAITYU.Equals(txtSeisaku.Text) And "".Equals(txtKamoku.Text) Then
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y�ȖڃR�[�h�z"), txtKamoku)
            End If
            '' 2011/01/13 add start sugano
            '�ȖڃR�[�h�����`�F�b�N�i������敪=2�F�O���̏ꍇ�̂݁j
            If SEISAKUKBN_GAITYU.Equals(txtSeisaku.Text) And Len(Trim(txtKamoku.Text)) <> 5 Then
                Throw New UsrDefException("�ȖڃR�[�h�͂T���œ��͂��ĉ������B", _msgHd.getMSG("notKamokuCDKeta", "�y�ȖڃR�[�h�z"), txtKamoku)
            End If
            '' 2011/01/13 add end sugano

            '���g�R�[�h�����̓`�F�b�N
            If "".Equals(txtMakiwaku.Text) Then
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y���g�R�[�h�z"), txtMakiwaku)
            End If

            '���g�R�[�h�����`�F�b�N
            If Len(Trim(txtMakiwaku.Text)) <> 6 Then
                Throw New UsrDefException("���g�R�[�h�͂U���œ��͂��ĉ������B", _msgHd.getMSG("notMakiwaku6Keta"), txtMakiwaku)
            End If

            '��E�\���敪�����̓`�F�b�N
            If "".Equals(txtHousou.Text) Then
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y��E�\���敪�z"), txtHousou)
            End If

            '�d�l���ԍ������̓`�F�b�N
            If "".Equals(txtSiyousyo.Text) Then
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y�d�l���ԍ��z"), txtSiyousyo)
            End If

            '�d�l���ԍ��������`�F�b�N
            If System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(txtSiyousyo.Text) > 20 Then
                Throw New UsrDefException("20�o�C�g�܂łœ��͂��Ă��������B", _msgHd.getMSG("over20bite", "�y�d�l���ԍ��z"), txtSiyousyo)
            End If

            '�������喢���̓`�F�b�N
            If "".Equals(txtSeizouBmn.Text) Then
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y��������z"), txtSeizouBmn)
            End If

            '�W�J�敪�����̓`�F�b�N
            If "".Equals(txtTenkaiKbn.Text) Then
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y�W�J�敪�z"), txtTenkaiKbn)
            End If

            '�W�J�敪���e�`�F�b�N
            If SEISAKUKBN_GAITYU.Equals(txtSeisaku.Text) And Not TENKAIKBN_BUBUN.Equals(txtTenkaiKbn.Text) Then
                Throw New UsrDefException("����敪�u�O���v���͓W�J�敪�u�����W�J�v�ȊO�I���ł��܂���B", _msgHd.getMSG("nonGaicyuSelect"), txtTenkaiKbn)
            End If

            '�W�J�敪�������W�J�̏ꍇ
            If TENKAIKBN_BUBUN.Equals(txtTenkaiKbn.Text) Then
                '�����W�J�H�������̓`�F�b�N
                If "".Equals(txtBTenkai.Text) Then
                    Throw New UsrDefException("�W�J�敪�u�����W�J�v���͕����W�J�w��H���͏ȗ��ł��܂���B", _msgHd.getMSG("nonBubunOmit"), txtBTenkai)
                End If

                '�����W�J�H�������`�F�b�N
                If System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(txtBTenkai.Text) > 6 Then
                    Throw New UsrDefException("�����W�J�H����6�o�C�g�܂łœ��͂��Ă��������B", _msgHd.getMSG("over6biteBubunTenkai", "�y�����W�J�H���z"), txtBTenkai)
                End If

                '�����W�J�H�����͓��e�`�F�b�N
                    If Not ("1".Equals(txtBTenkai.Text.Substring(0, 1)) Or "3".Equals(txtBTenkai.Text.Substring(0, 1))) Then
                    Throw New UsrDefException("1�܂���3����n�܂�H������͂��Ă��������B", _msgHd.getMSG("notBKouteiStart1Or3"), txtBTenkai)
                End If
            End If

            '�i�������敪�����̓`�F�b�N
            If "".Equals(txtHinsitu.Text) Then
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y�i�������敪�z"), txtHinsitu)
            End If

            '����L�������̓`�F�b�N
            If "".Equals(txtTatiai.Text) Then
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y����L���z"), txtTatiai)
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