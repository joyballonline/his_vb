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
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.DgvBilling = New System.Windows.Forms.DataGridView()
        Me.BtnSearch = New System.Windows.Forms.Button()
        Me.Search = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
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
        Me.BtnInvoice.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnInvoice.Location = New System.Drawing.Point(1002, 235)
        Me.BtnInvoice.Name = "BtnInvoice"
        Me.BtnInvoice.Size = New System.Drawing.Size(165, 40)
        Me.BtnInvoice.TabIndex = 4
        Me.BtnInvoice.Text = "請求書発行"
        Me.BtnInvoice.UseVisualStyleBackColor = True
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(1173, 235)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 5
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'DgvBilling
        '
        Me.DgvBilling.AllowUserToAddRows = False
        Me.DgvBilling.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvBilling.Location = New System.Drawing.Point(15, 34)
        Me.DgvBilling.Name = "DgvBilling"
        Me.DgvBilling.ReadOnly = True
        Me.DgvBilling.RowHeadersVisible = False
        Me.DgvBilling.RowTemplate.Height = 21
        Me.DgvBilling.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvBilling.Size = New System.Drawing.Size(1326, 195)
        Me.DgvBilling.TabIndex = 3
        '
        'BtnSearch
        '
        Me.BtnSearch.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnSearch.Location = New System.Drawing.Point(191, 5)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(75, 23)
        Me.BtnSearch.TabIndex = 2
        Me.BtnSearch.Text = "検索"
        Me.BtnSearch.UseVisualStyleBackColor = True
        '
        'Search
        '
        Me.Search.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Search.Location = New System.Drawing.Point(85, 6)
        Me.Search.Name = "Search"
        Me.Search.Size = New System.Drawing.Size(100, 22)
        Me.Search.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 15)
        Me.Label1.TabIndex = 156
        Me.Label1.Text = "得意先名"
        '
        'CustomerOrderList
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 287)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.Search)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DgvBilling)
        Me.Controls.Add(Me.ChkCancelData)
        Me.Controls.Add(Me.BtnInvoice)
        Me.Controls.Add(Me.BtnBack)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "CustomerOrderList"
        Me.Text = "CustomerOrderList"
        CType(Me.DgvBilling, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ChkCancelData As CheckBox
    Friend WithEvents BtnInvoice As Button
    Friend WithEvents BtnBack As Button
    Friend WithEvents DgvBilling As DataGridView
    Friend WithEvents BtnSearch As Button
    Friend WithEvents Search As TextBox
    Friend WithEvents Label1 As Label
End Class
