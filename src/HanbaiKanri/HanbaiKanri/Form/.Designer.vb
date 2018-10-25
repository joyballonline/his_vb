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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'BtnSelect
        '
        Me.BtnSelect.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnSelect.Location = New System.Drawing.Point(288, 396)
        Me.BtnSelect.Name = "BtnSelect"
        Me.BtnSelect.Size = New System.Drawing.Size(165, 40)
        Me.BtnSelect.TabIndex = 4
        Me.BtnSelect.Text = "選択"
        Me.BtnSelect.UseVisualStyleBackColor = True
        '
        'LbMaker
        '
        Me.LbMaker.FormattingEnabled = True
        Me.LbMaker.ItemHeight = 12
        Me.LbMaker.Location = New System.Drawing.Point(12, 27)
        Me.LbMaker.Name = "LbMaker"
        Me.LbMaker.Size = New System.Drawing.Size(200, 364)
        Me.LbMaker.TabIndex = 1
        '
        'LbItem
        '
        Me.LbItem.FormattingEnabled = True
        Me.LbItem.ItemHeight = 12
        Me.LbItem.Location = New System.Drawing.Point(218, 27)
        Me.LbItem.Name = "LbItem"
        Me.LbItem.Size = New System.Drawing.Size(200, 364)
        Me.LbItem.TabIndex = 2
        '
        'LbModel
        '
        Me.LbModel.FormattingEnabled = True
        Me.LbModel.ItemHeight = 12
        Me.LbModel.Location = New System.Drawing.Point(424, 27)
        Me.LbModel.Name = "LbModel"
        Me.LbModel.Size = New System.Drawing.Size(200, 364)
        Me.LbModel.TabIndex = 3
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(459, 396)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 5
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(9, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 15)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "メーカー"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(215, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 15)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "品名"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(421, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 15)
        Me.Label3.TabIndex = 29
        Me.Label3.Text = "型式"
        '
        'MakerSearch
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(636, 448)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.LbModel)
        Me.Controls.Add(Me.LbItem)
        Me.Controls.Add(Me.LbMaker)
        Me.Controls.Add(Me.BtnSelect)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "MakerSearch"
        Me.Text = "MakerSearch"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnSelect As Button
    Friend WithEvents LbMaker As ListBox
    Friend WithEvents LbItem As ListBox
    Friend WithEvents LbModel As ListBox
    Friend WithEvents BtnBack As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
End Class
