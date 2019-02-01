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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BtnDelete = New System.Windows.Forms.Button()
        Me.BtnClone = New System.Windows.Forms.Button()
        Me.BtnRegist = New System.Windows.Forms.Button()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.LblNo1 = New System.Windows.Forms.Label()
        Me.TxtCount1 = New System.Windows.Forms.TextBox()
        Me.LblNo3 = New System.Windows.Forms.Label()
        Me.LblRemarks2 = New System.Windows.Forms.Label()
        Me.TxtRemarks2 = New System.Windows.Forms.TextBox()
        Me.TxtCount3 = New System.Windows.Forms.TextBox()
        Me.DtpAPDate = New System.Windows.Forms.DateTimePicker()
        Me.LblRemarks1 = New System.Windows.Forms.Label()
        Me.TxtRemarks1 = New System.Windows.Forms.TextBox()
        Me.LblAccountsPayableDate = New System.Windows.Forms.Label()
        Me.LblNo2 = New System.Windows.Forms.Label()
        Me.TxtCount2 = New System.Windows.Forms.TextBox()
        Me.DgvCymn = New System.Windows.Forms.DataGridView()
        Me.発注番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
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
        CType(Me.DgvCymn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvCymndt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnDelete
        '
        Me.BtnDelete.Location = New System.Drawing.Point(112, 456)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(94, 20)
        Me.BtnDelete.TabIndex = 295
        Me.BtnDelete.Text = "行削除"
        Me.BtnDelete.UseVisualStyleBackColor = True
        Me.BtnDelete.Visible = False
        '
        'BtnClone
        '
        Me.BtnClone.Location = New System.Drawing.Point(12, 456)
        Me.BtnClone.Name = "BtnClone"
        Me.BtnClone.Size = New System.Drawing.Size(94, 20)
        Me.BtnClone.TabIndex = 294
        Me.BtnClone.Text = "行複写"
        Me.BtnClone.UseVisualStyleBackColor = True
        Me.BtnClone.Visible = False
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
        'TxtCount1
        '
        Me.TxtCount1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtCount1.Enabled = False
        Me.TxtCount1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCount1.Location = New System.Drawing.Point(1272, 111)
        Me.TxtCount1.Name = "TxtCount1"
        Me.TxtCount1.Size = New System.Drawing.Size(38, 22)
        Me.TxtCount1.TabIndex = 290
        Me.TxtCount1.TabStop = False
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
        'LblRemarks2
        '
        Me.LblRemarks2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblRemarks2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblRemarks2.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblRemarks2.Location = New System.Drawing.Point(518, 481)
        Me.LblRemarks2.Name = "LblRemarks2"
        Me.LblRemarks2.Size = New System.Drawing.Size(170, 22)
        Me.LblRemarks2.TabIndex = 288
        Me.LblRemarks2.Text = "備考2"
        Me.LblRemarks2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.LblRemarks2.Visible = False
        '
        'TxtRemarks2
        '
        Me.TxtRemarks2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtRemarks2.Location = New System.Drawing.Point(694, 481)
        Me.TxtRemarks2.Name = "TxtRemarks2"
        Me.TxtRemarks2.Size = New System.Drawing.Size(644, 22)
        Me.TxtRemarks2.TabIndex = 7
        Me.TxtRemarks2.Visible = False
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
        Me.DtpAPDate.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpAPDate.CustomFormat = "yyyy/MM/dd"
        Me.DtpAPDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpAPDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpAPDate.Location = New System.Drawing.Point(291, 379)
        Me.DtpAPDate.Name = "DtpAPDate"
        Me.DtpAPDate.Size = New System.Drawing.Size(148, 22)
        Me.DtpAPDate.TabIndex = 4
        Me.DtpAPDate.TabStop = False
        Me.DtpAPDate.Value = New Date(2018, 7, 25, 13, 29, 25, 0)
        '
        'LblRemarks1
        '
        Me.LblRemarks1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblRemarks1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblRemarks1.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblRemarks1.Location = New System.Drawing.Point(518, 453)
        Me.LblRemarks1.Name = "LblRemarks1"
        Me.LblRemarks1.Size = New System.Drawing.Size(170, 22)
        Me.LblRemarks1.TabIndex = 284
        Me.LblRemarks1.Text = "備考1"
        Me.LblRemarks1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.LblRemarks1.Visible = False
        '
        'TxtRemarks1
        '
        Me.TxtRemarks1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtRemarks1.Location = New System.Drawing.Point(694, 453)
        Me.TxtRemarks1.Name = "TxtRemarks1"
        Me.TxtRemarks1.Size = New System.Drawing.Size(644, 22)
        Me.TxtRemarks1.TabIndex = 6
        Me.TxtRemarks1.Visible = False
        '
        'LblAccountsPayableDate
        '
        Me.LblAccountsPayableDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblAccountsPayableDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblAccountsPayableDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAccountsPayableDate.Location = New System.Drawing.Point(187, 379)
        Me.LblAccountsPayableDate.Name = "LblAccountsPayableDate"
        Me.LblAccountsPayableDate.Size = New System.Drawing.Size(98, 22)
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
        'TxtCount2
        '
        Me.TxtCount2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtCount2.Enabled = False
        Me.TxtCount2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCount2.Location = New System.Drawing.Point(1272, 245)
        Me.TxtCount2.Name = "TxtCount2"
        Me.TxtCount2.Size = New System.Drawing.Size(38, 22)
        Me.TxtCount2.TabIndex = 280
        Me.TxtCount2.TabStop = False
        '
        'DgvCymn
        '
        Me.DgvCymn.AllowUserToAddRows = False
        Me.DgvCymn.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvCymn.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DgvCymn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvCymn.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.発注番号, Me.発注日, Me.仕入先, Me.客先番号, Me.発注金額, Me.買掛金額計, Me.買掛残高})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvCymn.DefaultCellStyle = DataGridViewCellStyle5
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
        Me.発注番号.Width = 78
        '
        '発注日
        '
        Me.発注日.HeaderText = "発注日"
        Me.発注日.Name = "発注日"
        Me.発注日.Width = 66
        '
        '仕入先
        '
        Me.仕入先.HeaderText = "仕入先"
        Me.仕入先.Name = "仕入先"
        Me.仕入先.Width = 66
        '
        '客先番号
        '
        Me.客先番号.HeaderText = "客先番号"
        Me.客先番号.Name = "客先番号"
        Me.客先番号.Width = 78
        '
        '発注金額
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.発注金額.DefaultCellStyle = DataGridViewCellStyle2
        Me.発注金額.HeaderText = "発注金額"
        Me.発注金額.Name = "発注金額"
        Me.発注金額.Width = 78
        '
        '買掛金額計
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.買掛金額計.DefaultCellStyle = DataGridViewCellStyle3
        Me.買掛金額計.HeaderText = "買掛金額計"
        Me.買掛金額計.Name = "買掛金額計"
        Me.買掛金額計.Width = 90
        '
        '買掛残高
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.買掛残高.DefaultCellStyle = DataGridViewCellStyle4
        Me.買掛残高.HeaderText = "買掛残高"
        Me.買掛残高.Name = "買掛残高"
        Me.買掛残高.Width = 78
        '
        'LblAdd
        '
        Me.LblAdd.AutoSize = True
        Me.LblAdd.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAdd.Location = New System.Drawing.Point(13, 383)
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
        Me.DgvAdd.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvAdd.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.DgvAdd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvAdd.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.AddNo, Me.今回支払先, Me.今回買掛金額計, Me.今回備考1, Me.今回備考2})
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvAdd.DefaultCellStyle = DataGridViewCellStyle8
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
        Me.AddNo.Width = 44
        '
        '今回支払先
        '
        Me.今回支払先.HeaderText = "支払先"
        Me.今回支払先.Name = "今回支払先"
        Me.今回支払先.Width = 66
        '
        '今回買掛金額計
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.今回買掛金額計.DefaultCellStyle = DataGridViewCellStyle7
        Me.今回買掛金額計.HeaderText = "買掛金額計"
        Me.今回買掛金額計.Name = "今回買掛金額計"
        Me.今回買掛金額計.Width = 90
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
        Me.DgvHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle9.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvHistory.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle9
        Me.DgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvHistory.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.No, Me.買掛番号, Me.買掛日, Me.買掛区分, Me.支払先, Me.買掛金額, Me.備考1, Me.備考2, Me.買掛済み発注番号, Me.買掛済み発注番号枝番})
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle11.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvHistory.DefaultCellStyle = DataGridViewCellStyle11
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
        Me.No.Width = 44
        '
        '買掛番号
        '
        Me.買掛番号.HeaderText = "買掛番号"
        Me.買掛番号.Name = "買掛番号"
        Me.買掛番号.Width = 78
        '
        '買掛日
        '
        Me.買掛日.HeaderText = "買掛日"
        Me.買掛日.Name = "買掛日"
        Me.買掛日.Width = 66
        '
        '買掛区分
        '
        Me.買掛区分.HeaderText = "買掛区分"
        Me.買掛区分.Name = "買掛区分"
        Me.買掛区分.Width = 78
        '
        '支払先
        '
        Me.支払先.HeaderText = "支払先"
        Me.支払先.Name = "支払先"
        Me.支払先.Width = 66
        '
        '買掛金額
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.買掛金額.DefaultCellStyle = DataGridViewCellStyle10
        Me.買掛金額.HeaderText = "買掛金額計"
        Me.買掛金額.Name = "買掛金額"
        Me.買掛金額.Width = 90
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
        '買掛済み発注番号
        '
        Me.買掛済み発注番号.HeaderText = "買掛済み発注番号"
        Me.買掛済み発注番号.Name = "買掛済み発注番号"
        Me.買掛済み発注番号.Visible = False
        '
        '買掛済み発注番号枝番
        '
        Me.買掛済み発注番号枝番.HeaderText = "請求済み発注番号枝番"
        Me.買掛済み発注番号枝番.Name = "買掛済み発注番号枝番"
        Me.買掛済み発注番号枝番.Visible = False
        '
        'DgvCymndt
        '
        Me.DgvCymndt.AllowUserToAddRows = False
        Me.DgvCymndt.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle12.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvCymndt.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle12
        Me.DgvCymndt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvCymndt.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.明細, Me.メーカー, Me.品名, Me.型式, Me.発注個数, Me.単位, Me.仕入数量, Me.仕入単価, Me.仕入金額})
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle17.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvCymndt.DefaultCellStyle = DataGridViewCellStyle17
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
        '発注個数
        '
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.発注個数.DefaultCellStyle = DataGridViewCellStyle13
        Me.発注個数.HeaderText = "発注個数"
        Me.発注個数.Name = "発注個数"
        Me.発注個数.Width = 78
        '
        '単位
        '
        Me.単位.HeaderText = "単位"
        Me.単位.Name = "単位"
        Me.単位.Width = 54
        '
        '仕入数量
        '
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.仕入数量.DefaultCellStyle = DataGridViewCellStyle14
        Me.仕入数量.HeaderText = "仕入数量"
        Me.仕入数量.Name = "仕入数量"
        Me.仕入数量.Width = 78
        '
        '仕入単価
        '
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.仕入単価.DefaultCellStyle = DataGridViewCellStyle15
        Me.仕入単価.HeaderText = "仕入単価"
        Me.仕入単価.Name = "仕入単価"
        Me.仕入単価.Width = 78
        '
        '仕入金額
        '
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.仕入金額.DefaultCellStyle = DataGridViewCellStyle16
        Me.仕入金額.HeaderText = "仕入金額"
        Me.仕入金額.Name = "仕入金額"
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
        'AccountsPayable
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.LblMode)
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
        Me.Controls.Add(Me.DtpAPDate)
        Me.Controls.Add(Me.LblRemarks1)
        Me.Controls.Add(Me.TxtRemarks1)
        Me.Controls.Add(Me.LblAccountsPayableDate)
        Me.Controls.Add(Me.LblNo2)
        Me.Controls.Add(Me.TxtCount2)
        Me.Controls.Add(Me.DgvCymn)
        Me.Controls.Add(Me.LblAdd)
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
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnDelete As Button
    Friend WithEvents BtnClone As Button
    Friend WithEvents BtnRegist As Button
    Friend WithEvents BtnBack As Button
    Friend WithEvents LblNo1 As Label
    Friend WithEvents TxtCount1 As TextBox
    Friend WithEvents LblNo3 As Label
    Friend WithEvents LblRemarks2 As Label
    Friend WithEvents TxtRemarks2 As TextBox
    Friend WithEvents TxtCount3 As TextBox
    Friend WithEvents DtpAPDate As DateTimePicker
    Friend WithEvents LblRemarks1 As Label
    Friend WithEvents TxtRemarks1 As TextBox
    Friend WithEvents LblAccountsPayableDate As Label
    Friend WithEvents LblNo2 As Label
    Friend WithEvents TxtCount2 As TextBox
    Friend WithEvents DgvCymn As DataGridView
    Friend WithEvents LblAdd As Label
    Friend WithEvents LblHistory As Label
    Friend WithEvents LblPurchaseOrder As Label
    Friend WithEvents DgvAdd As DataGridView
    Friend WithEvents DgvHistory As DataGridView
    Friend WithEvents DgvCymndt As DataGridView
    Friend WithEvents LblMode As Label
    Friend WithEvents 発注番号 As DataGridViewTextBoxColumn
    Friend WithEvents 発注日 As DataGridViewTextBoxColumn
    Friend WithEvents 仕入先 As DataGridViewTextBoxColumn
    Friend WithEvents 客先番号 As DataGridViewTextBoxColumn
    Friend WithEvents 発注金額 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛金額計 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛残高 As DataGridViewTextBoxColumn
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
End Class
