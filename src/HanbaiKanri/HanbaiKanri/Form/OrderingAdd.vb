Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class OrderingAdd
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
    Private _init As Boolean                             '初期処理済フラグ

    Private CompanyCode As String = ""
    Private NewPurchaseNo As String = ""
    Private PurchaseCount As String = ""
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
        TxtInput.Text = frmC01F10_Login.loginValue.TantoNM
        _init = True

    End Sub

    Private Sub Quote_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim reccnt As Integer = 0
        Dim dtNow As DateTime = DateTime.Now

        Dim SqlSaiban As String = ""
        SqlSaiban += "SELECT "
        SqlSaiban += "会社コード, "
        SqlSaiban += "採番キー, "
        SqlSaiban += "最新値, "
        SqlSaiban += "最小値, "
        SqlSaiban += "最大値, "
        SqlSaiban += "接頭文字, "
        SqlSaiban += "連番桁数 "
        SqlSaiban += "FROM public.m80_saiban"
        SqlSaiban += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        SqlSaiban += " AND 採番キー = '30'"

        Dim Saiban As DataSet = _db.selectDB(SqlSaiban, RS, reccnt)

        PurchaseCount = Saiban.Tables(RS).Rows(0)(2)
        NewPurchaseNo = Saiban.Tables(RS).Rows(0)(5)
        NewPurchaseNo += dtNow.ToString("MMdd")
        NewPurchaseNo += PurchaseCount.PadLeft(Saiban.Tables(RS).Rows(0)(6), "0")

        PurchaseCount += 1
        Dim Saiban4 As String = ""
        Saiban4 += "UPDATE Public.m80_saiban "
        Saiban4 += "SET "
        Saiban4 += " 最新値 = '" & PurchaseCount.ToString & "'"
        Saiban4 += " , 更新者 = 'Admin'"
        Saiban4 += " , 更新日 = '" & dtNow & "'"
        Saiban4 += " WHERE 会社コード ='" & frmC01F10_Login.loginValue.BumonCD & "'"
        Saiban4 += " AND 採番キー ='30' "
        _db.executeDB(Saiban4)

        TxtOrderingNo.Text = NewPurchaseNo
        TxtOrderingSuffix.Text = 1

        'DateTimePickerのフォーマットを指定
        DtpPurchaseRegistration.Text = DateAdd("m", 0, Now).ToString("yyyy/MM/dd")
        DtpPurchaseDate.Text = DateAdd("m", 0, Now).ToString("yyyy/MM/dd")

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
        'Dim column As New DataGridViewComboBoxColumn()
        'DataGridViewComboBoxColumnのDataSourceを設定
        'column.DataSource = table
        '実際の値が"Value"列、表示するテキストが"Display"列とする
        'column.ValueMember = "Value"
        'column.DisplayMember = "Display"
        'column.HeaderText = "仕入区分"
        'column.Name = "仕入区分"
        'column.ValueMember = 1
        'DataGridView1に追加する
        'DgvItemList.Columns.Insert(1, column)
    End Sub

    Private Sub CellValueChanged(ByVal sender As Object,
    ByVal e As DataGridViewCellEventArgs) _
    Handles DgvItemList.CellValueChanged

        TxtOrderingAmount.Clear()

        Dim PurchaseTotal As Integer = 0

        If e.RowIndex > -1 Then
            If DgvItemList.Rows(e.RowIndex).Cells("数量").Value IsNot Nothing And DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value IsNot Nothing Then
                DgvItemList.Rows(e.RowIndex).Cells("仕入金額").Value = DgvItemList.Rows(e.RowIndex).Cells("数量").Value * DgvItemList.Rows(e.RowIndex).Cells("仕入単価").Value
            End If
        End If

        For index As Integer = 0 To DgvItemList.Rows.Count - 1
            PurchaseTotal += DgvItemList.Rows(index).Cells("仕入金額").Value
        Next
        TxtOrderingAmount.Text = PurchaseTotal

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
    '任意の場所に行を挿入
    Private Sub BtnInsert_Click(sender As Object, e As EventArgs) Handles BtnInsert.Click
        If DgvItemList.Rows.Count > 0 Then
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
            TxtItemCount.Text = DgvItemList.Rows.Count()
        Else
            DgvItemList.Rows.Add()
            TxtItemCount.Text = DgvItemList.Rows.Count()

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

        TxtOrderingAmount.Clear()

        Dim Total As Integer = 0
        Dim PurchaseTotal As Integer = 0
        Dim GrossProfit As Decimal = 0

        For c As Integer = 0 To DgvItemList.Rows.Count - 1
            PurchaseTotal += DgvItemList.Rows(c).Cells("仕入金額").Value
        Next
        TxtOrderingAmount.Text = PurchaseTotal

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
            For c As Integer = 0 To 12
                Item(c) = DgvItemList.Rows(RowIdx).Cells(c).Value
            Next c

            '行を挿入
            DgvItemList.Rows.Insert(RowIdx + 1)

            '追加した行に複製元の値を格納
            For c As Integer = 0 To 12
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
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '前の画面に戻る
    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    Private Sub TxtCustomerCode_DoubleClick(sender As Object, e As EventArgs) Handles TxtSupplierCode.DoubleClick
        Dim openForm As Form = Nothing
        Dim idx As Integer = 0
        Dim Status As String = CommonConst.STATUS_ADD
        openForm = New SupplierSearch(_msgHd, _db, _langHd, idx, Me, Status)   '処理選択
        openForm.Show(Me)
        Me.Enabled = False
    End Sub

    Private Sub DgvItemList_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) _
     Handles DgvItemList.CellDoubleClick
        Dim Status As String = CommonConst.STATUS_ADD
        Dim ColIdx As Integer
        ColIdx = DgvItemList.CurrentCell.ColumnIndex
        Dim RowIdx As Integer
        RowIdx = DgvItemList.CurrentCell.RowIndex

        Dim Maker As String = DgvItemList.Rows(RowIdx).Cells(2).Value
        Dim Item As String = DgvItemList.Rows(RowIdx).Cells(3).Value
        Dim Model As String = DgvItemList.Rows(RowIdx).Cells(4).Value

        If ColIdx = 1 Then                  'メーカー検索
            Dim openForm As Form = Nothing
            openForm = New MakerSearch(_msgHd, _db, Me, RowIdx, ColIdx, Maker, Item, Model, Status)   '処理選択
            openForm.Show(Me)
            Me.Enabled = False
        End If

        If ColIdx = 2 Then              '品名検索
            If Maker IsNot Nothing Then
                Dim openForm As Form = Nothing
                openForm = New MakerSearch(_msgHd, _db, Me, RowIdx, ColIdx, Maker, Item, Model, Status)   '処理選択
                openForm.Show(Me)
                Me.Enabled = False
            Else
                MessageBox.Show("メーカーを入力してください。",
                "エラー",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
            End If
        End If

        If ColIdx = 3 Then
            If Maker IsNot Nothing And Item IsNot Nothing Then
                Dim openForm As Form = Nothing
                openForm = New MakerSearch(_msgHd, _db, Me, RowIdx, ColIdx, Maker, Item, Model, Status)
                openForm.Show(Me)
                Me.Enabled = False
            Else
                MessageBox.Show("メーカー、品名を入力してください。",
                "エラー",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub BtnRegistration_Click(sender As Object, e As EventArgs) Handles BtnRegistration.Click
        Dim reccnt As Integer = 0
        Dim dtNow As DateTime = DateTime.Now

        Dim Sql3 As String = ""
        Sql3 = ""
        Sql3 += "INSERT INTO Public.t20_hattyu("
        Sql3 += "会社コード, 発注番号, 発注番号枝番, 仕入先コード, 仕入先名, 仕入先郵便番号, 仕入先住所, 仕入先電話番号, 仕入先ＦＡＸ, 仕入先担当者役職, 仕入先担当者名, 支払条件, 仕入金額, 営業担当者, 入力担当者, 備考, 発注日, 登録日, 更新日, 更新者, 取消区分)"
        Sql3 += " VALUES('"
        Sql3 += frmC01F10_Login.loginValue.BumonCD
        Sql3 += "', '"
        Sql3 += TxtOrderingNo.Text
        Sql3 += "', '"
        Sql3 += TxtOrderingSuffix.Text
        Sql3 += "', '"
        Sql3 += TxtSupplierCode.Text
        Sql3 += "', '"
        Sql3 += TxtSupplierName.Text
        Sql3 += "', '"
        Sql3 += TxtPostalCode.Text
        Sql3 += "', '"
        Sql3 += TxtAddress1.Text
        Sql3 += "', '"
        Sql3 += TxtTel.Text
        Sql3 += "', '"
        Sql3 += TxtFax.Text
        Sql3 += "', '"
        Sql3 += TxtPosition.Text
        Sql3 += "', '"
        Sql3 += TxtPerson.Text
        Sql3 += "', '"
        Sql3 += TxtPaymentTerms.Text
        Sql3 += "', '"
        If TxtOrderingAmount.Text = "" Then
            Sql3 += "0"
        Else
            Sql3 += TxtOrderingAmount.Text
        End If
        Sql3 += "', '"
        Sql3 += TxtSales.Text
        Sql3 += "', '"
        Sql3 += TxtInput.Text
        Sql3 += "', '"
        Sql3 += TxtPurchaseRemark.Text
        Sql3 += "', '"
        Sql3 += DtpPurchaseDate.Value
        Sql3 += "', '"
        Sql3 += DtpPurchaseRegistration.Value
        Sql3 += "', '"
        Sql3 += dtNow
        Sql3 += "', '"
        Sql3 += frmC01F10_Login.loginValue.TantoNM
        Sql3 += "', '"
        Sql3 += "0"
        Sql3 += "')"

        _db.executeDB(Sql3)

        Dim Sql4 As String = ""
        For hattyuIdx As Integer = 0 To DgvItemList.Rows.Count - 1
            Sql4 = ""
            Sql4 += "INSERT INTO "
            Sql4 += "Public."
            Sql4 += "t21_hattyu("
            Sql4 += "会社コード, 発注番号, 発注番号枝番, 行番号, 仕入区分, メーカー, 品名, 型式, 単位, 仕入値, 発注数量, 仕入数量, 発注残数, 間接費, 仕入単価, 仕入金額, リードタイム, 入庫数, 未入庫数, 備考, 更新者, 登録日)"
            Sql4 += " VALUES('"
            Sql4 += frmC01F10_Login.loginValue.BumonCD
            Sql4 += "', '"
            Sql4 += TxtOrderingNo.Text
            Sql4 += "', '"
            Sql4 += TxtOrderingSuffix.Text
            Sql4 += "', '"
            Sql4 += DgvItemList.Rows(hattyuIdx).Cells("No").Value.ToString
            Sql4 += "', '2"
            'Sql4 += DgvItemList.Rows(hattyuIdx).Cells("仕入区分").Value.ToString
            Sql4 += "', '"
            Sql4 += DgvItemList.Rows(hattyuIdx).Cells("メーカー").Value.ToString
            Sql4 += "', '"
            Sql4 += DgvItemList.Rows(hattyuIdx).Cells("品名").Value.ToString
            Sql4 += "', '"
            Sql4 += DgvItemList.Rows(hattyuIdx).Cells("型式").Value.ToString
            Sql4 += "', '"
            Sql4 += DgvItemList.Rows(hattyuIdx).Cells("単位").Value.ToString
            Sql4 += "', '"
            If DgvItemList.Rows(hattyuIdx).Cells("仕入単価").Value Is Nothing Then
                Sql4 += "0"
            Else
                Sql4 += DgvItemList.Rows(hattyuIdx).Cells("仕入単価").Value.ToString
            End If
            Sql4 += "', '"
            If DgvItemList.Rows(hattyuIdx).Cells("数量").Value Is Nothing Then
                Sql4 += "0"
            Else
                Sql4 += DgvItemList.Rows(hattyuIdx).Cells("数量").Value.ToString
            End If
            Sql4 += "', '"
            Sql4 += "0"
            Sql4 += "', '"
            If DgvItemList.Rows(hattyuIdx).Cells("数量").Value Is Nothing Then
                Sql4 += "0"
            Else
                Sql4 += DgvItemList.Rows(hattyuIdx).Cells("数量").Value.ToString
            End If
            Sql4 += "', '"
            If DgvItemList.Rows(hattyuIdx).Cells("間接費").Value Is Nothing Then
                Sql4 += "0"
            Else
                Sql4 += DgvItemList.Rows(hattyuIdx).Cells("間接費").Value.ToString
            End If
            Sql4 += "', '"
            If DgvItemList.Rows(hattyuIdx).Cells("仕入単価").Value Is Nothing Then
                Sql4 += "0"
            Else
                Sql4 += DgvItemList.Rows(hattyuIdx).Cells("仕入単価").Value.ToString
            End If
            Sql4 += "', '"
            If DgvItemList.Rows(hattyuIdx).Cells("仕入金額").Value Is Nothing Then
                Sql4 += "0"
            Else
                Sql4 += DgvItemList.Rows(hattyuIdx).Cells("仕入金額").Value.ToString
            End If
            Sql4 += "', '"
            Sql4 += DgvItemList.Rows(hattyuIdx).Cells("リードタイム").Value.ToString
            Sql4 += "', '"
            Sql4 += "0"
            Sql4 += "', '"
            If DgvItemList.Rows(hattyuIdx).Cells("数量").Value Is Nothing Then
                Sql4 += "0"
            Else
                Sql4 += DgvItemList.Rows(hattyuIdx).Cells("数量").Value.ToString
            End If
            Sql4 += "', '"
            Sql4 += DgvItemList.Rows(hattyuIdx).Cells("備考").Value.ToString
            Sql4 += "', '"
            Sql4 += frmC01F10_Login.loginValue.TantoNM
            Sql4 += "', '"
            Sql4 += dtNow
            Sql4 += "')"

            _db.executeDB(Sql4)
        Next
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    Private Sub TxtSales_DoubleClick(sender As Object, e As EventArgs) Handles TxtSales.DoubleClick
        Dim openForm As Form = Nothing
        Dim Status As String = "PURCHASE"
        openForm = New SalesSearch(_msgHd, _db, _langHd, Me, Status)   '処理選択
        openForm.Show(Me)
        Me.Enabled = False
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class