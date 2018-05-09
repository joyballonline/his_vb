'===============================================================================
'　 （システム名）      カネキ吉田商店様向け　原価管理システム
'
'   （機能名）          商品マスタ一覧
'   （クラス名）        frmM30F10_ShohinList
'   （処理機能名）      
'   （本MDL使用前提）   
'   （備考）         
'
'===============================================================================
' 履歴  名前               日付       　　　 　内容
'-------------------------------------------------------------------------------
'  (1)  桜井               2018/03/13      　　流用新規
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.DB
Imports UtilMDL.MSG             'UtilMsgHandler用
Imports UtilMDL.DataGridView    'UtilDataGridView用
Imports UtilMDL.Text            'UtilTextWriter用
Imports System.Xml
Imports System.Configuration
Imports Microsoft.VisualBasic.FileIO

Public Class frmM30F10_ShohinList
#Region "宣言"

    '-------------------------------------------------------------------------------
    ' 定数宣言
    '-------------------------------------------------------------------------------
    ' PG制御文字
    Private Const N As String = ControlChars.NewLine                    ' 改行文字
    Private Const RS As String = "RecSet"                               ' レコードセットテーブル

    '汎用マスメン
    Private Const lM3010_DB_EXIST As Integer = 1                        'あり：登録済
    Private Const lM3010_DB_NONE As Integer = 0                         'なし：未登録

    'グリッド列№ 
    Private Const M3010_COLNO_SHOHINCODE As Integer = 0                             '商品コード
    Private Const M3010_COLNO_SHOHINNAME As Integer = 1                             '商品名
    Private Const M3010_COLNO_SHOHINRYAKUNAME As Integer = 2                        '商品名略称
    Private Const M3010_COLNO_SHOHINNAMEKANA As Integer = 3                         '商品名カナ
    Private Const M3010_COLNO_HANBAISIIREKBN As Integer = 4                         '販売仕入区分
    Private Const M3010_COLNO_HANBAISIIREKBNNAME As Integer = 5                     '販売仕入区分名称
    Private Const M3010_COLNO_DAIBUNRUI As Integer = 6                              '大分類
    Private Const M3010_COLNO_KAZEIKBN As Integer = 7                               '課税区分
    Private Const M3010_COLNO_KAZEIKBNNAME As Integer = 8                           '課税区分名称
    Private Const M3010_COLNO_REITOKBN As Integer = 9                               '冷凍区分
    Private Const M3010_COLNO_REITOKBNNAME As Integer = 10                          '冷凍区分名称
    Private Const M3010_COLNO_IRISU As Integer = 11                                 '入数
    Private Const M3010_COLNO_TANI As Integer = 12                                  '単位
    Private Const M3010_COLNO_SANTI As Integer = 13                                 '産地
    Private Const M3010_COLNO_DISPNO As Integer = 14                                '表示順
    Private Const M3010_COLNO_MEMO As Integer = 15                                  'メモ
    Private Const M3010_COLNO_UPDNM As Integer = 16                                 '更新者
    Private Const M3010_COLNO_UPDDT As Integer = 17                                 '更新日
    Private Const M3010_COLNO_MODFLG As Integer = 18                                '更新フラグ
    Private Const M3010_COLNO_HDSHOHINCODE As Integer = 19                          '変更前商品コード

    'グリッド列名 
    Private Const M3010_CCOL_SHOHINCODE As String = "cnShohinCode"                  '商品コード
    Private Const M3010_CCOL_SHOHINNAME As String = "cnShohinName"                  '商品名
    Private Const M3010_CCOL_SHOHINRYAKUNAME As String = "cnShohinRyakuName"        '商品名略称
    Private Const M3010_CCOL_SHOHINNAMEKANA As String = "cnShohinNameKana"          '商品名カナ
    Private Const M3010_CCOL_HANBAISIIREKBN As String = "cnHanbaiSiireKbn"          '販売仕入区分
    Private Const M3010_CCOL_HANBAISIIREKBNNAME As String = "cnHanbaiSiireKbnName"  '販売仕入区分名称
    Private Const M3010_CCOL_DAIBUNRUI As String = "cnDaibunrui"                    '大分類
    Private Const M3010_CCOL_KAZEIKBN As String = "cnKazeiKbn"                      '課税区分
    Private Const M3010_CCOL_KAZEIKBNNAME As String = "cnKazeiKbnName"              '課税区分名称
    Private Const M3010_CCOL_REITOKBN As String = "cnReitoKbn"                      '冷凍区分
    Private Const M3010_CCOL_REITOKBNANME As String = "cnReitoKbnName"              '冷凍区分名称
    Private Const M3010_CCOL_IRISU As String = "cnIrisu"                            '入数
    Private Const M3010_CCOL_TANI As String = "cnTani"                              '単位
    Private Const M3010_CCOL_SANTI As String = "cnSanti"                            '産地
    Private Const M3010_CCOL_DISPNO As String = "cnDispNo"                          '表示順
    Private Const M3010_CCOL_MEMO As String = "cnMemo"                              'メモ
    Private Const M3010_CCOL_UPDNM As String = "cnUpdNm"                            '更新者
    Private Const M3010_CCOL_UPDDT As String = "cnUpdDt"                            '更新日
    Private Const M3010_CCOL_MODFLG As String = "cnModFlg"                          '更新フラグ
    Private Const M3010_CCOL_HDSHOHINCODE As String = "cnHideShohinCode"            '変更前商品コード

    'グリッドデータ名 
    Private Const M3010_DTCOL_SHOHINCODE As String = "dtShohinCode"                 '商品コード
    Private Const M3010_DTCOL_SHOHINNAME As String = "dtShohinName"                 '商品名
    Private Const M3010_DTCOL_SHOHINRYAKUNAME As String = "dtShohinRyakuName"       '商品名略称
    Private Const M3010_DTCOL_SHOHINNAMEKANA As String = "dtShohinNameKana"         '商品名カナ
    Private Const M3010_DTCOL_HANBAISIIREKBN As String = "dtHanbaiSiireKbn"         '販売仕入区分
    Private Const M3010_DTCOL_HANBAISIIREKBNNAME As String = "dtHanbaiSiireKbnName" '販売仕入区分名称
    Private Const M3010_DTCOL_DAIBUNRUI As String = "dtDaibunrui"                   '大分類
    Private Const M3010_DTCOL_KAZEIKBN As String = "dtKazeiKbn"                     '課税区分
    Private Const M3010_DTCOL_KAZEIKBNNAME As String = "dtKazeiKbnName"             '課税区分名称
    Private Const M3010_DTCOL_REITOKBN As String = "dtReitoKbn"                     '冷凍区分
    Private Const M3010_DTCOL_REITOKBNNAME As String = "dtReitoKbnName"             '冷凍区分名称
    Private Const M3010_DTCOL_IRISU As String = "dtIrisu"                           '入数
    Private Const M3010_DTCOL_TANI As String = "dtTani"                             '単位
    Private Const M3010_DTCOL_SANTI As String = "dtSanti"                           '産地
    Private Const M3010_DTCOL_DISPNO As String = "dtDispNo"                         '表示順
    Private Const M3010_DTCOL_MEMO As String = "dtMemo"                             'メモ
    Private Const M3010_DTCOL_UPDNM As String = "dtUpdNm"                           '更新者
    Private Const M3010_DTCOL_UPDDT As String = "dtUpdDt"                           '更新日
    Private Const M3010_DTCOL_MODFLG As String = "dtModFlg"                         '更新フラグ
    Private Const M3010_DTCOL_HDSHOHINCODE As String = "dtHideShohinCode"           '変更前商品コード

    'CSVファイル出力エラー関連
    Public Const NO_ERR_DATA As String = "該当データなし"
    Public Const CANCEL_ERR_DATA As String = "出力キャンセル"

    Private Const CSVXmlTagName As String = "CSV出力情報"                           'CSV出力情報のタグ名

    '-------------------------------------------------------------------------------
    'メンバー変数宣言
    '-------------------------------------------------------------------------------
    Private _db As UtilDBIf
    Private _msgHd As UtilMsgHandler
    Private _parentForm As Form
    Private _ShoriMode As Integer
    Private _comLogc As CommonLogic                         '共通処理用
    Private _open As Boolean = False                        '画面起動済フラグ
    Private _dbErr As Boolean = False                       'DB登録エラー判定用
    Private _cellErr As Boolean = False
    Private _iColErr As Integer
    Private _iRowErr As Integer
    Private _companyCd As String
    Private _selectId As String
    Private _userId As String
    Private _ShohinCode As String
    Private Shared _shoriId As String
    Private _xmlDoc As XmlDocument
    Private _Redisplay As Boolean

    '-------------------------------------------------------------------------------
    'プロパティ宣言
    '-------------------------------------------------------------------------------
    Public Shared ReadOnly Property shoriId() As String
        Get
            Return _shoriId
        End Get
    End Property

    Public Property shohinCode() As String
        Get
            Return _ShohinCode
        End Get
        Set(ByVal value As String)
            _ShohinCode = value
        End Set
    End Property

    Public Property redisplay() As Boolean
        Get
            Return _Redisplay
        End Get
        Set(ByVal value As Boolean)
            _Redisplay = value
        End Set
    End Property

