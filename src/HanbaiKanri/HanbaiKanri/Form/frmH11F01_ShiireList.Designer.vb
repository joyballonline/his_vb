<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmH11F01_ShiireList
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
        Me.dtSiirebiTo = New System.Windows.Forms.DateTimePicker()
        Me.dtSiirebiFrom = New System.Windows.Forms.DateTimePicker()
        Me.TableLayoutPanel33 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmdPreview = New System.Windows.Forms.Button()
        Me.cmdBack = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.lblDenStrTo = New System.Windows.Forms.Label()
        Me.txtDenNoTo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblDenStrFrom = New System.Windows.Forms.Label()
        Me.txtDenNoFrom = New System.Windows.Forms.TextBox()
        Me.lblSiirebiWeekTo = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblSiirebiWeekFrom = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel33.SuspendLayout()
        Me.SuspendLayout()
        '
        'dtSiirebiTo
        '
        Me.dtSiirebiTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtSiirebiTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtSiirebiTo.Location = New System.Drawing.Point(351, 65)
        Me.dtSiirebiTo.Name = "dtSiirebiTo"
        Me.dtSiirebiTo.Size = New System.Drawing.Size(104, 22)
        Me.dtSiirebiTo.TabIndex = 1
        '
        'dtSiirebiFrom
        '
        Me.dtSiirebiFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtSiirebiFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtSiirebiFrom.Location = New System.Drawing.Point(170, 65)
        Me.dtSiirebiFrom.Name = "dtSiirebiFrom"
        Me.dtSiirebiFrom.Size = New System.Drawing.Size(104, 22)
        Me.dtSiirebiFrom.TabIndex = 0
        '
        'TableLayoutPanel33
        '
        Me.TableLayoutPanel33.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel33.ColumnCount = 3
        Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.97701!))
        Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.02299!))
        Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 143.0!))
        Me.TableLayoutPanel33.Controls.Add(Me.cmdPreview, 0, 0)
        Me.TableLayoutPanel33.Controls.Add(Me.cmdBack, 2, 0)
        Me.TableLayoutPanel33.Controls.Add(Me.cmdPrint, 1, 0)
        Me.TableLayoutPanel33.Location = New System.Drawing.Point(73, 183)
        Me.TableLayoutPanel33.Name = "TableLayoutPanel33"
        Me.TableLayoutPanel33.RowCount = 1
        Me.TableLayoutPanel33.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel33.Size = New System.Drawing.Size(418, 42)
        Me.TableLayoutPanel33.TabIndex = 10
        '
        'cmdPreview
        '
        Me.cmdPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdPreview.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdPreview.Location = New System.Drawing.Point(3, 3)
        Me.cmdPreview.Name = "cmdPreview"
        Me.cmdPreview.Size = New System.Drawing.Size(120, 36)
        Me.cmdPreview.TabIndex = 1
        Me.cmdPreview.Text = "プレビュー(&P)"
        Me.cmdPreview.UseVisualStyleBackColor = True
        '
        'cmdBack
        '
        Me.cmdBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdBack.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdBack.Location = New System.Drawing.Point(295, 3)
        Me.cmdBack.Name = "cmdBack"
        Me.cmdBack.Size = New System.Drawing.Size(120, 36)
        Me.cmdBack.TabIndex = 3
        Me.cmdBack.Text = "戻る(&B)"
        Me.cmdBack.UseVisualStyleBackColor = True
        '
        'cmdPrint
        '
        Me.cmdPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdPrint.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdPrint.Location = New System.Drawing.Point(151, 3)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.Size = New System.Drawing.Size(120, 36)
        Me.cmdPrint.TabIndex = 2
        Me.cmdPrint.Text = "印刷(&D)"
        Me.cmdPrint.UseVisualStyleBackColor = True
        '
        'lblDenStrTo
        '
        Me.lblDenStrTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblDenStrTo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDenStrTo.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDenStrTo.Location = New System.Drawing.Point(351, 122)
        Me.lblDenStrTo.Margin = New System.Windows.Forms.Padding(0)
        Me.lblDenStrTo.Name = "lblDenStrTo"
        Me.lblDenStrTo.Size = New System.Drawing.Size(19, 22)
        Me.lblDenStrTo.TabIndex = 42
        Me.lblDenStrTo.Text = "R"
        Me.lblDenStrTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDenNoTo
        '
        Me.txtDenNoTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDenNoTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDenNoTo.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtDenNoTo.Location = New System.Drawing.Point(371, 122)
        Me.txtDenNoTo.Margin = New System.Windows.Forms.Padding(0)
        Me.txtDenNoTo.MaxLength = 6
        Me.txtDenNoTo.Name = "txtDenNoTo"
        Me.txtDenNoTo.Size = New System.Drawing.Size(100, 22)
        Me.txtDenNoTo.TabIndex = 4
        Me.txtDenNoTo.Text = "999999"
        Me.txtDenNoTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(299, 122)
        Me.Label4.Margin = New System.Windows.Forms.Padding(0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(42, 22)
        Me.Label4.TabIndex = 41
        Me.Label4.Text = "～"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDenStrFrom
        '
        Me.lblDenStrFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblDenStrFrom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDenStrFrom.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDenStrFrom.Location = New System.Drawing.Point(170, 122)
        Me.lblDenStrFrom.Margin = New System.Windows.Forms.Padding(0)
        Me.lblDenStrFrom.Name = "lblDenStrFrom"
        Me.lblDenStrFrom.Size = New System.Drawing.Size(19, 22)
        Me.lblDenStrFrom.TabIndex = 40
        Me.lblDenStrFrom.Text = "R"
        Me.lblDenStrFrom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDenNoFrom
        '
        Me.txtDenNoFrom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDenNoFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDenNoFrom.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtDenNoFrom.Location = New System.Drawing.Point(191, 122)
        Me.txtDenNoFrom.Margin = New System.Windows.Forms.Padding(0)
        Me.txtDenNoFrom.MaxLength = 6
        Me.txtDenNoFrom.Name = "txtDenNoFrom"
        Me.txtDenNoFrom.Size = New System.Drawing.Size(100, 22)
        Me.txtDenNoFrom.TabIndex = 3
        Me.txtDenNoFrom.Text = "999999"
        Me.txtDenNoFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblSiirebiWeekTo
        '
        Me.lblSiirebiWeekTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblSiirebiWeekTo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSiirebiWeekTo.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSiirebiWeekTo.Location = New System.Drawing.Point(456, 65)
        Me.lblSiirebiWeekTo.Margin = New System.Windows.Forms.Padding(0)
        Me.lblSiirebiWeekTo.Name = "lblSiirebiWeekTo"
        Me.lblSiirebiWeekTo.Size = New System.Drawing.Size(19, 22)
        Me.lblSiirebiWeekTo.TabIndex = 39
        Me.lblSiirebiWeekTo.Text = "木"
        Me.lblSiirebiWeekTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(299, 66)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 22)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "～"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSiirebiWeekFrom
        '
        Me.lblSiirebiWeekFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblSiirebiWeekFrom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSiirebiWeekFrom.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSiirebiWeekFrom.Location = New System.Drawing.Point(275, 66)
        Me.lblSiirebiWeekFrom.Margin = New System.Windows.Forms.Padding(0)
        Me.lblSiirebiWeekFrom.Name = "lblSiirebiWeekFrom"
        Me.lblSiirebiWeekFrom.Size = New System.Drawing.Size(19, 22)
        Me.lblSiirebiWeekFrom.TabIndex = 37
        Me.lblSiirebiWeekFrom.Text = "木"
        Me.lblSiirebiWeekFrom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(90, 71)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(55, 15)
        Me.Label7.TabIndex = 73
        Me.Label7.Text = "仕入日"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(90, 125)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 15)
        Me.Label2.TabIndex = 74
        Me.Label2.Text = "伝票番号"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'frmH11F01_ShiireList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(564, 291)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.dtSiirebiTo)
        Me.Controls.Add(Me.dtSiirebiFrom)
        Me.Controls.Add(Me.TableLayoutPanel33)
        Me.Controls.Add(Me.lblDenStrTo)
        Me.Controls.Add(Me.txtDenNoTo)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblDenStrFrom)
        Me.Controls.Add(Me.txtDenNoFrom)
        Me.Controls.Add(Me.lblSiirebiWeekTo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblSiirebiWeekFrom)
        Me.Name = "frmH11F01_ShiireList"
        Me.Text = "仕入明細表出力指示（H11F01）"
        Me.TableLayoutPanel33.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtSiirebiTo As DateTimePicker
    Friend WithEvents dtSiirebiFrom As DateTimePicker
    Friend WithEvents TableLayoutPanel33 As TableLayoutPanel
    Friend WithEvents cmdPreview As Button
    Friend WithEvents cmdBack As Button
    Friend WithEvents cmdPrint As Button
    Friend WithEvents lblDenStrTo As Label
    Friend WithEvents txtDenNoTo As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents lblDenStrFrom As Label
    Friend WithEvents txtDenNoFrom As TextBox
    Friend WithEvents lblSiirebiWeekTo As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents lblSiirebiWeekFrom As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label2 As Label
End Class
