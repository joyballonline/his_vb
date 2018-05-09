'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���j��z�f�[�^�C��(�ڍ�)
'    �i�t�H�[��ID�jZE110Q_HanbaiJisseki
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ����        2010/12/07                 �V�K              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.FileDirectory

Public Class ZE110Q_HanbaiJisseki
    Inherits System.Windows.Forms.Form


    'PG���䕶��
    Private Const RS As String = "RecSet"                       '�f�[�^�Z�b�g�e�[�u����
    Private Const N As String = ControlChars.NewLine            '���s����
    Const COND_COL As Integer = 1 : Const COND_ROW As Integer = 3               '��������
    Const P_DT_COL As Integer = 18 : Const P_DT_ROW As Integer = 1              '�쐬����
    Const DATA_START_ROW As Integer = 6 : Const DATA_START_COL As Integer = 1   '�f�[�^�J�n�s


    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As Form

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
    '�R���X�g���N�^�iPrivate�ɂ��āA�O����͌ĂׂȂ��悤�ɂ���j
    '-------------------------------------------------------------------------------
    Private Sub New()
        ' ���̌Ăяo���́AWindows �t�H�[�� �f�U�C�i�ŕK�v�ł��B
        InitializeComponent()

    End Sub

    '-------------------------------------------------------------------------------
    '�R���X�g���N�^�@���j���[����Ă΂��
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmForm As Form)
        Call Me.New()

        '��������
        _msgHd = prmRefMsgHd                                                'MSG�n���h���̐ݒ�
        _db = prmRefDbHd                                                    'DB�n���h���̐ݒ�
        _parentForm = prmForm                                               '�e�t�H�[��
        StartPosition = FormStartPosition.CenterScreen                      '��ʒ����\��
    End Sub


    '-------------------------------------------------------------------------------
    '�@�����J�n�C�x���g
    '-------------------------------------------------------------------------------
    Private Sub ZE110Q_HanbaiJisseki_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            '�`��֌W�̐ݒ�
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           '�T�C�Y���ύX���ꂽ�Ƃ��ɁA�R���g���[�����R���g���[�����̂��ĕ`�悷�邩�ǂ����������l��ݒ�
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '�`��̓o�b�t�@�Ŏ��s����A������ɁA���ʂ���ʂɏo�͂����悤�ݒ�
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  '�R���g���[���́A��ʂɒ��ڂł͂Ȃ��A�܂��o�b�t�@�ɕ`�悳��܂��B����ɂ��A�������}���邱�Ƃ��ł��܂��B
            Me.SetStyle(ControlStyles.UserPaint, True)              '�R���g���[���́A�I�y���[�e�B���O �V�X�e���ɂ���Ăł͂Ȃ��A�Ǝ��ɕ`�悳���悤�ݒ�
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   '�R���g���[���̓E�B���h�E ���b�Z�[�W WM_ERASEBKGND �𖳎�����悤�ɐݒ�

            Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr '�^�C�g���I�v�V�����\��

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

        '����ʂ��I�����A���j���[��ʂɖ߂�B
        _parentForm.Show()
        _parentForm.Activate()

        Me.Close()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�i�ڃ��W�I�ύX�C�x���g
    '------------------------------------------------------------------------------------------------------
    Private Sub rdoHinmoku_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoHinmoku1.CheckedChanged, rdoHinmoku2.CheckedChanged, rdoHinmoku3.CheckedChanged
        Try
            txtSiyoCD.Enabled = rdoHinmoku1.Checked
            txtHinsyuCD.Enabled = rdoHinmoku1.Checked
            txtSensinsuu.Enabled = rdoHinmoku1.Checked
            txtSize.Enabled = rdoHinmoku1.Checked
            txtColor.Enabled = rdoHinmoku1.Checked
            txtSiyoCD.BackColor = IIf(txtSiyoCD.Enabled, StartUp.lCOLOR_WHITE, StartUp.lCOLOR_YELLOW)
            txtHinsyuCD.BackColor = IIf(txtHinsyuCD.Enabled, StartUp.lCOLOR_WHITE, StartUp.lCOLOR_YELLOW)
            txtSensinsuu.BackColor = IIf(txtSensinsuu.Enabled, StartUp.lCOLOR_WHITE, StartUp.lCOLOR_YELLOW)
            txtSize.BackColor = IIf(txtSize.Enabled, StartUp.lCOLOR_WHITE, StartUp.lCOLOR_YELLOW)
            txtColor.BackColor = IIf(txtColor.Enabled, StartUp.lCOLOR_WHITE, StartUp.lCOLOR_YELLOW)

            txtHinsyuFrom.Enabled = rdoHinmoku2.Checked
            txtHinsyuTo.Enabled = rdoHinmoku2.Checked
            txtHinsyuFrom.BackColor = IIf(txtHinsyuFrom.Enabled, StartUp.lCOLOR_WHITE, StartUp.lCOLOR_YELLOW)
            txtHinsyuTo.BackColor = IIf(txtHinsyuTo.Enabled, StartUp.lCOLOR_WHITE, StartUp.lCOLOR_YELLOW)

            txtHinmei.Enabled = rdoHinmoku3.Checked
            txtHinmei.BackColor = IIf(txtHinmei.Enabled, StartUp.lCOLOR_WHITE, StartUp.lCOLOR_YELLOW)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�L�[�����C�x���g
    '------------------------------------------------------------------------------------------------------
    Private Sub rdoBumon_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles rdoBumon1.KeyPress, rdoBumon2.KeyPress, rdoBumon3.KeyPress, rdoHinmoku1.KeyPress, rdoHinmoku2.KeyPress, rdoHinmoku3.KeyPress, txtSiyoCD.KeyPress, txtHinsyuCD.KeyPress, txtSensinsuu.KeyPress, txtSize.KeyPress, txtColor.KeyPress, txtHinsyuFrom.KeyPress, txtHinsyuTo.KeyPress, txtHinmei.KeyPress, txtNengetsuFrom.KeyPress, txtNengetsuTo.KeyPress
        Try
            UtilClass.moveNextFocus(Me, e)
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�t�H�[�J�X�擾�C�x���g
    '------------------------------------------------------------------------------------------------------
    Private Sub txt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSiyoCD.GotFocus, txtHinsyuCD.GotFocus, txtSensinsuu.GotFocus, txtSize.GotFocus, txtColor.GotFocus, txtHinsyuFrom.GotFocus, txtHinsyuTo.GotFocus, txtHinmei.GotFocus, txtNengetsuFrom.GotFocus, txtNengetsuTo.GotFocus
        Try
            UtilClass.selAll(sender)
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@Excel�{�^�������C�x���g
    '------------------------------------------------------------------------------------------------------
    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Try
            '���̓`�F�b�N
            If Not "".Equals(txtNengetsuFrom.Text.Replace("/", "").Trim) And _
               Not "".Equals(txtNengetsuTo.Text.Replace("/", "").Trim) And _
               txtNengetsuFrom.Text.CompareTo(txtNengetsuTo.Text) > 0 Then
                Throw New UsrDefException("�召�G���[", _msgHd.getMSG("ErrHaniChk"), txtNengetsuFrom)
            End If

            Dim sqlSelect As String = ""
            sqlSelect = sqlSelect & N & "SELECT "
            sqlSelect = sqlSelect & N & " HINMEIKBN2                                             �i���敪�Q "
            sqlSelect = sqlSelect & N & ",HINMEIKBN4                                             �i���敪�S "
            sqlSelect = sqlSelect & N & ",HINMEIKBN5                                             �i���敪�T "
            sqlSelect = sqlSelect & N & ",HINMEIKBN6                                             �i���敪�U "
            sqlSelect = sqlSelect & N & ",TORIHIKISAKIKBN1                                       �����敪�P "
            sqlSelect = sqlSelect & N & ",TORIHIKISAKIKBN2                                       �����敪�Q "
            sqlSelect = sqlSelect & N & ",RPAD(SIYO,2,' ') || HINSYU || SENSHIN || SIZECD || IRO �i���R�[�h "
            sqlSelect = sqlSelect & N & ",HINSYUMEI || ' ' || SIZEMEI || ' ' || IROMEI           �i�� "
            sqlSelect = sqlSelect & N & ",SYUKANUM                                               �o�א� "
            sqlSelect = sqlSelect & N & ",UNIT                                                   �P�� "
            sqlSelect = sqlSelect & N & ",DOUTAIKBN                                              ���̋敪 "
            sqlSelect = sqlSelect & N & ",TORIHIKISAKIMEI                                        ����於�� "
            sqlSelect = sqlSelect & N & ",NOUSYOMEI                                              �[������ "
            sqlSelect = sqlSelect & N & ",JYOCHO                                                 �� "
            sqlSelect = sqlSelect & N & ",KOSU                                                   �� "
            sqlSelect = sqlSelect & N & ",DECODE(SYUKKABI,NULL,NULL,SUBSTR(SYUKKABI,1,4) || '/' || SUBSTR(SYUKKABI,5,2) || '/' || SUBSTR(SYUKKABI,7,2)) �o�ד� "
            sqlSelect = sqlSelect & N & ",BUMON                                                  ���� "
            sqlSelect = sqlSelect & N & ",DOURYOU                                                ���� "


            Dim bumonConditilnDsp As String = ""
            Dim hinmeiCdConditionDsp As String = "�����w��Ȃ�"
            Dim hinshuConditionDsp As String = "�����w��Ȃ�"
            Dim hinmeiConditionDsp As String = "�����w��Ȃ�"
            Dim hanbaiYmDsp As String = ""

            Dim bumonCondition As String = ""
            Select Case True
                Case rdoBumon1.Checked
                    bumonCondition = "(HINMEIKBN2 like '01%' or HINMEIKBN2 like '02%')"
                    bumonConditilnDsp = rdoBumon1.Text
                Case rdoBumon2.Checked
                    bumonCondition = "(HINMEIKBN2 like '01%' )"
                    bumonConditilnDsp = rdoBumon2.Text
                Case rdoBumon3.Checked
                    bumonCondition = "(HINMEIKBN2 like '02%' )"
                    bumonConditilnDsp = rdoBumon3.Text
                Case Else : bumonCondition = "1 = 1"
            End Select

            Dim sqlFromWhere As String = ""
            sqlFromWhere = sqlFromWhere & N & "FROM T71HANBAIS "
            sqlFromWhere = sqlFromWhere & N & "WHERE " & bumonCondition & " "
            Select Case True
                Case rdoHinmoku1.Checked
                    If Not "".Equals(txtSiyoCD.Text.Trim) Then
                        sqlFromWhere = sqlFromWhere & N & " AND SIYO                            like '" & _db.rmSQ(txtSiyoCD.Text) & "%'"
                    End If
                    If Not "".Equals(txtHinsyuCD.Text) Then
                        sqlFromWhere = sqlFromWhere & N & " AND HINSYU                          like '" & _db.rmSQ(txtHinsyuCD.Text) & "%'"
                    End If
                    If Not "".Equals(txtSensinsuu.Text) Then
                        sqlFromWhere = sqlFromWhere & N & " AND SENSHIN                         like '" & _db.rmSQ(txtSensinsuu.Text) & "%'"
                    End If
                    If Not "".Equals(txtSize.Text) Then
                        sqlFromWhere = sqlFromWhere & N & " AND SIZECD                          like '" & _db.rmSQ(txtSize.Text) & "%'"
                    End If
                    If Not "".Equals(txtColor.Text) Then
                        sqlFromWhere = sqlFromWhere & N & " AND IRO                             like  '" & _db.rmSQ(txtColor.Text) & "%'"
                    End If
                    hinmeiCdConditionDsp = txtSiyoCD.Text.PadRight(2, "*") & "-" & _
                                           txtHinsyuCD.Text.PadRight(3, "*") & "-" & _
                                           txtSensinsuu.Text.PadRight(3, "*") & "-" & _
                                           txtSize.Text.PadRight(2, "*") & "-" & _
                                           txtColor.Text.PadRight(3, "*")
                Case rdoHinmoku2.Checked
                    If Not "".Equals(txtHinsyuFrom.Text) Then
                        sqlFromWhere = sqlFromWhere & N & " AND HINSYU                          >=   '" & _db.rmSQ(txtHinsyuFrom.Text) & "'"
                    End If
                    If Not "".Equals(txtHinsyuTo.Text) Then
                        sqlFromWhere = sqlFromWhere & N & " AND HINSYU                          <=   '" & _db.rmSQ(txtHinsyuTo.Text) & "'"
                    End If
                    hinshuConditionDsp = IIf("".Equals(txtHinsyuFrom.Text), "�n��", txtHinsyuFrom.Text) & "�`" & IIf("".Equals(txtHinsyuTo.Text), "�Ō�", txtHinsyuTo.Text)
                Case rdoHinmoku3.Checked
                    If Not "".Equals(txtHinmei.Text.Replace(" ", "")) Then
                        sqlFromWhere = sqlFromWhere & N & " AND (HINSYUMEI || SIZEMEI ||IROMEI) like '%" & _db.rmSQ(txtHinmei.Text.Replace(" ", "")) & "%'"
                    End If
                    hinmeiConditionDsp = txtHinmei.Text
            End Select
            If Not "".Equals(txtNengetsuFrom.Text.Replace("/", "").Trim) Then
                sqlFromWhere = sqlFromWhere & N & " AND substr(SYUKKABI,1,6)              >=   '" & _db.rmSQ(txtNengetsuFrom.Text.Replace("/", "").Trim) & "'"
            End If
            If Not "".Equals(txtNengetsuTo.Text.Replace("/", "").Trim) Then
                sqlFromWhere = sqlFromWhere & N & " AND substr(SYUKKABI,1,6)              <=   '" & _db.rmSQ(txtNengetsuTo.Text.Replace("/", "").Trim) & "'"
            End If
            hanbaiYmDsp = IIf("".Equals(txtNengetsuFrom.Text.Replace("/", "").Trim), "�n��", txtNengetsuFrom.Text) & "�`" & IIf("".Equals(txtNengetsuTo.Text.Replace("/", "").Trim), "�Ō�", txtNengetsuTo.Text)


            '���s�m�F�i���s���܂��B��낵���ł����H�j
            Dim opMsg As String = ""
            Dim c As Cursor = Me.Cursor
            Dim rtnCnt As Integer = 0
            Me.Cursor = Cursors.WaitCursor
            Try
                rtnCnt = _db.rmNullInt(_db.selectDB("SELECT COUNT(*) CNT " & sqlFromWhere, RS).Tables(RS).Rows(0)("CNT"))
            Finally
                Me.Cursor = c
            End Try
            If 65536 <= rtnCnt + DATA_START_ROW - 1 Then
                Throw New UsrDefException("�������ʁi" & Format(rtnCnt, "#,##0") & "�j��Excel�̕\���\�s���𒴂��Ă��܂��B" & N & "�������������Ă��������B")
            ElseIf rtnCnt = 0 Then
                _msgHd.dspMSG("noTargetData")
                Exit Sub
            ElseIf rtnCnt > 10000 Then
                opMsg = N & N & "���f�[�^�������������߁A�������̉ғ��󋵂ɂ���Ă͏����������ł��Ȃ��ꍇ������܂��B"
            End If

            Dim rtn As DialogResult = _msgHd.dspMSG("confirmRun", "�o�͌����F" & Format(rtnCnt, "#,##0") & opMsg)
            If rtn = Windows.Forms.DialogResult.No Or rtn = Windows.Forms.DialogResult.Cancel Then Exit Sub

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

                Dim sql As String = ""
                sql = sql & N & sqlSelect
                sql = sql & N & sqlFromWhere
                sql = sql & N & "ORDER BY HINSYU,SENSHIN,SIZECD,RPAD(SIYO,2,' '),IRO,SYUKKABI "
                Dim ds As DataSet = _db.selectDB(sql, RS)

                pb.jobName = "�o�͂��Ă��܂��B"

                Call printExcel(pb, ds.Tables(RS), bumonConditilnDsp, hinmeiCdConditionDsp, hinshuConditionDsp, hinmeiConditionDsp, hanbaiYmDsp)

            Catch pbe As UtilProgressBarCancelEx
                '�L�����Z�������������Ȃ�
            Finally
                '��ʏ���
                pb.Close()
            End Try


        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '�@�ꗗ�o��
    '------------------------------------------------------------------------------------------------------
    Private Sub printExcel(ByRef prmRefPb As UtilProgresBarCancelable, ByVal prmDt As DataTable, ByVal prmBumonConditilnDsp As String, ByVal prmHinmeiCdConditionDsp As String, ByVal prmHinshuConditionDsp As String, ByVal prmHinmeiConditionDsp As String, ByVal prmHanbaiYmDsp As String)
        '2011/01/21 add start Sugawara 
        Const EXCEL_COPYPASTE_MAX As Integer = 32000
        '2011/01/21 add end Sugawara 

        Try
            '���`�t�@�C��(�i���ʔ̔��v��Ɠ������`)
            Dim openFilePath As String = StartUp.iniValue.BaseXlsPath & "\" & StartUp.iniValue.ExcelZE110R1_Base
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
            Dim wkEditFile As String = StartUp.iniValue.OutXlsPath & "\" & StartUp.iniValue.ExcelZE110R1_Out     '�R�s�[��t�@�C��

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

                    prmRefPb.status = "�w�b�_���\�z���D�D�D"

                    '��������
                    Dim formatVal As String = eh.getValue(COND_ROW, COND_COL)
                    formatVal = formatVal.Replace("{@����}", prmBumonConditilnDsp)
                    formatVal = formatVal.Replace("{@�i��CD}", prmHinmeiCdConditionDsp)
                    formatVal = formatVal.Replace("{@�i��CD}", prmHinshuConditionDsp)
                    formatVal = formatVal.Replace("{@�i��}", prmHinmeiConditionDsp)
                    formatVal = formatVal.Replace("{@�̔��N��}", prmHanbaiYmDsp)
                    eh.setValue(formatVal, COND_ROW, COND_COL)

                    '�쐬����
                    eh.setValue(eh.getValue(P_DT_ROW, P_DT_COL) & Format(Now, "yyyy/MM/dd HH:mm"), P_DT_ROW, P_DT_COL)

                    prmRefPb.status = "�o�͗̈�m�ے��D�D�D"

                    '�o�͍s���g��
                    If prmDt.Rows.Count >= 2 Then
                        '�ʏ�
                        Dim insRow As Integer = prmDt.Rows.Count
                        If insRow > EXCEL_COPYPASTE_MAX Then
                            '16bit�͈�(Short�^)�𒴂����COM����O��f���̂ŁA�Ƃ肠����EXCEL_COPYPASTE_MAX���炢�ŏ�����������
                            Dim insCnt As Integer = insRow \ EXCEL_COPYPASTE_MAX
                            For ic As Integer = 0 To insCnt
                                Dim rowNum As Integer = 0
                                If ic = insCnt Then
                                    '�ŏI�}��
                                    rowNum = insRow - ic * EXCEL_COPYPASTE_MAX
                                Else
                                    '�܂��ŏI�łȂ�
                                    '2011/01/21 upd start Sugawara #93
                                    'rowNum = EXCEL_COPYPASTE_MAX
                                    rowNum = EXCEL_COPYPASTE_MAX + 1
                                    '2011/01/21 upd end Sugawara #93
                                End If
                                eh.copyRow(DATA_START_ROW)
                                eh.insertPasteRow(DATA_START_ROW, (DATA_START_ROW + 1) + (rowNum - 1) - 2)
                            Next
                        Else
                            '16bit���E�𒴂��Ȃ��̂ňꔭ�ŃR�s�[�\��t��
                            eh.copyRow(DATA_START_ROW)
                            eh.insertPasteRow(DATA_START_ROW, (DATA_START_ROW + 1) + (prmDt.Rows.Count - 1) - 2)
                        End If
                    ElseIf prmDt.Rows.Count = 0 Then
                        '�o�͂Ȃ�
                        eh.deleteRow(DATA_START_ROW)
                    End If

                    '�o�͕�����\�z
                    prmRefPb.status = "�f�[�^�\�z���D�D�D"
                    Dim buf As System.Text.StringBuilder = New System.Text.StringBuilder
                    With buf
                        prmRefPb.maxVal = prmDt.Rows.Count
                        For i As Integer = 0 To prmDt.Rows.Count - 1
                            .Append(_db.rmNullStr(prmDt.Rows(i)("�i���敪�Q")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("�i���敪�S")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("�i���敪�T")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("�i���敪�U")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("�����敪�P")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("�����敪�Q")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("�i���R�[�h")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("�i��")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("�o�א�")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("�P��")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("���̋敪")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("����於��")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("�[������")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("��")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("��")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("�o�ד�")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("����")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("����")) & ControlChars.Tab)
                            .Append(ControlChars.CrLf)
                            If i Mod 50 = 0 Then
                                prmRefPb.value = i
                            End If
                        Next
                    End With

                    If Not "".Equals(buf.ToString) Then
                        prmRefPb.status = "�f�[�^�o�͒��D�D�D"
                        Clipboard.SetText(buf.ToString)
                        Try
                            eh.paste(DATA_START_ROW, DATA_START_COL)
                        Catch ex As Exception
                            If "�f�[�^��\��t���ł��܂���B".Equals(ex.Message) Then
                                Throw New UsrDefException("�f�[�^(" & Format(prmDt.Rows.Count, "#,##0") & "��)���������邽�߁A�������p���ł��܂���B" & N & "�������������Ă��������B")
                            End If
                            Throw ex
                        End Try
                        Try
                            Clipboard.Clear()
                        Catch ex As Exception
                        End Try
                    End If

                Finally
                    eh.close()
                End Try

                'EXCEL�t�@�C���J��
                eh.display()

            Catch pge As UtilProgressBarCancelEx
                Throw pge
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

        Catch pge As UtilProgressBarCancelEx
            Throw pge
        Catch ue As UsrDefException         '���[�U�[��`��O
            Call ue.dspMsg()
            Throw ue                        '�L���b�`������O�����̂܂܃X���[
        Catch ex As Exception               '�V�X�e����O
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   '�L���b�`������O�����[�U�[��`��O�Ɉڂ��ς��X���[
        End Try
    End Sub

End Class