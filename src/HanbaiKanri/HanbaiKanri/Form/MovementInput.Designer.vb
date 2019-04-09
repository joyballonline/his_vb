<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MovementInput
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.LblMode = New System.Windows.Forms.Label()
        Me.DgvList = New System.Windows.Forms.DataGridView()
        Me.No = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.メーカー = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.品名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.型式 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.数量 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BtnRegistration = New System.Windows.Forms.Button()
        Me.BtnRowsAdd = New System.Windows.Forms.Button()
        Me.BtnRowsDel = New System.Windows.Forms.Button()
        Me.LblCustomerName = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.CmWarehouseTo = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DtpProcessingDate = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CmInOutKbn = New System.Windows.Forms.ComboBox()
        Me.CmWarehouseFrom = New System.Windows.Forms.ComboBox()
        Me.CmProcessingClassification = New System.Windows.Forms.ComboBox()
        CType(Me.DgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
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
        'DgvList
        '
        Me.DgvList.AllowUserToAddRows = False
        Me.DgvList.AllowUserToDeleteRows = False
        Me.DgvList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.No, Me.メーカー, Me.品名, Me.型式, Me.数量})
        Me.DgvList.Location = New System.Drawing.Point(9, 109)
        Me.DgvList.Name = "DgvList"
        Me.DgvList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DgvList.RowHeadersVisible = False
        Me.DgvList.RowTemplate.Height = 21
        Me.DgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvList.Size = New System.Drawing.Size(1329, 364)
        Me.DgvList.TabIndex = 68
        '
        'No
        '
        Me.No.HeaderText = "No"
        Me.No.Name = "No"
        Me.No.ReadOnly = True
        '
        'メーカー
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.メーカー.DefaultCellStyle = DataGridViewCellStyle1
        Me.メーカー.HeaderText = "メーカー"
        Me.メーカー.MaxInputLength = 50
        Me.メーカー.Name = "メーカー"
        Me.メーカー.Width = 250
        '
        '品名
        '
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.品名.DefaultCellStyle = DataGridViewCellStyle2
        Me.品名.HeaderText = "品名"
        Me.品名.MaxInputLength = 50
        Me.品名.Name = "品名"
        Me.品名.Width = 250
        '
        '型式
        '
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.型式.DefaultCellStyle = DataGridViewCellStyle3
        Me.型式.HeaderText = "型式"
        Me.型式.MaxInputLength = 255
        Me.型式.Name = "型式"
        Me.型式.Width = 250
        '
        '数量
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.数量.DefaultCellStyle = DataGridViewCellStyle4
        Me.数量.HeaderText = "数量"
        Me.数量.MaxInputLength = 8
        Me.数量.Name = "数量"
        Me.数量.Width = 150
        '
        'BtnRegistration
        '
        Me.BtnRegistration.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnRegistration.Location = New System.Drawing.Point(1003, 509)
        Me.BtnRegistration.Name = "BtnRegistration"
        Me.BtnRegistration.Size = New System.Drawing.Size(165, 40)
        Me.BtnRegistration.TabIndex = 69
        Me.BtnRegistration.Text = "登録"
        Me.BtnRegistration.UseVisualStyleBackColor = True
        '
        'BtnRowsAdd
        '
        Me.BtnRowsAdd.Location = New System.Drawing.Point(9, 479)
        Me.BtnRowsAdd.Name = "BtnRowsAdd"
        Me.BtnRowsAdd.Size = New System.Drawing.Size(120, 40)
        Me.BtnRowsAdd.TabIndex = 70
        Me.BtnRowsAdd.TabStop = False
        Me.BtnRowsAdd.Text = "行追加"
        Me.BtnRowsAdd.UseVisualStyleBackColor = True
        '
        'BtnRowsDel
        '
        Me.BtnRowsDel.Location = New System.Drawing.Point(135, 479)
        Me.BtnRowsDel.Name = "BtnRowsDel"
        Me.BtnRowsDel.Size = New System.Drawing.Size(120, 40)
        Me.BtnRowsDel.TabIndex = 71
        Me.BtnRowsDel.TabStop = False
        Me.BtnRowsDel.Text = "行削除"
        Me.BtnRowsDel.UseVisualStyleBackColor = True
        '
        'LblCustomerName
        '
        Me.LblCustomerName.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.LblCustomerName.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblCustomerName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCustomerName.Location = New System.Drawing.Point(3, 3)
        Me.LblCustomerName.Name = "LblCustomerName"
        Me.LblCustomerName.Size = New System.Drawing.Size(110, 23)
        Me.LblCustomerName.TabIndex = 72
        Me.LblCustomerName.Text = "処理区分"
        Me.LblCustomerName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(299, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(110, 23)
        Me.Label1.TabIndex = 73
        Me.Label1.Text = "処理日"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.ColumnCount = 5
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.Controls.Add(Me.CmWarehouseTo, 4, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LblCustomerName, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.DtpProcessingDate, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label4, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.CmInOutKbn, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.CmWarehouseFrom, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.CmProcessingClassification, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(9, 9)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(664, 87)
        Me.TableLayoutPanel1.TabIndex = 74
        '
        'CmWarehouseTo
        '
        Me.CmWarehouseTo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CmWarehouseTo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmWarehouseTo.FormattingEnabled = True
        Me.CmWarehouseTo.Items.AddRange(New Object() {"RegularShip", "DHL", "Air", "Ship"})
        Me.CmWarehouseTo.Location = New System.Drawing.Point(415, 32)
        Me.CmWarehouseTo.Name = "CmWarehouseTo"
        Me.CmWarehouseTo.Size = New System.Drawing.Size(154, 23)
        Me.CmWarehouseTo.TabIndex = 316
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(110, 23)
        Me.Label2.TabIndex = 74
        Me.Label2.Text = "対象倉庫"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DtpProcessingDate
        '
        Me.DtpProcessingDate.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.DtpProcessingDate.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpProcessingDate.CustomFormat = ""
        Me.DtpProcessingDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpProcessingDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpProcessingDate.Location = New System.Drawing.Point(415, 3)
        Me.DtpProcessingDate.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.DtpProcessingDate.Name = "DtpProcessingDate"
        Me.DtpProcessingDate.Size = New System.Drawing.Size(148, 22)
        Me.DtpProcessingDate.TabIndex = 75
        Me.DtpProcessingDate.TabStop = False
        Me.DtpProcessingDate.Value = New Date(2019, 1, 1, 0, 0, 0, 0)
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(299, 32)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(110, 23)
        Me.Label3.TabIndex = 318
        Me.Label3.Text = "移動先"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 61)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(110, 23)
        Me.Label4.TabIndex = 319
        Me.Label4.Text = "入出庫種別"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CmInOutKbn
        '
        Me.CmInOutKbn.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CmInOutKbn.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmInOutKbn.FormattingEnabled = True
        Me.CmInOutKbn.Items.AddRange(New Object() {"RegularShip", "DHL", "Air", "Ship"})
        Me.CmInOutKbn.Location = New System.Drawing.Point(119, 61)
        Me.CmInOutKbn.Name = "CmInOutKbn"
        Me.CmInOutKbn.Size = New System.Drawing.Size(153, 23)
        Me.CmInOutKbn.TabIndex = 320
        '
        'CmWarehouseFrom
        '
        Me.CmWarehouseFrom.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CmWarehouseFrom.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmWarehouseFrom.FormattingEnabled = True
        Me.CmWarehouseFrom.Items.AddRange(New Object() {"RegularShip", "DHL", "Air", "Ship"})
        Me.CmWarehouseFrom.Location = New System.Drawing.Point(119, 32)
        Me.CmWarehouseFrom.Name = "CmWarehouseFrom"
        Me.CmWarehouseFrom.Size = New System.Drawing.Size(154, 23)
        Me.CmWarehouseFrom.TabIndex = 317
        '
        'CmProcessingClassification
        '
        Me.CmProcessingClassification.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CmProcessingClassification.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmProcessingClassification.FormattingEnabled = True
        Me.CmProcessingClassification.Items.AddRange(New Object() {"RegularShip", "DHL", "Air", "Ship"})
        Me.CmProcessingClassification.Location = New System.Drawing.Point(119, 3)
        Me.CmProcessingClassification.Name = "CmProcessingClassification"
        Me.CmProcessingClassification.Size = New System.Drawing.Size(154, 23)
        Me.CmProcessingClassification.TabIndex = 316
        '
        'MovementInput
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.BtnRowsDel)
        Me.Controls.Add(Me.BtnRowsAdd)
        Me.Controls.Add(Me.BtnRegistration)
        Me.Controls.Add(Me.DgvList)
        Me.Controls.Add(Me.LblMode)
        Me.Controls.Add(Me.BtnBack)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "MovementInput"
        Me.Text = "MovementInput"
        CType(Me.DgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnBack As Button
    Friend WithEvents LblMode As Label
    Friend WithEvents VAT As DataGridViewTextBoxColumn
    Friend WithEvents DgvList As DataGridView
    Friend WithEvents BtnRegistration As Button
    Friend WithEvents BtnRowsAdd As Button
    Friend WithEvents BtnRowsDel As Button
    Friend WithEvents LblCustomerName As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Label2 As Label
    Friend WithEvents DtpProcessingDate As DateTimePicker
    Friend WithEvents CmWarehouseTo As ComboBox
    Friend WithEvents CmProcessingClassification As ComboBox
    Friend WithEvents CmWarehouseFrom As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents CmInOutKbn As ComboBox
    Friend WithEvents No As DataGridViewTextBoxColumn
    Friend WithEvents メーカー As DataGridViewTextBoxColumn
    Friend WithEvents 品名 As DataGridViewTextBoxColumn
    Friend WithEvents 型式 As DataGridViewTextBoxColumn
    Friend WithEvents 数量 As DataGridViewTextBoxColumn
End Class
