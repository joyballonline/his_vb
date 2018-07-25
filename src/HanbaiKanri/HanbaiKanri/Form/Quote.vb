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


Public Class Quote
    Inherits System.Windows.Forms.Form

    '------------------------------------------------------------------------------------------------------
    'メンバー定数宣言
    '------------------------------------------------------------------------------------------------------
    'PG制御文字 
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
    Private _db As UtilDBIf
    Private _langHd As UtilLangHandler
    Private _gh As UtilDataGridViewHandler
    Private _parentForm As Form
    Private _init As Boolean                             '初期処理済フラグ
    Private count As Integer = 0
    Private CompanyCode As String = ""
    Private KeyNo As String = ""
    Private QuoteNo As String = ""
    Private QuoteNoMin As String = ""
    Private QuoteNoMax As String = ""
    Private EditNo As String = ""
    Private EditSuffix As String = ""
    Private LoadFlg As Boolean = False
    Private Status As String = ""
    Private Input As String = ""

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
                   ByRef prmRefLangHd As UtilLangHandler,
                   ByRef prmRefForm As Form,
                   Optional ByRef prmRefNo As String = Nothing,
                   Optional ByRef prmRefSuffix As String = Nothing,
                   Optional ByRef prmRefStatus As String = Nothing)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLangHd
        _parentForm = prmRefForm
        EditNo = prmRefNo
        EditSuffix = prmRefSuffix
        Status = prmRefStatus
        Input = frmC01F10_Login.loginValue.TantoNM
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    Private Sub Quote_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'DateTimePickerのフォーマットを指定
        DtpRegistration.Text = DateAdd("m", 0, Now).ToString("yyyy/MM/dd")
        DtpQuote.Text = DateAdd("m", 0, Now).ToString("yyyy/MM/dd")
        DtpExpiration.Text = DateAdd("d", 7, Now).ToString("yyyy/MM/dd")

        DgvItemList.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        'セルの内容に合わせて、行の高さが自動的に調節されるようにする
        DgvItemList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        '"Column1"列のセルのテキストを折り返して表示する
        DgvItemList.Columns("型式").DefaultCellStyle.WrapMode = DataGridViewTriState.True

        'ComboBoxに表示する項目のリストを作成する
        Dim table As New DataTable("Table")
        table.Columns.Add("Display", GetType(String))
        table.Columns.Add("Value", GetType(Integer))
        table.Rows.Add("仕入", 1)
        table.Rows.Add("在庫", 2)
        table.Rows.Add("サービス", 9)

        'DataGridViewComboBoxColumnを作成
        Dim column As New DataGridViewComboBoxColumn()
        'DataGridViewComboBoxColumnのDataSourceを設定
        column.DataSource = table
        '実際の値が"Value"列、表示するテキストが"Display"列とする
        column.ValueMember = "Value"
        column.DisplayMember = "Display"
        column.HeaderText = "仕入区分"
        column.Name = "仕入区分"
        'column.ValueMember = 1
        'DataGridView1に追加する
        DgvItemList.Columns.Insert(1, column)

        If EditNo IsNot Nothing Then    '見積編集時
            '見積基本情報
            Dim Sql1 As String = ""
            Sql1 += "SELECT "
            Sql1 += "会社コード, "
            Sql1 += "見積番号, "
            Sql1 += "見積番号枝番, "
            Sql1 += "見積日, "
            Sql1 += "見積有効期限, "
            Sql1 += "得意先コード, "
            Sql1 += "得意先名, "
            Sql1 += "得意先担当者名, "
            Sql1 += "得意先担当者役職, "
            Sql1 += "得意先郵便番号, "
            Sql1 += "得意先住所, "
            Sql1 += "得意先電話番号, "
            Sql1 += "得意先ＦＡＸ, "
            Sql1 += "営業担当者, "
            Sql1 += "入力担当者, "
            Sql1 += "支払条件, "
            Sql1 += "備考, "
            Sql1 += "登録日, "
            Sql1 += "更新日, "
            Sql1 += "更新者, "
            Sql1 += "ＶＡＴ "
            Sql1 += "FROM "
            Sql1 += "public"
            Sql1 += "."
            Sql1 += "t01_mithd"
            Sql1 += " WHERE "
            Sql1 += "見積番号"
            Sql1 += " ILIKE "
            Sql1 += "'%"
            Sql1 += EditNo.ToString
            Sql1 += "%'"
            Sql1 += " AND "
            Sql1 += "見積番号枝番"
            Sql1 += " ILIKE "
            Sql1 += "'%"
            Sql1 += EditSuffix.ToString
            Sql1 += "%'"
            Dim reccnt As Integer = 0
            Dim ds1 = _db.selectDB(Sql1, RS, reccnt)

            Dim Sql2 As String = ""
            Sql2 += "SELECT "
            Sql2 += "見積番号枝番 "
            Sql2 += "FROM "
            Sql2 += "public"
            Sql2 += "."
            Sql2 += "t01_mithd"
            Sql2 += " WHERE "
            Sql2 += "見積番号"
            Sql2 += " ILIKE "
            Sql2 += "'%"
            Sql2 += EditNo.ToString
            Sql2 += "%'"

            Dim ds2 = _db.selectDB(Sql2, RS, reccnt)
            Dim SuffixMax As Integer = 0
            If Status Is "CLONE" Then
                Dim Sql As String = ""
                Sql += "SELECT "
                Sql += "会社コード, "
                Sql += "採番キー, "
                Sql += "最新値, "
                Sql += "最小値, "
                Sql += "最大値, "
                Sql += "接頭文字, "
                Sql += "連番桁数 "
                Sql += "FROM "
                Sql += "public"
                Sql += "."
                Sql += "m80_saiban"
                Sql += " WHERE "
                Sql += "採番キー"
                Sql += " ILIKE "
                Sql += "'%10%'"

                Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)
                Dim dtNow As DateTime = DateTime.Now
                ' 指定した書式で日付を文字列に変換する
                Dim QuoteDate As String = dtNow.ToString("MMdd")

                TxtQuoteNo.Text += ds.Tables(RS).Rows(0)(5)
                TxtQuoteNo.Text += QuoteDate
                CompanyCode = ds.Tables(RS).Rows(0)(0)
                KeyNo = ds.Tables(RS).Rows(0)(1)
                QuoteNo = ds.Tables(RS).Rows(0)(2)
                QuoteNoMin = ds.Tables(RS).Rows(0)(3)
                QuoteNoMax = ds.Tables(RS).Rows(0)(4)
                TxtQuoteNo.Text += QuoteNo.PadLeft(ds.Tables(RS).Rows(0)(6), "0")
            Else
                TxtQuoteNo.Text = ds1.Tables(RS).Rows(0)(1)
                For index As Integer = 0 To ds2.Tables(RS).Rows.Count - 1
                    If SuffixMax <= ds2.Tables(RS).Rows(index)(0) Then
                        SuffixMax = ds2.Tables(RS).Rows(index)(0)
                    End If
                Next
            End If

            CompanyCode = ds1.Tables(RS).Rows(0)(0)

            If Status IsNot "PRICE" Then
                TxtSuffixNo.Text = SuffixMax + 1
            Else
                TxtSuffixNo.Text = ds1.Tables(RS).Rows(0)(2)
            End If

            DtpQuote.Value = ds1.Tables(RS).Rows(0)(3)
            DtpExpiration.Value = ds1.Tables(RS).Rows(0)(4)
            TxtCustomerCode.Text = ds1.Tables(RS).Rows(0)(5)
            TxtCustomerName.Text = ds1.Tables(RS).Rows(0)(6)
            TxtPerson.Text = ds1.Tables(RS).Rows(0)(7)
            TxtPosition.Text = ds1.Tables(RS).Rows(0)(8)
            If ds1.Tables(RS).Rows(0)(9) IsNot DBNull.Value Then
                TxtPostalCode.Text = ds1.Tables(RS).Rows(0)(9)
            End If
            If ds1.Tables(RS).Rows(0)(10) IsNot DBNull.Value Then
                Dim Address As String = ds1.Tables(RS).Rows(0)(10)
                Dim delimiter As String = " "
                Dim parts As String() = Split(Address, delimiter, -1, CompareMethod.Text)
                TxtAddress1.Text = parts(0).ToString
                TxtAddress2.Text = parts(1).ToString
                TxtAddress3.Text = parts(2).ToString
            End If
            If ds1.Tables(RS).Rows(0)(11) IsNot DBNull.Value Then
                TxtTel.Text = ds1.Tables(RS).Rows(0)(11)
            End If
            If ds1.Tables(RS).Rows(0)(12) IsNot DBNull.Value Then
                TxtFax.Text = ds1.Tables(RS).Rows(0)(12)
            End If
            If ds1.Tables(RS).Rows(0)(13) IsNot DBNull.Value Then
                TxtSales.Text = ds1.Tables(RS).Rows(0)(13)
            End If
            If ds1.Tables(RS).Rows(0)(14) IsNot DBNull.Value Then
                TxtInput.Text = ds1.Tables(RS).Rows(0)(14)
            End If
            If ds1.Tables(RS).Rows(0)(15) IsNot DBNull.Value Then
                TxtPaymentTerms.Text = ds1.Tables(RS).Rows(0)(15)
            End If
            If ds1.Tables(RS).Rows(0)(16) IsNot DBNull.Value Then
                TxtRemarks.Text = ds1.Tables(RS).Rows(0)(16)
            End If
            If ds1.Tables(RS).Rows(0)("ＶＡＴ") IsNot DBNull.Value Then
                TxtVat.Text = ds1.Tables(RS).Rows(0)("ＶＡＴ")
            End If

            '見積明細情報
            Dim Sql3 As String = ""
            Sql3 += "SELECT "
            Sql3 += "仕入区分, "
            Sql3 += "メーカー, "
            Sql3 += "品名, "
            Sql3 += "型式, "
            Sql3 += "数量, "
            Sql3 += "単位, "
            Sql3 += "仕入先名称, "
            Sql3 += "仕入単価, "
            Sql3 += "間接費率, "
            Sql3 += "間接費, "
            Sql3 += "仕入金額, "
            Sql3 += "売単価, "
            Sql3 += "売上金額, "
            Sql3 += "粗利額, "
            Sql3 += "粗利率, "
            Sql3 += "リードタイム, "
            Sql3 += "備考, "
            Sql3 += "登録日 "
            Sql3 += "FROM "
            Sql3 += "public"
            Sql3 += "."
            Sql3 += "t02_mitdt"
            Sql3 += " WHERE "
            Sql3 += "見積番号"
            Sql3 += " ILIKE "
            Sql3 += "'%"
            Sql3 += EditNo.ToString
            Sql3 += "%'"
            Sql3 += " AND "
            Sql3 += "見積番号枝番"
            Sql3 += " ILIKE "
            Sql3 += "'%"
            Sql3 += EditSuffix.ToString
            Sql3 += "%'"

            Dim ds3 = _db.selectDB(Sql3, RS, reccnt)

            For index As Integer = 0 To ds3.Tables(RS).Rows.Count - 1
                DgvItemList.Rows.Add()
                Dim tmp As Integer = ds3.Tables(RS).Rows(index)(0)
                DgvItemList(1, index).Value = tmp
                DgvItemList.Rows(index).Cells(2).Value = ds3.Tables(RS).Rows(index)(1)
                DgvItemList.Rows(index).Cells(3).Value = ds3.Tables(RS).Rows(index)(2)
                DgvItemList.Rows(index).Cells(4).Value = ds3.Tables(RS).Rows(index)(3)
                DgvItemList.Rows(index).Cells(5).Value = ds3.Tables(RS).Rows(index)(4)
                DgvItemList.Rows(index).Cells(6).Value = ds3.Tables(RS).Rows(index)(5)
                DgvItemList.Rows(index).Cells(7).Value = ds3.Tables(RS).Rows(index)(6)
                DgvItemList.Rows(index).Cells(8).Value = ds3.Tables(RS).Rows(index)(7)
                DgvItemList.Rows(index).Cells(9).Value = ds3.Tables(RS).Rows(index)(8)
                DgvItemList.Rows(index).Cells(10).Value = ds3.Tables(RS).Rows(index)(9)
                DgvItemList.Rows(index).Cells(11).Value = ds3.Tables(RS).Rows(index)(10)
                DgvItemList.Rows(index).Cells(12).Value = ds3.Tables(RS).Rows(index)(11)
                DgvItemList.Rows(index).Cells(13).Value = ds3.Tables(RS).Rows(index)(12)
                DgvItemList.Rows(index).Cells(14).Value = ds3.Tables(RS).Rows(index)(13)
                DgvItemList.Rows(index).Cells(15).Value = ds3.Tables(RS).Rows(index)(14)
                DgvItemList.Rows(index).Cells(16).Value = ds3.Tables(RS).Rows(index)(15)
                DgvItemList.Rows(index).Cells(17).Value = ds3.Tables(RS).Rows(index)(16)
                DgvItemList.Rows(index).Cells(18).Value = "EDIT"
            Next

            '金額計算
            Dim Total As Integer = 0
            Dim PurchaseTotal As Integer = 0
            Dim GrossProfit As Decimal = 0

            For index As Integer = 0 To DgvItemList.Rows.Count - 1
                PurchaseTotal += DgvItemList.Rows(index).Cells(11).Value
                Total += DgvItemList.Rows(index).Cells(13).Value
            Next

            TxtPurchaseTotal.Text = PurchaseTotal
            TxtTotal.Text = Total
            TxtGrossProfit.Text = Total - PurchaseTotal

            '行番号の振り直し
            Dim i As Integer = DgvItemList.Rows.Count()
            Dim No As Integer = 1
            For c As Integer = 0 To i - 1
                DgvItemList.Rows(c).Cells(0).Value = No
                No += 1
            Next c
            TxtItemCount.Text = DgvItemList.Rows.Count()

        Else    '見積新規追加
            Dim dtNow As DateTime = DateTime.Now
            ' 指定した書式で日付を文字列に変換する
            Dim QuoteDate As String = dtNow.ToString("MMdd")

            TxtSuffixNo.Text = 1
            Dim Sql As String = ""
            Sql += "SELECT "
            Sql += "会社コード, "
            Sql += "採番キー, "
            Sql += "最新値, "
            Sql += "最小値, "
            Sql += "最大値, "
            Sql += "接頭文字, "
            Sql += "連番桁数 "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "m80_saiban"
            Sql += " WHERE "
            Sql += "採番キー"
            Sql += " ILIKE "
            Sql += "'%10%'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            TxtQuoteNo.Text += ds.Tables(RS).Rows(0)(5)
            TxtQuoteNo.Text += QuoteDate
            CompanyCode = ds.Tables(RS).Rows(0)(0)
            KeyNo = ds.Tables(RS).Rows(0)(1)
            QuoteNo = ds.Tables(RS).Rows(0)(2)
            QuoteNoMin = ds.Tables(RS).Rows(0)(3)
            QuoteNoMax = ds.Tables(RS).Rows(0)(4)
            TxtQuoteNo.Text += QuoteNo.PadLeft(ds.Tables(RS).Rows(0)(6), "0")

            TxtInput.Text = Input
        End If

        If Status Is "VIEW" Then
            LblMode.Text = "参照モード"
            DtpQuote.Enabled = False
            DtpExpiration.Enabled = False
            TxtCustomerCode.Enabled = False
            TxtCustomerName.Enabled = False
            TxtPostalCode.Enabled = False
            TxtAddress1.Enabled = False
            TxtAddress2.Enabled = False
            TxtAddress3.Enabled = False
            TxtTel.Enabled = False
            TxtFax.Enabled = False
            TxtPerson.Enabled = False
            TxtPosition.Enabled = False
            TxtSales.Enabled = False
            TxtPaymentTerms.Enabled = False
            TxtRemarks.Enabled = False
            DgvItemList.ReadOnly = True
            BtnRegistration.Visible = False
            BtnRowsAdd.Visible = False
            BtnRowsDel.Visible = False
            BtnUp.Visible = False
            BtnDown.Visible = False
            BtnClone.Visible = False
            BtnInsert.Visible = False
            Dim RequestFlg As Boolean = True
            For i As Integer = 0 To DgvItemList.Rows.Count() - 1
                If DgvItemList.Rows(i).Cells("仕入単価").Value = 0 Then
                Else
                    RequestFlg = False
                End If
                If DgvItemList.Rows(i).Cells("仕入区分").Value = 1 Then
                Else
                    RequestFlg = False
                End If
            Next
            If RequestFlg Then
                BtnQuoteRequest.Visible = True
                BtnQuoteRequest.Location = New Point(1004, 677)
            Else
                BtnQuote.Visible = True
                BtnQuote.Location = New Point(1004, 677)
            End If

        ElseIf Status Is "PRICE" Then
            LblMode.Text = "仕入単価入力モード"
            BtnRowsAdd.Visible = False
            BtnRowsDel.Visible = False
            BtnUp.Visible = False
            BtnDown.Visible = False
            BtnClone.Visible = False
            BtnInsert.Visible = False
            BtnQuote.Visible = False
        ElseIf Status Is "EDIT" Then
            LblMode.Text = "編集モード"
        ElseIf Status Is "ADD" Then
            LblMode.Text = "新規登録モード"
        End If


        LoadFlg = True

    End Sub

    '金額自動計算
    Private Sub CellValueChanged(ByVal sender As Object,
    ByVal e As DataGridViewCellEventArgs) _
    Handles DgvItemList.CellValueChanged
        If LoadFlg Then
            TxtPurchaseTotal.Clear()
            TxtTotal.Clear()
            TxtGrossProfit.Clear()

            Dim Total As Integer = 0
            Dim PurchaseTotal As Integer = 0
            Dim GrossProfit As Decimal = 0

            If e.RowIndex > -1 Then
                If DgvItemList.Rows(e.RowIndex).Cells(8).Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells(9).Value IsNot Nothing Then
                    DgvItemList.Rows(e.RowIndex).Cells(10).Value = DgvItemList.Rows(e.RowIndex).Cells(8).Value * DgvItemList.Rows(e.RowIndex).Cells(9).Value
                    If DgvItemList.Rows(e.RowIndex).Cells(5).Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells(8).Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells(10).Value IsNot Nothing Then
                        DgvItemList.Rows(e.RowIndex).Cells(11).Value = DgvItemList.Rows(e.RowIndex).Cells(5).Value * DgvItemList.Rows(e.RowIndex).Cells(8).Value + DgvItemList.Rows(e.RowIndex).Cells(10).Value
                    End If
                End If
                If DgvItemList.Rows(e.RowIndex).Cells(5).Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells(12).Value IsNot Nothing Then
                    DgvItemList.Rows(e.RowIndex).Cells(13).Value = DgvItemList.Rows(e.RowIndex).Cells(5).Value * DgvItemList.Rows(e.RowIndex).Cells(12).Value
                    If DgvItemList.Rows(e.RowIndex).Cells(11).Value IsNot Nothing Then
                        DgvItemList.Rows(e.RowIndex).Cells(14).Value = DgvItemList.Rows(e.RowIndex).Cells(13).Value - DgvItemList.Rows(e.RowIndex).Cells(11).Value
                        DgvItemList.Rows(e.RowIndex).Cells(15).Value = Format(DgvItemList.Rows(e.RowIndex).Cells(14).Value / DgvItemList.Rows(e.RowIndex).Cells(13).Value * 100, "0.000")
                    End If
                End If
            End If

            For index As Integer = 0 To DgvItemList.Rows.Count - 1
                PurchaseTotal += DgvItemList.Rows(index).Cells(11).Value
                Total += DgvItemList.Rows(index).Cells(13).Value
            Next
            TxtPurchaseTotal.Text = PurchaseTotal
            TxtTotal.Text = Total
            TxtGrossProfit.Text = Total - PurchaseTotal
        End If
    End Sub

    '任意の場所に行を挿入
    Private Sub BtnInsert_Click(sender As Object, e As EventArgs) Handles BtnInsert.Click
        If DgvItemList.Rows.Count > 0 Then
            Dim RowIdx As Integer = DgvItemList.CurrentCell.RowIndex
            '行を挿入
            DgvItemList.Rows.Insert(RowIdx + 1)
            DgvItemList.Rows(RowIdx + 1).Cells(18).Value = "ADD"
            '最終行のインデックスを取得
            Dim index As Integer = DgvItemList.Rows.Count()
            '行番号の振り直し
            Dim No As Integer = 1
            For c As Integer = 0 To index - 1
                DgvItemList.Rows(c).Cells(0).Value = No
                No += 1
            Next c
            TxtItemCount.Text = DgvItemList.Rows.Count()
        Else
            DgvItemList.Rows.Add()
            TxtItemCount.Text = DgvItemList.Rows.Count()
            DgvItemList.Rows(DgvItemList.Rows.Count() - 1).Cells(18).Value = "ADD"
            '行番号の振り直し
            Dim index As Integer = DgvItemList.Rows.Count()
            Dim No As Integer = 1
            For c As Integer = 0 To index - 1
                DgvItemList.Rows(c).Cells(0).Value = No
                No += 1
            Next c
            TxtItemCount.Text = DgvItemList.Rows.Count()
        End If
    End Sub

    '行追加（DGVの最終行に追加）
    Private Sub BtnRowsAdd_Click(sender As Object, e As EventArgs) Handles BtnRowsAdd.Click
        DgvItemList.Rows.Add()
        DgvItemList.Rows(DgvItemList.Rows.Count() - 1).Cells(18).Value = "ADD"
        '行番号の振り直し
        Dim index As Integer = DgvItemList.Rows.Count()
        Dim No As Integer = 1
        For c As Integer = 0 To index - 1
            DgvItemList.Rows(c).Cells(0).Value = No
            No += 1
        Next c
        TxtItemCount.Text = DgvItemList.Rows.Count()
    End Sub

    '選択行の削除（削除時に金額の再計算、Noの再採番）
    Private Sub BtnRowsDel_Click(sender As Object, e As EventArgs) Handles BtnRowsDel.Click

        For Each r As DataGridViewCell In DgvItemList.SelectedCells
            DgvItemList.Rows.RemoveAt(r.RowIndex)
        Next r

        TxtPurchaseTotal.Clear()
        TxtTotal.Clear()
        TxtGrossProfit.Clear()

        Dim Total As Integer = 0
        Dim PurchaseTotal As Integer = 0
        Dim GrossProfit As Decimal = 0

        For c As Integer = 0 To DgvItemList.Rows.Count - 1
            PurchaseTotal += DgvItemList.Rows(c).Cells(11).Value
            Total += DgvItemList.Rows(c).Cells(13).Value
        Next
        TxtPurchaseTotal.Text = PurchaseTotal
        TxtTotal.Text = Total
        TxtGrossProfit.Text = Total - PurchaseTotal

        '行番号の振り直し
        Dim index As Integer = DgvItemList.Rows.Count()
        Dim No As Integer = 1
        For c As Integer = 0 To index - 1
            DgvItemList.Rows(c).Cells(0).Value = No
            No += 1
        Next c
        TxtItemCount.Text = DgvItemList.Rows.Count()
    End Sub

    '行の複写（選択行の直下に複写）
    Private Sub BtnClone_Click(sender As Object, e As EventArgs) Handles BtnClone.Click
        Try
            'メニュー選択処理
            Dim RowIdx As Integer
            Dim Item(17) As String

            '一覧選択行インデックスの取得

            RowIdx = DgvItemList.CurrentCell.RowIndex


            '選択行の値を格納
            For c As Integer = 0 To 17
                Item(c) = DgvItemList.Rows(RowIdx).Cells(c).Value
            Next c

            '行を挿入
            DgvItemList.Rows.Insert(RowIdx + 1)
            DgvItemList.Rows(RowIdx + 1).Cells(18).Value = "ADD"
            '追加した行に複製元の値を格納
            For c As Integer = 0 To 17
                If c = 1 Then
                    If Item(c) IsNot Nothing Then
                        Dim tmp As Integer = Item(c)
                        DgvItemList(1, RowIdx + 1).Value = tmp
                    End If
                Else
                    DgvItemList.Rows(RowIdx + 1).Cells(c).Value = Item(c)
                End If

            Next c

            '最終行のインデックスを取得
            Dim index As Integer = DgvItemList.Rows.Count()
            '行番号の振り直し
            Dim No As Integer = 1
            For c As Integer = 0 To index - 1
                DgvItemList.Rows(c).Cells(0).Value = No
                No += 1
            Next c
            TxtItemCount.Text = DgvItemList.Rows.Count()
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '得意先コードをダブルクリック時得意先マスタから得意先を取得
    Private Sub TxtCustomerCode_DoubleClick(sender As Object, e As EventArgs) Handles TxtCustomerCode.DoubleClick
        Dim openForm As Form = Nothing
        openForm = New CustomerSearch(_msgHd, _db, _langHd, Me)   '処理選択
        openForm.Show(Me)
        Me.Enabled = False
    End Sub

    Private Sub TxtSales_DoubleClick(sender As Object, e As EventArgs) Handles TxtSales.DoubleClick
        Dim openForm As Form = Nothing
        openForm = New SalesSearch(_msgHd, _db, _langHd, Me)   '処理選択
        openForm.Show(Me)
        Me.Enabled = False
    End Sub

    'Dgv内での検索
    Private Sub DgvItemList_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) _
     Handles DgvItemList.CellDoubleClick

        Dim ColIdx As Integer
        ColIdx = DgvItemList.CurrentCell.ColumnIndex
        Dim RowIdx As Integer
        RowIdx = DgvItemList.CurrentCell.RowIndex

        Dim Maker As String = DgvItemList.Rows(RowIdx).Cells(2).Value
        Dim Item As String = DgvItemList.Rows(RowIdx).Cells(3).Value
        Dim Model As String = DgvItemList.Rows(RowIdx).Cells(4).Value

        If ColIdx = 2 Then                  'メーカー検索
            Dim openForm As Form = Nothing
            openForm = New MakerSearch(_msgHd, _db, Me, RowIdx, ColIdx, Maker, Item, Model)   '処理選択
            openForm.Show(Me)
            Me.Enabled = False
        End If

        If ColIdx = 3 Then              '品名検索
            If Maker IsNot Nothing Then
                Dim openForm As Form = Nothing
                openForm = New MakerSearch(_msgHd, _db, Me, RowIdx, ColIdx, Maker, Item, Model)   '処理選択
                openForm.Show(Me)
                Me.Enabled = False
            Else
                MessageBox.Show("メーカーを入力してください。",
                "エラー",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
            End If
        End If

        If ColIdx = 4 Then
            If Maker IsNot Nothing And Item IsNot Nothing Then
                Dim openForm As Form = Nothing
                openForm = New MakerSearch(_msgHd, _db, Me, RowIdx, ColIdx, Maker, Item, Model)
                openForm.Show(Me)
                Me.Enabled = False
            Else
                MessageBox.Show("メーカー、品名を入力してください。",
                "エラー",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
            End If
        End If

        If ColIdx = 7 Then
            Dim openForm As Form = Nothing
            openForm = New SupplierSearch(_msgHd, _db, _langHd, RowIdx, Me)
            openForm.Show(Me)
            Me.Enabled = False
        End If
    End Sub

    Private Sub BtnUp_Click(sender As Object, e As EventArgs) Handles BtnUp.Click
        If DgvItemList.CurrentCell.RowIndex > 0 Then
            DgvItemList.CurrentCell = DgvItemList(DgvItemList.CurrentCell.ColumnIndex, DgvItemList.CurrentCell.RowIndex - 1)
        End If
    End Sub

    Private Sub BtnDown_Click(sender As Object, e As EventArgs) Handles BtnDown.Click
        If DgvItemList.CurrentCell.RowIndex + 1 < DgvItemList.Rows.Count Then
            DgvItemList.CurrentCell = DgvItemList(DgvItemList.CurrentCell.ColumnIndex, DgvItemList.CurrentCell.RowIndex + 1)
        End If
    End Sub

    '前の画面に戻る
    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Show()
        _parentForm.Enabled = True
        Me.Dispose()
    End Sub

    Private Sub BtnRegistration_Click(sender As Object, e As EventArgs) Handles BtnRegistration.Click
        Dim dtToday As DateTime = DateTime.Now
        If Status Is "PRICE" Then
            Try
                Dim Sql1 As String = ""
                Sql1 = ""
                Sql1 += "UPDATE "
                Sql1 += "Public."
                Sql1 += "t01_mithd "
                Sql1 += "SET "

                Sql1 += "得意先コード"
                Sql1 += " = '"
                Sql1 += TxtCustomerCode.Text
                Sql1 += "', "
                Sql1 += "得意先名"
                Sql1 += " = '"
                Sql1 += TxtCustomerName.Text
                Sql1 += "', "
                Sql1 += "得意先郵便番号"
                Sql1 += " = '"
                Sql1 += TxtPostalCode.Text
                Sql1 += "', "
                Sql1 += "得意先住所"
                Sql1 += " = '"
                Sql1 += TxtAddress1.Text
                Sql1 += " "
                Sql1 += TxtAddress2.Text
                Sql1 += " "
                Sql1 += TxtAddress3.Text
                Sql1 += "', "
                Sql1 += "得意先電話番号"
                Sql1 += " = '"
                Sql1 += TxtTel.Text
                Sql1 += "', "
                Sql1 += "得意先ＦＡＸ"
                Sql1 += " = '"
                Sql1 += TxtFax.Text
                Sql1 += "', "
                Sql1 += "得意先担当者役職"
                Sql1 += " = '"
                Sql1 += TxtPosition.Text
                Sql1 += "', "
                Sql1 += "得意先担当者名"
                Sql1 += " = '"
                Sql1 += TxtPerson.Text
                Sql1 += "', "
                Sql1 += "見積日"
                Sql1 += " = '"
                Sql1 += DtpQuote.Text
                Sql1 += "', "
                Sql1 += "見積有効期限"
                Sql1 += " = '"
                Sql1 += DtpExpiration.Text
                Sql1 += "', "
                Sql1 += "支払条件"
                Sql1 += " = '"
                Sql1 += TxtPaymentTerms.Text
                Sql1 += "', "
                Sql1 += "見積金額"
                Sql1 += " = '"
                Sql1 += TxtTotal.Text
                Sql1 += "', "
                Sql1 += "仕入金額"
                Sql1 += " = '"
                Sql1 += TxtPurchaseTotal.Text
                Sql1 += "', "
                Sql1 += "粗利額"
                Sql1 += " = '"
                Sql1 += TxtGrossProfit.Text
                Sql1 += "', "
                Sql1 += "営業担当者"
                Sql1 += " = '"
                Sql1 += TxtSales.Text
                Sql1 += "', "
                Sql1 += "入力担当者"
                Sql1 += " = '"
                Sql1 += TxtInput.Text
                Sql1 += "', "
                Sql1 += "備考"
                Sql1 += " = '"
                Sql1 += TxtRemarks.Text
                Sql1 += "', "
                Sql1 += "登録日"
                Sql1 += " = '"
                Sql1 += DtpRegistration.Text
                Sql1 += "', "
                Sql1 += "更新日"
                Sql1 += " = '"
                Sql1 += dtToday
                Sql1 += "', "
                Sql1 += "更新者"
                Sql1 += " = '"
                Sql1 += Input
                Sql1 += "' "
                Sql1 += "WHERE"
                Sql1 += " 会社コード"
                Sql1 += "='"
                Sql1 += CompanyCode
                Sql1 += "'"
                Sql1 += " AND"
                Sql1 += " 見積番号"
                Sql1 += "='"
                Sql1 += TxtQuoteNo.Text
                Sql1 += "' "
                Sql1 += " AND"
                Sql1 += " 見積番号枝番"
                Sql1 += "='"
                Sql1 += TxtSuffixNo.Text
                Sql1 += "' "
                Sql1 += "RETURNING 会社コード"
                Sql1 += ", "
                Sql1 += "見積番号"
                Sql1 += ", "
                Sql1 += "見積番号枝番"
                Sql1 += ", "
                Sql1 += "得意先コード"
                Sql1 += ", "
                Sql1 += "得意先名"
                Sql1 += ", "
                Sql1 += "得意先郵便番号"
                Sql1 += ", "
                Sql1 += "得意先住所"
                Sql1 += ", "
                Sql1 += "得意先電話番号"
                Sql1 += ", "
                Sql1 += "得意先ＦＡＸ"
                Sql1 += ", "
                Sql1 += "得意先担当者役職"
                Sql1 += ", "
                Sql1 += "得意先担当者名"
                Sql1 += ", "
                Sql1 += "見積日"
                Sql1 += ", "
                Sql1 += "見積有効期限"
                Sql1 += ", "
                Sql1 += "支払条件"
                Sql1 += ", "
                Sql1 += "見積金額"
                Sql1 += ", "
                Sql1 += "仕入金額"
                Sql1 += ", "
                Sql1 += "営業担当者"
                Sql1 += ", "
                Sql1 += "入力担当者"
                Sql1 += ", "
                Sql1 += "備考"
                Sql1 += ", "
                Sql1 += "登録日"
                Sql1 += ", "
                Sql1 += "更新日"
                Sql1 += ", "
                Sql1 += "更新者"
                _db.executeDB(Sql1)

                Dim Sql2 As String = ""
                For index As Integer = 0 To DgvItemList.Rows.Count - 1
                    Sql2 = ""
                    Sql2 += "UPDATE "
                    Sql2 += "Public."
                    Sql2 += "t02_mitdt "
                    Sql2 += "SET "

                    Sql2 += "仕入区分"
                    Sql2 += " = '"
                    Sql2 += DgvItemList.Rows(index).Cells("仕入区分").Value.ToString
                    Sql2 += "', "
                    Sql2 += "メーカー"
                    Sql2 += " = '"
                    Sql2 += DgvItemList.Rows(index).Cells("メーカー").Value.ToString
                    Sql2 += "', "
                    Sql2 += "品名"
                    Sql2 += " = '"
                    Sql2 += DgvItemList.Rows(index).Cells("品名").Value.ToString
                    Sql2 += "', "
                    Sql2 += "型式"
                    Sql2 += " = '"
                    Sql2 += DgvItemList.Rows(index).Cells("型式").Value.ToString
                    Sql2 += "', "

                    If DgvItemList.Rows(index).Cells(5).Value IsNot Nothing Then
                        Sql2 += "数量"
                        Sql2 += " = '"
                        Sql2 += DgvItemList.Rows(index).Cells("数量").Value.ToString
                        Sql2 += "', "
                    Else
                        Sql2 += "数量"
                        Sql2 += " = '"
                        Sql2 += "0"
                        Sql2 += "', "
                    End If
                    Sql2 += "単位"
                    Sql2 += " = '"
                    Sql2 += DgvItemList.Rows(index).Cells("単位").Value.ToString
                    Sql2 += "', "
                    Sql2 += "仕入先名称"
                    Sql2 += " = '"
                    Sql2 += DgvItemList.Rows(index).Cells("仕入先").Value.ToString
                    Sql2 += "', "
                    If DgvItemList.Rows(index).Cells(8).Value IsNot Nothing Then
                        Sql2 += "仕入単価"
                        Sql2 += " = '"
                        Sql2 += DgvItemList.Rows(index).Cells("仕入単価").Value.ToString
                        Sql2 += "', "
                    Else
                        Sql2 += "仕入単価"
                        Sql2 += " = '"
                        Sql2 += "0"
                        Sql2 += "', "
                    End If
                    If DgvItemList.Rows(index).Cells(9).Value IsNot Nothing Then
                        Sql2 += "間接費率"
                        Sql2 += " = '"
                        Sql2 += DgvItemList.Rows(index).Cells("間接費率").Value.ToString
                        Sql2 += "', "
                    Else
                        Sql2 += "間接費率"
                        Sql2 += " = '"
                        Sql2 += "0"
                        Sql2 += "', "
                    End If
                    If DgvItemList.Rows(index).Cells(10).Value IsNot Nothing Then
                        Sql2 += "間接費"
                        Sql2 += " = '"
                        Sql2 += DgvItemList.Rows(index).Cells("間接費").Value.ToString
                        Sql2 += "', "
                    Else
                        Sql2 += "間接費"
                        Sql2 += " = '"
                        Sql2 += "0"
                        Sql2 += "', "
                    End If
                    If DgvItemList.Rows(index).Cells(11).Value IsNot Nothing Then
                        Sql2 += "仕入金額"
                        Sql2 += " = '"
                        Sql2 += DgvItemList.Rows(index).Cells("仕入金額").Value.ToString
                        Sql2 += "', "
                    Else
                        Sql2 += "仕入金額"
                        Sql2 += " = '"
                        Sql2 += "0"
                        Sql2 += "', "
                    End If
                    If DgvItemList.Rows(index).Cells(12).Value IsNot Nothing Then
                        Sql2 += "売単価"
                        Sql2 += " = '"
                        Sql2 += DgvItemList.Rows(index).Cells("売単価").Value.ToString
                        Sql2 += "', "
                    Else
                        Sql2 += "売単価"
                        Sql2 += " = '"
                        Sql2 += "0"
                        Sql2 += "', "
                    End If
                    If DgvItemList.Rows(index).Cells(13).Value IsNot Nothing Then
                        Sql2 += "売上金額"
                        Sql2 += " = '"
                        Sql2 += DgvItemList.Rows(index).Cells("売上金額").Value.ToString
                        Sql2 += "', "
                    Else
                        Sql2 += "売上金額"
                        Sql2 += " = '"
                        Sql2 += "0"
                        Sql2 += "', "
                    End If
                    If DgvItemList.Rows(index).Cells(14).Value IsNot Nothing Then
                        Sql2 += "粗利額"
                        Sql2 += " = '"
                        Sql2 += DgvItemList.Rows(index).Cells("粗利額").Value.ToString
                        Sql2 += "', "
                    Else
                        Sql2 += "粗利額"
                        Sql2 += " = '"
                        Sql2 += "0"
                        Sql2 += "', "
                    End If
                    If DgvItemList.Rows(index).Cells(15).Value IsNot Nothing Then
                        Sql2 += "粗利率"
                        Sql2 += " = '"
                        Sql2 += DgvItemList.Rows(index).Cells("粗利率").Value.ToString
                        Sql2 += "', "
                    Else
                        Sql2 += "粗利率"
                        Sql2 += " = '"
                        Sql2 += "0"
                        Sql2 += "', "
                    End If
                    If DgvItemList.Rows(index).Cells(16).Value IsNot Nothing Then
                        Sql2 += "リードタイム"
                        Sql2 += " = '"
                        Sql2 += DgvItemList.Rows(index).Cells("リードタイム").Value.ToString
                        Sql2 += "', "
                    Else
                        Sql2 += "リードタイム"
                        Sql2 += " = '"
                        Sql2 += "0"
                        Sql2 += "', "
                    End If

                    Sql2 += "備考"
                    Sql2 += " = '"
                    Sql2 += DgvItemList.Rows(index).Cells("備考").Value.ToString
                    Sql2 += "', "
                    Sql2 += "更新者"
                    Sql2 += " = '"
                    Sql2 += Input
                    Sql2 += "', "
                    Sql2 += "登録日"
                    Sql2 += " = '"
                    Sql2 += DtpRegistration.Text
                    Sql2 += "' "

                    Sql2 += "WHERE"
                    Sql2 += " 会社コード"
                    Sql2 += "='"
                    Sql2 += CompanyCode
                    Sql2 += "'"
                    Sql2 += " AND"
                    Sql2 += " 見積番号"
                    Sql2 += "='"
                    Sql2 += TxtQuoteNo.Text
                    Sql2 += "' "
                    Sql2 += " AND"
                    Sql2 += " 見積番号枝番"
                    Sql2 += "='"
                    Sql2 += TxtSuffixNo.Text
                    Sql2 += "' "
                    Sql2 += " AND"
                    Sql2 += " 行番号"
                    Sql2 += "='"
                    Sql2 += DgvItemList.Rows(index).Cells(0).Value.ToString
                    Sql2 += "' "

                    Sql2 += "RETURNING 会社コード"
                    Sql2 += ", "
                    Sql2 += "見積番号"
                    Sql2 += ", "
                    Sql2 += "見積番号枝番"
                    Sql2 += ", "
                    Sql2 += "行番号"
                    Sql2 += ", "
                    Sql2 += "仕入区分"
                    Sql2 += ", "
                    Sql2 += "メーカー"
                    Sql2 += ", "
                    Sql2 += "品名"
                    Sql2 += ", "
                    Sql2 += "型式"
                    Sql2 += ", "
                    Sql2 += "数量"
                    Sql2 += ", "
                    Sql2 += "単位"
                    Sql2 += ", "
                    Sql2 += "仕入先名称"
                    Sql2 += ", "
                    Sql2 += "仕入単価"
                    Sql2 += ", "
                    Sql2 += "間接費率"
                    Sql2 += ", "
                    Sql2 += "間接費"
                    Sql2 += ", "
                    Sql2 += "仕入金額"
                    Sql2 += ", "
                    Sql2 += "売単価"
                    Sql2 += ", "
                    Sql2 += "売上金額"
                    Sql2 += ", "
                    Sql2 += "粗利額"
                    Sql2 += ", "
                    Sql2 += "粗利率"
                    Sql2 += ", "
                    Sql2 += "リードタイム"
                    Sql2 += ", "
                    Sql2 += "備考"
                    Sql2 += ", "
                    Sql2 += "更新者"
                    Sql2 += ", "
                    Sql2 += "登録日"

                    _db.executeDB(Sql2)
                Next
            Catch ue As UsrDefException
                ue.dspMsg()
                Throw ue
            Catch ex As Exception
                'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
            End Try
        Else
            Try
                Dim Sql1 As String = ""
                Sql1 = ""
                Sql1 += "INSERT INTO "
                Sql1 += "Public."
                Sql1 += "t01_mithd("
                Sql1 += "会社コード, 見積番号, 見積番号枝番, 得意先コード, 得意先名, 得意先郵便番号, 得意先住所, 得意先電話番号, 得意先ＦＡＸ, 得意先担当者役職, 得意先担当者名, 見積日, 見積有効期限, 支払条件, 見積金額, 仕入金額, 粗利額, 営業担当者, 入力担当者, 備考, ＶＡＴ, 取消区分, 登録日, 更新日, 更新者)"
                Sql1 += " VALUES('"
                Sql1 += CompanyCode
                Sql1 += "', '"
                Sql1 += TxtQuoteNo.Text
                Sql1 += "', '"
                Sql1 += TxtSuffixNo.Text
                Sql1 += "', '"
                Sql1 += TxtCustomerCode.Text
                Sql1 += "', '"
                Sql1 += TxtCustomerName.Text
                Sql1 += "', '"
                Sql1 += TxtPostalCode.Text
                Sql1 += "', '"
                Sql1 += TxtAddress1.Text
                Sql1 += " "
                Sql1 += TxtAddress2.Text
                Sql1 += " "
                Sql1 += TxtAddress3.Text
                Sql1 += "', '"
                Sql1 += TxtTel.Text
                Sql1 += "', '"
                Sql1 += TxtFax.Text
                Sql1 += "', '"
                Sql1 += TxtPosition.Text
                Sql1 += "', '"
                Sql1 += TxtPerson.Text
                Sql1 += "', '"
                Sql1 += DtpQuote.Text
                Sql1 += "', '"
                Sql1 += DtpExpiration.Text
                Sql1 += "', '"
                Sql1 += TxtPaymentTerms.Text
                Sql1 += "', '"
                Sql1 += TxtTotal.Text
                Sql1 += "', '"
                Sql1 += TxtPurchaseTotal.Text
                Sql1 += "', '"
                Sql1 += TxtGrossProfit.Text
                Sql1 += "', '"
                Sql1 += TxtSales.Text
                Sql1 += "', '"
                Sql1 += TxtInput.Text
                Sql1 += "', '"
                Sql1 += TxtRemarks.Text
                Sql1 += "', '"
                If TxtVat.Text = Nothing Then
                    Sql1 += "0"
                Else
                    Sql1 += TxtVat.Text
                End If
                Sql1 += "', '"
                Sql1 += "0"
                Sql1 += "', '"
                Sql1 += DtpRegistration.Text
                Sql1 += "', '"
                Sql1 += dtToday
                Sql1 += "', '"
                Sql1 += Input
                Sql1 += " ')"
                Sql1 += "RETURNING 会社コード"
                Sql1 += ", "
                Sql1 += "見積番号"
                Sql1 += ", "
                Sql1 += "見積番号枝番"
                Sql1 += ", "
                Sql1 += "得意先コード"
                Sql1 += ", "
                Sql1 += "得意先名"
                Sql1 += ", "
                Sql1 += "得意先郵便番号"
                Sql1 += ", "
                Sql1 += "得意先住所"
                Sql1 += ", "
                Sql1 += "得意先電話番号"
                Sql1 += ", "
                Sql1 += "得意先ＦＡＸ"
                Sql1 += ", "
                Sql1 += "得意先担当者役職"
                Sql1 += ", "
                Sql1 += "得意先担当者名"
                Sql1 += ", "
                Sql1 += "見積日"
                Sql1 += ", "
                Sql1 += "見積有効期限"
                Sql1 += ", "
                Sql1 += "支払条件"
                Sql1 += ", "
                Sql1 += "見積金額"
                Sql1 += ", "
                Sql1 += "仕入金額"
                Sql1 += ", "
                Sql1 += "粗利額"
                Sql1 += ", "
                Sql1 += "営業担当者"
                Sql1 += ", "
                Sql1 += "入力担当者"
                Sql1 += ", "
                Sql1 += "備考"
                Sql1 += ", "
                Sql1 += "ＶＡＴ"
                Sql1 += ", "
                Sql1 += "取消区分"
                Sql1 += ", "
                Sql1 += "登録日"
                Sql1 += ", "
                Sql1 += "更新日"
                Sql1 += ", "
                Sql1 += "更新者"

                _db.executeDB(Sql1)

                Dim Sql2 As String = ""
                For index As Integer = 0 To DgvItemList.Rows.Count - 1
                    Sql2 = ""
                    Sql2 += "INSERT INTO "
                    Sql2 += "Public."
                    Sql2 += "t02_mitdt("
                    Sql2 += "会社コード, 見積番号, 見積番号枝番, 行番号, 仕入区分, メーカー, 品名, 型式, 数量, 単位, 仕入先名称, 仕入単価, 間接費率, 間接費, 仕入金額, 売単価, 売上金額, 粗利額, 粗利率, リードタイム, 備考, 更新者, 登録日)"
                    Sql2 += " VALUES('"
                    Sql2 += CompanyCode
                    Sql2 += "', '"
                    Sql2 += TxtQuoteNo.Text
                    Sql2 += "', '"
                    Sql2 += TxtSuffixNo.Text
                    Sql2 += "', '"


                    Dim ary As Integer() = New Integer() {0, 5, 8, 9, 10, 11, 12, 13, 14, 15, 16}
                    For i As Integer = 0 To 17
                        Dim res As Integer = Array.IndexOf(ary, i)
                        If DgvItemList.Rows(index).Cells(i).Value IsNot Nothing Then
                            Sql2 += DgvItemList.Rows(index).Cells(i).Value.ToString
                            Sql2 += "', '"
                        Else
                            If res = -1 Then
                                Sql2 += "', '"
                            Else
                                Sql2 += "0"
                                Sql2 += "', '"
                            End If
                        End If
                    Next
                    Sql2 += Input
                    Sql2 += "', '"
                    Sql2 += DtpRegistration.Text
                    Sql2 += " ')"
                    Sql2 += "RETURNING 会社コード"
                    Sql2 += ", "
                    Sql2 += "見積番号"
                    Sql2 += ", "
                    Sql2 += "見積番号枝番"
                    Sql2 += ", "
                    Sql2 += "行番号"
                    Sql2 += ", "
                    Sql2 += "仕入区分"
                    Sql2 += ", "
                    Sql2 += "メーカー"
                    Sql2 += ", "
                    Sql2 += "品名"
                    Sql2 += ", "
                    Sql2 += "型式"
                    Sql2 += ", "
                    Sql2 += "数量"
                    Sql2 += ", "
                    Sql2 += "単位"
                    Sql2 += ", "
                    Sql2 += "仕入先名称"
                    Sql2 += ", "
                    Sql2 += "仕入単価"
                    Sql2 += ", "
                    Sql2 += "間接費率"
                    Sql2 += ", "
                    Sql2 += "間接費"
                    Sql2 += ", "
                    Sql2 += "仕入金額"
                    Sql2 += ", "
                    Sql2 += "売単価"
                    Sql2 += ", "
                    Sql2 += "売上金額"
                    Sql2 += ", "
                    Sql2 += "粗利額"
                    Sql2 += ", "
                    Sql2 += "粗利率"
                    Sql2 += ", "
                    Sql2 += "リードタイム"
                    Sql2 += ", "
                    Sql2 += "備考"
                    Sql2 += ", "
                    Sql2 += "更新者"
                    Sql2 += ", "
                    Sql2 += "登録日"

                    _db.executeDB(Sql2)
                Next
                If Status = "ADD" Or Status = "CLONE" Then
                    If QuoteNo = QuoteNoMax Then
                        QuoteNo = QuoteNoMin
                    Else
                        QuoteNo = QuoteNo + 1
                    End If
                    Dim Sql3 As String = ""

                    Sql3 = ""
                    Sql3 += "UPDATE "
                    Sql3 += "Public."
                    Sql3 += "m80_saiban "
                    Sql3 += "SET "
                    Sql3 += " 最新値"
                    Sql3 += " = '"
                    Sql3 += QuoteNo.ToString
                    Sql3 += "', "
                    Sql3 += "更新者"
                    Sql3 += " = '"
                    Sql3 += Input
                    Sql3 += "', "
                    Sql3 += "更新日"
                    Sql3 += " = '"
                    Sql3 += dtToday
                    Sql3 += "' "
                    Sql3 += "WHERE"
                    Sql3 += " 会社コード"
                    Sql3 += "='"
                    Sql3 += CompanyCode.ToString
                    Sql3 += "'"
                    Sql3 += " AND"
                    Sql3 += " 採番キー"
                    Sql3 += "='"
                    Sql3 += KeyNo.ToString
                    Sql3 += "' "
                    Sql3 += "RETURNING 会社コード"
                    Sql3 += ", "
                    Sql3 += "採番キー"
                    Sql3 += ", "
                    Sql3 += "最新値"
                    Sql3 += ", "
                    Sql3 += "最小値"
                    Sql3 += ", "
                    Sql3 += "最大値"
                    Sql3 += ", "
                    Sql3 += "接頭文字"
                    Sql3 += ", "
                    Sql3 += "連番桁数"
                    Sql3 += ", "
                    Sql3 += "更新者"
                    Sql3 += ", "
                    Sql3 += "更新日"

                    _db.executeDB(Sql3)
                End If
            Catch ue As UsrDefException
                ue.dspMsg()
                Throw ue
            Catch ex As Exception
                'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
            End Try
        End If
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()

    End Sub

    Private Sub BtnQuote_Click(sender As Object, e As EventArgs) Handles BtnQuote.Click

        '見積基本情報
        Dim Sql1 As String = ""
        Sql1 += "SELECT "
        Sql1 += "会社コード, "
        Sql1 += "見積番号, "
        Sql1 += "見積番号枝番, "
        Sql1 += "見積日, "
        Sql1 += "見積有効期限, "
        Sql1 += "得意先コード, "
        Sql1 += "得意先名, "
        Sql1 += "得意先担当者名, "
        Sql1 += "得意先担当者役職, "
        Sql1 += "得意先郵便番号, "
        Sql1 += "得意先住所, "
        Sql1 += "得意先電話番号, "
        Sql1 += "得意先ＦＡＸ, "
        Sql1 += "営業担当者, "
        Sql1 += "入力担当者, "
        Sql1 += "支払条件, "
        Sql1 += "備考, "
        Sql1 += "登録日, "
        Sql1 += "更新日, "
        Sql1 += "更新者 "
        Sql1 += "FROM "
        Sql1 += "public"
        Sql1 += "."
        Sql1 += "t01_mithd"
        Sql1 += " WHERE "
        Sql1 += "見積番号"
        Sql1 += " ILIKE "
        Sql1 += "'%"
        Sql1 += EditNo.ToString
        Sql1 += "%'"
        Sql1 += " AND "
        Sql1 += "見積番号枝番"
        Sql1 += " ILIKE "
        Sql1 += "'%"
        Sql1 += EditSuffix.ToString
        Sql1 += "%'"
        Dim reccnt As Integer = 0
        Dim ds1 = _db.selectDB(Sql1, RS, reccnt)

        Dim Sql2 As String = ""
        Sql2 += "SELECT "
        Sql2 += "見積番号枝番 "
        Sql2 += "FROM "
        Sql2 += "public"
        Sql2 += "."
        Sql2 += "t01_mithd"
        Sql2 += " WHERE "
        Sql2 += "見積番号"
        Sql2 += " ILIKE "
        Sql2 += "'%"
        Sql2 += EditNo.ToString
        Sql2 += "%'"

        Dim ds2 = _db.selectDB(Sql2, RS, reccnt)
        Dim SuffixMax As Integer = 0


        CompanyCode = ds1.Tables(RS).Rows(0)(0)

        Dim CmnData = ds1.Tables(RS).Rows(0)


        '見積明細情報
        Dim Sql3 As String = ""
        Sql3 += "SELECT "
        Sql3 += "仕入区分, "
        Sql3 += "メーカー, "
        Sql3 += "品名, "
        Sql3 += "型式, "
        Sql3 += "数量, "
        Sql3 += "単位, "
        Sql3 += "仕入先名称, "
        Sql3 += "仕入単価, "
        Sql3 += "間接費率, "
        Sql3 += "間接費, "
        Sql3 += "仕入金額, "
        Sql3 += "売単価, "
        Sql3 += "売上金額, "
        Sql3 += "粗利額, "
        Sql3 += "粗利率, "
        Sql3 += "リードタイム, "
        Sql3 += "備考, "
        Sql3 += "登録日 "
        Sql3 += "FROM "
        Sql3 += "public"
        Sql3 += "."
        Sql3 += "t02_mitdt"
        Sql3 += " WHERE "
        Sql3 += "見積番号"
        Sql3 += " ILIKE "
        Sql3 += "'%"
        Sql3 += EditNo.ToString
        Sql3 += "%'"
        Sql3 += " AND "
        Sql3 += "見積番号枝番"
        Sql3 += " ILIKE "
        Sql3 += "'%"
        Sql3 += EditSuffix.ToString
        Sql3 += "%'"

        Dim ds3 = _db.selectDB(Sql3, RS, reccnt)

        Dim Sql4 As String = ""
        Sql4 += "SELECT "
        Sql4 += "会社コード, "
        Sql4 += "会社名, "
        Sql4 += "会社略称, "
        Sql4 += "郵便番号, "
        Sql4 += "住所１, "
        Sql4 += "住所２, "
        Sql4 += "住所３, "
        Sql4 += "電話番号, "
        Sql4 += "ＦＡＸ番号, "
        Sql4 += "代表者役職, "
        Sql4 += "代表者名, "
        Sql4 += "表示順, "
        Sql4 += "備考, "
        Sql4 += "銀行コード, "
        Sql4 += "支店コード, "
        Sql4 += "預金種目, "
        Sql4 += "口座番号, "
        Sql4 += "口座名義, "
        Sql4 += "更新者, "
        Sql4 += "更新日 "
        Sql4 += "FROM "
        Sql4 += "public"
        Sql4 += "."
        Sql4 += "m01_company"

        Dim ds4 = _db.selectDB(Sql4, RS, reccnt)


        '定義
        Dim app As Excel.Application = Nothing
        Dim book As Excel.Workbook = Nothing
        Dim sheet As Excel.Worksheet = Nothing



        Try
            '雛形パス
            Dim sHinaPath As String = ""
            sHinaPath = StartUp._iniVal.BaseXlsPath

            '雛形ファイル名
            Dim sHinaFile As String = ""
            sHinaFile = sHinaPath & "\" & "Quotation.xlsx"

            '出力先パス
            Dim sOutPath As String = ""
            sOutPath = StartUp._iniVal.OutXlsPath

            '出力ファイル名
            Dim sOutFile As String = ""
            sOutFile = sOutPath & "\" & CmnData(1) & "-" & CmnData(2) & ".xlsx"



            app = New Excel.Application()
            book = app.Workbooks.Add(sHinaFile)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet)

            sheet.Range("D1").Value = ds4.Tables(RS).Rows(0)(1)
            sheet.Range("D2").Value = ds4.Tables(RS).Rows(0)(3) & " " & ds4.Tables(RS).Rows(0)(4)
            sheet.Range("D3").Value = ds4.Tables(RS).Rows(0)(5) & " " & ds4.Tables(RS).Rows(0)(6)
            sheet.Range("D4").Value = "telp. " & ds4.Tables(RS).Rows(0)(7) & " Fax." & ds4.Tables(RS).Rows(0)(8)

            sheet.Range("E8").Value = CmnData(6)                       '得意先名
            sheet.Range("E9").Value = CmnData(8) & " " & CmnData(7)    '得意先担当者
            sheet.Range("E11").Value = CmnData(11)                       '得意先名
            sheet.Range("E12").Value = CmnData(12)                       '得意先名

            sheet.Range("S8").Value = CmnData(1) & "-" & CmnData(2)    '見積番号
            sheet.Range("S9").Value = CmnData(3)                       '見積番号

            sheet.Range("H27").Value = CmnData(15)                       '見積番号
            sheet.Range("H28").Value = CmnData(10) & " " & CmnData(11)                        '見積番号
            sheet.Range("H29").Value = CmnData(4)                       '見積番号
            sheet.Range("H30").Value = CmnData(16)                       '見積番号

            sheet.Range("D34").Value = CmnData(13)                       '見積番号
            sheet.Range("D35").Value = CmnData(14)                       '見積番号

            Dim rowCnt As Integer = 0
            Dim lstRow As Integer = 22
            Dim addRowCnt As Integer = 0
            Dim currentCnt As Integer = 20
            Dim num As Integer = 1

            rowCnt = ds3.Tables(RS).Rows.Count - 1
            'rowCnt = 10

            Dim cellPos As String = lstRow & ":" & lstRow

            If rowCnt > 1 Then
                For addRow As Integer = 0 To rowCnt
                    Dim R As Object
                    cellPos = lstRow - 2 & ":" & lstRow - 2
                    R = sheet.Range(cellPos)
                    R.Copy()
                    R.Insert()
                    If Marshal.IsComObject(R) Then
                        Marshal.ReleaseComObject(R)
                    End If

                    lstRow = lstRow + 1
                Next
            End If

            Dim totalPrice As Integer = 0

            For index As Integer = 0 To ds3.Tables(RS).Rows.Count - 1
                Dim cell As String

                cell = "A" & currentCnt
                sheet.Range(cell).Value = num
                cell = "C" & currentCnt
                sheet.Range(cell).Value = ds3.Tables(RS).Rows(index)(1) & "/" & ds3.Tables(RS).Rows(index)(2) & "/" & ds3.Tables(RS).Rows(index)(3)
                cell = "L" & currentCnt
                sheet.Range(cell).Value = ds3.Tables(RS).Rows(index)(4)
                cell = "O" & currentCnt
                sheet.Range(cell).Value = ds3.Tables(RS).Rows(index)(5)
                cell = "R" & currentCnt
                sheet.Range(cell).Value = ds3.Tables(RS).Rows(index)(11)
                cell = "V" & currentCnt
                sheet.Range(cell).Value = ds3.Tables(RS).Rows(index)(12)

                totalPrice = totalPrice + ds3.Tables(RS).Rows(index)(12)

                cell = "Z" & currentCnt
                sheet.Range(cell).Value = ds3.Tables(RS).Rows(index)(15)

                'sheet.Rows(currentCnt & ":" & currentCnt)

                currentCnt = currentCnt + 1
                num = num + 1
            Next


            sheet.Range("V" & lstRow + 1).Value = totalPrice
            sheet.Range("V" & lstRow + 2).Value = totalPrice * 10 * 0.01
            sheet.Range("V" & lstRow + 3).Value = totalPrice * 10 * 0.01 + totalPrice

            book.SaveAs(sOutFile)
            app.Visible = True

            _msgHd.dspMSG("CreateExcel")

        Catch ex As Exception
            Throw ex

        Finally
            'app.Quit()
            'Marshal.ReleaseComObject(sheet)
            'Marshal.ReleaseComObject(book)
            'Marshal.ReleaseComObject(app)

        End Try
    End Sub

    Private Sub BtnQuoteRequest_Click(sender As Object, e As EventArgs) Handles BtnQuoteRequest.Click
        Dim createFlg = False

        '見積基本情報
        Dim Sql1 As String = ""
        Sql1 += "SELECT "
        Sql1 += "会社コード, "
        Sql1 += "見積番号, "
        Sql1 += "見積番号枝番, "
        Sql1 += "見積日, "
        Sql1 += "見積有効期限, "
        Sql1 += "得意先コード, "
        Sql1 += "得意先名, "
        Sql1 += "得意先担当者名, "
        Sql1 += "得意先担当者役職, "
        Sql1 += "得意先郵便番号, "
        Sql1 += "得意先住所, "
        Sql1 += "得意先電話番号, "
        Sql1 += "得意先ＦＡＸ, "
        Sql1 += "営業担当者, "
        Sql1 += "入力担当者, "
        Sql1 += "支払条件, "
        Sql1 += "備考, "
        Sql1 += "登録日, "
        Sql1 += "更新日, "
        Sql1 += "更新者 "
        Sql1 += "FROM "
        Sql1 += "public"
        Sql1 += "."
        Sql1 += "t01_mithd"
        Sql1 += " WHERE "
        Sql1 += "見積番号"
        Sql1 += " ILIKE "
        Sql1 += "'%"
        Sql1 += EditNo.ToString
        Sql1 += "%'"
        Sql1 += " AND "
        Sql1 += "見積番号枝番"
        Sql1 += " ILIKE "
        Sql1 += "'%"
        Sql1 += EditSuffix.ToString
        Sql1 += "%'"

        Dim reccnt As Integer = 0
        Dim ds1 = _db.selectDB(Sql1, RS, reccnt)

        Dim Sql2 As String = ""
        Sql2 += "SELECT "
        Sql2 += "見積番号枝番 "
        Sql2 += "FROM "
        Sql2 += "public"
        Sql2 += "."
        Sql2 += "t01_mithd"
        Sql2 += " WHERE "
        Sql2 += "見積番号"
        Sql2 += " ILIKE "
        Sql2 += "'%"
        Sql2 += EditNo.ToString
        Sql2 += "%'"


        Dim ds2 = _db.selectDB(Sql2, RS, reccnt)
        Dim SuffixMax As Integer = 0


        CompanyCode = ds1.Tables(RS).Rows(0)(0)

        Dim CmnData = ds1.Tables(RS).Rows(0)


        '見積明細情報
        Dim Sql3 As String = ""
        Sql3 += "SELECT "
        Sql3 += "仕入区分, "
        Sql3 += "メーカー, "
        Sql3 += "品名, "
        Sql3 += "型式, "
        Sql3 += "数量, "
        Sql3 += "単位, "
        Sql3 += "仕入先名称, "
        Sql3 += "仕入単価, "
        Sql3 += "間接費率, "
        Sql3 += "間接費, "
        Sql3 += "仕入金額, "
        Sql3 += "売単価, "
        Sql3 += "売上金額, "
        Sql3 += "粗利額, "
        Sql3 += "粗利率, "
        Sql3 += "リードタイム, "
        Sql3 += "備考, "
        Sql3 += "登録日 "
        Sql3 += "FROM "
        Sql3 += "public"
        Sql3 += "."
        Sql3 += "t02_mitdt"
        Sql3 += " WHERE "
        Sql3 += "見積番号"
        Sql3 += " ILIKE "
        Sql3 += "'%"
        Sql3 += EditNo.ToString
        Sql3 += "%'"
        Sql3 += " AND "
        Sql3 += "見積番号枝番"
        Sql3 += " ILIKE "
        Sql3 += "'%"
        Sql3 += EditSuffix.ToString
        Sql3 += "%'"

        Dim ds3 = _db.selectDB(Sql3, RS, reccnt)


        Dim supplierlist As New List(Of String)(New String() {})
        Dim supplierChkList As New List(Of Boolean)
        For i As Integer = 0 To ds3.tables(RS).rows.count - 1
            If supplierlist.Contains(ds3.tables(RS).rows(i)("仕入先名称")) = False Then
                supplierlist.Add(ds3.tables(RS).rows(i)("仕入先名称"))
                supplierChkList.Add(False)
            End If
        Next

        Dim supplier

        For i As Integer = 0 To supplierlist.Count - 1
            supplier = supplierlist(i)
            For j As Integer = 0 To ds3.tables(RS).rows.count - 1
                If supplier Is ds3.tables(RS).rows(j)("仕入先名称") And ds3.tables(RS).rows(j)("仕入単価") <= 0 Then
                    supplierChkList(i) = True
                End If
            Next
        Next

        For i As Integer = 0 To supplierlist.Count - 1

            If supplierChkList(i) = True Then

                '定義
                Dim app As Excel.Application = Nothing
                Dim book As Excel.Workbook = Nothing
                Dim sheet As Excel.Worksheet = Nothing



                Try
                    '雛形パス
                    Dim sHinaPath As String = ""
                    sHinaPath = StartUp._iniVal.BaseXlsPath

                    '雛形ファイル名
                    Dim sHinaFile As String = ""
                    sHinaFile = sHinaPath & "\" & "QuotationRequest.xlsx"

                    '出力先パス
                    Dim sOutPath As String = ""
                    sOutPath = StartUp._iniVal.OutXlsPath

                    '出力ファイル名
                    Dim sOutFile As String = ""
                    sOutFile = sOutPath & "\" & CmnData("見積番号") & "-" & CmnData("見積番号枝番") & "_Request_" & supplierlist(i) & ".xlsx"



                    app = New Excel.Application()
                    book = app.Workbooks.Add(sHinaFile)  'テンプレート
                    sheet = CType(book.Worksheets(1), Excel.Worksheet)

                    sheet.Range("AA2").Value = CmnData("見積番号") & "-" & CmnData("見積番号枝番")
                    sheet.Range("AA3").Value = System.DateTime.Today
                    sheet.Range("A12").Value = supplierlist(i)
                    sheet.Range("V19").Value = CmnData("営業担当者")
                    sheet.Range("V20").Value = CmnData("入力担当者")


                    Dim rowCnt As Integer = 0
                    Dim lstRow As Integer = 23
                    'Dim addRowCnt As Integer = 0
                    'Dim currentCnt As Integer = 20
                    'Dim num As Integer = 1


                    For j As Integer = 0 To ds3.tables(RS).rows.count - 1
                        If supplierlist(i) Is ds3.tables(RS).rows(j)("仕入先名称") And ds3.tables(RS).rows(j)("仕入単価") <= 0 And ds3.tables(RS).rows(j)("仕入区分") = 1 Then
                            If rowCnt = 0 Then
                                sheet.Range("A23").Value = ds3.tables(RS).rows(j)("メーカー") & vbLf & ds3.tables(RS).rows(j)("品名") & vbLf & ds3.tables(RS).rows(j)("型式")
                                sheet.Range("J23").Value = ds3.tables(RS).rows(j)("数量") & " " & ds3.tables(RS).rows(j)("単位")
                                'sheet.Rows(lstRow & ":" & lstRow).AutoFit
                            Else
                                Dim cellPos As String = lstRow & ":" & lstRow
                                Dim R As Object
                                cellPos = lstRow & ":" & lstRow
                                R = sheet.Range(cellPos)
                                R.Copy()
                                R.Insert()
                                If Marshal.IsComObject(R) Then
                                    Marshal.ReleaseComObject(R)
                                End If

                                lstRow = lstRow + 1

                                sheet.Range("A" & lstRow).Value = ds3.tables(RS).rows(j)("メーカー") & vbLf & ds3.tables(RS).rows(j)("品名") & vbLf & ds3.tables(RS).rows(j)("型式")
                                sheet.Range("J" & lstRow).Value = ds3.tables(RS).rows(j)("数量") & " " & ds3.tables(RS).rows(j)("単位")
                                'sheet.Rows(lstRow & ":" & lstRow).AutoFit

                            End If



                        End If
                    Next

                    book.SaveAs(sOutFile)
                    app.Visible = True

                    '_msgHd.dspMSG("CreateExcel")
                    createFlg = True

                Catch ex As Exception
                    Throw ex

                Finally
                    'app.Quit()
                    'Marshal.ReleaseComObject(sheet)
                    'Marshal.ReleaseComObject(book)
                    'Marshal.ReleaseComObject(app)

                End Try
            End If

        Next

        If (createFlg = True) Then
            _msgHd.dspMSG("CreateExcel")
        End If

    End Sub

End Class