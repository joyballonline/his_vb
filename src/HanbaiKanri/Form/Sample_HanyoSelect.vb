'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）メニュー画面
'    （フォームID）ZC110M_Menu
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   高木        2010/10/15                 新規              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls

Public Class Sample_HanyoSelect

    Dim clickIndex As Integer
    'Public Sub New(ByVal clickIndex As Integer)
    '    'Form1から受け取ったデータをForm2インスタンスのメンバに格納
    '    Me.clickIndex = clickIndex

    '    'InitializeComponent()

    '    Me.Label21 = New System.Windows.Forms.Label()

    '    If Me.clickIndex = 4 Then
    '        Label21.Text = "税"
    '    ElseIf Me.clickIndex = 8 Then
    '        Label21.Text = "単位"
    '    End If

    'End Sub


    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        Me.Hide()
    End Sub

    Private Sub btnModoru_Click(sender As Object, e As EventArgs) Handles btnModoru.Click
        Me.Hide()
    End Sub
End Class