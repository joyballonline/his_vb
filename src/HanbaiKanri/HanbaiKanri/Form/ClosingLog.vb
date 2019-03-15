Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Globalization

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
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Sub ClosingLog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ClosingLogLoad()
        If frmC01F10_Login.loginValue.Language = "ENG" Then
            LblClosingDate.Text = "ClosingDate"
            LblPerson.Text = "Name of PIC"
            BtnClosing.Text = "Closing"
            BtnBack.Text = "Back"
            BtnSearch.Text = "Search"

            DgvClosingLog.Columns("締処理日時").HeaderText = "ClosingDate"
            DgvClosingLog.Columns("前回締日").HeaderText = "LastClosingDate"
            DgvClosingLog.Columns("今回締日").HeaderText = "ThisClosingDate"
            DgvClosingLog.Columns("次回締日").HeaderText = "NextClosingDate"
            DgvClosingLog.Columns("担当者").HeaderText = "Name of PIC"
        End If
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
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    Private Sub BtnClosing_Click(sender As Object, e As EventArgs) Handles BtnClosing.Click


        Dim reccnt As Integer = 0
        Dim dtToday As DateTime = DateTime.Now

        Dim Sql1 As String = ""
        Sql1 += "SELECT * FROM public.m01_company"
        Sql1 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

        Dim ds1 As DataSet = _db.selectDB(Sql1, RS, reccnt)

        '仕訳データ作成
        'CSVファイルの書き出し処理
        Accounting()

        Exit Sub

        Dim Sql2 As String = ""
        Sql2 += "SELECT * FROM public.t30_urighd"
        Sql2 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        '20190225 テストのため条件をいったんコメントアウト
        'Sql2 += " AND 売上日 > '" & ds1.Tables(RS).Rows(0)("前回締日") & "'"
        'Sql2 += " AND 売上日 <= '" & ds1.Tables(RS).Rows(0)("今回締日") & "'"
        'Sql2 += " AND 締処理日 IS NULL "
        Dim dsUrigdt As DataSet = _db.selectDB(Sql2, RS, reccnt)


        Dim Sql3 As String = ""
        Sql3 += "SELECT * FROM public.t40_sirehd"
        Sql3 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        '20190225 テストのため条件をいったんコメントアウト
        'Sql3 += " AND 仕入日 > '" & ds1.Tables(RS).Rows(0)("前回締日") & "'"
        'Sql3 += " AND 仕入日 <= '" & ds1.Tables(RS).Rows(0)("今回締日") & "'"
        'Sql3 += " AND 締処理日 IS NULL "
        Dim ds3 As DataSet = _db.selectDB(Sql3, RS, reccnt)


        Dim Sql4 As String = ""
        Sql4 += "SELECT * FROM public.t23_skyuhd"
        Sql4 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        '20190225 テストのため条件をいったんコメントアウト
        'Sql4 += " AND 請求日 > '" & ds1.Tables(RS).Rows(0)("前回締日") & "'"
        'Sql4 += " AND 請求日 <= '" & ds1.Tables(RS).Rows(0)("今回締日") & "'"
        'Sql4 += " AND 締処理日 IS NULL "
        Dim ds4 As DataSet = _db.selectDB(Sql4, RS, reccnt)


        Sql4 = ""
        Sql4 += "SELECT * FROM public.t46_kikehd"
        Sql4 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        '20190225 テストのため条件をいったんコメントアウト
        'Sql4 += " AND 買掛日 > '" & ds1.Tables(RS).Rows(0)("前回締日") & "'"
        'Sql4 += " AND 買掛日 <= '" & ds1.Tables(RS).Rows(0)("今回締日") & "'"
        'Sql4 += " AND 締処理日 IS NULL "
        Dim dsKike As DataSet = _db.selectDB(Sql4, RS, reccnt)

        Dim Sql5 As String = ""
        Sql5 += "SELECT * FROM public.t31_urigdt"
        Sql5 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        '20190225 テストのため条件をいったんコメントアウト
        'Sql5 += " AND 売上番号 IN ("
        'For i As Integer = 0 To dsUrigdt.Tables(RS).Rows.Count - 1
        '    If i = 0 Then
        '    Else
        '        Sql5 += ","
        '    End If
        '    Sql5 += "'" & dsUrigdt.Tables(RS).Rows(i)("売上番号") & "'"
        'Next
        'Sql5 += ")"

        Dim Sql6 As String = ""
        Sql6 += "SELECT * FROM public.t41_siredt"
        Sql6 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        '20190225 テストのため条件をいったんコメントアウト
        'Sql6 += " AND 仕入番号 IN ("
        'For i As Integer = 0 To ds3.Tables(RS).Rows.Count - 1
        '    If i = 0 Then
        '    Else
        '        Sql6 += ","
        '    End If
        '    Sql6 += "'" & ds3.Tables(RS).Rows(i)("仕入番号") & "'"
        'Next

        Dim ds5 As DataSet = _db.selectDB(Sql5, RS, reccnt)
        Dim ds6 As DataSet = _db.selectDB(Sql6, RS, reccnt)

        Dim zaiko As String = ""
        zaiko += "SELECT * FROM public.t50_zikhd"
        zaiko += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

        Dim dszaiko As DataSet = _db.selectDB(zaiko, RS, reccnt)
        Dim SqlZaiko As String = ""

        For i As Integer = 0 To dszaiko.Tables(RS).Rows.Count - 1
            Dim purchase As String = ""
            purchase += "SELECT * FROM public.t41_siredt"
            purchase += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            purchase += " AND メーカー = '" & dszaiko.Tables(RS).Rows(i)("メーカー") & "'"
            purchase += " AND 品名 = '" & dszaiko.Tables(RS).Rows(i)("品名") & "'"
            purchase += " AND 型式 = '" & dszaiko.Tables(RS).Rows(i)("型式") & "'"
            '20190225 テストのため条件をいったんコメントアウト
            'purchase += " AND 締処理日 IS NULL "

            Dim dsPurchase As DataSet = _db.selectDB(purchase, RS, reccnt)
            Dim PurchaseSum As Double = 0
            Dim OverheadSum As Double = 0
            Dim PurchaseQuantity As Double = 0

            For x As Integer = 0 To dsPurchase.Tables(RS).Rows.Count - 1
                PurchaseSum += dsPurchase.Tables(RS).Rows(x)("仕入金額")
                OverheadSum += dsPurchase.Tables(RS).Rows(x)("間接費")
                PurchaseQuantity += dsPurchase.Tables(RS).Rows(x)("仕入数量")

                purchase = ""
                purchase += "UPDATE Public.t41_siredt "
                purchase += "SET 締処理日 = '" & dtToday & "'"

                purchase += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                purchase += " AND 仕入番号 = '" & dsPurchase.Tables(RS).Rows(x)("仕入番号") & "'"
                purchase += " AND 行番号 = '" & dsPurchase.Tables(RS).Rows(x)("行番号").ToString & "'"

                '20190225 テストのため締め日更新をいったんコメントアウト
                '_db.executeDB(purchase)

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
            SqlZaiko += "UPDATE Public.t50_zikhd "
            SqlZaiko += "SET 前月末数量 = '" & dszaiko.Tables(RS).Rows(i)("今月末数量").ToString & "'"
            SqlZaiko += " , 前月末単価 = " & dszaiko.Tables(RS).Rows(i)("今月単価").ToString
            SqlZaiko += " , 前月末間接費 = " & dszaiko.Tables(RS).Rows(i)("今月間接費").ToString
            SqlZaiko += " , 今月末数量 = 0"
            SqlZaiko += " , "
            If unitPrice.ToString = "∞" Or unitPrice.ToString = "-∞" Then
                SqlZaiko += "今月単価 = 0"
            Else
                SqlZaiko += "今月単価 = " & unitPrice.ToString
            End If
            SqlZaiko += " , 今月入庫数 = 0"
            SqlZaiko += " , 今月出庫数 = 0"
            SqlZaiko += " , "
            If OverHead.ToString = "∞" Or OverHead.ToString = "-∞" Then
                SqlZaiko += "今月間接費 = 0"
            Else
                SqlZaiko += "今月間接費 = " & OverHead.ToString
            End If

            SqlZaiko += " , 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
            SqlZaiko += " , 更新日 = '" & dtToday & "'"

            SqlZaiko += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            SqlZaiko += " AND メーカー = '" & dszaiko.Tables(RS).Rows(i)("メーカー") & "'"
            SqlZaiko += " AND 品名 = '" & dszaiko.Tables(RS).Rows(i)("品名") & "'"
            SqlZaiko += " AND 型式 = '" & dszaiko.Tables(RS).Rows(i)("型式") & "'"

            '20190225 テストのため締め日更新をいったんコメントアウト
            '_db.executeDB(SqlZaiko)
        Next


        For i As Integer = 0 To dszaiko.Tables(RS).Rows.Count - 1
            SqlZaiko = ""
            SqlZaiko += "UPDATE Public.t50_zikhd "
            SqlZaiko += "SET 前月末数量 = " & dszaiko.Tables(RS).Rows(i)("今月末数量").ToString
            SqlZaiko += " , 今月末数量 = 0"
            SqlZaiko += " , 今月入庫数 = 0"
            SqlZaiko += " , 今月出庫数 = 0"
            SqlZaiko += " , 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
            SqlZaiko += " , 更新日 = '" & dtToday & "'"

            SqlZaiko += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            SqlZaiko += " AND メーカー = '" & dszaiko.Tables(RS).Rows(i)("メーカー") & "'"
            SqlZaiko += " AND 品名 = '" & dszaiko.Tables(RS).Rows(i)("品名") & "'"
            SqlZaiko += " AND 型式 = '" & dszaiko.Tables(RS).Rows(i)("型式") & "'"

            '20190225 テストのため締め日更新をいったんコメントアウト
            '_db.executeDB(SqlZaiko)
        Next

        Dim Sql7 As String = ""
        '20190225 テストのため月次用テーブルのデータを削除  ここから
        _db.executeDB("truncate table t50_zikhd")
        '20190225 テストのため月次用テーブルのデータを削除  ここまで

        For i As Integer = 0 To ds6.Tables(RS).Rows.Count - 1
            Sql7 += "SELECT * FROM public.t50_zikhd"
            Sql7 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql7 += " AND メーカー = '" & ds6.Tables(RS).Rows(i)("メーカー") & "'"
            Sql7 += " AND 品名 = '" & ds6.Tables(RS).Rows(i)("品名") & "'"
            Sql7 += " AND 型式 = '" & ds6.Tables(RS).Rows(i)("型式") & "'"

            Dim ds7 As DataSet = _db.selectDB(Sql7, RS, reccnt)
            Sql7 = ""

            Dim Sql8 As String = ""

            If ds7.Tables(RS).Rows.Count = 0 Then
                Sql8 = ""
                Sql8 += "INSERT INTO Public.t50_zikhd("
                Sql8 += "会社コード, 年月, メーカー, 品名, 型式, 前月末数量, 前月末間接費, 今月末数量, 今月入庫数, 今月出庫数, 今月間接費, 更新者, 更新日,前月末単価, 今月単価)"
                Sql8 += " VALUES("
                Sql8 += " '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql8 += " , '" & dtToday & "'"
                Sql8 += " , '" & ds6.Tables(RS).Rows(i)("メーカー") & "'"
                Sql8 += " , '" & ds6.Tables(RS).Rows(i)("品名") & "'"
                Sql8 += " , '" & ds6.Tables(RS).Rows(i)("型式") & "'"
                Sql8 += " , 0"          '前月末数量
                Sql8 += " , 0"          '前月末間接費
                Sql8 += " , " & ds6.Tables(RS).Rows(i)("仕入数量").ToString     '今月末数量
                Sql8 += " , " & ds6.Tables(RS).Rows(i)("仕入数量").ToString     '今月入庫数
                Sql8 += " , 0"          '今月出庫数
                Sql8 += " , " & ds6.Tables(RS).Rows(i)("間接費").ToString      '今月間接費
                Sql8 += " , '" & frmC01F10_Login.loginValue.TantoNM & "'"        '更新者
                Sql8 += " , '" & dtToday & "'"      '更新日
                Sql8 += " , 0"          '前月末単価
                Sql8 += " , " & ds6.Tables(RS).Rows(i)("仕入単価").ToString     '今月単価
                Sql8 += " )"
                _db.executeDB(Sql8)
            Else
                Dim tmp1 As Double = 0
                Dim tmp2 As Double = 0
                Sql8 = ""
                Sql8 += "UPDATE Public.t50_zikhd "
                Sql8 += "SET "
                Sql8 += " 今月末数量 = "
                tmp1 = ds7.Tables(RS).Rows(0)("今月末数量") + ds6.Tables(RS).Rows(i)("仕入数量")
                Sql8 += tmp1.ToString
                Sql8 += " , 今月入庫数 = "
                tmp2 = ds7.Tables(RS).Rows(0)("今月入庫数") + ds6.Tables(RS).Rows(i)("仕入数量")
                Sql8 += tmp2.ToString
                Sql8 += " , 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
                Sql8 += " , 更新日 = '" & dtToday & "'"

                Sql8 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql8 += " AND メーカー = '" & ds6.Tables(RS).Rows(i)("メーカー") & "'"
                Sql8 += " AND 品名 = '" & ds6.Tables(RS).Rows(i)("品名") & "'"
                Sql8 += " AND 型式 = '" & ds6.Tables(RS).Rows(i)("型式") & "'"

                _db.executeDB(Sql8)
            End If
        Next

        Sql7 = ""
        For i As Integer = 0 To ds5.Tables(RS).Rows.Count - 1
            Sql7 += "SELECT * FROM public.t50_zikhd"
            Sql7 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql7 += " AND メーカー = '" & ds5.Tables(RS).Rows(i)("メーカー") & "'"
            Sql7 += " AND 品名 = '" & ds5.Tables(RS).Rows(i)("品名") & "'"
            Sql7 += " AND 型式 = '" & ds5.Tables(RS).Rows(i)("型式") & "'"

            Dim ds7 As DataSet = _db.selectDB(Sql7, RS, reccnt)
            Sql7 = ""

            Dim Sql9 As String = ""

            If ds7.Tables(RS).Rows.Count = 0 Then
                Sql9 = ""
                Sql9 += "INSERT INTO Public.t50_zikhd("
                Sql9 += "会社コード, 年月, メーカー, 品名, 型式, 前月末数量, 前月末間接費, 今月末数量, 今月入庫数, 今月出庫数, 今月間接費, 更新者, 更新日, 前月末単価, 今月単価)"
                Sql9 += " VALUES("
                Sql9 += " '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql9 += " , '" & dtToday & "'"          '年月
                Sql9 += " , '" & ds5.Tables(RS).Rows(i)("メーカー") & "'"
                Sql9 += " , '" & ds5.Tables(RS).Rows(i)("品名") & "'"
                Sql9 += " , '" & ds5.Tables(RS).Rows(i)("型式") & "'"
                Sql9 += " , 0"      '前月末数量
                Sql9 += " , 0"      '前月末間接費
                Sql9 += " , " & ds5.Tables(RS).Rows(i)("売上数量").ToString     '今月末数量
                Sql9 += " , 0"      '今月入庫数
                Sql9 += " , " & ds5.Tables(RS).Rows(i)("売上数量").ToString     '今月出庫数
                Sql9 += " , 0"      '今月間接費
                Sql9 += " , '" & frmC01F10_Login.loginValue.TantoNM & "'"       '更新者
                Sql9 += " , '" & dtToday & "'"                                  '更新日
                Sql9 += " , 0"      '前月末単価
                Sql9 += " , 0"      '今月単価
                Sql9 += " )"

                _db.executeDB(Sql9)

            Else
                Dim tmp3 As Double = 0
                Dim tmp4 As Double = 0
                Sql9 = ""
                Sql9 += "UPDATE Public.t50_zikhd "
                Sql9 += "SET "
                Sql9 += " 今月末数量 = "
                tmp3 = ds7.Tables(RS).Rows(0)("今月末数量") - ds5.Tables(RS).Rows(i)("売上数量")
                Sql9 += tmp3.ToString
                Sql9 += " , 今月出庫数 = "
                tmp4 = ds7.Tables(RS).Rows(0)("今月出庫数") + ds5.Tables(RS).Rows(i)("売上数量")
                Sql9 += tmp4.ToString
                Sql9 += " , 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
                Sql9 += " , 更新日 = '" & dtToday & "'"

                Sql9 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql9 += " AND メーカー = '" & ds5.Tables(RS).Rows(i)("メーカー") & "'"
                Sql9 += " AND 品名 = '" & ds5.Tables(RS).Rows(i)("品名") & "'"
                Sql9 += " AND 型式 = '" & ds5.Tables(RS).Rows(i)("型式") & "'"
                _db.executeDB(Sql9)
            End If
        Next

        Dim Sql10 As String = ""
        Sql10 += "SELECT * FROM public.t50_zikhd"
        Sql10 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

        Dim ds10 As DataSet = _db.selectDB(Sql10, RS, reccnt)

        Dim Sql11 As String = ""
        For i As Integer = 0 To ds10.Tables(RS).Rows.Count - 1
            Dim tmp5 As Double = 0
            Sql11 = ""
            Sql11 += "UPDATE Public.t50_zikhd "
            Sql11 += "SET "
            Sql11 += " 今月末数量 = "
            tmp5 = ds10.Tables(RS).Rows(i)("今月末数量") + ds10.Tables(RS).Rows(i)("前月末数量")
            Sql11 += tmp5.ToString
            Sql11 += " , 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
            Sql11 += " , 更新日 = '" & dtToday & "'"

            Sql11 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql11 += " AND メーカー = '" & ds10.Tables(RS).Rows(i)("メーカー") & "'"
            Sql11 += " AND 品名 = '" & ds10.Tables(RS).Rows(i)("品名") & "'"
            Sql11 += " AND 型式 = '" & ds10.Tables(RS).Rows(i)("型式") & "'"

            _db.executeDB(Sql11)
        Next
        Dim Sql12 As String = ""
        For i As Integer = 0 To ds5.Tables(RS).Rows.Count - 1
            Sql12 = ""
            Sql12 += "UPDATE Public.t30_urighd "
            Sql12 += "SET "
            Sql12 += "締処理日  = '" & dtToday & "'"
            Sql12 += " , 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
            Sql12 += " , 更新日 = '" & dtToday & "'"

            Sql12 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql12 += " AND 売上番号 = '" & ds5.Tables(RS).Rows(i)("売上番号") & "'"
            '20190225 テストのため締め日更新をいったんコメントアウト
            '_db.executeDB(Sql12)
        Next

        Dim Sql13 As String = ""
        For i As Integer = 0 To ds6.Tables(RS).Rows.Count - 1
            Sql13 = ""
            Sql13 += "UPDATE Public.t40_sirehd "
            Sql13 += "SET "
            Sql13 += "締処理日  = '" & dtToday & "'"
            Sql13 += " , 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"
            Sql13 += " , 更新日 = '" & dtToday & "'"

            Sql13 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql13 += " AND 仕入番号 = '" & ds6.Tables(RS).Rows(i)("仕入番号") & "'"
            '20190225 テストのため締め日更新をいったんコメントアウト
            '_db.executeDB(Sql13)
        Next

        Dim Sql14 As String = ""

        Sql14 = ""
        Sql14 += "INSERT INTO Public.t51_smlog("
        Sql14 += "会社コード, 処理日時, 前回締日, 今回締日, 次回締日, 担当者)"
        Sql14 += " VALUES('" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql14 += " , '" & dtToday & "'"
        Sql14 += " , '" & ds1.Tables(RS).Rows(0)("前回締日") & "'"
        Sql14 += " , '" & ds1.Tables(RS).Rows(0)("今回締日") & "'"
        Sql14 += " , '" & ds1.Tables(RS).Rows(0)("次回締日") & "'"
        Sql14 += " , '" & frmC01F10_Login.loginValue.TantoNM & "'"
        Sql14 += " )"
        '20190225 テストのため締め日更新をいったんコメントアウト
        '_db.executeDB(Sql14)

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
        Sql15 += "UPDATE Public.m01_company "
        Sql15 += "SET "
        Sql15 += "前回締日  = '" & ds1.Tables(RS).Rows(0)("今回締日") & "'"
        Sql15 += " , 今回締日 = '" & ds1.Tables(RS).Rows(0)("次回締日") & "'"
        Sql15 += " , 次回締日 = '" & nextClosingDate & "'"

        Sql15 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        '20190225 テストのため締め日更新をいったんコメントアウト
        '_db.executeDB(Sql15)

        '請求締処理日更新
        Dim Sql16 As String = ""
        For i As Integer = 0 To ds4.Tables(RS).Rows.Count - 1
            Sql16 = ""
            Sql16 += "UPDATE Public.t23_skyuhd "
            Sql16 += "SET "
            Sql16 += "締処理日  = '" & dtToday & "'"
            Sql16 += " , 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"

            Sql16 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql16 += " AND 請求番号 = '" & ds4.Tables(RS).Rows(i)("請求番号") & "'"
            '20190225 テストのため締め日更新をいったんコメントアウト
            '_db.executeDB(Sql16)
        Next


        '買掛締処理日更新
        Dim Sql17 As String = ""
        For i As Integer = 0 To dsKike.Tables(RS).Rows.Count - 1
            Sql17 = ""
            Sql17 += "UPDATE Public.t46_kikehd "
            Sql17 += "SET "
            Sql17 += "締処理日 = '" & dtToday & "'"
            Sql17 += " , 更新日 = '" & dtToday & "'"
            Sql17 += " , 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"

            Sql17 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql17 += " AND 買掛番号 = '" & dsKike.Tables(RS).Rows(i)("買掛番号") & "'"
            '20190225 テストのため締め日更新をいったんコメントアウト
            '_db.executeDB(Sql17)
        Next

        '仕入明細締処理日更新
        Dim Sql18 As String = ""
        For i As Integer = 0 To ds6.Tables(RS).Rows.Count - 1
            Sql18 = ""
            Sql18 += "UPDATE Public.t41_siredt "
            Sql18 += "SET "
            Sql18 += "締処理日  = '" & dtToday & "'"
            Sql18 += " , 更新者 = '" & frmC01F10_Login.loginValue.TantoNM & "'"

            Sql18 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql18 += " AND 仕入番号 = '" & ds6.Tables(RS).Rows(i)("仕入番号") & "'"
            '20190225 テストのため締め日更新をいったんコメントアウト
            '_db.executeDB(Sql18)
        Next


        DgvClosingLog.Rows.Clear()
        ClosingLogLoad()
    End Sub

    '仕訳データ作成処理
    '
    Private Sub Accounting()
        Dim dtToday As DateTime = DateTime.Now
        Dim reccnt As Integer = 0
        Dim Sql As String = ""

        '20190225 テストのため締処理用テーブルを全件消去してから処理を行う
        _db.executeDB("truncate table t52_krurighd")
        _db.executeDB("truncate table t53_krurigdt")
        _db.executeDB("truncate table t54_krsirehd")
        _db.executeDB("truncate table t55_krsiredt")
        _db.executeDB("truncate table t56_krskyuhd")
        _db.executeDB("truncate table t57_krkikehd")
        _db.executeDB("truncate table t58_krnyukohd")
        _db.executeDB("truncate table t59_krnyukodt")
        _db.executeDB("truncate table t60_krshukohd")
        _db.executeDB("truncate table t61_krshukodt")
        _db.executeDB("truncate table t62_krnkinhd")
        _db.executeDB("truncate table t63_krnkindt")
        _db.executeDB("truncate table t64_krshrihd")
        _db.executeDB("truncate table t65_krshridt")
        _db.executeDB("truncate table t66_swkhd")


        Sql = ""
        Sql += "SELECT * FROM public.m01_company"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Dim dsCompany As DataSet = _db.selectDB(Sql, RS, reccnt)

