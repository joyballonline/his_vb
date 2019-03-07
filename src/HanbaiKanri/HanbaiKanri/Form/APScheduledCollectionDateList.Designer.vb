<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class APScheduledCollectionDateList
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
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.DgvCymndt = New System.Windows.Forms.DataGridView()
        Me.LblMode = New System.Windows.Forms.Label()
        Me.BtnExcelOutput = New System.Windows.Forms.Button()
        Me.支払期日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.仕入先名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.発注番号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛日 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛金額計 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.支払金額計 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.買掛金残高 = New System.Windows.Forms.DataGridViewTextBoxColumn()
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
        Me.DgvCymndt.AllowUserToDeleteRows = False
        Me.DgvCymndt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvCymndt.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.支払期日, Me.仕入先名, Me.発注番号, Me.買掛日, Me.買掛金額計, Me.支払金額計, Me.買掛金残高, Me.備考})
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
        Me.LblMode.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
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
        '支払期日
        '
        Me.支払期日.HeaderText = "支払期日"
        Me.支払期日.Name = "支払期日"
        Me.支払期日.ReadOnly = True
        '
        '仕入先名
        '
        Me.仕入先名.HeaderText = "仕入先名"
        Me.仕入先名.Name = "仕入先名"
        Me.仕入先名.ReadOnly = True
        '
        '発注番号
        '
        Me.発注番号.HeaderText = "発注番号"
        Me.発注番号.Name = "発注番号"
        Me.発注番号.ReadOnly = True
        '
        '買掛日
        '
        Me.買掛日.HeaderText = "買掛日"
        Me.買掛日.Name = "買掛日"
        Me.買掛日.ReadOnly = True
        '
        '買掛金額計
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.買掛金額計.DefaultCellStyle = DataGridViewCellStyle1
        Me.買掛金額計.HeaderText = "買掛金額計"
        Me.買掛金額計.Name = "買掛金額計"
        Me.買掛金額計.ReadOnly = True
        '
        '支払金額計
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.支払金額計.DefaultCellStyle = DataGridViewCellStyle2
        Me.支払金額計.HeaderText = "支払金額計"
        Me.支払金額計.Name = "支払金額計"
        Me.支払金額計.ReadOnly = True
        '
        '買掛金残高
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.買掛金残高.DefaultCellStyle = DataGridViewCellStyle3
        Me.買掛金残高.HeaderText = "買掛金残高"
        Me.買掛金残高.Name = "買掛金残高"
        Me.買掛金残高.ReadOnly = True
        '
        '備考
        '
        Me.備考.HeaderText = "備考"
        Me.備考.Name = "備考"
        Me.備考.ReadOnly = True
        '
        'APScheduledCollectionDateList
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(1350, 561)
        Me.Controls.Add(Me.LblMode)
        Me.Controls.Add(Me.BtnExcelOutput)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.DgvCymndt)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "APScheduledCollectionDateList"
        Me.Text = "ARScheduledCollectionDateList"
        CType(Me.DgvCymndt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnBack As Button
    Friend WithEvents DgvCymndt As DataGridView
    Friend WithEvents LblMode As Label
    Friend WithEvents BtnExcelOutput As Button
    Friend WithEvents VAT As DataGridViewTextBoxColumn
    Friend WithEvents 支払期日 As DataGridViewTextBoxColumn
    Friend WithEvents 仕入先名 As DataGridViewTextBoxColumn
    Friend WithEvents 発注番号 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛日 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛金額計 As DataGridViewTextBoxColumn
    Friend WithEvents 支払金額計 As DataGridViewTextBoxColumn
    Friend WithEvents 買掛金残高 As DataGridViewTextBoxColumn
    Friend WithEvents 備考 As DataGridViewTextBoxColumn
End Class
