<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class SampleSectionReport
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
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(SampleSectionReport))
        Me.PageHeader = New GrapeCity.ActiveReports.SectionReportModel.PageHeader()
        Me.Label1 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txtPrm = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.Label2 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label3 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label4 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Detail = New GrapeCity.ActiveReports.SectionReportModel.Detail()
        Me.txtCol1 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCol2 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCol3 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.PageFooter = New GrapeCity.ActiveReports.SectionReportModel.PageFooter()
        Me.Label5 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Line1 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.GroupHeader1 = New GrapeCity.ActiveReports.SectionReportModel.GroupHeader()
        Me.GroupFooter1 = New GrapeCity.ActiveReports.SectionReportModel.GroupFooter()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPrm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCol1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCol2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCol3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.Label1, Me.txtPrm, Me.Label2, Me.Label3, Me.Label4})
        Me.PageHeader.Height = 0.783!
        Me.PageHeader.Name = "PageHeader"
        '
        'Label1
        '
        Me.Label1.Height = 0.1979167!
        Me.Label1.HyperLink = Nothing
        Me.Label1.Left = 0.823!
        Me.Label1.Name = "Label1"
        Me.Label1.Style = ""
        Me.Label1.Text = "画面からのパラメータ"
        Me.Label1.Top = 0.135!
        Me.Label1.Width = 1.354!
        '
        'txtPrm
        '
        Me.txtPrm.Height = 0.1979167!
        Me.txtPrm.Left = 2.281!
        Me.txtPrm.Name = "txtPrm"
        Me.txtPrm.Text = Nothing
        Me.txtPrm.Top = 0.135!
        Me.txtPrm.Width = 1.0!
        '
        'Label2
        '
        Me.Label2.Height = 0.1979167!
        Me.Label2.HyperLink = Nothing
        Me.Label2.Left = 0.9479167!
        Me.Label2.Name = "Label2"
        Me.Label2.Style = ""
        Me.Label2.Text = "col1"
        Me.Label2.Top = 0.5833333!
        Me.Label2.Width = 1.0!
        '
        'Label3
        '
        Me.Label3.Height = 0.2!
        Me.Label3.HyperLink = Nothing
        Me.Label3.Left = 2.177!
        Me.Label3.Name = "Label3"
        Me.Label3.Style = ""
        Me.Label3.Text = "col2"
        Me.Label3.Top = 0.583!
        Me.Label3.Width = 1.0!
        '
        'Label4
        '
        Me.Label4.Height = 0.2!
        Me.Label4.HyperLink = Nothing
        Me.Label4.Left = 3.396!
        Me.Label4.Name = "Label4"
        Me.Label4.Style = ""
        Me.Label4.Text = "col3"
        Me.Label4.Top = 0.581!
        Me.Label4.Width = 1.0!
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.txtCol1, Me.txtCol2, Me.txtCol3})
        Me.Detail.Height = 0.2!
        Me.Detail.Name = "Detail"
        '
        'txtCol1
        '
        Me.txtCol1.Height = 0.2!
        Me.txtCol1.Left = 0.9480001!
        Me.txtCol1.Name = "txtCol1"
        Me.txtCol1.Text = "TextBox2"
        Me.txtCol1.Top = 0!
        Me.txtCol1.Width = 1.0!
        '
        'txtCol2
        '
        Me.txtCol2.Height = 0.2!
        Me.txtCol2.Left = 2.177!
        Me.txtCol2.Name = "txtCol2"
        Me.txtCol2.Text = "TextBox2"
        Me.txtCol2.Top = 0!
        Me.txtCol2.Width = 1.0!
        '
        'txtCol3
        '
        Me.txtCol3.Height = 0.2!
        Me.txtCol3.Left = 3.396!
        Me.txtCol3.Name = "txtCol3"
        Me.txtCol3.Text = "TextBox2"
        Me.txtCol3.Top = 0!
        Me.txtCol3.Width = 1.0!
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.Label5, Me.Line1})
        Me.PageFooter.Height = 0.6770833!
        Me.PageFooter.Name = "PageFooter"
        '
        'Label5
        '
        Me.Label5.Height = 0.1979167!
        Me.Label5.HyperLink = Nothing
        Me.Label5.Left = 2.177!
        Me.Label5.Name = "Label5"
        Me.Label5.Style = ""
        Me.Label5.Text = "ここはフッタですね。"
        Me.Label5.Top = 0.219!
        Me.Label5.Width = 1.281!
        '
        'Line1
        '
        Me.Line1.Height = 0!
        Me.Line1.Left = 0!
        Me.Line1.LineWeight = 1.0!
        Me.Line1.Name = "Line1"
        Me.Line1.Top = 0!
        Me.Line1.Width = 6.09375!
        Me.Line1.X1 = 0!
        Me.Line1.X2 = 6.09375!
        Me.Line1.Y1 = 0!
        Me.Line1.Y2 = 0!
        '
        'GroupHeader1
        '
        Me.GroupHeader1.Height = 0!
        Me.GroupHeader1.Name = "GroupHeader1"
        '
        'GroupFooter1
        '
        Me.GroupFooter1.Height = 0!
        Me.GroupFooter1.Name = "GroupFooter1"
        '
        'SampleSectionReport
        '
        Me.MasterReport = False
        Me.PageSettings.PaperHeight = 11.0!
        Me.PageSettings.PaperWidth = 8.5!
        Me.PrintWidth = 6.09375!
        Me.Sections.Add(Me.PageHeader)
        Me.Sections.Add(Me.GroupHeader1)
        Me.Sections.Add(Me.Detail)
        Me.Sections.Add(Me.GroupFooter1)
        Me.Sections.Add(Me.PageFooter)
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-style: normal; text-decoration: none; font-weight: normal; font-size: 10pt; " &
            "color: Black; font-family: ""MS UI Gothic""; ddo-char-set: 128", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold; font-family: ""MS UI Gothic""; ddo-char-set: 12" &
            "8", "Heading1", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 14pt; font-weight: bold; font-style: inherit; font-family: ""MS UI Goth" &
            "ic""; ddo-char-set: 128", "Heading2", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold; ddo-char-set: 128", "Heading3", "Normal"))
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPrm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCol1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCol2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCol3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

    Private WithEvents Label1 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txtPrm As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents Label2 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label3 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label4 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txtCol1 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCol2 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCol3 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents Label5 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Line1 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents GroupHeader1 As GrapeCity.ActiveReports.SectionReportModel.GroupHeader
    Private WithEvents GroupFooter1 As GrapeCity.ActiveReports.SectionReportModel.GroupFooter
End Class
