<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ZM311S_HanyouMstHensyuu
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ZM311S_HanyouMstHensyuu))
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblKoteiKey = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtKoumokumei = New System.Windows.Forms.TextBox
        Me.txtKoumokuSetumei = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnTouroku = New System.Windows.Forms.Button
        Me.btnModoru = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(20, 25)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 22)
        Me.Label4.TabIndex = 111
        Me.Label4.Text = "固定キー"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblKoteiKey
        '
        Me.lblKoteiKey.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblKoteiKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblKoteiKey.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKoteiKey.Location = New System.Drawing.Point(20, 46)
        Me.lblKoteiKey.Name = "lblKoteiKey"
        Me.lblKoteiKey.Size = New System.Drawing.Size(90, 22)
        Me.lblKoteiKey.TabIndex = 110
        Me.lblKoteiKey.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(125, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(160, 22)
        Me.Label1.TabIndex = 113
        Me.Label1.Text = "項目名"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtKoumokumei
        '
        Me.txtKoumokumei.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKoumokumei.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtKoumokumei.Location = New System.Drawing.Point(125, 46)
        Me.txtKoumokumei.MaxLength = 255
        Me.txtKoumokumei.Name = "txtKoumokumei"
        Me.txtKoumokumei.Size = New System.Drawing.Size(160, 22)
        Me.txtKoumokumei.TabIndex = 0
        '
        'txtKoumokuSetumei
        '
        Me.txtKoumokuSetumei.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtKoumokuSetumei.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtKoumokuSetumei.Location = New System.Drawing.Point(20, 104)
        Me.txtKoumokuSetumei.Multiline = True
        Me.txtKoumokuSetumei.Name = "txtKoumokuSetumei"
        Me.txtKoumokuSetumei.Size = New System.Drawing.Size(400, 200)
        Me.txtKoumokuSetumei.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(20, 83)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(400, 22)
        Me.Label2.TabIndex = 115
        Me.Label2.Text = "項目説明"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnTouroku
        '
        Me.btnTouroku.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnTouroku.Location = New System.Drawing.Point(155, 320)
        Me.btnTouroku.Name = "btnTouroku"
        Me.btnTouroku.Size = New System.Drawing.Size(130, 45)
        Me.btnTouroku.TabIndex = 2
        Me.btnTouroku.Text = "登録(&E)"
        Me.btnTouroku.UseVisualStyleBackColor = True
        '
        'btnModoru
        '
        Me.btnModoru.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnModoru.Location = New System.Drawing.Point(290, 320)
        Me.btnModoru.Name = "btnModoru"
        Me.btnModoru.Size = New System.Drawing.Size(130, 45)
        Me.btnModoru.TabIndex = 3
        Me.btnModoru.Text = "戻る(&B)"
        Me.btnModoru.UseVisualStyleBackColor = True
        '
        'ZM311S_HanyouMstHensyuu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(439, 378)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblKoteiKey)
        Me.Controls.Add(Me.btnTouroku)
        Me.Controls.Add(Me.btnModoru)
        Me.Controls.Add(Me.txtKoumokuSetumei)
        Me.Controls.Add(Me.txtKoumokumei)
        Me.Controls.Add(Me.Label4)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "ZM311S_HanyouMstHensyuu"
        Me.Text = "汎用マスタ　マスタ項目編集"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblKoteiKey As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtKoumokumei As System.Windows.Forms.TextBox
    Friend WithEvents txtKoumokuSetumei As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnTouroku As System.Windows.Forms.Button
    Friend WithEvents btnModoru As System.Windows.Forms.Button
End Class
