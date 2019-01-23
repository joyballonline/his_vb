<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ClosingLog
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
        Me.LblMode = New System.Windows.Forms.Label()
        Me.LblPerson = New System.Windows.Forms.Label()
        Me.TxtPerson = New System.Windows.Forms.TextBox()
        Me.LblClosingDate = New System.Windows.Forms.Label()
        Me.LblConditions = New System.Windows.Forms.Label()
        Me.TxtClosingDate = New System.Windows.Forms.TextBox()
        Me.BtnSearch = New System.Windows.Forms.Button()
        Me.DgvClosingLog = New System.Windows.Forms.DataGridView()
        Me.締処理日時 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.前回締日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.今回締日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.次回締日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.担当者 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BtnClosing = New System.Windows.Forms.Button()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.DgvClosingLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LblMode
        '
        Me.LblMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMode.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMode.Location = New System.Drawing.Point(1175, 5)
        Me.LblMode.Name = "LblMode"
        Me.LblMode.Size = New System.Drawing.Size(165, 24)
        Me.LblMode.TabIndex = 321
        Me.LblMode.Text = "モード"
        Me.LblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.LblMode.Visible = False
        '
        'LblPerson
        '
        Me.LblPerson.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblPerson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblPerson.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblPerson.Location = New System.Drawing.Point(15, 68)
        Me.LblPerson.Name = "LblPerson"
        Me.LblPerson.Size = New System.Drawing.Size(170, 24)
        Me.LblPerson.TabIndex = 314
        Me.LblPerson.Text = "担当者"
        Me.LblPerson.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtPerson
        '
        Me.TxtPerson.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPerson.Location = New System.Drawing.Point(191, 69)
        Me.TxtPerson.Name = "TxtPerson"
        Me.TxtPerson.Size = New System.Drawing.Size(200, 22)
        Me.TxtPerson.TabIndex = 2
        '
        'LblClosingDate
        '
        Me.LblClosingDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblClosingDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblClosingDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblClosingDate.Location = New System.Drawing.Point(15, 39)
        Me.LblClosingDate.Name = "LblClosingDate"
        Me.LblClosingDate.Size = New System.Drawing.Size(170, 24)
        Me.LblClosingDate.TabIndex = 310
        Me.LblClosingDate.Text = "締処理日時"
        Me.LblClosingDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblConditions
        '
        Me.LblConditions.AutoSize = True
        Me.LblConditions.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblConditions.Location = New System.Drawing.Point(12, 10)
        Me.LblConditions.Name = "LblConditions"
        Me.LblConditions.Size = New System.Drawing.Size(87, 15)
        Me.LblConditions.TabIndex = 309
        Me.LblConditions.Text = "■抽出条件"
        '
        'TxtClosingDate
        '
        Me.TxtClosingDate.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtClosingDate.Location = New System.Drawing.Point(191, 39)
        Me.TxtClosingDate.Name = "TxtClosingDate"
        Me.TxtClosingDate.Size = New System.Drawing.Size(200, 22)
        Me.TxtClosingDate.TabIndex = 1
        '
        'BtnSearch
        '
        Me.BtnSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnSearch.Location = New System.Drawing.Point(1175, 38)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(165, 43)
        Me.BtnSearch.TabIndex = 3
        Me.BtnSearch.Text = "検索"
        Me.BtnSearch.UseVisualStyleBackColor = True
        '
        'DgvClosingLog
        '
        Me.DgvClosingLog.AllowUserToAddRows = False
        DataGridViewCellStyle1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DgvClosingLog.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DgvClosingLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader
        Me.DgvClosingLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvClosingLog.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.締処理日時, Me.前回締日, Me.今回締日, Me.次回締日, Me.担当者})
        Me.DgvClosingLog.Location = New System.Drawing.Point(15, 183)
        Me.DgvClosingLog.Name = "DgvClosingLog"
        Me.DgvClosingLog.RowHeadersVisible = False
        Me.DgvClosingLog.RowTemplate.Height = 21
        Me.DgvClosingLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvClosingLog.Size = New System.Drawing.Size(1325, 362)
        Me.DgvClosingLog.TabIndex = 4
        '
        '締処理日時
        '
        Me.締処理日時.HeaderText = "締処理日時"
        Me.締処理日時.Name = "締処理日時"
        Me.締処理日時.Width = 92
        '
        '前回締日
        '
        Me.前回締日.HeaderText = "前回締日"
        Me.前回締日.Name = "前回締日"
        Me.前回締日.Width = 80
        '
        '今回締日
        '
        Me.今回締日.HeaderText = "今回締日"
        Me.今回締日.Name = "今回締日"
        Me.今回締日.Width = 80
        '
        '次回締日
        '
        Me.次回締日.HeaderText = "次回締日"
        Me.次回締日.Name = "次回締日"
        Me.次回締日.Width = 80
        '
        '担当者
        '
        Me.担当者.HeaderText = "担当者"
        Me.担当者.Name = "担当者"
        Me.担当者.Width = 68
        '
        'BtnClosing
        '
        Me.BtnClosing.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnClosing.Location = New System.Drawing.Point(1004, 551)
        Me.BtnClosing.Name = "BtnClosing"
        Me.BtnClosing.Size = New System.Drawing.Size(165, 43)
        Me.BtnClosing.TabIndex = 5
        Me.BtnClosing.Text = "締処理実行"
        Me.BtnClosing.UseVisualStyleBackColor = True
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(1175, 551)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 43)
        Me.BtnBack.TabIndex = 6
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(908, 563)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 25)
        Me.Button1.TabIndex = 322
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'ClosingLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1350, 608)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.BtnClosing)
        Me.Controls.Add(Me.DgvClosingLog)
        Me.Controls.Add(Me.LblMode)
        Me.Controls.Add(Me.LblPerson)
        Me.Controls.Add(Me.TxtPerson)
        Me.Controls.Add(Me.LblClosingDate)
        Me.Controls.Add(Me.LblConditions)
        Me.Controls.Add(Me.TxtClosingDate)
        Me.Controls.Add(Me.BtnSearch)
        Me.Name = "ClosingLog"
        Me.Text = "ClosingLog"
        CType(Me.DgvClosingLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LblMode As Label
    Friend WithEvents LblPerson As Label
    Friend WithEvents TxtPerson As TextBox
    Friend WithEvents LblClosingDate As Label
    Friend WithEvents LblConditions As Label
    Friend WithEvents TxtClosingDate As TextBox
    Friend WithEvents BtnSearch As Button
    Friend WithEvents DgvClosingLog As DataGridView
    Friend WithEvents BtnClosing As Button
    Friend WithEvents BtnBack As Button
    Friend WithEvents 締処理日時 As DataGridViewTextBoxColumn
    Friend WithEvents 前回締日 As DataGridViewTextBoxColumn
    Friend WithEvents 今回締日 As DataGridViewTextBoxColumn
    Friend WithEvents 次回締日 As DataGridViewTextBoxColumn
    Friend WithEvents 担当者 As DataGridViewTextBoxColumn
    Friend WithEvents Button1 As Button
End Class
