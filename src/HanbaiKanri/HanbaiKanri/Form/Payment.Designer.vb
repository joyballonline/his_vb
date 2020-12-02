<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Payment
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BtnDelete = New System.Windows.Forms.Button()
        Me.DgvHistory = New System.Windows.Forms.DataGridView()
        Me.No = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.売掛番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.支払済支払先 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.支払番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.支払日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.支払種目 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.支払済支払金額計 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.備考 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DgvPayment = New System.Windows.Forms.DataGridView()
        Me.LblNo3 = New System.Windows.Forms.Label()
        Me.TxtKikeCount = New System.Windows.Forms.TextBox()
        Me.BtnAdd = New System.Windows.Forms.Button()
        Me.BtnCal = New System.Windows.Forms.Button()
        Me.BtnRegist = New System.Windows.Forms.Button()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.LblNo1 = New System.Windows.Forms.Label()
        Me.TxtHistoryCount = New System.Windows.Forms.TextBox()
        Me.DtpDepositDate = New System.Windows.Forms.DateTimePicker()
        Me.LblRemarks = New System.Windows.Forms.Label()
        Me.TxtRemarks = New System.Windows.Forms.TextBox()
        Me.LblDepositDate = New System.Windows.Forms.Label()
        Me.LblNo2 = New System.Windows.Forms.Label()
        Me.TxtPaymentCount = New System.Windows.Forms.TextBox()
        Me.DgvSupplier = New System.Windows.Forms.DataGridView()
        Me.支払先 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛残高 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LblAPInfo = New System.Windows.Forms.Label()
        Me.LblPayment = New System.Windows.Forms.Label()
        Me.DgvKikeInfo = New System.Windows.Forms.DataGridView()
        Me.InfoNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.発注番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入先請求番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛情報買掛番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.支払予定日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛金額 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛情報支払金額計 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.支払金額計固定 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛情報買掛残高 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛情報買掛残高固定 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.支払金額 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛区分 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.発注番号枝番 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入先コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.客先番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LblHistory = New System.Windows.Forms.Label()
        Me.LblMode = New System.Windows.Forms.Label()
        Me.TxtIDRCurrency = New System.Windows.Forms.TextBox()
        Me.LblIDRCurrency = New System.Windows.Forms.Label()
        Me.ShiwakeData = New System.Windows.Forms.DataGridView()
        Me.買掛番号_仕訳 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛区分_仕訳 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛日_仕訳 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.発注番号_仕訳 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.発注番号枝番_仕訳 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入先コード_仕訳 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.支払番号_仕訳 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.識別番号_仕訳 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.行番号_仕訳 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.支払種目_仕訳 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.支払種目名_仕訳 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.支払額_仕訳 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.客先番号_仕訳 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.行番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.入力支払金額 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.入力支払額_計算用 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.REMARK1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DgvHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvPayment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvKikeInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ShiwakeData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnDelete
        '
        Me.BtnDelete.Location = New System.Drawing.Point(202, 203)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(94, 20)
        Me.BtnDelete.TabIndex = 319
        Me.BtnDelete.TabStop = False
        Me.BtnDelete.Text = "行削除"
        Me.BtnDelete.UseVisualStyleBackColor = True
        '
        'DgvHistory
        '
        Me.DgvHistory.AllowUserToAddRows = False
        Me.DgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvHistory.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.No, Me.売掛番号, Me.支払済支払先, Me.支払番号, Me.支払日, Me.支払種目, Me.支払済支払金額計, Me.備考})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvHistory.DefaultCellStyle = DataGridViewCellStyle2
        Me.DgvHistory.Location = New System.Drawing.Point(11, 92)
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
        '売掛番号
        '
        Me.売掛番号.HeaderText = "売掛番号"
        Me.売掛番号.Name = "売掛番号"
        Me.売掛番号.ReadOnly = True
        Me.売掛番号.Visible = False
        Me.売掛番号.Width = 150
        '
        '支払済支払先
        '
        Me.支払済支払先.HeaderText = "支払先"
        Me.支払済支払先.Name = "支払済支払先"
        Me.支払済支払先.ReadOnly = True
        Me.支払済支払先.Width = 200
        '
        '支払番号
        '
        Me.支払番号.HeaderText = "支払番号"
        Me.支払番号.Name = "支払番号"
        Me.支払番号.ReadOnly = True
        Me.支払番号.Width = 150
        '
        '支払日
        '
        Me.支払日.HeaderText = "支払日"
        Me.支払日.Name = "支払日"
        Me.支払日.ReadOnly = True
        Me.支払日.Width = 150
        '
        '支払種目
        '
        Me.支払種目.HeaderText = "支払種目"
        Me.支払種目.Name = "支払種目"
        Me.支払種目.ReadOnly = True
        Me.支払種目.Width = 150
        '
        '支払済支払金額計
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.支払済支払金額計.DefaultCellStyle = DataGridViewCellStyle1
        Me.支払済支払金額計.HeaderText = "支払金額計"
        Me.支払済支払金額計.Name = "支払済支払金額計"
        Me.支払済支払金額計.ReadOnly = True
        Me.支払済支払金額計.Width = 200
        '
        '備考
        '
        Me.備考.HeaderText = "備考"
        Me.備考.Name = "備考"
        Me.備考.ReadOnly = True
        Me.備考.Width = 200
        '
        'DgvPayment
        '
        Me.DgvPayment.AllowUserToAddRows = False
        Me.DgvPayment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvPayment.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.行番号, Me.入力支払金額, Me.入力支払額_計算用, Me.REMARK1})
        Me.DgvPayment.Location = New System.Drawing.Point(12, 229)
        Me.DgvPayment.Name = "DgvPayment"
        Me.DgvPayment.RowHeadersVisible = False
        Me.DgvPayment.RowTemplate.Height = 21
        Me.DgvPayment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvPayment.Size = New System.Drawing.Size(1327, 100)
        Me.DgvPayment.TabIndex = 5
        '
        'LblNo3
        '
        Me.LblNo3.BackColor = System.Drawing.Color.Transparent
        Me.LblNo3.Font = New System.Drawing.Font("MS Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblNo3.Location = New System.Drawing.Point(1317, 335)
        Me.LblNo3.Name = "LblNo3"
        Me.LblNo3.Size = New System.Drawing.Size(22, 22)
        Me.LblNo3.TabIndex = 318
        Me.LblNo3.Text = "件"
        Me.LblNo3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtKikeCount
        '
        Me.TxtKikeCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtKikeCount.Enabled = False
        Me.TxtKikeCount.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtKikeCount.Location = New System.Drawing.Point(1273, 335)
        Me.TxtKikeCount.Name = "TxtKikeCount"
        Me.TxtKikeCount.Size = New System.Drawing.Size(38, 22)
        Me.TxtKikeCount.TabIndex = 317
        Me.TxtKikeCount.TabStop = False
        Me.TxtKikeCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'BtnAdd
        '
        Me.BtnAdd.Location = New System.Drawing.Point(102, 203)
        Me.BtnAdd.Name = "BtnAdd"
        Me.BtnAdd.Size = New System.Drawing.Size(94, 20)
        Me.BtnAdd.TabIndex = 3
        Me.BtnAdd.TabStop = False
        Me.BtnAdd.Text = "行追加"
        Me.BtnAdd.UseVisualStyleBackColor = True
        '
        'BtnCal
        '
        Me.BtnCal.Location = New System.Drawing.Point(302, 203)
        Me.BtnCal.Name = "BtnCal"
        Me.BtnCal.Size = New System.Drawing.Size(94, 20)
        Me.BtnCal.TabIndex = 4
        Me.BtnCal.TabStop = False
        Me.BtnCal.Text = "自動振分"
        Me.BtnCal.UseVisualStyleBackColor = True
        '
        'BtnRegist
        '
        Me.BtnRegist.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnRegist.Location = New System.Drawing.Point(1004, 650)
        Me.BtnRegist.Name = "BtnRegist"
        Me.BtnRegist.Size = New System.Drawing.Size(165, 40)
        Me.BtnRegist.TabIndex = 9
        Me.BtnRegist.Text = "登録"
        Me.BtnRegist.UseVisualStyleBackColor = True
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(1175, 650)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 10
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'LblNo1
        '
        Me.LblNo1.BackColor = System.Drawing.Color.Transparent
        Me.LblNo1.Font = New System.Drawing.Font("MS Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblNo1.Location = New System.Drawing.Point(1317, 65)
        Me.LblNo1.Name = "LblNo1"
        Me.LblNo1.Size = New System.Drawing.Size(22, 22)
        Me.LblNo1.TabIndex = 313
        Me.LblNo1.Text = "件"
        Me.LblNo1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtHistoryCount
        '
        Me.TxtHistoryCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtHistoryCount.Enabled = False
        Me.TxtHistoryCount.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtHistoryCount.Location = New System.Drawing.Point(1273, 65)
        Me.TxtHistoryCount.Name = "TxtHistoryCount"
        Me.TxtHistoryCount.Size = New System.Drawing.Size(38, 22)
        Me.TxtHistoryCount.TabIndex = 312
        Me.TxtHistoryCount.TabStop = False
        Me.TxtHistoryCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DtpDepositDate
        '
        Me.DtpDepositDate.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpDepositDate.CustomFormat = ""
        Me.DtpDepositDate.Font = New System.Drawing.Font("MS Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpDepositDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpDepositDate.Location = New System.Drawing.Point(292, 335)
        Me.DtpDepositDate.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.DtpDepositDate.Name = "DtpDepositDate"
        Me.DtpDepositDate.Size = New System.Drawing.Size(148, 22)
        Me.DtpDepositDate.TabIndex = 6
        Me.DtpDepositDate.TabStop = False
        Me.DtpDepositDate.Value = New Date(2018, 7, 25, 13, 29, 25, 0)
        '
        'LblRemarks
        '
        Me.LblRemarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblRemarks.Font = New System.Drawing.Font("MS Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblRemarks.Location = New System.Drawing.Point(446, 335)
        Me.LblRemarks.Name = "LblRemarks"
        Me.LblRemarks.Size = New System.Drawing.Size(170, 22)
        Me.LblRemarks.TabIndex = 310
        Me.LblRemarks.Text = "備考"
        Me.LblRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.LblRemarks.Visible = False
        '
        'TxtRemarks
        '
        Me.TxtRemarks.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtRemarks.Location = New System.Drawing.Point(622, 335)
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(644, 22)
        Me.TxtRemarks.TabIndex = 7
        Me.TxtRemarks.Visible = False
        '
        'LblDepositDate
        '
        Me.LblDepositDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblDepositDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblDepositDate.Font = New System.Drawing.Font("MS Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblDepositDate.Location = New System.Drawing.Point(188, 335)
        Me.LblDepositDate.Name = "LblDepositDate"
        Me.LblDepositDate.Size = New System.Drawing.Size(98, 22)
        Me.LblDepositDate.TabIndex = 308
        Me.LblDepositDate.Text = "支払日"
        Me.LblDepositDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblNo2
        '
        Me.LblNo2.BackColor = System.Drawing.Color.Transparent
        Me.LblNo2.Font = New System.Drawing.Font("MS Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblNo2.Location = New System.Drawing.Point(1317, 202)
        Me.LblNo2.Name = "LblNo2"
        Me.LblNo2.Size = New System.Drawing.Size(22, 22)
        Me.LblNo2.TabIndex = 307
        Me.LblNo2.Text = "件"
        Me.LblNo2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtPaymentCount
        '
        Me.TxtPaymentCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtPaymentCount.Enabled = False
        Me.TxtPaymentCount.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPaymentCount.Location = New System.Drawing.Point(1273, 202)
        Me.TxtPaymentCount.Name = "TxtPaymentCount"
        Me.TxtPaymentCount.Size = New System.Drawing.Size(38, 22)
        Me.TxtPaymentCount.TabIndex = 306
        Me.TxtPaymentCount.TabStop = False
        Me.TxtPaymentCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DgvSupplier
        '
        Me.DgvSupplier.AllowUserToAddRows = False
        Me.DgvSupplier.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvSupplier.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.支払先, Me.買掛残高})
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle6.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvSupplier.DefaultCellStyle = DataGridViewCellStyle6
        Me.DgvSupplier.Location = New System.Drawing.Point(13, 19)
        Me.DgvSupplier.Name = "DgvSupplier"
        Me.DgvSupplier.RowHeadersVisible = False
        Me.DgvSupplier.RowTemplate.Height = 21
        Me.DgvSupplier.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvSupplier.Size = New System.Drawing.Size(504, 40)
        Me.DgvSupplier.TabIndex = 1
        '
        '支払先
        '
        Me.支払先.HeaderText = "支払先"
        Me.支払先.Name = "支払先"
        Me.支払先.ReadOnly = True
        Me.支払先.Width = 300
        '
        '買掛残高
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.買掛残高.DefaultCellStyle = DataGridViewCellStyle5
        Me.買掛残高.HeaderText = "買掛残高"
        Me.買掛残高.Name = "買掛残高"
        Me.買掛残高.ReadOnly = True
        Me.買掛残高.Width = 200
        '
        'LblAPInfo
        '
        Me.LblAPInfo.AutoSize = True
        Me.LblAPInfo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAPInfo.Location = New System.Drawing.Point(14, 339)
        Me.LblAPInfo.Name = "LblAPInfo"
        Me.LblAPInfo.Size = New System.Drawing.Size(82, 15)
        Me.LblAPInfo.TabIndex = 305
        Me.LblAPInfo.Text = "■買掛情報"
        '
        'LblPayment
        '
        Me.LblPayment.AutoSize = True
        Me.LblPayment.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPayment.Location = New System.Drawing.Point(14, 203)
        Me.LblPayment.Name = "LblPayment"
        Me.LblPayment.Size = New System.Drawing.Size(82, 15)
        Me.LblPayment.TabIndex = 303
        Me.LblPayment.Text = "■支払入力"
        '
        'DgvKikeInfo
        '
        Me.DgvKikeInfo.AllowUserToAddRows = False
        Me.DgvKikeInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvKikeInfo.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.InfoNo, Me.発注番号, Me.仕入先請求番号, Me.買掛情報買掛番号, Me.買掛日, Me.支払予定日, Me.買掛金額, Me.買掛情報支払金額計, Me.支払金額計固定, Me.買掛情報買掛残高, Me.買掛情報買掛残高固定, Me.支払金額, Me.買掛区分, Me.発注番号枝番, Me.仕入先コード, Me.客先番号})
        Me.DgvKikeInfo.Location = New System.Drawing.Point(12, 363)
        Me.DgvKikeInfo.Name = "DgvKikeInfo"
        Me.DgvKikeInfo.RowHeadersVisible = False
        Me.DgvKikeInfo.RowTemplate.Height = 21
        Me.DgvKikeInfo.Size = New System.Drawing.Size(1327, 280)
        Me.DgvKikeInfo.TabIndex = 8
        '
        'InfoNo
        '
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.InfoNo.DefaultCellStyle = DataGridViewCellStyle7
        Me.InfoNo.HeaderText = "No"
        Me.InfoNo.Name = "InfoNo"
        Me.InfoNo.ReadOnly = True
        Me.InfoNo.Width = 70
        '
        '発注番号
        '
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.発注番号.DefaultCellStyle = DataGridViewCellStyle8
        Me.発注番号.HeaderText = "発注番号"
        Me.発注番号.MaxInputLength = 10
        Me.発注番号.Name = "発注番号"
        '
        '仕入先請求番号
        '
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.仕入先請求番号.DefaultCellStyle = DataGridViewCellStyle9
        Me.仕入先請求番号.HeaderText = "Supplier Invoice No."
        Me.仕入先請求番号.MaxInputLength = 100
        Me.仕入先請求番号.Name = "仕入先請求番号"
        Me.仕入先請求番号.Width = 150
        '
        '買掛情報買掛番号
        '
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.買掛情報買掛番号.DefaultCellStyle = DataGridViewCellStyle10
        Me.買掛情報買掛番号.HeaderText = "買掛番号"
        Me.買掛情報買掛番号.Name = "買掛情報買掛番号"
        Me.買掛情報買掛番号.ReadOnly = True
        '
        '買掛日
        '
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.買掛日.DefaultCellStyle = DataGridViewCellStyle11
        Me.買掛日.HeaderText = "Supplier InvoiceDate"
        Me.買掛日.Name = "買掛日"
        Me.買掛日.ReadOnly = True
        Me.買掛日.Width = 120
        '
        '支払予定日
        '
        DataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.支払予定日.DefaultCellStyle = DataGridViewCellStyle12
        Me.支払予定日.HeaderText = "支払予定日"
        Me.支払予定日.Name = "支払予定日"
        Me.支払予定日.ReadOnly = True
        '
        '買掛金額
        '
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.買掛金額.DefaultCellStyle = DataGridViewCellStyle13
        Me.買掛金額.HeaderText = "買掛金額"
        Me.買掛金額.Name = "買掛金額"
        Me.買掛金額.ReadOnly = True
        Me.買掛金額.Width = 150
        '
        '買掛情報支払金額計
        '
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.買掛情報支払金額計.DefaultCellStyle = DataGridViewCellStyle14
        Me.買掛情報支払金額計.HeaderText = "既支払額"
        Me.買掛情報支払金額計.Name = "買掛情報支払金額計"
        Me.買掛情報支払金額計.ReadOnly = True
        Me.買掛情報支払金額計.Width = 150
        '
        '支払金額計固定
        '
        DataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.支払金額計固定.DefaultCellStyle = DataGridViewCellStyle15
        Me.支払金額計固定.HeaderText = "支払金額計固定"
        Me.支払金額計固定.Name = "支払金額計固定"
        Me.支払金額計固定.ReadOnly = True
        Me.支払金額計固定.Visible = False
        '
        '買掛情報買掛残高
        '
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle16.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.買掛情報買掛残高.DefaultCellStyle = DataGridViewCellStyle16
        Me.買掛情報買掛残高.HeaderText = "買掛残高(残債務)"
        Me.買掛情報買掛残高.Name = "買掛情報買掛残高"
        Me.買掛情報買掛残高.ReadOnly = True
        Me.買掛情報買掛残高.Width = 150
        '
        '買掛情報買掛残高固定
        '
        DataGridViewCellStyle17.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.買掛情報買掛残高固定.DefaultCellStyle = DataGridViewCellStyle17
        Me.買掛情報買掛残高固定.HeaderText = "買掛情報買掛残高固定"
        Me.買掛情報買掛残高固定.Name = "買掛情報買掛残高固定"
        Me.買掛情報買掛残高固定.ReadOnly = True
        Me.買掛情報買掛残高固定.Visible = False
        '
        '支払金額
        '
        DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.支払金額.DefaultCellStyle = DataGridViewCellStyle18
        Me.支払金額.HeaderText = "今回支払金額"
        Me.支払金額.MaxInputLength = 14
        Me.支払金額.Name = "支払金額"
        Me.支払金額.Width = 150
        '
        '買掛区分
        '
        Me.買掛区分.HeaderText = "買掛区分"
        Me.買掛区分.Name = "買掛区分"
        Me.買掛区分.Visible = False
        '
        '発注番号枝番
        '
        Me.発注番号枝番.HeaderText = "発注番号枝番"
        Me.発注番号枝番.Name = "発注番号枝番"
        Me.発注番号枝番.Visible = False
        '
        '仕入先コード
        '
        Me.仕入先コード.HeaderText = "仕入先コード"
        Me.仕入先コード.Name = "仕入先コード"
        Me.仕入先コード.Visible = False
        '
        '客先番号
        '
        Me.客先番号.HeaderText = "客先番号"
        Me.客先番号.Name = "客先番号"
        Me.客先番号.Visible = False
        '
        'LblHistory
        '
        Me.LblHistory.AutoSize = True
        Me.LblHistory.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblHistory.Location = New System.Drawing.Point(13, 68)
        Me.LblHistory.Name = "LblHistory"
        Me.LblHistory.Size = New System.Drawing.Size(80, 15)
        Me.LblHistory.TabIndex = 304
        Me.LblHistory.Text = "■支払済み"
        '
        'LblMode
        '
        Me.LblMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMode.Font = New System.Drawing.Font("MS Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMode.Location = New System.Drawing.Point(1172, 9)
        Me.LblMode.Name = "LblMode"
        Me.LblMode.Size = New System.Drawing.Size(165, 22)
        Me.LblMode.TabIndex = 321
        Me.LblMode.Text = "支払登録モード"
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
        Me.TxtIDRCurrency.Size = New System.Drawing.Size(70, 23)
        Me.TxtIDRCurrency.TabIndex = 332
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
        Me.LblIDRCurrency.TabIndex = 331
        Me.LblIDRCurrency.Text = "通貨"
        Me.LblIDRCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ShiwakeData
        '
        Me.ShiwakeData.AllowUserToAddRows = False
        Me.ShiwakeData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ShiwakeData.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.買掛番号_仕訳, Me.買掛区分_仕訳, Me.買掛日_仕訳, Me.発注番号_仕訳, Me.発注番号枝番_仕訳, Me.仕入先コード_仕訳, Me.支払番号_仕訳, Me.識別番号_仕訳, Me.行番号_仕訳, Me.支払種目_仕訳, Me.支払種目名_仕訳, Me.支払額_仕訳, Me.客先番号_仕訳})
        Me.ShiwakeData.Location = New System.Drawing.Point(29, 469)
        Me.ShiwakeData.Name = "ShiwakeData"
        Me.ShiwakeData.RowHeadersVisible = False
        Me.ShiwakeData.RowTemplate.Height = 21
        Me.ShiwakeData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.ShiwakeData.Size = New System.Drawing.Size(969, 80)
        Me.ShiwakeData.TabIndex = 333
        Me.ShiwakeData.Visible = False
        '
        '買掛番号_仕訳
        '
        Me.買掛番号_仕訳.HeaderText = "買掛番号"
        Me.買掛番号_仕訳.Name = "買掛番号_仕訳"
        '
        '買掛区分_仕訳
        '
        Me.買掛区分_仕訳.HeaderText = "買掛区分"
        Me.買掛区分_仕訳.Name = "買掛区分_仕訳"
        '
        '買掛日_仕訳
        '
        Me.買掛日_仕訳.HeaderText = "買掛日"
        Me.買掛日_仕訳.Name = "買掛日_仕訳"
        '
        '発注番号_仕訳
        '
        Me.発注番号_仕訳.HeaderText = "発注番号"
        Me.発注番号_仕訳.Name = "発注番号_仕訳"
        '
        '発注番号枝番_仕訳
        '
        Me.発注番号枝番_仕訳.HeaderText = "発注番号枝番"
        Me.発注番号枝番_仕訳.Name = "発注番号枝番_仕訳"
        '
        '仕入先コード_仕訳
        '
        Me.仕入先コード_仕訳.HeaderText = "仕入先コード"
        Me.仕入先コード_仕訳.Name = "仕入先コード_仕訳"
        '
        '支払番号_仕訳
        '
        Me.支払番号_仕訳.HeaderText = "支払番号"
        Me.支払番号_仕訳.Name = "支払番号_仕訳"
        '
        '識別番号_仕訳
        '
        Me.識別番号_仕訳.HeaderText = "識別番号"
        Me.識別番号_仕訳.Name = "識別番号_仕訳"
        '
        '行番号_仕訳
        '
        DataGridViewCellStyle19.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.行番号_仕訳.DefaultCellStyle = DataGridViewCellStyle19
        Me.行番号_仕訳.HeaderText = "行番号"
        Me.行番号_仕訳.Name = "行番号_仕訳"
        Me.行番号_仕訳.ReadOnly = True
        Me.行番号_仕訳.Width = 70
        '
        '支払種目_仕訳
        '
        Me.支払種目_仕訳.HeaderText = "支払種別"
        Me.支払種目_仕訳.Name = "支払種目_仕訳"
        '
        '支払種目名_仕訳
        '
        Me.支払種目名_仕訳.HeaderText = "支払種別名"
        Me.支払種目名_仕訳.Name = "支払種目名_仕訳"
        '
        '支払額_仕訳
        '
        Me.支払額_仕訳.HeaderText = "支払額"
        Me.支払額_仕訳.Name = "支払額_仕訳"
        '
        '客先番号_仕訳
        '
        Me.客先番号_仕訳.HeaderText = "客先番号"
        Me.客先番号_仕訳.Name = "客先番号_仕訳"
        '
        '行番号
        '
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.行番号.DefaultCellStyle = DataGridViewCellStyle3
        Me.行番号.HeaderText = "行番号"
        Me.行番号.Name = "行番号"
        Me.行番号.ReadOnly = True
        Me.行番号.Width = 70
        '
        '入力支払金額
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.入力支払金額.DefaultCellStyle = DataGridViewCellStyle4
        Me.入力支払金額.HeaderText = "今回支払金額"
        Me.入力支払金額.MaxInputLength = 14
        Me.入力支払金額.Name = "入力支払金額"
        Me.入力支払金額.Width = 130
        '
        '入力支払額_計算用
        '
        Me.入力支払額_計算用.HeaderText = "入力支払額_計算用"
        Me.入力支払額_計算用.Name = "入力支払額_計算用"
        Me.入力支払額_計算用.Visible = False
        '
        'REMARK1
        '
        Me.REMARK1.HeaderText = "備考"
        Me.REMARK1.Name = "REMARK1"
        Me.REMARK1.Width = 300
        '
        'Payment
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 711)
        Me.Controls.Add(Me.ShiwakeData)
        Me.Controls.Add(Me.TxtIDRCurrency)
        Me.Controls.Add(Me.LblIDRCurrency)
        Me.Controls.Add(Me.LblMode)
        Me.Controls.Add(Me.BtnDelete)
        Me.Controls.Add(Me.DgvHistory)
        Me.Controls.Add(Me.DgvPayment)
        Me.Controls.Add(Me.LblNo3)
        Me.Controls.Add(Me.TxtKikeCount)
        Me.Controls.Add(Me.BtnAdd)
        Me.Controls.Add(Me.BtnCal)
        Me.Controls.Add(Me.BtnRegist)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.LblNo1)
        Me.Controls.Add(Me.TxtHistoryCount)
        Me.Controls.Add(Me.DtpDepositDate)
        Me.Controls.Add(Me.LblRemarks)
        Me.Controls.Add(Me.TxtRemarks)
        Me.Controls.Add(Me.LblDepositDate)
        Me.Controls.Add(Me.LblNo2)
        Me.Controls.Add(Me.TxtPaymentCount)
        Me.Controls.Add(Me.DgvSupplier)
        Me.Controls.Add(Me.LblAPInfo)
        Me.Controls.Add(Me.LblPayment)
        Me.Controls.Add(Me.DgvKikeInfo)
        Me.Controls.Add(Me.LblHistory)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Payment"
        Me.Text = "Payment"
        CType(Me.DgvHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvPayment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvKikeInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ShiwakeData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnDelete As Button
    Friend WithEvents DgvHistory As DataGridView
    Friend WithEvents DgvPayment As DataGridView
    Friend WithEvents LblNo3 As Label
    Friend WithEvents TxtKikeCount As TextBox
    Friend WithEvents BtnAdd As Button
    Friend WithEvents BtnCal As Button
    Friend WithEvents BtnRegist As Button
    Friend WithEvents BtnBack As Button
    Friend WithEvents LblNo1 As Label
    Friend WithEvents TxtHistoryCount As TextBox
    Friend WithEvents DtpDepositDate As DateTimePicker
    Friend WithEvents LblRemarks As Label
    Friend WithEvents TxtRemarks As TextBox
    Friend WithEvents LblDepositDate As Label
    Friend WithEvents LblNo2 As Label
    Friend WithEvents TxtPaymentCount As TextBox
    Friend WithEvents DgvSupplier As DataGridView
    Friend WithEvents LblAPInfo As Label
    Friend WithEvents LblPayment As Label
    Friend WithEvents DgvKikeInfo As DataGridView
    Friend WithEvents LblHistory As Label
    Friend WithEvents LblMode As Label
    Friend WithEvents No As DataGridViewTextBoxColumn
    Friend WithEvents 売掛番号 As DataGridViewTextBoxColumn
    Friend WithEvents 支払済支払先 As DataGridViewTextBoxColumn
    Friend WithEvents 支払番号 As DataGridViewTextBoxColumn
    Friend WithEvents 支払日 As DataGridViewTextBoxColumn
    Friend WithEvents 支払種目 As DataGridViewTextBoxColumn
    Friend WithEvents 支払済支払金額計 As DataGridViewTextBoxColumn
    Friend WithEvents 備考 As DataGridViewTextBoxColumn
    Friend WithEvents 支払先 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛残高 As DataGridViewTextBoxColumn
    Friend WithEvents TxtIDRCurrency As TextBox
    Friend WithEvents LblIDRCurrency As Label
    Friend WithEvents ShiwakeData As DataGridView
    Friend WithEvents 買掛番号_仕訳 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛区分_仕訳 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛日_仕訳 As DataGridViewTextBoxColumn
    Friend WithEvents 発注番号_仕訳 As DataGridViewTextBoxColumn
    Friend WithEvents 発注番号枝番_仕訳 As DataGridViewTextBoxColumn
    Friend WithEvents 仕入先コード_仕訳 As DataGridViewTextBoxColumn
    Friend WithEvents 支払番号_仕訳 As DataGridViewTextBoxColumn
    Friend WithEvents 識別番号_仕訳 As DataGridViewTextBoxColumn
    Friend WithEvents 行番号_仕訳 As DataGridViewTextBoxColumn
    Friend WithEvents 支払種目_仕訳 As DataGridViewTextBoxColumn
    Friend WithEvents 支払種目名_仕訳 As DataGridViewTextBoxColumn
    Friend WithEvents 支払額_仕訳 As DataGridViewTextBoxColumn
    Friend WithEvents 客先番号_仕訳 As DataGridViewTextBoxColumn
    Friend WithEvents InfoNo As DataGridViewTextBoxColumn
    Friend WithEvents 発注番号 As DataGridViewTextBoxColumn
    Friend WithEvents 仕入先請求番号 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛情報買掛番号 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛日 As DataGridViewTextBoxColumn
    Friend WithEvents 支払予定日 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛金額 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛情報支払金額計 As DataGridViewTextBoxColumn
    Friend WithEvents 支払金額計固定 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛情報買掛残高 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛情報買掛残高固定 As DataGridViewTextBoxColumn
    Friend WithEvents 支払金額 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛区分 As DataGridViewTextBoxColumn
    Friend WithEvents 発注番号枝番 As DataGridViewTextBoxColumn
    Friend WithEvents 仕入先コード As DataGridViewTextBoxColumn
    Friend WithEvents 客先番号 As DataGridViewTextBoxColumn
    Friend WithEvents 行番号 As DataGridViewTextBoxColumn
    Friend WithEvents 入力支払金額 As DataGridViewTextBoxColumn
    Friend WithEvents 入力支払額_計算用 As DataGridViewTextBoxColumn
    Friend WithEvents REMARK1 As DataGridViewTextBoxColumn
End Class
