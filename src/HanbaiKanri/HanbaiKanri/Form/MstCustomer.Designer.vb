<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MstCustomer
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
        Me.Dgv_Customer = New System.Windows.Forms.DataGridView()
        Me.btnCustomerEdit = New System.Windows.Forms.Button()
        Me.btnCustomerAdd = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.BtnSearch = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtSearch = New System.Windows.Forms.TextBox()
        Me.会社コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.得意先コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.得意先名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.得意先名略称 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.郵便番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.住所１ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.住所２ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.住所３ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.電話番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.電話番号検索用 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FAX番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.担当者名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.担当者役職 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.既定支払条件 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.メモ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.会計用得意先コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.更新者 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.更新日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.Dgv_Customer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Dgv_Customer
        '
        Me.Dgv_Customer.AllowUserToAddRows = False
        Me.Dgv_Customer.AllowUserToDeleteRows = False
        Me.Dgv_Customer.AllowUserToResizeColumns = False
        Me.Dgv_Customer.AllowUserToResizeRows = False
        Me.Dgv_Customer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Dgv_Customer.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.会社コード, Me.得意先コード, Me.得意先名, Me.得意先名略称, Me.郵便番号, Me.住所１, Me.住所２, Me.住所３, Me.電話番号, Me.電話番号検索用, Me.FAX番号, Me.担当者名, Me.担当者役職, Me.既定支払条件, Me.メモ, Me.会計用得意先コード, Me.更新者, Me.更新日})
        Me.Dgv_Customer.Location = New System.Drawing.Point(12, 33)
        Me.Dgv_Customer.MultiSelect = False
        Me.Dgv_Customer.Name = "Dgv_Customer"
        Me.Dgv_Customer.ReadOnly = True
        Me.Dgv_Customer.RowHeadersVisible = False
        Me.Dgv_Customer.RowTemplate.Height = 21
        Me.Dgv_Customer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Dgv_Customer.Size = New System.Drawing.Size(1326, 470)
        Me.Dgv_Customer.TabIndex = 3
        '
        'btnCustomerEdit
        '
        Me.btnCustomerEdit.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCustomerEdit.Location = New System.Drawing.Point(1002, 509)
        Me.btnCustomerEdit.Name = "btnCustomerEdit"
        Me.btnCustomerEdit.Size = New System.Drawing.Size(165, 40)
        Me.btnCustomerEdit.TabIndex = 5
        Me.btnCustomerEdit.Text = "得意先編集"
        Me.btnCustomerEdit.UseVisualStyleBackColor = True
        '
        'btnCustomerAdd
        '
        Me.btnCustomerAdd.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCustomerAdd.Location = New System.Drawing.Point(831, 509)
        Me.btnCustomerAdd.Name = "btnCustomerAdd"
        Me.btnCustomerAdd.Size = New System.Drawing.Size(165, 40)
        Me.btnCustomerAdd.TabIndex = 4
        Me.btnCustomerAdd.Text = "得意先追加"
        Me.btnCustomerAdd.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.Location = New System.Drawing.Point(1173, 509)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(165, 40)
        Me.btnBack.TabIndex = 6
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = True
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 15)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "得意先名"
        '
        'TxtSearch
        '
        Me.TxtSearch.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSearch.Location = New System.Drawing.Point(85, 6)
        Me.TxtSearch.Name = "TxtSearch"
        Me.TxtSearch.Size = New System.Drawing.Size(100, 22)
        Me.TxtSearch.TabIndex = 1
        '
        '会社コード
        '
        Me.会社コード.HeaderText = "会社コード"
        Me.会社コード.Name = "会社コード"
        Me.会社コード.ReadOnly = True
        '
        '得意先コード
        '
        Me.得意先コード.HeaderText = "得意先コード"
        Me.得意先コード.Name = "得意先コード"
        Me.得意先コード.ReadOnly = True
        '
        '得意先名
        '
        Me.得意先名.HeaderText = "得意先名"
        Me.得意先名.Name = "得意先名"
        Me.得意先名.ReadOnly = True
        '
        '得意先名略称
        '
        Me.得意先名略称.HeaderText = "得意先名略称"
        Me.得意先名略称.Name = "得意先名略称"
        Me.得意先名略称.ReadOnly = True
        '
        '郵便番号
        '
        Me.郵便番号.HeaderText = "郵便番号"
        Me.郵便番号.Name = "郵便番号"
        Me.郵便番号.ReadOnly = True
        '
        '住所１
        '
        Me.住所１.HeaderText = "住所１"
        Me.住所１.Name = "住所１"
        Me.住所１.ReadOnly = True
        '
        '住所２
        '
        Me.住所２.HeaderText = "住所２"
        Me.住所２.Name = "住所２"
        Me.住所２.ReadOnly = True
        '
        '住所３
        '
        Me.住所３.HeaderText = "住所３"
        Me.住所３.Name = "住所３"
        Me.住所３.ReadOnly = True
        '
        '電話番号
        '
        Me.電話番号.HeaderText = "電話番号"
        Me.電話番号.Name = "電話番号"
        Me.電話番号.ReadOnly = True
        '
        '電話番号検索用
        '
        Me.電話番号検索用.HeaderText = "電話番号検索用"
        Me.電話番号検索用.Name = "電話番号検索用"
        Me.電話番号検索用.ReadOnly = True
        '
        'FAX番号
        '
        Me.FAX番号.HeaderText = "FAX番号"
        Me.FAX番号.Name = "FAX番号"
        Me.FAX番号.ReadOnly = True
        '
        '担当者名
        '
        Me.担当者名.HeaderText = "担当者名"
        Me.担当者名.Name = "担当者名"
        Me.担当者名.ReadOnly = True
        '
        '担当者役職
        '
        Me.担当者役職.HeaderText = "担当者役職"
        Me.担当者役職.Name = "担当者役職"
        Me.担当者役職.ReadOnly = True
        '
        '既定支払条件
        '
        Me.既定支払条件.HeaderText = "既定支払条件"
        Me.既定支払条件.Name = "既定支払条件"
        Me.既定支払条件.ReadOnly = True
        '
        'メモ
        '
        Me.メモ.HeaderText = "メモ"
        Me.メモ.Name = "メモ"
        Me.メモ.ReadOnly = True
        '
        '会計用得意先コード
        '
        Me.会計用得意先コード.HeaderText = "会計用得意先コード"
        Me.会計用得意先コード.Name = "会計用得意先コード"
        Me.会計用得意先コード.ReadOnly = True
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
        'MstCustomer
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtSearch)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnCustomerAdd)
        Me.Controls.Add(Me.btnCustomerEdit)
        Me.Controls.Add(Me.Dgv_Customer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "MstCustomer"
        Me.Text = "MstCustomer"
        CType(Me.Dgv_Customer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Dgv_Customer As DataGridView
    Friend WithEvents btnCustomerEdit As Button
    Friend WithEvents btnCustomerAdd As Button
    Friend WithEvents btnBack As Button
    Friend WithEvents BtnSearch As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents TxtSearch As TextBox
    Friend WithEvents 会社コード As DataGridViewTextBoxColumn
    Friend WithEvents 得意先コード As DataGridViewTextBoxColumn
    Friend WithEvents 得意先名 As DataGridViewTextBoxColumn
    Friend WithEvents 得意先名略称 As DataGridViewTextBoxColumn
    Friend WithEvents 郵便番号 As DataGridViewTextBoxColumn
    Friend WithEvents 住所１ As DataGridViewTextBoxColumn
    Friend WithEvents 住所２ As DataGridViewTextBoxColumn
    Friend WithEvents 住所３ As DataGridViewTextBoxColumn
    Friend WithEvents 電話番号 As DataGridViewTextBoxColumn
    Friend WithEvents 電話番号検索用 As DataGridViewTextBoxColumn
    Friend WithEvents FAX番号 As DataGridViewTextBoxColumn
    Friend WithEvents 担当者名 As DataGridViewTextBoxColumn
    Friend WithEvents 担当者役職 As DataGridViewTextBoxColumn
    Friend WithEvents 既定支払条件 As DataGridViewTextBoxColumn
    Friend WithEvents メモ As DataGridViewTextBoxColumn
    Friend WithEvents 会計用得意先コード As DataGridViewTextBoxColumn
    Friend WithEvents 更新者 As DataGridViewTextBoxColumn
    Friend WithEvents 更新日 As DataGridViewTextBoxColumn
End Class
