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
    Private CompanyCode As String = ""
    Private SalesNo As String()
    Private SalesStatus As String = ""


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
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True
        DgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells
    End Sub

    '画面表示時
    Private Sub SalesProfitList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
        End If

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            LblMode.Text = SalesStatus & " Mode"

            Label8.Text = "SalesDate"
            BtnExcelOutput.Text = "ExcelOutput"
            BtnBack.Text = "Back"
            LblMonth.Text = "Month"
            LblYear.Text = "Year"

            LblSalesAmount.Text = "OrderAmount"
            LblTotalSalesAmount.Text = "TotalSalesAmount"
            LblSalesCostAmount.Text = "PurchaseAmount"
            LblGrossMargin.Text = "ProfitMargin"
            LblGrossMarginRate.Text = "ProfitMarginRate"

        End If


        setComboBox(cmbYear) 'コンボボックスに年を設定
        setComboBox(cmbMonth) 'コンボボックスに月を設定
        cmbYear.SelectedValue = Integer.Parse(Format(System.DateTime.Now, "yyyy"))
        cmbMonth.SelectedValue = Integer.Parse(Format(System.DateTime.Now, "MM"))

        getList() '一覧表示


        msetLine()  '行の設定

    End Sub

    '一覧取得
    Private Sub getList()

        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""
        Dim selectYear As Integer = cmbYear.SelectedValue
        Dim selectMonth As Integer = cmbMonth.SelectedValue
        Dim strSelectYear As String = cmbYear.SelectedValue.ToString()
        Dim strSelectMonth As String = selectMonth.ToString()

        Dim uriDateSince As New Date(strSelectYear, strSelectMonth, "01")
        Dim uriDateUntil As New Date(selectYear, selectMonth, Date.DaysInMonth(selectYear, selectMonth))

        Dim curds As DataSet  'm25_currency
        Dim cur As String


        DgvList.Rows.Clear() '一覧クリア


#Region "SQL"

        Sql = " SELECT "
        Sql += " t10.受注番号,t10.受注番号枝番,t11.行番号"
        Sql += ",t30.売上番号, t30.売上番号枝番"

        Sql += ",t10.得意先コード,t10.得意先名"
        Sql += ",t11.メーカー,t11.品名,t11.型式"
        Sql += ",t11.通貨,t11.受注数量,t11.単位"
        Sql += ",t11.見積単価,t11.見積金額,t11.見積単価_外貨,t11.見積金額_外貨"
        Sql += ",t11.レート,t11.仕入レート"

        Sql += ",t11.仕入通貨,t11.仕入値,t11.仕入単価_外貨,t11.仕入原価,t11.間接費"

        Sql += ",t21.発注番号,t21.発注番号枝番,t21.行番号 as 発注行番号"

        Sql += ",t40.仕入先コード,t11.仕入先名"
        Sql += ",t41.仕入番号,t41.行番号 as 仕入行番号,t11.仕入区分"


        '受注
        Sql += " FROM t10_cymnhd as t10 "
        Sql += " left join t11_cymndt as t11 "
        Sql += " on t10.受注番号 = t11.受注番号 and t10.受注番号枝番 = t11.受注番号枝番"

        '売上
        Sql += " left join t30_urighd as t30 "
        Sql += " on t10.受注番号 = t30.受注番号 and t10.受注番号枝番 = t30.受注番号枝番"
        Sql += " and t30.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
        Sql += "   and t30.売上番号枝番 = (SELECT MAX(t30M.売上番号枝番) FROM t30_urighd as t30M where t30.売上番号 = t30M.売上番号) "


        '発注
        Sql += " left join t20_hattyu as t20 "
        Sql += " on t10.受注番号 = t20.受注番号 and t10.受注番号枝番 = t20.受注番号枝番"
        Sql += " and t20.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
        Sql += " and t20.発注番号枝番 = (SELECT MAX(t20M.発注番号枝番) FROM t20_hattyu as t20M where t20.発注番号 = t20M.発注番号) "

        Sql += " left join t21_hattyu as t21 "
        Sql += " on  t20.発注番号 = t21.発注番号 and t20.発注番号枝番 = t21.発注番号枝番 and t11.行番号 = t21.行番号"
        Sql += " and t11.メーカー = t21.メーカー and t11.品名 = t21.品名 and t11.型式 = t21.型式"


        '仕入
        Sql += " left join t40_sirehd as t40 "
        Sql += " on  t20.発注番号 = t40.発注番号 and t20.発注番号枝番 = t40.発注番号枝番"
        Sql += " and t40.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        Sql += " left join t41_siredt as t41 "
        Sql += " on  t40.仕入番号 = t41.仕入番号 and t20.発注番号 = t41.発注番号 and t20.発注番号枝番 = t41.発注番号枝番"
        'Sql += " and t11.メーカー = t21.メーカー and t11.品名 = t21.品名 and t11.型式 = t21.型式"



        Sql += " WHERE"
        Sql += "     t10.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
        Sql += " AND t10.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"

        '履歴最新
        Sql += "   and t10.受注番号枝番 = (SELECT MAX(t10M.受注番号枝番) FROM t10_cymnhd as t10M where t10.受注番号 = t10M.受注番号) "


        Sql += " AND "
        Sql += " t30.売上日 >= '" & UtilClass.strFormatDate(uriDateSince) & "'"
        Sql += " AND "
        Sql += " t30.売上日 <= '" & UtilClass.strFormatDate(uriDateUntil) & "'"


        Sql += " ORDER BY t10.受注番号,t10.受注番号枝番,t11.行番号 "
