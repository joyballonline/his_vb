Imports System.Windows.Forms
'===============================================================================
'
'  ユーティリティクラス
'    （クラス名）    UtilProgressBar
'    （処理機能名）      プログレスバー画面機能を提供する
'    （本MDL使用前提）   特になし
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
'  (1)   Laevigata, Inc.    2006/06/02              新規
'-------------------------------------------------------------------------------
Public Class UtilProgressBar
    Inherits System.Windows.Forms.Form

    '===============================================================================
    'メンバー変数定義
    '===============================================================================
    Private _parentForm As Form
    Private _cur As Cursor = Cursors.Default
    Private _windowTitle As String = ""                 'WindowTitle
    Private _jobName As String = ""                     '処理名称
    Private _status As String = ""                      '処理状態
    Private _maxVal As Integer = 100                    '最大処理ステップ
    Private _minVal As Integer = 0                      '最小処理ステップ
    Private _oneStep As Integer = 2                     'どの単位でステップアップするか
    Private _value As Integer = 0                       '現在の処理ステップ

    '===============================================================================
    'プロパティ(アクセサ)
    '===============================================================================
    'Windowタイトル
    Public Property windowTitle() As String
        Get
            Return _windowTitle
        End Get
        Set(ByVal value As String)
            _windowTitle = value
            Me.Text = _windowTitle
            Application.DoEvents()
            Me.Refresh()
        End Set
    End Property
    '処理名
    Public Property jobName() As String
        Get
            Return _jobName
        End Get
        Set(ByVal value As String)
            _jobName = value
            lblJobName.Text = _jobName
            pgbBar.Value = 0
            Application.DoEvents()
            Me.Refresh()
        End Set
    End Property
    '処理状態
    Public Property status() As String
        Get
            Return _status
        End Get
        Set(ByVal value As String)
            _status = value
            lblStatus.Text = _status
            Application.DoEvents()
            Me.Refresh()
        End Set
    End Property
    '最大値
    Public Property maxVal() As Integer
        Get
            Return _maxVal
        End Get
        Set(ByVal value As Integer)
            _maxVal = value
            pgbBar.Maximum = _maxVal
            Application.DoEvents()
            Me.Refresh()
        End Set
    End Property
    '最小値
    Public Property minVal() As Integer
        Get
            Return _minVal
        End Get
        Set(ByVal value As Integer)
            _minVal = value
            pgbBar.Minimum = _minVal
            Application.DoEvents()
            Me.Refresh()
        End Set
    End Property
    'ステップ量
    Public Property oneStep() As Integer
        Get
            Return _oneStep
        End Get
        Set(ByVal value As Integer)
            _oneStep = value
            pgbBar.Step = _oneStep
            Application.DoEvents()
            Me.Refresh()
        End Set
    End Property
    '進捗状態
    Public Property value() As Integer
        Get
            Return _value
        End Get
        Set(ByVal value As Integer)
            _value = value
            pgbBar.Value = _value
            Application.DoEvents()
            Me.Refresh()
        End Set
    End Property


    '-------------------------------------------------------------------------------
    'コンストラクタ
    '-------------------------------------------------------------------------------
    Private Sub New()
        InitializeComponent()
    End Sub
    Public Sub New(ByRef prmRefParentForm As Form)
        Me.New()

        _windowTitle = Me.Text
        _jobName = ""
        Me.lblJobName.Text = _jobName
        _status = ""
        Me.lblStatus.Text = _status

        _parentForm = prmRefParentForm
        _parentForm.Enabled = False

        Application.DoEvents()
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
    End Sub

    '-------------------------------------------------------------------------------
    'フォームロード
    '-------------------------------------------------------------------------------
    Private Sub UtilProgressBar_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '描画関係の設定
        Me.SetStyle(ControlStyles.ResizeRedraw, True)           'サイズが変更されたときに、コントロールがコントロール自体を再描画するかどうかを示す値を設定
        Me.SetStyle(ControlStyles.DoubleBuffer, True)           '描画はバッファで実行され、完了後に、結果が画面に出力されるよう設定
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  'コントロールは、画面に直接ではなく、まずバッファに描画されます。これにより、ちらつきを抑えることができます。
        Me.SetStyle(ControlStyles.UserPaint, True)              'コントロールは、オペレーティング システムによってではなく、独自に描画されるよう設定
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   'コントロールはウィンドウ メッセージ WM_ERASEBKGND を無視するように設定

        _cur = Me.Cursor
        Application.DoEvents()
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()

        Me.Refresh()
        Application.DoEvents()

    End Sub

    '-------------------------------------------------------------------------------
    'フォームクローズ
    '-------------------------------------------------------------------------------
    Private Sub UtilProgressBar_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Cursor = _cur
        _parentForm.Enabled = True
    End Sub

End Class
