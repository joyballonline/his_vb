'2020.01.18 t70_inout ロケ番号項目追加

Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.LANG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Imports System.IO
Imports System.Runtime.InteropServices

Public Class InitialRegistrationBulk
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

    '画面表示時
    Private Sub MstHanyoue_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If frmC01F10_Login.loginValue.Language = CommonConst.LANG_KBN_ENG Then  '英語
            BtnRegist.Text = "Bulk registration"
            BtnBack.Text = "Back"

        Else  '日本語

        End If

    End Sub


    '一括登録ボタン押下時
    Private Sub BtnRegist_Click(sender As Object, e As EventArgs) Handles BtnRegist.Click
        Dim filePath As String = ""
        Dim errorMsg As String = "" 'エラーメッセージ表示


        'ファイルを開くダイアログボックス表示
        Dim ofd As New OpenFileDialog()
        ofd.Filter = "Microsoft Excel ブック(*.xlsx)|*.xlsx" '保存ファイルの形式を指定

        'ダイアログを表示する
        If ofd.ShowDialog() = DialogResult.Cancel Then
            Exit Sub  'キャンセルの場合は終了
        End If


        filePath = ofd.FileName              'OKボタンがクリックされたとき、選択されたファイル名を取得
        Cursor.Current = Cursors.WaitCursor  'カーソルをビジー状態にする


#Region "AddTable"

        Dim addDt As New System.Data.DataTable("Table") '登録データの発注番号・枝番を格納
        Dim addRow As DataRow
        addDt.Columns.Add("No")
        addDt.Columns.Add("倉庫コード")
        addDt.Columns.Add("仕入先コード")
        addDt.Columns.Add("仕入先名")
        addDt.Columns.Add("メーカー")
        addDt.Columns.Add("品名")
        addDt.Columns.Add("型式")
        addDt.Columns.Add("製造番号")
        addDt.Columns.Add("現在在庫数")
        addDt.Columns.Add("入庫単価")
        addDt.Columns.Add("入庫金額")

#End Region


        'Try
        Dim getExcelData As System.Data.DataTable = ImportExcel(filePath) 'ファイルパスからデータを抽出

        If getExcelData.Rows.Count = 0 Then
            Exit Sub  'Excelにデータがない場合は終了
        End If


        Dim errorCnt As Integer = 0 'エラー数
        Dim saveCnt As Integer = 0 '登録可能件数
        Dim skipCnt As Integer = 0 'スキップ数


        'データチェック
        For Each row As DataRow In getExcelData.Rows

#Region "Skip"

            '空行チェック
            If row("No").ToString = "" OrElse row("倉庫コード").ToString = "" _
                OrElse row("メーカー").ToString = "" OrElse row("品名").ToString = "" OrElse row("型式").ToString = "" _
                 OrElse row("現在在庫数").ToString = "" OrElse row("入庫単価").ToString = "" Then

                skipCnt += 1 'スキップカウント

                'エラーの発注書番号を記録
                errorMsg = "No:" & row("No").ToString & vbCrLf
                Continue For '次の行へ
            End If

            '数量チェック
            If row("現在在庫数").ToString = 0 OrElse row("入庫単価").ToString = 0 Then

                skipCnt += 1 'スキップカウント

                'エラーの発注書番号を記録
                errorMsg = "No:" & row("No").ToString & vbCrLf
                Continue For '次の行へ
            End If

#End Region


#Region "Err"

            'データ型チェック 数値
            If IsNumeric(row("現在在庫数").ToString) = False OrElse IsNumeric(row("入庫単価").ToString) = False _
                OrElse IsNumeric(row("入庫金額").ToString) = False Then

                errorCnt += 1 'エラーカウント

                'エラーの発注書番号を記録
                errorMsg = "No:" & row("No").ToString & vbCrLf
                Continue For '次の行へ
            End If

            '文字数チェック
            If row("倉庫コード").ToString.Length > 20 OrElse row("メーカー").ToString.Length > 50 _
                OrElse row("品名").ToString.Length > 50 OrElse row("型式").ToString.Length > 255 Then

                errorCnt += 1 'エラーカウント

                'エラーの発注書番号を記録
                errorMsg = "No:" & row("No").ToString & vbCrLf
                Continue For '次の行へ
            End If
#End Region


            saveCnt += 1
            addRow = addDt.NewRow '行作成
            addRow("No") = row("No").ToString
            addRow("倉庫コード") = row("倉庫コード").ToString
            addRow("仕入先コード") = "ZZZZZ"
            addRow("仕入先名") = "ZZZZZ"
            addRow("メーカー") = row("メーカー").ToString
            addRow("品名") = row("品名").ToString
            addRow("型式") = row("型式").ToString
            'addRow("製造番号") = row("製造番号").ToString
            addRow("現在在庫数") = row("現在在庫数").ToString
            addRow("入庫単価") = row("入庫単価").ToString
            addRow("入庫金額") = row("入庫金額").ToString


            addDt.Rows.Add(addRow) '行追加

        Next


        'エラーがあったら
        If errorCnt > 0 Then
            '読み込みファイルエラーのアラートを出す
            _msgHd.dspMSG("importFileError", frmC01F10_Login.loginValue.Language, errorMsg)
            Exit Sub
        End If


        Dim saveMsg As String = "Registration#：" & saveCnt & vbCrLf & "Skip#：" & skipCnt

        '取消確認のアラート
        Dim result As DialogResult = _msgHd.dspMSG("registrationConfirmation",
                                                                frmC01F10_Login.loginValue.Language,
                                                                saveMsg)
        If result = DialogResult.No Then
            Exit Sub
        End If


        '買掛一括登録実行
        StockAddList(addDt) '登録一覧を渡す

        '登録完了メッセージ
        _msgHd.dspMSG("completeInsert", frmC01F10_Login.loginValue.Language)



        'Catch ex As Exception
        '    Throw ex
        'Finally
        '    'カーソルをビジー状態から元に戻す
        '    Cursor.Current = Cursors.Default

        'End Try

    End Sub


    '対象データから在庫データを登録する
    Private Sub StockAddList(ByVal addDt As System.Data.DataTable)

        Dim Sql As String = vbNullString
        Dim dtToday As DateTime = DateTime.Now
        Dim strToday As String = UtilClass.formatDatetime(dtToday)

        Dim dtmTemp As Date = DateSerial(dtToday.Year, dtToday.Month, 1)
        Dim strGessyo As String = UtilClass.strFormatDate(dtmTemp)  　'発注日など
        Dim strGessyo2 As String = UtilClass.formatDatetime(dtmTemp)  '入出庫日

#Region "insert_m11_supplier"

        Sql = " AND 仕入先コード = 'ZZZZZ'"
        Dim dtSup As System.Data.DataTable = getDsData("m11_supplier", Sql).Tables(0)

        If dtSup.Rows.Count = 0 Then

            Sql = "INSERT INTO m11_supplier"
            Sql += " VALUES("

            Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'" '会社コード
            Sql += ",'ZZZZZ'" '仕入先コード
            Sql += ",'ZZZZZ'" '仕入先名
            Sql += ",'ZZZZZ'" '仕入先名略称
            Sql += ",null" '郵便番号
            Sql += ",null" '住所１
            Sql += ",null" '住所２
            Sql += ",null" '住所３
            Sql += ",null" '電話番号
            Sql += ",null" '電話番号検索用
            Sql += ",null" 'ＦＡＸ番号
            Sql += ",null" '担当者名
            Sql += ",null" '既定間接費率
            Sql += ",null" 'メモ
            Sql += ",null" '銀行コード
            Sql += ",null" '支店コード
            Sql += ",null" '預金種目
            Sql += ",null" '口座番号
            Sql += ",null" '口座名義
            Sql += ",'" & frmC01F10_Login.loginValue.TantoNM & "'"  '更新者
            Sql += ",'" & UtilClass.formatDatetime(Now) & "'"       '更新日
            Sql += ",null" '担当者役職
            Sql += ",0" '関税率
            Sql += ",0" '前払法人税率
            Sql += ",0" '輸送費率
            Sql += ",null" '銀行名
            Sql += ",null" '支店名
            Sql += ",null" '会計用仕入先コード
            Sql += "," & CommonConst.DD_KBN_DOMESTIC  '国内区分
            Sql += ")"

            _db.executeDB(Sql)

        End If

