<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CustomerAdd
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
        Me.btnAddCustomer = New System.Windows.Forms.Button()
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
        Me.CustomerShortName = New System.Windows.Forms.TextBox()
        Me.CustomerName = New System.Windows.Forms.TextBox()
        Me.CustomerCode = New System.Windows.Forms.TextBox()
        Me.CompanyCode = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Memo = New System.Windows.Forms.TextBox()
        Me.PaymentTerms = New System.Windows.Forms.TextBox()
        Me.Person = New System.Windows.Forms.TextBox()
        Me.Fax = New System.Windows.Forms.TextBox()
        Me.TelSearch = New System.Windows.Forms.TextBox()
        Me.Tel = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'btnAddCustomer
        '
        Me.btnAddCustomer.Location = New System.Drawing.Point(18, 203)
        Me.btnAddCustomer.Name = "btnAddCustomer"
        Me.btnAddCustomer.Size = New System.Drawing.Size(692, 23)
        Me.btnAddCustomer.TabIndex = 34
        Me.btnAddCustomer.Text = "得意先追加"
        Me.btnAddCustomer.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(389, 90)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(37, 12)
        Me.Label8.TabIndex = 33
        Me.Label8.Text = "住所３"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(18, 93)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(37, 12)
        Me.Label7.TabIndex = 32
        Me.Label7.Text = "住所２"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(389, 65)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(37, 12)
        Me.Label6.TabIndex = 31
        Me.Label6.Text = "住所１"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(18, 65)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 12)
        Me.Label5.TabIndex = 30
        Me.Label5.Text = "郵便番号"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(387, 40)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 12)
        Me.Label4.TabIndex = 29
        Me.Label4.Text = "得意先名略称"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 12)
        Me.Label3.TabIndex = 28
        Me.Label3.Text = "得意先名"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(387, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 12)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "得意先コード"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 12)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "会社コード"
        '
        'Address3
        '
        Me.Address3.Location = New System.Drawing.Point(478, 87)
        Me.Address3.Name = "Address3"
        Me.Address3.Size = New System.Drawing.Size(234, 19)
        Me.Address3.TabIndex = 25
        '
        'Address2
        '
        Me.Address2.Location = New System.Drawing.Point(107, 90)
        Me.Address2.Name = "Address2"
        Me.Address2.Size = New System.Drawing.Size(234, 19)
        Me.Address2.TabIndex = 24
        '
        'Address1
        '
        Me.Address1.Location = New System.Drawing.Point(478, 62)
        Me.Address1.Name = "Address1"
        Me.Address1.Size = New System.Drawing.Size(234, 19)
        Me.Address1.TabIndex = 23
        '
        'PostalCode
        '
        Me.PostalCode.Location = New System.Drawing.Point(107, 62)
        Me.PostalCode.Name = "PostalCode"
        Me.PostalCode.Size = New System.Drawing.Size(234, 19)
        Me.PostalCode.TabIndex = 22
        '
        'CustomerShortName
        '
        Me.CustomerShortName.Location = New System.Drawing.Point(476, 37)
        Me.CustomerShortName.Name = "CustomerShortName"
        Me.CustomerShortName.Size = New System.Drawing.Size(234, 19)
        Me.CustomerShortName.TabIndex = 21
        '
        'CustomerName
        '
        Me.CustomerName.Location = New System.Drawing.Point(107, 37)
        Me.CustomerName.Name = "CustomerName"
        Me.CustomerName.Size = New System.Drawing.Size(234, 19)
        Me.CustomerName.TabIndex = 20
        '
        'CustomerCode
        '
        Me.CustomerCode.Location = New System.Drawing.Point(476, 12)
        Me.CustomerCode.Name = "CustomerCode"
        Me.CustomerCode.Size = New System.Drawing.Size(234, 19)
        Me.CustomerCode.TabIndex = 19
        '
        'CompanyCode
        '
        Me.CompanyCode.Location = New System.Drawing.Point(107, 12)
        Me.CompanyCode.Name = "CompanyCode"
        Me.CompanyCode.Size = New System.Drawing.Size(234, 19)
        Me.CompanyCode.TabIndex = 18
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(389, 165)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(22, 12)
        Me.Label14.TabIndex = 48
        Me.Label14.Text = "メモ"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(18, 168)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(77, 12)
        Me.Label13.TabIndex = 47
        Me.Label13.Text = "既定支払条件"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(389, 140)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(53, 12)
        Me.Label12.TabIndex = 46
        Me.Label12.Text = "担当者名"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(18, 143)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(51, 12)
        Me.Label11.TabIndex = 45
        Me.Label11.Text = "FAX番号"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(387, 115)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(89, 12)
        Me.Label10.TabIndex = 44
        Me.Label10.Text = "電話番号検索用"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(18, 118)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 12)
        Me.Label9.TabIndex = 43
        Me.Label9.Text = "電話番号"
        '
        'Memo
        '
        Me.Memo.Location = New System.Drawing.Point(478, 162)
        Me.Memo.Name = "Memo"
        Me.Memo.Size = New System.Drawing.Size(234, 19)
        Me.Memo.TabIndex = 40
        '
        'PaymentTerms
        '
        Me.PaymentTerms.Location = New System.Drawing.Point(107, 165)
        Me.PaymentTerms.Name = "PaymentTerms"
        Me.PaymentTerms.Size = New System.Drawing.Size(234, 19)
        Me.PaymentTerms.TabIndex = 39
        '
        'Person
        '
        Me.Person.Location = New System.Drawing.Point(478, 137)
        Me.Person.Name = "Person"
        Me.Person.Size = New System.Drawing.Size(234, 19)
        Me.Person.TabIndex = 38
        '
        'Fax
        '
        Me.Fax.Location = New System.Drawing.Point(107, 140)
        Me.Fax.Name = "Fax"
        Me.Fax.Size = New System.Drawing.Size(234, 19)
        Me.Fax.TabIndex = 37
        '
        'TelSearch
        '
        Me.TelSearch.Location = New System.Drawing.Point(478, 112)
        Me.TelSearch.Name = "TelSearch"
        Me.TelSearch.Size = New System.Drawing.Size(234, 19)
        Me.TelSearch.TabIndex = 36
        '
        'Tel
        '
        Me.Tel.Location = New System.Drawing.Point(107, 115)
        Me.Tel.Name = "Tel"
        Me.Tel.Size = New System.Drawing.Size(234, 19)
        Me.Tel.TabIndex = 35
        '
        'CustomerAdd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(729, 247)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Memo)
        Me.Controls.Add(Me.PaymentTerms)
        Me.Controls.Add(Me.Person)
        Me.Controls.Add(Me.Fax)
        Me.Controls.Add(Me.TelSearch)
        Me.Controls.Add(Me.Tel)
        Me.Controls.Add(Me.btnAddCustomer)
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
        Me.Controls.Add(Me.CustomerShortName)
        Me.Controls.Add(Me.CustomerName)
        Me.Controls.Add(Me.CustomerCode)
        Me.Controls.Add(Me.CompanyCode)
        Me.Name = "CustomerAdd"
        Me.Text = "CustomerAdd"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnAddCustomer As Button
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
    Friend WithEvents CustomerShortName As TextBox
    Friend WithEvents CustomerName As TextBox
    Friend WithEvents CustomerCode As TextBox
    Friend WithEvents CompanyCode As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Memo As TextBox
    Friend WithEvents PaymentTerms As TextBox
    Friend WithEvents Person As TextBox
    Friend WithEvents Fax As TextBox
    Friend WithEvents TelSearch As TextBox
    Friend WithEvents Tel As TextBox
End Class
