<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MakerSearch
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
        Me.BtnSelect = New System.Windows.Forms.Button()
        Me.LbMaker = New System.Windows.Forms.ListBox()
        Me.LbItem = New System.Windows.Forms.ListBox()
        Me.LbModel = New System.Windows.Forms.ListBox()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'BtnSelect
        '
        Me.BtnSelect.Location = New System.Drawing.Point(549, 418)
        Me.BtnSelect.Name = "BtnSelect"
        Me.BtnSelect.Size = New System.Drawing.Size(75, 23)
        Me.BtnSelect.TabIndex = 1
        Me.BtnSelect.Text = "選択"
        Me.BtnSelect.UseVisualStyleBackColor = True
        '
        'LbMaker
        '
        Me.LbMaker.FormattingEnabled = True
        Me.LbMaker.ItemHeight = 12
        Me.LbMaker.Location = New System.Drawing.Point(12, 12)
        Me.LbMaker.Name = "LbMaker"
        Me.LbMaker.Size = New System.Drawing.Size(200, 400)
        Me.LbMaker.TabIndex = 2
        '
        'LbItem
        '
        Me.LbItem.FormattingEnabled = True
        Me.LbItem.ItemHeight = 12
        Me.LbItem.Location = New System.Drawing.Point(218, 12)
        Me.LbItem.Name = "LbItem"
        Me.LbItem.Size = New System.Drawing.Size(200, 400)
        Me.LbItem.TabIndex = 3
        '
        'LbModel
        '
        Me.LbModel.FormattingEnabled = True
        Me.LbModel.ItemHeight = 12
        Me.LbModel.Location = New System.Drawing.Point(424, 12)
        Me.LbModel.Name = "LbModel"
        Me.LbModel.Size = New System.Drawing.Size(200, 400)
        Me.LbModel.TabIndex = 4
        '
        'BtnBack
        '
        Me.BtnBack.Location = New System.Drawing.Point(468, 418)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(75, 23)
        Me.BtnBack.TabIndex = 5
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'MakerSearch
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(636, 448)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.LbModel)
        Me.Controls.Add(Me.LbItem)
        Me.Controls.Add(Me.LbMaker)
        Me.Controls.Add(Me.BtnSelect)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "MakerSearch"
        Me.Text = "MakerSearch"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnSelect As Button
    Friend WithEvents LbMaker As ListBox
    Friend WithEvents LbItem As ListBox
    Friend WithEvents LbModel As ListBox
    Friend WithEvents BtnBack As Button
End Class
