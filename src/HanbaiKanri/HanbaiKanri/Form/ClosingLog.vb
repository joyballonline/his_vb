'2020.01.09 ロケ番号→出庫開始サインに名称変更

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
    Private _com As CommonLogic

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
        _com = New CommonLogic(_db, _msgHd)
        '_gh = New UtilDataGridViewHandler(dgvLIST)                          'DataGridViewユーティリティクラス
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint                                  'フォームタイトル表示
        Me.ControlBox = Not Me.ControlBox
        _init = True

    End Sub

    Private Sub ClosingLogLoad()
        Dim Sql As String = ""
        Dim reccnt As Integer = 0

        Try

            '締処理日
            'Sql = "SELECT COALESCE(今回締日,current_date) 今回締日"
            Sql = "SELECT 今回締日"
            Sql += " FROM m01_company"
            Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

            ds = _db.selectDB(Sql, RS, reccnt)


            '今回締日を判定
            Dim strShime As String = Convert.ToString(ds.Tables(RS).Rows(0)("今回締日"))
            If strShime = vbNullString Then
                '今回締日がテーブルに登録されていない場合は前月末の日付をセットする
                Dim dtmShime As Date = DateSerial(Now.Year, Now.Month, 1)
                dtmSime.Text = DateAdd("d", -1, dtmShime)
            Else
                'テーブルの値をセットする
                dtmSime.Text = ds.Tables(RS).Rows(0)("今回締日")
            End If


            '明細
            Sql = "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public"
            Sql += "."
            Sql += "t51_smlog"
            Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

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

        '締処理日と明細
        ClosingLogLoad()


        If frmC01F10_Login.loginValue.Language = "ENG" Then
            LblPerson.Text = "Name of PIC"
            BtnClosing.Text = "Closing"
            BtnBack.Text = "Back"
            BtnSearch.Text = "Search"
            LblShime.Text = "Closing Date"

            DgvClosingLog.Columns("締処理日時").HeaderText = "ClosingDate"
            DgvClosingLog.Columns("前回締日").HeaderText = "LastClosingDate"
            DgvClosingLog.Columns("今回締日").HeaderText = "ThisClosingDate"
            DgvClosingLog.Columns("次回締日").HeaderText = "NextClosingDate"
            DgvClosingLog.Columns("担当者").HeaderText = "Name of PIC"

            BtnOutput.Text = "JournalOutput"
            LblConditions.Text = "■ExtractionCondition"
        End If


        Me.MaximizeBox = False
        'Me.Width = 1366
        'Me.Height = 600
        Me.Left = 67
        Me.Top = 180

        Me.dtmSime.Enabled = False

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

    '締処理ボタン
    Public Sub BtnClosing_Click(sender As Object, e As EventArgs) Handles BtnClosing.Click

        Call Closing_btn(0)

    End Sub

    Public Sub Closing_btn(ByVal intFlg As Integer, Optional ByVal Shime As DateTime = Nothing)

        Dim reccnt As Integer = 0
        Dim Sql1 As String = ""

        Dim ds1 As DataSet

        Dim dtmLastMonth As DateTime
        Dim dtmThisMonth As DateTime
        Dim dtmShime As DateTime


        Sql1 += "SELECT * FROM public.m01_company"
        Sql1 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

        ds1 = _db.selectDB(Sql1, RS, reccnt)


        '日付を編集
        If intFlg = 0 Then  '通常

            dtmLastMonth = UtilClass.strFormatDate(DateAdd("d", 1, ds1.Tables(RS).Rows(0)("前回締日")))  '判定用
            dtmThisMonth = UtilClass.strFormatDate(DateAdd("d", 1, ds1.Tables(RS).Rows(0)("今回締日")))
            dtmShime = UtilClass.strFormatDate(ds1.Tables(RS).Rows(0)("今回締日"))  '締日

        Else  '管理者用

            dtmThisMonth = DateAdd("d", 1, Shime)          '来月1日
            dtmLastMonth = DateAdd("m", -1, dtmThisMonth)  '今月1日
            dtmShime = Shime  '締日


            ds1.Tables(RS).Rows(0)("前回締日") = UtilClass.strFormatDate(DateAdd("d", -1, dtmLastMonth))
            ds1.Tables(RS).Rows(0)("今回締日") = UtilClass.strFormatDate(Shime)
            ds1.Tables(RS).Rows(0)("次回締日") = UtilClass.strFormatDate(DateAdd("d", -1, DateAdd("m", 1, dtmThisMonth)))

        End If



        '締日のクリアと設定
        If mClear_Shime(dtmLastMonth, dtmThisMonth, dtmShime) = False Then
            Exit Sub
        End If


        '仕訳テーブルのクリア
        '締めデータのクリア
        If mClear_Table(dtmShime, dtmLastMonth, dtmThisMonth) = False Then
            Exit Sub
        End If


        '仕訳データ作成
        'CSVファイルの書き出し処理
        If Accounting(dtmShime) = False Then
            Exit Sub
        End If


        'krtable テーブルのバックアップ　  
        If mSetkrTable(dtmShime) = False Then
            Exit Sub
        End If


        '月末在庫の集計  一時的にコメントアウト
        'If mSet_t50_zikhd() = False Then
        '    Exit Sub
        'End If


        '月末在庫の集計  
        If mSet_t68_krzaiko(dtmShime, dtmLastMonth, dtmThisMonth) = False Then
            Exit Sub
        End If


        If intFlg = 0 Then  '通常

            'ログと会社マスタの更新
            If mSetLog(ds1) = False Then
                Exit Sub
            End If
        End If



        '登録完了メッセージ
        _msgHd.dspMSG("ClosingLog", frmC01F10_Login.loginValue.Language)

        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()

    End Sub

    Private Function mSet_t68_krzaiko(ByVal dtmShime As DateTime, ByVal dtmLastMonth As DateTime, ByVal dtmThisMonth As DateTime) As Boolean

        Dim reccnt As Integer = 0



#Region "前月"

        '前月の月次在庫データを呼び出す
        '在庫が存在するデータを当月分としてinsert
        Dim dtmTemp As DateTime = DateAdd("m", -1, dtmShime)
        Dim strSyoriZnegetu As String = dtmTemp.Year & dtmTemp.Month.ToString("00")   '処理年月 前月
        Dim strSyoriTogetu As String = dtmShime.Year & dtmShime.Month.ToString("00")  '処理年月 当月

        Dim Sql As String = vbNullString
        Sql += "insert into t68_krzaiko"

        Sql += " SELECT "
        Sql += " 会社コード," & strSyoriTogetu & " as 処理年月, 倉庫コード, メーカー, 品名, 型式, 月末数量, 最終出庫日, 入庫日, "
        Sql += " 入庫単価, 発注番号, 発注番号枝番, 入庫番号, 行番号, 仕入先コード, 仕入先名, 仕入先請求番号"

        Sql += " FROM t68_krzaiko"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += "   and 処理年月 = '" & strSyoriZnegetu & "'"
        Sql += "   and 月末数量 <> 0"

        _db.executeDB(Sql)

#End Region


