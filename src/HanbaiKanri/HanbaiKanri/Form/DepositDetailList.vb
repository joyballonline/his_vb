Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Text.RegularExpressions
Imports System.Globalization

Public Class DepositDetailList
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
    Private OrderingNo As String()
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
        _parentForm = prmRefForm
        _status = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        openDatetime = DateTime.Now
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True
        DgvBilling.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells
    End Sub

    '画面表示時
    Private Sub DepositDetailList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _status = CommonConst.STATUS_VIEW Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If

        ElseIf _status = CommonConst.STATUS_CANCEL Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "CancelMode"
            Else
                LblMode.Text = "取消モード"
            End If

            BtnDepositCancel.Visible = True
            BtnDepositCancel.Location = New Point(997, 509)
        End If

        '検索（Date）の初期値
        dtBillingDateSince.Value = DateAdd("d", CommonConst.SINCE_DEFAULT_DAY, DateTime.Today)
        dtBillingDateUntil.Value = DateTime.Today

        'データ描画
        setDgvBilling()

        '翻訳
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblConditions.Text = "ExtractionCondition"
            Label1.Text = "CustomerName"
            Label4.Text = "CustomerCode"
            Label8.Text = "MoneyReceiptDate"
            Label7.Text = "MoneyReceiptNumber"
            LblItemName.Text = "ItemName"
            LblSpec.Text = "Spec"
            Label10.Text = "DisplayFormat"
            RbtnSlip.Text = "UnitOfVoucher"

            RbtnDetails.Text = "UnitOfDetailData"
            RbtnDetails.Location = New Point(166, 196)
            ChkCancelData.Text = "IncludeCancelData"

            BtnDepositSearch.Text = "Search"
            BtnDepositCancel.Text = "CancelMfMoneyReceipt"
            BtnDepositView.Text = "MoneyReceiptView"
            BtnBack.Text = "Back"
        End If
    End Sub

    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    'DgvBilling内を再描画
    Private Sub setDgvBilling()
        clearDGV() 'テーブルクリア

        Dim ds As DataSet
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Dim curds As DataSet  'm25_currency
        Dim cur As String

        Try

            '伝票単位
            If RbtnSlip.Checked Then

                Sql = " SELECT "
                Sql += " t25.通貨,t25.入金番号,t25.入金日,t25.請求先名,t25.振込先"
                Sql += ",t25.入金額計_外貨,t25.入金額,t25.更新日,t25.備考"
                Sql += ",t25.更新日,t25.備考"
                Sql += ",t25.取消区分"
                Sql += ",m01.銀行コード , m01.銀行名 , m01.支店名, m01.預金種目 , m01.口座番号, m01.口座名義 "


                Sql += " FROM t25_nkinhd t25 "

                Sql += " INNER JOIN m01_company m01 "
                Sql += " ON t25.会社コード = m01.会社コード "

                Sql += " INNER JOIN t27_nkinkshihd t27 "
                Sql += " ON t27.会社コード = t25.会社コード "
                Sql += " AND t27.入金番号 = t25.入金番号 "

                Sql += " INNER JOIN t23_skyuhd t23 "
                Sql += " ON t27.会社コード = t23.会社コード "
                Sql += " AND t27.請求番号 = t23.請求番号 "

                Sql += " INNER JOIN t10_cymnhd t10"
                Sql += " ON t23.会社コード = t10.会社コード "
                Sql += " AND t23.受注番号 = t10.受注番号"
                Sql += " AND t23.受注番号枝番 = t10.受注番号枝番"
                Sql += " AND t10.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '受注取消されていないデータ

                Sql += " INNER JOIN t11_cymndt t11"
                Sql += " ON t10.会社コード = t11.会社コード"
                Sql += " AND t10.受注番号 = t11.受注番号"
                Sql += " AND t10.受注番号枝番 = t11.受注番号枝番"

                Sql += " WHERE t25.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

                Sql += viewSearchConditions() '抽出条件取得

                Sql += " GROUP BY "
                Sql += " t25.通貨,t25.入金番号,t25.入金日,t25.請求先名,t25.振込先"
                Sql += ",t25.入金額計_外貨,t25.入金額,t25.更新日,t25.備考"
                Sql += ",t25.更新日,t25.備考"
                Sql += ",t25.取消区分"
                Sql += ",m01.銀行コード , m01.銀行名 , m01.支店名, m01.預金種目 , m01.口座番号, m01.口座名義 "

                Sql += " ORDER BY t25.更新日 DESC"

                ds = _db.selectDB(Sql, RS, reccnt)

                '英語の表記
                If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                    DgvBilling.Columns.Add("取消", "Cancel")
                    DgvBilling.Columns.Add("入金番号", "MoneyReceiptNumber")
                    DgvBilling.Columns.Add("入金日", "MoneyReceiptDate")
                    DgvBilling.Columns.Add("請求先名", "BillingAddress")
                    DgvBilling.Columns.Add("振込先", "PaymentDestination")
                    DgvBilling.Columns.Add("通貨_外貨", "Currency")
                    DgvBilling.Columns.Add("入金額_外貨", "MoneyReceiptAmount")
                    DgvBilling.Columns.Add("通貨", "Currency")
                    DgvBilling.Columns.Add("入金額", "MoneyReceiptAmount")
                    DgvBilling.Columns.Add("更新日", "UpdateDate")
                    DgvBilling.Columns.Add("備考", "Remarks")
                Else
                    DgvBilling.Columns.Add("取消", "取消")
                    DgvBilling.Columns.Add("入金番号", "入金番号")
                    DgvBilling.Columns.Add("入金日", "入金日")
                    DgvBilling.Columns.Add("請求先名", "請求先名")
                    DgvBilling.Columns.Add("振込先", "振込先")
                    DgvBilling.Columns.Add("通貨_外貨", "通貨")
                    DgvBilling.Columns.Add("入金額_外貨", "入金額(外貨)")
                    DgvBilling.Columns.Add("通貨", "通貨")
                    DgvBilling.Columns.Add("入金額", "入金額")
                    DgvBilling.Columns.Add("更新日", "更新日")
                    DgvBilling.Columns.Add("備考", "備考")
                End If

                '伝票単位時のセル書式
                DgvBilling.Columns("入金額_外貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvBilling.Columns("入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                '数字形式
                DgvBilling.Columns("入金額_外貨").DefaultCellStyle.Format = "N2"
                DgvBilling.Columns("入金額").DefaultCellStyle.Format = "N2"


                For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1

                    If IsDBNull(ds.Tables(RS).Rows(0)("通貨")) Then
                        cur = vbNullString
                    Else
                        Sql = " and 採番キー = " & ds.Tables(RS).Rows(0)("通貨")
                        curds = getDsData("m25_currency", Sql)

                        cur = curds.Tables(RS).Rows(0)("通貨コード")
                    End If

                    Dim dsHanyo As DataSet = getDsHanyoData(CommonConst.DC_CODE, ds.Tables(RS).Rows(i)("預金種目"))
                    Dim dcName As String = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                                                dsHanyo.Tables(RS).Rows(0)("文字２"),
                                                                dsHanyo.Tables(RS).Rows(0)("文字１"))

                    DgvBilling.Rows.Add()
                    DgvBilling.Rows(i).Cells("取消").Value = getDelKbnTxt(ds.Tables(RS).Rows(i)("取消区分"))
                    DgvBilling.Rows(i).Cells("入金番号").Value = ds.Tables(RS).Rows(i)("入金番号")
                    DgvBilling.Rows(i).Cells("入金日").Value = ds.Tables(RS).Rows(i)("入金日").ToShortDateString
                    DgvBilling.Rows(i).Cells("請求先名").Value = ds.Tables(RS).Rows(i)("請求先名")
                    DgvBilling.Rows(i).Cells("振込先").Value = ds.Tables(RS).Rows(i)("振込先")
                    DgvBilling.Rows(i).Cells("振込先").Value = ds.Tables(RS).Rows(0)("銀行名") & " " &
                                                                    ds.Tables(RS).Rows(0)("支店名") & " " &
                                                                    dcName & " " &
                                                                    ds.Tables(RS).Rows(0)("口座番号") & " " &
                                                                    ds.Tables(RS).Rows(0)("口座名義")

                    DgvBilling.Rows(i).Cells("通貨_外貨").Value = cur
                    DgvBilling.Rows(i).Cells("入金額_外貨").Value = ds.Tables(RS).Rows(i)("入金額計_外貨")
                    DgvBilling.Rows(i).Cells("通貨").Value = setBaseCurrency()
                    DgvBilling.Rows(i).Cells("入金額").Value = ds.Tables(RS).Rows(i)("入金額")
                    DgvBilling.Rows(i).Cells("更新日").Value = ds.Tables(RS).Rows(i)("更新日")
                    DgvBilling.Rows(i).Cells("備考").Value = ds.Tables(RS).Rows(i)("備考")
                Next

            Else '明細単位

                Sql = " SELECT t27.* "
                Sql += " FROM t26_nkindt t26 "

                Sql += " INNER JOIN t25_nkinhd t25 "
                Sql += " ON t26.会社コード = t25.会社コード "
                Sql += " AND t26.入金番号 = t25.入金番号 "

                Sql += " INNER JOIN t27_nkinkshihd t27 "
                Sql += " ON t27.会社コード = t25.会社コード "
                Sql += " AND t27.入金番号 = t25.入金番号 "

                Sql += " INNER JOIN t23_skyuhd t23 "
                Sql += " ON t27.会社コード = t23.会社コード "
                Sql += " AND t27.請求番号 = t23.請求番号 "

                Sql += " INNER JOIN t10_cymnhd t10"
                Sql += " ON t23.会社コード = t10.会社コード "
                Sql += " AND t23.受注番号 = t10.受注番号"
                Sql += " AND t23.受注番号枝番 = t10.受注番号枝番"
                Sql += " AND t10.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '受注取消されていないデータ

                Sql += " INNER JOIN t11_cymndt t11"
                Sql += " ON t10.会社コード = t11.会社コード"
                Sql += " AND t10.受注番号 = t11.受注番号"
                Sql += " AND t10.受注番号枝番 = t11.受注番号枝番"

                Sql += " WHERE t25.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

                Sql += viewSearchConditions() '抽出条件取得

                Sql += " ORDER BY t27.更新日 DESC"

                ds = _db.selectDB(Sql, RS, reccnt)

                If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                    DgvBilling.Columns.Add("取消", CommonConst.STATUS_CANCEL)
                    DgvBilling.Columns.Add("入金番号", "MoneyReceiptNumber")
                    DgvBilling.Columns.Add("請求番号", "BillingNumber")
                    DgvBilling.Columns.Add("入金日", "MoneyReceiptDate")
                    DgvBilling.Columns.Add("請求先名", "BillingAddress")
                    DgvBilling.Columns.Add("入金額", "MoneyReceiptAmount")
                    DgvBilling.Columns.Add("備考", "Remarks")
                Else
                    DgvBilling.Columns.Add("取消", "取消")
                    DgvBilling.Columns.Add("入金番号", "入金番号")
                    DgvBilling.Columns.Add("請求番号", "請求番号")
                    DgvBilling.Columns.Add("入金日", "入金日")
                    DgvBilling.Columns.Add("請求先名", "請求先名")
                    DgvBilling.Columns.Add("入金額", "入金額")
                    DgvBilling.Columns.Add("備考", "備考")
                End If

                '伝票単位時のセル書式
                DgvBilling.Columns("入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                    DgvBilling.Rows.Add()
                    DgvBilling.Rows(i).Cells("取消").Value = getDelKbnTxt(ds.Tables(RS).Rows(i)("取消区分"))
                    DgvBilling.Rows(i).Cells("入金番号").Value = ds.Tables(RS).Rows(i)("入金番号")
                    DgvBilling.Rows(i).Cells("請求番号").Value = ds.Tables(RS).Rows(i)("請求番号")
                    DgvBilling.Rows(i).Cells("入金日").Value = ds.Tables(RS).Rows(i)("入金日").ToShortDateString
                    DgvBilling.Rows(i).Cells("請求先名").Value = ds.Tables(RS).Rows(i)("請求先名")
                    DgvBilling.Rows(i).Cells("入金額").Value = ds.Tables(RS).Rows(i)("入金消込額計")
                    DgvBilling.Rows(i).Cells("備考").Value = ds.Tables(RS).Rows(i)("備考")
                Next

                '数字形式
                DgvBilling.Columns("入金額").DefaultCellStyle.Format = "N2"

            End If

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '表示形式を切り替えたら
    Private Sub RbtnDetails_CheckedChanged(sender As Object, e As EventArgs) Handles RbtnDetails.CheckedChanged
        setDgvBilling()
    End Sub

    '検索ボタンをクリックしたら
    Private Sub BtnPurchaseSearch_Click(sender As Object, e As EventArgs) Handles BtnDepositSearch.Click
        setDgvBilling()
    End Sub

    '「取消データを含める」変更イベント取得時
    Private Sub ChkCancelData_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCancelData.CheckedChanged
        setDgvBilling()
    End Sub

    '入金取消処理
    Private Sub BtnBillingCancel_Click(sender As Object, e As EventArgs) Handles BtnDepositCancel.Click

        '明細表示時、または対象データがない場合は取消操作不可能
        If RbtnDetails.Checked Or DgvBilling.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("chkDetailsCancel", frmC01F10_Login.loginValue.Language)
            Return

        End If

        '明細表示時は取消操作不可能
        If RbtnDetails.Checked Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Return
        End If

        '取消済みデータは取消操作不可能
        If DgvBilling.Rows(DgvBilling.CurrentCell.RowIndex).Cells("取消").Value =
            IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_JPN, CommonConst.CANCEL_KBN_JPN_TXT, CommonConst.CANCEL_KBN_ENG_TXT) Then
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
    't25_nkinhd, t27_nkinkshihd, t23_skyuhd
    Private Sub updateData()
        Dim dtNow As String = UtilClass.formatDatetime(DateTime.Now)
        Dim Sql As String = ""
        Dim ds As DataSet

        Sql = " AND 入金番号 ='" & DgvBilling.Rows(DgvBilling.CurrentCell.RowIndex).Cells("入金番号").Value & "' "

        '画面を開いた時から対象データに対して更新がされていないかどうか確認
        ds = getDsData("t25_nkinhd", Sql)

        If ds.Tables(RS).Rows(0)("更新日") = DgvBilling.Rows(DgvBilling.CurrentCell.RowIndex).Cells("更新日").Value Then


