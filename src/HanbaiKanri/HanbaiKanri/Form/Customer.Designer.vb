<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Customer
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
        Me.btnRegistrarion = New System.Windows.Forms.Button()
        Me.LblAddress3 = New System.Windows.Forms.Label()
        Me.LblAddress2 = New System.Windows.Forms.Label()
        Me.LblAddress1 = New System.Windows.Forms.Label()
        Me.LblPostalCode = New System.Windows.Forms.Label()
        Me.LblCustomerShortName = New System.Windows.Forms.Label()
        Me.LblCustomerName = New System.Windows.Forms.Label()
        Me.LblCustomerCode = New System.Windows.Forms.Label()
        Me.TxtAddress3 = New System.Windows.Forms.TextBox()
        Me.TxtAddress2 = New System.Windows.Forms.TextBox()
        Me.TxtAddress1 = New System.Windows.Forms.TextBox()
        Me.TxtPostalCode = New System.Windows.Forms.TextBox()
        Me.TxtCustomerShortName = New System.Windows.Forms.TextBox()
        Me.TxtCustomerName = New System.Windows.Forms.TextBox()
        Me.TxtCustomerCode = New System.Windows.Forms.TextBox()
        Me.LblMemo = New System.Windows.Forms.Label()
        Me.LblPaymentTerms = New System.Windows.Forms.Label()
        Me.LblPerson = New System.Windows.Forms.Label()
        Me.LblFax = New System.Windows.Forms.Label()
        Me.LblTelSearch = New System.Windows.Forms.Label()
        Me.LblTEL = New System.Windows.Forms.Label()
        Me.TxtMemo = New System.Windows.Forms.TextBox()
        Me.TxtPaymentTerms = New System.Windows.Forms.TextBox()
        Me.TxtPerson = New System.Windows.Forms.TextBox()
        Me.TxtFax = New System.Windows.Forms.TextBox()
        Me.TxtTelSearch = New System.Windows.Forms.TextBox()
        Me.TxtTel = New System.Windows.Forms.TextBox()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.LblPosition = New System.Windows.Forms.Label()
        Me.TxtPosition = New System.Windows.Forms.TextBox()
        Me.TxtCompanyCode = New System.Windows.Forms.TextBox()
        Me.LblPostalCodeEx = New System.Windows.Forms.Label()
        Me.LblTelEx = New System.Windows.Forms.Label()
        Me.LblFaxEx = New System.Windows.Forms.Label()
        Me.LblTelSearchEx = New System.Windows.Forms.Label()
        Me.LblCustomerCodeRemarks = New System.Windows.Forms.Label()
        Me.LblAccountingVendorCode = New System.Windows.Forms.Label()
        Me.TxtAccountingVendorCode = New System.Windows.Forms.TextBox()
        Me.cmDomesticKbn = New System.Windows.Forms.ComboBox()
        Me.LblDomesticKbn = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnRegistrarion
        '
        Me.btnRegistrarion.Location = New System.Drawing.Point(381, 509)
        Me.btnRegistrarion.Name = "btnRegistrarion"
        Me.btnRegistrarion.Size = New System.Drawing.Size(165, 40)
        Me.btnRegistrarion.TabIndex = 16
        Me.btnRegistrarion.Text = "登録"
        Me.btnRegistrarion.UseVisualStyleBackColor = True
        '
        'LblAddress3
        '
        Me.LblAddress3.AutoSize = True
        Me.LblAddress3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAddress3.Location = New System.Drawing.Point(12, 150)
        Me.LblAddress3.Name = "LblAddress3"
        Me.LblAddress3.Size = New System.Drawing.Size(47, 15)
        Me.LblAddress3.TabIndex = 33
        Me.LblAddress3.Text = "住所３"
        '
        'LblAddress2
        '
        Me.LblAddress2.AutoSize = True
        Me.LblAddress2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAddress2.Location = New System.Drawing.Point(370, 105)
        Me.LblAddress2.Name = "LblAddress2"
        Me.LblAddress2.Size = New System.Drawing.Size(47, 15)
        Me.LblAddress2.TabIndex = 32
        Me.LblAddress2.Text = "住所２"
        '
        'LblAddress1
        '
        Me.LblAddress1.AutoSize = True
        Me.LblAddress1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAddress1.Location = New System.Drawing.Point(12, 105)
        Me.LblAddress1.Name = "LblAddress1"
        Me.LblAddress1.Size = New System.Drawing.Size(47, 15)
        Me.LblAddress1.TabIndex = 31
        Me.LblAddress1.Text = "住所１"
        '
        'LblPostalCode
        '
        Me.LblPostalCode.AutoSize = True
        Me.LblPostalCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPostalCode.Location = New System.Drawing.Point(370, 60)
        Me.LblPostalCode.Name = "LblPostalCode"
        Me.LblPostalCode.Size = New System.Drawing.Size(67, 15)
        Me.LblPostalCode.TabIndex = 30
        Me.LblPostalCode.Text = "郵便番号"
        '
        'LblCustomerShortName
        '
        Me.LblCustomerShortName.AutoSize = True
        Me.LblCustomerShortName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCustomerShortName.Location = New System.Drawing.Point(12, 60)
        Me.LblCustomerShortName.Name = "LblCustomerShortName"
        Me.LblCustomerShortName.Size = New System.Drawing.Size(97, 15)
        Me.LblCustomerShortName.TabIndex = 29
        Me.LblCustomerShortName.Text = "得意先名略称"
        '
        'LblCustomerName
        '
        Me.LblCustomerName.AutoSize = True
        Me.LblCustomerName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCustomerName.Location = New System.Drawing.Point(370, 15)
        Me.LblCustomerName.Name = "LblCustomerName"
        Me.LblCustomerName.Size = New System.Drawing.Size(67, 15)
        Me.LblCustomerName.TabIndex = 28
        Me.LblCustomerName.Text = "得意先名"
        '
        'LblCustomerCode
        '
        Me.LblCustomerCode.AutoSize = True
        Me.LblCustomerCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCustomerCode.Location = New System.Drawing.Point(12, 15)
        Me.LblCustomerCode.Name = "LblCustomerCode"
        Me.LblCustomerCode.Size = New System.Drawing.Size(85, 15)
        Me.LblCustomerCode.TabIndex = 27
        Me.LblCustomerCode.Text = "得意先コード"
        '
        'TxtAddress3
        '
        Me.TxtAddress3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAddress3.Location = New System.Drawing.Point(130, 147)
        Me.TxtAddress3.MaxLength = 100
        Me.TxtAddress3.Name = "TxtAddress3"
        Me.TxtAddress3.Size = New System.Drawing.Size(234, 22)
        Me.TxtAddress3.TabIndex = 7
        '
        'TxtAddress2
        '
        Me.TxtAddress2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAddress2.Location = New System.Drawing.Point(483, 102)
        Me.TxtAddress2.MaxLength = 100
        Me.TxtAddress2.Name = "TxtAddress2"
        Me.TxtAddress2.Size = New System.Drawing.Size(234, 22)
        Me.TxtAddress2.TabIndex = 6
        '
        'TxtAddress1
        '
        Me.TxtAddress1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAddress1.Location = New System.Drawing.Point(130, 102)
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
        'TxtCustomerShortName
        '
        Me.TxtCustomerShortName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerShortName.Location = New System.Drawing.Point(130, 57)
        Me.TxtCustomerShortName.MaxLength = 30
        Me.TxtCustomerShortName.Name = "TxtCustomerShortName"
        Me.TxtCustomerShortName.Size = New System.Drawing.Size(234, 22)
        Me.TxtCustomerShortName.TabIndex = 3
        '
        'TxtCustomerName
        '
        Me.TxtCustomerName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerName.Location = New System.Drawing.Point(483, 12)
        Me.TxtCustomerName.MaxLength = 100
        Me.TxtCustomerName.Name = "TxtCustomerName"
        Me.TxtCustomerName.Size = New System.Drawing.Size(234, 22)
        Me.TxtCustomerName.TabIndex = 2
        '
        'TxtCustomerCode
        '
        Me.TxtCustomerCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerCode.Location = New System.Drawing.Point(130, 12)
        Me.TxtCustomerCode.MaxLength = 8
        Me.TxtCustomerCode.Name = "TxtCustomerCode"
        Me.TxtCustomerCode.Size = New System.Drawing.Size(234, 22)
        Me.TxtCustomerCode.TabIndex = 1
        '
        'LblMemo
        '
        Me.LblMemo.AutoSize = True
        Me.LblMemo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMemo.Location = New System.Drawing.Point(370, 286)
        Me.LblMemo.Name = "LblMemo"
        Me.LblMemo.Size = New System.Drawing.Size(28, 15)
        Me.LblMemo.TabIndex = 48
        Me.LblMemo.Text = "メモ"
        '
        'LblPaymentTerms
        '
        Me.LblPaymentTerms.AutoSize = True
        Me.LblPaymentTerms.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPaymentTerms.Location = New System.Drawing.Point(12, 286)
        Me.LblPaymentTerms.Name = "LblPaymentTerms"
        Me.LblPaymentTerms.Size = New System.Drawing.Size(97, 15)
        Me.LblPaymentTerms.TabIndex = 47
        Me.LblPaymentTerms.Text = "既定支払条件"
        '
        'LblPerson
        '
        Me.LblPerson.AutoSize = True
        Me.LblPerson.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPerson.Location = New System.Drawing.Point(12, 241)
        Me.LblPerson.Name = "LblPerson"
        Me.LblPerson.Size = New System.Drawing.Size(67, 15)
        Me.LblPerson.TabIndex = 46
        Me.LblPerson.Text = "担当者名"
        '
        'LblFax
        '
        Me.LblFax.AutoSize = True
        Me.LblFax.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblFax.Location = New System.Drawing.Point(370, 195)
        Me.LblFax.Name = "LblFax"
        Me.LblFax.Size = New System.Drawing.Size(63, 15)
        Me.LblFax.TabIndex = 45
        Me.LblFax.Text = "FAX番号"
        '
        'LblTelSearch
        '
        Me.LblTelSearch.AutoSize = True
        Me.LblTelSearch.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTelSearch.Location = New System.Drawing.Point(12, 195)
        Me.LblTelSearch.Name = "LblTelSearch"
        Me.LblTelSearch.Size = New System.Drawing.Size(112, 15)
        Me.LblTelSearch.TabIndex = 44
        Me.LblTelSearch.Text = "電話番号検索用"
        '
        'LblTEL
        '
        Me.LblTEL.AutoSize = True
        Me.LblTEL.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTEL.Location = New System.Drawing.Point(370, 150)
        Me.LblTEL.Name = "LblTEL"
        Me.LblTEL.Size = New System.Drawing.Size(67, 15)
        Me.LblTEL.TabIndex = 43
        Me.LblTEL.Text = "電話番号"
        '
        'TxtMemo
        '
        Me.TxtMemo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtMemo.Location = New System.Drawing.Point(483, 283)
        Me.TxtMemo.MaxLength = 255
        Me.TxtMemo.Name = "TxtMemo"
        Me.TxtMemo.Size = New System.Drawing.Size(234, 22)
        Me.TxtMemo.TabIndex = 14
        '
        'TxtPaymentTerms
        '
        Me.TxtPaymentTerms.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPaymentTerms.Location = New System.Drawing.Point(130, 283)
        Me.TxtPaymentTerms.MaxLength = 50
        Me.TxtPaymentTerms.Name = "TxtPaymentTerms"
        Me.TxtPaymentTerms.Size = New System.Drawing.Size(234, 22)
        Me.TxtPaymentTerms.TabIndex = 13
        '
        'TxtPerson
        '
        Me.TxtPerson.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPerson.Location = New System.Drawing.Point(130, 238)
        Me.TxtPerson.MaxLength = 50
        Me.TxtPerson.Name = "TxtPerson"
        Me.TxtPerson.Size = New System.Drawing.Size(234, 22)
        Me.TxtPerson.TabIndex = 11
        '
        'TxtFax
        '
        Me.TxtFax.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtFax.Location = New System.Drawing.Point(483, 192)
        Me.TxtFax.MaxLength = 20
        Me.TxtFax.Name = "TxtFax"
        Me.TxtFax.Size = New System.Drawing.Size(234, 22)
        Me.TxtFax.TabIndex = 10
        '
        'TxtTelSearch
        '
        Me.TxtTelSearch.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtTelSearch.Location = New System.Drawing.Point(130, 192)
        Me.TxtTelSearch.MaxLength = 20
        Me.TxtTelSearch.Name = "TxtTelSearch"
        Me.TxtTelSearch.Size = New System.Drawing.Size(234, 22)
        Me.TxtTelSearch.TabIndex = 9
        '
        'TxtTel
        '
        Me.TxtTel.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtTel.Location = New System.Drawing.Point(483, 147)
        Me.TxtTel.MaxLength = 20
        Me.TxtTel.Name = "TxtTel"
        Me.TxtTel.Size = New System.Drawing.Size(234, 22)
        Me.TxtTel.TabIndex = 8
        '
        'btnBack
        '
        Me.btnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.Location = New System.Drawing.Point(552, 509)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(165, 40)
        Me.btnBack.TabIndex = 17
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'LblPosition
        '
        Me.LblPosition.AutoSize = True
        Me.LblPosition.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPosition.Location = New System.Drawing.Point(370, 241)
        Me.LblPosition.Name = "LblPosition"
        Me.LblPosition.Size = New System.Drawing.Size(82, 15)
        Me.LblPosition.TabIndex = 51
        Me.LblPosition.Text = "担当者役職"
        '
        'TxtPosition
        '
        Me.TxtPosition.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPosition.Location = New System.Drawing.Point(483, 238)
        Me.TxtPosition.MaxLength = 30
        Me.TxtPosition.Name = "TxtPosition"
        Me.TxtPosition.Size = New System.Drawing.Size(234, 22)
        Me.TxtPosition.TabIndex = 12
        '
        'TxtCompanyCode
        '
        Me.TxtCompanyCode.Location = New System.Drawing.Point(754, 12)
        Me.TxtCompanyCode.Name = "TxtCompanyCode"
        Me.TxtCompanyCode.Size = New System.Drawing.Size(23, 19)
        Me.TxtCompanyCode.TabIndex = 52
        Me.TxtCompanyCode.TabStop = False
        Me.TxtCompanyCode.Visible = False
        '
        'LblPostalCodeEx
        '
        Me.LblPostalCodeEx.AutoSize = True
        Me.LblPostalCodeEx.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPostalCodeEx.Location = New System.Drawing.Point(487, 82)
        Me.LblPostalCodeEx.Name = "LblPostalCodeEx"
        Me.LblPostalCodeEx.Size = New System.Drawing.Size(96, 15)
        Me.LblPostalCodeEx.TabIndex = 216
        Me.LblPostalCodeEx.Text = "(例：0123456)"
        '
        'LblTelEx
        '
        Me.LblTelEx.AutoSize = True
        Me.LblTelEx.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTelEx.Location = New System.Drawing.Point(487, 172)
        Me.LblTelEx.Name = "LblTelEx"
        Me.LblTelEx.Size = New System.Drawing.Size(120, 15)
        Me.LblTelEx.TabIndex = 218
        Me.LblTelEx.Text = "(例：0123456789)"
        '
        'LblFaxEx
        '
        Me.LblFaxEx.AutoSize = True
        Me.LblFaxEx.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblFaxEx.Location = New System.Drawing.Point(487, 218)
        Me.LblFaxEx.Name = "LblFaxEx"
        Me.LblFaxEx.Size = New System.Drawing.Size(120, 15)
        Me.LblFaxEx.TabIndex = 219
        Me.LblFaxEx.Text = "(例：0123456789)"
        '
        'LblTelSearchEx
        '
        Me.LblTelSearchEx.AutoSize = True
        Me.LblTelSearchEx.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTelSearchEx.Location = New System.Drawing.Point(133, 217)
        Me.LblTelSearchEx.Name = "LblTelSearchEx"
        Me.LblTelSearchEx.Size = New System.Drawing.Size(120, 15)
        Me.LblTelSearchEx.TabIndex = 222
        Me.LblTelSearchEx.Text = "(例：0123456789)"
        '
        'LblCustomerCodeRemarks
        '
        Me.LblCustomerCodeRemarks.AutoSize = True
        Me.LblCustomerCodeRemarks.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCustomerCodeRemarks.Location = New System.Drawing.Point(133, 37)
        Me.LblCustomerCodeRemarks.Name = "LblCustomerCodeRemarks"
        Me.LblCustomerCodeRemarks.Size = New System.Drawing.Size(230, 15)
        Me.LblCustomerCodeRemarks.TabIndex = 236
        Me.LblCustomerCodeRemarks.Text = "(他得意先コードと重複しない文字列)"
        '
        'LblAccountingVendorCode
        '
        Me.LblAccountingVendorCode.AutoSize = True
        Me.LblAccountingVendorCode.Font = New System.Drawing.Font("MS UI Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAccountingVendorCode.Location = New System.Drawing.Point(12, 332)
        Me.LblAccountingVendorCode.Name = "LblAccountingVendorCode"
        Me.LblAccountingVendorCode.Size = New System.Drawing.Size(96, 11)
        Me.LblAccountingVendorCode.TabIndex = 237
        Me.LblAccountingVendorCode.Text = "会計用得意先コード"
        '
        'TxtAccountingVendorCode
        '
        Me.TxtAccountingVendorCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAccountingVendorCode.Location = New System.Drawing.Point(130, 329)
        Me.TxtAccountingVendorCode.MaxLength = 20
        Me.TxtAccountingVendorCode.Name = "TxtAccountingVendorCode"
        Me.TxtAccountingVendorCode.Size = New System.Drawing.Size(234, 22)
        Me.TxtAccountingVendorCode.TabIndex = 15
        '
        'cmDomesticKbn
        '
        Me.cmDomesticKbn.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmDomesticKbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmDomesticKbn.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmDomesticKbn.FormattingEnabled = True
        Me.cmDomesticKbn.ItemHeight = 15
        Me.cmDomesticKbn.Location = New System.Drawing.Point(483, 328)
        Me.cmDomesticKbn.Name = "cmDomesticKbn"
        Me.cmDomesticKbn.Size = New System.Drawing.Size(234, 23)
        Me.cmDomesticKbn.TabIndex = 251
        '
        'LblDomesticKbn
        '
        Me.LblDomesticKbn.AutoSize = True
        Me.LblDomesticKbn.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblDomesticKbn.Location = New System.Drawing.Point(370, 332)
        Me.LblDomesticKbn.Name = "LblDomesticKbn"
        Me.LblDomesticKbn.Size = New System.Drawing.Size(67, 15)
        Me.LblDomesticKbn.TabIndex = 252
        Me.LblDomesticKbn.Text = "国内区分"
        '
        'Customer
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(729, 561)
        Me.Controls.Add(Me.LblDomesticKbn)
        Me.Controls.Add(Me.cmDomesticKbn)
        Me.Controls.Add(Me.TxtAccountingVendorCode)
        Me.Controls.Add(Me.LblAccountingVendorCode)
        Me.Controls.Add(Me.LblCustomerCodeRemarks)
        Me.Controls.Add(Me.LblTelSearchEx)
        Me.Controls.Add(Me.LblFaxEx)
        Me.Controls.Add(Me.LblTelEx)
        Me.Controls.Add(Me.LblPostalCodeEx)
        Me.Controls.Add(Me.TxtCompanyCode)
        Me.Controls.Add(Me.LblPosition)
        Me.Controls.Add(Me.TxtPosition)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.LblMemo)
        Me.Controls.Add(Me.LblPaymentTerms)
        Me.Controls.Add(Me.LblPerson)
        Me.Controls.Add(Me.LblFax)
        Me.Controls.Add(Me.LblTelSearch)
        Me.Controls.Add(Me.LblTEL)
        Me.Controls.Add(Me.TxtMemo)
        Me.Controls.Add(Me.TxtPaymentTerms)
        Me.Controls.Add(Me.TxtPerson)
        Me.Controls.Add(Me.TxtFax)
        Me.Controls.Add(Me.TxtTelSearch)
        Me.Controls.Add(Me.TxtTel)
        Me.Controls.Add(Me.btnRegistrarion)
        Me.Controls.Add(Me.LblAddress3)
        Me.Controls.Add(Me.LblAddress2)
        Me.Controls.Add(Me.LblAddress1)
        Me.Controls.Add(Me.LblPostalCode)
        Me.Controls.Add(Me.LblCustomerShortName)
        Me.Controls.Add(Me.LblCustomerName)
        Me.Controls.Add(Me.LblCustomerCode)
        Me.Controls.Add(Me.TxtAddress3)
        Me.Controls.Add(Me.TxtAddress2)
        Me.Controls.Add(Me.TxtAddress1)
        Me.Controls.Add(Me.TxtPostalCode)
        Me.Controls.Add(Me.TxtCustomerShortName)
        Me.Controls.Add(Me.TxtCustomerName)
        Me.Controls.Add(Me.TxtCustomerCode)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Customer"
        Me.Text = "CustomerAdd"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnRegistrarion As Button
    Friend WithEvents LblAddress3 As Label
    Friend WithEvents LblAddress2 As Label
    Friend WithEvents LblAddress1 As Label
    Friend WithEvents LblPostalCode As Label
    Friend WithEvents LblCustomerShortName As Label
    Friend WithEvents LblCustomerName As Label
    Friend WithEvents LblCustomerCode As Label
    Friend WithEvents TxtAddress3 As TextBox
    Friend WithEvents TxtAddress2 As TextBox
    Friend WithEvents TxtAddress1 As TextBox
    Friend WithEvents TxtPostalCode As TextBox
    Friend WithEvents TxtCustomerShortName As TextBox
    Friend WithEvents TxtCustomerName As TextBox
    Friend WithEvents TxtCustomerCode As TextBox
    Friend WithEvents LblMemo As Label
    Friend WithEvents LblPaymentTerms As Label
    Friend WithEvents LblPerson As Label
    Friend WithEvents LblFax As Label
    Friend WithEvents LblTelSearch As Label
    Friend WithEvents LblTEL As Label
    Friend WithEvents TxtMemo As TextBox
    Friend WithEvents TxtPaymentTerms As TextBox
    Friend WithEvents TxtPerson As TextBox
    Friend WithEvents TxtFax As TextBox
    Friend WithEvents TxtTelSearch As TextBox
    Friend WithEvents TxtTel As TextBox
    Friend WithEvents btnBack As Button
    Friend WithEvents LblPosition As Label
    Friend WithEvents TxtPosition As TextBox
    Friend WithEvents TxtCompanyCode As TextBox
    Friend WithEvents LblPostalCodeEx As Label
    Friend WithEvents LblTelEx As Label
    Friend WithEvents LblFaxEx As Label
    Friend WithEvents LblTelSearchEx As Label
    Friend WithEvents LblCustomerCodeRemarks As Label
    Friend WithEvents LblAccountingVendorCode As Label
    Friend WithEvents TxtAccountingVendorCode As TextBox
    Friend WithEvents cmDomesticKbn As ComboBox
    Friend WithEvents LblDomesticKbn As Label
End Class
