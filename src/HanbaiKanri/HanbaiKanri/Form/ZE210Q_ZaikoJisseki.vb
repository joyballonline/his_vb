'===============================================================================
'
'�@�k���{�d���������
'�@�@�i�V�X�e�����j�݌Ɍv��V�X�e��
'�@�@�i�����@�\���j��z�f�[�^�C��(�ڍ�)
'    �i�t�H�[��ID�jZE210Q_ZaikoJisseki
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ����        2010/12/09                 �V�K              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.FileDirectory

Public Class ZE210Q_ZaikoJisseki
    Inherits System.Windows.Forms.Form


    'PG���䕶��
    Private Const RS As String = "RecSet"                       '�f�[�^�Z�b�g�e�[�u����
    Private Const N As String = ControlChars.NewLine            '���s����
    Const COND_COL As Integer = 1 : Const COND_ROW As Integer = 3               '��������
    Const P_DT_COL As Integer = 14 : Const P_DT_ROW As Integer = 1              '�쐬����
    Const YM1__COL As Integer = 3 : Const YM___ROW As Integer = 5
    Const YM2__COL As Integer = 5
    Const YM3__COL As Integer = 7
    Const YM4__COL As Integer = 9
    Const YM5__COL As Integer = 11
    Const YM6__COL As Integer = 13
    Const DATA_START_ROW As Integer = 7 : Const DATA_START_COL As Integer = 1   '�f�[�^�J�n�s


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
    Private Sub ZE210Q_ZaikoJisseki_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            '�`��֌W�̐ݒ�
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           '�T�C�Y���ύX���ꂽ�Ƃ��ɁA�R���g���[�����R���g���[�����̂��ĕ`�悷�邩�ǂ����������l��ݒ�
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '�`��̓o�b�t�@�Ŏ��s����A������ɁA���ʂ���ʂɏo�͂����悤�ݒ�
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  '�R���g���[���́A��ʂɒ��ڂł͂Ȃ��A�܂��o�b�t�@�ɕ`�悳��܂��B����ɂ��A�������}���邱�Ƃ��ł��܂��B
            Me.SetStyle(ControlStyles.UserPaint, True)              '�R���g���[���́A�I�y���[�e�B���O �V�X�e���ɂ���Ăł͂Ȃ��A�Ǝ��ɕ`�悳���悤�ݒ�
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   '�R���g���[���̓E�B���h�E ���b�Z�[�W WM_ERASEBKGND �𖳎�����悤�ɐݒ�

            Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr '�^�C�g���I�v�V�����\��

            Dim d As DataSet = _db.selectDB("select SNENGETU,KNENGETU from T01KEIKANRI", RS)
            Dim syoriDate As String = _db.rmNullStr(d.Tables(RS).Rows(0)("SNENGETU"))
            txtNengetsuTo.Text = Format(DateAdd(DateInterval.Month, -1, CDate(syoriDate.Substring(0, 4) & "/" & syoriDate.Substring(4) & "/01")), "yyyy/MM")
            txtNengetsuFrom.Text = Format(DateAdd(DateInterval.Month, -5, CDate(txtNengetsuTo.Text & "/01")), "yyyy/MM")


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
    '�@���W�I�ύX�C�x���g
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
            If "".Equals(txtNengetsuFrom.Text.Replace("/", "").Trim) Then
                Throw New UsrDefException("�K�{�G���[", _msgHd.getMSG("requiredImput"), txtNengetsuFrom)
            End If
            If "".Equals(txtNengetsuTo.Text.Replace("/", "").Trim) Then
                Throw New UsrDefException("�K�{�G���[", _msgHd.getMSG("requiredImput"), txtNengetsuTo)
            End If
            If txtNengetsuFrom.Text.CompareTo(txtNengetsuTo.Text) > 0 Then
                Throw New UsrDefException("�召�G���[", _msgHd.getMSG("ErrHaniChk"), txtNengetsuFrom)
            End If
            Dim diffNum As Integer = DateDiff(DateInterval.Month, CDate(txtNengetsuFrom.Text & "/01"), CDate(txtNengetsuTo.Text & "/01"))
            If diffNum >= 6 Then
                Throw New UsrDefException("�����G���[", _msgHd.getMSG("ImputedInvalidDate"), txtNengetsuFrom)
            End If

            '���P�`���U�܂ł̃p�����^�擾
            Dim nengetsu1 As String = ""
            Dim nengetsu2 As String = ""
            Dim nengetsu3 As String = ""
            Dim nengetsu4 As String = ""
            Dim nengetsu5 As String = ""
            Dim nengetsu6 As String = ""
            Select Case diffNum
                Case 0
                    nengetsu1 = Format(DateAdd(DateInterval.Month, 0, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                Case 1
                    nengetsu1 = Format(DateAdd(DateInterval.Month, 0, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                    nengetsu2 = Format(DateAdd(DateInterval.Month, 1, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                Case 2
                    nengetsu1 = Format(DateAdd(DateInterval.Month, 0, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                    nengetsu2 = Format(DateAdd(DateInterval.Month, 1, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                    nengetsu3 = Format(DateAdd(DateInterval.Month, 2, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                Case 3
                    nengetsu1 = Format(DateAdd(DateInterval.Month, 0, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                    nengetsu2 = Format(DateAdd(DateInterval.Month, 1, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                    nengetsu3 = Format(DateAdd(DateInterval.Month, 2, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                    nengetsu4 = Format(DateAdd(DateInterval.Month, 3, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                Case 4
                    nengetsu1 = Format(DateAdd(DateInterval.Month, 0, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                    nengetsu2 = Format(DateAdd(DateInterval.Month, 1, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                    nengetsu3 = Format(DateAdd(DateInterval.Month, 2, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                    nengetsu4 = Format(DateAdd(DateInterval.Month, 3, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                    nengetsu5 = Format(DateAdd(DateInterval.Month, 4, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                Case 5
                    nengetsu1 = Format(DateAdd(DateInterval.Month, 0, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                    nengetsu2 = Format(DateAdd(DateInterval.Month, 1, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                    nengetsu3 = Format(DateAdd(DateInterval.Month, 2, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                    nengetsu4 = Format(DateAdd(DateInterval.Month, 3, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                    nengetsu5 = Format(DateAdd(DateInterval.Month, 4, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
                    nengetsu6 = Format(DateAdd(DateInterval.Month, 5, CDate(txtNengetsuFrom.Text & "/01")), "yyyyMM")
            End Select

            'Excel�\���p��SQL�w��p���������\�z
            Dim bumonConditilnDsp As String = ""
            Dim hinmeiCdConditionDsp As String = "�����w��Ȃ�"
            Dim hinshuConditionDsp As String = "�����w��Ȃ�"
            Dim hinmeiConditionDsp As String = "�����w��Ȃ�"
            Dim hanbaiYmDsp As String = ""

            Dim bumonCondition As String = ""
            Select Case True
                Case rdoBumon1.Checked
                    bumonCondition = "(HINKBN2 like '01%' or HINKBN2 like '02%')"
                    bumonConditilnDsp = rdoBumon1.Text
                Case rdoBumon2.Checked
                    bumonCondition = "(HINKBN2 like '01%' )"
                    bumonConditilnDsp = rdoBumon2.Text
                Case rdoBumon3.Checked
                    bumonCondition = "(HINKBN2 like '02%' )"
                    bumonConditilnDsp = rdoBumon3.Text
                Case Else : bumonCondition = "1 = 1"
            End Select

            Dim sqlWhere As String = ""
            sqlWhere = sqlWhere & N & "AND " & bumonCondition & " "
            Select Case True
                Case rdoHinmoku1.Checked
                    If Not "".Equals(txtSiyoCD.Text.Trim) Then
                        sqlWhere = sqlWhere & N & " AND SHIYO     like '" & _db.rmSQ(txtSiyoCD.Text) & "%'"
                    End If
                    If Not "".Equals(txtHinsyuCD.Text) Then
                        sqlWhere = sqlWhere & N & " AND HINSHU    like '" & _db.rmSQ(txtHinsyuCD.Text) & "%'"
                    End If
                    If Not "".Equals(txtSensinsuu.Text) Then
                        sqlWhere = sqlWhere & N & " AND SENSHIN   like '" & _db.rmSQ(txtSensinsuu.Text) & "%'"
                    End If
                    If Not "".Equals(txtSize.Text) Then
                        sqlWhere = sqlWhere & N & " AND SAIZU     like '" & _db.rmSQ(txtSize.Text) & "%'"
                    End If
                    If Not "".Equals(txtColor.Text) Then
                        sqlWhere = sqlWhere & N & " AND IRO       like  '" & _db.rmSQ(txtColor.Text) & "%'"
                    End If
                    hinmeiCdConditionDsp = txtSiyoCD.Text.PadRight(2, "*") & "-" & _
                                           txtHinsyuCD.Text.PadRight(3, "*") & "-" & _
                                           txtSensinsuu.Text.PadRight(3, "*") & "-" & _
                                           txtSize.Text.PadRight(2, "*") & "-" & _
                                           txtColor.Text.PadRight(3, "*")
                Case rdoHinmoku2.Checked
                    If Not "".Equals(txtHinsyuFrom.Text) Then
                        sqlWhere = sqlWhere & N & " AND HINSHU    >=   '" & _db.rmSQ(txtHinsyuFrom.Text) & "'"
                    End If
                    If Not "".Equals(txtHinsyuTo.Text) Then
                        sqlWhere = sqlWhere & N & " AND HINSHU    <=   '" & _db.rmSQ(txtHinsyuTo.Text) & "'"
                    End If
                    hinshuConditionDsp = IIf("".Equals(txtHinsyuFrom.Text), "�n��", txtHinsyuFrom.Text) & "�`" & IIf("".Equals(txtHinsyuTo.Text), "�Ō�", txtHinsyuTo.Text)
                Case rdoHinmoku3.Checked
                    If Not "".Equals(txtHinmei.Text.Replace(" ", "")) Then
                        sqlWhere = sqlWhere & N & " AND (HINSHUMEI || SAIZUMEI || IROMEI) like '%" & _db.rmSQ(txtHinmei.Text.Replace(" ", "")) & "%'"
                    End If
                    hinmeiConditionDsp = txtHinmei.Text
            End Select
            hanbaiYmDsp = IIf("".Equals(txtNengetsuFrom.Text.Replace("/", "").Trim), "�n��", txtNengetsuFrom.Text) & "�`" & IIf("".Equals(txtNengetsuTo.Text.Replace("/", "").Trim), "�Ō�", txtNengetsuTo.Text)


            '���s�m�F�i���s���܂��B��낵���ł����H�j
            Dim opMsg As String = ""
            Dim c As Cursor = Me.Cursor
            Dim rtnCnt As Integer = 0
            Dim ds As DataSet = Nothing
            Me.Cursor = Cursors.WaitCursor
            Try
                Dim SQL As String = ""
                _db.executeDB("delete from ZE210Q_W10 where UPDNAME = '" & UtilClass.getComputerName() & "'")

                '���P���C���T�[�g
                SQL = ""
                SQL = SQL & N & "		INSERT INTO ZE210Q_W10 ( "
                SQL = SQL & N & "		  HINMEICD "
                SQL = SQL & N & "		 ,NENGETSU "
                SQL = SQL & N & "		 ,HINMEI "
                SQL = SQL & N & "		 ,SU1 ,RYO1  "
                SQL = SQL & N & "		 ,SU2 ,RYO2  "
                SQL = SQL & N & "		 ,SU3 ,RYO3  "
                SQL = SQL & N & "		 ,SU4 ,RYO4  "
                SQL = SQL & N & "		 ,SU5 ,RYO5  "
                SQL = SQL & N & "		 ,SU6 ,RYO6  "
                SQL = SQL & N & "		 ,UPDNAME "
                SQL = SQL & N & "		 ,UPDDATE "
                SQL = SQL & N & "		) "
                SQL = SQL & N & "		SELECT "
                SQL = SQL & N & "		(SHIYO || HINSHU || SENSHIN || SAIZU || IRO) HINMEICD "
                SQL = SQL & N & "		,'" & nengetsu1 & "'  "
                SQL = SQL & N & "       ,max(HINSHUMEI) || max(SAIZUMEI) || max(IROMEI) "
                SQL = SQL & N & "		,SUM( ( NVL(KAGI_ZEN_SU ,0) + NVL(FUNA_ZEN_SU ,0) + NVL(SAPO_ZEN_SU ,0) + NVL(HEN_ZEN_SU ,0) + NVL(KARI_ZEN_SU ,0) + NVL(YU_ZEN_SU ,0) + NVL(KAGI_TOU_NYUKO_SU ,0) + NVL(FUNA_TOU_NYUKO_SU ,0) + NVL(SAPO_TOU_NYUKO_SU ,0) ) - ( NVL(KAGI_TOU_SHUKO_SU ,0) + NVL(FUNA_TOU_SHUKO_SU ,0) + NVL(SAPO_TOU_SHUKO_SU ,0) + NVL(HEN_TOU_SHUKO_SU ,0) + NVL(KARI_TOU_SU ,0) + NVL(YU_TOU_SU ,0) ) ) SU1 "
                SQL = SQL & N & "		,SUM( ( NVL(KAGI_ZEN_DOU ,0) + NVL(FUNA_ZEN_DOU ,0) + NVL(SAPO_ZEN_DOU ,0) + NVL(HEN_ZEN_DOU ,0) + NVL(KARI_ZEN_DOU ,0) + NVL(YU_ZEN_DOU ,0) + NVL(KAGI_TOU_NYUKO_DOU ,0) + NVL(FUNA_TOU_NYUKO_DOU ,0) + NVL(SAPO_TOU_NYUKO_DOU ,0) ) - ( NVL(KAGI_TOU_DHUKO_DOU ,0) + NVL(FUNA_TOU_SHUKO_DOU ,0) + NVL(SAPO_TOU_SHUKO_DOU ,0) + NVL(HEN_TOU_SHUKO_DOU ,0) + NVL(KARI_TOU_DOU ,0) + NVL(YU_TOU_DOU ,0) ) ) RYO1 "
                SQL = SQL & N & "		,0 SU2 ,0 RYO2 ,0 SU3 ,0 RYO3 ,0 SU4 ,0 RYO4 ,0 SU5 ,0 RYO5 ,0 SU6 ,0 RYO6 "
                SQL = SQL & N & "		,'" & UtilClass.getComputerName() & "' "
                SQL = SQL & N & "		,SYSDATE "
                SQL = SQL & N & "		FROM T72ZAIKOS "
                SQL = SQL & N & "		WHERE NENTETSU = '" & nengetsu1 & "' "
                SQL = SQL & N & "       " + sqlWhere
                SQL = SQL & N & "		GROUP BY (SHIYO || HINSHU || SENSHIN || SAIZU || IRO) "
                _db.executeDB(SQL)

                '���Q���C���T�[�g
                SQL = ""
                SQL = SQL & N & "		INSERT INTO ZE210Q_W10 ( "
                SQL = SQL & N & "		  HINMEICD "
                SQL = SQL & N & "		 ,NENGETSU "
                SQL = SQL & N & "		 ,HINMEI "
                SQL = SQL & N & "		 ,SU1 ,RYO1  "
                SQL = SQL & N & "		 ,SU2 ,RYO2  "
                SQL = SQL & N & "		 ,SU3 ,RYO3  "
                SQL = SQL & N & "		 ,SU4 ,RYO4  "
                SQL = SQL & N & "		 ,SU5 ,RYO5  "
                SQL = SQL & N & "		 ,SU6 ,RYO6  "
                SQL = SQL & N & "		 ,UPDNAME "
                SQL = SQL & N & "		 ,UPDDATE "
                SQL = SQL & N & "		) "
                SQL = SQL & N & "		SELECT "
                SQL = SQL & N & "		(SHIYO || HINSHU || SENSHIN || SAIZU || IRO) HINMEICD "
                SQL = SQL & N & "		,'" & nengetsu2 & "'  "
                SQL = SQL & N & "       ,max(HINSHUMEI) || max(SAIZUMEI) || max(IROMEI) "
                SQL = SQL & N & "		,0 SU1 ,0 RYO1 "
                SQL = SQL & N & "		,SUM( ( NVL(KAGI_ZEN_SU ,0) + NVL(FUNA_ZEN_SU ,0) + NVL(SAPO_ZEN_SU ,0) + NVL(HEN_ZEN_SU ,0) + NVL(KARI_ZEN_SU ,0) + NVL(YU_ZEN_SU ,0) + NVL(KAGI_TOU_NYUKO_SU ,0) + NVL(FUNA_TOU_NYUKO_SU ,0) + NVL(SAPO_TOU_NYUKO_SU ,0) ) - ( NVL(KAGI_TOU_SHUKO_SU ,0) + NVL(FUNA_TOU_SHUKO_SU ,0) + NVL(SAPO_TOU_SHUKO_SU ,0) + NVL(HEN_TOU_SHUKO_SU ,0) + NVL(KARI_TOU_SU ,0) + NVL(YU_TOU_SU ,0) ) ) SU2 "
                SQL = SQL & N & "		,SUM( ( NVL(KAGI_ZEN_DOU ,0) + NVL(FUNA_ZEN_DOU ,0) + NVL(SAPO_ZEN_DOU ,0) + NVL(HEN_ZEN_DOU ,0) + NVL(KARI_ZEN_DOU ,0) + NVL(YU_ZEN_DOU ,0) + NVL(KAGI_TOU_NYUKO_DOU ,0) + NVL(FUNA_TOU_NYUKO_DOU ,0) + NVL(SAPO_TOU_NYUKO_DOU ,0) ) - ( NVL(KAGI_TOU_DHUKO_DOU ,0) + NVL(FUNA_TOU_SHUKO_DOU ,0) + NVL(SAPO_TOU_SHUKO_DOU ,0) + NVL(HEN_TOU_SHUKO_DOU ,0) + NVL(KARI_TOU_DOU ,0) + NVL(YU_TOU_DOU ,0) ) ) RYO2 "
                SQL = SQL & N & "		,0 SU3 ,0 RYO3 ,0 SU4 ,0 RYO4 ,0 SU5 ,0 RYO5 ,0 SU6 ,0 RYO6 "
                SQL = SQL & N & "		,'" & UtilClass.getComputerName() & "' "
                SQL = SQL & N & "		,SYSDATE "
                SQL = SQL & N & "		FROM T72ZAIKOS "
                SQL = SQL & N & "		WHERE NENTETSU = '" & nengetsu2 & "' "
                SQL = SQL & N & "       " + sqlWhere
                SQL = SQL & N & "		GROUP BY (SHIYO || HINSHU || SENSHIN || SAIZU || IRO) "
                If Not "".Equals(nengetsu2) Then _db.executeDB(SQL)

                '���R���C���T�[�g
                SQL = ""
                SQL = SQL & N & "		INSERT INTO ZE210Q_W10 ( "
                SQL = SQL & N & "		  HINMEICD "
                SQL = SQL & N & "		 ,NENGETSU "
                SQL = SQL & N & "		 ,HINMEI "
                SQL = SQL & N & "		 ,SU1 ,RYO1  "
                SQL = SQL & N & "		 ,SU2 ,RYO2  "
                SQL = SQL & N & "		 ,SU3 ,RYO3  "
                SQL = SQL & N & "		 ,SU4 ,RYO4  "
                SQL = SQL & N & "		 ,SU5 ,RYO5  "
                SQL = SQL & N & "		 ,SU6 ,RYO6  "
                SQL = SQL & N & "		 ,UPDNAME "
                SQL = SQL & N & "		 ,UPDDATE "
                SQL = SQL & N & "		) "
                SQL = SQL & N & "		SELECT "
                SQL = SQL & N & "		(SHIYO || HINSHU || SENSHIN || SAIZU || IRO) HINMEICD "
                SQL = SQL & N & "		,'" & nengetsu3 & "'  "
                SQL = SQL & N & "       ,max(HINSHUMEI) || max(SAIZUMEI) || max(IROMEI) "
                SQL = SQL & N & "		,0 SU1 ,0 RYO1 ,0 SU2 ,0 RYO2 "
                SQL = SQL & N & "		,SUM( ( NVL(KAGI_ZEN_SU ,0) + NVL(FUNA_ZEN_SU ,0) + NVL(SAPO_ZEN_SU ,0) + NVL(HEN_ZEN_SU ,0) + NVL(KARI_ZEN_SU ,0) + NVL(YU_ZEN_SU ,0) + NVL(KAGI_TOU_NYUKO_SU ,0) + NVL(FUNA_TOU_NYUKO_SU ,0) + NVL(SAPO_TOU_NYUKO_SU ,0) ) - ( NVL(KAGI_TOU_SHUKO_SU ,0) + NVL(FUNA_TOU_SHUKO_SU ,0) + NVL(SAPO_TOU_SHUKO_SU ,0) + NVL(HEN_TOU_SHUKO_SU ,0) + NVL(KARI_TOU_SU ,0) + NVL(YU_TOU_SU ,0) ) ) SU3 "
                SQL = SQL & N & "		,SUM( ( NVL(KAGI_ZEN_DOU ,0) + NVL(FUNA_ZEN_DOU ,0) + NVL(SAPO_ZEN_DOU ,0) + NVL(HEN_ZEN_DOU ,0) + NVL(KARI_ZEN_DOU ,0) + NVL(YU_ZEN_DOU ,0) + NVL(KAGI_TOU_NYUKO_DOU ,0) + NVL(FUNA_TOU_NYUKO_DOU ,0) + NVL(SAPO_TOU_NYUKO_DOU ,0) ) - ( NVL(KAGI_TOU_DHUKO_DOU ,0) + NVL(FUNA_TOU_SHUKO_DOU ,0) + NVL(SAPO_TOU_SHUKO_DOU ,0) + NVL(HEN_TOU_SHUKO_DOU ,0) + NVL(KARI_TOU_DOU ,0) + NVL(YU_TOU_DOU ,0) ) ) RYO3 "
                SQL = SQL & N & "		,0 SU4 ,0 RYO4 ,0 SU5 ,0 RYO5 ,0 SU6 ,0 RYO6 "
                SQL = SQL & N & "		,'" & UtilClass.getComputerName() & "' "
                SQL = SQL & N & "		,SYSDATE "
                SQL = SQL & N & "		FROM T72ZAIKOS "
                SQL = SQL & N & "		WHERE NENTETSU = '" & nengetsu3 & "' "
                SQL = SQL & N & "       " + sqlWhere
                SQL = SQL & N & "		GROUP BY (SHIYO || HINSHU || SENSHIN || SAIZU || IRO) "
                If Not "".Equals(nengetsu3) Then _db.executeDB(SQL)

                '���S���C���T�[�g
                SQL = ""
                SQL = SQL & N & "		INSERT INTO ZE210Q_W10 ( "
                SQL = SQL & N & "		  HINMEICD "
                SQL = SQL & N & "		 ,NENGETSU "
                SQL = SQL & N & "		 ,HINMEI "
                SQL = SQL & N & "		 ,SU1 ,RYO1  "
                SQL = SQL & N & "		 ,SU2 ,RYO2  "
                SQL = SQL & N & "		 ,SU3 ,RYO3  "
                SQL = SQL & N & "		 ,SU4 ,RYO4  "
                SQL = SQL & N & "		 ,SU5 ,RYO5  "
                SQL = SQL & N & "		 ,SU6 ,RYO6  "
                SQL = SQL & N & "		 ,UPDNAME "
                SQL = SQL & N & "		 ,UPDDATE "
                SQL = SQL & N & "		) "
                SQL = SQL & N & "		SELECT "
                SQL = SQL & N & "		(SHIYO || HINSHU || SENSHIN || SAIZU || IRO) HINMEICD "
                SQL = SQL & N & "		,'" & nengetsu4 & "'  "
                SQL = SQL & N & "       ,max(HINSHUMEI) || max(SAIZUMEI) || max(IROMEI) "
                SQL = SQL & N & "		,0 SU1 ,0 RYO1 ,0 SU2 ,0 RYO2 ,0 SU3 ,0 RYO3 "
                SQL = SQL & N & "		,SUM( ( NVL(KAGI_ZEN_SU ,0) + NVL(FUNA_ZEN_SU ,0) + NVL(SAPO_ZEN_SU ,0) + NVL(HEN_ZEN_SU ,0) + NVL(KARI_ZEN_SU ,0) + NVL(YU_ZEN_SU ,0) + NVL(KAGI_TOU_NYUKO_SU ,0) + NVL(FUNA_TOU_NYUKO_SU ,0) + NVL(SAPO_TOU_NYUKO_SU ,0) ) - ( NVL(KAGI_TOU_SHUKO_SU ,0) + NVL(FUNA_TOU_SHUKO_SU ,0) + NVL(SAPO_TOU_SHUKO_SU ,0) + NVL(HEN_TOU_SHUKO_SU ,0) + NVL(KARI_TOU_SU ,0) + NVL(YU_TOU_SU ,0) ) ) SU4 "
                SQL = SQL & N & "		,SUM( ( NVL(KAGI_ZEN_DOU ,0) + NVL(FUNA_ZEN_DOU ,0) + NVL(SAPO_ZEN_DOU ,0) + NVL(HEN_ZEN_DOU ,0) + NVL(KARI_ZEN_DOU ,0) + NVL(YU_ZEN_DOU ,0) + NVL(KAGI_TOU_NYUKO_DOU ,0) + NVL(FUNA_TOU_NYUKO_DOU ,0) + NVL(SAPO_TOU_NYUKO_DOU ,0) ) - ( NVL(KAGI_TOU_DHUKO_DOU ,0) + NVL(FUNA_TOU_SHUKO_DOU ,0) + NVL(SAPO_TOU_SHUKO_DOU ,0) + NVL(HEN_TOU_SHUKO_DOU ,0) + NVL(KARI_TOU_DOU ,0) + NVL(YU_TOU_DOU ,0) ) ) RYO4 "
                SQL = SQL & N & "		,0 SU5 ,0 RYO5 ,0 SU6 ,0 RYO6 "
                SQL = SQL & N & "		,'" & UtilClass.getComputerName() & "' "
                SQL = SQL & N & "		,SYSDATE "
                SQL = SQL & N & "		FROM T72ZAIKOS "
                SQL = SQL & N & "		WHERE NENTETSU = '" & nengetsu4 & "' "
                SQL = SQL & N & "       " + sqlWhere
                SQL = SQL & N & "		GROUP BY (SHIYO || HINSHU || SENSHIN || SAIZU || IRO) "
                If Not "".Equals(nengetsu4) Then _db.executeDB(SQL)

                '���T���C���T�[�g
                SQL = ""
                SQL = SQL & N & "		INSERT INTO ZE210Q_W10 ( "
                SQL = SQL & N & "		  HINMEICD "
                SQL = SQL & N & "		 ,NENGETSU "
                SQL = SQL & N & "		 ,HINMEI "
                SQL = SQL & N & "		 ,SU1 ,RYO1  "
                SQL = SQL & N & "		 ,SU2 ,RYO2  "
                SQL = SQL & N & "		 ,SU3 ,RYO3  "
                SQL = SQL & N & "		 ,SU4 ,RYO4  "
                SQL = SQL & N & "		 ,SU5 ,RYO5  "
                SQL = SQL & N & "		 ,SU6 ,RYO6  "
                SQL = SQL & N & "		 ,UPDNAME "
                SQL = SQL & N & "		 ,UPDDATE "
                SQL = SQL & N & "		) "
                SQL = SQL & N & "		SELECT "
                SQL = SQL & N & "		(SHIYO || HINSHU || SENSHIN || SAIZU || IRO) HINMEICD "
                SQL = SQL & N & "		,'" & nengetsu5 & "'  "
                SQL = SQL & N & "       ,max(HINSHUMEI) || max(SAIZUMEI) || max(IROMEI) "
                SQL = SQL & N & "		,0 SU1 ,0 RYO1 ,0 SU2 ,0 RYO2 ,0 SU3 ,0 RYO3 ,0 SU4 ,0 RYO4 "
                SQL = SQL & N & "		,SUM( ( NVL(KAGI_ZEN_SU ,0) + NVL(FUNA_ZEN_SU ,0) + NVL(SAPO_ZEN_SU ,0) + NVL(HEN_ZEN_SU ,0) + NVL(KARI_ZEN_SU ,0) + NVL(YU_ZEN_SU ,0) + NVL(KAGI_TOU_NYUKO_SU ,0) + NVL(FUNA_TOU_NYUKO_SU ,0) + NVL(SAPO_TOU_NYUKO_SU ,0) ) - ( NVL(KAGI_TOU_SHUKO_SU ,0) + NVL(FUNA_TOU_SHUKO_SU ,0) + NVL(SAPO_TOU_SHUKO_SU ,0) + NVL(HEN_TOU_SHUKO_SU ,0) + NVL(KARI_TOU_SU ,0) + NVL(YU_TOU_SU ,0) ) ) SU5 "
                SQL = SQL & N & "		,SUM( ( NVL(KAGI_ZEN_DOU ,0) + NVL(FUNA_ZEN_DOU ,0) + NVL(SAPO_ZEN_DOU ,0) + NVL(HEN_ZEN_DOU ,0) + NVL(KARI_ZEN_DOU ,0) + NVL(YU_ZEN_DOU ,0) + NVL(KAGI_TOU_NYUKO_DOU ,0) + NVL(FUNA_TOU_NYUKO_DOU ,0) + NVL(SAPO_TOU_NYUKO_DOU ,0) ) - ( NVL(KAGI_TOU_DHUKO_DOU ,0) + NVL(FUNA_TOU_SHUKO_DOU ,0) + NVL(SAPO_TOU_SHUKO_DOU ,0) + NVL(HEN_TOU_SHUKO_DOU ,0) + NVL(KARI_TOU_DOU ,0) + NVL(YU_TOU_DOU ,0) ) ) RYO5 "
                SQL = SQL & N & "		,0 SU6 ,0 RYO6 "
                SQL = SQL & N & "		,'" & UtilClass.getComputerName() & "' "
                SQL = SQL & N & "		,SYSDATE "
                SQL = SQL & N & "		FROM T72ZAIKOS "
                SQL = SQL & N & "		WHERE NENTETSU = '" & nengetsu5 & "' "
                SQL = SQL & N & "       " + sqlWhere
                SQL = SQL & N & "		GROUP BY (SHIYO || HINSHU || SENSHIN || SAIZU || IRO) "
                If Not "".Equals(nengetsu5) Then _db.executeDB(SQL)

                '���U���C���T�[�g
                SQL = ""
                SQL = SQL & N & "		INSERT INTO ZE210Q_W10 ( "
                SQL = SQL & N & "		  HINMEICD "
                SQL = SQL & N & "		 ,NENGETSU "
                SQL = SQL & N & "		 ,HINMEI "
                SQL = SQL & N & "		 ,SU1 ,RYO1  "
                SQL = SQL & N & "		 ,SU2 ,RYO2  "
                SQL = SQL & N & "		 ,SU3 ,RYO3  "
                SQL = SQL & N & "		 ,SU4 ,RYO4  "
                SQL = SQL & N & "		 ,SU5 ,RYO5  "
                SQL = SQL & N & "		 ,SU6 ,RYO6  "
                SQL = SQL & N & "		 ,UPDNAME "
                SQL = SQL & N & "		 ,UPDDATE "
                SQL = SQL & N & "		) "
                SQL = SQL & N & "		SELECT "
                SQL = SQL & N & "		(SHIYO || HINSHU || SENSHIN || SAIZU || IRO) HINMEICD "
                SQL = SQL & N & "		,'" & nengetsu6 & "'  "
                SQL = SQL & N & "       ,max(HINSHUMEI) || max(SAIZUMEI) || max(IROMEI) "
                SQL = SQL & N & "		,0 SU1 ,0 RYO1 ,0 SU2 ,0 RYO2 ,0 SU3 ,0 RYO3 ,0 SU4 ,0 RYO4 ,0 SU5 ,0 RYO5 "
                SQL = SQL & N & "		,SUM( ( NVL(KAGI_ZEN_SU ,0) + NVL(FUNA_ZEN_SU ,0) + NVL(SAPO_ZEN_SU ,0) + NVL(HEN_ZEN_SU ,0) + NVL(KARI_ZEN_SU ,0) + NVL(YU_ZEN_SU ,0) + NVL(KAGI_TOU_NYUKO_SU ,0) + NVL(FUNA_TOU_NYUKO_SU ,0) + NVL(SAPO_TOU_NYUKO_SU ,0) ) - ( NVL(KAGI_TOU_SHUKO_SU ,0) + NVL(FUNA_TOU_SHUKO_SU ,0) + NVL(SAPO_TOU_SHUKO_SU ,0) + NVL(HEN_TOU_SHUKO_SU ,0) + NVL(KARI_TOU_SU ,0) + NVL(YU_TOU_SU ,0) ) ) SU6 "
                SQL = SQL & N & "		,SUM( ( NVL(KAGI_ZEN_DOU ,0) + NVL(FUNA_ZEN_DOU ,0) + NVL(SAPO_ZEN_DOU ,0) + NVL(HEN_ZEN_DOU ,0) + NVL(KARI_ZEN_DOU ,0) + NVL(YU_ZEN_DOU ,0) + NVL(KAGI_TOU_NYUKO_DOU ,0) + NVL(FUNA_TOU_NYUKO_DOU ,0) + NVL(SAPO_TOU_NYUKO_DOU ,0) ) - ( NVL(KAGI_TOU_DHUKO_DOU ,0) + NVL(FUNA_TOU_SHUKO_DOU ,0) + NVL(SAPO_TOU_SHUKO_DOU ,0) + NVL(HEN_TOU_SHUKO_DOU ,0) + NVL(KARI_TOU_DOU ,0) + NVL(YU_TOU_DOU ,0) ) ) RYO6 "
                SQL = SQL & N & "		,'" & UtilClass.getComputerName() & "' "
                SQL = SQL & N & "		,SYSDATE "
                SQL = SQL & N & "		FROM T72ZAIKOS "
                SQL = SQL & N & "		WHERE NENTETSU = '" & nengetsu6 & "' "
                SQL = SQL & N & "       " + sqlWhere
                SQL = SQL & N & "		GROUP BY (SHIYO || HINSHU || SENSHIN || SAIZU || IRO) "
                If Not "".Equals(nengetsu6) Then _db.executeDB(SQL)

                '���[�N���o
                SQL = ""
                SQL = SQL & N & " SELECT "
                SQL = SQL & N & "  HINMEICD,HINMEI,SU1,RYO1,SU2,RYO2,SU3,RYO3,SU4,RYO4,SU5,RYO5,SU6,RYO6 "
                SQL = SQL & N & " FROM ("
                SQL = SQL & N & " ( "
                '-----------------------�c���ϊ�
                SQL = SQL & N & "		SELECT "
                SQL = SQL & N & "		  SUBSTR(HINMEICD,3,3) KEY1"
                SQL = SQL & N & "		 ,1 KEY2"
                SQL = SQL & N & "		 ,HINMEICD"
                SQL = SQL & N & "		 ,HINMEI "
                SQL = SQL & N & "		 ,SU1 ,RYO1  "
                SQL = SQL & N & "		 ,SU2 ,RYO2  "
                SQL = SQL & N & "		 ,SU3 ,RYO3  "
                SQL = SQL & N & "		 ,SU4 ,RYO4  "
                SQL = SQL & N & "		 ,SU5 ,RYO5  "
                SQL = SQL & N & "		 ,SU6 ,RYO6  "
                SQL = SQL & N & "		FROM ( "
                SQL = SQL & N & "			SELECT "
                SQL = SQL & N & "			  HINMEICD"
                SQL = SQL & N & "			 ,MAX(HINMEI) HINMEI "
                SQL = SQL & N & "			 ,SUM(SU1) SU1 ,SUM(RYO1) RYO1 "
                SQL = SQL & N & "			 ,SUM(SU2) SU2 ,SUM(RYO2) RYO2 "
                SQL = SQL & N & "			 ,SUM(SU3) SU3 ,SUM(RYO3) RYO3 "
                SQL = SQL & N & "			 ,SUM(SU4) SU4 ,SUM(RYO4) RYO4 "
                SQL = SQL & N & "			 ,SUM(SU5) SU5 ,SUM(RYO5) RYO5 "
                SQL = SQL & N & "			 ,SUM(SU6) SU6 ,SUM(RYO6) RYO6 "
                SQL = SQL & N & "			FROM ZE210Q_W10 "
                SQL = SQL & N & "			WHERE UPDNAME = '" & UtilClass.getComputerName() & "' "
                SQL = SQL & N & "			GROUP BY HINMEICD"
                SQL = SQL & N & "			)"
                SQL = SQL & N & " )UNION ALL( "
                '-----------------------�u���C�N�t�b�_�s���o
                SQL = SQL & N & "		SELECT "
                SQL = SQL & N & "		  SUBSTR(HINMEICD,3,3) KEY1"
                SQL = SQL & N & "		 ,2 KEY2"
                SQL = SQL & N & "		 ,SUBSTR(HINMEICD,3,3) || '  �i��v' HINMEICD"
                SQL = SQL & N & "		 ,'�|' HINMEI "
                SQL = SQL & N & "		 ,SUM(SU1) ,SUM(RYO1)  "
                SQL = SQL & N & "		 ,SUM(SU2) ,SUM(RYO2)  "
                SQL = SQL & N & "		 ,SUM(SU3) ,SUM(RYO3)  "
                SQL = SQL & N & "		 ,SUM(SU4) ,SUM(RYO4)  "
                SQL = SQL & N & "		 ,SUM(SU5) ,SUM(RYO5)  "
                SQL = SQL & N & "		 ,SUM(SU6) ,SUM(RYO6)  "
                SQL = SQL & N & "		FROM ZE210Q_W10 "
                SQL = SQL & N & "		WHERE UPDNAME = '" & UtilClass.getComputerName() & "' "
                SQL = SQL & N & "		GROUP BY SUBSTR(HINMEICD,3,3) "
                SQL = SQL & N & " )UNION ALL( "
                '-----------------------��s���o
                SQL = SQL & N & "		SELECT "
                SQL = SQL & N & "		  SUBSTR(HINMEICD,3,3) KEY1"
                SQL = SQL & N & "		 ,3 KEY2"
                SQL = SQL & N & "		 ,NULL"
                SQL = SQL & N & "		 ,NULL"
                SQL = SQL & N & "		 ,NULL"
                SQL = SQL & N & "		 ,NULL"
                SQL = SQL & N & "		 ,NULL"
                SQL = SQL & N & "		 ,NULL"
                SQL = SQL & N & "		 ,NULL"
                SQL = SQL & N & "		 ,NULL"
                SQL = SQL & N & "		 ,NULL"
                SQL = SQL & N & "		 ,NULL"
                SQL = SQL & N & "		 ,NULL"
                SQL = SQL & N & "		 ,NULL"
                SQL = SQL & N & "		 ,NULL"
                SQL = SQL & N & "		 ,NULL"
                SQL = SQL & N & "		FROM ZE210Q_W10 "
                SQL = SQL & N & "		WHERE UPDNAME = '" & UtilClass.getComputerName() & "' "
                SQL = SQL & N & "		GROUP BY SUBSTR(HINMEICD,3,3) "
                SQL = SQL & N & " ) )"
                SQL = SQL & N & " ORDER BY KEY1,KEY2,SUBSTR(HINMEICD,3,3),SUBSTR(HINMEICD,6,3),SUBSTR(HINMEICD,9,2),SUBSTR(HINMEICD,1,2),SUBSTR(HINMEICD,11,3) "
                ds = _db.selectDB(SQL, RS)

                rtnCnt = ds.Tables(RS).Rows.Count
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

            Dim rtn As DialogResult = _msgHd.dspMSG("confirmRun", "�o�͍s���F" & Format(rtnCnt, "#,##0") & opMsg)
            If rtn = Windows.Forms.DialogResult.No Or rtn = Windows.Forms.DialogResult.Cancel Then Exit Sub

            Dim pb As UtilProgresBarCancelable = New UtilProgresBarCancelable(Me)
            pb.Show()
            Try
                Application.DoEvents()
                pb.Cursor = Cursors.WaitCursor
                pb.Refresh()
                Application.DoEvents()

                '�v���O���X�o�[�ݒ�
                'pb.jobName = "�o�͂��������Ă��܂��B"
                'pb.status = "�f�[�^�x�[�X�⍇�����D�D�D"


                pb.jobName = "�o�͂��Ă��܂��B"

                Call printExcel(pb, ds.Tables(RS), bumonConditilnDsp, hinmeiCdConditionDsp, hinshuConditionDsp, hinmeiConditionDsp, hanbaiYmDsp, nengetsu1, nengetsu2, nengetsu3, nengetsu4, nengetsu5, nengetsu6)

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
    Private Sub printExcel(ByRef prmRefPb As UtilProgresBarCancelable, ByVal prmDt As DataTable, ByVal prmBumonConditilnDsp As String, ByVal prmHinmeiCdConditionDsp As String, ByVal prmHinshuConditionDsp As String, ByVal prmHinmeiConditionDsp As String, ByVal prmZaikoYmDsp As String, ByVal prmNengetsu1 As String, ByVal prmNengetsu2 As String, ByVal prmNengetsu3 As String, ByVal prmNengetsu4 As String, ByVal prmNengetsu5 As String, ByVal prmNengetsu6 As String)
        '2011/01/21 add start Sugawara 
        Const EXCEL_COPYPASTE_MAX As Integer = 32000
        '2011/01/21 add end Sugawara 

        Try
            '���`�t�@�C��(�i���ʔ̔��v��Ɠ������`)
            Dim openFilePath As String = StartUp.iniValue.BaseXlsPath & "\" & StartUp.iniValue.ExcelZE210R1_Base
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
            Dim wkEditFile As String = StartUp.iniValue.OutXlsPath & "\" & StartUp.iniValue.ExcelZE210R1_Out     '�R�s�[��t�@�C��

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
                    formatVal = formatVal.Replace("{@�݌ɔN��}", prmZaikoYmDsp)
                    eh.setValue(formatVal, COND_ROW, COND_COL)

                    '�쐬����
                    eh.setValue(eh.getValue(P_DT_ROW, P_DT_COL) & Format(Now, "yyyy/MM/dd HH:mm"), P_DT_ROW, P_DT_COL)

                    '�N��
                    If "".Equals(prmNengetsu1) Then
                        eh.setValue("", YM___ROW, YM1__COL)
                    Else
                        eh.setValue(prmNengetsu1.Substring(0, 4) & "/" & prmNengetsu1.Substring(4, 2), YM___ROW, YM1__COL)
                    End If
                    If "".Equals(prmNengetsu2) Then
                        eh.setValue("", YM___ROW, YM2__COL)
                    Else
                        eh.setValue(prmNengetsu2.Substring(0, 4) & "/" & prmNengetsu2.Substring(4, 2), YM___ROW, YM2__COL)
                    End If
                    If "".Equals(prmNengetsu3) Then
                        eh.setValue("", YM___ROW, YM3__COL)
                    Else
                        eh.setValue(prmNengetsu3.Substring(0, 4) & "/" & prmNengetsu3.Substring(4, 2), YM___ROW, YM3__COL)
                    End If
                    If "".Equals(prmNengetsu4) Then
                        eh.setValue("", YM___ROW, YM4__COL)
                    Else
                        eh.setValue(prmNengetsu4.Substring(0, 4) & "/" & prmNengetsu4.Substring(4, 2), YM___ROW, YM4__COL)
                    End If
                    If "".Equals(prmNengetsu5) Then
                        eh.setValue("", YM___ROW, YM5__COL)
                    Else
                        eh.setValue(prmNengetsu5.Substring(0, 4) & "/" & prmNengetsu5.Substring(4, 2), YM___ROW, YM5__COL)
                    End If
                    If "".Equals(prmNengetsu6) Then
                        eh.setValue("", YM___ROW, YM6__COL)
                    Else
                        eh.setValue(prmNengetsu6.Substring(0, 4) & "/" & prmNengetsu6.Substring(4, 2), YM___ROW, YM6__COL)
                    End If
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
                            .Append(_db.rmNullStr(prmDt.Rows(i)("HINMEICD")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("HINMEI")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("SU1")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("RYO1")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("SU2")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("RYO2")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("SU3")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("RYO3")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("SU4")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("RYO4")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("SU5")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("RYO5")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("SU6")) & ControlChars.Tab)
                            .Append(_db.rmNullStr(prmDt.Rows(i)("RYO6")) & ControlChars.Tab)
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