<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class H11R01_ShiireList
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
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(H11R01_ShiireList))
        Me.PageHeader = New GrapeCity.ActiveReports.SectionReportModel.PageHeader()
        Me.txtPrm = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.Label2 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt区分ヘッダ = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label1 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt仕入先ヘッダ = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label4 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt伝票番号ヘッダ = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label6 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label7 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label8 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label10 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label12 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label14 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label16 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label18 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label19 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label20 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label21 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label24 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label41 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label43 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt作成日ヘッダ = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.ReportInfo1 = New GrapeCity.ActiveReports.SectionReportModel.ReportInfo()
        Me.Line3 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.Line6 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.Detail = New GrapeCity.ActiveReports.SectionReportModel.Detail()
        Me.txt商品名 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt税区分 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt単位 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt明細備考 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt入数 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt個数 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt数量 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt仕入単価 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt仕入金額 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.PageFooter = New GrapeCity.ActiveReports.SectionReportModel.PageFooter()
        Me.txt会社名 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.ReportHeader1 = New GrapeCity.ActiveReports.SectionReportModel.ReportHeader()
        Me.ReportFooter1 = New GrapeCity.ActiveReports.SectionReportModel.ReportFooter()
        Me.txt合計金額ページ = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt消費税ページ = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt仕入金額ページ = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.Label35 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label37 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt伝票件数 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label39 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt明細件数 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Line5 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.gh伝票 = New GrapeCity.ActiveReports.SectionReportModel.GroupHeader()
        Me.txt伝票番号 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt仕入日 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt仕入先名 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt支払先ID = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt支払先名 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Line1 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.Line2 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.gf伝票 = New GrapeCity.ActiveReports.SectionReportModel.GroupFooter()
        Me.txt合計金額グループ = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt消費税グループ = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt仕入金額グループ = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.Label31 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Line4 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.fid伝票番号 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld入数 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld個数 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld数量 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld仕入単価 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld仕入金額 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld仕入金額グループ = New GrapeCity.ActiveReports.Data.Field()
        Me.fld消費税グループ = New GrapeCity.ActiveReports.Data.Field()
        Me.fld合計金額グループ = New GrapeCity.ActiveReports.Data.Field()
        Me.fld仕入金額ページ = New GrapeCity.ActiveReports.Data.Field()
        Me.fld消費税ページ = New GrapeCity.ActiveReports.Data.Field()
        Me.fld合計金額ページ = New GrapeCity.ActiveReports.Data.Field()
        Me.Line7 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        CType(Me.txtPrm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt区分ヘッダ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt仕入先ヘッダ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt伝票番号ヘッダ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label41, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label43, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt作成日ヘッダ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReportInfo1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt商品名, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt税区分, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt単位, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt明細備考, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt入数, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt個数, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt数量, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt仕入単価, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt仕入金額, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt会社名, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt合計金額ページ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt消費税ページ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt仕入金額ページ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label35, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label37, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt伝票件数, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label39, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt明細件数, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt伝票番号, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt仕入日, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt仕入先名, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt支払先ID, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt支払先名, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt合計金額グループ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt消費税グループ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt仕入金額グループ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.txtPrm, Me.Label2, Me.txt区分ヘッダ, Me.Label1, Me.txt仕入先ヘッダ, Me.Label4, Me.txt伝票番号ヘッダ, Me.Label6, Me.Label7, Me.Label8, Me.Label10, Me.Label12, Me.Label14, Me.Label16, Me.Label18, Me.Label19, Me.Label20, Me.Label21, Me.Label24, Me.Label41, Me.Label43, Me.txt作成日ヘッダ, Me.ReportInfo1, Me.Line3, Me.Line6})
        Me.PageHeader.Height = 0.6770833!
        Me.PageHeader.Name = "PageHeader"
        '
        'txtPrm
        '
        Me.txtPrm.Height = 0.252!
        Me.txtPrm.Left = 0.1!
        Me.txtPrm.Name = "txtPrm"
        Me.txtPrm.Style = "font-family: ＭＳ ゴシック; font-size: 15.75pt; font-style: italic; font-weight: bold"
        Me.txtPrm.Text = "仕入明細表"
        Me.txtPrm.Top = 0.01254168!
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
        Me.Label2.Top = 0.01254168!
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
        Me.txt区分ヘッダ.Top = 0.1625417!
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
        Me.Label1.Text = "[仕入日]"
        Me.Label1.Top = 0.01254168!
        Me.Label1.Width = 1.774!
        '
        'txt仕入先ヘッダ
        '
        Me.txt仕入先ヘッダ.Height = 0.15!
        Me.txt仕入先ヘッダ.HyperLink = Nothing
        Me.txt仕入先ヘッダ.Left = 2.882!
        Me.txt仕入先ヘッダ.MultiLine = False
        Me.txt仕入先ヘッダ.Name = "txt仕入先ヘッダ"
        Me.txt仕入先ヘッダ.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left"
        Me.txt仕入先ヘッダ.Text = "2018/02/20 ～ 2018/02/20"
        Me.txt仕入先ヘッダ.Top = 0.1625417!
        Me.txt仕入先ヘッダ.Width = 1.774!
        '
        'Label4
        '
        Me.Label4.Height = 0.16!
        Me.Label4.HyperLink = Nothing
        Me.Label4.Left = 4.655999!
        Me.Label4.Name = "Label4"
        Me.Label4.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: top"
        Me.Label4.Text = "[伝票番号]"
        Me.Label4.Top = 0.01254168!
        Me.Label4.Width = 0.7!
        '
        'txt伝票番号ヘッダ
        '
        Me.txt伝票番号ヘッダ.Height = 0.15!
        Me.txt伝票番号ヘッダ.HyperLink = Nothing
        Me.txt伝票番号ヘッダ.Left = 4.655999!
        Me.txt伝票番号ヘッダ.MultiLine = False
        Me.txt伝票番号ヘッダ.Name = "txt伝票番号ヘッダ"
        Me.txt伝票番号ヘッダ.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left"
        Me.txt伝票番号ヘッダ.Text = "R000000 ～ R999999"
        Me.txt伝票番号ヘッダ.Top = 0.1625417!
        Me.txt伝票番号ヘッダ.Width = 2.055!
        '
        'Label6
        '
        Me.Label6.Height = 0.15!
        Me.Label6.HyperLink = Nothing
        Me.Label6.Left = 0.037!
        Me.Label6.Name = "Label6"
        Me.Label6.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: top"
        Me.Label6.Text = "伝票番号"
        Me.Label6.Top = 0.3645417!
        Me.Label6.Width = 0.5630001!
        '
        'Label7
        '
        Me.Label7.Height = 0.15!
        Me.Label7.HyperLink = Nothing
        Me.Label7.Left = 0.6000001!
        Me.Label7.Name = "Label7"
        Me.Label7.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: top"
        Me.Label7.Text = "仕入日"
        Me.Label7.Top = 0.3645417!
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
        Me.Label8.Text = "仕入先名"
        Me.Label8.Top = 0.3645417!
        Me.Label8.Width = 0.751!
        '
        'Label10
        '
        Me.Label10.Height = 0.15!
        Me.Label10.HyperLink = Nothing
        Me.Label10.Left = 0.979!
        Me.Label10.Name = "Label10"
        Me.Label10.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: top"
        Me.Label10.Text = "商品名"
        Me.Label10.Top = 0.5145416!
        Me.Label10.Width = 0.751!
        '
        'Label12
        '
        Me.Label12.Height = 0.15!
        Me.Label12.HyperLink = Nothing
        Me.Label12.Left = 4.449!
        Me.Label12.Name = "Label12"
        Me.Label12.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: top"
        Me.Label12.Text = "税区分"
        Me.Label12.Top = 0.5145416!
        Me.Label12.Width = 0.4179998!
        '
        'Label14
        '
        Me.Label14.Height = 0.15!
        Me.Label14.HyperLink = Nothing
        Me.Label14.Left = 4.867!
        Me.Label14.Name = "Label14"
        Me.Label14.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label14.Text = "入数"
        Me.Label14.Top = 0.5145416!
        Me.Label14.Width = 0.4990005!
        '
        'Label16
        '
        Me.Label16.Height = 0.15!
        Me.Label16.HyperLink = Nothing
        Me.Label16.Left = 5.366!
        Me.Label16.Name = "Label16"
        Me.Label16.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label16.Text = "個数"
        Me.Label16.Top = 0.5145416!
        Me.Label16.Width = 0.5539999!
        '
        'Label18
        '
        Me.Label18.Height = 0.15!
        Me.Label18.HyperLink = Nothing
        Me.Label18.Left = 5.92!
        Me.Label18.Name = "Label18"
        Me.Label18.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label18.Text = "数量"
        Me.Label18.Top = 0.5145416!
        Me.Label18.Width = 0.6860003!
        '
        'Label19
        '
        Me.Label19.Height = 0.15!
        Me.Label19.HyperLink = Nothing
        Me.Label19.Left = 6.816999!
        Me.Label19.Name = "Label19"
        Me.Label19.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label19.Text = "仕入単価"
        Me.Label19.Top = 0.5145416!
        Me.Label19.Width = 0.789!
        '
        'Label20
        '
        Me.Label20.Height = 0.15!
        Me.Label20.HyperLink = Nothing
        Me.Label20.Left = 7.606!
        Me.Label20.Name = "Label20"
        Me.Label20.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label20.Text = "仕入金額"
        Me.Label20.Top = 0.5145416!
        Me.Label20.Width = 0.747!
        '
        'Label21
        '
        Me.Label21.Height = 0.15!
        Me.Label21.HyperLink = Nothing
        Me.Label21.Left = 8.353001!
        Me.Label21.Name = "Label21"
        Me.Label21.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label21.Text = "消費税"
        Me.Label21.Top = 0.5145416!
        Me.Label21.Width = 0.747!
        '
        'Label24
        '
        Me.Label24.Height = 0.15!
        Me.Label24.HyperLink = Nothing
        Me.Label24.Left = 9.1!
        Me.Label24.Name = "Label24"
        Me.Label24.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label24.Text = "合計金額"
        Me.Label24.Top = 0.5145416!
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
        Me.Label41.Top = 0.1625417!
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
        Me.Label43.Top = 0.1625417!
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
        Me.txt作成日ヘッダ.Top = 0.1625417!
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
        Me.ReportInfo1.Top = 0.1625417!
        Me.ReportInfo1.Width = 0.6289997!
        '
        'Line3
        '
        Me.Line3.Height = 0!
        Me.Line3.Left = 0!
        Me.Line3.LineWeight = 2.0!
        Me.Line3.Name = "Line3"
        Me.Line3.Top = 0.3325417!
        Me.Line3.Width = 10.69!
        Me.Line3.X1 = 0!
        Me.Line3.X2 = 10.69!
        Me.Line3.Y1 = 0.3325417!
        Me.Line3.Y2 = 0.3325417!
        '
        'Line6
        '
        Me.Line6.Height = 0!
        Me.Line6.Left = 0!
        Me.Line6.LineWeight = 1.0!
        Me.Line6.Name = "Line6"
        Me.Line6.Top = 0.665!
        Me.Line6.Visible = False
        Me.Line6.Width = 10.69!
        Me.Line6.X1 = 0!
        Me.Line6.X2 = 10.69!
        Me.Line6.Y1 = 0.665!
        Me.Line6.Y2 = 0.665!
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.txt商品名, Me.txt税区分, Me.txt単位, Me.txt明細備考, Me.txt入数, Me.txt個数, Me.txt数量, Me.txt仕入単価, Me.txt仕入金額})
        Me.Detail.Height = 0.16!
        Me.Detail.Name = "Detail"
        '
        'txt商品名
        '
        Me.txt商品名.Height = 0.16!
        Me.txt商品名.HyperLink = Nothing
        Me.txt商品名.Left = 0.979!
        Me.txt商品名.MultiLine = False
        Me.txt商品名.Name = "txt商品名"
        Me.txt商品名.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left; vertical-align: middle"
        Me.txt商品名.Text = "ぶっかけめかぶバラ(宮城・岩手産) 1kgx3x2"
        Me.txt商品名.Top = 0!
        Me.txt商品名.Width = 3.470001!
        '
        'txt税区分
        '
        Me.txt税区分.Height = 0.16!
        Me.txt税区分.HyperLink = Nothing
        Me.txt税区分.Left = 4.449!
        Me.txt税区分.MultiLine = False
        Me.txt税区分.Name = "txt税区分"
        Me.txt税区分.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left; vertical-align: middle"
        Me.txt税区分.Text = "外税"
        Me.txt税区分.Top = 0.01199996!
        Me.txt税区分.Width = 0.3989997!
        '
        'txt単位
        '
        Me.txt単位.Height = 0.16!
        Me.txt単位.HyperLink = Nothing
        Me.txt単位.Left = 6.606!
        Me.txt単位.MultiLine = False
        Me.txt単位.Name = "txt単位"
        Me.txt単位.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: center; vertical-align: middle"
        Me.txt単位.Text = "個"
        Me.txt単位.Top = 0!
        Me.txt単位.Width = 0.211!
        '
        'txt明細備考
        '
        Me.txt明細備考.Height = 0.16!
        Me.txt明細備考.HyperLink = Nothing
        Me.txt明細備考.Left = 8.732!
        Me.txt明細備考.MultiLine = False
        Me.txt明細備考.Name = "txt明細備考"
        Me.txt明細備考.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left; vertical-align: middle"
        Me.txt明細備考.Text = "※明細備考"
        Me.txt明細備考.Top = 0!
        Me.txt明細備考.Width = 1.939001!
        '
        'txt入数
        '
        Me.txt入数.DataField = "fld入数"
        Me.txt入数.Height = 0.16!
        Me.txt入数.Left = 4.848!
        Me.txt入数.MultiLine = False
        Me.txt入数.Name = "txt入数"
        Me.txt入数.OutputFormat = resources.GetString("txt入数.OutputFormat")
        Me.txt入数.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt入数.Text = "1234.00"
        Me.txt入数.Top = 0!
        Me.txt入数.Width = 0.499!
        '
        'txt個数
        '
        Me.txt個数.DataField = "fld個数"
        Me.txt個数.Height = 0.16!
        Me.txt個数.Left = 5.366!
        Me.txt個数.MultiLine = False
        Me.txt個数.Name = "txt個数"
        Me.txt個数.OutputFormat = resources.GetString("txt個数.OutputFormat")
        Me.txt個数.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt個数.Text = "123"
        Me.txt個数.Top = 0!
        Me.txt個数.Width = 0.554!
        '
        'txt数量
        '
        Me.txt数量.DataField = "fld数量"
        Me.txt数量.Height = 0.16!
        Me.txt数量.Left = 5.92!
        Me.txt数量.MultiLine = False
        Me.txt数量.Name = "txt数量"
        Me.txt数量.OutputFormat = resources.GetString("txt数量.OutputFormat")
        Me.txt数量.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt数量.Text = "12,345.00"
        Me.txt数量.Top = 0!
        Me.txt数量.Width = 0.686!
        '
        'txt仕入単価
        '
        Me.txt仕入単価.DataField = "fld仕入単価"
        Me.txt仕入単価.Height = 0.16!
        Me.txt仕入単価.Left = 6.816999!
        Me.txt仕入単価.MultiLine = False
        Me.txt仕入単価.Name = "txt仕入単価"
        Me.txt仕入単価.OutputFormat = resources.GetString("txt仕入単価.OutputFormat")
        Me.txt仕入単価.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt仕入単価.Text = "12,345.00"
        Me.txt仕入単価.Top = 0!
        Me.txt仕入単価.Width = 0.789!
        '
        'txt仕入金額
        '
        Me.txt仕入金額.DataField = "fld仕入金額"
        Me.txt仕入金額.Height = 0.16!
        Me.txt仕入金額.Left = 7.606!
        Me.txt仕入金額.MultiLine = False
        Me.txt仕入金額.Name = "txt仕入金額"
        Me.txt仕入金額.OutputFormat = resources.GetString("txt仕入金額.OutputFormat")
        Me.txt仕入金額.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt仕入金額.Text = "30,240"
        Me.txt仕入金額.Top = 0!
        Me.txt仕入金額.Width = 0.747!
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
        'ReportHeader1
        '
        Me.ReportHeader1.Height = 0!
        Me.ReportHeader1.Name = "ReportHeader1"
        '
        'ReportFooter1
        '
        Me.ReportFooter1.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.txt合計金額ページ, Me.txt消費税ページ, Me.txt仕入金額ページ, Me.Label35, Me.Label37, Me.txt伝票件数, Me.Label39, Me.txt明細件数, Me.Line5})
        Me.ReportFooter1.Height = 0.16!
        Me.ReportFooter1.Name = "ReportFooter1"
        '
        'txt合計金額ページ
        '
        Me.txt合計金額ページ.DataField = "fld合計金額ページ"
        Me.txt合計金額ページ.Height = 0.16!
        Me.txt合計金額ページ.Left = 9.1!
        Me.txt合計金額ページ.MultiLine = False
        Me.txt合計金額ページ.Name = "txt合計金額ページ"
        Me.txt合計金額ページ.OutputFormat = resources.GetString("txt合計金額ページ.OutputFormat")
        Me.txt合計金額ページ.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt合計金額ページ.Text = "12,345,678"
        Me.txt合計金額ページ.Top = 0!
        Me.txt合計金額ページ.Width = 0.747!
        '
        'txt消費税ページ
        '
        Me.txt消費税ページ.DataField = "fld消費税ページ"
        Me.txt消費税ページ.Height = 0.16!
        Me.txt消費税ページ.Left = 8.353001!
        Me.txt消費税ページ.MultiLine = False
        Me.txt消費税ページ.Name = "txt消費税ページ"
        Me.txt消費税ページ.OutputFormat = resources.GetString("txt消費税ページ.OutputFormat")
        Me.txt消費税ページ.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt消費税ページ.Text = "12,345,678"
        Me.txt消費税ページ.Top = 0!
        Me.txt消費税ページ.Width = 0.747!
        '
        'txt仕入金額ページ
        '
        Me.txt仕入金額ページ.DataField = "fld仕入金額ページ"
        Me.txt仕入金額ページ.Height = 0.16!
        Me.txt仕入金額ページ.Left = 7.606!
        Me.txt仕入金額ページ.MultiLine = False
        Me.txt仕入金額ページ.Name = "txt仕入金額ページ"
        Me.txt仕入金額ページ.OutputFormat = resources.GetString("txt仕入金額ページ.OutputFormat")
        Me.txt仕入金額ページ.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt仕入金額ページ.Text = "12,345,678"
        Me.txt仕入金額ページ.Top = 0!
        Me.txt仕入金額ページ.Width = 0.747!
        '
        'Label35
        '
        Me.Label35.Height = 0.16!
        Me.Label35.HyperLink = Nothing
        Me.Label35.Left = 6.694999!
        Me.Label35.Name = "Label35"
        Me.Label35.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: middle"
        Me.Label35.Text = "★★[総合計]"
        Me.Label35.Top = 0!
        Me.Label35.Width = 0.911!
        '
        'Label37
        '
        Me.Label37.Height = 0.16!
        Me.Label37.HyperLink = Nothing
        Me.Label37.Left = 0!
        Me.Label37.Name = "Label37"
        Me.Label37.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: middle"
        Me.Label37.Text = "[伝票]"
        Me.Label37.Top = 0!
        Me.Label37.Width = 0.5210001!
        '
        'txt伝票件数
        '
        Me.txt伝票件数.Height = 0.16!
        Me.txt伝票件数.HyperLink = Nothing
        Me.txt伝票件数.Left = 0.521!
        Me.txt伝票件数.Name = "txt伝票件数"
        Me.txt伝票件数.Style = "font-family: ＭＳ 明朝; font-size: 9pt; font-weight: normal; text-align: left; vertic" &
    "al-align: middle"
        Me.txt伝票件数.Text = "3件"
        Me.txt伝票件数.Top = 0!
        Me.txt伝票件数.Width = 0.5630002!
        '
        'Label39
        '
        Me.Label39.Height = 0.16!
        Me.Label39.HyperLink = Nothing
        Me.Label39.Left = 1.084!
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
        Me.txt明細件数.Left = 1.563!
        Me.txt明細件数.Name = "txt明細件数"
        Me.txt明細件数.Style = "font-family: ＭＳ 明朝; font-size: 9pt; font-weight: normal; text-align: left; vertic" &
    "al-align: middle"
        Me.txt明細件数.Text = "9件"
        Me.txt明細件数.Top = 0!
        Me.txt明細件数.Width = 0.5630002!
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
        Me.gh伝票.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.txt伝票番号, Me.txt仕入日, Me.txt仕入先名, Me.txt支払先ID, Me.txt支払先名, Me.Line1, Me.Line2})
        Me.gh伝票.DataField = "fid伝票番号"
        Me.gh伝票.Height = 0.16!
        Me.gh伝票.Name = "gh伝票"
        '
        'txt伝票番号
        '
        Me.txt伝票番号.Height = 0.16!
        Me.txt伝票番号.HyperLink = Nothing
        Me.txt伝票番号.Left = 0.0000009536743!
        Me.txt伝票番号.MultiLine = False
        Me.txt伝票番号.Name = "txt伝票番号"
        Me.txt伝票番号.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left; vertical-align: middle"
        Me.txt伝票番号.Text = "U000000"
        Me.txt伝票番号.Top = 0!
        Me.txt伝票番号.Width = 0.5630001!
        '
        'txt仕入日
        '
        Me.txt仕入日.Height = 0.16!
        Me.txt仕入日.HyperLink = Nothing
        Me.txt仕入日.Left = 0.563001!
        Me.txt仕入日.MultiLine = False
        Me.txt仕入日.Name = "txt仕入日"
        Me.txt仕入日.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left; vertical-align: middle"
        Me.txt仕入日.Text = "2018/02/20"
        Me.txt仕入日.Top = 0!
        Me.txt仕入日.Width = 0.75!
        '
        'txt仕入先名
        '
        Me.txt仕入先名.Height = 0.16!
        Me.txt仕入先名.HyperLink = Nothing
        Me.txt仕入先名.Left = 1.463001!
        Me.txt仕入先名.MultiLine = False
        Me.txt仕入先名.Name = "txt仕入先名"
        Me.txt仕入先名.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left; vertical-align: middle"
        Me.txt仕入先名.Text = "㈱東成流通サービス"
        Me.txt仕入先名.Top = 0!
        Me.txt仕入先名.Width = 1.916!
        '
        'txt支払先ID
        '
        Me.txt支払先ID.Height = 0.16!
        Me.txt支払先ID.HyperLink = Nothing
        Me.txt支払先ID.Left = 3.379001!
        Me.txt支払先ID.MultiLine = False
        Me.txt支払先ID.Name = "txt支払先ID"
        Me.txt支払先ID.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; text-align: left; vertical-align: middle"
        Me.txt支払先ID.Text = "[支払先]"
        Me.txt支払先ID.Top = 0!
        Me.txt支払先ID.Width = 0.6020007!
        '
        'txt支払先名
        '
        Me.txt支払先名.Height = 0.16!
        Me.txt支払先名.HyperLink = Nothing
        Me.txt支払先名.Left = 3.981001!
        Me.txt支払先名.MultiLine = False
        Me.txt支払先名.Name = "txt支払先名"
        Me.txt支払先名.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left; vertical-align: middle"
        Me.txt支払先名.Text = "株式会社　丸中　二階堂"
        Me.txt支払先名.Top = 0!
        Me.txt支払先名.Width = 2.104!
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
        'Line2
        '
        Me.Line2.Height = 0!
        Me.Line2.Left = 0.6!
        Me.Line2.LineStyle = GrapeCity.ActiveReports.SectionReportModel.LineStyle.Dot
        Me.Line2.LineWeight = 1.0!
        Me.Line2.Name = "Line2"
        Me.Line2.Top = 0.16!
        Me.Line2.Width = 10.09!
        Me.Line2.X1 = 0.6!
        Me.Line2.X2 = 10.69!
        Me.Line2.Y1 = 0.16!
        Me.Line2.Y2 = 0.16!
        '
        'gf伝票
        '
        Me.gf伝票.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.txt合計金額グループ, Me.txt消費税グループ, Me.txt仕入金額グループ, Me.Label31, Me.Line4})
        Me.gf伝票.Height = 0.16!
        Me.gf伝票.Name = "gf伝票"
        '
        'txt合計金額グループ
        '
        Me.txt合計金額グループ.DataField = "fld合計金額グループ"
        Me.txt合計金額グループ.Height = 0.16!
        Me.txt合計金額グループ.Left = 9.1!
        Me.txt合計金額グループ.MultiLine = False
        Me.txt合計金額グループ.Name = "txt合計金額グループ"
        Me.txt合計金額グループ.OutputFormat = resources.GetString("txt合計金額グループ.OutputFormat")
        Me.txt合計金額グループ.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt合計金額グループ.Text = "67,608"
        Me.txt合計金額グループ.Top = 0!
        Me.txt合計金額グループ.Width = 0.747!
        '
        'txt消費税グループ
        '
        Me.txt消費税グループ.DataField = "fld消費税グループ"
        Me.txt消費税グループ.Height = 0.16!
        Me.txt消費税グループ.Left = 8.353001!
        Me.txt消費税グループ.MultiLine = False
        Me.txt消費税グループ.Name = "txt消費税グループ"
        Me.txt消費税グループ.OutputFormat = resources.GetString("txt消費税グループ.OutputFormat")
        Me.txt消費税グループ.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt消費税グループ.Text = "5,008"
        Me.txt消費税グループ.Top = 0!
        Me.txt消費税グループ.Width = 0.747!
        '
        'txt仕入金額グループ
        '
        Me.txt仕入金額グループ.DataField = "fld仕入金額グループ"
        Me.txt仕入金額グループ.Height = 0.16!
        Me.txt仕入金額グループ.Left = 7.606!
        Me.txt仕入金額グループ.MultiLine = False
        Me.txt仕入金額グループ.Name = "txt仕入金額グループ"
        Me.txt仕入金額グループ.OutputFormat = resources.GetString("txt仕入金額グループ.OutputFormat")
        Me.txt仕入金額グループ.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt仕入金額グループ.Text = "30,240"
        Me.txt仕入金額グループ.Top = 0!
        Me.txt仕入金額グループ.Width = 0.747!
        '
        'Label31
        '
        Me.Label31.Height = 0.16!
        Me.Label31.HyperLink = Nothing
        Me.Label31.Left = 6.694999!
        Me.Label31.Name = "Label31"
        Me.Label31.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label31.Text = "★[伝票計]"
        Me.Label31.Top = 0!
        Me.Label31.Width = 0.911!
        '
        'Line4
        '
        Me.Line4.Height = 0!
        Me.Line4.Left = 0.6!
        Me.Line4.LineStyle = GrapeCity.ActiveReports.SectionReportModel.LineStyle.Dot
        Me.Line4.LineWeight = 1.0!
        Me.Line4.Name = "Line4"
        Me.Line4.Top = 0!
        Me.Line4.Width = 10.09!
        Me.Line4.X1 = 0.6!
        Me.Line4.X2 = 10.69!
        Me.Line4.Y1 = 0!
        Me.Line4.Y2 = 0!
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
        'fld数量
        '
        Me.fld数量.DefaultValue = Nothing
        Me.fld数量.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld数量.Formula = Nothing
        Me.fld数量.Name = "fld数量"
        Me.fld数量.Tag = Nothing
        '
        'fld仕入単価
        '
        Me.fld仕入単価.DefaultValue = Nothing
        Me.fld仕入単価.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld仕入単価.Formula = Nothing
        Me.fld仕入単価.Name = "fld仕入単価"
        Me.fld仕入単価.Tag = Nothing
        '
        'fld仕入金額
        '
        Me.fld仕入金額.DefaultValue = Nothing
        Me.fld仕入金額.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld仕入金額.Formula = Nothing
        Me.fld仕入金額.Name = "fld仕入金額"
        Me.fld仕入金額.Tag = Nothing
        '
        'fld仕入金額グループ
        '
        Me.fld仕入金額グループ.DefaultValue = Nothing
        Me.fld仕入金額グループ.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld仕入金額グループ.Formula = Nothing
        Me.fld仕入金額グループ.Name = "fld仕入金額グループ"
        Me.fld仕入金額グループ.Tag = Nothing
        '
        'fld消費税グループ
        '
        Me.fld消費税グループ.DefaultValue = Nothing
        Me.fld消費税グループ.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld消費税グループ.Formula = Nothing
        Me.fld消費税グループ.Name = "fld消費税グループ"
        Me.fld消費税グループ.Tag = Nothing
        '
        'fld合計金額グループ
        '
        Me.fld合計金額グループ.DefaultValue = Nothing
        Me.fld合計金額グループ.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld合計金額グループ.Formula = Nothing
        Me.fld合計金額グループ.Name = "fld合計金額グループ"
        Me.fld合計金額グループ.Tag = Nothing
        '
        'fld仕入金額ページ
        '
        Me.fld仕入金額ページ.DefaultValue = Nothing
        Me.fld仕入金額ページ.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld仕入金額ページ.Formula = Nothing
        Me.fld仕入金額ページ.Name = "fld仕入金額ページ"
        Me.fld仕入金額ページ.Tag = Nothing
        '
        'fld消費税ページ
        '
        Me.fld消費税ページ.DefaultValue = Nothing
        Me.fld消費税ページ.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld消費税ページ.Formula = Nothing
        Me.fld消費税ページ.Name = "fld消費税ページ"
        Me.fld消費税ページ.Tag = Nothing
        '
        'fld合計金額ページ
        '
        Me.fld合計金額ページ.DefaultValue = Nothing
        Me.fld合計金額ページ.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld合計金額ページ.Formula = Nothing
        Me.fld合計金額ページ.Name = "fld合計金額ページ"
        Me.fld合計金額ページ.Tag = Nothing
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
        'H11R01_ShiireList
        '
        Me.MasterReport = False
        Me.CalculatedFields.Add(Me.fid伝票番号)
        Me.CalculatedFields.Add(Me.fld入数)
        Me.CalculatedFields.Add(Me.fld個数)
        Me.CalculatedFields.Add(Me.fld数量)
        Me.CalculatedFields.Add(Me.fld仕入単価)
        Me.CalculatedFields.Add(Me.fld仕入金額)
        Me.CalculatedFields.Add(Me.fld仕入金額グループ)
        Me.CalculatedFields.Add(Me.fld消費税グループ)
        Me.CalculatedFields.Add(Me.fld合計金額グループ)
        Me.CalculatedFields.Add(Me.fld仕入金額ページ)
        Me.CalculatedFields.Add(Me.fld消費税ページ)
        Me.CalculatedFields.Add(Me.fld合計金額ページ)
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
        Me.Sections.Add(Me.gh伝票)
        Me.Sections.Add(Me.Detail)
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
        CType(Me.txt仕入先ヘッダ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt伝票番号ヘッダ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label41, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label43, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt作成日ヘッダ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReportInfo1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt商品名, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt税区分, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt単位, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt明細備考, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt入数, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt個数, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt数量, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt仕入単価, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt仕入金額, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt会社名, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt合計金額ページ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt消費税ページ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt仕入金額ページ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label35, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label37, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt伝票件数, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label39, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt明細件数, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt伝票番号, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt仕入日, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt仕入先名, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt支払先ID, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt支払先名, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt合計金額グループ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt消費税グループ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt仕入金額グループ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label31, System.ComponentModel.ISupportInitialize).EndInit()
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
    Private WithEvents txt仕入先ヘッダ As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label4 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt伝票番号ヘッダ As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label6 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label7 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label8 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label10 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label12 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label14 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label16 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label18 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label19 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label20 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label21 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label24 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label41 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label43 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt作成日ヘッダ As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents ReportInfo1 As GrapeCity.ActiveReports.SectionReportModel.ReportInfo
    Private WithEvents Line3 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents Line6 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents Line1 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents txt伝票番号 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt仕入日 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt仕入先名 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt支払先ID As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt支払先名 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt商品名 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt税区分 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt単位 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt明細備考 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt入数 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt個数 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt数量 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt仕入単価 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt仕入金額 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt合計金額グループ As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt消費税グループ As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt仕入金額グループ As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents Label31 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt会社名 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt合計金額ページ As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt消費税ページ As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt仕入金額ページ As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents Label35 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label37 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt伝票件数 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label39 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt明細件数 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Line5 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents Line4 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents Line2 As GrapeCity.ActiveReports.SectionReportModel.Line
    Friend WithEvents fid伝票番号 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld入数 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld個数 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld数量 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld仕入単価 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld仕入金額 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld仕入金額グループ As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld消費税グループ As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld合計金額グループ As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld仕入金額ページ As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld消費税ページ As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld合計金額ページ As GrapeCity.ActiveReports.Data.Field
    Private WithEvents Line7 As GrapeCity.ActiveReports.SectionReportModel.Line
End Class