#End Region


        Try

            Dim totalSales As Decimal = 0
            Dim totalSalesAmount As Decimal = 0
            Dim salesUnitPrice As Decimal = 0
            Dim totalArari As Decimal = 0
            Dim totalArariRate As Decimal = 0

            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1

                DgvList.Rows.Add()


#Region "受注"

                DgvList.Rows(i).Cells("受注番号").Value = ds.Tables(RS).Rows(i)("受注番号")
                DgvList.Rows(i).Cells("受注番号枝番").Value = ds.Tables(RS).Rows(i)("受注番号枝番")
                DgvList.Rows(i).Cells("受注行番号").Value = ds.Tables(RS).Rows(i)("行番号")

                DgvList.Rows(i).Cells("売上番号").Value = ds.Tables(RS).Rows(i)("売上番号")
                DgvList.Rows(i).Cells("売上番号枝番").Value = ds.Tables(RS).Rows(i)("売上番号枝番")

                DgvList.Rows(i).Cells("得意先コード").Value = ds.Tables(RS).Rows(i)("得意先コード")
                DgvList.Rows(i).Cells("得意先名").Value = ds.Tables(RS).Rows(i)("得意先名")

                DgvList.Rows(i).Cells("メーカー").Value = ds.Tables(RS).Rows(i)("メーカー")
                DgvList.Rows(i).Cells("品名").Value = ds.Tables(RS).Rows(i)("品名")
                DgvList.Rows(i).Cells("型式").Value = ds.Tables(RS).Rows(i)("型式")


                If IsDBNull(ds.Tables(RS).Rows(i)("通貨")) Then
                    cur = vbNullString
                Else
                    Sql = " and 採番キー = " & ds.Tables(RS).Rows(i)("通貨")
                    curds = getDsData("m25_currency", Sql)

                    cur = curds.Tables(RS).Rows(0)("通貨コード")
                End If


                DgvList.Rows(i).Cells("販売通貨").Value = cur
                DgvList.Rows(i).Cells("受注単価_原通貨").Value = ds.Tables(RS).Rows(i)("見積単価")
                If rmNullDecimal(ds.Tables(RS).Rows(i)("レート")) = 0 Then
                Else
                    DgvList.Rows(i).Cells("受注単価_IDR").Value = rmNullDecimal(ds.Tables(RS).Rows(i)("見積単価_外貨"))
                End If

                DgvList.Rows(i).Cells("受注数量").Value = ds.Tables(RS).Rows(i)("受注数量")
                DgvList.Rows(i).Cells("単位").Value = ds.Tables(RS).Rows(i)("単位")


                DgvList.Rows(i).Cells("受注金額_原通貨").Value = ds.Tables(RS).Rows(i)("見積金額")
                If rmNullDecimal(ds.Tables(RS).Rows(i)("レート")) = 0 Then
                Else
                    DgvList.Rows(i).Cells("受注金額_IDR").Value = rmNullDecimal(ds.Tables(RS).Rows(i)("見積金額_外貨"))
                End If

                totalSales += DgvList.Rows(i).Cells("受注金額_IDR").Value

#End Region


