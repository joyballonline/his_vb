Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class HanyoAdd
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

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        Dim MstHanyou As MstHanyou
        MstHanyou = New MstHanyou(_msgHd, _db, _langHd)
        MstHanyou.Show()
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        Dim dtToday As DateTime = DateTime.Now
        Try
            Dim Sql As String = ""

            Sql = ""
            Sql += "INSERT INTO "
            Sql += "Public."
            Sql += "m90_hanyo("
            Sql += "会社コード, 固定キー, 可変キー, 表示順, 文字１, 文字２, 文字３, 文字４, 文字５, 文字６, 数値１, 数値２, 数値３, 数値４, 数値５, 数値６, メモ, 更新者, 更新日)"
            Sql += " VALUES('"
            Sql += CompanyCode.Text
            Sql += "', '"
            Sql += FixedKey.Text
            Sql += "', '"
            Sql += VariableKey.Text
            Sql += "', '"
            Sql += DisplayOrder.Text
            Sql += "', '"
            Sql += Char1.Text
            Sql += "', '"
            Sql += Char2.Text
            Sql += "', '"
            Sql += Char3.Text
            Sql += "', '"
            Sql += Char4.Text
            Sql += "', '"
            Sql += Char5.Text
            Sql += "', '"
            Sql += Char6.Text
            Sql += "', '"
            Sql += Num1.Text
            Sql += "', '"
            Sql += Num2.Text
            Sql += "', '"
            Sql += Num3.Text
            Sql += "', '"
            Sql += Num4.Text
            Sql += "', '"
            Sql += Num5.Text
            Sql += "', '"
            Sql += Num6.Text
            Sql += "', '"
            Sql += Memo.Text
            Sql += "', '"
            Sql += "Admin"
            Sql += "', '"
            Sql += dtToday
            Sql += " ')"
            Sql += "RETURNING 会社コード"
            Sql += ", "
            Sql += "固定キー"
            Sql += ", "
            Sql += "可変キー"
            Sql += ", "
            Sql += "表示順"
            Sql += ", "
            Sql += "文字１"
            Sql += ", "
            Sql += "文字２"
            Sql += ", "
            Sql += "文字３"
            Sql += ", "
            Sql += "文字４"
            Sql += ", "
            Sql += "文字５"
            Sql += ", "
            Sql += "文字６"
            Sql += ", "
            Sql += "数値１"
            Sql += ", "
            Sql += "数値２"
            Sql += ", "
            Sql += "数値３"
            Sql += ", "
            Sql += "数値４"
            Sql += ", "
            Sql += "数値５"
            Sql += ", "
            Sql += "数値６"
            Sql += ", "
            Sql += "メモ"
            Sql += ", "
            Sql += "更新者"
            Sql += ", "
            Sql += "更新日"

            _db.executeDB(Sql)

            Dim MstHanyou As MstHanyou
            MstHanyou = New MstHanyou(_msgHd, _db, _langHd)
            MstHanyou.Show()
            Me.Close()

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub
End Class