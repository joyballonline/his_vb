<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BillingManagement
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.LblAdd = New System.Windows.Forms.Label()
        Me.LblHistory = New System.Windows.Forms.Label()
        Me.LblCymndt = New System.Windows.Forms.Label()
        Me.DgvAdd = New System.Windows.Forms.DataGridView()
        Me.AddNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.今回請求先 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.今回請求金額計 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.今回備考1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.今回備考2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DgvHistory = New System.Windows.Forms.DataGridView()
        Me.No = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求区分 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求先 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求金額 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.備考1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.備考2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求済み受注番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求済み受注番号枝番 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DgvCymndt = New System.Windows.Forms.DataGridView()
        Me.明細 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.メーカー = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.品名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.型式 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.受注個数 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.単位 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.売上数量 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.売上単価 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.売上金額 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DgvCymn = New System.Windows.Forms.DataGridView()
        Me.受注番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.受注日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.得意先 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.客先番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.受注金額 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求金額計 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求残高 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtpBillingDate = New System.Windows.Forms.DateTimePicker()
        Me.LblRemarks1 = New System.Windows.Forms.Label()
        Me.TxtRemarks1 = New System.Windows.Forms.TextBox()
        Me.LblBillingDate = New System.Windows.Forms.Label()
        Me.LblNo2 = New System.Windows.Forms.Label()
        Me.TxtDgvHistoryCount = New System.Windows.Forms.TextBox()
        Me.LblRemarks2 = New System.Windows.Forms.Label()
        Me.TxtRemarks2 = New System.Windows.Forms.TextBox()
        Me.TxtDgvAddCount = New System.Windows.Forms.TextBox()
        Me.LblNo3 = New System.Windows.Forms.Label()
        Me.LblNo1 = New System.Windows.Forms.Label()
        Me.TxtDgvCymndtCount = New System.Windows.Forms.TextBox()
        Me.BtnRegist = New System.Windows.Forms.Button()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.BtnClone = New System.Windows.Forms.Button()
        Me.BtnDelete = New System.Windows.Forms.Button()
        Me.LblMode = New System.Windows.Forms.Label()
        Me.DtpDepositDate = New System.Windows.Forms.DateTimePicker()
        Me.LblDepositDate = New System.Windows.Forms.Label()
        Me.TxtIDRCurrency = New System.Windows.Forms.TextBox()
        Me.LblIDRCurrency = New System.Windows.Forms.Label()
        CType(Me.DgvAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvCymndt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvCymn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LblAdd
        '
        Me.LblAdd.AutoSize = True
        Me.LblAdd.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAdd.Location = New System.Drawing.Point(11, 387)
        Me.LblAdd.Name = "LblAdd"
        Me.LblAdd.Size = New System.Drawing.Size(82, 15)
        Me.LblAdd.TabIndex = 14
        Me.LblAdd.Text = "■今回請求"
        '
        'LblHistory
        '
        Me.LblHistory.AutoSize = True
        Me.LblHistory.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblHistory.Location = New System.Drawing.Point(12, 253)
        Me.LblHistory.Name = "LblHistory"
        Me.LblHistory.Size = New System.Drawing.Size(80, 15)
        Me.LblHistory.TabIndex = 13
        Me.LblHistory.Text = "■請求済み"
        '
        'LblCymndt
        '
        Me.LblCymndt.AutoSize = True
        Me.LblCymndt.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCymndt.Location = New System.Drawing.Point(12, 118)
        Me.LblCymndt.Name = "LblCymndt"
        Me.LblCymndt.Size = New System.Drawing.Size(82, 15)
        Me.LblCymndt.TabIndex = 12
        Me.LblCymndt.Text = "■受注明細"
        '
        'DgvAdd
        '
        Me.DgvAdd.AllowUserToAddRows = False
        Me.DgvAdd.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvAdd.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DgvAdd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvAdd.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.AddNo, Me.今回請求先, Me.今回請求金額計, Me.今回備考1, Me.今回備考2})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvAdd.DefaultCellStyle = DataGridViewCellStyle3
        Me.DgvAdd.Location = New System.Drawing.Point(10, 411)
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
        Me.AddNo.Width = 44
        '
        '今回請求先
        '
        Me.今回請求先.HeaderText = "請求先"
        Me.今回請求先.Name = "今回請求先"
        Me.今回請求先.Width = 66
        '
        '今回請求金額計
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.今回請求金額計.DefaultCellStyle = DataGridViewCellStyle2
        Me.今回請求金額計.HeaderText = "請求金額計"
        Me.今回請求金額計.Name = "今回請求金額計"
        Me.今回請求金額計.Width = 90
        '
        '今回備考1
        '
        Me.今回備考1.HeaderText = "備考1"
        Me.今回備考1.Name = "今回備考1"
        Me.今回備考1.Width = 60
        '
        '今回備考2
        '
        Me.今回備考2.HeaderText = "備考2"
        Me.今回備考2.Name = "今回備考2"
        Me.今回備考2.Width = 60
        '
        'DgvHistory
        '
        Me.DgvHistory.AllowUserToAddRows = False
        Me.DgvHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvHistory.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.DgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvHistory.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.No, Me.請求番号, Me.請求日, Me.請求区分, Me.請求先, Me.請求金額, Me.備考1, Me.備考2, Me.請求済み受注番号, Me.請求済み受注番号枝番})
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvHistory.DefaultCellStyle = DataGridViewCellStyle6
        Me.DgvHistory.Location = New System.Drawing.Point(10, 277)
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
        Me.No.Width = 44
        '
        '請求番号
        '
        Me.請求番号.HeaderText = "請求番号"
        Me.請求番号.Name = "請求番号"
        Me.請求番号.Width = 78
        '
        '請求日
        '
        Me.請求日.HeaderText = "請求日"
        Me.請求日.Name = "請求日"
        Me.請求日.Width = 66
        '
        '請求区分
        '
        Me.請求区分.HeaderText = "請求区分"
        Me.請求区分.Name = "請求区分"
        Me.請求区分.Width = 78
        '
        '請求先
        '
        Me.請求先.HeaderText = "請求先"
        Me.請求先.Name = "請求先"
        Me.請求先.Width = 66
        '
        '請求金額
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.請求金額.DefaultCellStyle = DataGridViewCellStyle5
        Me.請求金額.HeaderText = "請求金額計"
        Me.請求金額.Name = "請求金額"
        Me.請求金額.Width = 90
        '
        '備考1
        '
        Me.備考1.HeaderText = "備考1"
        Me.備考1.Name = "備考1"
        Me.備考1.Width = 60
        '
        '備考2
        '
        Me.備考2.HeaderText = "備考2"
        Me.備考2.Name = "備考2"
        Me.備考2.Width = 60
        '
        '請求済み受注番号
        '
        Me.請求済み受注番号.HeaderText = "請求済み受注番号"
        Me.請求済み受注番号.Name = "請求済み受注番号"
        Me.請求済み受注番号.Visible = False
        Me.請求済み受注番号.Width = 150
        '
        '請求済み受注番号枝番
        '
        Me.請求済み受注番号枝番.HeaderText = "請求済み受注番号枝番"
        Me.請求済み受注番号枝番.Name = "請求済み受注番号枝番"
        Me.請求済み受注番号枝番.Visible = False
        Me.請求済み受注番号枝番.Width = 180
        '
        'DgvCymndt
        '
        Me.DgvCymndt.AllowUserToAddRows = False
        Me.DgvCymndt.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvCymndt.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.DgvCymndt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvCymndt.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.明細, Me.メーカー, Me.品名, Me.型式, Me.受注個数, Me.単位, Me.売上数量, Me.売上単価, Me.売上金額})
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle12.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvCymndt.DefaultCellStyle = DataGridViewCellStyle12
        Me.DgvCymndt.Location = New System.Drawing.Point(10, 143)
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
        Me.明細.Width = 54
        '
        'メーカー
        '
        Me.メーカー.HeaderText = "メーカー"
        Me.メーカー.Name = "メーカー"
        Me.メーカー.Width = 67
        '
        '品名
        '
        Me.品名.HeaderText = "品名"
        Me.品名.Name = "品名"
        Me.品名.Width = 54
        '
        '型式
        '
        Me.型式.HeaderText = "型式"
        Me.型式.Name = "型式"
        Me.型式.Width = 54
        '
        '受注個数
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.受注個数.DefaultCellStyle = DataGridViewCellStyle8
        Me.受注個数.HeaderText = "受注個数"
        Me.受注個数.Name = "受注個数"
        Me.受注個数.Width = 78
        '
        '単位
        '
        Me.単位.HeaderText = "単位"
        Me.単位.Name = "単位"
        Me.単位.Width = 54
        '
        '売上数量
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.売上数量.DefaultCellStyle = DataGridViewCellStyle9
        Me.売上数量.HeaderText = "売上数量"
        Me.売上数量.Name = "売上数量"
        Me.売上数量.Width = 78
        '
        '売上単価
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.売上単価.DefaultCellStyle = DataGridViewCellStyle10
        Me.売上単価.HeaderText = "売上単価"
        Me.売上単価.Name = "売上単価"
        Me.売上単価.Width = 78
        '
        '売上金額
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.売上金額.DefaultCellStyle = DataGridViewCellStyle11
        Me.売上金額.HeaderText = "売上金額"
        Me.売上金額.Name = "売上金額"
        Me.売上金額.Width = 78
        '
        'DgvCymn
        '
        Me.DgvCymn.AllowUserToAddRows = False
        Me.DgvCymn.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle13.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvCymn.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle13
        Me.DgvCymn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvCymn.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.受注番号, Me.受注日, Me.得意先, Me.客先番号, Me.受注金額, Me.請求金額計, Me.請求残高})
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle17.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvCymn.DefaultCellStyle = DataGridViewCellStyle17
        Me.DgvCymn.Location = New System.Drawing.Point(12, 9)
        Me.DgvCymn.Name = "DgvCymn"
        Me.DgvCymn.RowHeadersVisible = False
        Me.DgvCymn.RowTemplate.Height = 21
        Me.DgvCymn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvCymn.Size = New System.Drawing.Size(1053, 100)
        Me.DgvCymn.TabIndex = 1
        '
        '受注番号
        '
        Me.受注番号.HeaderText = "受注番号"
        Me.受注番号.Name = "受注番号"
        Me.受注番号.Width = 78
        '
        '受注日
        '
        Me.受注日.HeaderText = "受注日"
        Me.受注日.Name = "受注日"
        Me.受注日.Width = 66
        '
        '得意先
        '
        Me.得意先.HeaderText = "得意先"
        Me.得意先.Name = "得意先"
        Me.得意先.Width = 66
        '
        '客先番号
        '
        Me.客先番号.HeaderText = "客先番号"
        Me.客先番号.Name = "客先番号"
        Me.客先番号.Width = 78
        '
        '受注金額
        '
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.受注金額.DefaultCellStyle = DataGridViewCellStyle14
        Me.受注金額.HeaderText = "受注金額"
        Me.受注金額.Name = "受注金額"
        Me.受注金額.Width = 78
        '
        '請求金額計
        '
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.請求金額計.DefaultCellStyle = DataGridViewCellStyle15
        Me.請求金額計.HeaderText = "請求金額計"
        Me.請求金額計.Name = "請求金額計"
        Me.請求金額計.Width = 90
        '
        '請求残高
        '
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.請求残高.DefaultCellStyle = DataGridViewCellStyle16
        Me.請求残高.HeaderText = "請求残高"
        Me.請求残高.Name = "請求残高"
        Me.請求残高.Width = 78
        '
        'DtpBillingDate
        '
        Me.DtpBillingDate.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpBillingDate.CustomFormat = ""
        Me.DtpBillingDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpBillingDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpBillingDate.Location = New System.Drawing.Point(289, 383)
        Me.DtpBillingDate.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.DtpBillingDate.Name = "DtpBillingDate"
        Me.DtpBillingDate.Size = New System.Drawing.Size(148, 22)
        Me.DtpBillingDate.TabIndex = 4
        Me.DtpBillingDate.TabStop = False
        Me.DtpBillingDate.Value = New Date(2019, 1, 1, 0, 0, 0, 0)
        '
        'LblRemarks1
        '
        Me.LblRemarks1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblRemarks1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblRemarks1.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblRemarks1.Location = New System.Drawing.Point(516, 457)
        Me.LblRemarks1.Name = "LblRemarks1"
        Me.LblRemarks1.Size = New System.Drawing.Size(170, 22)
        Me.LblRemarks1.TabIndex = 261
        Me.LblRemarks1.Text = "備考1"
        Me.LblRemarks1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.LblRemarks1.Visible = False
        '
        'TxtRemarks1
        '
        Me.TxtRemarks1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtRemarks1.Location = New System.Drawing.Point(692, 457)
        Me.TxtRemarks1.Name = "TxtRemarks1"
        Me.TxtRemarks1.Size = New System.Drawing.Size(644, 22)
        Me.TxtRemarks1.TabIndex = 260
        Me.TxtRemarks1.Visible = False
        '
        'LblBillingDate
        '
        Me.LblBillingDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblBillingDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblBillingDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblBillingDate.Location = New System.Drawing.Point(167, 383)
        Me.LblBillingDate.Name = "LblBillingDate"
        Me.LblBillingDate.Size = New System.Drawing.Size(116, 22)
        Me.LblBillingDate.TabIndex = 259
        Me.LblBillingDate.Text = "請求日"
        Me.LblBillingDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblNo2
        '
        Me.LblNo2.BackColor = System.Drawing.Color.Transparent
        Me.LblNo2.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblNo2.Location = New System.Drawing.Point(1314, 249)
        Me.LblNo2.Name = "LblNo2"
        Me.LblNo2.Size = New System.Drawing.Size(22, 22)
        Me.LblNo2.TabIndex = 258
        Me.LblNo2.Text = "件"
        Me.LblNo2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtDgvHistoryCount
        '
        Me.TxtDgvHistoryCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtDgvHistoryCount.Enabled = False
        Me.TxtDgvHistoryCount.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtDgvHistoryCount.Location = New System.Drawing.Point(1270, 249)
        Me.TxtDgvHistoryCount.Name = "TxtDgvHistoryCount"
        Me.TxtDgvHistoryCount.Size = New System.Drawing.Size(38, 22)
        Me.TxtDgvHistoryCount.TabIndex = 257
        Me.TxtDgvHistoryCount.TabStop = False
        Me.TxtDgvHistoryCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LblRemarks2
        '
        Me.LblRemarks2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblRemarks2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblRemarks2.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblRemarks2.Location = New System.Drawing.Point(516, 482)
        Me.LblRemarks2.Name = "LblRemarks2"
        Me.LblRemarks2.Size = New System.Drawing.Size(170, 22)
        Me.LblRemarks2.TabIndex = 265
        Me.LblRemarks2.Text = "備考2"
        Me.LblRemarks2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.LblRemarks2.Visible = False
        '
        'TxtRemarks2
        '
        Me.TxtRemarks2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtRemarks2.Location = New System.Drawing.Point(692, 482)
        Me.TxtRemarks2.Name = "TxtRemarks2"
        Me.TxtRemarks2.Size = New System.Drawing.Size(644, 22)
        Me.TxtRemarks2.TabIndex = 264
        Me.TxtRemarks2.Visible = False
        '
        'TxtDgvAddCount
        '
        Me.TxtDgvAddCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtDgvAddCount.Enabled = False
        Me.TxtDgvAddCount.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtDgvAddCount.Location = New System.Drawing.Point(1270, 383)
        Me.TxtDgvAddCount.Name = "TxtDgvAddCount"
        Me.TxtDgvAddCount.Size = New System.Drawing.Size(38, 22)
        Me.TxtDgvAddCount.TabIndex = 263
        Me.TxtDgvAddCount.TabStop = False
        Me.TxtDgvAddCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtDgvAddCount.Visible = False
        '
        'LblNo3
        '
        Me.LblNo3.BackColor = System.Drawing.Color.Transparent
        Me.LblNo3.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblNo3.Location = New System.Drawing.Point(1314, 383)
        Me.LblNo3.Name = "LblNo3"
        Me.LblNo3.Size = New System.Drawing.Size(22, 22)
        Me.LblNo3.TabIndex = 266
        Me.LblNo3.Text = "件"
        Me.LblNo3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.LblNo3.Visible = False
        '
        'LblNo1
        '
        Me.LblNo1.BackColor = System.Drawing.Color.Transparent
        Me.LblNo1.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblNo1.Location = New System.Drawing.Point(1314, 115)
        Me.LblNo1.Name = "LblNo1"
        Me.LblNo1.Size = New System.Drawing.Size(22, 22)
        Me.LblNo1.TabIndex = 268
        Me.LblNo1.Text = "件"
        Me.LblNo1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtDgvCymndtCount
        '
        Me.TxtDgvCymndtCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtDgvCymndtCount.Enabled = False
        Me.TxtDgvCymndtCount.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtDgvCymndtCount.Location = New System.Drawing.Point(1270, 115)
        Me.TxtDgvCymndtCount.Name = "TxtDgvCymndtCount"
        Me.TxtDgvCymndtCount.Size = New System.Drawing.Size(38, 22)
        Me.TxtDgvCymndtCount.TabIndex = 267
        Me.TxtDgvCymndtCount.TabStop = False
        Me.TxtDgvCymndtCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'BtnRegist
        '
        Me.BtnRegist.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnRegist.Location = New System.Drawing.Point(1000, 509)
        Me.BtnRegist.Name = "BtnRegist"
        Me.BtnRegist.Size = New System.Drawing.Size(165, 40)
        Me.BtnRegist.TabIndex = 6
        Me.BtnRegist.Text = "登録"
        Me.BtnRegist.UseVisualStyleBackColor = True
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(1171, 509)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 7
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'BtnClone
        '
        Me.BtnClone.Location = New System.Drawing.Point(10, 460)
        Me.BtnClone.Name = "BtnClone"
        Me.BtnClone.Size = New System.Drawing.Size(94, 20)
        Me.BtnClone.TabIndex = 271
        Me.BtnClone.Text = "行複写"
        Me.BtnClone.UseVisualStyleBackColor = True
        Me.BtnClone.Visible = False
        '
        'BtnDelete
        '
        Me.BtnDelete.Location = New System.Drawing.Point(110, 460)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(94, 20)
        Me.BtnDelete.TabIndex = 272
        Me.BtnDelete.Text = "行削除"
        Me.BtnDelete.UseVisualStyleBackColor = True
        Me.BtnDelete.Visible = False
        '
        'LblMode
        '
        Me.LblMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMode.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMode.Location = New System.Drawing.Point(1115, 9)
        Me.LblMode.Name = "LblMode"
        Me.LblMode.Size = New System.Drawing.Size(225, 22)
        Me.LblMode.TabIndex = 306
        Me.LblMode.Text = "モード"
        Me.LblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DtpDepositDate
        '
        Me.DtpDepositDate.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpDepositDate.CustomFormat = ""
        Me.DtpDepositDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpDepositDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpDepositDate.Location = New System.Drawing.Point(565, 383)
        Me.DtpDepositDate.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.DtpDepositDate.Name = "DtpDepositDate"
        Me.DtpDepositDate.Size = New System.Drawing.Size(148, 22)
        Me.DtpDepositDate.TabIndex = 307
        Me.DtpDepositDate.TabStop = False
        Me.DtpDepositDate.Value = New Date(2019, 1, 1, 0, 0, 0, 0)
        '
        'LblDepositDate
        '
        Me.LblDepositDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblDepositDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblDepositDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblDepositDate.Location = New System.Drawing.Point(443, 383)
        Me.LblDepositDate.Name = "LblDepositDate"
        Me.LblDepositDate.Size = New System.Drawing.Size(116, 22)
        Me.LblDepositDate.TabIndex = 308
        Me.LblDepositDate.Text = "入金予定日"
        Me.LblDepositDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtIDRCurrency
        '
        Me.TxtIDRCurrency.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtIDRCurrency.Enabled = False
        Me.TxtIDRCurrency.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtIDRCurrency.Location = New System.Drawing.Point(1222, 44)
        Me.TxtIDRCurrency.MaxLength = 20
        Me.TxtIDRCurrency.Name = "TxtIDRCurrency"
        Me.TxtIDRCurrency.ReadOnly = True
        Me.TxtIDRCurrency.Size = New System.Drawing.Size(70, 23)
        Me.TxtIDRCurrency.TabIndex = 322
        Me.TxtIDRCurrency.TabStop = False
        Me.TxtIDRCurrency.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LblIDRCurrency
        '
        Me.LblIDRCurrency.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblIDRCurrency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblIDRCurrency.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblIDRCurrency.Location = New System.Drawing.Point(1116, 44)
        Me.LblIDRCurrency.Name = "LblIDRCurrency"
        Me.LblIDRCurrency.Size = New System.Drawing.Size(100, 23)
        Me.LblIDRCurrency.TabIndex = 321
        Me.LblIDRCurrency.Text = "通貨"
        Me.LblIDRCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BillingManagement
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.TxtIDRCurrency)
        Me.Controls.Add(Me.LblIDRCurrency)
        Me.Controls.Add(Me.DtpDepositDate)
        Me.Controls.Add(Me.LblDepositDate)
        Me.Controls.Add(Me.LblMode)
        Me.Controls.Add(Me.BtnDelete)
        Me.Controls.Add(Me.BtnClone)
        Me.Controls.Add(Me.BtnRegist)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.LblNo1)
        Me.Controls.Add(Me.TxtDgvCymndtCount)
        Me.Controls.Add(Me.LblNo3)
        Me.Controls.Add(Me.LblRemarks2)
        Me.Controls.Add(Me.TxtRemarks2)
        Me.Controls.Add(Me.TxtDgvAddCount)
        Me.Controls.Add(Me.DtpBillingDate)
        Me.Controls.Add(Me.LblRemarks1)
        Me.Controls.Add(Me.TxtRemarks1)
        Me.Controls.Add(Me.LblBillingDate)
        Me.Controls.Add(Me.LblNo2)
        Me.Controls.Add(Me.TxtDgvHistoryCount)
        Me.Controls.Add(Me.LblAdd)
        Me.Controls.Add(Me.LblHistory)
        Me.Controls.Add(Me.LblCymndt)
        Me.Controls.Add(Me.DgvAdd)
        Me.Controls.Add(Me.DgvHistory)
        Me.Controls.Add(Me.DgvCymndt)
        Me.Controls.Add(Me.DgvCymn)
        Me.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "BillingManagement"
        Me.Text = "BillingManagement"
        CType(Me.DgvAdd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvCymndt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvCymn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LblAdd As Label
    Friend WithEvents LblHistory As Label
    Friend WithEvents LblCymndt As Label
    Friend WithEvents DgvAdd As DataGridView
    Friend WithEvents DgvHistory As DataGridView
    Friend WithEvents DgvCymndt As DataGridView
    Friend WithEvents DgvCymn As DataGridView
    Friend WithEvents DtpBillingDate As DateTimePicker
    Friend WithEvents LblRemarks1 As Label
    Friend WithEvents TxtRemarks1 As TextBox
    Friend WithEvents LblBillingDate As Label
    Friend WithEvents LblNo2 As Label
    Friend WithEvents TxtDgvHistoryCount As TextBox
    Friend WithEvents LblRemarks2 As Label
    Friend WithEvents TxtRemarks2 As TextBox
    Friend WithEvents TxtDgvAddCount As TextBox
    Friend WithEvents LblNo3 As Label
    Friend WithEvents LblNo1 As Label
    Friend WithEvents TxtDgvCymndtCount As TextBox
    Friend WithEvents BtnRegist As Button
    Friend WithEvents BtnBack As Button
    Friend WithEvents BtnClone As Button
    Friend WithEvents BtnDelete As Button
    Friend WithEvents LblMode As Label
    Friend WithEvents No As DataGridViewTextBoxColumn
    Friend WithEvents 請求番号 As DataGridViewTextBoxColumn
    Friend WithEvents 請求日 As DataGridViewTextBoxColumn
    Friend WithEvents 請求区分 As DataGridViewTextBoxColumn
    Friend WithEvents 請求先 As DataGridViewTextBoxColumn
    Friend WithEvents 請求金額 As DataGridViewTextBoxColumn
    Friend WithEvents 備考1 As DataGridViewTextBoxColumn
    Friend WithEvents 備考2 As DataGridViewTextBoxColumn
    Friend WithEvents 請求済み受注番号 As DataGridViewTextBoxColumn
    Friend WithEvents 請求済み受注番号枝番 As DataGridViewTextBoxColumn
    Friend WithEvents 明細 As DataGridViewTextBoxColumn
    Friend WithEvents メーカー As DataGridViewTextBoxColumn
    Friend WithEvents 品名 As DataGridViewTextBoxColumn
    Friend WithEvents 型式 As DataGridViewTextBoxColumn
    Friend WithEvents 受注個数 As DataGridViewTextBoxColumn
    Friend WithEvents 単位 As DataGridViewTextBoxColumn
    Friend WithEvents 売上数量 As DataGridViewTextBoxColumn
    Friend WithEvents 売上単価 As DataGridViewTextBoxColumn
    Friend WithEvents 売上金額 As DataGridViewTextBoxColumn
    Friend WithEvents 受注番号 As DataGridViewTextBoxColumn
    Friend WithEvents 受注日 As DataGridViewTextBoxColumn
    Friend WithEvents 得意先 As DataGridViewTextBoxColumn
    Friend WithEvents 客先番号 As DataGridViewTextBoxColumn
    Friend WithEvents 受注金額 As DataGridViewTextBoxColumn
    Friend WithEvents 請求金額計 As DataGridViewTextBoxColumn
    Friend WithEvents 請求残高 As DataGridViewTextBoxColumn
    Friend WithEvents AddNo As DataGridViewTextBoxColumn
    Friend WithEvents 今回請求先 As DataGridViewTextBoxColumn
    Friend WithEvents 今回請求金額計 As DataGridViewTextBoxColumn
    Friend WithEvents 今回備考1 As DataGridViewTextBoxColumn
    Friend WithEvents 今回備考2 As DataGridViewTextBoxColumn
    Friend WithEvents DtpDepositDate As DateTimePicker
    Friend WithEvents LblDepositDate As Label
    Friend WithEvents TxtIDRCurrency As TextBox
    Friend WithEvents LblIDRCurrency As Label
End Class
