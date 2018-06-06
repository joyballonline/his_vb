'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���j�R�[�h�I���q���
'    �i�t�H�[��ID�jZC910S_CodeSentaku
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

Public Class ZC910S_CodeSentaku
    Inherits System.Windows.Forms.Form

#Region "���e�����l��`"

    'PG���䕶��
    Private Const RS As String = "RecSet"                       '�f�[�^�Z�b�g�e�[�u����
    Private Const N As String = ControlChars.NewLine            '���s����

    '�ꗗ�f�[�^�Z�b�g�o�C���h��
    Private Const COLDT_CD As String = "dtCD"           '�σL�[
    Private Const COLDT_MEISYO As String = "dtMeisyou"  '����

    '�ꗗ��
    Private Const COLCN_CD As String = "cnCD"           '�σL�[
    Private Const COLCN_MAISYO As String = "cnMeisyou"  '����

#End Region

#Region "�����o�ϐ���`"

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As IfRturnKahenKey

    Dim _dgv As UtilDataGridViewHandler             '�O���b�h�n���h���[

    Private _oldRowIndex As Integer = -1            '�I���s�̔w�i�F��ύX���邽�߂̃t���O��錾
    Private _colorCtlFlg As Boolean = False         '�I���s�̔w�i�F��ύX���邽�߂̃t���O��錾

    Private _koteiKey As String = ""                '�e��ʂ���󂯎�����Œ�L�[�ێ��ϐ�
    Private _kahenKey As String = ""                '�e��ʂ���󂯎�����σL�[�ێ��ϐ�

    Private _backName As String = ""                '�ėp�}�X�^.���v��͖���2��Ԃ��̂ł����ɐݒ肷��
    Private _hinsyuKbnFlg As Boolean = False        '�i��敪�}�X�^�̒l��\������t���O

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
    '�R���X�g���N�^�@ZM110E_Sinki�AZM120E_Syuusei�Ȃǂ���Ă΂��
    '   �����̓p�����^  �FprmRefMsgHd       ���b�Z�[�W�n���h��
    '                     prmRefDbHd        DB�n���h��
    '                     prmForm           �ďo���e�t�H�[��
    '                     prmKoteiKey       �ėp�}�X�^�Œ�L�[(M02�i��敪�������̏ꍇ�͎��v��CD)
    '                     prmKahenKey       �ėp�}�X�^�σL�[(M02�i��敪�������̏ꍇ�͕i��敪)
    '                     prmBackName       �ėp�}�X�^�ԋp�l(���v��̏ꍇ�͖���2��Ԃ��̂ł����Őݒ�B�f�t�H���g�͖���1)
    '                     prmHinsyuKbnFlg   �����Ώ�TB����(True=�i��敪�}�X�^�AFalse=�ėp�}�X�^�B�f�t�H���g��False)
    '   ���o�̓p�����^  �F�Ȃ�
    '   �����\�b�h�߂�l�F�Ȃ�
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmForm As Form, ByVal prmKoteiKey As String, _
                           ByVal prmKahenKey As String, Optional ByVal prmBackName As String = "", Optional ByVal prmHinsyuKbnFlg As Boolean = False)
        Call Me.New()

        '��������
        _msgHd = prmRefMsgHd                                    'MSG�n���h���̐ݒ�
        _db = prmRefDbHd                                        'DB�n���h���̐ݒ�
        _parentForm = prmForm                                   '�e�t�H�[��
        StartPosition = FormStartPosition.CenterScreen          '��ʒ����\��

        _koteiKey = prmKoteiKey             '�e��ʂ���󂯎�����Œ�L�[
        _kahenKey = prmKahenKey             '�e��ʂ���󂯎�����σL�[
        _backName = prmBackName             '�e��ʂɕԂ��l�̔���p�ϐ�
        _hinsyuKbnFlg = prmHinsyuKbnFlg     'True�̏ꍇ�͕i��敪�}�X�^���������Ēl��Ԃ�

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

            Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr '�^�C�g���I�v�V�����\��

            '�����l�ݒ�
            Call dispDGV()

            _colorCtlFlg = True

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

#End Region

#Region "�{�^���C�x���g"

    '------------------------------------------------------------------------------------------------------
    '�I���{�^����������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        Try
            If "".Equals(lblMeisyo.Text) And "".Equals(lblKahenKey.Text) Then
                Throw New UsrDefException("�R�[�h��I�����Ă��������B", _msgHd.getMSG("ErrCodeChk"))
            End If

            '�e�t�H�[���ɒl�n��
            _parentForm.setKahenKey(lblKahenKey.Text, lblMeisyo.Text)

            '���e�t�H�[���\��
            _parentForm.myShow()
            _parentForm.myActivate()
            Me.Close()

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

        '�e�t�H�[���ɒl�n��
        _parentForm.setKahenKey("", "")     '�e�t�H�[�����󂯎��σL�[�����Z�b�g����
        '���e�t�H�[���\��
        _parentForm.myShow()
        _parentForm.myActivate()
        Me.Close()

    End Sub

#End Region

