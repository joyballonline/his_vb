<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Ordering
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
        Me.LblSupplierName = New System.Windows.Forms.Label()
        Me.TxtOrderingNo = New System.Windows.Forms.TextBox()
        Me.LblInput = New System.Windows.Forms.Label()
        Me.LblPurchaseDate = New System.Windows.Forms.Label()
        Me.LblPurchaseNo = New System.Windows.Forms.Label()
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
        Me.LblPurchaseRemarks = New System.Windows.Forms.Label()
        Me.TxtPurchaseRemark = New System.Windows.Forms.TextBox()
        Me.DtpPurchaseDate = New System.Windows.Forms.DateTimePicker()
        Me.TxtPurchaseAmount = New System.Windows.Forms.TextBox()
        Me.LblPurchaseAmount = New System.Windows.Forms.Label()
        Me.TxtItemCount = New System.Windows.Forms.TextBox()
        Me.LblItemCount = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtSupplierCode = New System.Windows.Forms.TextBox()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.DtpRegistrationDate = New System.Windows.Forms.DateTimePicker()
        Me.BtnRegistration = New System.Windows.Forms.Button()
        Me.TxtAddress3 = New System.Windows.Forms.TextBox()
        Me.TxtAddress2 = New System.Windows.Forms.TextBox()
        Me.LblRegistrationDate = New System.Windows.Forms.Label()
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
        Me.LblMode = New System.Windows.Forms.Label()
        Me.TxtCustomerPO = New System.Windows.Forms.TextBox()
        Me.LblCustomerPO = New System.Windows.Forms.Label()
        Me.BtnCodeSearch = New System.Windows.Forms.Button()
        Me.CbShippedBy = New System.Windows.Forms.ComboBox()
        Me.LblMethod = New System.Windows.Forms.Label()
        Me.DtpShippedDate = New System.Windows.Forms.DateTimePicker()
        Me.LblShipDate = New System.Windows.Forms.Label()
        CType(Me.DgvItemList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LblPaymentTerms
        '
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
        Me.TxtTel.BackColor = System.Drawing.Color.White
        Me.TxtTel.Enabled = False
        Me.TxtTel.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtTel.Location = New System.Drawing.Point(765, 42)
        Me.TxtTel.MaxLength = 15
        Me.TxtTel.Name = "TxtTel"
        Me.TxtTel.Size = New System.Drawing.Size(200, 23)
        Me.TxtTel.TabIndex = 13
        '
        'LblPerson
        '
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
        Me.TxtAddress1.BackColor = System.Drawing.Color.White
        Me.TxtAddress1.Enabled = False
        Me.TxtAddress1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAddress1.Location = New System.Drawing.Point(273, 71)
        Me.TxtAddress1.MaxLength = 100
        Me.TxtAddress1.Name = "TxtAddress1"
        Me.TxtAddress1.Size = New System.Drawing.Size(330, 23)
        Me.TxtAddress1.TabIndex = 7
        '
        'TxtPerson
        '
        Me.TxtPerson.BackColor = System.Drawing.Color.White
        Me.TxtPerson.Enabled = False
        Me.TxtPerson.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPerson.Location = New System.Drawing.Point(765, 100)
        Me.TxtPerson.MaxLength = 50
        Me.TxtPerson.Name = "TxtPerson"
        Me.TxtPerson.Size = New System.Drawing.Size(200, 23)
        Me.TxtPerson.TabIndex = 15
        '
        'LblFax
        '
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
        Me.TxtFax.BackColor = System.Drawing.Color.White
        Me.TxtFax.Enabled = False
        Me.TxtFax.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtFax.Location = New System.Drawing.Point(765, 71)
        Me.TxtFax.MaxLength = 15
        Me.TxtFax.Name = "TxtFax"
        Me.TxtFax.Size = New System.Drawing.Size(200, 23)
        Me.TxtFax.TabIndex = 14
        '
        'TxtPostalCode
        '
        Me.TxtPostalCode.BackColor = System.Drawing.Color.White
        Me.TxtPostalCode.Enabled = False
        Me.TxtPostalCode.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPostalCode.Location = New System.Drawing.Point(127, 71)
        Me.TxtPostalCode.MaxLength = 3
        Me.TxtPostalCode.Name = "TxtPostalCode"
        Me.TxtPostalCode.Size = New System.Drawing.Size(140, 23)
        Me.TxtPostalCode.TabIndex = 6
        Me.TxtPostalCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LblTel
        '
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
        Me.TxtSupplierName.BackColor = System.Drawing.Color.White
        Me.TxtSupplierName.Enabled = False
        Me.TxtSupplierName.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSupplierName.Location = New System.Drawing.Point(273, 42)
        Me.TxtSupplierName.MaxLength = 50
        Me.TxtSupplierName.Name = "TxtSupplierName"
        Me.TxtSupplierName.Size = New System.Drawing.Size(330, 23)
        Me.TxtSupplierName.TabIndex = 5
        '
        'TxtInput
        '
        Me.TxtInput.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtInput.Enabled = False
        Me.TxtInput.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtInput.Location = New System.Drawing.Point(1127, 71)
        Me.TxtInput.MaxLength = 20
        Me.TxtInput.Name = "TxtInput"
        Me.TxtInput.ReadOnly = True
        Me.TxtInput.Size = New System.Drawing.Size(213, 23)
        Me.TxtInput.TabIndex = 199
        Me.TxtInput.TabStop = False
        '
        'LblSupplierName
        '
        Me.LblSupplierName.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblSupplierName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblSupplierName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSupplierName.Location = New System.Drawing.Point(11, 42)
        Me.LblSupplierName.Name = "LblSupplierName"
        Me.LblSupplierName.Size = New System.Drawing.Size(110, 23)
        Me.LblSupplierName.TabIndex = 188
        Me.LblSupplierName.Text = "仕入先名称"
        Me.LblSupplierName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtOrderingNo
        '
        Me.TxtOrderingNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtOrderingNo.Enabled = False
        Me.TxtOrderingNo.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtOrderingNo.Location = New System.Drawing.Point(186, 13)
        Me.TxtOrderingNo.MaxLength = 8
        Me.TxtOrderingNo.Name = "TxtOrderingNo"
        Me.TxtOrderingNo.ReadOnly = True
        Me.TxtOrderingNo.Size = New System.Drawing.Size(88, 23)
        Me.TxtOrderingNo.TabIndex = 194
        Me.TxtOrderingNo.TabStop = False
        '
        'LblInput
        '
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
        'LblPurchaseDate
        '
        Me.LblPurchaseDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblPurchaseDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblPurchaseDate.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPurchaseDate.Location = New System.Drawing.Point(542, 13)
        Me.LblPurchaseDate.Name = "LblPurchaseDate"
        Me.LblPurchaseDate.Size = New System.Drawing.Size(112, 23)
        Me.LblPurchaseDate.TabIndex = 206
        Me.LblPurchaseDate.Text = "発注日"
        Me.LblPurchaseDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblPurchaseNo
        '
        Me.LblPurchaseNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblPurchaseNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblPurchaseNo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPurchaseNo.Location = New System.Drawing.Point(11, 13)
        Me.LblPurchaseNo.Name = "LblPurchaseNo"
        Me.LblPurchaseNo.Size = New System.Drawing.Size(169, 23)
        Me.LblPurchaseNo.TabIndex = 191
        Me.LblPurchaseNo.Text = "発注番号"
        Me.LblPurchaseNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DgvItemList
        '
        Me.DgvItemList.AllowUserToAddRows = False
        Me.DgvItemList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DgvItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvItemList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.No, Me.メーカー, Me.品名, Me.型式, Me.数量, Me.単位, Me.仕入先, Me.仕入単価, Me.間接費, Me.仕入金額, Me.リードタイム, Me.入庫数, Me.未入庫数, Me.備考})
        Me.DgvItemList.Location = New System.Drawing.Point(11, 274)
        Me.DgvItemList.Name = "DgvItemList"
        Me.DgvItemList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DgvItemList.RowHeadersVisible = False
        Me.DgvItemList.RowTemplate.Height = 21
        Me.DgvItemList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvItemList.Size = New System.Drawing.Size(1329, 185)
        Me.DgvItemList.TabIndex = 18
        '
        'No
        '
        Me.No.HeaderText = "No"
        Me.No.Name = "No"
        Me.No.Width = 43
        '
        'メーカー
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.メーカー.DefaultCellStyle = DataGridViewCellStyle1
        Me.メーカー.HeaderText = "メーカー"
        Me.メーカー.Name = "メーカー"
        Me.メーカー.Width = 220
        '
        '品名
        '
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.品名.DefaultCellStyle = DataGridViewCellStyle2
        Me.品名.HeaderText = "品名"
        Me.品名.Name = "品名"
        Me.品名.Width = 220
        '
        '型式
        '
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.型式.DefaultCellStyle = DataGridViewCellStyle3
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
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.仕入先.DefaultCellStyle = DataGridViewCellStyle4
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
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.間接費.DefaultCellStyle = DataGridViewCellStyle5
        Me.間接費.HeaderText = "間接費"
        Me.間接費.Name = "間接費"
        Me.間接費.ReadOnly = True
        Me.間接費.Visible = False
        '
        '仕入金額
        '
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.仕入金額.DefaultCellStyle = DataGridViewCellStyle6
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
        'LblPurchaseRemarks
        '
        Me.LblPurchaseRemarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblPurchaseRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblPurchaseRemarks.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPurchaseRemarks.Location = New System.Drawing.Point(11, 187)
        Me.LblPurchaseRemarks.Name = "LblPurchaseRemarks"
        Me.LblPurchaseRemarks.Size = New System.Drawing.Size(110, 23)
        Me.LblPurchaseRemarks.TabIndex = 239
        Me.LblPurchaseRemarks.Text = "発注備考"
        Me.LblPurchaseRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtPurchaseRemark
        '
        Me.TxtPurchaseRemark.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPurchaseRemark.Location = New System.Drawing.Point(127, 187)
        Me.TxtPurchaseRemark.MaxLength = 50
        Me.TxtPurchaseRemark.Name = "TxtPurchaseRemark"
        Me.TxtPurchaseRemark.Size = New System.Drawing.Size(476, 23)
        Me.TxtPurchaseRemark.TabIndex = 11
        '
        'DtpPurchaseDate
        '
        Me.DtpPurchaseDate.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpPurchaseDate.CustomFormat = ""
        Me.DtpPurchaseDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpPurchaseDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpPurchaseDate.Location = New System.Drawing.Point(660, 14)
        Me.DtpPurchaseDate.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.DtpPurchaseDate.Name = "DtpPurchaseDate"
        Me.DtpPurchaseDate.Size = New System.Drawing.Size(148, 22)
        Me.DtpPurchaseDate.TabIndex = 2
        Me.DtpPurchaseDate.Value = New Date(2018, 6, 5, 23, 23, 58, 0)
        '
        'TxtPurchaseAmount
        '
        Me.TxtPurchaseAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtPurchaseAmount.Enabled = False
        Me.TxtPurchaseAmount.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPurchaseAmount.Location = New System.Drawing.Point(1109, 465)
        Me.TxtPurchaseAmount.MaxLength = 10
        Me.TxtPurchaseAmount.Name = "TxtPurchaseAmount"
        Me.TxtPurchaseAmount.ReadOnly = True
        Me.TxtPurchaseAmount.Size = New System.Drawing.Size(231, 23)
        Me.TxtPurchaseAmount.TabIndex = 226
        Me.TxtPurchaseAmount.TabStop = False
        Me.TxtPurchaseAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LblPurchaseAmount
        '
        Me.LblPurchaseAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblPurchaseAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblPurchaseAmount.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPurchaseAmount.Location = New System.Drawing.Point(1003, 465)
        Me.LblPurchaseAmount.Name = "LblPurchaseAmount"
        Me.LblPurchaseAmount.Size = New System.Drawing.Size(100, 23)
        Me.LblPurchaseAmount.TabIndex = 227
        Me.LblPurchaseAmount.Text = "発注金額"
        Me.LblPurchaseAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtItemCount
        '
        Me.TxtItemCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtItemCount.Enabled = False
        Me.TxtItemCount.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtItemCount.Location = New System.Drawing.Point(1255, 245)
        Me.TxtItemCount.MaxLength = 20
        Me.TxtItemCount.Name = "TxtItemCount"
        Me.TxtItemCount.ReadOnly = True
        Me.TxtItemCount.Size = New System.Drawing.Size(85, 23)
        Me.TxtItemCount.TabIndex = 222
        Me.TxtItemCount.TabStop = False
        Me.TxtItemCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LblItemCount
        '
        Me.LblItemCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblItemCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblItemCount.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblItemCount.Location = New System.Drawing.Point(1255, 216)
        Me.LblItemCount.Name = "LblItemCount"
        Me.LblItemCount.Size = New System.Drawing.Size(85, 23)
        Me.LblItemCount.TabIndex = 223
        Me.LblItemCount.Text = "明細数"
        Me.LblItemCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(280, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(11, 12)
        Me.Label2.TabIndex = 221
        Me.Label2.Text = "-"
        '
        'TxtSupplierCode
        '
        Me.TxtSupplierCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtSupplierCode.Enabled = False
        Me.TxtSupplierCode.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSupplierCode.Location = New System.Drawing.Point(127, 42)
        Me.TxtSupplierCode.MaxLength = 50
        Me.TxtSupplierCode.Name = "TxtSupplierCode"
        Me.TxtSupplierCode.Size = New System.Drawing.Size(62, 23)
        Me.TxtSupplierCode.TabIndex = 3
        '
        'BtnBack
        '
        Me.BtnBack.Location = New System.Drawing.Point(1175, 509)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 21
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'DtpRegistrationDate
        '
        Me.DtpRegistrationDate.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpRegistrationDate.CustomFormat = ""
        Me.DtpRegistrationDate.Enabled = False
        Me.DtpRegistrationDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpRegistrationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpRegistrationDate.Location = New System.Drawing.Point(958, 13)
        Me.DtpRegistrationDate.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.DtpRegistrationDate.Name = "DtpRegistrationDate"
        Me.DtpRegistrationDate.Size = New System.Drawing.Size(150, 22)
        Me.DtpRegistrationDate.TabIndex = 186
        Me.DtpRegistrationDate.TabStop = False
        Me.DtpRegistrationDate.Value = New Date(2018, 6, 5, 23, 23, 58, 0)
        '
        'BtnRegistration
        '
        Me.BtnRegistration.Location = New System.Drawing.Point(1003, 509)
        Me.BtnRegistration.Name = "BtnRegistration"
        Me.BtnRegistration.Size = New System.Drawing.Size(165, 40)
        Me.BtnRegistration.TabIndex = 20
        Me.BtnRegistration.Text = "登録"
        Me.BtnRegistration.UseVisualStyleBackColor = True
        '
        'TxtAddress3
        '
        Me.TxtAddress3.BackColor = System.Drawing.Color.White
        Me.TxtAddress3.Enabled = False
        Me.TxtAddress3.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAddress3.Location = New System.Drawing.Point(273, 129)
        Me.TxtAddress3.MaxLength = 100
        Me.TxtAddress3.Name = "TxtAddress3"
        Me.TxtAddress3.Size = New System.Drawing.Size(330, 23)
        Me.TxtAddress3.TabIndex = 9
        '
        'TxtAddress2
        '
        Me.TxtAddress2.BackColor = System.Drawing.Color.White
        Me.TxtAddress2.Enabled = False
        Me.TxtAddress2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAddress2.Location = New System.Drawing.Point(273, 100)
        Me.TxtAddress2.MaxLength = 100
        Me.TxtAddress2.Name = "TxtAddress2"
        Me.TxtAddress2.Size = New System.Drawing.Size(330, 23)
        Me.TxtAddress2.TabIndex = 8
        '
        'LblRegistrationDate
        '
        Me.LblRegistrationDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblRegistrationDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblRegistrationDate.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblRegistrationDate.Location = New System.Drawing.Point(814, 13)
        Me.LblRegistrationDate.Name = "LblRegistrationDate"
        Me.LblRegistrationDate.Size = New System.Drawing.Size(138, 23)
        Me.LblRegistrationDate.TabIndex = 220
        Me.LblRegistrationDate.Text = "発注登録日"
        Me.LblRegistrationDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtSales
        '
        Me.TxtSales.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtSales.Enabled = False
        Me.TxtSales.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSales.Location = New System.Drawing.Point(1127, 42)
        Me.TxtSales.MaxLength = 20
        Me.TxtSales.Name = "TxtSales"
        Me.TxtSales.Size = New System.Drawing.Size(213, 23)
        Me.TxtSales.TabIndex = 17
        '
        'LblSales
        '
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
        Me.TxtOrderingSuffix.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtOrderingSuffix.Enabled = False
        Me.TxtOrderingSuffix.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtOrderingSuffix.Location = New System.Drawing.Point(297, 13)
        Me.TxtOrderingSuffix.MaxLength = 1
        Me.TxtOrderingSuffix.Name = "TxtOrderingSuffix"
        Me.TxtOrderingSuffix.ReadOnly = True
        Me.TxtOrderingSuffix.Size = New System.Drawing.Size(29, 23)
        Me.TxtOrderingSuffix.TabIndex = 196
        Me.TxtOrderingSuffix.TabStop = False
        '
        'TxtQuoteRemarks
        '
        Me.TxtQuoteRemarks.BackColor = System.Drawing.Color.White
        Me.TxtQuoteRemarks.Enabled = False
        Me.TxtQuoteRemarks.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtQuoteRemarks.Location = New System.Drawing.Point(127, 216)
        Me.TxtQuoteRemarks.MaxLength = 100
        Me.TxtQuoteRemarks.Multiline = True
        Me.TxtQuoteRemarks.Name = "TxtQuoteRemarks"
        Me.TxtQuoteRemarks.Size = New System.Drawing.Size(476, 23)
        Me.TxtQuoteRemarks.TabIndex = 12
        '
        'TxtPaymentTerms
        '
        Me.TxtPaymentTerms.BackColor = System.Drawing.Color.White
        Me.TxtPaymentTerms.Enabled = False
        Me.TxtPaymentTerms.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPaymentTerms.Location = New System.Drawing.Point(127, 158)
        Me.TxtPaymentTerms.MaxLength = 50
        Me.TxtPaymentTerms.Multiline = True
        Me.TxtPaymentTerms.Name = "TxtPaymentTerms"
        Me.TxtPaymentTerms.Size = New System.Drawing.Size(476, 23)
        Me.TxtPaymentTerms.TabIndex = 10
        '
        'LblRemarks
        '
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
        Me.TxtPosition.BackColor = System.Drawing.Color.White
        Me.TxtPosition.Enabled = False
        Me.TxtPosition.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPosition.Location = New System.Drawing.Point(765, 129)
        Me.TxtPosition.MaxLength = 50
        Me.TxtPosition.Name = "TxtPosition"
        Me.TxtPosition.Size = New System.Drawing.Size(200, 23)
        Me.TxtPosition.TabIndex = 16
        '
        'BtnPurchase
        '
        Me.BtnPurchase.Location = New System.Drawing.Point(832, 509)
        Me.BtnPurchase.Name = "BtnPurchase"
        Me.BtnPurchase.Size = New System.Drawing.Size(165, 40)
        Me.BtnPurchase.TabIndex = 19
        Me.BtnPurchase.Text = "発注書発行"
        Me.BtnPurchase.UseVisualStyleBackColor = True
        Me.BtnPurchase.Visible = False
        '
        'BtnClone
        '
        Me.BtnClone.Location = New System.Drawing.Point(263, 511)
        Me.BtnClone.Name = "BtnClone"
        Me.BtnClone.Size = New System.Drawing.Size(120, 40)
        Me.BtnClone.TabIndex = 296
        Me.BtnClone.TabStop = False
        Me.BtnClone.Text = "行複製"
        Me.BtnClone.UseVisualStyleBackColor = True
        '
        'BtnDown
        '
        Me.BtnDown.Location = New System.Drawing.Point(263, 465)
        Me.BtnDown.Name = "BtnDown"
        Me.BtnDown.Size = New System.Drawing.Size(120, 40)
        Me.BtnDown.TabIndex = 297
        Me.BtnDown.TabStop = False
        Me.BtnDown.Text = "行移動↓"
        Me.BtnDown.UseVisualStyleBackColor = True
        '
        'BtnUp
        '
        Me.BtnUp.Location = New System.Drawing.Point(137, 465)
        Me.BtnUp.Name = "BtnUp"
        Me.BtnUp.Size = New System.Drawing.Size(120, 40)
        Me.BtnUp.TabIndex = 298
        Me.BtnUp.TabStop = False
        Me.BtnUp.Text = "行移動↑"
        Me.BtnUp.UseVisualStyleBackColor = True
        '
        'BtnInsert
        '
        Me.BtnInsert.Location = New System.Drawing.Point(11, 465)
        Me.BtnInsert.Name = "BtnInsert"
        Me.BtnInsert.Size = New System.Drawing.Size(120, 40)
        Me.BtnInsert.TabIndex = 299
        Me.BtnInsert.TabStop = False
        Me.BtnInsert.Text = "行挿入"
        Me.BtnInsert.UseVisualStyleBackColor = True
        '
        'BtnRowsDel
        '
        Me.BtnRowsDel.Location = New System.Drawing.Point(134, 511)
        Me.BtnRowsDel.Name = "BtnRowsDel"
        Me.BtnRowsDel.Size = New System.Drawing.Size(120, 40)
        Me.BtnRowsDel.TabIndex = 300
        Me.BtnRowsDel.TabStop = False
        Me.BtnRowsDel.Text = "行削除"
        Me.BtnRowsDel.UseVisualStyleBackColor = True
        '
        'BtnRowsAdd
        '
        Me.BtnRowsAdd.Location = New System.Drawing.Point(11, 511)
        Me.BtnRowsAdd.Name = "BtnRowsAdd"
        Me.BtnRowsAdd.Size = New System.Drawing.Size(120, 40)
        Me.BtnRowsAdd.TabIndex = 301
        Me.BtnRowsAdd.TabStop = False
        Me.BtnRowsAdd.Text = "行追加"
        Me.BtnRowsAdd.UseVisualStyleBackColor = True
        '
        'LblMode
        '
        Me.LblMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMode.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMode.Location = New System.Drawing.Point(1127, 14)
        Me.LblMode.Name = "LblMode"
        Me.LblMode.Size = New System.Drawing.Size(213, 22)
        Me.LblMode.TabIndex = 302
        Me.LblMode.Text = "モード"
        Me.LblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtCustomerPO
        '
        Me.TxtCustomerPO.BackColor = System.Drawing.Color.White
        Me.TxtCustomerPO.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerPO.Location = New System.Drawing.Point(448, 13)
        Me.TxtCustomerPO.MaxLength = 8
        Me.TxtCustomerPO.Name = "TxtCustomerPO"
        Me.TxtCustomerPO.Size = New System.Drawing.Size(88, 23)
        Me.TxtCustomerPO.TabIndex = 1
        '
        'LblCustomerPO
        '
        Me.LblCustomerPO.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblCustomerPO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblCustomerPO.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCustomerPO.Location = New System.Drawing.Point(332, 13)
        Me.LblCustomerPO.Name = "LblCustomerPO"
        Me.LblCustomerPO.Size = New System.Drawing.Size(110, 23)
        Me.LblCustomerPO.TabIndex = 303
        Me.LblCustomerPO.Text = "客先番号"
        Me.LblCustomerPO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnCodeSearch
        '
        Me.BtnCodeSearch.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnCodeSearch.Location = New System.Drawing.Point(195, 42)
        Me.BtnCodeSearch.Name = "BtnCodeSearch"
        Me.BtnCodeSearch.Size = New System.Drawing.Size(72, 23)
        Me.BtnCodeSearch.TabIndex = 304
        Me.BtnCodeSearch.Text = "Search"
        Me.BtnCodeSearch.UseVisualStyleBackColor = True
        '
        'CbShippedBy
        '
        Me.CbShippedBy.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CbShippedBy.FormattingEnabled = True
        Me.CbShippedBy.Items.AddRange(New Object() {"RegularShip", "DHL", "Air", "Ship"})
        Me.CbShippedBy.Location = New System.Drawing.Point(127, 245)
        Me.CbShippedBy.Name = "CbShippedBy"
        Me.CbShippedBy.Size = New System.Drawing.Size(140, 23)
        Me.CbShippedBy.TabIndex = 305
        '
        'LblMethod
        '
        Me.LblMethod.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMethod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMethod.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMethod.Location = New System.Drawing.Point(11, 245)
        Me.LblMethod.Name = "LblMethod"
        Me.LblMethod.Size = New System.Drawing.Size(110, 23)
        Me.LblMethod.TabIndex = 306
        Me.LblMethod.Text = "出荷方法"
        Me.LblMethod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DtpShippedDate
        '
        Me.DtpShippedDate.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpShippedDate.CustomFormat = "yyyy/MM/dd"
        Me.DtpShippedDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpShippedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpShippedDate.Location = New System.Drawing.Point(389, 245)
        Me.DtpShippedDate.Name = "DtpShippedDate"
        Me.DtpShippedDate.Size = New System.Drawing.Size(150, 22)
        Me.DtpShippedDate.TabIndex = 307
        Me.DtpShippedDate.Value = New Date(2018, 6, 5, 23, 23, 58, 0)
        '
        'LblShipDate
        '
        Me.LblShipDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblShipDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblShipDate.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblShipDate.Location = New System.Drawing.Point(273, 245)
        Me.LblShipDate.Name = "LblShipDate"
        Me.LblShipDate.Size = New System.Drawing.Size(110, 23)
        Me.LblShipDate.TabIndex = 308
        Me.LblShipDate.Text = "出荷日"
        Me.LblShipDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Ordering
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.DtpShippedDate)
        Me.Controls.Add(Me.LblShipDate)
        Me.Controls.Add(Me.LblMethod)
        Me.Controls.Add(Me.CbShippedBy)
        Me.Controls.Add(Me.BtnCodeSearch)
        Me.Controls.Add(Me.TxtCustomerPO)
        Me.Controls.Add(Me.LblCustomerPO)
        Me.Controls.Add(Me.LblMode)
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
        Me.Controls.Add(Me.LblSupplierName)
        Me.Controls.Add(Me.TxtOrderingNo)
        Me.Controls.Add(Me.LblInput)
        Me.Controls.Add(Me.LblPurchaseDate)
        Me.Controls.Add(Me.LblPurchaseNo)
        Me.Controls.Add(Me.DgvItemList)
        Me.Controls.Add(Me.LblPurchaseRemarks)
        Me.Controls.Add(Me.TxtPurchaseRemark)
        Me.Controls.Add(Me.DtpPurchaseDate)
        Me.Controls.Add(Me.TxtPurchaseAmount)
        Me.Controls.Add(Me.LblPurchaseAmount)
        Me.Controls.Add(Me.TxtItemCount)
        Me.Controls.Add(Me.LblItemCount)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtSupplierCode)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.DtpRegistrationDate)
        Me.Controls.Add(Me.BtnRegistration)
        Me.Controls.Add(Me.TxtAddress3)
        Me.Controls.Add(Me.TxtAddress2)
        Me.Controls.Add(Me.LblRegistrationDate)
        Me.Controls.Add(Me.TxtSales)
        Me.Controls.Add(Me.LblSales)
        Me.Controls.Add(Me.TxtOrderingSuffix)
        Me.Controls.Add(Me.TxtQuoteRemarks)
        Me.Controls.Add(Me.TxtPaymentTerms)
        Me.Controls.Add(Me.LblRemarks)
        Me.Controls.Add(Me.TxtPosition)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
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
    Friend WithEvents LblSupplierName As Label
    Friend WithEvents TxtOrderingNo As TextBox
    Friend WithEvents LblInput As Label
    Friend WithEvents LblPurchaseDate As Label
    Friend WithEvents LblPurchaseNo As Label
    Friend WithEvents DgvItemList As DataGridView
    Friend WithEvents LblPurchaseRemarks As Label
    Friend WithEvents TxtPurchaseRemark As TextBox
    Friend WithEvents DtpPurchaseDate As DateTimePicker
    Friend WithEvents TxtPurchaseAmount As TextBox
    Friend WithEvents LblPurchaseAmount As Label
    Friend WithEvents TxtItemCount As TextBox
    Friend WithEvents LblItemCount As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtSupplierCode As TextBox
    Friend WithEvents BtnBack As Button
    Friend WithEvents DtpRegistrationDate As DateTimePicker
    Friend WithEvents BtnRegistration As Button
    Friend WithEvents TxtAddress3 As TextBox
    Friend WithEvents TxtAddress2 As TextBox
    Friend WithEvents LblRegistrationDate As Label
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
    Friend WithEvents LblMode As Label
    Friend WithEvents TxtCustomerPO As TextBox
    Friend WithEvents LblCustomerPO As Label
    Friend WithEvents BtnCodeSearch As Button
    Friend WithEvents CbShippedBy As ComboBox
    Friend WithEvents LblMethod As Label
    Friend WithEvents DtpShippedDate As DateTimePicker
    Friend WithEvents LblShipDate As Label
End Class
