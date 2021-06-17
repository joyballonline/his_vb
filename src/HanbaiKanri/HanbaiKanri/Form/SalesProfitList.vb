'2020.01.09 ロケ番号→出庫開始サインに名称変更

Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Imports System.Globalization
Imports System.Runtime.InteropServices
Imports System.IO

Public Class SalesProfitList
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
    'Private CompanyCode As String = ""
    Private SalesNo As String()
    Private SalesStatus As String = ""
    Private _com As CommonLogic
    Private _vs As String = "2"

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
        SalesStatus = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        _com = New CommonLogic(_db, _msgHd)
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text += _vs
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True
        'DgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells
    End Sub

    '画面表示時
    Private Sub SalesProfitList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            LblMode.Text = SalesStatus & " Mode"

            Label8.Text = "SalesDate"
            BtnExcelOutput.Text = "ExcelOutput"
            BtnBack.Text = "Back"
            LblMonth.Text = "From"
            LblYear.Text = "To"

            LblSalesAmount.Text = "AMOUNT(x)" '"OrderAmount"
            LblTotalSalesAmount.Text = "TotalSalesAmount"
            LblSalesCostAmount.Text = "AMOUNT(z)+OVERHEAD(f)" '"PurchaseAmount"
            LblGrossMargin.Text = "ProfitMargin(x-z-f)"
            LblGrossMarginRate.Text = "ProfitMarginRate"

        End If

        getList2() '一覧表示

        msetLine()  '行の設定

    End Sub

    '一覧取得
    Private Sub getList_del()

        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        DgvList.Rows.Clear() '一覧クリア


#Region "SQL"

        Sql = " SELECT "
        Sql += " t23.*, t31.*, t31.見積単価 as 売上単価_外貨, t31.見積金額 as 売上金額_外貨, t31.行番号 as 売上行番号 "
        Sql += " FROM t23_skyuhd t23, t31_urigdt t31, t30_urighd t30 "
        Sql += " WHERE "
        Sql += " t31.会社コード=t30.会社コード and t31.売上番号 = t30.売上番号 and t31.売上番号枝番 = t30.売上番号枝番 and "
        Sql += " t30.会社コード=t23.会社コード and t30.受注番号=t23.受注番号 and t30.受注番号枝番=t23.受注番号枝番 "
        Sql += " AND t23.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
        Sql += " AND t23.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND t30.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
        Sql += " AND "
        Sql += " t23.請求日 >= '" & UtilClass.strFormatDate(DateTimePicker1.Value) & "'"
        Sql += " AND "
        Sql += " t23.請求日 <= '" & UtilClass.strFormatDate(DateTimePicker2.Value) & "'"
        Sql += " ORDER BY t23.請求番号 "
#End Region


        Try

            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            Dim totalSales As Decimal = 0
            Dim totalSalesAmount As Decimal = 0
            Dim salesUnitPrice As Decimal = 0
            Dim totalArari As Decimal = 0
            Dim totalArariRate As Decimal = 0


            Dim intListCnt As Integer = 0  'データグリッドのカウント
            For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1

                DgvList.Rows.Add()
                DgvList.Rows(intListCnt).Cells("行番号").Value = intListCnt + 1
                DgvList.Rows(intListCnt).Cells("受注番号").Value = ds.Tables(RS).Rows(i)("受注番号")
                DgvList.Rows(intListCnt).Cells("受注番号枝番").Value = ds.Tables(RS).Rows(i)("受注番号枝番")
                DgvList.Rows(intListCnt).Cells("受注行番号").Value = ds.Tables(RS).Rows(i)("売上行番号")
                DgvList.Rows(intListCnt).Cells("請求番号").Value = ds.Tables(RS).Rows(i)("請求番号")
                Dim dtmTemp As DateTime = ds.Tables(RS).Rows(i)("請求日")
                DgvList.Rows(intListCnt).Cells("請求日").Value = dtmTemp.ToShortDateString
                DgvList.Rows(intListCnt).Cells("得意先コード").Value = ds.Tables(RS).Rows(i)("得意先コード")
                DgvList.Rows(intListCnt).Cells("得意先名").Value = ds.Tables(RS).Rows(i)("得意先名")
                DgvList.Rows(intListCnt).Cells("メーカー").Value = ds.Tables(RS).Rows(i)("メーカー")
                DgvList.Rows(intListCnt).Cells("品名").Value = ds.Tables(RS).Rows(i)("品名")
                DgvList.Rows(intListCnt).Cells("型式").Value = ds.Tables(RS).Rows(i)("型式")
                DgvList.Rows(intListCnt).Cells("販売通貨").Value = _com.getCurrencyEx(ds.Tables(RS).Rows(i)("通貨"))
                DgvList.Rows(intListCnt).Cells("受注単価_原通貨").Value = UtilClass.rmNullDecimal(ds.Tables(RS).Rows(i)("売上単価_外貨"))
                DgvList.Rows(intListCnt).Cells("受注単価_IDR").Value = UtilClass.rmNullDecimal(ds.Tables(RS).Rows(i)("見積単価")) '売単価"))
                DgvList.Rows(intListCnt).Cells("受注数量").Value = UtilClass.rmNullDecimal(ds.Tables(RS).Rows(i)("売上数量"))
                DgvList.Rows(intListCnt).Cells("単位").Value = ds.Tables(RS).Rows(i)("単位")
                DgvList.Rows(intListCnt).Cells("受注金額_原通貨").Value = UtilClass.rmNullDecimal(ds.Tables(RS).Rows(i)("売上金額_外貨"))
                DgvList.Rows(intListCnt).Cells("受注金額_IDR").Value = UtilClass.rmNullDecimal(ds.Tables(RS).Rows(i)("見積金額")) '売上金額"))

                totalSales += DgvList.Rows(intListCnt).Cells("受注金額_IDR").Value
                totalSalesAmount += UtilClass.rmNullDecimal(ds.Tables(RS).Rows(i)("請求金額計"))

                Dim fkpc_ As String = UtilClass.rmDBNull2StrNull(ds.Tables(RS).Rows(i)("仕入区分"))

                Dim dsHanyou As DataSet = _com.getDsHanyoData(CommonConst.FIXED_KEY_PURCHASING_CLASS, fkpc_)
                If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                    DgvList.Rows(intListCnt).Cells("仕入区分").Value = dsHanyou.Tables(RS).Rows(0)("文字２")
                Else
                    DgvList.Rows(intListCnt).Cells("仕入区分").Value = dsHanyou.Tables(RS).Rows(0)("文字１")
                End If

                If fkpc_ = CommonConst.Sire_KBN_Zaiko Then
                    Sql = "SELECT"
                    Sql += " t45.出庫番号, t45.行番号 "
                    Sql += " from t45_shukodt t45, t44_shukohd t44 "
                    Sql += " where t45.出庫番号 = t44.出庫番号"
                    Sql += " AND t45.受注番号 = '" & ds.Tables(RS).Rows(i)("受注番号") & "'"
                    Sql += " AND t45.受注番号枝番 = '" & ds.Tables(RS).Rows(i)("受注番号枝番") & "'"
                    Sql += " AND t45.行番号 = '" & ds.Tables(RS).Rows(i)("売上行番号") & "'"
                    Sql += " AND t44.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
                    Sql += " AND t45.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                    'Sql += " AND t45.出庫数量 = " & DgvList.Rows(intListCnt).Cells("受注数量").Value
                    Sql += " AND t45.出庫区分 <> '2'"

                    Dim ds3 As DataSet = _db.selectDB(Sql, RS, reccnt)

                    If reccnt = 0 Then
                        DgvList.Rows(intListCnt).Cells("発注番号").Value = "not yet delivery"
                    Else

                        For j As Integer = 0 To 0 'ds3.Tables(RS).Rows.Count - 1

                            Sql = "SELECT"
                            Sql += " t70.出庫開始サイン as ロケ番号 "
                            Sql += " from t70_inout t70 "
                            Sql += " where "
                            Sql += " t70.伝票番号 = '" & ds3.Tables(RS).Rows(j)("出庫番号") & "'"
                            Sql += " AND t70.行番号 = '" & ds3.Tables(RS).Rows(j)("行番号") & "'"
                            Sql += " AND t70.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
                            Sql += " AND t70.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

                            Dim ds4 As DataSet = _db.selectDB(Sql, RS, reccnt)

                            Sql = "SELECT"
                            Sql += " t42.発注番号, t42.発注番号枝番, t43.行番号 "
                            Sql += " from t43_nyukodt t43, t42_nyukohd t42 "
                            Sql += " where "
                            Sql += " t43.入庫番号 = '" & UtilClass.rmDBNull2StrNull(ds4.Tables(RS).Rows(0)("ロケ番号")).ToString.Substring(0, 10) & "'"
                            Sql += " and t43.行番号 = '" & UtilClass.rmDBNull2StrNull(ds4.Tables(RS).Rows(0)("ロケ番号")).ToString.Substring(10, 1) & "'"
                            Sql += " and t42.入庫番号 = t43.入庫番号 "
                            Sql += " And t42.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
                            Sql += " And t43.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

                            Dim ds5 As DataSet = _db.selectDB(Sql, RS, reccnt)
                            Dim po_ As String = "no goods receipt"
                            Dim v_ As String = ""
                            Dim l_ As String = ""
                            Dim cur_ As String = ""
                            Dim sc_ As String = ""
                            Dim sn_ As String = ""
                            Dim upf_ As Decimal = 0
                            Dim upi_ As Decimal = 0
                            Dim amtf_ As Decimal = 0
                            Dim amti_ As Decimal = 0
                            Dim oh_ As Decimal = 0

                            If ds5.Tables(RS).Rows.Count > 0 Then

                                Sql = "SELECT t21.*, t20.* "
                                Sql += " FROM t21_hattyu t21, t20_hattyu t20 "
                                Sql += " WHERE "
                                Sql += " t20.発注番号 = '" & ds5.Tables(RS).Rows(0)("発注番号") & "'"
                                Sql += " AND t20.発注番号枝番 = '" & ds5.Tables(RS).Rows(0)("発注番号枝番") & "'"
                                Sql += " AND t21.行番号 = '" & ds5.Tables(RS).Rows(0)("行番号") & "'"
                                Sql += " AND t21.発注番号 = t20.発注番号 and t21.発注番号枝番 = t20.発注番号枝番 "
                                Sql += " AND t20.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
                                Sql += " AND t21.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                                Sql += " Order by t21.行番号 desc "

                                Dim ds2 As DataSet = _db.selectDB(Sql, RS, reccnt)
                                Dim ii As Integer = 0

                                po_ = ds2.Tables(RS).Rows(ii)("発注番号")
                                v_ = ds2.Tables(RS).Rows(ii)("発注番号枝番")
                                l_ = ds2.Tables(RS).Rows(ii)("行番号")
                                cur_ = _com.getCurrencyEx(ds2.Tables(RS).Rows(ii)("仕入通貨"))
                                sc_ = ds2.Tables(RS).Rows(ii)("仕入先コード")
                                sn_ = ds2.Tables(RS).Rows(ii)("仕入先名")
                                upi_ = UtilClass.rmNullDecimal(ds2.Tables(RS).Rows(ii)("仕入値"))
                                upf_ = UtilClass.rmNullDecimal(ds2.Tables(RS).Rows(ii)("仕入値_外貨"))
                                amtf_ = DgvList.Rows(intListCnt).Cells("受注数量").Value * upf_ 'UtilClass.rmNullDecimal(ds2.Tables(RS).Rows(ii)("仕入金額_外貨"))
                                amti_ = DgvList.Rows(intListCnt).Cells("受注数量").Value * upi_ 'UtilClass.rmNullDecimal(ds2.Tables(RS).Rows(ii)("仕入金額"))
                                oh_ = UtilClass.rmNullDecimal(ds2.Tables(RS).Rows(ii)("間接費"))
                            End If

                            DgvList.Rows(intListCnt).Cells("発注番号").Value = po_
                            DgvList.Rows(intListCnt).Cells("発注番号枝番").Value = v_
                            DgvList.Rows(intListCnt).Cells("発注行番号").Value = l_
                            DgvList.Rows(intListCnt).Cells("仕入通貨").Value = cur_
                            DgvList.Rows(intListCnt).Cells("仕入先コード").Value = sc_
                            DgvList.Rows(intListCnt).Cells("仕入先名").Value = sn_
                            DgvList.Rows(intListCnt).Cells("仕入単価_IDR").Value = upi_
                            DgvList.Rows(intListCnt).Cells("仕入単価_原通貨").Value = upf_
                            DgvList.Rows(intListCnt).Cells("仕入原価_原通貨").Value += amtf_
                            DgvList.Rows(intListCnt).Cells("仕入原価_IDR").Value += amti_
                            DgvList.Rows(intListCnt).Cells("間接費").Value += oh_

                        Next

                    End If

                    DgvList.Rows(intListCnt).Cells("利益").Value = DgvList.Rows(intListCnt).Cells("受注金額_IDR").Value - DgvList.Rows(intListCnt).Cells("仕入原価_IDR").Value - DgvList.Rows(intListCnt).Cells("間接費").Value

                    If DgvList.Rows(intListCnt).Cells("受注金額_IDR").Value = 0 Then
                        DgvList.Rows(intListCnt).Cells("利益率").Value = 0
                    Else
                        DgvList.Rows(intListCnt).Cells("利益率").Value = DgvList.Rows(intListCnt).Cells("利益").Value / DgvList.Rows(intListCnt).Cells("受注金額_IDR").Value * 100
                    End If

                    salesUnitPrice += DgvList.Rows(intListCnt).Cells("仕入原価_IDR").Value + DgvList.Rows(intListCnt).Cells("間接費").Value

                ElseIf fkpc_ = CommonConst.Sire_KBN_Sire Then
                    Sql = "SELECT t21.*, t20.* "
                    Sql += " FROM t21_hattyu t21, t20_hattyu t20 "
                    Sql += " WHERE "
                    Sql += " t20.受注番号 = '" & ds.Tables(RS).Rows(i)("受注番号") & "'"
                    'Sql += " AND t20.受注番号枝番 = '" & ds.Tables(RS).Rows(i)("受注番号枝番") & "'"
                    Sql += " AND t21.行番号 = '" & ds.Tables(RS).Rows(i)("売上行番号") & "'"
                    Sql += " AND t21.発注番号 = t20.発注番号 and t21.発注番号枝番 = t20.発注番号枝番 "
                    Sql += " AND t20.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
                    Sql += " AND t21.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                    Sql += " Order by t21.発注番号枝番 desc "

                    Dim ds2 As DataSet = _db.selectDB(Sql, RS, reccnt)
                    Dim ii As Integer = 0
                    If reccnt = 0 Then
                    Else

                        DgvList.Rows(intListCnt).Cells("発注番号").Value = ds2.Tables(RS).Rows(ii)("発注番号")
                        DgvList.Rows(intListCnt).Cells("発注番号枝番").Value = ds2.Tables(RS).Rows(ii)("発注番号枝番")
                        DgvList.Rows(intListCnt).Cells("発注行番号").Value = ds2.Tables(RS).Rows(ii)("行番号")
                        DgvList.Rows(intListCnt).Cells("仕入通貨").Value = _com.getCurrencyEx(ds2.Tables(RS).Rows(ii)("仕入通貨"))
                        DgvList.Rows(intListCnt).Cells("仕入先コード").Value = ds2.Tables(RS).Rows(ii)("仕入先コード")
                        DgvList.Rows(intListCnt).Cells("仕入先名").Value = ds2.Tables(RS).Rows(ii)("仕入先名")
                        DgvList.Rows(intListCnt).Cells("仕入単価_IDR").Value = UtilClass.rmNullDecimal(ds2.Tables(RS).Rows(ii)("仕入値"))
                        DgvList.Rows(intListCnt).Cells("仕入単価_原通貨").Value = UtilClass.rmNullDecimal(ds2.Tables(RS).Rows(ii)("仕入値_外貨"))
                        DgvList.Rows(intListCnt).Cells("仕入原価_原通貨").Value = UtilClass.rmNullDecimal(ds2.Tables(RS).Rows(ii)("仕入金額_外貨"))
                        DgvList.Rows(intListCnt).Cells("仕入原価_IDR").Value = UtilClass.rmNullDecimal(ds2.Tables(RS).Rows(ii)("仕入金額"))
                        DgvList.Rows(intListCnt).Cells("間接費").Value = UtilClass.rmNullDecimal(ds2.Tables(RS).Rows(ii)("間接費"))

                        DgvList.Rows(intListCnt).Cells("利益").Value = DgvList.Rows(intListCnt).Cells("受注金額_IDR").Value - DgvList.Rows(intListCnt).Cells("仕入原価_IDR").Value - DgvList.Rows(intListCnt).Cells("間接費").Value

                        If DgvList.Rows(intListCnt).Cells("受注金額_IDR").Value = 0 Then
                            DgvList.Rows(intListCnt).Cells("利益率").Value = 0
                        Else
                            DgvList.Rows(intListCnt).Cells("利益率").Value = DgvList.Rows(intListCnt).Cells("利益").Value / DgvList.Rows(intListCnt).Cells("受注金額_IDR").Value * 100
                        End If

                        salesUnitPrice += DgvList.Rows(intListCnt).Cells("仕入原価_IDR").Value + DgvList.Rows(intListCnt).Cells("間接費").Value
                    End If
                Else

                End If

                intListCnt += 1  'データグリッドのカウントをアップ

            Next