#Region "発注"

                DgvList.Rows(i).Cells("発注番号").Value = ds.Tables(RS).Rows(i)("発注番号")
                DgvList.Rows(i).Cells("発注番号枝番").Value = ds.Tables(RS).Rows(i)("発注番号枝番")
                DgvList.Rows(i).Cells("発注行番号").Value = ds.Tables(RS).Rows(i)("発注行番号")


                DgvList.Rows(i).Cells("仕入番号").Value = ds.Tables(RS).Rows(i)("仕入番号")
                DgvList.Rows(i).Cells("仕入行番号").Value = ds.Tables(RS).Rows(i)("仕入行番号")


                'リードタイムのリストを汎用マスタから取得
                If rmNullDecimal(ds.Tables(RS).Rows(i)("仕入区分")) = 0 Then
                Else
                    Dim dsHanyou As DataSet = getDsHanyoData(CommonConst.FIXED_KEY_PURCHASING_CLASS, ds.Tables(RS).Rows(i)("仕入区分"))
                    If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                        DgvList.Rows(i).Cells("仕入区分").Value = dsHanyou.Tables(RS).Rows(0)("文字２")
                    Else
                        DgvList.Rows(i).Cells("仕入区分").Value = dsHanyou.Tables(RS).Rows(0)("文字１")
                    End If
                End If


                DgvList.Rows(i).Cells("仕入先コード").Value = ds.Tables(RS).Rows(i)("仕入先コード")
                DgvList.Rows(i).Cells("仕入先名").Value = ds.Tables(RS).Rows(i)("仕入先名")


                If IsDBNull(ds.Tables(RS).Rows(i)("仕入通貨")) Then
                    cur = vbNullString
                Else
                    Sql = " and 採番キー = " & ds.Tables(RS).Rows(i)("仕入通貨")
                    curds = getDsData("m25_currency", Sql)

                    cur = curds.Tables(RS).Rows(0)("通貨コード")
                End If

                DgvList.Rows(i).Cells("仕入通貨").Value = cur

                DgvList.Rows(i).Cells("仕入単価_原通貨").Value = ds.Tables(RS).Rows(i)("仕入値")
                DgvList.Rows(i).Cells("仕入単価_IDR").Value = rmNullDecimal(ds.Tables(RS).Rows(i)("仕入単価_外貨"))

                DgvList.Rows(i).Cells("仕入原価_原通貨").Value = ds.Tables(RS).Rows(i)("仕入原価")
                DgvList.Rows(i).Cells("仕入原価_IDR").Value = rmNullDecimal(ds.Tables(RS).Rows(i)("仕入単価_外貨")) * ds.Tables(RS).Rows(i)("受注数量")

                DgvList.Rows(i).Cells("間接費").Value = ds.Tables(RS).Rows(i)("間接費") * rmNullDecimal(ds.Tables(RS).Rows(i)("仕入レート"))

                DgvList.Rows(i).Cells("利益").Value = DgvList.Rows(i).Cells("受注金額_IDR").Value - DgvList.Rows(i).Cells("仕入原価_IDR").Value - DgvList.Rows(i).Cells("間接費").Value

                If DgvList.Rows(i).Cells("受注金額_IDR").Value = 0 Then
                Else
                    DgvList.Rows(i).Cells("利益率").Value = DgvList.Rows(i).Cells("利益").Value / DgvList.Rows(i).Cells("受注金額_IDR").Value * 100
                End If

                salesUnitPrice += DgvList.Rows(i).Cells("仕入原価_IDR").Value + DgvList.Rows(i).Cells("間接費").Value
#End Region


            Next


