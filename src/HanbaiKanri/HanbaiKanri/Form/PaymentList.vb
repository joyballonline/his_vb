﻿Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Text.RegularExpressions

Public Class PaymentList
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
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler,
                   ByRef prmRefDbHd As UtilDBIf,
                   ByRef prmRefLang As UtilLangHandler)
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
        DgvSupplier.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells
    End Sub

    '画面表示時
    Private Sub MstSuppliere_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '一覧取得
        getSiharaiList()

    End Sub

    '抽出条件を含め、一覧取得
    Private Sub getSiharaiList()

        Dim Sql As String = ""

        '一覧をクリア
        DgvSupplier.Rows.Clear()

        '検索条件を取得
        Sql = searchConditions()

        '仕入先リストの取得
        Dim dsSupplier As DataSet = getDsData("m11_supplier", Sql)

        Dim Count As Integer = 0
        Dim SupplierCount As Integer = dsSupplier.Tables(RS).Rows.Count

        Dim SupplierOrderCount As Integer
        Dim SupplierBillingCount As Integer
        Dim SupplierBillingAmount As Integer
        Dim SupplierOrderAmount As Integer
        Dim AccountsReceivable As Integer

        'Language=ENGの時
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            LblConditions.Text = "ExtractionCondition"
            Label1.Text = "SupplierName"
            Label2.Text = "Address"
            Label3.Text = "PhoneNumber"
            Label4.Text = "SupplierCode"
            LblMode.Text = "PaymentRegistrationMode"

            BtnPayment.Text = "PaymentInput"
            BtnSerach.Text = "Search"
            btnBack.Text = "Back"

            DgvSupplier.Columns("仕入先名").HeaderText = "SupplierName"
            DgvSupplier.Columns("仕入金額計").HeaderText = "TotalPurchaseAmount"
            DgvSupplier.Columns("支払残高").HeaderText = "PaymentAmount"

        End If

        '仕入先の一覧から、支払一覧を作成
        For i As Integer = 0 To dsSupplier.Tables(RS).Rows.Count - 1

            Sql = " AND "
            Sql += "仕入先コード"
            Sql += " ILIKE "
            Sql += "'%"
            Sql += dsSupplier.Tables(RS).Rows(i)("仕入先コード")
            Sql += "%'"
            Sql += " AND "
            Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED


            '仕入先と一致する買掛基本を取得
            Dim dsKikehd As DataSet = getDsData("t46_kikehd", Sql)

            '仕入先の買掛データ数を取得
            SupplierBillingCount = dsKikehd.Tables(RS).Rows.Count.ToString

            Sql = " AND "
            Sql += "仕入先コード"
            Sql += " ILIKE "
            Sql += "'%"
            Sql += dsSupplier.Tables(RS).Rows(i)("仕入先コード")
            Sql += "%'"
            Sql += " AND "
            Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED


            '仕入先と一致する発注基本を取得
            Dim dsHattyu As DataSet = getDsData("t20_hattyu", Sql)

            '発注データ数
            SupplierOrderCount = dsHattyu.Tables(RS).Rows.Count.ToString

            '買掛金額を集計
            SupplierBillingAmount = IIf(
                dsKikehd.Tables(RS).Compute("SUM(買掛金額計)", Nothing) IsNot DBNull.Value,
                dsKikehd.Tables(RS).Compute("SUM(買掛金額計)", Nothing),
                0
            )

            '仕入金額を集計
            SupplierOrderAmount = IIf(
                dsHattyu.Tables(RS).Compute("SUM(仕入金額)", Nothing) IsNot DBNull.Value,
                dsHattyu.Tables(RS).Compute("SUM(仕入金額)", Nothing),
                0
            )

            '買掛残高を集計
            AccountsReceivable = IIf(
                dsKikehd.Tables(RS).Compute("SUM(買掛残高)", Nothing) IsNot DBNull.Value,
                dsKikehd.Tables(RS).Compute("SUM(買掛残高)", Nothing),
                0
            )


            '表示エリアにデータを追加
            If SupplierOrderCount > 0 Then
                DgvSupplier.Rows.Add()
                DgvSupplier.Rows(Count).Cells("仕入先名").Value = dsSupplier.Tables(RS).Rows(i)("仕入先名")
                DgvSupplier.Rows(Count).Cells("仕入金額計").Value = SupplierOrderAmount
                DgvSupplier.Rows(Count).Cells("支払残高").Value = AccountsReceivable
                DgvSupplier.Rows(Count).Cells("仕入先コード").Value = dsSupplier.Tables(RS).Rows(i)("仕入先コード")
                DgvSupplier.Rows(Count).Cells("会社コード").Value = dsSupplier.Tables(RS).Rows(i)("会社コード")

                Count += 1 'カウントアップ
            End If
        Next
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim frmMenu As frmC01F30_Menu
        frmMenu = New frmC01F30_Menu(_msgHd, _langHd, _db)
        frmMenu.Show()
        Me.Close()
    End Sub

    Private Sub BtnDeposit_Click(sender As Object, e As EventArgs) Handles BtnPayment.Click

        '対象データがない場合は取消操作不可能
        If DgvSupplier.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Return

        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvSupplier.CurrentCell.RowIndex
        Dim Company As String = DgvSupplier.Rows(RowIdx).Cells("会社コード").Value
        Dim Supplier As String = DgvSupplier.Rows(RowIdx).Cells("仕入先コード").Value
        Dim Name As String = DgvSupplier.Rows(RowIdx).Cells("仕入先名").Value
        Dim openForm As Form = Nothing
        openForm = New Payment(_msgHd, _db, _langHd, Me, Company, Supplier, Name)   '処理選択
        openForm.Show(Me)
    End Sub

    '抽出条件取得
    Private Function searchConditions() As String
        Dim Sql As String = ""

        '抽出条件
        Dim customerName As String = escapeSql(TxtCustomerName.Text)
        Dim customerAddress As String = escapeSql(TxtAddress.Text)
        Dim customerTel As String = escapeSql(TxtTel.Text)
        Dim customerCode As String = escapeSql(TxtCustomerCode.Text)

        If customerName <> Nothing Then
            Sql += " AND "
            Sql += " 仕入先名 ILIKE '%" & customerName & "%' "
        End If

        If customerAddress <> Nothing Then
            Sql += " AND "
            Sql += " (住所１ ILIKE '%" & customerAddress & "%' "
            Sql += " OR "
            Sql += " 住所２ ILIKE '%" & customerAddress & "%' "
            Sql += " OR "
            Sql += " 住所３ ILIKE '%" & customerAddress & "%' )"
        End If

        If customerTel <> Nothing Then
            Sql += " AND "
            Sql += " 電話番号検索用 ILIKE '%" & customerTel & "%' "
        End If

        If customerCode <> Nothing Then
            Sql += " AND "
            Sql += " 仕入先コード ILIKE '%" & customerCode & "%' "
        End If

        Return Sql

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

    '検索ボタン押下時
    Private Sub BtnSerach_Click(sender As Object, e As EventArgs) Handles BtnSerach.Click
        '一覧取得
        getSiharaiList()
    End Sub

    'sqlで実行する文字列からシングルクォーテーションを文字コードにする
    Private Function escapeSql(ByVal prmSql As String) As String
        Dim sql As String = prmSql

        sql = sql.Replace("'"c, "''") 'シングルクォーテーションを置換

        Return Regex.Escape(sql)
        Return sql
    End Function

End Class