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
        Me.BtnEdit = New System.Windows.Forms.Button()
        Me.BtnAdd = New System.Windows.Forms.Button()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.BtnSearch = New System.Windows.Forms.Button()
        Me.TxtSearchString = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtSearchNumber = New System.Windows.Forms.TextBox()
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
        Me.Dgv_Hanyo.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.固定キー, Me.可変キー, Me.表示順, Me.文字１, Me.文字２, Me.文字３, Me.文字４, Me.文字５, Me.文字６, Me.数値１, Me.数値２, Me.数値３, Me.数値４, Me.数値５, Me.数値６, Me.メモ, Me.更新者, Me.更新日})
        Me.Dgv_Hanyo.Location = New System.Drawing.Point(12, 96)
        Me.Dgv_Hanyo.Name = "Dgv_Hanyo"
        Me.Dgv_Hanyo.ReadOnly = True
        Me.Dgv_Hanyo.RowHeadersVisible = False
        Me.Dgv_Hanyo.RowTemplate.Height = 21
        Me.Dgv_Hanyo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Dgv_Hanyo.Size = New System.Drawing.Size(1326, 407)
        Me.Dgv_Hanyo.TabIndex = 4
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
        'BtnEdit
        '
        Me.BtnEdit.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnEdit.Location = New System.Drawing.Point(1002, 509)
        Me.BtnEdit.Name = "BtnEdit"
        Me.BtnEdit.Size = New System.Drawing.Size(165, 40)
        Me.BtnEdit.TabIndex = 6
        Me.BtnEdit.Text = "編集"
        Me.BtnEdit.UseVisualStyleBackColor = True
        '
        'BtnAdd
        '
        Me.BtnAdd.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnAdd.Location = New System.Drawing.Point(831, 509)
        Me.BtnAdd.Name = "BtnAdd"
        Me.BtnAdd.Size = New System.Drawing.Size(165, 40)
        Me.BtnAdd.TabIndex = 5
        Me.BtnAdd.Text = "追加"
        Me.BtnAdd.UseVisualStyleBackColor = True
        Me.BtnAdd.Visible = False
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
        'BtnSearch
        '
        Me.BtnSearch.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnSearch.Location = New System.Drawing.Point(1182, 12)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(156, 40)
        Me.BtnSearch.TabIndex = 3
        Me.BtnSearch.Text = "検索"
        Me.BtnSearch.UseVisualStyleBackColor = True
        '
        'TxtSearchString
        '
        Me.TxtSearchString.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSearchString.Location = New System.Drawing.Point(66, 27)
        Me.TxtSearchString.Name = "TxtSearchString"
        Me.TxtSearchString.Size = New System.Drawing.Size(172, 22)
        Me.TxtSearchString.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(15, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 15)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "文字"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(15, 58)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 15)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "数字"
        '
        'TxtSearchNumber
        '
        Me.TxtSearchNumber.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSearchNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TxtSearchNumber.Location = New System.Drawing.Point(66, 55)
        Me.TxtSearchNumber.Name = "TxtSearchNumber"
        Me.TxtSearchNumber.Size = New System.Drawing.Size(172, 22)
        Me.TxtSearchNumber.TabIndex = 2
        '
        'MstHanyou
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtSearchNumber)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.TxtSearchString)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.BtnAdd)
        Me.Controls.Add(Me.BtnEdit)
        Me.Controls.Add(Me.Dgv_Hanyo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "MstHanyou"
        Me.Text = "MstHanyou"
        CType(Me.Dgv_Hanyo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Dgv_Hanyo As DataGridView
    Friend WithEvents BtnEdit As Button
    Friend WithEvents BtnAdd As Button
    Friend WithEvents BtnBack As Button
    Friend WithEvents BtnSearch As Button
    Friend WithEvents TxtSearchString As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TxtSearchNumber As TextBox
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
End Class
