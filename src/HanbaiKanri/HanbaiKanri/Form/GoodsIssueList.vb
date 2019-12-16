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
                DgvCymnhd.Columns.Add("仕入区分", "仕入区分")

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
                DgvCymnhd.Columns.Add("仕入区分", "仕入区分")

            End If

            DgvCymnhd.Columns("仕入区分").Visible = False

            For i As Integer = 0 To DgvCymnhd.Columns.Count - 1
                If i <> 16 Then
                    DgvCymnhd.Columns(i).ReadOnly = True
                End If
            Next

            Try

                '伝票単位選択時
                '----------------------------
                Sql = " SELECT "

                Sql += " t44.取消区分,t44.出庫番号,t44.出庫日,t44.受注番号,t44.受注番号枝番,t44.客先番号 "
                Sql += ",t44.得意先コード,t44.得意先名,t44.得意先郵便番号,t44.得意先住所,t44.得意先電話番号"

                Sql += ",t44.得意先ＦＡＸ,t44.得意先担当者名,t44.得意先担当者役職,t44.営業担当者,t44.入力担当者"
                Sql += ",t44.備考,t44.登録日,t45.仕入区分"

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

                Sql += " AND "
                Sql += " t45.出庫区分 <> '" & CommonConst.SHUKO_KBN_TMP.ToString & "'" '仮出庫以外のデータ

                Sql += " GROUP BY "
                Sql += " t44.取消区分,t44.出庫番号,t44.出庫日,t44.受注番号,t44.受注番号枝番,t44.客先番号 "
                Sql += ",t44.得意先コード,t44.得意先名,t44.得意先郵便番号,t44.得意先住所,t44.得意先電話番号"
                Sql += ",t44.得意先ＦＡＸ,t44.得意先担当者名,t44.得意先担当者役職,t44.営業担当者,t44.入力担当者"
                Sql += ",t44.備考,t44.登録日,t44.更新日,t45.仕入区分"

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

                    DgvCymnhd.Rows(i).Cells("仕入区分").Value = ds.Tables(RS).Rows(i)("仕入区分")

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
            '----------------------------
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

            For i As Integer = 0 To DgvCymnhd.Columns.Count - 1
                DgvCymnhd.Columns(i).ReadOnly = True
            Next

            DgvCymnhd.Columns("出庫数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns("売単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            '数字形式
            DgvCymnhd.Columns("出庫数量").DefaultCellStyle.Format = "N2"
            DgvCymnhd.Columns("売単価").DefaultCellStyle.Format = "N2"

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

            Sql += " AND "
            Sql += " t45.出庫区分 <> '" & CommonConst.SHUKO_KBN_TMP.ToString & "'" '仮出庫以外のデータ
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

        If salesName <> Nothing Then
            Sql += " AND "
            Sql += " t44.営業担当者 ILIKE '%" & salesName & "%' "
        End If

        If poNum <> Nothing Then
            Sql += " AND "
            Sql += " t44.客先番号 ILIKE '%" & poNum & "%' "
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
            _msgHd.dspMSG("chkDetailsCancel", frmC01F10_Login.loginValue.Language)
            Return

        End If

        '取消済みデータは取消操作不可能
        If DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("取消").Value =
            IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_JPN, CommonConst.CANCEL_KBN_JPN_TXT, CommonConst.CANCEL_KBN_ENG_TXT) Then
            '取消データは選択できないアラートを出す
            _msgHd.dspMSG("cannotSelectTorikeshiData", frmC01F10_Login.loginValue.Language)
            Return
        End If


        '出庫番号
        Dim strSyukoNo As String = DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("出庫番号").Value


        '取消確認のアラート
        Dim result As DialogResult = _msgHd.dspMSG("confirmCancel", frmC01F10_Login.loginValue.Language)

        If result = DialogResult.Yes Then

            updateData() 'データ更新


            'm90_hanyo  SimpleRegistrationの可変キーが1の場合は仕入取消処理も行う
            Dim Sql As String = "  AND 固定キー = 'SR'"
            Sql += " AND 可変キー = '1'"

            Dim dsHanyo As DataTable = getDsData("m90_hanyo", Sql).Tables(0)

            If dsHanyo.Rows.Count = 0 Then  'データなしの場合は終了
                Exit Sub
            End If


            '仕入取消処理
            Dim blnFlg As Boolean = mUpdate_UriageTorikeshi(strSyukoNo)
            If blnFlg = False Then
                Exit Sub
            End If
        End If

    End Sub


    Private Function mUpdate_UriageTorikeshi(ByVal strSyukoNo As String) As Boolean

        Dim dtNow As DateTime = DateTime.Now
        Dim strNow As String = UtilClass.formatDatetime(dtNow)
        Dim reccnt As Integer = 0


        Try


