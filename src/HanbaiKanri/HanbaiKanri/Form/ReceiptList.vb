﻿Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Text.RegularExpressions
Imports System.Globalization

Public Class ReceiptList
    Inherits System.Windows.Forms.Form

    '------------------------------------------------------------------------------------------------------
    'メンバー定数宣言
    '------------------------------------------------------------------------------------------------------
    'PG制御文字 
    Dim ds As DataSet
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
    Private CompanyCode As String = ""
    Private ReceiptNo As String()
    Private ReceiptStatus As String = ""


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
                   ByRef prmRefStatus As String)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        ReceiptStatus = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True
        DgvNyuko.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells
    End Sub

    '画面表示時
    Private Sub ReceiptList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If ReceiptStatus = CommonConst.STATUS_CANCEL Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "CancelMode"
            Else
                LblMode.Text = "取消モード"
            End If

            BtnReceiptCancel.Visible = True
            BtnReceiptCancel.Location = New Point(997, 509)
        ElseIf ReceiptStatus = CommonConst.STATUS_VIEW Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If

            BtnReceiptView.Visible = True
            BtnReceiptView.Location = New Point(997, 509)
        End If

        '検索（Date）の初期値
        dtReceiptDateSince.Value = DateAdd("d", CommonConst.SINCE_DEFAULT_DAY, DateTime.Today)
        dtReceiptDateUntil.Value = DateTime.Today

        nyukoList()

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblConditions.Text = "ExtractionCondition"
            Label1.Text = "SupplierName"
            Label2.Text = "Address"
            Label3.Text = "PhoneNumber"
            Label4.Text = "SupplierCode"
            Label8.Text = "GoodsReceiptDate"
            Label7.Text = "GoodsReceiptNumber"
            Label6.Text = "SalesPersonInCharge"
            Label11.Text = "CustomerNumber"
            Label10.Text = "DisplayFormat"
            RbtnSlip.Text = "UnitOfVoucher"

            RbtnDetails.Text = "UnitOfDetailData"
            RbtnDetails.Location = New Point(166, 196)

            ChkCancelData.Text = "IncludeCancelData"
            ChkCancelData.Location = New Point(556, 196)

            BtnSearch.Text = "Search"
            BtnReceiptCancel.Text = "CancelOfGoodsReceipt"
            BtnReceiptView.Text = "GoodsReceiptView"
            BtnBack.Text = "Back"
        End If
    End Sub

    '一覧表示処理
    Private Sub nyukoList(Optional ByRef prmRefStatus As String = "")
        Dim Status As String = prmRefStatus
        Dim Sql As String = ""

        '一覧クリア
        DgvNyuko.Rows.Clear()
        DgvNyuko.Columns.Clear()

        '伝票単位選択時
        If RbtnSlip.Checked Then

            '使用言語によって表示切替
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                DgvNyuko.Columns.Add("取消", "Cancel")
                DgvNyuko.Columns.Add("入庫番号", "GoodsReceiptNumber")
                DgvNyuko.Columns.Add("入庫日", "GoodsReceiptDate")
                DgvNyuko.Columns.Add("発注番号", "PurchaseOrderNumber")
                DgvNyuko.Columns.Add("発注番号枝番", "PurchaseOrderSubNumber")
                DgvNyuko.Columns.Add("客先番号", "CustomerNumber")
                DgvNyuko.Columns.Add("仕入先コード", "SupplierCode")
                DgvNyuko.Columns.Add("仕入先名", "SupplierName")
                DgvNyuko.Columns.Add("仕入先郵便番号", "PostalCode")
                DgvNyuko.Columns.Add("仕入先住所", "Address")
                DgvNyuko.Columns.Add("仕入先電話番号", "PhoneNumber")
                DgvNyuko.Columns.Add("仕入先ＦＡＸ", "FAX")
                DgvNyuko.Columns.Add("仕入先担当者名", "NameOfPIC")
                DgvNyuko.Columns.Add("仕入先担当者役職", "PositionPICSupplier")
                DgvNyuko.Columns.Add("営業担当者", "SalesPersonInCharge")
                DgvNyuko.Columns.Add("入力担当者", "PICForInputting")
                DgvNyuko.Columns.Add("備考", "Remarks")
                DgvNyuko.Columns.Add("登録日", "RegistrationDate")
            Else
                DgvNyuko.Columns.Add("取消", "取消")
                DgvNyuko.Columns.Add("入庫番号", "入庫番号")
                DgvNyuko.Columns.Add("入庫日", "入庫日")
                DgvNyuko.Columns.Add("発注番号", "発注番号")
                DgvNyuko.Columns.Add("発注番号枝番", "発注番号枝番")
                DgvNyuko.Columns.Add("客先番号", "客先番号")
                DgvNyuko.Columns.Add("仕入先コード", "仕入先コード")
                DgvNyuko.Columns.Add("仕入先名", "仕入先名")
                DgvNyuko.Columns.Add("仕入先郵便番号", "仕入先郵便番号")
                DgvNyuko.Columns.Add("仕入先住所", "仕入先住所")
                DgvNyuko.Columns.Add("仕入先電話番号", "仕入先電話番号")
                DgvNyuko.Columns.Add("仕入先ＦＡＸ", "仕入先ＦＡＸ")
                DgvNyuko.Columns.Add("仕入先担当者名", "仕入先担当者名")
                DgvNyuko.Columns.Add("仕入先担当者役職", "仕入先担当者役職")
                DgvNyuko.Columns.Add("営業担当者", "営業担当者")
                DgvNyuko.Columns.Add("入力担当者", "入力担当者")
                DgvNyuko.Columns.Add("備考", "備考")
                DgvNyuko.Columns.Add("登録日", "登録日")
            End If

            Try

                Sql = searchConditions()
                Sql += viewFormat() '取消有無
                Sql += " ORDER BY "
                Sql += "更新日 DESC"

                ds = getDsData("t42_nyukohd", Sql)

                For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                    DgvNyuko.Rows.Add()
                    DgvNyuko.Rows(i).Cells("取消").Value = getDelKbnTxt(ds.Tables(RS).Rows(i)("取消区分"))
                    DgvNyuko.Rows(i).Cells("入庫番号").Value = ds.Tables(RS).Rows(i)("入庫番号")
                    DgvNyuko.Rows(i).Cells("入庫日").Value = ds.Tables(RS).Rows(i)("入庫日").ToShortDateString()
                    DgvNyuko.Rows(i).Cells("発注番号").Value = ds.Tables(RS).Rows(i)("発注番号")
                    DgvNyuko.Rows(i).Cells("発注番号枝番").Value = ds.Tables(RS).Rows(i)("発注番号枝番")
                    DgvNyuko.Rows(i).Cells("客先番号").Value = ds.Tables(RS).Rows(i)("客先番号")
                    DgvNyuko.Rows(i).Cells("仕入先コード").Value = ds.Tables(RS).Rows(i)("仕入先コード")
                    DgvNyuko.Rows(i).Cells("仕入先名").Value = ds.Tables(RS).Rows(i)("仕入先名")
                    DgvNyuko.Rows(i).Cells("仕入先郵便番号").Value = ds.Tables(RS).Rows(i)("仕入先郵便番号")
                    DgvNyuko.Rows(i).Cells("仕入先住所").Value = ds.Tables(RS).Rows(i)("仕入先住所")
                    DgvNyuko.Rows(i).Cells("仕入先電話番号").Value = ds.Tables(RS).Rows(i)("仕入先電話番号")
                    DgvNyuko.Rows(i).Cells("仕入先ＦＡＸ").Value = ds.Tables(RS).Rows(i)("仕入先ＦＡＸ")
                    DgvNyuko.Rows(i).Cells("仕入先担当者名").Value = ds.Tables(RS).Rows(i)("仕入先担当者名")
                    DgvNyuko.Rows(i).Cells("仕入先担当者役職").Value = ds.Tables(RS).Rows(i)("仕入先担当者役職")
                    DgvNyuko.Rows(i).Cells("営業担当者").Value = ds.Tables(RS).Rows(i)("営業担当者")
                    DgvNyuko.Rows(i).Cells("入力担当者").Value = ds.Tables(RS).Rows(i)("入力担当者")
                    DgvNyuko.Rows(i).Cells("備考").Value = ds.Tables(RS).Rows(i)("備考")
                    DgvNyuko.Rows(i).Cells("登録日").Value = ds.Tables(RS).Rows(i)("登録日")
                Next

            Catch ue As UsrDefException
                ue.dspMsg()
                Throw ue
            Catch ex As Exception
                'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
            End Try

        Else

            '明細単位選択時

            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                DgvNyuko.Columns.Add("取消", "Cancel")
                DgvNyuko.Columns.Add("入庫番号", "GoodsReceiptNumber")
                DgvNyuko.Columns.Add("行番号", "LineNumber")
                DgvNyuko.Columns.Add("仕入区分", "PurchasingClassification")
                DgvNyuko.Columns.Add("メーカー", "Manufacturer")
                DgvNyuko.Columns.Add("品名", "ItemName")
                DgvNyuko.Columns.Add("型式", "Spec")
                DgvNyuko.Columns.Add("仕入先名", "SupplierName")
                DgvNyuko.Columns.Add("入庫数量", "GoodsReceiptQuantity")
                DgvNyuko.Columns.Add("単位", "Unit")
                DgvNyuko.Columns.Add("仕入値", "PurchaseAmount")
                DgvNyuko.Columns.Add("備考", "Remarks")
                DgvNyuko.Columns.Add("更新者", "ModifiedBy")

            Else
                DgvNyuko.Columns.Add("取消", "取消")
                DgvNyuko.Columns.Add("入庫番号", "入庫番号")
                DgvNyuko.Columns.Add("行番号", "行番号")
                DgvNyuko.Columns.Add("仕入区分", "仕入区分")
                DgvNyuko.Columns.Add("メーカー", "メーカー")
                DgvNyuko.Columns.Add("品名", "品名")
                DgvNyuko.Columns.Add("型式", "型式")
                DgvNyuko.Columns.Add("仕入先名", "仕入先名")
                DgvNyuko.Columns.Add("入庫数量", "入庫数量")
                DgvNyuko.Columns.Add("単位", "単位")
                DgvNyuko.Columns.Add("仕入値", "仕入値")
                DgvNyuko.Columns.Add("備考", "備考")
                DgvNyuko.Columns.Add("更新者", "更新者")
            End If

            DgvNyuko.Columns("入庫数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvNyuko.Columns("仕入値").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


            Dim reccnt As Integer = 0

            Sql = " SELECT t43.*, t42.取消区分 "
            Sql += " FROM "
            Sql += " t43_nyukodt t43"
            Sql += " INNER JOIN t42_nyukohd t42 ON "

            Sql += " t43.""会社コード"" = t42.""会社コード"""
            Sql += " AND "
            Sql += " t43.""入庫番号"" = t42.""入庫番号"""

            Sql += " where "
            Sql += " t43.""会社コード"" = '" & frmC01F10_Login.loginValue.BumonCD & "'"

            '抽出条件
            Dim supplierName As String = escapeSql(TxtSupplierName.Text)
            Dim supplierAddress As String = escapeSql(TxtAddress.Text)
            Dim supplierTel As String = escapeSql(TxtTel.Text)
            Dim supplierCode As String = escapeSql(TxtSupplierCode.Text)
            Dim sinceDate As String = strFormatDate(dtReceiptDateSince.Text)
            Dim untilDate As String = strFormatDate(dtReceiptDateUntil.Text)
            Dim sinceNum As String = escapeSql(TxtReceiptSince.Text)
            Dim untilNum As String = escapeSql(TxtReceiptUntil.Text)
            Dim salesName As String = escapeSql(TxtSales.Text)
            Dim poNum As String = escapeSql(TxtCustomerPO.Text)

            If supplierName <> Nothing Then
                Sql += " AND "
                Sql += " t42.仕入先名 ILIKE '%" & supplierName & "%' "
            End If

            If supplierAddress <> Nothing Then
                Sql += " AND "
                Sql += " t42.仕入先住所 ILIKE '%" & supplierAddress & "%' "
            End If

            If supplierTel <> Nothing Then
                Sql += " AND "
                Sql += " t42.仕入先電話番号 ILIKE '%" & supplierTel & "%' "
            End If

            If supplierCode <> Nothing Then
                Sql += " AND "
                Sql += " t42.仕入先コード ILIKE '%" & supplierCode & "%' "
            End If

            If sinceDate <> Nothing Then
                Sql += " AND "
                Sql += " t42.入庫日 >= '" & sinceDate & "'"
            End If
            If untilDate <> Nothing Then
                Sql += " AND "
                Sql += " t42.入庫日 <= '" & untilDate & "'"
            End If

            If sinceNum <> Nothing Then
                Sql += " AND "
                Sql += " t42.入庫番号 >= '" & sinceNum & "' "
            End If
            If untilNum <> Nothing Then
                Sql += " AND "
                Sql += " t42.入庫番号 <= '" & untilNum & "' "
            End If

            If poNum <> Nothing Then
                Sql += " AND "
                Sql += " t42.営業担当者 ILIKE '%" & salesName & "%' "
            End If

            If poNum <> Nothing Then
                Sql += " AND "
                Sql += " t42.客先番号 ILIKE '%" & salesName & "%' "
            End If

            If ChkCancelData.Checked = False Then
                Sql += " AND "
                Sql += "t42.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
            End If

            Sql += " ORDER BY "
            Sql += "t42.更新日 DESC"

            Try
                ds = _db.selectDB(Sql, RS, reccnt)

                For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                    DgvNyuko.Rows.Add()
                    DgvNyuko.Rows(i).Cells("取消").Value = getDelKbnTxt(ds.Tables(RS).Rows(i)("取消区分"))
                    DgvNyuko.Rows(i).Cells("入庫番号").Value = ds.Tables(RS).Rows(i)("入庫番号")
                    DgvNyuko.Rows(i).Cells("行番号").Value = ds.Tables(RS).Rows(i)("行番号")
                    DgvNyuko.Rows(i).Cells("仕入区分").Value = ds.Tables(RS).Rows(i)("仕入区分")
                    DgvNyuko.Rows(i).Cells("メーカー").Value = ds.Tables(RS).Rows(i)("メーカー")
                    DgvNyuko.Rows(i).Cells("品名").Value = ds.Tables(RS).Rows(i)("品名")
                    DgvNyuko.Rows(i).Cells("型式").Value = ds.Tables(RS).Rows(i)("型式")
                    DgvNyuko.Rows(i).Cells("仕入先名").Value = ds.Tables(RS).Rows(i)("仕入先名")
                    DgvNyuko.Rows(i).Cells("入庫数量").Value = ds.Tables(RS).Rows(i)("入庫数量")
                    DgvNyuko.Rows(i).Cells("単位").Value = ds.Tables(RS).Rows(i)("単位")
                    DgvNyuko.Rows(i).Cells("仕入値").Value = ds.Tables(RS).Rows(i)("仕入値")
                    DgvNyuko.Rows(i).Cells("備考").Value = ds.Tables(RS).Rows(i)("備考")
                    DgvNyuko.Rows(i).Cells("更新者").Value = ds.Tables(RS).Rows(i)("更新者")
                Next

            Catch ue As UsrDefException
                ue.dspMsg()
                Throw ue
            Catch ex As Exception
                'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
            End Try
        End If

    End Sub

    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        Dim openForm As Form = Nothing
        openForm = New frmC01F30_Menu(_msgHd, _langHd, _db)
        openForm.Show()
        Me.Close()
    End Sub

    '表示形式変更時
    Private Sub RbtnDetails_CheckedChanged(sender As Object, e As EventArgs) Handles RbtnDetails.CheckedChanged
        nyukoList() '一覧再表示
    End Sub

    '取消データを含めないチェックイベント
    Private Sub ChkCancelData_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCancelData.CheckedChanged
        nyukoList() '一覧再表示
    End Sub

    '検索ボタン押下時
    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        nyukoList() '一覧再表示
    End Sub

    '取消ボタン押下時
    Private Sub BtnSalesCancel_Click(sender As Object, e As EventArgs) Handles BtnReceiptCancel.Click
        Dim dtNow As DateTime = DateTime.Now

        '明細表示時、または対象データがない場合は取消操作不可能
        If RbtnDetails.Checked Or DgvNyuko.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Return

        End If


        '取消済みデータは取消操作不可能
        If DgvNyuko.Rows(DgvNyuko.CurrentCell.RowIndex).Cells("取消").Value = CommonConst.CANCEL_KBN_DISABLED_TXT Then
            '取消データは選択できないアラートを出す
            _msgHd.dspMSG("cannotSelectTorikeshiData", frmC01F10_Login.loginValue.Language)
            Return
        End If

        '取消確認のアラート
        Dim result As DialogResult = _msgHd.dspMSG("confirmCancel", frmC01F10_Login.loginValue.Language)

        If result = DialogResult.Yes Then
            updateData() 'データ更新
        End If

    End Sub

    '選択データをもとに以下テーブル更新
    't20_hattyu, t21_hattyu
    Private Sub updateData()
        Dim dtNow As String = formatDatetime(DateTime.Now)
        Dim Sql As String = ""

        Try

            Sql = "UPDATE "
            Sql += "Public."
            Sql += "t42_nyukohd "
            Sql += "SET "

            Sql += "取消区分 = " & CommonConst.CANCEL_KBN_DISABLED
            Sql += ", "
            Sql += "取消日"
            Sql += " = '"
            Sql += dtNow
            Sql += "', "
            Sql += "更新日"
            Sql += " = '"
            Sql += dtNow
            Sql += "', "
            Sql += "更新者"
            Sql += " = '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            Sql += " ' "

            Sql += "WHERE"
            Sql += " 会社コード"
            Sql += "='"
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "'"
            Sql += " AND"
            Sql += " 入庫番号"
            Sql += "='"
            Sql += DgvNyuko.Rows(DgvNyuko.CurrentCell.RowIndex).Cells("入庫番号").Value
            Sql += "' "

            _db.executeDB(Sql)

            Sql = "UPDATE "
            Sql += "Public."
            Sql += "t43_nyukodt "
            Sql += "SET "

            Sql += "更新日"
            Sql += " = '"
            Sql += dtNow
            Sql += "', "
            Sql += "更新者"
            Sql += " = '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            Sql += " ' "

            Sql += "WHERE"
            Sql += " 会社コード"
            Sql += "='"
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "'"
            Sql += " AND"
            Sql += " 入庫番号"
            Sql += "='"
            Sql += DgvNyuko.Rows(DgvNyuko.CurrentCell.RowIndex).Cells("入庫番号").Value
            Sql += "' "

            _db.executeDB(Sql)

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

        '一覧再表示
        nyukoList()

    End Sub

    '参照ボタン押下時
    Private Sub BtnSalesView_Click(sender As Object, e As EventArgs) Handles BtnReceiptView.Click

        '明細表示時、または対象データがない場合は取消操作不可能
        If RbtnDetails.Checked Or DgvNyuko.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Return

        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvNyuko.CurrentCell.RowIndex
        Dim No As String = DgvNyuko.Rows(RowIdx).Cells("発注番号").Value
        Dim Suffix As String = DgvNyuko.Rows(RowIdx).Cells("発注番号枝番").Value
        Dim Status As String = CommonConst.STATUS_VIEW

        Dim openForm As Form = Nothing
        openForm = New Receipt(_msgHd, _db, _langHd, No, Suffix, Status)   '処理選択
        openForm.Show(Me)
    End Sub

    'sqlで実行する文字列からシングルクォーテーションを文字コードにする
    Private Function escapeSql(ByVal prmSql As String) As String
        Dim sql As String = prmSql

        sql = sql.Replace("'"c, "''") 'シングルクォーテーションを置換

        Return Regex.Escape(sql)
        Return sql
    End Function

    Private Function actionChk() As Boolean
        '対象データがない場合は取消操作不可能
        If DgvNyuko.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)

            Return False

        Else

            Return True

        End If
    End Function

    '抽出条件取得
    Private Function searchConditions() As String
        Dim Sql As String = ""

        '抽出条件
        Dim supplierName As String = escapeSql(TxtSupplierName.Text)
        Dim supplierAddress As String = escapeSql(TxtAddress.Text)
        Dim supplierTel As String = escapeSql(TxtTel.Text)
        Dim supplierCode As String = escapeSql(TxtSupplierCode.Text)
        Dim sinceDate As String = strFormatDate(dtReceiptDateSince.Text)
        Dim untilDate As String = strFormatDate(dtReceiptDateUntil.Text)
        Dim sinceNum As String = escapeSql(TxtReceiptSince.Text)
        Dim untilNum As String = escapeSql(TxtReceiptUntil.Text)
        Dim salesName As String = escapeSql(TxtSales.Text)
        Dim poNum As String = escapeSql(TxtCustomerPO.Text)

        If supplierName <> Nothing Then
            Sql += " AND "
            Sql += " 仕入先名 ILIKE '%" & supplierName & "%' "
        End If

        If supplierAddress <> Nothing Then
            Sql += " AND "
            Sql += " 仕入先住所 ILIKE '%" & supplierAddress & "%' "
        End If

        If supplierTel <> Nothing Then
            Sql += " AND "
            Sql += " 仕入先電話番号 ILIKE '%" & supplierTel & "%' "
        End If

        If supplierCode <> Nothing Then
            Sql += " AND "
            Sql += " 仕入先コード ILIKE '%" & supplierCode & "%' "
        End If

        If sinceDate <> Nothing Then
            Sql += " AND "
            Sql += " 入庫日 >= '" & sinceDate & "'"
        End If
        If untilDate <> Nothing Then
            Sql += " AND "
            Sql += " 入庫日 <= '" & untilDate & "'"
        End If

        If sinceNum <> Nothing Then
            Sql += " AND "
            Sql += " 入庫番号 >= '" & sinceNum & "' "
        End If
        If untilNum <> Nothing Then
            Sql += " AND "
            Sql += " 入庫番号 <= '" & untilNum & "' "
        End If

        If poNum <> Nothing Then
            Sql += " AND "
            Sql += " 営業担当者 ILIKE '%" & salesName & "%' "
        End If

        If poNum <> Nothing Then
            Sql += " AND "
            Sql += " 客先番号 ILIKE '%" & salesName & "%' "
        End If

        Return Sql

    End Function

    '表示形式条件
    Private Function viewFormat() As String
        Dim Sql As String = ""

        '取消データを含めない場合
        If ChkCancelData.Checked = False Then
            Sql += " AND "
            Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
        End If

        Return Sql

    End Function

    '取消区分の表示テキストを返す
    'param1：String テーブル名
    'param2：String 詳細条件
    'Return: DataSet
    Public Function getDelKbnTxt(ByVal delKbn As String) As String
        '区分の値を取得し、使用言語に応じて値を返却

        Dim reDelKbn As String = IIf(delKbn = CommonConst.CANCEL_KBN_DISABLED,
                                    IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_JPN, CommonConst.CANCEL_KBN_JPN_TXT, CommonConst.CANCEL_KBN_ENG_TXT),
                                    "")
        Return reDelKbn
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

        Console.WriteLine(Sql)
        Return _db.selectDB(Sql, RS, reccnt)
    End Function

    'ユーザーのカルチャーから、日本の形式に変換する
    Private Function strFormatDate(ByVal prmDate As String, Optional ByRef prmFormat As String = "yyyy/MM/dd") As String

        'PCのカルチャーを取得し、それに応じてStringからDatetimeを作成
        Dim ci As New System.Globalization.CultureInfo(CultureInfo.CurrentCulture.Name.ToString)
        Dim dateFormat As DateTime = DateTime.Parse(prmDate, ci, System.Globalization.DateTimeStyles.AssumeLocal)

        '日本の形式に書き換える
        Return dateFormat.ToString(prmFormat)
    End Function

    'ユーザーのカルチャーから、日本の形式に変換する
    Private Function formatDatetime(ByVal prmDatetime As DateTime) As String

        'PCのカルチャーを取得し、それに応じてStringからDatetimeを作成
        Dim ciCurrent As New System.Globalization.CultureInfo(CultureInfo.CurrentCulture.Name.ToString)
        Dim dateFormat As DateTime = DateTime.Parse(prmDatetime.ToString, ciCurrent, System.Globalization.DateTimeStyles.AssumeLocal)

        Dim changeFormat As String = dateFormat.ToString("yyyy/MM/dd HH:mm:ss")

        Dim ciJP As New System.Globalization.CultureInfo(CommonConst.CI_JP)
        Dim rtnDatetime As DateTime = DateTime.Parse(changeFormat, ciJP, System.Globalization.DateTimeStyles.AssumeLocal)


        '日本の形式に書き換える
        Return changeFormat
    End Function

    '金額フォーマット（登録の際の小数点指定子）を日本の形式に合わせる
    '桁区切り記号は外す
    Private Function formatNumber(ByVal prmVal As Decimal) As String

        Dim nfi As NumberFormatInfo = New CultureInfo(CommonConst.CI_JP, False).NumberFormat

        '日本の形式に書き換える
        Return prmVal.ToString("F3", nfi) '売掛残高を増やす
    End Function

End Class