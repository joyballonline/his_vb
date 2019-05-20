<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MstCurrency
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
        Me.Dgv_Account = New System.Windows.Forms.DataGridView()
        Me.採番キー = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.名称 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.略称 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.通貨コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.備考 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.取消日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.取消区分 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.更新者 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.更新日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.LblKyeword = New System.Windows.Forms.Label()
        Me.TxtSearch = New System.Windows.Forms.TextBox()
        Me.BtnSearch = New System.Windows.Forms.Button()
        CType(Me.Dgv_Account, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Dgv_Account
        '
        Me.Dgv_Account.AllowUserToAddRows = False
        Me.Dgv_Account.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Dgv_Account.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.採番キー, Me.名称, Me.略称, Me.通貨コード, Me.備考, Me.取消日, Me.取消区分, Me.更新者, Me.更新日})
        Me.Dgv_Account.Location = New System.Drawing.Point(12, 49)
        Me.Dgv_Account.Name = "Dgv_Account"
        Me.Dgv_Account.ReadOnly = True
        Me.Dgv_Account.RowHeadersVisible = False
        Me.Dgv_Account.RowTemplate.Height = 21
        Me.Dgv_Account.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Dgv_Account.Size = New System.Drawing.Size(1326, 454)
        Me.Dgv_Account.TabIndex = 3
        '
        '採番キー
        '
        Me.採番キー.HeaderText = "採番キー"
        Me.採番キー.Name = "採番キー"
        Me.採番キー.ReadOnly = True
        '
        '名称
        '
        Me.名称.HeaderText = "名称"
        Me.名称.Name = "名称"
        Me.名称.ReadOnly = True
        '
        '略称
        '
        Me.略称.HeaderText = "略称"
        Me.略称.Name = "略称"
        Me.略称.ReadOnly = True
        '
        '通貨コード
        '
        Me.通貨コード.HeaderText = "通貨コード"
        Me.通貨コード.Name = "通貨コード"
        Me.通貨コード.ReadOnly = True
        '
        '備考
        '
        Me.備考.HeaderText = "備考"
        Me.備考.Name = "備考"
        Me.備考.ReadOnly = True
        '
        '取消日
        '
        Me.取消日.HeaderText = "取消日"
        Me.取消日.Name = "取消日"
        Me.取消日.ReadOnly = True
        '
        '取消区分
        '
        Me.取消区分.HeaderText = "取消区分"
        Me.取消区分.Name = "取消区分"
        Me.取消区分.ReadOnly = True
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
        'btnAdd
        '
        Me.btnAdd.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAdd.Location = New System.Drawing.Point(831, 509)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(165, 40)
        Me.btnAdd.TabIndex = 4
        Me.btnAdd.Text = "追加"
        Me.btnAdd.UseVisualStyleBackColor = True
        Me.btnAdd.Visible = False
        '
        'btnEdit
        '
        Me.btnEdit.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnEdit.Location = New System.Drawing.Point(1002, 509)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(165, 40)
        Me.btnEdit.TabIndex = 5
        Me.btnEdit.Text = "編集"
        Me.btnEdit.UseVisualStyleBackColor = True
        Me.btnEdit.Visible = False
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
        'LblKyeword
        '
        Me.LblKyeword.AutoSize = True
        Me.LblKyeword.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblKyeword.Location = New System.Drawing.Point(10, 16)
        Me.LblKyeword.Name = "LblKyeword"
        Me.LblKyeword.Size = New System.Drawing.Size(65, 15)
        Me.LblKyeword.TabIndex = 9
        Me.LblKyeword.Text = "キーワード"
        '
        'TxtSearch
        '
        Me.TxtSearch.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSearch.Location = New System.Drawing.Point(102, 13)
        Me.TxtSearch.Name = "TxtSearch"
        Me.TxtSearch.Size = New System.Drawing.Size(142, 22)
        Me.TxtSearch.TabIndex = 1
        '
        'BtnSearch
        '
        Me.BtnSearch.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnSearch.Location = New System.Drawing.Point(250, 12)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(75, 23)
        Me.BtnSearch.TabIndex = 2
        Me.BtnSearch.Text = "検索"
        Me.BtnSearch.UseVisualStyleBackColor = True
        '
        'MstCurrency
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.TxtSearch)
        Me.Controls.Add(Me.LblKyeword)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.Dgv_Account)
        Me.Name = "MstCurrency"
        Me.Text = "MstCurrency"
        CType(Me.Dgv_Account, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Dgv_Account As DataGridView
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnEdit As Button
    Friend WithEvents BtnBack As Button
    Friend WithEvents LblKyeword As Label
    Friend WithEvents TxtSearch As TextBox
    Friend WithEvents BtnSearch As Button
    Friend WithEvents 採番キー As DataGridViewTextBoxColumn
    Friend WithEvents 名称 As DataGridViewTextBoxColumn
    Friend WithEvents 略称 As DataGridViewTextBoxColumn
    Friend WithEvents 通貨コード As DataGridViewTextBoxColumn
    Friend WithEvents 備考 As DataGridViewTextBoxColumn
    Friend WithEvents 取消日 As DataGridViewTextBoxColumn
    Friend WithEvents 取消区分 As DataGridViewTextBoxColumn
    Friend WithEvents 更新者 As DataGridViewTextBoxColumn
    Friend WithEvents 更新日 As DataGridViewTextBoxColumn
End Class
