'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）計画対象品マスタメンテ修正画面
'    （フォームID）ZM120E_Syuusei
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   中澤        2010/11/02                 新規              
'　(2)   菅野        2010/12/17                 変更　ソート順を変更             
'　(3)   菅野        2011/01/13                 変更　科目コード桁数チェック変更
'　(4)   菅野        2011/02/10                 修正　更新ボタン押下時の不具合対応
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.Combo
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls

Public Class ZM120E_Syuusei
    Implements IfRturnKahenKey

#Region "リテラル値定義"
    '-------------------------------------------------------------------------------
    '   定数定義
    '-------------------------------------------------------------------------------
    'ヘッダ行数
    Private Const MZ0203_ROW_HEADER As Integer = -1             'ヘッダ行数

    'PG制御文字 
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const RS As String = "RecSet"                       'レコードセットテーブル
    Private Const RS2 As String = "RecSetM12ForxLS"             'レコードセットテーブル
    Private Const PGID As String = "ZM120E"                     'T91に登録するPGID

    '一覧列名
    Private Const COLCN_HINMEICD As String = "cnHinmeiCD"               '品名コード
    Private Const COLCN_HINMEI As String = "cnHinmei"                   '品名
    Private Const COLCN_SEISAKUKBN As String = "cnSeisakuKbn"           '製作区分
    Private Const COLCN_LOTTYOU As String = "cnLottyou"                 '標準ロット長
    Private Const COLCN_TANTYOU As String = "cnSeisakuTantyou"          '製作単長
    Private Const COLCN_JOSU As String = "cnJosu"                       '条数
    Private Const COLCN_KND As String = "cnKHonsuu"                     'KND入庫
    Private Const COLCN_HST As String = "cnHSTHonsuu"                   'HST入庫
    Private Const COLCN_ZAIKO As String = "cnZKurikaesi"                '在庫繰返
    Private Const COLCN_ZAIKOBTN As String = "cnZKurikaesiBtn"          '在庫繰返ボタン
    Private Const COLCN_ZAIKONM As String = "cnZKurikaesinm"            '在庫繰返名
    Private Const COLCN_CHUMONSAKI As String = "cnChumonsaki"           '注文先
    Private Const COLCN_JUYOUSAKINAME As String = "cnJuyousakiName"     '需要先名
    Private Const COLCN_JUYOUSAKI As String = "cnJuyousaki"             '需要先
    Private Const COLCN_JUYOUSAKIBTN As String = "cnJuyousakiBtn"       '需要先ボタン
    Private Const COLCN_SIZETENKAI As String = "cnSizeTenkai"           'サイズ展開
    Private Const COLCN_SIZETENKAIBTN As String = "cnSizeTenkaiBtn"     'サイズ展開ボタン
    Private Const COLCN_HINSYUKBN As String = "cnHinsyuKbn"             '品種区分
    Private Const COLCN_KIJUNTUKISU As String = "cnKijunTuki"           '基準月数
    Private Const COLCN_SAIGAI As String = "cnSaigai"                   '災害復旧用在庫
    Private Const COLCN_ANZENZAIKO As String = "cnAnzenZaiko"           '安全在庫
    Private Const COLCN_SHINMEI As String = "cnSHinmei"                 '集計品名
    Private Const COLCN_SHINMEIBTN As String = "cnSHinmeiBtn"           '集計品名ボタン
    Private Const COLCN_KAMOKUCD As String = "cnKamokuCD"               '科目コード
    Private Const COLCN_MAKIWAKU As String = "cnMakiwaku"               '巻枠コード
    Private Const COLCN_HOUSOU As String = "cnHousou"                   '包装／表示区分
    Private Const COLCN_SIYOUSYONO As String = "cnSiyousyoNo"           '仕様書No
    Private Const COLCN_SEIZOUBUMON As String = "cnSeizouBumon"         '製造部門コード
    Private Const COLCN_SEIZOUBUMONBTN As String = "cnSeizouBumonBtn"   '製造部門コードボタン
    Private Const COLCN_SEIZOUBUMONNM As String = "cnSeizouBumonnm"     '製造部門名
    Private Const COLCN_TENKAIKBN As String = "cnTenkaiKbn"             '展開区分
    Private Const COLCN_TENKAIKBNBTN As String = "cnTenkaiKbnBtn"       '展開区分ボタン
    Private Const COLCN_TENKAIKBNNM As String = "cnTenkaiKbnnm"         '展開区分名
    Private Const COLCN_BUBUNKOUTEI As String = "cnBubunKoutei"         '部分工程
    Private Const COLCN_HINSITU As String = "cnHinsitu"                 '品質試験
    Private Const COLCN_HINSITUBTN As String = "cnHinsituBtn"           '品質試験ボタン
    Private Const COLCN_TATIAI As String = "cnTatiai"                   '立会有無
    Private Const COLCN_TATIAIBTN As String = "cnTatiaiBtn"             '立会有無ボタン
    Private Const COLCN_CHANGEFLG As String = "cnChangeFlg"             '変更フラグ

    '一覧バインドDetaSet列名
    Private Const COLDT_HINMEICD As String = "dtHinmeiCD"               '品名コード
    Private Const COLDT_HINMEI As String = "dtHinmei"                   '品名
    Private Const COLDT_SEISAKUKUBUN As String = "dtSeisakuKbn"         '製作区分
    Private Const COLDT_LOTTYOU As String = "dtLottyou"                 '標準ロット長
    Private Const COLDT_TANTYOU As String = "dtSeisakuTantyou"          '単長
    Private Const COLDT_JOSU As String = "dtJosu"                       '条数
    Private Const COLDT_KND As String = "dtKHonsuu"                     'KND入庫
    Private Const COLDT_HST As String = "dtHSTHonsuu"                   'HST入庫
    Private Const COLDT_ZAIKO As String = "dtZKurikaesi"                '在庫繰返
    Private Const COLDT_ZAIKOBTN As String = "dtZKurikaesiBtn"          '在庫繰返ボタン
    Private Const COLDT_ZAIKONM As String = "dtZKurikaesinm"            '在庫繰返名
    Private Const COLDT_CHUMONSAKI As String = "dtChumonsaki"           '注文先
    Private Const COLDT_JUYOUSAKINAME As String = "dtJuyousakiName"     '需要先名
    Private Const COLDT_JUYOUSAKI As String = "dtJuyousaki"             '需要先
    Private Const COLDT_JUYOUSAKIBTN As String = "dtJuyousakiBtn"       '需要先ボタン
    Private Const COLDT_SIZETENKAI As String = "dtSizeTenkai"           'サイズ展開
    Private Const COLDT_SIZETENKAIBTN As String = "dtSizeTenkaiBtn"     'サイズ展開ボタン
    Private Const COLDT_HINSYUKBN As String = "dtHinsyuKbn"             '品種区分
    Private Const COLDT_KIJUNTUKISU As String = "dtKijunTuki"           '基準月数
    Private Const COLDT_SAIGAI As String = "dtSaigai"                   '災害復旧用在庫
    Private Const COLDT_ANZENZAIKO As String = "dtAnzenZaiko"           '安全在庫
    Private Const COLDT_SHINMEI As String = "dtSHinmei"                 '集計品名
    Private Const COLDT_SHINMEIBTN As String = "dtSHinmeiBtn"           '集計品名ボタン
    Private Const COLDT_KAMOKUCD As String = "dtKamokuCD"               '科目コード
    Private Const COLDT_MAKIWAKU As String = "dtMakiwaku"               '巻枠コード
    Private Const COLDT_HOUSOU As String = "dtHousou"                   '包装／表示区分
    Private Const COLDT_SIYOUSYONO As String = "dtSiyousyoNo"           '仕様書番号
    Private Const COLDT_SEIZOUBUMON As String = "dtSeizouBumon"         '製造部門
    Private Const COLDT_SEIZOUBUMONBTN As String = "dtSeizouBumonBtn"   '製造部門ボタン
    Private Const COLDT_SEIZOUBUMONNM As String = "dtSeizouBumonnm"     '製造部門名
    Private Const COLDT_TENKAIKBN As String = "dtTenkaiKbn"             '展開区分
    Private Const COLDT_TENKAIKBNBTN As String = "dtTenkaiKbnBtn"       '展開区分ボタン
    Private Const COLDT_TENKAIKBNNM As String = "dtTenkaiKbnnm"         '展開区分名
    Private Const COLDT_BUBUNKOUTEI As String = "dtBubunKoutei"         '部分工程
    Private Const COLDT_HINSITU As String = "dtHinsitu"                 '品質試験
    Private Const COLDT_HINSITUBTN As String = "dtHinsituBtn"           '品質試験ボタン
    Private Const COLDT_TATIAI As String = "dtTatiai"                   '立会有無
    Private Const COLDT_TATIAIBTN As String = "dtTatiaiBtn"             '立会有無ボタン
    Private Const COLDT_CHANGEFLG As String = "dtChangeFlg"             '変更フラグ

    Private Const COLDT_M12KHINMEICD As String = "KHINMEICD"
    Private Const COLDT_M12HINMEICD As String = "HINMEICD"

    '一覧列番号
    Private Const COLNO_HINMEICD As Integer = 0             '品名コード
    Private Const COLNO_HINMEI As Integer = 1               '品名
    Private Const COLNO_LOTTYOU As Integer = 2              '標準ロット長
    Private Const COLNO_TANTYOU As Integer = 3              '単長
    Private Const COLNO_JOSU As Integer = 4                 '条数
    Private Const COLNO_KND As Integer = 5                  '入庫本数 北日本本数
    Private Const COLNO_HST As Integer = 6                  '入庫本数 HST
    Private Const COLNO_MAKIWAKU As Integer = 7             '巻枠コード
    Private Const COLNO_HOUSOU As Integer = 8               '包装区分
    Private Const COLNO_SIYOUSYONO As Integer = 9           '仕様書番号
    Private Const COLNO_SEISAKUKBN As Integer = 10          '製作区分
    Private Const COLNO_ZAIKO As Integer = 11               '在庫繰返
    Private Const COLNO_ZAIKOBTN As Integer = 12            '在庫繰返ボタン
    Private Const COLNO_ZAIKONM As Integer = 13             '在庫繰返名
    Private Const COLNO_CHUMONSAKI As Integer = 14          '注文先
    Private Const COLNO_JUYOUSAKI As Integer = 15           '需要先
    Private Const COLNO_JUYOUSAKIBTN As Integer = 16        '需要先ボタン
    Private Const COLNO_JUYOUSAKINAME As Integer = 17       '需要先名
    Private Const COLNO_SIZETENKAI As Integer = 18          'サイズ展開
    Private Const COLNO_SIZETENKAIBTN As Integer = 19       'サイズ展開ボタン
    Private Const COLNO_HINSYUKBN As Integer = 20           '品種区分
    Private Const COLNO_KIJUNTUKISUU As Integer = 21        '基準月数
    Private Const COLNO_SAIGAI As Integer = 22              '災害復旧在庫
    Private Const COLNO_ANNZENNZAIKO As Integer = 23        '安全在庫
    Private Const COLNO_SYUUKEIHINMEI As Integer = 24       '集計品名
    Private Const COLNO_SYUUKEIHINMEIBTN As Integer = 25    '集計品名ボタン
    Private Const COLNO_KAMOKUCD As Integer = 26            '科目コード
    Private Const COLNO_SEIZOUBUMON As Integer = 27         '製造部門
    Private Const COLNO_SEIZOUBUMONBTN As Integer = 28      '製造部門ボタン
    Private Const COLNO_SEIZOUBUMONNM As Integer = 29       '製造部門名
    Private Const COLNO_TENKAIKBN As Integer = 30           '展開区分
    Private Const COLNO_TENKAIKBNBTN As Integer = 31        '展開区分ボタン
    Private Const COLNO_TENKAIKBNNM As Integer = 32         '展開区分名
    Private Const COLNO_BUBUNKOUTEI As Integer = 33         '部分工程
    Private Const COLNO_HINSITU As Integer = 34             '品質試験
    Private Const COLNO_HINSITUBTN As Integer = 35          '品質試験ボタン
    Private Const COLNO_TATIAI As Integer = 36              '立会有無
    Private Const COLNO_TATIAIBTN As Integer = 37           '立会有無ボタン
    Private Const COLNO_CHANGEFLG As Integer = 38           '変更フラグ


    '汎用マスタ固定キー
    Private Const M01KOTEI_SEISAKUKBN As String = "03"          '製作区分
    Private Const M01KOTEI_ZAIKO As String = "10"               '在庫繰返
    Private Const M01KOTEI_JUYOUSAKI As String = "01"           '需要先
    Private Const M01KOTEI_SEIZOBMN As String = "09"            '製造部門
    Private Const M01KOTEI_TENKAI As String = "04"              '展開区分
    Private Const M01KOTEI_HINSITU As String = "08"             '品質試験区分
    Private Const M01KOTEI_TATIAI As String = "06"              '立会有無
    Private Const M01KOTEI_SIZETENKAI As String = "11"          'サイズ展開

    '汎用マスタ可変キー
    'Private Const lH09_TUSIN As String = "1"        '製造部門コード：通信
    'Private Const lH09_DENRYOKU As String = "3"     '製造部門コード：電力
    'Private Const lH03_NAISAKU As String = "1"      '製作区分：内作
    'Private Const lH03_GAICHUU As String = "2"      '製作区分：外注
    Private Const lH03_KOUNYUU As Integer = 3       '製作区分：購入
    Private Const M01KAHEN_ZAIKO_ZAIKO As Integer = 1   '在庫繰返：在庫対象
    Private Const M01KAHEN_ZAIKO_JUTYU As Integer = 2   '在庫繰返：繰返し受注
    Private Const M01KAHEN_TENKAI_BUBUN As Integer = 2  '展開区分：部分展開

    '汎用マスタ略名称
    Private Const M01NAME2_SEISAKUKBN As String = "外"
    Private Const HANYOU_NAME1 As String = "NAME1"          '名称１

    'シート列番号
    Private Const XLSNO_HINMEICD As Integer = 1             '品名コード
    Private Const XLSNO_HINMEI As Integer = 2               '品名
    Private Const XLSNO_LOTTYOU As Integer = 3              '標準ロット長
    Private Const XLSNO_TANTYOU As Integer = 4              '単長
    Private Const XLSNO_JOSU As Integer = 5                '条数
    Private Const XLSNO_KND As Integer = 6                  '入庫本数 北日本本数
    Private Const XLSNO_HST As Integer = 7                  '入庫本数 HST
    Private Const XLSNO_MAKIWAKU As Integer = 8             '巻枠コード
    Private Const XLSNO_HOUSOU As Integer = 9               '包装区分
    Private Const XLSNO_SIYOUSYONO As Integer = 10           '仕様書番号
    Private Const XLSNO_SEISAKUKBN As Integer = 11          '製作区分
    Private Const XLSNO_ZAIKO As Integer = 12               '在庫繰返
    Private Const XLSNO_CHUMONSAKI As Integer = 13          '注文先
    Private Const XLSNO_JUYOUSAKI As Integer = 14          '需要先
    Private Const XLSNO_HINSYUKBN As Integer = 15           '品種区分
    Private Const XLSNO_SIZETENKAI As Integer = 16          'サイズ展開
    Private Const XLSNO_SHINMEICD As Integer = 17           '集約対象品名コード
    Private Const XLSNO_KIJUNTUKISUU As Integer = 18        '基準月数
    Private Const XLSNO_SAIGAI As Integer = 19              '災害復旧在庫
    Private Const XLSNO_ANNZENNZAIKO As Integer = 20        '安全在庫
    Private Const XLSNO_KAMOKUCD As Integer = 21            '科目コード
    Private Const XLSNO_SEIZOUBUMON As Integer = 22         '製造部門
    Private Const XLSNO_TENKAIKBN As Integer = 23           '展開区分
    Private Const XLSNO_BUBUNKOUTEI As Integer = 24         '部分工程
    Private Const XLSNO_HINSITU As Integer = 25             '品質試験
    Private Const XLSNO_TATIAI As Integer = 26              '立会有無
    
    '変更フラグ内容
    Private Const MZ0203_UPDFLG_OFF As Integer = 5     '変更なし
    Private Const ZM120E_UPDFLG_ON As Integer = 1      '変更あり

    '材料票DB検索用
    Private Const DT1_HINSYU As String = "HINSYU"
    Private Const DT1_LINE As String = "LINE"
    Private Const DT1_COLOR As String = "COLOR"

    '変更フラグ
    Private Const HENKO_FLG As String = "1"

    'EXCEL
    Private Const START_PRINT As Integer = 9               'EXCEL出力開始行数
    '計画対商品マスタ一覧雛形シート名
    Private Const XLSSHEETNM_HINSYU As String = "Ver1.0.00"

#End Region