#Region "入庫"

        '入庫データを呼び出す 
        '未取消
        '当月分

        Sql = vbNullString
        Sql += "insert into Public.t68_krzaiko"

        Sql += " SELECT t42.会社コード," & strSyoriTogetu & " as 処理年月"
        Sql += ",t42.倉庫コード, t43.メーカー, t43.品名, t43.型式, t43.入庫数量 as 月末数量"
        Sql += ",null as 最終出庫日, t42.入庫日"
        Sql += ",t43.仕入値 as 入庫単価, t43.発注番号, t43.発注番号枝番"
        Sql += ",t43.入庫番号, t43.行番号, t42.仕入先コード, t42.仕入先名, null as 仕入先請求番号"


        Sql += " FROM t43_nyukodt as t43 left join t42_nyukohd as t42"
        Sql += " on t43.入庫番号 = t42.入庫番号"

        Sql += " WHERE t42.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += "   and t42.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
        Sql += "   and t42.締処理日 = '" & UtilClass.formatDatetime(dtmShime) & "'"

        _db.executeDB(Sql)

#End Region


#Region "出庫"

        'inoutの出庫開始サイン（旧：ロケ番号）で入庫データを検索する
        'inoutの出庫データを取得
        '未取消

        't45_shukodt   
        '出庫区分=1(通常出庫) 仮出庫を省く

        '当月としてinsert済の入庫データより在庫をマイナスする


        Sql = vbNullString
        Sql += "SELECT t45.出庫番号,t45.出庫区分,t70.*"
        Sql += " FROM t70_inout as t70 left join t45_shukodt as t45"
        Sql += " on t70.伝票番号 = t45.出庫番号 and t70.行番号 = t45.行番号"

        Sql += " WHERE t70.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += "   and t70.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
        Sql += "   and t70.入出庫区分 = '2'"

        Sql += " AND (入出庫日 >= '" & UtilClass.strFormatDate(dtmLastMonth) & "'"
        Sql += " AND  入出庫日 < '" & UtilClass.strFormatDate(dtmThisMonth) & "')"

        ''Sql += " order by ロケ番号,入出庫日"              '2020.01.09 DEL
        Sql += " order by 出庫開始サイン,入出庫日"          '2020.01.09 ADD

        Dim dsinout As DataTable = _db.selectDB(Sql, RS, reccnt).Tables(0)


        For i As Integer = 0 To dsinout.Rows.Count - 1

            '出庫区分 = 2 仮出庫を読み飛ばし 
            'whereでも弾けそうだが、抽出したデータが怪しかったのでロジックで対応
            If dsinout.Rows(i)("出庫区分") = 2 Then
                Continue For
            End If

            ''Dim strNyukoNo As String = Mid(dsinout.Rows(i)("ロケ番号"), 1, 10)            '2020.01.09 DEL
            ''Dim strGyo As String = Mid(dsinout.Rows(i)("ロケ番号"), 11)                   '2020.01.09 DEL
            Dim strNyukoNo As String = Mid(dsinout.Rows(i)("出庫開始サイン"), 1, 10)        '2020.01.09 ADD
            Dim strGyo As String = Mid(dsinout.Rows(i)("出庫開始サイン"), 11)               '2020.01.09 ADD

            'update t68_krzaiko
            Sql = vbNullString
            Sql += "UPDATE t68_krzaiko "
            Sql += " SET "
            Sql += " 月末数量  = 月末数量 - " & dsinout.Rows(i)("数量")
            Sql += ",最終出庫日 = '" & UtilClass.formatDatetime(dsinout.Rows(i)("入出庫日")) & "'"

            Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += "   and 入庫番号 = '" & strNyukoNo & "'"
            Sql += "   and 行番号 = '" & strGyo & "'"
            Sql += "   and 処理年月 = '" & strSyoriTogetu & "'"

            _db.executeDB(Sql)

        Next
        dsinout = Nothing

#End Region


#Region "仕入先請求番号"


        't43_nyukodtにjoinするとデータが余計に分かれる場合があるので
        '念の為、ループで処理する
        Sql = vbNullString
        Sql += "SELECT 発注番号,発注番号枝番"
        Sql += " FROM t68_krzaiko"

        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += "   and 処理年月 = '" & strSyoriTogetu & "'"
        Sql += " group by 発注番号,発注番号枝番"

        Dim dskrzaiko As DataTable = _db.selectDB(Sql, RS, reccnt).Tables(0)


        For i As Integer = 0 To dskrzaiko.Rows.Count - 1


            '買掛データを発注番号で呼び出す
            '月末在庫のSupllierInvoiceをupdateする
            Sql = vbNullString
            Sql += "SELECT 仕入先請求番号,発注番号,発注番号枝番"
            Sql += " FROM t46_kikehd"

            Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += "   and 発注番号 = '" & dskrzaiko.Rows(i)("発注番号") & "'"
            Sql += "   and 発注番号枝番 = '" & dskrzaiko.Rows(i)("発注番号枝番") & "'"
            Sql += "   and 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED
            Sql += " group by 仕入先請求番号,発注番号,発注番号枝番"

            Dim dskikehd As DataTable = _db.selectDB(Sql, RS, reccnt).Tables(0)

            If dskikehd.Rows.Count = 0 Then  'データがない場合は読み飛ばし
                Continue For
            End If

            If IsDBNull(dskikehd.Rows(0)("仕入先請求番号")) OrElse String.IsNullOrEmpty(dskikehd.Rows(0)("仕入先請求番号")) Then  'データがない場合は読み飛ばし
                Continue For
            End If


            'update t68_krzaiko
            Sql = vbNullString
            Sql += "UPDATE t68_krzaiko "
            Sql += " SET "
            Sql += " 仕入先請求番号  = '" & dskikehd.Rows(0)("仕入先請求番号") & "'"

            Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += "   and 発注番号 = '" & dskrzaiko.Rows(i)("発注番号") & "'"
            Sql += "   and 発注番号枝番 = '" & dskrzaiko.Rows(i)("発注番号枝番") & "'"
            Sql += "   and 処理年月 = '" & strSyoriTogetu & "'"

            _db.executeDB(Sql)

        Next
        dskrzaiko = Nothing

#End Region

        mSet_t68_krzaiko = True

    End Function

    Private Function mSetkrTable(ByVal dtmShime As DateTime) As Boolean

        Dim Sql As String
        Dim reccnt As Integer = 0
        Dim strToday As String = UtilClass.formatDatetime(dtmShime)


#Region "入庫"

        '入庫ヘッダ
        Sql = vbNullString
        Sql += "insert into Public.t58_krnyukohd "

        Sql += " select "
        Sql += "会社コード, 入庫番号, 発注番号, 発注番号枝番, 仕入先コード, 仕入先名, 仕入先郵便番号,"
        Sql += "仕入先住所, 仕入先電話番号, 仕入先ＦＡＸ, 仕入先担当者役職, 仕入先担当者名, 支払条件, "
        Sql += "仕入金額, 粗利額, ＶＡＴ, ＰＰＨ, 営業担当者, 入力担当者, 備考, 入庫日, 登録日, "
        Sql += "更新日, 更新者, 取消日, 取消区分, 客先番号, 締処理日, 営業担当者コード, 入力担当者コード,"
        Sql += "倉庫コード"

        Sql += " from t42_nyukohd"

        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND 締処理日 = '" & strToday & "'"
        Sql += " AND 取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'"

        _db.executeDB(Sql)


        '入庫明細をinsert
        Sql = vbNullString
        Sql += "SELECT * FROM public.t42_nyukohd"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND 締処理日 = '" & strToday & "'"
        Sql += " AND 取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'"

        Dim dsNyukohd As DataTable = _db.selectDB(Sql, RS, reccnt).Tables(0)


        For i As Integer = 0 To dsNyukohd.Rows.Count - 1

            Sql = vbNullString
            Sql += "insert into t59_krnyukodt"

            Sql += " SELECT "
            Sql += "会社コード, 入庫番号, 行番号, 仕入区分, メーカー, 品名, 型式, 仕入先名, 仕入値,"
            Sql += "入庫数量, 単位, 備考, 発注番号, 発注番号枝番, 更新者, 更新日"

            Sql += "  FROM Public.t43_nyukodt"

            Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND 入庫番号 = '" & dsNyukohd.Rows(i)("入庫番号") & "'"

            _db.executeDB(Sql)
        Next
        dsNyukohd = Nothing

