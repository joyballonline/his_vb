<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmKR12_ChangePasswd
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.txtKakunin = New System.Windows.Forms.MaskedTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnback = New System.Windows.Forms.Button
        Me.btnOK = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtPasswd = New System.Windows.Forms.MaskedTextBox
        Me.lblTanto = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'txtKakunin
        '
        Me.txtKakunin.Font = New System.Drawing.Font("ＭＳ ゴシック", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKakunin.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtKakunin.Location = New System.Drawing.Point(166, 166)
        Me.txtKakunin.Name = "txtKakunin"
        Me.txtKakunin.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtKakunin.Size = New System.Drawing.Size(204, 23)
        Me.txtKakunin.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("HG創英角ﾎﾟｯﾌﾟ体", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Navy
        Me.Label3.Location = New System.Drawing.Point(56, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(178, 24)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "パスワード変更"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(96, 169)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 16)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "確認用："
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(57, 126)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(103, 16)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "新パスワード："
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'btnback
        '
        Me.btnback.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnback.Location = New System.Drawing.Point(245, 206)
        Me.btnback.Name = "btnback"
        Me.btnback.Size = New System.Drawing.Size(125, 40)
        Me.btnback.TabIndex = 3
        Me.btnback.Text = "戻る(&E)"
        Me.btnback.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnOK.Location = New System.Drawing.Point(99, 206)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(125, 40)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK(&O)"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(57, 82)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(103, 16)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "担当者コード："
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtPasswd
        '
        Me.txtPasswd.Font = New System.Drawing.Font("ＭＳ ゴシック", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtPasswd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtPasswd.Location = New System.Drawing.Point(166, 123)
        Me.txtPasswd.Name = "txtPasswd"
        Me.txtPasswd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPasswd.Size = New System.Drawing.Size(204, 23)
        Me.txtPasswd.TabIndex = 0
        '
        'lblTanto
        '
        Me.lblTanto.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblTanto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTanto.Font = New System.Drawing.Font("ＭＳ ゴシック", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTanto.Location = New System.Drawing.Point(166, 81)
        Me.lblTanto.Name = "lblTanto"
        Me.lblTanto.Size = New System.Drawing.Size(204, 23)
        Me.lblTanto.TabIndex = 16
        Me.lblTanto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmKR12_ChangePasswd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(456, 276)
        Me.Controls.Add(Me.lblTanto)
        Me.Controls.Add(Me.txtPasswd)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtKakunin)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnback)
        Me.Controls.Add(Me.btnOK)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Name = "frmKR12_ChangePasswd"
        Me.Text = "購買連携システム"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtKakunin As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnback As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPasswd As System.Windows.Forms.MaskedTextBox
    Friend WithEvents lblTanto As System.Windows.Forms.Label
End Class
