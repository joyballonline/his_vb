<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmH10F05_ShukkaSuList
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtShukkabi = New System.Windows.Forms.DateTimePicker()
        Me.TableLayoutPanel33 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmdPreview = New System.Windows.Forms.Button()
        Me.cmdBack = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.lblShukkabiWeekFrom = New System.Windows.Forms.Label()
        Me.cmbChohyo = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel33.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(73, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 15)
        Me.Label2.TabIndex = 52
        Me.Label2.Text = "出荷日"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'dtShukkabi
        '
        Me.dtShukkabi.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtShukkabi.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtShukkabi.Location = New System.Drawing.Point(154, 42)
        Me.dtShukkabi.Name = "dtShukkabi"
        Me.dtShukkabi.Size = New System.Drawing.Size(104, 22)
        Me.dtShukkabi.TabIndex = 0
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
        Me.TableLayoutPanel33.Location = New System.Drawing.Point(73, 210)
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
        'lblShukkabiWeekFrom
        '
        Me.lblShukkabiWeekFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblShukkabiWeekFrom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblShukkabiWeekFrom.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShukkabiWeekFrom.Location = New System.Drawing.Point(259, 42)
        Me.lblShukkabiWeekFrom.Margin = New System.Windows.Forms.Padding(0)
        Me.lblShukkabiWeekFrom.Name = "lblShukkabiWeekFrom"
        Me.lblShukkabiWeekFrom.Size = New System.Drawing.Size(19, 22)
        Me.lblShukkabiWeekFrom.TabIndex = 49
        Me.lblShukkabiWeekFrom.Text = "木"
        Me.lblShukkabiWeekFrom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbChohyo
        '
        Me.cmbChohyo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChohyo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbChohyo.FormattingEnabled = True
        Me.cmbChohyo.Items.AddRange(New Object() {"①東北陸送　イオン"})
        Me.cmbChohyo.Location = New System.Drawing.Point(154, 111)
        Me.cmbChohyo.Margin = New System.Windows.Forms.Padding(0)
        Me.cmbChohyo.Name = "cmbChohyo"
        Me.cmbChohyo.Size = New System.Drawing.Size(334, 23)
        Me.cmbChohyo.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(73, 114)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 15)
        Me.Label3.TabIndex = 54
        Me.Label3.Text = "出力帳票"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'frmH10F05_ShukkaSuList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(564, 291)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbChohyo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dtShukkabi)
        Me.Controls.Add(Me.TableLayoutPanel33)
        Me.Controls.Add(Me.lblShukkabiWeekFrom)
        Me.Name = "frmH10F05_ShukkaSuList"
        Me.Text = "出荷数一覧表出力指示（H10F05）"
        Me.TableLayoutPanel33.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label2 As Label
    Friend WithEvents dtShukkabi As DateTimePicker
    Friend WithEvents TableLayoutPanel33 As TableLayoutPanel
    Friend WithEvents cmdPreview As Button
    Friend WithEvents cmdBack As Button
    Friend WithEvents cmdPrint As Button
    Friend WithEvents lblShukkabiWeekFrom As Label
    Friend WithEvents cmbChohyo As ComboBox
    Friend WithEvents Label3 As Label
End Class