#Region "フッダー"

            totalArari = totalSales - salesUnitPrice

            If totalArari > 0 And salesUnitPrice <> 0 Then
                totalArariRate = (totalArari / totalSales) * 100
            Else
                totalArariRate = 0
            End If

            '売上計
            TxtSalesAmount.Text = IIf(
                totalSales <> 0,
                Format(totalSales, "#,##0.00"),
                0
            )
            '売上 + VAT
            TxtTotalSalesAmount.Text = IIf(
                totalSalesAmount <> 0,
                Format(totalSalesAmount, "#,##0.00"),
                0
            )
            '売上原価
            TxtSalesCostAmount.Text = IIf(
                salesUnitPrice <> 0,
                Format(salesUnitPrice, "#,##0.00"),
                0
            )
            '粗利額
            TxtGrossMargin.Text = IIf(
                totalArari <> 0,
                Format(totalArari, "#,##0.00"),
                0
            )
            '粗利率
            TxtGrossMarginRate.Text = IIf(
                totalArariRate <> 0,
                Format(totalArariRate, "0.0"),
                0
            )

#End Region


        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub


    Private Function mGetHatyuNo3(ByVal intCnt As Integer, ByRef ds As DataSet, ByRef dsHattyu As DataSet) As Boolean

        Dim reccnt As Integer = 0


        Dim strJyutyu As String = ds.Tables(RS).Rows(intCnt)("受注番号")
        Dim strEda As String = ds.Tables(RS).Rows(intCnt)("受注番号枝番")
        Dim strMaker As String = UtilClass.escapeSql(ds.Tables(RS).Rows(intCnt)("メーカー"))
        Dim strHIn As String = UtilClass.escapeSql(ds.Tables(RS).Rows(intCnt)("品名"))
        Dim strKata As String = UtilClass.escapeSql(ds.Tables(RS).Rows(intCnt)("型式"))


        Dim Sql As String = " SELECT "

        Sql += " t43.発注番号 as 発注番号2, t43.発注番号枝番 as 発注番号枝番2, t21.行番号 as 発注行番号2"
        Sql += ",t45.出庫数量 as 入庫数量,t21.仕入値,t21.仕入単価_外貨,t20.仕入先コード,t20.仕入先名 "


        't45_shukodt t44_shukohd
        Sql += " from t45_shukodt t45 "
        Sql += " left join t44_shukohd t44 on t45.出庫番号 = t44.出庫番号"

        't70_inout
        Sql += " left join t70_inout t70"
        Sql += "  on t45.出庫番号 = t70.伝票番号"
        Sql += " and t45.行番号 = t70.行番号"

        't43_nyukodt
        Sql += " left join t43_nyukodt t43"
        Sql += "  on left(t70.ロケ番号,10) = t43.入庫番号"
        Sql += " and right(t70.ロケ番号,1) = CAST(t43.行番号 AS VARCHAR(1))"

        't21_hattyu
        Sql += " left join t21_hattyu t21 "
        Sql += "  on t43.発注番号 = t21.発注番号"
        Sql += " and t43.発注番号枝番 = t21.発注番号枝番"
        Sql += " and t43.メーカー = t21.メーカー"
        Sql += " and t43.品名 = t21.品名"
        Sql += " and t43.型式 = t21.型式"

        't20_hattyu
        Sql += " left join t20_hattyu t20 "
        Sql += "  on t43.発注番号 = t20.発注番号"
        Sql += " and t43.発注番号枝番 = t20.発注番号枝番"

        Sql += " WHERE"
        Sql += " t45.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " and t44.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        Sql += " AND t45.受注番号 = '" & strJyutyu & "'"
        Sql += " AND t45.受注番号枝番 = '" & strEda & "'"
        Sql += " AND t45.行番号 = '" & ds.Tables(RS).Rows(intCnt)("行番号") & "'"

        Sql += " AND t45.メーカー = '" & strMaker & "'"
        Sql += " AND t45.品名 = '" & strHIn & "'"
        Sql += " AND t45.型式 = '" & strKata & "'"

        Sql += " ORDER BY 発注番号2,発注番号枝番2,発注行番号2 "


        dsHattyu = _db.selectDB(Sql, RS, reccnt)  '戻り値

        mGetHatyuNo3 = True

    End Function


    Private Sub msetLine()


