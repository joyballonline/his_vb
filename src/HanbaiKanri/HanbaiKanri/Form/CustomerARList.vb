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

Public Class CustomerARList
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
        DgvCymndt.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells
    End Sub

    '画面表示時
    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        LblMode.Text = "参照モード"

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblMode.Text = OrderStatus & " Mode"

            BtnExcelOutput.Text = "ExcelOutput"
            BtnBack.Text = "Back"

            DgvCymndt.Columns("得意先名").HeaderText = "CustomerName"
            DgvCymndt.Columns("請求番号").HeaderText = "SalesInvoiceNo"
            DgvCymndt.Columns("請求日").HeaderText = "SalesInvoiceDate"
            DgvCymndt.Columns("請求金額").HeaderText = "TotalBillingAmount"
            DgvCymndt.Columns("入金額").HeaderText = "MoneyReceiptAmount"
            DgvCymndt.Columns("売掛金残高").HeaderText = "ARBalance"
            DgvCymndt.Columns("備考").HeaderText = "Remarks"

        End If

        '数字形式
        DgvCymndt.Columns("請求金額").DefaultCellStyle.Format = "N2"
        DgvCymndt.Columns("入金額").DefaultCellStyle.Format = "N2"
        DgvCymndt.Columns("売掛金残高").DefaultCellStyle.Format = "N2"


        Dim dsSkyuhd As DataSet = getARList()

        Dim tmpCustomerCd As String = ""
        Dim tmpCustomerName As String = ""

        For i As Integer = 0 To dsSkyuhd.Tables(RS).Rows.Count - 1

            ''得意先コードが変わったら取得
            'If (tmpCustomerCd <> dsSkyuhd.Tables(RS).Rows(i)("得意先コード").ToString) Then
            '    tmpCustomerName = dsSkyuhd.Tables(RS).Rows(i)("得意先名")
            '    tmpCustomerCd = dsSkyuhd.Tables(RS).Rows(i)("得意先コード").ToString
            'Else
            '    tmpCustomerName = ""
            'End If
            tmpCustomerName = dsSkyuhd.Tables(RS).Rows(i)("得意先名")
            tmpCustomerCd = dsSkyuhd.Tables(RS).Rows(i)("得意先コード").ToString

            DgvCymndt.Rows.Add()
            DgvCymndt.Rows(i).Cells("得意先名").Value = tmpCustomerName
            DgvCymndt.Rows(i).Cells("請求番号").Value = dsSkyuhd.Tables(RS).Rows(i)("請求番号")
            DgvCymndt.Rows(i).Cells("請求日").Value = dsSkyuhd.Tables(RS).Rows(i)("請求日").ToShortDateString()
            DgvCymndt.Rows(i).Cells("請求金額").Value = dsSkyuhd.Tables(RS).Rows(i)("請求金額計")
            DgvCymndt.Rows(i).Cells("入金額").Value = dsSkyuhd.Tables(RS).Rows(i)("入金額計")
            DgvCymndt.Rows(i).Cells("売掛金残高").Value = dsSkyuhd.Tables(RS).Rows(i)("売掛残高")
            DgvCymndt.Rows(i).Cells("備考").Value = dsSkyuhd.Tables(RS).Rows(i)("備考1")

        Next


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
        If DgvCymndt.Rows.Count = 0 Then

            '該当データがないアラートを出す
            _msgHd.dspMSG("noTargetData", frmC01F10_Login.loginValue.Language)

            Return

        Else

            'Excel出力処理
            outputExcel()
        End If
    End Sub

    Public Function getARList()
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Sql = " AND "
        Sql += "売掛残高 > 0 "
        Sql += " AND "
        Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '取消区分=0
        Sql += " ORDER BY 得意先コード, 請求日 "

        Try
            Dim dsSkyuhd As DataSet = getDsData("t23_skyuhd", Sql)

            Return dsSkyuhd

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Function

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
            Dim sHinaFile As String = sHinaPath & "\" & "CustomerARList.xlsx"
            '出力先パス
            Dim sOutPath As String = StartUp._iniVal.OutXlsPath
            '出力ファイル名
            Dim sOutFile As String = sOutPath & "\CustomerARList_" & DateTime.Now.ToString("yyyyMMddHHmm") & ".xlsx"

            app = New Excel.Application()
            book = app.Workbooks.Add(sHinaFile)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet)

            sheet.PageSetup.RightHeader = "出力日：" & DateTime.Now.ToShortDateString

            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                sheet.PageSetup.LeftHeader = "Customer AR list"
                sheet.PageSetup.RightHeader = "OutputDate：" & DateTime.Now.ToShortDateString

                sheet.Range("A1").Value = "CustomerName"
                sheet.Range("B1").Value = "SalesInvoiceNo"
                sheet.Range("C1").Value = "SalesInvoiceDate"
                sheet.Range("D1").Value = "TotalBillingAmount"
                sheet.Range("E1").Value = "MoneyReceiptAmount"
                sheet.Range("F1").Value = "ARBalance"
                sheet.Range("G1").Value = "Remarks"

            End If

            For i As Integer = 0 To DgvCymndt.RowCount - 1
                Dim cellRowIndex As Integer = 2
                cellRowIndex += i

                sheet.Range("A" & cellRowIndex.ToString).Value = DgvCymndt.Rows(i).Cells("得意先名").Value
                sheet.Range("B" & cellRowIndex.ToString).Value = DgvCymndt.Rows(i).Cells("請求番号").Value
                sheet.Range("C" & cellRowIndex.ToString).Value = DgvCymndt.Rows(i).Cells("請求日").Value
                sheet.Range("D" & cellRowIndex.ToString).Value = DgvCymndt.Rows(i).Cells("請求金額").Value
                sheet.Range("E" & cellRowIndex.ToString).Value = DgvCymndt.Rows(i).Cells("入金額").Value
                sheet.Range("F" & cellRowIndex.ToString).Value = DgvCymndt.Rows(i).Cells("売掛金残高").Value
                sheet.Range("G" & cellRowIndex.ToString).Value = DgvCymndt.Rows(i).Cells("備考").Value

            Next

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