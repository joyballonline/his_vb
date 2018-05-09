<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ZG730Q_FukaYamadumiKoutei
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
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ZG730Q_FukaYamadumiKoutei))
        Me.Label2 = New System.Windows.Forms.Label
        Me.cboKoutei = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cboKikai = New System.Windows.Forms.ComboBox
        Me.btnKensaku = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblSyori = New System.Windows.Forms.Label
        Me.lblKeikaku = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.dgvFukaKakunin = New CustomTabStopDataGridView.TabStop.UtilTabStopDataGridView
        Me.cnMeisaiChk = New CustomTabStopDataGridView.TabStop.TabStopButtonColumn
        Me.cnKoutei = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnMashineName = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnSeisanNouryoku = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnMCHGoukei = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnMCHHakkou = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnMCHMihakkou = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnMCHGetujiZaiko = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnOverMCH = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnMHGoukei = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnMHHakkou = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnMihakkou = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnMHGetujiZaiko = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.btnModoru = New System.Windows.Forms.Button
        Me.btnExcel = New System.Windows.Forms.Button
        Me.lblKensuu = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.btnSeisan = New System.Windows.Forms.Button
        Me.lblSeisanSettei = New System.Windows.Forms.Label
        Me.lblSeisanDate = New System.Windows.Forms.Label
        CType(Me.dgvFukaKakunin, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(30, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(94, 22)
        Me.Label2.TabIndex = 1354
        Me.Label2.Text = "工程名コード"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboKoutei
        '
        Me.cboKoutei.BackColor = System.Drawing.Color.White
        Me.cboKoutei.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboKoutei.DropDownWidth = 95
        Me.cboKoutei.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cboKoutei.FormattingEnabled = True
        Me.cboKoutei.Location = New System.Drawing.Point(30, 51)
        Me.cboKoutei.Name = "cboKoutei"
        Me.cboKoutei.Size = New System.Drawing.Size(94, 23)
        Me.cboKoutei.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(130, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 22)
        Me.Label1.TabIndex = 1356
        Me.Label1.Text = "機械略記号"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboKikai
        '
        Me.cboKikai.BackColor = System.Drawing.Color.White
        Me.cboKikai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboKikai.DropDownWidth = 99
        Me.cboKikai.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cboKikai.FormattingEnabled = True
        Me.cboKikai.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.cboKikai.Location = New System.Drawing.Point(130, 51)
        Me.cboKikai.Name = "cboKikai"
        Me.cboKikai.Size = New System.Drawing.Size(99, 23)
        Me.cboKikai.TabIndex = 2
        '
        'btnKensaku
        '
        Me.btnKensaku.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnKensaku.Location = New System.Drawing.Point(235, 29)
        Me.btnKensaku.Name = "btnKensaku"
        Me.btnKensaku.Size = New System.Drawing.Size(130, 45)
        Me.btnKensaku.TabIndex = 3
        Me.btnKensaku.Text = "検索(&S)"
        Me.btnKensaku.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(855, 28)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 22)
        Me.Label3.TabIndex = 1361
        Me.Label3.Text = "計画年月"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSyori
        '
        Me.lblSyori.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblSyori.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSyori.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSyori.Location = New System.Drawing.Point(749, 28)
        Me.lblSyori.Name = "lblSyori"
        Me.lblSyori.Size = New System.Drawing.Size(100, 22)
        Me.lblSyori.TabIndex = 1360
        Me.lblSyori.Text = "2010/09"
        Me.lblSyori.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblKeikaku
        '
        Me.lblKeikaku.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblKeikaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblKeikaku.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKeikaku.Location = New System.Drawing.Point(934, 29)
        Me.lblKeikaku.Name = "lblKeikaku"
        Me.lblKeikaku.Size = New System.Drawing.Size(100, 22)
        Me.lblKeikaku.TabIndex = 1359
        Me.lblKeikaku.Text = "2010/08"
        Me.lblKeikaku.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(670, 28)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(73, 22)
        Me.Label11.TabIndex = 1358
        Me.Label11.Text = "処理年月"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dgvFukaKakunin
        '
        Me.dgvFukaKakunin.AllowUserToAddRows = False
        Me.dgvFukaKakunin.AllowUserToDeleteRows = False
        Me.dgvFukaKakunin.AllowUserToResizeColumns = False
        Me.dgvFukaKakunin.AllowUserToResizeRows = False
        Me.dgvFukaKakunin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvFukaKakunin.ColumnHeadersVisible = False
        Me.dgvFukaKakunin.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cnMeisaiChk, Me.cnKoutei, Me.cnMashineName, Me.cnSeisanNouryoku, Me.cnMCHGoukei, Me.cnMCHHakkou, Me.cnMCHMihakkou, Me.cnMCHGetujiZaiko, Me.cnOverMCH, Me.cnMHGoukei, Me.cnMHHakkou, Me.cnMihakkou, Me.cnMHGetujiZaiko})
        Me.dgvFukaKakunin.Location = New System.Drawing.Point(30, 164)
        Me.dgvFukaKakunin.MultiSelect = False
        Me.dgvFukaKakunin.Name = "dgvFukaKakunin"
        Me.dgvFukaKakunin.RowHeadersVisible = False
        Me.dgvFukaKakunin.RowTemplate.Height = 21
        Me.dgvFukaKakunin.Size = New System.Drawing.Size(1007, 423)
        Me.dgvFukaKakunin.TabIndex = 5
        '
        'cnMeisaiChk
        '
        Me.cnMeisaiChk.DataPropertyName = "dtMeisaiChk"
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText
        Me.cnMeisaiChk.DefaultCellStyle = DataGridViewCellStyle1
        Me.cnMeisaiChk.HeaderText = ""
        Me.cnMeisaiChk.Name = "cnMeisaiChk"
        Me.cnMeisaiChk.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnMeisaiChk.TabStop = True
        Me.cnMeisaiChk.Width = 26
        '
        'cnKoutei
        '
        Me.cnKoutei.DataPropertyName = "dtKoutei"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        Me.cnKoutei.DefaultCellStyle = DataGridViewCellStyle2
        Me.cnKoutei.HeaderText = " "
        Me.cnKoutei.Name = "cnKoutei"
        Me.cnKoutei.ReadOnly = True
        Me.cnKoutei.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnKoutei.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnKoutei.TabStop = False
        Me.cnKoutei.Width = 80
        '
        'cnMashineName
        '
        Me.cnMashineName.DataPropertyName = "dtMashineName"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        Me.cnMashineName.DefaultCellStyle = DataGridViewCellStyle3
        Me.cnMashineName.HeaderText = ""
        Me.cnMashineName.Name = "cnMashineName"
        Me.cnMashineName.ReadOnly = True
        Me.cnMashineName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnMashineName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnMashineName.TabStop = False
        Me.cnMashineName.Width = 80
        '
        'cnSeisanNouryoku
        '
        Me.cnSeisanNouryoku.DataPropertyName = "dtSeisanNouryoku"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle4.Format = "N1"
        DataGridViewCellStyle4.NullValue = Nothing
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        Me.cnSeisanNouryoku.DefaultCellStyle = DataGridViewCellStyle4
        Me.cnSeisanNouryoku.HeaderText = ""
        Me.cnSeisanNouryoku.Name = "cnSeisanNouryoku"
        Me.cnSeisanNouryoku.ReadOnly = True
        Me.cnSeisanNouryoku.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnSeisanNouryoku.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnSeisanNouryoku.TabStop = False
        Me.cnSeisanNouryoku.Width = 80
        '
        'cnMCHGoukei
        '
        Me.cnMCHGoukei.DataPropertyName = "dtMCHGoukei"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle5.Format = "N1"
        DataGridViewCellStyle5.NullValue = Nothing
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        Me.cnMCHGoukei.DefaultCellStyle = DataGridViewCellStyle5
        Me.cnMCHGoukei.HeaderText = ""
        Me.cnMCHGoukei.Name = "cnMCHGoukei"
        Me.cnMCHGoukei.ReadOnly = True
        Me.cnMCHGoukei.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnMCHGoukei.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnMCHGoukei.TabStop = False
        Me.cnMCHGoukei.Width = 80
        '
        'cnMCHHakkou
        '
        Me.cnMCHHakkou.DataPropertyName = "dtMCHHakkou"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle6.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle6.Format = "N1"
        DataGridViewCellStyle6.NullValue = Nothing
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black
        Me.cnMCHHakkou.DefaultCellStyle = DataGridViewCellStyle6
        Me.cnMCHHakkou.HeaderText = ""
        Me.cnMCHHakkou.Name = "cnMCHHakkou"
        Me.cnMCHHakkou.ReadOnly = True
        Me.cnMCHHakkou.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnMCHHakkou.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnMCHHakkou.TabStop = False
        Me.cnMCHHakkou.Width = 80
        '
        'cnMCHMihakkou
        '
        Me.cnMCHMihakkou.DataPropertyName = "dtMCHMihakkou"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle7.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle7.Format = "N1"
        DataGridViewCellStyle7.NullValue = Nothing
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black
        Me.cnMCHMihakkou.DefaultCellStyle = DataGridViewCellStyle7
        Me.cnMCHMihakkou.HeaderText = ""
        Me.cnMCHMihakkou.Name = "cnMCHMihakkou"
        Me.cnMCHMihakkou.ReadOnly = True
        Me.cnMCHMihakkou.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnMCHMihakkou.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnMCHMihakkou.TabStop = False
        Me.cnMCHMihakkou.Width = 80
        '
        'cnMCHGetujiZaiko
        '
        Me.cnMCHGetujiZaiko.DataPropertyName = "dtMCHGetujiZaiko"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle8.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle8.Format = "N1"
        DataGridViewCellStyle8.NullValue = Nothing
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black
        Me.cnMCHGetujiZaiko.DefaultCellStyle = DataGridViewCellStyle8
        Me.cnMCHGetujiZaiko.HeaderText = ""
        Me.cnMCHGetujiZaiko.Name = "cnMCHGetujiZaiko"
        Me.cnMCHGetujiZaiko.ReadOnly = True
        Me.cnMCHGetujiZaiko.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnMCHGetujiZaiko.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnMCHGetujiZaiko.TabStop = False
        Me.cnMCHGetujiZaiko.Width = 80
        '
        'cnOverMCH
        '
        Me.cnOverMCH.DataPropertyName = "dtOverMCH"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle9.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle9.Format = "N1"
        DataGridViewCellStyle9.NullValue = Nothing
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black
        Me.cnOverMCH.DefaultCellStyle = DataGridViewCellStyle9
        Me.cnOverMCH.HeaderText = ""
        Me.cnOverMCH.Name = "cnOverMCH"
        Me.cnOverMCH.ReadOnly = True
        Me.cnOverMCH.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnOverMCH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnOverMCH.TabStop = False
        Me.cnOverMCH.Width = 81
        '
        'cnMHGoukei
        '
        Me.cnMHGoukei.DataPropertyName = "dtMHGoukei"
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle10.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle10.Format = "N1"
        DataGridViewCellStyle10.NullValue = Nothing
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black
        Me.cnMHGoukei.DefaultCellStyle = DataGridViewCellStyle10
        Me.cnMHGoukei.HeaderText = ""
        Me.cnMHGoukei.Name = "cnMHGoukei"
        Me.cnMHGoukei.ReadOnly = True
        Me.cnMHGoukei.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnMHGoukei.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnMHGoukei.TabStop = False
        Me.cnMHGoukei.Width = 80
        '
        'cnMHHakkou
        '
        Me.cnMHHakkou.DataPropertyName = "dtMHHakkou"
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle11.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle11.Format = "N1"
        DataGridViewCellStyle11.NullValue = Nothing
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black
        Me.cnMHHakkou.DefaultCellStyle = DataGridViewCellStyle11
        Me.cnMHHakkou.HeaderText = ""
        Me.cnMHHakkou.Name = "cnMHHakkou"
        Me.cnMHHakkou.ReadOnly = True
        Me.cnMHHakkou.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnMHHakkou.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnMHHakkou.TabStop = False
        Me.cnMHHakkou.Width = 80
        '
        'cnMihakkou
        '
        Me.cnMihakkou.DataPropertyName = "dtMHMihakkou"
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle12.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle12.Format = "N1"
        DataGridViewCellStyle12.NullValue = Nothing
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black
        Me.cnMihakkou.DefaultCellStyle = DataGridViewCellStyle12
        Me.cnMihakkou.HeaderText = ""
        Me.cnMihakkou.Name = "cnMihakkou"
        Me.cnMihakkou.ReadOnly = True
        Me.cnMihakkou.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnMihakkou.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnMihakkou.TabStop = False
        Me.cnMihakkou.Width = 80
        '
        'cnMHGetujiZaiko
        '
        Me.cnMHGetujiZaiko.DataPropertyName = "dtMHGetujiZaiko"
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle13.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle13.Format = "N1"
        DataGridViewCellStyle13.NullValue = Nothing
        DataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Black
        Me.cnMHGetujiZaiko.DefaultCellStyle = DataGridViewCellStyle13
        Me.cnMHGetujiZaiko.HeaderText = ""
        Me.cnMHGetujiZaiko.Name = "cnMHGetujiZaiko"
        Me.cnMHGetujiZaiko.ReadOnly = True
        Me.cnMHGetujiZaiko.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnMHGetujiZaiko.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnMHGetujiZaiko.TabStop = False
        Me.cnMHGetujiZaiko.Width = 80
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(30, 99)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(28, 66)
        Me.Label6.TabIndex = 1372
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(57, 99)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(81, 66)
        Me.Label7.TabIndex = 1373
        Me.Label7.Text = "工程名" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "コード"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(137, 99)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(81, 66)
        Me.Label8.TabIndex = 1374
        Me.Label8.Text = "機械" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "略記号"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label9.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(217, 121)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(81, 44)
        Me.Label9.TabIndex = 1375
        Me.Label9.Text = "生産能力MCH"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label10.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(297, 121)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(81, 44)
        Me.Label10.TabIndex = 1376
        Me.Label10.Text = "合計"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label12.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.Location = New System.Drawing.Point(217, 99)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(482, 23)
        Me.Label12.TabIndex = 1377
        Me.Label12.Text = "山積MCH"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label13.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.Location = New System.Drawing.Point(377, 121)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(81, 44)
        Me.Label13.TabIndex = 1378
        Me.Label13.Text = "製作伝票発行分"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label14.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label14.Location = New System.Drawing.Point(457, 121)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(81, 44)
        Me.Label14.TabIndex = 1379
        Me.Label14.Text = "製作伝票未発行分"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label15.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label15.Location = New System.Drawing.Point(537, 121)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(81, 44)
        Me.Label15.TabIndex = 1380
        Me.Label15.Text = "　月　次　在庫分"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label16.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label16.Location = New System.Drawing.Point(698, 121)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(81, 44)
        Me.Label16.TabIndex = 1381
        Me.Label16.Text = "合計"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label17.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label17.Location = New System.Drawing.Point(778, 121)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(81, 44)
        Me.Label17.TabIndex = 1382
        Me.Label17.Text = "製作伝票発行分"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label18.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label18.Location = New System.Drawing.Point(858, 121)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(81, 44)
        Me.Label18.TabIndex = 1383
        Me.Label18.Text = "製作伝票未発行分"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label19.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label19.Location = New System.Drawing.Point(938, 121)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(81, 44)
        Me.Label19.TabIndex = 1384
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label20.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label20.Location = New System.Drawing.Point(617, 121)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(82, 44)
        Me.Label20.TabIndex = 1385
        Me.Label20.Text = "オーバーMCH"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label21.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label21.Location = New System.Drawing.Point(698, 99)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(321, 23)
        Me.Label21.TabIndex = 1386
        Me.Label21.Text = "山積MH"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnModoru
        '
        Me.btnModoru.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnModoru.Location = New System.Drawing.Point(907, 596)
        Me.btnModoru.Name = "btnModoru"
        Me.btnModoru.Size = New System.Drawing.Size(130, 45)
        Me.btnModoru.TabIndex = 7
        Me.btnModoru.Text = "戻る(&B)"
        Me.btnModoru.UseVisualStyleBackColor = True
        '
        'btnExcel
        '
        Me.btnExcel.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnExcel.Location = New System.Drawing.Point(30, 596)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(130, 45)
        Me.btnExcel.TabIndex = 6
        Me.btnExcel.Text = "EXCEL(&K)"
        Me.btnExcel.UseVisualStyleBackColor = True
        '
        'lblKensuu
        '
        Me.lblKensuu.BackColor = System.Drawing.SystemColors.Control
        Me.lblKensuu.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKensuu.Location = New System.Drawing.Point(968, 77)
        Me.lblKensuu.Name = "lblKensuu"
        Me.lblKensuu.Size = New System.Drawing.Size(69, 22)
        Me.lblKensuu.TabIndex = 1389
        Me.lblKensuu.Text = "9999件"
        Me.lblKensuu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(549, 126)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 35)
        Me.Label4.TabIndex = 1390
        Me.Label4.Text = " 月次 在庫分"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(949, 126)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 35)
        Me.Label5.TabIndex = 1391
        Me.Label5.Text = " 月次 在庫分"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnSeisan
        '
        Me.btnSeisan.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSeisan.Location = New System.Drawing.Point(381, 28)
        Me.btnSeisan.Name = "btnSeisan"
        Me.btnSeisan.Size = New System.Drawing.Size(140, 45)
        Me.btnSeisan.TabIndex = 4
        Me.btnSeisan.Text = "生産能力設定(&N)"
        Me.btnSeisan.UseVisualStyleBackColor = True
        '
        'lblSeisanSettei
        '
        Me.lblSeisanSettei.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblSeisanSettei.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSeisanSettei.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSeisanSettei.Location = New System.Drawing.Point(527, 28)
        Me.lblSeisanSettei.Name = "lblSeisanSettei"
        Me.lblSeisanSettei.Size = New System.Drawing.Size(140, 22)
        Me.lblSeisanSettei.TabIndex = 1393
        Me.lblSeisanSettei.Text = "設定あり"
        Me.lblSeisanSettei.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSeisanDate
        '
        Me.lblSeisanDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblSeisanDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSeisanDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSeisanDate.Location = New System.Drawing.Point(527, 51)
        Me.lblSeisanDate.Name = "lblSeisanDate"
        Me.lblSeisanDate.Size = New System.Drawing.Size(140, 22)
        Me.lblSeisanDate.TabIndex = 1394
        Me.lblSeisanDate.Text = "2010/08/07 10:00"
        Me.lblSeisanDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ZG730Q_FukaYamadumiKoutei
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1074, 653)
        Me.Controls.Add(Me.lblSeisanDate)
        Me.Controls.Add(Me.lblSeisanSettei)
        Me.Controls.Add(Me.btnSeisan)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblKensuu)
        Me.Controls.Add(Me.btnExcel)
        Me.Controls.Add(Me.btnModoru)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.dgvFukaKakunin)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblSyori)
        Me.Controls.Add(Me.lblKeikaku)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.btnKensaku)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboKikai)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboKoutei)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "ZG730Q_FukaYamadumiKoutei"
        Me.Text = "負荷山積結果確認(工程別)"
        CType(Me.dgvFukaKakunin, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboKoutei As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboKikai As System.Windows.Forms.ComboBox
    Friend WithEvents btnKensaku As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblSyori As System.Windows.Forms.Label
    Friend WithEvents lblKeikaku As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents dgvFukaKakunin As CustomTabStopDataGridView.TabStop.UtilTabStopDataGridView
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents btnModoru As System.Windows.Forms.Button
    Friend WithEvents btnExcel As System.Windows.Forms.Button
    Friend WithEvents lblKensuu As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnSeisan As System.Windows.Forms.Button
    Friend WithEvents lblSeisanSettei As System.Windows.Forms.Label
    Friend WithEvents lblSeisanDate As System.Windows.Forms.Label
    Friend WithEvents cnMeisaiChk As CustomTabStopDataGridView.TabStop.TabStopButtonColumn
    Friend WithEvents cnKoutei As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnMashineName As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnSeisanNouryoku As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnMCHGoukei As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnMCHHakkou As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnMCHMihakkou As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnMCHGetujiZaiko As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnOverMCH As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnMHGoukei As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnMHHakkou As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnMihakkou As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnMHGetujiZaiko As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
End Class
