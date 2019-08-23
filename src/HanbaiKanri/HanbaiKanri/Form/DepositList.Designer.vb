<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DepositList
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DgvCustomer = New System.Windows.Forms.DataGridView()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.BtnDeposit = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtCustomerCode = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtTel = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtAddress = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblConditions = New System.Windows.Forms.Label()
        Me.TxtCustomerName = New System.Windows.Forms.TextBox()
        Me.BtnSerach = New System.Windows.Forms.Button()
        Me.LblMode = New System.Windows.Forms.Label()
        Me.ChkZeroData = New System.Windows.Forms.CheckBox()
        Me.TxtBillingDateUntil = New System.Windows.Forms.DateTimePicker()
        Me.TxtBillingDateSince = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.LblBillingDate = New System.Windows.Forms.Label()
        Me.得意先コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.得意先名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.通貨_外貨 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.売掛金額_外貨 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.既入金額_外貨 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.売掛残高_外貨 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求金額残_外貨 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.通貨 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.受注金額計 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求金額計 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求金額残 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.売掛残高 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.受注件数 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求件数 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.会社コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.通貨_外貨コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DgvCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DgvCustomer
        '
        Me.DgvCustomer.AllowUserToAddRows = False
        Me.DgvCustomer.AllowUserToDeleteRows = False
        Me.DgvCustomer.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvCustomer.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DgvCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvCustomer.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.得意先コード, Me.得意先名, Me.通貨_外貨, Me.売掛金額_外貨, Me.既入金額_外貨, Me.売掛残高_外貨, Me.請求金額残_外貨, Me.通貨, Me.受注金額計, Me.請求金額計, Me.請求金額残, Me.売掛残高, Me.受注件数, Me.請求件数, Me.会社コード, Me.通貨_外貨コード})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvCustomer.DefaultCellStyle = DataGridViewCellStyle4
        Me.DgvCustomer.Location = New System.Drawing.Point(12, 162)
        Me.DgvCustomer.MultiSelect = False
        Me.DgvCustomer.Name = "DgvCustomer"
        Me.DgvCustomer.ReadOnly = True
        Me.DgvCustomer.RowHeadersVisible = False
        Me.DgvCustomer.RowTemplate.Height = 21
        Me.DgvCustomer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvCustomer.Size = New System.Drawing.Size(1326, 341)
        Me.DgvCustomer.TabIndex = 6
        '
        'btnBack
        '
        Me.btnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.Location = New System.Drawing.Point(1173, 509)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(165, 40)
        Me.btnBack.TabIndex = 8
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'BtnDeposit
        '
        Me.BtnDeposit.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnDeposit.Location = New System.Drawing.Point(1002, 509)
        Me.BtnDeposit.Name = "BtnDeposit"
        Me.BtnDeposit.Size = New System.Drawing.Size(165, 40)
        Me.BtnDeposit.TabIndex = 7
        Me.BtnDeposit.Text = "入金入力"
        Me.BtnDeposit.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 120)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(170, 22)
        Me.Label4.TabIndex = 56
        Me.Label4.Text = "得意先コード"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtCustomerCode
        '
        Me.TxtCustomerCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerCode.Location = New System.Drawing.Point(188, 120)
        Me.TxtCustomerCode.Name = "TxtCustomerCode"
        Me.TxtCustomerCode.Size = New System.Drawing.Size(350, 22)
        Me.TxtCustomerCode.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 92)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(170, 22)
        Me.Label3.TabIndex = 54
        Me.Label3.Text = "電話番号"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtTel
        '
        Me.TxtTel.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtTel.Location = New System.Drawing.Point(188, 92)
        Me.TxtTel.Name = "TxtTel"
        Me.TxtTel.Size = New System.Drawing.Size(350, 22)
        Me.TxtTel.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(170, 22)
        Me.Label2.TabIndex = 52
        Me.Label2.Text = "住所"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtAddress
        '
        Me.TxtAddress.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAddress.Location = New System.Drawing.Point(188, 64)
        Me.TxtAddress.Name = "TxtAddress"
        Me.TxtAddress.Size = New System.Drawing.Size(350, 22)
        Me.TxtAddress.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(170, 22)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "得意先名"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblConditions
        '
        Me.LblConditions.AutoSize = True
        Me.LblConditions.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblConditions.Location = New System.Drawing.Point(9, 9)
        Me.LblConditions.Name = "LblConditions"
        Me.LblConditions.Size = New System.Drawing.Size(87, 15)
        Me.LblConditions.TabIndex = 49
        Me.LblConditions.Text = "■抽出条件"
        '
        'TxtCustomerName
        '
        Me.TxtCustomerName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerName.Location = New System.Drawing.Point(188, 36)
        Me.TxtCustomerName.Name = "TxtCustomerName"
        Me.TxtCustomerName.Size = New System.Drawing.Size(350, 22)
        Me.TxtCustomerName.TabIndex = 1
        '
        'BtnSerach
        '
        Me.BtnSerach.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnSerach.Location = New System.Drawing.Point(1172, 36)
        Me.BtnSerach.Name = "BtnSerach"
        Me.BtnSerach.Size = New System.Drawing.Size(166, 40)
        Me.BtnSerach.TabIndex = 5
        Me.BtnSerach.Text = "検索"
        Me.BtnSerach.UseVisualStyleBackColor = True
        '
        'LblMode
        '
        Me.LblMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMode.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMode.Location = New System.Drawing.Point(1105, 9)
        Me.LblMode.Name = "LblMode"
        Me.LblMode.Size = New System.Drawing.Size(233, 22)
        Me.LblMode.TabIndex = 324
        Me.LblMode.Text = "入金入力モード"
        Me.LblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ChkZeroData
        '
        Me.ChkZeroData.AutoSize = True
        Me.ChkZeroData.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ChkZeroData.Location = New System.Drawing.Point(577, 121)
        Me.ChkZeroData.Name = "ChkZeroData"
        Me.ChkZeroData.Size = New System.Drawing.Size(145, 19)
        Me.ChkZeroData.TabIndex = 325
        Me.ChkZeroData.Text = "売掛残高０を含める"
        Me.ChkZeroData.UseVisualStyleBackColor = True
        '
        'TxtBillingDateUntil
        '
        Me.TxtBillingDateUntil.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtBillingDateUntil.CustomFormat = ""
        Me.TxtBillingDateUntil.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtBillingDateUntil.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.TxtBillingDateUntil.Location = New System.Drawing.Point(949, 36)
        Me.TxtBillingDateUntil.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.TxtBillingDateUntil.Name = "TxtBillingDateUntil"
        Me.TxtBillingDateUntil.Size = New System.Drawing.Size(170, 22)
        Me.TxtBillingDateUntil.TabIndex = 327
        Me.TxtBillingDateUntil.Value = New Date(2018, 6, 5, 23, 23, 58, 0)
        '
        'TxtBillingDateSince
        '
        Me.TxtBillingDateSince.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtBillingDateSince.CustomFormat = ""
        Me.TxtBillingDateSince.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtBillingDateSince.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.TxtBillingDateSince.Location = New System.Drawing.Point(750, 36)
        Me.TxtBillingDateSince.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.TxtBillingDateSince.Name = "TxtBillingDateSince"
        Me.TxtBillingDateSince.Size = New System.Drawing.Size(170, 22)
        Me.TxtBillingDateSince.TabIndex = 326
        Me.TxtBillingDateSince.Value = New Date(2018, 6, 5, 23, 23, 58, 0)
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(926, 41)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(17, 12)
        Me.Label5.TabIndex = 329
        Me.Label5.Text = "～"
        '
        'LblBillingDate
        '
        Me.LblBillingDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblBillingDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblBillingDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblBillingDate.Location = New System.Drawing.Point(574, 36)
        Me.LblBillingDate.Name = "LblBillingDate"
        Me.LblBillingDate.Size = New System.Drawing.Size(170, 22)
        Me.LblBillingDate.TabIndex = 328
        Me.LblBillingDate.Text = "SalesInvoiceDate"
        Me.LblBillingDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        '得意先コード
        '
        Me.得意先コード.HeaderText = "得意先コード"
        Me.得意先コード.Name = "得意先コード"
        Me.得意先コード.ReadOnly = True
        Me.得意先コード.Width = 181
        '
        '得意先名
        '
        Me.得意先名.HeaderText = "得意先名"
        Me.得意先名.Name = "得意先名"
        Me.得意先名.ReadOnly = True
        Me.得意先名.Width = 151
        '
        '通貨_外貨
        '
        Me.通貨_外貨.HeaderText = "販売通貨"
        Me.通貨_外貨.Name = "通貨_外貨"
        Me.通貨_外貨.ReadOnly = True
        Me.通貨_外貨.Width = 103
        '
        '売掛金額_外貨
        '
        Me.売掛金額_外貨.HeaderText = "売掛金額_外貨"
        Me.売掛金額_外貨.Name = "売掛金額_外貨"
        Me.売掛金額_外貨.ReadOnly = True
        '
        '既入金額_外貨
        '
        Me.既入金額_外貨.HeaderText = "既入金額_外貨"
        Me.既入金額_外貨.Name = "既入金額_外貨"
        Me.既入金額_外貨.ReadOnly = True
        '
        '売掛残高_外貨
        '
        Me.売掛残高_外貨.HeaderText = "売掛残高(外貨)"
        Me.売掛残高_外貨.Name = "売掛残高_外貨"
        Me.売掛残高_外貨.ReadOnly = True
        Me.売掛残高_外貨.Width = 146
        '
        '請求金額残_外貨
        '
        Me.請求金額残_外貨.HeaderText = "請求金額残(外貨)"
        Me.請求金額残_外貨.Name = "請求金額残_外貨"
        Me.請求金額残_外貨.ReadOnly = True
        Me.請求金額残_外貨.Visible = False
        Me.請求金額残_外貨.Width = 168
        '
        '通貨
        '
        Me.通貨.HeaderText = "通貨"
        Me.通貨.Name = "通貨"
        Me.通貨.ReadOnly = True
        Me.通貨.Visible = False
        Me.通貨.Width = 97
        '
        '受注金額計
        '
        Me.受注金額計.HeaderText = "受注金額計"
        Me.受注金額計.Name = "受注金額計"
        Me.受注金額計.ReadOnly = True
        Me.受注金額計.Visible = False
        Me.受注金額計.Width = 140
        '
        '請求金額計
        '
        Me.請求金額計.HeaderText = "請求金額計"
        Me.請求金額計.Name = "請求金額計"
        Me.請求金額計.ReadOnly = True
        Me.請求金額計.Visible = False
        Me.請求金額計.Width = 140
        '
        '請求金額残
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.請求金額残.DefaultCellStyle = DataGridViewCellStyle2
        Me.請求金額残.HeaderText = "請求金額残"
        Me.請求金額残.Name = "請求金額残"
        Me.請求金額残.ReadOnly = True
        Me.請求金額残.Visible = False
        Me.請求金額残.Width = 140
        '
        '売掛残高
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.売掛残高.DefaultCellStyle = DataGridViewCellStyle3
        Me.売掛残高.HeaderText = "売掛残高"
        Me.売掛残高.Name = "売掛残高"
        Me.売掛残高.ReadOnly = True
        Me.売掛残高.Visible = False
        Me.売掛残高.Width = 118
        '
        '受注件数
        '
        Me.受注件数.HeaderText = "受注件数"
        Me.受注件数.Name = "受注件数"
        Me.受注件数.ReadOnly = True
        Me.受注件数.Visible = False
        Me.受注件数.Width = 118
        '
        '請求件数
        '
        Me.請求件数.HeaderText = "請求件数"
        Me.請求件数.Name = "請求件数"
        Me.請求件数.ReadOnly = True
        Me.請求件数.Visible = False
        Me.請求件数.Width = 118
        '
        '会社コード
        '
        Me.会社コード.HeaderText = "会社コード"
        Me.会社コード.Name = "会社コード"
        Me.会社コード.ReadOnly = True
        Me.会社コード.Visible = False
        Me.会社コード.Width = 112
        '
        '通貨_外貨コード
        '
        Me.通貨_外貨コード.HeaderText = "通貨_外貨コード"
        Me.通貨_外貨コード.Name = "通貨_外貨コード"
        Me.通貨_外貨コード.ReadOnly = True
        Me.通貨_外貨コード.Visible = False
        Me.通貨_外貨コード.Width = 146
        '
        'DepositList
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.TxtBillingDateUntil)
        Me.Controls.Add(Me.TxtBillingDateSince)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.LblBillingDate)
        Me.Controls.Add(Me.ChkZeroData)
        Me.Controls.Add(Me.LblMode)
        Me.Controls.Add(Me.BtnSerach)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtCustomerCode)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtTel)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtAddress)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LblConditions)
        Me.Controls.Add(Me.TxtCustomerName)
        Me.Controls.Add(Me.DgvCustomer)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.BtnDeposit)
        Me.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "DepositList"
        Me.Text = "DepositList"
        CType(Me.DgvCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DgvCustomer As DataGridView
    Friend WithEvents btnBack As Button
    Friend WithEvents BtnDeposit As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents TxtCustomerCode As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TxtTel As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtAddress As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents LblConditions As Label
    Friend WithEvents TxtCustomerName As TextBox
    Friend WithEvents BtnSerach As Button
    Friend WithEvents LblMode As Label
    Friend WithEvents ChkZeroData As CheckBox
    Friend WithEvents TxtBillingDateUntil As DateTimePicker
    Friend WithEvents TxtBillingDateSince As DateTimePicker
    Friend WithEvents Label5 As Label
    Friend WithEvents LblBillingDate As Label
    Friend WithEvents 得意先コード As DataGridViewTextBoxColumn
    Friend WithEvents 得意先名 As DataGridViewTextBoxColumn
    Friend WithEvents 通貨_外貨 As DataGridViewTextBoxColumn
    Friend WithEvents 売掛金額_外貨 As DataGridViewTextBoxColumn
    Friend WithEvents 既入金額_外貨 As DataGridViewTextBoxColumn
    Friend WithEvents 売掛残高_外貨 As DataGridViewTextBoxColumn
    Friend WithEvents 請求金額残_外貨 As DataGridViewTextBoxColumn
    Friend WithEvents 通貨 As DataGridViewTextBoxColumn
    Friend WithEvents 受注金額計 As DataGridViewTextBoxColumn
    Friend WithEvents 請求金額計 As DataGridViewTextBoxColumn
    Friend WithEvents 請求金額残 As DataGridViewTextBoxColumn
    Friend WithEvents 売掛残高 As DataGridViewTextBoxColumn
    Friend WithEvents 受注件数 As DataGridViewTextBoxColumn
    Friend WithEvents 請求件数 As DataGridViewTextBoxColumn
    Friend WithEvents 会社コード As DataGridViewTextBoxColumn
    Friend WithEvents 通貨_外貨コード As DataGridViewTextBoxColumn
End Class
