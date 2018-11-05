Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class ClosingLog
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
    Private OrderingNo As String()
    Private _status As String = ""

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
                   Optional ByRef prmRefStatus As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _status = prmRefStatus
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    Private Sub ClosingLogLoad()
        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t51_smlog"

            Dim reccnt As Integer = 0
            ds = _db.selectDB(Sql, RS, reccnt)

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvClosingLog.Rows.Add()
                DgvClosingLog.Rows(index).Cells("締処理日時").Value = ds.Tables(RS).Rows(index)("処理日時")
                DgvClosingLog.Rows(index).Cells("前回締日").Value = ds.Tables(RS).Rows(index)("前回締日")
                DgvClosingLog.Rows(index).Cells("今回締日").Value = ds.Tables(RS).Rows(index)("今回締日")
                DgvClosingLog.Rows(index).Cells("次回締日").Value = ds.Tables(RS).Rows(index)("次回締日")
                DgvClosingLog.Rows(index).Cells("担当者").Value = ds.Tables(RS).Rows(index)("担当者")
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Sub ClosingLog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ClosingLogLoad()
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        Dim openForm As Form = Nothing
        openForm = New frmC01F30_Menu(_msgHd, _langHd, _db)
        openForm.Show()
        Me.Close()
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        DgvClosingLog.Rows.Clear()

        Dim count As Integer = 0
        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t51_smlog"
            If TxtClosingDate.Text = "" Then
            Else
                Sql += " WHERE "
                Sql += "処理日時"
                Sql += " ILIKE "
                Sql += "'%"
                Sql += TxtClosingDate.Text
                Sql += "%'"
                count += 1
            End If
            If TxtPerson.Text = "" Then
            Else
                If count > 0 Then
                    Sql += " AND "
                    Sql += "担当者"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtPerson.Text
                    Sql += "%'"
                Else
                    Sql += " WHERE "
                    Sql += "担当者"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtPerson.Text
                    Sql += "%'"
                    count += 1
                End If
            End If

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvClosingLog.Rows.Add()
                DgvClosingLog.Rows(index).Cells("締処理日時").Value = ds.Tables(RS).Rows(index)("処理日時")
                DgvClosingLog.Rows(index).Cells("前回締日").Value = ds.Tables(RS).Rows(index)("前回締日")
                DgvClosingLog.Rows(index).Cells("今回締日").Value = ds.Tables(RS).Rows(index)("今回締日")
                DgvClosingLog.Rows(index).Cells("次回締日").Value = ds.Tables(RS).Rows(index)("次回締日")
                DgvClosingLog.Rows(index).Cells("担当者").Value = ds.Tables(RS).Rows(index)("担当者")
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Sub BtnClosing_Click(sender As Object, e As EventArgs) Handles BtnClosing.Click


        Dim reccnt As Integer = 0
        Dim dtToday As DateTime = DateTime.Now

        Dim Sql1 As String = ""
        Sql1 += "SELECT "
        Sql1 += "* "
        Sql1 += "FROM "
        Sql1 += "public"
        Sql1 += "."
        Sql1 += "m01_company"
        Sql1 += " WHERE "
        Sql1 += "会社コード"
        Sql1 += " ILIKE  "
        Sql1 += "'"
        Sql1 += frmC01F10_Login.loginValue.BumonNM
        Sql1 += "'"

        Dim ds1 As DataSet = _db.selectDB(Sql1, RS, reccnt)


        Accounting()


        Dim Sql2 As String = ""
        Sql2 += "SELECT "
        Sql2 += "* "
        Sql2 += "FROM "
        Sql2 += "public"
        Sql2 += "."
        Sql2 += "t30_urighd"
        Sql2 += " WHERE "
        Sql2 += "売上日"
        Sql2 += " >  "
        Sql2 += "'"
        Sql2 += ds1.Tables(RS).Rows(0)("前回締日")
        Sql2 += "'"
        Sql2 += " AND "
        Sql2 += "売上日"
        Sql2 += " <=  "
        Sql2 += "'"
        Sql2 += ds1.Tables(RS).Rows(0)("今回締日")
        Sql2 += "'"
        Sql2 += " AND "
        Sql2 += "締処理日"
        Sql2 += " IS NULL "
        Dim dsUrigdt As DataSet = _db.selectDB(Sql2, RS, reccnt)


        Dim Sql3 As String = ""
        Sql3 += "SELECT "
        Sql3 += "* "
        Sql3 += "FROM "
        Sql3 += "public"
        Sql3 += "."
        Sql3 += "t40_sirehd"
        Sql3 += " WHERE "
        Sql3 += "仕入日"
        Sql3 += " >  "
        Sql3 += "'"
        Sql3 += ds1.Tables(RS).Rows(0)("前回締日")
        Sql3 += "'"
        Sql3 += " AND "
        Sql3 += "仕入日"
        Sql3 += " <=  "
        Sql3 += "'"
        Sql3 += ds1.Tables(RS).Rows(0)("今回締日")
        Sql3 += "'"
        Sql3 += " AND "
        Sql3 += "締処理日"
        Sql3 += " IS NULL "
        Dim ds3 As DataSet = _db.selectDB(Sql3, RS, reccnt)


        Dim Sql4 As String = ""
        Sql4 += "SELECT "
        Sql4 += "* "
        Sql4 += "FROM "
        Sql4 += "public"
        Sql4 += "."
        Sql4 += "t23_skyuhd"
        Sql4 += " WHERE "
        Sql4 += "請求日"
        Sql4 += " >  "
        Sql4 += "'"
        Sql4 += ds1.Tables(RS).Rows(0)("前回締日")
        Sql4 += "'"
        Sql4 += " AND "
        Sql4 += "請求日"
        Sql4 += " <=  "
        Sql4 += "'"
        Sql4 += ds1.Tables(RS).Rows(0)("今回締日")
        Sql4 += "'"
        Sql4 += " AND "
        Sql4 += "締処理日"
        Sql4 += " IS NULL "
        Dim ds4 As DataSet = _db.selectDB(Sql4, RS, reccnt)


        Sql4 = ""
        Sql4 += "SELECT "
        Sql4 += "* "
        Sql4 += "FROM "
        Sql4 += "public"
        Sql4 += "."
        Sql4 += "t46_kikehd"
        Sql4 += " WHERE "
        Sql4 += "買掛日"
        Sql4 += " >  "
        Sql4 += "'"
        Sql4 += ds1.Tables(RS).Rows(0)("前回締日")
        Sql4 += "'"
        Sql4 += " AND "
        Sql4 += "買掛日"
        Sql4 += " <=  "
        Sql4 += "'"
        Sql4 += ds1.Tables(RS).Rows(0)("今回締日")
        Sql4 += "'"
        Sql4 += " AND "
        Sql4 += "締処理日"
        Sql4 += " IS NULL "
        Dim dsKike As DataSet = _db.selectDB(Sql4, RS, reccnt)

        Dim Sql5 As String = ""
        Sql5 += "SELECT "
        Sql5 += "* "
        Sql5 += "FROM "
        Sql5 += "public"
        Sql5 += "."
        Sql5 += "t31_urigdt"
        For i As Integer = 0 To dsUrigdt.Tables(RS).Rows.Count - 1
            If i = 0 Then
                Sql5 += " WHERE "
                Sql5 += "売上番号"
                Sql5 += " ILIKE  "
                Sql5 += "'"
                Sql5 += dsUrigdt.Tables(RS).Rows(i)("売上番号")
                Sql5 += "'"
            Else
                Sql5 += " OR "
                Sql5 += "売上番号"
                Sql5 += " ILIKE  "
                Sql5 += "'"
                Sql5 += dsUrigdt.Tables(RS).Rows(i)("売上番号")
                Sql5 += "'"
            End If
        Next

        Dim Sql6 As String = ""
        Sql6 += "SELECT "
        Sql6 += "* "
        Sql6 += "FROM "
        Sql6 += "public"
        Sql6 += "."
        Sql6 += "t41_siredt"
        For i As Integer = 0 To ds3.Tables(RS).Rows.Count - 1
            If i = 0 Then
                Sql6 += " WHERE "
                Sql6 += "仕入番号"
                Sql6 += " ILIKE  "
                Sql6 += "'"
                Sql6 += ds3.Tables(RS).Rows(i)("仕入番号")
                Sql6 += "'"
            Else
                Sql6 += " OR "
                Sql6 += "仕入番号"
                Sql6 += " ILIKE  "
                Sql6 += "'"
                Sql6 += ds3.Tables(RS).Rows(i)("仕入番号")
                Sql6 += "'"
            End If
        Next

        Dim ds5 As DataSet = _db.selectDB(Sql5, RS, reccnt)
        Dim ds6 As DataSet = _db.selectDB(Sql6, RS, reccnt)

        Dim zaiko As String = ""
        zaiko += "SELECT "
        zaiko += "* "
        zaiko += "FROM "
        zaiko += "public"
        zaiko += "."
        zaiko += "t50_zikhd"

        Dim dszaiko As DataSet = _db.selectDB(zaiko, RS, reccnt)
        Dim SqlZaiko As String = ""

        For i As Integer = 0 To dszaiko.Tables(RS).Rows.Count - 1
            Dim purchase As String = ""
            purchase += "SELECT "
            purchase += "* "
            purchase += "FROM "
            purchase += "public"
            purchase += "."
            purchase += "t41_siredt"
            purchase += " WHERE "
            purchase += "会社コード"
            purchase += " ILIKE  "
            purchase += "'"
            purchase += frmC01F10_Login.loginValue.BumonNM
            purchase += "'"
            purchase += " AND "
            purchase += "メーカー"
            purchase += " ILIKE  "
            purchase += "'"
            purchase += dszaiko.Tables(RS).Rows(i)("メーカー")
            purchase += "'"
            purchase += " AND "
            purchase += "品名"
            purchase += " ILIKE  "
            purchase += "'"
            purchase += dszaiko.Tables(RS).Rows(i)("品名")
            purchase += "'"
            purchase += " AND "
            purchase += "型式"
            purchase += " ILIKE  "
            purchase += "'"
            purchase += dszaiko.Tables(RS).Rows(i)("型式")
            purchase += "'"
            purchase += " AND "
            purchase += "締処理日"
            purchase += " IS NULL "

            Dim dsPurchase As DataSet = _db.selectDB(purchase, RS, reccnt)
            Dim PurchaseSum As Double = 0
            Dim OverheadSum As Double = 0
            Dim PurchaseQuantity As Double = 0

            For x As Integer = 0 To dsPurchase.Tables(RS).Rows.Count - 1
                PurchaseSum += dsPurchase.Tables(RS).Rows(x)("仕入金額")
                OverheadSum += dsPurchase.Tables(RS).Rows(x)("間接費")
                PurchaseQuantity += dsPurchase.Tables(RS).Rows(x)("仕入数量")

                purchase = ""
                purchase += "UPDATE "
                purchase += "Public."
                purchase += "t41_siredt "
                purchase += "SET "
                purchase += " 締処理日"
                purchase += " = '"
                purchase += dtToday
                purchase += "' "

                purchase += " WHERE "
                purchase += "会社コード"
                purchase += " ILIKE  "
                purchase += "'"
                purchase += frmC01F10_Login.loginValue.BumonNM
                purchase += "'"
                purchase += " AND "
                purchase += "仕入番号"
                purchase += " ILIKE  "
                purchase += "'"
                purchase += dsPurchase.Tables(RS).Rows(x)("仕入番号")
                purchase += "'"
                purchase += " AND "
                purchase += "行番号"
                purchase += " =  "
                purchase += "'"
                purchase += dsPurchase.Tables(RS).Rows(x)("行番号").ToString
                purchase += "'"

                purchase += "RETURNING 会社コード"
                purchase += ", "
                purchase += "仕入番号"
                purchase += ", "
                purchase += "発注番号"
                purchase += ", "
                purchase += "発注番号枝番"
                purchase += ", "
                purchase += "行番号"
                purchase += ", "
                purchase += "仕入区分"
                purchase += ", "
                purchase += "メーカー"
                purchase += ", "
                purchase += "品名"
                purchase += ", "
                purchase += "型式"
                purchase += ", "
                purchase += "仕入先名"
                purchase += ", "
                purchase += "仕入値"
                purchase += ", "
                purchase += "発注数量"
                purchase += ", "
                purchase += "仕入数量"
                purchase += ", "
                purchase += "発注残数"
                purchase += ", "
                purchase += "単位"
                purchase += ", "
                purchase += "仕入単価"
                purchase += ", "
                purchase += "仕入金額"
                purchase += ", "
                purchase += "間接費"
                purchase += ", "
                purchase += "リードタイム"
                purchase += ", "
                purchase += "支払有無"
                purchase += ", "
                purchase += "支払番号"
                purchase += ", "
                purchase += "支払日"
                purchase += ", "
                purchase += "備考"
                purchase += ", "
                purchase += "仕入日"
                purchase += ", "
                purchase += "更新者"
                purchase += ", "
                purchase += "更新日"

                _db.executeDB(purchase)

            Next
            Dim sum1 As Double = 0
            Dim sum2 As Double = 0
            Dim unitPrice As Double = 0
            Dim sum3 As Double = 0
            Dim sum4 As Double = 0
            Dim OverHead As Double = 0
            If ds1.Tables(RS).Rows(0)("在庫単価評価法") = 1 Then
                unitPrice = dsPurchase.Tables(RS).Rows(dsPurchase.Tables(RS).Rows.Count)("仕入値")
                OverHead = dsPurchase.Tables(RS).Rows(dsPurchase.Tables(RS).Rows.Count)("間接費")
            Else
                sum1 = dszaiko.Tables(RS).Rows(i)("今月単価") * dszaiko.Tables(RS).Rows(i)("今月末数量")
                sum1 += PurchaseSum
                sum2 = dszaiko.Tables(RS).Rows(i)("今月末数量") + PurchaseQuantity
                unitPrice = sum1 / sum2
                sum3 = dszaiko.Tables(RS).Rows(i)("今月間接費") * dszaiko.Tables(RS).Rows(i)("今月末数量")
                sum3 += OverheadSum
                sum4 = dszaiko.Tables(RS).Rows(i)("今月末数量") + PurchaseQuantity
                OverHead = sum3 / sum4
            End If



            SqlZaiko = ""
            SqlZaiko += "UPDATE "
            SqlZaiko += "Public."
            SqlZaiko += "t50_zikhd "
            SqlZaiko += "SET "
            SqlZaiko += " 前月末数量"
            SqlZaiko += " = '"
            SqlZaiko += dszaiko.Tables(RS).Rows(i)("今月末数量").ToString
            SqlZaiko += "', "
            SqlZaiko += " 前月末単価"
            SqlZaiko += " = '"
            SqlZaiko += dszaiko.Tables(RS).Rows(i)("今月単価").ToString
            SqlZaiko += "', "
            SqlZaiko += " 前月末間接費"
            SqlZaiko += " = '"
            SqlZaiko += dszaiko.Tables(RS).Rows(i)("今月間接費").ToString
            SqlZaiko += "', "
            SqlZaiko += "今月末数量"
            SqlZaiko += " = '"
            SqlZaiko += "0"
            SqlZaiko += "', "
            SqlZaiko += "今月単価"
            SqlZaiko += " = '"
            SqlZaiko += unitPrice.ToString
            SqlZaiko += "', "
            SqlZaiko += "今月入庫数"
            SqlZaiko += " = '"
            SqlZaiko += "0"
            SqlZaiko += "', "
            SqlZaiko += "今月出庫数"
            SqlZaiko += " = '"
            SqlZaiko += "0"
            SqlZaiko += "', "
            SqlZaiko += "今月間接費"
            SqlZaiko += " = '"
            SqlZaiko += OverHead.ToString
            SqlZaiko += "', "
            SqlZaiko += "更新者"
            SqlZaiko += " = '"
            SqlZaiko += frmC01F10_Login.loginValue.TantoNM
            SqlZaiko += "', "
            SqlZaiko += "更新日"
            SqlZaiko += " = '"
            SqlZaiko += dtToday
            SqlZaiko += "' "

            SqlZaiko += " WHERE "
            SqlZaiko += "会社コード"
            SqlZaiko += " ILIKE  "
            SqlZaiko += "'"
            SqlZaiko += frmC01F10_Login.loginValue.BumonNM
            SqlZaiko += "'"
            SqlZaiko += " AND "
            SqlZaiko += "メーカー"
            SqlZaiko += " ILIKE  "
            SqlZaiko += "'"
            SqlZaiko += dszaiko.Tables(RS).Rows(i)("メーカー")
            SqlZaiko += "'"
            SqlZaiko += " AND "
            SqlZaiko += "品名"
            SqlZaiko += " ILIKE  "
            SqlZaiko += "'"
            SqlZaiko += dszaiko.Tables(RS).Rows(i)("品名")
            SqlZaiko += "'"
            SqlZaiko += " AND "
            SqlZaiko += "型式"
            SqlZaiko += " ILIKE  "
            SqlZaiko += "'"
            SqlZaiko += dszaiko.Tables(RS).Rows(i)("型式")
            SqlZaiko += "'"

            SqlZaiko += "RETURNING 会社コード"
            SqlZaiko += ", "
            SqlZaiko += "年月"
            SqlZaiko += ", "
            SqlZaiko += "メーカー"
            SqlZaiko += ", "
            SqlZaiko += "品名"
            SqlZaiko += ", "
            SqlZaiko += "型式"
            SqlZaiko += ", "
            SqlZaiko += "前月末数量"
            SqlZaiko += ", "
            SqlZaiko += "前月末間接費"
            SqlZaiko += ", "
            SqlZaiko += "今月末数量"
            SqlZaiko += ", "
            SqlZaiko += "今月入庫数"
            SqlZaiko += ", "
            SqlZaiko += "今月出庫数"
            SqlZaiko += ", "
            SqlZaiko += "今月間接費"
            SqlZaiko += ", "
            SqlZaiko += "前月末単価"
            SqlZaiko += ", "
            SqlZaiko += "今月単価"
            SqlZaiko += ", "
            SqlZaiko += "更新者"
            SqlZaiko += ", "
            SqlZaiko += "更新日"

            _db.executeDB(SqlZaiko)
        Next


        For i As Integer = 0 To dszaiko.Tables(RS).Rows.Count - 1
            SqlZaiko = ""
            SqlZaiko += "UPDATE "
            SqlZaiko += "Public."
            SqlZaiko += "t50_zikhd "
            SqlZaiko += "SET "
            SqlZaiko += " 前月末数量"
            SqlZaiko += " = '"
            SqlZaiko += dszaiko.Tables(RS).Rows(i)("今月末数量").ToString
            SqlZaiko += "', "
            SqlZaiko += "今月末数量"
            SqlZaiko += " = '"
            SqlZaiko += "0"
            SqlZaiko += "', "
            SqlZaiko += "今月入庫数"
            SqlZaiko += " = '"
            SqlZaiko += "0"
            SqlZaiko += "', "
            SqlZaiko += "今月出庫数"
            SqlZaiko += " = '"
            SqlZaiko += "0"
            SqlZaiko += "', "
            SqlZaiko += "更新者"
            SqlZaiko += " = '"
            SqlZaiko += frmC01F10_Login.loginValue.TantoNM
            SqlZaiko += "', "
            SqlZaiko += "更新日"
            SqlZaiko += " = '"
            SqlZaiko += dtToday
            SqlZaiko += "' "

            SqlZaiko += " WHERE "
            SqlZaiko += "会社コード"
            SqlZaiko += " ILIKE  "
            SqlZaiko += "'"
            SqlZaiko += frmC01F10_Login.loginValue.BumonNM
            SqlZaiko += "'"
            SqlZaiko += " AND "
            SqlZaiko += "メーカー"
            SqlZaiko += " ILIKE  "
            SqlZaiko += "'"
            SqlZaiko += dszaiko.Tables(RS).Rows(i)("メーカー")
            SqlZaiko += "'"
            SqlZaiko += " AND "
            SqlZaiko += "品名"
            SqlZaiko += " ILIKE  "
            SqlZaiko += "'"
            SqlZaiko += dszaiko.Tables(RS).Rows(i)("品名")
            SqlZaiko += "'"
            SqlZaiko += " AND "
            SqlZaiko += "型式"
            SqlZaiko += " ILIKE  "
            SqlZaiko += "'"
            SqlZaiko += dszaiko.Tables(RS).Rows(i)("型式")
            SqlZaiko += "'"

            SqlZaiko += "RETURNING 会社コード"
            SqlZaiko += ", "
            SqlZaiko += "年月"
            SqlZaiko += ", "
            SqlZaiko += "メーカー"
            SqlZaiko += ", "
            SqlZaiko += "品名"
            SqlZaiko += ", "
            SqlZaiko += "型式"
            SqlZaiko += ", "
            SqlZaiko += "前月末数量"
            SqlZaiko += ", "
            SqlZaiko += "前月末間接費"
            SqlZaiko += ", "
            SqlZaiko += "今月末数量"
            SqlZaiko += ", "
            SqlZaiko += "今月入庫数"
            SqlZaiko += ", "
            SqlZaiko += "今月出庫数"
            SqlZaiko += ", "
            SqlZaiko += "今月間接費"
            SqlZaiko += ", "
            SqlZaiko += "更新者"
            SqlZaiko += ", "
            SqlZaiko += "更新日"

            _db.executeDB(SqlZaiko)
        Next

        Dim Sql7 As String = ""
        For i As Integer = 0 To ds6.Tables(RS).Rows.Count - 1
            Sql7 += "SELECT "
            Sql7 += "* "
            Sql7 += "FROM "
            Sql7 += "public"
            Sql7 += "."
            Sql7 += "t50_zikhd"
            Sql7 += " WHERE "
            Sql7 += "会社コード"
            Sql7 += " ILIKE  "
            Sql7 += "'"
            Sql7 += frmC01F10_Login.loginValue.BumonNM
            Sql7 += "'"
            Sql7 += " AND "
            Sql7 += "メーカー"
            Sql7 += " ILIKE  "
            Sql7 += "'"
            Sql7 += ds6.Tables(RS).Rows(i)("メーカー")
            Sql7 += "'"
            Sql7 += " AND "
            Sql7 += "品名"
            Sql7 += " ILIKE  "
            Sql7 += "'"
            Sql7 += ds6.Tables(RS).Rows(i)("品名")
            Sql7 += "'"
            Sql7 += " AND "
            Sql7 += "型式"
            Sql7 += " ILIKE  "
            Sql7 += "'"
            Sql7 += ds6.Tables(RS).Rows(i)("型式")
            Sql7 += "'"

            Dim ds7 As DataSet = _db.selectDB(Sql7, RS, reccnt)
            Sql7 = ""

            Dim Sql8 As String = ""

            If ds7.Tables(RS).Rows.Count = 0 Then
                Sql8 = ""
                Sql8 += "INSERT INTO "
                Sql8 += "Public."
                Sql8 += "t50_zikhd("
                Sql8 += "会社コード, 年月, メーカー, 品名, 型式, 前月末数量, 前月末間接費, 今月末数量, 今月入庫数, 今月出庫数, 今月間接費, 更新者, 更新日,前月末単価, 今月単価)"
                Sql8 += " VALUES('"
                Sql8 += frmC01F10_Login.loginValue.BumonNM
                Sql8 += "', '"
                Sql8 += dtToday
                Sql8 += "', '"
                Sql8 += ds6.Tables(RS).Rows(i)("メーカー")
                Sql8 += "', '"
                Sql8 += ds6.Tables(RS).Rows(i)("品名")
                Sql8 += "', '"
                Sql8 += ds6.Tables(RS).Rows(i)("型式")
                Sql8 += "', '"
                Sql8 += "0"
                Sql8 += "', '"
                Sql8 += "0"
                Sql8 += "', '"
                Sql8 += ds6.Tables(RS).Rows(i)("仕入数量").ToString
                Sql8 += "', '"
                Sql8 += ds6.Tables(RS).Rows(i)("仕入数量").ToString
                Sql8 += "', '"
                Sql8 += "0"
                Sql8 += "', '"
                Sql8 += ds6.Tables(RS).Rows(i)("間接費").ToString
                Sql8 += "', '"
                Sql8 += frmC01F10_Login.loginValue.TantoNM
                Sql8 += "', '"
                Sql8 += dtToday
                Sql8 += "', '"
                Sql8 += "0"
                Sql8 += "', '"
                Sql8 += ds6.Tables(RS).Rows(i)("仕入単価").ToString
                Sql8 += " ')"
                Sql8 += "RETURNING 会社コード"
                Sql8 += ", "
                Sql8 += "年月"
                Sql8 += ", "
                Sql8 += "メーカー"
                Sql8 += ", "
                Sql8 += "品名"
                Sql8 += ", "
                Sql8 += "型式"
                Sql8 += ", "
                Sql8 += "前月末数量"
                Sql8 += ", "
                Sql8 += "前月末間接費"
                Sql8 += ", "
                Sql8 += "今月末数量"
                Sql8 += ", "
                Sql8 += "今月入庫数"
                Sql8 += ", "
                Sql8 += "今月出庫数"
                Sql8 += ", "
                Sql8 += "今月間接費"
                Sql8 += ", "
                Sql8 += "更新者"
                Sql8 += ", "
                Sql8 += "更新日"
                Sql8 += ", "
                Sql8 += "前月末単価"
                Sql8 += ", "
                Sql8 += "今月単価"
                _db.executeDB(Sql8)
            Else
                Dim tmp1 As Double = 0
                Dim tmp2 As Double = 0
                Sql8 = ""
                Sql8 += "UPDATE "
                Sql8 += "Public."
                Sql8 += "t50_zikhd "
                Sql8 += "SET "
                Sql8 += " 今月末数量"
                Sql8 += " = '"
                tmp1 = ds7.Tables(RS).Rows(0)("今月末数量") + ds6.Tables(RS).Rows(i)("仕入数量")
                Sql8 += tmp1.ToString
                Sql8 += "', "
                Sql8 += "今月入庫数"
                Sql8 += " = '"
                tmp2 = ds7.Tables(RS).Rows(0)("今月入庫数") + ds6.Tables(RS).Rows(i)("仕入数量")
                Sql8 += tmp2.ToString
                Sql8 += "', "
                Sql8 += "更新者"
                Sql8 += " = '"
                Sql8 += frmC01F10_Login.loginValue.TantoNM
                Sql8 += "', "
                Sql8 += "更新日"
                Sql8 += " = '"
                Sql8 += dtToday
                Sql8 += "' "

                Sql8 += " WHERE "
                Sql8 += "会社コード"
                Sql8 += " ILIKE  "
                Sql8 += "'"
                Sql8 += frmC01F10_Login.loginValue.BumonNM
                Sql8 += "'"
                Sql8 += " AND "
                Sql8 += "メーカー"
                Sql8 += " ILIKE  "
                Sql8 += "'"
                Sql8 += ds6.Tables(RS).Rows(i)("メーカー")
                Sql8 += "'"
                Sql8 += " AND "
                Sql8 += "品名"
                Sql8 += " ILIKE  "
                Sql8 += "'"
                Sql8 += ds6.Tables(RS).Rows(i)("品名")
                Sql8 += "'"
                Sql8 += " AND "
                Sql8 += "型式"
                Sql8 += " ILIKE  "
                Sql8 += "'"
                Sql8 += ds6.Tables(RS).Rows(i)("型式")
                Sql8 += "'"

                Sql8 += "RETURNING 会社コード"
                Sql8 += ", "
                Sql8 += "年月"
                Sql8 += ", "
                Sql8 += "メーカー"
                Sql8 += ", "
                Sql8 += "品名"
                Sql8 += ", "
                Sql8 += "型式"
                Sql8 += ", "
                Sql8 += "前月末数量"
                Sql8 += ", "
                Sql8 += "前月末間接費"
                Sql8 += ", "
                Sql8 += "今月末数量"
                Sql8 += ", "
                Sql8 += "今月入庫数"
                Sql8 += ", "
                Sql8 += "今月出庫数"
                Sql8 += ", "
                Sql8 += "今月間接費"
                Sql8 += ", "
                Sql8 += "更新者"
                Sql8 += ", "
                Sql8 += "更新日"

                _db.executeDB(Sql8)
            End If
        Next

        Sql7 = ""
        For i As Integer = 0 To ds5.Tables(RS).Rows.Count - 1
            Sql7 += "SELECT "
            Sql7 += "* "
            Sql7 += "FROM "
            Sql7 += "public"
            Sql7 += "."
            Sql7 += "t50_zikhd"
            Sql7 += " WHERE "
            Sql7 += "会社コード"
            Sql7 += " ILIKE  "
            Sql7 += "'"
            Sql7 += frmC01F10_Login.loginValue.BumonNM
            Sql7 += "'"
            Sql7 += " AND "
            Sql7 += "メーカー"
            Sql7 += " ILIKE  "
            Sql7 += "'"
            Sql7 += ds5.Tables(RS).Rows(i)("メーカー")
            Sql7 += "'"
            Sql7 += " AND "
            Sql7 += "品名"
            Sql7 += " ILIKE  "
            Sql7 += "'"
            Sql7 += ds5.Tables(RS).Rows(i)("品名")
            Sql7 += "'"
            Sql7 += " AND "
            Sql7 += "型式"
            Sql7 += " ILIKE  "
            Sql7 += "'"
            Sql7 += ds5.Tables(RS).Rows(i)("型式")
            Sql7 += "'"

            Dim ds7 As DataSet = _db.selectDB(Sql7, RS, reccnt)
            Sql7 = ""

            Dim Sql9 As String = ""

            If ds7.Tables(RS).Rows.Count = 0 Then
                Sql9 = ""
                Sql9 += "INSERT INTO "
                Sql9 += "Public."
                Sql9 += "t50_zikhd("
                Sql9 += "会社コード, 年月, メーカー, 品名, 型式, 前月末数量, 前月末間接費, 今月末数量, 今月入庫数, 今月出庫数, 今月間接費, 更新者, 更新日, 前月末単価, 今月単価)"
                Sql9 += " VALUES('"
                Sql9 += frmC01F10_Login.loginValue.BumonNM
                Sql9 += "', '"
                Sql9 += dtToday
                Sql9 += "', '"
                Sql9 += ds5.Tables(RS).Rows(i)("メーカー")
                Sql9 += "', '"
                Sql9 += ds5.Tables(RS).Rows(i)("品名")
                Sql9 += "', '"
                Sql9 += ds5.Tables(RS).Rows(i)("型式")
                Sql9 += "', '"
                Sql9 += "0"
                Sql9 += "', '"
                Sql9 += "0"
                Sql9 += "', '"
                Sql9 += ds5.Tables(RS).Rows(i)("売上数量").ToString
                Sql9 += "', '"
                Sql9 += "0"
                Sql9 += "', '"
                Sql9 += ds5.Tables(RS).Rows(i)("売上数量").ToString
                Sql9 += "', '"
                Sql9 += "0"
                Sql9 += "', '"
                Sql9 += frmC01F10_Login.loginValue.TantoNM
                Sql9 += "', '"
                Sql9 += dtToday
                Sql9 += "', '"
                Sql9 += "0"
                Sql9 += "', '"
                Sql9 += "0"
                Sql9 += " ')"
                Sql9 += "RETURNING 会社コード"
                Sql9 += ", "
                Sql9 += "年月"
                Sql9 += ", "
                Sql9 += "メーカー"
                Sql9 += ", "
                Sql9 += "品名"
                Sql9 += ", "
                Sql9 += "型式"
                Sql9 += ", "
                Sql9 += "前月末数量"
                Sql9 += ", "
                Sql9 += "前月末間接費"
                Sql9 += ", "
                Sql9 += "今月末数量"
                Sql9 += ", "
                Sql9 += "今月入庫数"
                Sql9 += ", "
                Sql9 += "今月出庫数"
                Sql9 += ", "
                Sql9 += "今月間接費"
                Sql9 += ", "
                Sql9 += "更新者"
                Sql9 += ", "
                Sql9 += "更新日"
                Sql9 += ", "
                Sql9 += "前月末単価"
                Sql9 += ", "
                Sql9 += "今月単価"

                _db.executeDB(Sql9)

            Else
                Dim tmp3 As Double = 0
                Dim tmp4 As Double = 0
                Sql9 = ""
                Sql9 += "UPDATE "
                Sql9 += "Public."
                Sql9 += "t50_zikhd "
                Sql9 += "SET "
                Sql9 += " 今月末数量"
                Sql9 += " = '"
                tmp3 = ds7.Tables(RS).Rows(0)("今月末数量") - ds5.Tables(RS).Rows(i)("売上数量")
                Sql9 += tmp3.ToString
                Sql9 += "', "
                Sql9 += "今月出庫数"
                Sql9 += " = '"
                tmp4 = ds7.Tables(RS).Rows(0)("今月出庫数") + ds5.Tables(RS).Rows(i)("売上数量")
                Sql9 += tmp4.ToString
                Sql9 += "', "
                Sql9 += "更新者"
                Sql9 += " = '"
                Sql9 += frmC01F10_Login.loginValue.TantoNM
                Sql9 += "', "
                Sql9 += "更新日"
                Sql9 += " = '"
                Sql9 += dtToday
                Sql9 += "' "

                Sql9 += " WHERE "
                Sql9 += "会社コード"
                Sql9 += " ILIKE  "
                Sql9 += "'"
                Sql9 += frmC01F10_Login.loginValue.BumonNM
                Sql9 += "'"
                Sql9 += " AND "
                Sql9 += "メーカー"
                Sql9 += " ILIKE  "
                Sql9 += "'"
                Sql9 += ds5.Tables(RS).Rows(i)("メーカー")
                Sql9 += "'"
                Sql9 += " AND "
                Sql9 += "品名"
                Sql9 += " ILIKE  "
                Sql9 += "'"
                Sql9 += ds5.Tables(RS).Rows(i)("品名")
                Sql9 += "'"
                Sql9 += " AND "
                Sql9 += "型式"
                Sql9 += " ILIKE  "
                Sql9 += "'"
                Sql9 += ds5.Tables(RS).Rows(i)("型式")
                Sql9 += "'"

                Sql9 += "RETURNING 会社コード"
                Sql9 += ", "
                Sql9 += "年月"
                Sql9 += ", "
                Sql9 += "メーカー"
                Sql9 += ", "
                Sql9 += "品名"
                Sql9 += ", "
                Sql9 += "型式"
                Sql9 += ", "
                Sql9 += "前月末数量"
                Sql9 += ", "
                Sql9 += "前月末間接費"
                Sql9 += ", "
                Sql9 += "今月末数量"
                Sql9 += ", "
                Sql9 += "今月入庫数"
                Sql9 += ", "
                Sql9 += "今月出庫数"
                Sql9 += ", "
                Sql9 += "今月間接費"
                Sql9 += ", "
                Sql9 += "更新者"
                Sql9 += ", "
                Sql9 += "更新日"

                _db.executeDB(Sql9)
            End If
        Next

        Dim Sql10 As String = ""
        Sql10 += "SELECT "
        Sql10 += "* "
        Sql10 += "FROM "
        Sql10 += "public"
        Sql10 += "."
        Sql10 += "t50_zikhd"

        Dim ds10 As DataSet = _db.selectDB(Sql10, RS, reccnt)

        Dim Sql11 As String = ""
        For i As Integer = 0 To ds10.Tables(RS).Rows.Count - 1
            Dim tmp5 As Double = 0
            Sql11 = ""
            Sql11 += "UPDATE "
            Sql11 += "Public."
            Sql11 += "t50_zikhd "
            Sql11 += "SET "
            Sql11 += " 今月末数量"
            Sql11 += " = '"
            tmp5 = ds10.Tables(RS).Rows(i)("今月末数量") + ds10.Tables(RS).Rows(i)("前月末数量")
            Sql11 += tmp5.ToString
            Sql11 += "', "
            Sql11 += "更新者"
            Sql11 += " = '"
            Sql11 += frmC01F10_Login.loginValue.TantoNM
            Sql11 += "', "
            Sql11 += "更新日"
            Sql11 += " = '"
            Sql11 += dtToday
            Sql11 += "' "

            Sql11 += " WHERE "
            Sql11 += "会社コード"
            Sql11 += " ILIKE  "
            Sql11 += "'"
            Sql11 += frmC01F10_Login.loginValue.BumonNM
            Sql11 += "'"
            Sql11 += " AND "
            Sql11 += "メーカー"
            Sql11 += " ILIKE  "
            Sql11 += "'"
            Sql11 += ds10.Tables(RS).Rows(i)("メーカー")
            Sql11 += "'"
            Sql11 += " AND "
            Sql11 += "品名"
            Sql11 += " ILIKE  "
            Sql11 += "'"
            Sql11 += ds10.Tables(RS).Rows(i)("品名")
            Sql11 += "'"
            Sql11 += " AND "
            Sql11 += "型式"
            Sql11 += " ILIKE  "
            Sql11 += "'"
            Sql11 += ds10.Tables(RS).Rows(i)("型式")
            Sql11 += "'"

            Sql11 += "RETURNING 会社コード"
            Sql11 += ", "
            Sql11 += "年月"
            Sql11 += ", "
            Sql11 += "メーカー"
            Sql11 += ", "
            Sql11 += "品名"
            Sql11 += ", "
            Sql11 += "型式"
            Sql11 += ", "
            Sql11 += "前月末数量"
            Sql11 += ", "
            Sql11 += "前月末間接費"
            Sql11 += ", "
            Sql11 += "今月末数量"
            Sql11 += ", "
            Sql11 += "今月入庫数"
            Sql11 += ", "
            Sql11 += "今月出庫数"
            Sql11 += ", "
            Sql11 += "今月間接費"
            Sql11 += ", "
            Sql11 += "更新者"
            Sql11 += ", "
            Sql11 += "更新日"

            _db.executeDB(Sql11)
        Next
        Dim Sql12 As String = ""
        For i As Integer = 0 To ds5.Tables(RS).Rows.Count - 1
            Sql12 = ""
            Sql12 += "UPDATE "
            Sql12 += "Public."
            Sql12 += "t30_urighd "
            Sql12 += "SET "
            Sql12 += "締処理日 "
            Sql12 += " = '"
            Sql12 += dtToday
            Sql12 += "', "
            Sql12 += "更新者"
            Sql12 += " = '"
            Sql12 += frmC01F10_Login.loginValue.TantoNM
            Sql12 += "', "
            Sql12 += "更新日"
            Sql12 += " = '"
            Sql12 += dtToday
            Sql12 += "' "

            Sql12 += " WHERE "
            Sql12 += "会社コード"
            Sql12 += " ILIKE  "
            Sql12 += "'"
            Sql12 += frmC01F10_Login.loginValue.BumonNM
            Sql12 += "'"
            Sql12 += " AND "
            Sql12 += "売上番号"
            Sql12 += " ILIKE  "
            Sql12 += "'"
            Sql12 += ds5.Tables(RS).Rows(i)("売上番号")
            Sql12 += "'"

            Sql12 += "RETURNING 会社コード"
            Sql12 += ", "
            Sql12 += "売上番号"
            Sql12 += ", "
            Sql12 += "売上番号枝番"
            Sql12 += ", "
            Sql12 += "客先番号"
            Sql12 += ", "
            Sql12 += "受注番号"
            Sql12 += ", "
            Sql12 += "受注番号枝番"
            Sql12 += ", "
            Sql12 += "見積番号"
            Sql12 += ", "
            Sql12 += "見積番号枝番"
            Sql12 += ", "
            Sql12 += "得意先コード"
            Sql12 += ", "
            Sql12 += "得意先名"
            Sql12 += ", "
            Sql12 += "得意先郵便番号"
            Sql12 += ", "
            Sql12 += "得意先住所"
            Sql12 += ", "
            Sql12 += "得意先電話番号"
            Sql12 += ", "
            Sql12 += "得意先ＦＡＸ"
            Sql12 += ", "
            Sql12 += "得意先担当者役職"
            Sql12 += ", "
            Sql12 += "得意先担当者名"
            Sql12 += ", "
            Sql12 += "見積日"
            Sql12 += ", "
            Sql12 += "見積有効期限"
            Sql12 += ", "
            Sql12 += "支払条件"
            Sql12 += ", "
            Sql12 += "見積金額"
            Sql12 += ", "
            Sql12 += "売上金額"
            Sql12 += ", "
            Sql12 += "粗利額"
            Sql12 += ", "
            Sql12 += "営業担当者"
            Sql12 += ", "
            Sql12 += "入力担当者"
            Sql12 += ", "
            Sql12 += "備考"
            Sql12 += ", "
            Sql12 += "取消日"
            Sql12 += ", "
            Sql12 += "取消区分"
            Sql12 += ", "
            Sql12 += "ＶＡＴ"
            Sql12 += ", "
            Sql12 += "ＰＰＨ"
            Sql12 += ", "
            Sql12 += "受注日"
            Sql12 += ", "
            Sql12 += "売上日"
            Sql12 += ", "
            Sql12 += "入金予定日"
            Sql12 += ", "
            Sql12 += "登録日"
            Sql12 += ", "
            Sql12 += "更新日"
            Sql12 += ", "
            Sql12 += "更新者"

            _db.executeDB(Sql12)
        Next

        Dim Sql13 As String = ""
        For i As Integer = 0 To ds6.Tables(RS).Rows.Count - 1
            Sql13 = ""
            Sql13 += "UPDATE "
            Sql13 += "Public."
            Sql13 += "t40_sirehd "
            Sql13 += "SET "
            Sql13 += "締処理日 "
            Sql13 += " = '"
            Sql13 += dtToday
            Sql13 += "', "
            Sql13 += "更新者"
            Sql13 += " = '"
            Sql13 += frmC01F10_Login.loginValue.TantoNM
            Sql13 += "', "
            Sql13 += "更新日"
            Sql13 += " = '"
            Sql13 += dtToday
            Sql13 += "' "

            Sql13 += " WHERE "
            Sql13 += "会社コード"
            Sql13 += " ILIKE  "
            Sql13 += "'"
            Sql13 += frmC01F10_Login.loginValue.BumonNM
            Sql13 += "'"
            Sql13 += " AND "
            Sql13 += "仕入番号"
            Sql13 += " ILIKE  "
            Sql13 += "'"
            Sql13 += ds6.Tables(RS).Rows(i)("仕入番号")
            Sql13 += "'"

            Sql13 += "RETURNING 締処理日"
            Sql13 += ", "
            Sql13 += "更新日"
            Sql13 += ", "
            Sql13 += "更新者"

            _db.executeDB(Sql13)
        Next

        Dim Sql14 As String = ""

        Sql14 = ""
        Sql14 += "INSERT INTO "
        Sql14 += "Public."
        Sql14 += "t51_smlog("
        Sql14 += "会社コード, 処理日時, 前回締日, 今回締日, 次回締日, 担当者)"
        Sql14 += " VALUES('"
        Sql14 += frmC01F10_Login.loginValue.BumonNM
        Sql14 += "', '"
        Sql14 += dtToday
        Sql14 += "', '"
        Sql14 += ds1.Tables(RS).Rows(0)("前回締日")
        Sql14 += "', '"
        Sql14 += ds1.Tables(RS).Rows(0)("今回締日")
        Sql14 += "', '"
        Sql14 += ds1.Tables(RS).Rows(0)("次回締日")
        Sql14 += "', '"
        Sql14 += frmC01F10_Login.loginValue.TantoNM
        Sql14 += " ')"
        Sql14 += "RETURNING "
        Sql14 += "会社コード"
        Sql14 += ", "
        Sql14 += "処理日時"
        Sql14 += ", "
        Sql14 += "前回締日"
        Sql14 += ", "
        Sql14 += "今回締日"
        Sql14 += ", "
        Sql14 += "次回締日"
        Sql14 += ", "
        Sql14 += "担当者"

        _db.executeDB(Sql14)

        Dim thisClosingDate As DateTime = DateTime.Parse(ds1.Tables(RS).Rows(0)("次回締日"))
        Dim dtYear As Integer = thisClosingDate.Year
        Dim dtMonth As Integer = thisClosingDate.Month
        If dtMonth < 12 Then
            dtMonth += 1
        Else
            dtYear += 1
            dtMonth = 1
        End If

        Dim dtdays As Integer = DateTime.DaysInMonth(dtYear, dtMonth)
        Dim nextClosingDate = New DateTime(dtYear, dtMonth, dtdays)
        Dim Sql15 As String = ""
        Sql15 = ""
        Sql15 += "UPDATE "
        Sql15 += "Public."
        Sql15 += "m01_company "
        Sql15 += "SET "
        Sql15 += "前回締日 "
        Sql15 += " = '"
        Sql15 += ds1.Tables(RS).Rows(0)("今回締日")
        Sql15 += "', "
        Sql15 += "今回締日"
        Sql15 += " = '"
        Sql15 += ds1.Tables(RS).Rows(0)("次回締日")
        Sql15 += "', "
        Sql15 += "次回締日"
        Sql15 += " = '"
        Sql15 += nextClosingDate
        Sql15 += "' "

        Sql15 += " WHERE "
        Sql15 += "会社コード"
        Sql15 += " ILIKE  "
        Sql15 += "'"
        Sql15 += frmC01F10_Login.loginValue.BumonNM
        Sql15 += "'"

        Sql15 += "RETURNING 会社コード"
        Sql15 += ", "
        Sql15 += "会社名"
        Sql15 += ", "
        Sql15 += "会社略称"
        Sql15 += ", "
        Sql15 += "郵便番号"
        Sql15 += ", "
        Sql15 += "住所１"
        Sql15 += ", "
        Sql15 += "住所２"
        Sql15 += ", "
        Sql15 += "住所３"
        Sql15 += ", "
        Sql15 += "電話番号"
        Sql15 += ", "
        Sql15 += "ＦＡＸ番号"
        Sql15 += ", "
        Sql15 += "代表者役職"
        Sql15 += ", "
        Sql15 += "代表者名"
        Sql15 += ", "
        Sql15 += "表示順"
        Sql15 += ", "
        Sql15 += "備考"
        Sql15 += ", "
        Sql15 += "銀行コード"
        Sql15 += ", "
        Sql15 += "支店コード"
        Sql15 += ", "
        Sql15 += "預金種目"
        Sql15 += ", "
        Sql15 += "口座番号"
        Sql15 += ", "
        Sql15 += "口座名義"
        Sql15 += ", "
        Sql15 += "更新者"
        Sql15 += ", "
        Sql15 += "更新日"
        Sql15 += ", "
        Sql15 += "前回締日"
        Sql15 += ", "
        Sql15 += "今回締日"
        Sql15 += ", "
        Sql15 += "次回締日"
        Sql15 += ", "
        Sql15 += "在庫単価評価法"

        _db.executeDB(Sql15)

        '請求締処理日更新
        Dim Sql16 As String = ""
        For i As Integer = 0 To ds4.Tables(RS).Rows.Count - 1
            Sql16 = ""
            Sql16 += "UPDATE "
            Sql16 += "Public."
            Sql16 += "t23_skyuhd "
            Sql16 += "SET "
            Sql16 += "締処理日 "
            Sql16 += " = '"
            Sql16 += dtToday
            Sql16 += "', "
            Sql16 += "更新者"
            Sql16 += " = '"
            Sql16 += frmC01F10_Login.loginValue.TantoNM
            Sql16 += "' "


            Sql16 += " WHERE "
            Sql16 += "会社コード"
            Sql16 += " ILIKE  "
            Sql16 += "'"
            Sql16 += frmC01F10_Login.loginValue.BumonNM
            Sql16 += "'"
            Sql16 += " AND "
            Sql16 += "請求番号"
            Sql16 += " ILIKE  "
            Sql16 += "'"
            Sql16 += ds4.Tables(RS).Rows(i)("請求番号")
            Sql16 += "'"

            Sql16 += "RETURNING 締処理日"
            Sql16 += ", "
            Sql16 += "更新者"

            _db.executeDB(Sql16)
        Next


        '買掛締処理日更新
        Dim Sql17 As String = ""
        For i As Integer = 0 To dsKike.Tables(RS).Rows.Count - 1
            Sql17 = ""
            Sql17 += "UPDATE "
            Sql17 += "Public."
            Sql17 += "t46_kikehd "
            Sql17 += "SET "
            Sql17 += "締処理日 "
            Sql17 += " = '"
            Sql17 += dtToday
            Sql17 += "', "
            Sql17 += "更新者"
            Sql17 += " = '"
            Sql17 += frmC01F10_Login.loginValue.TantoNM
            Sql17 += "' "

            Sql17 += " WHERE "
            Sql17 += "会社コード"
            Sql17 += " ILIKE  "
            Sql17 += "'"
            Sql17 += frmC01F10_Login.loginValue.BumonNM
            Sql17 += "'"
            Sql17 += " AND "
            Sql17 += "買掛番号"
            Sql17 += " ILIKE  "
            Sql17 += "'"
            Sql17 += dsKike.Tables(RS).Rows(i)("買掛番号")
            Sql17 += "'"

            Sql17 += "RETURNING 締処理日"
            Sql17 += ", "
            Sql17 += "更新者"

            _db.executeDB(Sql17)
        Next

        '仕入明細締処理日更新
        Dim Sql18 As String = ""
        For i As Integer = 0 To ds6.Tables(RS).Rows.Count - 1
            Sql18 = ""
            Sql18 += "UPDATE "
            Sql18 += "Public."
            Sql18 += "t41_siredt "
            Sql18 += "SET "
            Sql18 += "締処理日 "
            Sql18 += " = '"
            Sql18 += dtToday
            Sql18 += "', "
            Sql18 += "更新者"
            Sql18 += " = '"
            Sql18 += frmC01F10_Login.loginValue.TantoNM
            Sql18 += "' "

            Sql18 += " WHERE "
            Sql18 += "会社コード"
            Sql18 += " ILIKE  "
            Sql18 += "'"
            Sql18 += frmC01F10_Login.loginValue.BumonNM
            Sql18 += "'"
            Sql18 += " AND "
            Sql18 += "仕入番号"
            Sql18 += " ILIKE  "
            Sql18 += "'"
            Sql18 += ds6.Tables(RS).Rows(i)("仕入番号")
            Sql18 += "'"

            Sql18 += "RETURNING 締処理日"
            Sql18 += ", "
            Sql18 += "更新者"

            _db.executeDB(Sql18)
        Next


        DgvClosingLog.Rows.Clear()
        ClosingLogLoad()
    End Sub



    Private Sub Accounting()
        Dim dtToday As DateTime = DateTime.Now
        Dim reccnt As Integer = 0
        Dim Sql As String = ""

        Sql = ""
        Sql += "SELECT "
        Sql += "* "
        Sql += "FROM "
        Sql += "public"
        Sql += "."
        Sql += "m01_company"
        Sql += " WHERE "
        Sql += "会社コード"
        Sql += " ILIKE  "
        Sql += "'"
        Sql += frmC01F10_Login.loginValue.BumonNM
        Sql += "'"
        Dim dsCompany As DataSet = _db.selectDB(Sql, RS, reccnt)