#Region "仕訳前受金"
        Sql = ""
        Sql += "SELECT * FROM public.t27_nkinkshihd"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        'Sql += " AND "
        'Sql += "入金日"
        'Sql += " >  "
        'Sql += "'"
        'Sql += dsCompany.Tables(RS).Rows(0)("前回締日")
        'Sql += "'"
        'Sql += " AND "
        'Sql += "入金日"
        'Sql += " <=  "
        'Sql += "'"
        'Sql += dsCompany.Tables(RS).Rows(0)("今回締日")
        'Sql += "'"
        'Sql += " AND "
        'Sql += "締処理日"
        'Sql += " IS NULL "

        Dim dsNkinkshihd As DataSet = _db.selectDB(Sql, RS, reccnt)

        '入金消込データぶん回して
        For i As Integer = 0 To dsNkinkshihd.Tables(RS).Rows.Count - 1
            Sql = ""
            Sql += "SELECT * FROM public.t23_skyuhd"
            Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND 請求番号 = '" & dsNkinkshihd.Tables(RS).Rows(i)("請求番号") & "'"
            Dim dsNkinSkyu As DataSet = _db.selectDB(Sql, RS, reccnt)


            '請求データから同じ請求番号のものを探す（1個しかない）

            If dsNkinSkyu.Tables(RS).Rows(0)("請求区分") = "2" Then
                Sql = ""
                Sql += "SELECT * FROM public.t10_cymnhd"
                Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " AND 受注番号 = '" & dsNkinSkyu.Tables(RS).Rows(0)("受注番号") & "'"
                Dim dsNkinCymn As DataSet = _db.selectDB(Sql, RS, reccnt)
                Sql = ""
                Sql += "INSERT INTO Public.t66_swkhd("
                Sql += "会社コード, 見積番号, 受注番号, 客先番号, 取引先名, 仕分日, 借方勘定科目, 借方勘定科目金額, 貸方勘定科目, 貸方勘定科目金額)"
                Sql += " VALUES('"
                Sql += frmC01F10_Login.loginValue.BumonCD
                Sql += "', '"
                Sql += dsNkinCymn.Tables(RS).Rows(0)("見積番号").ToString
                Sql += "', '"
                Sql += dsNkinSkyu.Tables(RS).Rows(0)("受注番号").ToString
                Sql += "', '"
                Sql += dsNkinSkyu.Tables(RS).Rows(0)("客先番号").ToString
                Sql += "', '"
                Sql += dsNkinCymn.Tables(RS).Rows(0)("得意先名").ToString
                Sql += "', '"
                Sql += formatDatetime(dsNkinCymn.Tables(RS).Rows(0)("受注日").ToString)
                Sql += "', '"
                Sql += "現金預金"
                Sql += "', '"
                Sql += dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計").ToString
                Sql += "', '"
                Sql += "前受金"
                Sql += "', '"
                Sql += dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計").ToString
                Sql += " ')"

                _db.executeDB(Sql)

                Sql = ""
                Sql += "INSERT INTO Public.t66_swkhd("
                Sql += "会社コード, 見積番号, 受注番号, 客先番号, 取引先名, 仕分日, 借方勘定科目, 借方勘定科目金額, 貸方勘定科目, 貸方勘定科目金額)"
                Sql += " VALUES('"
                Sql += frmC01F10_Login.loginValue.BumonCD
                Sql += "', '"
                Sql += dsNkinCymn.Tables(RS).Rows(0)("見積番号").ToString
                Sql += "', '"
                Sql += dsNkinSkyu.Tables(RS).Rows(0)("受注番号").ToString
                Sql += "', '"
                Sql += dsNkinSkyu.Tables(RS).Rows(0)("客先番号").ToString
                Sql += "', '"
                Sql += dsNkinCymn.Tables(RS).Rows(0)("得意先名").ToString
                Sql += "', '"
                Sql += formatDatetime(dsNkinCymn.Tables(RS).Rows(0)("受注日").ToString)
                Sql += "', '"
                Sql += "前受金"
                Sql += "', '"
                Sql += dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計").ToString
                Sql += "', '"
                Sql += "売掛金"
                Sql += "', '"
                Sql += dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計").ToString
                Sql += " ')"

                _db.executeDB(Sql)
            Else
                Sql = ""
                Sql += "SELECT * FROM public.t10_cymnhd"
                Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " AND 受注番号 = '" & dsNkinSkyu.Tables(RS).Rows(0)("受注番号") & "'"
                Dim dsNkinCymn As DataSet = _db.selectDB(Sql, RS, reccnt)
                Sql = ""
                Sql += "INSERT INTO Public.t66_swkhd("
                Sql += "会社コード, 見積番号, 受注番号, 客先番号, 取引先名, 仕分日, 借方勘定科目, 借方勘定科目金額, 貸方勘定科目, 貸方勘定科目金額)"
                Sql += " VALUES('"
                Sql += frmC01F10_Login.loginValue.BumonCD
                Sql += "', '"
                Sql += dsNkinCymn.Tables(RS).Rows(0)("見積番号").ToString
                Sql += "', '"
                Sql += dsNkinSkyu.Tables(RS).Rows(0)("受注番号").ToString
                Sql += "', '"
                Sql += dsNkinSkyu.Tables(RS).Rows(0)("客先番号").ToString
                Sql += "', '"
                Sql += dsNkinCymn.Tables(RS).Rows(0)("得意先名").ToString
                Sql += "', '"
                Sql += formatDatetime(dsNkinCymn.Tables(RS).Rows(0)("受注日").ToString)
                Sql += "', '"
                Sql += "現金預金"
                Sql += "', '"
                Sql += dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計").ToString
                Sql += "', '"
                Sql += "売掛金"
                Sql += "', '"
                Sql += dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計").ToString
                Sql += " ')"

                _db.executeDB(Sql)
            End If

        Next
