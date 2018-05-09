<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ZG221S_TuikaNyuuryoku
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ZG221S_TuikaNyuuryoku))
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtHinmeiCD = New System.Windows.Forms.TextBox
        Me.txtSeiban = New System.Windows.Forms.TextBox
        Me.txtSuuryou = New CustomControl.TextBoxNum
        Me.txtKibou = New CustomControl.TextBoxDate
        Me.btnTouroku = New System.Windows.Forms.Button
        Me.btnModoru = New System.Windows.Forms.Button
        Me.lblHinmei = New System.Windows.Forms.Label
        Me.lblKhinmeicd = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(42, 20)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(90, 22)
        Me.Label11.TabIndex = 38
        Me.Label11.Text = "希望出来日"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(42, 55)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 22)
        Me.Label1.TabIndex = 39
        Me.Label1.Text = "品名コード"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(42, 94)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 22)
        Me.Label2.TabIndex = 40
        Me.Label2.Text = "製番"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(42, 129)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(90, 22)
        Me.Label3.TabIndex = 41
        Me.Label3.Text = "数量"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtHinmeiCD
        '
        Me.txtHinmeiCD.BackColor = System.Drawing.Color.White
        Me.txtHinmeiCD.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtHinmeiCD.Location = New System.Drawing.Point(138, 56)
        Me.txtHinmeiCD.MaxLength = 13
        Me.txtHinmeiCD.Name = "txtHinmeiCD"
        Me.txtHinmeiCD.Size = New System.Drawing.Size(120, 22)
        Me.txtHinmeiCD.TabIndex = 1
        '
        'txtSeiban
        '
        Me.txtSeiban.BackColor = System.Drawing.Color.White
        Me.txtSeiban.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSeiban.Location = New System.Drawing.Point(138, 95)
        Me.txtSeiban.MaxLength = 8
        Me.txtSeiban.Name = "txtSeiban"
        Me.txtSeiban.Size = New System.Drawing.Size(120, 22)
        Me.txtSeiban.TabIndex = 2
        Me.txtSeiban.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtSuuryou
        '
        Me.txtSuuryou.CommaFormat = True
        Me.txtSuuryou.DecimalDigit = CType(0, Short)
        Me.txtSuuryou.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSuuryou.ForeColor = System.Drawing.Color.Black
        Me.txtSuuryou.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSuuryou.IntegralDigit = CType(6, Short)
        Me.txtSuuryou.Location = New System.Drawing.Point(138, 129)
        Me.txtSuuryou.Margin = New System.Windows.Forms.Padding(3, 3, 15, 3)
        Me.txtSuuryou.MaxLength = 6
        Me.txtSuuryou.MinusInput = False
        Me.txtSuuryou.Name = "txtSuuryou"
        Me.txtSuuryou.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57)}
        Me.txtSuuryou.Size = New System.Drawing.Size(120, 22)
        Me.txtSuuryou.TabIndex = 3
        Me.txtSuuryou.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSuuryou.Value = ""
        Me.txtSuuryou.ZeroSuppress = False
        '
        'txtKibou
        '
        Me.txtKibou.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKibou.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtKibou.Location = New System.Drawing.Point(138, 20)
        Me.txtKibou.Mask = "0000/00/00"
        Me.txtKibou.Name = "txtKibou"
        Me.txtKibou.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57), Global.Microsoft.VisualBasic.ChrW(47), Global.Microsoft.VisualBasic.ChrW(95)}
        Me.txtKibou.Size = New System.Drawing.Size(120, 22)
        Me.txtKibou.TabIndex = 0
        Me.txtKibou.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnTouroku
        '
        Me.btnTouroku.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnTouroku.Location = New System.Drawing.Point(254, 179)
        Me.btnTouroku.Name = "btnTouroku"
        Me.btnTouroku.Size = New System.Drawing.Size(130, 45)
        Me.btnTouroku.TabIndex = 4
        Me.btnTouroku.Text = "登録(&I)"
        Me.btnTouroku.UseVisualStyleBackColor = True
        '
        'btnModoru
        '
        Me.btnModoru.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnModoru.Location = New System.Drawing.Point(390, 179)
        Me.btnModoru.Name = "btnModoru"
        Me.btnModoru.Size = New System.Drawing.Size(130, 45)
        Me.btnModoru.TabIndex = 5
        Me.btnModoru.Text = "戻る(&B)"
        Me.btnModoru.UseVisualStyleBackColor = True
        '
        'lblHinmei
        '
        Me.lblHinmei.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblHinmei.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblHinmei.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHinmei.Location = New System.Drawing.Point(264, 56)
        Me.lblHinmei.Name = "lblHinmei"
        Me.lblHinmei.Size = New System.Drawing.Size(290, 22)
        Me.lblHinmei.TabIndex = 47
        Me.lblHinmei.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblKhinmeicd
        '
        Me.lblKhinmeicd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblKhinmeicd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblKhinmeicd.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKhinmeicd.Location = New System.Drawing.Point(264, 95)
        Me.lblKhinmeicd.Name = "lblKhinmeicd"
        Me.lblKhinmeicd.Size = New System.Drawing.Size(290, 22)
        Me.lblKhinmeicd.TabIndex = 48
        Me.lblKhinmeicd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblKhinmeicd.Visible = False
        '
        'ZG221S_TuikaNyuuryoku
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(597, 236)
        Me.Controls.Add(Me.lblKhinmeicd)
        Me.Controls.Add(Me.lblHinmei)
        Me.Controls.Add(Me.btnTouroku)
        Me.Controls.Add(Me.btnModoru)
        Me.Controls.Add(Me.txtKibou)
        Me.Controls.Add(Me.txtSuuryou)
        Me.Controls.Add(Me.txtSeiban)
        Me.Controls.Add(Me.txtHinmeiCD)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label11)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "ZG221S_TuikaNyuuryoku"
        Me.Text = "追加手配登録"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtHinmeiCD As System.Windows.Forms.TextBox
    Friend WithEvents txtSeiban As System.Windows.Forms.TextBox
    Friend WithEvents txtSuuryou As CustomControl.TextBoxNum
    Friend WithEvents txtKibou As CustomControl.TextBoxDate
    Friend WithEvents btnTouroku As System.Windows.Forms.Button
    Friend WithEvents btnModoru As System.Windows.Forms.Button
    Friend WithEvents lblHinmei As System.Windows.Forms.Label
    Friend WithEvents lblKhinmeicd As System.Windows.Forms.Label
End Class
