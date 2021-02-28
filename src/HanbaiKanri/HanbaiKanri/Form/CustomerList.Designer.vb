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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BtnSearch = New System.Windows.Forms.Button()
        Me.TxtSearch = New System.Windows.Forms.TextBox()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.BtnBillingCalculation = New System.Windows.Forms.Button()
        Me.LblCustomerName = New System.Windows.Forms.Label()
        Me.DgvCustomer = New System.Windows.Forms.DataGridView()
        Me.得意先コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.得意先名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.通貨_外貨 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.受注金額計_外貨 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求金額計_外貨 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VAT_OUT計_外貨 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求残高_外貨 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.通貨 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.受注金額計 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求金額計 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求残高 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.受注件数 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求件数 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.会社コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.通貨_外貨コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DgvCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnSearch
        '
        Me.BtnSearch.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnSearch.Location = New System.Drawing.Point(239, 9)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(75, 23)
        Me.BtnSearch.TabIndex = 2
        Me.BtnSearch.Text = "検索"
        Me.BtnSearch.UseVisualStyleBackColor = True
        '
        'TxtSearch
        '
        Me.TxtSearch.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSearch.Location = New System.Drawing.Point(133, 10)
        Me.TxtSearch.Name = "TxtSearch"
        Me.TxtSearch.Size = New System.Drawing.Size(100, 22)
        Me.TxtSearch.TabIndex = 1
        '
        'btnBack
        '
        Me.btnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.Location = New System.Drawing.Point(1173, 509)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(165, 40)
        Me.btnBack.TabIndex = 5
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'BtnBillingCalculation
        '
        Me.BtnBillingCalculation.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBillingCalculation.Location = New System.Drawing.Point(1002, 509)
        Me.BtnBillingCalculation.Name = "BtnBillingCalculation"
        Me.BtnBillingCalculation.Size = New System.Drawing.Size(165, 40)
        Me.BtnBillingCalculation.TabIndex = 4
        Me.BtnBillingCalculation.Text = "請求計算"
        Me.BtnBillingCalculation.UseVisualStyleBackColor = True
        '
        'LblCustomerName
        '
        Me.LblCustomerName.AutoSize = True
        Me.LblCustomerName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCustomerName.Location = New System.Drawing.Point(12, 13)
        Me.LblCustomerName.Name = "LblCustomerName"
        Me.LblCustomerName.Size = New System.Drawing.Size(67, 15)
        Me.LblCustomerName.TabIndex = 18
        Me.LblCustomerName.Text = "得意先名"
        '
        'DgvCustomer
        '
        Me.DgvCustomer.AllowUserToAddRows = False
        Me.DgvCustomer.AllowUserToDeleteRows = False
        Me.DgvCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvCustomer.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.得意先コード, Me.得意先名, Me.通貨_外貨, Me.受注金額計_外貨, Me.請求金額計_外貨, Me.VAT_OUT計_外貨, Me.請求残高_外貨, Me.通貨, Me.受注金額計, Me.請求金額計, Me.請求残高, Me.受注件数, Me.請求件数, Me.会社コード, Me.通貨_外貨コード})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvCustomer.DefaultCellStyle = DataGridViewCellStyle4
        Me.DgvCustomer.Location = New System.Drawing.Point(12, 38)
        Me.DgvCustomer.MultiSelect = False
        Me.DgvCustomer.Name = "DgvCustomer"
        Me.DgvCustomer.ReadOnly = True
        Me.DgvCustomer.RowHeadersVisible = False
        Me.DgvCustomer.RowTemplate.Height = 21
        Me.DgvCustomer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvCustomer.Size = New System.Drawing.Size(1326, 465)
        Me.DgvCustomer.TabIndex = 3
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
        Me.得意先名.Width = 200
        '
        '通貨_外貨
        '
        Me.通貨_外貨.HeaderText = "販売通貨"
        Me.通貨_外貨.Name = "通貨_外貨"
        Me.通貨_外貨.ReadOnly = True
        '
        '受注金額計_外貨
        '
        Me.受注金額計_外貨.HeaderText = "受注金額計(外貨)"
        Me.受注金額計_外貨.Name = "受注金額計_外貨"
        Me.受注金額計_外貨.ReadOnly = True
        Me.受注金額計_外貨.Visible = False
        '
        '請求金額計_外貨
        '
        Me.請求金額計_外貨.HeaderText = "請求金額計(外貨)"
        Me.請求金額計_外貨.Name = "請求金額計_外貨"
        Me.請求金額計_外貨.ReadOnly = True
        '
        'VAT_OUT計_外貨
        '
        Me.VAT_OUT計_外貨.HeaderText = "VAT-OUT_外貨"
        Me.VAT_OUT計_外貨.Name = "VAT_OUT計_外貨"
        Me.VAT_OUT計_外貨.ReadOnly = True
        '
        '請求残高_外貨
        '
        Me.請求残高_外貨.HeaderText = "請求残高(外貨)"
        Me.請求残高_外貨.Name = "請求残高_外貨"
        Me.請求残高_外貨.ReadOnly = True
        Me.請求残高_外貨.Visible = False
        '
        '通貨
        '
        Me.通貨.HeaderText = "通貨"
        Me.通貨.Name = "通貨"
        Me.通貨.ReadOnly = True
        Me.通貨.Visible = False
        '
        '受注金額計
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.受注金額計.DefaultCellStyle = DataGridViewCellStyle1
        Me.受注金額計.HeaderText = "受注金額計"
        Me.受注金額計.Name = "受注金額計"
        Me.受注金額計.ReadOnly = True
        Me.受注金額計.Visible = False
        Me.受注金額計.Width = 200
        '
        '請求金額計
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.請求金額計.DefaultCellStyle = DataGridViewCellStyle2
        Me.請求金額計.HeaderText = "請求金額計"
        Me.請求金額計.Name = "請求金額計"
        Me.請求金額計.ReadOnly = True
        Me.請求金額計.Visible = False
        Me.請求金額計.Width = 200
        '
        '請求残高
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.請求残高.DefaultCellStyle = DataGridViewCellStyle3
        Me.請求残高.HeaderText = "請求残高"
        Me.請求残高.Name = "請求残高"
        Me.請求残高.ReadOnly = True
        Me.請求残高.Visible = False
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
        '通貨_外貨コード
        '
        Me.通貨_外貨コード.HeaderText = "通貨_外貨コード"
        Me.通貨_外貨コード.Name = "通貨_外貨コード"
        Me.通貨_外貨コード.ReadOnly = True
        Me.通貨_外貨コード.Visible = False
        '
        'CustomerList
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.DgvCustomer)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.TxtSearch)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.BtnBillingCalculation)
        Me.Controls.Add(Me.LblCustomerName)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "CustomerList"
        Me.Text = "CustomerList"
        CType(Me.DgvCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnSearch As Button
    Friend WithEvents TxtSearch As TextBox
    Friend WithEvents btnBack As Button
    Friend WithEvents BtnBillingCalculation As Button
    Friend WithEvents LblCustomerName As Label
    Friend WithEvents DgvCustomer As DataGridView
    Friend WithEvents 得意先コード As DataGridViewTextBoxColumn
    Friend WithEvents 得意先名 As DataGridViewTextBoxColumn
    Friend WithEvents 通貨_外貨 As DataGridViewTextBoxColumn
    Friend WithEvents 受注金額計_外貨 As DataGridViewTextBoxColumn
    Friend WithEvents 請求金額計_外貨 As DataGridViewTextBoxColumn
    Friend WithEvents VAT_OUT計_外貨 As DataGridViewTextBoxColumn
    Friend WithEvents 請求残高_外貨 As DataGridViewTextBoxColumn
    Friend WithEvents 通貨 As DataGridViewTextBoxColumn
    Friend WithEvents 受注金額計 As DataGridViewTextBoxColumn
    Friend WithEvents 請求金額計 As DataGridViewTextBoxColumn
    Friend WithEvents 請求残高 As DataGridViewTextBoxColumn
    Friend WithEvents 受注件数 As DataGridViewTextBoxColumn
    Friend WithEvents 請求件数 As DataGridViewTextBoxColumn
    Friend WithEvents 会社コード As DataGridViewTextBoxColumn
    Friend WithEvents 通貨_外貨コード As DataGridViewTextBoxColumn
End Class
