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

Public Class InventoryRegistrationBulk
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
        kikeAddList(addDt) '登録一覧を渡す

        '登録完了メッセージ
        _msgHd.dspMSG("completeInsert", frmC01F10_Login.loginValue.Language)



        'Catch ex As Exception
        '    Throw ex
        'Finally
        '    'カーソルをビジー状態から元に戻す
        '    Cursor.Current = Cursors.Default

        'End Try

    End Sub

    '対象データから買掛一覧を登録する
    Private Sub kikeAddList(ByVal addDt As System.Data.DataTable)

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

            Sql += "'" & frmC01F10_Login.loginValue.BumonCD & "'" '会社コード
            Sql += ",'1'"                                     '入出庫区分 1:入庫
            Sql += ",'" & addDt.Rows(i)("倉庫コード") & "'"             '倉庫コード
            Sql += ",'" & WH & "'"                            '伝票番号
            Sql += "," & lngCnt                               '行番号
            Sql += ",'" & CommonConst.INOUT_KBN_NORMAL & "'"　'入出庫種別 0:通常
            Sql += ",null"                                  　'引当区分
            Sql += ",'" & addDt.Rows(i)("メーカー") & "'"  'メーカー
            Sql += ",'" & addDt.Rows(i)("品名") & "'"      '品名
            Sql += ",'" & addDt.Rows(i)("型式") & "'"      '型式
            Sql += "," & addDt.Rows(i)("現在在庫数").ToString       '数量
            Sql += ",null"　'単位
            Sql += ",null"　'備考
            Sql += ",'" & strGessyo2 & "'"     　　　　　　　　　　 '入出庫日
            Sql += ",null"                                          '取消日
            Sql += "," & CommonConst.CANCEL_KBN_ENABLED　           '取消区分 0:未取消
            Sql += ",'" & frmC01F10_Login.loginValue.TantoNM & "'"  '更新者
            Sql += ",'" & UtilClass.formatDatetime(Now) & "'"       '更新日
            Sql += ",null"　                         '出庫開始サイン（旧：ロケ番号）
            Sql += "," & CommonConst.Sire_KBN_Zaiko  '仕入区分 2:在庫
            Sql += ",null"                           '製造番号

            Sql += ")"

            _db.executeDB(Sql)

