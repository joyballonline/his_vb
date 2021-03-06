﻿Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class MstCompany
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

    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            Label1.Text = "CompanyName"
            TxtSearch.Location = New Point(120, 6)
            BtnSearch.Text = "Search"
            BtnSearch.Location = New Point(226, 6)
            btnCompanyAdd.Text = "Add"
            btnCompanyEdit.Text = "Edit"
            BtnBack.Text = "Back"

            Dgv_Company.Columns("会社コード").HeaderText = "CompanyCode"
            Dgv_Company.Columns("会社名").HeaderText = "CompanyName"
            Dgv_Company.Columns("会社略称").HeaderText = "CompanyAbbreviationName"
            Dgv_Company.Columns("郵便番号").HeaderText = "PostalCode"
            Dgv_Company.Columns("住所１").HeaderText = "Address1"
            Dgv_Company.Columns("住所２").HeaderText = "Address2"
            Dgv_Company.Columns("住所３").HeaderText = "Address3"
            Dgv_Company.Columns("電話番号").HeaderText = "PhoneNumber"
            Dgv_Company.Columns("FAX番号").HeaderText = "FAX"
            Dgv_Company.Columns("代表者役職").HeaderText = "RepresentativePosition"
            Dgv_Company.Columns("代表者名").HeaderText = "RepresentativeName"
            Dgv_Company.Columns("表示順").HeaderText = "DisplayOrder"
            Dgv_Company.Columns("備考").HeaderText = "Remarks"
            Dgv_Company.Columns("銀行名").HeaderText = "BankName"
            Dgv_Company.Columns("銀行コード").HeaderText = "BankCode"
            Dgv_Company.Columns("支店名").HeaderText = "BranchName"
            Dgv_Company.Columns("支店コード").HeaderText = "BranchCode"
            Dgv_Company.Columns("預金種目").HeaderText = "DepositCategory"
            Dgv_Company.Columns("口座番号").HeaderText = "AccountNumber"
            Dgv_Company.Columns("口座名義").HeaderText = "AccountHolder"
            Dgv_Company.Columns("前回締日").HeaderText = "LastClosingDate"
            Dgv_Company.Columns("今回締日").HeaderText = "ThisClosingDate"
            Dgv_Company.Columns("次回締日").HeaderText = "NextClosingDate"
            Dgv_Company.Columns("在庫単価評価法").HeaderText = "EvaluationMethod"
            Dgv_Company.Columns("前払法人税率").HeaderText = "PPH"
            Dgv_Company.Columns("会計用コード").HeaderText = "AccountingCode"
            Dgv_Company.Columns("更新者").HeaderText = "ModifiedBy"
            Dgv_Company.Columns("更新日").HeaderText = "UpdateDate"

        End If

    End Sub

    Private Sub setList()
        Dgv_Company.Rows.Clear()

        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "m01_company"
            Sql += " WHERE "
            Sql += "会社コード"
            Sql += " ILIKE "
            Sql += "'"
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "'"
            Sql += " AND "
            Sql += "会社名"
            Sql += " ILIKE "
            Sql += "'%"
            Sql += TxtSearch.Text
            Sql += "%'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                Dim getDCHanyo As DataSet = getDsHanyoData(CommonConst.DC_CODE, ds.Tables(RS).Rows(i)("預金種目"))
                Dim getIPHanyo As DataSet = getDsHanyoData(CommonConst.IP_CODE, ds.Tables(RS).Rows(i)("在庫単価評価法"))

                Dgv_Company.Rows.Add()
                Dgv_Company.Rows(i).Cells("会社コード").Value = ds.Tables(RS).Rows(i)("会社コード")
                Dgv_Company.Rows(i).Cells("会社名").Value = ds.Tables(RS).Rows(i)("会社名")
                Dgv_Company.Rows(i).Cells("会社略称").Value = ds.Tables(RS).Rows(i)("会社略称")
                Dgv_Company.Rows(i).Cells("郵便番号").Value = ds.Tables(RS).Rows(i)("郵便番号")
                Dgv_Company.Rows(i).Cells("住所１").Value = ds.Tables(RS).Rows(i)("住所１")
                Dgv_Company.Rows(i).Cells("住所２").Value = ds.Tables(RS).Rows(i)("住所２")
                Dgv_Company.Rows(i).Cells("住所３").Value = ds.Tables(RS).Rows(i)("住所３")
                Dgv_Company.Rows(i).Cells("電話番号").Value = ds.Tables(RS).Rows(i)("電話番号")
                Dgv_Company.Rows(i).Cells("FAX番号").Value = ds.Tables(RS).Rows(i)("ＦＡＸ番号")
                Dgv_Company.Rows(i).Cells("代表者役職").Value = ds.Tables(RS).Rows(i)("代表者役職")
                Dgv_Company.Rows(i).Cells("代表者名").Value = ds.Tables(RS).Rows(i)("代表者名")
                Dgv_Company.Rows(i).Cells("表示順").Value = ds.Tables(RS).Rows(i)("表示順")
                Dgv_Company.Rows(i).Cells("備考").Value = ds.Tables(RS).Rows(i)("備考")
                Dgv_Company.Rows(i).Cells("更新者").Value = ds.Tables(RS).Rows(i)("更新者")
                Dgv_Company.Rows(i).Cells("更新日").Value = ds.Tables(RS).Rows(i)("更新日")
                Dgv_Company.Rows(i).Cells("銀行コード").Value = ds.Tables(RS).Rows(i)("銀行コード")
                Dgv_Company.Rows(i).Cells("支店コード").Value = ds.Tables(RS).Rows(i)("支店コード")
                Dgv_Company.Rows(i).Cells("預金種目").Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                                               getDCHanyo.Tables(RS).Rows(0)("文字２"),
                                                               getDCHanyo.Tables(RS).Rows(0)("文字１"))
                Dgv_Company.Rows(i).Cells("口座番号").Value = ds.Tables(RS).Rows(i)("口座番号")
                Dgv_Company.Rows(i).Cells("口座名義").Value = ds.Tables(RS).Rows(i)("口座名義")
                Dgv_Company.Rows(i).Cells("銀行名").Value = ds.Tables(RS).Rows(i)("銀行名")
                Dgv_Company.Rows(i).Cells("支店名").Value = ds.Tables(RS).Rows(i)("支店名")
                Dgv_Company.Rows(i).Cells("前回締日").Value = ds.Tables(RS).Rows(i)("前回締日")
                Dgv_Company.Rows(i).Cells("今回締日").Value = ds.Tables(RS).Rows(i)("今回締日")
                Dgv_Company.Rows(i).Cells("次回締日").Value = ds.Tables(RS).Rows(i)("次回締日")
                Dgv_Company.Rows(i).Cells("在庫単価評価法").Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                                               getIPHanyo.Tables(RS).Rows(0)("文字２"),
                                                               getIPHanyo.Tables(RS).Rows(0)("文字１"))
                Dgv_Company.Rows(i).Cells("前払法人税率").Value = ds.Tables(RS).Rows(i)("前払法人税率")
                Dgv_Company.Rows(i).Cells("会計用コード").Value = ds.Tables(RS).Rows(i)("会計用コード")

            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    Private Sub btnCompanyrAdd_Click(sender As Object, e As EventArgs) Handles btnCompanyAdd.Click
        Dim openForm As Form = Nothing
        Dim Status As String = CommonConst.STATUS_ADD
        openForm = New Company(_msgHd, _db, _langHd, Me, Status)   '処理選択
        openForm.Show()
        Me.Hide()   ' 自分は隠れる
    End Sub

    Private Sub btnSelectCompany_Click(sender As Object, e As EventArgs) Handles btnCompanyEdit.Click
        Dim openForm As Form = Nothing
        Dim Status As String = CommonConst.STATUS_EDIT
        Dim CompanyCode As String = Dgv_Company.Rows(Dgv_Company.CurrentCell.RowIndex).Cells("会社コード").Value
        openForm = New Company(_msgHd, _db, _langHd, Me, Status, CompanyCode)   '処理選択
        openForm.Show()
        Me.Hide()   ' 自分は隠れる
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        setList()
    End Sub

    Private Sub MstCompany_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
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

    '汎用マスタから固定キー、可変キーに応じた結果を返す
    'param1：String 固定キー
    'param2：String 可変キー
    'Return: DataSet
    Private Function getDsHanyoData(ByVal prmFixed As String, Optional ByVal prmVariable As String = "") As DataSet
        Dim Sql As String = ""

        Sql = " AND "
        Sql += "固定キー ILIKE '" & prmFixed & "'"

        If prmVariable IsNot "" Then
            Sql += " AND "
            Sql += "可変キー ILIKE '" & prmVariable & "'"
        End If

        'リードタイムのリストを汎用マスタから取得
        Return getDsData("m90_hanyo", Sql)
    End Function

End Class