#Region "メンバー変数宣言"

    '-------------------------------------------------------------------------------
    '   変数定義
    '-------------------------------------------------------------------------------

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu
    Private _chkCellVO As UtilDgvChkCellVO          '一覧の入力制限用
    Private _ctlText As Control

    Dim _dgv As UtilDataGridViewHandler         'グリッドハンドラー

    Private _mz02KahenKey As String             'コード選択子画面から受け取る汎用マスタ可変キー

    Dim _copyPositionRow As Integer = -1        'コピー開始位置ボタン押下時の行番号
    Dim _copyPositionCol As Integer = -1        'コピー開始位置ボタン押下時の列番号

    Private _errSet As UtilDataGridViewHandler.dgvErrSet
    Private _nyuuryokuErrFlg As Boolean = False

    Private _oldRowIndex As Integer = -1            '選択行の背景色を変更するための変数
    Private _colorCtlFlg As Boolean = False         '選択行の背景色を変更するためのフラグ

    Private _updFlg As Boolean = False              '更新可否

    Private _formOpenFlg As Boolean = True          '画面起動時フラグ
    Private _dgvChangeFlg As Boolean = False        '一覧変更フラグ

    Private _tanmatuID As String = ""               '端末ID

    '検索条件格納変数
    Private _serchSiyoCd As String = ""             '仕様コード
    Private _serchHinsyuCd As String = ""           '品種コード
    Private _serchSensinCd As String = ""           '線心数コード
    Private _serchSizeCd As String = ""             'サイズコード
    Private _serchColorCd As String = ""            '色コード
    Private _serchSeizouBmn As String = ""          '製造部門
    Private _serchSeisakuKbn As String = ""         '製作区分
    Private _serchJuyosaki As String = ""           '需要先
    Private _serchCdSeizouBmn As String = ""        '製造部門コード
    Private _serchCdSeisakuKbn As String = ""       '製作区分コード
    Private _serchCdJuyosaki As String = ""         '需要先コード

    '-->2010.12/12 add by takagi 
    '-------------------------------------------------------------------------------
    '   オーバーライドプロパティで×ボタンだけを無効にする(ControlBoxはTrueのまま使用可能)
    '-------------------------------------------------------------------------------
    Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
        Get
            Const CS_NOCLOSE As Integer = &H200

            Dim tmpCreateParams As System.Windows.Forms.CreateParams = MyBase.CreateParams
            tmpCreateParams.ClassStyle = tmpCreateParams.ClassStyle Or CS_NOCLOSE

            Return tmpCreateParams
        End Get
    End Property
    '<--2010.12/12 add by takagi 

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
    'コンストラクタ　メニューから呼ばれる
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmForm As ZC110M_Menu, ByVal prmUpdFlg As Boolean)
        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _parentForm = prmForm                                               '親フォーム
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        _updFlg = prmUpdFlg

    End Sub
#End Region

#Region "Formイベント"

    '-------------------------------------------------------------------------------
    '　処理開始イベント
    '-------------------------------------------------------------------------------
    Private Sub ZM120E_Syuusei_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            '描画関係の設定
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           'サイズが変更されたときに、コントロールがコントロール自体を再描画するかどうかを示す値を設定
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '描画はバッファで実行され、完了後に、結果が画面に出力されるよう設定
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  'コントロールは、画面に直接ではなく、まずバッファに描画されます。これにより、ちらつきを抑えることができます。
            Me.SetStyle(ControlStyles.UserPaint, True)              'コントロールは、オペレーティング システムによってではなく、独自に描画されるよう設定
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   'コントロールはウィンドウ メッセージ WM_ERASEBKGND を無視するように設定

            Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr 'タイトルオプション表示

            '端末IDの取得
            _tanmatuID = UtilClass.getComputerName

            '画面起動時フラグ有効
            _formOpenFlg = True

            '初期値設定
            Call initForm()

            '画面起動時フラグ無効
            _formOpenFlg = False

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　フォームアンロードイベント
    '-------------------------------------------------------------------------------
    Private Sub frmMZ02_03M_TehaiKousin_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        'トランザクション有効なら解除する
        If _db.isTransactionOpen Then
            Call _db.rollbackTran()
        End If

    End Sub

#End Region

#Region "ボタンイベント"

    '-------------------------------------------------------------------------------
    '　　検索ボタン押下イベント
    '-------------------------------------------------------------------------------
    Private Sub btnKensaku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensaku.Click
        Try

            '警告メッセージ
            If _dgvChangeFlg Then
                Dim rtn As DialogResult = _msgHd.dspMSG("confirmDgvEdit")   '編集中の内容が破棄されます。よろしいですか？
                If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If

            'マウスカーソル砂時計
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor
            Try

                Dim chSeizou As UtilComboBoxHandler = New UtilComboBoxHandler(cboSeizouBmn)
                Dim chSeisaku As UtilComboBoxHandler = New UtilComboBoxHandler(cboSeisakuKbn)
                Dim chJuyo As UtilComboBoxHandler = New UtilComboBoxHandler(cboJuyosaki)
                '検索条件の保持
                _serchSiyoCd = txtSiyou.Text
                _serchHinsyuCd = txtHinsyu.Text
                _serchSensinCd = txtSensin.Text
                _serchSizeCd = txtSize.Text
                _serchColorCd = txtColor.Text
                _serchSeizouBmn = _db.rmNullStr(chSeizou.getName)
                _serchSeisakuKbn = _db.rmNullStr(chSeisaku.getName)
                _serchJuyosaki = _db.rmNullStr(chJuyo.getName)
                _serchCdSeizouBmn = _db.rmNullStr(chSeizou.getCode)
                _serchCdSeisakuKbn = _db.rmNullStr(chSeisaku.getCode)
                _serchCdJuyosaki = _db.rmNullStr(chJuyo.getCode)

                '列着色フラグ無効
                _colorCtlFlg = False

                '一覧表示
                Call dispData()

                '列着色フラグ有効
                _colorCtlFlg = True

                '更新、コピー開始位置、コピーボタン、計画対象品マスタ一覧ボタン使用可
                If "0".Equals(lblKensuu.Text.Substring(0, 1)) Then

                    Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvKousin)
                    gh.clearRow()

                    btnKousin.Enabled = False
                    btnCopyPosition.Enabled = False
                    btnCopy.Enabled = False
                    btnInsatu.Enabled = False
                    Throw New UsrDefException("対象データがありません。", _msgHd.getMSG("NonData"), cboSeizouBmn)
                Else
                    '更新、コピー開始位置、コピーボタン、計画対象品マスタ一覧ボタン使用可
                    btnKousin.Enabled = True
                    btnCopyPosition.Enabled = True
                    btnCopy.Enabled = True
                    btnInsatu.Enabled = True
                    dgvKousin.Focus()

                    dgvKousin.CurrentCell = dgvKousin(COLNO_LOTTYOU, 0)
                End If

                '一覧変更フラグ
                _dgvChangeFlg = False

            Finally
                'マウスカーソル元に戻す
                Me.Cursor = cur
            End Try

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　コピー開始位置ボタン押下
    '　（処理概要）現在のセルの行･列番号を保持し、セルに着色する。
    '-------------------------------------------------------------------------------
    Private Sub btnCopyPosition_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyPosition.Click
        Try

            '入力可能な列のみ処理を行う
            If dgvKousin.CurrentCell.ColumnIndex = COLNO_HINMEI Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_HINMEICD Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_JOSU Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_SEISAKUKBN Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_ZAIKOBTN Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_ZAIKONM Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_JUYOUSAKIBTN Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_JUYOUSAKINAME Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_SIZETENKAIBTN Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_SYUUKEIHINMEI Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_SYUUKEIHINMEIBTN Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_SEIZOUBUMON Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_SEIZOUBUMONBTN Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_SEIZOUBUMONNM Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_TENKAIKBNBTN Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_TENKAIKBNNM Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_HINSITUBTN Or _
                        dgvKousin.CurrentCell.ColumnIndex = COLNO_TATIAIBTN Then
                Exit Sub
            End If

            '既にコピー開始位置が設定されている場合は、セルの色を元に戻す
            If _copyPositionRow <> -1 Then
                dgvKousin(_copyPositionCol, _copyPositionRow).Style.BackColor = StartUp.lCOLOR_WHITE
            End If

            '現在のセルの行・列番号を保持
            _copyPositionRow = dgvKousin.CurrentCell.RowIndex
            _copyPositionCol = dgvKousin.CurrentCell.ColumnIndex

            'コピー開始位置の着色
            dgvKousin(_copyPositionCol, _copyPositionRow).Style.BackColor = StartUp.lCOLOR_PINK

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　コピーボタン押下
    '　（処理概要）コピー開始位置のセル値を現在のセルまでコピーする。
    '-------------------------------------------------------------------------------
    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Try
            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGVハンドラの設定

            'コピー開始位置と現在のセルの列が違う場合は処理を行わない。
            If _copyPositionCol <> dgvKousin.CurrentCell.ColumnIndex Then
                Exit Sub
            End If
            Try
                'コピー開始位置の値を現在のセルまで貼り付ける。
                '開始位置とコピーボタン押下時の位置のうち、行数が小さい方のセル値をコピーする。
                Dim copyFrom As Integer
                Dim copyTo As Integer
                If _copyPositionRow < dgvKousin.CurrentCell.RowIndex Then
                    copyFrom = _copyPositionRow
                    copyTo = dgvKousin.CurrentCell.RowIndex
                Else
                    copyFrom = dgvKousin.CurrentCell.RowIndex
                    copyTo = _copyPositionRow
                End If
                '貼り付ける前と後で値が変わる場合は、変更フラグを立てる。
                For i As Integer = copyFrom + 1 To copyTo
                    If Not dgvKousin(_copyPositionCol, i).Value.Equals(dgvKousin(_copyPositionCol, copyFrom).Value) Then
                        '変更フラグＯＮ
                        _dgv.setCellData(COLDT_CHANGEFLG, i, HENKO_FLG)
                    End If
                    dgvKousin(_copyPositionCol, i).Value = dgvKousin(_copyPositionCol, copyFrom).Value
                Next
            Finally
                'コピー開始位置のセルを元の色に戻し、保持していた開始位置の行・列番号をリセットする。
                dgvKousin(_copyPositionCol, _copyPositionRow).Style.BackColor = StartUp.lCOLOR_WHITE
                _copyPositionCol = -1
                _copyPositionRow = -1
            End Try
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '   更新ボタン押下
    '-------------------------------------------------------------------------------
    Private Sub btnKousin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKousin.Click
        Try
            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGVハンドラの設定

            '一覧が変更されていない場合
            If _dgvChangeFlg = False Then
                Throw New UsrDefException("一覧が更新中されていません。", _msgHd.getMSG("noUpdDataGridView"))
            End If

            '入力チェック
            Call chkBeforeUpdate()

            '入力内容チェック
            Call chkInputValue()

            '登録確認ダイアログ表示
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmInsert")   '登録します。よろしいですか？
            If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            'マウスポインタ状態の保存
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor
            Try

                'DB更新
                Call updateDB()

            Finally
                'マウスポインタ状態の復元
                Me.Cursor = cur
            End Try

            '完了メッセージ
            Call _msgHd.dspMSG("completeInsert")          '登録が完了しました。
            '画面初期設定(コントロールの初期化)
            'Call initForm()

            '一覧変更フラグ
            _dgvChangeFlg = False

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　計画対象品マスタ一覧ボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnInsatu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsatu.Click
        Try

            '2度押し防止のため使用不可とする
            btnInsatu.Enabled = False

            'マウスポインタ状態の保存
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor
            Try

                'EXCEL出力
                Call printExcel()

            Finally
                'マウスポインタ状態の復元
                Me.Cursor = cur
            End Try

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        Finally
            'エクセル出力ボタン使用可
            btnInsatu.Enabled = True
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　戻るボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnModoru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModoru.Click

        '警告メッセージ
        If _dgvChangeFlg Then
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmDgvEdit")   '編集中の内容が破棄されます。よろしいですか？
            If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        'トランザクション有効なら解除する
        If _db.isTransactionOpen Then
            Call _db.rollbackTran()
        End If

        '■親フォーム表示
        _parentForm.Show()
        _parentForm.Activate()
        Me.Close()

    End Sub

#End Region

