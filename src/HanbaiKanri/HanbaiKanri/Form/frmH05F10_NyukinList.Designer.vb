<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmH05F10_NyukinList
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.dtpNyukinDtTo = New CustomControl.NullableDateTimePicker()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblDenpyoNoTopTo = New System.Windows.Forms.Label()
        Me.txtDenpyoNoTo = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dtpNyukinDtFrom = New CustomControl.NullableDateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblDenpyoNoTopFrom = New System.Windows.Forms.Label()
        Me.txtDenpyoNoFrom = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel10 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtNyukinCD = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtNyukinNM = New System.Windows.Forms.TextBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel32 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel34 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblListCount = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.入金日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.入金伝番 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求先コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.請求先名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.売上数量 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.売上単価 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.売上金額 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.商品名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.売上伝票番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.売上伝番枝番 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.TableLayoutPanel27 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel33 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmdBack = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        Me.TableLayoutPanel8.SuspendLayout()
        Me.TableLayoutPanel10.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        Me.TableLayoutPanel32.SuspendLayout()
        Me.TableLayoutPanel34.SuspendLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel27.SuspendLayout()
        Me.TableLayoutPanel33.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.62802!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.90821!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.38177!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label38, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnSearch, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel6, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.93578!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 77.06422!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1123, 99)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Label38
        '
        Me.Label38.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label38.AutoSize = True
        Me.Label38.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label38.Location = New System.Drawing.Point(3, 7)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(82, 15)
        Me.Label38.TabIndex = 0
        Me.Label38.Text = "■抽出条件"
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(1021, 22)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(102, 36)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.Text = "検索(&S)"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel3.ColumnCount = 4
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.57843!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.08823!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 29.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel3.Controls.Add(Me.dtpNyukinDtTo, 3, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel5, 3, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.Label6, 2, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.dtpNyukinDtFrom, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Label3, 0, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel4, 1, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.Label4, 2, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(569, 22)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 3
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(414, 44)
        Me.TableLayoutPanel3.TabIndex = 1
        '
        'dtpNyukinDtTo
        '
        Me.dtpNyukinDtTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtpNyukinDtTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpNyukinDtTo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.dtpNyukinDtTo.Location = New System.Drawing.Point(285, 0)
        Me.dtpNyukinDtTo.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpNyukinDtTo.Name = "dtpNyukinDtTo"
        Me.dtpNyukinDtTo.NullValue = ""
        Me.dtpNyukinDtTo.Size = New System.Drawing.Size(127, 22)
        Me.dtpNyukinDtTo.TabIndex = 1
        Me.dtpNyukinDtTo.Value = New Date(2018, 3, 27, 10, 55, 58, 627)
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel5.ColumnCount = 2
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.96552!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 81.03448!))
        Me.TableLayoutPanel5.Controls.Add(Me.lblDenpyoNoTopTo, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.txtDenpyoNoTo, 1, 0)
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(285, 22)
        Me.TableLayoutPanel5.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(129, 22)
        Me.TableLayoutPanel5.TabIndex = 3
        '
        'lblDenpyoNoTopTo
        '
        Me.lblDenpyoNoTopTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDenpyoNoTopTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblDenpyoNoTopTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDenpyoNoTopTo.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDenpyoNoTopTo.Location = New System.Drawing.Point(0, 0)
        Me.lblDenpyoNoTopTo.Margin = New System.Windows.Forms.Padding(0)
        Me.lblDenpyoNoTopTo.Name = "lblDenpyoNoTopTo"
        Me.lblDenpyoNoTopTo.Size = New System.Drawing.Size(24, 22)
        Me.lblDenpyoNoTopTo.TabIndex = 6
        Me.lblDenpyoNoTopTo.Text = "N"
        Me.lblDenpyoNoTopTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDenpyoNoTo
        '
        Me.txtDenpyoNoTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDenpyoNoTo.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtDenpyoNoTo.Location = New System.Drawing.Point(24, 0)
        Me.txtDenpyoNoTo.Margin = New System.Windows.Forms.Padding(0)
        Me.txtDenpyoNoTo.MaxLength = 7
        Me.txtDenpyoNoTo.Name = "txtDenpyoNoTo"
        Me.txtDenpyoNoTo.Size = New System.Drawing.Size(103, 22)
        Me.txtDenpyoNoTo.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(259, 25)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(22, 15)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "～"
        '
        'dtpNyukinDtFrom
        '
        Me.dtpNyukinDtFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtpNyukinDtFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpNyukinDtFrom.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.dtpNyukinDtFrom.Location = New System.Drawing.Point(129, 0)
        Me.dtpNyukinDtFrom.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpNyukinDtFrom.Name = "dtpNyukinDtFrom"
        Me.dtpNyukinDtFrom.NullValue = ""
        Me.dtpNyukinDtFrom.Size = New System.Drawing.Size(127, 22)
        Me.dtpNyukinDtFrom.TabIndex = 0
        Me.dtpNyukinDtFrom.Value = New Date(2018, 3, 27, 10, 55, 58, 627)
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(32, 22)
        Me.Label3.Margin = New System.Windows.Forms.Padding(0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(97, 22)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "入金伝番"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel4.ColumnCount = 2
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.96552!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 81.03448!))
        Me.TableLayoutPanel4.Controls.Add(Me.lblDenpyoNoTopFrom, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.txtDenpyoNoFrom, 1, 0)
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(129, 22)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(127, 22)
        Me.TableLayoutPanel4.TabIndex = 2
        '
        'lblDenpyoNoTopFrom
        '
        Me.lblDenpyoNoTopFrom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDenpyoNoTopFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblDenpyoNoTopFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDenpyoNoTopFrom.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDenpyoNoTopFrom.Location = New System.Drawing.Point(0, 0)
        Me.lblDenpyoNoTopFrom.Margin = New System.Windows.Forms.Padding(0)
        Me.lblDenpyoNoTopFrom.Name = "lblDenpyoNoTopFrom"
        Me.lblDenpyoNoTopFrom.Size = New System.Drawing.Size(24, 22)
        Me.lblDenpyoNoTopFrom.TabIndex = 0
        Me.lblDenpyoNoTopFrom.Text = "N"
        Me.lblDenpyoNoTopFrom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDenpyoNoFrom
        '
        Me.txtDenpyoNoFrom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDenpyoNoFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDenpyoNoFrom.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtDenpyoNoFrom.Location = New System.Drawing.Point(24, 0)
        Me.txtDenpyoNoFrom.Margin = New System.Windows.Forms.Padding(0)
        Me.txtDenpyoNoFrom.MaxLength = 7
        Me.txtDenpyoNoFrom.Name = "txtDenpyoNoFrom"
        Me.txtDenpyoNoFrom.Size = New System.Drawing.Size(103, 22)
        Me.txtDenpyoNoFrom.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(259, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(22, 15)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "～"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(32, 0)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 22)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "入金日"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 2
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.40489!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.59512!))
        Me.TableLayoutPanel6.Controls.Add(Me.TableLayoutPanel8, 1, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.TableLayoutPanel7, 1, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.Label40, 0, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.Label39, 0, 1)
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(0, 22)
        Me.TableLayoutPanel6.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 2
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(569, 44)
        Me.TableLayoutPanel6.TabIndex = 0
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.ColumnCount = 2
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.59664!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.40336!))
        Me.TableLayoutPanel8.Controls.Add(Me.TableLayoutPanel10, 1, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.txtNyukinCD, 0, 0)
        Me.TableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(93, 22)
        Me.TableLayoutPanel8.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 1
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(476, 22)
        Me.TableLayoutPanel8.TabIndex = 5
        '
        'TableLayoutPanel10
        '
        Me.TableLayoutPanel10.ColumnCount = 2
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.29146!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.70854!))
        Me.TableLayoutPanel10.Controls.Add(Me.Label11, 1, 0)
        Me.TableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel10.Location = New System.Drawing.Point(79, 0)
        Me.TableLayoutPanel10.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel10.Name = "TableLayoutPanel10"
        Me.TableLayoutPanel10.RowCount = 1
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel10.Size = New System.Drawing.Size(397, 22)
        Me.TableLayoutPanel10.TabIndex = 5
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(234, 3)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(160, 15)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "（前方一致検索）"
        '
        'txtNyukinCD
        '
        Me.txtNyukinCD.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNyukinCD.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtNyukinCD.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNyukinCD.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtNyukinCD.Location = New System.Drawing.Point(0, 0)
        Me.txtNyukinCD.Margin = New System.Windows.Forms.Padding(0)
        Me.txtNyukinCD.MaxLength = 20
        Me.txtNyukinCD.Name = "txtNyukinCD"
        Me.txtNyukinCD.Size = New System.Drawing.Size(79, 22)
        Me.txtNyukinCD.TabIndex = 3
        Me.txtNyukinCD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.ColumnCount = 2
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.12605!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.87395!))
        Me.TableLayoutPanel7.Controls.Add(Me.Label10, 1, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.txtNyukinNM, 0, 0)
        Me.TableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(93, 0)
        Me.TableLayoutPanel7.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 1
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(476, 22)
        Me.TableLayoutPanel7.TabIndex = 1
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(313, 3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(160, 15)
        Me.Label10.TabIndex = 6
        Me.Label10.Text = "（一部一致検索）"
        '
        'txtNyukinNM
        '
        Me.txtNyukinNM.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNyukinNM.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNyukinNM.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtNyukinNM.Location = New System.Drawing.Point(0, 0)
        Me.txtNyukinNM.Margin = New System.Windows.Forms.Padding(0)
        Me.txtNyukinNM.Name = "txtNyukinNM"
        Me.txtNyukinNM.Size = New System.Drawing.Size(310, 22)
        Me.txtNyukinNM.TabIndex = 1
        '
        'Label40
        '
        Me.Label40.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label40.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label40.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label40.Location = New System.Drawing.Point(1, 0)
        Me.Label40.Margin = New System.Windows.Forms.Padding(0)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(92, 22)
        Me.Label40.TabIndex = 1
        Me.Label40.Text = "請求先名"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label39
        '
        Me.Label39.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label39.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label39.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label39.Location = New System.Drawing.Point(1, 22)
        Me.Label39.Margin = New System.Windows.Forms.Padding(0)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(92, 22)
        Me.Label39.TabIndex = 0
        Me.Label39.Text = "請求先CD"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel32
        '
        Me.TableLayoutPanel32.ColumnCount = 1
        Me.TableLayoutPanel32.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel32.Controls.Add(Me.TableLayoutPanel34, 0, 0)
        Me.TableLayoutPanel32.Controls.Add(Me.dgvList, 0, 1)
        Me.TableLayoutPanel32.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel32.Location = New System.Drawing.Point(3, 108)
        Me.TableLayoutPanel32.Name = "TableLayoutPanel32"
        Me.TableLayoutPanel32.RowCount = 2
        Me.TableLayoutPanel32.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.459949!))
        Me.TableLayoutPanel32.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93.54005!))
        Me.TableLayoutPanel32.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel32.Size = New System.Drawing.Size(1123, 393)
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
        Me.TableLayoutPanel34.Size = New System.Drawing.Size(1123, 25)
        Me.TableLayoutPanel34.TabIndex = 5
        '
        'lblListCount
        '
        Me.lblListCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblListCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblListCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblListCount.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblListCount.Location = New System.Drawing.Point(1008, 3)
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
        Me.Label46.Location = New System.Drawing.Point(1098, 7)
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
        Me.dgvList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.入金日, Me.入金伝番, Me.請求先コード, Me.請求先名, Me.売上数量, Me.売上単価, Me.売上金額, Me.商品名, Me.売上伝票番号, Me.売上伝番枝番})
        Me.dgvList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvList.Location = New System.Drawing.Point(3, 28)
        Me.dgvList.MultiSelect = False
        Me.dgvList.Name = "dgvList"
        Me.dgvList.RowHeadersVisible = False
        Me.dgvList.RowTemplate.Height = 21
        Me.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvList.Size = New System.Drawing.Size(1117, 362)
        Me.dgvList.TabIndex = 6
        '
        '入金日
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.入金日.DefaultCellStyle = DataGridViewCellStyle2
        Me.入金日.HeaderText = "入金日"
        Me.入金日.Name = "入金日"
        '
        '入金伝番
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.入金伝番.DefaultCellStyle = DataGridViewCellStyle3
        Me.入金伝番.HeaderText = "入金伝番"
        Me.入金伝番.Name = "入金伝番"
        '
        '請求先コード
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.請求先コード.DefaultCellStyle = DataGridViewCellStyle4
        Me.請求先コード.HeaderText = "請求先CD"
        Me.請求先コード.Name = "請求先コード"
        Me.請求先コード.Width = 120
        '
        '請求先名
        '
        DataGridViewCellStyle5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.請求先名.DefaultCellStyle = DataGridViewCellStyle5
        Me.請求先名.HeaderText = "請求先名"
        Me.請求先名.Name = "請求先名"
        Me.請求先名.Width = 235
        '
        '売上数量
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.売上数量.DefaultCellStyle = DataGridViewCellStyle6
        Me.売上数量.HeaderText = "売上金額"
        Me.売上数量.Name = "売上数量"
        '
        '売上単価
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.売上単価.DefaultCellStyle = DataGridViewCellStyle7
        Me.売上単価.HeaderText = "消費税"
        Me.売上単価.Name = "売上単価"
        '
        '売上金額
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.売上金額.DefaultCellStyle = DataGridViewCellStyle8
        Me.売上金額.HeaderText = "入金額"
        Me.売上金額.Name = "売上金額"
        '
        '商品名
        '
        DataGridViewCellStyle9.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.商品名.DefaultCellStyle = DataGridViewCellStyle9
        Me.商品名.HeaderText = "備考"
        Me.商品名.Name = "商品名"
        Me.商品名.Width = 242
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
        'btnSelect
        '
        Me.btnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSelect.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSelect.Location = New System.Drawing.Point(60, 20)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(102, 36)
        Me.btnSelect.TabIndex = 4
        Me.btnSelect.Text = "選択(&G)"
        Me.btnSelect.UseVisualStyleBackColor = True
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
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.19788!))
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 69.20415!))
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.62976!))
        Me.TableLayoutPanel27.Size = New System.Drawing.Size(1129, 578)
        Me.TableLayoutPanel27.TabIndex = 5
        '
        'TableLayoutPanel33
        '
        Me.TableLayoutPanel33.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel33.ColumnCount = 3
        Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.07454!))
        Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.92546!))
        Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.TableLayoutPanel33.Controls.Add(Me.cmdBack, 0, 1)
        Me.TableLayoutPanel33.Controls.Add(Me.btnSelect, 0, 1)
        Me.TableLayoutPanel33.Location = New System.Drawing.Point(804, 516)
        Me.TableLayoutPanel33.Name = "TableLayoutPanel33"
        Me.TableLayoutPanel33.RowCount = 2
        Me.TableLayoutPanel33.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.25424!))
        Me.TableLayoutPanel33.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84.74576!))
        Me.TableLayoutPanel33.Size = New System.Drawing.Size(322, 59)
        Me.TableLayoutPanel33.TabIndex = 2
        '
        'cmdBack
        '
        Me.cmdBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdBack.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdBack.Location = New System.Drawing.Point(179, 20)
        Me.cmdBack.Name = "cmdBack"
        Me.cmdBack.Size = New System.Drawing.Size(102, 36)
        Me.cmdBack.TabIndex = 5
        Me.cmdBack.Text = "戻る(&B)"
        Me.cmdBack.UseVisualStyleBackColor = True
        '
        'frmH05F10_NyukinList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1129, 578)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel27)
        Me.Name = "frmH05F10_NyukinList"
        Me.Text = "入金一覧(H05F10)"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel8.ResumeLayout(False)
        Me.TableLayoutPanel8.PerformLayout()
        Me.TableLayoutPanel10.ResumeLayout(False)
        Me.TableLayoutPanel10.PerformLayout()
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.TableLayoutPanel7.PerformLayout()
        Me.TableLayoutPanel32.ResumeLayout(False)
        Me.TableLayoutPanel34.ResumeLayout(False)
        Me.TableLayoutPanel34.PerformLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel27.ResumeLayout(False)
        Me.TableLayoutPanel33.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Label38 As Label
    Friend WithEvents btnSearch As Button
    Friend WithEvents TableLayoutPanel6 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel8 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel10 As TableLayoutPanel
    Friend WithEvents Label11 As Label
    Friend WithEvents txtNyukinCD As TextBox
    Friend WithEvents TableLayoutPanel7 As TableLayoutPanel
    Friend WithEvents Label10 As Label
    Friend WithEvents txtNyukinNM As TextBox
    Friend WithEvents Label40 As Label
    Friend WithEvents Label39 As Label
    Friend WithEvents TableLayoutPanel32 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel34 As TableLayoutPanel
    Friend WithEvents lblListCount As Label
    Friend WithEvents Label46 As Label
    Friend WithEvents dgvList As DataGridView
    Friend WithEvents btnSelect As Button
    Friend WithEvents TableLayoutPanel27 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel33 As TableLayoutPanel
    Friend WithEvents cmdBack As Button
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents lblDenpyoNoTopTo As Label
    Friend WithEvents txtDenpyoNoTo As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents lblDenpyoNoTopFrom As Label
    Friend WithEvents txtDenpyoNoFrom As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents dtpNyukinDtFrom As CustomControl.NullableDateTimePicker
    Friend WithEvents dtpNyukinDtTo As CustomControl.NullableDateTimePicker
    Friend WithEvents 入金日 As DataGridViewTextBoxColumn
    Friend WithEvents 入金伝番 As DataGridViewTextBoxColumn
    Friend WithEvents 請求先コード As DataGridViewTextBoxColumn
    Friend WithEvents 請求先名 As DataGridViewTextBoxColumn
    Friend WithEvents 売上数量 As DataGridViewTextBoxColumn
    Friend WithEvents 売上単価 As DataGridViewTextBoxColumn
    Friend WithEvents 売上金額 As DataGridViewTextBoxColumn
    Friend WithEvents 商品名 As DataGridViewTextBoxColumn
    Friend WithEvents 売上伝票番号 As DataGridViewTextBoxColumn
    Friend WithEvents 売上伝番枝番 As DataGridViewTextBoxColumn
End Class
