Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Document
Imports GrapeCity.ActiveReports.SectionReportModel
Imports UtilMDL.DB

Public Class H01R20S_Okurijou

    Public _db As UtilDBIf
    Public _parentreport As H01R20_Okurijou
    Private RS As String
    Private _ds As DataSet
    Private _companyCd As String
    Private _shoriId As String
    Private _userId As String
    Private _printKbn As String
    Private rowIdx As Integer = 0
    Private RowNumber As Integer = 0�@�@' �����J�E���^

    '---------------------------------------------------------------------------------
    '���|�[�g�X�^�[�g
    '---------------------------------------------------------------------------------
    Private Sub H01R20S_Okurijou_ReportStart(sender As Object, e As EventArgs) Handles MyBase.ReportStart
        RS = Me.DataMember
        _ds = Me.DataSource
    End Sub

    '---------------------------------------------------------------------------------
    '�f�[�^�C�j�V�����C�Y
    '---------------------------------------------------------------------------------
    Private Sub H01R20S_Okurijou_DataInitialize(sender As Object, e As EventArgs) Handles MyBase.DataInitialize

    End Sub

    '---------------------------------------------------------------------------------
    '�t�F�b�`�f�[�^
    '---------------------------------------------------------------------------------
    Private Sub H01R20S_Okurijou_FetchData(sender As Object, eArgs As FetchEventArgs) Handles MyBase.FetchData
        'Try
        '    txtGoodsNm.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("���i��"))
        '    txtIrisu.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("����"))
        '    txtKosu.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("��"))
        '    txtBiko.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("���ה��l"))

        '    rowIdx = rowIdx + 1
        '    eArgs.EOF = False
        'Catch ie As IndexOutOfRangeException
        '    eArgs.EOF = True '�f�[�^����
        'End Try

    End Sub

    '---------------------------------------------------------------------------------
    '���|�[�g�G���h
    '---------------------------------------------------------------------------------
    Private Sub H01R20S_Okurijou_ReportEnd(sender As Object, e As EventArgs) Handles MyBase.ReportEnd

    End Sub

    Private Sub Detail_Format(sender As Object, e As EventArgs) Handles Detail.Format
        'RowNumber = RowNumber + 1

        'If RowNumber < 6 Then
        '    ' ������6���ɖ����Ȃ��ꍇ�A���y�[�W�͍s�Ȃ��܂���B
        '    _parentreport.Detail.RepeatToFill = True
        '    _parentreport.Detail.NewPage = NewPage.None
        '    'Me.Detail.RepeatToFill = True
        '    'Me.Detail.NewPage = NewPage.None
        'Else
        '    ' 6���o�͂�����A���y�[�W���s���A�J�E���^�����Z�b�g���܂��B
        '    _parentreport.Detail.RepeatToFill = False
        '    _parentreport.Detail.NewPage = NewPage.After
        '    'Me.Detail.RepeatToFill = False
        '    'Me.Detail.NewPage = NewPage.After
        '    RowNumber = 0
        'End If

    End Sub

End Class
