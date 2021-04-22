Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.IO

Public Class CustomerOrderList
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
    'Private CompanyCode As String = ""
    Private OrderingNo As String()
    Private CustomerCode As String = ""
    Private CurCode As String = ""  '通貨
    Private _parentForm As Form
    Private _com As CommonLogic
    Private _vs As String = "2"

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
                   ByRef prmRefCompany As String,
                   ByRef prmRefCustomer As String,
                   ByRef prmCurCode As Integer)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        'CompanyCode = prmRefCompany
        CustomerCode = prmRefCustomer
        CurCode = prmCurCode
        _parentForm = prmRefForm
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        _com = New CommonLogic(_db, _msgHd)
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text += _vs
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    '初期表示
    Private Sub CustomerOrderList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            LblCustomerName.Text = "CustomerName"
            BtnSearch.Text = "Search"
            BtnInvoice.Text = "Invoicing"
            BtnBack.Text = "Back"
        End If

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

            DgvBilling.Columns.Add("得意先コード", "CustomerCode")
            DgvBilling.Columns.Add("得意先名", "CustomerName")

            DgvBilling.Columns.Add("受注番号", _langHd.getLANG("受注番号", frmC01F10_Login.loginValue.Language))
            DgvBilling.Columns.Add("受注番号枝番", _langHd.getLANG("受注番号枝番", frmC01F10_Login.loginValue.Language))

            DgvBilling.Columns.Add("請求番号", "SalesInvoiceNo")
            DgvBilling.Columns.Add("請求日", "SalesInvoiceDate")

            DgvBilling.Columns.Add("通貨_外貨", "Currency")
            DgvBilling.Columns.Add("請求金額計_外貨", "OrderAmount")
            DgvBilling.Columns.Add("VAT", "VAT-OUT")

            DgvBilling.Columns.Add("備考1", "Remarks1")
            DgvBilling.Columns.Add("備考2", "Remarks2")
            DgvBilling.Columns.Add("登録日", "RegistrationDate")
            DgvBilling.Columns.Add("更新者", "UpdatedBy")

        Else
            DgvBilling.Columns.Add("得意先コード", "得意先コード")
            DgvBilling.Columns.Add("得意先名", "得意先名")

            DgvBilling.Columns.Add("受注番号", _langHd.getLANG("受注番号", frmC01F10_Login.loginValue.Language))
            DgvBilling.Columns.Add("受注番号枝番", _langHd.getLANG("受注番号枝番", frmC01F10_Login.loginValue.Language))

            DgvBilling.Columns.Add("請求番号", "SalesInvoiceNo")
            DgvBilling.Columns.Add("請求日", "SalesInvoiceDate")

            DgvBilling.Columns.Add("通貨_外貨", "販売通貨")
            DgvBilling.Columns.Add("請求金額計_外貨", "受注金額")
            DgvBilling.Columns.Add("VAT", "VAT-OUT")

            DgvBilling.Columns.Add("備考1", "備考1")
            DgvBilling.Columns.Add("備考2", "備考2")
            DgvBilling.Columns.Add("登録日", "売掛請求登録日")
            DgvBilling.Columns.Add("更新者", "更新者")
        End If

        '右寄せ
        DgvBilling.Columns("請求金額計_外貨").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvBilling.Columns("VAT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        '数字形式
        DgvBilling.Columns("請求金額計_外貨").DefaultCellStyle.Format = "N2"
        DgvBilling.Columns("VAT").DefaultCellStyle.Format = "N2"


        '一覧取得
        PurchaseListLoad()
    End Sub

    '一覧表示処理
    Private Sub PurchaseListLoad()

        'Dim curds As DataSet  'm25_currency
        Dim cur As String


        'データクリア
        DgvBilling.Rows.Clear()

        Dim Sql As String = ""
        Try
            Sql = " AND "
            Sql += "得意先コード ILIKE '%" & CustomerCode & "%'"
            Sql += " AND "
            Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
            If CurCode <> 0 Then
                Sql += " AND 通貨 = " & CurCode
            End If
            Sql += " ORDER BY 更新日 DESC "

            '請求基本取得
            Dim dsSkyuhd As DataSet = _com.getDsData("t23_skyuhd", Sql)

            Dim reccnt As Integer = 0

            'ds = _db.selectDB(Sql, RS, reccnt)

            For i As Integer = 0 To dsSkyuhd.Tables(RS).Rows.Count - 1

                cur = _com.getCurrencyEx(dsSkyuhd.Tables(RS).Rows(0)("通貨"))

                DgvBilling.Rows.Add()

                'If frmC01F10_Login.loginValue.Language = "ENG" Then
                '    DgvBilling.Rows(i).Cells("請求区分").Value = IIf(dsSkyuhd.Tables(RS).Rows(i)("請求区分") = CommonConst.BILLING_KBN_DEPOSIT,
                '                                             CommonConst.BILLING_KBN_DEPOSIT_TXT_E,
                '                                            CommonConst.BILLING_KBN_NORMAL_TXT_E)

                'Else
                '    DgvBilling.Rows(i).Cells("請求区分").Value = IIf(dsSkyuhd.Tables(RS).Rows(i)("請求区分") = CommonConst.BILLING_KBN_DEPOSIT,
                '                                             CommonConst.BILLING_KBN_DEPOSIT_TXT,
                '                                            CommonConst.BILLING_KBN_NORMAL_TXT)

                'End If

                DgvBilling.Rows(i).Cells("得意先コード").Value = dsSkyuhd.Tables(RS).Rows(i)("得意先コード")
                DgvBilling.Rows(i).Cells("得意先名").Value = dsSkyuhd.Tables(RS).Rows(i)("得意先名")

                DgvBilling.Rows(i).Cells("受注番号").Value = dsSkyuhd.Tables(RS).Rows(i)("受注番号")
                DgvBilling.Rows(i).Cells("受注番号枝番").Value = dsSkyuhd.Tables(RS).Rows(i)("受注番号枝番")

                DgvBilling.Rows(i).Cells("請求番号").Value = dsSkyuhd.Tables(RS).Rows(i)("請求番号")
                DgvBilling.Rows(i).Cells("請求日").Value = dsSkyuhd.Tables(RS).Rows(i)("請求日").ToShortDateString()

                DgvBilling.Rows(i).Cells("通貨_外貨").Value = cur
                DgvBilling.Rows(i).Cells("請求金額計_外貨").Value = dsSkyuhd.Tables(RS).Rows(i)("請求金額計_外貨")
                DgvBilling.Rows(i).Cells("VAT").Value = dsSkyuhd.Tables(RS).Rows(i)("請求消費税計")

                DgvBilling.Rows(i).Cells("備考1").Value = dsSkyuhd.Tables(RS).Rows(i)("備考1")
                DgvBilling.Rows(i).Cells("備考2").Value = dsSkyuhd.Tables(RS).Rows(i)("備考2")
                DgvBilling.Rows(i).Cells("登録日").Value = dsSkyuhd.Tables(RS).Rows(i)("登録日")
                DgvBilling.Rows(i).Cells("更新者").Value = dsSkyuhd.Tables(RS).Rows(i)("更新者")
            Next

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
        _parentForm.Show()
        _parentForm.Enabled = True
        Me.Dispose()
    End Sub

    '請求書発行ボタン押下時
    Private Sub BtnInvoice_Click(sender As Object, e As EventArgs) Handles BtnInvoice.Click
        Dim BillingNo As String = ""
        Dim OrderNo As String = ""
        Dim OrderSuffix As String = ""
        Dim BillingSubTotal As Decimal = 0
        Dim Sql As String = ""
        Dim Sql2 As String = ""
        Dim Sql3 As String = ""
        Dim reccnt As Integer = 0
        Dim flg1 As Boolean = False
        Dim flg2 As Boolean = False
        Dim flg3 As Boolean = False
        Dim blnFlg As Boolean = False

        If DgvBilling.Rows.Count() = 0 Then
            '対象データがないメッセージを表示
            _msgHd.dspMSG("NonData", frmC01F10_Login.loginValue.Language)

            Return
        End If

        '定義
        Dim app As Excel.Application = Nothing
        Dim book As Excel.Workbook = Nothing
        Dim sheet As Excel.Worksheet = Nothing

        'カーソルをビジー状態にする
        Cursor.Current = Cursors.WaitCursor

        For Each r As DataGridViewRow In DgvBilling.SelectedRows
            If flg1 = False Then
                BillingNo = DgvBilling.Rows(r.Index).Cells("請求番号").Value
                If DgvBilling.Rows(r.Index).Cells("受注番号").Value = CommonConst.COLINV Then
                    Call Print_Colinv(BillingNo, CType(DgvBilling.Rows(r.Index).Cells("請求日").Value, Date))
                    Exit Sub
                End If
                flg1 = True
            End If
        Next r

        Try
            '雛形パス
            Dim sHinaPath As String = ""
            sHinaPath = StartUp._iniVal.BaseXlsPath

            '雛形ファイル名
            Dim sHinaFile As String = ""
            sHinaFile = sHinaPath & "\" & "Invoice.xlsx"

            '出力先パス
            Dim sOutPath As String = ""
            sOutPath = StartUp._iniVal.OutXlsPath

            '出力ファイル名
            Dim sOutFile As String = ""
            sOutFile = sOutPath & "\" & BillingNo & ".xlsx"



            app = New Excel.Application()
            book = app.Workbooks.Add(sHinaFile)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet)


            BillingNo = ""

            Dim currentRow As Integer = 19
            Dim lastRow As Integer = 20
            Dim addRow As Integer = 0
            Dim currentNum As Integer = 1
			Dim VAT As Decimal = 0

            '選択したものだけ請求書を発行する
            For Each r As DataGridViewRow In DgvBilling.SelectedRows
                BillingNo = DgvBilling.Rows(r.Index).Cells("請求番号").Value
                OrderNo = DgvBilling.Rows(r.Index).Cells("受注番号").Value
                OrderSuffix = DgvBilling.Rows(r.Index).Cells("受注番号枝番").Value

                Sql = " AND 受注番号 = '" & OrderNo & "'"
                Sql += " AND 受注番号枝番 = '" & OrderSuffix & "'"

                '受注基本（請求債情報）
                Dim dsCymnhd As DataSet = _com.getDsData("t10_cymnhd", Sql)
                Dim dsSeikyu1 As DataSet = Nothing

                '入金予定日の取得
                If flg2 = False Then
                    Sql = "Select 入金予定日,請求消費税計 From t23_skyuhd t23 "
                    Sql += " where t23.会社コード =  '" & frmC01F10_Login.loginValue.BumonCD & "'"
                    Sql += " and t23.請求番号 =  '" & BillingNo & "'"
                    Sql += " AND t23.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED & ""
                    '請求データ取得
                    dsSeikyu1 = _db.selectDB(Sql, RS, reccnt)
                    If reccnt > 0 Then
                        sheet.Range("E10").Value = dsSeikyu1.Tables(RS).Rows(0)("入金予定日")
                        VAT = UtilClass.ToRoundDown(UtilClass.rmNullDecimal(dsSeikyu1.Tables(RS).Rows(0)("請求消費税計")), 0)
                    Else
                        sheet.Range("E10").Value = ""
                    End If
                End If
                '出庫情報
                Sql = " SELECT t44.出庫番号 "
                Sql += " FROM t44_shukohd t44 "
                Sql += " LEFT JOIN t10_cymnhd t10 "
                Sql += " ON t44.会社コード = t10.会社コード "
                Sql += " AND t44.受注番号 = t10.受注番号 "
                Sql += " AND t44.受注番号枝番 = t10.受注番号枝番 "
                Sql += " WHERE t44.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " AND t44.受注番号 = '" & OrderNo & "'"
                Sql += " AND t44.受注番号枝番 = '" & OrderSuffix & "'"
                Sql += " AND t44.取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'"

                '出庫データ取得
                Dim dsShukkoHd As DataSet = _db.selectDB(Sql, RS, reccnt)

                '初回だけ
                If flg2 = False Then
                    sheet.Range("B8").Value = dsCymnhd.Tables(RS).Rows(0)("得意先名")
                    sheet.Range("B13").Value = dsCymnhd.Tables(RS).Rows(0)("得意先郵便番号")
                    sheet.Range("B9").Value = dsCymnhd.Tables(RS).Rows(0)("得意先住所")
                    sheet.Range("B14").Value = dsCymnhd.Tables(RS).Rows(0)("得意先担当者役職") & " " & dsCymnhd.Tables(RS).Rows(0)("得意先担当者名")
                    'sheet.Range("A15").Value = "Telp." & dsCymnhd.Tables(RS).Rows(0)("得意先電話番号") & "　Fax." & dsCymnhd.Tables(RS).Rows(0)("得意先ＦＡＸ")
                    sheet.Range("A15").Value = "Telp." & dsCymnhd.Tables(RS).Rows(0)("得意先電話番号")
                    sheet.Range("E8").Value = BillingNo
                    sheet.Range("E9").Value = UtilClass.strFormatDate(DgvBilling.Rows(r.Index).Cells("請求日").Value)
                    sheet.Range("E11").Value = dsCymnhd.Tables(RS).Rows(0)("客先番号")

                    If dsShukkoHd.Tables(RS).Rows.Count > 0 Then
                        For x As Integer = 0 To dsShukkoHd.Tables(RS).Rows.Count - 1
                            sheet.Range("E12").Value += IIf(x > 0, ", " & dsShukkoHd.Tables(RS).Rows(x)("出庫番号"), dsShukkoHd.Tables(RS).Rows(x)("出庫番号"))
                        Next
                    End If

                    sheet.Range("E13").Value = dsCymnhd.Tables(RS).Rows(0)("支払条件")
                    'sheet.Range("D14").Value = ": " & dsCymnhd.Tables(RS).Rows(0)("支払条件")

                    Dim cur As String = _com.getCurrencyEx(dsCymnhd.Tables(RS).Rows(0)("通貨"))

                    sheet.Range("E18").Value = "(" & cur & ")"
                    sheet.Range("F18").Value = "(" & cur & ")"

                    flg2 = True

                Else
                    sheet.Range("E8").Value = sheet.Range("E8").Value & vbLf & BillingNo

                End If


                'kafu
                Sql = "SELECT"
                Sql += " t11.*"
                Sql += " FROM public.v_t31_urigdt_1 t11 "
                'Sql += " ,t30_urighd t10"
                'Sql += " WHERE t11.会社コード = t10.会社コード"
                'Sql += " AND t11.売上番号 = t10.売上番号"
                'Sql += " AND t11.売上番号枝番 = t10.売上番号枝番"
                Sql += " WHERE t11.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " AND t11.入金番号 = '" & BillingNo & "'"
                'Sql += " AND t11.受注番号枝番 = '" & OrderSuffix & "'"
                'Sql += " AND t10.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
                Sql += " order by t11.行番号"

                '受注明細（商品情報）
                Dim dsCymndt As DataSet = _db.selectDB(Sql, RS, reccnt)

                For i As Integer = 0 To dsCymndt.Tables(RS).Rows.Count - 1
                    If currentNum > 3 Then
                        Dim obj As Object
                        Dim cellPos = lastRow & ":" & lastRow
                        obj = sheet.Range(cellPos)
                        obj.Copy()
                        obj.Insert()
                        If Marshal.IsComObject(obj) Then
                            Marshal.ReleaseComObject(obj)
                        End If
                        lastRow += 1
                    End If
                    sheet.Range("A" & currentRow).Value = currentNum
                    sheet.Range("B" & currentRow).Value = dsCymndt.Tables(RS).Rows(i)("メーカー") & " " & dsCymndt.Tables(RS).Rows(i)("品名") & " " & dsCymndt.Tables(RS).Rows(i)("型式")  '& Environment.NewLine & dsCymndt.Tables(RS).Rows(i)("客先番号")
                    Dim x As Decimal = dsCymndt.Tables(RS).Rows(i)("見積単価_外貨")
                    Dim y As Decimal = dsCymndt.Tables(RS).Rows(i)("売上数量")

                    sheet.Range("C" & currentRow).Value = y
                    sheet.Range("D" & currentRow).Value = dsCymndt.Tables(RS).Rows(i)("単位")

                    sheet.Range("E" & currentRow).Value = x 'dsCymndt.Tables(RS).Rows(i)("見積単価_外貨")
                    sheet.Range("F" & currentRow).Value = x * y 'dsCymndt.Tables(RS).Rows(i)("見積金額") '("売上金額_外貨")
                    BillingSubTotal += (x * y) 'dsCymndt.Tables(RS).Rows(i)("見積金額")
                    currentNum += 1
                    currentRow += 1
                Next i

            Next r

            'Sql = " AND 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            'Dim dsCompany As DataSet = _com.getDsData("m01_company", Sql)
            'Dim getHanyo As DataSet = _com.getDsHanyoData(CommonConst.DC_CODE, dsCompany.Tables(RS).Rows(0)("預金種目"))
            lastRow += 1
            sheet.Range("F" & lastRow + 1).Value = BillingSubTotal
            sheet.Range("F" & lastRow + 2).Value = VAT 'BillingSubTotal * 0.1
            sheet.Range("F" & lastRow + 3).Value = BillingSubTotal + VAT ' * 1.1

            'sheet.Range("C" & lastRow + 5).Value = sheet.Range("F" & lastRow + 3).Value
            'sheet.Range("C" & lastRow + 8).Value = dsCompany.Tables(RS).Rows(0)("銀行名") & " " & dsCompany.Tables(RS).Rows(0)("支店名") & " " & getHanyo.Tables(RS).Rows(0)("文字2")
            'sheet.Range("C" & lastRow + 9).Value = dsCompany.Tables(RS).Rows(0)("口座名義")
            'sheet.Range("C" & lastRow + 10).Value = dsCompany.Tables(RS).Rows(0)("口座番号")
            'sheet.Range("C" & lastRow + 11).Value = dsCompany.Tables(RS).Rows(0)("住所1") & " " & dsCompany.Tables(RS).Rows(0)("住所2") & " " & dsCompany.Tables(RS).Rows(0)("住所3") & " " & dsCompany.Tables(RS).Rows(0)("郵便番号")

            'sheet.Range("A" & lastRow + 18).Value = dsCompany.Tables(RS).Rows(0)("会社名")

            app.DisplayAlerts = False 'Microsoft Excelのアラート一旦無効化

            Dim excelChk As Boolean = _com.excelOutput(sOutFile)
            If excelChk = False Then
                Exit Sub
            End If
            book.SaveAs(sOutFile) '書き込み実行

            app.DisplayAlerts = True 'アラート無効化を解除
            app.Visible = True

            'カーソルをビジー状態から元に戻す
            Cursor.Current = Cursors.Default

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

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        '一覧再表示
        PurchaseListLoad()
    End Sub

    Private Sub Print_Colinv(ByVal BillingNo As String, ByVal dtmInvoice As Date)
        Dim BillingSubTotal As Decimal = 0
        Dim Sql As String = ""
        Dim Sql2 As String = ""
        Dim Sql3 As String = ""
        Dim reccnt As Integer = 0
        Dim flg2 As Boolean = False
        Dim flg3 As Boolean = False
        Dim blnFlg As Boolean = False

        '定義
        Dim app As Excel.Application = Nothing
        Dim book As Excel.Workbook = Nothing
        Dim sheet As Excel.Worksheet = Nothing

        'カーソルをビジー状態にする
        Cursor.Current = Cursors.WaitCursor

        Try
            '雛形パス
            Dim sHinaPath As String = ""
            sHinaPath = StartUp._iniVal.BaseXlsPath

            '雛形ファイル名
            Dim sHinaFile As String = ""
            sHinaFile = sHinaPath & "\" & "CollectiveInvoice.xlsx"

            '出力先パス
            Dim sOutPath As String = ""
            sOutPath = StartUp._iniVal.OutXlsPath

            '出力ファイル名
            Dim sOutFile As String = ""
            sOutFile = sOutPath & "\" & BillingNo & ".xlsx"



            app = New Excel.Application()
            book = app.Workbooks.Add(sHinaFile)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet)

            Dim currentRow As Integer = 19
            Dim lastRow As Integer = 21
            Dim addRow As Integer = 0
            Dim currentNum As Integer = 1
            Dim VAT As Decimal = 0

            Dim cur As String = ""
            Dim sCustName As String = ""
            Dim sCustAdr As String = ""
            Dim sCustPIC As String = ""
            Dim sCustTel As String = ""

            '受注基本（請求債情報）
            'Dim dsCymnhd As DataSet = getDsData("t10_cymnhd", Sql)
            Dim dsSeikyu1 As DataSet = Nothing

            '入金予定日の取得
            If flg2 = False Then
                Sql = "Select 入金予定日,請求消費税計,得意先コード,通貨 From t23_skyuhd t23 "
                Sql += " where t23.会社コード =  '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " and t23.請求番号 =  '" & BillingNo & "'"
                Sql += " AND t23.取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'"
                '請求データ取得
                dsSeikyu1 = _db.selectDB(Sql, RS, reccnt)
                If reccnt > 0 Then
                    sheet.Range("F10").Value = dsSeikyu1.Tables(RS).Rows(0)("入金予定日")
                    VAT = UtilClass.ToRoundDown(UtilClass.rmNullDecimal(dsSeikyu1.Tables(RS).Rows(0)("請求消費税計")), 0)
                    cur = _com.getCurrencyEx(dsSeikyu1.Tables(RS).Rows(0)("通貨"))

                    'get customer info
                    Sql = " AND 得意先コード='" & dsSeikyu1.Tables(RS).Rows(0)("得意先コード") & "' "
                    Dim dsCust As DataSet = _com.getDsData("m10_customer", Sql)
                    sCustName = dsCust.Tables(RS).Rows(0)("得意先名")
                    sCustAdr = dsCust.Tables(RS).Rows(0)("住所１") & " " & dsCust.Tables(RS).Rows(0)("住所２") & " " & dsCust.Tables(RS).Rows(0)("住所３") & " " & dsCust.Tables(RS).Rows(0)("郵便番号")
                    sCustPIC = dsCust.Tables(RS).Rows(0)("担当者役職") & " " & dsCust.Tables(RS).Rows(0)("担当者名")
                    sCustTel = "Telp." & dsCust.Tables(RS).Rows(0)("電話番号")
                Else
                    sheet.Range("F10").Value = ""
                End If
            End If

            Sql = "SELECT"
            Sql += " t11.*"
            Sql += " FROM public.v_t31_urigdt_1 t11 "
            Sql += " WHERE t11.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND t11.入金番号 = '" & BillingNo & "'"
            Sql += " order by t11.受注番号,t11.受注番号枝番,t11.行番号"

            '受注明細（商品情報）
            Dim dsCymndt As DataSet = _db.selectDB(Sql, RS, reccnt)

            '初回だけ
            If flg2 = False Then
                sheet.Range("B8").Value = sCustName 'dsCymndt.Tables(RS).Rows(0)("得意先名")
                'sheet.Range("B13").Value = dsCymnhd.Tables(RS).Rows(0)("得意先郵便番号")
                sheet.Range("B9").Value = sCustAdr 'dsCymndt.Tables(RS).Rows(0)("得意先住所") & " " & dsCymndt.Tables(RS).Rows(0)("得意先郵便番号")
                sheet.Range("B14").Value = sCustPIC 'dsCymndt.Tables(RS).Rows(0)("得意先担当者役職") & " " & dsCymndt.Tables(RS).Rows(0)("得意先担当者名")
                'sheet.Range("A15").Value = "Telp." & dsCymnhd.Tables(RS).Rows(0)("得意先電話番号") & "　Fax." & dsCymnhd.Tables(RS).Rows(0)("得意先ＦＡＸ")
                sheet.Range("A15").Value = sCustTel '"Telp." & dsCymndt.Tables(RS).Rows(0)("得意先電話番号")
                sheet.Range("F8").Value = "" & BillingNo  'InvoiceNo
                sheet.Range("F9").Value = "" & UtilClass.strFormatDate(dtmInvoice)
                'sheet.Range("E10").Value = "" & 
                'sheet.Range("E12").Value = ": " & dsCymnhd.Tables(RS).Rows(0)("受注番号")
                sheet.Range("F11").Value = "" '& dsCymndt.Tables(RS).Rows(0)("客先番号")

                'If dsShukkoHd.Tables(RS).Rows.Count > 0 Then
                'For x As Integer = 0 To dsShukkoHd.Tables(RS).Rows.Count - 1
                'sheet.Range("E12").Value += IIf(x > 0, ", " & dsShukkoHd.Tables(RS).Rows(x)("出庫番号"), dsShukkoHd.Tables(RS).Rows(x)("出庫番号"))
                'Next
                'End If

                sheet.Range("F13").Value = dsCymndt.Tables(RS).Rows(0)("支払条件")

                sheet.Range("F18").Value = "(" & cur & ")"
                sheet.Range("G18").Value = "(" & cur & ")"

                flg2 = True

            Else
                'sheet.Range("E8").Value = sheet.Range("E8").Value & vbLf & BillingNo
            End If

            'Dim order_no_ As String = ""

            For i As Integer = 0 To dsCymndt.Tables(RS).Rows.Count - 1

                If currentNum > 3 Then
                    Dim obj As Object
                    Dim cellPos = lastRow & ":" & lastRow
                    obj = sheet.Range(cellPos)
                    obj.Copy()
                    obj.Insert()
                    If Marshal.IsComObject(obj) Then
                        Marshal.ReleaseComObject(obj)
                    End If
                    lastRow += 1
                End If

                sheet.Range("A" & currentRow).Value = currentNum
                sheet.Range("B" & currentRow).Value = dsCymndt.Tables(RS).Rows(i)("メーカー") & " " & dsCymndt.Tables(RS).Rows(i)("品名") & " " & dsCymndt.Tables(RS).Rows(i)("型式")  '& Environment.NewLine & dsCymndt.Tables(RS).Rows(i)("客先番号")
                Dim x As Decimal = dsCymndt.Tables(RS).Rows(i)("見積単価_外貨")
                Dim y As Decimal = dsCymndt.Tables(RS).Rows(i)("売上数量")
                sheet.Range("C" & currentRow).Value = dsCymndt.Tables(RS).Rows(i)("客先番号")
                sheet.Range("D" & currentRow).Value = y
                sheet.Range("E" & currentRow).Value = dsCymndt.Tables(RS).Rows(i)("単位")
                sheet.Range("F" & currentRow).Value = x 'dsCymndt.Tables(RS).Rows(i)("見積単価_外貨")
                sheet.Range("G" & currentRow).Value = x * y 'dsCymndt.Tables(RS).Rows(i)("見積金額") '("売上金額_外貨")
                BillingSubTotal += (x * y) 'dsCymndt.Tables(RS).Rows(i)("見積金額")
                currentNum += 1
                currentRow += 1

            Next i

            'sheet.Range("A" & lastRow & ":G" & lastRow).Borders(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous

            sheet.Range("G" & lastRow + 1).Value = BillingSubTotal

            'VATなしの場合が考慮されていなかったため修正　2020.03.01
            'sheet.Range("F" & lastRow + 2).Value = BillingSubTotal * 0.1
            'sheet.Range("F" & lastRow + 3).Value = BillingSubTotal * 1.1

            If VAT = 0 Then  'VATなし
                sheet.Range("G" & lastRow + 2).Value = 0
                sheet.Range("G" & lastRow + 3).Value = BillingSubTotal
            Else
                'Dim decVAT As Decimal = BillingSubTotal * VAT / 100
                sheet.Range("G" & lastRow + 2).Value = VAT 'decVAT
                sheet.Range("G" & lastRow + 3).Value = BillingSubTotal + VAT 'decVAT
            End If

            'sheet.Range("C" & lastRow + 5).Value = sheet.Range("G" & lastRow + 3).Value
            'sheet.Range("F" & lastRow + 9).Value = "Jakarta, " & dtmInvoice.Day & " " & dtmInvoice.ToString("MMMM") & " " & dtmInvoice.Year  'InvoiceDate

            app.DisplayAlerts = False 'Microsoft Excelのアラート一旦無効化

            Dim excelChk As Boolean = _com.excelOutput(sOutFile)
            If excelChk = False Then
                Exit Sub
            End If
            book.SaveAs(sOutFile) '書き込み実行

            app.DisplayAlerts = True 'アラート無効化を解除
            app.Visible = True

            'カーソルをビジー状態から元に戻す
            Cursor.Current = Cursors.Default

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

End Class