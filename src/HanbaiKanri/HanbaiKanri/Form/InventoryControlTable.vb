'------------------------------------------------------------------------------------------------------
'改訂履歴
'2020.01.09 
'ロケ番号→出庫開始サインに名称変更
'2020.03.15
'在庫管理区分、在庫表示区分によるリスト表示方式の改訂（区分を会社マスタに実装するまで変数で固定割り当てする）
'------------------------------------------------------------------------------------------------------

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
    Private InventoryControl As String = "V"                 '倉庫、入出庫種別を管理初期値とする
    Private InventoryViewer As String = "7"                  '倉庫、入出庫種別、ロケーションを表示初期値とする


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

            lblWarehouse.Text = "Warehouse"
            LblMovingDay.Text = "MovingDay"

            lblYear.Text = "Year"
            lblMonth.Text = "Month"

            lblMaker.Text = "Maker"
            LblItemName.Text = "ItemName"
            LblSpec.Text = "Spec"

            cmd検索.Text = "Search"
        End If

        makeTableLayoutPanel1()     'コンボボックスコントロールの表示制御
        createZaikoCombobox(cmWarehouseFrom)
        createZaikoCombobox(cmSyubetsuFrom)
        createZaikoCombobox(cmLocationFrom)
        createZaikoCombobox(cmSerialNoFrom)
        createZaikoCombobox(cmOrderNoFrom)

        '2020.03.17






        '対象年月
        Dim dtmTemp As Date = DateAdd("m", -1, Now)
        Dim strYear As String = dtmTemp.Year
        Dim strMonth As String = dtmTemp.Month

        txtYear.Text = strYear
        txtMonth.Text = strMonth


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

        DgvList.Columns.Clear() '見出しクリア
        DgvList.Rows.Clear()    '一覧クリア

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            DgvList.Columns.Add("倉庫", "Manufacturer")
            DgvList.Columns.Add("メーカー", "Manufacturer")
            DgvList.Columns.Add("品名", "ItemName")
            DgvList.Columns.Add("型式", "Spec")
            DgvList.Columns.Add("伝票番号", "SlipNumber")
            'DgvList.Columns.Add("入出庫種別", "StorageType")
            DgvList.Columns.Add("入出庫日", "InOutDate")
            DgvList.Columns.Add("取引先", "Suppliers")
            DgvList.Columns.Add("月末在庫数", "MonthEndInventory")
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
            'DgvList.Columns.Add("入出庫種別", "入出庫種別")
            DgvList.Columns.Add("入出庫日", "入出庫日")
            DgvList.Columns.Add("取引先", "取引先")
            DgvList.Columns.Add("月末在庫数", "月末在庫数")
            DgvList.Columns.Add("入庫数量", "入庫数量")
            DgvList.Columns.Add("入庫単価", "入庫単価")
            DgvList.Columns.Add("出庫数量", "出庫数量")
            DgvList.Columns.Add("出庫単価", "出庫単価")
            DgvList.Columns.Add("在庫数", "在庫数")
            DgvList.Columns.Add("備考", "備考")
        End If

        DgvList.Columns("月末在庫数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("入庫数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("入庫単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("出庫数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("出庫単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvList.Columns("在庫数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        '数字形式
        DgvList.Columns("月末在庫数").DefaultCellStyle.Format = "N2"
        DgvList.Columns("入庫数量").DefaultCellStyle.Format = "N2"
        DgvList.Columns("入庫単価").DefaultCellStyle.Format = "N2"
        DgvList.Columns("出庫数量").DefaultCellStyle.Format = "N2"
        DgvList.Columns("出庫単価").DefaultCellStyle.Format = "N2"
        DgvList.Columns("在庫数").DefaultCellStyle.Format = "N2"

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

            Dim strSoko As String = DgvList.Rows(0).Cells("倉庫").Value
            Dim strMaker As String = DgvList.Rows(0).Cells("メーカー").Value
            Dim strItem As String = DgvList.Rows(0).Cells("品名").Value
            Dim strSpec As String = DgvList.Rows(0).Cells("型式").Value

            Dim cellRowIndex As Integer = 1
            For i As Integer = 0 To DgvList.RowCount - 1

                cellRowIndex += 1

                Dim strSoko2 As String = DgvList.Rows(i).Cells("倉庫").Value
                Dim strMaker2 As String = DgvList.Rows(i).Cells("メーカー").Value
                Dim strItem2 As String = DgvList.Rows(i).Cells("品名").Value
                Dim strSpec2 As String = DgvList.Rows(i).Cells("型式").Value

                If strSoko <> strSoko2 Or strMaker <> strMaker2 Or strItem <> strItem2 Or strSpec <> strSpec2 Or i = 0 Then
                    'If DgvList.Rows(i).Cells("メーカー").Value <> "" Then

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

                    sheet.Range("A" & cellRowIndex.ToString, "J" & cellRowIndex.ToString).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlDouble

                    cellRowIndex += 1 '次の行

                    sheet.Range("D" & cellRowIndex.ToString, "E" & cellRowIndex.ToString).Merge() '入庫見出し
                    sheet.Range("F" & cellRowIndex.ToString, "G" & cellRowIndex.ToString).Merge() '出庫見出し
                    sheet.Range("D" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "GoodsReceipt", "入庫")
                    sheet.Range("F" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "GoodsDelivery", "出庫")
                    sheet.Range("D" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                    sheet.Range("F" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter

                    cellRowIndex += 1 '次の行

                    sheet.Range("C" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                    sheet.Range("D" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                    sheet.Range("E" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                    sheet.Range("F" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft
                    sheet.Range("G" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft

                    '年月日、取引先、入庫（数量、単価）、出庫（数量、単価）、在庫数、備考

                    sheet.Range("A" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "Date", "入出庫日")
                    sheet.Range("B" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "Suppliers", "取引先")
                    sheet.Range("C" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "MonthEndInventory", "月末在庫数")
                    sheet.Range("D" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "Quantity", "数量")
                    sheet.Range("E" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "UnitPrice", "単価")
                    sheet.Range("F" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "Quantity", "数量")
                    sheet.Range("G" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "UnitPrice", "単価")
                    sheet.Range("H" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "StockQuantity", "在庫数")
                    'sheet.Range("H" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "InOutClassification", "入出庫種別")
                    'sheet.Range("I" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "Remarks", "備考")
                    sheet.Range("I" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "Warehouse", "倉庫")
                    sheet.Range("J" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "SlipNumber", "伝票番号")

                    sheet.Range("A" & cellRowIndex.ToString, "J" & cellRowIndex.ToString).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous　　　　　'底の罫線

                    cellRowIndex += 1 '次の行

                    strSoko = DgvList.Rows(i).Cells("倉庫").Value
                    strMaker = DgvList.Rows(i).Cells("メーカー").Value
                    strItem = DgvList.Rows(i).Cells("品名").Value
                    strSpec = DgvList.Rows(i).Cells("型式").Value

                End If

                '年月日、取引先、入庫（数量、単価）、出庫（数量、単価）、在庫数、備考
                sheet.Range("A" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("入出庫日").Value
                sheet.Range("B" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("取引先").Value
                sheet.Range("C" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("月末在庫数").Value
                sheet.Range("D" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("入庫数量").Value
                sheet.Range("E" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("入庫単価").Value
                sheet.Range("F" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("出庫数量").Value
                sheet.Range("G" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("出庫単価").Value
                sheet.Range("H" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("在庫数").Value
                'sheet.Range("H" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("入出庫種別").Value
                'sheet.Range("I" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("備考").Value
                sheet.Range("I" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("倉庫").Value
                sheet.Range("J" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("伝票番号").Value

                sheet.Range("C" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlRight
                sheet.Range("D" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlRight
                sheet.Range("E" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlRight
                sheet.Range("F" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlRight
                sheet.Range("G" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlRight
                sheet.Range("H" & cellRowIndex.ToString).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlRight

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

    '-------------------------------------------------------------------------------
    '在庫コンボボックス項目をコントロールする
    '-------------------------------------------------------------------------------
    Private Sub makeTableLayoutPanel1()

        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Sql = "SELECT "
        Sql += " m01.在庫管理区分 AS 在庫管理区分,m01.在庫表示区分 AS 在庫表示区分 "
        Sql += " FROM m01_company m01"
        Sql += " WHERE "
        Sql += " m01.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"

        Try

            Dim dsM01 As DataSet = _db.selectDB(Sql, RS, reccnt)

            InventoryControl = dsM01.Tables(RS).Rows(0)("在庫管理区分")
            InventoryViewer = dsM01.Tables(RS).Rows(0)("在庫表示区分")

        Catch ue As UsrDefException
        Catch ex As Exception
        End Try

        'コンボボックス表示状態を「商品別」以外リセットする
        lblWarehouse.Visible = False
        lblSyubetsu.Visible = False
        lblLocation.Visible = False
        lblSerialNo.Visible = False
        lblOrderNo.Visible = False

        cmWarehouseFrom.Visible = False
        cmSyubetsuFrom.Visible = False
        cmLocationFrom.Visible = False
        cmSerialNoFrom.Visible = False
        cmOrderNoFrom.Visible = False

        Select Case InventoryControl
            Case "1", "3", "7", "F", "V"
                lblWarehouse.Visible = True
                cmWarehouseFrom.Visible = True
        End Select
        Select Case InventoryControl
            Case "3", "7", "F", "V"
                lblSyubetsu.Visible = True
                cmSyubetsuFrom.Visible = True
        End Select
        Select Case InventoryControl
            Case "7", "F", "V"
                lblLocation.Visible = True
                cmLocationFrom.Visible = True
        End Select
        Select Case InventoryControl
            Case "F", "V"
                lblSerialNo.Visible = True
                cmSerialNoFrom.Visible = True
        End Select
        Select Case InventoryControl
            Case "V"
                lblOrderNo.Visible = True
                cmOrderNoFrom.Visible = True
        End Select

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
    Private Sub createZaikoCombobox(ByRef prmComboboxName As ComboBox, Optional ByRef prmVal As String = "")

        Dim reccnt As Integer = 0 'DB用（デフォルト）

        prmComboboxName.DisplayMember = "Text"
        prmComboboxName.ValueMember = "Value"



        Dim Sql As String = ""

        If prmComboboxName.Name = "cmWarehouseFrom" Then
            Sql = "SELECT T70.倉庫コード AS 名称 FROM T70_INOUT T70 "
            Sql += "WHERE T70.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            ''Sql += " AND (T70.入出庫日 >= '" & UtilClass.strFormatDate(dtmSyoriNentuki.ToShortDateString) & "'"
            ''Sql += " AND  T70.入出庫日 < '" & UtilClass.strFormatDate(dtmSyoriNentukiE.ToShortDateString) & "')"
            Sql += " AND T70.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString
            Sql += " GROUP BY T70.倉庫コード"
            Sql += " ORDER BY T70.倉庫コード"
        End If
        If prmComboboxName.Name = "cmSyubetsuFrom" Then
            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                Sql = "SELECT M90.文字２ AS 名称 FROM M90_HANYO M90 INNER JOIN T70_INOUT T70 "
                Sql += " ON (M90.会社コード = T70.会社コード AND M90.可変キー = T70.入出庫種別)"
                Sql += "WHERE T70.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " AND M90.固定キー ILIKE '" & CommonConst.INOUT_CLASS & "'"
                Sql += " AND T70.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString
                Sql += " GROUP BY M90.文字２"
                Sql += " ORDER BY M90.文字２"
            Else
                Sql = "SELECT M90.文字１ AS 名称 FROM M90_HANYO M90 INNER JOIN T70_INOUT T70 "
                Sql += " ON (M90.会社コード = T70.会社コード AND M90.可変キー = T70.入出庫種別)"
                Sql += "WHERE T70.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " AND M90.固定キー ILIKE '" & CommonConst.INOUT_CLASS & "'"
                Sql += " AND T70.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString
                Sql += " GROUP BY M90.文字１"
                Sql += " ORDER BY M90.文字１"
            End If
        End If
        If prmComboboxName.Name = "cmLocationFrom" Then
            Sql = "SELECT T70.ロケ番号 AS 名称 FROM T70_INOUT T70 "
            Sql += "WHERE T70.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            ''Sql += " AND (T70.入出庫日 >= '" & UtilClass.strFormatDate(dtmSyoriNentuki.ToShortDateString) & "'"
            ''Sql += " AND  T70.入出庫日 < '" & UtilClass.strFormatDate(dtmSyoriNentukiE.ToShortDateString) & "')"
            Sql += " AND T70.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString
            Sql += " GROUP BY T70.ロケ番号"
            Sql += " ORDER BY T70.ロケ番号"
        End If
        If prmComboboxName.Name = "cmSerialNoFrom" Then
            Sql = "SELECT T70.製造番号 AS 名称 FROM T70_INOUT T70 "
            Sql += "WHERE T70.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            ''Sql += " AND (T70.入出庫日 >= '" & UtilClass.strFormatDate(dtmSyoriNentuki.ToShortDateString) & "'"
            ''Sql += " AND  T70.入出庫日 < '" & UtilClass.strFormatDate(dtmSyoriNentukiE.ToShortDateString) & "')"
            Sql += " AND T70.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString
            Sql += " GROUP BY T70.製造番号"
            Sql += " ORDER BY T70.製造番号"
        End If
        If prmComboboxName.Name = "cmOrderNoFrom" Then
            Sql = "SELECT T70.伝票番号 AS 名称 FROM T70_INOUT T70 "
            Sql += "WHERE T70.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            ''Sql += " AND (T70.入出庫日 >= '" & UtilClass.strFormatDate(dtmSyoriNentuki.ToShortDateString) & "'"
            ''Sql += " AND  T70.入出庫日 < '" & UtilClass.strFormatDate(dtmSyoriNentukiE.ToShortDateString) & "')"
            Sql += " AND T70.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString
            Sql += " GROUP BY T70.伝票番号"
            Sql += " ORDER BY T70.伝票番号"
        End If

        Try
            Dim dsList As DataSet = _db.selectDB(Sql, RS, reccnt)
            Dim tb As New DataTable("Table")
            tb.Columns.Add("Text", GetType(String))
            tb.Columns.Add("Value", GetType(String))
            tb.Rows.Add(IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "All", "すべて"), "all")
            For i As Integer = 0 To dsList.Tables(RS).Rows.Count - 1
                tb.Rows.Add(dsList.Tables(RS).Rows(i)("名称"), dsList.Tables(RS).Rows(i)("名称"))
            Next


            prmComboboxName.DataSource = tb

            '倉庫データがあったら
            If dsList.Tables(RS).Rows.Count > 0 Then
                If prmVal IsNot "" Then
                    prmComboboxName.SelectedValue = tb
                Else
                    prmComboboxName.SelectedIndex = 0
                End If
            End If


        Catch ex As Exception

            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try


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
        Sql += " AND "
        Sql += "可変キー <> '" & CommonConst.INOUT_KBN_INCREASE & "'"

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

    '変更されたら一覧を再取得する
    Private Sub setList()
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""


        setListHd() '見出し行セット


        Try

            Dim strSoko As String = cmWarehouseFrom.SelectedValue  '倉庫コード


#Region "t68_krzaiko"

            Dim strSyoriNentuki As String = txtYear.Text & txtMonth.Text.ToString.PadLeft(2, "0"c)  '処理年月


            Sql = " select"

            Sql += " 倉庫コード as 倉庫"
            Sql += ",メーカー"
            Sql += ",品名"
            Sql += ",型式"
            Sql += ",入庫番号 as 伝票番号"
            Sql += ",行番号"
            Sql += ",入庫日 as 入出庫日"
            Sql += ",仕入先名 as 取引先"
            Sql += ",月末数量 as 月末在庫数"
            Sql += ",null as 入庫数量"
            Sql += ",入庫単価 as 入庫単価"
            Sql += ",null as 出庫数量"
            Sql += ",null as 出庫単価"
            Sql += ",月末数量 as 在庫数"
            Sql += ",null as 備考"

            Sql += " FROM t68_krzaiko"

            Sql += " where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += "   and 処理年月 = '" & strSyoriNentuki & "'"

            If strSoko = "all" Then
            Else
                Sql += "   and 倉庫コード = '" & strSoko & "'"
            End If

            '商品の条件
            If UtilClass.IsExistString(txtMaker.Text) Then
                Sql += "   and メーカー like '%" & txtMaker.Text & "%'"
            End If
            If UtilClass.IsExistString(TxtItemName.Text) Then
                Sql += "   and 品名 like '%" & TxtItemName.Text & "%'"
            End If
            If UtilClass.IsExistString(TxtSpec.Text) Then
                Sql += "   and 型式 like '%" & TxtSpec.Text & "%'"
            End If

#End Region


#Region "nyuko"

            Dim strNen As String = Mid(strSyoriNentuki, 1, 4)
            Dim strTuki As String = Mid(strSyoriNentuki, 5, 2) + 1

            Dim dtmSyoriNentuki As Date = DateSerial(strNen, strTuki, 1)     '処理年月日
            Dim dtmSyoriNentukiE As Date = DateAdd("m", 1, dtmSyoriNentuki)  '処理年月日 end


            Sql += " union "

            Sql += " select "

            Sql += " t42.倉庫コード"
            Sql += ",t43.メーカー"
            Sql += ",t43.品名"
            Sql += ",t43.型式"
            Sql += ",t42.入庫番号 as 伝票番号"
            Sql += ",t43.行番号"
            Sql += ",t42.入庫日 as 入出庫日"
            Sql += ",t42.仕入先名 as 取引先"
            Sql += ",null as 月末在庫数"
            Sql += ",t43.入庫数量 as 入庫数量"
            Sql += ",t43.仕入値 as 入庫単価"
            Sql += ",null as 出庫数量"
            Sql += ",null as 出庫単価"
            Sql += ",入庫数量 as 在庫数"
            Sql += ",t43.備考"

            Sql += " FROM t42_nyukohd t42 left join t43_nyukodt t43"
            Sql += " on t42.入庫番号 = t43.入庫番号"

            Sql += " where t42.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += "   and (t42.入庫日 >= '" & UtilClass.strFormatDate(dtmSyoriNentuki.ToShortDateString) & "'"
            Sql += "   and t42.入庫日 < '" & UtilClass.strFormatDate(dtmSyoriNentukiE.ToShortDateString) & "')"

            Sql += "   and t42.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString

            If strSoko = "all" Then
            Else
                Sql += "   and 倉庫コード = '" & strSoko & "'"
            End If

            '商品の条件
            If UtilClass.IsExistString(txtMaker.Text) Then
                Sql += "   and メーカー like '%" & txtMaker.Text & "%'"
            End If
            If UtilClass.IsExistString(TxtItemName.Text) Then
                Sql += "   and 品名 like '%" & TxtItemName.Text & "%'"
            End If
            If UtilClass.IsExistString(TxtSpec.Text) Then
                Sql += "   and 型式 like '%" & TxtSpec.Text & "%'"
            End If

#End Region


            Sql += " order by メーカー,品名,型式,倉庫,入出庫日"

            Dim dsList As DataTable = _db.selectDB(Sql, RS, reccnt).Tables(0)
            If dsList.Rows.Count = 0 Then
                _msgHd.dspMSG("NonData", frmC01F10_Login.loginValue.Language)
                Exit Sub
            End If

            Dim intList As Integer = 0

            For i As Integer = 0 To dsList.Rows.Count - 1

                DgvList.Rows.Add()


#Region "t68_krzaiko"

                DgvList.Rows(intList).Cells("倉庫").Value = dsList.Rows(i)("倉庫").ToString

                DgvList.Rows(intList).Cells("メーカー").Value = dsList.Rows(i)("メーカー").ToString
                DgvList.Rows(intList).Cells("品名").Value = dsList.Rows(i)("品名").ToString
                DgvList.Rows(intList).Cells("型式").Value = dsList.Rows(i)("型式").ToString

                DgvList.Rows(intList).Cells("伝票番号").Value = dsList.Rows(i)("伝票番号").ToString

                'DgvList.Rows(i).Cells("入出庫種別").Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                '                                                    dsList.Rows(i)("文字２"),
                '                                                    dsList.Rows(i)("文字１"))

                '入出庫日
                If String.IsNullOrEmpty(dsList.Rows(i)("入庫数量").ToString) Then
                    DgvList.Rows(intList).Cells("入出庫日").Value = DateAdd("m", -1, dtmSyoriNentuki).ToShortDateString  '月末在庫データの場合
                Else
                    Dim dtmNyukobi As Date = dsList.Rows(i)("入出庫日").ToString
                    DgvList.Rows(intList).Cells("入出庫日").Value = dtmNyukobi.ToShortDateString
                End If

                DgvList.Rows(intList).Cells("取引先").Value = dsList.Rows(i)("取引先").ToString


                '月末在庫数
                If String.IsNullOrEmpty(dsList.Rows(i)("月末在庫数").ToString) Then
                Else
                    Dim decGetumatu As Decimal = dsList.Rows(i)("月末在庫数").ToString
                    DgvList.Rows(intList).Cells("月末在庫数").Value = decGetumatu.ToString("N2")
                End If

                '入庫数量
                If String.IsNullOrEmpty(dsList.Rows(i)("入庫数量").ToString) Then
                Else
                    Dim decNyuko As Decimal = dsList.Rows(i)("入庫数量").ToString
                    DgvList.Rows(intList).Cells("入庫数量").Value = decNyuko.ToString("N2")
                End If

                '入庫単価
                Dim decTemp As Decimal = rmNullDecimal(dsList.Rows(i)("入庫単価").ToString)
                DgvList.Rows(intList).Cells("入庫単価").Value = decTemp.ToString("N2")

                DgvList.Rows(intList).Cells("出庫数量").Value = dsList.Rows(i)("出庫数量")

                '出庫単価
                decTemp = rmNullDecimal(dsList.Rows(i)("出庫単価").ToString)
                If decTemp = 0 Then
                Else
                    DgvList.Rows(intList).Cells("出庫単価").Value = decTemp.ToString("N2")
                End If


                DgvList.Rows(intList).Cells("在庫数").Value = dsList.Rows(i)("在庫数")

                DgvList.Rows(intList).Cells("備考").Value = dsList.Rows(i)("備考").ToString
#End Region


#Region "inout_syuko"

                Dim strLocation As String = dsList.Rows(i)("伝票番号").ToString & dsList.Rows(i)("行番号").ToString

                Sql = " select t70.*,t45.売単価, t44.得意先名"

                Sql += " FROM t70_inout as t70 left join t45_shukodt as t45"
                Sql += " on t70.伝票番号 = t45.出庫番号 and t70.行番号 = t45.行番号"

                Sql += " left join t44_shukohd as t44"
                Sql += " on t45.出庫番号 = t44.出庫番号"

                Sql += " where t70.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += "   and (t70.入出庫日 >= '" & UtilClass.strFormatDate(dtmSyoriNentuki.ToShortDateString) & "'"
                Sql += "   and t70.入出庫日 < '" & UtilClass.strFormatDate(dtmSyoriNentukiE.ToShortDateString) & "')"

                Sql += "   and t70.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString
                ''Sql += "   and t70.ロケ番号 = '" & strLocation & "'"                      '2020.01.09 DEL
                Sql += "   and t70.出庫開始サイン = '" & strLocation & "'"                  '2020.01.09 ADD


                Dim dsinout As DataTable = _db.selectDB(Sql, RS, reccnt).Tables(0)


                For j As Integer = 0 To dsinout.Rows.Count - 1

                    intList += 1
                    DgvList.Rows.Add()

                    DgvList.Rows(intList).Cells("倉庫").Value = dsinout.Rows(j)("倉庫コード").ToString

                    DgvList.Rows(intList).Cells("メーカー").Value = dsinout.Rows(j)("メーカー").ToString
                    DgvList.Rows(intList).Cells("品名").Value = dsinout.Rows(j)("品名").ToString
                    DgvList.Rows(intList).Cells("型式").Value = dsinout.Rows(j)("型式").ToString

                    DgvList.Rows(intList).Cells("伝票番号").Value = dsinout.Rows(j)("伝票番号").ToString

                    '入出庫日
                    Dim dtmSyuko As Date = dsinout.Rows(j)("入出庫日").ToString
                    DgvList.Rows(intList).Cells("入出庫日").Value = dtmSyuko.ToShortDateString

                    DgvList.Rows(intList).Cells("取引先").Value = dsinout.Rows(j)("得意先名").ToString

                    DgvList.Rows(intList).Cells("出庫数量").Value = dsinout.Rows(j)("数量")

                    '出庫単価
                    decTemp = rmNullDecimal(dsinout.Rows(j)("売単価").ToString)
                    If decTemp = 0 Then
                    Else
                        DgvList.Rows(intList).Cells("出庫単価").Value = decTemp.ToString("N2")
                    End If

                    '在庫数
                    Dim decZaiko As Decimal = DgvList.Rows(intList - 1).Cells("在庫数").Value - dsinout.Rows(j)("数量")
                    DgvList.Rows(intList).Cells("在庫数").Value = decZaiko.ToString("N2")

                    DgvList.Rows(intList).Cells("備考").Value = dsinout.Rows(j)("備考")

                Next

#End Region

                intList += 1
            Next

        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try


        Debug.Print(DgvList.Rows.Count)

    End Sub


    Private Sub cmd検索_Click(sender As Object, e As EventArgs) Handles cmd検索.Click

        Dim blnFlg = mCheck()     'チェック
        If blnFlg = False Then
            Exit Sub
        End If

        Call setList()  '描画
    End Sub

    Private Function mCheck() As Boolean

#Region "年"

        If IsNumeric(txtYear.Text) Then
        Else
            _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
            txtYear.Focus()
            Exit Function
        End If

        If txtYear.TextLength = 4 Then
        Else
            _msgHd.dspMSG("chkAddData", frmC01F10_Login.loginValue.Language)
            txtYear.Focus()
            Exit Function
        End If
#End Region


#Region "月"

        If IsNumeric(txtMonth.Text) Then
        Else
            _msgHd.dspMSG("IsNotNumeric", frmC01F10_Login.loginValue.Language)
            txtMonth.Focus()
            Exit Function
        End If

        If txtMonth.Text >= 1 And txtMonth.Text <= 12 Then
        Else
            _msgHd.dspMSG("chkAddData", frmC01F10_Login.loginValue.Language)
            txtMonth.Focus()
            Exit Function
        End If
#End Region


        mCheck = True

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
End Class