#Region "行タイトル"

        DgvList.Columns("受注番号").HeaderText = _langHd.getLANG("受注番号", frmC01F10_Login.loginValue.Language) '"JobOrderNo" 
        DgvList.Columns("受注番号枝番").HeaderText = _langHd.getLANG("受注番号枝番", frmC01F10_Login.loginValue.Language) '"JobOrderVer"
        DgvList.Columns("受注行番号").HeaderText = _langHd.getLANG("行番号", frmC01F10_Login.loginValue.Language) '"LineNo"
        DgvList.Columns("発注番号枝番").HeaderText = _langHd.getLANG("発注番号枝番", frmC01F10_Login.loginValue.Language) '"PurchaseOrderVer"

        '言語の判定
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then  '英語 

            DgvList.Columns("行番号").HeaderText = "LineNo"
            'DgvList.Columns("売上番号").HeaderText = "SalesNumber"
            'DgvList.Columns("売上番号枝番").HeaderText = "SalesVer"
            DgvList.Columns("請求番号").HeaderText = "INVOICE NO. ZMEI" 'A B
            DgvList.Columns("請求日").HeaderText = "DATE" 'B C
            DgvList.Columns("得意先コード").HeaderText = "DATE" 'M
            DgvList.Columns("得意先名").HeaderText = "CUSTOMER" 'H
            DgvList.Columns("メーカー").HeaderText = "Maker"
            DgvList.Columns("品名").HeaderText = "ItemName"
            DgvList.Columns("型式").HeaderText = "Model"
            DgvList.Columns("販売通貨").HeaderText = "CURRENCY" 'D'
            DgvList.Columns("受注単価_原通貨").HeaderText = "AMOUNT" & vbCrLf & "x" 'E
            DgvList.Columns("受注単価_IDR").HeaderText = "OrderUnitPrice(IDR)" & vbCrLf & "a"
            DgvList.Columns("受注数量").HeaderText = "QTY" & vbCrLf & "b" 'C D
            DgvList.Columns("単位").HeaderText = "CUSTOMER PO NO." 'I
            DgvList.Columns("受注金額_原通貨").HeaderText = "TOTAL AMOUNT" & vbCrLf & "y=x+c" 'G
            DgvList.Columns("受注金額_IDR").HeaderText = "VAT 10%" & vbCrLf & "c" 'F
            DgvList.Columns("発注番号").HeaderText = "PO NO. ZMEI" 'J
            DgvList.Columns("発注行番号").HeaderText = "LINENO" 'N
            'DgvList.Columns("仕入番号").HeaderText = "" 'R 仕入原価_IDR
            'DgvList.Columns("仕入行番号").HeaderText = ""  '仕入単価_IDR
            DgvList.Columns("仕入区分").HeaderText = "PURCLASS" 'O
            DgvList.Columns("仕入先コード").HeaderText = "INVOICE NUMBER" 'L
            DgvList.Columns("仕入先名").HeaderText = "SUPPLIER" 'K
            DgvList.Columns("仕入通貨").HeaderText = "CURRENCY" 'P
            DgvList.Columns("仕入単価_原通貨").HeaderText = "AMOUNT" & vbCrLf & "z=d*(PurchaseUnitPrice)" 'Q O
            DgvList.Columns("仕入単価_IDR").HeaderText = "QTY" & vbCrLf & "d" 'N
            DgvList.Columns("仕入原価_原通貨").HeaderText = "PurchaseCost" & vbCrLf & "(OrignalCurrency)"
            DgvList.Columns("仕入原価_IDR").HeaderText = "REMARKS" 'R
            DgvList.Columns("間接費").HeaderText = "OVERHEAD" & vbCrLf & "f"
            DgvList.Columns("利益").HeaderText = "PROFIT" & vbCrLf & "g=x-z-f" 'R Q
            DgvList.Columns("利益率").HeaderText = "Balance" & vbCrLf & "e=b-d" 'P

        Else  ' 日本語

            DgvList.Columns("受注単価_原通貨").HeaderText = "受注単価" & vbCrLf & "(原通貨)"
            DgvList.Columns("受注単価_IDR").HeaderText = "受注単価(IDR)" & vbCrLf & "a"
            DgvList.Columns("受注数量").HeaderText = "受注数量" & vbCrLf & "b"
            DgvList.Columns("受注金額_原通貨").HeaderText = "受注金額" & vbCrLf & "(原通貨)"
            DgvList.Columns("受注金額_IDR").HeaderText = "受注金額(IDR)" & vbCrLf & "c=a*b"

            DgvList.Columns("仕入単価_原通貨").HeaderText = "仕入単価" & vbCrLf & "(原通貨)"
            DgvList.Columns("仕入単価_IDR").HeaderText = "仕入単価(IDR)" & vbCrLf & "d"
            DgvList.Columns("仕入原価_原通貨").HeaderText = "仕入原価" & vbCrLf & "(原通貨)"
            DgvList.Columns("仕入原価_IDR").HeaderText = "仕入原価(IDR)" & vbCrLf & "e=b*d"
            DgvList.Columns("間接費").HeaderText = "間接費" & vbCrLf & "f"
            DgvList.Columns("利益").HeaderText = "利益" & vbCrLf & "g=c-e-f"
            DgvList.Columns("利益率").HeaderText = "利益率" & vbCrLf & "h=g/c"


        End If

#End Region


