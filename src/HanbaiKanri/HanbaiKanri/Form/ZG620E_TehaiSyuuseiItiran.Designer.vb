<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ZG620E_TehaiSyuuseiItiran
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ZG620E_TehaiSyuuseiItiran))
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtTehaiNo = New System.Windows.Forms.TextBox
        Me.txtColor = New CustomControl.TextBoxNum
        Me.txtSize = New CustomControl.TextBoxNum
        Me.txtSensinsuu = New CustomControl.TextBoxNum
        Me.txtHinsyuCD = New CustomControl.TextBoxNum
        Me.txtSiyouCD = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtHinmei = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtKibouFrom = New CustomControl.TextBoxDate
        Me.txtKibouTo = New CustomControl.TextBoxDate
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblSyori = New System.Windows.Forms.Label
        Me.lblKeikaku = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.btnKensaku = New System.Windows.Forms.Button
        Me.dgvTehaiData = New CustomTabStopDataGridView.TabStop.UtilTabStopDataGridView
        Me.cnTaisyogai = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnTehaiNo = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnSyuttaibi = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnHinmeiCD = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnHinmei = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnTehaiSuuryou = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnTantyou = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnJousuu = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnSiyousyoNo = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.btnModoru = New System.Windows.Forms.Button
        Me.btnSyuusei = New System.Windows.Forms.Button
        Me.lblKensuu = New System.Windows.Forms.Label
        Me.btnInsatu = New System.Windows.Forms.Button
        CType(Me.dgvTehaiData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(30, 30)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(124, 22)
        Me.Label4.TabIndex = 114
        Me.Label4.Text = "手配No."
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTehaiNo
        '
        Me.txtTehaiNo.BackColor = System.Drawing.Color.White
        Me.txtTehaiNo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtTehaiNo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtTehaiNo.Location = New System.Drawing.Point(30, 51)
        Me.txtTehaiNo.MaxLength = 5
        Me.txtTehaiNo.Name = "txtTehaiNo"
        Me.txtTehaiNo.Size = New System.Drawing.Size(124, 22)
        Me.txtTehaiNo.TabIndex = 0
        '
        'txtColor
        '
        Me.txtColor.CommaFormat = False
        Me.txtColor.DecimalDigit = CType(0, Short)
        Me.txtColor.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtColor.ForeColor = System.Drawing.Color.Black
        Me.txtColor.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtColor.IntegralDigit = CType(3, Short)
        Me.txtColor.Location = New System.Drawing.Point(301, 51)
        Me.txtColor.MaxLength = 3
        Me.txtColor.MinusInput = False
        Me.txtColor.Name = "txtColor"
        Me.txtColor.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57)}
        Me.txtColor.Size = New System.Drawing.Size(30, 22)
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
        Me.txtSize.Location = New System.Drawing.Point(270, 51)
        Me.txtSize.MaxLength = 2
        Me.txtSize.MinusInput = False
        Me.txtSize.Name = "txtSize"
        Me.txtSize.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57)}
        Me.txtSize.Size = New System.Drawing.Size(25, 22)
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
        Me.txtSensinsuu.Location = New System.Drawing.Point(234, 51)
        Me.txtSensinsuu.MaxLength = 3
        Me.txtSensinsuu.MinusInput = False
        Me.txtSensinsuu.Name = "txtSensinsuu"
        Me.txtSensinsuu.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57)}
        Me.txtSensinsuu.Size = New System.Drawing.Size(30, 22)
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
        Me.txtHinsyuCD.Location = New System.Drawing.Point(200, 51)
        Me.txtHinsyuCD.MaxLength = 3
        Me.txtHinsyuCD.MinusInput = False
        Me.txtHinsyuCD.Name = "txtHinsyuCD"
        Me.txtHinsyuCD.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57)}
        Me.txtHinsyuCD.Size = New System.Drawing.Size(30, 22)
        Me.txtHinsyuCD.TabIndex = 2
        Me.txtHinsyuCD.Value = ""
        Me.txtHinsyuCD.ZeroSuppress = False
        '
        'txtSiyouCD
        '
        Me.txtSiyouCD.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSiyouCD.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSiyouCD.Location = New System.Drawing.Point(170, 51)
        Me.txtSiyouCD.MaxLength = 2
        Me.txtSiyouCD.Name = "txtSiyouCD"
        Me.txtSiyouCD.Size = New System.Drawing.Size(25, 22)
        Me.txtSiyouCD.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(170, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(161, 22)
        Me.Label1.TabIndex = 1302
        Me.Label1.Text = "品名コード"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(345, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(292, 22)
        Me.Label2.TabIndex = 1303
        Me.Label2.Text = "品名"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtHinmei
        '
        Me.txtHinmei.BackColor = System.Drawing.Color.White
        Me.txtHinmei.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtHinmei.Location = New System.Drawing.Point(345, 51)
        Me.txtHinmei.MaxLength = 35
        Me.txtHinmei.Name = "txtHinmei"
        Me.txtHinmei.Size = New System.Drawing.Size(292, 22)
        Me.txtHinmei.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(30, 85)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(232, 22)
        Me.Label3.TabIndex = 1305
        Me.Label3.Text = "希望出来日"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.Location = New System.Drawing.Point(136, 105)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(24, 22)
        Me.Label13.TabIndex = 1308
        Me.Label13.Text = "～"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtKibouFrom
        '
        Me.txtKibouFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKibouFrom.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtKibouFrom.Location = New System.Drawing.Point(30, 106)
        Me.txtKibouFrom.Mask = "0000/00/00"
        Me.txtKibouFrom.Name = "txtKibouFrom"
        Me.txtKibouFrom.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57), Global.Microsoft.VisualBasic.ChrW(47), Global.Microsoft.VisualBasic.ChrW(95)}
        Me.txtKibouFrom.Size = New System.Drawing.Size(100, 22)
        Me.txtKibouFrom.TabIndex = 7
        Me.txtKibouFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtKibouTo
        '
        Me.txtKibouTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKibouTo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtKibouTo.Location = New System.Drawing.Point(162, 106)
        Me.txtKibouTo.Mask = "0000/00/00"
        Me.txtKibouTo.Name = "txtKibouTo"
        Me.txtKibouTo.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57), Global.Microsoft.VisualBasic.ChrW(47), Global.Microsoft.VisualBasic.ChrW(95)}
        Me.txtKibouTo.Size = New System.Drawing.Size(100, 22)
        Me.txtKibouTo.TabIndex = 8
        Me.txtKibouTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(1060, 30)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(73, 22)
        Me.Label7.TabIndex = 1334
        Me.Label7.Text = "計画年月"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSyori
        '
        Me.lblSyori.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblSyori.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSyori.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!)
        Me.lblSyori.Location = New System.Drawing.Point(939, 30)
        Me.lblSyori.Name = "lblSyori"
        Me.lblSyori.Size = New System.Drawing.Size(100, 22)
        Me.lblSyori.TabIndex = 1333
        Me.lblSyori.Text = "2010/03"
        Me.lblSyori.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblKeikaku
        '
        Me.lblKeikaku.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblKeikaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblKeikaku.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!)
        Me.lblKeikaku.Location = New System.Drawing.Point(1140, 30)
        Me.lblKeikaku.Name = "lblKeikaku"
        Me.lblKeikaku.Size = New System.Drawing.Size(100, 22)
        Me.lblKeikaku.TabIndex = 1332
        Me.lblKeikaku.Text = "2010/04"
        Me.lblKeikaku.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(859, 30)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(73, 22)
        Me.Label11.TabIndex = 1331
        Me.Label11.Text = "処理年月"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnKensaku
        '
        Me.btnKensaku.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnKensaku.Location = New System.Drawing.Point(690, 30)
        Me.btnKensaku.Name = "btnKensaku"
        Me.btnKensaku.Size = New System.Drawing.Size(130, 45)
        Me.btnKensaku.TabIndex = 11
        Me.btnKensaku.Text = "検索(&S)"
        Me.btnKensaku.UseVisualStyleBackColor = True
        '
        'dgvTehaiData
        '
        Me.dgvTehaiData.AllowUserToAddRows = False
        Me.dgvTehaiData.AllowUserToResizeColumns = False
        Me.dgvTehaiData.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvTehaiData.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvTehaiData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTehaiData.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cnTaisyogai, Me.cnTehaiNo, Me.cnSyuttaibi, Me.cnHinmeiCD, Me.cnHinmei, Me.cnTehaiSuuryou, Me.cnTantyou, Me.cnJousuu, Me.cnSiyousyoNo})
        Me.dgvTehaiData.Location = New System.Drawing.Point(30, 147)
        Me.dgvTehaiData.MultiSelect = False
        Me.dgvTehaiData.Name = "dgvTehaiData"
        Me.dgvTehaiData.RowHeadersVisible = False
        Me.dgvTehaiData.RowTemplate.Height = 21
        Me.dgvTehaiData.Size = New System.Drawing.Size(1210, 733)
        Me.dgvTehaiData.TabIndex = 12
        '
        'cnTaisyogai
        '
        Me.cnTaisyogai.DataPropertyName = "dtTaisyogai"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        Me.cnTaisyogai.DefaultCellStyle = DataGridViewCellStyle2
        Me.cnTaisyogai.HeaderText = "対象外"
        Me.cnTaisyogai.Name = "cnTaisyogai"
        Me.cnTaisyogai.ReadOnly = True
        Me.cnTaisyogai.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnTaisyogai.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnTaisyogai.TabStop = True
        Me.cnTaisyogai.Width = 50
        '
        'cnTehaiNo
        '
        Me.cnTehaiNo.DataPropertyName = "dtTehaiNo"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        Me.cnTehaiNo.DefaultCellStyle = DataGridViewCellStyle3
        Me.cnTehaiNo.HeaderText = "手配No."
        Me.cnTehaiNo.Name = "cnTehaiNo"
        Me.cnTehaiNo.ReadOnly = True
        Me.cnTehaiNo.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnTehaiNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnTehaiNo.TabStop = False
        Me.cnTehaiNo.Width = 80
        '
        'cnSyuttaibi
        '
        Me.cnSyuttaibi.DataPropertyName = "dtSyuttaibi"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle4.NullValue = Nothing
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        Me.cnSyuttaibi.DefaultCellStyle = DataGridViewCellStyle4
        Me.cnSyuttaibi.HeaderText = "希望出来日"
        Me.cnSyuttaibi.Name = "cnSyuttaibi"
        Me.cnSyuttaibi.ReadOnly = True
        Me.cnSyuttaibi.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnSyuttaibi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnSyuttaibi.TabStop = False
        Me.cnSyuttaibi.Width = 105
        '
        'cnHinmeiCD
        '
        Me.cnHinmeiCD.DataPropertyName = "dtHinmeiCD"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        Me.cnHinmeiCD.DefaultCellStyle = DataGridViewCellStyle5
        Me.cnHinmeiCD.HeaderText = "品名コード"
        Me.cnHinmeiCD.MaxInputLength = 15
        Me.cnHinmeiCD.Name = "cnHinmeiCD"
        Me.cnHinmeiCD.ReadOnly = True
        Me.cnHinmeiCD.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnHinmeiCD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnHinmeiCD.TabStop = False
        Me.cnHinmeiCD.Width = 130
        '
        'cnHinmei
        '
        Me.cnHinmei.DataPropertyName = "dtHinmei"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle6.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black
        Me.cnHinmei.DefaultCellStyle = DataGridViewCellStyle6
        Me.cnHinmei.HeaderText = "品名"
        Me.cnHinmei.MaxInputLength = 35
        Me.cnHinmei.Name = "cnHinmei"
        Me.cnHinmei.ReadOnly = True
        Me.cnHinmei.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnHinmei.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnHinmei.TabStop = False
        Me.cnHinmei.Width = 335
        '
        'cnTehaiSuuryou
        '
        Me.cnTehaiSuuryou.DataPropertyName = "dtTehaiSuuryou"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle7.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle7.Format = "N0"
        DataGridViewCellStyle7.NullValue = Nothing
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black
        Me.cnTehaiSuuryou.DefaultCellStyle = DataGridViewCellStyle7
        Me.cnTehaiSuuryou.HeaderText = "手配数量"
        Me.cnTehaiSuuryou.Name = "cnTehaiSuuryou"
        Me.cnTehaiSuuryou.ReadOnly = True
        Me.cnTehaiSuuryou.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnTehaiSuuryou.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnTehaiSuuryou.TabStop = False
        '
        'cnTantyou
        '
        Me.cnTantyou.DataPropertyName = "dtTantyou"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle8.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle8.Format = "N0"
        DataGridViewCellStyle8.NullValue = Nothing
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black
        Me.cnTantyou.DefaultCellStyle = DataGridViewCellStyle8
        Me.cnTantyou.HeaderText = "単長"
        Me.cnTantyou.Name = "cnTantyou"
        Me.cnTantyou.ReadOnly = True
        Me.cnTantyou.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnTantyou.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnTantyou.TabStop = False
        '
        'cnJousuu
        '
        Me.cnJousuu.DataPropertyName = "dtJousuu"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle9.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle9.Format = "N0"
        DataGridViewCellStyle9.NullValue = Nothing
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black
        Me.cnJousuu.DefaultCellStyle = DataGridViewCellStyle9
        Me.cnJousuu.HeaderText = "条数"
        Me.cnJousuu.Name = "cnJousuu"
        Me.cnJousuu.ReadOnly = True
        Me.cnJousuu.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnJousuu.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnJousuu.TabStop = False
        '
        'cnSiyousyoNo
        '
        Me.cnSiyousyoNo.DataPropertyName = "dtSiyousyoNo"
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle10.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black
        Me.cnSiyousyoNo.DefaultCellStyle = DataGridViewCellStyle10
        Me.cnSiyousyoNo.HeaderText = "仕様書番号"
        Me.cnSiyousyoNo.Name = "cnSiyousyoNo"
        Me.cnSiyousyoNo.ReadOnly = True
        Me.cnSiyousyoNo.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnSiyousyoNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnSiyousyoNo.TabStop = False
        Me.cnSiyousyoNo.Width = 190
        '
        'btnModoru
        '
        Me.btnModoru.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnModoru.Location = New System.Drawing.Point(1110, 900)
        Me.btnModoru.Name = "btnModoru"
        Me.btnModoru.Size = New System.Drawing.Size(130, 45)
        Me.btnModoru.TabIndex = 16
        Me.btnModoru.Text = "戻る(&B)"
        Me.btnModoru.UseVisualStyleBackColor = True
        '
        'btnSyuusei
        '
        Me.btnSyuusei.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSyuusei.Location = New System.Drawing.Point(30, 900)
        Me.btnSyuusei.Name = "btnSyuusei"
        Me.btnSyuusei.Size = New System.Drawing.Size(130, 45)
        Me.btnSyuusei.TabIndex = 13
        Me.btnSyuusei.Text = "修正(&U)"
        Me.btnSyuusei.UseVisualStyleBackColor = True
        '
        'lblKensuu
        '
        Me.lblKensuu.BackColor = System.Drawing.SystemColors.Control
        Me.lblKensuu.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKensuu.Location = New System.Drawing.Point(1171, 122)
        Me.lblKensuu.Name = "lblKensuu"
        Me.lblKensuu.Size = New System.Drawing.Size(69, 22)
        Me.lblKensuu.TabIndex = 1340
        Me.lblKensuu.Text = "0件"
        Me.lblKensuu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnInsatu
        '
        Me.btnInsatu.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnInsatu.Location = New System.Drawing.Point(170, 900)
        Me.btnInsatu.Name = "btnInsatu"
        Me.btnInsatu.Size = New System.Drawing.Size(182, 45)
        Me.btnInsatu.TabIndex = 15
        Me.btnInsatu.Text = "在庫補充リスト出力(&K)"
        Me.btnInsatu.UseVisualStyleBackColor = True
        '
        'ZG620E_TehaiSyuuseiItiran
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1274, 967)
        Me.Controls.Add(Me.btnInsatu)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblKensuu)
        Me.Controls.Add(Me.btnSyuusei)
        Me.Controls.Add(Me.btnModoru)
        Me.Controls.Add(Me.dgvTehaiData)
        Me.Controls.Add(Me.btnKensaku)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.lblSyori)
        Me.Controls.Add(Me.lblKeikaku)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtKibouTo)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtKibouFrom)
        Me.Controls.Add(Me.txtHinmei)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtColor)
        Me.Controls.Add(Me.txtSize)
        Me.Controls.Add(Me.txtSensinsuu)
        Me.Controls.Add(Me.txtHinsyuCD)
        Me.Controls.Add(Me.txtSiyouCD)
        Me.Controls.Add(Me.txtTehaiNo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ZG620E_TehaiSyuuseiItiran"
        Me.Text = "手配データ修正(一覧)"
        CType(Me.dgvTehaiData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtTehaiNo As System.Windows.Forms.TextBox
    Friend WithEvents txtColor As CustomControl.TextBoxNum
    Friend WithEvents txtSize As CustomControl.TextBoxNum
    Friend WithEvents txtSensinsuu As CustomControl.TextBoxNum
    Friend WithEvents txtHinsyuCD As CustomControl.TextBoxNum
    Friend WithEvents txtSiyouCD As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtHinmei As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtKibouFrom As CustomControl.TextBoxDate
    Friend WithEvents txtKibouTo As CustomControl.TextBoxDate
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblSyori As System.Windows.Forms.Label
    Friend WithEvents lblKeikaku As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnKensaku As System.Windows.Forms.Button
    Friend WithEvents dgvTehaiData As CustomTabStopDataGridView.TabStop.UtilTabStopDataGridView
    Friend WithEvents btnModoru As System.Windows.Forms.Button
    Friend WithEvents btnSyuusei As System.Windows.Forms.Button
    Friend WithEvents lblKensuu As System.Windows.Forms.Label
    Friend WithEvents btnInsatu As System.Windows.Forms.Button
    Friend WithEvents cnTaisyogai As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnTehaiNo As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnSyuttaibi As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnHinmeiCD As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnHinmei As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnTehaiSuuryou As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnTantyou As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnJousuu As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnSiyousyoNo As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
End Class
