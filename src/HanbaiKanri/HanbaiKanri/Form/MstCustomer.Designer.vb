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
        Me.既定支払条件 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.メモ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.更新者 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.更新日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnSelectCompany = New System.Windows.Forms.Button()
        Me.btnCustomerAdd = New System.Windows.Forms.Button()
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
        Me.Dgv_Customer.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.会社コード, Me.得意先コード, Me.得意先名, Me.得意先名略称, Me.郵便番号, Me.住所１, Me.住所２, Me.住所３, Me.電話番号, Me.電話番号検索用, Me.FAX番号, Me.担当者名, Me.既定支払条件, Me.メモ, Me.更新者, Me.更新日})
        Me.Dgv_Customer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Dgv_Customer.Location = New System.Drawing.Point(0, 0)
        Me.Dgv_Customer.MultiSelect = False
        Me.Dgv_Customer.Name = "Dgv_Customer"
        Me.Dgv_Customer.ReadOnly = True
        Me.Dgv_Customer.RowTemplate.Height = 21
        Me.Dgv_Customer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Dgv_Customer.Size = New System.Drawing.Size(1088, 450)
        Me.Dgv_Customer.TabIndex = 0
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
        'btnSelectCompany
        '
        Me.btnSelectCompany.Location = New System.Drawing.Point(12, 246)
        Me.btnSelectCompany.Name = "btnSelectCompany"
        Me.btnSelectCompany.Size = New System.Drawing.Size(75, 23)
        Me.btnSelectCompany.TabIndex = 1
        Me.btnSelectCompany.Text = "選択"
        Me.btnSelectCompany.UseVisualStyleBackColor = True
        '
        'btnCustomerAdd
        '
        Me.btnCustomerAdd.Location = New System.Drawing.Point(93, 246)
        Me.btnCustomerAdd.Name = "btnCustomerAdd"
        Me.btnCustomerAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnCustomerAdd.TabIndex = 2
        Me.btnCustomerAdd.Text = "追加"
        Me.btnCustomerAdd.UseVisualStyleBackColor = True
        '
        'MstCustomer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1088, 450)
        Me.Controls.Add(Me.btnCustomerAdd)
        Me.Controls.Add(Me.btnSelectCompany)
        Me.Controls.Add(Me.Dgv_Customer)
        Me.Name = "MstCustomer"
        Me.Text = "MstCustomer"
        CType(Me.Dgv_Customer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Dgv_Customer As DataGridView
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
    Friend WithEvents 既定支払条件 As DataGridViewTextBoxColumn
    Friend WithEvents メモ As DataGridViewTextBoxColumn
    Friend WithEvents 更新者 As DataGridViewTextBoxColumn
    Friend WithEvents 更新日 As DataGridViewTextBoxColumn
    Friend WithEvents btnSelectCompany As Button
    Friend WithEvents btnCustomerAdd As Button
End Class
