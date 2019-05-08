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

Public Class GoodsIssueList
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
    Private GoodsNo As String()
    Private GoodsIssueStatus As String = ""


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
                  ByRef prmRefStatus As String)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
        GoodsIssueStatus = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True
        DgvCymnhd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells
    End Sub

    Private Sub getList()

        Dim Sql As String = ""
        Dim reccnt As Integer = 0 'DB用（デフォルト）

        '一覧クリア
        DgvCymnhd.Rows.Clear()
        DgvCymnhd.Columns.Clear()

        '伝票単位選択時
        If RbtnSlip.Checked Then

            '使用言語によって表示切替
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                DgvCymnhd.Columns.Add("取消", "Cancel")
                DgvCymnhd.Columns.Add("出庫番号", "GoodsDeliveryNumber")
                DgvCymnhd.Columns.Add("出庫日", "GoodsDeliveryDate")
                DgvCymnhd.Columns.Add("受注番号", "JobOrderNumber")
                DgvCymnhd.Columns.Add("受注番号枝番", "JobOrderSubNumber")
                DgvCymnhd.Columns.Add("客先番号", "CustomerNumber")
                DgvCymnhd.Columns.Add("得意先コード", "CustomerCode")
                DgvCymnhd.Columns.Add("得意先名", "CustomerName")
                DgvCymnhd.Columns.Add("得意先郵便番号", "PostalCode")
                DgvCymnhd.Columns.Add("得意先住所", "Address")
                DgvCymnhd.Columns.Add("得意先電話番号", "PhoneNumber")
                DgvCymnhd.Columns.Add("得意先ＦＡＸ", "FAX")
                DgvCymnhd.Columns.Add("得意先担当者名", "NameOfPIC")
                DgvCymnhd.Columns.Add("得意先担当者役職", "PositionPICCustomer")
                DgvCymnhd.Columns.Add("営業担当者", "SalesPersonInCharge")
                DgvCymnhd.Columns.Add("入力担当者", "PICForInputting")
                DgvCymnhd.Columns.Add("備考", "Remarks")
                DgvCymnhd.Columns.Add("登録日", "registrationDate")
            Else
                DgvCymnhd.Columns.Add("取消", "取消")
                DgvCymnhd.Columns.Add("出庫番号", "出庫番号")
                DgvCymnhd.Columns.Add("出庫日", "出庫日")
                DgvCymnhd.Columns.Add("受注番号", "受注番号")
                DgvCymnhd.Columns.Add("受注番号枝番", "受注番号枝番")
                DgvCymnhd.Columns.Add("客先番号", "客先番号")
                DgvCymnhd.Columns.Add("得意先コード", "得意先コード")
                DgvCymnhd.Columns.Add("得意先名", "得意先名")
                DgvCymnhd.Columns.Add("得意先郵便番号", "得意先郵便番号")
                DgvCymnhd.Columns.Add("得意先住所", "得意先住所")
                DgvCymnhd.Columns.Add("得意先電話番号", "得意先電話番号")
                DgvCymnhd.Columns.Add("得意先ＦＡＸ", "得意先ＦＡＸ")
                DgvCymnhd.Columns.Add("得意先担当者名", "得意先担当者名")
                DgvCymnhd.Columns.Add("得意先担当者役職", "得意先担当者役職")
                DgvCymnhd.Columns.Add("営業担当者", "営業担当者")
                DgvCymnhd.Columns.Add("入力担当者", "入力担当者")
                DgvCymnhd.Columns.Add("備考", "備考")
                DgvCymnhd.Columns.Add("登録日", "登録日")
            End If

            Try

                Sql = " SELECT t44.* "
                Sql += " FROM "
                Sql += " t44_shukohd t44"
                Sql += " INNER JOIN "
                Sql += " t45_shukodt t45 "
                Sql += " ON "
                Sql += " t44.""会社コード"" = t45.""会社コード"""
                Sql += " And "
                Sql += " t44.""出庫番号"" = t45.""出庫番号"""

                Sql += " WHERE "
                Sql += " t44.""会社コード"" = '" & frmC01F10_Login.loginValue.BumonCD & "'"

                Sql += viewSearchConditions() '検索条件

                Sql += " GROUP BY "
                Sql += " t44.会社コード, t44.出庫番号"
                Sql += " ORDER BY "
                Sql += " t44.更新日 DESC"

                ds = _db.selectDB(Sql, RS, reccnt)

                For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                    DgvCymnhd.Rows.Add()
                    DgvCymnhd.Rows(i).Cells("取消").Value = getDelKbnTxt(ds.Tables(RS).Rows(i)("取消区分"))
                    DgvCymnhd.Rows(i).Cells("出庫番号").Value = ds.Tables(RS).Rows(i)("出庫番号")
                    DgvCymnhd.Rows(i).Cells("出庫日").Value = ds.Tables(RS).Rows(i)("出庫日").ToShortDateString()
                    DgvCymnhd.Rows(i).Cells("受注番号").Value = ds.Tables(RS).Rows(i)("受注番号")
                    DgvCymnhd.Rows(i).Cells("受注番号枝番").Value = ds.Tables(RS).Rows(i)("受注番号枝番")
                    DgvCymnhd.Rows(i).Cells("客先番号").Value = ds.Tables(RS).Rows(i)("客先番号")
                    DgvCymnhd.Rows(i).Cells("得意先コード").Value = ds.Tables(RS).Rows(i)("得意先コード")
                    DgvCymnhd.Rows(i).Cells("得意先名").Value = ds.Tables(RS).Rows(i)("得意先名")
                    DgvCymnhd.Rows(i).Cells("得意先郵便番号").Value = ds.Tables(RS).Rows(i)("得意先郵便番号")
                    DgvCymnhd.Rows(i).Cells("得意先住所").Value = ds.Tables(RS).Rows(i)("得意先住所")
                    DgvCymnhd.Rows(i).Cells("得意先電話番号").Value = ds.Tables(RS).Rows(i)("得意先電話番号")
                    DgvCymnhd.Rows(i).Cells("得意先ＦＡＸ").Value = ds.Tables(RS).Rows(i)("得意先ＦＡＸ")
                    DgvCymnhd.Rows(i).Cells("得意先担当者名").Value = ds.Tables(RS).Rows(i)("得意先担当者名")
                    DgvCymnhd.Rows(i).Cells("得意先担当者役職").Value = ds.Tables(RS).Rows(i)("得意先担当者役職")
                    DgvCymnhd.Rows(i).Cells("営業担当者").Value = ds.Tables(RS).Rows(i)("営業担当者")
                    DgvCymnhd.Rows(i).Cells("入力担当者").Value = ds.Tables(RS).Rows(i)("入力担当者")
                    DgvCymnhd.Rows(i).Cells("備考").Value = ds.Tables(RS).Rows(i)("備考")
                    DgvCymnhd.Rows(i).Cells("登録日").Value = ds.Tables(RS).Rows(i)("登録日")
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
                DgvCymnhd.Columns.Add("取消", "Cancel")
                DgvCymnhd.Columns.Add("出庫番号", "GoodsDeliveryNumber")
                DgvCymnhd.Columns.Add("行番号", "LineNumber")
                DgvCymnhd.Columns.Add("出庫日", "GoodsDeliveryDate")
                DgvCymnhd.Columns.Add("受注番号", "JobOrderNumber")
                DgvCymnhd.Columns.Add("受注番号枝番", "JobOrderSubNumber")
                DgvCymnhd.Columns.Add("仕入区分", "PurchasingClassification")
                DgvCymnhd.Columns.Add("メーカー", "Manufacturer")
                DgvCymnhd.Columns.Add("品名", "ItemName")
                DgvCymnhd.Columns.Add("型式", "Spec")
                DgvCymnhd.Columns.Add("仕入先名", "SupplierName")
                DgvCymnhd.Columns.Add("出庫数量", "GoodsDeliveryQuantity")
                DgvCymnhd.Columns.Add("単位", "Unit")
                DgvCymnhd.Columns.Add("売単価", "SellingPrice")
                DgvCymnhd.Columns.Add("備考", "Remarks")
                DgvCymnhd.Columns.Add("更新者", "ModifiedBy")
                DgvCymnhd.Columns.Add("更新日", "UpdateDate")
            Else
                DgvCymnhd.Columns.Add("取消", "取消")
                DgvCymnhd.Columns.Add("出庫番号", "出庫番号")
                DgvCymnhd.Columns.Add("行番号", "行番号")
                DgvCymnhd.Columns.Add("出庫日", "出庫日")
                DgvCymnhd.Columns.Add("受注番号", "受注番号")
                DgvCymnhd.Columns.Add("受注番号枝番", "受注番号枝番")
                DgvCymnhd.Columns.Add("仕入区分", "仕入区分")
                DgvCymnhd.Columns.Add("メーカー", "メーカー")
                DgvCymnhd.Columns.Add("品名", "品名")
                DgvCymnhd.Columns.Add("型式", "型式")
                DgvCymnhd.Columns.Add("仕入先名", "仕入先名")
                DgvCymnhd.Columns.Add("出庫数量", "出庫数量")
                DgvCymnhd.Columns.Add("単位", "単位")
                DgvCymnhd.Columns.Add("売単価", "売単価")
                DgvCymnhd.Columns.Add("備考", "備考")
                DgvCymnhd.Columns.Add("更新者", "更新者")
                DgvCymnhd.Columns.Add("更新日", "更新日")
            End If

            DgvCymnhd.Columns("出庫数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns("売単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            Sql = " SELECT t45.*, t44.取消区分, t44.出庫日 "
            Sql += " FROM "
            Sql += " t45_shukodt t45"
            Sql += " INNER JOIN t44_shukohd t44 ON "

            Sql += " t45.""会社コード"" = t44.""会社コード"""
            Sql += " AND "
            Sql += " t45.""出庫番号"" = t44.""出庫番号"""

            Sql += " WHERE "
            Sql += " t45.""会社コード"" = '" & frmC01F10_Login.loginValue.BumonCD & "'"

            Sql += viewSearchConditions() '検索条件

            Sql += " GROUP BY "
            Sql += " t45.会社コード, t45.出庫番号, t44.取消区分, t44.出庫日, t45.受注番号, t45.受注番号枝番, t45.行番号,  t45.更新日"
            Sql += " ORDER BY "
            Sql += "t45.更新日 DESC"

            Try

                ds = _db.selectDB(Sql, RS, reccnt)

                For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                    '汎用マスタから仕入区分名称取得
                    Dim sireKbn As DataSet = getDsHanyoData(CommonConst.FIXED_KEY_PURCHASING_CLASS, ds.Tables(RS).Rows(i)("仕入区分").ToString)

                    DgvCymnhd.Rows.Add()
                    DgvCymnhd.Rows(i).Cells("取消").Value = getDelKbnTxt(ds.Tables(RS).Rows(i)("取消区分"))
                    DgvCymnhd.Rows(i).Cells("出庫番号").Value = ds.Tables(RS).Rows(i)("出庫番号")
                    DgvCymnhd.Rows(i).Cells("行番号").Value = ds.Tables(RS).Rows(i)("行番号")
                    DgvCymnhd.Rows(i).Cells("出庫日").Value = ds.Tables(RS).Rows(i)("出庫日").ToShortDateString()
                    DgvCymnhd.Rows(i).Cells("受注番号").Value = ds.Tables(RS).Rows(i)("受注番号")
                    DgvCymnhd.Rows(i).Cells("受注番号枝番").Value = ds.Tables(RS).Rows(i)("受注番号枝番")
                    DgvCymnhd.Rows(i).Cells("仕入区分").Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                                                sireKbn.Tables(RS).Rows(0)("文字２"),
                                                                sireKbn.Tables(RS).Rows(0)("文字１"))
                    DgvCymnhd.Rows(i).Cells("メーカー").Value = ds.Tables(RS).Rows(i)("メーカー")
                    DgvCymnhd.Rows(i).Cells("品名").Value = ds.Tables(RS).Rows(i)("品名")
                    DgvCymnhd.Rows(i).Cells("型式").Value = ds.Tables(RS).Rows(i)("型式")
                    DgvCymnhd.Rows(i).Cells("仕入先名").Value = ds.Tables(RS).Rows(i)("仕入先名")
                    DgvCymnhd.Rows(i).Cells("出庫数量").Value = ds.Tables(RS).Rows(i)("出庫数量")
                    DgvCymnhd.Rows(i).Cells("単位").Value = ds.Tables(RS).Rows(i)("単位")
                    DgvCymnhd.Rows(i).Cells("売単価").Value = ds.Tables(RS).Rows(i)("売単価")
                    DgvCymnhd.Rows(i).Cells("備考").Value = ds.Tables(RS).Rows(i)("備考")
                    DgvCymnhd.Rows(i).Cells("更新者").Value = ds.Tables(RS).Rows(i)("更新者")
                    DgvCymnhd.Rows(i).Cells("更新日").Value = ds.Tables(RS).Rows(i)("更新日")
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

    Private Function viewSearchConditions() As String
        Dim Sql As String = ""

        '抽出条件
        Dim customerNam As String = UtilClass.escapeSql(TxtCustomerName.Text)
        Dim supplierAddress As String = UtilClass.escapeSql(TxtAddress.Text)
        Dim supplierTel As String = UtilClass.escapeSql(TxtTel.Text)
        Dim customerCode As String = UtilClass.escapeSql(TxtCustomerCode.Text)
        Dim sinceDate As String = UtilClass.strFormatDate(dtDateSince.Text)
        Dim untilDate As String = UtilClass.strFormatDate(dtDateUntil.Text)
        Dim sinceNum As String = UtilClass.escapeSql(TxtGoodsSince.Text)
        Dim salesName As String = UtilClass.escapeSql(TxtSales.Text)
        Dim poNum As String = UtilClass.escapeSql(TxtCustomerPO.Text)
        Dim itemName As String = UtilClass.escapeSql(TxtItemName.Text)
        Dim spec As String = UtilClass.escapeSql(TxtSpec.Text)

        If customerNam <> Nothing Then
            Sql += " AND "
            Sql += " t44.得意先名 ILIKE '%" & customerNam & "%' "
        End If

        If supplierAddress <> Nothing Then
            Sql += " AND "
            Sql += " t44.住所 ILIKE '%" & supplierAddress & "%' "
        End If

        If supplierTel <> Nothing Then
            Sql += " AND "
            Sql += " t44.電話番号 ILIKE '%" & supplierTel & "%' "
        End If

        If customerCode <> Nothing Then
            Sql += " AND "
            Sql += " t44.得意先コード ILIKE '%" & customerCode & "%' "
        End If

        If sinceDate <> Nothing Then
            Sql += " AND "
            Sql += " t44.出庫日 >= '" & sinceDate & "'"
        End If
        If untilDate <> Nothing Then
            Sql += " AND "
            Sql += " t44.出庫日 <= '" & untilDate & "'"
        End If

        If sinceNum <> Nothing Then
            Sql += " AND "
            Sql += " t44.出庫番号 ILIKE '%" & sinceNum & "%' "
        End If

        If poNum <> Nothing Then
            Sql += " AND "
            Sql += " t45.営業担当者 ILIKE '%" & salesName & "%' "
        End If

        If poNum <> Nothing Then
            Sql += " AND "
            Sql += " t45.客先番号 ILIKE '%" & poNum & "%' "
        End If

        If itemName <> Nothing Then
            Sql += " AND "
            Sql += " t45.品名 ILIKE '%" & itemName & "%' "
        End If

        If spec <> Nothing Then
            Sql += " AND "
            Sql += " t45.型式 ILIKE '%" & spec & "%' "
        End If

        '取消データを含めない場合
        If ChkCancelData.Checked = False Then
            Sql += " AND "
            Sql += "t44.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
        End If

        Return Sql
    End Function

    '画面表示時
    Private Sub GoodsIssueList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GoodsIssueStatus = CommonConst.STATUS_CANCEL Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "CancelMode"
            Else
                LblMode.Text = "取消モード"
            End If

            BtnGoodsIssueCancel.Visible = True
            BtnGoodsIssueCancel.Location = New Point(997, 509)
        ElseIf GoodsIssueStatus = CommonConst.STATUS_VIEW Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If

            BtnGoodsIssueView.Visible = True
            BtnGoodsIssueView.Location = New Point(997, 509)
        End If

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblConditions.Text = "ExtractionCondition"
            Label1.Text = "CustomerName"
            Label2.Text = "Address"
            Label3.Text = "PhoneNumber"
            Label4.Text = "CustomerCode"
            Label8.Text = "GoodsDeliveryDate"
            Label7.Text = "GoodsDeliveryNumber"
            Label6.Text = "SalesPersonInCharge"
            Label11.Text = "CustomerNumber"
            LblItemName.Text = "ItemName"
            LblSpec.Text = "Spec"
            Label10.Text = "DisplayFormat"
            RbtnSlip.Text = "UnitOfVoucher"

            RbtnDetails.Text = "UnitOfDetailData"
            RbtnDetails.Location = New Point(166, 196)

            ChkCancelData.Text = "IncludeCancelData"
            ChkCancelData.Location = New Point(556, 196)

            BtnOrderSearch.Text = "Search"
            BtnGoodsIssueCancel.Text = "CancelOfGoodsDelivery"
            BtnGoodsIssueView.Text = "GoodsDelivelyDataView"
            BtnBack.Text = "Back"
        End If

        '検索（Date）の初期値
        dtDateSince.Value = DateAdd("d", CommonConst.SINCE_DEFAULT_DAY, DateTime.Today)
        dtDateUntil.Value = DateTime.Today

        getList() '一覧表示

    End Sub

    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    '表示形式切替時
    Private Sub RbtnDetails_CheckedChanged(sender As Object, e As EventArgs) Handles RbtnDetails.CheckedChanged
        getList() '一覧表示
    End Sub

    '検索ボタン押下時
    Private Sub BtnQuoteSearch_Click(sender As Object, e As EventArgs) Handles BtnOrderSearch.Click
        getList() '一覧表示
    End Sub

    '出庫取消ボタン押下時
    Private Sub BtnSalesCancel_Click(sender As Object, e As EventArgs) Handles BtnGoodsIssueCancel.Click

        '明細表示時、または対象データがない場合は取消操作不可能
        If RbtnDetails.Checked Or DgvCymnhd.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Return

        End If

        '取消済みデータは取消操作不可能
        If DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("取消").Value = CommonConst.CANCEL_KBN_DISABLED_TXT Then
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
    't44_shukohd, t21_hattyu
    Private Sub updateData()

        Dim dtNow As String = UtilClass.formatDatetime(DateTime.Now)
        Dim Sql As String = ""

        Try

            '受注データ
            Sql = " AND "
            Sql += "受注番号 ILIKE '" & DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("受注番号").Value & "'"
            Sql += " AND "
            Sql += "受注番号枝番 ILIKE '" & DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("受注番号枝番").Value & "'"

            Dim dsCymndt As DataSet = getDsData("t11_cymndt", Sql)

            '出庫データ
            Sql = " AND"
            Sql += " 出庫番号"
            Sql += "='"
            Sql += DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("出庫番号").Value
            Sql += "'"

            Dim dsShukodt As DataSet = getDsData("t45_shukodt", Sql)


            Sql = "UPDATE "
            Sql += "Public."
            Sql += "t44_shukohd "
            Sql += "SET "

            Sql += "取消区分 = " & CommonConst.CANCEL_KBN_DISABLED '取消区分：1
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
            Sql += " 出庫番号"
            Sql += "='"
            Sql += DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("出庫番号").Value
            Sql += "'"

            _db.executeDB(Sql)

            Sql = "UPDATE "
            Sql += "Public."
            Sql += "t45_shukodt "
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
            Sql += " 出庫番号"
            Sql += "='"
            Sql += DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("出庫番号").Value
            Sql += "' "

            _db.executeDB(Sql)

            '受注データを更新する
            For i As Integer = 0 To dsCymndt.Tables(RS).Rows.Count - 1
                For x As Integer = 0 To dsShukodt.Tables(RS).Rows.Count - 1

                    '行番号が一致したら
                    If dsCymndt.Tables(RS).Rows(i)("行番号") = dsShukodt.Tables(RS).Rows(x)("行番号") Then
                        Dim calShukko As Integer = dsCymndt.Tables(RS).Rows(i)("出庫数") - dsShukodt.Tables(RS).Rows(x)("出庫数量")
                        Dim calUnShukko As Integer = dsCymndt.Tables(RS).Rows(i)("未出庫数") + dsShukodt.Tables(RS).Rows(x)("出庫数量")

                        Sql = "update t11_cymndt set "
                        Sql += "出庫数 = '" & calShukko & "'"
                        Sql += ",未出庫数 = '" & calUnShukko & "'"
                        Sql += ",更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
                        Sql += " where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                        Sql += " AND "
                        Sql += "受注番号 ILIKE '" & DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("受注番号").Value & "'"
                        Sql += " AND "
                        Sql += "受注番号枝番 ILIKE '" & DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("受注番号枝番").Value & "'"
                        Sql += " AND "
                        Sql += "行番号 = '" & dsCymndt.Tables(RS).Rows(i)("行番号") & "'"

                        _db.executeDB(Sql)

                        Sql = "update t10_cymnhd set "
                        Sql += "更新日 = '" & dtNow & "'"
                        Sql += ",更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
                        Sql += " where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                        Sql += " AND "
                        Sql += "受注番号 ILIKE '" & DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("受注番号").Value & "'"
                        Sql += " AND "
                        Sql += "受注番号枝番 ILIKE '" & DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("受注番号枝番").Value & "'"
                        Sql += " AND "
                        Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '取消区分=0

                        _db.executeDB(Sql)

                    End If

                Next

            Next

            Sql = "UPDATE "
            Sql += "Public."
            Sql += "t70_inout "
            Sql += "SET "

            Sql += "取消日"
            Sql += " = '"
            Sql += dtNow
            Sql += "', "
            Sql += "取消区分"
            Sql += " = '"
            Sql += CommonConst.CANCEL_KBN_DISABLED.ToString
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
            Sql += " 伝票番号"
            Sql += "='"
            Sql += DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("出庫番号").Value
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
        getList()

    End Sub

    '取消データを含めるチェックイベント
    Private Sub ChkCancelData_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCancelData.CheckedChanged
        getList() '一覧表示
    End Sub

    '参照ボタン押下時
    Private Sub BtnGoodsIssueView_Click(sender As Object, e As EventArgs) Handles BtnGoodsIssueView.Click

        '対象データがない場合は取消操作不可能
        If DgvCymnhd.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Return

        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvCymnhd.CurrentCell.RowIndex
        Dim No As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号").Value
        Dim Suffix As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号枝番").Value
        Dim Status As String = CommonConst.STATUS_VIEW

        Dim openForm As Form = Nothing
        openForm = New GoodsIssue(_msgHd, _db, _langHd, No, Suffix, Status)   '処理選択
        openForm.Show(Me)
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
        Sql += "会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
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

    'sqlで実行する文字列からシングルクォーテーションを文字コードにする
    Private Function escapeSql(ByVal prmSql As String) As String
        Dim sql As String = prmSql

        sql = sql.Replace("'"c, "''") 'シングルクォーテーションを置換

        Return Regex.Escape(sql)
        Return sql
    End Function

    Private Function actionChk() As Boolean
        '対象データがない場合は取消操作不可能
        If DgvCymnhd.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)

            Return False

        Else

            Return True

        End If
    End Function

    '汎用マスタから固定キー、可変キーに応じた結果を返す
    'param1：String 固定キー
    'param2：String 可変キー
    'Return: DataSet
    Private Function getDsHanyoData(ByVal prmFixed As String, ByVal prmVariable As String) As DataSet
        Dim Sql As String = ""

        Sql = " AND 固定キー = '" & prmFixed & "'"
        Sql += " AND 可変キー = '" & prmVariable & "'"

        'リードタイムのリストを汎用マスタから取得
        Return getDsData("m90_hanyo", Sql)

    End Function

    'ユーザーのカルチャーから、日本の形式に変換する
    Private Function strFormatDate(ByVal prmDate As String, Optional ByRef prmFormat As String = "yyyy/MM/dd") As String

        'PCのカルチャーを取得し、それに応じてStringからDatetimeを作成
        Dim ci As New System.Globalization.CultureInfo(CultureInfo.CurrentCulture.Name.ToString)
        Dim dateFormat As DateTime = DateTime.Parse(prmDate, ci, System.Globalization.DateTimeStyles.AssumeLocal)

        '日本の形式に書き換える
        Return dateFormat.ToString(prmFormat)
    End Function

    '金額フォーマット（登録の際の小数点指定子）を日本の形式に合わせる
    '桁区切り記号は外す
    Private Function formatNumber(ByVal prmVal As Decimal) As String

        Dim nfi As NumberFormatInfo = New CultureInfo(CommonConst.CI_JP, False).NumberFormat

        '日本の形式に書き換える
        Return prmVal.ToString("F3", nfi) '売掛残高を増やす
    End Function

End Class