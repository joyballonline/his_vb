Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
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

    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += "会社コード, "
            Sql += "見積番号, "
            Sql += "見積番号枝番, "
            Sql += "見積日, "
            Sql += "見積有効期限, "
            Sql += "得意先コード, "
            Sql += "得意先名, "
            Sql += "得意先担当者名, "
            Sql += "得意先担当者役職, "
            Sql += "支払条件, "
            Sql += "仕入金額, "
            Sql += "見積金額, "
            Sql += "営業担当者, "
            Sql += "入力担当者, "
            Sql += "備考, "
            Sql += "登録日 "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t01_mithd"

            Dim reccnt As Integer = 0
            ds = _db.selectDB(Sql, RS, reccnt)

            DgvMithd.Columns.Add("得意先名", "得意先名")
            DgvMithd.Columns.Add("見積番号", "見積番号")
            DgvMithd.Columns.Add("見積番号枝番", "見積番号枝番")
            DgvMithd.Columns.Add("見積有効期限", "見積有効期限")
            DgvMithd.Columns.Add("見積金額", "見積金額")
            DgvMithd.Columns.Add("仕入金額", "仕入金額")
            DgvMithd.Columns.Add("支払条件", "支払条件")
            DgvMithd.Columns.Add("営業担当者", "営業担当者")
            DgvMithd.Columns.Add("入力担当者", "入力担当者")
            DgvMithd.Columns.Add("備考", "備考")
            DgvMithd.Columns.Add("登録日", "登録日")

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvMithd.Rows.Add()
                DgvMithd.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)(6)        '会社コード
                DgvMithd.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)(1)        '言語コード
                DgvMithd.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)(2)        '氏名
                DgvMithd.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)(4)      '略名
                DgvMithd.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)(10)      '備考
                DgvMithd.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)(11)      '無効フラグ
                DgvMithd.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)(9)      '更新者
                DgvMithd.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)(12)      '更新日
                DgvMithd.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)(13)        '会社コード
                DgvMithd.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)(14)        '言語コード
                DgvMithd.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)(15)        '氏名
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
        Dim openForm As Form = Nothing
        openForm = New frmC01F30_Menu(_msgHd, _db)
        openForm.Show()
        Me.Close()
    End Sub

    Private Sub BtnQuoteAdd_Click(sender As Object, e As EventArgs) Handles BtnQuoteAdd.Click
        Dim openForm As Form = Nothing
        openForm = New Quote(_msgHd, _db)
        openForm.Show(Me)
    End Sub

    Private Sub RbtnDetails_CheckedChanged(sender As Object, e As EventArgs) Handles RbtnDetails.CheckedChanged
        DgvMithd.Rows.Clear()
        DgvMithd.Columns.Clear()

        If RbtnSlip.Checked Then
            Dim Sql As String = ""
            Sql += "SELECT "
            Sql += "会社コード, "
            Sql += "見積番号, "
            Sql += "見積番号枝番, "
            Sql += "見積日, "
            Sql += "見積有効期限, "
            Sql += "得意先コード, "
            Sql += "得意先名, "
            Sql += "得意先担当者名, "
            Sql += "得意先担当者役職, "
            Sql += "支払条件, "
            Sql += "仕入金額, "
            Sql += "見積金額, "
            Sql += "営業担当者, "
            Sql += "入力担当者, "
            Sql += "備考, "
            Sql += "登録日 "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t01_mithd"

            Dim reccnt As Integer = 0
            ds = _db.selectDB(Sql, RS, reccnt)

            DgvMithd.Columns.Add("得意先名", "得意先名")
            DgvMithd.Columns.Add("見積番号", "見積番号")
            DgvMithd.Columns.Add("見積番号枝番", "見積番号枝番")
            DgvMithd.Columns.Add("見積有効期限", "見積有効期限")
            DgvMithd.Columns.Add("見積金額", "見積金額")
            DgvMithd.Columns.Add("仕入金額", "仕入金額")
            DgvMithd.Columns.Add("支払条件", "支払条件")
            DgvMithd.Columns.Add("営業担当者", "営業担当者")
            DgvMithd.Columns.Add("入力担当者", "入力担当者")
            DgvMithd.Columns.Add("備考", "備考")
            DgvMithd.Columns.Add("登録日", "登録日")

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvMithd.Rows.Add()
                DgvMithd.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)(6)        '会社コード
                DgvMithd.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)(1)        '言語コード
                DgvMithd.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)(2)        '氏名
                DgvMithd.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)(4)      '略名
                DgvMithd.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)(10)      '備考
                DgvMithd.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)(11)      '無効フラグ
                DgvMithd.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)(9)      '更新者
                DgvMithd.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)(12)      '更新日
                DgvMithd.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)(13)        '会社コード
                DgvMithd.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)(14)        '言語コード
                DgvMithd.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)(15)        '氏名
            Next
        Else
            Dim Sql As String = ""

            Sql += "SELECT "
            Sql += "会社コード, "
            Sql += "見積番号, "
            Sql += "見積番号枝番, "
            Sql += "仕入区分, "
            Sql += "メーカー, "
            Sql += "品名, "
            Sql += "型式, "
            Sql += "数量, "
            Sql += "単位, "
            Sql += "仕入先名称, "
            Sql += "仕入単価, "
            Sql += "間接費, "
            Sql += "仕入金額, "
            Sql += "売単価, "
            Sql += "売上金額, "
            Sql += "粗利額, "
            Sql += "粗利率, "
            Sql += "リードタイム, "
            Sql += "備考, "
            Sql += "登録日 "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t02_mitdt"

            Dim reccnt As Integer = 0
            ds = _db.selectDB(Sql, RS, reccnt)

            DgvMithd.Columns.Add("仕入先名称", "仕入先名称")
            DgvMithd.Columns.Add("見積番号", "見積番号")
            DgvMithd.Columns.Add("見積番号枝番", "見積番号枝番")
            DgvMithd.Columns.Add("仕入区分", "仕入区分")
            DgvMithd.Columns.Add("メーカー", "メーカー")
            DgvMithd.Columns.Add("品名", "品名")
            DgvMithd.Columns.Add("型式", "型式")
            DgvMithd.Columns.Add("数量", "数量")
            DgvMithd.Columns.Add("単位", "単位")
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

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvMithd.Rows.Add()
                DgvMithd.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)(9)        '会社コード
                DgvMithd.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)(1)        '言語コード
                DgvMithd.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)(2)        '氏名
                DgvMithd.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)(3)      '略名
                DgvMithd.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)(4)      '備考
                DgvMithd.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)(5)      '無効フラグ
                DgvMithd.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)(6)      '更新者
                DgvMithd.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)(7)      '更新日
                DgvMithd.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)(8)        '会社コード
                DgvMithd.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)(10)        '言語コード
                DgvMithd.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)(11)        '氏名
                DgvMithd.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)(12)        '言語コード
                DgvMithd.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)(13)        '氏名DgvMithd.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)(10)        '言語コード
                DgvMithd.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)(14)        '氏名
                DgvMithd.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)(15)        '言語コード
                DgvMithd.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)(16)        '氏名
                DgvMithd.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)(17)        '氏名
                DgvMithd.Rows(index).Cells(17).Value = ds.Tables(RS).Rows(index)(18)        '言語コード
                DgvMithd.Rows(index).Cells(18).Value = ds.Tables(RS).Rows(index)(19)        '言語コード
            Next
        End If
    End Sub

    Private Sub BtnQuoteEdit_Click(sender As Object, e As EventArgs) Handles BtnQuoteEdit.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvMithd.CurrentCell.RowIndex
        Dim No As String = DgvMithd.Rows(RowIdx).Cells(1).Value
        Dim Suffix As String = DgvMithd.Rows(RowIdx).Cells(2).Value

        Dim openForm As Form = Nothing
        openForm = New Quote(_msgHd, _db, No, Suffix)   '処理選択
        openForm.Show(Me)
    End Sub

    Private Sub BtnQuoteClone_Click(sender As Object, e As EventArgs) Handles BtnQuoteClone.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvMithd.CurrentCell.RowIndex
        Dim No As String = DgvMithd.Rows(RowIdx).Cells(1).Value
        Dim Suffix As String = DgvMithd.Rows(RowIdx).Cells(2).Value
        Dim Status As String = "CLONE"
        Dim openForm As Form = Nothing
        openForm = New Quote(_msgHd, _db, No, Suffix, Status)   '処理選択
        openForm.Show(Me)
    End Sub

    Private Sub BtnQuoteView_Click(sender As Object, e As EventArgs) Handles BtnQuoteView.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvMithd.CurrentCell.RowIndex
        Dim No As String = DgvMithd.Rows(RowIdx).Cells(1).Value
        Dim Suffix As String = DgvMithd.Rows(RowIdx).Cells(2).Value
        Dim Status As String = "VIEW"
        Dim openForm As Form = Nothing
        openForm = New Quote(_msgHd, _db, No, Suffix, Status)   '処理選択
        openForm.Show(Me)
    End Sub

    Private Sub BtnUnitPrice_Click(sender As Object, e As EventArgs) Handles BtnUnitPrice.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvMithd.CurrentCell.RowIndex
        Dim No As String = DgvMithd.Rows(RowIdx).Cells(1).Value
        Dim Suffix As String = DgvMithd.Rows(RowIdx).Cells(2).Value
        Dim Status As String = "PRICE"
        Dim openForm As Form = Nothing
        openForm = New Quote(_msgHd, _db, No, Suffix, Status)   '処理選択
        openForm.Show(Me)
    End Sub
End Class