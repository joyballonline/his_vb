<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SupplierSearch
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
        Me.btnSupplierSelect = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Dgv_Supplier = New System.Windows.Forms.DataGridView()
        Me.会社コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入先コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入先名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入先名略名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.関税率 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.前払法人税率 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.輸送費率 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.郵便番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.住所１ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.住所２ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.住所３ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.電話番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.電話番号検索用 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FAX番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.担当者名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.既定間接費率 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.メモ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.銀行コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.支店コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.預金種目 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.口座番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.口座名義 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.更新者 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.更新日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.担当者役職 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.Dgv_Supplier, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnSearch
        '
        Me.BtnSearch.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnSearch.Location = New System.Drawing.Point(209, 5)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(75, 23)
        Me.BtnSearch.TabIndex = 2
        Me.BtnSearch.Text = "検索"
        Me.BtnSearch.UseVisualStyleBackColor = True
        '
        'Search
        '
        Me.Search.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Search.Location = New System.Drawing.Point(103, 6)
        Me.Search.Name = "Search"
        Me.Search.Size = New System.Drawing.Size(100, 22)
        Me.Search.TabIndex = 1
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(1173, 235)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 5
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'btnSupplierSelect
        '
        Me.btnSupplierSelect.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSupplierSelect.Location = New System.Drawing.Point(1002, 235)
        Me.btnSupplierSelect.Name = "btnSupplierSelect"
        Me.btnSupplierSelect.Size = New System.Drawing.Size(165, 40)
        Me.btnSupplierSelect.TabIndex = 4
        Me.btnSupplierSelect.Text = "選択"
        Me.btnSupplierSelect.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 15)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "仕入先コード"
        '
        'Dgv_Supplier
        '
        Me.Dgv_Supplier.AllowUserToAddRows = False
        Me.Dgv_Supplier.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Dgv_Supplier.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.会社コード, Me.仕入先コード, Me.仕入先名, Me.仕入先名略名, Me.関税率, Me.前払法人税率, Me.輸送費率, Me.郵便番号, Me.住所１, Me.住所２, Me.住所３, Me.電話番号, Me.電話番号検索用, Me.FAX番号, Me.担当者名, Me.既定間接費率, Me.メモ, Me.銀行コード, Me.支店コード, Me.預金種目, Me.口座番号, Me.口座名義, Me.更新者, Me.更新日, Me.担当者役職})
        Me.Dgv_Supplier.Location = New System.Drawing.Point(12, 34)
        Me.Dgv_Supplier.Name = "Dgv_Supplier"
        Me.Dgv_Supplier.ReadOnly = True
        Me.Dgv_Supplier.RowHeadersVisible = False
        Me.Dgv_Supplier.RowTemplate.Height = 21
        Me.Dgv_Supplier.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Dgv_Supplier.Size = New System.Drawing.Size(1326, 195)
        Me.Dgv_Supplier.TabIndex = 3
        '
        '会社コード
        '
        Me.会社コード.HeaderText = "会社コード"
        Me.会社コード.Name = "会社コード"
        Me.会社コード.ReadOnly = True
        '
        '仕入先コード
        '
        Me.仕入先コード.HeaderText = "仕入先コード"
        Me.仕入先コード.Name = "仕入先コード"
        Me.仕入先コード.ReadOnly = True
        '
        '仕入先名
        '
        Me.仕入先名.HeaderText = "仕入先名"
        Me.仕入先名.Name = "仕入先名"
        Me.仕入先名.ReadOnly = True
        '
        '仕入先名略名
        '
        Me.仕入先名略名.HeaderText = "仕入先名略名"
        Me.仕入先名略名.Name = "仕入先名略名"
        Me.仕入先名略名.ReadOnly = True
        '
        '関税率
        '
        Me.関税率.HeaderText = "関税率"
        Me.関税率.Name = "関税率"
        Me.関税率.ReadOnly = True
        '
        '前払法人税率
        '
        Me.前払法人税率.HeaderText = "前払法人税率"
        Me.前払法人税率.Name = "前払法人税率"
        Me.前払法人税率.ReadOnly = True
        '
        '輸送費率
        '
        Me.輸送費率.HeaderText = "輸送費率"
        Me.輸送費率.Name = "輸送費率"
        Me.輸送費率.ReadOnly = True
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
        '既定間接費率
        '
        Me.既定間接費率.HeaderText = "既定間接費率"
        Me.既定間接費率.Name = "既定間接費率"
        Me.既定間接費率.ReadOnly = True
        '
        'メモ
        '
        Me.メモ.HeaderText = "メモ"
        Me.メモ.Name = "メモ"
        Me.メモ.ReadOnly = True
        '
        '銀行コード
        '
        Me.銀行コード.HeaderText = "銀行コード"
        Me.銀行コード.Name = "銀行コード"
        Me.銀行コード.ReadOnly = True
        '
        '支店コード
        '
        Me.支店コード.HeaderText = "支店コード"
        Me.支店コード.Name = "支店コード"
        Me.支店コード.ReadOnly = True
        '
        '預金種目
        '
        Me.預金種目.HeaderText = "預金種目"
        Me.預金種目.Name = "預金種目"
        Me.預金種目.ReadOnly = True
        '
        '口座番号
        '
        Me.口座番号.HeaderText = "口座番号"
        Me.口座番号.Name = "口座番号"
        Me.口座番号.ReadOnly = True
        '
        '口座名義
        '
        Me.口座名義.HeaderText = "口座名義"
        Me.口座名義.Name = "口座名義"
        Me.口座名義.ReadOnly = True
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
        '担当者役職
        '
        Me.担当者役職.HeaderText = "担当者役職"
        Me.担当者役職.Name = "担当者役職"
        Me.担当者役職.ReadOnly = True
        '
        'SupplierSearch
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 287)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.Search)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.btnSupplierSelect)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Dgv_Supplier)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "SupplierSearch"
        Me.Text = "SupplierSearch"
        CType(Me.Dgv_Supplier, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnSearch As Button
    Friend WithEvents Search As TextBox
    Friend WithEvents BtnBack As Button
    Friend WithEvents btnSupplierSelect As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Dgv_Supplier As DataGridView
    Friend WithEvents 会社コード As DataGridViewTextBoxColumn
    Friend WithEvents 仕入先コード As DataGridViewTextBoxColumn
    Friend WithEvents 仕入先名 As DataGridViewTextBoxColumn
    Friend WithEvents 仕入先名略名 As DataGridViewTextBoxColumn
    Friend WithEvents 関税率 As DataGridViewTextBoxColumn
    Friend WithEvents 前払法人税率 As DataGridViewTextBoxColumn
    Friend WithEvents 輸送費率 As DataGridViewTextBoxColumn
    Friend WithEvents 郵便番号 As DataGridViewTextBoxColumn
    Friend WithEvents 住所１ As DataGridViewTextBoxColumn
    Friend WithEvents 住所２ As DataGridViewTextBoxColumn
    Friend WithEvents 住所３ As DataGridViewTextBoxColumn
    Friend WithEvents 電話番号 As DataGridViewTextBoxColumn
    Friend WithEvents 電話番号検索用 As DataGridViewTextBoxColumn
    Friend WithEvents FAX番号 As DataGridViewTextBoxColumn
    Friend WithEvents 担当者名 As DataGridViewTextBoxColumn
    Friend WithEvents 既定間接費率 As DataGridViewTextBoxColumn
    Friend WithEvents メモ As DataGridViewTextBoxColumn
    Friend WithEvents 銀行コード As DataGridViewTextBoxColumn
    Friend WithEvents 支店コード As DataGridViewTextBoxColumn
    Friend WithEvents 預金種目 As DataGridViewTextBoxColumn
    Friend WithEvents 口座番号 As DataGridViewTextBoxColumn
    Friend WithEvents 口座名義 As DataGridViewTextBoxColumn
    Friend WithEvents 更新者 As DataGridViewTextBoxColumn
    Friend WithEvents 更新日 As DataGridViewTextBoxColumn
    Friend WithEvents 担当者役職 As DataGridViewTextBoxColumn
End Class