#End Region

#Region "コンストラクタ"
    '-------------------------------------------------------------------------------
    'コンストラクタ（Privateにして、外からは呼べないようにする）
    '-------------------------------------------------------------------------------
    Private Sub New()
        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()
    End Sub

    '-------------------------------------------------------------------------------
    'コンストラクタ　
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler,
                   ByRef prmRefDbHd As UtilDBIf,
                   ByRef prmSelectID As String,
                   ByRef prmParentForm As Form)

        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _parentForm = prmParentForm
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint  'フォームタイトル表示
        _companyCd = frmC01F10_Login.loginValue.BumonCD                     '会社コード
        _selectId = prmSelectID                                             '選択処理ID
        _shoriId = prmSelectID                                              '起動処理ID
        _userId = frmC01F10_Login.loginValue.TantoCD                        'ユーザＩＤ

        ' 共通処理使用の準備
        _comLogc = New CommonLogic(_db, _msgHd)                             ' 共通処理用

    End Sub
#End Region

#Region "イベント"

#Region "フォームロード"
    '-------------------------------------------------------------------------------
    '   Menu画面ロード処理
    '   （処理概要） Menu画面が起動したときの処理を行う。
    '-------------------------------------------------------------------------------
    Private Sub frmM30F10_ShohinList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim BkCur As Cursor = Cursor.Current                                ' 現在のカーソルを取っておく
        Try
            Cursor.Current = Cursors.WaitCursor                             ' 砂時計カーソルに入れ替える

            '操作履歴ログ作成
            Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, _shoriId, CommonConst.PROGRAM_START, CommonConst.STATUS_NORMAL,
                                                DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value,
                                                DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _userId)

            '画面の初期設定
            Call Init_Form()

            _open = True                                                    '画面起動済フラグ

            _Redisplay = False                                                 '検索済みフラグ

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        Finally
            Cursor.Current = BkCur                                          ' 取っておいたカーソルに戻す
        End Try

    End Sub