#End Region


#Region "出庫"

        '出庫ヘッダ
        Sql = vbNullString
        Sql += "insert into Public.t60_krshukohd "

        Sql += " select "
        Sql += "会社コード, 出庫番号, 見積番号, 見積番号枝番, 受注番号, 受注番号枝番, 得意先コード, "
        Sql += "得意先名, 得意先郵便番号, 得意先住所, 得意先電話番号, 得意先ＦＡＸ, 得意先担当者役職, "
        Sql += "得意先担当者名, 営業担当者, 入力担当者, 備考, 出庫日, 登録日, 更新者, 更新日, "
        Sql += "取消日, 取消区分, 客先番号, 締処理日, 営業担当者コード, 入力担当者コード"
        Sql += " from t44_shukohd"

        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND 締処理日 = '" & strToday & "'"
        Sql += " AND 取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'"

        _db.executeDB(Sql)


        '出庫明細をinsert
        Sql = vbNullString
        Sql += "SELECT * FROM public.t44_shukohd"

        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND 締処理日 = '" & strToday & "'"
        Sql += " AND 取消区分 = '" & CommonConst.CANCEL_KBN_ENABLED & "'"

        Dim dsShukohd As DataTable = _db.selectDB(Sql, RS, reccnt).Tables(0)


        For i As Integer = 0 To dsShukohd.Rows.Count - 1

            Sql = vbNullString
            Sql += "INSERT INTO Public.t61_krshukodt"

            Sql += " SELECT "
            Sql += "会社コード, 出庫番号, 行番号, 受注番号, 受注番号枝番, 仕入区分, メーカー, 品名, "
            Sql += "型式, 仕入先名, 出庫数量, 単位, 売単価, 備考, 更新者, 更新日"
            Sql += " FROM Public.t45_shukodt"

            Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND 出庫番号 = '" & dsShukohd.Rows(i)("出庫番号") & "'"

            _db.executeDB(Sql)

        Next
        dsShukohd = Nothing

#End Region


        mSetkrTable = True

    End Function

    Private Function mSetLog(ByRef ds1 As DataSet) As Boolean

        Dim dtToday As DateTime = DateTime.Now


        't51_smlog
        Dim Sql14 As String = ""

        Sql14 = ""
        Sql14 += "INSERT INTO Public.t51_smlog("
        Sql14 += "会社コード, 処理日時, 前回締日, 今回締日, 次回締日, 担当者)"
        Sql14 += " VALUES('" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql14 += " , '" & UtilClass.formatDatetime(dtToday) & "'"
        Sql14 += " , '" & UtilClass.strFormatDate(ds1.Tables(RS).Rows(0)("前回締日")) & "'"
        Sql14 += " , '" & UtilClass.strFormatDate(ds1.Tables(RS).Rows(0)("今回締日")) & "'"
        Sql14 += " , '" & UtilClass.strFormatDate(ds1.Tables(RS).Rows(0)("次回締日")) & "'"
        Sql14 += " , '" & frmC01F10_Login.loginValue.TantoNM & "'"
        Sql14 += " )"

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


        'm01_company
        Dim dtdays As Integer = DateTime.DaysInMonth(dtYear, dtMonth)
        Dim nextClosingDate = New DateTime(dtYear, dtMonth, dtdays)
        Dim Sql15 As String = ""
        Sql15 = ""
        Sql15 += "UPDATE Public.m01_company "
        Sql15 += "SET "
        Sql15 += "前回締日  = '" & UtilClass.strFormatDate(ds1.Tables(RS).Rows(0)("今回締日")) & "'"
        Sql15 += " , 今回締日 = '" & UtilClass.strFormatDate(ds1.Tables(RS).Rows(0)("次回締日")) & "'"
        Sql15 += " , 次回締日 = '" & UtilClass.strFormatDate(nextClosingDate) & "'"

        Sql15 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        _db.executeDB(Sql15)


        mSetLog = True

    End Function


    Private Function mClear_Shime(ByVal dtmLastMonth As DateTime, ByVal dtmThisMonth As DateTime, ByVal dtmShime As DateTime) As Boolean

        Dim sql As String = vbNullString


#Region "請求"

        'クリア
        sql = "UPDATE Public.t23_skyuhd "
        sql += " SET 締処理日 = null"
        sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        sql += " AND 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"

        _db.executeDB(sql)


        '締処理日をセット
        sql = "UPDATE Public.t23_skyuhd "
        sql += " SET 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"
        sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        sql += " AND (請求日 >= '" & UtilClass.strFormatDate(dtmLastMonth) & "'"
        sql += " AND  請求日 < '" & UtilClass.strFormatDate(dtmThisMonth) & "')"

        _db.executeDB(sql)

#End Region


#Region "入金"

        sql = "UPDATE Public.t25_nkinhd "
        sql += " SET 締処理日 = null"
        sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        sql += " AND 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"

        _db.executeDB(sql)


        sql = "UPDATE Public.t25_nkinhd "
        sql += " SET 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"
        sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        sql += " AND (入金日 >= '" & UtilClass.strFormatDate(dtmLastMonth) & "'"
        sql += " AND  入金日 < '" & UtilClass.strFormatDate(dtmThisMonth) & "')"

        _db.executeDB(sql)

#End Region


#Region "入金消込"

        sql = "UPDATE Public.t27_nkinkshihd "
        sql += " SET 締処理日 = null"
        sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        sql += " AND 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"

        _db.executeDB(sql)


        sql = "UPDATE Public.t27_nkinkshihd "
        sql += " SET 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"
        sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        sql += " AND (入金日 >= '" & UtilClass.strFormatDate(dtmLastMonth) & "'"
        sql += " AND  入金日 < '" & UtilClass.strFormatDate(dtmThisMonth) & "')"

        _db.executeDB(sql)


#End Region


#Region "売上"

        sql = "UPDATE Public.t30_urighd "
        sql += " SET 締処理日 = null"
        sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        sql += " AND 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"

        _db.executeDB(sql)


        sql = "UPDATE Public.t30_urighd "
        sql += " SET 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"
        sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        sql += " AND (売上日 >= '" & UtilClass.strFormatDate(dtmLastMonth) & "'"
        sql += " AND  売上日 < '" & UtilClass.strFormatDate(dtmThisMonth) & "')"

        _db.executeDB(sql)

#End Region


#Region "仕入"

        't40_sirehd
        sql = "UPDATE Public.t40_sirehd "
        sql += " SET 締処理日 = null"
        sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        sql += " AND 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"

        _db.executeDB(sql)


        sql = "UPDATE Public.t40_sirehd "
        sql += " SET 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"
        sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        sql += " AND (仕入日 >= '" & UtilClass.strFormatDate(dtmLastMonth) & "'"
        sql += " AND  仕入日 < '" & UtilClass.strFormatDate(dtmThisMonth) & "')"

        _db.executeDB(sql)


        't41_siredt
        sql = "UPDATE Public.t41_siredt "
        sql += " SET 締処理日 = null"
        sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        sql += " AND 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"

        _db.executeDB(sql)


        sql = "UPDATE Public.t41_siredt "
        sql += " SET 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"
        sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        sql += " AND (仕入日 >= '" & UtilClass.strFormatDate(dtmLastMonth) & "'"
        sql += " AND  仕入日 < '" & UtilClass.strFormatDate(dtmThisMonth) & "')"

        _db.executeDB(sql)

