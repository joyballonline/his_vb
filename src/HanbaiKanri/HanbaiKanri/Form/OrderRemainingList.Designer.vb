﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class OrderRemainingList
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.DgvCymndt = New System.Windows.Forms.DataGridView()
        Me.LblMode = New System.Windows.Forms.Label()
        Me.BtnExcelOutput = New System.Windows.Forms.Button()
        Me.受注番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.受注番号枝番 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.行番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.受注日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.得意先名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.メーカー = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.品名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.型式 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.数量 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.単位 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.通貨 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.単価 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ＶＡＴ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.計 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.受注残数 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.備考 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DgvCymndt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBack.Location = New System.Drawing.Point(1174, 509)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(165, 40)
        Me.BtnBack.TabIndex = 23
        Me.BtnBack.Text = "戻る"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'DgvCymndt
        '
        Me.DgvCymndt.AllowUserToAddRows = False
        Me.DgvCymndt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvCymndt.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.受注番号, Me.受注番号枝番, Me.行番号, Me.受注日, Me.得意先名, Me.メーカー, Me.品名, Me.型式, Me.数量, Me.単位, Me.通貨, Me.単価, Me.ＶＡＴ, Me.計, Me.受注残数, Me.備考})
        Me.DgvCymndt.Location = New System.Drawing.Point(13, 51)
        Me.DgvCymndt.Name = "DgvCymndt"
        Me.DgvCymndt.ReadOnly = True
        Me.DgvCymndt.RowHeadersVisible = False
        Me.DgvCymndt.RowTemplate.Height = 21
        Me.DgvCymndt.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvCymndt.Size = New System.Drawing.Size(1326, 444)
        Me.DgvCymndt.TabIndex = 15
        '
        'LblMode
        '
        Me.LblMode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMode.Font = New System.Drawing.Font("MS Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMode.Location = New System.Drawing.Point(1106, 9)
        Me.LblMode.Name = "LblMode"
        Me.LblMode.Size = New System.Drawing.Size(233, 22)
        Me.LblMode.TabIndex = 67
        Me.LblMode.Text = "モード"
        Me.LblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnExcelOutput
        '
        Me.BtnExcelOutput.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnExcelOutput.Location = New System.Drawing.Point(1003, 509)
        Me.BtnExcelOutput.Name = "BtnExcelOutput"
        Me.BtnExcelOutput.Size = New System.Drawing.Size(165, 40)
        Me.BtnExcelOutput.TabIndex = 22
        Me.BtnExcelOutput.Text = "Excel出力"
        Me.BtnExcelOutput.UseVisualStyleBackColor = True
        '
        '受注番号
        '
        Me.受注番号.HeaderText = "受注番号"
        Me.受注番号.MaxInputLength = 14
        Me.受注番号.Name = "受注番号"
        Me.受注番号.ReadOnly = True
        '
        '受注番号枝番
        '
        Me.受注番号枝番.HeaderText = "受注番号枝番"
        Me.受注番号枝番.MaxInputLength = 2
        Me.受注番号枝番.Name = "受注番号枝番"
        Me.受注番号枝番.ReadOnly = True
        Me.受注番号枝番.Visible = False
        '
        '行番号
        '
        Me.行番号.HeaderText = "行番号"
        Me.行番号.MaxInputLength = 3
        Me.行番号.Name = "行番号"
        Me.行番号.ReadOnly = True
        Me.行番号.Visible = False
        '
        '受注日
        '
        Me.受注日.HeaderText = "受注日"
        Me.受注日.Name = "受注日"
        Me.受注日.ReadOnly = True
        '
        '得意先名
        '
        Me.得意先名.HeaderText = "得意先名"
        Me.得意先名.MaxInputLength = 100
        Me.得意先名.Name = "得意先名"
        Me.得意先名.ReadOnly = True
        '
        'メーカー
        '
        Me.メーカー.HeaderText = "メーカー"
        Me.メーカー.MaxInputLength = 50
        Me.メーカー.Name = "メーカー"
        Me.メーカー.ReadOnly = True
        '
        '品名
        '
        Me.品名.HeaderText = "品名"
        Me.品名.MaxInputLength = 50
        Me.品名.Name = "品名"
        Me.品名.ReadOnly = True
        '
        '型式
        '
        Me.型式.HeaderText = "型式"
        Me.型式.MaxInputLength = 255
        Me.型式.Name = "型式"
        Me.型式.ReadOnly = True
        '
        '数量
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.数量.DefaultCellStyle = DataGridViewCellStyle1
        Me.数量.HeaderText = "数量"
        Me.数量.MaxInputLength = 8
        Me.数量.Name = "数量"
        Me.数量.ReadOnly = True
        '
        '単位
        '
        Me.単位.HeaderText = "単位"
        Me.単位.MaxInputLength = 10
        Me.単位.Name = "単位"
        Me.単位.ReadOnly = True
        '
        '通貨
        '
        Me.通貨.HeaderText = "通貨"
        Me.通貨.Name = "通貨"
        Me.通貨.ReadOnly = True
        '
        '単価
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.単価.DefaultCellStyle = DataGridViewCellStyle2
        Me.単価.HeaderText = "単価"
        Me.単価.MaxInputLength = 10
        Me.単価.Name = "単価"
        Me.単価.ReadOnly = True
        '
        'ＶＡＴ
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.ＶＡＴ.DefaultCellStyle = DataGridViewCellStyle3
        Me.ＶＡＴ.HeaderText = "ＶＡＴ"
        Me.ＶＡＴ.MaxInputLength = 14
        Me.ＶＡＴ.Name = "ＶＡＴ"
        Me.ＶＡＴ.ReadOnly = True
        '
        '計
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.計.DefaultCellStyle = DataGridViewCellStyle4
        Me.計.HeaderText = "計"
        Me.計.MaxInputLength = 14
        Me.計.Name = "計"
        Me.計.ReadOnly = True
        '
        '受注残数
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.受注残数.DefaultCellStyle = DataGridViewCellStyle5
        Me.受注残数.HeaderText = "受注残数"
        Me.受注残数.MaxInputLength = 8
        Me.受注残数.Name = "受注残数"
        Me.受注残数.ReadOnly = True
        '
        '備考
        '
        Me.備考.HeaderText = "備考"
        Me.備考.MaxInputLength = 255
        Me.備考.Name = "備考"
        Me.備考.ReadOnly = True
        '
        'OrderRemainingList
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.LblMode)
        Me.Controls.Add(Me.BtnExcelOutput)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.DgvCymndt)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "OrderRemainingList"
        Me.Text = "OrderRemainingList"
        CType(Me.DgvCymndt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnBack As Button
    Friend WithEvents DgvCymndt As DataGridView
    Friend WithEvents LblMode As Label
    Friend WithEvents BtnExcelOutput As Button
    Friend WithEvents VAT As DataGridViewTextBoxColumn
    Friend WithEvents 受注番号 As DataGridViewTextBoxColumn
    Friend WithEvents 受注番号枝番 As DataGridViewTextBoxColumn
    Friend WithEvents 行番号 As DataGridViewTextBoxColumn
    Friend WithEvents 受注日 As DataGridViewTextBoxColumn
    Friend WithEvents 得意先名 As DataGridViewTextBoxColumn
    Friend WithEvents メーカー As DataGridViewTextBoxColumn
    Friend WithEvents 品名 As DataGridViewTextBoxColumn
    Friend WithEvents 型式 As DataGridViewTextBoxColumn
    Friend WithEvents 数量 As DataGridViewTextBoxColumn
    Friend WithEvents 単位 As DataGridViewTextBoxColumn
    Friend WithEvents 通貨 As DataGridViewTextBoxColumn
    Friend WithEvents 単価 As DataGridViewTextBoxColumn
    Friend WithEvents ＶＡＴ As DataGridViewTextBoxColumn
    Friend WithEvents 計 As DataGridViewTextBoxColumn
    Friend WithEvents 受注残数 As DataGridViewTextBoxColumn
    Friend WithEvents 備考 As DataGridViewTextBoxColumn
End Class
