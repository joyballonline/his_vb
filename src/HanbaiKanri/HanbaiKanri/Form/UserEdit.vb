Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls

Public Class UserEdit
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
    'Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ
    Private srArr() As String
    Private key1 As String
    Private key2 As String

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
                   ByRef prmRefsrArr() As String,
                   ByRef prmRefkey1 As String,
                   ByRef prmRefkey2 As String)
        Call Me.New()

        _init = False

        '初期処理
        srArr = prmRefsrArr
        key1 = prmRefkey1
        key2 = prmRefkey2
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示

        _init = True

    End Sub

    Private Sub UserEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim test(7) As String
        TextBox1.Text = srArr(0)
        TextBox2.Text = srArr(1)
        TextBox3.Text = srArr(2)
        TextBox4.Text = srArr(3)
        TextBox5.Text = srArr(4)
        TextBox6.Text = srArr(5)
        TextBox7.Text = srArr(6)
        TextBox8.Text = srArr(7)
    End Sub

    Private Sub btn_userEdit_Click(sender As Object, e As EventArgs) Handles btn_userEdit.Click
        Dim dtToday As DateTime = DateTime.Now
        Try
            Dim Sql As String = ""

            Sql = ""
            Sql += "UPDATE "
            Sql += "Public."
            Sql += "m02_user "
            Sql += "SET "
            Sql += " 会社コード"
            Sql += " = '"
            Sql += TextBox1.Text
            Sql += "', "
            Sql += "ユーザＩＤ"
            Sql += " = '"
            Sql += TextBox2.Text
            Sql += "', "
            Sql += "氏名"
            Sql += " = '"
            Sql += TextBox3.Text
            Sql += "', "
            Sql += "略名"
            Sql += " = '"
            Sql += TextBox4.Text
            Sql += "', "
            Sql += "備考"
            Sql += " = '"
            Sql += TextBox5.Text
            Sql += "', "
            Sql += "無効フラグ"
            Sql += " = '"
            Sql += TextBox6.Text
            Sql += "', "
            Sql += "権限"
            Sql += " = '"
            Sql += TextBox7.Text
            Sql += "', "
            Sql += "言語"
            Sql += " = '"
            Sql += TextBox8.Text
            Sql += "', "
            Sql += "更新者"
            Sql += " = '"
            Sql += "Admin"
            Sql += "', "
            Sql += "更新日"
            Sql += " = '"
            Sql += dtToday
            Sql += "' "
            Sql += "WHERE"
            Sql += " 会社コード"
            Sql += "='"
            Sql += key1
            Sql += "'"
            Sql += " AND"
            Sql += " ユーザＩＤ"
            Sql += "='"
            Sql += key2
            Sql += "' "
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
            frmUM = New MstUser(_msgHd, _db)
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
        MstUser = New MstUser(_msgHd, _db)
        MstUser.Show()
        Me.Close()
    End Sub
End Class