#End Region


        Dim dv = New System.Data.DataView(addDt)  '並び替え
        dv.Sort = "倉庫コード,仕入先コード"
        addDt = dv.ToTable

        Dim MaxCnt As Long = addDt.Rows.Count  '最大カウント
        Dim lngCnt As Long = 1                 'カウント
        Dim decShiireSum As Decimal = 0        '入庫金額合計 他

        Dim strSokoCD As String = addDt.Rows(0)("倉庫コード")
        Dim strShiireCD As String = addDt.Rows(0)("仕入先コード")
        Dim WH As String = getSaiban("60", dtToday)  '入庫番号
        Dim PC As String = getSaiban("50", dtToday)  '仕入番号
        Dim PO As String = getSaiban("30", dtToday)  '発注番号


        For i As Integer = 0 To addDt.Rows.Count - 1


#Region "insert_t43_nyukodt"


            Sql = "INSERT INTO t43_nyukodt"
            Sql += " VALUES("

            Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'" '会社コード
            Sql += ",'" & WH & "'"                   '入庫番号
            Sql += "," & lngCnt                      '行番号
            Sql += "," & CommonConst.Sire_KBN_Zaiko  '仕入区分 2:在庫
            Sql += ",'" & addDt.Rows(i)("メーカー").ToString & "'"  'メーカー
            Sql += ",'" & addDt.Rows(i)("品名").ToString & "'"      '品名
            Sql += ",'" & addDt.Rows(i)("型式").ToString & "'"      '型式
            Sql += ",'" & addDt.Rows(i)("仕入先名").ToString & "'"  '仕入先名
            Sql += "," & addDt.Rows(i)("入庫単価").ToString    '仕入値
            Sql += ",null"                           '単位
            Sql += "," & addDt.Rows(i)("現在在庫数").ToString  '入庫数量
            Sql += ",null"                           '備考
            Sql += ",'" & frmC01F10_Login.loginValue.TantoNM & "'"  '更新者
            Sql += ",'" & UtilClass.formatDatetime(Now) & "'"       '更新日
            Sql += ",'" & PO & "'"  '発注番号
            Sql += ",'1'"           '発注番号枝番

            Sql += ")"

            _db.executeDB(Sql)

#End Region


#Region "insert_t41_siredt"

            Sql = "INSERT INTO t41_siredt"
            Sql += " VALUES("

            Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'" '会社コード
            Sql += ",'" & PC & "'"  '仕入番号
            Sql += "," & lngCnt     '行番号
            Sql += "," & CommonConst.Sire_KBN_Zaiko  '仕入区分 2:在庫
            Sql += ",'" & addDt.Rows(i)("メーカー") & "'"  'メーカー
            Sql += ",'" & addDt.Rows(i)("品名") & "'"      '品名
            Sql += ",'" & addDt.Rows(i)("型式") & "'"      '型式
            Sql += ",'" & addDt.Rows(i)("仕入先名") & "'"  '仕入先名
            Sql += "," & addDt.Rows(i)("入庫単価").ToString     '仕入値
            Sql += "," & addDt.Rows(i)("現在在庫数").ToString   '発注数量
            Sql += "," & addDt.Rows(i)("現在在庫数").ToString   '仕入数量
            Sql += ",0"     '発注残数
            Sql += ",null"  '単位
            Sql += ",null"  '間接費
            Sql += "," & addDt.Rows(i)("入庫単価").ToString    '仕入単価
            Sql += "," & addDt.Rows(i)("入庫金額").ToString    '仕入金額
            Sql += ",null" 'リードタイム
            Sql += ",null" '支払有無
            Sql += ",null" '支払番号
            Sql += ",null" '支払日
            Sql += ",null" '備考
            Sql += ",'" & frmC01F10_Login.loginValue.TantoNM & "'"  '更新者
            Sql += ",'" & UtilClass.formatDatetime(Now) & "'"       '更新日           
            Sql += ",'" & PO & "'"         '発注番号
            Sql += ",'1'"                  '発注番号枝番
            Sql += ",'" & strGessyo & "'"  '仕入日
            Sql += ",null"       '締処理日
            Sql += ",null"       'カウント
            Sql += "," & lngCnt  '発注行番号

            Sql += ")"

            _db.executeDB(Sql)

#End Region


#Region "insert_t21_hattyu"

            Sql = "INSERT INTO t21_hattyu"
            Sql += " VALUES("

            Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'" '会社コード
            Sql += ",'" & PO & "'"  '発注番号
            Sql += ",'1'"           '発注番号枝番
            Sql += "," & lngCnt     '行番号
            Sql += "," & CommonConst.Sire_KBN_Zaiko  '仕入区分 2:在庫
            Sql += ",'" & addDt.Rows(i)("メーカー") & "'"  'メーカー
            Sql += ",'" & addDt.Rows(i)("品名") & "'"      '品名
            Sql += ",'" & addDt.Rows(i)("型式") & "'"      '型式
            Sql += ",'" & addDt.Rows(i)("仕入先名") & "'" '仕入先名
            Sql += "," & addDt.Rows(i)("入庫単価").ToString    '仕入値
            Sql += "," & addDt.Rows(i)("現在在庫数").ToString  '発注数量
            Sql += "," & addDt.Rows(i)("現在在庫数").ToString  '仕入数量
            Sql += ",0"     '発注残数
            Sql += ",null"  '単位
            Sql += ",0"     '間接費
            Sql += "," & addDt.Rows(i)("入庫単価").ToString  '仕入単価
            Sql += "," & addDt.Rows(i)("入庫金額").ToString  '仕入金額
            Sql += ",null"                         'リードタイム
            Sql += "," & addDt.Rows(i)("現在在庫数").ToString   '入庫数
            Sql += ",0"     '未入庫数
            Sql += ",null"  '備考
            Sql += ",'" & frmC01F10_Login.loginValue.TantoNM & "'"  '更新者
            Sql += ",'" & UtilClass.formatDatetime(Now) & "'"       '登録日      
            Sql += ",1"                                             'リードタイム単位
            Sql += ",null"                                          '貿易条件
            Sql += ",'" & UtilClass.formatDatetime(Now) & "'"       '更新日      
            Sql += ",0" '見積単価_外貨
            Sql += ",0" '見積金額_外貨
            Sql += ",1" '通貨
            Sql += ",1" 'レート
            Sql += "," & addDt.Rows(i)("入庫単価").ToString    '仕入単価_外貨
            Sql += ",1" '仕入通貨
            Sql += ",1" '仕入レート
            Sql += ",0" '関税率
            Sql += ",0" '関税額
            Sql += ",0" '前払法人税率
            Sql += ",0" '前払法人税額
            Sql += ",0" '輸送費率
            Sql += ",0" '輸送費額
            Sql += "," & addDt.Rows(i)("入庫単価").ToString  '仕入値_外貨
            Sql += "," & addDt.Rows(i)("入庫金額").ToString  '仕入金額_外貨

            Sql += ")"

            _db.executeDB(Sql)

#End Region


#Region "insert_t70_inout"

            Sql = "INSERT INTO t70_inout"
            Sql += " VALUES("

            Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"   '会社コード
            Sql += ",'1'"                                           '入出庫区分 1:入庫
            Sql += ",'" & addDt.Rows(i)("倉庫コード") & "'"         '倉庫コード
            Sql += ",'" & WH & "'"                                  '伝票番号
            Sql += "," & lngCnt                                     '行番号
            Sql += ",'" & CommonConst.INOUT_KBN_NORMAL & "'"　      '入出庫種別 0:通常
            Sql += ",null"                                  　      '引当区分
            Sql += ",'" & addDt.Rows(i)("メーカー") & "'"           'メーカー
            Sql += ",'" & addDt.Rows(i)("品名") & "'"               '品名
            Sql += ",'" & addDt.Rows(i)("型式") & "'"               '型式
            Sql += "," & addDt.Rows(i)("現在在庫数").ToString       '数量
            Sql += ",null"　                                        '単位
            Sql += ",null"　                                        '備考
            Sql += ",'" & strGessyo2 & "'"     　　　　　　　　　　 '入出庫日
            Sql += ",null"                                          '取消日
            Sql += "," & CommonConst.CANCEL_KBN_ENABLED　           '取消区分 0:未取消
            Sql += ",'" & frmC01F10_Login.loginValue.TantoNM & "'"  '更新者
            Sql += ",'" & UtilClass.formatDatetime(Now) & "'"       '更新日
            Sql += ",null"　                                        '出庫開始サイン（旧：ロケ番号）
            Sql += "," & CommonConst.Sire_KBN_Zaiko                 '仕入区分 2:在庫
            Sql += ",null"                                          '製造番号
            Sql += ",null"　                                        'ロケ番号 2020.01.18 ADD

            Sql += ")"

            _db.executeDB(Sql)

