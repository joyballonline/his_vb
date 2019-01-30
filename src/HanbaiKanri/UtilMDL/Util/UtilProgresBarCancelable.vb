'===============================================================================
'
'  ユーティリティクラス
'    （クラス名）    UtilProgresBarCancelable
'    （処理機能名）      途中キャンセル可能なプログレスバー画面機能を提供する
'    （本MDL使用前提）   プロジェクトにUtilProgressBarが取込まれていること
'    （備考）            
'    （使用方法）       'プログレスバー画面を表示
'                       Dim pb As UtilProgressBar = New UtilProgressBar(Me)
'                       pb.Show()
'
'                       'プログレスバー設定
'                       pb.jobName = "出力データを生成しています。"
'                       pb.status = "初期化中．．．"
'
'                       '■実処理
'
'                       'プログレスバー値設定
'                       pb.status = "出力中．．．"
'                       pb.oneStep = 10
'                       pb.maxVal = rtnCnt
'                       For i As Integer = 0 To rtnCnt - 1
'                           pb.value = i 'プログレスバー値設定
'
'                           '■実処理
'                       
'                       Next
'                       
'                       '画面消去
'                       pb.Close()
'
'===============================================================================
'  履歴  名前          日  付      マーク      内容
'-------------------------------------------------------------------------------
'  (1)   Laevigata, Inc.    2010/12/07              新規
'-------------------------------------------------------------------------------
'Public Class UtilProgresBarCancelable

'    '===============================================================================
'    'メンバー変数定義
'    '===============================================================================
'    Private _pb As myProgressBar = Nothing

'    '===============================================================================
'    'プロパティ(アクセサ)
'    '===============================================================================
'    'Windowタイトル
'    Public Property windowTitle() As String
'        Get
'            Return _pb.windowTitle
'        End Get
'        Set(ByVal value As String)
'            _pb.windowTitle = value
'        End Set
'    End Property
'    '処理名
'    Public Property jobName() As String
'        Get
'            Return _pb.jobName
'        End Get
'        Set(ByVal value As String)
'            _pb.jobName = value
'        End Set
'    End Property
'    '処理状態
'    Public Property status() As String
'        Get
'            Return _pb.status
'        End Get
'        Set(ByVal value As String)
'            _pb.status = value
'        End Set
'    End Property
'    '最大値
'    Public Property maxVal() As Integer
'        Get
'            Return _pb.maxVal
'        End Get
'        Set(ByVal value As Integer)
'            _pb.maxVal = value
'        End Set
'    End Property
'    '最小値
'    Public Property minVal() As Integer
'        Get
'            Return _pb.minVal
'        End Get
'        Set(ByVal value As Integer)
'            _pb.minVal = value
'        End Set
'    End Property
'    'ステップ量
'    Public Property oneStep() As Integer
'        Get
'            Return _pb.oneStep
'        End Get
'        Set(ByVal value As Integer)
'            _pb.oneStep = value
'        End Set
'    End Property
'    '進捗状態
'    Public Property value() As Integer
'        Get
'            Return _pb.value
'        End Get
'        Set(ByVal value As Integer)

'            'If _flgCancel Then
'            '    Throw New UsrDefException("キャンセルされました。")
'            'End If

'            _pb.value = value
'        End Set
'    End Property


'    'Private _flgCancel As Boolean = False
'    'Friend Property noticeCancel() As Boolean
'    '    Get
'    '        Return _flgCancel
'    '    End Get
'    '    Set(ByVal value As Boolean)
'    '        _flgCancel = value
'    '    End Set
'    'End Property

'    '-------------------------------------------------------------------------------
'    'コンストラクタ
'    '-------------------------------------------------------------------------------
'    Public Sub New(ByRef prmRefParentForm As Form)
'        _pb = New myProgressBar(prmRefParentForm, Me)
'    End Sub

'    '-------------------------------------------------------------------------------
'    '画面表示
'    '-------------------------------------------------------------------------------
'    Public Sub Show()
'        _pb.Show()
'    End Sub

'    '-------------------------------------------------------------------------------
'    '画面消去
'    '-------------------------------------------------------------------------------
'    Public Sub Close()
'        _pb.Close()
'        _pb = Nothing
'    End Sub

'End Class


'===============================================================================
'
'  ユーティリティクラス
'    （クラス名）    myProgressBar
'    （処理機能名）      キャンセルボタンを保持したプログレスバー画面
'    （本MDL使用前提）   UtilProgresBarCancelableとセットで使用する
'    （備考）            
'===============================================================================
'  履歴  名前          日  付      マーク      内容
'-------------------------------------------------------------------------------
'  (1)   Laevigata, Inc.    2010/12/07              新規
'-------------------------------------------------------------------------------
'Public Class myProgressBar
Public Class UtilProgresBarCancelable
    Inherits UtilProgressBar
    Friend WithEvents btnCancel As System.Windows.Forms.Button

    '   Dim _hd As UtilProgresBarCancelable = Nothing


    Public Sub New(ByRef prmRefParentForm As Form) ', ByRef parentHandler As UtilProgresBarCancelable)
        MyBase.New(prmRefParentForm)

        Application.DoEvents()
        Me.Cursor = Cursors.WaitCursor
        Me.Refresh()

        '        _hd = parentHandler

        'キャンセルボタンの追加
        btnCancel = New System.Windows.Forms.Button
        btnCancel.Location = New System.Drawing.Point(367, 117)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New System.Drawing.Size(101, 23)
        btnCancel.TabIndex = 1
        btnCancel.Text = "キャンセル(&C)"
        btnCancel.UseVisualStyleBackColor = True
        btnCancel.Cursor = Cursors.Default
        Me.Controls.Add(btnCancel)

        Application.DoEvents()
        Me.Refresh()

    End Sub

    Public Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        '_hd.noticeCancel = True
        Throw New UtilProgressBarCancelEx("キャンセル実行")
    End Sub

    'Private Sub btnCancel_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.MouseHover
    '    btnCancel.Cursor = Cursors.Default
    'End Sub
    'Private Sub btnCancel_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.MouseLeave
    '    Me.Cursor
    'End Sub
End Class

Public Class UtilProgressBarCancelEx
    Inherits Exception

    Public Sub New(ByVal prmMessage As String)
        MyBase.New(prmMessage)
    End Sub

End Class

