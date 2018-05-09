<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmH06F10_SelectShiire
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
        Dim DataGridViewCellStyle21 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle30 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle22 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle23 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle24 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle25 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle26 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle27 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle28 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle29 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblMode = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel29 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel31 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel30 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtSyohinName = New System.Windows.Forms.TextBox()
        Me.txtSyohinCd = New System.Windows.Forms.TextBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.txtShiiresakiCd = New System.Windows.Forms.TextBox()
        Me.txtShiiresakiName = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel12 = New System.Windows.Forms.TableLayoutPanel()
        Me.chkTorikesi = New System.Windows.Forms.CheckBox()
        Me.TableLayoutPanel9 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.chkMiharai = New System.Windows.Forms.CheckBox()
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.chkTankaMiSettei = New System.Windows.Forms.CheckBox()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel10 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtDenpyoNoFrom = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel13 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtDenpyoNoTo = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.dtpShiireDateFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtpShiireDateTo = New System.Windows.Forms.DateTimePicker()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.cmdBack = New System.Windows.Forms.Button()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.TableLayoutPanel33 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel27 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel32 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel34 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel11 = New System.Windows.Forms.TableLayoutPanel()
        Me.rdbDenpyo = New System.Windows.Forms.RadioButton()
        Me.rdbMeisai = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.lblListCount = New System.Windows.Forms.Label()
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.仕入日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.伝票番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.伝票番号_コードのみ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入先CD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入先名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.商品CD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.商品名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.数量 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.単価 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.金額 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.支払 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.取消区分 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel29.SuspendLayout()
        Me.TableLayoutPanel31.SuspendLayout()
        Me.TableLayoutPanel30.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel12.SuspendLayout()
        Me.TableLayoutPanel9.SuspendLayout()
        Me.TableLayoutPanel8.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        Me.TableLayoutPanel10.SuspendLayout()
        Me.TableLayoutPanel13.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel33.SuspendLayout()
        Me.TableLayoutPanel27.SuspendLayout()
        Me.TableLayoutPanel32.SuspendLayout()
        Me.TableLayoutPanel34.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel11.SuspendLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.55766!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.04987!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.39248!))
        Me.TableLayoutPanel1.Controls.Add(Me.lblMode, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label38, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel29, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 2, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1179, 133)
        Me.TableLayoutPanel1.TabIndex = 10
        '
        'lblMode
        '
        Me.lblMode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMode.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMode.Location = New System.Drawing.Point(1035, 0)
        Me.lblMode.Name = "lblMode"
        Me.lblMode.Size = New System.Drawing.Size(141, 19)
        Me.lblMode.TabIndex = 13
        Me.lblMode.Text = "編集モード"
        Me.lblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label38
        '
        Me.Label38.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label38.AutoSize = True
        Me.Label38.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label38.Location = New System.Drawing.Point(3, 4)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(82, 15)
        Me.Label38.TabIndex = 0
        Me.Label38.Text = "■抽出条件"
        '
        'TableLayoutPanel29
        '
        Me.TableLayoutPanel29.ColumnCount = 2
        Me.TableLayoutPanel29.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.44756!))
        Me.TableLayoutPanel29.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.55245!))
        Me.TableLayoutPanel29.Controls.Add(Me.TableLayoutPanel31, 1, 0)
        Me.TableLayoutPanel29.Controls.Add(Me.TableLayoutPanel30, 0, 0)
        Me.TableLayoutPanel29.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel29.Location = New System.Drawing.Point(0, 19)
        Me.TableLayoutPanel29.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel29.Name = "TableLayoutPanel29"
        Me.TableLayoutPanel29.RowCount = 1
        Me.TableLayoutPanel29.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel29.Size = New System.Drawing.Size(572, 114)
        Me.TableLayoutPanel29.TabIndex = 20
        '
        'TableLayoutPanel31
        '
        Me.TableLayoutPanel31.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel31.ColumnCount = 1
        Me.TableLayoutPanel31.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.11765!))
        Me.TableLayoutPanel31.Controls.Add(Me.Label48, 0, 3)
        Me.TableLayoutPanel31.Controls.Add(Me.Label47, 0, 0)
        Me.TableLayoutPanel31.Controls.Add(Me.Label43, 0, 1)
        Me.TableLayoutPanel31.Controls.Add(Me.Label44, 0, 2)
        Me.TableLayoutPanel31.Location = New System.Drawing.Point(442, 0)
        Me.TableLayoutPanel31.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel31.Name = "TableLayoutPanel31"
        Me.TableLayoutPanel31.RowCount = 4
        Me.TableLayoutPanel31.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel31.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel31.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel31.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel31.Size = New System.Drawing.Size(130, 89)
        Me.TableLayoutPanel31.TabIndex = 30
        '
        'Label48
        '
        Me.Label48.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label48.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label48.Location = New System.Drawing.Point(3, 66)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(124, 22)
        Me.Label48.TabIndex = 7
        Me.Label48.Text = "（前方一致検索）"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label47
        '
        Me.Label47.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label47.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label47.Location = New System.Drawing.Point(3, 0)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(124, 22)
        Me.Label47.TabIndex = 4
        Me.Label47.Text = "（一部一致検索）"
        Me.Label47.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label43
        '
        Me.Label43.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label43.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label43.Location = New System.Drawing.Point(3, 22)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(124, 22)
        Me.Label43.TabIndex = 5
        Me.Label43.Text = "（前方一致検索）"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label44
        '
        Me.Label44.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label44.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label44.Location = New System.Drawing.Point(3, 44)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(124, 22)
        Me.Label44.TabIndex = 6
        Me.Label44.Text = "（一部一致検索）"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TableLayoutPanel30
        '
        Me.TableLayoutPanel30.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel30.ColumnCount = 2
        Me.TableLayoutPanel30.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.39468!))
        Me.TableLayoutPanel30.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.60532!))
        Me.TableLayoutPanel30.Controls.Add(Me.txtSyohinName, 1, 2)
        Me.TableLayoutPanel30.Controls.Add(Me.txtSyohinCd, 1, 3)
        Me.TableLayoutPanel30.Controls.Add(Me.Label39, 0, 0)
        Me.TableLayoutPanel30.Controls.Add(Me.Label40, 0, 1)
        Me.TableLayoutPanel30.Controls.Add(Me.Label41, 0, 2)
        Me.TableLayoutPanel30.Controls.Add(Me.Label42, 0, 3)
        Me.TableLayoutPanel30.Controls.Add(Me.txtShiiresakiCd, 1, 1)
        Me.TableLayoutPanel30.Controls.Add(Me.txtShiiresakiName, 1, 0)
        Me.TableLayoutPanel30.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel30.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel30.Name = "TableLayoutPanel30"
        Me.TableLayoutPanel30.RowCount = 4
        Me.TableLayoutPanel30.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel30.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel30.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel30.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel30.Size = New System.Drawing.Size(442, 89)
        Me.TableLayoutPanel30.TabIndex = 0
        '
        'txtSyohinName
        '
        Me.txtSyohinName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSyohinName.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSyohinName.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.txtSyohinName.Location = New System.Drawing.Point(98, 44)
        Me.txtSyohinName.Margin = New System.Windows.Forms.Padding(0)
        Me.txtSyohinName.Name = "txtSyohinName"
        Me.txtSyohinName.Size = New System.Drawing.Size(344, 22)
        Me.txtSyohinName.TabIndex = 4
        '
        'txtSyohinCd
        '
        Me.txtSyohinCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSyohinCd.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtSyohinCd.Location = New System.Drawing.Point(98, 66)
        Me.txtSyohinCd.Margin = New System.Windows.Forms.Padding(0)
        Me.txtSyohinCd.Name = "txtSyohinCd"
        Me.txtSyohinCd.Size = New System.Drawing.Size(120, 22)
        Me.txtSyohinCd.TabIndex = 4
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
        Me.Label39.Size = New System.Drawing.Size(98, 22)
        Me.Label39.TabIndex = 0
        Me.Label39.Text = "仕入先名"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label40
        '
        Me.Label40.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label40.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label40.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label40.Location = New System.Drawing.Point(0, 22)
        Me.Label40.Margin = New System.Windows.Forms.Padding(0)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(98, 22)
        Me.Label40.TabIndex = 1
        Me.Label40.Text = "仕入先CD"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label41
        '
        Me.Label41.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label41.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label41.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label41.Location = New System.Drawing.Point(0, 44)
        Me.Label41.Margin = New System.Windows.Forms.Padding(0)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(98, 22)
        Me.Label41.TabIndex = 2
        Me.Label41.Text = "商品名"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label42
        '
        Me.Label42.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label42.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label42.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label42.Location = New System.Drawing.Point(0, 66)
        Me.Label42.Margin = New System.Windows.Forms.Padding(0)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(98, 22)
        Me.Label42.TabIndex = 3
        Me.Label42.Text = "商品CD"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtShiiresakiCd
        '
        Me.txtShiiresakiCd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtShiiresakiCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShiiresakiCd.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtShiiresakiCd.Location = New System.Drawing.Point(98, 22)
        Me.txtShiiresakiCd.Margin = New System.Windows.Forms.Padding(0)
        Me.txtShiiresakiCd.Name = "txtShiiresakiCd"
        Me.txtShiiresakiCd.Size = New System.Drawing.Size(120, 22)
        Me.txtShiiresakiCd.TabIndex = 2
        '
        'txtShiiresakiName
        '
        Me.txtShiiresakiName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtShiiresakiName.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShiiresakiName.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.txtShiiresakiName.Location = New System.Drawing.Point(98, 0)
        Me.txtShiiresakiName.Margin = New System.Windows.Forms.Padding(0)
        Me.txtShiiresakiName.Name = "txtShiiresakiName"
        Me.txtShiiresakiName.Size = New System.Drawing.Size(344, 22)
        Me.txtShiiresakiName.TabIndex = 1
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel12, 0, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel9, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel8, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel7, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel4, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(572, 19)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 5
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(460, 114)
        Me.TableLayoutPanel2.TabIndex = 40
        '
        'TableLayoutPanel12
        '
        Me.TableLayoutPanel12.ColumnCount = 3
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.1588!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 88.8412!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel12.Controls.Add(Me.chkTorikesi, 2, 0)
        Me.TableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel12.Location = New System.Drawing.Point(0, 88)
        Me.TableLayoutPanel12.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel12.Name = "TableLayoutPanel12"
        Me.TableLayoutPanel12.RowCount = 1
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel12.Size = New System.Drawing.Size(460, 26)
        Me.TableLayoutPanel12.TabIndex = 11
        '
        'chkTorikesi
        '
        Me.chkTorikesi.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTorikesi.AutoSize = True
        Me.chkTorikesi.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkTorikesi.Location = New System.Drawing.Point(54, 3)
        Me.chkTorikesi.Name = "chkTorikesi"
        Me.chkTorikesi.Size = New System.Drawing.Size(149, 19)
        Me.chkTorikesi.TabIndex = 4
        Me.chkTorikesi.Text = "取消データを含める"
        Me.chkTorikesi.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel9
        '
        Me.TableLayoutPanel9.ColumnCount = 3
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.82114!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 86.17886!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel9.Controls.Add(Me.Label13, 1, 0)
        Me.TableLayoutPanel9.Controls.Add(Me.chkMiharai, 2, 0)
        Me.TableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel9.Location = New System.Drawing.Point(0, 66)
        Me.TableLayoutPanel9.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel9.Name = "TableLayoutPanel9"
        Me.TableLayoutPanel9.RowCount = 1
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel9.Size = New System.Drawing.Size(460, 22)
        Me.TableLayoutPanel9.TabIndex = 10
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label13.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.Location = New System.Drawing.Point(50, 0)
        Me.Label13.Margin = New System.Windows.Forms.Padding(0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(97, 22)
        Me.Label13.TabIndex = 3
        Me.Label13.Text = "支払"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkMiharai
        '
        Me.chkMiharai.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkMiharai.AutoSize = True
        Me.chkMiharai.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkMiharai.Location = New System.Drawing.Point(150, 1)
        Me.chkMiharai.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.chkMiharai.Name = "chkMiharai"
        Me.chkMiharai.Size = New System.Drawing.Size(116, 19)
        Me.chkMiharai.TabIndex = 6
        Me.chkMiharai.Text = "未払のみ表示"
        Me.chkMiharai.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.ColumnCount = 3
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.83705!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 86.16296!))
        Me.TableLayoutPanel8.Controls.Add(Me.Label12, 1, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.chkTankaMiSettei, 2, 0)
        Me.TableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(0, 44)
        Me.TableLayoutPanel8.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 1
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(460, 22)
        Me.TableLayoutPanel8.TabIndex = 9
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label12.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.Location = New System.Drawing.Point(50, 0)
        Me.Label12.Margin = New System.Windows.Forms.Padding(0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(97, 22)
        Me.Label12.TabIndex = 3
        Me.Label12.Text = "仕入単価"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkTankaMiSettei
        '
        Me.chkTankaMiSettei.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkTankaMiSettei.AutoSize = True
        Me.chkTankaMiSettei.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkTankaMiSettei.Location = New System.Drawing.Point(150, 1)
        Me.chkTankaMiSettei.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.chkTankaMiSettei.Name = "chkTankaMiSettei"
        Me.chkTankaMiSettei.Size = New System.Drawing.Size(131, 19)
        Me.chkTankaMiSettei.TabIndex = 5
        Me.chkTankaMiSettei.Text = "未設定のみ表示"
        Me.chkTankaMiSettei.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.ColumnCount = 5
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.78928!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.54386!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.52632!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.79904!))
        Me.TableLayoutPanel7.Controls.Add(Me.Label10, 1, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.Label11, 3, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.TableLayoutPanel10, 2, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.TableLayoutPanel13, 4, 0)
        Me.TableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(0, 22)
        Me.TableLayoutPanel7.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 1
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(460, 22)
        Me.TableLayoutPanel7.TabIndex = 8
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label10.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(50, 0)
        Me.Label10.Margin = New System.Windows.Forms.Padding(0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(97, 22)
        Me.Label10.TabIndex = 3
        Me.Label10.Text = "伝票番号"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(291, 3)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(22, 15)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "～"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel10
        '
        Me.TableLayoutPanel10.ColumnCount = 2
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel10.Controls.Add(Me.Label4, 0, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.txtDenpyoNoFrom, 1, 0)
        Me.TableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel10.Location = New System.Drawing.Point(147, 0)
        Me.TableLayoutPanel10.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel10.Name = "TableLayoutPanel10"
        Me.TableLayoutPanel10.RowCount = 1
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel10.Size = New System.Drawing.Size(136, 22)
        Me.TableLayoutPanel10.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Margin = New System.Windows.Forms.Padding(0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(27, 22)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "R"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDenpyoNoFrom
        '
        Me.txtDenpyoNoFrom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtDenpyoNoFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDenpyoNoFrom.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtDenpyoNoFrom.Location = New System.Drawing.Point(27, 0)
        Me.txtDenpyoNoFrom.Margin = New System.Windows.Forms.Padding(0)
        Me.txtDenpyoNoFrom.Name = "txtDenpyoNoFrom"
        Me.txtDenpyoNoFrom.Size = New System.Drawing.Size(109, 22)
        Me.txtDenpyoNoFrom.TabIndex = 4
        '
        'TableLayoutPanel13
        '
        Me.TableLayoutPanel13.ColumnCount = 2
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel13.Controls.Add(Me.Label5, 0, 0)
        Me.TableLayoutPanel13.Controls.Add(Me.txtDenpyoNoTo, 1, 0)
        Me.TableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel13.Location = New System.Drawing.Point(321, 0)
        Me.TableLayoutPanel13.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel13.Name = "TableLayoutPanel13"
        Me.TableLayoutPanel13.RowCount = 1
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel13.Size = New System.Drawing.Size(139, 22)
        Me.TableLayoutPanel13.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(0, 0)
        Me.Label5.Margin = New System.Windows.Forms.Padding(0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(27, 22)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "R"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDenpyoNoTo
        '
        Me.txtDenpyoNoTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDenpyoNoTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDenpyoNoTo.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtDenpyoNoTo.Location = New System.Drawing.Point(27, 0)
        Me.txtDenpyoNoTo.Margin = New System.Windows.Forms.Padding(0)
        Me.txtDenpyoNoTo.Name = "txtDenpyoNoTo"
        Me.txtDenpyoNoTo.Size = New System.Drawing.Size(112, 22)
        Me.txtDenpyoNoTo.TabIndex = 5
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 5
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.78928!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.54386!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.52632!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.79904!))
        Me.TableLayoutPanel4.Controls.Add(Me.Label6, 1, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.Label9, 3, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.dtpShiireDateFrom, 2, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.dtpShiireDateTo, 4, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(460, 22)
        Me.TableLayoutPanel4.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(50, 0)
        Me.Label6.Margin = New System.Windows.Forms.Padding(0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(97, 22)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "仕入日"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(291, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(22, 15)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "～"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpShiireDateFrom
        '
        Me.dtpShiireDateFrom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpShiireDateFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtpShiireDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpShiireDateFrom.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.dtpShiireDateFrom.Location = New System.Drawing.Point(147, 0)
        Me.dtpShiireDateFrom.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpShiireDateFrom.Name = "dtpShiireDateFrom"
        Me.dtpShiireDateFrom.Size = New System.Drawing.Size(136, 22)
        Me.dtpShiireDateFrom.TabIndex = 8
        '
        'dtpShiireDateTo
        '
        Me.dtpShiireDateTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpShiireDateTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtpShiireDateTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpShiireDateTo.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.dtpShiireDateTo.Location = New System.Drawing.Point(321, 0)
        Me.dtpShiireDateTo.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpShiireDateTo.Name = "dtpShiireDateTo"
        Me.dtpShiireDateTo.Size = New System.Drawing.Size(139, 22)
        Me.dtpShiireDateTo.TabIndex = 9
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel3.Controls.Add(Me.btnSearch, 0, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(1032, 19)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.27273!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.72727!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(147, 114)
        Me.TableLayoutPanel3.TabIndex = 60
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(45, 0)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(0)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(102, 36)
        Me.btnSearch.TabIndex = 12
        Me.btnSearch.Text = "検索(&S)"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'cmdBack
        '
        Me.cmdBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdBack.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdBack.Location = New System.Drawing.Point(217, 20)
        Me.cmdBack.Name = "cmdBack"
        Me.cmdBack.Size = New System.Drawing.Size(102, 36)
        Me.cmdBack.TabIndex = 5
        Me.cmdBack.Text = "戻る(&B)"
        Me.cmdBack.UseVisualStyleBackColor = True
        '
        'btnSelect
        '
        Me.btnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSelect.Enabled = False
        Me.btnSelect.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSelect.Location = New System.Drawing.Point(82, 20)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(102, 36)
        Me.btnSelect.TabIndex = 4
        Me.btnSelect.Text = "選択(&G)"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel33
        '
        Me.TableLayoutPanel33.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel33.ColumnCount = 2
        Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.07454!))
        Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.92546!))
        Me.TableLayoutPanel33.Controls.Add(Me.cmdBack, 0, 1)
        Me.TableLayoutPanel33.Controls.Add(Me.btnSelect, 0, 1)
        Me.TableLayoutPanel33.Location = New System.Drawing.Point(860, 516)
        Me.TableLayoutPanel33.Name = "TableLayoutPanel33"
        Me.TableLayoutPanel33.RowCount = 2
        Me.TableLayoutPanel33.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.25424!))
        Me.TableLayoutPanel33.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84.74577!))
        Me.TableLayoutPanel33.Size = New System.Drawing.Size(322, 59)
        Me.TableLayoutPanel33.TabIndex = 80
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
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.04844!))
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 64.70588!))
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.07266!))
        Me.TableLayoutPanel27.Size = New System.Drawing.Size(1185, 578)
        Me.TableLayoutPanel27.TabIndex = 0
        '
        'TableLayoutPanel32
        '
        Me.TableLayoutPanel32.ColumnCount = 1
        Me.TableLayoutPanel32.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel32.Controls.Add(Me.TableLayoutPanel34, 0, 0)
        Me.TableLayoutPanel32.Controls.Add(Me.dgvList, 0, 1)
        Me.TableLayoutPanel32.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel32.Location = New System.Drawing.Point(3, 142)
        Me.TableLayoutPanel32.Name = "TableLayoutPanel32"
        Me.TableLayoutPanel32.RowCount = 2
        Me.TableLayoutPanel32.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.81081!))
        Me.TableLayoutPanel32.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.18919!))
        Me.TableLayoutPanel32.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel32.Size = New System.Drawing.Size(1179, 368)
        Me.TableLayoutPanel32.TabIndex = 70
        '
        'TableLayoutPanel34
        '
        Me.TableLayoutPanel34.ColumnCount = 3
        Me.TableLayoutPanel34.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 79.72858!))
        Me.TableLayoutPanel34.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.32061!))
        Me.TableLayoutPanel34.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.035623!))
        Me.TableLayoutPanel34.Controls.Add(Me.TableLayoutPanel6, 0, 0)
        Me.TableLayoutPanel34.Controls.Add(Me.Label46, 2, 0)
        Me.TableLayoutPanel34.Controls.Add(Me.lblListCount, 1, 0)
        Me.TableLayoutPanel34.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel34.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel34.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel34.Name = "TableLayoutPanel34"
        Me.TableLayoutPanel34.RowCount = 1
        Me.TableLayoutPanel34.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel34.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel34.Size = New System.Drawing.Size(1179, 39)
        Me.TableLayoutPanel34.TabIndex = 0
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 3
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.29885!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.70115!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 497.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.Label3, 2, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.Panel1, 1, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.Label2, 0, 0)
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 2
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75.75758!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.24242!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(822, 33)
        Me.TableLayoutPanel6.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(344, 5)
        Me.Label3.Margin = New System.Windows.Forms.Padding(20, 0, 3, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(329, 15)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "伝票単位の場合、明細項目は先頭行の内容を表示"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.TableLayoutPanel11)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(88, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(236, 25)
        Me.Panel1.TabIndex = 9
        '
        'TableLayoutPanel11
        '
        Me.TableLayoutPanel11.ColumnCount = 2
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel11.Controls.Add(Me.rdbDenpyo, 0, 0)
        Me.TableLayoutPanel11.Controls.Add(Me.rdbMeisai, 1, 0)
        Me.TableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel11.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel11.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel11.Name = "TableLayoutPanel11"
        Me.TableLayoutPanel11.RowCount = 1
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel11.Size = New System.Drawing.Size(234, 23)
        Me.TableLayoutPanel11.TabIndex = 0
        '
        'rdbDenpyo
        '
        Me.rdbDenpyo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.rdbDenpyo.AutoSize = True
        Me.rdbDenpyo.Checked = True
        Me.rdbDenpyo.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rdbDenpyo.Location = New System.Drawing.Point(16, 2)
        Me.rdbDenpyo.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.rdbDenpyo.Name = "rdbDenpyo"
        Me.rdbDenpyo.Size = New System.Drawing.Size(85, 19)
        Me.rdbDenpyo.TabIndex = 0
        Me.rdbDenpyo.TabStop = True
        Me.rdbDenpyo.Text = "伝票単位"
        Me.rdbDenpyo.UseVisualStyleBackColor = True
        '
        'rdbMeisai
        '
        Me.rdbMeisai.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.rdbMeisai.AutoSize = True
        Me.rdbMeisai.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rdbMeisai.Location = New System.Drawing.Point(133, 2)
        Me.rdbMeisai.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.rdbMeisai.Name = "rdbMeisai"
        Me.rdbMeisai.Size = New System.Drawing.Size(85, 19)
        Me.rdbMeisai.TabIndex = 1
        Me.rdbMeisai.Text = "明細単位"
        Me.rdbMeisai.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Margin = New System.Windows.Forms.Padding(0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 25)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "■表示形式"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label46
        '
        Me.Label46.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label46.AutoSize = True
        Me.Label46.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label46.Location = New System.Drawing.Point(1157, 21)
        Me.Label46.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(19, 15)
        Me.Label46.TabIndex = 3
        Me.Label46.Text = "件"
        '
        'lblListCount
        '
        Me.lblListCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblListCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblListCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblListCount.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblListCount.Location = New System.Drawing.Point(1067, 17)
        Me.lblListCount.Name = "lblListCount"
        Me.lblListCount.Size = New System.Drawing.Size(84, 22)
        Me.lblListCount.TabIndex = 5
        Me.lblListCount.Text = "0"
        Me.lblListCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dgvList
        '
        Me.dgvList.AllowUserToAddRows = False
        Me.dgvList.AllowUserToDeleteRows = False
        Me.dgvList.AllowUserToResizeColumns = False
        Me.dgvList.AllowUserToResizeRows = False
        DataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle21.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle21.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle21
        Me.dgvList.ColumnHeadersHeight = 25
        Me.dgvList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.仕入日, Me.伝票番号, Me.伝票番号_コードのみ, Me.仕入先CD, Me.仕入先名, Me.商品CD, Me.商品名, Me.数量, Me.単価, Me.金額, Me.支払, Me.取消区分})
        DataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle30.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle30.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        DataGridViewCellStyle30.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle30.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle30.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle30.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvList.DefaultCellStyle = DataGridViewCellStyle30
        Me.dgvList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvList.Location = New System.Drawing.Point(3, 42)
        Me.dgvList.MultiSelect = False
        Me.dgvList.Name = "dgvList"
        Me.dgvList.RowHeadersVisible = False
        Me.dgvList.RowHeadersWidth = 25
        Me.dgvList.RowTemplate.Height = 21
        Me.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvList.Size = New System.Drawing.Size(1173, 323)
        Me.dgvList.TabIndex = 0
        '
        '仕入日
        '
        DataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.仕入日.DefaultCellStyle = DataGridViewCellStyle22
        Me.仕入日.HeaderText = "仕入日"
        Me.仕入日.Name = "仕入日"
        '
        '伝票番号
        '
        DataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.伝票番号.DefaultCellStyle = DataGridViewCellStyle23
        Me.伝票番号.HeaderText = "伝票番号"
        Me.伝票番号.Name = "伝票番号"
        Me.伝票番号.Width = 117
        '
        '伝票番号_コードのみ
        '
        Me.伝票番号_コードのみ.HeaderText = "伝票番号_コードのみ"
        Me.伝票番号_コードのみ.Name = "伝票番号_コードのみ"
        Me.伝票番号_コードのみ.Visible = False
        '
        '仕入先CD
        '
        DataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.仕入先CD.DefaultCellStyle = DataGridViewCellStyle24
        Me.仕入先CD.HeaderText = "仕入先CD"
        Me.仕入先CD.Name = "仕入先CD"
        Me.仕入先CD.Visible = False
        '
        '仕入先名
        '
        Me.仕入先名.HeaderText = "仕入先名"
        Me.仕入先名.Name = "仕入先名"
        Me.仕入先名.Width = 260
        '
        '商品CD
        '
        DataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.商品CD.DefaultCellStyle = DataGridViewCellStyle25
        Me.商品CD.HeaderText = "商品CD"
        Me.商品CD.Name = "商品CD"
        Me.商品CD.Visible = False
        '
        '商品名
        '
        Me.商品名.HeaderText = "商品名"
        Me.商品名.Name = "商品名"
        Me.商品名.Width = 325
        '
        '数量
        '
        DataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.数量.DefaultCellStyle = DataGridViewCellStyle26
        Me.数量.HeaderText = "数量"
        Me.数量.Name = "数量"
        '
        '単価
        '
        DataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle27.Format = "#,###.00"
        Me.単価.DefaultCellStyle = DataGridViewCellStyle27
        Me.単価.HeaderText = "単価"
        Me.単価.Name = "単価"
        '
        '金額
        '
        DataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle28.Format = "#,###"
        DataGridViewCellStyle28.NullValue = Nothing
        Me.金額.DefaultCellStyle = DataGridViewCellStyle28
        Me.金額.HeaderText = "金額"
        Me.金額.Name = "金額"
        '
        '支払
        '
        DataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.支払.DefaultCellStyle = DataGridViewCellStyle29
        Me.支払.HeaderText = "支払"
        Me.支払.Name = "支払"
        Me.支払.Width = 50
        '
        '取消区分
        '
        Me.取消区分.HeaderText = "取消区分"
        Me.取消区分.Name = "取消区分"
        Me.取消区分.Visible = False
        '
        'frmH06F10_SelectShiire
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1185, 578)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel27)
        Me.Name = "frmH06F10_SelectShiire"
        Me.Text = "仕入一覧(H06F10)"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel29.ResumeLayout(False)
        Me.TableLayoutPanel31.ResumeLayout(False)
        Me.TableLayoutPanel30.ResumeLayout(False)
        Me.TableLayoutPanel30.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel12.ResumeLayout(False)
        Me.TableLayoutPanel12.PerformLayout()
        Me.TableLayoutPanel9.ResumeLayout(False)
        Me.TableLayoutPanel9.PerformLayout()
        Me.TableLayoutPanel8.ResumeLayout(False)
        Me.TableLayoutPanel8.PerformLayout()
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.TableLayoutPanel7.PerformLayout()
        Me.TableLayoutPanel10.ResumeLayout(False)
        Me.TableLayoutPanel10.PerformLayout()
        Me.TableLayoutPanel13.ResumeLayout(False)
        Me.TableLayoutPanel13.PerformLayout()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel33.ResumeLayout(False)
        Me.TableLayoutPanel27.ResumeLayout(False)
        Me.TableLayoutPanel32.ResumeLayout(False)
        Me.TableLayoutPanel34.ResumeLayout(False)
        Me.TableLayoutPanel34.PerformLayout()
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel6.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.TableLayoutPanel11.ResumeLayout(False)
        Me.TableLayoutPanel11.PerformLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents lblMode As Label
    Friend WithEvents Label38 As Label
    Friend WithEvents TableLayoutPanel29 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel31 As TableLayoutPanel
    Friend WithEvents Label48 As Label
    Friend WithEvents Label47 As Label
    Friend WithEvents Label43 As Label
    Friend WithEvents Label44 As Label
    Friend WithEvents TableLayoutPanel30 As TableLayoutPanel
    Friend WithEvents txtSyohinCd As TextBox
    Friend WithEvents Label39 As Label
    Friend WithEvents Label40 As Label
    Friend WithEvents Label41 As Label
    Friend WithEvents Label42 As Label
    Friend WithEvents txtShiiresakiCd As TextBox
    Friend WithEvents txtShiiresakiName As TextBox
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel12 As TableLayoutPanel
    Friend WithEvents chkTorikesi As CheckBox
    Friend WithEvents TableLayoutPanel9 As TableLayoutPanel
    Friend WithEvents txtSyohinName As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents TableLayoutPanel8 As TableLayoutPanel
    Friend WithEvents Label12 As Label
    Friend WithEvents TableLayoutPanel7 As TableLayoutPanel
    Friend WithEvents txtDenpyoNoTo As TextBox
    Friend WithEvents txtDenpyoNoFrom As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents Label6 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents dtpShiireDateFrom As DateTimePicker
    Friend WithEvents dtpShiireDateTo As DateTimePicker
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents btnSearch As Button
    Friend WithEvents cmdBack As Button
    Friend WithEvents btnSelect As Button
    Friend WithEvents TableLayoutPanel33 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel27 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel32 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel34 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel6 As TableLayoutPanel
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents TableLayoutPanel11 As TableLayoutPanel
    Friend WithEvents rdbMeisai As RadioButton
    Friend WithEvents rdbDenpyo As RadioButton
    Friend WithEvents Label46 As Label
    Friend WithEvents lblListCount As Label
    Friend WithEvents dgvList As DataGridView
    Friend WithEvents TableLayoutPanel10 As TableLayoutPanel
    Friend WithEvents Label4 As Label
    Friend WithEvents TableLayoutPanel13 As TableLayoutPanel
    Friend WithEvents chkTankaMiSettei As CheckBox
    Friend WithEvents Label5 As Label
    Friend WithEvents chkMiharai As CheckBox
    Friend WithEvents 仕入日 As DataGridViewTextBoxColumn
    Friend WithEvents 伝票番号 As DataGridViewTextBoxColumn
    Friend WithEvents 伝票番号_コードのみ As DataGridViewTextBoxColumn
    Friend WithEvents 仕入先CD As DataGridViewTextBoxColumn
    Friend WithEvents 仕入先名 As DataGridViewTextBoxColumn
    Friend WithEvents 商品CD As DataGridViewTextBoxColumn
    Friend WithEvents 商品名 As DataGridViewTextBoxColumn
    Friend WithEvents 数量 As DataGridViewTextBoxColumn
    Friend WithEvents 単価 As DataGridViewTextBoxColumn
    Friend WithEvents 金額 As DataGridViewTextBoxColumn
    Friend WithEvents 支払 As DataGridViewTextBoxColumn
    Friend WithEvents 取消区分 As DataGridViewTextBoxColumn
End Class
