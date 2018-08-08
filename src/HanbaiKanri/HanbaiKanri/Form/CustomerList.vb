Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class CustomerList
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
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByRef prmRefLang As UtilLangHandler)
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

    Private Sub MstCustomere_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Sql1 As String = ""
        Dim Sql2 As String = ""
        Dim Sql3 As String = ""
        Dim reccnt As Integer = 0

        Sql1 += "SELECT "
        Sql1 += "* "
        Sql1 += "FROM "
        Sql1 += "public"
        Sql1 += "."
        Sql1 += "m10_customer"

        Dim ds1 As DataSet = _db.selectDB(Sql1, RS, reccnt)
        Dim Count As Integer = 0
        Dim CustomerCount As Integer = ds1.Tables(RS).Rows.Count
        Dim CustomerOrderCount(CustomerCount) As Integer
        Dim CustomerBillingCount(CustomerCount) As Integer
        Dim CustomerBillingAmount(CustomerCount) As Integer
        Dim CustomerOrderAmount(CustomerCount) As Integer

        For index1 As Integer = 0 To ds1.Tables(RS).Rows.Count - 1
            Sql2 += "SELECT "
            Sql2 += "* "
            Sql2 += "FROM "
            Sql2 += "public"
            Sql2 += "."
            Sql2 += "t23_skyuhd"
            Sql2 += " WHERE "
            Sql2 += "会社コード"
            Sql2 += " = "
            Sql2 += "'"
            Sql2 += ds1.Tables(RS).Rows(index1)("会社コード")
            Sql2 += "'"
            Sql2 += " AND "
            Sql2 += "得意先コード"
            Sql2 += " = "
            Sql2 += "'"
            Sql2 += ds1.Tables(RS).Rows(index1)("得意先コード")
            Sql2 += "'"
            Dim ds2 As DataSet = _db.selectDB(Sql2, RS, reccnt)
            CustomerBillingCount(index1) = ds2.Tables(RS).Rows.Count.ToString

            Sql3 += "SELECT "
            Sql3 += "* "
            Sql3 += "FROM "
            Sql3 += "public"
            Sql3 += "."
            Sql3 += "t10_cymnhd"
            Sql3 += " WHERE "
            Sql3 += "会社コード"
            Sql3 += " = "
            Sql3 += "'"
            Sql3 += ds1.Tables(RS).Rows(index1)("会社コード")
            Sql3 += "'"
            Sql3 += " AND "
            Sql3 += "得意先コード"
            Sql3 += " = "
            Sql3 += "'"
            Sql3 += ds1.Tables(RS).Rows(index1)("得意先コード")
            Sql3 += "'"
            Dim ds3 As DataSet = _db.selectDB(Sql3, RS, reccnt)

            CustomerOrderCount(index1) = ds3.Tables(RS).Rows.Count.ToString

            For index2 As Integer = 0 To ds2.Tables(RS).Rows.Count - 1
                CustomerBillingAmount(index1) += ds2.Tables(RS).Rows(index2)("請求金額計")
            Next

            For index3 As Integer = 0 To ds3.Tables(RS).Rows.Count - 1
                CustomerOrderAmount(index1) += ds3.Tables(RS).Rows(index3)("見積金額")
            Next

            If CustomerOrderCount(index1) > 0 Then
                DgvCustomer.Rows.Add()
                DgvCustomer.Rows(Count).Cells("得意先名").Value = ds1.Tables(RS).Rows(index1)("得意先名")
                DgvCustomer.Rows(Count).Cells("受注金額計").Value = CustomerOrderAmount(index1)
                DgvCustomer.Rows(Count).Cells("請求金額計").Value = CustomerBillingAmount(index1)
                DgvCustomer.Rows(Count).Cells("請求残高").Value = CustomerOrderAmount(index1) - CustomerBillingAmount(index1)
                DgvCustomer.Rows(Count).Cells("受注件数").Value = CustomerOrderCount(index1)
                DgvCustomer.Rows(Count).Cells("請求件数").Value = CustomerBillingCount(index1)
                DgvCustomer.Rows(Count).Cells("得意先コード").Value = ds1.Tables(RS).Rows(index1)("得意先コード")
                DgvCustomer.Rows(Count).Cells("会社コード").Value = ds1.Tables(RS).Rows(index1)("会社コード")

                Count += 1
            End If
            Sql2 = ""
            Sql3 = ""
        Next
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim frmMenu As frmC01F30_Menu
        frmMenu = New frmC01F30_Menu(_msgHd, _langHd, _db)
        frmMenu.Show()
        Me.Close()
    End Sub

    'Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
    '    DgvCustomer.Rows.Clear()

    '    Dim Sql As String = ""
    '    Try
    '        Sql += "SELECT "
    '        Sql += "* "
    '        Sql += "FROM "
    '        Sql += "public"
    '        Sql += "."
    '        Sql += "m10_customer"
    '        Sql += " WHERE "
    '        Sql += "会社コード"
    '        Sql += " ILIKE "
    '        Sql += "'%"
    '        Sql += Search.Text
    '        Sql += "%'"

    '        Dim reccnt As Integer = 0
    '        Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

    '        For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
    '            DgvCustomer.Rows.Add()
    '            DgvCustomer.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)(0)        '会社コード
    '            DgvCustomer.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)(1)        '言語コード
    '            DgvCustomer.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)(2)        '氏名
    '            DgvCustomer.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)(3)      '略名
    '            DgvCustomer.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)(4)      '備考
    '            DgvCustomer.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)(5)      '無効フラグ
    '            DgvCustomer.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)(6)      '更新者
    '            DgvCustomer.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)(7)      '更新日
    '            DgvCustomer.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)(8)        '会社コード
    '            DgvCustomer.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)(9)        '言語コード
    '            DgvCustomer.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)(10)        '氏名
    '            DgvCustomer.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)(11)      '略名
    '            DgvCustomer.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)(12)      '備考
    '            DgvCustomer.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)(13)      '無効フラグ
    '            DgvCustomer.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)(14)      '更新者
    '            DgvCustomer.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)(15)      '更新日
    '            DgvCustomer.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)(16)      '更新日
    '        Next

    '    Catch ue As UsrDefException
    '        ue.dspMsg()
    '        Throw ue
    '    Catch ex As Exception
    '        'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
    '        Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
    '    End Try
    'End Sub

    Private Sub BtnBillingCalculation_Click(sender As Object, e As EventArgs) Handles BtnBillingCalculation.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvCustomer.CurrentCell.RowIndex
        Dim Company As String = DgvCustomer.Rows(RowIdx).Cells("会社コード").Value
        Dim Customer As String = DgvCustomer.Rows(RowIdx).Cells("得意先コード").Value
        Dim openForm As Form = Nothing
        openForm = New CustomerOrderList(_msgHd, _db, _langHd, Me, Company, Customer)   '処理選択

        Me.Enabled = False
        Me.Hide()
        openForm.Show(Me)
    End Sub
End Class