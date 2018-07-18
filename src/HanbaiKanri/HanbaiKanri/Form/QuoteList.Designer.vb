<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class QuoteList
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
        Me.DgvMithd = New System.Windows.Forms.DataGridView()
        Me.BtnQuoteAdd = New System.Windows.Forms.Button()
        Me.BtnQuoteEdit = New System.Windows.Forms.Button()
        Me.BtnQuoteClone = New System.Windows.Forms.Button()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.BtnQuoteSearch = New System.Windows.Forms.Button()
        Me.TxtCustomerName = New System.Windows.Forms.TextBox()
        Me.LblConditions = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtAddress = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtTel = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtCustomerCode = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtSales = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TxtQuoteNo1 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TxtQuoteDate1 = New System.Windows.Forms.TextBox()
        Me.TxtQuoteDate2 = New System.Windows.Forms.TextBox()
        Me.TxtQuoteNo2 = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.RbtnSlip = New System.Windows.Forms.RadioButton()
        Me.RbtnDetails = New System.Windows.Forms.RadioButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.BtnQuoteView = New System.Windows.Forms.Button()
        Me.BtnUnitPrice = New System.Windows.Forms.Button()
        Me.BtnOrderAndPurchase = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.LblMode = New System.Windows.Forms.Label()
        Me.ChkExpired = New System.Windows.Forms.CheckBox()
        Me.BtnOrder = New System.Windows.Forms.Button()
        Me.BtnPurchase = New System.Windows.Forms.Button()
        CType(Me.DgvMithd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DgvMithd
        '
        Me.DgvMithd.AllowUserToAddRows = False
        Me.DgvMithd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvMithd.Location = New System.Drawing.Point(12, 241)
        Me.DgvMithd.Name = "DgvMithd"
        Me.DgvMithd.ReadOnly = True
        Me.DgvMithd.RowTemplate.Height = 21
        Me.DgvMithd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvMithd.Size = New System.Drawing.Size(1326, 420)
        Me.DgvMithd.TabIndex = 0
        '
        'BtnQuoteAdd
        '
        Me.BtnQuoteAdd.Location = New System.Drawing.Point(308, 677)
        Me.BtnQuoteAdd.Name = "BtnQuoteAdd"
        Me.BtnQuoteAdd.Size = New System.Drawing.Size(165, 40)
        Me.BtnQuoteAdd.TabIndex = 1
        Me.BtnQuoteAdd.Text = "新規登録"
        Me.BtnQuoteAdd.UseVisualStyleBackColor = True
        Me.BtnQuoteAdd.Visible = False
        '
        'BtnQuoteEdit
        '
        Me.BtnQuoteEdit.Location = New System.Drawing.Point(650, 677)
        Me.BtnQuoteEdit.Name = "BtnQuoteEdit"
        Me.BtnQuoteEdit.Size = New System.Drawing.Size(165, 40)
        Me.BtnQuoteEdit.TabIndex = 2
        Me.BtnQuoteEdit.Text = "見積修正"
        Me.BtnQuoteEdit.UseVisualStyleBackColor = True
        Me.BtnQuoteEdit.Visible = False
        '
        'BtnQuoteClone
        '
        Me.BtnQuoteClone.Location = New System.Drawing.Point(821, 677)
        Me.BtnQuoteClone.Name = "BtnQuoteClone"
        Me.BtnQuoteClone.Size = New System.Drawing.Size(165, 40)
        Me.BtnQuoteClone.TabIndex = 3
        Me.BtnQuoteClone.Text = "複製"
        Me.BtnQuoteClone.UseVisualStyleBackColor = True
        Me.BtnQuoteClone.Visible = False
        '
        'BtnBack
        '
        Me.BtnBack.Location = New System.Drawing.Point(1173, 677)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 4
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'BtnQuoteSearch
        '
        Me.BtnQuoteSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnQuoteSearch.Location = New System.Drawing.Point(1173, 46)
        Me.BtnQuoteSearch.Name = "BtnQuoteSearch"
        Me.BtnQuoteSearch.Size = New System.Drawing.Size(165, 40)
        Me.BtnQuoteSearch.TabIndex = 5
        Me.BtnQuoteSearch.Text = "検索"
        Me.BtnQuoteSearch.UseVisualStyleBackColor = True
        '
        'TxtCustomerName
        '
        Me.TxtCustomerName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerName.Location = New System.Drawing.Point(189, 45)
        Me.TxtCustomerName.Name = "TxtCustomerName"
        Me.TxtCustomerName.Size = New System.Drawing.Size(350, 22)
        Me.TxtCustomerName.TabIndex = 6
        '
        'LblConditions
        '
        Me.LblConditions.AutoSize = True
        Me.LblConditions.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblConditions.Location = New System.Drawing.Point(10, 18)
        Me.LblConditions.Name = "LblConditions"
        Me.LblConditions.Size = New System.Drawing.Size(87, 15)
        Me.LblConditions.TabIndex = 7
        Me.LblConditions.Text = "■抽出条件"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(170, 22)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "得意先名"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(13, 73)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(170, 22)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "住所"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtAddress
        '
        Me.TxtAddress.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAddress.Location = New System.Drawing.Point(189, 73)
        Me.TxtAddress.Name = "TxtAddress"
        Me.TxtAddress.Size = New System.Drawing.Size(350, 22)
        Me.TxtAddress.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(13, 101)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(170, 22)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "電話番号"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtTel
        '
        Me.TxtTel.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtTel.Location = New System.Drawing.Point(189, 101)
        Me.TxtTel.Name = "TxtTel"
        Me.TxtTel.Size = New System.Drawing.Size(350, 22)
        Me.TxtTel.TabIndex = 11
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(13, 129)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(170, 22)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "得意先コード"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtCustomerCode
        '
        Me.TxtCustomerCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerCode.Location = New System.Drawing.Point(189, 129)
        Me.TxtCustomerCode.Name = "TxtCustomerCode"
        Me.TxtCustomerCode.Size = New System.Drawing.Size(350, 22)
        Me.TxtCustomerCode.TabIndex = 13
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(583, 100)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(170, 22)
        Me.Label6.TabIndex = 20
        Me.Label6.Text = "営業担当者"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtSales
        '
        Me.TxtSales.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSales.Location = New System.Drawing.Point(759, 100)
        Me.TxtSales.Name = "TxtSales"
        Me.TxtSales.Size = New System.Drawing.Size(170, 22)
        Me.TxtSales.TabIndex = 19
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(583, 72)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(170, 22)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "見積番号"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtQuoteNo1
        '
        Me.TxtQuoteNo1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtQuoteNo1.Location = New System.Drawing.Point(759, 72)
        Me.TxtQuoteNo1.Name = "TxtQuoteNo1"
        Me.TxtQuoteNo1.Size = New System.Drawing.Size(170, 22)
        Me.TxtQuoteNo1.TabIndex = 17
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(583, 44)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(170, 22)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "見積日"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtQuoteDate1
        '
        Me.TxtQuoteDate1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtQuoteDate1.Location = New System.Drawing.Point(759, 45)
        Me.TxtQuoteDate1.Name = "TxtQuoteDate1"
        Me.TxtQuoteDate1.Size = New System.Drawing.Size(170, 22)
        Me.TxtQuoteDate1.TabIndex = 15
        '
        'TxtQuoteDate2
        '
        Me.TxtQuoteDate2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtQuoteDate2.Location = New System.Drawing.Point(958, 45)
        Me.TxtQuoteDate2.Name = "TxtQuoteDate2"
        Me.TxtQuoteDate2.Size = New System.Drawing.Size(170, 22)
        Me.TxtQuoteDate2.TabIndex = 21
        '
        'TxtQuoteNo2
        '
        Me.TxtQuoteNo2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtQuoteNo2.Location = New System.Drawing.Point(958, 73)
        Me.TxtQuoteNo2.Name = "TxtQuoteNo2"
        Me.TxtQuoteNo2.Size = New System.Drawing.Size(170, 22)
        Me.TxtQuoteNo2.TabIndex = 22
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(935, 51)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(17, 12)
        Me.Label5.TabIndex = 23
        Me.Label5.Text = "～"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(935, 79)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(17, 12)
        Me.Label9.TabIndex = 24
        Me.Label9.Text = "～"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(12, 167)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(87, 15)
        Me.Label10.TabIndex = 25
        Me.Label10.Text = "■表示形式"
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
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.RbtnSlip)
        Me.Panel1.Controls.Add(Me.RbtnDetails)
        Me.Panel1.Location = New System.Drawing.Point(15, 192)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(195, 37)
        Me.Panel1.TabIndex = 28
        '
        'BtnQuoteView
        '
        Me.BtnQuoteView.Location = New System.Drawing.Point(997, 677)
        Me.BtnQuoteView.Name = "BtnQuoteView"
        Me.BtnQuoteView.Size = New System.Drawing.Size(165, 40)
        Me.BtnQuoteView.TabIndex = 30
        Me.BtnQuoteView.Text = "見積参照"
        Me.BtnQuoteView.UseVisualStyleBackColor = True
        Me.BtnQuoteView.Visible = False
        '
        'BtnUnitPrice
        '
        Me.BtnUnitPrice.Location = New System.Drawing.Point(479, 677)
        Me.BtnUnitPrice.Name = "BtnUnitPrice"
        Me.BtnUnitPrice.Size = New System.Drawing.Size(165, 40)
        Me.BtnUnitPrice.TabIndex = 31
        Me.BtnUnitPrice.Text = "単価入力"
        Me.BtnUnitPrice.UseVisualStyleBackColor = True
        Me.BtnUnitPrice.Visible = False
        '
        'BtnOrderAndPurchase
        '
        Me.BtnOrderAndPurchase.Location = New System.Drawing.Point(137, 677)
        Me.BtnOrderAndPurchase.Name = "BtnOrderAndPurchase"
        Me.BtnOrderAndPurchase.Size = New System.Drawing.Size(165, 40)
        Me.BtnOrderAndPurchase.TabIndex = 32
        Me.BtnOrderAndPurchase.Text = "受発注登録"
        Me.BtnOrderAndPurchase.UseVisualStyleBackColor = True
        Me.BtnOrderAndPurchase.Visible = False
        '
        'BtnCancel
        '
        Me.BtnCancel.Location = New System.Drawing.Point(1360, 585)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(165, 40)
        Me.BtnCancel.TabIndex = 33
        Me.BtnCancel.Text = "見積取消"
        Me.BtnCancel.UseVisualStyleBackColor = True
        Me.BtnCancel.Visible = False
        '
        'LblMode
        '
        Me.LblMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMode.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMode.Location = New System.Drawing.Point(1173, 9)
        Me.LblMode.Name = "LblMode"
        Me.LblMode.Size = New System.Drawing.Size(165, 22)
        Me.LblMode.TabIndex = 34
        Me.LblMode.Text = "モード"
        Me.LblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ChkExpired
        '
        Me.ChkExpired.AutoSize = True
        Me.ChkExpired.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ChkExpired.Location = New System.Drawing.Point(288, 202)
        Me.ChkExpired.Name = "ChkExpired"
        Me.ChkExpired.Size = New System.Drawing.Size(221, 19)
        Me.ChkExpired.TabIndex = 35
        Me.ChkExpired.Text = "有効期限の切れたデータを含める"
        Me.ChkExpired.UseVisualStyleBackColor = True
        '
        'BtnOrder
        '
        Me.BtnOrder.Location = New System.Drawing.Point(1360, 677)
        Me.BtnOrder.Name = "BtnOrder"
        Me.BtnOrder.Size = New System.Drawing.Size(165, 40)
        Me.BtnOrder.TabIndex = 36
        Me.BtnOrder.Text = "受注登録"
        Me.BtnOrder.UseVisualStyleBackColor = True
        Me.BtnOrder.Visible = False
        '
        'BtnPurchase
        '
        Me.BtnPurchase.Location = New System.Drawing.Point(1360, 631)
        Me.BtnPurchase.Name = "BtnPurchase"
        Me.BtnPurchase.Size = New System.Drawing.Size(165, 40)
        Me.BtnPurchase.TabIndex = 37
        Me.BtnPurchase.Text = "発注登録"
        Me.BtnPurchase.UseVisualStyleBackColor = True
        Me.BtnPurchase.Visible = False
        '
        'QuoteList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1429, 729)
        Me.Controls.Add(Me.BtnPurchase)
        Me.Controls.Add(Me.BtnOrder)
        Me.Controls.Add(Me.ChkExpired)
        Me.Controls.Add(Me.LblMode)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOrderAndPurchase)
        Me.Controls.Add(Me.BtnUnitPrice)
        Me.Controls.Add(Me.BtnQuoteView)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TxtQuoteNo2)
        Me.Controls.Add(Me.TxtQuoteDate2)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TxtSales)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TxtQuoteNo1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TxtQuoteDate1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtCustomerCode)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtTel)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtAddress)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LblConditions)
        Me.Controls.Add(Me.TxtCustomerName)
        Me.Controls.Add(Me.BtnQuoteSearch)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.BtnQuoteClone)
        Me.Controls.Add(Me.BtnQuoteEdit)
        Me.Controls.Add(Me.BtnQuoteAdd)
        Me.Controls.Add(Me.DgvMithd)
        Me.Name = "QuoteList"
        Me.Text = "QuoteList"
        CType(Me.DgvMithd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DgvMithd As DataGridView
    Friend WithEvents BtnQuoteAdd As Button
    Friend WithEvents BtnQuoteEdit As Button
    Friend WithEvents BtnQuoteClone As Button
    Friend WithEvents BtnBack As Button
    Friend WithEvents BtnQuoteSearch As Button
    Friend WithEvents TxtCustomerName As TextBox
    Friend WithEvents LblConditions As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtAddress As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TxtTel As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TxtCustomerCode As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TxtSales As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TxtQuoteNo1 As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents TxtQuoteDate1 As TextBox
    Friend WithEvents TxtQuoteDate2 As TextBox
    Friend WithEvents TxtQuoteNo2 As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents RbtnSlip As RadioButton
    Friend WithEvents RbtnDetails As RadioButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents BtnQuoteView As Button
    Friend WithEvents BtnUnitPrice As Button
    Friend WithEvents BtnOrderAndPurchase As Button
    Friend WithEvents BtnCancel As Button
    Friend WithEvents LblMode As Label
    Friend WithEvents ChkExpired As CheckBox
    Friend WithEvents BtnOrder As Button
    Friend WithEvents BtnPurchase As Button
End Class
