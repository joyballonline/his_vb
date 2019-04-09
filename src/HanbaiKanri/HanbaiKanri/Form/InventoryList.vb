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
            DgvList.Columns.Add("在庫数", "StockQuontity")
            'DgvList.Columns.Add("単位", "Unit")
        Else
            DgvList.Columns.Add("メーカー", "メーカー")
            DgvList.Columns.Add("品名", "品名")
            DgvList.Columns.Add("型式", "型式")
            DgvList.Columns.Add("在庫数", "在庫数")
            'DgvList.Columns.Add("単位", "単位")

            DgvList.Columns("在庫数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        End If

    End Sub

    '倉庫別の見出しセット
    Private Sub setWarehouseHd()

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            DgvList.Columns.Add("倉庫", "Warehouse")
            DgvList.Columns.Add("メーカー", "Manufacturer")
            DgvList.Columns.Add("品名", "ItemName")
            DgvList.Columns.Add("型式", "Spec")
            DgvList.Columns.Add("在庫数", "StockQuontity")
            'DgvList.Columns.Add("単位", "Unit")
        Else
            DgvList.Columns.Add("倉庫", "倉庫")
            DgvList.Columns.Add("メーカー", "メーカー")
            DgvList.Columns.Add("品名", "品名")
            DgvList.Columns.Add("型式", "型式")
            DgvList.Columns.Add("在庫数", "在庫数")
            'DgvList.Columns.Add("単位", "単位")

            DgvList.Columns("在庫数").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        End If

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

                '入庫データの取得
                '
                Sql = " select "
                'Sql += " t43.メーカー, t43.品名, t43.型式, sum(t43.入庫数量) as 入庫数量, t43.単位 "
                Sql += " t43.メーカー, t43.品名, t43.型式, sum(t43.入庫数量) as 入庫数量 "
                Sql += " from "
                Sql += " t43_nyukodt t43 "

                Sql += " LEFT JOIN "
                Sql += " t42_nyukohd t42 "
                Sql += " ON  t43.会社コード = t42.会社コード "
                Sql += " AND  t43.入庫番号 = t42.入庫番号 "

                Sql += " where "
                Sql += " t43.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " AND "
                Sql += " t42.取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'"
                Sql += " GROUP BY "
                'Sql += " t43.メーカー, t43.品名, t43.型式, t43.単位 "
                Sql += " t43.メーカー, t43.品名, t43.型式 "
                Sql += " order by "
                Sql += " t43.メーカー, t43.品名, t43.型式 "

                Dim dsNyuko As DataSet = _db.selectDB(Sql, RS, reccnt)

                '出庫データの取得
                '
                Sql = " select "
                'Sql += " t45.メーカー, t45.品名, t45.型式, sum(t45.出庫数量) as 出庫数量, t45.単位 "
                Sql += " t45.メーカー, t45.品名, t45.型式, sum(t45.出庫数量) as 出庫数量 "
                Sql += " from "
                Sql += " t45_shukodt t45 "

                Sql += " LEFT JOIN "
                Sql += " t44_shukohd t44 "
                Sql += " ON  t45.会社コード = t44.会社コード "
                Sql += " AND  t45.出庫番号 = t44.出庫番号 "

                Sql += " where "
                Sql += " t45.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " AND "
                Sql += " t44.取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'"
                Sql += " GROUP BY "
                'Sql += " t45.メーカー, t45.品名, t45.型式, t45.単位 "
                Sql += " t45.メーカー, t45.品名, t45.型式 "
                Sql += " order by "
                Sql += " t45.メーカー, t45.品名, t45.型式 "

                Dim dsShukko As DataSet = _db.selectDB(Sql, RS, reccnt)
                Dim chkFlg As Boolean = False

                For i As Integer = 0 To dsNyuko.Tables(RS).Rows.Count - 1 '入庫データ
                    For x As Integer = 0 To dsShukko.Tables(RS).Rows.Count - 1 '出庫データ

                        '一致したら 入庫数 - 出庫数
                        '入庫あり、出庫なしの場合はフラグを見て入庫データのみで作成する
                        If dsNyuko.Tables(RS).Rows(i)("メーカー").ToString() = dsShukko.Tables(RS).Rows(x)("メーカー").ToString() And
                             dsNyuko.Tables(RS).Rows(i)("品名").ToString() = dsShukko.Tables(RS).Rows(x)("品名").ToString() And
                              dsNyuko.Tables(RS).Rows(i)("型式").ToString() = dsShukko.Tables(RS).Rows(x)("型式").ToString() Then

                            DgvList.Rows.Add()
                            DgvList.Rows(i).Cells("メーカー").Value = dsNyuko.Tables(RS).Rows(i)("メーカー")
                            DgvList.Rows(i).Cells("品名").Value = dsNyuko.Tables(RS).Rows(i)("品名")
                            DgvList.Rows(i).Cells("型式").Value = dsNyuko.Tables(RS).Rows(i)("型式")
                            DgvList.Rows(i).Cells("在庫数").Value = dsNyuko.Tables(RS).Rows(i)("入庫数量") - dsShukko.Tables(RS).Rows(x)("出庫数量")
                            'DgvList.Rows(i).Cells("単位").Value = dsNyuko.Tables(RS).Rows(i)("単位")

                            chkFlg = True '入庫データと出庫データがある場合は true

                        End If

                    Next

                    '出庫データがなかった場合
                    If chkFlg = False Then

                        DgvList.Rows.Add()
                        DgvList.Rows(i).Cells("メーカー").Value = dsNyuko.Tables(RS).Rows(i)("メーカー")
                        DgvList.Rows(i).Cells("品名").Value = dsNyuko.Tables(RS).Rows(i)("品名")
                        DgvList.Rows(i).Cells("型式").Value = dsNyuko.Tables(RS).Rows(i)("型式")
                        DgvList.Rows(i).Cells("在庫数").Value = dsNyuko.Tables(RS).Rows(i)("入庫数量")
                        'DgvList.Rows(i).Cells("単位").Value = dsNyuko.Tables(RS).Rows(i)("単位")

                    Else
                        chkFlg = False '初期化
                    End If

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

                '入庫データの取得
                '
                Sql = " select "
                'Sql += " t43.メーカー, t43.品名, t43.型式, sum(t43.入庫数量) as 入庫数量, t43.単位, t42.倉庫コード, m20.名称 "
                Sql += " t43.メーカー, t43.品名, t43.型式, sum(t43.入庫数量) as 入庫数量, t42.倉庫コード, m20.名称 "
                Sql += " from "
                Sql += " t43_nyukodt t43 "

                Sql += " LEFT JOIN "
                Sql += " t42_nyukohd t42 "
                Sql += " ON  t43.会社コード = t42.会社コード "
                Sql += " AND  t43.入庫番号 = t42.入庫番号 "

                Sql += " LEFT JOIN "
                Sql += " m20_warehouse m20 "
                Sql += " ON  t43.会社コード = m20.会社コード "
                Sql += " AND  t42.倉庫コード = m20.倉庫コード "

                Sql += " where "
                Sql += " t43.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " AND "
                Sql += " t42.取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'"

                Sql += " GROUP BY "
                'Sql += " t43.メーカー, t43.品名, t43.型式, t43.単位, t42.倉庫コード, m20.名称 "
                Sql += " t43.メーカー, t43.品名, t43.型式, t42.倉庫コード, m20.名称 "

                Sql += " order by "
                Sql += " t42.倉庫コード, t43.メーカー, t43.品名, t43.型式 "

                Dim dsNyuko As DataSet = _db.selectDB(Sql, RS, reccnt)

                '出庫データの取得
                '
                Sql = " select "
                'Sql += " t45.メーカー, t45.品名, t45.型式, sum(t45.出庫数量) as 出庫数量, t45.単位, t45.倉庫コード, m20.名称 "
                Sql += " t45.メーカー, t45.品名, t45.型式, sum(t45.出庫数量) as 出庫数量, t45.倉庫コード, m20.名称 "
                Sql += " from "
                Sql += " t45_shukodt t45 "

                Sql += " LEFT JOIN "
                Sql += " t44_shukohd t44 "
                Sql += " ON  t45.会社コード = t44.会社コード "
                Sql += " AND  t45.出庫番号 = t44.出庫番号 "

                Sql += " LEFT JOIN "
                Sql += " m20_warehouse m20 "
                Sql += " ON  t45.会社コード = m20.会社コード "
                Sql += " AND  t45.倉庫コード = m20.倉庫コード "

                Sql += " where "
                Sql += " t45.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " AND "
                Sql += " t44.取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'"
                Sql += " GROUP BY "
                'Sql += " t45.メーカー, t45.品名, t45.型式, t45.単位, t45.倉庫コード, m20.名称 "
                Sql += " t45.メーカー, t45.品名, t45.型式, t45.倉庫コード, m20.名称 "
                Sql += " order by "
                Sql += " t45.倉庫コード, t45.メーカー, t45.品名, t45.型式 "

                Dim dsShukko As DataSet = _db.selectDB(Sql, RS, reccnt)

                Dim tmpWarehouseCd As String = ""
                Dim tmpWarehouseName As String = ""
                Dim chkFlg As Boolean = False


                For i As Integer = 0 To dsNyuko.Tables(RS).Rows.Count - 1 '入庫データ

                    '得意先コードが変わったら取得
                    If (tmpWarehouseCd <> dsNyuko.Tables(RS).Rows(i)("倉庫コード").ToString) Then
                        tmpWarehouseName = dsNyuko.Tables(RS).Rows(i)("名称").ToString
                        tmpWarehouseCd = dsNyuko.Tables(RS).Rows(i)("倉庫コード").ToString
                    Else
                        tmpWarehouseName = ""
                    End If

                    For x As Integer = 0 To dsShukko.Tables(RS).Rows.Count - 1 '出庫データ

                        '一致したら 入庫数 - 出庫数
                        If dsNyuko.Tables(RS).Rows(i)("倉庫コード") = dsShukko.Tables(RS).Rows(x)("倉庫コード") And
                                dsNyuko.Tables(RS).Rows(i)("メーカー") = dsShukko.Tables(RS).Rows(x)("メーカー") And
                                 dsNyuko.Tables(RS).Rows(i)("品名") = dsShukko.Tables(RS).Rows(x)("品名") And
                                  dsNyuko.Tables(RS).Rows(i)("型式") = dsShukko.Tables(RS).Rows(x)("型式") Then

                            DgvList.Rows.Add()
                            DgvList.Rows(i).Cells("倉庫").Value = tmpWarehouseName
                            DgvList.Rows(i).Cells("メーカー").Value = dsNyuko.Tables(RS).Rows(i)("メーカー")
                            DgvList.Rows(i).Cells("品名").Value = dsNyuko.Tables(RS).Rows(i)("品名")
                            DgvList.Rows(i).Cells("型式").Value = dsNyuko.Tables(RS).Rows(i)("型式")
                            DgvList.Rows(i).Cells("在庫数").Value = dsNyuko.Tables(RS).Rows(i)("入庫数量") - dsShukko.Tables(RS).Rows(x)("出庫数量")
                            'DgvList.Rows(i).Cells("単位").Value = dsNyuko.Tables(RS).Rows(i)("単位")

                            chkFlg = True '入庫データと出庫データがある場合は true
                        End If

                    Next

                    '出庫データがなかった場合
                    If chkFlg = False Then

                        DgvList.Rows.Add()
                        DgvList.Rows(i).Cells("倉庫").Value = tmpWarehouseName
                        DgvList.Rows(i).Cells("メーカー").Value = dsNyuko.Tables(RS).Rows(i)("メーカー")
                        DgvList.Rows(i).Cells("品名").Value = dsNyuko.Tables(RS).Rows(i)("品名")
                        DgvList.Rows(i).Cells("型式").Value = dsNyuko.Tables(RS).Rows(i)("型式")
                        DgvList.Rows(i).Cells("在庫数").Value = dsNyuko.Tables(RS).Rows(i)("入庫数量")
                        'DgvList.Rows(i).Cells("単位").Value = dsNyuko.Tables(RS).Rows(i)("単位")

                    Else
                        chkFlg = False '初期化
                    End If

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
                    sheet.Range("D1").Value = "StockQuontity"
                    'sheet.Range("E1").Value = "Unit"
                End If

                For i As Integer = 0 To DgvList.RowCount - 1
                    Dim cellRowIndex As Integer = 2
                    cellRowIndex += i

                    sheet.Range("A" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("メーカー").Value
                    sheet.Range("B" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("品名").Value
                    sheet.Range("C" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("型式").Value
                    sheet.Range("D" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("在庫数").Value
                    'sheet.Range("E" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("単位").Value

                Next

            Else

                If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                    sheet.PageSetup.LeftHeader = "StockWarehouseList"
                    sheet.PageSetup.RightHeader = "OutputDate：" & DateTime.Now.ToShortDateString

                    sheet.Range("A1").Value = "WarehouseName"
                    sheet.Range("B1").Value = "Manufacturer"
                    sheet.Range("C1").Value = "ItemName"
                    sheet.Range("D1").Value = "Spec"
                    sheet.Range("E1").Value = "StockQuontity"
                    'sheet.Range("F1").Value = "Unit"
                End If

                For i As Integer = 0 To DgvList.RowCount - 1
                    Dim cellRowIndex As Integer = 2
                    cellRowIndex += i

                    sheet.Range("A" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("倉庫").Value
                    sheet.Range("B" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("メーカー").Value
                    sheet.Range("C" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("品名").Value
                    sheet.Range("D" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("型式").Value
                    sheet.Range("E" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("在庫数").Value
                    'sheet.Range("F" & cellRowIndex.ToString).Value = DgvList.Rows(i).Cells("単位").Value

                Next
            End If

            book.SaveAs(sOutFile)

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
End Class