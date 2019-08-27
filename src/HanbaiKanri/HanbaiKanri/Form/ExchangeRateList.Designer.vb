<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ExchangeRateList
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
        Me.BtnEdit = New System.Windows.Forms.Button()
        Me.BtnSearch = New System.Windows.Forms.Button()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.BtnAdd = New System.Windows.Forms.Button()
        Me.DgvList = New System.Windows.Forms.DataGridView()
        Me.基準日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.採番キー = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.レート = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.更新者 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.更新日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LblMode = New System.Windows.Forms.Label()
        Me.dtDateUntil = New System.Windows.Forms.DateTimePicker()
        Me.dtDateSince = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.LblStandardDate = New System.Windows.Forms.Label()
        Me.LblConditions = New System.Windows.Forms.Label()
        Me.BtnDel = New System.Windows.Forms.Button()
        CType(Me.DgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnEdit
        '
        Me.BtnEdit.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnEdit.Location = New System.Drawing.Point(500, 509)
        Me.BtnEdit.Name = "BtnEdit"
        Me.BtnEdit.Size = New System.Drawing.Size(165, 40)
        Me.BtnEdit.TabIndex = 17
        Me.BtnEdit.Text = "編集"
        Me.BtnEdit.UseVisualStyleBackColor = True
        Me.BtnEdit.Visible = False
        '
        'BtnSearch
        '
        Me.BtnSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnSearch.Location = New System.Drawing.Point(1174, 41)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(165, 40)
        Me.BtnSearch.TabIndex = 11
        Me.BtnSearch.Text = "検索"
        Me.BtnSearch.UseVisualStyleBackColor = True
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(1174, 509)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 18
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'BtnAdd
        '
        Me.BtnAdd.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnAdd.Location = New System.Drawing.Point(1003, 509)
        Me.BtnAdd.Name = "BtnAdd"
        Me.BtnAdd.Size = New System.Drawing.Size(165, 40)
        Me.BtnAdd.TabIndex = 16
        Me.BtnAdd.Text = "追加"
        Me.BtnAdd.UseVisualStyleBackColor = True
        '
        'DgvList
        '
        Me.DgvList.AllowUserToAddRows = False
        Me.DgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.基準日, Me.採番キー, Me.レート, Me.更新者, Me.更新日})
        Me.DgvList.Location = New System.Drawing.Point(13, 100)
        Me.DgvList.Name = "DgvList"
        Me.DgvList.ReadOnly = True
        Me.DgvList.RowHeadersVisible = False
        Me.DgvList.RowTemplate.Height = 21
        Me.DgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvList.Size = New System.Drawing.Size(1326, 403)
        Me.DgvList.TabIndex = 15
        '
        '基準日
        '
        Me.基準日.HeaderText = "基準日"
        Me.基準日.Name = "基準日"
        Me.基準日.ReadOnly = True
        '
        '採番キー
        '
        Me.採番キー.HeaderText = "採番キー"
        Me.採番キー.Name = "採番キー"
        Me.採番キー.ReadOnly = True
        '
        'レート
        '
        Me.レート.HeaderText = "レート"
        Me.レート.Name = "レート"
        Me.レート.ReadOnly = True
        '
        '更新者
        '
        Me.更新者.HeaderText = "更新者"
        Me.更新者.Name = "更新者"
        Me.更新者.ReadOnly = True
        '
        '更新日
        '
        Me.更新日.HeaderText = "更新日"
        Me.更新日.Name = "更新日"
        Me.更新日.ReadOnly = True
        '
        'LblMode
        '
        Me.LblMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMode.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMode.Location = New System.Drawing.Point(1174, 9)
        Me.LblMode.Name = "LblMode"
        Me.LblMode.Size = New System.Drawing.Size(165, 22)
        Me.LblMode.TabIndex = 305
        Me.LblMode.Text = "モード"
        Me.LblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.LblMode.Visible = False
        '
        'dtDateUntil
        '
        Me.dtDateUntil.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtDateUntil.CustomFormat = ""
        Me.dtDateUntil.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtDateUntil.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtDateUntil.Location = New System.Drawing.Point(390, 42)
        Me.dtDateUntil.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtDateUntil.Name = "dtDateUntil"
        Me.dtDateUntil.Size = New System.Drawing.Size(170, 22)
        Me.dtDateUntil.TabIndex = 336
        Me.dtDateUntil.Value = New Date(2019, 1, 1, 0, 0, 0, 0)
        '
        'dtDateSince
        '
        Me.dtDateSince.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtDateSince.CustomFormat = ""
        Me.dtDateSince.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtDateSince.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtDateSince.Location = New System.Drawing.Point(191, 41)
        Me.dtDateSince.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtDateSince.Name = "dtDateSince"
        Me.dtDateSince.Size = New System.Drawing.Size(170, 22)
        Me.dtDateSince.TabIndex = 335
        Me.dtDateSince.Value = New Date(2019, 1, 1, 0, 0, 0, 0)
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(367, 48)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(17, 12)
        Me.Label5.TabIndex = 334
        Me.Label5.Text = "～"
        '
        'LblStandardDate
        '
        Me.LblStandardDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblStandardDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblStandardDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblStandardDate.Location = New System.Drawing.Point(15, 41)
        Me.LblStandardDate.Name = "LblStandardDate"
        Me.LblStandardDate.Size = New System.Drawing.Size(170, 22)
        Me.LblStandardDate.TabIndex = 333
        Me.LblStandardDate.Text = "基準日"
        Me.LblStandardDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblConditions
        '
        Me.LblConditions.AutoSize = True
        Me.LblConditions.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblConditions.Location = New System.Drawing.Point(12, 16)
        Me.LblConditions.Name = "LblConditions"
        Me.LblConditions.Size = New System.Drawing.Size(87, 15)
        Me.LblConditions.TabIndex = 337
        Me.LblConditions.Text = "■抽出条件"
        '
        'BtnDel
        '
        Me.BtnDel.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnDel.Location = New System.Drawing.Point(12, 509)
        Me.BtnDel.Name = "BtnDel"
        Me.BtnDel.Size = New System.Drawing.Size(165, 40)
        Me.BtnDel.TabIndex = 338
        Me.BtnDel.Text = "削除"
        Me.BtnDel.UseVisualStyleBackColor = True
        '
        'ExchangeRateList
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.BtnDel)
        Me.Controls.Add(Me.LblConditions)
        Me.Controls.Add(Me.dtDateUntil)
        Me.Controls.Add(Me.dtDateSince)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.LblStandardDate)
        Me.Controls.Add(Me.LblMode)
        Me.Controls.Add(Me.BtnEdit)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.BtnAdd)
        Me.Controls.Add(Me.DgvList)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "ExchangeRateList"
        Me.Text = "ExchangeRate"
        CType(Me.DgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnEdit As Button
    Friend WithEvents BtnSearch As Button
    Friend WithEvents BtnBack As Button
    Friend WithEvents BtnAdd As Button
    Friend WithEvents DgvList As DataGridView
    Friend WithEvents LblMode As Label
    Friend WithEvents 基準日 As DataGridViewTextBoxColumn
    Friend WithEvents 採番キー As DataGridViewTextBoxColumn
    Friend WithEvents レート As DataGridViewTextBoxColumn
    Friend WithEvents 更新者 As DataGridViewTextBoxColumn
    Friend WithEvents 更新日 As DataGridViewTextBoxColumn
    Friend WithEvents dtDateUntil As DateTimePicker
    Friend WithEvents dtDateSince As DateTimePicker
    Friend WithEvents Label5 As Label
    Friend WithEvents LblStandardDate As Label
    Friend WithEvents LblConditions As Label
    Friend WithEvents BtnDel As Button
End Class