#End Region

#Region "戻るボタンクリック"
    '-------------------------------------------------------------------------------
    '　戻るボタン押下
    '-------------------------------------------------------------------------------
    Private Sub cmdModoru_Click(sender As Object, e As EventArgs) Handles cmdModoru.Click

        _parentForm.Show()                                              ' 前画面を表示
        _parentForm.Enabled = True                                      ' 前画面の使用を可能にする
        _parentForm.Activate()                                          ' 前画面をアクティブにする

        Me.Dispose()                                                    ' 自画面を閉じる

    End Sub
#End Region

#Region "CSV出力ボタン　クリック"
    '-------------------------------------------------------------------------------
    '　CSV出力ボタン　クリック
    '-------------------------------------------------------------------------------
    Private Sub cmdSyuturyoku_Click(sender As Object, e As EventArgs) Handles cmdSyuturyoku.Click

        Dim result As New Hashtable

        Try

            '* XML内容の取得
            _xmlDoc = New XmlDocument()
            _xmlDoc.Load(FileSystem.CombinePath(GetAppConfig("CSV出力情報ファイル格納フォルダ"), GetAppConfig("CSV出力情報ファイル")))

            'ルート要素を取得する
            Dim xmlRoot As XmlElement = _xmlDoc.DocumentElement

            'ＸＭＬファイルにルート要素が存在しない場合例外エラーを返す
            If xmlRoot Is Nothing Then
                Dim lex As UsrDefException = New UsrDefException(_xmlDoc.Name + "にルート要素が存在しません。")
                Debug.WriteLine(lex.Message)
                Throw lex
            End If

            Dim xmlElmList As XmlNodeList = xmlRoot.GetElementsByTagName(CSVXmlTagName)

            For Each xmlDetail As XmlElement In xmlElmList
                For Each xmlData As XmlElement In xmlDetail
                    result.Add(xmlData.Name, xmlData.InnerText)
                Next
            Next

        Catch ex As XmlException
            Dim lex As UsrDefException = New UsrDefException("XMLファイル読込エラー" & ControlChars.NewLine &
                                                     "XMLファイルの存在・パスを確認してください。")
            Debug.WriteLine(lex.Message)
            Throw lex
        End Try

        Try

            '登録確認ダイアログ表示
            Dim piRtn As DialogResult = _msgHd.dspMSG("confirmCSVOutput")       '一覧のデータをCSVファイルへ出力します。よろしいですか？
            If piRtn <> DialogResult.OK Then Exit Sub

            '対象データの有無チェック
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvIchiran)
            If gh.getMaxRow = 0 Then
                Throw New UsrDefException("対象データがありません。", _msgHd.getMSG("NonData"))
            End If

            ' FolderBrowserDialog の新しいインスタンスを生成する
            Dim fbd As New FolderBrowserDialog()

            fbd.Description = "CSVファイル出力先フォルダを選択してください。"

            ' 初期選択するパスを設定する
            fbd.SelectedPath = result("商品マスタ出力先")

            'ダイアログを表示する
            If fbd.ShowDialog() <> DialogResult.OK Then
                Exit Sub
            End If

            Dim strPath = ""

            strPath = fbd.SelectedPath
            If Strings.Right(strPath, 1) <> "\" Then
                strPath = strPath & "\"
            End If

            ' 不要になった時点で破棄する
            fbd.Dispose()

            '出力対象データの取得
            Dim ds As DataSet = Nothing
            ds = dgvIchiran.DataSource

            'テキスト作成（CSV出力の場合のみ）
            Dim editFile As String = ""
            Try
                CreateCsvM30(strPath, result("商品マスタCSVファイル名"), ds, editFile)
            Catch le As UsrDefException
                If NO_ERR_DATA.Equals(le.Message) Then
                    '該当データなし
                    'Call _msgHd.dspMSG("noTargetData")
                    'Exit Sub
                ElseIf CANCEL_ERR_DATA.Equals(le.Message) Then
                    Exit Sub
                Else
                    Throw le
                End If
            End Try
            _msgHd.dspMSG("completeRun")       '処理が完了しました。

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub
#End Region

