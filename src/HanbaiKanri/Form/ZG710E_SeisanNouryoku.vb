'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���j���Y�\�͐ݒ���
'    �i�t�H�[��ID�jZG710E_SeisanNouryoku
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ���        2010/11/25                 �V�K              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView

Public Class ZG710E_SeisanNouryoku
    Inherits System.Windows.Forms.Form

#Region "���e�����l��`"

    'PG���䕶��
    Private Const RS As String = "RecSet"                       '�f�[�^�Z�b�g�e�[�u����
    Private Const N As String = ControlChars.NewLine            '���s����

    '�ꗗ�f�[�^�o�C���h��
    Private Const COLDT_KOUTEI As String = "dtKoutei"            '�H��
    Private Const COLDT_MACHINENAME As String = "dtMachineName"  '�@�B���L��
    Private Const COLDT_TUUJOUDAY As String = "dtTuujouDay"      '�ʏ�ғ��i���j
    Private Const COLDT_TUUJOUMON As String = "dtTuujouMon"      '�ʏ�ғ��i���j
    Private Const COLDT_DOYOUDAY As String = "dtDoyouDay"        '�y�j�x�o�i���j
    Private Const COLDT_DOYOUMON As String = "dtDoyouMon"        '�y�j�x�o�i���j
    Private Const COLDT_NICHIYOUDAY As String = "dtNichiyouDay"  '���j�x�o�i���j
    Private Const COLDT_NICHIYOUMON As String = "dtNichiyouMon"  '���j�x�o�i���j
    Private Const COLDT_KEISANTI As String = "dtKeisanti"        '�v�ZMCH
    Private Const COLDT_TYOUSEI As String = "dtTyousei"          '����MCH
    Private Const COLDT_TEKIYOU As String = "dtTekiyou"          '���Y�\��MCH

    Private Const COLDT_TUUJOU As String = "dtTuujou"      '�ʏ�ғ�
    Private Const COLDT_DOYOU As String = "dtDoyou"        '�y�j�x�o
    Private Const COLDT_NICHIYOU As String = "dtNichiyou"  '���j�x�o
    Private Const COLDT_BUMON As String = "dtBumon"        '��������

    '�ꗗ�O���b�h��
    Private Const COLCN_KOUTEI As String = "cnKoutei"             '�H��
    Private Const COLCN_MACHINENAME As String = "cnMachineName"   '�@�B���L��
    Private Const COLCN_TUUJOUDAY As String = "cnTuujouDay"       '�ʏ�ғ��i���j
    Private Const COLCN_TUUJOUMON As String = "cnTuujouMon"       '�ʏ�ғ��i���j
    Private Const COLCN_DOYOUDAY As String = "cnDoyouDay"         '�y�j�x�o�i���j
    Private Const COLCN_DOYOUMON As String = "cnDoyouMon"         '�y�j�x�o�i���j
    Private Const COLCN_NICHIYOUDAY As String = "cnNichiyouDay"   '���j�x�o�i���j
    Private Const COLCN_NICHIYOUMON As String = "cnNichiyouMon"   '���j�x�o�i���j
    Private Const COLCN_KEISANTI As String = "cnKeisanti"         '�v�ZMCH
    Private Const COLCN_TYOUSEI As String = "cnTyousei"           '����MCH
    Private Const COLCN_TEKIYOU As String = "cnTekiyou"           '���Y�\��MCH

    '�ꗗ�O���b�h��
    Private Const COLNO_KOUTEI As Integer = 0          '�H��
    Private Const COLNO_MACHINENAME As Integer = 1     '�@�B���L��
    Private Const COLNO_TUUJOUDAY As Integer = 2       '�ʏ�ғ��i���j
    Private Const COLNO_TUUJOUMON As Integer = 3       '�ʏ�ғ��i���j
    Private Const COLNO_DOYOUDAY As Integer = 4        '�y�j�x�o�i���j
    Private Const COLNO_DOYOUMON As Integer = 5        '�y�j�x�o�i���j
    Private Const COLNO_NICHIYOUDAY As Integer = 6     '���j�x�o�i���j
    Private Const COLNO_NICHIYOUMON As Integer = 7     '���j�x�o�i���j
    Private Const COLNO_KEISANTI As Integer = 8        '�v�ZMCH
    Private Const COLNO_TYOUSEI As Integer = 9         '����MCH
    Private Const COLNO_TEKIYOU As Integer = 10        '���Y�\��MCH

    Private Const PGID As String = "ZG710E"
    Private Const BMNCD_DENSEN As String = "3"         '��������i�d���j
    Private Const BMNCD_TUUSIN As String = "1"         '��������i�ʐM�j
    Private Const TYOUSEIMAX As Single = 999.9         '����MCH�̍ő�l
    Private Const TYOUSEIMIN As Single = -999.9        '����MCH�̍ŏ��l
    Private Const KOUTEI As String = "12"              '�H���̌Œ�L�[
    Private Const MAXSORT As Integer = 99999999        '�\�[�g�ԍ��̍ő�l

#End Region

