Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls


Public Class StockList
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
    Private Sub StockListLoad()
        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t50_zikhd"

            Dim reccnt As Integer = 0
            ds = _db.selectDB(Sql, RS, reccnt)

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvStocklist.Rows.Add()
                DgvStocklist.Rows(index).Cells("年月").Value = ds.Tables(RS).Rows(index)("年月")
                DgvStocklist.Rows(index).Cells("メーカー").Value = ds.Tables(RS).Rows(index)("メーカー")
                DgvStocklist.Rows(index).Cells("品名").Value = ds.Tables(RS).Rows(index)("品名")
                DgvStocklist.Rows(index).Cells("型式").Value = ds.Tables(RS).Rows(index)("型式")
                DgvStocklist.Rows(index).Cells("前月末数量").Value = ds.Tables(RS).Rows(index)("前月末数量")
                DgvStocklist.Rows(index).Cells("前月末単価").Value = ds.Tables(RS).Rows(index)("前月末単価")
                DgvStocklist.Rows(index).Cells("前月末間接費").Value = ds.Tables(RS).Rows(index)("前月末間接費")
                DgvStocklist.Rows(index).Cells("今月末数量").Value = ds.Tables(RS).Rows(index)("今月末数量")
                DgvStocklist.Rows(index).Cells("今月入庫数").Value = ds.Tables(RS).Rows(index)("今月入庫数")
                DgvStocklist.Rows(index).Cells("今月出庫数").Value = ds.Tables(RS).Rows(index)("今月出庫数")
                DgvStocklist.Rows(index).Cells("今月単価").Value = ds.Tables(RS).Rows(index)("今月単価")
                DgvStocklist.Rows(index).Cells("今月間接費").Value = ds.Tables(RS).Rows(index)("今月間接費")
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
        StockListLoad()
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        Dim openForm As Form = Nothing
        openForm = New frmC01F30_Menu(_msgHd, _langHd, _db)
        openForm.Show()
        Me.Close()
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        DgvStocklist.Rows.Clear()

        Dim count As Integer = 0
        Dim Sql As String = ""
        Try
            Sql += "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t50_zikhd"
            If TxtMaker.Text = "" Then
            Else
                Sql += " WHERE "
                Sql += "メーカー"
                Sql += " ILIKE "
                Sql += "'%"
                Sql += TxtMaker.Text
                Sql += "%'"
                count += 1
            End If
            If TxtName.Text = "" Then
            Else
                If count > 0 Then
                    Sql += " AND "
                    Sql += "品名"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtName.Text
                    Sql += "%'"
                Else
                    Sql += " WHERE "
                    Sql += "品名"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtName.Text
                    Sql += "%'"
                    count += 1
                End If
            End If

            If TxtModel.Text = "" Then
            Else
                If count > 0 Then
                    Sql += " AND "
                    Sql += "型式"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtModel.Text
                    Sql += "%'"
                Else
                    Sql += " WHERE "
                    Sql += "型式"
                    Sql += " ILIKE "
                    Sql += "'%"
                    Sql += TxtModel.Text
                    Sql += "%'"
                    count += 1
                End If
            End If

            'If DtpDate1.Value.ToString = "" Then
            '    If DtpDate2.Value = "" Then
            '    Else
            '        If count > 0 Then
            '            Sql += " AND "
            '            Sql += "年月"
            '            Sql += " <=  "
            '            Sql += "'"
            '            Sql += DtpDate2.Value.ToString
            '            Sql += "'"
            '        Else
            '            Sql += " WHERE "
            '            Sql += "年月"
            '            Sql += " <=  "
            '            Sql += "'"
            '            Sql += DtpDate2.Value.ToString
            '            Sql += "'"
            '            count += 1
            '        End If
            '    End If
            'Else
            '    If DtpDate2.Value = "" Then
            '        If count > 0 Then
            '            Sql += " AND "
            '            Sql += "年月"
            '            Sql += " >=  "
            '            Sql += "'"
            '            Sql += DtpDate1.Text
            '            Sql += "'"
            '        Else
            '            Sql += " WHERE "
            '            Sql += "年月"
            '            Sql += " >=  "
            '            Sql += "'"
            '            Sql += DtpDate1.Text
            '            Sql += "'"
            '            count += 1
            '        End If
            '    Else
            '        If count > 0 Then
            '            Sql += " AND "
            '            Sql += "年月"
            '            Sql += " >=  "
            '            Sql += "'"
            '            Sql += DtpDate1.Text
            '            Sql += "' "
            '            Sql += "AND  "
            '            Sql += "年月"
            '            Sql += " <=  "
            '            Sql += "'"
            '            Sql += DtpDate2.Value
            '            Sql += "'"
            '        Else
            '            Sql += " WHERE "
            '            Sql += "年月"
            '            Sql += " >=  "
            '            Sql += "'"
            '            Sql += DtpDate1.Text
            '            Sql += "' "
            '            Sql += "AND  "
            '            Sql += "年月"
            '            Sql += " <=  "
            '            Sql += "'"
            '            Sql += DtpDate2.Value
            '            Sql += "'"
            '            count += 1
            '        End If
            '    End If
            'End If

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(Sql, RS, reccnt)

            For index As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                DgvStocklist.Rows.Add()
                DgvStocklist.Rows(index).Cells("年月").Value = ds.Tables(RS).Rows(index)("年月")
                DgvStocklist.Rows(index).Cells("メーカー").Value = ds.Tables(RS).Rows(index)("メーカー")
                DgvStocklist.Rows(index).Cells("品名").Value = ds.Tables(RS).Rows(index)("品名")
                DgvStocklist.Rows(index).Cells("型式").Value = ds.Tables(RS).Rows(index)("型式")
                DgvStocklist.Rows(index).Cells("前月末数量").Value = ds.Tables(RS).Rows(index)("前月末数量")
                DgvStocklist.Rows(index).Cells("前月末単価").Value = ds.Tables(RS).Rows(index)("前月末単価")
                DgvStocklist.Rows(index).Cells("前月末間接費").Value = ds.Tables(RS).Rows(index)("前月末間接費")
                DgvStocklist.Rows(index).Cells("今月末数量").Value = ds.Tables(RS).Rows(index)("今月末数量")
                DgvStocklist.Rows(index).Cells("今月入庫数").Value = ds.Tables(RS).Rows(index)("今月入庫数")
                DgvStocklist.Rows(index).Cells("今月出庫数").Value = ds.Tables(RS).Rows(index)("今月出庫数")
                DgvStocklist.Rows(index).Cells("今月単価").Value = ds.Tables(RS).Rows(index)("今月単価")
                DgvStocklist.Rows(index).Cells("今月間接費").Value = ds.Tables(RS).Rows(index)("今月間接費")
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