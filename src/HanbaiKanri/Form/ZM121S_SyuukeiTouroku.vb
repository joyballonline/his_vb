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
'�@(1)   ���V        2010/09/03                 �V�K
'�@(2)   ����        2014/06/04                 �ύX�@�ޗ��[�}�X�^�iMPESEKKEI�j�e�[�u���ύX�Ή�            
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView

Public Class ZM121S_SyuukeiTouroku
    Inherits System.Windows.Forms.Form

#Region "���e�����l��`"
    '-------------------------------------------------------------------------------
    '   �萔��`
    '-------------------------------------------------------------------------------

    'PG���䕶�� 
    Private Const N As String = ControlChars.NewLine            '���s����
    Private Const RS As String = "RecSet"                       '���R�[�h�Z�b�g�e�[�u��
    Private Const RS2 As String = "RecSet2"                     '���R�[�h�Z�b�g�e�[�u��
    Private Const PGID As String = "ZM121S"                     'T91�ɓo�^����PGID

    'M12�����p���e����
    Private Const COLDT_KHINMEICD As String = "dtSHinmeiCD"
    Private Const COLDT_KHINMEI As String = "dtSHinmei"

    '�ޗ��[�����p���e����
    Private Const INT_SEQNO As Integer = 1

#End Region

#Region "�����o�[�ϐ��錾"

    '-------------------------------------------------------------------------------
    '   �ϐ���`
    '-------------------------------------------------------------------------------

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As Form

    Private _oldRowIndex As Integer = -1            '�I���s�̔w�i�F��ύX���邽�߂̕ϐ�
    Private _colorCtlFlg As Boolean = False         '�I���s�̔w�i�F��ύX���邽�߂̃t���O

    Private _kHinmeiCD As String = ""               '�e��ʂ���󂯎�����v��i���R�[�h

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
    '�R���X�g���N�^�@���j���[����Ă΂��
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmForm As Form, ByVal prmKHinmeiCD As String)
        Call Me.New()

        '��������
        _msgHd = prmRefMsgHd                                                'MSG�n���h���̐ݒ�
        _db = prmRefDbHd                                                    'DB�n���h���̐ݒ�
        _parentForm = prmForm                                               '�e�t�H�[��
        StartPosition = FormStartPosition.CenterScreen                      '��ʒ����\��
        _kHinmeiCD = prmKHinmeiCD

    End Sub
#End Region

#Region "Form�C�x���g"

    '-------------------------------------------------------------------------------
    '�@�����J�n�C�x���g
    '-------------------------------------------------------------------------------
    Private Sub ZM121S_SyuukeiTouroku_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            Call dispDGV()

            '�ꗗ���F�t���O
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
    '   �ǉ��{�^������
    '   (�����T�v)���͂��ꂽ�R�[�h�̑��݃`�F�b�N���s���A�i���Ƌ��Ɉꗗ�ɒǉ�����B
    '------------------------------------------------------------------------------------------------------
    Private Sub btnTuika_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTuika.Click

        Try
            '�W�v�Ώەi���R�[�h���̓`�F�b�N
            Call checkInputSHinmeiCD()

            '�W�v�Ώەi���R�[�h�d���`�F�b�N
            Call checkKHinmeiCDRepeat(txtSSiyo.Text, txtSHinsyu.Text, txtSSensin.Text, txtSSize.Text, txtSColor.Text)

            '�d���L�[�`�F�b�N
            Call checkHinmeiCDRepeat()

            _colorCtlFlg = False

            '�ꗗ�ǉ��\��
            Call insertDGV()

            _colorCtlFlg = True

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '   �폜�{�^������
    '   (�����T�v)�I�����ꂽ�s���ꗗ����폜����B
    '------------------------------------------------------------------------------------------------------
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
    '�@�o�^�{�^������
    '�@(�����T�v)�ꗗ�ɕ\������Ă���R�[�h��M12�ɓo�^����B
    '-------------------------------------------------------------------------------
    Private Sub btnKakutei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKakutei.Click
        Try

            '�d���L�[�`�F�b�N
            'Call checkHinmeiCDRepeat()

            '�o�^�m�F�_�C�A���O�\��
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmInsert")   '�o�^���܂��B��낵���ł����H
            If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            '�}�E�X�J�[�\�������v
            Me.Cursor = Cursors.WaitCursor

            '�f�[�^�ǉ�
            Call insertDB()

            '�}�E�X�J�[�\�����
            Me.Cursor = Cursors.Arrow

            '�������b�Z�[�W
            Call _msgHd.dspMSG("completeInsert")          '�o�^���������܂����B

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        Finally
            '�}�E�X�J�[�\�����
            Me.Cursor = Cursors.Arrow
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�߂�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnModoru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModoru.Click

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

