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
Imports System.IO

Public Class InventoryList
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

        LblMode.Text = "参照モード"

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblMode.Text = OrderStatus & " Mode"

            rbAll.Text = "By product"
            rbWarehouse.Text = "By Warehouse"

            BtnExcelOutput.Text = "ExcelOutput"
            BtnBack.Text = "Back"

        End If

        getList() '一覧表示

    End Sub

    '商品別の見出しセット
    Private Sub setProductHd()

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            DgvList.Columns.Add("メーカー", "Manufacturer")
            DgvList.Columns.Add("品名", "ItemName")
            DgvList.Columns.Add("型式", "Spec")
            DgvList.Columns.Add("在庫数", "StockQuantity")
            DgvList.Columns.Add("入出庫種別", "StorageType")
            DgvList.Columns.Add("単価（入庫単価）", "UnitPrice (ReceiptUnitPrice)")
            DgvList.Columns.Add("最終入庫日", "LastReceiptDate")
            DgvList.Columns.Add("最終出庫日", "LastDeliveryDate")
        Else
            DgvList.Columns.Add("メーカー", "メーカー")
            DgvList.Columns.Add("品名", "品名")
            DgvList.Columns.Add("型式", "型式")
            DgvList.Columns.Add("在庫数", "在庫数")
            DgvList.Columns.Add("入出庫種別", "入出庫種別")
            DgvList.Columns.Add("単価（入庫単価）", "単価（入庫単価）")
            DgvList.Columns.Add("最終入庫日", "最終入庫日")
            DgvList.Columns.Add("最終出庫日", "最終出庫日")


        End If

        DgvList.Columns("在庫数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    End Sub

    '倉庫別の見出しセット
    Private Sub setWarehouseHd()

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            DgvList.Columns.Add("倉庫", "Warehouse")
            DgvList.Columns.Add("メーカー", "Manufacturer")
            DgvList.Columns.Add("品名", "ItemName")
            DgvList.Columns.Add("型式", "Spec")
            DgvList.Columns.Add("在庫数", "StockQuantity")
            DgvList.Columns.Add("入出庫種別", "StorageType")
            DgvList.Columns.Add("単価（入庫単価）", "UnitPrice (ReceiptUnitPrice)")
            DgvList.Columns.Add("最終入庫日", "LastReceiptDate")
            DgvList.Columns.Add("最終出庫日", "LastDeliveryDate")
        Else
            DgvList.Columns.Add("倉庫", "倉庫")
            DgvList.Columns.Add("メーカー", "メーカー")
            DgvList.Columns.Add("品名", "品名")
            DgvList.Columns.Add("型式", "型式")
            DgvList.Columns.Add("在庫数", "在庫数")
            DgvList.Columns.Add("入出庫種別", "入出庫種別")
            DgvList.Columns.Add("単価（入庫単価）", "単価（入庫単価）")
            DgvList.Columns.Add("最終入庫日", "最終入庫日")
            DgvList.Columns.Add("最終出庫日", "最終出庫日")

        End If

        DgvList.Columns("在庫数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

    End Sub

    Private Sub getList()
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        DgvList.Columns.Clear() '見出しクリア
        DgvList.Rows.Clear() '一覧クリア

        '商品別処理
        If rbAll.Checked Then

            setProductHd() '商品別の見出しセット

            Try

                Sql = "SELECT "
                Sql += " m21.メーカー, m21.品名, m21.型式, m21.入出庫種別, sum(m21.現在庫数) as 現在庫数 "
                Sql += " ,m21.入庫単価, m21.最終入庫日, m21.最終出庫日, m90.文字１, m90.文字２ "
                Sql += " FROM m21_zaiko m21"

                Sql += " LEFT JOIN "
                Sql += " m90_hanyo m90 "
                Sql += " ON "
                Sql += " m90.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " AND "
                Sql += " m90.固定キー ILIKE '" & CommonConst.INOUT_CLASS & "'"
                Sql += " AND "
                Sql += " m90.可変キー ILIKE m21.入出庫種別 "

                Sql += " WHERE "
                Sql += " m21.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " AND "
                Sql += " m21.無効フラグ = " & CommonConst.CANCEL_KBN_ENABLED.ToString
                Sql += " AND "
                Sql += " m21.現在庫数 <> 0 "
                Sql += " GROUP BY "
                Sql += " m21.メーカー, m21.品名, m21.型式, m21.入出庫種別 "
                Sql += " ,m21.入庫単価, m21.最終入庫日, m21.最終出庫日, m90.文字１, m90.文字２, m21.伝票番号 "
                Sql += " ORDER BY "
                'Sql += " m21.メーカー, m21.品名, m21.型式, m21.最終入庫日, m21.入出庫種別 "
                Sql += " m21.メーカー, m21.品名, m21.型式, m21.伝票番号, m21.入出庫種別 "

                Dim dsList As DataSet = _db.selectDB(Sql, RS, reccnt)

                Dim currentManufacturer As String = ""
                Dim currentItemName As String = ""
                Dim currentSpec As String = ""
                Dim productFlg As Boolean = False

                For i As Integer = 0 To dsList.Tables(RS).Rows.Count - 1

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

                    DgvList.Rows.Add()
                    DgvList.Rows(i).Cells("メーカー").Value = IIf(productFlg, "", currentManufacturer)
                    DgvList.Rows(i).Cells("品名").Value = IIf(productFlg, "", currentItemName)
                    DgvList.Rows(i).Cells("型式").Value = IIf(productFlg, "", currentSpec)
                    DgvList.Rows(i).Cells("在庫数").Value = dsList.Tables(RS).Rows(i)("現在庫数")
                    DgvList.Rows(i).Cells("入出庫種別").Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                                                    dsList.Tables(RS).Rows(i)("文字２"),
                                                                    dsList.Tables(RS).Rows(i)("文字１"))
                    DgvList.Rows(i).Cells("単価（入庫単価）").Value = dsList.Tables(RS).Rows(i)("入庫単価")
                    DgvList.Rows(i).Cells("最終入庫日").Value = dsList.Tables(RS).Rows(i)("最終入庫日")
                    DgvList.Rows(i).Cells("最終出庫日").Value = dsList.Tables(RS).Rows(i)("最終出庫日")

                Next

            Catch ue As UsrDefException
                ue.dspMsg()
                Throw ue
            Catch ex As Exception
                'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
            End Try

        Else
            '倉庫別処理

            setWarehouseHd() '倉庫別の見出しセット

            Try

                Sql = "SELECT "
                Sql += " m21.メーカー, m21.品名, m21.型式, m21.入出庫種別, sum(m21.現在庫数) as 現在庫数 "
                Sql += " ,m21.入庫単価, m21.最終入庫日, m21.最終出庫日, m90.文字１, m90.文字２, m20.名称 "
                Sql += " FROM m21_zaiko m21"

                Sql += " LEFT JOIN "
                Sql += " m90_hanyo m90 "
                Sql += " ON "
                Sql += " m90.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " AND "
                Sql += " m90.固定キー ILIKE '" & CommonConst.INOUT_CLASS & "'"
                Sql += " AND "
                Sql += " m90.可変キー ILIKE m21.入出庫種別 "

                Sql += " LEFT JOIN "
                Sql += " m20_warehouse m20 "
                Sql += " ON "
                Sql += " m20.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " AND "
                Sql += " m20.倉庫コード ILIKE m21.倉庫コード"

                Sql += " WHERE "
                Sql += " m21.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " AND "
                Sql += " m21.無効フラグ = " & CommonConst.CANCEL_KBN_ENABLED.ToString
                Sql += " AND "
                Sql += " m21.現在庫数 <> 0 "
                Sql += " GROUP BY "
                Sql += " m21.倉庫コード, m21.メーカー, m21.品名, m21.型式, m21.入出庫種別, m21.現在庫数 "
                Sql += " ,m21.入庫単価, m21.最終入庫日, m21.最終出庫日, m90.文字１, m90.文字２, m20.名称, m21.伝票番号 "
                Sql += " ORDER BY "
                'Sql += " m21.メーカー, m21.品名, m21.型式, m21.最終入庫日, m21.入出庫種別 "
                Sql += " m21.倉庫コード, m21.メーカー, m21.品名, m21.型式, m21.伝票番号, m21.入出庫種別 "

                Dim dsList As DataSet = _db.selectDB(Sql, RS, reccnt)

                Dim currentWarehouse As String = ""
                Dim warehouseFlg As Boolean = False

                For i As Integer = 0 To dsList.Tables(RS).Rows.Count - 1

                    If currentWarehouse <> dsList.Tables(RS).Rows(i)("名称").ToString Then
                        currentWarehouse = dsList.Tables(RS).Rows(i)("名称").ToString

                        warehouseFlg = False
                    Else
                        warehouseFlg = True
                    End If

                    DgvList.Rows.Add()
                    DgvList.Rows(i).Cells("倉庫").Value = IIf(warehouseFlg, "", currentWarehouse)
                    DgvList.Rows(i).Cells("メーカー").Value = dsList.Tables(RS).Rows(i)("メーカー")
                    DgvList.Rows(i).Cells("品名").Value = dsList.Tables(RS).Rows(i)("品名")
                    DgvList.Rows(i).Cells("型式").Value = dsList.Tables(RS).Rows(i)("型式")
                    DgvList.Rows(i).Cells("在庫数").Value = dsList.Tables(RS).Rows(i)("現在庫数")
                    DgvList.Rows(i).Cells("入出庫種別").Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                                                    dsList.Tables(RS).Rows(i)("文字２"),
                                                                    dsList.Tables(RS).Rows(i)("文字１"))
                    DgvList.Rows(i).Cells("単価（入庫単価）").Value = dsList.Tables(RS).Rows(i)("入庫単価")
                    DgvList.Rows(i).Cells("最終入庫日").Value = dsList.Tables(RS).Rows(i)("最終入庫日")
                    DgvList.Rows(i).Cells("最終出庫日").Value = dsList.Tables(RS).Rows(i)("最終出庫日")

                Next

            Catch ue As UsrDefException
                ue.dspMsg()
                Throw ue
            Catch ex As Exception
                'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
            End Try

        End If


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

    'excel出力処理
    Private Sub outputExcel()
        Dim outputDate As Date = DateTime.Now.ToShortDateString

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
            Dim sHinaFile As String = ""
            '出力先パス
            Dim sOutPath As String = StartUp._iniVal.OutXlsPath
            '出力ファイル名
            Dim sOutFile As String = ""

            '商品別処理
            If rbAll.Checked Then
                sHinaFile = sHinaPath & "\" & "StockProductList.xlsx"
                sOutFile = sOutPath & "\StockProductList_" & DateTime.Now.ToString("yyyyMMddHHmm") & ".xlsx"
            Else
                sHinaFile = sHinaPath & "\" & "StockWarehouseList.xlsx"
                sOutFile = sOutPath & "\StockWarehouseList_" & DateTime.Now.ToString("yyyyMMddHHmm") & ".xlsx"
            End If

            app = New Excel.Application()
            book = app.Workbooks.Add(sHinaFile)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet)

            '商品別処理
            If rbAll.Checked Then
                If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                    sheet.PageSetup.LeftHeader = "StockProductList"
                    sheet.PageSetup.RightHeader = "OutputDate：" & DateTime.Now.ToShortDateString

                    sheet.Range("A1").Value = "Manufacturer"
                    sheet.Range("B1").Value = "ItemName"
                    sheet.Range("C1").Value = "Spec"
                    sheet.Range("D1").Value = "StockQuantity"
                    sheet.Range("E1").Value = "StorageType"
                    sheet.Range("F1").Value = "UnitPrice (ReceiptUnitPrice)"
                    sheet.Range("G1").Value = "LastReceiptDate"
                    sheet.Range("H1").Value = "LastDeliveryDate"

                End If

                For i As Integer = 0 To DgvList.RowCount - 1
                    Dim cellRowIndex As Integer = 2
                    cellRowIndex += i

                    sheet.Range("A" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("メーカー").Value
                    sheet.Range("B" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("品名").Value
                    sheet.Range("C" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("型式").Value
                    sheet.Range("D" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("在庫数").Value
                    sheet.Range("E" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("入出庫種別").Value
                    sheet.Range("F" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("単価（入庫単価）").Value
                    sheet.Range("G" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("最終入庫日").Value
                    sheet.Range("H" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("最終出庫日").Value

                Next

            Else

                If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                    sheet.PageSetup.LeftHeader = "StockWarehouseList"
                    sheet.PageSetup.RightHeader = "OutputDate：" & DateTime.Now.ToShortDateString

                    sheet.Range("A1").Value = "WarehouseName"
                    sheet.Range("B1").Value = "Manufacturer"
                    sheet.Range("C1").Value = "ItemName"
                    sheet.Range("D1").Value = "Spec"
                    sheet.Range("E1").Value = "StockQuantity"
                    sheet.Range("F1").Value = "StorageType"
                    sheet.Range("G1").Value = "UnitPrice (ReceiptUnitPrice)"
                    sheet.Range("H1").Value = "LastReceiptDate"
                    sheet.Range("I1").Value = "LastDeliveryDate"
                End If

                For i As Integer = 0 To DgvList.RowCount - 1
                    Dim cellRowIndex As Integer = 2
                    cellRowIndex += i

                    sheet.Range("A" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("倉庫").Value
                    sheet.Range("B" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("メーカー").Value
                    sheet.Range("C" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("品名").Value
                    sheet.Range("D" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("型式").Value
                    sheet.Range("E" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("在庫数").Value
                    sheet.Range("F" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("入出庫種別").Value
                    sheet.Range("G" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("単価（入庫単価）").Value
                    sheet.Range("H" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("最終入庫日").Value
                    sheet.Range("I" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("最終出庫日").Value

                Next
            End If

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

    Private Sub rbWarehouse_CheckedChanged(sender As Object, e As EventArgs) Handles rbWarehouse.CheckedChanged
        getList()
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