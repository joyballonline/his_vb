<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class OrderingProgress
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
        Me.BtnOrderView = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtSales = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TxtOrderSince = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
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
        Me.DgvCymnhd = New System.Windows.Forms.DataGridView()
        Me.LblMode = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TxtCustomerPO = New System.Windows.Forms.TextBox()
        Me.dtOrderDateSince = New System.Windows.Forms.DateTimePicker()
        Me.dtOrderDateUntil = New System.Windows.Forms.DateTimePicker()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.RbtnSlip = New System.Windows.Forms.RadioButton()
        Me.RbtnDetails = New System.Windows.Forms.RadioButton()
        Me.ChkCancelData = New System.Windows.Forms.CheckBox()
        Me.TxtSpec = New System.Windows.Forms.TextBox()
        Me.LblSpec = New System.Windows.Forms.Label()
        Me.TxtItemName = New System.Windows.Forms.TextBox()
        Me.LblItemName = New System.Windows.Forms.Label()
        Me.txtMaker = New System.Windows.Forms.TextBox()
        Me.lblMaker = New System.Windows.Forms.Label()
        Me.発注日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.発注番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.発注番号枝番 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.行番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.メーカー = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.品名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.型式 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.受注番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.受注日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入登録 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.入庫登録 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛登録 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.支払登録 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.発注メーカー = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.発注品名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.発注型式 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DgvCymnhd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnOrderView
        '
        Me.BtnOrderView.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnOrderView.Location = New System.Drawing.Point(1003, 509)
        Me.BtnOrderView.Name = "BtnOrderView"
        Me.BtnOrderView.Size = New System.Drawing.Size(165, 40)
        Me.BtnOrderView.TabIndex = 22
        Me.BtnOrderView.Text = "受注参照"
        Me.BtnOrderView.UseVisualStyleBackColor = True
        Me.BtnOrderView.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(10, 179)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(87, 15)
        Me.Label10.TabIndex = 58
        Me.Label10.Text = "■表示形式"
        Me.Label10.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(936, 43)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(17, 12)
        Me.Label5.TabIndex = 56
        Me.Label5.Text = "～"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(14, 148)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(170, 22)
        Me.Label6.TabIndex = 53
        Me.Label6.Text = "営業担当者"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtSales
        '
        Me.TxtSales.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSales.Location = New System.Drawing.Point(190, 148)
        Me.TxtSales.Name = "TxtSales"
        Me.TxtSales.Size = New System.Drawing.Size(350, 22)
        Me.TxtSales.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(584, 65)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(170, 22)
        Me.Label7.TabIndex = 51
        Me.Label7.Text = "発注番号"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtOrderSince
        '
        Me.TxtOrderSince.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtOrderSince.Location = New System.Drawing.Point(760, 65)
        Me.TxtOrderSince.Name = "TxtOrderSince"
        Me.TxtOrderSince.Size = New System.Drawing.Size(369, 22)
        Me.TxtOrderSince.TabIndex = 6
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(584, 36)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(170, 22)
        Me.Label8.TabIndex = 49
        Me.Label8.Text = "発注日"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(14, 120)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(170, 22)
        Me.Label4.TabIndex = 47
        Me.Label4.Text = "得意先コード"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtCustomerCode
        '
        Me.TxtCustomerCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerCode.Location = New System.Drawing.Point(190, 120)
        Me.TxtCustomerCode.Name = "TxtCustomerCode"
        Me.TxtCustomerCode.Size = New System.Drawing.Size(350, 22)
        Me.TxtCustomerCode.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(14, 92)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(170, 22)
        Me.Label3.TabIndex = 45
        Me.Label3.Text = "電話番号"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtTel
        '
        Me.TxtTel.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtTel.Location = New System.Drawing.Point(190, 92)
        Me.TxtTel.Name = "TxtTel"
        Me.TxtTel.Size = New System.Drawing.Size(350, 22)
        Me.TxtTel.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(14, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(170, 22)
        Me.Label2.TabIndex = 43
        Me.Label2.Text = "住所"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtAddress
        '
        Me.TxtAddress.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAddress.Location = New System.Drawing.Point(190, 64)
        Me.TxtAddress.Name = "TxtAddress"
        Me.TxtAddress.Size = New System.Drawing.Size(350, 22)
        Me.TxtAddress.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(14, 36)
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
        Me.LblConditions.Location = New System.Drawing.Point(11, 13)
        Me.LblConditions.Name = "LblConditions"
        Me.LblConditions.Size = New System.Drawing.Size(87, 15)
        Me.LblConditions.TabIndex = 40
        Me.LblConditions.Text = "■抽出条件"
        '
        'TxtCustomerName
        '
        Me.TxtCustomerName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerName.Location = New System.Drawing.Point(190, 36)
        Me.TxtCustomerName.Name = "TxtCustomerName"
        Me.TxtCustomerName.Size = New System.Drawing.Size(350, 22)
        Me.TxtCustomerName.TabIndex = 1
        '
        'BtnOrderSearch
        '
        Me.BtnOrderSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnOrderSearch.Location = New System.Drawing.Point(1174, 43)
        Me.BtnOrderSearch.Name = "BtnOrderSearch"
        Me.BtnOrderSearch.Size = New System.Drawing.Size(165, 40)
        Me.BtnOrderSearch.TabIndex = 14
        Me.BtnOrderSearch.Text = "検索"
        Me.BtnOrderSearch.UseVisualStyleBackColor = True
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(1174, 509)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 23
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'DgvCymnhd
        '
        Me.DgvCymnhd.AllowUserToAddRows = False
        Me.DgvCymnhd.AllowUserToDeleteRows = False
        Me.DgvCymnhd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvCymnhd.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.発注日, Me.発注番号, Me.発注番号枝番, Me.行番号, Me.メーカー, Me.品名, Me.型式, Me.受注番号, Me.受注日, Me.仕入登録, Me.入庫登録, Me.買掛登録, Me.支払登録, Me.発注メーカー, Me.発注品名, Me.発注型式})
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvCymnhd.DefaultCellStyle = DataGridViewCellStyle1
        Me.DgvCymnhd.Location = New System.Drawing.Point(13, 238)
        Me.DgvCymnhd.Name = "DgvCymnhd"
        Me.DgvCymnhd.ReadOnly = True
        Me.DgvCymnhd.RowHeadersVisible = False
        Me.DgvCymnhd.RowTemplate.Height = 21
        Me.DgvCymnhd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvCymnhd.Size = New System.Drawing.Size(1326, 265)
        Me.DgvCymnhd.TabIndex = 15
        '
        'LblMode
        '
        Me.LblMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMode.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMode.Location = New System.Drawing.Point(1106, 9)
        Me.LblMode.Name = "LblMode"
        Me.LblMode.Size = New System.Drawing.Size(233, 22)
        Me.LblMode.TabIndex = 67
        Me.LblMode.Text = "モード"
        Me.LblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label11.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(584, 93)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(170, 22)
        Me.Label11.TabIndex = 97
        Me.Label11.Text = "客先番号"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtCustomerPO
        '
        Me.TxtCustomerPO.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerPO.Location = New System.Drawing.Point(760, 93)
        Me.TxtCustomerPO.Name = "TxtCustomerPO"
        Me.TxtCustomerPO.Size = New System.Drawing.Size(369, 22)
        Me.TxtCustomerPO.TabIndex = 10
        '
        'dtOrderDateSince
        '
        Me.dtOrderDateSince.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtOrderDateSince.CustomFormat = ""
        Me.dtOrderDateSince.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtOrderDateSince.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtOrderDateSince.Location = New System.Drawing.Point(760, 36)
        Me.dtOrderDateSince.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtOrderDateSince.Name = "dtOrderDateSince"
        Me.dtOrderDateSince.Size = New System.Drawing.Size(170, 22)
        Me.dtOrderDateSince.TabIndex = 331
        Me.dtOrderDateSince.Value = New Date(2019, 1, 1, 0, 0, 0, 0)
        '
        'dtOrderDateUntil
        '
        Me.dtOrderDateUntil.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtOrderDateUntil.CustomFormat = ""
        Me.dtOrderDateUntil.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtOrderDateUntil.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtOrderDateUntil.Location = New System.Drawing.Point(959, 37)
        Me.dtOrderDateUntil.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtOrderDateUntil.Name = "dtOrderDateUntil"
        Me.dtOrderDateUntil.Size = New System.Drawing.Size(170, 22)
        Me.dtOrderDateUntil.TabIndex = 332
        Me.dtOrderDateUntil.Value = New Date(2019, 1, 1, 0, 0, 0, 0)
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
        Me.TableLayoutPanel1.Controls.Add(Me.ChkCancelData, 4, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(13, 203)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(834, 29)
        Me.TableLayoutPanel1.TabIndex = 333
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
        Me.RbtnSlip.Visible = False
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
        Me.RbtnDetails.Visible = False
        '
        'ChkCancelData
        '
        Me.ChkCancelData.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ChkCancelData.AutoSize = True
        Me.ChkCancelData.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ChkCancelData.Location = New System.Drawing.Point(215, 5)
        Me.ChkCancelData.Name = "ChkCancelData"
        Me.ChkCancelData.Size = New System.Drawing.Size(139, 19)
        Me.ChkCancelData.TabIndex = 14
        Me.ChkCancelData.Text = "取消データを含める"
        Me.ChkCancelData.UseVisualStyleBackColor = True
        Me.ChkCancelData.Visible = False
        '
        'TxtSpec
        '
        Me.TxtSpec.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSpec.Location = New System.Drawing.Point(760, 178)
        Me.TxtSpec.Name = "TxtSpec"
        Me.TxtSpec.Size = New System.Drawing.Size(369, 22)
        Me.TxtSpec.TabIndex = 13
        '
        'LblSpec
        '
        Me.LblSpec.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblSpec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblSpec.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSpec.Location = New System.Drawing.Point(584, 178)
        Me.LblSpec.Name = "LblSpec"
        Me.LblSpec.Size = New System.Drawing.Size(170, 22)
        Me.LblSpec.TabIndex = 336
        Me.LblSpec.Text = "型式"
        Me.LblSpec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtItemName
        '
        Me.TxtItemName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtItemName.Location = New System.Drawing.Point(760, 150)
        Me.TxtItemName.Name = "TxtItemName"
        Me.TxtItemName.Size = New System.Drawing.Size(369, 22)
        Me.TxtItemName.TabIndex = 12
        '
        'LblItemName
        '
        Me.LblItemName.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblItemName.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblItemName.Location = New System.Drawing.Point(584, 150)
        Me.LblItemName.Name = "LblItemName"
        Me.LblItemName.Size = New System.Drawing.Size(170, 22)
        Me.LblItemName.TabIndex = 334
        Me.LblItemName.Text = "品名"
        Me.LblItemName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtMaker
        '
        Me.txtMaker.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtMaker.Location = New System.Drawing.Point(760, 121)
        Me.txtMaker.Name = "txtMaker"
        Me.txtMaker.Size = New System.Drawing.Size(369, 22)
        Me.txtMaker.TabIndex = 11
        '
        'lblMaker
        '
        Me.lblMaker.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblMaker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMaker.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMaker.Location = New System.Drawing.Point(584, 121)
        Me.lblMaker.Name = "lblMaker"
        Me.lblMaker.Size = New System.Drawing.Size(170, 22)
        Me.lblMaker.TabIndex = 348
        Me.lblMaker.Text = "メーカー"
        Me.lblMaker.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        '発注日
        '
        Me.発注日.HeaderText = "発注日"
        Me.発注日.Name = "発注日"
        Me.発注日.ReadOnly = True
        '
        '発注番号
        '
        Me.発注番号.HeaderText = "発注番号"
        Me.発注番号.Name = "発注番号"
        Me.発注番号.ReadOnly = True
        '
        '発注番号枝番
        '
        Me.発注番号枝番.HeaderText = "発注Ver"
        Me.発注番号枝番.Name = "発注番号枝番"
        Me.発注番号枝番.ReadOnly = True
        '
        '行番号
        '
        Me.行番号.HeaderText = "行No"
        Me.行番号.Name = "行番号"
        Me.行番号.ReadOnly = True
        '
        'メーカー
        '
        Me.メーカー.HeaderText = "メーカー"
        Me.メーカー.Name = "メーカー"
        Me.メーカー.ReadOnly = True
        '
        '品名
        '
        Me.品名.HeaderText = "品名"
        Me.品名.Name = "品名"
        Me.品名.ReadOnly = True
        '
        '型式
        '
        Me.型式.HeaderText = "型式"
        Me.型式.Name = "型式"
        Me.型式.ReadOnly = True
        '
        '受注番号
        '
        Me.受注番号.HeaderText = "受注番号"
        Me.受注番号.Name = "受注番号"
        Me.受注番号.ReadOnly = True
        '
        '受注日
        '
        Me.受注日.HeaderText = "受注日"
        Me.受注日.Name = "受注日"
        Me.受注日.ReadOnly = True
        '
        '仕入登録
        '
        Me.仕入登録.HeaderText = "仕入登録"
        Me.仕入登録.Name = "仕入登録"
        Me.仕入登録.ReadOnly = True
        '
        '入庫登録
        '
        Me.入庫登録.HeaderText = "入庫登録"
        Me.入庫登録.Name = "入庫登録"
        Me.入庫登録.ReadOnly = True
        '
        '買掛登録
        '
        Me.買掛登録.HeaderText = "買掛登録"
        Me.買掛登録.Name = "買掛登録"
        Me.買掛登録.ReadOnly = True
        '
        '支払登録
        '
        Me.支払登録.HeaderText = "支払登録"
        Me.支払登録.Name = "支払登録"
        Me.支払登録.ReadOnly = True
        '
        '発注メーカー
        '
        Me.発注メーカー.HeaderText = "発注メーカー"
        Me.発注メーカー.Name = "発注メーカー"
        Me.発注メーカー.ReadOnly = True
        '
        '発注品名
        '
        Me.発注品名.HeaderText = "発注品名"
        Me.発注品名.Name = "発注品名"
        Me.発注品名.ReadOnly = True
        '
        '発注型式
        '
        Me.発注型式.HeaderText = "発注型式"
        Me.発注型式.Name = "発注型式"
        Me.発注型式.ReadOnly = True
        '
        'OrderingProgress
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.txtMaker)
        Me.Controls.Add(Me.lblMaker)
        Me.Controls.Add(Me.TxtSpec)
        Me.Controls.Add(Me.LblSpec)
        Me.Controls.Add(Me.TxtItemName)
        Me.Controls.Add(Me.LblItemName)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.dtOrderDateUntil)
        Me.Controls.Add(Me.dtOrderDateSince)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.TxtCustomerPO)
        Me.Controls.Add(Me.LblMode)
        Me.Controls.Add(Me.BtnOrderView)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TxtSales)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TxtOrderSince)
        Me.Controls.Add(Me.Label8)
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
        Me.Controls.Add(Me.DgvCymnhd)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "OrderingProgress"
        Me.Text = "OrderingProgress"
        CType(Me.DgvCymnhd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnOrderView As Button
    Friend WithEvents Label10 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents TxtSales As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TxtOrderSince As TextBox
    Friend WithEvents Label8 As Label
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
    Friend WithEvents DgvCymnhd As DataGridView
    Friend WithEvents LblMode As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents TxtCustomerPO As TextBox
    Friend WithEvents dtOrderDateSince As DateTimePicker
    Friend WithEvents dtOrderDateUntil As DateTimePicker
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents RbtnSlip As RadioButton
    Friend WithEvents RbtnDetails As RadioButton
    Friend WithEvents ChkCancelData As CheckBox
    Friend WithEvents TxtSpec As TextBox
    Friend WithEvents LblSpec As Label
    Friend WithEvents TxtItemName As TextBox
    Friend WithEvents LblItemName As Label
    Friend WithEvents txtMaker As TextBox
    Friend WithEvents lblMaker As Label
    Friend WithEvents 発注日 As DataGridViewTextBoxColumn
    Friend WithEvents 発注番号 As DataGridViewTextBoxColumn
    Friend WithEvents 発注番号枝番 As DataGridViewTextBoxColumn
    Friend WithEvents 行番号 As DataGridViewTextBoxColumn
    Friend WithEvents メーカー As DataGridViewTextBoxColumn
    Friend WithEvents 品名 As DataGridViewTextBoxColumn
    Friend WithEvents 型式 As DataGridViewTextBoxColumn
    Friend WithEvents 受注番号 As DataGridViewTextBoxColumn
    Friend WithEvents 受注日 As DataGridViewTextBoxColumn
    Friend WithEvents 仕入登録 As DataGridViewTextBoxColumn
    Friend WithEvents 入庫登録 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛登録 As DataGridViewTextBoxColumn
    Friend WithEvents 支払登録 As DataGridViewTextBoxColumn
    Friend WithEvents 発注メーカー As DataGridViewTextBoxColumn
    Friend WithEvents 発注品名 As DataGridViewTextBoxColumn
    Friend WithEvents 発注型式 As DataGridViewTextBoxColumn
End Class