#End Region

#Region "仕分売掛金"
        Sql = ""
        Sql += "SELECT * FROM public.t30_urighd"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        'Sql += " AND "
        'Sql += "売上日"
        'Sql += " >  "
        'Sql += "'"
        'Sql += dsCompany.Tables(RS).Rows(0)("前回締日")
        'Sql += "'"
        'Sql += " AND "
        'Sql += "売上日"
        'Sql += " <=  "
        'Sql += "'"
        'Sql += dsCompany.Tables(RS).Rows(0)("今回締日")
        'Sql += "'"
        'Sql += " AND "
        'Sql += "締処理日"
        'Sql += " IS NULL "

        Dim dsSwkUrighd As DataSet = _db.selectDB(Sql, RS, reccnt)

        For i As Integer = 0 To dsSwkUrighd.Tables(RS).Rows.Count - 1
            Sql = ""
            Sql += "INSERT INTO Public.t66_swkhd("
            Sql += "会社コード, 見積番号, 受注番号, 客先番号, 取引先名, 仕分日, 借方勘定科目, 借方勘定科目金額, 貸方勘定科目, 貸方勘定科目金額)"
            Sql += " VALUES('"
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "', '"
            Sql += dsSwkUrighd.Tables(RS).Rows(i)("見積番号").ToString
            Sql += "', '"
            Sql += dsSwkUrighd.Tables(RS).Rows(i)("受注番号").ToString
            Sql += "', '"
            Sql += dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString
            Sql += "', '"
            Sql += dsSwkUrighd.Tables(RS).Rows(i)("得意先名").ToString
            Sql += "', '"
            Sql += formatDatetime(dsSwkUrighd.Tables(RS).Rows(i)("売上日").ToString)
            Sql += "', '"
            Sql += "売掛金"
            Sql += "', '"
            Sql += dsSwkUrighd.Tables(RS).Rows(i)("売上金額").ToString
            Sql += "', '"
            Sql += "売上"
            Sql += "', '"
            Sql += dsSwkUrighd.Tables(RS).Rows(i)("売上金額").ToString
            Sql += " ')"
            _db.executeDB(Sql)

            Dim VATOUT As Double = 0
            VATOUT = dsSwkUrighd.Tables(RS).Rows(i)("売上金額") * dsSwkUrighd.Tables(RS).Rows(i)("ＶＡＴ") / 100
            Sql = ""
            Sql += "INSERT INTO Public.t66_swkhd("
            Sql += "会社コード, 見積番号, 受注番号, 客先番号, 取引先名, 仕分日, 借方勘定科目, 借方勘定科目金額, 貸方勘定科目, 貸方勘定科目金額)"
            Sql += " VALUES('"
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "', '"
            Sql += dsSwkUrighd.Tables(RS).Rows(i)("見積番号").ToString
            Sql += "', '"
            Sql += dsSwkUrighd.Tables(RS).Rows(i)("受注番号").ToString
            Sql += "', '"
            Sql += dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString
            Sql += "', '"
            Sql += dsSwkUrighd.Tables(RS).Rows(i)("得意先名").ToString
            Sql += "', '"
            Sql += formatDatetime(dsSwkUrighd.Tables(RS).Rows(i)("売上日").ToString)
            Sql += "', '"
            Sql += "売掛金"
            Sql += "', '"
            Sql += VATOUT.ToString
            Sql += "', '"
            Sql += "VAT-OUT"
            Sql += "', '"
            Sql += VATOUT.ToString
            Sql += " ')"

            _db.executeDB(Sql)
        Next
