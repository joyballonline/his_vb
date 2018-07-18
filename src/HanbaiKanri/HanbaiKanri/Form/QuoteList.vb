Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class QuoteList
    Inherits System.Windows.Forms.Form

    '------------------------------------------------------------------------------------------------------
    'メンバー定数宣言
    '------------------------------------------------------------------------------------------------------
    'PG制御文字 
    Dim ds As DataSet
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
    Private _parentForm As Form
    Private _status As String = ""
    'Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ
    Private CompanyCode As String = ""
    Private QuoteNo As String()
    Private dtToday As DateTime = DateTime.Now

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
                   ByRef prmRefStatus As String)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLangHd
        _parentForm = prmRefForm
        _status = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示

        _init = True

    End Sub

    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If _status = "EDIT" Then
            LblMode.Text = "編集モード"
            BtnQuoteEdit.Visible = True
            BtnQuoteEdit.Location = New Point(997, 677)
        ElseIf _status = "CLONE" Then
            LblMode.Text = "複写モード"
            BtnQuoteClone.Visible = True
            BtnQuoteClone.Location = New Point(997, 677)
        ElseIf _status = "CANCEL" Then
            LblMode.Text = "取消モード"
            BtnCancel.Visible = True
            BtnCancel.Location = New Point(997, 677)
        ElseIf _status = "VIEW" Then
            LblMode.Text = "参照モード"
            BtnQuoteView.Visible = True
            BtnQuoteView.Location = New Point(997, 677)
        ElseIf _status = "PRICE" Then
            LblMode.Text = "仕入単価入力モード"
            BtnUnitPrice.Visible = True
            BtnUnitPrice.Location = New Point(997, 677)
        ElseIf _status = "ORDER_NEW" Then
            LblMode.Text = "受注新規入力モード"
            BtnOrder.Visible = True
            BtnOrder.Location = New Point(997, 677)
        ElseIf _status = "PURCHASE_NEW" Then
            LblMode.Text = "仕入新規入力モード"
            BtnPurchase.Visible = True
            BtnPurchase.Location = New Point(997, 677)
        End If

        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += " *  "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t01_mithd"
            Sql += " WHERE "
            Sql += "見積有効期限"
            Sql += " >=  "
            Sql += "'"
            Sql += dtToday
            Sql += "'"

            Dim reccnt As Integer = 0
            ds = _db.selectDB(Sql, RS, reccnt)


            DgvMithd.Columns.Add("見積番号", "見積番号")
            DgvMithd.Columns.Add("見積番号枝番", "見積番号枝番")
            DgvMithd.Columns.Add("見積日", "見積日")
            DgvMithd.Columns.Add("見積有効期限", "見積有効期限")
            DgvMithd.Columns.Add("得意先コード", "得意先コード")
            DgvMithd.Columns.Add("得意先名", "得意先名")
            DgvMithd.Columns.Add("得意先郵便番号", "得意先郵便番号")
            DgvMithd.Columns.Add("得意先住所", "得意先住所")
            DgvMithd.Columns.Add("得意先電話番号", "得意先電話番号")
            DgvMithd.Columns.Add("得意先ＦＡＸ", "得意先ＦＡＸ")
            DgvMithd.Columns.Add("見積金額", "見積金額")
            DgvMithd.Columns.Add("仕入金額", "仕入金額")
            DgvMithd.Columns.Add("ＶＡＴ", "ＶＡＴ")
            DgvMithd.Columns.Add("粗利額", "粗利額")
            DgvMithd.Columns.Add("支払条件", "支払条件")
            DgvMithd.Columns.Add("営業担当者", "営業担当者")
            DgvMithd.Columns.Add("入力担当者", "入力担当者")
            DgvMithd.Columns.Add("備考", "備考")
            DgvMithd.Columns.Add("登録日", "登録日")
            DgvMithd.Columns.Add("更新者", "更新者")
            DgvMithd.Columns.Add("更新日", "更新日")

            DgvMithd.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvMithd.Rows.Add()
                DgvMithd.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)("見積番号")
                DgvMithd.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)("見積番号枝番")
                DgvMithd.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)("見積日")
                DgvMithd.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)("見積有効期限")
                DgvMithd.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)("得意先コード")
                DgvMithd.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)("得意先名")
                DgvMithd.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)("得意先郵便番号")
                DgvMithd.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)("得意先住所")
                DgvMithd.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)("得意先電話番号")
                DgvMithd.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)("得意先ＦＡＸ")
                DgvMithd.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)("見積金額")
                DgvMithd.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)("仕入金額")
                DgvMithd.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)("ＶＡＴ")
                DgvMithd.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)("粗利額")
                DgvMithd.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)("支払条件")
                DgvMithd.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)("営業担当者")
                DgvMithd.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)("入力担当者")
                DgvMithd.Rows(index).Cells(17).Value = ds.Tables(RS).Rows(index)("備考")
                DgvMithd.Rows(index).Cells(18).Value = ds.Tables(RS).Rows(index)("登録日")
                DgvMithd.Rows(index).Cells(19).Value = ds.Tables(RS).Rows(index)("更新者")
                DgvMithd.Rows(index).Cells(20).Value = ds.Tables(RS).Rows(index)("更新日")
            Next



        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()

    End Sub

    Private Sub RbtnDetails_CheckedChanged(sender As Object, e As EventArgs) Handles RbtnDetails.CheckedChanged
        DgvMithd.Rows.Clear()
        DgvMithd.Columns.Clear()

        If RbtnSlip.Checked Then
            Dim Sql As String = ""
            Sql += "SELECT "
            Sql += " * "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t01_mithd"
            If QuoteNo IsNot Nothing Then
                For i As Integer = 0 To QuoteNo.Length - 1
                    If i = 0 Then
                        Sql += " WHERE "
                        Sql += "見積番号"
                        Sql += " ILIKE "
                        Sql += "'%"
                        Sql += QuoteNo(i)
                        Sql += "%'"
                    Else
                        Sql += " OR "
                        Sql += "見積番号"
                        Sql += " ILIKE "
                        Sql += "'%"
                        Sql += QuoteNo(i)
                        Sql += "%'"
                    End If
                Next
            End If

            Dim reccnt As Integer = 0
            ds = _db.selectDB(Sql, RS, reccnt)

            DgvMithd.Columns.Add("見積番号", "見積番号")
            DgvMithd.Columns.Add("見積番号枝番", "見積番号枝番")
            DgvMithd.Columns.Add("見積日", "見積日")
            DgvMithd.Columns.Add("見積有効期限", "見積有効期限")
            DgvMithd.Columns.Add("得意先コード", "得意先コード")
            DgvMithd.Columns.Add("得意先名", "得意先名")
            DgvMithd.Columns.Add("得意先郵便番号", "得意先郵便番号")
            DgvMithd.Columns.Add("得意先住所", "得意先住所")
            DgvMithd.Columns.Add("得意先電話番号", "得意先電話番号")
            DgvMithd.Columns.Add("得意先ＦＡＸ", "得意先ＦＡＸ")
            DgvMithd.Columns.Add("見積金額", "見積金額")
            DgvMithd.Columns.Add("仕入金額", "仕入金額")
            DgvMithd.Columns.Add("ＶＡＴ", "ＶＡＴ")
            DgvMithd.Columns.Add("粗利額", "粗利額")
            DgvMithd.Columns.Add("支払条件", "支払条件")
            DgvMithd.Columns.Add("営業担当者", "営業担当者")
            DgvMithd.Columns.Add("入力担当者", "入力担当者")
            DgvMithd.Columns.Add("備考", "備考")
            DgvMithd.Columns.Add("登録日", "登録日")
            DgvMithd.Columns.Add("更新者", "更新者")
            DgvMithd.Columns.Add("更新日", "更新日")

            DgvMithd.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvMithd.Rows.Add()
                DgvMithd.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)("見積番号")
                DgvMithd.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)("見積番号枝番")
                DgvMithd.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)("見積日")
                DgvMithd.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)("見積有効期限")
                DgvMithd.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)("得意先コード")
                DgvMithd.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)("得意先名")
                DgvMithd.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)("得意先郵便番号")
                DgvMithd.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)("得意先住所")
                DgvMithd.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)("得意先電話番号")
                DgvMithd.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)("得意先ＦＡＸ")
                DgvMithd.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)("見積金額")
                DgvMithd.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)("仕入金額")
                DgvMithd.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)("ＶＡＴ")
                DgvMithd.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)("粗利額")
                DgvMithd.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)("支払条件")
                DgvMithd.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)("営業担当者")
                DgvMithd.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)("入力担当者")
                DgvMithd.Rows(index).Cells(17).Value = ds.Tables(RS).Rows(index)("備考")
                DgvMithd.Rows(index).Cells(18).Value = ds.Tables(RS).Rows(index)("登録日")
                DgvMithd.Rows(index).Cells(19).Value = ds.Tables(RS).Rows(index)("更新者")
                DgvMithd.Rows(index).Cells(20).Value = ds.Tables(RS).Rows(index)("更新日")
            Next
        Else
            Dim Sql As String = ""

            Sql += "SELECT "
            Sql += " * "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t02_mitdt"
            If QuoteNo IsNot Nothing Then
                For i As Integer = 0 To QuoteNo.Length - 1
                    If i = 0 Then
                        Sql += " WHERE "
                        Sql += "見積番号"
                        Sql += " ILIKE "
                        Sql += "'%"
                        Sql += QuoteNo(i)
                        Sql += "%'"
                    Else
                        Sql += " OR "
                        Sql += "見積番号"
                        Sql += " ILIKE "
                        Sql += "'%"
                        Sql += QuoteNo(i)
                        Sql += "%'"
                    End If
                Next
            End If

            Dim reccnt As Integer = 0
            ds = _db.selectDB(Sql, RS, reccnt)

            DgvMithd.Columns.Add("見積番号", "見積番号")
            DgvMithd.Columns.Add("見積番号枝番", "見積番号枝番")
            DgvMithd.Columns.Add("仕入区分", "仕入区分")
            DgvMithd.Columns.Add("メーカー", "メーカー")
            DgvMithd.Columns.Add("品名", "品名")
            DgvMithd.Columns.Add("型式", "型式")
            DgvMithd.Columns.Add("数量", "数量")
            DgvMithd.Columns.Add("単位", "単位")
            DgvMithd.Columns.Add("仕入先名称", "仕入先名称")
            DgvMithd.Columns.Add("仕入単価", "仕入単価")
            DgvMithd.Columns.Add("間接費", "間接費")
            DgvMithd.Columns.Add("仕入金額", "仕入金額")
            DgvMithd.Columns.Add("売単価", "売単価")
            DgvMithd.Columns.Add("売上金額", "売上金額")
            DgvMithd.Columns.Add("粗利額", "粗利額")
            DgvMithd.Columns.Add("粗利率", "粗利率")
            DgvMithd.Columns.Add("リードタイム", "リードタイム")
            DgvMithd.Columns.Add("備考", "備考")
            DgvMithd.Columns.Add("登録日", "登録日")

            DgvMithd.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvMithd.Rows.Add()
                DgvMithd.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)("見積番号")
                DgvMithd.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)("見積番号枝番")
                DgvMithd.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)("仕入区分")
                DgvMithd.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)("メーカー")
                DgvMithd.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)("品名")
                DgvMithd.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)("型式")
                DgvMithd.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)("数量")
                DgvMithd.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)("単位")
                DgvMithd.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)("仕入先名称")
                DgvMithd.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)("仕入単価")
                DgvMithd.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)("間接費")
                DgvMithd.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)("仕入金額")
                DgvMithd.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)("売単価")
                DgvMithd.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)("売上金額")
                DgvMithd.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)("粗利額")
                DgvMithd.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)("粗利率")
                DgvMithd.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)("リードタイム")
                DgvMithd.Rows(index).Cells(17).Value = ds.Tables(RS).Rows(index)("備考")
                DgvMithd.Rows(index).Cells(18).Value = ds.Tables(RS).Rows(index)("登録日")
            Next
        End If
    End Sub

    Private Sub BtnQuoteAdd_Click(sender As Object, e As EventArgs) Handles BtnQuoteAdd.Click
        Dim Status As String = "ADD"
        Dim openForm As Form = Nothing
        openForm = New Quote(_msgHd, _db, _langHd, Me, , Status)
        Me.Hide()
        openForm.Show(Me)
    End Sub

    Private Sub BtnQuoteEdit_Click(sender As Object, e As EventArgs) Handles BtnQuoteEdit.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvMithd.CurrentCell.RowIndex
        Dim No As String = DgvMithd.Rows(RowIdx).Cells(0).Value
        Dim Suffix As String = DgvMithd.Rows(RowIdx).Cells(1).Value

        Dim openForm As Form = Nothing
        openForm = New Quote(_msgHd, _db, _langHd, Me, No, Suffix)   '処理選択
        Me.Enabled = False
        Me.Hide()
        openForm.ShowDialog()

        DgvMithd.Rows.Clear()
        DgvMithd.Columns.Clear()

        Dim Sql As String = ""

        Sql += "SELECT "
        Sql += " *  "
        Sql += "FROM "
        Sql += "public"
        Sql += "."
        Sql += "t01_mithd"
        Sql += " WHERE "
        Sql += "見積有効期限"
        Sql += " >=  "
        Sql += "'"
        Sql += dtToday
        Sql += "'"

        Dim reccnt As Integer = 0
        ds = _db.selectDB(Sql, RS, reccnt)

        DgvMithd.Columns.Add("見積番号", "見積番号")
        DgvMithd.Columns.Add("見積番号枝番", "見積番号枝番")
        DgvMithd.Columns.Add("見積日", "見積日")
        DgvMithd.Columns.Add("見積有効期限", "見積有効期限")
        DgvMithd.Columns.Add("得意先コード", "得意先コード")
        DgvMithd.Columns.Add("得意先名", "得意先名")
        DgvMithd.Columns.Add("得意先郵便番号", "得意先郵便番号")
        DgvMithd.Columns.Add("得意先住所", "得意先住所")
        DgvMithd.Columns.Add("得意先電話番号", "得意先電話番号")
        DgvMithd.Columns.Add("得意先ＦＡＸ", "得意先ＦＡＸ")
        DgvMithd.Columns.Add("見積金額", "見積金額")
        DgvMithd.Columns.Add("仕入金額", "仕入金額")
        DgvMithd.Columns.Add("ＶＡＴ", "ＶＡＴ")
        DgvMithd.Columns.Add("粗利額", "粗利額")
        DgvMithd.Columns.Add("支払条件", "支払条件")
        DgvMithd.Columns.Add("営業担当者", "営業担当者")
        DgvMithd.Columns.Add("入力担当者", "入力担当者")
        DgvMithd.Columns.Add("備考", "備考")
        DgvMithd.Columns.Add("登録日", "登録日")
        DgvMithd.Columns.Add("更新者", "更新者")
        DgvMithd.Columns.Add("更新日", "更新日")

        DgvMithd.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvMithd.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvMithd.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvMithd.Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
            DgvMithd.Rows.Add()
            DgvMithd.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)("見積番号")
            DgvMithd.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)("見積番号枝番")
            DgvMithd.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)("見積日")
            DgvMithd.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)("見積有効期限")
            DgvMithd.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)("得意先コード")
            DgvMithd.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)("得意先名")
            DgvMithd.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)("得意先郵便番号")
            DgvMithd.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)("得意先住所")
            DgvMithd.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)("得意先電話番号")
            DgvMithd.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)("得意先ＦＡＸ")
            DgvMithd.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)("見積金額")
            DgvMithd.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)("仕入金額")
            DgvMithd.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)("ＶＡＴ")
            DgvMithd.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)("粗利額")
            DgvMithd.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)("支払条件")
            DgvMithd.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)("営業担当者")
            DgvMithd.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)("入力担当者")
            DgvMithd.Rows(index).Cells(17).Value = ds.Tables(RS).Rows(index)("備考")
            DgvMithd.Rows(index).Cells(18).Value = ds.Tables(RS).Rows(index)("登録日")
            DgvMithd.Rows(index).Cells(19).Value = ds.Tables(RS).Rows(index)("更新者")
            DgvMithd.Rows(index).Cells(20).Value = ds.Tables(RS).Rows(index)("更新日")
        Next
    End Sub

    Private Sub BtnQuoteClone_Click(sender As Object, e As EventArgs) Handles BtnQuoteClone.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvMithd.CurrentCell.RowIndex
        Dim No As String = DgvMithd.Rows(RowIdx).Cells(0).Value
        Dim Suffix As String = DgvMithd.Rows(RowIdx).Cells(1).Value
        Dim Status As String = "CLONE"
        Dim openForm As Form = Nothing
        openForm = New Quote(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
        Me.Enabled = False
        Me.Hide()
        openForm.ShowDialog()

        DgvMithd.Rows.Clear()
        DgvMithd.Columns.Clear()

        Dim Sql As String = ""

        Sql += "SELECT "
        Sql += " *  "
        Sql += "FROM "
        Sql += "public"
        Sql += "."
        Sql += "t01_mithd"
        Sql += " WHERE "
        Sql += "見積有効期限"
        Sql += " >=  "
        Sql += "'"
        Sql += dtToday
        Sql += "'"

        Dim reccnt As Integer = 0
        ds = _db.selectDB(Sql, RS, reccnt)

        DgvMithd.Columns.Add("見積番号", "見積番号")
        DgvMithd.Columns.Add("見積番号枝番", "見積番号枝番")
        DgvMithd.Columns.Add("見積日", "見積日")
        DgvMithd.Columns.Add("見積有効期限", "見積有効期限")
        DgvMithd.Columns.Add("得意先コード", "得意先コード")
        DgvMithd.Columns.Add("得意先名", "得意先名")
        DgvMithd.Columns.Add("得意先郵便番号", "得意先郵便番号")
        DgvMithd.Columns.Add("得意先住所", "得意先住所")
        DgvMithd.Columns.Add("得意先電話番号", "得意先電話番号")
        DgvMithd.Columns.Add("得意先ＦＡＸ", "得意先ＦＡＸ")
        DgvMithd.Columns.Add("見積金額", "見積金額")
        DgvMithd.Columns.Add("仕入金額", "仕入金額")
        DgvMithd.Columns.Add("ＶＡＴ", "ＶＡＴ")
        DgvMithd.Columns.Add("粗利額", "粗利額")
        DgvMithd.Columns.Add("支払条件", "支払条件")
        DgvMithd.Columns.Add("営業担当者", "営業担当者")
        DgvMithd.Columns.Add("入力担当者", "入力担当者")
        DgvMithd.Columns.Add("備考", "備考")
        DgvMithd.Columns.Add("登録日", "登録日")
        DgvMithd.Columns.Add("更新者", "更新者")
        DgvMithd.Columns.Add("更新日", "更新日")

        DgvMithd.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvMithd.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvMithd.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvMithd.Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
            DgvMithd.Rows.Add()
            DgvMithd.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)("見積番号")
            DgvMithd.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)("見積番号枝番")
            DgvMithd.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)("見積日")
            DgvMithd.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)("見積有効期限")
            DgvMithd.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)("得意先コード")
            DgvMithd.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)("得意先名")
            DgvMithd.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)("得意先郵便番号")
            DgvMithd.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)("得意先住所")
            DgvMithd.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)("得意先電話番号")
            DgvMithd.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)("得意先ＦＡＸ")
            DgvMithd.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)("見積金額")
            DgvMithd.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)("仕入金額")
            DgvMithd.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)("ＶＡＴ")
            DgvMithd.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)("粗利額")
            DgvMithd.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)("支払条件")
            DgvMithd.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)("営業担当者")
            DgvMithd.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)("入力担当者")
            DgvMithd.Rows(index).Cells(17).Value = ds.Tables(RS).Rows(index)("備考")
            DgvMithd.Rows(index).Cells(18).Value = ds.Tables(RS).Rows(index)("登録日")
            DgvMithd.Rows(index).Cells(19).Value = ds.Tables(RS).Rows(index)("更新者")
            DgvMithd.Rows(index).Cells(20).Value = ds.Tables(RS).Rows(index)("更新日")
        Next
    End Sub

    Private Sub BtnQuoteView_Click(sender As Object, e As EventArgs) Handles BtnQuoteView.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvMithd.CurrentCell.RowIndex
        Dim No As String = DgvMithd.Rows(RowIdx).Cells(0).Value
        Dim Suffix As String = DgvMithd.Rows(RowIdx).Cells(1).Value
        Dim Status As String = "VIEW"
        Dim openForm As Form = Nothing
        openForm = New Quote(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
        Me.Enabled = False
        Me.Hide()
        openForm.Show(Me)
    End Sub

    Private Sub BtnUnitPrice_Click(sender As Object, e As EventArgs) Handles BtnUnitPrice.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvMithd.CurrentCell.RowIndex
        Dim No As String = DgvMithd.Rows(RowIdx).Cells(0).Value
        Dim Suffix As String = DgvMithd.Rows(RowIdx).Cells(1).Value
        Dim Status As String = "PRICE"
        Dim openForm As Form = Nothing
        openForm = New Quote(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
        Me.Enabled = False
        Me.Hide()
        openForm.ShowDialog()

        DgvMithd.Rows.Clear()
        DgvMithd.Columns.Clear()

        Dim Sql As String = ""

        Sql += "SELECT "
        Sql += " *  "
        Sql += "FROM "
        Sql += "public"
        Sql += "."
        Sql += "t01_mithd"
        Sql += " WHERE "
        Sql += "見積有効期限"
        Sql += " >=  "
        Sql += "'"
        Sql += dtToday
        Sql += "'"

        Dim reccnt As Integer = 0
        ds = _db.selectDB(Sql, RS, reccnt)

        DgvMithd.Columns.Add("見積番号", "見積番号")
        DgvMithd.Columns.Add("見積番号枝番", "見積番号枝番")
        DgvMithd.Columns.Add("見積日", "見積日")
        DgvMithd.Columns.Add("見積有効期限", "見積有効期限")
        DgvMithd.Columns.Add("得意先コード", "得意先コード")
        DgvMithd.Columns.Add("得意先名", "得意先名")
        DgvMithd.Columns.Add("得意先郵便番号", "得意先郵便番号")
        DgvMithd.Columns.Add("得意先住所", "得意先住所")
        DgvMithd.Columns.Add("得意先電話番号", "得意先電話番号")
        DgvMithd.Columns.Add("得意先ＦＡＸ", "得意先ＦＡＸ")
        DgvMithd.Columns.Add("見積金額", "見積金額")
        DgvMithd.Columns.Add("仕入金額", "仕入金額")
        DgvMithd.Columns.Add("ＶＡＴ", "ＶＡＴ")
        DgvMithd.Columns.Add("粗利額", "粗利額")
        DgvMithd.Columns.Add("支払条件", "支払条件")
        DgvMithd.Columns.Add("営業担当者", "営業担当者")
        DgvMithd.Columns.Add("入力担当者", "入力担当者")
        DgvMithd.Columns.Add("備考", "備考")
        DgvMithd.Columns.Add("登録日", "登録日")
        DgvMithd.Columns.Add("更新者", "更新者")
        DgvMithd.Columns.Add("更新日", "更新日")

        DgvMithd.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvMithd.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvMithd.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvMithd.Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
            DgvMithd.Rows.Add()
            DgvMithd.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)("見積番号")
            DgvMithd.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)("見積番号枝番")
            DgvMithd.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)("見積日")
            DgvMithd.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)("見積有効期限")
            DgvMithd.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)("得意先コード")
            DgvMithd.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)("得意先名")
            DgvMithd.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)("得意先郵便番号")
            DgvMithd.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)("得意先住所")
            DgvMithd.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)("得意先電話番号")
            DgvMithd.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)("得意先ＦＡＸ")
            DgvMithd.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)("見積金額")
            DgvMithd.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)("仕入金額")
            DgvMithd.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)("ＶＡＴ")
            DgvMithd.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)("粗利額")
            DgvMithd.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)("支払条件")
            DgvMithd.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)("営業担当者")
            DgvMithd.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)("入力担当者")
            DgvMithd.Rows(index).Cells(17).Value = ds.Tables(RS).Rows(index)("備考")
            DgvMithd.Rows(index).Cells(18).Value = ds.Tables(RS).Rows(index)("登録日")
            DgvMithd.Rows(index).Cells(19).Value = ds.Tables(RS).Rows(index)("更新者")
            DgvMithd.Rows(index).Cells(20).Value = ds.Tables(RS).Rows(index)("更新日")
        Next
    End Sub

    Private Sub BtnQuoteSearch_Click(sender As Object, e As EventArgs) Handles BtnQuoteSearch.Click
        DgvMithd.Rows.Clear()
        DgvMithd.Columns.Clear()

        Dim count As Integer = 0
        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t01_mithd"
            If TxtCustomerName.Text = "" Then
            Else
                Sql += " WHERE "
                Sql += "得意先名"
                Sql += " ILIKE "
                Sql += "'%"
                Sql += TxtCustomerName.Text
                Sql += "%'"
                count += 1
            End If
            If TxtAddress.Text = "" Then
            Else
                If count > 0 Then
                    Sql += " AND "
                    Sql += "得意先住所"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtAddress.Text
                    Sql += "%'"
                Else
                    Sql += " WHERE "
                    Sql += "得意先住所"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtAddress.Text
                    Sql += "%'"
                    count += 1
                End If
            End If
            If TxtTel.Text = "" Then
            Else
                If count > 0 Then
                    Sql += " AND "
                    Sql += "得意先電話番号"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtTel.Text
                    Sql += "%'"
                Else
                    Sql += " WHERE "
                    Sql += "得意先電話番号"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtTel.Text
                    Sql += "%'"
                    count += 1
                End If
            End If
            If TxtCustomerCode.Text = "" Then
            Else
                If count > 0 Then
                    Sql += " AND "
                    Sql += "得意先コード"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtCustomerCode.Text
                    Sql += "%'"
                Else
                    Sql += " WHERE "
                    Sql += "得意先コード"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtCustomerCode.Text
                    Sql += "%'"
                    count += 1
                End If
            End If
            If TxtQuoteDate1.Text = "" Then
                If TxtQuoteDate2.Text = "" Then
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "見積日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtQuoteDate2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "見積日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtQuoteDate2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            Else
                If TxtQuoteDate2.Text = "" Then
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "見積日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtQuoteDate1.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "見積日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtQuoteDate1.Text
                        Sql += "'"
                        count += 1
                    End If
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "見積日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtQuoteDate1.Text
                        Sql += "' "
                        Sql += "AND  "
                        Sql += "見積日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtQuoteDate2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "見積日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtQuoteDate1.Text
                        Sql += "' "
                        Sql += "AND  "
                        Sql += "見積日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtQuoteDate2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            End If
            If TxtQuoteNo1.Text = "" Then
                If TxtQuoteNo2.Text = "" Then
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "見積番号"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtQuoteNo2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "見積番号"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtQuoteNo2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            Else
                If TxtQuoteNo2.Text = "" Then
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "見積番号"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtQuoteNo1.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "見積番号"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtQuoteNo1.Text
                        Sql += "'"
                        count += 1
                    End If
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "見積番号"
                        Sql += " >= "
                        Sql += "'"
                        Sql += TxtQuoteNo1.Text
                        Sql += "' "
                        Sql += "AND "
                        Sql += "見積番号"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtQuoteNo2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "見積番号"
                        Sql += " >= "
                        Sql += "'"
                        Sql += TxtQuoteNo1.Text
                        Sql += "' "
                        Sql += "AND  "
                        Sql += "見積番号"
                        Sql += " <= "
                        Sql += "'"
                        Sql += TxtQuoteNo2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            End If
            If TxtSales.Text = "" Then
            Else
                If count > 0 Then
                    Sql += " AND "
                    Sql += "営業担当者"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtSales.Text
                    Sql += "%'"
                Else
                    Sql += " WHERE "
                    Sql += "営業担当者"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtSales.Text
                    Sql += "%'"
                    count += 1
                End If
            End If

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            DgvMithd.Columns.Add("見積番号", "見積番号")
            DgvMithd.Columns.Add("見積番号枝番", "見積番号枝番")
            DgvMithd.Columns.Add("見積日", "見積日")
            DgvMithd.Columns.Add("見積有効期限", "見積有効期限")
            DgvMithd.Columns.Add("得意先コード", "得意先コード")
            DgvMithd.Columns.Add("得意先名", "得意先名")
            DgvMithd.Columns.Add("得意先郵便番号", "得意先郵便番号")
            DgvMithd.Columns.Add("得意先住所", "得意先住所")
            DgvMithd.Columns.Add("得意先電話番号", "得意先電話番号")
            DgvMithd.Columns.Add("得意先ＦＡＸ", "得意先ＦＡＸ")
            DgvMithd.Columns.Add("見積金額", "見積金額")
            DgvMithd.Columns.Add("仕入金額", "仕入金額")
            DgvMithd.Columns.Add("ＶＡＴ", "ＶＡＴ")
            DgvMithd.Columns.Add("粗利額", "粗利額")
            DgvMithd.Columns.Add("支払条件", "支払条件")
            DgvMithd.Columns.Add("営業担当者", "営業担当者")
            DgvMithd.Columns.Add("入力担当者", "入力担当者")
            DgvMithd.Columns.Add("備考", "備考")
            DgvMithd.Columns.Add("登録日", "登録日")
            DgvMithd.Columns.Add("更新者", "更新者")
            DgvMithd.Columns.Add("更新日", "更新日")

            DgvMithd.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvMithd.Rows.Add()
                DgvMithd.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)("見積番号")
                DgvMithd.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)("見積番号枝番")
                DgvMithd.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)("見積日")
                DgvMithd.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)("見積有効期限")
                DgvMithd.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)("得意先コード")
                DgvMithd.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)("得意先名")
                DgvMithd.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)("得意先郵便番号")
                DgvMithd.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)("得意先住所")
                DgvMithd.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)("得意先電話番号")
                DgvMithd.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)("得意先ＦＡＸ")
                DgvMithd.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)("見積金額")
                DgvMithd.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)("仕入金額")
                DgvMithd.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)("ＶＡＴ")
                DgvMithd.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)("粗利額")
                DgvMithd.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)("支払条件")
                DgvMithd.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)("営業担当者")
                DgvMithd.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)("入力担当者")
                DgvMithd.Rows(index).Cells(17).Value = ds.Tables(RS).Rows(index)("備考")
                DgvMithd.Rows(index).Cells(18).Value = ds.Tables(RS).Rows(index)("登録日")
                DgvMithd.Rows(index).Cells(19).Value = ds.Tables(RS).Rows(index)("更新者")
                DgvMithd.Rows(index).Cells(20).Value = ds.Tables(RS).Rows(index)("更新日")
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Sub BtnOrder_Click(sender As Object, e As EventArgs) Handles BtnOrderAndPurchase.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvMithd.CurrentCell.RowIndex
        Dim No As String = DgvMithd.Rows(RowIdx).Cells(0).Value
        Dim Suffix As String = DgvMithd.Rows(RowIdx).Cells(1).Value

        Dim openForm As Form = Nothing
        openForm = New Cymn(_msgHd, _db, _langHd, Me, No, Suffix)   '処理選択
        Me.Hide()
        openForm.Show(Me)
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Dim dtToday As DateTime = DateTime.Now

        Dim Sql1 As String = ""
        Sql1 = ""
        Sql1 += "UPDATE "
        Sql1 += "Public."
        Sql1 += "t01_mithd "
        Sql1 += "SET "
        Sql1 += "取消区分"
        Sql1 += " = '"
        Sql1 += "1"
        Sql1 += "', "
        Sql1 += "取消日"
        Sql1 += " = '"
        Sql1 += dtToday
        Sql1 += "', "
        Sql1 += "更新日"
        Sql1 += " = '"
        Sql1 += dtToday
        Sql1 += "', "
        Sql1 += "更新者"
        Sql1 += " = '"
        Sql1 += frmC01F10_Login.loginValue.TantoNM
        Sql1 += "' "
        Sql1 += "WHERE"
        Sql1 += " 会社コード"
        Sql1 += "='"
        Sql1 += frmC01F10_Login.loginValue.BumonNM
        Sql1 += "'"
        Sql1 += " AND"
        Sql1 += " 見積番号"
        Sql1 += "='"
        Sql1 += DgvMithd.Rows(DgvMithd.CurrentCell.RowIndex).Cells("見積番号").Value
        Sql1 += "' "
        Sql1 += " AND"
        Sql1 += " 見積番号枝番"
        Sql1 += "='"
        Sql1 += DgvMithd.Rows(DgvMithd.CurrentCell.RowIndex).Cells("見積番号枝番").Value
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
        Sql1 += "ＶＡＴ"
        Sql1 += ", "
        Sql1 += "取消日"
        Sql1 += ", "
        Sql1 += "取消区分"
        Sql1 += ", "
        Sql1 += "登録日"
        Sql1 += ", "
        Sql1 += "受注日"
        Sql1 += ", "
        Sql1 += "更新日"
        Sql1 += ", "
        Sql1 += "更新者"
        _db.executeDB(Sql1)
    End Sub

    Private Sub ChkExpired_CheckedChanged(sender As Object, e As EventArgs) Handles ChkExpired.CheckedChanged
        If ChkExpired.Checked = True Then
            DgvMithd.Rows.Clear()
            DgvMithd.Columns.Clear()

            Dim Sql As String = ""

            Sql += "SELECT "
            Sql += " *  "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t01_mithd"

            Dim reccnt As Integer = 0
            ds = _db.selectDB(Sql, RS, reccnt)

            DgvMithd.Columns.Add("見積番号", "見積番号")
            DgvMithd.Columns.Add("見積番号枝番", "見積番号枝番")
            DgvMithd.Columns.Add("見積日", "見積日")
            DgvMithd.Columns.Add("見積有効期限", "見積有効期限")
            DgvMithd.Columns.Add("得意先コード", "得意先コード")
            DgvMithd.Columns.Add("得意先名", "得意先名")
            DgvMithd.Columns.Add("得意先郵便番号", "得意先郵便番号")
            DgvMithd.Columns.Add("得意先住所", "得意先住所")
            DgvMithd.Columns.Add("得意先電話番号", "得意先電話番号")
            DgvMithd.Columns.Add("得意先ＦＡＸ", "得意先ＦＡＸ")
            DgvMithd.Columns.Add("見積金額", "見積金額")
            DgvMithd.Columns.Add("仕入金額", "仕入金額")
            DgvMithd.Columns.Add("ＶＡＴ", "ＶＡＴ")
            DgvMithd.Columns.Add("粗利額", "粗利額")
            DgvMithd.Columns.Add("支払条件", "支払条件")
            DgvMithd.Columns.Add("営業担当者", "営業担当者")
            DgvMithd.Columns.Add("入力担当者", "入力担当者")
            DgvMithd.Columns.Add("備考", "備考")
            DgvMithd.Columns.Add("登録日", "登録日")
            DgvMithd.Columns.Add("更新者", "更新者")
            DgvMithd.Columns.Add("更新日", "更新日")

            DgvMithd.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvMithd.Rows.Add()
                DgvMithd.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)("見積番号")
                DgvMithd.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)("見積番号枝番")
                DgvMithd.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)("見積日")
                DgvMithd.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)("見積有効期限")
                DgvMithd.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)("得意先コード")
                DgvMithd.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)("得意先名")
                DgvMithd.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)("得意先郵便番号")
                DgvMithd.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)("得意先住所")
                DgvMithd.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)("得意先電話番号")
                DgvMithd.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)("得意先ＦＡＸ")
                DgvMithd.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)("見積金額")
                DgvMithd.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)("仕入金額")
                DgvMithd.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)("ＶＡＴ")
                DgvMithd.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)("粗利額")
                DgvMithd.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)("支払条件")
                DgvMithd.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)("営業担当者")
                DgvMithd.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)("入力担当者")
                DgvMithd.Rows(index).Cells(17).Value = ds.Tables(RS).Rows(index)("備考")
                DgvMithd.Rows(index).Cells(18).Value = ds.Tables(RS).Rows(index)("登録日")
                DgvMithd.Rows(index).Cells(19).Value = ds.Tables(RS).Rows(index)("更新者")
                DgvMithd.Rows(index).Cells(20).Value = ds.Tables(RS).Rows(index)("更新日")
            Next
        Else
            DgvMithd.Rows.Clear()
            DgvMithd.Columns.Clear()

            Dim Sql As String = ""

            Sql += "SELECT "
            Sql += " *  "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t01_mithd"
            Sql += " WHERE "
            Sql += "見積有効期限"
            Sql += " >=  "
            Sql += "'"
            Sql += dtToday
            Sql += "'"

            Dim reccnt As Integer = 0
            ds = _db.selectDB(Sql, RS, reccnt)

            DgvMithd.Columns.Add("見積番号", "見積番号")
            DgvMithd.Columns.Add("見積番号枝番", "見積番号枝番")
            DgvMithd.Columns.Add("見積日", "見積日")
            DgvMithd.Columns.Add("見積有効期限", "見積有効期限")
            DgvMithd.Columns.Add("得意先コード", "得意先コード")
            DgvMithd.Columns.Add("得意先名", "得意先名")
            DgvMithd.Columns.Add("得意先郵便番号", "得意先郵便番号")
            DgvMithd.Columns.Add("得意先住所", "得意先住所")
            DgvMithd.Columns.Add("得意先電話番号", "得意先電話番号")
            DgvMithd.Columns.Add("得意先ＦＡＸ", "得意先ＦＡＸ")
            DgvMithd.Columns.Add("見積金額", "見積金額")
            DgvMithd.Columns.Add("仕入金額", "仕入金額")
            DgvMithd.Columns.Add("ＶＡＴ", "ＶＡＴ")
            DgvMithd.Columns.Add("粗利額", "粗利額")
            DgvMithd.Columns.Add("支払条件", "支払条件")
            DgvMithd.Columns.Add("営業担当者", "営業担当者")
            DgvMithd.Columns.Add("入力担当者", "入力担当者")
            DgvMithd.Columns.Add("備考", "備考")
            DgvMithd.Columns.Add("登録日", "登録日")
            DgvMithd.Columns.Add("更新者", "更新者")
            DgvMithd.Columns.Add("更新日", "更新日")

            DgvMithd.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvMithd.Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvMithd.Rows.Add()
                DgvMithd.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)("見積番号")
                DgvMithd.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)("見積番号枝番")
                DgvMithd.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)("見積日")
                DgvMithd.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)("見積有効期限")
                DgvMithd.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)("得意先コード")
                DgvMithd.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)("得意先名")
                DgvMithd.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)("得意先郵便番号")
                DgvMithd.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)("得意先住所")
                DgvMithd.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)("得意先電話番号")
                DgvMithd.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)("得意先ＦＡＸ")
                DgvMithd.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)("見積金額")
                DgvMithd.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)("仕入金額")
                DgvMithd.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)("ＶＡＴ")
                DgvMithd.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)("粗利額")
                DgvMithd.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)("支払条件")
                DgvMithd.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)("営業担当者")
                DgvMithd.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)("入力担当者")
                DgvMithd.Rows(index).Cells(17).Value = ds.Tables(RS).Rows(index)("備考")
                DgvMithd.Rows(index).Cells(18).Value = ds.Tables(RS).Rows(index)("登録日")
                DgvMithd.Rows(index).Cells(19).Value = ds.Tables(RS).Rows(index)("更新者")
                DgvMithd.Rows(index).Cells(20).Value = ds.Tables(RS).Rows(index)("更新日")
            Next
        End If

    End Sub

    Private Sub BtnOrder_Click_1(sender As Object, e As EventArgs) Handles BtnOrder.Click
        Dim RowIdx As Integer
        RowIdx = DgvMithd.CurrentCell.RowIndex
        Dim No As String = DgvMithd.Rows(RowIdx).Cells(0).Value
        Dim Suffix As String = DgvMithd.Rows(RowIdx).Cells(1).Value
        Dim Status As String = "ADD"
        Dim openForm As Form = Nothing
        openForm = New Order(_msgHd, _db, _langHd, Me, No, Suffix, Status)   '処理選択
        openForm.Show(Me)
    End Sub
End Class