#Region "売上"
        Sql = ""
        Sql += "SELECT "
        Sql += "* "
        Sql += "FROM "
        Sql += "public"
        Sql += "."
        Sql += "t30_urighd"
        Sql += " WHERE "
        Sql += "会社コード"
        Sql += " ILIKE  "
        Sql += "'"
        Sql += frmC01F10_Login.loginValue.BumonNM
        Sql += "'"
        Sql += " AND "
        Sql += "売上日"
        Sql += " >  "
        Sql += "'"
        Sql += dsCompany.Tables(RS).Rows(0)("前回締日")
        Sql += "'"
        Sql += " AND "
        Sql += "売上日"
        Sql += " <=  "
        Sql += "'"
        Sql += dsCompany.Tables(RS).Rows(0)("今回締日")
        Sql += "'"
        Sql += " AND "
        Sql += "締処理日"
        Sql += " IS NULL "

        Dim dsUrighd As DataSet = _db.selectDB(Sql, RS, reccnt)

        For i As Integer = 0 To dsUrighd.Tables(RS).Rows.Count - 1
            Sql = ""
            Sql += "INSERT INTO "
            Sql += "Public."
            Sql += "t52_krurighd("
            Sql += "会社コード, 売上番号, 客先番号, 受注番号, 受注番号枝番, 見積番号, 見積番号枝番, 得意先コード, 得意先名, 得意先郵便番号, 得意先住所, 得意先電話番号, 得意先ＦＡＸ, 得意先担当者役職, 得意先担当者名, 見積日, 見積有効期限, 支払条件, 見積金額, 仕入金額, 売上金額, 粗利額, 営業担当者, 入力担当者, 備考, ＶＡＴ, ＰＰＨ, 受注日, 売上日, 締処理日, 入金予定日, 登録日, 更新日, 更新者, 取消日, 取消区分)"
            Sql += " VALUES('"
            Sql += frmC01F10_Login.loginValue.BumonNM
            Sql += "', '"
            Sql += dsUrighd.Tables(RS).Rows(i)("売上番号").ToString
            Sql += "', '"
            Sql += dsUrighd.Tables(RS).Rows(i)("客先番号").ToString
            Sql += "', '"
            Sql += dsUrighd.Tables(RS).Rows(i)("受注番号").ToString
            Sql += "', '"
            Sql += dsUrighd.Tables(RS).Rows(i)("受注番号枝番").ToString
            Sql += "', '"
            Sql += dsUrighd.Tables(RS).Rows(i)("見積番号").ToString
            Sql += "', '"
            Sql += dsUrighd.Tables(RS).Rows(i)("見積番号枝番").ToString
            Sql += "', '"
            Sql += dsUrighd.Tables(RS).Rows(i)("得意先コード").ToString
            Sql += "', '"
            Sql += dsUrighd.Tables(RS).Rows(i)("得意先名").ToString
            Sql += "', '"
            Sql += dsUrighd.Tables(RS).Rows(i)("得意先郵便番号").ToString
            Sql += "', '"
            Sql += dsUrighd.Tables(RS).Rows(i)("得意先住所").ToString
            Sql += "', '"
            Sql += dsUrighd.Tables(RS).Rows(i)("得意先電話番号").ToString
            Sql += "', '"
            Sql += dsUrighd.Tables(RS).Rows(i)("得意先ＦＡＸ").ToString
            Sql += "', '"
            Sql += dsUrighd.Tables(RS).Rows(i)("得意先担当者役職").ToString
            Sql += "', '"
            Sql += dsUrighd.Tables(RS).Rows(i)("得意先担当者名").ToString
            Sql += "', '"
            Sql += dsUrighd.Tables(RS).Rows(i)("見積日").ToString
            Sql += "', '"
            Sql += dsUrighd.Tables(RS).Rows(i)("見積有効期限").ToString
            Sql += "', '"
            Sql += dsUrighd.Tables(RS).Rows(i)("支払条件").ToString
            Sql += "', '"
            Sql += dsUrighd.Tables(RS).Rows(i)("見積金額").ToString
            Sql += "', '"
            Sql += dsUrighd.Tables(RS).Rows(i)("仕入金額").ToString
            Sql += "', '"
            Sql += dsUrighd.Tables(RS).Rows(i)("売上金額").ToString
            Sql += "', '"
            Sql += dsUrighd.Tables(RS).Rows(i)("粗利額").ToString
            Sql += "', '"
            Sql += dsUrighd.Tables(RS).Rows(i)("営業担当者").ToString
            Sql += "', '"
            Sql += dsUrighd.Tables(RS).Rows(i)("入力担当者").ToString
            Sql += "', '"
            Sql += dsUrighd.Tables(RS).Rows(i)("備考").ToString
            Sql += "', '"

            If dsUrighd.Tables(RS).Rows(i)("ＶＡＴ") Is DBNull.Value Then
                Sql += "0"
            Else
                Sql += dsUrighd.Tables(RS).Rows(i)("ＶＡＴ").ToString
            End If
            Sql += "', '"
            If dsUrighd.Tables(RS).Rows(i)("ＰＰＨ") Is DBNull.Value Then
                Sql += "0"
            Else
                Sql += dsUrighd.Tables(RS).Rows(i)("ＰＰＨ").ToString
            End If
            Sql += "', '"
            Sql += dsUrighd.Tables(RS).Rows(i)("受注日").ToString
            Sql += "', '"
            Sql += dsUrighd.Tables(RS).Rows(i)("売上日").ToString
            Sql += "', '"
            Sql += dtToday

            If dsUrighd.Tables(RS).Rows(i)("入金予定日") IsNot DBNull.Value Then
                Sql += "', '"
                Sql += dsUrighd.Tables(RS).Rows(i)("入金予定日").ToString
                Sql += "', '"
            Else
                Sql += "', "
                Sql += "null"
                Sql += ", '"
            End If

            Sql += dsUrighd.Tables(RS).Rows(i)("登録日").ToString
            Sql += "', '"
            Sql += dtToday
            Sql += "', '"
            Sql += frmC01F10_Login.loginValue.TantoNM

            If dsUrighd.Tables(RS).Rows(i)("取消日") IsNot DBNull.Value Then
                Sql += "', '"
                Sql += dsUrighd.Tables(RS).Rows(i)("取消日").ToString
                Sql += "', "
            Else
                Sql += "', "
                Sql += "null"
                Sql += ", "
            End If

            If dsUrighd.Tables(RS).Rows(i)("取消区分") IsNot DBNull.Value Then
                Sql += "'"
                Sql += dsUrighd.Tables(RS).Rows(i)("取消区分").ToString
            Else
                Sql += "0"
            End If

            Sql += " ')"
            Sql += "RETURNING 会社コード"
            Sql += ", "
            Sql += "売上番号"
            Sql += ", "
            Sql += "客先番号"
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
            Sql += "見積日"
            Sql += ", "
            Sql += "見積有効期限"
            Sql += ", "
            Sql += "支払条件"
            Sql += ", "
            Sql += "見積金額"
            Sql += ", "
            Sql += "仕入金額"
            Sql += ", "
            Sql += "売上金額"
            Sql += ", "
            Sql += "粗利額"
            Sql += ", "
            Sql += "営業担当者"
            Sql += ", "
            Sql += "入力担当者"
            Sql += ", "
            Sql += "備考"
            Sql += ", "
            Sql += "ＶＡＴ"
            Sql += ", "
            Sql += "ＰＰＨ"
            Sql += ", "
            Sql += "受注日"
            Sql += ", "
            Sql += "売上日"
            Sql += ", "
            Sql += "締処理日"
            Sql += ", "
            Sql += "入金予定日"
            Sql += ", "
            Sql += "登録日"
            Sql += ", "
            Sql += "更新日"
            Sql += ", "
            Sql += "更新者"
            Sql += ", "
            Sql += "取消日"
            Sql += ", "
            Sql += "取消区分"

            _db.executeDB(Sql)
        Next

        For i As Integer = 0 To dsUrighd.Tables(RS).Rows.Count - 1
            Sql = ""
            Sql += "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t31_urigdt"
            Sql += " WHERE "
            Sql += "売上番号"
            Sql += " ILIKE "
            Sql += "'"
            Sql += dsUrighd.Tables(RS).Rows(i)("売上番号")
            Sql += "'"

            Dim dsUrigdt As DataSet = _db.selectDB(Sql, RS, reccnt)

            If dsCompany.Tables(RS).Rows(0)("在庫単価評価法") = 1 Then
                '先入先出法の場合
                For x As Integer = 0 To dsUrigdt.Tables(RS).Rows.Count - 1
                    Sql = ""
                    Sql += "SELECT "
                    Sql += "* "
                    Sql += "FROM "
                    Sql += "public"
                    Sql += "."
                    Sql += "t41_siredt"
                    Sql += " WHERE "
                    Sql += "会社コード"
                    Sql += " ILIKE  "
                    Sql += "'"
                    Sql += frmC01F10_Login.loginValue.BumonNM
                    Sql += "'"
                    Sql += " AND "
                    Sql += "メーカー"
                    Sql += " ILIKE"
                    Sql += "'"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("メーカー")
                    Sql += "'"
                    Sql += " AND "
                    Sql += "品名"
                    Sql += " ILIKE"
                    Sql += "'"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("品名")
                    Sql += "'"
                    Sql += " AND "
                    Sql += "型式"
                    Sql += " ILIKE"
                    Sql += "'"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("型式")
                    Sql += "'"
                    Sql += " AND "
                    Sql += "仕入日"
                    Sql += " >  "
                    Sql += "'"
                    Sql += dsCompany.Tables(RS).Rows(0)("前回締日")
                    Sql += "'"
                    Sql += " AND "
                    Sql += "仕入日"
                    Sql += " <=  "
                    Sql += "'"
                    Sql += dsCompany.Tables(RS).Rows(0)("今回締日")
                    Sql += "'"
                    Sql += " AND "
                    Sql += "締処理日"
                    Sql += " IS NULL "
                    Sql += " AND "
                    Sql += "カウント IS NULL"

                    Dim dsSiredt As DataSet = _db.selectDB(Sql, RS, reccnt)

                    Dim Count As Integer = dsUrigdt.Tables(RS).Rows(x)("受注数量")
                    Dim Qty As Integer = 0
                    Dim SireQty As Integer = 0
                    Dim LineSuffix As Integer = 0

                    For y As Integer = 0 To dsSiredt.Tables(RS).Rows.Count - 1
                        If Count > 0 Then
                            If Count - dsSiredt.Tables(RS).Rows(y)("発注数量") < 0 Then
                                Qty = Count
                                Count = 0
                                SireQty = dsSiredt.Tables(RS).Rows(y)("発注数量") - dsUrigdt.Tables(RS).Rows(x)("受注数量")
                            Else
                                Qty = dsSiredt.Tables(RS).Rows(y)("発注数量")
                                Count = Count - dsSiredt.Tables(RS).Rows(y)("発注数量")
                                SireQty = 0
                            End If
                            LineSuffix = y + 1

                            Sql = ""
                            Sql += "INSERT INTO "
                            Sql += "Public."
                            Sql += "t53_krurigdt("
                            Sql += "会社コード, 売上番号, 受注番号, 受注番号枝番, 行番号, 行番号枝番, 仕入区分, メーカー, 品名, 型式, 仕入先名, 仕入値, 受注数量, 売上数量, 受注残数, 単位, 仕入原価, 関税率, 関税額, 前払法人税率, 前払法人税額, 輸送費率, 輸送費額, 間接費, 仕入金額, 売単価, 売上金額, 見積単価, 見積金額, 粗利額, 粗利率, リードタイム, 備考, 入金有無, 入金番号, 入金日, 更新者, 更新日)"
                            Sql += " VALUES('"
                            Sql += frmC01F10_Login.loginValue.BumonNM
                            Sql += "', '"
                            Sql += dsUrigdt.Tables(RS).Rows(x)("売上番号").ToString
                            Sql += "', '"
                            Sql += dsUrigdt.Tables(RS).Rows(x)("受注番号").ToString
                            Sql += "', '"
                            Sql += dsUrigdt.Tables(RS).Rows(x)("受注番号枝番").ToString
                            Sql += "', '"
                            Sql += dsUrigdt.Tables(RS).Rows(x)("行番号").ToString
                            Sql += "', '"
                            Sql += LineSuffix.ToString
                            Sql += "', '"
                            Sql += dsUrigdt.Tables(RS).Rows(x)("仕入区分").ToString
                            Sql += "', '"
                            Sql += dsUrigdt.Tables(RS).Rows(x)("メーカー").ToString
                            Sql += "', '"
                            Sql += dsUrigdt.Tables(RS).Rows(x)("品名").ToString
                            Sql += "', '"
                            Sql += dsUrigdt.Tables(RS).Rows(x)("型式").ToString
                            Sql += "', '"
                            Sql += dsUrigdt.Tables(RS).Rows(x)("仕入先名").ToString
                            Sql += "', '"
                            Sql += dsSiredt.Tables(RS).Rows(y)("仕入値").ToString
                            Sql += "', '"
                            Sql += dsUrigdt.Tables(RS).Rows(x)("受注数量").ToString
                            Sql += "', '"
                            Sql += Qty.ToString
                            Sql += "', '"
                            Sql += dsUrigdt.Tables(RS).Rows(x)("受注残数").ToString
                            Sql += "', '"
                            Sql += dsUrigdt.Tables(RS).Rows(x)("単位").ToString
                            Sql += "', '"
                            Sql += dsUrigdt.Tables(RS).Rows(x)("仕入原価").ToString
                            Sql += "', '"
                            Sql += dsUrigdt.Tables(RS).Rows(x)("関税率").ToString
                            Sql += "', '"
                            Sql += dsUrigdt.Tables(RS).Rows(x)("関税額").ToString
                            Sql += "', '"
                            Sql += dsUrigdt.Tables(RS).Rows(x)("前払法人税率").ToString
                            Sql += "', '"
                            Sql += dsUrigdt.Tables(RS).Rows(x)("前払法人税額").ToString
                            Sql += "', '"
                            Sql += dsUrigdt.Tables(RS).Rows(x)("輸送費率").ToString
                            Sql += "', '"
                            Sql += dsUrigdt.Tables(RS).Rows(x)("輸送費額").ToString
                            Sql += "', '"
                            Sql += dsUrigdt.Tables(RS).Rows(x)("間接費").ToString
                            Sql += "', '"
                            Sql += dsUrigdt.Tables(RS).Rows(x)("仕入金額").ToString
                            Sql += "', '"
                            Sql += dsUrigdt.Tables(RS).Rows(x)("売単価").ToString
                            Sql += "', '"
                            Sql += dsUrigdt.Tables(RS).Rows(x)("売上金額").ToString
                            Sql += "', '"
                            Sql += dsUrigdt.Tables(RS).Rows(x)("見積単価").ToString
                            Sql += "', '"
                            Sql += dsUrigdt.Tables(RS).Rows(x)("見積金額").ToString
                            Sql += "', '"
                            Sql += dsUrigdt.Tables(RS).Rows(x)("粗利率").ToString
                            Sql += "', '"
                            Sql += dsUrigdt.Tables(RS).Rows(x)("粗利額").ToString
                            Sql += "', '"
                            Sql += dsUrigdt.Tables(RS).Rows(x)("リードタイム").ToString
                            Sql += "', '"
                            Sql += dsUrigdt.Tables(RS).Rows(x)("備考").ToString

                            If dsUrigdt.Tables(RS).Rows(x)("入金有無") IsNot DBNull.Value Then
                                Sql += "', '"
                                Sql += dsUrigdt.Tables(RS).Rows(x)("入金有無").ToString
                                Sql += "', '"
                            Else
                                Sql += "', "
                                Sql += "0"
                                Sql += ", '"
                            End If


                            Sql += dsUrigdt.Tables(RS).Rows(x)("入金番号").Value

                            If dsUrigdt.Tables(RS).Rows(x)("入金日") IsNot DBNull.Value Then
                                Sql += "', '"
                                Sql += dsUrigdt.Tables(RS).Rows(x)("入金日").ToString
                                Sql += "', '"
                            Else
                                Sql += "', "
                                Sql += "null"
                                Sql += ", '"
                            End If

                            Sql += frmC01F10_Login.loginValue.TantoNM
                            Sql += "', '"
                            Sql += dtToday
                            Sql += " ')"
                            Sql += "RETURNING 会社コード"
                            Sql += ", "
                            Sql += "売上番号"
                            Sql += ", "
                            Sql += "受注番号"
                            Sql += ", "
                            Sql += "受注番号枝番"
                            Sql += ", "
                            Sql += "行番号"
                            Sql += ", "
                            Sql += "行番号枝番"
                            Sql += ", "
                            Sql += "仕入区分"
                            Sql += ", "
                            Sql += "メーカー"
                            Sql += ", "
                            Sql += "品名"
                            Sql += ", "
                            Sql += "型式"
                            Sql += ", "
                            Sql += "仕入先名"
                            Sql += ", "
                            Sql += "仕入値"
                            Sql += ", "
                            Sql += "受注数量"
                            Sql += ", "
                            Sql += "売上数量"
                            Sql += ", "
                            Sql += "受注残数"
                            Sql += ", "
                            Sql += "単位"
                            Sql += ", "
                            Sql += "仕入原価"
                            Sql += ", "
                            Sql += "関税率"
                            Sql += ", "
                            Sql += "関税額"
                            Sql += ", "
                            Sql += "前払法人税率"
                            Sql += ", "
                            Sql += "前払法人税額"
                            Sql += ", "
                            Sql += "輸送費率"
                            Sql += ", "
                            Sql += "輸送費額"
                            Sql += ", "
                            Sql += "間接費"
                            Sql += ", "
                            Sql += "仕入金額"
                            Sql += ", "
                            Sql += "売単価"
                            Sql += ", "
                            Sql += "売上金額"
                            Sql += ", "
                            Sql += "見積単価"
                            Sql += ", "
                            Sql += "見積金額"
                            Sql += ", "
                            Sql += "粗利額"
                            Sql += ", "
                            Sql += "粗利率"
                            Sql += ", "
                            Sql += "リードタイム"
                            Sql += ", "
                            Sql += "備考"
                            Sql += ", "
                            Sql += "入金有無"
                            Sql += ", "
                            Sql += "入金番号"
                            Sql += ", "
                            Sql += "入金日"
                            Sql += ", "
                            Sql += "備考"
                            Sql += ", "
                            Sql += "更新者"
                            Sql += ", "
                            Sql += "更新日"

                            _db.executeDB(Sql)

                            Sql = ""
                            Sql += "UPDATE "
                            Sql += "Public."
                            Sql += "t41_siredt "
                            Sql += "SET "
                            Sql += " カウント"
                            Sql += " = '"
                            Sql += SireQty.ToString
                            Sql += "' "

                            Sql += " WHERE "
                            Sql += "会社コード"
                            Sql += " ILIKE  "
                            Sql += "'"
                            Sql += frmC01F10_Login.loginValue.BumonNM
                            Sql += "'"
                            Sql += " AND "
                            Sql += "仕入番号"
                            Sql += " ILIKE  "
                            Sql += "'"
                            Sql += dsSiredt.Tables(RS).Rows(y)("仕入番号")
                            Sql += "'"
                            Sql += " AND "
                            Sql += "行番号"
                            Sql += " =  "
                            Sql += "'"
                            Sql += dsSiredt.Tables(RS).Rows(y)("行番号").ToString
                            Sql += "'"

                            Sql += "RETURNING カウント"

                            _db.executeDB(Sql)

                        End If
                    Next
                Next
            Else
                '平均法の場合
                For x As Integer = 0 To dsUrigdt.Tables(RS).Rows.Count - 1
                    Sql = ""
                    Sql += "INSERT INTO "
                    Sql += "Public."
                    Sql += "t53_krurigdt("
                    Sql += "会社コード, 売上番号, 受注番号, 受注番号枝番, 行番号, 行番号枝番, 仕入区分, メーカー, 品名, 型式, 仕入先名, 仕入値, 受注数量, 売上数量, 受注残数, 単位, 仕入原価, 関税率, 関税額, 前払法人税率, 前払法人税額, 輸送費率, 輸送費額, 仕入金額, 売単価, 売上金額, 見積単価, 見積金額, 粗利額, 粗利率, 間接費, リードタイム, 備考, 入金有無, 入金番号, 入金日, 更新者, 更新日)"
                    Sql += " VALUES('"
                    Sql += frmC01F10_Login.loginValue.BumonNM
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("売上番号").ToString
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("受注番号").ToString
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("受注番号枝番").ToString
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("行番号").ToString
                    Sql += "', '"
                    Sql += "0"
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("仕入区分").ToString
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("メーカー").ToString
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("品名").ToString
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("型式").ToString
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("仕入先名").ToString
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("仕入値").ToString
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("受注数量").ToString
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("売上数量").ToString
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("受注残数").ToString
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("単位").ToString
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("仕入原価").ToString
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("関税率").ToString
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("関税額").ToString
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("前払法人税率").ToString
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("前払法人税額").ToString
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("輸送費率").ToString
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("輸送費額").ToString
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("間接費").ToString
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("仕入金額").ToString
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("売単価").ToString
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("売上金額").ToString
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("見積単価").ToString
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("見積金額").ToString
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("粗利率").ToString
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("粗利額").ToString
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("リードタイム").ToString
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("備考").ToString
                    If dsUrigdt.Tables(RS).Rows(x)("入金有無") IsNot DBNull.Value Then
                        Sql += "', '"
                        Sql += dsUrigdt.Tables(RS).Rows(x)("入金有無").ToString
                        Sql += "', '"
                    Else
                        Sql += "', "
                        Sql += "0"
                        Sql += ", '"
                    End If


                    Sql += dsUrigdt.Tables(RS).Rows(x)("入金番号").Value

                    If dsUrigdt.Tables(RS).Rows(x)("入金日") IsNot DBNull.Value Then
                        Sql += "', '"
                        Sql += dsUrigdt.Tables(RS).Rows(x)("入金日").ToString
                        Sql += "', '"
                    Else
                        Sql += "', "
                        Sql += "null"
                        Sql += ", '"
                    End If
                    Sql += frmC01F10_Login.loginValue.TantoNM
                    Sql += "', '"
                    Sql += dtToday
                    Sql += " ')"
                    Sql += "RETURNING 会社コード"
                    Sql += ", "
                    Sql += "売上番号"
                    Sql += ", "
                    Sql += "受注番号"
                    Sql += ", "
                    Sql += "受注番号枝番"
                    Sql += ", "
                    Sql += "行番号"
                    Sql += ", "
                    Sql += "行番号枝番"
                    Sql += ", "
                    Sql += "仕入区分"
                    Sql += ", "
                    Sql += "メーカー"
                    Sql += ", "
                    Sql += "品名"
                    Sql += ", "
                    Sql += "型式"
                    Sql += ", "
                    Sql += "仕入先名"
                    Sql += ", "
                    Sql += "仕入値"
                    Sql += ", "
                    Sql += "受注数量"
                    Sql += ", "
                    Sql += "売上数量"
                    Sql += ", "
                    Sql += "受注残数"
                    Sql += ", "
                    Sql += "単位"
                    Sql += ", "
                    Sql += "仕入原価"
                    Sql += ", "
                    Sql += "関税率"
                    Sql += ", "
                    Sql += "関税額"
                    Sql += ", "
                    Sql += "前払法人税率"
                    Sql += ", "
                    Sql += "前払法人税額"
                    Sql += ", "
                    Sql += "輸送費率"
                    Sql += ", "
                    Sql += "輸送費額"
                    Sql += ", "
                    Sql += "間接費"
                    Sql += ", "
                    Sql += "仕入金額"
                    Sql += ", "
                    Sql += "売単価"
                    Sql += ", "
                    Sql += "売上金額"
                    Sql += ", "
                    Sql += "見積単価"
                    Sql += ", "
                    Sql += "見積金額"
                    Sql += ", "
                    Sql += "粗利額"
                    Sql += ", "
                    Sql += "粗利率"
                    Sql += ", "
                    Sql += "リードタイム"
                    Sql += ", "
                    Sql += "備考"
                    Sql += ", "
                    Sql += "入金有無"
                    Sql += ", "
                    Sql += "入金番号"
                    Sql += ", "
                    Sql += "入金日"
                    Sql += ", "
                    Sql += "備考"
                    Sql += ", "
                    Sql += "更新者"
                    Sql += ", "
                    Sql += "更新日"

                    _db.executeDB(Sql)
                Next
            End If
        Next
