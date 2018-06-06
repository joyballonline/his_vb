<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ItemSearch
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
        Me.BtnSelectItem = New System.Windows.Forms.Button()
        Me.Dgv_Item = New System.Windows.Forms.DataGridView()
        Me.品名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.型式 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.Dgv_Item, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnSelectItem
        '
        Me.BtnSelectItem.Location = New System.Drawing.Point(437, 370)
        Me.BtnSelectItem.Name = "BtnSelectItem"
        Me.BtnSelectItem.Size = New System.Drawing.Size(75, 23)
        Me.BtnSelectItem.TabIndex = 3
        Me.BtnSelectItem.Text = "選択"
        Me.BtnSelectItem.UseVisualStyleBackColor = True
        '
        'Dgv_Item
        '
        Me.Dgv_Item.AllowUserToAddRows = False
        Me.Dgv_Item.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Dgv_Item.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.品名, Me.型式})
        Me.Dgv_Item.Location = New System.Drawing.Point(12, 12)
        Me.Dgv_Item.Name = "Dgv_Item"
        Me.Dgv_Item.RowTemplate.Height = 21
        Me.Dgv_Item.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Dgv_Item.Size = New System.Drawing.Size(500, 150)
        Me.Dgv_Item.TabIndex = 2
        '
        '品名
        '
        Me.品名.HeaderText = "品名"
        Me.品名.Name = "品名"
        '
        '型式
        '
        Me.型式.HeaderText = "型式"
        Me.型式.Name = "型式"
        '
        'ItemSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(524, 405)
        Me.Controls.Add(Me.BtnSelectItem)
        Me.Controls.Add(Me.Dgv_Item)
        Me.Name = "ItemSearch"
        Me.Text = "ItemSearch"
        CType(Me.Dgv_Item, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BtnSelectItem As Button
    Friend WithEvents Dgv_Item As DataGridView
    Friend WithEvents 品名 As DataGridViewTextBoxColumn
    Friend WithEvents 型式 As DataGridViewTextBoxColumn
End Class
