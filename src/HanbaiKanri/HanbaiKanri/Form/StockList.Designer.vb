<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StockList
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
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.DgvStocklist = New System.Windows.Forms.DataGridView()
        Me.年月 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.メーカー = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.品名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.型式 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.前月末数量 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.前月末単価 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.前月末間接費 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.今月末数量 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.今月入庫数 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.今月出庫数 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.今月単価 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.今月間接費 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LblMode = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TxtName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblConditions = New System.Windows.Forms.Label()
        Me.TxtMaker = New System.Windows.Forms.TextBox()
        Me.BtnSearch = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtModel = New System.Windows.Forms.TextBox()
        CType(Me.DgvStocklist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(1174, 555)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 43)
        Me.BtnBack.TabIndex = 6
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'DgvStocklist
        '
        Me.DgvStocklist.AllowUserToAddRows = False
        Me.DgvStocklist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvStocklist.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.年月, Me.メーカー, Me.品名, Me.型式, Me.前月末数量, Me.前月末単価, Me.前月末間接費, Me.今月末数量, Me.今月入庫数, Me.今月出庫数, Me.今月単価, Me.今月間接費})
        Me.DgvStocklist.Location = New System.Drawing.Point(12, 186)
        Me.DgvStocklist.Name = "DgvStocklist"
        Me.DgvStocklist.RowHeadersVisible = False
        Me.DgvStocklist.RowTemplate.Height = 21
        Me.DgvStocklist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvStocklist.Size = New System.Drawing.Size(1325, 362)
        Me.DgvStocklist.TabIndex = 5
        '
        '年月
        '
        Me.年月.HeaderText = "年月"
        Me.年月.Name = "年月"
        '
        'メーカー
        '
        Me.メーカー.HeaderText = "メーカー"
        Me.メーカー.Name = "メーカー"
        '
        '品名
        '
        Me.品名.HeaderText = "品名"
        Me.品名.Name = "品名"
        '
        '型式
        '
        Me.型式.HeaderText = "型式"
        Me.型式.Name = "型式"
        '
        '前月末数量
        '
        Me.前月末数量.HeaderText = "前月末数量"
        Me.前月末数量.Name = "前月末数量"
        '
        '前月末単価
        '
        Me.前月末単価.HeaderText = "前月末単価"
        Me.前月末単価.Name = "前月末単価"
        '
        '前月末間接費
        '
        Me.前月末間接費.HeaderText = "前月末間接費"
        Me.前月末間接費.Name = "前月末間接費"
        '
        '今月末数量
        '
        Me.今月末数量.HeaderText = "今月数量"
        Me.今月末数量.Name = "今月末数量"
        '
        '今月入庫数
        '
        Me.今月入庫数.HeaderText = "今月入庫数"
        Me.今月入庫数.Name = "今月入庫数"
        '
        '今月出庫数
        '
        Me.今月出庫数.HeaderText = "今月出庫数"
        Me.今月出庫数.Name = "今月出庫数"
        '
        '今月単価
        '
        Me.今月単価.HeaderText = "今月単価"
        Me.今月単価.Name = "今月単価"
        '
        '今月間接費
        '
        Me.今月間接費.HeaderText = "今月間接費"
        Me.今月間接費.Name = "今月間接費"
        '
        'LblMode
        '
        Me.LblMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMode.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMode.Location = New System.Drawing.Point(1174, 9)
        Me.LblMode.Name = "LblMode"
        Me.LblMode.Size = New System.Drawing.Size(165, 24)
        Me.LblMode.TabIndex = 331
        Me.LblMode.Text = "モード"
        Me.LblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.LblMode.Visible = False
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(14, 72)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(170, 24)
        Me.Label8.TabIndex = 330
        Me.Label8.Text = "品名"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtName
        '
        Me.TxtName.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtName.Location = New System.Drawing.Point(190, 73)
        Me.TxtName.Name = "TxtName"
        Me.TxtName.Size = New System.Drawing.Size(200, 22)
        Me.TxtName.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(14, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(170, 24)
        Me.Label1.TabIndex = 328
        Me.Label1.Text = "メーカー"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblConditions
        '
        Me.LblConditions.AutoSize = True
        Me.LblConditions.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblConditions.Location = New System.Drawing.Point(11, 13)
        Me.LblConditions.Name = "LblConditions"
        Me.LblConditions.Size = New System.Drawing.Size(87, 15)
        Me.LblConditions.TabIndex = 327
        Me.LblConditions.Text = "■抽出条件"
        '
        'TxtMaker
        '
        Me.TxtMaker.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtMaker.Location = New System.Drawing.Point(190, 42)
        Me.TxtMaker.Name = "TxtMaker"
        Me.TxtMaker.Size = New System.Drawing.Size(200, 22)
        Me.TxtMaker.TabIndex = 1
        '
        'BtnSearch
        '
        Me.BtnSearch.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnSearch.Location = New System.Drawing.Point(1174, 41)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(165, 43)
        Me.BtnSearch.TabIndex = 4
        Me.BtnSearch.Text = "検索"
        Me.BtnSearch.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(14, 102)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(170, 24)
        Me.Label2.TabIndex = 336
        Me.Label2.Text = "型式"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtModel
        '
        Me.TxtModel.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtModel.Location = New System.Drawing.Point(190, 103)
        Me.TxtModel.Name = "TxtModel"
        Me.TxtModel.Size = New System.Drawing.Size(200, 22)
        Me.TxtModel.TabIndex = 3
        '
        'StockList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1350, 608)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtModel)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.DgvStocklist)
        Me.Controls.Add(Me.LblMode)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TxtName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LblConditions)
        Me.Controls.Add(Me.TxtMaker)
        Me.Controls.Add(Me.BtnSearch)
        Me.Name = "StockList"
        Me.Text = "StockList"
        CType(Me.DgvStocklist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnBack As Button
    Friend WithEvents DgvStocklist As DataGridView
    Friend WithEvents LblMode As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents TxtName As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents LblConditions As Label
    Friend WithEvents TxtMaker As TextBox
    Friend WithEvents BtnSearch As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtModel As TextBox
    Friend WithEvents 年月 As DataGridViewTextBoxColumn
    Friend WithEvents メーカー As DataGridViewTextBoxColumn
    Friend WithEvents 品名 As DataGridViewTextBoxColumn
    Friend WithEvents 型式 As DataGridViewTextBoxColumn
    Friend WithEvents 前月末数量 As DataGridViewTextBoxColumn
    Friend WithEvents 前月末単価 As DataGridViewTextBoxColumn
    Friend WithEvents 前月末間接費 As DataGridViewTextBoxColumn
    Friend WithEvents 今月末数量 As DataGridViewTextBoxColumn
    Friend WithEvents 今月入庫数 As DataGridViewTextBoxColumn
    Friend WithEvents 今月出庫数 As DataGridViewTextBoxColumn
    Friend WithEvents 今月単価 As DataGridViewTextBoxColumn
    Friend WithEvents 今月間接費 As DataGridViewTextBoxColumn
End Class
