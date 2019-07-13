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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.LblBaseCurrency = New System.Windows.Forms.Label()
        Me.LblForeignCurrency = New System.Windows.Forms.Label()
        Me.LblRate = New System.Windows.Forms.Label()
        Me.TxtBaseCurrency1 = New System.Windows.Forms.TextBox()
        Me.TxtRate1 = New System.Windows.Forms.TextBox()
        Me.TxtRate2 = New System.Windows.Forms.TextBox()
        Me.LblIDR1 = New System.Windows.Forms.Label()
        Me.LblJPY = New System.Windows.Forms.Label()
        Me.LblUSD = New System.Windows.Forms.Label()
        Me.LblIDR2 = New System.Windows.Forms.Label()
        Me.TxtBaseCurrency2 = New System.Windows.Forms.TextBox()
        Me.NudForeignCurrency1 = New System.Windows.Forms.NumericUpDown()
        Me.NudForeignCurrency2 = New System.Windows.Forms.NumericUpDown()
        Me.TxtForeignCurrency1 = New System.Windows.Forms.TextBox()
        Me.TxtForeignCurrency2 = New System.Windows.Forms.TextBox()
        Me.LblStandardDate = New System.Windows.Forms.Label()
        Me.DtpStandardDate = New System.Windows.Forms.DateTimePicker()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.NudForeignCurrency1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NudForeignCurrency2, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.TxtCompanyCode.Size = New System.Drawing.Size(24, 19)
        Me.TxtCompanyCode.TabIndex = 352
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 5
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.37322!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.80627!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.25253!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.12121!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.25253!))
        Me.TableLayoutPanel1.Controls.Add(Me.LblBaseCurrency, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LblForeignCurrency, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LblRate, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TxtBaseCurrency1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TxtRate1, 4, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TxtRate2, 4, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TxtBaseCurrency2, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.NudForeignCurrency1, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.NudForeignCurrency2, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LblIDR1, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LblJPY, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LblIDR2, 3, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LblUSD, 1, 2)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(15, 118)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(702, 107)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'LblBaseCurrency
        '
        Me.LblBaseCurrency.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblBaseCurrency.AutoSize = True
        Me.LblBaseCurrency.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblBaseCurrency.Location = New System.Drawing.Point(34, 10)
        Me.LblBaseCurrency.Name = "LblBaseCurrency"
        Me.LblBaseCurrency.Size = New System.Drawing.Size(67, 15)
        Me.LblBaseCurrency.TabIndex = 327
        Me.LblBaseCurrency.Text = "基準通貨"
        '
        'LblForeignCurrency
        '
        Me.LblForeignCurrency.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblForeignCurrency.AutoSize = True
        Me.LblForeignCurrency.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblForeignCurrency.Location = New System.Drawing.Point(331, 10)
        Me.LblForeignCurrency.Name = "LblForeignCurrency"
        Me.LblForeignCurrency.Size = New System.Drawing.Size(37, 15)
        Me.LblForeignCurrency.TabIndex = 328
        Me.LblForeignCurrency.Text = "外貨"
        '
        'LblRate
        '
        Me.LblRate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblRate.AutoSize = True
        Me.LblRate.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblRate.Location = New System.Drawing.Point(592, 10)
        Me.LblRate.Name = "LblRate"
        Me.LblRate.Size = New System.Drawing.Size(41, 15)
        Me.LblRate.TabIndex = 329
        Me.LblRate.Text = "レート"
        '
        'TxtBaseCurrency1
        '
        Me.TxtBaseCurrency1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtBaseCurrency1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtBaseCurrency1.Location = New System.Drawing.Point(3, 41)
        Me.TxtBaseCurrency1.Name = "TxtBaseCurrency1"
        Me.TxtBaseCurrency1.Size = New System.Drawing.Size(130, 22)
        Me.TxtBaseCurrency1.TabIndex = 1
        '
        'TxtRate1
        '
        Me.TxtRate1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtRate1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtRate1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtRate1.Location = New System.Drawing.Point(526, 41)
        Me.TxtRate1.Name = "TxtRate1"
        Me.TxtRate1.ReadOnly = True
        Me.TxtRate1.Size = New System.Drawing.Size(173, 22)
        Me.TxtRate1.TabIndex = 332
        '
        'TxtRate2
        '
        Me.TxtRate2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtRate2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtRate2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtRate2.Location = New System.Drawing.Point(526, 77)
        Me.TxtRate2.Name = "TxtRate2"
        Me.TxtRate2.ReadOnly = True
        Me.TxtRate2.Size = New System.Drawing.Size(173, 22)
        Me.TxtRate2.TabIndex = 335
        '
        'LblIDR1
        '
        Me.LblIDR1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblIDR1.AutoSize = True
        Me.LblIDR1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblIDR1.Location = New System.Drawing.Point(465, 45)
        Me.LblIDR1.Name = "LblIDR1"
        Me.LblIDR1.Size = New System.Drawing.Size(31, 15)
        Me.LblIDR1.TabIndex = 336
        Me.LblIDR1.Text = "---"
        '
        'LblJPY
        '
        Me.LblJPY.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblJPY.AutoSize = True
        Me.LblJPY.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblJPY.Location = New System.Drawing.Point(183, 45)
        Me.LblJPY.Name = "LblJPY"
        Me.LblJPY.Size = New System.Drawing.Size(31, 15)
        Me.LblJPY.TabIndex = 338
        Me.LblJPY.Text = "---"
        '
        'LblUSD
        '
        Me.LblUSD.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblUSD.AutoSize = True
        Me.LblUSD.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblUSD.Location = New System.Drawing.Point(183, 81)
        Me.LblUSD.Name = "LblUSD"
        Me.LblUSD.Size = New System.Drawing.Size(31, 15)
        Me.LblUSD.TabIndex = 337
        Me.LblUSD.Text = "---"
        '
        'LblIDR2
        '
        Me.LblIDR2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblIDR2.AutoSize = True
        Me.LblIDR2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblIDR2.Location = New System.Drawing.Point(465, 81)
        Me.LblIDR2.Name = "LblIDR2"
        Me.LblIDR2.Size = New System.Drawing.Size(31, 15)
        Me.LblIDR2.TabIndex = 339
        Me.LblIDR2.Text = "---"
        '
        'TxtBaseCurrency2
        '
        Me.TxtBaseCurrency2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtBaseCurrency2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtBaseCurrency2.Location = New System.Drawing.Point(3, 77)
        Me.TxtBaseCurrency2.Name = "TxtBaseCurrency2"
        Me.TxtBaseCurrency2.Size = New System.Drawing.Size(130, 22)
        Me.TxtBaseCurrency2.TabIndex = 3
        '
        'NudForeignCurrency1
        '
        Me.NudForeignCurrency1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NudForeignCurrency1.AutoSize = True
        Me.NudForeignCurrency1.DecimalPlaces = 10
        Me.NudForeignCurrency1.Location = New System.Drawing.Point(264, 43)
        Me.NudForeignCurrency1.Name = "NudForeignCurrency1"
        Me.NudForeignCurrency1.Size = New System.Drawing.Size(171, 19)
        Me.NudForeignCurrency1.TabIndex = 2
        '
        'NudForeignCurrency2
        '
        Me.NudForeignCurrency2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NudForeignCurrency2.AutoSize = True
        Me.NudForeignCurrency2.DecimalPlaces = 10
        Me.NudForeignCurrency2.Location = New System.Drawing.Point(264, 79)
        Me.NudForeignCurrency2.Name = "NudForeignCurrency2"
        Me.NudForeignCurrency2.Size = New System.Drawing.Size(171, 19)
        Me.NudForeignCurrency2.TabIndex = 4
        '
        'TxtForeignCurrency1
        '
        Me.TxtForeignCurrency1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtForeignCurrency1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtForeignCurrency1.Location = New System.Drawing.Point(278, 231)
        Me.TxtForeignCurrency1.Name = "TxtForeignCurrency1"
        Me.TxtForeignCurrency1.Size = New System.Drawing.Size(172, 22)
        Me.TxtForeignCurrency1.TabIndex = 2
        Me.TxtForeignCurrency1.Visible = False
        '
        'TxtForeignCurrency2
        '
        Me.TxtForeignCurrency2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtForeignCurrency2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtForeignCurrency2.Location = New System.Drawing.Point(278, 259)
        Me.TxtForeignCurrency2.Name = "TxtForeignCurrency2"
        Me.TxtForeignCurrency2.Size = New System.Drawing.Size(172, 22)
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
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.NudForeignCurrency1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NudForeignCurrency2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnRegistration As Button
    Friend WithEvents BtnBack As Button
    Friend WithEvents TxtCompanyCode As TextBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents LblBaseCurrency As Label
    Friend WithEvents LblForeignCurrency As Label
    Friend WithEvents LblRate As Label
    Friend WithEvents TxtBaseCurrency2 As TextBox
    Friend WithEvents TxtBaseCurrency1 As TextBox
    Friend WithEvents TxtForeignCurrency1 As TextBox
    Friend WithEvents TxtRate1 As TextBox
    Friend WithEvents TxtForeignCurrency2 As TextBox
    Friend WithEvents TxtRate2 As TextBox
    Friend WithEvents LblIDR2 As Label
    Friend WithEvents LblIDR1 As Label
    Friend WithEvents LblJPY As Label
    Friend WithEvents LblUSD As Label
    Friend WithEvents LblStandardDate As Label
    Friend WithEvents DtpStandardDate As DateTimePicker
    Friend WithEvents NudForeignCurrency1 As NumericUpDown
    Friend WithEvents NudForeignCurrency2 As NumericUpDown
End Class