#Region "select_t45_shukodt"

            Dim Sql As String = "SELECT * "

            Sql += " FROM t44_shukohd t44"
            Sql += " left join t45_shukodt t45"
            Sql += " on t44.出庫番号 = t45.出庫番号"

            Sql += " where t44.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += "   and t44.出庫番号 = '" & strSyukoNo & "'"

            Dim dtSyukodt As DataTable = _db.selectDB(Sql, RS, reccnt).Tables(0)

#End Region


            '取消す売上データをループさせながら、受注データから減算していく
            For index1 As Integer = 0 To dtSyukodt.Rows.Count() - 1  '出庫


                Dim strJyutyuNo As String = dtSyukodt.Rows(index1)("受注番号")
                Dim strEda As String = dtSyukodt.Rows(index1)("受注番号枝番")
                Dim intSyukosu As Integer = dtSyukodt.Rows(index1)("出庫数量")


#Region "select_t31_urigdt"

                Dim Sql2 As String = "SELECT *"

                Sql2 += " FROM t31_urigdt t31"
                Sql2 += " left join t30_urighd t30"
                Sql2 += " on t31.売上番号 = t30.売上番号 and t31.売上番号枝番 = t30.売上番号枝番"

                Sql2 += " where t31.会社コード ='" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql2 += "   and t31.行番号 = '" & dtSyukodt.Rows(index1)("行番号") & "'"
                Sql2 += "   and t31.受注番号 = '" & strJyutyuNo & "'"
                Sql2 += "   and t31.受注番号枝番 ='" & strEda & "'"
                Sql2 += "   and t30.売上日 ='" & UtilClass.strFormatDate(dtSyukodt.Rows(index1)("出庫日")） & "'"
                Sql2 += "   and t31.売上数量 ='" & intSyukosu & "'"


                Dim dtUrigDt As DataTable = _db.selectDB(Sql2, RS, reccnt).Tables(0)

#End Region


#Region "update_t11_cymndt"

                '受注明細から取消す売上データ明細の数を減算
                Dim Sql4 As String = "UPDATE t11_cymndt"

                Sql4 += " SET "
                Sql4 += " 売上数量 = 売上数量 - '" & intSyukosu & "'"
                Sql4 += ",受注残数 = 受注残数 + '" & intSyukosu & "'"
                Sql4 += ",更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"


                Sql4 += " where 会社コード ='" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql4 += "   and 受注番号 ='" & dtUrigDt.Rows(0)("受注番号") & "'"
                Sql4 += "   and 受注番号枝番 ='" & dtUrigDt.Rows(0)("受注番号枝番") & "'"
                Sql4 += "   and 行番号 = '" & dtUrigDt.Rows(0)("行番号") & "'"

                _db.executeDB(Sql4)

#End Region



                If index1 = 0 Then


#Region "update_t10_cymnhd"

                    '受注基本データを更新
                    Dim Sql5 As String = "UPDATE t10_cymnhd "
                    Sql5 += " SET "
                    Sql5 += " 更新日 = '" & UtilClass.formatDatetime(dtNow) & "'"
                    Sql5 += ",更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"

                    Sql5 += " where 会社コード ='" & frmC01F10_Login.loginValue.BumonCD & "'"
                    Sql5 += "   and 受注番号 ='" & dtUrigDt.Rows(0)("受注番号") & "'"
                    Sql5 += "   and 受注番号枝番 ='" & dtUrigDt.Rows(0)("受注番号枝番") & "'"

                    _db.executeDB(Sql5)

#End Region


#Region "update_t30_urighd"

                    Dim Sql6 As String = "UPDATE t30_urighd"
                    Sql6 += " SET "
                    Sql6 += " 取消区分 = " & CommonConst.CANCEL_KBN_DISABLED.ToString
                    Sql6 += ",取消日 = '" & UtilClass.strFormatDate(dtNow) & "'"
                    Sql6 += ",更新日 = '" & UtilClass.strFormatDate(dtNow) & "'"
                    Sql6 += ",更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"

                    Sql6 += " where 会社コード ='" & frmC01F10_Login.loginValue.BumonCD & "'"
                    Sql6 += "   and 売上番号 ='" & dtUrigDt.Rows(0)("売上番号") & "'"
                    Sql6 += "   and 売上番号枝番 ='" & dtUrigDt.Rows(0)("売上番号枝番") & "'"

                    _db.executeDB(Sql6)

#End Region


                End If

            Next


        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try


        mUpdate_UriageTorikeshi = True

    End Function



    '選択データをもとに以下テーブル更新
    't44_shukohd, t21_hattyu
    Private Sub updateData()

        Dim dtNow As String = UtilClass.formatDatetime(DateTime.Now)
        Dim dtNow_insert As DateTime = DateTime.Now
        Dim reccnt As Integer = 0
        Dim Sql As String = ""

        Try


#Region "在庫引当_Insert"

            '在庫引当の場合
            '新しい出庫データと履歴を作成する
            '出庫番号を最新、受注番号などは変更しない
            If DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("仕入区分").Value = CommonConst.Sire_KBN_Zaiko Then

                '採番データを取得・更新
                '出庫登録データの伝票番号は基本的に LS で統一される（1商品で複数の在庫マスタをまたぐ場合を除く）
                Dim MAIN_LS As String = getSaiban("70", dtNow_insert.ToShortDateString())

                't44_shukohd
                Sql = " and 出庫番号 ='" & DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("出庫番号").Value & "'"
                Dim dsShukohd As DataSet = getDsData("t44_shukohd", Sql)


                't44_shukohd
                Sql = "INSERT INTO Public.t44_shukohd ("
                Sql += "会社コード, 出庫番号, 見積番号, 見積番号枝番, 受注番号, 受注番号枝番, 客先番号, 得意先コード, 得意先名"
                Sql += ", 得意先郵便番号, 得意先住所, 得意先電話番号, 得意先ＦＡＸ, 得意先担当者役職, 得意先担当者名, 営業担当者"
                Sql += ", 入力担当者, 備考, 取消日, 取消区分, 出庫日, 登録日, 更新日, 更新者, 営業担当者コード, 入力担当者コード)"
                Sql += " VALUES('" & frmC01F10_Login.loginValue.BumonCD & "'"       '会社コード
                Sql += ", '" & MAIN_LS & "'"                 '出庫番号
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("見積番号") & "'"        '見積番号
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("見積番号枝番") & "'"    '見積番号枝番
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("受注番号") & "'"        '受注番号
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("受注番号枝番") & "'"    '受注番号枝番
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("客先番号") & "'"        '客先番号
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("得意先コード") & "'"    '得意先コード
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("得意先名") & "'"        '得意先名
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("得意先郵便番号") & "'"     '得意先郵便番号
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("得意先住所") & "'"       '得意先住所
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("得意先電話番号") & "'"   '得意先電話番号
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("得意先ＦＡＸ") & "'"      '得意先ＦＡＸ
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("得意先担当者役職") & "'"  '得意先担当者役職
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("得意先担当者名") & "'"    '得意先担当者名
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("営業担当者") & "'"          '営業担当者
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("入力担当者") & "'"          '入力担当者
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("備考") & "'"                '備考
                Sql += ", null"     '取消日
                Sql += ", 0"     '取消区分
                Sql += ", '" & UtilClass.formatDatetime(dsShukohd.Tables(RS).Rows(0)("出庫日")) & "'"     '出庫日
                Sql += ", '" & UtilClass.formatDatetime(dtNow) & "'"                 '登録日
                Sql += ", '" & UtilClass.formatDatetime(dtNow) & "'"                 '更新日
                Sql += ", '" & frmC01F10_Login.loginValue.TantoNM & "'"     '更新者
                Sql += ", '" & dsShukohd.Tables(RS).Rows(0)("営業担当者コード") & "'"    '営業担当者コード
                Sql += ", '" & frmC01F10_Login.loginValue.TantoCD & "'"    '入力担当者コード
                Sql += " )"

                _db.executeDB(Sql)



                't45_shukodt
                Sql = " and 出庫番号 ='" & DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("出庫番号").Value & "'"
                Dim dsShuko As DataSet = getDsData("t45_shukodt", Sql)

                For i As Integer = 0 To dsShuko.Tables(RS).Rows.Count - 1

                    Sql = "INSERT INTO "
                    Sql += "Public."
                    Sql += "t45_shukodt("
                    Sql += "会社コード, 出庫番号, 受注番号, 受注番号枝番, 行番号, 仕入区分, メーカー, 品名, 型式"
                    Sql += ", 仕入先名, 売単価, 出庫数量, 単位, 備考, 更新者, 更新日, 出庫区分, 倉庫コード)"
                    Sql += " VALUES('" & frmC01F10_Login.loginValue.BumonCD & "'"   '会社コード
                    Sql += ", '" & MAIN_LS & "'"     '出庫番号
                    Sql += ", '" & dsShuko.Tables(RS).Rows(i)("受注番号") & "'"            '受注番号
                    Sql += ", '" & dsShuko.Tables(RS).Rows(i)("受注番号枝番") & "'"        '受注番号枝番
                    Sql += ", " & dsShuko.Tables(RS).Rows(i)("行番号")                     '行番号
                    Sql += ", '" & dsShuko.Tables(RS).Rows(i)("仕入区分") & "'"            '仕入区分
                    Sql += ", '" & dsShuko.Tables(RS).Rows(i)("メーカー") & "'"            'メーカー
                    Sql += ", '" & dsShuko.Tables(RS).Rows(i)("品名") & "'"                '品名
                    Sql += ", '" & dsShuko.Tables(RS).Rows(i)("型式") & "'"                '型式
                    Sql += ", '" & dsShuko.Tables(RS).Rows(i)("仕入先名") & "'"            '仕入先名
                    Sql += ", " & UtilClass.formatNumber(dsShuko.Tables(RS).Rows(i)("売単価"))    '売単価
                    Sql += ", " & UtilClass.formatNumber(dsShuko.Tables(RS).Rows(i)("出庫数量"))        '数量
                    Sql += ", '" & dsShuko.Tables(RS).Rows(i)("単位") & "'"                         '単位
                    Sql += ", '" & dsShuko.Tables(RS).Rows(i)("備考") & "'"                          '備考
                    Sql += ", '" & frmC01F10_Login.loginValue.TantoNM & "'"                           '更新者
                    Sql += ", '" & UtilClass.formatDatetime(dtNow) & "'"                              '更新日
                    Sql += ", '" & CommonConst.SHUKO_KBN_TMP & "'" '仮出庫：2
                    Sql += ", '" & dsShuko.Tables(RS).Rows(i)("倉庫コード") & "')" '倉庫コード

                    _db.executeDB(Sql)


                    '在庫データの伝票番号と行番号を呼び出す（重要）
                    'inoutのロケ番号へ挿入
                    Sql = "SELECT ロケ番号 "
                    Sql += " from t70_inout t70 "

                    Sql += " WHERE 会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"

                    Sql += "   AND 伝票番号 = '" & dsShuko.Tables(RS).Rows(i)("出庫番号") & "'"
                    Sql += "   AND   行番号 = '" & dsShuko.Tables(RS).Rows(i)("行番号") & "'"

                    '在庫マスタから現在庫数を取得
                    Dim dsZaiko As DataSet = _db.selectDB(Sql, RS, reccnt)


                    't70_inout にデータ登録
                    Sql = "INSERT INTO "
                    Sql += "Public."
                    Sql += "t70_inout("
                    Sql += "会社コード, 入出庫区分, 倉庫コード, 伝票番号, 行番号, 入出庫種別, 引当区分"
                    Sql += ", メーカー, 品名, 型式, 数量, 単位, 備考, 入出庫日"
                    Sql += ", 取消区分, 更新者, 更新日, ロケ番号"
                    Sql += " )VALUES('"
                    Sql += frmC01F10_Login.loginValue.BumonCD '会社コード
                    Sql += "', '"
                    Sql += "2" '入出庫区分 1.入庫, 2.出庫
                    Sql += "', '"
                    Sql += dsShuko.Tables(RS).Rows(i)("倉庫コード")  '倉庫コード
                    Sql += "', '"
                    Sql += MAIN_LS '伝票番号
                    Sql += "', '"
                    Sql += UtilClass.formatNumber(dsShuko.Tables(RS).Rows(i)("行番号"))  '行番号
                    Sql += "', '"
                    'Sql += DgvAdd.Rows(i).Cells("入出庫種別").Value.ToString '入出庫種別
                    Sql += CommonConst.INOUT_KBN_NORMAL.ToString '入出庫種別(0：通常）
                    Sql += "', '"
                    Sql += CommonConst.AC_KBN_ASSIGN.ToString '引当区分 1
                    Sql += "', '"
                    Sql += dsShuko.Tables(RS).Rows(i)("メーカー") 'メーカー
                    Sql += "', '"
                    Sql += dsShuko.Tables(RS).Rows(i)("品名") '品名
                    Sql += "', '"
                    Sql += dsShuko.Tables(RS).Rows(i)("型式") '型式
                    Sql += "', '"
                    Sql += UtilClass.formatNumber(dsShuko.Tables(RS).Rows(i)("出庫数量")) '数量
                    Sql += "', '"
                    Sql += dsShuko.Tables(RS).Rows(i)("単位")  '単位
                    Sql += "', '"
                    Sql += dsShuko.Tables(RS).Rows(i)("備考") '備考
                    Sql += "', '"
                    Sql += UtilClass.formatDatetime(dsShukohd.Tables(RS).Rows(0)("出庫日")) '入出庫日
                    Sql += "', '"
                    Sql += CommonConst.CANCEL_KBN_ENABLED.ToString '取消区分
                    Sql += "', '"
                    Sql += frmC01F10_Login.loginValue.TantoNM '更新者
                    Sql += "', '"
                    Sql += UtilClass.formatDatetime(dtNow) '更新日
                    Sql += "', '"
                    'Sql += dsZaiko.Tables(RS).Rows(i)("伝票番号") & dsZaiko.Tables(RS).Rows(i)("行番号")
                    Sql += dsZaiko.Tables(RS).Rows(0)("ロケ番号")

                    'Sql += "WH082601801"

                    'Sql += " ', '"
                    'Sql += dsShuko.Tables(RS).Rows(i)("仕入区分") '仕入区分

                    Sql += "')"

                    _db.executeDB(Sql)

                Next

            End If

#End Region

#Region "出庫取消"


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
            Sql += "', "
            Sql += "備考"
            Sql += " = '"
            Sql += DgvCymnhd.Rows(index:=DgvCymnhd.CurrentCell.RowIndex).Cells("備考").Value.ToString
            Sql += "' "
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

#End Region


#Region "受注データを更新する"

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

#End Region


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

        Dim denpyoNo As String = "00000000000000"
        Dim denpyoEda As String = "00"

        If No = denpyoNo And Suffix = denpyoEda Then
            No = DgvCymnhd.Rows(RowIdx).Cells("出庫番号").Value
        End If

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

    'param1：String 採番キー
    'param2：DateTime 登録日
    'Return: String 伝票番号
    '伝票番号を取得
    Public Function getSaiban(ByVal key As String, ByVal today As DateTime) As String
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
            Sql += UtilClass.formatDatetime(today)
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

End Class