#End Region

            decShiireSum += addDt.Rows(i)("入庫金額").ToString
            lngCnt += 1

            If MaxCnt < i + 2 OrElse strSokoCD <> addDt.Rows(i + 1)("倉庫コード") OrElse strShiireCD <> addDt.Rows(i + 1)("仕入先コード") Then  '仕入先ブレイク


#Region "insert_t42_nyukohd"


                Sql = "INSERT INTO t42_nyukohd"
                Sql += " VALUES("

                Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'" '会社コード
                Sql += ",'" & WH & "'"  '入庫番号
                Sql += ",'" & PO & "'"  '発注番号
                Sql += ",'1'"           '発注番号枝番
                Sql += ",'" & addDt.Rows(i)("仕入先コード") & "'"  '仕入先コード
                Sql += ",'" & addDt.Rows(i)("仕入先名") & "'"      '仕入先名
                Sql += ",null" '仕入先郵便番号
                Sql += ",null" '仕入先住所
                Sql += ",null" '仕入先電話番号
                Sql += ",null" '仕入先ＦＡＸ
                Sql += ",null" '仕入先担当者役職
                Sql += ",null" '仕入先担当者名
                Sql += ",null" '支払条件
                Sql += "," & decShiireSum '仕入金額
                Sql += ",0"               '粗利額
                Sql += ",'" & frmC01F10_Login.loginValue.TantoNM & "'"  '営業担当者
                Sql += ",'" & frmC01F10_Login.loginValue.TantoNM & "'"  '入力担当者
                Sql += ",null"                                          '備考
                Sql += ",null"                                          '取消日
                Sql += "," & CommonConst.CANCEL_KBN_ENABLED　           '取消区分 0:未取消
                Sql += ",0"     'ＶＡＴ
                Sql += ",null"  'ＰＰＨ
                Sql += ",'" & strGessyo & "'" '入庫日
                Sql += ",'" & UtilClass.formatDatetime(Now) & "'"       '登録日
                Sql += ",'" & UtilClass.formatDatetime(Now) & "'"       '更新日
                Sql += ",'" & frmC01F10_Login.loginValue.TantoNM & "'"  '更新者
                Sql += ",null" '客先番号
                Sql += ",null" '締処理日
                Sql += ",'" & frmC01F10_Login.loginValue.TantoNM & "'"  '営業担当者コード
                Sql += ",'" & frmC01F10_Login.loginValue.TantoNM & "'"  '入力担当者コード
                Sql += ",'" & addDt.Rows(i)("倉庫コード") & "'"                   '倉庫コード

                Sql += ")"

                _db.executeDB(Sql)

#End Region


#Region "insert_t40_sirehd"

                Sql = "INSERT INTO t40_sirehd"
                Sql += " VALUES("

                Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'" '会社コード
                Sql += ",'" & PC & "'"  '仕入番号
                Sql += ",'" & PO & "'"  '発注番号
                Sql += ",'1'"           '発注番号枝番
                Sql += ",'" & addDt.Rows(i)("仕入先コード") & "'" '仕入先コード
                Sql += ",'" & addDt.Rows(i)("仕入先名") & "'"     '仕入先名
                Sql += ",null" '仕入先郵便番号
                Sql += ",null" '仕入先住所
                Sql += ",null" '仕入先電話番号
                Sql += ",null" '仕入先ＦＡＸ
                Sql += ",null" '仕入先担当者役職
                Sql += ",null" '仕入先担当者名
                Sql += ",null" '支払条件
                Sql += "," & decShiireSum  '仕入金額
                Sql += ",0"                '粗利額
                Sql += ",'" & frmC01F10_Login.loginValue.TantoNM & "'"  '営業担当者
                Sql += ",'" & frmC01F10_Login.loginValue.TantoNM & "'"  '営業担当入力担当者者コード
                Sql += ",null"                                          '備考
                Sql += ",null"                                          '取消日
                Sql += "," & CommonConst.CANCEL_KBN_ENABLED             '取消区分 0:未取消
                Sql += ",0"    'ＶＡＴ
                Sql += ",null" 'ＰＰＨ
                Sql += ",'" & strGessyo & "'" '仕入日
                Sql += ",'" & UtilClass.formatDatetime(Now) & "'"       '登録日
                Sql += ",'" & UtilClass.formatDatetime(Now) & "'"       '更新日
                Sql += ",'" & frmC01F10_Login.loginValue.TantoNM & "'"  '更新者
                Sql += ",null" '締処理日
                Sql += ",null" '客先番号
                Sql += ",'" & frmC01F10_Login.loginValue.TantoNM & "'"  '営業担当者コード
                Sql += ",'" & frmC01F10_Login.loginValue.TantoNM & "'"  '入力担当者コード
                Sql += ",null" '支払予定日

                Sql += ")"

                _db.executeDB(Sql)

#End Region


#Region "insert_t20_hattyu"

                Sql = "INSERT INTO t20_hattyu"
                Sql += " VALUES("

                Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'" '会社コード
                Sql += ",'" & PO & "'" '発注番号
                Sql += ",'1'"  '発注番号枝番
                Sql += ",null" '受注番号
                Sql += ",null" '受注番号枝番
                Sql += ",null" '見積番号
                Sql += ",null" '見積番号枝番
                Sql += ",'" & addDt.Rows(i)("仕入先コード") & "'" '仕入先コード
                Sql += ",'" & addDt.Rows(i)("仕入先名") & "'"     '仕入先名
                Sql += ",null" '仕入先郵便番号
                Sql += ",null" '仕入先住所
                Sql += ",null" '仕入先電話番号
                Sql += ",null" '仕入先ＦＡＸ
                Sql += ",null" '仕入先担当者役職
                Sql += ",null" '仕入先担当者名
                Sql += ",null" '見積日
                Sql += ",null" '見積有効期限
                Sql += ",null" 'インボイス日
                Sql += ",null" '検品完了日
                Sql += ",null" '支払条件
                Sql += ",0"                '見積金額
                Sql += "," & decShiireSum  '仕入金額
                Sql += ",0"                '粗利額
                Sql += ",'" & frmC01F10_Login.loginValue.TantoNM & "'"  '営業担当者
                Sql += ",'" & frmC01F10_Login.loginValue.TantoNM & "'"  '入力担当者
                Sql += ",null"  '備考
                Sql += ",null"  '取消日
                Sql += "," & CommonConst.CANCEL_KBN_ENABLED  '取消区分 0:未取消
                Sql += ",0"     'ＶＡＴ
                Sql += ",null"  'ＰＰＨ
                Sql += ",null"  '受注日
                Sql += ",'" & strGessyo & "'"                           '発注日
                Sql += ",'" & UtilClass.formatDatetime(Now) & "'"       '登録日
                Sql += ",'" & UtilClass.formatDatetime(Now) & "'"       '更新日
                Sql += ",'" & frmC01F10_Login.loginValue.TantoNM & "'"  '更新者
                Sql += ",null" '得意先コード
                Sql += ",null" '得意先名
                Sql += ",null" '得意先郵便番号
                Sql += ",null" '得意先住所
                Sql += ",null" '得意先電話番号
                Sql += ",null" '得意先ＦＡＸ
                Sql += ",null" '得意先担当者名
                Sql += ",null" '得意先担当者役職
                Sql += ",null" '見積備考
                Sql += ",null" '客先番号
                Sql += ",0"    '出荷方法
                Sql += ",null" '出荷日
                Sql += ",null" '貿易条件
                Sql += ",'" & frmC01F10_Login.loginValue.TantoNM & "'"  '営業担当者コード
                Sql += ",'" & frmC01F10_Login.loginValue.TantoNM & "'"  '入力担当者コード
                Sql += ",'" & addDt.Rows(i)("倉庫コード") & "'"                   '倉庫コード
                Sql += ",0"     '見積金額_外貨
                Sql += ",1"     '通貨
                Sql += ",1"     'レート
                Sql += "," & decShiireSum  '仕入金額_外貨

                Sql += ")"

                _db.executeDB(Sql)

