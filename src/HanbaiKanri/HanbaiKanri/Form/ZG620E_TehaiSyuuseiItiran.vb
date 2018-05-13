'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���j��z�f�[�^�ꗗ
'    �i�t�H�[��ID�jZG620E_TehaiSyuuseiItran
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ���{        2010/10/19                 �V�K              
'�@(2)   ���V        2010/11/17                 �ύX(���ځu�[���v�폜�Ή�)    
'�@(3)   ���V        2010/12/02                 �ύX(�ڍ׉�ʂŕύX�����ΏۊO�����f����Ȃ��o�O�C��)    
'�@(4)   ����        2011/01/17                 �ύX(��������e�[�u���̍X�V����߂�j    
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls

Public Class ZG620E_TehaiSyuuseiItiran
    Inherits System.Windows.Forms.Form
    Implements IfRturnUpDateData

#Region "���e�����l��`"
    '-------------------------------------------------------------------------------
    '   �萔��`
    '-------------------------------------------------------------------------------

    'PG���䕶��
    Private Const RS As String = "RecSet"                       '�f�[�^�Z�b�g�e�[�u����
    Private Const N As String = ControlChars.NewLine            '���s����
    Private Const PGID As String = "ZG620E"

    '�ꗗ�f�[�^�o�C���h��
    Private Const COLDT_TAISYOGAI As String = "dtTaisyogai"         '�ΏۊO
    Private Const COLDT_TEHAINO As String = "dtTehaiNo"             '��z��
    Private Const COLDT_SYUTTAIBI As String = "dtSyuttaibi"         '��]�o����
    '2010/11/17 delete start Nakazawa
    'Private Const COLDT_NOUKI As String = "dtNouki"                 '�[��
    '2010/11/17 delete end Nakazawa
    Private Const COLDT_HINMEICD As String = "dtHinmeiCD"           '�i���R�[�h
    Private Const COLDT_HINMEI As String = "dtHinmei"               '�i��
    Private Const COLDT_TEHAISUURYOU As String = "dtTehaiSuuryou"   '��z������
    Private Const COLDT_TANTYOU As String = "dtTantyou"             '�P��
    Private Const COLDT_JOUSUU As String = "dtJousuu"               '��
    Private Const COLDT_SIYOUSYONO As String = "dtSiyousyoNo"       '�d�l���ԍ�
    Private Const COLDT_MAKIWAKU As String = "dtMakiwaku"           '���g�R�[�h
    Private Const COLDT_HOUSOU As String = "dtHousou"               '�/�\���敪
    Private Const COLDT_JUYOU As String = "dtJuyou"                 '���v��
    Private Const COLDT_SEISAKU As String = "dtSeisaku"             '����敪
    Private Const COLDT_SEIZOUBMN As String = "dtSeizoubmn"         '��������
    Private Const COLDT_TEHAISORT As String = "dtTehaiSort"         '��z���\����

    '�ꗗ�O���b�h��
    Private Const COLCN_TAISYOGAI As String = "cnTaisyogai"         '�ΏۊO
    Private Const COLCN_TEHAINO As String = "cnTehaiNo"             '��z��
    Private Const COLCN_SYUTTAIBI As String = "cnSyuttaibi"         '��]�o����
    '2010/11/17 delete start Nakazawa
    'Private Const COLCN_NOUKI As String = "cnNouki"                 '�[��
    '2010/11/17 delete end Nakazawa
    Private Const COLCN_HINMEICD As String = "cnHinmeiCD"           '�i���R�[�h
    Private Const COLCN_HINMEI As String = "cnHinmei"               '�i��
    Private Const COLCN_TEHAISUURYOU As String = "cnTehaiSuuryou"   '��z������
    Private Const COLCN_TANTYOU As String = "cnTantyou"             '�P��
    Private Const COLCN_JOUSUU As String = "cnJousuu"               '��
    Private Const COLCN_SIYOUSYONO As String = "cnSiyousyoNo"       '�d�l���ԍ�
    Private Const COLCN_MAKIWAKU As String = "cnMakiwaku"           '���g�R�[�h
    Private Const COLCN_HOUSOU As String = "cnHousou"               '�/�\���敪
    Private Const COLCN_JUYOU As String = "cnJuyou"                 '���v��
    Private Const COLCN_SEISAKU As String = "cnSeisaku"             '����敪
    Private Const COLCN_SEIZOUBMN As String = "cnSeizoubmn"         '��������
    Private Const COLCN_TEHAISORT As String = "cnTehaiSort"         '��z���\����

    'M01�ėp�}�X�^�Œ跰
    Private Const COTEI_JUYOU As String = "01"                      '���v�於
    Private Const COTEI_SEISAKU As String = "03"                    '����敪
    Private Const COTEI_SEIZOUBMN As String = "09"                  '��������
    Private Const COTEI_GAIFLG As String = "17"                     '�ΏۊO�t���O

    'EXCEL
    Private Const START_PRINT As Integer = 11        'EXCEL�o�͊J�n�s��
    Private Const XLSSHEET_DENSEN As String = "�d��" '�V�[�g���f�p�Œ蕶��
    Private Const XLSSHEET_TSUSIN As String = "�ʐM" '�V�[�g���f�p�Œ蕶��

    'EXCEL��ԍ�
    Private Const XLSCOL_TEHAINO As Integer = 1      '��z��
    Private Const XLSCOL_JUYOU As Integer = 2        '���v�於
    Private Const XLSCOL_SEISAKU As Integer = 3      '����敪
    Private Const XLSCOL_HINMEICD As Integer = 4     '�i���R�[�h
    Private Const XLSCOL_HINMEI As Integer = 5       '�i���E�T�C�Y�E�F
    Private Const XLSCOL_SYUTTAIBI As Integer = 6    '��]�o����
    Private Const XLSCOL_TEHAISUURYOU As Integer = 7 '��z����
    Private Const XLSCOLTANTYOU As Integer = 8       '�P��
    Private Const XLSCOL_JOUSUU As Integer = 9       '��
    Private Const XLSCOL_MAKIWAKU As Integer = 10    '���g�R�[�h
    Private Const XLSCOL_HOUSOU As Integer = 11      '�/�\���敪
    Private Const XLSCOL_SIYOUSYONO As Integer = 12  '�d�l���ԍ�
    Private Const XLSCOL_GAICYU As Integer = 13      '�O��
    Private Const XLSCOL_ENKI As Integer = 14        '����
    Private Const XLSCOL_CYUSI As Integer = 15       '���~
    

#End Region

#Region "�����o�ϐ���`"

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu

    Private _oldRowIndex As Integer = -1            '�I���s�̔w�i�F��ύX���邽�߂̃t���O��錾
    Private _colorCtlFlg As Boolean = False         '�I���s�̔w�i�F��ύX���邽�߂̃t���O��錾

    '���������i�[�ϐ�
    Private _SerchCriteria As SerchCriteria
    Private Structure SerchCriteria
        Private _serchTehaiNo As String            '��z��
        Private _serchSiyoCd As String             '�d�l�R�[�h
        Private _serchHinsyuCd As String           '�i��R�[�h
        Private _serchSensinCd As String           '���S���R�[�h
        Private _serchSizeCd As String             '�T�C�Y�R�[�h
        Private _serchColorCd As String            '�F�R�[�h
        Private _serchHinmei As String             '�i��
        Private _serchKibouFrom As String          '��]�o����(From)
        Private _serchKibouTo As String            '��]�o����(To)
        '2010/11/17 delete start Nakazawa
        'Private _serchNoukiFrom As String          '�[��(From)
        'Private _serchNoukiTo As String            '�[��(To)
        '2010/11/17 delete end Nakazawa

        Public Property serchTehaiNo() As String
            Get
                Return _serchTehaiNo
            End Get
            Set(ByVal Value As String)
                _serchTehaiNo = Value
            End Set
        End Property
        Public Property serchSiyoCd() As String
            Get
                Return _serchSiyoCd
            End Get
            Set(ByVal Value As String)
                _serchSiyoCd = Value
            End Set
        End Property
        Public Property serchHinsyuCd() As String
            Get
                Return _serchHinsyuCd
            End Get
            Set(ByVal Value As String)
                _serchHinsyuCd = Value
            End Set
        End Property
        Public Property serchSensinCd() As String
            Get
                Return _serchSensinCd
            End Get
            Set(ByVal Value As String)
                _serchSensinCd = Value
            End Set
        End Property
        Public Property serchSizeCd() As String
            Get
                Return _serchSizeCd
            End Get
            Set(ByVal Value As String)
                _serchSizeCd = Value
            End Set
        End Property
        Public Property serchColorCd() As String
            Get
                Return _serchColorCd
            End Get
            Set(ByVal Value As String)
                _serchColorCd = Value
            End Set
        End Property
        Public Property serchHinmei() As String
            Get
                Return _serchHinmei
            End Get
            Set(ByVal Value As String)
                _serchHinmei = Value
            End Set
        End Property
        Public Property serchKibouFrom() As String
            Get
                Return _serchKibouFrom
            End Get
            Set(ByVal Value As String)
                _serchKibouFrom = Value
            End Set
        End Property
        Public Property serchKibouTo() As String
            Get
                Return _serchKibouTo
            End Get
            Set(ByVal Value As String)
                _serchKibouTo = Value
            End Set
        End Property
        '2010/11/17 delete start Nakazawa
        'Public Property serchNoukiFrom() As String
        '    Get
        '        Return _serchNoukiFrom
        '    End Get
        '    Set(ByVal Value As String)
        '        _serchNoukiFrom = Value
        '    End Set
        'End Property
        'Public Property serchNoukiTo() As String
        '    Get
        '        Return _serchNoukiTo
        '    End Get
        '    Set(ByVal Value As String)
        '        _serchNoukiTo = Value
        '    End Set
        'End Property
        '2010/11/17 delete end Nakazawa
    End Structure

    '��������
    Private _sqlWhere As String = ""
    Private _updFlg As Boolean = False

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
    Private Sub ZC910S_CodeSentaku_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            '�`��֌W�̐ݒ�
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           '�T�C�Y���ύX���ꂽ�Ƃ��ɁA�R���g���[�����R���g���[�����̂��ĕ`�悷�邩�ǂ����������l��ݒ�
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '�`��̓o�b�t�@�Ŏ��s����A������ɁA���ʂ���ʂɏo�͂����悤�ݒ�
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  '�R���g���[���́A��ʂɒ��ڂł͂Ȃ��A�܂��o�b�t�@�ɕ`�悳��܂��B����ɂ��A�������}���邱�Ƃ��ł��܂��B
            Me.SetStyle(ControlStyles.UserPaint, True)              '�R���g���[���́A�I�y���[�e�B���O �V�X�e���ɂ���Ăł͂Ȃ��A�Ǝ��ɕ`�悳���悤�ݒ�
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   '�R���g���[���̓E�B���h�E ���b�Z�[�W WM_ERASEBKGND �𖳎�����悤�ɐݒ�

            '�^�C�g���I�v�V�����\��
            Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr

            '��ʕ\��
            Call initForm()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

#End Region

#Region "�{�^���C�x���g"

    '------------------------------------------------------------------------------------------------------
    '�@�߂�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnModoru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModoru.Click
        Try

            '����ʂ��I�����A���j���[��ʂɖ߂�B
            _parentForm.Show()
            _parentForm.Activate()

            Me.Close()
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�����{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnKensaku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensaku.Click
        Try

            '���t�`�F�b�N
            Call checkDate()

            ' ����Wait�J�[�\����ێ�
            Dim preCursor As Cursor = Me.Cursor
            ' �J�[�\����ҋ@�J�[�\���ɕύX
            Me.Cursor = Cursors.WaitCursor

            Try
                '���������̍쐬
                _sqlWhere = createSerchStr()

                '�񒅐F�t���O����
                _colorCtlFlg = False

                '�ꗗ�\��
                Call dispDGV(_sqlWhere)

                ''2011/01/17 del start sugano
                ''��������e�[�u�����X�V����
                '_parentForm.updateSeigyoTbl(PGID, True, Now(), Now())
                ''2011/01/17 del end sugano

                '�ꗗ�s���F�t���O��L���ɂ���
                _colorCtlFlg = True

                '�ꗗ�̍ŏ��̓��͉\�Z���փt�H�[�J�X����
                setForcusCol(dgvTehaiData.CurrentCellAddress.Y, 0)
            Finally
                ' �J�[�\�������ɖ߂�
                Me.Cursor = preCursor
            End Try
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�݌ɕ�[���X�g�o�̓{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnInsatu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsatu.Click
        Try

            ' ����Wait�J�[�\����ێ�
            Dim preCursor As Cursor = Me.Cursor
            ' �J�[�\����ҋ@�J�[�\���ɕύX
            Me.Cursor = Cursors.WaitCursor
            Try

                'EXCEL�o��
                Call printExcel(_sqlWhere)

            Finally
                ' �J�[�\�������ɖ߂�
                Me.Cursor = preCursor
            End Try
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�C���{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnSyuusei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSyuusei.Click
        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvTehaiData)
            Dim rowcnt As Integer = dgvTehaiData.CurrentCellAddress.Y

            Dim kahenKey As String = gh.getCellData(COLDT_TEHAINO, rowcnt)    '���݃J�[�\��������s�̉σL�[���擾
            Dim syori As String = Trim(Replace(lblSyori.Text, "/", ""))       '�����N��
            Dim keikaku As String = Trim(Replace(lblKeikaku.Text, "/", ""))   '�v��N��

            Dim openForm As ZG621E_TehaiSyuuseiSyousai = New ZG621E_TehaiSyuuseiSyousai(_msgHd, _db, Me, kahenKey, syori, keikaku, _updFlg, _parentForm)      '��ʑJ��
            openForm.ShowDialog(Me)                                                                       '��ʕ\��
            openForm.Dispose()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-->2010.12.12 add by takagi 
    Private Sub dgvTehaiData_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTehaiData.CellContentDoubleClick
        If e.RowIndex < 0 Then Exit Sub
        Call btnSyuusei_Click(Nothing, Nothing) '�C���{�^�������]��
    End Sub
    '<--2010.12.12 add by takagi 

#End Region

#Region "���[�U��`�֐�:��ʐ���"
    '-------------------------------------------------------------------------------
    '�@��ʋN����
    '-------------------------------------------------------------------------------
    Private Sub initForm()
        Try
            '�C���{�^���E�݌ɕ�[���X�g�o�̓{�^���g�p�s��
            btnSyuusei.Enabled = False
            btnInsatu.Enabled = False

            '�����N���A�v��N���\��
            Call dispDate()

            '-->2010.12.27 chg by takagi #53
            ' '' 2010/12/22 add start sugano
            ''�񒅐F�t���O����
            '_colorCtlFlg = False

            ''�ꗗ�\��
            'Call dispDGV("")

            ''��������e�[�u�����X�V����
            '_parentForm.updateSeigyoTbl(PGID, True, Now(), Now())

            ''�ꗗ�s���F�t���O��L���ɂ���
            '_colorCtlFlg = True

            ''�ꗗ�̍ŏ��̓��͉\�Z���փt�H�[�J�X����
            'setForcusCol(dgvTehaiData.CurrentCellAddress.Y, 0)
            ' '' 2010/12/22 add end sugano
            Call btnKensaku_Click(Nothing, Nothing)
            ''<--2010.12.27 chg by takagi #53

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@��������SQL�쐬
    '�@(�����T�v)��ʂɓ��͂��ꂽ����������SQL���ɂ���
    '�@�@I�@�F�@�Ȃ�
    '�@�@R�@�F�@createSerchStr      '��������
    '-------------------------------------------------------------------------------
    Private Function createSerchStr() As String
        Try
            
            createSerchStr = ""

            '���������̕ێ�
            _SerchCriteria.serchTehaiNo = txtTehaiNo.Text        '��z��
            _SerchCriteria.serchSiyoCd = txtSiyouCD.Text         '�d�l�R�[�h
            _SerchCriteria.serchHinsyuCd = txtHinsyuCD.Text      '�i��R�[�h
            _SerchCriteria.serchSensinCd = txtSensinsuu.Text     '���S���R�[�h
            _SerchCriteria.serchSizeCd = txtSize.Text            '�T�C�Y�R�[�h
            _SerchCriteria.serchColorCd = txtColor.Text          '�F�R�[�h
            _SerchCriteria.serchHinmei = txtHinmei.Text          '�i��
            _SerchCriteria.serchKibouFrom = Trim(Replace(txtKibouFrom.Text, "/", ""))    '��]�o����(From)
            _SerchCriteria.serchKibouTo = Trim(Replace(txtKibouTo.Text, "/", ""))        '��]�o����(To)
            '2010/11/17 delete start Nakazawa
            '_SerchCriteria.serchNoukiFrom = Trim(Replace(txtNoukiFrom.Text, "/", ""))    '�[��(From)
            '_SerchCriteria.serchNoukiTo = Trim(Replace(txtNoukiTo.Text, "/", ""))        '�[��(To)
            '2010/11/17 delete end Nakazawa

            '��z��
            If Not "".Equals(_SerchCriteria.serchTehaiNo) Then
                If Not "".Equals(createSerchStr) Then
                    createSerchStr = createSerchStr & N & " AND "
                End If
                createSerchStr = createSerchStr & " TEHAI_NO LIKE '" & _SerchCriteria.serchTehaiNo & "%'"
            End If

            '�i���R�[�h�d�l
            If Not "".Equals(_SerchCriteria.serchSiyoCd) Then
                If Not "".Equals(createSerchStr) Then
                    createSerchStr = createSerchStr & N & " AND "
                End If
                createSerchStr = createSerchStr & " H_SIYOU_CD LIKE '" & _SerchCriteria.serchSiyoCd & "%'"
            End If

            '�i���R�[�h�i��
            If Not "".Equals(_SerchCriteria.serchHinsyuCd) Then
                If Not "".Equals(createSerchStr) Then
                    createSerchStr = createSerchStr & N & " AND "
                End If
                createSerchStr = createSerchStr & " H_HIN_CD LIKE '" & _SerchCriteria.serchHinsyuCd & "%'"
            End If

            '�i���R�[�h���S��
            If Not "".Equals(_SerchCriteria.serchSensinCd) Then
                If Not "".Equals(createSerchStr) Then
                    createSerchStr = createSerchStr & N & " AND "
                End If
                createSerchStr = createSerchStr & " H_SENSIN_CD LIKE '" & _SerchCriteria.serchSensinCd & "%'"
            End If

            '�i���R�[�h�T�C�Y
            If Not "".Equals(_SerchCriteria.serchSizeCd) Then
                If Not "".Equals(createSerchStr) Then
                    createSerchStr = createSerchStr & N & " AND "
                End If
                createSerchStr = createSerchStr & " H_SIZE_CD LIKE '" & _SerchCriteria.serchSizeCd & "%'"
            End If

            '�i���R�[�h�i��
            If Not "".Equals(_SerchCriteria.serchColorCd) Then
                If Not "".Equals(createSerchStr) Then
                    createSerchStr = createSerchStr & N & " AND "
                End If
                createSerchStr = createSerchStr & " H_COLOR_CD LIKE '" & _SerchCriteria.serchColorCd & "%'"
            End If

            '�i��
            If Not "".Equals(_SerchCriteria.serchHinmei) Then
                If Not "".Equals(createSerchStr) Then
                    createSerchStr = createSerchStr & N & " AND "
                End If
                createSerchStr = createSerchStr & " HINMEI LIKE '%" & _SerchCriteria.serchHinmei & "%'"
            End If

            '��]�o����(From)
            If Not "".Equals(_SerchCriteria.serchKibouFrom) Then
                If Not "".Equals(createSerchStr) Then
                    createSerchStr = createSerchStr & N & " AND "
                End If
                createSerchStr = createSerchStr & " KIBOU_DATE >= '" & _SerchCriteria.serchKibouFrom & "'"
            End If
            '��]�o����(To)
            If Not "".Equals(_SerchCriteria.serchKibouTo) Then
                If Not "".Equals(createSerchStr) Then
                    createSerchStr = createSerchStr & N & " AND "
                End If
                createSerchStr = createSerchStr & " KIBOU_DATE <= '" & _SerchCriteria.serchKibouTo & "'"
            End If
            '2010/11/17 delete start Nakazawa
            '�[��(From)
            'If Not "".Equals(_SerchCriteria.serchNoukiFrom) Then
            '    If Not "".Equals(createSerchStr) Then
            '        createSerchStr = createSerchStr & N & " AND "
            '    End If
            '    createSerchStr = createSerchStr & " NOUKI >= '" & _SerchCriteria.serchNoukiFrom & "'"
            'End If
            ''�[��(To)
            'If Not "".Equals(_SerchCriteria.serchNoukiTo) Then
            '    If Not "".Equals(createSerchStr) Then
            '        createSerchStr = createSerchStr & N & " AND "
            '    End If
            '    createSerchStr = createSerchStr & " NOUKI <= '" & _SerchCriteria.serchNoukiTo & "'"
            'End If
            '2010/11/17 delete end Nakazawa

            If Not "".Equals(createSerchStr) Then
                createSerchStr = " WHERE " & createSerchStr
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
    Private Sub txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTehaiNo.KeyPress, _
                                                                                                                txtHinsyuCD.KeyPress, _
                                                                                                                txtSiyouCD.KeyPress, _
                                                                                                                txtSensinsuu.KeyPress, _
                                                                                                                txtSize.KeyPress, _
                                                                                                                txtColor.KeyPress, _
                                                                                                                txtHinmei.KeyPress, _
                                                                                                                txtKibouFrom.KeyPress, _
                                                                                                                txtKibouTo.KeyPress
        Try
            '���̃R���g���[���ֈړ�����
            UtilClass.moveNextFocus(Me, e)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�R���g���[���L�[�t�H�[�J�X�擾�C�x���g
    '�@(�����T�v)�t�H�[�J�X�擾���A�S�I����Ԃɂ���
    '-------------------------------------------------------------------------------
    Private Sub txt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTehaiNo.GotFocus, _
                                                                                            txtHinsyuCD.GotFocus, _
                                                                                            txtSiyouCD.GotFocus, _
                                                                                            txtSensinsuu.GotFocus, _
                                                                                            txtSize.GotFocus, _
                                                                                            txtColor.GotFocus, _
                                                                                            txtHinmei.GotFocus, _
                                                                                            txtKibouFrom.GotFocus, _
                                                                                            txtKibouTo.GotFocus
        Try
            '�S�I����Ԃɂ���
            UtilClass.selAll(sender)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    ' �@�X�V�f�[�^�̎󂯎��
    '   (�����T�v)�q��ʂōX�V���ꂽ�f�[�^���󂯎��
    '�@�@I�@�F�@prmKibou     �@�@ ��]�o����
    '�@�@I�@�F�@prmTehaiSuuryou   ��z����
    '�@�@I�@�F�@prmTantyou        �P��
    '�@�@I�@�F�@prmJousuu         ��
    '�@�@I�@�F�@prmSiyousyoNo     �d�l���ԍ�
    '-------------------------------------------------------------------------------
    '2010/12/02 update start Nakazawa---
    '2010/11/17 update start Nakazawa
    'Sub setUpDateData(ByVal prmKibou As String, ByVal prmNouki As String, ByVal prmTehaiSuuryou As String, _
    '                ByVal prmTantyou As String, ByVal prmJousuu As String, ByVal prmSiyousyoNo As String) Implements IfRturnUpDateData.setUpDateData
    'Sub setUpDateData(ByVal prmKibou As String, ByVal prmTehaiSuuryou As String, _
    '                     ByVal prmTantyou As String, ByVal prmJousuu As String, ByVal prmSiyousyoNo As String) Implements IfRturnUpDateData.setUpDateData
    '2010/11/17 update end Nakazawa
    Sub setUpDateData(ByVal prmKibou As String, ByVal prmTehaiSuuryou As String, _
                ByVal prmTantyou As String, ByVal prmJousuu As String, ByVal prmSiyousyoNo As String, ByVal prmTaisyogaiFlg As Boolean) Implements IfRturnUpDateData.setUpDateData
        '2010/12/02 update end Nakazawa---

        Try
            '�q��ʂœ��͂��ꂽ���e���ꗗ�ɔ��f����
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvTehaiData)
            gh.setCellData(COLDT_SYUTTAIBI, dgvTehaiData.CurrentRow.Index, prmKibou)                '��]��
            '2010/11/17 delete start Nakazawa
            'gh.setCellData(COLDT_NOUKI, dgvTehaiData.CurrentRow.Index, prmNouki)                  '�[��
            '2010/11/17 delete end Nakazawa
            gh.setCellData(COLDT_TEHAISUURYOU, dgvTehaiData.CurrentRow.Index, prmTehaiSuuryou)      '��z����
            gh.setCellData(COLDT_TANTYOU, dgvTehaiData.CurrentRow.Index, prmTantyou)                '�P��
            gh.setCellData(COLDT_JOUSUU, dgvTehaiData.CurrentRow.Index, prmJousuu)                  '��
            gh.setCellData(COLDT_SIYOUSYONO, dgvTehaiData.CurrentRow.Index, prmSiyousyoNo)          '�d�l���ԍ�

            '2010/12/02 add start Nakazawa
            Dim taisyogai As String = ""        '�ΏۊO�̗�ɕ\������l
            If prmTaisyogaiFlg Then
                '�ڍ׉�ʂőΏۊO�Ƀ`�F�b�N����Ă����ꍇ�́A�ėp�}�X�^����\�����e���擾����
                Dim sql As String = ""
                sql = "SELECT NAME1 FROM M01HANYO WHERE KOTEIKEY = '" & COTEI_GAIFLG & "'"
                'SQL���s
                Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
                Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)
                If iRecCnt > 0 Then
                    taisyogai = _db.rmNullStr(ds.Tables(RS).Rows(0)("NAME1")).ToString
                End If
            Else
                '�`�F�b�N����Ă��Ȃ��ꍇ�͋��\������
                taisyogai = ""
            End If
            gh.setCellData(COLDT_TAISYOGAI, dgvTehaiData.CurrentRow.Index, taisyogai)          '�ΏۊO
            '2010/12/02 add end Nakazawa

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
    Public Sub myShow() Implements IfRturnUpDateData.myShow
        Me.Show()
    End Sub

    '-------------------------------------------------------------------------------
    '   myActivate���\�b�h
    '-------------------------------------------------------------------------------
    Public Sub myActivate() Implements IfRturnUpDateData.myActivate
        Me.Activate()
    End Sub

#End Region

#Region "���[�U��`�֐�:EXCEL�֘A"
    '------------------------------------------------------------------------------------------------------
    '�@EXCEL�o�͏���
    '------------------------------------------------------------------------------------------------------
    Private Sub printExcel(ByVal prmSql As String)
        Try
            '���`�t�@�C��
            Dim openFilePath As String = StartUp.iniValue.BaseXlsPath & "\" & StartUp.iniValue.ExcelZG640R1_Base
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
            Dim wkEditFile As String = StartUp.iniValue.OutXlsPath & "\" & StartUp.iniValue.ExcelZG640R1_Out     '�R�s�[��t�@�C��

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
                '�R�s�[��t�@�C���J��
                eh.open()
                Try
                    Dim startPrintRow As Integer = START_PRINT                                           '�o�͊J�n�s��
                    Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvTehaiData)        'DGV�n���h���̐ݒ�
                    Dim rowCnt As Integer = gh.getMaxRow
                    Dim i As Integer
                    Dim j As Integer = 0
                    Dim k As Integer = 0
                    Dim sql As String = ""
                    sql = "SELECT "
                    sql = sql & N & " T51.TEHAI_NO " & COLDT_TEHAINO
                    sql = sql & N & " ,M011.NAME2 " & COLDT_JUYOU
                    sql = sql & N & " ,M012.NAME1 " & COLDT_SEISAKU
                    sql = sql & N & " ,T51.HINMEI_CD " & COLDT_HINMEICD
                    sql = sql & N & " ,T51.HINMEI " & COLDT_HINMEI
                    sql = sql & N & " ,TO_CHAR(TO_DATE(T51.KIBOU_DATE,'YYYYMMDD'),'mm/dd') " & COLDT_SYUTTAIBI
                    sql = sql & N & " ,T51.TEHAI_SUU " & COLDT_TEHAISUURYOU
                    sql = sql & N & " ,T51.TANCYO " & COLDT_TANTYOU
                    sql = sql & N & " ,T51.JYOSU " & COLDT_JOUSUU
                    sql = sql & N & " ,T51.MAKI_CD " & COLDT_MAKIWAKU
                    sql = sql & N & " ,T51.HOSO_KBN " & COLDT_HOUSOU
                    sql = sql & N & " ,T51.SIYOUSYO_NO " & COLDT_SIYOUSYONO
                    sql = sql & N & " ,M013.NAME1 " & COLDT_SEIZOUBMN
                    sql = sql & N & " FROM T51TEHAI T51 "
                    '' 2010/12/22 upd start sugano
                    'sql = sql & N & "   LEFT OUTER JOIN M01HANYO M011 ON "
                    sql = sql & N & "   LEFT OUTER JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '01') M011 ON "
                    '' 2010/12/22 upd end sugano
                    sql = sql & N & "   T51.JUYOUCD = M011.KAHENKEY "
                    sql = sql & N & "   AND M011.KOTEIKEY = '" & COTEI_JUYOU & "'"
                    sql = sql & N & "   LEFT OUTER JOIN M01HANYO M012 ON "
                    sql = sql & N & "   T51.SEISAKU_KBN = M012.KAHENKEY "
                    sql = sql & N & "   AND M012.KOTEIKEY = '" & COTEI_SEISAKU & "'"
                    sql = sql & N & "   LEFT OUTER JOIN M01HANYO M013 ON "
                    sql = sql & N & "   T51.SEIZOU_BMN = M013.KAHENKEY "
                    sql = sql & N & "   AND M013.KOTEIKEY = '" & COTEI_SEIZOUBMN & "'"
                    '' 2010/12/22 add start sugano
                    sql = sql & N & " WHERE GAI_FLG IS NULL "   '�ΏۊO�t���O��1�̃f�[�^������
                    '' 2010/12/22 add start sugano

                    If Not "".Equals(prmSql) Then
                        '' 2010/12/22 upd start sugano
                        'sql = sql & N & prmSql
                        sql = sql & N & " AND " & prmSql.Replace("WHERE", "")
                        '' 2010/12/22 upd end sugano
                    End If
                    sql = sql & N & " ORDER BY T51.TEHAI_NO "

                    'SQL���s
                    Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
                    Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

                    If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                        Throw New UsrDefException("�Ώۃf�[�^������܂���B", _msgHd.getMSG("NonData"))
                    End If

                    '�擾�����f�[�^��EXCEL�ɓW�J����
                    For i = 0 To iRecCnt - 1
                        If XLSSHEET_DENSEN.Equals(_db.rmNullStr(ds.Tables(RS).Rows(i)(COLDT_SEIZOUBMN))) Then
                            '�������傪�d���̏ꍇ
                            printDetails(ds, eh, XLSSHEET_DENSEN, i, startPrintRow, j)
                            j = j + 1    '�d���J�E���g�A�b�v
                        Else
                            '�������傪�ʐM�̏ꍇ
                            printDetails(ds, eh, XLSSHEET_TSUSIN, i, startPrintRow, k)
                            k = k + 1    '�ʐM�J�E���g�A�b�v
                        End If
                    Next

                    '�d���̏o�͌��������݂���ꍇ�A�w�b�_�[��ҏW���A���݂��Ȃ��ꍇ�̓V�[�g���폜
                    If j > 0 Then
                        'EXCEL�w�b�_�[�ҏW
                        printHeader(eh, XLSSHEET_DENSEN, startPrintRow, j)
                    Else
                        '�V�[�g���폜
                        eh.deleteSheet(XLSSHEET_DENSEN)
                    End If
                    '�ʐM�̏o�͌��������݂���ꍇ�A�w�b�_�[��ҏW���A���݂��Ȃ��ꍇ�̓V�[�g���폜
                    If k > 0 Then
                        'EXCEL�w�b�_�[�ҏW
                        printHeader(eh, XLSSHEET_TSUSIN, startPrintRow, k)
                    Else
                        '�V�[�g���폜
                        eh.deleteSheet(XLSSHEET_TSUSIN)
                    End If

                    Clipboard.Clear()         '�N���b�v�{�[�h�̏�����
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

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@EXCEL���וҏW
    '�@(�����T�v)EXCEL�ɏo�͂��閾�ׂ�ҏW����
    '�@�@I�@�F�@prmds     �@�@�@�f�[�^�Z�b�g
    '�@�@I�@�F�@prmeh     �@�@�@EXCEL�n���h���[
    '�@�@I�@�F�@prmSeizoubmn �@ �����敪
    '�@�@I�@�F�@prmDbRows     �@�f�[�^�Z�b�g�C���f�b�N�X
    '�@�@I�@�F�@prmStartRow     �o�͊J�n�s��
    '�@�@I�@�F�@prmRow�@�@�@�@  �o�͌���
    '-------------------------------------------------------------------------------
    Private Sub printDetails(ByVal prmds As DataSet, ByVal prmeh As xls.UtilExcelHandler, ByVal prmSeizoubmn As String, ByVal prmDbRows As Integer, ByVal prmStartRow As Integer, ByVal prmRow As Integer)
        Try
            prmeh.selectSheet(prmSeizoubmn)
            '���1�s�ǉ�
            prmeh.copyRow(prmStartRow + prmRow)
            prmeh.insertPasteRow(prmStartRow + prmRow)
            '�ꗗ�f�[�^�o��
            prmeh.setValue(_db.rmNullStr(prmds.Tables(RS).Rows(prmDbRows)(COLDT_TEHAINO)), prmStartRow + prmRow, XLSCOL_TEHAINO)              '��z��
            prmeh.setValue(_db.rmNullStr(prmds.Tables(RS).Rows(prmDbRows)(COLDT_JUYOU)), prmStartRow + prmRow, XLSCOL_JUYOU)                  '���v�於
            prmeh.setValue(_db.rmNullStr(prmds.Tables(RS).Rows(prmDbRows)(COLDT_SEISAKU)), prmStartRow + prmRow, XLSCOL_SEISAKU)              '����敪
            prmeh.setValue(_db.rmNullStr(prmds.Tables(RS).Rows(prmDbRows)(COLDT_HINMEICD)), prmStartRow + prmRow, XLSCOL_HINMEICD)            '�i���R�[�h
            prmeh.setValue(_db.rmNullStr(prmds.Tables(RS).Rows(prmDbRows)(COLDT_HINMEI)), prmStartRow + prmRow, XLSCOL_HINMEI)                '�i���E�T�C�Y�E�F
            prmeh.setValue(_db.rmNullStr(prmds.Tables(RS).Rows(prmDbRows)(COLDT_SYUTTAIBI)), prmStartRow + prmRow, XLSCOL_SYUTTAIBI)          '��]�o����
            prmeh.setValue(_db.rmNullStr(prmds.Tables(RS).Rows(prmDbRows)(COLDT_TEHAISUURYOU)), prmStartRow + prmRow, XLSCOL_TEHAISUURYOU)    '��z����
            prmeh.setValue(_db.rmNullStr(prmds.Tables(RS).Rows(prmDbRows)(COLDT_TANTYOU)), prmStartRow + prmRow, XLSCOLTANTYOU)               '�P��
            prmeh.setValue(_db.rmNullStr(prmds.Tables(RS).Rows(prmDbRows)(COLDT_JOUSUU)), prmStartRow + prmRow, XLSCOL_JOUSUU)                '��
            prmeh.setValue(_db.rmNullStr(prmds.Tables(RS).Rows(prmDbRows)(COLDT_MAKIWAKU)), prmStartRow + prmRow, XLSCOL_MAKIWAKU)            '���g�R�[�h
            prmeh.setValue(_db.rmNullStr(prmds.Tables(RS).Rows(prmDbRows)(COLDT_HOUSOU)), prmStartRow + prmRow, XLSCOL_HOUSOU)                '�/�\���敪
            prmeh.setValue(_db.rmNullStr(prmds.Tables(RS).Rows(prmDbRows)(COLDT_SIYOUSYONO)), prmStartRow + prmRow, XLSCOL_SIYOUSYONO)        '�d�l���ԍ�
        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��V�X�e���G���[MSG�o�͌�X���[
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)), , StartUp.iniValue.LogFilePath)
        End Try
    End Sub
    '-------------------------------------------------------------------------------
    '�@EXCEL�w�b�_�[�ҏW
    '�@(�����T�v)EXCEL�ɏo�͂���w�b�_�[��ҏW����
    '�@�@I�@�F�@prmeh     �@�@�@EXCEL�n���h���[
    '�@�@I�@�F�@prmSeizoubmn �@ �����敪
    '�@�@I�@�F�@prmStartRow     �o�͊J�n�s��
    '�@�@I�@�F�@prmRow�@�@�@�@  �o�͌���
    '-------------------------------------------------------------------------------
    Private Sub printHeader(ByVal prmeh As xls.UtilExcelHandler, ByVal prmSeizoubmn As String, ByVal prmStartRow As Integer, ByVal prmRow As Integer)
        Try
            Dim startPrintRow As Integer = START_PRINT                                           '�o�͊J�n�s��

            prmeh.selectSheet(prmSeizoubmn)
            '�]���ȋ�s���폜
            prmeh.deleteRow(prmStartRow + prmRow)

            '�쐬�����ҏW
            Dim printDate As String = Now.ToString("yyyy/MM/dd HH:mm")
            prmeh.setValue("�쐬���� �F " & printDate, 1, 15)   'O1
            '�����N���A�v��N���ҏW
            prmeh.setValue("�����N���F" & lblSyori.Text & "�@�@�v��N���F" & lblKeikaku.Text, 4, 1)    'A4

            '���������ҏW
            Dim dtKibouFrom As String = Trim(Replace(txtKibouFrom.Text, "/", ""))
            Dim dtKibouTo As String = Trim(Replace(txtKibouTo.Text, "/", ""))
            '2010/11/17 delete start Nakazawa
            'Dim dtNoukiFrom As String = Trim(Replace(txtNoukiFrom.Text, "/", ""))
            'Dim dtNoukiTo As String = Trim(Replace(txtNoukiTo.Text, "/", ""))
            '2010/11/17 delete end Nakazawa
            Dim serchKibou As String = ""
            Dim serchNouki As String = ""
            If Not "".Equals(dtKibouFrom) Or Not "".Equals(dtKibouTo) Then
                serchKibou = dtKibouFrom & "�`" & dtKibouTo
            End If
            '2010/11/17 delete start Nakazawa
            'If Not "".Equals(dtNoukiFrom) Or Not "".Equals(dtNoukiTo) Then
            '    serchNouki = dtNoukiFrom & "�`" & dtNoukiTo
            'End If
            '2010/11/17 delete end Nakazawa

            '��������
            '2010/11/17 update start Nakazawa
            'prmeh.setValue("��z���F" & _SerchCriteria.serchTehaiNo & "�@�@�i�����ށF" & createHinmeiCd() & "�@�@�i���F" & _SerchCriteria.serchHinmei & _
            '            "�@�@��]�o�����F" & serchKibou & "�@�@�[���F" & serchNouki, 6, 1) 'A6
            prmeh.setValue("��z���F" & _SerchCriteria.serchTehaiNo & "�@�@�i�����ށF" & createHinmeiCd() & "�@�@�i���F" & _SerchCriteria.serchHinmei & _
                                    "�@�@��]�o�����F" & serchKibou, 6, 1) 'A6

            '2010/11/17 update end Nakazawa
            '����̃Z���Ƀt�H�[�J�X���Ă�
            prmeh.selectCell(11, 1)     'A11

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��V�X�e���G���[MSG�o�͌�X���[
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)), , StartUp.iniValue.LogFilePath)
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�i���R�[�h�쐬
    '�@(�����T�v)EXCEL�ɏo�͂���i���R�[�h��ҏW���ĕԂ��B
    '�@�@I�@�F�@�Ȃ�
    '�@�@R�@�F�@createHinmeiCd      '�ҏW�����i���R�[�h
    '-------------------------------------------------------------------------------
    Private Function createHinmeiCd() As String
        createHinmeiCd = ""

        '�d�l�R�[�h
        If _SerchCriteria.serchSiyoCd.Length = 2 Then
            createHinmeiCd = _SerchCriteria.serchSiyoCd & "-"
        ElseIf _SerchCriteria.serchSiyoCd.Length = 1 Then
            createHinmeiCd = _SerchCriteria.serchSiyoCd.Substring(0, 1) & "*-"
        Else
            createHinmeiCd = "**-"
        End If

        '�i��R�[�h
        If _SerchCriteria.serchHinsyuCd.Length = 3 Then
            createHinmeiCd = createHinmeiCd & _SerchCriteria.serchHinsyuCd & "-"
        ElseIf _SerchCriteria.serchHinsyuCd.Length = 2 Then
            createHinmeiCd = createHinmeiCd & _SerchCriteria.serchHinsyuCd.Substring(0, 2) & "*-"
        ElseIf _SerchCriteria.serchHinsyuCd.Length = 1 Then
            createHinmeiCd = createHinmeiCd & _SerchCriteria.serchHinsyuCd.Substring(0, 1) & "**-"
        Else
            createHinmeiCd = createHinmeiCd & "***-"
        End If

        '���S���R�[�h
        If _SerchCriteria.serchSensinCd.Length = 3 Then
            createHinmeiCd = createHinmeiCd & _SerchCriteria.serchSensinCd & "-"
        ElseIf _SerchCriteria.serchSensinCd.Length = 2 Then
            createHinmeiCd = createHinmeiCd & _SerchCriteria.serchSensinCd.Substring(0, 2) & "*-"
        ElseIf _SerchCriteria.serchSensinCd.Length = 1 Then
            createHinmeiCd = createHinmeiCd & _SerchCriteria.serchSensinCd.Substring(0, 1) & "**-"
        Else
            createHinmeiCd = createHinmeiCd & "***-"
        End If

        '�T�C�Y�R�[�h
        If _SerchCriteria.serchSizeCd.Length = 2 Then
            createHinmeiCd = createHinmeiCd & _SerchCriteria.serchSizeCd & "-"
        ElseIf _SerchCriteria.serchSizeCd.Length = 1 Then
            createHinmeiCd = createHinmeiCd & _SerchCriteria.serchSizeCd.Substring(0, 1) & "*-"
        Else
            createHinmeiCd = createHinmeiCd & "**-"
        End If

        '�F�R�[�h
        If _SerchCriteria.serchColorCd.Length = 3 Then
            createHinmeiCd = createHinmeiCd & _SerchCriteria.serchColorCd
        ElseIf _SerchCriteria.serchColorCd.Length = 2 Then
            createHinmeiCd = createHinmeiCd & _SerchCriteria.serchColorCd.Substring(0, 2) & "*"
        ElseIf _SerchCriteria.serchColorCd.Length = 1 Then
            createHinmeiCd = createHinmeiCd & _SerchCriteria.serchColorCd.Substring(0, 1) & "**"
        Else
            createHinmeiCd = createHinmeiCd & "***"
        End If

    End Function
#End Region

#Region "���[�U��`�֐�:DGV�֘A"

    '------------------------------------------------------------------------------------------------------
    '�I���s�ɒ��F���鏈��
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvTehaiData_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTehaiData.CellEnter
        If _colorCtlFlg Then
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvTehaiData)
            gh.setSelectionRowColor(dgvTehaiData.CurrentCellAddress.Y, _oldRowIndex, StartUp.lCOLOR_BLUE)
        End If
        _oldRowIndex = dgvTehaiData.CurrentCellAddress.Y

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�w�i�F�̐ݒ菈��
    '�@(�����T�v)�s�̔w�i�F��ɒ��F����B
    '�@�@I�@�F�@prmRowIndex     ���݃t�H�[�J�X������s��
    '�@�@I�@�F�@prmOldRowIndex  ���݂̍s�Ɉڂ�O�̍s��
    '------------------------------------------------------------------------------------------------------
    Private Sub setBackcolor(ByVal prmRowIndex As Integer, ByVal prmOldRowIndex As Integer)

        Dim _dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvTehaiData)

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
        dgvTehaiData.Focus()
        dgvTehaiData.CurrentCell = dgvTehaiData(prmColIndex, prmRowIndex)

        '�w�i�F�̐ݒ�
        Call setBackcolor(prmRowIndex, _oldRowIndex)

    End Sub
#End Region

#Region "���[�U��`�֐�:DB�֘A"
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
                Throw New UsrDefException("�o�^����Ă��܂���B", _msgHd.getMSG("noData"))
            End If

            Dim syoriDate As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("SYORI"))
            Dim keikakuDate As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("KEIKAKU"))

            '�uYYYY/MM�v�`���ŕ\��
            lblSyori.Text = syoriDate.Substring(0, 4) & "/" & syoriDate.Substring(4, 2)
            lblKeikaku.Text = keikakuDate.Substring(0, 4) & "/" & keikakuDate.Substring(4, 2)

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@�ꗗ�\��
    '�@(�����T�v)�ꗗ��\������
    '�@�@I�@�F�@prmSql      ��������
    '-------------------------------------------------------------------------------
    Private Sub dispDGV(Optional ByVal prmSql As String = "")
        Try
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & "  M01.NAME1 " & COLDT_TAISYOGAI
            sql = sql & N & " ,T51.TEHAI_NO " & COLDT_TEHAINO
            sql = sql & N & " ,TO_CHAR(TO_DATE(T51.KIBOU_DATE,'YYYYMMDD'),'yy/mm/dd') " & COLDT_SYUTTAIBI
            '2010/11/17 delete start Nakazawa
            'sql = sql & N & " ,TO_CHAR(TO_DATE(T51.NOUKI,'YYYYMMDD'),'yy/mm/dd') " & COLDT_NOUKI
            '2010/11/17 delete end Nakazawa
            sql = sql & N & " ,T51.HINMEI_CD " & COLDT_HINMEICD
            sql = sql & N & " ,T51.HINMEI " & COLDT_HINMEI
            sql = sql & N & " ,T51.TEHAI_SUU " & COLDT_TEHAISUURYOU
            sql = sql & N & " ,T51.TANCYO " & COLDT_TANTYOU
            sql = sql & N & " ,T51.JYOSU " & COLDT_JOUSUU
            sql = sql & N & " ,T51.SIYOUSYO_NO " & COLDT_SIYOUSYONO
            sql = sql & N & " FROM T51TEHAI T51 "
            sql = sql & N & "   LEFT OUTER JOIN M01HANYO M01 ON "
            sql = sql & N & "   T51.GAI_FLG = M01.KAHENKEY "
            sql = sql & N & "   AND M01.KOTEIKEY = '" & COTEI_GAIFLG & "'"
            If Not "".Equals(prmSql) Then
                sql = sql & N & prmSql
            End If
            sql = sql & N & " ORDER BY T51.TEHAI_NO "

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            '���o�f�[�^���ꗗ�ɕ\������
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvTehaiData)
            gh.clearRow()

            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ

                '�C���{�^���E�݌ɕ�[���X�g�o�̓{�^���g�p�s��
                btnSyuusei.Enabled = False
                btnInsatu.Enabled = False

                '����������\��()
                lblKensuu.Text = CStr(iRecCnt) & "��"

                Throw New UsrDefException("�Ώۃf�[�^������܂���B", _msgHd.getMSG("NonData"))
            Else
                '���o�f�[�^������ꍇ�A�o�^�{�^���EEXCEL�{�^����L���ɂ���
                btnSyuusei.Enabled = True
                btnInsatu.Enabled = True
            End If

            dgvTehaiData.DataSource = ds
            dgvTehaiData.DataMember = RS

            '����������\��
            lblKensuu.Text = CStr(iRecCnt) & "��"

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
    '  ���t�`�F�b�N
    '�@(�����T�v)���͂��ꂽ���t�̑召�֌W���`�F�b�N����
    '------------------------------------------------------------------------------------------------------
    Private Sub checkDate()
        Try
            Dim dtKibouFrom As String = Trim(Replace(txtKibouFrom.Text, "/", ""))
            Dim dtKibouTo As String = Trim(Replace(txtKibouTo.Text, "/", ""))
            '2010/11/17 delete start Nakazawa
            'Dim dtNoukiFrom As String = Trim(Replace(txtNoukiFrom.Text, "/", ""))
            'Dim dtNoukiTo As String = Trim(Replace(txtNoukiTo.Text, "/", ""))
            '2010/11/17 delete end Nakazawa

            '��]�o����
            If Not "".Equals(dtKibouFrom) And Not "".Equals(dtKibouTo) Then
                If Date.Compare(Date.Parse(Format(CInt(dtKibouFrom), "0000/00/00")), Date.Parse(Format(CInt(dtKibouTo), "0000/00/00"))) > 0 Then
                    Throw New UsrDefException("��]�o�����̑召�֌W���s���ł��B", _msgHd.getMSG("ErrHaniChk", "��]�o����"), txtKibouFrom)
                End If
            End If

            '2010/11/17 delete start Nakazawa
            '�[��
            'If Not "".Equals(dtNoukiFrom) And Not "".Equals(dtNoukiTo) Then
            '    If Date.Compare(Date.Parse(Format(CInt(dtNoukiFrom), "0000/00/00")), Date.Parse(Format(CInt(dtNoukiTo), "0000/00/00"))) > 0 Then
            '        Throw New UsrDefException("�[���̑召�֌W���s���ł��B", _msgHd.getMSG("ErrHaniChk", "�[��"), txtNoukiFrom)
            '    End If
            'End If
            '2010/11/17 delete end Nakazawa

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub
#End Region

    '-->2010.12.27 add by takagi
    Private Sub dgvTehaiData_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTehaiData.CellDoubleClick
        Call btnSyuusei_Click(Nothing, Nothing)
    End Sub
    '<--2010.12.27 add by takagi

End Class