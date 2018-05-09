<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ZM610E_SeisanMstMente
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
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ZM610E_SeisanMstMente))
        Me.btnModoru = New System.Windows.Forms.Button
        Me.btnTouroku = New System.Windows.Forms.Button
        Me.btnSinki = New System.Windows.Forms.Button
        Me.lblKensuu = New System.Windows.Forms.Label
        Me.btnExcel = New System.Windows.Forms.Button
        Me.dgvSeisanMst = New CustomTabStopDataGridView.TabStop.UtilTabStopDataGridView
        Me.cnSakujoChk = New CustomTabStopDataGridView.TabStop.TabStopCheckBoxColumn
        Me.cnKoutei = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnKouteiBtn = New CustomTabStopDataGridView.TabStop.TabStopButtonColumn
        Me.cnKikaiName = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnTjikan = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnDjikan = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnNjikan = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnFlg = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnBKoutei = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        Me.cnBKikaiName = New CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
        CType(Me.dgvSeisanMst, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.btnSinki.TabIndex = 1
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
        'btnExcel
        '
        Me.btnExcel.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnExcel.Location = New System.Drawing.Point(161, 528)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(130, 45)
        Me.btnExcel.TabIndex = 2
        Me.btnExcel.Text = "EXCEL(&K)"
        Me.btnExcel.UseVisualStyleBackColor = True
        '
        'dgvSeisanMst
        '
        Me.dgvSeisanMst.AllowUserToAddRows = False
        Me.dgvSeisanMst.AllowUserToDeleteRows = False
        Me.dgvSeisanMst.AllowUserToResizeColumns = False
        Me.dgvSeisanMst.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSeisanMst.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvSeisanMst.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSeisanMst.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cnSakujoChk, Me.cnKoutei, Me.cnKouteiBtn, Me.cnKikaiName, Me.cnTjikan, Me.cnDjikan, Me.cnNjikan, Me.cnFlg, Me.cnBKoutei, Me.cnBKikaiName})
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle9.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvSeisanMst.DefaultCellStyle = DataGridViewCellStyle9
        Me.dgvSeisanMst.Location = New System.Drawing.Point(25, 46)
        Me.dgvSeisanMst.MultiSelect = False
        Me.dgvSeisanMst.Name = "dgvSeisanMst"
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle10.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSeisanMst.RowHeadersDefaultCellStyle = DataGridViewCellStyle10
        Me.dgvSeisanMst.RowHeadersVisible = False
        Me.dgvSeisanMst.RowTemplate.Height = 21
        Me.dgvSeisanMst.Size = New System.Drawing.Size(665, 460)
        Me.dgvSeisanMst.TabIndex = 0
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
        'cnKoutei
        '
        Me.cnKoutei.DataPropertyName = "dtKoutei"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle3.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.cnKoutei.DefaultCellStyle = DataGridViewCellStyle3
        Me.cnKoutei.HeaderText = "工程名コード"
        Me.cnKoutei.MaxInputLength = 3
        Me.cnKoutei.Name = "cnKoutei"
        Me.cnKoutei.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnKoutei.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnKoutei.TabStop = True
        '
        'cnKouteiBtn
        '
        Me.cnKouteiBtn.DataPropertyName = "dtKouteiBtn"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Control
        Me.cnKouteiBtn.DefaultCellStyle = DataGridViewCellStyle4
        Me.cnKouteiBtn.HeaderText = ""
        Me.cnKouteiBtn.Name = "cnKouteiBtn"
        Me.cnKouteiBtn.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnKouteiBtn.TabStop = True
        Me.cnKouteiBtn.Width = 25
        '
        'cnKikaiName
        '
        Me.cnKikaiName.DataPropertyName = "dtKikaiName"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle5.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.cnKikaiName.DefaultCellStyle = DataGridViewCellStyle5
        Me.cnKikaiName.HeaderText = "機械略記号"
        Me.cnKikaiName.MaxInputLength = 4
        Me.cnKikaiName.Name = "cnKikaiName"
        Me.cnKikaiName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnKikaiName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnKikaiName.TabStop = True
        '
        'cnTjikan
        '
        Me.cnTjikan.DataPropertyName = "dtTjikan"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle6.Format = "#,##0.0"
        Me.cnTjikan.DefaultCellStyle = DataGridViewCellStyle6
        Me.cnTjikan.HeaderText = "通常稼働時間"
        Me.cnTjikan.MaxInputLength = 6
        Me.cnTjikan.Name = "cnTjikan"
        Me.cnTjikan.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnTjikan.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnTjikan.TabStop = True
        '
        'cnDjikan
        '
        Me.cnDjikan.DataPropertyName = "dtDjikan"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle7.Format = "#,##0.0"
        Me.cnDjikan.DefaultCellStyle = DataGridViewCellStyle7
        Me.cnDjikan.HeaderText = "土曜稼働時間"
        Me.cnDjikan.MaxInputLength = 6
        Me.cnDjikan.Name = "cnDjikan"
        Me.cnDjikan.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnDjikan.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnDjikan.TabStop = True
        '
        'cnNjikan
        '
        Me.cnNjikan.DataPropertyName = "dtNjikan"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle8.Format = "#,##0.0"
        Me.cnNjikan.DefaultCellStyle = DataGridViewCellStyle8
        Me.cnNjikan.HeaderText = "日曜稼働時間"
        Me.cnNjikan.MaxInputLength = 6
        Me.cnNjikan.Name = "cnNjikan"
        Me.cnNjikan.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.cnNjikan.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnNjikan.TabStop = True
        '
        'cnFlg
        '
        Me.cnFlg.DataPropertyName = "dtFlg"
        Me.cnFlg.HeaderText = "変更フラグ非表示"
        Me.cnFlg.Name = "cnFlg"
        Me.cnFlg.ReadOnly = True
        Me.cnFlg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cnFlg.TabStop = True
        Me.cnFlg.Visible = False
        '
        'cnBKoutei
        '
        Me.cnBKoutei.DataPropertyName = "dtBKoutei"
        Me.cnBKoutei.HeaderText = "工程変更前非表示"
        Me.cnBKoutei.Name = "cnBKoutei"
        Me.cnBKoutei.TabStop = True
        Me.cnBKoutei.Visible = False
        '
        'cnBKikaiName
        '
        Me.cnBKikaiName.DataPropertyName = "dtBKikaiName"
        Me.cnBKikaiName.HeaderText = "機械名変更前非表示"
        Me.cnBKikaiName.Name = "cnBKikaiName"
        Me.cnBKikaiName.TabStop = True
        Me.cnBKikaiName.Visible = False
        '
        'ZM610E_SeisanMstMente
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(722, 585)
        Me.Controls.Add(Me.btnExcel)
        Me.Controls.Add(Me.lblKensuu)
        Me.Controls.Add(Me.btnSinki)
        Me.Controls.Add(Me.btnModoru)
        Me.Controls.Add(Me.btnTouroku)
        Me.Controls.Add(Me.dgvSeisanMst)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "ZM610E_SeisanMstMente"
        Me.Text = "生産能力マスタメンテ"
        CType(Me.dgvSeisanMst, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvSeisanMst As CustomTabStopDataGridView.TabStop.UtilTabStopDataGridView
    Friend WithEvents btnModoru As System.Windows.Forms.Button
    Friend WithEvents btnTouroku As System.Windows.Forms.Button
    Friend WithEvents btnSinki As System.Windows.Forms.Button
    Friend WithEvents lblKensuu As System.Windows.Forms.Label
    Friend WithEvents btnExcel As System.Windows.Forms.Button
    Friend WithEvents cnSakujoChk As CustomTabStopDataGridView.TabStop.TabStopCheckBoxColumn
    Friend WithEvents cnKoutei As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnKouteiBtn As CustomTabStopDataGridView.TabStop.TabStopButtonColumn
    Friend WithEvents cnKikaiName As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnTjikan As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnDjikan As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnNjikan As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnFlg As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnBKoutei As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
    Friend WithEvents cnBKikaiName As CustomTabStopDataGridView.TabStop.TabStopTextBoxColumn
End Class