#End Region

#Region "仕訳前払金"
        Sql = ""
        Sql += "SELECT * FROM public.t49_shrikshihd"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        'Sql += " AND "
        'Sql += "支払日"
        'Sql += " >  "
        'Sql += "'"
        'Sql += dsCompany.Tables(RS).Rows(0)("前回締日")
        'Sql += "'"
        'Sql += " AND "
        'Sql += "支払日"
        'Sql += " <=  "
        'Sql += "'"
        'Sql += dsCompany.Tables(RS).Rows(0)("今回締日")
        'Sql += "'"
        'Sql += " AND "
        'Sql += "締処理日"
        'Sql += " IS NULL "

        Dim dsShrikshihd As DataSet = _db.selectDB(Sql, RS, reccnt)

        For i As Integer = 0 To dsShrikshihd.Tables(RS).Rows.Count - 1
            Sql = ""
            Sql += "SELECT * FROM public.t46_kikehd"
            Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND 買掛番号 = '" & dsShrikshihd.Tables(RS).Rows(i)("買掛番号") & "'"
            Dim dsShriKike As DataSet = _db.selectDB(Sql, RS, reccnt)

            If dsShriKike.Tables(RS).Rows(0)("買掛区分") = "2" Then
                Sql = ""
                Sql += "SELECT * FROM public.t20_hattyu"
                Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " AND 発注番号 = '" & dsShriKike.Tables(RS).Rows(0)("発注番号") & "'"
                Dim dsShriHattyu As DataSet = _db.selectDB(Sql, RS, reccnt)

                Sql = ""
                Sql += "INSERT INTO Public.t66_swkhd("
                Sql += "会社コード, 見積番号, 受注番号, 客先番号, 取引先名, 仕分日, 借方勘定科目, 借方勘定科目金額, 貸方勘定科目, 貸方勘定科目金額)"
                Sql += " VALUES('"
                Sql += frmC01F10_Login.loginValue.BumonCD
                Sql += "', '"
                Sql += dsShriHattyu.Tables(RS).Rows(0)("見積番号").ToString
                Sql += "', '"
                Sql += dsShriHattyu.Tables(RS).Rows(0)("受注番号").ToString
                Sql += "', '"
                Sql += dsShriHattyu.Tables(RS).Rows(0)("客先番号").ToString
                Sql += "', '"
                Sql += dsShriHattyu.Tables(RS).Rows(0)("仕入先名").ToString
                Sql += "', '"
                Sql += formatDatetime(dsShriHattyu.Tables(RS).Rows(0)("発注日").ToString)
                Sql += "', '"
                Sql += "前払金"
                Sql += "', '"
                Sql += dsShrikshihd.Tables(RS).Rows(i)("支払消込額計").ToString
                Sql += "', '"
                Sql += "現金預金"
                Sql += "', '"
                Sql += dsShrikshihd.Tables(RS).Rows(i)("支払消込額計").ToString
                Sql += " ')"

                _db.executeDB(Sql)

                Sql = ""
                Sql += "INSERT INTO Public.t66_swkhd("
                Sql += "会社コード, 見積番号, 受注番号, 客先番号, 取引先名, 仕分日, 借方勘定科目, 借方勘定科目金額, 貸方勘定科目, 貸方勘定科目金額)"
                Sql += " VALUES('"
                Sql += frmC01F10_Login.loginValue.BumonCD
                Sql += "', '"
                Sql += dsShriHattyu.Tables(RS).Rows(0)("見積番号").ToString
                Sql += "', '"
                Sql += dsShriHattyu.Tables(RS).Rows(0)("受注番号").ToString
                Sql += "', '"
                Sql += dsShriHattyu.Tables(RS).Rows(0)("客先番号").ToString
                Sql += "', '"
                Sql += dsShriHattyu.Tables(RS).Rows(0)("仕入先名").ToString
                Sql += "', '"
                Sql += formatDatetime(dsShriHattyu.Tables(RS).Rows(0)("発注日").ToString)
                Sql += "', '"
                Sql += "買掛金"
                Sql += "', '"
                Sql += dsShrikshihd.Tables(RS).Rows(i)("支払消込額計").ToString
                Sql += "', '"
                Sql += "前払金"
                Sql += "', '"
                Sql += dsShrikshihd.Tables(RS).Rows(i)("支払消込額計").ToString
                Sql += " ')"

                _db.executeDB(Sql)
            Else
                Sql = ""
                Sql += "SELECT * FROM public.t20_hattyu"
                Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " AND 発注番号 = '" & dsShriKike.Tables(RS).Rows(0)("発注番号") & "'"
                Dim dsShriHattyu As DataSet = _db.selectDB(Sql, RS, reccnt)

                Sql = ""
                Sql += "INSERT INTO Public.t66_swkhd("
                Sql += "会社コード, 見積番号, 受注番号, 客先番号, 取引先名, 仕分日, 借方勘定科目, 借方勘定科目金額, 貸方勘定科目, 貸方勘定科目金額)"
                Sql += " VALUES('"
                Sql += frmC01F10_Login.loginValue.BumonCD
                Sql += "', '"
                Sql += dsShriHattyu.Tables(RS).Rows(0)("見積番号").ToString
                Sql += "', '"
                Sql += dsShriHattyu.Tables(RS).Rows(0)("受注番号").ToString
                Sql += "', '"
                Sql += dsShriHattyu.Tables(RS).Rows(0)("客先番号").ToString
                Sql += "', '"
                Sql += dsShriHattyu.Tables(RS).Rows(0)("仕入先名").ToString
                Sql += "', '"
                Sql += formatDatetime(dsShriHattyu.Tables(RS).Rows(0)("発注日").ToString)
                Sql += "', '"
                Sql += "買掛金"
                Sql += "', '"
                Sql += dsShrikshihd.Tables(RS).Rows(i)("支払消込額計").ToString
                Sql += "', '"
                Sql += "現金預金"
                Sql += "', '"
                Sql += dsShrikshihd.Tables(RS).Rows(i)("支払消込額計").ToString
                Sql += " ')"

                _db.executeDB(Sql)
            End If

        Next
#End Region

#Region "仕訳買掛金"
        Sql = ""
        Sql += "SELECT * FROM public.t40_sirehd"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        'Sql += " AND "
        'Sql += "仕入日"
        'Sql += " >  "
        'Sql += "'"
        'Sql += dsCompany.Tables(RS).Rows(0)("前回締日")
        'Sql += "'"
        'Sql += " AND "
        'Sql += "仕入日"
        'Sql += " <=  "
        'Sql += "'"
        'Sql += dsCompany.Tables(RS).Rows(0)("今回締日")
        'Sql += "'"
        'Sql += " AND "
        'Sql += "締処理日"
        'Sql += " IS NULL "

        Dim dsSwkSirehd As DataSet = _db.selectDB(Sql, RS, reccnt)

        For i As Integer = 0 To dsSwkSirehd.Tables(RS).Rows.Count - 1
            Sql = ""
            Sql += "SELECT * FROM public.t20_hattyu"
            Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND 発注番号 = '" & dsSwkSirehd.Tables(RS).Rows(i)("発注番号") & "'"

            Dim dsSwkHattyu As DataSet = _db.selectDB(Sql, RS, reccnt)

            Sql = ""
            Sql += "SELECT * FROM public.t42_nyukohd"
            Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND 発注番号 = '" & dsSwkSirehd.Tables(RS).Rows(i)("発注番号") & "'"

            Dim dsSWKNyukohd As DataSet = _db.selectDB(Sql, RS, reccnt)

            For x As Integer = 0 To dsSWKNyukohd.Tables(RS).Rows.Count - 1
                Sql = ""
                Sql += "INSERT INTO Public.t66_swkhd("
                Sql += "会社コード, 見積番号, 受注番号, 客先番号, 取引先名, 仕分日, 借方勘定科目, 借方勘定科目金額, 貸方勘定科目, 貸方勘定科目金額)"
                Sql += " VALUES('"
                Sql += frmC01F10_Login.loginValue.BumonCD
                Sql += "', '"
                Sql += dsSwkHattyu.Tables(RS).Rows(0)("見積番号").ToString
                Sql += "', '"
                Sql += dsSwkHattyu.Tables(RS).Rows(0)("受注番号").ToString
                Sql += "', '"
                Sql += dsSwkHattyu.Tables(RS).Rows(0)("客先番号").ToString
                Sql += "', '"
                Sql += dsSwkHattyu.Tables(RS).Rows(0)("仕入先名").ToString
                Sql += "', '"
                Sql += formatDatetime(dsSwkHattyu.Tables(RS).Rows(0)("発注日").ToString)
                Sql += "', '"
                Sql += "棚卸資産"
                Sql += "', '"
                Sql += dsSWKNyukohd.Tables(RS).Rows(x)("仕入金額").ToString
                Sql += "', '"
                Sql += "買掛金"
                Sql += "', '"
                Sql += dsSWKNyukohd.Tables(RS).Rows(x)("仕入金額").ToString
                Sql += " ')"
                _db.executeDB(Sql)

                Sql = ""
                Sql += "INSERT INTO Public.t66_swkhd("
                Sql += "会社コード, 見積番号, 受注番号, 客先番号, 取引先名, 仕分日, 借方勘定科目, 借方勘定科目金額, 貸方勘定科目, 貸方勘定科目金額)"
                Sql += " VALUES('"
                Sql += frmC01F10_Login.loginValue.BumonCD
                Sql += "', '"
                Sql += dsSwkHattyu.Tables(RS).Rows(0)("見積番号").ToString
                Sql += "', '"
                Sql += dsSwkHattyu.Tables(RS).Rows(0)("受注番号").ToString
                Sql += "', '"
                Sql += dsSwkHattyu.Tables(RS).Rows(0)("客先番号").ToString
                Sql += "', '"
                Sql += dsSwkHattyu.Tables(RS).Rows(0)("仕入先名").ToString
                Sql += "', '"
                Sql += formatDatetime(dsSwkHattyu.Tables(RS).Rows(0)("発注日").ToString)
                Sql += "', '"
                Sql += "仕入"
                Sql += "', '"
                Sql += dsSWKNyukohd.Tables(RS).Rows(x)("仕入金額").ToString
                Sql += "', '"
                Sql += "棚卸資産"
                Sql += "', '"
                Sql += dsSWKNyukohd.Tables(RS).Rows(x)("仕入金額").ToString
                Sql += " ')"


                _db.executeDB(Sql)
            Next

            Dim VATIN As Double = 0
            VATIN = dsSwkSirehd.Tables(RS).Rows(i)("仕入金額") * dsSwkSirehd.Tables(RS).Rows(i)("ＶＡＴ") / 100
            Sql = ""
            Sql += "INSERT INTO Public.t66_swkhd("
            Sql += "会社コード, 見積番号, 受注番号, 客先番号, 取引先名, 仕分日, 借方勘定科目, 借方勘定科目金額, 貸方勘定科目, 貸方勘定科目金額)"
            Sql += " VALUES('"
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "', '"
            Sql += dsSwkHattyu.Tables(RS).Rows(0)("見積番号").ToString
            Sql += "', '"
            Sql += dsSwkHattyu.Tables(RS).Rows(0)("受注番号").ToString
            Sql += "', '"
            Sql += dsSwkHattyu.Tables(RS).Rows(0)("客先番号").ToString
            Sql += "', '"
            Sql += dsSwkHattyu.Tables(RS).Rows(0)("仕入先名").ToString
            Sql += "', '"
            Sql += formatDatetime(dsSwkHattyu.Tables(RS).Rows(0)("発注日").ToString)
            Sql += "', '"
            Sql += "VAT-IN"
            Sql += "', '"
            Sql += VATIN.ToString
            Sql += "', '"
            Sql += "買掛金"
            Sql += "', '"
            Sql += VATIN.ToString
            Sql += " ')"
            _db.executeDB(Sql)

        Next


