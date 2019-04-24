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

Public Class PurchaseStockAmountList
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

            DgvList.Columns("仕入番号").HeaderText = "PurchaseNumber"
            DgvList.Columns("仕入日").HeaderText = "PurchaseDate"
            DgvList.Columns("仕入先名").HeaderText = "SupplierName"
            DgvList.Columns("メーカー").HeaderText = "Manufacturer"
            DgvList.Columns("品名").HeaderText = "ItemName"
            DgvList.Columns("型式").HeaderText = "Spec"
            DgvList.Columns("数量").HeaderText = "PurchasedQuantity"
            DgvList.Columns("単位").HeaderText = "Unit"
            DgvList.Columns("仕入単価").HeaderText = "PurchaseUnitPrice"
            DgvList.Columns("ＶＡＴ").HeaderText = "ＶＡＴ"
            DgvList.Columns("間接費").HeaderText = "Overhead"
            DgvList.Columns("仕入計").HeaderText = "PurchaseAmount"

            Label8.Text = "SalesDate"
            BtnExcelOutput.Text = "ExcelOutput"
            BtnBack.Text = "Back"
            LblMonth.Text = "Month"
            LblYear.Text = "Year"

            LblTotal.Text = "Total"

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


        DgvList.Rows.Clear()

        Sql = "SELECT t40.仕入日,t40.取消区分,t41.仕入番号,t41.仕入先名,t41.メーカー,t41.品名,t41.型式, t41.仕入数量, t41.単位"
        Sql += ", t41.仕入値, t40.ＶＡＴ, t41.間接費, t41.仕入金額"
        Sql += " FROM  public.t41_siredt t41 "
        Sql += " INNER JOIN  t40_sirehd t40"
        Sql += " ON t41.会社コード = t40.会社コード"
        Sql += " And  t41.仕入番号 = t40.仕入番号"

        Sql += " WHERE t40.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND "
        Sql += " t40.仕入日 >= '" & UtilClass.strFormatDate(uriDateSince) & "'"
        Sql += " AND "
        Sql += " t40.仕入日 <= '" & UtilClass.strFormatDate(uriDateUntil) & "'"
        Sql += " AND "
        Sql += "t40.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '取消区分=0
        Sql += " ORDER BY t40.仕入日 "

        Try

            Dim totalAmount As Decimal = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1

                'VAT
                Dim calVAT As Decimal = (ds.Tables(RS).Rows(i)("仕入値") * ds.Tables(RS).Rows(i)("ＶＡＴ")) / 100

                DgvList.Rows.Add()
                DgvList.Rows(i).Cells("仕入番号").Value = ds.Tables(RS).Rows(i)("仕入番号")
                DgvList.Rows(i).Cells("仕入日").Value = ds.Tables(RS).Rows(i)("仕入日").ToShortDateString()
                DgvList.Rows(i).Cells("仕入先名").Value = ds.Tables(RS).Rows(i)("仕入先名")
                DgvList.Rows(i).Cells("メーカー").Value = ds.Tables(RS).Rows(i)("メーカー")
                DgvList.Rows(i).Cells("品名").Value = ds.Tables(RS).Rows(i)("品名")
                DgvList.Rows(i).Cells("型式").Value = ds.Tables(RS).Rows(i)("型式")
                DgvList.Rows(i).Cells("数量").Value = ds.Tables(RS).Rows(i)("仕入数量")
                DgvList.Rows(i).Cells("単位").Value = ds.Tables(RS).Rows(i)("単位")
                DgvList.Rows(i).Cells("仕入単価").Value = Format(ds.Tables(RS).Rows(i)("仕入値"), "0.000")
                DgvList.Rows(i).Cells("ＶＡＴ").Value = Format(calVAT, "0.000")
                DgvList.Rows(i).Cells("間接費").Value = Format(ds.Tables(RS).Rows(i)("間接費"), "0.000")
                DgvList.Rows(i).Cells("仕入計").Value = Format((ds.Tables(RS).Rows(i)("仕入値") + calVAT + ds.Tables(RS).Rows(i)("間接費")) * ds.Tables(RS).Rows(i)("仕入数量"), "0.000")
                totalAmount += Format((ds.Tables(RS).Rows(i)("仕入値") + calVAT + ds.Tables(RS).Rows(i)("間接費")) * ds.Tables(RS).Rows(i)("仕入数量"), "0.000")

            Next

            TxtTotal.Text = totalAmount

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
            Dim sHinaFile As String = sHinaPath & "\" & "PurchaseStockAmountList.xlsx"
            '出力先パス
            Dim sOutPath As String = StartUp._iniVal.OutXlsPath
            '出力ファイル名
            Dim sOutFile As String = sOutPath & "\PurchaseStockAmountList_" & DateTime.Now.ToString("yyyyMMddHHmm") & ".xlsx"

            app = New Excel.Application()
            book = app.Workbooks.Add(sHinaFile)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet)

            sheet.PageSetup.LeftHeader = "当月購入在庫金額・VAT一覧表（月次）"
            sheet.PageSetup.CenterHeader = strSelectYear & "/" & strSelectMonth
            sheet.PageSetup.RightHeader = "OutputDate：" & DateTime.Now.ToShortDateString

            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                sheet.PageSetup.LeftHeader = "PurchaseStockAmountList（Monthly）"
                sheet.PageSetup.CenterHeader = strSelectMonth & "/" & strSelectYear

                sheet.Range("A1").Value = "PurchaseNumber"
                sheet.Range("B1").Value = "PurchaseDate"
                sheet.Range("C1").Value = "SupplierName"
                sheet.Range("D1").Value = "Manufacturer"
                sheet.Range("E1").Value = "ItemName"
                sheet.Range("F1").Value = "Spec"
                sheet.Range("G1").Value = "PurchasedQuantity"
                sheet.Range("H1").Value = "Unit"
                sheet.Range("I1").Value = "PurchaseUnitPrice"
                sheet.Range("J1").Value = "ＶＡＴ"
                sheet.Range("K1").Value = "Overhead"
                sheet.Range("L1").Value = "PurchaseAmount"

            End If

            Dim cellRowIndex As Integer = 1
            For i As Integer = 0 To DgvList.RowCount - 1
                cellRowIndex += 1
                sheet.Rows(cellRowIndex).Insert
                sheet.Range("A" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("仕入番号").Value '仕入番号
                sheet.Range("B" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("仕入日").Value '仕入日
                sheet.Range("C" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("仕入先名").Value '仕入先名
                sheet.Range("D" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("メーカー").Value 'メーカー
                sheet.Range("E" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("品名").Value '品名
                sheet.Range("F" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("型式").Value '型式
                sheet.Range("G" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("数量").Value '数量
                sheet.Range("H" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("単位").Value '単位
                sheet.Range("I" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("仕入単価").Value '仕入単価
                sheet.Range("J" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("ＶＡＴ").Value 'ＶＡＴ
                sheet.Range("K" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("間接費").Value '間接費
                sheet.Range("L" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("仕入計").Value '仕入計

                sheet.Range("G" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
                sheet.Range("I" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
                sheet.Range("J" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
                sheet.Range("K" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
                sheet.Range("L" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight
            Next

            'border描画
            sheet.Range("A" & cellRowIndex.ToString & ":L" & cellRowIndex.ToString).Borders(XlBordersIndex.xlEdgeBottom).LineStyle = XlLineStyle.xlDouble

            '最後に合計行の追加
            cellRowIndex += 1
            sheet.Range("L" & cellRowIndex.ToString).Value = TxtTotal.Text '仕入計
            sheet.Range("L" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight


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