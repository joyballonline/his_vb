'===============================================================================
'
'�@�J�l�L�g�c���X�l
'�@�@�i�V�X�e�����j�̔��Ǘ�
'�@�@�i�����@�\���j�������ו\
'    �i�t�H�[��ID�jH10R01
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ���        2018/02/26                 �V�K              
'-------------------------------------------------------------------------------
Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Document
Imports HanbaiKanri
Imports UtilMDL.DB
Imports UtilMDL.MSG

Public Class H10R01_ChumonList
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
    Private dcUriageKin As Decimal = 0
    Private dcShohizei As Decimal = 0
    Private dcGoukeiKin As Decimal = 0
    Private sDenpyoNo As String = ""

    Private Const RS As String = "RecSet"                   '���R�[�h�Z�b�g�e�[�u��

    '---------------------------------------------------------------------------------
    '���|�[�g��\
    '---------------------------------------------------------------------------------
    Public Sub runReport() Implements RepSectionIF.runReport
        Dim v As New PreviewForm
        v.viewer.Document = Me.Document
        Me.Run()
        If frmH10F01_ChumonList.printKbn = CommonConst.REPORT_PREVIEW Then
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
        _shoriId = frmH10F01_ChumonList.shoriId                             '����ID
        _userId = frmC01F10_Login.loginValue.TantoCD                        '���[�U�h�c
        _printKbn = frmH10F01_ChumonList.printKbn

    End Sub

    '---------------------------------------------------------------------------------
    '�`�[�O���[�v�w�b�_��������
    '---------------------------------------------------------------------------------
    Private Sub gh�`�[_Format(sender As Object, e As EventArgs) Handles gh�`�[.Format

    End Sub

    '---------------------------------------------------------------------------------
    '�t�F�b�`�f�[�^
    '---------------------------------------------------------------------------------
    Private Sub H10R01_ChumonList_FetchData(sender As Object, eArgs As FetchEventArgs) Handles MyBase.FetchData
        Try

            '�O���[�s���O�p
            fid�`�[�ԍ�.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�����`��"))

            '�y�[�W�w�b�_
            txt�敪�w�b�_.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����_�敪"))
            txt�o�ד��w�b�_.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����_�o�ד�"))
            txt�`�[�ԍ��w�b�_.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����_�`�[�ԍ�"))
            txt�쐬���w�b�_.Text = _comLogc.getSysDdate
            '�y�[�W�̓v���p�e�B�ōs���Ă���

            '�`�[�ԍ��w�b�_
            txt�`�[�ԍ�.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�����`��"))
            txt�o�ד�.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�o�ד�"))
            txt�o�א於.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�o�א於"))
            txt������ID.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("������ID"))
            txt�����於.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�����於"))
            txt����.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����"))

            '����
            txt���i��.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("���i��"))
            txt�Ⓚ.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�Ⓚ�敪"))
            txt�ŋ敪.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�ېŋ敪"))
            fld����.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("����"))
            fld��.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("��"))
            fld����.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("����"))
            txt�P��.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�P��"))
            fld����P��.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�����P��"))
            fld������z.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�������z"))
            txt���ה��l.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("���ה��l"))

            '�`�[�ԍ��t�b�^
            fld������z�O���[�v.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("������z�v"))
            fld����ŃO���[�v.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("����Ōv"))
            fld���v���z�O���[�v.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�ō��z�v"))
            txt�ЊO���l.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�ЊO���l"))

            '�y�[�W�t�b�^
            txt��Ж�.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("��Ж�"))

            '�`�[�ԍ��u���C�N
            If Not _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�����`��")).Equals(sDenpyoNo) Then
                dcUriageKin = dcUriageKin + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("������z�v"))
                dcShohizei = dcShohizei + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("����Ōv"))
                dcGoukeiKin = dcGoukeiKin + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�ō��z�v"))
                iDenpyoCnt = iDenpyoCnt + 1
            End If
            sDenpyoNo = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�����`��"))

            '���|�[�g�t�b�^
            iMeisaiCnt = iMeisaiCnt + 1
            txt�`�[����.Text = Decimal.Parse(iDenpyoCnt).ToString("#,##0") & " ��"
            txt���׌���.Text = Decimal.Parse(iMeisaiCnt).ToString("#,##0") & " ��"

            fld������z�y�[�W.Value = Decimal.Parse(dcUriageKin)
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
    Private Sub H10R01_ChumonList_ReportEnd(sender As Object, e As EventArgs) Handles MyBase.ReportEnd

        '���엚�����O�쐬
        Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, _shoriId, CommonConst.PROGRAM_REPORT, CommonConst.STATUS_NORMAL,
                                                txt�敪�w�b�_.Text, txt�o�ד��w�b�_.Text, txt�`�[�ԍ��w�b�_.Text, _printKbn, DBNull.Value,
                                                iDenpyoCnt, iMeisaiCnt, dcUriageKin, dcShohizei, dcGoukeiKin, _userId)


    End Sub

End Class
