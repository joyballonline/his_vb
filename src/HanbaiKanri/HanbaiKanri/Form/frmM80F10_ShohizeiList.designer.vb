<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmM80F10_ShohizeiList
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
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.cmdSyuturyoku = New System.Windows.Forms.Button()
        Me.cmdModoru = New System.Windows.Forms.Button()
        Me.cmdSakujo = New System.Windows.Forms.Button()
        Me.dgvIchiran = New CustomTabStopDataGridView.TabStop.UtilTabStopDataGridView()
        Me.cnShohizeiKbn = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnTekiyoKaisiDate = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnTekiyoShuryoDate = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnShohizeiRitsu = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnUpdNm = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnUpdDt = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnModFlg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cnHideTekiyoKaisiDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdHenkou = New System.Windows.Forms.Button()
        Me.cmdTuika = New System.Windows.Forms.Button()
        Me.cmdSansyou = New System.Windows.Forms.Button()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmdKensaku = New System.Windows.Forms.Button()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtpKijunDt = New CustomControl.NullableDateTimePicker()
        CType(Me.dgvIchiran, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdSyuturyoku
        '
        Me.cmdSyuturyoku.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdSyuturyoku.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdSyuturyoku.Location = New System.Drawing.Point(44, 6)
        Me.cmdSyuturyoku.Name = "cmdSyuturyoku"
        Me.cmdSyuturyoku.Size = New System.Drawing.Size(121, 31)
        Me.cmdSyuturyoku.TabIndex = 7
        Me.cmdSyuturyoku.Text = "CSV出力(&O)"
        Me.cmdSyuturyoku.UseVisualStyleBackColor = True
        '
        'cmdModoru
        '
        Me.cmdModoru.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdModoru.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdModoru.Location = New System.Drawing.Point(344, 69)
        Me.cmdModoru.Name = "cmdModoru"
        Me.cmdModoru.Size = New System.Drawing.Size(121, 31)
        Me.cmdModoru.TabIndex = 13
        Me.cmdModoru.Text = "戻　る(&B)"
        Me.cmdModoru.UseVisualStyleBackColor = True
        '
        'cmdSakujo
        '
        Me.cmdSakujo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdSakujo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdSakujo.Location = New System.Drawing.Point(194, 69)
        Me.cmdSakujo.Name = "cmdSakujo"
        Me.cmdSakujo.Size = New System.Drawing.Size(121, 31)
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
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle8.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvIchiran.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.dgvIchiran.ColumnHeadersHeight = 22
        Me.dgvIchiran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvIchiran.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cnShohizeiKbn, Me.cnTekiyoKaisiDate, Me.cnTekiyoShuryoDate, Me.cnShohizeiRitsu, Me.cnUpdNm, Me.cnUpdDt, Me.cnModFlg, Me.cnHideTekiyoKaisiDate})
        Me.dgvIchiran.Location = New System.Drawing.Point(3, 68)
        Me.dgvIchiran.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgvIchiran.MultiSelect = False
        Me.dgvIchiran.Name = "dgvIchiran"
        Me.dgvIchiran.RowHeadersVisible = False
        Me.dgvIchiran.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvIchiran.ShowCellToolTips = False
        Me.dgvIchiran.Size = New System.Drawing.Size(512, 234)
        Me.dgvIchiran.TabIndex = 5
        '
        'cnShohizeiKbn
        '
        Me.cnShohizeiKbn.DataPropertyName = "dtShohizeiKbn"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnShohizeiKbn.DefaultCellStyle = DataGridViewCellStyle9
        Me.cnShohizeiKbn.HeaderText = "消費税区分"
        Me.cnShohizeiKbn.Name = "cnShohizeiKbn"
        Me.cnShohizeiKbn.ReadOnly = True
        Me.cnShohizeiKbn.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnShohizeiKbn.TabStop = False
        Me.cnShohizeiKbn.Visible = False
        Me.cnShohizeiKbn.Width = 81
        '
        'cnTekiyoKaisiDate
        '
        Me.cnTekiyoKaisiDate.DataPropertyName = "dtTekiyoKaisiDate"
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle10.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnTekiyoKaisiDate.DefaultCellStyle = DataGridViewCellStyle10
        Me.cnTekiyoKaisiDate.HeaderText = "適用開始日"
        Me.cnTekiyoKaisiDate.Name = "cnTekiyoKaisiDate"
        Me.cnTekiyoKaisiDate.ReadOnly = True
        Me.cnTekiyoKaisiDate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnTekiyoKaisiDate.TabStop = False
        Me.cnTekiyoKaisiDate.Width = 90
        '
        'cnTekiyoShuryoDate
        '
        Me.cnTekiyoShuryoDate.DataPropertyName = "dtTekiyoShuryoDate"
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle11.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnTekiyoShuryoDate.DefaultCellStyle = DataGridViewCellStyle11
        Me.cnTekiyoShuryoDate.HeaderText = "適用終了日"
        Me.cnTekiyoShuryoDate.Name = "cnTekiyoShuryoDate"
        Me.cnTekiyoShuryoDate.ReadOnly = True
        Me.cnTekiyoShuryoDate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnTekiyoShuryoDate.TabStop = False
        Me.cnTekiyoShuryoDate.Width = 90
        '
        'cnShohizeiRitsu
        '
        Me.cnShohizeiRitsu.DataPropertyName = "dtShohizeiRitsu"
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle12.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnShohizeiRitsu.DefaultCellStyle = DataGridViewCellStyle12
        Me.cnShohizeiRitsu.HeaderText = "消費税率"
        Me.cnShohizeiRitsu.Name = "cnShohizeiRitsu"
        Me.cnShohizeiRitsu.ReadOnly = True
        Me.cnShohizeiRitsu.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnShohizeiRitsu.TabStop = False
        Me.cnShohizeiRitsu.Width = 102
        '
        'cnUpdNm
        '
        Me.cnUpdNm.DataPropertyName = "dtUpdNm"
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle13.BackColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle13.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnUpdNm.DefaultCellStyle = DataGridViewCellStyle13
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
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle14.BackColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle14.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnUpdDt.DefaultCellStyle = DataGridViewCellStyle14
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
        'cnHideTekiyoKaisiDate
        '
        Me.cnHideTekiyoKaisiDate.DataPropertyName = "dtHideTekiyoKaisiDate"
        Me.cnHideTekiyoKaisiDate.HeaderText = "変更前適用開始日"
        Me.cnHideTekiyoKaisiDate.Name = "cnHideTekiyoKaisiDate"
        Me.cnHideTekiyoKaisiDate.Visible = False
        '
        'cmdHenkou
        '
        Me.cmdHenkou.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdHenkou.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdHenkou.Location = New System.Drawing.Point(44, 69)
        Me.cmdHenkou.Name = "cmdHenkou"
        Me.cmdHenkou.Size = New System.Drawing.Size(121, 31)
        Me.cmdHenkou.TabIndex = 11
        Me.cmdHenkou.Text = "変　更(&U)"
        Me.cmdHenkou.UseVisualStyleBackColor = True
        '
        'cmdTuika
        '
        Me.cmdTuika.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdTuika.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdTuika.Location = New System.Drawing.Point(344, 6)
        Me.cmdTuika.Name = "cmdTuika"
        Me.cmdTuika.Size = New System.Drawing.Size(121, 31)
        Me.cmdTuika.TabIndex = 9
        Me.cmdTuika.Text = "追　加(&I)"
        Me.cmdTuika.UseVisualStyleBackColor = True
        '
        'cmdSansyou
        '
        Me.cmdSansyou.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdSansyou.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdSansyou.Location = New System.Drawing.Point(194, 6)
        Me.cmdSansyou.Name = "cmdSansyou"
        Me.cmdSansyou.Size = New System.Drawing.Size(121, 31)
        Me.cmdSansyou.TabIndex = 8
        Me.cmdSansyou.Text = "参　照(&R)"
        Me.cmdSansyou.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 5
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.cmdSyuturyoku, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.cmdHenkou, 1, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.cmdSansyou, 2, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.cmdTuika, 3, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.cmdSakujo, 2, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.cmdModoru, 3, 2)
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 311)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 4
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(512, 127)
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
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(523, 441)
        Me.TableLayoutPanel4.TabIndex = 1
        Me.TableLayoutPanel4.TabStop = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.cmdKensaku, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(512, 60)
        Me.TableLayoutPanel1.TabIndex = 2
        Me.TableLayoutPanel1.TabStop = True
        '
        'cmdKensaku
        '
        Me.cmdKensaku.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdKensaku.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdKensaku.Location = New System.Drawing.Point(397, 18)
        Me.cmdKensaku.Name = "cmdKensaku"
        Me.cmdKensaku.Size = New System.Drawing.Size(102, 36)
        Me.cmdKensaku.TabIndex = 134
        Me.cmdKensaku.Text = "検索(&S)"
        Me.cmdKensaku.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 4
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel7, 0, 0)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 12)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(327, 48)
        Me.TableLayoutPanel2.TabIndex = 133
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.ColumnCount = 2
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.Controls.Add(Me.Label5, 0, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.dtpKijunDt, 1, 0)
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel7.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 3
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(285, 48)
        Me.TableLayoutPanel7.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Margin = New System.Windows.Forms.Padding(0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(112, 22)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "基準日"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpKijunDt
        '
        Me.dtpKijunDt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dtpKijunDt.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtpKijunDt.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpKijunDt.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.dtpKijunDt.Location = New System.Drawing.Point(112, 0)
        Me.dtpKijunDt.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpKijunDt.Name = "dtpKijunDt"
        Me.dtpKijunDt.NullValue = ""
        Me.dtpKijunDt.Size = New System.Drawing.Size(106, 22)
        Me.dtpKijunDt.TabIndex = 9
        Me.dtpKijunDt.Value = New Date(2018, 3, 22, 14, 31, 6, 795)
        '
        'frmM80F10_ShohizeiList
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(533, 450)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel4)
        Me.Name = "frmM80F10_ShohizeiList"
        Me.Text = "消費税マスタ一覧（M80F10）"
        CType(Me.dgvIchiran, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdSyuturyoku As Button
    Friend WithEvents cmdModoru As Button
    Friend WithEvents cmdSakujo As Button
    Friend WithEvents dgvIchiran As CustomTabStopDataGridView.TabStop.UtilTabStopDataGridView
    Friend WithEvents cmdHenkou As Button
    Friend WithEvents cmdTuika As Button
    Friend WithEvents cmdSansyou As Button
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents cmdKensaku As Button
    Friend WithEvents TableLayoutPanel7 As TableLayoutPanel
    Friend WithEvents Label5 As Label
    Friend WithEvents dtpKijunDt As CustomControl.NullableDateTimePicker
    Friend WithEvents cnShohizeiKbn As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnTekiyoKaisiDate As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnTekiyoShuryoDate As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnShohizeiRitsu As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnUpdNm As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnUpdDt As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnModFlg As DataGridViewTextBoxColumn
    Friend WithEvents cnHideTekiyoKaisiDate As DataGridViewTextBoxColumn
End Class