#End Region

#Region "売上"
        Sql = ""
        Sql += "SELECT * FROM public.t30_urighd"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        'Sql += " AND "
        'Sql += "売上日"
        'Sql += " >  "
        'Sql += "'"
        'Sql += dsCompany.Tables(RS).Rows(0)("前回締日")
        'Sql += "'"
        'Sql += " AND "
        'Sql += "売上日"
        'Sql += " <=  "
        'Sql += "'"
        'Sql += dsCompany.Tables(RS).Rows(0)("今回締日")
        'Sql += "'"
        'Sql += " AND "
        'Sql += "締処理日"
        'Sql += " IS NULL "

        Dim dsUrighd As DataSet = _db.selectDB(Sql, RS, reccnt)

        For i As Integer = 0 To dsUrighd.Tables(RS).Rows.Count - 1
            Sql = ""
            Sql += "INSERT INTO Public.t52_krurighd("
            Sql += "会社コード, 売上番号, 客先番号, 受注番号, 受注番号枝番, 見積番号, 見積番号枝番, 得意先コード, 得意先名, 得意先郵便番号, 得意先住所, 得意先電話番号, 得意先ＦＡＸ, 得意先担当者役職, 得意先担当者名, 見積日, 見積有効期限, 支払条件, 見積金額, 仕入金額, 売上金額, 粗利額, 営業担当者, 入力担当者, 備考, ＶＡＴ, ＰＰＨ, 受注日, 売上日, 締処理日, 入金予定日, 登録日, 更新日, 更新者, 取消日, 取消区分)"
            Sql += " VALUES('"
            Sql += frmC01F10_Login.loginValue.BumonCD
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

            _db.executeDB(Sql)
        Next

        For i As Integer = 0 To dsUrighd.Tables(RS).Rows.Count - 1
            Sql = ""
            Sql += "SELECT * FROM public.t31_urigdt"
            Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND 売上番号 = '" & dsUrighd.Tables(RS).Rows(i)("売上番号") & "'"

            Dim dsUrigdt As DataSet = _db.selectDB(Sql, RS, reccnt)

            If dsCompany.Tables(RS).Rows(0)("在庫単価評価法") = 1 Then
                '先入先出法の場合
                For x As Integer = 0 To dsUrigdt.Tables(RS).Rows.Count - 1
                    Sql = ""
                    Sql += "SELECT * FROM public.t41_siredt"
                    Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                    Sql += " AND メーカー ='" & dsUrigdt.Tables(RS).Rows(x)("メーカー") & "'"
                    Sql += " AND 品名 ='" & dsUrigdt.Tables(RS).Rows(x)("品名") & "'"
                    Sql += " AND 型式 ='" & dsUrigdt.Tables(RS).Rows(x)("型式") & "'"
                    'Sql += " AND "
                    'Sql += "仕入日"
                    'Sql += " >  "
                    'Sql += "'"
                    'Sql += dsCompany.Tables(RS).Rows(0)("前回締日")
                    'Sql += "'"
                    'Sql += " AND "
                    'Sql += "仕入日"
                    'Sql += " <=  "
                    'Sql += "'"
                    'Sql += dsCompany.Tables(RS).Rows(0)("今回締日")
                    'Sql += "'"
                    'Sql += " AND "
                    'Sql += "締処理日"
                    'Sql += " IS NULL "
                    'Sql += " AND "
                    'Sql += "カウント IS NULL"

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
                            Sql += frmC01F10_Login.loginValue.BumonCD
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
                            Sql += dsUrigdt.Tables(RS).Rows(x)("粗利額").ToString
                            Sql += "', '"
                            Sql += dsUrigdt.Tables(RS).Rows(x)("粗利率").ToString
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

                            _db.executeDB(Sql)

                            Sql = ""
                            Sql += "UPDATE Public.t41_siredt "
                            Sql += "SET "
                            Sql += " カウント = '" & SireQty.ToString & "'"

                            Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                            Sql += " AND 仕入番号 = '" & dsSiredt.Tables(RS).Rows(y)("仕入番号") & "'"
                            Sql += " AND 行番号 = '" & dsSiredt.Tables(RS).Rows(y)("行番号").ToString & "'"

                            _db.executeDB(Sql)

                        End If
                    Next
                Next
            Else
                '平均法の場合
                For x As Integer = 0 To dsUrigdt.Tables(RS).Rows.Count - 1
                    Sql = ""
                    Sql += "INSERT INTO Public.t53_krurigdt("
                    Sql += "会社コード, 売上番号, 受注番号, 受注番号枝番, 行番号, 行番号枝番, 仕入区分, メーカー, 品名, 型式, 仕入先名, 仕入値, 受注数量, 売上数量, 受注残数, 単位, 仕入原価, 関税率, 関税額, 前払法人税率, 前払法人税額, 輸送費率, 輸送費額, 間接費, 仕入金額, 売単価, 売上金額, 見積単価, 見積金額, 粗利額, 粗利率, リードタイム, 備考, 入金有無, 入金番号, 入金日, 更新者, 更新日)"
                    Sql += " VALUES('"
                    Sql += frmC01F10_Login.loginValue.BumonCD
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
                    Sql += dsUrigdt.Tables(RS).Rows(x)("粗利額").ToString
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("粗利率").ToString
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
                    _db.executeDB(Sql)
                Next
            End If
        Next
#End Region

#Region "仕入"
        Sql = ""
        Sql += "SELECT * FROM public.t40_sirehd"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        'Sql += " AND "
        'Sql += "仕入日"
        'Sql += " >  "
        'Sql += "'"
        'Sql += dsCompany.Tables(RS).Rows(0)("前回締日")
        'Sql += "'"
        'Sql += " AND "
        'Sql += "仕入日"
        'Sql += " <=  "
        'Sql += "'"
        'Sql += dsCompany.Tables(RS).Rows(0)("今回締日")
        'Sql += "'"
        'Sql += " AND "
        'Sql += "締処理日"
        'Sql += " IS NULL "

        Dim dsSirehd As DataSet = _db.selectDB(Sql, RS, reccnt)

        For i As Integer = 0 To dsSirehd.Tables(RS).Rows.Count - 1
            Sql = ""
            Sql += "INSERT INTO Public.t54_krsirehd("
            Sql += "会社コード, 仕入番号, 発注番号, 発注番号枝番, 客先番号, 仕入先コード, 仕入先名, 仕入先郵便番号, 仕入先住所, 仕入先電話番号, 仕入先ＦＡＸ, 仕入先担当者役職, 仕入先担当者名, 支払条件, 仕入金額, 粗利額, 営業担当者, 入力担当者, 備考, 取消日, 取消区分, ＶＡＴ, ＰＰＨ, 仕入日, 登録日, 締処理日, 更新日, 更新者)"
            Sql += " VALUES('"
            Sql += frmC01F10_Login.loginValue.BumonCD
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

            _db.executeDB(Sql)
        Next

        For i As Integer = 0 To dsSirehd.Tables(RS).Rows.Count - 1
            Sql = ""
            Sql += "SELECT * FROM public.t41_siredt"
            Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND 仕入番号 = '" & dsSirehd.Tables(RS).Rows(i)("仕入番号") & "'"

            Dim dssiredt As DataSet = _db.selectDB(Sql, RS, reccnt)
            For x As Integer = 0 To dssiredt.Tables(RS).Rows.Count - 1
                Sql = ""
                Sql += "INSERT INTO Public.t55_krsiredt("
                Sql += "会社コード, 仕入番号, 発注番号, 発注番号枝番, 支払番号, 行番号, 仕入区分, メーカー, 品名, 型式, 仕入先名, 仕入値, 発注数量, 仕入数量, 発注残数, 単位, 仕入単価, 仕入金額, 間接費, リードタイム, 備考, 仕入日, 支払日, 支払有無, 更新者, 更新日)"
                Sql += " VALUES('"
                Sql += frmC01F10_Login.loginValue.BumonCD
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
                _db.executeDB(Sql)
            Next
        Next
#End Region

#Region "売掛"
        Sql = ""
        Sql += "SELECT * FROM public.t23_skyuhd"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        'Sql += " AND "
        'Sql += "請求日"
        'Sql += " >  "
        'Sql += "'"
        'Sql += dsCompany.Tables(RS).Rows(0)("前回締日")
        'Sql += "'"
        'Sql += " AND "
        'Sql += "請求日"
        'Sql += " <=  "
        'Sql += "'"
        'Sql += dsCompany.Tables(RS).Rows(0)("今回締日")
        'Sql += "'"
        'Sql += " AND "
        'Sql += "締処理日"
        'Sql += " IS NULL "

        Dim dsSkyuhd As DataSet = _db.selectDB(Sql, RS, reccnt)

        For i As Integer = 0 To dsSkyuhd.Tables(RS).Rows.Count - 1
            Sql = ""
            Sql += "INSERT INTO Public.t56_krskyuhd("
            Sql += "会社コード, 請求番号, 請求区分, 請求日, 受注番号, 受注番号枝番, 客先番号, 得意先コード, 得意先名, 請求金額計, 入金額計, 売掛残高, 備考１, 備考２, 入金番号, 入金完了日, 取消日, 取消区分, 登録日, 締処理日, 更新者)"
            Sql += " VALUES('"
            Sql += frmC01F10_Login.loginValue.BumonCD
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

            _db.executeDB(Sql)
        Next
