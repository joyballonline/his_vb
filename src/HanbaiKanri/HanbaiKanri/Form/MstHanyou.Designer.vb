<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MstHanyou
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
        Me.Dgv_Company = New System.Windows.Forms.DataGridView()
        Me.会社コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.会社名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.会社略称 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.郵便番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.住所１ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.住所２ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.住所３ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.電話番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FAX番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.代表者役職 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.代表者名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.表示順 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.備考 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.銀行コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.支店コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.預金種目 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.口座番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.口座名義 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.更新者 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.更新日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.AccountName = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.AccountNumber = New System.Windows.Forms.TextBox()
        Me.DepositCategory = New System.Windows.Forms.TextBox()
        Me.BranchOfficeCode = New System.Windows.Forms.TextBox()
        Me.BankCode = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Remarks = New System.Windows.Forms.TextBox()
        Me.DisplayOrder = New System.Windows.Forms.TextBox()
        Me.RepresentativeName = New System.Windows.Forms.TextBox()
        Me.Fax = New System.Windows.Forms.TextBox()
        Me.Tel = New System.Windows.Forms.TextBox()
        Me.btnEditCompany = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Address3 = New System.Windows.Forms.TextBox()
        Me.Address2 = New System.Windows.Forms.TextBox()
        Me.Address1 = New System.Windows.Forms.TextBox()
        Me.PostalCode = New System.Windows.Forms.TextBox()
        Me.CompanyShortName = New System.Windows.Forms.TextBox()
        Me.CompanyName = New System.Windows.Forms.TextBox()
        Me.CompanyCode = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.RepresentativePosition = New System.Windows.Forms.TextBox()
        CType(Me.Dgv_Company, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Dgv_Company
        '
        Me.Dgv_Company.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Dgv_Company.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.会社コード, Me.会社名, Me.会社略称, Me.郵便番号, Me.住所１, Me.住所２, Me.住所３, Me.電話番号, Me.FAX番号, Me.代表者役職, Me.代表者名, Me.表示順, Me.備考, Me.銀行コード, Me.支店コード, Me.預金種目, Me.口座番号, Me.口座名義, Me.更新者, Me.更新日})
        Me.Dgv_Company.Location = New System.Drawing.Point(0, 0)
        Me.Dgv_Company.Name = "Dgv_Company"
        Me.Dgv_Company.RowTemplate.Height = 21
        Me.Dgv_Company.Size = New System.Drawing.Size(898, 69)
        Me.Dgv_Company.TabIndex = 0
        '
        '会社コード
        '
        Me.会社コード.HeaderText = "会社コード"
        Me.会社コード.Name = "会社コード"
        '
        '会社名
        '
        Me.会社名.HeaderText = "会社名"
        Me.会社名.Name = "会社名"
        '
        '会社略称
        '
        Me.会社略称.HeaderText = "会社略称"
        Me.会社略称.Name = "会社略称"
        '
        '郵便番号
        '
        Me.郵便番号.HeaderText = "郵便番号"
        Me.郵便番号.Name = "郵便番号"
        '
        '住所１
        '
        Me.住所１.HeaderText = "住所１"
        Me.住所１.Name = "住所１"
        '
        '住所２
        '
        Me.住所２.HeaderText = "住所２"
        Me.住所２.Name = "住所２"
        '
        '住所３
        '
        Me.住所３.HeaderText = "住所３"
        Me.住所３.Name = "住所３"
        '
        '電話番号
        '
        Me.電話番号.HeaderText = "電話番号"
        Me.電話番号.Name = "電話番号"
        '
        'FAX番号
        '
        Me.FAX番号.HeaderText = "FAX番号"
        Me.FAX番号.Name = "FAX番号"
        '
        '代表者役職
        '
        Me.代表者役職.HeaderText = "代表者役職"
        Me.代表者役職.Name = "代表者役職"
        '
        '代表者名
        '
        Me.代表者名.HeaderText = "代表者名"
        Me.代表者名.Name = "代表者名"
        '
        '表示順
        '
        Me.表示順.HeaderText = "表示順"
        Me.表示順.Name = "表示順"
        '
        '備考
        '
        Me.備考.HeaderText = "備考"
        Me.備考.Name = "備考"
        '
        '銀行コード
        '
        Me.銀行コード.HeaderText = "銀行コード"
        Me.銀行コード.Name = "銀行コード"
        '
        '支店コード
        '
        Me.支店コード.HeaderText = "支店コード"
        Me.支店コード.Name = "支店コード"
        '
        '預金種目
        '
        Me.預金種目.HeaderText = "預金種目"
        Me.預金種目.Name = "預金種目"
        '
        '口座番号
        '
        Me.口座番号.HeaderText = "口座番号"
        Me.口座番号.Name = "口座番号"
        '
        '口座名義
        '
        Me.口座名義.HeaderText = "口座名義"
        Me.口座名義.Name = "口座名義"
        '
        '更新者
        '
        Me.更新者.HeaderText = "更新者"
        Me.更新者.Name = "更新者"
        '
        '更新日
        '
        Me.更新日.HeaderText = "更新日"
        Me.更新日.Name = "更新日"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(381, 418)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(53, 12)
        Me.Label19.TabIndex = 165
        Me.Label19.Text = "口座名義"
        '
        'AccountName
        '
        Me.AccountName.Location = New System.Drawing.Point(470, 415)
        Me.AccountName.Name = "AccountName"
        Me.AccountName.Size = New System.Drawing.Size(234, 19)
        Me.AccountName.TabIndex = 164
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(12, 418)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(53, 12)
        Me.Label15.TabIndex = 163
        Me.Label15.Text = "口座番号"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(381, 393)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(53, 12)
        Me.Label16.TabIndex = 162
        Me.Label16.Text = "預金種目"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(12, 393)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(56, 12)
        Me.Label17.TabIndex = 161
        Me.Label17.Text = "支店コード"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(381, 368)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(56, 12)
        Me.Label18.TabIndex = 160
        Me.Label18.Text = "銀行コード"
        '
        'AccountNumber
        '
        Me.AccountNumber.Location = New System.Drawing.Point(101, 415)
        Me.AccountNumber.Name = "AccountNumber"
        Me.AccountNumber.Size = New System.Drawing.Size(234, 19)
        Me.AccountNumber.TabIndex = 159
        '
        'DepositCategory
        '
        Me.DepositCategory.Location = New System.Drawing.Point(470, 390)
        Me.DepositCategory.Name = "DepositCategory"
        Me.DepositCategory.Size = New System.Drawing.Size(234, 19)
        Me.DepositCategory.TabIndex = 158
        '
        'BranchOfficeCode
        '
        Me.BranchOfficeCode.Location = New System.Drawing.Point(101, 390)
        Me.BranchOfficeCode.Name = "BranchOfficeCode"
        Me.BranchOfficeCode.Size = New System.Drawing.Size(234, 19)
        Me.BranchOfficeCode.TabIndex = 157
        '
        'BankCode
        '
        Me.BankCode.Location = New System.Drawing.Point(470, 365)
        Me.BankCode.Name = "BankCode"
        Me.BankCode.Size = New System.Drawing.Size(234, 19)
        Me.BankCode.TabIndex = 156
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(12, 368)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(29, 12)
        Me.Label14.TabIndex = 155
        Me.Label14.Text = "備考"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(381, 343)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(41, 12)
        Me.Label13.TabIndex = 154
        Me.Label13.Text = "表示順"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(12, 343)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(53, 12)
        Me.Label12.TabIndex = 153
        Me.Label12.Text = "代表者名"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(12, 318)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(51, 12)
        Me.Label11.TabIndex = 152
        Me.Label11.Text = "FAX番号"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(381, 293)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 12)
        Me.Label9.TabIndex = 150
        Me.Label9.Text = "電話番号"
        '
        'Remarks
        '
        Me.Remarks.Location = New System.Drawing.Point(101, 365)
        Me.Remarks.Name = "Remarks"
        Me.Remarks.Size = New System.Drawing.Size(234, 19)
        Me.Remarks.TabIndex = 149
        '
        'DisplayOrder
        '
        Me.DisplayOrder.Location = New System.Drawing.Point(470, 340)
        Me.DisplayOrder.Name = "DisplayOrder"
        Me.DisplayOrder.Size = New System.Drawing.Size(234, 19)
        Me.DisplayOrder.TabIndex = 148
        '
        'RepresentativeName
        '
        Me.RepresentativeName.Location = New System.Drawing.Point(101, 340)
        Me.RepresentativeName.Name = "RepresentativeName"
        Me.RepresentativeName.Size = New System.Drawing.Size(234, 19)
        Me.RepresentativeName.TabIndex = 147
        '
        'Fax
        '
        Me.Fax.Location = New System.Drawing.Point(101, 315)
        Me.Fax.Name = "Fax"
        Me.Fax.Size = New System.Drawing.Size(234, 19)
        Me.Fax.TabIndex = 146
        '
        'Tel
        '
        Me.Tel.Location = New System.Drawing.Point(470, 290)
        Me.Tel.Name = "Tel"
        Me.Tel.Size = New System.Drawing.Size(234, 19)
        Me.Tel.TabIndex = 144
        '
        'btnEditCompany
        '
        Me.btnEditCompany.Location = New System.Drawing.Point(12, 454)
        Me.btnEditCompany.Name = "btnEditCompany"
        Me.btnEditCompany.Size = New System.Drawing.Size(692, 23)
        Me.btnEditCompany.TabIndex = 143
        Me.btnEditCompany.Text = "編集"
        Me.btnEditCompany.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 293)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(37, 12)
        Me.Label8.TabIndex = 142
        Me.Label8.Text = "住所３"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(381, 268)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(37, 12)
        Me.Label7.TabIndex = 141
        Me.Label7.Text = "住所２"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 268)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(37, 12)
        Me.Label6.TabIndex = 140
        Me.Label6.Text = "住所１"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(381, 243)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 12)
        Me.Label5.TabIndex = 139
        Me.Label5.Text = "郵便番号"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 243)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 12)
        Me.Label3.TabIndex = 137
        Me.Label3.Text = "会社略称"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(381, 218)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 12)
        Me.Label2.TabIndex = 136
        Me.Label2.Text = "会社名"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 218)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 12)
        Me.Label1.TabIndex = 135
        Me.Label1.Text = "会社コード"
        '
        'Address3
        '
        Me.Address3.Location = New System.Drawing.Point(101, 290)
        Me.Address3.Name = "Address3"
        Me.Address3.Size = New System.Drawing.Size(234, 19)
        Me.Address3.TabIndex = 134
        '
        'Address2
        '
        Me.Address2.Location = New System.Drawing.Point(470, 265)
        Me.Address2.Name = "Address2"
        Me.Address2.Size = New System.Drawing.Size(234, 19)
        Me.Address2.TabIndex = 133
        '
        'Address1
        '
        Me.Address1.Location = New System.Drawing.Point(101, 265)
        Me.Address1.Name = "Address1"
        Me.Address1.Size = New System.Drawing.Size(234, 19)
        Me.Address1.TabIndex = 132
        '
        'PostalCode
        '
        Me.PostalCode.Location = New System.Drawing.Point(470, 240)
        Me.PostalCode.Name = "PostalCode"
        Me.PostalCode.Size = New System.Drawing.Size(234, 19)
        Me.PostalCode.TabIndex = 131
        '
        'CompanyShortName
        '
        Me.CompanyShortName.Location = New System.Drawing.Point(101, 240)
        Me.CompanyShortName.Name = "CompanyShortName"
        Me.CompanyShortName.Size = New System.Drawing.Size(234, 19)
        Me.CompanyShortName.TabIndex = 129
        '
        'CompanyName
        '
        Me.CompanyName.Location = New System.Drawing.Point(470, 215)
        Me.CompanyName.Name = "CompanyName"
        Me.CompanyName.Size = New System.Drawing.Size(234, 19)
        Me.CompanyName.TabIndex = 128
        '
        'CompanyCode
        '
        Me.CompanyCode.Location = New System.Drawing.Point(101, 215)
        Me.CompanyCode.Name = "CompanyCode"
        Me.CompanyCode.Size = New System.Drawing.Size(234, 19)
        Me.CompanyCode.TabIndex = 127
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(381, 318)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 12)
        Me.Label4.TabIndex = 167
        Me.Label4.Text = "代表者役職"
        '
        'RepresentativePosition
        '
        Me.RepresentativePosition.Location = New System.Drawing.Point(470, 315)
        Me.RepresentativePosition.Name = "RepresentativePosition"
        Me.RepresentativePosition.Size = New System.Drawing.Size(234, 19)
        Me.RepresentativePosition.TabIndex = 166
        '
        'MstHanyou
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(719, 489)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.RepresentativePosition)
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
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Remarks)
        Me.Controls.Add(Me.DisplayOrder)
        Me.Controls.Add(Me.RepresentativeName)
        Me.Controls.Add(Me.Fax)
        Me.Controls.Add(Me.Tel)
        Me.Controls.Add(Me.btnEditCompany)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Address3)
        Me.Controls.Add(Me.Address2)
        Me.Controls.Add(Me.Address1)
        Me.Controls.Add(Me.PostalCode)
        Me.Controls.Add(Me.CompanyShortName)
        Me.Controls.Add(Me.CompanyName)
        Me.Controls.Add(Me.CompanyCode)
        Me.Controls.Add(Me.Dgv_Company)
        Me.Name = "MstHanyou"
        Me.Text = "MstHanyou"
        CType(Me.Dgv_Company, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Dgv_Company As DataGridView
    Friend WithEvents 会社コード As DataGridViewTextBoxColumn
    Friend WithEvents 会社名 As DataGridViewTextBoxColumn
    Friend WithEvents 会社略称 As DataGridViewTextBoxColumn
    Friend WithEvents 郵便番号 As DataGridViewTextBoxColumn
    Friend WithEvents 住所１ As DataGridViewTextBoxColumn
    Friend WithEvents 住所２ As DataGridViewTextBoxColumn
    Friend WithEvents 住所３ As DataGridViewTextBoxColumn
    Friend WithEvents 電話番号 As DataGridViewTextBoxColumn
    Friend WithEvents FAX番号 As DataGridViewTextBoxColumn
    Friend WithEvents 代表者役職 As DataGridViewTextBoxColumn
    Friend WithEvents 代表者名 As DataGridViewTextBoxColumn
    Friend WithEvents 表示順 As DataGridViewTextBoxColumn
    Friend WithEvents 備考 As DataGridViewTextBoxColumn
    Friend WithEvents 銀行コード As DataGridViewTextBoxColumn
    Friend WithEvents 支店コード As DataGridViewTextBoxColumn
    Friend WithEvents 預金種目 As DataGridViewTextBoxColumn
    Friend WithEvents 口座番号 As DataGridViewTextBoxColumn
    Friend WithEvents 口座名義 As DataGridViewTextBoxColumn
    Friend WithEvents 更新者 As DataGridViewTextBoxColumn
    Friend WithEvents 更新日 As DataGridViewTextBoxColumn
    Friend WithEvents Label19 As Label
    Friend WithEvents AccountName As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents AccountNumber As TextBox
    Friend WithEvents DepositCategory As TextBox
    Friend WithEvents BranchOfficeCode As TextBox
    Friend WithEvents BankCode As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Remarks As TextBox
    Friend WithEvents DisplayOrder As TextBox
    Friend WithEvents RepresentativeName As TextBox
    Friend WithEvents Fax As TextBox
    Friend WithEvents Tel As TextBox
    Friend WithEvents btnEditCompany As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Address3 As TextBox
    Friend WithEvents Address2 As TextBox
    Friend WithEvents Address1 As TextBox
    Friend WithEvents PostalCode As TextBox
    Friend WithEvents CompanyShortName As TextBox
    Friend WithEvents CompanyName As TextBox
    Friend WithEvents CompanyCode As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents RepresentativePosition As TextBox
End Class
