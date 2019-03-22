<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MstSupplier
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
        Me.Dgv_Supplier = New System.Windows.Forms.DataGridView()
        Me.btnSupplierEdit = New System.Windows.Forms.Button()
        Me.btnSupplierAdd = New System.Windows.Forms.Button()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.BtnSearch = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtSearch = New System.Windows.Forms.TextBox()
        Me.会社コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入先コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入先名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入先名略称 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.郵便番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.住所１ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.住所２ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.住所３ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.電話番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.電話番号検索用 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FAX番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.担当者名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.担当者役職 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.既定間接費率 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.メモ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.銀行名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.銀行コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.支店名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.支店コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.預金種目 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.口座番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.口座名義 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.関税率 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.前払法人税率 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.輸送費率 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.会計用仕入先コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.更新者 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.更新日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.Dgv_Supplier, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Dgv_Supplier
        '
        Me.Dgv_Supplier.AllowUserToAddRows = False
        Me.Dgv_Supplier.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Dgv_Supplier.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.会社コード, Me.仕入先コード, Me.仕入先名, Me.仕入先名略称, Me.郵便番号, Me.住所１, Me.住所２, Me.住所３, Me.電話番号, Me.電話番号検索用, Me.FAX番号, Me.担当者名, Me.担当者役職, Me.既定間接費率, Me.メモ, Me.銀行名, Me.銀行コード, Me.支店名, Me.支店コード, Me.預金種目, Me.口座番号, Me.口座名義, Me.関税率, Me.前払法人税率, Me.輸送費率, Me.会計用仕入先コード, Me.更新者, Me.更新日})
        Me.Dgv_Supplier.Location = New System.Drawing.Point(12, 34)
        Me.Dgv_Supplier.Name = "Dgv_Supplier"
        Me.Dgv_Supplier.ReadOnly = True
        Me.Dgv_Supplier.RowHeadersVisible = False
        Me.Dgv_Supplier.RowTemplate.Height = 21
        Me.Dgv_Supplier.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Dgv_Supplier.Size = New System.Drawing.Size(1326, 469)
        Me.Dgv_Supplier.TabIndex = 3
        '
        'btnSupplierEdit
        '
        Me.btnSupplierEdit.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSupplierEdit.Location = New System.Drawing.Point(1002, 509)
        Me.btnSupplierEdit.Name = "btnSupplierEdit"
        Me.btnSupplierEdit.Size = New System.Drawing.Size(165, 40)
        Me.btnSupplierEdit.TabIndex = 5
        Me.btnSupplierEdit.Text = "仕入先編集"
        Me.btnSupplierEdit.UseVisualStyleBackColor = True
        '
        'btnSupplierAdd
        '
        Me.btnSupplierAdd.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSupplierAdd.Location = New System.Drawing.Point(831, 509)
        Me.btnSupplierAdd.Name = "btnSupplierAdd"
        Me.btnSupplierAdd.Size = New System.Drawing.Size(165, 40)
        Me.btnSupplierAdd.TabIndex = 4
        Me.btnSupplierAdd.Text = "仕入先追加"
        Me.btnSupplierAdd.UseVisualStyleBackColor = True
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
        Me.BtnSearch.Location = New System.Drawing.Point(188, 5)
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
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "仕入先名"
        '
        'TxtSearch
        '
        Me.TxtSearch.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSearch.Location = New System.Drawing.Point(82, 6)
        Me.TxtSearch.Name = "TxtSearch"
        Me.TxtSearch.Size = New System.Drawing.Size(100, 22)
        Me.TxtSearch.TabIndex = 1
        '
        '会社コード
        '
        Me.会社コード.HeaderText = "会社コード"
        Me.会社コード.Name = "会社コード"
        Me.会社コード.ReadOnly = True
        Me.会社コード.Visible = False
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
        '仕入先名略称
        '
        Me.仕入先名略称.HeaderText = "仕入先名略称"
        Me.仕入先名略称.Name = "仕入先名略称"
        Me.仕入先名略称.ReadOnly = True
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
        '既定間接費率
        '
        Me.既定間接費率.HeaderText = "既定間接費率"
        Me.既定間接費率.Name = "既定間接費率"
        Me.既定間接費率.ReadOnly = True
        Me.既定間接費率.Visible = False
        '
        'メモ
        '
        Me.メモ.HeaderText = "メモ"
        Me.メモ.Name = "メモ"
        Me.メモ.ReadOnly = True
        '
        '銀行名
        '
        Me.銀行名.HeaderText = "銀行名"
        Me.銀行名.Name = "銀行名"
        Me.銀行名.ReadOnly = True
        '
        '銀行コード
        '
        Me.銀行コード.HeaderText = "銀行コード"
        Me.銀行コード.Name = "銀行コード"
        Me.銀行コード.ReadOnly = True
        '
        '支店名
        '
        Me.支店名.HeaderText = "支店名"
        Me.支店名.Name = "支店名"
        Me.支店名.ReadOnly = True
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
        '会計用仕入先コード
        '
        Me.会計用仕入先コード.HeaderText = "会計用仕入先コード"
        Me.会計用仕入先コード.Name = "会計用仕入先コード"
        Me.会計用仕入先コード.ReadOnly = True
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
        'MstSupplier
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtSearch)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.btnSupplierAdd)
        Me.Controls.Add(Me.btnSupplierEdit)
        Me.Controls.Add(Me.Dgv_Supplier)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "MstSupplier"
        Me.Text = "MstSupplier"
        CType(Me.Dgv_Supplier, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Dgv_Supplier As DataGridView
    Friend WithEvents btnSupplierEdit As Button
    Friend WithEvents btnSupplierAdd As Button
    Friend WithEvents BtnBack As Button
    Friend WithEvents BtnSearch As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents TxtSearch As TextBox
    Friend WithEvents 会社コード As DataGridViewTextBoxColumn
    Friend WithEvents 仕入先コード As DataGridViewTextBoxColumn
    Friend WithEvents 仕入先名 As DataGridViewTextBoxColumn
    Friend WithEvents 仕入先名略称 As DataGridViewTextBoxColumn
    Friend WithEvents 郵便番号 As DataGridViewTextBoxColumn
    Friend WithEvents 住所１ As DataGridViewTextBoxColumn
    Friend WithEvents 住所２ As DataGridViewTextBoxColumn
    Friend WithEvents 住所３ As DataGridViewTextBoxColumn
    Friend WithEvents 電話番号 As DataGridViewTextBoxColumn
    Friend WithEvents 電話番号検索用 As DataGridViewTextBoxColumn
    Friend WithEvents FAX番号 As DataGridViewTextBoxColumn
    Friend WithEvents 担当者名 As DataGridViewTextBoxColumn
    Friend WithEvents 担当者役職 As DataGridViewTextBoxColumn
    Friend WithEvents 既定間接費率 As DataGridViewTextBoxColumn
    Friend WithEvents メモ As DataGridViewTextBoxColumn
    Friend WithEvents 銀行名 As DataGridViewTextBoxColumn
    Friend WithEvents 銀行コード As DataGridViewTextBoxColumn
    Friend WithEvents 支店名 As DataGridViewTextBoxColumn
    Friend WithEvents 支店コード As DataGridViewTextBoxColumn
    Friend WithEvents 預金種目 As DataGridViewTextBoxColumn
    Friend WithEvents 口座番号 As DataGridViewTextBoxColumn
    Friend WithEvents 口座名義 As DataGridViewTextBoxColumn
    Friend WithEvents 関税率 As DataGridViewTextBoxColumn
    Friend WithEvents 前払法人税率 As DataGridViewTextBoxColumn
    Friend WithEvents 輸送費率 As DataGridViewTextBoxColumn
    Friend WithEvents 会計用仕入先コード As DataGridViewTextBoxColumn
    Friend WithEvents 更新者 As DataGridViewTextBoxColumn
    Friend WithEvents 更新日 As DataGridViewTextBoxColumn
End Class