#End Region


                decShiireSum = 0
                lngCnt = 1

                If MaxCnt < i + 2 Then
                Else
                    strShiireCD = addDt.Rows(i + 1)("仕入先コード")
                    strSokoCD = addDt.Rows(i + 1)("倉庫コード")

                    WH = getSaiban("60", dtToday)  '入庫番号
                    PC = getSaiban("50", dtToday)  '仕入番号
                    PO = getSaiban("30", dtToday)  '発注番号
                End If

            End If

        Next

    End Sub


    '指定したファイルパス(Excel)を読み込み、対象データをDataTableに格納する
    Private Function ImportExcel(ByRef prmFilePath As String)
        '定義
        Dim app As Excel.Application = Nothing
        Dim books As Excel.Workbooks = Nothing
        Dim book As Excel.Workbook = Nothing
        Dim sheet As Excel.Worksheet = Nothing

        Dim dt As New System.Data.DataTable("Table")
        Dim row As DataRow

        Try
            'Excelアプリケーションの開始
            app = New Excel.Application()
            books = app.Workbooks
            book = books.Open(prmFilePath)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet) '1つ目のシートを選択

            'Excelから取得したデータと再度取得し、一括登録するデータが一致しているかどうかチェック
            Dim rowEnd As Integer = sheet.UsedRange.Rows.Count '最終行取得
            Dim colEnd As Integer = sheet.UsedRange.Columns.Count '最終列取得

            dt.Columns.Add("No")
            dt.Columns.Add("倉庫コード"）
            dt.Columns.Add("メーカー")
            dt.Columns.Add("品名")
            dt.Columns.Add("型式")
            dt.Columns.Add("製造番号")
            dt.Columns.Add("現在在庫数")
            dt.Columns.Add("入庫単価")
            dt.Columns.Add("入庫金額")

            For i As Integer = 6 To rowEnd '6行目から

                row = dt.NewRow '行作成

                row("No") = sheet.Cells(i, 1).Value
                row("倉庫コード") = sheet.Cells(i, 2).Value
                row("メーカー") = sheet.Cells(i, 3).Value
                row("品名") = sheet.Cells(i, 4).Value
                row("型式") = sheet.Cells(i, 5).Value
                'row("製造番号") = sheet.Cells(i, 5).Value
                row("現在在庫数") = sheet.Cells(i, 6).Value
                row("入庫単価") = sheet.Cells(i, 9).Value
                row("入庫金額") = sheet.Cells(i, 14).Value


                dt.Rows.Add(row) '行追加

            Next

        Catch ex As Exception

            Throw ex
        Finally

            books.Close()
            app.Quit()

            'リソースの解放
            Marshal.ReleaseComObject(sheet)
            Marshal.ReleaseComObject(book)
            Marshal.ReleaseComObject(books)
            Marshal.ReleaseComObject(app)

            'カーソルをビジー状態から元に戻す
            Cursor.Current = Cursors.Default


        End Try

        Return dt

    End Function


    '請求残高登録
    Private Sub BtnRegist2_Click(sender As Object, e As EventArgs) Handles BtnRegist2.Click

        Dim reccnt As Integer = 0
        Dim filePath As String = ""
        Dim errorMsg As String = "" 'エラーメッセージ表示


        'ファイルを開くダイアログボックス表示
        Dim ofd As New OpenFileDialog()
        ofd.Filter = "Microsoft Excel ブック(*.xlsx)|*.xlsx" '保存ファイルの形式を指定

        'ダイアログを表示する
        If ofd.ShowDialog() = DialogResult.Cancel Then
            Exit Sub  'キャンセルの場合は終了
        End If


        filePath = ofd.FileName              'OKボタンがクリックされたとき、選択されたファイル名を取得
        Cursor.Current = Cursors.WaitCursor  'カーソルをビジー状態にする


        'テーブルの項目初期設定
#Region "AddTable"

        Dim addDt As New System.Data.DataTable("Table") '登録データの発注番号・枝番を格納
        Dim addRow As DataRow
        addDt.Columns.Add("No")
        addDt.Columns.Add("請求区分")
        addDt.Columns.Add("請求日")
        addDt.Columns.Add("受注番号")
        addDt.Columns.Add("受注番号枝番")
        addDt.Columns.Add("得意先コード")
        addDt.Columns.Add("得意先名")
        addDt.Columns.Add("請求金額計")
        addDt.Columns.Add("請求消費税計")
        addDt.Columns.Add("客先番号")
        addDt.Columns.Add("入金予定日")
        addDt.Columns.Add("通貨")
        addDt.Columns.Add("レート")
        addDt.Columns.Add("備考1")
        addDt.Columns.Add("備考2")

#End Region


        Try
            Dim getExcelData As System.Data.DataTable = ImportExcel2(filePath) 'ファイルパスからデータを抽出

            If getExcelData.Rows.Count = 0 Then
                _msgHd.dspMSG("NonData", frmC01F10_Login.loginValue.Language)
                Exit Sub  'Excelにデータがない場合は終了
            End If


            Dim errorCnt As Integer = 0 'エラー数
            Dim saveCnt As Integer = 0 '登録可能件数
            Dim skipCnt As Integer = 0 'スキップ数


            'データチェック
            For Each row As DataRow In getExcelData.Rows

#Region "Skip"

                '''空行チェック
                ''If row("No").ToString = "" OrElse row("倉庫コード").ToString = "" _
                ''    OrElse row("メーカー").ToString = "" OrElse row("品名").ToString = "" OrElse row("型式").ToString = "" _
                ''     OrElse row("現在在庫数").ToString = "" OrElse row("入庫単価").ToString = "" Then

                ''    skipCnt += 1 'スキップカウント

                ''    'エラーの発注書番号を記録
                ''    errorMsg = "No:" & row("No").ToString & vbCrLf
                ''    Continue For '次の行へ
                ''End If

                '''数量チェック
                ''If row("現在在庫数").ToString = 0 OrElse row("入庫単価").ToString = 0 Then

                ''    skipCnt += 1 'スキップカウント

                ''    'エラーの発注書番号を記録
                ''    errorMsg = "No:" & row("No").ToString & vbCrLf
                ''    Continue For '次の行へ
                ''End If

#End Region


#Region "Err"

                '空白
                If row("No").ToString = vbNullString OrElse row("請求日").ToString = vbNullString _
                    OrElse row("得意先コード").ToString = vbNullString OrElse row("請求金額計").ToString = vbNullString _
                    OrElse row("請求消費税計").ToString = vbNullString OrElse row("通貨").ToString = vbNullString Then

                    errorCnt += 1 'エラーカウント

                    'エラーの発注書番号を記録
                    errorMsg = "No:" & row("No").ToString & vbCrLf
                    Continue For '次の行へ
                End If

                'データ型チェック 数値
                If IsNumeric(row("請求金額計").ToString) = False OrElse IsNumeric(row("請求消費税計").ToString) = False _
                    OrElse IsNumeric(row("通貨").ToString) = False OrElse IsNumeric(row("レート").ToString) = False Then


                    errorCnt += 1 'エラーカウント

                    'エラーの発注書番号を記録
                    errorMsg = "No:" & row("No").ToString & vbCrLf
                    Continue For '次の行へ
                End If
                If row("請求区分").ToString <> vbNullString AndAlso IsNumeric(row("請求区分").ToString) = False Then

                    errorCnt += 1 'エラーカウント

                    'エラーの発注書番号を記録
                    errorMsg = "No:" & row("No").ToString & vbCrLf
                    Continue For '次の行へ
                End If
                If row("受注番号枝番").ToString <> vbNullString AndAlso IsNumeric(row("受注番号枝番").ToString) = False Then

                    errorCnt += 1 'エラーカウント

                    'エラーの発注書番号を記録
                    errorMsg = "No:" & row("No").ToString & vbCrLf
                    Continue For '次の行へ
                End If


                'データ型チェック 日付
                If IsDate(row("請求日").ToString) = False Then

                    errorCnt += 1 'エラーカウント

                    'エラーの発注書番号を記録
                    errorMsg = "No:" & row("No").ToString & vbCrLf
                    Continue For '次の行へ
                End If
                If row("入金予定日").ToString <> vbNullString AndAlso IsDate(row("入金予定日").ToString) = False Then

                    errorCnt += 1 'エラーカウント

                    'エラーの発注書番号を記録
                    errorMsg = "No:" & row("No").ToString & vbCrLf
                    Continue For '次の行へ
                End If


                '文字数チェック
                If row("請求区分").ToString.Length > 1 _
                    OrElse row("受注番号").ToString.Length > 14 OrElse row("受注番号枝番").ToString.Length > 2 _
                    OrElse row("得意先コード").ToString.Length > 8 OrElse row("得意先名").ToString.Length > 100 _
                    OrElse row("請求金額計").ToString.Length > 17 OrElse row("請求消費税計").ToString.Length > 17 _
                    OrElse row("客先番号").ToString.Length > 50 _
                    OrElse row("通貨").ToString.Length > 1 OrElse row("レート").ToString.Length > 25 _
                    OrElse row("備考1").ToString.Length > 255 OrElse row("備考2").ToString.Length > 255 Then

                    errorCnt += 1 'エラーカウント

                    'エラーの発注書番号を記録
                    errorMsg = "No:" & row("No").ToString & vbCrLf
                    Continue For '次の行へ
                End If

                '得意先コードチェック
                Dim Sql As String = "SELECT 得意先名 from m10_customer"
                Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " AND 得意先コード = '" & row("得意先コード").ToString & "'"

                Dim dsToku As DataSet = _db.selectDB(Sql, RS, reccnt)

                If dsToku.Tables(0).Rows.Count = 0 Then
                    _msgHd.dspMSG("chkCustomerCodeError", frmC01F10_Login.loginValue.Language, errorMsg)

                    errorCnt += 1 'エラーカウント

                    'エラーの発注書番号を記録
                    errorMsg = "No:" & row("No").ToString & vbCrLf
                    Exit For
                End If

#End Region

                saveCnt += 1
                addRow = addDt.NewRow '行作成
                addRow("No") = row("No").ToString
                addRow("請求区分") = row("請求区分").ToString
                addRow("請求日") = row("請求日").ToString
                addRow("受注番号") = row("受注番号").ToString
                addRow("受注番号枝番") = row("受注番号枝番").ToString
                addRow("得意先コード") = row("得意先コード").ToString
                addRow("得意先名") = row("得意先名").ToString
                addRow("請求金額計") = row("請求金額計").ToString
                addRow("請求消費税計") = row("請求消費税計").ToString
                addRow("客先番号") = row("客先番号").ToString
                addRow("入金予定日") = row("入金予定日").ToString
                addRow("通貨") = row("通貨").ToString
                addRow("レート") = row("レート").ToString
                addRow("備考1") = row("備考1").ToString
                addRow("備考2") = row("備考2").ToString

                addDt.Rows.Add(addRow) '行追加

            Next


            'エラーがあったら
            If errorCnt > 0 Then
                '読み込みファイルエラーのアラートを出す
                _msgHd.dspMSG("importFileError", frmC01F10_Login.loginValue.Language, errorMsg)
                Exit Sub
            End If


            Dim saveMsg As String = "Registration#：" & saveCnt & vbCrLf & "Skip#：" & skipCnt

            '取消確認のアラート
            Dim result As DialogResult = _msgHd.dspMSG("registrationConfirmation",
                                                                    frmC01F10_Login.loginValue.Language,
                                                                    saveMsg)
            If result = DialogResult.No Then
                Exit Sub
            End If


            '請求一括登録実行
            BillingAddList(addDt) '登録一覧を渡す

            '登録完了メッセージ
            _msgHd.dspMSG("completeInsert", frmC01F10_Login.loginValue.Language)



        Catch ex As Exception
            Throw ex
        Finally
            'カーソルをビジー状態から元に戻す
            Cursor.Current = Cursors.Default

        End Try

    End Sub


    '対象データから請求一覧を登録する
    Private Sub BillingAddList(ByVal addDt As System.Data.DataTable)

        Dim Sql As String = vbNullString
        Dim reccnt As Integer = 0
        Dim dtToday As DateTime = DateTime.Now
        Dim strToday As String = UtilClass.formatDatetime(dtToday)

        Dim dtmTemp As Date = DateSerial(dtToday.Year, dtToday.Month, 1)


        Dim dv = New System.Data.DataView(addDt)  '並び替え
        dv.Sort = "請求日,得意先コード"
        addDt = dv.ToTable

        For i As Integer = 0 To addDt.Rows.Count - 1

            '''レートの取得
            ''Dim strRate As Decimal = setRate(dsCymnhd.Tables(RS).Rows(0)("通貨").ToString())

            '請求区分
            Dim strKubun As String
            If String.IsNullOrEmpty(addDt.Rows(i)("請求区分").ToString) Then
                strKubun = "2"  '通常
            Else
                strKubun = addDt.Rows(i)("請求区分").ToString
            End If

            '得意先名
            Dim strTokui As String
            If String.IsNullOrEmpty(addDt.Rows(i)("得意先名").ToString) Then

                Sql = "SELECT 得意先名 from m10_customer"
                Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " AND 得意先コード = '" & addDt.Rows(i)("得意先コード").ToString & "'"

                Dim dsToku As DataSet = _db.selectDB(Sql, RS, reccnt)
                strTokui = dsToku.Tables(0).Rows(0)("得意先名").ToString

            Else
                strTokui = addDt.Rows(i)("得意先名").ToString
            End If


            '請求金額計_外貨
            Dim AmountFC As Decimal = addDt.Rows(i)("請求金額計").ToString / addDt.Rows(i)("レート").ToString


            '採番データを取得・更新
            Dim DM As String = getSaiban("80", dtToday)


#Region "insert_t23_skyuhd"

            Sql = "INSERT INTO "
            Sql += "Public."
            Sql += "t23_skyuhd("
            Sql += "会社コード, 請求番号, 請求区分, 請求日, 受注番号, 受注番号枝番, 客先番号, 得意先コード, 得意先名"
            Sql += ", 請求金額計, 請求消費税計, 入金額計, 売掛残高, 備考1, 備考2, 取消区分, 入金予定日, 登録日, 更新者, 更新日"
            Sql += ", 請求金額計_外貨, 入金額計_外貨, 売掛残高_外貨, 通貨, レート)"
            Sql += " VALUES("
            Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'"                           '会社コード
            Sql += ", '" & DM & "'"                                                         '請求番号
            Sql += ", '" & strKubun & "'"　　　　                                           '請求区分
            Sql += ", '" & UtilClass.strFormatDate(addDt.Rows(i)("請求日").ToString) & "'"  '請求日
            Sql += ", '" & addDt.Rows(i)("受注番号").ToString & "'"                         '受注番号
            Sql += ", '" & addDt.Rows(i)("受注番号枝番").ToString & "'"                     '受注番号枝番
            Sql += ", '" & addDt.Rows(i)("客先番号").ToString & "'"                         '客先番号
            Sql += ", '" & addDt.Rows(i)("得意先コード").ToString & "'"                     '得意先コード
            Sql += ", '" & strTokui & "'"                                                   '得意先名
            Sql += ", " & UtilClass.formatNumber(addDt.Rows(i)("請求金額計").ToString)      '請求金額計  
            Sql += ", " & UtilClass.formatNumber(addDt.Rows(i)("請求消費税計").ToString)    '請求消費税計  
            Sql += ", 0"                                                                    '入金額計を0で設定
            Sql += ", 0"                                                                    '売掛残高
            Sql += ", '" & addDt.Rows(i)("備考1").ToString & "'"                            '備考1
            Sql += ", '" & addDt.Rows(i)("備考2").ToString & "'"                            '備考2
            Sql += ", 0"                                                                    '取消区分
            If addDt.Rows(i)("入金予定日").ToString = vbNullString Then
                Sql += ", null"  '入金予定日
            Else
                Sql += ", '" & UtilClass.strFormatDate(addDt.Rows(i)("入金予定日").ToString) & "'"  '入金予定日
            End If
            Sql += ", current_timestamp"                                                    '登録日
            Sql += ", '" & frmC01F10_Login.loginValue.TantoNM & "'"                         '更新者
            Sql += ", current_timestamp"                                                    '更新日

            Sql += ", " & UtilClass.formatNumber(AmountFC)                                  '請求金額計_外貨
            Sql += ", 0"                                                                    '入金額計_外貨を0で設定
            Sql += ", 0"                                                                    '売掛残高_外貨

            Sql += ", '" & addDt.Rows(i)("通貨").ToString & "'"                             '通貨
            Sql += ", " & UtilClass.formatNumberF10(addDt.Rows(i)("レート").ToString)       'レート

            Sql += ")"

            _db.executeDB(Sql)

#End Region


        Next

    End Sub


    '指定したファイルパス(Excel)を読み込み、対象データをDataTableに格納する
    Private Function ImportExcel2(ByRef prmFilePath As String)
        '定義
        Dim app As Excel.Application = Nothing
        Dim books As Excel.Workbooks = Nothing
        Dim book As Excel.Workbook = Nothing
        Dim sheet As Excel.Worksheet = Nothing

        Dim dt As New System.Data.DataTable("Table")
        Dim row As DataRow

        Try
            'Excelアプリケーションの開始
            app = New Excel.Application()
            books = app.Workbooks
            book = books.Open(prmFilePath)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet) '1つ目のシートを選択

            'Excelから取得したデータと再度取得し、一括登録するデータが一致しているかどうかチェック
            Dim rowEnd As Integer = sheet.UsedRange.Rows.Count '最終行取得
            Dim colEnd As Integer = sheet.UsedRange.Columns.Count '最終列取得

            dt.Columns.Add("No")
            dt.Columns.Add("請求区分")
            dt.Columns.Add("請求日")
            dt.Columns.Add("受注番号")
            dt.Columns.Add("受注番号枝番")
            dt.Columns.Add("得意先コード")
            dt.Columns.Add("得意先名")
            dt.Columns.Add("請求金額計")
            dt.Columns.Add("請求消費税計")
            dt.Columns.Add("客先番号")
            dt.Columns.Add("入金予定日")
            dt.Columns.Add("通貨")
            dt.Columns.Add("レート")
            dt.Columns.Add("備考1")
            dt.Columns.Add("備考2")


            For i As Integer = 2 To rowEnd '2行目から

                row = dt.NewRow '行作成

                row("No") = sheet.Cells(i, 1).Value
                row("請求区分") = sheet.Cells(i, 2).Value
                row("請求日") = sheet.Cells(i, 3).Value
                row("受注番号") = sheet.Cells(i, 4).Value
                row("受注番号枝番") = sheet.Cells(i, 5).Value
                row("得意先コード") = sheet.Cells(i, 6).Value
                row("得意先名") = sheet.Cells(i, 7).Value
                row("請求金額計") = sheet.Cells(i, 8).Value
                row("請求消費税計") = sheet.Cells(i, 9).Value
                row("客先番号") = sheet.Cells(i, 10).Value
                row("入金予定日") = sheet.Cells(i, 11).Value
                row("通貨") = sheet.Cells(i, 12).Value
                row("レート") = sheet.Cells(i, 13).Value
                row("備考1") = sheet.Cells(i, 14).Value
                row("備考2") = sheet.Cells(i, 15).Value

                dt.Rows.Add(row) '行追加

            Next

        Catch ex As Exception

            Throw ex
        Finally

            books.Close()
            app.Quit()

            'リソースの解放
            Marshal.ReleaseComObject(sheet)
            Marshal.ReleaseComObject(book)
            Marshal.ReleaseComObject(books)
            Marshal.ReleaseComObject(app)

            'カーソルをビジー状態から元に戻す
            Cursor.Current = Cursors.Default


        End Try

        Return dt

    End Function



    '買掛残高登録
    Private Sub BtnRegist3_Click(sender As Object, e As EventArgs) Handles BtnRegist3.Click

        Dim reccnt As Integer = 0
        Dim filePath As String = ""
        Dim errorMsg As String = "" 'エラーメッセージ表示


        'ファイルを開くダイアログボックス表示
        Dim ofd As New OpenFileDialog()
        ofd.Filter = "Microsoft Excel ブック(*.xlsx)|*.xlsx" '保存ファイルの形式を指定

        'ダイアログを表示する
        If ofd.ShowDialog() = DialogResult.Cancel Then
            Exit Sub  'キャンセルの場合は終了
        End If


        filePath = ofd.FileName              'OKボタンがクリックされたとき、選択されたファイル名を取得
        Cursor.Current = Cursors.WaitCursor  'カーソルをビジー状態にする


        'テーブルの項目初期設定
#Region "AddTable"

        Dim addDt As New System.Data.DataTable("Table") '登録データの発注番号・枝番を格納
        Dim addRow As DataRow
        addDt.Columns.Add("No")
        addDt.Columns.Add("買掛区分")
        addDt.Columns.Add("買掛日")
        addDt.Columns.Add("発注番号")
        addDt.Columns.Add("発注番号枝番")
        addDt.Columns.Add("仕入先コード")
        addDt.Columns.Add("仕入先名")
        addDt.Columns.Add("買掛金額計")
        addDt.Columns.Add("買掛消費税計")
        addDt.Columns.Add("客先番号")
        addDt.Columns.Add("支払予定日")
        addDt.Columns.Add("仕入先請求番号")
        addDt.Columns.Add("通貨")
        addDt.Columns.Add("レート")
        addDt.Columns.Add("備考1")
        addDt.Columns.Add("備考2")

#End Region


        Try
            Dim getExcelData As System.Data.DataTable = ImportExcel3(filePath) 'ファイルパスからデータを抽出

            If getExcelData.Rows.Count = 0 Then
                Exit Sub  'Excelにデータがない場合は終了
            End If


            Dim errorCnt As Integer = 0 'エラー数
            Dim saveCnt As Integer = 0 '登録可能件数
            Dim skipCnt As Integer = 0 'スキップ数


            'データチェック
            For Each row As DataRow In getExcelData.Rows

#Region "Skip"

                '''空行チェック
                ''If row("No").ToString = "" OrElse row("倉庫コード").ToString = "" _
                ''    OrElse row("メーカー").ToString = "" OrElse row("品名").ToString = "" OrElse row("型式").ToString = "" _
                ''     OrElse row("現在在庫数").ToString = "" OrElse row("入庫単価").ToString = "" Then

                ''    skipCnt += 1 'スキップカウント

                ''    'エラーの発注書番号を記録
                ''    errorMsg = "No:" & row("No").ToString & vbCrLf
                ''    Continue For '次の行へ
                ''End If

                '''数量チェック
                ''If row("現在在庫数").ToString = 0 OrElse row("入庫単価").ToString = 0 Then

                ''    skipCnt += 1 'スキップカウント

                ''    'エラーの発注書番号を記録
                ''    errorMsg = "No:" & row("No").ToString & vbCrLf
                ''    Continue For '次の行へ
                ''End If

#End Region


#Region "Err"

                '空白
                If row("No").ToString = vbNullString OrElse row("買掛日").ToString = vbNullString _
                    OrElse row("仕入先コード").ToString = vbNullString OrElse row("買掛金額計").ToString = vbNullString _
                    OrElse row("買掛消費税計").ToString = vbNullString OrElse row("通貨").ToString = vbNullString Then

                    errorCnt += 1 'エラーカウント

                    'エラーの発注書番号を記録
                    errorMsg = "No:" & row("No").ToString & vbCrLf
                    Continue For '次の行へ
                End If

                'データ型チェック 数値
                If IsNumeric(row("買掛金額計").ToString) = False OrElse IsNumeric(row("買掛消費税計").ToString) = False _
                    OrElse IsNumeric(row("通貨").ToString) = False OrElse IsNumeric(row("レート").ToString) = False Then


                    errorCnt += 1 'エラーカウント

                    'エラーの発注書番号を記録
                    errorMsg = "No:" & row("No").ToString & vbCrLf
                    Continue For '次の行へ
                End If
                If row("買掛区分").ToString <> vbNullString AndAlso IsNumeric(row("買掛区分").ToString) = False Then

                    errorCnt += 1 'エラーカウント

                    'エラーの発注書番号を記録
                    errorMsg = "No:" & row("No").ToString & vbCrLf
                    Continue For '次の行へ
                End If
                If row("発注番号枝番").ToString <> vbNullString AndAlso IsNumeric(row("発注番号枝番").ToString) = False Then

                    errorCnt += 1 'エラーカウント

                    'エラーの発注書番号を記録
                    errorMsg = "No:" & row("No").ToString & vbCrLf
                    Continue For '次の行へ
                End If


                'データ型チェック 日付
                If IsDate(row("買掛日").ToString) = False Then


                    errorCnt += 1 'エラーカウント

                    'エラーの発注書番号を記録
                    errorMsg = "No:" & row("No").ToString & vbCrLf
                    Continue For '次の行へ
                End If
                If row("支払予定日").ToString <> vbNullString AndAlso IsDate(row("支払予定日").ToString) = False Then

                    errorCnt += 1 'エラーカウント

                    'エラーの発注書番号を記録
                    errorMsg = "No:" & row("No").ToString & vbCrLf
                    Continue For '次の行へ
                End If


                '文字数チェック
                If row("買掛区分").ToString.Length > 1 _
                    OrElse row("発注番号").ToString.Length > 14 OrElse row("発注番号枝番").ToString.Length > 2 _
                    OrElse row("仕入先コード").ToString.Length > 8 OrElse row("仕入先名").ToString.Length > 100 _
                    OrElse row("買掛金額計").ToString.Length > 17 OrElse row("買掛消費税計").ToString.Length > 17 _
                    OrElse row("客先番号").ToString.Length > 50 OrElse row("仕入先請求番号").ToString.Length > 100 _
                    OrElse row("通貨").ToString.Length > 1 OrElse row("レート").ToString.Length > 25 _
                    OrElse row("備考1").ToString.Length > 255 OrElse row("備考2").ToString.Length > 255 Then

                    errorCnt += 1 'エラーカウント

                    'エラーの発注書番号を記録
                    errorMsg = "No:" & row("No").ToString & vbCrLf
                    Continue For '次の行へ
                End If


                '仕入先コードチェック
                Dim Sql As String = "SELECT 仕入先名 from m11_supplier"
                Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " AND 仕入先コード = '" & row("仕入先コード").ToString & "'"

                Dim dsSiire As DataSet = _db.selectDB(Sql, RS, reccnt)

                If dsSiire.Tables(0).Rows.Count = 0 Then
                    _msgHd.dspMSG("chkSupplierCodeError", frmC01F10_Login.loginValue.Language, errorMsg)

                    errorCnt += 1 'エラーカウント

                    'エラーの発注書番号を記録
                    errorMsg = "No:" & row("No").ToString & vbCrLf
                    Exit For
                End If

#End Region

                saveCnt += 1
                addRow = addDt.NewRow '行作成
                addRow("No") = row("No").ToString
                addRow("買掛区分") = row("買掛区分").ToString
                addRow("買掛日") = row("買掛日").ToString
                addRow("発注番号") = row("発注番号").ToString
                addRow("発注番号枝番") = row("発注番号枝番").ToString
                addRow("仕入先コード") = row("仕入先コード").ToString
                addRow("仕入先名") = row("仕入先名").ToString
                addRow("買掛金額計") = row("買掛金額計").ToString
                addRow("買掛消費税計") = row("買掛消費税計").ToString
                addRow("客先番号") = row("客先番号").ToString
                addRow("支払予定日") = row("支払予定日").ToString
                addRow("仕入先請求番号") = row("仕入先請求番号").ToString
                addRow("通貨") = row("通貨").ToString
                addRow("レート") = row("レート").ToString
                addRow("備考1") = row("備考1").ToString
                addRow("備考2") = row("備考2").ToString


                addDt.Rows.Add(addRow) '行追加

            Next


            'エラーがあったら
            If errorCnt > 0 Then
                '読み込みファイルエラーのアラートを出す
                _msgHd.dspMSG("importFileError", frmC01F10_Login.loginValue.Language, errorMsg)
                Exit Sub
            End If


            Dim saveMsg As String = "Registration#：" & saveCnt & vbCrLf & "Skip#：" & skipCnt

            '取消確認のアラート
            Dim result As DialogResult = _msgHd.dspMSG("registrationConfirmation",
                                                                    frmC01F10_Login.loginValue.Language,
                                                                    saveMsg)
            If result = DialogResult.No Then
                Exit Sub
            End If


            '買掛残高登録実行
            AccountsPayableAddList(addDt) '登録一覧を渡す

            '登録完了メッセージ
            _msgHd.dspMSG("completeInsert", frmC01F10_Login.loginValue.Language)


        Catch ex As Exception
            Throw ex
        Finally
            'カーソルをビジー状態から元に戻す
            Cursor.Current = Cursors.Default

        End Try

    End Sub

    '対象データから買掛残高を登録する
    Private Sub AccountsPayableAddList(ByVal addDt As System.Data.DataTable)

        Dim Sql As String = vbNullString
        Dim reccnt As Integer = 0
        Dim dtToday As DateTime = DateTime.Now
        Dim strToday As String = UtilClass.formatDatetime(dtToday)

        Dim dtmTemp As Date = DateSerial(dtToday.Year, dtToday.Month, 1)
        Dim strGessyo As String = UtilClass.strFormatDate(dtmTemp)  　'発注日など
        Dim strGessyo2 As String = UtilClass.formatDatetime(dtmTemp)  '入出庫日


        Dim dv = New System.Data.DataView(addDt)  '並び替え
        dv.Sort = "買掛日,仕入先コード"
        addDt = dv.ToTable

        For i As Integer = 0 To addDt.Rows.Count - 1

            '買掛区分
            Dim strKubun As String
            If String.IsNullOrEmpty(addDt.Rows(i)("買掛区分").ToString) Then
                strKubun = "2"  '通常
            Else
                strKubun = addDt.Rows(i)("買掛区分").ToString
            End If


            '仕入先名
            Dim strSiire As String
            If String.IsNullOrEmpty(addDt.Rows(i)("仕入先名").ToString) Then

                Sql = "SELECT 仕入先名 from m11_supplier"
                Sql += " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
                Sql += " AND 仕入先コード = '" & addDt.Rows(i)("仕入先コード").ToString & "'"

                Dim dsSiire As DataSet = _db.selectDB(Sql, RS, reccnt)
                strSiire = dsSiire.Tables(0).Rows(0)("仕入先名").ToString

            Else
                strSiire = addDt.Rows(i)("仕入先名").ToString
            End If

            Dim BuyToHangAmountFC As Decimal = addDt.Rows(i)("買掛金額計").ToString / addDt.Rows(i)("レート").ToString

            '採番
            Dim AP As String = getSaiban("100", dtToday)


#Region "insert_t46_kikehd"

            Sql = "INSERT INTO "
            Sql += "Public."
            Sql += "t46_kikehd("
            Sql += "会社コード, 買掛番号, 買掛区分, 買掛日, 発注番号, 発注番号枝番, 客先番号, 仕入先コード, 仕入先名, 買掛金額計, 買掛消費税計, 買掛残高"
            Sql += ", 備考1, 備考2, 取消区分, 登録日, 更新者, 更新日, 支払予定日"
            Sql += ", 買掛金額計_外貨, 買掛残高_外貨, 通貨, レート, 仕入先請求番号)"

            Sql += " VALUES('"
            Sql += frmC01F10_Login.loginValue.BumonCD                                      '会社コード
            Sql += "','" & AP & "'"                                                        '買掛番号
            Sql += ",'" & strKubun & "'"                                                   '買掛区分
            Sql += ",'" & UtilClass.strFormatDate(addDt.Rows(i)("買掛日").ToString) & "'"  '買掛日
            Sql += ",'" & addDt.Rows(i)("発注番号").ToString & "'"                         '発注番号
            Sql += ",'" & addDt.Rows(i)("発注番号枝番").ToString & "'"                     '発注番号枝番
            Sql += ",'" & addDt.Rows(i)("客先番号").ToString & "'"                         '客先番号
            Sql += ",'" & addDt.Rows(i)("仕入先コード").ToString & "'"                     '仕入先コード
            Sql += ",'" & strSiire & "'"                                                   '仕入先名
            Sql += "," & UtilClass.formatNumber(addDt.Rows(i)("買掛金額計").ToString)      '買掛金額計
            Sql += "," & UtilClass.formatNumber(addDt.Rows(i)("買掛消費税計").ToString)      '買掛消費税計
            Sql += ",0"                                                                    '買掛残高
            Sql += ",'" & addDt.Rows(i)("備考1").ToString & "'"                            '備考1
            Sql += ",'" & addDt.Rows(i)("備考2").ToString & "'"                            '備考2
            Sql += ",'0'"                                                                  '取消区分
            Sql += ", current_timestamp"                                                   '登録日
            Sql += ",'" & frmC01F10_Login.loginValue.TantoNM & "'"                         '更新者
            Sql += ", current_timestamp"                                                   '更新日
            If addDt.Rows(i)("支払予定日").ToString = vbNullString Then
                Sql += ", null"  '支払予定日
            Else
                Sql += ", '" & UtilClass.strFormatDate(addDt.Rows(i)("支払予定日").ToString) & "'"  '支払予定日
            End If
            Sql += "," & UtilClass.formatNumber(BuyToHangAmountFC)                           '買掛金額計_外貨
            Sql += ",0"                                                                      '買掛残高_外貨
            Sql += "," & addDt.Rows(i)("通貨").ToString                                      '通貨
            Sql += ",'" & UtilClass.formatNumberF10(addDt.Rows(i)("レート").ToString) & "'"  'レート
            Sql += ",'" & addDt.Rows(i)("仕入先請求番号").ToString                           '仕入先請求番号

            Sql += "')"

            _db.executeDB(Sql)

#End Region


        Next

    End Sub


    '指定したファイルパス(Excel)を読み込み、対象データをDataTableに格納する
    Private Function ImportExcel3(ByRef prmFilePath As String)
        '定義
        Dim app As Excel.Application = Nothing
        Dim books As Excel.Workbooks = Nothing
        Dim book As Excel.Workbook = Nothing
        Dim sheet As Excel.Worksheet = Nothing

        Dim dt As New System.Data.DataTable("Table")
        Dim row As DataRow

        Try
            'Excelアプリケーションの開始
            app = New Excel.Application()
            books = app.Workbooks
            book = books.Open(prmFilePath)  'テンプレート
            sheet = CType(book.Worksheets(1), Excel.Worksheet) '1つ目のシートを選択

            'Excelから取得したデータと再度取得し、一括登録するデータが一致しているかどうかチェック
            Dim rowEnd As Integer = sheet.UsedRange.Rows.Count '最終行取得
            Dim colEnd As Integer = sheet.UsedRange.Columns.Count '最終列取得

            dt.Columns.Add("No")
            dt.Columns.Add("買掛区分")
            dt.Columns.Add("買掛日")
            dt.Columns.Add("発注番号")
            dt.Columns.Add("発注番号枝番")
            dt.Columns.Add("仕入先コード")
            dt.Columns.Add("仕入先名")
            dt.Columns.Add("買掛金額計")
            dt.Columns.Add("買掛消費税計")
            dt.Columns.Add("客先番号")
            dt.Columns.Add("支払予定日")
            dt.Columns.Add("仕入先請求番号")
            dt.Columns.Add("通貨")
            dt.Columns.Add("レート")
            dt.Columns.Add("備考1")
            dt.Columns.Add("備考2")

            For i As Integer = 2 To rowEnd '2行目から

                row = dt.NewRow '行作成

                row("No") = sheet.Cells(i, 1).Value
                row("買掛区分") = sheet.Cells(i, 2).Value
                row("買掛日") = sheet.Cells(i, 3).Value
                row("発注番号") = sheet.Cells(i, 4).Value
                row("発注番号枝番") = sheet.Cells(i, 5).Value
                row("仕入先コード") = sheet.Cells(i, 6).Value
                row("仕入先名") = sheet.Cells(i, 7).Value
                row("買掛金額計") = sheet.Cells(i, 8).Value
                row("買掛消費税計") = sheet.Cells(i, 9).Value
                row("客先番号") = sheet.Cells(i, 10).Value
                row("支払予定日") = sheet.Cells(i, 11).Value
                row("仕入先請求番号") = sheet.Cells(i, 12).Value
                row("通貨") = sheet.Cells(i, 13).Value
                row("レート") = sheet.Cells(i, 14).Value
                row("備考1") = sheet.Cells(i, 15).Value
                row("備考2") = sheet.Cells(i, 16).Value

                dt.Rows.Add(row) '行追加

            Next

        Catch ex As Exception

            Throw ex
        Finally

            books.Close()
            app.Quit()

            'リソースの解放
            Marshal.ReleaseComObject(sheet)
            Marshal.ReleaseComObject(book)
            Marshal.ReleaseComObject(books)
            Marshal.ReleaseComObject(app)

            'カーソルをビジー状態から元に戻す
            Cursor.Current = Cursors.Default


        End Try

        Return dt

    End Function



    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub


    'param1：String テーブル名
    'param2：String 詳細条件
    'Return: DataSet
    Private Function getDsData(ByVal tableName As String, Optional ByRef txtParam As String = "") As DataSet
        Dim reccnt As Integer = 0 'DB用（デフォルト）
        Dim Sql As String = ""

        Sql += "SELECT * FROM public." & tableName
        Sql += " WHERE "
        Sql += "会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        Sql += txtParam
        Return _db.selectDB(Sql, RS, reccnt)
    End Function

    '基準通貨の通貨コードを取得する
    Private Function setBaseCurrency() As String
        Dim Sql As String
        '通貨表示：ベースの設定
        Sql = " AND 採番キー = " & CommonConst.CURRENCY_CD_IDR.ToString
        Sql += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED.ToString

        Dim ds As DataSet = getDsData("m25_currency", Sql)
        'TxtIDRCurrency.Text = ds.Tables(RS).Rows(0)("通貨コード")
        setBaseCurrency = ds.Tables(RS).Rows(0)("通貨コード")
    End Function

    'param1：String 採番キー
    'param2：DateTime 登録日
    'Return: String 伝票番号
    '伝票番号を取得
    Private Function getSaiban(ByVal key As String, ByVal today As DateTime) As String
        Dim Sql As String = ""
        Dim saibanID As String = ""
        Dim reccnt As Integer = 0 'DB用（デフォルト）

        Try
            Sql = "SELECT "
            Sql += "* "
            Sql += "FROM "
            Sql += "public.m80_saiban"
            Sql += " WHERE "
            Sql += "会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            Sql += " AND "
            Sql += "採番キー = '" & key & "'"

            Dim dsSaiban As DataSet = _db.selectDB(Sql, RS, reccnt)

            saibanID = dsSaiban.Tables(RS).Rows(0)("接頭文字")
            saibanID += today.ToString("MMdd")
            saibanID += dsSaiban.Tables(RS).Rows(0)("最新値").ToString.PadLeft(dsSaiban.Tables(RS).Rows(0)("連番桁数"), "0")

            Dim keyNo As Integer

            If dsSaiban.Tables(RS).Rows(0)("最新値") = dsSaiban.Tables(RS).Rows(0)("最大値") Then
                '最新値が最大と同じ場合、最小値にリセット
                keyNo = dsSaiban.Tables(RS).Rows(0)("最小値")
            Else
                '最新値+1
                keyNo = dsSaiban.Tables(RS).Rows(0)("最新値") + 1
            End If

            Sql = "UPDATE "
            Sql += "Public.m80_saiban "
            Sql += "SET "
            Sql += " 最新値 "
            Sql += " = '"
            Sql += keyNo.ToString
            Sql += "', "
            Sql += "更新者"
            Sql += " = '"
            Sql += frmC01F10_Login.loginValue.TantoNM
            Sql += "', "
            Sql += "更新日"
            Sql += " = '"
            Sql += UtilClass.formatDatetime(today)
            Sql += "' "
            Sql += "WHERE"
            Sql += " 会社コード"
            Sql += "='"
            Sql += frmC01F10_Login.loginValue.BumonCD
            Sql += "'"
            Sql += " AND"
            Sql += " 採番キー = '" & key & "'"
            Console.WriteLine(Sql)
            _db.executeDB(Sql)

            Return saibanID
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", frmC01F10_Login.loginValue.Language, UtilClass.getErrDetail(ex)))
        End Try

    End Function

    '通貨の採番キーからレートを取得・設定
    '基準日が買掛日「以前」の最新のもの
    Private Function setRate(ByVal strKey As Integer, ByVal strAPDate As String) As Decimal
        Dim Sql As String

        Sql = " AND 採番キー = " & strKey & ""
        Sql += " AND 基準日 < '" & UtilClass.strFormatDate(strAPDate) & "'"  '買掛日
        Sql += " ORDER BY 基準日 DESC "

        Dim ds As DataSet = getDsData(" t71_exchangerate", Sql)

        If ds.Tables(RS).Rows.Count > 0 Then
            setRate = ds.Tables(RS).Rows(0)(" レート")
        Else
            'If CultureInfo.CurrentCulture.Name.ToString = CommonConst.CI_ID Then
            '    setRate = CommonConst.BASE_RATE_IDR
            'Else
            '    setRate = CommonConst.BASE_RATE_JPY
            'End If
            setRate = 1.ToString(" F10")
        End If

    End Function


End Class