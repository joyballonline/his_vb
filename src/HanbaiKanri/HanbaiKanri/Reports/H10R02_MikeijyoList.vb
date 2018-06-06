'===============================================================================
'
'�@�J�l�L�g�c���X�l
'�@�@�i�V�X�e�����j�̔��Ǘ�
'�@�@�i�����@�\���j���㖢�v��ꗗ�\
'    �i�t�H�[��ID�jH10R02
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

Public Class H10R02_MikeijyoList
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
    Private iMeisaiCnt As Integer = 0
    Private dcGoukeiKin As Decimal = 0
    Private _shukkabu As String = ""

    Private Const RS As String = "RecSet"                   '���R�[�h�Z�b�g�e�[�u��


    '---------------------------------------------------------------------------------
    '���|�[�g��\
    '---------------------------------------------------------------------------------
    Public Sub runReport() Implements RepSectionIF.runReport
        Dim v As New PreviewForm
        v.viewer.Document = Me.Document
        Me.Run()
        If frmH10F02_MikeijyoList.printKbn = CommonConst.REPORT_PREVIEW Then
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
        _shoriId = frmH10F02_MikeijyoList.shoriId                             '����ID
        _userId = frmC01F10_Login.loginValue.TantoCD                        '���[�U�h�c
        _printKbn = frmH10F02_MikeijyoList.printKbn

    End Sub

    '---------------------------------------------------------------------------------
    '�`�[�O���[�v�w�b�_
    '---------------------------------------------------------------------------------
    Private Sub gh�`�[_BeforePrint(sender As Object, e As EventArgs) Handles gh�`�[.BeforePrint

    End Sub
    '---------------------------------------------------------------------------------
    '�t�F�b�`�f�[�^
    '---------------------------------------------------------------------------------
    Private Sub H10R02_MikeijyoList_FetchData(sender As Object, eArgs As FetchEventArgs) Handles MyBase.FetchData
        Try

            '�O���[�s���O�p
            fid�`�[�ԍ�.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�ϑ��`��"))

            '�y�[�W�w�b�_
            txt�敪�w�b�_.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����_�敪"))
            txt�o�ד��w�b�_.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����_�o�ד�"))
            txt�쐬���w�b�_.Text = _comLogc.getSysDdate
            '�y�[�W�̓v���p�e�B�ōs���Ă���

            '�o�ד��w�b�_
            txt�o�ד�.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�o�ד�"))
            txt�`�[�ԍ�.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�ϑ��`��"))
            txt�o�א於.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�o�א於"))
            txt������ID.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("������ID"))
            txt�����於.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�����於"))
            txt����.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����"))

            '����
            txt���i��.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("���i����"))
            fld����.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("����"))
            fld��.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("��"))
            fld�ϑ�����.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�ϑ�����"))
            txt�P�ʈϑ�����.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�P��"))
            fld���㐔��.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("���㐔�ʌv"))
            txt�P�ʔ��㐔��.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�P��"))
            fld�ڐؐ���.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�ڐؐ��ʌv"))
            txt�P�ʖڐؐ���.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�P��"))
            fld�c����.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�ϑ��c��"))
            txt�P�ʎc����.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�P��"))
            fld���P��.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("���P��"))
            fld�c���z.Value = _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�ϑ��c���z"))

            '�y�[�W�t�b�^
            txt��Ж�.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("��Ж�"))

            '���|�[�g�t�b�^
            iMeisaiCnt = iMeisaiCnt + 1
            txt���׌���.Text = Decimal.Parse(iMeisaiCnt).ToString("#,##0") & " ��"

            '���O�p�W�v
            dcGoukeiKin = dcGoukeiKin + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�ϑ��c���z"))

            rowIdx = rowIdx + 1
            eArgs.EOF = False
        Catch ie As IndexOutOfRangeException
            eArgs.EOF = True '�f�[�^����
        End Try

    End Sub

    '---------------------------------------------------------------------------------
    '���|�[�g�G���h
    '---------------------------------------------------------------------------------
    Private Sub H10R02_MikeijyoList_ReportEnd(sender As Object, e As EventArgs) Handles MyBase.ReportEnd

        '���엚�����O�쐬
        Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, _shoriId, CommonConst.PROGRAM_REPORT, CommonConst.STATUS_NORMAL,
                                                txt�敪�w�b�_.Text, txt�o�ד��w�b�_.Text, DBNull.Value, _printKbn, DBNull.Value,
                                                iMeisaiCnt, dcGoukeiKin, DBNull.Value, DBNull.Value, DBNull.Value, _userId)


    End Sub

End Class
