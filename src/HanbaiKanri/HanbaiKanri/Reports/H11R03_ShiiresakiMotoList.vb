'===============================================================================
'
'�@�J�l�L�g�c���X�l
'�@�@�i�V�X�e�����j�̔��Ǘ�
'�@�@�i�����@�\���j�d���挳��
'    �i�t�H�[��ID�jH11R03
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ���        2018/03/20                 �V�K              
'-------------------------------------------------------------------------------
Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Document
Imports GrapeCity.ActiveReports.SectionReportModel
Imports HanbaiKanri
Imports UtilMDL.DB
Imports UtilMDL.MSG

Public Class H11R03_ShiiresakiMotoList
    Implements RepSectionIF

    Private _msgHd As UtilMsgHandler
    Private Shared _db As UtilDBIf
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
    'Private jyokenBotonn As String = ""
    Private _denpyoBango As String = "000000"
    Private _zandaka As Decimal = 0
    Private _shiharaisakiCd As String = ""
    Private _hidukeFrom As String = ""
    Private _hidukeTo As String = ""
    '�T�u���|�[�g
    Private rptSub As H11R03_ShiiresakiMotoList

    Private Const RS As String = "RecSet"                   '���R�[�h�Z�b�g�e�[�u��


    '-------------------------------------------------------------------------------
    '�v���p�e�B�錾
    '-------------------------------------------------------------------------------
    Public Shared ReadOnly Property parentDb() As UtilDBIf
        Get
            Return _db
        End Get
    End Property


    '---------------------------------------------------------------------------------
    '���|�[�g��\
    '---------------------------------------------------------------------------------
    Public Sub runReport() Implements RepSectionIF.runReport
        Dim v As New PreviewForm
        v.viewer.Document = Me.Document
        Me.Run()
        If frmH11F03_SiiresakiMotoList.printKbn = CommonConst.REPORT_PREVIEW Then
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
        _shoriId = frmH11F03_SiiresakiMotoList.shoriId                         '����ID
        _userId = frmC01F10_Login.loginValue.TantoCD                        '���[�U�h�c
        _printKbn = frmH11F03_SiiresakiMotoList.printKbn

    End Sub

    '---------------------------------------------------------------------------------
    '���|�[�g�X�^�[�g
    '---------------------------------------------------------------------------------
    Private Sub H11R03_ShiiresakiMotoList_ReportStart(sender As Object, e As EventArgs) Handles MyBase.ReportStart
        rptSub = New H11R03_ShiiresakiMotoList()
    End Sub

    '---------------------------------------------------------------------------------
    '�t�F�b�`�f�[�^
    '---------------------------------------------------------------------------------
    Private Sub H11R03_ShiiresakiMotoList_FetchData(sender As Object, eArgs As FetchEventArgs) Handles MyBase.FetchData
        Try

            '�O���[�s���O�p
            fld�x����.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�x����R�[�h"))
            _shiharaisakiCd = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�x����R�[�h"))
            _hidukeFrom = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����_���tFrom"))
            _hidukeTo = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����_���tTo"))

            '�y�[�W�w�b�_
            txt�敪�w�b�_.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����_�敪"))
            txt�d���x���N���w�b�_.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����_�N��"))
            txt���t�w�b�_.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����_���t"))
            txt�쐬���w�b�_.Text = _comLogc.getSysDdate
            txt�o�͏��w�b�_.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����_�o�͏�"))
            '�y�[�W�̓v���p�e�B�ōs���Ă���

            '������w�b�_
            txt�x����R�[�h.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�x����R�[�h"))
            txt�x���於.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�x���於"))

            '�c���v�Z�@�O���ׂ̎c���{�d���z�{����Ł|�x���z
            If _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�擾���e�[�u��")).Equals("1") Then
                _zandaka = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�c��"))
            Else
                _zandaka = _zandaka + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�d���z")) _
                                    + _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�����")) _
                                    - _db.rmNullDouble(_ds.Tables(RS).Rows(rowIdx)("�x���z"))

            End If

            '����
            If _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�擾���e�[�u��")).Equals("1") Then
                '���c��
                txt�d����.Text = ""
                txt�`�[�ԍ�.Text = ""
                txt�敪.Text = ""
                txt�d���於.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�d����"))
                txt���i�E�v.Text = ""
                txt����.Text = ""
                txt�P��.Text = ""
                txt�P��.Text = ""

                '�x����t�b�^�W�v�p�i�W�v�̓v���p�e�B�ōs���Ă���j
                fld�d���z.Value = 0
                fld�����.Value = 0
                fld�x���z.Value = 0
                fld�c��.Value = _zandaka

            ElseIf _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�擾���e�[�u��")).Equals("2") Then
                '�d����{�^����
                If Not _denpyoBango.Equals(_db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�d���`��"))) Then
                    txt�d����.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�d����"))
                    txt�`�[�ԍ�.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�d���`��"))
                    txt�敪.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�敪"))
                    txt�d���於.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�d����"))
                Else
                    '�d�����\��
                    txt�d����.Text = ""
                    txt�`�[�ԍ�.Text = ""
                    txt�敪.Text = ""
                    txt�d���於.Text = ""
                End If
                txt���i�E�v.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("���i�d���E�v"))
                txt����.Text = Decimal.Parse(_ds.Tables(RS).Rows(rowIdx)("����")).ToString("#,##0.00")
                txt�P��.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�P��"))
                txt�P��.Text = Decimal.Parse(_ds.Tables(RS).Rows(rowIdx)("�P��")).ToString("#,##0.00")

                '�x����t�b�^�W�v�p�i�W�v�̓v���p�e�B�ōs���Ă���j
                fld�d���z.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�d���z"))
                fld�����.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�����"))
                fld�x���z.Value = 0
                fld�c��.Value = _zandaka
            Else
                '�x����{�^����
                If Not _denpyoBango.Equals(_db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�d���`��"))) Then
                    txt�d����.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�d����"))
                    txt�`�[�ԍ�.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�d���`��"))
                    txt�敪.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�敪"))
                Else
                    '�d�����\��
                    txt�d����.Text = ""
                    txt�`�[�ԍ�.Text = ""
                    txt�敪.Text = ""
                End If
                txt�d���於.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�d����"))
                txt���i�E�v.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("���i�d���E�v"))
                txt����.Text = ""
                txt�P��.Text = ""
                txt�P��.Text = ""
                txt�d���z.Text = ""
                txt�����.Text = ""

                '�x����t�b�^�W�v�p�i�W�v�̓v���p�e�B�ōs���Ă���j
                fld�d���z.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�d���z"))
                fld�����.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�����"))
                fld�x���z.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�x���z"))
                fld�c��.Value = _zandaka
            End If


            '���ׂ̏��
            If _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�擾���e�[�u��")).Equals("1") Then
                Line4.Visible = False
            ElseIf Not _denpyoBango.Equals(_db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�d���`��"))) Then
                Line4.Visible = True
            Else
                Line4.Visible = False
            End If
            _denpyoBango = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�d���`��"))

            '�y�[�W�t�b�^
            txt��Ж�.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("��Ж�"))

            '���|�[�g�t�b�^

            rowIdx = rowIdx + 1

            '���O�p
            iMeisaiCnt = rowIdx

            eArgs.EOF = False
        Catch ie As IndexOutOfRangeException
            eArgs.EOF = True '�f�[�^����
        End Try

    End Sub


    '---------------------------------------------------------------------------------
    'Detail_Format�i�y�[�W�t�b�^�[�̕����ݒ�j
    '---------------------------------------------------------------------------------
    Private Sub Detail_Format(sender As Object, e As EventArgs) Handles Detail.Format

    End Sub

    '---------------------------------------------------------------------------------
    '�y�[�W�w�b�_�[�i�y�[�W�t�b�^�[�̕����ݒ�j
    '---------------------------------------------------------------------------------
    Private Sub PageHeader_Format(sender As Object, e As EventArgs) Handles PageHeader.Format

    End Sub

    '---------------------------------------------------------------------------------
    '���|�[�g�G���h
    '---------------------------------------------------------------------------------
    Private Sub H11R03_ShiiresakiMotoList_ReportEnd(sender As Object, e As EventArgs) Handles MyBase.ReportEnd

        '���엚�����O�쐬
        Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, _shoriId, CommonConst.PROGRAM_REPORT, CommonConst.STATUS_NORMAL,
                                                CommonConst.SHIIRE_KBN_NM_SHIIRE, txt�d���x���N���w�b�_.Text, txt���t�w�b�_.Text, _printKbn, DBNull.Value,
                                                iMeisaiCnt, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _userId)


    End Sub

End Class
