<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Quote
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
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.LblQuoteNo = New System.Windows.Forms.Label()
        Me.LblQuote = New System.Windows.Forms.Label()
        Me.TxtQuoteNo = New System.Windows.Forms.TextBox()
        Me.LblPaymentTerms = New System.Windows.Forms.Label()
        Me.TxtPaymentTerms = New System.Windows.Forms.TextBox()
        Me.DgvItemList = New System.Windows.Forms.DataGridView()
        Me.LblInput = New System.Windows.Forms.Label()
        Me.TxtCustomerName = New System.Windows.Forms.TextBox()
        Me.LblCustomerName = New System.Windows.Forms.Label()
        Me.LblAddress = New System.Windows.Forms.Label()
        Me.TxtPostalCode1 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtPostalCode2 = New System.Windows.Forms.TextBox()
        Me.TxtAddress1 = New System.Windows.Forms.TextBox()
        Me.LblFax = New System.Windows.Forms.Label()
        Me.LblPerson = New System.Windows.Forms.Label()
        Me.LblTel = New System.Windows.Forms.Label()
        Me.TxtPosition = New System.Windows.Forms.TextBox()
        Me.TxtTel = New System.Windows.Forms.TextBox()
        Me.TxtPerson = New System.Windows.Forms.TextBox()
        Me.TxtFax = New System.Windows.Forms.TextBox()
        Me.LblPosition = New System.Windows.Forms.Label()
        Me.TxtInput = New System.Windows.Forms.TextBox()
        Me.BtnRowsAdd = New System.Windows.Forms.Button()
        Me.BtnRowsDel = New System.Windows.Forms.Button()
        Me.BtnInsert = New System.Windows.Forms.Button()
        Me.BtnUp = New System.Windows.Forms.Button()
        Me.BtnDown = New System.Windows.Forms.Button()
        Me.BtnClone = New System.Windows.Forms.Button()
        Me.TxtRemarks = New System.Windows.Forms.TextBox()
        Me.LblRemarks = New System.Windows.Forms.Label()
        Me.TxtSuffixNo = New System.Windows.Forms.TextBox()
        Me.TxtSales = New System.Windows.Forms.TextBox()
        Me.LblSales = New System.Windows.Forms.Label()
        Me.LblRegistration = New System.Windows.Forms.Label()
        Me.LblExpiration = New System.Windows.Forms.Label()
        Me.TxtAddress2 = New System.Windows.Forms.TextBox()
        Me.TxtAddress3 = New System.Windows.Forms.TextBox()
        Me.TxtTotal = New System.Windows.Forms.TextBox()
        Me.LblTotal = New System.Windows.Forms.Label()
        Me.BtnRegistration = New System.Windows.Forms.Button()
        Me.DtpRegistration = New System.Windows.Forms.DateTimePicker()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.TxtCustomerCode = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtItemCount = New System.Windows.Forms.TextBox()
        Me.LblItemCount = New System.Windows.Forms.Label()
        Me.TxtGrossProfit = New System.Windows.Forms.TextBox()
        Me.LblGrossProfit = New System.Windows.Forms.Label()
        Me.TxtPurchaseTotal = New System.Windows.Forms.TextBox()
        Me.LblStockOrder = New System.Windows.Forms.Label()
        Me.DtpQuote = New System.Windows.Forms.DateTimePicker()
        Me.DtpExpiration = New System.Windows.Forms.DateTimePicker()
        Me.No = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.メーカー = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.品名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.型式 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.数量 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.単位 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入先 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入単価 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.間接費率 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.間接費 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入金額 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.売単価 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.売上金額 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.粗利額 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.粗利率 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.リードタイム = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.備考 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ステータス = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DgvItemList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LblQuoteNo
        '
        Me.LblQuoteNo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblQuoteNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblQuoteNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblQuoteNo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblQuoteNo.Location = New System.Drawing.Point(12, 15)
        Me.LblQuoteNo.Name = "LblQuoteNo"
        Me.LblQuoteNo.Size = New System.Drawing.Size(110, 23)
        Me.LblQuoteNo.TabIndex = 2
        Me.LblQuoteNo.Text = "見積番号"
        Me.LblQuoteNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblQuote
        '
        Me.LblQuote.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblQuote.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblQuote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblQuote.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblQuote.Location = New System.Drawing.Point(546, 15)
        Me.LblQuote.Name = "LblQuote"
        Me.LblQuote.Size = New System.Drawing.Size(112, 23)
        Me.LblQuote.TabIndex = 9
        Me.LblQuote.Text = "見積日"
        Me.LblQuote.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtQuoteNo
        '
        Me.TxtQuoteNo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtQuoteNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtQuoteNo.Enabled = False
        Me.TxtQuoteNo.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtQuoteNo.Location = New System.Drawing.Point(128, 15)
        Me.TxtQuoteNo.MaxLength = 8
        Me.TxtQuoteNo.Name = "TxtQuoteNo"
        Me.TxtQuoteNo.ReadOnly = True
        Me.TxtQuoteNo.Size = New System.Drawing.Size(88, 23)
        Me.TxtQuoteNo.TabIndex = 3
        Me.TxtQuoteNo.TabStop = False
        '
        'LblPaymentTerms
        '
        Me.LblPaymentTerms.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblPaymentTerms.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblPaymentTerms.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblPaymentTerms.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPaymentTerms.Location = New System.Drawing.Point(12, 179)
        Me.LblPaymentTerms.Name = "LblPaymentTerms"
        Me.LblPaymentTerms.Size = New System.Drawing.Size(110, 23)
        Me.LblPaymentTerms.TabIndex = 1
        Me.LblPaymentTerms.Text = "支払条件"
        Me.LblPaymentTerms.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtPaymentTerms
        '
        Me.TxtPaymentTerms.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtPaymentTerms.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtPaymentTerms.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPaymentTerms.Location = New System.Drawing.Point(128, 179)
        Me.TxtPaymentTerms.MaxLength = 50
        Me.TxtPaymentTerms.Multiline = True
        Me.TxtPaymentTerms.Name = "TxtPaymentTerms"
        Me.TxtPaymentTerms.Size = New System.Drawing.Size(476, 23)
        Me.TxtPaymentTerms.TabIndex = 15
        '
        'DgvItemList
        '
        Me.DgvItemList.AllowUserToAddRows = False
        Me.DgvItemList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DgvItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvItemList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.No, Me.メーカー, Me.品名, Me.型式, Me.数量, Me.単位, Me.仕入先, Me.仕入単価, Me.間接費率, Me.間接費, Me.仕入金額, Me.売単価, Me.売上金額, Me.粗利額, Me.粗利率, Me.リードタイム, Me.備考, Me.ステータス})
        Me.DgvItemList.Location = New System.Drawing.Point(12, 237)
        Me.DgvItemList.Name = "DgvItemList"
        Me.DgvItemList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DgvItemList.RowTemplate.Height = 21
        Me.DgvItemList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvItemList.Size = New System.Drawing.Size(1329, 337)
        Me.DgvItemList.TabIndex = 17
        '
        'LblInput
        '
        Me.LblInput.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblInput.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblInput.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblInput.Location = New System.Drawing.Point(972, 92)
        Me.LblInput.Name = "LblInput"
        Me.LblInput.Size = New System.Drawing.Size(150, 23)
        Me.LblInput.TabIndex = 10
        Me.LblInput.Text = "入力担当者"
        Me.LblInput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtCustomerName
        '
        Me.TxtCustomerName.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtCustomerName.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerName.Location = New System.Drawing.Point(274, 63)
        Me.TxtCustomerName.MaxLength = 50
        Me.TxtCustomerName.Name = "TxtCustomerName"
        Me.TxtCustomerName.Size = New System.Drawing.Size(330, 23)
        Me.TxtCustomerName.TabIndex = 4
        '
        'LblCustomerName
        '
        Me.LblCustomerName.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblCustomerName.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblCustomerName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCustomerName.Location = New System.Drawing.Point(12, 63)
        Me.LblCustomerName.Name = "LblCustomerName"
        Me.LblCustomerName.Size = New System.Drawing.Size(110, 23)
        Me.LblCustomerName.TabIndex = 1
        Me.LblCustomerName.Text = "得意先名称"
        Me.LblCustomerName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblAddress
        '
        Me.LblAddress.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblAddress.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblAddress.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAddress.Location = New System.Drawing.Point(12, 92)
        Me.LblAddress.Name = "LblAddress"
        Me.LblAddress.Size = New System.Drawing.Size(110, 81)
        Me.LblAddress.TabIndex = 8
        Me.LblAddress.Text = "住所"
        Me.LblAddress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtPostalCode1
        '
        Me.TxtPostalCode1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtPostalCode1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtPostalCode1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPostalCode1.Location = New System.Drawing.Point(128, 92)
        Me.TxtPostalCode1.MaxLength = 3
        Me.TxtPostalCode1.Name = "TxtPostalCode1"
        Me.TxtPostalCode1.Size = New System.Drawing.Size(54, 23)
        Me.TxtPostalCode1.TabIndex = 5
        Me.TxtPostalCode1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(188, 99)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(11, 12)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "-"
        '
        'TxtPostalCode2
        '
        Me.TxtPostalCode2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtPostalCode2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtPostalCode2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPostalCode2.Location = New System.Drawing.Point(205, 92)
        Me.TxtPostalCode2.MaxLength = 4
        Me.TxtPostalCode2.Name = "TxtPostalCode2"
        Me.TxtPostalCode2.Size = New System.Drawing.Size(63, 23)
        Me.TxtPostalCode2.TabIndex = 6
        Me.TxtPostalCode2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TxtAddress1
        '
        Me.TxtAddress1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtAddress1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAddress1.Location = New System.Drawing.Point(274, 92)
        Me.TxtAddress1.MaxLength = 100
        Me.TxtAddress1.Name = "TxtAddress1"
        Me.TxtAddress1.Size = New System.Drawing.Size(330, 23)
        Me.TxtAddress1.TabIndex = 7
        '
        'LblFax
        '
        Me.LblFax.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblFax.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblFax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblFax.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblFax.Location = New System.Drawing.Point(610, 92)
        Me.LblFax.Name = "LblFax"
        Me.LblFax.Size = New System.Drawing.Size(150, 23)
        Me.LblFax.TabIndex = 2
        Me.LblFax.Text = "FAX番号"
        Me.LblFax.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblPerson
        '
        Me.LblPerson.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblPerson.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblPerson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblPerson.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPerson.Location = New System.Drawing.Point(610, 121)
        Me.LblPerson.Name = "LblPerson"
        Me.LblPerson.Size = New System.Drawing.Size(150, 23)
        Me.LblPerson.TabIndex = 5
        Me.LblPerson.Text = "得意先担当者名"
        Me.LblPerson.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblTel
        '
        Me.LblTel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblTel.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblTel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblTel.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTel.Location = New System.Drawing.Point(610, 63)
        Me.LblTel.Name = "LblTel"
        Me.LblTel.Size = New System.Drawing.Size(150, 23)
        Me.LblTel.TabIndex = 1
        Me.LblTel.Text = "電話番号"
        Me.LblTel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtPosition
        '
        Me.TxtPosition.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtPosition.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPosition.Location = New System.Drawing.Point(766, 150)
        Me.TxtPosition.MaxLength = 50
        Me.TxtPosition.Name = "TxtPosition"
        Me.TxtPosition.Size = New System.Drawing.Size(200, 23)
        Me.TxtPosition.TabIndex = 13
        '
        'TxtTel
        '
        Me.TxtTel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtTel.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtTel.Location = New System.Drawing.Point(766, 63)
        Me.TxtTel.MaxLength = 15
        Me.TxtTel.Name = "TxtTel"
        Me.TxtTel.Size = New System.Drawing.Size(200, 23)
        Me.TxtTel.TabIndex = 10
        '
        'TxtPerson
        '
        Me.TxtPerson.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtPerson.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPerson.Location = New System.Drawing.Point(766, 121)
        Me.TxtPerson.MaxLength = 50
        Me.TxtPerson.Name = "TxtPerson"
        Me.TxtPerson.Size = New System.Drawing.Size(200, 23)
        Me.TxtPerson.TabIndex = 12
        '
        'TxtFax
        '
        Me.TxtFax.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtFax.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtFax.Location = New System.Drawing.Point(766, 92)
        Me.TxtFax.MaxLength = 15
        Me.TxtFax.Name = "TxtFax"
        Me.TxtFax.Size = New System.Drawing.Size(200, 23)
        Me.TxtFax.TabIndex = 11
        '
        'LblPosition
        '
        Me.LblPosition.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblPosition.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblPosition.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPosition.Location = New System.Drawing.Point(610, 150)
        Me.LblPosition.Name = "LblPosition"
        Me.LblPosition.Size = New System.Drawing.Size(150, 23)
        Me.LblPosition.TabIndex = 6
        Me.LblPosition.Text = "得意先担当者役職"
        Me.LblPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtInput
        '
        Me.TxtInput.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtInput.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtInput.Enabled = False
        Me.TxtInput.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtInput.Location = New System.Drawing.Point(1128, 92)
        Me.TxtInput.MaxLength = 20
        Me.TxtInput.Name = "TxtInput"
        Me.TxtInput.ReadOnly = True
        Me.TxtInput.Size = New System.Drawing.Size(200, 23)
        Me.TxtInput.TabIndex = 6
        Me.TxtInput.TabStop = False
        '
        'BtnRowsAdd
        '
        Me.BtnRowsAdd.Location = New System.Drawing.Point(12, 636)
        Me.BtnRowsAdd.Name = "BtnRowsAdd"
        Me.BtnRowsAdd.Size = New System.Drawing.Size(120, 40)
        Me.BtnRowsAdd.TabIndex = 0
        Me.BtnRowsAdd.TabStop = False
        Me.BtnRowsAdd.Text = "行追加"
        Me.BtnRowsAdd.UseVisualStyleBackColor = True
        '
        'BtnRowsDel
        '
        Me.BtnRowsDel.Location = New System.Drawing.Point(135, 636)
        Me.BtnRowsDel.Name = "BtnRowsDel"
        Me.BtnRowsDel.Size = New System.Drawing.Size(120, 40)
        Me.BtnRowsDel.TabIndex = 0
        Me.BtnRowsDel.TabStop = False
        Me.BtnRowsDel.Text = "行削除"
        Me.BtnRowsDel.UseVisualStyleBackColor = True
        '
        'BtnInsert
        '
        Me.BtnInsert.Location = New System.Drawing.Point(12, 590)
        Me.BtnInsert.Name = "BtnInsert"
        Me.BtnInsert.Size = New System.Drawing.Size(120, 40)
        Me.BtnInsert.TabIndex = 0
        Me.BtnInsert.TabStop = False
        Me.BtnInsert.Text = "行挿入"
        Me.BtnInsert.UseVisualStyleBackColor = True
        '
        'BtnUp
        '
        Me.BtnUp.Location = New System.Drawing.Point(138, 590)
        Me.BtnUp.Name = "BtnUp"
        Me.BtnUp.Size = New System.Drawing.Size(120, 40)
        Me.BtnUp.TabIndex = 0
        Me.BtnUp.TabStop = False
        Me.BtnUp.Text = "行移動↑"
        Me.BtnUp.UseVisualStyleBackColor = True
        '
        'BtnDown
        '
        Me.BtnDown.Location = New System.Drawing.Point(264, 590)
        Me.BtnDown.Name = "BtnDown"
        Me.BtnDown.Size = New System.Drawing.Size(120, 40)
        Me.BtnDown.TabIndex = 0
        Me.BtnDown.TabStop = False
        Me.BtnDown.Text = "行移動↓"
        Me.BtnDown.UseVisualStyleBackColor = True
        '
        'BtnClone
        '
        Me.BtnClone.Location = New System.Drawing.Point(264, 636)
        Me.BtnClone.Name = "BtnClone"
        Me.BtnClone.Size = New System.Drawing.Size(120, 40)
        Me.BtnClone.TabIndex = 0
        Me.BtnClone.TabStop = False
        Me.BtnClone.Text = "行複製"
        Me.BtnClone.UseVisualStyleBackColor = True
        '
        'TxtRemarks
        '
        Me.TxtRemarks.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtRemarks.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtRemarks.Location = New System.Drawing.Point(128, 208)
        Me.TxtRemarks.MaxLength = 100
        Me.TxtRemarks.Multiline = True
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(476, 23)
        Me.TxtRemarks.TabIndex = 16
        '
        'LblRemarks
        '
        Me.LblRemarks.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblRemarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblRemarks.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblRemarks.Location = New System.Drawing.Point(12, 208)
        Me.LblRemarks.Name = "LblRemarks"
        Me.LblRemarks.Size = New System.Drawing.Size(110, 23)
        Me.LblRemarks.TabIndex = 1
        Me.LblRemarks.Text = "備考"
        Me.LblRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtSuffixNo
        '
        Me.TxtSuffixNo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtSuffixNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtSuffixNo.Enabled = False
        Me.TxtSuffixNo.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSuffixNo.Location = New System.Drawing.Point(239, 15)
        Me.TxtSuffixNo.MaxLength = 1
        Me.TxtSuffixNo.Name = "TxtSuffixNo"
        Me.TxtSuffixNo.ReadOnly = True
        Me.TxtSuffixNo.Size = New System.Drawing.Size(29, 23)
        Me.TxtSuffixNo.TabIndex = 4
        Me.TxtSuffixNo.TabStop = False
        '
        'TxtSales
        '
        Me.TxtSales.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtSales.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtSales.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSales.Location = New System.Drawing.Point(1128, 63)
        Me.TxtSales.MaxLength = 20
        Me.TxtSales.Name = "TxtSales"
        Me.TxtSales.Size = New System.Drawing.Size(200, 23)
        Me.TxtSales.TabIndex = 14
        '
        'LblSales
        '
        Me.LblSales.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblSales.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblSales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblSales.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSales.Location = New System.Drawing.Point(972, 63)
        Me.LblSales.Name = "LblSales"
        Me.LblSales.Size = New System.Drawing.Size(150, 23)
        Me.LblSales.TabIndex = 26
        Me.LblSales.Text = "営業担当者"
        Me.LblSales.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblRegistration
        '
        Me.LblRegistration.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblRegistration.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblRegistration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblRegistration.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblRegistration.Location = New System.Drawing.Point(274, 15)
        Me.LblRegistration.Name = "LblRegistration"
        Me.LblRegistration.Size = New System.Drawing.Size(110, 23)
        Me.LblRegistration.TabIndex = 28
        Me.LblRegistration.Text = "登録日"
        Me.LblRegistration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblExpiration
        '
        Me.LblExpiration.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblExpiration.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblExpiration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblExpiration.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblExpiration.Location = New System.Drawing.Point(820, 15)
        Me.LblExpiration.Name = "LblExpiration"
        Me.LblExpiration.Size = New System.Drawing.Size(110, 23)
        Me.LblExpiration.TabIndex = 30
        Me.LblExpiration.Text = "見積有効期限"
        Me.LblExpiration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtAddress2
        '
        Me.TxtAddress2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtAddress2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAddress2.Location = New System.Drawing.Point(274, 121)
        Me.TxtAddress2.MaxLength = 100
        Me.TxtAddress2.Name = "TxtAddress2"
        Me.TxtAddress2.Size = New System.Drawing.Size(330, 23)
        Me.TxtAddress2.TabIndex = 8
        '
        'TxtAddress3
        '
        Me.TxtAddress3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtAddress3.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAddress3.Location = New System.Drawing.Point(274, 150)
        Me.TxtAddress3.MaxLength = 100
        Me.TxtAddress3.Name = "TxtAddress3"
        Me.TxtAddress3.Size = New System.Drawing.Size(330, 23)
        Me.TxtAddress3.TabIndex = 9
        '
        'TxtTotal
        '
        Me.TxtTotal.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtTotal.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtTotal.Enabled = False
        Me.TxtTotal.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtTotal.Location = New System.Drawing.Point(1110, 619)
        Me.TxtTotal.MaxLength = 10
        Me.TxtTotal.Name = "TxtTotal"
        Me.TxtTotal.ReadOnly = True
        Me.TxtTotal.Size = New System.Drawing.Size(231, 23)
        Me.TxtTotal.TabIndex = 21
        Me.TxtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LblTotal
        '
        Me.LblTotal.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblTotal.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblTotal.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTotal.Location = New System.Drawing.Point(1004, 619)
        Me.LblTotal.Name = "LblTotal"
        Me.LblTotal.Size = New System.Drawing.Size(100, 23)
        Me.LblTotal.TabIndex = 33
        Me.LblTotal.Text = "見積金額"
        Me.LblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnRegistration
        '
        Me.BtnRegistration.Location = New System.Drawing.Point(1004, 677)
        Me.BtnRegistration.Name = "BtnRegistration"
        Me.BtnRegistration.Size = New System.Drawing.Size(165, 40)
        Me.BtnRegistration.TabIndex = 18
        Me.BtnRegistration.Text = "登録"
        Me.BtnRegistration.UseVisualStyleBackColor = True
        '
        'DtpRegistration
        '
        Me.DtpRegistration.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpRegistration.CustomFormat = "yyyy/MM/dd"
        Me.DtpRegistration.Enabled = False
        Me.DtpRegistration.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpRegistration.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpRegistration.Location = New System.Drawing.Point(390, 15)
        Me.DtpRegistration.Name = "DtpRegistration"
        Me.DtpRegistration.Size = New System.Drawing.Size(150, 22)
        Me.DtpRegistration.TabIndex = 1
        Me.DtpRegistration.TabStop = False
        Me.DtpRegistration.Value = New Date(2018, 6, 5, 23, 23, 58, 0)
        '
        'BtnBack
        '
        Me.BtnBack.Location = New System.Drawing.Point(1176, 677)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 19
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'TxtCustomerCode
        '
        Me.TxtCustomerCode.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtCustomerCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TxtCustomerCode.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerCode.Location = New System.Drawing.Point(128, 63)
        Me.TxtCustomerCode.MaxLength = 50
        Me.TxtCustomerCode.Name = "TxtCustomerCode"
        Me.TxtCustomerCode.Size = New System.Drawing.Size(140, 23)
        Me.TxtCustomerCode.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(222, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(11, 12)
        Me.Label2.TabIndex = 37
        Me.Label2.Text = "-"
        '
        'TxtItemCount
        '
        Me.TxtItemCount.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtItemCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtItemCount.Enabled = False
        Me.TxtItemCount.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtItemCount.Location = New System.Drawing.Point(1275, 208)
        Me.TxtItemCount.MaxLength = 20
        Me.TxtItemCount.Name = "TxtItemCount"
        Me.TxtItemCount.ReadOnly = True
        Me.TxtItemCount.Size = New System.Drawing.Size(66, 23)
        Me.TxtItemCount.TabIndex = 38
        Me.TxtItemCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LblItemCount
        '
        Me.LblItemCount.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblItemCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblItemCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblItemCount.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblItemCount.Location = New System.Drawing.Point(1275, 179)
        Me.LblItemCount.Name = "LblItemCount"
        Me.LblItemCount.Size = New System.Drawing.Size(66, 23)
        Me.LblItemCount.TabIndex = 39
        Me.LblItemCount.Text = "明細数"
        Me.LblItemCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtGrossProfit
        '
        Me.TxtGrossProfit.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtGrossProfit.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtGrossProfit.Enabled = False
        Me.TxtGrossProfit.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtGrossProfit.Location = New System.Drawing.Point(1110, 648)
        Me.TxtGrossProfit.MaxLength = 10
        Me.TxtGrossProfit.Name = "TxtGrossProfit"
        Me.TxtGrossProfit.ReadOnly = True
        Me.TxtGrossProfit.Size = New System.Drawing.Size(231, 23)
        Me.TxtGrossProfit.TabIndex = 40
        Me.TxtGrossProfit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LblGrossProfit
        '
        Me.LblGrossProfit.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblGrossProfit.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblGrossProfit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblGrossProfit.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblGrossProfit.Location = New System.Drawing.Point(1004, 648)
        Me.LblGrossProfit.Name = "LblGrossProfit"
        Me.LblGrossProfit.Size = New System.Drawing.Size(100, 23)
        Me.LblGrossProfit.TabIndex = 41
        Me.LblGrossProfit.Text = "粗利額"
        Me.LblGrossProfit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtPurchaseTotal
        '
        Me.TxtPurchaseTotal.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtPurchaseTotal.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtPurchaseTotal.Enabled = False
        Me.TxtPurchaseTotal.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPurchaseTotal.Location = New System.Drawing.Point(1110, 590)
        Me.TxtPurchaseTotal.MaxLength = 10
        Me.TxtPurchaseTotal.Name = "TxtPurchaseTotal"
        Me.TxtPurchaseTotal.ReadOnly = True
        Me.TxtPurchaseTotal.Size = New System.Drawing.Size(231, 23)
        Me.TxtPurchaseTotal.TabIndex = 42
        Me.TxtPurchaseTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LblStockOrder
        '
        Me.LblStockOrder.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblStockOrder.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblStockOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblStockOrder.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblStockOrder.Location = New System.Drawing.Point(1004, 590)
        Me.LblStockOrder.Name = "LblStockOrder"
        Me.LblStockOrder.Size = New System.Drawing.Size(100, 23)
        Me.LblStockOrder.TabIndex = 43
        Me.LblStockOrder.Text = "仕入金額"
        Me.LblStockOrder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DtpQuote
        '
        Me.DtpQuote.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpQuote.CustomFormat = "yyyy/MM/dd"
        Me.DtpQuote.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpQuote.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpQuote.Location = New System.Drawing.Point(664, 16)
        Me.DtpQuote.Name = "DtpQuote"
        Me.DtpQuote.Size = New System.Drawing.Size(150, 22)
        Me.DtpQuote.TabIndex = 44
        Me.DtpQuote.TabStop = False
        Me.DtpQuote.Value = New Date(2018, 6, 5, 23, 23, 58, 0)
        '
        'DtpExpiration
        '
        Me.DtpExpiration.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpExpiration.CustomFormat = "yyyy/MM/dd"
        Me.DtpExpiration.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpExpiration.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpExpiration.Location = New System.Drawing.Point(936, 16)
        Me.DtpExpiration.Name = "DtpExpiration"
        Me.DtpExpiration.Size = New System.Drawing.Size(140, 22)
        Me.DtpExpiration.TabIndex = 45
        Me.DtpExpiration.TabStop = False
        Me.DtpExpiration.Value = New Date(2018, 6, 5, 23, 23, 58, 0)
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
        Me.仕入先.Width = 85
        '
        '仕入単価
        '
        Me.仕入単価.HeaderText = "仕入単価"
        Me.仕入単価.Name = "仕入単価"
        Me.仕入単価.Width = 80
        '
        '間接費率
        '
        Me.間接費率.HeaderText = "間接費率"
        Me.間接費率.Name = "間接費率"
        Me.間接費率.Width = 85
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
        '売単価
        '
        Me.売単価.HeaderText = "売単価"
        Me.売単価.Name = "売単価"
        Me.売単価.Width = 80
        '
        '売上金額
        '
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.売上金額.DefaultCellStyle = DataGridViewCellStyle7
        Me.売上金額.HeaderText = "売上金額"
        Me.売上金額.Name = "売上金額"
        Me.売上金額.ReadOnly = True
        '
        '粗利額
        '
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.粗利額.DefaultCellStyle = DataGridViewCellStyle8
        Me.粗利額.HeaderText = "粗利額"
        Me.粗利額.Name = "粗利額"
        Me.粗利額.ReadOnly = True
        '
        '粗利率
        '
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.粗利率.DefaultCellStyle = DataGridViewCellStyle9
        Me.粗利率.HeaderText = "粗利率"
        Me.粗利率.Name = "粗利率"
        Me.粗利率.ReadOnly = True
        '
        'リードタイム
        '
        Me.リードタイム.HeaderText = "リードタイム"
        Me.リードタイム.Name = "リードタイム"
        '
        '備考
        '
        Me.備考.HeaderText = "備考"
        Me.備考.Name = "備考"
        Me.備考.Width = 80
        '
        'ステータス
        '
        Me.ステータス.HeaderText = "ステータス"
        Me.ステータス.Name = "ステータス"
        Me.ステータス.Visible = False
        '
        'Quote
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1350, 729)
        Me.Controls.Add(Me.DtpExpiration)
        Me.Controls.Add(Me.DtpQuote)
        Me.Controls.Add(Me.TxtPurchaseTotal)
        Me.Controls.Add(Me.LblStockOrder)
        Me.Controls.Add(Me.TxtGrossProfit)
        Me.Controls.Add(Me.LblGrossProfit)
        Me.Controls.Add(Me.TxtItemCount)
        Me.Controls.Add(Me.LblItemCount)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtCustomerCode)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.DtpRegistration)
        Me.Controls.Add(Me.BtnRegistration)
        Me.Controls.Add(Me.TxtTotal)
        Me.Controls.Add(Me.LblTotal)
        Me.Controls.Add(Me.TxtAddress3)
        Me.Controls.Add(Me.TxtAddress2)
        Me.Controls.Add(Me.LblExpiration)
        Me.Controls.Add(Me.LblRegistration)
        Me.Controls.Add(Me.TxtSales)
        Me.Controls.Add(Me.LblSales)
        Me.Controls.Add(Me.TxtSuffixNo)
        Me.Controls.Add(Me.TxtRemarks)
        Me.Controls.Add(Me.TxtPaymentTerms)
        Me.Controls.Add(Me.LblRemarks)
        Me.Controls.Add(Me.LblPaymentTerms)
        Me.Controls.Add(Me.TxtPosition)
        Me.Controls.Add(Me.TxtPostalCode2)
        Me.Controls.Add(Me.LblPosition)
        Me.Controls.Add(Me.TxtTel)
        Me.Controls.Add(Me.LblPerson)
        Me.Controls.Add(Me.TxtAddress1)
        Me.Controls.Add(Me.TxtPerson)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.LblFax)
        Me.Controls.Add(Me.TxtFax)
        Me.Controls.Add(Me.TxtPostalCode1)
        Me.Controls.Add(Me.LblTel)
        Me.Controls.Add(Me.LblAddress)
        Me.Controls.Add(Me.TxtCustomerName)
        Me.Controls.Add(Me.TxtInput)
        Me.Controls.Add(Me.LblCustomerName)
        Me.Controls.Add(Me.TxtQuoteNo)
        Me.Controls.Add(Me.LblInput)
        Me.Controls.Add(Me.LblQuote)
        Me.Controls.Add(Me.LblQuoteNo)
        Me.Controls.Add(Me.BtnClone)
        Me.Controls.Add(Me.BtnDown)
        Me.Controls.Add(Me.BtnUp)
        Me.Controls.Add(Me.BtnInsert)
        Me.Controls.Add(Me.BtnRowsDel)
        Me.Controls.Add(Me.BtnRowsAdd)
        Me.Controls.Add(Me.DgvItemList)
        Me.Name = "Quote"
        Me.Text = "QuoteRequest"
        CType(Me.DgvItemList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LblQuoteNo As Label
    Friend WithEvents TxtQuoteNo As TextBox
    Friend WithEvents LblPaymentTerms As Label
    Friend WithEvents TxtPaymentTerms As TextBox
    Friend WithEvents DgvItemList As DataGridView
    Friend WithEvents LblQuote As Label
    Friend WithEvents LblInput As Label
    Friend WithEvents TxtCustomerName As TextBox
    Friend WithEvents LblCustomerName As Label
    Friend WithEvents LblAddress As Label
    Friend WithEvents LblFax As Label
    Friend WithEvents LblPerson As Label
    Friend WithEvents LblTel As Label
    Friend WithEvents TxtPostalCode1 As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TxtPostalCode2 As TextBox
    Friend WithEvents TxtAddress1 As TextBox
    Friend WithEvents TxtPosition As TextBox
    Friend WithEvents TxtTel As TextBox
    Friend WithEvents TxtPerson As TextBox
    Friend WithEvents TxtFax As TextBox
    Friend WithEvents LblPosition As Label
    Friend WithEvents TxtInput As TextBox
    Friend WithEvents BtnRowsAdd As Button
    Friend WithEvents BtnRowsDel As Button
    Friend WithEvents BtnInsert As Button
    Friend WithEvents BtnUp As Button
    Friend WithEvents BtnDown As Button
    Friend WithEvents BtnClone As Button
    Friend WithEvents TxtRemarks As TextBox
    Friend WithEvents LblRemarks As Label
    Friend WithEvents TxtSuffixNo As TextBox
    Friend WithEvents TxtSales As TextBox
    Friend WithEvents LblSales As Label
    Friend WithEvents LblRegistration As Label
    Friend WithEvents LblExpiration As Label
    Friend WithEvents TxtAddress2 As TextBox
    Friend WithEvents TxtAddress3 As TextBox
    Friend WithEvents TxtTotal As TextBox
    Friend WithEvents LblTotal As Label
    Friend WithEvents BtnRegistration As Button
    Friend WithEvents DtpRegistration As DateTimePicker
    Friend WithEvents BtnBack As Button
    Friend WithEvents TxtCustomerCode As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtItemCount As TextBox
    Friend WithEvents LblItemCount As Label
    Friend WithEvents TxtGrossProfit As TextBox
    Friend WithEvents LblGrossProfit As Label
    Friend WithEvents TxtPurchaseTotal As TextBox
    Friend WithEvents LblStockOrder As Label
    Friend WithEvents DtpQuote As DateTimePicker
    Friend WithEvents DtpExpiration As DateTimePicker
    Friend WithEvents No As DataGridViewTextBoxColumn
    Friend WithEvents メーカー As DataGridViewTextBoxColumn
    Friend WithEvents 品名 As DataGridViewTextBoxColumn
    Friend WithEvents 型式 As DataGridViewTextBoxColumn
    Friend WithEvents 数量 As DataGridViewTextBoxColumn
    Friend WithEvents 単位 As DataGridViewTextBoxColumn
    Friend WithEvents 仕入先 As DataGridViewTextBoxColumn
    Friend WithEvents 仕入単価 As DataGridViewTextBoxColumn
    Friend WithEvents 間接費率 As DataGridViewTextBoxColumn
    Friend WithEvents 間接費 As DataGridViewTextBoxColumn
    Friend WithEvents 仕入金額 As DataGridViewTextBoxColumn
    Friend WithEvents 売単価 As DataGridViewTextBoxColumn
    Friend WithEvents 売上金額 As DataGridViewTextBoxColumn
    Friend WithEvents 粗利額 As DataGridViewTextBoxColumn
    Friend WithEvents 粗利率 As DataGridViewTextBoxColumn
    Friend WithEvents リードタイム As DataGridViewTextBoxColumn
    Friend WithEvents 備考 As DataGridViewTextBoxColumn
    Friend WithEvents ステータス As DataGridViewTextBoxColumn
End Class