#End Region

#Region "買掛"
        Sql = ""
        Sql += "SELECT * FROM public.t46_kikehd"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        'Sql += " AND "
        'Sql += "買掛日"
        'Sql += " >  "
        'Sql += "'"
        'Sql += dsCompany.Tables(RS).Rows(0)("前回締日")
        'Sql += "'"
        'Sql += " AND "
        'Sql += "買掛日"
        'Sql += " <=  "
        'Sql += "'"
        'Sql += dsCompany.Tables(RS).Rows(0)("今回締日")
        'Sql += "'"
        'Sql += " AND "
        'Sql += "締処理日"
        'Sql += " IS NULL "

        Dim dsKikehd As DataSet = _db.selectDB(Sql, RS, reccnt)

        For i As Integer = 0 To dsKikehd.Tables(RS).Rows.Count - 1
            Sql = ""
            Sql += "INSERT INTO Public.t57_krkikehd("
            Sql += "会社コード, 買掛番号, 買掛区分, 買掛日, 発注番号, 発注番号枝番, 客先番号, 仕入先コード, 仕入先名, 買掛金額計, 支払金額計, 買掛残高, 備考１, 備考２, 支払完了日, 取消日, 取消区分, 登録日, 更新者, 締処理日)"
            Sql += " VALUES('"
            Sql += frmC01F10_Login.loginValue.BumonCD
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

            _db.executeDB(Sql)
        Next
#End Region

#Region "入庫"
        Sql = ""
        Sql += "SELECT * FROM public.t42_nyukohd"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        'Sql += " AND "
        'Sql += "入庫日"
        'Sql += " >  "
        'Sql += "'"
        'Sql += dsCompany.Tables(RS).Rows(0)("前回締日")
        'Sql += "'"
        'Sql += " AND "
        'Sql += "入庫日"
        'Sql += " <=  "
        'Sql += "'"
        'Sql += dsCompany.Tables(RS).Rows(0)("今回締日")
        'Sql += "'"
        'Sql += " AND "
        'Sql += "締処理日"
        'Sql += " IS NULL "

        Dim dsNyukohd As DataSet = _db.selectDB(Sql, RS, reccnt)

        For i As Integer = 0 To dsNyukohd.Tables(RS).Rows.Count - 1
            Sql = ""
            Sql += "INSERT INTO Public.t58_krnyukohd("
            Sql += "会社コード, 入庫番号, 客先番号, 発注番号, 発注番号枝番, 仕入先コード, 仕入先名, 仕入先郵便番号, 仕入先住所, 仕入先電話番号, 仕入先ＦＡＸ, 仕入先担当者役職, 仕入先担当者名, 支払条件, 仕入金額, 粗利額, ＶＡＴ, ＰＰＨ, 営業担当者, 入力担当者, 備考, 入庫日, 登録日, 更新日, 更新者, 取消日, 取消区分, 締処理日)"
            Sql += " VALUES('"
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "', '"
            Sql += dsNyukohd.Tables(RS).Rows(i)("入庫番号").ToString
            Sql += "', '"
            Sql += dsNyukohd.Tables(RS).Rows(i)("客先番号").ToString
            Sql += "', '"
            Sql += dsNyukohd.Tables(RS).Rows(i)("発注番号").ToString
            Sql += "', '"
            Sql += dsNyukohd.Tables(RS).Rows(i)("発注番号枝番").ToString
            Sql += "', '"
            Sql += dsNyukohd.Tables(RS).Rows(i)("仕入先コード").ToString
            Sql += "', '"
            Sql += dsNyukohd.Tables(RS).Rows(i)("仕入先名").ToString
            Sql += "', '"
            Sql += dsNyukohd.Tables(RS).Rows(i)("仕入先郵便番号").ToString
            Sql += "', '"
            Sql += dsNyukohd.Tables(RS).Rows(i)("仕入先住所").ToString
            Sql += "', '"
            Sql += dsNyukohd.Tables(RS).Rows(i)("仕入先電話番号").ToString
            Sql += "', '"
            Sql += dsNyukohd.Tables(RS).Rows(i)("仕入先ＦＡＸ").ToString
            Sql += "', '"
            Sql += dsNyukohd.Tables(RS).Rows(i)("仕入先担当者役職").ToString
            Sql += "', '"
            Sql += dsNyukohd.Tables(RS).Rows(i)("仕入先担当者名").ToString
            Sql += "', '"
            Sql += dsNyukohd.Tables(RS).Rows(i)("支払条件").ToString
            Sql += "', '"
            Sql += dsNyukohd.Tables(RS).Rows(i)("仕入金額").ToString
            Sql += "', '"
            Sql += dsNyukohd.Tables(RS).Rows(i)("粗利額").ToString
            Sql += "', '"
            If dsNyukohd.Tables(RS).Rows(i)("ＶＡＴ") Is DBNull.Value Then
                Sql += "0"
            Else
                Sql += dsNyukohd.Tables(RS).Rows(i)("ＶＡＴ").ToString
            End If
            Sql += "', '"
            If dsNyukohd.Tables(RS).Rows(i)("ＰＰＨ") Is DBNull.Value Then
                Sql += "0"
            Else
                Sql += dsNyukohd.Tables(RS).Rows(i)("ＰＰＨ").ToString
            End If
            Sql += "', '"
            Sql += dsNyukohd.Tables(RS).Rows(i)("営業担当者").ToString
            Sql += "', '"
            Sql += dsNyukohd.Tables(RS).Rows(i)("入力担当者").ToString
            Sql += "', '"
            Sql += dsNyukohd.Tables(RS).Rows(i)("備考").ToString
            Sql += "', '"
            Sql += dsNyukohd.Tables(RS).Rows(i)("入庫日").ToString
            Sql += "', '"
            Sql += dsNyukohd.Tables(RS).Rows(i)("登録日").ToString
            Sql += "', '"
            Sql += dtToday
            Sql += "', '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            If dsNyukohd.Tables(RS).Rows(i)("取消日") IsNot DBNull.Value Then
                Sql += "', '"
                Sql += dsNyukohd.Tables(RS).Rows(i)("取消日").ToString
                Sql += "', "
            Else
                Sql += "', "
                Sql += "null"
                Sql += ", "
            End If
            If dsNyukohd.Tables(RS).Rows(i)("取消区分") IsNot DBNull.Value Then
                Sql += "'"
                Sql += dsNyukohd.Tables(RS).Rows(i)("取消区分").ToString
            Else
                Sql += "0"
            End If
            Sql += "', '"
            Sql += dtToday
            Sql += " ')"


            _db.executeDB(Sql)
        Next

        For i As Integer = 0 To dsNyukohd.Tables(RS).Rows.Count - 1
            Sql = ""
            Sql += "SELECT * FROM public.t43_nyukodt"
            Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND 入庫番号 = '" & dsNyukohd.Tables(RS).Rows(i)("入庫番号") & "'"

            Dim dsNyukodt As DataSet = _db.selectDB(Sql, RS, reccnt)

            For x As Integer = 0 To dsNyukodt.Tables(RS).Rows.Count - 1
                Sql = ""
                Sql += "INSERT INTO Public.t59_krnyukodt("
                Sql += "会社コード, 入庫番号, 発注番号, 発注番号枝番, 行番号, 仕入区分, メーカー, 品名, 型式, 仕入先名, 仕入値, 入庫数量, 単位, 備考, 更新者, 更新日)"
                Sql += " VALUES('"
                Sql += frmC01F10_Login.loginValue.BumonCD
                Sql += "', '"
                Sql += dsNyukodt.Tables(RS).Rows(x)("入庫番号").ToString
                Sql += "', '"
                Sql += dsNyukodt.Tables(RS).Rows(x)("発注番号").ToString
                Sql += "', '"
                Sql += dsNyukodt.Tables(RS).Rows(x)("発注番号枝番").ToString
                Sql += "', '"
                Sql += dsNyukodt.Tables(RS).Rows(x)("行番号").ToString
                Sql += "', '"
                Sql += dsNyukodt.Tables(RS).Rows(x)("仕入区分").ToString
                Sql += "', '"
                Sql += dsNyukodt.Tables(RS).Rows(x)("メーカー").ToString
                Sql += "', '"
                Sql += dsNyukodt.Tables(RS).Rows(x)("品名").ToString
                Sql += "', '"
                Sql += dsNyukodt.Tables(RS).Rows(x)("型式").ToString
                Sql += "', '"
                Sql += dsNyukodt.Tables(RS).Rows(x)("仕入先名").ToString
                Sql += "', '"
                Sql += dsNyukodt.Tables(RS).Rows(x)("仕入値").ToString
                Sql += "', '"
                Sql += dsNyukodt.Tables(RS).Rows(x)("入庫数量").ToString
                Sql += "', '"
                Sql += dsNyukodt.Tables(RS).Rows(x)("単位").ToString
                Sql += "', '"
                Sql += dsNyukodt.Tables(RS).Rows(x)("備考").ToString
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoNM
                Sql += "', '"
                Sql += dtToday
                Sql += " ')"

                _db.executeDB(Sql)
            Next
        Next

#End Region

