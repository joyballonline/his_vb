﻿Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls

Public Class Account

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
    Private _AccountCode As String = ""

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
                   Optional ByRef prmRefCompany As String = "",
                   Optional ByRef prmRefAccount As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _status = prmRefStatus
        _companyCode = prmRefCompany
        _AccountCode = prmRefAccount
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    Private Sub btnAddAccount_Click(sender As Object, e As EventArgs) Handles btnAddAccount.Click
        Dim dtToday As DateTime = DateTime.Now
        Try
            If _status = "ADD" Then
                Dim Sql As String = ""

                Sql = ""
                Sql += "INSERT INTO "
                Sql += "Public."
                Sql += "m92_kanjo("
                Sql += "会社コード, 勘定科目コード, 勘定科目名称１, 勘定科目名称２, 勘定科目名称３, 会計用勘定科目コード, 備考, 有効区分, 更新者, 更新日)"
                Sql += " VALUES('"
                Sql += frmC01F10_Login.loginValue.BumonNM
                Sql += "', '"
                Sql += TxtAccountCode.Text
                Sql += "', '"
                Sql += TxtAccountName1.Text
                Sql += "', '"
                Sql += TxtAccountName2.Text
                Sql += "', '"
                Sql += TxtAccountName3.Text
                Sql += "', '"
                Sql += TxtAccountingAccountCode.Text
                Sql += "', '"
                Sql += TxtRemarks.Text
                Sql += "', '"
                Sql += TxtEffectiveClassification.Text
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoNM
                Sql += "', '"
                Sql += dtToday
                Sql += "')"
                Sql += "RETURNING 会社コード"
                Sql += ", "
                Sql += "勘定科目コード"
                Sql += ", "
                Sql += "勘定科目名称１"
                Sql += ", "
                Sql += "勘定科目名称２"
                Sql += ", "
                Sql += "勘定科目名称３"
                Sql += ", "
                Sql += "会計用勘定科目コード"
                Sql += ", "
                Sql += "備考"
                Sql += ", "
                Sql += "有効区分"
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
                Sql += "m92_kanjo "
                Sql += "SET "
                Sql += "勘定科目コード"
                Sql += " = '"
                Sql += TxtAccountCode.Text
                Sql += "', "
                Sql += "勘定科目名称１"
                Sql += " = '"
                Sql += TxtAccountName1.Text
                Sql += "', "
                Sql += "勘定科目名称２"
                Sql += " = '"
                Sql += TxtAccountName2.Text
                Sql += "', "
                Sql += "勘定科目名称３"
                Sql += " = '"
                Sql += TxtAccountName3.Text
                Sql += "', "
                Sql += "会計用勘定科目コード"
                Sql += " = '"
                Sql += TxtAccountingAccountCode.Text
                Sql += "', "
                Sql += "備考"
                Sql += " = '"
                Sql += TxtRemarks.Text
                Sql += "', "
                Sql += "有効区分"
                Sql += " = '"
                Sql += TxtEffectiveClassification.Text
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
                Sql += " 勘定科目コード"
                Sql += "='"
                Sql += _AccountCode
                Sql += "' "
                Sql += "RETURNING 会社コード"
                Sql += ", "
                Sql += "勘定科目コード"
                Sql += ", "
                Sql += "勘定科目名称１"
                Sql += ", "
                Sql += "勘定科目名称２"
                Sql += ", "
                Sql += "勘定科目名称３"
                Sql += ", "
                Sql += "会計用勘定科目コード"
                Sql += ", "
                Sql += "備考"
                Sql += ", "
                Sql += "有効区分"
                Sql += ", "
                Sql += "更新者"
                Sql += ", "
                Sql += "更新日"

                _db.executeDB(Sql)
            End If

            Dim frmMC As MstAccount
            frmMC = New MstAccount(_msgHd, _db, _langHd)
            frmMC.Show()
            Me.Close()

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim MstAccount As MstAccount
        MstAccount = New MstAccount(_msgHd, _db, _langHd)
        MstAccount.Show()
        Me.Close()
    End Sub

    Private Sub Account_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If frmC01F10_Login.loginValue.Language = "ENG" Then
            LblAccountCode.Text = "CustomerCode"
            LblAccountName1.Text = "CustomerName"
            LblAccountName2.Text = "CustomerCode"
            LblAccountName3.Text = "CustomerName"
            LblAccountingAccountCode.Text = "ShortName"
            LblRemarks.Text = "PostalCode"
            LblEffectiveClassification.Text = "EffectiveClassification"
            ExEffectiveClassification.Text = "(0:True 1:False)"
            btnAddAccount.Text = "Registration"
            btnBack.Text = "Back"
        End If
        If _status = "EDIT" Then
            Dim Sql As String = ""
            Sql += "SELECT "
            Sql += "会社コード, "
            Sql += "勘定科目コード, "
            Sql += "勘定科目名称１, "
            Sql += "勘定科目名称２, "
            Sql += "勘定科目名称３, "
            Sql += "会計用勘定科目コード, "
            Sql += "備考, "
            Sql += "有効区分, "
            Sql += "更新者, "
            Sql += "更新日 "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "m92_kanjo"
            Sql += " WHERE "
            Sql += "会社コード"
            Sql += " ILIKE "
            Sql += "'"
            Sql += _companyCode
            Sql += "'"
            Sql += " AND "
            Sql += "勘定科目コード"
            Sql += " ILIKE "
            Sql += "'"
            Sql += _AccountCode
            Sql += "'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            If ds.Tables(RS).Rows(0)("勘定科目コード") Is DBNull.Value Then
            Else
                TxtAccountCode.Text = ds.Tables(RS).Rows(0)("勘定科目コード")
            End If

            If ds.Tables(RS).Rows(0)("勘定科目名称１") Is DBNull.Value Then
            Else
                TxtAccountName1.Text = ds.Tables(RS).Rows(0)("勘定科目名称１")
            End If

            If ds.Tables(RS).Rows(0)("勘定科目名称２") Is DBNull.Value Then
            Else
                TxtAccountName2.Text = ds.Tables(RS).Rows(0)("勘定科目名称２")
            End If

            If ds.Tables(RS).Rows(0)("勘定科目名称３") Is DBNull.Value Then
            Else
                TxtAccountName3.Text = ds.Tables(RS).Rows(0)("勘定科目名称３")
            End If

            If ds.Tables(RS).Rows(0)("会計用勘定科目コード") Is DBNull.Value Then
            Else
                TxtAccountingAccountCode.Text = ds.Tables(RS).Rows(0)("会計用勘定科目コード")
            End If

            If ds.Tables(RS).Rows(0)("備考") Is DBNull.Value Then
            Else
                TxtRemarks.Text = ds.Tables(RS).Rows(0)("備考")
            End If

            If ds.Tables(RS).Rows(0)("有効区分") Is DBNull.Value Then
            Else
                TxtEffectiveClassification.Text = ds.Tables(RS).Rows(0)("有効区分")
            End If


        End If
    End Sub

End Class