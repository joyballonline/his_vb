﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class InventoryList
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
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.DgvList = New System.Windows.Forms.DataGridView()
        Me.LblMode = New System.Windows.Forms.Label()
        Me.BtnExcelOutput = New System.Windows.Forms.Button()
        Me.rbAll = New System.Windows.Forms.RadioButton()
        Me.rbWarehouse = New System.Windows.Forms.RadioButton()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.rbSerialNo = New System.Windows.Forms.RadioButton()
        Me.rbSyubetsu = New System.Windows.Forms.RadioButton()
        Me.rbOrderNo = New System.Windows.Forms.RadioButton()
        Me.rbLocation = New System.Windows.Forms.RadioButton()
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
        'DgvList
        '
        Me.DgvList.AllowUserToAddRows = False
        Me.DgvList.AllowUserToDeleteRows = False
        Me.DgvList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText
        Me.DgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvList.Location = New System.Drawing.Point(13, 51)
        Me.DgvList.Name = "DgvList"
        Me.DgvList.RowHeadersVisible = False
        Me.DgvList.RowTemplate.Height = 21
        Me.DgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvList.Size = New System.Drawing.Size(1326, 444)
        Me.DgvList.TabIndex = 15
        '
        'LblMode
        '
        Me.LblMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMode.Font = New System.Drawing.Font("MS Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMode.Location = New System.Drawing.Point(1106, 9)
        Me.LblMode.Name = "LblMode"
        Me.LblMode.Size = New System.Drawing.Size(233, 22)
        Me.LblMode.TabIndex = 67
        Me.LblMode.Text = "モード"
        Me.LblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnExcelOutput
        '
        Me.BtnExcelOutput.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnExcelOutput.Location = New System.Drawing.Point(1003, 509)
        Me.BtnExcelOutput.Name = "BtnExcelOutput"
        Me.BtnExcelOutput.Size = New System.Drawing.Size(165, 40)
        Me.BtnExcelOutput.TabIndex = 22
        Me.BtnExcelOutput.Text = "Excel出力"
        Me.BtnExcelOutput.UseVisualStyleBackColor = True
        '
        'rbAll
        '
        Me.rbAll.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.rbAll.AutoSize = True
        Me.rbAll.Checked = True
        Me.rbAll.Location = New System.Drawing.Point(3, 8)
        Me.rbAll.Name = "rbAll"
        Me.rbAll.Size = New System.Drawing.Size(61, 17)
        Me.rbAll.TabIndex = 68
        Me.rbAll.TabStop = True
        Me.rbAll.Text = "商品別"
        Me.rbAll.UseVisualStyleBackColor = True
        '
        'rbWarehouse
        '
        Me.rbWarehouse.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.rbWarehouse.AutoSize = True
        Me.rbWarehouse.Location = New System.Drawing.Point(107, 8)
        Me.rbWarehouse.Name = "rbWarehouse"
        Me.rbWarehouse.Size = New System.Drawing.Size(61, 17)
        Me.rbWarehouse.TabIndex = 69
        Me.rbWarehouse.TabStop = True
        Me.rbWarehouse.Text = "倉庫別"
        Me.rbWarehouse.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 6
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.48544!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.51456!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.rbAll, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.rbSerialNo, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.rbWarehouse, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.rbSyubetsu, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.rbOrderNo, 5, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.rbLocation, 3, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(13, 9)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(592, 33)
        Me.TableLayoutPanel1.TabIndex = 71
        '
        'rbSerialNo
        '
        Me.rbSerialNo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.rbSerialNo.AutoSize = True
        Me.rbSerialNo.Location = New System.Drawing.Point(399, 8)
        Me.rbSerialNo.Name = "rbSerialNo"
        Me.rbSerialNo.Size = New System.Drawing.Size(85, 17)
        Me.rbSerialNo.TabIndex = 73
        Me.rbSerialNo.TabStop = True
        Me.rbSerialNo.Text = "製造番号別"
        Me.rbSerialNo.UseVisualStyleBackColor = True
        '
        'rbSyubetsu
        '
        Me.rbSyubetsu.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.rbSyubetsu.AutoSize = True
        Me.rbSyubetsu.Location = New System.Drawing.Point(209, 8)
        Me.rbSyubetsu.Name = "rbSyubetsu"
        Me.rbSyubetsu.Size = New System.Drawing.Size(85, 17)
        Me.rbSyubetsu.TabIndex = 72
        Me.rbSyubetsu.TabStop = True
        Me.rbSyubetsu.Text = "入出庫種別"
        Me.rbSyubetsu.UseVisualStyleBackColor = True
        '
        'rbOrderNo
        '
        Me.rbOrderNo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.rbOrderNo.AutoSize = True
        Me.rbOrderNo.Location = New System.Drawing.Point(506, 8)
        Me.rbOrderNo.Name = "rbOrderNo"
        Me.rbOrderNo.Size = New System.Drawing.Size(61, 17)
        Me.rbOrderNo.TabIndex = 74
        Me.rbOrderNo.TabStop = True
        Me.rbOrderNo.Text = "伝票別"
        Me.rbOrderNo.UseVisualStyleBackColor = True
        '
        'rbLocation
        '
        Me.rbLocation.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.rbLocation.AutoSize = True
        Me.rbLocation.Location = New System.Drawing.Point(309, 8)
        Me.rbLocation.Name = "rbLocation"
        Me.rbLocation.Size = New System.Drawing.Size(84, 17)
        Me.rbLocation.TabIndex = 70
        Me.rbLocation.TabStop = True
        Me.rbLocation.Text = "ロケーション別"
        Me.rbLocation.UseVisualStyleBackColor = True
        '
        'InventoryList
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1348, 561)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.LblMode)
        Me.Controls.Add(Me.BtnExcelOutput)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.DgvList)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "InventoryList"
        Me.Text = "InventoryList"
        CType(Me.DgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnBack As Button
    Friend WithEvents DgvList As DataGridView
    Friend WithEvents LblMode As Label
    Friend WithEvents BtnExcelOutput As Button
    Friend WithEvents VAT As DataGridViewTextBoxColumn
    Friend WithEvents rbAll As RadioButton
    Friend WithEvents rbWarehouse As RadioButton
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents rbSerialNo As RadioButton
    Friend WithEvents rbSyubetsu As RadioButton
    Friend WithEvents rbOrderNo As RadioButton
    Friend WithEvents rbLocation As RadioButton
End Class
