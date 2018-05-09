<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class H11R02_KaikakeList
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(H11R02_KaikakeList))
        Me.PageHeader = New GrapeCity.ActiveReports.SectionReportModel.PageHeader()
        Me.txtPrm = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.Label2 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt区分ヘッダ = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label1 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt仕入支払年月ヘッダ = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label6 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label7 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label14 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label16 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label18 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.lbl当月残 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label24 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label41 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label43 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt作成日ヘッダ = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.ReportInfo1 = New GrapeCity.ActiveReports.SectionReportModel.ReportInfo()
        Me.Label4 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label12 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label17 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label3 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt出力順ヘッダ = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label11 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label5 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Line6 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.Label8 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Line3 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.Detail = New GrapeCity.ActiveReports.SectionReportModel.Detail()
        Me.txt支払先 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt備考 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt番号 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txtコード = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt前月残 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt仕入額 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt共販手数料 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt仕入合計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt消費税 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt支払額 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt手数料 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt当月残 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt税込額 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.Line1 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.PageFooter = New GrapeCity.ActiveReports.SectionReportModel.PageFooter()
        Me.txt会社名 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.ReportHeader1 = New GrapeCity.ActiveReports.SectionReportModel.ReportHeader()
        Me.ReportFooter1 = New GrapeCity.ActiveReports.SectionReportModel.ReportFooter()
        Me.Label35 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt前月残総計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt仕入額総計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt共販手数料総計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt仕入合計総計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt消費税総計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt支払額総計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt手数料総計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt当月残総計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt税込額総計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.Line5 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.fld前月残 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld仕入額 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld共販手数料 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld仕入合計 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld消費税 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld支払額 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld手数料 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld当月残 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld税込額 = New GrapeCity.ActiveReports.Data.Field()
        Me.Line2 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        CType(Me.txtPrm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt区分ヘッダ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt仕入支払年月ヘッダ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl当月残, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label41, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label43, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt作成日ヘッダ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReportInfo1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt出力順ヘッダ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt支払先, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt備考, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt番号, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtコード, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt前月残, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt仕入額, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt共販手数料, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt仕入合計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt消費税, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt支払額, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt手数料, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt当月残, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt税込額, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt会社名, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label35, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt前月残総計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt仕入額総計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt共販手数料総計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt仕入合計総計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt消費税総計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt支払額総計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt手数料総計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt当月残総計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt税込額総計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.txtPrm, Me.Label2, Me.txt区分ヘッダ, Me.Label1, Me.txt仕入支払年月ヘッダ, Me.Label6, Me.Label7, Me.Label14, Me.Label16, Me.Label18, Me.lbl当月残, Me.Label24, Me.Label41, Me.Label43, Me.txt作成日ヘッダ, Me.ReportInfo1, Me.Label4, Me.Label12, Me.Label17, Me.Label3, Me.txt出力順ヘッダ, Me.Label11, Me.Label5, Me.Line6, Me.Label8, Me.Line3})
        Me.PageHeader.Height = 0.485!
        Me.PageHeader.Name = "PageHeader"
        '
        'txtPrm
        '
        Me.txtPrm.Height = 0.252!
        Me.txtPrm.Left = 0.1!
        Me.txtPrm.Name = "txtPrm"
        Me.txtPrm.Style = "font-family: ＭＳ ゴシック; font-size: 15.75pt; font-style: italic; font-weight: bold"
        Me.txtPrm.Text = "買掛金一覧表"
        Me.txtPrm.Top = 4.656613E-10!
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
        Me.Label2.Top = 4.656613E-10!
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
        Me.Label1.Text = "[仕入・支払年月]"
        Me.Label1.Top = 4.656613E-10!
        Me.Label1.Width = 1.774!
        '
        'txt仕入支払年月ヘッダ
        '
        Me.txt仕入支払年月ヘッダ.Height = 0.15!
        Me.txt仕入支払年月ヘッダ.HyperLink = Nothing
        Me.txt仕入支払年月ヘッダ.Left = 2.882!
        Me.txt仕入支払年月ヘッダ.MultiLine = False
        Me.txt仕入支払年月ヘッダ.Name = "txt仕入支払年月ヘッダ"
        Me.txt仕入支払年月ヘッダ.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left"
        Me.txt仕入支払年月ヘッダ.Text = "2018/02"
        Me.txt仕入支払年月ヘッダ.Top = 0.15!
        Me.txt仕入支払年月ヘッダ.Width = 1.774!
        '
        'Label6
        '
        Me.Label6.Height = 0.15!
        Me.Label6.HyperLink = Nothing
        Me.Label6.Left = 0.8660001!
        Me.Label6.Name = "Label6"
        Me.Label6.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: top"
        Me.Label6.Text = "支払先"
        Me.Label6.Top = 0.326!
        Me.Label6.Width = 1.766!
        '
        'Label7
        '
        Me.Label7.Height = 0.15!
        Me.Label7.HyperLink = Nothing
        Me.Label7.Left = 0.028!
        Me.Label7.Name = "Label7"
        Me.Label7.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: center; ve" &
    "rtical-align: top"
        Me.Label7.Text = "№"
        Me.Label7.Top = 0.326!
        Me.Label7.Width = 0.348!
        '
        'Label14
        '
        Me.Label14.Height = 0.15!
        Me.Label14.HyperLink = Nothing
        Me.Label14.Left = 2.632!
        Me.Label14.Name = "Label14"
        Me.Label14.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label14.Text = "前月残"
        Me.Label14.Top = 0.326!
        Me.Label14.Width = 0.7370002!
        '
        'Label16
        '
        Me.Label16.Height = 0.15!
        Me.Label16.HyperLink = Nothing
        Me.Label16.Left = 3.369!
        Me.Label16.Name = "Label16"
        Me.Label16.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label16.Text = "仕入額"
        Me.Label16.Top = 0.326!
        Me.Label16.Width = 0.7370002!
        '
        'Label18
        '
        Me.Label18.Height = 0.15!
        Me.Label18.HyperLink = Nothing
        Me.Label18.Left = 4.106!
        Me.Label18.Name = "Label18"
        Me.Label18.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label18.Text = "共販手数料"
        Me.Label18.Top = 0.326!
        Me.Label18.Width = 0.7370002!
        '
        'lbl当月残
        '
        Me.lbl当月残.Height = 0.15!
        Me.lbl当月残.HyperLink = Nothing
        Me.lbl当月残.Left = 7.791!
        Me.lbl当月残.Name = "lbl当月残"
        Me.lbl当月残.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.lbl当月残.Text = "手数料"
        Me.lbl当月残.Top = 0.326!
        Me.lbl当月残.Width = 0.7370002!
        '
        'Label24
        '
        Me.Label24.Height = 0.15!
        Me.Label24.HyperLink = Nothing
        Me.Label24.Left = 9.326!
        Me.Label24.Name = "Label24"
        Me.Label24.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: top"
        Me.Label24.Text = "備考"
        Me.Label24.Top = 0.326!
        Me.Label24.Width = 1.35!
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
        Me.Label43.Left = 8.141!
        Me.Label43.Name = "Label43"
        Me.Label43.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label43.Text = "作成日："
        Me.Label43.Top = 0.15!
        Me.Label43.Width = 0.6099997!
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
        'Label4
        '
        Me.Label4.Height = 0.15!
        Me.Label4.HyperLink = Nothing
        Me.Label4.Left = 4.843!
        Me.Label4.Name = "Label4"
        Me.Label4.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label4.Text = "仕入合計"
        Me.Label4.Top = 0.326!
        Me.Label4.Width = 0.7370002!
        '
        'Label12
        '
        Me.Label12.Height = 0.15!
        Me.Label12.HyperLink = Nothing
        Me.Label12.Left = 5.58!
        Me.Label12.Name = "Label12"
        Me.Label12.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label12.Text = "消費税"
        Me.Label12.Top = 0.326!
        Me.Label12.Width = 0.7370002!
        '
        'Label17
        '
        Me.Label17.Height = 0.15!
        Me.Label17.HyperLink = Nothing
        Me.Label17.Left = 7.054!
        Me.Label17.Name = "Label17"
        Me.Label17.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label17.Text = "支払額"
        Me.Label17.Top = 0.326!
        Me.Label17.Width = 0.7370002!
        '
        'Label3
        '
        Me.Label3.Height = 0.16!
        Me.Label3.HyperLink = Nothing
        Me.Label3.Left = 7.425!
        Me.Label3.Name = "Label3"
        Me.Label3.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: top"
        Me.Label3.Text = "[出力順]"
        Me.Label3.Top = 4.656613E-10!
        Me.Label3.Width = 0.6309997!
        '
        'txt出力順ヘッダ
        '
        Me.txt出力順ヘッダ.Height = 0.15!
        Me.txt出力順ヘッダ.HyperLink = Nothing
        Me.txt出力順ヘッダ.Left = 7.425!
        Me.txt出力順ヘッダ.MultiLine = False
        Me.txt出力順ヘッダ.Name = "txt出力順ヘッダ"
        Me.txt出力順ヘッダ.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left"
        Me.txt出力順ヘッダ.Text = "コード順"
        Me.txt出力順ヘッダ.Top = 0.15!
        Me.txt出力順ヘッダ.Width = 0.6309997!
        '
        'Label11
        '
        Me.Label11.Height = 0.15!
        Me.Label11.HyperLink = Nothing
        Me.Label11.Left = 0.376!
        Me.Label11.Name = "Label11"
        Me.Label11.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: center; ve" &
    "rtical-align: top"
        Me.Label11.Text = "CD"
        Me.Label11.Top = 0.326!
        Me.Label11.Width = 0.4900002!
        '
        'Label5
        '
        Me.Label5.Height = 0.15!
        Me.Label5.HyperLink = Nothing
        Me.Label5.Left = 8.528001!
        Me.Label5.Name = "Label5"
        Me.Label5.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label5.Text = "当月残"
        Me.Label5.Top = 0.326!
        Me.Label5.Width = 0.7370002!
        '
        'Line6
        '
        Me.Line6.Height = 0!
        Me.Line6.Left = 0!
        Me.Line6.LineWeight = 2.0!
        Me.Line6.Name = "Line6"
        Me.Line6.Top = 0.476!
        Me.Line6.Width = 10.69!
        Me.Line6.X1 = 0!
        Me.Line6.X2 = 10.69!
        Me.Line6.Y1 = 0.476!
        Me.Line6.Y2 = 0.476!
        '
        'Label8
        '
        Me.Label8.Height = 0.15!
        Me.Label8.HyperLink = Nothing
        Me.Label8.Left = 6.317!
        Me.Label8.Name = "Label8"
        Me.Label8.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label8.Text = "税込額"
        Me.Label8.Top = 0.326!
        Me.Label8.Width = 0.737!
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
        'Detail
        '
        Me.Detail.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.txt支払先, Me.txt備考, Me.txt番号, Me.txtコード, Me.txt前月残, Me.txt仕入額, Me.txt共販手数料, Me.txt仕入合計, Me.txt消費税, Me.txt支払額, Me.txt手数料, Me.txt当月残, Me.txt税込額, Me.Line1})
        Me.Detail.Height = 0.16!
        Me.Detail.Name = "Detail"
        '
        'txt支払先
        '
        Me.txt支払先.Height = 0.16!
        Me.txt支払先.HyperLink = Nothing
        Me.txt支払先.Left = 0.8520001!
        Me.txt支払先.MultiLine = False
        Me.txt支払先.Name = "txt支払先"
        Me.txt支払先.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left; vertical-align: middle"
        Me.txt支払先.Text = "株式会社　〇〇〇〇〇〇〇〇"
        Me.txt支払先.Top = 0!
        Me.txt支払先.Width = 1.766!
        '
        'txt備考
        '
        Me.txt備考.Height = 0.16!
        Me.txt備考.HyperLink = Nothing
        Me.txt備考.Left = 9.326!
        Me.txt備考.MultiLine = False
        Me.txt備考.Name = "txt備考"
        Me.txt備考.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left; vertical-align: middle"
        Me.txt備考.Text = ""
        Me.txt備考.Top = 0!
        Me.txt備考.Width = 1.35!
        '
        'txt番号
        '
        Me.txt番号.Height = 0.16!
        Me.txt番号.HyperLink = Nothing
        Me.txt番号.Left = 0.014!
        Me.txt番号.MultiLine = False
        Me.txt番号.Name = "txt番号"
        Me.txt番号.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: center; vertical-align: middle"
        Me.txt番号.Text = "1234"
        Me.txt番号.Top = 0!
        Me.txt番号.Width = 0.348!
        '
        'txtコード
        '
        Me.txtコード.Height = 0.16!
        Me.txtコード.HyperLink = Nothing
        Me.txtコード.Left = 0.362!
        Me.txtコード.MultiLine = False
        Me.txtコード.Name = "txtコード"
        Me.txtコード.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: center; vertical-align: middle"
        Me.txtコード.Text = "R123456"
        Me.txtコード.Top = 0!
        Me.txtコード.Width = 0.4900002!
        '
        'txt前月残
        '
        Me.txt前月残.DataField = "fld前月残"
        Me.txt前月残.Height = 0.16!
        Me.txt前月残.Left = 2.632!
        Me.txt前月残.Name = "txt前月残"
        Me.txt前月残.OutputFormat = resources.GetString("txt前月残.OutputFormat")
        Me.txt前月残.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt前月残.Text = "123,456,789"
        Me.txt前月残.Top = 0!
        Me.txt前月残.Width = 0.7370002!
        '
        'txt仕入額
        '
        Me.txt仕入額.DataField = "fld仕入額"
        Me.txt仕入額.Height = 0.16!
        Me.txt仕入額.Left = 3.369!
        Me.txt仕入額.Name = "txt仕入額"
        Me.txt仕入額.OutputFormat = resources.GetString("txt仕入額.OutputFormat")
        Me.txt仕入額.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt仕入額.Text = "123,456,789"
        Me.txt仕入額.Top = 0.00000005960464!
        Me.txt仕入額.Width = 0.7370002!
        '
        'txt共販手数料
        '
        Me.txt共販手数料.DataField = "fld共販手数料"
        Me.txt共販手数料.Height = 0.16!
        Me.txt共販手数料.Left = 4.106!
        Me.txt共販手数料.Name = "txt共販手数料"
        Me.txt共販手数料.OutputFormat = resources.GetString("txt共販手数料.OutputFormat")
        Me.txt共販手数料.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt共販手数料.Text = "123,456,789"
        Me.txt共販手数料.Top = 0.003472209!
        Me.txt共販手数料.Width = 0.7370002!
        '
        'txt仕入合計
        '
        Me.txt仕入合計.DataField = "fld仕入合計"
        Me.txt仕入合計.Height = 0.16!
        Me.txt仕入合計.Left = 4.843!
        Me.txt仕入合計.Name = "txt仕入合計"
        Me.txt仕入合計.OutputFormat = resources.GetString("txt仕入合計.OutputFormat")
        Me.txt仕入合計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt仕入合計.Text = "123,456,789"
        Me.txt仕入合計.Top = 0.003000021!
        Me.txt仕入合計.Width = 0.7370002!
        '
        'txt消費税
        '
        Me.txt消費税.DataField = "fld消費税"
        Me.txt消費税.Height = 0.16!
        Me.txt消費税.Left = 5.58!
        Me.txt消費税.Name = "txt消費税"
        Me.txt消費税.OutputFormat = resources.GetString("txt消費税.OutputFormat")
        Me.txt消費税.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt消費税.Text = "123,456,789"
        Me.txt消費税.Top = 0.003000021!
        Me.txt消費税.Width = 0.7370002!
        '
        'txt支払額
        '
        Me.txt支払額.DataField = "fld支払額"
        Me.txt支払額.Height = 0.16!
        Me.txt支払額.Left = 7.054!
        Me.txt支払額.Name = "txt支払額"
        Me.txt支払額.OutputFormat = resources.GetString("txt支払額.OutputFormat")
        Me.txt支払額.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt支払額.Text = "123,456,789"
        Me.txt支払額.Top = 0.003000021!
        Me.txt支払額.Width = 0.7370002!
        '
        'txt手数料
        '
        Me.txt手数料.DataField = "fld手数料"
        Me.txt手数料.Height = 0.16!
        Me.txt手数料.Left = 7.791!
        Me.txt手数料.Name = "txt手数料"
        Me.txt手数料.OutputFormat = resources.GetString("txt手数料.OutputFormat")
        Me.txt手数料.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt手数料.Text = "123,456,789"
        Me.txt手数料.Top = 0.003000021!
        Me.txt手数料.Width = 0.7370002!
        '
        'txt当月残
        '
        Me.txt当月残.DataField = "fld当月残"
        Me.txt当月残.Height = 0.16!
        Me.txt当月残.Left = 8.528001!
        Me.txt当月残.Name = "txt当月残"
        Me.txt当月残.OutputFormat = resources.GetString("txt当月残.OutputFormat")
        Me.txt当月残.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt当月残.Text = "123,456,789"
        Me.txt当月残.Top = 0.003000021!
        Me.txt当月残.Width = 0.7370002!
        '
        'txt税込額
        '
        Me.txt税込額.DataField = "fld税込額"
        Me.txt税込額.Height = 0.16!
        Me.txt税込額.Left = 6.317!
        Me.txt税込額.Name = "txt税込額"
        Me.txt税込額.OutputFormat = resources.GetString("txt税込額.OutputFormat")
        Me.txt税込額.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt税込額.Text = "123,456,789"
        Me.txt税込額.Top = 0.003!
        Me.txt税込額.Width = 0.737!
        '
        'Line1
        '
        Me.Line1.Height = 0!
        Me.Line1.Left = 0!
        Me.Line1.LineWeight = 1.0!
        Me.Line1.Name = "Line1"
        Me.Line1.Top = 0.163!
        Me.Line1.Width = 10.69!
        Me.Line1.X1 = 0!
        Me.Line1.X2 = 10.69!
        Me.Line1.Y1 = 0.163!
        Me.Line1.Y2 = 0.163!
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
        'ReportHeader1
        '
        Me.ReportHeader1.Height = 0!
        Me.ReportHeader1.Name = "ReportHeader1"
        Me.ReportHeader1.Visible = False
        '
        'ReportFooter1
        '
        Me.ReportFooter1.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.Label35, Me.txt前月残総計, Me.txt仕入額総計, Me.txt共販手数料総計, Me.txt仕入合計総計, Me.txt消費税総計, Me.txt支払額総計, Me.txt手数料総計, Me.txt当月残総計, Me.txt税込額総計, Me.Line5})
        Me.ReportFooter1.Height = 0.16!
        Me.ReportFooter1.Name = "ReportFooter1"
        '
        'Label35
        '
        Me.Label35.Height = 0.16!
        Me.Label35.HyperLink = Nothing
        Me.Label35.Left = 0.8520001!
        Me.Label35.Name = "Label35"
        Me.Label35.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: middle"
        Me.Label35.Text = "★★[総合計]"
        Me.Label35.Top = 0!
        Me.Label35.Width = 0.9999993!
        '
        'txt前月残総計
        '
        Me.txt前月残総計.DataField = "fld前月残"
        Me.txt前月残総計.Height = 0.16!
        Me.txt前月残総計.Left = 2.632!
        Me.txt前月残総計.Name = "txt前月残総計"
        Me.txt前月残総計.OutputFormat = resources.GetString("txt前月残総計.OutputFormat")
        Me.txt前月残総計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt前月残総計.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.All
        Me.txt前月残総計.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.GrandTotal
        Me.txt前月残総計.Text = "123,456,789"
        Me.txt前月残総計.Top = 0!
        Me.txt前月残総計.Width = 0.7370002!
        '
        'txt仕入額総計
        '
        Me.txt仕入額総計.DataField = "fld仕入額"
        Me.txt仕入額総計.Height = 0.16!
        Me.txt仕入額総計.Left = 3.369!
        Me.txt仕入額総計.Name = "txt仕入額総計"
        Me.txt仕入額総計.OutputFormat = resources.GetString("txt仕入額総計.OutputFormat")
        Me.txt仕入額総計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt仕入額総計.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.All
        Me.txt仕入額総計.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.GrandTotal
        Me.txt仕入額総計.Text = "123,456,789"
        Me.txt仕入額総計.Top = 0!
        Me.txt仕入額総計.Width = 0.7370002!
        '
        'txt共販手数料総計
        '
        Me.txt共販手数料総計.DataField = "fld共販手数料"
        Me.txt共販手数料総計.Height = 0.16!
        Me.txt共販手数料総計.Left = 4.106!
        Me.txt共販手数料総計.Name = "txt共販手数料総計"
        Me.txt共販手数料総計.OutputFormat = resources.GetString("txt共販手数料総計.OutputFormat")
        Me.txt共販手数料総計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt共販手数料総計.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.All
        Me.txt共販手数料総計.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.GrandTotal
        Me.txt共販手数料総計.Text = "123,456,789"
        Me.txt共販手数料総計.Top = 0!
        Me.txt共販手数料総計.Width = 0.7370002!
        '
        'txt仕入合計総計
        '
        Me.txt仕入合計総計.DataField = "fld仕入合計"
        Me.txt仕入合計総計.Height = 0.16!
        Me.txt仕入合計総計.Left = 4.843!
        Me.txt仕入合計総計.Name = "txt仕入合計総計"
        Me.txt仕入合計総計.OutputFormat = resources.GetString("txt仕入合計総計.OutputFormat")
        Me.txt仕入合計総計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt仕入合計総計.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.All
        Me.txt仕入合計総計.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.GrandTotal
        Me.txt仕入合計総計.Text = "123,456,789"
        Me.txt仕入合計総計.Top = 0!
        Me.txt仕入合計総計.Width = 0.7370002!
        '
        'txt消費税総計
        '
        Me.txt消費税総計.DataField = "fld消費税"
        Me.txt消費税総計.Height = 0.16!
        Me.txt消費税総計.Left = 5.58!
        Me.txt消費税総計.Name = "txt消費税総計"
        Me.txt消費税総計.OutputFormat = resources.GetString("txt消費税総計.OutputFormat")
        Me.txt消費税総計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt消費税総計.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.All
        Me.txt消費税総計.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.GrandTotal
        Me.txt消費税総計.Text = "123,456,789"
        Me.txt消費税総計.Top = 0!
        Me.txt消費税総計.Width = 0.7370002!
        '
        'txt支払額総計
        '
        Me.txt支払額総計.DataField = "fld支払額"
        Me.txt支払額総計.Height = 0.16!
        Me.txt支払額総計.Left = 7.054!
        Me.txt支払額総計.Name = "txt支払額総計"
        Me.txt支払額総計.OutputFormat = resources.GetString("txt支払額総計.OutputFormat")
        Me.txt支払額総計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt支払額総計.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.All
        Me.txt支払額総計.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.GrandTotal
        Me.txt支払額総計.Text = "123,456,789"
        Me.txt支払額総計.Top = 0!
        Me.txt支払額総計.Width = 0.7370002!
        '
        'txt手数料総計
        '
        Me.txt手数料総計.DataField = "fld手数料"
        Me.txt手数料総計.Height = 0.16!
        Me.txt手数料総計.Left = 7.791!
        Me.txt手数料総計.Name = "txt手数料総計"
        Me.txt手数料総計.OutputFormat = resources.GetString("txt手数料総計.OutputFormat")
        Me.txt手数料総計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt手数料総計.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.All
        Me.txt手数料総計.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.GrandTotal
        Me.txt手数料総計.Text = "123,456,789"
        Me.txt手数料総計.Top = 0!
        Me.txt手数料総計.Width = 0.7370002!
        '
        'txt当月残総計
        '
        Me.txt当月残総計.DataField = "fld当月残"
        Me.txt当月残総計.Height = 0.16!
        Me.txt当月残総計.Left = 8.528001!
        Me.txt当月残総計.Name = "txt当月残総計"
        Me.txt当月残総計.OutputFormat = resources.GetString("txt当月残総計.OutputFormat")
        Me.txt当月残総計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt当月残総計.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.All
        Me.txt当月残総計.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.GrandTotal
        Me.txt当月残総計.Text = "123,456,789"
        Me.txt当月残総計.Top = 0!
        Me.txt当月残総計.Width = 0.7370002!
        '
        'txt税込額総計
        '
        Me.txt税込額総計.DataField = "fld税込額"
        Me.txt税込額総計.Height = 0.16!
        Me.txt税込額総計.Left = 6.317!
        Me.txt税込額総計.Name = "txt税込額総計"
        Me.txt税込額総計.OutputFormat = resources.GetString("txt税込額総計.OutputFormat")
        Me.txt税込額総計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt税込額総計.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.All
        Me.txt税込額総計.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.GrandTotal
        Me.txt税込額総計.Text = "123,456,789"
        Me.txt税込額総計.Top = 0!
        Me.txt税込額総計.Width = 0.737!
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
        'fld前月残
        '
        Me.fld前月残.DefaultValue = Nothing
        Me.fld前月残.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld前月残.Formula = Nothing
        Me.fld前月残.Name = "fld前月残"
        Me.fld前月残.Tag = Nothing
        '
        'fld仕入額
        '
        Me.fld仕入額.DefaultValue = Nothing
        Me.fld仕入額.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld仕入額.Formula = Nothing
        Me.fld仕入額.Name = "fld仕入額"
        Me.fld仕入額.Tag = Nothing
        '
        'fld共販手数料
        '
        Me.fld共販手数料.DefaultValue = Nothing
        Me.fld共販手数料.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld共販手数料.Formula = Nothing
        Me.fld共販手数料.Name = "fld共販手数料"
        Me.fld共販手数料.Tag = Nothing
        '
        'fld仕入合計
        '
        Me.fld仕入合計.DefaultValue = Nothing
        Me.fld仕入合計.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld仕入合計.Formula = Nothing
        Me.fld仕入合計.Name = "fld仕入合計"
        Me.fld仕入合計.Tag = Nothing
        '
        'fld消費税
        '
        Me.fld消費税.DefaultValue = Nothing
        Me.fld消費税.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld消費税.Formula = Nothing
        Me.fld消費税.Name = "fld消費税"
        Me.fld消費税.Tag = Nothing
        '
        'fld支払額
        '
        Me.fld支払額.DefaultValue = Nothing
        Me.fld支払額.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld支払額.Formula = Nothing
        Me.fld支払額.Name = "fld支払額"
        Me.fld支払額.Tag = Nothing
        '
        'fld手数料
        '
        Me.fld手数料.DefaultValue = Nothing
        Me.fld手数料.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld手数料.Formula = Nothing
        Me.fld手数料.Name = "fld手数料"
        Me.fld手数料.Tag = Nothing
        '
        'fld当月残
        '
        Me.fld当月残.DefaultValue = Nothing
        Me.fld当月残.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld当月残.Formula = Nothing
        Me.fld当月残.Name = "fld当月残"
        Me.fld当月残.Tag = Nothing
        '
        'fld税込額
        '
        Me.fld税込額.DefaultValue = Nothing
        Me.fld税込額.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld税込額.Formula = Nothing
        Me.fld税込額.Name = "fld税込額"
        Me.fld税込額.Tag = Nothing
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
        'H11R02_KaikakeList
        '
        Me.MasterReport = False
        Me.CalculatedFields.Add(Me.fld前月残)
        Me.CalculatedFields.Add(Me.fld仕入額)
        Me.CalculatedFields.Add(Me.fld共販手数料)
        Me.CalculatedFields.Add(Me.fld仕入合計)
        Me.CalculatedFields.Add(Me.fld消費税)
        Me.CalculatedFields.Add(Me.fld支払額)
        Me.CalculatedFields.Add(Me.fld手数料)
        Me.CalculatedFields.Add(Me.fld当月残)
        Me.CalculatedFields.Add(Me.fld税込額)
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
        CType(Me.txt仕入支払年月ヘッダ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl当月残, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label41, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label43, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt作成日ヘッダ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReportInfo1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt出力順ヘッダ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt支払先, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt備考, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt番号, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtコード, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt前月残, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt仕入額, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt共販手数料, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt仕入合計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt消費税, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt支払額, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt手数料, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt当月残, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt税込額, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt会社名, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label35, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt前月残総計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt仕入額総計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt共販手数料総計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt仕入合計総計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt消費税総計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt支払額総計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt手数料総計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt当月残総計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt税込額総計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

    Private WithEvents ReportHeader1 As GrapeCity.ActiveReports.SectionReportModel.ReportHeader
    Private WithEvents ReportFooter1 As GrapeCity.ActiveReports.SectionReportModel.ReportFooter
    Private WithEvents txtPrm As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents Label2 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt区分ヘッダ As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label1 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt仕入支払年月ヘッダ As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label6 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label7 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label14 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label16 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label18 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents lbl当月残 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label24 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label41 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label43 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt作成日ヘッダ As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents ReportInfo1 As GrapeCity.ActiveReports.SectionReportModel.ReportInfo
    Private WithEvents Label4 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label12 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label17 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label3 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt出力順ヘッダ As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label11 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Line3 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents Line6 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents txt支払先 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt備考 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt番号 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txtコード As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt前月残 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt仕入額 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt共販手数料 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt仕入合計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt消費税 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt支払額 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt手数料 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents Line1 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents Label35 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt前月残総計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt仕入額総計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt共販手数料総計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt仕入合計総計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt消費税総計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt支払額総計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt手数料総計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents Line5 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents txt会社名 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label5 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt当月残 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt当月残総計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Friend WithEvents fld前月残 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld仕入額 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld共販手数料 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld仕入合計 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld消費税 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld支払額 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld手数料 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld当月残 As GrapeCity.ActiveReports.Data.Field
    Private WithEvents Label8 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt税込額 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt税込額総計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Friend WithEvents fld税込額 As GrapeCity.ActiveReports.Data.Field
    Private WithEvents Line2 As GrapeCity.ActiveReports.SectionReportModel.Line
End Class
