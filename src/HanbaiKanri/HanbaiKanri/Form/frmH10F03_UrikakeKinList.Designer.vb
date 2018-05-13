<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmH10F03_UrikakeKinList
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
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TableLayoutPanel33 = New System.Windows.Forms.TableLayoutPanel()
        Me.cmdPreview = New System.Windows.Forms.Button()
        Me.cmdBack = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.gbBunrui = New System.Windows.Forms.GroupBox()
        Me.rbUriage = New System.Windows.Forms.RadioButton()
        Me.rbItaku = New System.Windows.Forms.RadioButton()
        Me.rbAll = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbSortKana = New System.Windows.Forms.RadioButton()
        Me.rbSortCode = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dgvKouho = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdAllRight = New System.Windows.Forms.Button()
        Me.cmdSelectRight = New System.Windows.Forms.Button()
        Me.cmdSelectLeft = New System.Windows.Forms.Button()
        Me.cmdAllLeft = New System.Windows.Forms.Button()
        Me.dgvTaisho = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtNyukinYm = New CustomControl.TextBoxDate()
        Me.TableLayoutPanel33.SuspendLayout()
        Me.gbBunrui.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvKouho, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvTaisho, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel33
        '
        Me.TableLayoutPanel33.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel33.ColumnCount = 3
        Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.97701!))
        Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.02299!))
        Me.TableLayoutPanel33.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 143.0!))
        Me.TableLayoutPanel33.Controls.Add(Me.cmdPreview, 0, 0)
        Me.TableLayoutPanel33.Controls.Add(Me.cmdBack, 2, 0)
        Me.TableLayoutPanel33.Controls.Add(Me.cmdPrint, 1, 0)
        Me.TableLayoutPanel33.Location = New System.Drawing.Point(454, 507)
        Me.TableLayoutPanel33.Name = "TableLayoutPanel33"
        Me.TableLayoutPanel33.RowCount = 1
        Me.TableLayoutPanel33.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel33.Size = New System.Drawing.Size(418, 42)
        Me.TableLayoutPanel33.TabIndex = 30
        '
        'cmdPreview
        '
        Me.cmdPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdPreview.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdPreview.Location = New System.Drawing.Point(3, 3)
        Me.cmdPreview.Name = "cmdPreview"
        Me.cmdPreview.Size = New System.Drawing.Size(120, 36)
        Me.cmdPreview.TabIndex = 1
        Me.cmdPreview.Text = "プレビュー(&P)"
        Me.cmdPreview.UseVisualStyleBackColor = True
        '
        'cmdBack
        '
        Me.cmdBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdBack.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdBack.Location = New System.Drawing.Point(295, 3)
        Me.cmdBack.Name = "cmdBack"
        Me.cmdBack.Size = New System.Drawing.Size(120, 36)
        Me.cmdBack.TabIndex = 3
        Me.cmdBack.Text = "戻る(&B)"
        Me.cmdBack.UseVisualStyleBackColor = True
        '
        'cmdPrint
        '
        Me.cmdPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdPrint.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdPrint.Location = New System.Drawing.Point(151, 3)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.Size = New System.Drawing.Size(120, 36)
        Me.cmdPrint.TabIndex = 2
        Me.cmdPrint.Text = "印刷(&D)"
        Me.cmdPrint.UseVisualStyleBackColor = True
        '
        'gbBunrui
        '
        Me.gbBunrui.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.gbBunrui.Controls.Add(Me.rbUriage)
        Me.gbBunrui.Controls.Add(Me.rbItaku)
        Me.gbBunrui.Controls.Add(Me.rbAll)
        Me.gbBunrui.Location = New System.Drawing.Point(23, 5)
        Me.gbBunrui.Margin = New System.Windows.Forms.Padding(0)
        Me.gbBunrui.Name = "gbBunrui"
        Me.gbBunrui.Padding = New System.Windows.Forms.Padding(0)
        Me.gbBunrui.Size = New System.Drawing.Size(351, 40)
        Me.gbBunrui.TabIndex = 40
        Me.gbBunrui.TabStop = False
        '
        'rbUriage
        '
        Me.rbUriage.AutoSize = True
        Me.rbUriage.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rbUriage.Location = New System.Drawing.Point(240, 13)
        Me.rbUriage.Name = "rbUriage"
        Me.rbUriage.Size = New System.Drawing.Size(55, 19)
        Me.rbUriage.TabIndex = 3
        Me.rbUriage.TabStop = True
        Me.rbUriage.Text = "売上"
        Me.rbUriage.UseVisualStyleBackColor = True
        '
        'rbItaku
        '
        Me.rbItaku.AutoSize = True
        Me.rbItaku.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rbItaku.Location = New System.Drawing.Point(130, 13)
        Me.rbItaku.Name = "rbItaku"
        Me.rbItaku.Size = New System.Drawing.Size(55, 19)
        Me.rbItaku.TabIndex = 2
        Me.rbItaku.TabStop = True
        Me.rbItaku.Text = "委託"
        Me.rbItaku.UseVisualStyleBackColor = True
        '
        'rbAll
        '
        Me.rbAll.AutoSize = True
        Me.rbAll.Checked = True
        Me.rbAll.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rbAll.Location = New System.Drawing.Point(34, 13)
        Me.rbAll.Name = "rbAll"
        Me.rbAll.Size = New System.Drawing.Size(54, 19)
        Me.rbAll.TabIndex = 1
        Me.rbAll.TabStop = True
        Me.rbAll.Text = "全て"
        Me.rbAll.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(20, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(103, 15)
        Me.Label2.TabIndex = 46
        Me.Label2.Text = "売上入金年月"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.GroupBox1.Controls.Add(Me.rbSortKana)
        Me.GroupBox1.Controls.Add(Me.rbSortCode)
        Me.GroupBox1.Location = New System.Drawing.Point(143, 82)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.Size = New System.Drawing.Size(231, 40)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'rbSortKana
        '
        Me.rbSortKana.AutoSize = True
        Me.rbSortKana.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rbSortKana.Location = New System.Drawing.Point(120, 13)
        Me.rbSortKana.Name = "rbSortKana"
        Me.rbSortKana.Size = New System.Drawing.Size(81, 19)
        Me.rbSortKana.TabIndex = 2
        Me.rbSortKana.TabStop = True
        Me.rbSortKana.Text = "カナ名称"
        Me.rbSortKana.UseVisualStyleBackColor = True
        '
        'rbSortCode
        '
        Me.rbSortCode.AutoSize = True
        Me.rbSortCode.Checked = True
        Me.rbSortCode.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.rbSortCode.Location = New System.Drawing.Point(34, 13)
        Me.rbSortCode.Name = "rbSortCode"
        Me.rbSortCode.Size = New System.Drawing.Size(62, 19)
        Me.rbSortCode.TabIndex = 1
        Me.rbSortCode.TabStop = True
        Me.rbSortCode.Text = "コード"
        Me.rbSortCode.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(20, 96)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 15)
        Me.Label1.TabIndex = 48
        Me.Label1.Text = "帳票出力順"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(20, 136)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(87, 15)
        Me.Label3.TabIndex = 49
        Me.Label3.Text = "■出力候補"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(511, 136)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(87, 15)
        Me.Label4.TabIndex = 50
        Me.Label4.Text = "■出力対象"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'dgvKouho
        '
        Me.dgvKouho.AllowUserToResizeColumns = False
        Me.dgvKouho.AllowUserToResizeRows = False
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle7.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvKouho.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.dgvKouho.ColumnHeadersHeight = 25
        Me.dgvKouho.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2})
        Me.dgvKouho.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvKouho.Location = New System.Drawing.Point(23, 154)
        Me.dgvKouho.Name = "dgvKouho"
        Me.dgvKouho.RowHeadersVisible = False
        Me.dgvKouho.RowTemplate.Height = 21
        Me.dgvKouho.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvKouho.Size = New System.Drawing.Size(355, 343)
        Me.dgvKouho.TabIndex = 5
        '
        'Column1
        '
        DataGridViewCellStyle8.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Column1.DefaultCellStyle = DataGridViewCellStyle8
        Me.Column1.HeaderText = "請求先"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 70
        '
        'Column2
        '
        DataGridViewCellStyle9.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Column2.DefaultCellStyle = DataGridViewCellStyle9
        Me.Column2.HeaderText = "　"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 265
        '
        'cmdAllRight
        '
        Me.cmdAllRight.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAllRight.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdAllRight.Location = New System.Drawing.Point(405, 208)
        Me.cmdAllRight.Name = "cmdAllRight"
        Me.cmdAllRight.Size = New System.Drawing.Size(80, 36)
        Me.cmdAllRight.TabIndex = 10
        Me.cmdAllRight.Text = "＞＞"
        Me.cmdAllRight.UseVisualStyleBackColor = True
        '
        'cmdSelectRight
        '
        Me.cmdSelectRight.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSelectRight.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdSelectRight.Location = New System.Drawing.Point(405, 275)
        Me.cmdSelectRight.Name = "cmdSelectRight"
        Me.cmdSelectRight.Size = New System.Drawing.Size(80, 36)
        Me.cmdSelectRight.TabIndex = 11
        Me.cmdSelectRight.Text = "＞"
        Me.cmdSelectRight.UseVisualStyleBackColor = True
        '
        'cmdSelectLeft
        '
        Me.cmdSelectLeft.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSelectLeft.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdSelectLeft.Location = New System.Drawing.Point(405, 342)
        Me.cmdSelectLeft.Name = "cmdSelectLeft"
        Me.cmdSelectLeft.Size = New System.Drawing.Size(80, 36)
        Me.cmdSelectLeft.TabIndex = 12
        Me.cmdSelectLeft.Text = "＜"
        Me.cmdSelectLeft.UseVisualStyleBackColor = True
        '
        'cmdAllLeft
        '
        Me.cmdAllLeft.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAllLeft.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdAllLeft.Location = New System.Drawing.Point(405, 409)
        Me.cmdAllLeft.Name = "cmdAllLeft"
        Me.cmdAllLeft.Size = New System.Drawing.Size(80, 36)
        Me.cmdAllLeft.TabIndex = 13
        Me.cmdAllLeft.Text = "＜＜"
        Me.cmdAllLeft.UseVisualStyleBackColor = True
        '
        'dgvTaisho
        '
        Me.dgvTaisho.AllowUserToResizeColumns = False
        Me.dgvTaisho.AllowUserToResizeRows = False
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle10.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvTaisho.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle10
        Me.dgvTaisho.ColumnHeadersHeight = 25
        Me.dgvTaisho.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2})
        Me.dgvTaisho.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvTaisho.Location = New System.Drawing.Point(514, 154)
        Me.dgvTaisho.Name = "dgvTaisho"
        Me.dgvTaisho.RowHeadersVisible = False
        Me.dgvTaisho.RowTemplate.Height = 21
        Me.dgvTaisho.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTaisho.Size = New System.Drawing.Size(355, 343)
        Me.dgvTaisho.TabIndex = 20
        '
        'DataGridViewTextBoxColumn1
        '
        DataGridViewCellStyle11.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle11
        Me.DataGridViewTextBoxColumn1.HeaderText = "請求先"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 70
        '
        'DataGridViewTextBoxColumn2
        '
        DataGridViewCellStyle12.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle12
        Me.DataGridViewTextBoxColumn2.HeaderText = "　"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 265
        '
        'txtNyukinYm
        '
        Me.txtNyukinYm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtNyukinYm.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtNyukinYm.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtNyukinYm.Location = New System.Drawing.Point(143, 55)
        Me.txtNyukinYm.Margin = New System.Windows.Forms.Padding(0)
        Me.txtNyukinYm.Mask = "0000/00"
        Me.txtNyukinYm.Name = "txtNyukinYm"
        Me.txtNyukinYm.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57), Global.Microsoft.VisualBasic.ChrW(47), Global.Microsoft.VisualBasic.ChrW(95)}
        Me.txtNyukinYm.Size = New System.Drawing.Size(96, 22)
        Me.txtNyukinYm.TabIndex = 0
        Me.txtNyukinYm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'frmH10F03_UrikakeKinList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 561)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtNyukinYm)
        Me.Controls.Add(Me.dgvTaisho)
        Me.Controls.Add(Me.cmdAllLeft)
        Me.Controls.Add(Me.cmdSelectLeft)
        Me.Controls.Add(Me.cmdSelectRight)
        Me.Controls.Add(Me.cmdAllRight)
        Me.Controls.Add(Me.dgvKouho)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TableLayoutPanel33)
        Me.Controls.Add(Me.gbBunrui)
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmH10F03_UrikakeKinList"
        Me.Text = "売掛金一覧表出力指示（H10F03）"
        Me.TableLayoutPanel33.ResumeLayout(False)
        Me.gbBunrui.ResumeLayout(False)
        Me.gbBunrui.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgvKouho, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvTaisho, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel33 As TableLayoutPanel
    Friend WithEvents cmdPreview As Button
    Friend WithEvents cmdBack As Button
    Friend WithEvents cmdPrint As Button
    Friend WithEvents gbBunrui As GroupBox
    Friend WithEvents rbUriage As RadioButton
    Friend WithEvents rbItaku As RadioButton
    Friend WithEvents rbAll As RadioButton
    Friend WithEvents Label2 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents rbSortKana As RadioButton
    Friend WithEvents rbSortCode As RadioButton
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents dgvKouho As DataGridView
    Friend WithEvents cmdAllRight As Button
    Friend WithEvents cmdSelectRight As Button
    Friend WithEvents cmdSelectLeft As Button
    Friend WithEvents cmdAllLeft As Button
    Friend WithEvents dgvTaisho As DataGridView
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents txtNyukinYm As CustomControl.TextBoxDate
End Class