#Region "�����o�ϐ���`"

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As IfRturnSeisanNouryoku
    Private _oyaKensu As Integer
    Private _oldRowIndex As Integer = -1            '�I���s�̔w�i�F��ύX���邽�߂̃t���O��錾
    Private _colorCtlFlg As Boolean = False         '�I���s�̔w�i�F��ύX���邽�߂̃t���O��錾
    Private _chkCellVO As UtilDgvChkCellVO          '�ꗗ�̓��͐����p
    Private _errSet As UtilDataGridViewHandler.dgvErrSet                '�G���[�������Ƀt�H�[�J�X����Z���ʒu
    Private _nyuuryokuErrFlg As Boolean = False                         '���̓G���[�L���t���O

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
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmForm As IfRturnSeisanNouryoku, ByVal prmOyaKensu As Integer)

        Call Me.New()

        '��������
        _msgHd = prmRefMsgHd                                                'MSG�n���h���̐ݒ�
        _db = prmRefDbHd                                                    'DB�n���h���̐ݒ�
        _parentForm = prmForm                                               '�e�t�H�[��
        _oyaKensu = prmOyaKensu
        StartPosition = FormStartPosition.CenterScreen                      '��ʒ����\��

    End Sub

#End Region

#Region "Form�C�x���g"

    '-------------------------------------------------------------------------------
    '�@�����J�n�C�x���g
    '-------------------------------------------------------------------------------
    Private Sub ZG710E_SeisanNouryoku_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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

        '����ʂ��I�����A���׎R�ό��ʊm�F�i�H���j��ʂɖ߂�B
        _parentForm.myShow()
        _parentForm.myActivate()

        Me.Close()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@MCH�Z�o�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnSansyutu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSansyutu.Click

        Try
            '�ғ����̓��̓`�F�b�N
            Call checkWorkingDay()

            'MCH�Z�o
            Dim dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanNouryoku)
            Dim maxRow As Integer = dgv.getMaxRow()         '�f�[�^�O���b�h�̍ő�s��
            Dim dTuujou As Single = CSng(txtDTuujou.Text)   '�i�d���j�ʏ�ғ���
            Dim dDoyou As Single = CSng(txtDDoyou.Text)     '�i�d���j�y�j�ғ���
            Dim dNitiyou As Single = CSng(txtDNitiyou.Text) '�i�d���j���j�ғ���
            Dim tTuujou As Single = CSng(txtTTuujou.Text)   '�i�ʐM�j�ʏ�ғ���
            Dim tDoyou As Single = CSng(txtTDoyou.Text)     '�i�ʐM�j�y�j�ғ���
            Dim tNitiyou As Single = CSng(txtTNitiyou.Text) '�i�ʐM�j���j�ғ���
            Dim tyouseiMch As Single                        '����MCH
            Dim bumon As String                             '��������
            Dim tuujou As Single                            '�ʏ�ғ����ԁi���j
            Dim doyou As Single                             '�y�j�ғ����ԁi���j
            Dim nitiyou As Single                           '���j�ғ����ԁi���j

            For i As Integer = 0 To maxRow - 1
                '�ғ����i���ԁj��̒l�`�F�b�N
                If String.Empty.Equals(_db.rmNullStr(dgv.getCellData(COLDT_TUUJOUDAY, i))) Or _
                    String.Empty.Equals(_db.rmNullStr(dgv.getCellData(COLDT_DOYOUDAY, i))) Or _
                    String.Empty.Equals(_db.rmNullStr(dgv.getCellData(COLDT_NICHIYOUDAY, i))) Then
                    '�v�ZMCH����ɂȂ�\��������Ȃ�v�Z�͍s��Ȃ�
                    Continue For
                End If

                '����MCH����ʂ���擾����
                If String.Empty.Equals(_db.rmNullStr(dgv.getCellData(COLDT_TYOUSEI, i))) Then
                    tyouseiMch = 0.0
                Else
                    tyouseiMch = _db.rmNullStr(dgv.getCellData(COLDT_TYOUSEI, i))
                End If

                '�@�B�����̂�萻��������擾
                bumon = _db.rmNullStr(dgv.getCellData(COLDT_MACHINENAME, i)).Substring(0, 1)

                If BMNCD_DENSEN.Equals(bumon) Then
                    tuujou = dTuujou * CSng(_db.rmNullStr(dgv.getCellData(COLDT_TUUJOUDAY, i)))
                    doyou = dDoyou * CSng(_db.rmNullStr(dgv.getCellData(COLDT_DOYOUDAY, i)))
                    nitiyou = dNitiyou * CSng(_db.rmNullStr(dgv.getCellData(COLDT_NICHIYOUDAY, i)))
                Else
                    tuujou = tTuujou * CSng(_db.rmNullStr(dgv.getCellData(COLDT_TUUJOUDAY, i)))
                    doyou = tDoyou * CSng(_db.rmNullStr(dgv.getCellData(COLDT_DOYOUDAY, i)))
                    nitiyou = tNitiyou * CSng(_db.rmNullStr(dgv.getCellData(COLDT_NICHIYOUDAY, i)))
                End If

                '�f�[�^�O���b�h�Ɋi�[
                dgv.setCellData(COLDT_TUUJOUMON, i, tuujou)
                dgv.setCellData(COLDT_DOYOUMON, i, doyou)
                dgv.setCellData(COLDT_NICHIYOUMON, i, nitiyou)
                dgv.setCellData(COLDT_KEISANTI, i, tuujou + doyou + nitiyou)
                dgv.setCellData(COLDT_TEKIYOU, i, tuujou + doyou + nitiyou + tyouseiMch)
            Next

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
            '�ꗗ�̃f�[�^���̎擾
            Dim dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanNouryoku)
            Dim lMaxCnt As Long = dgv.getMaxRow '�f�[�^�O���b�h�̍ő�s��

            '�o�^�`�F�b�N
            Call checkTouroku(lMaxCnt)

            '�o�^�m�F���b�Z�[�W
            If _oyaKensu > 0 Then
                '�e�t�H�[���̈ꗗ������0���傫���ꍇ�i�e��ʍĕ\���̎|�̃��b�Z�[�W�\���j
                Dim rtn As DialogResult = _msgHd.dspMSG("confirmRegistReDisp")
                If rtn <> Windows.Forms.DialogResult.Yes Then
                    Exit Sub
                End If
            Else
                '�e�t�H�[���̈ꗗ������0�̏ꍇ
                Dim rtn As DialogResult = _msgHd.dspMSG("confirmRegist")
                If rtn <> Windows.Forms.DialogResult.Yes Then
                    Exit Sub
                End If
            End If

            '�g�����U�N�V�����J�n
            _db.beginTran()

            '�}�E�X�J�[�\�������v
            Me.Cursor = Cursors.WaitCursor

            'DB�o�^����
            Call registDB()

            '�g�����U�N�V�����I��
            _db.commitTran()

            '�}�E�X�J�[�\�����
            Me.Cursor = Cursors.Arrow

            _parentForm.setRegist(True)

            '�������b�Z�[�W
            _msgHd.dspMSG("completeInsert")

            '�e�t�H�[���\��
            _parentForm.myShow()
            _parentForm.myActivate()
            Me.Close()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        Finally
            If _db.isTransactionOpen = True Then
                _db.rollbackTran()                          '���[���o�b�N
            End If
            '�}�E�X�J�[�\�����
            Me.Cursor = Cursors.Arrow
        End Try
    End Sub

