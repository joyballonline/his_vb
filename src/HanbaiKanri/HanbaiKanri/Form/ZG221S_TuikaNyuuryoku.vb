'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���j�ǉ��ǉ��o�^
'    �i�t�H�[��ID�jZG221S_TuikaNyuuryoku
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ���{        2010/10/26                 �V�K              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB

Public Class ZG221S_TuikaNyuuryoku
    Inherits System.Windows.Forms.Form

#Region "���e�����l��`"
    'PG���䕶��
    Private Const RS As String = "RecSet"                       '�f�[�^�Z�b�g�e�[�u����
    Private Const N As String = ControlChars.NewLine            '���s����

    '�v���O����ID�iT91���s�����e�[�u���o�^�p�j
    Private Const PGID As String = "ZG221S"
    '�X�V�����iT91���s�����e�[�u���o�^�p�j
    Private Const UPDATECOUNT As Integer = 1

    '���Y�����e�[�u���X�V���e�Œ�l
    Private Const UPDATENYUKOFLG As String = "1"
    Private Const UPDATETAISYOFLG As String = "3"

#End Region

#Region "�����o�ϐ���`"

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As IfRturnUpDDate

    Private _Syori As String
    Private _Keikaku As String

    Private _changeFlg As Boolean = False           '�ύX�t���O
    Private _beforeChange As String = ""            '�ύX�O�̃f�[�^

    '���������i�[�ϐ�
    Private _Updatedata As Updatedata
    Private Structure Updatedata
        Private _TehaiKbn As String            '��z�敪
        Private _JuyouCD As String             '���v��
        Private _Hinsyukbn As String           '�i��

        Public Property TehaiKbn() As String
            Get
                Return _TehaiKbn
            End Get
            Set(ByVal Value As String)
                _TehaiKbn = Value
            End Set
        End Property
        Public Property JuyouCD() As String
            Get
                Return _JuyouCD
            End Get
            Set(ByVal Value As String)
                _JuyouCD = Value
            End Set
        End Property
        Public Property Hinsyukbn() As String
            Get
                Return _Hinsyukbn
            End Get
            Set(ByVal Value As String)
                _Hinsyukbn = Value
            End Set
        End Property
    End Structure

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
    '�R���X�g���N�^�@���Y�ʕ␳��ʂ���Ă΂��
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmForm As Form, ByVal prmSyori As String, ByVal prmKeikaku As String)
        Call Me.New()

        '��������
        _msgHd = prmRefMsgHd                                                'MSG�n���h���̐ݒ�
        _db = prmRefDbHd                                                    'DB�n���h���̐ݒ�
        _parentForm = prmForm                                               '�e�t�H�[��
        _Syori = prmSyori                                                   '�e����󂯎���������N��
        _Keikaku = prmKeikaku                                               '�e����󂯎�����v��N��
        StartPosition = FormStartPosition.CenterScreen                      '��ʒ����\��
    End Sub

#End Region

