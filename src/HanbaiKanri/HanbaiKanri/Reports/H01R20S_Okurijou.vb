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
    Private RowNumber As Integer = 0　　' 件数カウンタ

    '---------------------------------------------------------------------------------
    'レポートスタート
    '---------------------------------------------------------------------------------
    Private Sub H01R20S_Okurijou_ReportStart(sender As Object, e As EventArgs) Handles MyBase.ReportStart
        RS = Me.DataMember
        _ds = Me.DataSource
    End Sub

    '---------------------------------------------------------------------------------
    'データイニシャライズ
    '---------------------------------------------------------------------------------
    Private Sub H01R20S_Okurijou_DataInitialize(sender As Object, e As EventArgs) Handles MyBase.DataInitialize

    End Sub

    '---------------------------------------------------------------------------------
    'フェッチデータ
    '---------------------------------------------------------------------------------
    Private Sub H01R20S_Okurijou_FetchData(sender As Object, eArgs As FetchEventArgs) Handles MyBase.FetchData
        'Try
        '    txtGoodsNm.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("商品名"))
        '    txtIrisu.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("入数"))
        '    txtKosu.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("個数"))
        '    txtBiko.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("明細備考"))

        '    rowIdx = rowIdx + 1
        '    eArgs.EOF = False
        'Catch ie As IndexOutOfRangeException
        '    eArgs.EOF = True 'データ末尾
        'End Try

    End Sub

    '---------------------------------------------------------------------------------
    'レポートエンド
    '---------------------------------------------------------------------------------
    Private Sub H01R20S_Okurijou_ReportEnd(sender As Object, e As EventArgs) Handles MyBase.ReportEnd

    End Sub

    Private Sub Detail_Format(sender As Object, e As EventArgs) Handles Detail.Format
        'RowNumber = RowNumber + 1

        'If RowNumber < 6 Then
        '    ' 件数が6件に満たない場合、改ページは行ないません。
        '    _parentreport.Detail.RepeatToFill = True
        '    _parentreport.Detail.NewPage = NewPage.None
        '    'Me.Detail.RepeatToFill = True
        '    'Me.Detail.NewPage = NewPage.None
        'Else
        '    ' 6件出力した後、改ページを行い、カウンタをリセットします。
        '    _parentreport.Detail.RepeatToFill = False
        '    _parentreport.Detail.NewPage = NewPage.After
        '    'Me.Detail.RepeatToFill = False
        '    'Me.Detail.NewPage = NewPage.After
        '    RowNumber = 0
        'End If

    End Sub

End Class
