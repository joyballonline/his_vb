Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class AccountsPayable
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
    Private HattyuNo As String = ""
    Private Suffix As String = ""
    Private _parentForm As Form
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
                   ByRef prmRefNo As String,
                   ByRef prmRefSuffix As String,
                   Optional ByRef prmRefStatus As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
        HattyuNo = prmRefNo
        Suffix = prmRefSuffix
        _status = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        DtpAPDate.Value = Date.Now
        _init = True

    End Sub

    Private Sub Quote_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'ComboBoxに表示する項目のリストを作成する
        Dim table As New DataTable("Table")
        table.Columns.Add("Display", GetType(String))
        table.Columns.Add("Value", GetType(Integer))
        table.Rows.Add("前払金買掛", 1)
        table.Rows.Add("通常買掛", 2)

        'DataGridViewComboBoxColumnを作成
        Dim column As New DataGridViewComboBoxColumn()
        'DataGridViewComboBoxColumnのDataSourceを設定
        column.DataSource = table
        '実際の値が"Value"列、表示するテキストが"Display"列とする
        column.ValueMember = "Value"
        column.DisplayMember = "Display"
        column.HeaderText = "買掛区分"
        column.Name = "買掛区分"
        'column.ValueMember = 1
        'DataGridView1に追加する
        DgvAdd.Columns.Insert(1, column)

        BillLoad()

        If _status = "VIEW" Then
            LblNo1.Visible = False
            LblNo2.Visible = False
            LblNo2.Visible = False
            LblCymndt.Visible = False
            LblAdd.Visible = False
            LblBillingDate.Visible = False
            DtpAPDate.Visible = False
            TxtCount1.Visible = False
            TxtCount2.Visible = False
            TxtCount3.Visible = False
            DgvCymn.Visible = False
            DgvCymndt.Visible = False
            DgvAdd.Visible = False
            DgvHistory.ReadOnly = False

            LblHistory.Location = New Point(12, 82)
            DgvHistory.Location = New Point(12, 106)
            DgvHistory.Size = New Point(1326, 566)

            BtnRegist.Visible = False
        End If

    End Sub

    Private Sub BillLoad()
        Dim Sql1 As String = ""
        Sql1 += "SELECT "
        Sql1 += "* "
        Sql1 += "FROM "
        Sql1 += "public"
        Sql1 += "."
        Sql1 += "t20_hattyu"
        Sql1 += " WHERE "
        Sql1 += "発注番号"
        Sql1 += " ILIKE "
        Sql1 += "'"
        Sql1 += HattyuNo
        Sql1 += "'"
        Sql1 += " AND "
        Sql1 += "発注番号枝番"
        Sql1 += " ILIKE "
        Sql1 += "'"
        Sql1 += Suffix
        Sql1 += "'"

        Dim Sql2 As String = ""
        Sql2 += "SELECT "
        Sql2 += "* "
        Sql2 += "FROM "
        Sql2 += "public"
        Sql2 += "."
        Sql2 += "t21_hattyu"
        Sql2 += " WHERE "
        Sql2 += "発注番号"
        Sql2 += " ILIKE "
        Sql2 += "'"
        Sql2 += HattyuNo
        Sql2 += "'"
        Sql2 += " AND "
        Sql2 += "発注番号枝番"
        Sql2 += " ILIKE "
        Sql2 += "'"
        Sql2 += Suffix
        Sql2 += "'"

        Dim Sql3 As String = ""
        Sql3 += "SELECT "
        Sql3 += "* "
        Sql3 += "FROM "
        Sql3 += "public"
        Sql3 += "."
        Sql3 += "t46_kikehd"
        Sql3 += " WHERE "
        Sql3 += "発注番号"
        Sql3 += " ILIKE "
        Sql3 += "'"
        Sql3 += HattyuNo
        Sql3 += "'"
        Sql3 += " AND "
        Sql3 += "発注番号枝番"
        Sql3 += "="
        Sql3 += Suffix

        Dim reccnt As Integer = 0
        Dim ds1 As DataSet = _db.selectDB(Sql1, RS, reccnt)
        Dim ds2 As DataSet = _db.selectDB(Sql2, RS, reccnt)
        Dim ds3 As DataSet = _db.selectDB(Sql3, RS, reccnt)
        Dim AccountsPayable As Integer = 0

        For index As Integer = 0 To ds3.Tables(RS).Rows.Count - 1
            AccountsPayable += ds3.Tables(RS).Rows(index)("買掛金額計")
        Next

        DgvCymn.Rows.Add()
        DgvCymn.Rows(0).Cells("発注番号").Value = ds1.Tables(RS).Rows(0)("発注番号")
        DgvCymn.Rows(0).Cells("発注日").Value = ds1.Tables(RS).Rows(0)("発注日")
        DgvCymn.Rows(0).Cells("仕入先").Value = ds1.Tables(RS).Rows(0)("仕入先名")
        DgvCymn.Rows(0).Cells("発注金額").Value = ds1.Tables(RS).Rows(0)("仕入金額")
        DgvCymn.Rows(0).Cells("買掛金額計").Value = AccountsPayable
        DgvCymn.Rows(0).Cells("買掛残高").Value = ds1.Tables(RS).Rows(0)("仕入金額") - AccountsPayable

        For index As Integer = 0 To ds2.Tables(RS).Rows.Count - 1
            DgvCymndt.Rows.Add()
            DgvCymndt.Rows(index).Cells("明細").Value = ds2.Tables(RS).Rows(index)("行番号")
            DgvCymndt.Rows(index).Cells("メーカー").Value = ds2.Tables(RS).Rows(index)("メーカー")
            DgvCymndt.Rows(index).Cells("品名").Value = ds2.Tables(RS).Rows(index)("品名")
            DgvCymndt.Rows(index).Cells("型式").Value = ds2.Tables(RS).Rows(index)("型式")
            DgvCymndt.Rows(index).Cells("発注個数").Value = ds2.Tables(RS).Rows(index)("発注数量")
            DgvCymndt.Rows(index).Cells("単位").Value = ds2.Tables(RS).Rows(index)("単位")
            DgvCymndt.Rows(index).Cells("仕入数量").Value = ds2.Tables(RS).Rows(index)("仕入数量")
            DgvCymndt.Rows(index).Cells("仕入単価").Value = ds2.Tables(RS).Rows(index)("仕入値")
            DgvCymndt.Rows(index).Cells("仕入金額").Value = ds2.Tables(RS).Rows(index)("仕入金額")
        Next

        TxtCount1.Text = ds2.Tables(RS).Rows.Count

        For index As Integer = 0 To ds3.Tables(RS).Rows.Count - 1
            DgvHistory.Rows.Add()
            DgvHistory.Rows(index).Cells("No").Value = index + 1
            DgvHistory.Rows(index).Cells("買掛番号").Value = ds3.Tables(RS).Rows(index)("買掛番号")
            DgvHistory.Rows(index).Cells("買掛日").Value = ds3.Tables(RS).Rows(index)("買掛日")
            DgvHistory.Rows(index).Cells("買掛区分").Value = ds3.Tables(RS).Rows(index)("買掛区分")
            DgvHistory.Rows(index).Cells("支払先").Value = ds3.Tables(RS).Rows(index)("仕入先名")
            DgvHistory.Rows(index).Cells("買掛金額").Value = ds3.Tables(RS).Rows(index)("買掛金額計")
            DgvHistory.Rows(index).Cells("備考1").Value = ds3.Tables(RS).Rows(index)("備考1")
            DgvHistory.Rows(index).Cells("備考2").Value = ds3.Tables(RS).Rows(index)("備考2")
            DgvHistory.Rows(index).Cells("買掛済み発注番号").Value = ds3.Tables(RS).Rows(index)("発注番号")
            DgvHistory.Rows(index).Cells("買掛済み発注番号枝番").Value = ds3.Tables(RS).Rows(index)("発注番号枝番")
        Next

        TxtCount2.Text = ds3.Tables(RS).Rows.Count

        If DgvCymn.Rows(0).Cells("買掛残高").Value = 0 Then
        Else
            DgvAdd.Rows.Add()
            DgvAdd.Rows(0).Cells("AddNo").Value = 1
            DgvAdd(1, 0).Value = 2
            DgvAdd.Rows(0).Cells("今回支払先").Value = ds1.Tables(RS).Rows(0)("仕入先名")
            DgvAdd.Rows(0).Cells("今回買掛金額計").Value = 0

            TxtCount3.Text = 1
        End If
    End Sub

    '前の画面に戻る
    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    Private Sub BtnClone_Click(sender As Object, e As EventArgs) Handles BtnClone.Click
        'メニュー選択処理
        Dim RowIdx As Integer
        Dim Item(5) As String

        '一覧選択行インデックスの取得

        RowIdx = DgvAdd.CurrentCell.RowIndex


        '選択行の値を格納
        For c As Integer = 0 To 5
            Item(c) = DgvAdd.Rows(RowIdx).Cells(c).Value
        Next c

        '行を挿入
        DgvAdd.Rows.Insert(RowIdx + 1)

        '追加した行に複製元の値を格納
        For c As Integer = 0 To 5
            If c = 1 Then
                If Item(c) IsNot Nothing Then
                    Dim tmp As Integer = Item(c)
                    DgvAdd(1, RowIdx + 1).Value = tmp
                End If
            Else
                DgvAdd.Rows(RowIdx + 1).Cells(c).Value = Item(c)
            End If

        Next c

        '最終行のインデックスを取得
        Dim index As Integer = DgvAdd.Rows.Count()
        '行番号の振り直し
        Dim No As Integer = 1
        For c As Integer = 0 To index - 1
            DgvAdd.Rows(c).Cells(0).Value = No
            No += 1
        Next c
        TxtCount3.Text = DgvAdd.Rows.Count()
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        For Each r As DataGridViewCell In DgvAdd.SelectedCells
            DgvAdd.Rows.RemoveAt(r.RowIndex)
        Next r

        '行番号の振り直し
        Dim index As Integer = DgvAdd.Rows.Count()
        Dim No As Integer = 1
        For c As Integer = 0 To index - 1
            DgvAdd.Rows(c).Cells(0).Value = No
            No += 1
        Next c
        TxtCount3.Text = DgvAdd.Rows.Count()
    End Sub

    Private Sub BtnRegist_Click(sender As Object, e As EventArgs) Handles BtnRegist.Click
        Dim errflg As Boolean = True
        Dim dtToday As DateTime = DateTime.Now
        Dim reccnt As Integer = 0
        Dim AccountsPayable As Integer = 0

        Dim Saiban1 As String = ""
        Dim Sql1 As String = ""
        Dim Sql2 As String = ""
        Dim Sql3 As String = ""
        Dim Sql4 As String = ""

        Saiban1 += "SELECT "
        Saiban1 += "* "
        Saiban1 += "FROM "
        Saiban1 += "public"
        Saiban1 += "."
        Saiban1 += "m80_saiban"
        Saiban1 += " WHERE "
        Saiban1 += "採番キー"
        Saiban1 += " ILIKE "
        Saiban1 += "'"
        Saiban1 += "100"
        Saiban1 += "'"

        Dim dsSaiban1 As DataSet = _db.selectDB(Saiban1, RS, reccnt)

        Dim AP As String = dsSaiban1.Tables(RS).Rows(0)("接頭文字")
        AP += dtToday.ToString("MMdd")
        AP += dsSaiban1.Tables(RS).Rows(0)("最新値").ToString.PadLeft(dsSaiban1.Tables(RS).Rows(0)("連番桁数"), "0")

        Sql1 += "SELECT "
        Sql1 += "* "
        Sql1 += "FROM "
        Sql1 += "public"
        Sql1 += "."
        Sql1 += "t20_hattyu"
        Sql1 += " WHERE "
        Sql1 += "発注番号"
        Sql1 += " ILIKE "
        Sql1 += "'"
        Sql1 += HattyuNo
        Sql1 += "'"
        Sql1 += " AND "
        Sql1 += "発注番号枝番"
        Sql1 += " ILIKE "
        Sql1 += "'"
        Sql1 += Suffix
        Sql1 += "'"

        Dim ds1 As DataSet = _db.selectDB(Sql1, RS, reccnt)

        Sql2 += "SELECT "
        Sql2 += "* "
        Sql2 += "FROM "
        Sql2 += "public"
        Sql2 += "."
        Sql2 += "t46_kikehd"
        Sql2 += " WHERE "
        Sql2 += "発注番号"
        Sql2 += " ILIKE "
        Sql2 += "'"
        Sql2 += HattyuNo
        Sql2 += "'"
        Sql2 += " AND "
        Sql2 += "発注番号枝番"
        Sql2 += "="
        Sql2 += Suffix

        Dim ds2 As DataSet = _db.selectDB(Sql2, RS, reccnt)

        For index As Integer = 0 To ds2.Tables(RS).Rows.Count - 1
            AccountsPayable += ds2.Tables(RS).Rows(index)("買掛金額計")
        Next


        Dim APTotal As Integer = DgvAdd.Rows(0).Cells("今回買掛金額計").Value + AccountsPayable
        Dim Balance As Integer = ds1.Tables(RS).Rows(0)("仕入金額") - APTotal

        If Balance < 0 Then
            MessageBox.Show("買掛金額計が発注金額を超えています。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            errflg = False
        End If

        If DgvAdd.Rows(0).Cells("今回買掛金額計").Value = 0 Then
            MessageBox.Show("買掛金額計が0になっています。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            errflg = False
        End If

        If errflg Then
            Sql3 = ""
            Sql3 += "INSERT INTO "
            Sql3 += "Public."
            Sql3 += "t46_kikehd("
            Sql3 += "会社コード, 買掛番号, 買掛区分, 買掛日, 発注番号, 発注番号枝番, 仕入先コード, 仕入先名, 買掛金額計, 買掛残高, 備考1, 備考2, 取消区分, 登録日, 更新者)"
            Sql3 += " VALUES('"
            Sql3 += ds1.Tables(RS).Rows(0)("会社コード").ToString
            Sql3 += "', '"
            Sql3 += AP
            Sql3 += "', '"
            Sql3 += DgvAdd.Rows(0).Cells("買掛区分").Value.ToString
            Sql3 += "', '"
            Sql3 += DtpAPDate.Value
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("発注番号").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("発注番号枝番").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("仕入先コード").ToString
            Sql3 += "', '"
            Sql3 += ds1.Tables(RS).Rows(0)("仕入先名").ToString
            Sql3 += "', '"
            Sql3 += DgvAdd.Rows(0).Cells("今回買掛金額計").Value
            Sql3 += "', '"
            Sql3 += DgvAdd.Rows(0).Cells("今回買掛金額計").Value
            Sql3 += "', '"
            Sql3 += DgvAdd.Rows(0).Cells("今回備考1").Value
            Sql3 += "', '"
            Sql3 += DgvAdd.Rows(0).Cells("今回備考2").Value
            Sql3 += "', '"
            Sql3 += "0"
            Sql3 += "', '"
            Sql3 += dtToday
            Sql3 += "', '"
            Sql3 += frmC01F10_Login.loginValue.TantoNM
            Sql3 += " ')"
            Sql3 += "RETURNING 会社コード"
            Sql3 += ", "
            Sql3 += "買掛番号"
            Sql3 += ", "
            Sql3 += "買掛区分"
            Sql3 += ", "
            Sql3 += "買掛日"
            Sql3 += ", "
            Sql3 += "発注番号"
            Sql3 += ", "
            Sql3 += "発注番号枝番"
            Sql3 += ", "
            Sql3 += "仕入先コード"
            Sql3 += ", "
            Sql3 += "仕入先名"
            Sql3 += ", "
            Sql3 += "買掛金額計"
            Sql3 += ", "
            Sql3 += "買掛残高"
            Sql3 += ", "
            Sql3 += "備考1"
            Sql3 += ", "
            Sql3 += "備考2"
            Sql3 += ", "
            Sql3 += "取消区分"
            Sql3 += ", "
            Sql3 += "登録日"
            Sql3 += ", "
            Sql3 += "更新者"

            _db.executeDB(Sql3)

            Dim APNo As Integer

            If dsSaiban1.Tables(RS).Rows(0)("最新値") = dsSaiban1.Tables(RS).Rows(0)("最大値") Then
                APNo = dsSaiban1.Tables(RS).Rows(0)("最小値")
            Else
                APNo = dsSaiban1.Tables(RS).Rows(0)("最新値") + 1
            End If

            Sql4 = ""
            Sql4 += "UPDATE "
            Sql4 += "Public."
            Sql4 += "m80_saiban "
            Sql4 += "SET "
            Sql4 += " 最新値"
            Sql4 += " = '"
            Sql4 += APNo.ToString
            Sql4 += "', "
            Sql4 += "更新者"
            Sql4 += " = '"
            Sql4 += frmC01F10_Login.loginValue.TantoNM
            Sql4 += "', "
            Sql4 += "更新日"
            Sql4 += " = '"
            Sql4 += dtToday
            Sql4 += "' "
            Sql4 += "WHERE"
            Sql4 += " 会社コード"
            Sql4 += "='"
            Sql4 += ds1.Tables(RS).Rows(0)("会社コード").ToString
            Sql4 += "'"
            Sql4 += " AND"
            Sql4 += " 採番キー"
            Sql4 += "='"
            Sql4 += "100"
            Sql4 += "' "
            Sql4 += "RETURNING 会社コード"
            Sql4 += ", "
            Sql4 += "採番キー"
            Sql4 += ", "
            Sql4 += "最新値"
            Sql4 += ", "
            Sql4 += "最小値"
            Sql4 += ", "
            Sql4 += "最大値"
            Sql4 += ", "
            Sql4 += "接頭文字"
            Sql4 += ", "
            Sql4 += "連番桁数"
            Sql4 += ", "
            Sql4 += "更新者"
            Sql4 += ", "
            Sql4 += "更新日"

            _db.executeDB(Sql4)
        End If

    End Sub
End Class