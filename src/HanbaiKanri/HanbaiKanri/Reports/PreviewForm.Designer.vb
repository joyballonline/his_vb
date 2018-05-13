<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PreviewForm
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
        Me.arvMain = New GrapeCity.ActiveReports.Viewer.Win.Viewer()
        Me.SuspendLayout()
        '
        'arvMain
        '
        Me.arvMain.BackColor = System.Drawing.SystemColors.Control
        Me.arvMain.CurrentPage = 0
        Me.arvMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.arvMain.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.0!)
        Me.arvMain.Location = New System.Drawing.Point(0, 0)
        Me.arvMain.Name = "arvMain"
        Me.arvMain.PreviewPages = 0
        '
        '
        '
        '
        '
        '
        Me.arvMain.Sidebar.ParametersPanel.ContextMenu = Nothing
        Me.arvMain.Sidebar.ParametersPanel.Width = 200
        '
        '
        '
        Me.arvMain.Sidebar.SearchPanel.ContextMenu = Nothing
        Me.arvMain.Sidebar.SearchPanel.Width = 200
        '
        '
        '
        Me.arvMain.Sidebar.ThumbnailsPanel.ContextMenu = Nothing
        Me.arvMain.Sidebar.ThumbnailsPanel.Width = 200
        Me.arvMain.Sidebar.ThumbnailsPanel.Zoom = 0.1R
        '
        '
        '
        Me.arvMain.Sidebar.TocPanel.ContextMenu = Nothing
        Me.arvMain.Sidebar.TocPanel.Expanded = True
        Me.arvMain.Sidebar.TocPanel.Width = 200
        Me.arvMain.Sidebar.Width = 200
        Me.arvMain.Size = New System.Drawing.Size(1184, 861)
        Me.arvMain.TabIndex = 1
        '
        'PreviewForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1184, 861)
        Me.Controls.Add(Me.arvMain)
        Me.Name = "PreviewForm"
        Me.Text = "プレビュー"
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents arvMain As GrapeCity.ActiveReports.Viewer.Win.Viewer
End Class