#End Region

#Region "仕入"
        Sql = ""
        Sql += "SELECT "
        Sql += "* "
        Sql += "FROM "
        Sql += "public"
        Sql += "."
        Sql += "t40_sirehd"
        Sql += " WHERE "
        Sql += "会社コード"
        Sql += " ILIKE  "
        Sql += "'"
        Sql += frmC01F10_Login.loginValue.BumonNM
        Sql += "'"
        Sql += " AND "
        Sql += "仕入日"
        Sql += " >  "
        Sql += "'"
        Sql += dsCompany.Tables(RS).Rows(0)("前回締日")
        Sql += "'"
        Sql += " AND "
        Sql += "仕入日"
        Sql += " <=  "
        Sql += "'"
        Sql += dsCompany.Tables(RS).Rows(0)("今回締日")
        Sql += "'"
        Sql += " AND "
        Sql += "締処理日"
        Sql += " IS NULL "

        Dim dsSirehd As DataSet = _db.selectDB(Sql, RS, reccnt)

        For i As Integer = 0 To dsSirehd.Tables(RS).Rows.Count - 1
            Sql = ""
            Sql += "INSERT INTO "
            Sql += "Public."
            Sql += "t54_krsirehd("
            Sql += "会社コード, 仕入番号, 発注番号, 発注番号枝番, 客先番号, 仕入先コード, 仕入先名, 仕入先郵便番号, 仕入先住所, 仕入先電話番号, 仕入先ＦＡＸ, 仕入先担当者役職, 仕入先担当者名, 支払条件, 仕入金額, 粗利額, 営業担当者, 入力担当者, 備考, 取消日, 取消区分, ＶＡＴ, ＰＰＨ, 仕入日, 登録日, 締処理日, 更新日, 更新者)"
            Sql += " VALUES('"
            Sql += frmC01F10_Login.loginValue.BumonNM
            Sql += "', '"
            Sql += dsSirehd.Tables(RS).Rows(i)("仕入番号").ToString
            Sql += "', '"
            Sql += dsSirehd.Tables(RS).Rows(i)("発注番号").ToString
            Sql += "', '"
            Sql += dsSirehd.Tables(RS).Rows(i)("発注番号枝番").ToString
            Sql += "', '"
            Sql += dsSirehd.Tables(RS).Rows(i)("客先番号").ToString
            Sql += "', '"
            Sql += dsSirehd.Tables(RS).Rows(i)("仕入先コード").ToString
            Sql += "', '"
            Sql += dsSirehd.Tables(RS).Rows(i)("仕入先名").ToString
            Sql += "', '"
            Sql += dsSirehd.Tables(RS).Rows(i)("仕入先郵便番号").ToString
            Sql += "', '"
            Sql += dsSirehd.Tables(RS).Rows(i)("仕入先住所").ToString
            Sql += "', '"
            Sql += dsSirehd.Tables(RS).Rows(i)("仕入先電話番号").ToString
            Sql += "', '"
            Sql += dsSirehd.Tables(RS).Rows(i)("仕入先ＦＡＸ").ToString
            Sql += "', '"
            Sql += dsSirehd.Tables(RS).Rows(i)("仕入先担当者役職").ToString
            Sql += "', '"
            Sql += dsSirehd.Tables(RS).Rows(i)("仕入先担当者名").ToString
            Sql += "', '"
            Sql += dsSirehd.Tables(RS).Rows(i)("支払条件").ToString
            Sql += "', '"
            Sql += dsSirehd.Tables(RS).Rows(i)("仕入金額").ToString
            Sql += "', '"
            Sql += dsSirehd.Tables(RS).Rows(i)("粗利額").ToString
            Sql += "', '"
            Sql += dsSirehd.Tables(RS).Rows(i)("営業担当者").ToString
            Sql += "', '"
            Sql += dsSirehd.Tables(RS).Rows(i)("入力担当者").ToString
            Sql += "', '"
            Sql += dsSirehd.Tables(RS).Rows(i)("備考").ToString

            If dsSirehd.Tables(RS).Rows(i)("取消日") IsNot DBNull.Value Then
                Sql += "', '"
                Sql += dsSirehd.Tables(RS).Rows(i)("取消日").ToString
                Sql += "', '"
            Else
                Sql += "', "
                Sql += "null"
                Sql += ", '"
            End If

            Sql += dsSirehd.Tables(RS).Rows(i)("取消区分").ToString
            Sql += "', '"
            Sql += dsSirehd.Tables(RS).Rows(i)("ＶＡＴ").ToString
            Sql += "', '"
            Sql += dsSirehd.Tables(RS).Rows(i)("ＰＰＨ").ToString
            Sql += "', '"
            Sql += dsSirehd.Tables(RS).Rows(i)("仕入日").ToString
            Sql += "', '"
            Sql += dsSirehd.Tables(RS).Rows(i)("登録日").ToString
            Sql += "', '"
            Sql += dtToday
            Sql += "', '"
            Sql += dtToday
            Sql += "', '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            Sql += " ')"
            Sql += "RETURNING 会社コード"
            Sql += ", "
            Sql += "仕入番号"
            Sql += ", "
            Sql += "発注番号"
            Sql += ", "
            Sql += "発注番号枝番"
            Sql += ", "
            Sql += "客先番号"
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
            Sql += "支払条件"
            Sql += ", "
            Sql += "仕入金額"
            Sql += ", "
            Sql += "粗利額"
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
            Sql += "ＶＡＴ"
            Sql += ", "
            Sql += "ＰＰＨ"
            Sql += ", "
            Sql += "仕入日"
            Sql += ", "
            Sql += "登録日"
            Sql += ", "
            Sql += "締処理日"
            Sql += ", "
            Sql += "更新日"
            Sql += ", "
            Sql += "更新者"

            _db.executeDB(Sql)
        Next

        For i As Integer = 0 To dsSirehd.Tables(RS).Rows.Count - 1
            Sql = ""
            Sql += "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t41_siredt"
            Sql += " WHERE "
            Sql += "仕入番号"
            Sql += " ILIKE "
            Sql += "'"
            Sql += dsSirehd.Tables(RS).Rows(i)("仕入番号")
            Sql += "'"

            Dim dssiredt As DataSet = _db.selectDB(Sql, RS, reccnt)
            For x As Integer = 0 To dssiredt.Tables(RS).Rows.Count - 1
                Sql = ""
                Sql += "INSERT INTO "
                Sql += "Public."
                Sql += "t55_krsiredt("
                Sql += "会社コード, 仕入番号, 発注番号, 発注番号枝番, 支払番号, 行番号, 仕入区分, メーカー, 品名, 型式, 仕入先名, 仕入値, 発注数量, 仕入数量, 発注残数, 単位, 仕入単価, 仕入金額, 間接費, リードタイム, 備考, 仕入日, 支払日, 支払有無, 更新者, 更新日)"
                Sql += " VALUES('"
                Sql += frmC01F10_Login.loginValue.BumonNM
                Sql += "', '"
                Sql += dssiredt.Tables(RS).Rows(x)("仕入番号").ToString
                Sql += "', '"
                Sql += dssiredt.Tables(RS).Rows(x)("発注番号").ToString
                Sql += "', '"
                Sql += dssiredt.Tables(RS).Rows(x)("発注番号枝番").ToString
                Sql += "', '"
                Sql += dssiredt.Tables(RS).Rows(x)("支払番号").ToString
                Sql += "', '"
                Sql += dssiredt.Tables(RS).Rows(x)("行番号").ToString
                Sql += "', '"
                Sql += dssiredt.Tables(RS).Rows(x)("仕入区分").ToString
                Sql += "', '"
                Sql += dssiredt.Tables(RS).Rows(x)("メーカー").ToString
                Sql += "', '"
                Sql += dssiredt.Tables(RS).Rows(x)("品名").ToString
                Sql += "', '"
                Sql += dssiredt.Tables(RS).Rows(x)("型式").ToString
                Sql += "', '"
                Sql += dssiredt.Tables(RS).Rows(x)("仕入先名").ToString
                Sql += "', '"
                Sql += dssiredt.Tables(RS).Rows(x)("仕入値").ToString
                Sql += "', '"
                Sql += dssiredt.Tables(RS).Rows(x)("発注数量").ToString
                Sql += "', '"
                Sql += dssiredt.Tables(RS).Rows(x)("仕入数量").ToString
                Sql += "', '"
                Sql += dssiredt.Tables(RS).Rows(x)("発注残数").ToString
                Sql += "', '"
                Sql += dssiredt.Tables(RS).Rows(x)("単位").ToString
                Sql += "', '"
                Sql += dssiredt.Tables(RS).Rows(x)("仕入単価").ToString
                Sql += "', '"
                Sql += dssiredt.Tables(RS).Rows(x)("仕入金額").ToString
                Sql += "', '"
                Sql += dssiredt.Tables(RS).Rows(x)("間接費").ToString
                Sql += "', '"
                Sql += dssiredt.Tables(RS).Rows(x)("リードタイム").ToString
                Sql += "', '"
                Sql += dssiredt.Tables(RS).Rows(x)("備考").ToString
                Sql += "', '"
                Sql += dssiredt.Tables(RS).Rows(x)("仕入日").ToString
                If dssiredt.Tables(RS).Rows(x)("支払日") IsNot DBNull.Value Then
                    Sql += "', '"
                    Sql += dssiredt.Tables(RS).Rows(x)("支払日").ToString
                    Sql += "', "
                Else
                    Sql += "', "
                    Sql += "null"
                    Sql += ", "
                End If
                If dssiredt.Tables(RS).Rows(x)("支払有無") IsNot DBNull.Value Then
                    Sql += " '"
                    Sql += dssiredt.Tables(RS).Rows(x)("支払有無").ToString
                    Sql += "', '"
                Else
                    Sql += "0"
                    Sql += ", '"
                End If

                Sql += frmC01F10_Login.loginValue.TantoNM
                Sql += "', '"
                Sql += dtToday
                Sql += " ')"
                Sql += "RETURNING 会社コード"
                Sql += ", "
                Sql += "仕入番号"
                Sql += ", "
                Sql += "発注番号"
                Sql += ", "
                Sql += "発注番号枝番"
                Sql += ", "
                Sql += "行番号"
                Sql += ", "
                Sql += "仕入区分"
                Sql += ", "
                Sql += "メーカー"
                Sql += ", "
                Sql += "品名"
                Sql += ", "
                Sql += "型式"
                Sql += ", "
                Sql += "仕入先名"
                Sql += ", "
                Sql += "仕入値"
                Sql += ", "
                Sql += "発注数量"
                Sql += ", "
                Sql += "仕入数量"
                Sql += ", "
                Sql += "発注残数"
                Sql += ", "
                Sql += "単位"
                Sql += ", "
                Sql += "仕入単価"
                Sql += ", "
                Sql += "仕入金額"
                Sql += ", "
                Sql += "間接費"
                Sql += ", "
                Sql += "リードタイム"
                Sql += ", "
                Sql += "支払有無"
                Sql += ", "
                Sql += "支払番号"
                Sql += ", "
                Sql += "支払日"
                Sql += ", "
                Sql += "備考"
                Sql += ", "
                Sql += "仕入日"
                Sql += ", "
                Sql += "更新者"
                Sql += ", "
                Sql += "更新日"
                _db.executeDB(Sql)
            Next
        Next
