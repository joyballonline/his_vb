<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmH03F30_SelectItakuList
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
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
        Me.TableLayoutPanel32 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel34 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblListCount = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.売上日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.着日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.伝票番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.出荷先名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.商品名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.売上数量 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.売上単価 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.売上金額 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.売上伝票番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.売上伝番枝番 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TableLayoutPanel27 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel33 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmdBack = New System.Windows.Forms.Button()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblDenpyoNoTo = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtDenpyoNoTo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpUriDtFrom = New CustomControl.NullableDateTimePicker()
        Me.dtpChakuDtFrom = New CustomControl.NullableDateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpUriDtTo = New CustomControl.NullableDateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtpChakuDtTo = New CustomControl.NullableDateTimePicker()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblDenpyoNoFrom = New System.Windows.Forms.Label()
        Me.lblDenpyoNoTopFrom = New System.Windows.Forms.Label()
        Me.txtDenpyoNoFrom = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel10 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtSyukkaNM = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblSyukkaNM = New System.Windows.Forms.Label()
        Me.txtSyukkaCD = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtShohinNM = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel32.SuspendLayout()
        Me.TableLayoutPanel34.SuspendLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel27.SuspendLayout()
        Me.TableLayoutPanel33.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel8.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel10.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel32
        '
        Me.TableLayoutPanel32.ColumnCount = 1
        Me.TableLayoutPanel32.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel32.Controls.Add(Me.TableLayoutPanel34, 0, 0)
        Me.TableLayoutPanel32.Controls.Add(Me.dgvList, 0, 1)
        Me.TableLayoutPanel32.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel32.Location = New System.Drawing.Point(3, 128)
        Me.TableLayoutPanel32.Name = "TableLayoutPanel32"
        Me.TableLayoutPanel32.RowCount = 2
        Me.TableLayoutPanel32.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.81081!))
        Me.TableLayoutPanel32.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.18919!))
        Me.TableLayoutPanel32.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel32.Size = New System.Drawing.Size(1132, 343)
        Me.TableLayoutPanel32.TabIndex = 1
        '
        'TableLayoutPanel34
        '
        Me.TableLayoutPanel34.ColumnCount = 3
        Me.TableLayoutPanel34.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel34.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.31095!))
        Me.TableLayoutPanel34.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.385159!))
        Me.TableLayoutPanel34.Controls.Add(Me.lblListCount, 1, 0)
        Me.TableLayoutPanel34.Controls.Add(Me.Label46, 2, 0)
        Me.TableLayoutPanel34.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel34.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel34.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel34.Name = "TableLayoutPanel34"
        Me.TableLayoutPanel34.RowCount = 1
        Me.TableLayoutPanel34.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel34.Size = New System.Drawing.Size(1132, 37)
        Me.TableLayoutPanel34.TabIndex = 5
        '
        'lblListCount
        '
        Me.lblListCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblListCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblListCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblListCount.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblListCount.Location = New System.Drawing.Point(1017, 15)
        Me.lblListCount.Name = "lblListCount"
        Me.lblListCount.Size = New System.Drawing.Size(84, 22)
        Me.lblListCount.TabIndex = 5
        Me.lblListCount.Text = "0"
        Me.lblListCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label46
        '
        Me.Label46.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label46.AutoSize = True
        Me.Label46.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label46.Location = New System.Drawing.Point(1107, 19)
        Me.Label46.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(22, 15)
        Me.Label46.TabIndex = 3
        Me.Label46.Text = "件"
        '
        'dgvList
        '
        Me.dgvList.AllowUserToAddRows = False
        Me.dgvList.AllowUserToDeleteRows = False
        Me.dgvList.AllowUserToResizeColumns = False
        Me.dgvList.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvList.ColumnHeadersHeight = 25
        Me.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.売上日, Me.着日, Me.伝票番号, Me.出荷先名, Me.商品名, Me.売上数量, Me.売上単価, Me.売上金額, Me.売上伝票番号, Me.売上伝番枝番})
        Me.dgvList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvList.Location = New System.Drawing.Point(3, 40)
        Me.dgvList.MultiSelect = False
        Me.dgvList.Name = "dgvList"
        Me.dgvList.RowHeadersVisible = False
        Me.dgvList.RowTemplate.Height = 21
        Me.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvList.Size = New System.Drawing.Size(1126, 300)
        Me.dgvList.TabIndex = 6
        '
        '売上日
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.売上日.DefaultCellStyle = DataGridViewCellStyle2
        Me.売上日.HeaderText = "売上日"
        Me.売上日.Name = "売上日"
        '
        '着日
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.着日.DefaultCellStyle = DataGridViewCellStyle3
        Me.着日.HeaderText = "着日"
        Me.着日.Name = "着日"
        '
        '伝票番号
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.伝票番号.DefaultCellStyle = DataGridViewCellStyle4
        Me.伝票番号.HeaderText = "伝番-枝番-行番"
        Me.伝票番号.Name = "伝票番号"
        Me.伝票番号.Width = 120
        '
        '出荷先名
        '
        DataGridViewCellStyle5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.出荷先名.DefaultCellStyle = DataGridViewCellStyle5
        Me.出荷先名.HeaderText = "出荷先名"
        Me.出荷先名.Name = "出荷先名"
        Me.出荷先名.Width = 200
        '
        '商品名
        '
        DataGridViewCellStyle6.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.商品名.DefaultCellStyle = DataGridViewCellStyle6
        Me.商品名.HeaderText = "商品名"
        Me.商品名.Name = "商品名"
        Me.商品名.Width = 255
        '
        '売上数量
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.売上数量.DefaultCellStyle = DataGridViewCellStyle7
        Me.売上数量.HeaderText = "売上数量"
        Me.売上数量.Name = "売上数量"
        Me.売上数量.Width = 110
        '
        '売上単価
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.売上単価.DefaultCellStyle = DataGridViewCellStyle8
        Me.売上単価.HeaderText = "売上単価"
        Me.売上単価.Name = "売上単価"
        Me.売上単価.Width = 110
        '
        '売上金額
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.売上金額.DefaultCellStyle = DataGridViewCellStyle9
        Me.売上金額.HeaderText = "売上金額"
        Me.売上金額.Name = "売上金額"
        Me.売上金額.Width = 110
        '
        '売上伝票番号
        '
        DataGridViewCellStyle10.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.売上伝票番号.DefaultCellStyle = DataGridViewCellStyle10
        Me.売上伝票番号.HeaderText = "売上伝票番号"
        Me.売上伝票番号.Name = "売上伝票番号"
        Me.売上伝票番号.Visible = False
        '
        '売上伝番枝番
        '
        DataGridViewCellStyle11.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.売上伝番枝番.DefaultCellStyle = DataGridViewCellStyle11
        Me.売上伝番枝番.HeaderText = "売上伝番枝番"
        Me.売上伝番枝番.Name = "売上伝番枝番"
        Me.売上伝番枝番.Visible = False
        '
        'TableLayoutPanel27
        '
        Me.TableLayoutPanel27.ColumnCount = 1
        Me.TableLayoutPanel27.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel27.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel27.Controls.Add(Me.TableLayoutPanel33, 0, 2)
        Me.TableLayoutPanel27.Controls.Add(Me.TableLayoutPanel32, 0, 1)
        Me.TableLayoutPanel27.Controls.Add(Me.TableLayoutPanel1, 0, 0)
        Me.TableLayoutPanel27.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel27.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel27.Name = "TableLayoutPanel27"
        Me.TableLayoutPanel27.RowCount = 3
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.25142!))
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 64.57565!))
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.36162!))
        Me.TableLayoutPanel27.Size = New System.Drawing.Size(1138, 542)
        Me.TableLayoutPanel27.TabIndex = 4
        '
        'TableLayoutPanel33
        '
        Me.TableLayoutPanel33.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel33.ColumnCount = 3
        Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.07454!))
        Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.92546!))
        Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 57.0!))
        Me.TableLayoutPanel33.Controls.Add(Me.cmdBack, 0, 1)
        Me.TableLayoutPanel33.Controls.Add(Me.btnSelect, 0, 1)
        Me.TableLayoutPanel33.Location = New System.Drawing.Point(813, 480)
        Me.TableLayoutPanel33.Name = "TableLayoutPanel33"
        Me.TableLayoutPanel33.RowCount = 2
        Me.TableLayoutPanel33.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.25424!))
        Me.TableLayoutPanel33.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84.74576!))
        Me.TableLayoutPanel33.Size = New System.Drawing.Size(322, 59)
        Me.TableLayoutPanel33.TabIndex = 5
        '
        'cmdBack
        '
        Me.cmdBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdBack.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdBack.Location = New System.Drawing.Point(159, 20)
        Me.cmdBack.Name = "cmdBack"
        Me.cmdBack.Size = New System.Drawing.Size(102, 36)
        Me.cmdBack.TabIndex = 5
        Me.cmdBack.Text = "戻る(&B)"
        Me.cmdBack.UseVisualStyleBackColor = True
        '
        'btnSelect
        '
        Me.btnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSelect.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSelect.Location = New System.Drawing.Point(48, 20)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(102, 36)
        Me.btnSelect.TabIndex = 4
        Me.btnSelect.Text = "選択(&G)"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.62802!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.50883!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.87279!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label38, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnSearch, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel4, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.93578!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 77.06422!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1132, 119)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Label38
        '
        Me.Label38.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label38.AutoSize = True
        Me.Label38.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label38.Location = New System.Drawing.Point(3, 12)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(82, 15)
        Me.Label38.TabIndex = 0
        Me.Label38.Text = "■抽出条件"
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(1030, 27)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(102, 36)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.Text = "検索(&S)"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel3, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(573, 27)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(367, 92)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 4
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.27988!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.5277!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.48397!))
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel8, 3, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Label2, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.dtpUriDtFrom, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.dtpChakuDtFrom, 1, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Label3, 2, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.dtpUriDtTo, 3, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Label4, 2, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Label5, 2, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.dtpChakuDtTo, 3, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Label13, 0, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel7, 1, 2)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 4
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(367, 92)
        Me.TableLayoutPanel3.TabIndex = 2
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel8.ColumnCount = 2
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.03053!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.96947!))
        Me.TableLayoutPanel8.Controls.Add(Me.lblDenpyoNoTo, 0, 1)
        Me.TableLayoutPanel8.Controls.Add(Me.Label14, 0, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.txtDenpyoNoTo, 1, 0)
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(236, 44)
        Me.TableLayoutPanel8.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 2
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(131, 22)
        Me.TableLayoutPanel8.TabIndex = 16
        '
        'lblDenpyoNoTo
        '
        Me.lblDenpyoNoTo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDenpyoNoTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblDenpyoNoTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDenpyoNoTo.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDenpyoNoTo.Location = New System.Drawing.Point(0, 0)
        Me.lblDenpyoNoTo.Margin = New System.Windows.Forms.Padding(0)
        Me.lblDenpyoNoTo.Name = "lblDenpyoNoTo"
        Me.lblDenpyoNoTo.Size = New System.Drawing.Size(20, 22)
        Me.lblDenpyoNoTo.TabIndex = 15
        Me.lblDenpyoNoTo.Text = "T"
        Me.lblDenpyoNoTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label14
        '
        Me.Label14.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label14.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label14.Location = New System.Drawing.Point(0, 0)
        Me.Label14.Margin = New System.Windows.Forms.Padding(0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(20, 1)
        Me.Label14.TabIndex = 6
        Me.Label14.Text = "T"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDenpyoNoTo
        '
        Me.txtDenpyoNoTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtDenpyoNoTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDenpyoNoTo.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtDenpyoNoTo.Location = New System.Drawing.Point(20, 0)
        Me.txtDenpyoNoTo.Margin = New System.Windows.Forms.Padding(0)
        Me.txtDenpyoNoTo.MaxLength = 7
        Me.txtDenpyoNoTo.Name = "txtDenpyoNoTo"
        Me.txtDenpyoNoTo.Size = New System.Drawing.Size(92, 22)
        Me.txtDenpyoNoTo.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 22)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "売上日"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 22)
        Me.Label2.Margin = New System.Windows.Forms.Padding(0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 22)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "着日"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpUriDtFrom
        '
        Me.dtpUriDtFrom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dtpUriDtFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtpUriDtFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpUriDtFrom.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.dtpUriDtFrom.Location = New System.Drawing.Point(95, 0)
        Me.dtpUriDtFrom.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpUriDtFrom.Name = "dtpUriDtFrom"
        Me.dtpUriDtFrom.NullValue = ""
        Me.dtpUriDtFrom.Size = New System.Drawing.Size(113, 22)
        Me.dtpUriDtFrom.TabIndex = 0
        Me.dtpUriDtFrom.Value = New Date(2018, 3, 27, 10, 55, 58, 627)
        '
        'dtpChakuDtFrom
        '
        Me.dtpChakuDtFrom.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpChakuDtFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtpChakuDtFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpChakuDtFrom.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.dtpChakuDtFrom.Location = New System.Drawing.Point(95, 22)
        Me.dtpChakuDtFrom.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpChakuDtFrom.Name = "dtpChakuDtFrom"
        Me.dtpChakuDtFrom.NullValue = ""
        Me.dtpChakuDtFrom.Size = New System.Drawing.Size(113, 22)
        Me.dtpChakuDtFrom.TabIndex = 2
        Me.dtpChakuDtFrom.Value = New Date(2018, 3, 27, 10, 55, 58, 627)
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(211, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(22, 15)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "～"
        '
        'dtpUriDtTo
        '
        Me.dtpUriDtTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dtpUriDtTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtpUriDtTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpUriDtTo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.dtpUriDtTo.Location = New System.Drawing.Point(236, 0)
        Me.dtpUriDtTo.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpUriDtTo.Name = "dtpUriDtTo"
        Me.dtpUriDtTo.NullValue = ""
        Me.dtpUriDtTo.Size = New System.Drawing.Size(113, 22)
        Me.dtpUriDtTo.TabIndex = 1
        Me.dtpUriDtTo.Value = New Date(2018, 3, 27, 10, 55, 58, 627)
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(211, 25)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(22, 15)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "～"
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(211, 47)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(22, 15)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "～"
        '
        'dtpChakuDtTo
        '
        Me.dtpChakuDtTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dtpChakuDtTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtpChakuDtTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpChakuDtTo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.dtpChakuDtTo.Location = New System.Drawing.Point(236, 22)
        Me.dtpChakuDtTo.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpChakuDtTo.Name = "dtpChakuDtTo"
        Me.dtpChakuDtTo.NullValue = ""
        Me.dtpChakuDtTo.Size = New System.Drawing.Size(113, 22)
        Me.dtpChakuDtTo.TabIndex = 3
        Me.dtpChakuDtTo.Value = New Date(2018, 3, 27, 10, 55, 58, 627)
        '
        'Label13
        '
        Me.Label13.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label13.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.Location = New System.Drawing.Point(0, 44)
        Me.Label13.Margin = New System.Windows.Forms.Padding(0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(95, 22)
        Me.Label13.TabIndex = 8
        Me.Label13.Text = "伝票番号"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel7.ColumnCount = 2
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.96552!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 81.03448!))
        Me.TableLayoutPanel7.Controls.Add(Me.lblDenpyoNoFrom, 0, 1)
        Me.TableLayoutPanel7.Controls.Add(Me.lblDenpyoNoTopFrom, 0, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.txtDenpyoNoFrom, 1, 0)
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(95, 44)
        Me.TableLayoutPanel7.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 2
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(113, 22)
        Me.TableLayoutPanel7.TabIndex = 15
        '
        'lblDenpyoNoFrom
        '
        Me.lblDenpyoNoFrom.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDenpyoNoFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblDenpyoNoFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDenpyoNoFrom.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDenpyoNoFrom.Location = New System.Drawing.Point(0, 0)
        Me.lblDenpyoNoFrom.Margin = New System.Windows.Forms.Padding(0)
        Me.lblDenpyoNoFrom.Name = "lblDenpyoNoFrom"
        Me.lblDenpyoNoFrom.Size = New System.Drawing.Size(21, 22)
        Me.lblDenpyoNoFrom.TabIndex = 17
        Me.lblDenpyoNoFrom.Text = "T"
        Me.lblDenpyoNoFrom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDenpyoNoTopFrom
        '
        Me.lblDenpyoNoTopFrom.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDenpyoNoTopFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblDenpyoNoTopFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDenpyoNoTopFrom.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDenpyoNoTopFrom.Location = New System.Drawing.Point(0, 0)
        Me.lblDenpyoNoTopFrom.Margin = New System.Windows.Forms.Padding(0)
        Me.lblDenpyoNoTopFrom.Name = "lblDenpyoNoTopFrom"
        Me.lblDenpyoNoTopFrom.Size = New System.Drawing.Size(21, 1)
        Me.lblDenpyoNoTopFrom.TabIndex = 6
        Me.lblDenpyoNoTopFrom.Text = "T"
        Me.lblDenpyoNoTopFrom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDenpyoNoFrom
        '
        Me.txtDenpyoNoFrom.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDenpyoNoFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDenpyoNoFrom.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtDenpyoNoFrom.Location = New System.Drawing.Point(21, 0)
        Me.txtDenpyoNoFrom.Margin = New System.Windows.Forms.Padding(0)
        Me.txtDenpyoNoFrom.MaxLength = 7
        Me.txtDenpyoNoFrom.Name = "txtDenpyoNoFrom"
        Me.txtDenpyoNoFrom.Size = New System.Drawing.Size(92, 22)
        Me.txtDenpyoNoFrom.TabIndex = 0
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 2
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.41509!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73.58491!))
        Me.TableLayoutPanel4.Controls.Add(Me.Label7, 0, 2)
        Me.TableLayoutPanel4.Controls.Add(Me.TableLayoutPanel10, 1, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.Label6, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.TableLayoutPanel5, 1, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.TableLayoutPanel6, 1, 2)
        Me.TableLayoutPanel4.Controls.Add(Me.Label8, 0, 1)
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(0, 27)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 4
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(521, 92)
        Me.TableLayoutPanel4.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(0, 44)
        Me.Label7.Margin = New System.Windows.Forms.Padding(0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(137, 22)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "商品名"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel10
        '
        Me.TableLayoutPanel10.ColumnCount = 2
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.54167!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.45833!))
        Me.TableLayoutPanel10.Controls.Add(Me.Label12, 0, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.txtSyukkaNM, 0, 0)
        Me.TableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel10.Location = New System.Drawing.Point(137, 22)
        Me.TableLayoutPanel10.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel10.Name = "TableLayoutPanel10"
        Me.TableLayoutPanel10.RowCount = 1
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel10.Size = New System.Drawing.Size(384, 22)
        Me.TableLayoutPanel10.TabIndex = 4
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.Location = New System.Drawing.Point(247, 3)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(134, 15)
        Me.Label12.TabIndex = 7
        Me.Label12.Text = "（一部一致検索）"
        '
        'txtSyukkaNM
        '
        Me.txtSyukkaNM.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSyukkaNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSyukkaNM.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtSyukkaNM.Location = New System.Drawing.Point(0, 0)
        Me.txtSyukkaNM.Margin = New System.Windows.Forms.Padding(0)
        Me.txtSyukkaNM.Name = "txtSyukkaNM"
        Me.txtSyukkaNM.Size = New System.Drawing.Size(244, 22)
        Me.txtSyukkaNM.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 0)
        Me.Label6.Margin = New System.Windows.Forms.Padding(0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(137, 22)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "出荷先CD"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 2
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.73958!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.26041!))
        Me.TableLayoutPanel5.Controls.Add(Me.lblSyukkaNM, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.txtSyukkaCD, 0, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(137, 0)
        Me.TableLayoutPanel5.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(384, 22)
        Me.TableLayoutPanel5.TabIndex = 3
        '
        'lblSyukkaNM
        '
        Me.lblSyukkaNM.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSyukkaNM.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblSyukkaNM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSyukkaNM.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSyukkaNM.Location = New System.Drawing.Point(95, 0)
        Me.lblSyukkaNM.Margin = New System.Windows.Forms.Padding(0)
        Me.lblSyukkaNM.Name = "lblSyukkaNM"
        Me.lblSyukkaNM.Size = New System.Drawing.Size(289, 22)
        Me.lblSyukkaNM.TabIndex = 7
        Me.lblSyukkaNM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSyukkaCD
        '
        Me.txtSyukkaCD.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSyukkaCD.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSyukkaCD.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSyukkaCD.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtSyukkaCD.Location = New System.Drawing.Point(0, 0)
        Me.txtSyukkaCD.Margin = New System.Windows.Forms.Padding(0)
        Me.txtSyukkaCD.MaxLength = 20
        Me.txtSyukkaCD.Name = "txtSyukkaCD"
        Me.txtSyukkaCD.Size = New System.Drawing.Size(95, 22)
        Me.txtSyukkaCD.TabIndex = 0
        Me.txtSyukkaCD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 2
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.28125!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.71875!))
        Me.TableLayoutPanel6.Controls.Add(Me.Label10, 0, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.txtShohinNM, 0, 0)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(137, 44)
        Me.TableLayoutPanel6.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 1
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(384, 22)
        Me.TableLayoutPanel6.TabIndex = 6
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(246, 3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(135, 15)
        Me.Label10.TabIndex = 7
        Me.Label10.Text = "（一部一致検索）"
        '
        'txtShohinNM
        '
        Me.txtShohinNM.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtShohinNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShohinNM.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtShohinNM.Location = New System.Drawing.Point(0, 0)
        Me.txtShohinNM.Margin = New System.Windows.Forms.Padding(0)
        Me.txtShohinNM.Name = "txtShohinNM"
        Me.txtShohinNM.Size = New System.Drawing.Size(243, 22)
        Me.txtShohinNM.TabIndex = 0
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 22)
        Me.Label8.Margin = New System.Windows.Forms.Padding(0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(137, 22)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "出荷先名"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmH03F30_SelectItakuList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1138, 542)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel27)
        Me.Name = "frmH03F30_SelectItakuList"
        Me.Text = "委託売上一覧(H03F30)"
        Me.TableLayoutPanel32.ResumeLayout(False)
        Me.TableLayoutPanel34.ResumeLayout(False)
        Me.TableLayoutPanel34.PerformLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel27.ResumeLayout(False)
        Me.TableLayoutPanel33.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.TableLayoutPanel8.ResumeLayout(False)
        Me.TableLayoutPanel8.PerformLayout()
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.TableLayoutPanel7.PerformLayout()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel10.ResumeLayout(False)
        Me.TableLayoutPanel10.PerformLayout()
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel6.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel32 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel34 As TableLayoutPanel
    Friend WithEvents lblListCount As Label
    Friend WithEvents Label46 As Label
    Friend WithEvents dgvList As DataGridView
    Friend WithEvents TableLayoutPanel27 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel33 As TableLayoutPanel
    Friend WithEvents cmdBack As Button
    Friend WithEvents btnSelect As Button
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Label38 As Label
    Friend WithEvents btnSearch As Button
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtDenpyoNoFrom As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents dtpUriDtFrom As CustomControl.NullableDateTimePicker
    Friend WithEvents dtpChakuDtFrom As CustomControl.NullableDateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents dtpUriDtTo As CustomControl.NullableDateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents dtpChakuDtTo As CustomControl.NullableDateTimePicker
    Friend WithEvents txtDenpyoNoTo As TextBox
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents Label7 As Label
    Friend WithEvents TableLayoutPanel10 As TableLayoutPanel
    Friend WithEvents Label12 As Label
    Friend WithEvents txtSyukkaNM As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents lblSyukkaNM As Label
    Friend WithEvents txtSyukkaCD As TextBox
    Friend WithEvents TableLayoutPanel6 As TableLayoutPanel
    Friend WithEvents Label10 As Label
    Friend WithEvents txtShohinNM As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents TableLayoutPanel7 As TableLayoutPanel
    Friend WithEvents lblDenpyoNoTopFrom As Label
    Friend WithEvents TableLayoutPanel8 As TableLayoutPanel
    Friend WithEvents Label14 As Label
    Friend WithEvents lblDenpyoNoTo As Label
    Friend WithEvents lblDenpyoNoFrom As Label
    Friend WithEvents 売上日 As DataGridViewTextBoxColumn
    Friend WithEvents 着日 As DataGridViewTextBoxColumn
    Friend WithEvents 伝票番号 As DataGridViewTextBoxColumn
    Friend WithEvents 出荷先名 As DataGridViewTextBoxColumn
    Friend WithEvents 商品名 As DataGridViewTextBoxColumn
    Friend WithEvents 売上数量 As DataGridViewTextBoxColumn
    Friend WithEvents 売上単価 As DataGridViewTextBoxColumn
    Friend WithEvents 売上金額 As DataGridViewTextBoxColumn
    Friend WithEvents 売上伝票番号 As DataGridViewTextBoxColumn
    Friend WithEvents 売上伝番枝番 As DataGridViewTextBoxColumn
End Class
