<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class H10R04S_TokuisakiMotoList
    Inherits GrapeCity.ActiveReports.SectionReport

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
        End If
        MyBase.Dispose(disposing)
    End Sub
    Private WithEvents Detail As GrapeCity.ActiveReports.SectionReportModel.Detail
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(H10R04S_TokuisakiMotoList))
        Me.Detail = New GrapeCity.ActiveReports.SectionReportModel.Detail()
        Me.txt出荷先名 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt委託日 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt伝票番号 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt区分 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt商品 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt単位 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt残数量 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt仮単価 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt見込額 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.Line4 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.PageHeader1 = New GrapeCity.ActiveReports.SectionReportModel.PageHeader()
        Me.Label6 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label7 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label18 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label4 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label12 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label11 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label13 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label14 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Line1 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.PageFooter1 = New GrapeCity.ActiveReports.SectionReportModel.PageFooter()
        Me.fld残数量 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld仮単価 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld見込額 = New GrapeCity.ActiveReports.Data.Field()
        Me.Label1 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Line3 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.ReportHeader1 = New GrapeCity.ActiveReports.SectionReportModel.ReportHeader()
        Me.ReportFooter1 = New GrapeCity.ActiveReports.SectionReportModel.ReportFooter()
        Me.Line2 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        CType(Me.txt出荷先名, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt委託日, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt伝票番号, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt区分, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt商品, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt単位, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt残数量, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt仮単価, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt見込額, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.txt出荷先名, Me.txt委託日, Me.txt伝票番号, Me.txt区分, Me.txt商品, Me.txt単位, Me.txt残数量, Me.txt仮単価, Me.txt見込額, Me.Line4})
        Me.Detail.Height = 0.16!
        Me.Detail.Name = "Detail"
        '
        'txt出荷先名
        '
        Me.txt出荷先名.Height = 0.16!
        Me.txt出荷先名.HyperLink = Nothing
        Me.txt出荷先名.Left = 1.646!
        Me.txt出荷先名.MultiLine = False
        Me.txt出荷先名.Name = "txt出荷先名"
        Me.txt出荷先名.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left; vertical-align: middle"
        Me.txt出荷先名.Text = "株式会社　〇〇〇〇〇〇〇〇"
        Me.txt出荷先名.Top = 0!
        Me.txt出荷先名.Width = 1.453!
        '
        'txt委託日
        '
        Me.txt委託日.Height = 0.16!
        Me.txt委託日.HyperLink = Nothing
        Me.txt委託日.Left = 0.0000001192093!
        Me.txt委託日.MultiLine = False
        Me.txt委託日.Name = "txt委託日"
        Me.txt委託日.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left; vertical-align: middle"
        Me.txt委託日.Text = "2018/02/01"
        Me.txt委託日.Top = 0!
        Me.txt委託日.Width = 0.67!
        '
        'txt伝票番号
        '
        Me.txt伝票番号.Height = 0.16!
        Me.txt伝票番号.HyperLink = Nothing
        Me.txt伝票番号.Left = 0.6700002!
        Me.txt伝票番号.MultiLine = False
        Me.txt伝票番号.Name = "txt伝票番号"
        Me.txt伝票番号.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left; vertical-align: middle"
        Me.txt伝票番号.Text = "T000021"
        Me.txt伝票番号.Top = 0!
        Me.txt伝票番号.Width = 0.636!
        '
        'txt区分
        '
        Me.txt区分.Height = 0.16!
        Me.txt区分.HyperLink = Nothing
        Me.txt区分.Left = 1.306!
        Me.txt区分.MultiLine = False
        Me.txt区分.Name = "txt区分"
        Me.txt区分.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: center; vertical-align: middle"
        Me.txt区分.Text = "委託"
        Me.txt区分.Top = 0!
        Me.txt区分.Width = 0.34!
        '
        'txt商品
        '
        Me.txt商品.Height = 0.16!
        Me.txt商品.HyperLink = Nothing
        Me.txt商品.Left = 3.099!
        Me.txt商品.MultiLine = False
        Me.txt商品.Name = "txt商品"
        Me.txt商品.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left; vertical-align: middle"
        Me.txt商品.Text = "いわて生協細切めかぶ(宮城産)40gx3p 12x4"
        Me.txt商品.Top = 0!
        Me.txt商品.Width = 2.43!
        '
        'txt単位
        '
        Me.txt単位.DataField = "単位"
        Me.txt単位.Height = 0.16!
        Me.txt単位.HyperLink = Nothing
        Me.txt単位.Left = 6.215!
        Me.txt単位.MultiLine = False
        Me.txt単位.Name = "txt単位"
        Me.txt単位.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: center; vertical-align: middle"
        Me.txt単位.Text = "個"
        Me.txt単位.Top = 0!
        Me.txt単位.Width = 0.211!
        '
        'txt残数量
        '
        Me.txt残数量.Height = 0.16!
        Me.txt残数量.Left = 5.529!
        Me.txt残数量.MultiLine = False
        Me.txt残数量.Name = "txt残数量"
        Me.txt残数量.OutputFormat = resources.GetString("txt残数量.OutputFormat")
        Me.txt残数量.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt残数量.Text = "12,345.00"
        Me.txt残数量.Top = 0!
        Me.txt残数量.Width = 0.686!
        '
        'txt仮単価
        '
        Me.txt仮単価.Height = 0.16!
        Me.txt仮単価.Left = 6.426001!
        Me.txt仮単価.MultiLine = False
        Me.txt仮単価.Name = "txt仮単価"
        Me.txt仮単価.OutputFormat = resources.GetString("txt仮単価.OutputFormat")
        Me.txt仮単価.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt仮単価.Text = "12,345.00"
        Me.txt仮単価.Top = 0!
        Me.txt仮単価.Width = 0.705!
        '
        'txt見込額
        '
        Me.txt見込額.Height = 0.16!
        Me.txt見込額.Left = 7.131!
        Me.txt見込額.MultiLine = False
        Me.txt見込額.Name = "txt見込額"
        Me.txt見込額.OutputFormat = resources.GetString("txt見込額.OutputFormat")
        Me.txt見込額.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt見込額.Text = "12,345"
        Me.txt見込額.Top = 0!
        Me.txt見込額.Width = 0.705!
        '
        'Line4
        '
        Me.Line4.Height = 0!
        Me.Line4.Left = 0!
        Me.Line4.LineStyle = GrapeCity.ActiveReports.SectionReportModel.LineStyle.Dash
        Me.Line4.LineWeight = 1.0!
        Me.Line4.Name = "Line4"
        Me.Line4.Top = 0!
        Me.Line4.Width = 8.0!
        Me.Line4.X1 = 0!
        Me.Line4.X2 = 8.0!
        Me.Line4.Y1 = 0!
        Me.Line4.Y2 = 0!
        '
        'PageHeader1
        '
        Me.PageHeader1.Height = 0!
        Me.PageHeader1.Name = "PageHeader1"
        Me.PageHeader1.Visible = False
        '
        'Label6
        '
        Me.Label6.Height = 0.15!
        Me.Label6.HyperLink = Nothing
        Me.Label6.Left = 1.306!
        Me.Label6.Name = "Label6"
        Me.Label6.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: center; ve" &
    "rtical-align: top"
        Me.Label6.Text = "区分"
        Me.Label6.Top = 0.17!
        Me.Label6.Width = 0.34!
        '
        'Label7
        '
        Me.Label7.Height = 0.15!
        Me.Label7.HyperLink = Nothing
        Me.Label7.Left = 0.0000002384186!
        Me.Label7.Name = "Label7"
        Me.Label7.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: center; ve" &
    "rtical-align: top"
        Me.Label7.Text = "委託日"
        Me.Label7.Top = 0.17!
        Me.Label7.Width = 0.67!
        '
        'Label18
        '
        Me.Label18.Height = 0.15!
        Me.Label18.HyperLink = Nothing
        Me.Label18.Left = 5.529!
        Me.Label18.Name = "Label18"
        Me.Label18.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label18.Text = "残数量"
        Me.Label18.Top = 0.17!
        Me.Label18.Width = 0.686!
        '
        'Label4
        '
        Me.Label4.Height = 0.15!
        Me.Label4.HyperLink = Nothing
        Me.Label4.Left = 6.426001!
        Me.Label4.Name = "Label4"
        Me.Label4.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label4.Text = "仮単価"
        Me.Label4.Top = 0.17!
        Me.Label4.Width = 0.705!
        '
        'Label12
        '
        Me.Label12.Height = 0.15!
        Me.Label12.HyperLink = Nothing
        Me.Label12.Left = 7.131!
        Me.Label12.Name = "Label12"
        Me.Label12.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label12.Text = "見込額"
        Me.Label12.Top = 0.17!
        Me.Label12.Width = 0.705!
        '
        'Label11
        '
        Me.Label11.Height = 0.15!
        Me.Label11.HyperLink = Nothing
        Me.Label11.Left = 0.6700003!
        Me.Label11.Name = "Label11"
        Me.Label11.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: top"
        Me.Label11.Text = "伝票番号"
        Me.Label11.Top = 0.17!
        Me.Label11.Width = 0.636!
        '
        'Label13
        '
        Me.Label13.Height = 0.15!
        Me.Label13.HyperLink = Nothing
        Me.Label13.Left = 1.646!
        Me.Label13.Name = "Label13"
        Me.Label13.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: top"
        Me.Label13.Text = "出荷先"
        Me.Label13.Top = 0.17!
        Me.Label13.Width = 1.631!
        '
        'Label14
        '
        Me.Label14.Height = 0.15!
        Me.Label14.HyperLink = Nothing
        Me.Label14.Left = 3.099!
        Me.Label14.Name = "Label14"
        Me.Label14.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: top"
        Me.Label14.Text = "商品"
        Me.Label14.Top = 0.17!
        Me.Label14.Width = 2.43!
        '
        'Line1
        '
        Me.Line1.Height = 0!
        Me.Line1.Left = 0.0000001192093!
        Me.Line1.LineWeight = 2.0!
        Me.Line1.Name = "Line1"
        Me.Line1.Top = 0.32!
        Me.Line1.Width = 8.000001!
        Me.Line1.X1 = 0.0000001192093!
        Me.Line1.X2 = 8.000001!
        Me.Line1.Y1 = 0.32!
        Me.Line1.Y2 = 0.32!
        '
        'PageFooter1
        '
        Me.PageFooter1.Height = 0!
        Me.PageFooter1.Name = "PageFooter1"
        '
        'fld残数量
        '
        Me.fld残数量.DefaultValue = Nothing
        Me.fld残数量.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld残数量.Formula = Nothing
        Me.fld残数量.Name = "fld残数量"
        Me.fld残数量.Tag = Nothing
        '
        'fld仮単価
        '
        Me.fld仮単価.DefaultValue = Nothing
        Me.fld仮単価.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld仮単価.Formula = Nothing
        Me.fld仮単価.Name = "fld仮単価"
        Me.fld仮単価.Tag = Nothing
        '
        'fld見込額
        '
        Me.fld見込額.DefaultValue = Nothing
        Me.fld見込額.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld見込額.Formula = Nothing
        Me.fld見込額.Name = "fld見込額"
        Me.fld見込額.Tag = Nothing
        '
        'Label1
        '
        Me.Label1.Height = 0.15!
        Me.Label1.HyperLink = Nothing
        Me.Label1.Left = 0!
        Me.Label1.Name = "Label1"
        Me.Label1.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: top"
        Me.Label1.Text = "【委託残情報】"
        Me.Label1.Top = 0!
        Me.Label1.Width = 1.014!
        '
        'Line3
        '
        Me.Line3.Height = 0!
        Me.Line3.Left = 0!
        Me.Line3.LineWeight = 2.0!
        Me.Line3.Name = "Line3"
        Me.Line3.Top = 0.16!
        Me.Line3.Width = 8.0!
        Me.Line3.X1 = 0!
        Me.Line3.X2 = 8.0!
        Me.Line3.Y1 = 0.16!
        Me.Line3.Y2 = 0.16!
        '
        'ReportHeader1
        '
        Me.ReportHeader1.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.Label6, Me.Label7, Me.Label18, Me.Label4, Me.Label12, Me.Label11, Me.Label13, Me.Label14, Me.Label1, Me.Line3, Me.Line1})
        Me.ReportHeader1.Height = 0.32!
        Me.ReportHeader1.Name = "ReportHeader1"
        '
        'ReportFooter1
        '
        Me.ReportFooter1.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.Line2})
        Me.ReportFooter1.Height = 0.04166667!
        Me.ReportFooter1.Name = "ReportFooter1"
        '
        'Line2
        '
        Me.Line2.Height = 0!
        Me.Line2.Left = 0!
        Me.Line2.LineWeight = 2.0!
        Me.Line2.Name = "Line2"
        Me.Line2.Top = 0!
        Me.Line2.Width = 8.0!
        Me.Line2.X1 = 0!
        Me.Line2.X2 = 8.0!
        Me.Line2.Y1 = 0!
        Me.Line2.Y2 = 0!
        '
        'H10R04S_TokuisakiMotoList
        '
        Me.MasterReport = False
        Me.CalculatedFields.Add(Me.fld残数量)
        Me.CalculatedFields.Add(Me.fld仮単価)
        Me.CalculatedFields.Add(Me.fld見込額)
        Me.PageSettings.PaperHeight = 11.0!
        Me.PageSettings.PaperWidth = 8.5!
        Me.PrintWidth = 8.000001!
        Me.ScriptLanguage = "VB.NET"
        Me.Sections.Add(Me.ReportHeader1)
        Me.Sections.Add(Me.PageHeader1)
        Me.Sections.Add(Me.Detail)
        Me.Sections.Add(Me.PageFooter1)
        Me.Sections.Add(Me.ReportFooter1)
        Me.ShowParameterUI = False
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-style: normal; text-decoration: none; font-weight: normal; font-size: 10pt; " &
            "color: Black; font-family: ""MS UI Gothic""; ddo-char-set: 128", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold; font-family: ""MS UI Gothic""; ddo-char-set: 12" &
            "8", "Heading1", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 14pt; font-weight: bold; font-style: inherit; font-family: ""MS UI Goth" &
            "ic""; ddo-char-set: 128", "Heading2", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold; ddo-char-set: 128", "Heading3", "Normal"))
        CType(Me.txt出荷先名, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt委託日, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt伝票番号, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt区分, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt商品, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt単位, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt残数量, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt仮単価, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt見込額, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

    Private WithEvents PageHeader1 As GrapeCity.ActiveReports.SectionReportModel.PageHeader
    Private WithEvents PageFooter1 As GrapeCity.ActiveReports.SectionReportModel.PageFooter
    Private WithEvents Label6 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label7 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label18 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label4 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label12 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label11 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label13 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label14 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Line1 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents txt出荷先名 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt委託日 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt伝票番号 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt区分 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt商品 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt単位 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt残数量 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt仮単価 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt見込額 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents Line4 As GrapeCity.ActiveReports.SectionReportModel.Line
    Friend WithEvents fld残数量 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld仮単価 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld見込額 As GrapeCity.ActiveReports.Data.Field
    Private WithEvents Label1 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Line3 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents ReportHeader1 As GrapeCity.ActiveReports.SectionReportModel.ReportHeader
    Private WithEvents ReportFooter1 As GrapeCity.ActiveReports.SectionReportModel.ReportFooter
    Private WithEvents Line2 As GrapeCity.ActiveReports.SectionReportModel.Line
End Class
