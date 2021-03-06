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

Public Class PurchaseList
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

    '仕入一覧を表示
    Private Sub PurchaseListLoad()

        '一覧をクリア
        DgvHtyhd.Rows.Clear()
        DgvHtyhd.Columns.Clear()

        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Dim curds As DataSet  'm25_currency
        Dim cur As String

        Try
            '伝票単位の場合
            If RbtnSlip.Checked Then

                Sql = "SELECT t40.*,t20.通貨"

                Sql += " FROM public.t40_sirehd t40 "
                Sql += " left join t20_hattyu t20"
                Sql += " on t40.発注番号 = t20.発注番号 and t40.発注番号枝番 = t20.発注番号枝番"

                Sql += " WHERE t40.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

                Sql += viewSearchConditions() '検索条件

                Sql += " ORDER BY "
                Sql += " t40.更新日 DESC"

                ds = _db.selectDB(Sql, RS, reccnt)

                setHdColumns() '表示カラムの設定

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
                    DgvHtyhd.Rows(i).Cells("仕入番号").Value = ds.Tables(RS).Rows(i)("仕入番号")
                    DgvHtyhd.Rows(i).Cells("客先番号").Value = ds.Tables(RS).Rows(i)("客先番号")
                    DgvHtyhd.Rows(i).Cells("発注番号").Value = ds.Tables(RS).Rows(i)("発注番号")
                    DgvHtyhd.Rows(i).Cells("発注番号枝番").Value = ds.Tables(RS).Rows(i)("発注番号枝番")
                    DgvHtyhd.Rows(i).Cells("仕入日").Value = ds.Tables(RS).Rows(i)("仕入日")
                    DgvHtyhd.Rows(i).Cells("仕入先コード").Value = ds.Tables(RS).Rows(i)("仕入先コード")
                    DgvHtyhd.Rows(i).Cells("仕入先名").Value = ds.Tables(RS).Rows(i)("仕入先名")
                    DgvHtyhd.Rows(i).Cells("仕入先郵便番号").Value = ds.Tables(RS).Rows(i)("仕入先郵便番号")
                    DgvHtyhd.Rows(i).Cells("仕入先住所").Value = ds.Tables(RS).Rows(i)("仕入先住所")
                    DgvHtyhd.Rows(i).Cells("仕入先電話番号").Value = ds.Tables(RS).Rows(i)("仕入先電話番号")
                    DgvHtyhd.Rows(i).Cells("仕入先ＦＡＸ").Value = ds.Tables(RS).Rows(i)("仕入先ＦＡＸ")
                    DgvHtyhd.Rows(i).Cells("仕入先担当者名").Value = ds.Tables(RS).Rows(i)("仕入先担当者名")
                    DgvHtyhd.Rows(i).Cells("仕入先担当者役職").Value = ds.Tables(RS).Rows(i)("仕入先担当者役職")

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

                    DgvHtyhd.Rows(i).Cells("支払条件").Value = ds.Tables(RS).Rows(i)("支払条件")
                    DgvHtyhd.Rows(i).Cells("営業担当者").Value = ds.Tables(RS).Rows(i)("営業担当者")
                    DgvHtyhd.Rows(i).Cells("入力担当者").Value = ds.Tables(RS).Rows(i)("入力担当者")
                    DgvHtyhd.Rows(i).Cells("備考").Value = ds.Tables(RS).Rows(i)("備考")
                    DgvHtyhd.Rows(i).Cells("登録日").Value = ds.Tables(RS).Rows(i)("登録日")
                    DgvHtyhd.Rows(i).Cells("最終更新日").Value = ds.Tables(RS).Rows(i)("更新日")

                Next

                '数字形式
                DgvHtyhd.Columns("仕入原価_外貨").DefaultCellStyle.Format = "N2"
                DgvHtyhd.Columns("仕入原価").DefaultCellStyle.Format = "N2"
                DgvHtyhd.Columns("仕入金額_外貨").DefaultCellStyle.Format = "N2"
                DgvHtyhd.Columns("仕入金額").DefaultCellStyle.Format = "N2"
            Else

                '明細単位の場合

                Sql = "SELECT t41.*,t20.通貨,t40.登録日,t40.更新日 FROM  public.t41_siredt t41 "
                Sql += " INNER JOIN  t40_sirehd t40 "
                Sql += " ON t41.会社コード = t40.会社コード"
                Sql += " AND  t41.仕入番号 = t40.仕入番号"

                Sql += " left join t20_hattyu t20"
                Sql += " on t40.発注番号 = t20.発注番号 and t40.発注番号枝番 = t20.発注番号枝番"

                Sql += " WHERE t41.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

                Sql += viewSearchConditions() '検索条件

                Sql += " ORDER BY "
                Sql += "t41.更新日 DESC"

                ds = _db.selectDB(Sql, RS, reccnt)

                setDtColumns() '表示カラムの設定

                For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1

                    If IsDBNull(ds.Tables(RS).Rows(i)("通貨")) Then
                        cur = vbNullString
                    Else
                        Sql = " and 採番キー = " & ds.Tables(RS).Rows(i)("通貨")
                        curds = getDsData("m25_currency", Sql)

                        cur = curds.Tables(RS).Rows(0)("通貨コード")
                    End If

                    DgvHtyhd.Rows.Add()
                    DgvHtyhd.Rows(i).Cells("仕入番号").Value = ds.Tables(RS).Rows(i)("仕入番号")
                    DgvHtyhd.Rows(i).Cells("行番号").Value = ds.Tables(RS).Rows(i)("行番号")
                    DgvHtyhd.Rows(i).Cells("発注番号").Value = ds.Tables(RS).Rows(i)("発注番号")
                    DgvHtyhd.Rows(i).Cells("発注番号枝番").Value = ds.Tables(RS).Rows(i)("発注番号枝番")
                    DgvHtyhd.Rows(i).Cells("仕入区分").Value = ds.Tables(RS).Rows(i)("仕入区分")

                    'リードタイムのリストを汎用マスタから取得
                    Dim dsHanyou As DataSet = getDsHanyoData(CommonConst.FIXED_KEY_PURCHASING_CLASS, ds.Tables(RS).Rows(i)("仕入区分"))
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

                    Dim decPurchase1 As Decimal = 0
                    Dim decPurchase2 As Decimal = 0
                    Dim decPurchaseAmount1 As Decimal = 0
                    Dim decPurchaseAmount2 As Decimal = 0

                    Call mPurchaseCost2(ds.Tables(RS).Rows(i)("発注番号"), ds.Tables(RS).Rows(i)("発注番号枝番"), ds.Tables(RS).Rows(i)("行番号") _
                                       , decPurchase1, decPurchase2 _
                                       , decPurchaseAmount1, decPurchaseAmount2)


                    DgvHtyhd.Rows(i).Cells("仕入値_外貨").Value = decPurchase1
                    DgvHtyhd.Rows(i).Cells("仕入値").Value = decPurchase2

                    DgvHtyhd.Rows(i).Cells("発注数量").Value = ds.Tables(RS).Rows(i)("発注数量")
                    DgvHtyhd.Rows(i).Cells("仕入数量").Value = ds.Tables(RS).Rows(i)("仕入数量")
                    DgvHtyhd.Rows(i).Cells("発注残数").Value = ds.Tables(RS).Rows(i)("発注残数")
                    DgvHtyhd.Rows(i).Cells("単位").Value = ds.Tables(RS).Rows(i)("単位")

                    DgvHtyhd.Rows(i).Cells("仕入金額_外貨").Value = decPurchaseAmount1
                    DgvHtyhd.Rows(i).Cells("仕入金額").Value = decPurchaseAmount2
                    DgvHtyhd.Rows(i).Cells("間接費").Value = ds.Tables(RS).Rows(i)("間接費")

                    DgvHtyhd.Rows(i).Cells("リードタイム").Value = ds.Tables(RS).Rows(i)("リードタイム")
                    DgvHtyhd.Rows(i).Cells("備考").Value = ds.Tables(RS).Rows(i)("備考")
                    DgvHtyhd.Rows(i).Cells("更新者").Value = ds.Tables(RS).Rows(i)("更新者")

                    DgvHtyhd.Rows(i).Cells("登録日").Value = ds.Tables(RS).Rows(i)("登録日")
                    DgvHtyhd.Rows(i).Cells("最終更新日").Value = ds.Tables(RS).Rows(i)("更新日")

                Next

                '数字形式
                DgvHtyhd.Columns("仕入値_外貨").DefaultCellStyle.Format = "N2"
                DgvHtyhd.Columns("仕入値").DefaultCellStyle.Format = "N2"
                DgvHtyhd.Columns("発注数量").DefaultCellStyle.Format = "N2"
                DgvHtyhd.Columns("仕入数量").DefaultCellStyle.Format = "N2"
                DgvHtyhd.Columns("発注残数").DefaultCellStyle.Format = "N2"
                DgvHtyhd.Columns("仕入金額_外貨").DefaultCellStyle.Format = "N2"
                DgvHtyhd.Columns("仕入金額").DefaultCellStyle.Format = "N2"
                DgvHtyhd.Columns("間接費").DefaultCellStyle.Format = "N2"

            End If

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

        Sql = "SELECT"
        Sql += " 仕入値,仕入値_外貨,発注数量,仕入金額,間接費,仕入金額_外貨"

        Sql += " FROM "
        Sql += " public.t21_hattyu"

        Sql += " WHERE "
        Sql += " 会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"

        Sql += " and 発注番号 = '" & OrderNo & "'"
        Sql += " and 発注番号枝番 = '" & BranchNo & "'"

        ds_t21 = _db.selectDB(Sql, RS, reccnt)

        decPurchase1 = 0  '外貨
        decPurchase2 = 0
        decPurchaseAmount1 = 0
        decPurchaseAmount2 = 0

        For i As Integer = 0 To ds_t21.Tables(RS).Rows.Count - 1

            decPurchase1 += rmNullDecimal(ds_t21.Tables(RS).Rows(i)("仕入値_外貨") * ds_t21.Tables(RS).Rows(i)("発注数量"))
            decPurchase2 += rmNullDecimal(ds_t21.Tables(RS).Rows(i)("仕入値") * ds_t21.Tables(RS).Rows(i)("発注数量"))

            decPurchaseAmount1 += rmNullDecimal(ds_t21.Tables(RS).Rows(i)("仕入金額_外貨"))
            decPurchaseAmount2 += rmNullDecimal(ds_t21.Tables(RS).Rows(i)("仕入金額") + ds_t21.Tables(RS).Rows(i)("間接費"))

        Next

    End Sub

    Private Sub mPurchaseCost2(ByVal OrderNo As String, ByVal BranchNo As String, ByVal No As String _
                                   , ByRef decPurchase1 As Decimal, ByRef decPurchase2 As Decimal _
                                   , ByRef decPurchaseAmount1 As Decimal, ByRef decPurchaseAmount2 As Decimal)


        Dim reccnt As Integer = 0 'DB用（デフォルト
        Dim Sql As String
        Dim ds_t21 As DataSet

        Sql = "SELECT"
        Sql += " 仕入値,仕入値_外貨,発注数量,仕入金額,間接費,仕入金額_外貨"

        Sql += " FROM "
        Sql += " public.t21_hattyu"

        Sql += " WHERE "
        Sql += " 会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"

        Sql += " and 発注番号 = '" & OrderNo & "'"
        Sql += " and 発注番号枝番 = '" & BranchNo & "'"
        Sql += " and 行番号 = '" & No & "'"

        ds_t21 = _db.selectDB(Sql, RS, reccnt)

        decPurchase1 = 0  '外貨
        decPurchase2 = 0
        decPurchaseAmount1 = 0
        decPurchaseAmount2 = 0

        For i As Integer = 0 To ds_t21.Tables(RS).Rows.Count - 1

            decPurchase1 += rmNullDecimal(ds_t21.Tables(RS).Rows(i)("仕入値_外貨"))
            decPurchase2 += rmNullDecimal(ds_t21.Tables(RS).Rows(i)("仕入値"))

            decPurchaseAmount1 += rmNullDecimal(ds_t21.Tables(RS).Rows(i)("仕入値_外貨") * ds_t21.Tables(RS).Rows(i)("発注数量"))
            decPurchaseAmount2 += rmNullDecimal(ds_t21.Tables(RS).Rows(i)("仕入値") * ds_t21.Tables(RS).Rows(i)("発注数量"))

        Next

    End Sub

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

    '使用言語に合わせて仕入基本見出しを切替
    Private Sub setHdColumns()

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            DgvHtyhd.Columns.Add("取消", "Cancel")
            DgvHtyhd.Columns.Add("発注番号", "PurchaseOrderNumber")
            DgvHtyhd.Columns.Add("発注番号枝番", "PurchaseOrderVer.")
            DgvHtyhd.Columns.Add("仕入番号", "PurchaseNumber")
            DgvHtyhd.Columns.Add("客先番号", "CustomerNumber")
            DgvHtyhd.Columns.Add("仕入日", "PurchaseDate")
            DgvHtyhd.Columns.Add("仕入先コード", "SupplierCode")
            DgvHtyhd.Columns.Add("仕入先名", "SupplierName")
            DgvHtyhd.Columns.Add("仕入先郵便番号", "PostalCode")
            DgvHtyhd.Columns.Add("仕入先住所", "Address")
            DgvHtyhd.Columns.Add("仕入先電話番号", "PhoneNumber")
            DgvHtyhd.Columns.Add("仕入先ＦＡＸ", "FAX")
            DgvHtyhd.Columns.Add("仕入先担当者名", "NameOfPIC")
            DgvHtyhd.Columns.Add("仕入先担当者役職", "PositionPICSupplier")

            DgvHtyhd.Columns.Add("通貨_外貨", "Currency")
            DgvHtyhd.Columns.Add("仕入原価_外貨", "PurchaseCost" & vbCrLf & "(OrignalCurrency)")
            DgvHtyhd.Columns.Add("仕入原価", "PurchaseCost" & vbCrLf & "(" & setBaseCurrency() & ")")
            DgvHtyhd.Columns.Add("仕入金額_外貨", "PurchaseAmount" & vbCrLf & "(OrignalCurrency)")
            DgvHtyhd.Columns.Add("仕入金額", "PurchaseAmount" & vbCrLf & "(" & setBaseCurrency() & ")")

            DgvHtyhd.Columns.Add("支払条件", "PaymentTerms")
            DgvHtyhd.Columns.Add("営業担当者", "SalesPersonInCharge")
            DgvHtyhd.Columns.Add("入力担当者", "PICForInputting")
            DgvHtyhd.Columns.Add("備考", "Remarks")
            DgvHtyhd.Columns.Add("登録日", "RegistrationDate")
            DgvHtyhd.Columns.Add("最終更新日", "LastUpdateDate")

        Else
            DgvHtyhd.Columns.Add("取消", "取消")
            DgvHtyhd.Columns.Add("発注番号", "発注番号")
            DgvHtyhd.Columns.Add("発注番号枝番", "発注Ver.")
            DgvHtyhd.Columns.Add("仕入番号", "仕入番号")
            DgvHtyhd.Columns.Add("客先番号", "客先番号")
            DgvHtyhd.Columns.Add("仕入日", "仕入登録日")
            DgvHtyhd.Columns.Add("仕入先コード", "仕入先コード")
            DgvHtyhd.Columns.Add("仕入先名", "仕入先名")

            DgvHtyhd.Columns.Add("通貨_外貨", "仕入通貨")
            DgvHtyhd.Columns.Add("仕入原価_外貨", "仕入原価" & vbCrLf & "(原通貨)")
            DgvHtyhd.Columns.Add("仕入原価", "仕入原価" & vbCrLf & "(" & setBaseCurrency() & ")")
            DgvHtyhd.Columns.Add("仕入金額_外貨", "仕入金額" & vbCrLf & "(原通貨)")
            DgvHtyhd.Columns.Add("仕入金額", "仕入金額" & vbCrLf & "(" & setBaseCurrency() & ")")

            DgvHtyhd.Columns.Add("仕入先郵便番号", "仕入先郵便番号")
            DgvHtyhd.Columns.Add("仕入先住所", "仕入先住所")
            DgvHtyhd.Columns.Add("仕入先電話番号", "仕入先電話番号")
            DgvHtyhd.Columns.Add("仕入先ＦＡＸ", "仕入先ＦＡＸ")
            DgvHtyhd.Columns.Add("仕入先担当者名", "仕入先担当者名")
            DgvHtyhd.Columns.Add("仕入先担当者役職", "仕入先担当者役職")
            DgvHtyhd.Columns.Add("支払条件", "支払条件")
            DgvHtyhd.Columns.Add("営業担当者", "営業担当者")
            DgvHtyhd.Columns.Add("入力担当者", "入力担当者")
            DgvHtyhd.Columns.Add("備考", "備考")
            DgvHtyhd.Columns.Add("登録日", "登録日")

            DgvHtyhd.Columns.Add("最終更新日", "最終更新日")

        End If

        DgvHtyhd.Columns("仕入原価_外貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvHtyhd.Columns("仕入原価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvHtyhd.Columns("仕入金額_外貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvHtyhd.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    End Sub

    '使用言語に合わせて仕入明細見出しを切替
    Private Sub setDtColumns()

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            DgvHtyhd.Columns.Add("取消", "Cancel")
            DgvHtyhd.Columns.Add("発注番号", "PurchaseOrderNumber")
            DgvHtyhd.Columns.Add("発注番号枝番", "PurchaseOrderVer.")
            DgvHtyhd.Columns.Add("仕入番号", "PurchaseNumber")
            DgvHtyhd.Columns.Add("行番号", "LineNumber")
            DgvHtyhd.Columns.Add("仕入区分", "PurchasingClassification")
            DgvHtyhd.Columns.Add("メーカー", "Manufacturer")
            DgvHtyhd.Columns.Add("品名", "ItemName")
            DgvHtyhd.Columns.Add("型式", "Spec")
            DgvHtyhd.Columns.Add("仕入先名", "SupplierName")

            DgvHtyhd.Columns.Add("通貨_外貨", "Currency")
            DgvHtyhd.Columns.Add("仕入値_外貨", "PurchasePrice" & vbCrLf & "(OrignalCurrency)")
            DgvHtyhd.Columns.Add("仕入値", "PurchasePrice" & vbCrLf & "(" & setBaseCurrency() & ")")

            DgvHtyhd.Columns.Add("発注数量", "OrderQuantity")
            DgvHtyhd.Columns.Add("仕入数量", "PurchaseQuantity")
            DgvHtyhd.Columns.Add("発注残数", "PurchasedQuantity")
            DgvHtyhd.Columns.Add("単位", "Unit")

            DgvHtyhd.Columns.Add("仕入金額_外貨", "PurchaseCost" & vbCrLf & "(OrignalCurrency)")
            DgvHtyhd.Columns.Add("仕入金額", "PurchaseCost" & vbCrLf & "(" & setBaseCurrency() & ")")
            DgvHtyhd.Columns.Add("間接費", "OverHead")

            DgvHtyhd.Columns.Add("リードタイム", "LeadTime")
            DgvHtyhd.Columns.Add("備考", "Remarks")
            DgvHtyhd.Columns.Add("更新者", "ModifiedBy")

            DgvHtyhd.Columns.Add("登録日", "RegistrationDate")
            DgvHtyhd.Columns.Add("最終更新日", "LastUpdateDate")

        Else
            DgvHtyhd.Columns.Add("取消", "取消")
            DgvHtyhd.Columns.Add("発注番号", "発注番号")
            DgvHtyhd.Columns.Add("発注番号枝番", "発注Ver.")
            DgvHtyhd.Columns.Add("仕入番号", "仕入番号")
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
            DgvHtyhd.Columns.Add("仕入数量", "入庫済数量")
            DgvHtyhd.Columns.Add("発注残数", "発注残数量")
            DgvHtyhd.Columns.Add("単位", "単位")

            DgvHtyhd.Columns.Add("仕入金額_外貨", "仕入原価" & vbCrLf & "(原通貨)")
            DgvHtyhd.Columns.Add("仕入金額", "仕入原価" & vbCrLf & "(" & setBaseCurrency() & ")")
            DgvHtyhd.Columns.Add("間接費", "間接費")


            DgvHtyhd.Columns.Add("リードタイム", "リードタイム")
            DgvHtyhd.Columns.Add("備考", "備考")
            DgvHtyhd.Columns.Add("更新者", "更新者")

            DgvHtyhd.Columns.Add("登録日", "登録日")
            DgvHtyhd.Columns.Add("最終更新日", "最終更新日")
        End If

        DgvHtyhd.Columns("仕入値_外貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvHtyhd.Columns("仕入値").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvHtyhd.Columns("発注数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvHtyhd.Columns("仕入数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvHtyhd.Columns("発注残数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvHtyhd.Columns("仕入金額_外貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvHtyhd.Columns("仕入金額").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvHtyhd.Columns("間接費").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvHtyhd.Columns("リードタイム").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    End Sub

    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _status = CommonConst.STATUS_VIEW Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "ViewMode"
            Else
                LblMode.Text = "参照モード"
            End If

            BtnPurchaseView.Visible = True
            BtnPurchaseView.Location = New Point(997, 509)
        ElseIf _status = CommonConst.STATUS_CANCEL Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                LblMode.Text = "CancelMode"
            Else
                LblMode.Text = "取消モード"
            End If

            BtnPurchaseCancel.Visible = True
            BtnPurchaseCancel.Location = New Point(997, 509)
        End If

        '検索（Date）の初期値
        dtPurchaseDateSince.Value = DateAdd("d", CommonConst.SINCE_DEFAULT_DAY, DateTime.Today)
        dtPurchaseDateUntil.Value = DateTime.Today

        PurchaseListLoad() '一覧欄を表示

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            LblConditions.Text = "ExtractionCondition"
            Label1.Text = "SupplierName"
            Label2.Text = "Address"
            Label3.Text = "PhoneNumber"
            Label4.Text = "SupplierCode"
            Label8.Text = "PurchaseDate"
            Label7.Text = "PurchaseNumber"
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

            BtnPurchaseSearch.Text = "Search"
            BtnPurchaseCancel.Text = "CancelOfPurchase"
            BtnPurchaseView.Text = "PurchaseDataView"
            BtnBack.Text = "Back"

        End If
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    Private Sub RbtnDetails_CheckedChanged(sender As Object, e As EventArgs) Handles RbtnDetails.CheckedChanged
        PurchaseListLoad() '一覧欄を表示
    End Sub

    Private Sub BtnPurchaseSearch_Click(sender As Object, e As EventArgs) Handles BtnPurchaseSearch.Click
        PurchaseListLoad() '一覧欄を表示
    End Sub

    Private Sub BtnPurchaseView_Click(sender As Object, e As EventArgs) Handles BtnPurchaseView.Click

        'データがない場合はなにもしない
        If DgvHtyhd.Rows.Count = 0 Then
            Exit Sub
        End If

        Dim RowIdx As Integer
        RowIdx = Me.DgvHtyhd.CurrentCell.RowIndex
        Dim No As String = DgvHtyhd.Rows(RowIdx).Cells("発注番号").Value
        Dim Suffix As String = DgvHtyhd.Rows(RowIdx).Cells("発注番号枝番").Value
        Dim Status As String = CommonConst.STATUS_VIEW
        Dim openForm As Form = Nothing
        openForm = New PurchasingManagement(_msgHd, _db, _langHd, No, Suffix, Status)   '処理選択
        openForm.Show(Me)
    End Sub

    Private Sub ChkCancelData_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCancelData.CheckedChanged
        PurchaseListLoad() '一覧欄を表示
    End Sub

    '仕入取消
    Private Sub BtnPurchaseCancel_Click(sender As Object, e As EventArgs) Handles BtnPurchaseCancel.Click

        '明細表示時、または対象データがない場合は取消操作不可能
        If RbtnDetails.Checked Or DgvHtyhd.Rows.Count = 0 Then

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
        If DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("取消").Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_JPN, CommonConst.CANCEL_KBN_JPN_TXT, CommonConst.CANCEL_KBN_ENG_TXT) Then
            '取消データは選択できないアラートを出す
            _msgHd.dspMSG("cannotSelectTorikeshiData", frmC01F10_Login.loginValue.Language)
            Return
        End If


        Dim dtNow As String = UtilClass.formatDatetime(DateTime.Now)
        Dim reccnt As Integer = 0

        Dim Sql As String = ""

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
        Sql += DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("発注番号").Value
        Sql += "' "
        Sql += " AND"
        Sql += " 発注番号枝番"
        Sql += "='"
        Sql += DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("発注番号枝番").Value
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
        Sql2 += DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("仕入番号").Value
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
        Sql3 += DgvHtyhd.Rows(DgvHtyhd.CurrentCell.RowIndex).Cells("仕入番号").Value
        Sql3 += "' "

        '取消確認のアラート
        Dim result As DialogResult = _msgHd.dspMSG("confirmCancel", frmC01F10_Login.loginValue.Language)

        If result = DialogResult.Yes Then
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

            PurchaseListLoad()
        End If
    End Sub

    '抽出条件取得
    Private Function viewSearchConditions() As String
        Dim Sql As String = ""

        '抽出条件
        Dim customerName As String = UtilClass.escapeSql(TxtSupplierName.Text)
        Dim customerAddress As String = UtilClass.escapeSql(TxtAddress.Text)
        Dim customerTel As String = UtilClass.escapeSql(TxtTel.Text)
        Dim customerCode As String = UtilClass.escapeSql(TxtSupplierCode.Text)
        Dim sinceDate As String = UtilClass.strFormatDate(dtPurchaseDateSince.Text)
        Dim untilDate As String = UtilClass.strFormatDate(dtPurchaseDateUntil.Text)
        Dim sinceNum As String = UtilClass.escapeSql(TxtPurchaseSince.Text)
        Dim salesName As String = UtilClass.escapeSql(TxtSales.Text)
        Dim customerPO As String = UtilClass.escapeSql(TxtCustomerPO.Text)
        Dim itemName As String = UtilClass.escapeSql(TxtItemName.Text)
        Dim spec As String = UtilClass.escapeSql(TxtSpec.Text)

        If customerName <> Nothing Then
            Sql += " AND "
            Sql += " t40.仕入先名 ILIKE '%" & customerName & "%' "
        End If

        If customerAddress <> Nothing Then
            Sql += " AND "
            Sql += " t40.仕入先住所 ILIKE '%" & customerAddress & "%' "
        End If

        If customerTel <> Nothing Then
            Sql += " AND "
            Sql += " t40.仕入先電話番号 ILIKE '%" & customerTel & "%' "
        End If

        If customerCode <> Nothing Then
            Sql += " AND "
            Sql += " t40.仕入先コード ILIKE '%" & customerCode & "%' "
        End If

        If sinceDate <> Nothing Then
            Sql += " AND "
            Sql += " t40.仕入日 >= '" & sinceDate & "'"
        End If
        If untilDate <> Nothing Then
            Sql += " AND "
            Sql += " t40.仕入日 <= '" & untilDate & "'"
        End If

        If sinceNum <> Nothing Then
            Sql += " AND "
            Sql += " t40.仕入番号 ILIKE '%" & sinceNum & "%' "
        End If

        If salesName <> Nothing Then
            Sql += " AND "
            Sql += " t40.営業担当者 ILIKE '%" & salesName & "%' "
        End If

        If customerPO <> Nothing Then
            Sql += " AND "
            Sql += " t40.客先番号 ILIKE '%" & customerPO & "%' "
        End If

        If itemName <> Nothing Then
            Sql += " AND "
            Sql += " t41.品名 ILIKE '%" & itemName & "%' "
        End If

        If spec <> Nothing Then
            Sql += " AND "
            Sql += " t41.型式 ILIKE '%" & spec & "%' "
        End If

        If ChkCancelData.Checked = False Then
            Sql += " AND "
            Sql += " t40.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
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

        Sql += "SELECT * FROM public." & tableName
        Sql += " WHERE "
        Sql += "会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += txtParam
        Return _db.selectDB(Sql, RS, reccnt)
    End Function

    '汎用マスタから固定キー、可変キーに応じた結果を返す
    'param1：String 固定キー
    'param2：String 可変キー
    'Return: DataSet
    Private Function getDsHanyoData(ByVal prmFixed As String, ByVal prmVariable As String) As DataSet
        Dim Sql As String = ""

        Sql = " AND "
        Sql += "固定キー ILIKE '" & prmFixed & "'"
        Sql += " AND "
        Sql += "可変キー ILIKE '" & prmVariable & "'"

        'リードタイムのリストを汎用マスタから取得
        Return getDsData("m90_hanyo", Sql)

    End Function


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