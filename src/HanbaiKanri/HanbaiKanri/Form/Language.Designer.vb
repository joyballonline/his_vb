<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Language
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
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.BtnRegistration = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtFlg = New System.Windows.Forms.TextBox()
        Me.TxtRemarks = New System.Windows.Forms.TextBox()
        Me.TxtShortName = New System.Windows.Forms.TextBox()
        Me.TxtName = New System.Windows.Forms.TextBox()
        Me.TxtLanguage = New System.Windows.Forms.TextBox()
        Me.TxtCompanyCode = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'BtnBack
        '
        Me.BtnBack.Location = New System.Drawing.Point(197, 131)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(175, 23)
        Me.BtnBack.TabIndex = 34
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'BtnRegistration
        '
        Me.BtnRegistration.Location = New System.Drawing.Point(12, 131)
        Me.BtnRegistration.Name = "BtnRegistration"
        Me.BtnRegistration.Size = New System.Drawing.Size(175, 23)
        Me.BtnRegistration.TabIndex = 33
        Me.BtnRegistration.Text = "登録"
        Me.BtnRegistration.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 109)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(54, 12)
        Me.Label6.TabIndex = 30
        Me.Label6.Text = "無効フラグ"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 84)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(29, 12)
        Me.Label5.TabIndex = 29
        Me.Label5.Text = "備考"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 59)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 12)
        Me.Label4.TabIndex = 28
        Me.Label4.Text = "言語略称"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 34)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 12)
        Me.Label3.TabIndex = 27
        Me.Label3.Text = "言語名称"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 12)
        Me.Label2.TabIndex = 26
        Me.Label2.Text = "言語コード"
        '
        'TxtFlg
        '
        Me.TxtFlg.Location = New System.Drawing.Point(101, 106)
        Me.TxtFlg.Name = "TxtFlg"
        Me.TxtFlg.Size = New System.Drawing.Size(271, 19)
        Me.TxtFlg.TabIndex = 23
        '
        'TxtRemarks
        '
        Me.TxtRemarks.Location = New System.Drawing.Point(101, 81)
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(271, 19)
        Me.TxtRemarks.TabIndex = 22
        '
        'TxtShortName
        '
        Me.TxtShortName.Location = New System.Drawing.Point(101, 56)
        Me.TxtShortName.Name = "TxtShortName"
        Me.TxtShortName.Size = New System.Drawing.Size(271, 19)
        Me.TxtShortName.TabIndex = 21
        '
        'TxtName
        '
        Me.TxtName.Location = New System.Drawing.Point(101, 31)
        Me.TxtName.Name = "TxtName"
        Me.TxtName.Size = New System.Drawing.Size(271, 19)
        Me.TxtName.TabIndex = 20
        '
        'TxtLanguage
        '
        Me.TxtLanguage.Location = New System.Drawing.Point(101, 6)
        Me.TxtLanguage.Name = "TxtLanguage"
        Me.TxtLanguage.Size = New System.Drawing.Size(271, 19)
        Me.TxtLanguage.TabIndex = 19
        '
        'TxtCompanyCode
        '
        Me.TxtCompanyCode.Location = New System.Drawing.Point(399, 6)
        Me.TxtCompanyCode.Name = "TxtCompanyCode"
        Me.TxtCompanyCode.Size = New System.Drawing.Size(28, 19)
        Me.TxtCompanyCode.TabIndex = 35
        '
        'Language
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(383, 163)
        Me.Controls.Add(Me.TxtCompanyCode)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.BtnRegistration)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtFlg)
        Me.Controls.Add(Me.TxtRemarks)
        Me.Controls.Add(Me.TxtShortName)
        Me.Controls.Add(Me.TxtName)
        Me.Controls.Add(Me.TxtLanguage)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Language"
        Me.Text = "Langage"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnBack As Button
    Friend WithEvents BtnRegistration As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtFlg As TextBox
    Friend WithEvents TxtRemarks As TextBox
    Friend WithEvents TxtShortName As TextBox
    Friend WithEvents TxtName As TextBox
    Friend WithEvents TxtLanguage As TextBox
    Friend WithEvents TxtCompanyCode As TextBox
End Class
