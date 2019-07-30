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

Public Class MstSupplier
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

    Private Sub MstSupplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
            Label1.Text = "SupplierName"
            TxtSearch.Location = New System.Drawing.Point(110, 6)
            BtnSearch.Text = "Search"
            BtnSearch.Location = New System.Drawing.Point(216, 5)
            btnSupplierAdd.Text = "Add"
            btnSupplierEdit.Text = "Edit"
            BtnBack.Text = "Back"
            btnOutputSupplier.Text = "Output EXCEL"

            Dgv_Supplier.Columns("会社コード").HeaderText = "CompanyCode"
            Dgv_Supplier.Columns("仕入先コード").HeaderText = "SupplierCode"
            Dgv_Supplier.Columns("仕入先名").HeaderText = "SupplierName"
            Dgv_Supplier.Columns("仕入先名略称").HeaderText = "SupplierShortName"
            Dgv_Supplier.Columns("郵便番号").HeaderText = "PostalCode"
            Dgv_Supplier.Columns("住所１").HeaderText = "Address1"
            Dgv_Supplier.Columns("住所２").HeaderText = "Address2"
            Dgv_Supplier.Columns("住所３").HeaderText = "Address3"
            Dgv_Supplier.Columns("電話番号").HeaderText = "PhoneNumber"
            Dgv_Supplier.Columns("電話番号検索用").HeaderText = "PhoneNumber(ForSearch)"
            Dgv_Supplier.Columns("FAX番号").HeaderText = "FAX"
            Dgv_Supplier.Columns("担当者名").HeaderText = "NameOfPIC"
            Dgv_Supplier.Columns("担当者役職").HeaderText = "PositionPICCustomer"
            Dgv_Supplier.Columns("メモ").HeaderText = "Memo"
            Dgv_Supplier.Columns("銀行名").HeaderText = "BankName"
            Dgv_Supplier.Columns("銀行コード").HeaderText = "BankCode"
            Dgv_Supplier.Columns("支店名").HeaderText = "BranchName"
            Dgv_Supplier.Columns("支店コード").HeaderText = "BranchCode"
            Dgv_Supplier.Columns("預金種目").HeaderText = "DepositCategory"
            Dgv_Supplier.Columns("口座番号").HeaderText = "AccountNumber"
            Dgv_Supplier.Columns("口座名義").HeaderText = "AccountHolder"
            Dgv_Supplier.Columns("関税率").HeaderText = "CustomsDutyRate"
            Dgv_Supplier.Columns("前払法人税率").HeaderText = "PPH"
            Dgv_Supplier.Columns("輸送費率").HeaderText = "TransportationCostRate"
            Dgv_Supplier.Columns("会計用仕入先コード").HeaderText = "AccountingVendorCode"
            Dgv_Supplier.Columns("国内区分").HeaderText = "DomesticDivision"
            Dgv_Supplier.Columns("更新者").HeaderText = "ModifiedBy"
            Dgv_Supplier.Columns("更新日").HeaderText = "UpdateDate"

        End If

        setList()
    End Sub

    Private Sub setList()
        Dgv_Supplier.Rows.Clear()

        Dim Sql As String = ""
        Try
            Sql += "SELECT "

            Sql += "m11.会社コード, "
            Sql += "m11.仕入先コード, "
            Sql += "m11.仕入先名, "
            Sql += "m11.仕入先名略称, "
            Sql += "m11.郵便番号, "
            Sql += "m11.住所１, "
            Sql += "m11.住所２, "
            Sql += "m11.住所３, "
            Sql += "m11.電話番号, "
            Sql += "m11.電話番号検索用, "
            Sql += "m11.ＦＡＸ番号, "
            Sql += "m11.担当者名, "
            Sql += "m11.担当者役職, "
            Sql += "m11.既定間接費率, "
            Sql += "m11.メモ, "
            Sql += "m11.銀行コード, "
            Sql += "m11.銀行名, "
            Sql += "m11.支店コード, "
            Sql += "m11.支店名, "
            Sql += "m11.預金種目, "
            Sql += "m11.口座番号, "
            Sql += "m11.口座名義, "
            Sql += "m11.関税率, "
            Sql += "m11.前払法人税率, "
            Sql += "m11.輸送費率, "
            Sql += "m11.会計用仕入先コード, "
            Sql += "m90.文字１, "
            Sql += "m90.文字２, "
            Sql += "m11.更新者, "
            Sql += "m11.更新日 "

            Sql += " FROM m11_supplier m11"
            Sql += " LEFT JOIN m90_hanyo m90"
            Sql += " ON m11.会社コード = m90.会社コード "
            Sql += " AND m90.固定キー = '" & CommonConst.DD_CODE & "'"
            Sql += " AND m11.国内区分 = m90.可変キー "

            Sql += " WHERE "
            Sql += " m11.会社コード"
            Sql += " ILIKE "
            Sql += "'"
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "'"
            Sql += " AND m11.仕入先名 ILIKE "
            Sql += "'%"
            Sql += TxtSearch.Text
            Sql += "%'"
            Sql += " order by m11.会社コード, m11.仕入先コード "

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                Console.WriteLine(ds.Tables(RS).Rows(i)("仕入先コード"))

                Dim getHanyo As DataSet
                If Not IsDBNull(ds.Tables(RS).Rows(i)("預金種目")) Then
                    getHanyo = getDsHanyoData(CommonConst.DC_CODE, Trim(ds.Tables(RS).Rows(i)("預金種目")))
                End If


                Dgv_Supplier.Rows.Add()
                Dgv_Supplier.Rows(i).Cells("会社コード").Value = ds.Tables(RS).Rows(i)("会社コード")
                Dgv_Supplier.Rows(i).Cells("仕入先コード").Value = ds.Tables(RS).Rows(i)("仕入先コード")
                Dgv_Supplier.Rows(i).Cells("仕入先名").Value = ds.Tables(RS).Rows(i)("仕入先名")
                Dgv_Supplier.Rows(i).Cells("仕入先名略称").Value = ds.Tables(RS).Rows(i)("仕入先名略称")
                Dgv_Supplier.Rows(i).Cells("郵便番号").Value = ds.Tables(RS).Rows(i)("郵便番号")
                Dgv_Supplier.Rows(i).Cells("住所１").Value = ds.Tables(RS).Rows(i)("住所１")
                Dgv_Supplier.Rows(i).Cells("住所２").Value = ds.Tables(RS).Rows(i)("住所２")
                Dgv_Supplier.Rows(i).Cells("住所３").Value = ds.Tables(RS).Rows(i)("住所３")
                Dgv_Supplier.Rows(i).Cells("電話番号").Value = ds.Tables(RS).Rows(i)("電話番号")
                Dgv_Supplier.Rows(i).Cells("電話番号検索用").Value = ds.Tables(RS).Rows(i)("電話番号検索用")
                Dgv_Supplier.Rows(i).Cells("FAX番号").Value = ds.Tables(RS).Rows(i)("ＦＡＸ番号")
                Dgv_Supplier.Rows(i).Cells("担当者名").Value = ds.Tables(RS).Rows(i)("担当者名")
                Dgv_Supplier.Rows(i).Cells("メモ").Value = ds.Tables(RS).Rows(i)("メモ")
                Dgv_Supplier.Rows(i).Cells("銀行名").Value = ds.Tables(RS).Rows(i)("銀行名")
                Dgv_Supplier.Rows(i).Cells("銀行コード").Value = ds.Tables(RS).Rows(i)("銀行コード")
                Dgv_Supplier.Rows(i).Cells("支店名").Value = ds.Tables(RS).Rows(i)("支店名")
                Dgv_Supplier.Rows(i).Cells("支店コード").Value = ds.Tables(RS).Rows(i)("支店コード")

                If IsDBNull(ds.Tables(RS).Rows(i)("預金種目")) Then
                    Dgv_Supplier.Rows(i).Cells("預金種目").Value = vbNullString
                Else
                    Dgv_Supplier.Rows(i).Cells("預金種目").Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                                               getHanyo.Tables(RS).Rows(0)("文字２"),
                                                               getHanyo.Tables(RS).Rows(0)("文字１"))
                End If

                Dgv_Supplier.Rows(i).Cells("口座番号").Value = ds.Tables(RS).Rows(i)("口座番号")
                Dgv_Supplier.Rows(i).Cells("口座名義").Value = ds.Tables(RS).Rows(i)("口座名義")
                Dgv_Supplier.Rows(i).Cells("担当者役職").Value = ds.Tables(RS).Rows(i)("担当者役職")
                Dgv_Supplier.Rows(i).Cells("関税率").Value = ds.Tables(RS).Rows(i)("関税率") * 100
                Dgv_Supplier.Rows(i).Cells("前払法人税率").Value = ds.Tables(RS).Rows(i)("前払法人税率") * 100
                Dgv_Supplier.Rows(i).Cells("輸送費率").Value = ds.Tables(RS).Rows(i)("輸送費率") * 100
                Dgv_Supplier.Rows(i).Cells("会計用仕入先コード").Value = ds.Tables(RS).Rows(i)("会計用仕入先コード")
                Dgv_Supplier.Rows(i).Cells("国内区分").Value = IIf(frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG,
                                                               ds.Tables(RS).Rows(i)("文字２"),
                                                               ds.Tables(RS).Rows(i)("文字１"))
                Dgv_Supplier.Rows(i).Cells("更新者").Value = ds.Tables(RS).Rows(i)("更新者")
                Dgv_Supplier.Rows(i).Cells("更新日").Value = ds.Tables(RS).Rows(i)("更新日")

            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    Private Sub btnSupplierAdd_Click(sender As Object, e As EventArgs) Handles btnSupplierAdd.Click
        Dim openForm As Form = Nothing
        Dim Status As String = CommonConst.STATUS_ADD

        openForm = New Supplier(_msgHd, _db, _langHd, Me, Status)   '処理選択
        openForm.Show()
        Me.Hide()   ' 自分は隠れる
    End Sub

    Private Sub btnSupplierEdit_Click(sender As Object, e As EventArgs) Handles btnSupplierEdit.Click
        Dim openForm As Form = Nothing
        Dim Status As String = CommonConst.STATUS_EDIT
        Dim CompanyCode As String = Dgv_Supplier.Rows(Dgv_Supplier.CurrentCell.RowIndex).Cells("会社コード").Value
        Dim SupplierCode As String = Dgv_Supplier.Rows(Dgv_Supplier.CurrentCell.RowIndex).Cells("仕入先コード").Value
        openForm = New Supplier(_msgHd, _db, _langHd, Me, Status, CompanyCode, SupplierCode)   '処理選択
        openForm.Show()
        Me.Hide()   ' 自分は隠れる
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        setList()
    End Sub

    Private Sub MstSupplier_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        setList()
    End Sub

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
            Dim sHinaFile As String = sHinaPath & "\" & "SupplierList.xlsx"
            '出力先パス
            Dim sOutPath As String = StartUp._iniVal.OutXlsPath
            '出力ファイル名
            Dim sOutFile As String = sOutPath & "\SupplierList_" & DateTime.Now.ToString("yyyyMMddHHmm") & ".xlsx"

            app = New Excel.Application()
            book = app.Workbooks.Add(sHinaFile)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet)

            sheet.PageSetup.LeftHeader = "SPIN"
            sheet.PageSetup.CenterHeader = "Supplier List"
            sheet.PageSetup.RightHeader = "OutputDate：" & DateTime.Now.ToShortDateString

            If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then
                sheet.PageSetup.LeftHeader = "SPIN"
                sheet.PageSetup.CenterHeader = "Supplier List"

                sheet.Range("A1").Value = "SupplierCode"
                sheet.Range("B1").Value = "SupplierName"
                sheet.Range("C1").Value = "SupplierShortName"
                sheet.Range("D1").Value = "PostalCode"
                sheet.Range("E1").Value = "Address1"
                sheet.Range("F1").Value = "Address2"
                sheet.Range("G1").Value = "Address3"
                sheet.Range("H1").Value = "PhoneNumber"
                sheet.Range("I1").Value = "PhoneNumber(ForSearch)"
                sheet.Range("J1").Value = "FAX"
                sheet.Range("K1").Value = "NameOfPIC"
                sheet.Range("L1").Value = "PositionPICCustomer"
                sheet.Range("M1").Value = "Memo"
                sheet.Range("N1").Value = "BankName"
                sheet.Range("O1").Value = "BankCode"
                sheet.Range("P1").Value = "BranchName"
                sheet.Range("Q1").Value = "BranchCode"
                sheet.Range("R1").Value = "DepositCategory"
                sheet.Range("S1").Value = "AccountNumber"
                sheet.Range("T1").Value = "AccountHolder"
                sheet.Range("U1").Value = "CustomsDutyRate"
                sheet.Range("V1").Value = "PPH"
                sheet.Range("W1").Value = "TransportationCostRate"
                sheet.Range("X1").Value = "AccountingVendorCode"
                sheet.Range("Y1").Value = "DomesticClassification"
                sheet.Range("Z1").Value = "ModifiedBy"
                sheet.Range("AA1").Value = "UpdateDate"

            End If

            Dim cellRowIndex As Integer = 1

            For i As Integer = 0 To Dgv_Supplier.RowCount - 1

                cellRowIndex += 1
                sheet.Rows(cellRowIndex).Insert
                sheet.Range("A" & cellRowIndex.ToString).Value = Dgv_Supplier.Rows(i).Cells("仕入先コード").Value '
                sheet.Range("B" & cellRowIndex.ToString).Value = Dgv_Supplier.Rows(i).Cells("仕入先名").Value '
                sheet.Range("C" & cellRowIndex.ToString).Value = Dgv_Supplier.Rows(i).Cells("仕入先名略称").Value '
                sheet.Range("D" & cellRowIndex.ToString).Value = Dgv_Supplier.Rows(i).Cells("郵便番号").Value '
                sheet.Range("E" & cellRowIndex.ToString).Value = Dgv_Supplier.Rows(i).Cells("住所１").Value '
                sheet.Range("F" & cellRowIndex.ToString).Value = Dgv_Supplier.Rows(i).Cells("住所２").Value '
                sheet.Range("G" & cellRowIndex.ToString).Value = Dgv_Supplier.Rows(i).Cells("住所３").Value '
                sheet.Range("H" & cellRowIndex.ToString).Value = Dgv_Supplier.Rows(i).Cells("電話番号").Value '
                sheet.Range("I" & cellRowIndex.ToString).Value = Dgv_Supplier.Rows(i).Cells("電話番号検索用").Value '
                sheet.Range("J" & cellRowIndex.ToString).Value = Dgv_Supplier.Rows(i).Cells("FAX番号").Value '
                sheet.Range("K" & cellRowIndex.ToString).Value = Dgv_Supplier.Rows(i).Cells("担当者名").Value '
                sheet.Range("L" & cellRowIndex.ToString).Value = Dgv_Supplier.Rows(i).Cells("担当者役職").Value '
                sheet.Range("M" & cellRowIndex.ToString).Value = Dgv_Supplier.Rows(i).Cells("メモ").Value '
                sheet.Range("N" & cellRowIndex.ToString).Value = Dgv_Supplier.Rows(i).Cells("銀行名").Value '
                sheet.Range("O" & cellRowIndex.ToString).Value = Dgv_Supplier.Rows(i).Cells("銀行コード").Value '
                sheet.Range("P" & cellRowIndex.ToString).Value = Dgv_Supplier.Rows(i).Cells("支店名").Value '
                sheet.Range("Q" & cellRowIndex.ToString).Value = Dgv_Supplier.Rows(i).Cells("支店コード").Value '
                sheet.Range("R" & cellRowIndex.ToString).Value = Dgv_Supplier.Rows(i).Cells("預金種目").Value '
                sheet.Range("S" & cellRowIndex.ToString).Value = Dgv_Supplier.Rows(i).Cells("口座番号").Value '
                sheet.Range("T" & cellRowIndex.ToString).Value = Dgv_Supplier.Rows(i).Cells("口座名義").Value '
                sheet.Range("U" & cellRowIndex.ToString).Value = Dgv_Supplier.Rows(i).Cells("関税率").Value '
                sheet.Range("V" & cellRowIndex.ToString).Value = Dgv_Supplier.Rows(i).Cells("前払法人税率").Value '
                sheet.Range("W" & cellRowIndex.ToString).Value = Dgv_Supplier.Rows(i).Cells("輸送費率").Value '
                sheet.Range("X" & cellRowIndex.ToString).Value = Dgv_Supplier.Rows(i).Cells("会計用仕入先コード").Value '
                sheet.Range("Y" & cellRowIndex.ToString).Value = Dgv_Supplier.Rows(i).Cells("国内区分").Value '
                sheet.Range("Z" & cellRowIndex.ToString).Value = Dgv_Supplier.Rows(i).Cells("更新者").Value '
                sheet.Range("AA" & cellRowIndex.ToString).Value = Dgv_Supplier.Rows(i).Cells("更新日").Value.ToString '
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

    Private Sub btnOutputSupplier_Click(sender As Object, e As EventArgs) Handles btnOutputSupplier.Click

        Dim c_ As Cursor = Me.Cursor
        Me.Cursor = Cursors.WaitCursor
        outputExcel()
        Me.Cursor = c_

    End Sub
End Class