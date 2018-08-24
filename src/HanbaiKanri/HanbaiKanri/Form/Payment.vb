Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class Payment
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
    Private SupplierCode As String = ""
    Private SupplierName As String = ""
    Private Suffix As String = ""
    Private _parentForm As Form
    Private _status As String = ""
    Private APAmount As Integer = 0
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
                   ByRef prmRefSupplier As String,
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
        SupplierCode = prmRefSupplier
        SupplierName = prmRefName
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
        column.HeaderText = "支払種目"
        column.Name = "支払種目"

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
        Sql1 += "t20_hattyu"
        Sql1 += " WHERE "
        Sql1 += "会社コード"
        Sql1 += " ILIKE "
        Sql1 += "'"
        Sql1 += CompanyCode
        Sql1 += "'"
        Sql1 += " AND "
        Sql1 += "仕入先コード"
        Sql1 += " ILIKE "
        Sql1 += "'%"
        Sql1 += SupplierCode
        Sql1 += "%'"

        Dim Sql2 As String = ""
        Sql2 += "SELECT "
        Sql2 += "* "
        Sql2 += "FROM "
        Sql2 += "public"
        Sql2 += "."
        Sql2 += "t47_shrihd"
        Sql2 += " WHERE "
        Sql2 += "会社コード"
        Sql2 += " ILIKE "
        Sql2 += "'"
        Sql2 += CompanyCode
        Sql2 += "'"
        Sql2 += " AND "
        Sql2 += "支払先コード"
        Sql2 += " ILIKE "
        Sql2 += "'%"
        Sql2 += SupplierCode
        Sql2 += "%'"

        Dim Sql4 As String = ""
        Sql4 += "SELECT "
        Sql4 += "* "
        Sql4 += "FROM "
        Sql4 += "public"
        Sql4 += "."
        Sql4 += "t48_shridt"
        Sql4 += " WHERE "
        Sql4 += "会社コード"
        Sql4 += " ILIKE "
        Sql4 += "'"
        Sql4 += CompanyCode
        Sql4 += "'"
        Sql4 += " AND "
        Sql4 += "支払先コード"
        Sql4 += " ILIKE "
        Sql4 += "'%"
        Sql4 += SupplierCode
        Sql4 += "%'"

        Dim Sql3 As String = ""
        Sql3 += "SELECT "
        Sql3 += "* "
        Sql3 += "FROM "
        Sql3 += "public"
        Sql3 += "."
        Sql3 += "t46_kikehd"
        Sql3 += " WHERE "
        Sql3 += "会社コード"
        Sql3 += " ILIKE "
        Sql3 += "'"
        Sql3 += CompanyCode
        Sql3 += "'"
        Sql3 += " AND "
        Sql3 += "仕入先コード"
        Sql3 += " ILIKE "
        Sql3 += "'%"
        Sql3 += SupplierCode
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
        Dim OrderingAmount As Integer = 0
        Dim APAmount As Integer = 0
        Dim Balance As Integer = 0
        Dim APBalance As Integer = 0

        For index1 As Integer = 0 To ds1.Tables(RS).Rows.Count - 1
            OrderingAmount += ds1.Tables(RS).Rows(index1)("仕入金額")
        Next

        For index2 As Integer = 0 To ds3.Tables(RS).Rows.Count - 1
            APAmount += ds3.Tables(RS).Rows(index2)("買掛金額計")
        Next

        For index2 As Integer = 0 To ds3.Tables(RS).Rows.Count - 1
            APBalance += ds3.Tables(RS).Rows(index2)("買掛残高")
        Next

        Balance = OrderingAmount - APAmount

        DgvCustomer.Rows.Add()
        DgvCustomer.Rows(0).Cells("支払先").Value = SupplierName
        DgvCustomer.Rows(0).Cells("買掛残高").Value = APBalance
        TxtCount1.Text = DgvDeposit.Rows.Count()
        For index As Integer = 0 To ds4.Tables(RS).Rows.Count - 1
            DgvHistory.Rows.Add()
            DgvHistory.Rows(index).Cells("No").Value = index + 1
            DgvHistory.Rows(index).Cells("支払済支払先").Value = ds4.Tables(RS).Rows(index)("支払先名")
            DgvHistory.Rows(index).Cells("支払番号").Value = ds4.Tables(RS).Rows(index)("支払番号")
            DgvHistory.Rows(index).Cells("支払日").Value = ds4.Tables(RS).Rows(index)("支払日")
            DgvHistory.Rows(index).Cells("支払種目").Value = ds4.Tables(RS).Rows(index)("支払種別名")
            DgvHistory.Rows(index).Cells("支払済支払金額計").Value = ds4.Tables(RS).Rows(index)("支払金額")
            DgvHistory.Rows(index).Cells("備考").Value = ds4.Tables(RS).Rows(index)("備考")
        Next

        TxtCount1.Text = ds2.Tables(RS).Rows.Count
        TxtCount2.Text = DgvHistory.Rows.Count()
        For index As Integer = 0 To ds3.Tables(RS).Rows.Count - 1
            DgvBillingInfo.Rows.Add()
            DgvBillingInfo.Rows(index).Cells("InfoNo").Value = index + 1
            DgvBillingInfo.Rows(index).Cells("買掛情報買掛番号").Value = ds3.Tables(RS).Rows(index)("買掛番号")
            DgvBillingInfo.Rows(index).Cells("買掛日").Value = ds3.Tables(RS).Rows(index)("買掛日")
            DgvBillingInfo.Rows(index).Cells("買掛金額").Value = ds3.Tables(RS).Rows(index)("買掛金額計")
            If ds3.Tables(RS).Rows(index)("支払金額計") Is DBNull.Value Then
                DgvBillingInfo.Rows(index).Cells("買掛情報支払金額計").Value = 0
            Else
                DgvBillingInfo.Rows(index).Cells("買掛情報支払金額計").Value = ds3.Tables(RS).Rows(index)("支払金額計")
            End If

            If ds3.Tables(RS).Rows(index)("支払金額計") Is DBNull.Value Then
                DgvBillingInfo.Rows(index).Cells("買掛情報買掛残高").Value = ds3.Tables(RS).Rows(index)("買掛金額計")
            Else
                DgvBillingInfo.Rows(index).Cells("買掛情報買掛残高").Value = ds3.Tables(RS).Rows(index)("買掛金額計") - ds3.Tables(RS).Rows(index)("支払金額計")
            End If
            DgvBillingInfo.Rows(index).Cells("支払金額").Value = 0
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
            Total += DgvDeposit.Rows(index).Cells("入力支払金額").Value
        Next

        For index As Integer = 0 To DgvBillingInfo.Rows.Count - 1
            If DgvBillingInfo.Rows(index).Cells("買掛情報買掛残高").Value > 0 Then
                If Total - DgvBillingInfo.Rows(index).Cells("買掛情報買掛残高").Value > 0 Then
                    DgvBillingInfo.Rows(index).Cells("支払金額").Value = DgvBillingInfo.Rows(index).Cells("買掛情報買掛残高").Value
                    DgvBillingInfo.Rows(index).Cells("買掛情報支払金額計").Value = DgvBillingInfo.Rows(index).Cells("買掛情報支払金額計").Value + DgvBillingInfo.Rows(index).Cells("支払金額").Value
                    DgvBillingInfo.Rows(index).Cells("買掛情報買掛残高").Value = 0
                    Total -= DgvBillingInfo.Rows(index).Cells("支払金額").Value
                ElseIf Total > 0 Then
                    DgvBillingInfo.Rows(index).Cells("支払金額").Value = Total
                    DgvBillingInfo.Rows(index).Cells("買掛情報支払金額計").Value = Total
                    DgvBillingInfo.Rows(index).Cells("買掛情報買掛残高").Value = DgvBillingInfo.Rows(index).Cells("買掛情報買掛残高").Value - Total
                    Total -= Total
                End If
            End If
        Next

    End Sub

    Private Sub BtnRegist_Click(sender As Object, e As EventArgs) Handles BtnRegist.Click
        Dim errflg As Boolean = True
        Dim dtToday As DateTime = DateTime.Now
        Dim reccnt As Integer = 0
        Dim PaymentAmount As Integer = 0

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
        Saiban1 += "110"
        Saiban1 += "'"

        Dim dsSaiban1 As DataSet = _db.selectDB(Saiban1, RS, reccnt)

        Dim P As String = dsSaiban1.Tables(RS).Rows(0)("接頭文字")
        P += dtToday.ToString("MMdd")
        P += dsSaiban1.Tables(RS).Rows(0)("最新値").ToString.PadLeft(dsSaiban1.Tables(RS).Rows(0)("連番桁数"), "0")

        For i As Integer = 0 To DgvDeposit.Rows.Count - 1
            PaymentAmount += DgvDeposit.Rows(i).Cells("入力支払金額").Value
        Next

        Sql += "SELECT "
        Sql += "* "
        Sql += "FROM "
        Sql += "public"
        Sql += "."
        Sql += "t46_kikehd"
        Sql += " WHERE "
        Sql += "会社コード"
        Sql += " ILIKE "
        Sql += "'"
        Sql += CompanyCode
        Sql += "'"
        Sql += " AND "
        Sql += "仕入先コード"
        Sql += " ILIKE "
        Sql += "'%"
        Sql += SupplierCode
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
            MessageBox.Show("支払情報が入力されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            errflg = False
        End If

        For index As Integer = 0 To DgvDeposit.Rows.Count - 1
            If DgvDeposit.Rows(index).Cells("入力支払金額").Value <= 0 Then
                errflg = False
            End If
        Next

        If errflg Then
            Sql2 = ""
            Sql2 += "INSERT INTO "
            Sql2 += "Public."
            Sql2 += "t47_shrihd("
            Sql2 += "会社コード, 支払番号, 支払日, 支払先コード, 支払先名, 支払先, 買掛金額, 支払金額計, 買掛残高, 備考, 取消区分, 登録日, 更新者, 更新日)"
            Sql2 += " VALUES('"
            Sql2 += CompanyCode
            Sql2 += "', '"
            Sql2 += P
            Sql2 += "', '"
            Sql2 += dtToday
            Sql2 += "', '"
            Sql2 += SupplierCode
            Sql2 += "', '"
            Sql2 += SupplierName
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
            Sql2 += APAmount.ToString
            Sql2 += "', '"
            Sql2 += PaymentAmount.ToString
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
            Sql2 += "支払番号"
            Sql2 += ", "
            Sql2 += "支払日"
            Sql2 += ", "
            Sql2 += "支払先コード"
            Sql2 += ", "
            Sql2 += "支払先名"
            Sql2 += ", "
            Sql2 += "支払先"
            Sql2 += ", "
            Sql2 += "買掛金額"
            Sql2 += ", "
            Sql2 += "支払金額計"
            Sql2 += ", "
            Sql2 += "買掛残高"
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
                Sql3 += "t48_shridt("
                Sql3 += "会社コード, 支払番号, 行番号, 支払種別, 支払種別名, 支払先, 支払金額, 更新者, 更新日, 支払先コード, 支払先名, 支払日, 備考)"
                Sql3 += " VALUES('"
                Sql3 += CompanyCode
                Sql3 += "', '"
                Sql3 += P
                Sql3 += "', '"
                Sql3 += DgvDeposit.Rows(index).Cells("行番号").Value.ToString
                Sql3 += "', '"
                Sql3 += DgvDeposit.Rows(index).Cells("支払種目").Value.ToString
                Sql3 += "', '"
                Sql3 += DgvDeposit.Rows(index).Cells("支払種目").Value.ToString
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
                Sql3 += DgvDeposit.Rows(index).Cells("入力支払金額").Value.ToString
                Sql3 += "', '"
                Sql3 += frmC01F10_Login.loginValue.TantoNM
                Sql3 += "', '"
                Sql3 += dtToday
                Sql3 += "', '"
                Sql3 += SupplierCode
                Sql3 += "', '"
                Sql3 += SupplierName
                Sql3 += "', '"
                Sql3 += dtToday
                Sql3 += "', '"
                Sql3 += TxtRemarks.Text
                Sql3 += " ')"
                Sql3 += "RETURNING 会社コード"
                Sql3 += ", "
                Sql3 += "支払番号"
                Sql3 += ", "
                Sql3 += "行番号"
                Sql3 += ", "
                Sql3 += "支払種別"
                Sql3 += ", "
                Sql3 += "支払種別名"
                Sql3 += ", "
                Sql3 += "支払先"
                Sql3 += ", "
                Sql3 += "支払金額"
                Sql3 += ", "
                Sql3 += "更新者"
                Sql3 += ", "
                Sql3 += "更新日"
                Sql3 += ", "
                Sql3 += "支払先コード"
                Sql3 += ", "
                Sql3 += "支払先名"
                Sql3 += ", "
                Sql3 += "支払日"
                Sql3 += ", "
                Sql3 += "備考"

                _db.executeDB(Sql3)

                Sql3 = ""
            Next

            For index As Integer = 0 To DgvBillingInfo.Rows.Count - 1
                Sql4 = ""
                Sql4 += "INSERT INTO "
                Sql4 += "Public."
                Sql4 += "t49_shrikshihd("
                Sql4 += "会社コード, 支払番号, 支払日, 買掛番号, 支払先コード, 支払先名, 支払消込額計, 備考, 取消区分, 更新者, 更新日)"
                Sql4 += " VALUES('"
                Sql4 += CompanyCode
                Sql4 += "', '"
                Sql4 += P
                Sql4 += "', '"
                Sql4 += dtToday
                Sql4 += "', '"
                Sql4 += DgvBillingInfo.Rows(index).Cells("買掛情報買掛番号").Value.ToString
                Sql4 += "', '"
                Sql4 += SupplierCode
                Sql4 += "', '"
                Sql4 += SupplierName
                Sql4 += "', '"
                Sql4 += DgvBillingInfo.Rows(index).Cells("支払金額").Value.ToString
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
                Sql4 += "支払番号"
                Sql4 += ", "
                Sql4 += "支払日"
                Sql4 += ", "
                Sql4 += "買掛番号"
                Sql4 += ", "
                Sql4 += "支払先コード"
                Sql4 += ", "
                Sql4 += "支払先名"
                Sql4 += ", "
                Sql4 += "支払消込額計"
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

            Dim DsAP As Integer = 0
            Dim DgPayment As Integer = 0
            Dim DsPayment As Integer = 0
            Dim APBalance As Integer = 0
            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                Sql5 = ""
                Sql5 += "UPDATE "
                Sql5 += "Public."
                Sql5 += "t46_kikehd "
                Sql5 += "SET "
                Sql5 += " 支払金額計"
                Sql5 += " = '"
                If ds.Tables(RS).Rows(index)("支払金額計") Is DBNull.Value Then
                    DsPayment = DgvBillingInfo.Rows(index).Cells("支払金額").Value
                Else
                    DsPayment = DgvBillingInfo.Rows(index).Cells("支払金額").Value + ds.Tables(RS).Rows(index)("支払金額計")
                End If
                Sql5 += DsPayment.ToString
                Sql5 += "', "
                Sql5 += "買掛残高"
                Sql5 += " = '"
                DsAP = ds.Tables(RS).Rows(index)("買掛残高")
                DgPayment = DgvBillingInfo.Rows(index).Cells("支払金額").Value
                APBalance = DsAP - DgPayment
                Sql5 += APBalance.ToString
                Sql5 += "', "
                Sql5 += "支払完了日"
                Sql5 += " = '"
                Sql5 += dtToday
                Sql5 += "' "
                Sql5 += "WHERE"
                Sql5 += " 会社コード"
                Sql5 += "='"
                Sql5 += CompanyCode
                Sql5 += "'"
                Sql5 += " AND"
                Sql5 += " 買掛番号"
                Sql5 += "='"
                Sql5 += ds.Tables(RS).Rows(index)("買掛番号")
                Sql5 += "' "
                Sql5 += "RETURNING 会社コード"
                Sql5 += ", "
                Sql5 += "買掛番号"
                Sql5 += ", "
                Sql5 += "買掛区分"
                Sql5 += ", "
                Sql5 += "買掛日"
                Sql5 += ", "
                Sql5 += "発注番号"
                Sql5 += ", "
                Sql5 += "発注番号枝番"
                Sql5 += ", "
                Sql5 += "仕入先コード"
                Sql5 += ", "
                Sql5 += "仕入先名"
                Sql5 += ", "
                Sql5 += "買掛明細数"
                Sql5 += ", "
                Sql5 += "買掛金額計"
                Sql5 += ", "
                Sql5 += "買掛消費税計"
                Sql5 += ", "
                Sql5 += "現金振込計"
                Sql5 += ", "
                Sql5 += "手数料計"
                Sql5 += ", "
                Sql5 += "支払金額計"
                Sql5 += ", "
                Sql5 += "買掛残高"
                Sql5 += ", "
                Sql5 += "備考1"
                Sql5 += ", "
                Sql5 += "備考2"
                Sql5 += ", "
                Sql5 += "支払完了日"
                Sql5 += ", "
                Sql5 += "登録日"
                Sql5 += ", "
                Sql5 += "更新者"
                Sql5 += ", "
                Sql5 += "取消日"
                Sql5 += ", "
                Sql5 += "取消区分"

                _db.executeDB(Sql5)
                Sql5 = ""
                DsAP = 0
                DgPayment = 0
                DsPayment = 0
                APBalance = 0
            Next

            Dim PNo As Integer

            If dsSaiban1.Tables(RS).Rows(0)("最新値") = dsSaiban1.Tables(RS).Rows(0)("最大値") Then
                PNo = dsSaiban1.Tables(RS).Rows(0)("最小値")
            Else
                PNo = dsSaiban1.Tables(RS).Rows(0)("最新値") + 1
            End If

            Sql6 = ""
            Sql6 += "UPDATE "
            Sql6 += "Public."
            Sql6 += "m80_saiban "
            Sql6 += "SET "
            Sql6 += " 最新値"
            Sql6 += " = '"
            Sql6 += PNo.ToString
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
            Sql6 += "110"
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
        End If
    End Sub
End Class