#End Region

#Region "売掛"
        Sql = ""
        Sql += "SELECT "
        Sql += "* "
        Sql += "FROM "
        Sql += "public"
        Sql += "."
        Sql += "t23_skyuhd"
        Sql += " WHERE "
        Sql += "会社コード"
        Sql += " ILIKE  "
        Sql += "'"
        Sql += frmC01F10_Login.loginValue.BumonNM
        Sql += "'"
        Sql += " AND "
        Sql += "請求日"
        Sql += " >  "
        Sql += "'"
        Sql += dsCompany.Tables(RS).Rows(0)("前回締日")
        Sql += "'"
        Sql += " AND "
        Sql += "請求日"
        Sql += " <=  "
        Sql += "'"
        Sql += dsCompany.Tables(RS).Rows(0)("今回締日")
        Sql += "'"
        Sql += " AND "
        Sql += "締処理日"
        Sql += " IS NULL "

        Dim dsSkyuhd As DataSet = _db.selectDB(Sql, RS, reccnt)

        For i As Integer = 0 To dsSkyuhd.Tables(RS).Rows.Count - 1
            Sql = ""
            Sql += "INSERT INTO "
            Sql += "Public."
            Sql += "t56_krskyuhd("
            Sql += "会社コード, 請求番号, 請求区分, 請求日, 受注番号, 受注番号枝番, 客先番号, 得意先コード, 得意先名, 請求金額計, 入金額計, 売掛残高, 備考１, 備考２, 入金番号, 入金完了日, 取消日, 取消区分, 登録日, 締処理日, 更新者)"
            Sql += " VALUES('"
            Sql += frmC01F10_Login.loginValue.BumonNM
            Sql += "', '"
            Sql += dsSkyuhd.Tables(RS).Rows(i)("請求番号").ToString
            Sql += "', '"
            Sql += dsSkyuhd.Tables(RS).Rows(i)("請求区分").ToString
            Sql += "', '"
            Sql += dsSkyuhd.Tables(RS).Rows(i)("請求日").ToString
            Sql += "', '"
            Sql += dsSkyuhd.Tables(RS).Rows(i)("受注番号").ToString
            Sql += "', '"
            Sql += dsSkyuhd.Tables(RS).Rows(i)("受注番号枝番").ToString
            Sql += "', '"
            Sql += dsSkyuhd.Tables(RS).Rows(i)("客先番号").ToString
            Sql += "', '"
            Sql += dsSkyuhd.Tables(RS).Rows(i)("得意先コード").ToString
            Sql += "', '"
            Sql += dsSkyuhd.Tables(RS).Rows(i)("得意先名").ToString
            Sql += "', '"
            Sql += dsSkyuhd.Tables(RS).Rows(i)("請求金額計").ToString

            If dsSkyuhd.Tables(RS).Rows(i)("入金額計") IsNot DBNull.Value Then
                Sql += "', '"
                Sql += dsSkyuhd.Tables(RS).Rows(i)("入金額計").ToString
                Sql += "', '"
            Else
                Sql += "', "
                Sql += "0"
                Sql += ", '"
            End If

            Sql += dsSkyuhd.Tables(RS).Rows(i)("売掛残高").ToString
            Sql += "', '"
            Sql += dsSkyuhd.Tables(RS).Rows(i)("備考1").ToString
            Sql += "', '"
            Sql += dsSkyuhd.Tables(RS).Rows(i)("備考2").ToString
            Sql += "', '"
            Sql += dsSkyuhd.Tables(RS).Rows(i)("入金番号").ToString


            If dsSkyuhd.Tables(RS).Rows(i)("入金完了日") IsNot DBNull.Value Then
                Sql += "', '"
                Sql += dsSkyuhd.Tables(RS).Rows(i)("入金完了日").ToString
                Sql += "', "
            Else
                Sql += "', "
                Sql += "null"
                Sql += ", "
            End If

            If dsSkyuhd.Tables(RS).Rows(i)("取消日") IsNot DBNull.Value Then
                Sql += "'"
                Sql += dsSkyuhd.Tables(RS).Rows(i)("取消日").ToString
                Sql += "', "
            Else
                Sql += "null"
                Sql += ", "
            End If

            If dsSkyuhd.Tables(RS).Rows(i)("取消区分") IsNot DBNull.Value Then
                Sql += "'"
                Sql += dsSkyuhd.Tables(RS).Rows(i)("取消区分").ToString
                Sql += "', "
            Else
                Sql += "0"
                Sql += ", "
            End If

            Sql += "'"
            Sql += dsSkyuhd.Tables(RS).Rows(i)("登録日").ToString
            Sql += "', '"
            Sql += dtToday
            Sql += "', '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            Sql += " ')"
            Sql += "RETURNING 会社コード"
            Sql += ", "
            Sql += "請求番号"
            Sql += ", "
            Sql += "請求区分"
            Sql += ", "
            Sql += "請求日"
            Sql += ", "
            Sql += "受注番号"
            Sql += ", "
            Sql += "受注番号枝番"
            Sql += ", "
            Sql += "客先番号"
            Sql += ", "
            Sql += "得意先コード"
            Sql += ", "
            Sql += "得意先名"
            Sql += ", "
            Sql += "請求金額計"
            Sql += ", "
            Sql += "入金額計"
            Sql += ", "
            Sql += "売掛残高"
            Sql += ", "
            Sql += "備考１"
            Sql += ", "
            Sql += "備考２"
            Sql += ", "
            Sql += "入金番号"
            Sql += ", "
            Sql += "入金完了日"
            Sql += ", "
            Sql += "取消日"
            Sql += ", "
            Sql += "取消区分"
            Sql += ", "
            Sql += "登録日"
            Sql += ", "
            Sql += "締処理日"
            Sql += ", "
            Sql += "更新者"

            _db.executeDB(Sql)
        Next
