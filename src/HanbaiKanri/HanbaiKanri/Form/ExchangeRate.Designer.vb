<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ExchangeRate
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
        Me.BtnRegistration = New System.Windows.Forms.Button()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.TxtCompanyCode = New System.Windows.Forms.TextBox()
        Me.TxtForeignCurrency1 = New System.Windows.Forms.TextBox()
        Me.TxtForeignCurrency2 = New System.Windows.Forms.TextBox()
        Me.LblStandardDate = New System.Windows.Forms.Label()
        Me.DtpStandardDate = New System.Windows.Forms.DateTimePicker()
        Me.LblIDR2 = New System.Windows.Forms.Label()
        Me.LblUSD = New System.Windows.Forms.Label()
        Me.LblJPY = New System.Windows.Forms.Label()
        Me.NudForeignCurrency2 = New System.Windows.Forms.NumericUpDown()
        Me.NudForeignCurrency1 = New System.Windows.Forms.NumericUpDown()
        Me.lblBaseCurrency1 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtRate2 = New System.Windows.Forms.TextBox()
        Me.lblBaseCurrency2 = New System.Windows.Forms.Label()
        Me.LblRate = New System.Windows.Forms.Label()
        Me.LblIDR1 = New System.Windows.Forms.Label()
        Me.txtRate1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblBaseCurrency = New System.Windows.Forms.Label()
        Me.LblCurrency4 = New System.Windows.Forms.Label()
        Me.LblCurrency5 = New System.Windows.Forms.Label()
        Me.LblCurrency6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.NudForeignCurrency4 = New System.Windows.Forms.NumericUpDown()
        Me.NudForeignCurrency5 = New System.Windows.Forms.NumericUpDown()
        Me.NudForeignCurrency6 = New System.Windows.Forms.NumericUpDown()
        CType(Me.NudForeignCurrency2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NudForeignCurrency1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.NudForeignCurrency4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NudForeignCurrency5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NudForeignCurrency6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnRegistration
        '
        Me.BtnRegistration.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnRegistration.Location = New System.Drawing.Point(381, 376)
        Me.BtnRegistration.Name = "BtnRegistration"
        Me.BtnRegistration.Size = New System.Drawing.Size(165, 40)
        Me.BtnRegistration.TabIndex = 3
        Me.BtnRegistration.Text = "登録"
        Me.BtnRegistration.UseVisualStyleBackColor = True
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(552, 376)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 4
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'TxtCompanyCode
        '
        Me.TxtCompanyCode.Location = New System.Drawing.Point(851, 6)
        Me.TxtCompanyCode.Name = "TxtCompanyCode"
        Me.TxtCompanyCode.Size = New System.Drawing.Size(24, 19)
        Me.TxtCompanyCode.TabIndex = 352
        '
        'TxtForeignCurrency1
        '
        Me.TxtForeignCurrency1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtForeignCurrency1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtForeignCurrency1.Location = New System.Drawing.Point(7, 394)
        Me.TxtForeignCurrency1.Name = "TxtForeignCurrency1"
        Me.TxtForeignCurrency1.Size = New System.Drawing.Size(29, 22)
        Me.TxtForeignCurrency1.TabIndex = 2
        Me.TxtForeignCurrency1.Visible = False
        '
        'TxtForeignCurrency2
        '
        Me.TxtForeignCurrency2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtForeignCurrency2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtForeignCurrency2.Location = New System.Drawing.Point(42, 394)
        Me.TxtForeignCurrency2.Name = "TxtForeignCurrency2"
        Me.TxtForeignCurrency2.Size = New System.Drawing.Size(29, 22)
        Me.TxtForeignCurrency2.TabIndex = 4
        Me.TxtForeignCurrency2.Visible = False
        '
        'LblStandardDate
        '
        Me.LblStandardDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblStandardDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblStandardDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblStandardDate.Location = New System.Drawing.Point(15, 24)
        Me.LblStandardDate.Name = "LblStandardDate"
        Me.LblStandardDate.Size = New System.Drawing.Size(135, 22)
        Me.LblStandardDate.TabIndex = 355
        Me.LblStandardDate.Text = "基準日"
        Me.LblStandardDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DtpStandardDate
        '
        Me.DtpStandardDate.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpStandardDate.CustomFormat = ""
        Me.DtpStandardDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpStandardDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpStandardDate.Location = New System.Drawing.Point(156, 24)
        Me.DtpStandardDate.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.DtpStandardDate.Name = "DtpStandardDate"
        Me.DtpStandardDate.Size = New System.Drawing.Size(170, 22)
        Me.DtpStandardDate.TabIndex = 1
        Me.DtpStandardDate.Value = New Date(2019, 1, 1, 0, 0, 0, 0)
        '
        'LblIDR2
        '
        Me.LblIDR2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblIDR2.AutoSize = True
        Me.LblIDR2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblIDR2.Location = New System.Drawing.Point(518, 94)
        Me.LblIDR2.Name = "LblIDR2"
        Me.LblIDR2.Size = New System.Drawing.Size(15, 45)
        Me.LblIDR2.TabIndex = 339
        Me.LblIDR2.Text = "---"
        Me.LblIDR2.Visible = False
        '
        'LblUSD
        '
        Me.LblUSD.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblUSD.AutoSize = True
        Me.LblUSD.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblUSD.Location = New System.Drawing.Point(242, 109)
        Me.LblUSD.Name = "LblUSD"
        Me.LblUSD.Size = New System.Drawing.Size(31, 15)
        Me.LblUSD.TabIndex = 337
        Me.LblUSD.Text = "---"
        '
        'LblJPY
        '
        Me.LblJPY.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblJPY.AutoSize = True
        Me.LblJPY.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblJPY.Location = New System.Drawing.Point(242, 62)
        Me.LblJPY.Name = "LblJPY"
        Me.LblJPY.Size = New System.Drawing.Size(31, 15)
        Me.LblJPY.TabIndex = 338
        Me.LblJPY.Text = "---"
        '
        'NudForeignCurrency2
        '
        Me.NudForeignCurrency2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NudForeignCurrency2.AutoSize = True
        Me.NudForeignCurrency2.DecimalPlaces = 10
        Me.NudForeignCurrency2.Location = New System.Drawing.Point(381, 107)
        Me.NudForeignCurrency2.Maximum = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.NudForeignCurrency2.Name = "NudForeignCurrency2"
        Me.NudForeignCurrency2.Size = New System.Drawing.Size(128, 19)
        Me.NudForeignCurrency2.TabIndex = 4
        Me.NudForeignCurrency2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'NudForeignCurrency1
        '
        Me.NudForeignCurrency1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NudForeignCurrency1.AutoSize = True
        Me.NudForeignCurrency1.DecimalPlaces = 10
        Me.NudForeignCurrency1.Location = New System.Drawing.Point(381, 60)
        Me.NudForeignCurrency1.Maximum = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.NudForeignCurrency1.Name = "NudForeignCurrency1"
        Me.NudForeignCurrency1.Size = New System.Drawing.Size(128, 19)
        Me.NudForeignCurrency1.TabIndex = 2
        Me.NudForeignCurrency1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblBaseCurrency1
        '
        Me.lblBaseCurrency1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblBaseCurrency1.AutoSize = True
        Me.lblBaseCurrency1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBaseCurrency1.Location = New System.Drawing.Point(61, 62)
        Me.lblBaseCurrency1.Name = "lblBaseCurrency1"
        Me.lblBaseCurrency1.Size = New System.Drawing.Size(15, 15)
        Me.lblBaseCurrency1.TabIndex = 340
        Me.lblBaseCurrency1.Text = "1"
        Me.lblBaseCurrency1.Visible = False
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 5
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.47478!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.60831!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.88131!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.72881!))
        Me.TableLayoutPanel1.Controls.Add(Me.txtRate2, 4, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.lblBaseCurrency1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.NudForeignCurrency1, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.NudForeignCurrency2, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LblJPY, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LblUSD, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.lblBaseCurrency2, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LblRate, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LblIDR1, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LblIDR2, 3, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.txtRate1, 4, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LblBaseCurrency, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LblCurrency4, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.LblCurrency5, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.LblCurrency6, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.Label8, 3, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Label9, 3, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Label10, 3, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.NudForeignCurrency4, 2, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.NudForeignCurrency5, 2, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.NudForeignCurrency6, 2, 5)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(15, 77)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 6
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.08271!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.83459!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(702, 281)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'txtRate2
        '
        Me.txtRate2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRate2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRate2.Location = New System.Drawing.Point(543, 105)
        Me.txtRate2.Name = "txtRate2"
        Me.txtRate2.Size = New System.Drawing.Size(156, 22)
        Me.txtRate2.TabIndex = 357
        Me.txtRate2.Visible = False
        '
        'lblBaseCurrency2
        '
        Me.lblBaseCurrency2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblBaseCurrency2.AutoSize = True
        Me.lblBaseCurrency2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBaseCurrency2.Location = New System.Drawing.Point(61, 109)
        Me.lblBaseCurrency2.Name = "lblBaseCurrency2"
        Me.lblBaseCurrency2.Size = New System.Drawing.Size(15, 15)
        Me.lblBaseCurrency2.TabIndex = 341
        Me.lblBaseCurrency2.Text = "1"
        Me.lblBaseCurrency2.Visible = False
        '
        'LblRate
        '
        Me.LblRate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblRate.AutoSize = True
        Me.LblRate.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblRate.Location = New System.Drawing.Point(393, 16)
        Me.LblRate.Name = "LblRate"
        Me.LblRate.Size = New System.Drawing.Size(104, 15)
        Me.LblRate.TabIndex = 342
        Me.LblRate.Text = "為替レート(IDR)"
        '
        'LblIDR1
        '
        Me.LblIDR1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblIDR1.AutoSize = True
        Me.LblIDR1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblIDR1.Location = New System.Drawing.Point(518, 47)
        Me.LblIDR1.Name = "LblIDR1"
        Me.LblIDR1.Size = New System.Drawing.Size(15, 45)
        Me.LblIDR1.TabIndex = 343
        Me.LblIDR1.Text = "---"
        Me.LblIDR1.Visible = False
        '
        'txtRate1
        '
        Me.txtRate1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRate1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRate1.Location = New System.Drawing.Point(543, 59)
        Me.txtRate1.Name = "txtRate1"
        Me.txtRate1.Size = New System.Drawing.Size(156, 22)
        Me.txtRate1.TabIndex = 356
        Me.txtRate1.Visible = False
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(600, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 15)
        Me.Label1.TabIndex = 357
        Me.Label1.Text = "レート"
        Me.Label1.Visible = False
        '
        'LblBaseCurrency
        '
        Me.LblBaseCurrency.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblBaseCurrency.AutoSize = True
        Me.LblBaseCurrency.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblBaseCurrency.Location = New System.Drawing.Point(204, 16)
        Me.LblBaseCurrency.Name = "LblBaseCurrency"
        Me.LblBaseCurrency.Size = New System.Drawing.Size(107, 15)
        Me.LblBaseCurrency.TabIndex = 327
        Me.LblBaseCurrency.Text = "通貨(通貨単位)"
        '
        'LblCurrency4
        '
        Me.LblCurrency4.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblCurrency4.AutoSize = True
        Me.LblCurrency4.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCurrency4.Location = New System.Drawing.Point(242, 155)
        Me.LblCurrency4.Name = "LblCurrency4"
        Me.LblCurrency4.Size = New System.Drawing.Size(31, 15)
        Me.LblCurrency4.TabIndex = 361
        Me.LblCurrency4.Text = "---"
        '
        'LblCurrency5
        '
        Me.LblCurrency5.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblCurrency5.AutoSize = True
        Me.LblCurrency5.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCurrency5.Location = New System.Drawing.Point(242, 201)
        Me.LblCurrency5.Name = "LblCurrency5"
        Me.LblCurrency5.Size = New System.Drawing.Size(31, 15)
        Me.LblCurrency5.TabIndex = 362
        Me.LblCurrency5.Text = "---"
        '
        'LblCurrency6
        '
        Me.LblCurrency6.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblCurrency6.AutoSize = True
        Me.LblCurrency6.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCurrency6.Location = New System.Drawing.Point(242, 248)
        Me.LblCurrency6.Name = "LblCurrency6"
        Me.LblCurrency6.Size = New System.Drawing.Size(31, 15)
        Me.LblCurrency6.TabIndex = 363
        Me.LblCurrency6.Text = "---"
        '
        'Label8
        '
        Me.Label8.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(518, 140)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(15, 45)
        Me.Label8.TabIndex = 364
        Me.Label8.Text = "---"
        Me.Label8.Visible = False
        '
        'Label9
        '
        Me.Label9.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(518, 186)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(15, 45)
        Me.Label9.TabIndex = 365
        Me.Label9.Text = "---"
        Me.Label9.Visible = False
        '
        'Label10
        '
        Me.Label10.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(518, 233)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(15, 45)
        Me.Label10.TabIndex = 366
        Me.Label10.Text = "---"
        Me.Label10.Visible = False
        '
        'NudForeignCurrency4
        '
        Me.NudForeignCurrency4.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NudForeignCurrency4.AutoSize = True
        Me.NudForeignCurrency4.DecimalPlaces = 10
        Me.NudForeignCurrency4.Location = New System.Drawing.Point(381, 153)
        Me.NudForeignCurrency4.Maximum = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.NudForeignCurrency4.Name = "NudForeignCurrency4"
        Me.NudForeignCurrency4.Size = New System.Drawing.Size(128, 19)
        Me.NudForeignCurrency4.TabIndex = 367
        Me.NudForeignCurrency4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'NudForeignCurrency5
        '
        Me.NudForeignCurrency5.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NudForeignCurrency5.AutoSize = True
        Me.NudForeignCurrency5.DecimalPlaces = 10
        Me.NudForeignCurrency5.Location = New System.Drawing.Point(381, 199)
        Me.NudForeignCurrency5.Maximum = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.NudForeignCurrency5.Name = "NudForeignCurrency5"
        Me.NudForeignCurrency5.Size = New System.Drawing.Size(128, 19)
        Me.NudForeignCurrency5.TabIndex = 368
        Me.NudForeignCurrency5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'NudForeignCurrency6
        '
        Me.NudForeignCurrency6.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NudForeignCurrency6.AutoSize = True
        Me.NudForeignCurrency6.DecimalPlaces = 10
        Me.NudForeignCurrency6.Location = New System.Drawing.Point(381, 246)
        Me.NudForeignCurrency6.Maximum = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.NudForeignCurrency6.Name = "NudForeignCurrency6"
        Me.NudForeignCurrency6.Size = New System.Drawing.Size(128, 19)
        Me.NudForeignCurrency6.TabIndex = 369
        Me.NudForeignCurrency6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ExchangeRate
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(729, 434)
        Me.Controls.Add(Me.DtpStandardDate)
        Me.Controls.Add(Me.LblStandardDate)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.TxtCompanyCode)
        Me.Controls.Add(Me.BtnRegistration)
        Me.Controls.Add(Me.TxtForeignCurrency1)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.TxtForeignCurrency2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "ExchangeRate"
        Me.Text = "ExchangeRate"
        CType(Me.NudForeignCurrency2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NudForeignCurrency1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.NudForeignCurrency4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NudForeignCurrency5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NudForeignCurrency6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnRegistration As Button
    Friend WithEvents BtnBack As Button
    Friend WithEvents TxtCompanyCode As TextBox
    Friend WithEvents TxtForeignCurrency1 As TextBox
    Friend WithEvents TxtForeignCurrency2 As TextBox
    Friend WithEvents LblStandardDate As Label
    Friend WithEvents DtpStandardDate As DateTimePicker
    Friend WithEvents LblIDR2 As Label
    Friend WithEvents LblUSD As Label
    Friend WithEvents LblJPY As Label
    Friend WithEvents NudForeignCurrency2 As NumericUpDown
    Friend WithEvents NudForeignCurrency1 As NumericUpDown
    Friend WithEvents lblBaseCurrency1 As Label
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents LblBaseCurrency As Label
    Friend WithEvents LblIDR1 As Label
    Friend WithEvents LblRate As Label
    Friend WithEvents txtRate1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtRate2 As TextBox
    Friend WithEvents lblBaseCurrency2 As Label
    Friend WithEvents LblCurrency4 As Label
    Friend WithEvents LblCurrency5 As Label
    Friend WithEvents LblCurrency6 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents NudForeignCurrency4 As NumericUpDown
    Friend WithEvents NudForeignCurrency5 As NumericUpDown
    Friend WithEvents NudForeignCurrency6 As NumericUpDown
End Class
