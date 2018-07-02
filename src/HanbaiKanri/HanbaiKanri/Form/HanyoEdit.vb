Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls

Public Class HanyoEdit
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
    Private srArr() As String
    Private key1 As String
    Private key2 As String
    Private key3 As String

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
                   ByRef prmRefsrArr() As String,
                   ByRef prmRefkey1 As String,
                   ByRef prmRefkey2 As String,
                   ByRef prmRefkey3 As String)
        Call Me.New()

        _init = False

        '初期処理
        srArr = prmRefsrArr
        key1 = prmRefkey1
        key2 = prmRefkey2
        key3 = prmRefkey3
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示

        _init = True

    End Sub

    Private Sub SupplierEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CompanyCode.Text = srArr(0)
        FixedKey.Text = srArr(1)
        VariableKey.Text = srArr(2)
        DisplayOrder.Text = srArr(3)
        Char1.Text = srArr(4)
        Char2.Text = srArr(5)
        Char3.Text = srArr(6)
        Char4.Text = srArr(7)
        Char5.Text = srArr(8)
        Char6.Text = srArr(9)
        Num1.Text = srArr(10)
        Num2.Text = srArr(11)
        Num3.Text = srArr(12)
        Num4.Text = srArr(13)
        Num5.Text = srArr(14)
        Num6.Text = srArr(15)
        Memo.Text = srArr(16)
    End Sub

    Private Sub BtnEditCompany_Click(sender As Object, e As EventArgs) Handles BtnEdit.Click
        Dim dtToday As DateTime = DateTime.Now
        Try
            Dim Sql As String = ""

            Sql = ""
            Sql += "UPDATE "
            Sql += "Public."
            Sql += "m90_hanyo "
            Sql += "SET "
            Sql += " 会社コード"
            Sql += " = '"
            Sql += CompanyCode.Text
            Sql += "', "
            Sql += "固定キー"
            Sql += " = '"
            Sql += FixedKey.Text
            Sql += "', "
            Sql += "可変キー"
            Sql += " = '"
            Sql += VariableKey.Text
            Sql += "', "
            Sql += "文字１"
            Sql += " = '"
            Sql += Char1.Text
            Sql += "', "
            Sql += "文字２"
            Sql += " = '"
            Sql += Char2.Text
            Sql += "', "
            Sql += "文字３"
            Sql += " = '"
            Sql += Char3.Text
            Sql += "', "
            Sql += "文字４"
            Sql += " = '"
            Sql += Char4.Text
            Sql += "', "
            Sql += "文字５"
            Sql += " = '"
            Sql += Char5.Text
            Sql += "', "
            Sql += "文字６"
            Sql += " = '"
            Sql += Char6.Text
            Sql += "', "
            Sql += "数値１"
            Sql += " = '"
            Sql += Num1.Text
            Sql += "', "
            Sql += "数値２"
            Sql += " = '"
            Sql += Num2.Text
            Sql += "', "
            Sql += "数値３"
            Sql += " = '"
            Sql += Num3.Text
            Sql += "', "
            Sql += "数値４"
            Sql += " = '"
            Sql += Num4.Text
            Sql += "', "
            Sql += "数値５"
            Sql += " = '"
            Sql += Num5.Text
            Sql += "', "
            Sql += "数値６"
            Sql += " = '"
            Sql += Num6.Text
            Sql += "', "
            Sql += "メモ"
            Sql += " = '"
            Sql += Memo.Text
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
            Sql += " 固定キー"
            Sql += "='"
            Sql += key2
            Sql += "' "
            Sql += " AND"
            Sql += " 可変キー"
            Sql += "='"
            Sql += key3
            Sql += "' "
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

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        Dim MstHanyou As MstHanyou
        MstHanyou = New MstHanyou(_msgHd, _db, _langHd)
        MstHanyou.Show()
        Me.Close()
    End Sub
End Class