#End Region


#Region "入庫"

        sql = "UPDATE Public.t42_nyukohd "
        sql += " SET 締処理日 = null"
        sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        sql += " AND 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"

        _db.executeDB(sql)


        sql = "UPDATE Public.t42_nyukohd "
        sql += " SET 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"
        sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        sql += " AND (入庫日 >= '" & UtilClass.strFormatDate(dtmLastMonth) & "'"
        sql += " AND  入庫日 < '" & UtilClass.strFormatDate(dtmThisMonth) & "')"

        _db.executeDB(sql)

#End Region


#Region "出庫"

        sql = "UPDATE Public.t44_shukohd "
        sql += " SET 締処理日 = null"
        sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        sql += " AND 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"

        _db.executeDB(sql)


        sql = "UPDATE Public.t44_shukohd "
        sql += " SET 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"
        sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        sql += " AND (出庫日 >= '" & UtilClass.strFormatDate(dtmLastMonth) & "'"
        sql += " AND  出庫日 < '" & UtilClass.strFormatDate(dtmThisMonth) & "')"

        _db.executeDB(sql)

#End Region


#Region "買掛"

        sql = "UPDATE Public.t46_kikehd "
        sql += " SET 締処理日 = null"
        sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        sql += " AND 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"

        _db.executeDB(sql)


        sql = "UPDATE Public.t46_kikehd "
        sql += " SET 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"
        sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        sql += " AND (買掛日 >= '" & UtilClass.strFormatDate(dtmLastMonth) & "'"
        sql += " AND  買掛日 < '" & UtilClass.strFormatDate(dtmThisMonth) & "')"

        _db.executeDB(sql)

#End Region


#Region "支払"

        sql = "UPDATE Public.t47_shrihd "
        sql += " SET 締処理日 = null"
        sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        sql += " AND 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"

        _db.executeDB(sql)


        sql = "UPDATE Public.t47_shrihd "
        sql += " SET 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"
        sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        sql += " AND (支払日 >= '" & UtilClass.strFormatDate(dtmLastMonth) & "'"
        sql += " AND  支払日 < '" & UtilClass.strFormatDate(dtmThisMonth) & "')"

        _db.executeDB(sql)

#End Region


#Region "支払消込"

        sql = "UPDATE Public.t49_shrikshihd "
        sql += " SET 締処理日 = null"
        sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        sql += " AND 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"

        _db.executeDB(sql)


        sql = "UPDATE Public.t49_shrikshihd "
        sql += " SET 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"
        sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        sql += " AND (支払日 >= '" & UtilClass.strFormatDate(dtmLastMonth) & "'"
        sql += " AND  支払日 < '" & UtilClass.strFormatDate(dtmThisMonth) & "')"

        _db.executeDB(sql)

#End Region


#Region "入金仕訳"

        sql = "UPDATE Public.t80_shiwakenyu "
        sql += " SET 締処理日 = null"
        sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        sql += " AND 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"

        _db.executeDB(sql)


        sql = "UPDATE Public.t80_shiwakenyu "
        sql += " SET 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"
        sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        sql += " AND (入金日 >= '" & UtilClass.strFormatDate(dtmLastMonth) & "'"
        sql += " AND  入金日 < '" & UtilClass.strFormatDate(dtmThisMonth) & "')"

        _db.executeDB(sql)

#End Region


#Region "支払仕訳"

        sql = "UPDATE Public.t81_shiwakeshi "
        sql += " SET 締処理日 = null"
        sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        sql += " AND 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"

        _db.executeDB(sql)


        sql = "UPDATE Public.t81_shiwakeshi "
        sql += " SET 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"
        sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        sql += " AND (支払日 >= '" & UtilClass.strFormatDate(dtmLastMonth) & "'"
        sql += " AND  支払日 < '" & UtilClass.strFormatDate(dtmThisMonth) & "')"

        _db.executeDB(sql)

#End Region

        mClear_Shime = True

    End Function


    Private Function mClear_Table(ByVal dtmShime As DateTime, ByVal dtmLastMonth As DateTime, ByVal dtmThisMonth As DateTime) As Boolean

        Dim strWhere As String
        Dim reccnt As Integer = 0

        '今回締処理日の仕訳データをクリア

        ''_db.executeDB("delete from t52_krurighd where ")
        ''_db.executeDB("delete from t53_krurigdt")
        ''_db.executeDB("delete from t54_krsirehd")
        ''_db.executeDB("delete from t55_krsiredt")
        ''_db.executeDB("delete from t56_krskyuhd")
        ''_db.executeDB("delete from t57_krkikehd")


#Region "入庫"

        '入庫明細を削除
        Dim Sql1 As String = ""
        Sql1 += "SELECT * FROM public.t58_krnyukohd"
        Sql1 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql1 += " AND 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"

        Dim ds1 As DataTable = _db.selectDB(Sql1, RS, reccnt).Tables(0)


        For i As Integer = 0 To ds1.Rows.Count - 1

            Sql1 = vbNullString
            Sql1 += "delete from t59_krnyukodt"
            Sql1 += " where 入庫番号 = '" & ds1.Rows(i)("入庫番号") & "'"

            _db.executeDB(Sql1)

        Next
        ds1 = Nothing


        '入庫ヘッダを削除
        strWhere = " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        strWhere += " AND 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"

        _db.executeDB("delete from t58_krnyukohd" & strWhere)

#End Region


#Region "出庫"


        '出庫明細を削除
        Sql1 = vbNullString
        Sql1 += "SELECT * FROM public.t60_krshukohd"
        Sql1 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql1 += " AND 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"

        Dim ds2 As DataTable = _db.selectDB(Sql1, RS, reccnt).Tables(0)


        For i As Integer = 0 To ds2.Rows.Count - 1

            Sql1 = vbNullString
            Sql1 += "delete from t61_krshukodt"
            Sql1 += " where 出庫番号 = '" & ds2.Rows(i)("出庫番号") & "'"

            _db.executeDB(Sql1)

        Next
        ds2 = Nothing


        '出庫ヘッダを削除
        strWhere = " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        strWhere += " AND 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"

        _db.executeDB("delete from t60_krshukohd" & strWhere)

#End Region


#Region "t68_krzaiko"

        Dim strSyoriNentuki As String = dtmShime.Year & dtmShime.Month.ToString("00")

        strWhere = " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        strWhere += " AND 処理年月 = '" & strSyoriNentuki & "'"

        _db.executeDB("delete from t68_krzaiko" & strWhere)

