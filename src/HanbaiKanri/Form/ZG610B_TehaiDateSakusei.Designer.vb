<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ZG610B_TehaiDateSakusei
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ZG610B_TehaiDateSakusei))
        Me.Label13 = New System.Windows.Forms.Label
        Me.lblZNyuuryokuKensu = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.lblJikkouDate = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.lblKeikakuDate = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblSyoriDate = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblZSyuturyokuKensu = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.lblKonkaiKensu = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.btnJikkou = New System.Windows.Forms.Button
        Me.btnModoru = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.Location = New System.Drawing.Point(402, 125)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(24, 22)
        Me.Label13.TabIndex = 673
        Me.Label13.Text = "件"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblZNyuuryokuKensu
        '
        Me.lblZNyuuryokuKensu.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblZNyuuryokuKensu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblZNyuuryokuKensu.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZNyuuryokuKensu.Location = New System.Drawing.Point(316, 125)
        Me.lblZNyuuryokuKensu.Name = "lblZNyuuryokuKensu"
        Me.lblZNyuuryokuKensu.Size = New System.Drawing.Size(80, 22)
        Me.lblZNyuuryokuKensu.TabIndex = 672
        Me.lblZNyuuryokuKensu.Text = "245"
        Me.lblZNyuuryokuKensu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(240, 125)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(70, 22)
        Me.Label9.TabIndex = 671
        Me.Label9.Text = "入力件数"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblJikkouDate
        '
        Me.lblJikkouDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblJikkouDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblJikkouDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblJikkouDate.Location = New System.Drawing.Point(316, 90)
        Me.lblJikkouDate.Name = "lblJikkouDate"
        Me.lblJikkouDate.Size = New System.Drawing.Size(200, 22)
        Me.lblJikkouDate.TabIndex = 670
        Me.lblJikkouDate.Text = "2010/07/04 11:23:03"
        Me.lblJikkouDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(240, 90)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(70, 22)
        Me.Label7.TabIndex = 669
        Me.Label7.Text = "実行日時"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(30, 90)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(112, 22)
        Me.Label5.TabIndex = 668
        Me.Label5.Text = "前回実行情報"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label17.Location = New System.Drawing.Point(220, 30)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(70, 22)
        Me.Label17.TabIndex = 667
        Me.Label17.Text = "計画年月"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblKeikakuDate
        '
        Me.lblKeikakuDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblKeikakuDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblKeikakuDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKeikakuDate.Location = New System.Drawing.Point(296, 30)
        Me.lblKeikakuDate.Name = "lblKeikakuDate"
        Me.lblKeikakuDate.Size = New System.Drawing.Size(80, 22)
        Me.lblKeikakuDate.TabIndex = 666
        Me.lblKeikakuDate.Text = "2010/09"
        Me.lblKeikakuDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(30, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 22)
        Me.Label1.TabIndex = 665
        Me.Label1.Text = "処理年月"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSyoriDate
        '
        Me.lblSyoriDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblSyoriDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSyoriDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSyoriDate.Location = New System.Drawing.Point(106, 30)
        Me.lblSyoriDate.Name = "lblSyoriDate"
        Me.lblSyoriDate.Size = New System.Drawing.Size(80, 22)
        Me.lblSyoriDate.TabIndex = 664
        Me.lblSyoriDate.Text = "2010/08"
        Me.lblSyoriDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(402, 158)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(24, 22)
        Me.Label2.TabIndex = 676
        Me.Label2.Text = "件"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblZSyuturyokuKensu
        '
        Me.lblZSyuturyokuKensu.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblZSyuturyokuKensu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblZSyuturyokuKensu.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZSyuturyokuKensu.Location = New System.Drawing.Point(316, 158)
        Me.lblZSyuturyokuKensu.Name = "lblZSyuturyokuKensu"
        Me.lblZSyuturyokuKensu.Size = New System.Drawing.Size(80, 22)
        Me.lblZSyuturyokuKensu.TabIndex = 675
        Me.lblZSyuturyokuKensu.Text = "357"
        Me.lblZSyuturyokuKensu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(240, 158)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(70, 22)
        Me.Label4.TabIndex = 674
        Me.Label4.Text = "出力件数"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(402, 202)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(24, 22)
        Me.Label11.TabIndex = 680
        Me.Label11.Text = "件"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblKonkaiKensu
        '
        Me.lblKonkaiKensu.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblKonkaiKensu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblKonkaiKensu.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKonkaiKensu.Location = New System.Drawing.Point(316, 202)
        Me.lblKonkaiKensu.Name = "lblKonkaiKensu"
        Me.lblKonkaiKensu.Size = New System.Drawing.Size(80, 22)
        Me.lblKonkaiKensu.TabIndex = 679
        Me.lblKonkaiKensu.Text = "245"
        Me.lblKonkaiKensu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label14.Location = New System.Drawing.Point(240, 202)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(70, 22)
        Me.Label14.TabIndex = 678
        Me.Label14.Text = "入力件数"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(30, 202)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(112, 22)
        Me.Label6.TabIndex = 677
        Me.Label6.Text = "今回実行情報"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnJikkou
        '
        Me.btnJikkou.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnJikkou.Location = New System.Drawing.Point(250, 251)
        Me.btnJikkou.Name = "btnJikkou"
        Me.btnJikkou.Size = New System.Drawing.Size(130, 45)
        Me.btnJikkou.TabIndex = 0
        Me.btnJikkou.Text = "実行(&S)"
        Me.btnJikkou.UseVisualStyleBackColor = True
        '
        'btnModoru
        '
        Me.btnModoru.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnModoru.Location = New System.Drawing.Point(386, 251)
        Me.btnModoru.Name = "btnModoru"
        Me.btnModoru.Size = New System.Drawing.Size(130, 45)
        Me.btnModoru.TabIndex = 1
        Me.btnModoru.Text = "戻る(&B)"
        Me.btnModoru.UseVisualStyleBackColor = True
        '
        'ZG610B_TehaiDateSakusei
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(549, 308)
        Me.Controls.Add(Me.btnJikkou)
        Me.Controls.Add(Me.btnModoru)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.lblKonkaiKensu)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblZSyuturyokuKensu)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.lblZNyuuryokuKensu)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.lblJikkouDate)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.lblKeikakuDate)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblSyoriDate)
        Me.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.Name = "ZG610B_TehaiDateSakusei"
        Me.Text = "手配データ作成"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblZNyuuryokuKensu As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblJikkouDate As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents lblKeikakuDate As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblSyoriDate As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblZSyuturyokuKensu As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblKonkaiKensu As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnJikkou As System.Windows.Forms.Button
    Friend WithEvents btnModoru As System.Windows.Forms.Button
End Class
