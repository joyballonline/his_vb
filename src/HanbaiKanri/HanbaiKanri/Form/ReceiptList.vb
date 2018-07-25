Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class ReceiptList
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
    Private ReceiptNo As String()
    Private ReceiptStatus As String = ""


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
        ReceiptStatus = prmRefStatus
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
                Sql += "t42_nyukohd"
                Sql += " WHERE "
                Sql += "取消区分"
                Sql += " = "
                Sql += "'"
                Sql += "0"
                Sql += "'"

                Dim reccnt As Integer = 0
                ds = _db.selectDB(Sql, RS, reccnt)
                DgvCymnhd.Columns.Add("入庫番号", "入庫番号")
                DgvCymnhd.Columns.Add("入庫日", "入庫日")
                DgvCymnhd.Columns.Add("発注番号", "発注番号")
                DgvCymnhd.Columns.Add("発注番号枝番", "発注番号枝番")
                DgvCymnhd.Columns.Add("仕入先コード", "仕入先コード")
                DgvCymnhd.Columns.Add("仕入先名", "仕入先名")
                DgvCymnhd.Columns.Add("仕入先郵便番号", "仕入先郵便番号")
                DgvCymnhd.Columns.Add("仕入先住所", "仕入先住所")
                DgvCymnhd.Columns.Add("仕入先電話番号", "仕入先電話番号")
                DgvCymnhd.Columns.Add("仕入先ＦＡＸ", "仕入先ＦＡＸ")
                DgvCymnhd.Columns.Add("仕入先担当者名", "仕入先担当者名")
                DgvCymnhd.Columns.Add("仕入先担当者役職", "仕入先担当者役職")
                DgvCymnhd.Columns.Add("営業担当者", "営業担当者")
                DgvCymnhd.Columns.Add("入力担当者", "入力担当者")
                DgvCymnhd.Columns.Add("備考", "備考")
                DgvCymnhd.Columns.Add("登録日", "登録日")

                'DgvCymnhd.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                'DgvCymnhd.Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                'DgvCymnhd.Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                'DgvCymnhd.Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                    DgvCymnhd.Rows.Add()
                    DgvCymnhd.Rows(index).Cells("入庫番号").Value = ds.Tables(RS).Rows(index)("入庫番号")
                    DgvCymnhd.Rows(index).Cells("入庫日").Value = ds.Tables(RS).Rows(index)("入庫日")
                    DgvCymnhd.Rows(index).Cells("発注番号").Value = ds.Tables(RS).Rows(index)("発注番号")
                    DgvCymnhd.Rows(index).Cells("発注番号枝番").Value = ds.Tables(RS).Rows(index)("発注番号枝番")
                    DgvCymnhd.Rows(index).Cells("仕入先コード").Value = ds.Tables(RS).Rows(index)("仕入先コード")
                    DgvCymnhd.Rows(index).Cells("仕入先名").Value = ds.Tables(RS).Rows(index)("仕入先名")
                    DgvCymnhd.Rows(index).Cells("仕入先郵便番号").Value = ds.Tables(RS).Rows(index)("仕入先郵便番号")
                    DgvCymnhd.Rows(index).Cells("仕入先住所").Value = ds.Tables(RS).Rows(index)("仕入先住所")
                    DgvCymnhd.Rows(index).Cells("仕入先電話番号").Value = ds.Tables(RS).Rows(index)("仕入先電話番号")
                    DgvCymnhd.Rows(index).Cells("仕入先ＦＡＸ").Value = ds.Tables(RS).Rows(index)("仕入先ＦＡＸ")
                    DgvCymnhd.Rows(index).Cells("仕入先担当者名").Value = ds.Tables(RS).Rows(index)("仕入先担当者名")
                    DgvCymnhd.Rows(index).Cells("仕入先担当者役職").Value = ds.Tables(RS).Rows(index)("仕入先担当者役職")
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
                Sql += "t42_nyukohd"

                Dim reccnt As Integer = 0
                ds = _db.selectDB(Sql, RS, reccnt)
                DgvCymnhd.Columns.Add("入庫番号", "入庫番号")
                DgvCymnhd.Columns.Add("入庫日", "入庫日")
                DgvCymnhd.Columns.Add("発注番号", "発注番号")
                DgvCymnhd.Columns.Add("発注番号枝番", "発注番号枝番")
                DgvCymnhd.Columns.Add("仕入先コード", "仕入先コード")
                DgvCymnhd.Columns.Add("仕入先名", "仕入先名")
                DgvCymnhd.Columns.Add("仕入先郵便番号", "仕入先郵便番号")
                DgvCymnhd.Columns.Add("仕入先住所", "仕入先住所")
                DgvCymnhd.Columns.Add("仕入先電話番号", "仕入先電話番号")
                DgvCymnhd.Columns.Add("仕入先ＦＡＸ", "仕入先ＦＡＸ")
                DgvCymnhd.Columns.Add("仕入先担当者名", "仕入先担当者名")
                DgvCymnhd.Columns.Add("仕入先担当者役職", "仕入先担当者役職")
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
                    DgvCymnhd.Rows(index).Cells("入庫番号").Value = ds.Tables(RS).Rows(index)("入庫番号")
                    DgvCymnhd.Rows(index).Cells("入庫日").Value = ds.Tables(RS).Rows(index)("入庫日")
                    DgvCymnhd.Rows(index).Cells("発注番号").Value = ds.Tables(RS).Rows(index)("発注番号")
                    DgvCymnhd.Rows(index).Cells("発注番号枝番").Value = ds.Tables(RS).Rows(index)("発注番号枝番")
                    DgvCymnhd.Rows(index).Cells("仕入先コード").Value = ds.Tables(RS).Rows(index)("仕入先コード")
                    DgvCymnhd.Rows(index).Cells("仕入先名").Value = ds.Tables(RS).Rows(index)("仕入先名")
                    DgvCymnhd.Rows(index).Cells("仕入先郵便番号").Value = ds.Tables(RS).Rows(index)("仕入先郵便番号")
                    DgvCymnhd.Rows(index).Cells("仕入先住所").Value = ds.Tables(RS).Rows(index)("仕入先住所")
                    DgvCymnhd.Rows(index).Cells("仕入先電話番号").Value = ds.Tables(RS).Rows(index)("仕入先電話番号")
                    DgvCymnhd.Rows(index).Cells("仕入先ＦＡＸ").Value = ds.Tables(RS).Rows(index)("仕入先ＦＡＸ")
                    DgvCymnhd.Rows(index).Cells("仕入先担当者名").Value = ds.Tables(RS).Rows(index)("仕入先担当者名")
                    DgvCymnhd.Rows(index).Cells("仕入先担当者役職").Value = ds.Tables(RS).Rows(index)("仕入先担当者役職")
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
        If ReceiptStatus = "CANCEL" Then
            BtnReceiptCancel.Visible = True
            BtnReceiptCancel.Location = New Point(997, 677)
        ElseIf ReceiptStatus = "VIEW" Then
            BtnReceiptView.Visible = True
            BtnReceiptView.Location = New Point(997, 677)
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
            Sql += "t42_nyukohd"

            If ReceiptNo IsNot Nothing Then
                For i As Integer = 0 To ReceiptNo.Length - 1
                    If i = 0 Then
                        Sql += " WHERE "
                        Sql += "出庫番号"
                        Sql += " ILIKE "
                        Sql += "'"
                        Sql += ReceiptNo(i)
                        Sql += "'"
                    Else
                        Sql += " OR "
                        Sql += "出庫番号"
                        Sql += " ILIKE "
                        Sql += "'"
                        Sql += ReceiptNo(i)
                        Sql += "%'"
                    End If
                Next
            End If

            Dim reccnt As Integer = 0
            ds = _db.selectDB(Sql, RS, reccnt)

            DgvCymnhd.Columns.Add("入庫番号", "入庫番号")
            DgvCymnhd.Columns.Add("入庫日", "入庫日")
            DgvCymnhd.Columns.Add("発注番号", "発注番号")
            DgvCymnhd.Columns.Add("発注番号枝番", "発注番号枝番")
            DgvCymnhd.Columns.Add("仕入先コード", "仕入先コード")
            DgvCymnhd.Columns.Add("仕入先名", "仕入先名")
            DgvCymnhd.Columns.Add("仕入先郵便番号", "仕入先郵便番号")
            DgvCymnhd.Columns.Add("仕入先住所", "仕入先住所")
            DgvCymnhd.Columns.Add("仕入先電話番号", "仕入先電話番号")
            DgvCymnhd.Columns.Add("仕入先ＦＡＸ", "仕入先ＦＡＸ")
            DgvCymnhd.Columns.Add("仕入先担当者名", "仕入先担当者名")
            DgvCymnhd.Columns.Add("仕入先担当者役職", "仕入先担当者役職")
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
                DgvCymnhd.Rows(index).Cells("入庫番号").Value = ds.Tables(RS).Rows(index)("入庫番号")
                DgvCymnhd.Rows(index).Cells("入庫日").Value = ds.Tables(RS).Rows(index)("入庫日")
                DgvCymnhd.Rows(index).Cells("発注番号").Value = ds.Tables(RS).Rows(index)("発注番号")
                DgvCymnhd.Rows(index).Cells("発注番号枝番").Value = ds.Tables(RS).Rows(index)("発注番号枝番")
                DgvCymnhd.Rows(index).Cells("仕入先コード").Value = ds.Tables(RS).Rows(index)("仕入先コード")
                DgvCymnhd.Rows(index).Cells("仕入先名").Value = ds.Tables(RS).Rows(index)("仕入先名")
                DgvCymnhd.Rows(index).Cells("仕入先郵便番号").Value = ds.Tables(RS).Rows(index)("仕入先郵便番号")
                DgvCymnhd.Rows(index).Cells("仕入先住所").Value = ds.Tables(RS).Rows(index)("仕入先住所")
                DgvCymnhd.Rows(index).Cells("仕入先電話番号").Value = ds.Tables(RS).Rows(index)("仕入先電話番号")
                DgvCymnhd.Rows(index).Cells("仕入先ＦＡＸ").Value = ds.Tables(RS).Rows(index)("仕入先ＦＡＸ")
                DgvCymnhd.Rows(index).Cells("仕入先担当者名").Value = ds.Tables(RS).Rows(index)("仕入先担当者名")
                DgvCymnhd.Rows(index).Cells("仕入先担当者役職").Value = ds.Tables(RS).Rows(index)("仕入先担当者役職")
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
            Sql += "t43_nyukodt"

            If ReceiptNo IsNot Nothing Then
                For i As Integer = 0 To ReceiptNo.Length - 1
                    If i = 0 Then
                        Sql += " WHERE "
                        Sql += "出庫番号"
                        Sql += " ILIKE "
                        Sql += "'"
                        Sql += ReceiptNo(i)
                        Sql += "'"
                    Else
                        Sql += " OR "
                        Sql += "出庫番号"
                        Sql += " ILIKE "
                        Sql += "'"
                        Sql += ReceiptNo(i)
                        Sql += "%'"
                    End If
                Next
            End If

            Dim reccnt As Integer = 0
            ds = _db.selectDB(Sql, RS, reccnt)

            DgvCymnhd.Columns.Add("入庫番号", "入庫番号")
            DgvCymnhd.Columns.Add("行番号", "行番号")
            DgvCymnhd.Columns.Add("仕入区分", "仕入区分")
            DgvCymnhd.Columns.Add("メーカー", "メーカー")
            DgvCymnhd.Columns.Add("品名", "品名")
            DgvCymnhd.Columns.Add("型式", "型式")
            DgvCymnhd.Columns.Add("仕入先名", "仕入先名")
            DgvCymnhd.Columns.Add("入庫数量", "入庫数量")
            DgvCymnhd.Columns.Add("単位", "単位")
            DgvCymnhd.Columns.Add("仕入値", "仕入値")
            DgvCymnhd.Columns.Add("備考", "備考")
            DgvCymnhd.Columns.Add("更新者", "更新者")

            DgvCymnhd.Columns("入庫数量").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvCymnhd.Columns("仕入値").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvCymnhd.Rows.Add()
                DgvCymnhd.Rows(index).Cells("入庫番号").Value = ds.Tables(RS).Rows(index)("入庫番号")
                DgvCymnhd.Rows(index).Cells("行番号").Value = ds.Tables(RS).Rows(index)("行番号")
                DgvCymnhd.Rows(index).Cells("仕入区分").Value = ds.Tables(RS).Rows(index)("仕入区分")
                DgvCymnhd.Rows(index).Cells("メーカー").Value = ds.Tables(RS).Rows(index)("メーカー")
                DgvCymnhd.Rows(index).Cells("品名").Value = ds.Tables(RS).Rows(index)("品名")
                DgvCymnhd.Rows(index).Cells("型式").Value = ds.Tables(RS).Rows(index)("型式")
                DgvCymnhd.Rows(index).Cells("仕入先名").Value = ds.Tables(RS).Rows(index)("仕入先名")
                DgvCymnhd.Rows(index).Cells("入庫数量").Value = ds.Tables(RS).Rows(index)("入庫数量")
                DgvCymnhd.Rows(index).Cells("単位").Value = ds.Tables(RS).Rows(index)("単位")
                DgvCymnhd.Rows(index).Cells("仕入値").Value = ds.Tables(RS).Rows(index)("仕入値")
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
            Sql += "t42_nyukohd"
            If TxtSupplierName.Text = "" Then
            Else
                Sql += " WHERE "
                Sql += "得意先名"
                Sql += " ILIKE "
                Sql += "'%"
                Sql += TxtSupplierName.Text
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
            If TxtSupplierCode.Text = "" Then
            Else
                If count > 0 Then
                    Sql += " AND "
                    Sql += "得意先コード"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtSupplierCode.Text
                    Sql += "%'"
                Else
                    Sql += " WHERE "
                    Sql += "得意先コード"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtSupplierCode.Text
                    Sql += "%'"
                    count += 1
                End If
            End If
            If TxtReceiptDate1.Text = "" Then
                If TxtReceiptDate2.Text = "" Then
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "出庫日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtReceiptDate2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "出庫日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtReceiptDate2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            Else
                If TxtReceiptDate2.Text = "" Then
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "出庫日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtReceiptDate1.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "出庫日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtReceiptDate1.Text
                        Sql += "'"
                        count += 1
                    End If
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "出庫日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtReceiptDate1.Text
                        Sql += "' "
                        Sql += "AND  "
                        Sql += "出庫日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtReceiptDate2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "出庫日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtReceiptDate1.Text
                        Sql += "' "
                        Sql += "AND  "
                        Sql += "出庫日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtReceiptDate2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            End If
            If TxtReceiptNo1.Text = "" Then
                If TxtReceiptNo2.Text = "" Then
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "出庫番号"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtReceiptNo2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "出庫番号"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtReceiptNo2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            Else
                If TxtReceiptNo2.Text = "" Then
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "出庫番号"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtReceiptNo1.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "出庫番号"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtReceiptNo1.Text
                        Sql += "'"
                        count += 1
                    End If
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "出庫番号"
                        Sql += " >= "
                        Sql += "'"
                        Sql += TxtReceiptNo1.Text
                        Sql += "' "
                        Sql += "AND "
                        Sql += "出庫番号"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtReceiptNo2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "出庫番号"
                        Sql += " >= "
                        Sql += "'"
                        Sql += TxtReceiptNo1.Text
                        Sql += "' "
                        Sql += "AND  "
                        Sql += "出庫番号"
                        Sql += " <= "
                        Sql += "'"
                        Sql += TxtReceiptNo2.Text
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

            DgvCymnhd.Columns.Add("入庫番号", "入庫番号")
            DgvCymnhd.Columns.Add("入庫日", "入庫日")
            DgvCymnhd.Columns.Add("発注番号", "発注番号")
            DgvCymnhd.Columns.Add("発注番号枝番", "発注番号枝番")
            DgvCymnhd.Columns.Add("仕入先コード", "仕入先コード")
            DgvCymnhd.Columns.Add("仕入先名", "仕入先名")
            DgvCymnhd.Columns.Add("仕入先郵便番号", "仕入先郵便番号")
            DgvCymnhd.Columns.Add("仕入先住所", "仕入先住所")
            DgvCymnhd.Columns.Add("仕入先電話番号", "仕入先電話番号")
            DgvCymnhd.Columns.Add("仕入先ＦＡＸ", "仕入先ＦＡＸ")
            DgvCymnhd.Columns.Add("仕入先担当者名", "仕入先担当者名")
            DgvCymnhd.Columns.Add("仕入先担当者役職", "仕入先担当者役職")
            DgvCymnhd.Columns.Add("営業担当者", "営業担当者")
            DgvCymnhd.Columns.Add("入力担当者", "入力担当者")
            DgvCymnhd.Columns.Add("備考", "備考")
            DgvCymnhd.Columns.Add("登録日", "登録日")

            Dim tmp As Integer = ds.Tables(RS).Rows.Count - 1
            ReDim ReceiptNo(tmp)

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvCymnhd.Rows.Add()
                DgvCymnhd.Rows(index).Cells("入庫番号").Value = ds.Tables(RS).Rows(index)("入庫番号")
                ReceiptNo(index) = ds.Tables(RS).Rows(index)("入庫番号")
                DgvCymnhd.Rows(index).Cells("入庫日").Value = ds.Tables(RS).Rows(index)("入庫日")
                DgvCymnhd.Rows(index).Cells("発注番号").Value = ds.Tables(RS).Rows(index)("発注番号")
                DgvCymnhd.Rows(index).Cells("発注番号枝番").Value = ds.Tables(RS).Rows(index)("発注番号枝番")
                DgvCymnhd.Rows(index).Cells("仕入先コード").Value = ds.Tables(RS).Rows(index)("仕入先コード")
                DgvCymnhd.Rows(index).Cells("仕入先名").Value = ds.Tables(RS).Rows(index)("仕入先名")
                DgvCymnhd.Rows(index).Cells("仕入先郵便番号").Value = ds.Tables(RS).Rows(index)("仕入先郵便番号")
                DgvCymnhd.Rows(index).Cells("仕入先住所").Value = ds.Tables(RS).Rows(index)("仕入先住所")
                DgvCymnhd.Rows(index).Cells("仕入先電話番号").Value = ds.Tables(RS).Rows(index)("仕入先電話番号")
                DgvCymnhd.Rows(index).Cells("仕入先ＦＡＸ").Value = ds.Tables(RS).Rows(index)("仕入先ＦＡＸ")
                DgvCymnhd.Rows(index).Cells("仕入先担当者名").Value = ds.Tables(RS).Rows(index)("仕入先担当者名")
                DgvCymnhd.Rows(index).Cells("仕入先担当者役職").Value = ds.Tables(RS).Rows(index)("仕入先担当者役職")
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
    End Sub

    Private Sub BtnSalesCancel_Click(sender As Object, e As EventArgs) Handles BtnReceiptCancel.Click
        Dim dtNow As DateTime = DateTime.Now
        Dim Sql As String = ""
        Sql = ""
        Sql += "UPDATE "
        Sql += "Public."
        Sql += "t42_nyukohd "
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
        Sql += " 入庫番号"
        Sql += "='"
        Sql += DgvCymnhd.Rows(DgvCymnhd.CurrentCell.RowIndex).Cells("入庫番号").Value
        Sql += "' "
        Sql += "RETURNING 会社コード"
        Sql += ", "
        Sql += "入庫番号"
        Sql += ", "
        Sql += "発注番号"
        Sql += ", "
        Sql += "発注番号枝番"
        Sql += ", "
        Sql += "仕入先コード"
        Sql += ", "
        Sql += "仕入先名"
        Sql += ", "
        Sql += "仕入先郵便番号"
        Sql += ", "
        Sql += "仕入先住所"
        Sql += ", "
        Sql += "仕入先電話番号"
        Sql += ", "
        Sql += "仕入先ＦＡＸ"
        Sql += ", "
        Sql += "仕入先担当者役職"
        Sql += ", "
        Sql += "仕入先担当者名"
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
        Sql += "入庫日"
        Sql += ", "
        Sql += "更新日"
        Sql += ", "
        Sql += "更新者"
        Dim result As DialogResult = MessageBox.Show("発注を取り消しますか？",
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

    Private Sub BtnSalesView_Click(sender As Object, e As EventArgs) Handles BtnReceiptView.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvCymnhd.CurrentCell.RowIndex
        Dim No As String = DgvCymnhd.Rows(RowIdx).Cells("発注番号").Value
        Dim Suffix As String = DgvCymnhd.Rows(RowIdx).Cells("発注番号枝番").Value
        Dim Status As String = "VIEW"

        Dim openForm As Form = Nothing
        openForm = New Receipt(_msgHd, _db, _langHd, No, Suffix, Status)   '処理選択
        openForm.Show(Me)
    End Sub
End Class