#End Region


        ''_db.executeDB("delete from t62_krnkinhd")
        ''_db.executeDB("delete from t63_krnkindt")
        ''_db.executeDB("delete from t64_krshrihd")
        ''_db.executeDB("delete from t65_krshridt")


        '仕訳テーブルの削除
        strWhere = " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        strWhere += " AND (仕分日 >= '" & UtilClass.strFormatDate(dtmLastMonth) & "'"
        strWhere += " AND 仕分日 < '" & UtilClass.strFormatDate(dtmThisMonth) & "')"

        _db.executeDB("delete from t66_swkhd" & strWhere)


        mClear_Table = True

    End Function


    Private Function mSet_t50_zikhd() As Boolean

        Dim reccnt As Integer = 0
        Dim dtToday As DateTime = dtmSime.Text

        Dim Sql1 As String = ""
        Sql1 += "SELECT * FROM public.m01_company"
        Sql1 += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

        Dim ds1 As DataSet = _db.selectDB(Sql1, RS, reccnt)


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

        mSet_t50_zikhd = True

    End Function

    Private Function Accounting(ByVal dtmShime As DateTime) As Boolean
        '仕訳データ作成処理

        Dim dtToday As DateTime = DateTime.Now
        Dim reccnt As Integer = 0
        Dim Sql As String = ""


#Region "仕訳前受金"
        Sql = ""
        Sql += "SELECT * FROM public.t27_nkinkshihd"
        Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += " AND 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"

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
        Sql += " AND 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"

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
        Sql += " AND 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"

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
        Sql += " AND 締処理日 = '" & UtilClass.strFormatDate(dtmShime) & "'"

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


        Accounting = True

    End Function

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
        Dim sr As System.IO.StreamWriter
        Try
            Dim srx As New System.IO.StreamWriter(sOutFile, False, enc)
            sr = srx
        Catch ex As Exception
            Cursor.Current = Cursors.Default
            MsgBox("Please close siwake.csv")
            Exit Sub
        End Try

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


            'shiwakeSql += " select t67.*,m92.会計用勘定科目コード from t67_swkhd t67,m92_kanjo m92 "
            'shiwakeSql += " WHERE t67.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and t67.会社コード = m92.会社コード and t67.""GLACCOUNT"" = m92.勘定科目名称１"
            shiwakeSql = "select t67.*,m92.会計用勘定科目コード"
            shiwakeSql += " from t67_swkhd t67 left join m92_kanjo m92 "
            shiwakeSql += " on t67.会社コード = m92.会社コード and t67.""GLACCOUNT"" = m92.勘定科目名称１"
            shiwakeSql += " WHERE t67.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

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


                Dim valGlaccountCD As String = shiwakeData.Tables(RS).Rows(i)(15).ToString()
                If valGlaccountCD = String.Empty Then '入金種別、支払種別の場合

                    Dim strTmp As String = shiwakeData.Tables(RS).Rows(i)("GLACCOUNT")

                    shiwakeSql = "select 文字３"
                    shiwakeSql += " from m90_hanyo "
                    shiwakeSql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                    shiwakeSql += "   and 固定キー ='" & strTmp.Substring(0, 1) & "'"
                    shiwakeSql += "   and 文字１ ='" & strTmp.Substring(1) & "'"

                    Dim shiwakeData2 As DataSet = _db.selectDB(shiwakeSql, RS, reccnt) 'reccnt:(省略可能)SELECT文の取得レコード件数
                    If reccnt > 0 Then
                        valGlaccountCD = shiwakeData2.Tables(RS).Rows(0)("文字３").ToString()
                    End If
                End If

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
            If dsData.Tables(RS).Rows(0)("会計用勘定科目コード") Is DBNull.Value Then
                strReturn = codeName
            Else
                strReturn = dsData.Tables(RS).Rows(0)("会計用勘定科目コード")
            End If
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
            If dsData.Tables(RS).Rows(0)("会計用仕入先コード") Is DBNull.Value Then
                strReturn = codeName
            Else
                strReturn = dsData.Tables(RS).Rows(0)("会計用仕入先コード")
            End If
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
            If dsData.Tables(RS).Rows(0)("会計用得意先コード") Is DBNull.Value Then
                strReturn = codeName
            Else
                strReturn = dsData.Tables(RS).Rows(0)("会計用得意先コード")
            End If
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
    Private Function getJVNO(ByVal seq_ As Integer) As String
        Return "'SPIN" & Format(Date.Now(), "yyyyMMdd") & "" & seq_.ToString & "'"
    End Function


    '仕訳データのXML出力
    Public Sub getShiwakeData(Optional ByVal x As String = "")
        Dim dtToday As DateTime = DateTime.Now '年月の設定
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = "" 'SQL文用
        Dim seqID As Integer 'TRANSACTIONID用変数

        Dim dsCompany As DataSet = getDsData("m01_company") 'ログイン情報から会社データの取得


#Region "仕訳買掛金"

        't40_sirehd
        't41_siredt
        't20_hattyu
        Sql = "SELECT "
        Sql += " t40.仕入日,t40.仕入番号,t40.ＶＡＴ"
        Sql += ",t40.仕入先名,t40.仕入先コード,t40.客先番号,t40.取消区分"
        Sql += ",t20.発注番号,t20.受注番号"

        Sql += " FROM t40_sirehd as t40"

        Sql += " left join t41_siredt as t41"
        Sql += " on t40.仕入番号 = t41.仕入番号"

        Sql += " left join t20_hattyu as t20"
        Sql += " on t40.発注番号 = t20.発注番号 and t40.発注番号枝番 = t20.発注番号枝番"


        Sql += " WHERE "
        Sql += " t40.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

        '条件オプション
        'Sql += " and t40.買掛日 <= '" & dsCompany.Tables(RS).Rows(0)("今回締日") & "'"

        Sql += " and t41.仕入区分 <> '" & CommonConst.Sire_KBN_Move & "'"  '「0:移動」以外
        Sql += " and t40.仕入番号 is not null"
        'Sql += " and t40.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        Sql += " group by "
        Sql += " t40.仕入日,t40.仕入番号,t40.ＶＡＴ"
        Sql += ",t40.仕入先名,t40.仕入先コード,t40.客先番号,t40.取消区分"
        Sql += ",t20.発注番号,t20.受注番号"

        Sql += " ORDER BY "
        Sql += " t40.仕入番号,t40.ＶＡＴ"
        Sql += ",t40.取消区分"
        Sql += ",t40.仕入日"


        Dim dsShiire As DataSet = _db.selectDB(Sql, RS, reccnt)


        For i As Integer = 0 To dsShiire.Tables(RS).Rows.Count - 1  't40_sirehd

            '入庫データ回しながら以下データ作成
            '借方：棚卸資産
            '貸方：Unbilled payable

            '会計用仕入先コードの取得
            Dim codeAAC As String = getSupplierName(dsShiire.Tables(RS).Rows(i)("仕入先コード"))

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
            Call mGet_money_t41_siredt(dsShiire.Tables(RS).Rows(i)("仕入番号"), dsShiire.Tables(RS).Rows(i)("ＶＡＴ") _
                                       , calVat, calCost, Indirectfees _
                                       , calKanzei, calMaebarai, calYuso)


            'DESCRIPTIONの生成 受注番号 発注番号格納
            strDESCRIPTION = mGet_DESCRIPTION(dsShiire.Tables(RS).Rows(i)("受注番号") _
                                             , dsShiire.Tables(RS).Rows(i)("発注番号") _
                                             , 0)

            If dsShiire.Tables(RS).Rows(i)("取消区分") = CommonConst.CANCEL_KBN_ENABLED Then  't40_sirehd 取消区分

                '仕入登録に伴う仕入伝票計上
                'レコード登録時、
                'かつ「仕入区分0:移動」以外かつ「取消区分0:有効」


#Region "買掛計上"

                '借方　棚卸資産  整数
                Sql = ",'" & Format(dsShiire.Tables(RS).Rows(i)("仕入日"), "yyyyMM") & "'"
                Sql += "," & seqID       'プライマリ
                Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'棚卸資産'"     '借方勘定  
                Sql += "," & UtilClass.formatNumber(formatDouble(calCost)) '仕入原価
                Sql += ",1" '固定
                Sql += ",'" & getSupplierName(dsShiire.Tables(RS).Rows(i)("仕入先コード").ToString) & "'"  '補助科目
                Sql += ",'WH-" & dsShiire.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsShiire.Tables(RS).Rows(i)("仕入日"), "yyyy-MM-dd") & "'"   '仕入日（検収日）
                Sql += ",''" '空でよし
                Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat + Indirectfees)) & "'" '仕入原価 + VAT + 間接費
                Sql += ",'" & codeAAC & "'"         '会計用仕入先コード
                Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                '貸方　Unbilled payable  負数
                countKeyID = getCount(countKeyID)

                Sql = ",'" & Format(dsShiire.Tables(RS).Rows(i)("仕入日"), "yyyyMM") & "'"
                Sql += "," & seqID       'プライマリ
                Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'Unbilled payable'"       '貸方勘定　
                Sql += "," & UtilClass.formatNumber(formatDouble(-calCost)) '仕入原価
                Sql += ",1" '固定
                Sql += ",'" & getSupplierName(dsShiire.Tables(RS).Rows(i)("仕入先コード").ToString) & "'"  '補助科目
                Sql += ",'WH-" & dsShiire.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'"  'PO
                Sql += ",'" & Format(dsShiire.Tables(RS).Rows(i)("仕入日"), "yyyy-MM-dd") & "'"    '仕入日
                Sql += ",''" '空でよし
                Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat + Indirectfees)) & "'"
                Sql += ",'" & codeAAC & "'"         '会計用仕入先コード
                Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                't67_swkhd データ登録
                updateT67Swkhd(Sql)

