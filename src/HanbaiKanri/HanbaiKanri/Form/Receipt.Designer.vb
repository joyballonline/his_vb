<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Receipt
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
        Me.DtpReceiptDate = New System.Windows.Forms.DateTimePicker()
        Me.LblRemarks = New System.Windows.Forms.Label()
        Me.TxtRemarks = New System.Windows.Forms.TextBox()
        Me.LblReceiptDate = New System.Windows.Forms.Label()
        Me.LblAdd = New System.Windows.Forms.Label()
        Me.LblCount3 = New System.Windows.Forms.Label()
        Me.TxtCount3 = New System.Windows.Forms.TextBox()
        Me.DgvAdd = New System.Windows.Forms.DataGridView()
        Me.LblHistory = New System.Windows.Forms.Label()
        Me.LblPurchase = New System.Windows.Forms.Label()
        Me.LblCount2 = New System.Windows.Forms.Label()
        Me.TxtCount2 = New System.Windows.Forms.TextBox()
        Me.DgvHistory = New System.Windows.Forms.DataGridView()
        Me.LblCount1 = New System.Windows.Forms.Label()
        Me.TxtCount1 = New System.Windows.Forms.TextBox()
        Me.DgvPurchase = New System.Windows.Forms.DataGridView()
        Me.LblPurchaseDate = New System.Windows.Forms.Label()
        Me.TxtOrdingDate = New System.Windows.Forms.TextBox()
        Me.TxtSupplierName = New System.Windows.Forms.TextBox()
        Me.LblSupplier = New System.Windows.Forms.Label()
        Me.TxtSupplierCode = New System.Windows.Forms.TextBox()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.LblPurchaseNo = New System.Windows.Forms.Label()
        Me.TxtPurchaseNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblMode = New System.Windows.Forms.Label()
        Me.LblCustomerNo = New System.Windows.Forms.Label()
        Me.TxtCustomerPO = New System.Windows.Forms.TextBox()
        Me.LblWarehouse = New System.Windows.Forms.Label()
        Me.CmWarehouse = New System.Windows.Forms.ComboBox()
        CType(Me.DgvAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvPurchase, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.TxtSuffixNo.TabIndex = 258
        Me.TxtSuffixNo.TabStop = False
        '
        'BtnRegist
        '
        Me.BtnRegist.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnRegist.Location = New System.Drawing.Point(1002, 509)
        Me.BtnRegist.Name = "BtnRegist"
        Me.BtnRegist.Size = New System.Drawing.Size(165, 40)
        Me.BtnRegist.TabIndex = 6
        Me.BtnRegist.Text = "登録"
        Me.BtnRegist.UseVisualStyleBackColor = True
        '
        'DtpReceiptDate
        '
        Me.DtpReceiptDate.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpReceiptDate.CustomFormat = ""
        Me.DtpReceiptDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpReceiptDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpReceiptDate.Location = New System.Drawing.Point(344, 343)
        Me.DtpReceiptDate.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.DtpReceiptDate.Name = "DtpReceiptDate"
        Me.DtpReceiptDate.Size = New System.Drawing.Size(148, 22)
        Me.DtpReceiptDate.TabIndex = 3
        Me.DtpReceiptDate.TabStop = False
        Me.DtpReceiptDate.Value = New Date(2019, 1, 1, 0, 0, 0, 0)
        '
        'LblRemarks
        '
        Me.LblRemarks.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblRemarks.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblRemarks.Location = New System.Drawing.Point(12, 474)
        Me.LblRemarks.Name = "LblRemarks"
        Me.LblRemarks.Size = New System.Drawing.Size(170, 22)
        Me.LblRemarks.TabIndex = 255
        Me.LblRemarks.Text = "備考"
        Me.LblRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtRemarks
        '
        Me.TxtRemarks.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtRemarks.Location = New System.Drawing.Point(188, 474)
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(644, 22)
        Me.TxtRemarks.TabIndex = 4
        '
        'LblReceiptDate
        '
        Me.LblReceiptDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblReceiptDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblReceiptDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblReceiptDate.Location = New System.Drawing.Point(198, 343)
        Me.LblReceiptDate.Name = "LblReceiptDate"
        Me.LblReceiptDate.Size = New System.Drawing.Size(140, 22)
        Me.LblReceiptDate.TabIndex = 253
        Me.LblReceiptDate.Text = "入庫日"
        Me.LblReceiptDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblAdd
        '
        Me.LblAdd.BackColor = System.Drawing.Color.Transparent
        Me.LblAdd.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAdd.Location = New System.Drawing.Point(12, 343)
        Me.LblAdd.Name = "LblAdd"
        Me.LblAdd.Size = New System.Drawing.Size(170, 22)
        Me.LblAdd.TabIndex = 252
        Me.LblAdd.Text = "■今回入庫"
        Me.LblAdd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblCount3
        '
        Me.LblCount3.BackColor = System.Drawing.Color.Transparent
        Me.LblCount3.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCount3.Location = New System.Drawing.Point(1316, 343)
        Me.LblCount3.Name = "LblCount3"
        Me.LblCount3.Size = New System.Drawing.Size(22, 22)
        Me.LblCount3.TabIndex = 251
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
        Me.TxtCount3.TabIndex = 250
        Me.TxtCount3.TabStop = False
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
        Me.LblHistory.TabIndex = 248
        Me.LblHistory.Text = "■入庫済み"
        Me.LblHistory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblPurchase
        '
        Me.LblPurchase.BackColor = System.Drawing.Color.Transparent
        Me.LblPurchase.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPurchase.Location = New System.Drawing.Point(12, 82)
        Me.LblPurchase.Name = "LblPurchase"
        Me.LblPurchase.Size = New System.Drawing.Size(170, 22)
        Me.LblPurchase.TabIndex = 247
        Me.LblPurchase.Text = "■発注"
        Me.LblPurchase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblCount2
        '
        Me.LblCount2.BackColor = System.Drawing.Color.Transparent
        Me.LblCount2.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCount2.Location = New System.Drawing.Point(1316, 212)
        Me.LblCount2.Name = "LblCount2"
        Me.LblCount2.Size = New System.Drawing.Size(22, 22)
        Me.LblCount2.TabIndex = 246
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
        Me.TxtCount2.TabIndex = 245
        Me.TxtCount2.TabStop = False
        '
        'DgvHistory
        '
        Me.DgvHistory.AllowUserToAddRows = False
        Me.DgvHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvHistory.Location = New System.Drawing.Point(12, 237)
        Me.DgvHistory.Name = "DgvHistory"
        Me.DgvHistory.ReadOnly = True
        Me.DgvHistory.RowHeadersVisible = False
        Me.DgvHistory.RowTemplate.Height = 21
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
        Me.LblCount1.TabIndex = 243
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
        Me.TxtCount1.TabIndex = 242
        Me.TxtCount1.TabStop = False
        '
        'DgvPurchase
        '
        Me.DgvPurchase.AllowUserToAddRows = False
        Me.DgvPurchase.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DgvPurchase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvPurchase.Location = New System.Drawing.Point(12, 106)
        Me.DgvPurchase.Name = "DgvPurchase"
        Me.DgvPurchase.ReadOnly = True
        Me.DgvPurchase.RowHeadersVisible = False
        Me.DgvPurchase.RowTemplate.Height = 21
        Me.DgvPurchase.Size = New System.Drawing.Size(1326, 100)
        Me.DgvPurchase.TabIndex = 1
        '
        'LblPurchaseDate
        '
        Me.LblPurchaseDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblPurchaseDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblPurchaseDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPurchaseDate.Location = New System.Drawing.Point(732, 10)
        Me.LblPurchaseDate.Name = "LblPurchaseDate"
        Me.LblPurchaseDate.Size = New System.Drawing.Size(170, 22)
        Me.LblPurchaseDate.TabIndex = 240
        Me.LblPurchaseDate.Text = "発注日"
        Me.LblPurchaseDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtOrdingDate
        '
        Me.TxtOrdingDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtOrdingDate.Enabled = False
        Me.TxtOrdingDate.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtOrdingDate.Location = New System.Drawing.Point(908, 10)
        Me.TxtOrdingDate.Name = "TxtOrdingDate"
        Me.TxtOrdingDate.Size = New System.Drawing.Size(157, 22)
        Me.TxtOrdingDate.TabIndex = 239
        Me.TxtOrdingDate.TabStop = False
        '
        'TxtSupplierName
        '
        Me.TxtSupplierName.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtSupplierName.Enabled = False
        Me.TxtSupplierName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSupplierName.Location = New System.Drawing.Point(351, 38)
        Me.TxtSupplierName.Name = "TxtSupplierName"
        Me.TxtSupplierName.Size = New System.Drawing.Size(333, 22)
        Me.TxtSupplierName.TabIndex = 238
        Me.TxtSupplierName.TabStop = False
        '
        'LblSupplier
        '
        Me.LblSupplier.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblSupplier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblSupplier.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSupplier.Location = New System.Drawing.Point(12, 38)
        Me.LblSupplier.Name = "LblSupplier"
        Me.LblSupplier.Size = New System.Drawing.Size(170, 22)
        Me.LblSupplier.TabIndex = 237
        Me.LblSupplier.Text = "仕入先"
        Me.LblSupplier.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtSupplierCode
        '
        Me.TxtSupplierCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtSupplierCode.Enabled = False
        Me.TxtSupplierCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSupplierCode.Location = New System.Drawing.Point(188, 38)
        Me.TxtSupplierCode.Name = "TxtSupplierCode"
        Me.TxtSupplierCode.Size = New System.Drawing.Size(157, 22)
        Me.TxtSupplierCode.TabIndex = 236
        Me.TxtSupplierCode.TabStop = False
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(1173, 509)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 7
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'LblPurchaseNo
        '
        Me.LblPurchaseNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblPurchaseNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblPurchaseNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPurchaseNo.Location = New System.Drawing.Point(12, 10)
        Me.LblPurchaseNo.Name = "LblPurchaseNo"
        Me.LblPurchaseNo.Size = New System.Drawing.Size(170, 22)
        Me.LblPurchaseNo.TabIndex = 234
        Me.LblPurchaseNo.Text = "発注番号"
        Me.LblPurchaseNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtPurchaseNo
        '
        Me.TxtPurchaseNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtPurchaseNo.Enabled = False
        Me.TxtPurchaseNo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPurchaseNo.Location = New System.Drawing.Point(188, 10)
        Me.TxtPurchaseNo.Name = "TxtPurchaseNo"
        Me.TxtPurchaseNo.Size = New System.Drawing.Size(157, 22)
        Me.TxtPurchaseNo.TabIndex = 233
        Me.TxtPurchaseNo.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 12)
        Me.Label1.TabIndex = 232
        '
        'LblMode
        '
        Me.LblMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMode.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMode.Location = New System.Drawing.Point(1139, 9)
        Me.LblMode.Name = "LblMode"
        Me.LblMode.Size = New System.Drawing.Size(199, 22)
        Me.LblMode.TabIndex = 305
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
        Me.LblCustomerNo.TabIndex = 307
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
        Me.TxtCustomerPO.TabIndex = 306
        Me.TxtCustomerPO.TabStop = False
        '
        'LblWarehouse
        '
        Me.LblWarehouse.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblWarehouse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblWarehouse.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblWarehouse.Location = New System.Drawing.Point(498, 343)
        Me.LblWarehouse.Name = "LblWarehouse"
        Me.LblWarehouse.Size = New System.Drawing.Size(140, 22)
        Me.LblWarehouse.TabIndex = 309
        Me.LblWarehouse.Text = "倉庫"
        Me.LblWarehouse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CmWarehouse
        '
        Me.CmWarehouse.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmWarehouse.FormattingEnabled = True
        Me.CmWarehouse.Items.AddRange(New Object() {"RegularShip", "DHL", "Air", "Ship"})
        Me.CmWarehouse.Location = New System.Drawing.Point(644, 342)
        Me.CmWarehouse.Name = "CmWarehouse"
        Me.CmWarehouse.Size = New System.Drawing.Size(200, 23)
        Me.CmWarehouse.TabIndex = 315
        '
        'Receipt
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.CmWarehouse)
        Me.Controls.Add(Me.LblWarehouse)
        Me.Controls.Add(Me.LblCustomerNo)
        Me.Controls.Add(Me.TxtCustomerPO)
        Me.Controls.Add(Me.LblMode)
        Me.Controls.Add(Me.TxtSuffixNo)
        Me.Controls.Add(Me.BtnRegist)
        Me.Controls.Add(Me.DtpReceiptDate)
        Me.Controls.Add(Me.LblRemarks)
        Me.Controls.Add(Me.TxtRemarks)
        Me.Controls.Add(Me.LblReceiptDate)
        Me.Controls.Add(Me.LblAdd)
        Me.Controls.Add(Me.LblCount3)
        Me.Controls.Add(Me.TxtCount3)
        Me.Controls.Add(Me.DgvAdd)
        Me.Controls.Add(Me.LblHistory)
        Me.Controls.Add(Me.LblPurchase)
        Me.Controls.Add(Me.LblCount2)
        Me.Controls.Add(Me.TxtCount2)
        Me.Controls.Add(Me.DgvHistory)
        Me.Controls.Add(Me.LblCount1)
        Me.Controls.Add(Me.TxtCount1)
        Me.Controls.Add(Me.DgvPurchase)
        Me.Controls.Add(Me.LblPurchaseDate)
        Me.Controls.Add(Me.TxtOrdingDate)
        Me.Controls.Add(Me.TxtSupplierName)
        Me.Controls.Add(Me.LblSupplier)
        Me.Controls.Add(Me.TxtSupplierCode)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.LblPurchaseNo)
        Me.Controls.Add(Me.TxtPurchaseNo)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Receipt"
        Me.Text = "Receipt"
        CType(Me.DgvAdd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvPurchase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtSuffixNo As TextBox
    Friend WithEvents BtnRegist As Button
    Friend WithEvents DtpReceiptDate As DateTimePicker
    Friend WithEvents LblRemarks As Label
    Friend WithEvents TxtRemarks As TextBox
    Friend WithEvents LblReceiptDate As Label
    Friend WithEvents LblAdd As Label
    Friend WithEvents LblCount3 As Label
    Friend WithEvents TxtCount3 As TextBox
    Friend WithEvents DgvAdd As DataGridView
    Friend WithEvents LblHistory As Label
    Friend WithEvents LblPurchase As Label
    Friend WithEvents LblCount2 As Label
    Friend WithEvents TxtCount2 As TextBox
    Friend WithEvents DgvHistory As DataGridView
    Friend WithEvents LblCount1 As Label
    Friend WithEvents TxtCount1 As TextBox
    Friend WithEvents DgvPurchase As DataGridView
    Friend WithEvents LblPurchaseDate As Label
    Friend WithEvents TxtOrdingDate As TextBox
    Friend WithEvents TxtSupplierName As TextBox
    Friend WithEvents LblSupplier As Label
    Friend WithEvents TxtSupplierCode As TextBox
    Friend WithEvents BtnBack As Button
    Friend WithEvents LblPurchaseNo As Label
    Friend WithEvents TxtPurchaseNo As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents LblMode As Label
    Friend WithEvents LblCustomerNo As Label
    Friend WithEvents TxtCustomerPO As TextBox
    Friend WithEvents LblWarehouse As Label
    Friend WithEvents CmWarehouse As ComboBox
End Class
