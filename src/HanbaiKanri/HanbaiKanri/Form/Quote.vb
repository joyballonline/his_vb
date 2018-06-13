Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


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
    Private _gh As UtilDataGridViewHandler
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
    Private ControllFlg As String = ""
    Private Status As String = ""

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
                   Optional ByRef prmRefNo As String = Nothing,
                   Optional ByRef prmRefSuffix As String = Nothing,
                   Optional ByRef prmRefStatus As String = Nothing)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        EditNo = prmRefNo
        EditSuffix = prmRefSuffix
        Status = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示

        _init = True

    End Sub

    Private Sub Quote_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'DateTimePickerのフォーマットを指定
        DtpRegistration.Text = DateAdd("m", 0, Now).ToString("yyyy/MM/dd")
        DtpQuote.Text = DateAdd("m", 0, Now).ToString("yyyy/MM/dd")
        DtpExpiration.Text = DateAdd("d", 7, Now).ToString("yyyy/MM/dd")

        DgvItemList.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItemList.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

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
        'column.ValueMember = 1
        'DataGridView1に追加する
        DgvItemList.Columns.Insert(1, column)

        If EditNo IsNot Nothing Then    '見積編集時
            ControllFlg = "EDIT"
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

            TxtSuffixNo.Text = SuffixMax + 1
            DtpQuote.Value = ds1.Tables(RS).Rows(0)(3)
            DtpExpiration.Value = ds1.Tables(RS).Rows(0)(4)
            TxtCustomerCode.Text = ds1.Tables(RS).Rows(0)(5)
            TxtCustomerName.Text = ds1.Tables(RS).Rows(0)(6)
            TxtPerson.Text = ds1.Tables(RS).Rows(0)(7)
            TxtPosition.Text = ds1.Tables(RS).Rows(0)(8)
            If ds1.Tables(RS).Rows(0)(9) IsNot DBNull.Value Then
                Dim PostalCode As String = ds1.Tables(RS).Rows(0)(9)
                TxtPostalCode1.Text = PostalCode.Substring(0, 3)
                TxtPostalCode2.Text = PostalCode.Substring(3, 4)
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
        Else    '見積新規追加
            ControllFlg = "ADD"
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
        End If
        If Status Is "VIEW" Then
            DgvItemList.ReadOnly = True
            BtnRegistration.Visible = False
        ElseIf Status Is "PRICE" Then
            BtnRowsAdd.Visible = False
            BtnRowsDel.Visible = False
            BtnUp.Visible = False
            BtnDown.Visible = False
            BtnClone.Visible = False
            BtnInsert.Visible = False

        End If
        LoadFlg = True

    End Sub

    '行追加時にNoを自動採番
    Private Sub DgvItemList_RowsAdded(ByVal sender As Object,
        ByVal e As DataGridViewRowsAddedEventArgs) _
        Handles DgvItemList.RowsAdded
        'セルの既定値を指定する
        count += 1
        Dim index As Integer = e.RowIndex
        DgvItemList.Rows(index).Cells(0).Value = count
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
                DgvItemList.Rows(e.RowIndex).Cells(10).Value = DgvItemList.Rows(e.RowIndex).Cells(8).Value * DgvItemList.Rows(e.RowIndex).Cells(9).Value
                DgvItemList.Rows(e.RowIndex).Cells(11).Value = DgvItemList.Rows(e.RowIndex).Cells(5).Value * DgvItemList.Rows(e.RowIndex).Cells(8).Value + DgvItemList.Rows(e.RowIndex).Cells(10).Value
                DgvItemList.Rows(e.RowIndex).Cells(13).Value = DgvItemList.Rows(e.RowIndex).Cells(5).Value * DgvItemList.Rows(e.RowIndex).Cells(12).Value
                DgvItemList.Rows(e.RowIndex).Cells(14).Value = DgvItemList.Rows(e.RowIndex).Cells(13).Value - DgvItemList.Rows(e.RowIndex).Cells(11).Value
                DgvItemList.Rows(e.RowIndex).Cells(15).Value = Format(DgvItemList.Rows(e.RowIndex).Cells(14).Value / DgvItemList.Rows(e.RowIndex).Cells(13).Value * 100, "0.000")
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
    End Sub

    '行追加（DGVの最終行に追加）
    Private Sub BtnRowsAdd_Click(sender As Object, e As EventArgs) Handles BtnRowsAdd.Click
        DgvItemList.Rows.Add()
        TxtItemCount.Text = DgvItemList.Rows.Count()
        DgvItemList.Rows(DgvItemList.Rows.Count() - 1).Cells(18).Value = "ADD"
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
                    Dim tmp As Integer = Item(c)
                    DgvItemList(1, RowIdx + 1).Value = tmp
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
        openForm = New CustomerSearch(_msgHd, _db)   '処理選択
        openForm.Show(Me)
        'Me.Hide()   ' 自分は隠れる
    End Sub

    'Dgv内での検索
    Private Sub DgvItemList_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) _
     Handles DgvItemList.CellDoubleClick

        Dim ColIdx As Integer
        ColIdx = Me.DgvItemList.CurrentCell.ColumnIndex
        Dim RowIdx As Integer
        RowIdx = Me.DgvItemList.CurrentCell.RowIndex
        Dim Maker As String = DgvItemList.Rows(RowIdx).Cells(2).Value
        Dim Item As String = DgvItemList.Rows(RowIdx).Cells(3).Value
        Dim Model As String = DgvItemList.Rows(RowIdx).Cells(4).Value

        If ColIdx = 2 Then                  'メーカー検索
            Dim openForm As Form = Nothing
            openForm = New MakerSearch(_msgHd, _db, RowIdx, ColIdx, Maker, Item, Model)   '処理選択
            openForm.Show(Me)
        End If

        If ColIdx = 3 Then              '品名検索
            If Maker IsNot Nothing Then
                Dim openForm As Form = Nothing
                openForm = New MakerSearch(_msgHd, _db, RowIdx, ColIdx, Maker, Item, Model)   '処理選択
                openForm.Show(Me)
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
                openForm = New MakerSearch(_msgHd, _db, RowIdx, ColIdx, Maker, Item, Model)   '処理選択
                openForm.Show(Me)
            Else
                MessageBox.Show("メーカー、品名を入力してください。",
                "エラー",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
            End If
        End If

        If ColIdx = 7 Then
            Dim openForm As Form = Nothing
            openForm = New SupplierSearch(_msgHd, _db, RowIdx)   '処理選択
            openForm.Show(Me)
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
        'Dim QuoteList As QuoteList
        'QuoteList = New QuoteList(_msgHd, _db)
        'QuoteList.Show()
        Me.Close()
    End Sub

    Private Sub BtnRegistration_Click(sender As Object, e As EventArgs) Handles BtnRegistration.Click
        Dim dtToday As DateTime = DateTime.Now
        Try
            Dim Sql1 As String = ""
            Sql1 = ""
            Sql1 += "INSERT INTO "
            Sql1 += "Public."
            Sql1 += "t01_mithd("
            Sql1 += "会社コード, 見積番号, 見積番号枝番, 得意先コード, 得意先名, 得意先郵便番号, 得意先住所, 得意先電話番号, 得意先ＦＡＸ, 得意先担当者役職, 得意先担当者名, 見積日, 見積有効期限, 支払条件, 見積金額, 仕入金額, 営業担当者, 入力担当者, 備考, 登録日, 更新日, 更新者)"
            Sql1 += " VALUES('"
            Sql1 += "ZENBI"
            Sql1 += "', '"
            Sql1 += TxtQuoteNo.Text
            Sql1 += "', '"
            Sql1 += TxtSuffixNo.Text
            Sql1 += "', '"
            Sql1 += TxtCustomerCode.Text
            Sql1 += "', '"
            Sql1 += TxtCustomerName.Text
            Sql1 += "', '"
            Sql1 += TxtPostalCode1.Text
            Sql1 += TxtPostalCode2.Text
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
            Sql1 += TxtSales.Text
            Sql1 += "', '"
            Sql1 += TxtInput.Text
            Sql1 += "', '"
            Sql1 += TxtRemarks.Text
            Sql1 += "', '"
            Sql1 += DtpRegistration.Text
            Sql1 += "', '"
            Sql1 += dtToday
            Sql1 += "', '"
            Sql1 += "zenbi01"
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
                Sql2 += "INSERT INTO "
                Sql2 += "Public."
                Sql2 += "t02_mitdt("
                Sql2 += "会社コード, 見積番号, 見積番号枝番, 行番号, 仕入区分, メーカー, 品名, 型式, 数量, 単位, 仕入先名称, 仕入単価, 間接費率, 間接費, 仕入金額, 売単価, 売上金額, 粗利額, 粗利率, リードタイム, 備考, 更新者, 登録日)"
                Sql2 += " VALUES('"
                Sql2 += "ZENBI"
                Sql2 += "', '"
                Sql2 += TxtQuoteNo.Text
                Sql2 += "', '"
                Sql2 += TxtSuffixNo.Text
                Sql2 += "', '"
                Sql2 += DgvItemList.Rows(index).Cells(0).Value.ToString
                Sql2 += "', '"
                Sql2 += DgvItemList.Rows(index).Cells(1).Value.ToString
                Sql2 += "', '"
                Sql2 += DgvItemList.Rows(index).Cells(2).Value.ToString
                Sql2 += "', '"
                Sql2 += DgvItemList.Rows(index).Cells(3).Value.ToString
                Sql2 += "', '"
                Sql2 += DgvItemList.Rows(index).Cells(4).Value.ToString
                Sql2 += "', '"
                Sql2 += DgvItemList.Rows(index).Cells(5).Value.ToString
                Sql2 += "', '"
                Sql2 += DgvItemList.Rows(index).Cells(6).Value.ToString
                Sql2 += "', '"
                Sql2 += DgvItemList.Rows(index).Cells(7).Value.ToString
                Sql2 += "', '"
                Sql2 += DgvItemList.Rows(index).Cells(8).Value.ToString
                Sql2 += "', '"
                Sql2 += DgvItemList.Rows(index).Cells(9).Value.ToString
                Sql2 += "', '"
                Sql2 += DgvItemList.Rows(index).Cells(10).Value.ToString
                Sql2 += "', '"
                Sql2 += DgvItemList.Rows(index).Cells(11).Value.ToString
                Sql2 += "', '"
                Sql2 += DgvItemList.Rows(index).Cells(12).Value.ToString
                Sql2 += "', '"
                Sql2 += DgvItemList.Rows(index).Cells(13).Value.ToString
                Sql2 += "', '"
                Sql2 += DgvItemList.Rows(index).Cells(14).Value.ToString
                Sql2 += "', '"
                Sql2 += DgvItemList.Rows(index).Cells(15).Value.ToString
                Sql2 += "', '"
                Sql2 += DgvItemList.Rows(index).Cells(16).Value.ToString
                Sql2 += "', '"
                Sql2 += DgvItemList.Rows(index).Cells(17).Value.ToString
                Sql2 += "', '"
                Sql2 += "Admin"
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
            If ControllFlg Is "ADD" Then
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
                Sql3 += "Admin"
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
        Me.Close()
    End Sub
End Class