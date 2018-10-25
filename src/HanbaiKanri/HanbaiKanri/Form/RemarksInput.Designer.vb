<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RemarksInput
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
        Me.TxtRemarks = New System.Windows.Forms.TextBox()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.BtnRemarksInput = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'TxtRemarks
        '
        Me.TxtRemarks.Location = New System.Drawing.Point(12, 12)
        Me.TxtRemarks.Multiline = True
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(336, 289)
        Me.TxtRemarks.TabIndex = 0
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(183, 307)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 7
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'BtnRemarksInput
        '
        Me.BtnRemarksInput.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnRemarksInput.Location = New System.Drawing.Point(12, 307)
        Me.BtnRemarksInput.Name = "BtnRemarksInput"
        Me.BtnRemarksInput.Size = New System.Drawing.Size(165, 40)
        Me.BtnRemarksInput.TabIndex = 6
        Me.BtnRemarksInput.Text = "選択"
        Me.BtnRemarksInput.UseVisualStyleBackColor = True
        '
        'RemarksInput
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(360, 359)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.BtnRemarksInput)
        Me.Controls.Add(Me.TxtRemarks)
        Me.Name = "RemarksInput"
        Me.Text = "RemarksInput"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtRemarks As TextBox
    Friend WithEvents BtnBack As Button
    Friend WithEvents BtnRemarksInput As Button
End Class
