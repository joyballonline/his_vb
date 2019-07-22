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
Imports System.IO

Public Class SalesVATList
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

            DgvList.Columns("売上番号").HeaderText = "SalesNumber"
            DgvList.Columns("売上日").HeaderText = "SalesDate"
            DgvList.Columns("得意先名").HeaderText = "CustomerName"
            DgvList.Columns("客先番号").HeaderText = "CustomerNumber"
            DgvList.Columns("営業担当者").HeaderText = "SalesPersonInCharge"
            DgvList.Columns("メーカー").HeaderText = "Manufacturer"
            DgvList.Columns("品名").HeaderText = "ItemName"
            DgvList.Columns("型式").HeaderText = "Spec"
            DgvList.Columns("数量").HeaderText = "Quantity"
            DgvList.Columns("単位").HeaderText = "Unit"
            DgvList.Columns("売単価").HeaderText = "SellingPrice"
            DgvList.Columns("ＶＡＴ").HeaderText = "ＶＡＴ"
            DgvList.Columns("売上計").HeaderText = "TotalAmount"

            LblMode.Text = SalesStatus & " Mode"

            Label8.Text = "SalesDate"
            BtnExcelOutput.Text = "ExcelOutput"
            BtnBack.Text = "Back"
            LblMonth.Text = "Month"
            LblYear.Text = "Year"

            LblSalesAmount.Text = "SalesAmount"

        End If

        '数字形式
        DgvList.Columns("数量").DefaultCellStyle.Format = "N2"
        DgvList.Columns("売単価").DefaultCellStyle.Format = "N2"
        'DgvList.Columns("ＶＡＴ").DefaultCellStyle.Format = "N0"
        DgvList.Columns("売上計").DefaultCellStyle.Format = "N0"


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

        DgvList.Rows.Clear() '一覧クリア

        Sql = "SELECT "
        Sql += "t30.売上番号, t30.売上日, t30.得意先名, t31.メーカー, t31.品名, t31.型式"
        Sql += ", t31.売上数量, t31.単位, t31.売単価, t31.見積単価, t30.ＶＡＴ, t30.客先番号, t30.営業担当者 "
        Sql += " FROM  public.t30_urighd t30 "
        Sql += " INNER JOIN  t31_urigdt t31"
        Sql += " ON t30.会社コード = t31.会社コード"
        Sql += " And  t30.売上番号 = t31.売上番号"
        Sql += " And  t30.売上番号枝番 = t31.売上番号枝番"

        Sql += " WHERE t30.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND "
        Sql += " t30.売上日 >= '" & strFormatDate(uriDateSince) & "'"
        Sql += " AND "
        Sql += " t30.売上日 <= '" & strFormatDate(uriDateUntil) & "'"
        Sql += " AND "
        Sql += "t30.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '取消区分=0
        Sql += " ORDER BY t30.更新日 DESC "

        Try

            Dim totalSalesAmount As Decimal = 0

            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                'VAT
                'Dim vatAmount As Decimal = Format((ds.Tables(RS).Rows(i)("見積単価") * ds.Tables(RS).Rows(i)("ＶＡＴ")) / 100, "0.000")
                '売上計
                Dim salesAmount As Decimal = ds.Tables(RS).Rows(i)("売上数量") * ds.Tables(RS).Rows(i)("売単価")

                DgvList.Rows.Add()
                DgvList.Rows(i).Cells("売上番号").Value = ds.Tables(RS).Rows(i)("売上番号")
                DgvList.Rows(i).Cells("売上日").Value = ds.Tables(RS).Rows(i)("売上日").ToShortDateString()
                DgvList.Rows(i).Cells("得意先名").Value = ds.Tables(RS).Rows(i)("得意先名")
                DgvList.Rows(i).Cells("客先番号").Value = ds.Tables(RS).Rows(i)("客先番号")
                DgvList.Rows(i).Cells("営業担当者").Value = ds.Tables(RS).Rows(i)("営業担当者")
                DgvList.Rows(i).Cells("メーカー").Value = ds.Tables(RS).Rows(i)("メーカー")
                DgvList.Rows(i).Cells("品名").Value = ds.Tables(RS).Rows(i)("品名")
                DgvList.Rows(i).Cells("型式").Value = ds.Tables(RS).Rows(i)("型式")
                DgvList.Rows(i).Cells("数量").Value = ds.Tables(RS).Rows(i)("売上数量")
                DgvList.Rows(i).Cells("単位").Value = ds.Tables(RS).Rows(i)("単位")
                DgvList.Rows(i).Cells("売単価").Value = ds.Tables(RS).Rows(i)("見積単価")
                DgvList.Rows(i).Cells("ＶＡＴ").Value = ds.Tables(RS).Rows(i)("ＶＡＴ")
                DgvList.Rows(i).Cells("売上計").Value = (ds.Tables(RS).Rows(i)("見積単価") + ds.Tables(RS).Rows(i)("ＶＡＴ")) * ds.Tables(RS).Rows(i)("売上数量")

                totalSalesAmount += (ds.Tables(RS).Rows(i)("見積単価") + ds.Tables(RS).Rows(i)("ＶＡＴ")) * ds.Tables(RS).Rows(i)("売上数量")
            Next

            TxtSalesAmount.Text = totalSalesAmount.ToString("N0")

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
            Dim sHinaFile As String = sHinaPath & "\" & "SalesVATList.xlsx"
            '出力先パス
            Dim sOutPath As String = StartUp._iniVal.OutXlsPath
            '出力ファイル名
            Dim sOutFile As String = sOutPath & "\SalesVATList_" & DateTime.Now.ToString("yyyyMMddHHmm") & ".xlsx"

            app = New Excel.Application()
            book = app.Workbooks.Add(sHinaFile)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet)

            sheet.PageSetup.LeftHeader = "売上金・ＶＡＴ一覧表（月次）"
            sheet.PageSetup.CenterHeader = strSelectYear & "/" & strSelectMonth
            sheet.PageSetup.RightHeader = "OutputDate：" & DateTime.Now.ToShortDateString

            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                sheet.PageSetup.LeftHeader = "Sales amount· VAT list（Monthly）"
                sheet.PageSetup.CenterHeader = strSelectMonth & "/" & strSelectYear

                sheet.Range("A1").Value = "SalesNumber"
                sheet.Range("B1").Value = "SalesDate"
                sheet.Range("C1").Value = "CustomerName"
                sheet.Range("D1").Value = "CustomerNumber"
                sheet.Range("E1").Value = "SalesPersonInCharge"
                sheet.Range("F1").Value = "Manufacturer"
                sheet.Range("G1").Value = "ItemName"
                sheet.Range("H1").Value = "Spec"
                sheet.Range("I1").Value = "Quantity"
                sheet.Range("J1").Value = "Unit"
                sheet.Range("K1").Value = "UnitPrice"
                sheet.Range("L1").Value = "ＶＡＴ"
                sheet.Range("M1").Value = "Amount"
            End If

            Dim cellRowIndex As Integer = 1

            For i As Integer = 0 To DgvList.RowCount - 1

                cellRowIndex += 1
                sheet.Rows(cellRowIndex).Insert
                sheet.Range("A" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("売上番号").Value '売上番号
                sheet.Range("B" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("売上日").Value '売上日
                sheet.Range("C" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("得意先名").Value '得意先
                sheet.Range("D" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("客先番号").Value '客先番号
                sheet.Range("E" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("営業担当者").Value '営業担当者
                sheet.Range("F" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("メーカー").Value 'メーカー
                sheet.Range("G" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("品名").Value '品名
                sheet.Range("H" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("型式").Value '型式
                sheet.Range("I" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("数量").Value '数量
                sheet.Range("J" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("単位").Value '単位
                sheet.Range("K" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("売単価").Value '売単価
                sheet.Range("L" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("ＶＡＴ").Value 'ＶＡＴ
                sheet.Range("M" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("売上計").Value '売上金額計

            Next

            ' 行7全体のオブジェクトを作成
            xlRngTmp = sheet.Rows
            xlRng = xlRngTmp(cellRowIndex)

            '最後に合計行の追加
            cellRowIndex += 1
            sheet.Range("M" & cellRowIndex.ToString).Value = TxtSalesAmount.Text '粗利率

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

            app.Visible = True

            'カーソルをビジー状態から元に戻す
            Cursor.Current = Cursors.Default

            app.Visible = True

            _msgHd.dspMSG("CreateExcel", frmC01F10_Login.loginValue.Language)

        Catch ex As Exception
            'カーソルをビジー状態から元に戻す
            Cursor.Current = Cursors.Default

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

End Class