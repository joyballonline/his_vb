<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MakerSearch
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
        Me.BtnSelect = New System.Windows.Forms.Button()
        Me.LbManufacturer = New System.Windows.Forms.ListBox()
        Me.LbItemName = New System.Windows.Forms.ListBox()
        Me.LbSpec = New System.Windows.Forms.ListBox()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.LblManufacturer = New System.Windows.Forms.Label()
        Me.LblItemName = New System.Windows.Forms.Label()
        Me.LblSpec = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TxtManufacturer = New System.Windows.Forms.TextBox()
        Me.TxtItemName = New System.Windows.Forms.TextBox()
        Me.TxtSpec = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnSelect
        '
        Me.BtnSelect.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnSelect.Location = New System.Drawing.Point(288, 396)
        Me.BtnSelect.Name = "BtnSelect"
        Me.BtnSelect.Size = New System.Drawing.Size(165, 40)
        Me.BtnSelect.TabIndex = 8
        Me.BtnSelect.Text = "選択"
        Me.BtnSelect.UseVisualStyleBackColor = True
        '
        'LbManufacturer
        '
        Me.LbManufacturer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LbManufacturer.FormattingEnabled = True
        Me.LbManufacturer.ItemHeight = 12
        Me.LbManufacturer.Location = New System.Drawing.Point(3, 58)
        Me.LbManufacturer.Name = "LbManufacturer"
        Me.LbManufacturer.Size = New System.Drawing.Size(198, 316)
        Me.LbManufacturer.TabIndex = 5
        '
        'LbItemName
        '
        Me.LbItemName.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LbItemName.FormattingEnabled = True
        Me.LbItemName.ItemHeight = 12
        Me.LbItemName.Location = New System.Drawing.Point(207, 58)
        Me.LbItemName.Name = "LbItemName"
        Me.LbItemName.Size = New System.Drawing.Size(198, 316)
        Me.LbItemName.TabIndex = 6
        '
        'LbSpec
        '
        Me.LbSpec.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LbSpec.FormattingEnabled = True
        Me.LbSpec.ItemHeight = 12
        Me.LbSpec.Location = New System.Drawing.Point(411, 58)
        Me.LbSpec.Name = "LbSpec"
        Me.LbSpec.Size = New System.Drawing.Size(198, 316)
        Me.LbSpec.TabIndex = 7
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(459, 396)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 9
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'LblManufacturer
        '
        Me.LblManufacturer.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblManufacturer.AutoSize = True
        Me.LblManufacturer.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblManufacturer.Location = New System.Drawing.Point(3, 5)
        Me.LblManufacturer.Name = "LblManufacturer"
        Me.LblManufacturer.Size = New System.Drawing.Size(52, 15)
        Me.LblManufacturer.TabIndex = 27
        Me.LblManufacturer.Text = "メーカー"
        Me.LblManufacturer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblItemName
        '
        Me.LblItemName.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblItemName.AutoSize = True
        Me.LblItemName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblItemName.Location = New System.Drawing.Point(207, 5)
        Me.LblItemName.Name = "LblItemName"
        Me.LblItemName.Size = New System.Drawing.Size(37, 15)
        Me.LblItemName.TabIndex = 28
        Me.LblItemName.Text = "品名"
        '
        'LblSpec
        '
        Me.LblSpec.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblSpec.AutoSize = True
        Me.LblSpec.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSpec.Location = New System.Drawing.Point(411, 5)
        Me.LblSpec.Name = "LblSpec"
        Me.LblSpec.Size = New System.Drawing.Size(37, 15)
        Me.LblSpec.TabIndex = 29
        Me.LblSpec.Text = "型式"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.Controls.Add(Me.TxtManufacturer, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LblSpec, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LbManufacturer, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LblItemName, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LbItemName, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LblManufacturer, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LbSpec, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TxtItemName, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TxtSpec, 2, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(12, 12)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.878307!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.201058!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.18519!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(612, 378)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'TxtManufacturer
        '
        Me.TxtManufacturer.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtManufacturer.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtManufacturer.Location = New System.Drawing.Point(3, 28)
        Me.TxtManufacturer.MaxLength = 50
        Me.TxtManufacturer.Name = "TxtManufacturer"
        Me.TxtManufacturer.Size = New System.Drawing.Size(198, 23)
        Me.TxtManufacturer.TabIndex = 2
        '
        'TxtItemName
        '
        Me.TxtItemName.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtItemName.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtItemName.Location = New System.Drawing.Point(207, 28)
        Me.TxtItemName.MaxLength = 50
        Me.TxtItemName.Name = "TxtItemName"
        Me.TxtItemName.Size = New System.Drawing.Size(198, 23)
        Me.TxtItemName.TabIndex = 3
        '
        'TxtSpec
        '
        Me.TxtSpec.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtSpec.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSpec.Location = New System.Drawing.Point(411, 28)
        Me.TxtSpec.MaxLength = 50
        Me.TxtSpec.Name = "TxtSpec"
        Me.TxtSpec.Size = New System.Drawing.Size(198, 23)
        Me.TxtSpec.TabIndex = 4
        '
        'MakerSearch
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(636, 448)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.BtnSelect)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "MakerSearch"
        Me.Text = "MakerSearch"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnSelect As Button
    Friend WithEvents LbManufacturer As ListBox
    Friend WithEvents LbItemName As ListBox
    Friend WithEvents LbSpec As ListBox
    Friend WithEvents BtnBack As Button
    Friend WithEvents LblManufacturer As Label
    Friend WithEvents LblItemName As Label
    Friend WithEvents LblSpec As Label
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TxtManufacturer As TextBox
    Friend WithEvents TxtItemName As TextBox
    Friend WithEvents TxtSpec As TextBox
End Class
