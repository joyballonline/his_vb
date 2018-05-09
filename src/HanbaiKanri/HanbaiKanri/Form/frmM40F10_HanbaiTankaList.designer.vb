<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmM40F10_HanbaiTankaList
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
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
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
        Me.txtToriName = New System.Windows.Forms.TextBox()
        Me.txtToriCode = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtShohinName = New System.Windows.Forms.TextBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.txtShohinCode = New System.Windows.Forms.TextBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtpKijunDt = New CustomControl.NullableDateTimePicker()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rboTokubaiIgai = New System.Windows.Forms.RadioButton()
        Me.rboTokubai = New System.Windows.Forms.RadioButton()
        Me.cnShohinCode = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnShohinName = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnToriCode = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnToriName = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnTekiyoKaisiDate = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnTekiyoShuryoDate = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnHanbaiTanka = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnTokubaiKbn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cnTokubaiKbnName = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.cnMemo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cnUpdNm = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnUpdDt = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnModFlg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cnHideShohinCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cnHideTokubaiKbn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cnHideToriCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cnHideTekiyoKaisiDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvIchiran, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        Me.Panel1.SuspendLayout()
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
        Me.dgvIchiran.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cnShohinCode, Me.cnShohinName, Me.cnToriCode, Me.cnToriName, Me.cnTekiyoKaisiDate, Me.cnTekiyoShuryoDate, Me.cnHanbaiTanka, Me.cnTokubaiKbn, Me.cnTokubaiKbnName, Me.cnMemo, Me.cnUpdNm, Me.cnUpdDt, Me.cnModFlg, Me.cnHideShohinCode, Me.cnHideTokubaiKbn, Me.cnHideToriCode, Me.cnHideTekiyoKaisiDate})
        Me.dgvIchiran.Location = New System.Drawing.Point(3, 145)
        Me.dgvIchiran.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgvIchiran.MultiSelect = False
        Me.dgvIchiran.Name = "dgvIchiran"
        Me.dgvIchiran.RowHeadersVisible = False
        Me.dgvIchiran.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvIchiran.ShowCellToolTips = False
        Me.dgvIchiran.Size = New System.Drawing.Size(1041, 338)
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
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 88.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.cmdKensaku, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1041, 137)
        Me.TableLayoutPanel1.TabIndex = 2
        Me.TableLayoutPanel1.TabStop = True
        '
        'cmdKensaku
        '
        Me.cmdKensaku.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdKensaku.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdKensaku.Location = New System.Drawing.Point(927, 64)
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
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel5, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel6, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel7, 3, 0)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 27)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(916, 91)
        Me.TableLayoutPanel2.TabIndex = 133
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 2
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.txtToriName, 1, 3)
        Me.TableLayoutPanel5.Controls.Add(Me.txtToriCode, 1, 2)
        Me.TableLayoutPanel5.Controls.Add(Me.Label4, 0, 3)
        Me.TableLayoutPanel5.Controls.Add(Me.txtShohinName, 1, 1)
        Me.TableLayoutPanel5.Controls.Add(Me.Label39, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.txtShohinCode, 1, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.Label41, 0, 2)
        Me.TableLayoutPanel5.Controls.Add(Me.Label40, 0, 1)
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel5.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 4
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(477, 88)
        Me.TableLayoutPanel5.TabIndex = 0
        '
        'txtToriName
        '
        Me.txtToriName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtToriName.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtToriName.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtToriName.Location = New System.Drawing.Point(119, 66)
        Me.txtToriName.Margin = New System.Windows.Forms.Padding(0)
        Me.txtToriName.MaxLength = 100
        Me.txtToriName.Name = "txtToriName"
        Me.txtToriName.Size = New System.Drawing.Size(358, 22)
        Me.txtToriName.TabIndex = 11
        '
        'txtToriCode
        '
        Me.txtToriCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtToriCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtToriCode.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtToriCode.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtToriCode.Location = New System.Drawing.Point(119, 44)
        Me.txtToriCode.Margin = New System.Windows.Forms.Padding(0)
        Me.txtToriCode.MaxLength = 8
        Me.txtToriCode.Name = "txtToriCode"
        Me.txtToriCode.Size = New System.Drawing.Size(75, 22)
        Me.txtToriCode.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(0, 66)
        Me.Label4.Margin = New System.Windows.Forms.Padding(0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(119, 22)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "取引先名"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtShohinName
        '
        Me.txtShohinName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtShohinName.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShohinName.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtShohinName.Location = New System.Drawing.Point(119, 22)
        Me.txtShohinName.Margin = New System.Windows.Forms.Padding(0)
        Me.txtShohinName.MaxLength = 100
        Me.txtShohinName.Name = "txtShohinName"
        Me.txtShohinName.Size = New System.Drawing.Size(358, 22)
        Me.txtShohinName.TabIndex = 1
        '
        'Label39
        '
        Me.Label39.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label39.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label39.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label39.Location = New System.Drawing.Point(0, 0)
        Me.Label39.Margin = New System.Windows.Forms.Padding(0)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(119, 22)
        Me.Label39.TabIndex = 5
        Me.Label39.Text = "商品コード"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtShohinCode
        '
        Me.txtShohinCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtShohinCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtShohinCode.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShohinCode.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtShohinCode.Location = New System.Drawing.Point(119, 0)
        Me.txtShohinCode.Margin = New System.Windows.Forms.Padding(0)
        Me.txtShohinCode.MaxLength = 5
        Me.txtShohinCode.Name = "txtShohinCode"
        Me.txtShohinCode.Size = New System.Drawing.Size(55, 22)
        Me.txtShohinCode.TabIndex = 0
        '
        'Label41
        '
        Me.Label41.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label41.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label41.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label41.Location = New System.Drawing.Point(0, 44)
        Me.Label41.Margin = New System.Windows.Forms.Padding(0)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(119, 22)
        Me.Label41.TabIndex = 10
        Me.Label41.Text = "取引先コード"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label40
        '
        Me.Label40.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label40.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label40.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label40.Location = New System.Drawing.Point(0, 22)
        Me.Label40.Margin = New System.Windows.Forms.Padding(0)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(119, 22)
        Me.Label40.TabIndex = 8
        Me.Label40.Text = "商品名"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 1
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.Label6, 0, 3)
        Me.TableLayoutPanel6.Controls.Add(Me.Label47, 0, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.Label1, 0, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.Label2, 0, 2)
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(477, 0)
        Me.TableLayoutPanel6.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 4
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(120, 88)
        Me.TableLayoutPanel6.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 69)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(113, 15)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "（一部一致検索）"
        '
        'Label47
        '
        Me.Label47.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label47.AutoSize = True
        Me.Label47.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label47.Location = New System.Drawing.Point(3, 3)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(113, 15)
        Me.Label47.TabIndex = 5
        Me.Label47.Text = "（前方一致検索）"
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(113, 15)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "（一部一致検索）"
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(113, 15)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "（前方一致検索）"
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.ColumnCount = 2
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.Controls.Add(Me.Label3, 0, 2)
        Me.TableLayoutPanel7.Controls.Add(Me.Label5, 0, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.dtpKijunDt, 1, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.Panel1, 1, 2)
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(631, 0)
        Me.TableLayoutPanel7.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 3
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(285, 88)
        Me.TableLayoutPanel7.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 66)
        Me.Label3.Margin = New System.Windows.Forms.Padding(0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(112, 22)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "特売区分"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        'Panel1
        '
        Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.rboTokubaiIgai)
        Me.Panel1.Controls.Add(Me.rboTokubai)
        Me.Panel1.Location = New System.Drawing.Point(112, 66)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(173, 22)
        Me.Panel1.TabIndex = 11
        '
        'rboTokubaiIgai
        '
        Me.rboTokubaiIgai.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rboTokubaiIgai.Location = New System.Drawing.Point(74, 2)
        Me.rboTokubaiIgai.Margin = New System.Windows.Forms.Padding(0)
        Me.rboTokubaiIgai.Name = "rboTokubaiIgai"
        Me.rboTokubaiIgai.Size = New System.Drawing.Size(89, 19)
        Me.rboTokubaiIgai.TabIndex = 17
        Me.rboTokubaiIgai.TabStop = True
        Me.rboTokubaiIgai.Text = "特売以外"
        Me.rboTokubaiIgai.UseVisualStyleBackColor = True
        '
        'rboTokubai
        '
        Me.rboTokubai.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rboTokubai.Location = New System.Drawing.Point(8, 2)
        Me.rboTokubai.Margin = New System.Windows.Forms.Padding(0)
        Me.rboTokubai.Name = "rboTokubai"
        Me.rboTokubai.Size = New System.Drawing.Size(62, 19)
        Me.rboTokubai.TabIndex = 16
        Me.rboTokubai.TabStop = True
        Me.rboTokubai.Text = "特売"
        Me.rboTokubai.UseVisualStyleBackColor = True
        '
        'cnShohinCode
        '
        Me.cnShohinCode.DataPropertyName = "dtShohinCode"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnShohinCode.DefaultCellStyle = DataGridViewCellStyle2
        Me.cnShohinCode.HeaderText = "商品コード"
        Me.cnShohinCode.Name = "cnShohinCode"
        Me.cnShohinCode.ReadOnly = True
        Me.cnShohinCode.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnShohinCode.TabStop = False
        Me.cnShohinCode.Width = 81
        '
        'cnShohinName
        '
        Me.cnShohinName.DataPropertyName = "dtShohinName"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnShohinName.DefaultCellStyle = DataGridViewCellStyle3
        Me.cnShohinName.HeaderText = "商品名"
        Me.cnShohinName.Name = "cnShohinName"
        Me.cnShohinName.ReadOnly = True
        Me.cnShohinName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnShohinName.TabStop = False
        Me.cnShohinName.Width = 200
        '
        'cnToriCode
        '
        Me.cnToriCode.DataPropertyName = "dtToriCode"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnToriCode.DefaultCellStyle = DataGridViewCellStyle4
        Me.cnToriCode.HeaderText = "取引先コード"
        Me.cnToriCode.MinimumWidth = 8
        Me.cnToriCode.Name = "cnToriCode"
        Me.cnToriCode.ReadOnly = True
        Me.cnToriCode.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnToriCode.TabStop = False
        Me.cnToriCode.Width = 95
        '
        'cnToriName
        '
        Me.cnToriName.DataPropertyName = "dtToriName"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnToriName.DefaultCellStyle = DataGridViewCellStyle5
        Me.cnToriName.HeaderText = "取引先名"
        Me.cnToriName.MinimumWidth = 100
        Me.cnToriName.Name = "cnToriName"
        Me.cnToriName.ReadOnly = True
        Me.cnToriName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnToriName.TabStop = False
        Me.cnToriName.Width = 110
        '
        'cnTekiyoKaisiDate
        '
        Me.cnTekiyoKaisiDate.DataPropertyName = "dtTekiyoKaisiDate"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnTekiyoKaisiDate.DefaultCellStyle = DataGridViewCellStyle6
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
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle7.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnTekiyoShuryoDate.DefaultCellStyle = DataGridViewCellStyle7
        Me.cnTekiyoShuryoDate.HeaderText = "適用終了日"
        Me.cnTekiyoShuryoDate.Name = "cnTekiyoShuryoDate"
        Me.cnTekiyoShuryoDate.ReadOnly = True
        Me.cnTekiyoShuryoDate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnTekiyoShuryoDate.TabStop = False
        Me.cnTekiyoShuryoDate.Width = 90
        '
        'cnHanbaiTanka
        '
        Me.cnHanbaiTanka.DataPropertyName = "dtHanbaiTanka"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnHanbaiTanka.DefaultCellStyle = DataGridViewCellStyle8
        Me.cnHanbaiTanka.HeaderText = "販売単価"
        Me.cnHanbaiTanka.Name = "cnHanbaiTanka"
        Me.cnHanbaiTanka.ReadOnly = True
        Me.cnHanbaiTanka.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnHanbaiTanka.TabStop = False
        Me.cnHanbaiTanka.Width = 102
        '
        'cnTokubaiKbn
        '
        Me.cnTokubaiKbn.DataPropertyName = "dtTokubaiKbn"
        Me.cnTokubaiKbn.HeaderText = "特売区分_区分"
        Me.cnTokubaiKbn.Name = "cnTokubaiKbn"
        Me.cnTokubaiKbn.Visible = False
        '
        'cnTokubaiKbnName
        '
        Me.cnTokubaiKbnName.DataPropertyName = "dtTokubaiKbnName"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle9.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle9.NullValue = False
        Me.cnTokubaiKbnName.DefaultCellStyle = DataGridViewCellStyle9
        Me.cnTokubaiKbnName.HeaderText = "特売"
        Me.cnTokubaiKbnName.Name = "cnTokubaiKbnName"
        Me.cnTokubaiKbnName.ReadOnly = True
        Me.cnTokubaiKbnName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnTokubaiKbnName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.cnTokubaiKbnName.Width = 43
        '
        'cnMemo
        '
        Me.cnMemo.DataPropertyName = "dtMemo"
        Me.cnMemo.HeaderText = "メモ"
        Me.cnMemo.Name = "cnMemo"
        Me.cnMemo.ReadOnly = True
        Me.cnMemo.Visible = False
        '
        'cnUpdNm
        '
        Me.cnUpdNm.DataPropertyName = "dtUpdNm"
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle10.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnUpdNm.DefaultCellStyle = DataGridViewCellStyle10
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
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle11.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnUpdDt.DefaultCellStyle = DataGridViewCellStyle11
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
        'cnHideShohinCode
        '
        Me.cnHideShohinCode.DataPropertyName = "dtHideShohinCode"
        Me.cnHideShohinCode.HeaderText = "変更前商品コード"
        Me.cnHideShohinCode.Name = "cnHideShohinCode"
        Me.cnHideShohinCode.ReadOnly = True
        Me.cnHideShohinCode.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnHideShohinCode.Visible = False
        '
        'cnHideTokubaiKbn
        '
        Me.cnHideTokubaiKbn.DataPropertyName = "dtHideTokubaiKbn"
        Me.cnHideTokubaiKbn.HeaderText = "変更前特売区分"
        Me.cnHideTokubaiKbn.Name = "cnHideTokubaiKbn"
        Me.cnHideTokubaiKbn.Visible = False
        '
        'cnHideToriCode
        '
        Me.cnHideToriCode.DataPropertyName = "dtHideToriCode"
        Me.cnHideToriCode.HeaderText = "変更前取引先コード"
        Me.cnHideToriCode.Name = "cnHideToriCode"
        Me.cnHideToriCode.Visible = False
        '
        'cnHideTekiyoKaisiDate
        '
        Me.cnHideTekiyoKaisiDate.DataPropertyName = "dtHideTekiyoKaisiDate"
        Me.cnHideTekiyoKaisiDate.HeaderText = "変更前適用開始日"
        Me.cnHideTekiyoKaisiDate.Name = "cnHideTekiyoKaisiDate"
        Me.cnHideTekiyoKaisiDate.Visible = False
        '
        'frmM40F10_HanbaiTankaList
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1060, 577)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel4)
        Me.Name = "frmM40F10_HanbaiTankaList"
        Me.Text = "販売単価マスタ一覧（M40F10）"
        CType(Me.dgvIchiran, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel6.PerformLayout()
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
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
    Friend WithEvents TableLayoutPanel6 As TableLayoutPanel
    Friend WithEvents Label40 As Label
    Friend WithEvents txtShohinCode As TextBox
    Friend WithEvents Label39 As Label
    Friend WithEvents txtToriCode As TextBox
    Friend WithEvents txtShohinName As TextBox
    Friend WithEvents Label41 As Label
    Friend WithEvents Label47 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents cmdKensaku As Button
    Friend WithEvents TableLayoutPanel7 As TableLayoutPanel
    Friend WithEvents Label5 As Label
    Friend WithEvents dtpKijunDt As CustomControl.NullableDateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents txtToriName As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents rboTokubaiIgai As RadioButton
    Friend WithEvents rboTokubai As RadioButton
    Friend WithEvents cnShohinCode As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnShohinName As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnToriCode As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnToriName As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnTekiyoKaisiDate As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnTekiyoShuryoDate As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnHanbaiTanka As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnTokubaiKbn As DataGridViewTextBoxColumn
    Friend WithEvents cnTokubaiKbnName As DataGridViewCheckBoxColumn
    Friend WithEvents cnMemo As DataGridViewTextBoxColumn
    Friend WithEvents cnUpdNm As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnUpdDt As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnModFlg As DataGridViewTextBoxColumn
    Friend WithEvents cnHideShohinCode As DataGridViewTextBoxColumn
    Friend WithEvents cnHideTokubaiKbn As DataGridViewTextBoxColumn
    Friend WithEvents cnHideToriCode As DataGridViewTextBoxColumn
    Friend WithEvents cnHideTekiyoKaisiDate As DataGridViewTextBoxColumn
End Class
