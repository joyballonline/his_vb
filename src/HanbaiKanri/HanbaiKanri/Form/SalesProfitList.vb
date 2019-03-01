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
                   ByRef prmRefStatus As String)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
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

            DgvList.Columns("売上番号").HeaderText = "SalesNumber"
            DgvList.Columns("売上日").HeaderText = "SalesDate"
            DgvList.Columns("得意先名").HeaderText = "CustomerName"
            DgvList.Columns("売上計").HeaderText = "SalesAmount"
            DgvList.Columns("ＶＡＴ").HeaderText = "ＶＡＴ"
            DgvList.Columns("売上金額計").HeaderText = "SalesAmount+ＶＡＴ"
            DgvList.Columns("間接費").HeaderText = "Overhead"
            DgvList.Columns("売上原価計").HeaderText = "SalesCost"
            DgvList.Columns("粗利").HeaderText = "GrossMargin"
            DgvList.Columns("粗利率").HeaderText = "GrossMarginRate"


            Label8.Text = "SalesDate"
            BtnExcelOutput.Text = "SalesDataView"
            BtnBack.Text = "Back"
            LblMonth.Text = "Month"
            LblYear.Text = "Year"

            LblSalesAmount.Text = "SalesAmount"
            LblTotalSalesAmount.Text = "TotalSalesAmount"
            LblSalesCostAmount.Text = "SalesCost"
            LblGrossMargin.Text = "GrossMargin"
            LblGrossMarginRate.Text = "GrossMarginRate"

        End If

        setComboBox(cmbYear) 'コンボボックスに年を設定
        setComboBox(cmbMonth) 'コンボボックスに月を設定
        cmbYear.SelectedValue = Integer.Parse(Format(System.DateTime.Now, "yyyy"))
        cmbMonth.SelectedValue = Integer.Parse(Format(System.DateTime.Now, "MM"))

        getList() '一覧表示

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

        'Dim salesAmount As Decimal = 0
        'Dim totalSalesAmount As Decimal = 0
        'Dim salesCostAmount As Decimal = 0
        'Dim grossMargin As Decimal = 0
        'Dim grossMarginRate As Decimal = 0

        DgvList.Rows.Clear()



        Sql = "SELECT t30.売上番号,t30.売上日,t30.得意先名,t30.売上金額,t30.ＶＡＴ,t30.仕入金額,t30.粗利額, t31.受注数量, t31.仕入値"
        Sql += " ,sum(t31.粗利率) as 粗利率,sum(t31.間接費) as 間接費, sum(t31.売上数量) as 売上数量, t31.売単価, t31.仕入原価, t31.関税率, t31.輸送費率, t31.前払法人税率, t31.関税額"
        Sql += " FROM  public.t30_urighd t30 "
        Sql += " INNER JOIN  t31_urigdt t31"
        Sql += " ON t30.会社コード = t31.会社コード"
        Sql += " And  t30.売上番号 = t31.売上番号"

        Sql += " WHERE t30.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND "
        Sql += " t30.売上日 >= '" & strFormatDate(uriDateSince) & "'"
        Sql += " AND "
        Sql += " t30.売上日 <= '" & strFormatDate(uriDateUntil) & "'"
        Sql += " AND "
        Sql += "t30.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '取消区分=0
        Sql += " GROUP BY t31.受注数量,t30.売上番号,t30.売上日,t30.得意先名,t30.売上金額,t30.ＶＡＴ,t30.仕入金額,t30.粗利額,粗利率,間接費,t30.更新日,売上数量, t31.仕入原価, t31.売単価, t31.関税率, t31.仕入値, t31.輸送費率, t31.前払法人税率, t31.関税額"
        Sql += " ORDER BY t30.更新日 DESC "

        Try

            Dim totalSales As Decimal = 0
            Dim totalSalesAmount As Decimal = 0
            Dim salesUnitPrice As Decimal = 0
            Dim totalArari As Decimal = 0
            Dim totalArariRate As Decimal = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)


            For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                '売上計
                Dim salesAmount As Decimal = ds.Tables(RS).Rows(i)("売上数量") * ds.Tables(RS).Rows(i)("売単価")
                'VAT
                Dim vatAmount As Decimal = Format((salesAmount * ds.Tables(RS).Rows(i)("ＶＡＴ")) / 100, "0.000")
                '仕入原価
                Dim sireGenka As Decimal = ds.Tables(RS).Rows(i)("仕入値") * ds.Tables(RS).Rows(i)("売上数量")

                '関税額
                Dim kanzeiGaku As Decimal = sireGenka * ds.Tables(RS).Rows(i)("関税率")
                '前払法人税額
                Dim maehoujinzeiGaku As Decimal = (sireGenka + kanzeiGaku) * ds.Tables(RS).Rows(i)("前払法人税率")
                '輸送費額
                Dim yusouhiGaku As Decimal = sireGenka * ds.Tables(RS).Rows(i)("輸送費率")

                '間接費
                Dim kansetuhi As Decimal = (kanzeiGaku + maehoujinzeiGaku + yusouhiGaku)

                Dim sireKingaku As Decimal = sireGenka + kansetuhi
                Dim arariAmount As Decimal = Math.Truncate(salesAmount + vatAmount - sireGenka)


                DgvList.Rows.Add()
                DgvList.Rows(i).Cells("売上番号").Value = ds.Tables(RS).Rows(i)("売上番号")
                DgvList.Rows(i).Cells("売上日").Value = ds.Tables(RS).Rows(i)("売上日").ToShortDateString()
                DgvList.Rows(i).Cells("得意先名").Value = ds.Tables(RS).Rows(i)("得意先名")
                DgvList.Rows(i).Cells("売上計").Value = salesAmount
                DgvList.Rows(i).Cells("ＶＡＴ").Value = vatAmount
                DgvList.Rows(i).Cells("売上金額計").Value = salesAmount + vatAmount

                DgvList.Rows(i).Cells("間接費").Value = Format((sireKingaku - sireGenka), "0.000")
                DgvList.Rows(i).Cells("売上原価計").Value = Format(salesAmount + vatAmount - kansetuhi, "0.000")
                DgvList.Rows(i).Cells("粗利").Value = arariAmount
                DgvList.Rows(i).Cells("粗利率").Value = 1 - (ds.Tables(RS).Rows(i)("仕入値") / ds.Tables(RS).Rows(i)("売単価"))

                totalSales += salesAmount
                totalSalesAmount += salesAmount + vatAmount
                salesUnitPrice += Format(salesAmount + vatAmount - kansetuhi, "0.000")
                totalArari += arariAmount
            Next
            If totalArari <> 0 And totalSalesAmount <> 0 Then
                totalArariRate = Format((totalArari / totalSalesAmount), "0.000")
            Else
                totalArariRate = 0
            End If

            TxtSalesAmount.Text = IIf(
                totalSales <> 0,
                totalSales,
                0
            )
            TxtTotalSalesAmount.Text = IIf(
                totalSalesAmount <> 0,
                totalSalesAmount,
                0
            )
            TxtSalesCostAmount.Text = IIf(
                salesUnitPrice <> 0,
                Format(salesUnitPrice, "0.000"),
                0
            )
            TxtGrossMargin.Text = IIf(
                totalArari <> 0,
                totalArari,
                0
            )
            TxtGrossMarginRate.Text = IIf(
                totalArariRate <> 0,
                totalArariRate,
                0
            )

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
        Dim openForm As Form = Nothing
        openForm = New frmC01F30_Menu(_msgHd, _langHd, _db)
        openForm.Show()
        Me.Close()
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

            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                sheet.PageSetup.RightHeader = "OutputDate：" & DateTime.Now.ToShortDateString

                sheet.Range("A1").Value = "SalesNumber"
                sheet.Range("B1").Value = "SalesDate"
                sheet.Range("C1").Value = "CustomerName"
                sheet.Range("D1").Value = "SalesAmount"
                sheet.Range("E1").Value = "ＶＡＴ"
                sheet.Range("F1").Value = "TotalSalesAmount"
                sheet.Range("G1").Value = "Overhead"
                sheet.Range("H1").Value = "SalesCost"
                sheet.Range("I1").Value = "GrossMargin"
                sheet.Range("J1").Value = "GrossMarginRate"
            End If

            Dim cellRowIndex As Integer = 2
            For i As Integer = 0 To DgvList.RowCount - 1
                cellRowIndex += i
                sheet.Rows(cellRowIndex).Insert
                sheet.Range("A" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("売上番号").Value '売上番号
                sheet.Range("B" & cellRowIndex.ToString).Value = strFormatDate(DgvList.Rows(i).Cells("売上日").Value) '売上日
                sheet.Range("C" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("得意先名").Value '得意先
                sheet.Range("D" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("売上計").Value '売上計
                sheet.Range("E" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("ＶＡＴ").Value 'VAT
                sheet.Range("F" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("売上金額計").Value '売上 + VAT
                sheet.Range("G" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("間接費").Value '間接費
                sheet.Range("H" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("売上原価計").Value '売上原価計
                sheet.Range("I" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("粗利").Value '粗利
                sheet.Range("J" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("粗利率").Value '粗利率

                sheet.Range("D" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
                sheet.Range("E" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
                sheet.Range("F" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
                sheet.Range("G" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
                sheet.Range("H" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
                sheet.Range("I" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
                sheet.Range("J" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
            Next

            ' 行7全体のオブジェクトを作成
            xlRngTmp = sheet.Rows
            xlRng = xlRngTmp(cellRowIndex)

            '最後に合計行の追加
            cellRowIndex += 1
            sheet.Range("D" & cellRowIndex.ToString).Value = TxtSalesAmount.Text '売上計
            sheet.Range("F" & cellRowIndex.ToString).Value = TxtTotalSalesAmount.Text '売上 + VAT
            sheet.Range("H" & cellRowIndex.ToString).Value = TxtSalesCostAmount.Text '売上原価計
            sheet.Range("I" & cellRowIndex.ToString).Value = TxtGrossMargin.Text '粗利
            sheet.Range("J" & cellRowIndex.ToString).Value = TxtGrossMarginRate.Text '粗利率

            ' 境界線オブジェクトを作成 →7行目の下部に罫線を描画する
            xlBorders = xlRngTmp.Borders
            xlBorder = xlBorders(XlBordersIndex.xlEdgeBottom)
            xlBorder.LineStyle = XlLineStyle.xlContinuous

            book.SaveAs(sOutFile)
            app.Visible = True

            _msgHd.dspMSG("CreateExcel", frmC01F10_Login.loginValue.Language)

        Catch ex As Exception
            Throw ex


        Finally
            'app.Quit()
            'Marshal.ReleaseComObject(sheet)
            'Marshal.ReleaseComObject(book)
            'Marshal.ReleaseComObject(app)

        End Try


    End Sub

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

    'どんなカルチャーであっても、日本の形式に変換する
    Private Function strFormatDate(ByVal prmDate As String, Optional ByRef prmFormat As String = "yyyy/MM/dd") As String

        'PCのカルチャーを取得し、それに応じてStringからDatetimeを作成
        Dim ci As New System.Globalization.CultureInfo(CultureInfo.CurrentCulture.Name.ToString)
        Dim dateFormat As DateTime = DateTime.Parse(prmDate, ci, System.Globalization.DateTimeStyles.AssumeLocal)

        '日本の形式に書き換える
        Return dateFormat.ToString(prmFormat)
    End Function

    'どんなカルチャーであっても、日本の形式に変換する
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

End Class