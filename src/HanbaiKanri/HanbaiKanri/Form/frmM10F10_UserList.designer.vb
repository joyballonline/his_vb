<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmM10F10_UserList
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.cmdSyuturyoku = New System.Windows.Forms.Button()
        Me.cmdModoru = New System.Windows.Forms.Button()
        Me.cmdSakujo = New System.Windows.Forms.Button()
        Me.dgvIchiran = New CustomTabStopDataGridView.TabStop.UtilTabStopDataGridView()
        Me.cmdHenkou = New System.Windows.Forms.Button()
        Me.cmdTuika = New System.Windows.Forms.Button()
        Me.cmdFukusya = New System.Windows.Forms.Button()
        Me.cmdSansyou = New System.Windows.Forms.Button()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmdKensaku = New System.Windows.Forms.Button()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.txtUserID = New System.Windows.Forms.TextBox()
        Me.cnUserID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cnSimei = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnRyakumei = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnBiko = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnMukoFlg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cnMukoFlgName = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnUpdNm = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnUpdDt = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnModFlg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cnHideUserID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvIchiran, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdSyuturyoku
        '
        Me.cmdSyuturyoku.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdSyuturyoku.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdSyuturyoku.Location = New System.Drawing.Point(33, 14)
        Me.cmdSyuturyoku.Name = "cmdSyuturyoku"
        Me.cmdSyuturyoku.Size = New System.Drawing.Size(121, 40)
        Me.cmdSyuturyoku.TabIndex = 7
        Me.cmdSyuturyoku.Text = "CSV出力(&O)"
        Me.cmdSyuturyoku.UseVisualStyleBackColor = True
        '
        'cmdModoru
        '
        Me.cmdModoru.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdModoru.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdModoru.Location = New System.Drawing.Point(883, 14)
        Me.cmdModoru.Name = "cmdModoru"
        Me.cmdModoru.Size = New System.Drawing.Size(121, 40)
        Me.cmdModoru.TabIndex = 13
        Me.cmdModoru.Text = "戻　る(&B)"
        Me.cmdModoru.UseVisualStyleBackColor = True
        '
        'cmdSakujo
        '
        Me.cmdSakujo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdSakujo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdSakujo.Location = New System.Drawing.Point(756, 14)
        Me.cmdSakujo.Name = "cmdSakujo"
        Me.cmdSakujo.Size = New System.Drawing.Size(121, 40)
        Me.cmdSakujo.TabIndex = 12
        Me.cmdSakujo.Text = "削　除(&D)"
        Me.cmdSakujo.UseVisualStyleBackColor = True
        '
        'dgvIchiran
        '
        Me.dgvIchiran.AllowUserToAddRows = False
        Me.dgvIchiran.AllowUserToDeleteRows = False
        Me.dgvIchiran.AllowUserToResizeColumns = False
        Me.dgvIchiran.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvIchiran.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvIchiran.ColumnHeadersHeight = 22
        Me.dgvIchiran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvIchiran.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cnUserID, Me.cnSimei, Me.cnRyakumei, Me.cnBiko, Me.cnMukoFlg, Me.cnMukoFlgName, Me.cnUpdNm, Me.cnUpdDt, Me.cnModFlg, Me.cnHideUserID})
        Me.dgvIchiran.Location = New System.Drawing.Point(3, 145)
        Me.dgvIchiran.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgvIchiran.MultiSelect = False
        Me.dgvIchiran.Name = "dgvIchiran"
        Me.dgvIchiran.RowHeadersVisible = False
        Me.dgvIchiran.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvIchiran.ShowCellToolTips = False
        Me.dgvIchiran.Size = New System.Drawing.Size(1040, 339)
        Me.dgvIchiran.TabIndex = 5
        '
        'cmdHenkou
        '
        Me.cmdHenkou.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdHenkou.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdHenkou.Location = New System.Drawing.Point(629, 14)
        Me.cmdHenkou.Name = "cmdHenkou"
        Me.cmdHenkou.Size = New System.Drawing.Size(121, 40)
        Me.cmdHenkou.TabIndex = 11
        Me.cmdHenkou.Text = "変　更(&U)"
        Me.cmdHenkou.UseVisualStyleBackColor = True
        '
        'cmdTuika
        '
        Me.cmdTuika.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdTuika.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdTuika.Location = New System.Drawing.Point(375, 14)
        Me.cmdTuika.Name = "cmdTuika"
        Me.cmdTuika.Size = New System.Drawing.Size(121, 40)
        Me.cmdTuika.TabIndex = 9
        Me.cmdTuika.Text = "追　加(&I)"
        Me.cmdTuika.UseVisualStyleBackColor = True
        '
        'cmdFukusya
        '
        Me.cmdFukusya.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdFukusya.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdFukusya.Location = New System.Drawing.Point(502, 14)
        Me.cmdFukusya.Name = "cmdFukusya"
        Me.cmdFukusya.Size = New System.Drawing.Size(121, 40)
        Me.cmdFukusya.TabIndex = 10
        Me.cmdFukusya.Text = "複写新規(&C)"
        Me.cmdFukusya.UseVisualStyleBackColor = True
        '
        'cmdSansyou
        '
        Me.cmdSansyou.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdSansyou.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdSansyou.Location = New System.Drawing.Point(248, 14)
        Me.cmdSansyou.Name = "cmdSansyou"
        Me.cmdSansyou.Size = New System.Drawing.Size(121, 40)
        Me.cmdSansyou.TabIndex = 8
        Me.cmdSansyou.Text = "参　照(&R)"
        Me.cmdSansyou.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 10
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.cmdSyuturyoku, 1, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.cmdSansyou, 3, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.cmdTuika, 4, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.cmdModoru, 8, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.cmdHenkou, 6, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.cmdSakujo, 7, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.cmdFukusya, 5, 1)
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 501)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(1041, 58)
        Me.TableLayoutPanel3.TabIndex = 6
        Me.TableLayoutPanel3.TabStop = True
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 1
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.TableLayoutPanel3, 0, 2)
        Me.TableLayoutPanel4.Controls.Add(Me.TableLayoutPanel1, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.dgvIchiran, 0, 1)
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(6, 5)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 3
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.5079!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.18875!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.30335!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(1047, 562)
        Me.TableLayoutPanel4.TabIndex = 1
        Me.TableLayoutPanel4.TabStop = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.cmdKensaku, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1041, 85)
        Me.TableLayoutPanel1.TabIndex = 2
        Me.TableLayoutPanel1.TabStop = True
        '
        'cmdKensaku
        '
        Me.cmdKensaku.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdKensaku.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdKensaku.Location = New System.Drawing.Point(885, 42)
        Me.cmdKensaku.Name = "cmdKensaku"
        Me.cmdKensaku.Size = New System.Drawing.Size(102, 36)
        Me.cmdKensaku.TabIndex = 134
        Me.cmdKensaku.Text = "検索(&S)"
        Me.cmdKensaku.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel5, 0, 0)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 35)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(832, 50)
        Me.TableLayoutPanel2.TabIndex = 133
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 2
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.Label39, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.txtUserID, 1, 0)
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel5.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 2
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(489, 50)
        Me.TableLayoutPanel5.TabIndex = 0
        '
        'Label39
        '
        Me.Label39.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label39.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label39.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label39.Location = New System.Drawing.Point(0, 0)
        Me.Label39.Margin = New System.Windows.Forms.Padding(0)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(122, 22)
        Me.Label39.TabIndex = 5
        Me.Label39.Text = "ユーザーID"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtUserID
        '
        Me.txtUserID.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUserID.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtUserID.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtUserID.Location = New System.Drawing.Point(122, 0)
        Me.txtUserID.Margin = New System.Windows.Forms.Padding(0)
        Me.txtUserID.Name = "txtUserID"
        Me.txtUserID.Size = New System.Drawing.Size(367, 22)
        Me.txtUserID.TabIndex = 0
        '
        'cnUserID
        '
        Me.cnUserID.DataPropertyName = "dtUserID"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnUserID.DefaultCellStyle = DataGridViewCellStyle2
        Me.cnUserID.HeaderText = "ユーザーID"
        Me.cnUserID.MinimumWidth = 8
        Me.cnUserID.Name = "cnUserID"
        Me.cnUserID.ReadOnly = True
        Me.cnUserID.Width = 180
        '
        'cnSimei
        '
        Me.cnSimei.DataPropertyName = "dtSimei"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnSimei.DefaultCellStyle = DataGridViewCellStyle3
        Me.cnSimei.HeaderText = "氏名"
        Me.cnSimei.MinimumWidth = 100
        Me.cnSimei.Name = "cnSimei"
        Me.cnSimei.ReadOnly = True
        Me.cnSimei.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnSimei.TabStop = False
        Me.cnSimei.Width = 195
        '
        'cnRyakumei
        '
        Me.cnRyakumei.DataPropertyName = "dtRyakumei"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnRyakumei.DefaultCellStyle = DataGridViewCellStyle4
        Me.cnRyakumei.HeaderText = "略名"
        Me.cnRyakumei.Name = "cnRyakumei"
        Me.cnRyakumei.ReadOnly = True
        Me.cnRyakumei.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnRyakumei.TabStop = False
        Me.cnRyakumei.Width = 120
        '
        'cnBiko
        '
        Me.cnBiko.DataPropertyName = "dtBiko"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnBiko.DefaultCellStyle = DataGridViewCellStyle5
        Me.cnBiko.HeaderText = "備考"
        Me.cnBiko.Name = "cnBiko"
        Me.cnBiko.ReadOnly = True
        Me.cnBiko.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnBiko.TabStop = False
        Me.cnBiko.Width = 210
        '
        'cnMukoFlg
        '
        Me.cnMukoFlg.DataPropertyName = "dtMukoFlg"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnMukoFlg.DefaultCellStyle = DataGridViewCellStyle6
        Me.cnMukoFlg.HeaderText = "無効フラグ"
        Me.cnMukoFlg.Name = "cnMukoFlg"
        Me.cnMukoFlg.ReadOnly = True
        Me.cnMukoFlg.Visible = False
        Me.cnMukoFlg.Width = 80
        '
        'cnMukoFlgName
        '
        Me.cnMukoFlgName.DataPropertyName = "dtMukoFlgName"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle7.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnMukoFlgName.DefaultCellStyle = DataGridViewCellStyle7
        Me.cnMukoFlgName.HeaderText = "無効フラグ"
        Me.cnMukoFlgName.Name = "cnMukoFlgName"
        Me.cnMukoFlgName.ReadOnly = True
        Me.cnMukoFlgName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnMukoFlgName.TabStop = False
        Me.cnMukoFlgName.Width = 105
        '
        'cnUpdNm
        '
        Me.cnUpdNm.DataPropertyName = "dtUpdNm"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle8.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnUpdNm.DefaultCellStyle = DataGridViewCellStyle8
        Me.cnUpdNm.HeaderText = "更新者"
        Me.cnUpdNm.Name = "cnUpdNm"
        Me.cnUpdNm.ReadOnly = True
        Me.cnUpdNm.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnUpdNm.TabStop = False
        Me.cnUpdNm.Width = 80
        '
        'cnUpdDt
        '
        Me.cnUpdDt.DataPropertyName = "dtUpdDt"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle9.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnUpdDt.DefaultCellStyle = DataGridViewCellStyle9
        Me.cnUpdDt.HeaderText = "更新日"
        Me.cnUpdDt.Name = "cnUpdDt"
        Me.cnUpdDt.ReadOnly = True
        Me.cnUpdDt.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnUpdDt.TabStop = False
        Me.cnUpdDt.Width = 130
        '
        'cnModFlg
        '
        Me.cnModFlg.DataPropertyName = "dtModFlg"
        Me.cnModFlg.HeaderText = "更新フラグ"
        Me.cnModFlg.MaxInputLength = 1
        Me.cnModFlg.Name = "cnModFlg"
        Me.cnModFlg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnModFlg.Visible = False
        '
        'cnHideUserID
        '
        Me.cnHideUserID.DataPropertyName = "dtHideUserID"
        Me.cnHideUserID.HeaderText = "変更前ユーザーID"
        Me.cnHideUserID.Name = "cnHideUserID"
        Me.cnHideUserID.Visible = False
        '
        'frmM10F10_UserList
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1056, 577)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel4)
        Me.Name = "frmM10F10_UserList"
        Me.Text = "ユーザマスタ一覧（M10F10）"
        CType(Me.dgvIchiran, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdSyuturyoku As Button
    Friend WithEvents cmdModoru As Button
    Friend WithEvents cmdSakujo As Button
    Friend WithEvents dgvIchiran As CustomTabStopDataGridView.TabStop.UtilTabStopDataGridView
    Friend WithEvents cmdHenkou As Button
    Friend WithEvents cmdTuika As Button
    Friend WithEvents cmdFukusya As Button
    Friend WithEvents cmdSansyou As Button
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents txtUserID As TextBox
    Friend WithEvents Label39 As Label
    Friend WithEvents cmdKensaku As Button
    Friend WithEvents cnUserID As DataGridViewTextBoxColumn
    Friend WithEvents cnSimei As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnRyakumei As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnBiko As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnMukoFlg As DataGridViewTextBoxColumn
    Friend WithEvents cnMukoFlgName As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnUpdNm As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnUpdDt As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnModFlg As DataGridViewTextBoxColumn
    Friend WithEvents cnHideUserID As DataGridViewTextBoxColumn
End Class
