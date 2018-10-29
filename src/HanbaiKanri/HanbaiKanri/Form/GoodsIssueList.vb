Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class GoodsIssueList
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
    'Private _gh As UtilDataGridViewHandler
    Private _init As Boolean                             '初期処理済フラグ
    Private CompanyCode As String = ""
    Private GoodsNo As String()
    Private GoodsIssueStatus As String = ""


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
                   ByRef prmRefStatus As String)
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        GoodsIssueStatus = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub
    Private Sub OrderListLoad(Optional ByRef prmRefStatus As String = "")
        Dim Status As String = prmRefStatus
        Dim Sql As String = ""

        If Status = "EXCLUSION" Then
            Try
                Sql += "SELECT "
                Sql += "* "
                Sql += "FROM "
                Sql += "public"
                Sql += "."
                Sql += "t44_shukohd"
                Sql += " WHERE "
                Sql += "取消区分"
                Sql += " = "
                Sql += "'"
                Sql += "0"
                Sql += "'"

                Dim reccnt As Integer = 0
                ds = _db.selectDB(Sql, RS, reccnt)
                DgvCymnhd.Columns.Add("出庫番号", "出庫番号")
                DgvCymnhd.Columns.Add("出庫日", "出庫日")
                DgvCymnhd.Columns.Add("受注番号", "受注番号")
                DgvCymnhd.Columns.Add("受注番号枝番", "受注番号枝番")
                DgvCymnhd.Columns.Add("客先番号", "客先番号")
                DgvCymnhd.Columns.Add("得意先コード", "得意先コード")
                DgvCymnhd.Columns.Add("得意先名", "得意先名")
                DgvCymnhd.Columns.Add("得意先郵便番号", "得意先郵便番号")
                DgvCymnhd.Columns.Add("得意先住所", "得意先住所")
                DgvCymnhd.Columns.Add("得意先電話番号", "得意先電話番号")
                DgvCymnhd.Columns.Add("得意先ＦＡＸ", "得意先ＦＡＸ")
                DgvCymnhd.Columns.Add("得意先担当者名", "得意先担当者名")
                DgvCymnhd.Columns.Add("得意先担当者役職", "得意先担当者役職")
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
                    DgvCymnhd.Rows(index).Cells("出庫番号").Value = ds.Tables(RS).Rows(index)("出庫番号")
                    DgvCymnhd.Rows(index).Cells("出庫日").Value = ds.Tables(RS).Rows(index)("出庫日")
                    DgvCymnhd.Rows(index).Cells("受注番号").Value = ds.Tables(RS).Rows(index)("受注番号")
                    DgvCymnhd.Rows(index).Cells("受注番号枝番").Value = ds.Tables(RS).Rows(index)("受注番号枝番")
                    DgvCymnhd.Rows(index).Cells("客先番号").Value = ds.Tables(RS).Rows(index)("客先番号")
                    DgvCymnhd.Rows(index).Cells("得意先コード").Value = ds.Tables(RS).Rows(index)("得意先コード")
                    DgvCymnhd.Rows(index).Cells("得意先名").Value = ds.Tables(RS).Rows(index)("得意先名")
                    DgvCymnhd.Rows(index).Cells("得意先郵便番号").Value = ds.Tables(RS).Rows(index)("得意先郵便番号")
                    DgvCymnhd.Rows(index).Cells("得意先住所").Value = ds.Tables(RS).Rows(index)("得意先住所")
                    DgvCymnhd.Rows(index).Cells("得意先電話番号").Value = ds.Tables(RS).Rows(index)("得意先電話番号")
                    DgvCymnhd.Rows(index).Cells("得意先ＦＡＸ").Value = ds.Tables(RS).Rows(index)("得意先ＦＡＸ")
                    DgvCymnhd.Rows(index).Cells("得意先担当者名").Value = ds.Tables(RS).Rows(index)("得意先担当者名")
                    DgvCymnhd.Rows(index).Cells("得意先担当者役職").Value = ds.Tables(RS).Rows(index)("得意先担当者役職")
                    DgvCymnhd.Rows(index).Cells("営業担当者").Value = ds.Tables(RS).Rows(index)("営業担当者")
                    DgvCymnhd.Rows(index).Cells("入力担当者").Value = ds.Tables(RS).Rows(index)("入力担当者")
                    DgvCymnhd.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
                    DgvCymnhd.Rows(index).Cells("登録日").Value = ds.Tables(RS).Rows(index)("登録日")
                Next

            Catch ue As UsrDefException
                ue.dspMsg()
                Throw ue
            Catch ex As Exception
                'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
            End Try
        Else
            Try
                Sql += "SELECT "
                Sql += "* "
                Sql += "FROM "
                Sql += "public"
                Sql += "."
                Sql += "t44_shukohd"

                Dim reccnt As Integer = 0
                ds = _db.selectDB(Sql, RS, reccnt)
                DgvCymnhd.Columns.Add("出庫番号", "出庫番号")
                DgvCymnhd.Columns.Add("出庫日", "出庫日")
                DgvCymnhd.Columns.Add("受注番号", "受注番号")
                DgvCymnhd.Columns.Add("受注番号枝番", "受注番号枝番")
                DgvCymnhd.Columns.Add("客先番号", "客先番号")
                DgvCymnhd.Columns.Add("得意先コード", "得意先コード")
                DgvCymnhd.Columns.Add("得意先名", "得意先名")
                DgvCymnhd.Columns.Add("得意先郵便番号", "得意先郵便番号")
                DgvCymnhd.Columns.Add("得意先住所", "得意先住所")
                DgvCymnhd.Columns.Add("得意先電話番号", "得意先電話番号")
                DgvCymnhd.Columns.Add("得意先ＦＡＸ", "得意先ＦＡＸ")
                DgvCymnhd.Columns.Add("得意先担当者名", "得意先担当者名")
                DgvCymnhd.Columns.Add("得意先担当者役職", "得意先担当者役職")
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
                    DgvCymnhd.Rows(index).Cells("出庫番号").Value = ds.Tables(RS).Rows(index)("出庫番号")
                    DgvCymnhd.Rows(index).Cells("出庫日").Value = ds.Tables(RS).Rows(index)("出庫日")
                    DgvCymnhd.Rows(index).Cells("受注番号").Value = ds.Tables(RS).Rows(index)("受注番号")
                    DgvCymnhd.Rows(index).Cells("受注番号枝番").Value = ds.Tables(RS).Rows(index)("受注番号枝番")
                    DgvCymnhd.Rows(index).Cells("客先番号").Value = ds.Tables(RS).Rows(index)("客先番号")
                    DgvCymnhd.Rows(index).Cells("得意先コード").Value = ds.Tables(RS).Rows(index)("得意先コード")
                    DgvCymnhd.Rows(index).Cells("得意先名").Value = ds.Tables(RS).Rows(index)("得意先名")
                    DgvCymnhd.Rows(index).Cells("得意先郵便番号").Value = ds.Tables(RS).Rows(index)("得意先郵便番号")
                    DgvCymnhd.Rows(index).Cells("得意先住所").Value = ds.Tables(RS).Rows(index)("得意先住所")
                    DgvCymnhd.Rows(index).Cells("得意先電話番号").Value = ds.Tables(RS).Rows(index)("得意先電話番号")
                    DgvCymnhd.Rows(index).Cells("得意先ＦＡＸ").Value = ds.Tables(RS).Rows(index)("得意先ＦＡＸ")
                    DgvCymnhd.Rows(index).Cells("得意先担当者名").Value = ds.Tables(RS).Rows(index)("得意先担当者名")
                    DgvCymnhd.Rows(index).Cells("得意先担当者役職").Value = ds.Tables(RS).Rows(index)("得意先担当者役職")
                    DgvCymnhd.Rows(index).Cells("営業担当者").Value = ds.Tables(RS).Rows(index)("営業担当者")
                    DgvCymnhd.Rows(index).Cells("入力担当者").Value = ds.Tables(RS).Rows(index)("入力担当者")
                    DgvCymnhd.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
                    DgvCymnhd.Rows(index).Cells("登録日").Value = ds.Tables(RS).Rows(index)("登録日")
                Next

            Catch ue As UsrDefException
                ue.dspMsg()
                Throw ue
            Catch ex As Exception
                'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
            End Try
        End If


    End Sub

    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GoodsIssueStatus = "CANCEL" Then
            LblMode.Text = "取消モード"
            BtnSalesCancel.Visible = True
            BtnSalesCancel.Location = New Point(997, 509)
        ElseIf GoodsIssueStatus = "VIEW" Then
            LblMode.Text = "参照モード"
            BtnSalesView.Visible = True
            BtnSalesView.Location = New Point(997, 509)
        End If

        Dim Status As String = "EXCLUSION"
        OrderListLoad(Status)
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        Dim openForm As Form = Nothing
        openForm = New frmC01F30_Menu(_msgHd, _langHd, _db)
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
            Sql += "t44_shukohd"

            If GoodsNo IsNot Nothing Then
                For i As Integer = 0 To GoodsNo.Length - 1
                    If i = 0 Then
                        Sql += " WHERE "
                        Sql += "出庫番号"
                        Sql += " ILIKE "
                        Sql += "'"
                        Sql += GoodsNo(i)
                        Sql += "'"
                    Else
                        Sql += " OR "
                        Sql += "出庫番号"
                        Sql += " ILIKE "
                        Sql += "'"
                        Sql += GoodsNo(i)
                        Sql += "%'"
                    End If
                Next
            End If

            Dim reccnt As Integer = 0
            ds = _db.selectDB(Sql, RS, reccnt)

            DgvCymnhd.Columns.Add("出庫番号", "出庫番号")
            DgvCymnhd.Columns.Add("出庫日", "出庫日")
            DgvCymnhd.Columns.Add("受注番号", "受注番号")
            DgvCymnhd.Columns.Add("受注番号枝番", "受注番号枝番")
            DgvCymnhd.Columns.Add("客先番号", "客先番号")
            DgvCymnhd.Columns.Add("得意先コード", "得意先コード")
            DgvCymnhd.Columns.Add("得意先名", "得意先名")
            DgvCymnhd.Columns.Add("得意先郵便番号", "得意先郵便番号")
            DgvCymnhd.Columns.Add("得意先住所", "得意先住所")
            DgvCymnhd.Columns.Add("得意先電話番号", "得意先電話番号")
            DgvCymnhd.Columns.Add("得意先ＦＡＸ", "得意先ＦＡＸ")
            DgvCymnhd.Columns.Add("得意先担当者名", "得意先担当者名")
            DgvCymnhd.Columns.Add("得意先担当者役職", "得意先担当者役職")
            DgvCymnhd.Columns.Add("営業担当者", "営業担当者")
            DgvCymnhd.Columns.Add("入力担当者", "入力担当者")
            DgvCymnhd.Columns.Add("備考", "備考")
            DgvCymnhd.Columns.Add("登録日", "登録日")

            

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvCymnhd.Rows.Add()
                DgvCymnhd.Rows(index).Cells("出庫番号").Value = ds.Tables(RS).Rows(index)("出庫番号")
                DgvCymnhd.Rows(index).Cells("出庫日").Value = ds.Tables(RS).Rows(index)("出庫日")
                DgvCymnhd.Rows(index).Cells("受注番号").Value = ds.Tables(RS).Rows(index)("受注番号")
                DgvCymnhd.Rows(index).Cells("受注番号枝番").Value = ds.Tables(RS).Rows(index)("受注番号枝番")
                DgvCymnhd.Rows(index).Cells("客先番号").Value = ds.Tables(RS).Rows(index)("客先番号")
                DgvCymnhd.Rows(index).Cells("得意先コード").Value = ds.Tables(RS).Rows(index)("得意先コード")
                DgvCymnhd.Rows(index).Cells("得意先名").Value = ds.Tables(RS).Rows(index)("得意先名")
                DgvCymnhd.Rows(index).Cells("得意先郵便番号").Value = ds.Tables(RS).Rows(index)("得意先郵便番号")
                DgvCymnhd.Rows(index).Cells("得意先住所").Value = ds.Tables(RS).Rows(index)("得意先住所")
                DgvCymnhd.Rows(index).Cells("得意先電話番号").Value = ds.Tables(RS).Rows(index)("得意先電話番号")
                DgvCymnhd.Rows(index).Cells("得意先ＦＡＸ").Value = ds.Tables(RS).Rows(index)("得意先ＦＡＸ")
                DgvCymnhd.Rows(index).Cells("得意先担当者名").Value = ds.Tables(RS).Rows(index)("得意先担当者名")
                DgvCymnhd.Rows(index).Cells("得意先担当者役職").Value = ds.Tables(RS).Rows(index)("得意先担当者役職")
                DgvCymnhd.Rows(index).Cells("営業担当者").Value = ds.Tables(RS).Rows(index)("営業担当者")
                DgvCymnhd.Rows(index).Cells("入力担当者").Value = ds.Tables(RS).Rows(index)("入力担当者")
                DgvCymnhd.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
                DgvCymnhd.Rows(index).Cells("登録日").Value = ds.Tables(RS).Rows(index)("登録日")
            Next

        Else
            Dim Sql As String = ""

            Sql += "SELECT "
            Sql += " * "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t45_shukodt"

            If GoodsNo IsNot Nothing Then
                For i As Integer = 0 To GoodsNo.Length - 1
                    If i = 0 Then
                        Sql += " WHERE "
                        Sql += "出庫番号"
                        Sql += " ILIKE "
                        Sql += "'"
                        Sql += GoodsNo(i)
                        Sql += "'"
                    Else
                        Sql += " OR "
                        Sql += "出庫番号"
                        Sql += " ILIKE "
                        Sql += "'"
                        Sql += GoodsNo(i)
                        Sql += "%'"
                    End If
                Next
            End If

            Dim reccnt As Integer = 0
            ds = _db.selectDB(Sql, RS, reccnt)

            DgvCymnhd.Columns.Add("出庫番号", "出庫番号")
            DgvCymnhd.Columns.Add("行番号", "行番号")
            DgvCymnhd.Columns.Add("仕入区分", "仕入区分")
            DgvCymnhd.Columns.Add("メーカー", "メーカー")
            DgvCymnhd.Columns.Add("品名", "品名")
            DgvCymnhd.Columns.Add("型式", "型式")
            DgvCymnhd.Columns.Add("仕入先名", "仕入先名")
            DgvCymnhd.Columns.Add("出庫数量", "出庫数量")
            DgvCymnhd.Columns.Add("単位", "単位")
            DgvCymnhd.Columns.Add("売単価", "売単価")
            DgvCymnhd.Columns.Add("備考", "備考")
            DgvCymnhd.Columns.Add("更新者", "更新者")

            DgvCymnhd.Columns("出庫数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns("売単価").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvCymnhd.Rows.Add()
                DgvCymnhd.Rows(index).Cells("出庫番号").Value = ds.Tables(RS).Rows(index)("出庫番号")
                DgvCymnhd.Rows(index).Cells("行番号").Value = ds.Tables(RS).Rows(index)("行番号")
                If ds.Tables(RS).Rows(index)("仕入区分") = 1 Then
                    DgvCymnhd.Rows(index).Cells("仕入区分").Value = "仕入"
                ElseIf ds.Tables(RS).Rows(index)("仕入区分") = 2 Then
                    DgvCymnhd.Rows(index).Cells("仕入区分").Value = "在庫"
                Else
                    DgvCymnhd.Rows(index).Cells("仕入区分").Value = "サービス"
                End If
                DgvCymnhd.Rows(index).Cells("メーカー").Value = ds.Tables(RS).Rows(index)("メーカー")
                DgvCymnhd.Rows(index).Cells("品名").Value = ds.Tables(RS).Rows(index)("品名")
                DgvCymnhd.Rows(index).Cells("型式").Value = ds.Tables(RS).Rows(index)("型式")
                DgvCymnhd.Rows(index).Cells("仕入先名").Value = ds.Tables(RS).Rows(index)("仕入先名")
                DgvCymnhd.Rows(index).Cells("出庫数量").Value = ds.Tables(RS).Rows(index)("出庫数量")
                DgvCymnhd.Rows(index).Cells("単位").Value = ds.Tables(RS).Rows(index)("単位")
                DgvCymnhd.Rows(index).Cells("売単価").Value = ds.Tables(RS).Rows(index)("売単価")
                DgvCymnhd.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
                DgvCymnhd.Rows(index).Cells("更新者").Value = ds.Tables(RS).Rows(index)("更新者")
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
            Sql += "t44_shukohd"
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
            If TxtSalesDate1.Text = "" Then
                If TxtSalesDate2.Text = "" Then
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "出庫日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtSalesDate2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "出庫日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtSalesDate2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            Else
                If TxtSalesDate2.Text = "" Then
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "出庫日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtSalesDate1.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "出庫日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtSalesDate1.Text
                        Sql += "'"
                        count += 1
                    End If
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "出庫日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtSalesDate1.Text
                        Sql += "' "
                        Sql += "AND  "
                        Sql += "出庫日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtSalesDate2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "出庫日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtSalesDate1.Text
                        Sql += "' "
                        Sql += "AND  "
                        Sql += "出庫日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtSalesDate2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            End If
            If TxtGoodsNo1.Text = "" Then
                If TxtGoodsNo2.Text = "" Then
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "出庫番号"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtGoodsNo2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "出庫番号"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtGoodsNo2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            Else
                If TxtGoodsNo2.Text = "" Then
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "出庫番号"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtGoodsNo1.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "出庫番号"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtGoodsNo1.Text
                        Sql += "'"
                        count += 1
                    End If
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "出庫番号"
                        Sql += " >= "
                        Sql += "'"
                        Sql += TxtGoodsNo1.Text
                        Sql += "' "
                        Sql += "AND "
                        Sql += "出庫番号"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtGoodsNo2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "出庫番号"
                        Sql += " >= "
                        Sql += "'"
                        Sql += TxtGoodsNo1.Text
                        Sql += "' "
                        Sql += "AND  "
                        Sql += "出庫番号"
                        Sql += " <= "
                        Sql += "'"
                        Sql += TxtGoodsNo2.Text
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
            If TxtCustomerPO.Text = "" Then
            Else
                If count > 0 Then
                    Sql += " AND "
                    Sql += "客先番号"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtCustomerPO.Text
                    Sql += "%'"
                Else
                    Sql += " WHERE "
                    Sql += "客先番号"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtCustomerPO.Text
                    Sql += "%'"
                    count += 1
                End If
            End If

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            DgvCymnhd.Columns.Add("出庫番号", "出庫番号")
            DgvCymnhd.Columns.Add("出庫日", "出庫日")
            DgvCymnhd.Columns.Add("受注番号", "受注番号")
            DgvCymnhd.Columns.Add("受注番号枝番", "受注番号枝番")
            DgvCymnhd.Columns.Add("客先番号", "客先番号")
            DgvCymnhd.Columns.Add("得意先コード", "得意先コード")
            DgvCymnhd.Columns.Add("得意先名", "得意先名")
            DgvCymnhd.Columns.Add("得意先郵便番号", "得意先郵便番号")
            DgvCymnhd.Columns.Add("得意先住所", "得意先住所")
            DgvCymnhd.Columns.Add("得意先電話番号", "得意先電話番号")
            DgvCymnhd.Columns.Add("得意先ＦＡＸ", "得意先ＦＡＸ")
            DgvCymnhd.Columns.Add("得意先担当者名", "得意先担当者名")
            DgvCymnhd.Columns.Add("得意先担当者役職", "得意先担当者役職")
            DgvCymnhd.Columns.Add("営業担当者", "営業担当者")
            DgvCymnhd.Columns.Add("入力担当者", "入力担当者")
            DgvCymnhd.Columns.Add("備考", "備考")
            DgvCymnhd.Columns.Add("登録日", "登録日")

            Dim tmp As Integer = ds.Tables(RS).Rows.Count - 1
            ReDim GoodsNo(tmp)

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvCymnhd.Rows.Add()
                DgvCymnhd.Rows(index).Cells("出庫番号").Value = ds.Tables(RS).Rows(index)("出庫番号")
                GoodsNo(index) = ds.Tables(RS).Rows(index)("出庫番号")
                DgvCymnhd.Rows(index).Cells("出庫日").Value = ds.Tables(RS).Rows(index)("出庫日")
                DgvCymnhd.Rows(index).Cells("受注番号").Value = ds.Tables(RS).Rows(index)("受注番号")
                DgvCymnhd.Rows(index).Cells("受注番号枝番").Value = ds.Tables(RS).Rows(index)("受注番号枝番")
                DgvCymnhd.Rows(index).Cells("客先番号").Value = ds.Tables(RS).Rows(index)("客先番号")
                DgvCymnhd.Rows(index).Cells("得意先コード").Value = ds.Tables(RS).Rows(index)("得意先コード")
                DgvCymnhd.Rows(index).Cells("得意先名").Value = ds.Tables(RS).Rows(index)("得意先名")
                DgvCymnhd.Rows(index).Cells("得意先郵便番号").Value = ds.Tables(RS).Rows(index)("得意先郵便番号")
                DgvCymnhd.Rows(index).Cells("得意先住所").Value = ds.Tables(RS).Rows(index)("得意先住所")
                DgvCymnhd.Rows(index).Cells("得意先電話番号").Value = ds.Tables(RS).Rows(index)("得意先電話番号")
                DgvCymnhd.Rows(index).Cells("得意先ＦＡＸ").Value = ds.Tables(RS).Rows(index)("得意先ＦＡＸ")
                DgvCymnhd.Rows(index).Cells("得意先担当者名").Value = ds.Tables(RS).Rows(index)("得意先担当者名")
                DgvCymnhd.Rows(index).Cells("得意先担当者役職").Value = ds.Tables(RS).Rows(index)("得意先担当者役職")
                DgvCymnhd.Rows(index).Cells("営業担当者").Value = ds.Tables(RS).Rows(index)("営業担当者")
                DgvCymnhd.Rows(index).Cells("入力担当者").Value = ds.Tables(RS).Rows(index)("入力担当者")
                DgvCymnhd.Rows(index).Cells("備考").Value = ds.Tables(RS).Rows(index)("備考")
            Next
        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Sub BtnSalesCancel_Click(sender As Object, e As EventArgs) Handles BtnSalesCancel.Click
        Dim dtNow As DateTime = DateTime.Now
        Dim Sql As String = ""
        Sql = ""
        Sql += "UPDATE "
        Sql += "Public."
        Sql += "t44_shukohd "
        Sql += "SET "

        Sql += "取消区分"
        Sql += " = '"
        Sql += "1"
        Sql += "', "
        Sql += "取消日"
        Sql += " = '"
        Sql += dtNow
        Sql += "', "
        Sql += "更新日"
        Sql += " = '"
        Sql += dtNow
        Sql += "', "
        Sql += "更新者"
        Sql += " = '"
        Sql += frmC01F10_Login.loginValue.TantoNM
        Sql += " ' "

        Sql += "WHERE"
        Sql += " 会社コード"
        Sql += "='"
        Sql += frmC01F10_Login.loginValue.BumonNM
        Sql += "'"
        Sql += " AND"
        Sql += " 出庫番号"
        Sql += "='"
        Sql += DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("出庫番号").Value
        Sql += "' "
        Sql += "RETURNING 会社コード"
        Sql += ", "
        Sql += "出庫番号"
        Sql += ", "
        Sql += "受注番号"
        Sql += ", "
        Sql += "受注番号枝番"
        Sql += ", "
        Sql += "見積番号"
        Sql += ", "
        Sql += "見積番号枝番"
        Sql += ", "
        Sql += "得意先コード"
        Sql += ", "
        Sql += "得意先名"
        Sql += ", "
        Sql += "得意先郵便番号"
        Sql += ", "
        Sql += "得意先住所"
        Sql += ", "
        Sql += "得意先電話番号"
        Sql += ", "
        Sql += "得意先ＦＡＸ"
        Sql += ", "
        Sql += "得意先担当者役職"
        Sql += ", "
        Sql += "得意先担当者名"
        Sql += ", "
        Sql += "営業担当者"
        Sql += ", "
        Sql += "入力担当者"
        Sql += ", "
        Sql += "備考"
        Sql += ", "
        Sql += "取消日"
        Sql += ", "
        Sql += "取消区分"
        Sql += ", "
        Sql += "出庫日"
        Sql += ", "
        Sql += "更新日"
        Sql += ", "
        Sql += "更新者"
        Dim result As DialogResult = MessageBox.Show("出庫を取り消しますか？",
                                             "質問",
                                             MessageBoxButtons.YesNoCancel,
                                             MessageBoxIcon.Exclamation,
                                             MessageBoxDefaultButton.Button2)

        If result = DialogResult.Yes Then
            _db.executeDB(Sql)
            DgvCymnhd.Rows.Clear()
            DgvCymnhd.Columns.Clear()
            Dim Status As String = "EXCLUSION"
            OrderListLoad(Status)
        ElseIf result = DialogResult.No Then

        ElseIf result = DialogResult.Cancel Then

        End If

    End Sub

    Private Sub ChkCancelData_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCancelData.CheckedChanged
        DgvCymnhd.Rows.Clear()
        DgvCymnhd.Columns.Clear()

        If ChkCancelData.Checked = False Then
            Dim Status As String = "EXCLUSION"
            OrderListLoad(Status)
        Else
            OrderListLoad()
        End If
    End Sub

    Private Sub BtnSalesView_Click(sender As Object, e As EventArgs) Handles BtnSalesView.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvCymnhd.CurrentCell.RowIndex
        Dim No As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号").Value
        Dim Suffix As String = DgvCymnhd.Rows(RowIdx).Cells("受注番号枝番").Value
        Dim Status As String = "VIEW"

        Dim openForm As Form = Nothing
        openForm = New GoodsIssue(_msgHd, _db, _langHd, No, Suffix, Status)   '処理選択
        openForm.Show(Me)
    End Sub
End Class