#End Region

#Region "買掛"
        Sql = ""
        Sql += "SELECT "
        Sql += "* "
        Sql += "FROM "
        Sql += "public"
        Sql += "."
        Sql += "t46_kikehd"
        Sql += " WHERE "
        Sql += "会社コード"
        Sql += " ILIKE  "
        Sql += "'"
        Sql += frmC01F10_Login.loginValue.BumonNM
        Sql += "'"
        Sql += " AND "
        Sql += "買掛日"
        Sql += " >  "
        Sql += "'"
        Sql += dsCompany.Tables(RS).Rows(0)("前回締日")
        Sql += "'"
        Sql += " AND "
        Sql += "買掛日"
        Sql += " <=  "
        Sql += "'"
        Sql += dsCompany.Tables(RS).Rows(0)("今回締日")
        Sql += "'"
        Sql += " AND "
        Sql += "締処理日"
        Sql += " IS NULL "

        Dim dsKikehd As DataSet = _db.selectDB(Sql, RS, reccnt)

        For i As Integer = 0 To dsKikehd.Tables(RS).Rows.Count - 1
            Sql = ""
            Sql += "INSERT INTO "
            Sql += "Public."
            Sql += "t57_krkikehd("
            Sql += "会社コード, 買掛番号, 買掛区分, 買掛日, 発注番号, 発注番号枝番, 客先番号, 仕入先コード, 仕入先名, 買掛金額計, 支払金額計, 買掛残高, 備考１, 備考２, 支払完了日, 取消日, 取消区分, 登録日, 更新者, 締処理日)"
            Sql += " VALUES('"
            Sql += frmC01F10_Login.loginValue.BumonNM
            Sql += "', '"
            Sql += dsKikehd.Tables(RS).Rows(i)("買掛番号").ToString
            Sql += "', '"
            Sql += dsKikehd.Tables(RS).Rows(i)("買掛区分").ToString
            Sql += "', '"
            Sql += dsKikehd.Tables(RS).Rows(i)("買掛日").ToString
            Sql += "', '"
            Sql += dsKikehd.Tables(RS).Rows(i)("発注番号").ToString
            Sql += "', '"
            Sql += dsKikehd.Tables(RS).Rows(i)("発注番号枝番").ToString
            Sql += "', '"
            Sql += dsKikehd.Tables(RS).Rows(i)("客先番号").ToString
            Sql += "', '"
            Sql += dsKikehd.Tables(RS).Rows(i)("仕入先コード").ToString
            Sql += "', '"
            Sql += dsKikehd.Tables(RS).Rows(i)("仕入先名").ToString
            Sql += "', '"
            Sql += dsKikehd.Tables(RS).Rows(i)("買掛金額計").ToString

            If dsKikehd.Tables(RS).Rows(i)("支払金額計") IsNot DBNull.Value Then
                Sql += "', '"
                Sql += dsKikehd.Tables(RS).Rows(i)("支払金額計").ToString
                Sql += "', '"
            Else
                Sql += "', '"
                Sql += "0"
                Sql += "', '"
            End If

            Sql += dsKikehd.Tables(RS).Rows(i)("買掛残高").ToString
            Sql += "', '"
            Sql += dsKikehd.Tables(RS).Rows(i)("備考1").ToString
            Sql += "', '"
            Sql += dsKikehd.Tables(RS).Rows(i)("備考2").ToString

            If dsKikehd.Tables(RS).Rows(i)("支払完了日") IsNot DBNull.Value Then
                Sql += "', '"
                Sql += dsKikehd.Tables(RS).Rows(i)("支払完了日").ToString
                Sql += "', "
            Else
                Sql += "', "
                Sql += "null"
                Sql += ", "
            End If

            If dsKikehd.Tables(RS).Rows(i)("取消日") IsNot DBNull.Value Then
                Sql += "'"
                Sql += dsKikehd.Tables(RS).Rows(i)("取消日").ToString
                Sql += "', "
            Else
                Sql += "null"
                Sql += ", "
            End If

            If dsKikehd.Tables(RS).Rows(i)("取消区分") IsNot DBNull.Value Then
                Sql += "'"
                Sql += dsKikehd.Tables(RS).Rows(i)("取消区分").ToString
                Sql += "', "
            Else
                Sql += "0"
                Sql += ", "
            End If

            Sql += "'"
            Sql += dsKikehd.Tables(RS).Rows(i)("登録日").ToString
            Sql += "', '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            Sql += "', '"
            Sql += dtToday
            Sql += " ')"
            Sql += "RETURNING 会社コード"
            Sql += ", "
            Sql += "買掛番号"
            Sql += ", "
            Sql += "買掛区分"
            Sql += ", "
            Sql += "買掛日"
            Sql += ", "
            Sql += "発注番号"
            Sql += ", "
            Sql += "発注番号枝番"
            Sql += ", "
            Sql += "客先番号"
            Sql += ", "
            Sql += "仕入先コード"
            Sql += ", "
            Sql += "仕入先名"
            Sql += ", "
            Sql += "買掛金額計"
            Sql += ", "
            Sql += "支払金額計"
            Sql += ", "
            Sql += "買掛残高"
            Sql += ", "
            Sql += "備考１"
            Sql += ", "
            Sql += "備考２"
            Sql += ", "
            Sql += "支払完了日"
            Sql += ", "
            Sql += "取消日"
            Sql += ", "
            Sql += "取消区分"
            Sql += ", "
            Sql += "登録日"
            Sql += ", "
            Sql += "更新者"
            Sql += ", "
            Sql += "締処理日"

            _db.executeDB(Sql)
        Next
