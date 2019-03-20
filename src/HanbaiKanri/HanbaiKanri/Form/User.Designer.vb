<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class User
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
        Me.TxtCompanyCode = New System.Windows.Forms.TextBox()
        Me.TxtUserId = New System.Windows.Forms.TextBox()
        Me.TxtName = New System.Windows.Forms.TextBox()
        Me.TxtRemarks = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.BtnRegistration = New System.Windows.Forms.Button()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.LblPassword = New System.Windows.Forms.Label()
        Me.TxtPassword = New System.Windows.Forms.TextBox()
        Me.cmAuthority = New System.Windows.Forms.ComboBox()
        Me.cmLangage = New System.Windows.Forms.ComboBox()
        Me.cmbInvalidFlag = New System.Windows.Forms.ComboBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtShortName = New System.Windows.Forms.TextBox()
        Me.LblConfirmPassword = New System.Windows.Forms.Label()
        Me.TxtConfirmPassword = New System.Windows.Forms.TextBox()
        Me.TxtGeneration = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TxtCompanyCode
        '
        Me.TxtCompanyCode.Location = New System.Drawing.Point(877, 6)
        Me.TxtCompanyCode.Name = "TxtCompanyCode"
        Me.TxtCompanyCode.Size = New System.Drawing.Size(28, 19)
        Me.TxtCompanyCode.TabIndex = 0
        '
        'TxtUserId
        '
        Me.TxtUserId.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtUserId.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtUserId.Location = New System.Drawing.Point(154, 3)
        Me.TxtUserId.MaxLength = 20
        Me.TxtUserId.Name = "TxtUserId"
        Me.TxtUserId.Size = New System.Drawing.Size(260, 22)
        Me.TxtUserId.TabIndex = 1
        '
        'TxtName
        '
        Me.TxtName.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtName.Location = New System.Drawing.Point(154, 31)
        Me.TxtName.MaxLength = 50
        Me.TxtName.Name = "TxtName"
        Me.TxtName.Size = New System.Drawing.Size(260, 22)
        Me.TxtName.TabIndex = 2
        '
        'TxtRemarks
        '
        Me.TxtRemarks.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtRemarks.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtRemarks.Location = New System.Drawing.Point(154, 87)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(260, 22)
        Me.TxtRemarks.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 15)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "ユーザID"
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 34)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 15)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "氏名"
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(3, 90)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(37, 15)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "備考"
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 119)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 15)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "無効フラグ"
        '
        'Label7
        '
        Me.Label7.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(3, 148)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(37, 15)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "権限"
        '
        'Label8
        '
        Me.Label8.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 177)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(37, 15)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "言語"
        '
        'BtnRegistration
        '
        Me.BtnRegistration.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnRegistration.Location = New System.Drawing.Point(367, 509)
        Me.BtnRegistration.Name = "BtnRegistration"
        Me.BtnRegistration.Size = New System.Drawing.Size(165, 40)
        Me.BtnRegistration.TabIndex = 9
        Me.BtnRegistration.Text = "登録"
        Me.BtnRegistration.UseVisualStyleBackColor = True
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(552, 509)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 10
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'Label26
        '
        Me.Label26.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label26.Location = New System.Drawing.Point(420, 6)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(188, 15)
        Me.Label26.TabIndex = 237
        Me.Label26.Text = "(他ユーザと重複しない文字列)"
        '
        'LblPassword
        '
        Me.LblPassword.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblPassword.AutoSize = True
        Me.LblPassword.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPassword.Location = New System.Drawing.Point(3, 205)
        Me.LblPassword.Name = "LblPassword"
        Me.LblPassword.Size = New System.Drawing.Size(64, 15)
        Me.LblPassword.TabIndex = 239
        Me.LblPassword.Text = "パスワード"
        '
        'TxtPassword
        '
        Me.TxtPassword.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtPassword.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPassword.Location = New System.Drawing.Point(154, 202)
        Me.TxtPassword.MaxLength = 255
        Me.TxtPassword.Name = "TxtPassword"
        Me.TxtPassword.Size = New System.Drawing.Size(260, 22)
        Me.TxtPassword.TabIndex = 8
        '
        'cmAuthority
        '
        Me.cmAuthority.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmAuthority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmAuthority.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmAuthority.FormattingEnabled = True
        Me.cmAuthority.ItemHeight = 15
        Me.cmAuthority.Location = New System.Drawing.Point(154, 115)
        Me.cmAuthority.Name = "cmAuthority"
        Me.cmAuthority.Size = New System.Drawing.Size(260, 23)
        Me.cmAuthority.TabIndex = 241
        '
        'cmLangage
        '
        Me.cmLangage.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmLangage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmLangage.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmLangage.FormattingEnabled = True
        Me.cmLangage.ItemHeight = 15
        Me.cmLangage.Location = New System.Drawing.Point(154, 144)
        Me.cmLangage.Name = "cmLangage"
        Me.cmLangage.Size = New System.Drawing.Size(260, 23)
        Me.cmLangage.TabIndex = 242
        '
        'cmbInvalidFlag
        '
        Me.cmbInvalidFlag.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbInvalidFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbInvalidFlag.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbInvalidFlag.FormattingEnabled = True
        Me.cmbInvalidFlag.ItemHeight = 15
        Me.cmbInvalidFlag.Location = New System.Drawing.Point(154, 173)
        Me.cmbInvalidFlag.Name = "cmbInvalidFlag"
        Me.cmbInvalidFlag.Size = New System.Drawing.Size(260, 23)
        Me.cmbInvalidFlag.TabIndex = 243
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.52974!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.8187!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.65156!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TxtUserId, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label26, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TxtName, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label4, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TxtShortName, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label5, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Label6, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Label7, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.Label8, 0, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.LblPassword, 0, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.LblConfirmPassword, 0, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.TxtRemarks, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.cmAuthority, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.cmLangage, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.cmbInvalidFlag, 1, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.TxtConfirmPassword, 1, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.TxtPassword, 1, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.TxtGeneration, 2, 7)
        Me.TableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(12, 12)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 9
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(705, 258)
        Me.TableLayoutPanel1.TabIndex = 244
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 62)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 15)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "略名"
        '
        'TxtShortName
        '
        Me.TxtShortName.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtShortName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtShortName.Location = New System.Drawing.Point(154, 59)
        Me.TxtShortName.MaxLength = 20
        Me.TxtShortName.Name = "TxtShortName"
        Me.TxtShortName.Size = New System.Drawing.Size(260, 22)
        Me.TxtShortName.TabIndex = 3
        '
        'LblConfirmPassword
        '
        Me.LblConfirmPassword.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LblConfirmPassword.AutoSize = True
        Me.LblConfirmPassword.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblConfirmPassword.Location = New System.Drawing.Point(3, 235)
        Me.LblConfirmPassword.Name = "LblConfirmPassword"
        Me.LblConfirmPassword.Size = New System.Drawing.Size(109, 15)
        Me.LblConfirmPassword.TabIndex = 244
        Me.LblConfirmPassword.Text = "パスワード確認用"
        '
        'TxtConfirmPassword
        '
        Me.TxtConfirmPassword.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtConfirmPassword.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtConfirmPassword.Location = New System.Drawing.Point(154, 231)
        Me.TxtConfirmPassword.MaxLength = 255
        Me.TxtConfirmPassword.Name = "TxtConfirmPassword"
        Me.TxtConfirmPassword.Size = New System.Drawing.Size(260, 22)
        Me.TxtConfirmPassword.TabIndex = 245
        '
        'TxtGeneration
        '
        Me.TxtGeneration.Enabled = False
        Me.TxtGeneration.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtGeneration.Location = New System.Drawing.Point(420, 202)
        Me.TxtGeneration.MaxLength = 255
        Me.TxtGeneration.Name = "TxtGeneration"
        Me.TxtGeneration.Size = New System.Drawing.Size(234, 22)
        Me.TxtGeneration.TabIndex = 246
        Me.TxtGeneration.Visible = False
        '
        'User
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(729, 561)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.BtnRegistration)
        Me.Controls.Add(Me.TxtCompanyCode)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "User"
        Me.Text = "User"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtCompanyCode As TextBox
    Friend WithEvents TxtUserId As TextBox
    Friend WithEvents TxtName As TextBox
    Friend WithEvents TxtRemarks As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents BtnRegistration As Button
    Friend WithEvents BtnBack As Button
    Friend WithEvents Label26 As Label
    Friend WithEvents LblPassword As Label
    Friend WithEvents TxtPassword As TextBox
    Friend WithEvents cmAuthority As ComboBox
    Friend WithEvents cmLangage As ComboBox
    Friend WithEvents cmbInvalidFlag As ComboBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Label4 As Label
    Friend WithEvents TxtShortName As TextBox
    Friend WithEvents LblConfirmPassword As Label
    Friend WithEvents TxtConfirmPassword As TextBox
    Friend WithEvents TxtGeneration As TextBox
End Class
