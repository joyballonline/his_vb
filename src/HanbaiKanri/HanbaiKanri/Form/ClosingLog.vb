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
    Private _parentForm As Form
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
                   ByRef prmRefForm As Form,
                   Optional ByRef prmRefStatus As String = "")
        Call Me.New()

        _init = False

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _langHd = prmRefLang
        _parentForm = prmRefForm
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
                DgvClosingLog.Rows(index).Cells("前回締日").Value = ds.Tables(RS).Rows(index)("前回締日").ToShortDateString
                DgvClosingLog.Rows(index).Cells("今回締日").Value = ds.Tables(RS).Rows(index)("今回締日").ToShortDateString
                DgvClosingLog.Rows(index).Cells("次回締日").Value = ds.Tables(RS).Rows(index)("次回締日").ToShortDateString
                DgvClosingLog.Rows(index).Cells("担当者").Value = ds.Tables(RS).Rows(index)("担当者")
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

        DgvClosingLog.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

    End Sub

    Private Sub ClosingLog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ClosingLogLoad()
        If frmC01F10_Login.loginValue.Language = "ENG" Then
            LblPerson.Text = "Name of PIC"
            BtnClosing.Text = "Closing"
            BtnBack.Text = "Back"
            BtnSearch.Text = "Search"

            DgvClosingLog.Columns("締処理日時").HeaderText = "ClosingDate"
            DgvClosingLog.Columns("前回締日").HeaderText = "LastClosingDate"
            DgvClosingLog.Columns("今回締日").HeaderText = "ThisClosingDate"
            DgvClosingLog.Columns("次回締日").HeaderText = "NextClosingDate"
            DgvClosingLog.Columns("担当者").HeaderText = "Name of PIC"

            BtnOutput.Text = "JournalOutput"
            LblConditions.Text = "■ExtractionCondition"
        End If
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
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
            Sql += " WHERE "
            Sql += "担当者"
            Sql += " ILIKE "
            Sql += "'%"
            Sql += TxtPerson.Text
            Sql += "%'"

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
                Sql8 += " , " & ds6.Tables(RS).Rows(i)("仕入値").ToString     '今月単価
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
                Sql += UtilClass.strFormatDate(dsNkinCymn.Tables(RS).Rows(0)("受注日").ToString)
                Sql += "', '"
                Sql += "現金預金"
                Sql += "', '"
                Sql += UtilClass.formatNumber(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計").ToString)
                Sql += "', '"
                Sql += "前受金"
                Sql += "', '"
                Sql += UtilClass.formatNumber(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計").ToString)
                Sql += "')"

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
                Sql += UtilClass.strFormatDate((dsNkinCymn.Tables(RS).Rows(0)("受注日").ToString))
                Sql += "', '"
                Sql += "前受金"
                Sql += "', '"
                Sql += UtilClass.formatNumber(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計").ToString)
                Sql += "', '"
                Sql += "売掛金"
                Sql += "', '"
                Sql += UtilClass.formatNumber(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計").ToString)
                Sql += "')"

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
                Sql += UtilClass.strFormatDate(dsNkinCymn.Tables(RS).Rows(0)("受注日").ToString)
                Sql += "', '"
                Sql += "現金預金"
                Sql += "', '"
                Sql += UtilClass.formatNumber(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計").ToString)
                Sql += "', '"
                Sql += "売掛金"
                Sql += "', '"
                Sql += UtilClass.formatNumber(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計").ToString)
                Sql += "')"

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
            Sql += UtilClass.strFormatDate(dsSwkUrighd.Tables(RS).Rows(i)("売上日").ToString)
            Sql += "', '"
            Sql += "売掛金"
            Sql += "', '"
            Sql += UtilClass.formatNumber(dsSwkUrighd.Tables(RS).Rows(i)("売上金額").ToString)
            Sql += "', '"
            Sql += "売上"
            Sql += "', '"
            Sql += UtilClass.formatNumber(dsSwkUrighd.Tables(RS).Rows(i)("売上金額").ToString)
            Sql += "')"
            _db.executeDB(Sql)

            Dim VATOUT As Double = 0
            'VATOUT = dsSwkUrighd.Tables(RS).Rows(i)("売上金額") * dsSwkUrighd.Tables(RS).Rows(i)("ＶＡＴ") / 100
            VATOUT = dsSwkUrighd.Tables(RS).Rows(i)("ＶＡＴ")
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
            Sql += UtilClass.strFormatDate(dsSwkUrighd.Tables(RS).Rows(i)("売上日").ToString)
            Sql += "', '"
            Sql += "売掛金"
            Sql += "', '"
            Sql += UtilClass.formatNumber(VATOUT.ToString)
            Sql += "', '"
            Sql += "VAT-OUT"
            Sql += "', '"
            Sql += UtilClass.formatNumber(VATOUT.ToString)
            Sql += "')"

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
                Sql += UtilClass.strFormatDate(dsShriHattyu.Tables(RS).Rows(0)("発注日").ToString)
                Sql += "', '"
                Sql += "前払金"
                Sql += "', '"
                Sql += UtilClass.formatNumber(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計").ToString)
                Sql += "', '"
                Sql += "現金預金"
                Sql += "', '"
                Sql += UtilClass.formatNumber(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計").ToString)
                Sql += "')"

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
                Sql += UtilClass.strFormatDate(dsShriHattyu.Tables(RS).Rows(0)("発注日").ToString)
                Sql += "', '"
                Sql += "買掛金"
                Sql += "', '"
                Sql += UtilClass.formatNumber(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計").ToString)
                Sql += "', '"
                Sql += "前払金"
                Sql += "', '"
                Sql += UtilClass.formatNumber(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計").ToString)
                Sql += "')"

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
                Sql += UtilClass.strFormatDate(dsShriHattyu.Tables(RS).Rows(0)("発注日").ToString)
                Sql += "', '"
                Sql += "買掛金"
                Sql += "', '"
                Sql += UtilClass.formatNumber(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計").ToString)
                Sql += "', '"
                Sql += "現金預金"
                Sql += "', '"
                Sql += UtilClass.formatNumber(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計").ToString)
                Sql += "')"

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
                Sql += UtilClass.strFormatDate(dsSwkHattyu.Tables(RS).Rows(0)("発注日").ToString)
                Sql += "', '"
                Sql += "棚卸資産"
                Sql += "', '"
                Sql += UtilClass.formatNumber(dsSWKNyukohd.Tables(RS).Rows(x)("仕入金額").ToString)
                Sql += "', '"
                Sql += "買掛金"
                Sql += "', '"
                Sql += UtilClass.formatNumber(dsSWKNyukohd.Tables(RS).Rows(x)("仕入金額").ToString)
                Sql += "')"
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
                Sql += UtilClass.strFormatDate(dsSwkHattyu.Tables(RS).Rows(0)("発注日").ToString)
                Sql += "', '"
                Sql += "仕入"
                Sql += "', '"
                Sql += UtilClass.formatNumber(dsSWKNyukohd.Tables(RS).Rows(x)("仕入金額").ToString)
                Sql += "', '"
                Sql += "棚卸資産"
                Sql += "', '"
                Sql += UtilClass.formatNumber(dsSWKNyukohd.Tables(RS).Rows(x)("仕入金額").ToString)
                Sql += "')"


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
            Sql += UtilClass.strFormatDate(dsSwkHattyu.Tables(RS).Rows(0)("発注日").ToString)
            Sql += "', '"
            Sql += "VAT-IN"
            Sql += "', '"
            Sql += UtilClass.formatNumber(VATIN.ToString)
            Sql += "', '"
            Sql += "買掛金"
            Sql += "', '"
            Sql += UtilClass.formatNumber(VATIN.ToString)
            Sql += "')"
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
            Sql += "',"
            If IsDBNull(dsUrighd.Tables(RS).Rows(i)("得意先郵便番号")) OrElse dsUrighd.Tables(RS).Rows(i)("得意先郵便番号").ToString = vbNullString Then
                Sql += "null"
            Else
                Sql += UtilClass.formatNumber(dsUrighd.Tables(RS).Rows(i)("得意先郵便番号").ToString）
            End If
            Sql += ", '"
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
            Sql += UtilClass.strFormatDate(dsUrighd.Tables(RS).Rows(i)("見積日").ToString)
            Sql += "', '"
            Sql += UtilClass.strFormatDate(dsUrighd.Tables(RS).Rows(i)("見積有効期限").ToString)
            Sql += "', '"
            Sql += dsUrighd.Tables(RS).Rows(i)("支払条件").ToString
            Sql += "', '"
            Sql += UtilClass.formatNumber(dsUrighd.Tables(RS).Rows(i)("見積金額").ToString)
            Sql += "', '"
            Sql += UtilClass.formatNumber(dsUrighd.Tables(RS).Rows(i)("仕入金額").ToString)
            Sql += "', '"
            Sql += UtilClass.formatNumber(dsUrighd.Tables(RS).Rows(i)("売上金額").ToString)
            Sql += "', '"
            Sql += UtilClass.formatNumber(dsUrighd.Tables(RS).Rows(i)("粗利額").ToString)
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
                Sql += UtilClass.formatNumber(dsUrighd.Tables(RS).Rows(i)("ＶＡＴ").ToString)
            End If
            Sql += "', '"
            If dsUrighd.Tables(RS).Rows(i)("ＰＰＨ") Is DBNull.Value Then
                Sql += "0"
            Else
                Sql += UtilClass.formatNumber(dsUrighd.Tables(RS).Rows(i)("ＰＰＨ").ToString)
            End If
            Sql += "', '"
            Sql += UtilClass.strFormatDate(dsUrighd.Tables(RS).Rows(i)("受注日").ToString)
            Sql += "', '"
            Sql += UtilClass.strFormatDate(dsUrighd.Tables(RS).Rows(i)("売上日").ToString)
            Sql += "', '"
            Sql += UtilClass.strFormatDate(dtToday)

            If dsUrighd.Tables(RS).Rows(i)("入金予定日") IsNot DBNull.Value Then
                Sql += "', '"
                Sql += UtilClass.strFormatDate(dsUrighd.Tables(RS).Rows(i)("入金予定日").ToString)
                Sql += "', '"
            Else
                Sql += "', "
                Sql += "null"
                Sql += ", '"
            End If

            Sql += UtilClass.strFormatDate(dsUrighd.Tables(RS).Rows(i)("登録日").ToString)
            Sql += "', '"
            Sql += UtilClass.strFormatDate(dtToday)
            Sql += "', '"
            Sql += frmC01F10_Login.loginValue.TantoNM

            If dsUrighd.Tables(RS).Rows(i)("取消日") IsNot DBNull.Value Then
                Sql += "', '"
                Sql += UtilClass.strFormatDate(dsUrighd.Tables(RS).Rows(i)("取消日").ToString)
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

            Sql += "')"

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
                            Sql += UtilClass.formatNumber(dsSiredt.Tables(RS).Rows(y)("仕入値").ToString)
                            Sql += "', '"
                            Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("受注数量").ToString)
                            Sql += "', '"
                            Sql += UtilClass.formatNumber(Qty.ToString)
                            Sql += "', '"
                            Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("受注残数").ToString)
                            Sql += "', '"
                            Sql += dsUrigdt.Tables(RS).Rows(x)("単位").ToString
                            Sql += "', '"
                            Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("仕入原価").ToString)
                            Sql += "', '"
                            Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("関税率").ToString)
                            Sql += "', '"
                            Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("関税額").ToString)
                            Sql += "', '"
                            Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("前払法人税率").ToString)
                            Sql += "', '"
                            Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("前払法人税額").ToString)
                            Sql += "', '"
                            Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("輸送費率").ToString)
                            Sql += "', '"
                            Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("輸送費額").ToString)
                            Sql += "', '"
                            Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("間接費").ToString)
                            Sql += "', '"
                            Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("仕入金額").ToString)
                            Sql += "', '"
                            Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("売単価").ToString)
                            Sql += "', '"
                            Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("売上金額").ToString)
                            Sql += "', '"
                            Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("見積単価").ToString)
                            Sql += "', '"
                            Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("見積金額").ToString)
                            Sql += "', '"
                            Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("粗利額").ToString)
                            Sql += "', '"
                            Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("粗利率").ToString)
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
                                Sql += UtilClass.strFormatDate(dsUrigdt.Tables(RS).Rows(x)("入金日").ToString)
                                Sql += "', '"
                            Else
                                Sql += "', "
                                Sql += "null"
                                Sql += ", '"
                            End If

                            Sql += frmC01F10_Login.loginValue.TantoNM
                            Sql += "', '"
                            Sql += UtilClass.strFormatDate(dtToday)
                            Sql += "')"

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
                    Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("仕入値").ToString)
                    Sql += "', '"
                    Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("受注数量").ToString)
                    Sql += "', '"
                    Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("売上数量").ToString)
                    Sql += "', '"
                    Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("受注残数").ToString)
                    Sql += "', '"
                    Sql += dsUrigdt.Tables(RS).Rows(x)("単位").ToString
                    Sql += "', '"
                    Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("仕入原価").ToString)
                    Sql += "', '"
                    Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("関税率").ToString)
                    Sql += "', '"
                    Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("関税額").ToString)
                    Sql += "', '"
                    Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("前払法人税率").ToString)
                    Sql += "', '"
                    Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("前払法人税額").ToString)
                    Sql += "', '"
                    Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("輸送費率").ToString)
                    Sql += "', '"
                    Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("輸送費額").ToString)
                    Sql += "', '"
                    Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("間接費").ToString)
                    Sql += "', '"
                    Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("仕入金額").ToString)
                    Sql += "', '"
                    Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("売単価").ToString)
                    Sql += "', '"
                    Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("売上金額").ToString)
                    Sql += "', '"
                    Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("見積単価").ToString)
                    Sql += "', '"
                    Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("見積金額").ToString)
                    Sql += "', '"
                    Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("粗利額").ToString)
                    Sql += "', '"
                    Sql += UtilClass.formatNumber(dsUrigdt.Tables(RS).Rows(x)("粗利率").ToString)
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
                        Sql += UtilClass.strFormatDate(dsUrigdt.Tables(RS).Rows(x)("入金日").ToString)
                        Sql += "', '"
                    Else
                        Sql += "', "
                        Sql += "null"
                        Sql += ", '"
                    End If
                    Sql += frmC01F10_Login.loginValue.TantoNM
                    Sql += "', '"
                    Sql += UtilClass.strFormatDate(dtToday)
                    Sql += "')"
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
            Sql += UtilClass.formatNumber(dsSirehd.Tables(RS).Rows(i)("仕入金額").ToString)
            Sql += "', '"
            Sql += UtilClass.formatNumber(dsSirehd.Tables(RS).Rows(i)("粗利額").ToString)
            Sql += "', '"
            Sql += dsSirehd.Tables(RS).Rows(i)("営業担当者").ToString
            Sql += "', '"
            Sql += dsSirehd.Tables(RS).Rows(i)("入力担当者").ToString
            Sql += "', '"
            Sql += dsSirehd.Tables(RS).Rows(i)("備考").ToString

            If dsSirehd.Tables(RS).Rows(i)("取消日") IsNot DBNull.Value Then
                Sql += "', '"
                Sql += UtilClass.strFormatDate(dsSirehd.Tables(RS).Rows(i)("取消日").ToString)
                Sql += "', '"
            Else
                Sql += "', "
                Sql += "null"
                Sql += ", '"
            End If

            Sql += dsSirehd.Tables(RS).Rows(i)("取消区分").ToString
            Sql += "', '"
            Sql += UtilClass.formatNumber(dsSirehd.Tables(RS).Rows(i)("ＶＡＴ").ToString)
            Sql += "', '"
            Sql += UtilClass.formatNumber(dsSirehd.Tables(RS).Rows(i)("ＰＰＨ").ToString)
            Sql += "', '"
            Sql += UtilClass.strFormatDate(dsSirehd.Tables(RS).Rows(i)("仕入日").ToString)
            Sql += "', '"
            Sql += UtilClass.strFormatDate(dsSirehd.Tables(RS).Rows(i)("登録日").ToString)
            Sql += "', '"
            Sql += UtilClass.strFormatDate(dtToday)
            Sql += "', '"
            Sql += UtilClass.strFormatDate(dtToday)
            Sql += "', '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            Sql += "')"

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
                Sql += UtilClass.formatNumber(dssiredt.Tables(RS).Rows(x)("仕入値").ToString)
                Sql += "', '"
                Sql += UtilClass.formatNumber(dssiredt.Tables(RS).Rows(x)("発注数量").ToString)
                Sql += "', '"
                Sql += UtilClass.formatNumber(dssiredt.Tables(RS).Rows(x)("仕入数量").ToString)
                Sql += "', '"
                Sql += UtilClass.formatNumber(dssiredt.Tables(RS).Rows(x)("発注残数").ToString)
                Sql += "', '"
                Sql += dssiredt.Tables(RS).Rows(x)("単位").ToString
                Sql += "', '"
                Sql += UtilClass.formatNumber(rmNullDecimal(dssiredt.Tables(RS).Rows(x)("仕入単価").ToString))
                Sql += "', '"
                Sql += UtilClass.formatNumber(dssiredt.Tables(RS).Rows(x)("仕入金額").ToString)
                Sql += "', '"
                Sql += UtilClass.formatNumber(dssiredt.Tables(RS).Rows(x)("間接費").ToString)
                Sql += "', '"
                Sql += dssiredt.Tables(RS).Rows(x)("リードタイム").ToString
                Sql += "', '"
                Sql += dssiredt.Tables(RS).Rows(x)("備考").ToString
                Sql += "', '"
                Sql += UtilClass.strFormatDate(dssiredt.Tables(RS).Rows(x)("仕入日").ToString)
                If dssiredt.Tables(RS).Rows(x)("支払日") IsNot DBNull.Value Then
                    Sql += "', '"
                    Sql += UtilClass.strFormatDate(dssiredt.Tables(RS).Rows(x)("支払日").ToString)
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
                Sql += UtilClass.strFormatDate(dtToday)
                Sql += "')"
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
            Sql += UtilClass.strFormatDate(dsSkyuhd.Tables(RS).Rows(i)("請求日").ToString)
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
            Sql += UtilClass.formatNumber(dsSkyuhd.Tables(RS).Rows(i)("請求金額計").ToString)

            If dsSkyuhd.Tables(RS).Rows(i)("入金額計") IsNot DBNull.Value Then
                Sql += "', '"
                Sql += UtilClass.formatNumber(dsSkyuhd.Tables(RS).Rows(i)("入金額計").ToString)
                Sql += "', '"
            Else
                Sql += "', "
                Sql += "0"
                Sql += ", '"
            End If

            Sql += UtilClass.formatNumber(dsSkyuhd.Tables(RS).Rows(i)("売掛残高").ToString)
            Sql += "', '"
            Sql += dsSkyuhd.Tables(RS).Rows(i)("備考1").ToString
            Sql += "', '"
            Sql += dsSkyuhd.Tables(RS).Rows(i)("備考2").ToString
            Sql += "', '"
            Sql += dsSkyuhd.Tables(RS).Rows(i)("入金番号").ToString


            If dsSkyuhd.Tables(RS).Rows(i)("入金完了日") IsNot DBNull.Value Then
                Sql += "', '"
                Sql += UtilClass.strFormatDate(dsSkyuhd.Tables(RS).Rows(i)("入金完了日").ToString)
                Sql += "', "
            Else
                Sql += "', "
                Sql += "null"
                Sql += ", "
            End If

            If dsSkyuhd.Tables(RS).Rows(i)("取消日") IsNot DBNull.Value Then
                Sql += "'"
                Sql += UtilClass.strFormatDate(dsSkyuhd.Tables(RS).Rows(i)("取消日").ToString)
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
            Sql += UtilClass.strFormatDate(dsSkyuhd.Tables(RS).Rows(i)("登録日").ToString)
            Sql += "', '"
            Sql += UtilClass.strFormatDate(dtToday)
            Sql += "', '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            Sql += "')"

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
            Sql += UtilClass.strFormatDate(dsKikehd.Tables(RS).Rows(i)("買掛日").ToString)
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
            Sql += UtilClass.formatNumber(dsKikehd.Tables(RS).Rows(i)("買掛金額計").ToString)

            If dsKikehd.Tables(RS).Rows(i)("支払金額計") IsNot DBNull.Value Then
                Sql += "', '"
                Sql += UtilClass.formatNumber(dsKikehd.Tables(RS).Rows(i)("支払金額計").ToString)
                Sql += "', '"
            Else
                Sql += "', '"
                Sql += "0"
                Sql += "', '"
            End If

            Sql += UtilClass.formatNumber(dsKikehd.Tables(RS).Rows(i)("買掛残高").ToString)
            Sql += "', '"
            Sql += dsKikehd.Tables(RS).Rows(i)("備考1").ToString
            Sql += "', '"
            Sql += dsKikehd.Tables(RS).Rows(i)("備考2").ToString

            If dsKikehd.Tables(RS).Rows(i)("支払完了日") IsNot DBNull.Value Then
                Sql += "', '"
                Sql += UtilClass.strFormatDate(dsKikehd.Tables(RS).Rows(i)("支払完了日").ToString)
                Sql += "', "
            Else
                Sql += "', "
                Sql += "null"
                Sql += ", "
            End If

            If dsKikehd.Tables(RS).Rows(i)("取消日") IsNot DBNull.Value Then
                Sql += "'"
                Sql += UtilClass.strFormatDate(dsKikehd.Tables(RS).Rows(i)("取消日").ToString)
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
            Sql += UtilClass.strFormatDate(dsKikehd.Tables(RS).Rows(i)("登録日").ToString)
            Sql += "', '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            Sql += "', '"
            Sql += UtilClass.strFormatDate(dtToday)
            Sql += "')"

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
            Sql += UtilClass.formatNumber(dsNyukohd.Tables(RS).Rows(i)("仕入金額").ToString)
            Sql += "', '"
            Sql += UtilClass.formatNumber(dsNyukohd.Tables(RS).Rows(i)("粗利額").ToString)
            Sql += "', '"
            If dsNyukohd.Tables(RS).Rows(i)("ＶＡＴ") Is DBNull.Value Then
                Sql += "0"
            Else
                Sql += UtilClass.formatNumber(dsNyukohd.Tables(RS).Rows(i)("ＶＡＴ").ToString)
            End If
            Sql += "', '"
            If dsNyukohd.Tables(RS).Rows(i)("ＰＰＨ") Is DBNull.Value Then
                Sql += "0"
            Else
                Sql += UtilClass.formatNumber(dsNyukohd.Tables(RS).Rows(i)("ＰＰＨ").ToString)
            End If
            Sql += "', '"
            Sql += dsNyukohd.Tables(RS).Rows(i)("営業担当者").ToString
            Sql += "', '"
            Sql += dsNyukohd.Tables(RS).Rows(i)("入力担当者").ToString
            Sql += "', '"
            Sql += dsNyukohd.Tables(RS).Rows(i)("備考").ToString
            Sql += "', '"
            Sql += UtilClass.strFormatDate(dsNyukohd.Tables(RS).Rows(i)("入庫日").ToString)
            Sql += "', '"
            Sql += UtilClass.strFormatDate(dsNyukohd.Tables(RS).Rows(i)("登録日").ToString)
            Sql += "', '"
            Sql += UtilClass.strFormatDate(dtToday)
            Sql += "', '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            If dsNyukohd.Tables(RS).Rows(i)("取消日") IsNot DBNull.Value Then
                Sql += "', '"
                Sql += UtilClass.strFormatDate(dsNyukohd.Tables(RS).Rows(i)("取消日").ToString)
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
            Sql += UtilClass.strFormatDate(dtToday)
            Sql += "')"


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
                Sql += UtilClass.formatNumber(dsNyukodt.Tables(RS).Rows(x)("仕入値").ToString)
                Sql += "', '"
                Sql += UtilClass.formatNumber(dsNyukodt.Tables(RS).Rows(x)("入庫数量").ToString)
                Sql += "', '"
                Sql += dsNyukodt.Tables(RS).Rows(x)("単位").ToString
                Sql += "', '"
                Sql += dsNyukodt.Tables(RS).Rows(x)("備考").ToString
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoNM
                Sql += "', '"
                Sql += UtilClass.strFormatDate(dtToday)
                Sql += "')"

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
            Sql += "', "
            If IsDBNull(dsShukohd.Tables(RS).Rows(i)("得意先郵便番号")) OrElse dsShukohd.Tables(RS).Rows(i)("得意先郵便番号") = vbNullString Then
                Sql += "null"
            Else
                Sql += dsShukohd.Tables(RS).Rows(i)("得意先郵便番号").ToString
            End If
            Sql += ", '"
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
            Sql += UtilClass.strFormatDate(dsShukohd.Tables(RS).Rows(i)("出庫日").ToString)
            Sql += "', '"
            Sql += UtilClass.strFormatDate(dsShukohd.Tables(RS).Rows(i)("登録日").ToString)
            Sql += "', '"
            Sql += UtilClass.strFormatDate(dtToday)
            Sql += "', '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            If dsShukohd.Tables(RS).Rows(i)("取消日") IsNot DBNull.Value Then
                Sql += "', '"
                Sql += UtilClass.strFormatDate(dsShukohd.Tables(RS).Rows(i)("取消日").ToString)
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
            Sql += UtilClass.strFormatDate(dtToday)
            Sql += "')"

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
                Sql += UtilClass.formatNumber(dsShukodt.Tables(RS).Rows(x)("出庫数量").ToString)
                Sql += "', '"
                Sql += dsShukodt.Tables(RS).Rows(x)("単位").ToString
                Sql += "', '"
                Sql += UtilClass.formatNumber(dsShukodt.Tables(RS).Rows(x)("売単価").ToString)
                Sql += "', '"
                Sql += dsShukodt.Tables(RS).Rows(x)("備考").ToString
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoNM
                Sql += "', '"
                Sql += UtilClass.strFormatDate(dtToday)
                Sql += "')"

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
            Sql += UtilClass.formatNumber(dsShrihd.Tables(RS).Rows(i)("買掛金額").ToString)
            Sql += "', '"
            Sql += UtilClass.formatNumber(dsShrihd.Tables(RS).Rows(i)("支払金額計").ToString)
            Sql += "', '"
            Sql += UtilClass.formatNumber(dsShrihd.Tables(RS).Rows(i)("買掛残高").ToString)
            Sql += "', '"
            Sql += dsShrihd.Tables(RS).Rows(i)("備考").ToString
            Sql += "', '"
            Sql += UtilClass.strFormatDate(dsShrihd.Tables(RS).Rows(i)("支払日").ToString)
            Sql += "', '"
            Sql += UtilClass.strFormatDate(dsShrihd.Tables(RS).Rows(i)("登録日").ToString)
            Sql += "', '"
            Sql += UtilClass.strFormatDate(dtToday)
            Sql += "', '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            If dsShrihd.Tables(RS).Rows(i)("取消日") IsNot DBNull.Value Then
                Sql += "', '"
                Sql += UtilClass.strFormatDate(dsShrihd.Tables(RS).Rows(i)("取消日").ToString)
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
            Sql += UtilClass.strFormatDate(dtToday)
            Sql += "')"

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
                Sql += UtilClass.formatNumber(dsShridt.Tables(RS).Rows(x)("支払金額").ToString)
                Sql += "', '"
                Sql += dsShridt.Tables(RS).Rows(x)("備考").ToString
                Sql += "', '"
                Sql += UtilClass.strFormatDate(dsShridt.Tables(RS).Rows(x)("支払日").ToString)
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoNM
                Sql += "', '"
                Sql += UtilClass.strFormatDate(dtToday)
                Sql += "')"

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
                Sql += " , " & UtilClass.formatNumber(dsNkinhd.Tables(RS).Rows(i)("請求金額").ToString)
            Else
                Sql += " , 0"
            End If
            Sql += ", '"
            Sql += UtilClass.formatNumber(dsNkinhd.Tables(RS).Rows(i)("入金額").ToString) & "'"
            If dsNkinhd.Tables(RS).Rows(i)("請求残高") IsNot DBNull.Value Then
                Sql += " , " & UtilClass.formatNumber(dsNkinhd.Tables(RS).Rows(i)("請求残高").ToString)
            Else
                Sql += " , 0"
            End If
            Sql += ", '"
            Sql += dsNkinhd.Tables(RS).Rows(i)("備考").ToString
            Sql += "', '"
            Sql += UtilClass.strFormatDate(dsNkinhd.Tables(RS).Rows(i)("入金日").ToString)
            Sql += "', '"
            Sql += UtilClass.strFormatDate(dsNkinhd.Tables(RS).Rows(i)("登録日").ToString)
            Sql += "', '"
            Sql += UtilClass.strFormatDate(dtToday)
            Sql += "', '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            If dsNkinhd.Tables(RS).Rows(i)("取消日") IsNot DBNull.Value Then
                Sql += "', '"
                Sql += UtilClass.strFormatDate(dsNkinhd.Tables(RS).Rows(i)("取消日").ToString)
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
            Sql += UtilClass.strFormatDate(dtToday)
            Sql += "')"

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
                Sql += UtilClass.formatNumber(dsNkindt.Tables(RS).Rows(x)("入金額").ToString)
                Sql += "', '"
                Sql += dsNkindt.Tables(RS).Rows(x)("備考").ToString
                Sql += "', '"
                Sql += UtilClass.strFormatDate(dsNkindt.Tables(RS).Rows(x)("入金日").ToString)
                Sql += "', '"
                Sql += frmC01F10_Login.loginValue.TantoNM
                Sql += "', '"
                Sql += UtilClass.strFormatDate(dtToday)
                Sql += "')"

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

        'ConvertDataTableToCsvSingle(csvSwkhd, "Shiwake")
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

    '仕訳データCSV出力
    Public Sub SiwakeConvertDataTableToCsv()

        Dim Sql As String
        Dim reccnt As Integer
        Sql = ""
        Sql += "SELECT * FROM public.t67_swkhd"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " order by 11,4,5"
        Dim ds1 As DataSet = _db.selectDB(Sql, RS, reccnt)

        Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("utf-8")
        '出力先パス
        Dim sOutPath As String = ""
        sOutPath = StartUp._iniVal.OutXlsPath

        '出力ファイル名
        Dim sOutFile As String = ""
        sOutFile = sOutPath & "\siwake.csv"

        '書き込むファイルを開く
        Dim sr As New System.IO.StreamWriter(sOutFile, False, enc)

        For i As Integer = 0 To ds1.Tables(RS).Rows.Count - 1
            '列名
            If i = 0 Then
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


            '明細
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

    Public Sub ConvertDataTableToCsvSingle(ds1 As DataSet, Name As String)


        Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
        '出力先パス
        Dim sOutPath As String = ""
        sOutPath = StartUp._iniVal.OutXlsPath

        '出力ファイル名
        Dim sOutFile As String = ""
        sOutFile = sOutPath & "\" & Name & ".csv"

        'カーソルをビジー状態にする
        Cursor.Current = Cursors.WaitCursor

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

        'カーソルをビジー状態から元に戻す
        Cursor.Current = Cursors.Default

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


    Private Sub BtnOutput_Click(sender As Object, e As EventArgs) Handles BtnOutput.Click

        '仕訳用テーブルをいったん削除　テスト用
        _db.executeDB("truncate table t67_swkhd")

        Cursor.Current = Cursors.WaitCursor

        '仕訳データを作成する
        getShiwakeData()

        'CSVファイル出力
        SiwakeConvertDataTableToCsv()

        '現在日時を取得
        Dim nowDatetime As String = DateTime.Now.ToString("yyyyMMddHHmmss")

        'xmlファイル内容の初期化
        Dim strXml As String
        Dim reccnt As Integer = 0

        Dim shiwakeSql As String = ""
        Dim shiwakeData As DataSet
        Dim branchCodeSql As String = ""
        Dim branchCode As DataSet
        Try

            '会計用コードの取得
            branchCodeSql += " WHERE "
            branchCodeSql += """会社コード"" = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            branchCode = _db.selectDB(allSelectSql("m01_company", branchCodeSql), RS, reccnt) 'reccnt:(省略可能)SELECT文の取得レコード件数

            Dim getRow As DataRow
            getRow = branchCode.Tables(0).Rows(0)


            shiwakeSql += " select t67.*,m92.会計用勘定科目コード from t67_swkhd t67,m92_kanjo m92 "
            shiwakeSql += " WHERE t67.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and t67.会社コード = m92.会社コード and t67.""GLACCOUNT"" = m92.勘定科目名称１"
            shiwakeSql += " ORDER BY "
            shiwakeSql += """TRANSACTIONID"",""KeyID"""

            shiwakeData = _db.selectDB(shiwakeSql, RS, reccnt) 'reccnt:(省略可能)SELECT文の取得レコード件数

            'Dim cdAR As String = getAccountName("accounts-receivable") '売掛金 アキュレート用勘定科目コード
            'Dim cdAP As String = getAccountName("accounts-payable") '買掛金 アキュレート用勘定科目コード
            Dim cdAR As String = "売掛金" '売掛金 アキュレート用勘定科目コード　テスト用
            Dim cdAP As String = "買掛金" '買掛金 アキュレート用勘定科目コード　テスト用

            '取得したデータをXML形式に加工する
            strXml = "<?xml version='1.0'?>"


            Dim checkTransactionid As String = ""
            Dim totalJvamount As Decimal = 0

            For i As Integer = 0 To shiwakeData.Tables(RS).Rows.Count - 1

                Dim valId As String = shiwakeData.Tables(RS).Rows(i)(0).ToString()
                Dim valComCd As String = shiwakeData.Tables(RS).Rows(i)(1).ToString()
                Dim valDate As String = shiwakeData.Tables(RS).Rows(i)(2).ToString()
                Dim valTransactionid As String = shiwakeData.Tables(RS).Rows(i)(3).ToString()
                Dim ci As New System.Globalization.CultureInfo("ja-JP")

                Dim nextTransactionid As String = ""
                If shiwakeData.Tables(RS).Rows.Count - 1 > i Then
                    nextTransactionid = shiwakeData.Tables(RS).Rows(i + 1)(3).ToString() '次のvalTransactionid（判定用）
                End If
                Dim valKeyId As String = shiwakeData.Tables(RS).Rows(i)(4).ToString()
                Dim valGlaccountCD As String = shiwakeData.Tables(RS).Rows(i)(15).ToString()
                Dim valGlaccount As String = shiwakeData.Tables(RS).Rows(i)(5).ToString()
                Dim valGlamount As String = Decimal.Parse(shiwakeData.Tables(RS).Rows(i)(6)).ToString("F2", ci)
                Dim valRate As String = shiwakeData.Tables(RS).Rows(i)(7).ToString()
                Dim valVendorno As String = shiwakeData.Tables(RS).Rows(i)(8).ToString()
                Dim valJvnumber As String = shiwakeData.Tables(RS).Rows(i)(9).ToString() 'PO
                Dim valTransdate As String = shiwakeData.Tables(RS).Rows(i)(10).ToString()
                Dim valTransdescription As String = shiwakeData.Tables(RS).Rows(i)(11).ToString()
                Dim valJvamount As String = Decimal.Parse(shiwakeData.Tables(RS).Rows(i)(12)).ToString("F2", ci)
                Dim valCustomerno As String = shiwakeData.Tables(RS).Rows(i)(13).ToString()
                Dim valDescription As String = shiwakeData.Tables(RS).Rows(i)(14).ToString()

                '初回に必ず入れる
                If i < 1 Then
                    strXml += "<NMEXML EximID='1' BranchCode='" & getRow("会計用コード") & "' ACCOUNTANTCOPYID=''>"
                    strXml += "<TRANSACTIONS OnError='CONTINUE'>"
                End If


                'TRANSACTIONID が同じ場合のみ
                If valTransactionid = checkTransactionid Then

                    'strXml += "<TRANSACTIONID>" & valTransactionid & "</TRANSACTIONID>"
                    strXml += "<ACCOUNTLINE operation='Add'>"
                    strXml += "<KeyID>" & valKeyId & "</KeyID>"
                    strXml += "<GLACCOUNT>" & valGlaccountCD & "</GLACCOUNT>"
                    strXml += "<GLAMOUNT>" & valGlamount & "</GLAMOUNT>"
                    strXml += "<DESCRIPTION>" & valDescription & "</DESCRIPTION>"
                    strXml += "<RATE>" & valRate & "</RATE>"
                    strXml += "<PRIMEAMOUNT></PRIMEAMOUNT>"
                    strXml += "<TXDATE/>"
                    strXml += "<POSTED/>"
                    strXml += "<CURRENCYNAME></CURRENCYNAME>"

                    '売掛金だったら
                    If valGlaccount = cdAR Then
                        strXml += "<CUSTOMERNO>" & valCustomerno & "</CUSTOMERNO>"
                    ElseIf valGlaccount = cdAP Then
                        strXml += "<VENDORNO>" & valVendorno & "</VENDORNO>"
                    End If

                    strXml += "</ACCOUNTLINE>"


                    'TRANSACTIONID が異なっていたら（初回含む）
                Else
                    checkTransactionid = valTransactionid
                    totalJvamount = 0 'リセット

                    strXml += "<JV operation='Add' REQUESTID='1'>"
                    strXml += "<TRANSACTIONID>" & valTransactionid & "</TRANSACTIONID>"
                    strXml += "<ACCOUNTLINE operation='Add'>"
                    strXml += "<KeyID>" & valKeyId & "</KeyID>"
                    strXml += "<GLACCOUNT>" & valGlaccountCD & "</GLACCOUNT>"
                    strXml += "<GLAMOUNT>" & valGlamount & "</GLAMOUNT>"
                    strXml += "<DESCRIPTION>" & valDescription & "</DESCRIPTION>"
                    strXml += "<RATE>" & valRate & "</RATE>"
                    strXml += "<PRIMEAMOUNT></PRIMEAMOUNT>"
                    strXml += "<TXDATE/>"
                    strXml += "<POSTED/>"
                    strXml += "<CURRENCYNAME></CURRENCYNAME>"

                    '売掛金だったら
                    If valGlaccount = cdAR Then
                        strXml += "<CUSTOMERNO>" & valCustomerno & "</CUSTOMERNO>"
                    ElseIf valGlaccount = cdAP Then
                        strXml += "<VENDORNO>" & valVendorno & "</VENDORNO>"
                    End If



                    'strXml += "<VENDORNO>" & valVendorno & "</VENDORNO>"
                    'strXml += "<CUSTOMERNO>" & valCustomerno & "</CUSTOMERNO>"

                    strXml += "</ACCOUNTLINE>"
                End If
                If valJvnumber = "ER-UQ2-0" Then
                    totalJvamount += 0
                End If

                '整数だったら加算していく
                If valGlamount > 0 Then
                    totalJvamount += valGlamount
                End If


                If nextTransactionid <> checkTransactionid Then
                    strXml += "<JVNUMBER>" & valJvnumber & "</JVNUMBER>"
                    strXml += "<TRANSDATE>" & valTransdate & "</TRANSDATE>"
                    strXml += "<SOURCE>GL</SOURCE>"
                    strXml += "<TRANSTYPE>journal voucher</TRANSTYPE>"
                    strXml += "<TRANSDESCRIPTION>" & valTransdescription & "</TRANSDESCRIPTION>"
                    strXml += "<JVAMOUNT>" & totalJvamount & "</JVAMOUNT>"
                    strXml += "</JV>"
                End If


            Next
            'Console.WriteLine("xml: " & strXml)
            strXml += "</TRANSACTIONS>"
            strXml += "</NMEXML>"

            Dim xmlDoc As New System.Xml.XmlDocument

            '文字列からDOMドキュメントを生成
            xmlDoc.LoadXml(strXml)

            Try
                '作成したDOMドキュメントをファイルに保存

                'Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
                Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("UTF-8")
                '出力先パス
                Dim sOutPath As String = ""
                sOutPath = StartUp._iniVal.OutXlsPath


                xmlDoc.Save(sOutPath & "\" & nowDatetime & ".xml")

                Cursor.Current = Cursors.Default
                _msgHd.dspMSG("CreateXML", frmC01F10_Login.loginValue.Language)
            Catch ex As System.Xml.XmlException
                'XMLによる例外をキャッチ
                Cursor.Current = Cursors.Default
                Console.WriteLine(ex.Message)
            End Try

            'Catch ex As Exception
        Catch lex As UsrDefException
            Cursor.Current = Cursors.Default
            lex.dspMsg()
            Exit Sub
        End Try



    End Sub

    '勘定科目コードからアキュレート用勘定科目コード取得（有効データのみ）
    Private Function getAccountName(ByVal codeName As String) As String
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""
        Dim dsData As DataSet

        Sql += "SELECT * FROM public.m92_kanjo"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND 勘定科目コード = '" & codeName & "'"
        Sql += " AND 有効区分 = 0"

        dsData = _db.selectDB(Sql, RS, reccnt)
        Dim strReturn As String
        If reccnt = 0 Then
            strReturn = ""
        Else
            strReturn = dsData.Tables(RS).Rows(0)("会計用勘定科目コード")
        End If
        Return strReturn
    End Function

    '仕入先コードからアキュレート用仕入先コード取得（有効データのみ）
    Private Function getSupplierName(ByVal codeName As String) As String
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""
        Dim dsData As DataSet

        Sql += "SELECT * FROM public.m11_supplier"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND 仕入先コード = '" & codeName & "'"

        dsData = _db.selectDB(Sql, RS, reccnt)
        Dim strReturn As String
        If reccnt = 0 Then
            strReturn = ""
        Else
            strReturn = dsData.Tables(RS).Rows(0)("会計用仕入先コード")
        End If
        Return strReturn
    End Function

    '得意先コードからアキュレート用得意先コード取得（有効データのみ）
    Private Function getCustomerName(ByVal codeName As String) As String
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""
        Dim dsData As DataSet

        Sql += "SELECT * FROM public.m10_customer"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND 得意先コード = '" & codeName & "'"

        dsData = _db.selectDB(Sql, RS, reccnt)
        Dim strReturn As String
        If reccnt = 0 Then
            strReturn = ""
        Else
            strReturn = dsData.Tables(RS).Rows(0)("会計用得意先コード")
        End If
        Return strReturn
    End Function

    'select文を返すfunc(Allのみ）paramでwhere句などを入れる
    Private Function allSelectSql(ByVal tableName As String, Optional ByRef txtParam As String = "") As String
        Dim txtSql As String = ""
        txtSql += "SELECT"
        txtSql += " *"
        txtSql += " FROM "

        txtSql += "public"
        txtSql += "."
        txtSql += tableName
        txtSql += txtParam

        Return txtSql
    End Function


    '仕訳データのXML出力
    Private Sub getShiwakeData()
        Dim dtToday As DateTime = DateTime.Now '年月の設定
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = "" 'SQL文用
        Dim seqID As Integer 'TRANSACTIONID用変数

        Dim dsCompany As DataSet = getDsData("m01_company") 'ログイン情報から会社データの取得


#Region "仕訳買掛金"

        't42_nyukohd 入庫基本  
        't43_nyukodt
        't40_sirehd
        't46_kikehd
        't20_hattyu
        Sql = "SELECT "
        Sql += " t40.仕入番号,t40.ＶＡＴ"
        Sql += ",t40.仕入先名,t40.仕入先コード,t40.客先番号,t40.取消区分"
        Sql += ",t46.買掛日,t20.発注番号,t20.受注番号"

        Sql += " FROM public.t42_nyukohd as t42 "
        Sql += " left join public.t43_nyukodt as t43"
        Sql += " on t42.入庫番号 = t43.入庫番号"

        Sql += " left join public.t40_sirehd as t40"
        Sql += " on t42.発注番号 = t40.発注番号 and t42.発注番号枝番 = t40.発注番号枝番"

        Sql += " left join public.t46_kikehd as t46"
        Sql += " on t42.発注番号 = t46.発注番号 and t42.発注番号枝番 = t46.発注番号枝番"

        Sql += " left join public.t20_hattyu as t20"
        Sql += " on t42.発注番号 = t20.発注番号 and t42.発注番号枝番 = t20.発注番号枝番"

        Sql += " WHERE "
        Sql += " t42.会社コード"
        Sql += " ILIKE  "
        Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"

        '条件オプション
        'Sql += " and t40.買掛日 <= '" & dsCompany.Tables(RS).Rows(0)("今回締日") & "'"

        Sql += " and t43.仕入区分 <> '" & CommonConst.Sire_KBN_Move & "'"  't43_nyukodt 「0:移動」以外
        Sql += " and t42.入庫番号 is not null"
        Sql += " and t40.仕入番号 is not null"
        Sql += " and t46.買掛番号 is not null"                             '入庫と仕入と買掛処理が終了しているデータ
        Sql += " and t46.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED     '買掛が取消の場合は取得しない

        Sql += " group by "
        Sql += " t40.仕入番号,t40.ＶＡＴ"
        Sql += ",t40.仕入先名,t40.仕入先コード,t40.客先番号,t40.取消区分"
        Sql += ",t46.買掛日,t20.発注番号,t20.受注番号"

        Sql += " ORDER BY "
        Sql += " t40.仕入番号,t40.ＶＡＴ"
        Sql += ",t40.取消区分"
        Sql += ",t46.買掛日"

        Dim dsSWKNyukohd As DataSet = _db.selectDB(Sql, RS, reccnt)


        For i As Integer = 0 To dsSWKNyukohd.Tables(RS).Rows.Count - 1  't42_nyukohd

            '入庫データ回しながら以下データ作成
            '借方：棚卸資産
            '貸方：買掛金

            '会計用仕入先コードの取得
            Dim codeAAC As String = getSupplierName(dsSWKNyukohd.Tables(RS).Rows(i)("仕入先コード"))

            'プライマリ
            upSeq()
            seqID = getSeq("transactionid_seq")
            Console.WriteLine(seqID)

            '行カウント
            Dim countKeyID As Integer = 0


            Dim calVat As Decimal = 0
            Dim calCost As Decimal = 0
            Dim Indirectfees As Decimal = 0
            Dim calKanzei As Decimal = 0
            Dim calMaebarai As Decimal = 0
            Dim calYuso As Decimal = 0
            Dim strDESCRIPTION As String = vbNullString


            't41_siredt　仕入原価等の値を取得
            Call mGet_money_t41_siredt(dsSWKNyukohd.Tables(RS).Rows(i)("仕入番号"), dsSWKNyukohd.Tables(RS).Rows(i)("ＶＡＴ") _
                                       , calVat, calCost, Indirectfees _
                                       , calKanzei, calMaebarai, calYuso)


            'DESCRIPTIONの生成 受注番号 発注番号格納
            strDESCRIPTION = mGet_DESCRIPTION(dsSWKNyukohd.Tables(RS).Rows(i)("受注番号") _
                                             , dsSWKNyukohd.Tables(RS).Rows(i)("発注番号") _
                                             , 0)

            If dsSWKNyukohd.Tables(RS).Rows(i)("取消区分") = CommonConst.CANCEL_KBN_ENABLED Then  't40_sirehd 取消区分

                '入庫入力に伴う仕入伝票計上
                '※入庫Ｈ，明細にレコード登録時、
                'かつ入庫dt「仕入区分0:移動」以外かつ「取消区分0:有効」


#Region "買掛計上"
                '借方　棚卸資産  整数
                Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("買掛日"), "yyyyMM") & "'"
                Sql += "," & seqID       'プライマリ
                Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'棚卸資産'"     '借方勘定  
                Sql += "," & UtilClass.formatNumber(formatDouble(calCost)) '仕入原価
                Sql += ",1" '固定
                Sql += ",'" & getSupplierName(dsSWKNyukohd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'"  '補助科目
                Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("買掛日"), "yyyy-MM-dd") & "'"   '買掛日
                Sql += ",''" '空でよし
                Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat + Indirectfees)) & "'" '仕入原価 + VAT + 間接費
                Sql += ",'" & codeAAC & "'"         '会計用仕入先コード
                Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                '貸方　買掛金  負数
                countKeyID = getCount(countKeyID)

                Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("買掛日"), "yyyyMM") & "'"
                Sql += "," & seqID       'プライマリ
                Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'買掛金'"       '貸方勘定　
                Sql += "," & UtilClass.formatNumber(formatDouble(-calCost)) '仕入原価
                Sql += ",1" '固定
                Sql += ",'" & getSupplierName(dsSWKNyukohd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'"  '補助科目
                Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'"  'PO
                Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("買掛日"), "yyyy-MM-dd") & "'"    '買掛日
                Sql += ",''" '空でよし
                Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat + Indirectfees)) & "'"
                Sql += ",'" & codeAAC & "'"         '会計用仕入先コード
                Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                'VATがある場合
                If calVat <> 0 Then

                    '借方　VAT-IN
                    countKeyID = getCount(countKeyID)

                    Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("買掛日"), "yyyyMM") & "'"
                    Sql += "," & seqID       'プライマリ
                    Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（iから）
                    Sql += ",'VAT-IN'"       '借方勘定  
                    Sql += "," & UtilClass.formatNumber(formatDouble(calVat))  'VAT
                    Sql += ",1" '固定
                    Sql += ",'" & getSupplierName(dsSWKNyukohd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                    Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("買掛日"), "yyyy-MM-dd") & "'"   '買掛日
                    Sql += ",''" '空でよし
                    Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat + Indirectfees)) & "'"
                    Sql += ",'" & codeAAC & "'"         '会計用仕入先コード
                    Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                    't67_swkhd データ登録
                    updateT67Swkhd(Sql)


                    '貸方　買掛金
                    countKeyID = getCount(countKeyID)

                    Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("買掛日"), "yyyyMM") & "'"
                    Sql += "," & seqID       'プライマリ
                    Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（iから）
                    Sql += ",'買掛金'"       '貸方勘定
                    Sql += "," & UtilClass.formatNumber(formatDouble(-calVat))  'VAT
                    Sql += ",1" '固定
                    Sql += ",'" & getSupplierName(dsSWKNyukohd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                    Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("買掛日"), "yyyy-MM-dd") & "'"   '買掛日
                    Sql += ",''" '空でよし
                    Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat + Indirectfees)) & "'"
                    Sql += ",'" & codeAAC & "'"         '会計用仕入先コード
                    Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                    't67_swkhd データ登録
                    updateT67Swkhd(Sql)

                End If


                '間接費がある場合
                If Indirectfees > 0 Then

                    Dim decTemp(3) As Decimal
                    decTemp(0) = calKanzei
                    decTemp(1) = calMaebarai
                    decTemp(2) = calYuso

                    '間接費の内訳_関税額、前払法人税、輸送額分ループ
                    For j As Integer = 0 To 2

                        If decTemp(j) > 0 Then

                            'DESCRIPTIONの生成 受注番号 発注番号格納
                            strDESCRIPTION = mGet_DESCRIPTION(dsSWKNyukohd.Tables(RS).Rows(i)("受注番号") _
                                             , dsSWKNyukohd.Tables(RS).Rows(i)("発注番号") _
                                             , j + 1)

                            '借方　棚卸資産
                            countKeyID = getCount(countKeyID)

                            Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("買掛日"), "yyyyMM") & "'"
                            Sql += "," & seqID       'プライマリ
                            Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（0から）
                            Sql += ",'棚卸資産'"     '借方勘定  
                            Sql += "," & UtilClass.formatNumber(formatDouble(decTemp(j)))  '間接費(関税額 or 前払法人税 or 輸送額)
                            Sql += ",1" '固定
                            Sql += ",'" & getSupplierName(dsSWKNyukohd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                            Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                            Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("買掛日"), "yyyy-MM-dd") & "'"   '買掛日
                            Sql += ",''" '空でよし
                            Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat + Indirectfees)) & "'"
                            Sql += ",'" & codeAAC & "'"         '会計用仕入先コード
                            Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                            't67_swkhd データ登録
                            updateT67Swkhd(Sql)


                            '貸方　買掛金
                            countKeyID = getCount(countKeyID)

                            Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("買掛日"), "yyyyMM") & "'"
                            Sql += "," & seqID       'プライマリ
                            Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（0から）
                            Sql += ",'買掛金'"       '貸方勘定　
                            Sql += "," & UtilClass.formatNumber(formatDouble(-decTemp(j)))
                            Sql += ",1" '固定
                            Sql += ",'" & getSupplierName(dsSWKNyukohd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                            Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                            Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("買掛日"), "yyyy-MM-dd") & "'"   '買掛日
                            Sql += ",''" '空でよし
                            Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat + Indirectfees)) & "'"
                            Sql += ",'" & codeAAC & "'"         '会計用仕入先コード
                            Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                            't67_swkhd データ登録
                            updateT67Swkhd(Sql)

                        End If

                    Next j

                End If

#End Region

            ElseIf dsSWKNyukohd.Tables(RS).Rows(i)("取消区分") = CommonConst.CANCEL_KBN_DISABLED Then
                '入庫取消に伴う仕入返品計上
                '※入庫Ｈ，明細にレコード登録時、
                'かつ入庫dt「仕入区分0:移動」以外かつ「取消区分1:取消」

#Region "買掛取消"

                '借方　買掛金
                Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("買掛日"), "yyyyMM") & "'"
                Sql += "," & seqID 　　　'プライマリ
                Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'買掛金'"       '借方勘定  
                Sql += "," & UtilClass.formatNumber(formatDouble(calCost)) '仕入原価
                Sql += ",1" '固定
                Sql += ",'" & getSupplierName(dsSWKNyukohd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("買掛日"), "yyyy-MM-dd") & "'"   '買掛日
                Sql += ",''" '空でよし
                Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat + Indirectfees)) & "'"
                Sql += ",'" & codeAAC & "'"         '会計用仕入先コード
                Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                '貸方　棚卸資産
                countKeyID = getCount(countKeyID)

                Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("買掛日"), "yyyyMM") & "'"
                Sql += "," & seqID       'プライマリ
                Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'棚卸資産'"     '貸方勘定　
                Sql += "," & UtilClass.formatNumber(formatDouble(-calCost)) '仕入原価
                Sql += ",1" '固定
                Sql += ",'" & getSupplierName(dsSWKNyukohd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("買掛日"), "yyyy-MM-dd") & "'"   '買掛日
                Sql += ",''" '空でよし
                Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat + Indirectfees)) & "'"
                Sql += ",'" & codeAAC & "'"         '会計用仕入先コード
                Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                'VATがある場合
                If calVat <> 0 Then

                    '借方　買掛金
                    countKeyID = getCount(countKeyID)

                    Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("買掛日"), "yyyyMM") & "'"
                    Sql += "," & seqID       'プライマリ
                    Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'買掛金'"       '借方勘定
                    Sql += "," & UtilClass.formatNumber(formatDouble(calVat)) 'VAT
                    Sql += ",1" '固定
                    Sql += ",'" & getSupplierName(dsSWKNyukohd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                    Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("買掛日"), "yyyy-MM-dd") & "'"   '買掛日
                    Sql += ",''" '空でよし
                    Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat + Indirectfees)) & "'"
                    Sql += ",'" & codeAAC & "'"         '会計用仕入先コード
                    Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                    't67_swkhd データ登録
                    updateT67Swkhd(Sql)


                    '貸方　VAT-IN
                    countKeyID = getCount(countKeyID)

                    Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("買掛日"), "yyyyMM") & "'"
                    Sql += "," & seqID       'プライマリ
                    Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'VAT-IN'"       '貸方勘定
                    Sql += "," & UtilClass.formatNumber(formatDouble(-calVat)) 'VAT
                    Sql += ",1" '固定
                    Sql += ",'" & getSupplierName(dsSWKNyukohd.Tables(RS).Rows(0)("仕入先コード").ToString) & "'"      '補助科目
                    Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("買掛日"), "yyyy-MM-dd") & "'" '買掛日
                    Sql += ",''" '空でよし
                    Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat + Indirectfees)) & "'"
                    Sql += ",'" & codeAAC & "'"         '会計用仕入先コード
                    Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                    't67_swkhd データ登録
                    updateT67Swkhd(Sql)

                End If

                '間接費がある場合
                If Indirectfees > 0 Then

                    Dim decTemp(3) As Decimal
                    decTemp(0) = calKanzei
                    decTemp(1) = calMaebarai
                    decTemp(2) = calYuso

                    '間接費の内訳_関税額、前払法人税、輸送額分ループ
                    For j As Integer = 0 To 2

                        If decTemp(j) > 0 Then

                            'DESCRIPTIONの生成 受注番号 発注番号格納
                            strDESCRIPTION = mGet_DESCRIPTION(dsSWKNyukohd.Tables(RS).Rows(i)("受注番号") _
                                             , dsSWKNyukohd.Tables(RS).Rows(i)("発注番号") _
                                             , j + 1)

                            '借方　買掛金
                            countKeyID = getCount(countKeyID)

                            Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("買掛日"), "yyyyMM") & "'"
                            Sql += "," & seqID       'プライマリ
                            Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（0から）
                            Sql += ",'買掛金'"     '借方勘定  
                            Sql += "," & UtilClass.formatNumber(formatDouble(decTemp(j)))  '間接費(関税額 or 前払法人税 or 輸送額)
                            Sql += ",1" '固定
                            Sql += ",'" & getSupplierName(dsSWKNyukohd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                            Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                            Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("買掛日"), "yyyy-MM-dd") & "'"   '買掛日
                            Sql += ",''" '空でよし
                            Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat + Indirectfees)) & "'"
                            Sql += ",'" & codeAAC & "'"         '会計用仕入先コード
                            Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                            't67_swkhd データ登録
                            updateT67Swkhd(Sql)


                            '貸方　棚卸資産
                            countKeyID = getCount(countKeyID)

                            Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("買掛日"), "yyyyMM") & "'"
                            Sql += "," & seqID       'プライマリ
                            Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（0から）
                            Sql += ",'棚卸資産'"       '貸方勘定　
                            Sql += "," & UtilClass.formatNumber(formatDouble(-decTemp(j)))
                            Sql += ",1" '固定
                            Sql += ",'" & getSupplierName(dsSWKNyukohd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                            Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                            Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("買掛日"), "yyyy-MM-dd") & "'"   '買掛日
                            Sql += ",''" '空でよし
                            Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat + Indirectfees)) & "'"
                            Sql += ",'" & codeAAC & "'"         '会計用仕入先コード
                            Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                            't67_swkhd データ登録
                            updateT67Swkhd(Sql)

                        End If

                    Next j

                End If

#End Region
            Else
                'todo:msgbox テスト
                MsgBox("エラー")
            End If
        Next

        dsSWKNyukohd.Dispose()

#End Region


#Region "仕訳売掛金"

        't44_shukohd  出庫基本
        't45_shukodt 
        't30_urighd
        't23_skyuhd
        't02_mitdt
        Sql = "SELECT "
        Sql += " t44.客先番号,t44.受注番号"
        Sql += ",t30.売上番号,t30.ＶＡＴ"
        Sql += ",t30.得意先コード,t30.得意先名,t30.ＶＡＴ,t30.取消区分"
        Sql += ",t23.請求日"

        'Sql += ",t45.仕入先名"
        'Sql += ",t02.仕入先コード"

        Sql += " FROM public.t44_shukohd as t44 "

        Sql += " left join public.t45_shukodt as t45"
        Sql += " on t44.出庫番号 = t45.出庫番号 "

        Sql += " left join public.t30_urighd as t30"
        Sql += " on (t44.受注番号 = t30.受注番号 and t44.受注番号枝番 = t30.受注番号枝番)"

        Sql += " left join public.t23_skyuhd as t23"
        Sql += " on (t44.受注番号 = t23.受注番号 and t44.受注番号枝番 = t23.受注番号枝番)"

        'Sql += " left join public.t02_mitdt as t02"
        'Sql += " on (t44.見積番号 = t02.見積番号 and t44.見積番号枝番 = t02.見積番号枝番)"

        Sql += " WHERE "
        Sql += " t44.会社コード"
        Sql += " ILIKE  "
        Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"

        '条件オプション
        'Sql += " and t30.請求日 <= '" & dsCompany.Tables(RS).Rows(0)("今回締日") & "'"

        Sql += " and t45.仕入区分 <> '" & CommonConst.Sire_KBN_Move & "'"  't45_shukodt 「0:移動」以外
        Sql += " and t44.出庫番号 is not null"
        Sql += " and t30.売上番号 is not null"
        Sql += " and t23.請求番号 is not null"                             '出庫と売上と請求処理が終了しているデータ
        'Sql += " and t46.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED     '買掛が取消の場合は取得しない


        Sql += " GROUP BY "
        Sql += " t44.客先番号,t44.受注番号"
        Sql += ",t30.売上番号,t30.ＶＡＴ"
        Sql += ",t30.得意先コード,t30.得意先名,t30.ＶＡＴ,t30.取消区分"
        Sql += ",t23.請求日"

        'Sql += ",t45.仕入先名"
        'Sql += ",t02.仕入先コード"


        Sql += " ORDER BY "
        Sql += " t30.売上番号,t30.ＶＡＴ"
        Sql += ",t30.得意先コード"
        'Sql += ",t02.仕入先コード"
        Sql += ",t30.取消区分,t23.請求日"

        Dim dsSwkUrighd As DataSet = _db.selectDB(Sql, RS, reccnt)


        For i As Integer = 0 To dsSwkUrighd.Tables(RS).Rows.Count - 1  't44_shukohd

            '会計用仕入先コードの取得
            'Dim codeAAC As String = getSupplierName(dsSwkUrighd.Tables(RS).Rows(i)("仕入先コード"))

            '会計用得意先コードの取得
            Dim codeAAB As String = getCustomerName(dsSwkUrighd.Tables(RS).Rows(i)("得意先コード"))


            'プライマリ
            upSeq()
            seqID = getSeq("transactionid_seq")

            '行カウント
            Dim countKeyID As Integer = 0

            Dim calSales As Decimal = 0
            Dim calVat As Decimal = 0
            Dim calCost As Decimal = 0

            Dim Indirectfees As Decimal = 0
            Dim calKanzei As Decimal = 0
            Dim calMaebarai As Decimal = 0
            Dim calYuso As Decimal = 0
            Dim strDESCRIPTION As String = vbNullString


            't31_urigdt　売上金額等の値を取得
            Call mGet_money_t31_urigdt(dsSwkUrighd.Tables(RS).Rows(i)("売上番号"), dsSwkUrighd.Tables(RS).Rows(i)("ＶＡＴ") _
                                       , calSales, calVat, calCost, Indirectfees _
                                       , calKanzei, calMaebarai, calYuso)


            'DESCRIPTIONの生成 受注番号 発注番号格納
            strDESCRIPTION = mGet_DESCRIPTION(dsSwkUrighd.Tables(RS).Rows(i)("受注番号") _
                                             , DBNull.Value _
                                             , 0)

            If dsSwkUrighd.Tables(RS).Rows(i)("取消区分") = CommonConst.CANCEL_KBN_ENABLED Then  't30_urighd 取消区分

                '出庫入力に伴う売上伝票計上
                '※出庫Ｈ、明細にレコード登録時、
                'かつ仕入区分「0:移動」以外かつ取消区分「0:有効」
                '「1:受発注」「2:在庫引当」「3:サービス（役務）」

                'サービス（役務）販売による売上伝票計上
                '※出庫入力に伴う売上伝票計上に準ずる

#Region "売上計上,サービス販売"


#Region "仕入"
                '借方　仕入
                Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyyMM") & "'"
                Sql += "," & seqID      'todo:プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'仕入'"        '借方勘定
                Sql += "," & UtilClass.formatNumber(formatDouble(calCost)) '仕入原価
                Sql += ",1" '固定
                'Sql += ",'" & getSupplierName(dsSwkUrighd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                Sql += ",''" '補助科目
                Sql += ",'ER-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyy-MM-dd") & "'" '請求日
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(formatDouble(calCost + Indirectfees)) '仕入原価 + 間接費
                'Sql += ",'" & codeAAC & "'" '会計用仕入先コード
                Sql += ",''" '会計用仕入先コード
                Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                '貸方  棚卸資産
                countKeyID = getCount(countKeyID)

                Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyyMM") & "'"
                Sql += "," & seqID 　   'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'棚卸資産'"    '借方科目
                Sql += "," & UtilClass.formatNumber(formatDouble(-(calCost))) '仕入原価
                Sql += ",1" '固定
                'Sql += ",'" & getSupplierName(dsSwkUrighd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                Sql += ",''" '補助科目
                Sql += ",'ER-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyy-MM-dd") & "'" '請求日
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(formatDouble(calCost + Indirectfees)) '仕入原価 + 間接費
                'Sql += ",'" & codeAAC & "'" '会計用仕入先コード
                Sql += ",''" '会計用仕入先コード
                Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                '間接費がある場合
                If Indirectfees > 0 Then

                    Dim decTemp(3) As Decimal
                    decTemp(0) = calKanzei
                    decTemp(1) = calMaebarai
                    decTemp(2) = calYuso

                    '間接費の内訳_関税額、前払法人税、輸送額分ループ
                    For j As Integer = 0 To 2

                        If decTemp(j) > 0 Then

                            'DESCRIPTIONの生成 受注番号 発注番号格納
                            strDESCRIPTION = mGet_DESCRIPTION(dsSwkUrighd.Tables(RS).Rows(i)("受注番号") _
                                             , DBNull.Value _
                                             , j + 1)

                            '借方　仕入
                            countKeyID = getCount(countKeyID)

                            Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyyMM") & "'"
                            Sql += "," & seqID       'プライマリ
                            Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（0から）
                            Sql += ",'仕入'"         '借方勘定  
                            Sql += "," & UtilClass.formatNumber(formatDouble(decTemp(j)))  '間接費(関税額 or 前払法人税 or 輸送額)
                            Sql += ",1" '固定
                            'Sql += ",'" & getSupplierName(dsSwkUrighd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                            Sql += ",''" '補助科目
                            Sql += ",'WH-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                            Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyy-MM-dd") & "'"   '請求日
                            Sql += ",''" '空でよし
                            Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + Indirectfees)) & "'"
                            'Sql += ",'" & codeAAC & "'" '会計用仕入先コード
                            Sql += ",''" '会計用仕入先コード
                            Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                            't67_swkhd データ登録
                            updateT67Swkhd(Sql)


                            '貸方　棚卸資産
                            countKeyID = getCount(countKeyID)

                            Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyyMM") & "'"
                            Sql += "," & seqID       'プライマリ
                            Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（0から）
                            Sql += ",'棚卸資産'"     '貸方勘定　
                            Sql += "," & UtilClass.formatNumber(formatDouble(-decTemp(j)))
                            Sql += ",1" '固定
                            'Sql += ",'" & getSupplierName(dsSwkUrighd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                            Sql += ",''" '補助科目
                            Sql += ",'WH-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                            Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyy-MM-dd") & "'"   '請求日
                            Sql += ",''" '空でよし
                            Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + Indirectfees)) & "'"
                            'Sql += ",'" & codeAAC & "'" '会計用仕入先コード
                            Sql += ",''" '会計用仕入先コード
                            Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                            't67_swkhd データ登録
                            updateT67Swkhd(Sql)

                        End If

                    Next j

                End If
#End Region

#Region "売上"

                'DESCRIPTIONの生成 受注番号 発注番号格納
                strDESCRIPTION = mGet_DESCRIPTION(dsSwkUrighd.Tables(RS).Rows(i)("受注番号") _
                                             , DBNull.Value _
                                             , 0)

                '借方　売掛金
                countKeyID = getCount(countKeyID)

                Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyyMM") & "'"
                Sql += "," & seqID      'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'売掛金'"      '借方勘定
                Sql += "," & UtilClass.formatNumber(formatDouble(calSales + Indirectfees)) '売上金額 + 間接費
                Sql += ",1" '固定
                Sql += ",'" & getCustomerName(dsSwkUrighd.Tables(RS).Rows(i)("得意先コード").ToString) & "'" '補助科目
                Sql += ",'ER-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyy-MM-dd") & "'" '請求日
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(formatDouble(calSales + calVat + Indirectfees)) '売上金額 + VAT IN + 間接費
                Sql += ",'" & codeAAB & "'" '会計用得意先コード
                Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                '貸方  売上
                countKeyID = getCount(countKeyID)

                Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyyMM") & "'"
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'売上'" '貸方科目　
                Sql += "," & UtilClass.formatNumber(formatDouble(-(calSales + Indirectfees))) '売上金額 + 間接費
                Sql += ",1" '固定
                Sql += ",'" & getCustomerName(dsSwkUrighd.Tables(RS).Rows(i)("得意先コード").ToString) & "'" '補助科目
                Sql += ",'ER-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyy-MM-dd") & "'" '請求日
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(formatDouble(calSales + calVat + Indirectfees))
                Sql += ",'" & codeAAB & "'" '会計用得意先コード
                Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                'VATがある場合
                If calVat <> 0 Then

                    '借方  売掛金
                    countKeyID = getCount(countKeyID)

                    Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyyMM") & "'"
                    Sql += "," & seqID      'プライマリ
                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'売掛金'"      '借方勘定　
                    Sql += "," & UtilClass.formatNumber(formatDouble(calVat)) 'VAT
                    Sql += ",1" '固定
                    Sql += ",'" & getCustomerName(dsSwkUrighd.Tables(RS).Rows(i)("得意先コード").ToString) & "'" '補助科目
                    Sql += ",'ER-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyy-MM-dd") & "'" '請求日
                    Sql += ",''" '空でよし
                    Sql += "," & UtilClass.formatNumber(formatDouble(calSales + calVat + Indirectfees))
                    Sql += ",'" & codeAAB & "'" '会計用得意先コード
                    Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                    't67_swkhd データ登録
                    updateT67Swkhd(Sql)


                    '貸方  VAT
                    countKeyID = getCount(countKeyID)

                    Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyyMM") & "'"
                    Sql += "," & seqID      'プライマリ
                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'VAT-OUT'"     '借方科目　
                    Sql += "," & UtilClass.formatNumber(formatDouble(-(calVat))) 'VAT
                    Sql += ",1" '固定
                    Sql += ",'" & getCustomerName(dsSwkUrighd.Tables(RS).Rows(i)("得意先コード").ToString) & "'" '補助科目
                    Sql += ",'ER-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyy-MM-dd") & "'" '請求日
                    Sql += ",''" '空でよし
                    Sql += "," & UtilClass.formatNumber(formatDouble(calSales + calVat + Indirectfees))
                    Sql += ",'" & codeAAB & "'" '会計用得意先コード
                    Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                    't67_swkhd データ登録
                    updateT67Swkhd(Sql)
                End If
#End Region
#End Region

            ElseIf dsSwkUrighd.Tables(RS).Rows(i)("取消区分") = CommonConst.CANCEL_KBN_DISABLED Then
                '出庫取消に伴う売上返品計上
                '※出庫Ｈ，明細にレコード登録時、
                'かつ「仕入区分0:移動」以外かつ「取消区分1:取消」

#Region "売上計上取消"


#Region "仕入取消"

                '借方　棚卸資産
                seqID = getSeq("transactionid_seq")

                Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyyMM") & "'"
                Sql += "," & seqID      'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'棚卸資産'"    '借方勘定　
                Sql += "," & UtilClass.formatNumber(formatDouble(calCost)) '仕入原価
                Sql += ",1" '固定
                'Sql += ",'" & getSupplierName(dsSwkUrighd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                Sql += ",''" '補助科目
                Sql += ",'ER-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyy-MM-dd") & "'" '請求日
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(formatDouble(calCost + Indirectfees)) '仕入金額
                'Sql += ",'" & codeAAC & "'" '会計用仕入先コード
                Sql += ",''" '会計用仕入先コード
                Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION


                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                '貸方  仕入
                countKeyID = getCount(countKeyID)

                Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyyMM") & "'"
                Sql += "," & seqID      'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'仕入'"        '貸方勘定
                Sql += "," & UtilClass.formatNumber(formatDouble(-(calCost)))
                Sql += ",1" '固定
                'Sql += ",'" & getSupplierName(dsSwkUrighd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                Sql += ",''" '補助科目
                Sql += ",'ER-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyy-MM-dd") & "'" '請求日
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(formatDouble(calCost + Indirectfees))
                'Sql += ",'" & codeAAC & "'" '会計用仕入先コード
                Sql += ",''" '会計用仕入先コード
                Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                '間接費がある場合
                If Indirectfees > 0 Then

                    Dim decTemp(3) As Decimal
                    decTemp(0) = calKanzei
                    decTemp(1) = calMaebarai
                    decTemp(2) = calYuso

                    '間接費の内訳_関税額、前払法人税、輸送額分ループ
                    For j As Integer = 0 To 2

                        If decTemp(j) > 0 Then

                            'DESCRIPTIONの生成 受注番号 発注番号格納
                            strDESCRIPTION = mGet_DESCRIPTION(dsSwkUrighd.Tables(RS).Rows(i)("受注番号") _
                                             , DBNull.Value _
                                             , j + 1)

                            '借方　棚卸資産
                            countKeyID = getCount(countKeyID)

                            Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyyMM") & "'"
                            Sql += "," & seqID       'プライマリ
                            Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（0から）
                            Sql += ",'棚卸資産'"     '借方勘定  
                            Sql += "," & UtilClass.formatNumber(formatDouble(decTemp(j)))  '間接費(関税額 or 前払法人税 or 輸送額)
                            Sql += ",1" '固定
                            'Sql += ",'" & getSupplierName(dsSwkUrighd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                            Sql += ",''" '補助科目
                            Sql += ",'WH-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                            Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyy-MM-dd") & "'"   '請求日
                            Sql += ",''" '空でよし
                            Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + Indirectfees)) & "'"
                            'Sql += ",'" & codeAAC & "'" '会計用仕入先コード
                            Sql += ",''" '会計用仕入先コード
                            Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                            't67_swkhd データ登録
                            updateT67Swkhd(Sql)


                            '貸方　仕入
                            countKeyID = getCount(countKeyID)

                            Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyyMM") & "'"
                            Sql += "," & seqID       'プライマリ
                            Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（0から）
                            Sql += ",'仕入'"         '貸方勘定　
                            Sql += "," & UtilClass.formatNumber(formatDouble(-decTemp(j)))
                            Sql += ",1" '固定
                            'Sql += ",'" & getSupplierName(dsSwkUrighd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                            Sql += ",''" '補助科目
                            Sql += ",'WH-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                            Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyy-MM-dd") & "'"   '請求日
                            Sql += ",''" '空でよし
                            Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + Indirectfees)) & "'"
                            'Sql += ",'" & codeAAC & "'" '会計用仕入先コード
                            Sql += ",''" '会計用仕入先コード
                            Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                            't67_swkhd データ登録
                            updateT67Swkhd(Sql)

                        End If

                    Next j

                End If

#End Region


#Region "売上取消"

                'DESCRIPTIONの生成 受注番号 発注番号格納
                strDESCRIPTION = mGet_DESCRIPTION(dsSwkUrighd.Tables(RS).Rows(i)("受注番号") _
                                             , DBNull.Value _
                                             , 0)

                '借方　売上
                countKeyID = getCount(countKeyID)

                Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyyMM") & "'"
                Sql += "," & seqID      'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'売上戻高'"    '借方勘定  
                Sql += "," & UtilClass.formatNumber(formatDouble(calSales + Indirectfees)) '売上金額 + 間接費
                Sql += ",1" '固定
                Sql += ",'" & getCustomerName(dsSwkUrighd.Tables(RS).Rows(i)("得意先コード").ToString) & "'"  '補助科目
                Sql += ",'WH-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyy-MM-dd") & "'"
                Sql += ",''" '空でよし
                Sql += ",'" & UtilClass.formatNumber(formatDouble(calSales + calVat + Indirectfees)) & "'" '売上金額 + VAT IN + 間接費
                Sql += ",'" & codeAAB & "'" '会計用得意先コード
                Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                '貸方　売掛金
                countKeyID = getCount(countKeyID)

                Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyyMM") & "'"
                Sql += "," & seqID      'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'売掛金'"      '貸方勘定　
                Sql += "," & UtilClass.formatNumber(formatDouble(-(calSales + Indirectfees)))
                Sql += ",1" '固定
                Sql += ",'" & getCustomerName(dsSwkUrighd.Tables(RS).Rows(i)("得意先コード").ToString) & "'"  '勘定科目
                Sql += ",'WH-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyy-MM-dd") & "'" '請求日
                Sql += ",''" '空でよし
                Sql += ",'" & UtilClass.formatNumber(formatDouble(calSales + calVat + Indirectfees)) & "'"
                Sql += ",'" & codeAAB & "'" '会計用得意先コード
                Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                'VATがある場合
                If calVat <> 0 Then

                    '借方　VAT-OUT
                    countKeyID = getCount(countKeyID)

                    Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyyMM") & "'"
                    Sql += "," & seqID      'プライマリ
                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'VAT-OUT'"     '借方勘定
                    Sql += "," & UtilClass.formatNumber(formatDouble(calVat)) 'VAT
                    Sql += ",1" '固定
                    Sql += ",'" & getCustomerName(dsSwkUrighd.Tables(RS).Rows(i)("得意先コード").ToString) & "'"  '補助科目
                    Sql += ",'WH-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyy-MM-dd") & "'" '請求日
                    Sql += ",''" '空でよし
                    Sql += ",'" & UtilClass.formatNumber(formatDouble(calSales + calVat + Indirectfees)) & "'"
                    Sql += ",'" & codeAAB & "'"         '会計用得意先コード
                    Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                    't67_swkhd データ登録
                    updateT67Swkhd(Sql)


                    '貸方　売掛金
                    countKeyID = getCount(countKeyID)

                    Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyyMM") & "'"
                    Sql += "," & seqID      'プライマリ
                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'売掛金'"      '貸方勘定
                    Sql += "," & UtilClass.formatNumber(formatDouble(-calVat)) 'VAT
                    Sql += ",1" '固定
                    Sql += ",'" & getCustomerName(dsSwkUrighd.Tables(RS).Rows(i)("得意先コード").ToString) & "'"  '補助科目
                    Sql += ",'WH-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("請求日"), "yyyy-MM-dd") & "'"
                    Sql += ",''" '空でよし
                    Sql += ",'" & UtilClass.formatNumber(formatDouble(calSales + calVat + Indirectfees)) & "'"
                    Sql += ",'" & codeAAB & "'"         '会計用得意先コード
                    Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                    't67_swkhd データ登録
                    updateT67Swkhd(Sql)

                End If

#End Region
#End Region

            Else
                'todo:msgbox
                MsgBox("エラー")

            End If
        Next

        dsSwkUrighd.Dispose()
#End Region


#Region "仕訳前受金"

        Dim strJyutyuNo As String = vbNullString
        Dim FormerGold As Decimal = 0  '前受金

        't80
        Sql = "SELECT "
        Sql += " t80.請求番号,t80.請求区分,t80.受注番号,t80.入金番号,t80.行番号,t80.入金日,t80.入金種目,t80.入金種目名"
        Sql += ",t80.得意先コード,t80.客先番号,t80.入金額"

        Sql += " FROM public.t80_shiwakenyu as t80 "

        Sql += " WHERE "
        Sql += " t80.会社コード"
        Sql += " ILIKE  "
        Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"

        '条件オプション
        'Sql += " and t26.入金日 <= '" & dsCompany.Tables(RS).Rows(0)("今回締日") & "'"

        'Sql += " GROUP BY "
        'Sql += " t80.請求番号,t80.請求区分,t80.受注番号,t80.入金番号,t80.入金日,t80.入金種目,t80.入金種目名"
        'Sql += ",t80.得意先コード,t80.客先番号"

        Sql += " ORDER BY "
        Sql += " t80.請求番号,t80.請求区分,t80.受注番号,t80.入金番号,t80.行番号,t80.入金日,t80.入金種目"
        Sql += ",t80.得意先コード"

        Dim dsNkinkshihd As DataSet = _db.selectDB(Sql, RS, reccnt)


        't80
        For i As Integer = 0 To dsNkinkshihd.Tables(RS).Rows.Count - 1


            '会計用得意先コードの取得
            Dim codeAAC As String = getCustomerName(dsNkinkshihd.Tables(RS).Rows(i)("得意先コード"))


            'プライマリ
            upSeq()
            seqID = getSeq("transactionid_seq")

            '行カウント
            Dim countKeyID As Integer = 0


            Dim calDeposit As Decimal = dsNkinkshihd.Tables(RS).Rows(i)("入金額")  '入金
            Dim Paymentfee As Decimal = 0  '支払手数料
            Dim strDESCRIPTION As String = vbNullString

            't80　入金金額等の値を取得
            Call mGet_money_t80(dsNkinkshihd.Tables(RS).Rows(i)("入金番号"), dsNkinkshihd.Tables(RS).Rows(i)("行番号") _
                                       , Paymentfee)


            'DESCRIPTIONの生成 受注番号 発注番号格納
            strDESCRIPTION = mGet_DESCRIPTION(dsNkinkshihd.Tables(RS).Rows(i)("請求番号") _
                                             , dsNkinkshihd.Tables(RS).Rows(i)("入金番号") _
                                             , 0)

            '入金種別に対応した科目を取得
            'Dim strKamoku As String = mGet_NyukinSyubetu(dsNkinkshihd.Tables(RS).Rows(i)("入金種別"))
            Dim strKamoku = dsNkinkshihd.Tables(RS).Rows(i)("入金種目名")

            If dsNkinkshihd.Tables(RS).Rows(i)("入金種目") = "9" Then  '相殺

#Region "相殺"

                '借方　買掛金
                Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'買掛金'" '借方科目
                Sql += "," & UtilClass.formatNumber(formatDouble(calDeposit)) '入金金額
                Sql += ",1" '固定
                'Sql += ",'" & getSupplierName(dsNkinkshihd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                Sql += ",''" '補助科目
                Sql += ",'PM-" & dsNkinkshihd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(formatDouble(calDeposit)) '入金金額
                Sql += ",''" '会計用得意先コード
                Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                '貸方　売掛金
                countKeyID = getCount(countKeyID)

                Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'売掛金'" '貸方科目
                Sql += "," & UtilClass.formatNumber(formatDouble(-calDeposit)) '入金金額
                Sql += ",1" '固定
                Sql += ",'" & getCustomerName(dsNkinkshihd.Tables(RS).Rows(i)("得意先コード").ToString) & "'" '補助科目
                Sql += ",'PM-" & dsNkinkshihd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(formatDouble(calDeposit)) '入金金額
                Sql += ",'" & codeAAC & "'" '会計用得意先コード
                Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                't67_swkhd データ登録
                updateT67Swkhd(Sql)
#End Region

            ElseIf dsNkinkshihd.Tables(RS).Rows(i)("請求区分") = "1" Then  '前受請求の場合

                '前受金のみ入金（現金入金）		　　　　　　　  　　現金				前受金	得意先
                '※入金ヘッダ,明細にレコード登録時、							
                'かつ入金番号に一致する請求Rが存在し、							
                'かつ請求R.請求区分が「1:前受金」の場合、							
                '「入金明細入金区分」に応じて伝票を発生させる
                'ここでは「区分3:現金」を例としている
                '    受注F-(受注番号) - 請求F - (入金番号) - 入金F            

#Region "前受請求"

                '借方　入金種別
                Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'" & strKamoku & "'" '貸方科目
                Sql += "," & UtilClass.formatNumber(formatDouble(calDeposit)) '入金金額
                Sql += ",1" '固定
                Sql += ",'" & getCustomerName(dsNkinkshihd.Tables(RS).Rows(i)("得意先コード").ToString) & "'" '補助科目
                Sql += ",'PM-" & dsNkinkshihd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(formatDouble(calDeposit + Paymentfee)) '入金金額
                Sql += ",'" & codeAAC & "'" '会計用得意先コード
                Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                '貸方　前受金
                countKeyID = getCount(countKeyID)

                Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'前受金'" '前受金
                Sql += "," & UtilClass.formatNumber(formatDouble(-calDeposit)) '入金金額
                Sql += ",1" '固定
                Sql += ",'" & getCustomerName(dsNkinkshihd.Tables(RS).Rows(i)("得意先コード").ToString) & "'" '補助科目
                Sql += ",'PM-" & dsNkinkshihd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(calDeposit + Paymentfee) '入金金額
                Sql += ",'" & codeAAC & "'" '会計用得意先コード
                Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                '支払手数料があれば
                If Paymentfee <> 0 Then

                    '借方　支払手数料
                    countKeyID = getCount(countKeyID)

                    Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
                    Sql += "," & seqID 'プライマリ
                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'支払手数料'" '支払手数料
                    Sql += "," & UtilClass.formatNumber(formatDouble(Paymentfee)) '支払手数料（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                    Sql += ",1" '固定
                    Sql += ",'" & getCustomerName(dsNkinkshihd.Tables(RS).Rows(i)("得意先コード").ToString) & "'" '補助科目
                    Sql += ",'PM-" & dsNkinkshihd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
                    Sql += ",''" '空でよし
                    Sql += "," & UtilClass.formatNumber(formatDouble(calDeposit + Paymentfee)) '入金金額
                    Sql += ",'" & codeAAC & "'" '会計用得意先コード
                    Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                    't67_swkhd データ登録
                    updateT67Swkhd(Sql)


                    '貸方　前受金
                    countKeyID = getCount(countKeyID)

                    Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
                    Sql += "," & seqID 'プライマリ
                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'前受金'" '前受金
                    Sql += "," & UtilClass.formatNumber(formatDouble(-Paymentfee)) '入金金額
                    Sql += ",1" '固定
                    Sql += ",'" & getCustomerName(dsNkinkshihd.Tables(RS).Rows(i)("得意先コード").ToString) & "'" '補助科目
                    Sql += ",'PM-" & dsNkinkshihd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
                    Sql += ",''" '空でよし
                    Sql += "," & UtilClass.formatNumber(calDeposit + Paymentfee) '入金金額
                    Sql += ",'" & codeAAC & "'" '会計用得意先コード
                    Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                    't67_swkhd データ登録
                    updateT67Swkhd(Sql)

                End If

#End Region

            ElseIf dsNkinkshihd.Tables(RS).Rows(i)("請求区分") = "2" Then  '通常請求の場合

                '前受金入金に対する振込入金処理                   普通預金				売掛金	得意先
                '（例：普通預金）		前受金					
                '※入金ヘッダ，明細にレコード登録時、							
                '　かつ入金番号に一致する請求Ｒが存在し、							
                '　かつ請求R.請求区分が「2:通常請求」の場合、							
                '　かつ請求R.受注番号と同一受注番号を持つ
                '            請求R.請求区分が「1:前受金」であるレコードが
                '            存在する場合、							
                '入金明細入金区分に応じて伝票を発生させる
                'ここでは「区分1:振込入金」を例としている

#Region "通常請求"

                Dim dsNkinSkyu2 As DataSet

                If strJyutyuNo = dsNkinkshihd.Tables(RS).Rows(i)("受注番号") Then
                Else
                    'select 入金消込額計 from t23_skyuhd join t27_nkinkshihd 
                    'where 受注番号 And 請求区分 = 1 And 締日 

                    Sql = "SELECT"
                    Sql += " sum(t27.入金消込額計) as 入金合計"
                    Sql += " FROM public.t23_skyuhd as t23 "
                    Sql += " left join public.t27_nkinkshihd as t27"
                    Sql += " on t23.請求番号 = t27.請求番号"

                    Sql += " WHERE "
                    Sql += " t27.会社コード"
                    Sql += " ILIKE  "
                    Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"

                    'todo:t23_skyuhd 条件
                    '条件オプション
                    'Sql = ""

                    Sql += " and t23.受注番号 = '" & dsNkinkshihd.Tables(RS).Rows(i)("受注番号") & "'"
                    'Sql += " and t23.請求番号 = '" & dsNkinkshihd.Tables(RS).Rows(i)("請求番号") & "'"
                    Sql += " and t23.請求区分 = 1"

                    dsNkinSkyu2 = _db.selectDB(Sql, RS, reccnt)  '他の請求データの取得

                    strJyutyuNo = dsNkinkshihd.Tables(RS).Rows(i)("受注番号")

                    '前払金のデータがあれば
                    If dsNkinSkyu2.Tables(RS).Rows.Count > 0 AndAlso Not IsDBNull(dsNkinSkyu2.Tables(RS).Rows(0)("入金合計")) Then
                        FormerGold = dsNkinSkyu2.Tables(RS).Rows(0)("入金合計")
                    End If

                End If


                '前受金のデータがあれば
                Dim FormerGold2 As Decimal = FormerGold
                If FormerGold2 > 0 Then

                    If calDeposit > FormerGold2 Then
                        '入金額が多い
                    Else
                        '前受金が多い
                        FormerGold2 = calDeposit
                    End If

                    '借方　前受金
                    Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
                    Sql += "," & seqID 'プライマリ
                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'前受金'" '借方勘定
                    Sql += "," & UtilClass.formatNumber(formatDouble(FormerGold2)) '入金金額
                    Sql += ",1" '固定
                    Sql += ",'" & getCustomerName(dsNkinkshihd.Tables(RS).Rows(i)("得意先コード").ToString) & "'" '補助科目
                    Sql += ",'PM-" & dsNkinkshihd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
                    Sql += ",''" '空でよし
                    Sql += "," & UtilClass.formatNumber(formatDouble(calDeposit + Paymentfee)) '入金金額
                    Sql += ",'" & codeAAC & "'" '会計用得意先コード
                    Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                    't67_swkhd データ登録
                    updateT67Swkhd(Sql)


                    '貸方　売掛金
                    countKeyID = getCount(countKeyID)

                    Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
                    Sql += "," & seqID 'プライマリ
                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'売掛金'" '借方勘定
                    Sql += "," & UtilClass.formatNumber(formatDouble(-FormerGold2)) '入金金額
                    Sql += ",1" '固定
                    Sql += ",'" & getCustomerName(dsNkinkshihd.Tables(RS).Rows(i)("得意先コード").ToString) & "'" '補助科目
                    Sql += ",'PM-" & dsNkinkshihd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
                    Sql += ",''" '空でよし
                    Sql += "," & UtilClass.formatNumber(formatDouble(calDeposit + Paymentfee)) '入金金額
                    Sql += ",'" & codeAAC & "'" '会計用得意先コード
                    Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                    't67_swkhd データ登録
                    updateT67Swkhd(Sql)

                End If


                '前受金相殺後に入金額があれば
                If calDeposit > FormerGold2 Then

                    If countKeyID = 0 Then
                    Else
                        countKeyID = getCount(countKeyID) '0～カウントアップ
                    End If

                    '借方  入金種別
                    Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
                    Sql += "," & seqID 'プライマリ
                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'" & strKamoku & "'"  '貸方科目
                    Sql += "," & UtilClass.formatNumber(formatDouble(calDeposit - FormerGold2)) '入金金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                    Sql += ",1" '固定
                    Sql += ",'" & getCustomerName(dsNkinkshihd.Tables(RS).Rows(i)("得意先コード").ToString) & "'" '補助科目
                    Sql += ",'PM-" & dsNkinkshihd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
                    Sql += ",''" '空でよし
                    Sql += "," & UtilClass.formatNumber(formatDouble(calDeposit + Paymentfee)) '入金金額
                    Sql += ",'" & codeAAC & "'" '会計用得意先コード
                    Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                    't67_swkhd データ登録
                    updateT67Swkhd(Sql)


                    '貸方　売掛金
                    countKeyID = getCount(countKeyID)

                    Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
                    Sql += "," & seqID 'プライマリ
                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'売掛金'" '借方勘定
                    Sql += "," & UtilClass.formatNumber(formatDouble(-(calDeposit - FormerGold2))) '入金金額
                    Sql += ",1" '固定
                    Sql += ",'" & getCustomerName(dsNkinkshihd.Tables(RS).Rows(i)("得意先コード").ToString) & "'" '補助科目
                    Sql += ",'PM-" & dsNkinkshihd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
                    Sql += ",''" '空でよし
                    Sql += "," & UtilClass.formatNumber(formatDouble(calDeposit + Paymentfee)) '入金金額
                    Sql += ",'" & codeAAC & "'" '会計用得意先コード
                    Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                    't67_swkhd データ登録
                    updateT67Swkhd(Sql)


                    '支払手数料があれば
                    If Paymentfee <> 0 Then

                        '借方　支払手数料
                        countKeyID = getCount(countKeyID)

                        Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
                        Sql += "," & seqID 'プライマリ
                        Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                        Sql += ",'支払手数料'" '支払手数料
                        Sql += "," & UtilClass.formatNumber(formatDouble(Paymentfee)) '支払手数料
                        Sql += ",1" '固定
                        Sql += ",'" & getCustomerName(dsNkinkshihd.Tables(RS).Rows(i)("得意先コード").ToString) & "'" '補助科目
                        Sql += ",'PM-" & dsNkinkshihd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                        Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
                        Sql += ",''" '空でよし
                        Sql += "," & UtilClass.formatNumber(formatDouble(calDeposit + Paymentfee)) '入金金額
                        Sql += ",'" & codeAAC & "'" '会計用得意先コード
                        Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                        't67_swkhd データ登録
                        updateT67Swkhd(Sql)


                        '貸方　売掛金
                        countKeyID = getCount(countKeyID)

                        Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
                        Sql += "," & seqID 'プライマリ
                        Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                        Sql += ",'売掛金'" '借方勘定
                        Sql += "," & UtilClass.formatNumber(formatDouble(-(Paymentfee))) '入金金額
                        Sql += ",1" '固定
                        Sql += ",'" & getCustomerName(dsNkinkshihd.Tables(RS).Rows(i)("得意先コード").ToString) & "'" '補助科目
                        Sql += ",'PM-" & dsNkinkshihd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                        Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
                        Sql += ",''" '空でよし
                        Sql += "," & UtilClass.formatNumber(formatDouble(calDeposit + Paymentfee)) '入金金額
                        Sql += ",'" & codeAAC & "'" '会計用得意先コード
                        Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                        't67_swkhd データ登録
                        updateT67Swkhd(Sql)
                    End If


                End If

                FormerGold -= FormerGold2

                'If calDeposit > FormerGold Then
                '    '支払額が多い
                '    FormerGold = 0
                'Else
                '    '前払金が多い
                '    FormerGold = FormerGold - calDeposit
                'End If
#End Region

            Else
                'todo:msgbox
                MsgBox("エラー")
            End If

            If Paymentfee <> 0 Then  '支払手数料があれば
                i = i + 1
            End If

        Next

        dsNkinkshihd = Nothing

#End Region


#Region "仕訳前払金"

        Dim strHatyuNo As String = vbNullString
        Dim DownPayment As Decimal = 0  '前支金

        't81
        Sql = "SELECT "
        Sql += " t81.買掛番号,t81.買掛区分,t81.発注番号,t81.支払番号,t81.行番号,t81.支払日,t81.支払種目,t81.支払種目名"
        Sql += ",t81.仕入先コード,t81.客先番号,t81.支払額"

        Sql += " FROM public.t81_shiwakeshi as t81 "

        Sql += " WHERE "
        Sql += " t81.会社コード"
        Sql += " ILIKE  "
        Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"

        '条件オプション
        'Sql += " and t26.入金日 <= '" & dsCompany.Tables(RS).Rows(0)("今回締日") & "'"

        Sql += " ORDER BY "
        Sql += " t81.買掛番号,t81.買掛区分,t81.発注番号,t81.支払番号,t81.行番号,t81.支払日,t81.支払種目"
        Sql += ",t81.仕入先コード"

        Dim dsShrikshihd As DataSet = _db.selectDB(Sql, RS, reccnt)

        't81
        For i As Integer = 0 To dsShrikshihd.Tables(RS).Rows.Count - 1

            '会計用仕入先コードの取得
            Dim codeAAC As String = getSupplierName(dsShrikshihd.Tables(RS).Rows(i)("仕入先コード"))

            'プライマリ
            upSeq()
            seqID = getSeq("transactionid_seq")

            '行カウント
            Dim countKeyID As Integer = 0

            Dim calPay As Decimal = dsShrikshihd.Tables(RS).Rows(i)("支払額")  '支払
            Dim Paymentfee As Decimal = 0  '支払手数料
            Dim strDESCRIPTION As String = vbNullString

            't81　支払金額等の値を取得
            Call mGet_money_t81(dsShrikshihd.Tables(RS).Rows(i)("支払番号"), dsShrikshihd.Tables(RS).Rows(i)("行番号") _
                                       , Paymentfee)


            ''DESCRIPTIONの生成 受注番号 発注番号格納
            strDESCRIPTION = mGet_DESCRIPTION(dsShrikshihd.Tables(RS).Rows(i)("買掛番号") _
                                             , dsShrikshihd.Tables(RS).Rows(i)("支払番号") _
                                             , 0)

            '支払種別に対応した科目を取得
            'Dim strKamoku As String = mGet_NyukinSyubetu(dsShrikshihd.Tables(RS).Rows(i)("支払種別"))
            Dim strKamoku = dsShrikshihd.Tables(RS).Rows(i)("支払種目名")


            If dsShrikshihd.Tables(RS).Rows(i)("支払種目") = "9" Then  '相殺

#Region "相殺"
                '借方　買掛金
                Sql = ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'"
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'買掛金'" '借方勘定　
                Sql += "," & UtilClass.formatNumber(formatDouble(calPay)) '支払金額
                Sql += ",1" '固定
                Sql += ",'" & getSupplierName(dsShrikshihd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                Sql += ",'PO-" & dsShrikshihd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'"
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(formatDouble(calPay)) '支払金額
                Sql += ",'" & codeAAC & "'" '会計用支払先コード
                Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                updateT67Swkhd(Sql) 'update実行


                '貸方　売掛金
                countKeyID = getCount(countKeyID) '0～カウントアップ

                Sql = ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'"
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'売掛金'"  '借方科目
                Sql += "," & UtilClass.formatNumber(formatDouble(-calPay)) '支払金額
                Sql += ",1" '固定
                Sql += ",'" & getSupplierName(dsShrikshihd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                Sql += ",'PO-" & dsShrikshihd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'"
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(formatDouble(calPay)) '支払金額
                Sql += ",'" & codeAAC & "'" '会計用支払先コード
                Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                updateT67Swkhd(Sql) 'update実行
#End Region


            ElseIf dsShrikshihd.Tables(RS).Rows(i)("買掛区分") = 1 Then  '前払買掛
                '前払金の支払（現金支払）		　　　　　前払金				現金	仕入先
                '※支払ヘッダ,明細にレコード登録時、							
                '　かつ支払番号に一致する消込Ｒが存在し、							
                '　消込Ｒ.買掛番号に一致する買掛Rが存在し、							
                '　かつ買掛R.買掛区分が「2:前払金」の場合、							
                '　支払明細支払種別に応じて伝票を発生させる
                'ここでは「区分3:現金」を例としている
                '  発注F-(発注番号) - 買掛F - (買掛番号) - 消込F - (支払番号) - 支払F                            


#Region "前払買掛"

                '借方　前払金
                Sql = ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'"
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'前払金'" '借方勘定　
                Sql += "," & UtilClass.formatNumber(formatDouble(calPay)) '支払金額
                Sql += ",1" '固定
                Sql += ",'" & getSupplierName(dsShrikshihd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'"
                Sql += ",'PO-" & dsShrikshihd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'"
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(formatDouble(calPay + Paymentfee))
                Sql += ",'" & codeAAC & "'" '会計用支払先コード
                Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                updateT67Swkhd(Sql) 'update実行


                '貸方　支払種別
                countKeyID = getCount(countKeyID) '0～カウントアップ

                Sql = ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'"
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'" & strKamoku & "'"  '貸方勘定
                Sql += "," & UtilClass.formatNumber(formatDouble(-calPay)) '支払金額
                Sql += ",1" '固定
                Sql += ",'" & getSupplierName(dsShrikshihd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'"
                Sql += ",'PO-" & dsShrikshihd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'"
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(formatDouble(calPay + Paymentfee)) '支払金額
                Sql += ",'" & codeAAC & "'" '会計用支払先コード
                Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                updateT67Swkhd(Sql) 'update実行


                If Paymentfee <> 0 Then  '支払手数料があれば

                    '借方　前払金
                    countKeyID = getCount(countKeyID)

                    Sql = ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'"
                    Sql += "," & seqID 'プライマリ
                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'前払金'" '借方勘定　
                    Sql += "," & UtilClass.formatNumber(formatDouble(Paymentfee)) '支払金額
                    Sql += ",1" '固定
                    Sql += ",'" & getSupplierName(dsShrikshihd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'"
                    Sql += ",'PO-" & dsShrikshihd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'"
                    Sql += ",''" '空でよし
                    Sql += "," & UtilClass.formatNumber(formatDouble(calPay + Paymentfee))
                    Sql += ",'" & codeAAC & "'" '会計用支払先コード
                    Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                    updateT67Swkhd(Sql) 'update実行


                    '貸方　支払手数料
                    countKeyID = getCount(countKeyID)

                    Sql = ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'"
                    Sql += "," & seqID 'プライマリ
                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'支払手数料'" '支払手数料
                    Sql += "," & UtilClass.formatNumber(formatDouble(-Paymentfee)) '支払手数料
                    Sql += ",1" '固定
                    Sql += ",'" & getSupplierName(dsShrikshihd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'"
                    Sql += ",'PM-" & dsShrikshihd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'"
                    Sql += ",''" '空でよし
                    Sql += "," & UtilClass.formatNumber(formatDouble(calPay + Paymentfee))
                    Sql += ",'" & codeAAC & "'" '会計用得意先コード
                    Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                    countKeyID = getCount(countKeyID)

                    't67_swkhd データ登録
                    updateT67Swkhd(Sql)
                End If

#End Region

            ElseIf dsShrikshihd.Tables(RS).Rows(i)("買掛区分") = 2 Then  '通常買掛
                '前払金支払に対する振込支払精算（普通預金）     買掛金	仕入先			普通預金	
                '							　　　　　　　　　　　　　　　　　　　　　　前払金	
                '※支払ヘッダ，明細にレコード登録時、							
                '　かつ支払番号に一致する消込Ｒが存在し、							
                '　消込R.買掛番号に一致する買掛Ｒが存在し、							
                '　かつ買掛R.買掛区分が1 : 通常支払の場合、							
                '　かつ買掛R.発注番号と同一発注番号を持つ
                '  買掛R.買掛区分が2 : 前受金であるレコードが存在する場合、							
                '  支払明細支払区分に応じて伝票を発生させる
                '  ここでは「区分1:振込入金」を例としている

#Region "通常買掛"

                'todo:前払金を減らす
                Dim dsShriKike2 As DataSet

                'select 支払消込額計 from t46_kikehd join t49_shrikshihd 
                'where 発注番号 And 買掛区分 = 1 And 締日 
                If strHatyuNo = dsShrikshihd.Tables(RS).Rows(i)("発注番号") Then
                Else

                    Sql = "SELECT"
                    Sql += " sum(t49.支払消込額計) as 支払合計"
                    Sql += " FROM public.t46_kikehd as t46 "
                    Sql += " left join public.t49_shrikshihd as t49"
                    Sql += " on t46.買掛番号 = t49.買掛番号"

                    Sql += " WHERE "
                    Sql += " t49.会社コード"
                    Sql += " ILIKE  "
                    Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"

                    'todo:t23_skyuhd 条件
                    '条件オプション
                    'Sql = ""

                    Sql += " and t46.発注番号 = '" & dsShrikshihd.Tables(RS).Rows(i)("発注番号") & "'"
                    Sql += " and t46.買掛区分 = 1"

                    dsShriKike2 = _db.selectDB(Sql, RS, reccnt)  '他の買掛データの取得

                    strHatyuNo = dsShrikshihd.Tables(RS).Rows(i)("発注番号")

                    '前払金のデータがあれば
                    If dsShriKike2.Tables(RS).Rows.Count > 0 AndAlso Not IsDBNull(dsShriKike2.Tables(RS).Rows(0)("支払合計")) Then
                        DownPayment = dsShriKike2.Tables(RS).Rows(0)("支払合計")
                    End If
                End If


                '前払金のデータがあれば
                Dim DownPayment2 As Decimal = DownPayment
                If DownPayment2 > 0 Then

                    If calPay > DownPayment2 Then
                        '支払額が多い
                    Else
                        '前払金が多い
                        DownPayment2 = calPay
                    End If


                    '借方 買掛金
                    Sql = ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'" '
                    Sql += "," & seqID 'プライマリ
                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'買掛金'" '借方勘定　
                    Sql += "," & UtilClass.formatNumber(formatDouble(DownPayment2)) '支払金額
                    Sql += ",1" '固定
                    Sql += ",'" & getSupplierName(dsShrikshihd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                    Sql += ",'PO-" & dsShrikshihd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'"
                    Sql += ",''" '空でよし
                    Sql += "," & UtilClass.formatNumber(formatDouble(calPay + Paymentfee)) '支払金額
                    Sql += ",'" & codeAAC & "'" '会計用支払先コード
                    Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                    updateT67Swkhd(Sql) 'update実行


                    '貸方　前払金
                    countKeyID = getCount(countKeyID) '0～カウントアップ

                    Sql = ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'"
                    Sql += "," & seqID 'プライマリ
                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'前払金'"  '貸方勘定
                    Sql += "," & UtilClass.formatNumber(formatDouble(-DownPayment2)) '支払金額
                    Sql += ",1" '固定
                    Sql += ",'" & getSupplierName(dsShrikshihd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                    Sql += ",'PO-" & dsShrikshihd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'"
                    Sql += ",''" '空でよし
                    Sql += "," & UtilClass.formatNumber(formatDouble(calPay + Paymentfee)) '支払金額
                    Sql += ",'" & codeAAC & "'" '会計用支払先コード
                    Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                    updateT67Swkhd(Sql) 'update実行

                End If


                '前受金相殺後に入金額があれば
                If calPay > DownPayment2 Then

                    '借方 買掛金
                    If countKeyID = 0 Then
                    Else
                        countKeyID = getCount(countKeyID) '0～カウントアップ
                    End If

                    Sql = ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'" '
                    Sql += "," & seqID 'プライマリ
                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'買掛金'" '借方勘定　
                    Sql += "," & UtilClass.formatNumber(formatDouble(calPay - DownPayment2)) '支払金額
                    Sql += ",1" '固定
                    Sql += ",'" & getSupplierName(dsShrikshihd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                    Sql += ",'PO-" & dsShrikshihd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'"
                    Sql += ",''" '空でよし
                    Sql += "," & UtilClass.formatNumber(formatDouble(calPay + Paymentfee)) '支払金額
                    Sql += ",'" & codeAAC & "'" '会計用支払先コード
                    Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                    updateT67Swkhd(Sql) 'update実行


                    '貸方　支払種別
                    countKeyID = getCount(countKeyID) '0～カウントアップ

                    Sql = ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'"
                    Sql += "," & seqID 'プライマリ
                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'" & strKamoku & "'"  '貸方科目
                    Sql += "," & UtilClass.formatNumber(formatDouble(-(calPay - DownPayment2))) '支払金額
                    Sql += ",1" '固定
                    Sql += ",'" & getSupplierName(dsShrikshihd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                    Sql += ",'PO-" & dsShrikshihd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'"
                    Sql += ",''" '空でよし
                    Sql += "," & UtilClass.formatNumber(formatDouble(calPay + Paymentfee)) '支払金額
                    Sql += ",'" & codeAAC & "'" '会計用支払先コード
                    Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                    updateT67Swkhd(Sql) 'update実行


                    '支払手数料があれば
                    If Paymentfee <> 0 Then

                        '借方 買掛金
                        countKeyID = getCount(countKeyID) '0～カウントアップ

                        Sql = ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'" '
                        Sql += "," & seqID 'プライマリ
                        Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                        Sql += ",'買掛金'" '借方勘定　
                        Sql += "," & UtilClass.formatNumber(formatDouble(Paymentfee)) '支払金額
                        Sql += ",1" '固定
                        Sql += ",'" & getSupplierName(dsShrikshihd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                        Sql += ",'PO-" & dsShrikshihd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                        Sql += ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'"
                        Sql += ",''" '空でよし
                        Sql += "," & UtilClass.formatNumber(formatDouble(calPay + Paymentfee)) '支払金額
                        Sql += ",'" & codeAAC & "'" '会計用支払先コード
                        Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                        updateT67Swkhd(Sql) 'update実行


                        '貸方　支払手数料
                        countKeyID = getCount(countKeyID)

                        Sql = ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'"
                        Sql += "," & seqID 'プライマリ
                        Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                        Sql += ",'支払手数料'" '貸方勘定
                        Sql += "," & UtilClass.formatNumber(formatDouble(-Paymentfee)) '支払手数料
                        Sql += ",1" '固定
                        Sql += ",'" & getSupplierName(dsShrikshihd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                        Sql += ",'PM-" & dsShrikshihd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                        Sql += ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'"
                        Sql += ",''" '空でよし
                        Sql += "," & UtilClass.formatNumber(formatDouble(calPay + Paymentfee)) '支払金額
                        Sql += ",'" & codeAAC & "'" '会計用得意先コード
                        Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                        't67_swkhd データ登録
                        updateT67Swkhd(Sql)
                    End If

                End If

                DownPayment -= DownPayment2
                'If calPay > DownPayment Then
                '    '支払額が多い
                '    DownPayment = 0
                'Else
                '    '前払金が多い
                '    DownPayment = DownPayment - calPay
                'End If
#End Region

            Else
                    'todo:msgbox テスト
                    MsgBox("エラー")

            End If

            If Paymentfee <> 0 Then  '支払手数料があれば
                i = i + 1
            End If

        Next

        dsShrikshihd = Nothing
#End Region


        'todo:大元を作成中につき、一時的にコメントアウト
#Region "入出庫"

        '        't70_inout
        '        't43_nyukodt
        '        Sql = "SELECT"
        '        Sql += " t70,入出庫日,t70.入出庫区分,t70.入出庫種別,t70.仕入区分"  'todo:取消区分

        '        Sql += ",sum(t70.数量) as 数量計,sum(t43.仕入値) as 仕入値計"

        '        Sql += " FROM public.t70_inout as t70 "
        '        Sql += " left join public.t43_nyukodt as t43"
        '        Sql += " on (t70.伝票番号 = t43.入庫番号 and t70.行番号 = t43.行番号)"

        '        Sql += " WHERE "
        '        Sql += " t70.会社コード"
        '        Sql += " ILIKE  "
        '        Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"

        '        'todo:t42_nyukohd 条件
        '        '条件オプション
        '        'Sql = ""

        '        'Sql += " and not(t41.仕入区分 is null)"

        '        Sql += " GROUP BY "
        '        Sql += " t70,入出庫日,t70.入出庫区分,t70.入出庫種別,t70.仕入区分"  'todo:取消区分

        '        Sql += " ORDER BY "
        '        Sql += " t70,入出庫日,t70.入出庫区分,t70.入出庫種別,t70.仕入区分"

        '        Dim dsNyusyukko As DataSet = _db.selectDB(Sql, RS, reccnt)


        '        For i As Integer = 0 To dsSWKNyukohd.Tables(RS).Rows.Count - 1  't42_nyukohd

        '            Dim countKeyID As Integer = 0

        '            upSeq() 'シーケンス更新
        '            seqID = getSeq("transactionid_seq")
        '            Console.WriteLine(seqID)

        '            Dim InOut As Decimal = dsNyusyukko.Tables(RS).Rows(i)("仕入値計") * dsNyusyukko.Tables(RS).Rows(i)("数量計")

        '            If dsNyusyukko.Tables(RS).Rows(i)("入出庫区分") = 1 _
        '                And （dsNyusyukko.Tables(RS).Rows(i)("入出庫種別") = 0 Or dsNyusyukko.Tables(RS).Rows(i)("入出庫種別") = 1) _
        '                And dsNyusyukko.Tables(RS).Rows(i)("仕入区分") = 0 Then

        '                '期中の実地棚卸等に基づく在庫増     　　　　　商品				雑収入	
        '                '※入出庫Fにレコード登録時、							
        '                '　かつ入出庫区分「1: 入庫」、							
        '                '　かつ入出庫種別「0:通常」 or「1:サンプル」、							
        '                '　かつ仕入区分「0:移動」のレコードである場合、伝票を発生させる							
        '                '入庫情報として伝票番号、行番号に一致する入庫明細を参照する

        '#Region "在庫増"

        '                '借方　商品
        '                Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("入出庫日"), "yyyyMM") & "'"
        '                Sql += "," & seqID 'プライマリ
        '                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
        '                'Sql += ",'" & getAccountName("inventory") & "'" '棚卸資産
        '                Sql += ",'商品'"  '借方勘定
        '                Sql += "," & formatDouble(InOut) '仕入値 * 数量（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
        '                Sql += ",1" '固定
        '                Sql += ",''" '補助科目
        '                'Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
        '                Sql += ",''" 'PO
        '                Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("入出庫日"), "yyyy-MM-dd") & "'"
        '                Sql += ",''" '空でよし
        '                Sql += ",'" & formatDouble(1) & "'" '仕入金額 + VAT IN
        '                Sql += ",''" '空でよし
        '                Sql += ",''" '空でよし

        '                't67_swkhd データ登録
        '                updateT67Swkhd(Sql)


        '                '貸方　雑収入
        '                countKeyID = getCount(countKeyID)

        '                Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("入出庫日"), "yyyyMM") & "'"
        '                Sql += "," & seqID 'プライマリ
        '                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
        '                Sql += ",'雑収入'"
        '                Sql += "," & formatDouble(InOut) '仕入値 * 数量（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
        '                Sql += ",1" '固定
        '                Sql += ",''" '補助科目
        '                'Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
        '                Sql += ",''" 'PO
        '                Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("入出庫日"), "yyyy-MM-dd") & "'"
        '                Sql += ",''" '空でよし
        '                Sql += ",'" & formatDouble(1) & "'" '仕入金額 + VAT IN
        '                Sql += ",''" '空でよし
        '                Sql += ",''" '空でよし

        '                't67_swkhd データ登録
        '                updateT67Swkhd(Sql)

        '#End Region

        '            ElseIf dsNyusyukko.Tables(RS).Rows(i)("入出庫区分") = 1 _
        '                And （dsNyusyukko.Tables(RS).Rows(i)("入出庫種別") = 2 Or dsNyusyukko.Tables(RS).Rows(i)("入出庫種別") = 3) _
        '                And dsNyusyukko.Tables(RS).Rows(i)("仕入区分") = 0 Then

        '                '期中の実地棚卸等に基づく在庫減  　　　　　   棚卸減耗損				期末商品棚卸高	
        '                '※入出庫Fにレコード登録時、							
        '                '　かつ入出庫区分「1:入庫」、							
        '                '　かつ入出庫種別「2:廃棄」、							
        '                '　かつ仕入区分「0:移動」のレコードである場合、伝票を発生させる							
        '                '入庫情報として伝票番号、行番号に一致する入庫明細を参照する

        '                '期末の実地棚卸等に基づく在庫減     　　　　　棚卸減耗損				期末商品棚卸高	
        '                '※入出庫Fにレコード登録時、							
        '                '　かつ入出庫区分「1:入庫」、							
        '                '　かつ入出庫種別「3:棚卸ロス」、							
        '                '　かつ仕入区分「0:移動」のレコードである場合、伝票を発生させる							
        '                '入庫情報として伝票番号、行番号に一致する入庫明細を参照する


        '#Region "在庫減,棚卸ロス"

        '                '借方　棚卸減耗損
        '                Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("入出庫日"), "yyyyMM") & "'"
        '                Sql += "," & seqID 'プライマリ
        '                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
        '                Sql += ",'棚卸減耗損'"
        '                Sql += "," & formatDouble(InOut) '仕入値 * 数量（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
        '                Sql += ",1" '固定
        '                Sql += ",''" '補助科目
        '                'Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
        '                Sql += ",''" 'PO
        '                Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("入出庫日"), "yyyy-MM-dd") & "'"
        '                Sql += ",''" '空でよし
        '                Sql += ",'" & formatDouble(1) & "'" '仕入金額 + VAT IN
        '                Sql += ",''" '空でよし
        '                Sql += ",''" '空でよし

        '                't67_swkhd データ登録
        '                updateT67Swkhd(Sql)


        '                '貸方　期末商品棚卸高
        '                countKeyID = getCount(countKeyID)

        '                Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("入出庫日"), "yyyyMM") & "'"
        '                Sql += "," & seqID 'プライマリ
        '                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
        '                'Sql += ",'" & getAccountName("inventory") & "'" '棚卸資産
        '                Sql += ",'期末商品棚卸高'"
        '                Sql += "," & formatDouble(InOut) '仕入値 * 数量（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
        '                Sql += ",1" '固定
        '                Sql += ",''" '補助科目
        '                'Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
        '                Sql += ",''" 'PO
        '                Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("入出庫日"), "yyyy-MM-dd") & "'"
        '                Sql += ",''" '空でよし
        '                Sql += ",'" & formatDouble(1) & "'"
        '                Sql += ",''" '空でよし
        '                Sql += ",''" '空でよし

        '                't67_swkhd データ登録
        '                updateT67Swkhd(Sql)

        '#End Region

        '            Else
        '                'todo:msgbox テスト
        '                MsgBox("エラー")

        '            End If

        '        Next

#End Region  '入出庫

    End Sub


    Private Sub mGet_money_t41_siredt(ByVal strSiire As String, ByVal VAT As Decimal _
                                      , ByRef calVat As Decimal, ByRef calCost As Decimal, ByRef Indirectfees As Decimal _
                                      , ByRef calKanzei As Decimal, ByRef calMaebarai As Decimal, ByRef calYuso As Decimal)

        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = "" 'SQL文用

        't41_siredt
        Sql = "SELECT "
        Sql += " t41.仕入値,t41.仕入数量,t41.間接費"
        Sql += ",t21.関税額,t21.前払法人税額,t21.輸送費額"

        Sql += " FROM public.t41_siredt as t41"

        Sql += " left join public.t21_hattyu t21"
        Sql += "  on t41.発注番号 = t21.発注番号 and t41.発注番号枝番 = t21.発注番号枝番"
        Sql += " and t41.行番号 = t21.行番号"

        Sql += " WHERE "
        Sql += " t41.会社コード"
        Sql += " ILIKE  "
        Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"

        Sql += " and t41.仕入番号 = '" & strSiire & "'"

        Dim dsSWKNyukodt As DataSet = _db.selectDB(Sql, RS, reccnt)

        For j As Integer = 0 To dsSWKNyukodt.Tables(RS).Rows.Count - 1

            '棚卸資産
            calCost += dsSWKNyukodt.Tables(RS).Rows(j)("仕入値") * dsSWKNyukodt.Tables(RS).Rows(j)("仕入数量")

            Indirectfees += dsSWKNyukodt.Tables(RS).Rows(j)("間接費")

            calKanzei += rmNullDecimal(dsSWKNyukodt.Tables(RS).Rows(j)("関税額")) * dsSWKNyukodt.Tables(RS).Rows(j)("仕入数量")
            calMaebarai += rmNullDecimal(dsSWKNyukodt.Tables(RS).Rows(j)("前払法人税額")) * dsSWKNyukodt.Tables(RS).Rows(j)("仕入数量")
            calYuso += rmNullDecimal(dsSWKNyukodt.Tables(RS).Rows(j)("輸送費額")) * dsSWKNyukodt.Tables(RS).Rows(j)("仕入数量")
        Next

        'VAT-IN
        calVat = calCost * VAT / 100


        dsSWKNyukodt = Nothing

    End Sub

    Private Function mGet_DESCRIPTION(ByVal strJyutyu As Object, ByVal strHatyu As Object, ByVal intFlg As Integer) As String

        Dim strDESCRIPTION As String


        If IsDBNull(strJyutyu) OrElse strJyutyu = vbNullString Then  '受注なし
            strDESCRIPTION = strHatyu
        Else
            strDESCRIPTION = strHatyu & " " & strJyutyu
        End If


        If intFlg = 0 Then
        ElseIf intFlg = 1 Then
            '関税額
            strDESCRIPTION += " 関税額"
        ElseIf intFlg = 2 Then
            '前払法人税額
            strDESCRIPTION += " 前払法人税額"
        ElseIf intFlg = 3 Then
            '輸送額
            strDESCRIPTION += " 輸送額"
        End If

        mGet_DESCRIPTION = LTrim(strDESCRIPTION)

    End Function


    Private Sub mGet_money_t31_urigdt(ByVal strUriage As String, ByVal VAT As Decimal _
                                      , ByRef calSales As Decimal, ByRef calVat As Decimal, ByRef calCost As Decimal, ByRef Indirectfees As Decimal _
                                      , ByRef calKanzei As Decimal, ByRef calMaebarai As Decimal, ByRef calYuso As Decimal)

        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = "" 'SQL文用

        't31_urigdt
        Sql = "SELECT "
        Sql += " *"

        Sql += " FROM public.t31_urigdt as t31"

        'Sql += " left join public.t21_hattyu t21"
        'Sql += "  on t41.発注番号 = t21.発注番号 and t41.発注番号枝番 = t21.発注番号枝番"
        'Sql += " and t41.行番号 = t21.行番号"

        Sql += " WHERE "
        Sql += " t31.会社コード"
        Sql += " ILIKE  "
        Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"

        Sql += " and t31.売上番号 = '" & strUriage & "'"

        Dim dsSwkUrigdt As DataSet = _db.selectDB(Sql, RS, reccnt)

        For j As Integer = 0 To dsSwkUrigdt.Tables(RS).Rows.Count - 1

            calSales += dsSwkUrigdt.Tables(RS).Rows(j)("売単価") * dsSwkUrigdt.Tables(RS).Rows(j)("売上数量")
            calCost += dsSwkUrigdt.Tables(RS).Rows(j)("仕入値") * dsSwkUrigdt.Tables(RS).Rows(j)("売上数量")


            calKanzei += rmNullDecimal(dsSwkUrigdt.Tables(RS).Rows(j)("関税額")) * dsSwkUrigdt.Tables(RS).Rows(j)("売上数量")
            calMaebarai += rmNullDecimal(dsSwkUrigdt.Tables(RS).Rows(j)("前払法人税額")) * dsSwkUrigdt.Tables(RS).Rows(j)("売上数量")
            calYuso += rmNullDecimal(dsSwkUrigdt.Tables(RS).Rows(j)("輸送費額")) * dsSwkUrigdt.Tables(RS).Rows(j)("売上数量")
        Next

        'VAT-IN
        calVat = calSales * VAT / 100

        '間接費
        Indirectfees = calKanzei + calMaebarai + calYuso

        dsSwkUrigdt = Nothing

    End Sub


    Private Sub mGet_money_t80(ByVal strNyukin As String, ByVal strGyo As String _
                                      , ByRef Paymentfee As Decimal)

        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = "" 'SQL文用

        't80
        Sql = "SELECT "
        Sql += " 入金種目,入金額"
        Sql += " FROM public.t80_shiwakenyu as t80"

        Sql += " WHERE "
        Sql += " t80.会社コード"
        Sql += " ILIKE  "
        Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"

        Sql += " and t80.入金番号 = '" & strNyukin & "'"
        Sql += " and t80.行番号 = '" & strGyo + 1 & "'"

        Dim dsNkinkshidt As DataSet = _db.selectDB(Sql, RS, reccnt)

        If dsNkinkshidt.Tables(RS).Rows.Count > 0 Then
            If dsNkinkshidt.Tables(RS).Rows(0)("入金種目") = 2 Then
                '手数料がある場合
                Paymentfee = dsNkinkshidt.Tables(RS).Rows(0)("入金額")
            End If
        End If

        dsNkinkshidt = Nothing

    End Sub

    Private Sub mGet_money_t81(ByVal strShiharai As String, ByVal strGyo As String _
                                      , ByRef Paymentfee As Decimal)

        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = "" 'SQL文用

        't81
        Sql = "SELECT "
        Sql += " 支払種目,支払額"
        Sql += " FROM public.t81_shiwakeshi as t81"

        Sql += " WHERE "
        Sql += " t81.会社コード"
        Sql += " ILIKE  "
        Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"

        Sql += " and t81.支払番号 = '" & strShiharai & "'"
        Sql += " and t81.行番号 = '" & strGyo + 1 & "'"

        Dim dsNkinkshidt As DataSet = _db.selectDB(Sql, RS, reccnt)

        If dsNkinkshidt.Tables(RS).Rows.Count > 0 Then
            If dsNkinkshidt.Tables(RS).Rows(0)("支払種目") = 2 Then
                '手数料がある場合
                Paymentfee = dsNkinkshidt.Tables(RS).Rows(0)("支払額")
            End If
        End If

        dsNkinkshidt = Nothing

    End Sub

    '仕訳データのXML出力
    Private Sub getShiwakeData_20190711()
        Dim dtToday As DateTime = DateTime.Now '年月の設定
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = "" 'SQL文用
        Dim seqID As Integer 'TRANSACTIONID用変数

        Dim dsCompany As DataSet = getDsData("m01_company") 'ログイン情報から会社データの取得


        'todo:仕分

#Region "仕訳買掛金"


        't42_nyukohd 入庫基本  
        't41_siredt
        't40_sirehd
        Sql = "SELECT"
        Sql += " t42.入庫日,t40.仕入先名,t40.客先番号,t40.仕入日,t41.仕入区分,t42.取消区分"
        Sql += ",sum(t42.仕入金額) as 仕入金額計,t42.ＶＡＴ"

        Sql += " FROM public.t42_nyukohd as t42 "
        Sql += " left join public.t41_siredt as t41"
        Sql += " on (t42.発注番号 = t41.発注番号 and t42.発注番号枝番 = t41.発注番号枝番)"

        Sql += " left join public.t40_sirehd as t40"
        Sql += " on t42.発注番号 = t40.発注番号"

        Sql += " WHERE "
        Sql += " t42.会社コード"
        Sql += " ILIKE  "
        Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"

        'todo:t42_nyukohd 条件
        '条件オプション
        'Sql = ""

        Sql += " and not(t41.仕入区分 is null)"

        Sql += " GROUP BY "
        Sql += " t42.入庫日,t40.仕入先名,t40.客先番号,t40.仕入日,t41.仕入区分,t42.取消区分,t42.ＶＡＴ"

        Sql += " ORDER BY "
        Sql += " t40.仕入日,t42.入庫日,t40.客先番号"


        Dim dsSWKNyukohd As DataSet = _db.selectDB(Sql, RS, reccnt)

        For i As Integer = 0 To dsSWKNyukohd.Tables(RS).Rows.Count - 1  't42_nyukohd

            '入庫データ回しながら以下データ作成
            '借方：棚卸資産
            '貸方：買掛金

            Dim countKeyID As Integer = 0

            upSeq() 'シーケンス更新
            seqID = getSeq("transactionid_seq")
            Console.WriteLine(seqID)


            ''棚卸資産, 買掛金 = 仕入金額 * (VAT / 100)
            'Dim calCost As Decimal = dsSWKNyukohd.Tables(RS).Rows(i)("仕入金額計") * (dsSWKNyukohd.Tables(RS).Rows(i)("VAT") / 100)
            ''VAT-IN, 買掛金 = calCost * (VAT / 100)
            'Dim calVat As Decimal = calCost * (dsSWKNyukohd.Tables(RS).Rows(i)("VAT") / 100)

            '棚卸資産, 買掛金 = 仕入金額 * (VAT / 100)
            Dim calVat As Decimal = dsSWKNyukohd.Tables(RS).Rows(i)("仕入金額計") * (dsSWKNyukohd.Tables(RS).Rows(i)("VAT") / 100)
            'VAT-IN, 買掛金
            Dim calCost As Decimal = dsSWKNyukohd.Tables(RS).Rows(i)("仕入金額計")

            If dsSWKNyukohd.Tables(RS).Rows(i)("仕入区分") <> 0 _
                And dsSWKNyukohd.Tables(RS).Rows(i)("取消区分") = 0 Then
                '入庫入力に伴う仕入伝票計上
                '※入庫Ｈ，明細にレコード登録時、
                'かつ「仕入区分0:移動」以外かつ「取消区分0:有効」

#Region "入庫入力"

                '借方　棚卸資産
                Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("入庫日"), "yyyyMM") & "'" '入庫日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                'Sql += ",'" & getAccountName("inventory") & "'" '棚卸資産
                Sql += ",'棚卸資産'" '借方勘定  
                Sql += "," & UtilClass.formatNumber(formatDouble(calCost)) '仕入金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & getSupplierName(dsSWKNyukohd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("仕入日"), "yyyy-MM-dd") & "'" '仕入日
                Sql += ",''" '空でよし
                Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat)) & "'" '仕入金額 + VAT IN
                Sql += ",''" '空でよし
                Sql += ",''" '空でよし

                't67_swkhd データ登録
                updateT67Swkhd(Sql)

                ''金額がゼロの時は登録しない
                'If calCost <> 0 Then
                '    't67_swkhd データ登録
                '    updateT67Swkhd(Sql)
                'End If


                '貸方　買掛金
                countKeyID = getCount(countKeyID)

                Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("入庫日"), "yyyyMM") & "'" '入庫日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'買掛金'" '貸方勘定　
                Sql += "," & UtilClass.formatNumber(formatDouble(-calCost)) '仕入金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & getSupplierName(dsSWKNyukohd.Tables(RS).Rows(0)("仕入先コード").ToString) & "'" '補助科目
                Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("仕入日"), "yyyy-MM-dd") & "'" '仕入日
                Sql += ",''" '空でよし
                Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat)) & "'" '仕入金額 + VAT IN
                Sql += ",''" '空でよし
                Sql += ",''" '空でよし

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                'VATがある場合
                If calVat <> 0 Then

                    '借方　VAT-IN
                    countKeyID = getCount(countKeyID)

                    Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("入庫日"), "yyyyMM") & "'" '入庫日
                    Sql += "," & seqID 'プライマリ
                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'VAT-IN'" '借方勘定  
                    Sql += "," & UtilClass.formatNumber(formatDouble(calVat)) '仕入金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                    Sql += ",1" '固定
                    Sql += ",'" & getSupplierName(dsSWKNyukohd.Tables(RS).Rows(0)("仕入先コード").ToString) & "'" '補助科目
                    Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("仕入日"), "yyyy-MM-dd") & "'" '仕入日
                    Sql += ",''" '空でよし
                    Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat)) & "'" '仕入金額 + VAT IN
                    Sql += ",''" '空でよし
                    Sql += ",''" '空でよし

                    't67_swkhd データ登録
                    updateT67Swkhd(Sql)


                    '貸方　買掛金
                    countKeyID = getCount(countKeyID)

                    Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("入庫日"), "yyyyMM") & "'" '入庫日
                    Sql += "," & seqID 'プライマリ
                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'買掛金'" '貸方勘定
                    Sql += "," & UtilClass.formatNumber(formatDouble(-calVat)) '仕入金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                    Sql += ",1" '固定
                    Sql += ",'" & getSupplierName(dsSWKNyukohd.Tables(RS).Rows(0)("仕入先コード").ToString) & "'" '補助科目
                    Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("仕入日"), "yyyy-MM-dd") & "'" '仕入日
                    Sql += ",''" '空でよし
                    Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat)) & "'" '仕入金額 + VAT IN
                    Sql += ",''" '空でよし
                    Sql += ",''" '空でよし

                    't67_swkhd データ登録
                    updateT67Swkhd(Sql)

                End If

                countKeyID = getCount(countKeyID)
#End Region

            ElseIf dsSWKNyukohd.Tables(RS).Rows(i)("仕入区分") <> 0 _
                And dsSWKNyukohd.Tables(RS).Rows(i)("取消区分") = 1 Then
                '入庫取消に伴う仕入返品計上
                '※入庫Ｈ，明細にレコード登録時、
                'かつ「仕入区分0:移動」以外かつ「取消区分1:取消」

#Region "入庫取消"

                '借方　買掛金
                Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("入庫日"), "yyyyMM") & "'" '入庫日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'買掛金'" '借方勘定  
                Sql += "," & UtilClass.formatNumber(formatDouble(calCost)) '仕入金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & getSupplierName(dsSWKNyukohd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("仕入日"), "yyyy-MM-dd") & "'" '仕入日
                Sql += ",''" '空でよし
                Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat)) & "'" '仕入金額 + VAT IN
                Sql += ",''" '空でよし
                Sql += ",''" '空でよし

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                '貸方　棚卸資産
                countKeyID = getCount(countKeyID)

                Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("入庫日"), "yyyyMM") & "'" '入庫日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'棚卸資産'" '貸方勘定　
                Sql += "," & UtilClass.formatNumber(formatDouble(-calCost)) '仕入金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & getSupplierName(dsSWKNyukohd.Tables(RS).Rows(0)("仕入先コード").ToString) & "'" '補助科目
                Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("仕入日"), "yyyy-MM-dd") & "'" '仕入日
                Sql += ",''" '空でよし
                Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat)) & "'" '仕入金額 + VAT IN
                Sql += ",''" '空でよし
                Sql += ",''" '空でよし

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                'VATがある場合
                If calVat <> 0 Then

                    '借方　買掛金
                    countKeyID = getCount(countKeyID)

                    Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("入庫日"), "yyyyMM") & "'" '入庫日
                    Sql += "," & seqID 'プライマリ
                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'買掛金'" '借方勘定
                    Sql += "," & UtilClass.formatNumber(formatDouble(calVat)) '仕入金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                    Sql += ",1" '固定
                    Sql += ",'" & getSupplierName(dsSWKNyukohd.Tables(RS).Rows(0)("仕入先コード").ToString) & "'" '補助科目
                    Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("仕入日"), "yyyy-MM-dd") & "'" '仕入日
                    Sql += ",''" '空でよし
                    Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat)) & "'" '仕入金額 + VAT IN
                    Sql += ",''" '空でよし
                    Sql += ",''" '空でよし

                    't67_swkhd データ登録
                    updateT67Swkhd(Sql)


                    '貸方　VAT-IN
                    countKeyID = getCount(countKeyID)

                    Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("入庫日"), "yyyyMM") & "'" '入庫日
                    Sql += "," & seqID 'プライマリ
                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'VAT-IN'" '貸方勘定
                    Sql += "," & UtilClass.formatNumber(formatDouble(-calVat)) '仕入金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                    Sql += ",1" '固定
                    Sql += ",'" & getSupplierName(dsSWKNyukohd.Tables(RS).Rows(0)("仕入先コード").ToString) & "'" '補助科目
                    Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("仕入日"), "yyyy-MM-dd") & "'" '仕入日
                    Sql += ",''" '空でよし
                    Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat)) & "'" '仕入金額 + VAT IN
                    Sql += ",''" '空でよし
                    Sql += ",''" '空でよし

                    't67_swkhd データ登録
                    updateT67Swkhd(Sql)

                End If

                countKeyID = getCount(countKeyID)
#End Region
            Else
                'todo:msgbox
                MsgBox("エラー")
            End If
        Next
#End Region


#Region "仕訳売掛金"

        't45_shukodt  出庫基本
        't31_urigdt
        't30_urighd
        Sql = "SELECT"
        Sql += " t45.出庫日,t45.仕入先名,t30.得意先名,t30.客先番号,t30.売上日,t45.仕入区分,t30.取消区分"
        Sql += ",sum(t30.売上金額) as 売上金額計,t30.ＶＡＴ,sum(t30.仕入金額) as 仕入金額計"

        Sql += " FROM public.t45_shukodt as t45 "
        Sql += " left join public.t30_urighd as t30"
        Sql += " on t45.受注番号 = t30.受注番号"

        Sql += " WHERE "
        Sql += " t45.会社コード"
        Sql += " ILIKE  "
        Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"

        'todo:t45_shukodt 条件
        '条件オプション
        'Sql = ""

        Sql += " and not(t45.仕入区分 is null)"

        Sql += " GROUP BY "
        Sql += " t45.出庫日,t45.仕入先名,t30.得意先名,t30.客先番号,t30.売上日,t45.仕入区分,t30.取消区分,t30.ＶＡＴ"

        Sql += " ORDER BY "
        Sql += " t30.売上日,t45.入庫日,t30.客先番号"


        Dim dsSwkUrighd As DataSet = _db.selectDB(Sql, RS, reccnt)

        For i As Integer = 0 To dsSwkUrighd.Tables(RS).Rows.Count - 1  't45_shukodt

            Dim countKeyID As Integer = 0

            upSeq() 'シーケンス更新
            seqID = getSeq("transactionid_seq")

            '売掛金, 売上 = 売上金額計 * (VAT / 100)
            Dim calVat As Decimal = dsSwkUrighd.Tables(RS).Rows(i)("売上金額計") * (dsSwkUrighd.Tables(RS).Rows(i)("VAT") / 100)
            Dim calCost As Decimal = dsSwkUrighd.Tables(RS).Rows(i)("売上金額計")

            '仕入金額（棚卸資産を減らすため）
            Dim calSiire = dsSwkUrighd.Tables(RS).Rows(i)("仕入金額計")


            If dsSwkUrighd.Tables(RS).Rows(i)("仕入区分") <> 0 _
                    And dsSwkUrighd.Tables(RS).Rows(i)("取消区分") = 0 Then
                '出庫入力に伴う売上伝票計上
                '※出庫Ｈ、明細にレコード登録時、
                'かつ仕入区分「0:移動」以外かつ取消区分「0:有効」
                '「1:受発注」「2:在庫引当」「3:サービス（役務）」

                'サービス（役務）販売による売上伝票計上
                '※出庫入力に伴う売上伝票計上に準ずる

#Region "出庫入力,サービス販売"

                '借方　仕入
                Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("出庫日"), "yyyyMM") & "'" '出庫日
                Sql += "," & "nextval('transactionid_seq')" 'todo:プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'仕入'" '借方勘定
                Sql += "," & UtilClass.formatNumber(formatDouble(calSiire)) '仕入金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & getSupplierName(dsSwkUrighd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                Sql += ",'ER-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("売上日"), "yyyy-MM-dd") & "'" '売上日
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(formatDouble(calCost + calVat + calSiire)) '仕入金額
                Sql += ",''" '空でよし
                Sql += ",''" '空でよし

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                '貸方  棚卸資産
                countKeyID = getCount(countKeyID)

                Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("出庫日"), "yyyyMM") & "'" '出庫日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'棚卸資産'"  '借方科目
                Sql += "," & UtilClass.formatNumber(formatDouble(-(calSiire))) '仕入金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & getSupplierName(dsSwkUrighd.Tables(RS).Rows(0)("仕入先コード").ToString) & "'" '補助科目
                Sql += ",'ER-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("売上日"), "yyyy-MM-dd") & "'" '売上日
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(formatDouble(calCost + calVat + calSiire)) '仕入金額
                Sql += ",''" '空でよし
                Sql += ",''" '空でよし

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                '借方　売掛金
                countKeyID = getCount(countKeyID)

                Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("出庫日"), "yyyyMM") & "'" '出庫日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'売掛金'" '借方勘定
                Sql += "," & UtilClass.formatNumber(formatDouble(calCost)) '売上金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & getCustomerName(dsSwkUrighd.Tables(RS).Rows(0)("得意先コード").ToString) & "'" '補助科目
                Sql += ",'ER-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("売上日"), "yyyy-MM-dd") & "'" '売上日
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(formatDouble(calCost + calVat + calSiire)) '売上金額 + VAT IN
                Sql += ",''" '空でよし
                Sql += ",''" '空でよし

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                '貸方  売上
                countKeyID = getCount(countKeyID)

                Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("出庫日"), "yyyyMM") & "'" '出庫日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'売上'" '貸方科目　
                Sql += "," & UtilClass.formatNumber(formatDouble(-(calCost))) '売上金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & getCustomerName(dsSwkUrighd.Tables(RS).Rows(0)("得意先コード").ToString) & "'" '補助科目
                Sql += ",'ER-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("売上日"), "yyyy-MM-dd") & "'" '売上日
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(formatDouble(calCost + calVat + calSiire)) '売上金額 + VAT IN
                Sql += ",''" '空でよし
                Sql += ",''" '空でよし

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                'VATがある場合
                If calVat <> 0 Then

                    '借方  売掛金
                    countKeyID = getCount(countKeyID)

                    Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("出庫日"), "yyyyMM") & "'" '出庫日
                    Sql += "," & seqID 'プライマリ
                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'売掛金'" '借方勘定　
                    Sql += "," & UtilClass.formatNumber(formatDouble(calVat)) 'VAT（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                    Sql += ",1" '固定
                    Sql += ",'" & getCustomerName(dsSwkUrighd.Tables(RS).Rows(i)("得意先コード").ToString) & "'" '補助科目
                    Sql += ",'ER-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("売上日"), "yyyy-MM-dd") & "'" '売上日
                    Sql += ",''" '空でよし
                    Sql += "," & UtilClass.formatNumber(formatDouble(calCost + calVat + calSiire)) '売上金額 + VAT IN + 仕入金額
                    Sql += ",''" '空でよし
                    Sql += ",''" '空でよし

                    't67_swkhd データ登録
                    updateT67Swkhd(Sql)


                    '貸方  VAT
                    countKeyID = getCount(countKeyID)

                    Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("出庫日"), "yyyyMM") & "'" '出庫日
                    Sql += "," & seqID 'プライマリ
                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'VAT-OUT'" '借方科目　
                    Sql += "," & UtilClass.formatNumber(formatDouble(-(calVat))) 'VAT（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                    Sql += ",1" '固定
                    Sql += ",'" & getCustomerName(dsSwkUrighd.Tables(RS).Rows(i)("得意先コード").ToString) & "'" '補助科目
                    Sql += ",'ER-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("売上日"), "yyyy-MM-dd") & "'" '売上日
                    Sql += ",''" '空でよし
                    Sql += "," & UtilClass.formatNumber(formatDouble(calCost + calVat + calSiire)) '売上金額 + VAT IN + 仕入金額
                    Sql += ",''" '空でよし
                    Sql += ",''" '空でよし

                    't67_swkhd データ登録
                    updateT67Swkhd(Sql)
                End If

#End Region

            ElseIf dsSwkUrighd.Tables(RS).Rows(i)("仕入区分") <> 0 _
                        And dsSwkUrighd.Tables(RS).Rows(i)("取消区分") = 1 Then
                '出庫取消に伴う売上返品計上
                '※出庫Ｈ，明細にレコード登録時、
                'かつ「仕入区分0:移動」以外かつ「取消区分1:取消」

#Region "出庫取消"

                '借方　棚卸資産
                seqID = getSeq("transactionid_seq")

                Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("出庫日"), "yyyyMM") & "'" '出庫日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'棚卸資産'" '借方勘定　
                Sql += "," & UtilClass.formatNumber(formatDouble(calSiire)) '仕入金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & getSupplierName(dsSwkUrighd.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                Sql += ",'ER-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("売上日"), "yyyy-MM-dd") & "'" '売上日
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(formatDouble(calCost + calVat + calSiire)) '仕入金額
                Sql += ",''" '空でよし
                Sql += ",''" '空でよし


                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                '貸方  仕入
                countKeyID = getCount(countKeyID)

                Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("出庫日"), "yyyyMM") & "'" '出庫日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'仕入'"  '貸方勘定
                Sql += "," & UtilClass.formatNumber(formatDouble(-(calSiire))) '仕入金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & getSupplierName(dsSwkUrighd.Tables(RS).Rows(0)("仕入先コード").ToString) & "'" '補助科目
                Sql += ",'ER-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("売上日"), "yyyy-MM-dd") & "'" '売上日
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(formatDouble(calCost + calVat + calSiire)) '仕入金額
                Sql += ",''" '空でよし
                Sql += ",''" '空でよし

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                '借方　売上
                countKeyID = getCount(countKeyID)

                Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("出庫日"), "yyyyMM") & "'" '出庫日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'売上'" '借方勘定  
                Sql += "," & UtilClass.formatNumber(formatDouble(calCost)) '売上金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & getCustomerName(dsSWKNyukohd.Tables(RS).Rows(i)("得意先コード").ToString) & "'"  '補助科目
                Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("仕入日"), "yyyy-MM-dd") & "'" '仕入日
                Sql += ",''" '空でよし
                Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat + calSiire)) & "'" '売上金額 + VAT IN
                Sql += ",''" '空でよし
                Sql += ",''" '空でよし

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                '貸方　売掛金
                countKeyID = getCount(countKeyID)

                Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("出庫日"), "yyyyMM") & "'" '出庫日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'売掛金'" '貸方勘定　
                Sql += "," & UtilClass.formatNumber(formatDouble(-calCost)) '売上金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & getCustomerName(dsSWKNyukohd.Tables(RS).Rows(0)("得意先コード").ToString) & "'"  '勘定科目
                Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("売上日"), "yyyy-MM-dd") & "'" '売上日
                Sql += ",''" '空でよし
                Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat + calSiire)) & "'" '仕入金額 + VAT IN
                Sql += ",''" '空でよし
                Sql += ",''" '空でよし

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                'VATがある場合
                If calVat <> 0 Then

                    '借方　VAT-OUT
                    countKeyID = getCount(countKeyID)

                    Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("入庫日"), "yyyyMM") & "'" '入庫日
                    Sql += "," & seqID 'プライマリ
                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'VAT-OUT'" '借方勘定
                    Sql += "," & UtilClass.formatNumber(formatDouble(calVat)) 'VAT（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                    Sql += ",1" '固定
                    Sql += ",'" & getCustomerName(dsSWKNyukohd.Tables(RS).Rows(0)("得意先コード").ToString) & "'"  '補助科目
                    Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("売上日"), "yyyy-MM-dd") & "'" '売上日
                    Sql += ",''" '空でよし
                    Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat + calSiire)) & "'"
                    Sql += ",''" '空でよし
                    Sql += ",''" '空でよし

                    't67_swkhd データ登録
                    updateT67Swkhd(Sql)


                    '貸方　売掛金
                    countKeyID = getCount(countKeyID)

                    Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("出庫日"), "yyyyMM") & "'" '出庫日
                    Sql += "," & seqID 'プライマリ
                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'売掛金'"  '貸方勘定
                    Sql += "," & UtilClass.formatNumber(formatDouble(-calVat)) 'VAT（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                    Sql += ",1" '固定
                    Sql += ",'" & getCustomerName(dsSWKNyukohd.Tables(RS).Rows(0)("得意先コード").ToString) & "'"  '補助科目
                    Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("仕入日"), "yyyy-MM-dd") & "'" '仕入日
                    Sql += ",''" '空でよし
                    Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat + calSiire)) & "'"
                    Sql += ",''" '空でよし
                    Sql += ",''" '空でよし

                    't67_swkhd データ登録
                    updateT67Swkhd(Sql)

                End If

                countKeyID = getCount(countKeyID)

#End Region

            Else
                'todo:msgbox
                MsgBox("エラー")

            End If


        Next
#End Region


#Region "仕訳前受金"

        't27_nkinkshihd  入金消込
        't26_nkindt
        Sql = "SELECT"
        Sql += " t27.入金日,t40.仕入先名,t40.客先番号,t40.仕入日,t41.仕入区分,t42.取消区分"
        Sql += ",sum(t42.仕入金額) as 仕入金額計,t42.ＶＡＴ"

        Sql += " FROM public.t27_nkinkshihd as t27 "
        Sql += " left join public.t41_siredt as t41"
        Sql += " on (t42.発注番号 = t41.発注番号 and t42.発注番号枝番 = t41.発注番号枝番)"

        Sql += " left join public.t40_sirehd as t40"
        Sql += " on t42.発注番号 = t40.発注番号"

        Sql += " WHERE "
        Sql += " t45.会社コード"
        Sql += " ILIKE  "
        Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"

        'todo:t27_nkinkshihd 条件
        '条件オプション
        'Sql = ""

        Sql += " and not(t41.仕入区分 is null)"

        Sql += " GROUP BY "
        Sql += " t42.入庫日,t40.仕入先名,t40.客先番号,t40.仕入日,t41.仕入区分,t42.取消区分,t42.ＶＡＴ"

        Sql += " ORDER BY "
        Sql += " t27.入金日,t42.入庫日,t40.客先番号"

        Dim dsNkinkshihd As DataSet = _db.selectDB(Sql, RS, reccnt)


        't27_nkinkshihd
        For i As Integer = 0 To dsNkinkshihd.Tables(RS).Rows.Count - 1

            Sql = " AND "
            Sql += "請求番号"
            Sql += " ILIKE "
            Sql += "'"
            Sql += dsNkinkshihd.Tables(RS).Rows(i)("請求番号")
            Sql += "'"

            Dim dsNkinSkyu As DataSet = getDsData("t23_skyuhd", Sql) '請求データの取得

            '------------------------------->> 共通化したい
            Sql = " AND "
            Sql += "得意先コード"
            Sql += " ILIKE "
            Sql += "'"
            Sql += dsNkinSkyu.Tables(0).Rows(0)("得意先コード")
            Sql += "'"

            'm10 得意先マスタ
            Dim dsCustomer As DataSet = getDsData("m10_customer", Sql) '得意先マスタデータの取得
            Console.WriteLine(dsCustomer.Tables(RS).Rows(0)("会計用得意先コード"))
            Dim codeAAC As String = dsCustomer.Tables(RS).Rows(0)("会計用得意先コード")
            '<<------------------------------- 共通化したい

            Dim transactionid As String = DateTime.Now.ToString("MMddHHmmss" & i) 'TRANSACTIONID
            Dim countKeyID As Integer = 0

            upSeq() 'シーケンス更新
            seqID = getSeq("transactionid_seq")

            '入金種別に対応した科目を取得
            Dim strKamoku As String = mGet_NyukinSyubetu(dsNkinkshihd.Tables(RS).Rows(0)("入金種別"))

            Dim Paymentfee As Decimal = 0  '支払手数料

            If dsNkinkshihd.Tables(RS).Rows(i)("入金種別") = 1 Then
                '振込の場合
                If dsNkinkshihd.Tables(RS).Rows(i + 1)("入金種別") = 2 Then
                    Paymentfee = dsNkinkshihd.Tables(RS).Rows(i + 1)("入金消込額計")
                End If
            End If

            If dsNkinkshihd.Tables(RS).Rows(0)("入金種別") = "9" Then  '相殺

#Region "入金相殺"
                '借方　買掛金
                Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'買掛金'" '借方科目
                Sql += "," & UtilClass.formatNumber(formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計"))) '入金金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & getSupplierName(dsNkinSkyu.Tables(RS).Rows(0)("仕入先コード").ToString) & "'" '補助科目
                Sql += ",'PM-" & dsNkinSkyu.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計"))) '入金金額
                Sql += ",'" & codeAAC & "'" '会計用得意先コード
                Sql += ",''" '空でよし

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                '貸方　売掛金
                countKeyID = getCount(countKeyID)

                Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'売掛金'" '貸方科目
                Sql += "," & UtilClass.formatNumber(formatDouble(-dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計"))) '入金金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & getCustomerName(dsNkinSkyu.Tables(RS).Rows(0)("得意先コード").ToString) & "'" '補助科目
                Sql += ",'PM-" & dsNkinSkyu.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計"))) '入金金額
                Sql += ",'" & codeAAC & "'" '会計用得意先コード
                Sql += ",''" '空でよし

                countKeyID = getCount(countKeyID)

                't67_swkhd データ登録
                updateT67Swkhd(Sql)

#End Region

            ElseIf dsNkinSkyu.Tables(RS).Rows(0)("請求区分") = "1" Then  '前受請求の場合

                '前受金のみ入金（現金入金）		　　　　　　　  　　現金				前受金	得意先
                '※入金ヘッダ,明細にレコード登録時、							
                'かつ入金番号に一致する請求Rが存在し、							
                'かつ請求R.請求区分が「1:前受金」の場合、							
                '「入金明細入金区分」に応じて伝票を発生させる
                'ここでは「区分3:現金」を例としている
                '    受注F-(受注番号) - 請求F - (入金番号) - 入金F            

#Region "前受請求"

                '借方　入金種別
                Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'" & strKamoku & "'" '貸方科目
                Sql += "," & UtilClass.formatNumber(formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計") - Paymentfee)) '入金金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & getCustomerName(dsNkinSkyu.Tables(RS).Rows(0)("得意先コード").ToString) & "'" '補助科目
                Sql += ",'PM-" & dsNkinSkyu.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計"))) '入金金額
                Sql += ",'" & codeAAC & "'" '会計用得意先コード
                Sql += ",''" '空でよし


                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                '支払手数料があれば
                If Paymentfee <> 0 Then

                    '借方　支払手数料
                    countKeyID = getCount(countKeyID)

                    Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
                    Sql += "," & seqID 'プライマリ
                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'支払手数料'" '支払手数料
                    Sql += "," & UtilClass.formatNumber(formatDouble(Paymentfee)) '支払手数料（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                    Sql += ",1" '固定
                    Sql += ",'" & getCustomerName(dsNkinSkyu.Tables(RS).Rows(0)("得意先コード").ToString) & "'" '補助科目
                    Sql += ",'PM-" & dsNkinSkyu.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
                    Sql += ",''" '空でよし
                    Sql += "," & UtilClass.formatNumber(formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計")))  '入金金額
                    Sql += ",'" & codeAAC & "'" '会計用得意先コード
                    Sql += ",''" '空でよし

                    countKeyID = getCount(countKeyID)

                    't67_swkhd データ登録
                    updateT67Swkhd(Sql)
                End If


                '貸方　前受金
                countKeyID = getCount(countKeyID)

                Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                'Sql += ",'" & getAccountName("advance") & "'" '前受金
                Sql += ",'前受金'" '前受金
                Sql += "," & UtilClass.formatNumber(formatDouble(-dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計"))) '入金金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & getCustomerName(dsNkinSkyu.Tables(RS).Rows(0)("得意先コード").ToString) & "'" '補助科目
                Sql += ",'PM-" & dsNkinSkyu.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計"))) '入金金額
                Sql += ",'" & codeAAC & "'" '会計用得意先コード
                Sql += ",''" '空でよし

                countKeyID = getCount(countKeyID)

                't67_swkhd データ登録
                updateT67Swkhd(Sql)

#End Region

            ElseIf dsNkinSkyu.Tables(RS).Rows(0)("請求区分") = "2" Then  '通常請求の場合

                '前受金入金に対する振込入金処理                   普通預金				売掛金	得意先
                '（例：普通預金）		前受金					
                '※入金ヘッダ，明細にレコード登録時、							
                '　かつ入金番号に一致する請求Ｒが存在し、							
                '　かつ請求R.請求区分が「2:通常請求」の場合、							
                '　かつ請求R.受注番号と同一受注番号を持つ
                '            請求R.請求区分が「1:前受金」であるレコードが
                '            存在する場合、							
                '入金明細入金区分に応じて伝票を発生させる
                'ここでは「区分1:振込入金」を例としている

#Region "通常請求"

                Dim FormerGold As Decimal = 0  '前受金

                'select 入金消込額計 from t23_skyuhd join t27_nkinkshihd 
                'where 受注番号 And 請求区分 = 1 And 締日 

                Sql = "SELECT"
                Sql += " t27.入金消込額計"
                Sql += " FROM public.t23_skyuhd as t23 "
                Sql += " left join public.t27_nkinkshihd as t27"
                Sql += " on t23.請求番号 = t27.請求番号"

                Sql += " WHERE "
                Sql += " t45.会社コード"
                Sql += " ILIKE  "
                Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"

                'todo:t23_skyuhd 条件
                '条件オプション
                'Sql = ""

                Sql += " and t23.受注番号 = '" & dsNkinSkyu.Tables(RS).Rows(0)("受注番号") & "'"
                Sql += " and t23.請求区分 = 1"
                'Sql += " and t23.請求番号 = '" & dsNkinkshihd.Tables(RS).Rows(i)("請求番号") & "'"

                'Sql += " GROUP BY "
                'Sql += " t27"

                'Sql += " ORDER BY "
                'Sql += " t27.入金日,t42.入庫日,t40.客先番号"


                Dim dsNkinSkyu2 As DataSet = _db.selectDB(Sql, RS, reccnt)  '他の請求データの取得

                '前受金のデータがあれば
                If dsNkinSkyu2.Tables(RS).Rows.Count > 0 Then

                    FormerGold = dsNkinSkyu2.Tables(RS).Rows(0)("入金消込額計")
                End If


                '借方  入金種別
                Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                'Sql += ",'" & getAccountName("cash-deposit") & "'" '現金預金
                Sql += ",'" & strKamoku & "'"  '貸方科目
                Sql += "," & UtilClass.formatNumber(formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計") - Paymentfee - FormerGold)) '入金金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & getCustomerName(dsNkinSkyu.Tables(RS).Rows(0)("得意先コード").ToString) & "'" '補助科目
                Sql += ",'PM-" & dsNkinSkyu.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計"))) '入金金額
                Sql += ",'" & codeAAC & "'" '会計用得意先コード
                Sql += ",''" '空でよし

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                '支払手数料があれば
                If Paymentfee <> 0 Then

                    '借方　支払手数料
                    countKeyID = getCount(countKeyID)

                    Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
                    Sql += "," & seqID 'プライマリ
                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'支払手数料'" '支払手数料
                    Sql += "," & UtilClass.formatNumber(formatDouble(Paymentfee)) '支払手数料（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                    Sql += ",1" '固定
                    Sql += ",'" & getCustomerName(dsNkinSkyu.Tables(RS).Rows(0)("得意先コード").ToString) & "'" '補助科目
                    Sql += ",'PM-" & dsNkinSkyu.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
                    Sql += ",''" '空でよし
                    Sql += "," & UtilClass.formatNumber(formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計"))) '入金金額
                    Sql += ",'" & codeAAC & "'" '会計用得意先コード
                    Sql += ",''" '空でよし

                    countKeyID = getCount(countKeyID)

                    't67_swkhd データ登録
                    updateT67Swkhd(Sql)
                End If


                '前受金があれば
                If FormerGold > 0 Then

                    '借方　前受金
                    countKeyID = getCount(countKeyID)

                    Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
                    Sql += "," & seqID 'プライマリ
                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'前受金'" '前受金
                    Sql += "," & UtilClass.formatNumber(formatDouble(FormerGold)) '入金金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                    Sql += ",1" '固定
                    Sql += ",'" & getCustomerName(dsNkinSkyu.Tables(RS).Rows(0)("得意先コード").ToString) & "'" '補助科目
                    Sql += ",'PM-" & dsNkinSkyu.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
                    Sql += ",''" '空でよし
                    Sql += "," & UtilClass.formatNumber(formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計"))) '入金金額
                    Sql += ",'" & codeAAC & "'" '会計用得意先コード
                    Sql += ",''" '空でよし


                    't67_swkhd データ登録
                    updateT67Swkhd(Sql)

                End If


                '貸方　売掛金
                countKeyID = getCount(countKeyID)

                Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'売掛金'" '売掛金
                Sql += "," & UtilClass.formatNumber(formatDouble(-dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計"))) '入金金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & getCustomerName(dsNkinSkyu.Tables(RS).Rows(0)("得意先コード").ToString) & "'" '補助科目
                Sql += ",'PM-" & dsNkinSkyu.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計"))) '入金金額
                Sql += ",'" & codeAAC & "'" '会計用得意先コード
                Sql += ",''" '空でよし

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


#End Region

            Else
                'todo:msgbox
                MsgBox("エラー")
            End If

            If Paymentfee <> 0 Then  '支払手数料があれば
                i = i + 1
            End If

        Next
#End Region


#Region "仕訳前払金"

        't49_shrikshihd  支払消込
        '
        '
        Sql = "SELECT"
        Sql += " t42.入庫日,t40.仕入先名,t40.客先番号,t40.仕入日,t41.仕入区分,t42.取消区分"
        Sql += ",sum(t42.仕入金額) as 仕入金額計,t42.ＶＡＴ"
        Sql += " FROM public.t49_shrikshihd as t49 "
        Sql += " left join public.t41_siredt as t41"
        Sql += " on (t42.発注番号 = t41.発注番号 and t42.発注番号枝番 = t41.発注番号枝番)"

        Sql += " left join public.t40_sirehd as t40"
        Sql += " on t42.発注番号 = t40.発注番号"

        Sql += " WHERE "
        Sql += " t45.会社コード"
        Sql += " ILIKE  "
        Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"

        'todo:t49_shrikshihd 条件
        '条件オプション
        'Sql = ""

        Sql += " and not(t41.仕入区分 is null)"

        Sql += " GROUP BY "
        Sql += " t42.入庫日,t40.仕入先名,t40.客先番号,t40.仕入日,t41.仕入区分,t42.取消区分,t42.ＶＡＴ"

        Sql += " ORDER BY "
        Sql += " t49.支払日,t42.入庫日,t40.客先番号"


        Dim dsShrikshihd As DataSet = _db.selectDB(Sql, RS, reccnt)

        For i As Integer = 0 To dsShrikshihd.Tables(RS).Rows.Count - 1  't49_shrikshihd

            Sql = " AND "
            Sql += "買掛番号"
            Sql += " ILIKE "
            Sql += "'"
            Sql += dsShrikshihd.Tables(RS).Rows(i)("買掛番号")
            Sql += "'"

            Dim dsShriKike As DataSet = getDsData("t46_kikehd", Sql) '買掛データの取得

            '------------------------------->> 共通化したい
            Sql = " AND "
            Sql += "仕入先コード"
            Sql += " ILIKE "
            Sql += "'"
            Sql += dsShriKike.Tables(0).Rows(0)("仕入先コード")
            Sql += "'"

            'm10 得意先マスタ
            Dim dsSupplier As DataSet = getDsData("m11_supplier", Sql) '得意先マスタデータの取得

            Dim codeAAC As String = dsSupplier.Tables(RS).Rows(0)("会計用仕入先コード")
            '<<------------------------------- 共通化したい

            Dim countKeyID As Integer = 0

            upSeq() 'シーケンス更新
            seqID = getSeq("transactionid_seq")

            '支払種別に対応した科目を取得
            Dim strKamoku As String = mGet_NyukinSyubetu(dsShrikshihd.Tables(RS).Rows(0)("支払種別"))

            Dim Paymentfee As Decimal = 0  '支払手数料

            If dsShrikshihd.Tables(RS).Rows(i)("支払種別") = 1 Then
                '振込の場合
                If dsShrikshihd.Tables(RS).Rows(i + 1)("支払種別") = 2 Then
                    Paymentfee = dsShrikshihd.Tables(RS).Rows(i + 1)("支払消込額計")
                End If
            End If


            If dsNkinkshihd.Tables(RS).Rows(0)("支払種別") = "9" Then  '相殺

#Region "支払相殺"

                '借方　買掛金
                Sql = ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'"
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'買掛金'" '借方勘定　
                Sql += "," & UtilClass.formatNumber(formatDouble(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計"))) '支払金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & dsShriKike.Tables(RS).Rows(0)("仕入先コード").ToString & "'" '補助科目
                Sql += ",'PO-" & dsShriKike.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'"
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(formatDouble(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計"))) '支払金額
                Sql += ",'" & codeAAC & "'" '会計用支払先コード
                Sql += ",''" '空でよし

                updateT67Swkhd(Sql) 'update実行


                '貸方　売掛金
                countKeyID = getCount(countKeyID) '0～カウントアップ

                Sql = ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'"
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'売掛金'"  '借方科目
                Sql += "," & UtilClass.formatNumber(formatDouble(-dsShrikshihd.Tables(RS).Rows(i)("支払消込額計"))) '支払金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & dsShriKike.Tables(RS).Rows(0)("仕入先コード").ToString & "'" '補助科目
                Sql += ",'PO-" & dsShriKike.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'"
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(formatDouble(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計"))) '支払金額
                Sql += ",'" & codeAAC & "'" '会計用支払先コード
                Sql += ",''" '空でよし

                updateT67Swkhd(Sql) 'update実行

#End Region

            ElseIf dsShriKike.Tables(RS).Rows(0)("買掛区分") = "1" Then  '前払買掛
                '前払金の支払（現金支払）		　　　　　前払金				現金	仕入先
                '※支払ヘッダ,明細にレコード登録時、							
                '　かつ支払番号に一致する消込Ｒが存在し、							
                '　消込Ｒ.買掛番号に一致する買掛Rが存在し、							
                '　かつ買掛R.買掛区分が「2:前払金」の場合、							
                '　支払明細支払種別に応じて伝票を発生させる
                'ここでは「区分3:現金」を例としている
                '  発注F-(発注番号) - 買掛F - (買掛番号) - 消込F - (支払番号) - 支払F                            


#Region "前払買掛"

                '借方　前払金
                Sql = ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'"
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'前払金'" '前払金　
                Sql += "," & UtilClass.formatNumber(formatDouble(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計"))) '支払金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & dsShriKike.Tables(RS).Rows(0)("仕入先コード").ToString & "'" '仕入先コード
                Sql += ",'PO-" & dsShriKike.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'" '入金日
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(formatDouble(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計"))) '支払金額
                Sql += ",'" & codeAAC & "'" '会計用支払先コード
                Sql += ",''" '空でよし

                updateT67Swkhd(Sql) 'update実行


                '貸方　支払種別
                countKeyID = getCount(countKeyID) '0～カウントアップ

                Sql = ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'"
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'" & strKamoku & "'"  '借方科目
                Sql += "," & UtilClass.formatNumber(formatDouble(-dsShrikshihd.Tables(RS).Rows(i)("支払消込額計") + Paymentfee)) '支払金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & dsShriKike.Tables(RS).Rows(0)("仕入先コード").ToString & "'" '仕入先コード
                Sql += ",'PO-" & dsShriKike.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'"
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(formatDouble(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計"))) '支払金額
                Sql += ",'" & codeAAC & "'" '会計用支払先コード
                Sql += ",''" '空でよし

                updateT67Swkhd(Sql) 'update実行


                If Paymentfee <> 0 Then  '支払手数料があれば

                    '貸方　支払手数料
                    countKeyID = getCount(countKeyID)

                    Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'"
                    Sql += "," & seqID 'プライマリ
                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'支払手数料'" '支払手数料
                    Sql += "," & UtilClass.formatNumber(formatDouble(-Paymentfee)) '支払手数料（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                    Sql += ",1" '固定
                    Sql += ",'" & dsShriKike.Tables(RS).Rows(0)("仕入先コード").ToString & "'" '仕入先コード
                    Sql += ",'PM-" & dsShriKike.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'"
                    Sql += ",''" '空でよし
                    Sql += "," & UtilClass.formatNumber(formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("支払消込額計"))) '支払金額
                    Sql += ",'" & codeAAC & "'" '会計用得意先コード
                    Sql += ",''" '空でよし

                    countKeyID = getCount(countKeyID)

                    't67_swkhd データ登録
                    updateT67Swkhd(Sql)
                End If

#End Region

            ElseIf dsShriKike.Tables(RS).Rows(0)("買掛区分") = "2" Then  '通常買掛
                '前払金支払に対する振込支払精算（普通預金）     買掛金	仕入先			普通預金	
                '							　　　　　　　　　　　　　　　　　　　　　　前払金	
                '※支払ヘッダ，明細にレコード登録時、							
                '　かつ支払番号に一致する消込Ｒが存在し、							
                '　消込R.買掛番号に一致する買掛Ｒが存在し、							
                '　かつ買掛R.買掛区分が1 : 通常支払の場合、							
                '　かつ買掛R.発注番号と同一発注番号を持つ
                '  買掛R.買掛区分が2 : 前受金であるレコードが存在する場合、							
                '  支払明細支払区分に応じて伝票を発生させる
                '  ここでは「区分1:振込入金」を例としている

#Region "通常買掛"


                Dim DownPayment As Decimal = 0  '前支金

                'select 支払消込額計 from t46_kikehd join t49_shrikshihd 
                'where 発注番号 And 買掛区分 = 1 And 締日 

                Sql = "SELECT"
                Sql += " t49.支払消込額計"
                Sql += " FROM public.t46_kikehd as t46 "
                Sql += " left join public.t49_shrikshihd as t49"
                Sql += " on t46.買掛番号 = t49.買掛番号"

                Sql += " WHERE "
                Sql += " t46.会社コード"
                Sql += " ILIKE  "
                Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"

                'todo:t46_kikehd 条件
                '条件オプション
                'Sql = ""

                Sql += " and t46.発注番号 = '" & dsShriKike.Tables(RS).Rows(0)("発注番号") & "'"
                Sql += " and t46.買掛区分 = 1"
                'Sql += " and t23.買掛番号 = '" & dsNkinkshihd.Tables(RS).Rows(i)("買掛番号") & "'"

                'Sql += " GROUP BY "
                'Sql += " t27"

                'Sql += " ORDER BY "
                'Sql += " t27.入金日,t42.入庫日,t40.客先番号"


                Dim dsShriKike2 As DataSet = _db.selectDB(Sql, RS, reccnt)  '他の買掛データの取得

                '前払金のデータがあれば
                If dsShriKike2.Tables(RS).Rows.Count > 0 Then

                    DownPayment = dsShriKike2.Tables(RS).Rows(0)("支払消込額計")
                End If


                '借方 買掛金
                Sql = ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'" '
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'買掛金'" '買掛金　
                Sql += "," & UtilClass.formatNumber(formatDouble(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計"))) '支払金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & dsShriKike.Tables(RS).Rows(0)("仕入先コード").ToString & "'" '仕入先コード
                Sql += ",'PO-" & dsShriKike.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'"
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(formatDouble(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計"))) '支払金額
                Sql += ",'" & codeAAC & "'" '会計用支払先コード
                Sql += ",''" '空でよし

                updateT67Swkhd(Sql) 'update実行


                '貸方　支払種別
                countKeyID = getCount(countKeyID) '0～カウントアップ

                Sql = ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'"
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'" & strKamoku & "'"  '借方科目
                Sql += "," & UtilClass.formatNumber(formatDouble(-dsShrikshihd.Tables(RS).Rows(i)("支払消込額計") + Paymentfee + DownPayment)) '支払金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",'" & dsShriKike.Tables(RS).Rows(0)("仕入先コード").ToString & "'" '仕入先コード
                Sql += ",'PO-" & dsShriKike.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'"
                Sql += ",''" '空でよし
                Sql += "," & UtilClass.formatNumber(formatDouble(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計"))) '支払金額
                Sql += ",'" & codeAAC & "'" '会計用支払先コード
                Sql += ",''" '空でよし

                updateT67Swkhd(Sql) 'update実行


                '支払手数料があれば
                If Paymentfee <> 0 Then

                    '貸方　支払手数料
                    countKeyID = getCount(countKeyID)

                    Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'"
                    Sql += "," & seqID 'プライマリ
                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'支払手数料'" '支払手数料
                    Sql += "," & UtilClass.formatNumber(formatDouble(-Paymentfee)) '支払手数料（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                    Sql += ",1" '固定
                    Sql += ",'" & dsShriKike.Tables(RS).Rows(0)("仕入先コード").ToString & "'" '仕入先コード
                    Sql += ",'PM-" & dsShriKike.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'"
                    Sql += ",''" '空でよし
                    Sql += "," & UtilClass.formatNumber(formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("支払消込額計"))) '支払金額
                    Sql += ",'" & codeAAC & "'" '会計用得意先コード
                    Sql += ",''" '空でよし

                    countKeyID = getCount(countKeyID)

                    't67_swkhd データ登録
                    updateT67Swkhd(Sql)
                End If


                '前払金があれば
                If DownPayment > 0 Then

                    '貸方　前払金
                    countKeyID = getCount(countKeyID) '0～カウントアップ

                    Sql = ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'"
                    Sql += "," & seqID 'プライマリ
                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'前払金'"  '借方科目
                    Sql += "," & UtilClass.formatNumber(formatDouble(DownPayment)) '支払金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                    Sql += ",1" '固定
                    Sql += ",'" & dsShriKike.Tables(RS).Rows(0)("仕入先コード").ToString & "'" '仕入先コード
                    Sql += ",'PO-" & dsShriKike.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'"
                    Sql += ",''" '空でよし
                    Sql += "," & UtilClass.formatNumber(formatDouble(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計"))) '支払金額
                    Sql += ",'" & codeAAC & "'" '会計用支払先コード
                    Sql += ",''" '空でよし

                    updateT67Swkhd(Sql) 'update実行

                End If

#End Region

            Else
                'todo:msgbox テスト
                MsgBox("エラー")

            End If

            If Paymentfee <> 0 Then  '支払手数料があれば
                i = i + 1
            End If

        Next
#End Region



#Region "入出庫"
        Sql = "SELECT"
        Sql += " t42.入庫日,t40.仕入先名,t40.客先番号,t40.仕入日,t41.仕入区分,t42.取消区分"
        Sql += ",sum(t42.仕入金額) as 仕入金額計,t42.ＶＡＴ"

        Sql += " FROM public.t70_inout as t42 "
        Sql += " left join public.t41_siredt as t41"
        Sql += " on (t42.発注番号 = t41.発注番号 and t42.発注番号枝番 = t41.発注番号枝番)"

        Sql += " left join public.t40_sirehd as t40"
        Sql += " on t42.発注番号 = t40.発注番号"

        Sql += " WHERE "
        Sql += " t42.会社コード"
        Sql += " ILIKE  "
        Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"

        'todo:t42_nyukohd 条件
        '条件オプション
        'Sql = ""

        Sql += " and not(t41.仕入区分 is null)"

        Sql += " GROUP BY "
        Sql += " t42.入庫日,t40.仕入先名,t40.客先番号,t40.仕入日,t41.仕入区分,t42.取消区分,t42.ＶＡＴ"

        Sql += " ORDER BY "
        Sql += " t40.仕入日,t42.入庫日,t40.客先番号"

        Dim dsNyusyukko As DataSet = _db.selectDB(Sql, RS, reccnt)


        For i As Integer = 0 To dsSWKNyukohd.Tables(RS).Rows.Count - 1  't42_nyukohd

            Dim countKeyID As Integer = 0

            upSeq() 'シーケンス更新
            seqID = getSeq("transactionid_seq")
            Console.WriteLine(seqID)


            If dsNyusyukko.Tables(RS).Rows(i)("入出庫区分") = 1 _
                And （dsNyusyukko.Tables(RS).Rows(i)("入出庫種別") = 0 Or dsNyusyukko.Tables(RS).Rows(i)("入出庫種別") = 1) _
                And dsNyusyukko.Tables(RS).Rows(i)("仕入区分") = 0 Then

                '期中の実地棚卸等に基づく在庫増     　　　　　商品				雑収入	
                '※入出庫Fにレコード登録時、							
                '　かつ入出庫区分「1: 入庫」、							
                '　かつ入出庫種別「0:通常」 or「1:サンプル」、							
                '　かつ仕入区分「0:移動」のレコードである場合、伝票を発生させる							
                '入庫情報として伝票番号、行番号に一致する入庫明細を参照する

#Region "在庫増"

                '借方　商品
                Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("入出庫日"), "yyyyMM") & "'"
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                'Sql += ",'" & getAccountName("inventory") & "'" '棚卸資産
                Sql += ",'商品'"
                Sql += "," & UtilClass.formatNumber(formatDouble(1)) '仕入値 * 数量（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",''" '補助科目
                Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("入出庫日"), "yyyy-MM-dd") & "'"
                Sql += ",''" '空でよし
                Sql += ",'" & UtilClass.formatNumber(formatDouble(1)) & "'" '仕入金額 + VAT IN
                Sql += ",''" '空でよし
                Sql += ",''" '空でよし

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                '貸方　雑収入
                countKeyID = getCount(countKeyID)

                Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("入出庫日"), "yyyyMM") & "'"
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                'Sql += ",'" & getAccountName("inventory") & "'" '棚卸資産
                Sql += ",'雑収入'"
                Sql += "," & UtilClass.formatNumber(formatDouble(1)) '仕入値 * 数量（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",''" '補助科目
                Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("入出庫日"), "yyyy-MM-dd") & "'"
                Sql += ",''" '空でよし
                Sql += ",'" & UtilClass.formatNumber(formatDouble(1)) & "'" '仕入金額 + VAT IN
                Sql += ",''" '空でよし
                Sql += ",''" '空でよし

                't67_swkhd データ登録
                updateT67Swkhd(Sql)

#End Region

            ElseIf dsNyusyukko.Tables(RS).Rows(i)("入出庫区分") = 1 _
                And （dsNyusyukko.Tables(RS).Rows(i)("入出庫種別") = 2 Or dsNyusyukko.Tables(RS).Rows(i)("入出庫種別") = 3) _
                And dsNyusyukko.Tables(RS).Rows(i)("仕入区分") = 0 Then

                '期中の実地棚卸等に基づく在庫減  　　　　　   棚卸減耗損				期末商品棚卸高	
                '※入出庫Fにレコード登録時、							
                '　かつ入出庫区分「1:入庫」、							
                '　かつ入出庫種別「2:廃棄」、							
                '　かつ仕入区分「0:移動」のレコードである場合、伝票を発生させる							
                '入庫情報として伝票番号、行番号に一致する入庫明細を参照する

                '期末の実地棚卸等に基づく在庫減     　　　　　棚卸減耗損				期末商品棚卸高	
                '※入出庫Fにレコード登録時、							
                '　かつ入出庫区分「1:入庫」、							
                '　かつ入出庫種別「3:棚卸ロス」、							
                '　かつ仕入区分「0:移動」のレコードである場合、伝票を発生させる							
                '入庫情報として伝票番号、行番号に一致する入庫明細を参照する


#Region "在庫減,棚卸ロス"

                '借方　棚卸減耗損
                Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("入出庫日"), "yyyyMM") & "'"
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'棚卸減耗損'"
                Sql += "," & UtilClass.formatNumber(formatDouble(1)) '仕入値 * 数量（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",''" '補助科目
                Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("入出庫日"), "yyyy-MM-dd") & "'"
                Sql += ",''" '空でよし
                Sql += ",'" & UtilClass.formatNumber(formatDouble(1)) & "'" '仕入金額 + VAT IN
                Sql += ",''" '空でよし
                Sql += ",''" '空でよし

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                '貸方　期末商品棚卸高
                countKeyID = getCount(countKeyID)

                Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("入出庫日"), "yyyyMM") & "'"
                Sql += "," & seqID 'プライマリ
                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
                'Sql += ",'" & getAccountName("inventory") & "'" '棚卸資産
                Sql += ",'期末商品棚卸高'"
                Sql += "," & UtilClass.formatNumber(formatDouble(1)) '仕入値 * 数量（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
                Sql += ",1" '固定
                Sql += ",''" '補助科目
                Sql += ",'WH-" & dsSWKNyukohd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(i)("入出庫日"), "yyyy-MM-dd") & "'"
                Sql += ",''" '空でよし
                Sql += ",'" & UtilClass.formatNumber(formatDouble(1)) & "'"
                Sql += ",''" '空でよし
                Sql += ",''" '空でよし

                't67_swkhd データ登録
                updateT67Swkhd(Sql)

#End Region

            Else
                'todo:msgbox テスト
                MsgBox("エラー")

            End If

        Next

#End Region  '入出庫

    End Sub

    '    '仕訳データのXML出力     bk
    '    Private Sub getShiwakeData()
    '        Dim dtToday As DateTime = DateTime.Now '年月の設定
    '        Dim reccnt As Integer = 0 'DB用（デフォルト）
    '        Dim Sql As String = "" 'SQL文用

    '        Dim dsCompany As DataSet = getDsData("m01_company") 'ログイン情報から会社データの取得


    '        'todo:仕分

    '#Region "仕訳買掛金"

    '        '条件オプション
    '        'Sql = ""
    '        Sql = " ORDER BY "
    '        Sql += " 仕入日 "

    '        'todo:t40_sirehd 条件?　グルーピング？

    '        Dim dsSwkSirehd As DataSet = getDsData("t40_sirehd", Sql) '仕入データの取得
    '        Dim seqID As Integer 'TRANSACTIONID用変数

    '        For i As Integer = 0 To dsSwkSirehd.Tables(RS).Rows.Count - 1  't40_sirehd
    '            '条件オプション
    '            'Sql = ""
    '            Sql = " AND "
    '            Sql += "発注番号"
    '            Sql += " ILIKE "
    '            Sql += "'"
    '            Sql += dsSwkSirehd.Tables(RS).Rows(i)("発注番号")
    '            Sql += "'"

    '            't20 発注基本
    '            Dim dsSwkHattyu As DataSet = getDsData("t20_hattyu", Sql) '発注基本データの取得

    '            't42 入庫基本
    '            Dim dsSWKNyukohd As DataSet = getDsData("t42_nyukohd", Sql) '入庫基本データの取得


    '            '入庫データ回しながら以下データ作成
    '            '借方：棚卸資産
    '            '貸方：買掛金

    '            Dim countKeyID As Integer = 0

    '            upSeq() 'シーケンス更新
    '            seqID = getSeq("transactionid_seq")
    '            Console.WriteLine(seqID)

    '            For x As Integer = 0 To dsSWKNyukohd.Tables(RS).Rows.Count - 1  't42_nyukohd

    '                '棚卸資産, 買掛金 = 仕入金額 * (VAT / 100)
    '                Dim calCost As Decimal = dsSWKNyukohd.Tables(RS).Rows(x)("仕入金額") * (dsSWKNyukohd.Tables(RS).Rows(x)("VAT") / 100)
    '                'VAT-IN, 買掛金 = calCost * (VAT / 100)
    '                Dim calVat As Decimal = calCost * (dsSWKNyukohd.Tables(RS).Rows(x)("VAT") / 100)


    '                If dsSwkSirehd.Tables(RS).Rows(i)("仕入区分") = 0 _
    '                    And dsSWKNyukohd.Tables(RS).Rows(i)("取消区分") = 0 Then
    '                    '入庫入力に伴う仕入伝票計上
    '                    '※入庫Ｈ，明細にレコード登録時、
    '                    'かつ「仕入区分0:移動以外」、「取消区分0:有効」
    '#Region "入庫入力"

    '                    Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(x)("入庫日"), "yyyyMM") & "'" '入庫日
    '                    Sql += "," & seqID 'プライマリ
    '                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
    '                    'Sql += ",'" & getAccountName("inventory") & "'" '棚卸資産
    '                    Sql += ",'棚卸資産'" '棚卸資産  テスト用
    '                    Sql += "," & formatDouble(calCost) '仕入金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
    '                    Sql += ",1" '固定
    '                    Sql += ",'" & dsSwkHattyu.Tables(RS).Rows(0)("仕入先名").ToString & "'" '仕入先コード
    '                    Sql += ",'WH-" & dsSwkSirehd.Tables(RS).Rows(x)("客先番号").ToString & "-" & i & "'" 'PO
    '                    Sql += ",'" & Format(dsSwkSirehd.Tables(RS).Rows(i)("仕入日"), "yyyy-MM-dd") & "'" '仕入日
    '                    Sql += ",''" '空でよし
    '                    Sql += ",'" & formatDouble(calCost + calVat) & "'" '仕入金額 + VAT IN
    '                    Sql += ",''" '空でよし
    '                    Sql += ",''" '空でよし

    '                    countKeyID = getCount(countKeyID)

    '                    '金額がゼロの時は登録しない
    '                    If calCost <> 0 Then
    '                        't67_swkhd データ登録
    '                        updateT67Swkhd(Sql)
    '                    End If

    '                    Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(x)("入庫日"), "yyyyMM") & "'" '入庫日
    '                    Sql += "," & seqID 'プライマリ
    '                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
    '                    'Sql += ",'" & getAccountName("accounts-payable") & "'" '買掛金
    '                    Sql += ",'買掛金'" '買掛金　テスト用
    '                    Sql += "," & formatDouble(-calCost) '仕入金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
    '                    Sql += ",1" '固定
    '                    Sql += ",'" & dsSwkHattyu.Tables(RS).Rows(0)("仕入先名").ToString & "'" '仕入先コード
    '                    Sql += ",'WH-" & dsSwkSirehd.Tables(RS).Rows(x)("客先番号").ToString & "-" & i & "'" 'PO
    '                    Sql += ",'" & Format(dsSwkSirehd.Tables(RS).Rows(i)("仕入日"), "yyyy-MM-dd") & "'" '仕入日
    '                    Sql += ",''" '空でよし
    '                    Sql += ",'" & formatDouble(calCost + calVat) & "'" '仕入金額 + VAT IN
    '                    Sql += ",''" '空でよし
    '                    Sql += ",''" '空でよし

    '                    '金額がゼロの時は登録しない
    '                    If calCost <> 0 Then
    '                        't67_swkhd データ登録
    '                        updateT67Swkhd(Sql)
    '                    End If

    '                    countKeyID = getCount(countKeyID)

    '                    Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(x)("入庫日"), "yyyyMM") & "'" '入庫日
    '                    Sql += "," & seqID 'プライマリ
    '                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
    '                    'Sql += ",'" & getAccountName("vat-in") & "'" 'VAT-IN 
    '                    Sql += ",'VAT-IN'" 'VAT-IN  テスト用
    '                    Sql += "," & formatDouble(calVat) '仕入金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
    '                    Sql += ",1" '固定
    '                    Sql += ",'" & dsSwkHattyu.Tables(RS).Rows(0)("仕入先名").ToString & "'" '仕入先コード
    '                    Sql += ",'WH-" & dsSwkSirehd.Tables(RS).Rows(x)("客先番号").ToString & "-" & i & "'" 'PO
    '                    Sql += ",'" & Format(dsSwkSirehd.Tables(RS).Rows(i)("仕入日"), "yyyy-MM-dd") & "'" '仕入日
    '                    Sql += ",''" '空でよし
    '                    Sql += ",'" & formatDouble(calCost + calVat) & "'" '仕入金額 + VAT IN
    '                    Sql += ",''" '空でよし
    '                    Sql += ",''" '空でよし

    '                    '金額がゼロの時は登録しない
    '                    If calVat <> 0 Then
    '                        't67_swkhd データ登録
    '                        updateT67Swkhd(Sql)
    '                    End If

    '                    countKeyID = getCount(countKeyID)


    '                    Sql = ",'" & Format(dsSWKNyukohd.Tables(RS).Rows(x)("入庫日"), "yyyyMM") & "'" '入庫日
    '                    Sql += "," & seqID 'プライマリ
    '                    Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
    '                    'Sql += ",'" & getAccountName("accounts-payable") & "'" '買掛金
    '                    Sql += ",'買掛金'" '買掛金　テスト用
    '                    Sql += "," & formatDouble(-calVat) '仕入金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
    '                    Sql += ",1" '固定
    '                    Sql += ",'" & dsSwkHattyu.Tables(RS).Rows(0)("仕入先名").ToString & "'" '仕入先コード
    '                    Sql += ",'WH-" & dsSwkSirehd.Tables(RS).Rows(x)("客先番号").ToString & "-" & i & "'" 'PO
    '                    Sql += ",'" & Format(dsSwkSirehd.Tables(RS).Rows(i)("仕入日"), "yyyy-MM-dd") & "'" '仕入日
    '                    Sql += ",''" '空でよし
    '                    Sql += ",'" & formatDouble(calCost + calVat) & "'" '仕入金額 + VAT IN
    '                    Sql += ",''" '空でよし
    '                    Sql += ",''" '空でよし

    '                    '金額がゼロの時は登録しない
    '                    If calVat <> 0 Then
    '                        't67_swkhd データ登録
    '                        updateT67Swkhd(Sql)
    '                    End If

    '                    countKeyID = getCount(countKeyID)
    '#End Region

    '                ElseIf dsSwkSirehd.Tables(RS).Rows(i)("仕入区分") = 0 _
    '                    And dsSWKNyukohd.Tables(RS).Rows(i)("取消区分") = 1 Then
    '                    '入庫取消に伴う仕入返品計上
    '                    '※入庫Ｈ，明細にレコード登録時、
    '                    'かつ「仕入区分0:移動以外」、「取消区分1:取消」
    '#Region "入庫取消"
    '#End Region
    '                Else
    '                    'todo:msgbox
    '                    MsgBox("エラー")
    '                End If


    '            Next

    '        Next
    '#End Region


    '#Region "仕訳売掛金"

    '        Sql = " ORDER BY "
    '        Sql += " 売上日 "
    '        'todo:t30_urighd 条件
    '        Dim dsSwkUrighd As DataSet = getDsData("t30_urighd", Sql) '売上データの取得

    '        For i As Integer = 0 To dsSwkUrighd.Tables(RS).Rows.Count - 1  't30_urighd

    '            Dim countKeyID As Integer = 0

    '            '売掛金, 売上 = 仕入金額 * (VAT / 100)
    '            Dim calCost As Decimal = dsSwkUrighd.Tables(RS).Rows(i)("売上金額") * (dsSwkUrighd.Tables(RS).Rows(i)("VAT") / 100)
    '            '売掛金, VAT-OUT = calCost * (VAT / 100)
    '            'Dim calVat As Decimal = calCost * (dsSwkUrighd.Tables(RS).Rows(i)("VAT") / 100)
    '            Dim calVat As Decimal = dsSwkUrighd.Tables(RS).Rows(i)("VAT")
    '            '仕入金額（棚卸資産を減らすため）
    '            Dim calSiire = dsSwkUrighd.Tables(RS).Rows(i)("仕入金額")


    '            If dsSwkUrighd.Tables(RS).Rows(i)("仕入区分") = 0 _
    '                    And dsSwkUrighd.Tables(RS).Rows(i)("取消区分") = 0 Then
    '                '出庫入力に伴う売上伝票計上
    '                '※出庫Ｈ、明細にレコード登録時、
    '                'かつ「仕入区分0:移動以外」、「取消区分0:有効」

    '#Region "出庫入力"
    '                Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("売上日"), "yyyyMM") & "'" '売上日
    '                Sql += "," & "nextval('transactionid_seq')" 'プライマリ
    '                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
    '                'Sql += ",'" & getAccountName("accounts-receivable") & "'" '売掛金
    '                Sql += ",'売掛金'" '売掛金　テスト用
    '                Sql += "," & formatDouble(calCost) '売上金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
    '                Sql += ",1" '固定
    '                Sql += ",'" & dsSwkUrighd.Tables(RS).Rows(i)("得意先コード").ToString & "'" '得意先コード
    '                Sql += ",'ER-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
    '                Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("売上日"), "yyyy-MM-dd") & "'" '売上日
    '                Sql += ",''" '空でよし
    '                Sql += "," & formatDouble(calCost + calVat + calSiire) '売上金額 + VAT IN + 仕入金額
    '                Sql += ",''" '空でよし
    '                Sql += ",''" '空でよし

    '                countKeyID = getCount(countKeyID)

    '                't67_swkhd データ登録
    '                updateT67Swkhd(Sql)

    '                seqID = getSeq("transactionid_seq")

    '                Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("売上日"), "yyyyMM") & "'" '売上日
    '                Sql += "," & seqID 'プライマリ
    '                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
    '                'Sql += ",'" & getAccountName("sales") & "'" '売上
    '                Sql += ",'売上'" '売上 テスト用
    '                Sql += "," & formatDouble(-(calCost)) '売上金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
    '                Sql += ",1" '固定
    '                Sql += ",'" & dsSwkUrighd.Tables(RS).Rows(0)("得意先コード").ToString & "'" '得意先コード
    '                Sql += ",'ER-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
    '                Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("売上日"), "yyyy-MM-dd") & "'" '売上日
    '                Sql += ",''" '空でよし
    '                Sql += "," & formatDouble(calCost + calVat + calSiire) '売上金額 + VAT IN + 仕入金額
    '                Sql += ",''" '空でよし
    '                Sql += ",''" '空でよし

    '                't67_swkhd データ登録
    '                updateT67Swkhd(Sql)

    '                countKeyID = getCount(countKeyID)

    '                Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("売上日"), "yyyyMM") & "'" '売上日
    '                Sql += "," & seqID 'プライマリ
    '                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
    '                'Sql += ",'" & getAccountName("accounts-receivable") & "'" '売掛金
    '                Sql += ",'売掛金'" '売掛金　テスト用
    '                Sql += "," & formatDouble(calVat) '売上金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
    '                Sql += ",1" '固定
    '                Sql += ",'" & dsSwkUrighd.Tables(RS).Rows(0)("得意先コード").ToString & "'" '得意先コード
    '                Sql += ",'ER-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
    '                Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("売上日"), "yyyy-MM-dd") & "'" '売上日
    '                Sql += ",''" '空でよし
    '                Sql += "," & formatDouble(calCost + calVat + calSiire) '売上金額 + VAT IN + 仕入金額
    '                Sql += ",''" '空でよし
    '                Sql += ",''" '空でよし

    '                't67_swkhd データ登録
    '                updateT67Swkhd(Sql)

    '                countKeyID = getCount(countKeyID)

    '                Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("売上日"), "yyyyMM") & "'" '売上日
    '                Sql += "," & seqID 'プライマリ
    '                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
    '                'Sql += ",'" & getAccountName("vat-out") & "'" 'VAT-OUT
    '                Sql += ",'VAT-OUT'" 'VAT-OUT　テスト用
    '                Sql += "," & formatDouble(-(calVat)) '売上金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
    '                Sql += ",1" '固定
    '                Sql += ",'" & dsSwkUrighd.Tables(RS).Rows(0)("得意先コード").ToString & "'" '得意先コード
    '                Sql += ",'ER-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
    '                Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("売上日"), "yyyy-MM-dd") & "'" '売上日
    '                Sql += ",''" '空でよし
    '                Sql += "," & formatDouble(calCost + calVat + calSiire) '売上金額 + VAT IN + 仕入金額
    '                Sql += ",''" '空でよし
    '                Sql += ",''" '空でよし

    '                't67_swkhd データ登録
    '                updateT67Swkhd(Sql)

    '                '出荷時の棚卸資産と仕入
    '                countKeyID = getCount(countKeyID)
    '                Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("売上日"), "yyyyMM") & "'" '売上日
    '                Sql += "," & seqID 'プライマリ
    '                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
    '                'Sql += ",'" & getAccountName("accounts-receivable") & "'" '仕入
    '                Sql += ",'仕入'" '仕入　テスト用
    '                Sql += "," & formatDouble(calSiire) '仕入金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
    '                Sql += ",1" '固定
    '                Sql += ",'" & dsSwkUrighd.Tables(RS).Rows(i)("得意先コード").ToString & "'" '得意先コード
    '                Sql += ",'ER-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
    '                Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("売上日"), "yyyy-MM-dd") & "'" '売上日
    '                Sql += ",''" '空でよし
    '                Sql += "," & formatDouble(calCost + calVat + calSiire) '売上金額 + VAT IN + 仕入金額
    '                Sql += ",''" '空でよし
    '                Sql += ",''" '空でよし

    '                't67_swkhd データ登録
    '                updateT67Swkhd(Sql)

    '                '出荷時の棚卸資産と仕入
    '                countKeyID = getCount(countKeyID)
    '                Sql = ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("売上日"), "yyyyMM") & "'" '売上日
    '                Sql += "," & seqID 'プライマリ
    '                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
    '                'Sql += ",'" & getAccountName("accounts-receivable") & "'" '仕入
    '                Sql += ",'棚卸資産'" '仕入　テスト用
    '                Sql += "," & formatDouble(-(calSiire)) '棚卸資産（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
    '                Sql += ",1" '固定
    '                Sql += ",'" & dsSwkUrighd.Tables(RS).Rows(i)("得意先コード").ToString & "'" '得意先コード
    '                Sql += ",'ER-" & dsSwkUrighd.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
    '                Sql += ",'" & Format(dsSwkUrighd.Tables(RS).Rows(i)("売上日"), "yyyy-MM-dd") & "'" '売上日
    '                Sql += ",''" '空でよし
    '                Sql += "," & formatDouble(calCost + calVat + calSiire) '売上金額 + VAT IN + 仕入金額
    '                Sql += ",''" '空でよし
    '                Sql += ",''" '空でよし

    '                't67_swkhd データ登録
    '                updateT67Swkhd(Sql)

    '#End Region
    '            ElseIf dsSwkUrighd.Tables(RS).Rows(i)("仕入区分") = 0 _
    '                    And dsSwkUrighd.Tables(RS).Rows(i)("取消区分") = 0 Then
    '                '出庫取消に伴う売上返品計上
    '                '※出庫Ｈ，明細にレコード登録時、
    '                'かつ「仕入区分0:移動以外」、「取消区分1:取消」
    '#Region "出庫取消"

    '#End Region
    '            Else
    '                'サービス（役務）販売による売上伝票計上
    '                '19.06.19追加
    '                '※出庫入力に伴う売上伝票計上に準ずる
    '#Region "サービス販売"

    '#End Region
    '            End If


    '        Next
    '#End Region


    '#Region "仕訳前受金"

    '        Sql = " ORDER BY "
    '        Sql += " 入金日 "
    '        'todo:t27_nkinkshihd 条件
    '        Dim dsNkinkshihd As DataSet = getDsData("t27_nkinkshihd", Sql) '入金消込データの取得

    '        For i As Integer = 0 To dsNkinkshihd.Tables(RS).Rows.Count - 1

    '            Sql = " AND "
    '            Sql += "請求番号"
    '            Sql += " ILIKE "
    '            Sql += "'"
    '            Sql += dsNkinkshihd.Tables(RS).Rows(i)("請求番号")
    '            Sql += "'"

    '            Dim dsNkinSkyu As DataSet = getDsData("t23_skyuhd", Sql) '請求データの取得

    '            '------------------------------->> 共通化したい
    '            Sql = " AND "
    '            Sql += "得意先コード"
    '            Sql += " ILIKE "
    '            Sql += "'"
    '            Sql += dsNkinSkyu.Tables(0).Rows(0)("得意先コード")
    '            Sql += "'"

    '            'm10 得意先マスタ
    '            Dim dsCustomer As DataSet = getDsData("m10_customer", Sql) '得意先マスタデータの取得
    '            Console.WriteLine(dsCustomer.Tables(RS).Rows(0)("会計用得意先コード"))
    '            Dim codeAAC As String = dsCustomer.Tables(RS).Rows(0)("会計用得意先コード")
    '            '<<------------------------------- 共通化したい

    '            Dim transactionid As String = DateTime.Now.ToString("MMddHHmmss" & i) 'TRANSACTIONID
    '            Dim countKeyID As Integer = 0

    '            upSeq() 'シーケンス更新
    '            seqID = getSeq("transactionid_seq")


    '            If dsNkinSkyu.Tables(RS).Rows(0)("請求区分") = "1" Then  '前受請求の場合

    '                '前受金のみ入金（現金入金）		　　　　　　　  　　現金				前受金	得意先
    '                '※入金ヘッダ,明細にレコード登録時、							
    '                'かつ入金番号に一致する請求Rが存在し、							
    '                'かつ請求R.請求区分が「1:前受金」の場合、							
    '                '「入金明細入金区分」に応じて伝票を発生させる
    '                'ここでは「区分3:現金」を例としている
    '                '    受注F-(受注番号) - 請求F - (入金番号) - 入金F            

    '#Region "前受請求"

    '                'Sql = " AND "
    '                'Sql += "受注番号"
    '                'Sql += " ILIKE "
    '                'Sql += "'"
    '                'Sql += dsNkinSkyu.Tables(RS).Rows(0)("受注番号")
    '                'Sql += "'"

    '                'Dim dsNkinCymn As DataSet = getDsData("t10_cymnhd", Sql) '受注データの取得

    '                '入金種別に対応した科目を取得
    '                Dim strKamoku As String = mGet_NyukinSyubetu(dsNkinkshihd.Tables(RS).Rows(0)("入金種別"))

    '                '貸方　入金種別
    '                Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
    '                Sql += "," & seqID 'プライマリ
    '                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
    '                'Sql += ",'" & getAccountName("cash-deposit") & "'" '現金預金
    '                Sql += ",'" & strKamoku & "'" '貸方科目
    '                Sql += "," & formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計")) '入金金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
    '                Sql += ",1" '固定
    '                Sql += ",'" & dsNkinSkyu.Tables(RS).Rows(0)("得意先コード").ToString & "'" '得意先コード
    '                Sql += ",'PM-" & dsNkinSkyu.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
    '                Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
    '                Sql += ",''" '空でよし
    '                Sql += "," & formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計")) '入金金額
    '                Sql += ",'" & codeAAC & "'" '会計用得意先コード
    '                Sql += ",''" '空でよし

    '                countKeyID = getCount(countKeyID)

    '                't67_swkhd データ登録
    '                updateT67Swkhd(Sql)


    '                '借方　前受金
    '                Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
    '                Sql += "," & seqID 'プライマリ
    '                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
    '                'Sql += ",'" & getAccountName("advance") & "'" '前受金
    '                Sql += ",'前受金'" '前受金
    '                Sql += "," & formatDouble(-dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計")) '入金金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
    '                Sql += ",1" '固定
    '                Sql += ",'" & dsNkinSkyu.Tables(RS).Rows(0)("得意先コード").ToString & "'" '得意先コード
    '                Sql += ",'PM-" & dsNkinSkyu.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
    '                Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
    '                Sql += ",''" '空でよし
    '                Sql += "," & formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計")) '入金金額
    '                Sql += ",'" & codeAAC & "'" '会計用得意先コード
    '                Sql += ",''" '空でよし

    '                countKeyID = getCount(countKeyID)

    '                't67_swkhd データ登録
    '                updateT67Swkhd(Sql)


    '                'Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
    '                'Sql += "," & seqID 'プライマリ
    '                'Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
    '                ''Sql += ",'" & getAccountName("advance") & "'" '前受金
    '                'Sql += ",'前受金'" '前受金　テスト用
    '                'Sql += "," & formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計")) '入金金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
    '                'Sql += ",1" '固定
    '                'Sql += ",'" & dsNkinSkyu.Tables(RS).Rows(0)("得意先コード").ToString & "'" '得意先コード
    '                'Sql += ",'PM-" & dsNkinSkyu.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
    '                'Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
    '                'Sql += ",''" '空でよし
    '                'Sql += "," & formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計")) '入金金額
    '                'Sql += ",'" & codeAAC & "'" '会計用得意先コード
    '                'Sql += ",''" '空でよし

    '                'countKeyID = getCount(countKeyID)

    '                ''t67_swkhd データ登録
    '                'updateT67Swkhd(Sql)


    '                'Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
    '                'Sql += "," & seqID 'プライマリ
    '                'Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
    '                ''Sql += ",'" & getAccountName("accounts-receivable") & "'" '売掛金
    '                'Sql += ",'売掛金'" '売掛金　テスト用
    '                'Sql += "," & formatDouble(-dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計")) '入金金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
    '                'Sql += ",1" '固定
    '                'Sql += ",'" & dsNkinSkyu.Tables(RS).Rows(0)("得意先コード").ToString & "'" '得意先コード
    '                'Sql += ",'PM-" & dsNkinSkyu.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
    '                'Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
    '                'Sql += ",''" '空でよし
    '                'Sql += "," & formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計")) '入金金額
    '                'Sql += ",'" & codeAAC & "'" '会計用得意先コード
    '                'Sql += ",''" '空でよし

    '                ''t67_swkhd データ登録
    '                'updateT67Swkhd(Sql)
    '#End Region

    '            ElseIf dsNkinSkyu.Tables(RS).Rows(0)("請求区分") = "2" Then  '通常請求の場合

    '                '前受金入金に対する振込入金処理                   普通預金				売掛金	得意先
    '                '（例：普通預金）		前受金					
    '                '※入金ヘッダ，明細にレコード登録時、							
    '                '　かつ入金番号に一致する請求Ｒが存在し、							
    '                '　かつ請求R.請求区分が「2:通常請求」の場合、							
    '                '　かつ請求R.受注番号と同一受注番号を持つ
    '                '            請求R.請求区分が「2:前受金」であるレコードが
    '                '            存在する場合、							
    '                '入金明細入金区分に応じて伝票を発生させる
    '                'ここでは「区分1:振込入金」を例としている

    '#Region "通常請求"

    '                Sql = " AND "
    '                Sql += "受注番号"
    '                Sql += " ILIKE "
    '                Sql += "'"
    '                Sql += dsNkinSkyu.Tables(RS).Rows(0)("受注番号")
    '                Sql += "'"

    '                Dim dsNkinCymn As DataSet = getDsData("t10_cymnhd", Sql) '受注データの取得

    '                '売掛データ
    '                Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
    '                Sql += "," & seqID 'プライマリ
    '                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
    '                'Sql += ",'" & getAccountName("cash-deposit") & "'" '現金預金
    '                Sql += ",'現金預金'" '現金預金　テスト用
    '                Sql += "," & formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計")) '入金金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
    '                Sql += ",1" '固定
    '                Sql += ",'" & dsNkinSkyu.Tables(RS).Rows(0)("得意先コード").ToString & "'" '得意先コード
    '                Sql += ",'PM-" & dsNkinSkyu.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
    '                Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
    '                Sql += ",''" '空でよし
    '                Sql += "," & formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計")) '入金金額
    '                Sql += ",'" & codeAAC & "'" '会計用得意先コード
    '                Sql += ",''" '空でよし

    '                countKeyID = getCount(countKeyID)

    '                't67_swkhd データ登録
    '                updateT67Swkhd(Sql)

    '                Sql = ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyyMM") & "'" '入金日
    '                Sql += "," & seqID 'プライマリ
    '                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
    '                'Sql += ",'" & getAccountName("accounts-receivable") & "'" '売掛金
    '                Sql += ",'売掛金'" '売掛金　テスト用
    '                Sql += "," & formatDouble(-dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計")) '入金金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
    '                Sql += ",1" '固定
    '                Sql += ",'" & dsNkinSkyu.Tables(RS).Rows(0)("得意先コード").ToString & "'" '得意先コード
    '                Sql += ",'PM-" & dsNkinSkyu.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
    '                Sql += ",'" & Format(dsNkinkshihd.Tables(RS).Rows(i)("入金日"), "yyyy-MM-dd") & "'" '入金日
    '                Sql += ",''" '空でよし
    '                Sql += "," & formatDouble(dsNkinkshihd.Tables(RS).Rows(i)("入金消込額計")) '入金金額
    '                Sql += ",'" & codeAAC & "'" '会計用得意先コード
    '                Sql += ",''" '空でよし

    '                't67_swkhd データ登録
    '                updateT67Swkhd(Sql)

    '#End Region
    '            End If

    '        Next
    '#End Region


    '#Region "仕訳前払金"

    '        Sql = " ORDER BY "
    '        Sql += " 支払日 "

    '        Dim dsShrikshihd As DataSet = getDsData("t49_shrikshihd", Sql) '支払消込データの取得

    '        For i As Integer = 0 To dsShrikshihd.Tables(RS).Rows.Count - 1

    '            Sql = " AND "
    '            Sql += "買掛番号"
    '            Sql += " ILIKE "
    '            Sql += "'"
    '            Sql += dsShrikshihd.Tables(RS).Rows(i)("買掛番号")
    '            Sql += "'"

    '            Dim dsShriKike As DataSet = getDsData("t46_kikehd", Sql) '買掛データの取得

    '            '------------------------------->> 共通化したい
    '            Sql = " AND "
    '            Sql += "仕入先コード"
    '            Sql += " ILIKE "
    '            Sql += "'"
    '            Sql += dsShriKike.Tables(0).Rows(0)("仕入先コード")
    '            Sql += "'"

    '            'm10 得意先マスタ
    '            Dim dsSupplier As DataSet = getDsData("m11_supplier", Sql) '得意先マスタデータの取得

    '            Dim codeAAC As String = dsSupplier.Tables(RS).Rows(0)("会計用仕入先コード")
    '            '<<------------------------------- 共通化したい

    '            Dim countKeyID As Integer = 0







    '            If dsShriKike.Tables(RS).Rows(0)("買掛区分") = "1" Then  '前払買掛
    '                '前払金の支払（現金支払）		　　　　　前払金				現金	仕入先
    '                '※支払ヘッダ,明細にレコード登録時、							
    '                '　かつ支払番号に一致する消込Ｒが存在し、							
    '                '　消込Ｒ.買掛番号に一致する買掛Rが存在し、							
    '                '　かつ買掛R.買掛区分が「2:前払金」の場合、							
    '                '　支払明細支払種別に応じて伝票を発生させる
    '                'ここでは「区分3:現金」を例としている
    '                '  発注F-(発注番号) - 買掛F - (買掛番号) - 消込F - (支払番号) - 支払F                            


    '#Region "前払買掛"

    '                Sql = " AND "
    '                Sql += "発注番号"
    '                Sql += " ILIKE "
    '                Sql += "'"
    '                Sql += dsShriKike.Tables(RS).Rows(0)("発注番号")
    '                Sql += "'"

    '                '買掛データの取得（仕入先名取るためっぽい）使うか？
    '                Dim dsShriHattyu As DataSet = getDsData("t20_hattyu", Sql)

    '                '支払種別に対応した科目を取得
    '                Dim strKamoku As String = mGet_NyukinSyubetu(dsNkinkshihd.Tables(RS).Rows(0)("支払種別"))


    '                '貸方　前払金
    '                Sql = ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'" '入金日
    '                Sql += "," & "nextval('transactionid_seq')" 'プライマリ
    '                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
    '                'Sql += ",'" & getAccountName("prepaid") & "'" '前払金
    '                Sql += ",'前払金'" '前払金　テスト用
    '                Sql += "," & formatDouble(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計")) '支払金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
    '                Sql += ",1" '固定
    '                Sql += ",'" & dsShriKike.Tables(RS).Rows(0)("仕入先コード").ToString & "'" '仕入先コード
    '                Sql += ",'PO-" & dsShriKike.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
    '                Sql += ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'" '入金日
    '                Sql += ",''" '空でよし
    '                Sql += "," & formatDouble(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計")) '支払金額
    '                Sql += ",'" & codeAAC & "'" '会計用支払先コード
    '                Sql += ",''" '空でよし

    '                updateT67Swkhd(Sql) 'update実行
    '                seqID = getSeq("transactionid_seq")
    '                countKeyID = getCount(countKeyID) '0～カウントアップ

    '                '借方　支払種別
    '                Sql = ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'" '入金日
    '                Sql += "," & seqID 'プライマリ
    '                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
    '                'Sql += ",'" & getAccountName("cash-deposit") & "'" '現金預金
    '                Sql += ",'" & strKamoku & "'"  '借方科目
    '                Sql += "," & formatDouble(-dsShrikshihd.Tables(RS).Rows(i)("支払消込額計")) '支払金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
    '                Sql += ",1" '固定
    '                Sql += ",'" & dsShriKike.Tables(RS).Rows(0)("仕入先コード").ToString & "'" '仕入先コード
    '                Sql += ",'PO-" & dsShriKike.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
    '                Sql += ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'" '入金日
    '                Sql += ",''" '空でよし
    '                Sql += "," & formatDouble(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計")) '支払金額
    '                Sql += ",'" & codeAAC & "'" '会計用支払先コード
    '                Sql += ",''" '空でよし

    '                updateT67Swkhd(Sql) 'update実行
    '                countKeyID = getCount(countKeyID) '0～カウントアップ

    '                'Sql = ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'" '入金日
    '                'Sql += "," & seqID 'プライマリ
    '                'Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
    '                ''Sql += ",'" & getAccountName("accounts-payable") & "'" '買掛金
    '                'Sql += ",'買掛金'" '買掛金　テスト用
    '                'Sql += "," & formatDouble(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計")) '支払金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
    '                'Sql += ",1" '固定
    '                'Sql += ",'" & dsShriKike.Tables(RS).Rows(0)("仕入先コード").ToString & "'" '仕入先コード
    '                'Sql += ",'PO-" & dsShriKike.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
    '                'Sql += ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'" '入金日
    '                'Sql += ",''" '空でよし
    '                'Sql += "," & formatDouble(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計")) '支払金額
    '                'Sql += ",'" & codeAAC & "'" '会計用支払先コード
    '                'Sql += ",''" '空でよし

    '                'updateT67Swkhd(Sql) 'update実行
    '                'countKeyID = getCount(countKeyID) '0～カウントアップ

    '                'Sql = ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'" '入金日
    '                'Sql += "," & seqID 'プライマリ
    '                'Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
    '                ''Sql += ",'" & getAccountName("prepaid") & "'" '前払金
    '                'Sql += ",'前払金'" '前払金　テスト用
    '                'Sql += "," & formatDouble(-dsShrikshihd.Tables(RS).Rows(i)("支払消込額計")) '支払金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
    '                'Sql += ",1" '固定
    '                'Sql += ",'" & dsShriKike.Tables(RS).Rows(0)("仕入先コード").ToString & "'" '仕入先コード
    '                'Sql += ",'PO-" & dsShriKike.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
    '                'Sql += ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'" '入金日
    '                'Sql += ",''" '空でよし
    '                'Sql += "," & formatDouble(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計")) '支払金額
    '                'Sql += ",'" & codeAAC & "'" '会計用支払先コード
    '                'Sql += ",''" '空でよし

    '                'updateT67Swkhd(Sql) 'update実行

    '#End Region

    '            ElseIf dsShriKike.Tables(RS).Rows(0)("買掛区分") = "2" Then  '通常買掛
    '                '前払金支払に対する振込支払精算（普通預金）     買掛金	仕入先			普通預金	
    '                '							　　　　　　　　　　　　　　　　　　　　　　前払金	
    '                '※支払ヘッダ，明細にレコード登録時、							
    '                '　かつ支払番号に一致する消込Ｒが存在し、							
    '                '　消込R.買掛番号に一致する買掛Ｒが存在し、							
    '                '　かつ買掛R.買掛区分が1 : 通常支払の場合、							
    '                '　かつ買掛R.発注番号と同一発注番号を持つ
    '                '  買掛R.買掛区分が2 : 前受金であるレコードが存在する場合、							
    '                '  支払明細支払区分に応じて伝票を発生させる
    '                '  ここでは「区分1:振込入金」を例としている

    '#Region "通常買掛"

    '                Sql = " AND "
    '                Sql += "発注番号"
    '                Sql += " ILIKE "
    '                Sql += "'"
    '                Sql += dsShriKike.Tables(RS).Rows(0)("発注番号")
    '                Sql += "'"

    '                '買掛データの取得（仕入先名取るためっぽい）使うか？
    '                Dim dsShriHattyu As DataSet = getDsData("t20_hattyu", Sql)

    '                '支払種別に対応した科目を取得
    '                Dim strKamoku As String = mGet_NyukinSyubetu(dsNkinkshihd.Tables(RS).Rows(0)("支払種別"))

    '                '貸方
    '                Sql = ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'" '入金日
    '                Sql += "," & "nextval('transactionid_seq')" 'プライマリ
    '                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
    '                'Sql += ",'" & getAccountName("accounts-payable") & "'" '買掛金
    '                Sql += ",'買掛金'" '買掛金　テスト用
    '                Sql += "," & formatDouble(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計")) '支払金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
    '                Sql += ",1" '固定
    '                Sql += ",'" & dsShriKike.Tables(RS).Rows(0)("仕入先コード").ToString & "'" '仕入先コード
    '                Sql += ",'PO-" & dsShriKike.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
    '                Sql += ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'" '入金日
    '                Sql += ",''" '空でよし
    '                Sql += "," & formatDouble(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計")) '支払金額
    '                Sql += ",'" & codeAAC & "'" '会計用支払先コード
    '                Sql += ",''" '空でよし

    '                updateT67Swkhd(Sql) 'update実行
    '                seqID = getSeq("transactionid_seq")
    '                countKeyID = getCount(countKeyID) '0～カウントアップ

    '                '借方
    '                Sql = ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyyMM") & "'" '入金日
    '                Sql += "," & seqID 'プライマリ
    '                Sql += "," & countKeyID 'TRANSACTIONID内でカウントアップ（0から）
    '                'Sql += ",'" & getAccountName("cash-deposit") & "'" '現金預金
    '                Sql += ",'現金預金'" '現金預金　テスト用
    '                Sql += "," & formatDouble(-dsShrikshihd.Tables(RS).Rows(i)("支払消込額計")) '支払金額（貸方金額は整数、借方金額は負数。小数点は含んでよい -nnnnnnn.nn）
    '                Sql += ",1" '固定
    '                Sql += ",'" & dsShriKike.Tables(RS).Rows(0)("仕入先コード").ToString & "'" '仕入先コード
    '                Sql += ",'PO-" & dsShriKike.Tables(RS).Rows(0)("客先番号").ToString & "-" & i & "'" 'PO
    '                Sql += ",'" & Format(dsShrikshihd.Tables(RS).Rows(i)("支払日"), "yyyy-MM-dd") & "'" '入金日
    '                Sql += ",''" '空でよし
    '                Sql += "," & formatDouble(dsShrikshihd.Tables(RS).Rows(i)("支払消込額計")) '支払金額
    '                Sql += ",'" & codeAAC & "'" '会計用支払先コード
    '                Sql += ",''" '空でよし

    '                updateT67Swkhd(Sql) 'update実行

    '#End Region
    '            End If

    '        Next
    '#End Region



    '#Region "在庫増"
    '        '期中の実地棚卸等に基づく在庫増     　　　　　商品				雑収入	
    '        '※入出庫Fにレコード登録時、							
    '        '　かつ入出庫区分「1: 入庫」、							
    '        '　かつ入出庫種別「0:通常」、「1:サンプル」、							
    '        '　かつ仕入区分「0:移動」のレコードである場合、伝票を発生させる							
    '        '　入庫情報として伝票番号、行番号に一致する							
    '        '　入庫明細を参照する
    '#End Region


    '#Region "在庫減"
    '        '期中の実地棚卸等に基づく在庫減  　　　　　   棚卸減耗損				期末商品棚卸高	
    '        '※入出庫Fにレコード登録時、							
    '        '　かつ入出庫区分「1:入庫」、							
    '        '　かつ入出庫種別「2:廃棄」、							
    '        '　かつ仕入区分「0:移動」のレコードである場合、伝票を発生させる							
    '        '　入庫情報として伝票番号、行番号に一致する							
    '        '　入庫明細を参照する
    '#End Region


    '#Region "棚卸ロス"
    '        '期末の実地棚卸等に基づく在庫減     　　　　　棚卸減耗損				期末商品棚卸高	
    '        '※入出庫Fにレコード登録時、							
    '        '　かつ入出庫区分「1:入庫」、							
    '        '　かつ入出庫種別「3:棚卸ロス」、							
    '        '　かつ仕入区分「0:移動」のレコードである場合、伝票を発生させる							
    '        '　入庫情報として伝票番号、行番号に一致する							
    '        '　入庫明細を参照する
    '#End Region

    '    End Sub

    '入金種別に対応した科目を取得


    Private Function mGet_NyukinSyubetu(ByVal strNyukin As String) As String

        Dim strKamoku As String = vbNullString

        Select Case strNyukin  '入金種別
            Case 1
                strKamoku = "現金預金"
            Case 2
                strKamoku = "振込手数料"
            Case 3
                strKamoku = "現金"
            Case 4  '受取手形入金
                strKamoku = "受取手形"
            Case 5  '電子債権入金
                strKamoku = "受取手形"
            Case 6
                strKamoku = "売上割引"
            Case 7
                strKamoku = "売上値引"
            Case 8  'リベート
                strKamoku = "売上割戻"
            Case 9  '相殺
                strKamoku = "買掛金"
        End Select

        mGet_NyukinSyubetu = strKamoku

    End Function

    '支払種別に対応した科目を取得
    Private Function mGet_SiharaiSyubetu(ByVal strSiharai As String) As String

        Dim strKamoku As String = vbNullString

        Select Case strSiharai  '入金種別
            Case 1
                strKamoku = "現金預金"
            Case 2
                strKamoku = "振込手数料"
            Case 3
                strKamoku = "現金"
            Case 4  '支払手形支払
                strKamoku = "支払手形"
            Case 5  '電子債権支払
                strKamoku = "支払手形"
            Case 6
                strKamoku = "仕入割引"
            Case 7
                strKamoku = "仕入値引"
            Case 8  'リベート
                strKamoku = "仕入割戻"
            Case 9  '相殺
                strKamoku = "買掛金"
        End Select

        mGet_SiharaiSyubetu = strKamoku

    End Function

    'シーケンス更新
    Private Sub upSeq()
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""
        Sql = "Select nextval('transactionid_seq')"

        _db.selectDB(Sql, RS, reccnt)
    End Sub
    'テーブル名
    'オプションがあれば（条件）第二引数
    Private Function getDsData(ByVal tableName As String, Optional ByRef txtParam As String = "") As DataSet
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Sql += "SELECT"
        Sql += " *"
        Sql += " FROM "

        Sql += "public." & tableName
        Sql += " WHERE "
        Sql += "会社コード"
        Sql += " ILIKE  "
        Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += txtParam
        Console.WriteLine(Sql)
        Return _db.selectDB(Sql, RS, reccnt)
    End Function

    '現状のシーケンス取得
    Private Function getSeq(ByVal seqName As String) As Integer
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""
        Dim dsData As DataSet

        Sql += "SELECT"
        Sql += " *"
        Sql += " FROM "
        Sql += "public." & seqName

        dsData = _db.selectDB(Sql, RS, reccnt)

        Return dsData.Tables(RS).Rows(0)("last_value")
    End Function

    'カウントアップ
    Private Function getCount(ByVal nowCount As Integer) As Integer
        Dim count As Integer
        count = nowCount + 1

        Return count
    End Function
    '登録する科目名
    'オプションがあれば（条件）第二引数
    Private Sub updateT67Swkhd(ByVal param As String)
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Sql += "INSERT INTO "
        Sql += "Public.t67_swkhd"
        Sql += "("
        Sql += """会社コード"""
        Sql += ",""処理年月"""
        Sql += ",""TRANSACTIONID"""
        Sql += ",""KeyID"""
        Sql += ",""GLACCOUNT"""
        Sql += ",""GLAMOUNT"""
        Sql += ",""RATE"""
        Sql += ",""VENDORNO"""
        Sql += ",""JVNUMBER"""
        Sql += ",""TRANSDATE"""
        Sql += ",""TRANSDESCRIPTION"""
        Sql += ",""JVAMOUNT"""
        Sql += ",""CUSTOMERNO"""
        Sql += ",""DESCRIPTION"""
        Sql += ") "
        Sql += " VALUES("
        Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += param
        Sql += ") "
        'Console.WriteLine(Sql)
        _db.executeDB(Sql)
    End Sub

    '小数部分のフォーマット
    Private Function formatDouble(ByVal val As Decimal) As Decimal
        Dim result As Decimal

        ' 小数点第三位で四捨五入し、小数点第二位まで出力
        result = Math.Round(val, 2, MidpointRounding.AwayFromZero)

        Return result
    End Function

    'NothingをDecimalに置換
    Private Function rmNullDecimal(ByVal prmField As Object) As Decimal
        If prmField Is Nothing Then
            rmNullDecimal = 0
            Exit Function
        End If
        If prmField Is DBNull.Value Then
            rmNullDecimal = 0
            Exit Function
        End If

        If Not IsNumeric(prmField) Then
            rmNullDecimal = 0
            Exit Function
        End If

        rmNullDecimal = prmField

    End Function

End Class