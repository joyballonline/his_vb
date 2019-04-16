<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmC01F30_Menu
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
    Friend WithEvents btnSelect As Button
    Friend WithEvents cmdExit As Button
    Friend WithEvents chkM01 As CheckBox
    Friend WithEvents chkH07 As CheckBox
    Friend WithEvents chkH05 As CheckBox
    Friend WithEvents chkH04 As CheckBox
    Friend WithEvents chkH02 As CheckBox
    Friend WithEvents chkH01 As CheckBox
    Friend WithEvents dgvLIST As DataGridView
    Friend WithEvents chkH06 As CheckBox
    Friend WithEvents chkH03 As CheckBox
    Friend WithEvents 処理ID As DataGridViewTextBoxColumn
    Friend WithEvents 業務 As DataGridViewTextBoxColumn
    Friend WithEvents 処理名 As DataGridViewTextBoxColumn
    Friend WithEvents 説明 As DataGridViewTextBoxColumn
    Friend WithEvents My前回操作日時 As DataGridViewTextBoxColumn
    Friend WithEvents 操作者 As DataGridViewTextBoxColumn
    Friend WithEvents 前回操作日時 As DataGridViewTextBoxColumn
    Friend WithEvents TabProcessingMenu As TabControl
    Friend WithEvents TabGeneral As TabPage
    Friend WithEvents TabMenu As TabPage
    Friend WithEvents dgvMasterList As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As DataGridViewTextBoxColumn
    Friend WithEvents BtnLogout As Button

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    '<System.Diagnostics.DebuggerStepThrough()> _
    'Private Sub InitializeComponent()
    '    components = New System.ComponentModel.Container
    '    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    '    Me.Text = "Form2"
    'End Sub
End Class
