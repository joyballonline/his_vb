<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MstLanguage
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
        Me.Dgv_Language = New System.Windows.Forms.DataGridView()
        Me.会社コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.言語コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.言語名称 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.言語略称 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.備考 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.無効フラグ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.更新者 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.更新日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BtnBack = New System.Windows.Forms.Button()
        CType(Me.Dgv_Language, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Dgv_Language
        '
        Me.Dgv_Language.AllowUserToAddRows = False
        Me.Dgv_Language.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Dgv_Language.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.会社コード, Me.言語コード, Me.言語名称, Me.言語略称, Me.備考, Me.無効フラグ, Me.更新者, Me.更新日})
        Me.Dgv_Language.Location = New System.Drawing.Point(12, 12)
        Me.Dgv_Language.Name = "Dgv_Language"
        Me.Dgv_Language.RowTemplate.Height = 21
        Me.Dgv_Language.Size = New System.Drawing.Size(1326, 659)
        Me.Dgv_Language.TabIndex = 0
        '
        '会社コード
        '
        Me.会社コード.HeaderText = "会社コード"
        Me.会社コード.Name = "会社コード"
        '
        '言語コード
        '
        Me.言語コード.HeaderText = "言語コード"
        Me.言語コード.Name = "言語コード"
        '
        '言語名称
        '
        Me.言語名称.HeaderText = "言語名称"
        Me.言語名称.Name = "言語名称"
        '
        '言語略称
        '
        Me.言語略称.HeaderText = "言語略称"
        Me.言語略称.Name = "言語略称"
        '
        '備考
        '
        Me.備考.HeaderText = "備考"
        Me.備考.Name = "備考"
        '
        '無効フラグ
        '
        Me.無効フラグ.HeaderText = "無効フラグ"
        Me.無効フラグ.Name = "無効フラグ"
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
        'BtnBack
        '
        Me.BtnBack.Location = New System.Drawing.Point(1173, 677)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 4
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'MstLanguage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1350, 729)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.Dgv_Language)
        Me.Name = "MstLanguage"
        Me.Text = "MstLanguage"
        CType(Me.Dgv_Language, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Dgv_Language As DataGridView
    Friend WithEvents 会社コード As DataGridViewTextBoxColumn
    Friend WithEvents 言語コード As DataGridViewTextBoxColumn
    Friend WithEvents 言語名称 As DataGridViewTextBoxColumn
    Friend WithEvents 言語略称 As DataGridViewTextBoxColumn
    Friend WithEvents 備考 As DataGridViewTextBoxColumn
    Friend WithEvents 無効フラグ As DataGridViewTextBoxColumn
    Friend WithEvents 更新者 As DataGridViewTextBoxColumn
    Friend WithEvents 更新日 As DataGridViewTextBoxColumn
    Friend WithEvents BtnBack As Button
End Class
