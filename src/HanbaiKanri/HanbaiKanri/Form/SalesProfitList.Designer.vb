<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SalesProfitList
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BtnExcelOutput = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.DgvList = New System.Windows.Forms.DataGridView()
        Me.売上番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.売上日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.得意先名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.売上計 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ＶＡＴ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.売上金額計 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.間接費 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.売上原価計 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.粗利 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.粗利率 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LblMode = New System.Windows.Forms.Label()
        Me.cmbYear = New System.Windows.Forms.ComboBox()
        Me.cmbMonth = New System.Windows.Forms.ComboBox()
        Me.TxtSalesCostAmount = New System.Windows.Forms.TextBox()
        Me.LblSalesCostAmount = New System.Windows.Forms.Label()
        Me.TxtSalesAmount = New System.Windows.Forms.TextBox()
        Me.LblSalesAmount = New System.Windows.Forms.Label()
        Me.TxtTotalSalesAmount = New System.Windows.Forms.TextBox()
        Me.LblTotalSalesAmount = New System.Windows.Forms.Label()
        Me.LblGrossMarginRate = New System.Windows.Forms.Label()
        Me.TxtGrossMarginRate = New System.Windows.Forms.TextBox()
        Me.LblGrossMargin = New System.Windows.Forms.Label()
        Me.TxtGrossMargin = New System.Windows.Forms.TextBox()
        Me.LblMonth = New System.Windows.Forms.Label()
        Me.LblYear = New System.Windows.Forms.Label()
        CType(Me.DgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnExcelOutput
        '
        Me.BtnExcelOutput.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnExcelOutput.Location = New System.Drawing.Point(1003, 509)
        Me.BtnExcelOutput.Name = "BtnExcelOutput"
        Me.BtnExcelOutput.Size = New System.Drawing.Size(165, 40)
        Me.BtnExcelOutput.TabIndex = 16
        Me.BtnExcelOutput.Text = "Excel出力"
        Me.BtnExcelOutput.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(13, 9)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(170, 22)
        Me.Label8.TabIndex = 80
        Me.Label8.Text = "対象年月"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(1174, 509)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 17
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'DgvList
        '
        Me.DgvList.AllowUserToAddRows = False
        Me.DgvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DgvList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.売上番号, Me.売上日, Me.得意先名, Me.売上計, Me.ＶＡＴ, Me.売上金額計, Me.間接費, Me.売上原価計, Me.粗利, Me.粗利率})
        Me.DgvList.Location = New System.Drawing.Point(13, 102)
        Me.DgvList.Name = "DgvList"
        Me.DgvList.ReadOnly = True
        Me.DgvList.RowHeadersVisible = False
        Me.DgvList.RowTemplate.Height = 21
        Me.DgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvList.Size = New System.Drawing.Size(1326, 345)
        Me.DgvList.TabIndex = 14
        '
        '売上番号
        '
        Me.売上番号.HeaderText = "売上番号"
        Me.売上番号.Name = "売上番号"
        Me.売上番号.ReadOnly = True
        Me.売上番号.Width = 78
        '
        '売上日
        '
        Me.売上日.HeaderText = "売上日"
        Me.売上日.Name = "売上日"
        Me.売上日.ReadOnly = True
        Me.売上日.Width = 66
        '
        '得意先名
        '
        Me.得意先名.HeaderText = "得意先名"
        Me.得意先名.Name = "得意先名"
        Me.得意先名.ReadOnly = True
        Me.得意先名.Width = 78
        '
        '売上計
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.売上計.DefaultCellStyle = DataGridViewCellStyle1
        Me.売上計.HeaderText = "売上計"
        Me.売上計.Name = "売上計"
        Me.売上計.ReadOnly = True
        Me.売上計.Width = 66
        '
        'ＶＡＴ
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.ＶＡＴ.DefaultCellStyle = DataGridViewCellStyle2
        Me.ＶＡＴ.HeaderText = "ＶＡＴ"
        Me.ＶＡＴ.Name = "ＶＡＴ"
        Me.ＶＡＴ.ReadOnly = True
        Me.ＶＡＴ.Width = 55
        '
        '売上金額計
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.売上金額計.DefaultCellStyle = DataGridViewCellStyle3
        Me.売上金額計.HeaderText = "売上 + VAT"
        Me.売上金額計.Name = "売上金額計"
        Me.売上金額計.ReadOnly = True
        Me.売上金額計.Width = 91
        '
        '間接費
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.間接費.DefaultCellStyle = DataGridViewCellStyle4
        Me.間接費.HeaderText = "間接費"
        Me.間接費.Name = "間接費"
        Me.間接費.ReadOnly = True
        Me.間接費.Width = 66
        '
        '売上原価計
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.売上原価計.DefaultCellStyle = DataGridViewCellStyle5
        Me.売上原価計.HeaderText = "売上原価計"
        Me.売上原価計.Name = "売上原価計"
        Me.売上原価計.ReadOnly = True
        Me.売上原価計.Width = 90
        '
        '粗利
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.粗利.DefaultCellStyle = DataGridViewCellStyle6
        Me.粗利.HeaderText = "粗利"
        Me.粗利.Name = "粗利"
        Me.粗利.ReadOnly = True
        Me.粗利.Width = 54
        '
        '粗利率
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.粗利率.DefaultCellStyle = DataGridViewCellStyle7
        Me.粗利率.HeaderText = "粗利率"
        Me.粗利率.Name = "粗利率"
        Me.粗利率.ReadOnly = True
        Me.粗利率.Width = 66
        '
        'LblMode
        '
        Me.LblMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMode.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMode.Location = New System.Drawing.Point(1173, 9)
        Me.LblMode.Name = "LblMode"
        Me.LblMode.Size = New System.Drawing.Size(165, 22)
        Me.LblMode.TabIndex = 96
        Me.LblMode.Text = "参照モード"
        Me.LblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbYear
        '
        Me.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbYear.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbYear.FormattingEnabled = True
        Me.cmbYear.Location = New System.Drawing.Point(85, 70)
        Me.cmbYear.Name = "cmbYear"
        Me.cmbYear.Size = New System.Drawing.Size(111, 23)
        Me.cmbYear.TabIndex = 335
        '
        'cmbMonth
        '
        Me.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMonth.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbMonth.FormattingEnabled = True
        Me.cmbMonth.Location = New System.Drawing.Point(85, 40)
        Me.cmbMonth.Name = "cmbMonth"
        Me.cmbMonth.Size = New System.Drawing.Size(111, 23)
        Me.cmbMonth.TabIndex = 336
        '
        'TxtSalesCostAmount
        '
        Me.TxtSalesCostAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtSalesCostAmount.Enabled = False
        Me.TxtSalesCostAmount.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSalesCostAmount.Location = New System.Drawing.Point(354, 522)
        Me.TxtSalesCostAmount.MaxLength = 10
        Me.TxtSalesCostAmount.Name = "TxtSalesCostAmount"
        Me.TxtSalesCostAmount.ReadOnly = True
        Me.TxtSalesCostAmount.Size = New System.Drawing.Size(231, 23)
        Me.TxtSalesCostAmount.TabIndex = 341
        Me.TxtSalesCostAmount.TabStop = False
        Me.TxtSalesCostAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LblSalesCostAmount
        '
        Me.LblSalesCostAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblSalesCostAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblSalesCostAmount.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSalesCostAmount.Location = New System.Drawing.Point(218, 522)
        Me.LblSalesCostAmount.Name = "LblSalesCostAmount"
        Me.LblSalesCostAmount.Size = New System.Drawing.Size(130, 23)
        Me.LblSalesCostAmount.TabIndex = 342
        Me.LblSalesCostAmount.Text = "売上原価計"
        Me.LblSalesCostAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtSalesAmount
        '
        Me.TxtSalesAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtSalesAmount.Enabled = False
        Me.TxtSalesAmount.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSalesAmount.Location = New System.Drawing.Point(354, 464)
        Me.TxtSalesAmount.MaxLength = 10
        Me.TxtSalesAmount.Name = "TxtSalesAmount"
        Me.TxtSalesAmount.ReadOnly = True
        Me.TxtSalesAmount.Size = New System.Drawing.Size(231, 23)
        Me.TxtSalesAmount.TabIndex = 339
        Me.TxtSalesAmount.TabStop = False
        Me.TxtSalesAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LblSalesAmount
        '
        Me.LblSalesAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblSalesAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblSalesAmount.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSalesAmount.Location = New System.Drawing.Point(218, 464)
        Me.LblSalesAmount.Name = "LblSalesAmount"
        Me.LblSalesAmount.Size = New System.Drawing.Size(130, 23)
        Me.LblSalesAmount.TabIndex = 340
        Me.LblSalesAmount.Text = "売上計"
        Me.LblSalesAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtTotalSalesAmount
        '
        Me.TxtTotalSalesAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtTotalSalesAmount.Enabled = False
        Me.TxtTotalSalesAmount.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtTotalSalesAmount.Location = New System.Drawing.Point(354, 493)
        Me.TxtTotalSalesAmount.MaxLength = 10
        Me.TxtTotalSalesAmount.Name = "TxtTotalSalesAmount"
        Me.TxtTotalSalesAmount.ReadOnly = True
        Me.TxtTotalSalesAmount.Size = New System.Drawing.Size(231, 23)
        Me.TxtTotalSalesAmount.TabIndex = 337
        Me.TxtTotalSalesAmount.TabStop = False
        Me.TxtTotalSalesAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LblTotalSalesAmount
        '
        Me.LblTotalSalesAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblTotalSalesAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblTotalSalesAmount.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTotalSalesAmount.Location = New System.Drawing.Point(218, 493)
        Me.LblTotalSalesAmount.Name = "LblTotalSalesAmount"
        Me.LblTotalSalesAmount.Size = New System.Drawing.Size(130, 23)
        Me.LblTotalSalesAmount.TabIndex = 338
        Me.LblTotalSalesAmount.Text = "売上 + VAT"
        Me.LblTotalSalesAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblGrossMarginRate
        '
        Me.LblGrossMarginRate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblGrossMarginRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblGrossMarginRate.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblGrossMarginRate.Location = New System.Drawing.Point(607, 493)
        Me.LblGrossMarginRate.Name = "LblGrossMarginRate"
        Me.LblGrossMarginRate.Size = New System.Drawing.Size(130, 23)
        Me.LblGrossMarginRate.TabIndex = 338
        Me.LblGrossMarginRate.Text = "粗利率"
        Me.LblGrossMarginRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtGrossMarginRate
        '
        Me.TxtGrossMarginRate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtGrossMarginRate.Enabled = False
        Me.TxtGrossMarginRate.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtGrossMarginRate.Location = New System.Drawing.Point(743, 493)
        Me.TxtGrossMarginRate.MaxLength = 10
        Me.TxtGrossMarginRate.Name = "TxtGrossMarginRate"
        Me.TxtGrossMarginRate.ReadOnly = True
        Me.TxtGrossMarginRate.Size = New System.Drawing.Size(231, 23)
        Me.TxtGrossMarginRate.TabIndex = 337
        Me.TxtGrossMarginRate.TabStop = False
        Me.TxtGrossMarginRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LblGrossMargin
        '
        Me.LblGrossMargin.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblGrossMargin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblGrossMargin.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblGrossMargin.Location = New System.Drawing.Point(607, 464)
        Me.LblGrossMargin.Name = "LblGrossMargin"
        Me.LblGrossMargin.Size = New System.Drawing.Size(130, 23)
        Me.LblGrossMargin.TabIndex = 340
        Me.LblGrossMargin.Text = "粗利"
        Me.LblGrossMargin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtGrossMargin
        '
        Me.TxtGrossMargin.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtGrossMargin.Enabled = False
        Me.TxtGrossMargin.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtGrossMargin.Location = New System.Drawing.Point(743, 464)
        Me.TxtGrossMargin.MaxLength = 10
        Me.TxtGrossMargin.Name = "TxtGrossMargin"
        Me.TxtGrossMargin.ReadOnly = True
        Me.TxtGrossMargin.Size = New System.Drawing.Size(231, 23)
        Me.TxtGrossMargin.TabIndex = 339
        Me.TxtGrossMargin.TabStop = False
        Me.TxtGrossMargin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LblMonth
        '
        Me.LblMonth.AutoSize = True
        Me.LblMonth.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMonth.Location = New System.Drawing.Point(34, 43)
        Me.LblMonth.Name = "LblMonth"
        Me.LblMonth.Size = New System.Drawing.Size(23, 15)
        Me.LblMonth.TabIndex = 343
        Me.LblMonth.Text = "月"
        '
        'LblYear
        '
        Me.LblYear.AutoSize = True
        Me.LblYear.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblYear.Location = New System.Drawing.Point(34, 73)
        Me.LblYear.Name = "LblYear"
        Me.LblYear.Size = New System.Drawing.Size(23, 15)
        Me.LblYear.TabIndex = 344
        Me.LblYear.Text = "年"
        '
        'SalesProfitList
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.LblYear)
        Me.Controls.Add(Me.LblMonth)
        Me.Controls.Add(Me.TxtSalesCostAmount)
        Me.Controls.Add(Me.LblSalesCostAmount)
        Me.Controls.Add(Me.TxtGrossMargin)
        Me.Controls.Add(Me.LblGrossMargin)
        Me.Controls.Add(Me.TxtSalesAmount)
        Me.Controls.Add(Me.TxtGrossMarginRate)
        Me.Controls.Add(Me.LblSalesAmount)
        Me.Controls.Add(Me.LblGrossMarginRate)
        Me.Controls.Add(Me.TxtTotalSalesAmount)
        Me.Controls.Add(Me.LblTotalSalesAmount)
        Me.Controls.Add(Me.cmbMonth)
        Me.Controls.Add(Me.cmbYear)
        Me.Controls.Add(Me.LblMode)
        Me.Controls.Add(Me.BtnExcelOutput)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.DgvList)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "SalesProfitList"
        Me.Text = "SalesProfitList"
        CType(Me.DgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnExcelOutput As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents BtnBack As Button
    Friend WithEvents DgvList As DataGridView
    Friend WithEvents LblMode As Label
    Friend WithEvents cmbYear As ComboBox
    Friend WithEvents cmbMonth As ComboBox
    Friend WithEvents TxtSalesCostAmount As TextBox
    Friend WithEvents LblSalesCostAmount As Label
    Friend WithEvents TxtSalesAmount As TextBox
    Friend WithEvents LblSalesAmount As Label
    Friend WithEvents TxtTotalSalesAmount As TextBox
    Friend WithEvents LblTotalSalesAmount As Label
    Friend WithEvents LblGrossMarginRate As Label
    Friend WithEvents TxtGrossMarginRate As TextBox
    Friend WithEvents LblGrossMargin As Label
    Friend WithEvents TxtGrossMargin As TextBox
    Friend WithEvents 売上番号 As DataGridViewTextBoxColumn
    Friend WithEvents 売上日 As DataGridViewTextBoxColumn
    Friend WithEvents 得意先名 As DataGridViewTextBoxColumn
    Friend WithEvents 売上計 As DataGridViewTextBoxColumn
    Friend WithEvents ＶＡＴ As DataGridViewTextBoxColumn
    Friend WithEvents 売上金額計 As DataGridViewTextBoxColumn
    Friend WithEvents 間接費 As DataGridViewTextBoxColumn
    Friend WithEvents 売上原価計 As DataGridViewTextBoxColumn
    Friend WithEvents 粗利 As DataGridViewTextBoxColumn
    Friend WithEvents 粗利率 As DataGridViewTextBoxColumn
    Friend WithEvents LblMonth As Label
    Friend WithEvents LblYear As Label
End Class
