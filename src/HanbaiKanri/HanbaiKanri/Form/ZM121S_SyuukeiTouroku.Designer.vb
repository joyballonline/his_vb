<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ZM121S_SyuukeiTouroku
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ZM121S_SyuukeiTouroku))
        Me.txtSColor = New CustomControl.TextBoxNum
        Me.txtSSize = New CustomControl.TextBoxNum
        Me.txtSSensin = New CustomControl.TextBoxNum
        Me.txtSHinsyu = New CustomControl.TextBoxNum
        Me.txtSSiyo = New System.Windows.Forms.TextBox
        Me.btnSakujo = New System.Windows.Forms.Button
        Me.btnTuika = New System.Windows.Forms.Button
        Me.dgvSHinmei = New CustomTabStopDataGridView.TabStop.UtilTabStopDataGridView
        Me.cnSHinmeiCD = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnSHinmei = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.btnModoru = New System.Windows.Forms.Button
        Me.lblKensuu = New System.Windows.Forms.Label
        Me.btnKakutei = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        CType(Me.dgvSHinmei, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtSColor
        '
        Me.txtSColor.CommaFormat = False
        Me.txtSColor.DecimalDigit = CType(0, Short)
        Me.txtSColor.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSColor.ForeColor = System.Drawing.Color.Black
        Me.txtSColor.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSColor.IntegralDigit = CType(3, Short)
        Me.txtSColor.Location = New System.Drawing.Point(315, 21)
        Me.txtSColor.MaxLength = 3
        Me.txtSColor.MinusInput = False
        Me.txtSColor.Name = "txtSColor"
        Me.txtSColor.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57)}
        Me.txtSColor.Size = New System.Drawing.Size(30, 22)
        Me.txtSColor.TabIndex = 4
        Me.txtSColor.Value = ""
        Me.txtSColor.ZeroSuppress = False
        '
        'txtSSize
        '
        Me.txtSSize.CommaFormat = False
        Me.txtSSize.DecimalDigit = CType(0, Short)
        Me.txtSSize.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSSize.ForeColor = System.Drawing.Color.Black
        Me.txtSSize.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSSize.IntegralDigit = CType(2, Short)
        Me.txtSSize.Location = New System.Drawing.Point(284, 21)
        Me.txtSSize.MaxLength = 2
        Me.txtSSize.MinusInput = False
        Me.txtSSize.Name = "txtSSize"
        Me.txtSSize.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57)}
        Me.txtSSize.Size = New System.Drawing.Size(25, 22)
        Me.txtSSize.TabIndex = 3
        Me.txtSSize.Value = ""
        Me.txtSSize.ZeroSuppress = False
        '
        'txtSSensin
        '
        Me.txtSSensin.CommaFormat = False
        Me.txtSSensin.DecimalDigit = CType(0, Short)
        Me.txtSSensin.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSSensin.ForeColor = System.Drawing.Color.Black
        Me.txtSSensin.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSSensin.IntegralDigit = CType(3, Short)
        Me.txtSSensin.Location = New System.Drawing.Point(248, 21)
        Me.txtSSensin.MaxLength = 3
        Me.txtSSensin.MinusInput = False
        Me.txtSSensin.Name = "txtSSensin"
        Me.txtSSensin.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57)}
        Me.txtSSensin.Size = New System.Drawing.Size(30, 22)
        Me.txtSSensin.TabIndex = 2
        Me.txtSSensin.Value = ""
        Me.txtSSensin.ZeroSuppress = False
        '
        'txtSHinsyu
        '
        Me.txtSHinsyu.CommaFormat = False
        Me.txtSHinsyu.DecimalDigit = CType(0, Short)
        Me.txtSHinsyu.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSHinsyu.ForeColor = System.Drawing.Color.Black
        Me.txtSHinsyu.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSHinsyu.IntegralDigit = CType(3, Short)
        Me.txtSHinsyu.Location = New System.Drawing.Point(212, 21)
        Me.txtSHinsyu.MaxLength = 3
        Me.txtSHinsyu.MinusInput = False
        Me.txtSHinsyu.Name = "txtSHinsyu"
        Me.txtSHinsyu.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57)}
        Me.txtSHinsyu.Size = New System.Drawing.Size(30, 22)
        Me.txtSHinsyu.TabIndex = 1
        Me.txtSHinsyu.Value = ""
        Me.txtSHinsyu.ZeroSuppress = False
        '
        'txtSSiyo
        '
        Me.txtSSiyo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSSiyo.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtSSiyo.Location = New System.Drawing.Point(181, 21)
        Me.txtSSiyo.MaxLength = 2
        Me.txtSSiyo.Name = "txtSSiyo"
        Me.txtSSiyo.Size = New System.Drawing.Size(25, 22)
        Me.txtSSiyo.TabIndex = 0
        '
        'btnSakujo
        '
        Me.btnSakujo.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSakujo.Location = New System.Drawing.Point(245, 56)
        Me.btnSakujo.Name = "btnSakujo"
        Me.btnSakujo.Size = New System.Drawing.Size(100, 30)
        Me.btnSakujo.TabIndex = 6
        Me.btnSakujo.Tag = ""
        Me.btnSakujo.Text = "▲削除(&D)"
        Me.btnSakujo.UseVisualStyleBackColor = True
        '
        'btnTuika
        '
        Me.btnTuika.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnTuika.Location = New System.Drawing.Point(135, 56)
        Me.btnTuika.Name = "btnTuika"
        Me.btnTuika.Size = New System.Drawing.Size(100, 30)
        Me.btnTuika.TabIndex = 5
        Me.btnTuika.Tag = ""
        Me.btnTuika.Text = "▼追加(&A)"
        Me.btnTuika.UseVisualStyleBackColor = True
        '
        'dgvSHinmei
        '
        Me.dgvSHinmei.AllowUserToAddRows = False
        Me.dgvSHinmei.AllowUserToDeleteRows = False
        Me.dgvSHinmei.AllowUserToResizeColumns = False
        Me.dgvSHinmei.AllowUserToResizeRows = False
        Me.dgvSHinmei.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSHinmei.ColumnHeadersVisible = False
        Me.dgvSHinmei.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cnSHinmeiCD, Me.cnSHinmei})
        Me.dgvSHinmei.Location = New System.Drawing.Point(30, 100)
        Me.dgvSHinmei.MultiSelect = False
        Me.dgvSHinmei.Name = "dgvSHinmei"
        Me.dgvSHinmei.ReadOnly = True
        Me.dgvSHinmei.RowHeadersVisible = False
        Me.dgvSHinmei.RowTemplate.Height = 21
        Me.dgvSHinmei.Size = New System.Drawing.Size(420, 129)
        Me.dgvSHinmei.TabIndex = 7
        '
        'cnSHinmeiCD
        '
        Me.cnSHinmeiCD.DataPropertyName = "dtSHinmeiCD"
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        Me.cnSHinmeiCD.DefaultCellStyle = DataGridViewCellStyle1
        Me.cnSHinmeiCD.HeaderText = "集計品名コード"
        Me.cnSHinmeiCD.MaxInputLength = 15
        Me.cnSHinmeiCD.Name = "cnSHinmeiCD"
        Me.cnSHinmeiCD.ReadOnly = True
        Me.cnSHinmeiCD.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnSHinmeiCD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnSHinmeiCD.TabStop = True
        '
        'cnSHinmei
        '
        Me.cnSHinmei.DataPropertyName = "dtSHinmei"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        Me.cnSHinmei.DefaultCellStyle = DataGridViewCellStyle2
        Me.cnSHinmei.HeaderText = "集計対象品名"
        Me.cnSHinmei.MaxInputLength = 50
        Me.cnSHinmei.Name = "cnSHinmei"
        Me.cnSHinmei.ReadOnly = True
        Me.cnSHinmei.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnSHinmei.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnSHinmei.TabStop = True
        Me.cnSHinmei.Width = 300
        '
        'btnModoru
        '
        Me.btnModoru.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnModoru.Location = New System.Drawing.Point(320, 244)
        Me.btnModoru.Name = "btnModoru"
        Me.btnModoru.Size = New System.Drawing.Size(130, 45)
        Me.btnModoru.TabIndex = 9
        Me.btnModoru.Text = "戻る(&B)"
        Me.btnModoru.UseVisualStyleBackColor = True
        '
        'lblKensuu
        '
        Me.lblKensuu.BackColor = System.Drawing.SystemColors.Control
        Me.lblKensuu.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKensuu.Location = New System.Drawing.Point(393, 74)
        Me.lblKensuu.Name = "lblKensuu"
        Me.lblKensuu.Size = New System.Drawing.Size(57, 22)
        Me.lblKensuu.TabIndex = 1301
        Me.lblKensuu.Text = "0件"
        Me.lblKensuu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnKakutei
        '
        Me.btnKakutei.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnKakutei.Location = New System.Drawing.Point(184, 244)
        Me.btnKakutei.Name = "btnKakutei"
        Me.btnKakutei.Size = New System.Drawing.Size(130, 45)
        Me.btnKakutei.TabIndex = 8
        Me.btnKakutei.Text = "確定(&E)"
        Me.btnKakutei.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(30, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(145, 22)
        Me.Label1.TabIndex = 1302
        Me.Label1.Text = "集計対象品名コード"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ZM121S_SyuukeiTouroku
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(479, 303)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnKakutei)
        Me.Controls.Add(Me.lblKensuu)
        Me.Controls.Add(Me.btnModoru)
        Me.Controls.Add(Me.dgvSHinmei)
        Me.Controls.Add(Me.btnSakujo)
        Me.Controls.Add(Me.btnTuika)
        Me.Controls.Add(Me.txtSColor)
        Me.Controls.Add(Me.txtSSize)
        Me.Controls.Add(Me.txtSSensin)
        Me.Controls.Add(Me.txtSHinsyu)
        Me.Controls.Add(Me.txtSSiyo)
        Me.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.Name = "ZM121S_SyuukeiTouroku"
        Me.Text = "集計対象品名登録"
        CType(Me.dgvSHinmei, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtSColor As CustomControl.TextBoxNum
    Friend WithEvents txtSSize As CustomControl.TextBoxNum
    Friend WithEvents txtSSensin As CustomControl.TextBoxNum
    Friend WithEvents txtSHinsyu As CustomControl.TextBoxNum
    Friend WithEvents txtSSiyo As System.Windows.Forms.TextBox
    Friend WithEvents btnSakujo As System.Windows.Forms.Button
    Friend WithEvents btnTuika As System.Windows.Forms.Button
    Friend WithEvents dgvSHinmei As CustomTabStopDataGridView.TabStop.UtilTabStopDataGridView
    Friend WithEvents btnModoru As System.Windows.Forms.Button
    Friend WithEvents lblKensuu As System.Windows.Forms.Label
    Friend WithEvents btnKakutei As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cnSHinmeiCD As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnSHinmei As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
End Class
