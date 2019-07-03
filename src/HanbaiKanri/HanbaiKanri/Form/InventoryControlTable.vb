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
'Imports Microsoft.Office.Interop
'Imports Microsoft.Office.Interop.Excel
Imports System.IO

Public Class InventoryControlTable
    Inherits System.Windows.Forms.Form

    '------------------------------------------------------------------------------------------------------
    'メンバー定数宣言
    '------------------------------------------------------------------------------------------------------
    'PG制御文字 
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
    Private OrderStatus As String = ""


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
        OrderStatus = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True
        DgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells
    End Sub

    '画面表示時
    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        LblMode.Text = "参照モード"

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblMode.Text = OrderStatus & " Mode"

            BtnExcelOutput.Text = "ExcelOutput"
            BtnBack.Text = "Back"

            'DgvCymndt.Columns("得意先名").HeaderText = "CustomerName"
            'DgvCymndt.Columns("請求番号").HeaderText = "InvoiceNumber"
            'DgvCymndt.Columns("請求日").HeaderText = "BillingDate"
            'DgvCymndt.Columns("請求金額").HeaderText = "TotalBillingAmount"
            'DgvCymndt.Columns("入金額").HeaderText = "MoneyReceiptAmount"
            'DgvCymndt.Columns("売掛金残高").HeaderText = "ARBalance"
            'DgvCymndt.Columns("備考").HeaderText = "Remarks"

        End If

        createWarehouseCombobox(CmWarehouseFrom)
        DtpMovingDayFrom.Text = New DateTime(Today.Year, Today.Month, 1)
        DtpMovingDayTo.Text = New DateTime(Today.Year, Today.Month, DateTime.DaysInMonth(Today.Year, Today.Month))
        createStorageType(CmStorageTypeFrom)
        createStorageType(CmStorageTypeTo)
        CmStorageTypeTo.SelectedIndex = CmStorageTypeTo.Items.Count() - 1

        AddHandler CmWarehouseFrom.SelectedIndexChanged, AddressOf CmWarehouseFrom_SelectedIndexChanged
        AddHandler DtpMovingDayFrom.ValueChanged, AddressOf DtpMovingDayFrom_ValueChanged
        AddHandler DtpMovingDayTo.ValueChanged, AddressOf DtpMovingDayTo_ValueChanged
        AddHandler CmStorageTypeFrom.SelectedIndexChanged, AddressOf CmStorageTypeFrom_SelectedIndexChanged
        AddHandler CmStorageTypeTo.SelectedIndexChanged, AddressOf CmStorageTypeTo_SelectedIndexChanged

        'getList() 'データ取得・表示
        setList() 'データ取得・表示
    End Sub

    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    'excel出力ボタン押下時
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

    '商品別の見出しセット
    Private Sub setListHd()

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            DgvList.Columns.Add("倉庫", "Manufacturer")
            DgvList.Columns.Add("メーカー", "Manufacturer")
            DgvList.Columns.Add("品名", "ItemName")
            DgvList.Columns.Add("型式", "Spec")
            DgvList.Columns.Add("伝票番号", "SlipNumber")
            DgvList.Columns.Add("入出庫種別", "StorageType")
            DgvList.Columns.Add("入出庫日", "InOutDate")
            DgvList.Columns.Add("取引先", "Suppliers")
            DgvList.Columns.Add("入庫数量", "GoodsReceiptQuantity")
            DgvList.Columns.Add("入庫単価", "ReceiptUnitPrice")
            DgvList.Columns.Add("出庫数量", "GoodsIssueQuantity")
            DgvList.Columns.Add("出庫単価", "DeliveryUnitPrice")
            DgvList.Columns.Add("在庫数", "StocksQuantity")
            DgvList.Columns.Add("備考", "Remarks")
        Else
            DgvList.Columns.Add("倉庫", "倉庫")
            DgvList.Columns.Add("メーカー", "メーカー")
            DgvList.Columns.Add("品名", "品名")
            DgvList.Columns.Add("型式", "型式")
            DgvList.Columns.Add("伝票番号", "伝票番号")
            DgvList.Columns.Add("入出庫種別", "入出庫種別")
            DgvList.Columns.Add("入出庫日", "入出庫日")
            DgvList.Columns.Add("取引先", "取引先")
            DgvList.Columns.Add("入庫数量", "入庫数量")
            DgvList.Columns.Add("入庫単価", "入庫単価")
            DgvList.Columns.Add("出庫数量", "出庫数量")
            DgvList.Columns.Add("出庫単価", "出庫単価")
            DgvList.Columns.Add("在庫数", "在庫数")
            DgvList.Columns.Add("備考", "備考")

            DgvList.Columns("入庫数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvList.Columns("入庫単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvList.Columns("出庫数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvList.Columns("出庫単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvList.Columns("在庫数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If

    End Sub

    'excel出力処理
    Private Sub outputExcel()

        '定義
        Dim app As Microsoft.Office.Interop.Excel.Application = Nothing
        Dim book As Microsoft.Office.Interop.Excel.Workbook = Nothing
        Dim sheet As Microsoft.Office.Interop.Excel.Worksheet = Nothing

        'カーソルをビジー状態にする
        Cursor.Current = Cursors.WaitCursor

        Try
            '雛形パス
            Dim sHinaPath As String = StartUp._iniVal.BaseXlsPath
            '雛形ファイル名
            Dim sHinaFile As String = sHinaPath & "\" & "InventoryControlTable.xlsx"
            '出力先パス
            Dim sOutPath As String = StartUp._iniVal.OutXlsPath
            '出力ファイル名
            Dim sOutFile As String = sOutPath & "\InventoryControlTable_" & DateTime.Now.ToString("yyyyMMddHHmm") & ".xlsx"

            app = New Microsoft.Office.Interop.Excel.Application()
            book = app.Workbooks.Add(sHinaFile)  'テンプレート
            sheet = CType(book.Worksheets(1), Microsoft.Office.Interop.Excel.Worksheet)

            sheet.PageSetup.RightHeader = "出力日：" & DateTime.Now.ToShortDateString

            app.DisplayAlerts = False 'セル結合のアラートが出るので一旦無効化

            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                sheet.PageSetup.LeftHeader = "Inventory control table"
                sheet.PageSetup.RightHeader = "OutputDate：" & DateTime.Now.ToShortDateString

                sheet.Range("A1").Value = "Manufacturer"
                sheet.Range("C1").Value = "ItemName"
                sheet.Range("F1").Value = "Spec"

            End If

            Dim cellRowIndex As Integer = 1
            For i As Integer = 0 To DgvList.RowCount - 1

                cellRowIndex += 1

                If DgvList.Rows(i).Cells("メーカー").Value <> "" Then

                    'sheet.Range("A" & cellRowIndex.ToString, "I" & cellRowIndex.ToString).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDouble
                    sheet.Range("A" & cellRowIndex.ToString, "J" & cellRowIndex.ToString).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDouble

                    'メーカー、品名、型式 の列結合
                    sheet.Range("A" & cellRowIndex.ToString, "B" & cellRowIndex.ToString).Merge() 'メーカー
                    sheet.Range("C" & cellRowIndex.ToString, "D" & cellRowIndex.ToString).Merge() '品名
                    sheet.Range("F" & cellRowIndex.ToString, "H" & cellRowIndex.ToString).Merge() '型式

                    sheet.Range("A" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("メーカー").Value
                    sheet.Range("C" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("品名").Value
                    sheet.Range("F" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("型式").Value

                    sheet.Range("A" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                    sheet.Range("C" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                    sheet.Range("F" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft

                    'sheet.Range("A" & cellRowIndex.ToString, "I" & cellRowIndex.ToString).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDouble
                    sheet.Range("A" & cellRowIndex.ToString, "J" & cellRowIndex.ToString).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDouble

                    cellRowIndex += 1 '次の行

                    sheet.Range("C" & cellRowIndex.ToString, "D" & cellRowIndex.ToString).Merge() '入庫見出し
                    sheet.Range("E" & cellRowIndex.ToString, "F" & cellRowIndex.ToString).Merge() '出庫見出し
                    sheet.Range("C" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "GoodsReceipt", "入庫")
                    sheet.Range("E" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "GoodsDelivery", "出庫")
                    sheet.Range("C" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                    sheet.Range("E" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                    cellRowIndex += 1 '次の行

                    sheet.Range("C" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                    sheet.Range("D" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                    sheet.Range("E" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                    sheet.Range("F" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                    sheet.Range("G" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft

                    '年月日、取引先、入庫（数量、単価）、出庫（数量、単価）、在庫数、備考

                    sheet.Range("A" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "Date", "入出庫日")
                    sheet.Range("B" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "Suppliers", "取引先")
                    sheet.Range("C" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "Quantity", "数量")
                    sheet.Range("D" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "UnitPrice", "単価")
                    sheet.Range("E" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "Quantity", "数量")
                    sheet.Range("F" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "UnitPrice", "単価")
                    sheet.Range("G" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "StockQuantity", "在庫数")
                    sheet.Range("H" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "InOutClassification", "入出庫種別")
                    'sheet.Range("I" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "Remarks", "備考")
                    sheet.Range("I" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "Warehouse", "倉庫")
                    sheet.Range("J" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "SlipNumber", "伝票番号")

                    'sheet.Range("A" & cellRowIndex.ToString, "I" & cellRowIndex.ToString).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous　　　　　'底の罫線
                    sheet.Range("A" & cellRowIndex.ToString, "J" & cellRowIndex.ToString).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous　　　　　'底の罫線

                    cellRowIndex += 1 '次の行

                End If

                '年月日、取引先、入庫（数量、単価）、出庫（数量、単価）、在庫数、備考
                sheet.Range("A" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("入出庫日").Value
                sheet.Range("B" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("取引先").Value
                sheet.Range("C" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("入庫数量").Value
                sheet.Range("D" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("入庫単価").Value
                sheet.Range("E" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("出庫数量").Value
                sheet.Range("F" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("出庫単価").Value
                sheet.Range("G" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("在庫数").Value
                sheet.Range("H" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("入出庫種別").Value
                'sheet.Range("I" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("備考").Value
                sheet.Range("I" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("倉庫").Value
                sheet.Range("J" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("伝票番号").Value

                'sheet.Range("A" & cellRowIndex.ToString, "I" & cellRowIndex.ToString).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous     '底の罫線
                sheet.Range("A" & cellRowIndex.ToString, "J" & cellRowIndex.ToString).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous     '底の罫線
            Next

            app.DisplayAlerts = True 'アラート無効化を解除

            app.DisplayAlerts = False 'Microsoft Excelのアラート一旦無効化

            Dim excelChk As Boolean = excelOutput(sOutFile)
            If excelChk = False Then
                Exit Sub
            End If
            book.SaveAs(sOutFile) '書き込み実行

            app.DisplayAlerts = True 'アラート無効化を解除
            app.Visible = True

            'カーソルを砂時計から元に戻す
            Cursor.Current = Cursors.Default

            _msgHd.dspMSG("CreateExcel", frmC01F10_Login.loginValue.Language)

        Catch ex As Exception
            'カーソルを砂時計から元に戻す
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

    '汎用マスタから固定キー、可変キーに応じた結果を返す
    'param1：String 固定キー
    'param2：String 可変キー
    'Return: DataSet
    Private Function getDsHanyoData(ByVal prmFixed As String, Optional ByVal prmVariable As String = "") As DataSet
        Dim Sql As String = ""

        Sql = " AND "
        Sql += "固定キー ILIKE '" & prmFixed & "'"

        If prmVariable IsNot "" Then
            Sql += " AND "
            Sql += "可変キー ILIKE '" & prmVariable & "'"
        End If

        'リードタイムのリストを汎用マスタから取得
        Return getDsData("m90_hanyo", Sql)
    End Function

    Private Sub createWarehouseCombobox(ByRef prmComboboxName As ComboBox, Optional ByRef prmVal As String = "")
        prmComboboxName.DisplayMember = "Text"
        prmComboboxName.ValueMember = "Value"

        Dim Sql As String = " AND 無効フラグ = '" & CommonConst.FLAG_ENABLED & "'"

        Dim ds As DataSet = getDsData("m20_warehouse", Sql)

        Dim tb As New DataTable("Table")
        tb.Columns.Add("Text", GetType(String))
        tb.Columns.Add("Value", GetType(String))

        'コンボボックスが倉庫だったら
        If prmComboboxName.Name = "CmWarehouseFrom" Then
            tb.Rows.Add(IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "All", "すべて"), "all")
        End If
        'For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
        '    tb.Rows.Add(ds.Tables(RS).Rows(i)("名称"), ds.Tables(RS).Rows(i)("倉庫コード"))
        'Next
        For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
            tb.Rows.Add(ds.Tables(RS).Rows(i)("名称"), ds.Tables(RS).Rows(i)("倉庫コード"))
        Next


        prmComboboxName.DataSource = tb

        '倉庫データがあったら
        If ds.Tables(RS).Rows.Count > 0 Then
            If prmVal IsNot "" Then
                prmComboboxName.SelectedValue = prmVal
            Else
                prmComboboxName.SelectedIndex = 0
            End If
        End If

    End Sub

    '処理区分のコンボボックスを作成
    Private Sub createStorageType(ByRef prmComboboxName As ComboBox, Optional ByRef prmVal As String = "")
        Dim Sql As String = ""
        Dim strViewText As String = ""
        Dim strArrayData As String() = prmVal.Split(","c)

        prmComboboxName.DisplayMember = "Text"
        prmComboboxName.ValueMember = "Value"

        Sql = " AND "
        Sql += "固定キー ILIKE '" & CommonConst.INOUT_CLASS & "'"

        Sql += " ORDER BY 表示順"

        'リードタイムのリストを汎用マスタから取得
        Dim dsHanyo As DataSet = getDsData("m90_hanyo", Sql)

        Dim tb As New DataTable("Table")
        tb.Columns.Add("Text", GetType(String))
        tb.Columns.Add("Value", GetType(String))

        strViewText = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "文字２", "文字１")

        For x As Integer = 0 To dsHanyo.Tables(RS).Rows.Count - 1
            tb.Rows.Add(dsHanyo.Tables(RS).Rows(x)(strViewText), dsHanyo.Tables(RS).Rows(x)("可変キー"))
        Next

        prmComboboxName.DataSource = tb
        prmComboboxName.SelectedIndex = 0

    End Sub

    Private Sub CmWarehouseFrom_SelectedIndexChanged(sender As Object, e As EventArgs)
        setList()
    End Sub

    Private Sub DtpMovingDayFrom_ValueChanged(sender As Object, e As EventArgs)
        setList()
    End Sub

    Private Sub DtpMovingDayTo_ValueChanged(sender As Object, e As EventArgs)
        setList()
    End Sub

    Private Sub CmStorageTypeFrom_SelectedIndexChanged(sender As Object, e As EventArgs)
        setList()
    End Sub

    Private Sub CmStorageTypeTo_SelectedIndexChanged(sender As Object, e As EventArgs)
        setList()
    End Sub

    '変更されたら一覧を再取得する
    Private Sub setList()
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        DgvList.Columns.Clear() '見出しクリア
        DgvList.Rows.Clear() '一覧クリア

        setListHd() '見出し行セット

        Try


            Sql = " select "
            Sql += " m21.メーカー, m21.品名, m21.型式, m21.伝票番号, m21.前月末数量 "
            Sql += " , t70.入出庫種別, t70.入出庫区分, t70.入出庫日, t70.備考, t70.数量 "
            Sql += " , m20.名称, m90.文字１, m90.文字２, t43.仕入先名, t43.仕入値 "
            Sql += " , t44.得意先名, t45.売単価 "

            Sql += " FROM "
            Sql += " m21_zaiko m21 "

            Sql += " left join "
            Sql += " t70_inout t70 "
            Sql += " on "
            Sql += " m21.会社コード = t70.会社コード "
            Sql += " AND "
            Sql += " m21.倉庫コード = t70.倉庫コード "

            'Sql += " AND "
            'Sql += " m21.伝票番号 = t70.伝票番号 "
            'Sql += " AND "
            'Sql += " m21.行番号 = t70.行番号 "

            Sql += " AND ( "
            Sql += " ( m21.伝票番号 = t70.伝票番号 "
            Sql += " AND "
            Sql += " m21.行番号 = t70.行番号) "
            Sql += " OR "
            Sql += " concat(m21.伝票番号, m21.行番号) = t70.ロケ番号 "
            Sql += " ) "

            Sql += " AND "
            Sql += " t70.入出庫日 >= '" & UtilClass.formatDatetime(DtpMovingDayFrom.Text) & "'"
            Sql += " AND "
            Sql += " t70.入出庫日 <= '" & UtilClass.formatDatetime(DtpMovingDayTo.Text) & "'"
            Sql += " AND "
            Sql += " t70.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString

            Sql += " left join " '倉庫マスタ
            Sql += " m20_warehouse m20 "
            Sql += " on "
            Sql += " m21.会社コード ILIKE m20.会社コード "
            Sql += " and "
            Sql += " m21.倉庫コード ILIKE m20.倉庫コード "

            Sql += " left join " '汎用マスタ（入出庫種別）
            Sql += " m90_hanyo m90 "
            Sql += " ON "
            Sql += " m90.会社コード ILIKE m21.会社コード "
            Sql += " AND "
            Sql += " m90.固定キー ILIKE '" & CommonConst.INOUT_CLASS & "'"
            Sql += " AND "
            Sql += " m90.可変キー ILIKE m21.入出庫種別 "

            Sql += " left outer join "
            Sql += " t43_nyukodt t43 "
            Sql += " on "
            Sql += " t70.会社コード ILIKE t43.会社コード "
            Sql += " and "
            Sql += " t70.伝票番号 ILIKE t43.入庫番号 "
            Sql += " and "
            Sql += " t70.行番号 = t43.行番号 "
            Sql += " and "
            Sql += " t70.入出庫区分 = '1'"

            Sql += " left outer join "
            Sql += " t45_shukodt t45 "
            Sql += " on "
            Sql += " t70.会社コード ILIKE t45.会社コード "
            Sql += " and "
            Sql += " t70.ロケ番号 = concat(t45.出庫番号, t45.行番号) "
            Sql += " and "
            Sql += " t70.入出庫区分 = '2'"

            Sql += " left outer join "
            Sql += " t44_shukohd t44 "
            Sql += " on "
            Sql += " t70.会社コード ILIKE t44.会社コード "
            Sql += " and "
            Sql += " t45.出庫番号 ILIKE t44.出庫番号 "
            Sql += " and "
            Sql += " t70.入出庫区分 = '2'"

            Sql += " WHERE "
            Sql += " m21.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "' "

            If CmWarehouseFrom.SelectedValue.ToString <> "all" Then
                Sql += " AND "
                Sql += " m21.倉庫コード = '" & CmWarehouseFrom.SelectedValue.ToString & "'"
            End If

            Sql += " AND "
            Sql += " m21.無効フラグ = " & CommonConst.CANCEL_KBN_ENABLED.ToString
            Sql += " AND "
            Sql += " m21.入出庫種別 >= '" & CmStorageTypeFrom.SelectedValue.ToString & "'"
            Sql += " AND "
            Sql += " m21.入出庫種別 <= '" & CmStorageTypeTo.SelectedValue.ToString & "'"

            Sql += " ORDER BY "
            Sql += " m21.倉庫コード, m21.メーカー, m21.品名, m21.型式, m21.伝票番号 "
            Sql += " , t70.入出庫種別, t70.入出庫日 "


            Dim dsList = _db.selectDB(Sql, RS, reccnt)

            Dim currentCul1 As Long = 0
            Dim currentWarehouse As String = ""
            Dim currentManufacturer As String = ""
            Dim currentItemName As String = ""
            Dim currentSpec As String = ""
            Dim productFlg As Boolean = False
            Dim warehouseFlg As Boolean = False

            Dim selectSearch As Date = DtpMovingDayFrom.Text

            'Dim rowIndex As Integer

            For i As Integer = 0 To dsList.Tables(RS).Rows.Count - 1

                '商品が一致するかチェック
                If currentManufacturer <> dsList.Tables(RS).Rows(i)("メーカー").ToString Or
                currentItemName <> dsList.Tables(RS).Rows(i)("品名").ToString Or
                currentSpec <> dsList.Tables(RS).Rows(i)("型式").ToString Then

                    currentManufacturer = dsList.Tables(RS).Rows(i)("メーカー").ToString
                    currentItemName = dsList.Tables(RS).Rows(i)("品名").ToString
                    currentSpec = dsList.Tables(RS).Rows(i)("型式").ToString

                    productFlg = False
                Else
                    productFlg = True
                End If

                If currentWarehouse <> dsList.Tables(RS).Rows(i)("名称").ToString Then
                    currentWarehouse = dsList.Tables(RS).Rows(i)("名称").ToString

                    warehouseFlg = False
                Else
                    warehouseFlg = True
                End If

                If selectSearch.ToString("dd") = "01" And productFlg = False Then

                    currentCul1 = Integer.Parse(dsList.Tables(RS).Rows(i)("前月末数量"))

                End If

                DgvList.Rows.Add()

                DgvList.Rows(i).Cells("倉庫").Value = IIf(warehouseFlg, "", currentWarehouse)

                DgvList.Rows(i).Cells("メーカー").Value = IIf(productFlg, "", currentManufacturer)
                DgvList.Rows(i).Cells("品名").Value = IIf(productFlg, "", currentItemName)
                DgvList.Rows(i).Cells("型式").Value = IIf(productFlg, "", currentSpec)

                DgvList.Rows(i).Cells("伝票番号").Value = dsList.Tables(RS).Rows(i)("伝票番号").ToString
                DgvList.Rows(i).Cells("入出庫種別").Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                                                    dsList.Tables(RS).Rows(i)("文字２"),
                                                                    dsList.Tables(RS).Rows(i)("文字１"))

                DgvList.Rows(i).Cells("入出庫日").Value = dsList.Tables(RS).Rows(i)("入出庫日").ToString 't70 入出庫日
                DgvList.Rows(i).Cells("取引先").Value = IIf(dsList.Tables(RS).Rows(i)("入出庫区分").ToString = "1",
                                                            dsList.Tables(RS).Rows(i)("仕入先名").ToString,
                                                            dsList.Tables(RS).Rows(i)("得意先名").ToString)

                DgvList.Rows(i).Cells("入庫数量").Value = IIf(dsList.Tables(RS).Rows(i)("入出庫区分").ToString = "1",
                                                            dsList.Tables(RS).Rows(i)("数量").ToString,
                                                            "")
                DgvList.Rows(i).Cells("入庫単価").Value = dsList.Tables(RS).Rows(i)("仕入値").ToString
                DgvList.Rows(i).Cells("出庫数量").Value = IIf(dsList.Tables(RS).Rows(i)("入出庫区分").ToString = "2",
                                                            dsList.Tables(RS).Rows(i)("数量").ToString,
                                                            "")
                DgvList.Rows(i).Cells("出庫単価").Value = dsList.Tables(RS).Rows(i)("売単価").ToString

                If selectSearch.ToString("dd") = "01" Then
                    If IsNumeric(dsList.Tables(RS).Rows(i)("数量").ToString) Then
                        If dsList.Tables(RS).Rows(i)("入出庫区分").ToString = "1" Then
                            currentCul1 += Integer.Parse(dsList.Tables(RS).Rows(i)("数量").ToString)
                        Else
                            currentCul1 -= Integer.Parse(dsList.Tables(RS).Rows(i)("数量").ToString)
                        End If
                    End If
                    DgvList.Rows(i).Cells("在庫数").Value = currentCul1
                End If

                DgvList.Rows(i).Cells("備考").Value = dsList.Tables(RS).Rows(i)("備考").ToString

            Next


        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

        'Dim dsList = _db.selectDB(Sql, RS, reccnt)

        'For i As Integer = 0 To dsList.Tables(RS).Rows.Count - 1

        '    DgvList.Rows.Add()
        '    DgvList.Rows(i).Cells("倉庫").Value = dsList.Tables(RS).Rows(i)("名称")
        '    DgvList.Rows(i).Cells("メーカー").Value = dsList.Tables(RS).Rows(i)("メーカー")
        '    DgvList.Rows(i).Cells("品名").Value = dsList.Tables(RS).Rows(i)("品名")
        '    DgvList.Rows(i).Cells("型式").Value = dsList.Tables(RS).Rows(i)("型式")
        '    DgvList.Rows(i).Cells("伝票番号").Value = dsList.Tables(RS).Rows(i)("伝票番号")
        '    DgvList.Rows(i).Cells("入出庫種別").Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
        '                                                            dsList.Tables(RS).Rows(i)("文字２"),
        '                                                            dsList.Tables(RS).Rows(i)("文字１"))
        '    'DgvList.Rows(i).Cells("入出庫日").Value = dsList.Tables(RS).Rows(i)("入出庫日")
        '    'DgvList.Rows(i).Cells("取引先").Value = dsList.Tables(RS).Rows(i)("現在庫数")
        '    'DgvList.Rows(i).Cells("入庫数量").Value = dsList.Tables(RS).Rows(i)("入庫単価")
        '    'DgvList.Rows(i).Cells("入庫単価").Value = dsList.Tables(RS).Rows(i)("最終入庫日")
        '    'DgvList.Rows(i).Cells("出庫数量").Value = dsList.Tables(RS).Rows(i)("最終出庫日")
        '    'DgvList.Rows(i).Cells("出庫単価").Value = dsList.Tables(RS).Rows(i)("最終出庫日")
        '    'DgvList.Rows(i).Cells("在庫数").Value = dsList.Tables(RS).Rows(i)("最終出庫日")
        '    'DgvList.Rows(i).Cells("備考").Value = dsList.Tables(RS).Rows(i)("備考")

        'Next
    End Sub
End Class