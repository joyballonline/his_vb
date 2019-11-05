Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Globalization
Imports System.Text.RegularExpressions

Public Class PaidList
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
    Private _parentForm As Form
    'Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ
    Private CompanyCode As String = ""
    Private OrderNo As String()
    Private _status As String = ""
    Private openDatetime As DateTime

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
                   Optional ByRef prmRefStatus As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _status = prmRefStatus
        _parentForm = prmRefForm
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True
        DgvHtyhd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells
    End Sub


    '画面表示時
    Private Sub PaidList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _status = CommonConst.STATUS_CANCEL Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "CancelMode"
            Else
                LblMode.Text = "取消モード"
            End If

            BtnCancel.Visible = True
        Else
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If
        End If

        '検索（Date）の初期値
        dtPaidDateSince.Value = DateAdd("d", CommonConst.SINCE_DEFAULT_DAY, DateTime.Today)
        dtPaidDateUntil.Value = DateTime.Today

        'データ描画
        setDgvHtyhd()

        '翻訳
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblConditions.Text = "ExtractionCondition"
            Label1.Text = "SupplierName"
            Label4.Text = "SupplierCode"
            Label8.Text = "PaymentDate"
            Label7.Text = "PaymentNumber"
            LblItemName.Text = "ItemName"
            LblSpec.Text = "Spec"
            Label10.Text = "DisplayFormat"
            RbtnSlip.Text = "UnitOfVoucher"

            RbtnDetails.Text = "LineItemUnit"
            RbtnDetails.Location = New Point(166, 196)
            ChkCancelData.Text = "IncludeCancelData"

            BtnPaymentSearch.Text = "Search"
            BtnCancel.Text = "CancelOfPayment"
            BtnBack.Text = "Back"
        End If
    End Sub

    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub


    'DgvHtyhd内を再描画
    Private Sub setDgvHtyhd()
        clearDGV() 'テーブルクリア
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""
        Dim curds As DataSet  'm25_currency
        Dim cur As String


        Try
            '伝票単位
            If RbtnSlip.Checked Then

                Sql = " SELECT"
                Sql += " t47.通貨,t47.支払番号,t47.支払日,t47.支払先名,t47.支払金額計_外貨,t47.支払金額計"
                Sql += ",t47.更新日,t47.備考, t46.支払予定日"
                Sql += ",t47.取消区分"
                Sql += ",m11.銀行コード , m11.銀行名 , m11.支店名, m11.預金種目 , m11.口座番号, m11.口座名義"

                Sql += " FROM t47_shrihd t47 "
                Sql += " INNER JOIN m11_supplier m11 "
                Sql += " ON t47.会社コード = m11.会社コード "
                Sql += " AND t47.支払先コード = m11.仕入先コード "

                Sql += " INNER JOIN t49_shrikshihd t49 "
                Sql += " ON t47.会社コード = t49.会社コード "
                Sql += " AND t47.支払番号 = t49.支払番号 "

                Sql += " INNER JOIN t46_kikehd t46 "
                Sql += " ON t49.会社コード = t46.会社コード "
                Sql += " AND t49.買掛番号 = t46.買掛番号 "
                Sql += " AND "
                Sql += " t46.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '買掛取消されていないデータ

                Sql += " INNER JOIN "
                Sql += " t20_hattyu t20"
                Sql += " ON "
                Sql += " t46.会社コード = t20.会社コード "
                Sql += " AND "
                Sql += " t46.発注番号 = t20.発注番号"
                Sql += " AND "
                Sql += " t46.発注番号枝番 = t20.発注番号枝番"
                Sql += " AND "
                Sql += " t20.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '発注取消されていないデータ

                Sql += " WHERE t47.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

                Sql += viewSearchConditions() '抽出条件取得

                Sql += " GROUP BY "
                Sql += " t47.通貨,t47.支払番号,t47.支払日,t47.支払先名,t47.支払金額計_外貨,t47.支払金額計"
                Sql += ",t47.更新日,t47.備考,t46.支払予定日"
                Sql += ",t47.取消区分"
                Sql += ",m11.銀行コード , m11.銀行名 , m11.支店名, m11.預金種目 , m11.口座番号, m11.口座名義"

                Sql += " ORDER BY "
                Sql += "t47.更新日 DESC"

                ds = _db.selectDB(Sql, RS, reccnt)

                '英語の表記
                If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                    DgvHtyhd.Columns.Add("取消", "Cancel")
                    DgvHtyhd.Columns.Add("支払番号", "PaymentNumber")
                    DgvHtyhd.Columns.Add("支払日", "PaymentDate")
                    DgvHtyhd.Columns.Add("支払予定日", "PaymentDueDate")
                    DgvHtyhd.Columns.Add("支払先名", "SupplierName")
                    DgvHtyhd.Columns.Add("支払先", "PaymentDestination")
                    DgvHtyhd.Columns.Add("通貨_外貨", "Currency")
                    DgvHtyhd.Columns.Add("支払金額計_外貨", "TotalPaymentAmountOrignalCurrency")
                    DgvHtyhd.Columns.Add("通貨", "Currency")
                    DgvHtyhd.Columns.Add("支払金額計", "TotalPaymentAmount")
                    DgvHtyhd.Columns.Add("更新日", "UpdateDate")
                    DgvHtyhd.Columns.Add("備考", "Remarks")
                Else
                    DgvHtyhd.Columns.Add("取消", "取消")
                    DgvHtyhd.Columns.Add("支払番号", "支払番号")
                    DgvHtyhd.Columns.Add("支払日", "支払日")
                    DgvHtyhd.Columns.Add("支払予定日", "支払予定日")
                    DgvHtyhd.Columns.Add("支払先名", "支払先名")
                    DgvHtyhd.Columns.Add("支払先", "支払先")
                    DgvHtyhd.Columns.Add("通貨_外貨", "通貨(外貨)")
                    DgvHtyhd.Columns.Add("支払金額計_外貨", "支払金額計(外貨)")
                    DgvHtyhd.Columns.Add("通貨", "通貨")
                    DgvHtyhd.Columns.Add("支払金額計", "支払金額計")
                    DgvHtyhd.Columns.Add("更新日", "更新日")
                    DgvHtyhd.Columns.Add("備考", "備考")
                End If

                '伝票単位時のセル書式
                DgvHtyhd.Columns("支払金額計_外貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvHtyhd.Columns("支払金額計").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                '数字形式
                DgvHtyhd.Columns("支払金額計_外貨").DefaultCellStyle.Format = "N2"
                DgvHtyhd.Columns("支払金額計").DefaultCellStyle.Format = "N2"


                For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1

                    If IsDBNull(ds.Tables(RS).Rows(index)("通貨")) Then
                        cur = vbNullString
                    Else
                        Sql = " and 採番キー = " & ds.Tables(RS).Rows(index)("通貨")
                        curds = getDsData("m25_currency", Sql)

                        cur = curds.Tables(RS).Rows(0)("通貨コード")
                    End If

                    Dim dcName As String

                    If IsDBNull(ds.Tables(RS).Rows(index)("預金種目")) Then
                        dcName = vbNullString
                    Else
                        Dim dsHanyo As DataSet = getDsHanyoData(CommonConst.DC_CODE, ds.Tables(RS).Rows(index)("預金種目"))
                        dcName = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                                                    dsHanyo.Tables(RS).Rows(0)("文字２"),
                                                                    dsHanyo.Tables(RS).Rows(0)("文字１"))
                    End If

                    DgvHtyhd.Rows.Add()
                    DgvHtyhd.Rows(index).Cells("取消").Value = getDelKbnTxt(ds.Tables(RS).Rows(index)("取消区分"))
                    DgvHtyhd.Rows(index).Cells("支払番号").Value = ds.Tables(RS).Rows(index)("支払番号")
                    DgvHtyhd.Rows(index).Cells("支払日").Value = ds.Tables(RS).Rows(index)("支払日").ToShortDateString
                    DgvHtyhd.Rows(index).Cells("支払先名").Value = ds.Tables(RS).Rows(index)("支払先名")
                    DgvHtyhd.Rows(index).Cells("支払先").Value = ds.Tables(RS).Rows(index)("銀行名") & " " &
                                                                    ds.Tables(RS).Rows(index)("支店名") & " " &
                                                                    dcName & " " &
                                                                    ds.Tables(RS).Rows(index)("口座番号") & " " &
                                                                    ds.Tables(RS).Rows(index)("口座名義")

                    DgvHtyhd.Rows(index).Cells("通貨_外貨").Value = cur
                    DgvHtyhd.Rows(index).Cells("支払金額計_外貨").Value = ds.Tables(RS).Rows(index)("支払金額計_外貨")
                    DgvHtyhd.Rows(index).Cells("通貨").Value = setBaseCurrency
                    DgvHtyhd.Rows(index).Cells("支払金額計").Value = ds.Tables(RS).Rows(index)("支払金額計")
                    DgvHtyhd.Rows(index).Cells("更新日").Value = ds.Tables(RS).Rows(index)("更新日")
                    DgvHtyhd.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
                    DgvHtyhd.Rows(index).Cells("支払予定日").Value = ds.Tables(RS).Rows(index)("支払予定日").ToShortDateString

                Next

            Else '明細単位

                Sql = " SELECT t48.*, t47.取消区分, t46.買掛番号"
                Sql += " FROM t48_shridt t48 "

                Sql += " INNER JOIN t47_shrihd t47 "
                Sql += " ON t48.会社コード = t47.会社コード "
                Sql += " AND t48.支払番号 = t47.支払番号 "

                Sql += " INNER JOIN t49_shrikshihd t49 "
                Sql += " ON t48.会社コード = t49.会社コード "
                Sql += " AND t48.支払番号 = t49.支払番号 "

                Sql += " INNER JOIN t46_kikehd t46 "
                Sql += " ON t49.会社コード = t46.会社コード "
                Sql += " AND t49.買掛番号 = t46.買掛番号 "
                Sql += " AND "
                Sql += " t46.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '買掛取消されていないデータ

                Sql += " INNER JOIN "
                Sql += " t20_hattyu t20"
                Sql += " ON "
                Sql += " t46.会社コード = t20.会社コード "
                Sql += " AND "
                Sql += " t46.発注番号 = t20.発注番号"
                Sql += " AND "
                Sql += " t46.発注番号枝番 = t20.発注番号枝番"
                Sql += " AND "
                Sql += " t20.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '発注取消されていないデータ

                'Sql += " INNER JOIN "
                'Sql += " t21_hattyu t21"
                'Sql += " ON "
                'Sql += " t20.会社コード = t21.会社コード "
                'Sql += " AND "
                'Sql += " t20.発注番号 = t21.発注番号"
                'Sql += " AND "
                'Sql += " t20.発注番号枝番 = t21.発注番号枝番"

                Sql += " WHERE t47.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

                Sql += viewSearchConditions() '抽出条件取得

                Sql += " ORDER BY "
                Sql += "t49.更新日 DESC, t49.支払番号"

                Console.WriteLine(Sql)

                ds = _db.selectDB(Sql, RS, reccnt)

                '英語の表記
                If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                    DgvHtyhd.Columns.Add("取消", "Cancel")
                    DgvHtyhd.Columns.Add("支払番号", "PaymentNumber")
                    DgvHtyhd.Columns.Add("買掛番号", "AccountsPayableNumber")
                    DgvHtyhd.Columns.Add("支払日", "PaymentDate")
                    DgvHtyhd.Columns.Add("支払先名", "SupplierName")
                    DgvHtyhd.Columns.Add("支払金額計", "TotalPaymentAmount")
                    DgvHtyhd.Columns.Add("備考", "Remarks")
                Else
                    DgvHtyhd.Columns.Add("取消", "取消")
                    DgvHtyhd.Columns.Add("支払番号", "支払番号")
                    DgvHtyhd.Columns.Add("支払日", "支払日")
                    DgvHtyhd.Columns.Add("支払先名", "支払先名")
                    DgvHtyhd.Columns.Add("支払金額計", "支払金額計")
                    DgvHtyhd.Columns.Add("備考", "備考")
                End If

                '伝票単位時のセル書式
                DgvHtyhd.Columns("支払金額計").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                    DgvHtyhd.Rows.Add()
                    DgvHtyhd.Rows(index).Cells("取消").Value = getDelKbnTxt(ds.Tables(RS).Rows(index)("取消区分"))
                    DgvHtyhd.Rows(index).Cells("買掛番号").Value = ds.Tables(RS).Rows(index)("買掛番号")
                    DgvHtyhd.Rows(index).Cells("支払番号").Value = ds.Tables(RS).Rows(index)("支払番号")
                    DgvHtyhd.Rows(index).Cells("支払日").Value = ds.Tables(RS).Rows(index)("支払日").ToShortDateString
                    DgvHtyhd.Rows(index).Cells("支払先名").Value = ds.Tables(RS).Rows(index)("支払先名")
                    DgvHtyhd.Rows(index).Cells("支払金額計").Value = ds.Tables(RS).Rows(index)("支払金額_外貨")
                    DgvHtyhd.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
                Next

            End If

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '抽出条件取得
    Private Function viewSearchConditions() As String
        Dim Sql As String = ""

        '抽出条件
        Dim customerName As String = UtilClass.escapeSql(TxtSupplierName.Text)
        Dim customerCode As String = UtilClass.escapeSql(TxtSupplierCode.Text)
        Dim sinceDate As String = UtilClass.strFormatDate(dtPaidDateSince.Text)
        Dim untilDate As String = UtilClass.strFormatDate(dtPaidDateUntil.Text)
        Dim sinceNum As String = UtilClass.escapeSql(TxtPaidNoSince.Text)
        Dim itemName As String = UtilClass.escapeSql(TxtItemName.Text)
        Dim spec As String = UtilClass.escapeSql(TxtSpec.Text)

        If customerName <> Nothing Then
            Sql += " AND "
            Sql += " t47.支払先名 ILIKE '%" & customerName & "%' "
        End If

        If customerCode <> Nothing Then
            Sql += " AND "
            Sql += " t47.支払先コード ILIKE '%" & customerCode & "%' "
        End If

        If sinceDate <> Nothing Then
            Sql += " AND "
            Sql += " t47.支払日 >= '" & sinceDate & "'"
        End If
        If untilDate <> Nothing Then
            Sql += " AND "
            Sql += " t47.支払日 <= '" & untilDate & "'"
        End If

        If sinceNum <> Nothing Then
            Sql += " AND "
            Sql += " t47.支払番号 ILIKE '%" & sinceNum & "%' "
        End If

        If itemName <> Nothing Then
            Sql += " AND "
            Sql += " t21.品名 ILIKE '%" & itemName & "%' "
        End If

        If spec <> Nothing Then
            Sql += " AND "
            Sql += " t21.型式 ILIKE '%" & spec & "%' "
        End If

        '取消データを含めない場合
        If ChkCancelData.Checked = False Then
            Sql += " AND "
            Sql += "t47.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
        End If

        Return Sql
    End Function

    '表示形式を切り替えたら
    Private Sub RbtnDetails_CheckedChanged(sender As Object, e As EventArgs) Handles RbtnDetails.CheckedChanged
        setDgvHtyhd() '一覧再取得
    End Sub

    '検索ボタンをクリックしたら
    Private Sub ChkCancelData_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCancelData.CheckedChanged
        setDgvHtyhd() '一覧再取得
    End Sub

    '「取消データを含める」変更イベント取得時
    Private Sub BtnPurchaseSearch_Click(sender As Object, e As EventArgs) Handles BtnPaymentSearch.Click
        setDgvHtyhd() '一覧再取得
    End Sub


    '支払取消処理
    Private Sub BtnPurchaseCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click

        '明細表示時、または対象データがない場合は取消操作不可能
        If RbtnDetails.Checked Or DgvHtyhd.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Return

        End If

        '取消済みデータは取消操作不可能
        If DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("取消").Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_JPN, CommonConst.CANCEL_KBN_JPN_TXT, CommonConst.CANCEL_KBN_ENG_TXT) Then
            '取消データは選択できないアラートを出す
            _msgHd.dspMSG("cannotSelectTorikeshiData", frmC01F10_Login.loginValue.Language)
            Return
        End If

        Try

            '取消確認のアラート
            Dim result As DialogResult = _msgHd.dspMSG("confirmCancel", frmC01F10_Login.loginValue.Language)

            If result = DialogResult.Yes Then
                updateData() 'データ更新
            End If

        Catch ex As Exception

            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))

        End Try


    End Sub

    '選択データをもとに以下テーブル更新
    't47_shrihd, t49_shrikshihd, t46_kikehd
    Private Sub updateData()
        Dim dtNow As String = UtilClass.formatDatetime(DateTime.Now)
        Dim Sql As String = ""
        Dim ds As DataSet

        Sql = " AND 支払番号 ='" & DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("支払番号").Value & "'"

        '画面を開いた時から対象データに対して更新がされていないかどうか確認
        ds = getDsData("t47_shrihd", Sql)

        If ds.Tables(RS).Rows(0)("更新日") = DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("更新日").Value Then

            Sql = "UPDATE Public.t47_shrihd "
            Sql += "SET "

            Sql += "取消区分 = '" & CommonConst.CANCEL_KBN_DISABLED & "'"
            Sql += ", 取消日 = '" & dtNow & "'"
            Sql += ", 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
            Sql += ", 更新日 = '" & dtNow & "'"

            Sql += "WHERE 会社コード ='" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND 支払番号 ='" & DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("支払番号").Value & "'"

            '支払基本を更新
            _db.executeDB(Sql)

            Sql = " AND 支払番号 ='" & DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("支払番号").Value & "'"

            '支払基本から支払金額計を取得
            Dim dstShrihd As DataSet = getDsData("t47_shrihd", Sql)
            Dim strSiharaiGaku As Decimal = dstShrihd.Tables(RS).Rows(0)("支払金額計_外貨")

            Sql = " AND 支払番号 ='" & DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("支払番号").Value & "'"

            '支払消込から買掛番号を取得
            Dim dsShrikshihd As DataSet = getDsData("t49_shrikshihd", Sql)

            Sql = " AND 買掛番号 ='" & dsShrikshihd.Tables(RS).Rows(0)("買掛番号") & "' "

            '買掛基本から金額を取得
            Dim dsKikehd As DataSet = getDsData("t46_kikehd", Sql)

            Dim decKaikakeZan As Decimal = dsKikehd.Tables(RS).Rows(0)("買掛残高_外貨") + strSiharaiGaku
            Dim decSiharaiKei As Decimal = dsKikehd.Tables(RS).Rows(0)("支払金額計_外貨") - strSiharaiGaku


            Sql = "UPDATE Public.t49_shrikshihd "
            Sql += "SET "

            Sql += "取消区分 = '" & CommonConst.CANCEL_KBN_DISABLED & "'"
            Sql += ", 取消日 = '" & dtNow & "'"
            Sql += ", 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
            Sql += ", 更新日 = '" & dtNow & "'"

            Sql += "WHERE 会社コード ='" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND 支払番号 ='" & DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("支払番号").Value & "'"

            '支払消込基本を更新
            _db.executeDB(Sql)

            Sql = "UPDATE Public.t46_kikehd "
            Sql += "SET "

            Sql += "買掛残高_外貨 = " & UtilClass.formatNumber(decKaikakeZan) '買掛残高を増やす
            Sql += ", 支払金額計_外貨 = " & UtilClass.formatNumber(decSiharaiKei) '支払金額計を減らす
            Sql += ", 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
            Sql += ", 更新日 = '" & dtNow & "'"

            Sql += "WHERE 会社コード ='" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND 買掛番号 ='" & dsShrikshihd.Tables(RS).Rows(0)("買掛番号") & "'"

            '買掛基本を更新
            _db.executeDB(Sql)

            setDgvHtyhd()

        Else

            '画面を開いたときの日時とデータの日時が異なっていた場合
            'データが誰かに変更された旨を伝える
            _msgHd.dspMSG("chkData", frmC01F10_Login.loginValue.Language)

            '表示データを更新
            setDgvHtyhd()

        End If

    End Sub

    'テーブルをクリア
    Private Sub clearDGV()
        DgvHtyhd.Rows.Clear()
        DgvHtyhd.Columns.Clear()
    End Sub

    'param1：String テーブル名
    'param2：String 詳細条件
    'Return: DataSet
    Private Function getDsData(ByVal tableName As String, Optional ByRef txtParam As String = "") As DataSet
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Sql += "SELECT * FROM "

        Sql += "public." & tableName
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += txtParam

        Return _db.selectDB(Sql, RS, reccnt)
    End Function

    'sqlで実行する文字列からシングルクォーテーションを文字コードにする
    Private Function escapeSql(ByVal prmSql As String) As String
        Dim sql As String = prmSql

        sql = sql.Replace("'"c, "''") 'シングルクォーテーションを置換

        Return Regex.Escape(sql)
        Return sql
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

    '汎用マスタから固定キー、可変キーに応じた結果を返す
    'param1：String 固定キー
    'param2：String 可変キー
    'Return: DataSet
    Private Function getDsHanyoData(ByVal prmFixed As String, Optional ByVal prmVariable As String = "") As DataSet
        Dim Sql As String = ""

        Sql = " AND "
        Sql += "固定キー = '" & prmFixed & "'"

        If prmVariable IsNot "" Then
            Sql += " AND 可変キー = '" & prmVariable & "'"
        End If

        'リードタイムのリストを汎用マスタから取得
        Return getDsData("m90_hanyo", Sql)
    End Function

    '基準通貨の通貨コードを取得する
    Private Function setBaseCurrency() As String
        Dim Sql As String
        '通貨表示：ベースの設定
        Sql = " AND 採番キー = " & CommonConst.CURRENCY_CD_IDR.ToString
        Sql += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString

        Dim ds As DataSet = getDsData("m25_currency", Sql)
        'TxtIDRCurrency.Text = ds.Tables(RS).Rows(0)("通貨コード")
        setBaseCurrency = ds.Tables(RS).Rows(0)("通貨コード")

    End Function

End Class