#Region "形式"

        DgvList.Columns("販売通貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvList.Columns("仕入通貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


        '右寄せ
        DgvList.Columns("受注単価_原通貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("受注単価_IDR").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("受注数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DgvList.Columns("受注金額_原通貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("受注金額_IDR").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DgvList.Columns("仕入単価_原通貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("仕入単価_IDR").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DgvList.Columns("仕入原価_原通貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvList.Columns("仕入原価_IDR").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DgvList.Columns("間接費").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("利益").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("利益率").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight



        '数字形式
        DgvList.Columns("受注単価_原通貨").DefaultCellStyle.Format = "N2"
        DgvList.Columns("受注単価_IDR").DefaultCellStyle.Format = "N2"
        DgvList.Columns("受注数量").DefaultCellStyle.Format = "N2"
        DgvList.Columns("受注金額_原通貨").DefaultCellStyle.Format = "N2"
        DgvList.Columns("受注金額_IDR").DefaultCellStyle.Format = "N2"

        DgvList.Columns("仕入単価_原通貨").DefaultCellStyle.Format = "N2"
        DgvList.Columns("仕入単価_IDR").DefaultCellStyle.Format = "N2"
        DgvList.Columns("仕入原価_原通貨").DefaultCellStyle.Format = "N2"
        DgvList.Columns("仕入原価_IDR").DefaultCellStyle.Format = "d"
        DgvList.Columns("間接費").DefaultCellStyle.Format = "N2"

        DgvList.Columns("利益").DefaultCellStyle.Format = "N2"
        DgvList.Columns("利益率").DefaultCellStyle.Format = "N2"

        'DgvList.Columns("請求日").DefaultCellStyle.Format = "d"
        'DgvList.Columns("得意先コード").DefaultCellStyle.Format = "d"
        'DgvList.Columns("仕入番号").DefaultCellStyle.Format = "d"


        'タイトル 中央寄せ
        'DgvList.Columns("受注単価_IDR").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        'DgvList.Columns("受注数量").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        'DgvList.Columns("受注金額_IDR").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        'DgvList.Columns("仕入単価_IDR").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        'DgvList.Columns("仕入原価_IDR").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        'DgvList.Columns("間接費").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        'DgvList.Columns("利益").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        'DgvList.Columns("利益率").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        'DgvList.Columns("仕入行番号").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter


#End Region


    End Sub


    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    'Excel出力ボタン押下時
    Private Sub BtnExcelOutput_Click(sender As Object, e As EventArgs) Handles BtnExcelOutput.Click
        '対象データがない場合は取消操作不可能
        If DgvList.Rows.Count = 0 Then

            '該当データがないアラートを出す
            _msgHd.dspMSG("noTargetData", frmC01F10_Login.loginValue.Language)

            Return

        Else

            'Excel出力処理
            outputExcel3()
        End If
    End Sub

    'excel出力処理
    Private Sub outputExcel_del()

        '定義
        Dim app As Excel.Application = Nothing
        Dim book As Excel.Workbook = Nothing
        Dim sheet As Excel.Worksheet = Nothing

        'Dim strSelectYear As String = cmbYear.SelectedValue.ToString()
        'Dim strSelectMonth As String = cmbMonth.SelectedValue.ToString()

        ' セル
        Dim xlRngTmp As Range = Nothing
        Dim xlRng As Range = Nothing

        ' セル境界線（枠）
        Dim xlBorders As Borders = Nothing
        Dim xlBorder As Border = Nothing

        'カーソルをビジー状態にする
        Cursor.Current = Cursors.WaitCursor

        Try
            '雛形パス
            Dim sHinaPath As String = StartUp._iniVal.BaseXlsPath
            '雛形ファイル名
            Dim sHinaFile As String = sHinaPath & "\" & "SalesProfitList.xlsx"
            '出力先パス
            Dim sOutPath As String = StartUp._iniVal.OutXlsPath
            '出力ファイル名
            Dim sOutFile As String = sOutPath & "\SalesProfitList_" & DateTime.Now.ToString("yyyyMMddHHmm") & ".xlsx"

            app = New Excel.Application()
            app.ScreenUpdating = False
            app.EnableEvents = False
            app.Visible = False
            app.DisplayAlerts = False

            book = app.Workbooks.Add(sHinaFile)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet)
            app.Calculation = Microsoft.Office.Interop.Excel.XlCalculation.xlCalculationManual

            sheet.PageSetup.LeftHeader = "売上・売上原価・利益・利益率一覧表"
            sheet.PageSetup.CenterHeader = DateTimePicker1.Value.ToShortDateString & "-" & DateTimePicker2.Value.ToShortDateString
            sheet.PageSetup.RightHeader = "OutputDate：" & DateTime.Now.ToShortDateString


#Region "タイトル"
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                sheet.PageSetup.LeftHeader = "SalesProfitList"
                sheet.PageSetup.CenterHeader = DateTimePicker1.Value.ToShortDateString & "-" & DateTimePicker2.Value.ToShortDateString

                sheet.Range("A1").Value = "LineNo"

                sheet.Range("B1").Value = "JobOrderNo"
                sheet.Range("C1").Value = "JobOrderVer"
                sheet.Range("D1").Value = "LineNo"

                'sheet.Range("D1").Value = "SalesNo"
                'sheet.Range("E1").Value = "SalesVer"

                sheet.Range("E1").Value = "SalesInvoiceNo"
                sheet.Range("F1").Value = "SalesInvoiceDate"

                sheet.Range("G1").Value = "CustomerNumber"
                sheet.Range("H1").Value = "CustomerName"

                sheet.Range("I1").Value = "Maker"
                sheet.Range("J1").Value = "Product"
                sheet.Range("K1").Value = "Model"

                sheet.Range("L1").Value = "SalesCurrency"
                sheet.Range("M1").Value = "OrderPrice(OriginalCurrency)"
                sheet.Range("N1").Value = "OrderPrice(IDR)"
                sheet.Range("O1").Value = "OrderQuantity"
                sheet.Range("P1").Value = "Unit"
                sheet.Range("Q1").Value = "OrderAmount(OriginalCurrency)"
                sheet.Range("R1").Value = "OrderAmount(IDR)"

                sheet.Range("S1").Value = "OrderNo"
                sheet.Range("T1").Value = "OrderVer"
                sheet.Range("U1").Value = "LineNo"
                'sheet.Range("U1").Value = "PurchaseNo"
                'sheet.Range("V1").Value = "LineNo"
                sheet.Range("V1").Value = "PurchaseCategory"
                sheet.Range("W1").Value = "VendorCode"
                sheet.Range("X1").Value = "VendorName"

                sheet.Range("Y1").Value = "PurchaseCurrency"
                sheet.Range("Z1").Value = "PurchasePrice(OriginalCurrency)"
                sheet.Range("AA1").Value = "PurchasePrice(IDR)"
                sheet.Range("AB1").Value = "PurchasingCost(OriginalCurrency)"
                sheet.Range("AC1").Value = "PurchasingCost(IDR)"
                sheet.Range("AD1").Value = "Overhead"
                sheet.Range("AE1").Value = "Profit"
                sheet.Range("AF1").Value = "ProfitRate(%)"



            End If
#End Region

            Dim rowCnt As Integer = 0
            'Dim lstRow As Integer = 2
            'Dim addRowCnt As Integer = 0
            'Dim currentCnt As Integer = 19
            'Dim num As Integer = 1

            rowCnt = DgvList.Rows.Count - 1
            Dim array2(DgvList.RowCount - 1, 32) As String

            Dim cellRowIndex As Integer = 1

            For i As Integer = 0 To DgvList.RowCount - 1
                'cellRowIndex += 1
                'sheet.Rows(cellRowIndex).Insert

                array2(i, 0) = DgvList.Rows(i).Cells("行番号").Value '行番号
                array2(i, 1) = DgvList.Rows(i).Cells("受注番号").Value '受注番号
                array2(i, 2) = DgvList.Rows(i).Cells("受注番号枝番").Value '受注番号枝番
                array2(i, 3) = DgvList.Rows(i).Cells("受注行番号").Value '行番号
                'sheet.Range("D" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("売上番号").Value '売上番号
                'sheet.Range("E" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("売上番号枝番").Value '売上番号枝番
                array2(i, 4) = DgvList.Rows(i).Cells("請求番号").Value '請求番号
                array2(i, 5) = DgvList.Rows(i).Cells("請求日").Value '請求日

                array2(i, 6) = DgvList.Rows(i).Cells("得意先コード").Value '得意先コード
                array2(i, 7) = DgvList.Rows(i).Cells("得意先名").Value '得意先名

                array2(i, 8) = DgvList.Rows(i).Cells("メーカー").Value 'メーカー
                array2(i, 9) = DgvList.Rows(i).Cells("品名").Value '品名
                array2(i, 10) = DgvList.Rows(i).Cells("型式").Value '型式

                array2(i, 11) = DgvList.Rows(i).Cells("販売通貨").Value '販売通貨
                array2(i, 12) = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("受注単価_原通貨").Value).ToString("N2") '受注単価_原通貨
                array2(i, 13) = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("受注単価_IDR").Value).ToString("N2") '受注単価_IDR
                array2(i, 14) = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("受注数量").Value).ToString("N2") '受注数量
                array2(i, 15) = DgvList.Rows(i).Cells("単位").Value '単位

                array2(i, 16) = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("受注金額_原通貨").Value).ToString("N2") '受注金額_原通貨
                array2(i, 17) = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("受注金額_IDR").Value).ToString("N2") '受注金額_IDR

                array2(i, 18) = DgvList.Rows(i).Cells("発注番号").Value '発注番号
                array2(i, 19) = DgvList.Rows(i).Cells("発注番号枝番").Value '発注番号枝番
                array2(i, 20) = DgvList.Rows(i).Cells("発注行番号").Value '発注行番号

                'sheet.Range("U" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("仕入番号").Value '仕入番号
                'sheet.Range("V" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("仕入行番号").Value '仕入行番号
                array2(i, 21) = DgvList.Rows(i).Cells("仕入区分").Value '仕入区分

                array2(i, 22) = DgvList.Rows(i).Cells("仕入先コード").Value '仕入先コード
                array2(i, 23) = DgvList.Rows(i).Cells("仕入先名").Value '仕入先名

                array2(i, 24) = DgvList.Rows(i).Cells("仕入通貨").Value '仕入通貨
                array2(i, 25) = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("仕入単価_原通貨").Value).ToString("N2") '仕入単価_原通貨
                array2(i, 26) = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("仕入単価_IDR").Value).ToString("N2") '仕入単価_IDR
                array2(i, 27) = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("仕入原価_原通貨").Value).ToString("N2") '仕入原価_原通貨
                array2(i, 28) = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("仕入原価_IDR").Value).ToString("N2") '仕入原価_IDR

                array2(i, 29) = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("間接費").Value).ToString("N2") '間接費
                array2(i, 30) = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("利益").Value).ToString("N2") '利益
                array2(i, 31) = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("利益率").Value).ToString("N1") '利益率

                'sheet.Range("L" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
                'sheet.Range("M" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
                'sheet.Range("N" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
                'sheet.Range("P" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
                'sheet.Range("Q" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight

            Next

            sheet.Range("A2:AF" & (DgvList.RowCount + 1).ToString).Value = array2
            'sheet.Columns("A:AF").EntireColumn.AutoFit  '幅の自動調整
            cellRowIndex = rowCnt + 1 + 1

            ' 行7全体のオブジェクトを作成
            xlRngTmp = sheet.Rows
            xlRng = xlRngTmp(cellRowIndex)

            '最後に合計行の追加
            cellRowIndex += 2

            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then  '英語
                sheet.Range("D" & cellRowIndex.ToString).Value = "OrderAmountTotal"
                sheet.Range("D" & cellRowIndex.ToString + 3).Value = "PurchaseCostMeter"
                sheet.Range("F" & cellRowIndex.ToString).Value = "ProfitMargin"
                sheet.Range("F" & cellRowIndex.ToString + 3).Value = "ProfitRate(%)"

            Else  '日本語
                sheet.Range("D" & cellRowIndex.ToString).Value = "受注金額計"
                sheet.Range("D" & cellRowIndex.ToString + 3).Value = "仕入原価計"
                sheet.Range("F" & cellRowIndex.ToString).Value = "利益"
                sheet.Range("F" & cellRowIndex.ToString + 3).Value = "利益率(%)"
            End If

            sheet.Range("D" & cellRowIndex.ToString + 1).Value = CDec(TxtSalesAmount.Text).ToString("0.00")
            sheet.Range("D" & cellRowIndex.ToString + 1).NumberFormatLocal = "#,##0.00"
            sheet.Range("D" & cellRowIndex.ToString + 4).Value = CDec(TxtSalesCostAmount.Text).ToString("0.00")
            sheet.Range("D" & cellRowIndex.ToString + 4).NumberFormatLocal = "#,##0.00"

            sheet.Range("F" & cellRowIndex.ToString + 1).Value = CDec(TxtGrossMargin.Text).ToString("0.00")
            sheet.Range("F" & cellRowIndex.ToString + 1).NumberFormatLocal = "#,##0.00"
            sheet.Range("F" & cellRowIndex.ToString + 4).Value = CDec(TxtGrossMarginRate.Text).ToString("0.0")
            sheet.Range("F" & cellRowIndex.ToString + 4).NumberFormatLocal = "0.0"


            ' 境界線オブジェクトを作成 →7行目の下部に罫線を描画する
            xlBorders = xlRngTmp.Borders
            xlBorder = xlBorders(XlBordersIndex.xlEdgeBottom)
            xlBorder.LineStyle = XlLineStyle.xlContinuous

            Dim excelChk As Boolean = _com.excelOutput(sOutFile)
            If excelChk = False Then
                Exit Sub
            End If
            book.SaveAs(sOutFile) '書き込み実行

            app.Calculation = Microsoft.Office.Interop.Excel.XlCalculation.xlCalculationAutomatic
            app.DisplayAlerts = True 'アラート無効化を解除
            app.EnableEvents = True
            app.ScreenUpdating = True
            app.Visible = True

            'カーソルをビジー状態から元に戻す
            Cursor.Current = Cursors.Default

            _msgHd.dspMSG("CreateExcel", frmC01F10_Login.loginValue.Language)

        Catch ex As Exception
            'Throw ex
            'カーソルをビジー状態から元に戻す
            Cursor.Current = Cursors.Default
            UtilMsgHandler.VbMessageboxShow(ex.Message, ex.StackTrace.ToString, CommonConst.AP_NAME, ex.HResult)
        Finally
            'app.Quit()
            'Marshal.ReleaseComObject(sheet)
            'Marshal.ReleaseComObject(book)
            'Marshal.ReleaseComObject(app)

        End Try


    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        getList2()
    End Sub

    'excel出力処理
    Private Sub outputExcel_del2()

        '定義
        Dim app As Excel.Application = Nothing
        Dim book As Excel.Workbook = Nothing
        Dim sheet As Excel.Worksheet = Nothing

        'Dim strSelectYear As String = cmbYear.SelectedValue.ToString()
        'Dim strSelectMonth As String = cmbMonth.SelectedValue.ToString()

        ' セル
        Dim xlRngTmp As Range = Nothing
        Dim xlRng As Range = Nothing

        ' セル境界線（枠）
        Dim xlBorders As Borders = Nothing
        Dim xlBorder As Border = Nothing

        'カーソルをビジー状態にする
        Cursor.Current = Cursors.WaitCursor

        Try
            '雛形パス
            Dim sHinaPath As String = StartUp._iniVal.BaseXlsPath
            '雛形ファイル名
            Dim sHinaFile As String = sHinaPath & "\" & "SalesProfitList.xlsx"
            '出力先パス
            Dim sOutPath As String = StartUp._iniVal.OutXlsPath
            '出力ファイル名
            Dim sOutFile As String = sOutPath & "\SalesProfitList_" & DateTime.Now.ToString("yyyyMMddHHmm") & ".xlsx"

            app = New Excel.Application()
            app.ScreenUpdating = False
            app.EnableEvents = False
            app.Visible = False
            app.DisplayAlerts = False

            book = app.Workbooks.Add(sHinaFile)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet)
            app.Calculation = Microsoft.Office.Interop.Excel.XlCalculation.xlCalculationManual

            sheet.PageSetup.LeftHeader = "売上・売上原価・利益・利益率一覧表"
            sheet.PageSetup.CenterHeader = DateTimePicker1.Value.ToShortDateString & "-" & DateTimePicker2.Value.ToShortDateString
            sheet.PageSetup.RightHeader = "OutputDate：" & DateTime.Now.ToShortDateString


