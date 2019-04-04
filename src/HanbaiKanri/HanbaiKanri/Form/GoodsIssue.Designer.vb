<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class GoodsIssue
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
        Me.TxtSuffixNo = New System.Windows.Forms.TextBox()
        Me.BtnRegist = New System.Windows.Forms.Button()
        Me.DtpGoodsIssueDate = New System.Windows.Forms.DateTimePicker()
        Me.LblRemarks = New System.Windows.Forms.Label()
        Me.TxtRemarks = New System.Windows.Forms.TextBox()
        Me.LblGoodsIssueDate = New System.Windows.Forms.Label()
        Me.LblAdd = New System.Windows.Forms.Label()
        Me.LblCount3 = New System.Windows.Forms.Label()
        Me.TxtCount3 = New System.Windows.Forms.TextBox()
        Me.DgvAdd = New System.Windows.Forms.DataGridView()
        Me.LblHistory = New System.Windows.Forms.Label()
        Me.LblOrder = New System.Windows.Forms.Label()
        Me.LblCount2 = New System.Windows.Forms.Label()
        Me.TxtCount2 = New System.Windows.Forms.TextBox()
        Me.DgvHistory = New System.Windows.Forms.DataGridView()
        Me.LblCount1 = New System.Windows.Forms.Label()
        Me.TxtCount1 = New System.Windows.Forms.TextBox()
        Me.DgvOrder = New System.Windows.Forms.DataGridView()
        Me.LblOrderDate = New System.Windows.Forms.Label()
        Me.TxtOrderDate = New System.Windows.Forms.TextBox()
        Me.TxtCustomerName = New System.Windows.Forms.TextBox()
        Me.LblCustomer = New System.Windows.Forms.Label()
        Me.TxtCustomerCode = New System.Windows.Forms.TextBox()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.LblOrderNo = New System.Windows.Forms.Label()
        Me.TxtOrderNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtnDeliveryNote = New System.Windows.Forms.Button()
        Me.LblMode = New System.Windows.Forms.Label()
        Me.LblCustomerNo = New System.Windows.Forms.Label()
        Me.TxtCustomerPO = New System.Windows.Forms.TextBox()
        CType(Me.DgvAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtSuffixNo
        '
        Me.TxtSuffixNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtSuffixNo.Enabled = False
        Me.TxtSuffixNo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSuffixNo.Location = New System.Drawing.Point(351, 10)
        Me.TxtSuffixNo.Name = "TxtSuffixNo"
        Me.TxtSuffixNo.Size = New System.Drawing.Size(36, 22)
        Me.TxtSuffixNo.TabIndex = 285
        Me.TxtSuffixNo.TabStop = False
        '
        'BtnRegist
        '
        Me.BtnRegist.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnRegist.Location = New System.Drawing.Point(1002, 508)
        Me.BtnRegist.Name = "BtnRegist"
        Me.BtnRegist.Size = New System.Drawing.Size(165, 40)
        Me.BtnRegist.TabIndex = 7
        Me.BtnRegist.Text = "登録"
        Me.BtnRegist.UseVisualStyleBackColor = True
        '
        'DtpGoodsIssueDate
        '
        Me.DtpGoodsIssueDate.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpGoodsIssueDate.CustomFormat = ""
        Me.DtpGoodsIssueDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpGoodsIssueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpGoodsIssueDate.Location = New System.Drawing.Point(292, 343)
        Me.DtpGoodsIssueDate.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.DtpGoodsIssueDate.Name = "DtpGoodsIssueDate"
        Me.DtpGoodsIssueDate.Size = New System.Drawing.Size(148, 22)
        Me.DtpGoodsIssueDate.TabIndex = 3
        Me.DtpGoodsIssueDate.TabStop = False
        Me.DtpGoodsIssueDate.Value = New Date(2018, 7, 16, 0, 0, 0, 0)
        '
        'LblRemarks
        '
        Me.LblRemarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblRemarks.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblRemarks.Location = New System.Drawing.Point(446, 343)
        Me.LblRemarks.Name = "LblRemarks"
        Me.LblRemarks.Size = New System.Drawing.Size(170, 22)
        Me.LblRemarks.TabIndex = 282
        Me.LblRemarks.Text = "備考"
        Me.LblRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtRemarks
        '
        Me.TxtRemarks.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtRemarks.Location = New System.Drawing.Point(622, 343)
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(644, 22)
        Me.TxtRemarks.TabIndex = 4
        '
        'LblGoodsIssueDate
        '
        Me.LblGoodsIssueDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblGoodsIssueDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblGoodsIssueDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblGoodsIssueDate.Location = New System.Drawing.Point(188, 343)
        Me.LblGoodsIssueDate.Name = "LblGoodsIssueDate"
        Me.LblGoodsIssueDate.Size = New System.Drawing.Size(98, 22)
        Me.LblGoodsIssueDate.TabIndex = 280
        Me.LblGoodsIssueDate.Text = "出庫日"
        Me.LblGoodsIssueDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblAdd
        '
        Me.LblAdd.BackColor = System.Drawing.Color.Transparent
        Me.LblAdd.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAdd.Location = New System.Drawing.Point(12, 343)
        Me.LblAdd.Name = "LblAdd"
        Me.LblAdd.Size = New System.Drawing.Size(170, 22)
        Me.LblAdd.TabIndex = 279
        Me.LblAdd.Text = "■今回出庫"
        Me.LblAdd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblCount3
        '
        Me.LblCount3.BackColor = System.Drawing.Color.Transparent
        Me.LblCount3.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCount3.Location = New System.Drawing.Point(1316, 343)
        Me.LblCount3.Name = "LblCount3"
        Me.LblCount3.Size = New System.Drawing.Size(22, 22)
        Me.LblCount3.TabIndex = 278
        Me.LblCount3.Text = "件"
        Me.LblCount3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtCount3
        '
        Me.TxtCount3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtCount3.Enabled = False
        Me.TxtCount3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCount3.Location = New System.Drawing.Point(1272, 343)
        Me.TxtCount3.Name = "TxtCount3"
        Me.TxtCount3.Size = New System.Drawing.Size(38, 22)
        Me.TxtCount3.TabIndex = 277
        Me.TxtCount3.TabStop = False
        Me.TxtCount3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DgvAdd
        '
        Me.DgvAdd.AllowUserToAddRows = False
        Me.DgvAdd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvAdd.Location = New System.Drawing.Point(12, 368)
        Me.DgvAdd.Name = "DgvAdd"
        Me.DgvAdd.RowHeadersVisible = False
        Me.DgvAdd.RowTemplate.Height = 21
        Me.DgvAdd.Size = New System.Drawing.Size(1326, 100)
        Me.DgvAdd.TabIndex = 5
        '
        'LblHistory
        '
        Me.LblHistory.BackColor = System.Drawing.Color.Transparent
        Me.LblHistory.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblHistory.Location = New System.Drawing.Point(12, 212)
        Me.LblHistory.Name = "LblHistory"
        Me.LblHistory.Size = New System.Drawing.Size(170, 22)
        Me.LblHistory.TabIndex = 275
        Me.LblHistory.Text = "■出庫済み"
        Me.LblHistory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblOrder
        '
        Me.LblOrder.BackColor = System.Drawing.Color.Transparent
        Me.LblOrder.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblOrder.Location = New System.Drawing.Point(12, 82)
        Me.LblOrder.Name = "LblOrder"
        Me.LblOrder.Size = New System.Drawing.Size(170, 22)
        Me.LblOrder.TabIndex = 274
        Me.LblOrder.Text = "■受注"
        Me.LblOrder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblCount2
        '
        Me.LblCount2.BackColor = System.Drawing.Color.Transparent
        Me.LblCount2.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCount2.Location = New System.Drawing.Point(1316, 212)
        Me.LblCount2.Name = "LblCount2"
        Me.LblCount2.Size = New System.Drawing.Size(22, 22)
        Me.LblCount2.TabIndex = 273
        Me.LblCount2.Text = "件"
        Me.LblCount2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtCount2
        '
        Me.TxtCount2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtCount2.Enabled = False
        Me.TxtCount2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCount2.Location = New System.Drawing.Point(1272, 212)
        Me.TxtCount2.Name = "TxtCount2"
        Me.TxtCount2.Size = New System.Drawing.Size(38, 22)
        Me.TxtCount2.TabIndex = 272
        Me.TxtCount2.TabStop = False
        Me.TxtCount2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DgvHistory
        '
        Me.DgvHistory.AllowUserToAddRows = False
        Me.DgvHistory.AllowUserToDeleteRows = False
        Me.DgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvHistory.Location = New System.Drawing.Point(12, 237)
        Me.DgvHistory.Name = "DgvHistory"
        Me.DgvHistory.ReadOnly = True
        Me.DgvHistory.RowHeadersVisible = False
        Me.DgvHistory.RowTemplate.Height = 21
        Me.DgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvHistory.Size = New System.Drawing.Size(1326, 100)
        Me.DgvHistory.TabIndex = 2
        '
        'LblCount1
        '
        Me.LblCount1.BackColor = System.Drawing.Color.Transparent
        Me.LblCount1.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCount1.Location = New System.Drawing.Point(1316, 82)
        Me.LblCount1.Name = "LblCount1"
        Me.LblCount1.Size = New System.Drawing.Size(22, 22)
        Me.LblCount1.TabIndex = 270
        Me.LblCount1.Text = "件"
        Me.LblCount1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtCount1
        '
        Me.TxtCount1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtCount1.Enabled = False
        Me.TxtCount1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCount1.Location = New System.Drawing.Point(1272, 82)
        Me.TxtCount1.Name = "TxtCount1"
        Me.TxtCount1.Size = New System.Drawing.Size(38, 22)
        Me.TxtCount1.TabIndex = 269
        Me.TxtCount1.TabStop = False
        Me.TxtCount1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DgvOrder
        '
        Me.DgvOrder.AllowUserToAddRows = False
        Me.DgvOrder.AllowUserToDeleteRows = False
        Me.DgvOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvOrder.Location = New System.Drawing.Point(12, 106)
        Me.DgvOrder.Name = "DgvOrder"
        Me.DgvOrder.ReadOnly = True
        Me.DgvOrder.RowHeadersVisible = False
        Me.DgvOrder.RowTemplate.Height = 21
        Me.DgvOrder.Size = New System.Drawing.Size(1326, 100)
        Me.DgvOrder.TabIndex = 1
        '
        'LblOrderDate
        '
        Me.LblOrderDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblOrderDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblOrderDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblOrderDate.Location = New System.Drawing.Point(732, 10)
        Me.LblOrderDate.Name = "LblOrderDate"
        Me.LblOrderDate.Size = New System.Drawing.Size(170, 22)
        Me.LblOrderDate.TabIndex = 267
        Me.LblOrderDate.Text = "受注日"
        Me.LblOrderDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtOrderDate
        '
        Me.TxtOrderDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtOrderDate.Enabled = False
        Me.TxtOrderDate.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtOrderDate.Location = New System.Drawing.Point(908, 10)
        Me.TxtOrderDate.Name = "TxtOrderDate"
        Me.TxtOrderDate.Size = New System.Drawing.Size(157, 22)
        Me.TxtOrderDate.TabIndex = 266
        Me.TxtOrderDate.TabStop = False
        '
        'TxtCustomerName
        '
        Me.TxtCustomerName.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtCustomerName.Enabled = False
        Me.TxtCustomerName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerName.Location = New System.Drawing.Point(351, 38)
        Me.TxtCustomerName.Name = "TxtCustomerName"
        Me.TxtCustomerName.Size = New System.Drawing.Size(333, 22)
        Me.TxtCustomerName.TabIndex = 265
        Me.TxtCustomerName.TabStop = False
        '
        'LblCustomer
        '
        Me.LblCustomer.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblCustomer.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCustomer.Location = New System.Drawing.Point(12, 38)
        Me.LblCustomer.Name = "LblCustomer"
        Me.LblCustomer.Size = New System.Drawing.Size(170, 22)
        Me.LblCustomer.TabIndex = 264
        Me.LblCustomer.Text = "得意先"
        Me.LblCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtCustomerCode
        '
        Me.TxtCustomerCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtCustomerCode.Enabled = False
        Me.TxtCustomerCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerCode.Location = New System.Drawing.Point(188, 38)
        Me.TxtCustomerCode.Name = "TxtCustomerCode"
        Me.TxtCustomerCode.Size = New System.Drawing.Size(157, 22)
        Me.TxtCustomerCode.TabIndex = 263
        Me.TxtCustomerCode.TabStop = False
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(1173, 509)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 8
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'LblOrderNo
        '
        Me.LblOrderNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblOrderNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblOrderNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblOrderNo.Location = New System.Drawing.Point(12, 10)
        Me.LblOrderNo.Name = "LblOrderNo"
        Me.LblOrderNo.Size = New System.Drawing.Size(170, 22)
        Me.LblOrderNo.TabIndex = 261
        Me.LblOrderNo.Text = "受注番号"
        Me.LblOrderNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtOrderNo
        '
        Me.TxtOrderNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtOrderNo.Enabled = False
        Me.TxtOrderNo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtOrderNo.Location = New System.Drawing.Point(188, 10)
        Me.TxtOrderNo.Name = "TxtOrderNo"
        Me.TxtOrderNo.Size = New System.Drawing.Size(157, 22)
        Me.TxtOrderNo.TabIndex = 260
        Me.TxtOrderNo.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 12)
        Me.Label1.TabIndex = 259
        '
        'BtnDeliveryNote
        '
        Me.BtnDeliveryNote.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnDeliveryNote.Location = New System.Drawing.Point(831, 508)
        Me.BtnDeliveryNote.Name = "BtnDeliveryNote"
        Me.BtnDeliveryNote.Size = New System.Drawing.Size(165, 40)
        Me.BtnDeliveryNote.TabIndex = 6
        Me.BtnDeliveryNote.Text = "納品書・受領書発行"
        Me.BtnDeliveryNote.UseVisualStyleBackColor = True
        Me.BtnDeliveryNote.Visible = False
        '
        'LblMode
        '
        Me.LblMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMode.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMode.Location = New System.Drawing.Point(1152, 9)
        Me.LblMode.Name = "LblMode"
        Me.LblMode.Size = New System.Drawing.Size(186, 22)
        Me.LblMode.TabIndex = 287
        Me.LblMode.Text = "モード"
        Me.LblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblCustomerNo
        '
        Me.LblCustomerNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblCustomerNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblCustomerNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCustomerNo.Location = New System.Drawing.Point(393, 10)
        Me.LblCustomerNo.Name = "LblCustomerNo"
        Me.LblCustomerNo.Size = New System.Drawing.Size(170, 22)
        Me.LblCustomerNo.TabIndex = 289
        Me.LblCustomerNo.Text = "客先番号"
        Me.LblCustomerNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtCustomerPO
        '
        Me.TxtCustomerPO.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtCustomerPO.Enabled = False
        Me.TxtCustomerPO.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerPO.Location = New System.Drawing.Point(569, 10)
        Me.TxtCustomerPO.Name = "TxtCustomerPO"
        Me.TxtCustomerPO.Size = New System.Drawing.Size(157, 22)
        Me.TxtCustomerPO.TabIndex = 288
        Me.TxtCustomerPO.TabStop = False
        '
        'GoodsIssue
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.LblCustomerNo)
        Me.Controls.Add(Me.TxtCustomerPO)
        Me.Controls.Add(Me.LblMode)
        Me.Controls.Add(Me.BtnDeliveryNote)
        Me.Controls.Add(Me.TxtSuffixNo)
        Me.Controls.Add(Me.BtnRegist)
        Me.Controls.Add(Me.DtpGoodsIssueDate)
        Me.Controls.Add(Me.LblRemarks)
        Me.Controls.Add(Me.TxtRemarks)
        Me.Controls.Add(Me.LblGoodsIssueDate)
        Me.Controls.Add(Me.LblAdd)
        Me.Controls.Add(Me.LblCount3)
        Me.Controls.Add(Me.TxtCount3)
        Me.Controls.Add(Me.DgvAdd)
        Me.Controls.Add(Me.LblHistory)
        Me.Controls.Add(Me.LblOrder)
        Me.Controls.Add(Me.LblCount2)
        Me.Controls.Add(Me.TxtCount2)
        Me.Controls.Add(Me.DgvHistory)
        Me.Controls.Add(Me.LblCount1)
        Me.Controls.Add(Me.TxtCount1)
        Me.Controls.Add(Me.DgvOrder)
        Me.Controls.Add(Me.LblOrderDate)
        Me.Controls.Add(Me.TxtOrderDate)
        Me.Controls.Add(Me.TxtCustomerName)
        Me.Controls.Add(Me.LblCustomer)
        Me.Controls.Add(Me.TxtCustomerCode)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.LblOrderNo)
        Me.Controls.Add(Me.TxtOrderNo)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "GoodsIssue"
        Me.Text = "GoodsIssue"
        CType(Me.DgvAdd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvOrder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtSuffixNo As TextBox
    Friend WithEvents BtnRegist As Button
    Friend WithEvents DtpGoodsIssueDate As DateTimePicker
    Friend WithEvents LblRemarks As Label
    Friend WithEvents TxtRemarks As TextBox
    Friend WithEvents LblGoodsIssueDate As Label
    Friend WithEvents LblAdd As Label
    Friend WithEvents LblCount3 As Label
    Friend WithEvents TxtCount3 As TextBox
    Friend WithEvents DgvAdd As DataGridView
    Friend WithEvents LblHistory As Label
    Friend WithEvents LblOrder As Label
    Friend WithEvents LblCount2 As Label
    Friend WithEvents TxtCount2 As TextBox
    Friend WithEvents DgvHistory As DataGridView
    Friend WithEvents LblCount1 As Label
    Friend WithEvents TxtCount1 As TextBox
    Friend WithEvents DgvOrder As DataGridView
    Friend WithEvents LblOrderDate As Label
    Friend WithEvents TxtOrderDate As TextBox
    Friend WithEvents TxtCustomerName As TextBox
    Friend WithEvents LblCustomer As Label
    Friend WithEvents TxtCustomerCode As TextBox
    Friend WithEvents BtnBack As Button
    Friend WithEvents LblOrderNo As Label
    Friend WithEvents TxtOrderNo As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents BtnDeliveryNote As Button
    Friend WithEvents LblMode As Label
    Friend WithEvents LblCustomerNo As Label
    Friend WithEvents TxtCustomerPO As TextBox
End Class
