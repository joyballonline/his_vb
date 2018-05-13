<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ZG110B_Junbi
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ZG110B_Junbi))
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblPreviousRunDt = New System.Windows.Forms.Label
        Me.dteSyoriDate = New CustomControl.TextBoxDate
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.dteKeikakuDate = New CustomControl.TextBoxDate
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.numSyuttaibi1 = New CustomControl.TextBoxNum
        Me.numSyuttaibi2 = New CustomControl.TextBoxNum
        Me.numSyuttaibi3 = New CustomControl.TextBoxNum
        Me.numSyuttaibi4 = New CustomControl.TextBoxNum
        Me.numSyuttaibi5 = New CustomControl.TextBoxNum
        Me.numSyuttaibi6 = New CustomControl.TextBoxNum
        Me.btnJikkou = New System.Windows.Forms.Button
        Me.btnModoru = New System.Windows.Forms.Button
        Me.btnKousin = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.lblPreviousRun_SyoriYM = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.lblPreviousRun_KeikakuYM = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.lblSyuttaibiUpdDt = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(30, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 22)
        Me.Label3.TabIndex = 626
        Me.Label3.Text = "前回実行情報"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPreviousRunDt
        '
        Me.lblPreviousRunDt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblPreviousRunDt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPreviousRunDt.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!)
        Me.lblPreviousRunDt.Location = New System.Drawing.Point(248, 30)
        Me.lblPreviousRunDt.Name = "lblPreviousRunDt"
        Me.lblPreviousRunDt.Size = New System.Drawing.Size(200, 22)
        Me.lblPreviousRunDt.TabIndex = 640
        Me.lblPreviousRunDt.Text = "2010/07/04 11:23:03"
        Me.lblPreviousRunDt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dteSyoriDate
        '
        Me.dteSyoriDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dteSyoriDate.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.dteSyoriDate.Location = New System.Drawing.Point(248, 108)
        Me.dteSyoriDate.Mask = "0000/00"
        Me.dteSyoriDate.Name = "dteSyoriDate"
        Me.dteSyoriDate.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57), Global.Microsoft.VisualBasic.ChrW(47), Global.Microsoft.VisualBasic.ChrW(95)}
        Me.dteSyoriDate.Size = New System.Drawing.Size(82, 22)
        Me.dteSyoriDate.TabIndex = 0
        Me.dteSyoriDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(170, 107)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 22)
        Me.Label2.TabIndex = 642
        Me.Label2.Text = "処理年月"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(345, 108)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(70, 22)
        Me.Label4.TabIndex = 644
        Me.Label4.Text = "計画年月"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dteKeikakuDate
        '
        Me.dteKeikakuDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dteKeikakuDate.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.dteKeikakuDate.Location = New System.Drawing.Point(421, 108)
        Me.dteKeikakuDate.Mask = "0000/00"
        Me.dteKeikakuDate.Name = "dteKeikakuDate"
        Me.dteKeikakuDate.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57), Global.Microsoft.VisualBasic.ChrW(47), Global.Microsoft.VisualBasic.ChrW(95)}
        Me.dteKeikakuDate.Size = New System.Drawing.Size(82, 22)
        Me.dteKeikakuDate.TabIndex = 1
        Me.dteKeikakuDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(22, 175)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(112, 22)
        Me.Label6.TabIndex = 649
        Me.Label6.Text = "希望出来日"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!)
        Me.Label7.Location = New System.Drawing.Point(143, 175)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(40, 22)
        Me.Label7.TabIndex = 650
        Me.Label7.Text = "1"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!)
        Me.Label8.Location = New System.Drawing.Point(182, 175)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(40, 22)
        Me.Label8.TabIndex = 651
        Me.Label8.Text = "2"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label9.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!)
        Me.Label9.Location = New System.Drawing.Point(221, 175)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(40, 22)
        Me.Label9.TabIndex = 652
        Me.Label9.Text = "3"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label10.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!)
        Me.Label10.Location = New System.Drawing.Point(260, 175)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(40, 22)
        Me.Label10.TabIndex = 653
        Me.Label10.Text = "4"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label11.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!)
        Me.Label11.Location = New System.Drawing.Point(299, 175)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(40, 22)
        Me.Label11.TabIndex = 654
        Me.Label11.Text = "5"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label12.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!)
        Me.Label12.Location = New System.Drawing.Point(338, 175)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(40, 22)
        Me.Label12.TabIndex = 655
        Me.Label12.Text = "6"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'numSyuttaibi1
        '
        Me.numSyuttaibi1.CommaFormat = True
        Me.numSyuttaibi1.DecimalDigit = CType(0, Short)
        Me.numSyuttaibi1.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSyuttaibi1.ForeColor = System.Drawing.Color.Black
        Me.numSyuttaibi1.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.numSyuttaibi1.IntegralDigit = CType(2, Short)
        Me.numSyuttaibi1.Location = New System.Drawing.Point(143, 196)
        Me.numSyuttaibi1.MaxLength = 2
        Me.numSyuttaibi1.MinusInput = False
        Me.numSyuttaibi1.Name = "numSyuttaibi1"
        Me.numSyuttaibi1.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57)}
        Me.numSyuttaibi1.Size = New System.Drawing.Size(40, 22)
        Me.numSyuttaibi1.TabIndex = 2
        Me.numSyuttaibi1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.numSyuttaibi1.Value = ""
        Me.numSyuttaibi1.ZeroSuppress = True
        '
        'numSyuttaibi2
        '
        Me.numSyuttaibi2.CommaFormat = True
        Me.numSyuttaibi2.DecimalDigit = CType(0, Short)
        Me.numSyuttaibi2.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSyuttaibi2.ForeColor = System.Drawing.Color.Black
        Me.numSyuttaibi2.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.numSyuttaibi2.IntegralDigit = CType(2, Short)
        Me.numSyuttaibi2.Location = New System.Drawing.Point(182, 196)
        Me.numSyuttaibi2.MaxLength = 2
        Me.numSyuttaibi2.MinusInput = False
        Me.numSyuttaibi2.Name = "numSyuttaibi2"
        Me.numSyuttaibi2.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57)}
        Me.numSyuttaibi2.Size = New System.Drawing.Size(40, 22)
        Me.numSyuttaibi2.TabIndex = 3
        Me.numSyuttaibi2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.numSyuttaibi2.Value = ""
        Me.numSyuttaibi2.ZeroSuppress = True
        '
        'numSyuttaibi3
        '
        Me.numSyuttaibi3.CommaFormat = True
        Me.numSyuttaibi3.DecimalDigit = CType(0, Short)
        Me.numSyuttaibi3.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSyuttaibi3.ForeColor = System.Drawing.Color.Black
        Me.numSyuttaibi3.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.numSyuttaibi3.IntegralDigit = CType(2, Short)
        Me.numSyuttaibi3.Location = New System.Drawing.Point(221, 196)
        Me.numSyuttaibi3.MaxLength = 2
        Me.numSyuttaibi3.MinusInput = False
        Me.numSyuttaibi3.Name = "numSyuttaibi3"
        Me.numSyuttaibi3.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57)}
        Me.numSyuttaibi3.Size = New System.Drawing.Size(40, 22)
        Me.numSyuttaibi3.TabIndex = 4
        Me.numSyuttaibi3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.numSyuttaibi3.Value = ""
        Me.numSyuttaibi3.ZeroSuppress = True
        '
        'numSyuttaibi4
        '
        Me.numSyuttaibi4.CommaFormat = True
        Me.numSyuttaibi4.DecimalDigit = CType(0, Short)
        Me.numSyuttaibi4.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSyuttaibi4.ForeColor = System.Drawing.Color.Black
        Me.numSyuttaibi4.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.numSyuttaibi4.IntegralDigit = CType(2, Short)
        Me.numSyuttaibi4.Location = New System.Drawing.Point(260, 196)
        Me.numSyuttaibi4.MaxLength = 2
        Me.numSyuttaibi4.MinusInput = False
        Me.numSyuttaibi4.Name = "numSyuttaibi4"
        Me.numSyuttaibi4.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57)}
        Me.numSyuttaibi4.Size = New System.Drawing.Size(40, 22)
        Me.numSyuttaibi4.TabIndex = 5
        Me.numSyuttaibi4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.numSyuttaibi4.Value = ""
        Me.numSyuttaibi4.ZeroSuppress = True
        '
        'numSyuttaibi5
        '
        Me.numSyuttaibi5.CommaFormat = True
        Me.numSyuttaibi5.DecimalDigit = CType(0, Short)
        Me.numSyuttaibi5.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSyuttaibi5.ForeColor = System.Drawing.Color.Black
        Me.numSyuttaibi5.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.numSyuttaibi5.IntegralDigit = CType(2, Short)
        Me.numSyuttaibi5.Location = New System.Drawing.Point(299, 196)
        Me.numSyuttaibi5.MaxLength = 2
        Me.numSyuttaibi5.MinusInput = False
        Me.numSyuttaibi5.Name = "numSyuttaibi5"
        Me.numSyuttaibi5.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57)}
        Me.numSyuttaibi5.Size = New System.Drawing.Size(40, 22)
        Me.numSyuttaibi5.TabIndex = 6
        Me.numSyuttaibi5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.numSyuttaibi5.Value = ""
        Me.numSyuttaibi5.ZeroSuppress = True
        '
        'numSyuttaibi6
        '
        Me.numSyuttaibi6.CommaFormat = True
        Me.numSyuttaibi6.DecimalDigit = CType(0, Short)
        Me.numSyuttaibi6.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.numSyuttaibi6.ForeColor = System.Drawing.Color.Black
        Me.numSyuttaibi6.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.numSyuttaibi6.IntegralDigit = CType(2, Short)
        Me.numSyuttaibi6.Location = New System.Drawing.Point(338, 196)
        Me.numSyuttaibi6.MaxLength = 2
        Me.numSyuttaibi6.MinusInput = False
        Me.numSyuttaibi6.Name = "numSyuttaibi6"
        Me.numSyuttaibi6.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57)}
        Me.numSyuttaibi6.Size = New System.Drawing.Size(40, 22)
        Me.numSyuttaibi6.TabIndex = 7
        Me.numSyuttaibi6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.numSyuttaibi6.Value = ""
        Me.numSyuttaibi6.ZeroSuppress = True
        '
        'btnJikkou
        '
        Me.btnJikkou.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnJikkou.Location = New System.Drawing.Point(299, 291)
        Me.btnJikkou.Name = "btnJikkou"
        Me.btnJikkou.Size = New System.Drawing.Size(130, 45)
        Me.btnJikkou.TabIndex = 9
        Me.btnJikkou.Text = "実行(&S)"
        Me.btnJikkou.UseVisualStyleBackColor = True
        '
        'btnModoru
        '
        Me.btnModoru.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnModoru.Location = New System.Drawing.Point(434, 291)
        Me.btnModoru.Name = "btnModoru"
        Me.btnModoru.Size = New System.Drawing.Size(130, 45)
        Me.btnModoru.TabIndex = 10
        Me.btnModoru.Text = "戻る(&B)"
        Me.btnModoru.UseVisualStyleBackColor = True
        '
        'btnKousin
        '
        Me.btnKousin.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnKousin.Location = New System.Drawing.Point(434, 208)
        Me.btnKousin.Name = "btnKousin"
        Me.btnKousin.Size = New System.Drawing.Size(130, 45)
        Me.btnKousin.TabIndex = 8
        Me.btnKousin.Text = "更新(&U)"
        Me.btnKousin.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(161, 30)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 22)
        Me.Label5.TabIndex = 656
        Me.Label5.Text = "実行日時"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.Location = New System.Drawing.Point(161, 62)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(79, 22)
        Me.Label13.TabIndex = 657
        Me.Label13.Text = "処理年月"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPreviousRun_SyoriYM
        '
        Me.lblPreviousRun_SyoriYM.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblPreviousRun_SyoriYM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPreviousRun_SyoriYM.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!)
        Me.lblPreviousRun_SyoriYM.Location = New System.Drawing.Point(248, 62)
        Me.lblPreviousRun_SyoriYM.Name = "lblPreviousRun_SyoriYM"
        Me.lblPreviousRun_SyoriYM.Size = New System.Drawing.Size(82, 22)
        Me.lblPreviousRun_SyoriYM.TabIndex = 658
        Me.lblPreviousRun_SyoriYM.Text = "2010/07"
        Me.lblPreviousRun_SyoriYM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label15.Location = New System.Drawing.Point(336, 62)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(79, 22)
        Me.Label15.TabIndex = 659
        Me.Label15.Text = "計画年月"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPreviousRun_KeikakuYM
        '
        Me.lblPreviousRun_KeikakuYM.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblPreviousRun_KeikakuYM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPreviousRun_KeikakuYM.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!)
        Me.lblPreviousRun_KeikakuYM.Location = New System.Drawing.Point(421, 62)
        Me.lblPreviousRun_KeikakuYM.Name = "lblPreviousRun_KeikakuYM"
        Me.lblPreviousRun_KeikakuYM.Size = New System.Drawing.Size(82, 22)
        Me.lblPreviousRun_KeikakuYM.TabIndex = 660
        Me.lblPreviousRun_KeikakuYM.Text = "2010/08"
        Me.lblPreviousRun_KeikakuYM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label17.Location = New System.Drawing.Point(30, 107)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(104, 22)
        Me.Label17.TabIndex = 661
        Me.Label17.Text = "今回実行情報"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label18.ForeColor = System.Drawing.SystemColors.Control
        Me.Label18.Location = New System.Drawing.Point(20, 151)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(545, 1)
        Me.Label18.TabIndex = 662
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label19.Location = New System.Drawing.Point(55, 231)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(79, 22)
        Me.Label19.TabIndex = 663
        Me.Label19.Text = "更新日時"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSyuttaibiUpdDt
        '
        Me.lblSyuttaibiUpdDt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblSyuttaibiUpdDt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSyuttaibiUpdDt.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!)
        Me.lblSyuttaibiUpdDt.Location = New System.Drawing.Point(143, 231)
        Me.lblSyuttaibiUpdDt.Name = "lblSyuttaibiUpdDt"
        Me.lblSyuttaibiUpdDt.Size = New System.Drawing.Size(200, 22)
        Me.lblSyuttaibiUpdDt.TabIndex = 664
        Me.lblSyuttaibiUpdDt.Text = "2010/07/04 11:23:03"
        Me.lblSyuttaibiUpdDt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ZG110B_Junbi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(587, 348)
        Me.Controls.Add(Me.lblSyuttaibiUpdDt)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.lblPreviousRun_KeikakuYM)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.lblPreviousRun_SyoriYM)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnKousin)
        Me.Controls.Add(Me.btnJikkou)
        Me.Controls.Add(Me.btnModoru)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.dteKeikakuDate)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dteSyoriDate)
        Me.Controls.Add(Me.lblPreviousRunDt)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.numSyuttaibi6)
        Me.Controls.Add(Me.numSyuttaibi5)
        Me.Controls.Add(Me.numSyuttaibi4)
        Me.Controls.Add(Me.numSyuttaibi3)
        Me.Controls.Add(Me.numSyuttaibi2)
        Me.Controls.Add(Me.numSyuttaibi1)
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.Name = "ZG110B_Junbi"
        Me.Text = "準備処理"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblPreviousRunDt As System.Windows.Forms.Label
    Friend WithEvents dteSyoriDate As CustomControl.TextBoxDate
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dteKeikakuDate As CustomControl.TextBoxDate
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents numSyuttaibi1 As CustomControl.TextBoxNum
    Friend WithEvents numSyuttaibi2 As CustomControl.TextBoxNum
    Friend WithEvents numSyuttaibi3 As CustomControl.TextBoxNum
    Friend WithEvents numSyuttaibi4 As CustomControl.TextBoxNum
    Friend WithEvents numSyuttaibi5 As CustomControl.TextBoxNum
    Friend WithEvents numSyuttaibi6 As CustomControl.TextBoxNum
    Friend WithEvents btnJikkou As System.Windows.Forms.Button
    Friend WithEvents btnModoru As System.Windows.Forms.Button
    Friend WithEvents btnKousin As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblPreviousRun_SyoriYM As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lblPreviousRun_KeikakuYM As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblSyuttaibiUpdDt As System.Windows.Forms.Label
End Class
