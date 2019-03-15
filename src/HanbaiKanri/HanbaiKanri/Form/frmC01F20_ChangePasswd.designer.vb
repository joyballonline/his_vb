<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmC01F20_ChangePasswd
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
        Me.txtKakunin = New System.Windows.Forms.MaskedTextBox()
        Me.LblTitle = New System.Windows.Forms.Label()
        Me.LblConfirmation = New System.Windows.Forms.Label()
        Me.LblNewPassword = New System.Windows.Forms.Label()
        Me.btnback = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.LblUserCd = New System.Windows.Forms.Label()
        Me.txtPasswd = New System.Windows.Forms.MaskedTextBox()
        Me.lblTanto = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtKakunin
        '
        Me.txtKakunin.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtKakunin.Font = New System.Drawing.Font("ＭＳ ゴシック", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKakunin.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtKakunin.Location = New System.Drawing.Point(183, 60)
        Me.txtKakunin.Name = "txtKakunin"
        Me.txtKakunin.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtKakunin.Size = New System.Drawing.Size(204, 23)
        Me.txtKakunin.TabIndex = 1
        '
        'LblTitle
        '
        Me.LblTitle.AutoSize = True
        Me.LblTitle.Font = New System.Drawing.Font("游ゴシック Medium", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.LblTitle.Location = New System.Drawing.Point(138, 22)
        Me.LblTitle.Name = "LblTitle"
        Me.LblTitle.Size = New System.Drawing.Size(189, 31)
        Me.LblTitle.TabIndex = 13
        Me.LblTitle.Text = "パスワード変更"
        '
        'LblConfirmation
        '
        Me.LblConfirmation.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblConfirmation.Font = New System.Drawing.Font("游ゴシック Medium", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblConfirmation.Location = New System.Drawing.Point(3, 63)
        Me.LblConfirmation.Name = "LblConfirmation"
        Me.LblConfirmation.Size = New System.Drawing.Size(174, 17)
        Me.LblConfirmation.TabIndex = 11
        Me.LblConfirmation.Text = "確認用："
        Me.LblConfirmation.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblNewPassword
        '
        Me.LblNewPassword.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblNewPassword.AutoSize = True
        Me.LblNewPassword.Font = New System.Drawing.Font("游ゴシック Medium", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblNewPassword.Location = New System.Drawing.Point(3, 33)
        Me.LblNewPassword.Name = "LblNewPassword"
        Me.LblNewPassword.Size = New System.Drawing.Size(174, 17)
        Me.LblNewPassword.TabIndex = 9
        Me.LblNewPassword.Text = "新パスワード："
        Me.LblNewPassword.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'btnback
        '
        Me.btnback.Font = New System.Drawing.Font("游ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnback.Location = New System.Drawing.Point(245, 206)
        Me.btnback.Name = "btnback"
        Me.btnback.Size = New System.Drawing.Size(125, 40)
        Me.btnback.TabIndex = 3
        Me.btnback.Text = "戻る"
        Me.btnback.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Font = New System.Drawing.Font("游ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnOK.Location = New System.Drawing.Point(99, 206)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(125, 40)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'LblUserCd
        '
        Me.LblUserCd.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblUserCd.AutoSize = True
        Me.LblUserCd.Font = New System.Drawing.Font("游ゴシック Medium", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblUserCd.Location = New System.Drawing.Point(3, 5)
        Me.LblUserCd.Name = "LblUserCd"
        Me.LblUserCd.Size = New System.Drawing.Size(174, 17)
        Me.LblUserCd.TabIndex = 15
        Me.LblUserCd.Text = "ユーザコード："
        Me.LblUserCd.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtPasswd
        '
        Me.txtPasswd.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtPasswd.Font = New System.Drawing.Font("ＭＳ ゴシック", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtPasswd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtPasswd.Location = New System.Drawing.Point(183, 31)
        Me.txtPasswd.Name = "txtPasswd"
        Me.txtPasswd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPasswd.Size = New System.Drawing.Size(204, 23)
        Me.txtPasswd.TabIndex = 0
        '
        'lblTanto
        '
        Me.lblTanto.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblTanto.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblTanto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTanto.Font = New System.Drawing.Font("ＭＳ ゴシック", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTanto.Location = New System.Drawing.Point(183, 2)
        Me.lblTanto.Name = "lblTanto"
        Me.lblTanto.Size = New System.Drawing.Size(204, 23)
        Me.lblTanto.TabIndex = 16
        Me.lblTanto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.86666!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.13334!))
        Me.TableLayoutPanel1.Controls.Add(Me.LblUserCd, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txtKakunin, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.txtPasswd, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblTanto, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LblConfirmation, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LblNewPassword, 0, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(12, 88)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(432, 87)
        Me.TableLayoutPanel1.TabIndex = 17
        '
        'frmC01F20_ChangePasswd
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(456, 276)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.LblTitle)
        Me.Controls.Add(Me.btnback)
        Me.Controls.Add(Me.btnOK)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Name = "frmC01F20_ChangePasswd"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "ChangePasswd(C01F20)"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtKakunin As System.Windows.Forms.MaskedTextBox
    Friend WithEvents LblTitle As System.Windows.Forms.Label
    Friend WithEvents LblConfirmation As System.Windows.Forms.Label
    Friend WithEvents LblNewPassword As System.Windows.Forms.Label
    Friend WithEvents btnback As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents LblUserCd As System.Windows.Forms.Label
    Friend WithEvents txtPasswd As System.Windows.Forms.MaskedTextBox
    Friend WithEvents lblTanto As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
End Class
