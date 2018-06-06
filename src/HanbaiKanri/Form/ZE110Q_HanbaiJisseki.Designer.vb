<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ZE110Q_HanbaiJisseki
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ZE110Q_HanbaiJisseki))
        Me.btnExcel = New System.Windows.Forms.Button
        Me.btnModoru = New System.Windows.Forms.Button
        Me.txtNengetsuTo = New CustomControl.TextBoxDate
        Me.txtNengetsuFrom = New CustomControl.TextBoxDate
        Me.Label33 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.rdoBumon3 = New System.Windows.Forms.RadioButton
        Me.rdoBumon2 = New System.Windows.Forms.RadioButton
        Me.rdoBumon1 = New System.Windows.Forms.RadioButton
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtHinsyuTo = New CustomControl.TextBoxNum
        Me.txtHinsyuFrom = New CustomControl.TextBoxNum
        Me.txtHinmei = New System.Windows.Forms.TextBox
        Me.txtColor = New CustomControl.TextBoxNum
        Me.txtSize = New CustomControl.TextBoxNum
        Me.txtSensinsuu = New CustomControl.TextBoxNum
        Me.txtHinsyuCD = New CustomControl.TextBoxNum
        Me.txtSiyoCD = New System.Windows.Forms.TextBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.rdoHinmoku3 = New System.Windows.Forms.RadioButton
        Me.rdoHinmoku2 = New System.Windows.Forms.RadioButton
        Me.rdoHinmoku1 = New System.Windows.Forms.RadioButton
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnExcel
        '
        Me.btnExcel.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnExcel.Location = New System.Drawing.Point(224, 388)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(130, 45)
        Me.btnExcel.TabIndex = 3
        Me.btnExcel.Text = "EXCEL(&K)"
        Me.btnExcel.UseVisualStyleBackColor = True
        '
        'btnModoru
        '
        Me.btnModoru.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnModoru.Location = New System.Drawing.Point(360, 388)
        Me.btnModoru.Name = "btnModoru"
        Me.btnModoru.Size = New System.Drawing.Size(130, 45)
        Me.btnModoru.TabIndex = 4
        Me.btnModoru.Text = "戻る(&B)"
        Me.btnModoru.UseVisualStyleBackColor = True
        '
        'txtNengetsuTo
        '
        Me.txtNengetsuTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNengetsuTo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtNengetsuTo.Location = New System.Drawing.Point(276, 29)
        Me.txtNengetsuTo.Mask = "0000/00"
        Me.txtNengetsuTo.Name = "txtNengetsuTo"
        Me.txtNengetsuTo.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57), Global.Microsoft.VisualBasic.ChrW(47), Global.Microsoft.VisualBasic.ChrW(95)}
        Me.txtNengetsuTo.Size = New System.Drawing.Size(97, 22)
        Me.txtNengetsuTo.TabIndex = 1
        Me.txtNengetsuTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtNengetsuFrom
        '
        Me.txtNengetsuFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNengetsuFrom.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtNengetsuFrom.Location = New System.Drawing.Point(128, 29)
        Me.txtNengetsuFrom.Mask = "0000/00"
        Me.txtNengetsuFrom.Name = "txtNengetsuFrom"
        Me.txtNengetsuFrom.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57), Global.Microsoft.VisualBasic.ChrW(47), Global.Microsoft.VisualBasic.ChrW(95)}
        Me.txtNengetsuFrom.Size = New System.Drawing.Size(97, 22)
        Me.txtNengetsuFrom.TabIndex = 0
        Me.txtNengetsuFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.SystemColors.Control
        Me.Label33.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label33.Location = New System.Drawing.Point(22, 28)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(70, 22)
        Me.Label33.TabIndex = 1420
        Me.Label33.Text = "販売年月"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label33)
        Me.GroupBox1.Controls.Add(Me.txtNengetsuTo)
        Me.GroupBox1.Controls.Add(Me.txtNengetsuFrom)
        Me.GroupBox1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(22, 258)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(468, 104)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "対象データ期間 "
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(22, 66)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(370, 22)
        Me.Label3.TabIndex = 1423
        Me.Label3.Text = "※2010年4月以降のデータが対象です"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(235, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 22)
        Me.Label2.TabIndex = 1422
        Me.Label2.Text = "～"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.rdoBumon3)
        Me.GroupBox3.Controls.Add(Me.rdoBumon2)
        Me.GroupBox3.Controls.Add(Me.rdoBumon1)
        Me.GroupBox3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(22, 22)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(316, 60)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "対象部門　（品名区分２）"
        Me.GroupBox3.UseCompatibleTextRendering = True
        '
        'rdoBumon3
        '
        Me.rdoBumon3.AutoSize = True
        Me.rdoBumon3.Location = New System.Drawing.Point(209, 24)
        Me.rdoBumon3.Name = "rdoBumon3"
        Me.rdoBumon3.Size = New System.Drawing.Size(86, 19)
        Me.rdoBumon3.TabIndex = 2
        Me.rdoBumon3.Text = "通信 (02)"
        Me.rdoBumon3.UseVisualStyleBackColor = True
        '
        'rdoBumon2
        '
        Me.rdoBumon2.AutoSize = True
        Me.rdoBumon2.Location = New System.Drawing.Point(108, 24)
        Me.rdoBumon2.Name = "rdoBumon2"
        Me.rdoBumon2.Size = New System.Drawing.Size(86, 19)
        Me.rdoBumon2.TabIndex = 1
        Me.rdoBumon2.Text = "電線 (01)"
        Me.rdoBumon2.UseVisualStyleBackColor = True
        '
        'rdoBumon1
        '
        Me.rdoBumon1.AutoSize = True
        Me.rdoBumon1.Checked = True
        Me.rdoBumon1.Location = New System.Drawing.Point(25, 24)
        Me.rdoBumon1.Name = "rdoBumon1"
        Me.rdoBumon1.Size = New System.Drawing.Size(54, 19)
        Me.rdoBumon1.TabIndex = 0
        Me.rdoBumon1.TabStop = True
        Me.rdoBumon1.Text = "全て"
        Me.rdoBumon1.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label1)
        Me.GroupBox4.Controls.Add(Me.txtHinsyuTo)
        Me.GroupBox4.Controls.Add(Me.txtHinsyuFrom)
        Me.GroupBox4.Controls.Add(Me.txtHinmei)
        Me.GroupBox4.Controls.Add(Me.txtColor)
        Me.GroupBox4.Controls.Add(Me.txtSize)
        Me.GroupBox4.Controls.Add(Me.txtSensinsuu)
        Me.GroupBox4.Controls.Add(Me.txtHinsyuCD)
        Me.GroupBox4.Controls.Add(Me.txtSiyoCD)
        Me.GroupBox4.Controls.Add(Me.Panel1)
        Me.GroupBox4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(22, 105)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(468, 131)
        Me.GroupBox4.TabIndex = 1
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "品目抽出条件"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(206, 55)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 22)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "～"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtHinsyuTo
        '
        Me.txtHinsyuTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtHinsyuTo.CommaFormat = False
        Me.txtHinsyuTo.DecimalDigit = CType(0, Short)
        Me.txtHinsyuTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtHinsyuTo.ForeColor = System.Drawing.Color.Black
        Me.txtHinsyuTo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtHinsyuTo.IntegralDigit = CType(3, Short)
        Me.txtHinsyuTo.Location = New System.Drawing.Point(244, 56)
        Me.txtHinsyuTo.MaxLength = 3
        Me.txtHinsyuTo.MinusInput = False
        Me.txtHinsyuTo.Name = "txtHinsyuTo"
        Me.txtHinsyuTo.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57)}
        Me.txtHinsyuTo.Size = New System.Drawing.Size(72, 22)
        Me.txtHinsyuTo.TabIndex = 7
        Me.txtHinsyuTo.Value = ""
        Me.txtHinsyuTo.ZeroSuppress = False
        '
        'txtHinsyuFrom
        '
        Me.txtHinsyuFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtHinsyuFrom.CommaFormat = False
        Me.txtHinsyuFrom.DecimalDigit = CType(0, Short)
        Me.txtHinsyuFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtHinsyuFrom.ForeColor = System.Drawing.Color.Black
        Me.txtHinsyuFrom.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtHinsyuFrom.IntegralDigit = CType(3, Short)
        Me.txtHinsyuFrom.Location = New System.Drawing.Point(128, 56)
        Me.txtHinsyuFrom.MaxLength = 3
        Me.txtHinsyuFrom.MinusInput = False
        Me.txtHinsyuFrom.Name = "txtHinsyuFrom"
        Me.txtHinsyuFrom.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57)}
        Me.txtHinsyuFrom.Size = New System.Drawing.Size(72, 22)
        Me.txtHinsyuFrom.TabIndex = 6
        Me.txtHinsyuFrom.Value = ""
        Me.txtHinsyuFrom.ZeroSuppress = False
        '
        'txtHinmei
        '
        Me.txtHinmei.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtHinmei.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtHinmei.ImeMode = System.Windows.Forms.ImeMode.Alpha
        Me.txtHinmei.Location = New System.Drawing.Point(128, 90)
        Me.txtHinmei.MaxLength = 35
        Me.txtHinmei.Name = "txtHinmei"
        Me.txtHinmei.Size = New System.Drawing.Size(289, 22)
        Me.txtHinmei.TabIndex = 8
        '
        'txtColor
        '
        Me.txtColor.CommaFormat = False
        Me.txtColor.DecimalDigit = CType(0, Short)
        Me.txtColor.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtColor.ForeColor = System.Drawing.Color.Black
        Me.txtColor.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtColor.IntegralDigit = CType(3, Short)
        Me.txtColor.Location = New System.Drawing.Point(276, 19)
        Me.txtColor.MaxLength = 3
        Me.txtColor.MinusInput = False
        Me.txtColor.Name = "txtColor"
        Me.txtColor.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57)}
        Me.txtColor.Size = New System.Drawing.Size(40, 22)
        Me.txtColor.TabIndex = 5
        Me.txtColor.Value = ""
        Me.txtColor.ZeroSuppress = False
        '
        'txtSize
        '
        Me.txtSize.CommaFormat = False
        Me.txtSize.DecimalDigit = CType(0, Short)
        Me.txtSize.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSize.ForeColor = System.Drawing.Color.Black
        Me.txtSize.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSize.IntegralDigit = CType(2, Short)
        Me.txtSize.Location = New System.Drawing.Point(244, 19)
        Me.txtSize.MaxLength = 2
        Me.txtSize.MinusInput = False
        Me.txtSize.Name = "txtSize"
        Me.txtSize.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57)}
        Me.txtSize.Size = New System.Drawing.Size(30, 22)
        Me.txtSize.TabIndex = 4
        Me.txtSize.Value = ""
        Me.txtSize.ZeroSuppress = False
        '
        'txtSensinsuu
        '
        Me.txtSensinsuu.CommaFormat = False
        Me.txtSensinsuu.DecimalDigit = CType(0, Short)
        Me.txtSensinsuu.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSensinsuu.ForeColor = System.Drawing.Color.Black
        Me.txtSensinsuu.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSensinsuu.IntegralDigit = CType(3, Short)
        Me.txtSensinsuu.Location = New System.Drawing.Point(202, 19)
        Me.txtSensinsuu.MaxLength = 3
        Me.txtSensinsuu.MinusInput = False
        Me.txtSensinsuu.Name = "txtSensinsuu"
        Me.txtSensinsuu.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57)}
        Me.txtSensinsuu.Size = New System.Drawing.Size(40, 22)
        Me.txtSensinsuu.TabIndex = 3
        Me.txtSensinsuu.Value = ""
        Me.txtSensinsuu.ZeroSuppress = False
        '
        'txtHinsyuCD
        '
        Me.txtHinsyuCD.CommaFormat = False
        Me.txtHinsyuCD.DecimalDigit = CType(0, Short)
        Me.txtHinsyuCD.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtHinsyuCD.ForeColor = System.Drawing.Color.Black
        Me.txtHinsyuCD.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtHinsyuCD.IntegralDigit = CType(3, Short)
        Me.txtHinsyuCD.Location = New System.Drawing.Point(160, 19)
        Me.txtHinsyuCD.MaxLength = 3
        Me.txtHinsyuCD.MinusInput = False
        Me.txtHinsyuCD.Name = "txtHinsyuCD"
        Me.txtHinsyuCD.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57)}
        Me.txtHinsyuCD.Size = New System.Drawing.Size(40, 22)
        Me.txtHinsyuCD.TabIndex = 2
        Me.txtHinsyuCD.Value = ""
        Me.txtHinsyuCD.ZeroSuppress = False
        '
        'txtSiyoCD
        '
        Me.txtSiyoCD.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSiyoCD.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSiyoCD.Location = New System.Drawing.Point(128, 19)
        Me.txtSiyoCD.MaxLength = 2
        Me.txtSiyoCD.Name = "txtSiyoCD"
        Me.txtSiyoCD.Size = New System.Drawing.Size(30, 22)
        Me.txtSiyoCD.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rdoHinmoku3)
        Me.Panel1.Controls.Add(Me.rdoHinmoku2)
        Me.Panel1.Controls.Add(Me.rdoHinmoku1)
        Me.Panel1.Location = New System.Drawing.Point(18, 14)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(105, 106)
        Me.Panel1.TabIndex = 0
        '
        'rdoHinmoku3
        '
        Me.rdoHinmoku3.AutoSize = True
        Me.rdoHinmoku3.Location = New System.Drawing.Point(6, 80)
        Me.rdoHinmoku3.Name = "rdoHinmoku3"
        Me.rdoHinmoku3.Size = New System.Drawing.Size(55, 19)
        Me.rdoHinmoku3.TabIndex = 5
        Me.rdoHinmoku3.Text = "品名"
        Me.rdoHinmoku3.UseVisualStyleBackColor = True
        '
        'rdoHinmoku2
        '
        Me.rdoHinmoku2.AutoSize = True
        Me.rdoHinmoku2.Location = New System.Drawing.Point(6, 44)
        Me.rdoHinmoku2.Name = "rdoHinmoku2"
        Me.rdoHinmoku2.Size = New System.Drawing.Size(92, 19)
        Me.rdoHinmoku2.TabIndex = 4
        Me.rdoHinmoku2.Text = "品種コード"
        Me.rdoHinmoku2.UseVisualStyleBackColor = True
        '
        'rdoHinmoku1
        '
        Me.rdoHinmoku1.AutoSize = True
        Me.rdoHinmoku1.Checked = True
        Me.rdoHinmoku1.Location = New System.Drawing.Point(6, 7)
        Me.rdoHinmoku1.Name = "rdoHinmoku1"
        Me.rdoHinmoku1.Size = New System.Drawing.Size(92, 19)
        Me.rdoHinmoku1.TabIndex = 3
        Me.rdoHinmoku1.TabStop = True
        Me.rdoHinmoku1.Text = "品名コード"
        Me.rdoHinmoku1.UseVisualStyleBackColor = True
        '
        'ZE110Q_HanbaiJisseki
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(514, 445)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnExcel)
        Me.Controls.Add(Me.btnModoru)
        Me.Controls.Add(Me.GroupBox4)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "ZE110Q_HanbaiJisseki"
        Me.Text = "販売実績照会"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnExcel As System.Windows.Forms.Button
    Friend WithEvents btnModoru As System.Windows.Forms.Button
    Friend WithEvents txtNengetsuTo As CustomControl.TextBoxDate
    Friend WithEvents txtNengetsuFrom As CustomControl.TextBoxDate
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents rdoBumon1 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoBumon3 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoBumon2 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents txtHinmei As System.Windows.Forms.TextBox
    Friend WithEvents txtColor As CustomControl.TextBoxNum
    Friend WithEvents txtSize As CustomControl.TextBoxNum
    Friend WithEvents txtSensinsuu As CustomControl.TextBoxNum
    Friend WithEvents txtHinsyuCD As CustomControl.TextBoxNum
    Friend WithEvents txtSiyoCD As System.Windows.Forms.TextBox
    Friend WithEvents txtHinsyuTo As CustomControl.TextBoxNum
    Friend WithEvents txtHinsyuFrom As CustomControl.TextBoxNum
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rdoHinmoku3 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoHinmoku2 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoHinmoku1 As System.Windows.Forms.RadioButton
End Class
