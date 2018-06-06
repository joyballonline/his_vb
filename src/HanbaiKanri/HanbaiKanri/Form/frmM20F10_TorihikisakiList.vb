'===============================================================================
'　 （システム名）      カネキ吉田商店様向け　原価管理システム
'
'   （機能名）          取引先マスタ一覧
'   （クラス名）        frmM20F10_TorihikisakiList
'   （処理機能名）      
'   （本MDL使用前提）   
'   （備考）         
'
'===============================================================================
' 履歴  名前               日付       　　　 　内容
'-------------------------------------------------------------------------------
'  (1)  桜井               2018/03/02      　　流用新規
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
Imports System.Text.RegularExpressions

Public Class frmM20F10_TorihikisakiList
#Region "宣言"

    '-------------------------------------------------------------------------------
    ' 定数宣言
    '-------------------------------------------------------------------------------
    ' PG制御文字
    Private Const N As String = ControlChars.NewLine                    ' 改行文字
    Private Const RS As String = "RecSet"                               ' レコードセットテーブル

    '汎用マスメン
    Private Const lM2010_DB_EXIST As Integer = 1                        'あり：登録済
    Private Const lM2010_DB_NONE As Integer = 0                         'なし：未登録

    'グリッド列№ 
    Private Const M2010_COLNO_TORICODE As Integer = 0                               '取引先コード
    Private Const M2010_COLNO_TORINAME As Integer = 1                               '取引先名
    Private Const M2010_COLNO_TORIRYAKUNAME As Integer = 2                          '取引先名略称
    Private Const M2010_COLNO_TORINAMEKANA As Integer = 3                           '取引先名カナ
    Private Const M2010_COLNO_POST As Integer = 4                                   '郵便番号
    Private Const M2010_COLNO_ADDR1 As Integer = 5                                  '住所１
    Private Const M2010_COLNO_ADDR2 As Integer = 6                                  '住所２
    Private Const M2010_COLNO_ADDR3 As Integer = 7                                  '住所３
    Private Const M2010_COLNO_TELNO As Integer = 8                                  '電話番号
    Private Const M2010_COLNO_TELNOSEARCH As Integer = 9                            '電話番号検索用
    Private Const M2010_COLNO_FAXNO As Integer = 10                                 'ＦＡＸ番号
    Private Const M2010_COLNO_TANTOSYA As Integer = 11                              '担当者名
    Private Const M2010_COLNO_IRAINUSI As Integer = 12                              '依頼主等
    Private Const M2010_COLNO_JIKANSITEI As Integer = 13                            '時間指定
    Private Const M2010_COLNO_HAISONISSU As Integer = 14                            '配送日数
    Private Const M2010_COLNO_UNSOBINCODE As Integer = 15                           '運送便コード
    Private Const M2010_COLNO_OKURIJOUMU As Integer = 16                            '送り状印刷有無
    Private Const M2010_COLNO_NIFUDAUMU As Integer = 17                             '荷札印刷有無
    Private Const M2010_COLNO_NOUHINDENPYOUMU As Integer = 18                       '納品伝票印刷有無
    Private Const M2010_COLNO_SEIKYUDENPYOUMU As Integer = 19                       '請求伝票印刷有無
    Private Const M2010_COLNO_LESPRITUMU As Integer = 20                            'レスプリ印刷有無
    Private Const M2010_COLNO_SIMEBI As Integer = 21                                '締日
    Private Const M2010_COLNO_BANKNAME As Integer = 22                              '銀行名
    Private Const M2010_COLNO_SITENNAME As Integer = 23                             '支店名
    Private Const M2010_COLNO_KOUZASYUBETU As Integer = 24                          '口座種別
    Private Const M2010_COLNO_KOUZANO As Integer = 25                               '口座番号
    Private Const M2010_COLNO_MEIGININ As Integer = 26                              '名義人名
    Private Const M2010_COLNO_KINGAKUHASUKUBUN As Integer = 27                      '金額端数区分
    Private Const M2010_COLNO_ZEISANSYUTUKUBUN As Integer = 28                      '税算出区分
    Private Const M2010_COLNO_ZEIHASUKUBUN As Integer = 29                          '税端数区分
    Private Const M2010_COLNO_SEIKYUSAKIGAITO As Integer = 30                       '請求先該当
    Private Const M2010_COLNO_SYUKKASAKIGAITO As Integer = 31                       '出荷先該当
    Private Const M2010_COLNO_SYUKKASAKIGGAITO As Integer = 32                      '出荷先Ｇ該当
    Private Const M2010_COLNO_SIIRESAKIGAITO As Integer = 33                        '仕入先該当
    Private Const M2010_COLNO_SIHARAISAKIGAITO As Integer = 34                      '支払先該当
    Private Const M2010_COLNO_SYUKKASAKIBUNRUI As Integer = 35                      '出荷先分類
    Private Const M2010_COLNO_SYUKKASAKIGCODE As Integer = 36                       '出荷先Ｇコード
    Private Const M2010_COLNO_SEIKYUSAKICODE As Integer = 37                        '請求先コード
    Private Const M2010_COLNO_SIHARAISAKICODE As Integer = 38                       '支払先コード
    Private Const M2010_COLNO_MEMO As Integer = 39                                  'メモ
    Private Const M2010_COLNO_UPDNM As Integer = 40                                 '更新者
    Private Const M2010_COLNO_UPDDT As Integer = 41                                 '更新日
    Private Const M2010_COLNO_MODFLG As Integer = 42                                '更新フラグ
    Private Const M2010_COLNO_HDTORICODE As Integer = 43                            '変更前取引先コード

    'グリッド列名 
    Private Const M2010_CCOL_TORICODE As String = "cnToriCode"                      '取引先コード
    Private Const M2010_CCOL_TORINAME As String = "cnToriName"                      '取引先名
    Private Const M2010_CCOL_TORIRYAKUNAME As String = "cnToriRyakuName"            '取引先名略称
    Private Const M2010_CCOL_TORINAMEKANA As String = "cnToriNameKana"              '取引先名カナ
    Private Const M2010_CCOL_POST As String = "cnPost"                              '郵便番号
    Private Const M2010_CCOL_ADDR1 As String = "cnAddr1"                            '住所１
    Private Const M2010_CCOL_ADDR2 As String = "cnAddr2"                            '住所２
    Private Const M2010_CCOL_ADDR3 As String = "cnAddr3"                            '住所３
    Private Const M2010_CCOL_TELNO As String = "cnTelNo"                            '電話番号
    Private Const M2010_CCOL_TELNOSEARCH As String = "cnTelNoSearch"                '電話番号検索用
    Private Const M2010_CCOL_FAXNO As String = "cnFaxNo"                            'ＦＡＸ番号
    Private Const M2010_CCOL_TANTOSYA As String = "cnTantoSya"                      '担当者名
    Private Const M2010_CCOL_IRAINUSI As String = "cnIrainusi"                      '依頼主等
    Private Const M2010_CCOL_JIKANSITEI As String = "cnJikanSitei"                  '時間指定
    Private Const M2010_CCOL_HAISONISSU As String = "cnHaisoNissu"                  '配送日数
    Private Const M2010_CCOL_UNSOBINCODE As String = "cnUnsobinCode"                '運送便コード
    Private Const M2010_CCOL_OKURIJOUMU As String = "cnOkurijoUmu"                  '送り状印刷有無
    Private Const M2010_CCOL_NIFUDAUMU As String = "cnNifudaUmu"                    '荷札印刷有無
    Private Const M2010_CCOL_NOUHINDENPYOUMU As String = "cnNouhinDenpyoUmu"        '納品伝票印刷有無
    Private Const M2010_CCOL_SEIKYUDENPYOUMU As String = "cnSeikyuDenpyoUmu"        '請求伝票印刷有無
    Private Const M2010_CCOL_LESPRITUMU As String = "cnLespritUmu"                  'レスプリ印刷有無
    Private Const M2010_CCOL_SIMEBI As String = "cnSimebi"                          '締日
    Private Const M2010_CCOL_BANKNAME As String = "cnBankName"                      '銀行名
    Private Const M2010_CCOL_SITENNAME As String = "cnSitenName"                    '支店名
    Private Const M2010_CCOL_KOUZASYUBETU As String = "cnKouzaSyubetu"              '口座種別
    Private Const M2010_CCOL_KOUZANO As String = "cnKouzaNo"                        '口座番号
    Private Const M2010_CCOL_MEIGININ As String = "cnMeiginin"                      '名義人名
    Private Const M2010_CCOL_KINGAKUHASUKUBUN As String = "cnKingakuHasuKubun"      '金額端数区分
    Private Const M2010_CCOL_ZEISANSYUTUKUBUN As String = "cnZeiSansyutuKubun"      '税算出区分
    Private Const M2010_CCOL_ZEIHASUKUBUN As String = "cnZeiHasuKubun"              '税端数区分
    Private Const M2010_CCOL_SEIKYUSAKIGAITO As String = "cnSeikyusakiGaito"        '請求先該当
    Private Const M2010_CCOL_SYUKKASAKIGAITO As String = "cnSyukkasakiGaito"        '出荷先該当
    Private Const M2010_CCOL_SYUKKASAKIGGAITO As String = "cnSyukkasakiGGaito"      '出荷先Ｇ該当
    Private Const M2010_CCOL_SIIRESAKIGAITO As String = "cnSiiresakiGaito"          '仕入先該当
    Private Const M2010_CCOL_SIHARAISAKIGAITO As String = "cnSiharaisakiGaito"      '支払先該当
    Private Const M2010_CCOL_SYUKKASAKIBUNRUI As String = "cnSyukkasakiBunrui"      '出荷先分類
    Private Const M2010_CCOL_SYUKKASAKIGCODE As String = "cnSyukkasakiGCode"        '出荷先Ｇコード
    Private Const M2010_CCOL_SEIKYUSAKICODE As String = "cnSeikyusakiCode"          '請求先コード
    Private Const M2010_CCOL_SIHARAISAKICODE As String = "cnSiharaisakiCode"        '支払先コード
    Private Const M2010_CCOL_MEMO As String = "cnMemo"                              'メモ
    Private Const M2010_CCOL_UPDNM As String = "cnUpdNm"                            '更新者
    Private Const M2010_CCOL_UPDDT As String = "cnUpdDt"                            '更新日
    Private Const M2010_CCOL_MODFLG As String = "cnModFlg"                          '更新フラグ
    Private Const M2010_CCOL_HDTORICODE As String = "cnHideToriCode"                '変更前取引先コード

    'グリッドデータ名 
    Private Const M2010_DTCOL_TORICODE As String = "dtToriCode"                     '取引先コード
    Private Const M2010_DTCOL_TORINAME As String = "dtToriName"                     '取引先名
    Private Const M2010_DTCOL_TORIRYAKUNAME As String = "dtToriRyakuName"           '取引先名略称
    Private Const M2010_DTCOL_TORINAMEKANA As String = "dtToriNameKana"             '取引先名カナ
    Private Const M2010_DTCOL_POST As String = "dtPost"                             '郵便番号
    Private Const M2010_DTCOL_ADDR1 As String = "dtAddr1"                           '住所１
    Private Const M2010_DTCOL_ADDR2 As String = "dtAddr2"                           '住所２
    Private Const M2010_DTCOL_ADDR3 As String = "dtAddr3"                           '住所３
    Private Const M2010_DTCOL_TELNO As String = "dtTelNo"                           '電話番号
    Private Const M2010_DTCOL_TELNOSEARCH As String = "dtTelNoSearch"               '電話番号検索用
    Private Const M2010_DTCOL_FAXNO As String = "dtFaxNo"                           'ＦＡＸ番号
    Private Const M2010_DTCOL_TANTOSYA As String = "dtTantoSya"                     '担当者名
    Private Const M2010_DTCOL_IRAINUSI As String = "dtIrainusi"                     '依頼主等
    Private Const M2010_DTCOL_JIKANSITEI As String = "dtJikanSitei"                 '時間指定
    Private Const M2010_DTCOL_HAISONISSU As String = "dtHaisoNissu"                 '配送日数
    Private Const M2010_DTCOL_UNSOBINCODE As String = "dtUnsobinCode"               '運送便コード
    Private Const M2010_DTCOL_OKURIJOUMU As String = "dtOkurijoUmu"                 '送り状印刷有無
    Private Const M2010_DTCOL_NIFUDAUMU As String = "dtNifudaUmu"                   '荷札印刷有無
    Private Const M2010_DTCOL_NOUHINDENPYOUMU As String = "dtNouhinDenpyoUmu"       '納品伝票印刷有無
    Private Const M2010_DTCOL_SEIKYUDENPYOUMU As String = "dtSeikyuDenpyoUmu"       '請求伝票印刷有無
    Private Const M2010_DTCOL_LESPRITUMU As String = "dtLespritUmu"                 'レスプリ印刷有無
    Private Const M2010_DTCOL_SIMEBI As String = "dtSimebi"                         '締日
    Private Const M2010_DTCOL_BANKNAME As String = "dtBankName"                     '銀行名
    Private Const M2010_DTCOL_SITENNAME As String = "dtSitenName"                   '支店名
    Private Const M2010_DTCOL_KOUZASYUBETU As String = "dtKouzaSyubetu"             '口座種別
    Private Const M2010_DTCOL_KOUZANO As String = "dtKouzaNo"                       '口座番号
    Private Const M2010_DTCOL_MEIGININ As String = "dtMeiginin"                     '名義人名
    Private Const M2010_DTCOL_KINGAKUHASUKUBUN As String = "dtKingakuHasuKubun"     '金額端数区分
    Private Const M2010_DTCOL_ZEISANSYUTUKUBUN As String = "dtZeiSansyutuKubun"     '税算出区分
    Private Const M2010_DTCOL_ZEIHASUKUBUN As String = "dtZeiHasuKubun"             '税端数区分
    Private Const M2010_DTCOL_SEIKYUSAKIGAITO As String = "dtSeikyusakiGaito"       '請求先該当
    Private Const M2010_DTCOL_SYUKKASAKIGAITO As String = "dtSyukkasakiGaito"       '出荷先該当
    Private Const M2010_DTCOL_SYUKKASAKIGGAITO As String = "dtSyukkasakiGGaito"     '出荷先Ｇ該当
    Private Const M2010_DTCOL_SIIRESAKIGAITO As String = "dtSiiresakiGaito"         '仕入先該当
    Private Const M2010_DTCOL_SIHARAISAKIGAITO As String = "dtSiharaisakiGaito"     '支払先該当
    Private Const M2010_DTCOL_SYUKKASAKIBUNRUI As String = "dtSyukkasakiBunrui"     '出荷先分類
    Private Const M2010_DTCOL_SYUKKASAKIGCODE As String = "dtSyukkasakiGCode"       '出荷先Ｇコード
    Private Const M2010_DTCOL_SEIKYUSAKICODE As String = "dtSeikyusakiCode"         '請求先コード
    Private Const M2010_DTCOL_SIHARAISAKICODE As String = "dtSiharaisakiCode"       '支払先コード
    Private Const M2010_DTCOL_MEMO As String = "dtMemo"                             'メモ
    Private Const M2010_DTCOL_UPDNM As String = "dtUpdNm"                           '更新者
    Private Const M2010_DTCOL_UPDDT As String = "dtUpdDt"                           '更新日
    Private Const M2010_DTCOL_MODFLG As String = "dtModFlg"                         '更新フラグ
    Private Const M2010_DTCOL_HDTORICODE As String = "dtHideToriCode"               '変更前取引先コード

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
    Private _ToriCode As String
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

    Public Property toriCode() As String
        Get
            Return _ToriCode
        End Get
        Set(ByVal value As String)
            _ToriCode = value
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
    Private Sub frmM20F10_TorihikisakiList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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
            fbd.SelectedPath = result("取引先マスタ出力先")

            'ダイアログを表示する
            If fbd.ShowDialog() <> DialogResult.OK Then
                Exit Sub
            End If

            Dim strPath As String = ""

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
                CreateCsvM20(strPath, result("取引先マスタCSVファイル名"), ds, editFile)
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

        _ToriCode = dgvIchiran.Rows(idx).Cells(M2010_CCOL_TORICODE).Value
        Dim openForm As Form = Nothing
        openForm = New frmM20F20_TorihikisakiHosyu(_msgHd, _db, Me, CommonConst.MODE_InquiryStatus, _selectId, _ToriCode)   '処理選択
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
        openForm = New frmM20F20_TorihikisakiHosyu(_msgHd, _db, Me, CommonConst.MODE_ADDNEW, _selectId, "")   '処理選択
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

        _ToriCode = dgvIchiran.Rows(idx).Cells(M2010_CCOL_TORICODE).Value
        Dim openForm As Form = Nothing
        openForm = New frmM20F20_TorihikisakiHosyu(_msgHd, _db, Me, CommonConst.MODE_ADDNEWCOPY, _selectId, _ToriCode)   '処理選択
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

        _ToriCode = dgvIchiran.Rows(idx).Cells(M2010_CCOL_TORICODE).Value
        Dim openForm As Form = Nothing
        openForm = New frmM20F20_TorihikisakiHosyu(_msgHd, _db, Me, CommonConst.MODE_EditStatus, _selectId, _ToriCode)   '処理選択
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

        _ToriCode = dgvIchiran.Rows(idx).Cells(M2010_CCOL_TORICODE).Value
        Dim openForm As Form = Nothing
        openForm = New frmM20F20_TorihikisakiHosyu(_msgHd, _db, Me, CommonConst.MODE_DELETE, _selectId, _ToriCode)   '処理選択
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
    '   （処理概要）　取引先マスタ一覧画面に表示されている内容をCSVファイルへ出力する
    '   ●入力パラメタ   ：prmOutDir    出力Dir                    
    '                      prmOutFile   出力File                    
    '                      prmDataSet   データセット
    '                      prmRefEditFileNm
    '   ●メソッド戻り値 ：なし
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' CSV出力処理（CreateCsvM20）
    ''' </summary>
    ''' <param name="prmOutDir"></param>
    ''' <param name="prmOutFile"></param>
    ''' <param name="prmDataSet"></param>
    ''' <param name="prmRefEditFileNm"></param>
    Private Sub CreateCsvM20(ByVal prmOutDir As String, ByVal prmOutFile As String, ByVal prmDataSet As DataSet, Optional ByRef prmRefEditFileNm As String = "")

        '出力データ生成
        Dim outStr As String = CreateCsvDataM20(prmDataSet)

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
    '   （処理概要）　取引先マスタ一覧画面に表示されているデータを生成して返却する
    '   ●入力パラメタ   ：なし
    '   ●メソッド戻り値 ：CSV内容文字列
    '-------------------------------------------------------------------------------
    ''' <summary>
    ''' CSV生成処理（createCsvDataM20）
    ''' </summary>
    ''' <param name="prmDataSet"></param>
    Private Function CreateCsvDataM20(ByVal prmDataSet As DataSet) As String

        '形成
        Dim outStr As System.Text.StringBuilder = New System.Text.StringBuilder()
        With outStr
            'ヘッダ
            .Append("取引先コード, 取引先名, 取引先名略称, 取引先名カナ, 郵便番号, 住所１, 住所２, 住所３, 電話番号, 電話番号検索用, ＦＡＸ番号, 担当者名, 依頼主等, 時間指定, 配送日数, 運送便コード, 送り状印刷有無, 荷札印刷有無, 納品伝票印刷有無, 請求伝票印刷有無, レスプリ印刷有無, 締日, 銀行名, 支店名, 口座種別, 口座番号, 名義人名, 金額端数区分, 税算出区分, 税端数区分, 請求先該当, 出荷先該当, 出荷先Ｇ該当, 仕入先該当, 支払先該当, 出荷先分類, 出荷先Ｇコード, 請求先コード, 支払先コード, メモ, 更新者, 更新日" & ControlChars.NewLine)

            '明細
            Dim t As DataTable = prmDataSet.Tables(RS)
            For i As Integer = 0 To prmDataSet.Tables(RS).Rows.Count - 1
                .Append("" & EnclosedItemDoubleQuotes(t.Rows(i)("dtToriCode")) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtToriName"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtToriRyakuName"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtToriNameKana"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtPost"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtAddr1"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtAddr2"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtAddr3"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtTelNo"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtTelNoSearch"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtFaxNo"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtTantoSya"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtIrainusi"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtJikanSitei"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtHaisoNissu"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtUnsobinCode"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtOkurijoUmu"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtNifudaUmu"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtNouhinDenpyoUmu"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtSeikyuDenpyoUmu"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtLespritUmu"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtSimebi"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtBankName"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtSitenName"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtKouzaSyubetu"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtKouzaNo"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtMeiginin"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtKingakuHasuKubun"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtZeiSansyutuKubun"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtZeiHasuKubun"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtSeikyusakiGaito"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtSyukkasakiGaito"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtSyukkasakiGGaito"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtSiiresakiGaito"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtSiharaisakiGaito"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtSyukkasakiBunrui"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtSyukkasakiGCode"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtSeikyusakiCode"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtSiharaisakiCode"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtMemo"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtUpdNm"))) & ",")
                .Append("" & EnclosedItemDoubleQuotes(_db.rmNullStr(t.Rows(i)("dtUpdDt"))) & "" & ControlChars.NewLine)
            Next
        End With

        CreateCsvDataM20 = outStr.ToString

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
    '   （処理概要）入力された条件に該当する取引先マスタデータを取得する
    '-------------------------------------------------------------------------------
    Private Sub Edit_frmM20F10_Ichiran()

        Dim iRecCnt As Integer = 0
        Dim oDataSet As DataSet = Nothing                           ' データセット型

        Try
            Dim sql As String = ""
            sql = "Select "
            sql = sql & N & "  c.取引先コード      " & M2010_DTCOL_TORICODE          ' 0：取引先コード
            sql = sql & N & ", c.取引先名          " & M2010_DTCOL_TORINAME          ' 1：取引先名
            sql = sql & N & ", c.取引先名略称      " & M2010_DTCOL_TORIRYAKUNAME     ' 2：取引先名略称
            sql = sql & N & ", c.取引先名カナ      " & M2010_DTCOL_TORINAMEKANA      ' 3：取引先名カナ
            sql = sql & N & ", c.郵便番号          " & M2010_DTCOL_POST              ' 4：郵便番号
            sql = sql & N & ", c.住所１            " & M2010_DTCOL_ADDR1             ' 5：住所１
            sql = sql & N & ", c.住所２            " & M2010_DTCOL_ADDR2             ' 6：住所２
            sql = sql & N & ", c.住所３            " & M2010_DTCOL_ADDR3             ' 7：住所３
            sql = sql & N & ", c.電話番号          " & M2010_DTCOL_TELNO             ' 8：電話番号
            sql = sql & N & ", c.電話番号検索用    " & M2010_DTCOL_TELNOSEARCH       ' 9：電話番号検索用
            sql = sql & N & ", c.ＦＡＸ番号        " & M2010_DTCOL_FAXNO             ' 10：ＦＡＸ番号
            sql = sql & N & ", c.担当者名          " & M2010_DTCOL_TANTOSYA          ' 11：担当者名
            sql = sql & N & ", c.依頼主等          " & M2010_DTCOL_IRAINUSI          ' 12：依頼主等
            sql = sql & N & ", c.時間指定          " & M2010_DTCOL_JIKANSITEI        ' 13：時間指定
            sql = sql & N & ", c.配送日数          " & M2010_DTCOL_HAISONISSU        ' 14：配送日数
            sql = sql & N & ", c.運送便コード      " & M2010_DTCOL_UNSOBINCODE       ' 15：運送便コード
            sql = sql & N & ", c.送り状印刷有無    " & M2010_DTCOL_OKURIJOUMU        ' 16：送り状印刷有無
            sql = sql & N & ", c.荷札印刷有無      " & M2010_DTCOL_NIFUDAUMU         ' 17：荷札印刷有無
            sql = sql & N & ", c.納品伝票印刷有無  " & M2010_DTCOL_NOUHINDENPYOUMU   ' 18：納品伝票印刷有無
            sql = sql & N & ", c.請求伝票印刷有無  " & M2010_DTCOL_SEIKYUDENPYOUMU   ' 19：請求伝票印刷有無
            sql = sql & N & ", c.レスプリ印刷有無  " & M2010_DTCOL_LESPRITUMU        ' 20：レスプリ印刷有無
            sql = sql & N & ", c.締日              " & M2010_DTCOL_SIMEBI            ' 21：締日
            sql = sql & N & ", c.銀行名            " & M2010_DTCOL_BANKNAME          ' 22：銀行名
            sql = sql & N & ", c.支店名            " & M2010_DTCOL_SITENNAME         ' 23：支店名
            sql = sql & N & ", c.口座種別          " & M2010_DTCOL_KOUZASYUBETU      ' 24：口座種別
            sql = sql & N & ", c.口座番号          " & M2010_DTCOL_KOUZANO           ' 25：口座番号
            sql = sql & N & ", c.名義人名          " & M2010_DTCOL_MEIGININ          ' 26：名義人名
            sql = sql & N & ", c.金額端数区分      " & M2010_DTCOL_KINGAKUHASUKUBUN  ' 27：金額端数区分
            sql = sql & N & ", c.税算出区分        " & M2010_DTCOL_ZEISANSYUTUKUBUN  ' 28：税算出区分
            sql = sql & N & ", c.税端数区分        " & M2010_DTCOL_ZEIHASUKUBUN      ' 29：税端数区分
            sql = sql & N & ", c.請求先該当        " & M2010_DTCOL_SEIKYUSAKIGAITO   ' 30：請求先該当
            sql = sql & N & ", c.出荷先該当        " & M2010_DTCOL_SYUKKASAKIGAITO   ' 31：出荷先該当
            sql = sql & N & ", c.出荷先Ｇ該当      " & M2010_DTCOL_SYUKKASAKIGGAITO  ' 32：出荷先Ｇ該当
            sql = sql & N & ", c.仕入先該当        " & M2010_DTCOL_SIIRESAKIGAITO    ' 33：仕入先該当
            sql = sql & N & ", c.支払先該当        " & M2010_DTCOL_SIHARAISAKIGAITO  ' 34：支払先該当
            sql = sql & N & ", c.出荷先分類        " & M2010_DTCOL_SYUKKASAKIBUNRUI  ' 35：出荷先分類
            sql = sql & N & ", c.出荷先Ｇコード    " & M2010_DTCOL_SYUKKASAKIGCODE   ' 36：出荷先Ｇコード
            sql = sql & N & ", c.請求先コード      " & M2010_DTCOL_SEIKYUSAKICODE    ' 37：請求先コード
            sql = sql & N & ", c.支払先コード      " & M2010_DTCOL_SIHARAISAKICODE   ' 38：支払先コード
            sql = sql & N & ", c.メモ              " & M2010_DTCOL_MEMO              ' 39：メモ
            sql = sql & N & ", u.氏名            " & M2010_DTCOL_UPDNM               ' 40：更新者
            sql = sql & N & ", TO_CHAR(c.更新日, 'YYYY/MM/DD HH24:MI') " & M2010_DTCOL_UPDDT   ' 41：更新日時
            sql = sql & N & ", '0'               " & M2010_DTCOL_MODFLG              ' 42：更新フラグ
            sql = sql & N & ", c.取引先コード      " & M2010_DTCOL_HDTORICODE        ' 43：変更前取引先コード
            sql = sql & N & " FROM M10_CUSTOMER c "
            sql = sql & N & " LEFT JOIN M02_USER u ON u.会社コード = c.会社コード AND u.ユーザＩＤ = c.更新者 "
            sql = sql & N & " WHERE c.会社コード  = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            sql = sql & N & makeLikeSql()
            sql = sql & N & " ORDER BY " & M2010_DTCOL_TORICODE

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
    '　画面初期化処理
    '   （処理概要）ボタン・テキストロック設定
    '-------------------------------------------------------------------------------
    Private Sub Init_Form()

        Me.txtTorihikisakiName.Text = String.Empty
        Me.txtAddress.Text = String.Empty
        Me.txtTel.Text = String.Empty

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
        Dim torihikisakiName As String = If(txtTorihikisakiName.Text IsNot "", txtTorihikisakiName.Text, "")     '取引先名
        Dim address As String = If(txtAddress.Text IsNot "", txtAddress.Text, "")                   '住所
        Dim tel As String = If(txtTel.Text IsNot "", txtTel.Text, "")                           '電話番号
        Dim reg As New Regex("[^\d]")
        Dim strDes As String = reg.Replace(tel, "")

        Dim termsSql As String = ""
        termsSql += " and ( 取引先名 like  '%" & torihikisakiName & "%' "
        termsSql += " and (coalesce(住所１,'') || coalesce(住所２,'') || coalesce(住所３,'')) like  '%" & address & "%' "
        termsSql += " and 電話番号検索用 like  '" & strDes & "%' ) "

        Return termsSql
    End Function

    Private Sub cmdKensaku_Click(sender As Object, e As EventArgs) Handles cmdKensaku.Click
        '一覧データを取得
        Edit_frmM20F10_Ichiran()

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

    Private Sub ctl_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTorihikisakiName.KeyPress,
                                                                               txtAddress.KeyPress,
                                                                               txtTel.KeyPress

        UtilMDL.UtilClass.moveNextFocus(Me, e)

    End Sub

    '-------------------------------------------------------------------------------
    '　フォーカス取得イベント
    '-------------------------------------------------------------------------------
    Private Sub ctl_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTorihikisakiName.GotFocus,
                                                                                          txtAddress.GotFocus,
                                                                                          txtTel.GotFocus

        'フォーカス取得時、入力パラメタのコントロールを全選択状態とする
        Call UtilClass.selAll(sender)

    End Sub

    Private Sub frmM20F10_TorihikisakiList_Activated(sender As Object, e As EventArgs) Handles Me.Activated

        '保守画面から戻ってきたとき以外は処理終了
        If _Redisplay = False Then
            Exit Sub
        End If

        '一覧のクリア
        Call Clear_Ichiran()

        '検索データ表示
        Call Edit_frmM20F10_Ichiran()

        '保守画面から戻ってきたときの画面遷移前選択行への位置づけ
        If _ToriCode = String.Empty Then
            If dgvIchiran.Rows.Count <> 0 Then
                dgvIchiran.CurrentCell = dgvIchiran.Rows(0).Cells(M2010_COLNO_TORICODE)
            End If
        Else
            For i As Integer = 0 To dgvIchiran.RowCount - 1
                If dgvIchiran.Rows(i).Cells(M2010_COLNO_TORICODE).Value = _ToriCode Then
                    dgvIchiran.CurrentCell = dgvIchiran.Rows(i).Cells(M2010_COLNO_TORICODE)
                    _ToriCode = String.Empty
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