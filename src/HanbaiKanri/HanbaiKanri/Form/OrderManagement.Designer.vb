﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OrderManagement
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
        Me.TxtSuffixNo = New System.Windows.Forms.TextBox()
        Me.BtnRegist = New System.Windows.Forms.Button()
        Me.DtpPurchaseDate = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TxtRemarks = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TxtCount3 = New System.Windows.Forms.TextBox()
        Me.DgvAdd = New System.Windows.Forms.DataGridView()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtCount2 = New System.Windows.Forms.TextBox()
        Me.DgvHistory = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtCount1 = New System.Windows.Forms.TextBox()
        Me.DgvOrder = New System.Windows.Forms.DataGridView()
        Me.LblPurchaseDate = New System.Windows.Forms.Label()
        Me.TxtOrderDate = New System.Windows.Forms.TextBox()
        Me.TxtCustomerName = New System.Windows.Forms.TextBox()
        Me.LblSupplier = New System.Windows.Forms.Label()
        Me.TxtCustomerCode = New System.Windows.Forms.TextBox()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.LblPurchaseNo = New System.Windows.Forms.Label()
        Me.TxtOrderNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
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
        Me.TxtSuffixNo.TabIndex = 258
        '
        'BtnRegist
        '
        Me.BtnRegist.Location = New System.Drawing.Point(1002, 678)
        Me.BtnRegist.Name = "BtnRegist"
        Me.BtnRegist.Size = New System.Drawing.Size(165, 40)
        Me.BtnRegist.TabIndex = 257
        Me.BtnRegist.Text = "登録"
        Me.BtnRegist.UseVisualStyleBackColor = True
        '
        'DtpPurchaseDate
        '
        Me.DtpPurchaseDate.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpPurchaseDate.CustomFormat = "yyyy/MM/dd"
        Me.DtpPurchaseDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpPurchaseDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpPurchaseDate.Location = New System.Drawing.Point(292, 443)
        Me.DtpPurchaseDate.Name = "DtpPurchaseDate"
        Me.DtpPurchaseDate.Size = New System.Drawing.Size(148, 22)
        Me.DtpPurchaseDate.TabIndex = 256
        Me.DtpPurchaseDate.TabStop = False
        Me.DtpPurchaseDate.Value = New Date(2018, 6, 5, 23, 23, 58, 0)
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label9.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(446, 443)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(170, 22)
        Me.Label9.TabIndex = 255
        Me.Label9.Text = "備考"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtRemarks
        '
        Me.TxtRemarks.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtRemarks.Location = New System.Drawing.Point(622, 443)
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(644, 22)
        Me.TxtRemarks.TabIndex = 254
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(188, 443)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(98, 22)
        Me.Label8.TabIndex = 253
        Me.Label8.Text = "受注日"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 443)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(89, 22)
        Me.Label4.TabIndex = 252
        Me.Label4.Text = "■今回売上"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(1316, 443)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(22, 22)
        Me.Label7.TabIndex = 251
        Me.Label7.Text = "件"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtCount3
        '
        Me.TxtCount3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtCount3.Enabled = False
        Me.TxtCount3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCount3.Location = New System.Drawing.Point(1272, 443)
        Me.TxtCount3.Name = "TxtCount3"
        Me.TxtCount3.Size = New System.Drawing.Size(38, 22)
        Me.TxtCount3.TabIndex = 250
        '
        'DgvAdd
        '
        Me.DgvAdd.AllowUserToAddRows = False
        Me.DgvAdd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvAdd.Location = New System.Drawing.Point(12, 468)
        Me.DgvAdd.Name = "DgvAdd"
        Me.DgvAdd.RowTemplate.Height = 21
        Me.DgvAdd.Size = New System.Drawing.Size(1326, 150)
        Me.DgvAdd.TabIndex = 249
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(12, 262)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(89, 22)
        Me.Label6.TabIndex = 248
        Me.Label6.Text = "■売上済み"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(12, 82)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(57, 22)
        Me.Label5.TabIndex = 247
        Me.Label5.Text = "■受注"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(1316, 262)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(22, 22)
        Me.Label3.TabIndex = 246
        Me.Label3.Text = "件"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtCount2
        '
        Me.TxtCount2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtCount2.Enabled = False
        Me.TxtCount2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCount2.Location = New System.Drawing.Point(1272, 262)
        Me.TxtCount2.Name = "TxtCount2"
        Me.TxtCount2.Size = New System.Drawing.Size(38, 22)
        Me.TxtCount2.TabIndex = 245
        '
        'DgvHistory
        '
        Me.DgvHistory.AllowUserToAddRows = False
        Me.DgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvHistory.Location = New System.Drawing.Point(12, 287)
        Me.DgvHistory.Name = "DgvHistory"
        Me.DgvHistory.ReadOnly = True
        Me.DgvHistory.RowTemplate.Height = 21
        Me.DgvHistory.Size = New System.Drawing.Size(1326, 150)
        Me.DgvHistory.TabIndex = 244
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(1316, 82)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(22, 22)
        Me.Label2.TabIndex = 243
        Me.Label2.Text = "件"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        '
        'DgvOrder
        '
        Me.DgvOrder.AllowUserToAddRows = False
        Me.DgvOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvOrder.Location = New System.Drawing.Point(12, 106)
        Me.DgvOrder.Name = "DgvOrder"
        Me.DgvOrder.ReadOnly = True
        Me.DgvOrder.RowTemplate.Height = 21
        Me.DgvOrder.Size = New System.Drawing.Size(1326, 150)
        Me.DgvOrder.TabIndex = 241
        '
        'LblPurchaseDate
        '
        Me.LblPurchaseDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblPurchaseDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblPurchaseDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPurchaseDate.Location = New System.Drawing.Point(393, 10)
        Me.LblPurchaseDate.Name = "LblPurchaseDate"
        Me.LblPurchaseDate.Size = New System.Drawing.Size(170, 22)
        Me.LblPurchaseDate.TabIndex = 240
        Me.LblPurchaseDate.Text = "受注日"
        Me.LblPurchaseDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtOrderDate
        '
        Me.TxtOrderDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtOrderDate.Enabled = False
        Me.TxtOrderDate.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtOrderDate.Location = New System.Drawing.Point(569, 10)
        Me.TxtOrderDate.Name = "TxtOrderDate"
        Me.TxtOrderDate.Size = New System.Drawing.Size(157, 22)
        Me.TxtOrderDate.TabIndex = 239
        '
        'TxtCustomerName
        '
        Me.TxtCustomerName.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtCustomerName.Enabled = False
        Me.TxtCustomerName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerName.Location = New System.Drawing.Point(351, 38)
        Me.TxtCustomerName.Name = "TxtCustomerName"
        Me.TxtCustomerName.Size = New System.Drawing.Size(333, 22)
        Me.TxtCustomerName.TabIndex = 238
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
        Me.LblSupplier.Text = "得意先"
        Me.LblSupplier.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtCustomerCode
        '
        Me.TxtCustomerCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtCustomerCode.Enabled = False
        Me.TxtCustomerCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtCustomerCode.Location = New System.Drawing.Point(188, 38)
        Me.TxtCustomerCode.Name = "TxtCustomerCode"
        Me.TxtCustomerCode.Size = New System.Drawing.Size(157, 22)
        Me.TxtCustomerCode.TabIndex = 236
        '
        'BtnBack
        '
        Me.BtnBack.Location = New System.Drawing.Point(1173, 678)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 235
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
        Me.LblPurchaseNo.Text = "受注番号"
        Me.LblPurchaseNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtOrderNo
        '
        Me.TxtOrderNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtOrderNo.Enabled = False
        Me.TxtOrderNo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtOrderNo.Location = New System.Drawing.Point(188, 10)
        Me.TxtOrderNo.Name = "TxtOrderNo"
        Me.TxtOrderNo.Size = New System.Drawing.Size(157, 22)
        Me.TxtOrderNo.TabIndex = 233
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 12)
        Me.Label1.TabIndex = 232
        '
        'OrderManagemant
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1350, 729)
        Me.Controls.Add(Me.TxtSuffixNo)
        Me.Controls.Add(Me.BtnRegist)
        Me.Controls.Add(Me.DtpPurchaseDate)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.TxtRemarks)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TxtCount3)
        Me.Controls.Add(Me.DgvAdd)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtCount2)
        Me.Controls.Add(Me.DgvHistory)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtCount1)
        Me.Controls.Add(Me.DgvOrder)
        Me.Controls.Add(Me.LblPurchaseDate)
        Me.Controls.Add(Me.TxtOrderDate)
        Me.Controls.Add(Me.TxtCustomerName)
        Me.Controls.Add(Me.LblSupplier)
        Me.Controls.Add(Me.TxtCustomerCode)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.LblPurchaseNo)
        Me.Controls.Add(Me.TxtOrderNo)
        Me.Controls.Add(Me.Label1)
        Me.Name = "OrderManagemant"
        Me.Text = "OrderManagemant"
        CType(Me.DgvAdd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvOrder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtSuffixNo As TextBox
    Friend WithEvents BtnRegist As Button
    Friend WithEvents DtpPurchaseDate As DateTimePicker
    Friend WithEvents Label9 As Label
    Friend WithEvents TxtRemarks As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents TxtCount3 As TextBox
    Friend WithEvents DgvAdd As DataGridView
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TxtCount2 As TextBox
    Friend WithEvents DgvHistory As DataGridView
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtCount1 As TextBox
    Friend WithEvents DgvOrder As DataGridView
    Friend WithEvents LblPurchaseDate As Label
    Friend WithEvents TxtOrderDate As TextBox
    Friend WithEvents TxtCustomerName As TextBox
    Friend WithEvents LblSupplier As Label
    Friend WithEvents TxtCustomerCode As TextBox
    Friend WithEvents BtnBack As Button
    Friend WithEvents LblPurchaseNo As Label
    Friend WithEvents TxtOrderNo As TextBox
    Friend WithEvents Label1 As Label
End Class