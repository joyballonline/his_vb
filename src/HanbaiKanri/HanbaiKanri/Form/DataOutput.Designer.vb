<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DataOutput
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
        Me.LblPeriod = New System.Windows.Forms.Label()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.LblMode = New System.Windows.Forms.Label()
        Me.DtpDateSince = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DtpDateUntil = New System.Windows.Forms.DateTimePicker()
        Me.LblTarget = New System.Windows.Forms.Label()
        Me.RbtnQuotation = New System.Windows.Forms.RadioButton()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.RbtnSales = New System.Windows.Forms.RadioButton()
        Me.RbtnJobOrder = New System.Windows.Forms.RadioButton()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.BtnCSVOutput = New System.Windows.Forms.Button()
        Me.DgvList = New System.Windows.Forms.DataGridView()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.DgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LblPeriod
        '
        Me.LblPeriod.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblPeriod.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblPeriod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblPeriod.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPeriod.Location = New System.Drawing.Point(3, 31)
        Me.LblPeriod.Name = "LblPeriod"
        Me.LblPeriod.Size = New System.Drawing.Size(193, 22)
        Me.LblPeriod.TabIndex = 80
        Me.LblPeriod.Text = "期間"
        Me.LblPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(1174, 509)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 17
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'LblMode
        '
        Me.LblMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMode.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMode.Location = New System.Drawing.Point(1173, 9)
        Me.LblMode.Name = "LblMode"
        Me.LblMode.Size = New System.Drawing.Size(165, 22)
        Me.LblMode.TabIndex = 96
        Me.LblMode.Text = "参照モード"
        Me.LblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DtpDateSince
        '
        Me.DtpDateSince.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.DtpDateSince.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpDateSince.CustomFormat = ""
        Me.DtpDateSince.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpDateSince.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpDateSince.Location = New System.Drawing.Point(0, 2)
        Me.DtpDateSince.Margin = New System.Windows.Forms.Padding(0)
        Me.DtpDateSince.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.DtpDateSince.Name = "DtpDateSince"
        Me.DtpDateSince.Size = New System.Drawing.Size(148, 22)
        Me.DtpDateSince.TabIndex = 97
        Me.DtpDateSince.TabStop = False
        Me.DtpDateSince.Value = New Date(2019, 1, 1, 0, 0, 0, 0)
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(151, 7)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(17, 12)
        Me.Label5.TabIndex = 99
        Me.Label5.Text = "～"
        '
        'DtpDateUntil
        '
        Me.DtpDateUntil.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.DtpDateUntil.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpDateUntil.CustomFormat = ""
        Me.DtpDateUntil.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DtpDateUntil.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpDateUntil.Location = New System.Drawing.Point(171, 2)
        Me.DtpDateUntil.Margin = New System.Windows.Forms.Padding(0)
        Me.DtpDateUntil.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.DtpDateUntil.Name = "DtpDateUntil"
        Me.DtpDateUntil.Size = New System.Drawing.Size(148, 22)
        Me.DtpDateUntil.TabIndex = 100
        Me.DtpDateUntil.TabStop = False
        Me.DtpDateUntil.Value = New Date(2019, 1, 1, 0, 0, 0, 0)
        '
        'LblTarget
        '
        Me.LblTarget.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblTarget.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblTarget.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblTarget.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTarget.Location = New System.Drawing.Point(3, 2)
        Me.LblTarget.Name = "LblTarget"
        Me.LblTarget.Size = New System.Drawing.Size(193, 22)
        Me.LblTarget.TabIndex = 101
        Me.LblTarget.Text = "対象"
        Me.LblTarget.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RbtnQuotation
        '
        Me.RbtnQuotation.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RbtnQuotation.AutoSize = True
        Me.RbtnQuotation.Checked = True
        Me.RbtnQuotation.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.RbtnQuotation.Location = New System.Drawing.Point(3, 4)
        Me.RbtnQuotation.Name = "RbtnQuotation"
        Me.RbtnQuotation.Size = New System.Drawing.Size(57, 19)
        Me.RbtnQuotation.TabIndex = 102
        Me.RbtnQuotation.TabStop = True
        Me.RbtnQuotation.Text = "見積"
        Me.RbtnQuotation.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.40925!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.59074!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.LblPeriod, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LblTarget, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 1, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(12, 9)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(562, 57)
        Me.TableLayoutPanel1.TabIndex = 103
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel3.ColumnCount = 3
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel3.Controls.Add(Me.RbtnSales, 2, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.RbtnJobOrder, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.RbtnQuotation, 0, 0)
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(199, 0)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(363, 27)
        Me.TableLayoutPanel3.TabIndex = 103
        '
        'RbtnSales
        '
        Me.RbtnSales.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RbtnSales.AutoSize = True
        Me.RbtnSales.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.RbtnSales.Location = New System.Drawing.Point(129, 4)
        Me.RbtnSales.Name = "RbtnSales"
        Me.RbtnSales.Size = New System.Drawing.Size(57, 19)
        Me.RbtnSales.TabIndex = 104
        Me.RbtnSales.Text = "売上"
        Me.RbtnSales.UseVisualStyleBackColor = True
        '
        'RbtnJobOrder
        '
        Me.RbtnJobOrder.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RbtnJobOrder.AutoSize = True
        Me.RbtnJobOrder.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.RbtnJobOrder.Location = New System.Drawing.Point(66, 4)
        Me.RbtnJobOrder.Name = "RbtnJobOrder"
        Me.RbtnJobOrder.Size = New System.Drawing.Size(57, 19)
        Me.RbtnJobOrder.TabIndex = 103
        Me.RbtnJobOrder.Text = "受注"
        Me.RbtnJobOrder.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel2.Controls.Add(Me.DtpDateSince, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Label5, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.DtpDateUntil, 2, 0)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(199, 29)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(363, 26)
        Me.TableLayoutPanel2.TabIndex = 102
        '
        'BtnCSVOutput
        '
        Me.BtnCSVOutput.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnCSVOutput.Location = New System.Drawing.Point(1003, 509)
        Me.BtnCSVOutput.Name = "BtnCSVOutput"
        Me.BtnCSVOutput.Size = New System.Drawing.Size(165, 40)
        Me.BtnCSVOutput.TabIndex = 104
        Me.BtnCSVOutput.Text = "CSV出力"
        Me.BtnCSVOutput.UseVisualStyleBackColor = True
        '
        'DgvList
        '
        Me.DgvList.AllowUserToAddRows = False
        Me.DgvList.AllowUserToDeleteRows = False
        Me.DgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvList.Location = New System.Drawing.Point(12, 72)
        Me.DgvList.Name = "DgvList"
        Me.DgvList.ReadOnly = True
        Me.DgvList.RowHeadersVisible = False
        Me.DgvList.RowTemplate.Height = 21
        Me.DgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvList.Size = New System.Drawing.Size(1326, 430)
        Me.DgvList.TabIndex = 105
        '
        'DataOutput
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.DgvList)
        Me.Controls.Add(Me.BtnCSVOutput)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.LblMode)
        Me.Controls.Add(Me.BtnBack)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "DataOutput"
        Me.Text = "DataOutput"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.DgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LblPeriod As Label
    Friend WithEvents BtnBack As Button
    Friend WithEvents LblMode As Label
    Friend WithEvents DtpDateSince As DateTimePicker
    Friend WithEvents Label5 As Label
    Friend WithEvents DtpDateUntil As DateTimePicker
    Friend WithEvents LblTarget As Label
    Friend WithEvents RbtnQuotation As RadioButton
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents RbtnSales As RadioButton
    Friend WithEvents RbtnJobOrder As RadioButton
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents BtnCSVOutput As Button
    Friend WithEvents DgvList As DataGridView
End Class
