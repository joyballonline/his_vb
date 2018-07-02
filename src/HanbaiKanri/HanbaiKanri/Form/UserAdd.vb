Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls

Public Class UserAdd
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
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByRef prmRefLang As UtilLangHandler)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示

        _init = True

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dtToday As DateTime = DateTime.Now
        Try
            '注文基本（T10_CYMNHD）の登録
            Dim Sql As String = ""

            Sql = ""
            Sql += "INSERT INTO "
            Sql += "Public."
            Sql += "m02_user("
            Sql += "会社コード, ユーザＩＤ, 氏名, 略名, 備考, 無効フラグ, 権限, 言語, 更新者, 更新日)"
            Sql += "VALUES('"
            Sql += TextBox1.Text
            Sql += "', '"
            Sql += TextBox2.Text
            Sql += "', '"
            Sql += TextBox3.Text
            Sql += "', '"
            Sql += TextBox4.Text
            Sql += "', '"
            Sql += TextBox5.Text
            Sql += "', '"
            Sql += TextBox6.Text
            Sql += "', '"
            Sql += TextBox7.Text
            Sql += "', '"
            Sql += TextBox8.Text
            Sql += "', '"
            Sql += "Admin"
            Sql += "', '"
            Sql += dtToday
            Sql += " ')"
            Sql += "RETURNING 会社コード"
            Sql += ", "
            Sql += "ユーザＩＤ"
            Sql += ", "
            Sql += "氏名"
            Sql += ", "
            Sql += "略名"
            Sql += ", "
            Sql += "備考"
            Sql += ", "
            Sql += "無効フラグ"
            Sql += ", "
            Sql += "権限"
            Sql += ", "
            Sql += "言語"
            Sql += ", "
            Sql += "更新者"
            Sql += ", "
            Sql += "更新日"

            _db.executeDB(Sql)

            Dim frmUM As MstUser
            frmUM = New MstUser(_msgHd, _db, _langHd)
            frmUM.Show()
            Me.Close()

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        Dim MstUser As MstUser
        MstUser = New MstUser(_msgHd, _db, _langHd)
        MstUser.Show()
        Me.Close()
    End Sub
End Class