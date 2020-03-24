<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MovementInput
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
        Me.LblMode = New System.Windows.Forms.Label()
        Me.BtnRegistration = New System.Windows.Forms.Button()
        Me.LblProcessingClassification = New System.Windows.Forms.Label()
        Me.LblProcessingDate = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.DtpProcessingDate = New System.Windows.Forms.DateTimePicker()
        Me.CmProcessingClassification = New System.Windows.Forms.ComboBox()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.TxtManufacturer = New System.Windows.Forms.TextBox()
        Me.LblManufacturer = New System.Windows.Forms.Label()
        Me.LblItemName = New System.Windows.Forms.Label()
        Me.TxtItemName = New System.Windows.Forms.TextBox()
        Me.TxtSpec = New System.Windows.Forms.TextBox()
        Me.LblSpec = New System.Windows.Forms.Label()
        Me.BtnSearch = New System.Windows.Forms.Button()
        Me.LblMovingSource = New System.Windows.Forms.Label()
        Me.LblMovingDestination = New System.Windows.Forms.Label()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.LblWarehouseSince = New System.Windows.Forms.Label()
        Me.TxtQuantityFrom = New System.Windows.Forms.TextBox()
        Me.TxtWarehouseSince = New System.Windows.Forms.TextBox()
        Me.TxtStorageTypeSince = New System.Windows.Forms.TextBox()
        Me.TxtUnitPrice = New System.Windows.Forms.TextBox()
        Me.TxtGoodsReceiptDate = New System.Windows.Forms.TextBox()
        Me.LblStorageTypeSince = New System.Windows.Forms.Label()
        Me.LblQuantityFrom = New System.Windows.Forms.Label()
        Me.LblUnitPrice = New System.Windows.Forms.Label()
        Me.LblGoodsReceiptDate = New System.Windows.Forms.Label()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.LblWarehouseTo = New System.Windows.Forms.Label()
        Me.LblStorageTypeTo = New System.Windows.Forms.Label()
        Me.LblLocationTo = New System.Windows.Forms.Label()
        Me.LblSerialNoTo = New System.Windows.Forms.Label()
        Me.LblQuantityTo = New System.Windows.Forms.Label()
        Me.LblUnitPriceTo = New System.Windows.Forms.Label()
        Me.LblOrderNoTo = New System.Windows.Forms.Label()
        Me.LblLocationSince = New System.Windows.Forms.Label()
        Me.LblSerialNoSince = New System.Windows.Forms.Label()
        Me.LblOrderNoSince = New System.Windows.Forms.Label()
        Me.TxtLocationSince = New System.Windows.Forms.TextBox()
        Me.TxtSerialNoSince = New System.Windows.Forms.TextBox()
        Me.TxtOrderNoSince = New System.Windows.Forms.TextBox()
        Me.TxtQuantityTo = New System.Windows.Forms.TextBox()
        Me.CmStorageTypeTo = New System.Windows.Forms.ComboBox()
        Me.CmWarehouseTo = New System.Windows.Forms.ComboBox()
        Me.TxtUnitPriceTo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LblGoodsReceiptDateTo = New System.Windows.Forms.Label()
        Me.TxtGoodsReceiptDateTo = New System.Windows.Forms.TextBox()
        Me.TxtSerialNoTo = New System.Windows.Forms.TextBox()
        Me.TxtOrderNoTo = New System.Windows.Forms.TextBox()
        Me.TxtLocationTo = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(660, 465)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 2
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'LblMode
        '
        Me.LblMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMode.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMode.Location = New System.Drawing.Point(592, 9)
        Me.LblMode.Name = "LblMode"
        Me.LblMode.Size = New System.Drawing.Size(233, 22)
        Me.LblMode.TabIndex = 67
        Me.LblMode.Text = "モード"
        Me.LblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnRegistration
        '
        Me.BtnRegistration.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnRegistration.Location = New System.Drawing.Point(489, 465)
        Me.BtnRegistration.Name = "BtnRegistration"
        Me.BtnRegistration.Size = New System.Drawing.Size(165, 40)
        Me.BtnRegistration.TabIndex = 1
        Me.BtnRegistration.Text = "登録"
        Me.BtnRegistration.UseVisualStyleBackColor = True
        '
        'LblProcessingClassification
        '
        Me.LblProcessingClassification.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblProcessingClassification.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblProcessingClassification.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblProcessingClassification.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblProcessingClassification.Location = New System.Drawing.Point(3, 32)
        Me.LblProcessingClassification.Name = "LblProcessingClassification"
        Me.LblProcessingClassification.Size = New System.Drawing.Size(235, 23)
        Me.LblProcessingClassification.TabIndex = 2
        Me.LblProcessingClassification.Text = "処理区分"
        Me.LblProcessingClassification.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblProcessingDate
        '
        Me.LblProcessingDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblProcessingDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblProcessingDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblProcessingDate.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblProcessingDate.Location = New System.Drawing.Point(3, 3)
        Me.LblProcessingDate.Name = "LblProcessingDate"
        Me.LblProcessingDate.Size = New System.Drawing.Size(235, 23)
        Me.LblProcessingDate.TabIndex = 0
        Me.LblProcessingDate.Text = "処理日"
        Me.LblProcessingDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.LblProcessingDate, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DtpProcessingDate, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LblProcessingClassification, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.CmProcessingClassification, 1, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(9, 9)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(482, 58)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'DtpProcessingDate
        '
        Me.DtpProcessingDate.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.DtpProcessingDate.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpProcessingDate.CustomFormat = ""
        Me.DtpProcessingDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpProcessingDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpProcessingDate.Location = New System.Drawing.Point(244, 3)
        Me.DtpProcessingDate.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.DtpProcessingDate.Name = "DtpProcessingDate"
        Me.DtpProcessingDate.Size = New System.Drawing.Size(148, 22)
        Me.DtpProcessingDate.TabIndex = 1
        Me.DtpProcessingDate.TabStop = False
        Me.DtpProcessingDate.Value = New Date(2019, 1, 1, 0, 0, 0, 0)
        '
        'CmProcessingClassification
        '
        Me.CmProcessingClassification.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CmProcessingClassification.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmProcessingClassification.FormattingEnabled = True
        Me.CmProcessingClassification.Items.AddRange(New Object() {"RegularShip", "DHL", "Air", "Ship"})
        Me.CmProcessingClassification.Location = New System.Drawing.Point(244, 32)
        Me.CmProcessingClassification.Name = "CmProcessingClassification"
        Me.CmProcessingClassification.Size = New System.Drawing.Size(235, 23)
        Me.CmProcessingClassification.TabIndex = 3
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.AutoSize = True
        Me.TableLayoutPanel3.ColumnCount = 3
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.82313!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.17687!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.TxtManufacturer, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.LblManufacturer, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.LblItemName, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.TxtItemName, 1, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.TxtSpec, 1, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.LblSpec, 0, 2)
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(9, 84)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 3
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(609, 87)
        Me.TableLayoutPanel3.TabIndex = 2
        '
        'TxtManufacturer
        '
        Me.TxtManufacturer.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtManufacturer.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtManufacturer.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtManufacturer.Location = New System.Drawing.Point(196, 3)
        Me.TxtManufacturer.MaxLength = 50
        Me.TxtManufacturer.Name = "TxtManufacturer"
        Me.TxtManufacturer.Size = New System.Drawing.Size(389, 23)
        Me.TxtManufacturer.TabIndex = 1
        Me.TxtManufacturer.TabStop = False
        '
        'LblManufacturer
        '
        Me.LblManufacturer.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblManufacturer.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblManufacturer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblManufacturer.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblManufacturer.Location = New System.Drawing.Point(3, 3)
        Me.LblManufacturer.Name = "LblManufacturer"
        Me.LblManufacturer.Size = New System.Drawing.Size(187, 23)
        Me.LblManufacturer.TabIndex = 0
        Me.LblManufacturer.Text = "メーカー"
        Me.LblManufacturer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblItemName
        '
        Me.LblItemName.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblItemName.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblItemName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblItemName.Location = New System.Drawing.Point(3, 32)
        Me.LblItemName.Name = "LblItemName"
        Me.LblItemName.Size = New System.Drawing.Size(187, 23)
        Me.LblItemName.TabIndex = 2
        Me.LblItemName.Text = "品名"
        Me.LblItemName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtItemName
        '
        Me.TxtItemName.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtItemName.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtItemName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtItemName.Location = New System.Drawing.Point(196, 32)
        Me.TxtItemName.MaxLength = 50
        Me.TxtItemName.Name = "TxtItemName"
        Me.TxtItemName.Size = New System.Drawing.Size(389, 22)
        Me.TxtItemName.TabIndex = 3
        Me.TxtItemName.TabStop = False
        '
        'TxtSpec
        '
        Me.TxtSpec.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtSpec.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtSpec.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSpec.Location = New System.Drawing.Point(196, 61)
        Me.TxtSpec.MaxLength = 255
        Me.TxtSpec.Name = "TxtSpec"
        Me.TxtSpec.Size = New System.Drawing.Size(389, 22)
        Me.TxtSpec.TabIndex = 5
        Me.TxtSpec.TabStop = False
        '
        'LblSpec
        '
        Me.LblSpec.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblSpec.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblSpec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblSpec.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSpec.Location = New System.Drawing.Point(3, 61)
        Me.LblSpec.Name = "LblSpec"
        Me.LblSpec.Size = New System.Drawing.Size(187, 23)
        Me.LblSpec.TabIndex = 4
        Me.LblSpec.Text = "型式"
        Me.LblSpec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnSearch
        '
        Me.BtnSearch.Location = New System.Drawing.Point(638, 109)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(120, 40)
        Me.BtnSearch.TabIndex = 0
        Me.BtnSearch.TabStop = False
        Me.BtnSearch.Text = "在庫検索"
        Me.BtnSearch.UseVisualStyleBackColor = True
        '
        'LblMovingSource
        '
        Me.LblMovingSource.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblMovingSource.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMovingSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMovingSource.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMovingSource.Location = New System.Drawing.Point(3, 2)
        Me.LblMovingSource.Name = "LblMovingSource"
        Me.LblMovingSource.Size = New System.Drawing.Size(386, 23)
        Me.LblMovingSource.TabIndex = 0
        Me.LblMovingSource.Text = "移動元"
        Me.LblMovingSource.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblMovingDestination
        '
        Me.LblMovingDestination.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblMovingDestination.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMovingDestination.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMovingDestination.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMovingDestination.Location = New System.Drawing.Point(415, 2)
        Me.LblMovingDestination.Name = "LblMovingDestination"
        Me.LblMovingDestination.Size = New System.Drawing.Size(386, 23)
        Me.LblMovingDestination.TabIndex = 1
        Me.LblMovingDestination.Text = "移動先"
        Me.LblMovingDestination.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.AutoSize = True
        Me.TableLayoutPanel5.ColumnCount = 3
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.LblMovingDestination, 2, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.LblMovingSource, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.TableLayoutPanel6, 0, 1)
        Me.TableLayoutPanel5.Controls.Add(Me.TableLayoutPanel4, 2, 1)
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(9, 193)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 2
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 239.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(804, 266)
        Me.TableLayoutPanel5.TabIndex = 4
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel6.AutoSize = True
        Me.TableLayoutPanel6.ColumnCount = 2
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.TxtOrderNoSince, 1, 4)
        Me.TableLayoutPanel6.Controls.Add(Me.TxtSerialNoSince, 1, 3)
        Me.TableLayoutPanel6.Controls.Add(Me.TxtLocationSince, 1, 2)
        Me.TableLayoutPanel6.Controls.Add(Me.LblOrderNoSince, 0, 4)
        Me.TableLayoutPanel6.Controls.Add(Me.LblSerialNoSince, 0, 3)
        Me.TableLayoutPanel6.Controls.Add(Me.LblLocationSince, 0, 2)
        Me.TableLayoutPanel6.Controls.Add(Me.LblWarehouseSince, 0, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.TxtGoodsReceiptDate, 1, 7)
        Me.TableLayoutPanel6.Controls.Add(Me.TxtWarehouseSince, 1, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.TxtStorageTypeSince, 1, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.LblGoodsReceiptDate, 0, 7)
        Me.TableLayoutPanel6.Controls.Add(Me.LblUnitPrice, 0, 6)
        Me.TableLayoutPanel6.Controls.Add(Me.TxtUnitPrice, 1, 6)
        Me.TableLayoutPanel6.Controls.Add(Me.TxtQuantityFrom, 1, 5)
        Me.TableLayoutPanel6.Controls.Add(Me.LblQuantityFrom, 0, 5)
        Me.TableLayoutPanel6.Controls.Add(Me.LblStorageTypeSince, 0, 1)
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(3, 30)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 8
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.49968!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.49967!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.49967!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.49842!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.49842!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.49917!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.50167!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.50328!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(386, 224)
        Me.TableLayoutPanel6.TabIndex = 76
        '
        'LblWarehouseSince
        '
        Me.LblWarehouseSince.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblWarehouseSince.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblWarehouseSince.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblWarehouseSince.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblWarehouseSince.Location = New System.Drawing.Point(3, 2)
        Me.LblWarehouseSince.Name = "LblWarehouseSince"
        Me.LblWarehouseSince.Size = New System.Drawing.Size(187, 23)
        Me.LblWarehouseSince.TabIndex = 0
        Me.LblWarehouseSince.Text = "倉庫"
        Me.LblWarehouseSince.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtQuantityFrom
        '
        Me.TxtQuantityFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtQuantityFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtQuantityFrom.Enabled = False
        Me.TxtQuantityFrom.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtQuantityFrom.Location = New System.Drawing.Point(196, 138)
        Me.TxtQuantityFrom.Name = "TxtQuantityFrom"
        Me.TxtQuantityFrom.Size = New System.Drawing.Size(187, 22)
        Me.TxtQuantityFrom.TabIndex = 11
        Me.TxtQuantityFrom.TabStop = False
        Me.TxtQuantityFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtWarehouseSince
        '
        Me.TxtWarehouseSince.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtWarehouseSince.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtWarehouseSince.Enabled = False
        Me.TxtWarehouseSince.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtWarehouseSince.Location = New System.Drawing.Point(196, 3)
        Me.TxtWarehouseSince.Name = "TxtWarehouseSince"
        Me.TxtWarehouseSince.Size = New System.Drawing.Size(187, 22)
        Me.TxtWarehouseSince.TabIndex = 1
        Me.TxtWarehouseSince.TabStop = False
        '
        'TxtStorageTypeSince
        '
        Me.TxtStorageTypeSince.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtStorageTypeSince.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtStorageTypeSince.Enabled = False
        Me.TxtStorageTypeSince.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtStorageTypeSince.Location = New System.Drawing.Point(196, 30)
        Me.TxtStorageTypeSince.Name = "TxtStorageTypeSince"
        Me.TxtStorageTypeSince.Size = New System.Drawing.Size(187, 22)
        Me.TxtStorageTypeSince.TabIndex = 3
        Me.TxtStorageTypeSince.TabStop = False
        '
        'TxtUnitPrice
        '
        Me.TxtUnitPrice.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtUnitPrice.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtUnitPrice.Enabled = False
        Me.TxtUnitPrice.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtUnitPrice.Location = New System.Drawing.Point(196, 165)
        Me.TxtUnitPrice.Name = "TxtUnitPrice"
        Me.TxtUnitPrice.Size = New System.Drawing.Size(187, 22)
        Me.TxtUnitPrice.TabIndex = 13
        Me.TxtUnitPrice.TabStop = False
        Me.TxtUnitPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtGoodsReceiptDate
        '
        Me.TxtGoodsReceiptDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtGoodsReceiptDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtGoodsReceiptDate.Enabled = False
        Me.TxtGoodsReceiptDate.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtGoodsReceiptDate.Location = New System.Drawing.Point(196, 196)
        Me.TxtGoodsReceiptDate.Name = "TxtGoodsReceiptDate"
        Me.TxtGoodsReceiptDate.Size = New System.Drawing.Size(187, 22)
        Me.TxtGoodsReceiptDate.TabIndex = 15
        Me.TxtGoodsReceiptDate.TabStop = False
        '
        'LblStorageTypeSince
        '
        Me.LblStorageTypeSince.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblStorageTypeSince.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblStorageTypeSince.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblStorageTypeSince.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblStorageTypeSince.Location = New System.Drawing.Point(3, 29)
        Me.LblStorageTypeSince.Name = "LblStorageTypeSince"
        Me.LblStorageTypeSince.Size = New System.Drawing.Size(187, 23)
        Me.LblStorageTypeSince.TabIndex = 2
        Me.LblStorageTypeSince.Text = "入出庫種別"
        Me.LblStorageTypeSince.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblQuantityFrom
        '
        Me.LblQuantityFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblQuantityFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblQuantityFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblQuantityFrom.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblQuantityFrom.Location = New System.Drawing.Point(3, 137)
        Me.LblQuantityFrom.Name = "LblQuantityFrom"
        Me.LblQuantityFrom.Size = New System.Drawing.Size(187, 23)
        Me.LblQuantityFrom.TabIndex = 10
        Me.LblQuantityFrom.Text = "数量"
        Me.LblQuantityFrom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblUnitPrice
        '
        Me.LblUnitPrice.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblUnitPrice.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblUnitPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblUnitPrice.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblUnitPrice.Location = New System.Drawing.Point(3, 164)
        Me.LblUnitPrice.Name = "LblUnitPrice"
        Me.LblUnitPrice.Size = New System.Drawing.Size(187, 23)
        Me.LblUnitPrice.TabIndex = 12
        Me.LblUnitPrice.Text = "単価"
        Me.LblUnitPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblGoodsReceiptDate
        '
        Me.LblGoodsReceiptDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblGoodsReceiptDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblGoodsReceiptDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblGoodsReceiptDate.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblGoodsReceiptDate.Location = New System.Drawing.Point(3, 195)
        Me.LblGoodsReceiptDate.Name = "LblGoodsReceiptDate"
        Me.LblGoodsReceiptDate.Size = New System.Drawing.Size(187, 23)
        Me.LblGoodsReceiptDate.TabIndex = 14
        Me.LblGoodsReceiptDate.Text = "入庫日"
        Me.LblGoodsReceiptDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel4.AutoSize = True
        Me.TableLayoutPanel4.ColumnCount = 2
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel4.Controls.Add(Me.TxtOrderNoTo, 1, 4)
        Me.TableLayoutPanel4.Controls.Add(Me.TxtSerialNoTo, 1, 3)
        Me.TableLayoutPanel4.Controls.Add(Me.TxtUnitPriceTo, 1, 6)
        Me.TableLayoutPanel4.Controls.Add(Me.LblGoodsReceiptDateTo, 0, 7)
        Me.TableLayoutPanel4.Controls.Add(Me.CmWarehouseTo, 1, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.LblWarehouseTo, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.CmStorageTypeTo, 1, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.LblStorageTypeTo, 0, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.LblLocationTo, 0, 2)
        Me.TableLayoutPanel4.Controls.Add(Me.LblSerialNoTo, 0, 3)
        Me.TableLayoutPanel4.Controls.Add(Me.LblQuantityTo, 0, 5)
        Me.TableLayoutPanel4.Controls.Add(Me.LblUnitPriceTo, 0, 6)
        Me.TableLayoutPanel4.Controls.Add(Me.LblOrderNoTo, 0, 4)
        Me.TableLayoutPanel4.Controls.Add(Me.TxtQuantityTo, 1, 5)
        Me.TableLayoutPanel4.Controls.Add(Me.Label2, 0, 8)
        Me.TableLayoutPanel4.Controls.Add(Me.TxtGoodsReceiptDateTo, 1, 7)
        Me.TableLayoutPanel4.Controls.Add(Me.TxtLocationTo, 1, 2)
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(415, 30)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 8
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.85294!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.58824!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.85294!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.58824!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.11765!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(386, 233)
        Me.TableLayoutPanel4.TabIndex = 77
        '
        'LblWarehouseTo
        '
        Me.LblWarehouseTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblWarehouseTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblWarehouseTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblWarehouseTo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblWarehouseTo.Location = New System.Drawing.Point(3, 2)
        Me.LblWarehouseTo.Name = "LblWarehouseTo"
        Me.LblWarehouseTo.Size = New System.Drawing.Size(187, 22)
        Me.LblWarehouseTo.TabIndex = 0
        Me.LblWarehouseTo.Text = "倉庫"
        Me.LblWarehouseTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblStorageTypeTo
        '
        Me.LblStorageTypeTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblStorageTypeTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblStorageTypeTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblStorageTypeTo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblStorageTypeTo.Location = New System.Drawing.Point(3, 29)
        Me.LblStorageTypeTo.Name = "LblStorageTypeTo"
        Me.LblStorageTypeTo.Size = New System.Drawing.Size(187, 23)
        Me.LblStorageTypeTo.TabIndex = 2
        Me.LblStorageTypeTo.Text = "入出庫種別"
        Me.LblStorageTypeTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblLocationTo
        '
        Me.LblLocationTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblLocationTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblLocationTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblLocationTo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblLocationTo.Location = New System.Drawing.Point(3, 57)
        Me.LblLocationTo.Name = "LblLocationTo"
        Me.LblLocationTo.Size = New System.Drawing.Size(187, 23)
        Me.LblLocationTo.TabIndex = 4
        Me.LblLocationTo.Text = "ロケ番号"
        Me.LblLocationTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblSerialNoTo
        '
        Me.LblSerialNoTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblSerialNoTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblSerialNoTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblSerialNoTo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSerialNoTo.Location = New System.Drawing.Point(3, 84)
        Me.LblSerialNoTo.Name = "LblSerialNoTo"
        Me.LblSerialNoTo.Size = New System.Drawing.Size(187, 23)
        Me.LblSerialNoTo.TabIndex = 6
        Me.LblSerialNoTo.Text = "製造番号"
        Me.LblSerialNoTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblQuantityTo
        '
        Me.LblQuantityTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblQuantityTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblQuantityTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblQuantityTo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblQuantityTo.Location = New System.Drawing.Point(3, 139)
        Me.LblQuantityTo.Name = "LblQuantityTo"
        Me.LblQuantityTo.Size = New System.Drawing.Size(187, 23)
        Me.LblQuantityTo.TabIndex = 10
        Me.LblQuantityTo.Text = "数量"
        Me.LblQuantityTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblUnitPriceTo
        '
        Me.LblUnitPriceTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblUnitPriceTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblUnitPriceTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblUnitPriceTo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblUnitPriceTo.Location = New System.Drawing.Point(3, 169)
        Me.LblUnitPriceTo.Name = "LblUnitPriceTo"
        Me.LblUnitPriceTo.Size = New System.Drawing.Size(187, 23)
        Me.LblUnitPriceTo.TabIndex = 12
        Me.LblUnitPriceTo.Text = "単価"
        Me.LblUnitPriceTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblOrderNoTo
        '
        Me.LblOrderNoTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblOrderNoTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblOrderNoTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblOrderNoTo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblOrderNoTo.Location = New System.Drawing.Point(3, 111)
        Me.LblOrderNoTo.Name = "LblOrderNoTo"
        Me.LblOrderNoTo.Size = New System.Drawing.Size(187, 23)
        Me.LblOrderNoTo.TabIndex = 8
        Me.LblOrderNoTo.Text = "伝票番号"
        Me.LblOrderNoTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblLocationSince
        '
        Me.LblLocationSince.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblLocationSince.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblLocationSince.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblLocationSince.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblLocationSince.Location = New System.Drawing.Point(3, 56)
        Me.LblLocationSince.Name = "LblLocationSince"
        Me.LblLocationSince.Size = New System.Drawing.Size(187, 23)
        Me.LblLocationSince.TabIndex = 4
        Me.LblLocationSince.Text = "ロケ番号"
        Me.LblLocationSince.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblSerialNoSince
        '
        Me.LblSerialNoSince.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblSerialNoSince.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblSerialNoSince.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblSerialNoSince.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSerialNoSince.Location = New System.Drawing.Point(3, 83)
        Me.LblSerialNoSince.Name = "LblSerialNoSince"
        Me.LblSerialNoSince.Size = New System.Drawing.Size(187, 23)
        Me.LblSerialNoSince.TabIndex = 6
        Me.LblSerialNoSince.Text = "製造番号"
        Me.LblSerialNoSince.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblOrderNoSince
        '
        Me.LblOrderNoSince.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblOrderNoSince.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblOrderNoSince.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblOrderNoSince.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblOrderNoSince.Location = New System.Drawing.Point(3, 110)
        Me.LblOrderNoSince.Name = "LblOrderNoSince"
        Me.LblOrderNoSince.Size = New System.Drawing.Size(187, 23)
        Me.LblOrderNoSince.TabIndex = 8
        Me.LblOrderNoSince.Text = "伝票番号"
        Me.LblOrderNoSince.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtLocationSince
        '
        Me.TxtLocationSince.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtLocationSince.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtLocationSince.Enabled = False
        Me.TxtLocationSince.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtLocationSince.Location = New System.Drawing.Point(196, 57)
        Me.TxtLocationSince.Name = "TxtLocationSince"
        Me.TxtLocationSince.Size = New System.Drawing.Size(187, 22)
        Me.TxtLocationSince.TabIndex = 5
        Me.TxtLocationSince.TabStop = False
        '
        'TxtSerialNoSince
        '
        Me.TxtSerialNoSince.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtSerialNoSince.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtSerialNoSince.Enabled = False
        Me.TxtSerialNoSince.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSerialNoSince.Location = New System.Drawing.Point(196, 84)
        Me.TxtSerialNoSince.Name = "TxtSerialNoSince"
        Me.TxtSerialNoSince.Size = New System.Drawing.Size(187, 22)
        Me.TxtSerialNoSince.TabIndex = 7
        Me.TxtSerialNoSince.TabStop = False
        '
        'TxtOrderNoSince
        '
        Me.TxtOrderNoSince.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtOrderNoSince.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtOrderNoSince.Enabled = False
        Me.TxtOrderNoSince.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtOrderNoSince.Location = New System.Drawing.Point(196, 111)
        Me.TxtOrderNoSince.Name = "TxtOrderNoSince"
        Me.TxtOrderNoSince.Size = New System.Drawing.Size(187, 22)
        Me.TxtOrderNoSince.TabIndex = 9
        Me.TxtOrderNoSince.TabStop = False
        '
        'TxtQuantityTo
        '
        Me.TxtQuantityTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtQuantityTo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtQuantityTo.Location = New System.Drawing.Point(193, 140)
        Me.TxtQuantityTo.Margin = New System.Windows.Forms.Padding(0)
        Me.TxtQuantityTo.MaxLength = 8
        Me.TxtQuantityTo.Name = "TxtQuantityTo"
        Me.TxtQuantityTo.Size = New System.Drawing.Size(193, 22)
        Me.TxtQuantityTo.TabIndex = 11
        Me.TxtQuantityTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CmStorageTypeTo
        '
        Me.CmStorageTypeTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CmStorageTypeTo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmStorageTypeTo.FormattingEnabled = True
        Me.CmStorageTypeTo.Items.AddRange(New Object() {"RegularShip", "DHL", "Air", "Ship"})
        Me.CmStorageTypeTo.Location = New System.Drawing.Point(196, 30)
        Me.CmStorageTypeTo.Name = "CmStorageTypeTo"
        Me.CmStorageTypeTo.Size = New System.Drawing.Size(187, 23)
        Me.CmStorageTypeTo.TabIndex = 3
        '
        'CmWarehouseTo
        '
        Me.CmWarehouseTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CmWarehouseTo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmWarehouseTo.FormattingEnabled = True
        Me.CmWarehouseTo.Items.AddRange(New Object() {"RegularShip", "DHL", "Air", "Ship"})
        Me.CmWarehouseTo.Location = New System.Drawing.Point(196, 3)
        Me.CmWarehouseTo.Name = "CmWarehouseTo"
        Me.CmWarehouseTo.Size = New System.Drawing.Size(187, 23)
        Me.CmWarehouseTo.TabIndex = 1
        '
        'TxtUnitPriceTo
        '
        Me.TxtUnitPriceTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtUnitPriceTo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtUnitPriceTo.Location = New System.Drawing.Point(193, 169)
        Me.TxtUnitPriceTo.Margin = New System.Windows.Forms.Padding(0)
        Me.TxtUnitPriceTo.MaxLength = 8
        Me.TxtUnitPriceTo.Name = "TxtUnitPriceTo"
        Me.TxtUnitPriceTo.Size = New System.Drawing.Size(193, 22)
        Me.TxtUnitPriceTo.TabIndex = 13
        Me.TxtUnitPriceTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 228)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(187, 0)
        Me.Label2.TabIndex = 333
        Me.Label2.Text = "数量"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblGoodsReceiptDateTo
        '
        Me.LblGoodsReceiptDateTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblGoodsReceiptDateTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblGoodsReceiptDateTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblGoodsReceiptDateTo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblGoodsReceiptDateTo.Location = New System.Drawing.Point(3, 197)
        Me.LblGoodsReceiptDateTo.Name = "LblGoodsReceiptDateTo"
        Me.LblGoodsReceiptDateTo.Size = New System.Drawing.Size(187, 23)
        Me.LblGoodsReceiptDateTo.TabIndex = 14
        Me.LblGoodsReceiptDateTo.Text = "移動日"
        Me.LblGoodsReceiptDateTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtGoodsReceiptDateTo
        '
        Me.TxtGoodsReceiptDateTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtGoodsReceiptDateTo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtGoodsReceiptDateTo.Location = New System.Drawing.Point(193, 198)
        Me.TxtGoodsReceiptDateTo.Margin = New System.Windows.Forms.Padding(0)
        Me.TxtGoodsReceiptDateTo.MaxLength = 8
        Me.TxtGoodsReceiptDateTo.Name = "TxtGoodsReceiptDateTo"
        Me.TxtGoodsReceiptDateTo.Size = New System.Drawing.Size(193, 22)
        Me.TxtGoodsReceiptDateTo.TabIndex = 15
        Me.TxtGoodsReceiptDateTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtSerialNoTo
        '
        Me.TxtSerialNoTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtSerialNoTo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSerialNoTo.Location = New System.Drawing.Point(193, 85)
        Me.TxtSerialNoTo.Margin = New System.Windows.Forms.Padding(0)
        Me.TxtSerialNoTo.MaxLength = 8
        Me.TxtSerialNoTo.Name = "TxtSerialNoTo"
        Me.TxtSerialNoTo.Size = New System.Drawing.Size(193, 22)
        Me.TxtSerialNoTo.TabIndex = 7
        Me.TxtSerialNoTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtOrderNoTo
        '
        Me.TxtOrderNoTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtOrderNoTo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtOrderNoTo.Location = New System.Drawing.Point(193, 112)
        Me.TxtOrderNoTo.Margin = New System.Windows.Forms.Padding(0)
        Me.TxtOrderNoTo.MaxLength = 8
        Me.TxtOrderNoTo.Name = "TxtOrderNoTo"
        Me.TxtOrderNoTo.Size = New System.Drawing.Size(193, 22)
        Me.TxtOrderNoTo.TabIndex = 9
        Me.TxtOrderNoTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtLocationTo
        '
        Me.TxtLocationTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtLocationTo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtLocationTo.Location = New System.Drawing.Point(193, 57)
        Me.TxtLocationTo.Margin = New System.Windows.Forms.Padding(0)
        Me.TxtLocationTo.MaxLength = 8
        Me.TxtLocationTo.Name = "TxtLocationTo"
        Me.TxtLocationTo.Size = New System.Drawing.Size(193, 22)
        Me.TxtLocationTo.TabIndex = 5
        Me.TxtLocationTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'MovementInput
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(836, 518)
        Me.Controls.Add(Me.TableLayoutPanel5)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.TableLayoutPanel3)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.BtnRegistration)
        Me.Controls.Add(Me.LblMode)
        Me.Controls.Add(Me.BtnBack)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "MovementInput"
        Me.Text = "MovementInput"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel6.PerformLayout()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnBack As Button
    Friend WithEvents LblMode As Label
    Friend WithEvents VAT As DataGridViewTextBoxColumn
    Friend WithEvents BtnRegistration As Button
    Friend WithEvents LblProcessingClassification As Label
    Friend WithEvents LblProcessingDate As Label
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents CmProcessingClassification As ComboBox
    Friend WithEvents DtpProcessingDate As DateTimePicker
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents LblSpec As Label
    Friend WithEvents LblManufacturer As Label
    Friend WithEvents LblItemName As Label
    Friend WithEvents TxtItemName As TextBox
    Friend WithEvents TxtSpec As TextBox
    Friend WithEvents BtnSearch As Button
    Friend WithEvents LblMovingSource As Label
    Friend WithEvents LblMovingDestination As Label
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel6 As TableLayoutPanel
    Friend WithEvents LblWarehouseSince As Label
    Friend WithEvents TxtQuantityFrom As TextBox
    Friend WithEvents TxtWarehouseSince As TextBox
    Friend WithEvents TxtStorageTypeSince As TextBox
    Friend WithEvents TxtUnitPrice As TextBox
    Friend WithEvents TxtGoodsReceiptDate As TextBox
    Friend WithEvents LblStorageTypeSince As Label
    Friend WithEvents LblQuantityFrom As Label
    Friend WithEvents LblUnitPrice As Label
    Friend WithEvents LblGoodsReceiptDate As Label
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents LblWarehouseTo As Label
    Friend WithEvents LblStorageTypeTo As Label
    Friend WithEvents LblLocationTo As Label
    Friend WithEvents TxtManufacturer As TextBox
    Friend WithEvents LblSerialNoTo As Label
    Friend WithEvents LblQuantityTo As Label
    Friend WithEvents LblUnitPriceTo As Label
    Friend WithEvents LblOrderNoTo As Label
    Friend WithEvents TxtOrderNoSince As TextBox
    Friend WithEvents TxtSerialNoSince As TextBox
    Friend WithEvents TxtLocationSince As TextBox
    Friend WithEvents LblOrderNoSince As Label
    Friend WithEvents LblSerialNoSince As Label
    Friend WithEvents LblLocationSince As Label
    Friend WithEvents TxtGoodsReceiptDateTo As TextBox
    Friend WithEvents TxtUnitPriceTo As TextBox
    Friend WithEvents LblGoodsReceiptDateTo As Label
    Friend WithEvents CmWarehouseTo As ComboBox
    Friend WithEvents CmStorageTypeTo As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtQuantityTo As TextBox
    Friend WithEvents TxtOrderNoTo As TextBox
    Friend WithEvents TxtSerialNoTo As TextBox
    Friend WithEvents TxtLocationTo As TextBox
End Class