#Region "フッダー"

            totalArari = totalSales - salesUnitPrice

            If totalArari <> 0 And salesUnitPrice <> 0 Then
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

    'BK
    'Private Sub getList()

    '    Dim reccnt As Integer = 0 'DB用（デフォルト）
    '    Dim Sql As String = ""
    '    Dim selectYear As Integer = cmbYear.SelectedValue
    '    Dim selectMonth As Integer = cmbMonth.SelectedValue
    '    Dim strSelectYear As String = cmbYear.SelectedValue.ToString()
    '    Dim strSelectMonth As String = selectMonth.ToString()

    '    Dim uriDateSince As New Date(strSelectYear, strSelectMonth, "01")
    '    Dim uriDateUntil As New Date(selectYear, selectMonth, Date.DaysInMonth(selectYear, selectMonth))

    '    DgvList.Rows.Clear() '一覧クリア

    '    Sql = " Select t30.会社コード, t30.売上番号, t30.売上番号枝番, SUM(t31.見積金額) As 見積金額, SUM(t31.間接費) As 間接費 "
    '    Sql += " , SUM(t31.売上金額) As 売上金額, SUM(t31.仕入原価) As 仕入原価, SUM(t31.粗利額) As 粗利額 ,t30.更新日,max(t30.売上日) As 売上日"
    '    Sql += " , MAX(t30.得意先名) As 得意先名, MAX(t30.客先番号) As 客先番号 ,MAX(t30.営業担当者) As 営業担当者 ,MAX(t30.ＶＡＴ) As ＶＡＴ"
    '    Sql += " FROM t30_urighd t30 "
    '    Sql += " INNER JOIN t31_urigdt t31 "
    '    Sql += " On t30.会社コード = t31.会社コード "
    '    Sql += " And t30.売上番号 = t31.売上番号"
    '    Sql += " And t30.売上番号枝番 = t31.売上番号枝番 "

    '    Sql += " And "
    '    Sql += " t30.売上日 >= '" & UtilClass.strFormatDate(uriDateSince) & "'"
    '    Sql += " AND "
    '    Sql += " t30.売上日 <= '" & UtilClass.strFormatDate(uriDateUntil) & "'"

    '    Sql += " WHERE t30.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '取消区分=0
    '    Sql += " AND t30.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
    '    Sql += " GROUP BY t30.会社コード, t30.売上番号, t30.売上番号枝番, t30.更新日 "
    '    Sql += " ORDER BY t30.更新日 DESC "

    '    Try

    '        Dim totalSales As Decimal = 0
    '        Dim totalSalesAmount As Decimal = 0
    '        Dim salesUnitPrice As Decimal = 0
    '        Dim totalArari As Decimal = 0
    '        Dim totalArariRate As Decimal = 0

    '        Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

    '        For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1

    '            DgvList.Rows.Add()
    '            DgvList.Rows(i).Cells("売上番号").Value = ds.Tables(RS).Rows(i)("売上番号")
    '            DgvList.Rows(i).Cells("売上日").Value = ds.Tables(RS).Rows(i)("売上日").ToShortDateString()
    '            DgvList.Rows(i).Cells("得意先名").Value = ds.Tables(RS).Rows(i)("得意先名")
    '            DgvList.Rows(i).Cells("客先番号").Value = ds.Tables(RS).Rows(i)("客先番号")
    '            DgvList.Rows(i).Cells("営業担当者").Value = ds.Tables(RS).Rows(i)("営業担当者")
    '            DgvList.Rows(i).Cells("売上計").Value = ds.Tables(RS).Rows(i)("見積金額")
    '            DgvList.Rows(i).Cells("ＶＡＴ").Value = ds.Tables(RS).Rows(i)("ＶＡＴ")
    '            DgvList.Rows(i).Cells("売上金額計").Value = ds.Tables(RS).Rows(i)("見積金額") + ds.Tables(RS).Rows(i)("ＶＡＴ")
    '            DgvList.Rows(i).Cells("間接費").Value = ds.Tables(RS).Rows(i)("間接費")
    '            DgvList.Rows(i).Cells("売上原価計").Value = ds.Tables(RS).Rows(i)("仕入原価")
    '            DgvList.Rows(i).Cells("粗利").Value = ds.Tables(RS).Rows(i)("粗利額")

    '            If ds.Tables(RS).Rows(i)("粗利額") And ds.Tables(RS).Rows(i)("見積金額") Then
    '                DgvList.Rows(i).Cells("粗利率").Value = Format((ds.Tables(RS).Rows(i)("粗利額") / ds.Tables(RS).Rows(i)("見積金額")) * 100, "0.0")
    '            Else
    '                DgvList.Rows(i).Cells("粗利率").Value = 0
    '            End If

    '            totalSales += ds.Tables(RS).Rows(i)("見積金額")
    '            totalSalesAmount += ds.Tables(RS).Rows(i)("見積金額") + ds.Tables(RS).Rows(i)("ＶＡＴ")
    '            salesUnitPrice += ds.Tables(RS).Rows(i)("仕入原価")
    '            totalArari += ds.Tables(RS).Rows(i)("粗利額")

    '        Next

    '        If totalArari <> 0 And totalSalesAmount <> 0 Then
    '            totalArariRate = (totalArari / totalSales) * 100
    '        Else
    '            totalArariRate = 0
    '        End If

    '        '売上計
    '        TxtSalesAmount.Text = IIf(
    '            totalSales <> 0,
    '            Format(totalSales, "#,##0"),
    '            0
    '        )
    '        '売上 + VAT
    '        TxtTotalSalesAmount.Text = IIf(
    '            totalSalesAmount <> 0,
    '            Format(totalSalesAmount, "#,##0"),
    '            0
    '        )
    '        '売上原価
    '        TxtSalesCostAmount.Text = IIf(
    '            salesUnitPrice <> 0,
    '            Format(salesUnitPrice, "#,##0"),
    '            0
    '        )
    '        '粗利額
    '        TxtGrossMargin.Text = IIf(
    '            totalArari <> 0,
    '            Format(totalArari, "#,##0"),
    '            0
    '        )
    '        '粗利率
    '        TxtGrossMarginRate.Text = IIf(
    '            totalArariRate <> 0,
    '            Format(totalArariRate, "0.0"),
    '            0
    '        )

    '    Catch ue As UsrDefException
    '        ue.dspMsg()
    '        Throw ue
    '    Catch ex As Exception
    '        'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
    '        Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
    '    End Try

    'End Sub


    Private Sub msetLine()


