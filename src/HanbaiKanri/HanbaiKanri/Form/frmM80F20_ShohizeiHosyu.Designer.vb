<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmM80F20_ShohizeiHosyu
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblKousinsya = New System.Windows.Forms.Label()
        Me.lblKousinbi = New System.Windows.Forms.Label()
        Me.TableLayoutPanel13 = New System.Windows.Forms.TableLayoutPanel()
        Me.dtpTekiyoFrDt = New CustomControl.NullableDateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTekiyoFrDt = New System.Windows.Forms.TextBox()
        Me.lblShoriMode = New System.Windows.Forms.Label()
        Me.TableLayoutPanel10 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel12 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtTekiyoToDt = New System.Windows.Forms.TextBox()
        Me.dtpTekiyoToDt = New CustomControl.NullableDateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel15 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtShohizeiRitsu = New CustomControl.TextBoxNum()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmdModoru = New System.Windows.Forms.Button()
        Me.cmdKakutei = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        Me.TableLayoutPanel13.SuspendLayout()
        Me.TableLayoutPanel10.SuspendLayout()
        Me.TableLayoutPanel12.SuspendLayout()
        Me.TableLayoutPanel15.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel4, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 76.3285!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.6715!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(734, 207)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel3, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel10, 1, 2)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 3
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(728, 151)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 79.33194!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.66806!))
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel6, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.lblShoriMode, 1, 0)
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(30, 0)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(668, 65)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 2
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.lblKousinbi, 1, 2)
        Me.TableLayoutPanel6.Controls.Add(Me.TableLayoutPanel13, 0, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.lblKousinsya, 1, 1)
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel6.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 3
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(527, 65)
        Me.TableLayoutPanel6.TabIndex = 2
        '
        'lblKousinsya
        '
        Me.lblKousinsya.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblKousinsya.BackColor = System.Drawing.Color.Transparent
        Me.lblKousinsya.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKousinsya.Location = New System.Drawing.Point(316, 15)
        Me.lblKousinsya.Margin = New System.Windows.Forms.Padding(0)
        Me.lblKousinsya.Name = "lblKousinsya"
        Me.lblKousinsya.Size = New System.Drawing.Size(202, 22)
        Me.lblKousinsya.TabIndex = 34
        Me.lblKousinsya.Text = "更新者：XXXXXXXX"
        Me.lblKousinsya.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblKousinbi
        '
        Me.lblKousinbi.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblKousinbi.BackColor = System.Drawing.Color.Transparent
        Me.lblKousinbi.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKousinbi.Location = New System.Drawing.Point(316, 43)
        Me.lblKousinbi.Margin = New System.Windows.Forms.Padding(0)
        Me.lblKousinbi.Name = "lblKousinbi"
        Me.lblKousinbi.Size = New System.Drawing.Size(202, 22)
        Me.lblKousinbi.TabIndex = 35
        Me.lblKousinbi.Text = "更新日：9999/99/99 00:00:00"
        Me.lblKousinbi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TableLayoutPanel13
        '
        Me.TableLayoutPanel13.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel13.ColumnCount = 3
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel13.Controls.Add(Me.dtpTekiyoFrDt, 0, 0)
        Me.TableLayoutPanel13.Controls.Add(Me.Label3, 0, 0)
        Me.TableLayoutPanel13.Controls.Add(Me.txtTekiyoFrDt, 2, 0)
        Me.TableLayoutPanel13.Location = New System.Drawing.Point(0, 15)
        Me.TableLayoutPanel13.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel13.Name = "TableLayoutPanel13"
        Me.TableLayoutPanel13.RowCount = 1
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel13.Size = New System.Drawing.Size(316, 22)
        Me.TableLayoutPanel13.TabIndex = 3
        '
        'dtpTekiyoFrDt
        '
        Me.dtpTekiyoFrDt.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtpTekiyoFrDt.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTekiyoFrDt.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.dtpTekiyoFrDt.Location = New System.Drawing.Point(140, 0)
        Me.dtpTekiyoFrDt.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpTekiyoFrDt.Name = "dtpTekiyoFrDt"
        Me.dtpTekiyoFrDt.NullValue = ""
        Me.dtpTekiyoFrDt.Size = New System.Drawing.Size(110, 22)
        Me.dtpTekiyoFrDt.TabIndex = 0
        Me.dtpTekiyoFrDt.Value = New Date(2018, 3, 22, 14, 31, 6, 795)
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Margin = New System.Windows.Forms.Padding(0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(140, 22)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "適用開始日"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTekiyoFrDt
        '
        Me.txtTekiyoFrDt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTekiyoFrDt.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTekiyoFrDt.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtTekiyoFrDt.Location = New System.Drawing.Point(250, 0)
        Me.txtTekiyoFrDt.Margin = New System.Windows.Forms.Padding(0)
        Me.txtTekiyoFrDt.MaxLength = 100
        Me.txtTekiyoFrDt.Name = "txtTekiyoFrDt"
        Me.txtTekiyoFrDt.ReadOnly = True
        Me.txtTekiyoFrDt.Size = New System.Drawing.Size(106, 22)
        Me.txtTekiyoFrDt.TabIndex = 11
        Me.txtTekiyoFrDt.TabStop = False
        '
        'lblShoriMode
        '
        Me.lblShoriMode.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblShoriMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblShoriMode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblShoriMode.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShoriMode.Location = New System.Drawing.Point(532, 0)
        Me.lblShoriMode.Name = "lblShoriMode"
        Me.lblShoriMode.Size = New System.Drawing.Size(133, 65)
        Me.lblShoriMode.TabIndex = 19
        Me.lblShoriMode.Text = "登録"
        Me.lblShoriMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel10
        '
        Me.TableLayoutPanel10.ColumnCount = 1
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel10.Controls.Add(Me.TableLayoutPanel12, 0, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.TableLayoutPanel15, 0, 1)
        Me.TableLayoutPanel10.Location = New System.Drawing.Point(30, 86)
        Me.TableLayoutPanel10.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel10.Name = "TableLayoutPanel10"
        Me.TableLayoutPanel10.RowCount = 2
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel10.Size = New System.Drawing.Size(668, 54)
        Me.TableLayoutPanel10.TabIndex = 5
        '
        'TableLayoutPanel12
        '
        Me.TableLayoutPanel12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel12.ColumnCount = 3
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel12.Controls.Add(Me.txtTekiyoToDt, 0, 0)
        Me.TableLayoutPanel12.Controls.Add(Me.dtpTekiyoToDt, 0, 0)
        Me.TableLayoutPanel12.Controls.Add(Me.Label4, 0, 0)
        Me.TableLayoutPanel12.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel12.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel12.Name = "TableLayoutPanel12"
        Me.TableLayoutPanel12.RowCount = 1
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel12.Size = New System.Drawing.Size(668, 22)
        Me.TableLayoutPanel12.TabIndex = 0
        '
        'txtTekiyoToDt
        '
        Me.txtTekiyoToDt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtTekiyoToDt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTekiyoToDt.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTekiyoToDt.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtTekiyoToDt.Location = New System.Drawing.Point(250, 0)
        Me.txtTekiyoToDt.Margin = New System.Windows.Forms.Padding(0)
        Me.txtTekiyoToDt.MaxLength = 100
        Me.txtTekiyoToDt.Name = "txtTekiyoToDt"
        Me.txtTekiyoToDt.ReadOnly = True
        Me.txtTekiyoToDt.Size = New System.Drawing.Size(106, 22)
        Me.txtTekiyoToDt.TabIndex = 12
        Me.txtTekiyoToDt.TabStop = False
        '
        'dtpTekiyoToDt
        '
        Me.dtpTekiyoToDt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dtpTekiyoToDt.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtpTekiyoToDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTekiyoToDt.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.dtpTekiyoToDt.Location = New System.Drawing.Point(140, 0)
        Me.dtpTekiyoToDt.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpTekiyoToDt.Name = "dtpTekiyoToDt"
        Me.dtpTekiyoToDt.NullValue = ""
        Me.dtpTekiyoToDt.Size = New System.Drawing.Size(110, 22)
        Me.dtpTekiyoToDt.TabIndex = 0
        Me.dtpTekiyoToDt.Value = New Date(2018, 3, 22, 14, 31, 6, 795)
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Margin = New System.Windows.Forms.Padding(0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(140, 22)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "適用終了日"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel15
        '
        Me.TableLayoutPanel15.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel15.ColumnCount = 2
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel15.Controls.Add(Me.txtShohizeiRitsu, 0, 0)
        Me.TableLayoutPanel15.Controls.Add(Me.Label8, 0, 0)
        Me.TableLayoutPanel15.Location = New System.Drawing.Point(0, 32)
        Me.TableLayoutPanel15.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel15.Name = "TableLayoutPanel15"
        Me.TableLayoutPanel15.RowCount = 1
        Me.TableLayoutPanel15.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel15.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29.0!))
        Me.TableLayoutPanel15.Size = New System.Drawing.Size(668, 22)
        Me.TableLayoutPanel15.TabIndex = 1
        '
        'txtShohizeiRitsu
        '
        Me.txtShohizeiRitsu.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtShohizeiRitsu.CommaFormat = True
        Me.txtShohizeiRitsu.DecimalDigit = CType(3, Short)
        Me.txtShohizeiRitsu.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShohizeiRitsu.ForeColor = System.Drawing.Color.Black
        Me.txtShohizeiRitsu.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtShohizeiRitsu.IntegralDigit = CType(1, Short)
        Me.txtShohizeiRitsu.Location = New System.Drawing.Point(140, 0)
        Me.txtShohizeiRitsu.Margin = New System.Windows.Forms.Padding(0)
        Me.txtShohizeiRitsu.MaxLength = 5
        Me.txtShohizeiRitsu.MinusInput = False
        Me.txtShohizeiRitsu.Name = "txtShohizeiRitsu"
        Me.txtShohizeiRitsu.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57), Global.Microsoft.VisualBasic.ChrW(46)}
        Me.txtShohizeiRitsu.Size = New System.Drawing.Size(59, 22)
        Me.txtShohizeiRitsu.TabIndex = 0
        Me.txtShohizeiRitsu.Text = "0.987"
        Me.txtShohizeiRitsu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtShohizeiRitsu.Value = "0.987"
        Me.txtShohizeiRitsu.ZeroSuppress = True
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 1)
        Me.Label8.Margin = New System.Windows.Forms.Padding(0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(140, 21)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "消費税率"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 6
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.1579!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.78947!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.263159!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.78947!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.cmdModoru, 4, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.cmdKakutei, 2, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(3, 160)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(728, 44)
        Me.TableLayoutPanel4.TabIndex = 3
        '
        'cmdModoru
        '
        Me.cmdModoru.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdModoru.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdModoru.Location = New System.Drawing.Point(594, 6)
        Me.cmdModoru.Name = "cmdModoru"
        Me.cmdModoru.Size = New System.Drawing.Size(99, 32)
        Me.cmdModoru.TabIndex = 1
        Me.cmdModoru.Text = "戻　る(&B)"
        Me.cmdModoru.UseVisualStyleBackColor = True
        '
        'cmdKakutei
        '
        Me.cmdKakutei.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdKakutei.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdKakutei.Location = New System.Drawing.Point(454, 6)
        Me.cmdKakutei.Name = "cmdKakutei"
        Me.cmdKakutei.Size = New System.Drawing.Size(99, 32)
        Me.cmdKakutei.TabIndex = 0
        Me.cmdKakutei.Text = "確　定(&G)"
        Me.cmdKakutei.UseVisualStyleBackColor = True
        '
        'frmM80F20_ShohizeiHosyu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(734, 207)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "frmM80F20_ShohizeiHosyu"
        Me.Text = "消費税マスタ保守（M80F20）"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel13.ResumeLayout(False)
        Me.TableLayoutPanel13.PerformLayout()
        Me.TableLayoutPanel10.ResumeLayout(False)
        Me.TableLayoutPanel12.ResumeLayout(False)
        Me.TableLayoutPanel12.PerformLayout()
        Me.TableLayoutPanel15.ResumeLayout(False)
        Me.TableLayoutPanel15.PerformLayout()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents cmdModoru As Button
    Friend WithEvents cmdKakutei As Button
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents lblKousinsya As Label
    Friend WithEvents lblKousinbi As Label
    Friend WithEvents TableLayoutPanel10 As TableLayoutPanel
    Friend WithEvents Label8 As Label
    Friend WithEvents TableLayoutPanel15 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel6 As TableLayoutPanel
    Friend WithEvents lblShoriMode As Label
    Friend WithEvents TableLayoutPanel13 As TableLayoutPanel
    Friend WithEvents Label3 As Label
    Friend WithEvents dtpTekiyoFrDt As CustomControl.NullableDateTimePicker
    Friend WithEvents txtShohizeiRitsu As CustomControl.TextBoxNum
    Friend WithEvents TableLayoutPanel12 As TableLayoutPanel
    Friend WithEvents dtpTekiyoToDt As CustomControl.NullableDateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents txtTekiyoFrDt As TextBox
    Friend WithEvents txtTekiyoToDt As TextBox
End Class
