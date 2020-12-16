Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class Hanyo
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
    Private _status As String = ""
    Private _companyCode As String = ""
    Private _key1 As String = ""
    Private _key2 As String = ""


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
                   ByRef prmRefForm As Form,
                   ByRef prmRefStatus As String,
                   Optional ByRef prmRefCode As String = Nothing,
                   Optional ByRef prmRefKey1 As String = Nothing,
                   Optional ByRef prmRefKey2 As String = Nothing)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
        _status = prmRefStatus
        _companyCode = prmRefCode
        _key1 = prmRefKey1
        _key2 = prmRefKey2
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles BtnRegistration.Click

        '入力チェック 2020.03.05
        Dim blnFlg As Boolean = mCheck
        If blnFlg = False Then
            Exit Sub
        End If

        Dim dtToday As String = UtilClass.formatDatetime(DateTime.Now)
        Try
            If _status = CommonConst.STATUS_ADD Then
                Dim Sql As String = ""

                Sql = ""
                Sql += "INSERT INTO "
                Sql += "Public."
                Sql += "m90_hanyo("
                Sql += "会社コード, 固定キー, 可変キー, 表示順, 文字１, 文字２, 文字３, 文字４, 文字５, 文字６, 数値１, 数値２, 数値３, 数値４, 数値５, 数値６, メモ, 更新者, 更新日)"
                Sql += " VALUES('"
                Sql += frmC01F10_Login.loginValue.BumonCD
                Sql += "', '"
                Sql += TxtFixedKey.Text
                Sql += "', '"
                Sql += TxtVariableKey.Text
                Sql += "', '"
                If TxtDisplayOrder.Text = "" Then
                    Sql += "0"
                Else
                    Sql += TxtDisplayOrder.Text
                End If
                Sql += "', '"
                Sql += TxtChar1.Text
                Sql += "', '"
                Sql += TxtChar2.Text
                Sql += "', '"
                Sql += TxtChar3.Text
                Sql += "', '"
                Sql += txtChar4.Text
                Sql += "', '"
                Sql += TxtChar5.Text
                Sql += "', '"
                Sql += TxtChar6.Text
                Sql += "', '"

                If TxtNum1.Text = "" Then
                    Sql += "0"
                Else
                    Sql += TxtNum1.Text
                End If
                Sql += "', '"
                If TxtNum2.Text = "" Then
                    Sql += "0"
                Else
                    Sql += TxtNum2.Text
                End If
                Sql += "', '"
                If TxtNum3.Text = "" Then
                    Sql += "0"
                Else
                    Sql += TxtNum3.Text
                End If
                Sql += "', '"
                If TxtNum4.Text = "" Then
                    Sql += "0"
                Else
                    Sql += TxtNum4.Text
                End If
                Sql += "', '"
                If TxtNum5.Text = "" Then
                    Sql += "0"
                Else
                    Sql += TxtNum5.Text
                End If
                Sql += "', '"
                If TxtNum6.Text = "" Then
                    Sql += "0"
                Else
                    Sql += TxtNum6.Text
                End If
                Sql += "', '"

                Sql += TxtMemo.Text
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoNM
                Sql += "', '"
                Sql += dtToday
                Sql += "')"

                _db.executeDB(Sql)
            Else
                Dim Sql As String = ""

                Sql = ""
                Sql += "UPDATE "
                Sql += "Public."
                Sql += "m90_hanyo "
                Sql += "SET "
                Sql += " 会社コード"
                Sql += " = '"
                Sql += frmC01F10_Login.loginValue.BumonCD
                Sql += "', "
                Sql += "固定キー"
                Sql += " = '"
                Sql += TxtFixedKey.Text
                Sql += "', "
                Sql += "可変キー"
                Sql += " = '"
                Sql += TxtVariableKey.Text
                Sql += "', "
                Sql += "文字１"
                Sql += " = '"
                Sql += TxtChar1.Text
                Sql += "', "
                Sql += "文字２"
                Sql += " = '"
                Sql += TxtChar2.Text
                Sql += "', "
                Sql += "文字３"
                Sql += " = '"
                Sql += TxtChar3.Text
                Sql += "', "
                Sql += "文字４"
                Sql += " = '"
                Sql += txtChar4.Text
                Sql += "', "
                Sql += "文字５"
                Sql += " = '"
                Sql += TxtChar5.Text
                Sql += "', "
                Sql += "文字６"
                Sql += " = '"
                Sql += TxtChar6.Text
                Sql += "', "

                Sql += "数値１"
                Sql += " = '"
                If TxtNum1.Text = "" Then
                    Sql += "0"
                Else
                    Sql += TxtNum1.Text
                End If
                Sql += "', "

                Sql += "数値２"
                Sql += " = '"
                If TxtNum2.Text = "" Then
                    Sql += "0"
                Else
                    Sql += TxtNum2.Text
                End If
                Sql += "', "

                Sql += "数値３"
                Sql += " = '"
                If TxtNum3.Text = "" Then
                    Sql += "0"
                Else
                    Sql += TxtNum3.Text
                End If
                Sql += "', "

                Sql += "数値４"
                Sql += " = '"
                If TxtNum4.Text = "" Then
                    Sql += "0"
                Else
                    Sql += TxtNum4.Text
                End If
                Sql += "', "

                Sql += "数値５"
                Sql += " = '"
                If TxtNum5.Text = "" Then
                    Sql += "0"
                Else
                    Sql += TxtNum5.Text
                End If
                Sql += "', "

                Sql += "数値６"
                Sql += " = '"
                If TxtNum6.Text = "" Then
                    Sql += "0"
                Else
                    Sql += TxtNum6.Text
                End If
                Sql += "', "

                Sql += "メモ"
                Sql += " = '"
                Sql += TxtMemo.Text
                Sql += "', "
                Sql += "更新者"
                Sql += " = '"
                Sql += frmC01F10_Login.loginValue.TantoNM
                Sql += "', "
                Sql += "更新日"
                Sql += " = '"
                Sql += dtToday
                Sql += "', "
                Sql += "表示順"
                Sql += " = "
                Sql += TxtDisplayOrder.Text
                Sql += " "
                Sql += "WHERE"
                Sql += " 会社コード"
                Sql += "='"
                Sql += _companyCode
                Sql += "'"
                Sql += " AND"
                Sql += " 固定キー"
                Sql += "='"
                Sql += _key1
                Sql += "' "
                Sql += " AND"
                Sql += " 可変キー"
                Sql += "='"
                Sql += _key2
                Sql += "' "

                _db.executeDB(Sql)
            End If

            _parentForm.Enabled = True
            _parentForm.Show()
            Me.Dispose()

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Function mCheck() As Boolean  '2020.03.05

        '文字数チェックはプロパティのMaxLengthで設定

        '表示順
        If IsNumeric(TxtDisplayOrder.Text) = False Then

            _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
            TxtDisplayOrder.Select()
            Exit Function
        End If

        '数値１
        If TxtNum1.Text <> vbNullString AndAlso IsNumeric(TxtNum1.Text) = False Then

            _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
            TxtNum1.Select()
            Exit Function
        End If

        '数値２
        If TxtNum2.Text <> vbNullString AndAlso IsNumeric(TxtNum2.Text) = False Then

            _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
            TxtNum2.Select()
            Exit Function
        End If

        '数値３
        If TxtNum3.Text <> vbNullString AndAlso IsNumeric(TxtNum3.Text) = False Then

            _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
            TxtNum3.Select()
            Exit Function
        End If

        '数値４
        If TxtNum4.Text <> vbNullString AndAlso IsNumeric(TxtNum4.Text) = False Then

            _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
            TxtNum4.Select()
            Exit Function
        End If

        '数値５
        If TxtNum5.Text <> vbNullString AndAlso IsNumeric(TxtNum5.Text) = False Then

            _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
            TxtNum5.Select()
            Exit Function
        End If

        '数値６
        If TxtNum6.Text <> vbNullString AndAlso IsNumeric(TxtNum6.Text) = False Then

            _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
            TxtNum6.Select()
            Exit Function
        End If

        mCheck = True

    End Function


    Private Sub Hanyo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If frmC01F10_Login.loginValue.Language = "ENG" Then
            Label2.Text = "FixedKey"
            Label3.Text = "VariableKey"
            Label5.Text = "DisplayOrder"
            Label6.Text = "Charcter1"
            Label7.Text = "Charcter2"
            Label8.Text = "Charcter3"
            Label9.Text = "Charcter4"
            Label11.Text = "Charcter5"
            Label4.Text = "Charcter6"
            Label12.Text = "Number1"
            Label13.Text = "Number2"
            Label14.Text = "Number3"
            Label18.Text = "Number4"
            Label17.Text = "Number5"
            Label16.Text = "Number6"
            Label15.Text = "Memo"

            Label1.Text = "(Non-Overlapping string)"
            Label20.Text = "(Non-Overlapping string)"


            BtnRegistration.Text = "Registration"
            BtnBack.Text = "Back"
        End If

        If _status = CommonConst.STATUS_EDIT Then
            Dim Sql As String = ""
            Sql += "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "m90_hanyo"
            Sql += " WHERE "
            Sql += "会社コード"
            Sql += " ILIKE "
            Sql += "'"
            Sql += _companyCode
            Sql += "'"
            Sql += " AND "
            Sql += "固定キー"
            Sql += " ILIKE "
            Sql += "'"
            Sql += _key1
            Sql += "'"
            Sql += " AND "
            Sql += "可変キー"
            Sql += " ILIKE "
            Sql += "'"
            Sql += _key2
            Sql += "'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            If ds.Tables(RS).Rows(0)("固定キー") Is DBNull.Value Then
            Else
                TxtFixedKey.Text = ds.Tables(RS).Rows(0)("固定キー")
            End If

            If ds.Tables(RS).Rows(0)("可変キー") Is DBNull.Value Then
            Else
                TxtVariableKey.Text = ds.Tables(RS).Rows(0)("可変キー")
            End If

            If ds.Tables(RS).Rows(0)("表示順") Is DBNull.Value Then
            Else
                TxtDisplayOrder.Text = ds.Tables(RS).Rows(0)("表示順")
            End If

            If ds.Tables(RS).Rows(0)("文字１") Is DBNull.Value Then
            Else
                TxtChar1.Text = ds.Tables(RS).Rows(0)("文字１")
            End If

            If ds.Tables(RS).Rows(0)("文字２") Is DBNull.Value Then
            Else
                TxtChar2.Text = ds.Tables(RS).Rows(0)("文字２")
            End If

            If ds.Tables(RS).Rows(0)("文字３") Is DBNull.Value Then
            Else
                TxtChar3.Text = ds.Tables(RS).Rows(0)("文字３")
            End If

            If ds.Tables(RS).Rows(0)("文字４") Is DBNull.Value Then
            Else
                txtChar4.Text = ds.Tables(RS).Rows(0)("文字４")
            End If

            If ds.Tables(RS).Rows(0)("文字５") Is DBNull.Value Then
            Else
                TxtChar5.Text = ds.Tables(RS).Rows(0)("文字５")
            End If

            If ds.Tables(RS).Rows(0)("文字６") Is DBNull.Value Then
            Else
                TxtChar6.Text = ds.Tables(RS).Rows(0)("文字６")
            End If

            If ds.Tables(RS).Rows(0)("数値１") Is DBNull.Value Then
            Else
                TxtNum1.Text = ds.Tables(RS).Rows(0)("数値１")
            End If

            If ds.Tables(RS).Rows(0)("数値２") Is DBNull.Value Then
            Else
                TxtNum2.Text = ds.Tables(RS).Rows(0)("数値２")
            End If

            If ds.Tables(RS).Rows(0)("数値３") Is DBNull.Value Then
            Else
                TxtNum3.Text = ds.Tables(RS).Rows(0)("数値３")
            End If

            If ds.Tables(RS).Rows(0)("数値４") Is DBNull.Value Then
            Else
                TxtNum4.Text = ds.Tables(RS).Rows(0)("数値４")
            End If

            If ds.Tables(RS).Rows(0)("数値５") Is DBNull.Value Then
            Else
                TxtNum5.Text = ds.Tables(RS).Rows(0)("数値５")
            End If

            If ds.Tables(RS).Rows(0)("数値６") Is DBNull.Value Then
            Else
                TxtNum6.Text = ds.Tables(RS).Rows(0)("数値６")
            End If

            If ds.Tables(RS).Rows(0)("メモ") Is DBNull.Value Then
            Else
                TxtMemo.Text = ds.Tables(RS).Rows(0)("メモ")
            End If
        End If
    End Sub
End Class