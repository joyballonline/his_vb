<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SupplierAdd
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
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Memo = New System.Windows.Forms.TextBox()
        Me.Costs = New System.Windows.Forms.TextBox()
        Me.Person = New System.Windows.Forms.TextBox()
        Me.Fax = New System.Windows.Forms.TextBox()
        Me.TelSearch = New System.Windows.Forms.TextBox()
        Me.Tel = New System.Windows.Forms.TextBox()
        Me.btnAddSupplier = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Address3 = New System.Windows.Forms.TextBox()
        Me.Address2 = New System.Windows.Forms.TextBox()
        Me.Address1 = New System.Windows.Forms.TextBox()
        Me.PostalCode = New System.Windows.Forms.TextBox()
        Me.SupplierShortName = New System.Windows.Forms.TextBox()
        Me.SupplierName = New System.Windows.Forms.TextBox()
        Me.SupplierCode = New System.Windows.Forms.TextBox()
        Me.CompanyCode = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.AccountNumber = New System.Windows.Forms.TextBox()
        Me.DepositCategory = New System.Windows.Forms.TextBox()
        Me.BranchOfficeCode = New System.Windows.Forms.TextBox()
        Me.BankCode = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.AccountName = New System.Windows.Forms.TextBox()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(383, 159)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(22, 12)
        Me.Label14.TabIndex = 77
        Me.Label14.Text = "メモ"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(12, 162)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(77, 12)
        Me.Label13.TabIndex = 76
        Me.Label13.Text = "既定間接費率"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(383, 134)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(53, 12)
        Me.Label12.TabIndex = 75
        Me.Label12.Text = "担当者名"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(12, 137)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(51, 12)
        Me.Label11.TabIndex = 74
        Me.Label11.Text = "FAX番号"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(381, 109)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(89, 12)
        Me.Label10.TabIndex = 73
        Me.Label10.Text = "電話番号検索用"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 112)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 12)
        Me.Label9.TabIndex = 72
        Me.Label9.Text = "電話番号"
        '
        'Memo
        '
        Me.Memo.Location = New System.Drawing.Point(472, 156)
        Me.Memo.Name = "Memo"
        Me.Memo.Size = New System.Drawing.Size(234, 19)
        Me.Memo.TabIndex = 71
        '
        'Costs
        '
        Me.Costs.Location = New System.Drawing.Point(101, 159)
        Me.Costs.Name = "Costs"
        Me.Costs.Size = New System.Drawing.Size(234, 19)
        Me.Costs.TabIndex = 70
        '
        'Person
        '
        Me.Person.Location = New System.Drawing.Point(472, 131)
        Me.Person.Name = "Person"
        Me.Person.Size = New System.Drawing.Size(234, 19)
        Me.Person.TabIndex = 69
        '
        'Fax
        '
        Me.Fax.Location = New System.Drawing.Point(101, 134)
        Me.Fax.Name = "Fax"
        Me.Fax.Size = New System.Drawing.Size(234, 19)
        Me.Fax.TabIndex = 68
        '
        'TelSearch
        '
        Me.TelSearch.Location = New System.Drawing.Point(472, 106)
        Me.TelSearch.Name = "TelSearch"
        Me.TelSearch.Size = New System.Drawing.Size(234, 19)
        Me.TelSearch.TabIndex = 67
        '
        'Tel
        '
        Me.Tel.Location = New System.Drawing.Point(101, 109)
        Me.Tel.Name = "Tel"
        Me.Tel.Size = New System.Drawing.Size(234, 19)
        Me.Tel.TabIndex = 66
        '
        'btnAddSupplier
        '
        Me.btnAddSupplier.Location = New System.Drawing.Point(385, 275)
        Me.btnAddSupplier.Name = "btnAddSupplier"
        Me.btnAddSupplier.Size = New System.Drawing.Size(321, 23)
        Me.btnAddSupplier.TabIndex = 65
        Me.btnAddSupplier.Text = "仕入先追加"
        Me.btnAddSupplier.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(383, 84)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(37, 12)
        Me.Label8.TabIndex = 64
        Me.Label8.Text = "住所３"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 87)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(37, 12)
        Me.Label7.TabIndex = 63
        Me.Label7.Text = "住所２"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(383, 59)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(37, 12)
        Me.Label6.TabIndex = 62
        Me.Label6.Text = "住所１"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 59)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 12)
        Me.Label5.TabIndex = 61
        Me.Label5.Text = "郵便番号"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(381, 34)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 12)
        Me.Label4.TabIndex = 60
        Me.Label4.Text = "仕入先名略称"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 34)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 12)
        Me.Label3.TabIndex = 59
        Me.Label3.Text = "仕入先名"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(381, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 12)
        Me.Label2.TabIndex = 58
        Me.Label2.Text = "仕入先コード"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 12)
        Me.Label1.TabIndex = 57
        Me.Label1.Text = "会社コード"
        '
        'Address3
        '
        Me.Address3.Location = New System.Drawing.Point(472, 81)
        Me.Address3.Name = "Address3"
        Me.Address3.Size = New System.Drawing.Size(234, 19)
        Me.Address3.TabIndex = 56
        '
        'Address2
        '
        Me.Address2.Location = New System.Drawing.Point(101, 84)
        Me.Address2.Name = "Address2"
        Me.Address2.Size = New System.Drawing.Size(234, 19)
        Me.Address2.TabIndex = 55
        '
        'Address1
        '
        Me.Address1.Location = New System.Drawing.Point(472, 56)
        Me.Address1.Name = "Address1"
        Me.Address1.Size = New System.Drawing.Size(234, 19)
        Me.Address1.TabIndex = 54
        '
        'PostalCode
        '
        Me.PostalCode.Location = New System.Drawing.Point(101, 56)
        Me.PostalCode.Name = "PostalCode"
        Me.PostalCode.Size = New System.Drawing.Size(234, 19)
        Me.PostalCode.TabIndex = 53
        '
        'SupplierShortName
        '
        Me.SupplierShortName.Location = New System.Drawing.Point(470, 31)
        Me.SupplierShortName.Name = "SupplierShortName"
        Me.SupplierShortName.Size = New System.Drawing.Size(234, 19)
        Me.SupplierShortName.TabIndex = 52
        '
        'SupplierName
        '
        Me.SupplierName.Location = New System.Drawing.Point(101, 31)
        Me.SupplierName.Name = "SupplierName"
        Me.SupplierName.Size = New System.Drawing.Size(234, 19)
        Me.SupplierName.TabIndex = 51
        '
        'SupplierCode
        '
        Me.SupplierCode.Location = New System.Drawing.Point(470, 6)
        Me.SupplierCode.Name = "SupplierCode"
        Me.SupplierCode.Size = New System.Drawing.Size(234, 19)
        Me.SupplierCode.TabIndex = 50
        '
        'CompanyCode
        '
        Me.CompanyCode.Location = New System.Drawing.Point(101, 6)
        Me.CompanyCode.Name = "CompanyCode"
        Me.CompanyCode.Size = New System.Drawing.Size(234, 19)
        Me.CompanyCode.TabIndex = 49
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(383, 209)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(53, 12)
        Me.Label15.TabIndex = 85
        Me.Label15.Text = "口座番号"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(12, 212)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(53, 12)
        Me.Label16.TabIndex = 84
        Me.Label16.Text = "預金種目"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(383, 184)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(56, 12)
        Me.Label17.TabIndex = 83
        Me.Label17.Text = "支店コード"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(12, 187)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(56, 12)
        Me.Label18.TabIndex = 82
        Me.Label18.Text = "銀行コード"
        '
        'AccountNumber
        '
        Me.AccountNumber.Location = New System.Drawing.Point(472, 206)
        Me.AccountNumber.Name = "AccountNumber"
        Me.AccountNumber.Size = New System.Drawing.Size(234, 19)
        Me.AccountNumber.TabIndex = 81
        '
        'DepositCategory
        '
        Me.DepositCategory.Location = New System.Drawing.Point(101, 209)
        Me.DepositCategory.Name = "DepositCategory"
        Me.DepositCategory.Size = New System.Drawing.Size(234, 19)
        Me.DepositCategory.TabIndex = 80
        '
        'BranchOfficeCode
        '
        Me.BranchOfficeCode.Location = New System.Drawing.Point(472, 181)
        Me.BranchOfficeCode.Name = "BranchOfficeCode"
        Me.BranchOfficeCode.Size = New System.Drawing.Size(234, 19)
        Me.BranchOfficeCode.TabIndex = 79
        '
        'BankCode
        '
        Me.BankCode.Location = New System.Drawing.Point(101, 184)
        Me.BankCode.Name = "BankCode"
        Me.BankCode.Size = New System.Drawing.Size(234, 19)
        Me.BankCode.TabIndex = 78
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(12, 237)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(53, 12)
        Me.Label19.TabIndex = 87
        Me.Label19.Text = "口座名義"
        '
        'AccountName
        '
        Me.AccountName.Location = New System.Drawing.Point(101, 234)
        Me.AccountName.Name = "AccountName"
        Me.AccountName.Size = New System.Drawing.Size(234, 19)
        Me.AccountName.TabIndex = 86
        '
        'BtnBack
        '
        Me.BtnBack.Location = New System.Drawing.Point(14, 275)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(321, 23)
        Me.BtnBack.TabIndex = 88
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'SupplierAdd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(721, 323)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.AccountName)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.AccountNumber)
        Me.Controls.Add(Me.DepositCategory)
        Me.Controls.Add(Me.BranchOfficeCode)
        Me.Controls.Add(Me.BankCode)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Memo)
        Me.Controls.Add(Me.Costs)
        Me.Controls.Add(Me.Person)
        Me.Controls.Add(Me.Fax)
        Me.Controls.Add(Me.TelSearch)
        Me.Controls.Add(Me.Tel)
        Me.Controls.Add(Me.btnAddSupplier)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Address3)
        Me.Controls.Add(Me.Address2)
        Me.Controls.Add(Me.Address1)
        Me.Controls.Add(Me.PostalCode)
        Me.Controls.Add(Me.SupplierShortName)
        Me.Controls.Add(Me.SupplierName)
        Me.Controls.Add(Me.SupplierCode)
        Me.Controls.Add(Me.CompanyCode)
        Me.Name = "SupplierAdd"
        Me.Text = "SupplierAdd"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label14 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Memo As TextBox
    Friend WithEvents Costs As TextBox
    Friend WithEvents Person As TextBox
    Friend WithEvents Fax As TextBox
    Friend WithEvents TelSearch As TextBox
    Friend WithEvents Tel As TextBox
    Friend WithEvents btnAddSupplier As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Address3 As TextBox
    Friend WithEvents Address2 As TextBox
    Friend WithEvents Address1 As TextBox
    Friend WithEvents PostalCode As TextBox
    Friend WithEvents SupplierShortName As TextBox
    Friend WithEvents SupplierName As TextBox
    Friend WithEvents SupplierCode As TextBox
    Friend WithEvents CompanyCode As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents AccountNumber As TextBox
    Friend WithEvents DepositCategory As TextBox
    Friend WithEvents BranchOfficeCode As TextBox
    Friend WithEvents BankCode As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents AccountName As TextBox
    Friend WithEvents BtnBack As Button
End Class
