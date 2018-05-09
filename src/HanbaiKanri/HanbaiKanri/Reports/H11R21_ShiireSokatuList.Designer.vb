<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class H11R21_ShiireSokatuList
    Inherits GrapeCity.ActiveReports.SectionReport

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
        End If
        MyBase.Dispose(disposing)
    End Sub

    'メモ: 以下のプロシージャは ActiveReports デザイナーで必要です。
    'ActiveReports デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    Private WithEvents PageHeader As GrapeCity.ActiveReports.SectionReportModel.PageHeader
    Private WithEvents Detail As GrapeCity.ActiveReports.SectionReportModel.Detail
    Private WithEvents PageFooter As GrapeCity.ActiveReports.SectionReportModel.PageFooter
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(H11R21_ShiireSokatuList))
        Me.PageHeader = New GrapeCity.ActiveReports.SectionReportModel.PageHeader()
        Me.txtPrm = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.Label2 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt区分ヘッダ = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label1 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt仕入年度ヘッダ = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label6 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label7 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label12 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label41 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label43 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt作成日ヘッダ = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.ReportInfo1 = New GrapeCity.ActiveReports.SectionReportModel.ReportInfo()
        Me.Label5 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label9 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label10 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label11 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label13 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label14 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label15 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label16 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label17 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label18 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label19 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label8 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label20 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label21 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label22 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label23 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label24 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Line3 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.Line6 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.Line2 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.Line4 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.Line8 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.Line9 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.Line10 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.Line11 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.Line12 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.Line13 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.Line14 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.Line15 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.Line16 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.Line17 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.Line18 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.Line19 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.Line20 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.Line21 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.Line22 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.Detail = New GrapeCity.ActiveReports.SectionReportModel.Detail()
        Me.txt仕入先名 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt２月数量 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt仕入先コード = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt２月金額 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt３月数量 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt４月数量 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt５月数量 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt数量計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt３月金額 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt４月金額 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt５月金額 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt金額計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt２月平均単価 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt３月平均単価 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt４月平均単価 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt５月平均単価 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt平均単価計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.Line1 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.PageFooter = New GrapeCity.ActiveReports.SectionReportModel.PageFooter()
        Me.txt会社名 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Line7 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.ReportHeader1 = New GrapeCity.ActiveReports.SectionReportModel.ReportHeader()
        Me.ReportFooter1 = New GrapeCity.ActiveReports.SectionReportModel.ReportFooter()
        Me.Label35 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label3 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt２月数量総合計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt２月金額総合計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt３月数量総合計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt４月数量総合計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt５月数量総合計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt数量総合計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt３月金額総合計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt４月金額総合計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt５月金額総合計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt金額総合計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt２月平均単価総合計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt３月平均単価総合計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt４月平均単価総合計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt５月平均単価総合計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt平均単価総合計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.Line5 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.fld２月数量 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld３月数量 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld４月数量 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld５月数量 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld数量計 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld２月金額 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld３月金額 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld４月金額 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld５月金額 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld金額計 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld２月平均単価 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld３月平均単価 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld４月平均単価 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld５月平均単価 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld平均単価計 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld２月平均単価総合計 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld３月平均単価総合計 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld４月平均単価総合計 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld５月平均単価総合計 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld平均単価総合計 = New GrapeCity.ActiveReports.Data.Field()
        Me.Line23 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        CType(Me.txtPrm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt区分ヘッダ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt仕入年度ヘッダ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label41, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label43, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt作成日ヘッダ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReportInfo1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt仕入先名, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt２月数量, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt仕入先コード, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt２月金額, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt３月数量, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt４月数量, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt５月数量, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt数量計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt３月金額, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt４月金額, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt５月金額, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt金額計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt２月平均単価, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt３月平均単価, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt４月平均単価, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt５月平均単価, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt平均単価計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt会社名, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label35, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt２月数量総合計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt２月金額総合計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt３月数量総合計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt４月数量総合計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt５月数量総合計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt数量総合計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt３月金額総合計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt４月金額総合計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt５月金額総合計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt金額総合計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt２月平均単価総合計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt３月平均単価総合計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt４月平均単価総合計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt５月平均単価総合計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt平均単価総合計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.txtPrm, Me.Label2, Me.txt区分ヘッダ, Me.Label1, Me.txt仕入年度ヘッダ, Me.Label6, Me.Label7, Me.Label12, Me.Label41, Me.Label43, Me.txt作成日ヘッダ, Me.ReportInfo1, Me.Label5, Me.Label9, Me.Label10, Me.Label11, Me.Label13, Me.Label14, Me.Label15, Me.Label16, Me.Label17, Me.Label18, Me.Label19, Me.Label8, Me.Label20, Me.Label21, Me.Label22, Me.Label23, Me.Label24, Me.Line3, Me.Line6, Me.Line2, Me.Line4, Me.Line8, Me.Line9, Me.Line10, Me.Line11, Me.Line12, Me.Line13, Me.Line14, Me.Line15, Me.Line16, Me.Line17, Me.Line18, Me.Line19, Me.Line20, Me.Line21, Me.Line22})
        Me.PageHeader.Height = 0.6563334!
        Me.PageHeader.Name = "PageHeader"
        '
        'txtPrm
        '
        Me.txtPrm.Height = 0.252!
        Me.txtPrm.Left = 0.1!
        Me.txtPrm.Name = "txtPrm"
        Me.txtPrm.Style = "font-family: ＭＳ ゴシック; font-size: 15.75pt; font-style: italic; font-weight: bold"
        Me.txtPrm.Text = "仕入総括表"
        Me.txtPrm.Top = 0!
        Me.txtPrm.Width = 2.151!
        '
        'Label2
        '
        Me.Label2.Height = 0.16!
        Me.Label2.HyperLink = Nothing
        Me.Label2.Left = 2.251!
        Me.Label2.Name = "Label2"
        Me.Label2.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: top"
        Me.Label2.Text = "[区分]"
        Me.Label2.Top = 0!
        Me.Label2.Width = 0.6309997!
        '
        'txt区分ヘッダ
        '
        Me.txt区分ヘッダ.Height = 0.15!
        Me.txt区分ヘッダ.HyperLink = Nothing
        Me.txt区分ヘッダ.Left = 2.251!
        Me.txt区分ヘッダ.Name = "txt区分ヘッダ"
        Me.txt区分ヘッダ.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left"
        Me.txt区分ヘッダ.Text = "仕入"
        Me.txt区分ヘッダ.Top = 0.15!
        Me.txt区分ヘッダ.Width = 0.6309997!
        '
        'Label1
        '
        Me.Label1.Height = 0.16!
        Me.Label1.HyperLink = Nothing
        Me.Label1.Left = 2.882!
        Me.Label1.Name = "Label1"
        Me.Label1.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: top"
        Me.Label1.Text = "[仕入年度]"
        Me.Label1.Top = 0!
        Me.Label1.Width = 1.774!
        '
        'txt仕入年度ヘッダ
        '
        Me.txt仕入年度ヘッダ.Height = 0.15!
        Me.txt仕入年度ヘッダ.HyperLink = Nothing
        Me.txt仕入年度ヘッダ.Left = 2.882!
        Me.txt仕入年度ヘッダ.MultiLine = False
        Me.txt仕入年度ヘッダ.Name = "txt仕入年度ヘッダ"
        Me.txt仕入年度ヘッダ.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left"
        Me.txt仕入年度ヘッダ.Text = "2018年度"
        Me.txt仕入年度ヘッダ.Top = 0.15!
        Me.txt仕入年度ヘッダ.Width = 1.774!
        '
        'Label6
        '
        Me.Label6.Height = 0.15!
        Me.Label6.HyperLink = Nothing
        Me.Label6.Left = 0.037!
        Me.Label6.Name = "Label6"
        Me.Label6.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: top"
        Me.Label6.Text = "仕入先CD"
        Me.Label6.Top = 0.5019999!
        Me.Label6.Width = 0.5630001!
        '
        'Label7
        '
        Me.Label7.Height = 0.15!
        Me.Label7.HyperLink = Nothing
        Me.Label7.Left = 0.6!
        Me.Label7.Name = "Label7"
        Me.Label7.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: top"
        Me.Label7.Text = "仕入先名"
        Me.Label7.Top = 0.5019999!
        Me.Label7.Width = 1.651!
        '
        'Label12
        '
        Me.Label12.Height = 0.15!
        Me.Label12.HyperLink = Nothing
        Me.Label12.Left = 2.251!
        Me.Label12.Name = "Label12"
        Me.Label12.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: center; ve" &
    "rtical-align: top"
        Me.Label12.Text = "２月"
        Me.Label12.Top = 0.502!
        Me.Label12.Width = 0.437!
        '
        'Label41
        '
        Me.Label41.Height = 0.15!
        Me.Label41.HyperLink = Nothing
        Me.Label41.Left = 9.626!
        Me.Label41.Name = "Label41"
        Me.Label41.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label41.Text = "頁："
        Me.Label41.Top = 0.15!
        Me.Label41.Width = 0.3390007!
        '
        'Label43
        '
        Me.Label43.Height = 0.15!
        Me.Label43.HyperLink = Nothing
        Me.Label43.Left = 7.985!
        Me.Label43.Name = "Label43"
        Me.Label43.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label43.Text = "作成日："
        Me.Label43.Top = 0.15!
        Me.Label43.Width = 0.766!
        '
        'txt作成日ヘッダ
        '
        Me.txt作成日ヘッダ.Height = 0.15!
        Me.txt作成日ヘッダ.HyperLink = Nothing
        Me.txt作成日ヘッダ.Left = 8.751!
        Me.txt作成日ヘッダ.Name = "txt作成日ヘッダ"
        Me.txt作成日ヘッダ.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left"
        Me.txt作成日ヘッダ.Text = "2018/02/20"
        Me.txt作成日ヘッダ.Top = 0.15!
        Me.txt作成日ヘッダ.Width = 0.875!
        '
        'ReportInfo1
        '
        Me.ReportInfo1.CanGrow = False
        Me.ReportInfo1.FormatString = "{PageNumber} / {PageCount} "
        Me.ReportInfo1.Height = 0.15!
        Me.ReportInfo1.Left = 9.965!
        Me.ReportInfo1.MultiLine = False
        Me.ReportInfo1.Name = "ReportInfo1"
        Me.ReportInfo1.Style = "font-family: ＭＳ 明朝; font-size: 9pt"
        Me.ReportInfo1.Top = 0.15!
        Me.ReportInfo1.Width = 0.6289997!
        '
        'Label5
        '
        Me.Label5.Height = 0.15!
        Me.Label5.HyperLink = Nothing
        Me.Label5.Left = 2.251!
        Me.Label5.Name = "Label5"
        Me.Label5.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: center; ve" &
    "rtical-align: top"
        Me.Label5.Text = "数量"
        Me.Label5.Top = 0.32!
        Me.Label5.Width = 2.185!
        '
        'Label9
        '
        Me.Label9.Height = 0.15!
        Me.Label9.HyperLink = Nothing
        Me.Label9.Left = 2.688!
        Me.Label9.Name = "Label9"
        Me.Label9.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: center; ve" &
    "rtical-align: top"
        Me.Label9.Text = "３月"
        Me.Label9.Top = 0.502!
        Me.Label9.Width = 0.437!
        '
        'Label10
        '
        Me.Label10.Height = 0.15!
        Me.Label10.HyperLink = Nothing
        Me.Label10.Left = 3.125!
        Me.Label10.Name = "Label10"
        Me.Label10.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: center; ve" &
    "rtical-align: top"
        Me.Label10.Text = "４月"
        Me.Label10.Top = 0.502!
        Me.Label10.Width = 0.437!
        '
        'Label11
        '
        Me.Label11.Height = 0.15!
        Me.Label11.HyperLink = Nothing
        Me.Label11.Left = 3.562!
        Me.Label11.Name = "Label11"
        Me.Label11.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: center; ve" &
    "rtical-align: top"
        Me.Label11.Text = "５月"
        Me.Label11.Top = 0.502!
        Me.Label11.Width = 0.437!
        '
        'Label13
        '
        Me.Label13.Height = 0.15!
        Me.Label13.HyperLink = Nothing
        Me.Label13.Left = 3.999!
        Me.Label13.Name = "Label13"
        Me.Label13.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: center; ve" &
    "rtical-align: top"
        Me.Label13.Text = "計"
        Me.Label13.Top = 0.502!
        Me.Label13.Width = 0.437!
        '
        'Label14
        '
        Me.Label14.Height = 0.15!
        Me.Label14.HyperLink = Nothing
        Me.Label14.Left = 4.436!
        Me.Label14.Name = "Label14"
        Me.Label14.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: center; ve" &
    "rtical-align: top"
        Me.Label14.Text = "２月"
        Me.Label14.Top = 0.502!
        Me.Label14.Width = 0.737!
        '
        'Label15
        '
        Me.Label15.Height = 0.15!
        Me.Label15.HyperLink = Nothing
        Me.Label15.Left = 4.436!
        Me.Label15.Name = "Label15"
        Me.Label15.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: center; ve" &
    "rtical-align: top"
        Me.Label15.Text = "金額"
        Me.Label15.Top = 0.32!
        Me.Label15.Width = 3.685!
        '
        'Label16
        '
        Me.Label16.Height = 0.15!
        Me.Label16.HyperLink = Nothing
        Me.Label16.Left = 5.173!
        Me.Label16.Name = "Label16"
        Me.Label16.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: center; ve" &
    "rtical-align: top"
        Me.Label16.Text = "３月"
        Me.Label16.Top = 0.502!
        Me.Label16.Width = 0.737!
        '
        'Label17
        '
        Me.Label17.Height = 0.15!
        Me.Label17.HyperLink = Nothing
        Me.Label17.Left = 5.91!
        Me.Label17.Name = "Label17"
        Me.Label17.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: center; ve" &
    "rtical-align: top"
        Me.Label17.Text = "４月"
        Me.Label17.Top = 0.502!
        Me.Label17.Width = 0.737!
        '
        'Label18
        '
        Me.Label18.Height = 0.15!
        Me.Label18.HyperLink = Nothing
        Me.Label18.Left = 6.647!
        Me.Label18.Name = "Label18"
        Me.Label18.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: center; ve" &
    "rtical-align: top"
        Me.Label18.Text = "５月"
        Me.Label18.Top = 0.502!
        Me.Label18.Width = 0.737!
        '
        'Label19
        '
        Me.Label19.Height = 0.15!
        Me.Label19.HyperLink = Nothing
        Me.Label19.Left = 7.384001!
        Me.Label19.Name = "Label19"
        Me.Label19.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: center; ve" &
    "rtical-align: top"
        Me.Label19.Text = "計"
        Me.Label19.Top = 0.502!
        Me.Label19.Width = 0.737!
        '
        'Label8
        '
        Me.Label8.Height = 0.15!
        Me.Label8.HyperLink = Nothing
        Me.Label8.Left = 8.126!
        Me.Label8.Name = "Label8"
        Me.Label8.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: center; ve" &
    "rtical-align: top"
        Me.Label8.Text = "２月"
        Me.Label8.Top = 0.502!
        Me.Label8.Width = 0.5!
        '
        'Label20
        '
        Me.Label20.Height = 0.15!
        Me.Label20.HyperLink = Nothing
        Me.Label20.Left = 8.126!
        Me.Label20.Name = "Label20"
        Me.Label20.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: center; ve" &
    "rtical-align: top"
        Me.Label20.Text = "平均単価"
        Me.Label20.Top = 0.32!
        Me.Label20.Width = 2.505!
        '
        'Label21
        '
        Me.Label21.Height = 0.15!
        Me.Label21.HyperLink = Nothing
        Me.Label21.Left = 8.626!
        Me.Label21.Name = "Label21"
        Me.Label21.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: center; ve" &
    "rtical-align: top"
        Me.Label21.Text = "３月"
        Me.Label21.Top = 0.502!
        Me.Label21.Width = 0.5!
        '
        'Label22
        '
        Me.Label22.Height = 0.15!
        Me.Label22.HyperLink = Nothing
        Me.Label22.Left = 9.131001!
        Me.Label22.Name = "Label22"
        Me.Label22.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: center; ve" &
    "rtical-align: top"
        Me.Label22.Text = "４月"
        Me.Label22.Top = 0.502!
        Me.Label22.Width = 0.5!
        '
        'Label23
        '
        Me.Label23.Height = 0.15!
        Me.Label23.HyperLink = Nothing
        Me.Label23.Left = 9.631001!
        Me.Label23.Name = "Label23"
        Me.Label23.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: center; ve" &
    "rtical-align: top"
        Me.Label23.Text = "５月"
        Me.Label23.Top = 0.502!
        Me.Label23.Width = 0.5!
        '
        'Label24
        '
        Me.Label24.Height = 0.15!
        Me.Label24.HyperLink = Nothing
        Me.Label24.Left = 10.131!
        Me.Label24.Name = "Label24"
        Me.Label24.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: center; ve" &
    "rtical-align: top"
        Me.Label24.Text = "計"
        Me.Label24.Top = 0.502!
        Me.Label24.Width = 0.559001!
        '
        'Line3
        '
        Me.Line3.Height = 0!
        Me.Line3.Left = 0!
        Me.Line3.LineWeight = 2.0!
        Me.Line3.Name = "Line3"
        Me.Line3.Top = 0.32!
        Me.Line3.Width = 10.69!
        Me.Line3.X1 = 0!
        Me.Line3.X2 = 10.69!
        Me.Line3.Y1 = 0.32!
        Me.Line3.Y2 = 0.32!
        '
        'Line6
        '
        Me.Line6.Height = 0!
        Me.Line6.Left = 0!
        Me.Line6.LineWeight = 2.0!
        Me.Line6.Name = "Line6"
        Me.Line6.Top = 0.6524583!
        Me.Line6.Width = 10.69!
        Me.Line6.X1 = 0!
        Me.Line6.X2 = 10.69!
        Me.Line6.Y1 = 0.6524583!
        Me.Line6.Y2 = 0.6524583!
        '
        'Line2
        '
        Me.Line2.Height = 0.317!
        Me.Line2.Left = 2.241!
        Me.Line2.LineWeight = 2.0!
        Me.Line2.Name = "Line2"
        Me.Line2.Top = 0.335!
        Me.Line2.Width = 0!
        Me.Line2.X1 = 2.241!
        Me.Line2.X2 = 2.241!
        Me.Line2.Y1 = 0.335!
        Me.Line2.Y2 = 0.652!
        '
        'Line4
        '
        Me.Line4.Height = 0.317!
        Me.Line4.Left = 4.436!
        Me.Line4.LineWeight = 2.0!
        Me.Line4.Name = "Line4"
        Me.Line4.Top = 0.335!
        Me.Line4.Width = 0!
        Me.Line4.X1 = 4.436!
        Me.Line4.X2 = 4.436!
        Me.Line4.Y1 = 0.335!
        Me.Line4.Y2 = 0.652!
        '
        'Line8
        '
        Me.Line8.Height = 0!
        Me.Line8.Left = 2.251!
        Me.Line8.LineWeight = 2.0!
        Me.Line8.Name = "Line8"
        Me.Line8.Top = 0.48!
        Me.Line8.Width = 8.438999!
        Me.Line8.X1 = 2.251!
        Me.Line8.X2 = 10.69!
        Me.Line8.Y1 = 0.48!
        Me.Line8.Y2 = 0.48!
        '
        'Line9
        '
        Me.Line9.Height = 0.317!
        Me.Line9.Left = 8.121!
        Me.Line9.LineWeight = 2.0!
        Me.Line9.Name = "Line9"
        Me.Line9.Top = 0.335!
        Me.Line9.Width = 0!
        Me.Line9.X1 = 8.121!
        Me.Line9.X2 = 8.121!
        Me.Line9.Y1 = 0.335!
        Me.Line9.Y2 = 0.652!
        '
        'Line10
        '
        Me.Line10.Height = 0.317!
        Me.Line10.Left = 10.69!
        Me.Line10.LineWeight = 2.0!
        Me.Line10.Name = "Line10"
        Me.Line10.Top = 0.335!
        Me.Line10.Width = 0!
        Me.Line10.X1 = 10.69!
        Me.Line10.X2 = 10.69!
        Me.Line10.Y1 = 0.335!
        Me.Line10.Y2 = 0.652!
        '
        'Line11
        '
        Me.Line11.Height = 0.172!
        Me.Line11.Left = 2.688!
        Me.Line11.LineWeight = 2.0!
        Me.Line11.Name = "Line11"
        Me.Line11.Top = 0.48!
        Me.Line11.Width = 0!
        Me.Line11.X1 = 2.688!
        Me.Line11.X2 = 2.688!
        Me.Line11.Y1 = 0.48!
        Me.Line11.Y2 = 0.652!
        '
        'Line12
        '
        Me.Line12.Height = 0.172!
        Me.Line12.Left = 3.125!
        Me.Line12.LineWeight = 2.0!
        Me.Line12.Name = "Line12"
        Me.Line12.Top = 0.48!
        Me.Line12.Width = 0!
        Me.Line12.X1 = 3.125!
        Me.Line12.X2 = 3.125!
        Me.Line12.Y1 = 0.48!
        Me.Line12.Y2 = 0.652!
        '
        'Line13
        '
        Me.Line13.Height = 0.172!
        Me.Line13.Left = 3.562!
        Me.Line13.LineWeight = 2.0!
        Me.Line13.Name = "Line13"
        Me.Line13.Top = 0.48!
        Me.Line13.Width = 0!
        Me.Line13.X1 = 3.562!
        Me.Line13.X2 = 3.562!
        Me.Line13.Y1 = 0.48!
        Me.Line13.Y2 = 0.652!
        '
        'Line14
        '
        Me.Line14.Height = 0.172!
        Me.Line14.Left = 3.999!
        Me.Line14.LineWeight = 2.0!
        Me.Line14.Name = "Line14"
        Me.Line14.Top = 0.48!
        Me.Line14.Width = 0!
        Me.Line14.X1 = 3.999!
        Me.Line14.X2 = 3.999!
        Me.Line14.Y1 = 0.48!
        Me.Line14.Y2 = 0.652!
        '
        'Line15
        '
        Me.Line15.Height = 0.172!
        Me.Line15.Left = 5.173!
        Me.Line15.LineWeight = 2.0!
        Me.Line15.Name = "Line15"
        Me.Line15.Top = 0.48!
        Me.Line15.Width = 0!
        Me.Line15.X1 = 5.173!
        Me.Line15.X2 = 5.173!
        Me.Line15.Y1 = 0.48!
        Me.Line15.Y2 = 0.652!
        '
        'Line16
        '
        Me.Line16.Height = 0.172!
        Me.Line16.Left = 5.91!
        Me.Line16.LineWeight = 2.0!
        Me.Line16.Name = "Line16"
        Me.Line16.Top = 0.48!
        Me.Line16.Width = 0!
        Me.Line16.X1 = 5.91!
        Me.Line16.X2 = 5.91!
        Me.Line16.Y1 = 0.48!
        Me.Line16.Y2 = 0.652!
        '
        'Line17
        '
        Me.Line17.Height = 0.172!
        Me.Line17.Left = 6.647!
        Me.Line17.LineWeight = 2.0!
        Me.Line17.Name = "Line17"
        Me.Line17.Top = 0.48!
        Me.Line17.Width = 0!
        Me.Line17.X1 = 6.647!
        Me.Line17.X2 = 6.647!
        Me.Line17.Y1 = 0.48!
        Me.Line17.Y2 = 0.652!
        '
        'Line18
        '
        Me.Line18.Height = 0.172!
        Me.Line18.Left = 7.384!
        Me.Line18.LineWeight = 2.0!
        Me.Line18.Name = "Line18"
        Me.Line18.Top = 0.48!
        Me.Line18.Width = 0!
        Me.Line18.X1 = 7.384!
        Me.Line18.X2 = 7.384!
        Me.Line18.Y1 = 0.48!
        Me.Line18.Y2 = 0.652!
        '
        'Line19
        '
        Me.Line19.Height = 0.172!
        Me.Line19.Left = 8.626!
        Me.Line19.LineWeight = 2.0!
        Me.Line19.Name = "Line19"
        Me.Line19.Top = 0.48!
        Me.Line19.Width = 0!
        Me.Line19.X1 = 8.626!
        Me.Line19.X2 = 8.626!
        Me.Line19.Y1 = 0.48!
        Me.Line19.Y2 = 0.652!
        '
        'Line20
        '
        Me.Line20.Height = 0.172!
        Me.Line20.Left = 9.131001!
        Me.Line20.LineWeight = 2.0!
        Me.Line20.Name = "Line20"
        Me.Line20.Top = 0.48!
        Me.Line20.Width = 0!
        Me.Line20.X1 = 9.131001!
        Me.Line20.X2 = 9.131001!
        Me.Line20.Y1 = 0.48!
        Me.Line20.Y2 = 0.652!
        '
        'Line21
        '
        Me.Line21.Height = 0.172!
        Me.Line21.Left = 9.631001!
        Me.Line21.LineWeight = 2.0!
        Me.Line21.Name = "Line21"
        Me.Line21.Top = 0.48!
        Me.Line21.Width = 0!
        Me.Line21.X1 = 9.631001!
        Me.Line21.X2 = 9.631001!
        Me.Line21.Y1 = 0.48!
        Me.Line21.Y2 = 0.652!
        '
        'Line22
        '
        Me.Line22.Height = 0.172!
        Me.Line22.Left = 10.131!
        Me.Line22.LineWeight = 2.0!
        Me.Line22.Name = "Line22"
        Me.Line22.Top = 0.48!
        Me.Line22.Width = 0!
        Me.Line22.X1 = 10.131!
        Me.Line22.X2 = 10.131!
        Me.Line22.Y1 = 0.48!
        Me.Line22.Y2 = 0.652!
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.txt仕入先名, Me.txt２月数量, Me.txt仕入先コード, Me.txt２月金額, Me.txt３月数量, Me.txt４月数量, Me.txt５月数量, Me.txt数量計, Me.txt３月金額, Me.txt４月金額, Me.txt５月金額, Me.txt金額計, Me.txt２月平均単価, Me.txt３月平均単価, Me.txt４月平均単価, Me.txt５月平均単価, Me.txt平均単価計, Me.Line1})
        Me.Detail.Height = 0.16!
        Me.Detail.Name = "Detail"
        '
        'txt仕入先名
        '
        Me.txt仕入先名.Height = 0.16!
        Me.txt仕入先名.HyperLink = Nothing
        Me.txt仕入先名.Left = 0.6!
        Me.txt仕入先名.MultiLine = False
        Me.txt仕入先名.Name = "txt仕入先名"
        Me.txt仕入先名.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left; vertical-align: middle"
        Me.txt仕入先名.Text = "（株）○○作商店□□"
        Me.txt仕入先名.Top = 0!
        Me.txt仕入先名.Width = 1.651!
        '
        'txt２月数量
        '
        Me.txt２月数量.DataField = "fld２月数量"
        Me.txt２月数量.Height = 0.16!
        Me.txt２月数量.Left = 2.251!
        Me.txt２月数量.MultiLine = False
        Me.txt２月数量.Name = "txt２月数量"
        Me.txt２月数量.OutputFormat = resources.GetString("txt２月数量.OutputFormat")
        Me.txt２月数量.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt２月数量.Text = "12,345"
        Me.txt２月数量.Top = 0!
        Me.txt２月数量.Width = 0.437!
        '
        'txt仕入先コード
        '
        Me.txt仕入先コード.Height = 0.16!
        Me.txt仕入先コード.HyperLink = Nothing
        Me.txt仕入先コード.Left = 0.037!
        Me.txt仕入先コード.MultiLine = False
        Me.txt仕入先コード.Name = "txt仕入先コード"
        Me.txt仕入先コード.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left; vertical-align: middle"
        Me.txt仕入先コード.Text = "R000011"
        Me.txt仕入先コード.Top = 0!
        Me.txt仕入先コード.Width = 0.5630001!
        '
        'txt２月金額
        '
        Me.txt２月金額.DataField = "fld２月金額"
        Me.txt２月金額.Height = 0.16!
        Me.txt２月金額.Left = 4.436!
        Me.txt２月金額.Name = "txt２月金額"
        Me.txt２月金額.OutputFormat = resources.GetString("txt２月金額.OutputFormat")
        Me.txt２月金額.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt２月金額.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.All
        Me.txt２月金額.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.GrandTotal
        Me.txt２月金額.Text = "123,456,789"
        Me.txt２月金額.Top = 0!
        Me.txt２月金額.Width = 0.737!
        '
        'txt３月数量
        '
        Me.txt３月数量.DataField = "fld３月数量"
        Me.txt３月数量.Height = 0.16!
        Me.txt３月数量.Left = 2.688!
        Me.txt３月数量.MultiLine = False
        Me.txt３月数量.Name = "txt３月数量"
        Me.txt３月数量.OutputFormat = resources.GetString("txt３月数量.OutputFormat")
        Me.txt３月数量.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt３月数量.Text = "12,345"
        Me.txt３月数量.Top = 0!
        Me.txt３月数量.Width = 0.437!
        '
        'txt４月数量
        '
        Me.txt４月数量.DataField = "fld４月数量"
        Me.txt４月数量.Height = 0.16!
        Me.txt４月数量.Left = 3.125!
        Me.txt４月数量.MultiLine = False
        Me.txt４月数量.Name = "txt４月数量"
        Me.txt４月数量.OutputFormat = resources.GetString("txt４月数量.OutputFormat")
        Me.txt４月数量.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt４月数量.Text = "12,345"
        Me.txt４月数量.Top = 0!
        Me.txt４月数量.Width = 0.437!
        '
        'txt５月数量
        '
        Me.txt５月数量.DataField = "fld５月数量"
        Me.txt５月数量.Height = 0.16!
        Me.txt５月数量.Left = 3.562!
        Me.txt５月数量.MultiLine = False
        Me.txt５月数量.Name = "txt５月数量"
        Me.txt５月数量.OutputFormat = resources.GetString("txt５月数量.OutputFormat")
        Me.txt５月数量.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt５月数量.Text = "12,345"
        Me.txt５月数量.Top = 0!
        Me.txt５月数量.Width = 0.437!
        '
        'txt数量計
        '
        Me.txt数量計.DataField = "fld数量計"
        Me.txt数量計.Height = 0.16!
        Me.txt数量計.Left = 3.999!
        Me.txt数量計.MultiLine = False
        Me.txt数量計.Name = "txt数量計"
        Me.txt数量計.OutputFormat = resources.GetString("txt数量計.OutputFormat")
        Me.txt数量計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt数量計.Text = "12,345"
        Me.txt数量計.Top = 0!
        Me.txt数量計.Width = 0.437!
        '
        'txt３月金額
        '
        Me.txt３月金額.DataField = "fld３月金額"
        Me.txt３月金額.Height = 0.16!
        Me.txt３月金額.Left = 5.173!
        Me.txt３月金額.Name = "txt３月金額"
        Me.txt３月金額.OutputFormat = resources.GetString("txt３月金額.OutputFormat")
        Me.txt３月金額.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt３月金額.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.All
        Me.txt３月金額.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.GrandTotal
        Me.txt３月金額.Text = "123,456,789"
        Me.txt３月金額.Top = 0!
        Me.txt３月金額.Width = 0.737!
        '
        'txt４月金額
        '
        Me.txt４月金額.DataField = "fld４月金額"
        Me.txt４月金額.Height = 0.16!
        Me.txt４月金額.Left = 5.91!
        Me.txt４月金額.Name = "txt４月金額"
        Me.txt４月金額.OutputFormat = resources.GetString("txt４月金額.OutputFormat")
        Me.txt４月金額.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt４月金額.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.All
        Me.txt４月金額.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.GrandTotal
        Me.txt４月金額.Text = "123,456,789"
        Me.txt４月金額.Top = 0!
        Me.txt４月金額.Width = 0.737!
        '
        'txt５月金額
        '
        Me.txt５月金額.DataField = "fld５月金額"
        Me.txt５月金額.Height = 0.16!
        Me.txt５月金額.Left = 6.647!
        Me.txt５月金額.Name = "txt５月金額"
        Me.txt５月金額.OutputFormat = resources.GetString("txt５月金額.OutputFormat")
        Me.txt５月金額.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt５月金額.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.All
        Me.txt５月金額.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.GrandTotal
        Me.txt５月金額.Text = "123,456,789"
        Me.txt５月金額.Top = 0!
        Me.txt５月金額.Width = 0.737!
        '
        'txt金額計
        '
        Me.txt金額計.DataField = "fld金額計"
        Me.txt金額計.Height = 0.16!
        Me.txt金額計.Left = 7.384!
        Me.txt金額計.Name = "txt金額計"
        Me.txt金額計.OutputFormat = resources.GetString("txt金額計.OutputFormat")
        Me.txt金額計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt金額計.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.All
        Me.txt金額計.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.GrandTotal
        Me.txt金額計.Text = "123,456,789"
        Me.txt金額計.Top = 0!
        Me.txt金額計.Width = 0.737!
        '
        'txt２月平均単価
        '
        Me.txt２月平均単価.DataField = "fld２月平均単価"
        Me.txt２月平均単価.Height = 0.16!
        Me.txt２月平均単価.Left = 8.121!
        Me.txt２月平均単価.MultiLine = False
        Me.txt２月平均単価.Name = "txt２月平均単価"
        Me.txt２月平均単価.OutputFormat = resources.GetString("txt２月平均単価.OutputFormat")
        Me.txt２月平均単価.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt２月平均単価.Text = "1,234.5"
        Me.txt２月平均単価.Top = 0!
        Me.txt２月平均単価.Width = 0.5!
        '
        'txt３月平均単価
        '
        Me.txt３月平均単価.DataField = "fld３月平均単価"
        Me.txt３月平均単価.Height = 0.16!
        Me.txt３月平均単価.Left = 8.626!
        Me.txt３月平均単価.MultiLine = False
        Me.txt３月平均単価.Name = "txt３月平均単価"
        Me.txt３月平均単価.OutputFormat = resources.GetString("txt３月平均単価.OutputFormat")
        Me.txt３月平均単価.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt３月平均単価.Text = "1,234.5"
        Me.txt３月平均単価.Top = 0!
        Me.txt３月平均単価.Width = 0.5!
        '
        'txt４月平均単価
        '
        Me.txt４月平均単価.DataField = "fld４月平均単価"
        Me.txt４月平均単価.Height = 0.16!
        Me.txt４月平均単価.Left = 9.126!
        Me.txt４月平均単価.MultiLine = False
        Me.txt４月平均単価.Name = "txt４月平均単価"
        Me.txt４月平均単価.OutputFormat = resources.GetString("txt４月平均単価.OutputFormat")
        Me.txt４月平均単価.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt４月平均単価.Text = "1,234.5"
        Me.txt４月平均単価.Top = 0!
        Me.txt４月平均単価.Width = 0.5!
        '
        'txt５月平均単価
        '
        Me.txt５月平均単価.DataField = "fld５月平均単価"
        Me.txt５月平均単価.Height = 0.16!
        Me.txt５月平均単価.Left = 9.626!
        Me.txt５月平均単価.MultiLine = False
        Me.txt５月平均単価.Name = "txt５月平均単価"
        Me.txt５月平均単価.OutputFormat = resources.GetString("txt５月平均単価.OutputFormat")
        Me.txt５月平均単価.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt５月平均単価.Text = "1,234.5"
        Me.txt５月平均単価.Top = 0!
        Me.txt５月平均単価.Width = 0.5!
        '
        'txt平均単価計
        '
        Me.txt平均単価計.DataField = "fld平均単価計"
        Me.txt平均単価計.Height = 0.16!
        Me.txt平均単価計.Left = 10.126!
        Me.txt平均単価計.MultiLine = False
        Me.txt平均単価計.Name = "txt平均単価計"
        Me.txt平均単価計.OutputFormat = resources.GetString("txt平均単価計.OutputFormat")
        Me.txt平均単価計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt平均単価計.Text = "1,234.5"
        Me.txt平均単価計.Top = 0!
        Me.txt平均単価計.Width = 0.559001!
        '
        'Line1
        '
        Me.Line1.Height = 0!
        Me.Line1.Left = 0!
        Me.Line1.LineStyle = GrapeCity.ActiveReports.SectionReportModel.LineStyle.Dot
        Me.Line1.LineWeight = 1.0!
        Me.Line1.Name = "Line1"
        Me.Line1.Top = 0.16!
        Me.Line1.Width = 10.69!
        Me.Line1.X1 = 0!
        Me.Line1.X2 = 10.69!
        Me.Line1.Y1 = 0.16!
        Me.Line1.Y2 = 0.16!
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.txt会社名, Me.Line7})
        Me.PageFooter.Height = 0.16!
        Me.PageFooter.Name = "PageFooter"
        '
        'txt会社名
        '
        Me.txt会社名.Height = 0.16!
        Me.txt会社名.HyperLink = Nothing
        Me.txt会社名.Left = 8.412001!
        Me.txt会社名.Name = "txt会社名"
        Me.txt会社名.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-style: italic; font-weight: normal; te" &
    "xt-align: right; vertical-align: middle"
        Me.txt会社名.Text = "株式会社　カネキ吉田商店"
        Me.txt会社名.Top = 0!
        Me.txt会社名.Width = 2.181501!
        '
        'Line7
        '
        Me.Line7.Height = 0!
        Me.Line7.Left = 0!
        Me.Line7.LineWeight = 2.0!
        Me.Line7.Name = "Line7"
        Me.Line7.Top = 0!
        Me.Line7.Width = 10.69!
        Me.Line7.X1 = 0!
        Me.Line7.X2 = 10.69!
        Me.Line7.Y1 = 0!
        Me.Line7.Y2 = 0!
        '
        'ReportHeader1
        '
        Me.ReportHeader1.Height = 0!
        Me.ReportHeader1.Name = "ReportHeader1"
        Me.ReportHeader1.Visible = False
        '
        'ReportFooter1
        '
        Me.ReportFooter1.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.Label35, Me.Label3, Me.txt２月数量総合計, Me.txt２月金額総合計, Me.txt３月数量総合計, Me.txt４月数量総合計, Me.txt５月数量総合計, Me.txt数量総合計, Me.txt３月金額総合計, Me.txt４月金額総合計, Me.txt５月金額総合計, Me.txt金額総合計, Me.txt２月平均単価総合計, Me.txt３月平均単価総合計, Me.txt４月平均単価総合計, Me.txt５月平均単価総合計, Me.txt平均単価総合計, Me.Line5, Me.Line23})
        Me.ReportFooter1.Height = 0.16!
        Me.ReportFooter1.Name = "ReportFooter1"
        '
        'Label35
        '
        Me.Label35.Height = 0.16!
        Me.Label35.HyperLink = Nothing
        Me.Label35.Left = 0.6!
        Me.Label35.Name = "Label35"
        Me.Label35.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: middle"
        Me.Label35.Text = "総合計"
        Me.Label35.Top = 0!
        Me.Label35.Width = 0.9999993!
        '
        'Label3
        '
        Me.Label3.Height = 0.16!
        Me.Label3.HyperLink = Nothing
        Me.Label3.Left = 0.037!
        Me.Label3.Name = "Label3"
        Me.Label3.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: middle"
        Me.Label3.Text = "★★"
        Me.Label3.Top = 0!
        Me.Label3.Width = 0.5630001!
        '
        'txt２月数量総合計
        '
        Me.txt２月数量総合計.DataField = "fld２月数量"
        Me.txt２月数量総合計.Height = 0.16!
        Me.txt２月数量総合計.Left = 2.251!
        Me.txt２月数量総合計.MultiLine = False
        Me.txt２月数量総合計.Name = "txt２月数量総合計"
        Me.txt２月数量総合計.OutputFormat = resources.GetString("txt２月数量総合計.OutputFormat")
        Me.txt２月数量総合計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt２月数量総合計.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.All
        Me.txt２月数量総合計.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.GrandTotal
        Me.txt２月数量総合計.Text = "12,345"
        Me.txt２月数量総合計.Top = 0!
        Me.txt２月数量総合計.Width = 0.437!
        '
        'txt２月金額総合計
        '
        Me.txt２月金額総合計.DataField = "fld２月金額"
        Me.txt２月金額総合計.Height = 0.16!
        Me.txt２月金額総合計.Left = 4.436!
        Me.txt２月金額総合計.Name = "txt２月金額総合計"
        Me.txt２月金額総合計.OutputFormat = resources.GetString("txt２月金額総合計.OutputFormat")
        Me.txt２月金額総合計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt２月金額総合計.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.All
        Me.txt２月金額総合計.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.GrandTotal
        Me.txt２月金額総合計.Text = "123,456,789"
        Me.txt２月金額総合計.Top = 0!
        Me.txt２月金額総合計.Width = 0.737!
        '
        'txt３月数量総合計
        '
        Me.txt３月数量総合計.DataField = "fld３月数量"
        Me.txt３月数量総合計.Height = 0.16!
        Me.txt３月数量総合計.Left = 2.688!
        Me.txt３月数量総合計.MultiLine = False
        Me.txt３月数量総合計.Name = "txt３月数量総合計"
        Me.txt３月数量総合計.OutputFormat = resources.GetString("txt３月数量総合計.OutputFormat")
        Me.txt３月数量総合計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt３月数量総合計.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.All
        Me.txt３月数量総合計.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.GrandTotal
        Me.txt３月数量総合計.Text = "12,345"
        Me.txt３月数量総合計.Top = 0!
        Me.txt３月数量総合計.Width = 0.437!
        '
        'txt４月数量総合計
        '
        Me.txt４月数量総合計.DataField = "fld４月数量"
        Me.txt４月数量総合計.Height = 0.16!
        Me.txt４月数量総合計.Left = 3.125!
        Me.txt４月数量総合計.MultiLine = False
        Me.txt４月数量総合計.Name = "txt４月数量総合計"
        Me.txt４月数量総合計.OutputFormat = resources.GetString("txt４月数量総合計.OutputFormat")
        Me.txt４月数量総合計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt４月数量総合計.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.All
        Me.txt４月数量総合計.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.GrandTotal
        Me.txt４月数量総合計.Text = "12,345"
        Me.txt４月数量総合計.Top = 0!
        Me.txt４月数量総合計.Width = 0.437!
        '
        'txt５月数量総合計
        '
        Me.txt５月数量総合計.DataField = "fld５月数量"
        Me.txt５月数量総合計.Height = 0.16!
        Me.txt５月数量総合計.Left = 3.562!
        Me.txt５月数量総合計.MultiLine = False
        Me.txt５月数量総合計.Name = "txt５月数量総合計"
        Me.txt５月数量総合計.OutputFormat = resources.GetString("txt５月数量総合計.OutputFormat")
        Me.txt５月数量総合計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt５月数量総合計.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.All
        Me.txt５月数量総合計.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.GrandTotal
        Me.txt５月数量総合計.Text = "12,345"
        Me.txt５月数量総合計.Top = 0!
        Me.txt５月数量総合計.Width = 0.437!
        '
        'txt数量総合計
        '
        Me.txt数量総合計.DataField = "fld数量計"
        Me.txt数量総合計.Height = 0.16!
        Me.txt数量総合計.Left = 3.999!
        Me.txt数量総合計.MultiLine = False
        Me.txt数量総合計.Name = "txt数量総合計"
        Me.txt数量総合計.OutputFormat = resources.GetString("txt数量総合計.OutputFormat")
        Me.txt数量総合計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt数量総合計.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.All
        Me.txt数量総合計.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.GrandTotal
        Me.txt数量総合計.Text = "12,345"
        Me.txt数量総合計.Top = 0!
        Me.txt数量総合計.Width = 0.437!
        '
        'txt３月金額総合計
        '
        Me.txt３月金額総合計.DataField = "fld３月金額"
        Me.txt３月金額総合計.Height = 0.16!
        Me.txt３月金額総合計.Left = 5.173!
        Me.txt３月金額総合計.Name = "txt３月金額総合計"
        Me.txt３月金額総合計.OutputFormat = resources.GetString("txt３月金額総合計.OutputFormat")
        Me.txt３月金額総合計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt３月金額総合計.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.All
        Me.txt３月金額総合計.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.GrandTotal
        Me.txt３月金額総合計.Text = "123,456,789"
        Me.txt３月金額総合計.Top = 0!
        Me.txt３月金額総合計.Width = 0.737!
        '
        'txt４月金額総合計
        '
        Me.txt４月金額総合計.DataField = "fld４月金額"
        Me.txt４月金額総合計.Height = 0.16!
        Me.txt４月金額総合計.Left = 5.91!
        Me.txt４月金額総合計.Name = "txt４月金額総合計"
        Me.txt４月金額総合計.OutputFormat = resources.GetString("txt４月金額総合計.OutputFormat")
        Me.txt４月金額総合計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt４月金額総合計.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.All
        Me.txt４月金額総合計.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.GrandTotal
        Me.txt４月金額総合計.Text = "123,456,789"
        Me.txt４月金額総合計.Top = 0!
        Me.txt４月金額総合計.Width = 0.737!
        '
        'txt５月金額総合計
        '
        Me.txt５月金額総合計.DataField = "fld５月金額"
        Me.txt５月金額総合計.Height = 0.16!
        Me.txt５月金額総合計.Left = 6.647!
        Me.txt５月金額総合計.Name = "txt５月金額総合計"
        Me.txt５月金額総合計.OutputFormat = resources.GetString("txt５月金額総合計.OutputFormat")
        Me.txt５月金額総合計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt５月金額総合計.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.All
        Me.txt５月金額総合計.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.GrandTotal
        Me.txt５月金額総合計.Text = "123,456,789"
        Me.txt５月金額総合計.Top = 0!
        Me.txt５月金額総合計.Width = 0.737!
        '
        'txt金額総合計
        '
        Me.txt金額総合計.DataField = "fld金額計"
        Me.txt金額総合計.Height = 0.16!
        Me.txt金額総合計.Left = 7.384!
        Me.txt金額総合計.Name = "txt金額総合計"
        Me.txt金額総合計.OutputFormat = resources.GetString("txt金額総合計.OutputFormat")
        Me.txt金額総合計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt金額総合計.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.All
        Me.txt金額総合計.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.GrandTotal
        Me.txt金額総合計.Text = "123,456,789"
        Me.txt金額総合計.Top = 0!
        Me.txt金額総合計.Width = 0.737!
        '
        'txt２月平均単価総合計
        '
        Me.txt２月平均単価総合計.DataField = "fld２月平均単価総合計"
        Me.txt２月平均単価総合計.Height = 0.16!
        Me.txt２月平均単価総合計.Left = 8.121!
        Me.txt２月平均単価総合計.MultiLine = False
        Me.txt２月平均単価総合計.Name = "txt２月平均単価総合計"
        Me.txt２月平均単価総合計.OutputFormat = resources.GetString("txt２月平均単価総合計.OutputFormat")
        Me.txt２月平均単価総合計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt２月平均単価総合計.Text = "1,234.5"
        Me.txt２月平均単価総合計.Top = 0!
        Me.txt２月平均単価総合計.Width = 0.5!
        '
        'txt３月平均単価総合計
        '
        Me.txt３月平均単価総合計.DataField = "fld３月平均単価総合計"
        Me.txt３月平均単価総合計.Height = 0.16!
        Me.txt３月平均単価総合計.Left = 8.626!
        Me.txt３月平均単価総合計.MultiLine = False
        Me.txt３月平均単価総合計.Name = "txt３月平均単価総合計"
        Me.txt３月平均単価総合計.OutputFormat = resources.GetString("txt３月平均単価総合計.OutputFormat")
        Me.txt３月平均単価総合計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt３月平均単価総合計.Text = "1,234.5"
        Me.txt３月平均単価総合計.Top = 0!
        Me.txt３月平均単価総合計.Width = 0.5!
        '
        'txt４月平均単価総合計
        '
        Me.txt４月平均単価総合計.DataField = "fld４月平均単価総合計"
        Me.txt４月平均単価総合計.Height = 0.16!
        Me.txt４月平均単価総合計.Left = 9.126!
        Me.txt４月平均単価総合計.MultiLine = False
        Me.txt４月平均単価総合計.Name = "txt４月平均単価総合計"
        Me.txt４月平均単価総合計.OutputFormat = resources.GetString("txt４月平均単価総合計.OutputFormat")
        Me.txt４月平均単価総合計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt４月平均単価総合計.Text = "1,234.5"
        Me.txt４月平均単価総合計.Top = 0!
        Me.txt４月平均単価総合計.Width = 0.5!
        '
        'txt５月平均単価総合計
        '
        Me.txt５月平均単価総合計.DataField = "fld５月平均単価総合計"
        Me.txt５月平均単価総合計.Height = 0.16!
        Me.txt５月平均単価総合計.Left = 9.626!
        Me.txt５月平均単価総合計.MultiLine = False
        Me.txt５月平均単価総合計.Name = "txt５月平均単価総合計"
        Me.txt５月平均単価総合計.OutputFormat = resources.GetString("txt５月平均単価総合計.OutputFormat")
        Me.txt５月平均単価総合計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt５月平均単価総合計.Text = "1,234.5"
        Me.txt５月平均単価総合計.Top = 0!
        Me.txt５月平均単価総合計.Width = 0.5!
        '
        'txt平均単価総合計
        '
        Me.txt平均単価総合計.DataField = "fld平均単価総合計"
        Me.txt平均単価総合計.Height = 0.16!
        Me.txt平均単価総合計.Left = 10.126!
        Me.txt平均単価総合計.MultiLine = False
        Me.txt平均単価総合計.Name = "txt平均単価総合計"
        Me.txt平均単価総合計.OutputFormat = resources.GetString("txt平均単価総合計.OutputFormat")
        Me.txt平均単価総合計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt平均単価総合計.Text = "1,234.5"
        Me.txt平均単価総合計.Top = 0!
        Me.txt平均単価総合計.Width = 0.559001!
        '
        'Line5
        '
        Me.Line5.Height = 0!
        Me.Line5.Left = 0!
        Me.Line5.LineWeight = 2.0!
        Me.Line5.Name = "Line5"
        Me.Line5.Top = 0.16!
        Me.Line5.Width = 10.69!
        Me.Line5.X1 = 0!
        Me.Line5.X2 = 10.69!
        Me.Line5.Y1 = 0.16!
        Me.Line5.Y2 = 0.16!
        '
        'fld２月数量
        '
        Me.fld２月数量.DefaultValue = Nothing
        Me.fld２月数量.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld２月数量.Formula = Nothing
        Me.fld２月数量.Name = "fld２月数量"
        Me.fld２月数量.Tag = Nothing
        '
        'fld３月数量
        '
        Me.fld３月数量.DefaultValue = Nothing
        Me.fld３月数量.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld３月数量.Formula = Nothing
        Me.fld３月数量.Name = "fld３月数量"
        Me.fld３月数量.Tag = Nothing
        '
        'fld４月数量
        '
        Me.fld４月数量.DefaultValue = Nothing
        Me.fld４月数量.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld４月数量.Formula = Nothing
        Me.fld４月数量.Name = "fld４月数量"
        Me.fld４月数量.Tag = Nothing
        '
        'fld５月数量
        '
        Me.fld５月数量.DefaultValue = Nothing
        Me.fld５月数量.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld５月数量.Formula = Nothing
        Me.fld５月数量.Name = "fld５月数量"
        Me.fld５月数量.Tag = Nothing
        '
        'fld数量計
        '
        Me.fld数量計.DefaultValue = Nothing
        Me.fld数量計.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld数量計.Formula = Nothing
        Me.fld数量計.Name = "fld数量計"
        Me.fld数量計.Tag = Nothing
        '
        'fld２月金額
        '
        Me.fld２月金額.DefaultValue = Nothing
        Me.fld２月金額.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld２月金額.Formula = Nothing
        Me.fld２月金額.Name = "fld２月金額"
        Me.fld２月金額.Tag = Nothing
        '
        'fld３月金額
        '
        Me.fld３月金額.DefaultValue = Nothing
        Me.fld３月金額.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld３月金額.Formula = Nothing
        Me.fld３月金額.Name = "fld３月金額"
        Me.fld３月金額.Tag = Nothing
        '
        'fld４月金額
        '
        Me.fld４月金額.DefaultValue = Nothing
        Me.fld４月金額.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld４月金額.Formula = Nothing
        Me.fld４月金額.Name = "fld４月金額"
        Me.fld４月金額.Tag = Nothing
        '
        'fld５月金額
        '
        Me.fld５月金額.DefaultValue = Nothing
        Me.fld５月金額.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld５月金額.Formula = Nothing
        Me.fld５月金額.Name = "fld５月金額"
        Me.fld５月金額.Tag = Nothing
        '
        'fld金額計
        '
        Me.fld金額計.DefaultValue = Nothing
        Me.fld金額計.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld金額計.Formula = Nothing
        Me.fld金額計.Name = "fld金額計"
        Me.fld金額計.Tag = Nothing
        '
        'fld２月平均単価
        '
        Me.fld２月平均単価.DefaultValue = Nothing
        Me.fld２月平均単価.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld２月平均単価.Formula = Nothing
        Me.fld２月平均単価.Name = "fld２月平均単価"
        Me.fld２月平均単価.Tag = Nothing
        '
        'fld３月平均単価
        '
        Me.fld３月平均単価.DefaultValue = Nothing
        Me.fld３月平均単価.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld３月平均単価.Formula = Nothing
        Me.fld３月平均単価.Name = "fld３月平均単価"
        Me.fld３月平均単価.Tag = Nothing
        '
        'fld４月平均単価
        '
        Me.fld４月平均単価.DefaultValue = Nothing
        Me.fld４月平均単価.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld４月平均単価.Formula = Nothing
        Me.fld４月平均単価.Name = "fld４月平均単価"
        Me.fld４月平均単価.Tag = Nothing
        '
        'fld５月平均単価
        '
        Me.fld５月平均単価.DefaultValue = Nothing
        Me.fld５月平均単価.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld５月平均単価.Formula = Nothing
        Me.fld５月平均単価.Name = "fld５月平均単価"
        Me.fld５月平均単価.Tag = Nothing
        '
        'fld平均単価計
        '
        Me.fld平均単価計.DefaultValue = Nothing
        Me.fld平均単価計.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld平均単価計.Formula = Nothing
        Me.fld平均単価計.Name = "fld平均単価計"
        Me.fld平均単価計.Tag = Nothing
        '
        'fld２月平均単価総合計
        '
        Me.fld２月平均単価総合計.DefaultValue = Nothing
        Me.fld２月平均単価総合計.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld２月平均単価総合計.Formula = Nothing
        Me.fld２月平均単価総合計.Name = "fld２月平均単価総合計"
        Me.fld２月平均単価総合計.Tag = Nothing
        '
        'fld３月平均単価総合計
        '
        Me.fld３月平均単価総合計.DefaultValue = Nothing
        Me.fld３月平均単価総合計.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld３月平均単価総合計.Formula = Nothing
        Me.fld３月平均単価総合計.Name = "fld３月平均単価総合計"
        Me.fld３月平均単価総合計.Tag = Nothing
        '
        'fld４月平均単価総合計
        '
        Me.fld４月平均単価総合計.DefaultValue = Nothing
        Me.fld４月平均単価総合計.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld４月平均単価総合計.Formula = Nothing
        Me.fld４月平均単価総合計.Name = "fld４月平均単価総合計"
        Me.fld４月平均単価総合計.Tag = Nothing
        '
        'fld５月平均単価総合計
        '
        Me.fld５月平均単価総合計.DefaultValue = Nothing
        Me.fld５月平均単価総合計.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld５月平均単価総合計.Formula = Nothing
        Me.fld５月平均単価総合計.Name = "fld５月平均単価総合計"
        Me.fld５月平均単価総合計.Tag = Nothing
        '
        'fld平均単価総合計
        '
        Me.fld平均単価総合計.DefaultValue = Nothing
        Me.fld平均単価総合計.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld平均単価総合計.Formula = Nothing
        Me.fld平均単価総合計.Name = "fld平均単価総合計"
        Me.fld平均単価総合計.Tag = Nothing
        '
        'Line23
        '
        Me.Line23.Height = 0!
        Me.Line23.Left = 0!
        Me.Line23.LineWeight = 2.0!
        Me.Line23.Name = "Line23"
        Me.Line23.Top = 0!
        Me.Line23.Width = 10.69!
        Me.Line23.X1 = 0!
        Me.Line23.X2 = 10.69!
        Me.Line23.Y1 = 0!
        Me.Line23.Y2 = 0!
        '
        'H11R21_ShiireSokatuList
        '
        Me.MasterReport = False
        Me.CalculatedFields.Add(Me.fld２月数量)
        Me.CalculatedFields.Add(Me.fld３月数量)
        Me.CalculatedFields.Add(Me.fld４月数量)
        Me.CalculatedFields.Add(Me.fld５月数量)
        Me.CalculatedFields.Add(Me.fld数量計)
        Me.CalculatedFields.Add(Me.fld２月金額)
        Me.CalculatedFields.Add(Me.fld３月金額)
        Me.CalculatedFields.Add(Me.fld４月金額)
        Me.CalculatedFields.Add(Me.fld５月金額)
        Me.CalculatedFields.Add(Me.fld金額計)
        Me.CalculatedFields.Add(Me.fld２月平均単価)
        Me.CalculatedFields.Add(Me.fld３月平均単価)
        Me.CalculatedFields.Add(Me.fld４月平均単価)
        Me.CalculatedFields.Add(Me.fld５月平均単価)
        Me.CalculatedFields.Add(Me.fld平均単価計)
        Me.CalculatedFields.Add(Me.fld２月平均単価総合計)
        Me.CalculatedFields.Add(Me.fld３月平均単価総合計)
        Me.CalculatedFields.Add(Me.fld４月平均単価総合計)
        Me.CalculatedFields.Add(Me.fld５月平均単価総合計)
        Me.CalculatedFields.Add(Me.fld平均単価総合計)
        Me.PageSettings.Margins.Bottom = 0.2!
        Me.PageSettings.Margins.Left = 0.5!
        Me.PageSettings.Margins.Right = 0.5!
        Me.PageSettings.Margins.Top = 0.5!
        Me.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape
        Me.PageSettings.PaperHeight = 11.0!
        Me.PageSettings.PaperWidth = 8.5!
        Me.PrintWidth = 10.69!
        Me.ScriptLanguage = "VB.NET"
        Me.Sections.Add(Me.ReportHeader1)
        Me.Sections.Add(Me.PageHeader)
        Me.Sections.Add(Me.Detail)
        Me.Sections.Add(Me.PageFooter)
        Me.Sections.Add(Me.ReportFooter1)
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-style: normal; text-decoration: none; font-weight: normal; font-size: 10pt; " &
            "color: Black; font-family: ""MS UI Gothic""; ddo-char-set: 128", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold; font-family: ""MS UI Gothic""; ddo-char-set: 12" &
            "8", "Heading1", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 14pt; font-weight: bold; font-style: inherit; font-family: ""MS UI Goth" &
            "ic""; ddo-char-set: 128", "Heading2", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold; ddo-char-set: 128", "Heading3", "Normal"))
        CType(Me.txtPrm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt区分ヘッダ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt仕入年度ヘッダ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label41, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label43, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt作成日ヘッダ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReportInfo1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt仕入先名, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt２月数量, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt仕入先コード, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt２月金額, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt３月数量, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt４月数量, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt５月数量, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt数量計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt３月金額, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt４月金額, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt５月金額, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt金額計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt２月平均単価, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt３月平均単価, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt４月平均単価, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt５月平均単価, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt平均単価計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt会社名, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label35, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt２月数量総合計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt２月金額総合計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt３月数量総合計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt４月数量総合計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt５月数量総合計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt数量総合計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt３月金額総合計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt４月金額総合計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt５月金額総合計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt金額総合計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt２月平均単価総合計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt３月平均単価総合計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt４月平均単価総合計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt５月平均単価総合計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt平均単価総合計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

    Private WithEvents ReportHeader1 As GrapeCity.ActiveReports.SectionReportModel.ReportHeader
    Private WithEvents ReportFooter1 As GrapeCity.ActiveReports.SectionReportModel.ReportFooter
    Private WithEvents txtPrm As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents Label2 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt区分ヘッダ As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label1 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt仕入年度ヘッダ As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label6 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label7 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label12 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label41 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label43 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt作成日ヘッダ As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents ReportInfo1 As GrapeCity.ActiveReports.SectionReportModel.ReportInfo
    Private WithEvents Line3 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents Line6 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents txt仕入先名 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt２月数量 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents Line1 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents txt会社名 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Line7 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents Label5 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label9 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label10 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label11 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label13 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label14 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label15 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label16 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label17 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label18 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label19 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label8 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label20 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label21 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label22 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label23 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label24 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Line2 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents Line4 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents Line8 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents Line9 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents Line10 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents Line11 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents Line12 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents Line13 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents Line14 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents Line15 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents Line16 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents Line17 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents Line18 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents Line19 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents Line20 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents Line21 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents Line22 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents txt仕入先コード As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt２月金額 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt３月数量 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt４月数量 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt５月数量 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt数量計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt３月金額 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt４月金額 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt５月金額 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt金額計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt２月平均単価 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt３月平均単価 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt４月平均単価 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt５月平均単価 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt平均単価計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents Label35 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label3 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt２月数量総合計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt２月金額総合計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt３月数量総合計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt４月数量総合計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt５月数量総合計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt数量総合計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt３月金額総合計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt４月金額総合計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt５月金額総合計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt金額総合計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt２月平均単価総合計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt３月平均単価総合計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt４月平均単価総合計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt５月平均単価総合計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt平均単価総合計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents Line5 As GrapeCity.ActiveReports.SectionReportModel.Line
    Friend WithEvents fld２月数量 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld３月数量 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld４月数量 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld５月数量 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld数量計 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld２月金額 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld３月金額 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld４月金額 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld５月金額 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld金額計 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld２月平均単価 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld３月平均単価 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld４月平均単価 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld５月平均単価 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld平均単価計 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld２月平均単価総合計 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld３月平均単価総合計 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld４月平均単価総合計 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld５月平均単価総合計 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld平均単価総合計 As GrapeCity.ActiveReports.Data.Field
    Private WithEvents Line23 As GrapeCity.ActiveReports.SectionReportModel.Line
End Class
