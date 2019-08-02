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

Public Class SupplierAPList
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

        Dim Sql As String

        Dim curds As DataSet  'm25_currency
        Dim cur As String


        LblMode.Text = "参照モード"

        '言語判定
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then  '英語
            LblMode.Text = OrderStatus & " Mode"

            BtnExcelOutput.Text = "ExcelOutput"
            BtnBack.Text = "Back"

            DgvCymndt.Columns("仕入先名").HeaderText = "SupplierName"
            DgvCymndt.Columns("発注番号").HeaderText = "PurchaseOrderNumber"
            DgvCymndt.Columns("買掛日").HeaderText = "AccountsPayableDate"

            DgvCymndt.Columns("通貨_外貨").HeaderText = "Currency" & vbCrLf & "(OriginalCurrency)"
            DgvCymndt.Columns("買掛金額計_外貨").HeaderText = "TotalAccountsPayable" & vbCrLf & "(OriginalCurrency)"
            DgvCymndt.Columns("支払金額計_外貨").HeaderText = "TotalPaymentAmount" & vbCrLf & "(OriginalCurrency)"
            DgvCymndt.Columns("買掛金残高_外貨").HeaderText = "APBalance" & vbCrLf & "(OriginalCurrency)"

            DgvCymndt.Columns("通貨").HeaderText = "Currency"
            DgvCymndt.Columns("買掛金額計").HeaderText = "TotalAccountsPayable"
            DgvCymndt.Columns("支払金額計").HeaderText = "TotalPaymentAmount"
            DgvCymndt.Columns("買掛金残高").HeaderText = "APBalance"
            DgvCymndt.Columns("備考").HeaderText = "Remarks"

        Else  '日本語

            DgvCymndt.Columns("通貨_外貨").HeaderText = "通貨" & vbCrLf & "(原通貨)"
            DgvCymndt.Columns("買掛金額計_外貨").HeaderText = "買掛金額計" & vbCrLf & "(原通貨)"
            DgvCymndt.Columns("支払金額計_外貨").HeaderText = "支払金額計" & vbCrLf & "(原通貨)"
            DgvCymndt.Columns("買掛金残高_外貨").HeaderText = "買掛金残高" & vbCrLf & "(原通貨)"
        End If

        Dim dsKikehd As DataSet = getAPList()

        Dim tmpSupplierCd As String = ""
        Dim tmpSupplierName As String = ""

        For i As Integer = 0 To dsKikehd.Tables(RS).Rows.Count - 1

            If IsDBNull(dsKikehd.Tables(RS).Rows(i)("通貨")) Then
                cur = vbNullString
            Else
                Sql = " and 採番キー = " & dsKikehd.Tables(RS).Rows(i)("通貨")
                curds = getDsData("m25_currency", Sql)

                cur = curds.Tables(RS).Rows(0)("通貨コード")
            End If

            '得意先コードが変わったら取得
            If (tmpSupplierCd <> dsKikehd.Tables(RS).Rows(i)("仕入先コード").ToString) Then
                tmpSupplierName = dsKikehd.Tables(RS).Rows(i)("仕入先名")
                tmpSupplierCd = dsKikehd.Tables(RS).Rows(i)("仕入先コード").ToString
            Else
                tmpSupplierName = ""
            End If

            DgvCymndt.Rows.Add()
            DgvCymndt.Rows(i).Cells("仕入先名").Value = tmpSupplierName
            DgvCymndt.Rows(i).Cells("発注番号").Value = dsKikehd.Tables(RS).Rows(i)("発注番号")
            DgvCymndt.Rows(i).Cells("買掛日").Value = dsKikehd.Tables(RS).Rows(i)("買掛日").ToShortDateString()

            DgvCymndt.Rows(i).Cells("通貨_外貨").Value = cur
            DgvCymndt.Rows(i).Cells("買掛金額計_外貨").Value = dsKikehd.Tables(RS).Rows(i)("買掛金額計_外貨")
            DgvCymndt.Rows(i).Cells("支払金額計_外貨").Value = dsKikehd.Tables(RS).Rows(i)("支払金額計_外貨")
            DgvCymndt.Rows(i).Cells("買掛金残高_外貨").Value = dsKikehd.Tables(RS).Rows(i)("買掛残高_外貨")

            DgvCymndt.Rows(i).Cells("通貨").Value = setBaseCurrency()
            DgvCymndt.Rows(i).Cells("買掛金額計").Value = dsKikehd.Tables(RS).Rows(i)("買掛金額計")
            DgvCymndt.Rows(i).Cells("支払金額計").Value = dsKikehd.Tables(RS).Rows(i)("支払金額計")
            DgvCymndt.Rows(i).Cells("買掛金残高").Value = dsKikehd.Tables(RS).Rows(i)("買掛残高")
            DgvCymndt.Rows(i).Cells("備考").Value = dsKikehd.Tables(RS).Rows(i)("備考1")
        Next

        '数字形式
        DgvCymndt.Columns("買掛金額計_外貨").DefaultCellStyle.Format = "N0"
        DgvCymndt.Columns("支払金額計_外貨").DefaultCellStyle.Format = "N0"
        DgvCymndt.Columns("買掛金残高_外貨").DefaultCellStyle.Format = "N0"

        DgvCymndt.Columns("買掛金額計").DefaultCellStyle.Format = "N0"
        DgvCymndt.Columns("支払金額計").DefaultCellStyle.Format = "N0"
        DgvCymndt.Columns("買掛金残高").DefaultCellStyle.Format = "N0"


    End Sub

    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    Public Function getAPList()
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Sql = " AND "
        Sql += "買掛残高 > 0 "
        Sql += " AND "
        Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED '取消区分=0
        Sql += " ORDER BY 仕入先コード, 買掛日 "

        Try
            Dim dsKikehd As DataSet = getDsData("t46_kikehd", Sql)

            Return dsKikehd

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Function

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
            Dim sHinaFile As String = sHinaPath & "\" & "SupplierAPList.xlsx"
            '出力先パス
            Dim sOutPath As String = StartUp._iniVal.OutXlsPath
            '出力ファイル名
            Dim sOutFile As String = sOutPath & "\SupplierAPList_" & DateTime.Now.ToString("yyyyMMddHHmm") & ".xlsx"

            app = New Excel.Application()
            book = app.Workbooks.Add(sHinaFile)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet)

            sheet.PageSetup.RightHeader = "出力日：" & DateTime.Now.ToShortDateString

            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                sheet.PageSetup.LeftHeader = "Supplier AP list"
                sheet.PageSetup.RightHeader = "OutputDate：" & DateTime.Now.ToShortDateString

                sheet.Range("A1").Value = "SupplierName"
                sheet.Range("B1").Value = "PurchaseOrderNumber"
                sheet.Range("C1").Value = "AccountsPayableDate"
                sheet.Range("D1").Value = "TotalAccountsPayable"
                sheet.Range("E1").Value = "TotalPaymentAmount"
                sheet.Range("F1").Value = "APBalance"
                sheet.Range("G1").Value = "Remarks"

            End If

            For i As Integer = 0 To DgvCymndt.RowCount - 1
                Dim cellRowIndex As Integer = 2
                cellRowIndex += i

                sheet.Range("A" & cellRowIndex.ToString).Value = DgvCymndt.Rows(i).Cells("仕入先名").Value
                sheet.Range("B" & cellRowIndex.ToString).Value = DgvCymndt.Rows(i).Cells("発注番号").Value
                sheet.Range("C" & cellRowIndex.ToString).Value = DgvCymndt.Rows(i).Cells("買掛日").Value
                sheet.Range("D" & cellRowIndex.ToString).Value = DgvCymndt.Rows(i).Cells("買掛金額計").Value
                sheet.Range("E" & cellRowIndex.ToString).Value = DgvCymndt.Rows(i).Cells("支払金額計").Value
                sheet.Range("F" & cellRowIndex.ToString).Value = DgvCymndt.Rows(i).Cells("買掛金残高").Value
                sheet.Range("G" & cellRowIndex.ToString).Value = DgvCymndt.Rows(i).Cells("備考").Value

            Next

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

    '基準通貨の通貨コードを取得する
    Private Function setBaseCurrency() As String
        Dim Sql As String
        '通貨表示：ベースの設定
        Sql = " AND 採番キー = " & CommonConst.CURRENCY_CD_IDR.ToString
        Sql += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString

        Dim ds As DataSet = getDsData("m25_currency", Sql)
        'TxtIDRCurrency.Text = ds.Tables(RS).Rows(0)("通貨コード")
        setBaseCurrency = ds.Tables(RS).Rows(0)("通貨コード")

    End Function

End Class