#End Region

#Region "�f�[�^�O���b�h�֐�"

    '-------------------------------------------------------------------------------
    '   �ꗗ�@�ҏW�`�F�b�N�iEditingControlShowing�C�x���g�j
    '   �i�����T�v�j���͂̐�����������
    '-------------------------------------------------------------------------------
    Private Sub dgvSeisanNouryoku_EditingControlShowing(ByVal sender As Object, _
                                              ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs _
                                              ) Handles dgvSeisanNouryoku.EditingControlShowing

        Try
            Dim dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanNouryoku) 'DGV�n���h���̐ݒ�
            '������MCH�̏ꍇ
            If dgvSeisanNouryoku.CurrentCell.ColumnIndex = COLNO_TYOUSEI Then

                '���O���b�h�ɁA���l���̓��[�h�̐�����������
                _chkCellVO = dgv.chkCell(sender, e, UtilDataGridViewHandler.chkType.Num_M)

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
    Private Sub dgvSeisanNouryoku_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvSeisanNouryoku.DataError

        Try
            e.Cancel = False    '�ҏW���[�h�I��

            Dim dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanNouryoku)
            Dim inputStr As String = _db.rmNullStr(dgvSeisanNouryoku.EditingControl.Text)       '����MCH�ɓ��͂��ꂽ������

            '������MCH�̏ꍇ�A�O���b�h�ɂ͐��l���̓��[�h(0�`9)�̐����������Ă���̂ŁA�����̉���
            If dgvSeisanNouryoku.CurrentCell.ColumnIndex = COLNO_TYOUSEI Then
                dgv.AfterchkCell(_chkCellVO)
            End If

            '����MCH����A������̏ꍇ�A����MCH��Null���Z�b�g
            dgv.setCellData(COLDT_TYOUSEI, dgvSeisanNouryoku.CurrentCell.RowIndex, DBNull.Value)

            If String.Empty.Equals(inputStr) = False Then
                '����MCH����̏ꍇ�G���[���b�Z�[�W�\��
                Throw New UsrDefException("���p�����̂ݓ��͉\�ł��B", _msgHd.getMSG("onlyAcceptNumeric"))
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   �ꗗ�̃Z���l�ύX��
    '-------------------------------------------------------------------------------
    Private Sub dgvSeisanNouryoku_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSeisanNouryoku.CellEndEdit

        Try
            Dim dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanNouryoku) 'DGV�n���h���̐ݒ�

            '���l���̓��[�h(0�`9)�̐������������Ă���ꍇ�́A�����̉���
            If dgvSeisanNouryoku.CurrentCell.ColumnIndex = COLNO_TYOUSEI Then
                dgv.AfterchkCell(_chkCellVO)
            End If

            '�ύX�����s���擾����
            Dim RowNo As Integer = dgvSeisanNouryoku.CurrentCell.RowIndex

            '�ύX�s�̒���MCH
            Dim tyousei As Single

            '����MCH����Ȃ�0�ƌ��Ȃ��Čv�Z����
            If String.Empty.Equals(_db.rmNullStr(dgv.getCellData(COLDT_TYOUSEI, RowNo))) Then
                tyousei = 0.0
            Else
                tyousei = _db.rmNullStr(dgv.getCellData(COLDT_TYOUSEI, RowNo))
                '����MCH�l�̏���E�������`�F�b�N
                If tyousei > TYOUSEIMAX Or tyousei < TYOUSEIMIN Then
                    Call _msgHd.dspMSG("over3Keta")     '��������𒴂��Ă����ꍇ
                    dgv.setCellData(COLDT_TYOUSEI, RowNo, DBNull.Value)                             '����MCH����ɂ���
                    If Not "".Equals(_db.rmNullStr(dgv.getCellData(COLDT_KEISANTI, RowNo))) Then
                        dgv.setCellData(COLDT_TEKIYOU, RowNo, _db.rmNullStr(dgv.getCellData(COLDT_KEISANTI, RowNo)))   '���Y�\��MCH���v�ZMCH�Ɠ��l�ɂ���
                    End If
                    Exit Sub
                End If
            End If

            '�ύX�s�̌v�ZMCH����̏ꍇ�A���Y�\��MCH�͋�ɍX�V���ďI��
            If String.Empty.Equals(_db.rmNullStr(dgv.getCellData(COLDT_KEISANTI, RowNo))) Then
                dgv.setCellData(COLDT_TEKIYOU, RowNo, DBNull.Value)
                Exit Sub
            End If

            '�ύX�s�̌v�ZMCH���擾
            Dim keisan As Single = _db.rmNullStr(dgv.getCellData(COLDT_KEISANTI, RowNo))
            '���Y�\��MCH���X�V
            dgv.setCellData(COLDT_TEKIYOU, RowNo, tyousei + keisan)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�@�I���s�ɒ��F���鏈��
    '�@�@(�����T�v�j�I���s�ɒ��F����
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvSeisanNouryoku_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvSeisanNouryoku.SelectionChanged

        Try
            Dim dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanNouryoku)

            If _colorCtlFlg Then
                dgv.setSelectionRowColor(dgvSeisanNouryoku.CurrentCellAddress.Y, _oldRowIndex, StartUp.lCOLOR_BLUE)
            End If
            _oldRowIndex = dgvSeisanNouryoku.CurrentCellAddress.Y

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

