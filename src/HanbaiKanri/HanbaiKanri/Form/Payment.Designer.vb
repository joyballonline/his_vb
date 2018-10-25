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
        Me.DgvDeposit = New System.Windows.Forms.DataGridView()
        Me.行番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.入力支払金額 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LblNo3 = New System.Windows.Forms.Label()
        Me.TxtCount3 = New System.Windows.Forms.TextBox()
        Me.BtnAdd = New System.Windows.Forms.Button()
        Me.BtnCal = New System.Windows.Forms.Button()
        Me.BtnRegist = New System.Windows.Forms.Button()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.LblNo1 = New System.Windows.Forms.Label()
        Me.TxtCount1 = New System.Windows.Forms.TextBox()
        Me.DtpDepositDate = New System.Windows.Forms.DateTimePicker()
        Me.LblRemarks = New System.Windows.Forms.Label()
        Me.TxtRemarks = New System.Windows.Forms.TextBox()
        Me.LblDepositDate = New System.Windows.Forms.Label()
        Me.LblNo2 = New System.Windows.Forms.Label()
        Me.TxtCount2 = New System.Windows.Forms.TextBox()
        Me.DgvCustomer = New System.Windows.Forms.DataGridView()
        Me.支払先 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛残高 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LblBillingInfo = New System.Windows.Forms.Label()
        Me.LblDeposit = New System.Windows.Forms.Label()
        Me.DgvBillingInfo = New System.Windows.Forms.DataGridView()
        Me.InfoNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛情報買掛番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛金額 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛情報支払金額計 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛情報買掛残高 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.支払金額 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LblHistory = New System.Windows.Forms.Label()
        Me.LblMode = New System.Windows.Forms.Label()
        CType(Me.DgvHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvDeposit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvBillingInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnDelete
        '
        Me.BtnDelete.Location = New System.Drawing.Point(302, 203)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(94, 20)
        Me.BtnDelete.TabIndex = 319
        Me.BtnDelete.TabStop = False
        Me.BtnDelete.Text = "行削除"
        Me.BtnDelete.UseVisualStyleBackColor = True
        Me.BtnDelete.Visible = False
        '
        'DgvHistory
        '
        Me.DgvHistory.AllowUserToAddRows = False
        Me.DgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvHistory.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.No, Me.売掛番号, Me.支払済支払先, Me.支払番号, Me.支払日, Me.支払種目, Me.支払済支払金額計, Me.備考})
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
        Me.No.Width = 70
        '
        '売掛番号
        '
        Me.売掛番号.HeaderText = "売掛番号"
        Me.売掛番号.Name = "売掛番号"
        Me.売掛番号.Visible = False
        Me.売掛番号.Width = 150
        '
        '支払済支払先
        '
        Me.支払済支払先.HeaderText = "支払先"
        Me.支払済支払先.Name = "支払済支払先"
        Me.支払済支払先.Width = 200
        '
        '支払番号
        '
        Me.支払番号.HeaderText = "支払番号"
        Me.支払番号.Name = "支払番号"
        Me.支払番号.Width = 150
        '
        '支払日
        '
        Me.支払日.HeaderText = "支払日"
        Me.支払日.Name = "支払日"
        Me.支払日.Width = 150
        '
        '支払種目
        '
        Me.支払種目.HeaderText = "支払種目"
        Me.支払種目.Name = "支払種目"
        Me.支払種目.Width = 150
        '
        '支払済支払金額計
        '
        Me.支払済支払金額計.HeaderText = "支払金額計"
        Me.支払済支払金額計.Name = "支払済支払金額計"
        Me.支払済支払金額計.Width = 200
        '
        '備考
        '
        Me.備考.HeaderText = "備考"
        Me.備考.Name = "備考"
        Me.備考.Width = 200
        '
        'DgvDeposit
        '
        Me.DgvDeposit.AllowUserToAddRows = False
        Me.DgvDeposit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvDeposit.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.行番号, Me.入力支払金額})
        Me.DgvDeposit.Location = New System.Drawing.Point(12, 229)
        Me.DgvDeposit.Name = "DgvDeposit"
        Me.DgvDeposit.RowHeadersVisible = False
        Me.DgvDeposit.RowTemplate.Height = 21
        Me.DgvDeposit.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvDeposit.Size = New System.Drawing.Size(1327, 100)
        Me.DgvDeposit.TabIndex = 5
        '
        '行番号
        '
        Me.行番号.HeaderText = "行番号"
        Me.行番号.Name = "行番号"
        Me.行番号.Width = 70
        '
        '入力支払金額
        '
        Me.入力支払金額.HeaderText = "支払金額"
        Me.入力支払金額.Name = "入力支払金額"
        '
        'LblNo3
        '
        Me.LblNo3.BackColor = System.Drawing.Color.Transparent
        Me.LblNo3.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblNo3.Location = New System.Drawing.Point(1317, 335)
        Me.LblNo3.Name = "LblNo3"
        Me.LblNo3.Size = New System.Drawing.Size(22, 22)
        Me.LblNo3.TabIndex = 318
        Me.LblNo3.Text = "件"
        Me.LblNo3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtCount3
        '
        Me.TxtCount3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtCount3.Enabled = False
        Me.TxtCount3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCount3.Location = New System.Drawing.Point(1273, 335)
        Me.TxtCount3.Name = "TxtCount3"
        Me.TxtCount3.Size = New System.Drawing.Size(38, 22)
        Me.TxtCount3.TabIndex = 317
        Me.TxtCount3.TabStop = False
        '
        'BtnAdd
        '
        Me.BtnAdd.Location = New System.Drawing.Point(102, 203)
        Me.BtnAdd.Name = "BtnAdd"
        Me.BtnAdd.Size = New System.Drawing.Size(94, 20)
        Me.BtnAdd.TabIndex = 3
        Me.BtnAdd.Text = "行追加"
        Me.BtnAdd.UseVisualStyleBackColor = True
        '
        'BtnCal
        '
        Me.BtnCal.Location = New System.Drawing.Point(202, 203)
        Me.BtnCal.Name = "BtnCal"
        Me.BtnCal.Size = New System.Drawing.Size(94, 20)
        Me.BtnCal.TabIndex = 4
        Me.BtnCal.Text = "自動振分"
        Me.BtnCal.UseVisualStyleBackColor = True
        '
        'BtnRegist
        '
        Me.BtnRegist.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnRegist.Location = New System.Drawing.Point(1004, 509)
        Me.BtnRegist.Name = "BtnRegist"
        Me.BtnRegist.Size = New System.Drawing.Size(165, 40)
        Me.BtnRegist.TabIndex = 9
        Me.BtnRegist.Text = "登録"
        Me.BtnRegist.UseVisualStyleBackColor = True
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(1175, 509)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 10
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'LblNo1
        '
        Me.LblNo1.BackColor = System.Drawing.Color.Transparent
        Me.LblNo1.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblNo1.Location = New System.Drawing.Point(1317, 65)
        Me.LblNo1.Name = "LblNo1"
        Me.LblNo1.Size = New System.Drawing.Size(22, 22)
        Me.LblNo1.TabIndex = 313
        Me.LblNo1.Text = "件"
        Me.LblNo1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtCount1
        '
        Me.TxtCount1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtCount1.Enabled = False
        Me.TxtCount1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCount1.Location = New System.Drawing.Point(1273, 65)
        Me.TxtCount1.Name = "TxtCount1"
        Me.TxtCount1.Size = New System.Drawing.Size(38, 22)
        Me.TxtCount1.TabIndex = 312
        Me.TxtCount1.TabStop = False
        '
        'DtpDepositDate
        '
        Me.DtpDepositDate.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpDepositDate.CustomFormat = "yyyy/MM/dd"
        Me.DtpDepositDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpDepositDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpDepositDate.Location = New System.Drawing.Point(292, 335)
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
        Me.LblRemarks.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblRemarks.Location = New System.Drawing.Point(446, 335)
        Me.LblRemarks.Name = "LblRemarks"
        Me.LblRemarks.Size = New System.Drawing.Size(170, 22)
        Me.LblRemarks.TabIndex = 310
        Me.LblRemarks.Text = "備考"
        Me.LblRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtRemarks
        '
        Me.TxtRemarks.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtRemarks.Location = New System.Drawing.Point(622, 335)
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(644, 22)
        Me.TxtRemarks.TabIndex = 7
        '
        'LblDepositDate
        '
        Me.LblDepositDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblDepositDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblDepositDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
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
        Me.LblNo2.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblNo2.Location = New System.Drawing.Point(1317, 198)
        Me.LblNo2.Name = "LblNo2"
        Me.LblNo2.Size = New System.Drawing.Size(22, 22)
        Me.LblNo2.TabIndex = 307
        Me.LblNo2.Text = "件"
        Me.LblNo2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtCount2
        '
        Me.TxtCount2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtCount2.Enabled = False
        Me.TxtCount2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCount2.Location = New System.Drawing.Point(1273, 198)
        Me.TxtCount2.Name = "TxtCount2"
        Me.TxtCount2.Size = New System.Drawing.Size(38, 22)
        Me.TxtCount2.TabIndex = 306
        Me.TxtCount2.TabStop = False
        '
        'DgvCustomer
        '
        Me.DgvCustomer.AllowUserToAddRows = False
        Me.DgvCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvCustomer.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.支払先, Me.買掛残高})
        Me.DgvCustomer.Location = New System.Drawing.Point(13, 19)
        Me.DgvCustomer.Name = "DgvCustomer"
        Me.DgvCustomer.RowHeadersVisible = False
        Me.DgvCustomer.RowTemplate.Height = 21
        Me.DgvCustomer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvCustomer.Size = New System.Drawing.Size(504, 40)
        Me.DgvCustomer.TabIndex = 1
        '
        '支払先
        '
        Me.支払先.HeaderText = "支払先"
        Me.支払先.Name = "支払先"
        Me.支払先.Width = 300
        '
        '買掛残高
        '
        Me.買掛残高.HeaderText = "買掛残高"
        Me.買掛残高.Name = "買掛残高"
        Me.買掛残高.Width = 200
        '
        'LblBillingInfo
        '
        Me.LblBillingInfo.AutoSize = True
        Me.LblBillingInfo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblBillingInfo.Location = New System.Drawing.Point(14, 339)
        Me.LblBillingInfo.Name = "LblBillingInfo"
        Me.LblBillingInfo.Size = New System.Drawing.Size(82, 15)
        Me.LblBillingInfo.TabIndex = 305
        Me.LblBillingInfo.Text = "■売掛情報"
        '
        'LblDeposit
        '
        Me.LblDeposit.AutoSize = True
        Me.LblDeposit.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblDeposit.Location = New System.Drawing.Point(14, 203)
        Me.LblDeposit.Name = "LblDeposit"
        Me.LblDeposit.Size = New System.Drawing.Size(82, 15)
        Me.LblDeposit.TabIndex = 303
        Me.LblDeposit.Text = "■支払入力"
        '
        'DgvBillingInfo
        '
        Me.DgvBillingInfo.AllowUserToAddRows = False
        Me.DgvBillingInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvBillingInfo.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.InfoNo, Me.買掛情報買掛番号, Me.買掛日, Me.買掛金額, Me.買掛情報支払金額計, Me.買掛情報買掛残高, Me.支払金額})
        Me.DgvBillingInfo.Location = New System.Drawing.Point(13, 363)
        Me.DgvBillingInfo.Name = "DgvBillingInfo"
        Me.DgvBillingInfo.RowHeadersVisible = False
        Me.DgvBillingInfo.RowTemplate.Height = 21
        Me.DgvBillingInfo.Size = New System.Drawing.Size(1327, 100)
        Me.DgvBillingInfo.TabIndex = 8
        '
        'InfoNo
        '
        Me.InfoNo.HeaderText = "No"
        Me.InfoNo.Name = "InfoNo"
        Me.InfoNo.Width = 70
        '
        '買掛情報買掛番号
        '
        Me.買掛情報買掛番号.HeaderText = "買掛番号"
        Me.買掛情報買掛番号.Name = "買掛情報買掛番号"
        Me.買掛情報買掛番号.Width = 150
        '
        '買掛日
        '
        Me.買掛日.HeaderText = "買掛日"
        Me.買掛日.Name = "買掛日"
        Me.買掛日.Width = 150
        '
        '買掛金額
        '
        Me.買掛金額.HeaderText = "買掛金額"
        Me.買掛金額.Name = "買掛金額"
        Me.買掛金額.Width = 150
        '
        '買掛情報支払金額計
        '
        Me.買掛情報支払金額計.HeaderText = "支払金額計"
        Me.買掛情報支払金額計.Name = "買掛情報支払金額計"
        Me.買掛情報支払金額計.Width = 150
        '
        '買掛情報買掛残高
        '
        Me.買掛情報買掛残高.HeaderText = "買掛残高"
        Me.買掛情報買掛残高.Name = "買掛情報買掛残高"
        Me.買掛情報買掛残高.Width = 150
        '
        '支払金額
        '
        Me.支払金額.HeaderText = "支払金額"
        Me.支払金額.Name = "支払金額"
        Me.支払金額.Width = 150
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
        Me.LblMode.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMode.Location = New System.Drawing.Point(1172, 9)
        Me.LblMode.Name = "LblMode"
        Me.LblMode.Size = New System.Drawing.Size(165, 22)
        Me.LblMode.TabIndex = 321
        Me.LblMode.Text = "支払登録モード"
        Me.LblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Payment
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.LblMode)
        Me.Controls.Add(Me.BtnDelete)
        Me.Controls.Add(Me.DgvHistory)
        Me.Controls.Add(Me.DgvDeposit)
        Me.Controls.Add(Me.LblNo3)
        Me.Controls.Add(Me.TxtCount3)
        Me.Controls.Add(Me.BtnAdd)
        Me.Controls.Add(Me.BtnCal)
        Me.Controls.Add(Me.BtnRegist)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.LblNo1)
        Me.Controls.Add(Me.TxtCount1)
        Me.Controls.Add(Me.DtpDepositDate)
        Me.Controls.Add(Me.LblRemarks)
        Me.Controls.Add(Me.TxtRemarks)
        Me.Controls.Add(Me.LblDepositDate)
        Me.Controls.Add(Me.LblNo2)
        Me.Controls.Add(Me.TxtCount2)
        Me.Controls.Add(Me.DgvCustomer)
        Me.Controls.Add(Me.LblBillingInfo)
        Me.Controls.Add(Me.LblDeposit)
        Me.Controls.Add(Me.DgvBillingInfo)
        Me.Controls.Add(Me.LblHistory)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Payment"
        Me.Text = "Payment"
        CType(Me.DgvHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvDeposit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvBillingInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnDelete As Button
    Friend WithEvents DgvHistory As DataGridView
    Friend WithEvents DgvDeposit As DataGridView
    Friend WithEvents 行番号 As DataGridViewTextBoxColumn
    Friend WithEvents 入力支払金額 As DataGridViewTextBoxColumn
    Friend WithEvents LblNo3 As Label
    Friend WithEvents TxtCount3 As TextBox
    Friend WithEvents BtnAdd As Button
    Friend WithEvents BtnCal As Button
    Friend WithEvents BtnRegist As Button
    Friend WithEvents BtnBack As Button
    Friend WithEvents LblNo1 As Label
    Friend WithEvents TxtCount1 As TextBox
    Friend WithEvents DtpDepositDate As DateTimePicker
    Friend WithEvents LblRemarks As Label
    Friend WithEvents TxtRemarks As TextBox
    Friend WithEvents LblDepositDate As Label
    Friend WithEvents LblNo2 As Label
    Friend WithEvents TxtCount2 As TextBox
    Friend WithEvents DgvCustomer As DataGridView
    Friend WithEvents LblBillingInfo As Label
    Friend WithEvents LblDeposit As Label
    Friend WithEvents DgvBillingInfo As DataGridView
    Friend WithEvents LblHistory As Label
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
    Friend WithEvents InfoNo As DataGridViewTextBoxColumn
    Friend WithEvents 買掛情報買掛番号 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛日 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛金額 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛情報支払金額計 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛情報買掛残高 As DataGridViewTextBoxColumn
    Friend WithEvents 支払金額 As DataGridViewTextBoxColumn
    Friend WithEvents LblMode As Label
End Class
