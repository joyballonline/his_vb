<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ZM130P_InsatuSiji
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ZM130P_InsatuSiji))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.rdoSGaityuu = New System.Windows.Forms.RadioButton
        Me.Label5 = New System.Windows.Forms.Label
        Me.rdoSSubete = New System.Windows.Forms.RadioButton
        Me.rdoSNaisaku = New System.Windows.Forms.RadioButton
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.rdoTTuusin = New System.Windows.Forms.RadioButton
        Me.Label4 = New System.Windows.Forms.Label
        Me.tdoTSubete = New System.Windows.Forms.RadioButton
        Me.rdoTDensen = New System.Windows.Forms.RadioButton
        Me.rdoKeikakuTaisyou = New System.Windows.Forms.RadioButton
        Me.tdoTehaiHenkan = New System.Windows.Forms.RadioButton
        Me.RadioButton1 = New System.Windows.Forms.RadioButton
        Me.btnModoru = New System.Windows.Forms.Button
        Me.btnInsatu = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.rdoKeikakuTaisyou)
        Me.GroupBox1.Controls.Add(Me.tdoTehaiHenkan)
        Me.GroupBox1.Location = New System.Drawing.Point(38, 50)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(461, 313)
        Me.GroupBox1.TabIndex = 1340
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "帳票種類"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.rdoSGaityuu)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.rdoSSubete)
        Me.GroupBox3.Controls.Add(Me.rdoSNaisaku)
        Me.GroupBox3.Location = New System.Drawing.Point(246, 95)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(178, 195)
        Me.GroupBox3.TabIndex = 1344
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "【製作区分】"
        '
        'rdoSGaityuu
        '
        Me.rdoSGaityuu.AutoSize = True
        Me.rdoSGaityuu.Location = New System.Drawing.Point(25, 120)
        Me.rdoSGaityuu.Name = "rdoSGaityuu"
        Me.rdoSGaityuu.Size = New System.Drawing.Size(55, 19)
        Me.rdoSGaityuu.TabIndex = 1343
        Me.rdoSGaityuu.Text = "外注"
        Me.rdoSGaityuu.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(270, 95)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(104, 22)
        Me.Label5.TabIndex = 1342
        Me.Label5.Text = "【製作区分】"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'rdoSSubete
        '
        Me.rdoSSubete.AutoSize = True
        Me.rdoSSubete.Checked = True
        Me.rdoSSubete.Location = New System.Drawing.Point(25, 30)
        Me.rdoSSubete.Name = "rdoSSubete"
        Me.rdoSSubete.Size = New System.Drawing.Size(54, 19)
        Me.rdoSSubete.TabIndex = 1341
        Me.rdoSSubete.TabStop = True
        Me.rdoSSubete.Text = "全て"
        Me.rdoSSubete.UseVisualStyleBackColor = True
        '
        'rdoSNaisaku
        '
        Me.rdoSNaisaku.AutoSize = True
        Me.rdoSNaisaku.Location = New System.Drawing.Point(25, 70)
        Me.rdoSNaisaku.Name = "rdoSNaisaku"
        Me.rdoSNaisaku.Size = New System.Drawing.Size(55, 19)
        Me.rdoSNaisaku.TabIndex = 1327
        Me.rdoSNaisaku.Text = "内作"
        Me.rdoSNaisaku.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rdoTTuusin)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.tdoTSubete)
        Me.GroupBox2.Controls.Add(Me.rdoTDensen)
        Me.GroupBox2.Location = New System.Drawing.Point(30, 95)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(210, 195)
        Me.GroupBox2.TabIndex = 1343
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "【対象部門(製造部門ｺｰﾄﾞ)】"
        '
        'rdoTTuusin
        '
        Me.rdoTTuusin.AutoSize = True
        Me.rdoTTuusin.Location = New System.Drawing.Point(25, 120)
        Me.rdoTTuusin.Name = "rdoTTuusin"
        Me.rdoTTuusin.Size = New System.Drawing.Size(73, 19)
        Me.rdoTTuusin.TabIndex = 1343
        Me.rdoTTuusin.Text = "通信(1)"
        Me.rdoTTuusin.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(270, 95)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 22)
        Me.Label4.TabIndex = 1342
        Me.Label4.Text = "【製作区分】"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tdoTSubete
        '
        Me.tdoTSubete.AutoSize = True
        Me.tdoTSubete.Checked = True
        Me.tdoTSubete.Location = New System.Drawing.Point(25, 30)
        Me.tdoTSubete.Name = "tdoTSubete"
        Me.tdoTSubete.Size = New System.Drawing.Size(54, 19)
        Me.tdoTSubete.TabIndex = 1341
        Me.tdoTSubete.TabStop = True
        Me.tdoTSubete.Text = "全て"
        Me.tdoTSubete.UseVisualStyleBackColor = True
        '
        'rdoTDensen
        '
        Me.rdoTDensen.AutoSize = True
        Me.rdoTDensen.Location = New System.Drawing.Point(25, 70)
        Me.rdoTDensen.Name = "rdoTDensen"
        Me.rdoTDensen.Size = New System.Drawing.Size(73, 19)
        Me.rdoTDensen.TabIndex = 1327
        Me.rdoTDensen.Text = "電線(3)"
        Me.rdoTDensen.UseVisualStyleBackColor = True
        '
        'rdoKeikakuTaisyou
        '
        Me.rdoKeikakuTaisyou.AutoSize = True
        Me.rdoKeikakuTaisyou.Checked = True
        Me.rdoKeikakuTaisyou.Location = New System.Drawing.Point(25, 30)
        Me.rdoKeikakuTaisyou.Name = "rdoKeikakuTaisyou"
        Me.rdoKeikakuTaisyou.Size = New System.Drawing.Size(130, 19)
        Me.rdoKeikakuTaisyou.TabIndex = 1341
        Me.rdoKeikakuTaisyou.TabStop = True
        Me.rdoKeikakuTaisyou.Text = "計画対象品一覧"
        Me.rdoKeikakuTaisyou.UseVisualStyleBackColor = True
        '
        'tdoTehaiHenkan
        '
        Me.tdoTehaiHenkan.AutoSize = True
        Me.tdoTehaiHenkan.Location = New System.Drawing.Point(25, 60)
        Me.tdoTehaiHenkan.Name = "tdoTehaiHenkan"
        Me.tdoTehaiHenkan.Size = New System.Drawing.Size(105, 19)
        Me.tdoTehaiHenkan.TabIndex = 1327
        Me.tdoTehaiHenkan.Text = "手配変換DB"
        Me.tdoTehaiHenkan.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Checked = True
        Me.RadioButton1.Location = New System.Drawing.Point(121, 127)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(14, 13)
        Me.RadioButton1.TabIndex = 1327
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'btnModoru
        '
        Me.btnModoru.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnModoru.Location = New System.Drawing.Point(369, 383)
        Me.btnModoru.Name = "btnModoru"
        Me.btnModoru.Size = New System.Drawing.Size(130, 45)
        Me.btnModoru.TabIndex = 1342
        Me.btnModoru.Text = "戻る(&B)"
        Me.btnModoru.UseVisualStyleBackColor = True
        '
        'btnInsatu
        '
        Me.btnInsatu.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnInsatu.Location = New System.Drawing.Point(233, 383)
        Me.btnInsatu.Name = "btnInsatu"
        Me.btnInsatu.Size = New System.Drawing.Size(130, 45)
        Me.btnInsatu.TabIndex = 1341
        Me.btnInsatu.Text = "印刷(&O)"
        Me.btnInsatu.UseVisualStyleBackColor = True
        '
        'ZM130P_InsatuSiji
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(526, 442)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnModoru)
        Me.Controls.Add(Me.btnInsatu)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.RadioButton1)
        Me.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ZM130P_InsatuSiji"
        Me.Text = "計画対象品マスタ印刷指示"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rdoKeikakuTaisyou As System.Windows.Forms.RadioButton
    Friend WithEvents tdoTehaiHenkan As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents rdoSGaityuu As System.Windows.Forms.RadioButton
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents rdoSSubete As System.Windows.Forms.RadioButton
    Friend WithEvents rdoSNaisaku As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rdoTTuusin As System.Windows.Forms.RadioButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents tdoTSubete As System.Windows.Forms.RadioButton
    Friend WithEvents rdoTDensen As System.Windows.Forms.RadioButton
    Friend WithEvents btnModoru As System.Windows.Forms.Button
    Friend WithEvents btnInsatu As System.Windows.Forms.Button
End Class
