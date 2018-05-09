'===============================================================================
'
'　カネキ吉田商店様
'　　（システム名）販売管理
'　　（処理機能名）得意先元帳（サブレポート）
'    （フォームID）H10R04
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   狩野        2018/03/13                 新規              
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
    'レポートスタート
    '---------------------------------------------------------------------------------
    Private Sub H10R04S_TokuisakiMotoList_ReportStart(sender As Object, e As EventArgs) Handles MyBase.ReportStart
        RS = Me.DataMember
        _ds = Me.DataSource
        _db = H10R04_TokuisakiMotoList.parentDb
        rowIdx = 0
    End Sub

    Private Sub H10R04S_TokuisakiMotoList_FetchData(sender As Object, eArgs As FetchEventArgs) Handles MyBase.FetchData
        Try

            txt委託日.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("委託日"))
            txt伝票番号.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("伝票番号"))
            txt区分.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("区分"))
            txt出荷先名.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("出荷先"))
            txt商品.Text = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("商品名"))
            fld残数量.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("残数量"))
            fld仮単価.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("仮単価"))
            fld見込額.Value = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("見込額"))
            '明細の上線
            If rowIdx = 0 Then
                Line4.Visible = False
            ElseIf Not _denpyoBango.Equals(_db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("伝票番号"))) Then
                Line4.Visible = True
            Else
                Line4.Visible = False
            End If
            _denpyoBango = _db.rmNullStr(_ds.Tables(RS).Rows(rowIdx)("伝票番号"))

            rowIdx = rowIdx + 1

            eArgs.EOF = False
        Catch ie As IndexOutOfRangeException
            eArgs.EOF = True 'データ末尾
        End Try
    End Sub

    Private Sub H10R04S_TokuisakiMotoList_DataInitialize(sender As Object, e As EventArgs) Handles MyBase.DataInitialize

    End Sub

    Private Sub H10R04S_TokuisakiMotoList_ReportEnd(sender As Object, e As EventArgs) Handles MyBase.ReportEnd

    End Sub

    Private Sub Detail_Format(sender As Object, e As EventArgs) Handles Detail.Format

    End Sub
End Class