#End Region

#Region "CSV"
        Sql = ""
        Sql += "SELECT "
        Sql += "* "
        Sql += "FROM "
        Sql += "public"
        Sql += "."
        Sql += "t52_krurighd"
        Sql += " WHERE "
        Sql += "会社コード"
        Sql += " ILIKE  "
        Sql += "'"
        Sql += frmC01F10_Login.loginValue.BumonNM
        Sql += "'"
        Sql += " AND "
        Sql += "締処理日"
        Sql += " = "
        Sql += "'"
        Sql += dtToday
        Sql += "'"
        Dim csvUrighd As DataSet = _db.selectDB(Sql, RS, reccnt)

        ConvertDataTableToCsv(csvUrighd, "t53_krurigdt", "売上番号", "Uriage")

        Sql = ""
        Sql += "SELECT "
        Sql += "* "
        Sql += "FROM "
        Sql += "public"
        Sql += "."
        Sql += "t54_krsirehd"
        Sql += " WHERE "
        Sql += "会社コード"
        Sql += " ILIKE  "
        Sql += "'"
        Sql += frmC01F10_Login.loginValue.BumonNM
        Sql += "'"
        Sql += " AND "
        Sql += "締処理日"
        Sql += " = "
        Sql += "'"
        Sql += dtToday
        Sql += "'"
        Dim csvSirehd As DataSet = _db.selectDB(Sql, RS, reccnt)

        ConvertDataTableToCsv(csvSirehd, "t55_krsiredt", "仕入番号", "Siire")

        Sql = ""
        Sql += "SELECT "
        Sql += "* "
        Sql += "FROM "
        Sql += "public"
        Sql += "."
        Sql += "t56_krskyuhd"
        Sql += " WHERE "
        Sql += "会社コード"
        Sql += " ILIKE  "
        Sql += "'"
        Sql += frmC01F10_Login.loginValue.BumonNM
        Sql += "'"
        Sql += " AND "
        Sql += "締処理日"
        Sql += " = "
        Sql += "'"
        Sql += dtToday
        Sql += "'"
        Dim csvSkyuhd As DataSet = _db.selectDB(Sql, RS, reccnt)

        ConvertDataTableToCsvSingle(csvSkyuhd, "Maeuke")

        Sql = ""
        Sql += "SELECT "
        Sql += "* "
        Sql += "FROM "
        Sql += "public"
        Sql += "."
        Sql += "t57_krkikehd"
        Sql += " WHERE "
        Sql += "会社コード"
        Sql += " ILIKE  "
        Sql += "'"
        Sql += frmC01F10_Login.loginValue.BumonNM
        Sql += "'"
        Sql += " AND "
        Sql += "締処理日"
        Sql += " = "
        Sql += "'"
        Sql += dtToday
        Sql += "'"
        Dim csvKikehd As DataSet = _db.selectDB(Sql, RS, reccnt)

        ConvertDataTableToCsvSingle(csvKikehd, "Maebarai")
