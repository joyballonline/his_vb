<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class StockSearch
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
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.DgvList = New System.Windows.Forms.DataGridView()
        Me.BtnSelect = New System.Windows.Forms.Button()
        Me.引当 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.倉庫コード = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.倉庫 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.最終入庫日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.入出庫種別区分 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.入出庫種別 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.現在庫数 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.入庫単価 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.最終出庫日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.伝票番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.行番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.単位 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ロケ番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.入庫番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.入庫行番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(537, 396)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 5
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'DgvList
        '
        Me.DgvList.AllowUserToAddRows = False
        Me.DgvList.AllowUserToDeleteRows = False
        Me.DgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.引当, Me.倉庫コード, Me.倉庫, Me.最終入庫日, Me.入出庫種別区分, Me.入出庫種別, Me.現在庫数, Me.入庫単価, Me.最終出庫日, Me.伝票番号, Me.行番号, Me.単位, Me.ロケ番号, Me.入庫番号, Me.入庫行番号})
        Me.DgvList.Location = New System.Drawing.Point(12, 12)
        Me.DgvList.Name = "DgvList"
        Me.DgvList.ReadOnly = True
        Me.DgvList.RowHeadersVisible = False
        Me.DgvList.RowTemplate.Height = 21
        Me.DgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvList.Size = New System.Drawing.Size(690, 367)
        Me.DgvList.TabIndex = 3
        '
        'BtnSelect
        '
        Me.BtnSelect.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnSelect.Location = New System.Drawing.Point(366, 396)
        Me.BtnSelect.Name = "BtnSelect"
        Me.BtnSelect.Size = New System.Drawing.Size(165, 40)
        Me.BtnSelect.TabIndex = 277
        Me.BtnSelect.Text = "選択"
        Me.BtnSelect.UseVisualStyleBackColor = True
        '
        '引当
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.引当.DefaultCellStyle = DataGridViewCellStyle1
        Me.引当.HeaderText = "引当"
        Me.引当.Name = "引当"
        Me.引当.ReadOnly = True
        Me.引当.Width = 70
        '
        '倉庫コード
        '
        Me.倉庫コード.HeaderText = "倉庫コード"
        Me.倉庫コード.Name = "倉庫コード"
        Me.倉庫コード.ReadOnly = True
        Me.倉庫コード.Visible = False
        '
        '倉庫
        '
        Me.倉庫.HeaderText = "倉庫"
        Me.倉庫.Name = "倉庫"
        Me.倉庫.ReadOnly = True
        '
        '最終入庫日
        '
        Me.最終入庫日.HeaderText = "最終入庫日"
        Me.最終入庫日.Name = "最終入庫日"
        Me.最終入庫日.ReadOnly = True
        '
        '入出庫種別区分
        '
        Me.入出庫種別区分.HeaderText = "入出庫種別区分"
        Me.入出庫種別区分.Name = "入出庫種別区分"
        Me.入出庫種別区分.ReadOnly = True
        Me.入出庫種別区分.Visible = False
        '
        '入出庫種別
        '
        Me.入出庫種別.HeaderText = "入出庫種別"
        Me.入出庫種別.Name = "入出庫種別"
        Me.入出庫種別.ReadOnly = True
        '
        '現在庫数
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.現在庫数.DefaultCellStyle = DataGridViewCellStyle2
        Me.現在庫数.HeaderText = "現在庫数"
        Me.現在庫数.Name = "現在庫数"
        Me.現在庫数.ReadOnly = True
        '
        '入庫単価
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.入庫単価.DefaultCellStyle = DataGridViewCellStyle3
        Me.入庫単価.HeaderText = "入庫単価"
        Me.入庫単価.Name = "入庫単価"
        Me.入庫単価.ReadOnly = True
        '
        '最終出庫日
        '
        Me.最終出庫日.HeaderText = "最終出庫日"
        Me.最終出庫日.Name = "最終出庫日"
        Me.最終出庫日.ReadOnly = True
        '
        '伝票番号
        '
        Me.伝票番号.HeaderText = "伝票番号"
        Me.伝票番号.Name = "伝票番号"
        Me.伝票番号.ReadOnly = True
        Me.伝票番号.Visible = False
        '
        '行番号
        '
        Me.行番号.HeaderText = "行番号"
        Me.行番号.Name = "行番号"
        Me.行番号.ReadOnly = True
        Me.行番号.Visible = False
        '
        '単位
        '
        Me.単位.HeaderText = "単位"
        Me.単位.Name = "単位"
        Me.単位.ReadOnly = True
        Me.単位.Visible = False
        '
        'ロケ番号
        '
        Me.ロケ番号.HeaderText = "ロケ番号"
        Me.ロケ番号.Name = "ロケ番号"
        Me.ロケ番号.ReadOnly = True
        Me.ロケ番号.Visible = False
        '
        '入庫番号
        '
        Me.入庫番号.HeaderText = "入庫番号"
        Me.入庫番号.Name = "入庫番号"
        Me.入庫番号.ReadOnly = True
        Me.入庫番号.Visible = False
        '
        '入庫行番号
        '
        Me.入庫行番号.HeaderText = "入庫行番号"
        Me.入庫行番号.Name = "入庫行番号"
        Me.入庫行番号.ReadOnly = True
        Me.入庫行番号.Visible = False
        '
        'StockSearch
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(714, 448)
        Me.Controls.Add(Me.BtnSelect)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.DgvList)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "StockSearch"
        Me.Text = "StockSearch"
        CType(Me.DgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnBack As Button
    Friend WithEvents DgvList As DataGridView
    Friend WithEvents BtnSelect As Button
    Friend WithEvents 引当 As DataGridViewTextBoxColumn
    Friend WithEvents 倉庫コード As DataGridViewTextBoxColumn
    Friend WithEvents 倉庫 As DataGridViewTextBoxColumn
    Friend WithEvents 最終入庫日 As DataGridViewTextBoxColumn
    Friend WithEvents 入出庫種別区分 As DataGridViewTextBoxColumn
    Friend WithEvents 入出庫種別 As DataGridViewTextBoxColumn
    Friend WithEvents 現在庫数 As DataGridViewTextBoxColumn
    Friend WithEvents 入庫単価 As DataGridViewTextBoxColumn
    Friend WithEvents 最終出庫日 As DataGridViewTextBoxColumn
    Friend WithEvents 伝票番号 As DataGridViewTextBoxColumn
    Friend WithEvents 行番号 As DataGridViewTextBoxColumn
    Friend WithEvents 単位 As DataGridViewTextBoxColumn
    Friend WithEvents ロケ番号 As DataGridViewTextBoxColumn
    Friend WithEvents 入庫番号 As DataGridViewTextBoxColumn
    Friend WithEvents 入庫行番号 As DataGridViewTextBoxColumn
End Class
