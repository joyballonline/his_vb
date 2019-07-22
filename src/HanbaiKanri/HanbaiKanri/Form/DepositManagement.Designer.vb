<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DepositManagement
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
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle21 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BtnRegist = New System.Windows.Forms.Button()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.LblNo1 = New System.Windows.Forms.Label()
        Me.TxtHistoryCount = New System.Windows.Forms.TextBox()
        Me.DtpDepositDate = New System.Windows.Forms.DateTimePicker()
        Me.LblRemarks = New System.Windows.Forms.Label()
        Me.TxtRemarks = New System.Windows.Forms.TextBox()
        Me.LblDepositDate = New System.Windows.Forms.Label()
        Me.LblNo2 = New System.Windows.Forms.Label()
        Me.TxtDepositCount = New System.Windows.Forms.TextBox()
        Me.DgvCustomer = New System.Windows.Forms.DataGridView()
        Me.請求先 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求残高 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LblBillingInfo = New System.Windows.Forms.Label()
        Me.LblHistory = New System.Windows.Forms.Label()
        Me.LblDeposit = New System.Windows.Forms.Label()
        Me.DgvBillingInfo = New System.Windows.Forms.DataGridView()
        Me.DgvHistory = New System.Windows.Forms.DataGridView()
        Me.No = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.入金済請求先 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.入金番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.入金日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.入金種目 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.入金済入金額計 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.備考 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DgvDeposit = New System.Windows.Forms.DataGridView()
        Me.BtnAdd = New System.Windows.Forms.Button()
        Me.LblNo3 = New System.Windows.Forms.Label()
        Me.TxtBillingCount = New System.Windows.Forms.TextBox()
        Me.BtnDelete = New System.Windows.Forms.Button()
        Me.BtnCal = New System.Windows.Forms.Button()
        Me.LblMode = New System.Windows.Forms.Label()
        Me.TxtIDRCurrency = New System.Windows.Forms.TextBox()
        Me.LblIDRCurrency = New System.Windows.Forms.Label()
        Me.行番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.入力入金額 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InfoNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求情報請求番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求金額 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求情報入金額計 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求情報入金額計固定 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求情報請求残高 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求情報請求残高固定 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.入金額 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DgvCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvBillingInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvDeposit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnRegist
        '
        Me.BtnRegist.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnRegist.Location = New System.Drawing.Point(1003, 509)
        Me.BtnRegist.Name = "BtnRegist"
        Me.BtnRegist.Size = New System.Drawing.Size(165, 40)
        Me.BtnRegist.TabIndex = 10
        Me.BtnRegist.Text = "登録"
        Me.BtnRegist.UseVisualStyleBackColor = True
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(1174, 509)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 11
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'LblNo1
        '
        Me.LblNo1.BackColor = System.Drawing.Color.Transparent
        Me.LblNo1.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblNo1.Location = New System.Drawing.Point(1316, 65)
        Me.LblNo1.Name = "LblNo1"
        Me.LblNo1.Size = New System.Drawing.Size(22, 22)
        Me.LblNo1.TabIndex = 291
        Me.LblNo1.Text = "件"
        Me.LblNo1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtHistoryCount
        '
        Me.TxtHistoryCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtHistoryCount.Enabled = False
        Me.TxtHistoryCount.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtHistoryCount.Location = New System.Drawing.Point(1272, 65)
        Me.TxtHistoryCount.Name = "TxtHistoryCount"
        Me.TxtHistoryCount.Size = New System.Drawing.Size(38, 37)
        Me.TxtHistoryCount.TabIndex = 290
        Me.TxtHistoryCount.TabStop = False
        Me.TxtHistoryCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DtpDepositDate
        '
        Me.DtpDepositDate.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpDepositDate.CustomFormat = ""
        Me.DtpDepositDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpDepositDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpDepositDate.Location = New System.Drawing.Point(291, 335)
        Me.DtpDepositDate.Name = "DtpDepositDate"
        Me.DtpDepositDate.Size = New System.Drawing.Size(148, 37)
        Me.DtpDepositDate.TabIndex = 7
        Me.DtpDepositDate.TabStop = False
        Me.DtpDepositDate.Value = New Date(2018, 7, 25, 13, 29, 25, 0)
        '
        'LblRemarks
        '
        Me.LblRemarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblRemarks.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblRemarks.Location = New System.Drawing.Point(445, 335)
        Me.LblRemarks.Name = "LblRemarks"
        Me.LblRemarks.Size = New System.Drawing.Size(170, 22)
        Me.LblRemarks.TabIndex = 284
        Me.LblRemarks.Text = "備考"
        Me.LblRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtRemarks
        '
        Me.TxtRemarks.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtRemarks.Location = New System.Drawing.Point(621, 335)
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(645, 37)
        Me.TxtRemarks.TabIndex = 8
        '
        'LblDepositDate
        '
        Me.LblDepositDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblDepositDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblDepositDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblDepositDate.Location = New System.Drawing.Point(187, 335)
        Me.LblDepositDate.Name = "LblDepositDate"
        Me.LblDepositDate.Size = New System.Drawing.Size(98, 22)
        Me.LblDepositDate.TabIndex = 282
        Me.LblDepositDate.Text = "入金日"
        Me.LblDepositDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblNo2
        '
        Me.LblNo2.BackColor = System.Drawing.Color.Transparent
        Me.LblNo2.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblNo2.Location = New System.Drawing.Point(1316, 198)
        Me.LblNo2.Name = "LblNo2"
        Me.LblNo2.Size = New System.Drawing.Size(22, 22)
        Me.LblNo2.TabIndex = 281
        Me.LblNo2.Text = "件"
        Me.LblNo2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtDepositCount
        '
        Me.TxtDepositCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtDepositCount.Enabled = False
        Me.TxtDepositCount.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtDepositCount.Location = New System.Drawing.Point(1272, 198)
        Me.TxtDepositCount.Name = "TxtDepositCount"
        Me.TxtDepositCount.Size = New System.Drawing.Size(38, 37)
        Me.TxtDepositCount.TabIndex = 280
        Me.TxtDepositCount.TabStop = False
        Me.TxtDepositCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DgvCustomer
        '
        Me.DgvCustomer.AllowUserToAddRows = False
        Me.DgvCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvCustomer.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.請求先, Me.請求残高})
        Me.DgvCustomer.Location = New System.Drawing.Point(12, 19)
        Me.DgvCustomer.Name = "DgvCustomer"
        Me.DgvCustomer.RowHeadersVisible = False
        Me.DgvCustomer.RowTemplate.Height = 21
        Me.DgvCustomer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvCustomer.Size = New System.Drawing.Size(504, 40)
        Me.DgvCustomer.TabIndex = 1
        '
        '請求先
        '
        Me.請求先.HeaderText = "請求先"
        Me.請求先.Name = "請求先"
        Me.請求先.ReadOnly = True
        Me.請求先.Width = 300
        '
        '請求残高
        '
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.請求残高.DefaultCellStyle = DataGridViewCellStyle15
        Me.請求残高.HeaderText = "請求残高"
        Me.請求残高.Name = "請求残高"
        Me.請求残高.ReadOnly = True
        Me.請求残高.Width = 200
        '
        'LblBillingInfo
        '
        Me.LblBillingInfo.AutoSize = True
        Me.LblBillingInfo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblBillingInfo.Location = New System.Drawing.Point(13, 339)
        Me.LblBillingInfo.Name = "LblBillingInfo"
        Me.LblBillingInfo.Size = New System.Drawing.Size(163, 30)
        Me.LblBillingInfo.TabIndex = 279
        Me.LblBillingInfo.Text = "■請求情報"
        '
        'LblHistory
        '
        Me.LblHistory.AutoSize = True
        Me.LblHistory.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblHistory.Location = New System.Drawing.Point(12, 68)
        Me.LblHistory.Name = "LblHistory"
        Me.LblHistory.Size = New System.Drawing.Size(160, 30)
        Me.LblHistory.TabIndex = 278
        Me.LblHistory.Text = "■入金済み"
        '
        'LblDeposit
        '
        Me.LblDeposit.AutoSize = True
        Me.LblDeposit.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblDeposit.Location = New System.Drawing.Point(13, 203)
        Me.LblDeposit.Name = "LblDeposit"
        Me.LblDeposit.Size = New System.Drawing.Size(163, 30)
        Me.LblDeposit.TabIndex = 277
        Me.LblDeposit.Text = "■入金入力"
        '
        'DgvBillingInfo
        '
        Me.DgvBillingInfo.AllowUserToAddRows = False
        Me.DgvBillingInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvBillingInfo.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.InfoNo, Me.請求情報請求番号, Me.請求日, Me.請求金額, Me.請求情報入金額計, Me.請求情報入金額計固定, Me.請求情報請求残高, Me.請求情報請求残高固定, Me.入金額})
        Me.DgvBillingInfo.Location = New System.Drawing.Point(12, 363)
        Me.DgvBillingInfo.Name = "DgvBillingInfo"
        Me.DgvBillingInfo.RowHeadersVisible = False
        Me.DgvBillingInfo.RowTemplate.Height = 21
        Me.DgvBillingInfo.Size = New System.Drawing.Size(1327, 100)
        Me.DgvBillingInfo.TabIndex = 9
        '
        'DgvHistory
        '
        Me.DgvHistory.AllowUserToAddRows = False
        Me.DgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvHistory.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.No, Me.請求番号, Me.入金済請求先, Me.入金番号, Me.入金日, Me.入金種目, Me.入金済入金額計, Me.備考})
        Me.DgvHistory.Location = New System.Drawing.Point(10, 92)
        Me.DgvHistory.Name = "DgvHistory"
        Me.DgvHistory.RowHeadersVisible = False
        Me.DgvHistory.RowTemplate.Height = 21
        Me.DgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvHistory.Size = New System.Drawing.Size(1327, 100)
        Me.DgvHistory.TabIndex = 2
        '
        'No
        '
        Me.No.HeaderText = "No"
        Me.No.Name = "No"
        Me.No.ReadOnly = True
        Me.No.Width = 70
        '
        '請求番号
        '
        Me.請求番号.HeaderText = "請求番号"
        Me.請求番号.Name = "請求番号"
        Me.請求番号.ReadOnly = True
        Me.請求番号.Visible = False
        Me.請求番号.Width = 150
        '
        '入金済請求先
        '
        Me.入金済請求先.HeaderText = "請求先"
        Me.入金済請求先.Name = "入金済請求先"
        Me.入金済請求先.ReadOnly = True
        Me.入金済請求先.Width = 200
        '
        '入金番号
        '
        Me.入金番号.HeaderText = "入金番号"
        Me.入金番号.Name = "入金番号"
        Me.入金番号.ReadOnly = True
        Me.入金番号.Width = 150
        '
        '入金日
        '
        Me.入金日.HeaderText = "入金日"
        Me.入金日.Name = "入金日"
        Me.入金日.ReadOnly = True
        Me.入金日.Width = 150
        '
        '入金種目
        '
        Me.入金種目.HeaderText = "入金種目"
        Me.入金種目.Name = "入金種目"
        Me.入金種目.ReadOnly = True
        Me.入金種目.Width = 150
        '
        '入金済入金額計
        '
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.入金済入金額計.DefaultCellStyle = DataGridViewCellStyle16
        Me.入金済入金額計.HeaderText = "入金額計"
        Me.入金済入金額計.Name = "入金済入金額計"
        Me.入金済入金額計.ReadOnly = True
        Me.入金済入金額計.Width = 200
        '
        '備考
        '
        Me.備考.HeaderText = "備考"
        Me.備考.Name = "備考"
        Me.備考.ReadOnly = True
        Me.備考.Width = 200
        '
        'DgvDeposit
        '
        Me.DgvDeposit.AllowUserToAddRows = False
        Me.DgvDeposit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvDeposit.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.行番号, Me.入力入金額})
        Me.DgvDeposit.Location = New System.Drawing.Point(11, 229)
        Me.DgvDeposit.Name = "DgvDeposit"
        Me.DgvDeposit.RowHeadersVisible = False
        Me.DgvDeposit.RowTemplate.Height = 21
        Me.DgvDeposit.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvDeposit.Size = New System.Drawing.Size(1327, 100)
        Me.DgvDeposit.TabIndex = 6
        '
        'BtnAdd
        '
        Me.BtnAdd.Location = New System.Drawing.Point(101, 203)
        Me.BtnAdd.Name = "BtnAdd"
        Me.BtnAdd.Size = New System.Drawing.Size(94, 20)
        Me.BtnAdd.TabIndex = 3
        Me.BtnAdd.Text = "行追加"
        Me.BtnAdd.UseVisualStyleBackColor = True
        '
        'LblNo3
        '
        Me.LblNo3.BackColor = System.Drawing.Color.Transparent
        Me.LblNo3.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblNo3.Location = New System.Drawing.Point(1316, 335)
        Me.LblNo3.Name = "LblNo3"
        Me.LblNo3.Size = New System.Drawing.Size(22, 22)
        Me.LblNo3.TabIndex = 296
        Me.LblNo3.Text = "件"
        Me.LblNo3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtBillingCount
        '
        Me.TxtBillingCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtBillingCount.Enabled = False
        Me.TxtBillingCount.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtBillingCount.Location = New System.Drawing.Point(1272, 335)
        Me.TxtBillingCount.Name = "TxtBillingCount"
        Me.TxtBillingCount.Size = New System.Drawing.Size(38, 37)
        Me.TxtBillingCount.TabIndex = 295
        Me.TxtBillingCount.TabStop = False
        Me.TxtBillingCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'BtnDelete
        '
        Me.BtnDelete.Location = New System.Drawing.Point(201, 203)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(94, 20)
        Me.BtnDelete.TabIndex = 4
        Me.BtnDelete.Text = "行削除"
        Me.BtnDelete.UseVisualStyleBackColor = True
        '
        'BtnCal
        '
        Me.BtnCal.Location = New System.Drawing.Point(301, 203)
        Me.BtnCal.Name = "BtnCal"
        Me.BtnCal.Size = New System.Drawing.Size(94, 20)
        Me.BtnCal.TabIndex = 5
        Me.BtnCal.Text = "自動振分"
        Me.BtnCal.UseVisualStyleBackColor = True
        '
        'LblMode
        '
        Me.LblMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMode.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMode.Location = New System.Drawing.Point(1170, 9)
        Me.LblMode.Name = "LblMode"
        Me.LblMode.Size = New System.Drawing.Size(166, 22)
        Me.LblMode.TabIndex = 324
        Me.LblMode.Text = "モード"
        Me.LblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtIDRCurrency
        '
        Me.TxtIDRCurrency.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtIDRCurrency.Enabled = False
        Me.TxtIDRCurrency.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtIDRCurrency.Location = New System.Drawing.Point(662, 36)
        Me.TxtIDRCurrency.MaxLength = 20
        Me.TxtIDRCurrency.Name = "TxtIDRCurrency"
        Me.TxtIDRCurrency.ReadOnly = True
        Me.TxtIDRCurrency.Size = New System.Drawing.Size(70, 39)
        Me.TxtIDRCurrency.TabIndex = 326
        Me.TxtIDRCurrency.TabStop = False
        Me.TxtIDRCurrency.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LblIDRCurrency
        '
        Me.LblIDRCurrency.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblIDRCurrency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblIDRCurrency.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblIDRCurrency.Location = New System.Drawing.Point(556, 36)
        Me.LblIDRCurrency.Name = "LblIDRCurrency"
        Me.LblIDRCurrency.Size = New System.Drawing.Size(100, 23)
        Me.LblIDRCurrency.TabIndex = 325
        Me.LblIDRCurrency.Text = "通貨"
        Me.LblIDRCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        '行番号
        '
        Me.行番号.HeaderText = "行番号"
        Me.行番号.Name = "行番号"
        Me.行番号.ReadOnly = True
        Me.行番号.Width = 70
        '
        '入力入金額
        '
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle17.Format = "N0"
        Me.入力入金額.DefaultCellStyle = DataGridViewCellStyle17
        Me.入力入金額.HeaderText = "入金額"
        Me.入力入金額.MaxInputLength = 14
        Me.入力入金額.Name = "入力入金額"
        '
        'InfoNo
        '
        Me.InfoNo.HeaderText = "No"
        Me.InfoNo.Name = "InfoNo"
        Me.InfoNo.ReadOnly = True
        Me.InfoNo.Width = 70
        '
        '請求情報請求番号
        '
        Me.請求情報請求番号.HeaderText = "請求番号"
        Me.請求情報請求番号.Name = "請求情報請求番号"
        Me.請求情報請求番号.ReadOnly = True
        Me.請求情報請求番号.Width = 150
        '
        '請求日
        '
        Me.請求日.HeaderText = "請求日"
        Me.請求日.Name = "請求日"
        Me.請求日.ReadOnly = True
        Me.請求日.Width = 150
        '
        '請求金額
        '
        DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.請求金額.DefaultCellStyle = DataGridViewCellStyle18
        Me.請求金額.HeaderText = "既請求額"
        Me.請求金額.Name = "請求金額"
        Me.請求金額.ReadOnly = True
        Me.請求金額.Width = 150
        '
        '請求情報入金額計
        '
        DataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.請求情報入金額計.DefaultCellStyle = DataGridViewCellStyle19
        Me.請求情報入金額計.HeaderText = "既入金額"
        Me.請求情報入金額計.Name = "請求情報入金額計"
        Me.請求情報入金額計.ReadOnly = True
        Me.請求情報入金額計.Width = 150
        '
        '請求情報入金額計固定
        '
        Me.請求情報入金額計固定.HeaderText = "請求情報入金額計固定"
        Me.請求情報入金額計固定.Name = "請求情報入金額計固定"
        Me.請求情報入金額計固定.ReadOnly = True
        Me.請求情報入金額計固定.Visible = False
        '
        '請求情報請求残高
        '
        DataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.請求情報請求残高.DefaultCellStyle = DataGridViewCellStyle20
        Me.請求情報請求残高.HeaderText = "未入金額"
        Me.請求情報請求残高.Name = "請求情報請求残高"
        Me.請求情報請求残高.ReadOnly = True
        Me.請求情報請求残高.Width = 150
        '
        '請求情報請求残高固定
        '
        Me.請求情報請求残高固定.HeaderText = "請求情報請求残高固定"
        Me.請求情報請求残高固定.Name = "請求情報請求残高固定"
        Me.請求情報請求残高固定.ReadOnly = True
        Me.請求情報請求残高固定.Visible = False
        '
        '入金額
        '
        DataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle21.Format = "N0"
        Me.入金額.DefaultCellStyle = DataGridViewCellStyle21
        Me.入金額.HeaderText = "今回入金額"
        Me.入金額.MaxInputLength = 14
        Me.入金額.Name = "入金額"
        Me.入金額.Width = 150
        '
        'DepositManagement
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.ControlBox = False
        Me.Controls.Add(Me.TxtIDRCurrency)
        Me.Controls.Add(Me.LblIDRCurrency)
        Me.Controls.Add(Me.LblMode)
        Me.Controls.Add(Me.BtnCal)
        Me.Controls.Add(Me.BtnDelete)
        Me.Controls.Add(Me.LblNo3)
        Me.Controls.Add(Me.TxtBillingCount)
        Me.Controls.Add(Me.BtnAdd)
        Me.Controls.Add(Me.BtnRegist)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.LblNo1)
        Me.Controls.Add(Me.TxtHistoryCount)
        Me.Controls.Add(Me.DtpDepositDate)
        Me.Controls.Add(Me.LblRemarks)
        Me.Controls.Add(Me.TxtRemarks)
        Me.Controls.Add(Me.LblDepositDate)
        Me.Controls.Add(Me.LblNo2)
        Me.Controls.Add(Me.TxtDepositCount)
        Me.Controls.Add(Me.DgvCustomer)
        Me.Controls.Add(Me.LblBillingInfo)
        Me.Controls.Add(Me.LblDeposit)
        Me.Controls.Add(Me.DgvBillingInfo)
        Me.Controls.Add(Me.DgvDeposit)
        Me.Controls.Add(Me.LblHistory)
        Me.Controls.Add(Me.DgvHistory)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "DepositManagement"
        Me.Text = "DepositManagement"
        CType(Me.DgvCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvBillingInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvDeposit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnRegist As Button
    Friend WithEvents BtnBack As Button
    Friend WithEvents LblNo1 As Label
    Friend WithEvents TxtHistoryCount As TextBox
    Friend WithEvents DtpDepositDate As DateTimePicker
    Friend WithEvents LblRemarks As Label
    Friend WithEvents TxtRemarks As TextBox
    Friend WithEvents LblDepositDate As Label
    Friend WithEvents LblNo2 As Label
    Friend WithEvents TxtDepositCount As TextBox
    Friend WithEvents DgvCustomer As DataGridView
    Friend WithEvents LblBillingInfo As Label
    Friend WithEvents LblHistory As Label
    Friend WithEvents LblDeposit As Label
    Friend WithEvents DgvBillingInfo As DataGridView
    Friend WithEvents DgvHistory As DataGridView
    Friend WithEvents DgvDeposit As DataGridView
    Friend WithEvents BtnAdd As Button
    Friend WithEvents LblNo3 As Label
    Friend WithEvents TxtBillingCount As TextBox
    Friend WithEvents BtnDelete As Button
    Friend WithEvents BtnCal As Button
    Friend WithEvents LblMode As Label
    Friend WithEvents 請求先 As DataGridViewTextBoxColumn
    Friend WithEvents 請求残高 As DataGridViewTextBoxColumn
    Friend WithEvents No As DataGridViewTextBoxColumn
    Friend WithEvents 請求番号 As DataGridViewTextBoxColumn
    Friend WithEvents 入金済請求先 As DataGridViewTextBoxColumn
    Friend WithEvents 入金番号 As DataGridViewTextBoxColumn
    Friend WithEvents 入金日 As DataGridViewTextBoxColumn
    Friend WithEvents 入金種目 As DataGridViewTextBoxColumn
    Friend WithEvents 入金済入金額計 As DataGridViewTextBoxColumn
    Friend WithEvents 備考 As DataGridViewTextBoxColumn
    Friend WithEvents TxtIDRCurrency As TextBox
    Friend WithEvents LblIDRCurrency As Label
    Friend WithEvents InfoNo As DataGridViewTextBoxColumn
    Friend WithEvents 請求情報請求番号 As DataGridViewTextBoxColumn
    Friend WithEvents 請求日 As DataGridViewTextBoxColumn
    Friend WithEvents 請求金額 As DataGridViewTextBoxColumn
    Friend WithEvents 請求情報入金額計 As DataGridViewTextBoxColumn
    Friend WithEvents 請求情報入金額計固定 As DataGridViewTextBoxColumn
    Friend WithEvents 請求情報請求残高 As DataGridViewTextBoxColumn
    Friend WithEvents 請求情報請求残高固定 As DataGridViewTextBoxColumn
    Friend WithEvents 入金額 As DataGridViewTextBoxColumn
    Friend WithEvents 行番号 As DataGridViewTextBoxColumn
    Friend WithEvents 入力入金額 As DataGridViewTextBoxColumn
End Class
