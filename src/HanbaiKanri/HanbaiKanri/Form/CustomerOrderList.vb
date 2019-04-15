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
    Private CompanyCode As String = ""
    Private OrderingNo As String()
    Private CustomerCode As String = ""
    Private _parentForm As Form

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
                   ByRef prmRefCustomer As String)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        CompanyCode = prmRefCompany
        CustomerCode = prmRefCustomer
        _parentForm = prmRefForm
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
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
            DgvBilling.Columns.Add("請求番号", "InvoiceNumber")
            DgvBilling.Columns.Add("請求区分", "BillingClassification")
            DgvBilling.Columns.Add("請求日", "BillingDate")
            DgvBilling.Columns.Add("受注番号", "JobOrderNumber")
            DgvBilling.Columns.Add("受注番号枝番", "JobOrderSubNumber")
            DgvBilling.Columns.Add("得意先コード", "CustomerCode")
            DgvBilling.Columns.Add("得意先名", "CustomerName")
            DgvBilling.Columns.Add("請求金額計", "TotalBillingAmount")
            DgvBilling.Columns.Add("売掛残高", "AccountsReceivableBalance")
            DgvBilling.Columns.Add("備考1", "Remarks1")
            DgvBilling.Columns.Add("備考2", "Remarks2")
            DgvBilling.Columns.Add("登録日", "RegistrationDate")
            DgvBilling.Columns.Add("更新者", "ModifiedBy")
        Else
            DgvBilling.Columns.Add("請求番号", "請求番号")
            DgvBilling.Columns.Add("請求区分", "請求区分")
            DgvBilling.Columns.Add("請求日", "請求日")
            DgvBilling.Columns.Add("受注番号", "受注番号")
            DgvBilling.Columns.Add("受注番号枝番", "受注番号枝番")
            DgvBilling.Columns.Add("得意先コード", "得意先コード")
            DgvBilling.Columns.Add("得意先名", "得意先名")
            DgvBilling.Columns.Add("請求金額計", "請求金額計")
            DgvBilling.Columns.Add("売掛残高", "売掛残高")
            DgvBilling.Columns.Add("備考1", "備考1")
            DgvBilling.Columns.Add("備考2", "備考2")
            DgvBilling.Columns.Add("登録日", "登録日")
            DgvBilling.Columns.Add("更新者", "更新者")
        End If


        DgvBilling.Columns("請求金額計").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvBilling.Columns("売掛残高").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        '一覧取得
        PurchaseListLoad()
    End Sub

    '一覧表示処理
    Private Sub PurchaseListLoad()

        'データクリア
        DgvBilling.Rows.Clear()

        Dim Sql As String = ""
        Try
            Sql = " AND "
            Sql += "得意先コード ILIKE '%" & CustomerCode & "%'"
            Sql += " AND "
            Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
            Sql += " ORDER BY 更新日 DESC "

            '請求基本取得
            Dim dsSkyuhd As DataSet = getDsData("t23_skyuhd", Sql)

            Dim reccnt As Integer = 0

            'ds = _db.selectDB(Sql, RS, reccnt)

            For i As Integer = 0 To dsSkyuhd.Tables(RS).Rows.Count - 1
                DgvBilling.Rows.Add()
                DgvBilling.Rows(i).Cells("請求番号").Value = dsSkyuhd.Tables(RS).Rows(i)("請求番号")
                If frmC01F10_Login.loginValue.Language = "ENG" Then
                    DgvBilling.Rows(i).Cells("請求区分").Value = IIf(dsSkyuhd.Tables(RS).Rows(i)("請求区分") = CommonConst.BILLING_KBN_DEPOSIT,
                                                             CommonConst.BILLING_KBN_DEPOSIT_TXT_E,
                                                            CommonConst.BILLING_KBN_NORMAL_TXT_E)

                Else
                    DgvBilling.Rows(i).Cells("請求区分").Value = IIf(dsSkyuhd.Tables(RS).Rows(i)("請求区分") = CommonConst.BILLING_KBN_DEPOSIT,
                                                             CommonConst.BILLING_KBN_DEPOSIT_TXT,
                                                            CommonConst.BILLING_KBN_NORMAL_TXT)

                End If

                DgvBilling.Rows(i).Cells("請求日").Value = dsSkyuhd.Tables(RS).Rows(i)("請求日").ToShortDateString()
                DgvBilling.Rows(i).Cells("受注番号").Value = dsSkyuhd.Tables(RS).Rows(i)("受注番号")
                DgvBilling.Rows(i).Cells("受注番号枝番").Value = dsSkyuhd.Tables(RS).Rows(i)("受注番号枝番")
                DgvBilling.Rows(i).Cells("得意先コード").Value = dsSkyuhd.Tables(RS).Rows(i)("得意先コード")
                DgvBilling.Rows(i).Cells("得意先名").Value = dsSkyuhd.Tables(RS).Rows(i)("得意先名")
                DgvBilling.Rows(i).Cells("請求金額計").Value = dsSkyuhd.Tables(RS).Rows(i)("請求金額計")
                DgvBilling.Rows(i).Cells("売掛残高").Value = dsSkyuhd.Tables(RS).Rows(i)("売掛残高")
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
        Dim BillingSubTotal As Integer = 0
        Dim Sql As String = ""
        Dim Sql2 As String = ""
        Dim Sql3 As String = ""
        Dim reccnt As Integer = 0
        Dim flg1 As Boolean = False
        Dim flg2 As Boolean = False
        Dim flg3 As Boolean = False

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

            Dim currentRow As Integer = 18
            Dim lastRow As Integer = 20
            Dim addRow As Integer = 0
            Dim currentNum As Integer = 1

            '選択したものだけ請求書を発行する
            For Each r As DataGridViewRow In DgvBilling.SelectedRows
                BillingNo = DgvBilling.Rows(r.Index).Cells("請求番号").Value
                OrderNo = DgvBilling.Rows(r.Index).Cells("受注番号").Value
                OrderSuffix = DgvBilling.Rows(r.Index).Cells("受注番号枝番").Value
                BillingSubTotal += DgvBilling.Rows(r.Index).Cells("請求金額計").Value

                Sql = " AND "
                Sql += "受注番号 = '" & OrderNo & "'"
                Sql += " AND "
                Sql += "受注番号枝番 = '" & OrderSuffix & "'"
                Sql += " AND "
                Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

                '受注基本（請求債情報）
                Dim dsCymnhd As DataSet = getDsData("t10_cymnhd", Sql)

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
                    sheet.Range("A15").Value = "Telp." & dsCymnhd.Tables(RS).Rows(0)("得意先電話番号") & "　Fax." & dsCymnhd.Tables(RS).Rows(0)("得意先ＦＡＸ")
                    sheet.Range("E8").Value = BillingNo
                    'sheet.Range("E9").Value = System.DateTime.Now
                    sheet.Range("E9").Value = DgvBilling.Rows(r.Index).Cells("請求日").Value
                    sheet.Range("E11").Value = dsCymnhd.Tables(RS).Rows(0)("客先番号")

                    If dsShukkoHd.Tables(RS).Rows.Count > 0 Then
                        For x As Integer = 0 To dsShukkoHd.Tables(RS).Rows.Count - 1
                            sheet.Range("E12").Value += IIf(x > 0, ", " & dsShukkoHd.Tables(RS).Rows(x)("出庫番号"), dsShukkoHd.Tables(RS).Rows(x)("出庫番号"))
                        Next
                    End If

                    sheet.Range("D13").Value = dsCymnhd.Tables(RS).Rows(0)("支払条件")

                    flg2 = True

                Else
                    sheet.Range("E8").Value = sheet.Range("E8").Value & vbLf & BillingNo

                End If


                'joinするのでとりあえず直書き
                Sql = "SELECT"
                Sql += " t11.メーカー, t11.品名, t11.型式, t11.受注数量, t11.売単価, t11.売上金額"
                Sql += " FROM "
                Sql += " public.t11_cymndt t11 "

                Sql += " INNER JOIN "
                Sql += " t10_cymnhd t10"
                Sql += " ON "

                Sql += " t11.会社コード = t10.会社コード"
                Sql += " AND "
                Sql += " t11.受注番号 = t10.受注番号"
                Sql += " AND "
                Sql += " t11.受注番号枝番 = t10.受注番号枝番"

                Sql += " WHERE "
                Sql += " t11.会社コード ILIKE '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " AND "
                Sql += "t11.受注番号 ILIKE '%" & OrderNo & "%'"
                Sql += " AND "
                Sql += "t11.受注番号枝番 ILIKE '%" & OrderSuffix & "%'"
                Sql += " AND "
                Sql += "t10.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

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
                    sheet.Range("B" & currentRow).Value = dsCymndt.Tables(RS).Rows(i)("メーカー") & Environment.NewLine & dsCymndt.Tables(RS).Rows(i)("品名") & Environment.NewLine & dsCymndt.Tables(RS).Rows(i)("型式")
                    sheet.Range("C" & currentRow).Value = dsCymndt.Tables(RS).Rows(i)("受注数量")
                    sheet.Range("D" & currentRow).Value = dsCymndt.Tables(RS).Rows(i)("売単価")
                    sheet.Range("E" & currentRow).Value = dsCymndt.Tables(RS).Rows(i)("売上金額")
                    currentNum += 1
                    currentRow += 1
                Next i

            Next r

            sheet.Range("E" & lastRow + 1).Value = BillingSubTotal
            sheet.Range("E" & lastRow + 2).Value = BillingSubTotal * 0.1
            sheet.Range("E" & lastRow + 3).Value = BillingSubTotal * 1.1

            sheet.Range("C" & lastRow + 5).Value = sheet.Range("E" & lastRow + 3).Value

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

    'sqlで実行する文字列からシングルクォーテーションを文字コードにする
    Private Function escapeSql(ByVal prmSql As String) As String
        Dim sql As String = prmSql

        sql = sql.Replace("'"c, "''") 'シングルクォーテーションを置換

        Return Regex.Escape(sql)
        Return sql
    End Function

    'param1：String テーブル名
    'param2：String 詳細条件
    'Return: DataSet
    Private Function getDsData(ByVal tableName As String, Optional ByRef txtParam As String = "") As DataSet
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Sql += "SELECT"
        Sql += " *"
        Sql += " FROM "

        Sql += "public." & tableName
        Sql += " WHERE "
        Sql += "会社コード"
        Sql += " ILIKE  "
        Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"
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