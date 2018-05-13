<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmH11F21_ShiireSokatuList
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
        Me.TableLayoutPanel33 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmdPreview = New System.Windows.Forms.Button()
        Me.cmdBack = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.txtSiireNendo = New CustomControl.TextBoxDate()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtNendo = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel33.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel33
        '
        Me.TableLayoutPanel33.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel33.ColumnCount = 3
        Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.97701!))
        Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.02299!))
        Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 146.0!))
        Me.TableLayoutPanel33.Controls.Add(Me.cmdPreview, 0, 0)
        Me.TableLayoutPanel33.Controls.Add(Me.cmdBack, 2, 0)
        Me.TableLayoutPanel33.Controls.Add(Me.cmdPrint, 1, 0)
        Me.TableLayoutPanel33.Location = New System.Drawing.Point(73, 185)
        Me.TableLayoutPanel33.Name = "TableLayoutPanel33"
        Me.TableLayoutPanel33.RowCount = 1
        Me.TableLayoutPanel33.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel33.Size = New System.Drawing.Size(418, 42)
        Me.TableLayoutPanel33.TabIndex = 11
        '
        'cmdPreview
        '
        Me.cmdPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdPreview.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdPreview.Location = New System.Drawing.Point(3, 3)
        Me.cmdPreview.Name = "cmdPreview"
        Me.cmdPreview.Size = New System.Drawing.Size(119, 36)
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
        Me.cmdPrint.Location = New System.Drawing.Point(148, 3)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.Size = New System.Drawing.Size(120, 36)
        Me.cmdPrint.TabIndex = 2
        Me.cmdPrint.Text = "印刷(&D)"
        Me.cmdPrint.UseVisualStyleBackColor = True
        '
        'txtSiireNendo
        '
        Me.txtSiireNendo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtSiireNendo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSiireNendo.HideSelection = False
        Me.txtSiireNendo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSiireNendo.Location = New System.Drawing.Point(308, 61)
        Me.txtSiireNendo.Margin = New System.Windows.Forms.Padding(0)
        Me.txtSiireNendo.Mask = "0000"
        Me.txtSiireNendo.Name = "txtSiireNendo"
        Me.txtSiireNendo.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57), Global.Microsoft.VisualBasic.ChrW(47), Global.Microsoft.VisualBasic.ChrW(95)}
        Me.txtSiireNendo.Size = New System.Drawing.Size(96, 22)
        Me.txtSiireNendo.TabIndex = 1
        Me.txtSiireNendo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtSiireNendo.ValidatingType = GetType(Date)
        Me.txtSiireNendo.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(74, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 15)
        Me.Label2.TabIndex = 48
        Me.Label2.Text = "仕入年度"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtNendo
        '
        Me.txtNendo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNendo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNendo.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtNendo.Location = New System.Drawing.Point(166, 61)
        Me.txtNendo.Margin = New System.Windows.Forms.Padding(0)
        Me.txtNendo.MaxLength = 4
        Me.txtNendo.Name = "txtNendo"
        Me.txtNendo.Size = New System.Drawing.Size(65, 22)
        Me.txtNendo.TabIndex = 0
        Me.txtNendo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'frmH11F21_ShiireSokatuList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(564, 291)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtNendo)
        Me.Controls.Add(Me.txtSiireNendo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TableLayoutPanel33)
        Me.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.Name = "frmH11F21_ShiireSokatuList"
        Me.Text = "仕入総括表出力指示（H11F21）"
        Me.TableLayoutPanel33.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TableLayoutPanel33 As TableLayoutPanel
    Friend WithEvents cmdPreview As Button
    Friend WithEvents cmdBack As Button
    Friend WithEvents cmdPrint As Button
    Friend WithEvents txtSiireNendo As CustomControl.TextBoxDate
    Friend WithEvents Label2 As Label
    Friend WithEvents txtNendo As TextBox
End Class