#Region "ユーザ定義関数:EXCEL関連"

    '------------------------------------------------------------------------------------------------------
    '　計画対象品マスタ一覧出力
    '------------------------------------------------------------------------------------------------------
    Private Sub printExcel()
        Try
            Dim pb As UtilProgressBar = New UtilProgressBar(Me)
            Try
                pb.Show()

                'プログレスバー設定
                pb.jobName = "出力を準備しています。"
                pb.status = "初期化中．．．"

                '雛形ファイル(品名別販売計画と同じ雛形)
                Dim openFilePath As String = StartUp.iniValue.BaseXlsPath & "\" & StartUp.iniValue.ExcelZM120R1_Base
                '雛形ファイルが開かれていないかチェック
                Dim fh As UtilFile = New UtilFile()
                Try
                    fh.move(openFilePath, openFilePath & 1)
                    fh.move(openFilePath & 1, openFilePath)
                Catch ioe As System.IO.IOException
                    Throw New UsrDefException("ファイルが開かれています。ファイルを閉じてから再実行してください。", _
                                              _msgHd.getMSG("fileOpenErr", "【ファイル名】：" & openFilePath))
                End Try

                '出力用ファイル
                'ファイル名取得-----------------------------------------------------
                Dim wkEditFile As String = StartUp.iniValue.OutXlsPath & "\" & StartUp.iniValue.ExcelZM120R1_Out     'コピー先ファイル

                'コピー先ファイルが存在する場合、コピー先ファイルを削除----------------
                If UtilClass.isFileExists(wkEditFile) Then
                    Try
                        fh.delete(wkEditFile)
                    Catch ioe As System.IO.IOException
                        Throw New UsrDefException("ファイルが開かれています。ファイルを閉じてから再実行してください。", _
                                                  _msgHd.getMSG("fileOpenErr", "【ファイル名】：" & wkEditFile))
                    End Try
                End If

                Try
                    '出力用ファイルへ雛型ファイルコピー
                    FileCopy(openFilePath, wkEditFile)
                Catch ioe As System.IO.IOException
                    Throw New UsrDefException(ioe, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ioe)))
                End Try

                Dim eh As xls.UtilExcelHandler = New xls.UtilExcelHandler(wkEditFile)
                Try
                    eh.open()
                    Try
                        '汎用マスタから製造部門情報を取得
                        Dim sql As String = ""
                        sql = sql & N & " SELECT KAHENKEY, NAME1 FROM M01HANYO "
                        sql = sql & N & " WHERE KOTEIKEY = '" & M01KOTEI_SEIZOBMN & "'"
                        If Not "".Equals(_serchSeizouBmn) Then
                            sql = sql & N & " AND KAHENKEY = '" & _db.rmSQ(_serchCdSeizouBmn) & "'"
                        End If
                        sql = sql & N & " ORDER BY KAHENKEY "
                        'SQL発行
                        Dim iRecCntSeizo As Integer          'データセットの行数
                        Dim dsHanyoSeizo As DataSet = _db.selectDB(sql, RS, iRecCntSeizo)

                        If iRecCntSeizo <= 0 Then                    'M01汎用マスタ抽出レコードが１件もない場合
                            Throw New UsrDefException("対象データがありません。", _msgHd.getMSG("NonData"))
                        End If

                        '汎用マスタから製作区分情報を取得
                        sql = ""
                        sql = sql & N & " SELECT KAHENKEY, NAME1 FROM M01HANYO "
                        sql = sql & N & " WHERE KOTEIKEY = '" & M01KOTEI_SEISAKUKBN & "'"
                        If Not "".Equals(_serchSeisakuKbn) Then
                            sql = sql & N & " AND KAHENKEY = '" & _db.rmSQ(_serchCdSeisakuKbn) & "'"
                        End If
                        sql = sql & N & " ORDER BY KAHENKEY "
                        'SQL発行
                        Dim iRecCntSeisaku As Integer          'データセットの行数
                        Dim dsHanyoSeisaku As DataSet = _db.selectDB(sql, RS, iRecCntSeisaku)

                        If iRecCntSeizo <= 0 Then                    'M01汎用マスタ抽出レコードが１件もない場合
                            Throw New UsrDefException("対象データがありません。", _msgHd.getMSG("NonData"))
                        End If

                        For i As Integer = 0 To iRecCntSeizo - 1

                            For j As Integer = 0 To iRecCntSeisaku - 1

                                'M11の値をデータセットに保持
                                Dim dsM11 As DataSet = Nothing
                                Dim rowCntM11 As Integer = 0
                                '製造部門、製作区分ごとにM11のデータを抽出
                                Call getM11DataForXls(_db.rmNullStr(dsHanyoSeizo.Tables(RS).Rows(i)("KAHENKEY")), _
                                                _db.rmNullStr(dsHanyoSeisaku.Tables(RS).Rows(j)("KAHENKEY")), dsM11, rowCntM11)

                                'M12の値をデータセットに保持
                                Dim dsM12 As DataSet = Nothing
                                Dim rowCntM12 As Integer = 0
                                '製造部門、製作区分ごとにM12のデータを抽出
                                Call getM12DataForXls(_db.rmNullStr(dsHanyoSeizo.Tables(RS).Rows(i)("KAHENKEY")), _
                                                _db.rmNullStr(dsHanyoSeisaku.Tables(RS).Rows(j)("KAHENKEY")), dsM12, rowCntM12)

                                If rowCntM11 > 0 Then

                                    'シート(雛形)を複製保存
                                    Dim baseName As String = XLSSHEETNM_HINSYU  '雛形シート名
                                    Dim newName As String = _db.rmNullStr(dsHanyoSeizo.Tables(RS).Rows(i)(HANYOU_NAME1)) & "・" & _
                                                _db.rmNullStr(dsHanyoSeisaku.Tables(RS).Rows(j)(HANYOU_NAME1))  '新たに作成するシート
                                    Try
                                        eh.targetSheet = baseName               '雛形シート選択
                                        eh.copySheetOnLast(newName)
                                    Catch ex As Exception
                                        Throw New UsrDefException("シートの複製に失敗しました。", _msgHd.getMSG("failCopySheet"))
                                    End Try

                                    'プログレスバー設定
                                    pb.jobName = newName & "出力中．．．"
                                    pb.status = ""

                                    eh.targetSheet = newName

                                    '作成日時編集
                                    Dim printDate As String = Now.ToString("yyyy/MM/dd HH:mm")
                                    eh.setValue("作成日時 ： " & printDate, 1, 20)   'T1

                                    '需要品・品名編集
                                    Dim kensakuStr As String = "需要先："
                                    '需要先取得
                                    If Not "".Equals(cboJuyosaki.Text) Then
                                        kensakuStr = kensakuStr & cboJuyosaki.Text
                                    End If
                                    '品名取得
                                    kensakuStr = kensakuStr & "   品名コード：" & createHinmeiCd()
                                    eh.setValue(kensakuStr, 5, 1)      'A5

                                    '製造部門編集
                                    eh.setValue(_db.rmNullStr(dsHanyoSeizo.Tables(RS).Rows(i)("NAME1")), 4, 3)    'C4

                                    '製作区分編集
                                    eh.setValue(_db.rmNullStr(dsHanyoSeisaku.Tables(RS).Rows(j)("NAME1")), 4, 6)    'F4
                                    
                                    Dim startPrintRow As Integer = START_PRINT          '出力開始行数

                                    Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder
                                    pb.maxVal = rowCntM11


                                    Dim k As Integer = 0        'ループカウンター
                                    Dim m As Integer = 0        'M12のレコード数カウンター
                                    Dim xlsRow As Integer = 0
                                    For k = 0 To rowCntM11 - 1

                                        pb.status = (k + 1) & "/" & rowCntM11 & "件"
                                        pb.oneStep = 10
                                        pb.value = k + 1

                                        xlsRow = startPrintRow + k

                                        '行を1行追加
                                        eh.copyRow(xlsRow)
                                        eh.insertPasteRow(xlsRow)

                                        '一覧データ出力
                                        With dsM11.Tables(RS)
                                            Dim sHinmeiCD As String = ""        '出力する計画品名コードを保持する変数
                                            For n As Integer = m To rowCntM12 - 1
                                                '実品名コードが等しい場合
                                                If _db.rmNullStr(_db.rmNullStr(dsM11.Tables(RS).Rows(k)(COLDT_HINMEICD)).Equals _
                                                                (_db.rmNullStr(dsM12.Tables(RS2).Rows(n)(COLDT_M12KHINMEICD)))) Then
                                                    If "".Equals(sHinmeiCD) Then
                                                        sHinmeiCD = _db.rmNullStr(dsM12.Tables(RS2).Rows(n)(COLDT_M12HINMEICD))
                                                    Else
                                                        sHinmeiCD = sHinmeiCD & "," & _db.rmNullStr(dsM12.Tables(RS2).Rows(n)(COLDT_M12HINMEICD))
                                                    End If
                                                    m = n + 1
                                                Else
                                                    Exit For
                                                End If

                                            Next

                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_HINMEICD)) & ControlChars.Tab)       '品名コード
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_HINMEI)) & ControlChars.Tab)         '品名
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_LOTTYOU)) & ControlChars.Tab)        'ロット長
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_TANTYOU)) & ControlChars.Tab)        '単長
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_JOSU)) & ControlChars.Tab)           '条数
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_KND)) & ControlChars.Tab)            '北日本分
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_HST)) & ControlChars.Tab)            '住電日立分
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_MAKIWAKU)) & ControlChars.Tab)       '巻枠コード
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_HOUSOU)) & ControlChars.Tab)         '包装区分
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_SIYOUSYONO)) & ControlChars.Tab)     '仕様書番号
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_SEISAKUKUBUN)) & ControlChars.Tab)   '製作区分
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_ZAIKO)) & ControlChars.Tab)          '在庫繰返
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_CHUMONSAKI)) & ControlChars.Tab)     '注文先
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_JUYOUSAKI)) & ControlChars.Tab)      '需要先
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_HINSYUKBN)) & ControlChars.Tab)      '品種区分
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_SIZETENKAI)) & ControlChars.Tab)     'サイズ展開
                                            sb.Append(sHinmeiCD & ControlChars.Tab)                                     '集計品名数
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_KIJUNTUKISU)) & ControlChars.Tab)    '基準月数
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_SAIGAI)) & ControlChars.Tab)         '災害復旧在庫
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_ANZENZAIKO)) & ControlChars.Tab)     '安全在庫
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_KAMOKUCD)) & ControlChars.Tab)       '科目コード
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_SEIZOUBUMON)) & ControlChars.Tab)    '製造部門
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_TENKAIKBN)) & ControlChars.Tab)      '展開区分
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_BUBUNKOUTEI)) & ControlChars.Tab)    '部分展開
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_HINSITU)) & ControlChars.Tab)        '品質試験
                                            sb.Append(_db.rmNullStr(.Rows(k)(COLDT_TATIAI)) & ControlChars.Tab)         '立会有無

                                            sb.Append(ControlChars.CrLf)
                                        End With
                                    Next

                                    Clipboard.SetText(sb.ToString)
                                    eh.paste(startPrintRow, XLSNO_HINMEICD) '一括貼り付け

                                    eh.deleteRow(xlsRow + 1)
                                End If

                            Next
                        Next

                        eh.deleteSheet(XLSSHEETNM_HINSYU)
                    Finally
                        eh.close()
                    End Try

                    'EXCELファイル開く
                    eh.display()

                Catch ue As UsrDefException
                    ue.dspMsg()
                    Throw ue
                Catch ex As Exception
                    'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                    Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)), , StartUp.iniValue.LogFilePath)
                Finally
                    eh.endUse()
                    eh = Nothing
                End Try

            Finally
                '画面消去
                pb.Close()
            End Try
        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '   エクセル出力用データ抽出
    '　（処理概要）エクセル出力用のデータをM11から抽出する。
    '   ●入力パラメタ  ：prmSeizo          汎用マスタ製造部門の値
    '   ●入力パラメタ  ：prmSeisaku        汎用マスタ製作区分の値
    '   ●出力パラメタ  ：prmDs             抽出結果のデータセット
    '   ●出力パラメタ  ：prmRecCnt         抽出結果件数
    '-------------------------------------------------------------------------------
    Private Sub getM11DataForXls(ByVal prmSeizo As String, ByVal prmSeisaku As String, ByRef prmDs As DataSet, ByRef prmRecCnt As Integer)
        Try

            'EXCEL用のデータ取得
            Dim SQL As String = ""
            SQL = "SELECT "
            SQL = SQL & N & " (RPAD(TT_H_SIYOU_CD, 2) "
            SQL = SQL & N & "  || TT_H_HIN_CD "
            SQL = SQL & N & "  || TT_H_SENSIN_CD "
            SQL = SQL & N & "  || TT_H_SIZE_CD "
            SQL = SQL & N & "  || TT_H_COLOR_CD)   " & COLDT_HINMEICD       '品名コード
            SQL = SQL & N & " ,TT_HINMEI           " & COLDT_HINMEI         '品名
            SQL = SQL & N & " ,TT_LOT              " & COLDT_LOTTYOU        '標準ロット長
            SQL = SQL & N & " ,TT_TANCYO           " & COLDT_TANTYOU        '製作単長
            SQL = SQL & N & " ,TT_JYOSU            " & COLDT_JOSU           '入庫本数 全体
            SQL = SQL & N & " ,TT_N_K_SUU          " & COLDT_KND            '入庫本数 北日本本数
            SQL = SQL & N & " ,TT_N_SH_SUU         " & COLDT_HST            '入庫本数 住電日立本数
            SQL = SQL & N & " ,TT_MAKI_CD          " & COLDT_MAKIWAKU       '巻枠コード
            SQL = SQL & N & " ,TT_HOSO_KBN         " & COLDT_HOUSOU         '包装区分
            SQL = SQL & N & " ,TT_SIYOUSYO_NO      " & COLDT_SIYOUSYONO     '仕様書№
            SQL = SQL & N & " ,M02.NAME1           " & COLDT_SEISAKUKUBUN   '製作区分
            SQL = SQL & N & " ,M03.NAME3           " & COLDT_ZAIKO          '在庫繰返
            SQL = SQL & N & " ,TT_KYAKSAKI         " & COLDT_CHUMONSAKI     '注文先名
            SQL = SQL & N & " ,M04.NAME2           " & COLDT_JUYOUSAKI      '需要先
            SQL = SQL & N & " ,TT_TENKAIPTN        " & COLDT_SIZETENKAI     'サイズ展開
            SQL = SQL & N & " ,TT_HINSYUKBN        " & COLDT_HINSYUKBN      '品種区分
            SQL = SQL & N & " ,TT_KZAIKOTUKISU     " & COLDT_KIJUNTUKISU    '基準月数
            SQL = SQL & N & " ,TT_SFUKKYUU         " & COLDT_SAIGAI         '災害復旧
            SQL = SQL & N & " ,TT_ANNZENZAIKO      " & COLDT_ANZENZAIKO     '安全在庫
            SQL = SQL & N & " ,TT_KAMOKU_CD        " & COLDT_KAMOKUCD       '科目コード
            SQL = SQL & N & " ,M05.NAME1           " & COLDT_SEIZOUBUMON    '製造部門名
            SQL = SQL & N & " ,M06.NAME2           " & COLDT_TENKAIKBN      '展開区分
            SQL = SQL & N & " ,TT_KOUTEI           " & COLDT_BUBUNKOUTEI    '部分工程
            SQL = SQL & N & " ,M07.NAME1    　     " & COLDT_HINSITU        '品質試験
            SQL = SQL & N & " ,M08.NAME1           " & COLDT_TATIAI         '立会有無
            SQL = SQL & N & " FROM M11KEIKAKUHIN "
            SQL = SQL & N & "   LEFT JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '" & M01KOTEI_SEISAKUKBN & "') M02 "
            SQL = SQL & N & "   ON TT_SEISAKU_KBN =  M02.KAHENKEY "
            SQL = SQL & N & "   LEFT JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '" & M01KOTEI_ZAIKO & "') M03 "
            SQL = SQL & N & "   ON TT_SYUBETU =  M03.KAHENKEY "
            SQL = SQL & N & "   LEFT JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '" & M01KOTEI_JUYOUSAKI & "') M04 "
            SQL = SQL & N & "   ON TT_JUYOUCD =  M04.KAHENKEY "
            SQL = SQL & N & "   LEFT JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '" & M01KOTEI_SEIZOBMN & "') M05 "
            SQL = SQL & N & "   ON TT_SEIZOU_BMN =  M05.KAHENKEY "
            SQL = SQL & N & "   LEFT JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '" & M01KOTEI_TENKAI & "') M06 "
            SQL = SQL & N & "   ON TT_TENKAI_KBN =  M06.KAHENKEY "
            SQL = SQL & N & "   LEFT JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '" & M01KOTEI_HINSITU & "') M07 "
            SQL = SQL & N & "   ON TT_HINSITU_KBN =  M07.KAHENKEY "
            SQL = SQL & N & "   LEFT JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '" & M01KOTEI_TATIAI & "') M08 "
            SQL = SQL & N & "   ON TT_TATIAI_UM =  M08.KAHENKEY "

            '製造部門
            SQL = SQL & N & "   WHERE "
            SQL = SQL & "   M05.KAHENKEY = '" & _db.rmNullStr(prmSeizo) & "'"

            '製作区分
            SQL = SQL & N & "   AND "
            SQL = SQL & "   M02.KAHENKEY = '" & _db.rmNullStr(prmSeisaku) & "'"

            '需要先
            If Not "".Equals(_serchJuyosaki) Then
                SQL = SQL & N & "   AND "
                SQL = SQL & "   M04.KAHENKEY = '" & _db.rmNullStr(_serchCdJuyosaki) & "'"
            End If

            '仕様コード
            If Not "".Equals(_serchSiyoCd) Then
                SQL = SQL & N & "   AND "

                'SQL = SQL & "   TT_H_SIYOU_CD LIKE '" & _db.rmSQ(Trim(_serchSiyoCd)) & "%'"
                SQL = SQL & "   TT_H_SIYOU_CD LIKE '" & _db.rmSQ(Trim(_serchSiyoCd.PadRight(2, " "))) & "%'"

            End If

            '品種コード
            If Not "".Equals(_serchHinsyuCd) Then
                SQL = SQL & N & "   AND "
                SQL = SQL & "   TT_H_HIN_CD LIKE '" & _db.rmSQ(Trim(_serchHinsyuCd)) & "%'"
            End If

            '線心数コード
            If Not "".Equals(_serchSensinCd) Then
                SQL = SQL & N & "   AND "
                SQL = SQL & "   TT_H_SENSIN_CD LIKE '" & _db.rmSQ(Trim(_serchSensinCd)) & "%'"
            End If

            'サイズコード
            If Not "".Equals(_serchSizeCd) Then
                SQL = SQL & N & "   AND "
                SQL = SQL & "   TT_H_SIZE_CD LIKE '" & _db.rmSQ(Trim(_serchSizeCd)) & "%'"
            End If

            '色コード
            If Not "".Equals(_serchColorCd) Then
                SQL = SQL & N & "   AND "
                SQL = SQL & "   TT_H_COLOR_CD LIKE '" & _db.rmSQ(Trim(_serchColorCd)) & "%'"
            End If

            '' 2010/12/17 upd start sugano
            'SQL = SQL & "   ORDER BY TT_SEIZOU_BMN, TT_SEISAKU_KBN, TT_JUYOUCD, "
            SQL = SQL & "   ORDER BY TT_SEIZOU_BMN, TT_SEISAKU_KBN, "
            '' 2010/12/17 upd end sugano

            SQL = SQL & "   TT_H_HIN_CD, TT_H_SENSIN_CD, TT_H_SIZE_CD, TT_H_SIYOU_CD, TT_H_COLOR_CD "

            'SQL発行
            prmDs = _db.selectDB(SQL, RS, prmRecCnt)

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '   エクセル出力用データ抽出
    '　（処理概要）エクセル出力用のデータをM12から抽出する。
    '   ●入力パラメタ  ：prmSeizo          汎用マスタ製造部門の値
    '   ●入力パラメタ  ：prmSeisaku        汎用マスタ製作区分の値
    '   ●出力パラメタ  ：prmDs             抽出結果のデータセット
    '   ●出力パラメタ  ：prmRecCnt         抽出結果件数
    '-------------------------------------------------------------------------------
    Private Sub getM12DataForXls(ByVal prmSeizo As String, ByVal prmSeisaku As String, ByRef prmDs As DataSet, ByRef prmRecCnt As Integer)
        Try

            'EXCEL用のデータ取得
            Dim SQL As String = ""
            SQL = "SELECT "
            SQL = SQL & N & "  M12.HINMEICD " & COLDT_M12HINMEICD       '実品名コード
            SQL = SQL & N & "  ,M12.KHINMEICD " & COLDT_M12KHINMEICD    '計画品名コード
            SQL = SQL & N & " FROM  M12SYUYAKU M12 "
            SQL = SQL & N & "   LEFT JOIN  M11KEIKAKUHIN M11 "
            SQL = SQL & N & "   ON M11.TT_KHINMEICD = M12.KHINMEICD "
            SQL = SQL & N & "   LEFT JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '" & M01KOTEI_SEIZOBMN & "') M05 "
            SQL = SQL & N & "   ON TT_SEIZOU_BMN =  M05.KAHENKEY "
            SQL = SQL & N & "   LEFT JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '" & M01KOTEI_SEISAKUKBN & "') M02 "
            SQL = SQL & N & "   ON TT_SEISAKU_KBN =  M02.KAHENKEY "
            SQL = SQL & N & "   LEFT JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '" & M01KOTEI_JUYOUSAKI & "') M04 "
            SQL = SQL & N & "   ON TT_JUYOUCD =  M04.KAHENKEY "
            SQL = SQL & N & "   WHERE "
            SQL = SQL & N & "   NOT M12.KHINMEICD = M12.HINMEICD "
            '製造部門
            SQL = SQL & N & "   AND "
            SQL = SQL & "   M05.KAHENKEY = '" & _db.rmNullStr(prmSeizo) & "'"

            '製作区分
            SQL = SQL & N & "   AND "
            SQL = SQL & "   M02.KAHENKEY = '" & _db.rmNullStr(prmSeisaku) & "'"

            '需要先
            If Not "".Equals(_serchJuyosaki) Then
                SQL = SQL & N & "   AND "
                SQL = SQL & "   M04.NAME1 = '" & _db.rmNullStr(_serchCdJuyosaki) & "'"
            End If

            '仕様コード
            If Not "".Equals(_serchSiyoCd) Then
                SQL = SQL & N & "   AND "

                'SQL = SQL & "   TT_H_SIYOU_CD LIKE '" & _db.rmSQ(Trim(_serchSiyoCd)) & "%'"
                SQL = SQL & "   TT_H_SIYOU_CD LIKE '" & _db.rmSQ(Trim(_serchSiyoCd.PadRight(2, " "))) & "%'"

            End If

            '品種コード
            If Not "".Equals(_serchHinsyuCd) Then
                SQL = SQL & N & "   AND "
                SQL = SQL & "   TT_H_HIN_CD LIKE '" & _db.rmSQ(Trim(_serchHinsyuCd)) & "%'"
            End If

            '線心数コード
            If Not "".Equals(_serchSensinCd) Then
                SQL = SQL & N & "   AND "
                SQL = SQL & "   TT_H_SENSIN_CD LIKE '" & _db.rmSQ(Trim(_serchSensinCd)) & "%'"
            End If

            'サイズコード
            If Not "".Equals(_serchSizeCd) Then
                SQL = SQL & N & "   AND "
                SQL = SQL & "   TT_H_SIZE_CD LIKE '" & _db.rmSQ(Trim(_serchSizeCd)) & "%'"
            End If

            '色コード
            If Not "".Equals(_serchColorCd) Then
                SQL = SQL & N & "   AND "
                SQL = SQL & "   TT_H_COLOR_CD LIKE '" & _db.rmSQ(Trim(_serchColorCd)) & "%'"
            End If

            SQL = SQL & "   ORDER BY TT_SEIZOU_BMN, TT_SEISAKU_KBN, TT_JUYOUCD, "
            SQL = SQL & "   TT_H_HIN_CD, TT_H_SENSIN_CD, TT_H_SIZE_CD, TT_H_SIYOU_CD, TT_H_COLOR_CD "

            'SQL発行
            prmDs = _db.selectDB(SQL, RS2, prmRecCnt)


        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　品名コード作成
    '　(処理概要)EXCELに出力する品名コードを編集して返す。
    '　　I　：　なし
    '　　R　：　createHinmeiCd      '編集した品名コード
    '-------------------------------------------------------------------------------
    Private Function createHinmeiCd() As String
        Try
            createHinmeiCd = ""

            '仕様コード
            If _serchSiyoCd.Length = 2 Then
                createHinmeiCd = _serchSiyoCd & "-"
            ElseIf _serchSiyoCd.Length = 1 Then
                createHinmeiCd = _serchSiyoCd.Substring(0, 1) & "*-"
            Else
                createHinmeiCd = "**-"
            End If

            '品種コード
            If _serchHinsyuCd.Length = 3 Then
                createHinmeiCd = createHinmeiCd & _serchHinsyuCd & "-"
            ElseIf _serchHinsyuCd.Length = 2 Then
                createHinmeiCd = createHinmeiCd & _serchHinsyuCd.Substring(0, 2) & "*-"
            ElseIf _serchHinsyuCd.Length = 1 Then
                createHinmeiCd = createHinmeiCd & _serchHinsyuCd.Substring(0, 1) & "**-"
            Else
                createHinmeiCd = createHinmeiCd & "***-"
            End If

            '線心数コード
            If _serchSensinCd.Length = 3 Then
                createHinmeiCd = createHinmeiCd & _serchSensinCd & "-"
            ElseIf _serchSensinCd.Length = 2 Then
                createHinmeiCd = createHinmeiCd & _serchSensinCd.Substring(0, 2) & "*-"
            ElseIf _serchSensinCd.Length = 1 Then
                createHinmeiCd = createHinmeiCd & _serchSensinCd.Substring(0, 1) & "**-"
            Else
                createHinmeiCd = createHinmeiCd & "***-"
            End If

            'サイズコード
            If _serchSizeCd.Length = 2 Then
                createHinmeiCd = createHinmeiCd & _serchSizeCd & "-"
            ElseIf _serchSizeCd.Length = 1 Then
                createHinmeiCd = createHinmeiCd & _serchSizeCd.Substring(0, 1) & "*-"
            Else
                createHinmeiCd = createHinmeiCd & "**-"
            End If

            '色コード
            If _serchColorCd.Length = 3 Then
                createHinmeiCd = createHinmeiCd & _serchColorCd
            ElseIf _serchColorCd.Length = 2 Then
                createHinmeiCd = createHinmeiCd & _serchColorCd.Substring(0, 2) & "*"
            ElseIf _serchColorCd.Length = 1 Then
                createHinmeiCd = createHinmeiCd & _serchColorCd.Substring(0, 1) & "**"
            Else
                createHinmeiCd = createHinmeiCd & "***"
            End If

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Function
#End Region

