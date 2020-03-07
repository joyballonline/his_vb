<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Warehouse
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
        Me.LblWarehouseCode = New System.Windows.Forms.Label()
        Me.LblName = New System.Windows.Forms.Label()
        Me.LblShortName = New System.Windows.Forms.Label()
        Me.LblRemarks = New System.Windows.Forms.Label()
        Me.LblInvalidFlag = New System.Windows.Forms.Label()
        Me.TxtWarehouseCode = New System.Windows.Forms.TextBox()
        Me.TxtName = New System.Windows.Forms.TextBox()
        Me.TxtShortName = New System.Windows.Forms.TextBox()
        Me.TxtRemarks = New System.Windows.Forms.TextBox()
        Me.btnAddAccount = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.cmbInvalidFlag = New System.Windows.Forms.ComboBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TxtPhone = New System.Windows.Forms.TextBox()
        Me.LblPhone = New System.Windows.Forms.Label()
        Me.TxtAddress3 = New System.Windows.Forms.TextBox()
        Me.TxtAddress2 = New System.Windows.Forms.TextBox()
        Me.TxtAddress1 = New System.Windows.Forms.TextBox()
        Me.TxtPostalCode = New System.Windows.Forms.TextBox()
        Me.LblAddress2 = New System.Windows.Forms.Label()
        Me.LblPostalCode = New System.Windows.Forms.Label()
        Me.LblAddress1 = New System.Windows.Forms.Label()
        Me.LblCustomsBondKbn = New System.Windows.Forms.Label()
        Me.LblFax = New System.Windows.Forms.Label()
        Me.cmCustomsBondKbn = New System.Windows.Forms.ComboBox()
        Me.TxtFax = New System.Windows.Forms.TextBox()
        Me.LblAddress3 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'LblWarehouseCode
        '
        Me.LblWarehouseCode.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblWarehouseCode.AutoSize = True
        Me.LblWarehouseCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblWarehouseCode.Location = New System.Drawing.Point(3, 6)
        Me.LblWarehouseCode.Name = "LblWarehouseCode"
        Me.LblWarehouseCode.Size = New System.Drawing.Size(70, 15)
        Me.LblWarehouseCode.TabIndex = 176
        Me.LblWarehouseCode.Text = "倉庫コード"
        '
        'LblName
        '
        Me.LblName.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblName.AutoSize = True
        Me.LblName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblName.Location = New System.Drawing.Point(3, 34)
        Me.LblName.Name = "LblName"
        Me.LblName.Size = New System.Drawing.Size(37, 15)
        Me.LblName.TabIndex = 178
        Me.LblName.Text = "名称"
        '
        'LblShortName
        '
        Me.LblShortName.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblShortName.AutoSize = True
        Me.LblShortName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblShortName.Location = New System.Drawing.Point(3, 62)
        Me.LblShortName.Name = "LblShortName"
        Me.LblShortName.Size = New System.Drawing.Size(37, 15)
        Me.LblShortName.TabIndex = 180
        Me.LblShortName.Text = "略称"
        '
        'LblRemarks
        '
        Me.LblRemarks.AutoSize = True
        Me.LblRemarks.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblRemarks.Location = New System.Drawing.Point(3, 335)
        Me.LblRemarks.Name = "LblRemarks"
        Me.LblRemarks.Padding = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.LblRemarks.Size = New System.Drawing.Size(37, 21)
        Me.LblRemarks.TabIndex = 182
        Me.LblRemarks.Text = "備考"
        '
        'LblInvalidFlag
        '
        Me.LblInvalidFlag.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblInvalidFlag.AutoSize = True
        Me.LblInvalidFlag.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblInvalidFlag.Location = New System.Drawing.Point(3, 395)
        Me.LblInvalidFlag.Name = "LblInvalidFlag"
        Me.LblInvalidFlag.Size = New System.Drawing.Size(69, 15)
        Me.LblInvalidFlag.TabIndex = 188
        Me.LblInvalidFlag.Text = "無効フラグ"
        '
        'TxtWarehouseCode
        '
        Me.TxtWarehouseCode.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TxtWarehouseCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtWarehouseCode.Location = New System.Drawing.Point(157, 3)
        Me.TxtWarehouseCode.MaxLength = 20
        Me.TxtWarehouseCode.Name = "TxtWarehouseCode"
        Me.TxtWarehouseCode.Size = New System.Drawing.Size(283, 22)
        Me.TxtWarehouseCode.TabIndex = 1
        '
        'TxtName
        '
        Me.TxtName.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TxtName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtName.Location = New System.Drawing.Point(157, 31)
        Me.TxtName.MaxLength = 50
        Me.TxtName.Name = "TxtName"
        Me.TxtName.Size = New System.Drawing.Size(283, 22)
        Me.TxtName.TabIndex = 2
        '
        'TxtShortName
        '
        Me.TxtShortName.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TxtShortName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtShortName.Location = New System.Drawing.Point(157, 59)
        Me.TxtShortName.MaxLength = 20
        Me.TxtShortName.Name = "TxtShortName"
        Me.TxtShortName.Size = New System.Drawing.Size(283, 22)
        Me.TxtShortName.TabIndex = 3
        '
        'TxtRemarks
        '
        Me.TxtRemarks.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TxtRemarks.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtRemarks.Location = New System.Drawing.Point(157, 338)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Multiline = True
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.TxtRemarks.Size = New System.Drawing.Size(283, 46)
        Me.TxtRemarks.TabIndex = 11
        '
        'btnAddAccount
        '
        Me.btnAddAccount.Location = New System.Drawing.Point(381, 509)
        Me.btnAddAccount.Name = "btnAddAccount"
        Me.btnAddAccount.Size = New System.Drawing.Size(165, 40)
        Me.btnAddAccount.TabIndex = 13
        Me.btnAddAccount.Text = "登録"
        Me.btnAddAccount.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.Location = New System.Drawing.Point(552, 509)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(165, 40)
        Me.btnBack.TabIndex = 14
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'cmbInvalidFlag
        '
        Me.cmbInvalidFlag.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmbInvalidFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbInvalidFlag.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbInvalidFlag.FormattingEnabled = True
        Me.cmbInvalidFlag.ItemHeight = 15
        Me.cmbInvalidFlag.Location = New System.Drawing.Point(157, 391)
        Me.cmbInvalidFlag.Name = "cmbInvalidFlag"
        Me.cmbInvalidFlag.Size = New System.Drawing.Size(283, 23)
        Me.cmbInvalidFlag.TabIndex = 12
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.92278!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.07722!))
        Me.TableLayoutPanel1.Controls.Add(Me.TxtPhone, 1, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.LblPhone, 0, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.TxtAddress3, 1, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.TxtAddress2, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.TxtAddress1, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.TxtPostalCode, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.LblWarehouseCode, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TxtWarehouseCode, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LblShortName, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TxtShortName, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LblName, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TxtName, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LblAddress2, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.LblPostalCode, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.LblAddress1, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.LblInvalidFlag, 0, 11)
        Me.TableLayoutPanel1.Controls.Add(Me.cmbInvalidFlag, 1, 11)
        Me.TableLayoutPanel1.Controls.Add(Me.LblCustomsBondKbn, 0, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.TxtRemarks, 1, 10)
        Me.TableLayoutPanel1.Controls.Add(Me.LblFax, 0, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.cmCustomsBondKbn, 1, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.TxtFax, 1, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.LblAddress3, 0, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.LblRemarks, 0, 10)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(12, 12)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 12
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(518, 419)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'TxtPhone
        '
        Me.TxtPhone.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TxtPhone.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPhone.Location = New System.Drawing.Point(157, 253)
        Me.TxtPhone.MaxLength = 20
        Me.TxtPhone.Name = "TxtPhone"
        Me.TxtPhone.Size = New System.Drawing.Size(283, 22)
        Me.TxtPhone.TabIndex = 8
        '
        'LblPhone
        '
        Me.LblPhone.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblPhone.AutoSize = True
        Me.LblPhone.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPhone.Location = New System.Drawing.Point(3, 256)
        Me.LblPhone.Name = "LblPhone"
        Me.LblPhone.Size = New System.Drawing.Size(67, 15)
        Me.LblPhone.TabIndex = 194
        Me.LblPhone.Text = "電話番号"
        '
        'TxtAddress3
        '
        Me.TxtAddress3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TxtAddress3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAddress3.Location = New System.Drawing.Point(157, 207)
        Me.TxtAddress3.MaxLength = 100
        Me.TxtAddress3.Multiline = True
        Me.TxtAddress3.Name = "TxtAddress3"
        Me.TxtAddress3.Size = New System.Drawing.Size(283, 40)
        Me.TxtAddress3.TabIndex = 7
        '
        'TxtAddress2
        '
        Me.TxtAddress2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TxtAddress2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAddress2.Location = New System.Drawing.Point(157, 161)
        Me.TxtAddress2.MaxLength = 100
        Me.TxtAddress2.Multiline = True
        Me.TxtAddress2.Name = "TxtAddress2"
        Me.TxtAddress2.Size = New System.Drawing.Size(283, 40)
        Me.TxtAddress2.TabIndex = 6
        '
        'TxtAddress1
        '
        Me.TxtAddress1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TxtAddress1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAddress1.Location = New System.Drawing.Point(157, 115)
        Me.TxtAddress1.MaxLength = 100
        Me.TxtAddress1.Multiline = True
        Me.TxtAddress1.Name = "TxtAddress1"
        Me.TxtAddress1.Size = New System.Drawing.Size(283, 40)
        Me.TxtAddress1.TabIndex = 5
        '
        'TxtPostalCode
        '
        Me.TxtPostalCode.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TxtPostalCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPostalCode.Location = New System.Drawing.Point(157, 87)
        Me.TxtPostalCode.MaxLength = 7
        Me.TxtPostalCode.Name = "TxtPostalCode"
        Me.TxtPostalCode.Size = New System.Drawing.Size(283, 22)
        Me.TxtPostalCode.TabIndex = 4
        '
        'LblAddress2
        '
        Me.LblAddress2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblAddress2.AutoSize = True
        Me.LblAddress2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAddress2.Location = New System.Drawing.Point(3, 173)
        Me.LblAddress2.Name = "LblAddress2"
        Me.LblAddress2.Size = New System.Drawing.Size(47, 15)
        Me.LblAddress2.TabIndex = 192
        Me.LblAddress2.Text = "住所２"
        '
        'LblPostalCode
        '
        Me.LblPostalCode.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblPostalCode.AutoSize = True
        Me.LblPostalCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPostalCode.Location = New System.Drawing.Point(3, 90)
        Me.LblPostalCode.Name = "LblPostalCode"
        Me.LblPostalCode.Size = New System.Drawing.Size(67, 15)
        Me.LblPostalCode.TabIndex = 190
        Me.LblPostalCode.Text = "郵便番号"
        '
        'LblAddress1
        '
        Me.LblAddress1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblAddress1.AutoSize = True
        Me.LblAddress1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAddress1.Location = New System.Drawing.Point(3, 127)
        Me.LblAddress1.Name = "LblAddress1"
        Me.LblAddress1.Size = New System.Drawing.Size(47, 15)
        Me.LblAddress1.TabIndex = 191
        Me.LblAddress1.Text = "住所１"
        '
        'LblCustomsBondKbn
        '
        Me.LblCustomsBondKbn.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblCustomsBondKbn.AutoSize = True
        Me.LblCustomsBondKbn.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCustomsBondKbn.Location = New System.Drawing.Point(3, 313)
        Me.LblCustomsBondKbn.Name = "LblCustomsBondKbn"
        Me.LblCustomsBondKbn.Size = New System.Drawing.Size(67, 15)
        Me.LblCustomsBondKbn.TabIndex = 189
        Me.LblCustomsBondKbn.Text = "保税有無"
        '
        'LblFax
        '
        Me.LblFax.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblFax.AutoSize = True
        Me.LblFax.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblFax.Location = New System.Drawing.Point(3, 284)
        Me.LblFax.Name = "LblFax"
        Me.LblFax.Size = New System.Drawing.Size(67, 15)
        Me.LblFax.TabIndex = 193
        Me.LblFax.Text = "ＦＡＸ番号"
        '
        'cmCustomsBondKbn
        '
        Me.cmCustomsBondKbn.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmCustomsBondKbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmCustomsBondKbn.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmCustomsBondKbn.FormattingEnabled = True
        Me.cmCustomsBondKbn.ItemHeight = 15
        Me.cmCustomsBondKbn.Location = New System.Drawing.Point(157, 309)
        Me.cmCustomsBondKbn.Name = "cmCustomsBondKbn"
        Me.cmCustomsBondKbn.Size = New System.Drawing.Size(283, 23)
        Me.cmCustomsBondKbn.TabIndex = 10
        '
        'TxtFax
        '
        Me.TxtFax.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TxtFax.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtFax.Location = New System.Drawing.Point(157, 281)
        Me.TxtFax.MaxLength = 20
        Me.TxtFax.Name = "TxtFax"
        Me.TxtFax.Size = New System.Drawing.Size(283, 22)
        Me.TxtFax.TabIndex = 9
        '
        'LblAddress3
        '
        Me.LblAddress3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblAddress3.AutoSize = True
        Me.LblAddress3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAddress3.Location = New System.Drawing.Point(3, 218)
        Me.LblAddress3.Name = "LblAddress3"
        Me.LblAddress3.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.LblAddress3.Size = New System.Drawing.Size(47, 18)
        Me.LblAddress3.TabIndex = 194
        Me.LblAddress3.Text = "住所３"
        '
        'Warehouse
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(729, 561)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnAddAccount)
        Me.Name = "Warehouse"
        Me.Text = "Warehouse"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LblWarehouseCode As Label
    Friend WithEvents LblName As Label
    Friend WithEvents LblShortName As Label
    Friend WithEvents LblRemarks As Label
    Friend WithEvents LblInvalidFlag As Label
    Friend WithEvents TxtWarehouseCode As TextBox
    Friend WithEvents TxtName As TextBox
    Friend WithEvents TxtShortName As TextBox
    Friend WithEvents TxtRemarks As TextBox
    Friend WithEvents btnAddAccount As Button
    Friend WithEvents btnBack As Button
    Friend WithEvents cmbInvalidFlag As ComboBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents LblCustomsBondKbn As Label
    Friend WithEvents cmCustomsBondKbn As ComboBox
    Friend WithEvents LblPostalCode As Label
    Friend WithEvents LblAddress1 As Label
    Friend WithEvents LblAddress2 As Label
    Friend WithEvents TxtAddress1 As TextBox
    Friend WithEvents TxtPostalCode As TextBox
    Friend WithEvents LblFax As Label
    Friend WithEvents LblAddress3 As Label
    Friend WithEvents TxtAddress3 As TextBox
    Friend WithEvents TxtAddress2 As TextBox
    Friend WithEvents TxtFax As TextBox
    Friend WithEvents LblPhone As Label
    Friend WithEvents TxtPhone As TextBox
End Class
