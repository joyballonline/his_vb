Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class QuoteRequest
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
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示

        _init = True

    End Sub

    Private Sub QuoteRequest_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim dtNow As DateTime = DateTime.Now
        ' 指定した書式で日付を文字列に変換する
        Dim QuoteNo As String = dtNow.ToString("yyyyMMdd")
        TxtQuoteNo.Text = QuoteNo
        TxtSuffixNo.Text = 1
        'Dim Sql As String = ""
        'Sql += "SELECT "
        'Sql += "見積番号 "
        'Sql += "FROM "
        'Sql += "public"
        'Sql += "."
        'Sql += "t01_mithd"

        'Dim reccnt As Integer = 0
        'Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

        ''重複無しのメーカリスト
        'For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
        '    If LbMaker.Items.Contains(ds.Tables(RS).Rows(index)(0)) = False Then
        '        LbMaker.Items.Add(ds.Tables(RS).Rows(index)(0))
        '    End If
        'Next

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
    End Sub

    '任意の場所に行を挿入
    Private Sub BtnInsert_Click(sender As Object, e As EventArgs) Handles BtnInsert.Click
        Dim RowIdx As Integer = DgvItemList.CurrentCell.RowIndex
        '行を挿入
        DgvItemList.Rows.Insert(RowIdx + 1)

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

            '追加した行に複製元の値を格納
            For c As Integer = 0 To 17
                DgvItemList.Rows(RowIdx + 1).Cells(c).Value = Item(c)
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
        Dim frmMenu As frmC01F30_Menu
        frmMenu = New frmC01F30_Menu(_msgHd, _db)
        frmMenu.Show()
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
            Sql1 += "会社コード, 見積番号, 見積番号枝番, 得意先コード, 得意先名, 得意先担当者役職, 得意先担当者名, 見積日, 見積有効期限, 支払条件, 見積金額, 営業担当者, 入力担当者, 備考, 登録日)"
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
            Sql1 += TxtPerson.Text
            Sql1 += "', '"
            Sql1 += TxtPosition.Text
            Sql1 += "', '"
            Sql1 += DtpQuote.Text
            Sql1 += "', '"
            Sql1 += DtpExpiration.Text
            Sql1 += "', '"
            Sql1 += TxtPaymentTerms.Text
            Sql1 += "', '"
            Sql1 += TxtTotal.Text
            Sql1 += "', '"
            Sql1 += TxtSales.Text
            Sql1 += "', '"
            Sql1 += TxtInput.Text
            Sql1 += "', '"
            Sql1 += TxtRemarks.Text
            Sql1 += "', '"
            Sql1 += DtpRegistration.Text
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
            Sql1 += "営業担当者"
            Sql1 += ", "
            Sql1 += "入力担当者"
            Sql1 += ", "
            Sql1 += "備考"
            Sql1 += ", "
            Sql1 += "登録日"

            _db.executeDB(Sql1)

            Dim Sql2 As String = ""
            For index As Integer = 0 To DgvItemList.Rows.Count - 1
                Sql2 = ""
                Sql2 += "INSERT INTO "
                Sql2 += "Public."
                Sql2 += "t02_mitdt("
                Sql2 += "会社コード, 見積番号, 見積番号枝番, 行番号, 仕入区分, メーカー, 品名, 型式, 数量, 単位, 仕入先名称, 仕入単価, 間接費, 仕入金額, 売単価, 売上金額, 粗利額, 粗利率, リードタイム, 備考, 更新者, 登録日)"
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
    End Sub
End Class