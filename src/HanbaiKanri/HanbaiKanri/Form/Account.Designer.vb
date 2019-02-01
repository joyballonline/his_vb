<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Account
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
        Me.LblAccountCode = New System.Windows.Forms.Label()
        Me.LblAccountName1 = New System.Windows.Forms.Label()
        Me.LblAccountName2 = New System.Windows.Forms.Label()
        Me.LblAccountName3 = New System.Windows.Forms.Label()
        Me.LblAccountingAccountCode = New System.Windows.Forms.Label()
        Me.LblRemarks = New System.Windows.Forms.Label()
        Me.LblEffectiveClassification = New System.Windows.Forms.Label()
        Me.ExEffectiveClassification = New System.Windows.Forms.Label()
        Me.TxtAccountCode = New System.Windows.Forms.TextBox()
        Me.TxtAccountName1 = New System.Windows.Forms.TextBox()
        Me.TxtAccountName2 = New System.Windows.Forms.TextBox()
        Me.TxtAccountName3 = New System.Windows.Forms.TextBox()
        Me.TxtAccountingAccountCode = New System.Windows.Forms.TextBox()
        Me.TxtRemarks = New System.Windows.Forms.TextBox()
        Me.TxtEffectiveClassification = New System.Windows.Forms.TextBox()
        Me.btnAddAccount = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'LblAccountCode
        '
        Me.LblAccountCode.AutoSize = True
        Me.LblAccountCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAccountCode.Location = New System.Drawing.Point(12, 15)
        Me.LblAccountCode.Name = "LblAccountCode"
        Me.LblAccountCode.Size = New System.Drawing.Size(100, 15)
        Me.LblAccountCode.TabIndex = 176
        Me.LblAccountCode.Text = "勘定科目コード"
        '
        'LblAccountName1
        '
        Me.LblAccountName1.AutoSize = True
        Me.LblAccountName1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAccountName1.Location = New System.Drawing.Point(12, 60)
        Me.LblAccountName1.Name = "LblAccountName1"
        Me.LblAccountName1.Size = New System.Drawing.Size(107, 15)
        Me.LblAccountName1.TabIndex = 178
        Me.LblAccountName1.Text = "勘定科目名称１"
        '
        'LblAccountName2
        '
        Me.LblAccountName2.AutoSize = True
        Me.LblAccountName2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAccountName2.Location = New System.Drawing.Point(12, 112)
        Me.LblAccountName2.Name = "LblAccountName2"
        Me.LblAccountName2.Size = New System.Drawing.Size(107, 15)
        Me.LblAccountName2.TabIndex = 180
        Me.LblAccountName2.Text = "勘定科目名称２"
        '
        'LblAccountName3
        '
        Me.LblAccountName3.AutoSize = True
        Me.LblAccountName3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAccountName3.Location = New System.Drawing.Point(12, 161)
        Me.LblAccountName3.Name = "LblAccountName3"
        Me.LblAccountName3.Size = New System.Drawing.Size(107, 15)
        Me.LblAccountName3.TabIndex = 182
        Me.LblAccountName3.Text = "勘定科目名称３"
        '
        'LblAccountingAccountCode
        '
        Me.LblAccountingAccountCode.AutoSize = True
        Me.LblAccountingAccountCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAccountingAccountCode.Location = New System.Drawing.Point(12, 214)
        Me.LblAccountingAccountCode.Name = "LblAccountingAccountCode"
        Me.LblAccountingAccountCode.Size = New System.Drawing.Size(145, 15)
        Me.LblAccountingAccountCode.TabIndex = 184
        Me.LblAccountingAccountCode.Text = "会計用勘定科目コード"
        '
        'LblRemarks
        '
        Me.LblRemarks.AutoSize = True
        Me.LblRemarks.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblRemarks.Location = New System.Drawing.Point(12, 270)
        Me.LblRemarks.Name = "LblRemarks"
        Me.LblRemarks.Size = New System.Drawing.Size(37, 15)
        Me.LblRemarks.TabIndex = 186
        Me.LblRemarks.Text = "備考"
        '
        'LblEffectiveClassification
        '
        Me.LblEffectiveClassification.AutoSize = True
        Me.LblEffectiveClassification.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblEffectiveClassification.Location = New System.Drawing.Point(12, 322)
        Me.LblEffectiveClassification.Name = "LblEffectiveClassification"
        Me.LblEffectiveClassification.Size = New System.Drawing.Size(67, 15)
        Me.LblEffectiveClassification.TabIndex = 188
        Me.LblEffectiveClassification.Text = "有効区分"
        '
        'ExEffectiveClassification
        '
        Me.ExEffectiveClassification.AutoSize = True
        Me.ExEffectiveClassification.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ExEffectiveClassification.Location = New System.Drawing.Point(189, 344)
        Me.ExEffectiveClassification.Name = "ExEffectiveClassification"
        Me.ExEffectiveClassification.Size = New System.Drawing.Size(104, 15)
        Me.ExEffectiveClassification.TabIndex = 217
        Me.ExEffectiveClassification.Text = "(0:有効 1:無効)"
        '
        'TxtAccountCode
        '
        Me.TxtAccountCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAccountCode.Location = New System.Drawing.Point(183, 12)
        Me.TxtAccountCode.MaxLength = 20
        Me.TxtAccountCode.Name = "TxtAccountCode"
        Me.TxtAccountCode.Size = New System.Drawing.Size(234, 22)
        Me.TxtAccountCode.TabIndex = 218
        '
        'TxtAccountName1
        '
        Me.TxtAccountName1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAccountName1.Location = New System.Drawing.Point(183, 57)
        Me.TxtAccountName1.MaxLength = 50
        Me.TxtAccountName1.Name = "TxtAccountName1"
        Me.TxtAccountName1.Size = New System.Drawing.Size(234, 22)
        Me.TxtAccountName1.TabIndex = 219
        '
        'TxtAccountName2
        '
        Me.TxtAccountName2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAccountName2.Location = New System.Drawing.Point(183, 105)
        Me.TxtAccountName2.MaxLength = 50
        Me.TxtAccountName2.Name = "TxtAccountName2"
        Me.TxtAccountName2.Size = New System.Drawing.Size(234, 22)
        Me.TxtAccountName2.TabIndex = 220
        '
        'TxtAccountName3
        '
        Me.TxtAccountName3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAccountName3.Location = New System.Drawing.Point(183, 158)
        Me.TxtAccountName3.MaxLength = 50
        Me.TxtAccountName3.Name = "TxtAccountName3"
        Me.TxtAccountName3.Size = New System.Drawing.Size(234, 22)
        Me.TxtAccountName3.TabIndex = 221
        '
        'TxtAccountingAccountCode
        '
        Me.TxtAccountingAccountCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAccountingAccountCode.Location = New System.Drawing.Point(183, 211)
        Me.TxtAccountingAccountCode.MaxLength = 20
        Me.TxtAccountingAccountCode.Name = "TxtAccountingAccountCode"
        Me.TxtAccountingAccountCode.Size = New System.Drawing.Size(234, 22)
        Me.TxtAccountingAccountCode.TabIndex = 222
        '
        'TxtRemarks
        '
        Me.TxtRemarks.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtRemarks.Location = New System.Drawing.Point(183, 267)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(234, 22)
        Me.TxtRemarks.TabIndex = 223
        '
        'TxtEffectiveClassification
        '
        Me.TxtEffectiveClassification.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtEffectiveClassification.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TxtEffectiveClassification.Location = New System.Drawing.Point(183, 319)
        Me.TxtEffectiveClassification.MaxLength = 1
        Me.TxtEffectiveClassification.Name = "TxtEffectiveClassification"
        Me.TxtEffectiveClassification.Size = New System.Drawing.Size(234, 22)
        Me.TxtEffectiveClassification.TabIndex = 224
        '
        'btnAddAccount
        '
        Me.btnAddAccount.Location = New System.Drawing.Point(381, 509)
        Me.btnAddAccount.Name = "btnAddAccount"
        Me.btnAddAccount.Size = New System.Drawing.Size(165, 40)
        Me.btnAddAccount.TabIndex = 225
        Me.btnAddAccount.Text = "登録"
        Me.btnAddAccount.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.Location = New System.Drawing.Point(552, 509)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(165, 40)
        Me.btnBack.TabIndex = 226
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'Account
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(729, 561)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnAddAccount)
        Me.Controls.Add(Me.TxtEffectiveClassification)
        Me.Controls.Add(Me.TxtRemarks)
        Me.Controls.Add(Me.TxtAccountingAccountCode)
        Me.Controls.Add(Me.TxtAccountName3)
        Me.Controls.Add(Me.TxtAccountName2)
        Me.Controls.Add(Me.TxtAccountName1)
        Me.Controls.Add(Me.TxtAccountCode)
        Me.Controls.Add(Me.ExEffectiveClassification)
        Me.Controls.Add(Me.LblEffectiveClassification)
        Me.Controls.Add(Me.LblRemarks)
        Me.Controls.Add(Me.LblAccountingAccountCode)
        Me.Controls.Add(Me.LblAccountName3)
        Me.Controls.Add(Me.LblAccountName2)
        Me.Controls.Add(Me.LblAccountName1)
        Me.Controls.Add(Me.LblAccountCode)
        Me.Name = "Account"
        Me.Text = "Account"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LblAccountCode As Label
    Friend WithEvents LblAccountName1 As Label
    Friend WithEvents LblAccountName2 As Label
    Friend WithEvents LblAccountName3 As Label
    Friend WithEvents LblAccountingAccountCode As Label
    Friend WithEvents LblRemarks As Label
    Friend WithEvents LblEffectiveClassification As Label
    Friend WithEvents ExEffectiveClassification As Label
    Friend WithEvents TxtAccountCode As TextBox
    Friend WithEvents TxtAccountName1 As TextBox
    Friend WithEvents TxtAccountName2 As TextBox
    Friend WithEvents TxtAccountName3 As TextBox
    Friend WithEvents TxtAccountingAccountCode As TextBox
    Friend WithEvents TxtRemarks As TextBox
    Friend WithEvents TxtEffectiveClassification As TextBox
    Friend WithEvents btnAddAccount As Button
    Friend WithEvents btnBack As Button
End Class