#Region "参照ボタンクリック"
    '-------------------------------------------------------------------------------
    '　参照ボタン押下
    '-------------------------------------------------------------------------------
    Private Sub cmdSansyou_Click(sender As Object, e As EventArgs) Handles cmdSansyou.Click

        Dim idx As Integer
        For Each c As DataGridViewRow In dgvIchiran.SelectedRows
            idx = c.Index
            Exit For
        Next c

        _ShohinCode = dgvIchiran.Rows(idx).Cells(M3010_CCOL_SHOHINCODE).Value
        Dim openForm As Form = Nothing
        openForm = New frmM30F20_ShohinHosyu(_msgHd, _db, Me, CommonConst.MODE_InquiryStatus, _selectId, _ShohinCode)   '処理選択
        openForm.Show()
        Me.Hide()   ' 自分は隠れる

    End Sub
#End Region

#Region "追加ボタンクリック"
    '-------------------------------------------------------------------------------
    '　追加ボタン押下
    '-------------------------------------------------------------------------------
    Private Sub cmdTuika_Click(sender As Object, e As EventArgs) Handles cmdTuika.Click

        Dim openForm As Form = Nothing
        openForm = New frmM30F20_ShohinHosyu(_msgHd, _db, Me, CommonConst.MODE_ADDNEW, _selectId, "")   '処理選択
        openForm.Show()
        Me.Hide()   ' 自分は隠れる

    End Sub
#End Region

#Region "複写新規ボタンクリック"
    '-------------------------------------------------------------------------------
    '　複写新規ボタン押下
    '-------------------------------------------------------------------------------
    Private Sub cmdFukusya_Click(sender As Object, e As EventArgs) Handles cmdFukusya.Click

        Dim idx As Integer
        For Each c As DataGridViewRow In dgvIchiran.SelectedRows
            idx = c.Index
            Exit For
        Next c

        _ShohinCode = dgvIchiran.Rows(idx).Cells(M3010_CCOL_SHOHINCODE).Value
        Dim openForm As Form = Nothing
        openForm = New frmM30F20_ShohinHosyu(_msgHd, _db, Me, CommonConst.MODE_ADDNEWCOPY, _selectId, _ShohinCode)   '処理選択
        openForm.Show()
        Me.Hide()   ' 自分は隠れる

    End Sub
#End Region

