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
        Me.components = New System.ComponentModel.Container()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.DgvList = New System.Windows.Forms.DataGridView()
        Me.LblMode = New System.Windows.Forms.Label()
        Me.BtnExcelOutput = New System.Windows.Forms.Button()
        Me.cmd検索 = New System.Windows.Forms.Button()
        Me.TxtSpec = New System.Windows.Forms.TextBox()
        Me.LblItemName = New System.Windows.Forms.Label()
        Me.cmWarehouseFrom = New System.Windows.Forms.ComboBox()
        Me.lblWarehouse = New System.Windows.Forms.Label()
        Me.LblMovingDay = New System.Windows.Forms.Label()
        Me.lblMaker = New System.Windows.Forms.Label()
        Me.txtMaker = New System.Windows.Forms.TextBox()
        Me.TxtItemName = New System.Windows.Forms.TextBox()
        Me.LblSpec = New System.Windows.Forms.Label()
        Me.txtYear = New System.Windows.Forms.TextBox()
        Me.lblYear = New System.Windows.Forms.Label()
        Me.lblMonth = New System.Windows.Forms.Label()
        Me.txtMonth = New System.Windows.Forms.TextBox()
        Me.lblSyubetsu = New System.Windows.Forms.Label()
        Me.lblLocation = New System.Windows.Forms.Label()
        Me.lblSerialNo = New System.Windows.Forms.Label()
        Me.lblOrderNo = New System.Windows.Forms.Label()
        Me.cmSyubetsuFrom = New System.Windows.Forms.ComboBox()
        Me.cmLocationFrom = New System.Windows.Forms.ComboBox()
        Me.cmSerialNoFrom = New System.Windows.Forms.ComboBox()
        Me.cmOrderNoFrom = New System.Windows.Forms.ComboBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.DtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.DtpTo = New System.Windows.Forms.DateTimePicker()
        Me.ChkBB = New System.Windows.Forms.CheckBox()
        CType(Me.DgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(1174, 509)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 24
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'DgvList
        '
        Me.DgvList.AllowUserToAddRows = False
        Me.DgvList.AllowUserToDeleteRows = False
        Me.DgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvList.Location = New System.Drawing.Point(13, 115)
        Me.DgvList.Name = "DgvList"
        Me.DgvList.ReadOnly = True
        Me.DgvList.RowHeadersVisible = False
        Me.DgvList.RowTemplate.Height = 21
        Me.DgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvList.Size = New System.Drawing.Size(1326, 380)
        Me.DgvList.TabIndex = 0
        '
        'LblMode
        '
        Me.LblMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMode.Font = New System.Drawing.Font("MS Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
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
        Me.BtnExcelOutput.TabIndex = 23
        Me.BtnExcelOutput.Text = "Excel出力"
        Me.BtnExcelOutput.UseVisualStyleBackColor = True
        '
        'cmd検索
        '
        Me.cmd検索.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmd検索.Location = New System.Drawing.Point(935, 7)
        Me.cmd検索.Name = "cmd検索"
        Me.cmd検索.Size = New System.Drawing.Size(165, 40)
        Me.cmd検索.TabIndex = 22
        Me.cmd検索.Text = "検索"
        Me.cmd検索.UseVisualStyleBackColor = True
        '
        'TxtSpec
        '
        Me.TxtSpec.CausesValidation = False
        Me.TxtSpec.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSpec.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TxtSpec.Location = New System.Drawing.Point(189, 85)
        Me.TxtSpec.Name = "TxtSpec"
        Me.TxtSpec.Size = New System.Drawing.Size(218, 22)
        Me.TxtSpec.TabIndex = 11
        '
        'LblItemName
        '
        Me.LblItemName.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblItemName.Font = New System.Drawing.Font("MS Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblItemName.Location = New System.Drawing.Point(13, 59)
        Me.LblItemName.Name = "LblItemName"
        Me.LblItemName.Size = New System.Drawing.Size(170, 22)
        Me.LblItemName.TabIndex = 8
        Me.LblItemName.Text = "品名"
        Me.LblItemName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmWarehouseFrom
        '
        Me.cmWarehouseFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmWarehouseFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmWarehouseFrom.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmWarehouseFrom.FormattingEnabled = True
        Me.cmWarehouseFrom.Items.AddRange(New Object() {"RegularShip", "DHL", "Air", "Ship"})
        Me.cmWarehouseFrom.Location = New System.Drawing.Point(606, 7)
        Me.cmWarehouseFrom.Name = "cmWarehouseFrom"
        Me.cmWarehouseFrom.Size = New System.Drawing.Size(239, 23)
        Me.cmWarehouseFrom.TabIndex = 13
        '
        'lblWarehouse
        '
        Me.lblWarehouse.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblWarehouse.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblWarehouse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblWarehouse.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblWarehouse.Location = New System.Drawing.Point(430, 7)
        Me.lblWarehouse.Name = "lblWarehouse"
        Me.lblWarehouse.Size = New System.Drawing.Size(170, 22)
        Me.lblWarehouse.TabIndex = 12
        Me.lblWarehouse.Text = "倉　　　庫"
        Me.lblWarehouse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblMovingDay
        '
        Me.LblMovingDay.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblMovingDay.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMovingDay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMovingDay.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMovingDay.Location = New System.Drawing.Point(13, 7)
        Me.LblMovingDay.Name = "LblMovingDay"
        Me.LblMovingDay.Size = New System.Drawing.Size(170, 22)
        Me.LblMovingDay.TabIndex = 1
        Me.LblMovingDay.Text = "対象年月"
        Me.LblMovingDay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMaker
        '
        Me.lblMaker.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblMaker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMaker.Font = New System.Drawing.Font("MS Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMaker.Location = New System.Drawing.Point(13, 33)
        Me.lblMaker.Name = "lblMaker"
        Me.lblMaker.Size = New System.Drawing.Size(170, 22)
        Me.lblMaker.TabIndex = 6
        Me.lblMaker.Text = "メーカー"
        Me.lblMaker.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtMaker
        '
        Me.txtMaker.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtMaker.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtMaker.Location = New System.Drawing.Point(189, 33)
        Me.txtMaker.Name = "txtMaker"
        Me.txtMaker.Size = New System.Drawing.Size(218, 22)
        Me.txtMaker.TabIndex = 7
        '
        'TxtItemName
        '
        Me.TxtItemName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtItemName.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TxtItemName.Location = New System.Drawing.Point(189, 59)
        Me.TxtItemName.Name = "TxtItemName"
        Me.TxtItemName.Size = New System.Drawing.Size(218, 22)
        Me.TxtItemName.TabIndex = 9
        '
        'LblSpec
        '
        Me.LblSpec.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblSpec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblSpec.Font = New System.Drawing.Font("MS Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSpec.Location = New System.Drawing.Point(13, 85)
        Me.LblSpec.Name = "LblSpec"
        Me.LblSpec.Size = New System.Drawing.Size(170, 22)
        Me.LblSpec.TabIndex = 10
        Me.LblSpec.Text = "型式"
        Me.LblSpec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtYear
        '
        Me.txtYear.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtYear.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtYear.Location = New System.Drawing.Point(924, 60)
        Me.txtYear.Name = "txtYear"
        Me.txtYear.Size = New System.Drawing.Size(52, 22)
        Me.txtYear.TabIndex = 3
        Me.txtYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtYear.Visible = False
        '
        'lblYear
        '
        Me.lblYear.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblYear.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblYear.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblYear.Location = New System.Drawing.Point(860, 60)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.Size = New System.Drawing.Size(58, 22)
        Me.lblYear.TabIndex = 2
        Me.lblYear.Text = "年"
        Me.lblYear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblYear.Visible = False
        '
        'lblMonth
        '
        Me.lblMonth.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMonth.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMonth.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMonth.Location = New System.Drawing.Point(982, 60)
        Me.lblMonth.Name = "lblMonth"
        Me.lblMonth.Size = New System.Drawing.Size(58, 22)
        Me.lblMonth.TabIndex = 4
        Me.lblMonth.Text = "月"
        Me.lblMonth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblMonth.Visible = False
        '
        'txtMonth
        '
        Me.txtMonth.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtMonth.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtMonth.Location = New System.Drawing.Point(1046, 60)
        Me.txtMonth.Name = "txtMonth"
        Me.txtMonth.Size = New System.Drawing.Size(32, 22)
        Me.txtMonth.TabIndex = 5
        Me.txtMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMonth.Visible = False
        '
        'lblSyubetsu
        '
        Me.lblSyubetsu.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSyubetsu.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblSyubetsu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSyubetsu.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSyubetsu.Location = New System.Drawing.Point(430, 33)
        Me.lblSyubetsu.Name = "lblSyubetsu"
        Me.lblSyubetsu.Size = New System.Drawing.Size(170, 22)
        Me.lblSyubetsu.TabIndex = 14
        Me.lblSyubetsu.Text = "入出庫種別"
        Me.lblSyubetsu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLocation
        '
        Me.lblLocation.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLocation.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLocation.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblLocation.Location = New System.Drawing.Point(430, 59)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(170, 22)
        Me.lblLocation.TabIndex = 16
        Me.lblLocation.Text = "ロケーション"
        Me.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSerialNo
        '
        Me.lblSerialNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSerialNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblSerialNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSerialNo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSerialNo.Location = New System.Drawing.Point(430, 85)
        Me.lblSerialNo.Name = "lblSerialNo"
        Me.lblSerialNo.Size = New System.Drawing.Size(170, 22)
        Me.lblSerialNo.TabIndex = 18
        Me.lblSerialNo.Text = "製造番号"
        Me.lblSerialNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblOrderNo
        '
        Me.lblOrderNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblOrderNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblOrderNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblOrderNo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblOrderNo.Location = New System.Drawing.Point(860, 86)
        Me.lblOrderNo.Name = "lblOrderNo"
        Me.lblOrderNo.Size = New System.Drawing.Size(170, 22)
        Me.lblOrderNo.TabIndex = 20
        Me.lblOrderNo.Text = "伝票番号"
        Me.lblOrderNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmSyubetsuFrom
        '
        Me.cmSyubetsuFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmSyubetsuFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmSyubetsuFrom.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmSyubetsuFrom.FormattingEnabled = True
        Me.cmSyubetsuFrom.Items.AddRange(New Object() {"RegularShip", "DHL", "Air", "Ship"})
        Me.cmSyubetsuFrom.Location = New System.Drawing.Point(606, 33)
        Me.cmSyubetsuFrom.Name = "cmSyubetsuFrom"
        Me.cmSyubetsuFrom.Size = New System.Drawing.Size(239, 23)
        Me.cmSyubetsuFrom.TabIndex = 15
        '
        'cmLocationFrom
        '
        Me.cmLocationFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmLocationFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmLocationFrom.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmLocationFrom.FormattingEnabled = True
        Me.cmLocationFrom.Items.AddRange(New Object() {"RegularShip", "DHL", "Air", "Ship"})
        Me.cmLocationFrom.Location = New System.Drawing.Point(606, 59)
        Me.cmLocationFrom.Name = "cmLocationFrom"
        Me.cmLocationFrom.Size = New System.Drawing.Size(239, 23)
        Me.cmLocationFrom.TabIndex = 17
        '
        'cmSerialNoFrom
        '
        Me.cmSerialNoFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmSerialNoFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmSerialNoFrom.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmSerialNoFrom.FormattingEnabled = True
        Me.cmSerialNoFrom.Items.AddRange(New Object() {"RegularShip", "DHL", "Air", "Ship"})
        Me.cmSerialNoFrom.Location = New System.Drawing.Point(606, 85)
        Me.cmSerialNoFrom.Name = "cmSerialNoFrom"
        Me.cmSerialNoFrom.Size = New System.Drawing.Size(239, 23)
        Me.cmSerialNoFrom.TabIndex = 19
        '
        'cmOrderNoFrom
        '
        Me.cmOrderNoFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmOrderNoFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmOrderNoFrom.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmOrderNoFrom.FormattingEnabled = True
        Me.cmOrderNoFrom.Items.AddRange(New Object() {"RegularShip", "DHL", "Air", "Ship"})
        Me.cmOrderNoFrom.Location = New System.Drawing.Point(1036, 86)
        Me.cmOrderNoFrom.Name = "cmOrderNoFrom"
        Me.cmOrderNoFrom.Size = New System.Drawing.Size(239, 23)
        Me.cmOrderNoFrom.TabIndex = 21
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(150, 26)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.ShortcutKeyDisplayString = ""
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(149, 22)
        Me.ToolStripMenuItem1.Text = "Order Detail ..."
        '
        'DtpFrom
        '
        Me.DtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DtpFrom.Location = New System.Drawing.Point(191, 9)
        Me.DtpFrom.Name = "DtpFrom"
        Me.DtpFrom.Size = New System.Drawing.Size(99, 20)
        Me.DtpFrom.TabIndex = 68
        '
        'DtpTo
        '
        Me.DtpTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DtpTo.Location = New System.Drawing.Point(296, 9)
        Me.DtpTo.Name = "DtpTo"
        Me.DtpTo.Size = New System.Drawing.Size(111, 20)
        Me.DtpTo.TabIndex = 69
        '
        'ChkBB
        '
        Me.ChkBB.AutoSize = True
        Me.ChkBB.Checked = True
        Me.ChkBB.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkBB.Location = New System.Drawing.Point(935, 59)
        Me.ChkBB.Name = "ChkBB"
        Me.ChkBB.Size = New System.Drawing.Size(152, 17)
        Me.ChkBB.TabIndex = 70
        Me.ChkBB.Text = "Display Begining Balance?"
        Me.ChkBB.UseVisualStyleBackColor = True
        '
        'InventoryControlTable
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.ChkBB)
        Me.Controls.Add(Me.DtpTo)
        Me.Controls.Add(Me.DtpFrom)
        Me.Controls.Add(Me.cmOrderNoFrom)
        Me.Controls.Add(Me.cmSerialNoFrom)
        Me.Controls.Add(Me.cmLocationFrom)
        Me.Controls.Add(Me.cmSyubetsuFrom)
        Me.Controls.Add(Me.lblOrderNo)
        Me.Controls.Add(Me.lblSerialNo)
        Me.Controls.Add(Me.lblLocation)
        Me.Controls.Add(Me.lblSyubetsu)
        Me.Controls.Add(Me.txtMonth)
        Me.Controls.Add(Me.lblMonth)
        Me.Controls.Add(Me.lblYear)
        Me.Controls.Add(Me.txtYear)
        Me.Controls.Add(Me.TxtSpec)
        Me.Controls.Add(Me.LblItemName)
        Me.Controls.Add(Me.cmWarehouseFrom)
        Me.Controls.Add(Me.lblWarehouse)
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
        Me.ContextMenuStrip1.ResumeLayout(False)
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
    Friend WithEvents cmWarehouseFrom As ComboBox
    Friend WithEvents lblWarehouse As Label
    Friend WithEvents LblMovingDay As Label
    Friend WithEvents lblMaker As Label
    Friend WithEvents txtMaker As TextBox
    Friend WithEvents TxtItemName As TextBox
    Friend WithEvents LblSpec As Label
    Friend WithEvents txtYear As TextBox
    Friend WithEvents lblYear As Label
    Friend WithEvents lblMonth As Label
    Friend WithEvents txtMonth As TextBox
    Friend WithEvents lblSyubetsu As Label
    Friend WithEvents lblLocation As Label
    Friend WithEvents lblSerialNo As Label
    Friend WithEvents lblOrderNo As Label
    Friend WithEvents cmSyubetsuFrom As ComboBox
    Friend WithEvents cmLocationFrom As ComboBox
    Friend WithEvents cmSerialNoFrom As ComboBox
    Friend WithEvents cmOrderNoFrom As ComboBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents DtpFrom As DateTimePicker
    Friend WithEvents DtpTo As DateTimePicker
    Friend WithEvents ChkBB As CheckBox
End Class
