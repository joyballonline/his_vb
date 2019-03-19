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
        Me.TxtShortName = New System.Windows.Forms.TextBox()
        Me.TxtRemarks = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.BtnRegistration = New System.Windows.Forms.Button()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.LblPassword = New System.Windows.Forms.Label()
        Me.TxtPassword = New System.Windows.Forms.TextBox()
        Me.cmAuthority = New System.Windows.Forms.ComboBox()
        Me.cmLangage = New System.Windows.Forms.ComboBox()
        Me.cmbInvalidFlag = New System.Windows.Forms.ComboBox()
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
        Me.TxtUserId.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtUserId.Location = New System.Drawing.Point(99, 12)
        Me.TxtUserId.MaxLength = 20
        Me.TxtUserId.Name = "TxtUserId"
        Me.TxtUserId.Size = New System.Drawing.Size(234, 22)
        Me.TxtUserId.TabIndex = 1
        '
        'TxtName
        '
        Me.TxtName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtName.Location = New System.Drawing.Point(483, 12)
        Me.TxtName.MaxLength = 50
        Me.TxtName.Name = "TxtName"
        Me.TxtName.Size = New System.Drawing.Size(234, 22)
        Me.TxtName.TabIndex = 2
        '
        'TxtShortName
        '
        Me.TxtShortName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtShortName.Location = New System.Drawing.Point(99, 57)
        Me.TxtShortName.MaxLength = 20
        Me.TxtShortName.Name = "TxtShortName"
        Me.TxtShortName.Size = New System.Drawing.Size(234, 22)
        Me.TxtShortName.TabIndex = 3
        '
        'TxtRemarks
        '
        Me.TxtRemarks.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtRemarks.Location = New System.Drawing.Point(483, 57)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(234, 22)
        Me.TxtRemarks.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 15)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "ユーザID"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(364, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 15)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "氏名"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 60)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 15)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "略名"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(364, 60)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(37, 15)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "備考"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(12, 105)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 15)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "無効フラグ"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(364, 105)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(37, 15)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "権限"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(12, 149)
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
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label27.Location = New System.Drawing.Point(102, 127)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(104, 15)
        Me.Label27.TabIndex = 222
        Me.Label27.Text = "(0:有効 1:無効)"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(102, 172)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(158, 15)
        Me.Label1.TabIndex = 223
        Me.Label1.Text = "(JPN:日本語 ENG:英語)"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(486, 127)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(164, 15)
        Me.Label9.TabIndex = 224
        Me.Label9.Text = "(0:一般権限 1:管理権限)"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label26.Location = New System.Drawing.Point(102, 37)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(188, 15)
        Me.Label26.TabIndex = 237
        Me.Label26.Text = "(他ユーザと重複しない文字列)"
        '
        'LblPassword
        '
        Me.LblPassword.AutoSize = True
        Me.LblPassword.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPassword.Location = New System.Drawing.Point(364, 149)
        Me.LblPassword.Name = "LblPassword"
        Me.LblPassword.Size = New System.Drawing.Size(64, 15)
        Me.LblPassword.TabIndex = 239
        Me.LblPassword.Text = "パスワード"
        '
        'TxtPassword
        '
        Me.TxtPassword.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPassword.Location = New System.Drawing.Point(483, 147)
        Me.TxtPassword.MaxLength = 255
        Me.TxtPassword.Name = "TxtPassword"
        Me.TxtPassword.Size = New System.Drawing.Size(234, 22)
        Me.TxtPassword.TabIndex = 8
        '
        'cmAuthority
        '
        Me.cmAuthority.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmAuthority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmAuthority.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmAuthority.FormattingEnabled = True
        Me.cmAuthority.ItemHeight = 15
        Me.cmAuthority.Location = New System.Drawing.Point(483, 101)
        Me.cmAuthority.Name = "cmAuthority"
        Me.cmAuthority.Size = New System.Drawing.Size(234, 23)
        Me.cmAuthority.TabIndex = 241
        '
        'cmLangage
        '
        Me.cmLangage.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmLangage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmLangage.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmLangage.FormattingEnabled = True
        Me.cmLangage.ItemHeight = 15
        Me.cmLangage.Location = New System.Drawing.Point(99, 146)
        Me.cmLangage.Name = "cmLangage"
        Me.cmLangage.Size = New System.Drawing.Size(234, 23)
        Me.cmLangage.TabIndex = 242
        '
        'cmbInvalidFlag
        '
        Me.cmbInvalidFlag.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.cmbInvalidFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbInvalidFlag.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbInvalidFlag.FormattingEnabled = True
        Me.cmbInvalidFlag.ItemHeight = 15
        Me.cmbInvalidFlag.Location = New System.Drawing.Point(99, 101)
        Me.cmbInvalidFlag.Name = "cmbInvalidFlag"
        Me.cmbInvalidFlag.Size = New System.Drawing.Size(234, 23)
        Me.cmbInvalidFlag.TabIndex = 243
        '
        'User
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(729, 561)
        Me.Controls.Add(Me.cmbInvalidFlag)
        Me.Controls.Add(Me.cmLangage)
        Me.Controls.Add(Me.cmAuthority)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtUserId)
        Me.Controls.Add(Me.LblPassword)
        Me.Controls.Add(Me.TxtPassword)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.BtnRegistration)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtRemarks)
        Me.Controls.Add(Me.TxtShortName)
        Me.Controls.Add(Me.TxtName)
        Me.Controls.Add(Me.TxtCompanyCode)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "User"
        Me.Text = "User"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtCompanyCode As TextBox
    Friend WithEvents TxtUserId As TextBox
    Friend WithEvents TxtName As TextBox
    Friend WithEvents TxtShortName As TextBox
    Friend WithEvents TxtRemarks As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents BtnRegistration As Button
    Friend WithEvents BtnBack As Button
    Friend WithEvents Label27 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label26 As Label
    Friend WithEvents LblPassword As Label
    Friend WithEvents TxtPassword As TextBox
    Friend WithEvents cmAuthority As ComboBox
    Friend WithEvents cmLangage As ComboBox
    Friend WithEvents cmbInvalidFlag As ComboBox
End Class
