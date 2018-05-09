<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class H11R03_ShiiresakiMotoList
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
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(H11R03_ShiiresakiMotoList))
        Me.PageHeader = New GrapeCity.ActiveReports.SectionReportModel.PageHeader()
        Me.txtPrm = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.Label2 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt区分ヘッダ = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label1 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt仕入支払年月ヘッダ = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label41 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label43 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt作成日ヘッダ = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.ReportInfo1 = New GrapeCity.ActiveReports.SectionReportModel.ReportInfo()
        Me.Label3 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt出力順ヘッダ = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label10 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt日付ヘッダ = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Line3 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.Detail = New GrapeCity.ActiveReports.SectionReportModel.Detail()
        Me.txt仕入先名 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt仕入日 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt伝票番号 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt区分 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt商品摘要 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt単位 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt数量 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt単価 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt仕入額 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt消費税 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt支払額 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt残高 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.Line4 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.PageFooter = New GrapeCity.ActiveReports.SectionReportModel.PageFooter()
        Me.txt会社名 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Line5 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.ReportHeader1 = New GrapeCity.ActiveReports.SectionReportModel.ReportHeader()
        Me.ReportFooter1 = New GrapeCity.ActiveReports.SectionReportModel.ReportFooter()
        Me.gh支払先 = New GrapeCity.ActiveReports.SectionReportModel.GroupHeader()
        Me.Label6 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label7 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label18 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.lbl当月残 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label4 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label12 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label17 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label11 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label5 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt支払先コード = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txt支払先名 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label13 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label14 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label16 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Line2 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.Line1 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.gf支払先 = New GrapeCity.ActiveReports.SectionReportModel.GroupFooter()
        Me.txt仕入額計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt消費税計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt支払額計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txt残高計 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.Label35 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Line6 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        Me.fld支払先 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld仕入額 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld消費税 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld支払額 = New GrapeCity.ActiveReports.Data.Field()
        Me.fld残高 = New GrapeCity.ActiveReports.Data.Field()
        CType(Me.txtPrm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt区分ヘッダ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt仕入支払年月ヘッダ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label41, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label43, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt作成日ヘッダ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReportInfo1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt出力順ヘッダ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt日付ヘッダ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt仕入先名, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt仕入日, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt伝票番号, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt区分, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt商品摘要, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt単位, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt数量, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt単価, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt仕入額, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt消費税, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt支払額, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt残高, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt会社名, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbl当月残, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt支払先コード, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt支払先名, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt仕入額計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt消費税計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt支払額計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txt残高計, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label35, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.txtPrm, Me.Label2, Me.txt区分ヘッダ, Me.Label1, Me.txt仕入支払年月ヘッダ, Me.Label41, Me.Label43, Me.txt作成日ヘッダ, Me.ReportInfo1, Me.Label3, Me.txt出力順ヘッダ, Me.Label10, Me.txt日付ヘッダ, Me.Line3})
        Me.PageHeader.Height = 0.33!
        Me.PageHeader.Name = "PageHeader"
        '
        'txtPrm
        '
        Me.txtPrm.Height = 0.252!
        Me.txtPrm.Left = 0.09800005!
        Me.txtPrm.Name = "txtPrm"
        Me.txtPrm.Style = "font-family: ＭＳ ゴシック; font-size: 15.75pt; font-style: italic; font-weight: bold"
        Me.txtPrm.Text = "仕入先元帳"
        Me.txtPrm.Top = 4.656613E-10!
        Me.txtPrm.Width = 2.151!
        '
        'Label2
        '
        Me.Label2.Height = 0.16!
        Me.Label2.HyperLink = Nothing
        Me.Label2.Left = 2.249!
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
        Me.txt区分ヘッダ.Left = 2.249!
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
        Me.Label1.Left = 2.88!
        Me.Label1.Name = "Label1"
        Me.Label1.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: top"
        Me.Label1.Text = "[仕入・支払年月]"
        Me.Label1.Top = 4.656613E-10!
        Me.Label1.Width = 1.149!
        '
        'txt仕入支払年月ヘッダ
        '
        Me.txt仕入支払年月ヘッダ.Height = 0.15!
        Me.txt仕入支払年月ヘッダ.HyperLink = Nothing
        Me.txt仕入支払年月ヘッダ.Left = 2.88!
        Me.txt仕入支払年月ヘッダ.MultiLine = False
        Me.txt仕入支払年月ヘッダ.Name = "txt仕入支払年月ヘッダ"
        Me.txt仕入支払年月ヘッダ.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left"
        Me.txt仕入支払年月ヘッダ.Text = "2018/02"
        Me.txt仕入支払年月ヘッダ.Top = 0.15!
        Me.txt仕入支払年月ヘッダ.Width = 1.149!
        '
        'Label41
        '
        Me.Label41.Height = 0.15!
        Me.Label41.HyperLink = Nothing
        Me.Label41.Left = 9.624001!
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
        Me.Label43.Left = 8.139!
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
        Me.txt作成日ヘッダ.Left = 8.749001!
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
        Me.ReportInfo1.Left = 9.963!
        Me.ReportInfo1.MultiLine = False
        Me.ReportInfo1.Name = "ReportInfo1"
        Me.ReportInfo1.Style = "font-family: ＭＳ 明朝; font-size: 9pt"
        Me.ReportInfo1.Top = 0.15!
        Me.ReportInfo1.Width = 0.6289997!
        '
        'Label3
        '
        Me.Label3.Height = 0.16!
        Me.Label3.HyperLink = Nothing
        Me.Label3.Left = 7.423!
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
        Me.txt出力順ヘッダ.Left = 7.423!
        Me.txt出力順ヘッダ.MultiLine = False
        Me.txt出力順ヘッダ.Name = "txt出力順ヘッダ"
        Me.txt出力順ヘッダ.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left"
        Me.txt出力順ヘッダ.Text = "コード順"
        Me.txt出力順ヘッダ.Top = 0.15!
        Me.txt出力順ヘッダ.Width = 0.6309997!
        '
        'Label10
        '
        Me.Label10.Height = 0.16!
        Me.Label10.HyperLink = Nothing
        Me.Label10.Left = 4.029!
        Me.Label10.Name = "Label10"
        Me.Label10.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: top"
        Me.Label10.Text = "[日付]"
        Me.Label10.Top = 0!
        Me.Label10.Width = 1.774!
        '
        'txt日付ヘッダ
        '
        Me.txt日付ヘッダ.Height = 0.15!
        Me.txt日付ヘッダ.HyperLink = Nothing
        Me.txt日付ヘッダ.Left = 4.029!
        Me.txt日付ヘッダ.MultiLine = False
        Me.txt日付ヘッダ.Name = "txt日付ヘッダ"
        Me.txt日付ヘッダ.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left"
        Me.txt日付ヘッダ.Text = "2018/02/01 ～ 2018/02/28"
        Me.txt日付ヘッダ.Top = 0.15!
        Me.txt日付ヘッダ.Width = 1.774!
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
        Me.Detail.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.txt仕入先名, Me.txt仕入日, Me.txt伝票番号, Me.txt区分, Me.txt商品摘要, Me.txt単位, Me.txt数量, Me.txt単価, Me.txt仕入額, Me.txt消費税, Me.txt支払額, Me.txt残高, Me.Line4})
        Me.Detail.Height = 0.16!
        Me.Detail.Name = "Detail"
        '
        'txt仕入先名
        '
        Me.txt仕入先名.Height = 0.16!
        Me.txt仕入先名.HyperLink = Nothing
        Me.txt仕入先名.Left = 1.646!
        Me.txt仕入先名.MultiLine = False
        Me.txt仕入先名.Name = "txt仕入先名"
        Me.txt仕入先名.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left; vertical-align: middle"
        Me.txt仕入先名.Text = "株式会社　〇〇〇〇〇〇〇〇"
        Me.txt仕入先名.Top = 0!
        Me.txt仕入先名.Width = 1.631!
        '
        'txt仕入日
        '
        Me.txt仕入日.Height = 0.16!
        Me.txt仕入日.HyperLink = Nothing
        Me.txt仕入日.Left = 0.0000001192093!
        Me.txt仕入日.MultiLine = False
        Me.txt仕入日.Name = "txt仕入日"
        Me.txt仕入日.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left; vertical-align: middle"
        Me.txt仕入日.Text = "2018/02/01"
        Me.txt仕入日.Top = 0!
        Me.txt仕入日.Width = 0.67!
        '
        'txt伝票番号
        '
        Me.txt伝票番号.Height = 0.16!
        Me.txt伝票番号.HyperLink = Nothing
        Me.txt伝票番号.Left = 0.6700001!
        Me.txt伝票番号.MultiLine = False
        Me.txt伝票番号.Name = "txt伝票番号"
        Me.txt伝票番号.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left; vertical-align: middle"
        Me.txt伝票番号.Text = "U123456"
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
        Me.txt区分.Text = "仕入"
        Me.txt区分.Top = 0!
        Me.txt区分.Width = 0.34!
        '
        'txt商品摘要
        '
        Me.txt商品摘要.Height = 0.16!
        Me.txt商品摘要.HyperLink = Nothing
        Me.txt商品摘要.Left = 3.277!
        Me.txt商品摘要.MultiLine = False
        Me.txt商品摘要.Name = "txt商品摘要"
        Me.txt商品摘要.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left; vertical-align: middle"
        Me.txt商品摘要.Text = "いわて生協細切めかぶ(宮城産)40gx3p 12x4"
        Me.txt商品摘要.Top = 0!
        Me.txt商品摘要.Width = 2.252!
        '
        'txt単位
        '
        Me.txt単位.Height = 0.16!
        Me.txt単位.HyperLink = Nothing
        Me.txt単位.Left = 6.215!
        Me.txt単位.MultiLine = False
        Me.txt単位.Name = "txt単位"
        Me.txt単位.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: center; vertical-align: middle"
        Me.txt単位.Text = "Kg"
        Me.txt単位.Top = 0!
        Me.txt単位.Width = 0.211!
        '
        'txt数量
        '
        Me.txt数量.Height = 0.16!
        Me.txt数量.Left = 5.529!
        Me.txt数量.MultiLine = False
        Me.txt数量.Name = "txt数量"
        Me.txt数量.OutputFormat = resources.GetString("txt数量.OutputFormat")
        Me.txt数量.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt数量.Text = "12,345.00"
        Me.txt数量.Top = 0!
        Me.txt数量.Width = 0.686!
        '
        'txt単価
        '
        Me.txt単価.Height = 0.16!
        Me.txt単価.Left = 6.426001!
        Me.txt単価.MultiLine = False
        Me.txt単価.Name = "txt単価"
        Me.txt単価.OutputFormat = resources.GetString("txt単価.OutputFormat")
        Me.txt単価.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt単価.Text = "12,345.00"
        Me.txt単価.Top = 0!
        Me.txt単価.Width = 0.705!
        '
        'txt仕入額
        '
        Me.txt仕入額.DataField = "fld仕入額"
        Me.txt仕入額.Height = 0.16!
        Me.txt仕入額.Left = 7.131!
        Me.txt仕入額.MultiLine = False
        Me.txt仕入額.Name = "txt仕入額"
        Me.txt仕入額.OutputFormat = resources.GetString("txt仕入額.OutputFormat")
        Me.txt仕入額.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt仕入額.Text = "12,345"
        Me.txt仕入額.Top = 0!
        Me.txt仕入額.Width = 0.705!
        '
        'txt消費税
        '
        Me.txt消費税.DataField = "fld消費税"
        Me.txt消費税.Height = 0.16!
        Me.txt消費税.Left = 7.836!
        Me.txt消費税.MultiLine = False
        Me.txt消費税.Name = "txt消費税"
        Me.txt消費税.OutputFormat = resources.GetString("txt消費税.OutputFormat")
        Me.txt消費税.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt消費税.Text = "12,345"
        Me.txt消費税.Top = 0!
        Me.txt消費税.Width = 0.705!
        '
        'txt支払額
        '
        Me.txt支払額.DataField = "fld支払額"
        Me.txt支払額.Height = 0.16!
        Me.txt支払額.Left = 8.541!
        Me.txt支払額.MultiLine = False
        Me.txt支払額.Name = "txt支払額"
        Me.txt支払額.OutputFormat = resources.GetString("txt支払額.OutputFormat")
        Me.txt支払額.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt支払額.Text = "12,345"
        Me.txt支払額.Top = 0!
        Me.txt支払額.Width = 0.705!
        '
        'txt残高
        '
        Me.txt残高.DataField = "fld残高"
        Me.txt残高.Height = 0.16!
        Me.txt残高.Left = 9.246!
        Me.txt残高.MultiLine = False
        Me.txt残高.Name = "txt残高"
        Me.txt残高.OutputFormat = resources.GetString("txt残高.OutputFormat")
        Me.txt残高.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right; vertical-align: middle"
        Me.txt残高.Text = "30,240"
        Me.txt残高.Top = 0!
        Me.txt残高.Width = 0.705!
        '
        'Line4
        '
        Me.Line4.Height = 0!
        Me.Line4.Left = 0!
        Me.Line4.LineStyle = GrapeCity.ActiveReports.SectionReportModel.LineStyle.Dash
        Me.Line4.LineWeight = 1.0!
        Me.Line4.Name = "Line4"
        Me.Line4.Top = 0!
        Me.Line4.Width = 10.69!
        Me.Line4.X1 = 0!
        Me.Line4.X2 = 10.69!
        Me.Line4.Y1 = 0!
        Me.Line4.Y2 = 0!
        '
        'PageFooter
        '
        Me.PageFooter.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.txt会社名, Me.Line5})
        Me.PageFooter.Height = 0.16!
        Me.PageFooter.Name = "PageFooter"
        '
        'txt会社名
        '
        Me.txt会社名.Height = 0.16!
        Me.txt会社名.HyperLink = Nothing
        Me.txt会社名.Left = 8.410001!
        Me.txt会社名.Name = "txt会社名"
        Me.txt会社名.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-style: italic; font-weight: normal; te" &
    "xt-align: right; vertical-align: middle"
        Me.txt会社名.Text = "株式会社　カネキ吉田商店"
        Me.txt会社名.Top = 0!
        Me.txt会社名.Width = 2.181501!
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
        'ReportHeader1
        '
        Me.ReportHeader1.Height = 0!
        Me.ReportHeader1.Name = "ReportHeader1"
        Me.ReportHeader1.Visible = False
        '
        'ReportFooter1
        '
        Me.ReportFooter1.Height = 0!
        Me.ReportFooter1.Name = "ReportFooter1"
        '
        'gh支払先
        '
        Me.gh支払先.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.Label6, Me.Label7, Me.Label18, Me.lbl当月残, Me.Label4, Me.Label12, Me.Label17, Me.Label11, Me.Label5, Me.txt支払先コード, Me.txt支払先名, Me.Label13, Me.Label14, Me.Label16, Me.Line2, Me.Line1})
        Me.gh支払先.DataField = "fld支払先"
        Me.gh支払先.Height = 0.32!
        Me.gh支払先.Name = "gh支払先"
        Me.gh支払先.NewPage = GrapeCity.ActiveReports.SectionReportModel.NewPage.Before
        '
        'Label6
        '
        Me.Label6.Height = 0.15!
        Me.Label6.HyperLink = Nothing
        Me.Label6.Left = 1.32!
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
        Me.Label7.Left = 0.014!
        Me.Label7.Name = "Label7"
        Me.Label7.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: center; ve" &
    "rtical-align: top"
        Me.Label7.Text = "仕入日"
        Me.Label7.Top = 0.17!
        Me.Label7.Width = 0.67!
        '
        'Label18
        '
        Me.Label18.Height = 0.15!
        Me.Label18.HyperLink = Nothing
        Me.Label18.Left = 5.543!
        Me.Label18.Name = "Label18"
        Me.Label18.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label18.Text = "数量"
        Me.Label18.Top = 0.17!
        Me.Label18.Width = 0.686!
        '
        'lbl当月残
        '
        Me.lbl当月残.Height = 0.15!
        Me.lbl当月残.HyperLink = Nothing
        Me.lbl当月残.Left = 8.555!
        Me.lbl当月残.Name = "lbl当月残"
        Me.lbl当月残.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.lbl当月残.Text = "支払額"
        Me.lbl当月残.Top = 0.17!
        Me.lbl当月残.Width = 0.705!
        '
        'Label4
        '
        Me.Label4.Height = 0.15!
        Me.Label4.HyperLink = Nothing
        Me.Label4.Left = 6.440001!
        Me.Label4.Name = "Label4"
        Me.Label4.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label4.Text = "単価"
        Me.Label4.Top = 0.17!
        Me.Label4.Width = 0.705!
        '
        'Label12
        '
        Me.Label12.Height = 0.15!
        Me.Label12.HyperLink = Nothing
        Me.Label12.Left = 7.145!
        Me.Label12.Name = "Label12"
        Me.Label12.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label12.Text = "仕入額"
        Me.Label12.Top = 0.17!
        Me.Label12.Width = 0.705!
        '
        'Label17
        '
        Me.Label17.Height = 0.15!
        Me.Label17.HyperLink = Nothing
        Me.Label17.Left = 7.85!
        Me.Label17.Name = "Label17"
        Me.Label17.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label17.Text = "消費税"
        Me.Label17.Top = 0.17!
        Me.Label17.Width = 0.705!
        '
        'Label11
        '
        Me.Label11.Height = 0.15!
        Me.Label11.HyperLink = Nothing
        Me.Label11.Left = 0.684!
        Me.Label11.Name = "Label11"
        Me.Label11.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: top"
        Me.Label11.Text = "伝票番号"
        Me.Label11.Top = 0.17!
        Me.Label11.Width = 0.636!
        '
        'Label5
        '
        Me.Label5.Height = 0.15!
        Me.Label5.HyperLink = Nothing
        Me.Label5.Left = 0.02799988!
        Me.Label5.Name = "Label5"
        Me.Label5.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: top"
        Me.Label5.Text = "支払先："
        Me.Label5.Top = 0!
        Me.Label5.Width = 0.6309997!
        '
        'txt支払先コード
        '
        Me.txt支払先コード.Height = 0.15!
        Me.txt支払先コード.HyperLink = Nothing
        Me.txt支払先コード.Left = 0.659!
        Me.txt支払先コード.MultiLine = False
        Me.txt支払先コード.Name = "txt支払先コード"
        Me.txt支払先コード.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left"
        Me.txt支払先コード.Text = "100250"
        Me.txt支払先コード.Top = 0!
        Me.txt支払先コード.Width = 0.4330001!
        '
        'txt支払先名
        '
        Me.txt支払先名.Height = 0.15!
        Me.txt支払先名.HyperLink = Nothing
        Me.txt支払先名.Left = 1.092!
        Me.txt支払先名.MultiLine = False
        Me.txt支払先名.Name = "txt支払先名"
        Me.txt支払先名.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: left"
        Me.txt支払先名.Text = "仙台水産"
        Me.txt支払先名.Top = 0!
        Me.txt支払先名.Width = 2.86!
        '
        'Label13
        '
        Me.Label13.Height = 0.15!
        Me.Label13.HyperLink = Nothing
        Me.Label13.Left = 1.66!
        Me.Label13.Name = "Label13"
        Me.Label13.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: top"
        Me.Label13.Text = "仕入先"
        Me.Label13.Top = 0.17!
        Me.Label13.Width = 1.631!
        '
        'Label14
        '
        Me.Label14.Height = 0.15!
        Me.Label14.HyperLink = Nothing
        Me.Label14.Left = 3.291!
        Me.Label14.Name = "Label14"
        Me.Label14.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: left; vert" &
    "ical-align: top"
        Me.Label14.Text = "商品/摘要"
        Me.Label14.Top = 0.17!
        Me.Label14.Width = 2.252!
        '
        'Label16
        '
        Me.Label16.Height = 0.15!
        Me.Label16.HyperLink = Nothing
        Me.Label16.Left = 9.26!
        Me.Label16.Name = "Label16"
        Me.Label16.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: top"
        Me.Label16.Text = "残高"
        Me.Label16.Top = 0.17!
        Me.Label16.Width = 0.705!
        '
        'Line2
        '
        Me.Line2.Height = 0!
        Me.Line2.Left = 0!
        Me.Line2.LineWeight = 1.0!
        Me.Line2.Name = "Line2"
        Me.Line2.Top = 0.16!
        Me.Line2.Width = 10.69!
        Me.Line2.X1 = 0!
        Me.Line2.X2 = 10.69!
        Me.Line2.Y1 = 0.16!
        Me.Line2.Y2 = 0.16!
        '
        'Line1
        '
        Me.Line1.Height = 0!
        Me.Line1.Left = 0!
        Me.Line1.LineWeight = 2.0!
        Me.Line1.Name = "Line1"
        Me.Line1.Top = 0.32!
        Me.Line1.Width = 10.79!
        Me.Line1.X1 = 0!
        Me.Line1.X2 = 10.79!
        Me.Line1.Y1 = 0.32!
        Me.Line1.Y2 = 0.32!
        '
        'gf支払先
        '
        Me.gf支払先.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.txt仕入額計, Me.txt消費税計, Me.txt支払額計, Me.txt残高計, Me.Label35, Me.Line6})
        Me.gf支払先.Height = 0.16!
        Me.gf支払先.Name = "gf支払先"
        '
        'txt仕入額計
        '
        Me.txt仕入額計.DataField = "fld仕入額"
        Me.txt仕入額計.Height = 0.14!
        Me.txt仕入額計.Left = 7.145!
        Me.txt仕入額計.MultiLine = False
        Me.txt仕入額計.Name = "txt仕入額計"
        Me.txt仕入額計.OutputFormat = resources.GetString("txt仕入額計.OutputFormat")
        Me.txt仕入額計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt仕入額計.SummaryGroup = "gh支払先"
        Me.txt仕入額計.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.Group
        Me.txt仕入額計.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.SubTotal
        Me.txt仕入額計.Text = "123,456,789"
        Me.txt仕入額計.Top = 0!
        Me.txt仕入額計.Width = 0.705!
        '
        'txt消費税計
        '
        Me.txt消費税計.DataField = "fld消費税"
        Me.txt消費税計.Height = 0.14!
        Me.txt消費税計.Left = 7.85!
        Me.txt消費税計.MultiLine = False
        Me.txt消費税計.Name = "txt消費税計"
        Me.txt消費税計.OutputFormat = resources.GetString("txt消費税計.OutputFormat")
        Me.txt消費税計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt消費税計.SummaryGroup = "gh支払先"
        Me.txt消費税計.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.Group
        Me.txt消費税計.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.SubTotal
        Me.txt消費税計.Text = "12,345,678"
        Me.txt消費税計.Top = 0!
        Me.txt消費税計.Width = 0.705!
        '
        'txt支払額計
        '
        Me.txt支払額計.DataField = "fld支払額"
        Me.txt支払額計.Height = 0.14!
        Me.txt支払額計.Left = 8.555!
        Me.txt支払額計.MultiLine = False
        Me.txt支払額計.Name = "txt支払額計"
        Me.txt支払額計.OutputFormat = resources.GetString("txt支払額計.OutputFormat")
        Me.txt支払額計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt支払額計.SummaryGroup = "gh支払先"
        Me.txt支払額計.SummaryRunning = GrapeCity.ActiveReports.SectionReportModel.SummaryRunning.Group
        Me.txt支払額計.SummaryType = GrapeCity.ActiveReports.SectionReportModel.SummaryType.SubTotal
        Me.txt支払額計.Text = "12,345,678"
        Me.txt支払額計.Top = 0!
        Me.txt支払額計.Width = 0.705!
        '
        'txt残高計
        '
        Me.txt残高計.DataField = "fld残高"
        Me.txt残高計.Height = 0.14!
        Me.txt残高計.Left = 9.26!
        Me.txt残高計.MultiLine = False
        Me.txt残高計.Name = "txt残高計"
        Me.txt残高計.OutputFormat = resources.GetString("txt残高計.OutputFormat")
        Me.txt残高計.Style = "font-family: ＭＳ 明朝; font-size: 9pt; text-align: right"
        Me.txt残高計.Text = "12,345,678"
        Me.txt残高計.Top = 0!
        Me.txt残高計.Width = 0.705!
        '
        'Label35
        '
        Me.Label35.Height = 0.14!
        Me.Label35.HyperLink = Nothing
        Me.Label35.Left = 6.145!
        Me.Label35.Name = "Label35"
        Me.Label35.Style = "font-family: ＭＳ ゴシック; font-size: 9pt; font-weight: normal; text-align: right; ver" &
    "tical-align: middle"
        Me.Label35.Text = "★[得意先計]"
        Me.Label35.Top = 0!
        Me.Label35.Width = 0.9999993!
        '
        'Line6
        '
        Me.Line6.Height = 0!
        Me.Line6.Left = 0!
        Me.Line6.LineWeight = 1.0!
        Me.Line6.Name = "Line6"
        Me.Line6.Top = 0!
        Me.Line6.Width = 10.69!
        Me.Line6.X1 = 0!
        Me.Line6.X2 = 10.69!
        Me.Line6.Y1 = 0!
        Me.Line6.Y2 = 0!
        '
        'fld支払先
        '
        Me.fld支払先.DefaultValue = Nothing
        Me.fld支払先.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld支払先.Formula = Nothing
        Me.fld支払先.Name = "fld支払先"
        Me.fld支払先.Tag = Nothing
        '
        'fld仕入額
        '
        Me.fld仕入額.DefaultValue = Nothing
        Me.fld仕入額.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld仕入額.Formula = Nothing
        Me.fld仕入額.Name = "fld仕入額"
        Me.fld仕入額.Tag = Nothing
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
        'fld残高
        '
        Me.fld残高.DefaultValue = Nothing
        Me.fld残高.FieldType = GrapeCity.ActiveReports.Data.FieldTypeEnum.None
        Me.fld残高.Formula = Nothing
        Me.fld残高.Name = "fld残高"
        Me.fld残高.Tag = Nothing
        '
        'H11R03_ShiiresakiMotoList
        '
        Me.MasterReport = False
        Me.CalculatedFields.Add(Me.fld支払先)
        Me.CalculatedFields.Add(Me.fld仕入額)
        Me.CalculatedFields.Add(Me.fld消費税)
        Me.CalculatedFields.Add(Me.fld支払額)
        Me.CalculatedFields.Add(Me.fld残高)
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
        Me.Sections.Add(Me.gh支払先)
        Me.Sections.Add(Me.Detail)
        Me.Sections.Add(Me.gf支払先)
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
        CType(Me.Label41, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label43, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt作成日ヘッダ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReportInfo1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt出力順ヘッダ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt日付ヘッダ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt仕入先名, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt仕入日, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt伝票番号, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt区分, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt商品摘要, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt単位, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt数量, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt単価, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt仕入額, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt消費税, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt支払額, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt残高, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt会社名, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbl当月残, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt支払先コード, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt支払先名, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt仕入額計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt消費税計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt支払額計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txt残高計, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label35, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

    Private WithEvents ReportHeader1 As GrapeCity.ActiveReports.SectionReportModel.ReportHeader
    Private WithEvents ReportFooter1 As GrapeCity.ActiveReports.SectionReportModel.ReportFooter
    Private WithEvents gh支払先 As GrapeCity.ActiveReports.SectionReportModel.GroupHeader
    Private WithEvents gf支払先 As GrapeCity.ActiveReports.SectionReportModel.GroupFooter
    Private WithEvents txtPrm As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents Label2 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt区分ヘッダ As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label1 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt仕入支払年月ヘッダ As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label41 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label43 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt作成日ヘッダ As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents ReportInfo1 As GrapeCity.ActiveReports.SectionReportModel.ReportInfo
    Private WithEvents Label3 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt出力順ヘッダ As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label10 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt日付ヘッダ As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Line3 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents Label6 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label7 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label18 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents lbl当月残 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label4 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label12 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label17 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label11 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label5 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt支払先コード As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt支払先名 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label13 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label14 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label16 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Line2 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents Line1 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents txt仕入先名 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt仕入日 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt伝票番号 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt区分 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt商品摘要 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt単位 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txt数量 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt単価 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt仕入額 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt消費税 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt支払額 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt残高 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents Line4 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents txt仕入額計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt消費税計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt支払額計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txt残高計 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents Label35 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Line6 As GrapeCity.ActiveReports.SectionReportModel.Line
    Private WithEvents txt会社名 As GrapeCity.ActiveReports.SectionReportModel.Label
    Friend WithEvents fld支払先 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld仕入額 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld消費税 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld支払額 As GrapeCity.ActiveReports.Data.Field
    Friend WithEvents fld残高 As GrapeCity.ActiveReports.Data.Field
    Private WithEvents Line5 As GrapeCity.ActiveReports.SectionReportModel.Line
End Class
