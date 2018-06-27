Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class PurchasingManagement
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
            Sql += "* "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t20_hattyu"

            Dim reccnt As Integer = 0
            ds = _db.selectDB(Sql, RS, reccnt)
            DgvHtyhd.Columns.Add("発注番号", "発注番号")
            DgvHtyhd.Columns.Add("発注番号枝番", "発注番号枝番")
            DgvHtyhd.Columns.Add("発注日", "発注日")
            DgvHtyhd.Columns.Add("仕入先コード", "仕入先コード")
            DgvHtyhd.Columns.Add("仕入先名", "仕入先名")
            DgvHtyhd.Columns.Add("仕入先郵便番号", "仕入先郵便番号")
            DgvHtyhd.Columns.Add("仕入先住所", "仕入先住所")
            DgvHtyhd.Columns.Add("仕入先電話番号", "仕入先電話番号")
            DgvHtyhd.Columns.Add("仕入先ＦＡＸ", "仕入先ＦＡＸ")
            DgvHtyhd.Columns.Add("仕入先担当者名", "仕入先担当者名")
            DgvHtyhd.Columns.Add("仕入先担当者役職", "仕入先担当者役職")
            DgvHtyhd.Columns.Add("仕入金額", "仕入金額")
            DgvHtyhd.Columns.Add("支払条件", "支払条件")
            DgvHtyhd.Columns.Add("営業担当者", "営業担当者")
            DgvHtyhd.Columns.Add("入力担当者", "入力担当者")
            DgvHtyhd.Columns.Add("備考", "備考")
            DgvHtyhd.Columns.Add("登録日", "登録日")

            DgvHtyhd.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvHtyhd.Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvHtyhd.Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvHtyhd.Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvHtyhd.Rows.Add()
                DgvHtyhd.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)("発注番号")
                DgvHtyhd.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)("発注番号枝番")
                DgvHtyhd.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)("発注日")
                DgvHtyhd.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)("仕入先コード")
                DgvHtyhd.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)("仕入先名")
                DgvHtyhd.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)("仕入先郵便番号")
                DgvHtyhd.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)("仕入先住所")
                DgvHtyhd.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)("仕入先電話番号")
                DgvHtyhd.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)("仕入先ＦＡＸ")
                DgvHtyhd.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)("仕入先担当者名")
                DgvHtyhd.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)("仕入先担当者役職")
                DgvHtyhd.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)("仕入金額")
                DgvHtyhd.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)("支払条件")
                DgvHtyhd.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)("営業担当者")
                DgvHtyhd.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)("入力担当者")
                DgvHtyhd.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)("備考")
                DgvHtyhd.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)("登録日")
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



    Private Sub BtnSelect_Click(sender As Object, e As EventArgs) Handles BtnSelect.Click
        Dim RowIdx As Integer
        RowIdx = Me.DgvHtyhd.CurrentCell.RowIndex
        Dim No As String = DgvHtyhd.Rows(RowIdx).Cells(0).Value
        Dim Suffix As String = DgvHtyhd.Rows(RowIdx).Cells(1).Value
        Dim status As String = "EDIT"

        Dim openForm As Form = Nothing
        openForm = New Purchase(_msgHd, _db, No, Suffix)   '処理選択
        openForm.Show(Me)
    End Sub

    Private Sub BtnPurchaseSearch_Click(sender As Object, e As EventArgs) Handles BtnPurchaseSearch.Click
        DgvHtyhd.Rows.Clear()
        DgvHtyhd.Columns.Clear()

        Dim count As Integer = 0
        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t20_hattyu"
            If TxtSupplierName.Text = "" Then
            Else
                Sql += " WHERE "
                Sql += "仕入先名"
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
                    Sql += "仕入先住所"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtAddress.Text
                    Sql += "%'"
                Else
                    Sql += " WHERE "
                    Sql += "仕入先住所"
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
                    Sql += "仕入先電話番号"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtTel.Text
                    Sql += "%'"
                Else
                    Sql += " WHERE "
                    Sql += "仕入先電話番号"
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
                    Sql += "仕入先コード"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtSupplierCode.Text
                    Sql += "%'"
                Else
                    Sql += " WHERE "
                    Sql += "仕入先コード"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtSupplierCode.Text
                    Sql += "%'"
                    count += 1
                End If
            End If
            If TxtPurchaseDate1.Text = "" Then
                If TxtPurchaseDate2.Text = "" Then
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "発注日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtPurchaseDate2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "発注日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtPurchaseDate2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            Else
                If TxtPurchaseDate2.Text = "" Then
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "発注日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtPurchaseDate1.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "発注日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtPurchaseDate1.Text
                        Sql += "'"
                        count += 1
                    End If
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "発注日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtPurchaseDate1.Text
                        Sql += "' "
                        Sql += "AND  "
                        Sql += "発注日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtPurchaseDate2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "発注日"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtPurchaseDate1.Text
                        Sql += "' "
                        Sql += "AND  "
                        Sql += "発注日"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtPurchaseDate2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            End If
            If TxtPurchaseNo1.Text = "" Then
                If TxtPurchaseNo2.Text = "" Then
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "発注番号"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtPurchaseNo2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "発注番号"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtPurchaseNo2.Text
                        Sql += "'"
                        count += 1
                    End If
                End If
            Else
                If TxtPurchaseNo2.Text = "" Then
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "発注番号"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtPurchaseNo1.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "発注番号"
                        Sql += " >=  "
                        Sql += "'"
                        Sql += TxtPurchaseNo1.Text
                        Sql += "'"
                        count += 1
                    End If
                Else
                    If count > 0 Then
                        Sql += " AND "
                        Sql += "発注番号"
                        Sql += " >= "
                        Sql += "'"
                        Sql += TxtPurchaseNo1.Text
                        Sql += "' "
                        Sql += "AND "
                        Sql += "発注番号"
                        Sql += " <=  "
                        Sql += "'"
                        Sql += TxtPurchaseNo2.Text
                        Sql += "'"
                    Else
                        Sql += " WHERE "
                        Sql += "発注番号"
                        Sql += " >= "
                        Sql += "'"
                        Sql += TxtPurchaseNo1.Text
                        Sql += "' "
                        Sql += "AND  "
                        Sql += "発注番号"
                        Sql += " <= "
                        Sql += "'"
                        Sql += TxtPurchaseNo2.Text
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

            DgvHtyhd.Columns.Add("発注番号", "発注番号")
            DgvHtyhd.Columns.Add("発注番号枝番", "発注番号枝番")
            DgvHtyhd.Columns.Add("発注日", "発注日")
            DgvHtyhd.Columns.Add("仕入先コード", "仕入先コード")
            DgvHtyhd.Columns.Add("仕入先名", "仕入先名")
            DgvHtyhd.Columns.Add("仕入先郵便番号", "仕入先郵便番号")
            DgvHtyhd.Columns.Add("仕入先住所", "仕入先住所")
            DgvHtyhd.Columns.Add("仕入先電話番号", "仕入先電話番号")
            DgvHtyhd.Columns.Add("仕入先ＦＡＸ", "仕入先ＦＡＸ")
            DgvHtyhd.Columns.Add("仕入先担当者名", "仕入先担当者名")
            DgvHtyhd.Columns.Add("仕入先担当者役職", "仕入先担当者役職")
            DgvHtyhd.Columns.Add("仕入金額", "仕入金額")
            DgvHtyhd.Columns.Add("支払条件", "支払条件")
            DgvHtyhd.Columns.Add("営業担当者", "営業担当者")
            DgvHtyhd.Columns.Add("入力担当者", "入力担当者")
            DgvHtyhd.Columns.Add("備考", "備考")
            DgvHtyhd.Columns.Add("登録日", "登録日")

            DgvHtyhd.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            Dim tmp As Integer = ds.Tables(RS).Rows.Count - 1
            ReDim OrderNo(tmp)

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvHtyhd.Rows.Add()
                DgvHtyhd.Rows(index).Cells(0).Value = ds.Tables(RS).Rows(index)("発注番号")
                DgvHtyhd.Rows(index).Cells(1).Value = ds.Tables(RS).Rows(index)("発注番号枝番")
                DgvHtyhd.Rows(index).Cells(2).Value = ds.Tables(RS).Rows(index)("発注日")
                DgvHtyhd.Rows(index).Cells(3).Value = ds.Tables(RS).Rows(index)("仕入先コード")
                DgvHtyhd.Rows(index).Cells(4).Value = ds.Tables(RS).Rows(index)("仕入先名")
                DgvHtyhd.Rows(index).Cells(5).Value = ds.Tables(RS).Rows(index)("仕入先郵便番号")
                DgvHtyhd.Rows(index).Cells(6).Value = ds.Tables(RS).Rows(index)("仕入先住所")
                DgvHtyhd.Rows(index).Cells(7).Value = ds.Tables(RS).Rows(index)("仕入先電話番号")
                DgvHtyhd.Rows(index).Cells(8).Value = ds.Tables(RS).Rows(index)("仕入先ＦＡＸ")
                DgvHtyhd.Rows(index).Cells(9).Value = ds.Tables(RS).Rows(index)("仕入先担当者名")
                DgvHtyhd.Rows(index).Cells(10).Value = ds.Tables(RS).Rows(index)("仕入先担当者役職")
                DgvHtyhd.Rows(index).Cells(11).Value = ds.Tables(RS).Rows(index)("仕入金額")
                DgvHtyhd.Rows(index).Cells(12).Value = ds.Tables(RS).Rows(index)("支払条件")
                DgvHtyhd.Rows(index).Cells(13).Value = ds.Tables(RS).Rows(index)("営業担当者")
                DgvHtyhd.Rows(index).Cells(14).Value = ds.Tables(RS).Rows(index)("入力担当者")
                DgvHtyhd.Rows(index).Cells(15).Value = ds.Tables(RS).Rows(index)("備考")
                DgvHtyhd.Rows(index).Cells(16).Value = ds.Tables(RS).Rows(index)("登録日")
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub
End Class