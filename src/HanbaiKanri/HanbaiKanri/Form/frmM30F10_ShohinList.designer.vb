<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmM30F10_ShohinList
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
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle21 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle22 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle23 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle24 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle25 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle26 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle27 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle28 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.cmdSyuturyoku = New System.Windows.Forms.Button()
        Me.cmdModoru = New System.Windows.Forms.Button()
        Me.cmdSakujo = New System.Windows.Forms.Button()
        Me.dgvIchiran = New CustomTabStopDataGridView.TabStop.UtilTabStopDataGridView()
        Me.cnShohinCode = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnShohinName = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnShohinRyakuName = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnShohinNameKana = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnHanbaiSiireKbn = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnHanbaiSiireKbnName = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnDaibunrui = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnKazeiKbn = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnKazeiKbnName = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnReitoKbn = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnReitoKbnName = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnIrisu = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnTani = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnSanti = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnDispNo = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnMemo = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnUpdNm = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnUpdDt = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnModFlg = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
        Me.cnHideShohinCode = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn()
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
        Me.txtShohinName = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboHanbaiSiireKbn = New System.Windows.Forms.ComboBox()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        CType(Me.dgvIchiran, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdSyuturyoku
        '
        Me.cmdSyuturyoku.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdSyuturyoku.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdSyuturyoku.Location = New System.Drawing.Point(33, 14)
        Me.cmdSyuturyoku.Name = "cmdSyuturyoku"
        Me.cmdSyuturyoku.Size = New System.Drawing.Size(121, 39)
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
        Me.cmdModoru.Size = New System.Drawing.Size(121, 39)
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
        Me.cmdSakujo.Size = New System.Drawing.Size(121, 39)
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
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle15.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvIchiran.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle15
        Me.dgvIchiran.ColumnHeadersHeight = 22
        Me.dgvIchiran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvIchiran.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cnShohinCode, Me.cnShohinName, Me.cnShohinRyakuName, Me.cnShohinNameKana, Me.cnHanbaiSiireKbn, Me.cnHanbaiSiireKbnName, Me.cnDaibunrui, Me.cnKazeiKbn, Me.cnKazeiKbnName, Me.cnReitoKbn, Me.cnReitoKbnName, Me.cnIrisu, Me.cnTani, Me.cnSanti, Me.cnDispNo, Me.cnMemo, Me.cnUpdNm, Me.cnUpdDt, Me.cnModFlg, Me.cnHideShohinCode})
        Me.dgvIchiran.Location = New System.Drawing.Point(3, 140)
        Me.dgvIchiran.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.dgvIchiran.MultiSelect = False
        Me.dgvIchiran.Name = "dgvIchiran"
        Me.dgvIchiran.RowHeadersVisible = False
        Me.dgvIchiran.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvIchiran.ShowCellToolTips = False
        Me.dgvIchiran.Size = New System.Drawing.Size(1041, 338)
        Me.dgvIchiran.TabIndex = 5
        '
        'cnShohinCode
        '
        Me.cnShohinCode.DataPropertyName = "dtShohinCode"
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle16.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnShohinCode.DefaultCellStyle = DataGridViewCellStyle16
        Me.cnShohinCode.HeaderText = "商品コード"
        Me.cnShohinCode.MinimumWidth = 8
        Me.cnShohinCode.Name = "cnShohinCode"
        Me.cnShohinCode.ReadOnly = True
        Me.cnShohinCode.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnShohinCode.TabStop = False
        Me.cnShohinCode.Width = 85
        '
        'cnShohinName
        '
        Me.cnShohinName.DataPropertyName = "dtShohinName"
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle17.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnShohinName.DefaultCellStyle = DataGridViewCellStyle17
        Me.cnShohinName.HeaderText = "商品名"
        Me.cnShohinName.MinimumWidth = 100
        Me.cnShohinName.Name = "cnShohinName"
        Me.cnShohinName.ReadOnly = True
        Me.cnShohinName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnShohinName.TabStop = False
        Me.cnShohinName.Width = 195
        '
        'cnShohinRyakuName
        '
        Me.cnShohinRyakuName.DataPropertyName = "dtShohinRyakuName"
        Me.cnShohinRyakuName.HeaderText = "商品名略称"
        Me.cnShohinRyakuName.Name = "cnShohinRyakuName"
        Me.cnShohinRyakuName.ReadOnly = True
        Me.cnShohinRyakuName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnShohinRyakuName.TabStop = False
        Me.cnShohinRyakuName.Width = 105
        '
        'cnShohinNameKana
        '
        Me.cnShohinNameKana.DataPropertyName = "dtShohinNameKana"
        Me.cnShohinNameKana.HeaderText = "商品名カナ"
        Me.cnShohinNameKana.Name = "cnShohinNameKana"
        Me.cnShohinNameKana.ReadOnly = True
        Me.cnShohinNameKana.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnShohinNameKana.TabStop = False
        Me.cnShohinNameKana.Visible = False
        '
        'cnHanbaiSiireKbn
        '
        Me.cnHanbaiSiireKbn.DataPropertyName = "dtHanbaiSiireKbn"
        DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle18.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnHanbaiSiireKbn.DefaultCellStyle = DataGridViewCellStyle18
        Me.cnHanbaiSiireKbn.HeaderText = "販売仕入区分"
        Me.cnHanbaiSiireKbn.Name = "cnHanbaiSiireKbn"
        Me.cnHanbaiSiireKbn.ReadOnly = True
        Me.cnHanbaiSiireKbn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnHanbaiSiireKbn.TabStop = False
        Me.cnHanbaiSiireKbn.Visible = False
        Me.cnHanbaiSiireKbn.Width = 80
        '
        'cnHanbaiSiireKbnName
        '
        Me.cnHanbaiSiireKbnName.DataPropertyName = "dtHanbaiSiireKbnName"
        DataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle19.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnHanbaiSiireKbnName.DefaultCellStyle = DataGridViewCellStyle19
        Me.cnHanbaiSiireKbnName.HeaderText = "販売仕入区分"
        Me.cnHanbaiSiireKbnName.Name = "cnHanbaiSiireKbnName"
        Me.cnHanbaiSiireKbnName.ReadOnly = True
        Me.cnHanbaiSiireKbnName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnHanbaiSiireKbnName.TabStop = False
        Me.cnHanbaiSiireKbnName.Width = 105
        '
        'cnDaibunrui
        '
        Me.cnDaibunrui.DataPropertyName = "dtDaibunrui"
        Me.cnDaibunrui.HeaderText = "大分類"
        Me.cnDaibunrui.Name = "cnDaibunrui"
        Me.cnDaibunrui.ReadOnly = True
        Me.cnDaibunrui.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnDaibunrui.TabStop = False
        Me.cnDaibunrui.Visible = False
        '
        'cnKazeiKbn
        '
        Me.cnKazeiKbn.DataPropertyName = "dtKazeiKbn"
        Me.cnKazeiKbn.HeaderText = "課税区分"
        Me.cnKazeiKbn.Name = "cnKazeiKbn"
        Me.cnKazeiKbn.ReadOnly = True
        Me.cnKazeiKbn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnKazeiKbn.TabStop = False
        Me.cnKazeiKbn.Visible = False
        '
        'cnKazeiKbnName
        '
        Me.cnKazeiKbnName.DataPropertyName = "dtKazeiKbnName"
        DataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle20.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnKazeiKbnName.DefaultCellStyle = DataGridViewCellStyle20
        Me.cnKazeiKbnName.HeaderText = "課税区分"
        Me.cnKazeiKbnName.Name = "cnKazeiKbnName"
        Me.cnKazeiKbnName.ReadOnly = True
        Me.cnKazeiKbnName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnKazeiKbnName.TabStop = False
        Me.cnKazeiKbnName.Width = 75
        '
        'cnReitoKbn
        '
        Me.cnReitoKbn.DataPropertyName = "dtReitoKbn"
        DataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle21.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnReitoKbn.DefaultCellStyle = DataGridViewCellStyle21
        Me.cnReitoKbn.HeaderText = "冷凍区分"
        Me.cnReitoKbn.Name = "cnReitoKbn"
        Me.cnReitoKbn.ReadOnly = True
        Me.cnReitoKbn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnReitoKbn.TabStop = False
        Me.cnReitoKbn.Visible = False
        Me.cnReitoKbn.Width = 120
        '
        'cnReitoKbnName
        '
        Me.cnReitoKbnName.DataPropertyName = "dtReitoKbnName"
        DataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle22.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnReitoKbnName.DefaultCellStyle = DataGridViewCellStyle22
        Me.cnReitoKbnName.HeaderText = "冷凍区分"
        Me.cnReitoKbnName.Name = "cnReitoKbnName"
        Me.cnReitoKbnName.ReadOnly = True
        Me.cnReitoKbnName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnReitoKbnName.TabStop = False
        Me.cnReitoKbnName.Width = 75
        '
        'cnIrisu
        '
        Me.cnIrisu.DataPropertyName = "dtIrisu"
        DataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle23.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnIrisu.DefaultCellStyle = DataGridViewCellStyle23
        Me.cnIrisu.HeaderText = "入数"
        Me.cnIrisu.Name = "cnIrisu"
        Me.cnIrisu.ReadOnly = True
        Me.cnIrisu.TabStop = False
        Me.cnIrisu.Width = 60
        '
        'cnTani
        '
        Me.cnTani.DataPropertyName = "dtTani"
        DataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle24.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnTani.DefaultCellStyle = DataGridViewCellStyle24
        Me.cnTani.HeaderText = "単位"
        Me.cnTani.Name = "cnTani"
        Me.cnTani.ReadOnly = True
        Me.cnTani.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnTani.TabStop = False
        Me.cnTani.Width = 50
        '
        'cnSanti
        '
        Me.cnSanti.DataPropertyName = "dtSanti"
        DataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle25.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnSanti.DefaultCellStyle = DataGridViewCellStyle25
        Me.cnSanti.HeaderText = "産地"
        Me.cnSanti.Name = "cnSanti"
        Me.cnSanti.ReadOnly = True
        Me.cnSanti.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnSanti.TabStop = False
        Me.cnSanti.Visible = False
        '
        'cnDispNo
        '
        Me.cnDispNo.DataPropertyName = "dtDispNo"
        DataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle26.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnDispNo.DefaultCellStyle = DataGridViewCellStyle26
        Me.cnDispNo.HeaderText = "表示順"
        Me.cnDispNo.Name = "cnDispNo"
        Me.cnDispNo.ReadOnly = True
        Me.cnDispNo.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnDispNo.TabStop = False
        Me.cnDispNo.Width = 60
        '
        'cnMemo
        '
        Me.cnMemo.DataPropertyName = "dtMemo"
        Me.cnMemo.HeaderText = "メモ"
        Me.cnMemo.Name = "cnMemo"
        Me.cnMemo.ReadOnly = True
        Me.cnMemo.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnMemo.TabStop = False
        Me.cnMemo.Visible = False
        '
        'cnUpdNm
        '
        Me.cnUpdNm.DataPropertyName = "dtUpdNm"
        DataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle27.BackColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle27.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnUpdNm.DefaultCellStyle = DataGridViewCellStyle27
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
        DataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle28.BackColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle28.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnUpdDt.DefaultCellStyle = DataGridViewCellStyle28
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
        Me.cnModFlg.Name = "cnModFlg"
        Me.cnModFlg.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnModFlg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnModFlg.TabStop = False
        Me.cnModFlg.Visible = False
        '
        'cnHideShohinCode
        '
        Me.cnHideShohinCode.DataPropertyName = "dtHideShohinCode"
        Me.cnHideShohinCode.HeaderText = "変更前商品コード"
        Me.cnHideShohinCode.Name = "cnHideShohinCode"
        Me.cnHideShohinCode.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cnHideShohinCode.TabStop = False
        Me.cnHideShohinCode.Visible = False
        '
        'cmdHenkou
        '
        Me.cmdHenkou.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmdHenkou.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdHenkou.Location = New System.Drawing.Point(629, 14)
        Me.cmdHenkou.Name = "cmdHenkou"
        Me.cmdHenkou.Size = New System.Drawing.Size(121, 39)
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
        Me.cmdTuika.Size = New System.Drawing.Size(121, 39)
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
        Me.cmdFukusya.Size = New System.Drawing.Size(121, 39)
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
        Me.cmdSansyou.Size = New System.Drawing.Size(121, 39)
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
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 483)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(1041, 56)
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
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(1047, 542)
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
        Me.cmdKensaku.Location = New System.Drawing.Point(885, 45)
        Me.cmdKensaku.Name = "cmdKensaku"
        Me.cmdKensaku.Size = New System.Drawing.Size(102, 36)
        Me.cmdKensaku.TabIndex = 134
        Me.cmdKensaku.Text = "検索(&S)"
        Me.cmdKensaku.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel5, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel6, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel7, 0, 1)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 42)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(832, 43)
        Me.TableLayoutPanel2.TabIndex = 133
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 2
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.Label39, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.txtShohinName, 1, 0)
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel5.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(499, 21)
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
        Me.Label39.Size = New System.Drawing.Size(122, 21)
        Me.Label39.TabIndex = 5
        Me.Label39.Text = "商品名"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtShohinName
        '
        Me.txtShohinName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtShohinName.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShohinName.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtShohinName.Location = New System.Drawing.Point(122, 0)
        Me.txtShohinName.Margin = New System.Windows.Forms.Padding(0)
        Me.txtShohinName.Name = "txtShohinName"
        Me.txtShohinName.Size = New System.Drawing.Size(377, 22)
        Me.txtShohinName.TabIndex = 0
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 1
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.Label47, 0, 0)
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(499, 0)
        Me.TableLayoutPanel6.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 1
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(333, 21)
        Me.TableLayoutPanel6.TabIndex = 1
        '
        'Label47
        '
        Me.Label47.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label47.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label47.Location = New System.Drawing.Point(3, 0)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(175, 22)
        Me.Label47.TabIndex = 5
        Me.Label47.Text = "（一部一致検索）"
        Me.Label47.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Margin = New System.Windows.Forms.Padding(0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(122, 22)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "販売仕入区分"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboHanbaiSiireKbn
        '
        Me.cboHanbaiSiireKbn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cboHanbaiSiireKbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboHanbaiSiireKbn.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cboHanbaiSiireKbn.FormattingEnabled = True
        Me.cboHanbaiSiireKbn.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.cboHanbaiSiireKbn.Location = New System.Drawing.Point(122, 0)
        Me.cboHanbaiSiireKbn.Margin = New System.Windows.Forms.Padding(0)
        Me.cboHanbaiSiireKbn.MaxDropDownItems = 9
        Me.cboHanbaiSiireKbn.Name = "cboHanbaiSiireKbn"
        Me.cboHanbaiSiireKbn.Size = New System.Drawing.Size(113, 23)
        Me.cboHanbaiSiireKbn.TabIndex = 6
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.ColumnCount = 2
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel7.Controls.Add(Me.cboHanbaiSiireKbn, 1, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.Label2, 0, 0)
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(0, 21)
        Me.TableLayoutPanel7.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 1
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(499, 22)
        Me.TableLayoutPanel7.TabIndex = 8
        '
        'frmM30F10_ShohinList
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1058, 561)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel4)
        Me.Name = "frmM30F10_ShohinList"
        Me.Text = "商品マスタ一覧（M30F10）"
        CType(Me.dgvIchiran, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel7.ResumeLayout(False)
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
    Friend WithEvents txtShohinName As TextBox
    Friend WithEvents Label39 As Label
    Friend WithEvents Label47 As Label
    Friend WithEvents cmdKensaku As Button
    Friend WithEvents cnShohinCode As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnShohinName As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnShohinRyakuName As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnShohinNameKana As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnHanbaiSiireKbn As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnHanbaiSiireKbnName As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnDaibunrui As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnKazeiKbn As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnKazeiKbnName As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnReitoKbn As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnReitoKbnName As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnIrisu As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnTani As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnSanti As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnDispNo As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnMemo As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnUpdNm As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnUpdDt As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnModFlg As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnHideShohinCode As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents TableLayoutPanel7 As TableLayoutPanel
    Friend WithEvents cboHanbaiSiireKbn As ComboBox
    Friend WithEvents Label2 As Label
End Class
