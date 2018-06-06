'===============================================================================
'
'�@�J�l�L�g�c���X�l
'�@�@�i�V�X�e�����j�̔��Ǘ�
'�@�@�i�����@�\���j�����
'    �i�t�H�[��ID�jH01R20
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ����        2018/03/02                 �V�K              
'-------------------------------------------------------------------------------
Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Document
Imports GrapeCity.ActiveReports.SectionReportModel
Imports HanbaiKanri
Imports UtilMDL.DB
Imports UtilMDL.MSG

Public Class H01R20_Okurijou
    Implements RepSectionIF

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _ds As DataSet
    Private _comLogc As CommonLogic                             '���ʏ����p
    Private _companyCd As String
    Private _shoriId As String
    Private _userId As String
    Private _printKbn As String
    Private rowIdx As Integer = 0
    Private _PageCount As Integer = 0
    Private _kosuSum As Integer = 0
    Private iDenpyoCnt As Integer = 0
    Private iMeisaiCnt As Integer = 0
    Private dcUriageKin As Decimal = 0
    Private dcShohizei As Decimal = 0
    Private dcGoukeiKin As Decimal = 0
    Private RowNumber As Integer = 0�@�@                        ' �����J�E���^
    Private sDenpyoNo As String = ""
    Private sOkurijyoStr As String = ""

    Private rpt As H01R20S_Okurijou
    Private rpt2 As H01R20S_Okurijou

    Private Const RS As String = "RecSet"                       '���R�[�h�Z�b�g�e�[�u��
    Private Const HANYO_KAHENKEY_OKURIJYO As String = "H01R20"  '�����

    '---------------------------------------------------------------------------------
    '���|�[�g��\
    '---------------------------------------------------------------------------------
    Public Sub runReport() Implements RepSectionIF.runReport
        Dim v As New PreviewForm
        v.viewer.Document = Me.Document
        Me.Run()
        If frmH01F70_Chohyo.printKbn = CommonConst.REPORT_PREVIEW Then
            v.Show()            '�v���r���[���
        Else
            Me.Document.Print   '�v�����^�I���_�C�A���O
        End If
    End Sub

    '---------------------------------------------------------------------------------
    '�f�[�^���
    '---------------------------------------------------------------------------------
    Public Sub setData(prmDs As DataSet, ByRef prmRefDbHd As UtilDBIf) Implements RepSectionIF.setData
        Dim dt As DataTableCollection

        _ds = prmDs
        dt = _ds.Tables
        _PageCount = dt.Item(0).Rows.Count                                  '�y�[�W�J�E���g
        _db = prmRefDbHd
        rowIdx = 0
        ' ���ʏ����g�p�̏���
        _comLogc = New CommonLogic(_db, _msgHd)                             '���ʏ����p
        _companyCd = frmC01F10_Login.loginValue.BumonCD                     '��ЃR�[�h
        _shoriId = frmH01F70_Chohyo._shoriId                                '����ID
        _userId = frmC01F10_Login.loginValue.TantoCD                        '���[�U�h�c
        _printKbn = frmH01F70_Chohyo._printKbn

        '�ėp�}�X�^���e�i�[
        Call _comLogc.Get_HanyouMST()

        '�[�i���E�������̒��[���l�擾
        For lidx As Integer = 0 To UBound(CommonLogic.uHanyou_tb) - 1
            If CommonLogic.uHanyou_tb(lidx).KoteiKey.Equals(CommonConst.HANYO_CHOHYO_BIKOU) Then
                If CommonLogic.uHanyou_tb(lidx).KahenKey.Equals(HANYO_KAHENKEY_OKURIJYO) Then
                    sOkurijyoStr = CommonLogic.uHanyou_tb(lidx).Char1
                End If
            End If
        Next

    End Sub

    '---------------------------------------------------------------------------------
    '���|�[�g�X�^�[�g
    '---------------------------------------------------------------------------------
    Private Sub H01R20_Okurijou_ReportStart(sender As Object, e As EventArgs) Handles MyBase.ReportStart
        rpt = New H01R20S_Okurijou()
        rpt2 = New H01R20S_Okurijou()
    End Sub

    '---------------------------------------------------------------------------------
    '�f�[�^�C�j�V�����C�Y
    '---------------------------------------------------------------------------------
    Private Sub H01R20_Okurijou_DataInitialize(sender As Object, e As EventArgs) Handles MyBase.DataInitialize

    End Sub

    '---------------------------------------------------------------------------------
    '�y�[�W�w�b�_��������
    '---------------------------------------------------------------------------------
    Private Sub PageHeader_Format(sender As Object, e As EventArgs) Handles PageHeader.Format

    End Sub

    '---------------------------------------------------------------------------------
    '�t�F�b�`�f�[�^
    '---------------------------------------------------------------------------------
    Private Sub H01R20_Okurijou_FetchData(sender As Object, eArgs As FetchEventArgs) Handles MyBase.FetchData
        Try
            txtUnsoubin1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�^����"))
            lblYuubinNo1.Text = "�� " & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�X�֔ԍ�")).Substring(0, 3) & "-" & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�X�֔ԍ�")).Substring(3, 4)
            lblAddress1_1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�Z���P")) & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�Z���Q"))
            lblAddress1_2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�Z���R"))
            lblShukkasakiNm1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�o�א於")) & "�@" & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�S���Җ�")) & "�@�l"
            lblShukkasakiCd1.Text = "(" & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�o�א�R�[�h")) & ")"
            lblCompanyNm1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("��Ж�"))
            lblCoPosition1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("��Б�\�Җ�E"))
            lblCoPresidentNm1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("��Б�\�Җ�"))
            lblCoYuubinNo1.Text = "��" & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("��ЗX�֔ԍ�")).Substring(0, 3) & "-" & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("��ЗX�֔ԍ�")).Substring(3, 4)
            lblCoAddress1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("��ЏZ���P")) & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("��ЏZ���Q")) & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("��ЏZ���R"))
            lblCoTelNo1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("��Гd�b�ԍ�"))
            lblCoFaxNo1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("��Ђe�`�w�ԍ�"))

            txtPageNo.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�y�[�W"))
            txtShagaiBikou1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�ЊO���l"))
            '��
            _kosuSum = _kosuSum + Integer.Parse(_ds.Tables(RS).Rows(rowIdx)("��"))
            If _PageCount > Integer.Parse(_ds.Tables(RS).Rows(rowIdx)("�y�[�W")) Then
                txtKosuuSum1.Text = "***"
            Else
                txtKosuuSum1.Text = _kosuSum.ToString()
            End If
            txtJikanSitei1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("���Ԏw��"))
            txtShukkaDt1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�o�ד�"))
            txtChakuDt1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����"))
            txtDenpyoNo1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�����`��"))
            txtIrainusi1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�˗��哙"))

            txtUnsoubin2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�^����"))
            lblYuubinNo2.Text = "�� " & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�X�֔ԍ�")).Substring(0, 3) & "-" & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�X�֔ԍ�")).Substring(3, 4)
            lblAddress2_1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�Z���P")) & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�Z���Q"))
            lblAddress2_2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�Z���R"))
            lblShukkasakiNm2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�o�א於")) & "�@" & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�S���Җ�")) & "�@�l"
            lblShukkasakiCd2.Text = "(" & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�o�א�R�[�h")) & ")"
            lblCompanyNm2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("��Ж�"))
            lblCoPosition2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("��Б�\�Җ�E"))
            lblCoPresidentNm2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("��Б�\�Җ�"))
            lblCoYuubinNo2.Text = "��" & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("��ЗX�֔ԍ�")).Substring(0, 3) & "-" & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("��ЗX�֔ԍ�")).Substring(3, 4)
            lblCoAddress2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("��ЏZ���P")) & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("��ЏZ���Q")) & _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("��ЏZ���R"))
            lblCoTelNo2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("��Гd�b�ԍ�"))
            lblCoFaxNo2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("��Ђe�`�w�ԍ�"))

            txtShagaiBikou2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�ЊO���l"))
            '��
            If _PageCount > Integer.Parse(_ds.Tables(RS).Rows(rowIdx)("�y�[�W")) Then
                txtKosuuSum2.Text = "***"
            Else
                txtKosuuSum2.Text = _kosuSum.ToString()
            End If
            txtJikanSitei2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("���Ԏw��"))
            txtShukkaDt2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�o�ד�"))
            txtChakuDt2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����"))
            txtDenpyoNo2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�����`��"))
            txtIrainusi2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�˗��哙"))

            '���[���l
            txt���[���l.Text = sOkurijyoStr

            rowIdx = rowIdx + 1
            eArgs.EOF = False
        Catch ie As IndexOutOfRangeException
            eArgs.EOF = True '�f�[�^����
        End Try

    End Sub

    '---------------------------------------------------------------------------------
    '���|�[�g�G���h
    '---------------------------------------------------------------------------------
    Private Sub H01R20_Okurijou_ReportEnd(sender As Object, e As EventArgs) Handles MyBase.ReportEnd
        rpt.Dispose()
        rpt2.Dispose()
    End Sub

    Private Sub GroupFooter1_Format(sender As Object, e As EventArgs) Handles GroupFooter1.Format

    End Sub

    Private Sub PageFooter_Format(sender As Object, e As EventArgs) Handles PageFooter.Format

    End Sub

    Private Sub Detail_Format(sender As Object, e As EventArgs) Handles Detail.Format

        '�f�[�^���݃`�F�b�N
        Dim sSql As String = ""
        Dim rc As Integer = 0

        sSql = sSql & "SELECT "
        sSql = sSql & "      TRUNC(((T11_CYMNDT.�s�� - 1)/ 6) + 1) AS ""�y�[�W"" "
        sSql = sSql & "     ,T11_CYMNDT.���i�R�[�h  "
        sSql = sSql & "     ,T11_CYMNDT.���i�� || T11_CYMNDT.�׎p�`�� AS ""���i��"" "
        sSql = sSql & "     ,CASE WHEN T11_CYMNDT.�Ⓚ�敪 = '" & CommonConst.REITOU_KBN_REITOU & "' THEN M90_HANYO.�����P ELSE NULL END AS ""�����P"" "
        sSql = sSql & "     ,T11_CYMNDT.���� "
        sSql = sSql & "     ,T11_CYMNDT.�P�� "
        sSql = sSql & "     ,T11_CYMNDT.�� "
        sSql = sSql & "     ,'��' AS ""�P�ʂQ"" "
        sSql = sSql & "     ,T11_CYMNDT.���ה��l "
        sSql = sSql & "FROM T11_CYMNDT "
        sSql = sSql & "LEFT JOIN M90_HANYO On M90_HANYO.��ЃR�[�h = T11_CYMNDT.��ЃR�[�h "
        sSql = sSql & " And M90_HANYO.�Œ�L�[ = '" & CommonConst.HANYO_REITOU_KBN & "' "
        sSql = sSql & " AND M90_HANYO.�σL�[ = T11_CYMNDT.�Ⓚ�敪 "
        sSql = sSql & "WHERE T11_CYMNDT.��ЃR�[�h = '" & frmC01F10_Login.loginValue.BumonCD & "' "
        sSql = sSql & "  AND T11_CYMNDT.�����`�� = '" & Me.txtDenpyoNo1.Text & "' "
        sSql = sSql & "  AND TRUNC(((T11_CYMNDT.�s�� - 1) / 6) + 1) = '" & txtPageNo.Text & "' "
        sSql = sSql & "ORDER BY T11_CYMNDT.�s�� "

        Dim ds As DataSet = _db.selectDB(sSql, RS, rc)

        rpt.DataSource = ds
        rpt.DataMember = RS
        SubReport1.Report = rpt

        Dim ds2 As DataSet = _db.selectDB(sSql, RS, rc)

        rpt2.DataSource = ds2
        rpt2.DataMember = RS
        SubReport2.Report = rpt2

    End Sub

End Class
