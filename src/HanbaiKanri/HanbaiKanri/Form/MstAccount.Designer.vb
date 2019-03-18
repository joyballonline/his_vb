<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MstAccount
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
        Me.Dgv_Account = New System.Windows.Forms.DataGridView()
        Me.勘定科目コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.勘定科目名称１ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.勘定科目名称２ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.勘定科目名称３ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.会計用勘定科目コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.備考 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.有効区分 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.更新者 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.更新日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnAccountAdd = New System.Windows.Forms.Button()
        Me.btnAccountEdit = New System.Windows.Forms.Button()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.LblAccountName = New System.Windows.Forms.Label()
        Me.TxtSearch = New System.Windows.Forms.TextBox()
        Me.BtnSearch = New System.Windows.Forms.Button()
        CType(Me.Dgv_Account, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Dgv_Account
        '
        Me.Dgv_Account.AllowUserToAddRows = False
        Me.Dgv_Account.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Dgv_Account.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.勘定科目コード, Me.勘定科目名称１, Me.勘定科目名称２, Me.勘定科目名称３, Me.会計用勘定科目コード, Me.備考, Me.有効区分, Me.更新者, Me.更新日})
        Me.Dgv_Account.Location = New System.Drawing.Point(12, 33)
        Me.Dgv_Account.Name = "Dgv_Account"
        Me.Dgv_Account.ReadOnly = True
        Me.Dgv_Account.RowHeadersVisible = False
        Me.Dgv_Account.RowTemplate.Height = 21
        Me.Dgv_Account.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Dgv_Account.Size = New System.Drawing.Size(1326, 470)
        Me.Dgv_Account.TabIndex = 4
        '
        '勘定科目コード
        '
        Me.勘定科目コード.HeaderText = "勘定科目コード"
        Me.勘定科目コード.Name = "勘定科目コード"
        Me.勘定科目コード.ReadOnly = True
        '
        '勘定科目名称１
        '
        Me.勘定科目名称１.HeaderText = "勘定科目名称１"
        Me.勘定科目名称１.Name = "勘定科目名称１"
        Me.勘定科目名称１.ReadOnly = True
        '
        '勘定科目名称２
        '
        Me.勘定科目名称２.HeaderText = "勘定科目名称２"
        Me.勘定科目名称２.Name = "勘定科目名称２"
        Me.勘定科目名称２.ReadOnly = True
        '
        '勘定科目名称３
        '
        Me.勘定科目名称３.HeaderText = "勘定科目名称３"
        Me.勘定科目名称３.Name = "勘定科目名称３"
        Me.勘定科目名称３.ReadOnly = True
        '
        '会計用勘定科目コード
        '
        Me.会計用勘定科目コード.HeaderText = "会計用勘定科目コード"
        Me.会計用勘定科目コード.Name = "会計用勘定科目コード"
        Me.会計用勘定科目コード.ReadOnly = True
        '
        '備考
        '
        Me.備考.HeaderText = "備考"
        Me.備考.Name = "備考"
        Me.備考.ReadOnly = True
        '
        '有効区分
        '
        Me.有効区分.HeaderText = "有効区分"
        Me.有効区分.Name = "有効区分"
        Me.有効区分.ReadOnly = True
        '
        '更新者
        '
        Me.更新者.HeaderText = "更新者"
        Me.更新者.Name = "更新者"
        Me.更新者.ReadOnly = True
        '
        '更新日
        '
        Me.更新日.HeaderText = "更新日"
        Me.更新日.Name = "更新日"
        Me.更新日.ReadOnly = True
        '
        'btnAccountAdd
        '
        Me.btnAccountAdd.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAccountAdd.Location = New System.Drawing.Point(831, 509)
        Me.btnAccountAdd.Name = "btnAccountAdd"
        Me.btnAccountAdd.Size = New System.Drawing.Size(165, 40)
        Me.btnAccountAdd.TabIndex = 5
        Me.btnAccountAdd.Text = "勘定科目追加"
        Me.btnAccountAdd.UseVisualStyleBackColor = True
        Me.btnAccountAdd.Visible = False
        '
        'btnAccountEdit
        '
        Me.btnAccountEdit.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAccountEdit.Location = New System.Drawing.Point(1002, 509)
        Me.btnAccountEdit.Name = "btnAccountEdit"
        Me.btnAccountEdit.Size = New System.Drawing.Size(165, 40)
        Me.btnAccountEdit.TabIndex = 6
        Me.btnAccountEdit.Text = "勘定科目編集"
        Me.btnAccountEdit.UseVisualStyleBackColor = True
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(1173, 509)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 7
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'LblAccountName
        '
        Me.LblAccountName.AutoSize = True
        Me.LblAccountName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAccountName.Location = New System.Drawing.Point(12, 9)
        Me.LblAccountName.Name = "LblAccountName"
        Me.LblAccountName.Size = New System.Drawing.Size(82, 15)
        Me.LblAccountName.TabIndex = 9
        Me.LblAccountName.Text = "勘定科目名"
        '
        'TxtSearch
        '
        Me.TxtSearch.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSearch.Location = New System.Drawing.Point(104, 6)
        Me.TxtSearch.Name = "TxtSearch"
        Me.TxtSearch.Size = New System.Drawing.Size(142, 22)
        Me.TxtSearch.TabIndex = 10
        '
        'BtnSearch
        '
        Me.BtnSearch.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnSearch.Location = New System.Drawing.Point(252, 5)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(75, 23)
        Me.BtnSearch.TabIndex = 11
        Me.BtnSearch.Text = "検索"
        Me.BtnSearch.UseVisualStyleBackColor = True
        '
        'MstAccount
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.TxtSearch)
        Me.Controls.Add(Me.LblAccountName)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.btnAccountEdit)
        Me.Controls.Add(Me.btnAccountAdd)
        Me.Controls.Add(Me.Dgv_Account)
        Me.Name = "MstAccount"
        Me.Text = "MstAccountItem"
        CType(Me.Dgv_Account, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Dgv_Account As DataGridView
    Friend WithEvents btnAccountAdd As Button
    Friend WithEvents btnAccountEdit As Button
    Friend WithEvents BtnBack As Button
    Friend WithEvents LblAccountName As Label
    Friend WithEvents TxtSearch As TextBox
    Friend WithEvents BtnSearch As Button
    Friend WithEvents 勘定科目コード As DataGridViewTextBoxColumn
    Friend WithEvents 勘定科目名称１ As DataGridViewTextBoxColumn
    Friend WithEvents 勘定科目名称２ As DataGridViewTextBoxColumn
    Friend WithEvents 勘定科目名称３ As DataGridViewTextBoxColumn
    Friend WithEvents 会計用勘定科目コード As DataGridViewTextBoxColumn
    Friend WithEvents 備考 As DataGridViewTextBoxColumn
    Friend WithEvents 有効区分 As DataGridViewTextBoxColumn
    Friend WithEvents 更新者 As DataGridViewTextBoxColumn
    Friend WithEvents 更新日 As DataGridViewTextBoxColumn
End Class
