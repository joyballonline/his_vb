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
        Me.LblCustomerName = New System.Windows.Forms.Label()
        Me.LblAddress = New System.Windows.Forms.Label()
        Me.TxtAddress = New System.Windows.Forms.TextBox()
        Me.LblTel = New System.Windows.Forms.Label()
        Me.TxtTel = New System.Windows.Forms.TextBox()
        Me.LblCustomerCode = New System.Windows.Forms.Label()
        Me.TxtCustomerCode = New System.Windows.Forms.TextBox()
        Me.LblSales = New System.Windows.Forms.Label()
        Me.TxtSales = New System.Windows.Forms.TextBox()
        Me.LblQuoteNo = New System.Windows.Forms.Label()
        Me.TxtQuoteNoSince = New System.Windows.Forms.TextBox()
        Me.LblQuoteDate = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.BtnQuoteView = New System.Windows.Forms.Button()
        Me.BtnUnitPrice = New System.Windows.Forms.Button()
        Me.BtnOrderPurchase = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.ChkExpired = New System.Windows.Forms.CheckBox()
        Me.BtnOrder = New System.Windows.Forms.Button()
        Me.BtnPurchase = New System.Windows.Forms.Button()
        Me.ChkCancel = New System.Windows.Forms.CheckBox()
        Me.LblMode = New System.Windows.Forms.Label()
        Me.RbtnDetails = New System.Windows.Forms.RadioButton()
        Me.RbtnSlip = New System.Windows.Forms.RadioButton()
        Me.TxtQuoteDateSince = New System.Windows.Forms.DateTimePicker()
        Me.TxtQuoteDateUntil = New System.Windows.Forms.DateTimePicker()
        Me.LblManufacturer = New System.Windows.Forms.Label()
        Me.TxtManufacturer = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.LblItemName = New System.Windows.Forms.Label()
        Me.TxtItemName = New System.Windows.Forms.TextBox()
        Me.LblSpec = New System.Windows.Forms.Label()
        Me.TxtSpec = New System.Windows.Forms.TextBox()
        CType(Me.DgvMithd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DgvMithd
        '
        Me.DgvMithd.AllowUserToAddRows = False
        Me.DgvMithd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvMithd.Location = New System.Drawing.Point(12, 241)
        Me.DgvMithd.Name = "DgvMithd"
        Me.DgvMithd.ReadOnly = True
        Me.DgvMithd.RowHeadersVisible = False
        Me.DgvMithd.RowTemplate.Height = 21
        Me.DgvMithd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvMithd.Size = New System.Drawing.Size(1326, 262)
        Me.DgvMithd.TabIndex = 15
        '
        'BtnQuoteAdd
        '
        Me.BtnQuoteAdd.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnQuoteAdd.Location = New System.Drawing.Point(308, 509)
        Me.BtnQuoteAdd.Name = "BtnQuoteAdd"
        Me.BtnQuoteAdd.Size = New System.Drawing.Size(165, 40)
        Me.BtnQuoteAdd.TabIndex = 17
        Me.BtnQuoteAdd.Text = "新規登録"
        Me.BtnQuoteAdd.UseVisualStyleBackColor = True
        Me.BtnQuoteAdd.Visible = False
        '
        'BtnQuoteEdit
        '
        Me.BtnQuoteEdit.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnQuoteEdit.Location = New System.Drawing.Point(650, 509)
        Me.BtnQuoteEdit.Name = "BtnQuoteEdit"
        Me.BtnQuoteEdit.Size = New System.Drawing.Size(165, 40)
        Me.BtnQuoteEdit.TabIndex = 19
        Me.BtnQuoteEdit.Text = "見積修正"
        Me.BtnQuoteEdit.UseVisualStyleBackColor = True
        Me.BtnQuoteEdit.Visible = False
        '
        'BtnQuoteClone
        '
        Me.BtnQuoteClone.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnQuoteClone.Location = New System.Drawing.Point(821, 509)
        Me.BtnQuoteClone.Name = "BtnQuoteClone"
        Me.BtnQuoteClone.Size = New System.Drawing.Size(165, 40)
        Me.BtnQuoteClone.TabIndex = 20
        Me.BtnQuoteClone.Text = "複製"
        Me.BtnQuoteClone.UseVisualStyleBackColor = True
        Me.BtnQuoteClone.Visible = False
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(1173, 509)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 22
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'BtnQuoteSearch
        '
        Me.BtnQuoteSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnQuoteSearch.Location = New System.Drawing.Point(1173, 46)
        Me.BtnQuoteSearch.Name = "BtnQuoteSearch"
        Me.BtnQuoteSearch.Size = New System.Drawing.Size(165, 40)
        Me.BtnQuoteSearch.TabIndex = 10
        Me.BtnQuoteSearch.Text = "検索"
        Me.BtnQuoteSearch.UseVisualStyleBackColor = True
        '
        'TxtCustomerName
        '
        Me.TxtCustomerName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerName.Location = New System.Drawing.Point(189, 35)
        Me.TxtCustomerName.Name = "TxtCustomerName"
        Me.TxtCustomerName.Size = New System.Drawing.Size(350, 22)
        Me.TxtCustomerName.TabIndex = 1
        '
        'LblConditions
        '
        Me.LblConditions.AutoSize = True
        Me.LblConditions.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblConditions.Location = New System.Drawing.Point(12, 13)
        Me.LblConditions.Name = "LblConditions"
        Me.LblConditions.Size = New System.Drawing.Size(87, 15)
        Me.LblConditions.TabIndex = 7
        Me.LblConditions.Text = "■抽出条件"
        '
        'LblCustomerName
        '
        Me.LblCustomerName.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblCustomerName.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCustomerName.Location = New System.Drawing.Point(13, 35)
        Me.LblCustomerName.Name = "LblCustomerName"
        Me.LblCustomerName.Size = New System.Drawing.Size(170, 22)
        Me.LblCustomerName.TabIndex = 8
        Me.LblCustomerName.Text = "得意先名"
        Me.LblCustomerName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblAddress
        '
        Me.LblAddress.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblAddress.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAddress.Location = New System.Drawing.Point(13, 63)
        Me.LblAddress.Name = "LblAddress"
        Me.LblAddress.Size = New System.Drawing.Size(170, 22)
        Me.LblAddress.TabIndex = 10
        Me.LblAddress.Text = "住所"
        Me.LblAddress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtAddress
        '
        Me.TxtAddress.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAddress.Location = New System.Drawing.Point(189, 63)
        Me.TxtAddress.Name = "TxtAddress"
        Me.TxtAddress.Size = New System.Drawing.Size(350, 22)
        Me.TxtAddress.TabIndex = 12
        '
        'LblTel
        '
        Me.LblTel.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblTel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblTel.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTel.Location = New System.Drawing.Point(13, 91)
        Me.LblTel.Name = "LblTel"
        Me.LblTel.Size = New System.Drawing.Size(170, 22)
        Me.LblTel.TabIndex = 12
        Me.LblTel.Text = "電話番号"
        Me.LblTel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtTel
        '
        Me.TxtTel.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtTel.Location = New System.Drawing.Point(189, 91)
        Me.TxtTel.Name = "TxtTel"
        Me.TxtTel.Size = New System.Drawing.Size(350, 22)
        Me.TxtTel.TabIndex = 3
        '
        'LblCustomerCode
        '
        Me.LblCustomerCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblCustomerCode.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCustomerCode.Location = New System.Drawing.Point(13, 119)
        Me.LblCustomerCode.Name = "LblCustomerCode"
        Me.LblCustomerCode.Size = New System.Drawing.Size(170, 22)
        Me.LblCustomerCode.TabIndex = 14
        Me.LblCustomerCode.Text = "得意先コード"
        Me.LblCustomerCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtCustomerCode
        '
        Me.TxtCustomerCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerCode.Location = New System.Drawing.Point(189, 119)
        Me.TxtCustomerCode.Name = "TxtCustomerCode"
        Me.TxtCustomerCode.Size = New System.Drawing.Size(350, 22)
        Me.TxtCustomerCode.TabIndex = 4
        '
        'LblSales
        '
        Me.LblSales.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblSales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblSales.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSales.Location = New System.Drawing.Point(13, 147)
        Me.LblSales.Name = "LblSales"
        Me.LblSales.Size = New System.Drawing.Size(170, 22)
        Me.LblSales.TabIndex = 20
        Me.LblSales.Text = "営業担当者"
        Me.LblSales.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtSales
        '
        Me.TxtSales.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSales.Location = New System.Drawing.Point(189, 147)
        Me.TxtSales.Name = "TxtSales"
        Me.TxtSales.Size = New System.Drawing.Size(350, 22)
        Me.TxtSales.TabIndex = 9
        '
        'LblQuoteNo
        '
        Me.LblQuoteNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblQuoteNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblQuoteNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblQuoteNo.Location = New System.Drawing.Point(583, 62)
        Me.LblQuoteNo.Name = "LblQuoteNo"
        Me.LblQuoteNo.Size = New System.Drawing.Size(170, 22)
        Me.LblQuoteNo.TabIndex = 18
        Me.LblQuoteNo.Text = "見積番号"
        Me.LblQuoteNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtQuoteNoSince
        '
        Me.TxtQuoteNoSince.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtQuoteNoSince.Location = New System.Drawing.Point(759, 62)
        Me.TxtQuoteNoSince.Name = "TxtQuoteNoSince"
        Me.TxtQuoteNoSince.Size = New System.Drawing.Size(369, 22)
        Me.TxtQuoteNoSince.TabIndex = 7
        '
        'LblQuoteDate
        '
        Me.LblQuoteDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblQuoteDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblQuoteDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblQuoteDate.Location = New System.Drawing.Point(583, 34)
        Me.LblQuoteDate.Name = "LblQuoteDate"
        Me.LblQuoteDate.Size = New System.Drawing.Size(170, 22)
        Me.LblQuoteDate.TabIndex = 16
        Me.LblQuoteDate.Text = "見積日"
        Me.LblQuoteDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(935, 41)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(17, 12)
        Me.Label5.TabIndex = 23
        Me.Label5.Text = "～"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(10, 182)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(87, 15)
        Me.Label10.TabIndex = 25
        Me.Label10.Text = "■表示形式"
        '
        'BtnQuoteView
        '
        Me.BtnQuoteView.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnQuoteView.Location = New System.Drawing.Point(997, 509)
        Me.BtnQuoteView.Name = "BtnQuoteView"
        Me.BtnQuoteView.Size = New System.Drawing.Size(165, 40)
        Me.BtnQuoteView.TabIndex = 21
        Me.BtnQuoteView.Text = "見積参照"
        Me.BtnQuoteView.UseVisualStyleBackColor = True
        Me.BtnQuoteView.Visible = False
        '
        'BtnUnitPrice
        '
        Me.BtnUnitPrice.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnUnitPrice.Location = New System.Drawing.Point(479, 509)
        Me.BtnUnitPrice.Name = "BtnUnitPrice"
        Me.BtnUnitPrice.Size = New System.Drawing.Size(165, 40)
        Me.BtnUnitPrice.TabIndex = 18
        Me.BtnUnitPrice.Text = "単価入力"
        Me.BtnUnitPrice.UseVisualStyleBackColor = True
        Me.BtnUnitPrice.Visible = False
        '
        'BtnOrderPurchase
        '
        Me.BtnOrderPurchase.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnOrderPurchase.Location = New System.Drawing.Point(137, 509)
        Me.BtnOrderPurchase.Name = "BtnOrderPurchase"
        Me.BtnOrderPurchase.Size = New System.Drawing.Size(165, 40)
        Me.BtnOrderPurchase.TabIndex = 16
        Me.BtnOrderPurchase.Text = "受発注登録"
        Me.BtnOrderPurchase.UseVisualStyleBackColor = True
        Me.BtnOrderPurchase.Visible = False
        '
        'BtnCancel
        '
        Me.BtnCancel.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnCancel.Location = New System.Drawing.Point(1360, 585)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(165, 40)
        Me.BtnCancel.TabIndex = 33
        Me.BtnCancel.Text = "見積取消"
        Me.BtnCancel.UseVisualStyleBackColor = True
        Me.BtnCancel.Visible = False
        '
        'ChkExpired
        '
        Me.ChkExpired.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ChkExpired.AutoSize = True
        Me.ChkExpired.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ChkExpired.Location = New System.Drawing.Point(215, 5)
        Me.ChkExpired.Name = "ChkExpired"
        Me.ChkExpired.Size = New System.Drawing.Size(221, 19)
        Me.ChkExpired.TabIndex = 13
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
        'ChkCancel
        '
        Me.ChkCancel.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ChkCancel.AutoSize = True
        Me.ChkCancel.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ChkCancel.Location = New System.Drawing.Point(442, 5)
        Me.ChkCancel.Name = "ChkCancel"
        Me.ChkCancel.Size = New System.Drawing.Size(139, 19)
        Me.ChkCancel.TabIndex = 14
        Me.ChkCancel.Text = "取消データを含める"
        Me.ChkCancel.UseVisualStyleBackColor = True
        '
        'LblMode
        '
        Me.LblMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMode.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMode.Location = New System.Drawing.Point(1105, 9)
        Me.LblMode.Name = "LblMode"
        Me.LblMode.Size = New System.Drawing.Size(233, 22)
        Me.LblMode.TabIndex = 34
        Me.LblMode.Text = "モード"
        Me.LblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RbtnDetails
        '
        Me.RbtnDetails.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RbtnDetails.AutoSize = True
        Me.RbtnDetails.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.RbtnDetails.Location = New System.Drawing.Point(98, 5)
        Me.RbtnDetails.Name = "RbtnDetails"
        Me.RbtnDetails.Size = New System.Drawing.Size(89, 19)
        Me.RbtnDetails.TabIndex = 12
        Me.RbtnDetails.Text = "明細単位"
        Me.RbtnDetails.UseVisualStyleBackColor = True
        '
        'RbtnSlip
        '
        Me.RbtnSlip.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RbtnSlip.AutoSize = True
        Me.RbtnSlip.Checked = True
        Me.RbtnSlip.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.RbtnSlip.Location = New System.Drawing.Point(3, 5)
        Me.RbtnSlip.Name = "RbtnSlip"
        Me.RbtnSlip.Size = New System.Drawing.Size(89, 19)
        Me.RbtnSlip.TabIndex = 11
        Me.RbtnSlip.TabStop = True
        Me.RbtnSlip.Text = "伝票単位"
        Me.RbtnSlip.UseVisualStyleBackColor = True
        '
        'TxtQuoteDateSince
        '
        Me.TxtQuoteDateSince.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtQuoteDateSince.CustomFormat = ""
        Me.TxtQuoteDateSince.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtQuoteDateSince.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.TxtQuoteDateSince.Location = New System.Drawing.Point(759, 36)
        Me.TxtQuoteDateSince.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.TxtQuoteDateSince.Name = "TxtQuoteDateSince"
        Me.TxtQuoteDateSince.Size = New System.Drawing.Size(170, 22)
        Me.TxtQuoteDateSince.TabIndex = 5
        Me.TxtQuoteDateSince.Value = New Date(2018, 6, 5, 23, 23, 58, 0)
        '
        'TxtQuoteDateUntil
        '
        Me.TxtQuoteDateUntil.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtQuoteDateUntil.CustomFormat = ""
        Me.TxtQuoteDateUntil.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtQuoteDateUntil.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.TxtQuoteDateUntil.Location = New System.Drawing.Point(958, 36)
        Me.TxtQuoteDateUntil.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.TxtQuoteDateUntil.Name = "TxtQuoteDateUntil"
        Me.TxtQuoteDateUntil.Size = New System.Drawing.Size(170, 22)
        Me.TxtQuoteDateUntil.TabIndex = 6
        Me.TxtQuoteDateUntil.Value = New Date(2018, 6, 5, 23, 23, 58, 0)
        '
        'LblManufacturer
        '
        Me.LblManufacturer.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblManufacturer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblManufacturer.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblManufacturer.Location = New System.Drawing.Point(583, 91)
        Me.LblManufacturer.Name = "LblManufacturer"
        Me.LblManufacturer.Size = New System.Drawing.Size(170, 22)
        Me.LblManufacturer.TabIndex = 38
        Me.LblManufacturer.Text = "メーカー"
        Me.LblManufacturer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtManufacturer
        '
        Me.TxtManufacturer.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtManufacturer.Location = New System.Drawing.Point(759, 91)
        Me.TxtManufacturer.Name = "TxtManufacturer"
        Me.TxtManufacturer.Size = New System.Drawing.Size(369, 22)
        Me.TxtManufacturer.TabIndex = 39
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel1.ColumnCount = 5
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.Controls.Add(Me.RbtnSlip, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.RbtnDetails, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ChkExpired, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ChkCancel, 4, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(12, 206)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(834, 29)
        Me.TableLayoutPanel1.TabIndex = 40
        '
        'LblItemName
        '
        Me.LblItemName.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblItemName.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblItemName.Location = New System.Drawing.Point(583, 119)
        Me.LblItemName.Name = "LblItemName"
        Me.LblItemName.Size = New System.Drawing.Size(170, 22)
        Me.LblItemName.TabIndex = 41
        Me.LblItemName.Text = "品名"
        Me.LblItemName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtItemName
        '
        Me.TxtItemName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtItemName.Location = New System.Drawing.Point(759, 119)
        Me.TxtItemName.Name = "TxtItemName"
        Me.TxtItemName.Size = New System.Drawing.Size(369, 22)
        Me.TxtItemName.TabIndex = 42
        '
        'LblSpec
        '
        Me.LblSpec.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblSpec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblSpec.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSpec.Location = New System.Drawing.Point(583, 147)
        Me.LblSpec.Name = "LblSpec"
        Me.LblSpec.Size = New System.Drawing.Size(170, 22)
        Me.LblSpec.TabIndex = 43
        Me.LblSpec.Text = "型式"
        Me.LblSpec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtSpec
        '
        Me.TxtSpec.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSpec.Location = New System.Drawing.Point(759, 147)
        Me.TxtSpec.Name = "TxtSpec"
        Me.TxtSpec.Size = New System.Drawing.Size(369, 22)
        Me.TxtSpec.TabIndex = 44
        '
        'QuoteList
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.TxtSpec)
        Me.Controls.Add(Me.LblSpec)
        Me.Controls.Add(Me.TxtItemName)
        Me.Controls.Add(Me.LblItemName)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.TxtManufacturer)
        Me.Controls.Add(Me.LblManufacturer)
        Me.Controls.Add(Me.TxtQuoteDateUntil)
        Me.Controls.Add(Me.TxtQuoteDateSince)
        Me.Controls.Add(Me.BtnPurchase)
        Me.Controls.Add(Me.BtnOrder)
        Me.Controls.Add(Me.LblMode)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOrderPurchase)
        Me.Controls.Add(Me.BtnUnitPrice)
        Me.Controls.Add(Me.BtnQuoteView)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.LblSales)
        Me.Controls.Add(Me.TxtSales)
        Me.Controls.Add(Me.LblQuoteNo)
        Me.Controls.Add(Me.TxtQuoteNoSince)
        Me.Controls.Add(Me.LblQuoteDate)
        Me.Controls.Add(Me.LblCustomerCode)
        Me.Controls.Add(Me.TxtCustomerCode)
        Me.Controls.Add(Me.LblTel)
        Me.Controls.Add(Me.TxtTel)
        Me.Controls.Add(Me.LblAddress)
        Me.Controls.Add(Me.TxtAddress)
        Me.Controls.Add(Me.LblCustomerName)
        Me.Controls.Add(Me.LblConditions)
        Me.Controls.Add(Me.TxtCustomerName)
        Me.Controls.Add(Me.BtnQuoteSearch)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.BtnQuoteClone)
        Me.Controls.Add(Me.BtnQuoteEdit)
        Me.Controls.Add(Me.BtnQuoteAdd)
        Me.Controls.Add(Me.DgvMithd)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "QuoteList"
        Me.Text = "QuoteList"
        CType(Me.DgvMithd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
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
    Friend WithEvents LblCustomerName As Label
    Friend WithEvents LblAddress As Label
    Friend WithEvents TxtAddress As TextBox
    Friend WithEvents LblTel As Label
    Friend WithEvents TxtTel As TextBox
    Friend WithEvents LblCustomerCode As Label
    Friend WithEvents TxtCustomerCode As TextBox
    Friend WithEvents LblSales As Label
    Friend WithEvents TxtSales As TextBox
    Friend WithEvents LblQuoteNo As Label
    Friend WithEvents TxtQuoteNoSince As TextBox
    Friend WithEvents LblQuoteDate As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents BtnQuoteView As Button
    Friend WithEvents BtnUnitPrice As Button
    Friend WithEvents BtnOrderPurchase As Button
    Friend WithEvents BtnCancel As Button
    Friend WithEvents ChkExpired As CheckBox
    Friend WithEvents BtnOrder As Button
    Friend WithEvents BtnPurchase As Button
    Friend WithEvents ChkCancel As CheckBox
    Friend WithEvents LblMode As Label
    Friend WithEvents RbtnDetails As RadioButton
    Friend WithEvents RbtnSlip As RadioButton
    Friend WithEvents TxtQuoteDateSince As DateTimePicker
    Friend WithEvents TxtQuoteDateUntil As DateTimePicker
    Friend WithEvents LblManufacturer As Label
    Friend WithEvents TxtManufacturer As TextBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents LblItemName As Label
    Friend WithEvents TxtItemName As TextBox
    Friend WithEvents LblSpec As Label
    Friend WithEvents TxtSpec As TextBox
End Class