#Region "t25_nkinhd"

            Sql = "UPDATE Public.t25_nkinhd "
            Sql += " SET "

            Sql += "取消区分 = '" & CommonConst.CANCEL_KBN_DISABLED & "'"
            Sql += ", 取消日 = '" & dtNow & "' "
            Sql += ", 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "' "
            Sql += ", 更新日 = '" & dtNow & "'"

            Sql += "WHERE 会社コード ='" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND 入金番号 ='" & DgvBilling.Rows(DgvBilling.CurrentCell.RowIndex).Cells("入金番号").Value & "'"

            '入金基本を更新
            _db.executeDB(Sql)

#End Region


#Region "t27_nkinkshihd"

            Sql = "UPDATE Public.t27_nkinkshihd "
            Sql += "SET "

            Sql += "取消区分 = '" & CommonConst.CANCEL_KBN_DISABLED & "'"
            Sql += ", 取消日 = '" & dtNow & "'"
            Sql += ", 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
            Sql += ", 更新日 = '" & dtNow & "'"

            Sql += "WHERE 会社コード ='" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND 入金番号 ='" & DgvBilling.Rows(DgvBilling.CurrentCell.RowIndex).Cells("入金番号").Value & "'"

            '入金消込基本を更新
            _db.executeDB(Sql)