#Region "行タイトル"

        '言語の判定
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then  '英語 

            DgvList.Columns("受注番号").HeaderText = "OrderNumber"
            DgvList.Columns("受注番号枝番").HeaderText = "OrderVer"
            DgvList.Columns("受注行番号").HeaderText = "OrderNo"

            DgvList.Columns("売上番号").HeaderText = "SalesNumber"
            DgvList.Columns("売上番号枝番").HeaderText = "SalesVer"

            DgvList.Columns("得意先コード").HeaderText = "CustomerCode"
            DgvList.Columns("得意先名").HeaderText = "CustomerName"

            DgvList.Columns("メーカー").HeaderText = "Maker"
            DgvList.Columns("品名").HeaderText = "ItemName"
            DgvList.Columns("型式").HeaderText = "Model"

            DgvList.Columns("販売通貨").HeaderText = "Currency"
            DgvList.Columns("受注単価_原通貨").HeaderText = "OrderUnitPrice" & vbCrLf & "(OrignalCurrency)"
            DgvList.Columns("受注単価_IDR").HeaderText = "OrderUnitPrice(IDR)" & vbCrLf & "a"

            DgvList.Columns("受注数量").HeaderText = "OrderQuantity" & vbCrLf & "b"
            DgvList.Columns("単位").HeaderText = "Unit"
            DgvList.Columns("受注金額_原通貨").HeaderText = "OrderAmount" & vbCrLf & "(OrignalCurrency)"
            DgvList.Columns("受注金額_IDR").HeaderText = "OrderAmount(IDR)" & vbCrLf & "c=a*b"

            DgvList.Columns("発注番号").HeaderText = "PurchaseOrderNumber"
            DgvList.Columns("発注番号枝番").HeaderText = "PurchaseOrderVer"
            DgvList.Columns("発注行番号").HeaderText = "PurchaseNo"


            DgvList.Columns("仕入番号").HeaderText = "PurchaseNumber"
            DgvList.Columns("仕入行番号").HeaderText = "PurchaseNo"
            DgvList.Columns("仕入区分").HeaderText = "PurchasingClassification"
            DgvList.Columns("仕入先コード").HeaderText = "SupplierCode"
            DgvList.Columns("仕入先名").HeaderText = "SupplierName"

            DgvList.Columns("仕入通貨").HeaderText = "Currency"
            DgvList.Columns("仕入単価_原通貨").HeaderText = "PurchaseUnitPrice" & vbCrLf & "(OrignalCurrency)"
            DgvList.Columns("仕入単価_IDR").HeaderText = "PurchaseUnitPrice(IDR)" & vbCrLf & "d"
            DgvList.Columns("仕入原価_原通貨").HeaderText = "PurchaseCost" & vbCrLf & "(OrignalCurrency)"
            DgvList.Columns("仕入原価_IDR").HeaderText = "PurchaseCost(IDR)" & vbCrLf & "e=b*d"
            DgvList.Columns("間接費").HeaderText = "Overhead" & vbCrLf & "f"
            DgvList.Columns("利益").HeaderText = "ProfitMargin" & vbCrLf & "g=c-e-f"
            DgvList.Columns("利益率").HeaderText = "ProfitMarginRate" & vbCrLf & "h=g/c"


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
        DgvList.Columns("仕入原価_IDR").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DgvList.Columns("間接費").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight



        '数字形式
        DgvList.Columns("受注単価_原通貨").DefaultCellStyle.Format = "N2"
        DgvList.Columns("受注単価_IDR").DefaultCellStyle.Format = "N2"
        DgvList.Columns("受注数量").DefaultCellStyle.Format = "N2"
        DgvList.Columns("受注金額_原通貨").DefaultCellStyle.Format = "N2"
        DgvList.Columns("受注金額_IDR").DefaultCellStyle.Format = "N2"

        DgvList.Columns("仕入単価_原通貨").DefaultCellStyle.Format = "N2"
        DgvList.Columns("仕入単価_IDR").DefaultCellStyle.Format = "N2"
        DgvList.Columns("仕入原価_原通貨").DefaultCellStyle.Format = "N2"
        DgvList.Columns("仕入原価_IDR").DefaultCellStyle.Format = "N2"
        DgvList.Columns("間接費").DefaultCellStyle.Format = "N2"

        DgvList.Columns("利益").DefaultCellStyle.Format = "N2"
        DgvList.Columns("利益率").DefaultCellStyle.Format = "N1"


        'タイトル 中央寄せ
        DgvList.Columns("受注単価_IDR").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvList.Columns("受注数量").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        DgvList.Columns("受注金額_IDR").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        DgvList.Columns("仕入単価_IDR").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvList.Columns("仕入原価_IDR").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        DgvList.Columns("間接費").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvList.Columns("利益").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DgvList.Columns("利益率").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

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
            outputExcel()
        End If
    End Sub

    'excel出力処理
    Private Sub outputExcel()

        '定義
        Dim app As Excel.Application = Nothing
        Dim book As Excel.Workbook = Nothing
        Dim sheet As Excel.Worksheet = Nothing

        Dim strSelectYear As String = cmbYear.SelectedValue.ToString()
        Dim strSelectMonth As String = cmbMonth.SelectedValue.ToString()

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
            book = app.Workbooks.Add(sHinaFile)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet)

            sheet.PageSetup.LeftHeader = "売上・売上原価・利益・利益率一覧表（月次）"
            sheet.PageSetup.CenterHeader = strSelectYear & "/" & strSelectMonth
            sheet.PageSetup.RightHeader = "OutputDate：" & DateTime.Now.ToShortDateString