#Region "���[�U��`�֐�:��ʐ���"

    '-------------------------------------------------------------------------------
    ' �@�t�H�[�J�X�ړ�
    '�@�i�����T�v�j�������ڂ̃e�L�X�g�{�b�N�X�ŃG���^�[�L�[�������͎��̃R���g���[���ֈړ�����B
    '-------------------------------------------------------------------------------
    Private Sub txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSSiyo.KeyPress, _
                                                                                                    txtSHinsyu.KeyPress, _
                                                                                                    txtSSensin.KeyPress, _
                                                                                                    txtSSize.KeyPress, _
                                                                                                    txtSColor.KeyPress

        UtilClass.moveNextFocus(Me, e)  '���̃R���g���[���ֈړ�����

    End Sub

    '-------------------------------------------------------------------------------
    '�@�R���g���[���S�I��
    '�@(�����T�v)�R���g���[���ړ����ɑS�I����Ԃɂ���
    '-------------------------------------------------------------------------------
    Private Sub serch_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSSiyo.GotFocus, _
                                                                                            txtSHinsyu.GotFocus, _
                                                                                            txtSSensin.GotFocus, _
                                                                                            txtSSize.GotFocus, _
                                                                                            txtSColor.GotFocus
        UtilClass.selAll(sender)

    End Sub

#End Region

#Region "���[�U��`�֐�:DGV�֘A"

    '------------------------------------------------------------------------------------------------------
    '�I���s�ɒ��F���鏈��
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvSHinmei_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvSHinmei.SelectionChanged
        If _colorCtlFlg Then
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSHinmei)
            gh.setSelectionRowColor(dgvSHinmei.CurrentCellAddress.Y, _oldRowIndex, StartUp.lCOLOR_BLUE)
        End If
        _oldRowIndex = dgvSHinmei.CurrentCellAddress.Y
    End Sub

#End Region