#Region "変更ボタンクリック"
    '-------------------------------------------------------------------------------
    '　変更ボタン押下
    '-------------------------------------------------------------------------------
    Private Sub cmdHenkou_Click(sender As Object, e As EventArgs) Handles cmdHenkou.Click

        Dim idx As Integer
        For Each c As DataGridViewRow In dgvIchiran.SelectedRows
            idx = c.Index
            Exit For
        Next c

        _ShohinCode = dgvIchiran.Rows(idx).Cells(M3010_CCOL_SHOHINCODE).Value
        Dim openForm As Form = Nothing
        openForm = New frmM30F20_ShohinHosyu(_msgHd, _db, Me, CommonConst.MODE_EditStatus, _selectId, _ShohinCode)   '処理選択
        openForm.Show()
        Me.Hide()   ' 自分は隠れる

    End Sub
#End Region

#Region "削除ボタンクリック"
    '-------------------------------------------------------------------------------
    '　削除ボタン押下
    '-------------------------------------------------------------------------------
    Private Sub cmdSakujo_Click(sender As Object, e As EventArgs) Handles cmdSakujo.Click

        Dim idx As Integer
        For Each c As DataGridViewRow In dgvIchiran.SelectedRows
            idx = c.Index
            Exit For
        Next c

        _ShohinCode = dgvIchiran.Rows(idx).Cells(M3010_CCOL_SHOHINCODE).Value
        Dim openForm As Form = Nothing
        openForm = New frmM30F20_ShohinHosyu(_msgHd, _db, Me, CommonConst.MODE_DELETE, _selectId, _ShohinCode)   '処理選択
        openForm.Show()
        Me.Hide()   ' 自分は隠れる

    End Sub
#End Region

#Region "一覧　SelectionChanged"
    '-------------------------------------------------------------------------------
    '　一覧　SelectionChanged
    '-------------------------------------------------------------------------------
    Private Sub dgvIchiran_SelectionChanged(sender As Object, e As EventArgs) Handles dgvIchiran.SelectionChanged

        If Not _open Then Exit Sub

        If dgvIchiran.RowCount > 0 Then
            dgvIchiran.BeginEdit(False)
        End If

    End Sub
#End Region

#End Region