#Region "出庫"
        Sql = ""
        Sql += "SELECT * FROM public.t44_shukohd"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        'Sql += " AND "
        'Sql += "出庫日"
        'Sql += " >  "
        'Sql += "'"
        'Sql += dsCompany.Tables(RS).Rows(0)("前回締日")
        'Sql += "'"
        'Sql += " AND "
        'Sql += "出庫日"
        'Sql += " <=  "
        'Sql += "'"
        'Sql += dsCompany.Tables(RS).Rows(0)("今回締日")
        'Sql += "'"
        'Sql += " AND "
        'Sql += "締処理日"
        'Sql += " IS NULL "

        Dim dsShukohd As DataSet = _db.selectDB(Sql, RS, reccnt)

        For i As Integer = 0 To dsShukohd.Tables(RS).Rows.Count - 1
            Sql = ""
            Sql += "INSERT INTO Public.t60_krshukohd("
            Sql += "会社コード, 出庫番号, 客先番号, 見積番号, 見積番号枝番, 受注番号, 受注番号枝番, 得意先コード, 得意先名, 得意先郵便番号, 得意先住所, 得意先電話番号, 得意先ＦＡＸ, 得意先担当者役職, 得意先担当者名, 営業担当者, 入力担当者, 備考, 出庫日, 登録日, 更新日, 更新者, 取消日, 取消区分, 締処理日)"
            Sql += " VALUES('"
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "', '"
            Sql += dsShukohd.Tables(RS).Rows(i)("出庫番号").ToString
            Sql += "', '"
            Sql += dsShukohd.Tables(RS).Rows(i)("客先番号").ToString
            Sql += "', '"
            Sql += dsShukohd.Tables(RS).Rows(i)("見積番号").ToString
            Sql += "', '"
            Sql += dsShukohd.Tables(RS).Rows(i)("見積番号枝番").ToString
            Sql += "', '"
            Sql += dsShukohd.Tables(RS).Rows(i)("受注番号").ToString
            Sql += "', '"
            Sql += dsShukohd.Tables(RS).Rows(i)("受注番号枝番").ToString
            Sql += "', '"
            Sql += dsShukohd.Tables(RS).Rows(i)("得意先コード").ToString
            Sql += "', '"
            Sql += dsShukohd.Tables(RS).Rows(i)("得意先名").ToString
            Sql += "', '"
            Sql += dsShukohd.Tables(RS).Rows(i)("得意先郵便番号").ToString
            Sql += "', '"
            Sql += dsShukohd.Tables(RS).Rows(i)("得意先住所").ToString
            Sql += "', '"
            Sql += dsShukohd.Tables(RS).Rows(i)("得意先電話番号").ToString
            Sql += "', '"
            Sql += dsShukohd.Tables(RS).Rows(i)("得意先ＦＡＸ").ToString
            Sql += "', '"
            Sql += dsShukohd.Tables(RS).Rows(i)("得意先担当者役職").ToString
            Sql += "', '"
            Sql += dsShukohd.Tables(RS).Rows(i)("得意先担当者名").ToString
            Sql += "', '"
            Sql += dsShukohd.Tables(RS).Rows(i)("営業担当者").ToString
            Sql += "', '"
            Sql += dsShukohd.Tables(RS).Rows(i)("入力担当者").ToString
            Sql += "', '"
            Sql += dsShukohd.Tables(RS).Rows(i)("備考").ToString
            Sql += "', '"
            Sql += dsShukohd.Tables(RS).Rows(i)("出庫日").ToString
            Sql += "', '"
            Sql += dsShukohd.Tables(RS).Rows(i)("登録日").ToString
            Sql += "', '"
            Sql += dtToday
            Sql += "', '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            If dsShukohd.Tables(RS).Rows(i)("取消日") IsNot DBNull.Value Then
                Sql += "', '"
                Sql += dsShukohd.Tables(RS).Rows(i)("取消日").ToString
                Sql += "', "
            Else
                Sql += "', "
                Sql += "null"
                Sql += ", "
            End If
            If dsShukohd.Tables(RS).Rows(i)("取消区分") IsNot DBNull.Value Then
                Sql += dsShukohd.Tables(RS).Rows(i)("取消区分").ToString
            Else
                Sql += "0"
            End If
            Sql += ", '"
            Sql += dtToday
            Sql += " ')"

            _db.executeDB(Sql)
        Next

        For i As Integer = 0 To dsShukohd.Tables(RS).Rows.Count - 1
            Sql = ""
            Sql += "SELECT * FROM public.t45_shukodt"
            Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND 出庫番号 = '" & dsShukohd.Tables(RS).Rows(i)("出庫番号") & "'"

            Dim dsShukodt As DataSet = _db.selectDB(Sql, RS, reccnt)

            For x As Integer = 0 To dsShukodt.Tables(RS).Rows.Count - 1
                Sql = ""
                Sql += "INSERT INTO Public.t61_krshukodt("
                Sql += "会社コード, 出庫番号, 受注番号, 受注番号枝番, 行番号, 仕入区分, メーカー, 品名, 型式, 仕入先名, 出庫数量, 単位, 売単価, 備考, 更新者, 更新日)"
                Sql += " VALUES('"
                Sql += frmC01F10_Login.loginValue.BumonCD
                Sql += "', '"
                Sql += dsShukodt.Tables(RS).Rows(x)("出庫番号").ToString
                Sql += "', '"
                Sql += dsShukodt.Tables(RS).Rows(x)("受注番号").ToString
                Sql += "', '"
                Sql += dsShukodt.Tables(RS).Rows(x)("受注番号枝番").ToString
                Sql += "', '"
                Sql += dsShukodt.Tables(RS).Rows(x)("行番号").ToString
                Sql += "', '"
                Sql += dsShukodt.Tables(RS).Rows(x)("仕入区分").ToString
                Sql += "', '"
                Sql += dsShukodt.Tables(RS).Rows(x)("メーカー").ToString
                Sql += "', '"
                Sql += dsShukodt.Tables(RS).Rows(x)("品名").ToString
                Sql += "', '"
                Sql += dsShukodt.Tables(RS).Rows(x)("型式").ToString
                Sql += "', '"
                Sql += dsShukodt.Tables(RS).Rows(x)("仕入先名").ToString
                Sql += "', '"
                Sql += dsShukodt.Tables(RS).Rows(x)("出庫数量").ToString
                Sql += "', '"
                Sql += dsShukodt.Tables(RS).Rows(x)("単位").ToString
                Sql += "', '"
                Sql += dsShukodt.Tables(RS).Rows(x)("売単価").ToString
                Sql += "', '"
                Sql += dsShukodt.Tables(RS).Rows(x)("備考").ToString
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoNM
                Sql += "', '"
                Sql += dtToday
                Sql += " ')"

                _db.executeDB(Sql)
            Next
        Next
#End Region

#Region "支払"
        Sql = ""
        Sql += "SELECT * FROM public.t47_shrihd"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        'Sql += " AND "
        'Sql += "支払日"
        'Sql += " >  "
        'Sql += "'"
        'Sql += dsCompany.Tables(RS).Rows(0)("前回締日")
        'Sql += "'"
        'Sql += " AND "
        'Sql += "支払日"
        'Sql += " <=  "
        'Sql += "'"
        'Sql += dsCompany.Tables(RS).Rows(0)("今回締日")
        'Sql += "'"
        'Sql += " AND "
        'Sql += "締処理日"
        'Sql += " IS NULL "

        Dim dsShrihd As DataSet = _db.selectDB(Sql, RS, reccnt)

        For i As Integer = 0 To dsShrihd.Tables(RS).Rows.Count - 1
            Sql = ""
            Sql += "INSERT INTO Public.t64_krshrihd("
            Sql += "会社コード, 支払番号, 客先番号, 支払先コード, 支払先名, 支払先, 買掛金額, 支払金額計, 買掛残高, 備考, 支払日, 登録日, 更新日, 更新者, 取消日, 取消区分, 締処理日)"
            Sql += " VALUES('"
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "', '"
            Sql += dsShrihd.Tables(RS).Rows(i)("支払番号").ToString
            Sql += "', '"
            Sql += dsShrihd.Tables(RS).Rows(i)("客先番号").ToString
            Sql += "', '"
            Sql += dsShrihd.Tables(RS).Rows(i)("支払先コード").ToString
            Sql += "', '"
            Sql += dsShrihd.Tables(RS).Rows(i)("支払先名").ToString
            Sql += "', '"
            Sql += dsShrihd.Tables(RS).Rows(i)("支払先").ToString
            Sql += "', '"
            Sql += dsShrihd.Tables(RS).Rows(i)("買掛金額").ToString
            Sql += "', '"
            Sql += dsShrihd.Tables(RS).Rows(i)("支払金額計").ToString
            Sql += "', '"
            Sql += dsShrihd.Tables(RS).Rows(i)("買掛残高").ToString
            Sql += "', '"
            Sql += dsShrihd.Tables(RS).Rows(i)("備考").ToString
            Sql += "', '"
            Sql += dsShrihd.Tables(RS).Rows(i)("支払日").ToString
            Sql += "', '"
            Sql += dsShrihd.Tables(RS).Rows(i)("登録日").ToString
            Sql += "', '"
            Sql += dtToday
            Sql += "', '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            If dsShrihd.Tables(RS).Rows(i)("取消日") IsNot DBNull.Value Then
                Sql += "', '"
                Sql += dsShrihd.Tables(RS).Rows(i)("取消日").ToString
                Sql += "', "
            Else
                Sql += "', "
                Sql += "null"
                Sql += ", "
            End If
            If dsShrihd.Tables(RS).Rows(i)("取消区分") IsNot DBNull.Value Then
                Sql += "'"
                Sql += dsShrihd.Tables(RS).Rows(i)("取消区分").ToString
            Else
                Sql += "0"
            End If
            Sql += "', '"
            Sql += dtToday
            Sql += " ')"

            _db.executeDB(Sql)
        Next

        For i As Integer = 0 To dsShrihd.Tables(RS).Rows.Count - 1
            Sql = ""
            Sql += "SELECT * FROM public.t48_shridt"
            Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND 支払番号 = '" & dsShrihd.Tables(RS).Rows(i)("支払番号") & "'"

            Dim dsShridt As DataSet = _db.selectDB(Sql, RS, reccnt)

            For x As Integer = 0 To dsShridt.Tables(RS).Rows.Count - 1
                Sql = ""
                Sql += "INSERT INTO Public.t65_krshridt("
                Sql += "会社コード, 支払番号, 行番号, 支払種別, 支払種別名, 支払先コード, 支払先名, 支払先, 支払金額, 備考, 支払日, 更新者, 更新日)"
                Sql += " VALUES('"
                Sql += frmC01F10_Login.loginValue.BumonCD
                Sql += "', '"
                Sql += dsShridt.Tables(RS).Rows(x)("支払番号").ToString
                Sql += "', '"
                Sql += dsShridt.Tables(RS).Rows(x)("行番号").ToString
                Sql += "', '"
                Sql += dsShridt.Tables(RS).Rows(x)("支払種別").ToString
                Sql += "', '"
                Sql += dsShridt.Tables(RS).Rows(x)("支払種別名").ToString
                Sql += "', '"
                Sql += dsShridt.Tables(RS).Rows(x)("支払先コード").ToString
                Sql += "', '"
                Sql += dsShridt.Tables(RS).Rows(x)("支払先名").ToString
                Sql += "', '"
                Sql += dsShridt.Tables(RS).Rows(x)("支払先").ToString
                Sql += "', '"
                Sql += dsShridt.Tables(RS).Rows(x)("支払金額").ToString
                Sql += "', '"
                Sql += dsShridt.Tables(RS).Rows(x)("備考").ToString
                Sql += "', '"
                Sql += dsShridt.Tables(RS).Rows(x)("支払日").ToString
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoNM
                Sql += "', '"
                Sql += dtToday
                Sql += " ')"

                _db.executeDB(Sql)
            Next
        Next
