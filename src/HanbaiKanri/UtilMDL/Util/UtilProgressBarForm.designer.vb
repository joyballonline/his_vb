<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UtilProgressBar
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UtilProgressBar))
        Me.pgbBar = New System.Windows.Forms.ProgressBar
        Me.lblJobName = New System.Windows.Forms.Label
        Me.lblStatus = New System.Windows.Forms.Label
        Me.lblStatusTitle = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pgbBar
        '
        Me.pgbBar.Location = New System.Drawing.Point(96, 91)
        Me.pgbBar.Name = "pgbBar"
        Me.pgbBar.Size = New System.Drawing.Size(372, 20)
        Me.pgbBar.TabIndex = 2
        '
        'lblJobName
        '
        Me.lblJobName.Location = New System.Drawing.Point(94, 20)
        Me.lblJobName.Name = "lblJobName"
        Me.lblJobName.Size = New System.Drawing.Size(374, 20)
        Me.lblJobName.TabIndex = 3
        Me.lblJobName.Text = "コピーしています。"
        '
        'lblStatus
        '
        Me.lblStatus.Location = New System.Drawing.Point(134, 44)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(355, 44)
        Me.lblStatus.TabIndex = 4
        Me.lblStatus.Text = "○○○から□□□へ．．．"
        '
        'lblStatusTitle
        '
        Me.lblStatusTitle.AutoSize = True
        Me.lblStatusTitle.Location = New System.Drawing.Point(94, 44)
        Me.lblStatusTitle.Name = "lblStatusTitle"
        Me.lblStatusTitle.Size = New System.Drawing.Size(35, 12)
        Me.lblStatusTitle.TabIndex = 5
        Me.lblStatusTitle.Text = "状態："
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(30, 19)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        '
        'UtilProgressBar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(501, 144)
        Me.ControlBox = False
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lblStatusTitle)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.lblJobName)
        Me.Controls.Add(Me.pgbBar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "UtilProgressBar"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "処理中です．．．"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pgbBar As System.Windows.Forms.ProgressBar
    Friend WithEvents lblJobName As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents lblStatusTitle As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox

End Class
