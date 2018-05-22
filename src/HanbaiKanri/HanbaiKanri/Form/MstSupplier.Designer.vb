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
        Me.会社コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入先コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入先名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入先名略名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
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
        Me.btnSupplierEdit = New System.Windows.Forms.Button()
        Me.btnSupplierAdd = New System.Windows.Forms.Button()
        CType(Me.Dgv_Supplier, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Dgv_Supplier
        '
        Me.Dgv_Supplier.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Dgv_Supplier.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.会社コード, Me.仕入先コード, Me.仕入先名, Me.仕入先名略名, Me.郵便番号, Me.住所１, Me.住所２, Me.住所３, Me.電話番号, Me.電話番号検索用, Me.FAX番号, Me.担当者名, Me.既定間接費率, Me.メモ, Me.銀行コード, Me.支店コード, Me.預金種目, Me.口座番号, Me.口座名義, Me.更新者, Me.更新日})
        Me.Dgv_Supplier.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Dgv_Supplier.Location = New System.Drawing.Point(0, 0)
        Me.Dgv_Supplier.Name = "Dgv_Supplier"
        Me.Dgv_Supplier.RowTemplate.Height = 21
        Me.Dgv_Supplier.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Dgv_Supplier.Size = New System.Drawing.Size(1090, 450)
        Me.Dgv_Supplier.TabIndex = 0
        '
        '会社コード
        '
        Me.会社コード.HeaderText = "会社コード"
        Me.会社コード.Name = "会社コード"
        '
        '仕入先コード
        '
        Me.仕入先コード.HeaderText = "仕入先コード"
        Me.仕入先コード.Name = "仕入先コード"
        '
        '仕入先名
        '
        Me.仕入先名.HeaderText = "仕入先名"
        Me.仕入先名.Name = "仕入先名"
        '
        '仕入先名略名
        '
        Me.仕入先名略名.HeaderText = "仕入先名略名"
        Me.仕入先名略名.Name = "仕入先名略名"
        '
        '郵便番号
        '
        Me.郵便番号.HeaderText = "郵便番号"
        Me.郵便番号.Name = "郵便番号"
        '
        '住所１
        '
        Me.住所１.HeaderText = "住所１"
        Me.住所１.Name = "住所１"
        '
        '住所２
        '
        Me.住所２.HeaderText = "住所２"
        Me.住所２.Name = "住所２"
        '
        '住所３
        '
        Me.住所３.HeaderText = "住所３"
        Me.住所３.Name = "住所３"
        '
        '電話番号
        '
        Me.電話番号.HeaderText = "電話番号"
        Me.電話番号.Name = "電話番号"
        '
        '電話番号検索用
        '
        Me.電話番号検索用.HeaderText = "電話番号検索用"
        Me.電話番号検索用.Name = "電話番号検索用"
        '
        'FAX番号
        '
        Me.FAX番号.HeaderText = "FAX番号"
        Me.FAX番号.Name = "FAX番号"
        '
        '担当者名
        '
        Me.担当者名.HeaderText = "担当者名"
        Me.担当者名.Name = "担当者名"
        '
        '既定間接費率
        '
        Me.既定間接費率.HeaderText = "既定間接費率"
        Me.既定間接費率.Name = "既定間接費率"
        '
        'メモ
        '
        Me.メモ.HeaderText = "メモ"
        Me.メモ.Name = "メモ"
        '
        '銀行コード
        '
        Me.銀行コード.HeaderText = "銀行コード"
        Me.銀行コード.Name = "銀行コード"
        '
        '支店コード
        '
        Me.支店コード.HeaderText = "支店コード"
        Me.支店コード.Name = "支店コード"
        '
        '預金種目
        '
        Me.預金種目.HeaderText = "預金種目"
        Me.預金種目.Name = "預金種目"
        '
        '口座番号
        '
        Me.口座番号.HeaderText = "口座番号"
        Me.口座番号.Name = "口座番号"
        '
        '口座名義
        '
        Me.口座名義.HeaderText = "口座名義"
        Me.口座名義.Name = "口座名義"
        '
        '更新者
        '
        Me.更新者.HeaderText = "更新者"
        Me.更新者.Name = "更新者"
        '
        '更新日
        '
        Me.更新日.HeaderText = "更新日"
        Me.更新日.Name = "更新日"
        '
        'btnSupplierEdit
        '
        Me.btnSupplierEdit.Location = New System.Drawing.Point(12, 383)
        Me.btnSupplierEdit.Name = "btnSupplierEdit"
        Me.btnSupplierEdit.Size = New System.Drawing.Size(75, 23)
        Me.btnSupplierEdit.TabIndex = 1
        Me.btnSupplierEdit.Text = "選択"
        Me.btnSupplierEdit.UseVisualStyleBackColor = True
        '
        'btnSupplierAdd
        '
        Me.btnSupplierAdd.Location = New System.Drawing.Point(93, 383)
        Me.btnSupplierAdd.Name = "btnSupplierAdd"
        Me.btnSupplierAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnSupplierAdd.TabIndex = 2
        Me.btnSupplierAdd.Text = "追加"
        Me.btnSupplierAdd.UseVisualStyleBackColor = True
        '
        'MstSupplier
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1090, 450)
        Me.Controls.Add(Me.btnSupplierAdd)
        Me.Controls.Add(Me.btnSupplierEdit)
        Me.Controls.Add(Me.Dgv_Supplier)
        Me.Name = "MstSupplier"
        Me.Text = "MstSupplier"
        CType(Me.Dgv_Supplier, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Dgv_Supplier As DataGridView
    Friend WithEvents 会社コード As DataGridViewTextBoxColumn
    Friend WithEvents 仕入先コード As DataGridViewTextBoxColumn
    Friend WithEvents 仕入先名 As DataGridViewTextBoxColumn
    Friend WithEvents 仕入先名略名 As DataGridViewTextBoxColumn
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
    Friend WithEvents btnSupplierEdit As Button
    Friend WithEvents btnSupplierAdd As Button
End Class
