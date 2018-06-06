<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ZM410B_ABCBunseki
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ZM410B_ABCBunseki))
        Me.lblZenkaiJikkouDate = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblZenkaiJissekiFrom = New System.Windows.Forms.Label
        Me.lblZenkaiJissekiTo = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.dteKonkaiJissekiFrom = New CustomControl.TextBoxDate
        Me.dteKonkaiJissekiTo = New CustomControl.TextBoxDate
        Me.Label13 = New System.Windows.Forms.Label
        Me.btnJikkou = New System.Windows.Forms.Button
        Me.btnModoru = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblSum = New System.Windows.Forms.Label
        Me.lblS = New System.Windows.Forms.Label
        Me.lblA = New System.Windows.Forms.Label
        Me.lblB = New System.Windows.Forms.Label
        Me.lblC = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lblZenkaiJikkouDate
        '
        Me.lblZenkaiJikkouDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblZenkaiJikkouDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblZenkaiJikkouDate.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZenkaiJikkouDate.Location = New System.Drawing.Point(310, 30)
        Me.lblZenkaiJikkouDate.Name = "lblZenkaiJikkouDate"
        Me.lblZenkaiJikkouDate.Size = New System.Drawing.Size(178, 22)
        Me.lblZenkaiJikkouDate.TabIndex = 112
        Me.lblZenkaiJikkouDate.Text = "2010/07/04 11:23:03"
        Me.lblZenkaiJikkouDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(30, 30)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(130, 22)
        Me.Label4.TabIndex = 113
        Me.Label4.Text = "前回実行情報"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(170, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(130, 22)
        Me.Label1.TabIndex = 114
        Me.Label1.Text = "実行日時"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(170, 70)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(130, 22)
        Me.Label7.TabIndex = 118
        Me.Label7.Text = "実績参照期間"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblZenkaiJissekiFrom
        '
        Me.lblZenkaiJissekiFrom.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblZenkaiJissekiFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblZenkaiJissekiFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZenkaiJissekiFrom.Location = New System.Drawing.Point(310, 70)
        Me.lblZenkaiJissekiFrom.Name = "lblZenkaiJissekiFrom"
        Me.lblZenkaiJissekiFrom.Size = New System.Drawing.Size(70, 22)
        Me.lblZenkaiJissekiFrom.TabIndex = 119
        Me.lblZenkaiJissekiFrom.Text = "2010/04"
        Me.lblZenkaiJissekiFrom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblZenkaiJissekiTo
        '
        Me.lblZenkaiJissekiTo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblZenkaiJissekiTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblZenkaiJissekiTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblZenkaiJissekiTo.Location = New System.Drawing.Point(410, 70)
        Me.lblZenkaiJissekiTo.Name = "lblZenkaiJissekiTo"
        Me.lblZenkaiJissekiTo.Size = New System.Drawing.Size(70, 22)
        Me.lblZenkaiJissekiTo.TabIndex = 120
        Me.lblZenkaiJissekiTo.Text = "2010/06"
        Me.lblZenkaiJissekiTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(386, 70)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(24, 22)
        Me.Label10.TabIndex = 121
        Me.Label10.Text = "～"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(30, 170)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(130, 22)
        Me.Label11.TabIndex = 122
        Me.Label11.Text = "今回実行情報"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.Location = New System.Drawing.Point(170, 170)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(130, 22)
        Me.Label12.TabIndex = 123
        Me.Label12.Text = "実績参照期間"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dteKonkaiJissekiFrom
        '
        Me.dteKonkaiJissekiFrom.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dteKonkaiJissekiFrom.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.dteKonkaiJissekiFrom.Location = New System.Drawing.Point(310, 171)
        Me.dteKonkaiJissekiFrom.Mask = "0000/00"
        Me.dteKonkaiJissekiFrom.Name = "dteKonkaiJissekiFrom"
        Me.dteKonkaiJissekiFrom.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57), Global.Microsoft.VisualBasic.ChrW(47), Global.Microsoft.VisualBasic.ChrW(95)}
        Me.dteKonkaiJissekiFrom.Size = New System.Drawing.Size(70, 22)
        Me.dteKonkaiJissekiFrom.TabIndex = 0
        Me.dteKonkaiJissekiFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'dteKonkaiJissekiTo
        '
        Me.dteKonkaiJissekiTo.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.dteKonkaiJissekiTo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.dteKonkaiJissekiTo.Location = New System.Drawing.Point(410, 170)
        Me.dteKonkaiJissekiTo.Mask = "0000/00"
        Me.dteKonkaiJissekiTo.Name = "dteKonkaiJissekiTo"
        Me.dteKonkaiJissekiTo.PermitChars = New Char() {Global.Microsoft.VisualBasic.ChrW(48), Global.Microsoft.VisualBasic.ChrW(49), Global.Microsoft.VisualBasic.ChrW(50), Global.Microsoft.VisualBasic.ChrW(51), Global.Microsoft.VisualBasic.ChrW(52), Global.Microsoft.VisualBasic.ChrW(53), Global.Microsoft.VisualBasic.ChrW(54), Global.Microsoft.VisualBasic.ChrW(55), Global.Microsoft.VisualBasic.ChrW(56), Global.Microsoft.VisualBasic.ChrW(57), Global.Microsoft.VisualBasic.ChrW(47), Global.Microsoft.VisualBasic.ChrW(95)}
        Me.dteKonkaiJissekiTo.Size = New System.Drawing.Size(70, 22)
        Me.dteKonkaiJissekiTo.TabIndex = 1
        Me.dteKonkaiJissekiTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.Location = New System.Drawing.Point(386, 170)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(24, 22)
        Me.Label13.TabIndex = 126
        Me.Label13.Text = "～"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnJikkou
        '
        Me.btnJikkou.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnJikkou.Location = New System.Drawing.Point(253, 210)
        Me.btnJikkou.Name = "btnJikkou"
        Me.btnJikkou.Size = New System.Drawing.Size(130, 45)
        Me.btnJikkou.TabIndex = 2
        Me.btnJikkou.Text = "実行(&S)"
        Me.btnJikkou.UseVisualStyleBackColor = True
        '
        'btnModoru
        '
        Me.btnModoru.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnModoru.Location = New System.Drawing.Point(389, 210)
        Me.btnModoru.Name = "btnModoru"
        Me.btnModoru.Size = New System.Drawing.Size(130, 45)
        Me.btnModoru.TabIndex = 3
        Me.btnModoru.Text = "戻る(&B)"
        Me.btnModoru.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(173, 111)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(60, 22)
        Me.Label6.TabIndex = 662
        Me.Label6.Text = "合計"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(232, 111)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 22)
        Me.Label2.TabIndex = 663
        Me.Label2.Text = "S"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(291, 111)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 22)
        Me.Label3.TabIndex = 664
        Me.Label3.Text = "A"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(350, 111)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 22)
        Me.Label5.TabIndex = 665
        Me.Label5.Text = "B"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(409, 111)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(60, 22)
        Me.Label8.TabIndex = 666
        Me.Label8.Text = "C"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSum
        '
        Me.lblSum.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblSum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSum.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSum.Location = New System.Drawing.Point(173, 132)
        Me.lblSum.Name = "lblSum"
        Me.lblSum.Size = New System.Drawing.Size(60, 22)
        Me.lblSum.TabIndex = 667
        Me.lblSum.Text = "245"
        Me.lblSum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblS
        '
        Me.lblS.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblS.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblS.Location = New System.Drawing.Point(232, 132)
        Me.lblS.Name = "lblS"
        Me.lblS.Size = New System.Drawing.Size(60, 22)
        Me.lblS.TabIndex = 668
        Me.lblS.Text = "120"
        Me.lblS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblA
        '
        Me.lblA.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblA.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblA.Location = New System.Drawing.Point(291, 132)
        Me.lblA.Name = "lblA"
        Me.lblA.Size = New System.Drawing.Size(60, 22)
        Me.lblA.TabIndex = 669
        Me.lblA.Text = "20"
        Me.lblA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblB
        '
        Me.lblB.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblB.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblB.Location = New System.Drawing.Point(350, 132)
        Me.lblB.Name = "lblB"
        Me.lblB.Size = New System.Drawing.Size(60, 22)
        Me.lblB.TabIndex = 670
        Me.lblB.Text = "30"
        Me.lblB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblC
        '
        Me.lblC.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblC.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblC.Location = New System.Drawing.Point(409, 132)
        Me.lblC.Name = "lblC"
        Me.lblC.Size = New System.Drawing.Size(60, 22)
        Me.lblC.TabIndex = 671
        Me.lblC.Text = "75"
        Me.lblC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ZM410B_ABCBunseki
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(544, 268)
        Me.Controls.Add(Me.lblC)
        Me.Controls.Add(Me.lblB)
        Me.Controls.Add(Me.lblA)
        Me.Controls.Add(Me.lblS)
        Me.Controls.Add(Me.lblSum)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btnJikkou)
        Me.Controls.Add(Me.btnModoru)
        Me.Controls.Add(Me.lblZenkaiJissekiTo)
        Me.Controls.Add(Me.dteKonkaiJissekiTo)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.dteKonkaiJissekiFrom)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.lblZenkaiJissekiFrom)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblZenkaiJikkouDate)
        Me.Controls.Add(Me.Label4)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "ZM410B_ABCBunseki"
        Me.Text = "ABC分析実行指示"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblZenkaiJikkouDate As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblZenkaiJissekiFrom As System.Windows.Forms.Label
    Friend WithEvents lblZenkaiJissekiTo As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents dteKonkaiJissekiFrom As CustomControl.TextBoxDate
    Friend WithEvents dteKonkaiJissekiTo As CustomControl.TextBoxDate
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents btnJikkou As System.Windows.Forms.Button
    Friend WithEvents btnModoru As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblSum As System.Windows.Forms.Label
    Friend WithEvents lblS As System.Windows.Forms.Label
    Friend WithEvents lblA As System.Windows.Forms.Label
    Friend WithEvents lblB As System.Windows.Forms.Label
    Friend WithEvents lblC As System.Windows.Forms.Label
End Class
