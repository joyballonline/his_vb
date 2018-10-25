<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MstHanyou
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
        Me.Dgv_Hanyo = New System.Windows.Forms.DataGridView()
        Me.会社コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.固定キー = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.可変キー = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.表示順 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.文字１ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.文字２ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.文字３ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.文字４ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.文字５ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.文字６ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.数値１ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.数値２ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.数値３ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.数値４ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.数値５ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.数値６ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.メモ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.更新者 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.更新日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BtnSelect = New System.Windows.Forms.Button()
        Me.BtnAdd = New System.Windows.Forms.Button()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.BtnSearch = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Search = New System.Windows.Forms.TextBox()
        CType(Me.Dgv_Hanyo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Dgv_Hanyo
        '
        Me.Dgv_Hanyo.AllowUserToAddRows = False
        Me.Dgv_Hanyo.AllowUserToDeleteRows = False
        Me.Dgv_Hanyo.AllowUserToResizeColumns = False
        Me.Dgv_Hanyo.AllowUserToResizeRows = False
        Me.Dgv_Hanyo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Dgv_Hanyo.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.会社コード, Me.固定キー, Me.可変キー, Me.表示順, Me.文字１, Me.文字２, Me.文字３, Me.文字４, Me.文字５, Me.文字６, Me.数値１, Me.数値２, Me.数値３, Me.数値４, Me.数値５, Me.数値６, Me.メモ, Me.更新者, Me.更新日})
        Me.Dgv_Hanyo.Location = New System.Drawing.Point(12, 31)
        Me.Dgv_Hanyo.Name = "Dgv_Hanyo"
        Me.Dgv_Hanyo.ReadOnly = True
        Me.Dgv_Hanyo.RowHeadersVisible = False
        Me.Dgv_Hanyo.RowTemplate.Height = 21
        Me.Dgv_Hanyo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Dgv_Hanyo.Size = New System.Drawing.Size(1326, 472)
        Me.Dgv_Hanyo.TabIndex = 3
        '
        '会社コード
        '
        Me.会社コード.HeaderText = "会社コード"
        Me.会社コード.Name = "会社コード"
        Me.会社コード.ReadOnly = True
        '
        '固定キー
        '
        Me.固定キー.HeaderText = "固定キー"
        Me.固定キー.Name = "固定キー"
        Me.固定キー.ReadOnly = True
        '
        '可変キー
        '
        Me.可変キー.HeaderText = "可変キー"
        Me.可変キー.Name = "可変キー"
        Me.可変キー.ReadOnly = True
        '
        '表示順
        '
        Me.表示順.HeaderText = "表示順"
        Me.表示順.Name = "表示順"
        Me.表示順.ReadOnly = True
        '
        '文字１
        '
        Me.文字１.HeaderText = "文字１"
        Me.文字１.Name = "文字１"
        Me.文字１.ReadOnly = True
        '
        '文字２
        '
        Me.文字２.HeaderText = "文字２"
        Me.文字２.Name = "文字２"
        Me.文字２.ReadOnly = True
        '
        '文字３
        '
        Me.文字３.HeaderText = "文字３"
        Me.文字３.Name = "文字３"
        Me.文字３.ReadOnly = True
        '
        '文字４
        '
        Me.文字４.HeaderText = "文字４"
        Me.文字４.Name = "文字４"
        Me.文字４.ReadOnly = True
        '
        '文字５
        '
        Me.文字５.HeaderText = "文字５"
        Me.文字５.Name = "文字５"
        Me.文字５.ReadOnly = True
        '
        '文字６
        '
        Me.文字６.HeaderText = "文字６"
        Me.文字６.Name = "文字６"
        Me.文字６.ReadOnly = True
        '
        '数値１
        '
        Me.数値１.HeaderText = "数値１"
        Me.数値１.Name = "数値１"
        Me.数値１.ReadOnly = True
        '
        '数値２
        '
        Me.数値２.HeaderText = "数値２"
        Me.数値２.Name = "数値２"
        Me.数値２.ReadOnly = True
        '
        '数値３
        '
        Me.数値３.HeaderText = "数値３"
        Me.数値３.Name = "数値３"
        Me.数値３.ReadOnly = True
        '
        '数値４
        '
        Me.数値４.HeaderText = "数値４"
        Me.数値４.Name = "数値４"
        Me.数値４.ReadOnly = True
        '
        '数値５
        '
        Me.数値５.HeaderText = "数値５"
        Me.数値５.Name = "数値５"
        Me.数値５.ReadOnly = True
        '
        '数値６
        '
        Me.数値６.HeaderText = "数値６"
        Me.数値６.Name = "数値６"
        Me.数値６.ReadOnly = True
        '
        'メモ
        '
        Me.メモ.HeaderText = "メモ"
        Me.メモ.Name = "メモ"
        Me.メモ.ReadOnly = True
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
        'BtnSelect
        '
        Me.BtnSelect.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnSelect.Location = New System.Drawing.Point(1002, 509)
        Me.BtnSelect.Name = "BtnSelect"
        Me.BtnSelect.Size = New System.Drawing.Size(165, 40)
        Me.BtnSelect.TabIndex = 5
        Me.BtnSelect.Text = "編集"
        Me.BtnSelect.UseVisualStyleBackColor = True
        '
        'BtnAdd
        '
        Me.BtnAdd.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnAdd.Location = New System.Drawing.Point(831, 509)
        Me.BtnAdd.Name = "BtnAdd"
        Me.BtnAdd.Size = New System.Drawing.Size(165, 40)
        Me.BtnAdd.TabIndex = 4
        Me.BtnAdd.Text = "追加"
        Me.BtnAdd.UseVisualStyleBackColor = True
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(1173, 509)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 6
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'BtnSearch
        '
        Me.BtnSearch.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnSearch.Location = New System.Drawing.Point(185, 5)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(75, 23)
        Me.BtnSearch.TabIndex = 2
        Me.BtnSearch.Text = "検索"
        Me.BtnSearch.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 15)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "固定キー"
        '
        'Search
        '
        Me.Search.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Search.Location = New System.Drawing.Point(79, 6)
        Me.Search.Name = "Search"
        Me.Search.Size = New System.Drawing.Size(100, 22)
        Me.Search.TabIndex = 1
        '
        'MstHanyou
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Search)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.BtnAdd)
        Me.Controls.Add(Me.BtnSelect)
        Me.Controls.Add(Me.Dgv_Hanyo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "MstHanyou"
        Me.Text = "MstHanyou"
        CType(Me.Dgv_Hanyo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Dgv_Hanyo As DataGridView
    Friend WithEvents 会社コード As DataGridViewTextBoxColumn
    Friend WithEvents 固定キー As DataGridViewTextBoxColumn
    Friend WithEvents 可変キー As DataGridViewTextBoxColumn
    Friend WithEvents 表示順 As DataGridViewTextBoxColumn
    Friend WithEvents 文字１ As DataGridViewTextBoxColumn
    Friend WithEvents 文字２ As DataGridViewTextBoxColumn
    Friend WithEvents 文字３ As DataGridViewTextBoxColumn
    Friend WithEvents 文字４ As DataGridViewTextBoxColumn
    Friend WithEvents 文字５ As DataGridViewTextBoxColumn
    Friend WithEvents 文字６ As DataGridViewTextBoxColumn
    Friend WithEvents 数値１ As DataGridViewTextBoxColumn
    Friend WithEvents 数値２ As DataGridViewTextBoxColumn
    Friend WithEvents 数値３ As DataGridViewTextBoxColumn
    Friend WithEvents 数値４ As DataGridViewTextBoxColumn
    Friend WithEvents 数値５ As DataGridViewTextBoxColumn
    Friend WithEvents 数値６ As DataGridViewTextBoxColumn
    Friend WithEvents メモ As DataGridViewTextBoxColumn
    Friend WithEvents 更新者 As DataGridViewTextBoxColumn
    Friend WithEvents 更新日 As DataGridViewTextBoxColumn
    Friend WithEvents BtnSelect As Button
    Friend WithEvents BtnAdd As Button
    Friend WithEvents BtnBack As Button
    Friend WithEvents BtnSearch As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Search As TextBox
End Class
