<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmH03F10_SelectItakuList
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
        Me.lblListCount = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dtpSyukkaDtTo = New CustomControl.NullableDateTimePicker()
        Me.dtpSyukkaDtFrom = New CustomControl.NullableDateTimePicker()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblDenpyoNoTopTo = New System.Windows.Forms.Label()
        Me.txtDenpyoNoTo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblDenpyoNoTopFrom = New System.Windows.Forms.Label()
        Me.txtDenpyoNoFrom = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtpChakuDtFrom = New CustomControl.NullableDateTimePicker()
        Me.dtpChakuDtTo = New CustomControl.NullableDateTimePicker()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel9 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtShohinNM = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtSyukkaNM = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblSyukkaNM = New System.Windows.Forms.Label()
        Me.txtSyukkaCD = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.rdbZan = New System.Windows.Forms.RadioButton()
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.着日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.伝票番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.出荷先名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.商品名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.委託数量 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.売上数量 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.目切数量 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.委託残数 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.伝番 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel27 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel33 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmdBack = New System.Windows.Forms.Button()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.TableLayoutPanel32 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel34 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        Me.TableLayoutPanel9.SuspendLayout()
        Me.TableLayoutPanel8.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel27.SuspendLayout()
        Me.TableLayoutPanel33.SuspendLayout()
        Me.TableLayoutPanel32.SuspendLayout()
        Me.TableLayoutPanel34.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblListCount
        '
        Me.lblListCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblListCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblListCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblListCount.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblListCount.Location = New System.Drawing.Point(925, 14)
        Me.lblListCount.Name = "lblListCount"
        Me.lblListCount.Size = New System.Drawing.Size(84, 22)
        Me.lblListCount.TabIndex = 5
        Me.lblListCount.Text = "0"
        Me.lblListCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.62802!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.90821!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.38177!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label38, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Button5, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel6, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 76.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1035, 121)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Label38
        '
        Me.Label38.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label38.AutoSize = True
        Me.Label38.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label38.Location = New System.Drawing.Point(3, 14)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(82, 15)
        Me.Label38.TabIndex = 0
        Me.Label38.Text = "■抽出条件"
        '
        'Button5
        '
        Me.Button5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button5.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button5.Location = New System.Drawing.Point(933, 29)
        Me.Button5.Margin = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(102, 36)
        Me.Button5.TabIndex = 2
        Me.Button5.Text = "検索(&S)"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel3, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(524, 29)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(382, 92)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel3.ColumnCount = 4
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.57843!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.20339!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.46328!))
        Me.TableLayoutPanel3.Controls.Add(Me.Label6, 2, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.dtpSyukkaDtTo, 3, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.dtpSyukkaDtFrom, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel5, 3, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.Label3, 0, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel4, 1, 2)
        Me.TableLayoutPanel3.Controls.Add(Me.Label4, 2, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Label2, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Label5, 2, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.dtpChakuDtFrom, 1, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.dtpChakuDtTo, 3, 1)
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 4
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(382, 87)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label6.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(234, 44)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(22, 22)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "～"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpSyukkaDtTo
        '
        Me.dtpSyukkaDtTo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpSyukkaDtTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtpSyukkaDtTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpSyukkaDtTo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.dtpSyukkaDtTo.Location = New System.Drawing.Point(259, 0)
        Me.dtpSyukkaDtTo.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpSyukkaDtTo.Name = "dtpSyukkaDtTo"
        Me.dtpSyukkaDtTo.NullValue = ""
        Me.dtpSyukkaDtTo.Size = New System.Drawing.Size(123, 22)
        Me.dtpSyukkaDtTo.TabIndex = 1
        Me.dtpSyukkaDtTo.Value = New Date(2018, 3, 27, 10, 55, 58, 627)
        '
        'dtpSyukkaDtFrom
        '
        Me.dtpSyukkaDtFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtpSyukkaDtFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpSyukkaDtFrom.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.dtpSyukkaDtFrom.Location = New System.Drawing.Point(118, 0)
        Me.dtpSyukkaDtFrom.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpSyukkaDtFrom.Name = "dtpSyukkaDtFrom"
        Me.dtpSyukkaDtFrom.NullValue = ""
        Me.dtpSyukkaDtFrom.Size = New System.Drawing.Size(113, 22)
        Me.dtpSyukkaDtFrom.TabIndex = 0
        Me.dtpSyukkaDtFrom.Value = New Date(2018, 3, 27, 10, 55, 58, 627)
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel5.ColumnCount = 2
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.96552!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 81.03448!))
        Me.TableLayoutPanel5.Controls.Add(Me.lblDenpyoNoTopTo, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.txtDenpyoNoTo, 1, 0)
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(259, 44)
        Me.TableLayoutPanel5.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(123, 22)
        Me.TableLayoutPanel5.TabIndex = 10
        '
        'lblDenpyoNoTopTo
        '
        Me.lblDenpyoNoTopTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDenpyoNoTopTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblDenpyoNoTopTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDenpyoNoTopTo.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblDenpyoNoTopTo.Location = New System.Drawing.Point(0, 0)
        Me.lblDenpyoNoTopTo.Margin = New System.Windows.Forms.Padding(0)
        Me.lblDenpyoNoTopTo.Name = "lblDenpyoNoTopTo"
        Me.lblDenpyoNoTopTo.Size = New System.Drawing.Size(23, 22)
        Me.lblDenpyoNoTopTo.TabIndex = 6
        Me.lblDenpyoNoTopTo.Text = "T"
        Me.lblDenpyoNoTopTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDenpyoNoTo
        '
        Me.txtDenpyoNoTo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDenpyoNoTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDenpyoNoTo.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtDenpyoNoTo.Location = New System.Drawing.Point(23, 0)
        Me.txtDenpyoNoTo.Margin = New System.Windows.Forms.Padding(0)
        Me.txtDenpyoNoTo.MaxLength = 7
        Me.txtDenpyoNoTo.Name = "txtDenpyoNoTo"
        Me.txtDenpyoNoTo.Size = New System.Drawing.Size(100, 22)
        Me.txtDenpyoNoTo.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(21, 44)
        Me.Label3.Margin = New System.Windows.Forms.Padding(0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(97, 22)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "伝票番号"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 2
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.96552!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 81.03448!))
        Me.TableLayoutPanel4.Controls.Add(Me.lblDenpyoNoTopFrom, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.txtDenpyoNoFrom, 1, 0)
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(118, 44)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(113, 22)
        Me.TableLayoutPanel4.TabIndex = 9
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
        Me.lblDenpyoNoTopFrom.Size = New System.Drawing.Size(21, 22)
        Me.lblDenpyoNoTopFrom.TabIndex = 6
        Me.lblDenpyoNoTopFrom.Text = "T"
        Me.lblDenpyoNoTopFrom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDenpyoNoFrom
        '
        Me.txtDenpyoNoFrom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
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
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(234, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(22, 22)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "～"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(21, 0)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 22)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "出荷日"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(21, 22)
        Me.Label2.Margin = New System.Windows.Forms.Padding(0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 22)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "着日"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(234, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(22, 22)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "～"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpChakuDtFrom
        '
        Me.dtpChakuDtFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtpChakuDtFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpChakuDtFrom.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.dtpChakuDtFrom.Location = New System.Drawing.Point(118, 22)
        Me.dtpChakuDtFrom.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpChakuDtFrom.Name = "dtpChakuDtFrom"
        Me.dtpChakuDtFrom.NullValue = ""
        Me.dtpChakuDtFrom.Size = New System.Drawing.Size(113, 22)
        Me.dtpChakuDtFrom.TabIndex = 2
        Me.dtpChakuDtFrom.Value = New Date(2018, 3, 27, 10, 55, 58, 627)
        '
        'dtpChakuDtTo
        '
        Me.dtpChakuDtTo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpChakuDtTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtpChakuDtTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpChakuDtTo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.dtpChakuDtTo.Location = New System.Drawing.Point(259, 22)
        Me.dtpChakuDtTo.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpChakuDtTo.Name = "dtpChakuDtTo"
        Me.dtpChakuDtTo.NullValue = ""
        Me.dtpChakuDtTo.Size = New System.Drawing.Size(123, 22)
        Me.dtpChakuDtTo.TabIndex = 3
        Me.dtpChakuDtTo.Value = New Date(2018, 3, 27, 10, 55, 58, 627)
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel6.ColumnCount = 2
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.95367!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82.04633!))
        Me.TableLayoutPanel6.Controls.Add(Me.TableLayoutPanel9, 1, 2)
        Me.TableLayoutPanel6.Controls.Add(Me.TableLayoutPanel8, 1, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.Label9, 0, 3)
        Me.TableLayoutPanel6.Controls.Add(Me.Label39, 0, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.Label7, 0, 1)
        Me.TableLayoutPanel6.Controls.Add(Me.Label8, 0, 2)
        Me.TableLayoutPanel6.Controls.Add(Me.TableLayoutPanel7, 1, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.Panel1, 1, 3)
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(0, 29)
        Me.TableLayoutPanel6.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 4
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(524, 92)
        Me.TableLayoutPanel6.TabIndex = 0
        '
        'TableLayoutPanel9
        '
        Me.TableLayoutPanel9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel9.ColumnCount = 2
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.90697!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.09302!))
        Me.TableLayoutPanel9.Controls.Add(Me.Label12, 0, 0)
        Me.TableLayoutPanel9.Controls.Add(Me.txtShohinNM, 0, 0)
        Me.TableLayoutPanel9.Location = New System.Drawing.Point(94, 44)
        Me.TableLayoutPanel9.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel9.Name = "TableLayoutPanel9"
        Me.TableLayoutPanel9.RowCount = 1
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel9.Size = New System.Drawing.Size(430, 21)
        Me.TableLayoutPanel9.TabIndex = 7
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.Location = New System.Drawing.Point(295, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(132, 21)
        Me.Label12.TabIndex = 8
        Me.Label12.Text = "（一部一致検索）"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtShohinNM
        '
        Me.txtShohinNM.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtShohinNM.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtShohinNM.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtShohinNM.Location = New System.Drawing.Point(0, 0)
        Me.txtShohinNM.Margin = New System.Windows.Forms.Padding(0)
        Me.txtShohinNM.Name = "txtShohinNM"
        Me.txtShohinNM.Size = New System.Drawing.Size(292, 22)
        Me.txtShohinNM.TabIndex = 7
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel8.ColumnCount = 2
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.90697!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.09302!))
        Me.TableLayoutPanel8.Controls.Add(Me.Label11, 0, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.txtSyukkaNM, 0, 0)
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(94, 22)
        Me.TableLayoutPanel8.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 1
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(430, 21)
        Me.TableLayoutPanel8.TabIndex = 6
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(295, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(132, 21)
        Me.Label11.TabIndex = 8
        Me.Label11.Text = "（一部一致検索）"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSyukkaNM
        '
        Me.txtSyukkaNM.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSyukkaNM.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSyukkaNM.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtSyukkaNM.Location = New System.Drawing.Point(0, 0)
        Me.txtSyukkaNM.Margin = New System.Windows.Forms.Padding(0)
        Me.txtSyukkaNM.Name = "txtSyukkaNM"
        Me.txtSyukkaNM.Size = New System.Drawing.Size(292, 22)
        Me.txtSyukkaNM.TabIndex = 1
        '
        'Label9
        '
        Me.Label9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label9.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(0, 66)
        Me.Label9.Margin = New System.Windows.Forms.Padding(0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(94, 22)
        Me.Label9.TabIndex = 4
        Me.Label9.Text = "表示対象"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label39
        '
        Me.Label39.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label39.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label39.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label39.Location = New System.Drawing.Point(0, 0)
        Me.Label39.Margin = New System.Windows.Forms.Padding(0)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(94, 22)
        Me.Label39.TabIndex = 1
        Me.Label39.Text = "出荷先CD"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(0, 22)
        Me.Label7.Margin = New System.Windows.Forms.Padding(0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(94, 22)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "出荷先名"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 44)
        Me.Label8.Margin = New System.Windows.Forms.Padding(0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(94, 22)
        Me.Label8.TabIndex = 3
        Me.Label8.Text = "商品名"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel7.ColumnCount = 2
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.95349!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.04651!))
        Me.TableLayoutPanel7.Controls.Add(Me.lblSyukkaNM, 0, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.txtSyukkaCD, 0, 0)
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(94, 0)
        Me.TableLayoutPanel7.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 1
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(430, 22)
        Me.TableLayoutPanel7.TabIndex = 5
        '
        'lblSyukkaNM
        '
        Me.lblSyukkaNM.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSyukkaNM.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblSyukkaNM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSyukkaNM.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSyukkaNM.Location = New System.Drawing.Point(103, 0)
        Me.lblSyukkaNM.Margin = New System.Windows.Forms.Padding(0)
        Me.lblSyukkaNM.Name = "lblSyukkaNM"
        Me.lblSyukkaNM.Size = New System.Drawing.Size(327, 22)
        Me.lblSyukkaNM.TabIndex = 7
        Me.lblSyukkaNM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSyukkaCD
        '
        Me.txtSyukkaCD.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSyukkaCD.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSyukkaCD.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtSyukkaCD.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf
        Me.txtSyukkaCD.Location = New System.Drawing.Point(0, 0)
        Me.txtSyukkaCD.Margin = New System.Windows.Forms.Padding(0)
        Me.txtSyukkaCD.MaxLength = 20
        Me.txtSyukkaCD.Name = "txtSyukkaCD"
        Me.txtSyukkaCD.Size = New System.Drawing.Size(103, 22)
        Me.txtSyukkaCD.TabIndex = 4
        Me.txtSyukkaCD.TabStop = False
        Me.txtSyukkaCD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.RadioButton2)
        Me.Panel1.Controls.Add(Me.rdbZan)
        Me.Panel1.Location = New System.Drawing.Point(94, 66)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(430, 22)
        Me.Panel1.TabIndex = 8
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.RadioButton2.Location = New System.Drawing.Point(209, 1)
        Me.RadioButton2.Margin = New System.Windows.Forms.Padding(0)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(84, 19)
        Me.RadioButton2.TabIndex = 1
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "全て表示"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'rdbZan
        '
        Me.rdbZan.AutoSize = True
        Me.rdbZan.Checked = True
        Me.rdbZan.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rdbZan.Location = New System.Drawing.Point(24, 1)
        Me.rdbZan.Margin = New System.Windows.Forms.Padding(0)
        Me.rdbZan.Name = "rdbZan"
        Me.rdbZan.Size = New System.Drawing.Size(170, 19)
        Me.rdbZan.TabIndex = 0
        Me.rdbZan.TabStop = True
        Me.rdbZan.Text = "委託残数ありのみ表示"
        Me.rdbZan.UseVisualStyleBackColor = True
        '
        'dgvList
        '
        Me.dgvList.AllowUserToAddRows = False
        Me.dgvList.AllowUserToDeleteRows = False
        Me.dgvList.AllowUserToResizeColumns = False
        Me.dgvList.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvList.ColumnHeadersHeight = 25
        Me.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.着日, Me.伝票番号, Me.出荷先名, Me.商品名, Me.委託数量, Me.売上数量, Me.目切数量, Me.委託残数, Me.伝番})
        Me.dgvList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvList.Location = New System.Drawing.Point(3, 39)
        Me.dgvList.MultiSelect = False
        Me.dgvList.Name = "dgvList"
        Me.dgvList.RowHeadersVisible = False
        Me.dgvList.RowTemplate.Height = 21
        Me.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvList.Size = New System.Drawing.Size(1029, 300)
        Me.dgvList.TabIndex = 6
        '
        '着日
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.着日.DefaultCellStyle = DataGridViewCellStyle2
        Me.着日.HeaderText = "着日"
        Me.着日.Name = "着日"
        '
        '伝票番号
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.伝票番号.DefaultCellStyle = DataGridViewCellStyle3
        Me.伝票番号.HeaderText = "伝番-行番"
        Me.伝票番号.Name = "伝票番号"
        '
        '出荷先名
        '
        DataGridViewCellStyle4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.出荷先名.DefaultCellStyle = DataGridViewCellStyle4
        Me.出荷先名.HeaderText = "出荷先名"
        Me.出荷先名.Name = "出荷先名"
        Me.出荷先名.Width = 197
        '
        '商品名
        '
        DataGridViewCellStyle5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.商品名.DefaultCellStyle = DataGridViewCellStyle5
        Me.商品名.HeaderText = "商品名"
        Me.商品名.Name = "商品名"
        Me.商品名.Width = 212
        '
        '委託数量
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        DataGridViewCellStyle6.Format = "#,##0"
        Me.委託数量.DefaultCellStyle = DataGridViewCellStyle6
        Me.委託数量.HeaderText = "委託数量"
        Me.委託数量.Name = "委託数量"
        '
        '売上数量
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.売上数量.DefaultCellStyle = DataGridViewCellStyle7
        Me.売上数量.HeaderText = "売上数量"
        Me.売上数量.Name = "売上数量"
        '
        '目切数量
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.目切数量.DefaultCellStyle = DataGridViewCellStyle8
        Me.目切数量.HeaderText = "目切数量"
        Me.目切数量.Name = "目切数量"
        '
        '委託残数
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.委託残数.DefaultCellStyle = DataGridViewCellStyle9
        Me.委託残数.HeaderText = "委託残数"
        Me.委託残数.Name = "委託残数"
        '
        '伝番
        '
        DataGridViewCellStyle10.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
        Me.伝番.DefaultCellStyle = DataGridViewCellStyle10
        Me.伝番.HeaderText = "伝番"
        Me.伝番.Name = "伝番"
        Me.伝番.Visible = False
        '
        'Label46
        '
        Me.Label46.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label46.AutoSize = True
        Me.Label46.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label46.Location = New System.Drawing.Point(1015, 18)
        Me.Label46.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(17, 15)
        Me.Label46.TabIndex = 3
        Me.Label46.Text = "件"
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
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.049!))
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.15789!))
        Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.57388!))
        Me.TableLayoutPanel27.Size = New System.Drawing.Size(1041, 551)
        Me.TableLayoutPanel27.TabIndex = 0
        '
        'TableLayoutPanel33
        '
        Me.TableLayoutPanel33.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel33.ColumnCount = 3
        Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.07454!))
        Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.92546!))
        Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 54.0!))
        Me.TableLayoutPanel33.Controls.Add(Me.cmdBack, 0, 1)
        Me.TableLayoutPanel33.Controls.Add(Me.btnSelect, 0, 1)
        Me.TableLayoutPanel33.Location = New System.Drawing.Point(716, 489)
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
        Me.cmdBack.Location = New System.Drawing.Point(162, 20)
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
        Me.btnSelect.Location = New System.Drawing.Point(50, 20)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(102, 36)
        Me.btnSelect.TabIndex = 4
        Me.btnSelect.Text = "選択(&G)"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel32
        '
        Me.TableLayoutPanel32.ColumnCount = 1
        Me.TableLayoutPanel32.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel32.Controls.Add(Me.TableLayoutPanel34, 0, 0)
        Me.TableLayoutPanel32.Controls.Add(Me.dgvList, 0, 1)
        Me.TableLayoutPanel32.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel32.Location = New System.Drawing.Point(3, 130)
        Me.TableLayoutPanel32.Name = "TableLayoutPanel32"
        Me.TableLayoutPanel32.RowCount = 2
        Me.TableLayoutPanel32.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.81081!))
        Me.TableLayoutPanel32.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.18919!))
        Me.TableLayoutPanel32.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel32.Size = New System.Drawing.Size(1035, 342)
        Me.TableLayoutPanel32.TabIndex = 1
        '
        'TableLayoutPanel34
        '
        Me.TableLayoutPanel34.ColumnCount = 3
        Me.TableLayoutPanel34.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel34.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.63768!))
        Me.TableLayoutPanel34.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.125604!))
        Me.TableLayoutPanel34.Controls.Add(Me.Label46, 2, 0)
        Me.TableLayoutPanel34.Controls.Add(Me.lblListCount, 1, 0)
        Me.TableLayoutPanel34.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel34.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel34.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel34.Name = "TableLayoutPanel34"
        Me.TableLayoutPanel34.RowCount = 1
        Me.TableLayoutPanel34.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel34.Size = New System.Drawing.Size(1035, 36)
        Me.TableLayoutPanel34.TabIndex = 5
        '
        'frmH03F10_SelectItakuList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1041, 551)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel27)
        Me.Name = "frmH03F10_SelectItakuList"
        Me.Text = "委託注文一覧(H03F10)"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel9.ResumeLayout(False)
        Me.TableLayoutPanel9.PerformLayout()
        Me.TableLayoutPanel8.ResumeLayout(False)
        Me.TableLayoutPanel8.PerformLayout()
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.TableLayoutPanel7.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel27.ResumeLayout(False)
        Me.TableLayoutPanel33.ResumeLayout(False)
        Me.TableLayoutPanel32.ResumeLayout(False)
        Me.TableLayoutPanel34.ResumeLayout(False)
        Me.TableLayoutPanel34.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lblListCount As Label
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Label38 As Label
    Friend WithEvents Button5 As Button
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents Label4 As Label
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents dgvList As DataGridView
    Friend WithEvents Label46 As Label
    Friend WithEvents TableLayoutPanel27 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel33 As TableLayoutPanel
    Friend WithEvents cmdBack As Button
    Friend WithEvents btnSelect As Button
    Friend WithEvents TableLayoutPanel32 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel34 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel6 As TableLayoutPanel
    Friend WithEvents Label9 As Label
    Friend WithEvents Label39 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents TableLayoutPanel9 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel8 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel7 As TableLayoutPanel
    Friend WithEvents txtSyukkaCD As TextBox
    Friend WithEvents lblSyukkaNM As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents txtShohinNM As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents txtSyukkaNM As TextBox
    Friend WithEvents RadioButton2 As RadioButton
    Friend WithEvents rdbZan As RadioButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents lblDenpyoNoTopTo As Label
    Friend WithEvents txtDenpyoNoTo As TextBox
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents lblDenpyoNoTopFrom As Label
    Friend WithEvents txtDenpyoNoFrom As TextBox
    Friend WithEvents dtpSyukkaDtFrom As CustomControl.NullableDateTimePicker
    Friend WithEvents dtpSyukkaDtTo As CustomControl.NullableDateTimePicker
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents 着日 As DataGridViewTextBoxColumn
    Friend WithEvents 伝票番号 As DataGridViewTextBoxColumn
    Friend WithEvents 出荷先名 As DataGridViewTextBoxColumn
    Friend WithEvents 商品名 As DataGridViewTextBoxColumn
    Friend WithEvents 委託数量 As DataGridViewTextBoxColumn
    Friend WithEvents 売上数量 As DataGridViewTextBoxColumn
    Friend WithEvents 目切数量 As DataGridViewTextBoxColumn
    Friend WithEvents 委託残数 As DataGridViewTextBoxColumn
    Friend WithEvents 伝番 As DataGridViewTextBoxColumn
    Friend WithEvents dtpChakuDtFrom As CustomControl.NullableDateTimePicker
    Friend WithEvents dtpChakuDtTo As CustomControl.NullableDateTimePicker
End Class
