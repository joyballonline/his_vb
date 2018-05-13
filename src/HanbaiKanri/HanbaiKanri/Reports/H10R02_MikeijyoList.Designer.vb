<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class H10R02_MikeijyoList
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
    Private WithEvents lstl明細 As GrapeCity.ActiveReports.SectionReportModel.Detail
    Private WithEvents PageFooter As GrapeCity.ActiveReports.SectionReportModel.PageFooter
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(H10R02_MikeijyoList))
        Me.PageHeader = New GrapeCity.ActiveReports.SectionReportModel.PageHeader()
        Me.txtPrm = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.Label2 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt区分ヘッダ = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label1 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt出荷日ヘッダ = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label6 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label7 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label8 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label9 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label10 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label14 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label16 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label18 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label21 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label24 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label41 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label43 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt作成日ヘッダ = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.ReportInfo1 = New GrapeCity.ActiveReports.SectionReportModel.ReportInfo()
        Me.Line3 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.Label4 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label12 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label17 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.lstl明細 = New GrapeCity.ActiveReports.SectionReportModel.Detail()
        Me.txt商品名 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt単位委託数量 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt単位売上数量 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt単位目切数量 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt単位残数量 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt入数 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt個数 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt委託数量 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt売上数量 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt目切数量 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt残数量 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt仮単価 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt残金額 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.PageFooter = New GrapeCity.ActiveReports.SectionReportModel.PageFooter()
        Me.txt会社名 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Line2 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.ReportHeader1 = New GrapeCity.ActiveReports.SectionReportModel.ReportHeader()
        Me.ReportFooter1 = New GrapeCity.ActiveReports.SectionReportModel.ReportFooter()
        Me.txt合計残金額 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.Label35 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label39 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt明細件数 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Line5 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.gh伝票 = New GrapeCity.ActiveReports.SectionReportModel.GroupHeader()
        Me.txt着日 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt伝票番号 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt出荷日 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt出荷先名 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt請求先ID = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt請求先名 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Line1 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.Line4 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.gf伝票 = New GrapeCity.ActiveReports.SectionReportModel.GroupFooter()
        Me.fid伝票番号 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld入数 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld個数 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld委託数量 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld売上数量 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld目切数量 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld残数量 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld仮単価 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld残金額 = New GrapeCity.ActiveReports.Data.Field()
        CType(Me.txtPrm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt区分ヘッダ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt出荷日ヘッダ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label41, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label43, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt作成日ヘッダ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReportInfo1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt商品名, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt単位委託数量, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt単位売上数量, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt単位目切数量, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt単位残数量, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt入数, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt個数, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt委託数量, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt売上数量, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt目切数量, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt残数量, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt仮単価, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt残金額, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt会社名, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt合計残金額, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label35, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label39, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt明細件数, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt着日, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt伝票番号, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt出荷日, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt出荷先名, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt請求先ID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt請求先名, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.txtPrm, Me.Label2, Me.txt区分ヘッダ, Me.Label1, Me.txt出荷日ヘッダ, Me.Label6, Me.Label7, Me.Label8, Me.Label9, Me.Label10, Me.Label14, Me.Label16, Me.Label18, Me.Label21, Me.Label24, Me.Label41, Me.Label43, Me.txt作成日ヘッダ, Me.ReportInfo1, Me.Line3, Me.Label4, Me.Label12, Me.Label17})
        Me.PageHeader.Height = 0.6600001!
        Me.PageHeader.Name = "PageHeader"
        '
        'txtPrm
        '
        Me.txtPrm.Height = 0.252!
        Me.txtPrm.Left = 0.1!
        Me.txtPrm.Name = "txtPrm"
        Me.txtPrm.Style = "font-family: ＭＳ ゴシック; font-size: 15.75pt; font-style: italic; font-weight: bold"
        Me.txtPrm.Text = "売上未計上一覧表"
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
        Me.txt区分ヘッダ.MultiLine = False
        Me.txt区分ヘッダ.Name = "txt区分ヘッダ"
        Me.txt区分ヘッダ.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left"
        Me.txt区分ヘッダ.Text = "売上"
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
        Me.Label1.Text = "[出荷日]"
        Me.Label1.Top = 0!
        Me.Label1.Width = 1.774!
        '
        'txt出荷日ヘッダ
        '
        Me.txt出荷日ヘッダ.Height = 0.15!
        Me.txt出荷日ヘッダ.HyperLink = Nothing
        Me.txt出荷日ヘッダ.Left = 2.882!
        Me.txt出荷日ヘッダ.MultiLine = False
        Me.txt出荷日ヘッダ.Name = "txt出荷日ヘッダ"
        Me.txt出荷日ヘッダ.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left"
        Me.txt出荷日ヘッダ.Text = "2018/02/20 ～ 2018/02/20"
        Me.txt出荷日ヘッダ.Top = 0.15!
        Me.txt出荷日ヘッダ.Width = 1.774!
        '
        'Label6
        '
        Me.Label6.Height = 0.15!
        Me.Label6.HyperLink = Nothing
        Me.Label6.Left = 0.7780001!
        Me.Label6.Name = "Label6"
        Me.Label6.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: top"
        Me.Label6.Text = "伝票番号"
        Me.Label6.Top = 0.352!
        Me.Label6.Width = 0.5630001!
        '
        'Label7
        '
        Me.Label7.Height = 0.15!
        Me.Label7.HyperLink = Nothing
        Me.Label7.Left = 0.028!
        Me.Label7.Name = "Label7"
        Me.Label7.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: top"
        Me.Label7.Text = "出荷日"
        Me.Label7.Top = 0.352!
        Me.Label7.Width = 0.5630001!
        '
        'Label8
        '
        Me.Label8.Height = 0.15!
        Me.Label8.HyperLink = Nothing
        Me.Label8.Left = 1.5!
        Me.Label8.Name = "Label8"
        Me.Label8.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: top"
        Me.Label8.Text = "出荷先名"
        Me.Label8.Top = 0.352!
        Me.Label8.Width = 0.751!
        '
        'Label9
        '
        Me.Label9.Height = 0.15!
        Me.Label9.HyperLink = Nothing
        Me.Label9.Left = 9.94!
        Me.Label9.Name = "Label9"
        Me.Label9.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: top"
        Me.Label9.Text = "着日"
        Me.Label9.Top = 0.352!
        Me.Label9.Width = 0.731!
        '
        'Label10
        '
        Me.Label10.Height = 0.15!
        Me.Label10.HyperLink = Nothing
        Me.Label10.Left = 0.489!
        Me.Label10.Name = "Label10"
        Me.Label10.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: top"
        Me.Label10.Text = "商品名"
        Me.Label10.Top = 0.5019999!
        Me.Label10.Width = 0.751!
        '
        'Label14
        '
        Me.Label14.Height = 0.15!
        Me.Label14.HyperLink = Nothing
        Me.Label14.Left = 3.626!
        Me.Label14.Name = "Label14"
        Me.Label14.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label14.Text = "入数"
        Me.Label14.Top = 0.502!
        Me.Label14.Width = 0.4990005!
        '
        'Label16
        '
        Me.Label16.Height = 0.15!
        Me.Label16.HyperLink = Nothing
        Me.Label16.Left = 4.125!
        Me.Label16.Name = "Label16"
        Me.Label16.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label16.Text = "個数"
        Me.Label16.Top = 0.502!
        Me.Label16.Width = 0.5539999!
        '
        'Label18
        '
        Me.Label18.Height = 0.15!
        Me.Label18.HyperLink = Nothing
        Me.Label18.Left = 4.679!
        Me.Label18.Name = "Label18"
        Me.Label18.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label18.Text = "委託数量"
        Me.Label18.Top = 0.502!
        Me.Label18.Width = 0.6860003!
        '
        'Label21
        '
        Me.Label21.Height = 0.15!
        Me.Label21.HyperLink = Nothing
        Me.Label21.Left = 8.267!
        Me.Label21.Name = "Label21"
        Me.Label21.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label21.Text = "仮単価"
        Me.Label21.Top = 0.5019999!
        Me.Label21.Width = 0.789!
        '
        'Label24
        '
        Me.Label24.Height = 0.15!
        Me.Label24.HyperLink = Nothing
        Me.Label24.Left = 9.056001!
        Me.Label24.Name = "Label24"
        Me.Label24.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label24.Text = "残金額"
        Me.Label24.Top = 0.5019999!
        Me.Label24.Width = 0.747!
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
        Me.txt作成日ヘッダ.MultiLine = False
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
        'Label4
        '
        Me.Label4.Height = 0.15!
        Me.Label4.HyperLink = Nothing
        Me.Label4.Left = 5.576!
        Me.Label4.Name = "Label4"
        Me.Label4.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label4.Text = "売上数量"
        Me.Label4.Top = 0.502!
        Me.Label4.Width = 0.6860003!
        '
        'Label12
        '
        Me.Label12.Height = 0.15!
        Me.Label12.HyperLink = Nothing
        Me.Label12.Left = 6.473001!
        Me.Label12.Name = "Label12"
        Me.Label12.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label12.Text = "目切数量"
        Me.Label12.Top = 0.502!
        Me.Label12.Width = 0.6860003!
        '
        'Label17
        '
        Me.Label17.Height = 0.15!
        Me.Label17.HyperLink = Nothing
        Me.Label17.Left = 7.37!
        Me.Label17.Name = "Label17"
        Me.Label17.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label17.Text = "残数量"
        Me.Label17.Top = 0.502!
        Me.Label17.Width = 0.6860003!
        '
        'lstl明細
        '
        Me.lstl明細.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.txt商品名, Me.txt単位委託数量, Me.txt単位売上数量, Me.txt単位目切数量, Me.txt単位残数量, Me.txt入数, Me.txt個数, Me.txt委託数量, Me.txt売上数量, Me.txt目切数量, Me.txt残数量, Me.txt仮単価, Me.txt残金額})
        Me.lstl明細.Height = 0.16!
        Me.lstl明細.Name = "lstl明細"
        '
        'txt商品名
        '
        Me.txt商品名.Height = 0.16!
        Me.txt商品名.HyperLink = Nothing
        Me.txt商品名.Left = 0.489!
        Me.txt商品名.MultiLine = False
        Me.txt商品名.Name = "txt商品名"
        Me.txt商品名.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left; vertical-align: middle"
        Me.txt商品名.Text = "ぶっかけめかぶバラ(宮城・岩手産) 1kgx3x2"
        Me.txt商品名.Top = 0!
        Me.txt商品名.Width = 3.137!
        '
        'txt単位委託数量
        '
        Me.txt単位委託数量.Height = 0.16!
        Me.txt単位委託数量.HyperLink = Nothing
        Me.txt単位委託数量.Left = 5.365!
        Me.txt単位委託数量.MultiLine = False
        Me.txt単位委託数量.Name = "txt単位委託数量"
        Me.txt単位委託数量.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: center; vertical-align: middle"
        Me.txt単位委託数量.Text = "個"
        Me.txt単位委託数量.Top = 0!
        Me.txt単位委託数量.Width = 0.211!
        '
        'txt単位売上数量
        '
        Me.txt単位売上数量.Height = 0.16!
        Me.txt単位売上数量.HyperLink = Nothing
        Me.txt単位売上数量.Left = 6.262!
        Me.txt単位売上数量.MultiLine = False
        Me.txt単位売上数量.Name = "txt単位売上数量"
        Me.txt単位売上数量.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: center; vertical-align: middle"
        Me.txt単位売上数量.Text = "個"
        Me.txt単位売上数量.Top = 0!
        Me.txt単位売上数量.Width = 0.211!
        '
        'txt単位目切数量
        '
        Me.txt単位目切数量.Height = 0.16!
        Me.txt単位目切数量.HyperLink = Nothing
        Me.txt単位目切数量.Left = 7.159001!
        Me.txt単位目切数量.MultiLine = False
        Me.txt単位目切数量.Name = "txt単位目切数量"
        Me.txt単位目切数量.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: center; vertical-align: middle"
        Me.txt単位目切数量.Text = "個"
        Me.txt単位目切数量.Top = 0!
        Me.txt単位目切数量.Width = 0.211!
        '
        'txt単位残数量
        '
        Me.txt単位残数量.Height = 0.16!
        Me.txt単位残数量.HyperLink = Nothing
        Me.txt単位残数量.Left = 8.056001!
        Me.txt単位残数量.MultiLine = False
        Me.txt単位残数量.Name = "txt単位残数量"
        Me.txt単位残数量.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: center; vertical-align: middle"
        Me.txt単位残数量.Text = "個"
        Me.txt単位残数量.Top = 0!
        Me.txt単位残数量.Width = 0.211!
        '
        'txt入数
        '
        Me.txt入数.DataField = "fld入数"
        Me.txt入数.Height = 0.16!
        Me.txt入数.Left = 3.626!
        Me.txt入数.MultiLine = False
        Me.txt入数.Name = "txt入数"
        Me.txt入数.OutputFormat = resources.GetString("txt入数.OutputFormat")
        Me.txt入数.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt入数.Text = "60.00"
        Me.txt入数.Top = 0!
        Me.txt入数.Width = 0.499!
        '
        'txt個数
        '
        Me.txt個数.DataField = "fld個数"
        Me.txt個数.Height = 0.16!
        Me.txt個数.Left = 4.125!
        Me.txt個数.MultiLine = False
        Me.txt個数.Name = "txt個数"
        Me.txt個数.OutputFormat = resources.GetString("txt個数.OutputFormat")
        Me.txt個数.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt個数.Text = "123"
        Me.txt個数.Top = 0!
        Me.txt個数.Width = 0.554!
        '
        'txt委託数量
        '
        Me.txt委託数量.DataField = "fld委託数量"
        Me.txt委託数量.Height = 0.16!
        Me.txt委託数量.Left = 4.679!
        Me.txt委託数量.MultiLine = False
        Me.txt委託数量.Name = "txt委託数量"
        Me.txt委託数量.OutputFormat = resources.GetString("txt委託数量.OutputFormat")
        Me.txt委託数量.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt委託数量.Text = "12,345.00"
        Me.txt委託数量.Top = 0!
        Me.txt委託数量.Width = 0.686!
        '
        'txt売上数量
        '
        Me.txt売上数量.DataField = "fld売上数量"
        Me.txt売上数量.Height = 0.16!
        Me.txt売上数量.Left = 5.576!
        Me.txt売上数量.MultiLine = False
        Me.txt売上数量.Name = "txt売上数量"
        Me.txt売上数量.OutputFormat = resources.GetString("txt売上数量.OutputFormat")
        Me.txt売上数量.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt売上数量.Text = "12,345.00"
        Me.txt売上数量.Top = 0!
        Me.txt売上数量.Width = 0.686!
        '
        'txt目切数量
        '
        Me.txt目切数量.DataField = "fld目切数量"
        Me.txt目切数量.Height = 0.16!
        Me.txt目切数量.Left = 6.473!
        Me.txt目切数量.MultiLine = False
        Me.txt目切数量.Name = "txt目切数量"
        Me.txt目切数量.OutputFormat = resources.GetString("txt目切数量.OutputFormat")
        Me.txt目切数量.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt目切数量.Text = "12,345.00"
        Me.txt目切数量.Top = 0!
        Me.txt目切数量.Width = 0.686!
        '
        'txt残数量
        '
        Me.txt残数量.DataField = "fld残数量"
        Me.txt残数量.Height = 0.16!
        Me.txt残数量.Left = 7.37!
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
        Me.txt仮単価.DataField = "fld仮単価"
        Me.txt仮単価.Height = 0.16!
        Me.txt仮単価.Left = 8.267!
        Me.txt仮単価.MultiLine = False
        Me.txt仮単価.Name = "txt仮単価"
        Me.txt仮単価.OutputFormat = resources.GetString("txt仮単価.OutputFormat")
        Me.txt仮単価.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt仮単価.Text = "12,345.00"
        Me.txt仮単価.Top = 0!
        Me.txt仮単価.Width = 0.789!
        '
        'txt残金額
        '
        Me.txt残金額.DataField = "fld残金額"
        Me.txt残金額.Height = 0.16!
        Me.txt残金額.Left = 9.056!
        Me.txt残金額.MultiLine = False
        Me.txt残金額.Name = "txt残金額"
        Me.txt残金額.OutputFormat = resources.GetString("txt残金額.OutputFormat")
        Me.txt残金額.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt残金額.Text = "30,240"
        Me.txt残金額.Top = 0!
        Me.txt残金額.Width = 0.747!
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.txt会社名, Me.Line2})
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
        'Line2
        '
        Me.Line2.Height = 0!
        Me.Line2.Left = 0!
        Me.Line2.LineWeight = 2.0!
        Me.Line2.Name = "Line2"
        Me.Line2.Top = 0!
        Me.Line2.Width = 10.69!
        Me.Line2.X1 = 0!
        Me.Line2.X2 = 10.69!
        Me.Line2.Y1 = 0!
        Me.Line2.Y2 = 0!
        '
        'ReportHeader1
        '
        Me.ReportHeader1.Height = 0!
        Me.ReportHeader1.Name = "ReportHeader1"
        Me.ReportHeader1.Visible = False
        '
        'ReportFooter1
        '
        Me.ReportFooter1.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.txt合計残金額, Me.Label35, Me.Label39, Me.txt明細件数, Me.Line5})
        Me.ReportFooter1.Height = 0.16!
        Me.ReportFooter1.Name = "ReportFooter1"
        '
        'txt合計残金額
        '
        Me.txt合計残金額.DataField = "fld残金額"
        Me.txt合計残金額.Height = 0.16!
        Me.txt合計残金額.Left = 9.056!
        Me.txt合計残金額.MultiLine = False
        Me.txt合計残金額.Name = "txt合計残金額"
        Me.txt合計残金額.OutputFormat = resources.GetString("txt合計残金額.OutputFormat")
        Me.txt合計残金額.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt合計残金額.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.All
        Me.txt合計残金額.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.GrandTotal
        Me.txt合計残金額.Text = "12,345,678"
        Me.txt合計残金額.Top = 0!
        Me.txt合計残金額.Width = 0.747!
        '
        'Label35
        '
        Me.Label35.Height = 0.16!
        Me.Label35.HyperLink = Nothing
        Me.Label35.Left = 8.056001!
        Me.Label35.Name = "Label35"
        Me.Label35.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: middle"
        Me.Label35.Text = "★★[総合計]"
        Me.Label35.Top = 0!
        Me.Label35.Width = 0.999999!
        '
        'Label39
        '
        Me.Label39.Height = 0.16!
        Me.Label39.HyperLink = Nothing
        Me.Label39.Left = 0.028!
        Me.Label39.Name = "Label39"
        Me.Label39.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: middle"
        Me.Label39.Text = "[明細]"
        Me.Label39.Top = 0!
        Me.Label39.Width = 0.4789999!
        '
        'txt明細件数
        '
        Me.txt明細件数.Height = 0.16!
        Me.txt明細件数.HyperLink = Nothing
        Me.txt明細件数.Left = 0.5069996!
        Me.txt明細件数.Name = "txt明細件数"
        Me.txt明細件数.Style = "font-family: ＭＳ 明朝; font-size: 9pt; font-weight: normal; text-align: left; vertic" &
    "al-align: middle"
        Me.txt明細件数.Text = "9件"
        Me.txt明細件数.Top = 0!
        Me.txt明細件数.Width = 0.7330004!
        '
        'Line5
        '
        Me.Line5.Height = 0!
        Me.Line5.Left = 0!
        Me.Line5.LineWeight = 2.0!
        Me.Line5.Name = "Line5"
        Me.Line5.Top = 0!
        Me.Line5.Width = 10.69!
        Me.Line5.X1 = 0!
        Me.Line5.X2 = 10.69!
        Me.Line5.Y1 = 0!
        Me.Line5.Y2 = 0!
        '
        'gh伝票
        '
        Me.gh伝票.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.txt着日, Me.txt伝票番号, Me.txt出荷日, Me.txt出荷先名, Me.txt請求先ID, Me.txt請求先名, Me.Line1, Me.Line4})
        Me.gh伝票.DataField = "fid伝票番号"
        Me.gh伝票.Height = 0.16!
        Me.gh伝票.Name = "gh伝票"
        '
        'txt着日
        '
        Me.txt着日.Height = 0.16!
        Me.txt着日.Left = 9.931!
        Me.txt着日.MultiLine = False
        Me.txt着日.Name = "txt着日"
        Me.txt着日.OutputFormat = resources.GetString("txt着日.OutputFormat")
        Me.txt着日.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; text-align: left; vertical-align: middle"
        Me.txt着日.Text = "2018/02/20"
        Me.txt着日.Top = 0!
        Me.txt着日.Width = 0.731!
        '
        'txt伝票番号
        '
        Me.txt伝票番号.Height = 0.16!
        Me.txt伝票番号.HyperLink = Nothing
        Me.txt伝票番号.Left = 0.7780001!
        Me.txt伝票番号.MultiLine = False
        Me.txt伝票番号.Name = "txt伝票番号"
        Me.txt伝票番号.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left; vertical-align: middle"
        Me.txt伝票番号.Text = "U000000"
        Me.txt伝票番号.Top = 0!
        Me.txt伝票番号.Width = 0.5630001!
        '
        'txt出荷日
        '
        Me.txt出荷日.Height = 0.16!
        Me.txt出荷日.HyperLink = Nothing
        Me.txt出荷日.Left = 0.028!
        Me.txt出荷日.MultiLine = False
        Me.txt出荷日.Name = "txt出荷日"
        Me.txt出荷日.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left; vertical-align: middle"
        Me.txt出荷日.Text = "2018/02/20"
        Me.txt出荷日.Top = 0!
        Me.txt出荷日.Width = 0.75!
        '
        'txt出荷先名
        '
        Me.txt出荷先名.Height = 0.16!
        Me.txt出荷先名.HyperLink = Nothing
        Me.txt出荷先名.Left = 1.491!
        Me.txt出荷先名.MultiLine = False
        Me.txt出荷先名.Name = "txt出荷先名"
        Me.txt出荷先名.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left; vertical-align: middle"
        Me.txt出荷先名.Text = "㈱東成流通サービス"
        Me.txt出荷先名.Top = 0!
        Me.txt出荷先名.Width = 1.916!
        '
        'txt請求先ID
        '
        Me.txt請求先ID.Height = 0.16!
        Me.txt請求先ID.HyperLink = Nothing
        Me.txt請求先ID.Left = 3.407!
        Me.txt請求先ID.MultiLine = False
        Me.txt請求先ID.Name = "txt請求先ID"
        Me.txt請求先ID.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; text-align: left; vertical-align: middle"
        Me.txt請求先ID.Text = "[請求先]"
        Me.txt請求先ID.Top = 0!
        Me.txt請求先ID.Width = 0.6020007!
        '
        'txt請求先名
        '
        Me.txt請求先名.Height = 0.16!
        Me.txt請求先名.HyperLink = Nothing
        Me.txt請求先名.Left = 4.009!
        Me.txt請求先名.MultiLine = False
        Me.txt請求先名.Name = "txt請求先名"
        Me.txt請求先名.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left; vertical-align: middle"
        Me.txt請求先名.Text = "株式会社　丸中　二階堂"
        Me.txt請求先名.Top = 0!
        Me.txt請求先名.Width = 2.104!
        '
        'Line1
        '
        Me.Line1.Height = 0!
        Me.Line1.Left = 0!
        Me.Line1.LineWeight = 1.0!
        Me.Line1.Name = "Line1"
        Me.Line1.Top = 0!
        Me.Line1.Width = 10.69!
        Me.Line1.X1 = 0!
        Me.Line1.X2 = 10.69!
        Me.Line1.Y1 = 0!
        Me.Line1.Y2 = 0!
        '
        'Line4
        '
        Me.Line4.Height = 0!
        Me.Line4.Left = 0.558!
        Me.Line4.LineStyle = GrapeCity.ActiveReports.SectionReportModel.LineStyle.Dot
        Me.Line4.LineWeight = 1.0!
        Me.Line4.Name = "Line4"
        Me.Line4.Top = 0.16!
        Me.Line4.Width = 10.09!
        Me.Line4.X1 = 0.558!
        Me.Line4.X2 = 10.648!
        Me.Line4.Y1 = 0.16!
        Me.Line4.Y2 = 0.16!
        '
        'gf伝票
        '
        Me.gf伝票.Height = 0!
        Me.gf伝票.Name = "gf伝票"
        Me.gf伝票.Visible = False
        '
        'fid伝票番号
        '
        Me.fid伝票番号.DefaultValue = Nothing
        Me.fid伝票番号.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fid伝票番号.Formula = Nothing
        Me.fid伝票番号.Name = "fid伝票番号"
        Me.fid伝票番号.Tag = Nothing
        '
        'fld入数
        '
        Me.fld入数.DefaultValue = Nothing
        Me.fld入数.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld入数.Formula = Nothing
        Me.fld入数.Name = "fld入数"
        Me.fld入数.Tag = Nothing
        '
        'fld個数
        '
        Me.fld個数.DefaultValue = Nothing
        Me.fld個数.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld個数.Formula = Nothing
        Me.fld個数.Name = "fld個数"
        Me.fld個数.Tag = Nothing
        '
        'fld委託数量
        '
        Me.fld委託数量.DefaultValue = Nothing
        Me.fld委託数量.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld委託数量.Formula = Nothing
        Me.fld委託数量.Name = "fld委託数量"
        Me.fld委託数量.Tag = Nothing
        '
        'fld売上数量
        '
        Me.fld売上数量.DefaultValue = Nothing
        Me.fld売上数量.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld売上数量.Formula = Nothing
        Me.fld売上数量.Name = "fld売上数量"
        Me.fld売上数量.Tag = Nothing
        '
        'fld目切数量
        '
        Me.fld目切数量.DefaultValue = Nothing
        Me.fld目切数量.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld目切数量.Formula = Nothing
        Me.fld目切数量.Name = "fld目切数量"
        Me.fld目切数量.Tag = Nothing
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
        'fld残金額
        '
        Me.fld残金額.DefaultValue = Nothing
        Me.fld残金額.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld残金額.Formula = Nothing
        Me.fld残金額.Name = "fld残金額"
        Me.fld残金額.Tag = Nothing
        '
        'H10R02_MikeijyoList
        '
        Me.MasterReport = False
        Me.CalculatedFields.Add(Me.fid伝票番号)
        Me.CalculatedFields.Add(Me.fld入数)
        Me.CalculatedFields.Add(Me.fld個数)
        Me.CalculatedFields.Add(Me.fld委託数量)
        Me.CalculatedFields.Add(Me.fld売上数量)
        Me.CalculatedFields.Add(Me.fld目切数量)
        Me.CalculatedFields.Add(Me.fld残数量)
        Me.CalculatedFields.Add(Me.fld仮単価)
        Me.CalculatedFields.Add(Me.fld残金額)
        Me.PageSettings.Margins.Bottom = 0.2!
        Me.PageSettings.Margins.Left = 0.5!
        Me.PageSettings.Margins.Right = 0.5!
        Me.PageSettings.Margins.Top = 0.5!
        Me.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape
        Me.PageSettings.PaperHeight = 11.0!
        Me.PageSettings.PaperWidth = 8.5!
        Me.PrintWidth = 10.69!
        Me.Sections.Add(Me.ReportHeader1)
        Me.Sections.Add(Me.PageHeader)
        Me.Sections.Add(Me.gh伝票)
        Me.Sections.Add(Me.lstl明細)
        Me.Sections.Add(Me.gf伝票)
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
        CType(Me.txt出荷日ヘッダ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label41, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label43, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt作成日ヘッダ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReportInfo1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt商品名, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt単位委託数量, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt単位売上数量, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt単位目切数量, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt単位残数量, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt入数, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt個数, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt委託数量, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt売上数量, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt目切数量, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt残数量, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt仮単価, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt残金額, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt会社名, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt合計残金額, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label35, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label39, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt明細件数, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt着日, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt伝票番号, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt出荷日, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt出荷先名, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt請求先ID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt請求先名, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

    Private WithEvents ReportHeader1 As GrapeCity.ActiveReports.SectionReportModel.ReportHeader
    Private WithEvents ReportFooter1 As GrapeCity.ActiveReports.SectionReportModel.ReportFooter
    Private WithEvents gh伝票 As GrapeCity.ActiveReports.SectionReportModel.GroupHeader
    Private WithEvents gf伝票 As GrapeCity.ActiveReports.SectionReportModel.GroupFooter
    Private WithEvents txtPrm As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents Label2 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt区分ヘッダ As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label1 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt出荷日ヘッダ As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label6 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label7 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label8 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label9 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label10 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label14 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label16 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label18 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label21 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label24 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label41 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label43 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt作成日ヘッダ As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents ReportInfo1 As GrapeCity.ActiveReports.SectionReportModel.ReportInfo
    Private WithEvents Line3 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents txt伝票番号 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt出荷日 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt出荷先名 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt請求先ID As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt請求先名 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Line1 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents txt商品名 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt単位委託数量 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Line4 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents txt会社名 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label35 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label39 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt明細件数 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Line5 As GrapeCity.ActiveReports.SectionReportModel.Line
    Friend WithEvents fid伝票番号 As GrapeCity.ActiveReports.Data.Field
    Private WithEvents Label4 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label12 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label17 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt単位売上数量 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt単位目切数量 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt単位残数量 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Line2 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents txt着日 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt入数 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt個数 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt委託数量 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt売上数量 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt目切数量 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt残数量 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt仮単価 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt残金額 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt合計残金額 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Friend WithEvents fld入数 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld個数 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld委託数量 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld売上数量 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld目切数量 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld残数量 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld仮単価 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld残金額 As GrapeCity.ActiveReports.Data.Field
End Class
