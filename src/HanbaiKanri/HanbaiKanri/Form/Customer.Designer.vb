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
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtAddress3 = New System.Windows.Forms.TextBox()
        Me.TxtAddress2 = New System.Windows.Forms.TextBox()
        Me.TxtAddress1 = New System.Windows.Forms.TextBox()
        Me.TxtPostalCode = New System.Windows.Forms.TextBox()
        Me.TxtCustomerShortName = New System.Windows.Forms.TextBox()
        Me.TxtCustomerName = New System.Windows.Forms.TextBox()
        Me.TxtCustomerCode = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TxtMemo = New System.Windows.Forms.TextBox()
        Me.TxtPaymentTerms = New System.Windows.Forms.TextBox()
        Me.TxtPerson = New System.Windows.Forms.TextBox()
        Me.TxtFax = New System.Windows.Forms.TextBox()
        Me.TxtTelSearch = New System.Windows.Forms.TextBox()
        Me.TxtTel = New System.Windows.Forms.TextBox()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.TxtPosition = New System.Windows.Forms.TextBox()
        Me.TxtCompanyCode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnRegistrarion
        '
        Me.btnRegistrarion.Location = New System.Drawing.Point(381, 509)
        Me.btnRegistrarion.Name = "btnRegistrarion"
        Me.btnRegistrarion.Size = New System.Drawing.Size(165, 40)
        Me.btnRegistrarion.TabIndex = 15
        Me.btnRegistrarion.Text = "登録"
        Me.btnRegistrarion.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(77, 150)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(47, 15)
        Me.Label8.TabIndex = 33
        Me.Label8.Text = "住所３"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(430, 105)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(47, 15)
        Me.Label7.TabIndex = 32
        Me.Label7.Text = "住所２"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(77, 105)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(47, 15)
        Me.Label6.TabIndex = 31
        Me.Label6.Text = "住所１"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(410, 60)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(67, 15)
        Me.Label5.TabIndex = 30
        Me.Label5.Text = "郵便番号"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(27, 60)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(97, 15)
        Me.Label4.TabIndex = 29
        Me.Label4.Text = "得意先名略称"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(410, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 15)
        Me.Label3.TabIndex = 28
        Me.Label3.Text = "得意先名"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(39, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 15)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "得意先コード"
        '
        'TxtAddress3
        '
        Me.TxtAddress3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAddress3.Location = New System.Drawing.Point(130, 147)
        Me.TxtAddress3.Name = "TxtAddress3"
        Me.TxtAddress3.Size = New System.Drawing.Size(234, 22)
        Me.TxtAddress3.TabIndex = 7
        '
        'TxtAddress2
        '
        Me.TxtAddress2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAddress2.Location = New System.Drawing.Point(483, 102)
        Me.TxtAddress2.Name = "TxtAddress2"
        Me.TxtAddress2.Size = New System.Drawing.Size(234, 22)
        Me.TxtAddress2.TabIndex = 6
        '
        'TxtAddress1
        '
        Me.TxtAddress1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAddress1.Location = New System.Drawing.Point(130, 102)
        Me.TxtAddress1.Name = "TxtAddress1"
        Me.TxtAddress1.Size = New System.Drawing.Size(234, 22)
        Me.TxtAddress1.TabIndex = 5
        '
        'TxtPostalCode
        '
        Me.TxtPostalCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPostalCode.Location = New System.Drawing.Point(483, 57)
        Me.TxtPostalCode.Name = "TxtPostalCode"
        Me.TxtPostalCode.Size = New System.Drawing.Size(234, 22)
        Me.TxtPostalCode.TabIndex = 4
        '
        'TxtCustomerShortName
        '
        Me.TxtCustomerShortName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerShortName.Location = New System.Drawing.Point(130, 57)
        Me.TxtCustomerShortName.Name = "TxtCustomerShortName"
        Me.TxtCustomerShortName.Size = New System.Drawing.Size(234, 22)
        Me.TxtCustomerShortName.TabIndex = 3
        '
        'TxtCustomerName
        '
        Me.TxtCustomerName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerName.Location = New System.Drawing.Point(483, 12)
        Me.TxtCustomerName.Name = "TxtCustomerName"
        Me.TxtCustomerName.Size = New System.Drawing.Size(234, 22)
        Me.TxtCustomerName.TabIndex = 2
        '
        'TxtCustomerCode
        '
        Me.TxtCustomerCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerCode.Location = New System.Drawing.Point(130, 12)
        Me.TxtCustomerCode.Name = "TxtCustomerCode"
        Me.TxtCustomerCode.Size = New System.Drawing.Size(234, 22)
        Me.TxtCustomerCode.TabIndex = 1
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label14.Location = New System.Drawing.Point(449, 286)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(28, 15)
        Me.Label14.TabIndex = 48
        Me.Label14.Text = "メモ"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.Location = New System.Drawing.Point(27, 286)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(97, 15)
        Me.Label13.TabIndex = 47
        Me.Label13.Text = "既定支払条件"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.Location = New System.Drawing.Point(57, 241)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(67, 15)
        Me.Label12.TabIndex = 46
        Me.Label12.Text = "担当者名"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(414, 195)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(63, 15)
        Me.Label11.TabIndex = 45
        Me.Label11.Text = "FAX番号"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(12, 195)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(112, 15)
        Me.Label10.TabIndex = 44
        Me.Label10.Text = "電話番号検索用"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(410, 150)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(67, 15)
        Me.Label9.TabIndex = 43
        Me.Label9.Text = "電話番号"
        '
        'TxtMemo
        '
        Me.TxtMemo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtMemo.Location = New System.Drawing.Point(483, 283)
        Me.TxtMemo.Name = "TxtMemo"
        Me.TxtMemo.Size = New System.Drawing.Size(234, 22)
        Me.TxtMemo.TabIndex = 14
        '
        'TxtPaymentTerms
        '
        Me.TxtPaymentTerms.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPaymentTerms.Location = New System.Drawing.Point(130, 283)
        Me.TxtPaymentTerms.Name = "TxtPaymentTerms"
        Me.TxtPaymentTerms.Size = New System.Drawing.Size(234, 22)
        Me.TxtPaymentTerms.TabIndex = 13
        '
        'TxtPerson
        '
        Me.TxtPerson.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPerson.Location = New System.Drawing.Point(130, 238)
        Me.TxtPerson.Name = "TxtPerson"
        Me.TxtPerson.Size = New System.Drawing.Size(234, 22)
        Me.TxtPerson.TabIndex = 11
        '
        'TxtFax
        '
        Me.TxtFax.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtFax.Location = New System.Drawing.Point(483, 192)
        Me.TxtFax.Name = "TxtFax"
        Me.TxtFax.Size = New System.Drawing.Size(234, 22)
        Me.TxtFax.TabIndex = 10
        '
        'TxtTelSearch
        '
        Me.TxtTelSearch.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtTelSearch.Location = New System.Drawing.Point(130, 192)
        Me.TxtTelSearch.Name = "TxtTelSearch"
        Me.TxtTelSearch.Size = New System.Drawing.Size(234, 22)
        Me.TxtTelSearch.TabIndex = 9
        '
        'TxtTel
        '
        Me.TxtTel.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtTel.Location = New System.Drawing.Point(483, 147)
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
        Me.btnBack.TabIndex = 16
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label15.Location = New System.Drawing.Point(395, 241)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(82, 15)
        Me.Label15.TabIndex = 51
        Me.Label15.Text = "担当者役職"
        '
        'TxtPosition
        '
        Me.TxtPosition.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPosition.Location = New System.Drawing.Point(483, 238)
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(487, 82)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 15)
        Me.Label1.TabIndex = 216
        Me.Label1.Text = "(例：0123456)"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label17.Location = New System.Drawing.Point(487, 172)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(120, 15)
        Me.Label17.TabIndex = 218
        Me.Label17.Text = "(例：0123456789)"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label18.Location = New System.Drawing.Point(487, 218)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(120, 15)
        Me.Label18.TabIndex = 219
        Me.Label18.Text = "(例：0123456789)"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label16.Location = New System.Drawing.Point(133, 217)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(120, 15)
        Me.Label16.TabIndex = 222
        Me.Label16.Text = "(例：0123456789)"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label26.Location = New System.Drawing.Point(133, 37)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(230, 15)
        Me.Label26.TabIndex = 236
        Me.Label26.Text = "(他得意先コードと重複しない文字列)"
        '
        'Customer
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(729, 561)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtCompanyCode)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.TxtPosition)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.TxtMemo)
        Me.Controls.Add(Me.TxtPaymentTerms)
        Me.Controls.Add(Me.TxtPerson)
        Me.Controls.Add(Me.TxtFax)
        Me.Controls.Add(Me.TxtTelSearch)
        Me.Controls.Add(Me.TxtTel)
        Me.Controls.Add(Me.btnRegistrarion)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
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
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtAddress3 As TextBox
    Friend WithEvents TxtAddress2 As TextBox
    Friend WithEvents TxtAddress1 As TextBox
    Friend WithEvents TxtPostalCode As TextBox
    Friend WithEvents TxtCustomerShortName As TextBox
    Friend WithEvents TxtCustomerName As TextBox
    Friend WithEvents TxtCustomerCode As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents TxtMemo As TextBox
    Friend WithEvents TxtPaymentTerms As TextBox
    Friend WithEvents TxtPerson As TextBox
    Friend WithEvents TxtFax As TextBox
    Friend WithEvents TxtTelSearch As TextBox
    Friend WithEvents TxtTel As TextBox
    Friend WithEvents btnBack As Button
    Friend WithEvents Label15 As Label
    Friend WithEvents TxtPosition As TextBox
    Friend WithEvents TxtCompanyCode As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label26 As Label
End Class
