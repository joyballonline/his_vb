<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Order
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TxtVat = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TxtOrderRemark = New System.Windows.Forms.TextBox()
        Me.DtpExpiration = New System.Windows.Forms.DateTimePicker()
        Me.DtpQuoteDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DtpQuoteRegistration = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtQuoteSuffix = New System.Windows.Forms.TextBox()
        Me.TxtQuoteNo = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.DtpOrderDate = New System.Windows.Forms.DateTimePicker()
        Me.TxtOrderAmount = New System.Windows.Forms.TextBox()
        Me.LblOrderAmount = New System.Windows.Forms.Label()
        Me.TxtGrossProfit = New System.Windows.Forms.TextBox()
        Me.LblGrossProfit = New System.Windows.Forms.Label()
        Me.TxtItemCount = New System.Windows.Forms.TextBox()
        Me.LblItemCount = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtCustomerCode = New System.Windows.Forms.TextBox()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.DtpOrderRegistration = New System.Windows.Forms.DateTimePicker()
        Me.BtnRegistration = New System.Windows.Forms.Button()
        Me.TxtAddress3 = New System.Windows.Forms.TextBox()
        Me.TxtAddress2 = New System.Windows.Forms.TextBox()
        Me.LblRegistration = New System.Windows.Forms.Label()
        Me.TxtSales = New System.Windows.Forms.TextBox()
        Me.LblSales = New System.Windows.Forms.Label()
        Me.TxtOrderSuffix = New System.Windows.Forms.TextBox()
        Me.TxtQuoteRemarks = New System.Windows.Forms.TextBox()
        Me.TxtPaymentTerms = New System.Windows.Forms.TextBox()
        Me.LblRemarks = New System.Windows.Forms.Label()
        Me.TxtPosition = New System.Windows.Forms.TextBox()
        Me.LblPaymentTerms = New System.Windows.Forms.Label()
        Me.TxtPostalCode2 = New System.Windows.Forms.TextBox()
        Me.LblPosition = New System.Windows.Forms.Label()
        Me.TxtTel = New System.Windows.Forms.TextBox()
        Me.LblPerson = New System.Windows.Forms.Label()
        Me.TxtAddress1 = New System.Windows.Forms.TextBox()
        Me.TxtPerson = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LblFax = New System.Windows.Forms.Label()
        Me.TxtFax = New System.Windows.Forms.TextBox()
        Me.TxtPostalCode1 = New System.Windows.Forms.TextBox()
        Me.LblTel = New System.Windows.Forms.Label()
        Me.LblAddress = New System.Windows.Forms.Label()
        Me.TxtCustomerName = New System.Windows.Forms.TextBox()
        Me.TxtInput = New System.Windows.Forms.TextBox()
        Me.LblCustomerName = New System.Windows.Forms.Label()
        Me.TxtOrderNo = New System.Windows.Forms.TextBox()
        Me.LblInput = New System.Windows.Forms.Label()
        Me.LblOrderDate = New System.Windows.Forms.Label()
        Me.LblOrderNo = New System.Windows.Forms.Label()
        Me.DgvItemList = New System.Windows.Forms.DataGridView()
        Me.No = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.メーカー = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.品名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.型式 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.数量 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.単位 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入先 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入値 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.間接費 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.売単価 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.売上金額 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.粗利額 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.粗利率 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.リードタイム = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.備考 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.出庫数 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.未出庫数 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DgvItemList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtVat
        '
        Me.TxtVat.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtVat.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtVat.Enabled = False
        Me.TxtVat.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtVat.Location = New System.Drawing.Point(1107, 588)
        Me.TxtVat.MaxLength = 10
        Me.TxtVat.Name = "TxtVat"
        Me.TxtVat.ReadOnly = True
        Me.TxtVat.Size = New System.Drawing.Size(231, 23)
        Me.TxtVat.TabIndex = 184
        Me.TxtVat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label11.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(1001, 588)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(100, 23)
        Me.Label11.TabIndex = 185
        Me.Label11.Text = "VAT"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label9.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(11, 588)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(110, 23)
        Me.Label9.TabIndex = 176
        Me.Label9.Text = "受注備考"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtOrderRemark
        '
        Me.TxtOrderRemark.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtOrderRemark.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtOrderRemark.Location = New System.Drawing.Point(127, 588)
        Me.TxtOrderRemark.MaxLength = 50
        Me.TxtOrderRemark.Name = "TxtOrderRemark"
        Me.TxtOrderRemark.Size = New System.Drawing.Size(476, 23)
        Me.TxtOrderRemark.TabIndex = 178
        '
        'DtpExpiration
        '
        Me.DtpExpiration.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpExpiration.CustomFormat = "yyyy/MM/dd"
        Me.DtpExpiration.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpExpiration.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpExpiration.Location = New System.Drawing.Point(933, 43)
        Me.DtpExpiration.Name = "DtpExpiration"
        Me.DtpExpiration.Size = New System.Drawing.Size(140, 22)
        Me.DtpExpiration.TabIndex = 175
        Me.DtpExpiration.TabStop = False
        Me.DtpExpiration.Value = New Date(2018, 6, 5, 23, 23, 58, 0)
        '
        'DtpQuoteDate
        '
        Me.DtpQuoteDate.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpQuoteDate.CustomFormat = "yyyy/MM/dd"
        Me.DtpQuoteDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpQuoteDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpQuoteDate.Location = New System.Drawing.Point(661, 43)
        Me.DtpQuoteDate.Name = "DtpQuoteDate"
        Me.DtpQuoteDate.Size = New System.Drawing.Size(150, 22)
        Me.DtpQuoteDate.TabIndex = 174
        Me.DtpQuoteDate.TabStop = False
        Me.DtpQuoteDate.Value = New Date(2018, 6, 5, 23, 23, 58, 0)
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(221, 49)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(11, 12)
        Me.Label1.TabIndex = 173
        Me.Label1.Text = "-"
        '
        'DtpQuoteRegistration
        '
        Me.DtpQuoteRegistration.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpQuoteRegistration.CustomFormat = "yyyy/MM/dd"
        Me.DtpQuoteRegistration.Enabled = False
        Me.DtpQuoteRegistration.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpQuoteRegistration.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpQuoteRegistration.Location = New System.Drawing.Point(391, 42)
        Me.DtpQuoteRegistration.Name = "DtpQuoteRegistration"
        Me.DtpQuoteRegistration.Size = New System.Drawing.Size(148, 22)
        Me.DtpQuoteRegistration.TabIndex = 166
        Me.DtpQuoteRegistration.TabStop = False
        Me.DtpQuoteRegistration.Value = New Date(2018, 6, 5, 23, 23, 58, 0)
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(817, 42)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(110, 23)
        Me.Label3.TabIndex = 172
        Me.Label3.Text = "見積有効期限"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(273, 42)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(112, 23)
        Me.Label5.TabIndex = 171
        Me.Label5.Text = "見積登録日"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtQuoteSuffix
        '
        Me.TxtQuoteSuffix.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtQuoteSuffix.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtQuoteSuffix.Enabled = False
        Me.TxtQuoteSuffix.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtQuoteSuffix.Location = New System.Drawing.Point(238, 42)
        Me.TxtQuoteSuffix.MaxLength = 1
        Me.TxtQuoteSuffix.Name = "TxtQuoteSuffix"
        Me.TxtQuoteSuffix.ReadOnly = True
        Me.TxtQuoteSuffix.Size = New System.Drawing.Size(29, 23)
        Me.TxtQuoteSuffix.TabIndex = 169
        Me.TxtQuoteSuffix.TabStop = False
        '
        'TxtQuoteNo
        '
        Me.TxtQuoteNo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtQuoteNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtQuoteNo.Enabled = False
        Me.TxtQuoteNo.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtQuoteNo.Location = New System.Drawing.Point(127, 42)
        Me.TxtQuoteNo.MaxLength = 8
        Me.TxtQuoteNo.Name = "TxtQuoteNo"
        Me.TxtQuoteNo.ReadOnly = True
        Me.TxtQuoteNo.Size = New System.Drawing.Size(88, 23)
        Me.TxtQuoteNo.TabIndex = 168
        Me.TxtQuoteNo.TabStop = False
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(545, 42)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(110, 23)
        Me.Label6.TabIndex = 170
        Me.Label6.Text = "見積日"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(11, 42)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(110, 23)
        Me.Label7.TabIndex = 167
        Me.Label7.Text = "見積番号"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DtpOrderDate
        '
        Me.DtpOrderDate.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpOrderDate.CustomFormat = "yyyy/MM/dd"
        Me.DtpOrderDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpOrderDate.Location = New System.Drawing.Point(391, 14)
        Me.DtpOrderDate.Name = "DtpOrderDate"
        Me.DtpOrderDate.Size = New System.Drawing.Size(148, 22)
        Me.DtpOrderDate.TabIndex = 164
        Me.DtpOrderDate.TabStop = False
        Me.DtpOrderDate.Value = New Date(2018, 6, 5, 23, 23, 58, 0)
        '
        'TxtOrderAmount
        '
        Me.TxtOrderAmount.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtOrderAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtOrderAmount.Enabled = False
        Me.TxtOrderAmount.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtOrderAmount.Location = New System.Drawing.Point(1107, 617)
        Me.TxtOrderAmount.MaxLength = 10
        Me.TxtOrderAmount.Name = "TxtOrderAmount"
        Me.TxtOrderAmount.ReadOnly = True
        Me.TxtOrderAmount.Size = New System.Drawing.Size(231, 23)
        Me.TxtOrderAmount.TabIndex = 162
        Me.TxtOrderAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LblOrderAmount
        '
        Me.LblOrderAmount.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblOrderAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblOrderAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblOrderAmount.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblOrderAmount.Location = New System.Drawing.Point(1001, 617)
        Me.LblOrderAmount.Name = "LblOrderAmount"
        Me.LblOrderAmount.Size = New System.Drawing.Size(100, 23)
        Me.LblOrderAmount.TabIndex = 163
        Me.LblOrderAmount.Text = "受注金額"
        Me.LblOrderAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtGrossProfit
        '
        Me.TxtGrossProfit.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtGrossProfit.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtGrossProfit.Enabled = False
        Me.TxtGrossProfit.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtGrossProfit.Location = New System.Drawing.Point(1109, 646)
        Me.TxtGrossProfit.MaxLength = 10
        Me.TxtGrossProfit.Name = "TxtGrossProfit"
        Me.TxtGrossProfit.ReadOnly = True
        Me.TxtGrossProfit.Size = New System.Drawing.Size(231, 23)
        Me.TxtGrossProfit.TabIndex = 160
        Me.TxtGrossProfit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LblGrossProfit
        '
        Me.LblGrossProfit.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblGrossProfit.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblGrossProfit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblGrossProfit.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblGrossProfit.Location = New System.Drawing.Point(1003, 646)
        Me.LblGrossProfit.Name = "LblGrossProfit"
        Me.LblGrossProfit.Size = New System.Drawing.Size(100, 23)
        Me.LblGrossProfit.TabIndex = 161
        Me.LblGrossProfit.Text = "粗利額"
        Me.LblGrossProfit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtItemCount
        '
        Me.TxtItemCount.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtItemCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtItemCount.Enabled = False
        Me.TxtItemCount.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtItemCount.Location = New System.Drawing.Point(1274, 216)
        Me.TxtItemCount.MaxLength = 20
        Me.TxtItemCount.Name = "TxtItemCount"
        Me.TxtItemCount.ReadOnly = True
        Me.TxtItemCount.Size = New System.Drawing.Size(66, 23)
        Me.TxtItemCount.TabIndex = 158
        Me.TxtItemCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LblItemCount
        '
        Me.LblItemCount.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblItemCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblItemCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblItemCount.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblItemCount.Location = New System.Drawing.Point(1274, 187)
        Me.LblItemCount.Name = "LblItemCount"
        Me.LblItemCount.Size = New System.Drawing.Size(66, 23)
        Me.LblItemCount.TabIndex = 159
        Me.LblItemCount.Text = "明細数"
        Me.LblItemCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(221, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(11, 12)
        Me.Label2.TabIndex = 157
        Me.Label2.Text = "-"
        '
        'TxtCustomerCode
        '
        Me.TxtCustomerCode.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtCustomerCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtCustomerCode.Enabled = False
        Me.TxtCustomerCode.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerCode.Location = New System.Drawing.Point(127, 71)
        Me.TxtCustomerCode.MaxLength = 50
        Me.TxtCustomerCode.Name = "TxtCustomerCode"
        Me.TxtCustomerCode.Size = New System.Drawing.Size(140, 23)
        Me.TxtCustomerCode.TabIndex = 126
        '
        'BtnBack
        '
        Me.BtnBack.Location = New System.Drawing.Point(1175, 675)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 151
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'DtpOrderRegistration
        '
        Me.DtpOrderRegistration.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpOrderRegistration.CustomFormat = "yyyy/MM/dd"
        Me.DtpOrderRegistration.Enabled = False
        Me.DtpOrderRegistration.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpOrderRegistration.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpOrderRegistration.Location = New System.Drawing.Point(661, 13)
        Me.DtpOrderRegistration.Name = "DtpOrderRegistration"
        Me.DtpOrderRegistration.Size = New System.Drawing.Size(150, 22)
        Me.DtpOrderRegistration.TabIndex = 119
        Me.DtpOrderRegistration.TabStop = False
        Me.DtpOrderRegistration.Value = New Date(2018, 6, 5, 23, 23, 58, 0)
        '
        'BtnRegistration
        '
        Me.BtnRegistration.Location = New System.Drawing.Point(1003, 675)
        Me.BtnRegistration.Name = "BtnRegistration"
        Me.BtnRegistration.Size = New System.Drawing.Size(165, 40)
        Me.BtnRegistration.TabIndex = 150
        Me.BtnRegistration.Text = "登録"
        Me.BtnRegistration.UseVisualStyleBackColor = True
        '
        'TxtAddress3
        '
        Me.TxtAddress3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtAddress3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtAddress3.Enabled = False
        Me.TxtAddress3.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAddress3.Location = New System.Drawing.Point(273, 158)
        Me.TxtAddress3.MaxLength = 100
        Me.TxtAddress3.Name = "TxtAddress3"
        Me.TxtAddress3.Size = New System.Drawing.Size(330, 23)
        Me.TxtAddress3.TabIndex = 138
        '
        'TxtAddress2
        '
        Me.TxtAddress2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtAddress2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtAddress2.Enabled = False
        Me.TxtAddress2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAddress2.Location = New System.Drawing.Point(273, 129)
        Me.TxtAddress2.MaxLength = 100
        Me.TxtAddress2.Name = "TxtAddress2"
        Me.TxtAddress2.Size = New System.Drawing.Size(330, 23)
        Me.TxtAddress2.TabIndex = 137
        '
        'LblRegistration
        '
        Me.LblRegistration.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblRegistration.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblRegistration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblRegistration.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblRegistration.Location = New System.Drawing.Point(545, 13)
        Me.LblRegistration.Name = "LblRegistration"
        Me.LblRegistration.Size = New System.Drawing.Size(110, 23)
        Me.LblRegistration.TabIndex = 154
        Me.LblRegistration.Text = "受注登録日"
        Me.LblRegistration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtSales
        '
        Me.TxtSales.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtSales.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtSales.Enabled = False
        Me.TxtSales.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSales.Location = New System.Drawing.Point(1127, 71)
        Me.TxtSales.MaxLength = 20
        Me.TxtSales.Name = "TxtSales"
        Me.TxtSales.Size = New System.Drawing.Size(200, 23)
        Me.TxtSales.TabIndex = 146
        '
        'LblSales
        '
        Me.LblSales.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblSales.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblSales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblSales.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSales.Location = New System.Drawing.Point(971, 71)
        Me.LblSales.Name = "LblSales"
        Me.LblSales.Size = New System.Drawing.Size(150, 23)
        Me.LblSales.TabIndex = 153
        Me.LblSales.Text = "営業担当者"
        Me.LblSales.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtOrderSuffix
        '
        Me.TxtOrderSuffix.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtOrderSuffix.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtOrderSuffix.Enabled = False
        Me.TxtOrderSuffix.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtOrderSuffix.Location = New System.Drawing.Point(238, 13)
        Me.TxtOrderSuffix.MaxLength = 1
        Me.TxtOrderSuffix.Name = "TxtOrderSuffix"
        Me.TxtOrderSuffix.ReadOnly = True
        Me.TxtOrderSuffix.Size = New System.Drawing.Size(29, 23)
        Me.TxtOrderSuffix.TabIndex = 129
        Me.TxtOrderSuffix.TabStop = False
        '
        'TxtQuoteRemarks
        '
        Me.TxtQuoteRemarks.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtQuoteRemarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtQuoteRemarks.Enabled = False
        Me.TxtQuoteRemarks.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtQuoteRemarks.Location = New System.Drawing.Point(127, 216)
        Me.TxtQuoteRemarks.MaxLength = 100
        Me.TxtQuoteRemarks.Multiline = True
        Me.TxtQuoteRemarks.Name = "TxtQuoteRemarks"
        Me.TxtQuoteRemarks.Size = New System.Drawing.Size(476, 23)
        Me.TxtQuoteRemarks.TabIndex = 148
        '
        'TxtPaymentTerms
        '
        Me.TxtPaymentTerms.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtPaymentTerms.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtPaymentTerms.Enabled = False
        Me.TxtPaymentTerms.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPaymentTerms.Location = New System.Drawing.Point(127, 187)
        Me.TxtPaymentTerms.MaxLength = 50
        Me.TxtPaymentTerms.Multiline = True
        Me.TxtPaymentTerms.Name = "TxtPaymentTerms"
        Me.TxtPaymentTerms.Size = New System.Drawing.Size(476, 23)
        Me.TxtPaymentTerms.TabIndex = 147
        '
        'LblRemarks
        '
        Me.LblRemarks.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblRemarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblRemarks.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblRemarks.Location = New System.Drawing.Point(11, 216)
        Me.LblRemarks.Name = "LblRemarks"
        Me.LblRemarks.Size = New System.Drawing.Size(110, 23)
        Me.LblRemarks.TabIndex = 122
        Me.LblRemarks.Text = "見積備考"
        Me.LblRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtPosition
        '
        Me.TxtPosition.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtPosition.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtPosition.Enabled = False
        Me.TxtPosition.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPosition.Location = New System.Drawing.Point(765, 158)
        Me.TxtPosition.MaxLength = 50
        Me.TxtPosition.Name = "TxtPosition"
        Me.TxtPosition.Size = New System.Drawing.Size(200, 23)
        Me.TxtPosition.TabIndex = 145
        '
        'LblPaymentTerms
        '
        Me.LblPaymentTerms.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblPaymentTerms.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblPaymentTerms.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblPaymentTerms.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPaymentTerms.Location = New System.Drawing.Point(11, 187)
        Me.LblPaymentTerms.Name = "LblPaymentTerms"
        Me.LblPaymentTerms.Size = New System.Drawing.Size(110, 23)
        Me.LblPaymentTerms.TabIndex = 123
        Me.LblPaymentTerms.Text = "支払条件"
        Me.LblPaymentTerms.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtPostalCode2
        '
        Me.TxtPostalCode2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtPostalCode2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtPostalCode2.Enabled = False
        Me.TxtPostalCode2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPostalCode2.Location = New System.Drawing.Point(204, 100)
        Me.TxtPostalCode2.MaxLength = 4
        Me.TxtPostalCode2.Name = "TxtPostalCode2"
        Me.TxtPostalCode2.Size = New System.Drawing.Size(63, 23)
        Me.TxtPostalCode2.TabIndex = 133
        Me.TxtPostalCode2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LblPosition
        '
        Me.LblPosition.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblPosition.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblPosition.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPosition.Location = New System.Drawing.Point(609, 158)
        Me.LblPosition.Name = "LblPosition"
        Me.LblPosition.Size = New System.Drawing.Size(150, 23)
        Me.LblPosition.TabIndex = 134
        Me.LblPosition.Text = "得意先担当者役職"
        Me.LblPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtTel
        '
        Me.TxtTel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtTel.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtTel.Enabled = False
        Me.TxtTel.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtTel.Location = New System.Drawing.Point(765, 71)
        Me.TxtTel.MaxLength = 15
        Me.TxtTel.Name = "TxtTel"
        Me.TxtTel.Size = New System.Drawing.Size(200, 23)
        Me.TxtTel.TabIndex = 142
        '
        'LblPerson
        '
        Me.LblPerson.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblPerson.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblPerson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblPerson.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPerson.Location = New System.Drawing.Point(609, 129)
        Me.LblPerson.Name = "LblPerson"
        Me.LblPerson.Size = New System.Drawing.Size(150, 23)
        Me.LblPerson.TabIndex = 130
        Me.LblPerson.Text = "得意先担当者名"
        Me.LblPerson.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtAddress1
        '
        Me.TxtAddress1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtAddress1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtAddress1.Enabled = False
        Me.TxtAddress1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAddress1.Location = New System.Drawing.Point(273, 100)
        Me.TxtAddress1.MaxLength = 100
        Me.TxtAddress1.Name = "TxtAddress1"
        Me.TxtAddress1.Size = New System.Drawing.Size(330, 23)
        Me.TxtAddress1.TabIndex = 135
        '
        'TxtPerson
        '
        Me.TxtPerson.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtPerson.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtPerson.Enabled = False
        Me.TxtPerson.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPerson.Location = New System.Drawing.Point(765, 129)
        Me.TxtPerson.MaxLength = 50
        Me.TxtPerson.Name = "TxtPerson"
        Me.TxtPerson.Size = New System.Drawing.Size(200, 23)
        Me.TxtPerson.TabIndex = 144
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(187, 107)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(11, 12)
        Me.Label4.TabIndex = 141
        Me.Label4.Text = "-"
        '
        'LblFax
        '
        Me.LblFax.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblFax.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblFax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblFax.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblFax.Location = New System.Drawing.Point(609, 100)
        Me.LblFax.Name = "LblFax"
        Me.LblFax.Size = New System.Drawing.Size(150, 23)
        Me.LblFax.TabIndex = 125
        Me.LblFax.Text = "FAX番号"
        Me.LblFax.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtFax
        '
        Me.TxtFax.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtFax.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtFax.Enabled = False
        Me.TxtFax.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtFax.Location = New System.Drawing.Point(765, 100)
        Me.TxtFax.MaxLength = 15
        Me.TxtFax.Name = "TxtFax"
        Me.TxtFax.Size = New System.Drawing.Size(200, 23)
        Me.TxtFax.TabIndex = 143
        '
        'TxtPostalCode1
        '
        Me.TxtPostalCode1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtPostalCode1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtPostalCode1.Enabled = False
        Me.TxtPostalCode1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPostalCode1.Location = New System.Drawing.Point(127, 100)
        Me.TxtPostalCode1.MaxLength = 3
        Me.TxtPostalCode1.Name = "TxtPostalCode1"
        Me.TxtPostalCode1.Size = New System.Drawing.Size(54, 23)
        Me.TxtPostalCode1.TabIndex = 131
        Me.TxtPostalCode1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LblTel
        '
        Me.LblTel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblTel.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblTel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblTel.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTel.Location = New System.Drawing.Point(609, 71)
        Me.LblTel.Name = "LblTel"
        Me.LblTel.Size = New System.Drawing.Size(150, 23)
        Me.LblTel.TabIndex = 120
        Me.LblTel.Text = "電話番号"
        Me.LblTel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblAddress
        '
        Me.LblAddress.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblAddress.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblAddress.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAddress.Location = New System.Drawing.Point(11, 100)
        Me.LblAddress.Name = "LblAddress"
        Me.LblAddress.Size = New System.Drawing.Size(110, 81)
        Me.LblAddress.TabIndex = 136
        Me.LblAddress.Text = "住所"
        Me.LblAddress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtCustomerName
        '
        Me.TxtCustomerName.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtCustomerName.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtCustomerName.Enabled = False
        Me.TxtCustomerName.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerName.Location = New System.Drawing.Point(273, 71)
        Me.TxtCustomerName.MaxLength = 50
        Me.TxtCustomerName.Name = "TxtCustomerName"
        Me.TxtCustomerName.Size = New System.Drawing.Size(330, 23)
        Me.TxtCustomerName.TabIndex = 128
        '
        'TxtInput
        '
        Me.TxtInput.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtInput.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtInput.Enabled = False
        Me.TxtInput.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtInput.Location = New System.Drawing.Point(1127, 100)
        Me.TxtInput.MaxLength = 20
        Me.TxtInput.Name = "TxtInput"
        Me.TxtInput.ReadOnly = True
        Me.TxtInput.Size = New System.Drawing.Size(200, 23)
        Me.TxtInput.TabIndex = 132
        Me.TxtInput.TabStop = False
        '
        'LblCustomerName
        '
        Me.LblCustomerName.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblCustomerName.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblCustomerName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCustomerName.Location = New System.Drawing.Point(11, 71)
        Me.LblCustomerName.Name = "LblCustomerName"
        Me.LblCustomerName.Size = New System.Drawing.Size(110, 23)
        Me.LblCustomerName.TabIndex = 121
        Me.LblCustomerName.Text = "得意先名称"
        Me.LblCustomerName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtOrderNo
        '
        Me.TxtOrderNo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtOrderNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtOrderNo.Enabled = False
        Me.TxtOrderNo.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtOrderNo.Location = New System.Drawing.Point(127, 13)
        Me.TxtOrderNo.MaxLength = 8
        Me.TxtOrderNo.Name = "TxtOrderNo"
        Me.TxtOrderNo.ReadOnly = True
        Me.TxtOrderNo.Size = New System.Drawing.Size(88, 23)
        Me.TxtOrderNo.TabIndex = 127
        Me.TxtOrderNo.TabStop = False
        '
        'LblInput
        '
        Me.LblInput.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblInput.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblInput.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblInput.Location = New System.Drawing.Point(971, 100)
        Me.LblInput.Name = "LblInput"
        Me.LblInput.Size = New System.Drawing.Size(150, 23)
        Me.LblInput.TabIndex = 140
        Me.LblInput.Text = "入力担当者"
        Me.LblInput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblOrderDate
        '
        Me.LblOrderDate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblOrderDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblOrderDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblOrderDate.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblOrderDate.Location = New System.Drawing.Point(273, 13)
        Me.LblOrderDate.Name = "LblOrderDate"
        Me.LblOrderDate.Size = New System.Drawing.Size(112, 23)
        Me.LblOrderDate.TabIndex = 139
        Me.LblOrderDate.Text = "受注日"
        Me.LblOrderDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblOrderNo
        '
        Me.LblOrderNo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LblOrderNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblOrderNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblOrderNo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblOrderNo.Location = New System.Drawing.Point(11, 13)
        Me.LblOrderNo.Name = "LblOrderNo"
        Me.LblOrderNo.Size = New System.Drawing.Size(110, 23)
        Me.LblOrderNo.TabIndex = 124
        Me.LblOrderNo.Text = "受注番号"
        Me.LblOrderNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DgvItemList
        '
        Me.DgvItemList.AllowUserToAddRows = False
        Me.DgvItemList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DgvItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvItemList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.No, Me.メーカー, Me.品名, Me.型式, Me.数量, Me.単位, Me.仕入先, Me.仕入値, Me.間接費, Me.売単価, Me.売上金額, Me.粗利額, Me.粗利率, Me.リードタイム, Me.備考, Me.出庫数, Me.未出庫数})
        Me.DgvItemList.Location = New System.Drawing.Point(11, 245)
        Me.DgvItemList.Name = "DgvItemList"
        Me.DgvItemList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DgvItemList.RowTemplate.Height = 21
        Me.DgvItemList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvItemList.Size = New System.Drawing.Size(1329, 337)
        Me.DgvItemList.TabIndex = 149
        '
        'No
        '
        Me.No.HeaderText = "No"
        Me.No.Name = "No"
        Me.No.Width = 43
        '
        'メーカー
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.メーカー.DefaultCellStyle = DataGridViewCellStyle1
        Me.メーカー.HeaderText = "メーカー"
        Me.メーカー.Name = "メーカー"
        Me.メーカー.Width = 220
        '
        '品名
        '
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.品名.DefaultCellStyle = DataGridViewCellStyle2
        Me.品名.HeaderText = "品名"
        Me.品名.Name = "品名"
        Me.品名.Width = 220
        '
        '型式
        '
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.型式.DefaultCellStyle = DataGridViewCellStyle3
        Me.型式.HeaderText = "型式"
        Me.型式.Name = "型式"
        Me.型式.Width = 230
        '
        '数量
        '
        Me.数量.HeaderText = "数量"
        Me.数量.Name = "数量"
        Me.数量.Width = 80
        '
        '単位
        '
        Me.単位.HeaderText = "単位"
        Me.単位.Name = "単位"
        Me.単位.Width = 80
        '
        '仕入先
        '
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.仕入先.DefaultCellStyle = DataGridViewCellStyle4
        Me.仕入先.HeaderText = "仕入先"
        Me.仕入先.Name = "仕入先"
        Me.仕入先.Width = 85
        '
        '仕入値
        '
        Me.仕入値.HeaderText = "仕入値"
        Me.仕入値.Name = "仕入値"
        Me.仕入値.Width = 80
        '
        '間接費
        '
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.間接費.DefaultCellStyle = DataGridViewCellStyle5
        Me.間接費.HeaderText = "間接費"
        Me.間接費.Name = "間接費"
        Me.間接費.ReadOnly = True
        Me.間接費.Visible = False
        '
        '売単価
        '
        Me.売単価.HeaderText = "売単価"
        Me.売単価.Name = "売単価"
        Me.売単価.Width = 80
        '
        '売上金額
        '
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.売上金額.DefaultCellStyle = DataGridViewCellStyle6
        Me.売上金額.HeaderText = "売上金額"
        Me.売上金額.Name = "売上金額"
        Me.売上金額.ReadOnly = True
        '
        '粗利額
        '
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.粗利額.DefaultCellStyle = DataGridViewCellStyle7
        Me.粗利額.HeaderText = "粗利額"
        Me.粗利額.Name = "粗利額"
        Me.粗利額.ReadOnly = True
        '
        '粗利率
        '
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.粗利率.DefaultCellStyle = DataGridViewCellStyle8
        Me.粗利率.HeaderText = "粗利率"
        Me.粗利率.Name = "粗利率"
        Me.粗利率.ReadOnly = True
        '
        'リードタイム
        '
        Me.リードタイム.HeaderText = "リードタイム"
        Me.リードタイム.Name = "リードタイム"
        '
        '備考
        '
        Me.備考.HeaderText = "備考"
        Me.備考.Name = "備考"
        Me.備考.Width = 80
        '
        '出庫数
        '
        Me.出庫数.HeaderText = "出庫数"
        Me.出庫数.Name = "出庫数"
        '
        '未出庫数
        '
        Me.未出庫数.HeaderText = "未出庫数"
        Me.未出庫数.Name = "未出庫数"
        '
        'Order
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1350, 729)
        Me.Controls.Add(Me.TxtVat)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.TxtOrderRemark)
        Me.Controls.Add(Me.DtpExpiration)
        Me.Controls.Add(Me.DtpQuoteDate)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DtpQuoteRegistration)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TxtQuoteSuffix)
        Me.Controls.Add(Me.TxtQuoteNo)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.DtpOrderDate)
        Me.Controls.Add(Me.TxtOrderAmount)
        Me.Controls.Add(Me.LblOrderAmount)
        Me.Controls.Add(Me.TxtGrossProfit)
        Me.Controls.Add(Me.LblGrossProfit)
        Me.Controls.Add(Me.TxtItemCount)
        Me.Controls.Add(Me.LblItemCount)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtCustomerCode)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.DtpOrderRegistration)
        Me.Controls.Add(Me.BtnRegistration)
        Me.Controls.Add(Me.TxtAddress3)
        Me.Controls.Add(Me.TxtAddress2)
        Me.Controls.Add(Me.LblRegistration)
        Me.Controls.Add(Me.TxtSales)
        Me.Controls.Add(Me.LblSales)
        Me.Controls.Add(Me.TxtOrderSuffix)
        Me.Controls.Add(Me.TxtQuoteRemarks)
        Me.Controls.Add(Me.TxtPaymentTerms)
        Me.Controls.Add(Me.LblRemarks)
        Me.Controls.Add(Me.TxtPosition)
        Me.Controls.Add(Me.LblPaymentTerms)
        Me.Controls.Add(Me.TxtPostalCode2)
        Me.Controls.Add(Me.LblPosition)
        Me.Controls.Add(Me.TxtTel)
        Me.Controls.Add(Me.LblPerson)
        Me.Controls.Add(Me.TxtAddress1)
        Me.Controls.Add(Me.TxtPerson)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.LblFax)
        Me.Controls.Add(Me.TxtFax)
        Me.Controls.Add(Me.TxtPostalCode1)
        Me.Controls.Add(Me.LblTel)
        Me.Controls.Add(Me.LblAddress)
        Me.Controls.Add(Me.TxtCustomerName)
        Me.Controls.Add(Me.TxtInput)
        Me.Controls.Add(Me.LblCustomerName)
        Me.Controls.Add(Me.TxtOrderNo)
        Me.Controls.Add(Me.LblInput)
        Me.Controls.Add(Me.LblOrderDate)
        Me.Controls.Add(Me.LblOrderNo)
        Me.Controls.Add(Me.DgvItemList)
        Me.Name = "Order"
        Me.Text = "Order"
        CType(Me.DgvItemList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtVat As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents TxtOrderRemark As TextBox
    Friend WithEvents DtpExpiration As DateTimePicker
    Friend WithEvents DtpQuoteDate As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents DtpQuoteRegistration As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents TxtQuoteSuffix As TextBox
    Friend WithEvents TxtQuoteNo As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents DtpOrderDate As DateTimePicker
    Friend WithEvents TxtOrderAmount As TextBox
    Friend WithEvents LblOrderAmount As Label
    Friend WithEvents TxtGrossProfit As TextBox
    Friend WithEvents LblGrossProfit As Label
    Friend WithEvents TxtItemCount As TextBox
    Friend WithEvents LblItemCount As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtCustomerCode As TextBox
    Friend WithEvents BtnBack As Button
    Friend WithEvents DtpOrderRegistration As DateTimePicker
    Friend WithEvents BtnRegistration As Button
    Friend WithEvents TxtAddress3 As TextBox
    Friend WithEvents TxtAddress2 As TextBox
    Friend WithEvents LblRegistration As Label
    Friend WithEvents TxtSales As TextBox
    Friend WithEvents LblSales As Label
    Friend WithEvents TxtOrderSuffix As TextBox
    Friend WithEvents TxtQuoteRemarks As TextBox
    Friend WithEvents TxtPaymentTerms As TextBox
    Friend WithEvents LblRemarks As Label
    Friend WithEvents TxtPosition As TextBox
    Friend WithEvents LblPaymentTerms As Label
    Friend WithEvents TxtPostalCode2 As TextBox
    Friend WithEvents LblPosition As Label
    Friend WithEvents TxtTel As TextBox
    Friend WithEvents LblPerson As Label
    Friend WithEvents TxtAddress1 As TextBox
    Friend WithEvents TxtPerson As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents LblFax As Label
    Friend WithEvents TxtFax As TextBox
    Friend WithEvents TxtPostalCode1 As TextBox
    Friend WithEvents LblTel As Label
    Friend WithEvents LblAddress As Label
    Friend WithEvents TxtCustomerName As TextBox
    Friend WithEvents TxtInput As TextBox
    Friend WithEvents LblCustomerName As Label
    Friend WithEvents TxtOrderNo As TextBox
    Friend WithEvents LblInput As Label
    Friend WithEvents LblOrderDate As Label
    Friend WithEvents LblOrderNo As Label
    Friend WithEvents DgvItemList As DataGridView
    Friend WithEvents No As DataGridViewTextBoxColumn
    Friend WithEvents メーカー As DataGridViewTextBoxColumn
    Friend WithEvents 品名 As DataGridViewTextBoxColumn
    Friend WithEvents 型式 As DataGridViewTextBoxColumn
    Friend WithEvents 数量 As DataGridViewTextBoxColumn
    Friend WithEvents 単位 As DataGridViewTextBoxColumn
    Friend WithEvents 仕入先 As DataGridViewTextBoxColumn
    Friend WithEvents 仕入値 As DataGridViewTextBoxColumn
    Friend WithEvents 間接費 As DataGridViewTextBoxColumn
    Friend WithEvents 売単価 As DataGridViewTextBoxColumn
    Friend WithEvents 売上金額 As DataGridViewTextBoxColumn
    Friend WithEvents 粗利額 As DataGridViewTextBoxColumn
    Friend WithEvents 粗利率 As DataGridViewTextBoxColumn
    Friend WithEvents リードタイム As DataGridViewTextBoxColumn
    Friend WithEvents 備考 As DataGridViewTextBoxColumn
    Friend WithEvents 出庫数 As DataGridViewTextBoxColumn
    Friend WithEvents 未出庫数 As DataGridViewTextBoxColumn
End Class
