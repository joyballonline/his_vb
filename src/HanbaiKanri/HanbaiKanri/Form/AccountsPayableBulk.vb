Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Imports System.IO
Imports System.Runtime.InteropServices

Public Class AccountsPayableBulk
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
    Private OrderingNo As String()
    Private _status As String = ""

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
        _status = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    '画面表示時
    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If _status = CommonConst.STATUS_VIEW Then

        '    LblMode.Text = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
        '                    "ViewMode",
        '                    "参照モード")

        'ElseIf _status = CommonConst.STATUS_CANCEL Then

        '    LblMode.Text = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
        '                    "CancelMode",
        '                    "取消モード")

        '    BtnAPCancel.Visible = True
        '    BtnAPCancel.Location = New Point(997, 509)
        'End If

        ''検索（Date）の初期値
        'dtAPDateSince.Value = DateAdd("d", CommonConst.SINCE_DEFAULT_DAY, DateTime.Today)
        'dtAPDateUntil.Value = DateTime.Today

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then

        Else  '日本語

        End If


        'PurchaseListLoad() '一覧表示


    End Sub

    '一覧取得
    Private Function kikeList()

        '変数等
        Dim Sql As String = ""
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim hachuList As DataSet
        Dim dt As New System.Data.DataTable("Table") '登録データの発注番号・枝番を格納

        '発注データ取得
        '有効、対象会社データ、枝番が最新(MAX)
        Sql = "SELECT "
        Sql += "t20.発注番号"
        Sql += ",max(t20.発注番号枝番) as 枝番"
        Sql += ",sum(t21.発注数量) as 発注数量"
        Sql += ",sum(t21.仕入数量) as 仕入数量"
        Sql += " FROM t20_hattyu t20"
        Sql += " LEFT JOIN t21_hattyu t21"
        Sql += " ON t20.発注番号 = t21.発注番号"
        Sql += " AND t20.発注番号枝番 = t21.発注番号枝番"
        Sql += " LEFT JOIN t46_kikehd t46"
        Sql += " ON t20.発注番号 = t46.発注番号"
        Sql += " AND t20.発注番号枝番 = t46.発注番号枝番"
        Sql += " WHERE "
        Sql += " t20.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND "
        Sql += " t20.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString
        'Sql += " AND "
        'Sql += " ((t46.買掛番号 IS NULL) OR (t46.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString & ")) "
        Sql += " GROUP BY t20.発注番号"
        Sql += " ORDER BY t20.発注番号,枝番"

        Try
            hachuList = _db.selectDB(Sql, RS, reccnt)

            Dim row As DataRow
            dt.Columns.Add("発注番号")
            dt.Columns.Add("発注番号枝番")

            For i As Integer = 0 To hachuList.Tables(RS).Rows.Count - 1
                If hachuList.Tables(RS).Rows(i)("発注数量").ToString = hachuList.Tables(RS).Rows(i)("仕入数量").ToString Then

                    Sql = " AND "
                    Sql += "発注番号 ILIKE '" & hachuList.Tables(RS).Rows(i)("発注番号").ToString & "'"
                    Sql += " AND "
                    Sql += "発注番号枝番 ILIKE '" & hachuList.Tables(RS).Rows(i)("枝番").ToString & "'"
                    Sql += " AND "
                    Sql += "取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
                    Sql += " ORDER BY 更新日 DESC "

                    Dim dsKikehd As DataSet = getDsData("t46_kikehd", Sql)

                    If dsKikehd.Tables(RS).Rows.Count = 0 Then
                        row = dt.NewRow '行作成
                        row("発注番号") = hachuList.Tables(RS).Rows(i)("発注番号").ToString
                        row("発注番号枝番") = hachuList.Tables(RS).Rows(i)("枝番").ToString
                        dt.Rows.Add(row) '行追加

                    End If

                End If
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

        Return dt

    End Function

    'Excel出力ボタン押下時
    Private Sub BtnExcelOutput_Click(sender As Object, e As EventArgs) Handles BtnExcelOutput.Click

        Dim ds As System.Data.DataTable = kikeList() '発注データを基に買掛対象のデータを取得

        '対象データがない場合は取消操作不可能
        If ds.Rows.Count = 0 Then

            '該当データがないアラートを出す
            _msgHd.dspMSG("noTargetData", frmC01F10_Login.loginValue.Language)

            Return

        Else

            'Excel出力処理
            outputExcel(ds)
        End If
    End Sub

    'excel出力処理
    Private Sub outputExcel(ByRef prmkikeData As System.Data.DataTable)

        '定義
        Dim app As Excel.Application = Nothing
        Dim books As Excel.Workbooks = Nothing
        Dim book As Excel.Workbook = Nothing
        Dim sheet As Excel.Worksheet = Nothing

        'Dim dtToday As DateTime = DateTime.Today

        ' セル
        Dim xlRngTmp As Range = Nothing
        Dim xlRng As Range = Nothing

        ' セル境界線（枠）
        Dim xlBorders As Borders = Nothing
        Dim xlBorder As Border = Nothing

        Dim filePath As String = ""

        'カーソルをビジー状態にする
        Cursor.Current = Cursors.WaitCursor

        Try
            'インスタンス作成
            Dim sfd As New SaveFileDialog()

            sfd.Filter = "Microsoft Excel ブック(*.xlsx)|*.xlsx" '保存ファイルの形式を指定
            sfd.FileName = "AccountsPayableBulkList_" & DateTime.Now.ToString("yyyyMMddHHmm") & ".xlsx"

            'ダイアログを表示する
            If sfd.ShowDialog() = DialogResult.OK Then

                'OKボタンがクリックされたとき、選択されたファイル名を表示する
                filePath = sfd.FileName

                '雛形パス
                Dim sHinaPath As String = StartUp._iniVal.BaseXlsPath
                '雛形ファイル名
                Dim sHinaFile As String = sHinaPath & "\" & "AccountsPayableBulkList.xlsx"

                app = New Excel.Application()
                books = app.Workbooks
                book = books.Add(sHinaFile)  'テンプレート
                sheet = CType(book.Worksheets(1), Excel.Worksheet)

                '見出し 英語表示対応
                If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                    sheet.PageSetup.LeftHeader = "Inventory control table"
                    sheet.PageSetup.RightHeader = "OutputDate：" & DateTime.Now.ToShortDateString

                    sheet.Range("A1").Value = "PurchaseOrderNumber" '発注番号
                    sheet.Range("B1").Value = "PurchaseOrderSubNumber" '発注番号枝番
                    sheet.Range("C1").Value = "BillingDate" '請求日
                    sheet.Range("D1").Value = "SupplierInvoiceNumber" '仕入先請求番号
                    sheet.Range("E1").Value = "PaymentDueDate" '支払予定日
                    sheet.Range("F1").Value = "Remark1" '備考1
                    sheet.Range("G1").Value = "Remark2" '備考2
                End If

                Dim cellRowIndex As Integer = 1

                For Each setRow As DataRow In prmkikeData.Rows

                    cellRowIndex += 1

                    sheet.Range("A" & cellRowIndex.ToString).Value = setRow("発注番号").ToString '発注番号
                    sheet.Range("B" & cellRowIndex.ToString).Value = setRow("発注番号枝番").ToString '発注番号枝番

                Next

                app.DisplayAlerts = False 'Microsoft Excelのアラート一旦無効化

                Dim excelChk As Boolean = excelOutput(filePath)
                If excelChk = False Then
                    Exit Sub
                End If
                book.SaveAs(filePath) '書き込み実行

                app.DisplayAlerts = True 'アラート無効化を解除
                app.Visible = True

                'カーソルを砂時計から元に戻す
                Cursor.Current = Cursors.Default

                _msgHd.dspMSG("CreateExcel", frmC01F10_Login.loginValue.Language)
            End If

        Catch ex As Exception
            'カーソルをビジー状態から元に戻す
            Cursor.Current = Cursors.Default

            Throw ex
        Finally
            'app.Quit()

            'リソースの解放
            Marshal.ReleaseComObject(sheet)
            Marshal.ReleaseComObject(book)
            Marshal.ReleaseComObject(books)
            Marshal.ReleaseComObject(app)
        End Try


    End Sub

    '一括登録ボタン押下時
    Private Sub BtnRegist_Click(sender As Object, e As EventArgs) Handles BtnRegist.Click
        Dim filePath As String = ""

        'ファイルを開くダイアログボックス表示
        Dim ofd As New OpenFileDialog()
        ofd.Filter = "Microsoft Excel ブック(*.xlsx)|*.xlsx" '保存ファイルの形式を指定

        'ダイアログを表示する
        If ofd.ShowDialog() = DialogResult.OK Then

            'OKボタンがクリックされたとき、選択されたファイル名を取得
            filePath = ofd.FileName

            'カーソルをビジー状態にする
            Cursor.Current = Cursors.WaitCursor

            Dim errorMsg As String = "" 'エラーメッセージ表示

            Dim addDt As New System.Data.DataTable("Table") '登録データの発注番号・枝番を格納
            Dim addRow As DataRow
            addDt.Columns.Add("発注番号")
            addDt.Columns.Add("発注番号枝番")
            addDt.Columns.Add("請求日")
            addDt.Columns.Add("仕入先請求番号")
            addDt.Columns.Add("支払予定日")
            addDt.Columns.Add("備考1")
            addDt.Columns.Add("備考2")

            Try
                Dim getExcelData As System.Data.DataTable = ImportExcel(filePath) 'ファイルパスからデータを抽出
                If getExcelData.Rows.Count > 0 Then
                    '最新の対象データを再取得
                    Dim ds As System.Data.DataTable = kikeList() '発注データを基に買掛対象のデータを取得

                    '対象データがない場合は取消操作不可能
                    If ds.Rows.Count <> getExcelData.Rows.Count Then
                        '該当データが更新されているアラートを出す
                        _msgHd.dspMSG("chkData", frmC01F10_Login.loginValue.Language)
                        Exit Sub '処理終了
                    End If

                    Dim errorCnt As Integer = 0 'エラー数
                    Dim saveCnt As Integer = 0 '登録可能件数
                    Dim skipCnt As Integer = 0 'スキップ数

                    '対象データと比較する
                    For Each rexcelRow As DataRow In getExcelData.Rows

                        Dim dataCheckFlg As Boolean = False '伝票番号有無チェック

                        For Each row As DataRow In ds.Rows

                            'Excelのデータと今現在のDB内容を比較
                            If rexcelRow("発注番号").ToString() = row("発注番号").ToString And
                                        rexcelRow("発注番号枝番").ToString() = row("発注番号枝番").ToString Then

                                dataCheckFlg = True

                            Else

                                '違っていたらアラートに表示する内容を作成
                                '一旦コンソールに出力
                                Console.WriteLine(row("発注番号").ToString)

                            End If

                        Next

                        '1件も引っかからなかったら終了とする（全件一致した場合のみデータ登録を行う）
                        If dataCheckFlg = False Then
                            '該当データが更新されているアラートを出す
                            _msgHd.dspMSG("chkData", frmC01F10_Login.loginValue.Language)
                            Exit Sub '処理終了
                        End If

                    Next

                    'データチェック
                    For Each row As DataRow In getExcelData.Rows

                        '登録対象データかどうか確認する ======================
                        '空行チェック
                        If row("請求日").ToString = "" And row("仕入先請求番号").ToString = "" And row("支払予定日").ToString = "" Then
                            errorCnt += 1 'エラーカウント

                            'エラーの発注書番号を記録
                            errorMsg = row("発注番号").ToString & vbCrLf
                            'Console.WriteLine(row("発注番号").ToString)
                            Continue For '次の行へ
                        End If

                        'データの整合性を確認する ======================
                        'データ有無チェック
                        If row("請求日").ToString = "" Or row("仕入先請求番号").ToString = "" Or row("支払予定日").ToString = "" Then
                            skipCnt += 1 'スキップカウント

                            '一旦コンソールに出力
                            Console.WriteLine(row("発注番号").ToString)
                            Continue For '次の行へ
                        End If
                        'データ型チェック
                        If IsDate(row("請求日").ToString) = False Or IsDate(row("支払予定日").ToString) = False Then
                            errorCnt += 1 'エラーカウント

                            'エラーの発注書番号を記録
                            errorMsg = row("発注番号").ToString & vbCrLf
                            'Console.WriteLine(row("発注番号").ToString)
                            Continue For '次の行へ
                        End If

                        '仕入先請求番号,備考1,2のサイズが既定内であるかチェック
                        If row("仕入先請求番号").ToString.Length > 100 Or row("備考1").ToString.Length > 50 Or row("備考2").ToString.Length > 50 Then
                            errorCnt += 1 'エラーカウント

                            'エラーの発注書番号を記録
                            errorMsg = row("発注番号").ToString & vbCrLf
                            'Console.WriteLine(row("発注番号").ToString)
                            Continue For '次の行へ
                        End If

                        saveCnt += 1
                        addRow = addDt.NewRow '行作成
                        addRow("発注番号") = row("発注番号").ToString
                        addRow("発注番号枝番") = row("発注番号枝番").ToString
                        addRow("請求日") = row("請求日").ToString
                        addRow("仕入先請求番号") = row("仕入先請求番号").ToString
                        addRow("支払予定日") = row("支払予定日").ToString
                        addRow("備考1") = row("備考1").ToString
                        addRow("備考2") = row("備考2").ToString
                        addDt.Rows.Add(addRow) '行追加

                    Next


                    'エラーがあったら
                    If errorCnt > 0 Then
                        '読み込みファイルエラーのアラートを出す
                        _msgHd.dspMSG("importFileError", frmC01F10_Login.loginValue.Language, errorMsg)
                    Else

                        Dim saveMsg As String = "登録件数：" & saveCnt & vbCrLf & "スキップ件数：" & skipCnt

                        '取消確認のアラート
                        Dim result As DialogResult = _msgHd.dspMSG("registrationConfirmation",
                                                                       frmC01F10_Login.loginValue.Language,
                                                                       saveMsg)

                        If result = DialogResult.Yes Then
                            '買掛一括登録実行
                            kikeAddList(addDt) '登録一覧を渡す
                            'Else
                            '    Exit Do
                        End If

                    End If

                End If

            Catch ex As Exception
                Throw ex
            Finally
                'カーソルをビジー状態から元に戻す
                Cursor.Current = Cursors.Default

            End Try

        End If
    End Sub

    '対象データから買掛一覧を登録する
    Private Sub kikeAddList(ByVal hacchuList As System.Data.DataTable)

        For Each row As DataRow In hacchuList.Rows
            kikeAdd(row)
        Next

    End Sub



    '発注データ詳細取得および買掛データ登録
    Private Sub kikeAdd(ByVal prmRow As DataRow)

        '登録
        Dim dtToday As DateTime = DateTime.Now
        Dim strToday As String = UtilClass.formatDatetime(dtToday)

        Dim reccnt As Integer = 0
        Dim Sql As String = ""
        Dim PurchaseCostFC As Decimal = 0  '仕入原価_外貨
        Dim PurchaseAmountFC As Decimal = 0  '仕入金額_外貨
        Dim VAT_FC As Decimal = 0
        Dim AccountsPayable As Decimal = 0    '買掛残高
        Dim AccountsPayableFC As Decimal = 0  '買掛残高_外貨
        Dim BuyToHangAmount As Decimal = 0    '今回買掛金額計
        Dim BuyToHangAmountFC As Decimal = 0  '今回買掛金額計_外貨

        Dim AP As String = getSaiban("100", dtToday)

        Sql = " AND 発注番号 = '" & prmRow("発注番号").ToString & "'"
        Sql += " AND 発注番号枝番 = '" & prmRow("発注番号枝番").ToString & "'"
        Sql += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        Dim dsHattyu As DataSet = getDsData("t20_hattyu", Sql)


        Sql = " SELECT t21.* ,t20.仕入先コード ,t20.ＶＡＴ"
        Sql += " FROM "
        Sql += " t20_hattyu t20"
        Sql += " INNER JOIN t21_hattyu t21 ON "

        Sql += " t20.""発注番号"" = t21.""発注番号"""
        Sql += " AND "
        Sql += " t20.""発注番号枝番"" = t21.""発注番号枝番"""

        Sql += " where "
        Sql += " t20.""会社コード"" = '" & frmC01F10_Login.loginValue.BumonCD & "'"

        Sql += " AND "
        Sql += " t20.""発注番号"" ILIKE '" & prmRow("発注番号").ToString & "'"
        Sql += " AND "
        Sql += " t20.""発注番号枝番"" ILIKE '" & prmRow("発注番号枝番").ToString & "'"
        Sql += " AND "
        Sql += " t20.""取消区分"" = " & CommonConst.CANCEL_KBN_ENABLED
        Sql += " ORDER BY 行番号 ASC "

        Dim dsHattyudt As DataSet = _db.selectDB(Sql, RS, reccnt)

        '買掛金額計を集計
        '発注明細データより仕入金額を算出
        For i As Integer = 0 To dsHattyudt.Tables(RS).Rows.Count - 1
            PurchaseCostFC = PurchaseCostFC + (dsHattyudt.Tables(RS).Rows(i)("仕入値_外貨") * dsHattyudt.Tables(RS).Rows(i)("仕入数量"))
        Next

        VAT_FC = PurchaseCostFC * dsHattyu.Tables(RS).Rows(0)("ＶＡＴ").ToString / 100 '買掛金額（VAT)
        PurchaseAmountFC = PurchaseCostFC + VAT_FC '買掛金額

        'レートの取得
        Dim strRate As Decimal = setRate(dsHattyu.Tables(RS).Rows(0)("通貨").ToString(), prmRow("請求日").ToString)

        '今回買掛金額計
        BuyToHangAmountFC = PurchaseAmountFC
        BuyToHangAmount = Math.Ceiling(PurchaseAmountFC / strRate)  '画面の金額をIDRに変換　切り上げ

        '買掛残高
        AccountsPayableFC = PurchaseAmountFC


        Sql = "INSERT INTO "
        Sql += "Public."
        Sql += "t46_kikehd("
        Sql += "会社コード, 買掛番号, 買掛区分, 買掛日, 発注番号, 発注番号枝番, 客先番号, 仕入先コード, 仕入先名, 買掛金額計, 買掛残高"
        Sql += ", 備考1, 備考2, 取消区分, 登録日, 更新者, 更新日, 支払予定日"
        Sql += ", 買掛金額計_外貨, 買掛残高_外貨, 通貨, レート, 仕入先請求番号)"

        Sql += " VALUES('"

        Sql += dsHattyu.Tables(RS).Rows(0)("会社コード").ToString '会社コード
        Sql += "', '"
        Sql += AP '買掛番号
        Sql += "', '"
        Sql += CommonConst.APC_KBN_NORMAL.ToString '買掛区分
        Sql += "', '"
        Sql += UtilClass.strFormatDate(prmRow("請求日").ToString) '買掛日(請求日)
        Sql += "', '"
        Sql += dsHattyu.Tables(RS).Rows(0)("発注番号").ToString '発注番号
        Sql += "', '"
        Sql += dsHattyu.Tables(RS).Rows(0)("発注番号枝番").ToString '発注番号枝番
        Sql += "', '"
        Sql += dsHattyu.Tables(RS).Rows(0)("客先番号").ToString '客先番号
        Sql += "', '"
        Sql += dsHattyu.Tables(RS).Rows(0)("仕入先コード").ToString '仕入先コード
        Sql += "', '"
        Sql += dsHattyu.Tables(RS).Rows(0)("仕入先名").ToString '仕入先名
        Sql += "', "
        Sql += UtilClass.formatNumber(PurchaseAmountFC) '買掛金額計
        Sql += ", "
        Sql += "0" '買掛残高
        Sql += ", '"
        Sql += prmRow("備考1").ToString '備考1
        Sql += "', '"
        Sql += prmRow("備考2").ToString '備考2
        Sql += "', '"
        Sql += "0" '取消区分
        Sql += "', '"
        Sql += strToday '登録日
        Sql += "', '"
        Sql += frmC01F10_Login.loginValue.TantoNM '更新者
        Sql += "', '"
        Sql += strToday '更新日
        Sql += "', '"
        Sql += UtilClass.strFormatDate(prmRow("支払予定日").ToString) '支払予定日

        Sql += "','"
        Sql += UtilClass.formatNumber(BuyToHangAmountFC) '買掛金額計_外貨
        Sql += "',"
        Sql += UtilClass.formatNumber("0") '買掛残高_外貨

        Sql += ","
        Sql += dsHattyu.Tables(RS).Rows(0)("通貨").ToString() '通貨
        Sql += ",'"
        Sql += UtilClass.formatNumberF10(strRate) 'レート

        Sql += "','"
        Sql += prmRow("仕入先請求番号").ToString  '仕入先請求番号

        Sql += "')"

        _db.executeDB(Sql)

        '登録完了メッセージ
        _msgHd.dspMSG("completeInsert", frmC01F10_Login.loginValue.Language)

    End Sub


    '指定したファイルパス(Excel)を読み込み、対象データをDataTableに格納する
    Private Function ImportExcel(ByRef prmFilePath As String)
        '定義
        Dim app As Excel.Application = Nothing
        Dim books As Excel.Workbooks = Nothing
        Dim book As Excel.Workbook = Nothing
        Dim sheet As Excel.Worksheet = Nothing

        Dim dt As New System.Data.DataTable("Table")
        Dim row As DataRow

        Try
            'Excelアプリケーションの開始
            app = New Excel.Application()
            books = app.Workbooks
            book = books.Open(prmFilePath)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet) '1つ目のシートを選択

            'Excelから取得したデータと再度取得し、一括登録するデータが一致しているかどうかチェック
            Dim rowEnd As Integer = sheet.UsedRange.Rows.Count '最終行取得
            Dim colEnd As Integer = sheet.UsedRange.Columns.Count '最終列取得

            dt.Columns.Add("発注番号")
            dt.Columns.Add("発注番号枝番")
            dt.Columns.Add("請求日")
            dt.Columns.Add("仕入先請求番号")
            dt.Columns.Add("支払予定日")
            dt.Columns.Add("備考1")
            dt.Columns.Add("備考2")

            For i As Integer = 2 To rowEnd '2行目から

                row = dt.NewRow '行作成

                row("発注番号") = sheet.Cells(i, 1).Value
                row("発注番号枝番") = sheet.Cells(i, 2).Value
                row("請求日") = sheet.Cells(i, 3).Value
                row("仕入先請求番号") = sheet.Cells(i, 4).Value
                row("支払予定日") = sheet.Cells(i, 5).Value
                row("備考1") = sheet.Cells(i, 6).Value
                row("備考2") = sheet.Cells(i, 7).Value

                dt.Rows.Add(row) '行追加

            Next

        Catch ex As Exception

            Throw ex
        Finally

            app.Quit()

            'リソースの解放

            'Marshal.ReleaseComObject(range)
            Marshal.ReleaseComObject(sheet)
            Marshal.ReleaseComObject(book)
            Marshal.ReleaseComObject(books)
            Marshal.ReleaseComObject(app)

            'カーソルをビジー状態から元に戻す
            Cursor.Current = Cursors.Default


        End Try

        Return dt

    End Function

    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    'Excel出力する際のチェック
    Private Function excelOutput(ByVal prmFilePath As String)
        Dim fileChk As String = Dir(prmFilePath)
        Dim sr As StreamReader

        '同名ファイルがあるかどうかチェック
        If fileChk <> "" Then
            Dim result = _msgHd.dspMSG("confirmFileExist", frmC01F10_Login.loginValue.Language, prmFilePath)
            If result = DialogResult.No Then
                Return False
            End If

            Try
                'ファイルが開けるかどうかチェック
                sr = New StreamReader(prmFilePath)
                sr.Close() '処理が通ったら閉じる
                Marshal.ReleaseComObject(sr)
            Catch ex As Exception
                '開けない場合はアラートを表示してリターンさせる
                MessageBox.Show(ex.Message, CommonConst.AP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try

            Return True
        End If
        Return True
    End Function

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

    'param1：String 採番キー
    'param2：DateTime 登録日
    'Return: String 伝票番号
    '伝票番号を取得
    Private Function getSaiban(ByVal key As String, ByVal today As DateTime) As String
        Dim Sql As String = ""
        Dim saibanID As String = ""
        Dim reccnt As Integer = 0 'DB用（デフォルト）

        Try
            Sql = "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public.m80_saiban"
            Sql += " WHERE "
            Sql += "会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND "
            Sql += "採番キー = '" & key & "'"

            Dim dsSaiban As DataSet = _db.selectDB(Sql, RS, reccnt)

            saibanID = dsSaiban.Tables(RS).Rows(0)("接頭文字")
            saibanID += today.ToString("MMdd")
            saibanID += dsSaiban.Tables(RS).Rows(0)("最新値").ToString.PadLeft(dsSaiban.Tables(RS).Rows(0)("連番桁数"), "0")

            Dim keyNo As Integer

            If dsSaiban.Tables(RS).Rows(0)("最新値") = dsSaiban.Tables(RS).Rows(0)("最大値") Then
                '最新値が最大と同じ場合、最小値にリセット
                keyNo = dsSaiban.Tables(RS).Rows(0)("最小値")
            Else
                '最新値+1
                keyNo = dsSaiban.Tables(RS).Rows(0)("最新値") + 1
            End If

            Sql = "UPDATE "
            Sql += "Public.m80_saiban "
            Sql += "SET "
            Sql += " 最新値 "
            Sql += " = '"
            Sql += keyNo.ToString
            Sql += "', "
            Sql += "更新者"
            Sql += " = '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            Sql += "', "
            Sql += "更新日"
            Sql += " = '"
            Sql += UtilClass.formatDatetime(today)
            Sql += "' "
            Sql += "WHERE"
            Sql += " 会社コード"
            Sql += "='"
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "'"
            Sql += " AND"
            Sql += " 採番キー = '" & key & "'"
            Console.WriteLine(Sql)
            _db.executeDB(Sql)

            Return saibanID
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Function

    '通貨の採番キーからレートを取得・設定
    '基準日が買掛日「以前」の最新のもの
    Private Function setRate(ByVal strKey As Integer, ByVal strAPDate As String) As Decimal
        Dim Sql As String

        Sql = " AND 採番キー = " & strKey & ""
        Sql += " AND 基準日 < '" & UtilClass.strFormatDate(strAPDate) & "'"  '買掛日
        Sql += " ORDER BY 基準日 DESC "

        Dim ds As DataSet = getDsData("t71_exchangerate", Sql)

        If ds.Tables(RS).Rows.Count > 0 Then
            setRate = ds.Tables(RS).Rows(0)("レート")
        Else
            'If CultureInfo.CurrentCulture.Name.ToString = CommonConst.CI_ID Then
            '    setRate = CommonConst.BASE_RATE_IDR
            'Else
            '    setRate = CommonConst.BASE_RATE_JPY
            'End If
            setRate = 1.ToString("F10")
        End If

    End Function

End Class