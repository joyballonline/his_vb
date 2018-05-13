'===============================================================================
'
'�@�J�l�L�g�c���X�l
'�@�@�i�V�X�e�����j�̔��Ǘ�
'�@�@�i�����@�\���j���|���ꗗ�\
'    �i�t�H�[��ID�jH10R03
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ���        2018/03/06                 �V�K              
'-------------------------------------------------------------------------------
Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Document
Imports HanbaiKanri
Imports UtilMDL.DB
Imports UtilMDL.MSG

Public Class H10R03_UrikakeKinList
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
    Private dcNyukinZan_Itaku As Decimal = 0
    Private dcNyukinZan_Uriage As Decimal = 0
    Private dcNyukinZan_Gokei As Decimal = 0
    Private _shukkabu As String = ""
    Private jyokenBotonn As String = ""

    Private Const RS As String = "RecSet"                   '���R�[�h�Z�b�g�e�[�u��


    '---------------------------------------------------------------------------------
    '���|�[�g��\
    '---------------------------------------------------------------------------------
    Public Sub runReport() Implements RepSectionIF.runReport
        Dim v As New PreviewForm
        v.viewer.Document = Me.Document
        Me.Run()
        If frmH10F03_UrikakeKinList.printKbn = CommonConst.REPORT_PREVIEW Then
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
        _shoriId = frmH10F03_UrikakeKinList.shoriId                         '����ID
        _userId = frmC01F10_Login.loginValue.TantoCD                        '���[�U�h�c
        _printKbn = frmH10F03_UrikakeKinList.printKbn

    End Sub

    '---------------------------------------------------------------------------------
    '�t�F�b�`�f�[�^
    '---------------------------------------------------------------------------------
    Private Sub H10R03_UrikakeKinList_FetchData(sender As Object, eArgs As FetchEventArgs) Handles MyBase.FetchData
        Try

            '�O���[�s���O�p
            fid�o�א敪��.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�o�א敪��"))

            '�y�[�W�w�b�_
            txt�敪�w�b�_.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����_�敪"))
            txt��������N���w�b�_.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����_�N��"))
            txt�쐬���w�b�_.Text = _comLogc.getSysDdate
            txt�o�͏��w�b�_.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����_�o�͏�"))
            jyokenBotonn = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����_���O�p"))
            '�y�[�W�̓v���p�e�B�ōs���Ă���

            '�o�א敪�ރw�b�_

            '����
            txt�ԍ�.Text = (rowIdx + 1).ToString()
            txt�R�[�h.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("������R�[�h"))
            txt�����於.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�����於"))

            '�W�v�p�i�W�v�̓v���p�e�B�ōs���Ă���j
            fld�O���c.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�O���c�W�v"))
            fld����z.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����z�W�v"))
            fld�����.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����Ŋz�W�v"))
            fld�ō��z.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�ō��z�W�v"))
            fld�����z.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�����z�W�v"))
            fld�萔��.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�萔���W�v"))
            fld�����c�z.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�����c�z"))

            '�y�[�W�t�b�^
            txt��Ж�.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("��Ж�"))

            '���|�[�g�t�b�^

            rowIdx = rowIdx + 1

            '���O�p
            iMeisaiCnt = rowIdx
            If fid�o�א敪��.Value.Equals(CommonConst.SKBUNRUI_ITAKU) Then
                dcNyukinZan_Itaku = dcNyukinZan_Itaku + CType(fld�����c�z.Value, Decimal)
            Else
                dcNyukinZan_Uriage = dcNyukinZan_Uriage + CType(fld�����c�z.Value, Decimal)
            End If
            dcNyukinZan_Gokei = dcNyukinZan_Itaku + dcNyukinZan_Uriage

            eArgs.EOF = False
        Catch ie As IndexOutOfRangeException
            eArgs.EOF = True '�f�[�^����
        End Try

    End Sub

    '---------------------------------------------------------------------------------
    '�y�[�W�w�b�_�[�i�y�[�W�t�b�^�[�̕����ݒ�j
    '---------------------------------------------------------------------------------
    Private Sub PageHeader_Format(sender As Object, e As EventArgs) Handles PageHeader.Format
        If fid�o�א敪��.Value.Equals(CommonConst.SKBUNRUI_ITAKU) Then
            lbl���o�v.Text = "��[�ϑ��v]"
        Else
            lbl���o�v.Text = "��[����v]"
        End If
    End Sub

    '---------------------------------------------------------------------------------
    '���|�[�g�G���h
    '---------------------------------------------------------------------------------
    Private Sub H10R03_UrikakeKinList_ReportEnd(sender As Object, e As EventArgs) Handles MyBase.ReportEnd

        '���엚�����O�쐬
        Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, _shoriId, CommonConst.PROGRAM_REPORT, CommonConst.STATUS_NORMAL,
                                                jyokenBotonn, txt��������N���w�b�_.Text, DBNull.Value, _printKbn, DBNull.Value,
                                                iMeisaiCnt, dcNyukinZan_Itaku, dcNyukinZan_Uriage, dcNyukinZan_Gokei, DBNull.Value, _userId)


    End Sub

End Class
