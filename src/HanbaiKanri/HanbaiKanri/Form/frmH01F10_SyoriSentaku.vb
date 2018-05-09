Option Explicit On

Imports UtilMDL.MSG
Imports UtilMDL.DB

Public Class frmH01F10_SyoriSentaku
    '-------------------------------------------------------------------------------
    '   変数定義
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _SelectID As String
    Private _parentForm As Form                             '親フォーム
    Private _comLogc As CommonLogic                         '共通処理用

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
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByRef prmSelectID As String, ByRef prmParentForm As Form)
        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _SelectID = prmSelectID
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]"                                  'フォームタイトル表示
        ' 共通処理使用の準備
        _comLogc = New CommonLogic(_db, _msgHd)                             ' 共通処理用

        _parentForm = prmParentForm
        ''操作履歴ログ作成
        '_comLogc.Insert_L01_ProcLog(frmC01F10_Login.loginValue.BumonCD, DBNull.Value, _SelectID, CommonConst.PROGRAM_START, CommonConst.STATUS_NORMAL,
        '                                        CommonConst.MODE_ADDNEW_NAME, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value,
        '                                        DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, frmC01F10_Login.loginValue.TantoNM)


    End Sub

    '戻るボタンクリック
    Private Sub btnModoru_Click(sender As Object, e As EventArgs) Handles btnModoru.Click
        'メニュー画面に戻る
        _parentForm.Show()                                              ' 前画面を表示
        _parentForm.Enabled = True                                      ' 前画面の使用を可能にする
        _parentForm.Activate()                                          ' 前画面をアクティブにする

        Me.Dispose()                                                    ' 自画面を閉じる
    End Sub

    '出荷先一覧から選択ボタンクリック
    Private Sub cmdSelectSyukka_Click(sender As Object, e As EventArgs) Handles cmdSelectSyukka.Click

        '出荷先選択フォームオープン
        Dim openForm As New frmH01F20_SelectSyukka(_msgHd, _db, _SelectID, Me)
        openForm.Show()
        Me.Hide()
    End Sub

    Private Sub cmdSelectChumonList_Click(sender As Object, e As EventArgs) Handles cmdSelectChumonList.Click

        '注文帳選択フォームオープン
        Dim openForm As New frmH01F30_SelectChumon(_msgHd, _db, _SelectID, Me)
        openForm.Show()
        Me.Hide()

    End Sub

    Private Sub cmdSelectRireki_Click(sender As Object, e As EventArgs) Handles cmdSelectRireki.Click

        '注文履歴選択フォームオープン
        Dim openForm As New frmH01F50_SelectRireki(_msgHd, _db, _SelectID, Me, CommonConst.MODE_ADDNEW)
        openForm.Show()
        Me.Hide()

    End Sub
End Class