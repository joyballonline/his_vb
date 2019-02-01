Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class MstHanyou
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
    Private _langHd As UtilLangHandler
    Private _db As UtilDBIf
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
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If frmC01F10_Login.loginValue.Language = "ENG" Then
            Label1.Text = "FixedKey"
            Label2.Text = "Char1"
            Label5.Text = "Char2"
            Label7.Text = "Char3"
            Label9.Text = "Char4"
            Label11.Text = "Char5"
            Label13.Text = "Char6"
            Label3.Text = "Num1"
            Label4.Text = "Num2"
            Label6.Text = "Num3"
            Label8.Text = "Num4"
            Label10.Text = "Num5"
            Label12.Text = "Num6"

            BtnSearch.Text = "Search"
            BtnAdd.Text = "Add"
            BtnEdit.Text = "Edit"
            BtnBack.Text = "Back"

            Dgv_Hanyo.Columns("会社コード").HeaderText = "CompanyCode"
            Dgv_Hanyo.Columns("固定キー").HeaderText = "FixedKey"
            Dgv_Hanyo.Columns("可変キー").HeaderText = "VariableKey"
            Dgv_Hanyo.Columns("表示順").HeaderText = "DisplayOrder"
            Dgv_Hanyo.Columns("文字１").HeaderText = "Charcter1"
            Dgv_Hanyo.Columns("文字２").HeaderText = "Charcter2"
            Dgv_Hanyo.Columns("文字３").HeaderText = "Charcter3"
            Dgv_Hanyo.Columns("文字４").HeaderText = "Charcter4"
            Dgv_Hanyo.Columns("文字５").HeaderText = "Charcter5"
            Dgv_Hanyo.Columns("文字６").HeaderText = "Charcter6"
            Dgv_Hanyo.Columns("数値１").HeaderText = "Number1"
            Dgv_Hanyo.Columns("数値２").HeaderText = "Number2"
            Dgv_Hanyo.Columns("数値３").HeaderText = "Number3"
            Dgv_Hanyo.Columns("数値４").HeaderText = "Number4"
            Dgv_Hanyo.Columns("数値５").HeaderText = "Number5"
            Dgv_Hanyo.Columns("数値６").HeaderText = "Number6"
            Dgv_Hanyo.Columns("メモ").HeaderText = "Memo"
            Dgv_Hanyo.Columns("更新者").HeaderText = "ModifiedBy"
            Dgv_Hanyo.Columns("更新日").HeaderText = "UpDateDate"

        End If
        Dim Sql As String = ""
        Try
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
            Sql += frmC01F10_Login.loginValue.BumonNM
            Sql += "'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                Dgv_Hanyo.Rows.Add()
                Dgv_Hanyo.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)(0)        '会社コード
                Dgv_Hanyo.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)(1)        '言語コード
                Dgv_Hanyo.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)(2)        '氏名
                Dgv_Hanyo.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)(3)      '略名
                Dgv_Hanyo.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)(4)      '備考
                Dgv_Hanyo.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)(5)      '無効フラグ
                Dgv_Hanyo.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)(6)      '更新者
                Dgv_Hanyo.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)(7)      '更新日
                Dgv_Hanyo.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)(8)        '会社コード
                Dgv_Hanyo.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)(9)        '言語コード
                Dgv_Hanyo.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)(10)        '氏名
                Dgv_Hanyo.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)(11)      '略名
                Dgv_Hanyo.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)(12)      '備考
                Dgv_Hanyo.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)(13)      '無効フラグ
                Dgv_Hanyo.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)(14)      '更新者
                Dgv_Hanyo.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)(15)      '更新日
                Dgv_Hanyo.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)(16)      '備考
                Dgv_Hanyo.Rows(index).Cells(17).Value = ds.Tables(RS).Rows(index)(17)      '無効フラグ
                Dgv_Hanyo.Rows(index).Cells(18).Value = ds.Tables(RS).Rows(index)(18)      '更新者
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Sub BtnSelect_Click(sender As Object, e As EventArgs) Handles BtnEdit.Click
        Dim openForm As Form = Nothing
        Dim Status As String = "EDIT"
        Dim Code As String = Dgv_Hanyo.Rows(Dgv_Hanyo.CurrentCell.RowIndex).Cells("会社コード").Value
        Dim Key1 As String = Dgv_Hanyo.Rows(Dgv_Hanyo.CurrentCell.RowIndex).Cells("固定キー").Value
        Dim Key2 As String = Dgv_Hanyo.Rows(Dgv_Hanyo.CurrentCell.RowIndex).Cells("可変キー").Value

        openForm = New Hanyo(_msgHd, _db, _langHd, Status, Code, Key1, Key2)   '処理選択
        openForm.Show()
        Me.Hide()   ' 自分は隠れる
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        Dim frmC01F30_Menu As frmC01F30_Menu
        frmC01F30_Menu = New frmC01F30_Menu(_msgHd, _langHd, _db)
        frmC01F30_Menu.Show()
        Me.Close()
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        Dim openForm As Form = Nothing
        Dim Status As String = "ADD"
        openForm = New Hanyo(_msgHd, _db, _langHd, Status)   '処理選択
        openForm.Show()
        Me.Hide()   ' 自分は隠れる
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        Dgv_Hanyo.Rows.Clear()

        Dim Sql As String = ""
        Try
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
            Sql += frmC01F10_Login.loginValue.BumonNM
            Sql += "'"

            If TxtFixedkey.Text = "" Then
            Else
                Sql += " AND "
                Sql += "固定キー"
                Sql += " ILIKE "
                Sql += "'"
                Sql += TxtFixedkey.Text
                Sql += "'"
            End If

            If TxtText1.Text = "" Then
            Else

                Sql += " AND "
                Sql += "文字１"
                Sql += " ILIKE "
                Sql += "'%"
                Sql += TxtText1.Text
                Sql += "%'"
            End If

            If TxtText2.Text = "" Then
            Else
                Sql += " AND "
                Sql += "文字２"
                Sql += " ILIKE "
                Sql += "'%"
                Sql += TxtText2.Text
                Sql += "%'"
            End If

            If TxtText3.Text = "" Then
            Else
                Sql += " AND "
                Sql += "文字３"
                Sql += " ILIKE "
                Sql += "'%"
                Sql += TxtText3.Text
                Sql += "%'"
            End If

            If TxtText4.Text = "" Then
            Else
                Sql += " AND "
                Sql += "文字４"
                Sql += " ILIKE "
                Sql += "'%"
                Sql += TxtText4.Text
                Sql += "%'"
            End If

            If TxtText5.Text = "" Then
            Else
                Sql += " AND "
                Sql += "文字５"
                Sql += " ILIKE "
                Sql += "'%"
                Sql += TxtText5.Text
                Sql += "%'"
            End If

            If TxtText6.Text = "" Then
            Else
                Sql += " AND "
                Sql += "文字６"
                Sql += " ILIKE "
                Sql += "'%"
                Sql += TxtText6.Text
                Sql += "%'"
            End If

            If TxtNumber1.Text = "" Then
            Else
                Sql += " AND "
                Sql += "数値１"
                Sql += " = "
                Sql += TxtNumber1.Text
            End If

            If TxtNumber2.Text = "" Then
            Else
                Sql += " AND "
                Sql += "数値２"
                Sql += " = "
                Sql += TxtNumber2.Text
            End If

            If TxtNumber3.Text = "" Then
            Else
                Sql += " AND "
                Sql += "数値３"
                Sql += " = "
                Sql += TxtNumber3.Text
            End If

            If TxtNumber4.Text = "" Then
            Else
                Sql += " AND "
                Sql += "数値４"
                Sql += " = "
                Sql += TxtNumber4.Text
            End If

            If TxtNumber5.Text = "" Then
            Else
                Sql += " AND "
                Sql += "数値５"
                Sql += " = "
                Sql += TxtNumber5.Text
            End If

            If TxtNumber6.Text = "" Then
            Else
                Sql += " AND "
                Sql += "数値６"
                Sql += " = "
                Sql += TxtNumber6.Text
            End If

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                Dgv_Hanyo.Rows.Add()
                Dgv_Hanyo.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)(0)        '会社コード
                Dgv_Hanyo.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)(1)        '言語コード
                Dgv_Hanyo.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)(2)        '氏名
                Dgv_Hanyo.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)(3)      '略名
                Dgv_Hanyo.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)(4)      '備考
                Dgv_Hanyo.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)(5)      '無効フラグ
                Dgv_Hanyo.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)(6)      '更新者
                Dgv_Hanyo.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)(7)      '更新日
                Dgv_Hanyo.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)(8)        '会社コード
                Dgv_Hanyo.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)(9)        '言語コード
                Dgv_Hanyo.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)(10)        '氏名
                Dgv_Hanyo.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)(11)      '略名
                Dgv_Hanyo.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)(12)      '備考
                Dgv_Hanyo.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)(13)      '無効フラグ
                Dgv_Hanyo.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)(14)      '更新者
                Dgv_Hanyo.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)(15)      '更新日
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub
End Class