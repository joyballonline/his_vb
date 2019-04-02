<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AccountsPayable
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
        Dim DataGridViewCellStyle52 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle56 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle53 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle54 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle55 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle57 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle59 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle58 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle60 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle62 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle61 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle63 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle68 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle64 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle65 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle66 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle67 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BtnRegist = New System.Windows.Forms.Button()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.LblNo1 = New System.Windows.Forms.Label()
        Me.TxtHattyudtCount = New System.Windows.Forms.TextBox()
        Me.LblNo3 = New System.Windows.Forms.Label()
        Me.TxtCount3 = New System.Windows.Forms.TextBox()
        Me.DtpAPDate = New System.Windows.Forms.DateTimePicker()
        Me.LblAccountsPayableDate = New System.Windows.Forms.Label()
        Me.LblNo2 = New System.Windows.Forms.Label()
        Me.TxtKikehdCount = New System.Windows.Forms.TextBox()
        Me.DgvCymn = New System.Windows.Forms.DataGridView()
        Me.発注番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.発注番号枝番 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.発注日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入先 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.客先番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.発注金額 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛金額計 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛残高 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LblAdd = New System.Windows.Forms.Label()
        Me.LblHistory = New System.Windows.Forms.Label()
        Me.LblPurchaseOrder = New System.Windows.Forms.Label()
        Me.DgvAdd = New System.Windows.Forms.DataGridView()
        Me.AddNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.今回支払先 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.今回買掛金額計 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.今回備考1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.今回備考2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DgvHistory = New System.Windows.Forms.DataGridView()
        Me.No = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛区分 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.支払先 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛金額 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.備考1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.備考2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛済み発注番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛済み発注番号枝番 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DgvCymndt = New System.Windows.Forms.DataGridView()
        Me.明細 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.メーカー = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.品名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.型式 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.発注個数 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.単位 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入数量 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入単価 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入金額 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LblMode = New System.Windows.Forms.Label()
        Me.DtpPaymentDate = New System.Windows.Forms.DateTimePicker()
        Me.LblPaymentDate = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        CType(Me.DgvCymn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvCymndt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnRegist
        '
        Me.BtnRegist.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnRegist.Location = New System.Drawing.Point(1004, 509)
        Me.BtnRegist.Name = "BtnRegist"
        Me.BtnRegist.Size = New System.Drawing.Size(165, 40)
        Me.BtnRegist.TabIndex = 8
        Me.BtnRegist.Text = "登録"
        Me.BtnRegist.UseVisualStyleBackColor = True
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(1175, 509)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 9
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'LblNo1
        '
        Me.LblNo1.BackColor = System.Drawing.Color.Transparent
        Me.LblNo1.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblNo1.Location = New System.Drawing.Point(1316, 111)
        Me.LblNo1.Name = "LblNo1"
        Me.LblNo1.Size = New System.Drawing.Size(22, 22)
        Me.LblNo1.TabIndex = 291
        Me.LblNo1.Text = "件"
        Me.LblNo1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtHattyudtCount
        '
        Me.TxtHattyudtCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtHattyudtCount.Enabled = False
        Me.TxtHattyudtCount.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtHattyudtCount.Location = New System.Drawing.Point(1272, 111)
        Me.TxtHattyudtCount.Name = "TxtHattyudtCount"
        Me.TxtHattyudtCount.Size = New System.Drawing.Size(38, 22)
        Me.TxtHattyudtCount.TabIndex = 290
        Me.TxtHattyudtCount.TabStop = False
        '
        'LblNo3
        '
        Me.LblNo3.BackColor = System.Drawing.Color.Transparent
        Me.LblNo3.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblNo3.Location = New System.Drawing.Point(1316, 379)
        Me.LblNo3.Name = "LblNo3"
        Me.LblNo3.Size = New System.Drawing.Size(22, 22)
        Me.LblNo3.TabIndex = 289
        Me.LblNo3.Text = "件"
        Me.LblNo3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.LblNo3.Visible = False
        '
        'TxtCount3
        '
        Me.TxtCount3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtCount3.Enabled = False
        Me.TxtCount3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCount3.Location = New System.Drawing.Point(1272, 379)
        Me.TxtCount3.Name = "TxtCount3"
        Me.TxtCount3.Size = New System.Drawing.Size(38, 22)
        Me.TxtCount3.TabIndex = 286
        Me.TxtCount3.TabStop = False
        Me.TxtCount3.Visible = False
        '
        'DtpAPDate
        '
        Me.DtpAPDate.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.DtpAPDate.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpAPDate.CustomFormat = ""
        Me.DtpAPDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpAPDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpAPDate.Location = New System.Drawing.Point(381, 3)
        Me.DtpAPDate.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.DtpAPDate.Name = "DtpAPDate"
        Me.DtpAPDate.Size = New System.Drawing.Size(148, 22)
        Me.DtpAPDate.TabIndex = 4
        Me.DtpAPDate.TabStop = False
        Me.DtpAPDate.Value = New Date(2019, 1, 1, 0, 0, 0, 0)
        '
        'LblAccountsPayableDate
        '
        Me.LblAccountsPayableDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblAccountsPayableDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblAccountsPayableDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblAccountsPayableDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAccountsPayableDate.Location = New System.Drawing.Point(183, 3)
        Me.LblAccountsPayableDate.Name = "LblAccountsPayableDate"
        Me.LblAccountsPayableDate.Size = New System.Drawing.Size(192, 22)
        Me.LblAccountsPayableDate.TabIndex = 282
        Me.LblAccountsPayableDate.Text = "買掛日"
        Me.LblAccountsPayableDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblNo2
        '
        Me.LblNo2.BackColor = System.Drawing.Color.Transparent
        Me.LblNo2.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblNo2.Location = New System.Drawing.Point(1316, 245)
        Me.LblNo2.Name = "LblNo2"
        Me.LblNo2.Size = New System.Drawing.Size(22, 22)
        Me.LblNo2.TabIndex = 281
        Me.LblNo2.Text = "件"
        Me.LblNo2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtKikehdCount
        '
        Me.TxtKikehdCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtKikehdCount.Enabled = False
        Me.TxtKikehdCount.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtKikehdCount.Location = New System.Drawing.Point(1272, 245)
        Me.TxtKikehdCount.Name = "TxtKikehdCount"
        Me.TxtKikehdCount.Size = New System.Drawing.Size(38, 22)
        Me.TxtKikehdCount.TabIndex = 280
        Me.TxtKikehdCount.TabStop = False
        '
        'DgvCymn
        '
        Me.DgvCymn.AllowUserToAddRows = False
        Me.DgvCymn.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        DataGridViewCellStyle52.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle52.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle52.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle52.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle52.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle52.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle52.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvCymn.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle52
        Me.DgvCymn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvCymn.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.発注番号, Me.発注番号枝番, Me.発注日, Me.仕入先, Me.客先番号, Me.発注金額, Me.買掛金額計, Me.買掛残高})
        DataGridViewCellStyle56.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle56.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle56.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle56.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle56.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle56.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle56.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvCymn.DefaultCellStyle = DataGridViewCellStyle56
        Me.DgvCymn.Location = New System.Drawing.Point(12, 9)
        Me.DgvCymn.Name = "DgvCymn"
        Me.DgvCymn.RowHeadersVisible = False
        Me.DgvCymn.RowTemplate.Height = 21
        Me.DgvCymn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvCymn.Size = New System.Drawing.Size(1053, 100)
        Me.DgvCymn.TabIndex = 1
        '
        '発注番号
        '
        Me.発注番号.HeaderText = "発注番号"
        Me.発注番号.Name = "発注番号"
        Me.発注番号.ReadOnly = True
        Me.発注番号.Width = 61
        '
        '発注番号枝番
        '
        Me.発注番号枝番.HeaderText = "発注番号枝番"
        Me.発注番号枝番.MaxInputLength = 2
        Me.発注番号枝番.Name = "発注番号枝番"
        Me.発注番号枝番.ReadOnly = True
        Me.発注番号枝番.Width = 72
        '
        '発注日
        '
        Me.発注日.HeaderText = "発注日"
        Me.発注日.Name = "発注日"
        Me.発注日.ReadOnly = True
        Me.発注日.Width = 61
        '
        '仕入先
        '
        Me.仕入先.HeaderText = "仕入先"
        Me.仕入先.Name = "仕入先"
        Me.仕入先.ReadOnly = True
        Me.仕入先.Width = 61
        '
        '客先番号
        '
        Me.客先番号.HeaderText = "客先番号"
        Me.客先番号.Name = "客先番号"
        Me.客先番号.ReadOnly = True
        Me.客先番号.Width = 61
        '
        '発注金額
        '
        DataGridViewCellStyle53.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.発注金額.DefaultCellStyle = DataGridViewCellStyle53
        Me.発注金額.HeaderText = "発注金額"
        Me.発注金額.Name = "発注金額"
        Me.発注金額.ReadOnly = True
        Me.発注金額.Width = 61
        '
        '買掛金額計
        '
        DataGridViewCellStyle54.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.買掛金額計.DefaultCellStyle = DataGridViewCellStyle54
        Me.買掛金額計.HeaderText = "買掛金額計"
        Me.買掛金額計.Name = "買掛金額計"
        Me.買掛金額計.ReadOnly = True
        Me.買掛金額計.Width = 72
        '
        '買掛残高
        '
        DataGridViewCellStyle55.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.買掛残高.DefaultCellStyle = DataGridViewCellStyle55
        Me.買掛残高.HeaderText = "買掛残高"
        Me.買掛残高.Name = "買掛残高"
        Me.買掛残高.ReadOnly = True
        Me.買掛残高.Width = 61
        '
        'LblAdd
        '
        Me.LblAdd.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblAdd.AutoSize = True
        Me.LblAdd.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAdd.Location = New System.Drawing.Point(3, 6)
        Me.LblAdd.Name = "LblAdd"
        Me.LblAdd.Size = New System.Drawing.Size(82, 15)
        Me.LblAdd.TabIndex = 279
        Me.LblAdd.Text = "■今回買掛"
        '
        'LblHistory
        '
        Me.LblHistory.AutoSize = True
        Me.LblHistory.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblHistory.Location = New System.Drawing.Point(14, 249)
        Me.LblHistory.Name = "LblHistory"
        Me.LblHistory.Size = New System.Drawing.Size(80, 15)
        Me.LblHistory.TabIndex = 278
        Me.LblHistory.Text = "■買掛済み"
        '
        'LblPurchaseOrder
        '
        Me.LblPurchaseOrder.AutoSize = True
        Me.LblPurchaseOrder.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPurchaseOrder.Location = New System.Drawing.Point(14, 114)
        Me.LblPurchaseOrder.Name = "LblPurchaseOrder"
        Me.LblPurchaseOrder.Size = New System.Drawing.Size(82, 15)
        Me.LblPurchaseOrder.TabIndex = 277
        Me.LblPurchaseOrder.Text = "■発注明細"
        '
        'DgvAdd
        '
        Me.DgvAdd.AllowUserToAddRows = False
        Me.DgvAdd.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        DataGridViewCellStyle57.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle57.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle57.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle57.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle57.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle57.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle57.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvAdd.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle57
        Me.DgvAdd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvAdd.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.AddNo, Me.今回支払先, Me.今回買掛金額計, Me.今回備考1, Me.今回備考2})
        DataGridViewCellStyle59.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle59.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle59.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle59.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle59.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle59.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle59.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvAdd.DefaultCellStyle = DataGridViewCellStyle59
        Me.DgvAdd.Location = New System.Drawing.Point(12, 407)
        Me.DgvAdd.Name = "DgvAdd"
        Me.DgvAdd.RowHeadersVisible = False
        Me.DgvAdd.RowTemplate.Height = 21
        Me.DgvAdd.Size = New System.Drawing.Size(1327, 40)
        Me.DgvAdd.TabIndex = 5
        '
        'AddNo
        '
        Me.AddNo.HeaderText = "No"
        Me.AddNo.Name = "AddNo"
        Me.AddNo.ReadOnly = True
        Me.AddNo.Width = 44
        '
        '今回支払先
        '
        Me.今回支払先.HeaderText = "支払先"
        Me.今回支払先.Name = "今回支払先"
        Me.今回支払先.ReadOnly = True
        Me.今回支払先.Width = 66
        '
        '今回買掛金額計
        '
        DataGridViewCellStyle58.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.今回買掛金額計.DefaultCellStyle = DataGridViewCellStyle58
        Me.今回買掛金額計.HeaderText = "買掛金額計"
        Me.今回買掛金額計.MaxInputLength = 14
        Me.今回買掛金額計.Name = "今回買掛金額計"
        Me.今回買掛金額計.Width = 90
        '
        '今回備考1
        '
        Me.今回備考1.HeaderText = "備考1"
        Me.今回備考1.MaxInputLength = 50
        Me.今回備考1.Name = "今回備考1"
        Me.今回備考1.Width = 60
        '
        '今回備考2
        '
        Me.今回備考2.HeaderText = "備考2"
        Me.今回備考2.MaxInputLength = 50
        Me.今回備考2.Name = "今回備考2"
        Me.今回備考2.Width = 60
        '
        'DgvHistory
        '
        Me.DgvHistory.AllowUserToAddRows = False
        Me.DgvHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        DataGridViewCellStyle60.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle60.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle60.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle60.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle60.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle60.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle60.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvHistory.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle60
        Me.DgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvHistory.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.No, Me.買掛番号, Me.買掛日, Me.買掛区分, Me.支払先, Me.買掛金額, Me.備考1, Me.備考2, Me.買掛済み発注番号, Me.買掛済み発注番号枝番})
        DataGridViewCellStyle62.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle62.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle62.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle62.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle62.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle62.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle62.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvHistory.DefaultCellStyle = DataGridViewCellStyle62
        Me.DgvHistory.Location = New System.Drawing.Point(12, 273)
        Me.DgvHistory.Name = "DgvHistory"
        Me.DgvHistory.RowHeadersVisible = False
        Me.DgvHistory.RowTemplate.Height = 21
        Me.DgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvHistory.Size = New System.Drawing.Size(1326, 100)
        Me.DgvHistory.TabIndex = 3
        '
        'No
        '
        Me.No.HeaderText = "No"
        Me.No.Name = "No"
        Me.No.ReadOnly = True
        Me.No.Width = 44
        '
        '買掛番号
        '
        Me.買掛番号.HeaderText = "買掛番号"
        Me.買掛番号.Name = "買掛番号"
        Me.買掛番号.ReadOnly = True
        Me.買掛番号.Width = 78
        '
        '買掛日
        '
        Me.買掛日.HeaderText = "買掛日"
        Me.買掛日.Name = "買掛日"
        Me.買掛日.ReadOnly = True
        Me.買掛日.Width = 66
        '
        '買掛区分
        '
        Me.買掛区分.HeaderText = "買掛区分"
        Me.買掛区分.Name = "買掛区分"
        Me.買掛区分.ReadOnly = True
        Me.買掛区分.Width = 78
        '
        '支払先
        '
        Me.支払先.HeaderText = "支払先"
        Me.支払先.Name = "支払先"
        Me.支払先.ReadOnly = True
        Me.支払先.Width = 66
        '
        '買掛金額
        '
        DataGridViewCellStyle61.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.買掛金額.DefaultCellStyle = DataGridViewCellStyle61
        Me.買掛金額.HeaderText = "買掛金額計"
        Me.買掛金額.Name = "買掛金額"
        Me.買掛金額.ReadOnly = True
        Me.買掛金額.Width = 90
        '
        '備考1
        '
        Me.備考1.HeaderText = "備考1"
        Me.備考1.Name = "備考1"
        Me.備考1.ReadOnly = True
        Me.備考1.Width = 60
        '
        '備考2
        '
        Me.備考2.HeaderText = "備考2"
        Me.備考2.Name = "備考2"
        Me.備考2.ReadOnly = True
        Me.備考2.Width = 60
        '
        '買掛済み発注番号
        '
        Me.買掛済み発注番号.HeaderText = "買掛済み発注番号"
        Me.買掛済み発注番号.Name = "買掛済み発注番号"
        Me.買掛済み発注番号.ReadOnly = True
        Me.買掛済み発注番号.Visible = False
        Me.買掛済み発注番号.Width = 125
        '
        '買掛済み発注番号枝番
        '
        Me.買掛済み発注番号枝番.HeaderText = "請求済み発注番号枝番"
        Me.買掛済み発注番号枝番.Name = "買掛済み発注番号枝番"
        Me.買掛済み発注番号枝番.ReadOnly = True
        Me.買掛済み発注番号枝番.Visible = False
        Me.買掛済み発注番号枝番.Width = 149
        '
        'DgvCymndt
        '
        Me.DgvCymndt.AllowUserToAddRows = False
        Me.DgvCymndt.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        DataGridViewCellStyle63.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle63.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle63.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle63.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle63.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle63.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle63.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvCymndt.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle63
        Me.DgvCymndt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvCymndt.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.明細, Me.メーカー, Me.品名, Me.型式, Me.発注個数, Me.単位, Me.仕入数量, Me.仕入単価, Me.仕入金額})
        DataGridViewCellStyle68.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle68.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle68.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle68.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle68.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle68.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle68.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvCymndt.DefaultCellStyle = DataGridViewCellStyle68
        Me.DgvCymndt.Location = New System.Drawing.Point(12, 139)
        Me.DgvCymndt.Name = "DgvCymndt"
        Me.DgvCymndt.RowHeadersVisible = False
        Me.DgvCymndt.RowTemplate.Height = 21
        Me.DgvCymndt.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvCymndt.Size = New System.Drawing.Size(1326, 100)
        Me.DgvCymndt.TabIndex = 2
        '
        '明細
        '
        Me.明細.HeaderText = "明細"
        Me.明細.Name = "明細"
        Me.明細.ReadOnly = True
        Me.明細.Width = 54
        '
        'メーカー
        '
        Me.メーカー.HeaderText = "メーカー"
        Me.メーカー.Name = "メーカー"
        Me.メーカー.ReadOnly = True
        Me.メーカー.Width = 67
        '
        '品名
        '
        Me.品名.HeaderText = "品名"
        Me.品名.Name = "品名"
        Me.品名.ReadOnly = True
        Me.品名.Width = 54
        '
        '型式
        '
        Me.型式.HeaderText = "型式"
        Me.型式.Name = "型式"
        Me.型式.ReadOnly = True
        Me.型式.Width = 54
        '
        '発注個数
        '
        DataGridViewCellStyle64.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.発注個数.DefaultCellStyle = DataGridViewCellStyle64
        Me.発注個数.HeaderText = "発注個数"
        Me.発注個数.Name = "発注個数"
        Me.発注個数.ReadOnly = True
        Me.発注個数.Width = 78
        '
        '単位
        '
        Me.単位.HeaderText = "単位"
        Me.単位.Name = "単位"
        Me.単位.ReadOnly = True
        Me.単位.Width = 54
        '
        '仕入数量
        '
        DataGridViewCellStyle65.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.仕入数量.DefaultCellStyle = DataGridViewCellStyle65
        Me.仕入数量.HeaderText = "仕入数量"
        Me.仕入数量.Name = "仕入数量"
        Me.仕入数量.ReadOnly = True
        Me.仕入数量.Width = 78
        '
        '仕入単価
        '
        DataGridViewCellStyle66.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.仕入単価.DefaultCellStyle = DataGridViewCellStyle66
        Me.仕入単価.HeaderText = "仕入単価"
        Me.仕入単価.Name = "仕入単価"
        Me.仕入単価.ReadOnly = True
        Me.仕入単価.Width = 78
        '
        '仕入金額
        '
        DataGridViewCellStyle67.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.仕入金額.DefaultCellStyle = DataGridViewCellStyle67
        Me.仕入金額.HeaderText = "仕入金額"
        Me.仕入金額.Name = "仕入金額"
        Me.仕入金額.ReadOnly = True
        Me.仕入金額.Width = 78
        '
        'LblMode
        '
        Me.LblMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMode.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMode.Location = New System.Drawing.Point(1115, 9)
        Me.LblMode.Name = "LblMode"
        Me.LblMode.Size = New System.Drawing.Size(225, 22)
        Me.LblMode.TabIndex = 323
        Me.LblMode.Text = "買掛入力モード"
        Me.LblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DtpPaymentDate
        '
        Me.DtpPaymentDate.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.DtpPaymentDate.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpPaymentDate.CustomFormat = ""
        Me.DtpPaymentDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpPaymentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpPaymentDate.Location = New System.Drawing.Point(745, 3)
        Me.DtpPaymentDate.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.DtpPaymentDate.Name = "DtpPaymentDate"
        Me.DtpPaymentDate.Size = New System.Drawing.Size(148, 22)
        Me.DtpPaymentDate.TabIndex = 324
        Me.DtpPaymentDate.TabStop = False
        Me.DtpPaymentDate.Value = New Date(2019, 1, 1, 0, 0, 0, 0)
        '
        'LblPaymentDate
        '
        Me.LblPaymentDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblPaymentDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblPaymentDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblPaymentDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPaymentDate.Location = New System.Drawing.Point(553, 3)
        Me.LblPaymentDate.Name = "LblPaymentDate"
        Me.LblPaymentDate.Size = New System.Drawing.Size(186, 22)
        Me.LblPaymentDate.TabIndex = 325
        Me.LblPaymentDate.Text = "支払予定日"
        Me.LblPaymentDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel1.ColumnCount = 5
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.86792!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.75472!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.02935!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.12579!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.1174!))
        Me.TableLayoutPanel1.Controls.Add(Me.LblAccountsPayableDate, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DtpPaymentDate, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DtpAPDate, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LblPaymentDate, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LblAdd, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(12, 378)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(954, 28)
        Me.TableLayoutPanel1.TabIndex = 326
        '
        'AccountsPayable
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.LblMode)
        Me.Controls.Add(Me.BtnRegist)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.LblNo1)
        Me.Controls.Add(Me.TxtHattyudtCount)
        Me.Controls.Add(Me.LblNo3)
        Me.Controls.Add(Me.TxtCount3)
        Me.Controls.Add(Me.LblNo2)
        Me.Controls.Add(Me.TxtKikehdCount)
        Me.Controls.Add(Me.DgvCymn)
        Me.Controls.Add(Me.LblHistory)
        Me.Controls.Add(Me.LblPurchaseOrder)
        Me.Controls.Add(Me.DgvAdd)
        Me.Controls.Add(Me.DgvHistory)
        Me.Controls.Add(Me.DgvCymndt)
        Me.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "AccountsPayable"
        Me.Text = "AccountsPayable"
        CType(Me.DgvCymn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvAdd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvCymndt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnRegist As Button
    Friend WithEvents BtnBack As Button
    Friend WithEvents LblNo1 As Label
    Friend WithEvents TxtHattyudtCount As TextBox
    Friend WithEvents LblNo3 As Label
    Friend WithEvents TxtCount3 As TextBox
    Friend WithEvents DtpAPDate As DateTimePicker
    Friend WithEvents LblAccountsPayableDate As Label
    Friend WithEvents LblNo2 As Label
    Friend WithEvents TxtKikehdCount As TextBox
    Friend WithEvents DgvCymn As DataGridView
    Friend WithEvents LblAdd As Label
    Friend WithEvents LblHistory As Label
    Friend WithEvents LblPurchaseOrder As Label
    Friend WithEvents DgvAdd As DataGridView
    Friend WithEvents DgvHistory As DataGridView
    Friend WithEvents DgvCymndt As DataGridView
    Friend WithEvents LblMode As Label
    Friend WithEvents AddNo As DataGridViewTextBoxColumn
    Friend WithEvents 今回支払先 As DataGridViewTextBoxColumn
    Friend WithEvents 今回買掛金額計 As DataGridViewTextBoxColumn
    Friend WithEvents 今回備考1 As DataGridViewTextBoxColumn
    Friend WithEvents 今回備考2 As DataGridViewTextBoxColumn
    Friend WithEvents No As DataGridViewTextBoxColumn
    Friend WithEvents 買掛番号 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛日 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛区分 As DataGridViewTextBoxColumn
    Friend WithEvents 支払先 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛金額 As DataGridViewTextBoxColumn
    Friend WithEvents 備考1 As DataGridViewTextBoxColumn
    Friend WithEvents 備考2 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛済み発注番号 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛済み発注番号枝番 As DataGridViewTextBoxColumn
    Friend WithEvents 明細 As DataGridViewTextBoxColumn
    Friend WithEvents メーカー As DataGridViewTextBoxColumn
    Friend WithEvents 品名 As DataGridViewTextBoxColumn
    Friend WithEvents 型式 As DataGridViewTextBoxColumn
    Friend WithEvents 発注個数 As DataGridViewTextBoxColumn
    Friend WithEvents 単位 As DataGridViewTextBoxColumn
    Friend WithEvents 仕入数量 As DataGridViewTextBoxColumn
    Friend WithEvents 仕入単価 As DataGridViewTextBoxColumn
    Friend WithEvents 仕入金額 As DataGridViewTextBoxColumn
    Friend WithEvents 発注番号 As DataGridViewTextBoxColumn
    Friend WithEvents 発注番号枝番 As DataGridViewTextBoxColumn
    Friend WithEvents 発注日 As DataGridViewTextBoxColumn
    Friend WithEvents 仕入先 As DataGridViewTextBoxColumn
    Friend WithEvents 客先番号 As DataGridViewTextBoxColumn
    Friend WithEvents 発注金額 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛金額計 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛残高 As DataGridViewTextBoxColumn
    Friend WithEvents DtpPaymentDate As DateTimePicker
    Friend WithEvents LblPaymentDate As Label
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
End Class
