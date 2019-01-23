<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Shiwake
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
        Me.LblMode = New System.Windows.Forms.Label()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.BtnOutput = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'LblMode
        '
        Me.LblMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMode.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMode.Location = New System.Drawing.Point(1175, 5)
        Me.LblMode.Name = "LblMode"
        Me.LblMode.Size = New System.Drawing.Size(165, 22)
        Me.LblMode.TabIndex = 322
        Me.LblMode.Text = "モード"
        Me.LblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(1175, 509)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 323
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'BtnOutput
        '
        Me.BtnOutput.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnOutput.Location = New System.Drawing.Point(1004, 509)
        Me.BtnOutput.Name = "BtnOutput"
        Me.BtnOutput.Size = New System.Drawing.Size(165, 40)
        Me.BtnOutput.TabIndex = 324
        Me.BtnOutput.Text = "出力"
        Me.BtnOutput.UseVisualStyleBackColor = True
        '
        'Shiwake
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.BtnOutput)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.LblMode)
        Me.Name = "Shiwake"
        Me.Text = "Shiwake"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LblMode As Label
    Friend WithEvents BtnBack As Button
    Friend WithEvents BtnOutput As Button
End Class
