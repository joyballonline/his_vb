<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class InitialRegistrationBulk
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
        Me.BtnRegist = New System.Windows.Forms.Button()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.BtnRegist2 = New System.Windows.Forms.Button()
        Me.BtnRegist3 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'BtnRegist
        '
        Me.BtnRegist.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnRegist.Location = New System.Drawing.Point(281, 68)
        Me.BtnRegist.Name = "BtnRegist"
        Me.BtnRegist.Size = New System.Drawing.Size(165, 40)
        Me.BtnRegist.TabIndex = 9
        Me.BtnRegist.Text = "在庫登録"
        Me.BtnRegist.UseVisualStyleBackColor = True
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(503, 426)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 10
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'BtnRegist2
        '
        Me.BtnRegist2.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnRegist2.Location = New System.Drawing.Point(281, 148)
        Me.BtnRegist2.Name = "BtnRegist2"
        Me.BtnRegist2.Size = New System.Drawing.Size(165, 40)
        Me.BtnRegist2.TabIndex = 11
        Me.BtnRegist2.Text = "請求残高登録"
        Me.BtnRegist2.UseVisualStyleBackColor = True
        '
        'BtnRegist3
        '
        Me.BtnRegist3.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnRegist3.Location = New System.Drawing.Point(281, 230)
        Me.BtnRegist3.Name = "BtnRegist3"
        Me.BtnRegist3.Size = New System.Drawing.Size(165, 40)
        Me.BtnRegist3.TabIndex = 12
        Me.BtnRegist3.Text = "買掛残高登録"
        Me.BtnRegist3.UseVisualStyleBackColor = True
        '
        'InitialRegistrationBulk
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(694, 490)
        Me.Controls.Add(Me.BtnRegist3)
        Me.Controls.Add(Me.BtnRegist2)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.BtnRegist)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "InitialRegistrationBulk"
        Me.Text = "InitialRegistrationBulk"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BtnRegist As Button
    Friend WithEvents BtnBack As Button
    Friend WithEvents BtnRegist2 As Button
    Friend WithEvents BtnRegist3 As Button
End Class
