<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmH10F01_ChumonList
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
        Me.gbBunrui = New System.Windows.Forms.GroupBox()
        Me.rbItaku = New System.Windows.Forms.RadioButton()
        Me.rbUriage = New System.Windows.Forms.RadioButton()
        Me.lblShukkabiWeekFrom = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblShukkabiWeekTo = New System.Windows.Forms.Label()
        Me.txtDenNoFrom = New System.Windows.Forms.TextBox()
        Me.lblDenStrFrom = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblDenStrTo = New System.Windows.Forms.Label()
        Me.txtDenNoTo = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel33 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmdPreview = New System.Windows.Forms.Button()
        Me.cmdBack = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.dtShukkabiFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtShukkabiTo = New System.Windows.Forms.DateTimePicker()
        Me.cbShukkabi = New System.Windows.Forms.CheckBox()
        Me.cbDenpyo = New System.Windows.Forms.CheckBox()
        Me.gbBunrui.SuspendLayout()
        Me.TableLayoutPanel33.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbBunrui
        '
        Me.gbBunrui.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.gbBunrui.Controls.Add(Me.rbItaku)
        Me.gbBunrui.Controls.Add(Me.rbUriage)
        Me.gbBunrui.Location = New System.Drawing.Point(73, 21)
        Me.gbBunrui.Margin = New System.Windows.Forms.Padding(0)
        Me.gbBunrui.Name = "gbBunrui"
        Me.gbBunrui.Padding = New System.Windows.Forms.Padding(0)
        Me.gbBunrui.Size = New System.Drawing.Size(264, 40)
        Me.gbBunrui.TabIndex = 20
        Me.gbBunrui.TabStop = False
        '
        'rbItaku
        '
        Me.rbItaku.AutoSize = True
        Me.rbItaku.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rbItaku.Location = New System.Drawing.Point(151, 13)
        Me.rbItaku.Name = "rbItaku"
        Me.rbItaku.Size = New System.Drawing.Size(85, 19)
        Me.rbItaku.TabIndex = 2
        Me.rbItaku.TabStop = True
        Me.rbItaku.Text = "委託注文"
        Me.rbItaku.UseVisualStyleBackColor = True
        '
        'rbUriage
        '
        Me.rbUriage.AutoSize = True
        Me.rbUriage.Checked = True
        Me.rbUriage.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rbUriage.Location = New System.Drawing.Point(34, 13)
        Me.rbUriage.Name = "rbUriage"
        Me.rbUriage.Size = New System.Drawing.Size(85, 19)
        Me.rbUriage.TabIndex = 1
        Me.rbUriage.TabStop = True
        Me.rbUriage.Text = "売上注文"
        Me.rbUriage.UseVisualStyleBackColor = True
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
        Me.lblShukkabiWeekFrom.TabIndex = 11
        Me.lblShukkabiWeekFrom.Text = "木"
        Me.lblShukkabiWeekFrom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(315, 92)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 22)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "～"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.lblShukkabiWeekTo.TabIndex = 14
        Me.lblShukkabiWeekTo.Text = "木"
        Me.lblShukkabiWeekTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDenNoFrom
        '
        Me.txtDenNoFrom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDenNoFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDenNoFrom.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtDenNoFrom.Location = New System.Drawing.Point(207, 148)
        Me.txtDenNoFrom.Margin = New System.Windows.Forms.Padding(0)
        Me.txtDenNoFrom.MaxLength = 6
        Me.txtDenNoFrom.Name = "txtDenNoFrom"
        Me.txtDenNoFrom.Size = New System.Drawing.Size(100, 22)
        Me.txtDenNoFrom.TabIndex = 3
        Me.txtDenNoFrom.Text = "999999"
        Me.txtDenNoFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblDenStrFrom
        '
        Me.lblDenStrFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblDenStrFrom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDenStrFrom.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDenStrFrom.Location = New System.Drawing.Point(186, 148)
        Me.lblDenStrFrom.Margin = New System.Windows.Forms.Padding(0)
        Me.lblDenStrFrom.Name = "lblDenStrFrom"
        Me.lblDenStrFrom.Size = New System.Drawing.Size(19, 22)
        Me.lblDenStrFrom.TabIndex = 17
        Me.lblDenStrFrom.Text = "U"
        Me.lblDenStrFrom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(315, 148)
        Me.Label4.Margin = New System.Windows.Forms.Padding(0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(42, 22)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "～"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDenStrTo
        '
        Me.lblDenStrTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblDenStrTo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDenStrTo.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDenStrTo.Location = New System.Drawing.Point(367, 148)
        Me.lblDenStrTo.Margin = New System.Windows.Forms.Padding(0)
        Me.lblDenStrTo.Name = "lblDenStrTo"
        Me.lblDenStrTo.Size = New System.Drawing.Size(19, 22)
        Me.lblDenStrTo.TabIndex = 20
        Me.lblDenStrTo.Text = "U"
        Me.lblDenStrTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDenNoTo
        '
        Me.txtDenNoTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDenNoTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDenNoTo.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtDenNoTo.Location = New System.Drawing.Point(387, 148)
        Me.txtDenNoTo.Margin = New System.Windows.Forms.Padding(0)
        Me.txtDenNoTo.MaxLength = 6
        Me.txtDenNoTo.Name = "txtDenNoTo"
        Me.txtDenNoTo.Size = New System.Drawing.Size(100, 22)
        Me.txtDenNoTo.TabIndex = 4
        Me.txtDenNoTo.Text = "999999"
        Me.txtDenNoTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
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
        Me.TableLayoutPanel33.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
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
        'dtShukkabiFrom
        '
        Me.dtShukkabiFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtShukkabiFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtShukkabiFrom.Location = New System.Drawing.Point(186, 91)
        Me.dtShukkabiFrom.Name = "dtShukkabiFrom"
        Me.dtShukkabiFrom.Size = New System.Drawing.Size(104, 22)
        Me.dtShukkabiFrom.TabIndex = 0
        '
        'dtShukkabiTo
        '
        Me.dtShukkabiTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtShukkabiTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtShukkabiTo.Location = New System.Drawing.Point(367, 91)
        Me.dtShukkabiTo.Name = "dtShukkabiTo"
        Me.dtShukkabiTo.Size = New System.Drawing.Size(104, 22)
        Me.dtShukkabiTo.TabIndex = 1
        '
        'cbShukkabi
        '
        Me.cbShukkabi.AutoSize = True
        Me.cbShukkabi.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cbShukkabi.Location = New System.Drawing.Point(90, 96)
        Me.cbShukkabi.Name = "cbShukkabi"
        Me.cbShukkabi.Size = New System.Drawing.Size(74, 19)
        Me.cbShukkabi.TabIndex = 30
        Me.cbShukkabi.Text = "出荷日"
        Me.cbShukkabi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cbShukkabi.UseVisualStyleBackColor = True
        '
        'cbDenpyo
        '
        Me.cbDenpyo.AutoSize = True
        Me.cbDenpyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cbDenpyo.Location = New System.Drawing.Point(90, 152)
        Me.cbDenpyo.Name = "cbDenpyo"
        Me.cbDenpyo.Size = New System.Drawing.Size(90, 19)
        Me.cbDenpyo.TabIndex = 2
        Me.cbDenpyo.Text = "伝票番号"
        Me.cbDenpyo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cbDenpyo.UseVisualStyleBackColor = True
        '
        'frmH10F01_ChumonList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(564, 291)
        Me.ControlBox = False
        Me.Controls.Add(Me.cbDenpyo)
        Me.Controls.Add(Me.cbShukkabi)
        Me.Controls.Add(Me.dtShukkabiTo)
        Me.Controls.Add(Me.dtShukkabiFrom)
        Me.Controls.Add(Me.TableLayoutPanel33)
        Me.Controls.Add(Me.lblDenStrTo)
        Me.Controls.Add(Me.txtDenNoTo)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblDenStrFrom)
        Me.Controls.Add(Me.txtDenNoFrom)
        Me.Controls.Add(Me.lblShukkabiWeekTo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblShukkabiWeekFrom)
        Me.Controls.Add(Me.gbBunrui)
        Me.Name = "frmH10F01_ChumonList"
        Me.Text = "注文明細出力指示（H10F01）"
        Me.gbBunrui.ResumeLayout(False)
        Me.gbBunrui.PerformLayout()
        Me.TableLayoutPanel33.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents gbBunrui As GroupBox
    Friend WithEvents lblShukkabiWeekFrom As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents lblShukkabiWeekTo As Label
    Friend WithEvents txtDenNoFrom As TextBox
    Friend WithEvents lblDenStrFrom As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents lblDenStrTo As Label
    Friend WithEvents txtDenNoTo As TextBox
    Friend WithEvents TableLayoutPanel33 As TableLayoutPanel
    Friend WithEvents cmdPreview As Button
    Friend WithEvents cmdBack As Button
    Friend WithEvents cmdPrint As Button
    Friend WithEvents dtShukkabiFrom As DateTimePicker
    Friend WithEvents dtShukkabiTo As DateTimePicker
    Friend WithEvents cbShukkabi As CheckBox
    Friend WithEvents cbDenpyo As CheckBox
    Friend WithEvents rbItaku As RadioButton
    Friend WithEvents rbUriage As RadioButton
End Class