#Region "���[�U��`�֐�:DGV�֘A"

    '-------------------------------------------------------------------------------
    '�@�ꗗ�N���b�N������
    '   �i�����T�v�j�I�����ꂽ���̂����x���ɕ\������B
    '-------------------------------------------------------------------------------
    Private Sub dgvJuyousaki_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvJuyousaki.CellClick

        Try

            Call showDGV(e.RowIndex)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@DGV�\��
    '   �i�����T�v�jDGV���I�����ꂽ�f�[�^�����x���ɕ\������B
    '-------------------------------------------------------------------------------
    Private Sub showDGV(ByVal prmRowIndex As Integer)
        Try

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvJuyousaki)

            '�N���b�N���ꂽ�Z���̓��e�����x���ɕ\��
            lblMeisyo.Text = gh.getCellData(COLDT_MEISYO, prmRowIndex)
            '�e��ʂ֕ԋp����σL�[���\�����x���ɕێ�
            lblKahenKey.Text = gh.getCellData(COLDT_CD, prmRowIndex)

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��V�X�e���G���[MSG�o�͌�X���[
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�ꗗ�s�ړ����̏���
    '�@(�������e)�@�ړ���̃Z���̓��e�����x���ɕ\������
    '�@�@�@�@�@�@�A��ɒ��F����
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvJuyousaki_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvJuyousaki.CellEnter
        Call showDGV(dgvJuyousaki.CurrentCellAddress.Y)

        If _colorCtlFlg Then
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvJuyousaki)
            gh.setSelectionRowColor(dgvJuyousaki.CurrentCellAddress.Y, _oldRowIndex, StartUp.lCOLOR_BLUE)
        End If
        _oldRowIndex = dgvJuyousaki.CurrentCellAddress.Y
    End Sub

#End Region

#Region "���[�U��`�֐�:DB�֘A"

    '------------------------------------------------------------------------------------------------------
    '�@�ꗗ�\������
    '�@(�������e)�e��ʂ���n���ꂽ�Œ�L�[�����Ɉꗗ�\�����s��
    '------------------------------------------------------------------------------------------------------
    Private Sub dispDGV()

        Try

            Dim sql As String = ""
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = Nothing

            'M01�ėp�}�X�^�̒l��\������ꍇ
            If Not _hinsyuKbnFlg Then
                sql = "SELECT "
                sql = sql & N & " KAHENKEY " & COLDT_CD                 '�σL�[

                '�\�������͎��v�悪����2�A����ȊO������1
                '_backName�Ɍ�������񂪎w�肳��Ă���ꍇ�͂��̒l��Ԃ�
                Select Case _backName
                    Case StartUp.HANYO_BACK_NAME1
                        sql = sql & N & " ,NAME1 " & COLDT_MEISYO       '����
                    Case StartUp.HANYO_BACK_NAME2
                        sql = sql & N & " ,NAME2 " & COLDT_MEISYO
                    Case StartUp.HANYO_BACK_NAME3
                        sql = sql & N & " ,NAME3 " & COLDT_MEISYO
                    Case StartUp.HANYO_BACK_NAME4
                        sql = sql & N & " ,NAME4 " & COLDT_MEISYO
                    Case StartUp.HANYO_BACK_NAME5
                        sql = sql & N & " ,NAME5 " & COLDT_MEISYO
                    Case Else
                        sql = sql & N & " ,NAME1 " & COLDT_MEISYO
                End Select
                sql = sql & N & " FROM M01HANYO "
                sql = sql & N & " WHERE KOTEIKEY = '" & _koteiKey & "'"
                sql = sql & N & " ORDER BY KAHENKEY "

                'M02�i��敪�}�X�^�̒l��\������ꍇ
            Else
                sql = "SELECT "
                sql = sql & N & "  HINSYUKBN " & COLDT_CD               '�i��敪
                sql = sql & N & " ,HINSYUKBNNM " & COLDT_MEISYO         '�i��敪��
                sql = sql & N & " FROM M02HINSYUKBN "
                sql = sql & N & " WHERE JUYOUCD = '" & _koteiKey & "'"
                sql = sql & N & " ORDER BY HINSYUKBN "

            End If

            'SQL���s
            ds = _db.selectDB(sql, RS, iRecCnt)
            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                Throw New UsrDefException("�o�^����Ă��܂���B", _msgHd.getMSG("noData"))
            End If

            '���o�f�[�^���ꗗ�ɕ\������
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(Me.dgvJuyousaki)
            gh.clearRow()
            dgvJuyousaki.DataSource = ds
            dgvJuyousaki.DataMember = RS

            lblKensu.Text = CStr(iRecCnt) & "��"

            '�擪�s���F
            gh.setSelectionRowColor(0, 0, StartUp.lCOLOR_BLUE)

            '�e��ʂ���σL�[(�i��敪)���n���ꂽ�ꍇ�A��v����ꗗ�s�Ƀt�H�[�J�X����
            Dim dispCnt As Integer = 0
            If Not "".Equals(_kahenKey) Then
                '�n���ꂽ�σL�[(�i��敪)�ƈꗗ�̉σL�[(�i��敪)����v����Ȃ�
                For i As Integer = 0 To gh.getMaxRow - 1
                    If _kahenKey.Equals(gh.getCellData(COLDT_CD, i)) Then
                        dispCnt = i
                        Exit For
                    End If
                Next
                '�t�H�[�J�X���ړ����A���̍s�𒅐F����
                gh.setCurrentCell(COLCN_CD, dispCnt)
                gh.setSelectionRowColor(dispCnt, 0, StartUp.lCOLOR_BLUE)
                _oldRowIndex = dispCnt
            End If

            '���݃t�H�[�J�X������s�̒l�����x���ɕ\������B
            lblKahenKey.Text = gh.getCellData(COLDT_CD, dispCnt)
            lblMeisyo.Text = gh.getCellData(COLDT_MEISYO, dispCnt)

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

#End Region

    '' 2010/12/15 add start sugano
    Private Sub dgvJuyousaki_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvJuyousaki.CellDoubleClick
        Call btnSelect_Click(sender, e)
    End Sub
    '' 2010/12/15 add end sugano

End Class