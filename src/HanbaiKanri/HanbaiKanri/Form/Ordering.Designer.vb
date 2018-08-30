<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Ordering
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
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.LblPaymentTerms = New System.Windows.Forms.Label()
        Me.LblPosition = New System.Windows.Forms.Label()
        Me.TxtTel = New System.Windows.Forms.TextBox()
        Me.LblPerson = New System.Windows.Forms.Label()
        Me.TxtAddress1 = New System.Windows.Forms.TextBox()
        Me.TxtPerson = New System.Windows.Forms.TextBox()
        Me.LblFax = New System.Windows.Forms.Label()
        Me.TxtFax = New System.Windows.Forms.TextBox()
        Me.TxtPostalCode = New System.Windows.Forms.TextBox()
        Me.LblTel = New System.Windows.Forms.Label()
        Me.LblAddress = New System.Windows.Forms.Label()
        Me.TxtSupplierName = New System.Windows.Forms.TextBox()
        Me.TxtInput = New System.Windows.Forms.TextBox()
        Me.LblCustomerName = New System.Windows.Forms.Label()
        Me.TxtOrderingNo = New System.Windows.Forms.TextBox()
        Me.LblInput = New System.Windows.Forms.Label()
        Me.LblOrderDate = New System.Windows.Forms.Label()
        Me.LblOrderNo = New System.Windows.Forms.Label()
        Me.DgvItemList = New System.Windows.Forms.DataGridView()
        Me.No = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.メーカー = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.品名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.型式 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.数量 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.単位 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入先 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入単価 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.間接費 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入金額 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.リードタイム = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.入庫数 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.未入庫数 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.備考 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TxtPurchaseRemark = New System.Windows.Forms.TextBox()
        Me.DtpOrderingDate = New System.Windows.Forms.DateTimePicker()
        Me.TxtOrderingAmount = New System.Windows.Forms.TextBox()
        Me.LblOrderAmount = New System.Windows.Forms.Label()
        Me.TxtItemCount = New System.Windows.Forms.TextBox()
        Me.LblItemCount = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtSupplierCode = New System.Windows.Forms.TextBox()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.DtpOrderingRegistration = New System.Windows.Forms.DateTimePicker()
        Me.BtnRegistration = New System.Windows.Forms.Button()
        Me.TxtAddress3 = New System.Windows.Forms.TextBox()
        Me.TxtAddress2 = New System.Windows.Forms.TextBox()
        Me.LblRegistration = New System.Windows.Forms.Label()
        Me.TxtSales = New System.Windows.Forms.TextBox()
        Me.LblSales = New System.Windows.Forms.Label()
        Me.TxtOrderingSuffix = New System.Windows.Forms.TextBox()
        Me.TxtQuoteRemarks = New System.Windows.Forms.TextBox()
        Me.TxtPaymentTerms = New System.Windows.Forms.TextBox()
        Me.LblRemarks = New System.Windows.Forms.Label()
        Me.TxtPosition = New System.Windows.Forms.TextBox()
        Me.BtnPurchase = New System.Windows.Forms.Button()
        Me.BtnClone = New System.Windows.Forms.Button()
        Me.BtnDown = New System.Windows.Forms.Button()
        Me.BtnUp = New System.Windows.Forms.Button()
        Me.BtnInsert = New System.Windows.Forms.Button()
        Me.BtnRowsDel = New System.Windows.Forms.Button()
        Me.BtnRowsAdd = New System.Windows.Forms.Button()
        CType(Me.DgvItemList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LblPaymentTerms
        '
        Me.LblPaymentTerms.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblPaymentTerms.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblPaymentTerms.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblPaymentTerms.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPaymentTerms.Location = New System.Drawing.Point(11, 158)
        Me.LblPaymentTerms.Name = "LblPaymentTerms"
        Me.LblPaymentTerms.Size = New System.Drawing.Size(110, 23)
        Me.LblPaymentTerms.TabIndex = 190
        Me.LblPaymentTerms.Text = "支払条件"
        Me.LblPaymentTerms.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblPosition
        '
        Me.LblPosition.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblPosition.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblPosition.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPosition.Location = New System.Drawing.Point(609, 129)
        Me.LblPosition.Name = "LblPosition"
        Me.LblPosition.Size = New System.Drawing.Size(150, 23)
        Me.LblPosition.TabIndex = 201
        Me.LblPosition.Text = "仕入先担当者役職"
        Me.LblPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtTel
        '
        Me.TxtTel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtTel.BackColor = System.Drawing.Color.White
        Me.TxtTel.Enabled = False
        Me.TxtTel.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtTel.Location = New System.Drawing.Point(765, 42)
        Me.TxtTel.MaxLength = 15
        Me.TxtTel.Name = "TxtTel"
        Me.TxtTel.Size = New System.Drawing.Size(200, 23)
        Me.TxtTel.TabIndex = 209
        '
        'LblPerson
        '
        Me.LblPerson.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblPerson.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblPerson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblPerson.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPerson.Location = New System.Drawing.Point(609, 100)
        Me.LblPerson.Name = "LblPerson"
        Me.LblPerson.Size = New System.Drawing.Size(150, 23)
        Me.LblPerson.TabIndex = 197
        Me.LblPerson.Text = "仕入先担当者名"
        Me.LblPerson.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtAddress1
        '
        Me.TxtAddress1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtAddress1.BackColor = System.Drawing.Color.White
        Me.TxtAddress1.Enabled = False
        Me.TxtAddress1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAddress1.Location = New System.Drawing.Point(273, 71)
        Me.TxtAddress1.MaxLength = 100
        Me.TxtAddress1.Name = "TxtAddress1"
        Me.TxtAddress1.Size = New System.Drawing.Size(330, 23)
        Me.TxtAddress1.TabIndex = 202
        '
        'TxtPerson
        '
        Me.TxtPerson.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtPerson.BackColor = System.Drawing.Color.White
        Me.TxtPerson.Enabled = False
        Me.TxtPerson.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPerson.Location = New System.Drawing.Point(765, 100)
        Me.TxtPerson.MaxLength = 50
        Me.TxtPerson.Name = "TxtPerson"
        Me.TxtPerson.Size = New System.Drawing.Size(200, 23)
        Me.TxtPerson.TabIndex = 211
        '
        'LblFax
        '
        Me.LblFax.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblFax.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblFax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblFax.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblFax.Location = New System.Drawing.Point(609, 71)
        Me.LblFax.Name = "LblFax"
        Me.LblFax.Size = New System.Drawing.Size(150, 23)
        Me.LblFax.TabIndex = 192
        Me.LblFax.Text = "FAX番号"
        Me.LblFax.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtFax
        '
        Me.TxtFax.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtFax.BackColor = System.Drawing.Color.White
        Me.TxtFax.Enabled = False
        Me.TxtFax.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtFax.Location = New System.Drawing.Point(765, 71)
        Me.TxtFax.MaxLength = 15
        Me.TxtFax.Name = "TxtFax"
        Me.TxtFax.Size = New System.Drawing.Size(200, 23)
        Me.TxtFax.TabIndex = 210
        '
        'TxtPostalCode
        '
        Me.TxtPostalCode.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtPostalCode.BackColor = System.Drawing.Color.White
        Me.TxtPostalCode.Enabled = False
        Me.TxtPostalCode.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPostalCode.Location = New System.Drawing.Point(127, 71)
        Me.TxtPostalCode.MaxLength = 3
        Me.TxtPostalCode.Name = "TxtPostalCode"
        Me.TxtPostalCode.Size = New System.Drawing.Size(140, 23)
        Me.TxtPostalCode.TabIndex = 198
        Me.TxtPostalCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LblTel
        '
        Me.LblTel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblTel.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblTel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblTel.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTel.Location = New System.Drawing.Point(609, 42)
        Me.LblTel.Name = "LblTel"
        Me.LblTel.Size = New System.Drawing.Size(150, 23)
        Me.LblTel.TabIndex = 187
        Me.LblTel.Text = "電話番号"
        Me.LblTel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblAddress
        '
        Me.LblAddress.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblAddress.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblAddress.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAddress.Location = New System.Drawing.Point(11, 71)
        Me.LblAddress.Name = "LblAddress"
        Me.LblAddress.Size = New System.Drawing.Size(110, 81)
        Me.LblAddress.TabIndex = 203
        Me.LblAddress.Text = "住所"
        Me.LblAddress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtSupplierName
        '
        Me.TxtSupplierName.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtSupplierName.BackColor = System.Drawing.Color.White
        Me.TxtSupplierName.Enabled = False
        Me.TxtSupplierName.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSupplierName.Location = New System.Drawing.Point(273, 42)
        Me.TxtSupplierName.MaxLength = 50
        Me.TxtSupplierName.Name = "TxtSupplierName"
        Me.TxtSupplierName.Size = New System.Drawing.Size(330, 23)
        Me.TxtSupplierName.TabIndex = 195
        '
        'TxtInput
        '
        Me.TxtInput.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtInput.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtInput.Enabled = False
        Me.TxtInput.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtInput.Location = New System.Drawing.Point(1127, 71)
        Me.TxtInput.MaxLength = 20
        Me.TxtInput.Name = "TxtInput"
        Me.TxtInput.ReadOnly = True
        Me.TxtInput.Size = New System.Drawing.Size(200, 23)
        Me.TxtInput.TabIndex = 199
        Me.TxtInput.TabStop = False
        '
        'LblCustomerName
        '
        Me.LblCustomerName.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblCustomerName.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblCustomerName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCustomerName.Location = New System.Drawing.Point(11, 42)
        Me.LblCustomerName.Name = "LblCustomerName"
        Me.LblCustomerName.Size = New System.Drawing.Size(110, 23)
        Me.LblCustomerName.TabIndex = 188
        Me.LblCustomerName.Text = "仕入先名称"
        Me.LblCustomerName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtOrderingNo
        '
        Me.TxtOrderingNo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtOrderingNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtOrderingNo.Enabled = False
        Me.TxtOrderingNo.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtOrderingNo.Location = New System.Drawing.Point(127, 13)
        Me.TxtOrderingNo.MaxLength = 8
        Me.TxtOrderingNo.Name = "TxtOrderingNo"
        Me.TxtOrderingNo.ReadOnly = True
        Me.TxtOrderingNo.Size = New System.Drawing.Size(88, 23)
        Me.TxtOrderingNo.TabIndex = 194
        Me.TxtOrderingNo.TabStop = False
        '
        'LblInput
        '
        Me.LblInput.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblInput.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblInput.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblInput.Location = New System.Drawing.Point(971, 71)
        Me.LblInput.Name = "LblInput"
        Me.LblInput.Size = New System.Drawing.Size(150, 23)
        Me.LblInput.TabIndex = 207
        Me.LblInput.Text = "入力担当者"
        Me.LblInput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblOrderDate
        '
        Me.LblOrderDate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblOrderDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblOrderDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblOrderDate.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblOrderDate.Location = New System.Drawing.Point(273, 13)
        Me.LblOrderDate.Name = "LblOrderDate"
        Me.LblOrderDate.Size = New System.Drawing.Size(112, 23)
        Me.LblOrderDate.TabIndex = 206
        Me.LblOrderDate.Text = "発注日"
        Me.LblOrderDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblOrderNo
        '
        Me.LblOrderNo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblOrderNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblOrderNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblOrderNo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblOrderNo.Location = New System.Drawing.Point(11, 13)
        Me.LblOrderNo.Name = "LblOrderNo"
        Me.LblOrderNo.Size = New System.Drawing.Size(110, 23)
        Me.LblOrderNo.TabIndex = 191
        Me.LblOrderNo.Text = "発注番号"
        Me.LblOrderNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DgvItemList
        '
        Me.DgvItemList.AllowUserToAddRows = False
        Me.DgvItemList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DgvItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvItemList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.No, Me.メーカー, Me.品名, Me.型式, Me.数量, Me.単位, Me.仕入先, Me.仕入単価, Me.間接費, Me.仕入金額, Me.リードタイム, Me.入庫数, Me.未入庫数, Me.備考})
        Me.DgvItemList.Location = New System.Drawing.Point(11, 246)
        Me.DgvItemList.Name = "DgvItemList"
        Me.DgvItemList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DgvItemList.RowHeadersVisible = False
        Me.DgvItemList.RowTemplate.Height = 21
        Me.DgvItemList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvItemList.Size = New System.Drawing.Size(1329, 379)
        Me.DgvItemList.TabIndex = 216
        '
        'No
        '
        Me.No.HeaderText = "No"
        Me.No.Name = "No"
        Me.No.Width = 43
        '
        'メーカー
        '
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.メーカー.DefaultCellStyle = DataGridViewCellStyle7
        Me.メーカー.HeaderText = "メーカー"
        Me.メーカー.Name = "メーカー"
        Me.メーカー.Width = 220
        '
        '品名
        '
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.品名.DefaultCellStyle = DataGridViewCellStyle8
        Me.品名.HeaderText = "品名"
        Me.品名.Name = "品名"
        Me.品名.Width = 220
        '
        '型式
        '
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.型式.DefaultCellStyle = DataGridViewCellStyle9
        Me.型式.HeaderText = "型式"
        Me.型式.Name = "型式"
        Me.型式.Width = 230
        '
        '数量
        '
        Me.数量.HeaderText = "数量"
        Me.数量.Name = "数量"
        Me.数量.Width = 80
        '
        '単位
        '
        Me.単位.HeaderText = "単位"
        Me.単位.Name = "単位"
        Me.単位.Width = 80
        '
        '仕入先
        '
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.仕入先.DefaultCellStyle = DataGridViewCellStyle10
        Me.仕入先.HeaderText = "仕入先"
        Me.仕入先.Name = "仕入先"
        Me.仕入先.Visible = False
        Me.仕入先.Width = 85
        '
        '仕入単価
        '
        Me.仕入単価.HeaderText = "仕入単価"
        Me.仕入単価.Name = "仕入単価"
        Me.仕入単価.Width = 80
        '
        '間接費
        '
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.間接費.DefaultCellStyle = DataGridViewCellStyle11
        Me.間接費.HeaderText = "間接費"
        Me.間接費.Name = "間接費"
        Me.間接費.ReadOnly = True
        Me.間接費.Visible = False
        '
        '仕入金額
        '
        DataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.仕入金額.DefaultCellStyle = DataGridViewCellStyle12
        Me.仕入金額.HeaderText = "仕入金額"
        Me.仕入金額.Name = "仕入金額"
        Me.仕入金額.ReadOnly = True
        '
        'リードタイム
        '
        Me.リードタイム.HeaderText = "リードタイム"
        Me.リードタイム.Name = "リードタイム"
        '
        '入庫数
        '
        Me.入庫数.HeaderText = "入庫数"
        Me.入庫数.Name = "入庫数"
        Me.入庫数.Visible = False
        '
        '未入庫数
        '
        Me.未入庫数.HeaderText = "未入庫数"
        Me.未入庫数.Name = "未入庫数"
        Me.未入庫数.Visible = False
        '
        '備考
        '
        Me.備考.HeaderText = "備考"
        Me.備考.Name = "備考"
        Me.備考.Width = 80
        '
        'Label9
        '
        Me.Label9.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label9.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(11, 187)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(110, 23)
        Me.Label9.TabIndex = 239
        Me.Label9.Text = "発注備考"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtPurchaseRemark
        '
        Me.TxtPurchaseRemark.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtPurchaseRemark.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPurchaseRemark.Location = New System.Drawing.Point(127, 187)
        Me.TxtPurchaseRemark.MaxLength = 50
        Me.TxtPurchaseRemark.Name = "TxtPurchaseRemark"
        Me.TxtPurchaseRemark.Size = New System.Drawing.Size(476, 23)
        Me.TxtPurchaseRemark.TabIndex = 240
        '
        'DtpOrderingDate
        '
        Me.DtpOrderingDate.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpOrderingDate.CustomFormat = "yyyy/MM/dd"
        Me.DtpOrderingDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpOrderingDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpOrderingDate.Location = New System.Drawing.Point(391, 14)
        Me.DtpOrderingDate.Name = "DtpOrderingDate"
        Me.DtpOrderingDate.Size = New System.Drawing.Size(148, 22)
        Me.DtpOrderingDate.TabIndex = 228
        Me.DtpOrderingDate.TabStop = False
        Me.DtpOrderingDate.Value = New Date(2018, 6, 5, 23, 23, 58, 0)
        '
        'TxtOrderingAmount
        '
        Me.TxtOrderingAmount.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtOrderingAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtOrderingAmount.Enabled = False
        Me.TxtOrderingAmount.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtOrderingAmount.Location = New System.Drawing.Point(1109, 631)
        Me.TxtOrderingAmount.MaxLength = 10
        Me.TxtOrderingAmount.Name = "TxtOrderingAmount"
        Me.TxtOrderingAmount.ReadOnly = True
        Me.TxtOrderingAmount.Size = New System.Drawing.Size(231, 23)
        Me.TxtOrderingAmount.TabIndex = 226
        Me.TxtOrderingAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LblOrderAmount
        '
        Me.LblOrderAmount.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblOrderAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblOrderAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblOrderAmount.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblOrderAmount.Location = New System.Drawing.Point(1003, 631)
        Me.LblOrderAmount.Name = "LblOrderAmount"
        Me.LblOrderAmount.Size = New System.Drawing.Size(100, 23)
        Me.LblOrderAmount.TabIndex = 227
        Me.LblOrderAmount.Text = "発注金額"
        Me.LblOrderAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtItemCount
        '
        Me.TxtItemCount.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtItemCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtItemCount.Enabled = False
        Me.TxtItemCount.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtItemCount.Location = New System.Drawing.Point(1274, 187)
        Me.TxtItemCount.MaxLength = 20
        Me.TxtItemCount.Name = "TxtItemCount"
        Me.TxtItemCount.ReadOnly = True
        Me.TxtItemCount.Size = New System.Drawing.Size(66, 23)
        Me.TxtItemCount.TabIndex = 222
        Me.TxtItemCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LblItemCount
        '
        Me.LblItemCount.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblItemCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblItemCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblItemCount.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblItemCount.Location = New System.Drawing.Point(1274, 158)
        Me.LblItemCount.Name = "LblItemCount"
        Me.LblItemCount.Size = New System.Drawing.Size(66, 23)
        Me.LblItemCount.TabIndex = 223
        Me.LblItemCount.Text = "明細数"
        Me.LblItemCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(221, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(11, 12)
        Me.Label2.TabIndex = 221
        Me.Label2.Text = "-"
        '
        'TxtSupplierCode
        '
        Me.TxtSupplierCode.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtSupplierCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtSupplierCode.Enabled = False
        Me.TxtSupplierCode.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSupplierCode.Location = New System.Drawing.Point(127, 42)
        Me.TxtSupplierCode.MaxLength = 50
        Me.TxtSupplierCode.Name = "TxtSupplierCode"
        Me.TxtSupplierCode.Size = New System.Drawing.Size(140, 23)
        Me.TxtSupplierCode.TabIndex = 193
        '
        'BtnBack
        '
        Me.BtnBack.Location = New System.Drawing.Point(1175, 675)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 218
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'DtpOrderingRegistration
        '
        Me.DtpOrderingRegistration.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpOrderingRegistration.CustomFormat = "yyyy/MM/dd"
        Me.DtpOrderingRegistration.Enabled = False
        Me.DtpOrderingRegistration.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpOrderingRegistration.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpOrderingRegistration.Location = New System.Drawing.Point(661, 13)
        Me.DtpOrderingRegistration.Name = "DtpOrderingRegistration"
        Me.DtpOrderingRegistration.Size = New System.Drawing.Size(150, 22)
        Me.DtpOrderingRegistration.TabIndex = 186
        Me.DtpOrderingRegistration.TabStop = False
        Me.DtpOrderingRegistration.Value = New Date(2018, 6, 5, 23, 23, 58, 0)
        '
        'BtnRegistration
        '
        Me.BtnRegistration.Location = New System.Drawing.Point(1003, 675)
        Me.BtnRegistration.Name = "BtnRegistration"
        Me.BtnRegistration.Size = New System.Drawing.Size(165, 40)
        Me.BtnRegistration.TabIndex = 217
        Me.BtnRegistration.Text = "登録"
        Me.BtnRegistration.UseVisualStyleBackColor = True
        '
        'TxtAddress3
        '
        Me.TxtAddress3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtAddress3.BackColor = System.Drawing.Color.White
        Me.TxtAddress3.Enabled = False
        Me.TxtAddress3.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAddress3.Location = New System.Drawing.Point(273, 129)
        Me.TxtAddress3.MaxLength = 100
        Me.TxtAddress3.Name = "TxtAddress3"
        Me.TxtAddress3.Size = New System.Drawing.Size(330, 23)
        Me.TxtAddress3.TabIndex = 205
        '
        'TxtAddress2
        '
        Me.TxtAddress2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtAddress2.BackColor = System.Drawing.Color.White
        Me.TxtAddress2.Enabled = False
        Me.TxtAddress2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAddress2.Location = New System.Drawing.Point(273, 100)
        Me.TxtAddress2.MaxLength = 100
        Me.TxtAddress2.Name = "TxtAddress2"
        Me.TxtAddress2.Size = New System.Drawing.Size(330, 23)
        Me.TxtAddress2.TabIndex = 204
        '
        'LblRegistration
        '
        Me.LblRegistration.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblRegistration.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblRegistration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblRegistration.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblRegistration.Location = New System.Drawing.Point(545, 13)
        Me.LblRegistration.Name = "LblRegistration"
        Me.LblRegistration.Size = New System.Drawing.Size(110, 23)
        Me.LblRegistration.TabIndex = 220
        Me.LblRegistration.Text = "発注登録日"
        Me.LblRegistration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtSales
        '
        Me.TxtSales.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtSales.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtSales.Enabled = False
        Me.TxtSales.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSales.Location = New System.Drawing.Point(1127, 42)
        Me.TxtSales.MaxLength = 20
        Me.TxtSales.Name = "TxtSales"
        Me.TxtSales.Size = New System.Drawing.Size(200, 23)
        Me.TxtSales.TabIndex = 213
        '
        'LblSales
        '
        Me.LblSales.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblSales.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblSales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblSales.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSales.Location = New System.Drawing.Point(971, 42)
        Me.LblSales.Name = "LblSales"
        Me.LblSales.Size = New System.Drawing.Size(150, 23)
        Me.LblSales.TabIndex = 219
        Me.LblSales.Text = "営業担当者"
        Me.LblSales.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtOrderingSuffix
        '
        Me.TxtOrderingSuffix.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtOrderingSuffix.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtOrderingSuffix.Enabled = False
        Me.TxtOrderingSuffix.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtOrderingSuffix.Location = New System.Drawing.Point(238, 13)
        Me.TxtOrderingSuffix.MaxLength = 1
        Me.TxtOrderingSuffix.Name = "TxtOrderingSuffix"
        Me.TxtOrderingSuffix.ReadOnly = True
        Me.TxtOrderingSuffix.Size = New System.Drawing.Size(29, 23)
        Me.TxtOrderingSuffix.TabIndex = 196
        Me.TxtOrderingSuffix.TabStop = False
        '
        'TxtQuoteRemarks
        '
        Me.TxtQuoteRemarks.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtQuoteRemarks.BackColor = System.Drawing.Color.White
        Me.TxtQuoteRemarks.Enabled = False
        Me.TxtQuoteRemarks.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtQuoteRemarks.Location = New System.Drawing.Point(127, 216)
        Me.TxtQuoteRemarks.MaxLength = 100
        Me.TxtQuoteRemarks.Multiline = True
        Me.TxtQuoteRemarks.Name = "TxtQuoteRemarks"
        Me.TxtQuoteRemarks.Size = New System.Drawing.Size(476, 23)
        Me.TxtQuoteRemarks.TabIndex = 215
        '
        'TxtPaymentTerms
        '
        Me.TxtPaymentTerms.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtPaymentTerms.BackColor = System.Drawing.Color.White
        Me.TxtPaymentTerms.Enabled = False
        Me.TxtPaymentTerms.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPaymentTerms.Location = New System.Drawing.Point(127, 158)
        Me.TxtPaymentTerms.MaxLength = 50
        Me.TxtPaymentTerms.Multiline = True
        Me.TxtPaymentTerms.Name = "TxtPaymentTerms"
        Me.TxtPaymentTerms.Size = New System.Drawing.Size(476, 23)
        Me.TxtPaymentTerms.TabIndex = 214
        '
        'LblRemarks
        '
        Me.LblRemarks.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblRemarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblRemarks.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblRemarks.Location = New System.Drawing.Point(11, 216)
        Me.LblRemarks.Name = "LblRemarks"
        Me.LblRemarks.Size = New System.Drawing.Size(110, 23)
        Me.LblRemarks.TabIndex = 189
        Me.LblRemarks.Text = "備考"
        Me.LblRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtPosition
        '
        Me.TxtPosition.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtPosition.BackColor = System.Drawing.Color.White
        Me.TxtPosition.Enabled = False
        Me.TxtPosition.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPosition.Location = New System.Drawing.Point(765, 129)
        Me.TxtPosition.MaxLength = 50
        Me.TxtPosition.Name = "TxtPosition"
        Me.TxtPosition.Size = New System.Drawing.Size(200, 23)
        Me.TxtPosition.TabIndex = 212
        '
        'BtnPurchase
        '
        Me.BtnPurchase.Location = New System.Drawing.Point(832, 675)
        Me.BtnPurchase.Name = "BtnPurchase"
        Me.BtnPurchase.Size = New System.Drawing.Size(165, 40)
        Me.BtnPurchase.TabIndex = 241
        Me.BtnPurchase.Text = "発注書発行"
        Me.BtnPurchase.UseVisualStyleBackColor = True
        Me.BtnPurchase.Visible = False
        '
        'BtnClone
        '
        Me.BtnClone.Location = New System.Drawing.Point(263, 677)
        Me.BtnClone.Name = "BtnClone"
        Me.BtnClone.Size = New System.Drawing.Size(120, 40)
        Me.BtnClone.TabIndex = 296
        Me.BtnClone.TabStop = False
        Me.BtnClone.Text = "行複製"
        Me.BtnClone.UseVisualStyleBackColor = True
        '
        'BtnDown
        '
        Me.BtnDown.Location = New System.Drawing.Point(263, 631)
        Me.BtnDown.Name = "BtnDown"
        Me.BtnDown.Size = New System.Drawing.Size(120, 40)
        Me.BtnDown.TabIndex = 297
        Me.BtnDown.TabStop = False
        Me.BtnDown.Text = "行移動↓"
        Me.BtnDown.UseVisualStyleBackColor = True
        '
        'BtnUp
        '
        Me.BtnUp.Location = New System.Drawing.Point(137, 631)
        Me.BtnUp.Name = "BtnUp"
        Me.BtnUp.Size = New System.Drawing.Size(120, 40)
        Me.BtnUp.TabIndex = 298
        Me.BtnUp.TabStop = False
        Me.BtnUp.Text = "行移動↑"
        Me.BtnUp.UseVisualStyleBackColor = True
        '
        'BtnInsert
        '
        Me.BtnInsert.Location = New System.Drawing.Point(11, 631)
        Me.BtnInsert.Name = "BtnInsert"
        Me.BtnInsert.Size = New System.Drawing.Size(120, 40)
        Me.BtnInsert.TabIndex = 299
        Me.BtnInsert.TabStop = False
        Me.BtnInsert.Text = "行挿入"
        Me.BtnInsert.UseVisualStyleBackColor = True
        '
        'BtnRowsDel
        '
        Me.BtnRowsDel.Location = New System.Drawing.Point(134, 677)
        Me.BtnRowsDel.Name = "BtnRowsDel"
        Me.BtnRowsDel.Size = New System.Drawing.Size(120, 40)
        Me.BtnRowsDel.TabIndex = 300
        Me.BtnRowsDel.TabStop = False
        Me.BtnRowsDel.Text = "行削除"
        Me.BtnRowsDel.UseVisualStyleBackColor = True
        '
        'BtnRowsAdd
        '
        Me.BtnRowsAdd.Location = New System.Drawing.Point(11, 677)
        Me.BtnRowsAdd.Name = "BtnRowsAdd"
        Me.BtnRowsAdd.Size = New System.Drawing.Size(120, 40)
        Me.BtnRowsAdd.TabIndex = 301
        Me.BtnRowsAdd.TabStop = False
        Me.BtnRowsAdd.Text = "行追加"
        Me.BtnRowsAdd.UseVisualStyleBackColor = True
        '
        'Ordering
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1350, 729)
        Me.Controls.Add(Me.BtnClone)
        Me.Controls.Add(Me.BtnDown)
        Me.Controls.Add(Me.BtnUp)
        Me.Controls.Add(Me.BtnInsert)
        Me.Controls.Add(Me.BtnRowsDel)
        Me.Controls.Add(Me.BtnRowsAdd)
        Me.Controls.Add(Me.BtnPurchase)
        Me.Controls.Add(Me.LblPaymentTerms)
        Me.Controls.Add(Me.LblPosition)
        Me.Controls.Add(Me.TxtTel)
        Me.Controls.Add(Me.LblPerson)
        Me.Controls.Add(Me.TxtAddress1)
        Me.Controls.Add(Me.TxtPerson)
        Me.Controls.Add(Me.LblFax)
        Me.Controls.Add(Me.TxtFax)
        Me.Controls.Add(Me.TxtPostalCode)
        Me.Controls.Add(Me.LblTel)
        Me.Controls.Add(Me.LblAddress)
        Me.Controls.Add(Me.TxtSupplierName)
        Me.Controls.Add(Me.TxtInput)
        Me.Controls.Add(Me.LblCustomerName)
        Me.Controls.Add(Me.TxtOrderingNo)
        Me.Controls.Add(Me.LblInput)
        Me.Controls.Add(Me.LblOrderDate)
        Me.Controls.Add(Me.LblOrderNo)
        Me.Controls.Add(Me.DgvItemList)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.TxtPurchaseRemark)
        Me.Controls.Add(Me.DtpOrderingDate)
        Me.Controls.Add(Me.TxtOrderingAmount)
        Me.Controls.Add(Me.LblOrderAmount)
        Me.Controls.Add(Me.TxtItemCount)
        Me.Controls.Add(Me.LblItemCount)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtSupplierCode)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.DtpOrderingRegistration)
        Me.Controls.Add(Me.BtnRegistration)
        Me.Controls.Add(Me.TxtAddress3)
        Me.Controls.Add(Me.TxtAddress2)
        Me.Controls.Add(Me.LblRegistration)
        Me.Controls.Add(Me.TxtSales)
        Me.Controls.Add(Me.LblSales)
        Me.Controls.Add(Me.TxtOrderingSuffix)
        Me.Controls.Add(Me.TxtQuoteRemarks)
        Me.Controls.Add(Me.TxtPaymentTerms)
        Me.Controls.Add(Me.LblRemarks)
        Me.Controls.Add(Me.TxtPosition)
        Me.Name = "Ordering"
        Me.Text = "Ordering"
        CType(Me.DgvItemList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LblPaymentTerms As Label
    Friend WithEvents LblPosition As Label
    Friend WithEvents TxtTel As TextBox
    Friend WithEvents LblPerson As Label
    Friend WithEvents TxtAddress1 As TextBox
    Friend WithEvents TxtPerson As TextBox
    Friend WithEvents LblFax As Label
    Friend WithEvents TxtFax As TextBox
    Friend WithEvents TxtPostalCode As TextBox
    Friend WithEvents LblTel As Label
    Friend WithEvents LblAddress As Label
    Friend WithEvents TxtSupplierName As TextBox
    Friend WithEvents TxtInput As TextBox
    Friend WithEvents LblCustomerName As Label
    Friend WithEvents TxtOrderingNo As TextBox
    Friend WithEvents LblInput As Label
    Friend WithEvents LblOrderDate As Label
    Friend WithEvents LblOrderNo As Label
    Friend WithEvents DgvItemList As DataGridView
    Friend WithEvents Label9 As Label
    Friend WithEvents TxtPurchaseRemark As TextBox
    Friend WithEvents DtpOrderingDate As DateTimePicker
    Friend WithEvents TxtOrderingAmount As TextBox
    Friend WithEvents LblOrderAmount As Label
    Friend WithEvents TxtItemCount As TextBox
    Friend WithEvents LblItemCount As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtSupplierCode As TextBox
    Friend WithEvents BtnBack As Button
    Friend WithEvents DtpOrderingRegistration As DateTimePicker
    Friend WithEvents BtnRegistration As Button
    Friend WithEvents TxtAddress3 As TextBox
    Friend WithEvents TxtAddress2 As TextBox
    Friend WithEvents LblRegistration As Label
    Friend WithEvents TxtSales As TextBox
    Friend WithEvents LblSales As Label
    Friend WithEvents TxtOrderingSuffix As TextBox
    Friend WithEvents TxtQuoteRemarks As TextBox
    Friend WithEvents TxtPaymentTerms As TextBox
    Friend WithEvents LblRemarks As Label
    Friend WithEvents TxtPosition As TextBox
    Friend WithEvents BtnPurchase As Button
    Friend WithEvents No As DataGridViewTextBoxColumn
    Friend WithEvents メーカー As DataGridViewTextBoxColumn
    Friend WithEvents 品名 As DataGridViewTextBoxColumn
    Friend WithEvents 型式 As DataGridViewTextBoxColumn
    Friend WithEvents 数量 As DataGridViewTextBoxColumn
    Friend WithEvents 単位 As DataGridViewTextBoxColumn
    Friend WithEvents 仕入先 As DataGridViewTextBoxColumn
    Friend WithEvents 仕入単価 As DataGridViewTextBoxColumn
    Friend WithEvents 間接費 As DataGridViewTextBoxColumn
    Friend WithEvents 仕入金額 As DataGridViewTextBoxColumn
    Friend WithEvents リードタイム As DataGridViewTextBoxColumn
    Friend WithEvents 入庫数 As DataGridViewTextBoxColumn
    Friend WithEvents 未入庫数 As DataGridViewTextBoxColumn
    Friend WithEvents 備考 As DataGridViewTextBoxColumn
    Friend WithEvents BtnClone As Button
    Friend WithEvents BtnDown As Button
    Friend WithEvents BtnUp As Button
    Friend WithEvents BtnInsert As Button
    Friend WithEvents BtnRowsDel As Button
    Friend WithEvents BtnRowsAdd As Button
End Class