#Region "���[�U��`�֐�:DB�֘A"

    '-------------------------------------------------------------------------------
    '�@��ʕ\��
    '-------------------------------------------------------------------------------
    Private Sub dispDGV()

        Try

            'M12
            Dim Sql As String = ""
            Sql = Sql & N & "SELECT "
            Sql = Sql & N & " M12.HINMEICD " & COLDT_KHINMEICD
            Sql = Sql & N & ",(MPE.HINSYU_MEI "
            Sql = Sql & N & "		|| MPE.SAIZU_MEI"
            Sql = Sql & N & "		|| MPE.IRO_MEI) " & COLDT_KHINMEI
            Sql = Sql & N & " FROM M12SYUYAKU M12 "
            '2014/06/04 UPD-S Sugano
            'Sql = Sql & N & "	 INNER JOIN MPESEKKEI MPE ON"
            'Sql = Sql & N & "	 M12.KHINMEICD = '" & _db.rmNullStr(_kHinmeiCD) & "'"
            'Sql = Sql & N & " WHERE MPE.SHIYO || "
            'Sql = Sql & N & "       LPAD(TO_CHAR(MPE.HINSYU)  ,3,'0') || "
            'Sql = Sql & N & "       LPAD(TO_CHAR(MPE.SENSHIN)  ,3,'0') || "
            'Sql = Sql & N & "       LPAD(TO_CHAR(MPE.SAIZU)  ,2,'0')  || "
            'Sql = Sql & N & "       LPAD(TO_CHAR(MPE.IRO)  ,3,'0')  = '" & _db.rmNullStr(_kHinmeiCD) & "'"
            'Sql = Sql & N & " AND MPE.SEQ_NO = " & INT_SEQNO
            'Sql = Sql & N & " AND MPE.SEKKEI_HUKA = "
            'Sql = Sql & N & "           (SELECT MAX(SEKKEI_HUKA) FROM MPESEKKEI MPE"
            'Sql = Sql & N & "               WHERE SHIYO || "
            'Sql = Sql & N & "                   LPAD(TO_CHAR(MPE.HINSYU)  ,3,'0') || "
            'Sql = Sql & N & "                   LPAD(TO_CHAR(MPE.SENSHIN)  ,3,'0') || "
            'Sql = Sql & N & "                   LPAD(TO_CHAR(MPE.SAIZU)  ,2,'0')  || "
            'Sql = Sql & N & "                   LPAD(TO_CHAR(MPE.IRO)  ,3,'0')  = '" & _db.rmNullStr(_kHinmeiCD) & "') "
            'Sql = Sql & N & " AND NOT M12.KHINMEICD = M12.HINMEICD "
            Sql = Sql & N & "	 INNER JOIN (SELECT M1.* FROM MPESEKKEI1 M1 "
            Sql = Sql & N & "	             INNER JOIN (SELECT SHIYO,HINSYU,SENSHIN,SAIZU,IRO,SEKKEI_FUKA,MAX(SEKKEI_KAITEI) KAITEI FROM MPESEKKEI1 WHERE SEKKEI_FUKA = 'A' "
            Sql = Sql & N & "	                         GROUP BY SHIYO,HINSYU,SENSHIN,SAIZU,IRO,SEKKEI_FUKA) M2 "
            Sql = Sql & N & "	             ON  M1.SHIYO = M2.SHIYO "
            Sql = Sql & N & "	             AND M1.HINSYU = M2.HINSYU "
            Sql = Sql & N & "	             AND M1.SENSHIN = M2.SENSHIN "
            Sql = Sql & N & "	             AND M1.SAIZU = M2.SAIZU "
            Sql = Sql & N & "	             AND M1.IRO = M2.IRO "
            Sql = Sql & N & "	             AND M1.SEKKEI_FUKA = M2.SEKKEI_FUKA "
            Sql = Sql & N & "	             AND M1.SEKKEI_KAITEI = M2.KAITEI ) MPE"
            Sql = Sql & N & " ON M12.HINMEICD = MPE.SHIYO || MPE.HINSYU || MPE.SENSHIN || MPE.SAIZU || MPE.IRO "
            Sql = Sql & N & " WHERE NOT M12.KHINMEICD = M12.HINMEICD "
            Sql = Sql & N & " AND M12.KHINMEICD = '" & _db.rmNullStr(_kHinmeiCD) & "'"
            '2014/06/04 UPD-E Sugano

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            'If iRecCnt <= 0 Then                    '���o���R�[�h���P�����Ȃ��ꍇ
            '    Throw New UsrDefException("�o�^����Ă��܂���B", _msgHd.getMSG("noData"))
            'End If

            '���o�f�[�^���ꗗ�ɕ\������
            Dim gh As UtilMDL.DataGridView.UtilDataGridViewHandler = New UtilMDL.DataGridView.UtilDataGridViewHandler(Me.dgvSHinmei)
            gh.clearRow()
            dgvSHinmei.DataSource = ds
            dgvSHinmei.DataMember = RS

            '�����\��
            lblKensuu.Text = CStr(iRecCnt) & "��"

            _colorCtlFlg = True

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '�@�ꗗ�s�ǉ�
    '�@�����T�v�j���͂��ꂽ�W�v�Ώەi���R�[�h�Ƃ���ɑΉ����閼�̂��ꗗ�ɕ\������B
    '�@�@�����̓p�����[�^�F�Ȃ�
    '�@�@���֐��߂�l�@�@�F�Ȃ�
    '-------------------------------------------------------------------------------
    Private Sub insertDGV()
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
            SQL = SQL & N & " '" & sHinmeiCD & "'" & COLDT_KHINMEICD
            SQL = SQL & N & " ,HINSYU_MEI || SAIZU_MEI || IRO_MEI " & COLDT_KHINMEI
            '2014/06/04 UPD-S Sugano
            'SQL = SQL & N & " FROM MPESEKKEI "
            'SQL = SQL & N & "   WHERE SHIYO = '" & _db.rmSQ(txtSSiyo.Text) & "'  "
            SQL = SQL & N & " FROM MPESEKKEI1 "
            SQL = SQL & N & "   WHERE SHIYO = '" & sSiyo & "'  "
            '2014/06/04 UPD-E Sugano
            SQL = SQL & N & "   AND HINSYU = '" & _db.rmSQ(txtSHinsyu.Text) & "'  "
            SQL = SQL & N & "   AND SENSHIN = '" & _db.rmSQ(txtSSensin.Text) & "'  "
            SQL = SQL & N & "   AND SAIZU = '" & _db.rmSQ(txtSSize.Text) & "'  "
            SQL = SQL & N & "   AND IRO = '" & _db.rmSQ(txtSColor.Text) & "' "
            '2014/06/04 UPD-S Sugano
            'SQL = SQL & N & "   AND SEQ_NO = " & INT_SEQNO
            'SQL = SQL & N & "   AND SEKKEI_HUKA = "
            'SQL = SQL & N & "           (SELECT MAX(SEKKEI_HUKA) FROM MPESEKKEI "
            'SQL = SQL & N & "               WHERE SHIYO = '" & _db.rmSQ(txtSSiyo.Text) & "'  "
            SQL = SQL & N & "   AND SEKKEI_FUKA = 'A'"
            SQL = SQL & N & "   AND SEKKEI_KAITEI = (SELECT MAX(SEKKEI_KAITEI) FROM MPESEKKEI1 "
            SQL = SQL & N & "               WHERE SHIYO = '" & sSiyo & "'  "
            '2014/06/04 UPD-E Sugano
            SQL = SQL & N & "               AND HINSYU = '" & _db.rmSQ(txtSHinsyu.Text) & "'  "
            SQL = SQL & N & "               AND SENSHIN = '" & _db.rmSQ(txtSSensin.Text) & "'  "
            SQL = SQL & N & "               AND SAIZU = '" & _db.rmSQ(txtSSize.Text) & "'  "
            '2014/06/04 UPD-S Sugano
            'SQL = SQL & N & "               AND IRO = '" & _db.rmSQ(txtSColor.Text) & "')"
            SQL = SQL & N & "               AND IRO = '" & _db.rmSQ(txtSColor.Text) & "'"
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
            Dim rowDt As Object() = {_db.rmNullStr(ds.Tables(RS2).Rows(0)(COLDT_KHINMEICD)), _
                                            _db.rmNullStr(ds.Tables(RS2).Rows(0)(COLDT_KHINMEI))}

            '�ǉ�����R�[�h�ƈꗗ�ɕ\������Ă���R�[�h�̏d���`�F�b�N
            For i As Integer = 0 To dgvSHinmei.RowCount - 1
                If _db.rmNullStr(ds.Tables(RS2).Rows(0)(COLDT_KHINMEICD)).Equals(_db.rmNullStr(dgvSHinmei(0, i).Value)) Then
                    txtSSiyo.Focus()
                    Throw New UsrDefException("���łɈꗗ�ɒǉ����ꂽ�W�v�Ώەi���R�[�h�ł��B", _
                                                            _msgHd.getMSG("alreadyAddSyukeiItiran"))
                End If
            Next

            '�O���b�h�Ƀo�C���h���ꂽ�f�[�^�Z�b�g�擾
            Dim wkDs As DataSet = dgvSHinmei.DataSource

            '���̃f�[�^�Z�b�g�ɍs�ǉ�
            wkDs.Tables(RS).Rows.Add(rowDt)

            '�\�[�g����
            Dim dtTmp As DataTable = wkDs.Tables(RS).Clone()

            '�\�[�g���ꂽ�f�[�^�r���[�̍쐬
            Dim dv As DataView = New DataView(wkDs.Tables(RS))
            dv.Sort = COLDT_KHINMEICD

            '�\�[�g���ꂽ���R�[�h�̃R�s�[
            For Each drv As DataRowView In dv
                dtTmp.ImportRow(drv.Row)
            Next

            '�o�C���h�p�f�[�^�Z�b�g����
            Dim bindDs As DataSet = New DataSet
            bindDs.Tables.Add(dtTmp)

            '�ăo�C���h
            dgvSHinmei.DataSource = bindDs
            dgvSHinmei.DataMember = RS

            '�ꗗ�̌�����\������
            lblKensuu.Text = CStr(dgvSHinmei.RowCount) & "��"

            '�ǉ������s�Ƀt�H�[�J�X
            For c As Integer = 0 To dgvSHinmei.RowCount - 1
                If _db.rmNullStr(ds.Tables(RS2).Rows(0)(COLDT_KHINMEICD)).Equals(_db.rmNullStr(dgvSHinmei(0, c).Value)) Then
                    Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSHinmei)
                    dgvSHinmei.Focus()
                    dgvSHinmei.CurrentCell = dgvSHinmei(0, c)
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
            Dim wkDs As DataSet = dgvSHinmei.DataSource

            '�I���s�폜
            wkDs.Tables(RS).Rows.RemoveAt(dgvSHinmei.CurrentCellAddress.Y)

            '�ăo�C���h
            dgvSHinmei.DataSource = wkDs
            dgvSHinmei.DataMember = RS

            '�ꗗ�̌�����\������
            lblKensuu.Text = CStr(dgvSHinmei.RowCount) & "��"

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
            If _db.rmSQ(txtSSiyo.Text).Length = 1 Then
                siyoCD = siyoCD & _db.rmSQ(txtSSiyo.Text) & " "
            Else
                siyoCD = _db.rmSQ(txtSSiyo.Text)
            End If

            '�v��i���R�[�h�쐬
            Dim kHinmei As String = ""
            kHinmei = siyoCD & _db.rmSQ(txtSHinsyu.Text) & _db.rmSQ(txtSSensin.Text) & _db.rmSQ(txtSSize.Text) & _db.rmSQ(txtSColor.Text)

            '�X�V�J�n�������擾
            Dim updStartDate As Date = Now

            '�g�����U�N�V�����J�n
            _db.beginTran()

            'M12�̍폜
            Dim sql As String = ""
            sql = sql & N & " DELETE FROM M12SYUYAKU "
            sql = sql & N & "   WHERE KHINMEICD = '" & _kHinmeiCD & "'"
            _db.executeDB(sql)

            'M12 �e�R�[�h�̓o�^
            sql = ""
            sql = sql & N & " INSERT INTO M12SYUYAKU ("
            sql = sql & N & "  HINMEICD "
            sql = sql & N & " ,KHINMEICD "
            sql = sql & N & " ,UPDNAME "
            sql = sql & N & " ,UPDDATE) "
            sql = sql & N & " VALUES ( "
            sql = sql & N & "   '" & _kHinmeiCD & "', "
            sql = sql & N & "   '" & _kHinmeiCD & "', "
            sql = sql & N & " '" & _tanmatuID & "', "                                       '�[��ID
            sql = sql & N & " TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS')) "   '�X�V����
            _db.executeDB(sql)

            'M12 �ꗗ�ɕ\������Ă���v��i���R�[�h�̓o�^
            For loopCnt As Integer = 0 To dgvSHinmei.RowCount - 1

                sql = ""
                sql = sql & N & " INSERT INTO M12SYUYAKU ("
                sql = sql & N & "  HINMEICD "
                sql = sql & N & " ,KHINMEICD "
                sql = sql & N & " ,UPDNAME "
                sql = sql & N & " ,UPDDATE) "
                sql = sql & N & " VALUES ( "
                sql = sql & N & "   '" & _db.rmSQ(dgvSHinmei(0, loopCnt).Value) & "', "
                sql = sql & N & "   '" & _kHinmeiCD & "', "
                sql = sql & N & " '" & _tanmatuID & "', "                                       '�[��ID
                sql = Sql & N & " TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS')) "   '�X�V����
                _db.executeDB(Sql)
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