#Region "ユーザ定義関数:画面制御"

    '-------------------------------------------------------------------------------
    '   画面初期設定
    '-------------------------------------------------------------------------------
    Private Sub initForm()
        Try

            'ボタン制御
            btnKousin.Enabled = False           '更新ボタン
            btnCopyPosition.Enabled = False     'コピー開始位置ボタン
            btnCopy.Enabled = False             'コピーボタン
            btnInsatu.Enabled = False           '計画対象品マスタ一覧ボタン
            txtSiyou.Text = ""                  '仕様コード
            txtHinsyu.Text = ""                 '品種コード
            txtSensin.Text = ""                 '線心数コード
            txtSize.Text = ""                   'サイズコード
            txtColor.Text = ""                  '色コード

            'コンボボックスセット
            Call setCboJuyosaki()               '需要先
            Call setCboSeisakuKbn()             '製作区分
            Call setCboSeizoBmn()               '製造部門

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    ' 　フォーカス移動
    '　（処理概要）検索項目のテキストボックスでエンターキー押下時は次のコントロールへ移動する。
    '-------------------------------------------------------------------------------
    Private Sub txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSiyou.KeyPress, _
                                                                                                    txtHinsyu.KeyPress, _
                                                                                                    txtSensin.KeyPress, _
                                                                                                    txtSize.KeyPress, _
                                                                                                    txtColor.KeyPress, _
                                                                                                    cboJuyosaki.KeyPress, _
                                                                                                    cboSeisakuKbn.KeyPress, _
                                                                                                    cboSeizouBmn.KeyPress

        UtilClass.moveNextFocus(Me, e)  '次のコントロールへ移動する

    End Sub

    '-------------------------------------------------------------------------------
    '　コントロール全選択
    '　(処理概要)コントロール移動時に全選択状態にする
    '-------------------------------------------------------------------------------
    Private Sub serch_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSiyou.GotFocus, _
                                                                                            txtHinsyu.GotFocus, _
                                                                                            txtSensin.GotFocus, _
                                                                                            txtSize.GotFocus, _
                                                                                            txtColor.GotFocus, _
                                                                                            cboJuyosaki.GotFocus, _
                                                                                            cboSeisakuKbn.GotFocus, _
                                                                                            cboSeizouBmn.GotFocus
        UtilClass.selAll(sender)

    End Sub

    '-------------------------------------------------------------------------------
    ' 　汎用マスタ可変キーの受け取り
    '-------------------------------------------------------------------------------
    Public Sub setKahenKey(ByVal prmKahenKey As String, ByVal prmMeisyo1 As String) Implements IfRturnKahenKey.setKahenKey
        Try

            _mz02KahenKey = prmKahenKey

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   myShowメソッド
    '-------------------------------------------------------------------------------
    Public Sub myShow() Implements IfRturnKahenKey.myShow
        Me.Show()
    End Sub

    '-------------------------------------------------------------------------------
    '   myActivateメソッド
    '-------------------------------------------------------------------------------
    Public Sub myActivate() Implements IfRturnKahenKey.myActivate
        Me.Activate()
    End Sub

#End Region

