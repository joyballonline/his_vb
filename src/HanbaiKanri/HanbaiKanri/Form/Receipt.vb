﻿Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Globalization

Public Class Receipt
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
    'Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ
    Private CompanyCode As String = ""
    Private No As String = ""
    Private Suffix As String = ""
    Private _status As String = ""
    Private _langHd As UtilLangHandler
    Private Input As String = frmC01F10_Login.loginValue.TantoNM

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
                   ByRef prmRefNo As String,
                   ByRef prmRefSuffix As String,
                   Optional ByRef prmRefStatus As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        No = prmRefNo
        Suffix = prmRefSuffix
        _status = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    '画面表示時
    Private Sub Receipt_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblPurchaseNo.Text = "PurchaseOrderNumber"
            LblCustomerNo.Text = "CustomerNumber"
            LblPurchaseDate.Text = "OrderDate"
            LblSupplier.Text = "SupplierName"
            LblPurchase.Text = "PurchaseOrder"
            LblHistory.Text = "ReceiptHistory"
            LblAdd.Text = "GoodsReceiptThisTime"
            LblReceiptDate.Text = "GoodsReceiptDate"
            LblReceiptDate.Size = New Size(148, 22)
            DtpReceiptDate.Location = New Point(342, 343)

            LblRemarks.Location = New Point(496, 343)
            LblRemarks.Size = New Size(150, 22)
            TxtRemarks.Location = New Point(652, 343)
            TxtRemarks.Size = New Size(560, 22)

            LblRemarks.Text = "Remarks"
            LblCount1.Text = "Record"
            LblCount1.Location = New Point(1272, 82)
            LblCount1.Size = New Size(66, 22)
            LblCount2.Text = "Record"
            LblCount2.Location = New Point(1272, 212)
            LblCount2.Size = New Size(66, 22)
            LblCount3.Text = "Record"
            LblCount3.Location = New Point(1272, 343)
            LblCount3.Size = New Size(66, 22)

            TxtCount1.Location = New Point(1228, 82)
            TxtCount2.Location = New Point(1228, 212)
            TxtCount3.Location = New Point(1228, 343)

            BtnRegist.Text = "Registration"
            BtnBack.Text = "Back"

        End If

        If _status = CommonConst.STATUS_VIEW Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If

            LblCount1.Visible = False
            LblCount2.Visible = False
            LblCount3.Visible = False
            LblPurchase.Visible = False
            LblAdd.Visible = False
            LblReceiptDate.Visible = False
            LblRemarks.Visible = False
            DtpReceiptDate.Visible = False
            TxtCount1.Visible = False
            TxtCount2.Visible = False
            TxtCount3.Visible = False
            TxtRemarks.Visible = False
            DgvPurchase.Visible = False
            DgvAdd.Visible = False
            DgvHistory.ReadOnly = False

            LblHistory.Location = New Point(12, 82)
            DgvHistory.Location = New Point(12, 106)
            DgvHistory.Size = New Point(1326, 400)

            BtnRegist.Visible = False
        Else
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "ReceiptInputMode"
            Else
                LblMode.Text = "入庫入力モード"
            End If
        End If

        '発注エリア
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            DgvPurchase.Columns.Add("明細", "DetailData")
            DgvPurchase.Columns.Add("メーカー", "Manufacturer")
            DgvPurchase.Columns.Add("品名", "ItemName")
            DgvPurchase.Columns.Add("型式", "Spec")
            DgvPurchase.Columns.Add("発注数量", "OrderQuantity")
            DgvPurchase.Columns.Add("単位", "Unit")
            DgvPurchase.Columns.Add("仕入数量", "PurchasedQuantity")
            DgvPurchase.Columns.Add("仕入単価", "PurchaseUnitPrice")
            DgvPurchase.Columns.Add("仕入金額", "PurchaseAmount")
            DgvPurchase.Columns.Add("発注残数", "NumberOfOrderRemaining")
            DgvPurchase.Columns.Add("更新日", "UpdateDate")
        Else
            DgvPurchase.Columns.Add("明細", "明細")
            DgvPurchase.Columns.Add("メーカー", "メーカー")
            DgvPurchase.Columns.Add("品名", "品名")
            DgvPurchase.Columns.Add("型式", "型式")
            DgvPurchase.Columns.Add("発注数量", "発注数量")
            DgvPurchase.Columns.Add("単位", "単位")
            DgvPurchase.Columns.Add("仕入数量", "仕入数量")
            DgvPurchase.Columns.Add("仕入単価", "仕入単価")
            DgvPurchase.Columns.Add("仕入金額", "仕入金額")
            DgvPurchase.Columns.Add("発注残数", "発注残数")
            DgvPurchase.Columns.Add("更新日", "更新日")
        End If

        DgvPurchase.Columns("発注数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvPurchase.Columns("仕入数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvPurchase.Columns("仕入単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvPurchase.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvPurchase.Columns("発注残数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvPurchase.Columns("更新日").Visible = False

        '入庫済みエリア
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            DgvHistory.Columns.Add("No", "No")
            DgvHistory.Columns.Add("入庫番号", "GoodsReceiptNumber")
            DgvHistory.Columns.Add("行番号", "LineNumber")
            DgvHistory.Columns.Add("仕入区分", "PurchasingClassification")
            DgvHistory.Columns.Add("メーカー", "Manufacturer")
            DgvHistory.Columns.Add("品名", "ItemName")
            DgvHistory.Columns.Add("型式", "Spec")
            DgvHistory.Columns.Add("単位", "Unit")
            DgvHistory.Columns.Add("仕入先", "SupplierName")
            DgvHistory.Columns.Add("仕入値", "PurchaseAmount")
            DgvHistory.Columns.Add("入庫数量", "GoodsReceiptQuantity")
            DgvHistory.Columns.Add("備考", "Remarks")

        Else
            DgvHistory.Columns.Add("No", "No")
            DgvHistory.Columns.Add("入庫番号", "入庫番号")
            DgvHistory.Columns.Add("行番号", "行番号")
            DgvHistory.Columns.Add("仕入区分", "仕入区分")
            DgvHistory.Columns.Add("メーカー", "メーカー")
            DgvHistory.Columns.Add("品名", "品名")
            DgvHistory.Columns.Add("型式", "型式")
            DgvHistory.Columns.Add("単位", "単位")
            DgvHistory.Columns.Add("仕入先", "仕入先")
            DgvHistory.Columns.Add("仕入値", "仕入値")
            DgvHistory.Columns.Add("入庫数量", "入庫数量")
            DgvHistory.Columns.Add("備考", "備考")
        End If

        DgvHistory.Columns("仕入値").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvHistory.Columns("入庫数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


        '今回入庫エリア
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            DgvAdd.Columns.Add("No", "No")
            DgvAdd.Columns.Add("行番号", "LineNumber")
            DgvAdd.Columns.Add("仕入区分", "PurchasingClassification")
            DgvAdd.Columns.Add("メーカー", "Manufacturer")
            DgvAdd.Columns.Add("品名", "ItemName")
            DgvAdd.Columns.Add("型式", "Spec")
            DgvAdd.Columns.Add("単位", "Unit")
            DgvAdd.Columns.Add("仕入先", "SupplierName")
            DgvAdd.Columns.Add("仕入値", "PurchaseAmount")
            DgvAdd.Columns.Add("入庫数量", "GoodsReceiptQuantity")
            DgvAdd.Columns.Add("備考", "Remarks")

        Else
            DgvAdd.Columns.Add("No", "No")
            DgvAdd.Columns.Add("行番号", "行番号")
            DgvAdd.Columns.Add("仕入区分", "仕入区分")
            DgvAdd.Columns.Add("メーカー", "メーカー")
            DgvAdd.Columns.Add("品名", "品名")
            DgvAdd.Columns.Add("型式", "型式")
            DgvAdd.Columns.Add("単位", "単位")
            DgvAdd.Columns.Add("仕入先", "仕入先")
            DgvAdd.Columns.Add("仕入値", "仕入値")
            DgvAdd.Columns.Add("入庫数量", "入庫数量")
            DgvAdd.Columns.Add("備考", "備考")
        End If

        DgvAdd.Columns("仕入値").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvAdd.Columns("入庫数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        Dim reccnt As Integer = 0
        Dim Sql As String = ""

        Try

            Sql = " AND "
            Sql += "発注番号"
            Sql += " ILIKE '" & No & "'"
            Sql += " AND "
            Sql += "発注番号枝番"
            Sql += " ILIKE '" & Suffix & "'"
            Sql += " AND "
            Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

            Dim dsHattyu As DataSet = getDsData("t20_hattyu", Sql)

            Sql = " SELECT t43.*, t42.取消区分 "
            Sql += " FROM "
            Sql += " t43_nyukodt t43"
            Sql += " INNER JOIN t42_nyukohd t42 ON "

            Sql += " t43.""会社コード"" = t42.""会社コード"""
            Sql += " AND "
            Sql += " t43.""入庫番号"" = t42.""入庫番号"""

            Sql += " where "
            Sql += " t43.""会社コード"" = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND "
            Sql += "t43.発注番号"
            Sql += " ILIKE '" & No & "'"
            Sql += " AND "
            Sql += "t43.発注番号枝番"
            Sql += " ILIKE '" & Suffix & "'"
            Sql += " AND "
            Sql += "t42.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
            Sql += " ORDER BY "
            Sql += "t42.更新日 DESC"

            Dim dsNyukodt As DataSet = _db.selectDB(Sql, RS, reccnt)

            Sql = " SELECT t21.メーカー, t21.品名, t21.型式, t21.発注数量, t21.単位, t21.仕入数量, t21.仕入値, t21.仕入金額"
            Sql += ", t21.発注残数, t21.行番号, t21.仕入区分, t21.仕入先名, t21.備考, t20.取消区分, t20.更新日 "
            Sql += " FROM "
            Sql += " t21_hattyu t21"
            Sql += " INNER JOIN t20_hattyu t20 ON "

            Sql += " t21.""会社コード"" = t20.""会社コード"""
            Sql += " AND "
            Sql += " t21.""発注番号"" = t20.""発注番号"""
            Sql += " AND "
            Sql += " t21.""発注番号枝番"" = t20.""発注番号枝番"""

            Sql += " where "
            Sql += " t21.""会社コード"" = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND "
            Sql += "t21.発注番号"
            Sql += " ILIKE '" & No & "'"
            Sql += " AND "
            Sql += "t21.発注番号枝番"
            Sql += " ILIKE '" & Suffix & "'"
            Sql += " AND "
            Sql += "t20.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

            Dim dsHattyudt As DataSet = _db.selectDB(Sql, RS, reccnt)

            For i As Integer = 0 To dsHattyudt.Tables(RS).Rows.Count - 1
                DgvPurchase.Rows.Add()

                DgvPurchase.Rows(i).Cells("メーカー").Value = dsHattyudt.Tables(RS).Rows(i)("メーカー")
                DgvPurchase.Rows(i).Cells("品名").Value = dsHattyudt.Tables(RS).Rows(i)("品名")
                DgvPurchase.Rows(i).Cells("型式").Value = dsHattyudt.Tables(RS).Rows(i)("型式")
                DgvPurchase.Rows(i).Cells("発注数量").Value = dsHattyudt.Tables(RS).Rows(i)("発注数量")
                DgvPurchase.Rows(i).Cells("単位").Value = dsHattyudt.Tables(RS).Rows(i)("単位")
                DgvPurchase.Rows(i).Cells("仕入数量").Value = dsHattyudt.Tables(RS).Rows(i)("仕入数量")
                DgvPurchase.Rows(i).Cells("仕入単価").Value = dsHattyudt.Tables(RS).Rows(i)("仕入値")
                DgvPurchase.Rows(i).Cells("仕入金額").Value = dsHattyudt.Tables(RS).Rows(i)("仕入金額")
                DgvPurchase.Rows(i).Cells("発注残数").Value = dsHattyudt.Tables(RS).Rows(i)("発注残数")
                DgvPurchase.Rows(i).Cells("更新日").Value = dsHattyudt.Tables(RS).Rows(i)("更新日")
            Next

            For i As Integer = 0 To dsNyukodt.Tables(RS).Rows.Count - 1
                DgvHistory.Rows.Add()
                DgvHistory.Rows(i).Cells("入庫番号").Value = dsNyukodt.Tables(RS).Rows(i)("入庫番号")
                DgvHistory.Rows(i).Cells("行番号").Value = dsNyukodt.Tables(RS).Rows(i)("行番号")
                DgvHistory.Rows(i).Cells("仕入区分").Value = dsNyukodt.Tables(RS).Rows(i)("仕入区分")
                DgvHistory.Rows(i).Cells("メーカー").Value = dsNyukodt.Tables(RS).Rows(i)("メーカー")
                DgvHistory.Rows(i).Cells("品名").Value = dsNyukodt.Tables(RS).Rows(i)("品名")
                DgvHistory.Rows(i).Cells("型式").Value = dsNyukodt.Tables(RS).Rows(i)("型式")
                DgvHistory.Rows(i).Cells("単位").Value = dsNyukodt.Tables(RS).Rows(i)("単位")
                DgvHistory.Rows(i).Cells("仕入先").Value = dsNyukodt.Tables(RS).Rows(i)("仕入先名")
                DgvHistory.Rows(i).Cells("仕入値").Value = dsNyukodt.Tables(RS).Rows(i)("仕入値")
                DgvHistory.Rows(i).Cells("入庫数量").Value = dsNyukodt.Tables(RS).Rows(i)("入庫数量")
                DgvHistory.Rows(i).Cells("備考").Value = dsNyukodt.Tables(RS).Rows(i)("備考")
            Next


            DgvAdd.Columns("No").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("No").ReadOnly = True
            DgvAdd.Columns("行番号").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("行番号").ReadOnly = True
            DgvAdd.Columns("仕入区分").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("仕入区分").ReadOnly = True
            DgvAdd.Columns("メーカー").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("メーカー").ReadOnly = True
            DgvAdd.Columns("品名").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("品名").ReadOnly = True
            DgvAdd.Columns("型式").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("型式").ReadOnly = True
            DgvAdd.Columns("単位").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("単位").ReadOnly = True
            DgvAdd.Columns("仕入先").DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
            DgvAdd.Columns("仕入先").ReadOnly = True

            For index As Integer = 0 To dsHattyudt.Tables(RS).Rows.Count - 1
                If dsHattyudt.Tables(RS).Rows(index)("発注残数") = 0 Then
                Else
                    DgvAdd.Rows.Add()
                    DgvAdd.Rows(index).Cells("行番号").Value = dsHattyudt.Tables(RS).Rows(index)("行番号")
                    DgvAdd.Rows(index).Cells("仕入区分").Value = dsHattyudt.Tables(RS).Rows(index)("仕入区分")
                    DgvAdd.Rows(index).Cells("メーカー").Value = dsHattyudt.Tables(RS).Rows(index)("メーカー")
                    DgvAdd.Rows(index).Cells("品名").Value = dsHattyudt.Tables(RS).Rows(index)("品名")
                    DgvAdd.Rows(index).Cells("型式").Value = dsHattyudt.Tables(RS).Rows(index)("型式")
                    DgvAdd.Rows(index).Cells("仕入先").Value = dsHattyudt.Tables(RS).Rows(index)("仕入先名")
                    DgvAdd.Rows(index).Cells("単位").Value = dsHattyudt.Tables(RS).Rows(index)("単位")
                    DgvAdd.Rows(index).Cells("仕入値").Value = dsHattyudt.Tables(RS).Rows(index)("仕入値")
                    DgvAdd.Rows(index).Cells("入庫数量").Value = 0
                    DgvAdd.Rows(index).Cells("備考").Value = dsHattyudt.Tables(RS).Rows(index)("備考")
                End If
            Next

            '行番号の振り直し
            Dim i1 As Integer = DgvPurchase.Rows.Count()
            Dim No1 As Integer = 1
            For c As Integer = 0 To i1 - 1
                DgvPurchase.Rows(c).Cells(0).Value = No1
                No1 += 1
            Next c
            TxtCount1.Text = DgvPurchase.Rows.Count()

            Dim i2 As Integer = DgvHistory.Rows.Count()
            Dim No2 As Integer = 1
            For c As Integer = 0 To i2 - 1
                DgvHistory.Rows(c).Cells(0).Value = No2
                No2 += 1
            Next c
            TxtCount2.Text = DgvHistory.Rows.Count()

            Dim i3 As Integer = DgvAdd.Rows.Count()
            Dim No3 As Integer = 1
            For c As Integer = 0 To i3 - 1
                DgvAdd.Rows(c).Cells(0).Value = No3
                No3 += 1
            Next c
            TxtCount3.Text = DgvAdd.Rows.Count()

            TxtPurchaseNo.Text = dsHattyu.Tables(RS).Rows(0)("発注番号")
            TxtSuffixNo.Text = dsHattyu.Tables(RS).Rows(0)("発注番号枝番")
            TxtCustomerPO.Text = dsHattyu.Tables(RS).Rows(0)("客先番号")
            TxtOrdingDate.Text = dsHattyu.Tables(RS).Rows(0)("発注日").ToShortDateString()
            TxtSupplierCode.Text = dsHattyu.Tables(RS).Rows(0)("仕入先コード")
            TxtSupplierName.Text = dsHattyu.Tables(RS).Rows(0)("仕入先名")
            DtpReceiptDate.Value = Date.Now

            '入庫日の選択最小日を発注日にする
            DtpReceiptDate.MinDate = dsHattyu.Tables(RS).Rows(0)("発注日").ToShortDateString()

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        'Dim openForm As Form = Nothing
        'openForm = New frmC01F30_Menu(_msgHd, _langHd, _db)
        'openForm.Show()
        Me.Close()
    End Sub

    '登録ボタン押下時
    Private Sub BtnRegist_Click(sender As Object, e As EventArgs) Handles BtnRegist.Click

        Dim dtToday As String = DateTime.Now
        Dim strToday As String = formatDatetime(dtToday)
        Dim reccnt As Integer = 0

        Dim Sql As String = ""

        Sql = " AND "
        Sql += "発注番号 ILIKE '" & No & "'"
        Sql += " AND "
        Sql += "発注番号枝番 ILIKE '" & Suffix & "'"
        Sql += " AND "
        Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '取消区分=0

        Dim dsHattyu As DataSet = getDsData("t20_hattyu", Sql)

        Dim chkReceiptAmount As Integer = 0 '入庫データがあるか合算する用

        '発注明細の仕入数量と今回入庫数の合算が発注数量を超えたら
        'なぜ入庫時だけ発注まで見ているのか（仕入時も入庫をみないといけないのでは）

        '対象データがなかったらメッセージを表示
        If DgvAdd.RowCount = 0 Then
            '操作できないメッセージを表示
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)

            Return
        End If

        '最初に今回入庫に入力がなかったらエラーで返す
        For i As Integer = 0 To DgvAdd.RowCount - 1
            chkReceiptAmount += DgvAdd.Rows(i).Cells("入庫数量").Value
        Next
        If chkReceiptAmount <= 0 Then
            '対象データがないメッセージを表示
            _msgHd.dspMSG("chkReceiptAddError", frmC01F10_Login.loginValue.Language)

            Return
        End If


        If DgvPurchase.Rows(0).Cells("更新日").Value <> dsHattyu.Tables(RS).Rows(0)("更新日") Then
            '画面を開いたときの日時とデータの日時が異なっていた場合
            'データが誰かに変更された旨を伝える
            _msgHd.dspMSG("chkData", frmC01F10_Login.loginValue.Language)

            Return
        End If

        '発注明細をループさせながら
        '入庫数量が発注残数を超えない範囲か確認
        For i As Integer = 0 To DgvPurchase.RowCount - 1
            If (DgvPurchase.Rows(i).Cells("発注残数").Value < Integer.Parse(DgvAdd.Rows(i).Cells("入庫数量").Value)) Then

                '対象データがないメッセージを表示
                _msgHd.dspMSG("chkGRBalanceError", frmC01F10_Login.loginValue.Language)

                Return
            End If

        Next

        'ここからは登録データがある前提
        'それぞれ伝票番号を取得
        Dim PC As String = getSaiban("50", dtToday)
        Dim WH As String = getSaiban("60", dtToday)

        Try

            '入庫基本に発注基本の内容を登録
            Sql = "INSERT INTO "
            Sql += "Public."
            Sql += "t42_nyukohd("
            Sql += "会社コード, 入庫番号, 発注番号, 発注番号枝番, 客先番号, 仕入先コード, 仕入先名, 仕入先郵便番号, 仕入先住所, 仕入先電話番号, 仕入先ＦＡＸ, 仕入先担当者役職, 仕入先担当者名, 支払条件, 仕入金額, 粗利額, 営業担当者, 入力担当者, 備考, 取消区分, ＶＡＴ, ＰＰＨ, 入庫日, 登録日, 更新日, 更新者)"
            Sql += " VALUES ("
            Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += ", '"
            Sql += WH
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("発注番号").ToString
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("発注番号枝番").ToString
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("客先番号").ToString
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("仕入先コード").ToString
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("仕入先名").ToString
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("仕入先郵便番号").ToString
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("仕入先住所").ToString
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("仕入先電話番号").ToString
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("仕入先ＦＡＸ").ToString
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("仕入先担当者役職").ToString
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("仕入先担当者名").ToString
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("支払条件").ToString
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("仕入金額").ToString
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("粗利額").ToString
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("営業担当者").ToString
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("入力担当者").ToString
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("備考").ToString
            Sql += "', '"
            Sql += CommonConst.CANCEL_KBN_ENABLED.ToString
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("ＶＡＴ").ToString
            Sql += "', '"
            Sql += dsHattyu.Tables(RS).Rows(0)("ＰＰＨ").ToString
            Sql += "', '"
            Sql += strToday
            Sql += "', '"
            Sql += strToday
            Sql += "', '"
            Sql += strToday
            Sql += "', '"
            Sql += Input
            Sql += " ')"

            _db.executeDB(Sql)

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try


        '発注明細をループさせながら
        For i As Integer = 0 To DgvPurchase.RowCount - 1

            '入庫数量が0以外だったら
            If Integer.Parse(DgvAdd.Rows(i).Cells("入庫数量").Value) <> 0 Then

                Try

                    '明細部分を登録する
                    Sql = "INSERT INTO "
                    Sql += "Public."
                    Sql += "t43_nyukodt("
                    Sql += "会社コード, 入庫番号, 発注番号, 発注番号枝番, 行番号, 仕入区分, メーカー, 品名, 型式, 仕入先名, 仕入値, 入庫数量, 単位, 備考, 更新者, 更新日)"
                    Sql += " VALUES( "
                    Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"
                    Sql += ", '"
                    Sql += WH
                    Sql += "', '"
                    Sql += dsHattyu.Tables(RS).Rows(0)("発注番号").ToString
                    Sql += "', '"
                    Sql += dsHattyu.Tables(RS).Rows(0)("発注番号枝番").ToString
                    Sql += "', '"
                    Sql += DgvAdd.Rows(i).Cells("No").Value.ToString
                    Sql += "', '"
                    Sql += DgvAdd.Rows(i).Cells("仕入区分").Value.ToString
                    Sql += "', '"
                    Sql += DgvAdd.Rows(i).Cells("メーカー").Value.ToString
                    Sql += "', '"
                    Sql += DgvAdd.Rows(i).Cells("品名").Value.ToString
                    Sql += "', '"
                    Sql += DgvAdd.Rows(i).Cells("型式").Value.ToString
                    Sql += "', '"
                    Sql += DgvAdd.Rows(i).Cells("仕入先").Value.ToString
                    Sql += "', '"
                    Sql += formatNumber(DgvAdd.Rows(i).Cells("仕入値").Value.ToString)
                    Sql += "', '"
                    Sql += formatNumber(DgvAdd.Rows(i).Cells("入庫数量").Value.ToString)
                    Sql += "', '"
                    Sql += DgvAdd.Rows(i).Cells("単位").Value.ToString
                    Sql += "', '"
                    Sql += DgvAdd.Rows(i).Cells("備考").Value.ToString
                    Sql += "', '"
                    Sql += Input
                    Sql += "', '"
                    Sql += strToday
                    Sql += " ')"

                    _db.executeDB(Sql)

                Catch ue As UsrDefException
                    ue.dspMsg()
                    Throw ue
                Catch ex As Exception
                    'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                    Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
                End Try

            End If

        Next

        Dim openForm As Form = Nothing
        openForm = New OrderingList(_msgHd, _db, _langHd, CommonConst.STATUS_RECEIPT)
        openForm.Show()
        Me.Close()

    End Sub

    'param1：String 採番キー
    'param2：DateTime 登録日
    'Return: String 伝票番号
    '伝票番号を取得
    Private Function getSaiban(ByVal key As String, ByVal today As DateTime) As String
        Dim Sql As String = ""
        Dim saibanID As String = ""
        Dim reccnt As Integer = 0 'DB用（デフォルト）

        Try
            Sql = "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public.m80_saiban"
            Sql += " WHERE "
            Sql += "会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND "
            Sql += "採番キー = '" & key & "'"

            Dim dsSaiban As DataSet = _db.selectDB(Sql, RS, reccnt)

            saibanID = dsSaiban.Tables(RS).Rows(0)("接頭文字")
            saibanID += today.ToString("MMdd")
            saibanID += dsSaiban.Tables(RS).Rows(0)("最新値").ToString.PadLeft(dsSaiban.Tables(RS).Rows(0)("連番桁数"), "0")

            Dim keyNo As Integer

            If dsSaiban.Tables(RS).Rows(0)("最新値") = dsSaiban.Tables(RS).Rows(0)("最大値") Then
                '最新値が最大と同じ場合、最小値にリセット
                keyNo = dsSaiban.Tables(RS).Rows(0)("最小値")
            Else
                '最新値+1
                keyNo = dsSaiban.Tables(RS).Rows(0)("最新値") + 1
            End If

            Sql = "UPDATE "
            Sql += "Public.m80_saiban "
            Sql += "SET "
            Sql += " 最新値 "
            Sql += " = '"
            Sql += keyNo.ToString
            Sql += "', "
            Sql += "更新者"
            Sql += " = '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            Sql += "', "
            Sql += "更新日"
            Sql += " = '"
            Sql += formatDatetime(today)
            Sql += "' "
            Sql += "WHERE"
            Sql += " 会社コード"
            Sql += "='"
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "'"
            Sql += " AND"
            Sql += " 採番キー = '" & key & "'"

            _db.executeDB(Sql)

            Return saibanID
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Function

    'param1：String テーブル名
    'param2：String 詳細条件
    'Return: DataSet
    Private Function getDsData(ByVal tableName As String, Optional ByRef txtParam As String = "") As DataSet
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Sql += "SELECT * FROM public." & tableName
        Sql += " WHERE "
        Sql += "会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += txtParam
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