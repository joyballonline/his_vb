<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OrderList
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
        Me.BtnOrderView = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RbtnSlip = New System.Windows.Forms.RadioButton()
        Me.RbtnDetails = New System.Windows.Forms.RadioButton()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtOrderNo2 = New System.Windows.Forms.TextBox()
        Me.TxtOrderDate2 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtSales = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TxtOrderNo1 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TxtOrderDate1 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtCustomerCode = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtTel = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtAddress = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblConditions = New System.Windows.Forms.Label()
        Me.TxtCustomerName = New System.Windows.Forms.TextBox()
        Me.BtnOrderSearch = New System.Windows.Forms.Button()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.BtnOrderEdit = New System.Windows.Forms.Button()
        Me.DgvCymnhd = New System.Windows.Forms.DataGridView()
        Me.Panel1.SuspendLayout()
        CType(Me.DgvCymnhd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnOrderView
        '
        Me.BtnOrderView.Location = New System.Drawing.Point(1002, 677)
        Me.BtnOrderView.Name = "BtnOrderView"
        Me.BtnOrderView.Size = New System.Drawing.Size(165, 40)
        Me.BtnOrderView.TabIndex = 60
        Me.BtnOrderView.Text = "受注参照"
        Me.BtnOrderView.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.RbtnSlip)
        Me.Panel1.Controls.Add(Me.RbtnDetails)
        Me.Panel1.Location = New System.Drawing.Point(16, 189)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(195, 37)
        Me.Panel1.TabIndex = 59
        '
        'RbtnSlip
        '
        Me.RbtnSlip.AutoSize = True
        Me.RbtnSlip.Checked = True
        Me.RbtnSlip.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.RbtnSlip.Location = New System.Drawing.Point(6, 8)
        Me.RbtnSlip.Name = "RbtnSlip"
        Me.RbtnSlip.Size = New System.Drawing.Size(89, 19)
        Me.RbtnSlip.TabIndex = 26
        Me.RbtnSlip.TabStop = True
        Me.RbtnSlip.Text = "伝票単位"
        Me.RbtnSlip.UseVisualStyleBackColor = True
        '
        'RbtnDetails
        '
        Me.RbtnDetails.AutoSize = True
        Me.RbtnDetails.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.RbtnDetails.Location = New System.Drawing.Point(101, 8)
        Me.RbtnDetails.Name = "RbtnDetails"
        Me.RbtnDetails.Size = New System.Drawing.Size(89, 19)
        Me.RbtnDetails.TabIndex = 27
        Me.RbtnDetails.Text = "明細単位"
        Me.RbtnDetails.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(13, 164)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(87, 15)
        Me.Label10.TabIndex = 58
        Me.Label10.Text = "■表示形式"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(936, 76)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(17, 12)
        Me.Label9.TabIndex = 57
        Me.Label9.Text = "～"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(936, 48)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(17, 12)
        Me.Label5.TabIndex = 56
        Me.Label5.Text = "～"
        '
        'TxtOrderNo2
        '
        Me.TxtOrderNo2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtOrderNo2.Location = New System.Drawing.Point(959, 70)
        Me.TxtOrderNo2.Name = "TxtOrderNo2"
        Me.TxtOrderNo2.Size = New System.Drawing.Size(170, 22)
        Me.TxtOrderNo2.TabIndex = 55
        '
        'TxtOrderDate2
        '
        Me.TxtOrderDate2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtOrderDate2.Location = New System.Drawing.Point(959, 42)
        Me.TxtOrderDate2.Name = "TxtOrderDate2"
        Me.TxtOrderDate2.Size = New System.Drawing.Size(170, 22)
        Me.TxtOrderDate2.TabIndex = 54
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(584, 97)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(170, 22)
        Me.Label6.TabIndex = 53
        Me.Label6.Text = "営業担当者"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtSales
        '
        Me.TxtSales.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSales.Location = New System.Drawing.Point(760, 97)
        Me.TxtSales.Name = "TxtSales"
        Me.TxtSales.Size = New System.Drawing.Size(170, 22)
        Me.TxtSales.TabIndex = 52
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(584, 69)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(170, 22)
        Me.Label7.TabIndex = 51
        Me.Label7.Text = "受注番号"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtOrderNo1
        '
        Me.TxtOrderNo1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtOrderNo1.Location = New System.Drawing.Point(760, 69)
        Me.TxtOrderNo1.Name = "TxtOrderNo1"
        Me.TxtOrderNo1.Size = New System.Drawing.Size(170, 22)
        Me.TxtOrderNo1.TabIndex = 50
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(584, 41)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(170, 22)
        Me.Label8.TabIndex = 49
        Me.Label8.Text = "受注日"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtOrderDate1
        '
        Me.TxtOrderDate1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtOrderDate1.Location = New System.Drawing.Point(760, 42)
        Me.TxtOrderDate1.Name = "TxtOrderDate1"
        Me.TxtOrderDate1.Size = New System.Drawing.Size(170, 22)
        Me.TxtOrderDate1.TabIndex = 48
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(14, 126)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(170, 22)
        Me.Label4.TabIndex = 47
        Me.Label4.Text = "得意先コード"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtCustomerCode
        '
        Me.TxtCustomerCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerCode.Location = New System.Drawing.Point(190, 126)
        Me.TxtCustomerCode.Name = "TxtCustomerCode"
        Me.TxtCustomerCode.Size = New System.Drawing.Size(350, 22)
        Me.TxtCustomerCode.TabIndex = 46
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(14, 98)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(170, 22)
        Me.Label3.TabIndex = 45
        Me.Label3.Text = "電話番号"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtTel
        '
        Me.TxtTel.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtTel.Location = New System.Drawing.Point(190, 98)
        Me.TxtTel.Name = "TxtTel"
        Me.TxtTel.Size = New System.Drawing.Size(350, 22)
        Me.TxtTel.TabIndex = 44
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(14, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(170, 22)
        Me.Label2.TabIndex = 43
        Me.Label2.Text = "住所"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtAddress
        '
        Me.TxtAddress.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAddress.Location = New System.Drawing.Point(190, 70)
        Me.TxtAddress.Name = "TxtAddress"
        Me.TxtAddress.Size = New System.Drawing.Size(350, 22)
        Me.TxtAddress.TabIndex = 42
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(14, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(170, 22)
        Me.Label1.TabIndex = 41
        Me.Label1.Text = "得意先名"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblConditions
        '
        Me.LblConditions.AutoSize = True
        Me.LblConditions.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblConditions.Location = New System.Drawing.Point(11, 15)
        Me.LblConditions.Name = "LblConditions"
        Me.LblConditions.Size = New System.Drawing.Size(87, 15)
        Me.LblConditions.TabIndex = 40
        Me.LblConditions.Text = "■抽出条件"
        '
        'TxtCustomerName
        '
        Me.TxtCustomerName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerName.Location = New System.Drawing.Point(190, 42)
        Me.TxtCustomerName.Name = "TxtCustomerName"
        Me.TxtCustomerName.Size = New System.Drawing.Size(350, 22)
        Me.TxtCustomerName.TabIndex = 39
        '
        'BtnOrderSearch
        '
        Me.BtnOrderSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnOrderSearch.Location = New System.Drawing.Point(1174, 43)
        Me.BtnOrderSearch.Name = "BtnOrderSearch"
        Me.BtnOrderSearch.Size = New System.Drawing.Size(165, 40)
        Me.BtnOrderSearch.TabIndex = 38
        Me.BtnOrderSearch.Text = "検索"
        Me.BtnOrderSearch.UseVisualStyleBackColor = True
        '
        'BtnBack
        '
        Me.BtnBack.Location = New System.Drawing.Point(1173, 677)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 37
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'BtnOrderEdit
        '
        Me.BtnOrderEdit.Location = New System.Drawing.Point(831, 677)
        Me.BtnOrderEdit.Name = "BtnOrderEdit"
        Me.BtnOrderEdit.Size = New System.Drawing.Size(165, 40)
        Me.BtnOrderEdit.TabIndex = 35
        Me.BtnOrderEdit.Text = "受注修正"
        Me.BtnOrderEdit.UseVisualStyleBackColor = True
        '
        'DgvCymnhd
        '
        Me.DgvCymnhd.AllowUserToAddRows = False
        Me.DgvCymnhd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvCymnhd.Location = New System.Drawing.Point(13, 238)
        Me.DgvCymnhd.Name = "DgvCymnhd"
        Me.DgvCymnhd.ReadOnly = True
        Me.DgvCymnhd.RowTemplate.Height = 21
        Me.DgvCymnhd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvCymnhd.Size = New System.Drawing.Size(1326, 420)
        Me.DgvCymnhd.TabIndex = 33
        '
        'OrderList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1350, 729)
        Me.Controls.Add(Me.BtnOrderView)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TxtOrderNo2)
        Me.Controls.Add(Me.TxtOrderDate2)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TxtSales)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TxtOrderNo1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TxtOrderDate1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtCustomerCode)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtTel)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtAddress)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LblConditions)
        Me.Controls.Add(Me.TxtCustomerName)
        Me.Controls.Add(Me.BtnOrderSearch)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.BtnOrderEdit)
        Me.Controls.Add(Me.DgvCymnhd)
        Me.Name = "OrderList"
        Me.Text = "OrderList"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DgvCymnhd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnOrderView As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents RbtnSlip As RadioButton
    Friend WithEvents RbtnDetails As RadioButton
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents TxtOrderNo2 As TextBox
    Friend WithEvents TxtOrderDate2 As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TxtSales As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TxtOrderNo1 As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents TxtOrderDate1 As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TxtCustomerCode As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TxtTel As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtAddress As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents LblConditions As Label
    Friend WithEvents TxtCustomerName As TextBox
    Friend WithEvents BtnOrderSearch As Button
    Friend WithEvents BtnBack As Button
    Friend WithEvents BtnOrderEdit As Button
    Friend WithEvents DgvCymnhd As DataGridView
End Class
