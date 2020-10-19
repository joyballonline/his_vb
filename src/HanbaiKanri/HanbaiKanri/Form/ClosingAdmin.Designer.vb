<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ClosingAdmin
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
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.LblMode = New System.Windows.Forms.Label()
        Me.BtnRegistration = New System.Windows.Forms.Button()
        Me.LblShime = New System.Windows.Forms.Label()
        Me.dtmSime = New System.Windows.Forms.DateTimePicker()
        Me.BtnOutput = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(228, 205)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 2
        Me.BtnBack.Text = "閉じる"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'LblMode
        '
        Me.LblMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMode.Font = New System.Drawing.Font("MS Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMode.Location = New System.Drawing.Point(254, 9)
        Me.LblMode.Name = "LblMode"
        Me.LblMode.Size = New System.Drawing.Size(165, 22)
        Me.LblMode.TabIndex = 67
        Me.LblMode.Text = "管理者モード"
        Me.LblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnRegistration
        '
        Me.BtnRegistration.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnRegistration.Location = New System.Drawing.Point(40, 204)
        Me.BtnRegistration.Name = "BtnRegistration"
        Me.BtnRegistration.Size = New System.Drawing.Size(165, 40)
        Me.BtnRegistration.TabIndex = 1
        Me.BtnRegistration.Text = "締処理実行"
        Me.BtnRegistration.UseVisualStyleBackColor = True
        '
        'LblShime
        '
        Me.LblShime.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblShime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblShime.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblShime.Location = New System.Drawing.Point(93, 92)
        Me.LblShime.Name = "LblShime"
        Me.LblShime.Size = New System.Drawing.Size(112, 23)
        Me.LblShime.TabIndex = 329
        Me.LblShime.Text = "締処理日"
        Me.LblShime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtmSime
        '
        Me.dtmSime.CalendarFont = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtmSime.CustomFormat = ""
        Me.dtmSime.Font = New System.Drawing.Font("MS Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dtmSime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtmSime.Location = New System.Drawing.Point(211, 93)
        Me.dtmSime.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.dtmSime.Name = "dtmSime"
        Me.dtmSime.Size = New System.Drawing.Size(155, 22)
        Me.dtmSime.TabIndex = 0
        Me.dtmSime.Value = New Date(2019, 1, 1, 0, 0, 0, 0)
        '
        'BtnOutput
        '
        Me.BtnOutput.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnOutput.Location = New System.Drawing.Point(40, 155)
        Me.BtnOutput.Name = "BtnOutput"
        Me.BtnOutput.Size = New System.Drawing.Size(165, 40)
        Me.BtnOutput.TabIndex = 330
        Me.BtnOutput.Text = "仕訳出力"
        Me.BtnOutput.UseVisualStyleBackColor = True
        '
        'ClosingAdmin
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(431, 256)
        Me.Controls.Add(Me.BtnOutput)
        Me.Controls.Add(Me.dtmSime)
        Me.Controls.Add(Me.LblShime)
        Me.Controls.Add(Me.BtnRegistration)
        Me.Controls.Add(Me.LblMode)
        Me.Controls.Add(Me.BtnBack)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "ClosingAdmin"
        Me.Text = "ClosingAdmin"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnBack As Button
    Friend WithEvents LblMode As Label
    Friend WithEvents VAT As DataGridViewTextBoxColumn
    Friend WithEvents BtnRegistration As Button
    Friend WithEvents LblShime As Label
    Friend WithEvents dtmSime As DateTimePicker
    Friend WithEvents BtnOutput As Button
End Class
