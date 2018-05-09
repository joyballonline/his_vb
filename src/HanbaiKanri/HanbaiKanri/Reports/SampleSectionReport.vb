Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Document
Imports HanbaiKanri
Imports UtilMDL.DB

Public Class SampleSectionReport
    Implements RepSectionIF

    Private _db As UtilDBIf
    Private _ds As DataSet
    Private rowIdx As Integer = 0

    Private Const RS As String = "RecSet"                           'レコードセットテーブル

    '---------------------------------------------------------------------------------
    'レポート作表
    '---------------------------------------------------------------------------------
    Public Sub runReport() Implements RepSectionIF.runReport
        Dim v As New PreviewForm
        v.viewer.Document = Me.Document
        Me.Run()
        v.Show()
    End Sub

    '---------------------------------------------------------------------------------
    'データ受領
    '---------------------------------------------------------------------------------
    Public Sub setData(prmDs As DataSet, ByRef prmRefDbHd As UtilDBIf) Implements RepSectionIF.setData

        _ds = prmDs
        _db = prmRefDbHd
        rowIdx = 0

    End Sub

    '---------------------------------------------------------------------------------
    'レポートスタート
    '---------------------------------------------------------------------------------
    Private Sub SampleSectionReport_ReportStart(sender As Object, e As EventArgs) Handles Me.ReportStart

        '何かあれば
        Debug.Print("SampleSectionReport_ReportStart 通過")

    End Sub

    '---------------------------------------------------------------------------------
    'データイニシャライズ
    '---------------------------------------------------------------------------------
    Private Sub SampleSectionReport_DataInitialize(sender As Object, e As EventArgs) Handles Me.DataInitialize

        '何かあれば
        Debug.Print("SampleSectionReport_DataInitialize 通過")

    End Sub

    '---------------------------------------------------------------------------------
    'ページヘッダー初期化時
    '---------------------------------------------------------------------------------
    Private Sub PageHeader_Format(sender As Object, e As EventArgs) Handles PageHeader.Format

        txtPrm.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("prm"))

    End Sub


    '---------------------------------------------------------------------------------
    'グループヘッダー初期化時
    '---------------------------------------------------------------------------------
    Private Sub GroupHeader1_Format(sender As Object, e As EventArgs) Handles GroupHeader1.Format

        '何かあれば
        Debug.Print("GroupHeader1_Format 通過")

    End Sub

    '---------------------------------------------------------------------------------
    'フェッチデータ
    '---------------------------------------------------------------------------------
    Private Sub SampleSectionReport_FetchData(sender As Object, eArgs As FetchEventArgs) Handles Me.FetchData
        Try

            txtCol1.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("col1"))
            txtCol2.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("col2"))
            txtCol3.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("col3"))

            rowIdx = rowIdx + 1
            eArgs.EOF = False
        Catch ie As IndexOutOfRangeException
            eArgs.EOF = True 'データ末尾
        End Try

    End Sub

    '---------------------------------------------------------------------------------
    'グループフッダ―初期化時
    '---------------------------------------------------------------------------------
    Private Sub GroupFooter1_Format(sender As Object, e As EventArgs) Handles GroupFooter1.Format

        '何かあれば
        Debug.Print("GroupFooter1_Format 通過")

    End Sub

    '---------------------------------------------------------------------------------
    'ページフッダー初期化時
    '---------------------------------------------------------------------------------
    Private Sub PageFooter_Format(sender As Object, e As EventArgs) Handles PageFooter.Format

        '何かあれば
        Debug.Print("PageFooter_Format 通過")

    End Sub

    '---------------------------------------------------------------------------------
    'レポートエンド
    '---------------------------------------------------------------------------------
    Private Sub SampleSectionReport_ReportEnd(sender As Object, e As EventArgs) Handles Me.ReportEnd

        '何かあれば
        Debug.Print("SampleSectionReport_ReportEnd 通過")

    End Sub
End Class
