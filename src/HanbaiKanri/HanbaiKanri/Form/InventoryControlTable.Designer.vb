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
        Me.DtpMovingDayFrom = New System.Windows.Forms.DateTimePicker()
        Me.CmWarehouseFrom = New System.Windows.Forms.ComboBox()
        Me.LblMovingDay = New System.Windows.Forms.Label()
        Me.LblWarehouse = New System.Windows.Forms.Label()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.LblStorageType = New System.Windows.Forms.Label()
        Me.DtpMovingDayTo = New System.Windows.Forms.DateTimePicker()
        Me.CmStorageTypeFrom = New System.Windows.Forms.ComboBox()
        Me.CmStorageTypeTo = New System.Windows.Forms.ComboBox()
        CType(Me.DgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(1174, 509)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 23
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'DgvList
        '
        Me.DgvList.AllowUserToAddRows = False
        Me.DgvList.AllowUserToDeleteRows = False
        Me.DgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvList.Location = New System.Drawing.Point(13, 105)
        Me.DgvList.Name = "DgvList"
        Me.DgvList.ReadOnly = True
        Me.DgvList.RowHeadersVisible = False
        Me.DgvList.RowTemplate.Height = 21
        Me.DgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvList.Size = New System.Drawing.Size(1326, 390)
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
        '
        'BtnExcelOutput
        '
        Me.BtnExcelOutput.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnExcelOutput.Location = New System.Drawing.Point(1003, 509)
        Me.BtnExcelOutput.Name = "BtnExcelOutput"
        Me.BtnExcelOutput.Size = New System.Drawing.Size(165, 40)
        Me.BtnExcelOutput.TabIndex = 22
        Me.BtnExcelOutput.Text = "Excel出力"
        Me.BtnExcelOutput.UseVisualStyleBackColor = True
        '
        'DtpMovingDayFrom
        '
        Me.DtpMovingDayFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DtpMovingDayFrom.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpMovingDayFrom.CustomFormat = ""
        Me.DtpMovingDayFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpMovingDayFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpMovingDayFrom.Location = New System.Drawing.Point(279, 32)
        Me.DtpMovingDayFrom.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.DtpMovingDayFrom.Name = "DtpMovingDayFrom"
        Me.DtpMovingDayFrom.Size = New System.Drawing.Size(199, 22)
        Me.DtpMovingDayFrom.TabIndex = 320
        Me.DtpMovingDayFrom.TabStop = False
        Me.DtpMovingDayFrom.Value = New Date(2019, 1, 1, 0, 0, 0, 0)
        '
        'CmWarehouseFrom
        '
        Me.CmWarehouseFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CmWarehouseFrom.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmWarehouseFrom.FormattingEnabled = True
        Me.CmWarehouseFrom.Items.AddRange(New Object() {"RegularShip", "DHL", "Air", "Ship"})
        Me.CmWarehouseFrom.Location = New System.Drawing.Point(279, 3)
        Me.CmWarehouseFrom.Name = "CmWarehouseFrom"
        Me.CmWarehouseFrom.Size = New System.Drawing.Size(199, 23)
        Me.CmWarehouseFrom.TabIndex = 319
        '
        'LblMovingDay
        '
        Me.LblMovingDay.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblMovingDay.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMovingDay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMovingDay.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMovingDay.Location = New System.Drawing.Point(3, 32)
        Me.LblMovingDay.Name = "LblMovingDay"
        Me.LblMovingDay.Size = New System.Drawing.Size(270, 22)
        Me.LblMovingDay.TabIndex = 74
        Me.LblMovingDay.Text = "移動日"
        Me.LblMovingDay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblWarehouse
        '
        Me.LblWarehouse.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblWarehouse.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblWarehouse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblWarehouse.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblWarehouse.Location = New System.Drawing.Point(3, 3)
        Me.LblWarehouse.Name = "LblWarehouse"
        Me.LblWarehouse.Size = New System.Drawing.Size(270, 22)
        Me.LblWarehouse.TabIndex = 73
        Me.LblWarehouse.Text = "倉庫"
        Me.LblWarehouse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 4
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.29851!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.85075!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.85075!))
        Me.TableLayoutPanel2.Controls.Add(Me.LblStorageType, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.LblWarehouse, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.LblMovingDay, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.CmWarehouseFrom, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.DtpMovingDayFrom, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.DtpMovingDayTo, 3, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.CmStorageTypeFrom, 1, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.CmStorageTypeTo, 3, 2)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(13, 12)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 3
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(707, 87)
        Me.TableLayoutPanel2.TabIndex = 76
        '
        'LblStorageType
        '
        Me.LblStorageType.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblStorageType.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblStorageType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblStorageType.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblStorageType.Location = New System.Drawing.Point(3, 62)
        Me.LblStorageType.Name = "LblStorageType"
        Me.LblStorageType.Size = New System.Drawing.Size(270, 21)
        Me.LblStorageType.TabIndex = 318
        Me.LblStorageType.Text = "入出庫種別"
        Me.LblStorageType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DtpMovingDayTo
        '
        Me.DtpMovingDayTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DtpMovingDayTo.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpMovingDayTo.CustomFormat = ""
        Me.DtpMovingDayTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpMovingDayTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpMovingDayTo.Location = New System.Drawing.Point(504, 32)
        Me.DtpMovingDayTo.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.DtpMovingDayTo.Name = "DtpMovingDayTo"
        Me.DtpMovingDayTo.Size = New System.Drawing.Size(200, 22)
        Me.DtpMovingDayTo.TabIndex = 321
        Me.DtpMovingDayTo.TabStop = False
        Me.DtpMovingDayTo.Value = New Date(2019, 1, 1, 0, 0, 0, 0)
        '
        'CmStorageTypeFrom
        '
        Me.CmStorageTypeFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CmStorageTypeFrom.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmStorageTypeFrom.FormattingEnabled = True
        Me.CmStorageTypeFrom.Items.AddRange(New Object() {"RegularShip", "DHL", "Air", "Ship"})
        Me.CmStorageTypeFrom.Location = New System.Drawing.Point(279, 61)
        Me.CmStorageTypeFrom.Name = "CmStorageTypeFrom"
        Me.CmStorageTypeFrom.Size = New System.Drawing.Size(199, 23)
        Me.CmStorageTypeFrom.TabIndex = 323
        '
        'CmStorageTypeTo
        '
        Me.CmStorageTypeTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CmStorageTypeTo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmStorageTypeTo.FormattingEnabled = True
        Me.CmStorageTypeTo.Items.AddRange(New Object() {"RegularShip", "DHL", "Air", "Ship"})
        Me.CmStorageTypeTo.Location = New System.Drawing.Point(504, 61)
        Me.CmStorageTypeTo.Name = "CmStorageTypeTo"
        Me.CmStorageTypeTo.Size = New System.Drawing.Size(200, 23)
        Me.CmStorageTypeTo.TabIndex = 324
        '
        'InventoryControlTable
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Controls.Add(Me.LblMode)
        Me.Controls.Add(Me.BtnExcelOutput)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.DgvList)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "InventoryControlTable"
        Me.Text = "InventoryControlTable"
        CType(Me.DgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnBack As Button
    Friend WithEvents DgvList As DataGridView
    Friend WithEvents LblMode As Label
    Friend WithEvents BtnExcelOutput As Button
    Friend WithEvents VAT As DataGridViewTextBoxColumn
    Friend WithEvents DtpMovingDayFrom As DateTimePicker
    Friend WithEvents CmWarehouseFrom As ComboBox
    Friend WithEvents LblMovingDay As Label
    Friend WithEvents LblWarehouse As Label
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents LblStorageType As Label
    Friend WithEvents DtpMovingDayTo As DateTimePicker
    Friend WithEvents CmStorageTypeFrom As ComboBox
    Friend WithEvents CmStorageTypeTo As ComboBox
End Class