#Region "タイトル"
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                sheet.PageSetup.LeftHeader = "SalesProfitList"
                sheet.PageSetup.CenterHeader = DateTimePicker1.Value.ToShortDateString & "-" & DateTimePicker2.Value.ToShortDateString

                sheet.Range("A1").Value = "LineNo"

                sheet.Range("B1").Value = "JobOrderNo"
                sheet.Range("C1").Value = "JobOrderVer"
                sheet.Range("D1").Value = "LineNo"

                'sheet.Range("D1").Value = "SalesNo"
                'sheet.Range("E1").Value = "SalesVer"

                sheet.Range("E1").Value = "SalesInvoiceNo"
                sheet.Range("F1").Value = "SalesInvoiceDate"

                sheet.Range("G1").Value = "CustomerNumber"
                sheet.Range("H1").Value = "CustomerName"

                sheet.Range("I1").Value = "Maker"
                sheet.Range("J1").Value = "Product"
                sheet.Range("K1").Value = "Model"

                sheet.Range("L1").Value = "SalesCurrency"
                sheet.Range("M1").Value = "OrderPrice(OriginalCurrency)"
                sheet.Range("N1").Value = "OrderPrice(IDR)"
                sheet.Range("O1").Value = "OrderQuantity"
                sheet.Range("P1").Value = "Unit"
                sheet.Range("Q1").Value = "OrderAmount(OriginalCurrency)"
                sheet.Range("R1").Value = "OrderAmount(IDR)"

                sheet.Range("S1").Value = "OrderNo"
                sheet.Range("T1").Value = "OrderVer"
                sheet.Range("U1").Value = "LineNo"
                'sheet.Range("U1").Value = "PurchaseNo"
                'sheet.Range("V1").Value = "LineNo"
                sheet.Range("V1").Value = "PurchaseCategory"
                sheet.Range("W1").Value = "VendorCode"
                sheet.Range("X1").Value = "VendorName"

                sheet.Range("Y1").Value = "PurchaseCurrency"
                sheet.Range("Z1").Value = "PurchasePrice(OriginalCurrency)"
                sheet.Range("AA1").Value = "PurchasePrice(IDR)"
                sheet.Range("AB1").Value = "PurchasingCost(OriginalCurrency)"
                sheet.Range("AC1").Value = "PurchasingCost(IDR)"
                sheet.Range("AD1").Value = "Overhead"
                sheet.Range("AE1").Value = "Profit"
                sheet.Range("AF1").Value = "ProfitRate(%)"



            End If