#Region "タイトル"
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                sheet.PageSetup.LeftHeader = "SalesProfitList（Monthly）"
                sheet.PageSetup.CenterHeader = strSelectMonth & "/" & strSelectYear

                sheet.Range("A1").Value = "JobOrderNo"
                sheet.Range("B1").Value = "JobOrderVer"
                sheet.Range("C1").Value = "LineNo"

                sheet.Range("D1").Value = "SalesNo"
                sheet.Range("E1").Value = "SalesVer"

                sheet.Range("F1").Value = "CustomerNumber"
                sheet.Range("G1").Value = "CustomerName"

                sheet.Range("H1").Value = "Maker"
                sheet.Range("I1").Value = "Product"
                sheet.Range("J1").Value = "Model"

                sheet.Range("K1").Value = "SalesCurrency"
                sheet.Range("L1").Value = "OrderPrice(OriginalCurrency)"
                sheet.Range("M1").Value = "OrderPrice(IDR)"
                sheet.Range("N1").Value = "OrderQuantity"
                sheet.Range("O1").Value = "Unit"
                sheet.Range("P1").Value = "OrderAmount(OriginalCurrency)"
                sheet.Range("Q1").Value = "OrderAmount(IDR)"

                sheet.Range("R1").Value = "OrderNo"
                sheet.Range("S1").Value = "OrderVer"
                sheet.Range("T1").Value = "LineNo"
                sheet.Range("U1").Value = "PurchaseNo"
                sheet.Range("V1").Value = "LineNo"
                sheet.Range("W1").Value = "PurchaseCategory"
                sheet.Range("X1").Value = "VendorCode"

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


                sheet.Range("A" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("受注番号").Value '受注番号
                sheet.Range("B" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("受注番号枝番").Value '受注番号枝番
                sheet.Range("C" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("受注行番号").Value '行番号

                sheet.Range("D" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("売上番号").Value '売上番号
                sheet.Range("E" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("売上番号枝番").Value '売上番号枝番

                sheet.Range("F" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("得意先コード").Value '得意先コード
                sheet.Range("G" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("得意先名").Value '得意先名

                sheet.Range("H" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("メーカー").Value 'メーカー
                sheet.Range("I" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("品名").Value '品名
                sheet.Range("J" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("型式").Value '型式

                sheet.Range("K" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("販売通貨").Value '販売通貨
                sheet.Range("L" & cellRowIndex.ToString).Value = CDec(DgvList.Rows(i).Cells("受注単価_原通貨").Value) '受注単価_原通貨
                sheet.Range("M" & cellRowIndex.ToString).Value = CDec(DgvList.Rows(i).Cells("受注単価_IDR").Value) '受注単価_IDR
                sheet.Range("N" & cellRowIndex.ToString).Value = CDec(DgvList.Rows(i).Cells("受注数量").Value) '受注数量
                sheet.Range("O" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("単位").Value '単位

                sheet.Range("P" & cellRowIndex.ToString).Value = CDec(DgvList.Rows(i).Cells("受注金額_原通貨").Value) '受注金額_原通貨
                sheet.Range("Q" & cellRowIndex.ToString).Value = CDec(DgvList.Rows(i).Cells("受注金額_IDR").Value) '受注金額_IDR


                sheet.Range("R" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("発注番号").Value '発注番号
                sheet.Range("S" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("発注番号枝番").Value '発注番号枝番
                sheet.Range("T" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("発注行番号").Value '発注行番号

                sheet.Range("U" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("仕入番号").Value '仕入番号
                sheet.Range("V" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("仕入行番号").Value '仕入行番号
                sheet.Range("W" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("仕入区分").Value '仕入区分

                sheet.Range("X" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("仕入先コード").Value '仕入先コード
                sheet.Range("Y" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("仕入先名").Value '仕入先名


                sheet.Range("Z" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("仕入通貨").Value '仕入通貨
                sheet.Range("AA" & cellRowIndex.ToString).Value = CDec(DgvList.Rows(i).Cells("仕入単価_原通貨").Value) '仕入単価_原通貨
                sheet.Range("AB" & cellRowIndex.ToString).Value = CDec(DgvList.Rows(i).Cells("仕入単価_IDR").Value) '仕入単価_IDR
                sheet.Range("AC" & cellRowIndex.ToString).Value = CDec(DgvList.Rows(i).Cells("仕入原価_原通貨").Value) '仕入原価_原通貨
                sheet.Range("AA" & cellRowIndex.ToString).Value = CDec(DgvList.Rows(i).Cells("仕入原価_IDR").Value) '仕入原価_IDR

                sheet.Range("AD" & cellRowIndex.ToString).Value = CDec(DgvList.Rows(i).Cells("間接費").Value) '間接費
                sheet.Range("AE" & cellRowIndex.ToString).Value = CDec(DgvList.Rows(i).Cells("利益").Value) '利益
                sheet.Range("AF" & cellRowIndex.ToString).Value = CDec(DgvList.Rows(i).Cells("利益率").Value) '利益率


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
            sheet.Range("D" & cellRowIndex.ToString + 1).NumberFormatLocal = "0.00"
            sheet.Range("D" & cellRowIndex.ToString + 4).Value = CDec(TxtSalesCostAmount.Text).ToString("0.00")
            sheet.Range("D" & cellRowIndex.ToString + 4).NumberFormatLocal = "0.00"

            sheet.Range("F" & cellRowIndex.ToString + 1).Value = CDec(TxtGrossMargin.Text).ToString("0.00")
            sheet.Range("F" & cellRowIndex.ToString + 1).NumberFormatLocal = "0.00"
            sheet.Range("F" & cellRowIndex.ToString + 4).Value = CDec(TxtGrossMarginRate.Text).ToString("0.00")
            sheet.Range("F" & cellRowIndex.ToString + 4).NumberFormatLocal = "0.0"


            ' 境界線オブジェクトを作成 →7行目の下部に罫線を描画する
            xlBorders = xlRngTmp.Borders
            xlBorder = xlBorders(XlBordersIndex.xlEdgeBottom)
            xlBorder.LineStyle = XlLineStyle.xlContinuous

            app.DisplayAlerts = False 'Microsoft Excelのアラート一旦無効化

            Dim excelChk As Boolean = excelOutput(sOutFile)
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
            Throw ex

            'カーソルをビジー状態から元に戻す
            Cursor.Current = Cursors.Default

        Finally
            'app.Quit()
            'Marshal.ReleaseComObject(sheet)
            'Marshal.ReleaseComObject(book)
            'Marshal.ReleaseComObject(app)

        End Try


    End Sub


    'BK
    'Private Sub outputExcel()

    '    '定義
    '    Dim app As Excel.Application = Nothing
    '    Dim book As Excel.Workbook = Nothing
    '    Dim sheet As Excel.Worksheet = Nothing

    '    Dim strSelectYear As String = cmbYear.SelectedValue.ToString()
    '    Dim strSelectMonth As String = cmbMonth.SelectedValue.ToString()

    '    ' セル
    '    Dim xlRngTmp As Range = Nothing
    '    Dim xlRng As Range = Nothing

    '    ' セル境界線（枠）
    '    Dim xlBorders As Borders = Nothing
    '    Dim xlBorder As Border = Nothing

    '    'カーソルをビジー状態にする
    '    Cursor.Current = Cursors.WaitCursor

    '    Try
    '        '雛形パス
    '        Dim sHinaPath As String = StartUp._iniVal.BaseXlsPath
    '        '雛形ファイル名
    '        Dim sHinaFile As String = sHinaPath & "\" & "SalesProfitList.xlsx"
    '        '出力先パス
    '        Dim sOutPath As String = StartUp._iniVal.OutXlsPath
    '        '出力ファイル名
    '        Dim sOutFile As String = sOutPath & "\SalesProfitList_" & DateTime.Now.ToString("yyyyMMddHHmm") & ".xlsx"

    '        app = New Excel.Application()
    '        book = app.Workbooks.Add(sHinaFile)  'テンプレート
    '        sheet = CType(book.Worksheets(1), Excel.Worksheet)

    '        sheet.PageSetup.LeftHeader = "売上・売上原価・利益・利益率一覧表（月次）"
    '        sheet.PageSetup.CenterHeader = strSelectYear & "/" & strSelectMonth
    '        sheet.PageSetup.RightHeader = "OutputDate：" & DateTime.Now.ToShortDateString

    '        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
    '            sheet.PageSetup.LeftHeader = "SalesProfitList（Monthly）"
    '            sheet.PageSetup.CenterHeader = strSelectMonth & "/" & strSelectYear

    '            sheet.Range("A1").Value = "SalesNumber"
    '            sheet.Range("B1").Value = "SalesDate"
    '            sheet.Range("C1").Value = "CustomerName"
    '            sheet.Range("D1").Value = "SalesAmount"
    '            sheet.Range("E1").Value = "ＶＡＴ"
    '            sheet.Range("F1").Value = "CustomerNumber"
    '            sheet.Range("G1").Value = "SalesPersonInCharge"
    '            sheet.Range("H1").Value = "TotalSalesAmount"
    '            sheet.Range("I1").Value = "Overhead"
    '            sheet.Range("J1").Value = "SalesCost"
    '            sheet.Range("K1").Value = "GrossMargin"
    '            sheet.Range("L1").Value = "GrossMarginRate"
    '        End If

    '        Dim cellRowIndex As Integer = 1
    '        For i As Integer = 0 To DgvList.RowCount - 1
    '            cellRowIndex += 1
    '            sheet.Rows(cellRowIndex).Insert
    '            sheet.Range("A" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("売上番号").Value '売上番号
    '            sheet.Range("B" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("売上日").Value '売上日
    '            sheet.Range("C" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("得意先名").Value '得意先
    '            sheet.Range("D" & cellRowIndex.ToString).Value = CDec(DgvList.Rows(i).Cells("売上計").Value) '売上計
    '            sheet.Range("E" & cellRowIndex.ToString).Value = CDec(DgvList.Rows(i).Cells("ＶＡＴ").Value) 'VAT
    '            sheet.Range("F" & cellRowIndex.ToString).Value = CDec(DgvList.Rows(i).Cells("売上金額計").Value) '売上 + VAT
    '            sheet.Range("G" & cellRowIndex.ToString).Value = CDec(DgvList.Rows(i).Cells("間接費").Value) '間接費
    '            sheet.Range("H" & cellRowIndex.ToString).Value = CDec(DgvList.Rows(i).Cells("売上原価計").Value) '売上原価計
    '            sheet.Range("I" & cellRowIndex.ToString).Value = CDec(DgvList.Rows(i).Cells("粗利").Value) '粗利
    '            sheet.Range("J" & cellRowIndex.ToString).Value = CDec(DgvList.Rows(i).Cells("粗利率").Value) '粗利率

    '            sheet.Range("D" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
    '            sheet.Range("E" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
    '            sheet.Range("F" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
    '            sheet.Range("G" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
    '            sheet.Range("H" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
    '            sheet.Range("I" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
    '            sheet.Range("J" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
    '        Next

    '        ' 行7全体のオブジェクトを作成
    '        xlRngTmp = sheet.Rows
    '        xlRng = xlRngTmp(cellRowIndex)

    '        '最後に合計行の追加
    '        cellRowIndex += 1
    '        sheet.Range("D" & cellRowIndex.ToString).Value = CDec(TxtSalesAmount.Text) '売上計
    '        sheet.Range("F" & cellRowIndex.ToString).Value = CDec(TxtTotalSalesAmount.Text) '売上 + VAT
    '        sheet.Range("H" & cellRowIndex.ToString).Value = CDec(TxtSalesCostAmount.Text) '売上原価計
    '        sheet.Range("I" & cellRowIndex.ToString).Value = CDec(TxtGrossMargin.Text) '粗利
    '        sheet.Range("J" & cellRowIndex.ToString).Value = CDec(TxtGrossMarginRate.Text) '粗利率

    '        ' 境界線オブジェクトを作成 →7行目の下部に罫線を描画する
    '        xlBorders = xlRngTmp.Borders
    '        xlBorder = xlBorders(XlBordersIndex.xlEdgeBottom)
    '        xlBorder.LineStyle = XlLineStyle.xlContinuous

    '        app.DisplayAlerts = False 'Microsoft Excelのアラート一旦無効化

    '        Dim excelChk As Boolean = excelOutput(sOutFile)
    '        If excelChk = False Then
    '            Exit Sub
    '        End If
    '        book.SaveAs(sOutFile) '書き込み実行

    '        app.DisplayAlerts = True 'アラート無効化を解除

    '        'カーソルをビジー状態から元に戻す
    '        Cursor.Current = Cursors.Default

    '        app.Visible = True
    '        _msgHd.dspMSG("CreateExcel", frmC01F10_Login.loginValue.Language)

    '    Catch ex As Exception
    '        Throw ex

    '        'カーソルをビジー状態から元に戻す
    '        Cursor.Current = Cursors.Default

    '    Finally
    '        'app.Quit()
    '        'Marshal.ReleaseComObject(sheet)
    '        'Marshal.ReleaseComObject(book)
    '        'Marshal.ReleaseComObject(app)

    '    End Try


    'End Sub


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

    ''' ------------------------------------------------------------------------
    ''' <summary>
    '''     指定した精度の数値に切り捨てします。</summary>
    ''' <param name="dValue">
    '''     丸め対象の倍精度浮動小数点数。</param>
    ''' <param name="iDigits">
    '''     戻り値の有効桁数の精度。</param>
    ''' <returns>
    '''     iDigits に等しい精度の数値に切り捨てられた数値。</returns>
    ''' ------------------------------------------------------------------------
    Public Shared Function ToRoundDown(ByVal dValue As Double, ByVal iDigits As Integer) As Double
        Dim dCoef As Double = System.Math.Pow(10, iDigits)

        If dValue > 0 Then
            Return System.Math.Floor(dValue * dCoef) / dCoef
        Else
            Return System.Math.Ceiling(dValue * dCoef) / dCoef
        End If
    End Function

    ''' <summary>
    ''' コンボボックスに値を設定する
    ''' </summary>
    ''' <param name="combo">値を設定するコンボボックスコントロール</param>
    Private Sub setComboBox(ByVal combo As ComboBox)
        '=========================================
        '初期化
        '=========================================
        'コンボボックスの表示アイテムをクリア
        combo.Items.Clear()
        combo.DisplayMember = "Key" '表示値としてDataSourceの'Key'を利用
        combo.ValueMember = "Value" '値としてDataSourceの'Value'を利用

        '=========================================
        '設定するデータソースの準備
        '=========================================
        Dim dic As New Dictionary(Of String, Integer)

        Dim dtToday As DateTime = DateTime.Today

        If combo.Items.Count() = 0 Then
            If combo.Name = "cmbYear" Then
                Dim nowDate As Integer = Integer.Parse(dtToday.Year)
                For i As Integer = CommonConst.SINCE_DEFAULT_YEAR To nowDate
                    dic(i.ToString) = i  '表示値 = 値
                Next
            Else
                For i As Integer = 1 To 12
                    dic(i.ToString) = i  '表示値 = 値
                Next
            End If
        End If
        ''ダミー行が欲しい場合(未選択時の空白とか)は以下の様に入れとくと便利
        'dic("") = -1 '表示値:空白(未設定) => 値:-1

        '=========================================
        'データソースをコンボボックスへ設定
        '=========================================
        combo.DataSource = New BindingSource(dic, Nothing)
    End Sub

    'コンボボックスを変更したら
    Private Sub cmbYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbYear.TextChanged
        If cmbYear.Items.Count() <> 0 And cmbMonth.Items.Count() <> 0 Then
            getList() '一覧再表示
        End If
    End Sub

    'コンボボックスを変更したら
    Private Sub cmbMonth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMonth.TextChanged
        If cmbYear.Items.Count() <> 0 And cmbMonth.Items.Count() <> 0 Then
            getList() '一覧再表示
        End If
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

End Class