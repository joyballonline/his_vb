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
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
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

        getList() 'データ取得・表示

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

            DgvList.Columns.Add("メーカー", "Manufacturer")
            DgvList.Columns.Add("品名", "ItemName")
            DgvList.Columns.Add("型式", "Spec")
            DgvList.Columns.Add("年月日", "Date")
            DgvList.Columns.Add("取引先", "Suppliers")
            DgvList.Columns.Add("入庫数量", "ReceiptQuantiy")
            DgvList.Columns.Add("入庫単価", "ReceiptUnitPrice")
            DgvList.Columns.Add("出庫数量", "DeliveryQuantiy")
            DgvList.Columns.Add("出庫単価", "DeliveryUnitPrice")
            DgvList.Columns.Add("在庫数", "StockQuantity")
            DgvList.Columns.Add("備考", "Remarks")
        Else
            DgvList.Columns.Add("メーカー", "メーカー")
            DgvList.Columns.Add("品名", "品名")
            DgvList.Columns.Add("型式", "型式")
            DgvList.Columns.Add("年月日", "年月日")
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

    Private Sub getList()

        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        DgvList.Columns.Clear() '見出しクリア
        DgvList.Rows.Clear() '一覧クリア

        setListHd() '見出し行セット

        Try

            '入庫データの取得
            '
            Sql = " SELECT "
            Sql += " t70.メーカー, t70.品名, t70.型式, t70.数量, t70.入出庫日, t70.入出庫区分, t43.仕入先名 AS 入庫仕入先 "
            Sql += " ,t45.仕入先名 AS 出庫仕入先, t43.仕入値, t45.売単価, t70.備考 "
            Sql += " FROM "
            Sql += " t70_inout t70 "

            Sql += " LEFT JOIN "
            Sql += " t43_nyukodt t43 "
            Sql += " ON  t70.会社コード = t43.会社コード "
            Sql += " AND  t70.伝票番号 = t43.入庫番号 "
            Sql += " AND  t70.メーカー = t43.メーカー "
            Sql += " AND  t70.品名 = t43.品名 "
            Sql += " AND  t70.型式 = t43.型式 "

            Sql += " LEFT JOIN "
            Sql += " t45_shukodt t45 "
            Sql += " ON  t70.会社コード = t45.会社コード "
            Sql += " AND  t70.伝票番号 = t45.出庫番号 "
            Sql += " AND  t70.メーカー = t45.メーカー "
            Sql += " AND  t70.品名 = t45.品名 "
            Sql += " AND  t70.型式 = t45.型式 "

            Sql += " WHERE "
            Sql += " t70.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND "
            Sql += " t70.取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'"

            Sql += " ORDER BY "
            Sql += " t70.メーカー, t70.品名, t70.型式, t70.入出庫日 "

            Dim dsZaiko As DataSet = _db.selectDB(Sql, RS, reccnt)

            Dim tmpMaker As String = ""
            Dim tmpItemName As String = ""
            Dim tmpSpec As String = ""
            Dim tmpQuantity As Integer = 0

            For i As Integer = 0 To dsZaiko.Tables(RS).Rows.Count - 1 '在庫データ

                DgvList.Rows.Add()

                '商品が変わったら取得
                If (tmpMaker <> dsZaiko.Tables(RS).Rows(i)("メーカー").ToString And
                    tmpItemName <> dsZaiko.Tables(RS).Rows(i)("品名").ToString And
                    tmpSpec <> dsZaiko.Tables(RS).Rows(i)("型式").ToString) Then

                    tmpMaker = dsZaiko.Tables(RS).Rows(i)("メーカー").ToString
                    tmpItemName = dsZaiko.Tables(RS).Rows(i)("品名").ToString
                    tmpSpec = dsZaiko.Tables(RS).Rows(i)("型式").ToString
                    tmpQuantity = 0

                    DgvList.Rows(i).Cells("メーカー").Value = tmpMaker
                    DgvList.Rows(i).Cells("品名").Value = tmpItemName
                    DgvList.Rows(i).Cells("型式").Value = tmpSpec


                Else
                    tmpMaker = dsZaiko.Tables(RS).Rows(i)("メーカー").ToString
                    tmpItemName = dsZaiko.Tables(RS).Rows(i)("品名").ToString
                    tmpSpec = dsZaiko.Tables(RS).Rows(i)("型式").ToString

                End If

                If dsZaiko.Tables(RS).Rows(i)("入出庫区分") = 1 Then
                    tmpQuantity += dsZaiko.Tables(RS).Rows(i)("数量")
                Else
                    tmpQuantity -= dsZaiko.Tables(RS).Rows(i)("数量")
                End If


                DgvList.Rows(i).Cells("年月日").Value = dsZaiko.Tables(RS).Rows(i)("入出庫日").ToShortDateString
                DgvList.Rows(i).Cells("取引先").Value = IIf(dsZaiko.Tables(RS).Rows(i)("入出庫区分") = 1,
                                                         dsZaiko.Tables(RS).Rows(i)("入庫仕入先"),
                                                         dsZaiko.Tables(RS).Rows(i)("出庫仕入先"))
                If dsZaiko.Tables(RS).Rows(i)("入出庫区分") = 1 Then
                    DgvList.Rows(i).Cells("取引先").Value = dsZaiko.Tables(RS).Rows(i)("入庫仕入先")
                    DgvList.Rows(i).Cells("入庫数量").Value = dsZaiko.Tables(RS).Rows(i)("数量")
                    DgvList.Rows(i).Cells("入庫単価").Value = dsZaiko.Tables(RS).Rows(i)("仕入値")

                Else
                    DgvList.Rows(i).Cells("取引先").Value = dsZaiko.Tables(RS).Rows(i)("出庫仕入先")
                    DgvList.Rows(i).Cells("出庫数量").Value = dsZaiko.Tables(RS).Rows(i)("数量")
                    DgvList.Rows(i).Cells("出庫単価").Value = dsZaiko.Tables(RS).Rows(i)("売単価")

                End If

                DgvList.Rows(i).Cells("在庫数").Value = tmpQuantity
                DgvList.Rows(i).Cells("備考").Value = dsZaiko.Tables(RS).Rows(i)("備考")

            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    'excel出力処理
    Private Sub outputExcel()

        '定義
        Dim app As Excel.Application = Nothing
        Dim book As Excel.Workbook = Nothing
        Dim sheet As Excel.Worksheet = Nothing

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

            app = New Excel.Application()
            book = app.Workbooks.Add(sHinaFile)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet)

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

                    sheet.Range("A" & cellRowIndex.ToString, "H" & cellRowIndex.ToString).Borders(XlBordersIndex.xlEdgeTop).LineStyle = XlLineStyle.xlDouble

                    'メーカー、品名、型式 の列結合
                    sheet.Range("A" & cellRowIndex.ToString, "B" & cellRowIndex.ToString).Merge() 'メーカー
                    sheet.Range("C" & cellRowIndex.ToString, "D" & cellRowIndex.ToString).Merge() '品名
                    sheet.Range("F" & cellRowIndex.ToString, "H" & cellRowIndex.ToString).Merge() '型式

                    sheet.Range("A" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("メーカー").Value
                    sheet.Range("C" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("品名").Value
                    sheet.Range("F" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("型式").Value

                    sheet.Range("A" & cellRowIndex.ToString).HorizontalAlignment = Constants.xlLeft
                    sheet.Range("C" & cellRowIndex.ToString).HorizontalAlignment = Constants.xlLeft
                    sheet.Range("F" & cellRowIndex.ToString).HorizontalAlignment = Constants.xlLeft

                    sheet.Range("A" & cellRowIndex.ToString, "H" & cellRowIndex.ToString).Borders(XlBordersIndex.xlEdgeBottom).LineStyle = XlLineStyle.xlDouble

                    cellRowIndex += 1 '次の行

                    sheet.Range("C" & cellRowIndex.ToString, "D" & cellRowIndex.ToString).Merge() '入庫見出し
                    sheet.Range("E" & cellRowIndex.ToString, "F" & cellRowIndex.ToString).Merge() '出庫見出し
                    sheet.Range("C" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "GoodsReceipt", "入庫")
                    sheet.Range("E" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "GoodsDelivery", "出庫")
                    sheet.Range("C" & cellRowIndex.ToString).HorizontalAlignment = Constants.xlCenter
                    sheet.Range("E" & cellRowIndex.ToString).HorizontalAlignment = Constants.xlCenter

                    cellRowIndex += 1 '次の行

                    sheet.Range("C" & cellRowIndex.ToString).HorizontalAlignment = Constants.xlLeft
                    sheet.Range("D" & cellRowIndex.ToString).HorizontalAlignment = Constants.xlLeft
                    sheet.Range("E" & cellRowIndex.ToString).HorizontalAlignment = Constants.xlLeft
                    sheet.Range("F" & cellRowIndex.ToString).HorizontalAlignment = Constants.xlLeft
                    sheet.Range("G" & cellRowIndex.ToString).HorizontalAlignment = Constants.xlLeft

                    '年月日、取引先、入庫（数量、単価）、出庫（数量、単価）、在庫数、備考

                    sheet.Range("A" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "Date", "年月日")
                    sheet.Range("B" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "Suppliers", "取引先")
                    sheet.Range("C" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "Quantity", "数量")
                    sheet.Range("D" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "UnitPrice", "単価")
                    sheet.Range("E" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "Quantity", "数量")
                    sheet.Range("F" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "UnitPrice", "単価")
                    sheet.Range("G" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "StockQuantity", "在庫数")
                    sheet.Range("H" & cellRowIndex.ToString).Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG, "Remarks", "備考")

                    sheet.Range("A" & cellRowIndex.ToString, "H" & cellRowIndex.ToString).Borders(XlBordersIndex.xlEdgeBottom).LineStyle = XlLineStyle.xlContinuous　　　　　'底の罫線

                    cellRowIndex += 1 '次の行

                End If

                '年月日、取引先、入庫（数量、単価）、出庫（数量、単価）、在庫数、備考
                sheet.Range("A" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("年月日").Value
                sheet.Range("B" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("取引先").Value
                sheet.Range("C" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("入庫数量").Value
                sheet.Range("D" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("入庫単価").Value
                sheet.Range("E" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("出庫数量").Value
                sheet.Range("F" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("出庫単価").Value
                sheet.Range("G" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("在庫数").Value
                sheet.Range("H" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("備考").Value

                sheet.Range("A" & cellRowIndex.ToString, "H" & cellRowIndex.ToString).Borders(XlBordersIndex.xlEdgeBottom).LineStyle = XlLineStyle.xlContinuous　　　　　'底の罫線
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

End Class