#End Region

#Region "���[�U��`�֐�:�`�F�b�N����"

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
    '   �i�����T�v�j�@�e��ʂœn����Ă����v��i���R�[�h�Əd�����Ă��Ȃ����`�F�b�N����
    '               �A�W�v�i���}�X�^���������A�i���R�[�h�̏d�����Ȃ����`�F�b�N����
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

            '�v��i���R�[�h�Ƃ̏d���`�F�b�N
            If _kHinmeiCD.Equals(kHinmei) Then
                txtSSiyo.Focus()
                Throw New UsrDefException("���͂��ꂽ�R�[�h�͌v��i���R�[�h�Ɠ����ł��B", _
                                    _msgHd.getMSG("equalsKHinmeiCD", "�v��i���R�[�h�@�F�@" & _kHinmeiCD))
            End If

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
                txtSSiyo.Focus()
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
    '�@ �i���R�[�h�d���`�F�b�N
    '   �i�����T�v�j�v��Ώەi�}�X�^���������A�i���R�[�h�̏d�����Ȃ����`�F�b�N����
    '-------------------------------------------------------------------------------
    Private Sub checkHinmeiCDRepeat()
        Try

            Dim sSiyo As String = _db.rmSQ(txtSSiyo.Text)
            If _db.rmSQ(txtSSiyo.Text).Length = 1 Then
                sSiyo = sSiyo & " "
            End If


            'M11
            Dim sql As String = ""
            sql = ""
            sql = sql & N & " SELECT "
            sql = sql & N & " * "
            sql = sql & N & " FROM M11KEIKAKUHIN "
            sql = sql & N & "   WHERE TT_H_SIYOU_CD = '" & sSiyo & "' "
            sql = sql & N & "   AND TT_H_HIN_CD = '" & _db.rmSQ(txtSHinsyu.Text) & "' "
            sql = sql & N & "   AND TT_H_SENSIN_CD = '" & _db.rmSQ(txtSSensin.Text) & "' "
            sql = sql & N & "   AND TT_H_SIZE_CD = '" & _db.rmSQ(txtSSize.Text) & "' "
            sql = sql & N & "   AND TT_H_COLOR_CD = '" & _db.rmSQ(txtSColor.Text) & "' "

            Dim iRecCnt As Integer
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If Not iRecCnt = 0 Then     '���o���R�[�h������0���ȊO�̏ꍇ
                txtSSiyo.Focus()
                Throw New UsrDefException("�i���R�[�h�͊��ɓo�^����Ă��܂��B", _msgHd.getMSG("alreadyHinmei"))
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