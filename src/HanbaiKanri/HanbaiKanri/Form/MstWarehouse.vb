Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Text.RegularExpressions

Public Class MstWarehouse

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
                   ByRef prmRefForm As Form)
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

    '画面表示時
    Private Sub MstAccount_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblAccountName.Text = "AccountName"
            TxtSearch.Location = New Point(120, 6)
            BtnSearch.Text = "Search"
            BtnSearch.Location = New Point(226, 6)
            btnAdd.Text = "Add"
            btnEdit.Text = "Edit"
            BtnBack.Text = "Back"

            Dgv_Account.Columns("倉庫コード").HeaderText = "WarehouseCode"
            Dgv_Account.Columns("名称").HeaderText = "Name"
            Dgv_Account.Columns("略称").HeaderText = "ShortName"
            Dgv_Account.Columns("郵便番号").HeaderText = "PostalCode"
            Dgv_Account.Columns("住所１").HeaderText = "Address１"
            Dgv_Account.Columns("住所２").HeaderText = "Address２"
            Dgv_Account.Columns("住所３").HeaderText = "Address３"
            Dgv_Account.Columns("電話番号").HeaderText = "PhoneNumber"
            Dgv_Account.Columns("ＦＡＸ番号").HeaderText = "ＦＡＸ"
            Dgv_Account.Columns("保税有無").HeaderText = "PresenceOfBondedBonds"
            Dgv_Account.Columns("備考").HeaderText = "Remarks"
            Dgv_Account.Columns("無効フラグ").HeaderText = "InvalidFlag"
            Dgv_Account.Columns("更新者").HeaderText = "ModifiedBy"
            Dgv_Account.Columns("更新日").HeaderText = "UpdateDate"

        End If

        '一覧取得
        setList()
    End Sub

    '一覧取得
    Private Sub setList()

        '一覧クリア
        Dgv_Account.Rows.Clear()

        Dim Sql As String = ""
        Try
            Sql = " AND "
            Sql += " ( 名称"
            Sql += " ILIKE "
            Sql += "'%"
            Sql += escapeSql(TxtSearch.Text)
            Sql += "%'"
            Sql += " OR "
            Sql += "略称"
            Sql += " ILIKE "
            Sql += "'%"
            Sql += escapeSql(TxtSearch.Text)
            Sql += "%'"
            Sql += " OR "
            Sql += "備考"
            Sql += " ILIKE "
            Sql += "'%"
            Sql += escapeSql(TxtSearch.Text)
            Sql += "%')"

            Dim ds As DataSet = getDsData("m20_warehouse", Sql)

            For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1

                Dgv_Account.Rows.Add()
                Dgv_Account.Rows(i).Cells("倉庫コード").Value = ds.Tables(RS).Rows(i)("倉庫コード")
                Dgv_Account.Rows(i).Cells("名称").Value = ds.Tables(RS).Rows(i)("名称")
                Dgv_Account.Rows(i).Cells("略称").Value = ds.Tables(RS).Rows(i)("略称")
                Dgv_Account.Rows(i).Cells("郵便番号").Value = ds.Tables(RS).Rows(i)("郵便番号")
                Dgv_Account.Rows(i).Cells("住所１").Value = ds.Tables(RS).Rows(i)("住所１")
                Dgv_Account.Rows(i).Cells("住所２").Value = ds.Tables(RS).Rows(i)("住所２")
                Dgv_Account.Rows(i).Cells("住所３").Value = ds.Tables(RS).Rows(i)("住所３")
                Dgv_Account.Rows(i).Cells("電話番号").Value = ds.Tables(RS).Rows(i)("電話番号")
                Dgv_Account.Rows(i).Cells("ＦＡＸ番号").Value = ds.Tables(RS).Rows(i)("ＦＡＸ番号")
                Dgv_Account.Rows(i).Cells("保税有無").Value = setCBText(ds.Tables(RS).Rows(i)("保税有無"))
                Dgv_Account.Rows(i).Cells("備考").Value = ds.Tables(RS).Rows(i)("備考")
                Dgv_Account.Rows(i).Cells("無効フラグ").Value = setEnabledText(ds.Tables(RS).Rows(i)("無効フラグ"))
                Dgv_Account.Rows(i).Cells("更新者").Value = ds.Tables(RS).Rows(i)("更新者")
                Dgv_Account.Rows(i).Cells("更新日").Value = ds.Tables(RS).Rows(i)("更新日")
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '追加ボタン押下時
    Private Sub btnCompanyrAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim openForm As Form = Nothing
        Dim Status As String = CommonConst.STATUS_ADD
        openForm = New Warehouse(_msgHd, _db, _langHd, Me, Status)   '処理選択
        openForm.Show()
        Me.Hide()   ' 自分は隠れる
    End Sub

    '編集ボタン押下時
    Private Sub btnAccountEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If Dgv_Account.Rows.Count > 0 Then
            Dim openForm As Form = Nothing
            Dim Status As String = CommonConst.STATUS_EDIT
            Dim CompanyCode As String = frmC01F10_Login.loginValue.BumonCD
            Dim AccountCode As String = Dgv_Account.Rows(Dgv_Account.CurrentCell.RowIndex).Cells("倉庫コード").Value
            openForm = New Warehouse(_msgHd, _db, _langHd, Me, Status, CompanyCode, AccountCode)   '処理選択
            openForm.Show()
            Me.Hide()   ' 自分は隠れる
        Else
            '該当データがなかったら
            _msgHd.dspMSG("NonData", frmC01F10_Login.loginValue.Language)
        End If

    End Sub

    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    '検索ボタン押下時
    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        '一覧取得
        setList()
    End Sub

    'param1：String テーブル名
    'param2：String 詳細条件
    'Return: DataSet
    Private Function getDsData(ByVal tableName As String, Optional ByRef txtParam As String = "") As DataSet
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Sql += "SELECT"
        Sql += " *"
        Sql += " FROM "

        Sql += "public." & tableName
        Sql += " WHERE "
        Sql += "会社コード"
        Sql += " ILIKE  "
        Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += txtParam
        Return _db.selectDB(Sql, RS, reccnt)
    End Function

    Private Function setEnabledText(ByVal prmVal As String) As String
        Dim reVal As String

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            reVal = IIf(Integer.Parse(prmVal) = CommonConst.FLAG_ENABLED, CommonConst.FLAG_ENABLED_TXT_ENG, CommonConst.FLAG_DISABLED_TXT_ENG)
        Else
            reVal = IIf(Integer.Parse(prmVal) = CommonConst.FLAG_ENABLED, CommonConst.FLAG_ENABLED_TXT, CommonConst.FLAG_DISABLED_TXT)
        End If

        Return reVal
    End Function

    Private Function setCBText(ByVal prmVal As String) As String
        Dim reVal As String

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            reVal = IIf(Integer.Parse(prmVal) = CommonConst.CB_KBN_MOT_AVAILABLE, CommonConst.FLAG_ENABLED_TXT_ENG, CommonConst.FLAG_DISABLED_TXT_ENG)
        Else
            reVal = IIf(Integer.Parse(prmVal) = CommonConst.CB_KBN_MOT_AVAILABLE, CommonConst.FLAG_ENABLED_TXT, CommonConst.FLAG_DISABLED_TXT)
        End If

        Return reVal
    End Function

    'sqlで実行する文字列からシングルクォーテーションを文字コードにする
    Private Function escapeSql(ByVal prmSql As String) As String
        Dim sql As String = prmSql

        sql = sql.Replace("'"c, "''") 'シングルクォーテーションを置換

        Return Regex.Escape(sql)
        Return sql
    End Function

    Private Sub MstWarehouse_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        setList()
    End Sub
End Class