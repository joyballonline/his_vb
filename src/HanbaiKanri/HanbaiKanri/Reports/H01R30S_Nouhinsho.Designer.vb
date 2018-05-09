<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class H01R30S_Nouhinsho
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
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(H01R30S_Nouhinsho))
        Me.Detail = New GrapeCity.ActiveReports.SectionReportModel.Detail()
        Me.txtGoodsNm = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtIrisu = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtKingaku = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtBiko = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtTani1 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtGoodsCd = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.lblReitouKb = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txtSuuryo = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtTanka = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtKosuu = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtTani2 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        CType(Me.txtGoodsNm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtIrisu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKingaku, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBiko, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTani1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtGoodsCd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblReitouKb, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSuuryo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTanka, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKosuu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTani2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.txtGoodsNm, Me.txtIrisu, Me.txtKingaku, Me.txtBiko, Me.txtTani1, Me.txtGoodsCd, Me.lblReitouKb, Me.txtSuuryo, Me.txtTanka, Me.txtKosuu, Me.txtTani2})
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
        Me.txtGoodsNm.Width = 2.7!
        '
        'txtIrisu
        '
        Me.txtIrisu.Border.LeftStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtIrisu.Border.TopStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtIrisu.DataField = "入数"
        Me.txtIrisu.Height = 0.18!
        Me.txtIrisu.Left = 2.742!
        Me.txtIrisu.Name = "txtIrisu"
        Me.txtIrisu.Style = "font-family: ＭＳ 明朝; font-size: 9.75pt; text-align: right; vertical-align: bottom"
        Me.txtIrisu.Text = "9.00"
        Me.txtIrisu.Top = 0!
        Me.txtIrisu.Width = 0.5!
        '
        'txtKingaku
        '
        Me.txtKingaku.Border.BottomStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtKingaku.Border.LeftStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtKingaku.Border.RightStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtKingaku.Border.TopStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtKingaku.DataField = "注文金額"
        Me.txtKingaku.Height = 0.36!
        Me.txtKingaku.Left = 4.992!
        Me.txtKingaku.Name = "txtKingaku"
        Me.txtKingaku.OutputFormat = resources.GetString("txtKingaku.OutputFormat")
        Me.txtKingaku.Padding = New GrapeCity.ActiveReports.PaddingEx(0, 0, 5, 0)
        Me.txtKingaku.Style = "font-family: ＭＳ 明朝; font-size: 9.75pt; text-align: right; vertical-align: middle"
        Me.txtKingaku.Text = "1"
        Me.txtKingaku.Top = 0!
        Me.txtKingaku.Width = 0.75!
        '
        'txtBiko
        '
        Me.txtBiko.Border.BottomStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtBiko.Border.LeftStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtBiko.Border.RightStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtBiko.Border.TopStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtBiko.DataField = "明細備考"
        Me.txtBiko.Height = 0.36!
        Me.txtBiko.Left = 5.742!
        Me.txtBiko.Name = "txtBiko"
        Me.txtBiko.Padding = New GrapeCity.ActiveReports.PaddingEx(5, 0, 0, 0)
        Me.txtBiko.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left; vertical-align: middle"
        Me.txtBiko.Text = "商品毎の備考を印字する２" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "行目"
        Me.txtBiko.Top = 0!
        Me.txtBiko.Width = 1.65!
        '
        'txtTani1
        '
        Me.txtTani1.Border.RightStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtTani1.Border.TopStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtTani1.DataField = "単位"
        Me.txtTani1.Height = 0.18!
        Me.txtTani1.Left = 3.242!
        Me.txtTani1.Name = "txtTani1"
        Me.txtTani1.Padding = New GrapeCity.ActiveReports.PaddingEx(0, 0, 3, 0)
        Me.txtTani1.Style = "font-family: ＭＳ 明朝; font-size: 9.75pt; text-align: right; vertical-align: bottom"
        Me.txtTani1.Text = "Kg"
        Me.txtTani1.Top = 0!
        Me.txtTani1.Width = 0.25!
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
        Me.lblReitouKb.Left = 1.742!
        Me.lblReitouKb.Name = "lblReitouKb"
        Me.lblReitouKb.Padding = New GrapeCity.ActiveReports.PaddingEx(0, 0, 5, 0)
        Me.lblReitouKb.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: bottom"
        Me.lblReitouKb.Text = "冷凍区分"
        Me.lblReitouKb.Top = 0!
        Me.lblReitouKb.Width = 1.0!
        '
        'txtSuuryo
        '
        Me.txtSuuryo.Border.BottomStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtSuuryo.Border.LeftStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtSuuryo.Border.RightStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtSuuryo.Border.TopStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtSuuryo.DataField = "数量"
        Me.txtSuuryo.Height = 0.36!
        Me.txtSuuryo.Left = 3.492!
        Me.txtSuuryo.Name = "txtSuuryo"
        Me.txtSuuryo.Padding = New GrapeCity.ActiveReports.PaddingEx(0, 0, 5, 0)
        Me.txtSuuryo.Style = "font-family: ＭＳ 明朝; font-size: 9.75pt; text-align: right; vertical-align: middle"
        Me.txtSuuryo.Text = "1"
        Me.txtSuuryo.Top = 0!
        Me.txtSuuryo.Width = 0.75!
        '
        'txtTanka
        '
        Me.txtTanka.Border.BottomStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtTanka.Border.LeftStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtTanka.Border.RightStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtTanka.Border.TopStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtTanka.DataField = "注文単価"
        Me.txtTanka.Height = 0.36!
        Me.txtTanka.Left = 4.242!
        Me.txtTanka.Name = "txtTanka"
        Me.txtTanka.OutputFormat = resources.GetString("txtTanka.OutputFormat")
        Me.txtTanka.Padding = New GrapeCity.ActiveReports.PaddingEx(0, 0, 5, 0)
        Me.txtTanka.Style = "font-family: ＭＳ 明朝; font-size: 9.75pt; text-align: right; vertical-align: middle"
        Me.txtTanka.Text = "1"
        Me.txtTanka.Top = 0!
        Me.txtTanka.Width = 0.75!
        '
        'txtKosuu
        '
        Me.txtKosuu.Border.BottomStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtKosuu.Border.LeftStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtKosuu.DataField = "個数"
        Me.txtKosuu.Height = 0.18!
        Me.txtKosuu.Left = 2.742!
        Me.txtKosuu.Name = "txtKosuu"
        Me.txtKosuu.Style = "font-family: ＭＳ 明朝; font-size: 9.75pt; text-align: right; vertical-align: bottom"
        Me.txtKosuu.Text = "9.00"
        Me.txtKosuu.Top = 0.18!
        Me.txtKosuu.Width = 0.5!
        '
        'txtTani2
        '
        Me.txtTani2.Border.BottomStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtTani2.Border.RightStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid
        Me.txtTani2.DataField = "単位"
        Me.txtTani2.Height = 0.18!
        Me.txtTani2.Left = 3.242!
        Me.txtTani2.Name = "txtTani2"
        Me.txtTani2.Padding = New GrapeCity.ActiveReports.PaddingEx(0, 0, 3, 0)
        Me.txtTani2.Style = "font-family: ＭＳ 明朝; font-size: 9.75pt; text-align: right; vertical-align: bottom"
        Me.txtTani2.Text = "Kg"
        Me.txtTani2.Top = 0.18!
        Me.txtTani2.Width = 0.25!
        '
        'H01R30S_Nouhinsho
        '
        Me.MasterReport = False
        Me.PageSettings.PaperHeight = 11.0!
        Me.PageSettings.PaperWidth = 8.5!
        Me.PrintWidth = 7.5!
        Me.Sections.Add(Me.Detail)
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-style: normal; text-decoration: none; font-weight: normal; font-size: 10pt; " &
            "color: Black; font-family: ""MS UI Gothic""; ddo-char-set: 128", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold; font-family: ""MS UI Gothic""; ddo-char-set: 12" &
            "8", "Heading1", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 14pt; font-weight: bold; font-style: inherit; font-family: ""MS UI Goth" &
            "ic""; ddo-char-set: 128", "Heading2", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold; ddo-char-set: 128", "Heading3", "Normal"))
        CType(Me.txtGoodsNm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtIrisu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKingaku, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBiko, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTani1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtGoodsCd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblReitouKb, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSuuryo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTanka, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKosuu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTani2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

    Private WithEvents txtGoodsNm As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtIrisu As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtKingaku As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtBiko As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtTani1 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtGoodsCd As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents lblReitouKb As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txtSuuryo As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtTanka As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtKosuu As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtTani2 As GrapeCity.ActiveReports.SectionReportModel.TextBox
End Class
