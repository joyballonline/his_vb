<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class InventoryControlTable
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
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.DgvList = New System.Windows.Forms.DataGridView()
        Me.LblMode = New System.Windows.Forms.Label()
        Me.BtnExcelOutput = New System.Windows.Forms.Button()
        Me.cmd検索 = New System.Windows.Forms.Button()
        Me.TxtSpec = New System.Windows.Forms.TextBox()
        Me.LblItemName = New System.Windows.Forms.Label()
        Me.CmWarehouseFrom = New System.Windows.Forms.ComboBox()
        Me.LblWarehouse = New System.Windows.Forms.Label()
        Me.LblMovingDay = New System.Windows.Forms.Label()
        Me.lblMaker = New System.Windows.Forms.Label()
        Me.txtMaker = New System.Windows.Forms.TextBox()
        Me.TxtItemName = New System.Windows.Forms.TextBox()
        Me.LblSpec = New System.Windows.Forms.Label()
        Me.txtYear = New System.Windows.Forms.TextBox()
        Me.lblYear = New System.Windows.Forms.Label()
        Me.lblMonth = New System.Windows.Forms.Label()
        Me.txtMonth = New System.Windows.Forms.TextBox()
        CType(Me.DgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(1174, 509)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 8
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'DgvList
        '
        Me.DgvList.AllowUserToAddRows = False
        Me.DgvList.AllowUserToDeleteRows = False
        Me.DgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvList.Location = New System.Drawing.Point(13, 159)
        Me.DgvList.Name = "DgvList"
        Me.DgvList.ReadOnly = True
        Me.DgvList.RowHeadersVisible = False
        Me.DgvList.RowTemplate.Height = 21
        Me.DgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvList.Size = New System.Drawing.Size(1326, 336)
        Me.DgvList.TabIndex = 15
        '
        'LblMode
        '
        Me.LblMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMode.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMode.Location = New System.Drawing.Point(1106, 9)
        Me.LblMode.Name = "LblMode"
        Me.LblMode.Size = New System.Drawing.Size(233, 22)
        Me.LblMode.TabIndex = 67
        Me.LblMode.Text = "モード"
        Me.LblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.LblMode.Visible = False
        '
        'BtnExcelOutput
        '
        Me.BtnExcelOutput.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnExcelOutput.Location = New System.Drawing.Point(1003, 509)
        Me.BtnExcelOutput.Name = "BtnExcelOutput"
        Me.BtnExcelOutput.Size = New System.Drawing.Size(165, 40)
        Me.BtnExcelOutput.TabIndex = 9
        Me.BtnExcelOutput.Text = "Excel出力"
        Me.BtnExcelOutput.UseVisualStyleBackColor = True
        '
        'cmd検索
        '
        Me.cmd検索.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmd検索.Location = New System.Drawing.Point(449, 6)
        Me.cmd検索.Name = "cmd検索"
        Me.cmd検索.Size = New System.Drawing.Size(165, 40)
        Me.cmd検索.TabIndex = 7
        Me.cmd検索.Text = "検索"
        Me.cmd検索.UseVisualStyleBackColor = True
        '
        'TxtSpec
        '
        Me.TxtSpec.CausesValidation = False
        Me.TxtSpec.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSpec.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TxtSpec.Location = New System.Drawing.Point(189, 118)
        Me.TxtSpec.Name = "TxtSpec"
        Me.TxtSpec.Size = New System.Drawing.Size(218, 22)
        Me.TxtSpec.TabIndex = 6
        '
        'LblItemName
        '
        Me.LblItemName.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblItemName.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblItemName.Location = New System.Drawing.Point(13, 91)
        Me.LblItemName.Name = "LblItemName"
        Me.LblItemName.Size = New System.Drawing.Size(170, 22)
        Me.LblItemName.TabIndex = 360
        Me.LblItemName.Text = "品名"
        Me.LblItemName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CmWarehouseFrom
        '
        Me.CmWarehouseFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CmWarehouseFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmWarehouseFrom.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmWarehouseFrom.FormattingEnabled = True
        Me.CmWarehouseFrom.Items.AddRange(New Object() {"RegularShip", "DHL", "Air", "Ship"})
        Me.CmWarehouseFrom.Location = New System.Drawing.Point(189, 6)
        Me.CmWarehouseFrom.Name = "CmWarehouseFrom"
        Me.CmWarehouseFrom.Size = New System.Drawing.Size(218, 23)
        Me.CmWarehouseFrom.TabIndex = 0
        '
        'LblWarehouse
        '
        Me.LblWarehouse.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblWarehouse.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblWarehouse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblWarehouse.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblWarehouse.Location = New System.Drawing.Point(13, 7)
        Me.LblWarehouse.Name = "LblWarehouse"
        Me.LblWarehouse.Size = New System.Drawing.Size(170, 22)
        Me.LblWarehouse.TabIndex = 358
        Me.LblWarehouse.Text = "倉庫"
        Me.LblWarehouse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblMovingDay
        '
        Me.LblMovingDay.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblMovingDay.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMovingDay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMovingDay.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMovingDay.Location = New System.Drawing.Point(13, 37)
        Me.LblMovingDay.Name = "LblMovingDay"
        Me.LblMovingDay.Size = New System.Drawing.Size(170, 22)
        Me.LblMovingDay.TabIndex = 359
        Me.LblMovingDay.Text = "対象年月"
        Me.LblMovingDay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMaker
        '
        Me.lblMaker.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblMaker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMaker.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMaker.Location = New System.Drawing.Point(13, 64)
        Me.lblMaker.Name = "lblMaker"
        Me.lblMaker.Size = New System.Drawing.Size(170, 22)
        Me.lblMaker.TabIndex = 362
        Me.lblMaker.Text = "メーカー"
        Me.lblMaker.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtMaker
        '
        Me.txtMaker.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtMaker.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtMaker.Location = New System.Drawing.Point(189, 65)
        Me.txtMaker.Name = "txtMaker"
        Me.txtMaker.Size = New System.Drawing.Size(218, 22)
        Me.txtMaker.TabIndex = 3
        '
        'TxtItemName
        '
        Me.TxtItemName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtItemName.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TxtItemName.Location = New System.Drawing.Point(189, 91)
        Me.TxtItemName.Name = "TxtItemName"
        Me.TxtItemName.Size = New System.Drawing.Size(218, 22)
        Me.TxtItemName.TabIndex = 4
        '
        'LblSpec
        '
        Me.LblSpec.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblSpec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblSpec.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSpec.Location = New System.Drawing.Point(13, 118)
        Me.LblSpec.Name = "LblSpec"
        Me.LblSpec.Size = New System.Drawing.Size(170, 22)
        Me.LblSpec.TabIndex = 361
        Me.LblSpec.Text = "型式"
        Me.LblSpec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtYear
        '
        Me.txtYear.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtYear.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtYear.Location = New System.Drawing.Point(253, 38)
        Me.txtYear.Name = "txtYear"
        Me.txtYear.Size = New System.Drawing.Size(52, 22)
        Me.txtYear.TabIndex = 1
        Me.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblYear
        '
        Me.lblYear.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblYear.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblYear.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblYear.Location = New System.Drawing.Point(189, 37)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.Size = New System.Drawing.Size(58, 22)
        Me.lblYear.TabIndex = 364
        Me.lblYear.Text = "年"
        Me.lblYear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMonth
        '
        Me.lblMonth.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMonth.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMonth.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMonth.Location = New System.Drawing.Point(311, 38)
        Me.lblMonth.Name = "lblMonth"
        Me.lblMonth.Size = New System.Drawing.Size(58, 22)
        Me.lblMonth.TabIndex = 365
        Me.lblMonth.Text = "月"
        Me.lblMonth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtMonth
        '
        Me.txtMonth.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtMonth.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtMonth.Location = New System.Drawing.Point(375, 39)
        Me.txtMonth.Name = "txtMonth"
        Me.txtMonth.Size = New System.Drawing.Size(32, 22)
        Me.txtMonth.TabIndex = 2
        Me.txtMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'InventoryControlTable
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.txtMonth)
        Me.Controls.Add(Me.lblMonth)
        Me.Controls.Add(Me.lblYear)
        Me.Controls.Add(Me.txtYear)
        Me.Controls.Add(Me.TxtSpec)
        Me.Controls.Add(Me.LblItemName)
        Me.Controls.Add(Me.CmWarehouseFrom)
        Me.Controls.Add(Me.LblWarehouse)
        Me.Controls.Add(Me.LblMovingDay)
        Me.Controls.Add(Me.lblMaker)
        Me.Controls.Add(Me.txtMaker)
        Me.Controls.Add(Me.TxtItemName)
        Me.Controls.Add(Me.LblSpec)
        Me.Controls.Add(Me.cmd検索)
        Me.Controls.Add(Me.LblMode)
        Me.Controls.Add(Me.BtnExcelOutput)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.DgvList)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "InventoryControlTable"
        Me.Text = "InventoryControlTable"
        CType(Me.DgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnBack As Button
    Friend WithEvents DgvList As DataGridView
    Friend WithEvents LblMode As Label
    Friend WithEvents BtnExcelOutput As Button
    Friend WithEvents VAT As DataGridViewTextBoxColumn
    Friend WithEvents cmd検索 As Button
    Friend WithEvents TxtSpec As TextBox
    Friend WithEvents LblItemName As Label
    Friend WithEvents CmWarehouseFrom As ComboBox
    Friend WithEvents LblWarehouse As Label
    Friend WithEvents LblMovingDay As Label
    Friend WithEvents lblMaker As Label
    Friend WithEvents txtMaker As TextBox
    Friend WithEvents TxtItemName As TextBox
    Friend WithEvents LblSpec As Label
    Friend WithEvents txtYear As TextBox
    Friend WithEvents lblYear As Label
    Friend WithEvents lblMonth As Label
    Friend WithEvents txtMonth As TextBox
End Class
