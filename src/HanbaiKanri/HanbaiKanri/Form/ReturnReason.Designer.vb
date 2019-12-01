<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ReturnReason
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.LblRRcode = New System.Windows.Forms.Label()
        Me.TxtReturnReason = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(183, 52)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 43)
        Me.BtnBack.TabIndex = 9
        Me.BtnBack.Text = "Cancel"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'BtnOK
        '
        Me.BtnOK.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnOK.Location = New System.Drawing.Point(12, 52)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(165, 43)
        Me.BtnOK.TabIndex = 8
        Me.BtnOK.Text = "OK"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'LblRRcode
        '
        Me.LblRRcode.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblRRcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblRRcode.Font = New System.Drawing.Font("MS Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblRRcode.Location = New System.Drawing.Point(12, 23)
        Me.LblRRcode.Name = "LblRRcode"
        Me.LblRRcode.Size = New System.Drawing.Size(84, 22)
        Me.LblRRcode.TabIndex = 131
        Me.LblRRcode.Text = "ReturnReason"
        Me.LblRRcode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtReturnReason
        '
        Me.TxtReturnReason.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtReturnReason.Location = New System.Drawing.Point(102, 24)
        Me.TxtReturnReason.Name = "TxtReturnReason"
        Me.TxtReturnReason.Size = New System.Drawing.Size(246, 22)
        Me.TxtReturnReason.TabIndex = 130
        '
        'ReturnReason
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(360, 114)
        Me.Controls.Add(Me.LblRRcode)
        Me.Controls.Add(Me.TxtReturnReason)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.BtnOK)
        Me.Name = "ReturnReason"
        Me.Text = "ReturnReason"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnBack As Button
    Friend WithEvents BtnOK As Button
    Friend WithEvents LblRRcode As Label
    Friend WithEvents TxtReturnReason As TextBox
End Class
