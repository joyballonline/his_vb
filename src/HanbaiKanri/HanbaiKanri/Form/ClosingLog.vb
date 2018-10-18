﻿Option Explicit On

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

        If dtToday > ds1.Tables(RS).Rows(0)("今回締日") Then

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

            Dim ds2 As DataSet = _db.selectDB(Sql2, RS, reccnt)
            Dim ds3 As DataSet = _db.selectDB(Sql3, RS, reccnt)
            Dim ds4 As DataSet = _db.selectDB(Sql4, RS, reccnt)

            Dim Sql5 As String = ""
            Sql5 += "SELECT "
            Sql5 += "* "
            Sql5 += "FROM "
            Sql5 += "public"
            Sql5 += "."
            Sql5 += "t31_urigdt"
            For i As Integer = 0 To ds2.Tables(RS).Rows.Count - 1
                If i = 0 Then
                    Sql5 += " WHERE "
                    Sql5 += "売上番号"
                    Sql5 += " ILIKE  "
                    Sql5 += "'"
                    Sql5 += ds2.Tables(RS).Rows(i)("売上番号")
                    Sql5 += "'"
                Else
                    Sql5 += " OR "
                    Sql5 += "売上番号"
                    Sql5 += " ILIKE  "
                    Sql5 += "'"
                    Sql5 += ds2.Tables(RS).Rows(i)("売上番号")
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
                sum1 = dszaiko.Tables(RS).Rows(i)("今月単価") * dszaiko.Tables(RS).Rows(i)("今月末数量")
                sum1 += PurchaseSum
                sum2 = dszaiko.Tables(RS).Rows(i)("今月末数量") + PurchaseQuantity
                unitPrice = sum1 / sum2

                Dim sum3 As Double = 0
                Dim sum4 As Double = 0
                Dim OverHead As Double = 0
                sum3 = dszaiko.Tables(RS).Rows(i)("今月間接費") * dszaiko.Tables(RS).Rows(i)("今月末数量")
                sum3 += OverheadSum
                sum4 = dszaiko.Tables(RS).Rows(i)("今月末数量") + PurchaseQuantity
                OverHead = sum3 / sum4

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
                    Sql8 += "会社コード, 年月, メーカー, 品名, 型式, 前月末数量, 前月末間接費, 今月末数量, 今月入庫数, 今月出庫数, 今月間接費, 更新者, 更新日)"
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
                    Sql8 += "0"
                    Sql8 += "', '"
                    Sql8 += frmC01F10_Login.loginValue.TantoNM
                    Sql8 += "', '"
                    Sql8 += dtToday
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
                    Sql9 += "会社コード, 年月, メーカー, 品名, 型式, 前月末数量, 前月末間接費, 今月末数量, 今月入庫数, 今月出庫数, 今月間接費, 更新者, 更新日)"
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

            'Dim Sql11 As String = ""
            'For i As Integer = 0 To ds10.Tables(RS).Rows.Count - 1
            '    Dim tmp5 As Double = 0
            '    Sql11 = ""
            '    Sql11 += "UPDATE "
            '    Sql11 += "Public."
            '    Sql11 += "t50_zikhd "
            '    Sql11 += "SET "
            '    Sql11 += " 今月末数量"
            '    Sql11 += " = '"
            '    tmp5 = ds10.Tables(RS).Rows(i)("今月末数量") + ds10.Tables(RS).Rows(i)("前月末数量")
            '    Sql11 += tmp5.ToString
            '    Sql11 += "', "
            '    Sql11 += "更新者"
            '    Sql11 += " = '"
            '    Sql11 += frmC01F10_Login.loginValue.TantoNM
            '    Sql11 += "', "
            '    Sql11 += "更新日"
            '    Sql11 += " = '"
            '    Sql11 += dtToday
            '    Sql11 += "' "

            '    Sql11 += " WHERE "
            '    Sql11 += "会社コード"
            '    Sql11 += " ILIKE  "
            '    Sql11 += "'"
            '    Sql11 += frmC01F10_Login.loginValue.BumonNM
            '    Sql11 += "'"
            '    Sql11 += " AND "
            '    Sql11 += "メーカー"
            '    Sql11 += " ILIKE  "
            '    Sql11 += "'"
            '    Sql11 += ds10.Tables(RS).Rows(i)("メーカー")
            '    Sql11 += "'"
            '    Sql11 += " AND "
            '    Sql11 += "品名"
            '    Sql11 += " ILIKE  "
            '    Sql11 += "'"
            '    Sql11 += ds10.Tables(RS).Rows(i)("品名")
            '    Sql11 += "'"
            '    Sql11 += " AND "
            '    Sql11 += "型式"
            '    Sql11 += " ILIKE  "
            '    Sql11 += "'"
            '    Sql11 += ds10.Tables(RS).Rows(i)("型式")
            '    Sql11 += "'"

            '    Sql11 += "RETURNING 会社コード"
            '    Sql11 += ", "
            '    Sql11 += "年月"
            '    Sql11 += ", "
            '    Sql11 += "メーカー"
            '    Sql11 += ", "
            '    Sql11 += "品名"
            '    Sql11 += ", "
            '    Sql11 += "型式"
            '    Sql11 += ", "
            '    Sql11 += "前月末数量"
            '    Sql11 += ", "
            '    Sql11 += "前月末間接費"
            '    Sql11 += ", "
            '    Sql11 += "今月末数量"
            '    Sql11 += ", "
            '    Sql11 += "今月入庫数"
            '    Sql11 += ", "
            '    Sql11 += "今月出庫数"
            '    Sql11 += ", "
            '    Sql11 += "今月間接費"
            '    Sql11 += ", "
            '    Sql11 += "更新者"
            '    Sql11 += ", "
            '    Sql11 += "更新日"

            '    _db.executeDB(Sql11)
            'Next
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

                Sql13 += "RETURNING 会社コード"
                Sql13 += ", "
                Sql13 += "仕入番号"
                Sql13 += ", "
                Sql13 += "発注番号"
                Sql13 += ", "
                Sql13 += "発注番号枝番"
                Sql13 += ", "
                Sql13 += "仕入先コード"
                Sql13 += ", "
                Sql13 += "仕入先名"
                Sql13 += ", "
                Sql13 += "仕入先郵便番号"
                Sql13 += ", "
                Sql13 += "仕入先住所"
                Sql13 += ", "
                Sql13 += "仕入先電話番号"
                Sql13 += ", "
                Sql13 += "仕入先ＦＡＸ"
                Sql13 += ", "
                Sql13 += "仕入先担当者役職"
                Sql13 += ", "
                Sql13 += "仕入先担当者名"
                Sql13 += ", "
                Sql13 += "支払条件"
                Sql13 += ", "
                Sql13 += "仕入金額"
                Sql13 += ", "
                Sql13 += "粗利額"
                Sql13 += ", "
                Sql13 += "営業担当者"
                Sql13 += ", "
                Sql13 += "入力担当者"
                Sql13 += ", "
                Sql13 += "備考"
                Sql13 += ", "
                Sql13 += "取消日"
                Sql13 += ", "
                Sql13 += "取消区分"
                Sql13 += ", "
                Sql13 += "ＶＡＴ"
                Sql13 += ", "
                Sql13 += "ＰＰＨ"
                Sql13 += ", "
                Sql13 += "仕入日"
                Sql13 += ", "
                Sql13 += "登録日"
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
            Sql15 += "在庫単価評価表"

            _db.executeDB(Sql15)
        End If
        DgvClosingLog.Rows.Clear()
        ClosingLogLoad()
    End Sub
End Class