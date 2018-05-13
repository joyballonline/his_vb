<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmH01F10_SyoriSentaku
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
        Me.cmdSelectSyukka = New System.Windows.Forms.Button()
        Me.cmdSelectChumonList = New System.Windows.Forms.Button()
        Me.cmdSelectRireki = New System.Windows.Forms.Button()
        Me.btnModoru = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cmdSelectSyukka
        '
        Me.cmdSelectSyukka.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdSelectSyukka.Location = New System.Drawing.Point(12, 21)
        Me.cmdSelectSyukka.Name = "cmdSelectSyukka"
        Me.cmdSelectSyukka.Size = New System.Drawing.Size(450, 33)
        Me.cmdSelectSyukka.TabIndex = 0
        Me.cmdSelectSyukka.Text = "出荷先一覧から選択"
        Me.cmdSelectSyukka.UseVisualStyleBackColor = True
        '
        'cmdSelectChumonList
        '
        Me.cmdSelectChumonList.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdSelectChumonList.Location = New System.Drawing.Point(12, 69)
        Me.cmdSelectChumonList.Name = "cmdSelectChumonList"
        Me.cmdSelectChumonList.Size = New System.Drawing.Size(450, 33)
        Me.cmdSelectChumonList.TabIndex = 1
        Me.cmdSelectChumonList.Text = "注文帳から選択"
        Me.cmdSelectChumonList.UseVisualStyleBackColor = True
        '
        'cmdSelectRireki
        '
        Me.cmdSelectRireki.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdSelectRireki.Location = New System.Drawing.Point(12, 118)
        Me.cmdSelectRireki.Name = "cmdSelectRireki"
        Me.cmdSelectRireki.Size = New System.Drawing.Size(450, 33)
        Me.cmdSelectRireki.TabIndex = 2
        Me.cmdSelectRireki.Text = "注文履歴から選択（複写新規）"
        Me.cmdSelectRireki.UseVisualStyleBackColor = True
        '
        'btnModoru
        '
        Me.btnModoru.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnModoru.Location = New System.Drawing.Point(360, 167)
        Me.btnModoru.Name = "btnModoru"
        Me.btnModoru.Size = New System.Drawing.Size(102, 36)
        Me.btnModoru.TabIndex = 5
        Me.btnModoru.Text = "戻る(&B)"
        Me.btnModoru.UseVisualStyleBackColor = True
        '
        'frmH01F10_SyoriSentaku
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(474, 224)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnModoru)
        Me.Controls.Add(Me.cmdSelectRireki)
        Me.Controls.Add(Me.cmdSelectChumonList)
        Me.Controls.Add(Me.cmdSelectSyukka)
        Me.Name = "frmH01F10_SyoriSentaku"
        Me.Text = "処理選択（H01F10)"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cmdSelectSyukka As Button
    Friend WithEvents cmdSelectChumonList As Button
    Friend WithEvents cmdSelectRireki As Button
    Friend WithEvents btnModoru As Button
End Class
