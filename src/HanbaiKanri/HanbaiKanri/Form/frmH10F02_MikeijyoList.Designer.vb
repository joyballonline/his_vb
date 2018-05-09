<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmH10F02_MikeijyoList
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
        Me.dtShukkabiTo = New System.Windows.Forms.DateTimePicker()
        Me.dtShukkabiFrom = New System.Windows.Forms.DateTimePicker()
        Me.TableLayoutPanel33 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmdPreview = New System.Windows.Forms.Button()
        Me.cmdBack = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.lblShukkabiWeekTo = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblShukkabiWeekFrom = New System.Windows.Forms.Label()
        Me.gbBunrui = New System.Windows.Forms.GroupBox()
        Me.rbItaku = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel33.SuspendLayout()
        Me.gbBunrui.SuspendLayout()
        Me.SuspendLayout()
        '
        'dtShukkabiTo
        '
        Me.dtShukkabiTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtShukkabiTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtShukkabiTo.Location = New System.Drawing.Point(367, 91)
        Me.dtShukkabiTo.Name = "dtShukkabiTo"
        Me.dtShukkabiTo.Size = New System.Drawing.Size(104, 22)
        Me.dtShukkabiTo.TabIndex = 32
        '
        'dtShukkabiFrom
        '
        Me.dtShukkabiFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtShukkabiFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtShukkabiFrom.Location = New System.Drawing.Point(186, 91)
        Me.dtShukkabiFrom.Name = "dtShukkabiFrom"
        Me.dtShukkabiFrom.Size = New System.Drawing.Size(104, 22)
        Me.dtShukkabiFrom.TabIndex = 31
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
        Me.TableLayoutPanel33.Location = New System.Drawing.Point(89, 209)
        Me.TableLayoutPanel33.Name = "TableLayoutPanel33"
        Me.TableLayoutPanel33.RowCount = 1
        Me.TableLayoutPanel33.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel33.Size = New System.Drawing.Size(418, 42)
        Me.TableLayoutPanel33.TabIndex = 36
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
        'lblShukkabiWeekTo
        '
        Me.lblShukkabiWeekTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblShukkabiWeekTo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblShukkabiWeekTo.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShukkabiWeekTo.Location = New System.Drawing.Point(472, 91)
        Me.lblShukkabiWeekTo.Margin = New System.Windows.Forms.Padding(0)
        Me.lblShukkabiWeekTo.Name = "lblShukkabiWeekTo"
        Me.lblShukkabiWeekTo.Size = New System.Drawing.Size(19, 22)
        Me.lblShukkabiWeekTo.TabIndex = 39
        Me.lblShukkabiWeekTo.Text = "木"
        Me.lblShukkabiWeekTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(315, 92)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 22)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "～"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblShukkabiWeekFrom
        '
        Me.lblShukkabiWeekFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblShukkabiWeekFrom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblShukkabiWeekFrom.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShukkabiWeekFrom.Location = New System.Drawing.Point(291, 92)
        Me.lblShukkabiWeekFrom.Margin = New System.Windows.Forms.Padding(0)
        Me.lblShukkabiWeekFrom.Name = "lblShukkabiWeekFrom"
        Me.lblShukkabiWeekFrom.Size = New System.Drawing.Size(19, 22)
        Me.lblShukkabiWeekFrom.TabIndex = 37
        Me.lblShukkabiWeekFrom.Text = "木"
        Me.lblShukkabiWeekFrom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'gbBunrui
        '
        Me.gbBunrui.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.gbBunrui.Controls.Add(Me.rbItaku)
        Me.gbBunrui.Location = New System.Drawing.Point(73, 21)
        Me.gbBunrui.Margin = New System.Windows.Forms.Padding(0)
        Me.gbBunrui.Name = "gbBunrui"
        Me.gbBunrui.Padding = New System.Windows.Forms.Padding(0)
        Me.gbBunrui.Size = New System.Drawing.Size(264, 40)
        Me.gbBunrui.TabIndex = 43
        Me.gbBunrui.TabStop = False
        '
        'rbItaku
        '
        Me.rbItaku.AutoSize = True
        Me.rbItaku.Checked = True
        Me.rbItaku.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rbItaku.Location = New System.Drawing.Point(35, 13)
        Me.rbItaku.Name = "rbItaku"
        Me.rbItaku.Size = New System.Drawing.Size(85, 19)
        Me.rbItaku.TabIndex = 2
        Me.rbItaku.TabStop = True
        Me.rbItaku.Text = "委託注文"
        Me.rbItaku.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(105, 94)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 15)
        Me.Label2.TabIndex = 45
        Me.Label2.Text = "出荷日"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'frmH10F02_MikeijyoList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(564, 291)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dtShukkabiTo)
        Me.Controls.Add(Me.dtShukkabiFrom)
        Me.Controls.Add(Me.TableLayoutPanel33)
        Me.Controls.Add(Me.lblShukkabiWeekTo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblShukkabiWeekFrom)
        Me.Controls.Add(Me.gbBunrui)
        Me.Name = "frmH10F02_MikeijyoList"
        Me.Text = "売上未計上一覧出力指示（H10F02）"
        Me.TableLayoutPanel33.ResumeLayout(False)
        Me.gbBunrui.ResumeLayout(False)
        Me.gbBunrui.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtShukkabiTo As DateTimePicker
    Friend WithEvents dtShukkabiFrom As DateTimePicker
    Friend WithEvents TableLayoutPanel33 As TableLayoutPanel
    Friend WithEvents cmdPreview As Button
    Friend WithEvents cmdBack As Button
    Friend WithEvents cmdPrint As Button
    Friend WithEvents lblShukkabiWeekTo As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents lblShukkabiWeekFrom As Label
    Friend WithEvents gbBunrui As GroupBox
    Friend WithEvents rbItaku As RadioButton
    Friend WithEvents Label2 As Label
End Class