#End Region


            'Sql = " AND 入金番号 ='" & DgvBilling.Rows(DgvBilling.CurrentCell.RowIndex).Cells("入金番号").Value & "'"

            ''入金基本から入金額を取得
            'Dim dsNkinhd As DataSet = getDsData("t25_nkinhd", Sql)
            'Dim strNyukinGaku As Decimal = dsNkinhd.Tables(RS).Rows(0)("入金額")
            'Dim strNyukinGaku_g As Decimal = dsNkinhd.Tables(RS).Rows(0)("入金額計_外貨")


            't27_nkinkshihd
            Sql = " AND 入金番号 ='" & DgvBilling.Rows(DgvBilling.CurrentCell.RowIndex).Cells("入金番号").Value & "'"

            '入金消込から請求番号を取得
            Dim dsNkinkshihd As DataSet = getDsData("t27_nkinkshihd", Sql)


            '入金消込を請求書データ分ループ
            For i As Integer = 0 To dsNkinkshihd.Tables(RS).Rows.Count - 1


#Region "t23_skyuhd"

                Dim strNyukinGaku As Decimal = dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計")
                Dim strNyukinGaku_g As Decimal = dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計_外貨")


                Sql = " AND 請求番号 ='" & dsNkinkshihd.Tables(RS).Rows(i)("請求番号") & "'"

                '請求基本から受注番号を取得
                Dim dsSkyuhd As DataSet = getDsData("t23_skyuhd", Sql)


                Dim decUrikakeZan As Decimal = dsSkyuhd.Tables(RS).Rows(0)("売掛残高") + strNyukinGaku
                Dim decUrikakeZan_g As Decimal = dsSkyuhd.Tables(RS).Rows(0)("売掛残高_外貨") + strNyukinGaku_g

                Dim decNyukinKei As Decimal = dsSkyuhd.Tables(RS).Rows(0)("入金額計") - strNyukinGaku
                Dim decNyukinKei_g As Decimal = dsSkyuhd.Tables(RS).Rows(0)("入金額計_外貨") - strNyukinGaku_g


                Dim nfi As NumberFormatInfo = New CultureInfo(CommonConst.CI_JP, False).NumberFormat


                Sql = "UPDATE Public.t23_skyuhd "
                Sql += "SET "

                Sql += "  売掛残高 = " & UtilClass.formatNumber(decUrikakeZan)      '売掛残高を増やす
                Sql += ", 売掛残高_外貨 = " & UtilClass.formatNumber(decUrikakeZan_g)

                Sql += ", 入金額計 = " & UtilClass.formatNumber(decNyukinKei)      '入金額計を減らす
                Sql += ", 入金額計_外貨 = " & UtilClass.formatNumber(decNyukinKei_g)

                Sql += ", 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
                Sql += ", 更新日 = '" & dtNow & "'"

                '売掛が残るなら入金完了日は削除する
                If dsSkyuhd.Tables(RS).Rows(0)("請求金額計") <> FormatNumber(decNyukinKei) Then
                    Sql += ", 入金完了日 = NULL "
                End If

                Sql += " WHERE 会社コード='" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " AND 請求番号 ='" & dsNkinkshihd.Tables(RS).Rows(i)("請求番号") & "' "

                '請求基本を更新
                _db.executeDB(Sql)

