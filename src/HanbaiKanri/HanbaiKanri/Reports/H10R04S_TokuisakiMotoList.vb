'===============================================================================
'
'�@�J�l�L�g�c���X�l
'�@�@�i�V�X�e�����j�̔��Ǘ�
'�@�@�i�����@�\���j���Ӑ挳���i�T�u���|�[�g�j
'    �i�t�H�[��ID�jH10R04
'
'===============================================================================
'�@�����@���O�@�@�@�@�@���@�t       �}�[�N      ���e
'-------------------------------------------------------------------------------
'�@(1)   ���        2018/03/13                 �V�K              
'-------------------------------------------------------------------------------
Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Document
Imports GrapeCity.ActiveReports.SectionReportModel
Imports UtilMDL.DB
Public Class H10R04S_TokuisakiMotoList

    Private _db As UtilDBIf
    Private RS As String
    Private _ds As DataSet
    Private rowIdx As Integer = 0
    Private _denpyoBango As String = "000000"

    '---------------------------------------------------------------------------------
    '���|�[�g�X�^�[�g
    '---------------------------------------------------------------------------------
    Private Sub H10R04S_TokuisakiMotoList_ReportStart(sender As Object, e As EventArgs) Handles MyBase.ReportStart
        RS = Me.DataMember
        _ds = Me.DataSource
        _db = H10R04_TokuisakiMotoList.parentDb
        rowIdx = 0
    End Sub

    Private Sub H10R04S_TokuisakiMotoList_FetchData(sender As Object, eArgs As FetchEventArgs) Handles MyBase.FetchData
        Try

            txt�ϑ���.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�ϑ���"))
            txt�`�[�ԍ�.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�`�[�ԍ�"))
            txt�敪.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�敪"))
            txt�o�א於.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�o�א�"))
            txt���i.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("���i��"))
            fld�c����.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�c����"))
            fld���P��.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("���P��"))
            fld�����z.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�����z"))
            '���ׂ̏��
            If rowIdx = 0 Then
                Line4.Visible = False
            ElseIf Not _denpyoBango.Equals(_db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�`�[�ԍ�"))) Then
                Line4.Visible = True
            Else
                Line4.Visible = False
            End If
            _denpyoBango = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("�`�[�ԍ�"))

            rowIdx = rowIdx + 1

            eArgs.EOF = False
        Catch ie As IndexOutOfRangeException
            eArgs.EOF = True '�f�[�^����
        End Try
    End Sub

    Private Sub H10R04S_TokuisakiMotoList_DataInitialize(sender As Object, e As EventArgs) Handles MyBase.DataInitialize

    End Sub

    Private Sub H10R04S_TokuisakiMotoList_ReportEnd(sender As Object, e As EventArgs) Handles MyBase.ReportEnd

    End Sub

    Private Sub Detail_Format(sender As Object, e As EventArgs) Handles Detail.Format

    End Sub
End Class
