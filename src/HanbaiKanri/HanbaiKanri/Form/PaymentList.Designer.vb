<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PaymentList
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BtnSerach = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtCustomerCode = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtTel = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtAddress = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblConditions = New System.Windows.Forms.Label()
        Me.TxtCustomerName = New System.Windows.Forms.TextBox()
        Me.DgvSupplier = New System.Windows.Forms.DataGridView()
        Me.BtnPayment = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.LblMode = New System.Windows.Forms.Label()
        Me.ChkZeroData = New System.Windows.Forms.CheckBox()
        Me.TxtInvoiceDateUntil = New System.Windows.Forms.DateTimePicker()
        Me.TxtInvoiceDateSince = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.LblQuoteDate = New System.Windows.Forms.Label()
        Me.仕入先コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入先名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.通貨_外貨 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入金額計_外貨 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛金額計_外貨 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.支払残高_外貨 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.通貨 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入金額計 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛金額計 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.支払残高 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.会社コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.通貨_外貨コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DgvSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnSerach
        '
        Me.BtnSerach.Location = New System.Drawing.Point(1175, 35)
        Me.BtnSerach.Name = "BtnSerach"
        Me.BtnSerach.Size = New System.Drawing.Size(165, 40)
        Me.BtnSerach.TabIndex = 5
        Me.BtnSerach.Text = "検索"
        Me.BtnSerach.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(14, 119)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(170, 22)
        Me.Label4.TabIndex = 69
        Me.Label4.Text = "支払先コード"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtCustomerCode
        '
        Me.TxtCustomerCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerCode.Location = New System.Drawing.Point(190, 119)
        Me.TxtCustomerCode.MaxLength = 8
        Me.TxtCustomerCode.Name = "TxtCustomerCode"
        Me.TxtCustomerCode.Size = New System.Drawing.Size(350, 37)
        Me.TxtCustomerCode.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(14, 91)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(170, 22)
        Me.Label3.TabIndex = 67
        Me.Label3.Text = "電話番号"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtTel
        '
        Me.TxtTel.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtTel.Location = New System.Drawing.Point(190, 91)
        Me.TxtTel.MaxLength = 20
        Me.TxtTel.Name = "TxtTel"
        Me.TxtTel.Size = New System.Drawing.Size(350, 37)
        Me.TxtTel.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(14, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(170, 22)
        Me.Label2.TabIndex = 65
        Me.Label2.Text = "住所"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtAddress
        '
        Me.TxtAddress.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAddress.Location = New System.Drawing.Point(190, 63)
        Me.TxtAddress.MaxLength = 100
        Me.TxtAddress.Name = "TxtAddress"
        Me.TxtAddress.Size = New System.Drawing.Size(350, 37)
        Me.TxtAddress.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(14, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(170, 22)
        Me.Label1.TabIndex = 63
        Me.Label1.Text = "支払先名"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblConditions
        '
        Me.LblConditions.AutoSize = True
        Me.LblConditions.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblConditions.Location = New System.Drawing.Point(11, 8)
        Me.LblConditions.Name = "LblConditions"
        Me.LblConditions.Size = New System.Drawing.Size(163, 30)
        Me.LblConditions.TabIndex = 62
        Me.LblConditions.Text = "■抽出条件"
        '
        'TxtCustomerName
        '
        Me.TxtCustomerName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerName.Location = New System.Drawing.Point(190, 35)
        Me.TxtCustomerName.MaxLength = 100
        Me.TxtCustomerName.Name = "TxtCustomerName"
        Me.TxtCustomerName.Size = New System.Drawing.Size(350, 37)
        Me.TxtCustomerName.TabIndex = 1
        '
        'DgvSupplier
        '
        Me.DgvSupplier.AllowUserToAddRows = False
        Me.DgvSupplier.AllowUserToDeleteRows = False
        Me.DgvSupplier.AllowUserToResizeColumns = False
        Me.DgvSupplier.AllowUserToResizeRows = False
        Me.DgvSupplier.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvSupplier.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.仕入先コード, Me.仕入先名, Me.通貨_外貨, Me.仕入金額計_外貨, Me.買掛金額計_外貨, Me.支払残高_外貨, Me.通貨, Me.仕入金額計, Me.買掛金額計, Me.支払残高, Me.会社コード, Me.通貨_外貨コード})
        Me.DgvSupplier.Location = New System.Drawing.Point(14, 161)
        Me.DgvSupplier.MultiSelect = False
        Me.DgvSupplier.Name = "DgvSupplier"
        Me.DgvSupplier.ReadOnly = True
        Me.DgvSupplier.RowHeadersVisible = False
        Me.DgvSupplier.RowTemplate.Height = 21
        Me.DgvSupplier.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvSupplier.Size = New System.Drawing.Size(1326, 342)
        Me.DgvSupplier.TabIndex = 6
        '
        'BtnPayment
        '
        Me.BtnPayment.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnPayment.Location = New System.Drawing.Point(1004, 509)
        Me.BtnPayment.Name = "BtnPayment"
        Me.BtnPayment.Size = New System.Drawing.Size(165, 40)
        Me.BtnPayment.TabIndex = 7
        Me.BtnPayment.Text = "支払入力"
        Me.BtnPayment.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.Location = New System.Drawing.Point(1175, 509)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(165, 40)
        Me.btnBack.TabIndex = 8
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'LblMode
        '
        Me.LblMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMode.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMode.Location = New System.Drawing.Point(1107, 9)
        Me.LblMode.Name = "LblMode"
        Me.LblMode.Size = New System.Drawing.Size(233, 22)
        Me.LblMode.TabIndex = 306
        Me.LblMode.Text = "支払登録モード"
        Me.LblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ChkZeroData
        '
        Me.ChkZeroData.AutoSize = True
        Me.ChkZeroData.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ChkZeroData.Location = New System.Drawing.Point(577, 121)
        Me.ChkZeroData.Name = "ChkZeroData"
        Me.ChkZeroData.Size = New System.Drawing.Size(256, 34)
        Me.ChkZeroData.TabIndex = 307
        Me.ChkZeroData.Text = "支払残０を含める"
        Me.ChkZeroData.UseVisualStyleBackColor = True
        '
        'TxtInvoiceDateUntil
        '
        Me.TxtInvoiceDateUntil.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtInvoiceDateUntil.CustomFormat = ""
        Me.TxtInvoiceDateUntil.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtInvoiceDateUntil.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.TxtInvoiceDateUntil.Location = New System.Drawing.Point(952, 37)
        Me.TxtInvoiceDateUntil.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.TxtInvoiceDateUntil.Name = "TxtInvoiceDateUntil"
        Me.TxtInvoiceDateUntil.Size = New System.Drawing.Size(170, 37)
        Me.TxtInvoiceDateUntil.TabIndex = 309
        Me.TxtInvoiceDateUntil.Value = New Date(2018, 6, 5, 23, 23, 58, 0)
        '
        'TxtInvoiceDateSince
        '
        Me.TxtInvoiceDateSince.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtInvoiceDateSince.CustomFormat = ""
        Me.TxtInvoiceDateSince.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtInvoiceDateSince.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.TxtInvoiceDateSince.Location = New System.Drawing.Point(753, 37)
        Me.TxtInvoiceDateSince.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.TxtInvoiceDateSince.Name = "TxtInvoiceDateSince"
        Me.TxtInvoiceDateSince.Size = New System.Drawing.Size(170, 37)
        Me.TxtInvoiceDateSince.TabIndex = 308
        Me.TxtInvoiceDateSince.Value = New Date(2018, 6, 5, 23, 23, 58, 0)
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(929, 42)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(34, 24)
        Me.Label5.TabIndex = 311
        Me.Label5.Text = "～"
        '
        'LblQuoteDate
        '
        Me.LblQuoteDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblQuoteDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblQuoteDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblQuoteDate.Location = New System.Drawing.Point(577, 37)
        Me.LblQuoteDate.Name = "LblQuoteDate"
        Me.LblQuoteDate.Size = New System.Drawing.Size(170, 22)
        Me.LblQuoteDate.TabIndex = 310
        Me.LblQuoteDate.Text = "Supplier InvoiceDate"
        Me.LblQuoteDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        '仕入先コード
        '
        Me.仕入先コード.HeaderText = "仕入先コード"
        Me.仕入先コード.Name = "仕入先コード"
        Me.仕入先コード.ReadOnly = True
        '
        '仕入先名
        '
        Me.仕入先名.HeaderText = "仕入先名"
        Me.仕入先名.Name = "仕入先名"
        Me.仕入先名.ReadOnly = True
        Me.仕入先名.Width = 200
        '
        '通貨_外貨
        '
        Me.通貨_外貨.HeaderText = "仕入通貨"
        Me.通貨_外貨.Name = "通貨_外貨"
        Me.通貨_外貨.ReadOnly = True
        '
        '仕入金額計_外貨
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.仕入金額計_外貨.DefaultCellStyle = DataGridViewCellStyle1
        Me.仕入金額計_外貨.HeaderText = "仕入金額計(外貨)"
        Me.仕入金額計_外貨.Name = "仕入金額計_外貨"
        Me.仕入金額計_外貨.ReadOnly = True
        Me.仕入金額計_外貨.Width = 200
        '
        '買掛金額計_外貨
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.買掛金額計_外貨.DefaultCellStyle = DataGridViewCellStyle2
        Me.買掛金額計_外貨.HeaderText = "買掛金額計(外貨)"
        Me.買掛金額計_外貨.Name = "買掛金額計_外貨"
        Me.買掛金額計_外貨.ReadOnly = True
        Me.買掛金額計_外貨.Width = 200
        '
        '支払残高_外貨
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.支払残高_外貨.DefaultCellStyle = DataGridViewCellStyle3
        Me.支払残高_外貨.HeaderText = "支払残高(外貨)"
        Me.支払残高_外貨.Name = "支払残高_外貨"
        Me.支払残高_外貨.ReadOnly = True
        Me.支払残高_外貨.Width = 200
        '
        '通貨
        '
        Me.通貨.HeaderText = "通貨"
        Me.通貨.Name = "通貨"
        Me.通貨.ReadOnly = True
        Me.通貨.Visible = False
        '
        '仕入金額計
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.仕入金額計.DefaultCellStyle = DataGridViewCellStyle4
        Me.仕入金額計.HeaderText = "仕入金額計"
        Me.仕入金額計.Name = "仕入金額計"
        Me.仕入金額計.ReadOnly = True
        Me.仕入金額計.Visible = False
        Me.仕入金額計.Width = 200
        '
        '買掛金額計
        '
        Me.買掛金額計.HeaderText = "買掛金額計"
        Me.買掛金額計.Name = "買掛金額計"
        Me.買掛金額計.ReadOnly = True
        Me.買掛金額計.Visible = False
        Me.買掛金額計.Width = 200
        '
        '支払残高
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.支払残高.DefaultCellStyle = DataGridViewCellStyle5
        Me.支払残高.HeaderText = "支払残高"
        Me.支払残高.Name = "支払残高"
        Me.支払残高.ReadOnly = True
        Me.支払残高.Visible = False
        Me.支払残高.Width = 200
        '
        '会社コード
        '
        Me.会社コード.HeaderText = "会社コード"
        Me.会社コード.Name = "会社コード"
        Me.会社コード.ReadOnly = True
        Me.会社コード.Visible = False
        '
        '通貨_外貨コード
        '
        Me.通貨_外貨コード.HeaderText = "通貨_外貨コード"
        Me.通貨_外貨コード.Name = "通貨_外貨コード"
        Me.通貨_外貨コード.ReadOnly = True
        Me.通貨_外貨コード.Visible = False
        '
        'PaymentList
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.TxtInvoiceDateUntil)
        Me.Controls.Add(Me.TxtInvoiceDateSince)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.LblQuoteDate)
        Me.Controls.Add(Me.ChkZeroData)
        Me.Controls.Add(Me.LblMode)
        Me.Controls.Add(Me.DgvSupplier)
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
        Me.Controls.Add(Me.BtnPayment)
        Me.Controls.Add(Me.btnBack)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "PaymentList"
        Me.Text = "PaymentList"
        CType(Me.DgvSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnSerach As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents TxtCustomerCode As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TxtTel As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtAddress As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents LblConditions As Label
    Friend WithEvents TxtCustomerName As TextBox
    Friend WithEvents DgvSupplier As DataGridView
    Friend WithEvents BtnPayment As Button
    Friend WithEvents btnBack As Button
    Friend WithEvents LblMode As Label
    Friend WithEvents ChkZeroData As CheckBox
    Friend WithEvents TxtInvoiceDateUntil As DateTimePicker
    Friend WithEvents TxtInvoiceDateSince As DateTimePicker
    Friend WithEvents Label5 As Label
    Friend WithEvents LblQuoteDate As Label
    Friend WithEvents 仕入先コード As DataGridViewTextBoxColumn
    Friend WithEvents 仕入先名 As DataGridViewTextBoxColumn
    Friend WithEvents 通貨_外貨 As DataGridViewTextBoxColumn
    Friend WithEvents 仕入金額計_外貨 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛金額計_外貨 As DataGridViewTextBoxColumn
    Friend WithEvents 支払残高_外貨 As DataGridViewTextBoxColumn
    Friend WithEvents 通貨 As DataGridViewTextBoxColumn
    Friend WithEvents 仕入金額計 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛金額計 As DataGridViewTextBoxColumn
    Friend WithEvents 支払残高 As DataGridViewTextBoxColumn
    Friend WithEvents 会社コード As DataGridViewTextBoxColumn
    Friend WithEvents 通貨_外貨コード As DataGridViewTextBoxColumn
End Class
