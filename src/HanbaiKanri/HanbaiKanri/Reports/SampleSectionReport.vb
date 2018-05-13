Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Document
Imports HanbaiKanri
Imports UtilMDL.DB

Public Class SampleSectionReport
    Implements RepSectionIF

    Private _db As UtilDBIf
    Private _ds As DataSet
    Private rowIdx As Integer = 0

    Private Const RS As String = "RecSet"                           '���R�[�h�Z�b�g�e�[�u��

    '---------------------------------------------------------------------------------
    '���|�[�g��\
    '---------------------------------------------------------------------------------
    Public Sub runReport() Implements RepSectionIF.runReport
        Dim v As New PreviewForm
        v.viewer.Document = Me.Document
        Me.Run()
        v.Show()
    End Sub

    '---------------------------------------------------------------------------------
    '�f�[�^���
    '---------------------------------------------------------------------------------
    Public Sub setData(prmDs As DataSet, ByRef prmRefDbHd As UtilDBIf) Implements RepSectionIF.setData

        _ds = prmDs
        _db = prmRefDbHd
        rowIdx = 0

    End Sub

    '---------------------------------------------------------------------------------
    '���|�[�g�X�^�[�g
    '---------------------------------------------------------------------------------
    Private Sub SampleSectionReport_ReportStart(sender As Object, e As EventArgs) Handles Me.ReportStart

        '���������
        Debug.Print("SampleSectionReport_ReportStart �ʉ�")

    End Sub

    '---------------------------------------------------------------------------------
    '�f�[�^�C�j�V�����C�Y
    '---------------------------------------------------------------------------------
    Private Sub SampleSectionReport_DataInitialize(sender As Object, e As EventArgs) Handles Me.DataInitialize

        '���������
        Debug.Print("SampleSectionReport_DataInitialize �ʉ�")

    End Sub

    '---------------------------------------------------------------------------------
    '�y�[�W�w�b�_�[��������
    '---------------------------------------------------------------------------------
    Private Sub PageHeader_Format(sender As Object, e As EventArgs) Handles PageHeader.Format

        txtPrm.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("prm"))

    End Sub


    '---------------------------------------------------------------------------------
    '�O���[�v�w�b�_�[��������
    '---------------------------------------------------------------------------------
    Private Sub GroupHeader1_Format(sender As Object, e As EventArgs) Handles GroupHeader1.Format

        '���������
        Debug.Print("GroupHeader1_Format �ʉ�")

    End Sub

    '---------------------------------------------------------------------------------
    '�t�F�b�`�f�[�^
    '---------------------------------------------------------------------------------
    Private Sub SampleSectionReport_FetchData(sender As Object, eArgs As FetchEventArgs) Handles Me.FetchData
        Try

            txtCol1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("col1"))
            txtCol2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("col2"))
            txtCol3.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("col3"))

            rowIdx = rowIdx + 1
            eArgs.EOF = False
        Catch ie As IndexOutOfRangeException
            eArgs.EOF = True '�f�[�^����
        End Try

    End Sub

    '---------------------------------------------------------------------------------
    '�O���[�v�t�b�_�\��������
    '---------------------------------------------------------------------------------
    Private Sub GroupFooter1_Format(sender As Object, e As EventArgs) Handles GroupFooter1.Format

        '���������
        Debug.Print("GroupFooter1_Format �ʉ�")

    End Sub

    '---------------------------------------------------------------------------------
    '�y�[�W�t�b�_�[��������
    '---------------------------------------------------------------------------------
    Private Sub PageFooter_Format(sender As Object, e As EventArgs) Handles PageFooter.Format

        '���������
        Debug.Print("PageFooter_Format �ʉ�")

    End Sub

    '---------------------------------------------------------------------------------
    '���|�[�g�G���h
    '---------------------------------------------------------------------------------
    Private Sub SampleSectionReport_ReportEnd(sender As Object, e As EventArgs) Handles Me.ReportEnd

        '���������
        Debug.Print("SampleSectionReport_ReportEnd �ʉ�")

    End Sub
End Class
