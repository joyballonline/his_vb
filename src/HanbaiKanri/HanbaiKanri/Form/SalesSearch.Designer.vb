<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SalesSearch
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
        Me.BtnSearch = New System.Windows.Forms.Button()
        Me.Search = New System.Windows.Forms.TextBox()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.BtnSelect = New System.Windows.Forms.Button()
        Me.更新日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.更新者 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.言語 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.無効フラグ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.備考 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.略名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.氏名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ユーザID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.会社コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.権限 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DgvUser = New System.Windows.Forms.DataGridView()
        CType(Me.DgvUser, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnSearch
        '
        Me.BtnSearch.Location = New System.Drawing.Point(180, 4)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(75, 23)
        Me.BtnSearch.TabIndex = 28
        Me.BtnSearch.Text = "検索"
        Me.BtnSearch.UseVisualStyleBackColor = True
        '
        'Search
        '
        Me.Search.Location = New System.Drawing.Point(74, 6)
        Me.Search.Name = "Search"
        Me.Search.Size = New System.Drawing.Size(100, 19)
        Me.Search.TabIndex = 26
        '
        'BtnBack
        '
        Me.BtnBack.Location = New System.Drawing.Point(1263, 259)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(75, 23)
        Me.BtnBack.TabIndex = 25
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'BtnSelect
        '
        Me.BtnSelect.Location = New System.Drawing.Point(1182, 259)
        Me.BtnSelect.Name = "BtnSelect"
        Me.BtnSelect.Size = New System.Drawing.Size(75, 23)
        Me.BtnSelect.TabIndex = 24
        Me.BtnSelect.Text = "選択"
        Me.BtnSelect.UseVisualStyleBackColor = True
        '
        '更新日
        '
        Me.更新日.HeaderText = "更新日"
        Me.更新日.Name = "更新日"
        Me.更新日.ReadOnly = True
        '
        '更新者
        '
        Me.更新者.HeaderText = "更新者"
        Me.更新者.Name = "更新者"
        Me.更新者.ReadOnly = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 12)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "会社コード"
        '
        '言語
        '
        Me.言語.HeaderText = "言語"
        Me.言語.Name = "言語"
        Me.言語.ReadOnly = True
        '
        '無効フラグ
        '
        Me.無効フラグ.HeaderText = "無効フラグ"
        Me.無効フラグ.Name = "無効フラグ"
        Me.無効フラグ.ReadOnly = True
        '
        '備考
        '
        Me.備考.HeaderText = "備考"
        Me.備考.Name = "備考"
        Me.備考.ReadOnly = True
        '
        '略名
        '
        Me.略名.HeaderText = "略名"
        Me.略名.Name = "略名"
        Me.略名.ReadOnly = True
        '
        '氏名
        '
        Me.氏名.HeaderText = "氏名"
        Me.氏名.Name = "氏名"
        Me.氏名.ReadOnly = True
        '
        'ユーザID
        '
        Me.ユーザID.HeaderText = "ユーザID"
        Me.ユーザID.Name = "ユーザID"
        Me.ユーザID.ReadOnly = True
        '
        '会社コード
        '
        Me.会社コード.HeaderText = "会社コード"
        Me.会社コード.Name = "会社コード"
        Me.会社コード.ReadOnly = True
        '
        '権限
        '
        Me.権限.HeaderText = "権限"
        Me.権限.Name = "権限"
        Me.権限.ReadOnly = True
        '
        'DgvUser
        '
        Me.DgvUser.AllowUserToAddRows = False
        Me.DgvUser.AllowUserToDeleteRows = False
        Me.DgvUser.AllowUserToResizeColumns = False
        Me.DgvUser.AllowUserToResizeRows = False
        Me.DgvUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvUser.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.会社コード, Me.ユーザID, Me.氏名, Me.略名, Me.備考, Me.無効フラグ, Me.権限, Me.言語, Me.更新者, Me.更新日})
        Me.DgvUser.Location = New System.Drawing.Point(12, 33)
        Me.DgvUser.Name = "DgvUser"
        Me.DgvUser.ReadOnly = True
        Me.DgvUser.RowTemplate.Height = 21
        Me.DgvUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvUser.Size = New System.Drawing.Size(1326, 220)
        Me.DgvUser.TabIndex = 22
        '
        'SalesSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1350, 729)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.Search)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.BtnSelect)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DgvUser)
        Me.Name = "SalesSearch"
        Me.Text = "SalesSearch"
        CType(Me.DgvUser, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnSearch As Button
    Friend WithEvents Search As TextBox
    Friend WithEvents BtnBack As Button
    Friend WithEvents BtnSelect As Button
    Friend WithEvents 更新日 As DataGridViewTextBoxColumn
    Friend WithEvents 更新者 As DataGridViewTextBoxColumn
    Friend WithEvents Label1 As Label
    Friend WithEvents 言語 As DataGridViewTextBoxColumn
    Friend WithEvents 無効フラグ As DataGridViewTextBoxColumn
    Friend WithEvents 備考 As DataGridViewTextBoxColumn
    Friend WithEvents 略名 As DataGridViewTextBoxColumn
    Friend WithEvents 氏名 As DataGridViewTextBoxColumn
    Friend WithEvents ユーザID As DataGridViewTextBoxColumn
    Friend WithEvents 会社コード As DataGridViewTextBoxColumn
    Friend WithEvents 権限 As DataGridViewTextBoxColumn
    Friend WithEvents DgvUser As DataGridView
End Class