#Region "Form�C�x���g"
    '-------------------------------------------------------------------------------
    '�@�����J�n�C�x���g
    '-------------------------------------------------------------------------------
    Private Sub ZG221S_TuikaNyuuryoku_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            '�`��֌W�̐ݒ�
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           '�T�C�Y���ύX���ꂽ�Ƃ��ɁA�R���g���[�����R���g���[�����̂��ĕ`�悷�邩�ǂ����������l��ݒ�
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '�`��̓o�b�t�@�Ŏ��s����A������ɁA���ʂ���ʂɏo�͂����悤�ݒ�
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  '�R���g���[���́A��ʂɒ��ڂł͂Ȃ��A�܂��o�b�t�@�ɕ`�悳��܂��B����ɂ��A�������}���邱�Ƃ��ł��܂��B
            Me.SetStyle(ControlStyles.UserPaint, True)              '�R���g���[���́A�I�y���[�e�B���O �V�X�e���ɂ���Ăł͂Ȃ��A�Ǝ��ɕ`�悳���悤�ݒ�
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   '�R���g���[���̓E�B���h�E ���b�Z�[�W WM_ERASEBKGND �𖳎�����悤�ɐݒ�

            Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr '�^�C�g���I�v�V�����\��

            '������
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

            '�x�����b�Z�[�W
            If _changeFlg Then
                Dim rtn As DialogResult = _msgHd.dspMSG("confirmDgvEdit")   '�ҏW���̓��e���j������܂��B��낵���ł����H
                If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If

            '�e�t�H�[���\��
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
    '�@�o�^�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnTouroku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTouroku.Click
        Try
            '�K�{���̓`�F�b�N
            Call checkTouroku()

            '�o�^�m�F���b�Z�[�W
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmInsert")    '�o�^���܂��B
            If rtn <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If

            ' ����Wait�J�[�\����ێ�
            Dim preCursor As Cursor = Me.Cursor
            ' �J�[�\����ҋ@�J�[�\���ɕύX
            Me.Cursor = Cursors.WaitCursor

            Try
                '�����J�n���Ԃƒ[��ID�̎擾
                Dim dStartSysdate As Date = Now()                           '�����J�n����
                Dim sPCName As String = UtilClass.getComputerName           '�[��ID

                '�o�^���e�擾
                Call getUpdateData()

                '�g�����U�N�V�����J�n
                _db.beginTran()

                '���Y�����e�[�u���̒ǉ�����
                Call AddT21Seisanm(dStartSysdate, sPCName)

                '�����I�������̎擾
                Dim dFinishSysdate As Date = Now()

                '���s�����e�[�u���̍X�V����
                Call updT91Rireki(sPCName, dStartSysdate, dFinishSysdate)

                '�g�����U�N�V�����I��
                _db.commitTran()

                '�e�t�H�[���ɒl�n��
                _parentForm.setUpDDate(dStartSysdate)

                '�������b�Z�[�W
                _msgHd.dspMSG("completeInsert")

                '�ύX�t���O�𖳌��ɂ���
                _changeFlg = False

                '�e�t�H�[���\��
                _parentForm.myShow()
                _parentForm.myActivate()

                Me.Close()

            Finally
                If _db.isTransactionOpen = True Then
                    _db.rollbackTran()                          '���[���o�b�N
                End If
                ' �J�[�\�������ɖ߂�
                Me.Cursor = preCursor
            End Try
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

#End Region

