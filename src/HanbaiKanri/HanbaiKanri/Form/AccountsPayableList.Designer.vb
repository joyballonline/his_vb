<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AccountsPayableList
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BtnAPCancel = New System.Windows.Forms.Button()
        Me.ChkCancelData = New System.Windows.Forms.CheckBox()
        Me.BtnAPView = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TxtAPSince = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtSupplierCode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblConditions = New System.Windows.Forms.Label()
        Me.TxtSupplierName = New System.Windows.Forms.TextBox()
        Me.BtnAPSearch = New System.Windows.Forms.Button()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.DgvKike = New System.Windows.Forms.DataGridView()
        Me.LblMode = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TxtCustomerPO = New System.Windows.Forms.TextBox()
        Me.dtAPDateSince = New System.Windows.Forms.DateTimePicker()
        Me.dtAPDateUntil = New System.Windows.Forms.DateTimePicker()
        Me.TxtSpec = New System.Windows.Forms.TextBox()
        Me.LblSpec = New System.Windows.Forms.Label()
        Me.TxtItemName = New System.Windows.Forms.TextBox()
        Me.LblItemName = New System.Windows.Forms.Label()
        Me.取消 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛区分 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入先請求番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.発注番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.発注番号枝番 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.客先番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入先コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入先名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.通貨_外貨 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入原価 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VAT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛金額 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛金額計_外貨 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.未登録額 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛残高_外貨 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.通貨 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛金額計 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛残高 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.備考1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.備考2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.更新日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.更新者 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DgvKike, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnAPCancel
        '
        Me.BtnAPCancel.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnAPCancel.Location = New System.Drawing.Point(832, 509)
        Me.BtnAPCancel.Name = "BtnAPCancel"
        Me.BtnAPCancel.Size = New System.Drawing.Size(165, 40)
        Me.BtnAPCancel.TabIndex = 13
        Me.BtnAPCancel.Text = "買掛取消"
        Me.BtnAPCancel.UseVisualStyleBackColor = True
        Me.BtnAPCancel.Visible = False
        '
        'ChkCancelData
        '
        Me.ChkCancelData.AutoSize = True
        Me.ChkCancelData.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ChkCancelData.Location = New System.Drawing.Point(16, 211)
        Me.ChkCancelData.Name = "ChkCancelData"
        Me.ChkCancelData.Size = New System.Drawing.Size(139, 19)
        Me.ChkCancelData.TabIndex = 9
        Me.ChkCancelData.Text = "取消データを含める"
        Me.ChkCancelData.UseVisualStyleBackColor = True
        '
        'BtnAPView
        '
        Me.BtnAPView.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnAPView.Location = New System.Drawing.Point(1003, 509)
        Me.BtnAPView.Name = "BtnAPView"
        Me.BtnAPView.Size = New System.Drawing.Size(165, 40)
        Me.BtnAPView.TabIndex = 14
        Me.BtnAPView.Text = "買掛参照"
        Me.BtnAPView.UseVisualStyleBackColor = True
        Me.BtnAPView.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(12, 185)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(87, 15)
        Me.Label10.TabIndex = 166
        Me.Label10.Text = "■表示形式"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(936, 46)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(17, 12)
        Me.Label5.TabIndex = 164
        Me.Label5.Text = "～"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(584, 67)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(170, 22)
        Me.Label7.TabIndex = 161
        Me.Label7.Text = "買掛番号"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtAPSince
        '
        Me.TxtAPSince.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAPSince.Location = New System.Drawing.Point(760, 67)
        Me.TxtAPSince.Name = "TxtAPSince"
        Me.TxtAPSince.Size = New System.Drawing.Size(369, 22)
        Me.TxtAPSince.TabIndex = 5
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(584, 39)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(170, 22)
        Me.Label8.TabIndex = 159
        Me.Label8.Text = "SupplierInvoiceDate"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(14, 68)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(170, 22)
        Me.Label4.TabIndex = 157
        Me.Label4.Text = "仕入先コード"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtSupplierCode
        '
        Me.TxtSupplierCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSupplierCode.Location = New System.Drawing.Point(190, 68)
        Me.TxtSupplierCode.Name = "TxtSupplierCode"
        Me.TxtSupplierCode.Size = New System.Drawing.Size(350, 22)
        Me.TxtSupplierCode.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(14, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(170, 22)
        Me.Label1.TabIndex = 155
        Me.Label1.Text = "仕入先名"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblConditions
        '
        Me.LblConditions.AutoSize = True
        Me.LblConditions.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblConditions.Location = New System.Drawing.Point(11, 13)
        Me.LblConditions.Name = "LblConditions"
        Me.LblConditions.Size = New System.Drawing.Size(87, 15)
        Me.LblConditions.TabIndex = 154
        Me.LblConditions.Text = "■抽出条件"
        '
        'TxtSupplierName
        '
        Me.TxtSupplierName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSupplierName.Location = New System.Drawing.Point(190, 40)
        Me.TxtSupplierName.Name = "TxtSupplierName"
        Me.TxtSupplierName.Size = New System.Drawing.Size(350, 22)
        Me.TxtSupplierName.TabIndex = 1
        '
        'BtnAPSearch
        '
        Me.BtnAPSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnAPSearch.Location = New System.Drawing.Point(1173, 41)
        Me.BtnAPSearch.Name = "BtnAPSearch"
        Me.BtnAPSearch.Size = New System.Drawing.Size(166, 40)
        Me.BtnAPSearch.TabIndex = 8
        Me.BtnAPSearch.Text = "検索"
        Me.BtnAPSearch.UseVisualStyleBackColor = True
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(1174, 509)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 15
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'DgvKike
        '
        Me.DgvKike.AllowUserToAddRows = False
        Me.DgvKike.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvKike.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DgvKike.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvKike.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.取消, Me.買掛番号, Me.買掛区分, Me.買掛日, Me.仕入先請求番号, Me.発注番号, Me.発注番号枝番, Me.客先番号, Me.仕入先コード, Me.仕入先名, Me.通貨_外貨, Me.仕入原価, Me.VAT, Me.買掛金額, Me.買掛金額計_外貨, Me.未登録額, Me.買掛残高_外貨, Me.通貨, Me.買掛金額計, Me.買掛残高, Me.備考1, Me.備考2, Me.更新日, Me.更新者})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvKike.DefaultCellStyle = DataGridViewCellStyle4
        Me.DgvKike.Location = New System.Drawing.Point(13, 236)
        Me.DgvKike.Name = "DgvKike"
        Me.DgvKike.ReadOnly = True
        Me.DgvKike.RowHeadersVisible = False
        Me.DgvKike.RowTemplate.Height = 21
        Me.DgvKike.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvKike.Size = New System.Drawing.Size(1326, 267)
        Me.DgvKike.TabIndex = 12
        '
        'LblMode
        '
        Me.LblMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMode.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMode.Location = New System.Drawing.Point(1173, 9)
        Me.LblMode.Name = "LblMode"
        Me.LblMode.Size = New System.Drawing.Size(166, 22)
        Me.LblMode.TabIndex = 323
        Me.LblMode.Text = "モード"
        Me.LblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label11.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(584, 95)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(170, 22)
        Me.Label11.TabIndex = 325
        Me.Label11.Text = "客先番号"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtCustomerPO
        '
        Me.TxtCustomerPO.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerPO.Location = New System.Drawing.Point(760, 95)
        Me.TxtCustomerPO.Name = "TxtCustomerPO"
        Me.TxtCustomerPO.Size = New System.Drawing.Size(369, 22)
        Me.TxtCustomerPO.TabIndex = 7
        '
        'dtAPDateSince
        '
        Me.dtAPDateSince.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtAPDateSince.CustomFormat = ""
        Me.dtAPDateSince.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtAPDateSince.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtAPDateSince.Location = New System.Drawing.Point(760, 39)
        Me.dtAPDateSince.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtAPDateSince.Name = "dtAPDateSince"
        Me.dtAPDateSince.Size = New System.Drawing.Size(170, 22)
        Me.dtAPDateSince.TabIndex = 332
        Me.dtAPDateSince.Value = New Date(2019, 1, 1, 0, 0, 0, 0)
        '
        'dtAPDateUntil
        '
        Me.dtAPDateUntil.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtAPDateUntil.CustomFormat = ""
        Me.dtAPDateUntil.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtAPDateUntil.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtAPDateUntil.Location = New System.Drawing.Point(959, 40)
        Me.dtAPDateUntil.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtAPDateUntil.Name = "dtAPDateUntil"
        Me.dtAPDateUntil.Size = New System.Drawing.Size(170, 22)
        Me.dtAPDateUntil.TabIndex = 333
        Me.dtAPDateUntil.Value = New Date(2019, 1, 1, 0, 0, 0, 0)
        '
        'TxtSpec
        '
        Me.TxtSpec.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSpec.Location = New System.Drawing.Point(760, 151)
        Me.TxtSpec.Name = "TxtSpec"
        Me.TxtSpec.Size = New System.Drawing.Size(369, 22)
        Me.TxtSpec.TabIndex = 345
        '
        'LblSpec
        '
        Me.LblSpec.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblSpec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblSpec.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSpec.Location = New System.Drawing.Point(584, 151)
        Me.LblSpec.Name = "LblSpec"
        Me.LblSpec.Size = New System.Drawing.Size(170, 22)
        Me.LblSpec.TabIndex = 344
        Me.LblSpec.Text = "型式"
        Me.LblSpec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtItemName
        '
        Me.TxtItemName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtItemName.Location = New System.Drawing.Point(760, 123)
        Me.TxtItemName.Name = "TxtItemName"
        Me.TxtItemName.Size = New System.Drawing.Size(369, 22)
        Me.TxtItemName.TabIndex = 343
        '
        'LblItemName
        '
        Me.LblItemName.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblItemName.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblItemName.Location = New System.Drawing.Point(584, 123)
        Me.LblItemName.Name = "LblItemName"
        Me.LblItemName.Size = New System.Drawing.Size(170, 22)
        Me.LblItemName.TabIndex = 342
        Me.LblItemName.Text = "品名"
        Me.LblItemName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        '取消
        '
        Me.取消.HeaderText = "取消"
        Me.取消.MaxInputLength = 1
        Me.取消.Name = "取消"
        Me.取消.ReadOnly = True
        Me.取消.Width = 54
        '
        '買掛番号
        '
        Me.買掛番号.HeaderText = "買掛番号"
        Me.買掛番号.MaxInputLength = 14
        Me.買掛番号.Name = "買掛番号"
        Me.買掛番号.ReadOnly = True
        Me.買掛番号.Width = 78
        '
        '買掛区分
        '
        Me.買掛区分.HeaderText = "買掛区分"
        Me.買掛区分.MaxInputLength = 1
        Me.買掛区分.Name = "買掛区分"
        Me.買掛区分.ReadOnly = True
        Me.買掛区分.Width = 78
        '
        '買掛日
        '
        Me.買掛日.HeaderText = "SupplierInvoiceDate"
        Me.買掛日.MaxInputLength = 10
        Me.買掛日.Name = "買掛日"
        Me.買掛日.ReadOnly = True
        Me.買掛日.Width = 131
        '
        '仕入先請求番号
        '
        Me.仕入先請求番号.HeaderText = "SupplierInvoiceNo"
        Me.仕入先請求番号.Name = "仕入先請求番号"
        Me.仕入先請求番号.ReadOnly = True
        Me.仕入先請求番号.Width = 121
        '
        '発注番号
        '
        Me.発注番号.HeaderText = "発注番号"
        Me.発注番号.MaxInputLength = 14
        Me.発注番号.Name = "発注番号"
        Me.発注番号.ReadOnly = True
        Me.発注番号.Width = 78
        '
        '発注番号枝番
        '
        Me.発注番号枝番.HeaderText = "発注Ver"
        Me.発注番号枝番.MaxInputLength = 2
        Me.発注番号枝番.Name = "発注番号枝番"
        Me.発注番号枝番.ReadOnly = True
        Me.発注番号枝番.Width = 72
        '
        '客先番号
        '
        Me.客先番号.HeaderText = "客先番号"
        Me.客先番号.MaxInputLength = 20
        Me.客先番号.Name = "客先番号"
        Me.客先番号.ReadOnly = True
        Me.客先番号.Width = 78
        '
        '仕入先コード
        '
        Me.仕入先コード.HeaderText = "仕入先コード"
        Me.仕入先コード.MaxInputLength = 8
        Me.仕入先コード.Name = "仕入先コード"
        Me.仕入先コード.ReadOnly = True
        Me.仕入先コード.Width = 93
        '
        '仕入先名
        '
        Me.仕入先名.HeaderText = "仕入先名"
        Me.仕入先名.MaxInputLength = 100
        Me.仕入先名.Name = "仕入先名"
        Me.仕入先名.ReadOnly = True
        Me.仕入先名.Width = 78
        '
        '通貨_外貨
        '
        Me.通貨_外貨.HeaderText = "仕入通貨"
        Me.通貨_外貨.Name = "通貨_外貨"
        Me.通貨_外貨.ReadOnly = True
        Me.通貨_外貨.Width = 78
        '
        '仕入原価
        '
        Me.仕入原価.HeaderText = "仕入原価"
        Me.仕入原価.Name = "仕入原価"
        Me.仕入原価.ReadOnly = True
        Me.仕入原価.Width = 78
        '
        'VAT
        '
        Me.VAT.HeaderText = "VAT-IN"
        Me.VAT.Name = "VAT"
        Me.VAT.ReadOnly = True
        Me.VAT.Width = 70
        '
        '買掛金額
        '
        Me.買掛金額.HeaderText = "買掛金額"
        Me.買掛金額.Name = "買掛金額"
        Me.買掛金額.ReadOnly = True
        Me.買掛金額.Width = 78
        '
        '買掛金額計_外貨
        '
        Me.買掛金額計_外貨.HeaderText = "既登録額"
        Me.買掛金額計_外貨.Name = "買掛金額計_外貨"
        Me.買掛金額計_外貨.ReadOnly = True
        Me.買掛金額計_外貨.Visible = False
        Me.買掛金額計_外貨.Width = 78
        '
        '未登録額
        '
        Me.未登録額.HeaderText = "未登録額"
        Me.未登録額.Name = "未登録額"
        Me.未登録額.ReadOnly = True
        Me.未登録額.Visible = False
        Me.未登録額.Width = 78
        '
        '買掛残高_外貨
        '
        Me.買掛残高_外貨.HeaderText = "買掛残高_外貨"
        Me.買掛残高_外貨.Name = "買掛残高_外貨"
        Me.買掛残高_外貨.ReadOnly = True
        Me.買掛残高_外貨.Visible = False
        Me.買掛残高_外貨.Width = 106
        '
        '通貨
        '
        Me.通貨.HeaderText = "通貨"
        Me.通貨.Name = "通貨"
        Me.通貨.ReadOnly = True
        Me.通貨.Visible = False
        Me.通貨.Width = 54
        '
        '買掛金額計
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.買掛金額計.DefaultCellStyle = DataGridViewCellStyle2
        Me.買掛金額計.HeaderText = "買掛金額計"
        Me.買掛金額計.MaxInputLength = 14
        Me.買掛金額計.Name = "買掛金額計"
        Me.買掛金額計.ReadOnly = True
        Me.買掛金額計.Visible = False
        Me.買掛金額計.Width = 90
        '
        '買掛残高
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.買掛残高.DefaultCellStyle = DataGridViewCellStyle3
        Me.買掛残高.HeaderText = "買掛残高"
        Me.買掛残高.MaxInputLength = 14
        Me.買掛残高.Name = "買掛残高"
        Me.買掛残高.ReadOnly = True
        Me.買掛残高.Visible = False
        Me.買掛残高.Width = 78
        '
        '備考1
        '
        Me.備考1.HeaderText = "備考1"
        Me.備考1.MaxInputLength = 50
        Me.備考1.Name = "備考1"
        Me.備考1.ReadOnly = True
        Me.備考1.Width = 60
        '
        '備考2
        '
        Me.備考2.HeaderText = "備考2"
        Me.備考2.MaxInputLength = 50
        Me.備考2.Name = "備考2"
        Me.備考2.ReadOnly = True
        Me.備考2.Width = 60
        '
        '更新日
        '
        Me.更新日.HeaderText = "更新日"
        Me.更新日.MaxInputLength = 15
        Me.更新日.Name = "更新日"
        Me.更新日.ReadOnly = True
        Me.更新日.Width = 66
        '
        '更新者
        '
        Me.更新者.HeaderText = "更新者"
        Me.更新者.MaxInputLength = 50
        Me.更新者.Name = "更新者"
        Me.更新者.ReadOnly = True
        Me.更新者.Width = 66
        '
        'AccountsPayableList
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.TxtSpec)
        Me.Controls.Add(Me.LblSpec)
        Me.Controls.Add(Me.TxtItemName)
        Me.Controls.Add(Me.LblItemName)
        Me.Controls.Add(Me.dtAPDateUntil)
        Me.Controls.Add(Me.dtAPDateSince)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.TxtCustomerPO)
        Me.Controls.Add(Me.LblMode)
        Me.Controls.Add(Me.BtnAPCancel)
        Me.Controls.Add(Me.ChkCancelData)
        Me.Controls.Add(Me.BtnAPView)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TxtAPSince)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtSupplierCode)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LblConditions)
        Me.Controls.Add(Me.TxtSupplierName)
        Me.Controls.Add(Me.BtnAPSearch)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.DgvKike)
        Me.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "AccountsPayableList"
        Me.Text = "AccountsPayableList"
        CType(Me.DgvKike, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnAPCancel As Button
    Friend WithEvents ChkCancelData As CheckBox
    Friend WithEvents BtnAPView As Button
    Friend WithEvents Label10 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents TxtAPSince As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents TxtSupplierCode As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents LblConditions As Label
    Friend WithEvents TxtSupplierName As TextBox
    Friend WithEvents BtnAPSearch As Button
    Friend WithEvents BtnBack As Button
    Friend WithEvents DgvKike As DataGridView
    Friend WithEvents LblMode As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents TxtCustomerPO As TextBox
    Friend WithEvents dtAPDateSince As DateTimePicker
    Friend WithEvents dtAPDateUntil As DateTimePicker
    Friend WithEvents TxtSpec As TextBox
    Friend WithEvents LblSpec As Label
    Friend WithEvents TxtItemName As TextBox
    Friend WithEvents LblItemName As Label
    Friend WithEvents 取消 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛番号 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛区分 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛日 As DataGridViewTextBoxColumn
    Friend WithEvents 仕入先請求番号 As DataGridViewTextBoxColumn
    Friend WithEvents 発注番号 As DataGridViewTextBoxColumn
    Friend WithEvents 発注番号枝番 As DataGridViewTextBoxColumn
    Friend WithEvents 客先番号 As DataGridViewTextBoxColumn
    Friend WithEvents 仕入先コード As DataGridViewTextBoxColumn
    Friend WithEvents 仕入先名 As DataGridViewTextBoxColumn
    Friend WithEvents 通貨_外貨 As DataGridViewTextBoxColumn
    Friend WithEvents 仕入原価 As DataGridViewTextBoxColumn
    Friend WithEvents VAT As DataGridViewTextBoxColumn
    Friend WithEvents 買掛金額 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛金額計_外貨 As DataGridViewTextBoxColumn
    Friend WithEvents 未登録額 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛残高_外貨 As DataGridViewTextBoxColumn
    Friend WithEvents 通貨 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛金額計 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛残高 As DataGridViewTextBoxColumn
    Friend WithEvents 備考1 As DataGridViewTextBoxColumn
    Friend WithEvents 備考2 As DataGridViewTextBoxColumn
    Friend WithEvents 更新日 As DataGridViewTextBoxColumn
    Friend WithEvents 更新者 As DataGridViewTextBoxColumn
End Class
