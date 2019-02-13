<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Supplier
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
        Me.LblMemo = New System.Windows.Forms.Label()
        Me.LblTariffRate = New System.Windows.Forms.Label()
        Me.LblPerson = New System.Windows.Forms.Label()
        Me.LblFax = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.LblTel = New System.Windows.Forms.Label()
        Me.TxtMemo = New System.Windows.Forms.TextBox()
        Me.TxtTariffRate = New System.Windows.Forms.TextBox()
        Me.TxtPerson = New System.Windows.Forms.TextBox()
        Me.TxtFax = New System.Windows.Forms.TextBox()
        Me.TxtTelSearch = New System.Windows.Forms.TextBox()
        Me.TxtTel = New System.Windows.Forms.TextBox()
        Me.BtnRegistration = New System.Windows.Forms.Button()
        Me.Lbladdress3 = New System.Windows.Forms.Label()
        Me.LblAddress2 = New System.Windows.Forms.Label()
        Me.LblAddress1 = New System.Windows.Forms.Label()
        Me.LblPostalCode = New System.Windows.Forms.Label()
        Me.LblShortName = New System.Windows.Forms.Label()
        Me.LblSupplierName = New System.Windows.Forms.Label()
        Me.LblSupplierCode = New System.Windows.Forms.Label()
        Me.TxtAddress3 = New System.Windows.Forms.TextBox()
        Me.TxtAddress2 = New System.Windows.Forms.TextBox()
        Me.TxtAddress1 = New System.Windows.Forms.TextBox()
        Me.TxtPostalCode = New System.Windows.Forms.TextBox()
        Me.TxtSupplierShortName = New System.Windows.Forms.TextBox()
        Me.TxtSupplierName = New System.Windows.Forms.TextBox()
        Me.TxtSupplierCode = New System.Windows.Forms.TextBox()
        Me.TxtCompanyCode = New System.Windows.Forms.TextBox()
        Me.LblAccountNumber = New System.Windows.Forms.Label()
        Me.LblDepositCategory = New System.Windows.Forms.Label()
        Me.LblBranchCode = New System.Windows.Forms.Label()
        Me.LblBankCode = New System.Windows.Forms.Label()
        Me.TxtAccountNumber = New System.Windows.Forms.TextBox()
        Me.TxtDepositCategory = New System.Windows.Forms.TextBox()
        Me.TxtBranchOfficeCode = New System.Windows.Forms.TextBox()
        Me.TxtBankCode = New System.Windows.Forms.TextBox()
        Me.LblAccountHolder = New System.Windows.Forms.Label()
        Me.TxtAccountName = New System.Windows.Forms.TextBox()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.LblPosition = New System.Windows.Forms.Label()
        Me.TxtPosition = New System.Windows.Forms.TextBox()
        Me.LblPostalCodeText = New System.Windows.Forms.Label()
        Me.LblTelText = New System.Windows.Forms.Label()
        Me.LblFaxText = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.LblTariffRateText = New System.Windows.Forms.Label()
        Me.LblPphText = New System.Windows.Forms.Label()
        Me.LblPph = New System.Windows.Forms.Label()
        Me.TxtPph = New System.Windows.Forms.TextBox()
        Me.LblTransportationCostText = New System.Windows.Forms.Label()
        Me.LblTransportationCost = New System.Windows.Forms.Label()
        Me.TxtTransportationCost = New System.Windows.Forms.TextBox()
        Me.LblBankName = New System.Windows.Forms.Label()
        Me.TxtBankName = New System.Windows.Forms.TextBox()
        Me.LblBranchName = New System.Windows.Forms.Label()
        Me.TxtBranchName = New System.Windows.Forms.TextBox()
        Me.LblBankCodeText = New System.Windows.Forms.Label()
        Me.LblBranchCodeText = New System.Windows.Forms.Label()
        Me.LblDepositCategoryText = New System.Windows.Forms.Label()
        Me.LblAccountNumberText = New System.Windows.Forms.Label()
        Me.LblSupplierCodeText = New System.Windows.Forms.Label()
        Me.TxtAccountingVendorCode = New System.Windows.Forms.TextBox()
        Me.LblAccountingVendorCode = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'LblMemo
        '
        Me.LblMemo.AutoSize = True
        Me.LblMemo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMemo.Location = New System.Drawing.Point(12, 331)
        Me.LblMemo.Name = "LblMemo"
        Me.LblMemo.Size = New System.Drawing.Size(28, 15)
        Me.LblMemo.TabIndex = 77
        Me.LblMemo.Text = "メモ"
        '
        'LblTariffRate
        '
        Me.LblTariffRate.AutoSize = True
        Me.LblTariffRate.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTariffRate.Location = New System.Drawing.Point(368, 241)
        Me.LblTariffRate.Name = "LblTariffRate"
        Me.LblTariffRate.Size = New System.Drawing.Size(52, 15)
        Me.LblTariffRate.TabIndex = 76
        Me.LblTariffRate.Text = "関税率"
        '
        'LblPerson
        '
        Me.LblPerson.AutoSize = True
        Me.LblPerson.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPerson.Location = New System.Drawing.Point(368, 196)
        Me.LblPerson.Name = "LblPerson"
        Me.LblPerson.Size = New System.Drawing.Size(67, 15)
        Me.LblPerson.TabIndex = 75
        Me.LblPerson.Text = "担当者名"
        '
        'LblFax
        '
        Me.LblFax.AutoSize = True
        Me.LblFax.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblFax.Location = New System.Drawing.Point(12, 196)
        Me.LblFax.Name = "LblFax"
        Me.LblFax.Size = New System.Drawing.Size(63, 15)
        Me.LblFax.TabIndex = 74
        Me.LblFax.Text = "FAX番号"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(873, 173)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(112, 15)
        Me.Label10.TabIndex = 73
        Me.Label10.Text = "電話番号検索用"
        '
        'LblTel
        '
        Me.LblTel.AutoSize = True
        Me.LblTel.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTel.Location = New System.Drawing.Point(368, 151)
        Me.LblTel.Name = "LblTel"
        Me.LblTel.Size = New System.Drawing.Size(67, 15)
        Me.LblTel.TabIndex = 72
        Me.LblTel.Text = "電話番号"
        '
        'TxtMemo
        '
        Me.TxtMemo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtMemo.Location = New System.Drawing.Point(128, 328)
        Me.TxtMemo.MaxLength = 255
        Me.TxtMemo.Name = "TxtMemo"
        Me.TxtMemo.Size = New System.Drawing.Size(234, 22)
        Me.TxtMemo.TabIndex = 15
        '
        'TxtTariffRate
        '
        Me.TxtTariffRate.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtTariffRate.Location = New System.Drawing.Point(483, 238)
        Me.TxtTariffRate.MaxLength = 6
        Me.TxtTariffRate.Name = "TxtTariffRate"
        Me.TxtTariffRate.Size = New System.Drawing.Size(234, 22)
        Me.TxtTariffRate.TabIndex = 12
        '
        'TxtPerson
        '
        Me.TxtPerson.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPerson.Location = New System.Drawing.Point(483, 193)
        Me.TxtPerson.MaxLength = 50
        Me.TxtPerson.Name = "TxtPerson"
        Me.TxtPerson.Size = New System.Drawing.Size(234, 22)
        Me.TxtPerson.TabIndex = 10
        '
        'TxtFax
        '
        Me.TxtFax.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtFax.Location = New System.Drawing.Point(128, 193)
        Me.TxtFax.MaxLength = 20
        Me.TxtFax.Name = "TxtFax"
        Me.TxtFax.Size = New System.Drawing.Size(234, 22)
        Me.TxtFax.TabIndex = 9
        '
        'TxtTelSearch
        '
        Me.TxtTelSearch.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtTelSearch.Location = New System.Drawing.Point(991, 170)
        Me.TxtTelSearch.Name = "TxtTelSearch"
        Me.TxtTelSearch.Size = New System.Drawing.Size(234, 22)
        Me.TxtTelSearch.TabIndex = 9
        Me.TxtTelSearch.TabStop = False
        '
        'TxtTel
        '
        Me.TxtTel.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtTel.Location = New System.Drawing.Point(483, 148)
        Me.TxtTel.MaxLength = 20
        Me.TxtTel.Name = "TxtTel"
        Me.TxtTel.Size = New System.Drawing.Size(234, 22)
        Me.TxtTel.TabIndex = 8
        '
        'BtnRegistration
        '
        Me.BtnRegistration.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnRegistration.Location = New System.Drawing.Point(381, 509)
        Me.BtnRegistration.Name = "BtnRegistration"
        Me.BtnRegistration.Size = New System.Drawing.Size(165, 40)
        Me.BtnRegistration.TabIndex = 24
        Me.BtnRegistration.Text = "登録"
        Me.BtnRegistration.UseVisualStyleBackColor = True
        '
        'Lbladdress3
        '
        Me.Lbladdress3.AutoSize = True
        Me.Lbladdress3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Lbladdress3.Location = New System.Drawing.Point(12, 155)
        Me.Lbladdress3.Name = "Lbladdress3"
        Me.Lbladdress3.Size = New System.Drawing.Size(47, 15)
        Me.Lbladdress3.TabIndex = 64
        Me.Lbladdress3.Text = "住所３"
        '
        'LblAddress2
        '
        Me.LblAddress2.AutoSize = True
        Me.LblAddress2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAddress2.Location = New System.Drawing.Point(368, 106)
        Me.LblAddress2.Name = "LblAddress2"
        Me.LblAddress2.Size = New System.Drawing.Size(47, 15)
        Me.LblAddress2.TabIndex = 63
        Me.LblAddress2.Text = "住所２"
        '
        'LblAddress1
        '
        Me.LblAddress1.AutoSize = True
        Me.LblAddress1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAddress1.Location = New System.Drawing.Point(12, 106)
        Me.LblAddress1.Name = "LblAddress1"
        Me.LblAddress1.Size = New System.Drawing.Size(47, 15)
        Me.LblAddress1.TabIndex = 62
        Me.LblAddress1.Text = "住所１"
        '
        'LblPostalCode
        '
        Me.LblPostalCode.AutoSize = True
        Me.LblPostalCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPostalCode.Location = New System.Drawing.Point(368, 60)
        Me.LblPostalCode.Name = "LblPostalCode"
        Me.LblPostalCode.Size = New System.Drawing.Size(67, 15)
        Me.LblPostalCode.TabIndex = 61
        Me.LblPostalCode.Text = "郵便番号"
        '
        'LblShortName
        '
        Me.LblShortName.AutoSize = True
        Me.LblShortName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblShortName.Location = New System.Drawing.Point(12, 60)
        Me.LblShortName.Name = "LblShortName"
        Me.LblShortName.Size = New System.Drawing.Size(97, 15)
        Me.LblShortName.TabIndex = 60
        Me.LblShortName.Text = "仕入先名略称"
        '
        'LblSupplierName
        '
        Me.LblSupplierName.AutoSize = True
        Me.LblSupplierName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSupplierName.Location = New System.Drawing.Point(368, 15)
        Me.LblSupplierName.Name = "LblSupplierName"
        Me.LblSupplierName.Size = New System.Drawing.Size(67, 15)
        Me.LblSupplierName.TabIndex = 59
        Me.LblSupplierName.Text = "仕入先名"
        '
        'LblSupplierCode
        '
        Me.LblSupplierCode.AutoSize = True
        Me.LblSupplierCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSupplierCode.Location = New System.Drawing.Point(12, 15)
        Me.LblSupplierCode.Name = "LblSupplierCode"
        Me.LblSupplierCode.Size = New System.Drawing.Size(85, 15)
        Me.LblSupplierCode.TabIndex = 58
        Me.LblSupplierCode.Text = "仕入先コード"
        '
        'TxtAddress3
        '
        Me.TxtAddress3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAddress3.Location = New System.Drawing.Point(128, 148)
        Me.TxtAddress3.MaxLength = 100
        Me.TxtAddress3.Name = "TxtAddress3"
        Me.TxtAddress3.Size = New System.Drawing.Size(234, 22)
        Me.TxtAddress3.TabIndex = 7
        '
        'TxtAddress2
        '
        Me.TxtAddress2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAddress2.Location = New System.Drawing.Point(483, 103)
        Me.TxtAddress2.MaxLength = 100
        Me.TxtAddress2.Name = "TxtAddress2"
        Me.TxtAddress2.Size = New System.Drawing.Size(234, 22)
        Me.TxtAddress2.TabIndex = 6
        '
        'TxtAddress1
        '
        Me.TxtAddress1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAddress1.Location = New System.Drawing.Point(128, 103)
        Me.TxtAddress1.MaxLength = 100
        Me.TxtAddress1.Name = "TxtAddress1"
        Me.TxtAddress1.Size = New System.Drawing.Size(234, 22)
        Me.TxtAddress1.TabIndex = 5
        '
        'TxtPostalCode
        '
        Me.TxtPostalCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPostalCode.Location = New System.Drawing.Point(483, 57)
        Me.TxtPostalCode.MaxLength = 7
        Me.TxtPostalCode.Name = "TxtPostalCode"
        Me.TxtPostalCode.Size = New System.Drawing.Size(234, 22)
        Me.TxtPostalCode.TabIndex = 4
        '
        'TxtSupplierShortName
        '
        Me.TxtSupplierShortName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSupplierShortName.Location = New System.Drawing.Point(128, 57)
        Me.TxtSupplierShortName.MaxLength = 30
        Me.TxtSupplierShortName.Name = "TxtSupplierShortName"
        Me.TxtSupplierShortName.Size = New System.Drawing.Size(234, 22)
        Me.TxtSupplierShortName.TabIndex = 3
        '
        'TxtSupplierName
        '
        Me.TxtSupplierName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSupplierName.Location = New System.Drawing.Point(483, 12)
        Me.TxtSupplierName.MaxLength = 100
        Me.TxtSupplierName.Name = "TxtSupplierName"
        Me.TxtSupplierName.Size = New System.Drawing.Size(234, 22)
        Me.TxtSupplierName.TabIndex = 2
        '
        'TxtSupplierCode
        '
        Me.TxtSupplierCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSupplierCode.Location = New System.Drawing.Point(128, 12)
        Me.TxtSupplierCode.MaxLength = 8
        Me.TxtSupplierCode.Name = "TxtSupplierCode"
        Me.TxtSupplierCode.Size = New System.Drawing.Size(234, 22)
        Me.TxtSupplierCode.TabIndex = 1
        '
        'TxtCompanyCode
        '
        Me.TxtCompanyCode.Location = New System.Drawing.Point(758, 6)
        Me.TxtCompanyCode.Name = "TxtCompanyCode"
        Me.TxtCompanyCode.Size = New System.Drawing.Size(26, 19)
        Me.TxtCompanyCode.TabIndex = 49
        Me.TxtCompanyCode.TabStop = False
        Me.TxtCompanyCode.Visible = False
        '
        'LblAccountNumber
        '
        Me.LblAccountNumber.AutoSize = True
        Me.LblAccountNumber.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAccountNumber.Location = New System.Drawing.Point(12, 463)
        Me.LblAccountNumber.Name = "LblAccountNumber"
        Me.LblAccountNumber.Size = New System.Drawing.Size(67, 15)
        Me.LblAccountNumber.TabIndex = 85
        Me.LblAccountNumber.Text = "口座番号"
        '
        'LblDepositCategory
        '
        Me.LblDepositCategory.AutoSize = True
        Me.LblDepositCategory.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblDepositCategory.Location = New System.Drawing.Point(368, 421)
        Me.LblDepositCategory.Name = "LblDepositCategory"
        Me.LblDepositCategory.Size = New System.Drawing.Size(67, 15)
        Me.LblDepositCategory.TabIndex = 84
        Me.LblDepositCategory.Text = "預金種目"
        '
        'LblBranchCode
        '
        Me.LblBranchCode.AutoSize = True
        Me.LblBranchCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblBranchCode.Location = New System.Drawing.Point(12, 421)
        Me.LblBranchCode.Name = "LblBranchCode"
        Me.LblBranchCode.Size = New System.Drawing.Size(70, 15)
        Me.LblBranchCode.TabIndex = 83
        Me.LblBranchCode.Text = "支店コード"
        '
        'LblBankCode
        '
        Me.LblBankCode.AutoSize = True
        Me.LblBankCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblBankCode.Location = New System.Drawing.Point(12, 376)
        Me.LblBankCode.Name = "LblBankCode"
        Me.LblBankCode.Size = New System.Drawing.Size(70, 15)
        Me.LblBankCode.TabIndex = 82
        Me.LblBankCode.Text = "銀行コード"
        '
        'TxtAccountNumber
        '
        Me.TxtAccountNumber.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAccountNumber.Location = New System.Drawing.Point(128, 463)
        Me.TxtAccountNumber.MaxLength = 20
        Me.TxtAccountNumber.Name = "TxtAccountNumber"
        Me.TxtAccountNumber.Size = New System.Drawing.Size(234, 22)
        Me.TxtAccountNumber.TabIndex = 21
        '
        'TxtDepositCategory
        '
        Me.TxtDepositCategory.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtDepositCategory.Location = New System.Drawing.Point(483, 418)
        Me.TxtDepositCategory.MaxLength = 1
        Me.TxtDepositCategory.Name = "TxtDepositCategory"
        Me.TxtDepositCategory.Size = New System.Drawing.Size(234, 22)
        Me.TxtDepositCategory.TabIndex = 20
        '
        'TxtBranchOfficeCode
        '
        Me.TxtBranchOfficeCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtBranchOfficeCode.Location = New System.Drawing.Point(128, 418)
        Me.TxtBranchOfficeCode.MaxLength = 3
        Me.TxtBranchOfficeCode.Name = "TxtBranchOfficeCode"
        Me.TxtBranchOfficeCode.Size = New System.Drawing.Size(234, 22)
        Me.TxtBranchOfficeCode.TabIndex = 19
        '
        'TxtBankCode
        '
        Me.TxtBankCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtBankCode.Location = New System.Drawing.Point(128, 373)
        Me.TxtBankCode.MaxLength = 4
        Me.TxtBankCode.Name = "TxtBankCode"
        Me.TxtBankCode.Size = New System.Drawing.Size(234, 22)
        Me.TxtBankCode.TabIndex = 17
        '
        'LblAccountHolder
        '
        Me.LblAccountHolder.AutoSize = True
        Me.LblAccountHolder.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAccountHolder.Location = New System.Drawing.Point(368, 466)
        Me.LblAccountHolder.Name = "LblAccountHolder"
        Me.LblAccountHolder.Size = New System.Drawing.Size(67, 15)
        Me.LblAccountHolder.TabIndex = 87
        Me.LblAccountHolder.Text = "口座名義"
        '
        'TxtAccountName
        '
        Me.TxtAccountName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAccountName.Location = New System.Drawing.Point(483, 463)
        Me.TxtAccountName.MaxLength = 50
        Me.TxtAccountName.Name = "TxtAccountName"
        Me.TxtAccountName.Size = New System.Drawing.Size(234, 22)
        Me.TxtAccountName.TabIndex = 22
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(552, 509)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 25
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'LblPosition
        '
        Me.LblPosition.AutoSize = True
        Me.LblPosition.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPosition.Location = New System.Drawing.Point(12, 241)
        Me.LblPosition.Name = "LblPosition"
        Me.LblPosition.Size = New System.Drawing.Size(82, 15)
        Me.LblPosition.TabIndex = 90
        Me.LblPosition.Text = "担当者役職"
        '
        'TxtPosition
        '
        Me.TxtPosition.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPosition.Location = New System.Drawing.Point(128, 238)
        Me.TxtPosition.MaxLength = 20
        Me.TxtPosition.Name = "TxtPosition"
        Me.TxtPosition.Size = New System.Drawing.Size(234, 22)
        Me.TxtPosition.TabIndex = 11
        '
        'LblPostalCodeText
        '
        Me.LblPostalCodeText.AutoSize = True
        Me.LblPostalCodeText.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPostalCodeText.Location = New System.Drawing.Point(486, 82)
        Me.LblPostalCodeText.Name = "LblPostalCodeText"
        Me.LblPostalCodeText.Size = New System.Drawing.Size(96, 15)
        Me.LblPostalCodeText.TabIndex = 217
        Me.LblPostalCodeText.Text = "(例：0123456)"
        '
        'LblTelText
        '
        Me.LblTelText.AutoSize = True
        Me.LblTelText.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTelText.Location = New System.Drawing.Point(486, 173)
        Me.LblTelText.Name = "LblTelText"
        Me.LblTelText.Size = New System.Drawing.Size(120, 15)
        Me.LblTelText.TabIndex = 219
        Me.LblTelText.Text = "(例：0123456789)"
        '
        'LblFaxText
        '
        Me.LblFaxText.AutoSize = True
        Me.LblFaxText.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblFaxText.Location = New System.Drawing.Point(131, 218)
        Me.LblFaxText.Name = "LblFaxText"
        Me.LblFaxText.Size = New System.Drawing.Size(120, 15)
        Me.LblFaxText.TabIndex = 220
        Me.LblFaxText.Text = "(例：0123456789)"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label24.Location = New System.Drawing.Point(994, 195)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(40, 15)
        Me.Label24.TabIndex = 221
        Me.Label24.Text = "(例：)"
        '
        'LblTariffRateText
        '
        Me.LblTariffRateText.AutoSize = True
        Me.LblTariffRateText.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTariffRateText.Location = New System.Drawing.Point(486, 263)
        Me.LblTariffRateText.Name = "LblTariffRateText"
        Me.LblTariffRateText.Size = New System.Drawing.Size(67, 15)
        Me.LblTariffRateText.TabIndex = 222
        Me.LblTariffRateText.Text = "(例：0.01)"
        '
        'LblPphText
        '
        Me.LblPphText.AutoSize = True
        Me.LblPphText.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPphText.Location = New System.Drawing.Point(131, 308)
        Me.LblPphText.Name = "LblPphText"
        Me.LblPphText.Size = New System.Drawing.Size(75, 15)
        Me.LblPphText.TabIndex = 233
        Me.LblPphText.Text = "(例：0.025)"
        '
        'LblPph
        '
        Me.LblPph.AutoSize = True
        Me.LblPph.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPph.Location = New System.Drawing.Point(12, 286)
        Me.LblPph.Name = "LblPph"
        Me.LblPph.Size = New System.Drawing.Size(97, 15)
        Me.LblPph.TabIndex = 232
        Me.LblPph.Text = "前払法人税率"
        '
        'TxtPph
        '
        Me.TxtPph.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPph.Location = New System.Drawing.Point(128, 283)
        Me.TxtPph.MaxLength = 6
        Me.TxtPph.Name = "TxtPph"
        Me.TxtPph.Size = New System.Drawing.Size(234, 22)
        Me.TxtPph.TabIndex = 13
        '
        'LblTransportationCostText
        '
        Me.LblTransportationCostText.AutoSize = True
        Me.LblTransportationCostText.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTransportationCostText.Location = New System.Drawing.Point(486, 308)
        Me.LblTransportationCostText.Name = "LblTransportationCostText"
        Me.LblTransportationCostText.Size = New System.Drawing.Size(59, 15)
        Me.LblTransportationCostText.TabIndex = 236
        Me.LblTransportationCostText.Text = "(例：0.1)"
        '
        'LblTransportationCost
        '
        Me.LblTransportationCost.AutoSize = True
        Me.LblTransportationCost.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTransportationCost.Location = New System.Drawing.Point(368, 286)
        Me.LblTransportationCost.Name = "LblTransportationCost"
        Me.LblTransportationCost.Size = New System.Drawing.Size(67, 15)
        Me.LblTransportationCost.TabIndex = 235
        Me.LblTransportationCost.Text = "輸送費率"
        '
        'TxtTransportationCost
        '
        Me.TxtTransportationCost.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtTransportationCost.Location = New System.Drawing.Point(483, 283)
        Me.TxtTransportationCost.MaxLength = 6
        Me.TxtTransportationCost.Name = "TxtTransportationCost"
        Me.TxtTransportationCost.Size = New System.Drawing.Size(234, 22)
        Me.TxtTransportationCost.TabIndex = 14
        '
        'LblBankName
        '
        Me.LblBankName.AutoSize = True
        Me.LblBankName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblBankName.Location = New System.Drawing.Point(368, 331)
        Me.LblBankName.Name = "LblBankName"
        Me.LblBankName.Size = New System.Drawing.Size(52, 15)
        Me.LblBankName.TabIndex = 239
        Me.LblBankName.Text = "銀行名"
        '
        'TxtBankName
        '
        Me.TxtBankName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtBankName.Location = New System.Drawing.Point(483, 328)
        Me.TxtBankName.MaxLength = 50
        Me.TxtBankName.Name = "TxtBankName"
        Me.TxtBankName.Size = New System.Drawing.Size(234, 22)
        Me.TxtBankName.TabIndex = 16
        '
        'LblBranchName
        '
        Me.LblBranchName.AutoSize = True
        Me.LblBranchName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblBranchName.Location = New System.Drawing.Point(368, 376)
        Me.LblBranchName.Name = "LblBranchName"
        Me.LblBranchName.Size = New System.Drawing.Size(52, 15)
        Me.LblBranchName.TabIndex = 242
        Me.LblBranchName.Text = "支店名"
        '
        'TxtBranchName
        '
        Me.TxtBranchName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtBranchName.Location = New System.Drawing.Point(483, 373)
        Me.TxtBranchName.MaxLength = 50
        Me.TxtBranchName.Name = "TxtBranchName"
        Me.TxtBranchName.Size = New System.Drawing.Size(234, 22)
        Me.TxtBranchName.TabIndex = 18
        '
        'LblBankCodeText
        '
        Me.LblBankCodeText.AutoSize = True
        Me.LblBankCodeText.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblBankCodeText.Location = New System.Drawing.Point(131, 398)
        Me.LblBankCodeText.Name = "LblBankCodeText"
        Me.LblBankCodeText.Size = New System.Drawing.Size(72, 15)
        Me.LblBankCodeText.TabIndex = 243
        Me.LblBankCodeText.Text = "(例：0123)"
        '
        'LblBranchCodeText
        '
        Me.LblBranchCodeText.AutoSize = True
        Me.LblBranchCodeText.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblBranchCodeText.Location = New System.Drawing.Point(131, 443)
        Me.LblBranchCodeText.Name = "LblBranchCodeText"
        Me.LblBranchCodeText.Size = New System.Drawing.Size(64, 15)
        Me.LblBranchCodeText.TabIndex = 244
        Me.LblBranchCodeText.Text = "(例：012)"
        '
        'LblDepositCategoryText
        '
        Me.LblDepositCategoryText.AutoSize = True
        Me.LblDepositCategoryText.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblDepositCategoryText.Location = New System.Drawing.Point(486, 443)
        Me.LblDepositCategoryText.Name = "LblDepositCategoryText"
        Me.LblDepositCategoryText.Size = New System.Drawing.Size(127, 15)
        Me.LblDepositCategoryText.TabIndex = 245
        Me.LblDepositCategoryText.Text = "(例：1:普通 2:預金)"
        '
        'LblAccountNumberText
        '
        Me.LblAccountNumberText.AutoSize = True
        Me.LblAccountNumberText.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAccountNumberText.Location = New System.Drawing.Point(131, 488)
        Me.LblAccountNumberText.Name = "LblAccountNumberText"
        Me.LblAccountNumberText.Size = New System.Drawing.Size(96, 15)
        Me.LblAccountNumberText.TabIndex = 246
        Me.LblAccountNumberText.Text = "(例：0123456)"
        '
        'LblSupplierCodeText
        '
        Me.LblSupplierCodeText.AutoSize = True
        Me.LblSupplierCodeText.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSupplierCodeText.Location = New System.Drawing.Point(131, 37)
        Me.LblSupplierCodeText.Name = "LblSupplierCodeText"
        Me.LblSupplierCodeText.Size = New System.Drawing.Size(230, 15)
        Me.LblSupplierCodeText.TabIndex = 247
        Me.LblSupplierCodeText.Text = "(他仕入先コードと重複しない文字列)"
        '
        'TxtAccountingVendorCode
        '
        Me.TxtAccountingVendorCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAccountingVendorCode.Location = New System.Drawing.Point(127, 509)
        Me.TxtAccountingVendorCode.MaxLength = 20
        Me.TxtAccountingVendorCode.Name = "TxtAccountingVendorCode"
        Me.TxtAccountingVendorCode.Size = New System.Drawing.Size(234, 22)
        Me.TxtAccountingVendorCode.TabIndex = 23
        '
        'LblAccountingVendorCode
        '
        Me.LblAccountingVendorCode.AutoSize = True
        Me.LblAccountingVendorCode.Font = New System.Drawing.Font("MS UI Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAccountingVendorCode.Location = New System.Drawing.Point(12, 509)
        Me.LblAccountingVendorCode.Name = "LblAccountingVendorCode"
        Me.LblAccountingVendorCode.Size = New System.Drawing.Size(96, 11)
        Me.LblAccountingVendorCode.TabIndex = 248
        Me.LblAccountingVendorCode.Text = "会計用仕入先コード"
        '
        'Supplier
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(729, 561)
        Me.Controls.Add(Me.LblAccountingVendorCode)
        Me.Controls.Add(Me.TxtAccountingVendorCode)
        Me.Controls.Add(Me.LblSupplierCodeText)
        Me.Controls.Add(Me.LblAccountNumberText)
        Me.Controls.Add(Me.LblDepositCategoryText)
        Me.Controls.Add(Me.LblBranchCodeText)
        Me.Controls.Add(Me.LblBankCodeText)
        Me.Controls.Add(Me.LblBranchName)
        Me.Controls.Add(Me.TxtBranchName)
        Me.Controls.Add(Me.LblBankName)
        Me.Controls.Add(Me.TxtBankName)
        Me.Controls.Add(Me.LblTransportationCostText)
        Me.Controls.Add(Me.LblTransportationCost)
        Me.Controls.Add(Me.TxtTransportationCost)
        Me.Controls.Add(Me.LblPphText)
        Me.Controls.Add(Me.LblPph)
        Me.Controls.Add(Me.TxtPph)
        Me.Controls.Add(Me.LblTariffRateText)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.LblFaxText)
        Me.Controls.Add(Me.LblTelText)
        Me.Controls.Add(Me.LblPostalCodeText)
        Me.Controls.Add(Me.LblPosition)
        Me.Controls.Add(Me.TxtPosition)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.LblAccountHolder)
        Me.Controls.Add(Me.TxtAccountName)
        Me.Controls.Add(Me.LblAccountNumber)
        Me.Controls.Add(Me.LblDepositCategory)
        Me.Controls.Add(Me.LblBranchCode)
        Me.Controls.Add(Me.LblBankCode)
        Me.Controls.Add(Me.TxtAccountNumber)
        Me.Controls.Add(Me.TxtDepositCategory)
        Me.Controls.Add(Me.TxtBranchOfficeCode)
        Me.Controls.Add(Me.TxtBankCode)
        Me.Controls.Add(Me.LblMemo)
        Me.Controls.Add(Me.LblTariffRate)
        Me.Controls.Add(Me.LblPerson)
        Me.Controls.Add(Me.LblFax)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.LblTel)
        Me.Controls.Add(Me.TxtMemo)
        Me.Controls.Add(Me.TxtTariffRate)
        Me.Controls.Add(Me.TxtPerson)
        Me.Controls.Add(Me.TxtFax)
        Me.Controls.Add(Me.TxtTelSearch)
        Me.Controls.Add(Me.TxtTel)
        Me.Controls.Add(Me.BtnRegistration)
        Me.Controls.Add(Me.Lbladdress3)
        Me.Controls.Add(Me.LblAddress2)
        Me.Controls.Add(Me.LblAddress1)
        Me.Controls.Add(Me.LblPostalCode)
        Me.Controls.Add(Me.LblShortName)
        Me.Controls.Add(Me.LblSupplierName)
        Me.Controls.Add(Me.LblSupplierCode)
        Me.Controls.Add(Me.TxtAddress3)
        Me.Controls.Add(Me.TxtAddress2)
        Me.Controls.Add(Me.TxtAddress1)
        Me.Controls.Add(Me.TxtPostalCode)
        Me.Controls.Add(Me.TxtSupplierShortName)
        Me.Controls.Add(Me.TxtSupplierName)
        Me.Controls.Add(Me.TxtSupplierCode)
        Me.Controls.Add(Me.TxtCompanyCode)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Supplier"
        Me.Text = "SupplierAdd"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LblMemo As Label
    Friend WithEvents LblTariffRate As Label
    Friend WithEvents LblPerson As Label
    Friend WithEvents LblFax As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents LblTel As Label
    Friend WithEvents TxtMemo As TextBox
    Friend WithEvents TxtTariffRate As TextBox
    Friend WithEvents TxtPerson As TextBox
    Friend WithEvents TxtFax As TextBox
    Friend WithEvents TxtTelSearch As TextBox
    Friend WithEvents TxtTel As TextBox
    Friend WithEvents BtnRegistration As Button
    Friend WithEvents Lbladdress3 As Label
    Friend WithEvents LblAddress2 As Label
    Friend WithEvents LblAddress1 As Label
    Friend WithEvents LblPostalCode As Label
    Friend WithEvents LblShortName As Label
    Friend WithEvents LblSupplierName As Label
    Friend WithEvents LblSupplierCode As Label
    Friend WithEvents TxtAddress3 As TextBox
    Friend WithEvents TxtAddress2 As TextBox
    Friend WithEvents TxtAddress1 As TextBox
    Friend WithEvents TxtPostalCode As TextBox
    Friend WithEvents TxtSupplierShortName As TextBox
    Friend WithEvents TxtSupplierName As TextBox
    Friend WithEvents TxtSupplierCode As TextBox
    Friend WithEvents TxtCompanyCode As TextBox
    Friend WithEvents LblAccountNumber As Label
    Friend WithEvents LblDepositCategory As Label
    Friend WithEvents LblBranchCode As Label
    Friend WithEvents LblBankCode As Label
    Friend WithEvents TxtAccountNumber As TextBox
    Friend WithEvents TxtDepositCategory As TextBox
    Friend WithEvents TxtBranchOfficeCode As TextBox
    Friend WithEvents TxtBankCode As TextBox
    Friend WithEvents LblAccountHolder As Label
    Friend WithEvents TxtAccountName As TextBox
    Friend WithEvents BtnBack As Button
    Friend WithEvents LblPosition As Label
    Friend WithEvents TxtPosition As TextBox
    Friend WithEvents LblPostalCodeText As Label
    Friend WithEvents LblTelText As Label
    Friend WithEvents LblFaxText As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents LblTariffRateText As Label
    Friend WithEvents LblPphText As Label
    Friend WithEvents LblPph As Label
    Friend WithEvents TxtPph As TextBox
    Friend WithEvents LblTransportationCostText As Label
    Friend WithEvents LblTransportationCost As Label
    Friend WithEvents TxtTransportationCost As TextBox
    Friend WithEvents LblBankName As Label
    Friend WithEvents TxtBankName As TextBox
    Friend WithEvents LblBranchName As Label
    Friend WithEvents TxtBranchName As TextBox
    Friend WithEvents LblBankCodeText As Label
    Friend WithEvents LblBranchCodeText As Label
    Friend WithEvents LblDepositCategoryText As Label
    Friend WithEvents LblAccountNumberText As Label
    Friend WithEvents LblSupplierCodeText As Label
    Friend WithEvents TxtAccountingVendorCode As TextBox
    Friend WithEvents LblAccountingVendorCode As Label
End Class
