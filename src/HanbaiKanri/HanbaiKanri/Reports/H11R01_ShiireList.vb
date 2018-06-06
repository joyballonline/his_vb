'===============================================================================
'
'�@�J�l�L�g�c���X�l
'�@�@�i�V�X�e�����j�̔��Ǘ�
'�@�@�i�����@�\���j�d�����ו\
'    �i�t�H�[��ID�jH11R01
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ���        2018/03/15                 �V�K              
'-------------------------------------------------------------------------------
Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Document
Imports HanbaiKanri
Imports UtilMDL.DB
Imports UtilMDL.MSG

Public Class H11R01_ShiireList
    Implements RepSectionIF

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _ds As DataSet
    Private _comLogc As CommonLogic                         '���ʏ����p
    Private _companyCd As String
    Private _shoriId As String
    Private _userId As String
    Private _printKbn As String
    Private rowIdx As Integer = 0
    Private _PageCount = 0
    Private iDenpyoCnt As Integer = 0
    Private iMeisaiCnt As Integer = 0
    Private dcSiireKin As Decimal = 0
    Private dcShohizei As Decimal = 0
    Private dcGoukeiKin As Decimal = 0
    Private sDenpyoNo As String = ""

    Private Const RS As String = "RecSet"                           '���R�[�h�Z�b�g�e�[�u��

    '---------------------------------------------------------------------------------
    '���|�[�g��\
    '---------------------------------------------------------------------------------
    Public Sub runReport() Implements RepSectionIF.runReport
        Dim v As New PreviewForm
        v.viewer.Document = Me.Document
        Me.Run()
        If frmH11F01_ShiireList.printKbn = CommonConst.REPORT_PREVIEW Then
            v.Show()            '�v���r���[���
        Else
            Me.Document.Print   '�v�����^�I���_�C�A���O
        End If
    End Sub

    '---------------------------------------------------------------------------------
    '�f�[�^���
    '---------------------------------------------------------------------------------
    Public Sub setData(prmDs As DataSet, ByRef prmRefDbHd As UtilDBIf) Implements RepSectionIF.setData

        _ds = prmDs
        _db = prmRefDbHd
        rowIdx = 0
        ' ���ʏ����g�p�̏���
        _comLogc = New CommonLogic(_db, _msgHd)                             ' ���ʏ����p
        _companyCd = frmC01F10_Login.loginValue.BumonCD                     '��ЃR�[�h
        _shoriId = frmH11F01_ShiireList.shoriId                             '����ID
        _userId = frmC01F10_Login.loginValue.TantoCD                        '���[�U�h�c
        _printKbn = frmH11F01_ShiireList.printKbn

    End Sub

    '---------------------------------------------------------------------------------
    '�O���[�v�w�b�_�[
    '---------------------------------------------------------------------------------
    Private Sub gh�`�[_Format(sender As Object, e As EventArgs) Handles gh�`�[.Format

    End Sub

    '---------------------------------------------------------------------------------
    '�t�F�b�`�f�[�^
    '---------------------------------------------------------------------------------
    Private Sub H11R01_ShiireList_FetchData(sender As Object, eArgs As FetchEventArgs) Handles MyBase.FetchData
        Try

            '�O���[�s���O�p
            fid�`�[�ԍ�.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�d���`��"))

            '�y�[�W�w�b�_
            txt�敪�w�b�_.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����_�敪"))
            txt�d����w�b�_.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����_�d����"))
            txt�`�[�ԍ��w�b�_.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����_�`�[�ԍ�"))
            txt�쐬���w�b�_.Text = _comLogc.getSysDdate
            '�y�[�W�̓v���p�e�B�ōs���Ă���

            '�`�[�ԍ��w�b�_
            txt�`�[�ԍ�.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�d���`��"))
            txt�d����.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�d����"))
            txt�d���於.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�d���於"))
            txt�x����ID.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�x����ID"))
            txt�x���於.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�x���於"))

            '����
            txt���i��.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("���i��"))
            txt�ŋ敪.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�ېŋ敪"))
            fld����.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("����"))
            fld��.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("��"))
            fld����.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�d������"))
            txt�P��.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�P��"))
            fld�d���P��.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�d���P��"))
            fld�d�����z.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�d�����z"))
            txt���ה��l.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�d�����ה��l"))

            '�`�[�ԍ��t�b�^
            fld�d�����z�O���[�v.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�d�����z�v"))
            fld����ŃO���[�v.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����Ōv"))
            fld���v���z�O���[�v.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�ō��z�v"))

            '�y�[�W�t�b�^
            txt��Ж�.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("��Ж�"))

            '�`�[�ԍ��u���C�N
            If Not _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�d���`��")).Equals(sDenpyoNo) Then
                dcSiireKin = dcSiireKin + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�d�����z�v"))
                dcShohizei = dcShohizei + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("����Ōv"))
                dcGoukeiKin = dcGoukeiKin + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�ō��z�v"))
                iDenpyoCnt = iDenpyoCnt + 1
            End If
            sDenpyoNo = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�d���`��"))

            '���|�[�g�t�b�^
            iMeisaiCnt = iMeisaiCnt + 1
            txt�`�[����.Text = iDenpyoCnt.ToString("#,##0") & " ��"
            txt���׌���.Text = iMeisaiCnt.ToString("#,##0") & " ��"
            fld�d�����z�y�[�W.Value = Decimal.Parse(dcSiireKin)
            fld����Ńy�[�W.Value = Decimal.Parse(dcShohizei)
            fld���v���z�y�[�W.Value = Decimal.Parse(dcGoukeiKin)

            rowIdx = rowIdx + 1
            eArgs.EOF = False
        Catch ie As IndexOutOfRangeException
            eArgs.EOF = True '�f�[�^����
        End Try

    End Sub

    '---------------------------------------------------------------------------------
    '���|�[�g�G���h
    '---------------------------------------------------------------------------------
    Private Sub H11R01_ShiireList_ReportEnd(sender As Object, e As EventArgs) Handles MyBase.ReportEnd

        '���엚�����O�쐬
        Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, _shoriId, CommonConst.PROGRAM_REPORT, CommonConst.STATUS_NORMAL,
                                                txt�敪�w�b�_.Text, txt�d����w�b�_.Text, txt�`�[�ԍ��w�b�_.Text, _printKbn, DBNull.Value,
                                                iDenpyoCnt, iMeisaiCnt, dcSiireKin, dcShohizei, dcGoukeiKin, _userId)


    End Sub
End Class