#Region "ユーザ定義関数:DGV関連"

    '-------------------------------------------------------------------------------
    '   一覧のセル値変更時
    '   （処理概要）変更があった行の変更フラグを立てる
    '-------------------------------------------------------------------------------
    Private Sub dgvKousin_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvKousin.CellValueChanged
        Try

            '画面起動時は処理を行わない
            If _formOpenFlg Then
                Exit Sub
            End If

            _dgv = New UtilDataGridViewHandler(dgvKousin)
            
            '変更フラグＯＮ
            _dgv.setCellData(COLDT_CHANGEFLG, e.RowIndex, ZM120E_UPDFLG_ON)

            '一覧変更フラグ
            _dgvChangeFlg = True

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '   一覧　編集チェック（EditingControlShowingイベント）
    '   （処理概要）入力の制限をかける
    '-------------------------------------------------------------------------------
    Private Sub dgvkousin_EditingControlShowing(ByVal sender As Object, _
                                              ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs _
                                              ) Handles dgvKousin.EditingControlShowing

        Try
            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGVハンドラの設定

            If dgvKousin.CurrentCell.ColumnIndex = COLNO_LOTTYOU Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_TANTYOU Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_KND Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_HST Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_MAKIWAKU Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_ZAIKO Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_JUYOUSAKI Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_SIZETENKAI Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_KIJUNTUKISUU Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_SAIGAI Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_ANNZENNZAIKO Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_KAMOKUCD Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_SEIZOUBUMON Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_TENKAIKBN Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_HINSITU Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_TATIAI Then

                '■グリッドに、数値入力モードの制限をかける
                _chkCellVO = _dgv.chkCell(sender, e, UtilDataGridViewHandler.chkType.Num)

            End If

            Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '   選択セル検証イベント（DataErrorイベント）
    '   （処理概要）数値入力欄に数値以外が入力された場合のエラー処理
    '-------------------------------------------------------------------------------
    Private Sub dgvKousin_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvKousin.DataError
        Try
            e.Cancel = False                                   '編集モード終了

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvKousin)
            'グリッドには数値入力モード(0～9)の制限をかけているので、制限の解除
            If dgvKousin.CurrentCell.ColumnIndex = COLNO_LOTTYOU Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_TANTYOU Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_KND Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_HST Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_MAKIWAKU Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_ZAIKO Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_JUYOUSAKI Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_SIZETENKAI Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_KIJUNTUKISUU Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_SAIGAI Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_ANNZENNZAIKO Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_KAMOKUCD Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_SEIZOUBUMON Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_TENKAIKBN Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_HINSITU Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_TATIAI Then
                gh.AfterchkCell(_chkCellVO)
            End If

            Dim colName As String = ""
            Select Case e.ColumnIndex
                Case COLNO_LOTTYOU
                    colName = COLDT_LOTTYOU
                Case COLNO_TANTYOU
                    colName = COLDT_TANTYOU
                Case COLNO_KND
                    colName = COLDT_KND
                Case COLNO_HST
                    colName = COLDT_HST
                Case COLNO_MAKIWAKU
                    colName = COLDT_MAKIWAKU
                Case COLNO_ZAIKO
                    colName = COLDT_ZAIKO
                Case COLNO_JUYOUSAKI
                    colName = COLDT_JUYOUSAKI
                Case COLNO_SIZETENKAI
                    colName = COLDT_SIZETENKAI
                Case COLNO_KIJUNTUKISUU
                    colName = COLDT_KIJUNTUKISU
                Case COLNO_SAIGAI
                    colName = COLDT_SAIGAI
                Case COLNO_ANNZENNZAIKO
                    colName = COLDT_ANZENZAIKO
                Case COLNO_KAMOKUCD
                    colName = COLDT_KAMOKUCD
                Case COLNO_SEIZOUBUMON
                    colName = COLDT_SEIZOUBUMON
                Case COLNO_TENKAIKBN
                    colName = COLDT_TENKAIKBN
                Case COLNO_HINSITU
                    colName = COLDT_HINSITU
                Case COLNO_TATIAI
                    colName = COLDT_TATIAI
                Case Else
                    Exit Sub
            End Select

            '入力エラーフラグを立てる
            _nyuuryokuErrFlg = True

            'エラーセルにフォーカスをあてる
            Call _dgv.setCellData(colName, e.RowIndex, DBNull.Value)

            'エラーメッセージ表示
            Throw New UsrDefException("半角数字のみ入力可能です。", _msgHd.getMSG("onlyAcceptNumeric"))

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '   セル編集後処理
    '-------------------------------------------------------------------------------
    Private Sub dgvKousin_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvKousin.CellEndEdit

        If _nyuuryokuErrFlg Then
            dgvKousin.CancelEdit()
            Exit Sub
        End If

        Try
            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGVハンドラの設定

            '数値入力モード(0～9)の制限がかかっている場合は、制限の解除
            If dgvKousin.CurrentCell.ColumnIndex = COLNO_LOTTYOU Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_TANTYOU Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_KND Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_HST Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_MAKIWAKU Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_ZAIKO Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_JUYOUSAKI Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_SIZETENKAI Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_KIJUNTUKISUU Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_SAIGAI Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_ANNZENNZAIKO Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_KAMOKUCD Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_SEIZOUBUMON Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_TENKAIKBN Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_HINSITU Or _
                                dgvKousin.CurrentCell.ColumnIndex = COLNO_TATIAI Then
                _dgv.AfterchkCell(_chkCellVO)
            End If
            'ヘッダ行の場合抜ける
            If e.RowIndex <= MZ0203_ROW_HEADER Then Exit Sub

            '条数の自動計算
            If dgvKousin.CurrentCell.ColumnIndex = COLNO_LOTTYOU Or dgvKousin.CurrentCell.ColumnIndex = COLNO_TANTYOU Then
                Call calcJosu(e)
            End If

            '本数の自動計算
            If dgvKousin.CurrentCell.ColumnIndex = COLNO_KND Or dgvKousin.CurrentCell.ColumnIndex = COLNO_HST Then
                Call calcHonsu(e)
            End If

            '巻枠コードのチェック
            If dgvKousin.CurrentCell.ColumnIndex = COLNO_MAKIWAKU Then
                If checkMakiwaku(dgvKousin.CurrentCellAddress.Y) = False Then
                    Throw New UsrDefException("巻枠名がマスタに登録されていません。", _
                                    _msgHd.getMSG("noExistMakiwaku", "【巻枠コード】"), dgvKousin, COLCN_MAKIWAKU, dgvKousin.CurrentCellAddress.Y)
                End If
            End If

            '包装表示区分のチェック
            If dgvKousin.CurrentCell.ColumnIndex = COLNO_HOUSOU Then
                If checkHousou(dgvKousin.CurrentCellAddress.Y) = False Then
                    Throw New UsrDefException("包装／表示種類がマスタに登録されていません。", _
                            _msgHd.getMSG("noExistHousou", "【包装表示区分】"), dgvKousin, COLCN_HOUSOU, dgvKousin.CurrentCellAddress.Y)
                End If
            End If

            '在庫繰返変更時
            If dgvKousin.CurrentCell.ColumnIndex = COLNO_ZAIKO Then
                If checkZaiko(dgvKousin.CurrentCellAddress.Y) = False Then
                    Throw New UsrDefException("汎用マスタに登録されていない値です。", _
                                _msgHd.getMSG("noExistHanyouMst", "【在庫繰返】"), dgvKousin, COLCN_ZAIKO, dgvKousin.CurrentCellAddress.Y)
                End If
            End If

            '在庫繰返 = 「1」のときのみ以下のチェックを行う。
            If _db.rmNullInt(dgvKousin(COLNO_ZAIKO, dgvKousin.CurrentCell.RowIndex).Value) = M01KAHEN_ZAIKO_ZAIKO Then

                '需要先入力時
                If dgvKousin.CurrentCell.ColumnIndex = COLNO_JUYOUSAKI Then
                    If checkJuyo(dgvKousin.CurrentCellAddress.Y) = False Then
                        Throw New UsrDefException("汎用マスタに登録されていない値です。", _
                                _msgHd.getMSG("noExistHanyouMst", "【需要先】"), dgvKousin, COLCN_JUYOUSAKI, dgvKousin.CurrentCellAddress.Y)
                    End If
                End If

                'サイズ展開入力時
                If dgvKousin.CurrentCell.ColumnIndex = COLNO_SIZETENKAI Then
                    If checkSizeTenkai(dgvKousin.CurrentCellAddress.Y) = False Then
                        Throw New UsrDefException("汎用マスタに登録されていない値です。", _
                                _msgHd.getMSG("noExistHanyouMst", "【サイズ展開】"), dgvKousin, COLCN_SIZETENKAI, dgvKousin.CurrentCellAddress.Y)
                    End If
                End If

                '品種区分入力時
                If dgvKousin.CurrentCell.ColumnIndex = COLNO_HINSYUKBN Then
                    If checkHinsyuKbn(dgvKousin.CurrentCellAddress.Y) = False Then
                        Throw New UsrDefException("品種区分がマスタに登録されていません。", _
                                        _msgHd.getMSG("noHinsyuKbn", "【品種区分】"), dgvKousin, COLCN_HINSYUKBN, dgvKousin.CurrentCellAddress.Y)
                    End If
                End If
            End If

            '製造部門入力時
            If dgvKousin.CurrentCell.ColumnIndex = COLNO_SEIZOUBUMON Then
                If checkSeizouBmn(dgvKousin.CurrentCellAddress.Y) = False Then
                    Throw New UsrDefException("汎用マスタに登録されていない値です。", _msgHd.getMSG("noExistHanyouMst", "【製造部門】"))
                End If
            End If

            '展開区分入力時
            If dgvKousin.CurrentCell.ColumnIndex = COLNO_TENKAIKBN Then
                If checkTenkaiKbn(dgvKousin.CurrentCellAddress.Y) = 1 Then
                    Throw New UsrDefException("汎用マスタに登録されていない値です。", _
                            _msgHd.getMSG("noExistHanyouMst", "【展開区分】"), dgvKousin, COLCN_TENKAIKBN, dgvKousin.CurrentCellAddress.Y)
                ElseIf checkTenkaiKbn(dgvKousin.CurrentCellAddress.Y) = 2 Then
                    Throw New UsrDefException("製作区分「外注」時は展開区分「部分展開」以外選択できません。", _
                            _msgHd.getMSG("nonGaicyuSelect"), dgvKousin, COLCN_TENKAIKBN, dgvKousin.CurrentCellAddress.Y)
                End If
            End If

            '品質試験区分入力時
            If dgvKousin.CurrentCell.ColumnIndex = COLNO_HINSITU Then
                If checkHinsitu(dgvKousin.CurrentCellAddress.Y) = False Then
                    Throw New UsrDefException("汎用マスタに登録されていない値です。", _
                                _msgHd.getMSG("noExistHanyouMst", "【品質試験部門】"), dgvKousin, COLCN_HINSITU, dgvKousin.CurrentCellAddress.Y)
                End If
            End If

            '立会有無入力時
            If dgvKousin.CurrentCell.ColumnIndex = COLNO_TATIAI Then
                If checkTatiai(dgvKousin.CurrentCellAddress.Y) = False Then
                    Throw New UsrDefException("汎用マスタに登録されていない値です。", _
                                _msgHd.getMSG("noExistHanyouMst", "【立会有無】"), dgvKousin, COLCN_TATIAI, dgvKousin.CurrentCellAddress.Y)
                End If
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    ' 　グリッドフォーカス設定
    '　（処理概要）セル編集後にエラーになった場合に、エラーセルにフォーカスを戻す。
    '-------------------------------------------------------------------------------
    Private Sub dgvKousin_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvKousin.SelectionChanged
        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvKousin)

            '入力エラーがあった場合
            If _nyuuryokuErrFlg Then
                _nyuuryokuErrFlg = False
                'フォーカスを入力エラーセルに移す
                gh.setCurrentCell(_errSet)
                Else
                End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　　選択行に着色する処理
    '　　(処理概要）選択行に着色する。
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvSeisanMst_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvKousin.CellEnter
        Try

            If _colorCtlFlg Then
                Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvKousin)
                '背景色の設定
                Call setBackcolor(dgvKousin.CurrentCellAddress.Y, _oldRowIndex)

            End If
            _oldRowIndex = dgvKousin.CurrentCellAddress.Y

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　背景色の設定処理
    '　(処理概要)行の背景列を青にし、ボタンの列を元に戻す。
    '　　I　：　prmRowIndex     現在フォーカスがある行数
    '　　I　：　prmOldRowIndex  現在の行に移る前の行数
    '------------------------------------------------------------------------------------------------------
    Private Sub setBackcolor(ByVal prmRowIndex As Integer, ByVal prmOldRowIndex As Integer)
        Try
            Dim _dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvKousin)

            '指定した行の背景色を青にする
            _dgv.setSelectionRowColor(prmRowIndex, prmOldRowIndex, StartUp.lCOLOR_BLUE)

            'ボタン列の色も変わってしまうので、戻す処理
            Call colBtnColorSilver(prmRowIndex)

            _oldRowIndex = prmRowIndex

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    ' 　ボタン着色
    '　（処理概要）ブルーに着色されたボタンを元に戻す
    '-------------------------------------------------------------------------------
    Private Sub colBtnColorSilver(ByVal prmNewRowIdx As Integer)

        dgvKousin(COLNO_ZAIKOBTN, prmNewRowIdx).Style.BackColor = SystemColors.Control
        dgvKousin(COLNO_JUYOUSAKIBTN, prmNewRowIdx).Style.BackColor = SystemColors.Control
        dgvKousin(COLNO_SIZETENKAIBTN, prmNewRowIdx).Style.BackColor = SystemColors.Control
        dgvKousin(COLNO_SYUUKEIHINMEIBTN, prmNewRowIdx).Style.BackColor = SystemColors.Control
        dgvKousin(COLNO_TENKAIKBNBTN, prmNewRowIdx).Style.BackColor = SystemColors.Control
        dgvKousin(COLNO_HINSITUBTN, prmNewRowIdx).Style.BackColor = SystemColors.Control
        dgvKousin(COLNO_TATIAIBTN, prmNewRowIdx).Style.BackColor = SystemColors.Control
        dgvKousin(COLNO_SEIZOUBUMONBTN, prmNewRowIdx).Style.BackColor = SystemColors.Control

    End Sub

    '-------------------------------------------------------------------------------
    '   シートコマンドボタン押下
    '-------------------------------------------------------------------------------
    Private Sub dgvKousin_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvKousin.CellContentClick
        Try
            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGVハンドラの設定

            Dim nowRow As Integer = e.RowIndex   '現在の行数

            If Not e.ColumnIndex.Equals(COLNO_SYUUKEIHINMEIBTN) Then
                'コード選択子画面
                Dim koteiKey As String = ""         'コード選択子画面に渡す固定キー
                Dim kahenKey As String = ""         'コード選択子画面に渡す可変キー
                Dim colName As String = ""          '列名
                Dim selectCol As String = "NAME1"   'コード選択子画面に表示する汎用マスタの列名

                Select Case e.ColumnIndex
                    Case COLNO_ZAIKOBTN            '在庫繰返選択ボタン
                        koteiKey = M01KOTEI_ZAIKO
                        kahenKey = _db.rmNullStr(dgvKousin(COLNO_ZAIKO, e.RowIndex).Value)
                        colName = COLDT_ZAIKO
                    Case COLNO_JUYOUSAKIBTN             '需要先コード選択ボタン
                        koteiKey = M01KOTEI_JUYOUSAKI
                        kahenKey = _db.rmNullStr(dgvKousin(COLNO_JUYOUSAKI, e.RowIndex).Value)
                        colName = COLDT_JUYOUSAKI
                        selectCol = "NAME2"
                    Case COLNO_SIZETENKAIBTN           'サイズ展開コード選択ボタン
                        koteiKey = M01KOTEI_SIZETENKAI
                        kahenKey = _db.rmNullStr(dgvKousin(COLNO_SIZETENKAI, e.RowIndex).Value)
                        colName = COLDT_SIZETENKAI
                    Case COLNO_SEIZOUBUMONBTN           '製造部門コード選択ボタン
                        koteiKey = M01KOTEI_SEIZOBMN
                        kahenKey = _db.rmNullStr(dgvKousin(COLNO_SEIZOUBUMON, e.RowIndex).Value)
                        colName = COLDT_SEIZOUBUMON
                    Case COLNO_TENKAIKBNBTN             '展開区分コード選択ボタン
                        koteiKey = M01KOTEI_TENKAI
                        kahenKey = _db.rmNullStr(dgvKousin(COLNO_TENKAIKBN, e.RowIndex).Value)
                        colName = COLDT_TENKAIKBN
                    Case COLNO_HINSITUBTN          '品質試験区分選択ボタン
                        koteiKey = M01KOTEI_HINSITU
                        kahenKey = _db.rmNullStr(dgvKousin(COLNO_HINSITU, e.RowIndex).Value)
                        colName = COLDT_HINSITU
                    Case COLNO_TATIAIBTN                '立会有無選択ボタン
                        koteiKey = M01KOTEI_TATIAI
                        kahenKey = _db.rmNullStr(dgvKousin(COLNO_TATIAI, e.RowIndex).Value)
                        colName = COLDT_TATIAI
                    Case Else
                        Exit Sub
                End Select

                'コード選択画面表示
                Dim openForm As ZC910S_CodeSentaku = New ZC910S_CodeSentaku(_msgHd, _db, Me, koteiKey, kahenKey, selectCol, )      'パラメタを遷移先画面へ渡す
                openForm.ShowDialog(Me)                                                             '画面表示
                openForm.Dispose()

                If Not "".Equals(_mz02KahenKey) Then
                    'コード選択画面で選択した区分を表示
                    _dgv.setCellData(colName, e.RowIndex, _mz02KahenKey)
                    '変更フラグＯＮ
                    _dgv.setCellData(COLDT_CHANGEFLG, e.RowIndex, ZM120E_UPDFLG_ON)
                    _dgvChangeFlg = True
                    'コードに対応するラベルの再表示
                    If colName.Equals(COLDT_JUYOUSAKI) Then         '需要先
                        Call checkJuyo(e.RowIndex)
                    ElseIf colName.Equals(COLDT_SEIZOUBUMON) Then   '製造部門
                        Call checkSeizouBmn(e.RowIndex)
                    ElseIf colName.Equals(COLDT_TENKAIKBN) Then     '展開区分
                        Call checkTenkaiKbn(e.RowIndex)
                    End If
                End If

                '在庫繰返が「1」以外の場合
                If e.ColumnIndex.Equals(COLNO_ZAIKOBTN) Then
                    Call checkZaiko(e.RowIndex)
                End If
            Else
                '集計対象品名登録画面
                'コード選択画面表示
                Dim openForm As ZM121S_SyuukeiTouroku = New ZM121S_SyuukeiTouroku(_msgHd, _db, Me, _dgv.getCellData(COLDT_HINMEICD, e.RowIndex).ToString)      'パラメタを遷移先画面へ渡す
                openForm.ShowDialog(Me)                                                             '画面表示
                openForm.Dispose()

                '集計品名数の再表示
                Call reDispSyukeiHinmei(e.RowIndex)

                dgvKousin.CurrentCell = dgvKousin(COLNO_SYUUKEIHINMEIBTN, nowRow)
                _dgv.setSelectionRowColor(nowRow, _oldRowIndex, StartUp.lCOLOR_BLUE)

                'ボタン列の色をシルバーに戻す
                Call colBtnColorSilver(nowRow)

                _oldRowIndex = nowRow

            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   条数自動計算
    '　（処理概要）標準ロット長・単長の両方が入力されている場合、条数を自動計算する。
    '   ●入力パラメタ  ：prmE      DGVイベント
    '-------------------------------------------------------------------------------
    Private Sub calcJosu(ByVal prmE As System.Windows.Forms.DataGridViewCellEventArgs)
        Try
            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGVハンドラの設定

            Dim lottyo As String = _dgv.getCellData(COLDT_LOTTYOU, prmE.RowIndex)
            Dim tantyo As String = _dgv.getCellData(COLDT_TANTYOU, prmE.RowIndex)
            Dim josu As String = ""

            If Not "".Equals(lottyo) And Not "".Equals(tantyo) Then
                '標準ロット長・製作単長入力値チェック(標準ロット長÷製作単長=整数)
                If CInt(lottyo) Mod CInt(tantyo) <> 0 Then
                    _dgv.setCellData(COLDT_JOSU, prmE.RowIndex, 0)
                    Exit Sub
                Else
                    josu = CStr(CInt(lottyo) / CInt(tantyo))
                    _dgv.setCellData(COLDT_JOSU, prmE.RowIndex, josu)
                    '条数変更に伴う本数の変更
                    If Not "".Equals(_dgv.getCellData(COLDT_KND, prmE.RowIndex)) Then
                        _dgv.setCellData(COLDT_HST, prmE.RowIndex, CStr(josu - CInt(_dgv.getCellData(COLDT_KND, prmE.RowIndex))))
                    ElseIf Not "".Equals(_dgv.getCellData(COLDT_HST, prmE.RowIndex)) Then
                        _dgv.setCellData(COLDT_KND, prmE.RowIndex, CStr(josu - CInt(_dgv.getCellData(COLDT_HST, prmE.RowIndex))))
                    End If

                End If
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '   本数自動計算
    '　（処理概要）条数と北日本分または住電日立分が入力されている場合、本数を自動計算する。
    '   ●入力パラメタ  ：prmE      DGVイベント
    '-------------------------------------------------------------------------------
    Private Sub calcHonsu(ByVal prmE As System.Windows.Forms.DataGridViewCellEventArgs)
        Try

            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGVハンドラの設定

            Dim josu As String = _dgv.getCellData(COLDT_JOSU, prmE.RowIndex)

            If "".Equals(josu) Then Exit Sub

            Dim sumi As String = _dgv.getCellData(COLDT_HST, prmE.RowIndex)
            Dim knd As String = _dgv.getCellData(COLDT_KND, prmE.RowIndex)

            If prmE.ColumnIndex = COLNO_KND And Not "".Equals(knd) Then
                _dgv.setCellData(COLDT_HST, prmE.RowIndex, CStr(CInt(josu) - CInt(knd)))
            ElseIf prmE.ColumnIndex = COLNO_HST And Not "".Equals(sumi) Then
                _dgv.setCellData(COLDT_KND, prmE.RowIndex, CStr(CInt(josu) - CInt(sumi)))
            Else
                Exit Sub
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '   巻枠コード存在チェック
    '　（処理概要）入力された巻枠が巻枠コードテーブルに存在しているかチェックする。
    '   ●入力パラメタ  ：prmRowIndex       DGV列番号
    '-------------------------------------------------------------------------------
    Private Function checkMakiwaku(ByVal prmRowCnt As Integer) As Boolean
        Try
            checkMakiwaku = True

            '空のときはチェックしない
            If DBNull.Value.Equals(dgvKousin(COLNO_MAKIWAKU, prmRowCnt).Value) Then
                Exit Function
            End If

            '-->2010.12.25 add by takagi #48
            '999999のときもチェックしない
            Dim tmpVal As String = CStr(dgvKousin(COLNO_MAKIWAKU, prmRowCnt).Value)
            If "999999".Equals(tmpVal) Then
                Exit Function
            End If
            '<--2010.12.25 add by takagi #48

            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGVハンドラの設定

            '巻枠コード存在チェック
            If Not serchMakiwaku(CStr(dgvKousin(COLNO_MAKIWAKU, prmRowCnt).Value)) Then
                checkMakiwaku = False
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Function

    '-------------------------------------------------------------------------------
    '   包装表示区分コード存在チェック
    '　（処理概要）入力された包装表示区分が包装テーブルに存在しているかチェックする。
    '   ●入力パラメタ  ：prmRowIndex       DGV列番号
    '-------------------------------------------------------------------------------
    Private Function checkHousou(ByVal prmRowCnt As Integer) As Boolean
        Try
            checkHousou = True

            '空のときはチェックしない
            If "".Equals(_db.rmNullStr(dgvKousin(COLNO_HOUSOU, prmRowCnt).Value)) Then
                'checkHousou = False
                Exit Function
            End If

            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGVハンドラの設定

            '包装表示区分存在チェック
            If Not serchHousou(CStr(dgvKousin(COLNO_HOUSOU, prmRowCnt).Value)) Then
                checkHousou = False
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Function

    '-------------------------------------------------------------------------------
    '   在庫繰返入力時処理
    '　（処理概要）①在庫繰返名が汎用マスタにあるかチェックする。
    '　　　　　　　②在庫繰返が「1」以外の場合は、以下の項目をクリアする。
    '                   1.需要先
    '                   2.需要先名
    '                   3.サイズ展開
    '                   4.品種区分
    '                   5.基準月数
    '                   6.災害復旧用在庫量
    '                   7.安全在庫量
    '                   8.集計品名数
    '   ●入力パラメタ  ：prmRowIndex       DGV列番号
    '-------------------------------------------------------------------------------
    Private Function checkZaiko(ByVal prmRowCnt As Integer) As Boolean
        Try

            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGVハンドラの設定

            '在庫繰返存在チェック
            Dim name As String = backNameFromM01(M01KOTEI_ZAIKO, _db.rmNullStr(dgvKousin(COLNO_ZAIKO, prmRowCnt).Value))
            If "".Equals(name) Then

                Return False

            Else

                '戻り値を在庫繰返名に表示
                _dgv.setCellData(COLDT_ZAIKONM, prmRowCnt, name)

                '在庫繰返が「1」以外の場合は上記コントロールクリア
                If Not CStr(M01KAHEN_ZAIKO_ZAIKO).Equals(_db.rmNullStr(dgvKousin(COLNO_ZAIKO, prmRowCnt).Value)) Then

                    _dgv.setCellData(COLDT_JUYOUSAKI, prmRowCnt, DBNull.Value)          '需要先
                    _dgv.setCellData(COLDT_JUYOUSAKINAME, prmRowCnt, DBNull.Value)      '需要先名
                    _dgv.setCellData(COLDT_SIZETENKAI, prmRowCnt, DBNull.Value)         'サイズ展開
                    _dgv.setCellData(COLDT_HINSYUKBN, prmRowCnt, DBNull.Value)          '品種区分
                    _dgv.setCellData(COLDT_KIJUNTUKISU, prmRowCnt, DBNull.Value)        '基準月数
                    _dgv.setCellData(COLDT_SAIGAI, prmRowCnt, DBNull.Value)             '災害復旧用在庫数
                    _dgv.setCellData(COLDT_ANZENZAIKO, prmRowCnt, DBNull.Value)         '安全在庫数
                    _dgv.setCellData(COLDT_SHINMEI, prmRowCnt, DBNull.Value)            '集計品名数

                    '変更フラグＯＮ
                    _dgv.setCellData(COLDT_CHANGEFLG, prmRowCnt, ZM120E_UPDFLG_ON)

                    '一覧変更フラグ
                    _dgvChangeFlg = True

                End If
                Return True
            End If
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Function

    '-------------------------------------------------------------------------------
    '   需要先入力時処理
    '　（処理概要）①需要先名が汎用マスタにあるかチェックする。
    '   ●入力パラメタ  ：prmRowIndex       DGV列番号
    '-------------------------------------------------------------------------------
    Private Function checkJuyo(ByVal prmRowCnt As Integer) As Boolean
        Try
            checkJuyo = True
            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGVハンドラの設定

            '需要先存在チェック
            Dim name As String = backNameFromM01(M01KOTEI_JUYOUSAKI, _db.rmNullStr(dgvKousin(COLNO_JUYOUSAKI, prmRowCnt).Value))
            If "".Equals(name) Then

                checkJuyo = False

            Else

                '戻り値を需要先名に表示
                _dgv.setCellData(COLDT_JUYOUSAKINAME, prmRowCnt, name)

                '品種区分をクリア
                '_dgv.setCellData(COLDT_HINSYUKBN, prmRowCnt, DBNull.Value)

            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Function

    '-------------------------------------------------------------------------------
    '   サイズ展開入力時処理
    '　（処理概要）サイズ展開が汎用マスタにあるかチェックする。
    '   ●入力パラメタ  ：prmRowIndex       DGV列番号
    '-------------------------------------------------------------------------------
    Private Function checkSizeTenkai(ByVal prmRowCnt As Integer) As Boolean
        Try
            checkSizeTenkai = True
            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGVハンドラの設定

            'サイズ展開存在チェック
            Dim name As String = backNameFromM01(M01KOTEI_SIZETENKAI, _db.rmNullStr(dgvKousin(COLNO_SIZETENKAI, prmRowCnt).Value))
            If "".Equals(Name) Then

                checkSizeTenkai = False

            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Function

    '-------------------------------------------------------------------------------
    '   品種区分入力時処理
    '　（処理概要）品種区分と需要先の組み合わせが正しいかチェックする。
    '   ●入力パラメタ  ：prmRowIndex       DGV列番号
    '-------------------------------------------------------------------------------
    Private Function checkHinsyuKbn(ByVal prmRowIndex As Integer) As Boolean

        Try
            checkHinsyuKbn = True

            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGVハンドラの設定

            '品種区分チェック
            If Not hinsyuKbnExist(_db.rmNullStr(dgvKousin(COLNO_HINSYUKBN, prmRowIndex).Value), _db.rmNullStr(dgvKousin(COLNO_JUYOUSAKI, prmRowIndex).Value)) Then
                checkHinsyuKbn = False
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Function

    '-------------------------------------------------------------------------------
    '   製造部門入力時処理
    '　（処理概要）製造部門名が汎用マスタにあるかチェックする。
    '   ●入力パラメタ  ：prmRowIndex       DGV列番号
    '-------------------------------------------------------------------------------
    Private Function checkSeizouBmn(ByVal prmRowIndex As Integer) As Boolean
        Try
            checkSeizouBmn = True
            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGVハンドラの設定

            '製造部門存在チェック
            Dim name As String = backNameFromM01(M01KOTEI_SEIZOBMN, _db.rmNullStr(dgvKousin(COLNO_SEIZOUBUMON, prmRowIndex).Value))
            If "".Equals(name) Then

                checkSeizouBmn = False

            Else

                '戻り値を製造部門名に表示
                _dgv.setCellData(COLDT_SEIZOUBUMONNM, prmRowIndex, name)

            End If
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Function

    '-------------------------------------------------------------------------------
    '   展開区分入力時処理
    '　（処理概要）展開区分が汎用マスタにあるかチェックする。
    '   ●入力パラメタ  ：prmRowIndex       DGV列番号
    '-------------------------------------------------------------------------------
    Private Function checkTenkaiKbn(ByVal prmRowIndex As Integer) As Integer
        Try
            checkTenkaiKbn = 0
            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGVハンドラの設定

            '未入力なら処理を行わない
            If "".Equals(_db.rmNullStr(dgvKousin(COLNO_TENKAIKBN, prmRowIndex).Value)) Then Exit Function

            '展開区分存在チェック
            Dim name As String = backNameFromM01(M01KOTEI_TENKAI, _db.rmNullStr(dgvKousin(COLNO_TENKAIKBN, prmRowIndex).Value))
            If "".Equals(name) Then

                checkTenkaiKbn = 1
                
            ElseIf M01NAME2_SEISAKUKBN.Equals(_db.rmNullStr(dgvKousin(COLNO_SEISAKUKBN, prmRowIndex).Value)) And _
                        Not "2".Equals(_db.rmNullStr(dgvKousin(COLNO_TENKAIKBN, prmRowIndex).Value)) Then

                checkTenkaiKbn = 2

            Else

                checkTenkaiKbn = 0
                '戻り値を製造部門名に表示
                _dgv.setCellData(COLDT_TENKAIKBNNM, prmRowIndex, name)

            End If
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Function

    '-------------------------------------------------------------------------------
    '   品質試験区分入力時処理
    '　（処理概要）品質試験区分が汎用マスタにあるかチェックする。
    '   ●入力パラメタ  ：prmRowIndex       DGV列番号
    '-------------------------------------------------------------------------------
    Private Function checkHinsitu(ByVal prmRowIndex As Integer) As Boolean
        Try
            checkHinsitu = True
            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGVハンドラの設定

            '品質試験区分存在チェック
            Dim name As String = backNameFromM01(M01KOTEI_HINSITU, _db.rmNullStr(dgvKousin(COLNO_HINSITU, prmRowIndex).Value))
            If "".Equals(name) Then

                checkHinsitu = False
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Function

    '-------------------------------------------------------------------------------
    '   立会有無入力時処理
    '　（処理概要）立会有無が汎用マスタにあるかチェックする。
    '   ●入力パラメタ  ：prmRowIndex       DGV列番号
    '-------------------------------------------------------------------------------
    Private Function checkTatiai(ByVal prmRowIndex As Integer) As Boolean
        Try
            checkTatiai = True
            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGVハンドラの設定

            '立会有無存在チェック
            Dim name As String = backNameFromM01(M01KOTEI_TATIAI, _db.rmNullStr(dgvKousin(COLNO_TATIAI, prmRowIndex).Value))
            If "".Equals(name) Then

                checkTatiai = False
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Function

