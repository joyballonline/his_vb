<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SalesVATList
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
        Me.BtnExcelOutput = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.DgvList = New System.Windows.Forms.DataGridView()
        Me.LblMode = New System.Windows.Forms.Label()
        Me.cmbYear = New System.Windows.Forms.ComboBox()
        Me.cmbMonth = New System.Windows.Forms.ComboBox()
        Me.TxtSalesAmount = New System.Windows.Forms.TextBox()
        Me.LblSalesAmount = New System.Windows.Forms.Label()
        Me.LblMonth = New System.Windows.Forms.Label()
        Me.LblYear = New System.Windows.Forms.Label()
        Me.売上番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.売上日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.得意先名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.客先番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.営業担当者 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.メーカー = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.品名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.型式 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.数量 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.単位 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.通貨_外貨 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.売単価 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ＶＡＴ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.売上計 = New System.Windows.Forms.DataGridViewTextBoxColumn()
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
        Me.Label8.Font = New System.Drawing.Font("MS Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
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
        Me.DgvList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.売上番号, Me.売上日, Me.得意先名, Me.客先番号, Me.営業担当者, Me.メーカー, Me.品名, Me.型式, Me.数量, Me.単位, Me.通貨_外貨, Me.売単価, Me.ＶＡＴ, Me.売上計})
        Me.DgvList.Location = New System.Drawing.Point(13, 102)
        Me.DgvList.Name = "DgvList"
        Me.DgvList.ReadOnly = True
        Me.DgvList.RowHeadersVisible = False
        Me.DgvList.RowTemplate.Height = 21
        Me.DgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvList.Size = New System.Drawing.Size(1326, 388)
        Me.DgvList.TabIndex = 14
        '
        'LblMode
        '
        Me.LblMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMode.Font = New System.Drawing.Font("MS Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
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
        'TxtSalesAmount
        '
        Me.TxtSalesAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtSalesAmount.Enabled = False
        Me.TxtSalesAmount.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSalesAmount.Location = New System.Drawing.Point(684, 516)
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
        Me.LblSalesAmount.Location = New System.Drawing.Point(548, 516)
        Me.LblSalesAmount.Name = "LblSalesAmount"
        Me.LblSalesAmount.Size = New System.Drawing.Size(130, 23)
        Me.LblSalesAmount.TabIndex = 340
        Me.LblSalesAmount.Text = "売上計"
        Me.LblSalesAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblMonth
        '
        Me.LblMonth.AutoSize = True
        Me.LblMonth.Font = New System.Drawing.Font("MS Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMonth.Location = New System.Drawing.Point(34, 43)
        Me.LblMonth.Name = "LblMonth"
        Me.LblMonth.Size = New System.Drawing.Size(23, 15)
        Me.LblMonth.TabIndex = 343
        Me.LblMonth.Text = "月"
        '
        'LblYear
        '
        Me.LblYear.AutoSize = True
        Me.LblYear.Font = New System.Drawing.Font("MS Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblYear.Location = New System.Drawing.Point(34, 73)
        Me.LblYear.Name = "LblYear"
        Me.LblYear.Size = New System.Drawing.Size(23, 15)
        Me.LblYear.TabIndex = 344
        Me.LblYear.Text = "年"
        '
        '売上番号
        '
        Me.売上番号.HeaderText = "売上番号"
        Me.売上番号.Name = "売上番号"
        Me.売上番号.ReadOnly = True
        Me.売上番号.Width = 80
        '
        '売上日
        '
        Me.売上日.HeaderText = "売上日"
        Me.売上日.Name = "売上日"
        Me.売上日.ReadOnly = True
        Me.売上日.Width = 68
        '
        '得意先名
        '
        Me.得意先名.HeaderText = "得意先名"
        Me.得意先名.Name = "得意先名"
        Me.得意先名.ReadOnly = True
        Me.得意先名.Width = 80
        '
        '客先番号
        '
        Me.客先番号.HeaderText = "客先番号"
        Me.客先番号.Name = "客先番号"
        Me.客先番号.ReadOnly = True
        Me.客先番号.Width = 80
        '
        '営業担当者
        '
        Me.営業担当者.HeaderText = "営業担当者"
        Me.営業担当者.Name = "営業担当者"
        Me.営業担当者.ReadOnly = True
        Me.営業担当者.Width = 92
        '
        'メーカー
        '
        Me.メーカー.HeaderText = "メーカー"
        Me.メーカー.Name = "メーカー"
        Me.メーカー.ReadOnly = True
        Me.メーカー.Width = 69
        '
        '品名
        '
        Me.品名.HeaderText = "品名"
        Me.品名.Name = "品名"
        Me.品名.ReadOnly = True
        Me.品名.Width = 56
        '
        '型式
        '
        Me.型式.HeaderText = "型式"
        Me.型式.Name = "型式"
        Me.型式.ReadOnly = True
        Me.型式.Width = 56
        '
        '数量
        '
        Me.数量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.数量.DefaultCellStyle = DataGridViewCellStyle1
        Me.数量.HeaderText = "数量"
        Me.数量.Name = "数量"
        Me.数量.ReadOnly = True
        Me.数量.Width = 56
        '
        '単位
        '
        Me.単位.HeaderText = "単位"
        Me.単位.Name = "単位"
        Me.単位.ReadOnly = True
        Me.単位.Width = 56
        '
        '通貨_外貨
        '
        Me.通貨_外貨.HeaderText = "通貨"
        Me.通貨_外貨.Name = "通貨_外貨"
        Me.通貨_外貨.ReadOnly = True
        Me.通貨_外貨.Width = 56
        '
        '売単価
        '
        Me.売単価.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.売単価.DefaultCellStyle = DataGridViewCellStyle2
        Me.売単価.HeaderText = "売単価"
        Me.売単価.Name = "売単価"
        Me.売単価.ReadOnly = True
        Me.売単価.Width = 68
        '
        'ＶＡＴ
        '
        Me.ＶＡＴ.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.ＶＡＴ.DefaultCellStyle = DataGridViewCellStyle3
        Me.ＶＡＴ.HeaderText = "ＶＡＴ"
        Me.ＶＡＴ.Name = "ＶＡＴ"
        Me.ＶＡＴ.ReadOnly = True
        Me.ＶＡＴ.Width = 57
        '
        '売上計
        '
        Me.売上計.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.売上計.DefaultCellStyle = DataGridViewCellStyle4
        Me.売上計.HeaderText = "売上計"
        Me.売上計.Name = "売上計"
        Me.売上計.ReadOnly = True
        Me.売上計.Width = 68
        '
        'SalesVATList
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.LblYear)
        Me.Controls.Add(Me.LblMonth)
        Me.Controls.Add(Me.TxtSalesAmount)
        Me.Controls.Add(Me.LblSalesAmount)
        Me.Controls.Add(Me.cmbMonth)
        Me.Controls.Add(Me.cmbYear)
        Me.Controls.Add(Me.LblMode)
        Me.Controls.Add(Me.BtnExcelOutput)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.DgvList)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "SalesVATList"
        Me.Text = "SalesVATList"
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
    Friend WithEvents TxtSalesAmount As TextBox
    Friend WithEvents LblSalesAmount As Label
    Friend WithEvents LblMonth As Label
    Friend WithEvents LblYear As Label
    Friend WithEvents 売上番号 As DataGridViewTextBoxColumn
    Friend WithEvents 売上日 As DataGridViewTextBoxColumn
    Friend WithEvents 得意先名 As DataGridViewTextBoxColumn
    Friend WithEvents 客先番号 As DataGridViewTextBoxColumn
    Friend WithEvents 営業担当者 As DataGridViewTextBoxColumn
    Friend WithEvents メーカー As DataGridViewTextBoxColumn
    Friend WithEvents 品名 As DataGridViewTextBoxColumn
    Friend WithEvents 型式 As DataGridViewTextBoxColumn
    Friend WithEvents 数量 As DataGridViewTextBoxColumn
    Friend WithEvents 単位 As DataGridViewTextBoxColumn
    Friend WithEvents 通貨_外貨 As DataGridViewTextBoxColumn
    Friend WithEvents 売単価 As DataGridViewTextBoxColumn
    Friend WithEvents ＶＡＴ As DataGridViewTextBoxColumn
    Friend WithEvents 売上計 As DataGridViewTextBoxColumn
End Class
