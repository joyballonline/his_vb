Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class Information
    Inherits System.Windows.Forms.Form

    '------------------------------------------------------------------------------------------------------
    'メンバー定数宣言
    '------------------------------------------------------------------------------------------------------
    'PG制御文字 
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const RS As String = "RecSet"                               'レコードセットテーブル
    Private Const HAIFUN_ID As String = "H@@@@@"
    Private Const HAIFUN_GYOMU1 As String = "-----------"
    Private Const HAIFUN_SHORI As String = "----------------"
    Private Const HAIFUN_SETUMEI As String = "-------------------------------------------"
    Private Const HAIFUN_MYSOUSANICHIJI As String = "---------------"
    Private Const HAIFUN_SOUSA As String = "----------"
    Private Const HAIFUN_ZENKAI As String = "---------------"

    '-------------------------------------------------------------------------------
    '   変数定義
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _langHd As UtilLangHandler
    Private _parentForm As Form
    'Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ

    '-------------------------------------------------------------------------------
    'デフォルトコンストラクタ（隠蔽）
    '-------------------------------------------------------------------------------
    Private Sub New()
        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()
    End Sub

    '-------------------------------------------------------------------------------
    'コンストラクタ　メニューから呼ばれる
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler,
                   ByRef prmRefDbHd As UtilDBIf,
                   ByRef prmRefLang As UtilLangHandler,
                   ByRef prmRefForm As Form
                   )
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm

        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    '初期表示
    Private Sub Hanyo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If frmC01F10_Login.loginValue.Language = "ENG" Then

            BtnBack.Text = "Back"

        End If

        Dim orderRemainingClass = New OrderRemainingList(_msgHd, _db, _langHd, Me)
        Dim orderRemainingData As DataSet = orderRemainingClass.getOrderRemainingList()


        'Dim orderRemainingData As DataSet = getList()

        If orderRemainingData.Tables(RS).Rows.Count > 0 Then
            Dim i As Integer = orderRemainingData.Tables(RS).Rows.Count

            'DgvList.Rows.Add()
            'DgvList.Rows(i).Cells("項目").Value = "受注残"
            'DgvList.Rows(i).Cells("件数").Value = cntOrderRemaining.ToString

            If frmC01F10_Login.loginValue.Language = "ENG" Then

                LbMessage.Text = "There is " & i.ToString & " order backlog"

            Else

                LbMessage.Text = "受注残が " & i.ToString & " 件あります"

            End If



        End If

    End Sub

    '戻るボタン押下
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click

        Dim openForm As Form = Nothing

        Console.WriteLine(_parentForm.Name)

        If _parentForm.Name = "frmC01F10_Login" Then

            openForm = New frmC01F30_Menu(_msgHd, _langHd, _db)
            openForm.Show()
            Me.Dispose()

        Else

            _parentForm.Enabled = True
            _parentForm.Show()
            Me.Dispose()

        End If

    End Sub

End Class