#End Region
        _msgHd.dspMSG("CreateExcel")
    End Sub

    Public Sub ConvertDataTableToCsv(ds1 As DataSet, tableName As String, ColName As String, Name As String)

        Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
        '出力先パス
        Dim sOutPath As String = ""
        sOutPath = StartUp._iniVal.OutXlsPath

        '出力ファイル名
        Dim sOutFile As String = ""
        sOutFile = sOutPath & "\" & Name & ".csv"

        '書き込むファイルを開く
        Dim sr As New System.IO.StreamWriter(sOutFile, False, enc)
        Dim Sql As String = ""
        Dim reccnt As Integer = 0

        For i As Integer = 0 To ds1.Tables(RS).Rows.Count - 1
            Sql = ""
            Sql += "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += tableName
            Sql += " WHERE "
            Sql += "会社コード"
            Sql += " ILIKE  "
            Sql += "'"
            Sql += frmC01F10_Login.loginValue.BumonNM
            Sql += "'"
            Sql += " AND "
            Sql += ColName
            Sql += " = "
            Sql += "'"
            Sql += ds1.Tables(RS).Rows(i)(ColName)
            Sql += "'"

            Dim ds3 As DataSet = _db.selectDB(Sql, RS, reccnt)
            If i = 0 Then
                '基本列名
                For y As Integer = 0 To ds1.Tables(RS).Columns.Count - 1
                    Dim field As String = ds1.Tables(RS).Columns(y).ColumnName
                    field = EncloseDoubleQuotesIfNeed(field)
                    sr.Write(field)
                    If y < ds1.Tables(RS).Columns.Count - 1 Then
                        sr.Write(","c)
                    End If
                Next

                '明細列名
                For z As Integer = 0 To ds3.Tables(RS).Columns.Count - 1
                    Dim field As String = ds3.Tables(RS).Columns(z).ColumnName
                    field = EncloseDoubleQuotesIfNeed(field)
                    sr.Write(field)
                    sr.Write(","c)
                    If z < ds3.Tables(RS).Columns.Count - 1 Then
                        sr.Write(","c)
                    End If
                Next
                sr.Write(vbCrLf)
            End If

            For x As Integer = 0 To ds3.Tables(RS).Rows.Count - 1

                '基本
                For y As Integer = 0 To ds1.Tables(RS).Columns.Count - 1
                    Dim field As String = ds1.Tables(RS).Rows(i)(y).ToString()
                    field = EncloseDoubleQuotesIfNeed(field)
                    sr.Write(field)
                    If y < ds1.Tables(RS).Columns.Count - 1 Then
                        sr.Write(","c)
                    End If
                Next

                '明細
                For z As Integer = 0 To ds3.Tables(RS).Columns.Count - 1
                    Dim field As String = ds3.Tables(RS).Rows(x)(z).ToString()
                    field = EncloseDoubleQuotesIfNeed(field)
                    sr.Write(field)
                    sr.Write(","c)
                    If z < ds3.Tables(RS).Columns.Count - 1 Then
                        sr.Write(","c)
                    End If
                Next
                sr.Write(vbCrLf)
            Next
        Next
        sr.Close()
    End Sub


    Public Sub ConvertDataTableToCsvSingle(ds1 As DataSet, Name As String)

        Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
        '出力先パス
        Dim sOutPath As String = ""
        sOutPath = StartUp._iniVal.OutXlsPath

        '出力ファイル名
        Dim sOutFile As String = ""
        sOutFile = sOutPath & "\" & Name & ".csv"

        '書き込むファイルを開く
        Dim sr As New System.IO.StreamWriter(sOutFile, False, enc)
        Dim Sql As String = ""
        Dim reccnt As Integer = 0

        For i As Integer = 0 To ds1.Tables(RS).Rows.Count - 1
            If i = 0 Then
                '基本列名
                For y As Integer = 0 To ds1.Tables(RS).Columns.Count - 1
                    Dim field As String = ds1.Tables(RS).Columns(y).ColumnName
                    field = EncloseDoubleQuotesIfNeed(field)
                    sr.Write(field)
                    If y < ds1.Tables(RS).Columns.Count - 1 Then
                        sr.Write(","c)
                    End If
                Next
                sr.Write(vbCrLf)
            End If

            '基本
            For y As Integer = 0 To ds1.Tables(RS).Columns.Count - 1
                Dim field As String = ds1.Tables(RS).Rows(i)(y).ToString()
                field = EncloseDoubleQuotesIfNeed(field)
                sr.Write(field)
                If y < ds1.Tables(RS).Columns.Count - 1 Then
                    sr.Write(","c)
                End If
            Next
            sr.Write(vbCrLf)
        Next
        sr.Close()
    End Sub

    Private Function EncloseDoubleQuotesIfNeed(field As String) As String
        If NeedEncloseDoubleQuotes(field) Then
            Return EncloseDoubleQuotes(field)
        End If
        Return field
    End Function

    Private Function EncloseDoubleQuotes(field As String) As String
        If field.IndexOf(""""c) > -1 Then
            '"を""とする
            field = field.Replace("""", """""")
        End If
        Return """" & field & """"
    End Function

    Private Function NeedEncloseDoubleQuotes(field As String) As Boolean
        Return field.IndexOf(""""c) > -1 OrElse
        field.IndexOf(","c) > -1 OrElse
        field.IndexOf(ControlChars.Cr) > -1 OrElse
        field.IndexOf(ControlChars.Lf) > -1 OrElse
        field.StartsWith(" ") OrElse
        field.StartsWith(vbTab) OrElse
        field.EndsWith(" ") OrElse
        field.EndsWith(vbTab)
    End Function
End Class