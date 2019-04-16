Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel


Public Class MstCustomer
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
                   ByRef prmRefForm As Form)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    Private Sub MstCustomere_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If frmC01F10_Login.loginValue.Language = "ENG" Then
            Label1.Text = "CustomerName"
            TxtSearch.Location = New System.Drawing.Point(120, 6)
            BtnSearch.Text = "Search"
            BtnSearch.Location = New System.Drawing.Point(226, 6)
            btnCustomerAdd.Text = "Add"
            btnCustomerEdit.Text = "Edit"
            btnBack.Text = "Back"
            BtnOutputCustomer.Text = "Output EXCEL"

            Dgv_Customer.Columns("会社コード").HeaderText = "CompanyCode"
            Dgv_Customer.Columns("得意先コード").HeaderText = "CustomerCode"
            Dgv_Customer.Columns("得意先名").HeaderText = "CustomerName"
            Dgv_Customer.Columns("得意先名略称").HeaderText = "CustomerShortName"
            Dgv_Customer.Columns("郵便番号").HeaderText = "PostalCode"
            Dgv_Customer.Columns("住所１").HeaderText = "Address1"
            Dgv_Customer.Columns("住所２").HeaderText = "Address2"
            Dgv_Customer.Columns("住所３").HeaderText = "Address3"
            Dgv_Customer.Columns("電話番号").HeaderText = "TEL"
            Dgv_Customer.Columns("電話番号検索用").HeaderText = "TEL(ForSearch)"
            Dgv_Customer.Columns("FAX番号").HeaderText = "FAX"
            Dgv_Customer.Columns("担当者名").HeaderText = "ContactPersonName"
            Dgv_Customer.Columns("担当者役職").HeaderText = "ContactPersonPosition"
            Dgv_Customer.Columns("既定支払条件").HeaderText = "PaymentTerms"
            Dgv_Customer.Columns("メモ").HeaderText = "Memo"
            Dgv_Customer.Columns("会計用得意先コード").HeaderText = "AccountingCustomerCode"
            Dgv_Customer.Columns("国内区分").HeaderText = "DomesticClassification"
            Dgv_Customer.Columns("更新者").HeaderText = "ModifiedBy"
            Dgv_Customer.Columns("更新日").HeaderText = "UpdateDate"

        End If

        setList()
    End Sub

    '一覧取得
    Private Sub setList()
        Dgv_Customer.Rows.Clear()

        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += "m10.会社コード, "
            Sql += "m10.得意先コード, "
            Sql += "m10.得意先名, "
            Sql += "m10.得意先名略称, "
            Sql += "m10.郵便番号, "
            Sql += "m10.住所１, "
            Sql += "m10.住所２, "
            Sql += "m10.住所３, "
            Sql += "m10.電話番号, "
            Sql += "m10.電話番号検索用, "
            Sql += "m10.ＦＡＸ番号, "
            Sql += "m10.担当者名, "
            Sql += "m10.担当者役職, "
            Sql += "m10.既定支払条件, "
            Sql += "m10.メモ, "
            Sql += "m10.会計用得意先コード, "
            Sql += "m90.文字１, "
            Sql += "m90.文字２, "
            Sql += "m10.更新者, "
            Sql += "m10.更新日 "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "m10_customer m10"
            Sql += " LEFT JOIN m90_hanyo m90"
            Sql += " ON m10.会社コード = m90.会社コード "
            Sql += " AND m90.固定キー = '" & CommonConst.DD_CODE & "'"
            Sql += " AND m10.国内区分 = m90.可変キー "

            Sql += " WHERE "
            Sql += " m10.会社コード"
            Sql += " ILIKE "
            Sql += "'"
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "'"
            Sql += " AND "
            Sql += "得意先名"
            Sql += " ILIKE "
            Sql += "'%"
            Sql += TxtSearch.Text
            Sql += "%'"
            Sql += " order by m10.会社コード, m10.得意先コード "



            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                Dgv_Customer.Rows.Add()
                Dgv_Customer.Rows(i).Cells("会社コード").Value = ds.Tables(RS).Rows(i)("会社コード")
                Dgv_Customer.Rows(i).Cells("得意先コード").Value = ds.Tables(RS).Rows(i)("得意先コード")
                Dgv_Customer.Rows(i).Cells("得意先名").Value = ds.Tables(RS).Rows(i)("得意先名")
                Dgv_Customer.Rows(i).Cells("得意先名略称").Value = ds.Tables(RS).Rows(i)("得意先名略称")
                Dgv_Customer.Rows(i).Cells("郵便番号").Value = ds.Tables(RS).Rows(i)("郵便番号")
                Dgv_Customer.Rows(i).Cells("住所１").Value = ds.Tables(RS).Rows(i)("住所１")
                Dgv_Customer.Rows(i).Cells("住所２").Value = ds.Tables(RS).Rows(i)("住所２")
                Dgv_Customer.Rows(i).Cells("住所３").Value = ds.Tables(RS).Rows(i)("住所３")
                Dgv_Customer.Rows(i).Cells("電話番号").Value = ds.Tables(RS).Rows(i)("電話番号")
                Dgv_Customer.Rows(i).Cells("電話番号検索用").Value = ds.Tables(RS).Rows(i)("電話番号検索用")
                Dgv_Customer.Rows(i).Cells("FAX番号").Value = ds.Tables(RS).Rows(i)("ＦＡＸ番号")
                Dgv_Customer.Rows(i).Cells("担当者名").Value = ds.Tables(RS).Rows(i)("担当者名")
                Dgv_Customer.Rows(i).Cells("担当者役職").Value = ds.Tables(RS).Rows(i)("担当者役職")
                Dgv_Customer.Rows(i).Cells("既定支払条件").Value = ds.Tables(RS).Rows(i)("既定支払条件")
                Dgv_Customer.Rows(i).Cells("メモ").Value = ds.Tables(RS).Rows(i)("メモ")
                Dgv_Customer.Rows(i).Cells("会計用得意先コード").Value = ds.Tables(RS).Rows(i)("会計用得意先コード")
                Dgv_Customer.Rows(i).Cells("国内区分").Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                                               ds.Tables(RS).Rows(i)("文字２"),
                                                               ds.Tables(RS).Rows(i)("文字１"))
                Dgv_Customer.Rows(i).Cells("更新者").Value = ds.Tables(RS).Rows(i)("更新者")
                Dgv_Customer.Rows(i).Cells("更新日").Value = ds.Tables(RS).Rows(i)("更新日")

            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    Private Sub btnCustomerAdd_Click(sender As Object, e As EventArgs) Handles btnCustomerAdd.Click
        Dim openForm As Form = Nothing
        Dim Status As String = CommonConst.STATUS_ADD
        openForm = New Customer(_msgHd, _db, _langHd, Me, Status)   '処理選択
        openForm.Show()
        Me.Hide()   ' 自分は隠れる
    End Sub

    Private Sub btnSelectCustomer_Click(sender As Object, e As EventArgs) Handles btnCustomerEdit.Click
        Dim openForm As Form = Nothing
        Dim Status As String = CommonConst.STATUS_EDIT
        Dim CompanyCode As String = Dgv_Customer.Rows(Dgv_Customer.CurrentCell.RowIndex).Cells("会社コード").Value
        Dim CustomerCode As String = Dgv_Customer.Rows(Dgv_Customer.CurrentCell.RowIndex).Cells("得意先コード").Value
        openForm = New Customer(_msgHd, _db, _langHd, Me, Status, CompanyCode, CustomerCode)   '処理選択
        openForm.Show()
        Me.Hide()   ' 自分は隠れる
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        setList()
        'Dgv_Customer.Rows.Clear()

        'Dim Sql As String = ""
        'Try
        '    Sql += "SELECT "
        '    Sql += "* "
        '    Sql += "FROM "
        '    Sql += "public"
        '    Sql += "."
        '    Sql += "m10_customer"
        '    Sql += " WHERE "
        '    Sql += "会社コード"
        '    Sql += " ILIKE "
        '    Sql += "'"
        '    Sql += frmC01F10_Login.loginValue.BumonCD
        '    Sql += "'"
        '    Sql += " AND "
        '    Sql += "得意先名"
        '    Sql += " ILIKE "
        '    Sql += "'%"
        '    Sql += TxtSearch.Text
        '    Sql += "%'"

        '    Dim reccnt As Integer = 0
        '    Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

        '    For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1

        '        Dgv_Customer.Rows.Add()

        '        Dgv_Customer.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)("会社コード")
        '        Dgv_Customer.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)("得意先コード")
        '        Dgv_Customer.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)("得意先名")
        '        Dgv_Customer.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)("得意先名略称")
        '        Dgv_Customer.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)("郵便番号")
        '        Dgv_Customer.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)("住所１")
        '        Dgv_Customer.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)("住所２")
        '        Dgv_Customer.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)("住所３")
        '        Dgv_Customer.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)("電話番号")
        '        Dgv_Customer.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)("電話番号検索用")
        '        Dgv_Customer.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)("ＦＡＸ番号")
        '        Dgv_Customer.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)("担当者名")
        '        Dgv_Customer.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)("担当者役職")
        '        Dgv_Customer.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)("既定支払条件")
        '        Dgv_Customer.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)("メモ")
        '        Dgv_Customer.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)("会計用得意先コード")
        '        Dgv_Customer.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)("更新者")
        '        Dgv_Customer.Rows(index).Cells(17).Value = ds.Tables(RS).Rows(index)("更新日")

        '    Next

        'Catch ue As UsrDefException
        '    ue.dspMsg()
        '    Throw ue
        'Catch ex As Exception
        '    'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
        '    Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        'End Try
    End Sub

    Private Sub MstCustomer_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        setList() '一覧取得
    End Sub

    Private Sub BtnOutputCustomer_Click(sender As Object, e As EventArgs) Handles BtnOutputCustomer.Click
        Dim c_ As Cursor = Me.Cursor
        Me.Cursor = Cursors.WaitCursor
        outputExcel()
        Me.Cursor = c_
    End Sub

    'excel出力処理
    Private Sub outputExcel()

        '定義
        Dim app As Excel.Application = Nothing
        Dim book As Excel.Workbook = Nothing
        Dim sheet As Excel.Worksheet = Nothing

        'Dim strSelectYear As String = cmbYear.SelectedValue.ToString()
        'Dim strSelectMonth As String = cmbMonth.SelectedValue.ToString()

        ' セル
        Dim xlRngTmp As Range = Nothing
        Dim xlRng As Range = Nothing

        ' セル境界線（枠）
        Dim xlBorders As Borders = Nothing
        Dim xlBorder As Border = Nothing

        Try
            '雛形パス
            Dim sHinaPath As String = StartUp._iniVal.BaseXlsPath
            '雛形ファイル名
            Dim sHinaFile As String = sHinaPath & "\" & "CustomerList.xlsx"
            '出力先パス
            Dim sOutPath As String = StartUp._iniVal.OutXlsPath
            '出力ファイル名
            Dim sOutFile As String = sOutPath & "\CustomerList_" & DateTime.Now.ToString("yyyyMMddHHmm") & ".xlsx"

            app = New Excel.Application()
            book = app.Workbooks.Add(sHinaFile)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet)

            sheet.PageSetup.LeftHeader = "SPIN"
            sheet.PageSetup.CenterHeader = "Customer List"
            sheet.PageSetup.RightHeader = "OutputDate：" & DateTime.Now.ToShortDateString

            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                sheet.PageSetup.LeftHeader = "SPIN"
                sheet.PageSetup.CenterHeader = "Customer List"
                sheet.Range("A1").Value = "CustomerCode"
                sheet.Range("B1").Value = "CustomerName"
                sheet.Range("C1").Value = "CustomerShortName"
                sheet.Range("D1").Value = "PostalCode"
                sheet.Range("E1").Value = "Address1"
                sheet.Range("F1").Value = "Address2"
                sheet.Range("G1").Value = "Address3"
                sheet.Range("H1").Value = "TEL"
                sheet.Range("I1").Value = "TEL(ForSearch)"
                sheet.Range("J1").Value = "FAX"
                sheet.Range("K1").Value = "ContactPersonName"
                sheet.Range("L1").Value = "ContactPersonPosition"
                sheet.Range("M1").Value = "PaymentTerms"
                sheet.Range("N1").Value = "Memo"
                sheet.Range("O1").Value = "AccountingCustomerCode"
                sheet.Range("P1").Value = "DomesticClassification"
                sheet.Range("Q1").Value = "ModifiedBy"
                sheet.Range("R1").Value = "UpdateDate"

            End If

            Dim cellRowIndex As Integer = 1

            For i As Integer = 0 To Dgv_Customer.RowCount - 1

                cellRowIndex += 1
                sheet.Rows(cellRowIndex).Insert
                sheet.Range("A" & cellRowIndex.ToString).Value = Dgv_Customer.Rows(i).Cells("得意先コード").Value '
                sheet.Range("B" & cellRowIndex.ToString).Value = Dgv_Customer.Rows(i).Cells("得意先名").Value '
                sheet.Range("C" & cellRowIndex.ToString).Value = Dgv_Customer.Rows(i).Cells("得意先名略称").Value '
                sheet.Range("D" & cellRowIndex.ToString).Value = Dgv_Customer.Rows(i).Cells("郵便番号").Value '
                sheet.Range("E" & cellRowIndex.ToString).Value = Dgv_Customer.Rows(i).Cells("住所１").Value '
                sheet.Range("F" & cellRowIndex.ToString).Value = Dgv_Customer.Rows(i).Cells("住所２").Value '
                sheet.Range("G" & cellRowIndex.ToString).Value = Dgv_Customer.Rows(i).Cells("住所３").Value '
                sheet.Range("H" & cellRowIndex.ToString).Value = Dgv_Customer.Rows(i).Cells("電話番号").Value '
                sheet.Range("I" & cellRowIndex.ToString).Value = Dgv_Customer.Rows(i).Cells("電話番号検索用").Value '
                sheet.Range("J" & cellRowIndex.ToString).Value = Dgv_Customer.Rows(i).Cells("FAX番号").Value '
                sheet.Range("K" & cellRowIndex.ToString).Value = Dgv_Customer.Rows(i).Cells("担当者名").Value '
                sheet.Range("L" & cellRowIndex.ToString).Value = Dgv_Customer.Rows(i).Cells("担当者役職").Value '
                sheet.Range("M" & cellRowIndex.ToString).Value = Dgv_Customer.Rows(i).Cells("既定支払条件").Value '
                sheet.Range("N" & cellRowIndex.ToString).Value = Dgv_Customer.Rows(i).Cells("メモ").Value '
                sheet.Range("O" & cellRowIndex.ToString).Value = Dgv_Customer.Rows(i).Cells("会計用得意先コード").Value '
                sheet.Range("P" & cellRowIndex.ToString).Value = Dgv_Customer.Rows(i).Cells("国内区分").Value '
                sheet.Range("Q" & cellRowIndex.ToString).Value = Dgv_Customer.Rows(i).Cells("更新者").Value '
                sheet.Range("R" & cellRowIndex.ToString).Value = Dgv_Customer.Rows(i).Cells("更新日").Value '
            Next

            'app.Visible = True

            ' 行7全体のオブジェクトを作成
            xlRngTmp = sheet.Rows
            xlRng = xlRngTmp(cellRowIndex)

            '最後に合計行の追加
            cellRowIndex += 1
            'sheet.Range("K" & cellRowIndex.ToString).Value = TxtSalesAmount.Text '粗利率

            ' 境界線オブジェクトを作成 →7行目の下部に罫線を描画する
            xlBorders = xlRngTmp.Borders
            xlBorder = xlBorders(XlBordersIndex.xlEdgeBottom)
            xlBorder.LineStyle = XlLineStyle.xlContinuous

            book.SaveAs(sOutFile)
            app.Visible = True

            _msgHd.dspMSG("CreateExcel", frmC01F10_Login.loginValue.Language)

        Catch ex As Exception
            Throw ex


        Finally
            'app.Quit()
            'Marshal.ReleaseComObject(sheet)
            'Marshal.ReleaseComObject(book)
            'Marshal.ReleaseComObject(app)

        End Try


    End Sub

End Class