#Region "プロシージャ"

    '-------------------------------------------------------------------------------
    '　一覧クリア
    '-------------------------------------------------------------------------------
    Private Sub Clear_Ichiran()

        Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvIchiran)
        If gh.getMaxRow > 0 Then
            gh.clearRow()
        End If

        'ボタン制御
        cmdSyuturyoku.Enabled = False   'CSV出力ボタン
        cmdSansyou.Enabled = False      '参照ボタン
        cmdTuika.Enabled = True         '追加ボタン
        cmdFukusya.Enabled = False      '複写新規ボタン
        cmdHenkou.Enabled = False       '変更ボタン
        cmdSakujo.Enabled = False       '削除ボタン

    End Sub

    '-------------------------------------------------------------------------------
    '　カーソル移動
    '   （処理概要）Enterキーが押された場合、次のコントロールに移動する
    '-------------------------------------------------------------------------------
    Private Sub Set_EnterNext(para_e As KeyEventArgs)
        If para_e.KeyCode = Keys.Enter Then
            If para_e.Control = False Then
                Me.SelectNextControl(Me.ActiveControl, Not para_e.Shift, True, True, True)
            End If
        End If
    End Sub

    '-------------------------------------------------------------------------------
    '   CSV出力処理
    '   （処理概要）　商品マスタ一覧画面に表示されている内容をCSVファイルへ出力する
    '   ●入力パラメタ   ：prmOutDir    出力Dir                    
    '                      prmOutFile   出力File                    
    '                      prmDataSet   データセット
    '                      prmRefEditFileNm
    '   ●メソッド戻り値 ：なし
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' CSV出力処理（CreateCsvM30）
    ''' </summary>
    ''' <param name="prmOutDir"></param>
    ''' <param name="prmOutFile"></param>
    ''' <param name="prmDataSet"></param>
    ''' <param name="prmRefEditFileNm"></param>
    Private Sub CreateCsvM30(ByVal prmOutDir As String, ByVal prmOutFile As String, ByVal prmDataSet As DataSet, Optional ByRef prmRefEditFileNm As String = "")

        '出力データ生成
        Dim outStr As String = CreateCsvDataM30(prmDataSet)

        Dim w As UtilTextWriter = New UtilTextWriter(prmOutDir & prmOutFile)
        w.open(False)
        Try
            w.write(outStr.ToString)
        Finally
            w.close()
        End Try
        prmRefEditFileNm = prmOutDir & prmOutFile

    End Sub

    '-------------------------------------------------------------------------------
    '   CSV生成処理
    '   （処理概要）　商品マスタ一覧画面に表示されているデータを生成して返却する
    '   ●入力パラメタ   ：なし
    '   ●メソッド戻り値 ：CSV内容文字列
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' CSV生成処理（createCsvDataM30）
    ''' </summary>
    ''' <param name="prmDataSet"></param>
    Private Function CreateCsvDataM30(ByVal prmDataSet As DataSet) As String

        '形成
        Dim outStr As System.Text.StringBuilder = New System.Text.StringBuilder()
        With outStr
            'ヘッダ
            .Append("商品コード, 商品名, 商品名略称, 商品名カナ, 販売仕入区分, 大分類, 課税区分, 冷凍区分, 入数, 単位, 産地, 表示順, メモ, 更新者, 更新日" & ControlChars.NewLine)

            '明細
            Dim t As DataTable = prmDataSet.Tables(RS)
            For i As Integer = 0 To prmDataSet.Tables(RS).Rows.Count - 1
                .Append("" & EnclosedItemDoubleQuotes(t.Rows(i)("dtShohinCode")) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtShohinName"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtShohinRyakuName"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtShohinNameKana"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtHanbaiSiireKbn"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtDaibunrui"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtKazeiKbn"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtReitoKbn"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtIrisu"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtTani"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtSanti"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtDispNo"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtMemo"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtUpdNm"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtUpdDt"))) & "" & ControlChars.NewLine)
            Next
        End With

        CreateCsvDataM30 = outStr.ToString

    End Function

    '-------------------------------------------------------------------------------
    '　文字列をダブルクォートで囲む
    '-------------------------------------------------------------------------------
    Private Function EnclosedItemDoubleQuotes(item As String) As String
        If item.IndexOf(""""c) > -1 Then
            '"を""とする
            item = item.Replace("""", """""")
        End If
        Return """" & item & """"
    End Function

    '-------------------------------------------------------------------------------
    '　一覧表示
    '   （処理概要）入力された条件に該当する商品マスタデータを取得する
    '-------------------------------------------------------------------------------
    Private Sub Edit_frmM30F10_Ichiran()

        Dim iRecCnt As Integer = 0
        Dim oDataSet As DataSet = Nothing                           ' データセット型

        Try
            Dim sql As String = ""
            sql = "Select "
            sql = sql & N & "  g.商品コード      " & M3010_DTCOL_SHOHINCODE          ' 0：商品コード
            sql = sql & N & ", g.商品名          " & M3010_DTCOL_SHOHINNAME          ' 1：商品名
            sql = sql & N & ", g.商品名略称      " & M3010_DTCOL_SHOHINRYAKUNAME     ' 2：商品名略称
            sql = sql & N & ", g.商品名カナ      " & M3010_DTCOL_SHOHINNAMEKANA      ' 3：商品名カナ
            sql = sql & N & ", g.販売仕入区分    " & M3010_DTCOL_HANBAISIIREKBN      ' 4：販売仕入区分
            sql = sql & N & ", h1.文字１         " & M3010_DTCOL_HANBAISIIREKBNNAME  ' 5：販売仕入区分名称
            sql = sql & N & ", g.大分類          " & M3010_DTCOL_DAIBUNRUI           ' 6：大分類
            sql = sql & N & ", g.課税区分        " & M3010_DTCOL_KAZEIKBN            ' 7：課税区分
            sql = sql & N & ", h2.文字１         " & M3010_DTCOL_KAZEIKBNNAME        ' 8：課税区分名称
            sql = sql & N & ", g.冷凍区分        " & M3010_DTCOL_REITOKBN            ' 9：冷凍区分
            sql = sql & N & ", h3.文字１         " & M3010_DTCOL_REITOKBNNAME        ' 10：冷凍区分名称
            sql = sql & N & ", g.入数            " & M3010_DTCOL_IRISU               ' 11：入数
            sql = sql & N & ", g.単位            " & M3010_DTCOL_TANI                ' 12：単位
            sql = sql & N & ", g.産地            " & M3010_DTCOL_SANTI               ' 13：産地
            sql = sql & N & ", g.表示順          " & M3010_DTCOL_DISPNO              ' 14：表示順
            sql = sql & N & ", g.メモ            " & M3010_DTCOL_MEMO                ' 15：メモ
            sql = sql & N & ", u.氏名            " & M3010_DTCOL_UPDNM               ' 16：更新者
            sql = sql & N & ", TO_CHAR(g.更新日, 'YYYY/MM/DD HH24:MI') " & M3010_DTCOL_UPDDT   ' 17：更新日時
            sql = sql & N & ", '0'               " & M3010_DTCOL_MODFLG              ' 18：更新フラグ
            sql = sql & N & ", g.商品コード      " & M3010_DTCOL_HDSHOHINCODE        ' 19：変更前商品コード
            sql = sql & N & " FROM M20_GOODS g "
            sql = sql & N & " LEFT JOIN M90_HANYO h1 ON h1.会社コード = g.会社コード AND h1.固定キー = '" & CommonConst.HANYO_HANBAISIIRE_KBN & "' AND h1.可変キー = g.販売仕入区分 "
            sql = sql & N & " LEFT JOIN M90_HANYO h2 ON h2.会社コード = g.会社コード AND h2.固定キー = '" & CommonConst.HANYO_KAZEI_KBN & "' AND h2.可変キー = g.課税区分 "
            sql = sql & N & " LEFT JOIN M90_HANYO h3 ON h3.会社コード = g.会社コード AND h3.固定キー = '" & CommonConst.HANYO_REITOU_KBN & "' AND h3.可変キー = g.冷凍区分 "
            sql = sql & N & " LEFT JOIN M02_USER u ON u.会社コード = g.会社コード AND u.ユーザＩＤ = g.更新者 "
            sql = sql & N & " WHERE g.会社コード  = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            sql = sql & N & makeLikeSql()
            sql = sql & N & " ORDER BY " & M3010_DTCOL_SHOHINCODE

            Try
                'sql発行
                oDataSet = _db.selectDB(sql, RS, iRecCnt)                     '抽出結果をDSへ格納
            Catch ex As Exception
                Throw New UsrDefException("DBデータ取得失敗", _msgHd.getMSG("ErrDbSelect", UtilClass.getErrDetail(ex)))
            End Try

            '抽出レコードが０件の場合、メッセージ表示
            If iRecCnt = 0 Then
                'Throw New UsrDefException("対象データがありません。", _msgHd.getMSG("NonData"))
                '一覧クリア
                dgvIchiran.DataSource = oDataSet
                dgvIchiran.DataMember = RS
                _msgHd.dspMSG("NonData")
                Exit Sub
            End If

            '一覧バインド
            dgvIchiran.DataSource = oDataSet
            dgvIchiran.DataMember = RS

        Catch lue As UsrDefException
            Throw lue
        Catch lex As Exception
            Throw New UsrDefException(lex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(lex)))       'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　コンボボックス設定
    '   （処理概要）コンボボックスを設定
    '   ●その他　　　
    '-------------------------------------------------------------------------------
    Private Sub SetCombo(ByRef cob As ComboBox)

        Dim sql As String = ""
        '汎用マスタをコンボボックスにセット
        Try
            sql = "SELECT "
            sql = sql & N & "    可変キー, 文字１ "
            sql = sql & N & " FROM m90_hanyo "
            sql = sql & N & "   WHERE 会社コード= '" & frmC01F10_Login.loginValue.BumonCD & "' and 固定キー ='" & CommonConst.HANYO_HANBAISIIRE_KBN & "' "
            sql = sql & N & " order by 表示順 "
            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)
            ds.Tables("RecSet").Rows.InsertAt(ds.Tables("RecSet").NewRow, 0)

            cob.DisplayMember = "文字１"
            cob.ValueMember = "可変キー"
            cob.DataSource = ds.Tables(RS)

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　画面初期化処理
    '   （処理概要）ボタン・テキストロック設定
    '-------------------------------------------------------------------------------
    Private Sub Init_Form()

        Me.txtShohinName.Text = String.Empty

        '販売仕入区分コンボボックス編集
        Call SetCombo(cboHanbaiSiireKbn)
        cboHanbaiSiireKbn.SelectedIndex = 0

        '一覧クリア
        Call Clear_Ichiran()

    End Sub


    Private Shared Function NewMethod() As String
        Return ""
    End Function

    Function makeLikeSql() As String
        '--------------------------------
        '抽出条件の取得、SQLwhere句の作成
        '--------------------------------
        Dim shohinName As String = If(txtShohinName.Text IsNot "", txtShohinName.Text, "")     '商品名

        Dim termsSql As String = ""
        termsSql += " and ( 商品名 like  '%" & shohinName & "%' "
        '販売仕入区分
        If Me.cboHanbaiSiireKbn.SelectedIndex <> 0 Then
            termsSql += " and 販売仕入区分 = '" & cboHanbaiSiireKbn.SelectedValue & "' "
        End If
        termsSql += " ) "

        Return termsSql
    End Function

    Private Sub cmdKensaku_Click(sender As Object, e As EventArgs) Handles cmdKensaku.Click
        '一覧データを取得
        Edit_frmM30F10_Ichiran()

        'ボタン制御
        If (dgvIchiran.Rows.Count = 0) Then
            cmdSyuturyoku.Enabled = False   'CSV出力ボタン
            cmdSansyou.Enabled = False      '参照ボタン
            cmdTuika.Enabled = True         '追加ボタン
            cmdFukusya.Enabled = False      '複写新規ボタン
            cmdHenkou.Enabled = False       '変更ボタン
            cmdSakujo.Enabled = False       '削除ボタン

        Else
            cmdSyuturyoku.Enabled = True   'CSV出力ボタン
            cmdSansyou.Enabled = True      '参照ボタン
            cmdTuika.Enabled = True        '追加ボタン
            cmdFukusya.Enabled = True      '複写新規ボタン
            cmdHenkou.Enabled = True       '変更ボタン
            cmdSakujo.Enabled = True       '削除ボタン
        End If

    End Sub

    '*-----------------------------------------------*
    '* 指定したキーの AppConfig 情報を取得する
    '*
    '* 引数：In ：参照キー
    '* 戻値：取得値
    '*-----------------------------------------------*
    Private Function GetAppConfig(ByVal prmKeyValue As String) As String
        Dim config As System.Configuration.Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        Dim appSettings As System.Configuration.AppSettingsSection = CType(config.GetSection("appSettings"), System.Configuration.AppSettingsSection)
        Dim wRetValue As String = String.Empty

        '* キー無しの判定
        If prmKeyValue = "" Then
            GetAppConfig = ""
            Exit Function
        End If
        '* Value の取得
        Try
            wRetValue = appSettings.Settings(prmKeyValue).Value
            If IsNothing(wRetValue) Then
                wRetValue = ""
            End If
            '*
            GetAppConfig = wRetValue
        Catch ex As Exception
            GetAppConfig = ""
        End Try

    End Function

    Private Sub ctl_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtShohinName.KeyPress,
                                                                               cboHanbaiSiireKbn.KeyPress

        UtilMDL.UtilClass.moveNextFocus(Me, e)

    End Sub

    '-------------------------------------------------------------------------------
    '　フォーカス取得イベント
    '-------------------------------------------------------------------------------
    Private Sub ctl_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtShohinName.GotFocus

        'フォーカス取得時、入力パラメタのコントロールを全選択状態とする
        Call UtilClass.selAll(sender)

    End Sub

    Private Sub frmM30F10_ShohinList_Activated(sender As Object, e As EventArgs) Handles Me.Activated

        '保守画面から戻ってきたとき以外は処理終了
        If _Redisplay = False Then
            Exit Sub
        End If

        '一覧のクリア
        Call Clear_Ichiran()

        '検索データ表示
        Call Edit_frmM30F10_Ichiran()

        '保守画面から戻ってきたときの画面遷移前選択行への位置づけ
        If _ShohinCode = String.Empty Then
            If dgvIchiran.Rows.Count <> 0 Then
                dgvIchiran.CurrentCell = dgvIchiran.Rows(0).Cells(M3010_COLNO_SHOHINCODE)
            End If
        Else
            For i As Integer = 0 To dgvIchiran.RowCount - 1
                If dgvIchiran.Rows(i).Cells(M3010_COLNO_SHOHINCODE).Value = _ShohinCode Then
                    dgvIchiran.CurrentCell = dgvIchiran.Rows(i).Cells(M3010_COLNO_SHOHINCODE)
                    _ShohinCode = String.Empty
                    Exit For
                End If
            Next i
        End If

        'ボタン制御
        '※モードに関わらず検索結果ありの場合は使用可にする
        cmdSyuturyoku.Enabled = True    'CSV出力ボタン
        cmdSansyou.Enabled = True       '参照ボタン
        cmdTuika.Enabled = True         '追加ボタン
        cmdFukusya.Enabled = True       '複写新規ボタン
        cmdHenkou.Enabled = True        '変更ボタン
        cmdSakujo.Enabled = True        '削除ボタン

        _Redisplay = False

    End Sub

#End Region

End Class