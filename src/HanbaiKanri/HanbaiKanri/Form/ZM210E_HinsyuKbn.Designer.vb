<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ZM210E_HinsyuKbn
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
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ZM210E_HinsyuKbn))
        Me.btnModoru = New System.Windows.Forms.Button
        Me.btnTouroku = New System.Windows.Forms.Button
        Me.btnSinki = New System.Windows.Forms.Button
        Me.lblKensuu = New System.Windows.Forms.Label
        Me.dgvHinsyuMst = New CustomTabStopDataGridView.TabStop.UtilTabStopDataGridView
        Me.cnSakujoChk = New CustomTabStopDataGridView.TabStop.TabStopCheckBoxColumn
        Me.cnJuyousakiCD = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnJuyousakiCDBtn = New CustomTabStopDataGridView.TabStop.TabStopButtonColumn
        Me.cnJuyousaki = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnCD = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnHinsyu = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnHenkouFlg = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnHenkomaeJuyou = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnHenkomaeHinsyu = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        CType(Me.dgvHinsyuMst, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnModoru
        '
        Me.btnModoru.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnModoru.Location = New System.Drawing.Point(560, 528)
        Me.btnModoru.Name = "btnModoru"
        Me.btnModoru.Size = New System.Drawing.Size(130, 45)
        Me.btnModoru.TabIndex = 4
        Me.btnModoru.Text = "戻る(&B)"
        Me.btnModoru.UseVisualStyleBackColor = True
        '
        'btnTouroku
        '
        Me.btnTouroku.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnTouroku.Location = New System.Drawing.Point(424, 528)
        Me.btnTouroku.Name = "btnTouroku"
        Me.btnTouroku.Size = New System.Drawing.Size(130, 45)
        Me.btnTouroku.TabIndex = 3
        Me.btnTouroku.Text = "登録(&R)"
        Me.btnTouroku.UseVisualStyleBackColor = True
        '
        'btnSinki
        '
        Me.btnSinki.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSinki.Location = New System.Drawing.Point(25, 528)
        Me.btnSinki.Name = "btnSinki"
        Me.btnSinki.Size = New System.Drawing.Size(130, 45)
        Me.btnSinki.TabIndex = 2
        Me.btnSinki.Text = "新規追加(&I)"
        Me.btnSinki.UseVisualStyleBackColor = True
        '
        'lblKensuu
        '
        Me.lblKensuu.BackColor = System.Drawing.SystemColors.Control
        Me.lblKensuu.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKensuu.Location = New System.Drawing.Point(642, 21)
        Me.lblKensuu.Name = "lblKensuu"
        Me.lblKensuu.Size = New System.Drawing.Size(57, 22)
        Me.lblKensuu.TabIndex = 1301
        Me.lblKensuu.Text = "9999件"
        Me.lblKensuu.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dgvHinsyuMst
        '
        Me.dgvHinsyuMst.AllowUserToAddRows = False
        Me.dgvHinsyuMst.AllowUserToDeleteRows = False
        Me.dgvHinsyuMst.AllowUserToResizeColumns = False
        Me.dgvHinsyuMst.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvHinsyuMst.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvHinsyuMst.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvHinsyuMst.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cnSakujoChk, Me.cnJuyousakiCD, Me.cnJuyousakiCDBtn, Me.cnJuyousaki, Me.cnCD, Me.cnHinsyu, Me.cnHenkouFlg, Me.cnHenkomaeJuyou, Me.cnHenkomaeHinsyu})
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvHinsyuMst.DefaultCellStyle = DataGridViewCellStyle8
        Me.dgvHinsyuMst.Location = New System.Drawing.Point(25, 46)
        Me.dgvHinsyuMst.MultiSelect = False
        Me.dgvHinsyuMst.Name = "dgvHinsyuMst"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle9.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvHinsyuMst.RowHeadersDefaultCellStyle = DataGridViewCellStyle9
        Me.dgvHinsyuMst.RowHeadersVisible = False
        Me.dgvHinsyuMst.RowTemplate.Height = 21
        Me.dgvHinsyuMst.Size = New System.Drawing.Size(665, 460)
        Me.dgvHinsyuMst.TabIndex = 0
        '
        'cnSakujoChk
        '
        Me.cnSakujoChk.DataPropertyName = "dtSakujoChk"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle2.NullValue = False
        Me.cnSakujoChk.DefaultCellStyle = DataGridViewCellStyle2
        Me.cnSakujoChk.HeaderText = "削除"
        Me.cnSakujoChk.Name = "cnSakujoChk"
        Me.cnSakujoChk.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnSakujoChk.TabStop = True
        Me.cnSakujoChk.Width = 50
        '
        'cnJuyousakiCD
        '
        Me.cnJuyousakiCD.DataPropertyName = "dtJuyousakiCD"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnJuyousakiCD.DefaultCellStyle = DataGridViewCellStyle3
        Me.cnJuyousakiCD.HeaderText = "需要先"
        Me.cnJuyousakiCD.MaxInputLength = 1
        Me.cnJuyousakiCD.Name = "cnJuyousakiCD"
        Me.cnJuyousakiCD.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnJuyousakiCD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnJuyousakiCD.TabStop = True
        '
        'cnJuyousakiCDBtn
        '
        Me.cnJuyousakiCDBtn.DataPropertyName = "dtJuyousakiCDBtn"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Control
        Me.cnJuyousakiCDBtn.DefaultCellStyle = DataGridViewCellStyle4
        Me.cnJuyousakiCDBtn.HeaderText = ""
        Me.cnJuyousakiCDBtn.Name = "cnJuyousakiCDBtn"
        Me.cnJuyousakiCDBtn.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnJuyousakiCDBtn.TabStop = True
        Me.cnJuyousakiCDBtn.Width = 25
        '
        'cnJuyousaki
        '
        Me.cnJuyousaki.DataPropertyName = "dtJuyousaki"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        Me.cnJuyousaki.DefaultCellStyle = DataGridViewCellStyle5
        Me.cnJuyousaki.HeaderText = "需要先名"
        Me.cnJuyousaki.Name = "cnJuyousaki"
        Me.cnJuyousaki.ReadOnly = True
        Me.cnJuyousaki.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnJuyousaki.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnJuyousaki.TabStop = False
        Me.cnJuyousaki.Width = 120
        '
        'cnCD
        '
        Me.cnCD.DataPropertyName = "dtCD"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnCD.DefaultCellStyle = DataGridViewCellStyle6
        Me.cnCD.HeaderText = "品種区分"
        Me.cnCD.MaxInputLength = 3
        Me.cnCD.Name = "cnCD"
        Me.cnCD.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnCD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnCD.TabStop = True
        '
        'cnHinsyu
        '
        Me.cnHinsyu.DataPropertyName = "dtHinsyu"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cnHinsyu.DefaultCellStyle = DataGridViewCellStyle7
        Me.cnHinsyu.HeaderText = "品種区分名"
        Me.cnHinsyu.MaxInputLength = 50
        Me.cnHinsyu.Name = "cnHinsyu"
        Me.cnHinsyu.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnHinsyu.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnHinsyu.TabStop = True
        Me.cnHinsyu.Width = 250
        '
        'cnHenkouFlg
        '
        Me.cnHenkouFlg.DataPropertyName = "dtHenkouFlg"
        Me.cnHenkouFlg.HeaderText = "変更フラグ"
        Me.cnHenkouFlg.Name = "cnHenkouFlg"
        Me.cnHenkouFlg.ReadOnly = True
        Me.cnHenkouFlg.TabStop = True
        Me.cnHenkouFlg.Visible = False
        '
        'cnHenkomaeJuyou
        '
        Me.cnHenkomaeJuyou.DataPropertyName = "dtHenkomaeJuyou"
        Me.cnHenkomaeJuyou.HeaderText = "変更前需要先"
        Me.cnHenkomaeJuyou.Name = "cnHenkomaeJuyou"
        Me.cnHenkomaeJuyou.ReadOnly = True
        Me.cnHenkomaeJuyou.TabStop = True
        Me.cnHenkomaeJuyou.Visible = False
        '
        'cnHenkomaeHinsyu
        '
        Me.cnHenkomaeHinsyu.DataPropertyName = "dtHenkomaeHinsyu"
        Me.cnHenkomaeHinsyu.HeaderText = "変更前品種区分"
        Me.cnHenkomaeHinsyu.Name = "cnHenkomaeHinsyu"
        Me.cnHenkomaeHinsyu.ReadOnly = True
        Me.cnHenkomaeHinsyu.TabStop = True
        Me.cnHenkomaeHinsyu.Visible = False
        '
        'ZM210E_HinsyuKbn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(722, 585)
        Me.Controls.Add(Me.lblKensuu)
        Me.Controls.Add(Me.btnSinki)
        Me.Controls.Add(Me.btnModoru)
        Me.Controls.Add(Me.btnTouroku)
        Me.Controls.Add(Me.dgvHinsyuMst)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "ZM210E_HinsyuKbn"
        Me.Text = "品種区分マスタメンテ"
        CType(Me.dgvHinsyuMst, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvHinsyuMst As CustomTabStopDataGridView.TabStop.UtilTabStopDataGridView
    Friend WithEvents btnModoru As System.Windows.Forms.Button
    Friend WithEvents btnTouroku As System.Windows.Forms.Button
    Friend WithEvents btnSinki As System.Windows.Forms.Button
    Friend WithEvents lblKensuu As System.Windows.Forms.Label
    Friend WithEvents cnSakujoChk As CustomTabStopDataGridView.TabStop.TabStopCheckBoxColumn
    Friend WithEvents cnJuyousakiCD As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnJuyousakiCDBtn As CustomTabStopDataGridView.TabStop.TabStopButtonColumn
    Friend WithEvents cnJuyousaki As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnCD As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnHinsyu As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnHenkouFlg As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnHenkomaeJuyou As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnHenkomaeHinsyu As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
End Class
