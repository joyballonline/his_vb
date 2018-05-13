<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmM50F20_SiireTankaHosyu
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
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtShohinCode = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtShohinName = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtToriName = New System.Windows.Forms.TextBox()
        Me.txtToriCode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel13 = New System.Windows.Forms.TableLayoutPanel()
        Me.dtpTekiyoFrDt = New CustomControl.NullableDateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTekiyoFrDt = New System.Windows.Forms.TextBox()
        Me.lblShoriMode = New System.Windows.Forms.Label()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel10 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel12 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtTekiyoToDt = New System.Windows.Forms.TextBox()
        Me.dtpTekiyoToDt = New CustomControl.NullableDateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel11 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtMemo = New System.Windows.Forms.TextBox()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel15 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtSiireTanka = New CustomControl.TextBoxNum()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmdModoru = New System.Windows.Forms.Button()
        Me.cmdKakutei = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        Me.TableLayoutPanel8.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        Me.TableLayoutPanel13.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.TableLayoutPanel10.SuspendLayout()
        Me.TableLayoutPanel12.SuspendLayout()
        Me.TableLayoutPanel11.SuspendLayout()
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
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1030, 314)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel3, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel5, 1, 2)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 3
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(1024, 245)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 79.33194!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.66806!))
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel6, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.lblShoriMode, 1, 0)
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(33, 3)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(958, 88)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 2
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.78353!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.21647!))
        Me.TableLayoutPanel6.Controls.Add(Me.lblKousinsya, 1, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.lblKousinbi, 1, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.TableLayoutPanel8, 0, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.TableLayoutPanel7, 0, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.TableLayoutPanel13, 0, 2)
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 3
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(753, 81)
        Me.TableLayoutPanel6.TabIndex = 2
        '
        'lblKousinsya
        '
        Me.lblKousinsya.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblKousinsya.BackColor = System.Drawing.Color.Transparent
        Me.lblKousinsya.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKousinsya.Location = New System.Drawing.Point(532, 5)
        Me.lblKousinsya.Margin = New System.Windows.Forms.Padding(0)
        Me.lblKousinsya.Name = "lblKousinsya"
        Me.lblKousinsya.Size = New System.Drawing.Size(221, 22)
        Me.lblKousinsya.TabIndex = 34
        Me.lblKousinsya.Text = "更新者：XXXXXXXX"
        Me.lblKousinsya.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblKousinbi
        '
        Me.lblKousinbi.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblKousinbi.BackColor = System.Drawing.Color.Transparent
        Me.lblKousinbi.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKousinbi.Location = New System.Drawing.Point(532, 32)
        Me.lblKousinbi.Margin = New System.Windows.Forms.Padding(0)
        Me.lblKousinbi.Name = "lblKousinbi"
        Me.lblKousinbi.Size = New System.Drawing.Size(221, 22)
        Me.lblKousinbi.TabIndex = 35
        Me.lblKousinbi.Text = "更新日：9999/99/99 00:00:00"
        Me.lblKousinbi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.ColumnCount = 3
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel8.Controls.Add(Me.txtShohinCode, 1, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.Label5, 0, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.txtShohinName, 2, 0)
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel8.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 1
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(532, 26)
        Me.TableLayoutPanel8.TabIndex = 0
        '
        'txtShohinCode
        '
        Me.txtShohinCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtShohinCode.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShohinCode.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtShohinCode.Location = New System.Drawing.Point(140, 0)
        Me.txtShohinCode.Margin = New System.Windows.Forms.Padding(0)
        Me.txtShohinCode.MaxLength = 5
        Me.txtShohinCode.Name = "txtShohinCode"
        Me.txtShohinCode.Size = New System.Drawing.Size(48, 22)
        Me.txtShohinCode.TabIndex = 0
        Me.txtShohinCode.Text = "12345"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Margin = New System.Windows.Forms.Padding(0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(140, 22)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "商品"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtShohinName
        '
        Me.txtShohinName.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtShohinName.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShohinName.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf
        Me.txtShohinName.Location = New System.Drawing.Point(188, 0)
        Me.txtShohinName.Margin = New System.Windows.Forms.Padding(0)
        Me.txtShohinName.MaxLength = 20
        Me.txtShohinName.Name = "txtShohinName"
        Me.txtShohinName.ReadOnly = True
        Me.txtShohinName.Size = New System.Drawing.Size(302, 22)
        Me.txtShohinName.TabIndex = 31
        Me.txtShohinName.TabStop = False
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.ColumnCount = 3
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel7.Controls.Add(Me.txtToriName, 2, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.txtToriCode, 1, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(0, 27)
        Me.TableLayoutPanel7.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 1
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(532, 24)
        Me.TableLayoutPanel7.TabIndex = 2
        '
        'txtToriName
        '
        Me.txtToriName.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtToriName.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtToriName.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf
        Me.txtToriName.Location = New System.Drawing.Point(215, 0)
        Me.txtToriName.Margin = New System.Windows.Forms.Padding(0)
        Me.txtToriName.MaxLength = 20
        Me.txtToriName.Name = "txtToriName"
        Me.txtToriName.ReadOnly = True
        Me.txtToriName.Size = New System.Drawing.Size(275, 22)
        Me.txtToriName.TabIndex = 32
        Me.txtToriName.TabStop = False
        '
        'txtToriCode
        '
        Me.txtToriCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtToriCode.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtToriCode.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtToriCode.Location = New System.Drawing.Point(140, 0)
        Me.txtToriCode.Margin = New System.Windows.Forms.Padding(0)
        Me.txtToriCode.MaxLength = 8
        Me.txtToriCode.Name = "txtToriCode"
        Me.txtToriCode.Size = New System.Drawing.Size(75, 22)
        Me.txtToriCode.TabIndex = 0
        Me.txtToriCode.Text = "12345678"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(140, 22)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "取引先"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel13
        '
        Me.TableLayoutPanel13.ColumnCount = 3
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel13.Controls.Add(Me.dtpTekiyoFrDt, 0, 0)
        Me.TableLayoutPanel13.Controls.Add(Me.Label3, 0, 0)
        Me.TableLayoutPanel13.Controls.Add(Me.txtTekiyoFrDt, 2, 0)
        Me.TableLayoutPanel13.Location = New System.Drawing.Point(0, 54)
        Me.TableLayoutPanel13.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel13.Name = "TableLayoutPanel13"
        Me.TableLayoutPanel13.RowCount = 1
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel13.Size = New System.Drawing.Size(532, 22)
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
        Me.lblShoriMode.Location = New System.Drawing.Point(762, 11)
        Me.lblShoriMode.Name = "lblShoriMode"
        Me.lblShoriMode.Size = New System.Drawing.Size(193, 66)
        Me.lblShoriMode.TabIndex = 19
        Me.lblShoriMode.Text = "登録"
        Me.lblShoriMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.AutoSize = True
        Me.TableLayoutPanel5.ColumnCount = 1
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.TableLayoutPanel10, 0, 0)
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(33, 135)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 107.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(958, 107)
        Me.TableLayoutPanel5.TabIndex = 1
        '
        'TableLayoutPanel10
        '
        Me.TableLayoutPanel10.ColumnCount = 1
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel10.Controls.Add(Me.TableLayoutPanel12, 0, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.TableLayoutPanel11, 0, 2)
        Me.TableLayoutPanel10.Controls.Add(Me.TableLayoutPanel15, 0, 1)
        Me.TableLayoutPanel10.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel10.Name = "TableLayoutPanel10"
        Me.TableLayoutPanel10.RowCount = 4
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel10.Size = New System.Drawing.Size(952, 101)
        Me.TableLayoutPanel10.TabIndex = 5
        '
        'TableLayoutPanel12
        '
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
        Me.TableLayoutPanel12.Size = New System.Drawing.Size(532, 22)
        Me.TableLayoutPanel12.TabIndex = 0
        '
        'txtTekiyoToDt
        '
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
        'TableLayoutPanel11
        '
        Me.TableLayoutPanel11.ColumnCount = 2
        Me.TableLayoutPanel10.SetColumnSpan(Me.TableLayoutPanel11, 2)
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel11.Controls.Add(Me.txtMemo, 1, 0)
        Me.TableLayoutPanel11.Controls.Add(Me.Label45, 0, 0)
        Me.TableLayoutPanel11.Location = New System.Drawing.Point(0, 50)
        Me.TableLayoutPanel11.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel11.Name = "TableLayoutPanel11"
        Me.TableLayoutPanel11.RowCount = 1
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel11.Size = New System.Drawing.Size(952, 22)
        Me.TableLayoutPanel11.TabIndex = 2
        '
        'txtMemo
        '
        Me.txtMemo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtMemo.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtMemo.Location = New System.Drawing.Point(140, 0)
        Me.txtMemo.Margin = New System.Windows.Forms.Padding(0)
        Me.txtMemo.MaxLength = 255
        Me.txtMemo.Name = "txtMemo"
        Me.txtMemo.Size = New System.Drawing.Size(812, 22)
        Me.txtMemo.TabIndex = 0
        Me.txtMemo.Text = "123456789012345678901234567890123456789012345678901234567890123456789012345678901" &
    "2345678901234567890"
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label45.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label45.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label45.Location = New System.Drawing.Point(0, 0)
        Me.Label45.Margin = New System.Windows.Forms.Padding(0)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(140, 22)
        Me.Label45.TabIndex = 5
        Me.Label45.Text = "メモ"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel15
        '
        Me.TableLayoutPanel15.ColumnCount = 2
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel15.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel15.Controls.Add(Me.txtSiireTanka, 0, 0)
        Me.TableLayoutPanel15.Controls.Add(Me.Label8, 0, 0)
        Me.TableLayoutPanel15.Location = New System.Drawing.Point(0, 25)
        Me.TableLayoutPanel15.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel15.Name = "TableLayoutPanel15"
        Me.TableLayoutPanel15.RowCount = 1
        Me.TableLayoutPanel15.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel15.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel15.Size = New System.Drawing.Size(952, 22)
        Me.TableLayoutPanel15.TabIndex = 1
        '
        'txtSiireTanka
        '
        Me.txtSiireTanka.CommaFormat = True
        Me.txtSiireTanka.DecimalDigit = CType(2, Short)
        Me.txtSiireTanka.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSiireTanka.ForeColor = System.Drawing.Color.Black
        Me.txtSiireTanka.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSiireTanka.IntegralDigit = CType(8, Short)
        Me.txtSiireTanka.Location = New System.Drawing.Point(140, 0)
        Me.txtSiireTanka.Margin = New System.Windows.Forms.Padding(0)
        Me.txtSiireTanka.MaxLength = 11
        Me.txtSiireTanka.MinusInput = False
        Me.txtSiireTanka.Name = "txtSiireTanka"
        Me.txtSiireTanka.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57), Global.Microsoft.VisualBasic.ChrW(46)}
        Me.txtSiireTanka.Size = New System.Drawing.Size(110, 22)
        Me.txtSiireTanka.TabIndex = 0
        Me.txtSiireTanka.Text = "12,345,678.90"
        Me.txtSiireTanka.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSiireTanka.Value = "12345678.9"
        Me.txtSiireTanka.ZeroSuppress = True
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
        Me.Label8.Text = "仕入単価"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 6
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.15789!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.78947!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.263158!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.78947!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.cmdModoru, 4, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.cmdKakutei, 2, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(3, 254)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(1024, 57)
        Me.TableLayoutPanel4.TabIndex = 3
        '
        'cmdModoru
        '
        Me.cmdModoru.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdModoru.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdModoru.Location = New System.Drawing.Point(851, 12)
        Me.cmdModoru.Name = "cmdModoru"
        Me.cmdModoru.Size = New System.Drawing.Size(120, 32)
        Me.cmdModoru.TabIndex = 1
        Me.cmdModoru.Text = "戻　る(&B)"
        Me.cmdModoru.UseVisualStyleBackColor = True
        '
        'cmdKakutei
        '
        Me.cmdKakutei.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdKakutei.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdKakutei.Location = New System.Drawing.Point(650, 12)
        Me.cmdKakutei.Name = "cmdKakutei"
        Me.cmdKakutei.Size = New System.Drawing.Size(120, 32)
        Me.cmdKakutei.TabIndex = 0
        Me.cmdKakutei.Text = "確　定(&G)"
        Me.cmdKakutei.UseVisualStyleBackColor = True
        '
        'frmM50F20_SiireTankaHosyu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1030, 314)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "frmM50F20_SiireTankaHosyu"
        Me.Text = "仕入単価マスタ保守（M50F20）"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel8.ResumeLayout(False)
        Me.TableLayoutPanel8.PerformLayout()
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.TableLayoutPanel7.PerformLayout()
        Me.TableLayoutPanel13.ResumeLayout(False)
        Me.TableLayoutPanel13.PerformLayout()
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel10.ResumeLayout(False)
        Me.TableLayoutPanel12.ResumeLayout(False)
        Me.TableLayoutPanel12.PerformLayout()
        Me.TableLayoutPanel11.ResumeLayout(False)
        Me.TableLayoutPanel11.PerformLayout()
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
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents Label5 As Label
    Friend WithEvents lblKousinsya As Label
    Friend WithEvents lblKousinbi As Label
    Friend WithEvents TableLayoutPanel10 As TableLayoutPanel
    Friend WithEvents Label8 As Label
    Friend WithEvents txtShohinCode As TextBox
    Friend WithEvents TableLayoutPanel15 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel6 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel8 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel7 As TableLayoutPanel
    Friend WithEvents txtToriCode As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lblShoriMode As Label
    Friend WithEvents TableLayoutPanel13 As TableLayoutPanel
    Friend WithEvents Label3 As Label
    Friend WithEvents dtpTekiyoFrDt As CustomControl.NullableDateTimePicker
    Friend WithEvents txtSiireTanka As CustomControl.TextBoxNum
    Friend WithEvents TableLayoutPanel12 As TableLayoutPanel
    Friend WithEvents dtpTekiyoToDt As CustomControl.NullableDateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents TableLayoutPanel11 As TableLayoutPanel
    Friend WithEvents txtMemo As TextBox
    Friend WithEvents Label45 As Label
    Friend WithEvents txtShohinName As TextBox
    Friend WithEvents txtToriName As TextBox
    Friend WithEvents txtTekiyoFrDt As TextBox
    Friend WithEvents txtTekiyoToDt As TextBox
End Class
