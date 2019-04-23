<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BillingList
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.BtnBillingCancel = New System.Windows.Forms.Button()
        Me.ChkCancelData = New System.Windows.Forms.CheckBox()
        Me.BtnBillingView = New System.Windows.Forms.Button()
        Me.RbtnSlip = New System.Windows.Forms.RadioButton()
        Me.RbtnDetails = New System.Windows.Forms.RadioButton()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TxtBillingNoSince = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtCustomerCode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblConditions = New System.Windows.Forms.Label()
        Me.TxtCustomerName = New System.Windows.Forms.TextBox()
        Me.BtnBillingSearch = New System.Windows.Forms.Button()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.DgvBilling = New System.Windows.Forms.DataGridView()
        Me.LblMode = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TxtCustomerPO = New System.Windows.Forms.TextBox()
        Me.dtBillingDateSince = New System.Windows.Forms.DateTimePicker()
        Me.dtBillingDateUntil = New System.Windows.Forms.DateTimePicker()
        CType(Me.DgvBilling, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnBillingCancel
        '
        Me.BtnBillingCancel.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBillingCancel.Location = New System.Drawing.Point(832, 509)
        Me.BtnBillingCancel.Name = "BtnBillingCancel"
        Me.BtnBillingCancel.Size = New System.Drawing.Size(165, 40)
        Me.BtnBillingCancel.TabIndex = 13
        Me.BtnBillingCancel.Text = "請求取消"
        Me.BtnBillingCancel.UseVisualStyleBackColor = True
        Me.BtnBillingCancel.Visible = False
        '
        'ChkCancelData
        '
        Me.ChkCancelData.AutoSize = True
        Me.ChkCancelData.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ChkCancelData.Location = New System.Drawing.Point(16, 196)
        Me.ChkCancelData.Name = "ChkCancelData"
        Me.ChkCancelData.Size = New System.Drawing.Size(139, 19)
        Me.ChkCancelData.TabIndex = 9
        Me.ChkCancelData.Text = "取消データを含める"
        Me.ChkCancelData.UseVisualStyleBackColor = True
        '
        'BtnBillingView
        '
        Me.BtnBillingView.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBillingView.Location = New System.Drawing.Point(1003, 509)
        Me.BtnBillingView.Name = "BtnBillingView"
        Me.BtnBillingView.Size = New System.Drawing.Size(165, 40)
        Me.BtnBillingView.TabIndex = 14
        Me.BtnBillingView.Text = "請求参照"
        Me.BtnBillingView.UseVisualStyleBackColor = True
        Me.BtnBillingView.Visible = False
        '
        'RbtnSlip
        '
        Me.RbtnSlip.AutoSize = True
        Me.RbtnSlip.Checked = True
        Me.RbtnSlip.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.RbtnSlip.Location = New System.Drawing.Point(1146, 196)
        Me.RbtnSlip.Name = "RbtnSlip"
        Me.RbtnSlip.Size = New System.Drawing.Size(89, 19)
        Me.RbtnSlip.TabIndex = 10
        Me.RbtnSlip.TabStop = True
        Me.RbtnSlip.Text = "伝票単位"
        Me.RbtnSlip.UseVisualStyleBackColor = True
        Me.RbtnSlip.Visible = False
        '
        'RbtnDetails
        '
        Me.RbtnDetails.AutoSize = True
        Me.RbtnDetails.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.RbtnDetails.Location = New System.Drawing.Point(1241, 196)
        Me.RbtnDetails.Name = "RbtnDetails"
        Me.RbtnDetails.Size = New System.Drawing.Size(89, 19)
        Me.RbtnDetails.TabIndex = 11
        Me.RbtnDetails.Text = "明細単位"
        Me.RbtnDetails.UseVisualStyleBackColor = True
        Me.RbtnDetails.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(13, 162)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(87, 15)
        Me.Label10.TabIndex = 145
        Me.Label10.Text = "■表示形式"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(936, 46)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(17, 12)
        Me.Label5.TabIndex = 143
        Me.Label5.Text = "～"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(584, 67)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(170, 22)
        Me.Label7.TabIndex = 138
        Me.Label7.Text = "請求番号"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtBillingNoSince
        '
        Me.TxtBillingNoSince.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtBillingNoSince.Location = New System.Drawing.Point(760, 67)
        Me.TxtBillingNoSince.Name = "TxtBillingNoSince"
        Me.TxtBillingNoSince.Size = New System.Drawing.Size(170, 22)
        Me.TxtBillingNoSince.TabIndex = 5
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(584, 39)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(170, 22)
        Me.Label8.TabIndex = 136
        Me.Label8.Text = "請求日"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(14, 68)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(170, 22)
        Me.Label4.TabIndex = 134
        Me.Label4.Text = "得意先コード"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtCustomerCode
        '
        Me.TxtCustomerCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerCode.Location = New System.Drawing.Point(190, 68)
        Me.TxtCustomerCode.Name = "TxtCustomerCode"
        Me.TxtCustomerCode.Size = New System.Drawing.Size(350, 22)
        Me.TxtCustomerCode.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(14, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(170, 22)
        Me.Label1.TabIndex = 128
        Me.Label1.Text = "得意先名"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblConditions
        '
        Me.LblConditions.AutoSize = True
        Me.LblConditions.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblConditions.Location = New System.Drawing.Point(11, 13)
        Me.LblConditions.Name = "LblConditions"
        Me.LblConditions.Size = New System.Drawing.Size(87, 15)
        Me.LblConditions.TabIndex = 127
        Me.LblConditions.Text = "■抽出条件"
        '
        'TxtCustomerName
        '
        Me.TxtCustomerName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerName.Location = New System.Drawing.Point(190, 40)
        Me.TxtCustomerName.Name = "TxtCustomerName"
        Me.TxtCustomerName.Size = New System.Drawing.Size(350, 22)
        Me.TxtCustomerName.TabIndex = 1
        '
        'BtnBillingSearch
        '
        Me.BtnBillingSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBillingSearch.Location = New System.Drawing.Point(1174, 41)
        Me.BtnBillingSearch.Name = "BtnBillingSearch"
        Me.BtnBillingSearch.Size = New System.Drawing.Size(165, 40)
        Me.BtnBillingSearch.TabIndex = 8
        Me.BtnBillingSearch.Text = "検索"
        Me.BtnBillingSearch.UseVisualStyleBackColor = True
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(1174, 509)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 15
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'DgvBilling
        '
        Me.DgvBilling.AllowUserToAddRows = False
        Me.DgvBilling.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader
        Me.DgvBilling.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvBilling.Location = New System.Drawing.Point(13, 236)
        Me.DgvBilling.Name = "DgvBilling"
        Me.DgvBilling.ReadOnly = True
        Me.DgvBilling.RowHeadersVisible = False
        Me.DgvBilling.RowTemplate.Height = 21
        Me.DgvBilling.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvBilling.Size = New System.Drawing.Size(1326, 267)
        Me.DgvBilling.TabIndex = 12
        '
        'LblMode
        '
        Me.LblMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMode.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMode.Location = New System.Drawing.Point(1174, 9)
        Me.LblMode.Name = "LblMode"
        Me.LblMode.Size = New System.Drawing.Size(165, 22)
        Me.LblMode.TabIndex = 306
        Me.LblMode.Text = "モード"
        Me.LblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label11.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(584, 95)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(170, 22)
        Me.Label11.TabIndex = 327
        Me.Label11.Text = "客先番号"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtCustomerPO
        '
        Me.TxtCustomerPO.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerPO.Location = New System.Drawing.Point(760, 95)
        Me.TxtCustomerPO.Name = "TxtCustomerPO"
        Me.TxtCustomerPO.Size = New System.Drawing.Size(170, 22)
        Me.TxtCustomerPO.TabIndex = 7
        '
        'dtBillingDateSince
        '
        Me.dtBillingDateSince.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtBillingDateSince.CustomFormat = ""
        Me.dtBillingDateSince.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtBillingDateSince.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtBillingDateSince.Location = New System.Drawing.Point(760, 39)
        Me.dtBillingDateSince.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtBillingDateSince.Name = "dtBillingDateSince"
        Me.dtBillingDateSince.Size = New System.Drawing.Size(170, 22)
        Me.dtBillingDateSince.TabIndex = 331
        Me.dtBillingDateSince.Value = New Date(2019, 1, 1, 0, 0, 0, 0)
        '
        'dtBillingDateUntil
        '
        Me.dtBillingDateUntil.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtBillingDateUntil.CustomFormat = ""
        Me.dtBillingDateUntil.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtBillingDateUntil.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtBillingDateUntil.Location = New System.Drawing.Point(959, 40)
        Me.dtBillingDateUntil.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtBillingDateUntil.Name = "dtBillingDateUntil"
        Me.dtBillingDateUntil.Size = New System.Drawing.Size(170, 22)
        Me.dtBillingDateUntil.TabIndex = 332
        Me.dtBillingDateUntil.Value = New Date(2019, 1, 1, 0, 0, 0, 0)
        '
        'BillingList
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.dtBillingDateUntil)
        Me.Controls.Add(Me.dtBillingDateSince)
        Me.Controls.Add(Me.RbtnDetails)
        Me.Controls.Add(Me.RbtnSlip)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.TxtCustomerPO)
        Me.Controls.Add(Me.LblMode)
        Me.Controls.Add(Me.BtnBillingCancel)
        Me.Controls.Add(Me.ChkCancelData)
        Me.Controls.Add(Me.BtnBillingView)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TxtBillingNoSince)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtCustomerCode)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LblConditions)
        Me.Controls.Add(Me.TxtCustomerName)
        Me.Controls.Add(Me.BtnBillingSearch)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.DgvBilling)
        Me.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "BillingList"
        Me.Text = "BillingList"
        CType(Me.DgvBilling, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnBillingCancel As Button
    Friend WithEvents ChkCancelData As CheckBox
    Friend WithEvents BtnBillingView As Button
    Friend WithEvents RbtnSlip As RadioButton
    Friend WithEvents RbtnDetails As RadioButton
    Friend WithEvents Label10 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents TxtBillingNoSince As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents TxtCustomerCode As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents LblConditions As Label
    Friend WithEvents TxtCustomerName As TextBox
    Friend WithEvents BtnBillingSearch As Button
    Friend WithEvents BtnBack As Button
    Friend WithEvents DgvBilling As DataGridView
    Friend WithEvents LblMode As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents TxtCustomerPO As TextBox
    Friend WithEvents dtBillingDateSince As DateTimePicker
    Friend WithEvents dtBillingDateUntil As DateTimePicker
End Class
