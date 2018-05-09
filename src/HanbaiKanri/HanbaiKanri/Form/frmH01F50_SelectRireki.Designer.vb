<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmH01F50_SelectRireki
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
    '<System.Diagnostics.DebuggerStepThrough()> _
    'Private Sub InitializeComponent()
    '    Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
    '    Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
    '    Me.Label48 = New System.Windows.Forms.Label()
    '    Me.Label47 = New System.Windows.Forms.Label()
    '    Me.Label44 = New System.Windows.Forms.Label()
    '    Me.TableLayoutPanel31 = New System.Windows.Forms.TableLayoutPanel()
    '    Me.Label43 = New System.Windows.Forms.Label()
    '    Me.Label38 = New System.Windows.Forms.Label()
    '    Me.TableLayoutPanel29 = New System.Windows.Forms.TableLayoutPanel()
    '    Me.TableLayoutPanel30 = New System.Windows.Forms.TableLayoutPanel()
    '    Me.txtSyukkaCd = New System.Windows.Forms.TextBox()
    '    Me.Label39 = New System.Windows.Forms.Label()
    '    Me.Label40 = New System.Windows.Forms.Label()
    '    Me.Label41 = New System.Windows.Forms.Label()
    '    Me.Label42 = New System.Windows.Forms.Label()
    '    Me.txtAddress = New System.Windows.Forms.TextBox()
    '    Me.txtTel = New System.Windows.Forms.TextBox()
    '    Me.txtSyukkasakiName = New System.Windows.Forms.TextBox()
    '    Me.cmdBack = New System.Windows.Forms.Button()
    '    Me.btnSelect = New System.Windows.Forms.Button()
    '    Me.TableLayoutPanel33 = New System.Windows.Forms.TableLayoutPanel()
    '    Me.TableLayoutPanel27 = New System.Windows.Forms.TableLayoutPanel()
    '    Me.TableLayoutPanel32 = New System.Windows.Forms.TableLayoutPanel()
    '    Me.TableLayoutPanel34 = New System.Windows.Forms.TableLayoutPanel()
    '    Me.Label45 = New System.Windows.Forms.Label()
    '    Me.Label46 = New System.Windows.Forms.Label()
    '    Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
    '    Me.Label3 = New System.Windows.Forms.Label()
    '    Me.Label2 = New System.Windows.Forms.Label()
    '    Me.TableLayoutPanel10 = New System.Windows.Forms.TableLayoutPanel()
    '    Me.TableLayoutPanel11 = New System.Windows.Forms.TableLayoutPanel()
    '    Me.RadioButton2 = New System.Windows.Forms.RadioButton()
    '    Me.RadioButton1 = New System.Windows.Forms.RadioButton()
    '    Me.dgvList = New System.Windows.Forms.DataGridView()
    '    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
    '    Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
    '    Me.TableLayoutPanel9 = New System.Windows.Forms.TableLayoutPanel()
    '    Me.txtSyohinName = New System.Windows.Forms.TextBox()
    '    Me.Label13 = New System.Windows.Forms.Label()
    '    Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel()
    '    Me.txtUnsoubin = New System.Windows.Forms.TextBox()
    '    Me.Label12 = New System.Windows.Forms.Label()
    '    Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel()
    '    Me.txtDenpyoNoTo = New System.Windows.Forms.TextBox()
    '    Me.txtDenpyoNoFrom = New System.Windows.Forms.TextBox()
    '    Me.Label10 = New System.Windows.Forms.Label()
    '    Me.Label11 = New System.Windows.Forms.Label()
    '    Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
    '    Me.txtSyukkaDateTo = New System.Windows.Forms.TextBox()
    '    Me.txtSyukkaDateFrom = New System.Windows.Forms.TextBox()
    '    Me.Label6 = New System.Windows.Forms.Label()
    '    Me.Label9 = New System.Windows.Forms.Label()
    '    Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
    '    Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
    '    Me.Label1 = New System.Windows.Forms.Label()
    '    Me.Button5 = New System.Windows.Forms.Button()
    '    Me.出荷日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    '    Me.伝票番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    '    Me.出荷先CD = New System.Windows.Forms.DataGridViewTextBoxColumn()
    '    Me.出荷先名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    '    Me.住所 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    '    Me.電話番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    '    Me.運送便 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    '    Me.商品名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
    '    Me.TableLayoutPanel31.SuspendLayout()
    '    Me.TableLayoutPanel29.SuspendLayout()
    '    Me.TableLayoutPanel30.SuspendLayout()
    '    Me.TableLayoutPanel33.SuspendLayout()
    '    Me.TableLayoutPanel27.SuspendLayout()
    '    Me.TableLayoutPanel32.SuspendLayout()
    '    Me.TableLayoutPanel34.SuspendLayout()
    '    Me.TableLayoutPanel6.SuspendLayout()
    '    Me.TableLayoutPanel10.SuspendLayout()
    '    Me.TableLayoutPanel11.SuspendLayout()
    '    CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
    '    Me.TableLayoutPanel1.SuspendLayout()
    '    Me.TableLayoutPanel2.SuspendLayout()
    '    Me.TableLayoutPanel9.SuspendLayout()
    '    Me.TableLayoutPanel8.SuspendLayout()
    '    Me.TableLayoutPanel7.SuspendLayout()
    '    Me.TableLayoutPanel4.SuspendLayout()
    '    Me.TableLayoutPanel3.SuspendLayout()
    '    Me.TableLayoutPanel5.SuspendLayout()
    '    Me.SuspendLayout()
    '    '
    '    'Label48
    '    '
    '    Me.Label48.Anchor = System.Windows.Forms.AnchorStyles.Left
    '    Me.Label48.AutoSize = True
    '    Me.Label48.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '    Me.Label48.Location = New System.Drawing.Point(3, 67)
    '    Me.Label48.Name = "Label48"
    '    Me.Label48.Size = New System.Drawing.Size(113, 15)
    '    Me.Label48.TabIndex = 7
    '    Me.Label48.Text = "（前方一致検索）"
    '    '
    '    'Label47
    '    '
    '    Me.Label47.Anchor = System.Windows.Forms.AnchorStyles.Left
    '    Me.Label47.AutoSize = True
    '    Me.Label47.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '    Me.Label47.Location = New System.Drawing.Point(3, 3)
    '    Me.Label47.Name = "Label47"
    '    Me.Label47.Size = New System.Drawing.Size(113, 15)
    '    Me.Label47.TabIndex = 4
    '    Me.Label47.Text = "（一部一致検索）"
    '    '
    '    'Label44
    '    '
    '    Me.Label44.Anchor = System.Windows.Forms.AnchorStyles.Left
    '    Me.Label44.AutoSize = True
    '    Me.Label44.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '    Me.Label44.Location = New System.Drawing.Point(3, 45)
    '    Me.Label44.Name = "Label44"
    '    Me.Label44.Size = New System.Drawing.Size(113, 15)
    '    Me.Label44.TabIndex = 6
    '    Me.Label44.Text = "（一部一致検索）"
    '    '
    '    'TableLayoutPanel31
    '    '
    '    Me.TableLayoutPanel31.Anchor = System.Windows.Forms.AnchorStyles.Left
    '    Me.TableLayoutPanel31.ColumnCount = 1
    '    Me.TableLayoutPanel31.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.11765!))
    '    Me.TableLayoutPanel31.Controls.Add(Me.Label48, 0, 3)
    '    Me.TableLayoutPanel31.Controls.Add(Me.Label47, 0, 0)
    '    Me.TableLayoutPanel31.Controls.Add(Me.Label43, 0, 1)
    '    Me.TableLayoutPanel31.Controls.Add(Me.Label44, 0, 2)
    '    Me.TableLayoutPanel31.Location = New System.Drawing.Point(444, 2)
    '    Me.TableLayoutPanel31.Margin = New System.Windows.Forms.Padding(0)
    '    Me.TableLayoutPanel31.Name = "TableLayoutPanel31"
    '    Me.TableLayoutPanel31.RowCount = 4
    '    Me.TableLayoutPanel31.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
    '    Me.TableLayoutPanel31.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
    '    Me.TableLayoutPanel31.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
    '    Me.TableLayoutPanel31.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
    '    Me.TableLayoutPanel31.Size = New System.Drawing.Size(122, 87)
    '    Me.TableLayoutPanel31.TabIndex = 1
    '    '
    '    'Label43
    '    '
    '    Me.Label43.Anchor = System.Windows.Forms.AnchorStyles.Left
    '    Me.Label43.AutoSize = True
    '    Me.Label43.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '    Me.Label43.Location = New System.Drawing.Point(3, 24)
    '    Me.Label43.Name = "Label43"
    '    Me.Label43.Size = New System.Drawing.Size(113, 15)
    '    Me.Label43.TabIndex = 5
    '    Me.Label43.Text = "（一部一致検索）"
    '    '
    '    'Label38
    '    '
    '    Me.Label38.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    '    Me.Label38.AutoSize = True
    '    Me.Label38.Location = New System.Drawing.Point(3, 16)
    '    Me.Label38.Name = "Label38"
    '    Me.Label38.Size = New System.Drawing.Size(65, 12)
    '    Me.Label38.TabIndex = 0
    '    Me.Label38.Text = "■抽出条件"
    '    '
    '    'TableLayoutPanel29
    '    '
    '    Me.TableLayoutPanel29.ColumnCount = 2
    '    Me.TableLayoutPanel29.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.49462!))
    '    Me.TableLayoutPanel29.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.50538!))
    '    Me.TableLayoutPanel29.Controls.Add(Me.TableLayoutPanel31, 1, 0)
    '    Me.TableLayoutPanel29.Controls.Add(Me.TableLayoutPanel30, 0, 0)
    '    Me.TableLayoutPanel29.Dock = System.Windows.Forms.DockStyle.Fill
    '    Me.TableLayoutPanel29.Location = New System.Drawing.Point(0, 28)
    '    Me.TableLayoutPanel29.Margin = New System.Windows.Forms.Padding(0)
    '    Me.TableLayoutPanel29.Name = "TableLayoutPanel29"
    '    Me.TableLayoutPanel29.RowCount = 1
    '    Me.TableLayoutPanel29.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    '    Me.TableLayoutPanel29.Size = New System.Drawing.Size(566, 92)
    '    Me.TableLayoutPanel29.TabIndex = 1
    '    '
    '    'TableLayoutPanel30
    '    '
    '    Me.TableLayoutPanel30.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
    '        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '    Me.TableLayoutPanel30.ColumnCount = 2
    '    Me.TableLayoutPanel30.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.39468!))
    '    Me.TableLayoutPanel30.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.60532!))
    '    Me.TableLayoutPanel30.Controls.Add(Me.txtSyukkaCd, 1, 3)
    '    Me.TableLayoutPanel30.Controls.Add(Me.Label39, 0, 0)
    '    Me.TableLayoutPanel30.Controls.Add(Me.Label40, 0, 1)
    '    Me.TableLayoutPanel30.Controls.Add(Me.Label41, 0, 2)
    '    Me.TableLayoutPanel30.Controls.Add(Me.Label42, 0, 3)
    '    Me.TableLayoutPanel30.Controls.Add(Me.txtAddress, 1, 1)
    '    Me.TableLayoutPanel30.Controls.Add(Me.txtTel, 1, 2)
    '    Me.TableLayoutPanel30.Controls.Add(Me.txtSyukkasakiName, 1, 0)
    '    Me.TableLayoutPanel30.Location = New System.Drawing.Point(0, 0)
    '    Me.TableLayoutPanel30.Margin = New System.Windows.Forms.Padding(0)
    '    Me.TableLayoutPanel30.Name = "TableLayoutPanel30"
    '    Me.TableLayoutPanel30.RowCount = 4
    '    Me.TableLayoutPanel30.RowStyles.Add(New System.Windows.Forms.RowStyle())
    '    Me.TableLayoutPanel30.RowStyles.Add(New System.Windows.Forms.RowStyle())
    '    Me.TableLayoutPanel30.RowStyles.Add(New System.Windows.Forms.RowStyle())
    '    Me.TableLayoutPanel30.RowStyles.Add(New System.Windows.Forms.RowStyle())
    '    Me.TableLayoutPanel30.Size = New System.Drawing.Size(444, 89)
    '    Me.TableLayoutPanel30.TabIndex = 0
    '    '
    '    'txtSyukkaCd
    '    '
    '    Me.txtSyukkaCd.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '    Me.txtSyukkaCd.Location = New System.Drawing.Point(99, 66)
    '    Me.txtSyukkaCd.Margin = New System.Windows.Forms.Padding(0)
    '    Me.txtSyukkaCd.Name = "txtSyukkaCd"
    '    Me.txtSyukkaCd.Size = New System.Drawing.Size(120, 22)
    '    Me.txtSyukkaCd.TabIndex = 4
    '    '
    '    'Label39
    '    '
    '    Me.Label39.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '    Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
    '    Me.Label39.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    '    Me.Label39.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '    Me.Label39.Location = New System.Drawing.Point(0, 0)
    '    Me.Label39.Margin = New System.Windows.Forms.Padding(0)
    '    Me.Label39.Name = "Label39"
    '    Me.Label39.Size = New System.Drawing.Size(99, 22)
    '    Me.Label39.TabIndex = 0
    '    Me.Label39.Text = "出荷先名"
    '    Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '    '
    '    'Label40
    '    '
    '    Me.Label40.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '    Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
    '    Me.Label40.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    '    Me.Label40.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '    Me.Label40.Location = New System.Drawing.Point(0, 22)
    '    Me.Label40.Margin = New System.Windows.Forms.Padding(0)
    '    Me.Label40.Name = "Label40"
    '    Me.Label40.Size = New System.Drawing.Size(99, 22)
    '    Me.Label40.TabIndex = 1
    '    Me.Label40.Text = "住所"
    '    Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '    '
    '    'Label41
    '    '
    '    Me.Label41.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '    Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
    '    Me.Label41.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    '    Me.Label41.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '    Me.Label41.Location = New System.Drawing.Point(0, 44)
    '    Me.Label41.Margin = New System.Windows.Forms.Padding(0)
    '    Me.Label41.Name = "Label41"
    '    Me.Label41.Size = New System.Drawing.Size(99, 22)
    '    Me.Label41.TabIndex = 2
    '    Me.Label41.Text = "電話番号"
    '    Me.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '    '
    '    'Label42
    '    '
    '    Me.Label42.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '    Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
    '    Me.Label42.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    '    Me.Label42.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '    Me.Label42.Location = New System.Drawing.Point(0, 66)
    '    Me.Label42.Margin = New System.Windows.Forms.Padding(0)
    '    Me.Label42.Name = "Label42"
    '    Me.Label42.Size = New System.Drawing.Size(99, 22)
    '    Me.Label42.TabIndex = 3
    '    Me.Label42.Text = "出荷先CD"
    '    Me.Label42.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '    '
    '    'txtAddress
    '    '
    '    Me.txtAddress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
    '        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '    Me.txtAddress.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '    Me.txtAddress.Location = New System.Drawing.Point(99, 22)
    '    Me.txtAddress.Margin = New System.Windows.Forms.Padding(0)
    '    Me.txtAddress.Name = "txtAddress"
    '    Me.txtAddress.Size = New System.Drawing.Size(345, 22)
    '    Me.txtAddress.TabIndex = 2
    '    '
    '    'txtTel
    '    '
    '    Me.txtTel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    '    Me.txtTel.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '    Me.txtTel.Location = New System.Drawing.Point(99, 44)
    '    Me.txtTel.Margin = New System.Windows.Forms.Padding(0)
    '    Me.txtTel.Name = "txtTel"
    '    Me.txtTel.Size = New System.Drawing.Size(120, 22)
    '    Me.txtTel.TabIndex = 3
    '    '
    '    'txtSyukkasakiName
    '    '
    '    Me.txtSyukkasakiName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
    '        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '    Me.txtSyukkasakiName.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '    Me.txtSyukkasakiName.Location = New System.Drawing.Point(99, 0)
    '    Me.txtSyukkasakiName.Margin = New System.Windows.Forms.Padding(0)
    '    Me.txtSyukkasakiName.Name = "txtSyukkasakiName"
    '    Me.txtSyukkasakiName.Size = New System.Drawing.Size(345, 22)
    '    Me.txtSyukkasakiName.TabIndex = 1
    '    Me.txtSyukkasakiName.Text = "横手"
    '    '
    '    'cmdBack
    '    '
    '    Me.cmdBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '    Me.cmdBack.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '    Me.cmdBack.Location = New System.Drawing.Point(217, 20)
    '    Me.cmdBack.Name = "cmdBack"
    '    Me.cmdBack.Size = New System.Drawing.Size(102, 36)
    '    Me.cmdBack.TabIndex = 5
    '    Me.cmdBack.Text = "戻る(&B)"
    '    Me.cmdBack.UseVisualStyleBackColor = True
    '    '
    '    'btnSelect
    '    '
    '    Me.btnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '    Me.btnSelect.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '    Me.btnSelect.Location = New System.Drawing.Point(82, 20)
    '    Me.btnSelect.Name = "btnSelect"
    '    Me.btnSelect.Size = New System.Drawing.Size(102, 36)
    '    Me.btnSelect.TabIndex = 4
    '    Me.btnSelect.Text = "選択(&G)"
    '    Me.btnSelect.UseVisualStyleBackColor = True
    '    '
    '    'TableLayoutPanel33
    '    '
    '    Me.TableLayoutPanel33.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '    Me.TableLayoutPanel33.ColumnCount = 2
    '    Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.07454!))
    '    Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.92546!))
    '    Me.TableLayoutPanel33.Controls.Add(Me.cmdBack, 0, 1)
    '    Me.TableLayoutPanel33.Controls.Add(Me.btnSelect, 0, 1)
    '    Me.TableLayoutPanel33.Location = New System.Drawing.Point(860, 516)
    '    Me.TableLayoutPanel33.Name = "TableLayoutPanel33"
    '    Me.TableLayoutPanel33.RowCount = 2
    '    Me.TableLayoutPanel33.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.25424!))
    '    Me.TableLayoutPanel33.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84.74577!))
    '    Me.TableLayoutPanel33.Size = New System.Drawing.Size(322, 59)
    '    Me.TableLayoutPanel33.TabIndex = 5
    '    '
    '    'TableLayoutPanel27
    '    '
    '    Me.TableLayoutPanel27.ColumnCount = 1
    '    Me.TableLayoutPanel27.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    '    Me.TableLayoutPanel27.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
    '    Me.TableLayoutPanel27.Controls.Add(Me.TableLayoutPanel33, 0, 2)
    '    Me.TableLayoutPanel27.Controls.Add(Me.TableLayoutPanel32, 0, 1)
    '    Me.TableLayoutPanel27.Controls.Add(Me.TableLayoutPanel1, 0, 0)
    '    Me.TableLayoutPanel27.Dock = System.Windows.Forms.DockStyle.Fill
    '    Me.TableLayoutPanel27.Location = New System.Drawing.Point(0, 0)
    '    Me.TableLayoutPanel27.Name = "TableLayoutPanel27"
    '    Me.TableLayoutPanel27.RowCount = 3
    '    Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.82131!))
    '    Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 64.60481!))
    '    Me.TableLayoutPanel27.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.57388!))
    '    Me.TableLayoutPanel27.Size = New System.Drawing.Size(1185, 578)
    '    Me.TableLayoutPanel27.TabIndex = 2
    '    '
    '    'TableLayoutPanel32
    '    '
    '    Me.TableLayoutPanel32.ColumnCount = 1
    '    Me.TableLayoutPanel32.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    '    Me.TableLayoutPanel32.Controls.Add(Me.TableLayoutPanel34, 0, 0)
    '    Me.TableLayoutPanel32.Controls.Add(Me.dgvList, 0, 1)
    '    Me.TableLayoutPanel32.Dock = System.Windows.Forms.DockStyle.Fill
    '    Me.TableLayoutPanel32.Location = New System.Drawing.Point(3, 129)
    '    Me.TableLayoutPanel32.Name = "TableLayoutPanel32"
    '    Me.TableLayoutPanel32.RowCount = 2
    '    Me.TableLayoutPanel32.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.81081!))
    '    Me.TableLayoutPanel32.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.18919!))
    '    Me.TableLayoutPanel32.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
    '    Me.TableLayoutPanel32.Size = New System.Drawing.Size(1179, 367)
    '    Me.TableLayoutPanel32.TabIndex = 1
    '    '
    '    'TableLayoutPanel34
    '    '
    '    Me.TableLayoutPanel34.ColumnCount = 3
    '    Me.TableLayoutPanel34.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 79.72858!))
    '    Me.TableLayoutPanel34.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.481764!))
    '    Me.TableLayoutPanel34.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.74636!))
    '    Me.TableLayoutPanel34.Controls.Add(Me.Label45, 2, 0)
    '    Me.TableLayoutPanel34.Controls.Add(Me.Label46, 1, 0)
    '    Me.TableLayoutPanel34.Controls.Add(Me.TableLayoutPanel6, 0, 0)
    '    Me.TableLayoutPanel34.Dock = System.Windows.Forms.DockStyle.Fill
    '    Me.TableLayoutPanel34.Location = New System.Drawing.Point(0, 0)
    '    Me.TableLayoutPanel34.Margin = New System.Windows.Forms.Padding(0)
    '    Me.TableLayoutPanel34.Name = "TableLayoutPanel34"
    '    Me.TableLayoutPanel34.RowCount = 1
    '    Me.TableLayoutPanel34.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    '    Me.TableLayoutPanel34.Size = New System.Drawing.Size(1179, 39)
    '    Me.TableLayoutPanel34.TabIndex = 5
    '    '
    '    'Label45
    '    '
    '    Me.Label45.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '    Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
    '    Me.Label45.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    '    Me.Label45.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '    Me.Label45.Location = New System.Drawing.Point(1092, 17)
    '    Me.Label45.Name = "Label45"
    '    Me.Label45.Size = New System.Drawing.Size(84, 22)
    '    Me.Label45.TabIndex = 5
    '    Me.Label45.Text = "1"
    '    Me.Label45.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '    '
    '    'Label46
    '    '
    '    Me.Label46.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '    Me.Label46.AutoSize = True
    '    Me.Label46.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '    Me.Label46.Location = New System.Drawing.Point(1008, 24)
    '    Me.Label46.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
    '    Me.Label46.Name = "Label46"
    '    Me.Label46.Size = New System.Drawing.Size(29, 12)
    '    Me.Label46.TabIndex = 3
    '    Me.Label46.Text = "件数"
    '    '
    '    'TableLayoutPanel6
    '    '
    '    Me.TableLayoutPanel6.ColumnCount = 3
    '    Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.29885!))
    '    Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.70115!))
    '    Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 474.0!))
    '    Me.TableLayoutPanel6.Controls.Add(Me.Label3, 2, 0)
    '    Me.TableLayoutPanel6.Controls.Add(Me.Label2, 0, 0)
    '    Me.TableLayoutPanel6.Controls.Add(Me.TableLayoutPanel10, 1, 0)
    '    Me.TableLayoutPanel6.Location = New System.Drawing.Point(3, 3)
    '    Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
    '    Me.TableLayoutPanel6.RowCount = 2
    '    Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75.75758!))
    '    Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.24242!))
    '    Me.TableLayoutPanel6.Size = New System.Drawing.Size(822, 33)
    '    Me.TableLayoutPanel6.TabIndex = 6
    '    '
    '    'Label3
    '    '
    '    Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Left
    '    Me.Label3.AutoSize = True
    '    Me.Label3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '    Me.Label3.Location = New System.Drawing.Point(366, 5)
    '    Me.Label3.Margin = New System.Windows.Forms.Padding(20, 0, 3, 0)
    '    Me.Label3.Name = "Label3"
    '    Me.Label3.Size = New System.Drawing.Size(329, 15)
    '    Me.Label3.TabIndex = 8
    '    Me.Label3.Text = "伝票単位の場合、明細項目は先頭行の内容を表示"
    '    Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '    '
    '    'Label2
    '    '
    '    Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Left
    '    Me.Label2.AutoSize = True
    '    Me.Label2.Location = New System.Drawing.Point(3, 6)
    '    Me.Label2.Name = "Label2"
    '    Me.Label2.Size = New System.Drawing.Size(65, 12)
    '    Me.Label2.TabIndex = 1
    '    Me.Label2.Text = "■表示形式"
    '    Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '    '
    '    'TableLayoutPanel10
    '    '
    '    Me.TableLayoutPanel10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
    '        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '    Me.TableLayoutPanel10.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
    '    Me.TableLayoutPanel10.ColumnCount = 1
    '    Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    '    Me.TableLayoutPanel10.Controls.Add(Me.TableLayoutPanel11, 0, 0)
    '    Me.TableLayoutPanel10.Location = New System.Drawing.Point(94, 0)
    '    Me.TableLayoutPanel10.Margin = New System.Windows.Forms.Padding(0)
    '    Me.TableLayoutPanel10.Name = "TableLayoutPanel10"
    '    Me.TableLayoutPanel10.RowCount = 1
    '    Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    '    Me.TableLayoutPanel10.Size = New System.Drawing.Size(252, 25)
    '    Me.TableLayoutPanel10.TabIndex = 2
    '    '
    '    'TableLayoutPanel11
    '    '
    '    Me.TableLayoutPanel11.ColumnCount = 2
    '    Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    '    Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    '    Me.TableLayoutPanel11.Controls.Add(Me.RadioButton2, 1, 0)
    '    Me.TableLayoutPanel11.Controls.Add(Me.RadioButton1, 0, 0)
    '    Me.TableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill
    '    Me.TableLayoutPanel11.Location = New System.Drawing.Point(1, 1)
    '    Me.TableLayoutPanel11.Margin = New System.Windows.Forms.Padding(0)
    '    Me.TableLayoutPanel11.Name = "TableLayoutPanel11"
    '    Me.TableLayoutPanel11.RowCount = 1
    '    Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
    '    Me.TableLayoutPanel11.Size = New System.Drawing.Size(250, 23)
    '    Me.TableLayoutPanel11.TabIndex = 0
    '    '
    '    'RadioButton2
    '    '
    '    Me.RadioButton2.Anchor = System.Windows.Forms.AnchorStyles.None
    '    Me.RadioButton2.AutoSize = True
    '    Me.RadioButton2.Location = New System.Drawing.Point(152, 3)
    '    Me.RadioButton2.Name = "RadioButton2"
    '    Me.RadioButton2.Size = New System.Drawing.Size(71, 16)
    '    Me.RadioButton2.TabIndex = 1
    '    Me.RadioButton2.Text = "明細単位"
    '    Me.RadioButton2.UseVisualStyleBackColor = True
    '    '
    '    'RadioButton1
    '    '
    '    Me.RadioButton1.Anchor = System.Windows.Forms.AnchorStyles.None
    '    Me.RadioButton1.AutoSize = True
    '    Me.RadioButton1.Checked = True
    '    Me.RadioButton1.Location = New System.Drawing.Point(27, 3)
    '    Me.RadioButton1.Name = "RadioButton1"
    '    Me.RadioButton1.Size = New System.Drawing.Size(71, 16)
    '    Me.RadioButton1.TabIndex = 0
    '    Me.RadioButton1.TabStop = True
    '    Me.RadioButton1.Text = "伝票単位"
    '    Me.RadioButton1.UseVisualStyleBackColor = True
    '    '
    '    'dgvList
    '    '
    '    Me.dgvList.AllowUserToAddRows = False
    '    Me.dgvList.AllowUserToDeleteRows = False
    '    Me.dgvList.AllowUserToResizeColumns = False
    '    Me.dgvList.AllowUserToResizeRows = False
    '    DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
    '    DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
    '    DataGridViewCellStyle1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '    DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
    '    DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
    '    DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
    '    DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
    '    Me.dgvList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
    '    Me.dgvList.ColumnHeadersHeight = 40
    '    Me.dgvList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.出荷日, Me.伝票番号, Me.出荷先CD, Me.出荷先名, Me.住所, Me.電話番号, Me.運送便, Me.商品名})
    '    Me.dgvList.Dock = System.Windows.Forms.DockStyle.Fill
    '    Me.dgvList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
    '    Me.dgvList.Location = New System.Drawing.Point(3, 42)
    '    Me.dgvList.MultiSelect = False
    '    Me.dgvList.Name = "dgvList"
    '    Me.dgvList.RowHeadersVisible = False
    '    Me.dgvList.RowTemplate.Height = 21
    '    Me.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    '    Me.dgvList.Size = New System.Drawing.Size(1173, 322)
    '    Me.dgvList.TabIndex = 6
    '    '
    '    'TableLayoutPanel1
    '    '
    '    Me.TableLayoutPanel1.ColumnCount = 3
    '    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.97936!))
    '    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.55288!))
    '    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.38177!))
    '    Me.TableLayoutPanel1.Controls.Add(Me.Label38, 0, 0)
    '    Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel29, 0, 1)
    '    Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 1, 1)
    '    Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 2, 1)
    '    Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
    '    Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
    '    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    '    Me.TableLayoutPanel1.RowCount = 2
    '    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.0!))
    '    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 76.0!))
    '    Me.TableLayoutPanel1.Size = New System.Drawing.Size(1179, 120)
    '    Me.TableLayoutPanel1.TabIndex = 6
    '    '
    '    'TableLayoutPanel2
    '    '
    '    Me.TableLayoutPanel2.ColumnCount = 1
    '    Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    '    Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel9, 0, 3)
    '    Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel8, 0, 2)
    '    Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel7, 0, 1)
    '    Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel4, 0, 0)
    '    Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
    '    Me.TableLayoutPanel2.Location = New System.Drawing.Point(566, 28)
    '    Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
    '    Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
    '    Me.TableLayoutPanel2.RowCount = 4
    '    Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
    '    Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
    '    Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
    '    Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
    '    Me.TableLayoutPanel2.Size = New System.Drawing.Size(466, 92)
    '    Me.TableLayoutPanel2.TabIndex = 6
    '    '
    '    'TableLayoutPanel9
    '    '
    '    Me.TableLayoutPanel9.ColumnCount = 3
    '    Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.82114!))
    '    Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
    '    Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 86.17886!))
    '    Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
    '    Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
    '    Me.TableLayoutPanel9.Controls.Add(Me.txtSyohinName, 2, 0)
    '    Me.TableLayoutPanel9.Controls.Add(Me.Label13, 1, 0)
    '    Me.TableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill
    '    Me.TableLayoutPanel9.Location = New System.Drawing.Point(0, 66)
    '    Me.TableLayoutPanel9.Margin = New System.Windows.Forms.Padding(0)
    '    Me.TableLayoutPanel9.Name = "TableLayoutPanel9"
    '    Me.TableLayoutPanel9.RowCount = 1
    '    Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    '    Me.TableLayoutPanel9.Size = New System.Drawing.Size(466, 26)
    '    Me.TableLayoutPanel9.TabIndex = 10
    '    '
    '    'txtSyohinName
    '    '
    '    Me.txtSyohinName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
    '        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '    Me.txtSyohinName.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '    Me.txtSyohinName.Location = New System.Drawing.Point(148, 0)
    '    Me.txtSyohinName.Margin = New System.Windows.Forms.Padding(0)
    '    Me.txtSyohinName.Name = "txtSyohinName"
    '    Me.txtSyohinName.Size = New System.Drawing.Size(318, 22)
    '    Me.txtSyohinName.TabIndex = 4
    '    '
    '    'Label13
    '    '
    '    Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '    Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
    '    Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    '    Me.Label13.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '    Me.Label13.Location = New System.Drawing.Point(51, 0)
    '    Me.Label13.Margin = New System.Windows.Forms.Padding(0)
    '    Me.Label13.Name = "Label13"
    '    Me.Label13.Size = New System.Drawing.Size(97, 22)
    '    Me.Label13.TabIndex = 3
    '    Me.Label13.Text = "商品名"
    '    Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '    '
    '    'TableLayoutPanel8
    '    '
    '    Me.TableLayoutPanel8.ColumnCount = 5
    '    Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.78928!))
    '    Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
    '    Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.54386!))
    '    Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.52632!))
    '    Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.79904!))
    '    Me.TableLayoutPanel8.Controls.Add(Me.txtUnsoubin, 2, 0)
    '    Me.TableLayoutPanel8.Controls.Add(Me.Label12, 1, 0)
    '    Me.TableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill
    '    Me.TableLayoutPanel8.Location = New System.Drawing.Point(0, 44)
    '    Me.TableLayoutPanel8.Margin = New System.Windows.Forms.Padding(0)
    '    Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
    '    Me.TableLayoutPanel8.RowCount = 1
    '    Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    '    Me.TableLayoutPanel8.Size = New System.Drawing.Size(466, 22)
    '    Me.TableLayoutPanel8.TabIndex = 9
    '    '
    '    'txtUnsoubin
    '    '
    '    Me.txtUnsoubin.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
    '        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '    Me.txtUnsoubin.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '    Me.txtUnsoubin.Location = New System.Drawing.Point(148, 0)
    '    Me.txtUnsoubin.Margin = New System.Windows.Forms.Padding(0)
    '    Me.txtUnsoubin.Name = "txtUnsoubin"
    '    Me.txtUnsoubin.Size = New System.Drawing.Size(139, 22)
    '    Me.txtUnsoubin.TabIndex = 4
    '    '
    '    'Label12
    '    '
    '    Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '    Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
    '    Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    '    Me.Label12.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '    Me.Label12.Location = New System.Drawing.Point(51, 0)
    '    Me.Label12.Margin = New System.Windows.Forms.Padding(0)
    '    Me.Label12.Name = "Label12"
    '    Me.Label12.Size = New System.Drawing.Size(97, 22)
    '    Me.Label12.TabIndex = 3
    '    Me.Label12.Text = "運送便"
    '    Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '    '
    '    'TableLayoutPanel7
    '    '
    '    Me.TableLayoutPanel7.ColumnCount = 5
    '    Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.78928!))
    '    Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
    '    Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.54386!))
    '    Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.52632!))
    '    Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.79904!))
    '    Me.TableLayoutPanel7.Controls.Add(Me.txtDenpyoNoTo, 4, 0)
    '    Me.TableLayoutPanel7.Controls.Add(Me.txtDenpyoNoFrom, 2, 0)
    '    Me.TableLayoutPanel7.Controls.Add(Me.Label10, 1, 0)
    '    Me.TableLayoutPanel7.Controls.Add(Me.Label11, 3, 0)
    '    Me.TableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
    '    Me.TableLayoutPanel7.Location = New System.Drawing.Point(0, 22)
    '    Me.TableLayoutPanel7.Margin = New System.Windows.Forms.Padding(0)
    '    Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
    '    Me.TableLayoutPanel7.RowCount = 1
    '    Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    '    Me.TableLayoutPanel7.Size = New System.Drawing.Size(466, 22)
    '    Me.TableLayoutPanel7.TabIndex = 8
    '    '
    '    'txtDenpyoNoTo
    '    '
    '    Me.txtDenpyoNoTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
    '        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '    Me.txtDenpyoNoTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '    Me.txtDenpyoNoTo.Location = New System.Drawing.Point(325, 0)
    '    Me.txtDenpyoNoTo.Margin = New System.Windows.Forms.Padding(0)
    '    Me.txtDenpyoNoTo.Name = "txtDenpyoNoTo"
    '    Me.txtDenpyoNoTo.Size = New System.Drawing.Size(141, 22)
    '    Me.txtDenpyoNoTo.TabIndex = 5
    '    '
    '    'txtDenpyoNoFrom
    '    '
    '    Me.txtDenpyoNoFrom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
    '        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '    Me.txtDenpyoNoFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '    Me.txtDenpyoNoFrom.Location = New System.Drawing.Point(148, 0)
    '    Me.txtDenpyoNoFrom.Margin = New System.Windows.Forms.Padding(0)
    '    Me.txtDenpyoNoFrom.Name = "txtDenpyoNoFrom"
    '    Me.txtDenpyoNoFrom.Size = New System.Drawing.Size(139, 22)
    '    Me.txtDenpyoNoFrom.TabIndex = 4
    '    '
    '    'Label10
    '    '
    '    Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '    Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
    '    Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    '    Me.Label10.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '    Me.Label10.Location = New System.Drawing.Point(51, 0)
    '    Me.Label10.Margin = New System.Windows.Forms.Padding(0)
    '    Me.Label10.Name = "Label10"
    '    Me.Label10.Size = New System.Drawing.Size(97, 22)
    '    Me.Label10.TabIndex = 3
    '    Me.Label10.Text = "伝票番号"
    '    Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '    '
    '    'Label11
    '    '
    '    Me.Label11.Anchor = System.Windows.Forms.AnchorStyles.None
    '    Me.Label11.AutoSize = True
    '    Me.Label11.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '    Me.Label11.Location = New System.Drawing.Point(295, 3)
    '    Me.Label11.Name = "Label11"
    '    Me.Label11.Size = New System.Drawing.Size(22, 15)
    '    Me.Label11.TabIndex = 7
    '    Me.Label11.Text = "～"
    '    Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '    '
    '    'TableLayoutPanel4
    '    '
    '    Me.TableLayoutPanel4.ColumnCount = 5
    '    Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.78928!))
    '    Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
    '    Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.54386!))
    '    Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.52632!))
    '    Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.79904!))
    '    Me.TableLayoutPanel4.Controls.Add(Me.txtSyukkaDateTo, 4, 0)
    '    Me.TableLayoutPanel4.Controls.Add(Me.txtSyukkaDateFrom, 2, 0)
    '    Me.TableLayoutPanel4.Controls.Add(Me.Label6, 1, 0)
    '    Me.TableLayoutPanel4.Controls.Add(Me.Label9, 3, 0)
    '    Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
    '    Me.TableLayoutPanel4.Location = New System.Drawing.Point(0, 0)
    '    Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(0)
    '    Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
    '    Me.TableLayoutPanel4.RowCount = 1
    '    Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    '    Me.TableLayoutPanel4.Size = New System.Drawing.Size(466, 22)
    '    Me.TableLayoutPanel4.TabIndex = 7
    '    '
    '    'txtSyukkaDateTo
    '    '
    '    Me.txtSyukkaDateTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
    '        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '    Me.txtSyukkaDateTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '    Me.txtSyukkaDateTo.Location = New System.Drawing.Point(325, 0)
    '    Me.txtSyukkaDateTo.Margin = New System.Windows.Forms.Padding(0)
    '    Me.txtSyukkaDateTo.Name = "txtSyukkaDateTo"
    '    Me.txtSyukkaDateTo.Size = New System.Drawing.Size(141, 22)
    '    Me.txtSyukkaDateTo.TabIndex = 5
    '    '
    '    'txtSyukkaDateFrom
    '    '
    '    Me.txtSyukkaDateFrom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
    '        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '    Me.txtSyukkaDateFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '    Me.txtSyukkaDateFrom.Location = New System.Drawing.Point(148, 0)
    '    Me.txtSyukkaDateFrom.Margin = New System.Windows.Forms.Padding(0)
    '    Me.txtSyukkaDateFrom.Name = "txtSyukkaDateFrom"
    '    Me.txtSyukkaDateFrom.Size = New System.Drawing.Size(139, 22)
    '    Me.txtSyukkaDateFrom.TabIndex = 4
    '    '
    '    'Label6
    '    '
    '    Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    '    Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
    '    Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    '    Me.Label6.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '    Me.Label6.Location = New System.Drawing.Point(51, 0)
    '    Me.Label6.Margin = New System.Windows.Forms.Padding(0)
    '    Me.Label6.Name = "Label6"
    '    Me.Label6.Size = New System.Drawing.Size(97, 22)
    '    Me.Label6.TabIndex = 3
    '    Me.Label6.Text = "出荷日"
    '    Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '    '
    '    'Label9
    '    '
    '    Me.Label9.Anchor = System.Windows.Forms.AnchorStyles.None
    '    Me.Label9.AutoSize = True
    '    Me.Label9.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '    Me.Label9.Location = New System.Drawing.Point(295, 3)
    '    Me.Label9.Name = "Label9"
    '    Me.Label9.Size = New System.Drawing.Size(22, 15)
    '    Me.Label9.TabIndex = 7
    '    Me.Label9.Text = "～"
    '    Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '    '
    '    'TableLayoutPanel3
    '    '
    '    Me.TableLayoutPanel3.ColumnCount = 1
    '    Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
    '    Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel5, 0, 1)
    '    Me.TableLayoutPanel3.Controls.Add(Me.Button5, 0, 0)
    '    Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
    '    Me.TableLayoutPanel3.Location = New System.Drawing.Point(1035, 31)
    '    Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
    '    Me.TableLayoutPanel3.RowCount = 2
    '    Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 72.09303!))
    '    Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.90698!))
    '    Me.TableLayoutPanel3.Size = New System.Drawing.Size(141, 86)
    '    Me.TableLayoutPanel3.TabIndex = 7
    '    '
    '    'TableLayoutPanel5
    '    '
    '    Me.TableLayoutPanel5.ColumnCount = 1
    '    Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    '    Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
    '    Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
    '    Me.TableLayoutPanel5.Controls.Add(Me.Label1, 0, 0)
    '    Me.TableLayoutPanel5.Location = New System.Drawing.Point(0, 61)
    '    Me.TableLayoutPanel5.Margin = New System.Windows.Forms.Padding(0)
    '    Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
    '    Me.TableLayoutPanel5.RowCount = 1
    '    Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    '    Me.TableLayoutPanel5.Size = New System.Drawing.Size(466, 22)
    '    Me.TableLayoutPanel5.TabIndex = 11
    '    '
    '    'Label1
    '    '
    '    Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Left
    '    Me.Label1.AutoSize = True
    '    Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '    Me.Label1.Location = New System.Drawing.Point(3, 3)
    '    Me.Label1.Name = "Label1"
    '    Me.Label1.Size = New System.Drawing.Size(113, 15)
    '    Me.Label1.TabIndex = 7
    '    Me.Label1.Text = "（一部一致検索）"
    '    '
    '    'Button5
    '    '
    '    Me.Button5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!)
    '    Me.Button5.Location = New System.Drawing.Point(3, 3)
    '    Me.Button5.Name = "Button5"
    '    Me.Button5.Size = New System.Drawing.Size(102, 36)
    '    Me.Button5.TabIndex = 12
    '    Me.Button5.Text = "検索(&S)"
    '    Me.Button5.UseVisualStyleBackColor = True
    '    '
    '    '出荷日
    '    '
    '    Me.出荷日.HeaderText = "出荷日"
    '    Me.出荷日.Name = "出荷日"
    '    '
    '    '伝票番号
    '    '
    '    Me.伝票番号.HeaderText = "伝票番号"
    '    Me.伝票番号.Name = "伝票番号"
    '    '
    '    '出荷先CD
    '    '
    '    DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
    '    Me.出荷先CD.DefaultCellStyle = DataGridViewCellStyle2
    '    Me.出荷先CD.HeaderText = "出荷先CD"
    '    Me.出荷先CD.Name = "出荷先CD"
    '    '
    '    '出荷先名
    '    '
    '    Me.出荷先名.HeaderText = "出荷先名"
    '    Me.出荷先名.Name = "出荷先名"
    '    Me.出荷先名.Width = 200
    '    '
    '    '住所
    '    '
    '    Me.住所.HeaderText = "住所"
    '    Me.住所.Name = "住所"
    '    Me.住所.Width = 200
    '    '
    '    '電話番号
    '    '
    '    Me.電話番号.HeaderText = "電話番号"
    '    Me.電話番号.Name = "電話番号"
    '    Me.電話番号.Width = 150
    '    '
    '    '運送便
    '    '
    '    Me.運送便.HeaderText = "運送便"
    '    Me.運送便.Name = "運送便"
    '    '
    '    '商品名
    '    '
    '    Me.商品名.HeaderText = "商品名"
    '    Me.商品名.Name = "商品名"
    '    Me.商品名.ReadOnly = True
    '    Me.商品名.Width = 200
    '    '
    '    'frmH01F50_SelectRireki
    '    '
    '    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
    '    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    '    Me.ClientSize = New System.Drawing.Size(1185, 578)
    '    Me.Controls.Add(Me.TableLayoutPanel27)
    '    Me.Name = "frmH01F50_SelectRireki"
    '    Me.Text = "注文選択(H01F50)"
    '    Me.TableLayoutPanel31.ResumeLayout(False)
    '    Me.TableLayoutPanel31.PerformLayout()
    '    Me.TableLayoutPanel29.ResumeLayout(False)
    '    Me.TableLayoutPanel30.ResumeLayout(False)
    '    Me.TableLayoutPanel30.PerformLayout()
    '    Me.TableLayoutPanel33.ResumeLayout(False)
    '    Me.TableLayoutPanel27.ResumeLayout(False)
    '    Me.TableLayoutPanel32.ResumeLayout(False)
    '    Me.TableLayoutPanel34.ResumeLayout(False)
    '    Me.TableLayoutPanel34.PerformLayout()
    '    Me.TableLayoutPanel6.ResumeLayout(False)
    '    Me.TableLayoutPanel6.PerformLayout()
    '    Me.TableLayoutPanel10.ResumeLayout(False)
    '    Me.TableLayoutPanel11.ResumeLayout(False)
    '    Me.TableLayoutPanel11.PerformLayout()
    '    CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
    '    Me.TableLayoutPanel1.ResumeLayout(False)
    '    Me.TableLayoutPanel1.PerformLayout()
    '    Me.TableLayoutPanel2.ResumeLayout(False)
    '    Me.TableLayoutPanel9.ResumeLayout(False)
    '    Me.TableLayoutPanel9.PerformLayout()
    '    Me.TableLayoutPanel8.ResumeLayout(False)
    '    Me.TableLayoutPanel8.PerformLayout()
    '    Me.TableLayoutPanel7.ResumeLayout(False)
    '    Me.TableLayoutPanel7.PerformLayout()
    '    Me.TableLayoutPanel4.ResumeLayout(False)
    '    Me.TableLayoutPanel4.PerformLayout()
    '    Me.TableLayoutPanel3.ResumeLayout(False)
    '    Me.TableLayoutPanel5.ResumeLayout(False)
    '    Me.TableLayoutPanel5.PerformLayout()
    '    Me.ResumeLayout(False)

    'End Sub

    Friend WithEvents Label48 As Label
    Friend WithEvents Label47 As Label
    Friend WithEvents Label44 As Label
    Friend WithEvents TableLayoutPanel31 As TableLayoutPanel
    Friend WithEvents Label43 As Label
    Friend WithEvents Label38 As Label
    Friend WithEvents TableLayoutPanel29 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel30 As TableLayoutPanel
    Friend WithEvents txtSyukkaCd As TextBox
    Friend WithEvents Label39 As Label
    Friend WithEvents Label40 As Label
    Friend WithEvents Label41 As Label
    Friend WithEvents Label42 As Label
    Friend WithEvents txtSyukkasakiName As TextBox
    Friend WithEvents txtAddress As TextBox
    Friend WithEvents txtTel As TextBox
    Friend WithEvents cmdBack As Button
    Friend WithEvents btnSelect As Button
    Friend WithEvents TableLayoutPanel33 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel27 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel32 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel34 As TableLayoutPanel
    Friend WithEvents lblListCount As Label
    Friend WithEvents Label46 As Label
    Friend WithEvents dgvList As DataGridView
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel9 As TableLayoutPanel
    Friend WithEvents txtSyohinName As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents TableLayoutPanel8 As TableLayoutPanel
    Friend WithEvents txtUnsoubin As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents TableLayoutPanel7 As TableLayoutPanel
    Friend WithEvents txtDenpyoNoTo As TextBox
    Friend WithEvents txtDenpyoNoFrom As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents Label6 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents Label1 As Label
    Friend WithEvents btnSearch As Button
    Friend WithEvents TableLayoutPanel6 As TableLayoutPanel
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TableLayoutPanel11 As TableLayoutPanel
    Friend WithEvents rdbMeisai As RadioButton
    Friend WithEvents rdbDenpyo As RadioButton
    Friend WithEvents TableLayoutPanel12 As TableLayoutPanel
    Friend WithEvents chkTorikesi As CheckBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents dtpSyukkaDateFrom As DateTimePicker
    Friend WithEvents dtpSyukkaDateTo As DateTimePicker
    Friend WithEvents lblMode As Label
    Friend WithEvents 出荷日 As DataGridViewTextBoxColumn
    Friend WithEvents 伝票番号 As DataGridViewTextBoxColumn
    Friend WithEvents 出荷先CD As DataGridViewTextBoxColumn
    Friend WithEvents 出荷先名 As DataGridViewTextBoxColumn
    Friend WithEvents 住所 As DataGridViewTextBoxColumn
    Friend WithEvents 電話番号 As DataGridViewTextBoxColumn
    Friend WithEvents 運送便 As DataGridViewTextBoxColumn
    Friend WithEvents 商品名 As DataGridViewTextBoxColumn
End Class
