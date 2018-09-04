﻿Option Explicit On

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
        Dim MstHanyou As MstHanyou
        MstHanyou = New MstHanyou(_msgHd, _db, _langHd)
        MstHanyou.Show()
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles BtnRegistration.Click
        Dim dtToday As DateTime = DateTime.Now
        Try
            If _status = "ADD" Then
                Dim Sql As String = ""

                Sql = ""
                Sql += "INSERT INTO "
                Sql += "Public."
                Sql += "m90_hanyo("
                Sql += "会社コード, 固定キー, 可変キー, 表示順, 文字１, 文字２, 文字３, 文字４, 文字５, 文字６, 数値１, 数値２, 数値３, 数値４, 数値５, 数値６, メモ, 更新者, 更新日)"
                Sql += " VALUES('"
                Sql += frmC01F10_Login.loginValue.BumonNM
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
            Else
                Dim Sql As String = ""

                Sql = ""
                Sql += "UPDATE "
                Sql += "Public."
                Sql += "m90_hanyo "
                Sql += "SET "
                Sql += " 会社コード"
                Sql += " = '"
                Sql += frmC01F10_Login.loginValue.BumonNM
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
                Sql += TxtNum1.Text
                Sql += "', "
                Sql += "数値２"
                Sql += " = '"
                Sql += TxtNum2.Text
                Sql += "', "
                Sql += "数値３"
                Sql += " = '"
                Sql += TxtNum3.Text
                Sql += "', "
                Sql += "数値４"
                Sql += " = '"
                Sql += TxtNum4.Text
                Sql += "', "
                Sql += "数値５"
                Sql += " = '"
                Sql += TxtNum5.Text
                Sql += "', "
                Sql += "数値６"
                Sql += " = '"
                Sql += TxtNum6.Text
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
                Sql += "' "
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
            End If

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

    Private Sub Hanyo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _status = "EDIT" Then
            Dim Sql As String = ""
            Sql += "SELECT "
            Sql += "会社コード, "
            Sql += "固定キー, "
            Sql += "可変キー, "
            Sql += "表示順, "
            Sql += "文字１, "
            Sql += "文字２, "
            Sql += "文字３, "
            Sql += "文字４, "
            Sql += "文字５, "
            Sql += "文字６, "
            Sql += "数値１, "
            Sql += "数値２, "
            Sql += "数値３, "
            Sql += "数値４, "
            Sql += "数値５, "
            Sql += "数値６, "
            Sql += "メモ, "
            Sql += "更新者, "
            Sql += "更新日 "
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

            TxtFixedKey.Text = ds.Tables(RS).Rows(0)("固定キー")
            TxtVariableKey.Text = ds.Tables(RS).Rows(0)("可変キー")
            TxtDisplayOrder.Text = ds.Tables(RS).Rows(0)("表示順")
            TxtChar1.Text = ds.Tables(RS).Rows(0)("文字１")
            TxtChar2.Text = ds.Tables(RS).Rows(0)("文字２")
            TxtChar3.Text = ds.Tables(RS).Rows(0)("文字３")
            txtChar4.Text = ds.Tables(RS).Rows(0)("文字４")
            TxtChar5.Text = ds.Tables(RS).Rows(0)("文字５")
            TxtChar6.Text = ds.Tables(RS).Rows(0)("文字６")
            TxtNum1.Text = ds.Tables(RS).Rows(0)("数値１")
            TxtNum2.Text = ds.Tables(RS).Rows(0)("数値２")
            TxtNum3.Text = ds.Tables(RS).Rows(0)("数値３")
            TxtNum4.Text = ds.Tables(RS).Rows(0)("数値４")
            TxtNum5.Text = ds.Tables(RS).Rows(0)("数値５")
            TxtNum6.Text = ds.Tables(RS).Rows(0)("数値６")
            TxtMemo.Text = ds.Tables(RS).Rows(0)("メモ")

        End If
    End Sub
End Class