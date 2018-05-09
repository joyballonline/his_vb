'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���j���׎R�ϏW�v���ʊm�F(����)
'    �i�t�H�[��ID�jZG731Q_FukaYamadumiMeisai
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ���        2010/11/29                 �V�K              
'�@(2)   ����        2011/01/25                 �ύX�@���ڕύX�i����敪����z�敪�j#91              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory

Public Class ZG731Q_FukaYamadumiMeisai
    Inherits System.Windows.Forms.Form

#Region "���e�����l��`"

    'PG���䕶��
    Private Const RS As String = "RecSet"                       '�f�[�^�Z�b�g�e�[�u����
    Private Const N As String = ControlChars.NewLine            '���s����
    Private Const T As String = ControlChars.Tab                '��ؕ���

    '�ꗗ�f�[�^�o�C���h��
    Private Const COLDT_FUKA As String = "dtFuka"                   '���׋敪
    Private Const COLDT_TEHAINO As String = "dtTehaiNo"             '��zNo
    Private Const COLDT_SEIBAN As String = "dtSeiban"               '����
    Private Const COLDT_HINMEI As String = "dtHinmei"               '�i��
    '' 2011/01/25 CHG-S Sugano #91
    'Private Const COLDT_SEISAKUKUBUN As String = "dtSeisakuKbn"     '����敪
    Private Const COLDT_TEHAIKUBUN As String = "dtTehaiKbn"         '��z�敪
    '' 2011/01/25 CHG-E Sugano #91
    Private Const COLDT_KIBOUSYUTTAIBI As String = "dtKibou"        '��]�o����
    Private Const COLDT_KOUTEITYAKUSYUBI As String = "dtKoutei"      '�H�������
    Private Const COLDT_MCH As String = "dtMCH"                     'MCH
    Private Const COLDT_MH As String = "dtMH"                       'MH
    Private Const COLDT_RUIKEIMCH As String = "dtRuikeiMCH"         '�݌vMCH
    Private Const COLDT_RUIKEIMH As String = "dtRuikeiMH"           '�݌vMH

    '�ꗗ�O���b�h��
    Private Const COLCN_FUKA As String = "cnFuka"                   '���׋敪
    Private Const COLCN_TEHAINO As String = "cnTehaiNo"             '��zNo
    Private Const COLCN_SEIBAN As String = "cnSeiban"               '����
    Private Const COLCN_HINMEI As String = "cnHinmei"               '�i��
    '' 2011/01/25 CHG-S Sugano #91
    'Private Const COLCN_SEISAKUKUBUN As String = "cnSeisakuKbn"     '����敪
    Private Const COLCN_TEHAIKUBUN As String = "cnTehaiKbn"         '��z�敪
    '' 2011/01/25 CHG-E Sugano #91
    Private Const COLCN_KIBOUSYUTTAIBI As String = "cnKibou"        '��]�o����
    Private Const COLCN_KOUTEITYAKUSYUBI As String = "cnKoutei"      '�H�������
    Private Const COLCN_MCH As String = "cnMCH"                     'MCH
    Private Const COLCN_MH As String = "cnMH"                       'MH
    Private Const COLCN_RUIKEIMCH As String = "cnRuikeiMCH"         '�݌vMCH
    Private Const COLCN_RUIKEIMH As String = "cnRuikeiMH"           '�݌vMH

    Private Const FUKAKBN As String = "14"                          '���׋敪�̌Œ�L�[
    '' 2011/01/25 CHG-S Sugano #91
    'Private Const SEISANKBN As String = "03"                        '����敪�̌Œ�L�[
    Private Const TEHAIKBN As String = "02"                         '��z�敪�̌Œ�L�[
    '' 2011/01/25 CHG-E Sugano #91

#End Region

#Region "�����o�ϐ���`"

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As Form

    Private _oldRowIndex As Integer = -1            '�I���s�̔w�i�F��ύX���邽�߂̃t���O��錾
    Private _colorCtlFlg As Boolean = False         '�I���s�̔w�i�F��ύX���邽�߂̃t���O��錾

    Private _syoriDate As String                    '�����N��
    Private _keikakuDate As String                  '�v��N��

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
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmForm As Form, _
                    ByVal prmKikaimei As String, ByVal prmSeisanMch As String, ByVal prmYamadumiMch As String, ByVal prmOverMch As String, ByVal prmYamadumiMh As String, _
                    ByVal prmSyoriDate As String, ByVal prmKeikakuDate As String)

        Call Me.New()

        '��������
        _msgHd = prmRefMsgHd                                                'MSG�n���h���̐ݒ�
        _db = prmRefDbHd                                                    'DB�n���h���̐ݒ�
        _parentForm = prmForm                                               '�e�t�H�[��
        StartPosition = FormStartPosition.CenterScreen                      '��ʒ����\��

        '�O��ʂ���̎󂯓n���l�����x���ɃZ�b�g
        lblMachine.Text = prmKikaimei
        If String.Empty.Equals(prmSeisanMch) Then
            lblSNouryoku.Text = String.Empty
        Else
            lblSNouryoku.Text = Format(CDec(prmSeisanMch), "#,##0.0")
        End If
        If String.Empty.Equals(prmYamadumiMch) Then
            lblYamadumiMCH.Text = String.Empty
        Else
            lblYamadumiMCH.Text = Format(CDec(prmYamadumiMch), "#,##0.0")
        End If
        If String.Empty.Equals(prmOverMch) Then
            lblOverMCH.Text = String.Empty
        Else
            lblOverMCH.Text = Format(CDec(prmOverMch), "#,##0.0")
        End If
        If String.Empty.Equals(prmYamadumiMh) Then
            lblYamadumiMH.Text = String.Empty
        Else
            lblYamadumiMH.Text = Format(CDec(prmYamadumiMh), "#,##0.0")
        End If

        '�O��ʂ̏����N���A�v��N���������o�ϐ��ɃZ�b�g
        _syoriDate = prmSyoriDate
        _keikakuDate = prmKeikakuDate

    End Sub

