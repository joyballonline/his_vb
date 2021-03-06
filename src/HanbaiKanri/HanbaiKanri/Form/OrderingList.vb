﻿Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Globalization
Imports System.Text.RegularExpressions

Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Imports System.Runtime.InteropServices
Imports System.IO

Public Class OrderingList
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
    Private List As New List(Of String)(New String() {})

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
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True
        DgvHtyhd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells
    End Sub

    '画面表示時
    Private Sub OrderingList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _status = CommonConst.STATUS_ORDING Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "PurchasedInputMode"
            Else
                LblMode.Text = "仕入入力モード"
            End If

            BtnOrding.Visible = True
            BtnOrding.Location = New System.Drawing.Point(997, 509)
        ElseIf _status = CommonConst.STATUS_RECEIPT Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "GoodsReceiptInputMode"
            Else
                LblMode.Text = "入庫入力モード"
            End If

            BtnReceipt.Visible = True
            BtnReceipt.Location = New System.Drawing.Point(997, 509)
        ElseIf _status = CommonConst.STATUS_EDIT Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "EditMode"
            Else
                LblMode.Text = "編集モード"
            End If

            BtnPurchaseEdit.Visible = True
            BtnPurchaseEdit.Location = New System.Drawing.Point(997, 509)
        ElseIf _status = CommonConst.STATUS_VIEW Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If

            BtnPurchaseView.Visible = True
            BtnPurchaseView.Location = New System.Drawing.Point(997, 509)
        ElseIf _status = CommonConst.STATUS_CANCEL Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "CancelMode"
            Else
                LblMode.Text = "取消モード"
            End If

            BtnPurchaseCancel.Visible = True
            BtnPurchaseCancel.Location = New System.Drawing.Point(997, 509)
        ElseIf _status = CommonConst.STATUS_CLONE Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "NewCopyMode"
            Else
                LblMode.Text = "新規複写モード"
            End If

            BtnPurchaseClone.Visible = True
            BtnPurchaseClone.Location = New System.Drawing.Point(997, 509)
        ElseIf _status = CommonConst.STATUS_AP Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "AccountsPayableInputMode"
            Else
                LblMode.Text = "買掛入力モード"
            End If

            BtnAP.Visible = True
            BtnAP.Location = New System.Drawing.Point(997, 509)

            BtnExcelOutput.Visible = True
            BtnExcelOutput.Location = New System.Drawing.Point(13, 509)

            ChkGoodsReceiptDate.Visible = True
        End If

        '検索（Date）の初期値
        dtPurchaseDateSince.Value = DateAdd("d", CommonConst.SINCE_DEFAULT_DAY, DateTime.Today)
        dtPurchaseDateUntil.Value = DateTime.Today

        '一覧再表示
        getList()

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblConditions.Text = "ExtractionCondition" '抽出条件
            Label1.Text = "SupplierName"
            Label2.Text = "Address"
            Label3.Text = "PhoneNumber"
            Label4.Text = "SupplierCode"
            lblOrderDate.Text = "PurchaseDate"
            Label7.Text = "PurchaseNumber"
            Label6.Text = "SalesPersonInCharge"
            Label11.Text = "CustomerNumber"
            lblMaker.Text = "Maker"
            LblItemName.Text = "ItemName"
            LblSpec.Text = "Spec"
            Label10.Text = "DisplayFormat"
            RbtnSlip.Text = "UnitOfVoucher"

            RbtnDetails.Text = "LineItemUnit"
            RbtnDetails.Location = New System.Drawing.Point(166, 196)

            ChkCancelData.Text = "IncludeCancelData"
            ChkCancelData.Location = New System.Drawing.Point(556, 196)

            BtnPurchaseView.Text = "PurchaseView"
            BtnPurchaseSearch.Text = "Search"
            BtnPurchaseCancel.Text = "CancelOfPurchase"
            BtnPurchaseClone.Text = "PurchaseCopy"
            BtnBack.Text = "Back"
            BtnAP.Text = "AccountsPayable"
            BtnOrding.Text = "PurchaseRegistration"
            BtnReceipt.Text = "ReceiptRegistration"
            BtnPurchaseEdit.Text = "PurchaseEdit"

            BtnExcelOutput.Text = "Excel Output"
            ChkGoodsReceiptDate.Text = "narrow down by goods receipt date"
        End If
        DgvHtyhd.Visible = True
    End Sub

    Private Sub getList()
        DgvHtyhd.Visible = False

        ' 行や列を追加したり、セルに値を設定するときは、自動サイズ設定しない。
        DgvHtyhd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        DgvHtyhd.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None
        DgvHtyhd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

        '一覧クリア
        DgvHtyhd.Rows.Clear()
        DgvHtyhd.Columns.Clear()

        Dim ds As DataSet
        Dim Sql As String = ""
        Dim reccnt As Integer = 0 'DB用（デフォルト）

        Dim curds As DataSet  'm25_currency
        Dim cur As String

        Dim strBaseCur = setBaseCurrency()

        Try

            '伝票単位
            If RbtnSlip.Checked Then

                If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                    DgvHtyhd.Columns.Add("取消", "Cancel")
                    DgvHtyhd.Columns.Add("発注番号", "PurchaseNumber")
                    DgvHtyhd.Columns.Add("発注番号枝番", "PurchaseOrderVer.")
                    DgvHtyhd.Columns.Add("客先番号", "CustomerNumber")

                    DgvHtyhd.Columns.Add("発注日", "PurchaseDate")

                    DgvHtyhd.Columns.Add("得意先名", "CustomerName")
                    DgvHtyhd.Columns.Add("仕入先コード", "SupplierCode")
                    DgvHtyhd.Columns.Add("仕入先名", "SupplierName")

                    DgvHtyhd.Columns.Add("通貨_外貨", "Currency")

                    DgvHtyhd.Columns.Add("仕入原価_外貨", "PurchasingCost" & vbCrLf & "(OrignalCurrency)")
                    DgvHtyhd.Columns.Add("仕入原価", "PurchasingCost" & vbCrLf & "(" & setBaseCurrency() & ")")


                    DgvHtyhd.Columns.Add("仕入金額_外貨", "PurchaseAmount" & vbCrLf & "(OrignalCurrency)")
                    DgvHtyhd.Columns.Add("仕入金額", "PurchaseAmount" & vbCrLf & "(" & setBaseCurrency() & ")")


                    DgvHtyhd.Columns.Add("仕入先郵便番号", "PostalCode")
                    DgvHtyhd.Columns.Add("仕入先住所", "Address")
                    DgvHtyhd.Columns.Add("仕入先電話番号", "PhoneNumber")
                    DgvHtyhd.Columns.Add("仕入先ＦＡＸ", "FAX")
                    DgvHtyhd.Columns.Add("仕入先担当者名", "NameOfPIC")
                    DgvHtyhd.Columns.Add("仕入先担当者役職", "PositionPICSupplier")
                    DgvHtyhd.Columns.Add("支払条件", "PaymentTerms")
                    DgvHtyhd.Columns.Add("営業担当者", "SalesPersonInCharge")
                    DgvHtyhd.Columns.Add("入力担当者", "PICForInputting")
                    DgvHtyhd.Columns.Add("備考", "Remarks")
                    DgvHtyhd.Columns.Add("登録日", "RegistrationDate")
                    DgvHtyhd.Columns.Add("更新日", "LastUpdateDate")
                Else
                    DgvHtyhd.Columns.Add("取消", "取消")
                    DgvHtyhd.Columns.Add("発注番号", "発注番号")
                    DgvHtyhd.Columns.Add("発注番号枝番", "発注Ver.")
                    DgvHtyhd.Columns.Add("客先番号", "客先番号")

                    DgvHtyhd.Columns.Add("発注日", "発注日")
                    DgvHtyhd.Columns.Add("得意先名", "得意先名")
                    DgvHtyhd.Columns.Add("仕入先コード", "仕入先コード")
                    DgvHtyhd.Columns.Add("仕入先名", "仕入先名")

                    DgvHtyhd.Columns.Add("通貨_外貨", "仕入通貨")
                    DgvHtyhd.Columns.Add("仕入原価_外貨", "仕入原価" & vbCrLf & "(原通貨)")
                    DgvHtyhd.Columns.Add("仕入原価", "仕入原価" & vbCrLf & "(" & setBaseCurrency() & ")")
                    DgvHtyhd.Columns.Add("仕入金額_外貨", "仕入金額" & vbCrLf & "(原通貨)")
                    DgvHtyhd.Columns.Add("仕入金額", "仕入金額" & vbCrLf & "(" & setBaseCurrency() & ")")

                    DgvHtyhd.Columns.Add("仕入先郵便番号", "仕入先郵便番号")
                    DgvHtyhd.Columns.Add("仕入先住所", "仕入先先住所")
                    DgvHtyhd.Columns.Add("仕入先電話番号", "仕入先電話番号")
                    DgvHtyhd.Columns.Add("仕入先ＦＡＸ", "仕入先ＦＡＸ")
                    DgvHtyhd.Columns.Add("仕入先担当者名", "仕入先担当者名")
                    DgvHtyhd.Columns.Add("仕入先担当者役職", "仕入先担当者役職")

                    DgvHtyhd.Columns.Add("支払条件", "支払条件")
                    DgvHtyhd.Columns.Add("営業担当者", "営業担当者")
                    DgvHtyhd.Columns.Add("入力担当者", "入力担当者")
                    DgvHtyhd.Columns.Add("備考", "備考")
                    DgvHtyhd.Columns.Add("登録日", "登録日")
                    DgvHtyhd.Columns.Add("更新日", "最終更新日")
                End If

                '数字形式
                DgvHtyhd.Columns("仕入原価_外貨").DefaultCellStyle.Format = "N2"
                DgvHtyhd.Columns("仕入原価").DefaultCellStyle.Format = "N2"

                DgvHtyhd.Columns("仕入金額_外貨").DefaultCellStyle.Format = "N2"
                DgvHtyhd.Columns("仕入金額").DefaultCellStyle.Format = "N2"
                '日付表示
                DgvHtyhd.Columns("発注日").DefaultCellStyle.Format = "d"


                DgvHtyhd.Columns("取消").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("取消").ReadOnly = True
                DgvHtyhd.Columns("発注番号").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("発注番号").ReadOnly = True
                DgvHtyhd.Columns("発注番号枝番").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("発注番号枝番").ReadOnly = True
                DgvHtyhd.Columns("客先番号").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("客先番号").ReadOnly = True
                DgvHtyhd.Columns("発注日").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("発注日").ReadOnly = True
                DgvHtyhd.Columns("得意先名").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("得意先名").ReadOnly = True
                DgvHtyhd.Columns("仕入先コード").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("仕入先コード").ReadOnly = True
                DgvHtyhd.Columns("仕入先名").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("仕入先名").ReadOnly = True
                DgvHtyhd.Columns("通貨_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("通貨_外貨").ReadOnly = True
                DgvHtyhd.Columns("仕入原価_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("仕入原価_外貨").ReadOnly = True
                DgvHtyhd.Columns("仕入原価").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("仕入原価").ReadOnly = True
                DgvHtyhd.Columns("仕入金額_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("仕入金額_外貨").ReadOnly = True
                DgvHtyhd.Columns("仕入金額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("仕入金額").ReadOnly = True
                DgvHtyhd.Columns("仕入先郵便番号").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("仕入先郵便番号").ReadOnly = True
                DgvHtyhd.Columns("仕入先住所").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("仕入先住所").ReadOnly = True
                DgvHtyhd.Columns("仕入先電話番号").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("仕入先電話番号").ReadOnly = True
                DgvHtyhd.Columns("仕入先ＦＡＸ").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("仕入先ＦＡＸ").ReadOnly = True
                DgvHtyhd.Columns("仕入先担当者名").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("仕入先担当者名").ReadOnly = True
                DgvHtyhd.Columns("仕入先担当者役職").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("仕入先担当者役職").ReadOnly = True
                DgvHtyhd.Columns("支払条件").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("支払条件").ReadOnly = True
                DgvHtyhd.Columns("営業担当者").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("営業担当者").ReadOnly = True
                DgvHtyhd.Columns("入力担当者").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("入力担当者").ReadOnly = True
                DgvHtyhd.Columns("備考").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("備考").ReadOnly = True
                DgvHtyhd.Columns("登録日").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("登録日").ReadOnly = True
                DgvHtyhd.Columns("更新日").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("更新日").ReadOnly = True


                '発注基本を取得
                Sql = "Select count(*) As 件数"
                Sql += " from Public.t20_hattyu t20 "
                Sql += " left join Public.t21_hattyu t21"
                Sql += "  On (t20.発注番号 = t21.発注番号 And t20.発注番号枝番 = t21.発注番号枝番)"

                Sql += " WHERE t20.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

                Sql += viewSearchConditions() '抽出条件取得

                ds = _db.selectDB(Sql, RS, reccnt)

                If ds.Tables(RS).Rows(0)("件数") = 0 Then
                    DgvHtyhd.Visible = True
                    Exit Sub
                End If

                '発注基本を取得
                Sql = "SELECT"

                Sql += " t20.発注番号,t20.発注番号枝番,t20.発注日,t20.取消区分"
                Sql += ",t20.得意先名,t20.客先番号,t20.仕入先コード,t20.仕入先名,t20.仕入先郵便番号,t20.仕入先住所"
                Sql += ",t20.仕入先電話番号,t20.仕入先ＦＡＸ,t20.仕入先担当者名,t20.仕入先担当者役職"
                Sql += ",t20.仕入金額_外貨,t20.仕入金額,t20.支払条件,t20.営業担当者,t20.入力担当者,t20.備考"
                Sql += ",t20.登録日,t20.更新日,t20.通貨"

                'Sql += ",sum(t21.仕入金額 + t21.間接費) as 仕入金額合計, sum(t21.仕入金額_外貨) as 仕入金額合計_外貨 "

                Sql += " FROM public.t20_hattyu t20 "
                Sql += " left join public.t21_hattyu t21"
                Sql += "  on (t20.発注番号 = t21.発注番号 and t20.発注番号枝番 = t21.発注番号枝番)"

                Sql += " WHERE t20.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

                Sql += viewSearchConditions() '抽出条件取得



                Sql += " GROUP BY "
                Sql += " t20.発注番号,t20.発注番号枝番,t20.発注日,t20.取消区分"
                Sql += ",t20.得意先名,t20.客先番号,t20.仕入先コード,t20.仕入先名,t20.仕入先郵便番号,t20.仕入先住所"
                Sql += ",t20.仕入先電話番号,t20.仕入先ＦＡＸ,t20.仕入先担当者名,t20.仕入先担当者役職"
                Sql += ",t20.仕入金額_外貨,t20.仕入金額,t20.支払条件,t20.営業担当者,t20.入力担当者,t20.備考"
                Sql += ",t20.登録日,t20.更新日,t20.通貨"


                Sql += " ORDER BY "
                Sql += "t20.更新日 DESC, t20.発注番号, t20.発注番号枝番"

                ds = _db.selectDB(Sql, RS, reccnt)


                '伝票単位時のセル書式
                DgvHtyhd.Columns("仕入原価_外貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvHtyhd.Columns("仕入原価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvHtyhd.Columns("仕入金額_外貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvHtyhd.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1

                    If IsDBNull(ds.Tables(RS).Rows(i)("通貨")) Then
                        cur = vbNullString
                    Else
                        Sql = " and 採番キー = " & ds.Tables(RS).Rows(i)("通貨")
                        curds = getDsData("m25_currency", Sql)

                        cur = curds.Tables(RS).Rows(0)("通貨コード")
                    End If

                    DgvHtyhd.Rows.Add()
                    DgvHtyhd.Rows(i).Cells("取消").Value = getDelKbnTxt(ds.Tables(RS).Rows(i)("取消区分"))
                    DgvHtyhd.Rows(i).Cells("発注番号").Value = ds.Tables(RS).Rows(i)("発注番号")
                    DgvHtyhd.Rows(i).Cells("発注番号枝番").Value = ds.Tables(RS).Rows(i)("発注番号枝番")
                    DgvHtyhd.Rows(i).Cells("客先番号").Value = ds.Tables(RS).Rows(i)("客先番号")

                    DgvHtyhd.Rows(i).Cells("発注日").Value = ds.Tables(RS).Rows(i)("発注日")

                    DgvHtyhd.Rows(i).Cells("得意先名").Value = ds.Tables(RS).Rows(i)("得意先名")
                    DgvHtyhd.Rows(i).Cells("仕入先コード").Value = ds.Tables(RS).Rows(i)("仕入先コード")
                    DgvHtyhd.Rows(i).Cells("仕入先名").Value = ds.Tables(RS).Rows(i)("仕入先名")

                    DgvHtyhd.Rows(i).Cells("通貨_外貨").Value = cur

                    Dim decPurchase1 As Decimal = 0
                    Dim decPurchase2 As Decimal = 0
                    Dim decPurchaseAmount1 As Decimal = 0
                    Dim decPurchaseAmount2 As Decimal = 0

                    Call mPurchaseCost(ds.Tables(RS).Rows(i)("発注番号"), ds.Tables(RS).Rows(i)("発注番号枝番") _
                                            , decPurchase1, decPurchase2 _
                                            , decPurchaseAmount1, decPurchaseAmount2)

                    DgvHtyhd.Rows(i).Cells("仕入原価_外貨").Value = decPurchase1
                    DgvHtyhd.Rows(i).Cells("仕入原価").Value = decPurchase2
                    DgvHtyhd.Rows(i).Cells("仕入金額_外貨").Value = decPurchaseAmount1
                    DgvHtyhd.Rows(i).Cells("仕入金額").Value = decPurchaseAmount2

                    DgvHtyhd.Rows(i).Cells("仕入先郵便番号").Value = ds.Tables(RS).Rows(i)("仕入先郵便番号")
                    DgvHtyhd.Rows(i).Cells("仕入先住所").Value = ds.Tables(RS).Rows(i)("仕入先住所")
                    DgvHtyhd.Rows(i).Cells("仕入先電話番号").Value = ds.Tables(RS).Rows(i)("仕入先電話番号")
                    DgvHtyhd.Rows(i).Cells("仕入先ＦＡＸ").Value = ds.Tables(RS).Rows(i)("仕入先ＦＡＸ")
                    DgvHtyhd.Rows(i).Cells("仕入先担当者名").Value = ds.Tables(RS).Rows(i)("仕入先担当者名")
                    DgvHtyhd.Rows(i).Cells("仕入先担当者役職").Value = ds.Tables(RS).Rows(i)("仕入先担当者役職")

                    DgvHtyhd.Rows(i).Cells("支払条件").Value = ds.Tables(RS).Rows(i)("支払条件")
                    DgvHtyhd.Rows(i).Cells("営業担当者").Value = ds.Tables(RS).Rows(i)("営業担当者")
                    DgvHtyhd.Rows(i).Cells("入力担当者").Value = ds.Tables(RS).Rows(i)("入力担当者")
                    DgvHtyhd.Rows(i).Cells("備考").Value = ds.Tables(RS).Rows(i)("備考")
                    DgvHtyhd.Rows(i).Cells("登録日").Value = ds.Tables(RS).Rows(i)("登録日")
                    DgvHtyhd.Rows(i).Cells("更新日").Value = ds.Tables(RS).Rows(i)("更新日")
                Next

            Else '明細単位

                    '発注基本を取得

                    '抽出条件
                    Dim supplierName As String = escapeSql(TxtSupplierName.Text)
                Dim supplierAddress As String = escapeSql(TxtAddress.Text)
                Dim supplierTel As String = escapeSql(TxtTel.Text)
                Dim supplierCode As String = escapeSql(TxtSupplierCode.Text)
                Dim sinceDate As String = UtilClass.strFormatDate(dtPurchaseDateSince.Text) '日付の書式を日本の形式に合わせる
                Dim untilDate As String = UtilClass.strFormatDate(dtPurchaseDateUntil.Text) '日付の書式を日本の形式に合わせる
                Dim sinceNum As String = escapeSql(TxtPurchaseSince.Text)
                Dim salesName As String = escapeSql(TxtSales.Text)
                Dim poCode As String = escapeSql(TxtCustomerPO.Text)


                Sql = "SELECT t21.*, t20.取消区分"
                Sql += " FROM public.t21_hattyu t21 "

                Sql += " INNER JOIN t20_hattyu t20"
                Sql += " ON t21.会社コード = t20.会社コード "
                Sql += " AND t21.発注番号 = t20.発注番号"
                Sql += " AND t21.発注番号枝番 = t20.発注番号枝番"

                Sql += " WHERE t21.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' "

                Sql += viewSearchConditions() '抽出条件取得

                Sql += " ORDER BY t21.更新日 DESC, t21.発注番号, t21.発注番号枝番, t21.行番号"


                '得意先と一致する入金明細を取得
                ds = _db.selectDB(Sql, RS, reccnt)

                If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                    DgvHtyhd.Columns.Add("取消", "Cancel")
                    DgvHtyhd.Columns.Add("発注番号", "PurchaseNumber")
                    DgvHtyhd.Columns.Add("発注番号枝番", "PurchaseOrderVer.")
                    DgvHtyhd.Columns.Add("行番号", "LineNo")
                    DgvHtyhd.Columns.Add("仕入区分", "PurchaseClassification")
                    DgvHtyhd.Columns.Add("メーカー", "Manufacturer")
                    DgvHtyhd.Columns.Add("品名", "ItemName")
                    DgvHtyhd.Columns.Add("型式", "Spec")
                    DgvHtyhd.Columns.Add("仕入先名", "SupplierName")

                    DgvHtyhd.Columns.Add("通貨_外貨", "Currency")
                    DgvHtyhd.Columns.Add("仕入値_外貨", "PurchasePrice" & vbCrLf & "(OrignalCurrency)")
                    DgvHtyhd.Columns.Add("仕入値", "PurchasePrice" & vbCrLf & "(" & setBaseCurrency() & ")")

                    DgvHtyhd.Columns.Add("発注数量", "OrderQuantity")
                    DgvHtyhd.Columns.Add("仕入数量", "PurchasedQuantity")
                    DgvHtyhd.Columns.Add("発注残数", "NumberOfOrderRemaining ")
                    DgvHtyhd.Columns.Add("入庫済数量", "InStock")
                    DgvHtyhd.Columns.Add("未入庫数量", "NotReceived")
                    DgvHtyhd.Columns.Add("単位", "Unit")

                    DgvHtyhd.Columns.Add("仕入金額_外貨", "PurchaseCost" & vbCrLf & "(OrignalCurrency)")
                    DgvHtyhd.Columns.Add("仕入金額", "PurchaseCost" & vbCrLf & "(" & setBaseCurrency() & ")")
                    DgvHtyhd.Columns.Add("間接費", "OverHead")

                    DgvHtyhd.Columns.Add("リードタイム", "LeadTime")
                    DgvHtyhd.Columns.Add("貿易条件", "TradeTerms")
                    'DgvHtyhd.Columns.Add("入庫数", "GoodsReceiptQuantity")
                    'DgvHtyhd.Columns.Add("未入庫数", "NoGoodsReceiptQuantity")
                    DgvHtyhd.Columns.Add("備考", "Remarks")
                    DgvHtyhd.Columns.Add("更新者", "ModifiedBy")
                    DgvHtyhd.Columns.Add("登録日", "RegistrationDate")
                    DgvHtyhd.Columns.Add("更新日", "LastUpdateDate")
                Else
                    DgvHtyhd.Columns.Add("取消", "取消")
                    DgvHtyhd.Columns.Add("発注番号", "発注番号")
                    DgvHtyhd.Columns.Add("発注番号枝番", "発注Ver.")
                    DgvHtyhd.Columns.Add("行番号", "行No")
                    DgvHtyhd.Columns.Add("仕入区分", "仕入区分")
                    DgvHtyhd.Columns.Add("メーカー", "メーカー")
                    DgvHtyhd.Columns.Add("品名", "品名")
                    DgvHtyhd.Columns.Add("型式", "型式")
                    DgvHtyhd.Columns.Add("仕入先名", "仕入先名")

                    DgvHtyhd.Columns.Add("通貨_外貨", "仕入通貨")
                    DgvHtyhd.Columns.Add("仕入値_外貨", "仕入単価" & vbCrLf & "(原通貨)")
                    DgvHtyhd.Columns.Add("仕入値", "仕入単価" & vbCrLf & "(" & setBaseCurrency() & ")")


                    DgvHtyhd.Columns.Add("発注数量", "発注数量")
                    DgvHtyhd.Columns.Add("仕入数量", "仕入登録済数量")
                    DgvHtyhd.Columns.Add("発注残数", "発注残数量")
                    DgvHtyhd.Columns.Add("入庫済数量", "入庫済数量")
                    DgvHtyhd.Columns.Add("未入庫数量", "未入庫数量")
                    DgvHtyhd.Columns.Add("単位", "単位")

                    DgvHtyhd.Columns.Add("仕入金額_外貨", "仕入原価" & vbCrLf & "(原通貨)")
                    DgvHtyhd.Columns.Add("仕入金額", "仕入原価" & vbCrLf & "(" & setBaseCurrency() & ")")
                    DgvHtyhd.Columns.Add("間接費", "間接費")

                    DgvHtyhd.Columns.Add("リードタイム", "リードタイム")
                    DgvHtyhd.Columns.Add("貿易条件", "貿易条件")
                    DgvHtyhd.Columns.Add("備考", "備考")
                    DgvHtyhd.Columns.Add("更新者", "更新者")
                    DgvHtyhd.Columns.Add("登録日", "登録日")
                    DgvHtyhd.Columns.Add("更新日", "最終更新日")
                End If

                '伝票単位時のセル書式
                DgvHtyhd.Columns("仕入値_外貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvHtyhd.Columns("仕入値").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvHtyhd.Columns("発注数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvHtyhd.Columns("仕入数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvHtyhd.Columns("発注残数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvHtyhd.Columns("入庫済数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvHtyhd.Columns("未入庫数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvHtyhd.Columns("間接費").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvHtyhd.Columns("仕入金額_外貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvHtyhd.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                'DgvHtyhd.Columns("入庫数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                'DgvHtyhd.Columns("未入庫数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


                '数字形式
                DgvHtyhd.Columns("仕入値_外貨").DefaultCellStyle.Format = "N2"
                DgvHtyhd.Columns("仕入値").DefaultCellStyle.Format = "N2"
                DgvHtyhd.Columns("発注数量").DefaultCellStyle.Format = "N2"
                DgvHtyhd.Columns("仕入数量").DefaultCellStyle.Format = "N2"
                DgvHtyhd.Columns("発注残数").DefaultCellStyle.Format = "N2"
                DgvHtyhd.Columns("入庫済数量").DefaultCellStyle.Format = "N2"
                DgvHtyhd.Columns("未入庫数量").DefaultCellStyle.Format = "N2"
                DgvHtyhd.Columns("間接費").DefaultCellStyle.Format = "N2"
                DgvHtyhd.Columns("仕入金額_外貨").DefaultCellStyle.Format = "N2"
                DgvHtyhd.Columns("仕入金額").DefaultCellStyle.Format = "N2"
                'DgvHtyhd.Columns("入庫数").DefaultCellStyle.Format = "N2"
                'DgvHtyhd.Columns("未入庫数").DefaultCellStyle.Format = "N2"

                DgvHtyhd.Columns("取消").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("取消").ReadOnly = True
                DgvHtyhd.Columns("発注番号").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("発注番号").ReadOnly = True
                DgvHtyhd.Columns("発注番号枝番").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("発注番号枝番").ReadOnly = True
                DgvHtyhd.Columns("行番号").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("行番号").ReadOnly = True
                DgvHtyhd.Columns("仕入区分").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("仕入区分").ReadOnly = True
                DgvHtyhd.Columns("メーカー").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("メーカー").ReadOnly = True
                DgvHtyhd.Columns("品名").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("品名").ReadOnly = True
                DgvHtyhd.Columns("型式").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("型式").ReadOnly = True
                DgvHtyhd.Columns("仕入先名").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("仕入先名").ReadOnly = True
                DgvHtyhd.Columns("通貨_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("通貨_外貨").ReadOnly = True
                DgvHtyhd.Columns("仕入値_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("仕入値_外貨").ReadOnly = True
                DgvHtyhd.Columns("仕入値").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("仕入値").ReadOnly = True
                DgvHtyhd.Columns("発注数量").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("発注数量").ReadOnly = True
                DgvHtyhd.Columns("仕入数量").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("仕入数量").ReadOnly = True
                DgvHtyhd.Columns("発注残数").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("発注残数").ReadOnly = True
                DgvHtyhd.Columns("入庫済数量").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("入庫済数量").ReadOnly = True
                DgvHtyhd.Columns("未入庫数量").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("未入庫数量").ReadOnly = True
                DgvHtyhd.Columns("単位").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("単位").ReadOnly = True
                DgvHtyhd.Columns("仕入金額_外貨").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("仕入金額_外貨").ReadOnly = True
                DgvHtyhd.Columns("仕入金額").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("仕入金額").ReadOnly = True
                DgvHtyhd.Columns("間接費").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("間接費").ReadOnly = True
                DgvHtyhd.Columns("リードタイム").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("リードタイム").ReadOnly = True
                DgvHtyhd.Columns("貿易条件").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("貿易条件").ReadOnly = True
                DgvHtyhd.Columns("備考").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("備考").ReadOnly = True
                DgvHtyhd.Columns("更新者").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("更新者").ReadOnly = True
                DgvHtyhd.Columns("登録日").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("登録日").ReadOnly = True
                DgvHtyhd.Columns("更新日").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DgvHtyhd.Columns("更新日").ReadOnly = True

                Dim dsHanyou As DataSet

                '発注明細ぶん回し
                For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1

                    If IsDBNull(ds.Tables(RS).Rows(i)("通貨")) Then
                        cur = vbNullString
                    Else
                        Sql = " and 採番キー = " & ds.Tables(RS).Rows(i)("通貨")
                        curds = getDsData("m25_currency", Sql)

                        cur = curds.Tables(RS).Rows(0)("通貨コード")
                    End If


                    DgvHtyhd.Rows.Add()
                    DgvHtyhd.Rows(i).Cells("取消").Value = getDelKbnTxt(ds.Tables(RS).Rows(i)("取消区分"))
                    DgvHtyhd.Rows(i).Cells("発注番号").Value = ds.Tables(RS).Rows(i)("発注番号")
                    DgvHtyhd.Rows(i).Cells("発注番号枝番").Value = ds.Tables(RS).Rows(i)("発注番号枝番")
                    DgvHtyhd.Rows(i).Cells("行番号").Value = ds.Tables(RS).Rows(i)("行番号")

                    'リードタイムのリストを汎用マスタから取得
                    dsHanyou = getDsHanyoData(CommonConst.FIXED_KEY_PURCHASING_CLASS, ds.Tables(RS).Rows(i)("仕入区分"))
                    If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                        DgvHtyhd.Rows(i).Cells("仕入区分").Value = dsHanyou.Tables(RS).Rows(0)("文字２")
                    Else
                        DgvHtyhd.Rows(i).Cells("仕入区分").Value = dsHanyou.Tables(RS).Rows(0)("文字１")
                    End If

                    DgvHtyhd.Rows(i).Cells("メーカー").Value = ds.Tables(RS).Rows(i)("メーカー")
                    DgvHtyhd.Rows(i).Cells("品名").Value = ds.Tables(RS).Rows(i)("品名")
                    DgvHtyhd.Rows(i).Cells("型式").Value = ds.Tables(RS).Rows(i)("型式")
                    DgvHtyhd.Rows(i).Cells("仕入先名").Value = ds.Tables(RS).Rows(i)("仕入先名")

                    DgvHtyhd.Rows(i).Cells("通貨_外貨").Value = cur
                    DgvHtyhd.Rows(i).Cells("仕入値_外貨").Value = ds.Tables(RS).Rows(i)("仕入値_外貨")
                    DgvHtyhd.Rows(i).Cells("仕入値").Value = ds.Tables(RS).Rows(i)("仕入値")
                    DgvHtyhd.Rows(i).Cells("発注数量").Value = ds.Tables(RS).Rows(i)("発注数量")
                    DgvHtyhd.Rows(i).Cells("仕入数量").Value = ds.Tables(RS).Rows(i)("仕入数量")
                    DgvHtyhd.Rows(i).Cells("発注残数").Value = ds.Tables(RS).Rows(i)("発注残数")
                    DgvHtyhd.Rows(i).Cells("入庫済数量").Value = ds.Tables(RS).Rows(i)("入庫数")
                    DgvHtyhd.Rows(i).Cells("未入庫数量").Value = ds.Tables(RS).Rows(i)("未入庫数")
                    DgvHtyhd.Rows(i).Cells("単位").Value = ds.Tables(RS).Rows(i)("単位")

                    DgvHtyhd.Rows(i).Cells("仕入金額_外貨").Value = rmNullDecimal(ds.Tables(RS).Rows(i)("仕入値_外貨")) * ds.Tables(RS).Rows(i)("発注数量")
                    DgvHtyhd.Rows(i).Cells("仕入金額").Value = ds.Tables(RS).Rows(i)("仕入値") * ds.Tables(RS).Rows(i)("発注数量")

                    DgvHtyhd.Rows(i).Cells("間接費").Value = ds.Tables(RS).Rows(i)("間接費")

                    If ds.Tables(RS).Rows(i)("リードタイム") Is "" Then
                        DgvHtyhd.Rows(i).Cells("リードタイム").Value = ""
                    Else

                        'リードタイムのリストを汎用マスタから取得
                        dsHanyou = getDsHanyoData(CommonConst.FIXED_KEY_READTIME, ds.Tables(RS).Rows(i)("リードタイム単位").ToString)
                        DgvHtyhd.Rows(i).Cells("リードタイム").Value = ds.Tables(RS).Rows(i)("リードタイム") & dsHanyou.Tables(RS).Rows(0)("文字１").ToString

                    End If

                    If ds.Tables(RS).Rows(i)("貿易条件") IsNot DBNull.Value Then

                        '貿易条件のリストを汎用マスタから取得
                        dsHanyou = getDsHanyoData(CommonConst.FIXED_KEY_TRADE_TERMS, ds.Tables(RS).Rows(i)("貿易条件").ToString)
                        DgvHtyhd.Rows(i).Cells("貿易条件").Value = dsHanyou.Tables(RS).Rows(0)("文字１").ToString
                    End If
                    'DgvHtyhd.Rows(i).Cells("入庫数").Value = ds.Tables(RS).Rows(i)("入庫数")
                    'DgvHtyhd.Rows(i).Cells("未入庫数").Value = ds.Tables(RS).Rows(i)("未入庫数")
                    DgvHtyhd.Rows(i).Cells("備考").Value = ds.Tables(RS).Rows(i)("備考")
                    DgvHtyhd.Rows(i).Cells("更新者").Value = ds.Tables(RS).Rows(i)("更新者")
                    DgvHtyhd.Rows(i).Cells("登録日").Value = ds.Tables(RS).Rows(i)("登録日")
                    DgvHtyhd.Rows(i).Cells("更新日").Value = ds.Tables(RS).Rows(i)("更新日")
                Next

            End If

            '見出しの文字位置
            DgvHtyhd.ReadOnly = False
            'DataGridViewの列幅を固定
            'DgvHtyhd.AllowUserToResizeColumns = True
            'DgvHtyhd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.AllCells
            'DgvHtyhd.AllowUserToResizeRows = True
            '列ヘッダー高さを固定
            'DgvHtyhd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing

            '行ヘッダーの幅を可変
            'DgvHtyhd.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
            'DgvHtyhd.ColumnHeadersHeight = 40

            '自動でサイズを設定するのは、行や列を追加したり、セルに値を設定した後にする。
            DgvHtyhd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            DgvHtyhd.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            DgvHtyhd.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
            DgvHtyhd.Visible = True


        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    Private Sub mPurchaseCost(ByVal OrderNo As String, ByVal BranchNo As String _
                                   , ByRef decPurchase1 As Decimal, ByRef decPurchase2 As Decimal _
                                   , ByRef decPurchaseAmount1 As Decimal, ByRef decPurchaseAmount2 As Decimal)


        Dim reccnt As Integer = 0 'DB用（デフォルト
        Dim Sql As String
        Dim ds_t21 As DataSet

        Sql = "SELECT 仕入値,仕入値_外貨,発注数量,仕入金額,間接費,仕入金額_外貨"

        Sql += " FROM public.t21_hattyu"

        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " and 発注番号 = '" & OrderNo & "'"
        Sql += " and 発注番号枝番 = '" & BranchNo & "'"

        ds_t21 = _db.selectDB(Sql, RS, reccnt)

        decPurchase1 = 0  '外貨
        decPurchase2 = 0
        decPurchaseAmount1 = 0
        decPurchaseAmount2 = 0

        For i As Integer = 0 To ds_t21.Tables(RS).Rows.Count - 1

            decPurchase1 += rmNullDecimal(ds_t21.Tables(RS).Rows(i)("仕入値_外貨")) * ds_t21.Tables(RS).Rows(i)("発注数量")
            decPurchase2 += rmNullDecimal(ds_t21.Tables(RS).Rows(i)("仕入値")) * ds_t21.Tables(RS).Rows(i)("発注数量")

            decPurchaseAmount1 += rmNullDecimal(ds_t21.Tables(RS).Rows(i)("仕入金額_外貨"))
            decPurchaseAmount2 += rmNullDecimal(ds_t21.Tables(RS).Rows(i)("仕入金額") + ds_t21.Tables(RS).Rows(i)("間接費"))

        Next

    End Sub
    '
    'NothingをDecimalに置換
    Private Function rmNullDecimal(ByVal prmField As Object) As Decimal
        If prmField Is Nothing Then
            rmNullDecimal = 0
            Exit Function
        End If
        If prmField Is DBNull.Value Then
            rmNullDecimal = 0
            Exit Function
        End If

        If Not IsNumeric(prmField) Then
            rmNullDecimal = 0
            Exit Function
        End If

        rmNullDecimal = prmField

    End Function
    '検索ボタン押下時
    Private Sub BtnPurchaseSearch_Click(sender As Object, e As EventArgs) Handles BtnPurchaseSearch.Click
        '一覧再表示
        getList()
    End Sub

    '取消データチェックイベント
    Private Sub ChkCancelData_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCancelData.CheckedChanged
        '一覧再表示
        getList()
    End Sub

    Private Sub ChkGoodsReceiptDate_CheckedChanged(sender As Object, e As EventArgs) Handles ChkGoodsReceiptDate.CheckedChanged

        If ChkGoodsReceiptDate.Checked = True Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                lblOrderDate.Text = "GoodsReceiptDate"
            Else
                lblOrderDate.Text = "入庫日"
            End If
        Else
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                lblOrderDate.Text = "PurchaseDate"
            Else
                lblOrderDate.Text = "発注日"
            End If

        End If

        '一覧再表示
        getList()
    End Sub

    'ラジオボタン変更時
    Private Sub RbtnDetails_CheckedChanged(sender As Object, e As EventArgs) Handles RbtnDetails.CheckedChanged
        '一覧再表示
        getList()
    End Sub

    '発注参照ボタン押下時
    Private Sub BtnPurchaseView_Click(sender As Object, e As EventArgs) Handles BtnPurchaseView.Click

        '明細表示時、または対象データがない場合は取消操作不可能
        If DgvHtyhd.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Return

        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvHtyhd.CurrentCell.RowIndex
        Dim No As String = DgvHtyhd.Rows(RowIdx).Cells("発注番号").Value
        Dim Suffix As String = DgvHtyhd.Rows(RowIdx).Cells("発注番号枝番").Value
        Dim Status As String = CommonConst.STATUS_VIEW
        Dim openForm As Form = Nothing
        openForm = New Ordering(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
        openForm.Show(Me)
    End Sub

    '発注複製ボタン押下時
    Private Sub BtnPurchaseClone_Click(sender As Object, e As EventArgs) Handles BtnPurchaseClone.Click
        '明細表示時、または対象データがない場合は取消操作不可能
        If DgvHtyhd.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Return

        End If


        Dim RowIdx As Integer
        RowIdx = Me.DgvHtyhd.CurrentCell.RowIndex
        Dim No As String = DgvHtyhd.Rows(RowIdx).Cells("発注番号").Value
        Dim Suffix As String = DgvHtyhd.Rows(RowIdx).Cells("発注番号枝番").Value
        Dim Status As String = CommonConst.STATUS_CLONE
        Dim openForm As Form = Nothing
        openForm = New Ordering(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
        openForm.ShowDialog(Me)

        '一覧再表示
        getList()

    End Sub

    '発注修正ボタン押下時
    Private Sub BtnPurchaseeEdit_Click(sender As Object, e As EventArgs) Handles BtnPurchaseEdit.Click

        '明細表示時、または対象データがない場合は取消操作不可能
        If DgvHtyhd.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Return

        End If

        '取消済みデータは取消操作不可能
        If DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("取消").Value =
            IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_JPN, CommonConst.CANCEL_KBN_JPN_TXT, CommonConst.CANCEL_KBN_ENG_TXT) Then
            '取消データは選択できないアラートを出す
            _msgHd.dspMSG("cannotSelectTorikeshiData", frmC01F10_Login.loginValue.Language)
            Return
        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvHtyhd.CurrentCell.RowIndex
        Dim No As String = DgvHtyhd.Rows(RowIdx).Cells("発注番号").Value
        Dim Suffix As String = DgvHtyhd.Rows(RowIdx).Cells("発注番号枝番").Value
        Dim status As String = CommonConst.STATUS_EDIT

        '入庫済みなら発注編集不可とする→入庫済みです。入庫取消をしてください。
        'Dim Sql As String = ""
        'Sql = "Select Case 1 from t42_nyukohd t42 where t42.発注番号 ='" & No & "' and t42.発注番号枝番='" & Suffix & "' and t42.取消区分=0 and t42.会社コード='SOFTBANK'
        Dim DS As DataSet = getDsData("t42_nyukohd", " and 発注番号 ='" & No & "' and 発注番号枝番='" & Suffix & "' and 取消区分=0")
        If DS.Tables(RS).Rows.Count() > 0 Then
            _msgHd.dspMSG("cannoteditafterreceipt", frmC01F10_Login.loginValue.Language)
            Return
        End If

        Dim openForm As Form = Nothing
        openForm = New Ordering(_msgHd, _db, _langHd, Me, No, Suffix, status)   '処理選択
        openForm.ShowDialog(Me)

        '一覧再表示
        getList()
    End Sub

    '発注取消ボタン押下時
    Private Sub BtnPurchaseCancel_Click(sender As Object, e As EventArgs) Handles BtnPurchaseCancel.Click

        '明細表示時、または対象データがない場合は取消操作不可能
        If RbtnDetails.Checked Or DgvHtyhd.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("chkDetailsCancel", frmC01F10_Login.loginValue.Language)
            Return

        End If

        Dim dtNow As String = FormatDateTime(DateTime.Now)
        Dim Sql As String = ""

        '取消済みデータは取消操作不可能
        If DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("取消").Value =
            IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_JPN, CommonConst.CANCEL_KBN_JPN_TXT, CommonConst.CANCEL_KBN_ENG_TXT) Then
            '取消データは選択できないアラートを出す
            _msgHd.dspMSG("cannotSelectTorikeshiData", frmC01F10_Login.loginValue.Language)
            Return
        End If


        '支払済みのデータはエラー
        Dim strHatyuNo As String = DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("発注番号").Value
        Dim strEda As String = DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("発注番号枝番").Value

        Dim blnFlg As Boolean = gCheckShiharai(strHatyuNo, strEda)
        If blnFlg = False Then
            '取消データは選択できないアラートを出す
            _msgHd.dspMSG("cannotSelectTorikeshiData_shiharai", frmC01F10_Login.loginValue.Language)
            Return
        End If


        Try
            Dim strHatyu As String = DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("発注番号").Value
            Dim strHatyuEda As String = DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("発注番号枝番").Value


            '取消確認のアラート
            Dim result As DialogResult = _msgHd.dspMSG("confirmCancel", frmC01F10_Login.loginValue.Language)

            If result = DialogResult.No Then
                Exit Sub
            End If


            '仕入、買掛、入庫取消
            blnFlg = gCheckShiire(0, strHatyuNo, strEda)
            If blnFlg = False Then
                'キャンセルボタンの場合は終了
                Exit Sub
            End If


            '発注取消
            blnFlg = gHatyuCancel(strHatyu, strHatyuEda)
            If blnFlg = False Then
                'キャンセルボタンの場合は終了
                Exit Sub
            End If


            getList() 'データ更新

        Catch ex As Exception

            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))

        End Try

    End Sub


    Public Function gHatyuCancel(ByVal strHatyu As String, ByVal strHatyuEda As String) As Boolean


        Dim Sql As String = "UPDATE Public.t20_hattyu "
        Sql += "SET "

        Sql += "取消区分 = " & CommonConst.CANCEL_KBN_DISABLED
        Sql += ", 取消日 = current_date"
        Sql += ", 更新日 = current_timestamp"
        Sql += ", 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "' "

        Sql += "WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND 発注番号 = '" & strHatyu & "'"
        Sql += " AND 発注番号枝番 = '" & strHatyuEda & "'"


        _db.executeDB(Sql)

        gHatyuCancel = True


    End Function


    Public Function gCheckShiire(ByVal intFlg As Long, ByVal strHatyuNo As String, ByVal strEda As String) As Boolean

        Dim reccnt As Integer = 0
        gCheckShiire = False


        Dim strMessage As String = vbNullString
        Dim blnFlg1 As Boolean = False
        Dim blnFlg2 As Boolean = False
        Dim blnFlg3 As Boolean = False

        Dim strNyukoNo As String = vbNullString
        Dim strShiireNo As String = vbNullString
        Dim strKaikakeNo As String = vbNullString


#Region "入庫"


        '発注と結び付いた入庫データが存在するか検索する
        Dim Sql As String = "SELECT 入庫番号"

        Sql += " FROM t42_nyukohd "

        Sql += " WHERE "
        Sql += "     会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " and 発注番号 = '" & strHatyuNo & "'"
        Sql += " and 発注番号枝番 = '" & strEda & "'"
        Sql += " and 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        Dim dsNyuko As DataSet = _db.selectDB(Sql, RS, reccnt)


        If dsNyuko.Tables(RS).Rows.Count = 0 Then
        Else
            '入庫登録あり
            blnFlg1 = True
            gCheckShiire = True

            strNyukoNo = dsNyuko.Tables(RS).Rows(0)("入庫番号")
        End If


        dsNyuko = Nothing

#End Region


#Region "仕入"


        '発注と結び付いた仕入データが存在するか検索する
        Sql = "SELECT 仕入番号"

        Sql += " FROM t40_sirehd "

        Sql += " WHERE "
        Sql += "     会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " and 発注番号 = '" & strHatyuNo & "'"
        Sql += " and 発注番号枝番 = '" & strEda & "'"
        Sql += " and 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        Dim dsShiire As DataSet = _db.selectDB(Sql, RS, reccnt)

        If dsShiire.Tables(RS).Rows.Count = 0 Then
        Else
            '仕入登録あり
            blnFlg2 = True
            gCheckShiire = True

            strShiireNo = dsShiire.Tables(RS).Rows(0)("仕入番号")
        End If


        dsShiire = Nothing

#End Region


#Region "買掛"


        '買掛と結び付いた仕入データが存在するか検索する
        Sql = "SELECT 買掛番号"

        Sql += " FROM t46_kikehd "

        Sql += " WHERE "
        Sql += "     会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " and 発注番号 = '" & strHatyuNo & "'"
        Sql += " and 発注番号枝番 = '" & strEda & "'"
        Sql += " and 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        Dim dsKaikake As DataSet = _db.selectDB(Sql, RS, reccnt)

        If dsKaikake.Tables(RS).Rows.Count = 0 Then
        Else
            '買掛登録あり
            blnFlg3 = True
            gCheckShiire = True

            strKaikakeNo = dsKaikake.Tables(RS).Rows(0)("買掛番号")
        End If


        dsKaikake = Nothing

#End Region


        If intFlg = 0 Then  'メッセージを表示する場合

#Region "メッセージ確認"

            '入庫、仕入、買掛に対象の発注番号がない場合は終了
            If gCheckShiire = False Then
                gCheckShiire = True
                Exit Function
            End If


            '確認メッセージの作成
            If blnFlg1 = True Then
                If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then  '英語
                    strMessage += "Goods receipt registration" & vbCrLf
                Else
                    strMessage += "入庫登録" & vbCrLf
                End If
            End If

            If blnFlg2 = True Then
                If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then  '英語
                    strMessage += "Purchase registration" & vbCrLf
                Else
                    strMessage += "仕入登録" & vbCrLf
                End If
            End If

            If blnFlg3 = True Then
                If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then  '英語
                    strMessage += "Accounts payable registration" & vbCrLf
                Else
                    strMessage += "買掛登録" & vbCrLf
                End If
            End If


            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then  '英語
                strMessage += "Has already been done.Together with order registration" & vbCrLf

                If blnFlg1 = True Then
                    strMessage += "Goods receipt registration"
                End If

                If blnFlg2 = True Then
                    If blnFlg1 = True Then
                        strMessage += "・"
                    End If
                    strMessage += "Purchase registration"
                End If

                If blnFlg3 = True Then
                    If blnFlg1 = True Or blnFlg2 = True Then
                        strMessage += "・"
                    End If
                    strMessage += "Accounts payable registration"
                End If

            Else
                strMessage += "が既になされています。発注登録と合わせて" & vbCrLf

                If blnFlg1 = True Then
                    strMessage += "入庫登録"
                End If

                If blnFlg2 = True Then
                    If blnFlg1 = True Then
                        strMessage += "・"
                    End If
                    strMessage += "仕入登録"
                End If

                If blnFlg3 = True Then
                    If blnFlg1 = True Or blnFlg2 = True Then
                        strMessage += "・"
                    End If
                    strMessage += "買掛登録"
                End If

            End If


            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then  '英語
                strMessage += " Do you also cancel?"
            Else
                strMessage += "も取り消しますか？"
            End If


            Dim result As DialogResult = MessageBox.Show(strMessage, CommonConst.AP_NAME, MessageBoxButtons.OKCancel,
                                                         MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)

            If result = DialogResult.Cancel Then
                'キャンセル場合は終了
                gCheckShiire = False
                Exit Function
            End If

#End Region

        End If


#Region "入庫取消"

        '入庫に対象の発注番号がある場合
        If blnFlg1 = True Then
            Dim blnFlg As Boolean = updateData(strHatyuNo, strEda, strNyukoNo)  '入庫取消
            If blnFlg = False Then
                Exit Function
            End If
        End If


#End Region


#Region "仕入取消"

        '仕入に対象の発注番号がある場合
        If blnFlg2 = True Then
            Dim blnFlg As Boolean = updateShiire(strHatyuNo, strEda, strShiireNo)  '仕入取消
            If blnFlg = False Then
                Exit Function
            End If
        End If

#End Region


#Region "買掛取消"

        '買掛に対象の発注番号がある場合
        If blnFlg3 = True Then
            Dim blnFlg As Boolean = updateKaikake(strKaikakeNo)  '買掛取消
            If blnFlg = False Then
                Exit Function
            End If
        End If

#End Region


        gCheckShiire = True

    End Function


    Private Function updateKaikake(ByVal strKaikakeNo As String) As Boolean

        Dim dtNow As String = UtilClass.formatDatetime(DateTime.Now)
        Dim Sql As String = ""


        Try

            Sql = "UPDATE Public.t46_kikehd "
            Sql += "SET "

            Sql += "取消区分 = " & CommonConst.CANCEL_KBN_DISABLED
            Sql += " , 取消日 = '" & dtNow & "'"
            Sql += " , 更新日 = '" & dtNow & "'"
            Sql += " , 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"

            Sql += "WHERE 会社コード ='" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND 買掛番号 ='" & strKaikakeNo & "'"

            '買掛基本を更新
            _db.executeDB(Sql)


        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try


        updateKaikake = True

    End Function


    Private Function updateShiire(ByVal strHatyuNo As String, ByVal strEda As String, ByVal strShiireNo As String) As Boolean

        Dim dtNow As String = UtilClass.formatDatetime(DateTime.Now)
        Dim reccnt As Integer = 0

        Dim Sql As String = ""

        Try

            Sql = "SELECT "
            Sql += " * "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t21_hattyu "
            Sql += "WHERE"
            Sql += " 会社コード"
            Sql += "='"
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "'"
            Sql += " AND"
            Sql += " 発注番号"
            Sql += "='"
            Sql += strHatyuNo
            Sql += "' "
            Sql += " AND"
            Sql += " 発注番号枝番"
            Sql += "='"
            Sql += strEda
            Sql += "' "

            Dim ds1 As DataSet = _db.selectDB(Sql, RS, reccnt)

            Dim Sql2 As String = ""
            Sql2 += "SELECT "
            Sql2 += " *, coalesce(発注行番号, 行番号, 0) X "
            Sql2 += "FROM "
            Sql2 += "public"
            Sql2 += "."
            Sql2 += "t41_siredt "
            Sql2 += "WHERE"
            Sql2 += " 会社コード"
            Sql2 += "='"
            Sql2 += frmC01F10_Login.loginValue.BumonCD
            Sql2 += "'"
            Sql2 += " AND"
            Sql2 += " 仕入番号"
            Sql2 += "='"
            Sql2 += strShiireNo
            Sql2 += "' "

            Dim ds2 As DataSet = _db.selectDB(Sql2, RS, reccnt)

            Dim Sql3 As String = ""
            Sql3 = ""
            Sql3 += "UPDATE "
            Sql3 += "Public."
            Sql3 += "t40_sirehd "
            Sql3 += "SET "

            Sql3 += "取消区分"
            Sql3 += " = '"
            Sql3 += "1"
            Sql3 += "', "
            Sql3 += "取消日"
            Sql3 += " = '"
            Sql3 += dtNow
            Sql3 += "', "
            Sql3 += "更新日"
            Sql3 += " = '"
            Sql3 += dtNow
            Sql3 += "', "
            Sql3 += "更新者"
            Sql3 += " = '"
            Sql3 += frmC01F10_Login.loginValue.TantoNM
            Sql3 += " ' "

            Sql3 += "WHERE"
            Sql3 += " 会社コード"
            Sql3 += "='"
            Sql3 += frmC01F10_Login.loginValue.BumonCD
            Sql3 += "'"
            Sql3 += " AND"
            Sql3 += " 仕入番号"
            Sql3 += "='"
            Sql3 += strShiireNo
            Sql3 += "' "

            _db.executeDB(Sql3)

            Dim Sql4 As String = ""
            Dim PurchaseNum As Integer = 0
            Dim OrderingNum As Integer = 0

            For index1 As Integer = 0 To ds1.Tables(RS).Rows.Count() - 1
                For index2 As Integer = 0 To ds2.Tables(RS).Rows.Count() - 1
                    If ds1.Tables(RS).Rows(index1)("行番号") = ds2.Tables(RS).Rows(index2)("X") Then
                        Sql4 = ""
                        Sql4 += "UPDATE "
                        Sql4 += "Public."
                        Sql4 += "t21_hattyu "
                        Sql4 += "SET "
                        Sql4 += "仕入数量"
                        Sql4 += " = '"
                        PurchaseNum = ds1.Tables(RS).Rows(index1)("仕入数量") - ds2.Tables(RS).Rows(index2)("仕入数量")
                        'If PurchaseNum < 0 Then
                        '_msgHd.dspMSG("chkAPBalanceError", frmC01F10_Login.loginValue.Language)
                        'End If
                        Sql4 += PurchaseNum.ToString
                        Sql4 += "', "
                        Sql4 += " 発注残数"
                        Sql4 += " = '"
                        OrderingNum = ds1.Tables(RS).Rows(index1)("発注残数") + ds2.Tables(RS).Rows(index2)("仕入数量")
                        Sql4 += OrderingNum.ToString
                        Sql4 += "', "
                        Sql4 += "更新者"
                        Sql4 += " = '"
                        Sql4 += frmC01F10_Login.loginValue.TantoNM
                        Sql4 += "' "
                        Sql4 += "WHERE"
                        Sql4 += " 会社コード"
                        Sql4 += "='"
                        Sql4 += ds1.Tables(RS).Rows(index1)("会社コード")
                        Sql4 += "'"
                        Sql4 += " AND"
                        Sql4 += " 発注番号"
                        Sql4 += "='"
                        Sql4 += ds1.Tables(RS).Rows(index1)("発注番号")
                        Sql4 += "'"
                        Sql4 += " AND"
                        Sql4 += " 発注番号枝番"
                        Sql4 += "='"
                        Sql4 += ds1.Tables(RS).Rows(index1)("発注番号枝番")
                        Sql4 += "'"
                        Sql4 += " AND"
                        Sql4 += " 行番号"
                        Sql4 += "='"
                        Sql4 += ds1.Tables(RS).Rows(index1)("行番号").ToString
                        Sql4 += "' "

                        _db.executeDB(Sql4)

                        Sql4 = ""
                        PurchaseNum = 0
                        OrderingNum = 0
                    End If
                Next
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

        updateShiire = True

    End Function

    Private Function updateData(ByVal strHatyuNo As String, ByVal strEda As String, ByVal strNyukoNo As String) As String

        Dim dtNow As String = FormatDateTime(DateTime.Now)
        Dim Sql As String = ""

        Try

            '発注データ
            Sql = " AND "
            Sql += "発注番号 ILIKE '" & strHatyuNo & "'"
            Sql += " AND "
            Sql += "発注番号枝番 ILIKE '" & strEda & "'"

            Dim dsHattyudt As DataSet = getDsData("t21_hattyu", Sql)

            '入庫データ
            Sql = " AND"
            Sql += " 入庫番号"
            Sql += "='"
            Sql += strNyukoNo
            Sql += "'"

            Dim dsNyukodt As DataSet = getDsData("t43_nyukodt", Sql)

            Sql = "UPDATE "
            Sql += "Public."
            Sql += "t42_nyukohd "
            Sql += "SET "

            Sql += "取消区分 = " & CommonConst.CANCEL_KBN_DISABLED '取消区分：1
            Sql += ", "
            Sql += "取消日"
            Sql += " = '"
            Sql += UtilClass.strFormatDate(dtNow)
            Sql += "', "
            Sql += "更新日"
            Sql += " = '"
            Sql += UtilClass.strFormatDate(dtNow)
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
            Sql += strNyukoNo
            Sql += "' "

            _db.executeDB(Sql)

            Sql = "UPDATE "
            Sql += "Public."
            Sql += "t43_nyukodt "
            Sql += "SET "

            Sql += "更新日"
            Sql += " = '"
            Sql += UtilClass.strFormatDate(dtNow)
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
            Sql += strNyukoNo
            Sql += "' "

            _db.executeDB(Sql)

            '発注データを更新する
            For i As Integer = 0 To dsHattyudt.Tables(RS).Rows.Count - 1
                For x As Integer = 0 To dsNyukodt.Tables(RS).Rows.Count - 1

                    '行番号が一致したら
                    If dsHattyudt.Tables(RS).Rows(i)("行番号") = dsNyukodt.Tables(RS).Rows(x)("行番号") Then
                        Dim calShukko As Integer = dsHattyudt.Tables(RS).Rows(i)("入庫数") - dsNyukodt.Tables(RS).Rows(x)("入庫数量")
                        Dim calUnShukko As Integer = dsHattyudt.Tables(RS).Rows(i)("未入庫数") + dsNyukodt.Tables(RS).Rows(x)("入庫数量")

                        Sql = "update t21_hattyu set "
                        Sql += "入庫数 = '" & calShukko & "'"
                        Sql += ",未入庫数 = '" & calUnShukko & "'"
                        Sql += ",更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
                        Sql += " where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                        Sql += " AND "
                        Sql += "発注番号 ILIKE '" & strHatyuNo & "'"
                        Sql += " AND "
                        Sql += "発注番号枝番 ILIKE '" & strEda & "'"
                        Sql += " AND "
                        Sql += "行番号 = '" & dsHattyudt.Tables(RS).Rows(i)("行番号") & "'"

                        _db.executeDB(Sql)

                        Sql = "update t20_hattyu set "
                        Sql += "更新日 = '" & UtilClass.strFormatDate(dtNow) & "'"
                        Sql += ",更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
                        Sql += " where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                        Sql += " AND "
                        Sql += "発注番号 ILIKE '" & strHatyuNo & "'"
                        Sql += " AND "
                        Sql += "発注番号枝番 ILIKE '" & strEda & "'"
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
            Sql += UtilClass.strFormatDate(dtNow)
            Sql += "', "
            Sql += "取消区分"
            Sql += " = '"
            Sql += CommonConst.CANCEL_KBN_DISABLED.ToString
            Sql += "', "
            Sql += "更新日"
            Sql += " = '"
            Sql += UtilClass.strFormatDate(dtNow)
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
            Sql += strNyukoNo
            Sql += "' "

            _db.executeDB(Sql)

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

        updateData = True

    End Function



    Public Function gCheckShiharai(ByVal strHatyuNo As String, ByVal strEda As String) As Boolean

        Dim reccnt As Integer = 0


        '発注と結び付いた支払データが存在するか検索する
        '発注番号で買掛データを検索 → 買掛番号で取消されていない支払データを検索
        Dim Sql As String = "SELECT t46.買掛番号 as 買掛1, t49.買掛番号 as 買掛2"

        Sql += " FROM t46_kikehd t46 left join t49_shrikshihd t49"
        Sql += " on t46.買掛番号 = t49.買掛番号"

        Sql += " WHERE "
        Sql += "     t46.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " and t46.発注番号 = '" & strHatyuNo & "'"
        Sql += " and t46.発注番号枝番 = '" & strEda & "'"
        Sql += " and t49.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        Dim dsShiharai As DataSet = _db.selectDB(Sql, RS, reccnt)

        If dsShiharai.Tables(RS).Rows.Count = 0 Then
            '対象の出庫データがない場合は正常終了
            gCheckShiharai = True
        Else
            Dim strMoji = Convert.ToString(dsShiharai.Tables(RS).Rows(0)("買掛2"))
            If String.IsNullOrEmpty(strMoji) Then
                '対象の出庫データがない場合は正常終了
                gCheckShiharai = True
            Else
                '対象の出庫データがあった場合は入庫取消ができない
                gCheckShiharai = False
            End If
        End If


        dsShiharai = Nothing


    End Function


    '仕入入力ボタン押下時
    Private Sub BtnOrding_Click(sender As Object, e As EventArgs) Handles BtnOrding.Click

        '明細表示時、または対象データがない場合は取消操作不可能
        If DgvHtyhd.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Return

        End If

        '取消済みデータは取消操作不可能
        If DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("取消").Value =
            IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_JPN, CommonConst.CANCEL_KBN_JPN_TXT, CommonConst.CANCEL_KBN_ENG_TXT) Then
            '取消データは選択できないアラートを出す
            _msgHd.dspMSG("cannotSelectTorikeshiData", frmC01F10_Login.loginValue.Language)
            Return
        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvHtyhd.CurrentCell.RowIndex
        Dim No As String = DgvHtyhd.Rows(RowIdx).Cells("発注番号").Value
        Dim Suffix As String = DgvHtyhd.Rows(RowIdx).Cells("発注番号枝番").Value
        Dim openForm As Form = Nothing
        openForm = New PurchasingManagement(_msgHd, _db, _langHd, No, Suffix)   '処理選択
        openForm.Show(Me)

    End Sub

    '入庫入力ボタン押下時
    Private Sub BtnGoodsIssue_Click(sender As Object, e As EventArgs) Handles BtnReceipt.Click

        '明細表示時、または対象データがない場合は取消操作不可能
        If DgvHtyhd.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Return

        End If

        '取消済みデータは取消操作不可能
        If DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("取消").Value =
            IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_JPN, CommonConst.CANCEL_KBN_JPN_TXT, CommonConst.CANCEL_KBN_ENG_TXT) Then
            '取消データは選択できないアラートを出す
            _msgHd.dspMSG("cannotSelectTorikeshiData", frmC01F10_Login.loginValue.Language)
            Return
        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvHtyhd.CurrentCell.RowIndex
        Dim No As String = DgvHtyhd.Rows(RowIdx).Cells("発注番号").Value
        Dim Suffix As String = DgvHtyhd.Rows(RowIdx).Cells("発注番号枝番").Value
        Dim openForm As Form = Nothing
        openForm = New Receipt(_msgHd, _db, _langHd, No, Suffix)   '処理選択
        openForm.Show(Me)

    End Sub

    '買掛登録ボタン押下時
    Private Sub BtnAP_Click(sender As Object, e As EventArgs) Handles BtnAP.Click

        '明細表示時、または対象データがない場合は取消操作不可能
        If DgvHtyhd.Rows.Count = 0 Then

            '操作できないアラートを出す
            _msgHd.dspMSG("NonAction", frmC01F10_Login.loginValue.Language)
            Return

        End If

        '明細表示時は取消操作不可能
        If RbtnDetails.Checked Then

            '操作できないアラートを出す
            _msgHd.dspMSG("chkDetailsCancel", frmC01F10_Login.loginValue.Language)
            Return

        End If

        '取消済みデータは取消操作不可能
        If DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("取消").Value =
            IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_JPN, CommonConst.CANCEL_KBN_JPN_TXT, CommonConst.CANCEL_KBN_ENG_TXT) Then
            '取消データは選択できないアラートを出す
            _msgHd.dspMSG("cannotSelectTorikeshiData", frmC01F10_Login.loginValue.Language)
            Return
        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvHtyhd.CurrentCell.RowIndex
        Dim No As String = DgvHtyhd.Rows(RowIdx).Cells("発注番号").Value
        Dim Suffix As String = DgvHtyhd.Rows(RowIdx).Cells("発注番号枝番").Value
        Dim openForm As Form = Nothing
        openForm = New AccountsPayable(_msgHd, _db, _langHd, Me, No, Suffix)   '処理選択
        Me.Enabled = False
        Me.Hide()
        openForm.ShowDialog(Me)

        '一覧再表示
        getList()

    End Sub


    'Excel出力ボタン押下時
    Private Sub BtnExcelOutput_Click(sender As Object, e As EventArgs) Handles BtnExcelOutput.Click
        '対象データがない場合は取消操作不可能
        If DgvHtyhd.Rows.Count = 0 Then

            '該当データがないアラートを出す
            _msgHd.dspMSG("noTargetData", frmC01F10_Login.loginValue.Language)

            Return
        End If

        '明細表示時は取消操作不可能
        If RbtnDetails.Checked Then

            '操作できないアラートを出す
            _msgHd.dspMSG("chkDetailsCancel", frmC01F10_Login.loginValue.Language)
            Return
        End If


        'Excel出力処理
        outputExcel()


    End Sub


    'excel出力処理
    Private Sub outputExcel()

        '定義
        Dim app As Excel.Application = Nothing
        Dim books As Excel.Workbooks = Nothing
        Dim book As Excel.Workbook = Nothing
        Dim sheet As Excel.Worksheet = Nothing

        'Dim dtToday As DateTime = DateTime.Today

        ' セル
        Dim xlRngTmp As Range = Nothing
        Dim xlRng As Range = Nothing

        ' セル境界線（枠）
        Dim xlBorders As Borders = Nothing
        Dim xlBorder As Border = Nothing

        Dim filePath As String = ""

        'カーソルをビジー状態にする
        Cursor.Current = Cursors.WaitCursor

        Try
            'インスタンス作成
            Dim sfd As New SaveFileDialog()

            sfd.Filter = "Microsoft Excel ブック(*.xlsx)|*.xlsx" '保存ファイルの形式を指定
            sfd.FileName = "AccountsPayableBulkList_" & DateTime.Now.ToString("yyyyMMddHHmm") & ".xlsx"

            'ダイアログを表示する
            If sfd.ShowDialog() = DialogResult.OK Then

                'OKボタンがクリックされたとき、選択されたファイル名を表示する
                filePath = sfd.FileName

                '雛形パス
                Dim sHinaPath As String = StartUp._iniVal.BaseXlsPath
                '雛形ファイル名
                Dim sHinaFile As String = sHinaPath & "\" & "AccountsPayableBulkList.xlsx"

                app = New Excel.Application()
                books = app.Workbooks
                book = books.Add(sHinaFile)  'テンプレート
                sheet = CType(book.Worksheets(1), Excel.Worksheet)

                '見出し 英語表示対応
                If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                    sheet.PageSetup.LeftHeader = "Inventory control table"
                    sheet.PageSetup.RightHeader = "OutputDate：" & DateTime.Now.ToShortDateString

                    sheet.Range("A1").Value = "PurchaseOrderNumber" '発注番号
                    sheet.Range("B1").Value = "PurchaseOrderSubNumber" '発注番号枝番
                    sheet.Range("C1").Value = "BillingDate" '請求日
                    sheet.Range("D1").Value = "SupplierInvoiceNumber" '仕入先請求番号
                    sheet.Range("E1").Value = "PaymentDueDate" '支払予定日
                    sheet.Range("F1").Value = "Remark1" '備考1
                    sheet.Range("G1").Value = "Remark2" '備考2
                End If


                'For Each setRow As DataRow In prmkikeData.Rows
                For i As Integer = 0 To DgvHtyhd.Rows.Count - 1

                    sheet.Range("A" & i + 2).Value = DgvHtyhd.Rows(i).Cells("発注番号").Value '発注番号
                    sheet.Range("B" & i + 2).Value = DgvHtyhd.Rows(i).Cells("発注番号枝番").Value '発注番号枝番

                Next

                app.DisplayAlerts = False 'Microsoft Excelのアラート一旦無効化

                Dim excelChk As Boolean = excelOutput(filePath)
                If excelChk = False Then
                    Exit Sub
                End If
                book.SaveAs(filePath) '書き込み実行

                app.DisplayAlerts = True 'アラート無効化を解除
                app.Visible = True

                'カーソルを砂時計から元に戻す
                Cursor.Current = Cursors.Default

                _msgHd.dspMSG("CreateExcel", frmC01F10_Login.loginValue.Language)

                'app.Quit()

                'リソースの解放
                Marshal.ReleaseComObject(sheet)
                Marshal.ReleaseComObject(book)
                Marshal.ReleaseComObject(books)
                Marshal.ReleaseComObject(app)
            End If

        Catch ex As Exception
            'カーソルをビジー状態から元に戻す
            Cursor.Current = Cursors.Default

            Throw ex
        Finally

        End Try


    End Sub


    'Excel出力する際のチェック
    Private Function excelOutput(ByVal prmFilePath As String)
        Dim fileChk As String = Dir(prmFilePath)
        '同名ファイルがあるかどうかチェック
        If fileChk <> "" Then
            Dim result = _msgHd.dspMSG("confirmFileExist", frmC01F10_Login.loginValue.Language, prmFilePath)
            If result = DialogResult.No Then
                Return False
            End If

            Try
                'ファイルが開けるかどうかチェック
                Dim sr As StreamReader = New StreamReader(prmFilePath)
                sr.Close() '処理が通ったら閉じる
            Catch ex As Exception
                '開けない場合はアラートを表示してリターンさせる
                MessageBox.Show(ex.Message, CommonConst.AP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try

            Return True
        End If
        Return True
    End Function


    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    '抽出条件取得
    Private Function viewSearchConditions() As String
        Dim Sql As String = ""

        '抽出条件
        Dim supplierName As String = UtilClass.escapeSql(TxtSupplierName.Text)
        Dim supplierAddress As String = UtilClass.escapeSql(TxtAddress.Text)
        Dim supplierTel As String = UtilClass.escapeSql(TxtTel.Text)
        Dim supplierCode As String = UtilClass.escapeSql(TxtSupplierCode.Text)
        Dim sinceDate As String = UtilClass.strFormatDate(dtPurchaseDateSince.Text) '日付の書式を日本の形式に合わせる
        Dim untilDate As String = UtilClass.strFormatDate(dtPurchaseDateUntil.Text) '日付の書式を日本の形式に合わせる
        Dim sinceNum As String = UtilClass.escapeSql(TxtPurchaseSince.Text)
        Dim salesName As String = UtilClass.escapeSql(TxtSales.Text)
        Dim poCode As String = UtilClass.escapeSql(TxtCustomerPO.Text)
        Dim itemName As String = UtilClass.escapeSql(TxtItemName.Text)
        Dim spec As String = UtilClass.escapeSql(TxtSpec.Text)
        Dim Maker As String = UtilClass.escapeSql(txtMaker.Text)

        If supplierName <> Nothing Then
            Sql += " AND t20.仕入先名 ILIKE '%" & supplierName & "%' "
        End If

        If supplierAddress <> Nothing Then
            Sql += " AND t20.仕入先住所 ILIKE '%" & supplierAddress & "%' "
        End If

        If supplierTel <> Nothing Then
            Sql += " AND t20.仕入先電話番号 ILIKE '%" & supplierTel & "%' "
        End If

        If supplierCode <> Nothing Then
            Sql += " AND t20.仕入先コード ILIKE '%" & supplierCode & "%' "
        End If

        If sinceDate <> Nothing Then
            Sql += " AND t20.発注日 >= '" & sinceDate & "'"
        End If
        If untilDate <> Nothing Then
            Sql += " AND t20.発注日 <= '" & untilDate & "'"
        End If

        If sinceNum <> Nothing Then
            Sql += " AND t20.発注番号 ILIKE '%" & sinceNum & "%' "
        End If

        If salesName <> Nothing Then
            Sql += " AND t20.受注番号 ILIKE '%" & salesName & "%' "
        End If

        If poCode <> Nothing Then
            Sql += " AND t20.客先番号 ILIKE '%" & poCode & "%' "
        End If

        If Maker <> Nothing Then
            Sql += " AND t21.メーカー ILIKE '%" & Maker & "%' "
        End If

        If itemName <> Nothing Then
            Sql += " AND t21.品名 ILIKE '%" & itemName & "%' "
        End If

        If spec <> Nothing Then
            Sql += " AND t21.型式 ILIKE '%" & spec & "%' "
        End If


        If ChkGoodsReceiptDate.Checked = True Then  '入庫にチェック
            Sql += " and t20.発注番号 In(Select 発注番号 from t42_nyukohd t42"

            Dim sql2 As String = vbNullString


            If sinceDate <> Nothing Then
                sql2 += " where t42.入庫日 >= '" & sinceDate & "'"
            End If
            If untilDate <> Nothing Then
                If sql2 = vbNullString Then
                    sql2 += " where t42.入庫日 <= '" & untilDate & "'"
                Else
                    sql2 += " AND t42.入庫日 <= '" & untilDate & "'"
                End If
            End If

            Sql += sql2 + ")"
        Else
        End If

        '取消データを含めない場合
        If ChkCancelData.Checked = False Then
            Sql += " AND t20.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
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

        Sql += "SELECT * FROM "

        Sql += "public." & tableName
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' "
        Sql += txtParam

        Return _db.selectDB(Sql, RS, reccnt)
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

    Private Sub OrderingList_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        '一覧再表示
        'getList()
        'DgvHtyhd.Visible = True
    End Sub

    '基準通貨の取得
    Private Function setBaseCurrency() As String
        Dim Sql As String
        '通貨表示：ベースの設定
        Sql = " AND 採番キー = " & CommonConst.CURRENCY_CD_IDR.ToString
        Sql += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString

        Dim ds As DataSet = getDsData("m25_currency", Sql)
        setBaseCurrency = ds.Tables(RS).Rows(0)("通貨コード")

    End Function


End Class