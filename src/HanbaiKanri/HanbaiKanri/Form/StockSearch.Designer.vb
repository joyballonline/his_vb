<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class StockSearch
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.DgvList = New System.Windows.Forms.DataGridView()
        Me.倉庫 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.数量 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(459, 396)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 5
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'DgvList
        '
        Me.DgvList.AllowUserToAddRows = False
        Me.DgvList.AllowUserToDeleteRows = False
        Me.DgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.倉庫, Me.数量})
        Me.DgvList.Location = New System.Drawing.Point(12, 12)
        Me.DgvList.Name = "DgvList"
        Me.DgvList.ReadOnly = True
        Me.DgvList.RowHeadersVisible = False
        Me.DgvList.RowTemplate.Height = 21
        Me.DgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvList.Size = New System.Drawing.Size(612, 367)
        Me.DgvList.TabIndex = 3
        '
        '倉庫
        '
        Me.倉庫.HeaderText = "倉庫"
        Me.倉庫.Name = "倉庫"
        Me.倉庫.ReadOnly = True
        '
        '数量
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.数量.DefaultCellStyle = DataGridViewCellStyle1
        Me.数量.HeaderText = "数量"
        Me.数量.Name = "数量"
        Me.数量.ReadOnly = True
        '
        'StockSearch
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(636, 448)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.DgvList)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "StockSearch"
        Me.Text = "StockSearch"
        CType(Me.DgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnBack As Button
    Friend WithEvents DgvList As DataGridView
    Friend WithEvents 倉庫 As DataGridViewTextBoxColumn
    Friend WithEvents 数量 As DataGridViewTextBoxColumn
End Class
