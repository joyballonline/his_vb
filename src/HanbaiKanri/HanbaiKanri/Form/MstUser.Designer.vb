<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MstUser
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
        Me.Dgv_User = New System.Windows.Forms.DataGridView()
        Me.会社コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ユーザID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.氏名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.略名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.備考 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.無効フラグ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.権限 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.言語 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.更新者 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.更新日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btn_userAdd = New System.Windows.Forms.Button()
        Me.btn_selectedRow = New System.Windows.Forms.Button()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.BtnSearch = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Search = New System.Windows.Forms.TextBox()
        CType(Me.Dgv_User, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Dgv_User
        '
        Me.Dgv_User.AllowUserToAddRows = False
        Me.Dgv_User.AllowUserToDeleteRows = False
        Me.Dgv_User.AllowUserToResizeColumns = False
        Me.Dgv_User.AllowUserToResizeRows = False
        Me.Dgv_User.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Dgv_User.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.会社コード, Me.ユーザID, Me.氏名, Me.略名, Me.備考, Me.無効フラグ, Me.権限, Me.言語, Me.更新者, Me.更新日})
        Me.Dgv_User.Location = New System.Drawing.Point(12, 34)
        Me.Dgv_User.Name = "Dgv_User"
        Me.Dgv_User.ReadOnly = True
        Me.Dgv_User.RowHeadersVisible = False
        Me.Dgv_User.RowTemplate.Height = 21
        Me.Dgv_User.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Dgv_User.Size = New System.Drawing.Size(1326, 469)
        Me.Dgv_User.TabIndex = 3
        '
        '会社コード
        '
        Me.会社コード.HeaderText = "会社コード"
        Me.会社コード.Name = "会社コード"
        Me.会社コード.ReadOnly = True
        '
        'ユーザID
        '
        Me.ユーザID.HeaderText = "ユーザID"
        Me.ユーザID.Name = "ユーザID"
        Me.ユーザID.ReadOnly = True
        '
        '氏名
        '
        Me.氏名.HeaderText = "氏名"
        Me.氏名.Name = "氏名"
        Me.氏名.ReadOnly = True
        '
        '略名
        '
        Me.略名.HeaderText = "略名"
        Me.略名.Name = "略名"
        Me.略名.ReadOnly = True
        '
        '備考
        '
        Me.備考.HeaderText = "備考"
        Me.備考.Name = "備考"
        Me.備考.ReadOnly = True
        '
        '無効フラグ
        '
        Me.無効フラグ.HeaderText = "無効フラグ"
        Me.無効フラグ.Name = "無効フラグ"
        Me.無効フラグ.ReadOnly = True
        '
        '権限
        '
        Me.権限.HeaderText = "権限"
        Me.権限.Name = "権限"
        Me.権限.ReadOnly = True
        '
        '言語
        '
        Me.言語.HeaderText = "言語"
        Me.言語.Name = "言語"
        Me.言語.ReadOnly = True
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
        'btn_userAdd
        '
        Me.btn_userAdd.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn_userAdd.Location = New System.Drawing.Point(831, 509)
        Me.btn_userAdd.Name = "btn_userAdd"
        Me.btn_userAdd.Size = New System.Drawing.Size(165, 40)
        Me.btn_userAdd.TabIndex = 4
        Me.btn_userAdd.Text = "ユーザ追加"
        Me.btn_userAdd.UseVisualStyleBackColor = True
        '
        'btn_selectedRow
        '
        Me.btn_selectedRow.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btn_selectedRow.Location = New System.Drawing.Point(1002, 509)
        Me.btn_selectedRow.Name = "btn_selectedRow"
        Me.btn_selectedRow.Size = New System.Drawing.Size(165, 40)
        Me.btn_selectedRow.TabIndex = 5
        Me.btn_selectedRow.Text = "ユーザ編集"
        Me.btn_selectedRow.UseVisualStyleBackColor = True
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
        Me.BtnSearch.Location = New System.Drawing.Point(181, 5)
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
        Me.Label1.Size = New System.Drawing.Size(57, 15)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "ユーザID"
        '
        'Search
        '
        Me.Search.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Search.Location = New System.Drawing.Point(75, 6)
        Me.Search.Name = "Search"
        Me.Search.Size = New System.Drawing.Size(100, 22)
        Me.Search.TabIndex = 1
        '
        'MstUser
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Search)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.btn_selectedRow)
        Me.Controls.Add(Me.btn_userAdd)
        Me.Controls.Add(Me.Dgv_User)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "MstUser"
        Me.Text = "MstUser"
        CType(Me.Dgv_User, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Dgv_User As DataGridView
    Friend WithEvents btn_userAdd As Button
    Friend WithEvents btn_selectedRow As Button
    Friend WithEvents 会社コード As DataGridViewTextBoxColumn
    Friend WithEvents ユーザID As DataGridViewTextBoxColumn
    Friend WithEvents 氏名 As DataGridViewTextBoxColumn
    Friend WithEvents 略名 As DataGridViewTextBoxColumn
    Friend WithEvents 備考 As DataGridViewTextBoxColumn
    Friend WithEvents 無効フラグ As DataGridViewTextBoxColumn
    Friend WithEvents 権限 As DataGridViewTextBoxColumn
    Friend WithEvents 言語 As DataGridViewTextBoxColumn
    Friend WithEvents 更新者 As DataGridViewTextBoxColumn
    Friend WithEvents 更新日 As DataGridViewTextBoxColumn
    Friend WithEvents BtnBack As Button
    Friend WithEvents BtnSearch As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Search As TextBox
End Class