#End Region


            ElseIf dsShiire.Tables(RS).Rows(i)("取消区分") = CommonConst.CANCEL_KBN_DISABLED Then
                '取消に伴う仕入返品計上
                '明細にレコード登録時、
                '仕入区分0:移動」以外かつ「取消区分1:取消」

#Region "買掛取消"

                '借方　Unbilled payable
                Sql = ",'" & Format(dsShiire.Tables(RS).Rows(i)("仕入日"), "yyyyMM") & "'"
                Sql += "," & seqID 　　　'プライマリ
                Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'Unbilled payable'"       '借方勘定  
                Sql += "," & UtilClass.formatNumber(formatDouble(calCost)) '仕入原価
                Sql += ",1" '固定
                Sql += ",'" & getSupplierName(dsShiire.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                Sql += ",'WH-" & dsShiire.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsShiire.Tables(RS).Rows(i)("仕入日"), "yyyy-MM-dd") & "'"   '仕入日
                Sql += ",''" '空でよし
                Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat + Indirectfees)) & "'"
                Sql += ",'" & codeAAC & "'"         '会計用仕入先コード
                Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                '貸方　棚卸資産
                countKeyID = getCount(countKeyID)

                Sql = ",'" & Format(dsShiire.Tables(RS).Rows(i)("仕入日"), "yyyyMM") & "'"
                Sql += "," & seqID       'プライマリ
                Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'棚卸資産'"     '貸方勘定　
                Sql += "," & UtilClass.formatNumber(formatDouble(-calCost)) '仕入原価
                Sql += ",1" '固定
                Sql += ",'" & getSupplierName(dsShiire.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                Sql += ",'WH-" & dsShiire.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsShiire.Tables(RS).Rows(i)("仕入日"), "yyyy-MM-dd") & "'"   '仕入日
                Sql += ",''" '空でよし
                Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat + Indirectfees)) & "'"
                Sql += ",'" & codeAAC & "'"         '会計用仕入先コード
                Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                't67_swkhd データ登録
                updateT67Swkhd(Sql)

#End Region
            Else
                'todo:msgbox テスト
                MsgBox("エラー")
            End If
        Next

        dsShiire = Nothing

#End Region



#Region "仕訳買掛金_SupplierInvoice"

        't46_kikehd
        't40_sirehd
        't41_siredt
        't20_hattyu
        Sql = "SELECT "
        Sql += " t40.仕入番号,t40.ＶＡＴ"
        Sql += ",t40.仕入先名,t40.仕入先コード,t40.客先番号,t46.取消区分"
        Sql += ",t46.買掛日,t20.発注番号,t20.受注番号"

        Sql += " from t46_kikehd as t46"

        Sql += " left join t40_sirehd as t40"
        Sql += " on t46.発注番号 = t40.発注番号 and t46.発注番号枝番 = t40.発注番号枝番"

        Sql += " left join t41_siredt as t41"
        Sql += " on t40.仕入番号 = t41.仕入番号"

        Sql += " left join public.t20_hattyu as t20"
        Sql += " on t46.発注番号 = t20.発注番号 and t46.発注番号枝番 = t20.発注番号枝番"

        Sql += " WHERE "
        Sql += " t46.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

        '条件オプション
        'Sql += " and t40.買掛日 <= '" & dsCompany.Tables(RS).Rows(0)("今回締日") & "'"

        Sql += " and t41.仕入区分 <> '" & CommonConst.Sire_KBN_Move & "'"  '「0:移動」以外
        Sql += " and t46.買掛番号 is not null"                             '仕入と買掛処理が終了しているデータ
        Sql += " and t40.仕入番号 is not null"

        'Sql += " and t46.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        Sql += " group by "
        Sql += " t40.仕入番号,t40.ＶＡＴ"
        Sql += ",t40.仕入先名,t40.仕入先コード,t40.客先番号,t46.取消区分"
        Sql += ",t46.買掛日,t20.発注番号,t20.受注番号"

        Sql += " ORDER BY "
        Sql += " t40.仕入番号,t40.ＶＡＴ"
        Sql += ",t46.取消区分"
        Sql += ",t46.買掛日"

        Dim dsKaikake As DataSet = _db.selectDB(Sql, RS, reccnt)


        For i As Integer = 0 To dsKaikake.Tables(RS).Rows.Count - 1  't46_kikehd


            '会計用仕入先コードの取得
            Dim codeAAC As String = getSupplierName(dsKaikake.Tables(RS).Rows(i)("仕入先コード"))

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
            Call mGet_money_t41_siredt(dsKaikake.Tables(RS).Rows(i)("仕入番号"), dsKaikake.Tables(RS).Rows(i)("ＶＡＴ") _
                                       , calVat, calCost, Indirectfees _
                                       , calKanzei, calMaebarai, calYuso)


            'DESCRIPTIONの生成 受注番号 発注番号格納
            strDESCRIPTION = mGet_DESCRIPTION(dsKaikake.Tables(RS).Rows(i)("受注番号") _
                                             , dsKaikake.Tables(RS).Rows(i)("発注番号") _
                                             , 0)

            If dsKaikake.Tables(RS).Rows(i)("取消区分") = CommonConst.CANCEL_KBN_ENABLED Then  't46_kikehd 取消区分

                '買掛入力に伴う仕入伝票計上
                '※レコード登録時、
                'かつ「仕入区分0:移動」以外かつ「取消区分0:有効」