#Region "���[�U��`�֐�:��ʐ���"
    '------------------------------------------------------------------------------------------------------
    '   �t�H�[��������
    '   �i�����T�v�j�@�e�R���g���[���̏����ݒ���s��
    '   �����̓p�����^   �F�Ȃ�
    '   �����\�b�h�߂�l �F�Ȃ�
    '------------------------------------------------------------------------------------------------------
    Private Sub initForm()
        Try
            '�e�R���g���[���̏�����
            txtKibou.Text = ""
            txtHinmeiCD.Text = ""
            lblHinmei.Text = ""
            txtSeiban.Text = ""
            txtSuuryou.Text = ""
        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��V�X�e���G���[MSG�o�͌�X���[
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�R���g���[���L�[�����C�x���g
    '�@(�����T�v)�G���^�[�{�^���������Ɏ��̃R���g���[���Ɉڂ�
    '-------------------------------------------------------------------------------
    Private Sub txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtKibou.KeyPress, _
                                                                                                                txtHinmeiCD.KeyPress, _
                                                                                                                txtSeiban.KeyPress, _
                                                                                                                txtSuuryou.KeyPress
        Try

            UtilClass.moveNextFocus(Me, e) '���̃R���g���[���ֈړ�����

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
    Private Sub txt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtKibou.GotFocus, _
                                                                                          txtHinmeiCD.GotFocus, _
                                                                                          txtSeiban.GotFocus, _
                                                                                          txtSuuryou.GotFocus
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
    '�@�i���\��
    '�@(�����N��)�i����\������
    '-------------------------------------------------------------------------------
    Private Sub dispHinmei()
        Try
            '�i���擾
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & "  M11.TT_HINMEI " & "HINMEI"          '�i��
            sql = sql & N & " ,M12.KHINMEICD " & "KHINMEICD"          '�v��i���R�[�h
            sql = sql & N & " FROM M11KEIKAKUHIN M11 "
            sql = sql & N & "   LEFT OUTER JOIN M12SYUYAKU M12 ON "
            sql = sql & N & "   M11.TT_KHINMEICD = M12.KHINMEICD "
            sql = sql & N & " WHERE M12.HINMEICD = '" & txtHinmeiCD.Text & "'"

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                Throw New UsrDefException("�i���R�[�h���v��Ώەi�}�X�^�ɓo�^����Ă��܂���B", _msgHd.getMSG("NonKeikakuhinMst"), txtHinmeiCD)
            End If

            '�i���\��
            lblHinmei.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("HINMEI"))
            lblKhinmeicd.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("KHINMEICD"))

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�e�L�X�g�{�b�N�X�R���g���[���̃t�H�[�J�X�擾���C�x���g
    '�@(�����T�v)�e�L�X�g�{�b�N�X�R���g���[���̃t�H�[�J�X�擾���̏���
    '-------------------------------------------------------------------------------
    Private Sub txt_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKibou.Enter, _
                                                                                          txtHinmeiCD.Enter, _
                                                                                          txtSeiban.Enter, _
                                                                                          txtSuuryou.Enter
        Try
            '�ҏW�O�̓��e��ێ�
            If sender.GetType Is GetType(TextBox) Then
                Dim txt As TextBox = DirectCast(sender, TextBox)
                _beforeChange = txt.Text
            ElseIf sender.GetType Is GetType(CustomControl.TextBoxNum) Then
                Dim txtnum As CustomControl.TextBoxNum = DirectCast(sender, CustomControl.TextBoxNum)
                _beforeChange = txtnum.Text
            ElseIf sender.GetType Is GetType(CustomControl.TextBoxDate) Then
                Dim txtdate As CustomControl.TextBoxDate = DirectCast(sender, CustomControl.TextBoxDate)
                _beforeChange = txtdate.Text
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�e�L�X�g�{�b�N�X�R���g���[���̃t�H�[�J�X�������C�x���g
    '�@(�����T�v)�e�L�X�g�R���g���[���̃t�H�[�J�X�������̏���
    '-------------------------------------------------------------------------------
    Private Sub txt_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKibou.Leave, _
                                                                                              txtHinmeiCD.Leave, _
                                                                                              txtSeiban.Leave, _
                                                                                              txtSuuryou.Leave
        Try

            '�ҏW�O�ƒl���ς���Ă����ꍇ�A�t���O�𗧂Ă�
            If sender.GetType Is GetType(TextBox) Then
                Dim txt As TextBox = DirectCast(sender, TextBox)
                If Not _beforeChange.Equals(txt.Text) Then
                    _changeFlg = True
                End If
                Select Case txt.Name
                    Case txtHinmeiCD.Name
                        If "".Equals(txtHinmeiCD.Text) Then
                            lblHinmei.Text = ""
                        Else
                            '���̓`�F�b�N
                            Call checkHinCD()
                            '�i���\��
                            Call dispHinmei()
                        End If
                End Select
            ElseIf sender.GetType Is GetType(CustomControl.TextBoxNum) Then
                Dim txtnum As CustomControl.TextBoxNum = DirectCast(sender, CustomControl.TextBoxNum)
                If Not _beforeChange.Equals(txtnum.Text) Then
                    _changeFlg = True
                End If
            ElseIf sender.GetType Is GetType(CustomControl.TextBoxDate) Then
                Dim txtdate As CustomControl.TextBoxDate = DirectCast(sender, CustomControl.TextBoxDate)
                If Not _beforeChange.Equals(txtdate.Text) Then
                    _changeFlg = True
                End If
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub
#End Region

#Region "���[�U��`�֐�:DB�֘A"
    '------------------------------------------------------------------------------------------------------
    '�@���s�����e�[�u���̍X�V����
    '  (�����T�v)���s�����e�[�u���Ƀ��R�[�h��ǉ�����
    '�@�@I�@�F�@prmPCName      �@�@�[��ID
    '�@�@I�@�F�@prmStartDate       �����J�n����
    '�@�@I�@�F�@prmFinishDate      �����I������
    '------------------------------------------------------------------------------------------------------
    Private Sub updT91Rireki(ByVal prmPCName As String, ByVal prmStartDate As Date, ByVal prmFinishDate As Date)
        Try
            '�o�^����
            Dim sql As String = ""
            sql = "INSERT INTO T91RIREKI ("
            sql = sql & N & "  SNENGETU"                                                    '�����N��
            sql = sql & N & ", KNENGETU"                                                    '�v��N��
            sql = sql & N & ", PGID"                                                        '�@�\ID
            sql = sql & N & ", SDATESTART"                                                  '�����J�n����
            sql = sql & N & ", SDATEEND"                                                    '�����I������
            sql = sql & N & ", KENNSU1"                                                     '�����P�i�X�V�����j
            sql = sql & N & ", UPDNAME"                                                     '�[��ID
            sql = sql & N & ", UPDDATE"                                                     '�X�V����
            sql = sql & N & ") VALUES ("
            sql = sql & N & "  '" & _Syori & "'"                                            '�����N��
            sql = sql & N & ", '" & _Keikaku & "'"                                          '�v��N��
            sql = sql & N & ", '" & PGID & "'"                                              '�@�\ID
            sql = sql & N & ", TO_DATE('" & prmStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "   '�����J�n����
            sql = sql & N & ", TO_DATE('" & prmFinishDate & "', 'YYYY/MM/DD HH24:MI:SS') "  '�����I������
            sql = sql & N & ", " & UPDATECOUNT                                              '�����P�i�X�V�����j
            sql = sql & N & ", '" & prmPCName & "'"                                         '�[��ID
            sql = sql & N & ", TO_DATE('" & prmStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "   '�X�V����
            sql = sql & N & " )"
            _db.executeDB(sql)

        Catch ex As Exception
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�o�^���e�̎擾����
    '  (�����T�v)���Y�����e�[�u���ɐݒ肷����e���擾
    '------------------------------------------------------------------------------------------------------
    Private Sub getUpdatedata()
        Try
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & "  TT_TEHAI_KBN " & "TEHAIKBN"       '��z�敪
            sql = sql & N & " ,TT_JUYOUCD " & "JUYOUCD"          '���v��
            sql = sql & N & " ,TT_HINSYUKBN " & "HINSYUKBN"      '�i��
            sql = sql & N & " FROM M11KEIKAKUHIN "
            sql = sql & N & " WHERE TT_KHINMEICD = '" & lblKhinmeicd.Text & "'"

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
                Throw New UsrDefException("�o�^����Ă��܂���B", _msgHd.getMSG("noData"))
            End If

            '�o�^���e�ҏW
            _Updatedata.TehaiKbn = _db.rmNullStr(ds.Tables(RS).Rows(0)("TEHAIKBN"))
            _Updatedata.JuyouCD = _db.rmNullStr(ds.Tables(RS).Rows(0)("JUYOUCD"))
            _Updatedata.Hinsyukbn = _db.rmNullStr(ds.Tables(RS).Rows(0)("HINSYUKBN"))

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@���Y�����e�[�u���ǉ�����
    '------------------------------------------------------------------------------------------------------
    Private Sub AddT21Seisanm(ByVal prmSysdate As Date, ByVal prmPCName As String)
        Try
            '�ǉ�����
            Dim sql As String = ""
            sql = "INSERT INTO T21SEISANM ("
            sql = sql & N & "  HINMEICD"                                                  '���i���R�[�h
            sql = sql & N & ", SIYOU_CD"                                                  '�d�l�R�[�h
            sql = sql & N & ", HIN_CD"                                                    '�i��R�[�h
            sql = sql & N & ", SENSIN_CD"                                                 '���S���R�[�h
            sql = sql & N & ", SIZE_CD"                                                   '�T�C�Y�R�[�h
            sql = sql & N & ", COLOR_CD"                                                  '�F�R�[�h
            sql = sql & N & ", HINMEI"                                                    '�i��
            sql = sql & N & ", KHINMEICD"                                                 '�v��i���R�[�h
            sql = sql & N & ", NEN"                                                       '�N
            sql = sql & N & ", TEHAINO"                                                   '��z��
            sql = sql & N & ", RENBAN"                                                    '�A��
            sql = sql & N & ", SEIBAN"                                                    '����
            sql = sql & N & ", TEHAI_KBN"                                                 '��z�敪
            sql = sql & N & ", TANCYO"                                                    '�P��
            sql = sql & N & ", JYOSU"                                                     '��
            sql = sql & N & ", TEHAISU"                                                   '��z��
            sql = sql & N & ", KYUTTAIBI"                                                 '��]�o����
            sql = sql & N & ", YSYUTTAIBI"                                                '�\��o����
            sql = sql & N & ", NYUUKOSU"                                                  '���ɐ�
            sql = sql & N & ", MINOUZAN"                                                  '���[�c
            sql = sql & N & ", SMIKOMISU"                                                 '���Y������
            sql = sql & N & ", NENGETSU"                                                  '�N��
            sql = sql & N & ", JUYOU_CD"                                                  '���v��
            sql = sql & N & ", HINSYU_KBN"                                                '�i��敪
            sql = sql & N & ", NYUKO_FLG"                                                 '�����t���O
            sql = sql & N & ", TAISYO_FLG"                                                '�Ώۃt���O
            sql = sql & N & ", SAKUSEI_KBN"                                               '�쐬�敪
            sql = sql & N & ", NYUKOBI"                                                   '���ɓ�
            sql = sql & N & ", UPDNAME"                                                   '�[��ID
            sql = sql & N & ", UPDDATE )"                                                 '�X�V����
            sql = sql & N & " VALUES ("
            '-->2010.12.12 chg by takagi
            'sql = sql & N & "  '" & txtHinmeiCD.Text & "'"                                         '���i���R�[�h
            'sql = sql & N & ", '" & txtHinmeiCD.Text.Substring(0, 2) & "'"                         '�d�l�R�[�h
            'sql = sql & N & ", '" & txtHinmeiCD.Text.Substring(2, 3) & "'"                         '�i��R�[�h
            'sql = sql & N & ", '" & txtHinmeiCD.Text.Substring(5, 3) & "'"                         '���S���R�[�h
            'sql = sql & N & ", '" & txtHinmeiCD.Text.Substring(8, 2) & "'"                         '�T�C�Y�R�[�h
            'sql = sql & N & ", '" & txtHinmeiCD.Text.Substring(10, 3) & "'"                        '�F�R�[�h
            'sql = sql & N & ", '" & lblHinmei.Text & "'"                                           '�i��
            'sql = sql & N & ", '" & lblKhinmeicd.Text & "'"                                        '�v��i���R�[�h
            'sql = sql & N & ", ''"                                                                 '�N
            'sql = sql & N & ", ''"                                                                 '��z��
            'sql = sql & N & ", ''"                                                                 '�A��
            'sql = sql & N & ", '" & txtSeiban.Text & "'"                                           '����
            'sql = sql & N & ", '" & _Updatedata.TehaiKbn & "'"                                     '��z�敪
            'sql = sql & N & ", 0"                                                                  '�P��
            'sql = sql & N & ", 0"                                                                  '��
            'sql = sql & N & ", 0"                                                                  '��z��
            'sql = sql & N & ", '" & Trim(Replace(txtKibou.Text, "/", "")) & "'"                    '��]�o����
            'sql = sql & N & ", ''"                                                                 '�\��o����
            'sql = sql & N & ", 0"                                                                  '���ɐ�
            'sql = sql & N & ", 0"                                                                  '���[�c
            'sql = sql & N & ", '" & txtSuuryou.Text & "'"                                          '���Y������
            'sql = sql & N & ", '" & Trim(Replace(txtKibou.Text, "/", "")).Substring(0, 6) & "'"    '�N��
            'sql = sql & N & ", '" & _Updatedata.JuyouCD & "'"                                      '���v��
            'sql = sql & N & ", '" & _Updatedata.Hinsyukbn & "'"                                     '�i��敪
            'sql = sql & N & ", ''"                                                                 '�����t���O
            'sql = sql & N & ", '" & UPDATENYUKOFLG & "'"                                           '�Ώۃt���O"
            'sql = sql & N & ", '" & UPDATETAISYOFLG & "'"                                          '�쐬�敪
            'sql = sql & N & ", ''"                                                                 '���ɓ�
            'sql = sql & N & ", '" & prmPCName & "'"                                                '�[��ID
            'sql = sql & N & ", TO_DATE('" & prmSysdate & "', 'YYYY/MM/DD HH24:MI:SS') "            '�X�V����
            sql = sql & N & "  '" & _db.rmSQ(txtHinmeiCD.Text) & "'"                                         '���i���R�[�h
            sql = sql & N & ", '" & _db.rmSQ(txtHinmeiCD.Text.Substring(0, 2)) & "'"                         '�d�l�R�[�h
            sql = sql & N & ", '" & _db.rmSQ(txtHinmeiCD.Text.Substring(2, 3)) & "'"                         '�i��R�[�h
            sql = sql & N & ", '" & _db.rmSQ(txtHinmeiCD.Text.Substring(5, 3)) & "'"                         '���S���R�[�h
            sql = sql & N & ", '" & _db.rmSQ(txtHinmeiCD.Text.Substring(8, 2)) & "'"                         '�T�C�Y�R�[�h
            sql = sql & N & ", '" & _db.rmSQ(txtHinmeiCD.Text.Substring(10, 3)) & "'"                        '�F�R�[�h
            sql = sql & N & ", '" & _db.rmSQ(lblHinmei.Text) & "'"                                           '�i��
            sql = sql & N & ", '" & _db.rmSQ(lblKhinmeicd.Text) & "'"                                        '�v��i���R�[�h
            sql = sql & N & ", ''"                                                                 '�N
            sql = sql & N & ", ''"                                                                 '��z��
            sql = sql & N & ", ''"                                                                 '�A��
            sql = sql & N & ", '" & _db.rmSQ(txtSeiban.Text) & "'"                                           '����
            sql = sql & N & ", '" & _db.rmSQ(_Updatedata.TehaiKbn) & "'"                                     '��z�敪
            sql = sql & N & ", 0"                                                                  '�P��
            sql = sql & N & ", 0"                                                                  '��
            sql = sql & N & ", 0"                                                                  '��z��
            sql = sql & N & ", '" & Trim(Replace(txtKibou.Text, "/", "")) & "'"                    '��]�o����
            sql = sql & N & ", ''"                                                                 '�\��o����
            sql = sql & N & ", 0"                                                                  '���ɐ�
            sql = sql & N & ", 0"                                                                  '���[�c
            sql = sql & N & ", '" & _db.rmSQ(CInt(txtSuuryou.Text)) & "'"                                          '���Y������
            sql = sql & N & ", '" & Trim(Replace(txtKibou.Text, "/", "")).Substring(0, 6) & "'"    '�N��
            sql = sql & N & ", '" & _db.rmSQ(_Updatedata.JuyouCD) & "'"                                      '���v��
            sql = sql & N & ", '" & _db.rmSQ(_Updatedata.Hinsyukbn) & "'"                                     '�i��敪
            sql = sql & N & ", ''"                                                                 '�����t���O
            sql = sql & N & ", '" & _db.rmSQ(UPDATENYUKOFLG) & "'"                                           '�Ώۃt���O"
            sql = sql & N & ", '" & _db.rmSQ(UPDATETAISYOFLG) & "'"                                          '�쐬�敪
            sql = sql & N & ", ''"                                                                 '���ɓ�
            sql = sql & N & ", '" & _db.rmSQ(prmPCName) & "'"                                                '�[��ID
            sql = sql & N & ", TO_DATE('" & prmSysdate & "', 'YYYY/MM/DD HH24:MI:SS') "            '�X�V����
            '<--2010.12.12 chg by takagi
            sql = sql & N & ")"
            _db.executeDB(sql)
        Catch ex As Exception
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub
#End Region

#Region "���[�U��`�֐�:�`�F�b�N����"

    '------------------------------------------------------------------------------------------------------
    '  ���̓`�F�b�N
    '�@(�����T�v)�i���R�[�h�ɓ��͂��ꂽ���e�̃`�F�b�N���s��
    '------------------------------------------------------------------------------------------------------
    Private Sub checkHinCD()
        Try
            '�i���R�[�h�`�F�b�N
            If Not txtHinmeiCD.TextLength.Equals(13) Then
                '�G���[���b�Z�[�W�̕\��
                Throw New UsrDefException("�i���R�[�h�̌������Ⴂ�܂��B", _msgHd.getMSG("HinmeiLengthNG", "�y �i���R�[�h �z"), txtHinmeiCD)
            End If

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '  �o�^�`�F�b�N
    '�@(�����T�v)�e���ڂ̕K�{���ڃ`�F�b�N���s��
    '------------------------------------------------------------------------------------------------------
    Private Sub checkTouroku()
        Try
            Call chekuHissu(txtKibou, Trim(Replace(txtKibou.Text, "/", "")), "��]�o����")
            Call chekuHissu(txtHinmeiCD, txtHinmeiCD.Text, "�i���R�[�h")
            Call chekuHissu(txtSeiban, txtSeiban.Text, "����")
            Call chekuHissu(txtSuuryou, txtSuuryou.Text, "����")

            '�󗓃`�F�b�N
            If "".Equals(lblHinmei.Text) Then
                '�G���[���b�Z�[�W�̕\��
                Throw New UsrDefException("�i���R�[�h���v��Ώەi�}�X�^�ɓo�^����Ă��܂���B", _msgHd.getMSG("NonKeikakuhinMst"), txtHinmeiCD)
            End If

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '  �K�{���̓`�F�b�N
    '�@(�����T�v)�e�L�X�g�����͂���Ă��邩�`�F�b�N����
    '�@�@I�@�F�@prmSender              �`�F�b�N����I�u�W�F�N�g
    '�@�@I�@�F�@prmControlName         �`�F�b�N����I�u�W�F�N�g��
    '------------------------------------------------------------------------------------------------------
    Private Sub chekuHissu(ByVal prmSender As System.Object, ByVal prmChktxt As String, ByVal prmControlName As String)
        Try
            '�K�{���̓`�F�b�N
            If "".Equals(prmChktxt) Then
                '�G���[���b�Z�[�W�̕\��
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput", "�y " & prmControlName & "�z"), prmSender)
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