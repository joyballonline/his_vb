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
        Me.lblBaseCurrency2 = New System.Windows.Forms.Label()
        Me.LblUSD = New System.Windows.Forms.Label()
        Me.LblJPY = New System.Windows.Forms.Label()
        Me.NudForeignCurrency2 = New System.Windows.Forms.NumericUpDown()
        Me.NudForeignCurrency1 = New System.Windows.Forms.NumericUpDown()
        Me.lblBaseCurrency1 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtRate2 = New System.Windows.Forms.TextBox()
        Me.LblBaseCurrency = New System.Windows.Forms.Label()
        Me.LblRate = New System.Windows.Forms.Label()
        Me.LblIDR1 = New System.Windows.Forms.Label()
        Me.txtRate1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.NudForeignCurrency2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NudForeignCurrency1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnRegistration
        '
        Me.BtnRegistration.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnRegistration.Location = New System.Drawing.Point(381, 295)
        Me.BtnRegistration.Name = "BtnRegistration"
        Me.BtnRegistration.Size = New System.Drawing.Size(165, 40)
        Me.BtnRegistration.TabIndex = 3
        Me.BtnRegistration.Text = "登録"
        Me.BtnRegistration.UseVisualStyleBackColor = True
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(552, 295)
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
        Me.TxtCompanyCode.Size = New System.Drawing.Size(24, 31)
        Me.TxtCompanyCode.TabIndex = 352
        '
        'TxtForeignCurrency1
        '
        Me.TxtForeignCurrency1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtForeignCurrency1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtForeignCurrency1.Location = New System.Drawing.Point(278, 231)
        Me.TxtForeignCurrency1.Name = "TxtForeignCurrency1"
        Me.TxtForeignCurrency1.Size = New System.Drawing.Size(172, 37)
        Me.TxtForeignCurrency1.TabIndex = 2
        Me.TxtForeignCurrency1.Visible = False
        '
        'TxtForeignCurrency2
        '
        Me.TxtForeignCurrency2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtForeignCurrency2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtForeignCurrency2.Location = New System.Drawing.Point(278, 259)
        Me.TxtForeignCurrency2.Name = "TxtForeignCurrency2"
        Me.TxtForeignCurrency2.Size = New System.Drawing.Size(172, 37)
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
        Me.DtpStandardDate.Size = New System.Drawing.Size(170, 37)
        Me.DtpStandardDate.TabIndex = 1
        Me.DtpStandardDate.Value = New Date(2019, 1, 1, 0, 0, 0, 0)
        '
        'LblIDR2
        '
        Me.LblIDR2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblIDR2.AutoSize = True
        Me.LblIDR2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblIDR2.Location = New System.Drawing.Point(470, 73)
        Me.LblIDR2.Name = "LblIDR2"
        Me.LblIDR2.Size = New System.Drawing.Size(58, 30)
        Me.LblIDR2.TabIndex = 339
        Me.LblIDR2.Text = "---"
        '
        'lblBaseCurrency2
        '
        Me.lblBaseCurrency2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblBaseCurrency2.AutoSize = True
        Me.lblBaseCurrency2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBaseCurrency2.Location = New System.Drawing.Point(25, 73)
        Me.lblBaseCurrency2.Name = "lblBaseCurrency2"
        Me.lblBaseCurrency2.Size = New System.Drawing.Size(28, 30)
        Me.lblBaseCurrency2.TabIndex = 341
        Me.lblBaseCurrency2.Text = "1"
        '
        'LblUSD
        '
        Me.LblUSD.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblUSD.AutoSize = True
        Me.LblUSD.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblUSD.Location = New System.Drawing.Point(131, 73)
        Me.LblUSD.Name = "LblUSD"
        Me.LblUSD.Size = New System.Drawing.Size(58, 30)
        Me.LblUSD.TabIndex = 337
        Me.LblUSD.Text = "---"
        '
        'LblJPY
        '
        Me.LblJPY.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblJPY.AutoSize = True
        Me.LblJPY.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblJPY.Location = New System.Drawing.Point(131, 37)
        Me.LblJPY.Name = "LblJPY"
        Me.LblJPY.Size = New System.Drawing.Size(58, 30)
        Me.LblJPY.TabIndex = 338
        Me.LblJPY.Text = "---"
        '
        'NudForeignCurrency2
        '
        Me.NudForeignCurrency2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NudForeignCurrency2.AutoSize = True
        Me.NudForeignCurrency2.DecimalPlaces = 10
        Me.NudForeignCurrency2.Location = New System.Drawing.Point(244, 73)
        Me.NudForeignCurrency2.Maximum = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.NudForeignCurrency2.Name = "NudForeignCurrency2"
        Me.NudForeignCurrency2.Size = New System.Drawing.Size(185, 31)
        Me.NudForeignCurrency2.TabIndex = 4
        '
        'NudForeignCurrency1
        '
        Me.NudForeignCurrency1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NudForeignCurrency1.AutoSize = True
        Me.NudForeignCurrency1.DecimalPlaces = 10
        Me.NudForeignCurrency1.Location = New System.Drawing.Point(244, 38)
        Me.NudForeignCurrency1.Maximum = New Decimal(New Integer() {999999999, 0, 0, 0})
        Me.NudForeignCurrency1.Name = "NudForeignCurrency1"
        Me.NudForeignCurrency1.Size = New System.Drawing.Size(185, 31)
        Me.NudForeignCurrency1.TabIndex = 2
        '
        'lblBaseCurrency1
        '
        Me.lblBaseCurrency1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblBaseCurrency1.AutoSize = True
        Me.lblBaseCurrency1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBaseCurrency1.Location = New System.Drawing.Point(25, 37)
        Me.lblBaseCurrency1.Name = "lblBaseCurrency1"
        Me.lblBaseCurrency1.Size = New System.Drawing.Size(28, 30)
        Me.lblBaseCurrency1.TabIndex = 340
        Me.lblBaseCurrency1.Text = "1"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 5
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.92617!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.47341!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.61921!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 134.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.72881!))
        Me.TableLayoutPanel1.Controls.Add(Me.txtRate2, 4, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.lblBaseCurrency1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LblBaseCurrency, 0, 0)
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(15, 118)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(702, 107)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'txtRate2
        '
        Me.txtRate2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRate2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRate2.Location = New System.Drawing.Point(569, 73)
        Me.txtRate2.Name = "txtRate2"
        Me.txtRate2.Size = New System.Drawing.Size(130, 37)
        Me.txtRate2.TabIndex = 357
        '
        'LblBaseCurrency
        '
        Me.LblBaseCurrency.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblBaseCurrency.AutoSize = True
        Me.LblBaseCurrency.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblBaseCurrency.Location = New System.Drawing.Point(3, 0)
        Me.LblBaseCurrency.Name = "LblBaseCurrency"
        Me.LblBaseCurrency.Size = New System.Drawing.Size(73, 35)
        Me.LblBaseCurrency.TabIndex = 327
        Me.LblBaseCurrency.Text = "基準通貨"
        '
        'LblRate
        '
        Me.LblRate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblRate.AutoSize = True
        Me.LblRate.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblRate.Location = New System.Drawing.Point(285, 2)
        Me.LblRate.Name = "LblRate"
        Me.LblRate.Size = New System.Drawing.Size(103, 30)
        Me.LblRate.TabIndex = 342
        Me.LblRate.Text = "原通貨"
        '
        'LblIDR1
        '
        Me.LblIDR1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblIDR1.AutoSize = True
        Me.LblIDR1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblIDR1.Location = New System.Drawing.Point(470, 37)
        Me.LblIDR1.Name = "LblIDR1"
        Me.LblIDR1.Size = New System.Drawing.Size(58, 30)
        Me.LblIDR1.TabIndex = 343
        Me.LblIDR1.Text = "---"
        '
        'txtRate1
        '
        Me.txtRate1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRate1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRate1.Location = New System.Drawing.Point(569, 38)
        Me.txtRate1.Name = "txtRate1"
        Me.txtRate1.Size = New System.Drawing.Size(130, 37)
        Me.txtRate1.TabIndex = 356
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(593, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 30)
        Me.Label1.TabIndex = 357
        Me.Label1.Text = "レート"
        '
        'ExchangeRate
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(729, 351)
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
    Friend WithEvents lblBaseCurrency2 As Label
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
End Class
