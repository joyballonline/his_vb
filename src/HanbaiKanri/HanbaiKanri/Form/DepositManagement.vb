Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class DepositManagement
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
    Private CustomerCode As String = ""
    Private CustomerName As String = ""
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
                   ByRef prmRefCompany As String,
                   ByRef prmRefCustomer As String,
                   ByRef prmRefName As String,
                   Optional ByRef prmRefStatus As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
        CompanyCode = prmRefCompany
        CustomerCode = prmRefCustomer
        CustomerName = prmRefName
        _status = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        DtpDepositDate.Value = Date.Now
        _init = True

    End Sub

    Private Sub Quote_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim table As New DataTable("Table")
        table.Columns.Add("Display", GetType(String))
        table.Columns.Add("Value", GetType(Integer))
        table.Rows.Add("振込入金", 1)
        table.Rows.Add("振込手数料", 2)
        table.Rows.Add("現金入金", 3)
        table.Rows.Add("手形受入", 4)
        table.Rows.Add("電子債権", 5)
        table.Rows.Add("売上割引", 6)
        table.Rows.Add("売上値引", 7)
        table.Rows.Add("リベート", 8)
        table.Rows.Add("相殺", 9)
        table.Rows.Add("諸口", 10)

        Dim column As New DataGridViewComboBoxColumn()
        column.DataSource = table
        column.ValueMember = "Value"
        column.DisplayMember = "Display"
        column.HeaderText = "入金種目"
        column.Name = "入金種目"

        DgvDeposit.Columns.Insert(1, column)

        BillLoad()
    End Sub

    Private Sub BillLoad()
        Dim Sql1 As String = ""
        Sql1 += "SELECT "
        Sql1 += "* "
        Sql1 += "FROM "
        Sql1 += "public"
        Sql1 += "."
        Sql1 += "t10_cymnhd"
        Sql1 += " WHERE "
        Sql1 += "会社コード"
        Sql1 += " = "
        Sql1 += "'"
        Sql1 += CompanyCode
        Sql1 += "'"
        Sql1 += " AND "
        Sql1 += "得意先コード"
        Sql1 += " ILIKE "
        Sql1 += "'"
        Sql1 += CustomerCode
        Sql1 += "'"

        Dim Sql2 As String = ""
        Sql2 += "SELECT "
        Sql2 += "* "
        Sql2 += "FROM "
        Sql2 += "public"
        Sql2 += "."
        Sql2 += "t25_nkinhd"
        Sql2 += " WHERE "
        Sql2 += "会社コード"
        Sql2 += " = "
        Sql2 += "'"
        Sql2 += CompanyCode
        Sql2 += "'"
        Sql2 += " AND "
        Sql2 += "請求先コード"
        Sql2 += " = "
        Sql2 += "'"
        Sql2 += CustomerCode
        Sql2 += "'"

        Dim Sql3 As String = ""
        Sql3 += "SELECT "
        Sql3 += "* "
        Sql3 += "FROM "
        Sql3 += "public"
        Sql3 += "."
        Sql3 += "t23_skyuhd"
        Sql3 += " WHERE "
        Sql3 += "会社コード"
        Sql3 += " = "
        Sql3 += "'"
        Sql3 += CompanyCode
        Sql3 += "'"
        Sql3 += " AND "
        Sql3 += "得意先コード"
        Sql3 += " = "
        Sql3 += "'"
        Sql3 += CustomerCode
        Sql3 += "'"

        Dim reccnt As Integer = 0
        Dim ds1 As DataSet = _db.selectDB(Sql1, RS, reccnt)
        Dim ds2 As DataSet = _db.selectDB(Sql2, RS, reccnt)
        Dim ds3 As DataSet = _db.selectDB(Sql3, RS, reccnt)
        Dim OrderAmount As Integer = 0
        Dim BillingAmount As Integer = 0
        Dim Balance As Integer = 0

        For index1 As Integer = 0 To ds1.Tables(RS).Rows.Count - 1
            OrderAmount += ds1.Tables(RS).Rows(index1)("見積金額")
        Next

        For index2 As Integer = 0 To ds3.Tables(RS).Rows.Count - 1
            BillingAmount += ds3.Tables(RS).Rows(index2)("請求金額計")
        Next

        Balance = OrderAmount - BillingAmount

        DgvCustomer.Rows.Add()
        DgvCustomer.Rows(0).Cells("請求先").Value = CustomerName
        DgvCustomer.Rows(0).Cells("請求残高").Value = Balance
        TxtCount1.Text = DgvDeposit.Rows.Count()
        'For index As Integer = 0 To ds2.Tables(RS).Rows.Count - 1
        '    DgvHistory.Rows.Add()
        '    DgvHistory.Rows(index).Cells("No").Value = index + 1
        '    DgvHistory.Rows(index).Cells("請求先").Value = ds2.Tables(RS).Rows(index)("請求先名")
        '    DgvHistory.Rows(index).Cells("入金番号").Value = ds2.Tables(RS).Rows(index)("入金番号")
        '    DgvHistory.Rows(index).Cells("型式").Value = ds2.Tables(RS).Rows(index)("型式")
        '    DgvHistory.Rows(index).Cells("受注個数").Value = ds2.Tables(RS).Rows(index)("受注数量")
        '    DgvHistory.Rows(index).Cells("単位").Value = ds2.Tables(RS).Rows(index)("単位")
        '    DgvHistory.Rows(index).Cells("売上数量").Value = ds2.Tables(RS).Rows(index)("売上数量")
        '    DgvHistory.Rows(index).Cells("売上単価").Value = ds2.Tables(RS).Rows(index)("売単価")
        '    DgvHistory.Rows(index).Cells("売上金額").Value = ds2.Tables(RS).Rows(index)("売上金額")
        'Next

        'TxtCount1.Text = ds2.Tables(RS).Rows.Count
        TxtCount2.Text = DgvHistory.Rows.Count()
        For index As Integer = 0 To ds3.Tables(RS).Rows.Count - 1
            DgvBillingInfo.Rows.Add()
            DgvBillingInfo.Rows(index).Cells("InfoNo").Value = index + 1
            DgvBillingInfo.Rows(index).Cells("請求情報請求番号").Value = ds3.Tables(RS).Rows(index)("請求番号")
            DgvBillingInfo.Rows(index).Cells("請求日").Value = ds3.Tables(RS).Rows(index)("請求日")
            DgvBillingInfo.Rows(index).Cells("請求金額").Value = ds3.Tables(RS).Rows(index)("請求金額計")
            If ds3.Tables(RS).Rows(index)("入金額計") Is DBNull.Value Then
                DgvBillingInfo.Rows(index).Cells("請求情報入金額計").Value = 0
            Else
                DgvBillingInfo.Rows(index).Cells("請求情報入金額計").Value = ds3.Tables(RS).Rows(index)("入金額計")
            End If

            If ds3.Tables(RS).Rows(index)("入金額計") Is DBNull.Value Then
                DgvBillingInfo.Rows(index).Cells("請求情報請求残高").Value = ds3.Tables(RS).Rows(index)("請求金額計")
            Else
                DgvBillingInfo.Rows(index).Cells("請求情報請求残高").Value = ds3.Tables(RS).Rows(index)("請求金額計") - ds3.Tables(RS).Rows(index)("入金額計")
            End If
            DgvBillingInfo.Rows(index).Cells("入金額").Value = 0
        Next
        TxtCount3.Text = DgvBillingInfo.Rows.Count()
    End Sub

    '前の画面に戻る
    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        DgvDeposit.Rows.Add()
        '最終行のインデックスを取得
        Dim index As Integer = DgvDeposit.Rows.Count()
        '行番号の振り直し
        Dim No As Integer = 1
        For c As Integer = 0 To index - 1
            DgvDeposit.Rows(c).Cells(0).Value = No
            No += 1
        Next c
        TxtCount2.Text = DgvDeposit.Rows.Count()
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        For Each r As DataGridViewCell In DgvDeposit.SelectedCells
            DgvDeposit.Rows.RemoveAt(r.RowIndex)
        Next r

        '行番号の振り直し
        Dim index As Integer = DgvDeposit.Rows.Count()
        Dim No As Integer = 1
        For c As Integer = 0 To index - 1
            DgvDeposit.Rows(c).Cells(0).Value = No
            No += 1
        Next c
        TxtCount3.Text = DgvDeposit.Rows.Count()
    End Sub

    Private Sub BtnCal_Click(sender As Object, e As EventArgs) Handles BtnCal.Click
        Dim Total As Integer = 0
        Dim count As Integer = 0

        For index As Integer = 0 To DgvDeposit.Rows.Count - 1
            Total += DgvDeposit.Rows(index).Cells("入力入金額").Value
        Next

        For index As Integer = 0 To DgvBillingInfo.Rows.Count - 1
            If Total - DgvBillingInfo.Rows(index).Cells("請求金額").Value > 0 Then
                DgvBillingInfo.Rows(index).Cells("入金額").Value = DgvBillingInfo.Rows(index).Cells("請求金額").Value
                DgvBillingInfo.Rows(index).Cells("請求情報入金額計").Value = DgvBillingInfo.Rows(index).Cells("入金額").Value
                DgvBillingInfo.Rows(index).Cells("請求情報請求残高").Value = 0
                Total -= DgvBillingInfo.Rows(index).Cells("請求金額").Value
            ElseIf Total > 0 Then
                DgvBillingInfo.Rows(index).Cells("入金額").Value = Total
                DgvBillingInfo.Rows(index).Cells("請求情報入金額計").Value = Total
                DgvBillingInfo.Rows(index).Cells("請求情報請求残高").Value = DgvBillingInfo.Rows(index).Cells("請求金額").Value - Total
                Total -= Total
            End If
        Next

    End Sub
End Class