#End Region

#Region "���[�U��`�֐�"

    '-------------------------------------------------------------------------------
    '�@��ʋN����
    '-------------------------------------------------------------------------------
    Private Sub initForm()

        Try
            '�����N���A�v��N���\��
            Call dispDate()

            '�ғ������\��
            Call dispWorkingdays()

            _colorCtlFlg = False
            '�ꗗ��ʕ\��
            Call dispDGV()
            _colorCtlFlg = True

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '�@�R���g���[���L�[�����C�x���g
    '�@(�����T�v)�G���^�[�{�^���������Ɏ��̃R���g���[���Ɉڂ�
    '-------------------------------------------------------------------------------
    Private Sub txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDTuujou.KeyPress, _
                                                                                                                txtDDoyou.KeyPress, _
                                                                                                                txtDNitiyou.KeyPress, _
                                                                                                                txtTTuujou.KeyPress, _
                                                                                                                txtTDoyou.KeyPress, _
                                                                                                                txtTNitiyou.KeyPress
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
    Private Sub txt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDTuujou.GotFocus, _
                                                                                          txtDDoyou.GotFocus, _
                                                                                          txtDNitiyou.GotFocus, _
                                                                                          txtTTuujou.GotFocus, _
                                                                                          txtTDoyou.GotFocus, _
                                                                                          txtTNitiyou.GotFocus
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
    '�@�ғ������̓`�F�b�N
    '-------------------------------------------------------------------------------
    Private Sub checkWorkingDay()

        Try
            '�K�{���̓`�F�b�N
            If String.Empty.Equals(Trim(txtDTuujou.Text)) Then
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput"), txtDTuujou)
            End If
            If String.Empty.Equals(Trim(txtDDoyou.Text)) Then
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput"), txtDDoyou)
            End If
            If String.Empty.Equals(Trim(txtDNitiyou.Text)) Then
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput"), txtDNitiyou)
            End If
            If String.Empty.Equals(Trim(txtTTuujou.Text)) Then
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput"), txtTTuujou)
            End If
            If String.Empty.Equals(Trim(txtTDoyou.Text)) Then
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput"), txtTDoyou)
            End If
            If String.Empty.Equals(Trim(txtTNitiyou.Text)) Then
                Throw New UsrDefException("�K�{���͍��ڂł��B", _msgHd.getMSG("requiredImput"), txtTNitiyou)
            End If

            '�����l�`�F�b�N
            If checkSeisuu(txtDTuujou.Text) = False Then
                Throw New UsrDefException("0�ȏ�̐��̐�������͂��Ă��������B", _msgHd.getMSG("inputOverZero"), txtDTuujou)
            End If
            If checkSeisuu(txtDDoyou.Text) = False Then
                Throw New UsrDefException("0�ȏ�̐��̐�������͂��Ă��������B", _msgHd.getMSG("inputOverZero"), txtDDoyou)
            End If
            If checkSeisuu(txtDNitiyou.Text) = False Then
                Throw New UsrDefException("0�ȏ�̐��̐�������͂��Ă��������B", _msgHd.getMSG("inputOverZero"), txtDNitiyou)
            End If
            If checkSeisuu(txtTTuujou.Text) = False Then
                Throw New UsrDefException("0�ȏ�̐��̐�������͂��Ă��������B", _msgHd.getMSG("inputOverZero"), txtTTuujou)
            End If
            If checkSeisuu(txtTDoyou.Text) = False Then
                Throw New UsrDefException("0�ȏ�̐��̐�������͂��Ă��������B", _msgHd.getMSG("inputOverZero"), txtTDoyou)
            End If
            If checkSeisuu(txtTNitiyou.Text) = False Then
                Throw New UsrDefException("0�ȏ�̐��̐�������͂��Ă��������B", _msgHd.getMSG("inputOverZero"), txtTNitiyou)
            End If

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    ' �@0�ȏ�̐����`�F�b�N�֐�
    '   (�����T�v)�p�����[�^��0�ȏ�̐��̐����ł��邩�m�F����
    '�@�@I�@�F�@prmInput     �@�@ ���͂��ꂽ������
    '    R�@�F�@Boolean           0�ȏ�̐��̐����̏ꍇTrue�A�����ł͂Ȃ��ꍇFalse
    '-------------------------------------------------------------------------------
    Private Function checkSeisuu(ByVal prmInput As String) As Boolean

        Dim retFlg As Boolean = True    '�ԋp�p�t���O
        Dim value As Integer            '���l�ϊ��̖߂�l

        If Integer.TryParse(Trim(prmInput), value) Then '���l�ɕϊ��ł����ꍇ
            If value < 0 Then   '�}�C�i�X�l�Ȃ�NG
                retFlg = False
            End If
        Else    '���l�ɕϊ��ł��Ȃ������ꍇ
            retFlg = False
        End If

        Return retFlg

    End Function

    '------------------------------------------------------------------------------------------------------
    '  �o�^�`�F�b�N
    '�@(�����T�v)��ʂ�DB�o�^���e���ݒ肳��Ă��邩�`�F�b�N����
    '�@�@I�@�F�@prmMaxCnt     �@�@ �ꗗ�̌���
    '------------------------------------------------------------------------------------------------------
    Private Sub checkTouroku(ByRef prmMaxCnt As Long)

        Try
            Dim dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanNouryoku)

            Dim tuujou As String    '�ʏ�ғ��i���j
            Dim doyou As String     '�y�j�x�o�i���j
            Dim nitiyou As String   '���j�x�o�i���j
            Dim seisan As String    '���Y�\��MCH
            Dim tyousei As Single   '����MCH

            For i As Integer = 0 To prmMaxCnt - 1
                tuujou = _db.rmNullStr(dgv.getCellData(COLDT_TUUJOUMON, i))       '�ʏ�ғ��i���j
                doyou = _db.rmNullStr(dgv.getCellData(COLDT_DOYOUMON, i))         '�y�j�x�o�i���j
                nitiyou = _db.rmNullStr(dgv.getCellData(COLDT_NICHIYOUMON, i))    '���j�x�o�i���j
                seisan = _db.rmNullStr(dgv.getCellData(COLDT_TEKIYOU, i))         '���Y�\��MCH

                '�K�{���̓`�F�b�N
                If String.Empty.Equals(tuujou) Or String.Empty.Equals(doyou) Or String.Empty.Equals(nitiyou) Or String.Empty.Equals(seisan) Then
                    Throw New UsrDefException("MCH���Z�o���Ă��������B", _msgHd.getMSG("calculateMch"), btnSansyutu)
                End If

                '����MCH����ł͂Ȃ��ꍇ�A�͈̓`�F�b�N���s��
                If String.Empty.Equals(_db.rmNullStr(dgv.getCellData(COLDT_TYOUSEI, i))) = False Then
                    tyousei = _db.rmNullStr(dgv.getCellData(COLDT_TYOUSEI, i))
                    '����MCH�l�̏���E�������`�F�b�N
                    If tyousei > TYOUSEIMAX Or tyousei < TYOUSEIMIN Then
                        dgv.setCurrentCell(dgv.readyForErrSet(i, COLCN_TYOUSEI))
                        If _colorCtlFlg Then
                            dgv.setSelectionRowColor(dgvSeisanNouryoku.CurrentCellAddress.Y, _oldRowIndex, StartUp.lCOLOR_BLUE)
                        End If
                        _oldRowIndex = dgvSeisanNouryoku.CurrentCellAddress.Y

                        '�G���[���b�Z�[�W�\��
                        '-->2010.12.17 chg by takagi #13
                        'Throw New UsrDefException("�������͂R���ȓ��œ��͂��ĉ������B", _msgHd.getMSG("over3Keta", "�y '����MCH' �F" & i + 1 & "�s�ځz"))
                        Throw New UsrDefException("�������͂R���ȓ��œ��͂��ĉ������B", _msgHd.getMSG("over3Keta"))
                        '<--2010.12.17 chg by takagi #13
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

            Dim syoriDate As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("SYORI"))     '�����N��
            Dim keikakuDate As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("KEIKAKU")) '�v��N��

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
    '�@�ғ������\��
    '�@(�����T�v)T63���d���E�ʐM�̉ғ��������擾���A��ʂɕ\������
    '-------------------------------------------------------------------------------
    Private Sub dispWorkingdays()

        Try
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & " TUUJOUD " & COLDT_TUUJOU         '�ʏ�ғ�
            sql = sql & N & " ,DOYOUD " & COLDT_DOYOU          '�y�j�x�o
            sql = sql & N & " ,NITIYOUD " & COLDT_NICHIYOU     '���j�x�o
            sql = sql & N & " ,BMNCD " & COLDT_BUMON           '��������
            sql = sql & N & " FROM T63KADOUBI "
            sql = sql & N & " ORDER BY BMNCD"

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            '���o���R�[�h��2���łȂ��ꍇ
            If iRecCnt <> 2 Then
                '�ғ������͋�\��
                txtDTuujou.Text = String.Empty
                txtDDoyou.Text = String.Empty
                txtDNitiyou.Text = String.Empty
                txtTTuujou.Text = String.Empty
                txtTDoyou.Text = String.Empty
                txtTNitiyou.Text = String.Empty
            Else
                '1���R�[�h�ڂ̕��傪�ʐM����2���R�[�h�ڂ̕��傪�d���̏ꍇ
                If BMNCD_TUUSIN.Equals(_db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_BUMON))) And BMNCD_DENSEN.Equals(_db.rmNullStr(ds.Tables(RS).Rows(1)(COLDT_BUMON))) Then
                    txtTTuujou.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TUUJOU))
                    txtTDoyou.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_DOYOU))
                    txtTNitiyou.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_NICHIYOU))
                    txtDTuujou.Text = _db.rmNullStr(ds.Tables(RS).Rows(1)(COLDT_TUUJOU))
                    txtDDoyou.Text = _db.rmNullStr(ds.Tables(RS).Rows(1)(COLDT_DOYOU))
                    txtDNitiyou.Text = _db.rmNullStr(ds.Tables(RS).Rows(1)(COLDT_NICHIYOU))
                Else
                    '���傪�ʐM�Ɠd���ł͂Ȃ��ꍇ�͋�\��
                    txtDTuujou.Text = String.Empty
                    txtDDoyou.Text = String.Empty
                    txtDNitiyou.Text = String.Empty
                    txtTTuujou.Text = String.Empty
                    txtTDoyou.Text = String.Empty
                    txtTNitiyou.Text = String.Empty
                End If
            End If

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '��������
    '   �i�����T�v�j�@�����������s�Ȃ��A�ꗗ�Ƀf�[�^��\������B
    '------------------------------------------------------------------------------------------------------
    Private Sub dispDGV()

        Dim c As Cursor = Me.Cursor
        Me.Cursor = Cursors.WaitCursor

        Try
            Dim dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanNouryoku)

            '�ꗗ�̏�����
            dgv.clearRow()
            dgvSeisanNouryoku.Enabled = False
            lblKensuu.Text = "0��"

            'SQL
            'M21�����C���Ƃ��āAT63�AT64����f�[�^�擾
            '�\�[�g���ɂ��Ă�M01���Q��(M01�ɑ��݂��Ȃ��H�������l��)
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & "  TMP.KOUTEI " & COLDT_KOUTEI
            sql = sql & N & " ,TMP.KIKAIMEI " & COLDT_MACHINENAME
            sql = sql & N & " ,TMP.TUUJOUH " & COLDT_TUUJOUDAY
            sql = sql & N & " ,TO_NUMBER(TMP.TUUJOUM) " & COLDT_TUUJOUMON
            sql = sql & N & " ,TMP.DOYOUH " & COLDT_DOYOUDAY
            sql = sql & N & " ,TO_NUMBER(TMP.DOYOUM) " & COLDT_DOYOUMON
            sql = sql & N & " ,TMP.NITIYOUH " & COLDT_NICHIYOUDAY
            sql = sql & N & " ,TO_NUMBER(TMP.NITIYOUM) " & COLDT_NICHIYOUMON
            sql = sql & N & " ,DECODE(TMP.KEIFLG,1,TMP.TUUJOUM + TMP.DOYOUM + TMP.NITIYOUM,NULL) " & COLDT_KEISANTI
            sql = sql & N & " ,T64.TYOUSEIMCH " & COLDT_TYOUSEI
            sql = sql & N & " ,DECODE(TMP.KEIFLG,1,TMP.TUUJOUM + TMP.DOYOUM + TMP.NITIYOUM + NVL(T64.TYOUSEIMCH,0)) " & COLDT_TEKIYOU
            sql = sql & N & " FROM (SELECT "
            sql = sql & N & "    M21.KOUTEI "
            sql = sql & N & "   ,M21.KIKAIMEI "
            sql = sql & N & "   ,M21.TUUJOUH "
            sql = sql & N & "   ,M21.DOYOUH "
            sql = sql & N & "   ,M21.NITIYOUH "
            sql = sql & N & "   ,DECODE(T63.TUUJOUD,NULL,NULL,T63.TUUJOUD * M21.TUUJOUH) TUUJOUM "
            sql = sql & N & "   ,DECODE(T63.DOYOUD,NULL,NULL,T63.DOYOUD * M21.DOYOUH) DOYOUM "
            sql = sql & N & "   ,DECODE(T63.NITIYOUD,NULL,NULL,T63.NITIYOUD * M21.NITIYOUH) NITIYOUM "
            sql = sql & N & "   ,(CASE WHEN T63.TUUJOUD IS NULL OR T63.DOYOUD IS NULL OR T63.NITIYOUD IS NULL THEN 0 ELSE 1 END) KEIFLG "
            sql = sql & N & "   FROM M21SEISAN M21 "
            sql = sql & N & "   LEFT OUTER JOIN T63KADOUBI T63 ON T63.BMNCD = SUBSTR(M21.KIKAIMEI,1,1) " '�@�B����1���ڂ���������R�[�h
            sql = sql & N & " ) TMP LEFT OUTER JOIN T64MCH T64 ON T64.NAME = TMP.KIKAIMEI "
            sql = sql & N & " LEFT OUTER JOIN (SELECT KAHENKEY,SORT FROM M01HANYO WHERE KOTEIKEY = '" & KOUTEI & "') M01 ON M01.KAHENKEY = TMP.KOUTEI "
            sql = sql & N & " ORDER BY NVL(M01.SORT," & maxSort & "), TMP.KIKAIMEI "

            'SQL���s
            Dim iRecCnt As Integer                  '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            '���o�f�[�^���ꗗ�ɕ\������
            dgvSeisanNouryoku.DataSource = ds
            dgvSeisanNouryoku.DataMember = RS

            '������\��
            lblKensuu.Text = CStr(iRecCnt) & "��"

            '�{�^������
            If dgvSeisanNouryoku.RowCount <= 0 Then
                dgvSeisanNouryoku.Enabled = False  '�ꗗ�̎g�p�s��
                btnTouroku.Enabled = False         '�o�^�{�^���̎g�p�s��
            Else
                dgvSeisanNouryoku.Enabled = True   '�ꗗ�̎g�p�s��
                btnTouroku.Enabled = True          '�o�^�{�^���̎g�p��
                '�ꗗ�擪�s�I��
                dgvSeisanNouryoku.Focus()
                dgv.setSelectionRowColor(0, 0, StartUp.lCOLOR_BLUE)
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��V�X�e���G���[MSG�o�͌�X���[
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        Finally
            Me.Cursor = c
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@DB�o�^����
    '�@(�����T�v)��ʓ��e��DB�iT64,T63�j�ɔ��f����
    '------------------------------------------------------------------------------------------------------
    Private Sub registDB()

        Try
            Dim dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanNouryoku)
            Dim sql As String = ""

            '�����J�n���Ԃƒ[��ID�̎擾
            Dim updStartDate As Date = Now()                            '�����J�n����
            Dim sPCName As String = _db.rmSQ(UtilClass.getComputerName) '�[��ID

            '�폜����
            'SQL�����s(T63,T64�̑S�폜)
            sql = "DELETE FROM T63KADOUBI"
            _db.executeDB(sql)
            sql = "DELETE FROM T64MCH"
            _db.executeDB(sql)

            'T63�ɉғ�����o�^
            'SQL�����s(�o�^������2���R�[�h�Œ�Ȃ̂ŁA�܂Ƃ߂ēo�^����)
            sql = "INSERT ALL INTO T63KADOUBI ("
            sql = sql & N & " BMNCD "
            sql = sql & N & ",TUUJOUD "
            sql = sql & N & ",DOYOUD "
            sql = sql & N & ",NITIYOUD "
            sql = sql & N & ",UPDNAME "
            sql = sql & N & ",UPDDATE "
            sql = sql & N & ") VALUES ("
            sql = sql & N & "'" & BMNCD_TUUSIN & "'"      '��������(�ʐM)
            sql = sql & N & "," & txtTTuujou.Text         '�ʏ�ғ�����
            sql = sql & N & "," & txtTDoyou.Text          '�y�j�x�o����
            sql = sql & N & "," & txtTNitiyou.Text        '���j�x�o����
            sql = sql & N & ",'" & sPCName & "'" '�[��ID
            sql = sql & N & ",TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "  '�X�V����
            sql = sql & N & ") "
            sql = sql & N & "INTO T63KADOUBI ("
            sql = sql & N & " BMNCD "
            sql = sql & N & ",TUUJOUD "
            sql = sql & N & ",DOYOUD "
            sql = sql & N & ",NITIYOUD "
            sql = sql & N & ",UPDNAME "
            sql = sql & N & ",UPDDATE "
            sql = sql & N & ") VALUES ("
            sql = sql & N & "'" & BMNCD_DENSEN & "'"      '��������(�d��)
            sql = sql & N & "," & txtDTuujou.Text         '�ʏ�ғ�����
            sql = sql & N & "," & txtDDoyou.Text          '�y�j�x�o����
            sql = sql & N & "," & txtDNitiyou.Text        '���j�x�o����
            sql = sql & N & ",'" & sPCName & "'" '�[��ID
            sql = sql & N & ",TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "  '�X�V����
            sql = sql & N & ") "
            sql = sql & N & " SELECT * FROM DUAL "
            _db.executeDB(sql)

            '�o�^������������
            Dim rCntIns As Integer = 0
            Dim tyousei As String
            'T64�Ɉꗗ�̓��e��o�^����
            For i As Integer = 0 To dgv.getMaxRow - 1
                tyousei = _db.rmNullStr(dgv.getCellData(COLDT_TYOUSEI, i))
                If String.Empty.Equals(tyousei) Then
                    tyousei = "0"
                End If
                'SQL�����s
                sql = "INSERT INTO T64MCH ("
                sql = sql & N & " KOUTEI "         '�H��
                sql = sql & N & ",NAME "           '�@�B��
                sql = sql & N & ",TUUJOUH_D "      '�ʏ�ғ�����(��)
                sql = sql & N & ",TUUJOUH_M "      '�ʏ�ғ�����(����)
                sql = sql & N & ",DOYOUH_D "       '�y�j�ғ�����(��)
                sql = sql & N & ",DOYOUH_M "       '�y�j�ғ�����(����)
                sql = sql & N & ",NITIYOUH_D "     '���j�ғ�����(��)
                sql = sql & N & ",NITIYOUH_M "     '���j�ғ�����(����)
                sql = sql & N & ",KEISANMCH "      '�v�ZMCH
                sql = sql & N & ",TYOUSEIMCH "     '����MCH
                sql = sql & N & ",MSTMCH "         '�}�X�^MCH
                sql = sql & N & ",UPDNAME "        '�[��ID
                sql = sql & N & ",UPDDATE "        '�X�V����
                sql = sql & N & ") VALUES ("
                sql = sql & N & " '" & _db.rmNullStr(dgv.getCellData(COLDT_KOUTEI, i)) & "'"        '�H��
                sql = sql & N & ",'" & _db.rmNullStr(dgv.getCellData(COLDT_MACHINENAME, i)) & "'"   '�@�B��
                sql = sql & N & ", " & _db.rmNullStr(dgv.getCellData(COLDT_TUUJOUDAY, i))           '�ʏ�ғ�����(��)
                sql = sql & N & ", " & _db.rmNullStr(dgv.getCellData(COLDT_TUUJOUMON, i))           '�ʏ�ғ�����(����)
                sql = sql & N & ", " & _db.rmNullStr(dgv.getCellData(COLDT_DOYOUDAY, i))            '�ʏ�ғ�����(��)
                sql = sql & N & ", " & _db.rmNullStr(dgv.getCellData(COLDT_DOYOUMON, i))            '�ʏ�ғ�����(����)
                sql = sql & N & ", " & _db.rmNullStr(dgv.getCellData(COLDT_NICHIYOUDAY, i))         '�ʏ�ғ�����(��)
                sql = sql & N & ", " & _db.rmNullStr(dgv.getCellData(COLDT_NICHIYOUMON, i))         '�ʏ�ғ�����(����)
                sql = sql & N & ", " & _db.rmNullStr(dgv.getCellData(COLDT_KEISANTI, i))            '�v�ZMCH
                sql = sql & N & ", " & _db.rmSQ(tyousei)                                            '����MCH
                sql = sql & N & ", " & _db.rmNullStr(dgv.getCellData(COLDT_TEKIYOU, i))             '�}�X�^MCH
                sql = sql & N & ", '" & sPCName & "'"                                               '�[��ID
                sql = sql & N & ", TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "       '�X�V����
                sql = sql & N & " )"
                _db.executeDB(sql)

                '�o�^�����̃J�E���g�A�b�v
                rCntIns += 1
            Next

            '�����I���������擾
            Dim updFinDate As Date
            updFinDate = Now

            '���s�����e�[�u���X�V
            sql = "INSERT INTO T91RIREKI "
            sql = sql & N & "( "
            sql = sql & N & " PGID "       '�@�\ID
            sql = sql & N & ",SNENGETU "   '�����N��
            sql = sql & N & ",KNENGETU "   '�v��N��
            sql = sql & N & ",SDATESTART " '�����J�n����
            sql = sql & N & ",SDATEEND "   '�����I������
            sql = sql & N & ",KENNSU1 "    '����1
            sql = sql & N & ",KENNSU2 "    '����2
            sql = sql & N & ",UPDNAME "    '�[��ID
            sql = sql & N & ",UPDDATE "    '�ŏI�X�V��
            sql = sql & N & ")VALUES( "
            sql = sql & N & " '" & _db.rmSQ(PGID) & "' "                                 '�@�\ID
            sql = sql & N & ",'" & _db.rmSQ(lblSyori.Text.Replace("/", "")) & "' "       '�����N��
            sql = sql & N & ",'" & _db.rmSQ(lblKeikaku.Text.Replace("/", "")) & "' "     '�v��N��
            sql = sql & N & ",TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS') " '�����J�n����
            sql = sql & N & ",TO_DATE('" & updFinDate & "', 'YYYY/MM/DD HH24:MI:SS') "   '�����I������
            sql = sql & N & ",2"                                                         '����1
            sql = sql & N & ", " & rCntIns                                               '����2
            sql = sql & N & ",'" & sPCName & "' "                                        '�[��ID
            sql = sql & N & ",TO_DATE('" & updFinDate & "', 'YYYY/MM/DD HH24:MI:SS') "   '�ŏI�X�V��
            sql = sql & N & ") "
            _db.executeDB(sql)

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try

    End Sub

#End Region

End Class