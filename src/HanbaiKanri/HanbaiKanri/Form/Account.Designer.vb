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
        Me.TxtAccountCode = New System.Windows.Forms.TextBox()
        Me.TxtAccountName1 = New System.Windows.Forms.TextBox()
        Me.TxtAccountName2 = New System.Windows.Forms.TextBox()
        Me.TxtAccountName3 = New System.Windows.Forms.TextBox()
        Me.TxtAccountingAccountCode = New System.Windows.Forms.TextBox()
        Me.TxtRemarks = New System.Windows.Forms.TextBox()
        Me.btnAddAccount = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.cmbEffectiveClassification = New System.Windows.Forms.ComboBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'LblAccountCode
        '
        Me.LblAccountCode.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblAccountCode.AutoSize = True
        Me.LblAccountCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAccountCode.Location = New System.Drawing.Point(3, 6)
        Me.LblAccountCode.Name = "LblAccountCode"
        Me.LblAccountCode.Size = New System.Drawing.Size(100, 15)
        Me.LblAccountCode.TabIndex = 176
        Me.LblAccountCode.Text = "勘定科目コード"
        '
        'LblAccountName1
        '
        Me.LblAccountName1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblAccountName1.AutoSize = True
        Me.LblAccountName1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAccountName1.Location = New System.Drawing.Point(3, 34)
        Me.LblAccountName1.Name = "LblAccountName1"
        Me.LblAccountName1.Size = New System.Drawing.Size(107, 15)
        Me.LblAccountName1.TabIndex = 178
        Me.LblAccountName1.Text = "勘定科目名称１"
        '
        'LblAccountName2
        '
        Me.LblAccountName2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblAccountName2.AutoSize = True
        Me.LblAccountName2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAccountName2.Location = New System.Drawing.Point(3, 62)
        Me.LblAccountName2.Name = "LblAccountName2"
        Me.LblAccountName2.Size = New System.Drawing.Size(107, 15)
        Me.LblAccountName2.TabIndex = 180
        Me.LblAccountName2.Text = "勘定科目名称２"
        '
        'LblAccountName3
        '
        Me.LblAccountName3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblAccountName3.AutoSize = True
        Me.LblAccountName3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAccountName3.Location = New System.Drawing.Point(3, 90)
        Me.LblAccountName3.Name = "LblAccountName3"
        Me.LblAccountName3.Size = New System.Drawing.Size(107, 15)
        Me.LblAccountName3.TabIndex = 182
        Me.LblAccountName3.Text = "勘定科目名称３"
        '
        'LblAccountingAccountCode
        '
        Me.LblAccountingAccountCode.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblAccountingAccountCode.AutoSize = True
        Me.LblAccountingAccountCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAccountingAccountCode.Location = New System.Drawing.Point(3, 118)
        Me.LblAccountingAccountCode.Name = "LblAccountingAccountCode"
        Me.LblAccountingAccountCode.Size = New System.Drawing.Size(145, 15)
        Me.LblAccountingAccountCode.TabIndex = 184
        Me.LblAccountingAccountCode.Text = "会計用勘定科目コード"
        '
        'LblRemarks
        '
        Me.LblRemarks.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblRemarks.AutoSize = True
        Me.LblRemarks.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblRemarks.Location = New System.Drawing.Point(3, 146)
        Me.LblRemarks.Name = "LblRemarks"
        Me.LblRemarks.Size = New System.Drawing.Size(37, 15)
        Me.LblRemarks.TabIndex = 186
        Me.LblRemarks.Text = "備考"
        '
        'LblEffectiveClassification
        '
        Me.LblEffectiveClassification.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblEffectiveClassification.AutoSize = True
        Me.LblEffectiveClassification.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblEffectiveClassification.Location = New System.Drawing.Point(3, 175)
        Me.LblEffectiveClassification.Name = "LblEffectiveClassification"
        Me.LblEffectiveClassification.Size = New System.Drawing.Size(67, 15)
        Me.LblEffectiveClassification.TabIndex = 188
        Me.LblEffectiveClassification.Text = "有効区分"
        '
        'TxtAccountCode
        '
        Me.TxtAccountCode.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TxtAccountCode.Enabled = False
        Me.TxtAccountCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAccountCode.Location = New System.Drawing.Point(157, 3)
        Me.TxtAccountCode.MaxLength = 20
        Me.TxtAccountCode.Name = "TxtAccountCode"
        Me.TxtAccountCode.Size = New System.Drawing.Size(234, 22)
        Me.TxtAccountCode.TabIndex = 1
        '
        'TxtAccountName1
        '
        Me.TxtAccountName1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TxtAccountName1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAccountName1.Location = New System.Drawing.Point(157, 31)
        Me.TxtAccountName1.MaxLength = 50
        Me.TxtAccountName1.Name = "TxtAccountName1"
        Me.TxtAccountName1.Size = New System.Drawing.Size(234, 22)
        Me.TxtAccountName1.TabIndex = 2
        '
        'TxtAccountName2
        '
        Me.TxtAccountName2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TxtAccountName2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAccountName2.Location = New System.Drawing.Point(157, 59)
        Me.TxtAccountName2.MaxLength = 50
        Me.TxtAccountName2.Name = "TxtAccountName2"
        Me.TxtAccountName2.Size = New System.Drawing.Size(234, 22)
        Me.TxtAccountName2.TabIndex = 3
        '
        'TxtAccountName3
        '
        Me.TxtAccountName3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TxtAccountName3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAccountName3.Location = New System.Drawing.Point(157, 87)
        Me.TxtAccountName3.MaxLength = 50
        Me.TxtAccountName3.Name = "TxtAccountName3"
        Me.TxtAccountName3.Size = New System.Drawing.Size(234, 22)
        Me.TxtAccountName3.TabIndex = 4
        '
        'TxtAccountingAccountCode
        '
        Me.TxtAccountingAccountCode.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TxtAccountingAccountCode.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtAccountingAccountCode.Location = New System.Drawing.Point(157, 115)
        Me.TxtAccountingAccountCode.MaxLength = 20
        Me.TxtAccountingAccountCode.Name = "TxtAccountingAccountCode"
        Me.TxtAccountingAccountCode.Size = New System.Drawing.Size(234, 22)
        Me.TxtAccountingAccountCode.TabIndex = 5
        '
        'TxtRemarks
        '
        Me.TxtRemarks.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TxtRemarks.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtRemarks.Location = New System.Drawing.Point(157, 143)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(234, 22)
        Me.TxtRemarks.TabIndex = 6
        '
        'btnAddAccount
        '
        Me.btnAddAccount.Location = New System.Drawing.Point(381, 509)
        Me.btnAddAccount.Name = "btnAddAccount"
        Me.btnAddAccount.Size = New System.Drawing.Size(165, 40)
        Me.btnAddAccount.TabIndex = 2
        Me.btnAddAccount.Text = "登録"
        Me.btnAddAccount.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBack.Location = New System.Drawing.Point(552, 509)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(165, 40)
        Me.btnBack.TabIndex = 3
        Me.btnBack.Text = "戻る"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'cmbEffectiveClassification
        '
        Me.cmbEffectiveClassification.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmbEffectiveClassification.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEffectiveClassification.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbEffectiveClassification.FormattingEnabled = True
        Me.cmbEffectiveClassification.Location = New System.Drawing.Point(157, 171)
        Me.cmbEffectiveClassification.Name = "cmbEffectiveClassification"
        Me.cmbEffectiveClassification.Size = New System.Drawing.Size(234, 23)
        Me.cmbEffectiveClassification.TabIndex = 7
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.92278!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.07722!))
        Me.TableLayoutPanel1.Controls.Add(Me.LblAccountCode, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.cmbEffectiveClassification, 1, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.TxtAccountCode, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TxtAccountName1, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LblAccountName2, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TxtRemarks, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.LblEffectiveClassification, 0, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.TxtAccountName2, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TxtAccountingAccountCode, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.LblAccountName3, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.LblRemarks, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.TxtAccountName3, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.LblAccountingAccountCode, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.LblAccountName1, 0, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(12, 12)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 7
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(518, 198)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'Account
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(729, 561)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnAddAccount)
        Me.Name = "Account"
        Me.Text = "Account"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LblAccountCode As Label
    Friend WithEvents LblAccountName1 As Label
    Friend WithEvents LblAccountName2 As Label
    Friend WithEvents LblAccountName3 As Label
    Friend WithEvents LblAccountingAccountCode As Label
    Friend WithEvents LblRemarks As Label
    Friend WithEvents LblEffectiveClassification As Label
    Friend WithEvents TxtAccountCode As TextBox
    Friend WithEvents TxtAccountName1 As TextBox
    Friend WithEvents TxtAccountName2 As TextBox
    Friend WithEvents TxtAccountName3 As TextBox
    Friend WithEvents TxtAccountingAccountCode As TextBox
    Friend WithEvents TxtRemarks As TextBox
    Friend WithEvents btnAddAccount As Button
    Friend WithEvents btnBack As Button
    Friend WithEvents cmbEffectiveClassification As ComboBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
End Class
