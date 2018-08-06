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
        Me.受注金額 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求金額計 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求残高 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtpBillingDate = New System.Windows.Forms.DateTimePicker()
        Me.LblRemarks1 = New System.Windows.Forms.Label()
        Me.TxtRemarks1 = New System.Windows.Forms.TextBox()
        Me.LblBillingDate = New System.Windows.Forms.Label()
        Me.LblNo2 = New System.Windows.Forms.Label()
        Me.TxtCount2 = New System.Windows.Forms.TextBox()
        Me.LblRemarks2 = New System.Windows.Forms.Label()
        Me.TxtRemarks2 = New System.Windows.Forms.TextBox()
        Me.TxtCount3 = New System.Windows.Forms.TextBox()
        Me.LblNo3 = New System.Windows.Forms.Label()
        Me.LblNo1 = New System.Windows.Forms.Label()
        Me.TxtCount1 = New System.Windows.Forms.TextBox()
        Me.BtnRegist = New System.Windows.Forms.Button()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.BtnClone = New System.Windows.Forms.Button()
        Me.BtnDelete = New System.Windows.Forms.Button()
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
        Me.LblAdd.Location = New System.Drawing.Point(13, 465)
        Me.LblAdd.Name = "LblAdd"
        Me.LblAdd.Size = New System.Drawing.Size(82, 15)
        Me.LblAdd.TabIndex = 14
        Me.LblAdd.Text = "■今回請求"
        '
        'LblHistory
        '
        Me.LblHistory.AutoSize = True
        Me.LblHistory.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblHistory.Location = New System.Drawing.Point(14, 311)
        Me.LblHistory.Name = "LblHistory"
        Me.LblHistory.Size = New System.Drawing.Size(80, 15)
        Me.LblHistory.TabIndex = 13
        Me.LblHistory.Text = "■請求済み"
        '
        'LblCymndt
        '
        Me.LblCymndt.AutoSize = True
        Me.LblCymndt.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCymndt.Location = New System.Drawing.Point(14, 156)
        Me.LblCymndt.Name = "LblCymndt"
        Me.LblCymndt.Size = New System.Drawing.Size(82, 15)
        Me.LblCymndt.TabIndex = 12
        Me.LblCymndt.Text = "■受注明細"
        '
        'DgvAdd
        '
        Me.DgvAdd.AllowUserToAddRows = False
        Me.DgvAdd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvAdd.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.AddNo, Me.今回請求先, Me.今回請求金額計, Me.今回備考1, Me.今回備考2})
        Me.DgvAdd.Location = New System.Drawing.Point(12, 489)
        Me.DgvAdd.Name = "DgvAdd"
        Me.DgvAdd.RowHeadersVisible = False
        Me.DgvAdd.RowTemplate.Height = 21
        Me.DgvAdd.Size = New System.Drawing.Size(1327, 40)
        Me.DgvAdd.TabIndex = 11
        '
        'AddNo
        '
        Me.AddNo.HeaderText = "No"
        Me.AddNo.Name = "AddNo"
        Me.AddNo.Width = 70
        '
        '今回請求先
        '
        Me.今回請求先.HeaderText = "請求先"
        Me.今回請求先.Name = "今回請求先"
        Me.今回請求先.Width = 200
        '
        '今回請求金額計
        '
        Me.今回請求金額計.HeaderText = "請求金額計"
        Me.今回請求金額計.Name = "今回請求金額計"
        Me.今回請求金額計.Width = 150
        '
        '今回備考1
        '
        Me.今回備考1.HeaderText = "備考1"
        Me.今回備考1.Name = "今回備考1"
        Me.今回備考1.Width = 300
        '
        '今回備考2
        '
        Me.今回備考2.HeaderText = "備考2"
        Me.今回備考2.Name = "今回備考2"
        Me.今回備考2.Width = 300
        '
        'DgvHistory
        '
        Me.DgvHistory.AllowUserToAddRows = False
        Me.DgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvHistory.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.No, Me.請求番号, Me.請求日, Me.請求区分, Me.請求先, Me.請求金額, Me.備考1, Me.備考2, Me.請求済み受注番号, Me.請求済み受注番号枝番})
        Me.DgvHistory.Location = New System.Drawing.Point(12, 335)
        Me.DgvHistory.Name = "DgvHistory"
        Me.DgvHistory.RowHeadersVisible = False
        Me.DgvHistory.RowTemplate.Height = 21
        Me.DgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvHistory.Size = New System.Drawing.Size(1326, 120)
        Me.DgvHistory.TabIndex = 10
        '
        'No
        '
        Me.No.HeaderText = "No"
        Me.No.Name = "No"
        Me.No.Width = 70
        '
        '請求番号
        '
        Me.請求番号.HeaderText = "請求番号"
        Me.請求番号.Name = "請求番号"
        Me.請求番号.Width = 150
        '
        '請求日
        '
        Me.請求日.HeaderText = "請求日"
        Me.請求日.Name = "請求日"
        Me.請求日.Width = 150
        '
        '請求区分
        '
        Me.請求区分.HeaderText = "請求区分"
        Me.請求区分.Name = "請求区分"
        '
        '請求先
        '
        Me.請求先.HeaderText = "請求先"
        Me.請求先.Name = "請求先"
        Me.請求先.Width = 200
        '
        '請求金額
        '
        Me.請求金額.HeaderText = "請求金額計"
        Me.請求金額.Name = "請求金額"
        Me.請求金額.Width = 150
        '
        '備考1
        '
        Me.備考1.HeaderText = "備考1"
        Me.備考1.Name = "備考1"
        Me.備考1.Width = 200
        '
        '備考2
        '
        Me.備考2.HeaderText = "備考2"
        Me.備考2.Name = "備考2"
        Me.備考2.Width = 200
        '
        '請求済み受注番号
        '
        Me.請求済み受注番号.HeaderText = "請求済み受注番号"
        Me.請求済み受注番号.Name = "請求済み受注番号"
        Me.請求済み受注番号.Visible = False
        '
        '請求済み受注番号枝番
        '
        Me.請求済み受注番号枝番.HeaderText = "請求済み受注番号枝番"
        Me.請求済み受注番号枝番.Name = "請求済み受注番号枝番"
        Me.請求済み受注番号枝番.Visible = False
        '
        'DgvCymndt
        '
        Me.DgvCymndt.AllowUserToAddRows = False
        Me.DgvCymndt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvCymndt.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.明細, Me.メーカー, Me.品名, Me.型式, Me.受注個数, Me.単位, Me.売上数量, Me.売上単価, Me.売上金額})
        Me.DgvCymndt.Location = New System.Drawing.Point(12, 181)
        Me.DgvCymndt.Name = "DgvCymndt"
        Me.DgvCymndt.RowHeadersVisible = False
        Me.DgvCymndt.RowTemplate.Height = 21
        Me.DgvCymndt.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvCymndt.Size = New System.Drawing.Size(1326, 120)
        Me.DgvCymndt.TabIndex = 9
        '
        '明細
        '
        Me.明細.HeaderText = "明細"
        Me.明細.Name = "明細"
        Me.明細.Width = 70
        '
        'メーカー
        '
        Me.メーカー.HeaderText = "メーカー"
        Me.メーカー.Name = "メーカー"
        Me.メーカー.Width = 150
        '
        '品名
        '
        Me.品名.HeaderText = "品名"
        Me.品名.Name = "品名"
        Me.品名.Width = 250
        '
        '型式
        '
        Me.型式.HeaderText = "型式"
        Me.型式.Name = "型式"
        Me.型式.Width = 150
        '
        '受注個数
        '
        Me.受注個数.HeaderText = "受注個数"
        Me.受注個数.Name = "受注個数"
        Me.受注個数.Width = 150
        '
        '単位
        '
        Me.単位.HeaderText = "単位"
        Me.単位.Name = "単位"
        '
        '売上数量
        '
        Me.売上数量.HeaderText = "売上数量"
        Me.売上数量.Name = "売上数量"
        Me.売上数量.Width = 150
        '
        '売上単価
        '
        Me.売上単価.HeaderText = "売上単価"
        Me.売上単価.Name = "売上単価"
        Me.売上単価.Width = 150
        '
        '売上金額
        '
        Me.売上金額.HeaderText = "売上金額"
        Me.売上金額.Name = "売上金額"
        Me.売上金額.Width = 150
        '
        'DgvCymn
        '
        Me.DgvCymn.AllowUserToAddRows = False
        Me.DgvCymn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvCymn.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.受注番号, Me.受注日, Me.得意先, Me.受注金額, Me.請求金額計, Me.請求残高})
        Me.DgvCymn.Location = New System.Drawing.Point(12, 27)
        Me.DgvCymn.Name = "DgvCymn"
        Me.DgvCymn.RowHeadersVisible = False
        Me.DgvCymn.RowTemplate.Height = 21
        Me.DgvCymn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvCymn.Size = New System.Drawing.Size(1053, 120)
        Me.DgvCymn.TabIndex = 8
        '
        '受注番号
        '
        Me.受注番号.HeaderText = "受注番号"
        Me.受注番号.Name = "受注番号"
        Me.受注番号.Width = 150
        '
        '受注日
        '
        Me.受注日.HeaderText = "受注日"
        Me.受注日.Name = "受注日"
        Me.受注日.Width = 150
        '
        '得意先
        '
        Me.得意先.HeaderText = "得意先"
        Me.得意先.Name = "得意先"
        Me.得意先.Width = 300
        '
        '受注金額
        '
        Me.受注金額.HeaderText = "受注金額"
        Me.受注金額.Name = "受注金額"
        Me.受注金額.Width = 150
        '
        '請求金額計
        '
        Me.請求金額計.HeaderText = "請求金額計"
        Me.請求金額計.Name = "請求金額計"
        Me.請求金額計.Width = 150
        '
        '請求残高
        '
        Me.請求残高.HeaderText = "請求残高"
        Me.請求残高.Name = "請求残高"
        Me.請求残高.Width = 150
        '
        'DtpBillingDate
        '
        Me.DtpBillingDate.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpBillingDate.CustomFormat = "yyyy/MM/dd"
        Me.DtpBillingDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpBillingDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpBillingDate.Location = New System.Drawing.Point(291, 461)
        Me.DtpBillingDate.Name = "DtpBillingDate"
        Me.DtpBillingDate.Size = New System.Drawing.Size(148, 22)
        Me.DtpBillingDate.TabIndex = 262
        Me.DtpBillingDate.TabStop = False
        Me.DtpBillingDate.Value = New Date(2018, 7, 25, 13, 29, 25, 0)
        '
        'LblRemarks1
        '
        Me.LblRemarks1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblRemarks1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblRemarks1.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblRemarks1.Location = New System.Drawing.Point(518, 535)
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
        Me.TxtRemarks1.Location = New System.Drawing.Point(694, 535)
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
        Me.LblBillingDate.Location = New System.Drawing.Point(187, 461)
        Me.LblBillingDate.Name = "LblBillingDate"
        Me.LblBillingDate.Size = New System.Drawing.Size(98, 22)
        Me.LblBillingDate.TabIndex = 259
        Me.LblBillingDate.Text = "請求日"
        Me.LblBillingDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblNo2
        '
        Me.LblNo2.BackColor = System.Drawing.Color.Transparent
        Me.LblNo2.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblNo2.Location = New System.Drawing.Point(1316, 307)
        Me.LblNo2.Name = "LblNo2"
        Me.LblNo2.Size = New System.Drawing.Size(22, 22)
        Me.LblNo2.TabIndex = 258
        Me.LblNo2.Text = "件"
        Me.LblNo2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtCount2
        '
        Me.TxtCount2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtCount2.Enabled = False
        Me.TxtCount2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCount2.Location = New System.Drawing.Point(1272, 307)
        Me.TxtCount2.Name = "TxtCount2"
        Me.TxtCount2.Size = New System.Drawing.Size(38, 22)
        Me.TxtCount2.TabIndex = 257
        '
        'LblRemarks2
        '
        Me.LblRemarks2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblRemarks2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblRemarks2.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblRemarks2.Location = New System.Drawing.Point(518, 560)
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
        Me.TxtRemarks2.Location = New System.Drawing.Point(694, 560)
        Me.TxtRemarks2.Name = "TxtRemarks2"
        Me.TxtRemarks2.Size = New System.Drawing.Size(644, 22)
        Me.TxtRemarks2.TabIndex = 264
        Me.TxtRemarks2.Visible = False
        '
        'TxtCount3
        '
        Me.TxtCount3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtCount3.Enabled = False
        Me.TxtCount3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCount3.Location = New System.Drawing.Point(1272, 461)
        Me.TxtCount3.Name = "TxtCount3"
        Me.TxtCount3.Size = New System.Drawing.Size(38, 22)
        Me.TxtCount3.TabIndex = 263
        Me.TxtCount3.Visible = False
        '
        'LblNo3
        '
        Me.LblNo3.BackColor = System.Drawing.Color.Transparent
        Me.LblNo3.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblNo3.Location = New System.Drawing.Point(1316, 461)
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
        Me.LblNo1.Location = New System.Drawing.Point(1316, 153)
        Me.LblNo1.Name = "LblNo1"
        Me.LblNo1.Size = New System.Drawing.Size(22, 22)
        Me.LblNo1.TabIndex = 268
        Me.LblNo1.Text = "件"
        Me.LblNo1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtCount1
        '
        Me.TxtCount1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtCount1.Enabled = False
        Me.TxtCount1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCount1.Location = New System.Drawing.Point(1272, 153)
        Me.TxtCount1.Name = "TxtCount1"
        Me.TxtCount1.Size = New System.Drawing.Size(38, 22)
        Me.TxtCount1.TabIndex = 267
        '
        'BtnRegist
        '
        Me.BtnRegist.Location = New System.Drawing.Point(1002, 677)
        Me.BtnRegist.Name = "BtnRegist"
        Me.BtnRegist.Size = New System.Drawing.Size(165, 40)
        Me.BtnRegist.TabIndex = 270
        Me.BtnRegist.Text = "登録"
        Me.BtnRegist.UseVisualStyleBackColor = True
        '
        'BtnBack
        '
        Me.BtnBack.Location = New System.Drawing.Point(1173, 677)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 269
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'BtnClone
        '
        Me.BtnClone.Location = New System.Drawing.Point(12, 538)
        Me.BtnClone.Name = "BtnClone"
        Me.BtnClone.Size = New System.Drawing.Size(94, 20)
        Me.BtnClone.TabIndex = 271
        Me.BtnClone.Text = "行複写"
        Me.BtnClone.UseVisualStyleBackColor = True
        Me.BtnClone.Visible = False
        '
        'BtnDelete
        '
        Me.BtnDelete.Location = New System.Drawing.Point(112, 538)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(94, 20)
        Me.BtnDelete.TabIndex = 272
        Me.BtnDelete.Text = "行削除"
        Me.BtnDelete.UseVisualStyleBackColor = True
        Me.BtnDelete.Visible = False
        '
        'BillingManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1350, 729)
        Me.Controls.Add(Me.BtnDelete)
        Me.Controls.Add(Me.BtnClone)
        Me.Controls.Add(Me.BtnRegist)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.LblNo1)
        Me.Controls.Add(Me.TxtCount1)
        Me.Controls.Add(Me.LblNo3)
        Me.Controls.Add(Me.LblRemarks2)
        Me.Controls.Add(Me.TxtRemarks2)
        Me.Controls.Add(Me.TxtCount3)
        Me.Controls.Add(Me.DtpBillingDate)
        Me.Controls.Add(Me.LblRemarks1)
        Me.Controls.Add(Me.TxtRemarks1)
        Me.Controls.Add(Me.LblBillingDate)
        Me.Controls.Add(Me.LblNo2)
        Me.Controls.Add(Me.TxtCount2)
        Me.Controls.Add(Me.LblAdd)
        Me.Controls.Add(Me.LblHistory)
        Me.Controls.Add(Me.LblCymndt)
        Me.Controls.Add(Me.DgvAdd)
        Me.Controls.Add(Me.DgvHistory)
        Me.Controls.Add(Me.DgvCymndt)
        Me.Controls.Add(Me.DgvCymn)
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
    Friend WithEvents TxtCount2 As TextBox
    Friend WithEvents LblRemarks2 As Label
    Friend WithEvents TxtRemarks2 As TextBox
    Friend WithEvents TxtCount3 As TextBox
    Friend WithEvents LblNo3 As Label
    Friend WithEvents LblNo1 As Label
    Friend WithEvents TxtCount1 As TextBox
    Friend WithEvents 受注番号 As DataGridViewTextBoxColumn
    Friend WithEvents 受注日 As DataGridViewTextBoxColumn
    Friend WithEvents 得意先 As DataGridViewTextBoxColumn
    Friend WithEvents 受注金額 As DataGridViewTextBoxColumn
    Friend WithEvents 請求金額計 As DataGridViewTextBoxColumn
    Friend WithEvents 請求残高 As DataGridViewTextBoxColumn
    Friend WithEvents BtnRegist As Button
    Friend WithEvents BtnBack As Button
    Friend WithEvents 明細 As DataGridViewTextBoxColumn
    Friend WithEvents メーカー As DataGridViewTextBoxColumn
    Friend WithEvents 品名 As DataGridViewTextBoxColumn
    Friend WithEvents 型式 As DataGridViewTextBoxColumn
    Friend WithEvents 受注個数 As DataGridViewTextBoxColumn
    Friend WithEvents 単位 As DataGridViewTextBoxColumn
    Friend WithEvents 売上数量 As DataGridViewTextBoxColumn
    Friend WithEvents 売上単価 As DataGridViewTextBoxColumn
    Friend WithEvents 売上金額 As DataGridViewTextBoxColumn
    Friend WithEvents BtnClone As Button
    Friend WithEvents BtnDelete As Button
    Friend WithEvents AddNo As DataGridViewTextBoxColumn
    Friend WithEvents 今回請求先 As DataGridViewTextBoxColumn
    Friend WithEvents 今回請求金額計 As DataGridViewTextBoxColumn
    Friend WithEvents 今回備考1 As DataGridViewTextBoxColumn
    Friend WithEvents 今回備考2 As DataGridViewTextBoxColumn
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
End Class