#End Region

            Dim rowCnt As Integer = 0
            Dim lstRow As Integer = 2
            Dim addRowCnt As Integer = 0
            Dim currentCnt As Integer = 19
            Dim num As Integer = 1

            rowCnt = DgvList.Rows.Count - 1

            Dim cellPos As String = lstRow & ":" & lstRow

            If rowCnt > 1 Then
                For addRow As Integer = 0 To rowCnt
                    Dim R As Object
                    cellPos = lstRow & ":" & lstRow
                    R = sheet.Range(cellPos)
                    R.Copy()
                    R.Insert()
                    If Marshal.IsComObject(R) Then
                        Marshal.ReleaseComObject(R)
                    End If

                    lstRow = lstRow + 1
                Next
            End If


            Dim cellRowIndex As Integer = 1
            For i As Integer = 0 To DgvList.RowCount - 1
                cellRowIndex += 1
                'sheet.Rows(cellRowIndex).Insert

                sheet.Range("A" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("行番号").Value '行番号


                sheet.Range("B" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("受注番号").Value '受注番号
                sheet.Range("C" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("受注番号枝番").Value '受注番号枝番
                sheet.Range("D" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("受注行番号").Value '行番号

                'sheet.Range("D" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("売上番号").Value '売上番号
                'sheet.Range("E" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("売上番号枝番").Value '売上番号枝番

                sheet.Range("E" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("請求番号").Value '請求番号
                sheet.Range("F" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("請求日").Value '請求日

                sheet.Range("G" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("得意先コード").Value '得意先コード
                sheet.Range("H" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("得意先名").Value '得意先名

                sheet.Range("I" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("メーカー").Value 'メーカー
                sheet.Range("J" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("品名").Value '品名
                sheet.Range("K" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("型式").Value '型式

                sheet.Range("L" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("販売通貨").Value '販売通貨
                sheet.Range("M" & cellRowIndex.ToString).Value = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("受注単価_原通貨").Value) '受注単価_原通貨
                sheet.Range("N" & cellRowIndex.ToString).Value = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("受注単価_IDR").Value) '受注単価_IDR
                sheet.Range("O" & cellRowIndex.ToString).Value = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("受注数量").Value) '受注数量
                sheet.Range("P" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("単位").Value '単位

                sheet.Range("Q" & cellRowIndex.ToString).Value = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("受注金額_原通貨").Value) '受注金額_原通貨
                sheet.Range("R" & cellRowIndex.ToString).Value = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("受注金額_IDR").Value) '受注金額_IDR


                sheet.Range("S" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("発注番号").Value '発注番号
                sheet.Range("T" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("発注番号枝番").Value '発注番号枝番
                sheet.Range("U" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("発注行番号").Value '発注行番号

                'sheet.Range("U" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("仕入番号").Value '仕入番号
                'sheet.Range("V" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("仕入行番号").Value '仕入行番号
                sheet.Range("V" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("仕入区分").Value '仕入区分

                sheet.Range("W" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("仕入先コード").Value '仕入先コード
                sheet.Range("X" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("仕入先名").Value '仕入先名


                sheet.Range("Y" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("仕入通貨").Value '仕入通貨
                sheet.Range("Z" & cellRowIndex.ToString).Value = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("仕入単価_原通貨").Value) '仕入単価_原通貨
                sheet.Range("AA" & cellRowIndex.ToString).Value = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("仕入単価_IDR").Value) '仕入単価_IDR
                sheet.Range("AB" & cellRowIndex.ToString).Value = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("仕入原価_原通貨").Value) '仕入原価_原通貨
                sheet.Range("AC" & cellRowIndex.ToString).Value = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("仕入原価_IDR").Value) '仕入原価_IDR

                sheet.Range("AD" & cellRowIndex.ToString).Value = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("間接費").Value) '間接費
                sheet.Range("AE" & cellRowIndex.ToString).Value = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("利益").Value) '利益
                sheet.Range("AF" & cellRowIndex.ToString).Value = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("利益率").Value) '利益率


                'sheet.Range("L" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
                'sheet.Range("M" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
                'sheet.Range("N" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
                'sheet.Range("P" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
                'sheet.Range("Q" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight

            Next

            sheet.Columns("A:AF").EntireColumn.AutoFit  '幅の自動調整

            ' 行7全体のオブジェクトを作成
            xlRngTmp = sheet.Rows
            xlRng = xlRngTmp(cellRowIndex)

            '最後に合計行の追加
            cellRowIndex += 2

            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then  '英語
                sheet.Range("D" & cellRowIndex.ToString).Value = "OrderAmountTotal"
                sheet.Range("D" & cellRowIndex.ToString + 3).Value = "PurchaseCostMeter"
                sheet.Range("F" & cellRowIndex.ToString).Value = "ProfitMargin"
                sheet.Range("F" & cellRowIndex.ToString + 3).Value = "ProfitRate(%)"

            Else  '日本語
                sheet.Range("D" & cellRowIndex.ToString).Value = "受注金額計"
                sheet.Range("D" & cellRowIndex.ToString + 3).Value = "仕入原価計"
                sheet.Range("F" & cellRowIndex.ToString).Value = "利益"
                sheet.Range("F" & cellRowIndex.ToString + 3).Value = "利益率(%)"
            End If

            sheet.Range("D" & cellRowIndex.ToString + 1).Value = CDec(TxtSalesAmount.Text).ToString("0.00")
            sheet.Range("D" & cellRowIndex.ToString + 1).NumberFormatLocal = "#,##0.00"
            sheet.Range("D" & cellRowIndex.ToString + 4).Value = CDec(TxtSalesCostAmount.Text).ToString("0.00")
            sheet.Range("D" & cellRowIndex.ToString + 4).NumberFormatLocal = "#,##0.00"

            sheet.Range("F" & cellRowIndex.ToString + 1).Value = CDec(TxtGrossMargin.Text).ToString("0.00")
            sheet.Range("F" & cellRowIndex.ToString + 1).NumberFormatLocal = "#,##0.00"
            sheet.Range("F" & cellRowIndex.ToString + 4).Value = CDec(TxtGrossMarginRate.Text).ToString("0.00")
            sheet.Range("F" & cellRowIndex.ToString + 4).NumberFormatLocal = "0.0"


            ' 境界線オブジェクトを作成 →7行目の下部に罫線を描画する
            xlBorders = xlRngTmp.Borders
            xlBorder = xlBorders(XlBordersIndex.xlEdgeBottom)
            xlBorder.LineStyle = XlLineStyle.xlContinuous

            app.DisplayAlerts = False 'Microsoft Excelのアラート一旦無効化

            Dim excelChk As Boolean = _com.excelOutput(sOutFile)
            If excelChk = False Then
                Exit Sub
            End If
            book.SaveAs(sOutFile) '書き込み実行

            app.DisplayAlerts = True 'アラート無効化を解除

            'カーソルをビジー状態から元に戻す
            Cursor.Current = Cursors.Default

            app.Visible = True
            _msgHd.dspMSG("CreateExcel", frmC01F10_Login.loginValue.Language)

        Catch ex As Exception
            'Throw ex
            'カーソルをビジー状態から元に戻す
            Cursor.Current = Cursors.Default
            UtilMsgHandler.VbMessageboxShow(ex.Message, ex.StackTrace.ToString, CommonConst.AP_NAME, ex.HResult)
        Finally
            'app.Quit()
            'Marshal.ReleaseComObject(sheet)
            'Marshal.ReleaseComObject(book)
            'Marshal.ReleaseComObject(app)

        End Try


    End Sub

    '一覧取得
    Private Sub getList2()

        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        DgvList.Rows.Clear() '一覧クリア
        Cursor.Current = Cursors.WaitCursor

#Region "SQL"

        Sql = " SELECT "
        Sql += " t23.* "
        Sql += " FROM v_t23_skyuhd_1 t23 "
        Sql += " WHERE "
        Sql += " t23.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND "
        Sql += " t23.請求日 >= '" & UtilClass.strFormatDate(DateTimePicker1.Value) & "'"
        Sql += " AND "
        Sql += " t23.請求日 <= '" & UtilClass.strFormatDate(DateTimePicker2.Value) & "'"
        Sql += " ORDER BY t23.請求番号,t23.売上行番号"
#End Region


        Try

            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            Dim totalSales As Decimal = 0
            Dim totalSalesAmount As Decimal = 0
            Dim salesUnitPrice As Decimal = 0
            Dim totalArari As Decimal = 0
            Dim totalArariRate As Decimal = 0
            Dim intListCnt As Integer = 0  'データグリッドのカウント
            Dim save_idx As Integer = 0
            Dim save_dm As String = ""
            Dim bal As Decimal = 0
            Dim ln As Integer = 0
            Dim flag As Boolean = False
            For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                flag = False
                DgvList.Rows.Add()
                'DgvList.Rows(intListCnt).Cells("行番号").Value = intListCnt + 1
                'DgvList.Rows(intListCnt).Cells("受注番号").Value = ds.Tables(RS).Rows(i)("受注番号")
                'DgvList.Rows(intListCnt).Cells("受注番号枝番").Value = ds.Tables(RS).Rows(i)("受注番号枝番")
                'DgvList.Rows(intListCnt).Cells("受注行番号").Value = ds.Tables(RS).Rows(i)("売上行番号")

                If save_dm <> ds.Tables(RS).Rows(i)("請求番号") Then
                    ln += 1
                    DgvList.Rows(intListCnt).Cells("行番号").Value = ln
                    DgvList.Rows(intListCnt).Cells("請求番号").Value = ds.Tables(RS).Rows(i)("請求番号")
                    save_dm = DgvList.Rows(intListCnt).Cells("請求番号").Value
                    save_idx = intListCnt
                    Dim dtmTemp As DateTime = ds.Tables(RS).Rows(i)("請求日")
                    DgvList.Rows(intListCnt).Cells("請求日").Value = dtmTemp.ToShortDateString

                    'DgvList.Rows(intListCnt).Cells("得意先コード").Value = ds.Tables(RS).Rows(i)("得意先コード")
                    DgvList.Rows(intListCnt).Cells("得意先名").Value = ds.Tables(RS).Rows(i)("得意先名")
                    'DgvList.Rows(intListCnt).Cells("メーカー").Value = ds.Tables(RS).Rows(i)("メーカー")
                    'DgvList.Rows(intListCnt).Cells("品名").Value = ds.Tables(RS).Rows(i)("品名")
                    'DgvList.Rows(intListCnt).Cells("型式").Value = ds.Tables(RS).Rows(i)("型式")
                    DgvList.Rows(intListCnt).Cells("販売通貨").Value = _com.getCurrencyEx(ds.Tables(RS).Rows(i)("通貨"))

                    'DgvList.Rows(intListCnt).Cells("受注単価_IDR").Value = UtilClass.rmNullDecimal(ds.Tables(RS).Rows(i)("見積単価")) '売単価"))
                    'DgvList.Rows(intListCnt).Cells("受注数量").Value = UtilClass.rmNullDecimal(ds.Tables(RS).Rows(i)("売上数量"))
                    'bal = DgvList.Rows(intListCnt).Cells("受注数量").Value
                    DgvList.Rows(intListCnt).Cells("単位").Value = ds.Tables(RS).Rows(i)("客先番号")
                    DgvList.Rows(intListCnt).Cells("受注金額_原通貨").Value = UtilClass.rmNullDecimal(ds.Tables(RS).Rows(i)("請求金額計_外貨"))
                    DgvList.Rows(intListCnt).Cells("受注金額_IDR").Value = UtilClass.rmNullDecimal(ds.Tables(RS).Rows(i)("請求消費税計"))
                    DgvList.Rows(intListCnt).Cells("受注単価_原通貨").Value = DgvList.Rows(intListCnt).Cells("受注金額_原通貨").Value - DgvList.Rows(intListCnt).Cells("受注金額_IDR").Value

                    totalSales += DgvList.Rows(intListCnt).Cells("受注単価_原通貨").Value
                    totalSalesAmount += DgvList.Rows(intListCnt).Cells("受注金額_原通貨").Value

                    DgvList.Rows(intListCnt).Cells("利益").Value = DgvList.Rows(intListCnt).Cells("受注単価_原通貨").Value

                    flag = True

                End If

                DgvList.Rows(save_idx).Cells("受注数量").Value += UtilClass.rmNullDecimal(ds.Tables(RS).Rows(i)("売上数量"))
                bal = UtilClass.rmNullDecimal(ds.Tables(RS).Rows(i)("売上数量"))

                Dim fkpc_ As String = UtilClass.rmDBNull2StrNull(ds.Tables(RS).Rows(i)("仕入区分"))

                Dim dsHanyou As DataSet = _com.getDsHanyoData(CommonConst.FIXED_KEY_PURCHASING_CLASS, fkpc_)
                If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                    DgvList.Rows(intListCnt).Cells("仕入区分").Value = dsHanyou.Tables(RS).Rows(0)("文字２")
                Else
                    DgvList.Rows(intListCnt).Cells("仕入区分").Value = dsHanyou.Tables(RS).Rows(0)("文字１")
                End If

                If fkpc_ = CommonConst.Sire_KBN_Zaiko Then
                    If flag Then
                        'save_dm = DgvList.Rows(intListCnt).Cells("請求番号").Value
                        Sql = "SELECT"
                        Sql += " t45.出庫番号, t45.行番号, t45.出庫数量 "
                        Sql += " from t45_shukodt t45, t44_shukohd t44 "
                        Sql += " where t45.出庫番号 = t44.出庫番号"
                        Sql += " AND t45.受注番号 = '" & ds.Tables(RS).Rows(i)("受注番号") & "'"
                        Sql += " AND t45.受注番号枝番 = '" & ds.Tables(RS).Rows(i)("受注番号枝番") & "'"
                        Sql += " AND t45.行番号 = '" & ds.Tables(RS).Rows(i)("売上行番号") & "'"
                        Sql += " AND t44.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
                        Sql += " AND t45.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                        'Sql += " AND t45.出庫数量 = " & DgvList.Rows(intListCnt).Cells("受注数量").Value
                        Sql += " AND t45.出庫区分 <> '2'"

                        Dim ds3 As DataSet = _db.selectDB(Sql, RS, reccnt)

                        If reccnt = 0 Then
                            DgvList.Rows(intListCnt).Cells("発注番号").Value = "not yet delivery"
                        Else

                            For j As Integer = 0 To ds3.Tables(RS).Rows.Count - 1

                                Sql = "SELECT"
                                Sql += " t70.出庫開始サイン as ロケ番号 "
                                Sql += " from t70_inout t70 "
                                Sql += " where "
                                Sql += " t70.伝票番号 = '" & ds3.Tables(RS).Rows(j)("出庫番号") & "'"
                                Sql += " AND t70.行番号 = '" & ds3.Tables(RS).Rows(j)("行番号") & "'"
                                Sql += " AND t70.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
                                Sql += " AND t70.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

                                Dim ds4 As DataSet = _db.selectDB(Sql, RS, reccnt)
                                Dim lgh As Integer = ds4.Tables(RS).Rows(0)("ロケ番号").ToString.Length()

                                Sql = "SELECT"
                                Sql += " t42.発注番号, t42.発注番号枝番, t43.行番号 "
                                Sql += " from t43_nyukodt t43, t42_nyukohd t42 "
                                Sql += " where "
                                Sql += " t43.入庫番号 = '" & UtilClass.rmDBNull2StrNull(ds4.Tables(RS).Rows(0)("ロケ番号")).ToString.Substring(0, 10) & "'"
                                Sql += " and t43.行番号 = '" & UtilClass.rmDBNull2StrNull(ds4.Tables(RS).Rows(0)("ロケ番号")).ToString.Substring(10, lgh - 10) & "'"
                                Sql += " and t42.入庫番号 = t43.入庫番号 "
                                Sql += " And t42.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
                                Sql += " And t43.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

                                Dim ds5 As DataSet = _db.selectDB(Sql, RS, reccnt)
                                Dim po_ As String = "no goods receipt"
                                Dim v_ As String = ""
                                Dim l_ As String = ""
                                Dim cur_ As String = ""
                                Dim sc_ As String = ""
                                Dim sn_ As String = ""
                                Dim upf_ As Decimal = 0
                                Dim upi_ As Decimal = 0
                                Dim amtf_ As Decimal = 0
                                Dim amti_ As Decimal = 0
                                Dim oh_ As Decimal = 0
                                Dim supinv As String = ""
                                Dim invd As Date
                                Dim qty_ As Decimal = 0

                                If ds5.Tables(RS).Rows.Count > 0 Then

                                    Sql = "SELECT t21.*, t20.* "
                                    Sql += " FROM t21_hattyu t21, t20_hattyu t20 "
                                    Sql += " WHERE "
                                    Sql += " t20.発注番号 = '" & ds5.Tables(RS).Rows(0)("発注番号") & "'"
                                    Sql += " AND t20.発注番号枝番 = '" & ds5.Tables(RS).Rows(0)("発注番号枝番") & "'"
                                    Sql += " AND t21.行番号 = '" & ds5.Tables(RS).Rows(0)("行番号") & "'"
                                    Sql += " AND t21.発注番号 = t20.発注番号 and t21.発注番号枝番 = t20.発注番号枝番 "
                                    Sql += " AND t20.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
                                    Sql += " AND t21.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                                    Sql += " Order by t21.行番号 desc "

                                    Dim ds2 As DataSet = _db.selectDB(Sql, RS, reccnt)
                                    Dim ii As Integer = 0

                                    Sql = "SELECT t46.* "
                                    Sql += " FROM t46_kikehd t46 "
                                    Sql += " WHERE "
                                    Sql += " t46.発注番号 = '" & ds2.Tables(RS).Rows(ii)("発注番号") & "'"
                                    Sql += " AND t46.発注番号枝番 = '" & ds2.Tables(RS).Rows(ii)("発注番号枝番") & "'"
                                    Sql += " AND t46.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
                                    Sql += " AND t46.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                                    Dim ds9 As DataSet = _db.selectDB(Sql, RS, reccnt)
                                    If reccnt > 0 Then
                                        supinv = UtilClass.rmDBNull2StrNull(ds9.Tables(RS).Rows(ii)("仕入先請求番号"))
                                        invd = ds9.Tables(RS).Rows(ii)("買掛日")
                                    End If

                                    po_ = ds2.Tables(RS).Rows(ii)("発注番号")
                                    v_ = ds2.Tables(RS).Rows(ii)("発注番号枝番")
                                    l_ = ds2.Tables(RS).Rows(ii)("行番号")
                                    cur_ = _com.getCurrencyEx(ds2.Tables(RS).Rows(ii)("仕入通貨"))
                                    sc_ = ds2.Tables(RS).Rows(ii)("仕入先コード")
                                    sn_ = ds2.Tables(RS).Rows(ii)("仕入先名")
                                    upi_ = UtilClass.rmNullDecimal(ds2.Tables(RS).Rows(ii)("仕入値"))
                                    upf_ = UtilClass.rmNullDecimal(ds2.Tables(RS).Rows(ii)("仕入値_外貨"))
                                    amtf_ = DgvList.Rows(intListCnt).Cells("受注数量").Value * upf_ 'UtilClass.rmNullDecimal(ds2.Tables(RS).Rows(ii)("仕入金額_外貨"))
                                    amti_ = DgvList.Rows(intListCnt).Cells("受注数量").Value * upi_ 'UtilClass.rmNullDecimal(ds2.Tables(RS).Rows(ii)("仕入金額"))
                                    oh_ = ds2.Tables(RS).Rows(ii)("関税額") + ds2.Tables(RS).Rows(ii)("前払法人税額") + ds2.Tables(RS).Rows(ii)("輸送費額")
                                    qty_ = UtilClass.rmNullDecimal(ds3.Tables(RS).Rows(j)("出庫数量"))
                                End If

                                DgvList.Rows(intListCnt).Cells("発注番号").Value += po_ + "-" + v_
                                'DgvList.Rows(intListCnt).Cells("発注番号枝番").Value = v_
                                DgvList.Rows(intListCnt).Cells("発注行番号").Value = "" 'l_
                                DgvList.Rows(intListCnt).Cells("仕入通貨").Value = cur_
                                DgvList.Rows(intListCnt).Cells("仕入先コード").Value += supinv
                                DgvList.Rows(intListCnt).Cells("仕入先名").Value += sn_
                                DgvList.Rows(intListCnt).Cells("仕入単価_IDR").Value += qty_ 'upi_
                                DgvList.Rows(intListCnt).Cells("仕入単価_原通貨").Value += (qty_ * upf_)
                                'DgvList.Rows(intListCnt).Cells("仕入原価_原通貨").Value += amtf_
                                DgvList.Rows(intListCnt).Cells("仕入原価_IDR").Value = Now '+= amti_
                                DgvList.Rows(intListCnt).Cells("間接費").Value += oh_
                                DgvList.Rows(intListCnt).Cells("得意先コード").Value = invd

                            Next

                        End If

                        DgvList.Rows(save_idx).Cells("利益").Value -= (DgvList.Rows(intListCnt).Cells("仕入単価_原通貨").Value + DgvList.Rows(intListCnt).Cells("間接費").Value)
                        'DgvList.Rows(intListCnt).Cells("利益").Value = DgvList.Rows(intListCnt).Cells("受注金額_IDR").Value - DgvList.Rows(intListCnt).Cells("仕入原価_IDR").Value - DgvList.Rows(intListCnt).Cells("間接費").Value

                        bal -= DgvList.Rows(intListCnt).Cells("仕入単価_IDR").Value

                        'If DgvList.Rows(intListCnt).Cells("受注金額_IDR").Value = 0 Then
                        'DgvList.Rows(intListCnt).Cells("利益率").Value = 0
                        'Else
                        'DgvList.Rows(intListCnt).Cells("利益率").Value = DgvList.Rows(intListCnt).Cells("利益").Value / DgvList.Rows(intListCnt).Cells("受注金額_IDR").Value * 100
                        'End If

                        salesUnitPrice += DgvList.Rows(intListCnt).Cells("仕入単価_原通貨").Value + DgvList.Rows(intListCnt).Cells("間接費").Value
                    End If

                    DgvList.Rows(save_idx).Cells("利益率").Value += bal

                ElseIf (fkpc_ = CommonConst.Sire_KBN_Sire) Or (fkpc_ = CommonConst.Sire_KBN_DELIVERY) Or (fkpc_ = CommonConst.Sire_KBN_OS) Then
                        Sql = "SELECT t21.*, t20.* "
                        Sql += " FROM t21_hattyu t21, t20_hattyu t20 "
                        Sql += " WHERE "
                        Sql += " t20.受注番号 = '" & ds.Tables(RS).Rows(i)("受注番号") & "'"
                        'Sql += " AND t20.受注番号枝番 = '" & ds.Tables(RS).Rows(i)("受注番号枝番") & "'"
                        Sql += " AND t21.行番号 = '" & ds.Tables(RS).Rows(i)("売上行番号") & "'"
                        Sql += " AND t21.発注番号 = t20.発注番号 and t21.発注番号枝番 = t20.発注番号枝番 "
                        Sql += " AND t20.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
                        Sql += " AND t21.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                        Sql += " Order by t21.発注番号枝番 desc "

                        Dim ds2 As DataSet = _db.selectDB(Sql, RS, reccnt)
                        Dim ii As Integer = 0

                        If reccnt = 0 Then
                        Else

                            Dim reccnt2 As Integer = 0

                            Sql = "SELECT t46.* "
                            Sql += " FROM t46_kikehd t46 "
                            Sql += " WHERE "
                            Sql += " t46.発注番号 = '" & ds2.Tables(RS).Rows(ii)("発注番号") & "'"
                            Sql += " AND t46.発注番号枝番 = '" & ds2.Tables(RS).Rows(ii)("発注番号枝番") & "'"
                            'Sql += " AND t21.行番号 = '" & ds.Tables(RS).Rows(i)("売上行番号") & "'"
                            'Sql += " AND t21.発注番号 = t20.発注番号 and t21.発注番号枝番 = t20.発注番号枝番 "
                            Sql += " AND t46.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
                            Sql += " AND t46.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                            'Sql += " Order by t46.発注番号枝番 desc "

                            Dim ds3 As DataSet = _db.selectDB(Sql, RS, reccnt2)
                            Dim supinv As String = ""
                            Dim invd As Date
                            If reccnt2 > 0 Then
                                supinv = UtilClass.rmDBNull2StrNull(ds3.Tables(RS).Rows(ii)("仕入先請求番号"))
                                invd = ds3.Tables(RS).Rows(ii)("買掛日")
                            End If

                            DgvList.Rows(intListCnt).Cells("発注番号").Value = ds2.Tables(RS).Rows(ii)("発注番号") & "-" & ds2.Tables(RS).Rows(ii)("発注番号枝番")
                            'DgvList.Rows(intListCnt).Cells("発注番号枝番").Value = ds2.Tables(RS).Rows(ii)("発注番号枝番")
                            DgvList.Rows(intListCnt).Cells("発注行番号").Value = ds2.Tables(RS).Rows(ii)("行番号")
                            DgvList.Rows(intListCnt).Cells("仕入通貨").Value = _com.getCurrencyEx(ds2.Tables(RS).Rows(ii)("仕入通貨"))
                            DgvList.Rows(intListCnt).Cells("仕入先コード").Value = supinv
                            DgvList.Rows(intListCnt).Cells("仕入先名").Value = ds2.Tables(RS).Rows(ii)("仕入先名")
                            DgvList.Rows(intListCnt).Cells("仕入単価_IDR").Value = bal 'DgvList.Rows(intListCnt).Cells("受注数量").Value 'ds2.Tables(RS).Rows(ii)("入庫数")
                            DgvList.Rows(intListCnt).Cells("仕入単価_原通貨").Value = DgvList.Rows(intListCnt).Cells("仕入単価_IDR").Value * UtilClass.rmNullDecimal(ds2.Tables(RS).Rows(ii)("仕入値_外貨"))
                        'DgvList.Rows(intListCnt).Cells("仕入原価_原通貨").Value = UtilClass.rmNullDecimal(ds2.Tables(RS).Rows(ii)("仕入金額_外貨"))
                        DgvList.Rows(intListCnt).Cells("仕入原価_IDR").Value = ds.Tables(RS).Rows(i)("入金予定日")
                        Dim ind As Decimal = ds2.Tables(RS).Rows(ii)("関税額") + ds2.Tables(RS).Rows(ii)("前払法人税額") + ds2.Tables(RS).Rows(ii)("輸送費額")
                            DgvList.Rows(intListCnt).Cells("間接費").Value = ind
                            DgvList.Rows(intListCnt).Cells("得意先コード").Value = invd

                            DgvList.Rows(save_idx).Cells("利益").Value -= (DgvList.Rows(intListCnt).Cells("仕入単価_原通貨").Value + DgvList.Rows(intListCnt).Cells("間接費").Value)

                            'If DgvList.Rows(intListCnt).Cells("受注金額_IDR").Value = 0 Then
                            bal -= DgvList.Rows(intListCnt).Cells("仕入単価_IDR").Value
                            DgvList.Rows(intListCnt).Cells("利益率").Value = bal
                            'Else
                            'DgvList.Rows(intListCnt).Cells("利益率").Value = DgvList.Rows(intListCnt).Cells("利益").Value / DgvList.Rows(intListCnt).Cells("受注金額_IDR").Value * 100
                            'End If

                            salesUnitPrice += DgvList.Rows(intListCnt).Cells("仕入単価_原通貨").Value + DgvList.Rows(intListCnt).Cells("間接費").Value 'DgvList.Rows(intListCnt).Cells("仕入原価_IDR").Value + DgvList.Rows(intListCnt).Cells("間接費").Value
                        End If
                    Else

                    End If

                intListCnt += 1  'データグリッドのカウントをアップ

            Next

#Region "フッダー"

            totalArari = totalSales - salesUnitPrice

            If totalArari > 0 And salesUnitPrice <> 0 Then
                totalArariRate = (totalArari / totalSales) * 100
            Else
                totalArariRate = 0
            End If

            '売上計
            TxtSalesAmount.Text = IIf(
                totalSales <> 0,
                Format(totalSales, "#,##0.00"),
                0
            )
            '売上 + VAT
            TxtTotalSalesAmount.Text = IIf(
                totalSalesAmount <> 0,
                Format(totalSalesAmount, "#,##0.00"),
                0
            )
            '売上原価
            TxtSalesCostAmount.Text = IIf(
                salesUnitPrice <> 0,
                Format(salesUnitPrice, "#,##0.00"),
                0
            )
            '粗利額
            TxtGrossMargin.Text = IIf(
                totalArari <> 0,
                Format(totalArari, "#,##0.00"),
                0
            )
            '粗利率
            TxtGrossMarginRate.Text = IIf(
                totalArariRate <> 0,
                Format(totalArariRate, "0.0"),
                0
            )

#End Region


        Catch ue As UsrDefException
            'ue.dspMsg()
            'Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            'Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try
        Cursor.Current = Cursors.AppStarting
    End Sub

    Private Sub outputExcel3()

        '定義
        Dim app As Excel.Application = Nothing
        Dim book As Excel.Workbook = Nothing
        Dim sheet As Excel.Worksheet = Nothing

        ' セル
        Dim xlRngTmp As Range = Nothing
        Dim xlRng As Range = Nothing

        ' セル境界線（枠）
        Dim xlBorders As Borders = Nothing
        Dim xlBorder As Border = Nothing

        'カーソルをビジー状態にする
        Cursor.Current = Cursors.WaitCursor

        Try
            '雛形パス
            Dim sHinaPath As String = StartUp._iniVal.BaseXlsPath
            '雛形ファイル名
            Dim sHinaFile As String = sHinaPath & "\" & "SalesProfitList.xlsx"
            '出力先パス
            Dim sOutPath As String = StartUp._iniVal.OutXlsPath
            '出力ファイル名
            Dim sOutFile As String = sOutPath & "\SalesProfitList_" & DateTime.Now.ToString("yyyyMMddHHmm") & ".xlsx"

            app = New Excel.Application()
            app.ScreenUpdating = False
            app.EnableEvents = False
            app.Visible = False
            app.DisplayAlerts = False

            book = app.Workbooks.Add(sHinaFile)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet)
            app.Calculation = Microsoft.Office.Interop.Excel.XlCalculation.xlCalculationManual

            sheet.PageSetup.LeftHeader = "SALES AND PURCHASE REPORT" '"売上・売上原価・利益・利益率一覧表"
            sheet.PageSetup.CenterHeader = DateTimePicker1.Value.ToShortDateString & "-" & DateTimePicker2.Value.ToShortDateString
            sheet.PageSetup.RightHeader = "OutputDate：" & DateTime.Now.ToShortDateString

            Dim rowCnt As Integer = 0
            'Dim lstRow As Integer = 2
            'Dim addRowCnt As Integer = 0
            'Dim currentCnt As Integer = 19
            'Dim num As Integer = 1

            rowCnt = DgvList.Rows.Count - 1
            Dim array2(rowCnt, 21) As String

            Dim cellRowIndex As Integer = 1

            For i As Integer = 0 To DgvList.RowCount - 1
                'cellRowIndex += 1
                'sheet.Rows(cellRowIndex).Insert

                array2(i, 0) = DgvList.Rows(i).Cells("行番号").Value '行番号 'A
                'array2(i, 1) = DgvList.Rows(i).Cells("受注番号").Value '受注番号
                'array2(i, 2) = DgvList.Rows(i).Cells("受注番号枝番").Value '受注番号枝番
                'array2(i, 3) = DgvList.Rows(i).Cells("受注行番号").Value '行番号
                'sheet.Range("D" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("売上番号").Value '売上番号
                'sheet.Range("E" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("売上番号枝番").Value '売上番号枝番
                array2(i, 1) = DgvList.Rows(i).Cells("請求番号").Value '請求番号 B
                array2(i, 2) = DgvList.Rows(i).Cells("請求日").Value '請求日 C
                array2(i, 3) = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("受注数量").Value) '.ToString("N2") '受注数量 D
                array2(i, 4) = DgvList.Rows(i).Cells("販売通貨").Value '販売通貨 E
                array2(i, 5) = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("受注単価_原通貨").Value) '.ToString("N2") '受注単価_原通貨 F
                array2(i, 6) = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("受注金額_IDR").Value) '.ToString("N2") '受注金額_IDR G
                array2(i, 7) = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("受注金額_原通貨").Value) '.ToString("N2") '受注金額_原通貨 H
                array2(i, 8) = DgvList.Rows(i).Cells("得意先名").Value '得意先名 I
                array2(i, 9) = DgvList.Rows(i).Cells("単位").Value '単位 J
                array2(i, 10) = DgvList.Rows(i).Cells("発注番号").Value '発注番号 K
                array2(i, 11) = DgvList.Rows(i).Cells("仕入先名").Value '仕入先名 L
                array2(i, 12) = DgvList.Rows(i).Cells("仕入先コード").Value '仕入先コード M
                array2(i, 13) = DgvList.Rows(i).Cells("得意先コード").Value '得意先コード N
                array2(i, 14) = DgvList.Rows(i).Cells("発注行番号").Value '発注行番号 O
                array2(i, 15) = DgvList.Rows(i).Cells("仕入区分").Value '仕入区分 P
                array2(i, 16) = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("仕入単価_IDR").Value) '.ToString("N2") '仕入単価_IDR Q
                array2(i, 17) = DgvList.Rows(i).Cells("仕入通貨").Value '仕入通貨 R
                array2(i, 18) = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("仕入単価_原通貨").Value) '.ToString("N2") '仕入単価_原通貨 S
                array2(i, 19) = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("利益率").Value) '.ToString("N2") '利益率 T
                array2(i, 20) = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("利益").Value) '.ToString("N2") '利益 U
                array2(i, 21) = DgvList.Rows(i).Cells("仕入原価_IDR").Value '仕入原価_IDR V

                'array2(i, 8) = DgvList.Rows(i).Cells("メーカー").Value 'メーカー
                'array2(i, 9) = DgvList.Rows(i).Cells("品名").Value '品名
                'array2(i, 10) = DgvList.Rows(i).Cells("型式").Value '型式
                'array2(i, 13) = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("受注単価_IDR").Value).ToString("N2") '受注単価_IDR
                'array2(i, 19) = DgvList.Rows(i).Cells("発注番号枝番").Value '発注番号枝番
                'sheet.Range("U" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("仕入番号").Value '仕入番号
                'sheet.Range("V" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("仕入行番号").Value '仕入行番号
                'array2(i, 27) = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("仕入原価_原通貨").Value).ToString("N2") '仕入原価_原通貨
                'array2(i, 29) = UtilClass.rmNullDecimal(DgvList.Rows(i).Cells("間接費").Value).ToString("N2") '間接費

            Next

            sheet.Range("A5:V" & (DgvList.RowCount + 1 + 3).ToString).Value = array2
            'sheet.Columns("A:AF").EntireColumn.AutoFit  '幅の自動調整
            cellRowIndex = rowCnt + 1 + 5

            ' 行7全体のオブジェクトを作成
            xlRngTmp = sheet.Rows
            xlRng = xlRngTmp(cellRowIndex)

            '最後に合計行の追加
            cellRowIndex += 2

            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then  '英語
                sheet.Range("D" & cellRowIndex.ToString).Value = "OrderAmountTotal"
                sheet.Range("D" & cellRowIndex.ToString + 3).Value = "PurchaseCostMeter"
                sheet.Range("F" & cellRowIndex.ToString).Value = "ProfitMargin"
                sheet.Range("F" & cellRowIndex.ToString + 3).Value = "ProfitRate(%)"

            Else  '日本語
                sheet.Range("D" & cellRowIndex.ToString).Value = "受注金額計"
                sheet.Range("D" & cellRowIndex.ToString + 3).Value = "仕入原価計"
                sheet.Range("F" & cellRowIndex.ToString).Value = "利益"
                sheet.Range("F" & cellRowIndex.ToString + 3).Value = "利益率(%)"
            End If

            sheet.Range("D" & cellRowIndex.ToString + 1).Value = CDec(TxtSalesAmount.Text).ToString("0.00")
            sheet.Range("D" & cellRowIndex.ToString + 1).NumberFormatLocal = "#,##0.00"
            sheet.Range("D" & cellRowIndex.ToString + 4).Value = CDec(TxtSalesCostAmount.Text).ToString("0.00")
            sheet.Range("D" & cellRowIndex.ToString + 4).NumberFormatLocal = "#,##0.00"

            sheet.Range("F" & cellRowIndex.ToString + 1).Value = CDec(TxtGrossMargin.Text).ToString("0.00")
            sheet.Range("F" & cellRowIndex.ToString + 1).NumberFormatLocal = "#,##0.00"
            sheet.Range("F" & cellRowIndex.ToString + 4).Value = CDec(TxtGrossMarginRate.Text).ToString("0.0")
            sheet.Range("F" & cellRowIndex.ToString + 4).NumberFormatLocal = "0.0"


            ' 境界線オブジェクトを作成 →7行目の下部に罫線を描画する
            'xlBorders = xlRng.Borders
            'xlBorder = xlBorders(XlBordersIndex.xlEdgeBottom)
            'xlBorder.LineStyle = XlLineStyle.xlContinuous

            Dim excelChk As Boolean = _com.excelOutput(sOutFile)
            If excelChk = False Then
                Exit Sub
            End If
            book.SaveAs(sOutFile) '書き込み実行

            app.Calculation = Microsoft.Office.Interop.Excel.XlCalculation.xlCalculationAutomatic
            app.DisplayAlerts = True 'アラート無効化を解除
            app.EnableEvents = True
            app.ScreenUpdating = True
            app.Visible = True

            'カーソルをビジー状態から元に戻す
            Cursor.Current = Cursors.Default

            _msgHd.dspMSG("CreateExcel", frmC01F10_Login.loginValue.Language)

        Catch ex As Exception
            'Throw ex
            'カーソルをビジー状態から元に戻す
            Cursor.Current = Cursors.Default
            UtilMsgHandler.VbMessageboxShow(ex.Message, ex.StackTrace.ToString, CommonConst.AP_NAME, ex.HResult)
        Finally
            'app.Quit()
            'Marshal.ReleaseComObject(sheet)
            'Marshal.ReleaseComObject(book)
            'Marshal.ReleaseComObject(app)

        End Try


    End Sub

End Class