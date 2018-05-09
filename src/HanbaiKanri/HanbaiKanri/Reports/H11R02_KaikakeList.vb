'===============================================================================
'
'�@�J�l�L�g�c���X�l
'�@�@�i�V�X�e�����j�̔��Ǘ�
'�@�@�i�����@�\���j���|���ꗗ�\
'    �i�t�H�[��ID�jH11R02
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ���        2018/03/16                 �V�K              
'-------------------------------------------------------------------------------
Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Document
Imports HanbaiKanri
Imports UtilMDL.DB
Imports UtilMDL.MSG

Public Class H11R02_KaikakeList
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
    Private dcSiharaiZan_Gokei As Decimal = 0
    Private _shukkabu As String = ""

    Private Const RS As String = "RecSet"                   '���R�[�h�Z�b�g�e�[�u��


    '---------------------------------------------------------------------------------
    '���|�[�g��\
    '---------------------------------------------------------------------------------
    Public Sub runReport() Implements RepSectionIF.runReport
        Dim v As New PreviewForm
        v.viewer.Document = Me.Document
        Me.Run()
        If frmH11F02_KaikakeList.printKbn = CommonConst.REPORT_PREVIEW Then
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
        _shoriId = frmH11F02_KaikakeList.shoriId                         '����ID
        _userId = frmC01F10_Login.loginValue.TantoCD                        '���[�U�h�c
        _printKbn = frmH11F02_KaikakeList.printKbn

    End Sub

    '---------------------------------------------------------------------------------
    '�t�F�b�`�f�[�^
    '---------------------------------------------------------------------------------
    Private Sub H10R03_UrikakeKinList_FetchData(sender As Object, eArgs As FetchEventArgs) Handles MyBase.FetchData
        Try

            '�y�[�W�w�b�_
            txt�敪�w�b�_.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����_�敪"))
            txt�d���x���N���w�b�_.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����_�N��"))
            txt�쐬���w�b�_.Text = _comLogc.getSysDdate
            txt�o�͏��w�b�_.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����_�o�͏�"))
            '�y�[�W�̓v���p�e�B�ōs���Ă���

            '����
            txt�ԍ�.Text = (rowIdx + 1).ToString()
            txt�R�[�h.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�x����R�[�h"))
            txt�x����.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�x���於"))

            '�W�v�p�i�W�v�̓v���p�e�B�ōs���Ă���j
            fld�O���c.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�O���c"))
            fld�d���z.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�d���z"))
            fld���̎萔��.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("���̎萔��"))
            fld�d�����v.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�d���z") + _ds.Tables(RS).Rows(rowIdx)("���̎萔��"))
            fld�����.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�d�������"))
            fld�ō��z.Value = CType(fld�d�����v.Value, Decimal) + CType(fld�����.Value, Decimal)
            fld�x���z.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�x���z"))
            fld�萔��.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�萔��"))
            fld�����c.Value = CType(fld�O���c.Value, Decimal) + CType(fld�ō��z.Value, Decimal) -
                              CType(fld�x���z.Value, Decimal) - CType(fld�萔��.Value, Decimal)

            '�y�[�W�t�b�^
            txt��Ж�.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("��Ж�"))

            '���|�[�g�t�b�^

            rowIdx = rowIdx + 1

            '���O�p
            iMeisaiCnt = rowIdx
            dcSiharaiZan_Gokei = dcSiharaiZan_Gokei + CType(fld�����c.Value, Decimal)

            eArgs.EOF = False
        Catch ie As IndexOutOfRangeException
            eArgs.EOF = True '�f�[�^����
        End Try

    End Sub

    '---------------------------------------------------------------------------------
    '���|�[�g�G���h
    '---------------------------------------------------------------------------------
    Private Sub H10R03_UrikakeKinList_ReportEnd(sender As Object, e As EventArgs) Handles MyBase.ReportEnd

        '���엚�����O�쐬
        Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, _shoriId, CommonConst.PROGRAM_REPORT, CommonConst.STATUS_NORMAL,
                                                txt�敪�w�b�_.Text, txt�d���x���N���w�b�_.Text, DBNull.Value, _printKbn, DBNull.Value,
                                                iMeisaiCnt, DBNull.Value, DBNull.Value, dcSiharaiZan_Gokei, DBNull.Value, _userId)


    End Sub

End Class
