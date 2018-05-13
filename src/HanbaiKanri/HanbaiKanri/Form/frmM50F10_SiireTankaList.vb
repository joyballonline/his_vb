'===============================================================================
'　 （システム名）      カネキ吉田商店様向け　原価管理システム
'
'   （機能名）          仕入単価マスタ一覧
'   （クラス名）        frmM50F10_SiireTankaList
'   （処理機能名）      
'   （本MDL使用前提）   
'   （備考）         
'
'===============================================================================
' 履歴  名前               日付       　　　 　内容
'-------------------------------------------------------------------------------
'  (1)  桜井               2018/03/28      　　流用新規
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

Public Class frmM50F10_SiireTankaList
#Region "宣言"

    '-------------------------------------------------------------------------------
    ' 定数宣言
    '-------------------------------------------------------------------------------
    ' PG制御文字
    Private Const N As String = ControlChars.NewLine                    ' 改行文字
    Private Const RS As String = "RecSet"                               ' レコードセットテーブル

    '汎用マスメン
    Private Const lM5010_DB_EXIST As Integer = 1                        'あり：登録済
    Private Const lM5010_DB_NONE As Integer = 0                         'なし：未登録

    'グリッド列№ 
    Private Const M5010_COLNO_SHOHINCODE As Integer = 0                             '商品コード
    Private Const M5010_COLNO_SHOHINNAME As Integer = 1                             '商品名
    Private Const M5010_COLNO_TORICODE As Integer = 2                               '取引先コード
    Private Const M5010_COLNO_TORINAME As Integer = 3                               '取引先名
    Private Const M5010_COLNO_TEKIYOKAISIDATE As Integer = 4                        '適用開始日
    Private Const M5010_COLNO_TEKIYOSHURYODATE As Integer = 5                       '適用終了日
    Private Const M5010_COLNO_SIIRETANKA As Integer = 6                             '仕入単価
    Private Const M5010_COLNO_MEMO As Integer = 7                                   'メモ
    Private Const M5010_COLNO_UPDNM As Integer = 8                                  '更新者
    Private Const M5010_COLNO_UPDDT As Integer = 9                                  '更新日
    Private Const M5010_COLNO_MODFLG As Integer = 10                                '更新フラグ
    Private Const M5010_COLNO_HDSHOHINCODE As Integer = 11                          '変更前商品コード
    Private Const M5010_COLNO_HDTORICODE As Integer = 12                            '変更前取引先コード
    Private Const M5010_COLNO_HDTEKIYOKAISIDATE As Integer = 13                     '変更前適用開始日

    'グリッド列名 
    Private Const M5010_CCOL_SHOHINCODE As String = "cnShohinCode"                  '商品コード
    Private Const M5010_CCOL_SHOHINNAME As String = "cnShohinName"                  '商品名
    Private Const M5010_CCOL_TORICODE As String = "cnToriCode"                      '取引先コード
    Private Const M5010_CCOL_TORINAME As String = "cnToriName"                      '取引先名
    Private Const M5010_CCOL_TEKIYOKAISIDATE As String = "cnTekiyoKaisiDate"        '適用開始日
    Private Const M5010_CCOL_TEKIYOSHURYODATE As String = "cnTekiyoShuryoDate"      '適用終了日
    Private Const M5010_CCOL_SIIRETANKA As String = "cnSiireTanka"                  '仕入単価
    Private Const M5010_CCOL_MEMO As String = "cnMemo"                              'メモ
    Private Const M5010_CCOL_UPDNM As String = "cnUpdNm"                            '更新者
    Private Const M5010_CCOL_UPDDT As String = "cnUpdDt"                            '更新日
    Private Const M5010_CCOL_MODFLG As String = "cnModFlg"                          '更新フラグ
    Private Const M5010_CCOL_HDSHOHINCODE As String = "cnHideShohinCode"            '変更前商品コード
    Private Const M5010_CCOL_HDTORICODE As String = "cnHideToriCode"                '変更前取引先コード
    Private Const M5010_CCOL_HDTEKIYOKAISIDATE As String = "cnHideTekiyoKaisiDate"  '変更前適用開始日

    'グリッドデータ名 
    Private Const M5010_DTCOL_SHOHINCODE As String = "dtShohinCode"                 '商品コード
    Private Const M5010_DTCOL_SHOHINNAME As String = "dtShohinName"                 '商品名
    Private Const M5010_DTCOL_TORICODE As String = "dtToriCode"                     '取引先コード
    Private Const M5010_DTCOL_TORINAME As String = "dtToriName"                     '取引先名
    Private Const M5010_DTCOL_TEKIYOKAISIDATE As String = "dtTekiyoKaisiDate"       '適用開始日
    Private Const M5010_DTCOL_TEKIYOSHURYODATE As String = "dtTekiyoShuryoDate"     '適用終了日
    Private Const M5010_DTCOL_SIIRETANKA As String = "dtSiireTanka"                 '仕入単価
    Private Const M5010_DTCOL_MEMO As String = "dtMemo"                             'メモ
    Private Const M5010_DTCOL_UPDNM As String = "dtUpdNm"                           '更新者
    Private Const M5010_DTCOL_UPDDT As String = "dtUpdDt"                           '更新日
    Private Const M5010_DTCOL_MODFLG As String = "dtModFlg"                         '更新フラグ
    Private Const M5010_DTCOL_HDSHOHINCODE As String = "dtHideShohinCode"           '変更前商品コード
    Private Const M5010_DTCOL_HDTORICODE As String = "dtHideToriCode"               '変更前取引先コード
    Private Const M5010_DTCOL_HDTEKIYOKAISIDATE As String = "dtHideTekiyoKaisiDate" '変更前適用開始日

    'CSVファイル出力エラー関連
    Public Const NO_ERR_DATA As String = "該当データなし"
    Public Const CANCEL_ERR_DATA As String = "出力キャンセル"

    Private Const CSVXmlTagName As String = "CSV出力情報"                           'CSV出力情報のタグ名

    '取引先コード指定なし
    Private Const TORICODE_ALL As String = "ALL"                                    '指定なし

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
    Private _ToriCode As String
    Private _TekiyoFrDT As String
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

    Public Property toriCode() As String
        Get
            Return _ToriCode
        End Get
        Set(ByVal value As String)
            _ToriCode = value
        End Set
    End Property

    Public Property tekiyoFrDT() As String
        Get
            Return _TekiyoFrDT
        End Get
        Set(ByVal value As String)
            _TekiyoFrDT = value
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
    Private Sub frmM50F10_SiireTankaList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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
            fbd.SelectedPath = result("仕入単価マスタ出力先")

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
                CreateCsvM50(strPath, result("仕入単価マスタCSVファイル名"), ds, editFile)
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

        _ShohinCode = dgvIchiran.Rows(idx).Cells(M5010_CCOL_SHOHINCODE).Value
        _ToriCode = dgvIchiran.Rows(idx).Cells(M5010_CCOL_TORICODE).Value
        _TekiyoFrDT = dgvIchiran.Rows(idx).Cells(M5010_CCOL_TEKIYOKAISIDATE).Value
        Dim openForm As Form = Nothing
        openForm = New frmM50F20_SiireTankaHosyu(_msgHd, _db, Me, CommonConst.MODE_InquiryStatus, _selectId, _ShohinCode, _ToriCode, _TekiyoFrDT)   '処理選択
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
        openForm = New frmM50F20_SiireTankaHosyu(_msgHd, _db, Me, CommonConst.MODE_ADDNEW, _selectId, "", "", "")   '処理選択
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

        _ShohinCode = dgvIchiran.Rows(idx).Cells(M5010_CCOL_SHOHINCODE).Value
        _ToriCode = dgvIchiran.Rows(idx).Cells(M5010_CCOL_TORICODE).Value
        _TekiyoFrDT = dgvIchiran.Rows(idx).Cells(M5010_CCOL_TEKIYOKAISIDATE).Value
        Dim openForm As Form = Nothing
        openForm = New frmM50F20_SiireTankaHosyu(_msgHd, _db, Me, CommonConst.MODE_ADDNEWCOPY, _selectId, _ShohinCode, _ToriCode, _TekiyoFrDT)   '処理選択
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

        _ShohinCode = dgvIchiran.Rows(idx).Cells(M5010_CCOL_SHOHINCODE).Value
        _ToriCode = dgvIchiran.Rows(idx).Cells(M5010_CCOL_TORICODE).Value
        _TekiyoFrDT = dgvIchiran.Rows(idx).Cells(M5010_CCOL_TEKIYOKAISIDATE).Value
        Dim openForm As Form = Nothing
        openForm = New frmM50F20_SiireTankaHosyu(_msgHd, _db, Me, CommonConst.MODE_EditStatus, _selectId, _ShohinCode, _ToriCode, _TekiyoFrDT)   '処理選択
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

        _ShohinCode = dgvIchiran.Rows(idx).Cells(M5010_CCOL_SHOHINCODE).Value
        _ToriCode = dgvIchiran.Rows(idx).Cells(M5010_CCOL_TORICODE).Value
        _TekiyoFrDT = dgvIchiran.Rows(idx).Cells(M5010_CCOL_TEKIYOKAISIDATE).Value
        Dim openForm As Form = Nothing
        openForm = New frmM50F20_SiireTankaHosyu(_msgHd, _db, Me, CommonConst.MODE_DELETE, _selectId, _ShohinCode, _ToriCode, _TekiyoFrDT)   '処理選択
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
        cmdTuika.Enabled = True        '追加ボタン
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
    '   （処理概要）　仕入単価マスタ一覧画面に表示されている内容をCSVファイルへ出力する
    '   ●入力パラメタ   ：prmOutDir    出力Dir                    
    '                      prmOutFile   出力File                    
    '                      prmDataSet   データセット
    '                      prmRefEditFileNm
    '   ●メソッド戻り値 ：なし
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' CSV出力処理（CreateCsvM50）
    ''' </summary>
    ''' <param name="prmOutDir"></param>
    ''' <param name="prmOutFile"></param>
    ''' <param name="prmDataSet"></param>
    ''' <param name="prmRefEditFileNm"></param>
    Private Sub CreateCsvM50(ByVal prmOutDir As String, ByVal prmOutFile As String, ByVal prmDataSet As DataSet, Optional ByRef prmRefEditFileNm As String = "")

        '出力データ生成
        Dim outStr As String = CreateCsvDataM50(prmDataSet)

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
    '   （処理概要）　仕入単価マスタ一覧画面に表示されているデータを生成して返却する
    '   ●入力パラメタ   ：なし
    '   ●メソッド戻り値 ：CSV内容文字列
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' CSV生成処理（createCsvDataM50）
    ''' </summary>
    ''' <param name="prmDataSet"></param>
    Private Function CreateCsvDataM50(ByVal prmDataSet As DataSet) As String

        '形成
        Dim outStr As System.Text.StringBuilder = New System.Text.StringBuilder()
        With outStr
            'ヘッダ
            .Append("商品コード, 取引先コード, 適用開始日, 適用終了日, 仕入単価, メモ, 更新者, 更新日" & ControlChars.NewLine)

            '明細
            Dim t As DataTable = prmDataSet.Tables(RS)
            For i As Integer = 0 To prmDataSet.Tables(RS).Rows.Count - 1
                .Append("" & EnclosedItemDoubleQuotes(t.Rows(i)("dtShohinCode")) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtToriCode"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtTekiyoKaisiDate"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtTekiyoShuryoDate"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtSiireTanka"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtMemo"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtUpdNm"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtUpdDt"))) & "" & ControlChars.NewLine)
            Next
        End With

        CreateCsvDataM50 = outStr.ToString

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
    '   （処理概要）入力された条件に該当する仕入単価マスタデータを取得する
    '-------------------------------------------------------------------------------
    Private Sub Edit_frmM50F10_Ichiran()

        Dim iRecCnt As Integer = 0
        Dim oDataSet As DataSet = Nothing                           ' データセット型

        Try
            Dim sql As String = ""
            sql = "Select "
            sql = sql & N & "  pp.商品コード        " & M5010_DTCOL_SHOHINCODE        ' 0：商品コード
            sql = sql & N & ",  g.商品名            " & M5010_DTCOL_SHOHINNAME        ' 1：商品名
            sql = sql & N & ", pp.取引先コード      " & M5010_DTCOL_TORICODE          ' 2：取引先コード
            sql = sql & N & ", (Select Case pp.取引先コード WHEN '" & TORICODE_ALL & "' THEN '' "
            sql = sql & N & "                                                           ELSE c.取引先名 END) " & M5010_DTCOL_TORINAME    ' 3：取引先名
            sql = sql & N & ", pp.適用開始日        " & M5010_DTCOL_TEKIYOKAISIDATE   ' 4：適用開始日
            sql = sql & N & ", pp.適用終了日        " & M5010_DTCOL_TEKIYOSHURYODATE  ' 5：適用終了日
            sql = sql & N & ", TO_CHAR(pp.仕入単価, '99,999,999.99')          " & M5010_DTCOL_SIIRETANKA       ' 6：仕入単価
            sql = sql & N & ", pp.メモ              " & M5010_DTCOL_MEMO              ' 7：メモ
            sql = sql & N & ", u.氏名            " & M5010_DTCOL_UPDNM                ' 8：更新者
            sql = sql & N & ", TO_CHAR(pp.更新日, 'YYYY/MM/DD HH24:MI') " & M5010_DTCOL_UPDDT   ' 9：更新日時
            sql = sql & N & ", '0'               " & M5010_DTCOL_MODFLG               ' 10：更新フラグ
            sql = sql & N & ", pp.商品コード        " & M5010_DTCOL_HDSHOHINCODE      ' 11：変更前商品コード
            sql = sql & N & ", pp.取引先コード      " & M5010_DTCOL_HDTORICODE        ' 12：変更前取引先コード
            sql = sql & N & ", pp.適用開始日        " & M5010_DTCOL_HDTEKIYOKAISIDATE ' 13：変更前適用開始日
            sql = sql & N & " FROM M26_PCPRICE pp "
            sql = sql & N & " LEFT JOIN M20_GOODS g ON g.会社コード = pp.会社コード AND g.商品コード = pp.商品コード "
            sql = sql & N & " LEFT JOIN M10_CUSTOMER c ON c.会社コード = pp.会社コード AND c.取引先コード = pp.取引先コード "
            sql = sql & N & " LEFT JOIN M02_USER u ON u.会社コード = pp.会社コード AND u.ユーザＩＤ = pp.更新者 "
            sql = sql & N & " WHERE pp.会社コード  = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            sql = sql & N & makeLikeSql()
            sql = sql & N & " ORDER BY " & M5010_DTCOL_SHOHINCODE & ", " & M5010_DTCOL_TORICODE & ", " & M5010_DTCOL_TEKIYOKAISIDATE

            Try
                'sql発行
                oDataSet = _db.selectDB(sql, RS, iRecCnt)                     '抽出結果をDSへ格納
            Catch ex As Exception
                Throw New UsrDefException("DBデータ取得失敗", _msgHd.getMSG("ErrDbSelect", UtilClass.getErrDetail(ex)))
            End Try

            '抽出レコードが０件の場合、メッセージ表示
            If iRecCnt = 0 Then
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
    '　画面初期化処理
    '   （処理概要）ボタン・テキストロック設定
    '-------------------------------------------------------------------------------
    Private Sub Init_Form()

        Me.txtShohinCode.Text = String.Empty
        Me.txtShohinName.Text = String.Empty
        Me.txtToriCode.Text = String.Empty
        Me.txtToriName.Text = String.Empty
        Me.dtpKijunDt.Value = DBNull.Value

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
        Dim shohinCode As String = If(txtShohinCode.Text IsNot "", txtShohinCode.Text, "")                      '商品コード
        Dim shohinName As String = If(txtShohinName.Text IsNot "", txtShohinName.Text, "")                      '商品名
        Dim toriCode As String = If(txtToriCode.Text IsNot "", txtToriCode.Text, "")                            '取引先コード
        Dim toriName As String = If(txtToriName.Text IsNot "", txtToriName.Text, "")                            '取引先名

        Dim termsSql As String = ""
        termsSql += " and ( pp.商品コード like  '" & shohinCode & "%' "
        termsSql += " and    g.商品名 like  '%" & shohinName & "%' "
        termsSql += " and   pp.取引先コード like  '" & toriCode & "%' "
        If toriName <> "" Then
            termsSql += " and    c.取引先名 like  '%" & toriName & "%' "
        End If
        termsSql += " )"
        If dtpKijunDt.Value <> Nothing Then
            termsSql = termsSql & N & " and (pp.適用開始日 <= '" & Me.dtpKijunDt.Text & "' "
            termsSql = termsSql & N & " and  pp.適用終了日 >= '" & Me.dtpKijunDt.Text & "') "
        End If

        Return termsSql
    End Function

    Private Sub cmdKensaku_Click(sender As Object, e As EventArgs) Handles cmdKensaku.Click
        '一覧データを取得
        Edit_frmM50F10_Ichiran()

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

    Private Sub ctl_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtShohinCode.KeyPress,
                                                                               txtShohinName.KeyPress,
                                                                               txtToriCode.KeyPress,
                                                                               txtToriName.KeyPress,
                                                                               dtpKijunDt.KeyPress

        UtilMDL.UtilClass.moveNextFocus(Me, e)

    End Sub

    '-------------------------------------------------------------------------------
    '　フォーカス取得イベント
    '-------------------------------------------------------------------------------
    Private Sub ctl_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtShohinCode.GotFocus,
                                                                                          txtShohinName.GotFocus,
                                                                                          txtToriCode.GotFocus,
                                                                                          txtToriName.GotFocus,
                                                                                          dtpKijunDt.GotFocus

        'フォーカス取得時、入力パラメタのコントロールを全選択状態とする
        Call UtilClass.selAll(sender)

    End Sub

    Private Sub frmM50F10_SiireTankaList_Activated(sender As Object, e As EventArgs) Handles Me.Activated

        '保守画面から戻ってきたとき以外は処理終了
        If _Redisplay = False Then
            Exit Sub
        End If

        '一覧のクリア
        Call Clear_Ichiran()

        '検索データ表示
        Call Edit_frmM50F10_Ichiran()

        '保守画面から戻ってきたときの画面遷移前選択行への位置づけ
        If _ShohinCode = String.Empty Then
            If dgvIchiran.Rows.Count <> 0 Then
                dgvIchiran.CurrentCell = dgvIchiran.Rows(0).Cells(M5010_COLNO_TORICODE)
            End If
        Else
            For i As Integer = 0 To dgvIchiran.RowCount - 1
                If dgvIchiran.Rows(i).Cells(M5010_COLNO_SHOHINCODE).Value = _ShohinCode And
                    dgvIchiran.Rows(i).Cells(M5010_COLNO_TORICODE).Value = _ToriCode And
                    dgvIchiran.Rows(i).Cells(M5010_COLNO_TEKIYOKAISIDATE).Value = _TekiyoFrDT Then
                    dgvIchiran.CurrentCell = dgvIchiran.Rows(i).Cells(M5010_COLNO_SHOHINCODE)
                    _ShohinCode = String.Empty
                    _ToriCode = String.Empty
                    _TekiyoFrDT = String.Empty
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

    Private Sub txtShohinCode_DoubleClick(sender As Object, e As EventArgs) Handles txtShohinCode.DoubleClick
        Dim openForm As frmC10F10_Shohin = New frmC10F10_Shohin(_msgHd, _db, CommonConst.HAN_HANBAISIIRE_KBN_SHIIRE)      '画面遷移
        openForm.ShowDialog()                      '画面表示
        If openForm.Selected Then
            '選択されました
            Me.txtShohinCode.Text = openForm.GettShohinCD
        End If
        openForm = Nothing

    End Sub

    Private Sub txtToriCode_DoubleClick(sender As Object, e As EventArgs) Handles txtToriCode.DoubleClick
        Dim openForm As frmC10F20_Torihikisaki = New frmC10F20_Torihikisaki(_msgHd, _db, CommonConst.TORIHIKISAKI_TARGET_KBN_SHIIRE)      '画面遷移
        openForm.ShowDialog()                      '画面表示
        If openForm.Selected Then
            '選択されました
            Me.txtToriCode.Text = openForm.GetValTorihikisakiCd
        End If
        openForm = Nothing

    End Sub

#End Region

End Class