<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CustomerOrderList
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
        Me.ChkCancelData = New System.Windows.Forms.CheckBox()
        Me.BtnInvoice = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.DgvBilling = New System.Windows.Forms.DataGridView()
        CType(Me.DgvBilling, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ChkCancelData
        '
        Me.ChkCancelData.AutoSize = True
        Me.ChkCancelData.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ChkCancelData.Location = New System.Drawing.Point(15, 40)
        Me.ChkCancelData.Name = "ChkCancelData"
        Me.ChkCancelData.Size = New System.Drawing.Size(139, 19)
        Me.ChkCancelData.TabIndex = 153
        Me.ChkCancelData.Text = "取消データを含める"
        Me.ChkCancelData.UseVisualStyleBackColor = True
        Me.ChkCancelData.Visible = False
        '
        'BtnInvoice
        '
        Me.BtnInvoice.Location = New System.Drawing.Point(1002, 649)
        Me.BtnInvoice.Name = "BtnInvoice"
        Me.BtnInvoice.Size = New System.Drawing.Size(165, 40)
        Me.BtnInvoice.TabIndex = 152
        Me.BtnInvoice.Text = "請求書発行"
        Me.BtnInvoice.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(12, 9)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(87, 15)
        Me.Label10.TabIndex = 151
        Me.Label10.Text = "■表示形式"
        Me.Label10.Visible = False
        '
        'BtnBack
        '
        Me.BtnBack.Location = New System.Drawing.Point(1173, 649)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 150
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'DgvBilling
        '
        Me.DgvBilling.AllowUserToAddRows = False
        Me.DgvBilling.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvBilling.Location = New System.Drawing.Point(12, 9)
        Me.DgvBilling.Name = "DgvBilling"
        Me.DgvBilling.ReadOnly = True
        Me.DgvBilling.RowHeadersVisible = False
        Me.DgvBilling.RowTemplate.Height = 21
        Me.DgvBilling.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvBilling.Size = New System.Drawing.Size(1326, 634)
        Me.DgvBilling.TabIndex = 149
        '
        'CustomerOrderList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1350, 701)
        Me.Controls.Add(Me.DgvBilling)
        Me.Controls.Add(Me.ChkCancelData)
        Me.Controls.Add(Me.BtnInvoice)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.BtnBack)
        Me.Name = "CustomerOrderList"
        Me.Text = "CustomerOrderList"
        CType(Me.DgvBilling, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ChkCancelData As CheckBox
    Friend WithEvents BtnInvoice As Button
    Friend WithEvents Label10 As Label
    Friend WithEvents BtnBack As Button
    Friend WithEvents DgvBilling As DataGridView
End Class
