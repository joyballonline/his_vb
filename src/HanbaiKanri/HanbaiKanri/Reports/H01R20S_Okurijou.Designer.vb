<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class H01R20S_Okurijou
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
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(H01R20S_Okurijou))
        Me.Detail = New GrapeCity.ActiveReports.SectionReportModel.Detail()
        Me.txtGoodsNm = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtIrisu = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtKosu = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtBiko = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtTani = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.TextBox1 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtGoodsCd = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.lblReitouKb = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Parameter1 = New GrapeCity.ActiveReports.SectionReportModel.Parameter()
        CType(Me.txtGoodsNm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIrisu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKosu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBiko, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTani, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGoodsCd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReitouKb, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.txtGoodsNm, Me.txtIrisu, Me.txtKosu, Me.txtBiko, Me.txtTani, Me.TextBox1, Me.txtGoodsCd, Me.lblReitouKb})
        Me.Detail.Height = 0.36!
        Me.Detail.Name = "Detail"
        Me.Detail.RepeatToFill = True
        '
        'txtGoodsNm
        '
        Me.txtGoodsNm.Border.BottomStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtGoodsNm.Border.LeftStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtGoodsNm.Border.RightStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtGoodsNm.Border.TopStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtGoodsNm.DataField = "商品名"
        Me.txtGoodsNm.Height = 0.36!
        Me.txtGoodsNm.Left = 0.042!
        Me.txtGoodsNm.MultiLine = False
        Me.txtGoodsNm.Name = "txtGoodsNm"
        Me.txtGoodsNm.Padding = New GrapeCity.ActiveReports.PaddingEx(5, 0, 0, 0)
        Me.txtGoodsNm.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left; vertical-align: bottom"
        Me.txtGoodsNm.Text = "商品名"
        Me.txtGoodsNm.Top = 0!
        Me.txtGoodsNm.Width = 3.0!
        '
        'txtIrisu
        '
        Me.txtIrisu.Border.BottomStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtIrisu.Border.LeftStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtIrisu.Border.TopStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtIrisu.DataField = "入数"
        Me.txtIrisu.Height = 0.36!
        Me.txtIrisu.Left = 3.042!
        Me.txtIrisu.Name = "txtIrisu"
        Me.txtIrisu.Style = "font-family: ＭＳ 明朝; font-size: 12pt; text-align: right; vertical-align: middle"
        Me.txtIrisu.Text = "9.00"
        Me.txtIrisu.Top = 0!
        Me.txtIrisu.Width = 0.75!
        '
        'txtKosu
        '
        Me.txtKosu.Border.BottomStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtKosu.Border.LeftStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtKosu.Border.TopStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtKosu.DataField = "個数"
        Me.txtKosu.Height = 0.36!
        Me.txtKosu.Left = 4.042!
        Me.txtKosu.Name = "txtKosu"
        Me.txtKosu.Style = "font-family: ＭＳ 明朝; font-size: 12pt; text-align: right; vertical-align: middle"
        Me.txtKosu.Text = "1"
        Me.txtKosu.Top = 0!
        Me.txtKosu.Width = 0.75!
        '
        'txtBiko
        '
        Me.txtBiko.Border.BottomStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtBiko.Border.LeftStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtBiko.Border.RightStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtBiko.Border.TopStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtBiko.DataField = "明細備考"
        Me.txtBiko.Height = 0.36!
        Me.txtBiko.Left = 5.042!
        Me.txtBiko.Name = "txtBiko"
        Me.txtBiko.Padding = New GrapeCity.ActiveReports.PaddingEx(5, 0, 0, 0)
        Me.txtBiko.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left; vertical-align: middle"
        Me.txtBiko.Text = "商品毎の備考を印字する２" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "行目"
        Me.txtBiko.Top = 0!
        Me.txtBiko.Width = 2.0!
        '
        'txtTani
        '
        Me.txtTani.Border.BottomStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtTani.Border.RightStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtTani.Border.TopStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtTani.DataField = "単位"
        Me.txtTani.Height = 0.36!
        Me.txtTani.Left = 3.792!
        Me.txtTani.Name = "txtTani"
        Me.txtTani.Padding = New GrapeCity.ActiveReports.PaddingEx(0, 0, 3, 0)
        Me.txtTani.Style = "font-family: ＭＳ 明朝; font-size: 11.25pt; text-align: right; vertical-align: middle" &
    ""
        Me.txtTani.Text = "Kg"
        Me.txtTani.Top = 0!
        Me.txtTani.Width = 0.25!
        '
        'TextBox1
        '
        Me.TextBox1.Border.BottomStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.TextBox1.Border.RightStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.TextBox1.Border.TopStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.TextBox1.DataField = "単位２"
        Me.TextBox1.Height = 0.36!
        Me.TextBox1.Left = 4.792!
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Padding = New GrapeCity.ActiveReports.PaddingEx(0, 0, 3, 0)
        Me.TextBox1.Style = "font-family: ＭＳ 明朝; font-size: 11.25pt; text-align: right; text-justify: auto; ve" &
    "rtical-align: middle"
        Me.TextBox1.Text = "個"
        Me.TextBox1.Top = 0!
        Me.TextBox1.Width = 0.25!
        '
        'txtGoodsCd
        '
        Me.txtGoodsCd.DataField = "商品コード"
        Me.txtGoodsCd.Height = 0.18!
        Me.txtGoodsCd.HyperLink = Nothing
        Me.txtGoodsCd.Left = 0.042!
        Me.txtGoodsCd.Name = "txtGoodsCd"
        Me.txtGoodsCd.Padding = New GrapeCity.ActiveReports.PaddingEx(5, 0, 0, 0)
        Me.txtGoodsCd.Style = "font-family: ＭＳ 明朝; font-size: 9pt; vertical-align: bottom"
        Me.txtGoodsCd.Text = "商品コード"
        Me.txtGoodsCd.Top = 0!
        Me.txtGoodsCd.Width = 1.0!
        '
        'lblReitouKb
        '
        Me.lblReitouKb.DataField = "文字１"
        Me.lblReitouKb.Height = 0.18!
        Me.lblReitouKb.HyperLink = Nothing
        Me.lblReitouKb.Left = 2.042!
        Me.lblReitouKb.Name = "lblReitouKb"
        Me.lblReitouKb.Padding = New GrapeCity.ActiveReports.PaddingEx(0, 0, 5, 0)
        Me.lblReitouKb.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: bottom"
        Me.lblReitouKb.Text = "冷凍区分"
        Me.lblReitouKb.Top = 0!
        Me.lblReitouKb.Width = 1.0!
        '
        'Parameter1
        '
        Me.Parameter1.DefaultValue = ""
        Me.Parameter1.Key = "Parameter1"
        Me.Parameter1.Prompt = Nothing
        Me.Parameter1.PromptUser = True
        Me.Parameter1.QueryCreated = False
        Me.Parameter1.Tag = Nothing
        Me.Parameter1.Type = GrapeCity.ActiveReports.SectionReportModel.Parameter.DataType.[String]
        '
        'H01R20S_Okurijou
        '
        Me.MasterReport = False
        Me.PageSettings.PaperHeight = 11.0!
        Me.PageSettings.PaperWidth = 8.5!
        Me.Parameters.AddRange(New GrapeCity.ActiveReports.SectionReportModel.Parameter() {Me.Parameter1})
        Me.PrintWidth = 7.197917!
        Me.ScriptLanguage = "VB.NET"
        Me.Sections.Add(Me.Detail)
        Me.ShowParameterUI = False
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-style: normal; text-decoration: none; font-weight: normal; font-size: 10pt; " &
            "color: Black; font-family: ""MS UI Gothic""; ddo-char-set: 128", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold; font-family: ""MS UI Gothic""; ddo-char-set: 12" &
            "8", "Heading1", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 14pt; font-weight: bold; font-style: inherit; font-family: ""MS UI Goth" &
            "ic""; ddo-char-set: 128", "Heading2", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold; ddo-char-set: 128", "Heading3", "Normal"))
        CType(Me.txtGoodsNm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIrisu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKosu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBiko, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTani, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGoodsCd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReitouKb, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

    Private WithEvents txtGoodsNm As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtIrisu As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtKosu As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtBiko As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Public WithEvents Parameter1 As GrapeCity.ActiveReports.SectionReportModel.Parameter
    Private WithEvents txtTani As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents TextBox1 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtGoodsCd As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents lblReitouKb As GrapeCity.ActiveReports.SectionReportModel.Label
End Class
