<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmC01F10_Login
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows フォーム デザイナで必要です。

    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。

    'Windows フォーム デザイナを使用して変更できます。  
    'コード エディタを使って変更しないでください。

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.btnEnd = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtTanto = New System.Windows.Forms.TextBox()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblVer = New System.Windows.Forms.Label()
        Me.txtPasswd = New System.Windows.Forms.MaskedTextBox()
        Me.chkPasswd = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbCampany = New System.Windows.Forms.ComboBox()
        Me.lblBackup = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnLogin
        '
        Me.btnLogin.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnLogin.Location = New System.Drawing.Point(472, 412)
        Me.btnLogin.Margin = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(271, 80)
        Me.btnLogin.TabIndex = 2
        Me.btnLogin.Text = "ログイン(&G)"
        Me.btnLogin.UseVisualStyleBackColor = True
        '
        'btnEnd
        '
        Me.btnEnd.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnEnd.Location = New System.Drawing.Point(765, 412)
        Me.btnEnd.Margin = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.btnEnd.Name = "btnEnd"
        Me.btnEnd.Size = New System.Drawing.Size(271, 80)
        Me.btnEnd.TabIndex = 3
        Me.btnEnd.Text = "終了(&B)"
        Me.btnEnd.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(56, 242)
        Me.Label1.Margin = New System.Windows.Forms.Padding(7, 0, 7, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(189, 33)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "ユーザコード"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(61, 328)
        Me.Label2.Margin = New System.Windows.Forms.Padding(7, 0, 7, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(160, 33)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "パスワード"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtTanto
        '
        Me.txtTanto.Font = New System.Drawing.Font("ＭＳ ゴシック", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTanto.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtTanto.Location = New System.Drawing.Point(293, 236)
        Me.txtTanto.Margin = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.txtTanto.MaxLength = 10
        Me.txtTanto.Name = "txtTanto"
        Me.txtTanto.Size = New System.Drawing.Size(580, 39)
        Me.txtTanto.TabIndex = 0
        Me.txtTanto.Text = "zenbi01"
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("HG創英角ﾎﾟｯﾌﾟ体", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.Navy
        Me.lblTitle.Location = New System.Drawing.Point(54, 38)
        Me.lblTitle.Margin = New System.Windows.Forms.Padding(7, 0, 7, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(404, 48)
        Me.lblTitle.TabIndex = 6
        Me.lblTitle.Text = "販売管理システム"
        '
        'lblVer
        '
        Me.lblVer.Font = New System.Drawing.Font("HG創英角ﾎﾟｯﾌﾟ体", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblVer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblVer.Location = New System.Drawing.Point(667, 32)
        Me.lblVer.Margin = New System.Windows.Forms.Padding(7, 0, 7, 0)
        Me.lblVer.Name = "lblVer"
        Me.lblVer.Size = New System.Drawing.Size(379, 68)
        Me.lblVer.TabIndex = 11
        Me.lblVer.Text = "Ver : @.@.@@"
        Me.lblVer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPasswd
        '
        Me.txtPasswd.Font = New System.Drawing.Font("ＭＳ ゴシック", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtPasswd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtPasswd.Location = New System.Drawing.Point(293, 322)
        Me.txtPasswd.Margin = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.txtPasswd.Name = "txtPasswd"
        Me.txtPasswd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPasswd.Size = New System.Drawing.Size(580, 39)
        Me.txtPasswd.TabIndex = 1
        Me.txtPasswd.Text = "kaneki01"
        '
        'chkPasswd
        '
        Me.chkPasswd.AutoSize = True
        Me.chkPasswd.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkPasswd.Location = New System.Drawing.Point(63, 434)
        Me.chkPasswd.Margin = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.chkPasswd.Name = "chkPasswd"
        Me.chkPasswd.Size = New System.Drawing.Size(288, 37)
        Me.chkPasswd.TabIndex = 13
        Me.chkPasswd.Text = "パスワードの変更"
        Me.chkPasswd.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(56, 158)
        Me.Label5.Margin = New System.Windows.Forms.Padding(7, 0, 7, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 33)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "会社"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbCampany
        '
        Me.cmbCampany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCampany.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmbCampany.FormattingEnabled = True
        Me.cmbCampany.Items.AddRange(New Object() {"株式会社全備"})
        Me.cmbCampany.Location = New System.Drawing.Point(293, 156)
        Me.cmbCampany.Margin = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.cmbCampany.Name = "cmbCampany"
        Me.cmbCampany.Size = New System.Drawing.Size(580, 38)
        Me.cmbCampany.TabIndex = 15
        '
        'lblBackup
        '
        Me.lblBackup.AutoSize = True
        Me.lblBackup.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBackup.ForeColor = System.Drawing.Color.Red
        Me.lblBackup.Location = New System.Drawing.Point(-9, 480)
        Me.lblBackup.Margin = New System.Windows.Forms.Padding(7, 0, 7, 0)
        Me.lblBackup.Name = "lblBackup"
        Me.lblBackup.Size = New System.Drawing.Size(433, 33)
        Me.lblBackup.TabIndex = 16
        Me.lblBackup.Text = "★バックアップサーバ接続中★"
        Me.lblBackup.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblBackup.Visible = False
        '
        'frmC01F10_Login
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(13.0!, 24.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1062, 542)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblBackup)
        Me.Controls.Add(Me.cmbCampany)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.chkPasswd)
        Me.Controls.Add(Me.txtPasswd)
        Me.Controls.Add(Me.lblVer)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.txtTanto)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnEnd)
        Me.Controls.Add(Me.btnLogin)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.Name = "frmC01F10_Login"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ユーザ認証（C01F10）"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnLogin As System.Windows.Forms.Button
    Friend WithEvents btnEnd As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtTanto As System.Windows.Forms.TextBox
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblVer As System.Windows.Forms.Label
    Friend WithEvents txtPasswd As System.Windows.Forms.MaskedTextBox
    Friend WithEvents chkPasswd As CheckBox
    Friend WithEvents Label5 As Label
    Friend WithEvents cmbCampany As ComboBox
    Friend WithEvents lblBackup As Label
End Class
