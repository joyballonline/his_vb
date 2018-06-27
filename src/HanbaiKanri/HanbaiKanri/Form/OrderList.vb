Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class OrderList
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
    Private CompanyCode As String = ""
    Private OrderNo As String()


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
            Sql += "受注番号, "
            Sql += "受注番号枝番, "
            Sql += "受注日, "
            Sql += "見積番号, "
            Sql += "見積番号枝番, "
            Sql += "見積日, "
            Sql += "見積有効期限, "
            Sql += "得意先コード, "
            Sql += "得意先名, "
            Sql += "得意先郵便番号, "
            Sql += "得意先住所, "
            Sql += "得意先電話番号, "
            Sql += "得意先ＦＡＸ, "
            Sql += "得意先担当者名, "
            Sql += "得意先担当者役職, "
            Sql += "支払条件, "
            Sql += "ＶＡＴ, "
            Sql += "見積金額, "
            Sql += "仕入金額, "
            Sql += "粗利額, "
            Sql += "営業担当者, "
            Sql += "入力担当者, "
            Sql += "備考, "
            Sql += "登録日 "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t10_cymnhd"

            Dim reccnt As Integer = 0
            ds = _db.selectDB(Sql, RS, reccnt)
            DgvCymnhd.Columns.Add("受注番号", "受注番号")
            DgvCymnhd.Columns.Add("受注番号枝番", "受注番号枝番")
            DgvCymnhd.Columns.Add("受注日", "受注日")
            DgvCymnhd.Columns.Add("見積番号", "見積番号")
            DgvCymnhd.Columns.Add("見積番号枝番", "見積番号枝番")
            DgvCymnhd.Columns.Add("見積日", "見積日")
            DgvCymnhd.Columns.Add("見積有効期限", "見積有効期限")
            DgvCymnhd.Columns.Add("得意先コード", "得意先コード")
            DgvCymnhd.Columns.Add("得意先名", "得意先名")
            DgvCymnhd.Columns.Add("得意先郵便番号", "得意先郵便番号")
            DgvCymnhd.Columns.Add("得意先住所", "得意先住所")
            DgvCymnhd.Columns.Add("得意先電話番号", "得意先電話番号")
            DgvCymnhd.Columns.Add("得意先ＦＡＸ", "得意先ＦＡＸ")
            DgvCymnhd.Columns.Add("得意先担当者名", "得意先担当者名")
            DgvCymnhd.Columns.Add("得意先担当者役職", "得意先担当者役職")
            DgvCymnhd.Columns.Add("ＶＡＴ", "ＶＡＴ")
            DgvCymnhd.Columns.Add("受注金額", "受注金額")
            DgvCymnhd.Columns.Add("仕入金額", "仕入金額")
            DgvCymnhd.Columns.Add("粗利額", "粗利額")
            DgvCymnhd.Columns.Add("支払条件", "支払条件")
            DgvCymnhd.Columns.Add("営業担当者", "営業担当者")
            DgvCymnhd.Columns.Add("入力担当者", "入力担当者")
            DgvCymnhd.Columns.Add("備考", "備考")
            DgvCymnhd.Columns.Add("登録日", "登録日")

            DgvCymnhd.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvCymnhd.Rows.Add()
                DgvCymnhd.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)("受注番号")
                DgvCymnhd.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)("受注番号枝番")
                DgvCymnhd.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)("受注日")
                DgvCymnhd.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)("見積番号")
                DgvCymnhd.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)("見積番号枝番")
                DgvCymnhd.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)("見積日")
                DgvCymnhd.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)("見積有効期限")
                DgvCymnhd.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)("得意先コード")
                DgvCymnhd.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)("得意先名")
                DgvCymnhd.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)("得意先郵便番号")
                DgvCymnhd.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)("得意先住所")
                DgvCymnhd.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)("得意先電話番号")
                DgvCymnhd.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)("得意先ＦＡＸ")
                DgvCymnhd.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)("得意先担当者名")
                DgvCymnhd.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)("得意先担当者役職")
                DgvCymnhd.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)("ＶＡＴ")
                DgvCymnhd.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)("見積金額")
                DgvCymnhd.Rows(index).Cells(17).Value = ds.Tables(RS).Rows(index)("仕入金額")
                DgvCymnhd.Rows(index).Cells(18).Value = ds.Tables(RS).Rows(index)("粗利額")
                DgvCymnhd.Rows(index).Cells(19).Value = ds.Tables(RS).Rows(index)("支払条件")
                DgvCymnhd.Rows(index).Cells(20).Value = ds.Tables(RS).Rows(index)("営業担当者")
                DgvCymnhd.Rows(index).Cells(21).Value = ds.Tables(RS).Rows(index)("入力担当者")
                DgvCymnhd.Rows(index).Cells(22).Value = ds.Tables(RS).Rows(index)("備考")
                DgvCymnhd.Rows(index).Cells(23).Value = ds.Tables(RS).Rows(index)("登録日")
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

    Private Sub RbtnDetails_CheckedChanged(sender As Object, e As EventArgs) Handles RbtnDetails.CheckedChanged
        DgvCymnhd.Rows.Clear()
        DgvCymnhd.Columns.Clear()

        If RbtnSlip.Checked Then
            Dim Sql As String = ""
            Sql += "SELECT "
            Sql += " * "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t10_cymnhd"
            If OrderNo IsNot Nothing Then
                For i As Integer = 0 To OrderNo.Length - 1
                    If i = 0 Then
                        Sql += " WHERE "
                        Sql += "受注番号"
                        Sql += " ILIKE "
                        Sql += "'%"
                        Sql += OrderNo(i)
                        Sql += "%'"
                    Else
                        Sql += " OR "
                        Sql += "受注番号"
                        Sql += " ILIKE "
                        Sql += "'%"
                        Sql += OrderNo(i)
                        Sql += "%'"
                    End If
                Next
            End If



            Dim reccnt As Integer = 0
            ds = _db.selectDB(Sql, RS, reccnt)

            DgvCymnhd.Columns.Add("受注番号", "受注番号")
            DgvCymnhd.Columns.Add("受注番号枝番", "受注番号枝番")
            DgvCymnhd.Columns.Add("受注日", "受注日")
            DgvCymnhd.Columns.Add("見積番号", "見積番号")
            DgvCymnhd.Columns.Add("見積番号枝番", "見積番号枝番")
            DgvCymnhd.Columns.Add("見積日", "見積日")
            DgvCymnhd.Columns.Add("見積有効期限", "見積有効期限")
            DgvCymnhd.Columns.Add("得意先コード", "得意先コード")
            DgvCymnhd.Columns.Add("得意先名", "得意先名")
            DgvCymnhd.Columns.Add("得意先郵便番号", "得意先郵便番号")
            DgvCymnhd.Columns.Add("得意先住所", "得意先住所")
            DgvCymnhd.Columns.Add("得意先電話番号", "得意先電話番号")
            DgvCymnhd.Columns.Add("得意先ＦＡＸ", "得意先ＦＡＸ")
            DgvCymnhd.Columns.Add("得意先担当者名", "得意先担当者名")
            DgvCymnhd.Columns.Add("得意先担当者役職", "得意先担当者役職")
            DgvCymnhd.Columns.Add("ＶＡＴ", "ＶＡＴ")
            DgvCymnhd.Columns.Add("受注金額", "受注金額")
            DgvCymnhd.Columns.Add("仕入金額", "仕入金額")
            DgvCymnhd.Columns.Add("粗利額", "粗利額")
            DgvCymnhd.Columns.Add("支払条件", "支払条件")
            DgvCymnhd.Columns.Add("営業担当者", "営業担当者")
            DgvCymnhd.Columns.Add("入力担当者", "入力担当者")
            DgvCymnhd.Columns.Add("備考", "備考")
            DgvCymnhd.Columns.Add("登録日", "登録日")

            DgvCymnhd.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvCymnhd.Rows.Add()
                DgvCymnhd.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)("受注番号")
                DgvCymnhd.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)("受注番号枝番")
                DgvCymnhd.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)("受注日")
                DgvCymnhd.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)("見積番号")
                DgvCymnhd.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)("見積番号枝番")
                DgvCymnhd.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)("見積日")
                DgvCymnhd.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)("見積有効期限")
                DgvCymnhd.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)("得意先コード")
                DgvCymnhd.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)("得意先名")
                DgvCymnhd.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)("得意先郵便番号")
                DgvCymnhd.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)("得意先住所")
                DgvCymnhd.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)("得意先電話番号")
                DgvCymnhd.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)("得意先ＦＡＸ")
                DgvCymnhd.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)("得意先担当者名")
                DgvCymnhd.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)("得意先担当者役職")
                DgvCymnhd.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)("ＶＡＴ")
                DgvCymnhd.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)("見積金額")
                DgvCymnhd.Rows(index).Cells(17).Value = ds.Tables(RS).Rows(index)("仕入金額")
                DgvCymnhd.Rows(index).Cells(18).Value = ds.Tables(RS).Rows(index)("粗利額")
                DgvCymnhd.Rows(index).Cells(19).Value = ds.Tables(RS).Rows(index)("支払条件")
                DgvCymnhd.Rows(index).Cells(20).Value = ds.Tables(RS).Rows(index)("営業担当者")
                DgvCymnhd.Rows(index).Cells(21).Value = ds.Tables(RS).Rows(index)("入力担当者")
                DgvCymnhd.Rows(index).Cells(22).Value = ds.Tables(RS).Rows(index)("備考")
                DgvCymnhd.Rows(index).Cells(23).Value = ds.Tables(RS).Rows(index)("登録日")
            Next
        Else
            Dim Sql As String = ""

            Sql += "SELECT "
            Sql += " * "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t11_cymndt"

            If OrderNo IsNot Nothing Then
                For i As Integer = 0 To OrderNo.Length - 1
                    If i = 0 Then
                        Sql += " WHERE "
                        Sql += "受注番号"
                        Sql += " ILIKE "
                        Sql += "'%"
                        Sql += OrderNo(i)
                        Sql += "%'"
                    Else
                        Sql += " OR "
                        Sql += "受注番号"
                        Sql += " ILIKE "
                        Sql += "'%"
                        Sql += OrderNo(i)
                        Sql += "%'"
                    End If
                Next
            End If

            Dim reccnt As Integer = 0
            ds = _db.selectDB(Sql, RS, reccnt)

            DgvCymnhd.Columns.Add("受注番号", "受注番号")
            DgvCymnhd.Columns.Add("受注番号枝番", "受注番号枝番")
            DgvCymnhd.Columns.Add("行番号", "行番号")
            DgvCymnhd.Columns.Add("仕入区分", "仕入区分")
            DgvCymnhd.Columns.Add("メーカー", "メーカー")
            DgvCymnhd.Columns.Add("品名", "品名")
            DgvCymnhd.Columns.Add("型式", "型式")
            DgvCymnhd.Columns.Add("仕入先名", "仕入先名")
            DgvCymnhd.Columns.Add("仕入値", "仕入値")
            DgvCymnhd.Columns.Add("受注数量", "受注数量")
            DgvCymnhd.Columns.Add("売上数量", "売上数量")
            DgvCymnhd.Columns.Add("受注残数", "受注残数")
            DgvCymnhd.Columns.Add("単位", "単位")
            DgvCymnhd.Columns.Add("間接費", "間接費")
            DgvCymnhd.Columns.Add("売単価", "売単価")
            DgvCymnhd.Columns.Add("売上金額", "売上金額")
            DgvCymnhd.Columns.Add("粗利額", "粗利額")
            DgvCymnhd.Columns.Add("粗利率", "粗利率")
            DgvCymnhd.Columns.Add("リードタイム", "リードタイム")
            DgvCymnhd.Columns.Add("出庫数", "出庫数")
            DgvCymnhd.Columns.Add("未出庫数", "未出庫数")
            DgvCymnhd.Columns.Add("備考", "備考")
            DgvCymnhd.Columns.Add("更新者", "更新者")
            DgvCymnhd.Columns.Add("登録日", "登録日")

            DgvCymnhd.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns(18).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns(19).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns(20).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvCymnhd.Rows.Add()
                DgvCymnhd.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)("受注番号")
                DgvCymnhd.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)("受注番号枝番")
                DgvCymnhd.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)("行番号")
                DgvCymnhd.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)("仕入区分")
                DgvCymnhd.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)("メーカー")
                DgvCymnhd.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)("品名")
                DgvCymnhd.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)("型式")
                DgvCymnhd.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)("仕入先名")
                DgvCymnhd.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)("仕入値")
                DgvCymnhd.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)("受注数量")
                DgvCymnhd.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)("売上数量")
                DgvCymnhd.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)("受注残数")
                DgvCymnhd.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)("単位")
                DgvCymnhd.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)("間接費")
                DgvCymnhd.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)("売単価")
                DgvCymnhd.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)("売上金額")
                DgvCymnhd.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)("粗利額")
                DgvCymnhd.Rows(index).Cells(17).Value = ds.Tables(RS).Rows(index)("粗利率")
                DgvCymnhd.Rows(index).Cells(18).Value = ds.Tables(RS).Rows(index)("リードタイム")
                DgvCymnhd.Rows(index).Cells(19).Value = ds.Tables(RS).Rows(index)("出庫数")
                DgvCymnhd.Rows(index).Cells(20).Value = ds.Tables(RS).Rows(index)("未出庫数")
                DgvCymnhd.Rows(index).Cells(21).Value = ds.Tables(RS).Rows(index)("備考")
                DgvCymnhd.Rows(index).Cells(22).Value = ds.Tables(RS).Rows(index)("更新者")
                DgvCymnhd.Rows(index).Cells(23).Value = ds.Tables(RS).Rows(index)("登録日")
            Next
        End If
    End Sub

    Private Sub BtnQuoteSearch_Click(sender As Object, e As EventArgs) Handles BtnOrderSearch.Click
        DgvCymnhd.Rows.Clear()
        DgvCymnhd.Columns.Clear()

        Dim count As Integer = 0
        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t10_cymnhd"
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
            If TxtOrderDate1.Text = "" Then
                If TxtOrderDate2.Text = "" Then
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "受注日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtOrderDate2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "受注日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtOrderDate2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            Else
                If TxtOrderDate2.Text = "" Then
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "受注日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtOrderDate1.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "受注日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtOrderDate1.Text
                        Sql += "'"
                        count += 1
                    End If
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "受注日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtOrderDate1.Text
                        Sql += "' "
                        Sql += "AND  "
                        Sql += "受注日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtOrderDate2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "受注日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtOrderDate1.Text
                        Sql += "' "
                        Sql += "AND  "
                        Sql += "受注日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtOrderDate2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            End If
            If TxtOrderNo1.Text = "" Then
                If TxtOrderNo2.Text = "" Then
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "受注番号"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtOrderNo2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "受注番号"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtOrderNo2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            Else
                If TxtOrderNo2.Text = "" Then
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "受注番号"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtOrderNo1.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "受注番号"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtOrderNo1.Text
                        Sql += "'"
                        count += 1
                    End If
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "受注番号"
                        Sql += " >= "
                        Sql += "'"
                        Sql += TxtOrderNo1.Text
                        Sql += "' "
                        Sql += "AND "
                        Sql += "受注番号"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtOrderNo2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "受注番号"
                        Sql += " >= "
                        Sql += "'"
                        Sql += TxtOrderNo1.Text
                        Sql += "' "
                        Sql += "AND  "
                        Sql += "受注番号"
                        Sql += " <= "
                        Sql += "'"
                        Sql += TxtOrderNo2.Text
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

            DgvCymnhd.Columns.Add("受注番号", "受注番号")
            DgvCymnhd.Columns.Add("受注番号枝番", "受注番号枝番")
            DgvCymnhd.Columns.Add("受注日", "受注日")
            DgvCymnhd.Columns.Add("見積番号", "見積番号")
            DgvCymnhd.Columns.Add("見積番号枝番", "見積番号枝番")
            DgvCymnhd.Columns.Add("見積日", "見積日")
            DgvCymnhd.Columns.Add("見積有効期限", "見積有効期限")
            DgvCymnhd.Columns.Add("得意先コード", "得意先コード")
            DgvCymnhd.Columns.Add("得意先名", "得意先名")
            DgvCymnhd.Columns.Add("得意先郵便番号", "得意先郵便番号")
            DgvCymnhd.Columns.Add("得意先住所", "得意先住所")
            DgvCymnhd.Columns.Add("得意先電話番号", "得意先電話番号")
            DgvCymnhd.Columns.Add("得意先ＦＡＸ", "得意先ＦＡＸ")
            DgvCymnhd.Columns.Add("得意先担当者名", "得意先担当者名")
            DgvCymnhd.Columns.Add("得意先担当者役職", "得意先担当者役職")
            DgvCymnhd.Columns.Add("ＶＡＴ", "ＶＡＴ")
            DgvCymnhd.Columns.Add("受注金額", "受注金額")
            DgvCymnhd.Columns.Add("仕入金額", "仕入金額")
            DgvCymnhd.Columns.Add("粗利額", "粗利額")
            DgvCymnhd.Columns.Add("支払条件", "支払条件")
            DgvCymnhd.Columns.Add("営業担当者", "営業担当者")
            DgvCymnhd.Columns.Add("入力担当者", "入力担当者")
            DgvCymnhd.Columns.Add("備考", "備考")
            DgvCymnhd.Columns.Add("登録日", "登録日")

            DgvCymnhd.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            Dim tmp As Integer = ds.Tables(RS).Rows.Count - 1
            ReDim OrderNo(tmp)

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvCymnhd.Rows.Add()
                DgvCymnhd.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)("受注番号")
                OrderNo(index) = ds.Tables(RS).Rows(index)("受注番号")
                DgvCymnhd.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)("受注番号枝番")
                DgvCymnhd.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)("受注日")
                DgvCymnhd.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)("見積番号")
                DgvCymnhd.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)("見積番号枝番")
                DgvCymnhd.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)("見積日")
                DgvCymnhd.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)("見積有効期限")
                DgvCymnhd.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)("得意先コード")
                DgvCymnhd.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)("得意先名")
                DgvCymnhd.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)("得意先郵便番号")
                DgvCymnhd.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)("得意先住所")
                DgvCymnhd.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)("得意先電話番号")
                DgvCymnhd.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)("得意先ＦＡＸ")
                DgvCymnhd.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)("得意先担当者名")
                DgvCymnhd.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)("得意先担当者役職")
                DgvCymnhd.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)("ＶＡＴ")
                DgvCymnhd.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)("見積金額")
                DgvCymnhd.Rows(index).Cells(17).Value = ds.Tables(RS).Rows(index)("仕入金額")
                DgvCymnhd.Rows(index).Cells(18).Value = ds.Tables(RS).Rows(index)("粗利額")
                DgvCymnhd.Rows(index).Cells(19).Value = ds.Tables(RS).Rows(index)("支払条件")
                DgvCymnhd.Rows(index).Cells(20).Value = ds.Tables(RS).Rows(index)("営業担当者")
                DgvCymnhd.Rows(index).Cells(21).Value = ds.Tables(RS).Rows(index)("入力担当者")
                DgvCymnhd.Rows(index).Cells(22).Value = ds.Tables(RS).Rows(index)("備考")
                DgvCymnhd.Rows(index).Cells(23).Value = ds.Tables(RS).Rows(index)("登録日")
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Sub BtnOrderEdit_Click(sender As Object, e As EventArgs) Handles BtnOrderEdit.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvCymnhd.CurrentCell.RowIndex
        Dim No As String = DgvCymnhd.Rows(RowIdx).Cells(0).Value
        Dim Suffix As String = DgvCymnhd.Rows(RowIdx).Cells(1).Value

        Dim openForm As Form = Nothing
        openForm = New Order(_msgHd, _db, No, Suffix)   '処理選択
        openForm.Show(Me)
    End Sub

    Private Sub BtnOrderView_Click(sender As Object, e As EventArgs) Handles BtnOrderView.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvCymnhd.CurrentCell.RowIndex
        Dim No As String = DgvCymnhd.Rows(RowIdx).Cells(0).Value
        Dim Suffix As String = DgvCymnhd.Rows(RowIdx).Cells(1).Value
        Dim Status As String = "VIEW"

        Dim openForm As Form = Nothing
        openForm = New Order(_msgHd, _db, No, Suffix, Status)   '処理選択
        openForm.Show(Me)
    End Sub
End Class