#Region "買掛計上"
                '借方　棚卸資産  整数
                Sql = ",'" & Format(dsKaikake.Tables(RS).Rows(i)("買掛日"), "yyyyMM") & "'"
                Sql += "," & seqID       'プライマリ
                Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'Unbilled payable'"     '借方勘定  
                Sql += "," & UtilClass.formatNumber(formatDouble(calCost)) '仕入原価
                Sql += ",1" '固定
                Sql += ",'" & getSupplierName(dsKaikake.Tables(RS).Rows(i)("仕入先コード").ToString) & "'"  '補助科目
                Sql += ",'WH-" & dsKaikake.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsKaikake.Tables(RS).Rows(i)("買掛日"), "yyyy-MM-dd") & "'"   '買掛日
                Sql += ",''" '空でよし
                Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat + Indirectfees)) & "'" '仕入原価 + VAT + 間接費
                Sql += ",'" & codeAAC & "'"         '会計用仕入先コード
                Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                '貸方　買掛金  負数
                countKeyID = getCount(countKeyID)

                Sql = ",'" & Format(dsKaikake.Tables(RS).Rows(i)("買掛日"), "yyyyMM") & "'"
                Sql += "," & seqID       'プライマリ
                Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'買掛金'"       '貸方勘定　
                Sql += "," & UtilClass.formatNumber(formatDouble(-calCost)) '仕入原価
                Sql += ",1" '固定
                Sql += ",'" & getSupplierName(dsKaikake.Tables(RS).Rows(i)("仕入先コード").ToString) & "'"  '補助科目
                Sql += ",'WH-" & dsKaikake.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'"  'PO
                Sql += ",'" & Format(dsKaikake.Tables(RS).Rows(i)("買掛日"), "yyyy-MM-dd") & "'"    '買掛日
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

                    Sql = ",'" & Format(dsKaikake.Tables(RS).Rows(i)("買掛日"), "yyyyMM") & "'"
                    Sql += "," & seqID       'プライマリ
                    Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（iから）
                    Sql += ",'VAT-IN'"       '借方勘定  
                    Sql += "," & UtilClass.formatNumber(formatDouble(calVat))  'VAT
                    Sql += ",1" '固定
                    Sql += ",'" & getSupplierName(dsKaikake.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                    Sql += ",'WH-" & dsKaikake.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsKaikake.Tables(RS).Rows(i)("買掛日"), "yyyy-MM-dd") & "'"   '買掛日
                    Sql += ",''" '空でよし
                    Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat + Indirectfees)) & "'"
                    Sql += ",'" & codeAAC & "'"         '会計用仕入先コード
                    Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                    't67_swkhd データ登録
                    updateT67Swkhd(Sql)


                    '貸方　買掛金
                    countKeyID = getCount(countKeyID)

                    Sql = ",'" & Format(dsKaikake.Tables(RS).Rows(i)("買掛日"), "yyyyMM") & "'"
                    Sql += "," & seqID       'プライマリ
                    Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（iから）
                    Sql += ",'買掛金'"       '貸方勘定
                    Sql += "," & UtilClass.formatNumber(formatDouble(-calVat))  'VAT
                    Sql += ",1" '固定
                    Sql += ",'" & getSupplierName(dsKaikake.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                    Sql += ",'WH-" & dsKaikake.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsKaikake.Tables(RS).Rows(i)("買掛日"), "yyyy-MM-dd") & "'"   '買掛日
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
                            strDESCRIPTION = mGet_DESCRIPTION(dsKaikake.Tables(RS).Rows(i)("受注番号") _
                                             , dsKaikake.Tables(RS).Rows(i)("発注番号") _
                                             , j + 1)

                            '借方　棚卸資産
                            countKeyID = getCount(countKeyID)

                            Sql = ",'" & Format(dsKaikake.Tables(RS).Rows(i)("買掛日"), "yyyyMM") & "'"
                            Sql += "," & seqID       'プライマリ
                            Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（0から）
                            Sql += ",'Unbilled payable'"     '借方勘定  
                            Sql += "," & UtilClass.formatNumber(formatDouble(decTemp(j)))  '間接費(関税額 or 前払法人税 or 輸送額)
                            Sql += ",1" '固定
                            Sql += ",'" & getSupplierName(dsKaikake.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                            Sql += ",'WH-" & dsKaikake.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                            Sql += ",'" & Format(dsKaikake.Tables(RS).Rows(i)("買掛日"), "yyyy-MM-dd") & "'"   '買掛日
                            Sql += ",''" '空でよし
                            Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat + Indirectfees)) & "'"
                            Sql += ",'" & codeAAC & "'"         '会計用仕入先コード
                            Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                            't67_swkhd データ登録
                            updateT67Swkhd(Sql)


                            '貸方　買掛金
                            countKeyID = getCount(countKeyID)

                            Sql = ",'" & Format(dsKaikake.Tables(RS).Rows(i)("買掛日"), "yyyyMM") & "'"
                            Sql += "," & seqID       'プライマリ
                            Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（0から）
                            Sql += ",'買掛金'"       '貸方勘定　
                            Sql += "," & UtilClass.formatNumber(formatDouble(-decTemp(j)))
                            Sql += ",1" '固定
                            Sql += ",'" & getSupplierName(dsKaikake.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                            Sql += ",'WH-" & dsKaikake.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                            Sql += ",'" & Format(dsKaikake.Tables(RS).Rows(i)("買掛日"), "yyyy-MM-dd") & "'"   '買掛日
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

            ElseIf dsKaikake.Tables(RS).Rows(i)("取消区分") = CommonConst.CANCEL_KBN_DISABLED Then
                '取消に伴う仕入返品計上
                '※レコード登録時、
                '「仕入区分0:移動」以外かつ「取消区分1:取消」

#Region "買掛取消"

                '借方　買掛金
                Sql = ",'" & Format(dsKaikake.Tables(RS).Rows(i)("買掛日"), "yyyyMM") & "'"
                Sql += "," & seqID 　　　'プライマリ
                Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'買掛金'"       '借方勘定  
                Sql += "," & UtilClass.formatNumber(formatDouble(calCost)) '仕入原価
                Sql += ",1" '固定
                Sql += ",'" & getSupplierName(dsKaikake.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                Sql += ",'WH-" & dsKaikake.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsKaikake.Tables(RS).Rows(i)("買掛日"), "yyyy-MM-dd") & "'"   '買掛日
                Sql += ",''" '空でよし
                Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat + Indirectfees)) & "'"
                Sql += ",'" & codeAAC & "'"         '会計用仕入先コード
                Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                't67_swkhd データ登録
                updateT67Swkhd(Sql)


                '貸方　棚卸資産
                countKeyID = getCount(countKeyID)

                Sql = ",'" & Format(dsKaikake.Tables(RS).Rows(i)("買掛日"), "yyyyMM") & "'"
                Sql += "," & seqID       'プライマリ
                Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（0から）
                Sql += ",'Unbilled payable'"     '貸方勘定　
                Sql += "," & UtilClass.formatNumber(formatDouble(-calCost)) '仕入原価
                Sql += ",1" '固定
                Sql += ",'" & getSupplierName(dsKaikake.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                Sql += ",'WH-" & dsKaikake.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                Sql += ",'" & Format(dsKaikake.Tables(RS).Rows(i)("買掛日"), "yyyy-MM-dd") & "'"   '買掛日
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

                    Sql = ",'" & Format(dsKaikake.Tables(RS).Rows(i)("買掛日"), "yyyyMM") & "'"
                    Sql += "," & seqID       'プライマリ
                    Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'買掛金'"       '借方勘定
                    Sql += "," & UtilClass.formatNumber(formatDouble(calVat)) 'VAT
                    Sql += ",1" '固定
                    Sql += ",'" & getSupplierName(dsKaikake.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                    Sql += ",'WH-" & dsKaikake.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsKaikake.Tables(RS).Rows(i)("買掛日"), "yyyy-MM-dd") & "'"   '買掛日
                    Sql += ",''" '空でよし
                    Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat + Indirectfees)) & "'"
                    Sql += ",'" & codeAAC & "'"         '会計用仕入先コード
                    Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                    't67_swkhd データ登録
                    updateT67Swkhd(Sql)


                    '貸方　VAT-IN
                    countKeyID = getCount(countKeyID)

                    Sql = ",'" & Format(dsKaikake.Tables(RS).Rows(i)("買掛日"), "yyyyMM") & "'"
                    Sql += "," & seqID       'プライマリ
                    Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（0から）
                    Sql += ",'VAT-IN'"       '貸方勘定
                    Sql += "," & UtilClass.formatNumber(formatDouble(-calVat)) 'VAT
                    Sql += ",1" '固定
                    Sql += ",'" & getSupplierName(dsKaikake.Tables(RS).Rows(0)("仕入先コード").ToString) & "'"      '補助科目
                    Sql += ",'WH-" & dsKaikake.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                    Sql += ",'" & Format(dsKaikake.Tables(RS).Rows(i)("買掛日"), "yyyy-MM-dd") & "'" '買掛日
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
                            strDESCRIPTION = mGet_DESCRIPTION(dsKaikake.Tables(RS).Rows(i)("受注番号") _
                                             , dsKaikake.Tables(RS).Rows(i)("発注番号") _
                                             , j + 1)

                            '借方　買掛金
                            countKeyID = getCount(countKeyID)

                            Sql = ",'" & Format(dsKaikake.Tables(RS).Rows(i)("買掛日"), "yyyyMM") & "'"
                            Sql += "," & seqID       'プライマリ
                            Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（0から）
                            Sql += ",'買掛金'"     '借方勘定  
                            Sql += "," & UtilClass.formatNumber(formatDouble(decTemp(j)))  '間接費(関税額 or 前払法人税 or 輸送額)
                            Sql += ",1" '固定
                            Sql += ",'" & getSupplierName(dsKaikake.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                            Sql += ",'WH-" & dsKaikake.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                            Sql += ",'" & Format(dsKaikake.Tables(RS).Rows(i)("買掛日"), "yyyy-MM-dd") & "'"   '買掛日
                            Sql += ",''" '空でよし
                            Sql += ",'" & UtilClass.formatNumber(formatDouble(calCost + calVat + Indirectfees)) & "'"
                            Sql += ",'" & codeAAC & "'"         '会計用仕入先コード
                            Sql += ",'" & strDESCRIPTION & "'"  'DESCRIPTION

                            't67_swkhd データ登録
                            updateT67Swkhd(Sql)


                            '貸方　棚卸資産
                            countKeyID = getCount(countKeyID)

                            Sql = ",'" & Format(dsKaikake.Tables(RS).Rows(i)("買掛日"), "yyyyMM") & "'"
                            Sql += "," & seqID       'プライマリ
                            Sql += "," & countKeyID  'TRANSACTIONID内でカウントアップ（0から）
                            Sql += ",'Unbilled payable'"       '貸方勘定　
                            Sql += "," & UtilClass.formatNumber(formatDouble(-decTemp(j)))
                            Sql += ",1" '固定
                            Sql += ",'" & getSupplierName(dsKaikake.Tables(RS).Rows(i)("仕入先コード").ToString) & "'" '補助科目
                            Sql += ",'WH-" & dsKaikake.Tables(RS).Rows(i)("客先番号").ToString & "-" & i & "'" 'PO
                            Sql += ",'" & Format(dsKaikake.Tables(RS).Rows(i)("買掛日"), "yyyy-MM-dd") & "'"   '買掛日
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

        dsKaikake = Nothing

#End Region




#Region "仕訳売掛金"

        't23_skyuhd
        't30_urighd
        Sql = "SELECT "
        Sql += " t23.客先番号,t23.受注番号"
        Sql += ",t30.売上番号,t30.ＶＡＴ"
        Sql += ",t30.得意先コード,t30.得意先名,t30.ＶＡＴ,t23.取消区分"
        Sql += ",t23.請求日"

        Sql += " FROM t23_skyuhd t23 "

        Sql += " left join t30_urighd as t30"
        Sql += " on (t23.受注番号 = t30.受注番号 and t23.受注番号枝番 = t30.受注番号枝番)"

        Sql += " left join t31_urigdt as t31"
        Sql += " on (t30.売上番号 = t31.売上番号 and t30.売上番号枝番 = t31.売上番号枝番)"

        Sql += " WHERE "
        Sql += " t23.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"

        '条件オプション
        'Sql += " and t30.請求日 <= '" & dsCompany.Tables(RS).Rows(0)("今回締日") & "'"

        Sql += " and t31.仕入区分 <> '" & CommonConst.Sire_KBN_Move & "'"  '「0:移動」以外
        Sql += " and t23.請求番号 is not null"                             '売上と請求処理が終了しているデータ
        Sql += " and t30.売上番号 is not null"

        'Sql += " and t23.取消区分 = " & CommonConst.CANCEL_KBN_ENABLED   


        Sql += " GROUP BY "
        Sql += " t23.客先番号,t23.受注番号"
        Sql += ",t30.売上番号,t30.ＶＡＴ"
        Sql += ",t30.得意先コード,t30.得意先名,t30.ＶＡＴ,t23.取消区分"
        Sql += ",t23.請求日"


        Sql += " ORDER BY "
        Sql += " t30.売上番号,t30.ＶＡＴ"
        Sql += ",t30.得意先コード"
        Sql += ",t23.取消区分,t23.請求日"

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
                Sql += ",'1" & strKamoku & "'" '貸方科目
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
                    Sql += ",'1振込手数料'"  '支払手数料
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
                    Sql += ",'1" & strKamoku & "'"  '貸方科目
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
                        Sql += ",'1振込手数料'"  '支払手数料
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
                Sql += ",'2" & strKamoku & "'"  '貸方勘定
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
                    Sql += ",'2振込手数料'" 　'支払手数料
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
                    Sql += ",'2" & strKamoku & "'"   '貸方科目
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
                        Sql += ",'2振込手数料'"  '貸方勘定
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

            calKanzei += UtilClass.rmNullDecimal(dsSWKNyukodt.Tables(RS).Rows(j)("関税額")) * dsSWKNyukodt.Tables(RS).Rows(j)("仕入数量")
            calMaebarai += UtilClass.rmNullDecimal(dsSWKNyukodt.Tables(RS).Rows(j)("前払法人税額")) * dsSWKNyukodt.Tables(RS).Rows(j)("仕入数量")
            calYuso += UtilClass.rmNullDecimal(dsSWKNyukodt.Tables(RS).Rows(j)("輸送費額")) * dsSWKNyukodt.Tables(RS).Rows(j)("仕入数量")
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
            strDESCRIPTION += " CustomsDutyParUnit"
        ElseIf intFlg = 2 Then
            '前払法人税額
            strDESCRIPTION += " PrepaidCorporateTaxAmountParUnit"
        ElseIf intFlg = 3 Then
            '輸送額
            strDESCRIPTION += " TransportationCostParUnit"
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


            calKanzei += UtilClass.rmNullDecimal(dsSwkUrigdt.Tables(RS).Rows(j)("関税額")) * dsSwkUrigdt.Tables(RS).Rows(j)("売上数量")
            calMaebarai += UtilClass.rmNullDecimal(dsSwkUrigdt.Tables(RS).Rows(j)("前払法人税額")) * dsSwkUrigdt.Tables(RS).Rows(j)("売上数量")
            calYuso += UtilClass.rmNullDecimal(dsSwkUrigdt.Tables(RS).Rows(j)("輸送費額")) * dsSwkUrigdt.Tables(RS).Rows(j)("売上数量")
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
        'Dim result As Decimal

        ' 小数点第三位で四捨五入し、小数点第二位まで出力
        Return UtilClass.roundUp(val)

        'result = Math.Round(val, 2, MidpointRounding.AwayFromZero)

        'Return result
    End Function

    Private Sub LblConditions_DoubleClick(sender As Object, e As EventArgs) Handles LblConditions.DoubleClick
        Dim openForm As Form = Nothing
        openForm = New ClosingAdmin(_msgHd, _db, _langHd, Me)
        openForm.ShowDialog()
        'Me.Hide()
    End Sub
End Class