#End Region

#Region "Form�C�x���g"


    '-------------------------------------------------------------------------------
    '�@�����J�n�C�x���g
    '-------------------------------------------------------------------------------
    Private Sub ZG731Q_FukaYamadumiMeisai_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
    '�@�߂�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnModoru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModoru.Click

        '����ʂ��I�����A���j���[��ʂɖ߂�B
        _parentForm.Show()
        _parentForm.Activate()

        Me.Close()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@EXCEL�{�^������
    '------------------------------------------------------------------------------------------------------
    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click

        Try
            ' ����Wait�J�[�\����ێ�
            Dim cur As Cursor = Me.Cursor
            ' �J�[�\����ҋ@�J�[�\���ɕύX
            Me.Cursor = Cursors.WaitCursor
            Try

                'EXCEL�o��
                Call printExcel()

            Finally
                ' �J�[�\�������ɖ߂�
                Me.Cursor = cur
            End Try
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

#End Region

#Region "���[�U��`�C�x���g"

    '------------------------------------------------------------------------------------------------------
    '��������
    '   �i�����T�v�j�@�����������s�Ȃ��A�ꗗ�Ƀf�[�^��\������B
    '------------------------------------------------------------------------------------------------------
    Private Sub dispDGV()

        Dim c As Cursor = Me.Cursor
        Me.Cursor = Cursors.WaitCursor
        lblKensuu.Text = "0��"
        btnExcel.Enabled = False

        Try
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & "  M01F.NAME1 " & COLDT_FUKA
            sql = sql & N & " ,T62.TEHAINO " & COLDT_TEHAINO
            sql = sql & N & " ,T62.SEIBAN " & COLDT_SEIBAN
            sql = sql & N & " ,T62.HINMEI " & COLDT_HINMEI
            '' 2011/01/25 CHG-S Sugano #91
            'sql = sql & N & " ,M01S.NAME1 " & COLDT_SEISAKUKUBUN
            sql = sql & N & " ,M01S.NAME1 " & COLDT_TEHAIKUBUN
            '' 2011/01/25 CHG-E Sugano #91
            sql = sql & N & " ,DECODE(T62.SYUTTAIBI,NULL,NULL,SUBSTR(T62.SYUTTAIBI,3,2) || '/' || SUBSTR(T62.SYUTTAIBI,5,2) || '/' || SUBSTR(T62.SYUTTAIBI,7,2)) " & COLDT_KIBOUSYUTTAIBI
            sql = sql & N & " ,DECODE(T62.TYAKUSYUBI,NULL,NULL,SUBSTR(T62.TYAKUSYUBI,3,2) || '/' || SUBSTR(T62.TYAKUSYUBI,5,2) || '/' || SUBSTR(T62.TYAKUSYUBI,7,2)) " & COLDT_KOUTEITYAKUSYUBI
            sql = sql & N & " ,T62.MCH " & COLDT_MCH
            sql = sql & N & " ,T62.MH " & COLDT_MH
            sql = sql & N & " ,0.0 " & COLDT_RUIKEIMCH
            sql = sql & N & " ,0.0 " & COLDT_RUIKEIMH
            sql = sql & N & " FROM T62FUKAMEISAI T62 "
            sql = sql & N & " LEFT OUTER JOIN (SELECT NAME1,KAHENKEY FROM M01HANYO WHERE KOTEIKEY = '" & FUKAKBN & "') M01F "
            sql = sql & N & " ON M01F.KAHENKEY = T62.FUKAKBN "
            '' 2011/01/25 CHG-S Sugano #91
            'sql = sql & N & " LEFT OUTER JOIN (SELECT NAME1,KAHENKEY FROM M01HANYO WHERE KOTEIKEY = '" & SEISANKBN & "') M01S "
            'sql = sql & N & " ON M01S.KAHENKEY = T62.SEISAKUKBN "
            sql = sql & N & " LEFT OUTER JOIN (SELECT NAME1,KAHENKEY FROM M01HANYO WHERE KOTEIKEY = '" & TEHAIKBN & "') M01S "
            sql = sql & N & " ON M01S.KAHENKEY = T62.TEHAIKBN "
            '' 2011/01/25 CHG-E Sugano #91

            '' 2011/01/25 CHG-S Sugano #91
            'sql = sql & N & " WHERE T62.NAME ='" & _db.rmSQ(_db.rmNullStr(lblMachine.Text)) & "' "
            sql = sql & N & " WHERE T62.KIKAIMEI ='" & _db.rmSQ(_db.rmNullStr(lblMachine.Text)) & "' "
            '' 2011/01/25 CHG-E Sugano #91
            sql = sql & N & " ORDER BY T62.FUKAKBN,T62.SYUTTAIBI,T62.TEHAINO "

            'SQL���s
            Dim iRecCnt As Integer          '�f�[�^�Z�b�g�̍s��
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then            '���o���R�[�h���P�����Ȃ��ꍇ
                Throw New UsrDefException("�Ώۃf�[�^������܂���B", _msgHd.getMSG("NonData"))
            End If

            '0�����傫���ꍇ��EXCEL�{�^��������
            btnExcel.Enabled = True

            '�݌v�l���Z�b�g����
            Dim rukeiMCH As Decimal = 0.0 '�݌vMCH
            Dim rukeiMH As Decimal = 0.0  '�݌vMH
            For i As Integer = 0 To iRecCnt - 1
                rukeiMCH = rukeiMCH + CDec(_db.rmNullStr(ds.Tables(RS).Rows(i)(COLDT_MCH)))
                rukeiMH = rukeiMH + CDec(_db.rmNullStr(ds.Tables(RS).Rows(i)(COLDT_MH)))
                ds.Tables(RS).Rows(i).Item(COLDT_RUIKEIMCH) = rukeiMCH
                ds.Tables(RS).Rows(i).Item(COLDT_RUIKEIMH) = rukeiMH
            Next

            '���o�f�[�^���ꗗ�ɕ\������
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvFukaMeisai)
            gh.clearRow()
            dgvFukaMeisai.DataSource = ds
            dgvFukaMeisai.DataMember = RS

            lblKensuu.Text = CStr(iRecCnt) & "��"

            '�ꗗ�擪�s�I��
            dgvFukaMeisai.Focus()
            gh.setSelectionRowColor(0, 0, StartUp.lCOLOR_BLUE)

        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        Finally
            Me.Cursor = c
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�I���s�ɒ��F���鏈��
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvFukaMeisai_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvFukaMeisai.SelectionChanged
        If _colorCtlFlg Then
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvFukaMeisai)
            gh.setSelectionRowColor(dgvFukaMeisai.CurrentCellAddress.Y, _oldRowIndex, StartUp.lCOLOR_BLUE)
        End If
        _oldRowIndex = dgvFukaMeisai.CurrentCellAddress.Y
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@EXCEL�o�͏���
    '   �i�����T�v�j�@��ʕ\�������@�B�ʕi���ʕ��׎R�ϏW�v�\���o�͂���B
    '------------------------------------------------------------------------------------------------------
    Private Sub printExcel()

        Try
            '���`�t�@�C��
            Dim openFilePath As String = StartUp.iniValue.BaseXlsPath & "\" & StartUp.iniValue.ExcelZG731R1_Base
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
            '�t�@�C�����擾
            Dim wkEditFile As String = StartUp.iniValue.OutXlsPath & "\" & StartUp.iniValue.ExcelZG731R1_Out     '�R�s�[��t�@�C��

            '�R�s�[��t�@�C�������݂���ꍇ�A�R�s�[��t�@�C�����폜
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
                    Dim startPrintRow As Integer = 7    '�o�͊J�n�s��
                    Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvFukaMeisai)        'DGV�n���h���̐ݒ�
                    Dim rowCnt As Integer = gh.getMaxRow    '�f�[�^�O���b�h�ŏI�s
                    Dim xlsi As Integer = 0                 '�G�N�Z���t�@�C���̏������ݍs
                    Dim sLine As Integer = startPrintRow    '�W�v�J�n�s
                    Dim eLine As Integer = startPrintRow    '�W�v�I���s
                    Dim syuukeiFlg As Boolean = False       '�W�v������s���t���O
                    Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder

                    For i As Integer = 0 To rowCnt - 1

                        If i = rowCnt - 1 Then
                            '�ŏI�s�̏ꍇ�W�v�t���OON
                            syuukeiFlg = True
                        ElseIf _db.rmNullStr(dgvFukaMeisai(COLCN_FUKA, i).Value).Equals(_db.rmNullStr(dgvFukaMeisai(COLCN_FUKA, i + 1).Value)) = False Then
                            '���H���Ǝ��H�����قȂ�ꍇ���W�v�t���OON
                            syuukeiFlg = True
                        End If

                        '�ꗗ�f�[�^�o��
                        sb.Append(_db.rmNullStr(dgvFukaMeisai(COLCN_FUKA, i).Value) & T)              '���׋敪
                        sb.Append(_db.rmNullStr(dgvFukaMeisai(COLCN_TEHAINO, i).Value) & T)           '��zNo
                        sb.Append(_db.rmNullStr(dgvFukaMeisai(COLCN_SEIBAN, i).Value) & T)            '����
                        sb.Append(_db.rmNullStr(dgvFukaMeisai(COLCN_HINMEI, i).Value) & T & T & T)    '�i��
                        '' 2011/01/25 CHG-S Sugano #91
                        'sb.Append(_db.rmNullStr(dgvFukaMeisai(COLCN_SEISAKUKUBUN, i).Value) & T)      '����敪
                        sb.Append(_db.rmNullStr(dgvFukaMeisai(COLCN_TEHAIKUBUN, i).Value) & T)        '��z�敪
                        '' 2011/01/25 CHG-E Sugano #91
                        sb.Append(_db.rmNullStr(dgvFukaMeisai(COLCN_KIBOUSYUTTAIBI, i).Value) & T)    '��]�o����
                        sb.Append(_db.rmNullStr(dgvFukaMeisai(COLCN_KOUTEITYAKUSYUBI, i).Value) & T)  '�H�������
                        sb.Append(_db.rmNullStr(dgvFukaMeisai(COLCN_MCH, i).Value) & T)               'MCH
                        sb.Append(_db.rmNullStr(dgvFukaMeisai(COLCN_MH, i).Value) & T)                'MH
                        sb.Append(_db.rmNullStr(dgvFukaMeisai(COLCN_RUIKEIMCH, i).Value) & T)         '�݌vMCH
                        sb.Append(_db.rmNullStr(dgvFukaMeisai(COLCN_RUIKEIMH, i).Value) & N)          '�݌vMH

                        If syuukeiFlg = True Then
                            '�W�v�I���s�̐ݒ�
                            eLine = startPrintRow + xlsi

                            '�s��1�s�ǉ�
                            xlsi += 1

                            '�W�v�s�̑}��
                            sb.Append("�����v" & T)            '���׋敪
                            sb.Append("-" & T)                 '��zNo
                            sb.Append("-" & T)                 '����
                            sb.Append("-" & T & T & T)         '�i��
                            sb.Append("-" & T)                 '����敪����z�敪 (2011/01/25 CHG Sugano #91)
                            sb.Append("-" & T)                 '��]�o����
                            sb.Append("-" & T)                 '�H�������
                            sb.Append("=subtotal(9,J" & sLine & ":J" & eLine & ")" & T) 'MCH
                            sb.Append("=subtotal(9,K" & sLine & ":K" & eLine & ")" & T) 'MH
                            sb.Append("-" & T)                 '�݌vMCH
                            sb.Append("-" & N)                 '�݌vMH

                            '�s��1�s�ǉ�
                            xlsi += 1
                            sb.Append(N)
                            syuukeiFlg = False

                            '�W�v�J�n�s�̐ݒ�
                            sLine = startPrintRow + xlsi + 1
                        End If

                        xlsi += 1

                    Next

                    '�s��ǉ�
                    eh.copyRow(startPrintRow)
                    eh.insertPasteRow(startPrintRow, startPrintRow + xlsi)

                    '���`�Ɉꊇ�\��t��
                    Clipboard.SetText(sb.ToString)
                    eh.paste(startPrintRow, 1)

                    '�]���ȋ�s���폜
                    eh.deleteRow(startPrintRow + xlsi - 1, startPrintRow + xlsi + 1)

                    '�쐬�����ҏW
                    Dim printDate As String = Now.ToString("yyyy/MM/dd HH:mm")
                    eh.setValue("�쐬���� �F " & printDate, 1, 11)   'K1:

                    '�����N���A�v��N���ҏW
                    eh.setValue("�����N���F" & _syoriDate & "�@�@�v��N���F" & _keikakuDate, 1, 5)    'E1

                    '�w�b�_��\������
                    eh.setValue(_db.rmNullStr(lblMachine.Text), 4, 2)       'B4
                    eh.setValue(_db.rmNullStr(lblSNouryoku.Text), 4, 4)     'D4
                    eh.setValue(_db.rmNullStr(lblYamadumiMCH.Text), 4, 6)   'F4
                    eh.setValue(_db.rmNullStr(lblOverMCH.Text), 4, 9)       'I4
                    eh.setValue(_db.rmNullStr(lblYamadumiMH.Text), 4, 12)   'L4

                    '����̃Z���Ƀt�H�[�J�X���Ă�
                    eh.selectCell(startPrintRow, 1)     'A7

                    '�N���b�v�{�[�h�̏�����
                    Clipboard.Clear()

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

#End Region

End Class