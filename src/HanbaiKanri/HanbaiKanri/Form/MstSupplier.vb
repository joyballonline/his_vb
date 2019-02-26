Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


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
                   ByRef prmRefLang As UtilLangHandler)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang

        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    Private Sub MstSupplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If frmC01F10_Login.loginValue.Language = "ENG" Then
            Label1.Text = "SupplierName"
            TxtSearch.Location = New Point(110, 6)
            BtnSearch.Text = "Search"
            BtnSearch.Location = New Point(216, 5)
            btnSupplierAdd.Text = "Add"
            btnSupplierEdit.Text = "Edit"
            BtnBack.Text = "Back"

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
            Dgv_Supplier.Columns("更新者").HeaderText = "ModifiedBy"
            Dgv_Supplier.Columns("更新日").HeaderText = "UpdateDate"

        End If
        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "m11_supplier"
            Sql += " WHERE "
            Sql += "会社コード"
            Sql += " ILIKE "
            Sql += "'"
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                Dgv_Supplier.Rows.Add()
                Dgv_Supplier.Rows(index).Cells("会社コード").Value = ds.Tables(RS).Rows(index)("会社コード")
                Dgv_Supplier.Rows(index).Cells("仕入先コード").Value = ds.Tables(RS).Rows(index)("仕入先コード")
                Dgv_Supplier.Rows(index).Cells("仕入先名").Value = ds.Tables(RS).Rows(index)("仕入先名")
                Dgv_Supplier.Rows(index).Cells("仕入先名略称").Value = ds.Tables(RS).Rows(index)("仕入先名略称")
                Dgv_Supplier.Rows(index).Cells("郵便番号").Value = ds.Tables(RS).Rows(index)("郵便番号")
                Dgv_Supplier.Rows(index).Cells("住所１").Value = ds.Tables(RS).Rows(index)("住所１")
                Dgv_Supplier.Rows(index).Cells("住所２").Value = ds.Tables(RS).Rows(index)("住所２")
                Dgv_Supplier.Rows(index).Cells("住所３").Value = ds.Tables(RS).Rows(index)("住所３")
                Dgv_Supplier.Rows(index).Cells("電話番号").Value = ds.Tables(RS).Rows(index)("電話番号")
                Dgv_Supplier.Rows(index).Cells("電話番号検索用").Value = ds.Tables(RS).Rows(index)("電話番号検索用")
                Dgv_Supplier.Rows(index).Cells("FAX番号").Value = ds.Tables(RS).Rows(index)("ＦＡＸ番号")
                Dgv_Supplier.Rows(index).Cells("担当者名").Value = ds.Tables(RS).Rows(index)("担当者名")
                Dgv_Supplier.Rows(index).Cells("メモ").Value = ds.Tables(RS).Rows(index)("メモ")
                Dgv_Supplier.Rows(index).Cells("銀行名").Value = ds.Tables(RS).Rows(index)("銀行名")
                Dgv_Supplier.Rows(index).Cells("銀行コード").Value = ds.Tables(RS).Rows(index)("銀行コード")
                Dgv_Supplier.Rows(index).Cells("支店名").Value = ds.Tables(RS).Rows(index)("支店名")
                Dgv_Supplier.Rows(index).Cells("支店コード").Value = ds.Tables(RS).Rows(index)("支店コード")
                Dgv_Supplier.Rows(index).Cells("預金種目").Value = ds.Tables(RS).Rows(index)("預金種目")
                Dgv_Supplier.Rows(index).Cells("口座番号").Value = ds.Tables(RS).Rows(index)("口座番号")
                Dgv_Supplier.Rows(index).Cells("口座名義").Value = ds.Tables(RS).Rows(index)("口座名義")
                Dgv_Supplier.Rows(index).Cells("更新者").Value = ds.Tables(RS).Rows(index)("更新者")
                Dgv_Supplier.Rows(index).Cells("更新日").Value = ds.Tables(RS).Rows(index)("更新日")
                Dgv_Supplier.Rows(index).Cells("担当者役職").Value = ds.Tables(RS).Rows(index)("担当者役職")
                Dgv_Supplier.Rows(index).Cells("関税率").Value = ds.Tables(RS).Rows(index)("関税率")
                Dgv_Supplier.Rows(index).Cells("前払法人税率").Value = ds.Tables(RS).Rows(index)("前払法人税率")
                Dgv_Supplier.Rows(index).Cells("輸送費率").Value = ds.Tables(RS).Rows(index)("輸送費率")
                Dgv_Supplier.Rows(index).Cells("会計用仕入先コード").Value = ds.Tables(RS).Rows(index)("会計用仕入先コード")

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

        openForm = New Supplier(_msgHd, _db, _langHd, Status)   '処理選択
        openForm.Show()
        Me.Hide()   ' 自分は隠れる
    End Sub

    Private Sub btnSupplierEdit_Click(sender As Object, e As EventArgs) Handles btnSupplierEdit.Click
        Dim openForm As Form = Nothing
        Dim Status As String = CommonConst.STATUS_EDIT
        Dim CompanyCode As String = Dgv_Supplier.Rows(Dgv_Supplier.CurrentCell.RowIndex).Cells("会社コード").Value
        Dim SupplierCode As String = Dgv_Supplier.Rows(Dgv_Supplier.CurrentCell.RowIndex).Cells("仕入先コード").Value
        openForm = New Supplier(_msgHd, _db, _langHd, Status, CompanyCode, SupplierCode)   '処理選択
        openForm.Show()
        Me.Hide()   ' 自分は隠れる
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        Dim frmMenu As frmC01F30_Menu
        frmMenu = New frmC01F30_Menu(_msgHd, _langHd, _db)
        frmMenu.Show()
        Me.Close()
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        Dgv_Supplier.Rows.Clear()

        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "m11_supplier"
            Sql += " WHERE "
            Sql += "会社コード"
            Sql += " ILIKE "
            Sql += "'"
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "'"
            Sql += " AND "
            Sql += "仕入先名"
            Sql += " ILIKE "
            Sql += "'%"
            Sql += TxtSearch.Text
            Sql += "%'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                Dgv_Supplier.Rows.Add()
                Dgv_Supplier.Rows(index).Cells("会社コード").Value = ds.Tables(RS).Rows(index)("会社コード")
                Dgv_Supplier.Rows(index).Cells("仕入先コード").Value = ds.Tables(RS).Rows(index)("仕入先コード")
                Dgv_Supplier.Rows(index).Cells("仕入先名").Value = ds.Tables(RS).Rows(index)("仕入先名")
                Dgv_Supplier.Rows(index).Cells("仕入先名略称").Value = ds.Tables(RS).Rows(index)("仕入先名略称")
                Dgv_Supplier.Rows(index).Cells("郵便番号").Value = ds.Tables(RS).Rows(index)("郵便番号")
                Dgv_Supplier.Rows(index).Cells("住所１").Value = ds.Tables(RS).Rows(index)("住所１")
                Dgv_Supplier.Rows(index).Cells("住所２").Value = ds.Tables(RS).Rows(index)("住所２")
                Dgv_Supplier.Rows(index).Cells("住所３").Value = ds.Tables(RS).Rows(index)("住所３")
                Dgv_Supplier.Rows(index).Cells("電話番号").Value = ds.Tables(RS).Rows(index)("電話番号")
                Dgv_Supplier.Rows(index).Cells("電話番号検索用").Value = ds.Tables(RS).Rows(index)("電話番号検索用")
                Dgv_Supplier.Rows(index).Cells("FAX番号").Value = ds.Tables(RS).Rows(index)("ＦＡＸ番号")
                Dgv_Supplier.Rows(index).Cells("担当者名").Value = ds.Tables(RS).Rows(index)("担当者名")
                Dgv_Supplier.Rows(index).Cells("メモ").Value = ds.Tables(RS).Rows(index)("メモ")
                Dgv_Supplier.Rows(index).Cells("銀行名").Value = ds.Tables(RS).Rows(index)("銀行名")
                Dgv_Supplier.Rows(index).Cells("銀行コード").Value = ds.Tables(RS).Rows(index)("銀行コード")
                Dgv_Supplier.Rows(index).Cells("支店名").Value = ds.Tables(RS).Rows(index)("支店名")
                Dgv_Supplier.Rows(index).Cells("支店コード").Value = ds.Tables(RS).Rows(index)("支店コード")
                Dgv_Supplier.Rows(index).Cells("預金種目").Value = ds.Tables(RS).Rows(index)("預金種目")
                Dgv_Supplier.Rows(index).Cells("口座番号").Value = ds.Tables(RS).Rows(index)("口座番号")
                Dgv_Supplier.Rows(index).Cells("口座名義").Value = ds.Tables(RS).Rows(index)("口座名義")
                Dgv_Supplier.Rows(index).Cells("更新者").Value = ds.Tables(RS).Rows(index)("更新者")
                Dgv_Supplier.Rows(index).Cells("更新日").Value = ds.Tables(RS).Rows(index)("更新日")
                Dgv_Supplier.Rows(index).Cells("担当者役職").Value = ds.Tables(RS).Rows(index)("担当者役職")
                Dgv_Supplier.Rows(index).Cells("関税率").Value = ds.Tables(RS).Rows(index)("関税率")
                Dgv_Supplier.Rows(index).Cells("前払法人税率").Value = ds.Tables(RS).Rows(index)("前払法人税率")
                Dgv_Supplier.Rows(index).Cells("輸送費率").Value = ds.Tables(RS).Rows(index)("輸送費率")
                Dgv_Supplier.Rows(index).Cells("会計用仕入先コード").Value = ds.Tables(RS).Rows(index)("会計用仕入先コード")
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try
    End Sub
End Class