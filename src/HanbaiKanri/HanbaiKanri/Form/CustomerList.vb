﻿Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Text.RegularExpressions

Public Class CustomerList
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

    'ロード時
    Private Sub CustomerList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        CustomerListLoad()

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblCustomerName.Text = "CustomerName"
            BtnSearch.Text = "Search"
            BtnBillingCalculation.Text = "SelectInvoice"
            btnBack.Text = "Back"

            DgvCustomer.Columns("得意先名").HeaderText = "CustomerName"
            DgvCustomer.Columns("受注金額計").HeaderText = "TotalJobOrderAmount"
            DgvCustomer.Columns("請求金額計").HeaderText = "TotalBillingAmount"
            DgvCustomer.Columns("請求残高").HeaderText = "BillingBalance"
        End If

    End Sub

    '一覧表示処理
    Private Sub CustomerListLoad()
        Dim Sql As String = ""
        Dim reccnt As Integer = 0

        'リストクリア
        DgvCustomer.Rows.Clear()

        Sql += searchConditions()

        Dim dsCustomer As DataSet = getDsData("m10_customer", Sql)

        Dim Count As Integer = 0
        Dim CustomerCount As Integer = dsCustomer.Tables(RS).Rows.Count
        Dim CustomerOrderCount As Integer
        Dim CustomerBillingCount As Integer

        Dim CustomerBillingAmount As Decimal
        Dim CustomerOrderAmount As Decimal

        For i As Integer = 0 To dsCustomer.Tables(RS).Rows.Count - 1

            Sql = " AND "
            Sql += "得意先コード"
            Sql += " = "
            Sql += "'"
            Sql += dsCustomer.Tables(RS).Rows(i)("得意先コード")
            Sql += "'"
            Sql += " AND "
            Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

            '得意先ごとの請求基本を取得
            Dim dsSkyuhd As DataSet = getDsData("t23_skyuhd", Sql)

            '請求件数を取得
            CustomerBillingCount = dsSkyuhd.Tables(RS).Rows.Count.ToString

            Sql = " AND "
            Sql += "得意先コード"
            Sql += " = "
            Sql += "'"
            Sql += dsCustomer.Tables(RS).Rows(i)("得意先コード")
            Sql += "'"
            Sql += " AND "
            Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

            '得意先ごとの受注基本を取得
            Dim dsCymnhd As DataSet = getDsData("t10_cymnhd", Sql)

            '受注件数を取得
            CustomerOrderCount = dsCymnhd.Tables(RS).Rows.Count.ToString

            CustomerBillingAmount = IIf(
                dsSkyuhd.Tables(RS).Compute("SUM(請求金額計)", Nothing) IsNot DBNull.Value,
                dsSkyuhd.Tables(RS).Compute("SUM(請求金額計)", Nothing),
                0
            )
            CustomerOrderAmount = IIf(
                dsCymnhd.Tables(RS).Compute("SUM(見積金額)", Nothing) IsNot DBNull.Value,
                dsCymnhd.Tables(RS).Compute("SUM(見積金額)", Nothing),
                0
            )

            If CustomerOrderCount > 0 Then
                Dim idx = DgvCustomer.Rows.Count()

                DgvCustomer.Rows.Add()
                DgvCustomer.Rows(idx).Cells("得意先名").Value = dsCustomer.Tables(RS).Rows(i)("得意先名")
                DgvCustomer.Rows(idx).Cells("受注金額計").Value = CustomerOrderAmount
                DgvCustomer.Rows(idx).Cells("請求金額計").Value = CustomerBillingAmount
                DgvCustomer.Rows(idx).Cells("請求残高").Value = CustomerOrderAmount - CustomerBillingAmount
                DgvCustomer.Rows(idx).Cells("受注件数").Value = CustomerOrderCount
                DgvCustomer.Rows(idx).Cells("請求件数").Value = CustomerBillingCount
                DgvCustomer.Rows(idx).Cells("得意先コード").Value = dsCustomer.Tables(RS).Rows(i)("得意先コード")
                DgvCustomer.Rows(idx).Cells("会社コード").Value = dsCustomer.Tables(RS).Rows(i)("会社コード")

            End If

        Next
    End Sub

    '戻るボタン押下時
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    '請求計算ボタン押下時
    Private Sub BtnBillingCalculation_Click(sender As Object, e As EventArgs) Handles BtnBillingCalculation.Click
        If DgvCustomer.Rows.Count = 0 Then
            '対象データがないメッセージを表示
            _msgHd.dspMSG("NonData", frmC01F10_Login.loginValue.Language)

            Return
        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvCustomer.CurrentCell.RowIndex
        Dim Company As String = DgvCustomer.Rows(RowIdx).Cells("会社コード").Value
        Dim Customer As String = DgvCustomer.Rows(RowIdx).Cells("得意先コード").Value
        Dim openForm As Form = Nothing
        openForm = New CustomerOrderList(_msgHd, _db, _langHd, Me, Company, Customer)   '処理選択

        Me.Enabled = False
        Me.Hide()
        openForm.Show(Me)
    End Sub

    '検索ボタン押下時
    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        DgvCustomer.Rows.Clear()
        CustomerListLoad()
    End Sub

    '抽出条件取得
    Private Function searchConditions() As String
        Dim Sql As String = ""

        '抽出条件
        Dim customerName As String = escapeSql(TxtSearch.Text)

        If customerName <> Nothing Then
            Sql += " AND "
            Sql += " 得意先名 ILIKE '%" & customerName & "%' "
        End If

        Return Sql

    End Function

    'sqlで実行する文字列からシングルクォーテーションを文字コードにする
    Private Function escapeSql(ByVal prmSql As String) As String
        Dim sql As String = prmSql

        sql = sql.Replace("'"c, "''") 'シングルクォーテーションを置換

        Return Regex.Escape(sql)
        Return sql
    End Function

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

End Class