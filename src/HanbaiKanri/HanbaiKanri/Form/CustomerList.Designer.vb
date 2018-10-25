<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CustomerList
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
        Me.btnBack = New System.Windows.Forms.Button()
        Me.BtnBillingCalculation = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DgvCustomer = New System.Windows.Forms.DataGridView()
        Me.得意先名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.得意先コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.受注金額計 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求金額計 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求残高 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.受注件数 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求件数 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.会社コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DgvCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnSearch
        '
        Me.BtnSearch.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnSearch.Location = New System.Drawing.Point(191, 9)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(75, 23)
        Me.BtnSearch.TabIndex = 2
        Me.BtnSearch.Text = "検索"
        Me.BtnSearch.UseVisualStyleBackColor = True
        Me.BtnSearch.Visible = False
        '
        'Search
        '
        Me.Search.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Search.Location = New System.Drawing.Point(85, 10)
        Me.Search.Name = "Search"
        Me.Search.Size = New System.Drawing.Size(100, 22)
        Me.Search.TabIndex = 1
        Me.Search.Visible = False
        '
        'btnBack
        '
        Me.btnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.Location = New System.Drawing.Point(1173, 235)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(165, 40)
        Me.btnBack.TabIndex = 5
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'BtnBillingCalculation
        '
        Me.BtnBillingCalculation.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBillingCalculation.Location = New System.Drawing.Point(1002, 235)
        Me.BtnBillingCalculation.Name = "BtnBillingCalculation"
        Me.BtnBillingCalculation.Size = New System.Drawing.Size(165, 40)
        Me.BtnBillingCalculation.TabIndex = 4
        Me.BtnBillingCalculation.Text = "請求計算"
        Me.BtnBillingCalculation.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 15)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "得意先名"
        Me.Label1.Visible = False
        '
        'DgvCustomer
        '
        Me.DgvCustomer.AllowUserToAddRows = False
        Me.DgvCustomer.AllowUserToDeleteRows = False
        Me.DgvCustomer.AllowUserToResizeColumns = False
        Me.DgvCustomer.AllowUserToResizeRows = False
        Me.DgvCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvCustomer.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.得意先名, Me.得意先コード, Me.受注金額計, Me.請求金額計, Me.請求残高, Me.受注件数, Me.請求件数, Me.会社コード})
        Me.DgvCustomer.Location = New System.Drawing.Point(12, 38)
        Me.DgvCustomer.MultiSelect = False
        Me.DgvCustomer.Name = "DgvCustomer"
        Me.DgvCustomer.ReadOnly = True
        Me.DgvCustomer.RowHeadersVisible = False
        Me.DgvCustomer.RowTemplate.Height = 21
        Me.DgvCustomer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvCustomer.Size = New System.Drawing.Size(1326, 191)
        Me.DgvCustomer.TabIndex = 3
        '
        '得意先名
        '
        Me.得意先名.HeaderText = "得意先名"
        Me.得意先名.Name = "得意先名"
        Me.得意先名.ReadOnly = True
        Me.得意先名.Width = 200
        '
        '得意先コード
        '
        Me.得意先コード.HeaderText = "得意先コード"
        Me.得意先コード.Name = "得意先コード"
        Me.得意先コード.ReadOnly = True
        Me.得意先コード.Visible = False
        '
        '受注金額計
        '
        Me.受注金額計.HeaderText = "受注金額計"
        Me.受注金額計.Name = "受注金額計"
        Me.受注金額計.ReadOnly = True
        Me.受注金額計.Width = 200
        '
        '請求金額計
        '
        Me.請求金額計.HeaderText = "請求金額計"
        Me.請求金額計.Name = "請求金額計"
        Me.請求金額計.ReadOnly = True
        Me.請求金額計.Width = 200
        '
        '請求残高
        '
        Me.請求残高.HeaderText = "請求残高"
        Me.請求残高.Name = "請求残高"
        Me.請求残高.ReadOnly = True
        Me.請求残高.Width = 200
        '
        '受注件数
        '
        Me.受注件数.HeaderText = "受注件数"
        Me.受注件数.Name = "受注件数"
        Me.受注件数.ReadOnly = True
        Me.受注件数.Visible = False
        Me.受注件数.Width = 200
        '
        '請求件数
        '
        Me.請求件数.HeaderText = "請求件数"
        Me.請求件数.Name = "請求件数"
        Me.請求件数.ReadOnly = True
        Me.請求件数.Visible = False
        Me.請求件数.Width = 200
        '
        '会社コード
        '
        Me.会社コード.HeaderText = "会社コード"
        Me.会社コード.Name = "会社コード"
        Me.会社コード.ReadOnly = True
        Me.会社コード.Visible = False
        '
        'CustomerList
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 287)
        Me.Controls.Add(Me.DgvCustomer)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.Search)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.BtnBillingCalculation)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "CustomerList"
        Me.Text = "CustomerList"
        CType(Me.DgvCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnSearch As Button
    Friend WithEvents Search As TextBox
    Friend WithEvents btnBack As Button
    Friend WithEvents BtnBillingCalculation As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents DgvCustomer As DataGridView
    Friend WithEvents 得意先名 As DataGridViewTextBoxColumn
    Friend WithEvents 得意先コード As DataGridViewTextBoxColumn
    Friend WithEvents 受注金額計 As DataGridViewTextBoxColumn
    Friend WithEvents 請求金額計 As DataGridViewTextBoxColumn
    Friend WithEvents 請求残高 As DataGridViewTextBoxColumn
    Friend WithEvents 受注件数 As DataGridViewTextBoxColumn
    Friend WithEvents 請求件数 As DataGridViewTextBoxColumn
    Friend WithEvents 会社コード As DataGridViewTextBoxColumn
End Class