#End Region

            Next


#Region "t80_shiwakenyu"

            't80_shiwakenyu
            Sql = "UPDATE Public.t80_shiwakenyu "
            Sql += " SET "

            Sql += "取消区分 = '" & CommonConst.CANCEL_KBN_DISABLED & "'"
            Sql += ", 取消日 = '" & dtNow & "' "
            Sql += ", 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "' "
            Sql += ", 更新日 = '" & dtNow & "'"

            Sql += "WHERE 会社コード ='" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND 入金番号 ='" & DgvBilling.Rows(DgvBilling.CurrentCell.RowIndex).Cells("入金番号").Value & "'"

            '入金基本を更新
            _db.executeDB(Sql)

#End Region

            setDgvBilling()

        Else

            '画面を開いたときの日時とデータの日時が異なっていた場合
            'データが誰かに変更された旨を伝える
            _msgHd.dspMSG("chkData", frmC01F10_Login.loginValue.Language)

            '表示データを更新
            setDgvBilling()


        End If

    End Sub

    'テーブルをクリア
    Private Sub clearDGV()
        DgvBilling.Rows.Clear()
        DgvBilling.Columns.Clear()
    End Sub

    'sqlで実行する文字列からシングルクォーテーションを文字コードにする
    Private Function escapeSql(ByVal prmSql As String) As String
        Dim sql As String = prmSql

        sql = sql.Replace("'"c, "''") 'シングルクォーテーションを置換

        Return Regex.Escape(sql)
        Return sql
    End Function

    '抽出条件取得
    Private Function viewSearchConditions() As String
        Dim Sql As String = ""

        '抽出条件
        Dim customerName As String = escapeSql(TxtCustomerName.Text)
        Dim customerCode As String = escapeSql(TxtCustomerCode.Text)
        Dim sinceDate As String = UtilClass.strFormatDate(dtBillingDateSince.Text) '日付の書式を日本の形式に合わせる
        Dim untilDate As String = UtilClass.strFormatDate(dtBillingDateUntil.Text) '日付の書式を日本の形式に合わせる
        Dim sinceNum As String = escapeSql(TxtBillingNo1.Text)
        Dim itemName As String = UtilClass.escapeSql(TxtItemName.Text)
        Dim spec As String = UtilClass.escapeSql(TxtSpec.Text)

        If customerName <> Nothing Then
            Sql += " AND t25.請求先名 ILIKE '%" & customerName & "%' "
        End If

        If customerCode <> Nothing Then
            Sql += " AND t25.請求先コード ILIKE '%" & customerCode & "%' "
        End If

        If sinceDate <> Nothing Then
            Sql += " AND t25.入金日 >= '" & sinceDate & "'"
        End If
        If untilDate <> Nothing Then
            Sql += " AND t25.入金日 <= '" & untilDate & "'"
        End If

        If sinceNum <> Nothing Then
            Sql += " AND t25.入金番号 ILIKE '%" & sinceNum & "%' "
        End If

        If itemName <> Nothing Then
            Sql += " AND t11.品名 ILIKE '%" & itemName & "%' "
        End If

        If spec <> Nothing Then
            Sql += " AND t11.型式 ILIKE '%" & spec & "%' "
        End If

        '取消データを含めない場合
        If ChkCancelData.Checked = False Then
            Sql += " AND t25.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
        End If

        Return Sql
    End Function

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

        Sql = " AND 固定キー = '" & prmFixed & "'"

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