#End Region

#Region "ユーザ定義関数:DB関連"

    '-------------------------------------------------------------------------------
    '   一覧表示
    '   (処理概要)入力された検索条件をもとに、一覧にデータを表示する。
    '-------------------------------------------------------------------------------
    Private Sub dispData()
        Try

            Dim SQL As String = ""
            SQL = "SELECT "
            SQL = SQL & N & " (MAX(RPAD(TT_H_SIYOU_CD,2)) "
            SQL = SQL & N & "  || MAX(TT_H_HIN_CD) "
            SQL = SQL & N & "  || MAX(TT_H_SENSIN_CD) "
            SQL = SQL & N & "  || MAX(TT_H_SIZE_CD) "
            SQL = SQL & N & "  || MAX(TT_H_COLOR_CD) )  " & COLDT_HINMEICD          '品名コード
            SQL = SQL & N & " ,MAX(TT_HINMEI)           " & COLDT_HINMEI            '品名
            SQL = SQL & N & " ,MAX(TT_LOT)              " & COLDT_LOTTYOU           '標準ロット長
            SQL = SQL & N & " ,MAX(TT_TANCYO)           " & COLDT_TANTYOU    '製作単長
            SQL = SQL & N & " ,MAX(TT_JYOSU)            " & COLDT_JOSU              '入庫本数 全体
            SQL = SQL & N & " ,MAX(TT_N_K_SUU)          " & COLDT_KND           '入庫本数 北日本本数
            SQL = SQL & N & " ,MAX(TT_N_SH_SUU)         " & COLDT_HST         '入庫本数 住電日立本数
            SQL = SQL & N & " ,MAX(TT_MAKI_CD)          " & COLDT_MAKIWAKU          '巻枠コード
            SQL = SQL & N & " ,MAX(TT_HOSO_KBN)         " & COLDT_HOUSOU            '包装区分
            SQL = SQL & N & " ,MAX(TT_SIYOUSYO_NO)      " & COLDT_SIYOUSYONO        '仕様書№
            SQL = SQL & N & " ,MAX(M02.NAME2)           " & COLDT_SEISAKUKUBUN      '製作区分
            SQL = SQL & N & " ,MAX(TT_SYUBETU)          " & COLDT_ZAIKO        '在庫繰返
            SQL = SQL & N & " ,''                       " & COLDT_ZAIKOBTN     '在庫繰返ボタン
            SQL = SQL & N & " ,MAX(M03.NAME2)           " & COLDT_ZAIKONM      '在庫繰返名
            SQL = SQL & N & " ,MAX(TT_KYAKSAKI)         " & COLDT_CHUMONSAKI        '注文先名
            SQL = SQL & N & " ,MAX(TT_JUYOUCD)          " & COLDT_JUYOUSAKI         '需要先
            SQL = SQL & N & " ,''                       " & COLDT_JUYOUSAKIBTN      '需要先ボタン
            SQL = SQL & N & " ,MAX(M04.NAME2)           " & COLDT_JUYOUSAKINAME     '需要先名
            SQL = SQL & N & " ,MAX(TT_TENKAIPTN)        " & COLDT_SIZETENKAI        'サイズ展開
            SQL = SQL & N & " ,''                       " & COLDT_SIZETENKAIBTN     'サイズ展開ボタン
            SQL = SQL & N & " ,MAX(TT_HINSYUKBN)        " & COLDT_HINSYUKBN         '品種区分
            SQL = SQL & N & " ,MAX(TT_KZAIKOTUKISU)     " & COLDT_KIJUNTUKISU       '基準月数
            SQL = SQL & N & " ,MAX(TT_SFUKKYUU)         " & COLDT_SAIGAI            '災害復旧
            SQL = SQL & N & " ,MAX(TT_ANNZENZAIKO)      " & COLDT_ANZENZAIKO        '安全在庫
            SQL = SQL & N & " ,COUNT(M12.HINMEICD) - 1  " & COLDT_SHINMEI           '集計品名数
            SQL = SQL & N & " ,''                       " & COLDT_SHINMEIBTN        '集計品名ボタン
            SQL = SQL & N & " ,MAX(TT_KAMOKU_CD)        " & COLDT_KAMOKUCD          '科目コード
            SQL = SQL & N & " ,MAX(TT_SEIZOU_BMN)       " & COLDT_SEIZOUBUMON       '製造部門コード
            SQL = SQL & N & " ,''                       " & COLDT_SEIZOUBUMONBTN    '製造部門コードボタン
            SQL = SQL & N & " ,MAX(M05.NAME2)           " & COLDT_SEIZOUBUMONNM     '製造部門名
            SQL = SQL & N & " ,MAX(TT_TENKAI_KBN)       " & COLDT_TENKAIKBN         '展開区分
            SQL = SQL & N & " ,''                       " & COLDT_TENKAIKBNBTN      '展開区分ボタン
            SQL = SQL & N & " ,MAX(M06.NAME2)           " & COLDT_TENKAIKBNNM       '展開区分名
            SQL = SQL & N & " ,MAX(TT_KOUTEI)           " & COLDT_BUBUNKOUTEI       '部分工程
            SQL = SQL & N & " ,MAX(TT_HINSITU_KBN)      " & COLDT_HINSITU           '品質試験
            SQL = SQL & N & " ,''                       " & COLDT_HINSITUBTN        '品質試験ボタン
            SQL = SQL & N & " ,MAX(TT_TATIAI_UM)        " & COLDT_TATIAI            '立会有無
            SQL = SQL & N & " ,''                       " & COLDT_TATIAIBTN         '立会有無ボタン
            SQL = SQL & N & " ,'0'                      " & COLDT_CHANGEFLG         '変更フラグ
            SQL = SQL & N & " FROM M11KEIKAKUHIN "
            SQL = SQL & N & "   LEFT JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '" & M01KOTEI_SEISAKUKBN & "') M02 "
            SQL = SQL & N & "   ON TT_SEISAKU_KBN =  M02.KAHENKEY "
            SQL = SQL & N & "   LEFT JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '" & M01KOTEI_ZAIKO & "') M03 "
            SQL = SQL & N & "   ON TT_SYUBETU =  M03.KAHENKEY "
            SQL = SQL & N & "   LEFT JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '" & M01KOTEI_JUYOUSAKI & "') M04 "
            SQL = SQL & N & "   ON TT_JUYOUCD =  M04.KAHENKEY "
            SQL = SQL & N & "   LEFT JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '" & M01KOTEI_SEIZOBMN & "') M05 "
            SQL = SQL & N & "   ON TT_SEIZOU_BMN =  M05.KAHENKEY "
            SQL = SQL & N & "   LEFT JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '" & M01KOTEI_TENKAI & "') M06 "
            SQL = SQL & N & "   ON TT_TENKAI_KBN =  M06.KAHENKEY "
            SQL = SQL & N & "   LEFT JOIN M12SYUYAKU M12 "
            SQL = SQL & N & "   ON TT_KHINMEICD = M12.KHINMEICD "

            Dim sqlWhere As Boolean = False             '「AND」を付けるフラグ
            '製造部門
            If Not "".Equals(_serchSeizouBmn) Then
                SQL = SQL & N & "   WHERE "
                SQL = SQL & "   M05.KAHENKEY = '" & _db.rmSQ(_serchCdSeizouBmn) & "'"
                sqlWhere = True
            End If
            '製作区分
            If Not "".Equals(_serchSeisakuKbn) Then
                If sqlWhere Then
                    SQL = SQL & N & "   AND "
                Else
                    SQL = SQL & N & "   WHERE "
                End If
                SQL = SQL & "   M02.KAHENKEY = '" & _db.rmSQ(_serchCdSeisakuKbn) & "'"
                sqlWhere = True
            End If
            '需要先
            If Not "".Equals(_serchJuyosaki) Then
                If sqlWhere Then
                    SQL = SQL & N & "   AND "
                Else
                    SQL = SQL & N & "   WHERE "
                End If
                SQL = SQL & "   M04.KAHENKEY = '" & _db.rmSQ(_serchCdJuyosaki) & "'"
                sqlWhere = True
            End If
            '仕様コード
            If Not "".Equals(txtSiyou.Text) Then
                If sqlWhere Then
                    SQL = SQL & N & "   AND "
                Else
                    SQL = SQL & N & "   WHERE "
                End If

                'SQL = SQL & "   TT_H_SIYOU_CD LIKE '" & _db.rmSQ(Trim(txtSiyou.Text)) & "%'"
                SQL = SQL & "   TT_H_SIYOU_CD LIKE '" & _db.rmSQ(Trim(txtSiyou.Text.PadRight(2, " "))) & "%'"

                sqlWhere = True
            End If
            '品種コード
            If Not "".Equals(txtHinsyu.Text) Then
                If sqlWhere Then
                    SQL = SQL & N & "   AND "
                Else
                    SQL = SQL & N & "   WHERE "
                End If
                SQL = SQL & "   TT_H_HIN_CD LIKE '" & _db.rmSQ(Trim(txtHinsyu.Text)) & "%'"
                sqlWhere = True
            End If
            '線心数コード
            If Not "".Equals(txtSensin.Text) Then
                If sqlWhere Then
                    SQL = SQL & N & "   AND "
                Else
                    SQL = SQL & N & "   WHERE "
                End If
                SQL = SQL & "   TT_H_SENSIN_CD LIKE '" & _db.rmSQ(Trim(txtSensin.Text)) & "%'"
                sqlWhere = True
            End If
            'サイズコード
            If Not "".Equals(txtSize.Text) Then
                If sqlWhere Then
                    SQL = SQL & N & "   AND "
                Else
                    SQL = SQL & N & "   WHERE "
                End If
                SQL = SQL & "   TT_H_SIZE_CD LIKE '" & _db.rmSQ(Trim(txtSize.Text)) & "%'"
                sqlWhere = True
            End If
            '色コード
            If Not "".Equals(txtColor.Text) Then
                If sqlWhere Then
                    SQL = SQL & N & "   AND "
                Else
                    SQL = SQL & N & "   WHERE "
                End If
                SQL = SQL & "   TT_H_COLOR_CD LIKE '" & _db.rmSQ(Trim(txtColor.Text)) & "%'"
            End If

            SQL = SQL & N & "   GROUP BY TT_KHINMEICD "

            '' 2010/12/17 upd start sugano
            'SQL = SQL & "   ORDER BY MAX(TT_SEIZOU_BMN), MAX(TT_SEISAKU_KBN), MAX(TT_JUYOUCD), "
            SQL = SQL & "   ORDER BY "
            '' 2010/12/17 upd end sugano
            SQL = SQL & "   MAX(TT_H_HIN_CD), MAX(TT_H_SENSIN_CD), MAX(TT_H_SIZE_CD), MAX(TT_H_SIYOU_CD), MAX(TT_H_COLOR_CD) "

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(SQL, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                lblKensuu.Text = "0件"
            Else
                '抽出データを一覧に表示する
                dgvKousin.DataSource = ds
                dgvKousin.DataMember = RS

                lblKensuu.Text = iRecCnt & "件"
            End If

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　データ更新
    '　（処理概要）変更されたデータをDBに更新する
    '   ●入力パラメタ：なし
    '   ●関数戻り値　：なし
    '-------------------------------------------------------------------------------
    Private Sub updateDB()
        Try
            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGVハンドラの設定

            '更新開始日時を取得
            Dim updStartDate As Date = Now

            'トランザクション開始
            _db.beginTran()

            Dim SQL As String = ""
            Dim updCnt As Integer = 0       '更新した件数

            '全件ループ
            For i As Integer = 0 To _dgv.getMaxRow - 1

                '変更フラグが有効な行だけ更新する
                If HENKO_FLG.Equals(_dgv.getCellData(COLDT_CHANGEFLG, i)) Then
                    'M11計画対象品マスタ
                    SQL = ""
                    SQL = SQL & N & " UPDATE M11KEIKAKUHIN SET "
                    SQL = SQL & N & "    TT_LOT = " & _db.rmSQ(_dgv.getCellData(COLDT_LOTTYOU, i))                      '標準ロット長
                    SQL = SQL & N & "   ,TT_TANCYO = " & _db.rmSQ(_dgv.getCellData(COLDT_TANTYOU, i))                   '単長
                    SQL = SQL & N & "   ,TT_JYOSU = " & _db.rmSQ(_dgv.getCellData(COLDT_JOSU, i))                       '条数
                    SQL = SQL & N & "   ,TT_N_SO_SUU = " & _db.rmSQ(_dgv.getCellData(COLDT_JOSU, i))                    '入庫本数
                    SQL = SQL & N & "   ,TT_N_K_SUU = " & _db.rmSQ(_dgv.getCellData(COLDT_KND, i))                      '北日本分
                    SQL = SQL & N & "   ,TT_N_SH_SUU = " & _db.rmSQ(_dgv.getCellData(COLDT_HST, i))                     '住電日立分
                    SQL = SQL & N & "   ,TT_MAKI_CD = " & CInt(_db.rmSQ(_dgv.getCellData(COLDT_MAKIWAKU, i)))           '巻枠コード
                    SQL = SQL & N & "   ,TT_HOSO_KBN = '" & _db.rmSQ(_dgv.getCellData(COLDT_HOUSOU, i)) & "'"           '包装表示区分
                    SQL = SQL & N & "   ,TT_SIYOUSYO_NO = '" & _db.rmSQ(_dgv.getCellData(COLDT_SIYOUSYONO, i)) & "'"    '仕様書番号
                    SQL = SQL & N & "   ,TT_SYUBETU = " & _db.rmSQ(_dgv.getCellData(COLDT_ZAIKO, i))                    '在庫繰返
                    SQL = SQL & N & "   ,TT_KYAKSAKI = '" & _db.rmSQ(_dgv.getCellData(COLDT_CHUMONSAKI, i)) & "'"       '注文先
                    SQL = SQL & N & "   ,TT_JUYOUCD = '" & _db.rmSQ(_dgv.getCellData(COLDT_JUYOUSAKI, i)) & "'"         '需要先
                    SQL = SQL & N & "   ,TT_TENKAIPTN = '" & _db.rmSQ(_dgv.getCellData(COLDT_SIZETENKAI, i)) & "'"      'サイズ展開パターン
                    SQL = SQL & N & "   ,TT_HINSYUKBN = '" & _db.rmSQ(_dgv.getCellData(COLDT_HINSYUKBN, i)) & "'"       '品種区分
                    SQL = SQL & N & "   ,TT_KZAIKOTUKISU = " & NS(_db.rmSQ(_dgv.getCellData(COLDT_KIJUNTUKISU, i)))     '基準月数
                    SQL = SQL & N & "   ,TT_SFUKKYUU = " & NS(_db.rmSQ(_dgv.getCellData(COLDT_SAIGAI, i)))              '災害復旧在庫
                    SQL = SQL & N & "   ,TT_ANNZENZAIKO = " & NS(_db.rmSQ(_dgv.getCellData(COLDT_ANZENZAIKO, i)))       '安全在庫
                    SQL = SQL & N & "   ,TT_KAMOKU_CD = " & NS(_db.rmSQ(_dgv.getCellData(COLDT_KAMOKUCD, i)))           '科目コード
                    SQL = SQL & N & "   ,TT_SEIZOU_BMN = " & _db.rmSQ(_dgv.getCellData(COLDT_SEIZOUBUMON, i))           '製造部門
                    SQL = SQL & N & "   ,TT_TENKAI_KBN = " & _db.rmSQ(_dgv.getCellData(COLDT_TENKAIKBN, i))             '展開区分
                    SQL = SQL & N & "   ,TT_KOUTEI = '" & _db.rmSQ(_dgv.getCellData(COLDT_BUBUNKOUTEI, i)) & "'"        '部分展開指定工程
                    SQL = SQL & N & "   ,TT_HINSITU_KBN = " & _db.rmSQ(_dgv.getCellData(COLDT_HINSITU, i))              '品質区分
                    SQL = SQL & N & "   ,TT_TATIAI_UM = " & _db.rmSQ(_dgv.getCellData(COLDT_TATIAI, i))                 '立会有無
                    SQL = SQL & N & "   ,TT_UPDNAME = '" & _tanmatuID & "'"                                             '端末ID
                    SQL = SQL & N & "   ,TT_DATE = TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "           '更新日時
                    '' 2011/02/10 ADD-S sugano
                    SQL = SQL & N & "   ,TT_TEHAI_SUU = " & _db.rmSQ(_dgv.getCellData(COLDT_LOTTYOU, i))                '手配数量（＝標準ロット長）
                    SQL = SQL & N & "   ,TT_TEHAI_KBN = " & _db.rmSQ(_dgv.getCellData(COLDT_ZAIKO, i))                  '手配区分（＝在庫繰返）
                    '' 2011/02/10 ADD-E sugano
                    SQL = SQL & N & " WHERE TT_KHINMEICD = '" & _dgv.getCellData(COLDT_HINMEICD, i) & "'"

                    'SQL発行
                    Dim recCnt As Integer
                    '更新に失敗しても特にメッセージは出さない。
                    '更新件数は正確に取得して、履歴テーブルに登録する。
                    Call _db.executeDB(SQL, recCnt)

                    '更新件数保持
                    updCnt = updCnt + 1

                    '変更フラグ無効化
                    _dgv.setCellData(COLDT_CHANGEFLG, i, "0")

                End If
            Next

            '更新終了日時を取得
            Dim updFinDate As Date = Now

            'T91実行履歴登録処理
            SQL = ""
            SQL = "INSERT INTO T91RIREKI ("
            SQL = SQL & N & "  PGID"                                                        '機能ID
            SQL = SQL & N & ", SNENGETU"                                                    '処理日時
            SQL = SQL & N & ", KNENGETU"                                                    '計画日時
            SQL = SQL & N & ", SDATESTART"                                                  '処理開始日時
            SQL = SQL & N & ", SDATEEND"                                                    '処理終了日時
            SQL = SQL & N & ", KENNSU1"                                                     '件数１（削除件数）
            SQL = SQL & N & ", UPDNAME"                                                     '端末ID
            SQL = SQL & N & ", UPDDATE"                                                     '更新日時
            SQL = SQL & N & ") VALUES ("
            SQL = SQL & N & "  '" & PGID & "'"                                              '機能ID
            SQL = SQL & N & ", NULL "
            SQL = SQL & N & ", NULL "
            SQL = SQL & N & ", TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "   '処理開始日時
            SQL = SQL & N & ", TO_DATE('" & updFinDate & "', 'YYYY/MM/DD HH24:MI:SS') "     '処理終了日時
            SQL = SQL & N & ", " & updCnt                                                   '件数１（更新件数）
            SQL = SQL & N & ", '" & _tanmatuID & "'"                                        '端末ID
            SQL = SQL & N & ", TO_DATE('" & updFinDate & "', 'YYYY/MM/DD HH24:MI:SS') "     '更新日時
            SQL = SQL & N & " )"
            _db.executeDB(SQL)

            'T02処理制御テーブル更新
            _parentForm.updateSeigyoTbl(PGID, True, updStartDate, updFinDate)

            'トランザクション終了
            _db.commitTran()

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        Finally
            If _db.isTransactionOpen = True Then
                _db.rollbackTran()                          'ロールバック
            End If
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　製造部門コンボボックス作成
    '-------------------------------------------------------------------------------
    Private Sub setCboSeizoBmn()
        Try

            Dim sql As String = ""
            sql = sql & N & " SELECT KAHENKEY KAHEN, NAME1 NAME FROM M01HANYO "
            sql = sql & N & "   WHERE KOTEIKEY = '" & M01KOTEI_SEIZOBMN & "'"

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                Exit Sub
            End If

            'コンボボックスクリア
            Me.cboSeizouBmn.Items.Clear()
            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(cboSeizouBmn)

            '先頭に空行
            ch.addItem(New UtilCboVO("", ""))

            'ループさせてコンボボックスにセット
            For i As Integer = 0 To iRecCnt - 1
                ch.addItem(New UtilCboVO(_db.rmNullStr(ds.Tables(RS).Rows(i)("KAHEN")), _db.rmNullStr(ds.Tables(RS).Rows(i)("NAME"))))
            Next

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　製作区分コンボボックス作成
    '-------------------------------------------------------------------------------
    Private Sub setCboSeisakuKbn()
        Try

            Dim sql As String = ""
            sql = sql & N & " SELECT KAHENKEY KAHEN, NAME1 NAME FROM M01HANYO "
            sql = sql & N & "   WHERE KOTEIKEY = '" & M01KOTEI_SEISAKUKBN & "'"

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                Exit Sub
            End If

            'コンボボックスクリア
            Me.cboSeisakuKbn.Items.Clear()
            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(cboSeisakuKbn)

            '先頭に空行
            ch.addItem(New UtilCboVO("", ""))

            'ループさせてコンボボックスにセット
            For i As Integer = 0 To iRecCnt - 1
                ch.addItem(New UtilCboVO(_db.rmNullStr(ds.Tables(RS).Rows(i)("KAHEN")), _db.rmNullStr(ds.Tables(RS).Rows(i)("NAME"))))
            Next

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　需要先コンボボックス作成
    '-------------------------------------------------------------------------------
    Private Sub setCboJuyosaki()
        Try

            Dim sql As String = ""
            sql = sql & N & " SELECT KAHENKEY KAHEN, NAME2 NAME FROM M01HANYO "
            sql = sql & N & "   WHERE KOTEIKEY = '" & M01KOTEI_JUYOUSAKI & "'"

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                Exit Sub
            End If

            'コンボボックスクリア
            Me.cboJuyosaki.Items.Clear()
            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(cboJuyosaki)

            '先頭に空行
            ch.addItem(New UtilCboVO("", ""))

            'ループさせてコンボボックスにセット
            For i As Integer = 0 To iRecCnt - 1
                ch.addItem(New UtilCboVO(_db.rmNullStr(ds.Tables(RS).Rows(i)("KAHEN")), _db.rmNullStr(ds.Tables(RS).Rows(i)("NAME"))))
            Next

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　巻枠コード存在チェック
    '　（処理概要）渡された巻枠コードをもとに巻枠コードテーブルを検索する
    '   ●入力パラメタ：prmMakiwakuCD   巻枠コード入力値
    '   ●関数戻り値　：TRUE(存在)/FALSE(存在せず)
    '-------------------------------------------------------------------------------
    Private Function serchMakiwaku(ByVal prmMakiwakuCD As String)
        Try
            Dim sql = ""
            sql = sql & N & " SELECT "
            sql = sql & N & " ZE_NAME "
            sql = sql & N & " FROM ZEASYCODE_TB "
            sql = sql & N & "   WHERE ZE_CODE = '" & prmMakiwakuCD & "'"

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then            '抽出レコードが１件もない場合
                Return False
            End If

            Return True

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Function

    '-------------------------------------------------------------------------------
    '　包装表示区分存在チェック
    '　（処理概要）渡された包装表示区分をもとに包装テーブルを検索する
    '   ●入力パラメタ：prmHousouCD   包装表示区分入力値
    '   ●関数戻り値　：TRUE(存在)/FALSE(存在せず)
    '-------------------------------------------------------------------------------
    Private Function serchHousou(ByVal prmHousou As String)
        Try
            Dim sql = ""
            sql = sql & N & " SELECT "
            sql = sql & N & " HN_NAME "
            sql = sql & N & " FROM HOSONAME_TB "
            sql = sql & N & "   WHERE HN_KUBUN = '" & prmHousou & "'"

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then            '抽出レコードが１件もない場合
                Return False
            End If

            Return True

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Function

    '-------------------------------------------------------------------------------
    '　汎用マスタ名称2抽出
    '　（処理概要）渡された固定キーおよび可変キーをもとに名称2を検索して返す
    '   ●入力パラメタ：prmKoteiKey     固定キー
    '                 ：prmKahenKey     可変キー
    '   ●関数戻り値　：                抽出した名称
    '-------------------------------------------------------------------------------
    Private Function backNameFromM01(ByVal prmKoteiKey As String, ByVal prmKahenKey As String)
        Try

            Dim dtCol1 As String = "NAME1"    '名称1エイリアス
            Dim dtCol2 As String = "NAME2"    '名称2エイリアス

            Dim sql As String = ""
            sql = sql & N & " SELECT NAME1 " & dtCol1 & " , NAME2 " & dtCol2
            sql = sql & N & " FROM M01HANYO "
            sql = sql & N & "   WHERE KOTEIKEY = '" & prmKoteiKey & "'"
            sql = sql & N & "   AND KAHENKEY = '" & prmKahenKey & "'"

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then            '抽出レコードが１件もない場合
                Return ""
            Else
                '品質試験区分及び立会有無は画面表示しないので、とりあえず名称1を返す
                If prmKoteiKey.Equals(M01KOTEI_HINSITU) Or prmKoteiKey.Equals(M01KOTEI_TATIAI) Then
                    Return _db.rmNullStr(ds.Tables(RS).Rows(0)(dtCol1))
                End If
                'その他は名称2を返す
                Return _db.rmNullStr(ds.Tables(RS).Rows(0)(dtCol2))
            End If

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Function

    '-------------------------------------------------------------------------------
    '　品種区分
    '　（処理概要）入力された需要先と品種区分の組み合わせが存在するかチェックする
    '   ●入力パラメタ：prmHinsyuKbnCD      品種区分コード
    '                 ：prmJuyosakiCD       需要先コード
    '   ●関数戻り値　：TRUE(存在)/FALSE(存在せず)
    '-------------------------------------------------------------------------------
    Private Function hinsyuKbnExist(ByVal prmHinsyuKbnCD As String, ByVal prmJuyosakiCD As String)
        Try

            Dim sql = ""
            sql = sql & N & " SELECT "
            sql = sql & N & " HINSYUKBNNM "
            sql = sql & N & " FROM M02HINSYUKBN "
            sql = sql & N & "   WHERE JUYOUCD = '" & prmJuyosakiCD & "'"
            sql = sql & N & "   AND HINSYUKBN = '" & prmHinsyuKbnCD & "'"

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                Return False
            End If
            Return True

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Function

    '-------------------------------------------------------------------------------
    '　集計品名数再表示
    '　（処理概要）集計対象品名登録画面で編集後のレコード数を一覧に反映させる
    '   ●入力パラメタ：prmRowIndex         一覧の行番号
    '   ●関数戻り値　：なし
    '-------------------------------------------------------------------------------
    Private Sub reDispSyukeiHinmei(ByVal prmRowIndex As Integer)
        Try
            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGVハンドラの設定

            Dim sql = ""
            sql = sql & N & " SELECT "
            sql = sql & N & " COUNT(M12.HINMEICD) - 1 CNT "
            sql = sql & N & " FROM M12SYUYAKU M12 "
            sql = sql & N & " WHERE M12.KHINMEICD = '" & _dgv.getCellData(COLDT_HINMEICD, prmRowIndex) & "'"

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                _dgv.setCellData(COLDT_SHINMEI, prmRowIndex, "0")
            Else
                _dgv.setCellData(COLDT_SHINMEI, prmRowIndex, CStr(_db.rmNullInt(ds.Tables(RS).Rows(0)("CNT"))))
            End If

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　NULL判定
    '　(処理概要)セルの内容がNULLなら"NULL"を返す
    '　　I　：　prmStr      DBに登録するDOUBLE型の値
    '　　R　：　NS          prmStrが空なら「NULL」、それ以外なら「prmStr」
    '------------------------------------------------------------------------------------------------------
    Private Function NS(ByVal prmStr As String) As String
        Dim ret As String = ""
        If "".Equals(prmStr) Then
            ret = "NULL"
        Else
            ret = prmStr
        End If
        Return ret
    End Function

#End Region

#Region "ユーザ定義関数:チェック処理"

    '-------------------------------------------------------------------------------
    '　入力値チェック
    '　（処理概要）入力内容の妥当性をチェックする
    '-------------------------------------------------------------------------------
    Private Sub chkInputValue()
        Try

            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGVハンドラの設定

            '全行ループ
            For rowCnt As Integer = 0 To dgvKousin.RowCount - 1
                '変更フラグが有効な行のみチェックを行う。
                If HENKO_FLG.Equals(_dgv.getCellData(COLDT_CHANGEFLG, rowCnt)) Then

                    '巻枠コードのチェック
                    If checkMakiwaku(rowCnt) = False Then
                        Throw New UsrDefException("巻枠名がマスタに登録されていません。", _
                                _msgHd.getMSG("noExistMakiwaku", "【巻枠コード】"), dgvKousin, COLCN_MAKIWAKU, rowCnt)
                    End If

                    '包装表示区分のチェック
                    If checkHousou(rowCnt) = False Then
                        Throw New UsrDefException("包装／表示種類がマスタに登録されていません。", _
                                _msgHd.getMSG("noExistHousou", "【包装表示区分】"), dgvKousin, COLCN_HOUSOU, rowCnt)
                    End If

                    '在庫繰返変更時
                    If checkZaiko(rowCnt) = False Then
                        Throw New UsrDefException("汎用マスタに登録されていない値です。", _
                                    _msgHd.getMSG("noExistHanyouMst", "【在庫繰返】"), dgvKousin, COLCN_ZAIKO, rowCnt)
                    End If

                    '在庫繰返 = 「1」のときのみ以下のチェックを行う。
                    If _db.rmNullInt(dgvKousin(COLNO_ZAIKO, rowCnt).Value) = M01KAHEN_ZAIKO_ZAIKO Then

                        '需要先入力時
                        If checkJuyo(rowCnt) = False Then
                            Throw New UsrDefException("汎用マスタに登録されていない値です。", _
                                    _msgHd.getMSG("noExistHanyouMst", "【需要先】"), dgvKousin, COLCN_JUYOUSAKI, rowCnt)
                        End If

                        'サイズ展開入力時
                        If checkSizeTenkai(rowCnt) = False Then
                            Throw New UsrDefException("汎用マスタに登録されていない値です。", _
                                    _msgHd.getMSG("noExistHanyouMst", "【サイズ展開】"), dgvKousin, COLCN_SIZETENKAI, rowCnt)
                        End If

                        '品種区分
                        If checkHinsyuKbn(rowCnt) = False Then
                            Throw New UsrDefException("品種区分がマスタに登録されていません。", _
                                            _msgHd.getMSG("noHinsyuKbn", "【品種区分】"), dgvKousin, COLCN_HINSYUKBN, rowCnt)
                        End If
                    End If

                    '製造部門入力時
                    If checkSeizouBmn(rowCnt) = False Then
                        Throw New UsrDefException("汎用マスタに登録されていない値です。", _
                                    _msgHd.getMSG("noExistHanyouMst", "【製造部門】"), dgvKousin, COLCN_SEIZOUBUMON, rowCnt)
                    End If

                    '展開区分入力時
                    If checkTenkaiKbn(rowCnt) = 1 Then
                        Throw New UsrDefException("汎用マスタに登録されていない値です。", _
                                    _msgHd.getMSG("noExistHanyouMst", "【展開区分】"), dgvKousin, COLCN_TENKAIKBN, rowCnt)
                    ElseIf checkTenkaiKbn(rowCnt) = 2 Then
                        Throw New UsrDefException("製作区分「外注」時は展開区分「部分展開」以外選択できません。", _
                                    _msgHd.getMSG("nonGaicyuSelect"), dgvKousin, COLCN_TENKAIKBN, rowCnt)
                    End If

                    '品質試験区分入力時
                    If checkHinsitu(rowCnt) = False Then
                        Throw New UsrDefException("汎用マスタに登録されていない値です。", _
                                    _msgHd.getMSG("noExistHanyouMst", "【品質試験部門】"), dgvKousin, COLCN_HINSITU, rowCnt)
                    End If

                    '立会有無入力時
                    If checkTatiai(rowCnt) = False Then
                        Throw New UsrDefException("汎用マスタに登録されていない値です。", _
                                    _msgHd.getMSG("noExistHanyouMst", "【立会有無】"), dgvKousin, COLCN_TATIAI, rowCnt)
                    End If

                End If
            Next

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　入力チェック処理（更新ボタン押下時）
    '   （処理概要）各入力項目のチェックを行う。
    '-------------------------------------------------------------------------------
    Private Sub chkBeforeUpdate()
        Try
            _dgv = New UtilDataGridViewHandler(dgvKousin)                       'DGVハンドラの設定

            '全行ループ
            For rowCnt As Integer = 0 To dgvKousin.RowCount - 1

                '変更フラグが有効な行のみチェックを行う。
                If HENKO_FLG.Equals(_dgv.getCellData(COLDT_CHANGEFLG, rowCnt)) Then

                    '標準ロット長必須チェック
                    If "".Equals(_db.rmNullStr(dgvKousin(COLNO_LOTTYOU, rowCnt).Value)) Then
                        Throw New UsrDefException("必須入力項目です。", _
                                    _msgHd.getMSG("requiredImput", "【標準ロット長】"), dgvKousin, COLCN_LOTTYOU, rowCnt)
                    End If

                    '単長必須チェック
                    If "".Equals(_db.rmNullStr(dgvKousin(COLNO_TANTYOU, rowCnt).Value)) Then
                        Throw New UsrDefException("必須入力項目です。", _
                                        _msgHd.getMSG("requiredImput", "【単長】"), dgvKousin, COLCN_TANTYOU, rowCnt)
                    End If

                    '条数数値チェック
                    If _db.rmNullInt(dgvKousin(COLNO_LOTTYOU, rowCnt).Value) Mod _db.rmNullInt(dgvKousin(COLNO_TANTYOU, rowCnt).Value) <> 0 Then
                        Throw New UsrDefException("標準ロット長または製作単長が違います。", _
                                        _msgHd.getMSG("failLotOrTantyou"), dgvKousin, COLCN_LOTTYOU, rowCnt)
                    End If

                    '単長×条数が「9999999」を超える場合
                    If dgvKousin(COLNO_TANTYOU, rowCnt).Value * dgvKousin(COLNO_JOSU, rowCnt).Value > 9999999 Then
                        Throw New UsrDefException("単長×条数の値が7桁を超えています。", _
                                                    _msgHd.getMSG("over7Keta"), dgvKousin, COLCN_TANTYOU, rowCnt)
                    End If

                    '北日本分必須チェック
                    If "".Equals(_db.rmNullStr(dgvKousin(COLNO_KND, rowCnt).Value)) Then
                        Throw New UsrDefException("必須入力項目です。", _
                                            _msgHd.getMSG("requiredImput", "【入庫KND本数】"), dgvKousin, COLCN_KND, rowCnt)
                    End If

                    '北日本分数値チェック
                    If _db.rmNullInt(dgvKousin(COLNO_KND, rowCnt).Value) > _db.rmNullInt(dgvKousin(COLNO_JOSU, rowCnt).Value) Then
                        Throw New UsrDefException("範囲外の値が入力されました。", _
                                        _msgHd.getMSG("errOutOfRange", "【入庫KND本数】"), dgvKousin, COLCN_KND, rowCnt)
                    End If

                    '住電日立分必須チェック
                    If "".Equals(_db.rmNullStr(dgvKousin(COLNO_HST, rowCnt).Value)) Then
                        Throw New UsrDefException("必須入力項目です。", _
                                        _msgHd.getMSG("requiredImput", "【入庫HST本数】"), dgvKousin, COLCN_HST, rowCnt)
                    End If

                    '住電日立分数値チェック
                    If _db.rmNullInt(dgvKousin(COLNO_HST, rowCnt).Value) > _db.rmNullInt(dgvKousin(COLNO_JOSU, rowCnt).Value) Then
                        Throw New UsrDefException("範囲外の値が入力されました。", _
                                            _msgHd.getMSG("errOutOfRange", "【入庫HST本数】"), dgvKousin, COLCN_HST, rowCnt)
                    End If

                    '巻枠コード必須チェック
                    If "".Equals(_db.rmNullStr(dgvKousin(COLNO_MAKIWAKU, rowCnt).Value)) Then
                        Throw New UsrDefException("必須入力項目です。", _
                                    _msgHd.getMSG("requiredImput", "【巻枠コード】"), dgvKousin, COLCN_MAKIWAKU, rowCnt)
                    End If

                    '包装表示区分コード必須チェック
                    If "".Equals(_db.rmNullStr(dgvKousin(COLNO_HOUSOU, rowCnt).Value)) Then
                        Throw New UsrDefException("必須入力項目です。", _
                                    _msgHd.getMSG("requiredImput", "【包装表示区分】"), dgvKousin, COLCN_HOUSOU, rowCnt)
                    End If

                    '仕様書番号必須チェック
                    If "".Equals(_db.rmNullStr(dgvKousin(COLNO_SIYOUSYONO, rowCnt).Value)) Then
                        Throw New UsrDefException("必須入力項目です。", _
                                _msgHd.getMSG("requiredImput", "【仕様書番号】"), dgvKousin, COLCN_SIYOUSYONO, rowCnt)
                    End If

                    '在庫繰返コード必須チェック
                    If "".Equals(_db.rmNullStr(dgvKousin(COLNO_ZAIKO, rowCnt).Value)) Then
                        Throw New UsrDefException("必須入力項目です。", _
                                    _msgHd.getMSG("requiredImput", "【在庫繰返】"), dgvKousin, COLCN_ZAIKO, rowCnt)
                    End If

                    '在庫繰返 =「2」のときのみ注文先は必須入力
                    If dgvKousin(COLNO_ZAIKO, rowCnt).Value = M01KAHEN_ZAIKO_JUTYU Then
                        '注文先コード入力チェック
                        If "".Equals(_db.rmNullStr(dgvKousin(COLNO_CHUMONSAKI, rowCnt).Value)) Then
                            Throw New UsrDefException("必須入力項目です。", _
                                    _msgHd.getMSG("requiredImput", "【注文先】"), dgvKousin, COLCN_CHUMONSAKI, rowCnt)
                        End If
                    End If

                    '在庫繰返 = 「1」のときのみ以下のチェックを行う。
                    If _db.rmNullInt(dgvKousin(COLNO_ZAIKO, rowCnt).Value) = M01KAHEN_ZAIKO_ZAIKO Then

                        '需要先コード必須チェック
                        If "".Equals(_db.rmNullStr(dgvKousin(COLNO_JUYOUSAKI, rowCnt).Value)) Then
                            Throw New UsrDefException("必須入力項目です。", _
                                    _msgHd.getMSG("requiredImput", "【需要先】"), dgvKousin, COLCN_JUYOUSAKI, rowCnt)
                        End If

                        'サイズ展開必須チェック
                        If "".Equals(_db.rmNullStr(dgvKousin(COLNO_SIZETENKAI, rowCnt).Value)) Then
                            Throw New UsrDefException("必須入力項目です。", _
                                    _msgHd.getMSG("requiredImput", "【サイズ展開】"), dgvKousin, COLCN_SIZETENKAI, rowCnt)
                        End If

                        '品種区分必須チェック
                        If "".Equals(_db.rmNullStr(dgvKousin(COLNO_HINSYUKBN, rowCnt).Value)) Then
                            Throw New UsrDefException("必須入力項目です。", _
                                     _msgHd.getMSG("requiredImput", "【品種区分】"), dgvKousin, COLCN_HINSYUKBN, rowCnt)
                        End If

                        '基準月数必須チェック
                        If "".Equals(_db.rmNullStr(dgvKousin(COLNO_KIJUNTUKISUU, rowCnt).Value)) Then
                            Throw New UsrDefException("必須入力項目です。", _
                                    _msgHd.getMSG("requiredImput", "【基準月数】"), dgvKousin, COLCN_KIJUNTUKISU, rowCnt)
                        End If
                    End If

                    '製作区分 = 「2」の場合のみチェック
                    If M01NAME2_SEISAKUKBN.Equals(_db.rmNullStr(dgvKousin(COLNO_SEISAKUKBN, rowCnt).Value)) Then
                        '科目コード必須チェック
                        If "".Equals(_db.rmNullStr(dgvKousin(COLNO_KAMOKUCD, rowCnt).Value)) Then
                            Throw New UsrDefException("必須入力項目です。", _
                                    _msgHd.getMSG("requiredImput", "【科目コード】"), dgvKousin, COLCN_KAMOKUCD, rowCnt)
                        End If

                        '科目コード桁数チェック
                        '' 2011/01/13 upd start sugano
                        'If _db.rmNullStr(_dgv.getCellData(COLDT_KAMOKUCD, rowCnt)).Length <> 6 Then
                        '    Throw New UsrDefException("科目コードは６桁で入力して下さい。", _
                        '                _msgHd.getMSG("notKamokuCD6Keta", "【科目コード】"), dgvKousin, COLCN_KAMOKUCD, rowCnt)
                        'End If
                        If _db.rmNullStr(_dgv.getCellData(COLDT_KAMOKUCD, rowCnt)).Length <> 5 Then
                            Throw New UsrDefException("科目コードは５桁で入力して下さい。", _
                                        _msgHd.getMSG("notKamokuCDKeta", "【科目コード】"), dgvKousin, COLCN_KAMOKUCD, rowCnt)
                        End If
                        '' 2011/01/13 upd end sugano
                    End If

                    '製造部門必須チェック
                    If "".Equals(_db.rmNullStr(dgvKousin(COLNO_SEIZOUBUMON, rowCnt).Value)) Then
                        Throw New UsrDefException("必須入力項目です。", _
                            _msgHd.getMSG("requiredImput", "【製造部門】"), dgvKousin, COLCN_SEIZOUBUMON, rowCnt)
                    End If

                    '展開区分必須チェック
                    If "".Equals(_db.rmNullStr(dgvKousin(COLNO_TENKAIKBN, rowCnt).Value)) Then
                        Throw New UsrDefException("必須入力項目です。", _
                                _msgHd.getMSG("requiredImput", "【展開区分】"), dgvKousin, COLCN_TENKAIKBN, rowCnt)
                    End If

                    '展開区分 = 「2」ならチェック
                    If dgvKousin(COLNO_TENKAIKBN, rowCnt).Value = M01KAHEN_TENKAI_BUBUN Then
                        '部分展開工程必須チェック
                        If "".Equals(_db.rmNullStr(dgvKousin(COLNO_BUBUNKOUTEI, rowCnt).Value)) Then
                            Throw New UsrDefException("必須入力項目です。", _
                                    _msgHd.getMSG("requiredImput", "【部分展開工程】"), dgvKousin, COLCN_BUBUNKOUTEI, rowCnt)
                        End If

                        '部分展開工程入力内容チェック
                        If Not CStr(dgvKousin(COLNO_BUBUNKOUTEI, rowCnt).Value).Substring(0, 1) = "1" And _
                                Not CStr(dgvKousin(COLNO_BUBUNKOUTEI, rowCnt).Value).Substring(0, 1) = "3" Then

                            Throw New UsrDefException("1または3から始まる工程を入力してください。", _
                                    _msgHd.getMSG("notBKouteiStart1Or3", "【部分展開工程】"), dgvKousin, COLCN_BUBUNKOUTEI, rowCnt)
                        End If
                    End If

                    '品質試験必須チェック
                    If "".Equals(_db.rmNullStr(dgvKousin(COLNO_HINSITU, rowCnt).Value)) Then
                        Throw New UsrDefException("必須入力項目です。", _
                                _msgHd.getMSG("requiredImput", "【品質試験】"), dgvKousin, COLCN_HINSITU, rowCnt)
                    End If

                    '立会有無チェック
                    If "".Equals(_db.rmNullStr(dgvKousin(COLNO_TATIAI, rowCnt).Value)) Then
                        Throw New UsrDefException("必須入力項目です。", _
                                _msgHd.getMSG("requiredImput", "【立会有無】"), dgvKousin, COLCN_TATIAI, rowCnt)
                    End If

                End If
            Next

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

#End Region

End Class