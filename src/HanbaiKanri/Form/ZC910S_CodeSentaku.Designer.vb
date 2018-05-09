<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ZC910S_CodeSentaku
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows フォーム デザイナで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使用して変更できます。  
    'コード エディタを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ZC910S_CodeSentaku))
        Me.dgvJuyousaki = New CustomTabStopDataGridView.TabStop.UtilTabStopDataGridView
        Me.cnCd = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cdMeisyou = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.lblMeisyo = New System.Windows.Forms.Label
        Me.btnSelect = New System.Windows.Forms.Button
        Me.btnModoru = New System.Windows.Forms.Button
        Me.lblKensu = New System.Windows.Forms.Label
        Me.lblKahenKey = New System.Windows.Forms.Label
        CType(Me.dgvJuyousaki, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvJuyousaki
        '
        Me.dgvJuyousaki.AllowUserToAddRows = False
        Me.dgvJuyousaki.AllowUserToDeleteRows = False
        Me.dgvJuyousaki.AllowUserToResizeColumns = False
        Me.dgvJuyousaki.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvJuyousaki.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvJuyousaki.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvJuyousaki.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cnCd, Me.cdMeisyou})
        Me.dgvJuyousaki.Location = New System.Drawing.Point(17, 58)
        Me.dgvJuyousaki.MultiSelect = False
        Me.dgvJuyousaki.Name = "dgvJuyousaki"
        Me.dgvJuyousaki.ReadOnly = True
        Me.dgvJuyousaki.RowHeadersVisible = False
        Me.dgvJuyousaki.RowTemplate.Height = 21
        Me.dgvJuyousaki.Size = New System.Drawing.Size(254, 124)
        Me.dgvJuyousaki.TabIndex = 0
        '
        'cnCd
        '
        Me.cnCd.DataPropertyName = "dtCd"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        Me.cnCd.DefaultCellStyle = DataGridViewCellStyle2
        Me.cnCd.HeaderText = "コード"
        Me.cnCd.Name = "cnCd"
        Me.cnCd.ReadOnly = True
        Me.cnCd.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnCd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnCd.TabStop = True
        Me.cnCd.Width = 50
        '
        'cdMeisyou
        '
        Me.cdMeisyou.DataPropertyName = "dtMeisyou"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        Me.cdMeisyou.DefaultCellStyle = DataGridViewCellStyle3
        Me.cdMeisyou.HeaderText = "名称"
        Me.cdMeisyou.Name = "cdMeisyou"
        Me.cdMeisyou.ReadOnly = True
        Me.cdMeisyou.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cdMeisyou.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cdMeisyou.TabStop = True
        Me.cdMeisyou.Width = 183
        '
        'lblMeisyo
        '
        Me.lblMeisyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblMeisyo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMeisyo.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMeisyo.Location = New System.Drawing.Point(17, 24)
        Me.lblMeisyo.Name = "lblMeisyo"
        Me.lblMeisyo.Size = New System.Drawing.Size(138, 22)
        Me.lblMeisyo.TabIndex = 29
        Me.lblMeisyo.Text = "需要先"
        Me.lblMeisyo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnSelect
        '
        Me.btnSelect.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSelect.Location = New System.Drawing.Point(45, 214)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(110, 45)
        Me.btnSelect.TabIndex = 1
        Me.btnSelect.Text = "選択(&E)"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'btnModoru
        '
        Me.btnModoru.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnModoru.Location = New System.Drawing.Point(161, 214)
        Me.btnModoru.Name = "btnModoru"
        Me.btnModoru.Size = New System.Drawing.Size(110, 45)
        Me.btnModoru.TabIndex = 2
        Me.btnModoru.Text = "戻る(&B)"
        Me.btnModoru.UseVisualStyleBackColor = True
        '
        'lblKensu
        '
        Me.lblKensu.BackColor = System.Drawing.SystemColors.Control
        Me.lblKensu.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!)
        Me.lblKensu.Location = New System.Drawing.Point(202, 33)
        Me.lblKensu.Name = "lblKensu"
        Me.lblKensu.Size = New System.Drawing.Size(69, 22)
        Me.lblKensu.TabIndex = 96
        Me.lblKensu.Text = "9999件"
        Me.lblKensu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblKahenKey
        '
        Me.lblKahenKey.BackColor = System.Drawing.SystemColors.Control
        Me.lblKahenKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblKahenKey.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKahenKey.Location = New System.Drawing.Point(64, 185)
        Me.lblKahenKey.Name = "lblKahenKey"
        Me.lblKahenKey.Size = New System.Drawing.Size(91, 22)
        Me.lblKahenKey.TabIndex = 97
        Me.lblKahenKey.Text = "可変キー非表示"
        Me.lblKahenKey.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblKahenKey.Visible = False
        '
        'ZC910S_CodeSentaku
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Controls.Add(Me.lblKahenKey)
        Me.Controls.Add(Me.lblKensu)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.btnModoru)
        Me.Controls.Add(Me.lblMeisyo)
        Me.Controls.Add(Me.dgvJuyousaki)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "ZC910S_CodeSentaku"
        Me.Text = "コード選択"
        CType(Me.dgvJuyousaki, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvJuyousaki As CustomTabStopDataGridView.TabStop.UtilTabStopDataGridView
    Friend WithEvents lblMeisyo As System.Windows.Forms.Label
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents btnModoru As System.Windows.Forms.Button
    Friend WithEvents lblKensu As System.Windows.Forms.Label
    Friend WithEvents lblKahenKey As System.Windows.Forms.Label
    Friend WithEvents cnCd As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cdMeisyou As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn

End Class
