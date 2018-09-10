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
        Me.TxtFlg = New System.Windows.Forms.TextBox()
        Me.TxtAuthority = New System.Windows.Forms.TextBox()
        Me.TxtLangage = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.BtnRegistration = New System.Windows.Forms.Button()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'TxtCompanyCode
        '
        Me.TxtCompanyCode.Location = New System.Drawing.Point(399, 6)
        Me.TxtCompanyCode.Name = "TxtCompanyCode"
        Me.TxtCompanyCode.Size = New System.Drawing.Size(28, 19)
        Me.TxtCompanyCode.TabIndex = 0
        '
        'TxtUserId
        '
        Me.TxtUserId.Location = New System.Drawing.Point(99, 6)
        Me.TxtUserId.Name = "TxtUserId"
        Me.TxtUserId.Size = New System.Drawing.Size(271, 19)
        Me.TxtUserId.TabIndex = 1
        '
        'TxtName
        '
        Me.TxtName.Location = New System.Drawing.Point(99, 31)
        Me.TxtName.Name = "TxtName"
        Me.TxtName.Size = New System.Drawing.Size(271, 19)
        Me.TxtName.TabIndex = 2
        '
        'TxtShortName
        '
        Me.TxtShortName.Location = New System.Drawing.Point(99, 56)
        Me.TxtShortName.Name = "TxtShortName"
        Me.TxtShortName.Size = New System.Drawing.Size(271, 19)
        Me.TxtShortName.TabIndex = 3
        '
        'TxtRemarks
        '
        Me.TxtRemarks.Location = New System.Drawing.Point(99, 81)
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(271, 19)
        Me.TxtRemarks.TabIndex = 4
        '
        'TxtFlg
        '
        Me.TxtFlg.Location = New System.Drawing.Point(99, 106)
        Me.TxtFlg.Name = "TxtFlg"
        Me.TxtFlg.Size = New System.Drawing.Size(271, 19)
        Me.TxtFlg.TabIndex = 5
        '
        'TxtAuthority
        '
        Me.TxtAuthority.Location = New System.Drawing.Point(99, 131)
        Me.TxtAuthority.Name = "TxtAuthority"
        Me.TxtAuthority.Size = New System.Drawing.Size(271, 19)
        Me.TxtAuthority.TabIndex = 6
        '
        'TxtLangage
        '
        Me.TxtLangage.Location = New System.Drawing.Point(99, 156)
        Me.TxtLangage.Name = "TxtLangage"
        Me.TxtLangage.Size = New System.Drawing.Size(271, 19)
        Me.TxtLangage.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 12)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "ユーザID"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 34)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 12)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "氏名"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 59)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 12)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "略名"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(10, 84)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(29, 12)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "備考"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(10, 109)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(54, 12)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "無効フラグ"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(10, 134)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(29, 12)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "権限"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(10, 159)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(29, 12)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "言語"
        '
        'BtnRegistration
        '
        Me.BtnRegistration.Location = New System.Drawing.Point(12, 181)
        Me.BtnRegistration.Name = "BtnRegistration"
        Me.BtnRegistration.Size = New System.Drawing.Size(175, 23)
        Me.BtnRegistration.TabIndex = 17
        Me.BtnRegistration.Text = "登録"
        Me.BtnRegistration.UseVisualStyleBackColor = True
        '
        'BtnBack
        '
        Me.BtnBack.Location = New System.Drawing.Point(197, 181)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(175, 23)
        Me.BtnBack.TabIndex = 18
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'User
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(382, 212)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.BtnRegistration)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtLangage)
        Me.Controls.Add(Me.TxtAuthority)
        Me.Controls.Add(Me.TxtFlg)
        Me.Controls.Add(Me.TxtRemarks)
        Me.Controls.Add(Me.TxtShortName)
        Me.Controls.Add(Me.TxtName)
        Me.Controls.Add(Me.TxtUserId)
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
    Friend WithEvents TxtFlg As TextBox
    Friend WithEvents TxtAuthority As TextBox
    Friend WithEvents TxtLangage As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents BtnRegistration As Button
    Friend WithEvents BtnBack As Button
End Class