#End Region

            'mDateAdd(addDt.rows(i))

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



    'データ登録
    Private Sub mDateAdd(ByVal prmRow As DataRow)

        '登録
        Dim dtToday As DateTime = DateTime.Now
        Dim strToday As String = UtilClass.formatDatetime(dtToday)

        Dim reccnt As Integer = 0
        Dim Sql As String = ""













        'Sql = " AND 発注番号 = '" & prmRow("発注番号").ToString & "'"
        'Sql += " AND 発注番号枝番 = '" & prmRow("発注番号枝番").ToString & "'"
        'Sql += " AND 取消区分 = " & CommonConst.CANCEL_KBN_ENABLED

        'Dim dsHattyu As DataSet = getDsData("t20_hattyu", Sql)


        'Sql = " SELECT t21.* ,t20.仕入先コード ,t20.ＶＡＴ"
        'Sql += " FROM "
        'Sql += " t20_hattyu t20"
        'Sql += " INNER JOIN t21_hattyu t21 ON "

        'Sql += " t20.""発注番号"" = t21.""発注番号"""
        'Sql += " AND "
        'Sql += " t20.""発注番号枝番"" = t21.""発注番号枝番"""

        'Sql += " where "
        'Sql += " t20.""会社コード"" = '" & frmC01F10_Login.loginValue.BumonCD & "'"

        'Sql += " AND "
        'Sql += " t20.""発注番号"" ILIKE '" & prmRow("発注番号").ToString & "'"
        'Sql += " AND "
        'Sql += " t20.""発注番号枝番"" ILIKE '" & prmRow("発注番号枝番").ToString & "'"
        'Sql += " AND "
        'Sql += " t20.""取消区分"" = " & CommonConst.CANCEL_KBN_ENABLED
        'Sql += " ORDER BY 行番号 ASC "

        'Dim dsHattyudt As DataSet = _db.selectDB(Sql, RS, reccnt)

        ''買掛金額計を集計
        ''発注明細データより仕入金額を算出
        'For i As Integer = 0 To dsHattyudt.Tables(RS).Rows.Count - 1
        '    PurchaseCostFC = PurchaseCostFC + (dsHattyudt.Tables(RS).Rows(i)("仕入値_外貨") * dsHattyudt.Tables(RS).Rows(i)("仕入数量"))
        'Next

        'VAT_FC = PurchaseCostFC * dsHattyu.Tables(RS).Rows(0)("ＶＡＴ").ToString / 100 '買掛金額（VAT)
        'PurchaseAmountFC = PurchaseCostFC + VAT_FC '買掛金額

        ''レートの取得
        'Dim strRate As Decimal = setRate(dsHattyu.Tables(RS).Rows(0)("通貨").ToString(), prmRow("請求日").ToString)

        ''今回買掛金額計
        'BuyToHangAmountFC = PurchaseAmountFC
        'BuyToHangAmount = Math.Ceiling(BuyToHangAmountFC / strRate)  '画面の金額をIDRに変換　切り上げ

        ''買掛残高
        'AccountsPayableFC = PurchaseAmountFC
        'AccountsPayable = Math.Ceiling(AccountsPayableFC / strRate)  '画面の金額をIDRに変換　切り上げ


        'Sql = "INSERT INTO "
        'Sql += "Public."
        'Sql += "t46_kikehd("
        'Sql += "会社コード, 買掛番号, 買掛区分, 買掛日, 発注番号, 発注番号枝番, 客先番号, 仕入先コード, 仕入先名, 買掛金額計, 買掛残高"
        'Sql += ", 備考1, 備考2, 取消区分, 登録日, 更新者, 更新日, 支払予定日"
        'Sql += ", 買掛金額計_外貨, 買掛残高_外貨, 通貨, レート, 仕入先請求番号)"

        'Sql += " VALUES('"

        'Sql += dsHattyu.Tables(RS).Rows(0)("会社コード").ToString '会社コード
        'Sql += "', '"
        'Sql += AP '買掛番号
        'Sql += "', '"
        'Sql += CommonConst.APC_KBN_NORMAL.ToString '買掛区分
        'Sql += "', '"
        'Sql += UtilClass.strFormatDate(prmRow("請求日").ToString) '買掛日(請求日)
        'Sql += "', '"
        'Sql += dsHattyu.Tables(RS).Rows(0)("発注番号").ToString '発注番号
        'Sql += "', '"
        'Sql += dsHattyu.Tables(RS).Rows(0)("発注番号枝番").ToString '発注番号枝番
        'Sql += "', '"
        'Sql += dsHattyu.Tables(RS).Rows(0)("客先番号").ToString '客先番号
        'Sql += "', '"
        'Sql += dsHattyu.Tables(RS).Rows(0)("仕入先コード").ToString '仕入先コード
        'Sql += "', '"
        'Sql += dsHattyu.Tables(RS).Rows(0)("仕入先名").ToString '仕入先名
        'Sql += "', "
        'Sql += UtilClass.formatNumber(BuyToHangAmount) '買掛金額計
        'Sql += ", "
        'Sql += UtilClass.formatNumber(AccountsPayable) '買掛残高
        'Sql += ", '"
        'Sql += prmRow("備考1").ToString '備考1
        'Sql += "', '"
        'Sql += prmRow("備考2").ToString '備考2
        'Sql += "', '"
        'Sql += "0" '取消区分
        'Sql += "', '"
        'Sql += strToday '登録日
        'Sql += "', '"
        'Sql += frmC01F10_Login.loginValue.TantoNM '更新者
        'Sql += "', '"
        'Sql += strToday '更新日
        'Sql += "', '"
        'Sql += UtilClass.strFormatDate(prmRow("支払予定日").ToString) '支払予定日

        'Sql += "','"
        'Sql += UtilClass.formatNumber(BuyToHangAmountFC) '買掛金額計_外貨
        'Sql += "',"
        'Sql += UtilClass.formatNumber(AccountsPayableFC) '買掛残高_外貨

        'Sql += ","
        'Sql += dsHattyu.Tables(RS).Rows(0)("通貨").ToString() '通貨
        'Sql += ",'"
        'Sql += UtilClass.formatNumberF10(strRate) 'レート

        'Sql += "','"
        'Sql += prmRow("仕入先請求番号").ToString  '仕入先請求番号

        'Sql += "')"

        '_db.executeDB(Sql)

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


    '戻るボタン押下時
    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        _parentForm.Enabled = True
        _parentForm.Show()
        Me.Dispose()
    End Sub

    'Excel出力する際のチェック
    Private Function excelOutput(ByVal prmFilePath As String)
        Dim fileChk As String = Dir(prmFilePath)
        Dim sr As StreamReader

        '同名ファイルがあるかどうかチェック
        If fileChk <> "" Then
            Dim result = _msgHd.dspMSG("confirmFileExist", frmC01F10_Login.loginValue.Language, prmFilePath)
            If result = DialogResult.No Then
                Return False
            End If

            Try
                'ファイルが開けるかどうかチェック
                sr = New StreamReader(prmFilePath)
                sr.Close() '処理が通ったら閉じる
                Marshal.ReleaseComObject(sr)
            Catch ex As Exception
                '開けない場合はアラートを表示してリターンさせる
                MessageBox.Show(ex.Message, CommonConst.AP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try

            Return True
        End If
        Return True
    End Function

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