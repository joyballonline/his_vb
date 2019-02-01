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
    Private BillingAmount As Integer = 0
    Private Balance As Integer = 0

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
        'If _status = "VIEW" Then
        '    DgvCustomer.Visible = False
        '    DgvDeposit.Visible = False
        '    DgvBillingInfo.Visible = False
        '    LblDeposit.Visible = False
        '    LblBillingInfo.Visible = False
        '    LblNo1.Visible = False
        '    LblNo2.Visible = False
        '    LblNo3.Visible = False
        '    LblDepositDate.Visible = False
        '    LblRemarks.Visible = False
        '    TxtCount1.Visible = False
        '    TxtCount2.Visible = False
        '    TxtCount3.Visible = False
        '    TxtRemarks.Visible = False
        '    DtpDepositDate.Visible = False

        '    BtnAdd.Visible = False
        '    BtnCal.Visible = False
        '    BtnDelete.Visible = False
        '    BtnRegist.Visible = False

        '    DgvHistory.Location = New Point(10, 19)
        '    DgvHistory.Location = New Size(1326, 625)
        'End If
        Dim Sql1 As String = ""

        Sql1 += "SELECT "
        Sql1 += "* "
        Sql1 += "FROM "
        Sql1 += "public"
        Sql1 += "."
        Sql1 += "m90_hanyo"
        Sql1 += " WHERE "
        Sql1 += "会社コード"
        Sql1 += " ILIKE "
        Sql1 += "'"
        Sql1 += CompanyCode
        Sql1 += "'"
        Sql1 += " AND "
        Sql1 += "固定キー"
        Sql1 += " ILIKE "
        Sql1 += "'"
        Sql1 += "2"
        Sql1 += "'"

        Dim reccnt As Integer = 0
        Dim ds1 As DataSet = _db.selectDB(Sql1, RS, reccnt)

        Dim table As New DataTable("Table")
        table.Columns.Add("Display", GetType(String))
        table.Columns.Add("Value", GetType(Integer))

        For i As Integer = 0 To ds1.Tables(RS).Rows.Count - 1
            table.Rows.Add(ds1.Tables(RS).Rows(i)("文字１"), ds1.Tables(RS).Rows(i)("可変キー"))
        Next

        Dim column As New DataGridViewComboBoxColumn()
        column.DataSource = table
        column.ValueMember = "Value"
        column.DisplayMember = "Display"
        column.HeaderText = "入金種目"
        column.Name = "入金種目"

        DgvDeposit.Columns.Insert(1, column)

        BillLoad()

        If frmC01F10_Login.loginValue.Language = "ENG" Then
            LblHistory.Text = "MoneyReceiptDataHistory"
            LblDeposit.Text = "MoneyReceiptInput"
            LblDeposit.Location = New Point(13, 203)
            LblDeposit.Size = New Size(142, 15)
            LblBillingInfo.Text = "BillingInfomation"
            LblDepositDate.Text = "MoneyReceiptDate"
            LblDepositDate.Location = New Point(137, 335)
            LblDepositDate.Size = New Size(140, 22)
            LblRemarks.Text = "Remarks"
            TxtRemarks.Size = New Size(600, 22)
            LblNo1.Text = "Record"
            LblNo1.Location = New Point(1272, 65)
            LblNo1.Size = New Size(66, 22)
            LblNo2.Text = "Record"
            LblNo2.Location = New Point(1272, 198)
            LblNo2.Size = New Size(66, 22)
            LblNo3.Text = "Record"
            LblNo3.Location = New Point(1272, 335)
            LblNo3.Size = New Size(66, 22)

            TxtCount1.Location = New Point(1228, 65)
            TxtCount2.Location = New Point(1228, 198)
            TxtCount3.Location = New Point(1228, 335)

            BtnAdd.Text = "Add"
            BtnAdd.Location = New Point(151, 203)
            BtnDelete.Text = "Delete"
            BtnDelete.Location = New Point(251, 203)
            BtnCal.Text = "AutomaticAllocation"
            BtnCal.Location = New Point(351, 203)
            BtnCal.Size = New Size(120, 20)
            BtnRegist.Text = "Registration"
            BtnBack.Text = "Back"

            DgvCustomer.Columns("請求先").HeaderText = "BillingAddress"
            DgvCustomer.Columns("請求残高").HeaderText = "BillingBalance"

            DgvHistory.Columns("請求番号").HeaderText = "InvoiceNumber"
            DgvHistory.Columns("入金済請求先").HeaderText = "CustomerName"
            DgvHistory.Columns("入金番号").HeaderText = "DepositNumber"
            DgvHistory.Columns("入金日").HeaderText = "DepositDate"
            DgvHistory.Columns("入金種目").HeaderText = "DepositType"
            DgvHistory.Columns("入金済入金額計").HeaderText = "TotalMoneyReceiptAmount"
            DgvHistory.Columns("備考").HeaderText = "Remarks"

            DgvDeposit.Columns("行番号").HeaderText = "LineNumber"
            DgvDeposit.Columns("入金種目").HeaderText = "MoneyReceiptType"
            DgvDeposit.Columns("入力入金額").HeaderText = "TotalMoneyReceiptAmount"

            DgvBillingInfo.Columns("請求情報請求番号").HeaderText = "InvoiceNumber"
            DgvBillingInfo.Columns("請求日").HeaderText = "BillingDate"
            DgvBillingInfo.Columns("請求金額").HeaderText = "BillingAmount"
            DgvBillingInfo.Columns("請求情報入金額計").HeaderText = "TotalMoneyReceiptAmount"
            DgvBillingInfo.Columns("請求情報請求残高").HeaderText = "BillingBalance"
            DgvBillingInfo.Columns("入金額").HeaderText = "MoneyReceiptAmount"

        End If
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
        Sql1 += " ILIKE "
        Sql1 += "'"
        Sql1 += CompanyCode
        Sql1 += "'"
        Sql1 += " AND "
        Sql1 += "得意先コード"
        Sql1 += " ILIKE "
        Sql1 += "'%"
        Sql1 += CustomerCode
        Sql1 += "%'"

        Dim Sql2 As String = ""
        Sql2 += "SELECT "
        Sql2 += "* "
        Sql2 += "FROM "
        Sql2 += "public"
        Sql2 += "."
        Sql2 += "t25_nkinhd"
        Sql2 += " WHERE "
        Sql2 += "会社コード"
        Sql2 += " ILIKE "
        Sql2 += "'"
        Sql2 += CompanyCode
        Sql2 += "'"
        Sql2 += " AND "
        Sql2 += "請求先コード"
        Sql2 += " ILIKE "
        Sql2 += "'%"
        Sql2 += CustomerCode
        Sql2 += "%'"

        Dim Sql4 As String = ""
        Sql4 += "SELECT "
        Sql4 += "* "
        Sql4 += "FROM "
        Sql4 += "public"
        Sql4 += "."
        Sql4 += "t26_nkindt"
        Sql4 += " WHERE "
        Sql4 += "会社コード"
        Sql4 += " ILIKE "
        Sql4 += "'"
        Sql4 += CompanyCode
        Sql4 += "'"
        Sql4 += " AND "
        Sql4 += "請求先コード"
        Sql4 += " ILIKE "
        Sql4 += "'%"
        Sql4 += CustomerCode
        Sql4 += "%'"

        Dim Sql3 As String = ""
        Sql3 += "SELECT "
        Sql3 += "* "
        Sql3 += "FROM "
        Sql3 += "public"
        Sql3 += "."
        Sql3 += "t23_skyuhd"
        Sql3 += " WHERE "
        Sql3 += "会社コード"
        Sql3 += " ILIKE "
        Sql3 += "'"
        Sql3 += CompanyCode
        Sql3 += "'"
        Sql3 += " AND "
        Sql3 += "得意先コード"
        Sql3 += " ILIKE "
        Sql3 += "'%"
        Sql3 += CustomerCode
        Sql3 += "%'"
        Sql3 += " AND "
        Sql3 += "取消区分"
        Sql3 += " = "
        Sql3 += "0"

        Dim reccnt As Integer = 0
        Dim ds1 As DataSet = _db.selectDB(Sql1, RS, reccnt)
        Dim ds2 As DataSet = _db.selectDB(Sql2, RS, reccnt)
        Dim ds3 As DataSet = _db.selectDB(Sql3, RS, reccnt)
        Dim ds4 As DataSet = _db.selectDB(Sql4, RS, reccnt)
        Dim OrderAmount As Integer = 0
        Dim BillingAmount As Integer = 0
        Dim Balance As Integer = 0
        Dim SellingBalance As Integer = 0

        For index1 As Integer = 0 To ds1.Tables(RS).Rows.Count - 1
            OrderAmount += ds1.Tables(RS).Rows(index1)("見積金額")
        Next

        For index2 As Integer = 0 To ds3.Tables(RS).Rows.Count - 1
            BillingAmount += ds3.Tables(RS).Rows(index2)("請求金額計")
        Next

        For index2 As Integer = 0 To ds3.Tables(RS).Rows.Count - 1
            SellingBalance += ds3.Tables(RS).Rows(index2)("売掛残高")
        Next

        Balance = OrderAmount - BillingAmount

        DgvCustomer.Rows.Add()
        DgvCustomer.Rows(0).Cells("請求先").Value = CustomerName
        DgvCustomer.Rows(0).Cells("請求残高").Value = SellingBalance
        TxtCount1.Text = DgvDeposit.Rows.Count()
        For index As Integer = 0 To ds4.Tables(RS).Rows.Count - 1
            DgvHistory.Rows.Add()
            DgvHistory.Rows(index).Cells("No").Value = index + 1
            DgvHistory.Rows(index).Cells("入金済請求先").Value = ds4.Tables(RS).Rows(index)("請求先名")
            DgvHistory.Rows(index).Cells("入金番号").Value = ds4.Tables(RS).Rows(index)("入金番号")
            DgvHistory.Rows(index).Cells("入金日").Value = ds4.Tables(RS).Rows(index)("入金日")
            DgvHistory.Rows(index).Cells("入金種目").Value = ds4.Tables(RS).Rows(index)("入金種別名")
            DgvHistory.Rows(index).Cells("入金済入金額計").Value = ds4.Tables(RS).Rows(index)("入金額")
            DgvHistory.Rows(index).Cells("備考").Value = ds4.Tables(RS).Rows(index)("備考")
        Next

        TxtCount1.Text = ds2.Tables(RS).Rows.Count
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
        DgvDeposit.Rows(DgvDeposit.Rows.Count() - 1).Cells("入金種目").Value = 1
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
            If DgvBillingInfo.Rows(index).Cells("請求情報請求残高").Value > 0 Then
                If Total - DgvBillingInfo.Rows(index).Cells("請求情報請求残高").Value > 0 Then
                    DgvBillingInfo.Rows(index).Cells("入金額").Value = DgvBillingInfo.Rows(index).Cells("請求情報請求残高").Value
                    DgvBillingInfo.Rows(index).Cells("請求情報入金額計").Value = DgvBillingInfo.Rows(index).Cells("請求情報入金額計").Value + DgvBillingInfo.Rows(index).Cells("入金額").Value
                    DgvBillingInfo.Rows(index).Cells("請求情報請求残高").Value = 0
                    Total -= DgvBillingInfo.Rows(index).Cells("入金額").Value
                ElseIf Total > 0 Then
                    DgvBillingInfo.Rows(index).Cells("入金額").Value = Total
                    If DgvBillingInfo.Rows(index).Cells("請求情報請求残高").Value - Total > 0 Then
                        DgvBillingInfo.Rows(index).Cells("請求情報入金額計").Value = DgvBillingInfo.Rows(index).Cells("請求情報入金額計").Value + DgvBillingInfo.Rows(index).Cells("入金額").Value
                    ElseIf DgvBillingInfo.Rows(index).Cells("請求情報請求残高").Value - Total = 0 Then
                        DgvBillingInfo.Rows(index).Cells("請求情報入金額計").Value = DgvBillingInfo.Rows(index).Cells("請求情報入金額計").Value + DgvBillingInfo.Rows(index).Cells("入金額").Value
                    Else
                        DgvBillingInfo.Rows(index).Cells("請求情報入金額計").Value = Total
                    End If
                    DgvBillingInfo.Rows(index).Cells("請求情報請求残高").Value = DgvBillingInfo.Rows(index).Cells("請求情報請求残高").Value - Total
                    Total -= Total
                End If
            End If
        Next

    End Sub

    Private Sub BtnRegist_Click(sender As Object, e As EventArgs) Handles BtnRegist.Click
        Dim errflg As Boolean = True
        Dim dtToday As DateTime = DateTime.Now
        Dim reccnt As Integer = 0
        Dim DepositAmount As Integer = 0

        Dim Saiban1 As String = ""
        Dim Sql As String = ""
        Dim Sql1 As String = ""
        Dim Sql2 As String = ""
        Dim Sql3 As String = ""
        Dim Sql4 As String = ""
        Dim Sql5 As String = ""
        Dim Sql6 As String = ""

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
        Saiban1 += "90"
        Saiban1 += "'"

        Dim dsSaiban1 As DataSet = _db.selectDB(Saiban1, RS, reccnt)

        Dim PM As String = dsSaiban1.Tables(RS).Rows(0)("接頭文字")
        PM += dtToday.ToString("MMdd")
        PM += dsSaiban1.Tables(RS).Rows(0)("最新値").ToString.PadLeft(dsSaiban1.Tables(RS).Rows(0)("連番桁数"), "0")

        For i As Integer = 0 To DgvDeposit.Rows.Count - 1
            DepositAmount += DgvDeposit.Rows(i).Cells("入力入金額").Value
        Next

        Sql += "SELECT "
        Sql += "* "
        Sql += "FROM "
        Sql += "public"
        Sql += "."
        Sql += "t23_skyuhd"
        Sql += " WHERE "
        Sql += "会社コード"
        Sql += " ILIKE "
        Sql += "'"
        Sql += CompanyCode
        Sql += "'"
        Sql += " AND "
        Sql += "得意先コード"
        Sql += " ILIKE "
        Sql += "'%"
        Sql += CustomerCode
        Sql += "%'"
        Sql += " AND "
        Sql += "取消区分"
        Sql += " = "
        Sql += "0"

        Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

        Sql1 += "SELECT "
        Sql1 += "* "
        Sql1 += "FROM "
        Sql1 += "public"
        Sql1 += "."
        Sql1 += "m01_company"
        Sql1 += " WHERE "
        Sql1 += "会社コード"
        Sql1 += " ILIKE "
        Sql1 += "'"
        Sql1 += CompanyCode
        Sql1 += "'"

        Dim ds1 As DataSet = _db.selectDB(Sql1, RS, reccnt)

        If DgvDeposit.Rows.Count = -1 Then
            If frmC01F10_Login.loginValue.Language = "ENG" Then
                MessageBox.Show("Deposit information has not been entered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                MessageBox.Show("入金情報が入力されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            errflg = False
        End If

        For index As Integer = 0 To DgvDeposit.Rows.Count - 1
            If DgvDeposit.Rows(index).Cells("入力入金額").Value <= 0 Then
                If frmC01F10_Login.loginValue.Language = "ENG" Then
                    MessageBox.Show("Deposit amount is 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    MessageBox.Show("入金額が0です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                errflg = False
            End If
        Next

        If errflg Then
            Sql2 = ""
            Sql2 += "INSERT INTO "
            Sql2 += "Public."
            Sql2 += "t25_nkinhd("
            Sql2 += "会社コード, 入金番号, 入金日, 請求先コード, 請求先名, 振込先, 請求金額, 入金額, 請求残高, 備考, 取消区分, 登録日, 更新者, 更新日)"
            Sql2 += " VALUES('"
            Sql2 += CompanyCode
            Sql2 += "', '"
            Sql2 += PM
            Sql2 += "', '"
            Sql2 += dtToday
            Sql2 += "', '"
            Sql2 += CustomerCode
            Sql2 += "', '"
            Sql2 += CustomerName
            Sql2 += "', '"
            Sql2 += ds1.Tables(RS).Rows(0)("銀行名")
            Sql2 += " "
            Sql2 += ds1.Tables(RS).Rows(0)("支店名")
            Sql2 += " "
            Sql2 += ds1.Tables(RS).Rows(0)("預金種目")
            Sql2 += " "
            Sql2 += ds1.Tables(RS).Rows(0)("口座番号")
            Sql2 += " "
            Sql2 += ds1.Tables(RS).Rows(0)("口座名義")
            Sql2 += "', '"
            Sql2 += BillingAmount.ToString
            Sql2 += "', '"
            Sql2 += DepositAmount.ToString
            Sql2 += "', '"
            Sql2 += Balance.ToString
            Sql2 += "', '"
            Sql2 += TxtRemarks.Text
            Sql2 += "', '"
            Sql2 += "0"
            Sql2 += "', '"
            Sql2 += dtToday
            Sql2 += "', '"
            Sql2 += frmC01F10_Login.loginValue.TantoNM
            Sql2 += "', '"
            Sql2 += dtToday
            Sql2 += " ')"
            Sql2 += "RETURNING 会社コード"
            Sql2 += ", "
            Sql2 += "入金番号"
            Sql2 += ", "
            Sql2 += "入金日"
            Sql2 += ", "
            Sql2 += "請求先コード"
            Sql2 += ", "
            Sql2 += "請求先名"
            Sql2 += ", "
            Sql2 += "振込先"
            Sql2 += ", "
            Sql2 += "請求金額"
            Sql2 += ", "
            Sql2 += "入金額"
            Sql2 += ", "
            Sql2 += "請求残高"
            Sql2 += ", "
            Sql2 += "備考"
            Sql2 += ", "
            Sql2 += "取消区分"
            Sql2 += ", "
            Sql2 += "登録日"
            Sql2 += ", "
            Sql2 += "更新者"
            Sql2 += ", "
            Sql2 += "更新日"

            _db.executeDB(Sql2)

            For index As Integer = 0 To DgvDeposit.Rows.Count - 1
                Sql3 = ""
                Sql3 += "INSERT INTO "
                Sql3 += "Public."
                Sql3 += "t26_nkindt("
                Sql3 += "会社コード, 入金番号, 行番号, 入金種別, 入金種別名, 振込先, 入金額, 更新者, 更新日, 請求先コード, 請求先名, 入金日, 備考)"
                Sql3 += " VALUES('"
                Sql3 += CompanyCode
                Sql3 += "', '"
                Sql3 += PM
                Sql3 += "', '"
                Sql3 += DgvDeposit.Rows(index).Cells("行番号").Value.ToString
                Sql3 += "', '"
                Sql3 += DgvDeposit.Rows(index).Cells("入金種目").Value.ToString
                Sql3 += "', '"
                Sql3 += DgvDeposit.Rows(index).Cells("入金種目").Value.ToString
                Sql3 += "', '"
                Sql3 += ds1.Tables(RS).Rows(0)("銀行名").ToString
                Sql3 += " "
                Sql3 += ds1.Tables(RS).Rows(0)("支店名").ToString
                Sql3 += " "
                Sql3 += ds1.Tables(RS).Rows(0)("預金種目").ToString
                Sql3 += " "
                Sql3 += ds1.Tables(RS).Rows(0)("口座番号").ToString
                Sql3 += " "
                Sql3 += ds1.Tables(RS).Rows(0)("口座名義").ToString
                Sql3 += "', '"
                Sql3 += DgvDeposit.Rows(index).Cells("入力入金額").Value.ToString
                Sql3 += "', '"
                Sql3 += frmC01F10_Login.loginValue.TantoNM
                Sql3 += "', '"
                Sql3 += dtToday
                Sql3 += "', '"
                Sql3 += CustomerCode
                Sql3 += "', '"
                Sql3 += CustomerName
                Sql3 += "', '"
                Sql3 += dtToday
                Sql3 += "', '"
                Sql3 += TxtRemarks.Text
                Sql3 += " ')"
                Sql3 += "RETURNING 会社コード"
                Sql3 += ", "
                Sql3 += "入金番号"
                Sql3 += ", "
                Sql3 += "行番号"
                Sql3 += ", "
                Sql3 += "入金種別"
                Sql3 += ", "
                Sql3 += "入金種別名"
                Sql3 += ", "
                Sql3 += "振込先"
                Sql3 += ", "
                Sql3 += "入金額"
                Sql3 += ", "
                Sql3 += "更新者"
                Sql3 += ", "
                Sql3 += "更新日"
                Sql3 += ", "
                Sql3 += "請求先コード"
                Sql3 += ", "
                Sql3 += "請求先名"
                Sql3 += ", "
                Sql3 += "入金日"
                Sql3 += ", "
                Sql3 += "備考"

                _db.executeDB(Sql3)

                Sql3 = ""
            Next

            For index As Integer = 0 To DgvBillingInfo.Rows.Count - 1
                Sql4 = ""
                Sql4 += "INSERT INTO "
                Sql4 += "Public."
                Sql4 += "t27_nkinkshihd("
                Sql4 += "会社コード, 入金番号, 入金日, 請求番号, 請求先コード, 請求先名, 入金消込額計, 備考, 取消区分, 更新者, 更新日)"
                Sql4 += " VALUES('"
                Sql4 += CompanyCode
                Sql4 += "', '"
                Sql4 += PM
                Sql4 += "', '"
                Sql4 += dtToday
                Sql4 += "', '"
                Sql4 += DgvBillingInfo.Rows(index).Cells("請求情報請求番号").Value.ToString
                Sql4 += "', '"
                Sql4 += CustomerCode
                Sql4 += "', '"
                Sql4 += CustomerName
                Sql4 += "', '"
                Sql4 += DgvBillingInfo.Rows(index).Cells("入金額").Value.ToString
                Sql4 += "', '"
                Sql4 += TxtRemarks.Text
                Sql4 += "', '"
                Sql4 += "0"
                Sql4 += "', '"
                Sql4 += frmC01F10_Login.loginValue.TantoNM
                Sql4 += "', '"
                Sql4 += dtToday
                Sql4 += " ')"
                Sql4 += "RETURNING 会社コード"
                Sql4 += ", "
                Sql4 += "入金番号"
                Sql4 += ", "
                Sql4 += "入金日"
                Sql4 += ", "
                Sql4 += "請求番号"
                Sql4 += ", "
                Sql4 += "請求先コード"
                Sql4 += ", "
                Sql4 += "請求先名"
                Sql4 += ", "
                Sql4 += "入金消込額計"
                Sql4 += ", "
                Sql4 += "備考"
                Sql4 += ", "
                Sql4 += "取消区分"
                Sql4 += ", "
                Sql4 += "更新者"
                Sql4 += ", "
                Sql4 += "更新日"

                _db.executeDB(Sql4)
            Next

            Dim DsSelling As Integer = 0
            Dim DgDeposit As Integer = 0
            Dim DsDeposit As Integer = 0
            Dim SellingBalance As Integer = 0
            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                Sql5 = ""
                Sql5 += "UPDATE "
                Sql5 += "Public."
                Sql5 += "t23_skyuhd "
                Sql5 += "SET "
                Sql5 += " 入金額計"
                Sql5 += " = '"
                If ds.Tables(RS).Rows(index)("入金額計") Is DBNull.Value Then
                    DsDeposit = DgvBillingInfo.Rows(index).Cells("入金額").Value
                Else
                    DsDeposit = DgvBillingInfo.Rows(index).Cells("入金額").Value + ds.Tables(RS).Rows(index)("入金額計")
                End If

                Sql5 += DsDeposit.ToString
                Sql5 += "', "
                Sql5 += "売掛残高"
                Sql5 += " = '"
                DsSelling = ds.Tables(RS).Rows(index)("売掛残高")
                DgDeposit = DgvBillingInfo.Rows(index).Cells("入金額").Value
                SellingBalance = DsSelling - DgDeposit
                Sql5 += SellingBalance.ToString
                Sql5 += "', "
                Sql5 += "入金完了日"
                Sql5 += " = '"
                Sql5 += dtToday
                Sql5 += "' "
                Sql5 += "WHERE"
                Sql5 += " 会社コード"
                Sql5 += "='"
                Sql5 += CompanyCode
                Sql5 += "'"
                Sql5 += " AND"
                Sql5 += " 請求番号"
                Sql5 += "='"
                Sql5 += ds.Tables(RS).Rows(index)("請求番号")
                Sql5 += "' "
                Sql5 += "RETURNING 会社コード"
                Sql5 += ", "
                Sql5 += "請求番号"
                Sql5 += ", "
                Sql5 += "請求区分"
                Sql5 += ", "
                Sql5 += "請求日"
                Sql5 += ", "
                Sql5 += "受注番号"
                Sql5 += ", "
                Sql5 += "受注番号枝番"
                Sql5 += ", "
                Sql5 += "得意先コード"
                Sql5 += ", "
                Sql5 += "得意先名"
                Sql5 += ", "
                Sql5 += "請求明細数"
                Sql5 += ", "
                Sql5 += "請求金額計"
                Sql5 += ", "
                Sql5 += "請求消費税計"
                Sql5 += ", "
                Sql5 += "現金振込計"
                Sql5 += ", "
                Sql5 += "手数料計"
                Sql5 += ", "
                Sql5 += "入金額計"
                Sql5 += ", "
                Sql5 += "売掛残高"
                Sql5 += ", "
                Sql5 += "備考1"
                Sql5 += ", "
                Sql5 += "備考2"
                Sql5 += ", "
                Sql5 += "入金完了日"
                Sql5 += ", "
                Sql5 += "登録日"
                Sql5 += ", "
                Sql5 += "更新者"
                Sql5 += ", "
                Sql5 += "取消日"
                Sql5 += ", "
                Sql5 += "取消区分"
                Sql5 += ", "
                Sql5 += "入金番号"

                _db.executeDB(Sql5)
                Sql5 = ""
                DsSelling = 0
                DgDeposit = 0
                DsDeposit = 0
                SellingBalance = 0
            Next

            Dim PMNo As Integer

            If dsSaiban1.Tables(RS).Rows(0)("最新値") = dsSaiban1.Tables(RS).Rows(0)("最大値") Then
                PMNo = dsSaiban1.Tables(RS).Rows(0)("最小値")
            Else
                PMNo = dsSaiban1.Tables(RS).Rows(0)("最新値") + 1
            End If

            Sql6 = ""
            Sql6 += "UPDATE "
            Sql6 += "Public."
            Sql6 += "m80_saiban "
            Sql6 += "SET "
            Sql6 += " 最新値"
            Sql6 += " = '"
            Sql6 += PMNo.ToString
            Sql6 += "', "
            Sql6 += "更新者"
            Sql6 += " = '"
            Sql6 += frmC01F10_Login.loginValue.TantoNM
            Sql6 += "', "
            Sql6 += "更新日"
            Sql6 += " = '"
            Sql6 += dtToday
            Sql6 += "' "
            Sql6 += "WHERE"
            Sql6 += " 会社コード"
            Sql6 += "='"
            Sql6 += ds1.Tables(RS).Rows(0)("会社コード").ToString
            Sql6 += "'"
            Sql6 += " AND"
            Sql6 += " 採番キー"
            Sql6 += "='"
            Sql6 += "90"
            Sql6 += "' "
            Sql6 += "RETURNING 会社コード"
            Sql6 += ", "
            Sql6 += "採番キー"
            Sql6 += ", "
            Sql6 += "最新値"
            Sql6 += ", "
            Sql6 += "最小値"
            Sql6 += ", "
            Sql6 += "最大値"
            Sql6 += ", "
            Sql6 += "接頭文字"
            Sql6 += ", "
            Sql6 += "連番桁数"
            Sql6 += ", "
            Sql6 += "更新者"
            Sql6 += ", "
            Sql6 += "更新日"

            _db.executeDB(Sql6)

            _parentForm.Enabled = True
            _parentForm.Show()
            Me.Dispose()
        End If
    End Sub
End Class