#End Region

#Region "入金"
        Sql = ""
        Sql += "SELECT * FROM public.t25_nkinhd"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        'Sql += " AND "
        'Sql += "入金日"
        'Sql += " >  "
        'Sql += "'"
        'Sql += dsCompany.Tables(RS).Rows(0)("前回締日")
        'Sql += "'"
        'Sql += " AND "
        'Sql += "入金日"
        'Sql += " <=  "
        'Sql += "'"
        'Sql += dsCompany.Tables(RS).Rows(0)("今回締日")
        'Sql += "'"
        'Sql += " AND "
        'Sql += "締処理日"
        'Sql += " IS NULL "

        Dim dsNkinhd As DataSet = _db.selectDB(Sql, RS, reccnt)

        For i As Integer = 0 To dsNkinhd.Tables(RS).Rows.Count - 1
            Sql = ""
            Sql += "INSERT INTO Public.t62_krnkinhd("
            Sql += "会社コード, 入金番号, 客先番号, 請求先コード, 請求先名, 振込先, 請求金額, 入金額計, 請求残高, 備考, 入金日, 登録日, 更新日, 更新者, 取消日, 取消区分, 締処理日)"
            Sql += " VALUES('"
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "', '"
            Sql += dsNkinhd.Tables(RS).Rows(i)("入金番号").ToString
            Sql += "', '"
            Sql += dsNkinhd.Tables(RS).Rows(i)("客先番号").ToString
            Sql += "', '"
            Sql += dsNkinhd.Tables(RS).Rows(i)("請求先コード").ToString
            Sql += "', '"
            Sql += dsNkinhd.Tables(RS).Rows(i)("請求先名").ToString
            Sql += "', '"
            Sql += dsNkinhd.Tables(RS).Rows(i)("振込先").ToString & "'"
            If dsNkinhd.Tables(RS).Rows(i)("請求金額") IsNot DBNull.Value Then
                Sql += " , " & dsNkinhd.Tables(RS).Rows(i)("請求金額").ToString
            Else
                Sql += " , 0"
            End If
            Sql += ", '"
            Sql += dsNkinhd.Tables(RS).Rows(i)("入金額").ToString & "'"
            If dsNkinhd.Tables(RS).Rows(i)("請求残高") IsNot DBNull.Value Then
                Sql += " , " & dsNkinhd.Tables(RS).Rows(i)("請求残高").ToString
            Else
                Sql += " , 0"
            End If
            Sql += ", '"
            Sql += dsNkinhd.Tables(RS).Rows(i)("備考").ToString
            Sql += "', '"
            Sql += dsNkinhd.Tables(RS).Rows(i)("入金日").ToString
            Sql += "', '"
            Sql += dsNkinhd.Tables(RS).Rows(i)("登録日").ToString
            Sql += "', '"
            Sql += dtToday
            Sql += "', '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            If dsNkinhd.Tables(RS).Rows(i)("取消日") IsNot DBNull.Value Then
                Sql += "', '"
                Sql += dsNkinhd.Tables(RS).Rows(i)("取消日").ToString
                Sql += "', "
            Else
                Sql += "', "
                Sql += "null"
                Sql += ", "
            End If
            If dsNkinhd.Tables(RS).Rows(i)("取消区分") IsNot DBNull.Value Then
                Sql += "'"
                Sql += dsNkinhd.Tables(RS).Rows(i)("取消区分").ToString
            Else
                Sql += "0"
            End If
            Sql += "', '"
            Sql += dtToday
            Sql += " ')"

            _db.executeDB(Sql)
        Next

        For i As Integer = 0 To dsNkinhd.Tables(RS).Rows.Count - 1
            Sql = ""
            Sql += "SELECT * FROM public.t26_nkindt"
            Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND 入金番号 = '" & dsNkinhd.Tables(RS).Rows(i)("入金番号") & "'"

            Dim dsNkindt As DataSet = _db.selectDB(Sql, RS, reccnt)

            For x As Integer = 0 To dsNkindt.Tables(RS).Rows.Count - 1
                Sql = ""
                Sql += "INSERT INTO Public.t63_krnkindt("
                Sql += "会社コード, 入金番号, 行番号, 入金種別, 入金種別名, 請求先コード, 請求先名, 振込先, 入金額, 備考, 入金日, 更新者, 更新日)"
                Sql += " VALUES('"
                Sql += frmC01F10_Login.loginValue.BumonCD
                Sql += "', '"
                Sql += dsNkindt.Tables(RS).Rows(x)("入金番号").ToString
                Sql += "', '"
                Sql += dsNkindt.Tables(RS).Rows(x)("行番号").ToString
                Sql += "', '"
                Sql += dsNkindt.Tables(RS).Rows(x)("入金種別").ToString
                Sql += "', '"
                Sql += dsNkindt.Tables(RS).Rows(x)("入金種別名").ToString
                Sql += "', '"
                Sql += dsNkindt.Tables(RS).Rows(x)("請求先コード").ToString
                Sql += "', '"
                Sql += dsNkindt.Tables(RS).Rows(x)("請求先名").ToString
                Sql += "', '"
                Sql += dsNkindt.Tables(RS).Rows(x)("振込先").ToString
                Sql += "', '"
                Sql += dsNkindt.Tables(RS).Rows(x)("入金額").ToString
                Sql += "', '"
                Sql += dsNkindt.Tables(RS).Rows(x)("備考").ToString
                Sql += "', '"
                Sql += dsNkindt.Tables(RS).Rows(x)("入金日").ToString
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoNM
                Sql += "', '"
                Sql += dtToday
                Sql += " ')"

                _db.executeDB(Sql)
            Next
        Next
#End Region

#Region "CSV"
        Sql = ""
        Sql += "SELECT * FROM public.t52_krurighd"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        'Sql += " AND "
        'Sql += "締処理日"
        'Sql += " = "
        'Sql += "'"
        'Sql += dtToday
        'Sql += "'"
        Dim csvUrighd As DataSet = _db.selectDB(Sql, RS, reccnt)

        ConvertDataTableToCsv(csvUrighd, "t53_krurigdt", "売上番号", "Uriage")

        Sql = ""
        Sql += "SELECT * FROM public.t54_krsirehd"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        'Sql += " AND "
        'Sql += "締処理日"
        'Sql += " = "
        'Sql += "'"
        'Sql += dtToday
        'Sql += "'"
        Dim csvSirehd As DataSet = _db.selectDB(Sql, RS, reccnt)

        ConvertDataTableToCsv(csvSirehd, "t55_krsiredt", "仕入番号", "Siire")

        Sql = ""
        Sql += "SELECT * FROM public.t56_krskyuhd"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        'Sql += " AND "
        'Sql += "締処理日"
        'Sql += " = "
        'Sql += "'"
        'Sql += dtToday
        'Sql += "'"
        Dim csvSkyuhd As DataSet = _db.selectDB(Sql, RS, reccnt)

        ConvertDataTableToCsvSingle(csvSkyuhd, "Maeuke")

        Sql = ""
        Sql += "SELECT * FROM public.t57_krkikehd"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        'Sql += " AND "
        'Sql += "締処理日"
        'Sql += " = "
        'Sql += "'"
        'Sql += dtToday
        'Sql += "'"
        Dim csvKikehd As DataSet = _db.selectDB(Sql, RS, reccnt)

        ConvertDataTableToCsvSingle(csvKikehd, "Maebarai")

        Sql = ""
        Sql += "SELECT * FROM public.t58_krnyukohd"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        'Sql += " AND "
        'Sql += "締処理日"
        'Sql += " = "
        'Sql += "'"
        'Sql += dtToday
        'Sql += "'"
        Dim csvNyukohd As DataSet = _db.selectDB(Sql, RS, reccnt)

        ConvertDataTableToCsv(csvNyukohd, "t59_krnyukodt", "入庫番号", "Nyuko")

        Sql = ""
        Sql += "SELECT * FROM public.t60_krshukohd"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        'Sql += " AND "
        'Sql += "締処理日"
        'Sql += " = "
        'Sql += "'"
        'Sql += dtToday
        'Sql += "'"
        Dim csvShukohd As DataSet = _db.selectDB(Sql, RS, reccnt)

        ConvertDataTableToCsv(csvShukohd, "t61_krshukodt", "出庫番号", "Shuko")

        Sql = ""
        Sql += "SELECT * FROM public.t62_krnkinhd"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        'Sql += " AND "
        'Sql += "締処理日"
        'Sql += " = "
        'Sql += "'"
        'Sql += dtToday
        'Sql += "'"
        Dim csvNkinhd As DataSet = _db.selectDB(Sql, RS, reccnt)

        ConvertDataTableToCsv(csvNkinhd, "t63_krnkindt", "入金番号", "Nyukin")

        Sql = ""
        Sql += "SELECT * FROM public.t64_krshrihd"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        'Sql += " AND "
        'Sql += "締処理日"
        'Sql += " = "
        'Sql += "'"
        'Sql += dtToday
        'Sql += "'"
        Dim csvShrihd As DataSet = _db.selectDB(Sql, RS, reccnt)

        ConvertDataTableToCsv(csvShrihd, "t65_krshridt", "支払番号", "Siharai")

        Sql = ""
        Sql += "SELECT * FROM public.t66_swkhd"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        'Sql += " AND "
        'Sql += "仕分日"
        'Sql += " = "
        'Sql += "'"
        'Sql += dtToday
        'Sql += "'"
        Dim csvSwkhd As DataSet = _db.selectDB(Sql, RS, reccnt)

        ConvertDataTableToCsvSingle(csvSwkhd, "Shiwake")
#End Region
        _msgHd.dspMSG("CreateExcel", frmC01F10_Login.loginValue.Language)

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
            Sql += "SELECT * FROM public." & tableName
            Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND " & ColName & " = '" & ds1.Tables(RS).Rows(i)(ColName) & "'"

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
    'どんなカルチャーであっても、日本の形式に変換する
    Private Function formatDatetime(ByVal prmDatetime As DateTime) As String

        'PCのカルチャーを取得し、それに応じてStringからDatetimeを作成
        Dim ciCurrent As New System.Globalization.CultureInfo(CultureInfo.CurrentCulture.Name.ToString)
        Dim dateFormat As DateTime = DateTime.Parse(prmDatetime.ToString, ciCurrent, System.Globalization.DateTimeStyles.AssumeLocal)

        Dim changeFormat As String = dateFormat.ToString("yyyy/MM/dd HH:mm:ss")

        Dim ciJP As New System.Globalization.CultureInfo(CommonConst.CI_JP)
        Dim rtnDatetime As DateTime = DateTime.Parse(changeFormat, ciJP, System.Globalization.DateTimeStyles.AssumeLocal)


        '日本の形式に書き換える
        Return changeFormat
    End Function

End Class