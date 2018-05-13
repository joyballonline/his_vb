'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）生産計画数量修正
'    （フォームID）ZG530E_SeisanSuuryouSyuusei
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   中澤        2010/09/01                 新規              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.Combo
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls

Public Class ZG530E_SeisanSuuryouSyuusei
    Inherits System.Windows.Forms.Form

#Region "リテラル値定義"
    '-------------------------------------------------------------------------------
    '   定数定義
    '-------------------------------------------------------------------------------

    'PG制御文字 
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const RS As String = "RecSet"                       'レコードセットテーブル

    Private Const PGID As String = "ZG530E"                     'T91に登録するPGID

    '一覧データバインド名
    Private Const COLDT_JUYOUCD As String = "dtJuyouCD"             '需要先
    Private Const COLDT_JUYOUSAKI As String = "dtJuyousaki"         '需要先名
    Private Const COLDT_HINMEICD As String = "dtHinmeiCD"           '品名コード
    Private Const COLDT_HINMEI As String = "dtHinmei"               '品名
    Private Const COLDT_HINSYUCD As String = "dtHinsyuCD"           '品種コード
    Private Const COLDT_HINSYUNM As String = "dtHinsyuNM"           '品種名
    Private Const COLDT_LOTTYO As String = "dtLottyou"              'ロット長
    Private Const COLDT_ABC As String = "dtABC"                     'ABC
    Private Const COLDT_ZZZAIKOS As String = "dtZZZaikosu"          '前々月末在庫数
    Private Const COLDT_ZZZAIKOR As String = "dtZZZaikoryou"        '前々月末在庫量
    Private Const COLDT_ZSEISANS As String = "dtZSeisansu"          '前月生産実績数
    Private Const COLDT_ZSEISANR As String = "dtZSeisanryou"        '前月生産実績量
    Private Const COLDT_ZHANBAIS As String = "dtZHanbaisu"          '前月販売実績数
    Private Const COLDT_ZHANBAIR As String = "dtZHanbairyou"        '前月販売実績量
    Private Const COLDT_ZZAIKOS As String = "dtZZaikoS"             '前月末在庫数
    Private Const COLDT_ZZAIKOR As String = "dtZZaikoR"             '前月末在庫量
    Private Const COLDT_TSEISANS As String = "dtTSeisanS"           '当月生産計画数
    Private Const COLDT_TSEISANR As String = "dtTSeisanR"           '当月生産計画量
    Private Const COLDT_THANBAIS As String = "dtTHanbaiS"           '当月販売計画数
    Private Const COLDT_THANBAIR As String = "dtTHanbaiR"           '当月販売計画量
    Private Const COLDT_TZAIKOS As String = "dtTZaikoS"             '当月末在庫数
    Private Const COLDT_TZAIKOR As String = "dtTZaikoR"             '当月末在庫量
    Private Const COLDT_KURIKOSIS As String = "dtKurikosiS"         '繰越数
    Private Const COLDT_KURIKOSIR As String = "dtKurikosiR"         '繰越量
    Private Const COLDT_LOTSU As String = "dtLotsuu"                'ロット数
    Private Const COLDT_YSEISANS As String = "dtYSeisanS"           '翌月生産計画数
    Private Const COLDT_YSEISANR As String = "dtYSeisanR"           '翌月生産計画量
    Private Const COLDT_YHANBAIS As String = "dtYHanbaiS"           '翌月販売計画数
    Private Const COLDT_YHANBAIR As String = "dtYHanbaiR"           '翌月販売計画量
    Private Const COLDT_YZAIKOS As String = "dtYZaikoS"             '翌月末在庫数
    Private Const COLDT_YZAIKOR As String = "dtYZaikoR"             '翌月末在庫量
    Private Const COLDT_ZAIKOTUKISU As String = "dtZaikoTukisu"     '在庫月数
    Private Const COLDT_YYHANBAIS As String = "dtYYHanbaiS"         '翌々月販売計画数
    Private Const COLDT_YYHANBAIR As String = "dtYYHanbaiR"         '翌々月販売計画量
    Private Const COLDT_KTUKISU As String = "dtKTukisu"             '基準月数
    Private Const COLDT_FZAIKOS As String = "dtFukkyuS"             '復旧用在庫数
    Private Const COLDT_FZAIKOR As String = "dtFukkyuR"             '復旧用在庫量
    Private Const COLDT_AZAIKOS As String = "dtAZaikoS"             '安全在庫数
    Private Const COLDT_AZAIKOR As String = "dtAZaikoR"             '安全在庫量
    Private Const COLDT_METUKE As String = "dtMetuke"               '目付
    Private Const COLDT_SIYOCD As String = "dtSiyoCD"               '仕様コード

    '一覧グリッド名
    Private Const COLCN_JUYOUCD As String = "cnJuyouCD"             '需要先
    Private Const COLCN_JUYOUSAKI As String = "cnJuyousaki"         '需要先名
    Private Const COLCN_HINMEICD As String = "cnHinmeiCD"           '品名コード
    Private Const COLCN_HINMEI As String = "cnHinmei"               '品名
    Private Const COLCN_HINSYUNM As String = "cnHinsyuNM"           '品種名
    Private Const COLCN_LOTTYO As String = "cnLottyou"              'ロット長
    Private Const COLCN_ABC As String = "cnABC"                     'ABC
    Private Const COLCN_ZZAIKOS As String = "cnZZaikoS"             '前月末在庫数
    Private Const COLCN_ZZAIKOR As String = "cnZZaikoR"             '前月末在庫量
    Private Const COLCN_TSEISANS As String = "cnTSeisanS"           '当月生産計画数
    Private Const COLCN_TSEISANR As String = "cnTSeisanR"           '当月生産計画量
    Private Const COLCN_THANBAIS As String = "cnTHanbaiS"           '当月販売計画数
    Private Const COLCN_THANBAIR As String = "cnTHanbaiR"           '当月販売計画量
    Private Const COLCN_TZAIKOS As String = "cnTZaikoS"             '当月末在庫数
    Private Const COLCN_TZAIKOR As String = "cnTZaikoR"             '当月末在庫量
    Private Const COLCN_KURIKOSIS As String = "cnKurikosiS"         '繰越数
    Private Const COLCN_KURIKOSIR As String = "cnKurikosiR"         '繰越量
    Private Const COLCN_LOTSUU As String = "cnLotsuu"               'ロット数
    Private Const COLCN_YSEISANS As String = "cnYSeisanS"           '翌月生産計画数
    Private Const COLCN_YSEISANR As String = "cnYSeisanR"           '翌月生産計画量
    Private Const COLCN_YHANBAIS As String = "cnYHanbaiS"           '翌月販売計画数
    Private Const COLCN_YHANBAIR As String = "cnYHanbaiR"           '翌月販売計画量
    Private Const COLCN_YZAIKOS As String = "cnYZaikoS"             '翌月末在庫数
    Private Const COLCN_YZAIKOR As String = "cnYZaikoR"             '翌月末在庫量
    Private Const COLCN_ZAIKOTUKISU As String = "cnZaikoTukisu"     '在庫月数
    Private Const COLCN_YYHANBAIS As String = "cnYYHanbaiS"         '翌々月販売計画数
    Private Const COLCN_YYHANBAIR As String = "cnYYHanbaiR"         '翌々月販売計画量
    Private Const COLCN_KTUKISU As String = "cnKTukisu"             '基準月数
    Private Const COLCN_FUKKYUS As String = "cnFukkyuS"             '復旧用在庫数
    Private Const COLCN_FUKKYUR As String = "cnFukkyuR"             '復旧用在庫量
    Private Const COLCN_AZAIKOS As String = "cnAZaikoS"             '安全在庫数
    Private Const COLCN_AZAIKOR As String = "cnAZaikoR"             '安全在庫量
    Private Const COLCN_METUKE As String = "cnMetuke"               '目付

    '一覧列番号
    Private Const COLNO_JUYOUCD As Integer = 0          '需要先
    Private Const COLNO_JUYOUSAKI As Integer = 1        '需要先名
    Private Const COLNO_HINMEICD As Integer = 2         '品名コード
    Private Const COLNO_HINMEI As Integer = 3           '品名
    Private Const COLNO_LOTTYO As Integer = 4           'ロット長
    Private Const COLNO_ABC As Integer = 5              'ABC
    Private Const COLNO_ZZAIKOS As Integer = 6          '前月末在庫数
    Private Const COLNO_ZZAIKOR As Integer = 7          '前月末在庫量
    Private Const COLNO_TSEISANS As Integer = 8         '当月生産計画数
    Private Const COLNO_TSEISANR As Integer = 9         '当月生産計画量
    Private Const COLNO_THANBAIS As Integer = 10        '当月販売計画数
    Private Const COLNO_THANBAIR As Integer = 11        '当月販売計画量
    Private Const COLNO_TZAIKOS As Integer = 12         '当月末在庫数
    Private Const COLNO_TZAIKOR As Integer = 13         '当月末在庫量
    Private Const COLNO_KURIKOSIS As Integer = 14       '繰越数
    Private Const COLNO_KURIKOSIR As Integer = 15       '繰越量
    Private Const COLNO_LOTSUU As Integer = 16          'ロット数
    Private Const COLNO_YSEISANS As Integer = 17        '翌月生産計画数
    Private Const COLNO_YSEISANR As Integer = 18        '翌月生産計画量
    Private Const COLNO_YHANBAIS As Integer = 19        '翌月販売計画数
    Private Const COLNO_YHANBAIR As Integer = 20        '翌月販売計画量
    Private Const COLNO_YZAIKOS As Integer = 21         '翌月末在庫数
    Private Const COLNO_YZAIKOR As Integer = 22         '翌月末在庫量
    Private Const COLNO_ZAIKOTUKISU As Integer = 23     '在庫月数
    Private Const COLNO_YYHANBAIS As Integer = 24       '翌々月販売計画数
    Private Const COLNO_YYHANBAIR As Integer = 25       '翌々月販売計画量
    Private Const COLNO_KTUKISU As Integer = 26         '基準月数
    Private Const COLNO_FUKKYUS As Integer = 27         '復旧用在庫数
    Private Const COLNO_FUKKYUR As Integer = 28         '復旧用在庫量
    Private Const COLNO_AZAIKOS As Integer = 29         '安全在庫数
    Private Const COLNO_AZAIKOR As Integer = 30         '安全在庫量
    Private Const COLNO_METUKE As Integer = 31          '目付
    Private Const COLNO_HINSYUNM As Integer = 32        '品種名

    'ラベル押下時並べ替え用リテラル
    Private Const LBL_JUYO As String = "需要先"
    Private Const LBL_HINSYU As String = "品種区分"
    Private Const LBL_HINMEICD As String = "品名ｺｰﾄﾞ"
    Private Const LBL_HINMEI As String = "品名"
    Private Const LBL_SYOJUN As String = "▼"
    Private Const LBL_KOJUN As String = "▲"

    '表示単位切り替え時ラベル変更用リテラル
    Private Const LBL_ZAIKO As String = "在庫"
    Private Const LBL_SEISAN As String = "生産"
    Private Const LBL_HANBAI As String = "販売"
    Private Const LBL_KURIKOSIS As String = "繰越数"
    Private Const LBL_KURIKOSIR As String = "繰越量"
    Private Const LBL_FUKKYU As String = "復旧用"
    Private Const LBL_ANNZEN As String = "安全在庫"
    Private Const LBL_KM As String = "Km"
    Private Const LBL_TON As String = "t"

    '一覧ヘッダ用リテラル
    Private Const MONTH As String = "月"            '一覧ヘッダ編集用

    '汎用マスタ
    Private Const KOTEI_JUYOU As String = "01"              '需要先固定キー
    Private Const HANYOU_NAME1 As String = "NAME1"          '名称１
    Private Const HANYOU_KAHENKEY As String = "KAHENKEY"    '可変キー

    'EXCEL
    Private Const START_PRINT As Integer = 10       'EXCEL出力開始行数

    '品種別集計表EXCEL出力列番号
    Private Const XLSCOL_H_HINSYUCD = 1     '品種コード
    Private Const XLSCOL_H_HINSYUNM = 2     '品種名
    Private Const XLSCOL_H_ZZZAIKO = 3      '前々月末在庫量
    Private Const XLSCOL_H_ZSEISAN = 4      '前月生産実績量
    Private Const XLSCOL_H_ZHANBAI = 5      '前月販売実績量
    Private Const XLSCOL_H_ZZAIKO = 6       '前月末在庫量
    Private Const XLSCOL_H_TSEISAN = 7      '当月生産計画量
    Private Const XLSCOL_H_THANBAI = 8      '当月販売計画量
    Private Const XLSCOL_H_TZAIKO = 9       '当月末在庫量
    Private Const XLSCOL_H_KURIKOSI = 10    '繰越量
    Private Const XLSCOL_H_LOT = 11         'ロット数
    Private Const XLSCOL_H_YSEISAN = 12     '翌月生産量
    Private Const XLSCOL_H_YHANBAI = 13     '翌月販売量
    Private Const XLSCOL_H_YZAIKO = 14      '翌月在庫量
    Private Const XLSCOL_H_YYHANBAI = 15    '翌々月販売量

    '生産販売在庫計画EXCEL出力列番号
    Private Const XLSCOL_SIYOUCD As Integer = 1         '仕様コード
    Private Const XLSCOL_HINMEI As Integer = 2          '品名
    Private Const XLSCOL_LOTTYO As Integer = 3          'ロット長
    Private Const XLSCOL_ABC As Integer = 4             'ABC区分
    Private Const XLSCOL_ZZZAIKOS As Integer = 5        '前々月末在庫数
    Private Const XLSCOL_ZSEISANS As Integer = 6        '前月生産実績数
    Private Const XLSCOL_ZHANBAIS As Integer = 7        '前月販売実績数
    Private Const XLSCOL_ZZAIKOS As Integer = 8         '前月末在庫数
    Private Const XLSCOL_TSEISANS As Integer = 9        '当月生産計画数
    Private Const XLSCOL_TSEISANR As Integer = 10       '当月生産計画量
    Private Const XLSCOL_THANBAIS As Integer = 11       '当月販売計画数
    Private Const XLSCOL_THANBAIR As Integer = 12       '当月販売計画量
    Private Const XLSCOL_TZAIKOS As Integer = 13        '当月末在庫数
    Private Const XLSCOL_TZAIKOR As Integer = 14        '当月末在庫量
    Private Const XLSCOL_KURIKOSIS As Integer = 15      '繰越数
    Private Const XLSCOL_LOTSU As Integer = 16          'ロット数
    Private Const XLSCOL_YSEISANS As Integer = 17       '翌月生産計画数
    Private Const XLSCOL_YSEISANR As Integer = 18       '翌月生産計画量
    Private Const XLSCOL_YHANBAIS As Integer = 19       '翌月販売計画数
    Private Const XLSCOL_YHANBAIR As Integer = 20       '翌月販売計画量
    Private Const XLSCOL_YZAIKOS As Integer = 21        '翌月末在庫数
    Private Const XLSCOL_YZAIKOR As Integer = 22        '翌月末在庫量
    Private Const XLSCOL_YTUKISU As Integer = 23        '在庫月数
    Private Const XLSCOL_YYHANBAIS As Integer = 24      '翌々月販売計画数
    Private Const XLSCOL_YYHANBAIR As Integer = 25      '翌々月販売計画量
    Private Const XLSCOL_KIJUNTUKISU As Integer = 26    '基準月数
    Private Const XLSCOL_FUKKYUYO As Integer = 27       '復旧用在庫数
    Private Const XLSCOL_ANZEN As Integer = 28          '安全在庫数
    Private Const XLSCOL_METUKE As Integer = 29         '目付
    Private Const XLSCOL_ZZAIKOR As Integer = 30        '前々月末在庫量

    '出力列アルファベット
    Private Const XLSALP_ZZZAIKOS As String = "E"       '前々月末在庫数
    Private Const XLSALP_ZSEISANS As String = "F"       '前月生産実績数
    Private Const XLSALP_ZHANBAIS As String = "G"       '前月販売実績数
    Private Const XLSALP_ZZAIKOS As String = "H"        '前月末在庫数
    Private Const XLSALP_TSEISANS As String = "I"       '当月生産計画数
    Private Const XLSALP_TSEISANR As String = "J"       '当月生産計画量
    Private Const XLSALP_THANBAIS As String = "K"       '当月販売計画数
    Private Const XLSALP_THANBAIR As String = "L"       '当月販売計画量
    Private Const XLSALP_TZAIKOS As String = "M"        '当月末在庫数
    Private Const XLSALP_TZAIKOR As String = "N"        '当月末在庫量
    Private Const XLSALP_KURIKOSIS As String = "O"      '繰越数
    Private Const XLSALP_LOTSU As String = "P"          'ロット数
    Private Const XLSALP_YSEISANS As String = "Q"       '翌月生産計画数
    Private Const XLSALP_YSEISANR As String = "R"       '翌月生産計画量
    Private Const XLSALP_YHANBAIS As String = "S"       '翌月販売計画数
    Private Const XLSALP_YHANBAIR As String = "T"       '翌月販売計画量
    Private Const XLSALP_YZAIKOS As String = "U"        '翌月末在庫数
    Private Const XLSALP_YZAIKOR As String = "V"        '翌月末在庫量
    Private Const XLSALP_YTUKISU As String = "W"        '在庫月数
    Private Const XLSALP_YYHANBAIS As String = "X"      '翌々月販売計画数
    Private Const XLSALP_YYHANBAIR As String = "Y"      '翌々月販売計画量
    Private Const XLSALP_ZZAIKOR As String = "AD"       '前々月末在庫量

    '品種別集計表雛形シート名
    Private Const XLSSHEETNM_HINSYU As String = "Ver1.0.00"

    Private Const CBONULLCODE As String = "NotSelect"   'コンボボックス未選択時のコード

#End Region

#Region "メンバー変数宣言"

    '-------------------------------------------------------------------------------
    '   変数定義
    '-------------------------------------------------------------------------------

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu

    Private _oldRowIndex As Integer = -1            '選択行の背景色を変更するための変数
    Private _colorCtlFlg As Boolean = False         '選択行の背景色を変更するためのフラグ

    Private _tanmatuID As String = ""               '端末ID

    Private _changeFlg As Boolean = False           '一覧データ変更フラグ
    Private _beforeChange As Double = 0             '一覧変更前のデータ

    Private _chkCellVO As UtilDgvChkCellVO          '一覧の入力制限用

    '検索条件格納変数
    Private _serchJuyo As String = ""               '需要先

    Private _startForm As Boolean = True            '画面起動時かどうかのフラグ
    Private _saikeisanFlg As Boolean = False        '再計算したかどうかのフラグ

    Private _updFlg As Boolean = False  '更新可否

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
        _updFlg = prmUpdFlg                                                 '更新可否

    End Sub
#End Region

#Region "Formイベント"

    '-------------------------------------------------------------------------------
    '　処理開始イベント
    '-------------------------------------------------------------------------------
    Private Sub ZG530E_SeisanSuuryouSyuusei_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            '描画関係の設定
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           'サイズが変更されたときに、コントロールがコントロール自体を再描画するかどうかを示す値を設定
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '描画はバッファで実行され、完了後に、結果が画面に出力されるよう設定
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  'コントロールは、画面に直接ではなく、まずバッファに描画されます。これにより、ちらつきを抑えることができます。
            Me.SetStyle(ControlStyles.UserPaint, True)              'コントロールは、オペレーティング システムによってではなく、独自に描画されるよう設定
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   'コントロールはウィンドウ メッセージ WM_ERASEBKGND を無視するように設定

            Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr 'タイトルオプション表示

            '画面表示
            Call initForm()
            _startForm = False

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

#End Region

#Region "ボタンイベント"

    '------------------------------------------------------------------------------------------------------
    '　登録ボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnTouroku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTouroku.Click
        Try

            '再計算されているかチェック
            If _saikeisanFlg = False Then
                Throw New UsrDefException("登録の前に再計算を行ってください。", _msgHd.getMSG("doSaikeisan"), btnSaikeisan)
            End If

            '必須入力チェック
            Call checkTouroku()

            '登録確認メッセージ
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmInsert")    '登録します。
            If rtn <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If

            '変更フラグをリセットする
            _changeFlg = False

            'マウスカーソル砂時計
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor
            Try

                '一覧のデータをワークテーブルに更新
                Call updateWK10()

                'トランザクション開始
                _db.beginTran()

                'T41へ登録
                Call registT41()

                'トランザクション終了
                _db.commitTran()

                'マウスカーソル矢印
                Me.Cursor = Cursors.Arrow

                '並べ替えラベルの表示初期化
                Call initLabel()

                '完了メッセージ
                _msgHd.dspMSG("completeInsert")

            Finally
                'マウスカーソル矢印
                Me.Cursor = cur
            End Try

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        Finally
            If _db.isTransactionOpen = True Then
                _db.rollbackTran()                          'ロールバック
            End If
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　戻るボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnModoru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModoru.Click

        '警告メッセージ
        If _changeFlg Then
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmDgvEdit")   '編集中の内容が破棄されます。よろしいですか？
            If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        '自画面を終了し、メニュー画面に戻る。
        _parentForm.Show()
        _parentForm.Activate()

        Me.Close()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　検索ボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnKensaku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensaku.Click

        Try
            '警告メッセージ
            If _changeFlg Then
                Dim rtn As DialogResult = _msgHd.dspMSG("confirmSrcEdit")   '編集中の内容が破棄されます。よろしいですか？
                If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If

            '変更フラグをリセットする
            _changeFlg = False

            'マウスカーソル砂時計
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor
            Try

                '検索条件の作成
                Dim sqlWhere As String = ""
                sqlWhere = createSerchStr()

                'トランザクション開始
                _db.beginTran()

                'ワークテーブルの作成
                Call delInsWK10(sqlWhere)
                Call createWK10()

                'トランザクション終了
                _db.commitTran()

                '一覧行着色フラグを無効にする
                _colorCtlFlg = False

                '一覧表示
                Call dispWK10()

                '再計算済みにする
                _saikeisanFlg = True

                '一覧行着色フラグを有効にする
                _colorCtlFlg = True

                'ロット数列の一番上のセルへフォーカスする
                setForcusCol(COLNO_LOTSUU, 0)

                '並べ替えラベルの表示初期化
                Call initLabel()

            Finally
                'マウスカーソル矢印
                Me.Cursor = cur
            End Try

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        Finally
            If _db.isTransactionOpen = True Then
                _db.rollbackTran()                          'ロールバック
            End If
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　表示単位変更オプションボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub rdoKm_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoKm.CheckedChanged
        Try

            '画面起動時は処理しない
            If _startForm Then
                Exit Sub
            End If

            '再計算ボタン押下時の処理を走らせる(表示切替も同時に走る)
            Call btnSaikeisan_Click(Nothing, Nothing)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　再計算ボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnSaikeisan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaikeisan.Click
        Try

            'マウスカーソル砂時計
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor
            Try

                '一覧のデータをワークテーブルに更新
                Call updateWK10()

                '計画数・計画量の再計算
                Call culcUpdateWK10()

                _colorCtlFlg = False

                '一覧再表示
                Call dispWK10()

                _colorCtlFlg = True

                'ロット数列の一番上のセルへフォーカスする
                setForcusCol(COLNO_LOTSUU, 0)

                '並べ替えラベルの表示初期化
                Call initLabel()

                '選択されている単位に従って画面表示
                Call changeDisp()

                '再計算済みにする
                _saikeisanFlg = True

            Finally
                'マウスカーソル矢印
                Me.Cursor = cur
            End Try

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        Finally
            If _db.isTransactionOpen = True Then
                _db.rollbackTran()                          'ロールバック
            End If
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　品種別集計表出力ボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnHinsyuPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHinsyuPrint.Click
        '-->2010.12.12 add by takagi
        Dim cur As Cursor = Me.Cursor
        Me.Cursor = Cursors.WaitCursor
        Try
            '<--2010.12.12 add by takagi

            '-->2010.12.27 add by takagi #高速化
            Dim pb As UtilProgresBarCancelable = New UtilProgresBarCancelable(Me)
            pb.Show()
            Try
                Application.DoEvents()
                pb.Cursor = Cursors.WaitCursor
                pb.Refresh()
                Application.DoEvents()

                'プログレスバー設定
                pb.jobName = "出力を準備しています。"
                pb.status = "データベース問合せ中．．．"
                '<--2010.12.27 add by takagi #高速化

                '一覧のデータをワークテーブルに更新
                Call updateWK10()

                '計画数・計画量の再計算
                Call culcUpdateWK10()

                pb.jobName = "出力しています。" '2010.12.27 add by takagi #高速化
                pb.status = ""                  '2010.12.27 add by takagi #高速化

                'EXCEL出力
                '-->2010.12.27 chg by takagi #高速化
                'Call printHinsyuExcel()
                Call printHinsyuExcel(pb)
                '<--2010.12.27 chg by takagi #高速化

                '-->2010.12.27 add by takagi #高速化
            Catch pbe As UtilProgressBarCancelEx
                'キャンセル押下→処理なし
            Finally
                '画面消去
                pb.Close()
            End Try
            '<--2010.12.27 add by takagi #高速化

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
            '-->2010.12.12 add by takagi
        Finally
            Me.Cursor = cur
            '<--2010.12.12 add by takagi
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　生産販売在庫計画出力ボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnSeisanPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeisanPrint.Click
        '-->2010.12.12 add by takagi
        Dim cur As Cursor = Me.Cursor
        Me.Cursor = Cursors.WaitCursor
        Try
            '<--2010.12.12 add by takagi

            '-->2010.12.27 add by takagi #高速化
            Dim pb As UtilProgresBarCancelable = New UtilProgresBarCancelable(Me)
            pb.Show()
            Try
                Application.DoEvents()
                pb.Cursor = Cursors.WaitCursor
                pb.Refresh()
                Application.DoEvents()

                'プログレスバー設定
                pb.jobName = "出力を準備しています。"
                pb.status = "データベース問合せ中．．．"
                '<--2010.12.27 add by takagi #高速化


                '一覧のデータをワークテーブルに更新
                Call updateWK10()

                '計画数・計画量の再計算
                Call culcUpdateWK10()

                pb.jobName = "出力しています。" '2010.12.27 add by takagi #高速化
                pb.status = ""                  '2010.12.27 add by takagi #高速化

                'EXCEL出力
                '-->2010.12.27 chg by takagi #高速化
                'Call printSeisanExcel()
                Call printSeisanExcel(pb)
                '<--2010.12.27 chg by takagi #高速化

                '-->2010.12.27 add by takagi #高速化
            Catch pbe As UtilProgressBarCancelEx
                'キャンセル押下→処理なし
            Finally
                '画面消去
                pb.Close()
            End Try
            '<--2010.12.27 add by takagi #高速化

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
            '-->2010.12.12 add by takagi
        Finally
            Me.Cursor = cur
            '<--2010.12.12 add by takagi
        End Try

    End Sub

#End Region

#Region "ユーザ定義関数:EXCEL関連"

    '------------------------------------------------------------------------------------------------------
    '　品種別集計表EXCEL出力処理
    '------------------------------------------------------------------------------------------------------
    '-->2010.12.27 chg by takagi #高速化
    'Private Sub printHinsyuExcel()
    Private Sub printHinsyuExcel(ByVal prmPb As UtilProgresBarCancelable)
        '<--2010.12.27 chg by takagi #高速化
        Try

            '雛形ファイル(品名別販売計画と同じ雛形)
            Dim openFilePath As String = StartUp.iniValue.BaseXlsPath & "\" & StartUp.iniValue.ExcelZG530R2_Base
            '雛形ファイルが開かれていないかチェック
            Dim fh As UtilFile = New UtilFile()
            Try
                fh.move(openFilePath, openFilePath & 1)
                fh.move(openFilePath & 1, openFilePath)
                '-->2010.12.27 add by takagi #高速化
            Catch pbe As UtilProgressBarCancelEx
                Throw pbe '上位転送
                '<--2010.12.27 add by takagi #高速化
            Catch ioe As System.IO.IOException
                Throw New UsrDefException("ファイルが開かれています。ファイルを閉じてから再実行してください。", _
                                          _msgHd.getMSG("fileOpenErr", "【ファイル名】：" & openFilePath))
            End Try

            '出力用ファイル
            'ファイル名取得-----------------------------------------------------
            Dim wkEditFile As String = StartUp.iniValue.OutXlsPath & "\" & StartUp.iniValue.ExcelZG530R2_Out     'コピー先ファイル

            'コピー先ファイルが存在する場合、コピー先ファイルを削除----------------
            If UtilClass.isFileExists(wkEditFile) Then
                Try
                    fh.delete(wkEditFile)
                    '-->2010.12.27 add by takagi #高速化
                Catch pbe As UtilProgressBarCancelEx
                    Throw pbe '上位転送
                    '<--2010.12.27 add by takagi #高速化
                Catch ioe As System.IO.IOException
                    Throw New UsrDefException("ファイルが開かれています。ファイルを閉じてから再実行してください。", _
                                              _msgHd.getMSG("fileOpenErr", "【ファイル名】：" & wkEditFile))
                End Try
            End If

            Try
                '出力用ファイルへ雛型ファイルコピー
                FileCopy(openFilePath, wkEditFile)
                '-->2010.12.27 add by takagi #高速化
            Catch pbe As UtilProgressBarCancelEx
                Throw pbe '上位転送
                '<--2010.12.27 add by takagi #高速化
            Catch ioe As System.IO.IOException
                Throw New UsrDefException(ioe, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ioe)))
            End Try

            Dim eh As xls.UtilExcelHandler = New xls.UtilExcelHandler(wkEditFile)
            Try
                eh.open()
                Try

                    '汎用マスタから需要先情報を取得
                    Dim sql As String = ""
                    sql = sql & N & " SELECT KAHENKEY, NAME1 FROM M01HANYO "
                    sql = sql & N & " WHERE KOTEIKEY = '" & KOTEI_JUYOU & "'"
                    'SQL発行
                    Dim iRecCnt As Integer          'データセットの行数
                    Dim dsHanyo As DataSet = _db.selectDB(sql, RS, iRecCnt)

                    If iRecCnt <= 0 Then                    'M01汎用マスタ抽出レコードが１件もない場合
                        Throw New UsrDefException("登録されていません。", _msgHd.getMSG("noData"))
                    End If

                    '需要先の数だけループ
                    For hanyoCnt As Integer = 0 To iRecCnt - 1
                        'ワークテーブルの値をデータセットに保持
                        Dim ds As DataSet = Nothing
                        Dim rowCnt As Integer = 0
                        '需要先ごとにワークのデータを抽出
                        Call getDataForHinsyuXls(_db.rmNullStr(dsHanyo.Tables(RS).Rows(hanyoCnt)(HANYOU_KAHENKEY)), ds, rowCnt)

                        If rowCnt > 0 Then

                            '■シート(雛形)を複製保存
                            Dim baseName As String = XLSSHEETNM_HINSYU  '雛形シート名
                            Dim newName As String = _db.rmNullStr(dsHanyo.Tables(RS).Rows(hanyoCnt)(HANYOU_NAME1))     '新たに作成するシート
                            Try
                                eh.targetSheet = baseName               '雛形シート選択
                                eh.copySheetOnLast(newName)
                                '-->2010.12.27 add by takagi #高速化
                            Catch pbe As UtilProgressBarCancelEx
                                Throw pbe '上位転送
                                '<--2010.12.27 add by takagi #高速化
                            Catch ex As Exception
                                Throw New UsrDefException("シートの複製に失敗しました。", _msgHd.getMSG("failCopySheet"))
                            End Try

                            '-->2010.12.27 chg by takagi #高速化
                            prmPb.status = newName & "出力中"
                            '<--2010.12.27 chg by takagi #高速化

                            eh.targetSheet = newName

                            Dim startPrintRow As Integer = START_PRINT          '出力開始行数

                            '合計保持用の変数
                            Dim zzZaiko As Double = 0           '前々月末在庫量
                            Dim zSeisanryou As Double = 0       '前月生産実績量
                            Dim zHanbairyou As Double = 0       '前月販売実績量
                            Dim zZaikoryou As Double = 0        '前月末在庫量
                            Dim tSeisanryou As Double = 0       '当月生産計画量
                            Dim tHanbairyou As Double = 0       '当月販売計画量
                            Dim tZaikoryou As Double = 0        '当月末在庫量
                            Dim kurikosiryou As Double = 0      '繰越量
                            Dim lot As Double = 0               'ロット数
                            Dim ySeisanryou As Double = 0       '翌月生産量
                            Dim yHanbairyou As Double = 0       '翌月販売量
                            Dim yZaikoryou As Double = 0        '翌月在庫量
                            Dim yyHanbairyou As Double = 0      '翌々月販売量
                            With ds.Tables(RS)
                                Dim i As Integer
                                prmPb.maxVal = rowCnt '2010.12.27 chg by takagi #高速化
                                For i = 0 To rowCnt - 1

                                    prmPb.value = i + 1 '2010.12.27 chg by takagi #高速化


                                    '列を1行追加
                                    eh.copyRow(startPrintRow + i)
                                    eh.insertPasteRow(startPrintRow + i)

                                    '一覧データ出力
                                    eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_HINSYUCD)), startPrintRow + i, XLSCOL_H_HINSYUCD)      '品種コード
                                    eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_HINSYUNM)), startPrintRow + i, XLSCOL_H_HINSYUNM)      '品種名
                                    eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ZZZAIKOR)), startPrintRow + i, XLSCOL_H_ZZZAIKO)       '前々月末在庫量
                                    eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ZSEISANR)), startPrintRow + i, XLSCOL_H_ZSEISAN)       '前月生産実績量
                                    eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ZHANBAIR)), startPrintRow + i, XLSCOL_H_ZHANBAI)       '前月販売実績量
                                    eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ZZAIKOR)), startPrintRow + i, XLSCOL_H_ZZAIKO)         '前月末在庫量
                                    eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_TSEISANR)), startPrintRow + i, XLSCOL_H_TSEISAN)       '当月生産計画量
                                    eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_THANBAIR)), startPrintRow + i, XLSCOL_H_THANBAI)       '当月販売計画量
                                    eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_TZAIKOR)), startPrintRow + i, XLSCOL_H_TZAIKO)         '当月末在庫量
                                    eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_KURIKOSIR)), startPrintRow + i, XLSCOL_H_KURIKOSI)     '繰越量
                                    eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_LOTSU)), startPrintRow + i, XLSCOL_H_LOT)              'ロット数
                                    eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YSEISANR)), startPrintRow + i, XLSCOL_H_YSEISAN)       '翌月生産量
                                    eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YHANBAIR)), startPrintRow + i, XLSCOL_H_YHANBAI)       '翌月販売量
                                    eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YZAIKOR)), startPrintRow + i, XLSCOL_H_YZAIKO)         '翌月在庫量
                                    eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YYHANBAIR)), startPrintRow + i, XLSCOL_H_YYHANBAI)     '翌々月販売量

                                    '合計値計算
                                    zzZaiko = zzZaiko + _db.rmNullDouble(.Rows(i)(COLDT_ZZZAIKOR))            '前々月末在庫量
                                    zSeisanryou = zSeisanryou + _db.rmNullDouble(.Rows(i)(COLDT_ZSEISANR))    '前月生産実績量
                                    zHanbairyou = zHanbairyou + _db.rmNullDouble(.Rows(i)(COLDT_ZHANBAIR))    '前月販売実績量
                                    zZaikoryou = zZaikoryou + _db.rmNullDouble(.Rows(i)(COLDT_ZZAIKOR))       '前月末在庫量
                                    tSeisanryou = tSeisanryou + _db.rmNullDouble(.Rows(i)(COLDT_TSEISANR))    '当月生産計画量
                                    tHanbairyou = tHanbairyou + _db.rmNullDouble(.Rows(i)(COLDT_THANBAIR))    '当月販売計画量
                                    tZaikoryou = tZaikoryou + _db.rmNullDouble(.Rows(i)(COLDT_TZAIKOR))       '当月末在庫量
                                    kurikosiryou = kurikosiryou + _db.rmNullDouble(.Rows(i)(COLDT_KURIKOSIR)) '繰越量
                                    lot = lot + _db.rmNullDouble(.Rows(i)(COLDT_LOTSU))                       'ロット数
                                    ySeisanryou = ySeisanryou + _db.rmNullDouble(.Rows(i)(COLDT_YSEISANR))    '翌月生産量
                                    yHanbairyou = yHanbairyou + _db.rmNullDouble(.Rows(i)(COLDT_YHANBAIR))    '翌月販売量
                                    yZaikoryou = yZaikoryou + _db.rmNullDouble(.Rows(i)(COLDT_YZAIKOR))       '翌月在庫量
                                    yyHanbairyou = yyHanbairyou + _db.rmNullDouble(.Rows(i)(COLDT_YYHANBAIR)) '翌々月販売量
                                Next

                                '余分な空行を削除
                                eh.deleteRow(startPrintRow + i)

                                '合計行表示
                                eh.setValue(zzZaiko, startPrintRow + i, XLSCOL_H_ZZZAIKO)         '前々月末在庫量
                                eh.setValue(zSeisanryou, startPrintRow + i, XLSCOL_H_ZSEISAN)     '前月生産実績量
                                eh.setValue(zHanbairyou, startPrintRow + i, XLSCOL_H_ZHANBAI)     '前月販売実績量
                                eh.setValue(zZaikoryou, startPrintRow + i, XLSCOL_H_ZZAIKO)       '前月末在庫量
                                eh.setValue(tSeisanryou, startPrintRow + i, XLSCOL_H_TSEISAN)     '当月生産計画量
                                eh.setValue(tHanbairyou, startPrintRow + i, XLSCOL_H_THANBAI)     '当月販売計画量
                                eh.setValue(tZaikoryou, startPrintRow + i, XLSCOL_H_TZAIKO)       '当月末在庫量
                                eh.setValue(kurikosiryou, startPrintRow + i, XLSCOL_H_KURIKOSI)   '繰越量
                                eh.setValue(lot, startPrintRow + i, XLSCOL_H_LOT)                 'ロット数
                                eh.setValue(ySeisanryou, startPrintRow + i, XLSCOL_H_YSEISAN)     '翌月生産量
                                eh.setValue(yHanbairyou, startPrintRow + i, XLSCOL_H_YHANBAI)     '翌月販売量
                                eh.setValue(yZaikoryou, startPrintRow + i, XLSCOL_H_YZAIKO)       '翌月在庫量
                                eh.setValue(yyHanbairyou, startPrintRow + i, XLSCOL_H_YYHANBAI)   '翌々月販売量
                            End With
                            '作成日時編集
                            Dim printDate As String = Now.ToString("yyyy/MM/dd HH:mm")
                            eh.setValue("作成日時 ： " & printDate, 1, 15)   'O1

                            '需要品名編集
                            eh.setValue("(" & _db.rmNullStr(dsHanyo.Tables(RS).Rows(hanyoCnt)(HANYOU_NAME1)) & ")", 2, 3)      'C2

                            '処理年月、計画年月編集
                            eh.setValue("処理年月：" & lblSyori.Text & "　　計画年月：" & lblKeikaku.Text, 2, 6)    'F2

                            '検索条件
                            eh.setValue("需要先：" & _serchJuyo, 4, 1)        'A4

                            'ヘッダーの年月編集
                            eh.setValue(lblZengetu.Text, 7, 5)      'E7　前月
                            eh.setValue(lblTogetu.Text, 7, 8)       'H7　当月
                            eh.setValue(lblYokugetu.Text, 7, 12)    'L7　翌月
                            eh.setValue(lblYygetu.Text, 7, 15)      'O7　翌々月
                            '前々月の月を計算
                            '-->2011.01.15 chg by takagi #69
                            'Dim zzgetu As Integer = CInt(lblSyori.Text.Substring(5, 2)) - 2
                            'If zzgetu = 0 Then
                            '    zzgetu = 12
                            'End If
                            Dim zzgetu As Integer = Format(DateAdd(DateInterval.Month, -2, CDate(lblSyori.Text & "/01")), "MM")
                            '<--2011.01.15 chg by takagi #69
                            eh.setValue(zzgetu & "月", 7, 3)               'C7　前々月

                            '左上のセルにフォーカス当てる
                            eh.selectCell(START_PRINT, XLSCOL_H_HINSYUCD)     'A10

                            Clipboard.Clear()       'クリップボードの初期化
                        End If
                    Next
                    eh.deleteSheet("Ver1.0.00")

                    '' 2011/01/20 add start sugano
                    '先頭シートを選択
                    eh.targetSheetByIdx = 1
                    eh.selectSheet(eh.targetSheet)
                    eh.selectCell(1, 1)
                    '' 2011/01/20 add end sugano

                Finally
                    eh.close()
                End Try

                'EXCELファイル開く
                eh.display()

                '-->2010.12.27 add by takagi #高速化
            Catch pbe As UtilProgressBarCancelEx
                Throw pbe '上位転送
                '<--2010.12.27 add by takagi #高速化
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

            '-->2010.12.27 add by takagi #高速化
        Catch pbe As UtilProgressBarCancelEx
            Throw pbe '上位転送
            '<--2010.12.27 add by takagi #高速化
        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　生産販売在庫計画EXCEL出力処理
    '------------------------------------------------------------------------------------------------------
    '-->2010.12.27 chg by takagi #高速化
    'Private Sub printSeisanExcel()
    Private Sub printSeisanExcel(ByVal prmPb As UtilProgresBarCancelable)
        '<--2010.12.27 chg by takagi #高速化
        Try


            '雛形ファイル(品名別販売計画と同じ雛形)
            Dim openFilePath As String = StartUp.iniValue.BaseXlsPath & "\" & StartUp.iniValue.ExcelZG530R1_Base
            '雛形ファイルが開かれていないかチェック
            Dim fh As UtilFile = New UtilFile()
            Try
                fh.move(openFilePath, openFilePath & 1)
                fh.move(openFilePath & 1, openFilePath)

                '-->2010.12.27 add by takagi #高速化
            Catch pbe As UtilProgressBarCancelEx
                Throw pbe '上位転送
                '<--2010.12.27 add by takagi #高速化
            Catch ioe As System.IO.IOException
                Throw New UsrDefException("ファイルが開かれています。ファイルを閉じてから再実行してください。", _
                                          _msgHd.getMSG("fileOpenErr", "【ファイル名】：" & openFilePath))
            End Try

            '出力用ファイル
            'ファイル名取得-----------------------------------------------------
            Dim wkEditFile As String = StartUp.iniValue.OutXlsPath & "\" & StartUp.iniValue.ExcelZG530R1_Out     'コピー先ファイル

            'コピー先ファイルが存在する場合、コピー先ファイルを削除----------------
            If UtilClass.isFileExists(wkEditFile) Then
                Try
                    fh.delete(wkEditFile)
                    '-->2010.12.27 add by takagi #高速化
                Catch pbe As UtilProgressBarCancelEx
                    Throw pbe '上位転送
                    '<--2010.12.27 add by takagi #高速化
                Catch ioe As System.IO.IOException
                    Throw New UsrDefException("ファイルが開かれています。ファイルを閉じてから再実行してください。", _
                                              _msgHd.getMSG("fileOpenErr", "【ファイル名】：" & wkEditFile))
                End Try
            End If

            Try
                '出力用ファイルへ雛型ファイルコピー
                FileCopy(openFilePath, wkEditFile)
                '-->2010.12.27 add by takagi #高速化
            Catch pbe As UtilProgressBarCancelEx
                Throw pbe '上位転送
                '<--2010.12.27 add by takagi #高速化
            Catch ioe As System.IO.IOException
                Throw New UsrDefException(ioe, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ioe)))
            End Try

            Dim eh As xls.UtilExcelHandler = New xls.UtilExcelHandler(wkEditFile)
            Try
                eh.open()
                Try

                    '汎用マスタから需要先情報を取得
                    Dim sql As String = ""
                    sql = sql & N & " SELECT KAHENKEY, NAME1 FROM M01HANYO "
                    sql = sql & N & " WHERE KOTEIKEY = '" & KOTEI_JUYOU & "'"
                    'SQL発行
                    Dim iRecCnt As Integer          'データセットの行数
                    Dim dsHanyo As DataSet = _db.selectDB(sql, RS, iRecCnt)

                    If iRecCnt <= 0 Then                    'M01汎用マスタ抽出レコードが１件もない場合
                        Throw New UsrDefException("登録されていません。", _msgHd.getMSG("noData"))
                    End If

                    '需要先の数だけループ
                    For hanyoCnt As Integer = 0 To iRecCnt - 1
                        'ワークテーブルの値をデータセットに保持
                        Dim ds As DataSet = Nothing
                        Dim rowCnt As Integer = 0
                        '需要先ごとにワークのデータを抽出
                        Call getDataForSeisanXls(_db.rmNullStr(dsHanyo.Tables(RS).Rows(hanyoCnt)(HANYOU_KAHENKEY)), ds, rowCnt)

                        If rowCnt > 0 Then

                            '■シート(雛形)を複製保存
                            Dim baseName As String = XLSSHEETNM_HINSYU  '雛形シート名
                            Dim newName As String = _db.rmNullStr(dsHanyo.Tables(RS).Rows(hanyoCnt)(HANYOU_NAME1))     '新たに作成するシート
                            Try
                                eh.targetSheet = baseName               '雛形シート選択
                                eh.copySheetOnLast(newName)
                                '-->2010.12.27 add by takagi #高速化
                            Catch pbe As UtilProgressBarCancelEx
                                Throw pbe '上位転送
                                '<--2010.12.27 add by takagi #高速化
                            Catch ex As Exception
                                Throw New UsrDefException("シートの複製に失敗しました。", _msgHd.getMSG("failCopySheet"))
                            End Try

                            '-->2010.12.27 chg by takagi #高速化
                            prmPb.status = newName & "出力中"
                            '<--2010.12.27 chg by takagi #高速化

                            eh.targetSheet = newName

                            Dim startPrintRow As Integer = START_PRINT          '出力開始行数

                            '最初の品種コードを持つ
                            Dim startHinsyu As String = _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_HINSYUCD))

                            Dim startTotalRow As Integer = startPrintRow    '「品種計」出力するために合計する最初の列数
                            Dim totalRow As Integer = 0         '「品種計」の列を出力した回数

                            '掛線設定用
                            Dim lineV As LineVO = New LineVO()

                            Dim i As Integer                    'ループカウンター
                            Dim xlsRow As Integer = startPrintRow + i + totalRow

                            '-->2011.03.23 add by takagi #在庫月数は当月在庫÷翌月販売で求める
                            Dim existsMetsukeFlg As Boolean = False
                            '<--2011.03.23 add by takagi #在庫月数は当月在庫÷翌月販売で求める

                            '-->2010.12.27 add by takagi #高速化
                            Dim buf As System.Text.StringBuilder = New System.Text.StringBuilder
                            With ds.Tables(RS)
                                prmPb.maxVal = rowCnt
                                '<--2010.12.27 add by takagi #高速化

                                For i = 0 To rowCnt - 1
                                    xlsRow = startPrintRow + i + totalRow
                                    'With ds.Tables(RS)         '2010.12.27 del by takagi #高速化
                                    If startHinsyu.Equals(_db.rmNullStr(ds.Tables(RS).Rows(i)(COLDT_HINSYUCD))) Then

                                        '行を1行追加
                                        eh.copyRow(xlsRow)
                                        eh.insertPasteRow(xlsRow)
                                        '一覧データ出力
                                        '-->2010.12.27 chg by takagi #高速化
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_SIYOCD)), xlsRow, XLSCOL_SIYOUCD)        '仕様コード                                    
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_HINMEI)), xlsRow, XLSCOL_HINMEI)         '品種名
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_LOTTYO)), xlsRow, XLSCOL_LOTTYO)         'ロット長
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ABC)), xlsRow, XLSCOL_ABC)               'ABC
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ZZZAIKOS)), xlsRow, XLSCOL_ZZZAIKOS)     '前々月在庫数
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ZSEISANS)), xlsRow, XLSCOL_ZSEISANS)     '前月生産実績数
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ZHANBAIS)), xlsRow, XLSCOL_ZHANBAIS)     '前月販売実績数
                                        'eh.setValue("=(" & XLSALP_ZZZAIKOS & CStr(xlsRow) & " + " & XLSALP_ZSEISANS & CStr(xlsRow) & " - " & XLSALP_ZHANBAIS & CStr(xlsRow) & ")", xlsRow, XLSCOL_ZZAIKOS)  '前月末在庫数
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ZZAIKOR)), xlsRow, XLSCOL_ZZAIKOR)       '前月末在庫量
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_TSEISANS)), xlsRow, XLSCOL_TSEISANS)     '当月生産計画数
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_TSEISANR)), xlsRow, XLSCOL_TSEISANR)     '当月生産計画量
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_THANBAIS)), xlsRow, XLSCOL_THANBAIS)     '当月販売計画数
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_THANBAIR)), xlsRow, XLSCOL_THANBAIR)     '当月販売計画量
                                        'eh.setValue("=(" & XLSALP_ZZAIKOS & CStr(xlsRow) & " + " & XLSALP_TSEISANS & CStr(xlsRow) & " - " & XLSALP_THANBAIS & CStr(xlsRow) & ")", xlsRow, XLSCOL_TZAIKOS)   '当月末在庫数
                                        'eh.setValue("=(" & XLSALP_ZZAIKOR & CStr(xlsRow) & " + " & XLSALP_TSEISANR & CStr(xlsRow) & " - " & XLSALP_THANBAIR & CStr(xlsRow) & ")", xlsRow, XLSCOL_TZAIKOR)   '当月末在庫量
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_KURIKOSIS)), xlsRow, XLSCOL_KURIKOSIS)   '繰越数
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_LOTSU)), xlsRow, XLSCOL_LOTSU)           'ロット数
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YSEISANS)), xlsRow, XLSCOL_YSEISANS)     '翌月生産計画数
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YSEISANR)), xlsRow, XLSCOL_YSEISANR)     '翌月生産計画量
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YHANBAIS)), xlsRow, XLSCOL_YHANBAIS)     '翌月販売計画数
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YHANBAIR)), xlsRow, XLSCOL_YHANBAIR)     '翌月販売計画量
                                        'eh.setValue("=(" & XLSALP_TZAIKOS & CStr(xlsRow) & " + " & XLSALP_YSEISANS & CStr(xlsRow) & " - " & XLSALP_YHANBAIS & CStr(xlsRow) & ")", xlsRow, XLSCOL_YZAIKOS)   '翌月末在庫数
                                        'eh.setValue("=(" & XLSALP_TZAIKOR & CStr(xlsRow) & " + " & XLSALP_YSEISANR & CStr(xlsRow) & " - " & XLSALP_YHANBAIR & CStr(xlsRow) & ")", xlsRow, XLSCOL_YZAIKOR)   '翌月末在庫量
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ZAIKOTUKISU)), xlsRow, XLSCOL_YTUKISU)   '在庫月数
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YYHANBAIS)), xlsRow, XLSCOL_YYHANBAIS)   '翌々月販売計画数
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YYHANBAIR)), xlsRow, XLSCOL_YYHANBAIR)   '翌々月販売計画量
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_KTUKISU)), xlsRow, XLSCOL_KIJUNTUKISU)   '基準月数
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_FZAIKOS)), xlsRow, XLSCOL_FUKKYUYO)      '復旧用在庫
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_AZAIKOS)), xlsRow, XLSCOL_ANZEN)         '安全在庫
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_METUKE)), xlsRow, XLSCOL_METUKE)         '目付
                                        prmPb.value = i + 1
                                        buf.Remove(0, buf.Length)
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_SIYOCD)) & ControlChars.Tab)       '仕様コード                                    
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_HINMEI)) & ControlChars.Tab)       '品種名
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_LOTTYO)) & ControlChars.Tab)       'ロット長
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_ABC)) & ControlChars.Tab)          'ABC
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_ZZZAIKOS)) & ControlChars.Tab)     '前々月在庫数
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_ZSEISANS)) & ControlChars.Tab)     '前月生産実績数
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_ZHANBAIS)) & ControlChars.Tab)     '前月販売実績数
                                        buf.Append("=(" & XLSALP_ZZZAIKOS & CStr(xlsRow) & " + " & XLSALP_ZSEISANS & CStr(xlsRow) & " - " & XLSALP_ZHANBAIS & CStr(xlsRow) & ")" & ControlChars.Tab)  '前月末在庫数
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_TSEISANS)) & ControlChars.Tab)     '当月生産計画数
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_TSEISANR)) & ControlChars.Tab)     '当月生産計画量
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_THANBAIS)) & ControlChars.Tab)     '当月販売計画数
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_THANBAIR)) & ControlChars.Tab)     '当月販売計画量
                                        buf.Append("=(" & XLSALP_ZZAIKOS & CStr(xlsRow) & " + " & XLSALP_TSEISANS & CStr(xlsRow) & " - " & XLSALP_THANBAIS & CStr(xlsRow) & ")" & ControlChars.Tab)   '当月末在庫数
                                        buf.Append("=(" & XLSALP_ZZAIKOR & CStr(xlsRow) & " + " & XLSALP_TSEISANR & CStr(xlsRow) & " - " & XLSALP_THANBAIR & CStr(xlsRow) & ")" & ControlChars.Tab)   '当月末在庫量
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_KURIKOSIS)) & ControlChars.Tab)    '繰越数
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_LOTSU)) & ControlChars.Tab)        'ロット数
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_YSEISANS)) & ControlChars.Tab)     '翌月生産計画数
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_YSEISANR)) & ControlChars.Tab)     '翌月生産計画量
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_YHANBAIS)) & ControlChars.Tab)     '翌月販売計画数
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_YHANBAIR)) & ControlChars.Tab)     '翌月販売計画量
                                        buf.Append("=(" & XLSALP_TZAIKOS & CStr(xlsRow) & " + " & XLSALP_YSEISANS & CStr(xlsRow) & " - " & XLSALP_YHANBAIS & CStr(xlsRow) & ")" & ControlChars.Tab)   '翌月末在庫数
                                        buf.Append("=(" & XLSALP_TZAIKOR & CStr(xlsRow) & " + " & XLSALP_YSEISANR & CStr(xlsRow) & " - " & XLSALP_YHANBAIR & CStr(xlsRow) & ")" & ControlChars.Tab)   '翌月末在庫量
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_ZAIKOTUKISU)) & ControlChars.Tab)  '在庫月数
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_YYHANBAIS)) & ControlChars.Tab)    '翌々月販売計画数
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_YYHANBAIR)) & ControlChars.Tab)    '翌々月販売計画量
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_KTUKISU)) & ControlChars.Tab)      '基準月数
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_FZAIKOS)) & ControlChars.Tab)      '復旧用在庫
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_AZAIKOS)) & ControlChars.Tab)      '安全在庫
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_METUKE)) & ControlChars.Tab)       '目付
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_ZZAIKOR)) & ControlChars.Tab)      '前月末在庫量
                                        Clipboard.SetText(buf.ToString)
                                        eh.paste(xlsRow, XLSCOL_SIYOUCD)
                                        Clipboard.Clear()
                                        '<--2010.12.27 chg by takagi #高速化

                                        '-->2011.03.23 add by takagi #在庫月数は当月在庫÷翌月販売で求める
                                        '目付が存在すればフラグを立てる
                                        If "".Equals(_db.rmNullStr(.Rows(i)(COLDT_METUKE))) Or _
                                           _db.rmNullDouble(.Rows(i)(COLDT_METUKE)) = 0 Then
                                            existsMetsukeFlg = False
                                        Else
                                            existsMetsukeFlg = True
                                        End If
                                        '<--2011.03.23 add by takagi #在庫月数は当月在庫÷翌月販売で求める
                                    Else

                                        '行を1行追加
                                        eh.copyRow(xlsRow)
                                        eh.insertPasteRow(xlsRow)

                                        '品種計行出力
                                        '-->2010.12.27 chg by takagi #高速化
                                        'eh.setValue("品 種 計", xlsRow, XLSCOL_HINMEI)
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_ZZZAIKOS & startTotalRow & ":" & XLSALP_ZZZAIKOS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_ZZZAIKOS)     '前々月末在庫数
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_ZSEISANS & startTotalRow & ":" & XLSALP_ZSEISANS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_ZSEISANS)     '前月生産実績数
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_ZHANBAIS & startTotalRow & ":" & XLSALP_ZHANBAIS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_ZHANBAIS)     '前月販売実績数
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_ZZAIKOS & startTotalRow & ":" & XLSALP_ZZAIKOS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_ZZAIKOS)        '前月末在庫数
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_TSEISANS & startTotalRow & ":" & XLSALP_TSEISANS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_TSEISANS)     '当月生産計画数
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_TSEISANR & startTotalRow & ":" & XLSALP_TSEISANR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_TSEISANR)     '当月生産計画量
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_THANBAIS & startTotalRow & ":" & XLSALP_THANBAIS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_THANBAIS)     '当月販売計画数
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_THANBAIR & startTotalRow & ":" & XLSALP_THANBAIR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_THANBAIR)     '当月販売計画量
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_TZAIKOS & startTotalRow & ":" & XLSALP_TZAIKOS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_TZAIKOS)        '当月末在庫数
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_TZAIKOR & startTotalRow & ":" & XLSALP_TZAIKOR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_TZAIKOR)        '当月末在庫量
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_KURIKOSIS & startTotalRow & ":" & XLSALP_KURIKOSIS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_KURIKOSIS)  '繰越数
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_LOTSU & startTotalRow & ":" & XLSALP_LOTSU & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_LOTSU)              'ロット数
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_YSEISANS & startTotalRow & ":" & XLSALP_YSEISANS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YSEISANS)     '翌月生産計画数
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_YSEISANR & startTotalRow & ":" & XLSALP_YSEISANR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YSEISANR)     '翌月生産計画量
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_YHANBAIS & startTotalRow & ":" & XLSALP_YHANBAIS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YHANBAIS)     '翌月販売計画数
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_YHANBAIR & startTotalRow & ":" & XLSALP_YHANBAIR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YHANBAIR)     '翌月販売計画量
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_YZAIKOS & startTotalRow & ":" & XLSALP_YZAIKOS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YZAIKOS)        '翌月末在庫数
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_YZAIKOR & startTotalRow & ":" & XLSALP_YZAIKOR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YZAIKOR)        '翌月末在庫量
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_YTUKISU & startTotalRow & ":" & XLSALP_YTUKISU & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YTUKISU)        '月数
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_YYHANBAIS & startTotalRow & ":" & XLSALP_YYHANBAIS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YYHANBAIS)  '翌々月販売計画数
                                        'eh.setValue("=SUBTOTAL(9," & XLSALP_YYHANBAIR & startTotalRow & ":" & XLSALP_YYHANBAIR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YYHANBAIR)  '翌々月販売計画量
                                        buf.Remove(0, buf.Length)
                                        buf.Append("品 種 計" & ControlChars.Tab & ControlChars.Tab & ControlChars.Tab)
                                        buf.Append("=SUBTOTAL(9," & XLSALP_ZZZAIKOS & startTotalRow & ":" & XLSALP_ZZZAIKOS & CStr(xlsRow - 1) & ")" & ControlChars.Tab)     '前々月末在庫数
                                        buf.Append("=SUBTOTAL(9," & XLSALP_ZSEISANS & startTotalRow & ":" & XLSALP_ZSEISANS & CStr(xlsRow - 1) & ")" & ControlChars.Tab)     '前月生産実績数
                                        buf.Append("=SUBTOTAL(9," & XLSALP_ZHANBAIS & startTotalRow & ":" & XLSALP_ZHANBAIS & CStr(xlsRow - 1) & ")" & ControlChars.Tab)     '前月販売実績数
                                        buf.Append("=SUBTOTAL(9," & XLSALP_ZZAIKOS & startTotalRow & ":" & XLSALP_ZZAIKOS & CStr(xlsRow - 1) & ")" & ControlChars.Tab)        '前月末在庫数
                                        buf.Append("=SUBTOTAL(9," & XLSALP_TSEISANS & startTotalRow & ":" & XLSALP_TSEISANS & CStr(xlsRow - 1) & ")" & ControlChars.Tab)     '当月生産計画数
                                        buf.Append("=SUBTOTAL(9," & XLSALP_TSEISANR & startTotalRow & ":" & XLSALP_TSEISANR & CStr(xlsRow - 1) & ")" & ControlChars.Tab)     '当月生産計画量
                                        buf.Append("=SUBTOTAL(9," & XLSALP_THANBAIS & startTotalRow & ":" & XLSALP_THANBAIS & CStr(xlsRow - 1) & ")" & ControlChars.Tab)     '当月販売計画数
                                        buf.Append("=SUBTOTAL(9," & XLSALP_THANBAIR & startTotalRow & ":" & XLSALP_THANBAIR & CStr(xlsRow - 1) & ")" & ControlChars.Tab)     '当月販売計画量
                                        buf.Append("=SUBTOTAL(9," & XLSALP_TZAIKOS & startTotalRow & ":" & XLSALP_TZAIKOS & CStr(xlsRow - 1) & ")" & ControlChars.Tab)        '当月末在庫数
                                        buf.Append("=SUBTOTAL(9," & XLSALP_TZAIKOR & startTotalRow & ":" & XLSALP_TZAIKOR & CStr(xlsRow - 1) & ")" & ControlChars.Tab)        '当月末在庫量
                                        buf.Append("=SUBTOTAL(9," & XLSALP_KURIKOSIS & startTotalRow & ":" & XLSALP_KURIKOSIS & CStr(xlsRow - 1) & ")" & ControlChars.Tab)  '繰越数
                                        buf.Append("=SUBTOTAL(9," & XLSALP_LOTSU & startTotalRow & ":" & XLSALP_LOTSU & CStr(xlsRow - 1) & ")" & ControlChars.Tab)              'ロット数
                                        buf.Append("=SUBTOTAL(9," & XLSALP_YSEISANS & startTotalRow & ":" & XLSALP_YSEISANS & CStr(xlsRow - 1) & ")" & ControlChars.Tab)     '翌月生産計画数
                                        buf.Append("=SUBTOTAL(9," & XLSALP_YSEISANR & startTotalRow & ":" & XLSALP_YSEISANR & CStr(xlsRow - 1) & ")" & ControlChars.Tab)     '翌月生産計画量
                                        buf.Append("=SUBTOTAL(9," & XLSALP_YHANBAIS & startTotalRow & ":" & XLSALP_YHANBAIS & CStr(xlsRow - 1) & ")" & ControlChars.Tab)     '翌月販売計画数
                                        buf.Append("=SUBTOTAL(9," & XLSALP_YHANBAIR & startTotalRow & ":" & XLSALP_YHANBAIR & CStr(xlsRow - 1) & ")" & ControlChars.Tab)     '翌月販売計画量
                                        buf.Append("=SUBTOTAL(9," & XLSALP_YZAIKOS & startTotalRow & ":" & XLSALP_YZAIKOS & CStr(xlsRow - 1) & ")" & ControlChars.Tab)        '翌月末在庫数
                                        buf.Append("=SUBTOTAL(9," & XLSALP_YZAIKOR & startTotalRow & ":" & XLSALP_YZAIKOR & CStr(xlsRow - 1) & ")" & ControlChars.Tab)        '翌月末在庫量
                                        '-->2011.03.23 chg by takagi #在庫月数は当月在庫÷翌月販売で求める
                                        'buf.Append("=SUBTOTAL(9," & XLSALP_YTUKISU & startTotalRow & ":" & XLSALP_YTUKISU & CStr(xlsRow - 1) & ")" & ControlChars.Tab)        '月数
                                        If existsMetsukeFlg Then
                                            '目付あり→重量(t)
                                            buf.Append("=" & XLSALP_YZAIKOR & CStr(xlsRow) & "/" & XLSALP_YYHANBAIR & CStr(xlsRow) & ControlChars.Tab)                         '月数(t)
                                        Else
                                            '目付なし→重量(Km)
                                            buf.Append("=" & XLSALP_YZAIKOS & CStr(xlsRow) & "/" & XLSALP_YYHANBAIS & CStr(xlsRow) & ControlChars.Tab)                         '月数(Km)
                                        End If
                                        '<--2011.03.23 chg by takagi #在庫月数は当月在庫÷翌月販売で求める
                                        buf.Append("=SUBTOTAL(9," & XLSALP_YYHANBAIS & startTotalRow & ":" & XLSALP_YYHANBAIS & CStr(xlsRow - 1) & ")" & ControlChars.Tab)  '翌々月販売計画数
                                        buf.Append("=SUBTOTAL(9," & XLSALP_YYHANBAIR & startTotalRow & ":" & XLSALP_YYHANBAIR & CStr(xlsRow - 1) & ")" & ControlChars.Tab)  '翌々月販売計画量
                                        Clipboard.SetText(buf.ToString)
                                        eh.paste(xlsRow, XLSCOL_HINMEI)
                                        Clipboard.Clear()
                                        '<--2010.12.27 chg by takagi #高速化

                                        '罫線を再設定
                                        lineV.Bottom = LineVO.LineType.BrokenL
                                        lineV.Top = LineVO.LineType.BrokenL
                                        eh.drawRuledLine(lineV, xlsRow, 1, , 29)

                                        startTotalRow = xlsRow + 1

                                        '次の品種コードを保持
                                        startHinsyu = _db.rmNullStr(ds.Tables(RS).Rows(i)(COLDT_HINSYUCD))

                                        '出力する行を次の行に移す
                                        totalRow = totalRow + 1
                                        xlsRow = startPrintRow + i + totalRow

                                        '行を1行追加
                                        eh.copyRow(xlsRow)
                                        eh.insertPasteRow(xlsRow)
                                        '一覧データ出力
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_SIYOCD)), xlsRow, XLSCOL_SIYOUCD)        '品種コード
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_HINMEI)), xlsRow, XLSCOL_HINMEI)         '品種名
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_LOTTYO)), xlsRow, XLSCOL_LOTTYO)         '前々月末在庫量
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ABC)), xlsRow, XLSCOL_ABC)               '前月生産実績量
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ZZZAIKOS)), xlsRow, XLSCOL_ZZZAIKOS)     '前月販売実績量
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ZSEISANS)), xlsRow, XLSCOL_ZSEISANS)     '前月末在庫量
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ZHANBAIS)), xlsRow, XLSCOL_ZHANBAIS)     '当月生産計画量
                                        'eh.setValue("=(" & XLSALP_ZZZAIKOS & CStr(xlsRow) & " + " & XLSALP_ZSEISANS & CStr(xlsRow) & " - " & XLSALP_ZHANBAIS & CStr(xlsRow) & ")", xlsRow, XLSCOL_ZZAIKOS)
                                        ''-->2010.12.27 add by takagi #50
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ZZAIKOR)), xlsRow, XLSCOL_ZZAIKOR)       '前月末在庫量
                                        ''<--2010.12.27 add by takagi #50
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_TSEISANS)), xlsRow, XLSCOL_TSEISANS)     '当月販売計画量
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_TSEISANR)), xlsRow, XLSCOL_TSEISANR)     '当月末在庫量
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_THANBAIS)), xlsRow, XLSCOL_THANBAIS)     '繰越量
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_THANBAIR)), xlsRow, XLSCOL_THANBAIR)     'ロット数
                                        'eh.setValue("=(" & XLSALP_ZZAIKOS & CStr(xlsRow) & " + " & XLSALP_TSEISANS & CStr(xlsRow) & " - " & XLSALP_THANBAIS & CStr(xlsRow) & ")", xlsRow, XLSCOL_TZAIKOS)
                                        'eh.setValue("=(" & XLSALP_ZZAIKOR & CStr(xlsRow) & " + " & XLSALP_TSEISANR & CStr(xlsRow) & " - " & XLSALP_THANBAIR & CStr(xlsRow) & ")", xlsRow, XLSCOL_TZAIKOR)   '当月末在庫量
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_KURIKOSIS)), xlsRow, XLSCOL_KURIKOSIS)   '翌月生産量
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_LOTSU)), xlsRow, XLSCOL_LOTSU)           '翌月販売量
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YSEISANS)), xlsRow, XLSCOL_YSEISANS)     '翌月在庫量
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YSEISANR)), xlsRow, XLSCOL_YSEISANR)     '翌々月販売量
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YHANBAIS)), xlsRow, XLSCOL_YHANBAIS)     '翌月販売計画数
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YHANBAIR)), xlsRow, XLSCOL_YHANBAIR)     '翌月販売計画量
                                        'eh.setValue("=(" & XLSALP_TZAIKOS & CStr(xlsRow) & " + " & XLSALP_YSEISANS & CStr(xlsRow) & " - " & XLSALP_YHANBAIS & CStr(xlsRow) & ")", xlsRow, XLSCOL_YZAIKOS)   '翌月末在庫数
                                        'eh.setValue("=(" & XLSALP_TZAIKOR & CStr(xlsRow) & " + " & XLSALP_YSEISANR & CStr(xlsRow) & " - " & XLSALP_YHANBAIR & CStr(xlsRow) & ")", xlsRow, XLSCOL_YZAIKOR)   '翌月末在庫量
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_ZAIKOTUKISU)), xlsRow, XLSCOL_YTUKISU)   '月数
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YYHANBAIS)), xlsRow, XLSCOL_YYHANBAIS)   '翌々月販売計画数
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_YYHANBAIR)), xlsRow, XLSCOL_YYHANBAIR)   '翌々月販売計画量
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_KTUKISU)), xlsRow, XLSCOL_KIJUNTUKISU)   '基準月数
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_FZAIKOS)), xlsRow, XLSCOL_FUKKYUYO)      '復旧用在庫
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_AZAIKOS)), xlsRow, XLSCOL_ANZEN)         '安全在庫
                                        'eh.setValue(_db.rmNullStr(.Rows(i)(COLDT_METUKE)), xlsRow, XLSCOL_METUKE)         '目付
                                        prmPb.value = i + 1
                                        buf.Remove(0, buf.Length)
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_SIYOCD)) & ControlChars.Tab)       '品種コード
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_HINMEI)) & ControlChars.Tab)       '品種名
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_LOTTYO)) & ControlChars.Tab)       '前々月末在庫量
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_ABC)) & ControlChars.Tab)          '前月生産実績量
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_ZZZAIKOS)) & ControlChars.Tab)     '前月販売実績量
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_ZSEISANS)) & ControlChars.Tab)     '前月末在庫量
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_ZHANBAIS)) & ControlChars.Tab)     '当月生産計画量
                                        buf.Append("=(" & XLSALP_ZZZAIKOS & CStr(xlsRow) & " + " & XLSALP_ZSEISANS & CStr(xlsRow) & " - " & XLSALP_ZHANBAIS & CStr(xlsRow) & ")" & ControlChars.Tab)
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_TSEISANS)) & ControlChars.Tab)     '当月販売計画量
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_TSEISANR)) & ControlChars.Tab)     '当月末在庫量
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_THANBAIS)) & ControlChars.Tab)     '繰越量
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_THANBAIR)) & ControlChars.Tab)     'ロット数
                                        buf.Append("=(" & XLSALP_ZZAIKOS & CStr(xlsRow) & " + " & XLSALP_TSEISANS & CStr(xlsRow) & " - " & XLSALP_THANBAIS & CStr(xlsRow) & ")" & ControlChars.Tab)
                                        buf.Append("=(" & XLSALP_ZZAIKOR & CStr(xlsRow) & " + " & XLSALP_TSEISANR & CStr(xlsRow) & " - " & XLSALP_THANBAIR & CStr(xlsRow) & ")" & ControlChars.Tab)   '当月末在庫量
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_KURIKOSIS)) & ControlChars.Tab)    '翌月生産量
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_LOTSU)) & ControlChars.Tab)        '翌月販売量
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_YSEISANS)) & ControlChars.Tab)     '翌月在庫量
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_YSEISANR)) & ControlChars.Tab)     '翌々月販売量
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_YHANBAIS)) & ControlChars.Tab)     '翌月販売計画数
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_YHANBAIR)) & ControlChars.Tab)     '翌月販売計画量
                                        buf.Append("=(" & XLSALP_TZAIKOS & CStr(xlsRow) & " + " & XLSALP_YSEISANS & CStr(xlsRow) & " - " & XLSALP_YHANBAIS & CStr(xlsRow) & ")" & ControlChars.Tab)   '翌月末在庫数
                                        buf.Append("=(" & XLSALP_TZAIKOR & CStr(xlsRow) & " + " & XLSALP_YSEISANR & CStr(xlsRow) & " - " & XLSALP_YHANBAIR & CStr(xlsRow) & ")" & ControlChars.Tab)   '翌月末在庫量
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_ZAIKOTUKISU)) & ControlChars.Tab)  '月数
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_YYHANBAIS)) & ControlChars.Tab)    '翌々月販売計画数
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_YYHANBAIR)) & ControlChars.Tab)    '翌々月販売計画量
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_KTUKISU)) & ControlChars.Tab)      '基準月数
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_FZAIKOS)) & ControlChars.Tab)      '復旧用在庫
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_AZAIKOS)) & ControlChars.Tab)      '安全在庫
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_METUKE)) & ControlChars.Tab)       '目付
                                        buf.Append(_db.rmNullStr(.Rows(i)(COLDT_ZZAIKOR)) & ControlChars.Tab)      '前月末在庫量
                                        Clipboard.SetText(buf.ToString)
                                        eh.paste(xlsRow, XLSCOL_SIYOUCD)
                                        Clipboard.Clear()
                                        '<--2010.12.27 chg by takagi #高速化

                                        '-->2011.03.23 add by takagi #在庫月数は当月在庫÷翌月販売で求める
                                        '目付が存在すればフラグを立てる
                                        If "".Equals(_db.rmNullStr(.Rows(i)(COLDT_METUKE))) Or _
                                           _db.rmNullDouble(.Rows(i)(COLDT_METUKE)) = 0 Then
                                            existsMetsukeFlg = False
                                        Else
                                            existsMetsukeFlg = True
                                        End If
                                        '<--2011.03.23 add by takagi #在庫月数は当月在庫÷翌月販売で求める

                                    End If
                                    'End With               '2010.12.27 del by takagi #高速化
                                Next
                            End With                        '2010.12.27 add by takagi #高速化

                            '出力する行を次の行に移す
                            xlsRow = xlsRow + 1

                            '列を1行追加
                            eh.copyRow(xlsRow)
                            eh.insertPasteRow(xlsRow)

                            '品種計行出力
                            eh.setValue("品 種 計", xlsRow, XLSCOL_HINMEI)
                            eh.setValue("=SUBTOTAL(9," & XLSALP_ZZZAIKOS & startTotalRow & ":" & XLSALP_ZZZAIKOS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_ZZZAIKOS)     '前々月末在庫数
                            eh.setValue("=SUBTOTAL(9," & XLSALP_ZSEISANS & startTotalRow & ":" & XLSALP_ZSEISANS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_ZSEISANS)     '前月生産実績数
                            eh.setValue("=SUBTOTAL(9," & XLSALP_ZHANBAIS & startTotalRow & ":" & XLSALP_ZHANBAIS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_ZHANBAIS)     '前月販売実績数
                            eh.setValue("=SUBTOTAL(9," & XLSALP_ZZAIKOS & startTotalRow & ":" & XLSALP_ZZAIKOS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_ZZAIKOS)        '前月末在庫数
                            eh.setValue("=SUBTOTAL(9," & XLSALP_TSEISANS & startTotalRow & ":" & XLSALP_TSEISANS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_TSEISANS)     '当月生産計画数
                            eh.setValue("=SUBTOTAL(9," & XLSALP_TSEISANR & startTotalRow & ":" & XLSALP_TSEISANR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_TSEISANR)     '当月生産計画量
                            eh.setValue("=SUBTOTAL(9," & XLSALP_THANBAIS & startTotalRow & ":" & XLSALP_THANBAIS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_THANBAIS)     '当月販売計画数
                            eh.setValue("=SUBTOTAL(9," & XLSALP_THANBAIR & startTotalRow & ":" & XLSALP_THANBAIR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_THANBAIR)     '当月販売計画量
                            eh.setValue("=SUBTOTAL(9," & XLSALP_TZAIKOS & startTotalRow & ":" & XLSALP_TZAIKOS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_TZAIKOS)        '当月末在庫数
                            eh.setValue("=SUBTOTAL(9," & XLSALP_TZAIKOR & startTotalRow & ":" & XLSALP_TZAIKOR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_TZAIKOR)        '当月末在庫量
                            eh.setValue("=SUBTOTAL(9," & XLSALP_KURIKOSIS & startTotalRow & ":" & XLSALP_KURIKOSIS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_KURIKOSIS)  '繰越数
                            eh.setValue("=SUBTOTAL(9," & XLSALP_LOTSU & startTotalRow & ":" & XLSALP_LOTSU & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_LOTSU)              'ロット数
                            eh.setValue("=SUBTOTAL(9," & XLSALP_YSEISANS & startTotalRow & ":" & XLSALP_YSEISANS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YSEISANS)     '翌月生産計画数
                            eh.setValue("=SUBTOTAL(9," & XLSALP_YSEISANR & startTotalRow & ":" & XLSALP_YSEISANR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YSEISANR)     '翌月生産計画量
                            eh.setValue("=SUBTOTAL(9," & XLSALP_YHANBAIS & startTotalRow & ":" & XLSALP_YHANBAIS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YHANBAIS)     '翌月販売計画数
                            eh.setValue("=SUBTOTAL(9," & XLSALP_YHANBAIR & startTotalRow & ":" & XLSALP_YHANBAIR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YHANBAIR)     '翌月販売計画量
                            eh.setValue("=SUBTOTAL(9," & XLSALP_YZAIKOS & startTotalRow & ":" & XLSALP_YZAIKOS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YZAIKOS)        '翌月末在庫数
                            eh.setValue("=SUBTOTAL(9," & XLSALP_YZAIKOR & startTotalRow & ":" & XLSALP_YZAIKOR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YZAIKOR)        '翌月末在庫量
                            '-->2011.03.23 chg by takagi #在庫月数は当月在庫÷翌月販売で求める
                            'eh.setValue("=SUBTOTAL(9," & XLSALP_YTUKISU & startTotalRow & ":" & XLSALP_YTUKISU & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YTUKISU)        '月数
                            If existsMetsukeFlg Then
                                '目付あり→重量(t)
                                eh.setValue("=" & XLSALP_YZAIKOR & CStr(xlsRow) & "/" & XLSALP_YYHANBAIR & CStr(xlsRow), xlsRow, XLSCOL_YTUKISU)                         '月数(t)
                            Else
                                '目付なし→重量(Km)
                                eh.setValue("=" & XLSALP_YZAIKOS & CStr(xlsRow) & "/" & XLSALP_YYHANBAIS & CStr(xlsRow), xlsRow, XLSCOL_YTUKISU)                         '月数(Km)
                            End If
                            '<--2011.03.23 chg by takagi #在庫月数は当月在庫÷翌月販売で求める
                            eh.setValue("=SUBTOTAL(9," & XLSALP_YYHANBAIS & startTotalRow & ":" & XLSALP_YYHANBAIS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YYHANBAIS)  '翌々月販売計画数
                            eh.setValue("=SUBTOTAL(9," & XLSALP_YYHANBAIR & startTotalRow & ":" & XLSALP_YYHANBAIR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YYHANBAIR)  '翌々月販売計画量

                            '罫線を再設定
                            lineV.Bottom = LineVO.LineType.BrokenL
                            lineV.Top = LineVO.LineType.BrokenL
                            eh.drawRuledLine(lineV, xlsRow, 1, , 29)

                            '最終合計行出力
                            xlsRow = xlsRow + 1
                            eh.deleteRow(xlsRow)
                            '-->2011.01.15 del by takagi
                            'eh.setValue("MAX", xlsRow, XLSCOL_SIYOUCD)
                            '<--2011.01.15 del by takagi
                            eh.setValue("総 合 計", xlsRow, XLSCOL_HINMEI)
                            eh.setValue("=SUBTOTAL(9," & XLSALP_ZZZAIKOS & startPrintRow & ":" & XLSALP_ZZZAIKOS & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_ZZZAIKOS)     '前々月末在庫数
                            eh.setValue("-", xlsRow, XLSCOL_LOTTYO)
                            eh.setValue("-", xlsRow, XLSCOL_ZZZAIKOS)
                            eh.setValue("-", xlsRow, XLSCOL_ZSEISANS)
                            eh.setValue("-", xlsRow, XLSCOL_ZHANBAIS)
                            eh.setValue("-", xlsRow, XLSCOL_ZZAIKOS)
                            eh.setValue("-", xlsRow, XLSCOL_TSEISANS)
                            eh.setValue("=SUBTOTAL(9," & XLSALP_TSEISANR & startPrintRow & ":" & XLSALP_TSEISANR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_TSEISANR)     '当月生産計画量
                            eh.setValue("-", xlsRow, XLSCOL_THANBAIS)
                            eh.setValue("=SUBTOTAL(9," & XLSALP_THANBAIR & startPrintRow & ":" & XLSALP_THANBAIR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_THANBAIR)     '当月販売計画量
                            eh.setValue("-", xlsRow, XLSCOL_TZAIKOS)
                            eh.setValue("=SUBTOTAL(9," & XLSALP_TZAIKOR & startPrintRow & ":" & XLSALP_TZAIKOR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_TZAIKOR)        '当月末在庫量
                            eh.setValue("-", xlsRow, XLSCOL_KURIKOSIS)
                            eh.setValue("-", xlsRow, XLSCOL_LOTSU)
                            eh.setValue("-", xlsRow, XLSCOL_YSEISANS)
                            eh.setValue("=SUBTOTAL(9," & XLSALP_YSEISANR & startPrintRow & ":" & XLSALP_YSEISANR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YSEISANR)     '翌月生産計画量
                            eh.setValue("-", xlsRow, XLSCOL_YHANBAIS)
                            eh.setValue("=SUBTOTAL(9," & XLSALP_YHANBAIR & startPrintRow & ":" & XLSALP_YHANBAIR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YHANBAIR)     '翌月販売計画量
                            eh.setValue("-", xlsRow, XLSCOL_YZAIKOS)
                            eh.setValue("=SUBTOTAL(9," & XLSALP_YZAIKOR & startPrintRow & ":" & XLSALP_YZAIKOR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YZAIKOR)        '翌月末在庫量
                            '-->2011.03.23 add by takagi #在庫月数は当月在庫÷翌月販売で求める
                            'eh.setValue("=SUBTOTAL(9," & XLSALP_YTUKISU & startPrintRow & ":" & XLSALP_YTUKISU & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YTUKISU)        '月数
                            eh.setValue("-", xlsRow, XLSCOL_YTUKISU)        '月数
                            '<--2011.03.23 add by takagi #在庫月数は当月在庫÷翌月販売で求める
                            eh.setValue("-", xlsRow, XLSCOL_YYHANBAIS)
                            eh.setValue("=SUBTOTAL(9," & XLSALP_YYHANBAIR & startPrintRow & ":" & XLSALP_YYHANBAIR & CStr(xlsRow - 1) & ")", xlsRow, XLSCOL_YYHANBAIR)  '翌々月販売計画量
                            eh.setValue("-", xlsRow, XLSCOL_KIJUNTUKISU)
                            eh.setValue("-", xlsRow, XLSCOL_FUKKYUYO)
                            eh.setValue("-", xlsRow, XLSCOL_ANZEN)

                            '罫線を再設定
                            lineV.Bottom = LineVO.LineType.NomalL
                            lineV.Top = LineVO.LineType.NomalL
                            eh.drawRuledLine(lineV, xlsRow, 1, , 29)


                            '作成日時編集
                            Dim printDate As String = Now.ToString("yyyy/MM/dd HH:mm")
                            '' 2010/12/22 upd start sugano
                            'eh.setValue("作成日時 ： " & printDate, 1, 29)   'AC1
                            eh.setValue("作成日時 ： " & printDate, 1, 28)   'AB1
                            '' 2010/12/22 upd end sugano

                            '需要品名編集
                            eh.setValue("(" & _db.rmNullStr(dsHanyo.Tables(RS).Rows(hanyoCnt)(HANYOU_NAME1)) & ")", 2, 6)      'F2

                            '処理年月、計画年月編集
                            eh.setValue("処理年月：" & lblSyori.Text & "　　計画年月：" & lblKeikaku.Text, 2, 10)    'J2

                            '検索条件
                            eh.setValue("需要先：" & _serchJuyo, 4, 1)        'A4

                            'ヘッダーの年月編集
                            eh.setValue(lblZengetu.Text, 7, 7)      'G7　前月
                            eh.setValue(lblTogetu.Text, 7, 11)      'K7　当月
                            eh.setValue(lblYokugetu.Text, 7, 19)    'S7　翌月
                            eh.setValue(lblYygetu.Text, 7, 24)      'X7　翌々月
                            '前々月の月を計算
                            '-->2011.01.15 chg by takagi #69
                            'Dim zzgetu As Integer = CInt(lblSyori.Text.Substring(5, 2)) - 2
                            'If zzgetu = 0 Then
                            '    zzgetu = 12
                            'End If
                            Dim zzgetu As Integer = Format(DateAdd(DateInterval.Month, -2, CDate(lblSyori.Text & "/01")), "MM")
                            '<--2011.01.15 chg by takagi #69
                            eh.setValue(zzgetu & "月", 7, 5)        'E7　前々月

                            '左上のセルにフォーカス当てる
                            eh.selectCell(START_PRINT, XLSCOL_SIYOUCD)  'A10

                            Clipboard.Clear()       'クリップボードの初期化
                        End If
                    Next
                    eh.deleteSheet("Ver1.0.00")

                    '-->2010.12.27 add by takagi 
                    '先頭シートを選択
                    eh.targetSheetByIdx = 1
                    eh.selectSheet(eh.targetSheet)
                    eh.selectCell(1, 1)
                    '<--2010.12.27 add by takagi

                Finally
                    eh.close()
                End Try

                'EXCELファイル開く
                eh.display()

                '-->2010.12.27 add by takagi #高速化
            Catch pbe As UtilProgressBarCancelEx
                Throw pbe '上位転送
                '<--2010.12.27 add by takagi #高速化
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

            '-->2010.12.27 add by takagi #高速化
        Catch pbe As UtilProgressBarCancelEx
            Throw pbe '上位転送
            '<--2010.12.27 add by takagi #高速化
        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

#End Region

#Region "ユーザ定義関数:画面制御"

    '-------------------------------------------------------------------------------
    '　画面起動時
    '-------------------------------------------------------------------------------
    Private Sub initForm()
        Try

            '登録ボタン・再計算ボタン・EXCEL出力ボタン・オプションボタン使用不可
            btnTouroku.Enabled = False
            btnSaikeisan.Enabled = False
            btnSeisanPrint.Enabled = False
            btnHinsyuPrint.Enabled = False
            optTanni.Enabled = False

            '端末IDの取得
            _tanmatuID = UtilClass.getComputerName

            'マウスカーソル砂時計
            Me.Cursor = Cursors.WaitCursor

            '処理年月、計画年月表示
            Call dispDate()

            '需要先コンボボックスのセット
            Call setCbo()

            'トランザクション開始
            _db.beginTran()

            '一覧データ作成
            Call delInsWK10()
            Call createWK10()

            'トランザクション終了
            _db.commitTran()

            '一覧表示
            Call dispWK10()

            '一覧行着色フラグを有効にする
            _colorCtlFlg = True

            '背景色の設定
            Call setBackcolor(0, 0)

            '再計算済みにする
            _saikeisanFlg = True

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        Finally
            If _db.isTransactionOpen = True Then
                _db.rollbackTran()                          'ロールバック
            End If
            'マウスカーソル矢印
            Me.Cursor = Cursors.Arrow
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　並べ替えラベル押下時
    '　(処理概要)一覧を並び替える
    '-------------------------------------------------------------------------------
    Private Sub lblJuyoSort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblJuyoSort.Click, _
                                                                                                        lblHinCDSort.Click, _
                                                                                                        lblHinmeiSort.Click
        Try
            'マウスカーソル砂時計
            Me.Cursor = Cursors.WaitCursor

            '一覧行着色フラグを無効にする
            _colorCtlFlg = False

            '一覧のデータをワークテーブルに更新
            Call updateWK10()

            '計画数・計画量の再計算
            Call culcUpdateWK10()

            '再計算済みにする
            _saikeisanFlg = True

            '一覧ヘッダーラベル編集
            If sender.Equals(lblJuyoSort) Then
                '需要先
                If (LBL_JUYO & N & LBL_SYOJUN).Equals(lblJuyoSort.Text) Then
                    '既に昇順で並べ替えられている場合
                    dispWK10("JUYOSORT DESC")
                    lblJuyoSort.Text = LBL_JUYO & N & LBL_KOJUN
                Else
                    '初回押下時または既に降順で並べ替えられている場合
                    dispWK10("JUYOSORT")
                    lblJuyoSort.Text = LBL_JUYO & N & LBL_SYOJUN
                End If
                '品名コード・品名のラベルを元に戻す
                lblHinCDSort.Text = LBL_HINMEICD
                lblHinmeiSort.Text = LBL_HINMEI

            ElseIf sender.Equals(lblHinCDSort) Then
                '品名コード
                If (LBL_HINMEICD & N & LBL_SYOJUN).Equals(lblHinCDSort.Text) Then
                    '既に昇順で並べ替えられている場合
                    dispWK10("KHINMEICD DESC")
                    lblHinCDSort.Text = LBL_HINMEICD & N & LBL_KOJUN
                Else
                    '初回押下時または既に降順で並べ替えられている場合
                    dispWK10("KHINMEICD")
                    lblHinCDSort.Text = LBL_HINMEICD & N & LBL_SYOJUN
                End If
                '需要先・品名のラベルを元に戻す
                lblJuyoSort.Text = LBL_JUYO
                lblHinmeiSort.Text = LBL_HINMEI

            ElseIf sender.Equals(lblHinmeiSort) Then
                '品名
                If (LBL_HINMEI & N & LBL_SYOJUN).Equals(lblHinmeiSort.Text) Then
                    '既に昇順で並べ替えられている場合
                    dispWK10("HINMEI DESC")
                    lblHinmeiSort.Text = LBL_HINMEI & N & LBL_KOJUN
                Else
                    '初回押下時または既に降順で並べ替えられている場合
                    dispWK10("HINMEI")
                    lblHinmeiSort.Text = LBL_HINMEI & N & LBL_SYOJUN
                End If
                '需要先・品種コードのラベルを元に戻す
                lblJuyoSort.Text = LBL_JUYO
                lblHinCDSort.Text = LBL_HINMEICD

            Else
                Exit Sub
            End If

            '背景色の設定
            Call setBackcolor(0, 0)

            '一覧行着色フラグを有効にする
            _colorCtlFlg = True

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        Finally
            'マウスカーソル元に戻す
            Me.Cursor = Cursors.Arrow
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　オプションボタン押下後の表示列の切り替え
    '　(処理概要)ｔ列とkm列の表示を切り替える
    '-------------------------------------------------------------------------------
    Private Sub changeDisp()
        Try

            Dim kazuFlg As Boolean = False
            Dim ryouFlg As Boolean = False
            Dim tanni As String = ""

            If rdoKm.Checked Then   'km表示
                kazuFlg = True
                ryouFlg = False
                tanni = LBL_KM
                lblKurikosi.Text = LBL_KURIKOSIS & LBL_KM
            Else                    't表示
                kazuFlg = False
                ryouFlg = True
                tanni = LBL_TON
                lblKurikosi.Text = LBL_KURIKOSIR & LBL_TON
            End If

            '各列の表示設定
            dgvSeisanSyuusei.Columns(COLNO_ZZAIKOS).Visible = kazuFlg           '前月末在庫数		
            dgvSeisanSyuusei.Columns(COLNO_ZZAIKOR).Visible = ryouFlg           '前月末在庫量		
            dgvSeisanSyuusei.Columns(COLNO_TSEISANS).Visible = kazuFlg          '当月生産計画数		
            dgvSeisanSyuusei.Columns(COLNO_TSEISANR).Visible = ryouFlg          '当月生産計画量		
            dgvSeisanSyuusei.Columns(COLNO_THANBAIS).Visible = kazuFlg          '当月販売計画数		
            dgvSeisanSyuusei.Columns(COLNO_THANBAIR).Visible = ryouFlg          '当月販売計画量		
            dgvSeisanSyuusei.Columns(COLNO_TZAIKOS).Visible = kazuFlg           '当月末在庫数		
            dgvSeisanSyuusei.Columns(COLNO_TZAIKOR).Visible = ryouFlg           '当月末在庫量		
            dgvSeisanSyuusei.Columns(COLNO_KURIKOSIS).Visible = kazuFlg         '繰越数
            dgvSeisanSyuusei.Columns(COLNO_KURIKOSIR).Visible = ryouFlg         '繰越量
            dgvSeisanSyuusei.Columns(COLNO_YSEISANS).Visible = kazuFlg          '翌月生産計画数		
            dgvSeisanSyuusei.Columns(COLNO_YSEISANR).Visible = ryouFlg          '翌月生産計画量		
            dgvSeisanSyuusei.Columns(COLNO_YHANBAIS).Visible = kazuFlg          '翌月販売計画数		
            dgvSeisanSyuusei.Columns(COLNO_YHANBAIR).Visible = ryouFlg          '翌月販売計画量		
            dgvSeisanSyuusei.Columns(COLNO_YZAIKOS).Visible = kazuFlg           '翌月末在庫数		
            dgvSeisanSyuusei.Columns(COLNO_YZAIKOR).Visible = ryouFlg           '翌月末在庫量		
            dgvSeisanSyuusei.Columns(COLNO_YYHANBAIS).Visible = kazuFlg         '翌々月販売計画数	
            dgvSeisanSyuusei.Columns(COLNO_YYHANBAIR).Visible = ryouFlg         '翌々月販売計画量	
            dgvSeisanSyuusei.Columns(COLNO_FUKKYUS).Visible = kazuFlg           '復旧用在庫数		
            dgvSeisanSyuusei.Columns(COLNO_FUKKYUR).Visible = ryouFlg           '復旧用在庫量		
            dgvSeisanSyuusei.Columns(COLNO_AZAIKOS).Visible = kazuFlg           '安全在庫数			
            dgvSeisanSyuusei.Columns(COLNO_AZAIKOR).Visible = ryouFlg           '安全在庫量	

            '見出しラベルの表示設定
            lblZZaiko.Text = LBL_ZAIKO & N & tanni          '前月末在庫
            lblTSeisan.Text = LBL_SEISAN & N & tanni        '当月生産計画
            lblTHanbai.Text = LBL_HANBAI & N & tanni        '当月販売計画
            lblTZaiko.Text = LBL_ZAIKO & N & tanni          '当月末在庫
            lblYSeisan.Text = LBL_SEISAN & N & tanni        '翌月生産計画
            lblYHanbai.Text = LBL_HANBAI & N & tanni        '翌月販売計画
            lblYZaiko.Text = LBL_ZAIKO & N & tanni          '翌月末在庫
            lblYYHanbai.Text = LBL_HANBAI & N & tanni       '翌々月販売計画
            lblFZaiko.Text = LBL_FUKKYU & N & tanni         '復旧用在庫
            lblAZaiko.Text = LBL_ANNZEN & N & tanni         '安全在庫

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　検索条件SQL作成
    '　(処理概要)画面に入力された検索条件をSQL文にする
    '-------------------------------------------------------------------------------
    Private Function createSerchStr() As String
        Try
            createSerchStr = ""

            '検索条件の保持
            _serchJuyo = cboJuyou.Text

            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(cboJuyou)
            If ch.getCode.Equals(CBONULLCODE) Then
                createSerchStr = ""
            Else
                createSerchStr = ch.getCode

            End If

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Function

    '-------------------------------------------------------------------------------
    '　コントロールキー押下イベント
    '　(処理概要)エンターボタン押下時に次のコントロールに移る
    '-------------------------------------------------------------------------------
    Private Sub txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboJuyou.KeyPress
        UtilClass.moveNextFocus(Me, e) '次のコントロールへ移動する 

    End Sub

    '-------------------------------------------------------------------------------
    '　並べ替えラベルの表示初期化
    '　(処理概要)検索・登録ボタン押下時、並べ替えラベルの表示を初期化する
    '-------------------------------------------------------------------------------
    Private Sub initLabel()

        lblJuyoSort.Text = LBL_JUYO
        lblHinCDSort.Text = LBL_HINMEICD
        lblHinmeiSort.Text = LBL_HINMEI

    End Sub

#End Region

#Region "ユーザ定義関数:DGV関連"

    '-------------------------------------------------------------------------------
    '   一覧　編集チェック（EditingControlShowingイベント）
    '   （処理概要）入力の制限をかける
    '-------------------------------------------------------------------------------
    Private Sub dgvSeisanSyuusei_EditingControlShowing(ByVal sender As Object, _
                                              ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs _
                                              ) Handles dgvSeisanSyuusei.EditingControlShowing

        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanSyuusei)
            '■ロット数の場合
            If dgvSeisanSyuusei.CurrentCell.ColumnIndex = COLNO_LOTSUU Then

                '■グリッドに、数値入力モードの制限をかける
                _chkCellVO = gh.chkCell(sender, e, UtilDataGridViewHandler.chkType.Num)

            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '   サイズ別一覧　選択セル検証イベント（DataErrorイベント）
    '   （処理概要）数値入力欄に数値以外が入力された場合のエラー処理
    '-------------------------------------------------------------------------------
    Private Sub dgvSeisanSyuusei_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvSeisanSyuusei.DataError

        Try
            e.Cancel = False                                   '編集モード終了

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanSyuusei)
            '■ロット数グリッドには数値入力モード(0～9)の制限をかけているので、制限の解除
            If dgvSeisanSyuusei.CurrentCellAddress.X = COLNO_LOTSUU Then
                gh.AfterchkCell(_chkCellVO)
            End If

            '文字入力されたらセルを空にする
            gh.setCellData(COLDT_LOTSU, e.RowIndex, System.DBNull.Value)

            'エラーメッセージ表示
            Throw New UsrDefException("半角数字のみ入力可能です。", _msgHd.getMSG("onlyAcceptNumeric"))

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　一覧データ編集前
    '　(処理概要)一覧のデータが変更される前の値を保持する
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvSeisanSyuusei_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles dgvSeisanSyuusei.CellBeginEdit

        '既に変更フラグが立っている場合は何も行わない
        If _changeFlg = False Then
            _beforeChange = _db.rmNullDouble(dgvSeisanSyuusei(e.ColumnIndex, e.RowIndex).Value)
        End If
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　一覧データ編集後
    '　(処理概要)一覧のデータが変更された場合、変更フラグを立てる
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvSeisanSyuusei_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSeisanSyuusei.CellEndEdit
        Try

            '再計算していない状態にする
            _saikeisanFlg = False

            If _changeFlg = False Then
                '編集前と値が変わっていた場合、フラグを立てる
                If Not _beforeChange.Equals(_db.rmNullDouble(dgvSeisanSyuusei(e.ColumnIndex, e.RowIndex).Value)) Then
                    _changeFlg = True

                Else
                    Exit Sub
                End If
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '選択行に着色する処理
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvSeisanSyuusei_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvSeisanSyuusei.SelectionChanged
        If _colorCtlFlg Then
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanSyuusei)
            gh.setSelectionRowColor(dgvSeisanSyuusei.CurrentCellAddress.Y, _oldRowIndex, StartUp.lCOLOR_BLUE)
        End If
        _oldRowIndex = dgvSeisanSyuusei.CurrentCellAddress.Y
        Debug.Print("列数　：　" & dgvSeisanSyuusei.CurrentCellAddress.X)
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　背景色の設定処理
    '　(処理概要)行の背景色を青に着色する。
    '　　I　：　prmRowIndex     現在フォーカスがある行数
    '　　I　：　prmOldRowIndex  現在の行に移る前の行数
    '------------------------------------------------------------------------------------------------------
    Private Sub setBackcolor(ByVal prmRowIndex As Integer, ByVal prmOldRowIndex As Integer)

        Dim _dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanSyuusei)

        '指定した行の背景色を青にする
        _dgv.setSelectionRowColor(prmRowIndex, prmOldRowIndex, StartUp.lCOLOR_BLUE)

        _oldRowIndex = prmRowIndex

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　指定列へのフォーカス設定処理
    '　(処理概要)指定されたセルにフォーカスする。
    '　　I　：　prmCoIndex      フォーカスさせるセルの列数
    '　　I　：　prmRowIndex     フォーカスさせるセルの行数
    '------------------------------------------------------------------------------------------------------
    Private Sub setForcusCol(ByVal prmColIndex As Integer, ByVal prmRowIndex As Integer)

        'フォーカスをあてる
        dgvSeisanSyuusei.Focus()
        dgvSeisanSyuusei.CurrentCell = dgvSeisanSyuusei(prmColIndex, prmRowIndex)

        '背景色の設定
        Call setBackcolor(prmRowIndex, _oldRowIndex)

    End Sub

#End Region

#Region "ユーザ定義関数:DB関連"

    '-------------------------------------------------------------------------------
    '　ワークテーブルデータの作成
    '　(処理概要)ワークテーブルのデータを作成(delete & insert)
    '　　I　：　prmSql     検索条件(画面起動時は何も受け取らない)
    '-------------------------------------------------------------------------------
    Private Sub delInsWK10(Optional ByVal prmSql As String = "")
        Try

            Dim sql As String = ""
            sql = " DELETE FROM ZG530E_W10 WHERE UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql)

            '更新日時を取得
            Dim updateDate As Date = Now

            'T41生産計画
            sql = ""
            sql = sql & N & " INSERT INTO ZG530E_W10 ("
            sql = sql & N & "    JUYOUCD "          '需要先コード
            sql = sql & N & "   ,KHINMEICD "        '計画品名コード
            sql = sql & N & "   ,HINMEI "           '品名
            sql = sql & N & "   ,HINSYUNM"          '品種名
            sql = sql & N & "   ,SIYOUCD "          '仕様コード
            sql = sql & N & "   ,HINSYUCD "         '品種コード
            sql = sql & N & "   ,SENSINCD "         '線心数コード
            sql = sql & N & "   ,SIZECD "           'サイズコード
            sql = sql & N & "   ,COLORCD "          '色コード
            sql = sql & N & "   ,LOT "              '標準ロット長
            sql = sql & N & "   ,ABCKBN "           'ABC区分
            sql = sql & N & "   ,ZZZAIKOSU "        '前々月末在庫数
            sql = sql & N & "   ,ZZZAIKORYOU "      '前々月末在庫量
            sql = sql & N & "   ,ZSEISANSU "        '前月生産実績数
            sql = sql & N & "   ,ZSEISANRYOU"       '前月生産実績量
            sql = sql & N & "   ,ZHANBAISU "        '前月販売実績数
            sql = sql & N & "   ,ZHANBAIRYOU "      '前月販売実績量
            sql = sql & N & "   ,ZZAIKOSU "         '前月末在庫数
            sql = sql & N & "   ,ZZAIKORYOU "       '前月末在庫量
            sql = sql & N & "   ,TSEISANSU "        '当月生産計画数
            sql = sql & N & "   ,TSEISANRYOU "      '当月生産計画量
            sql = sql & N & "   ,THANBAISU "        '当月販売計画数
            sql = sql & N & "   ,THANBAIRYOU "      '当月販売計画量
            sql = sql & N & "   ,TZAIKOSU "         '当月末在庫数
            sql = sql & N & "   ,TZAIKORYOU "       '当月末在庫量
            sql = sql & N & "   ,KURIKOSISU "       '繰越数
            sql = sql & N & "   ,KURIKOSIRYOU "     '繰越量
            sql = sql & N & "   ,IKATULOTOSU "      '一括算出ロット数
            sql = sql & N & "   ,LOTOSU "           'ロット数
            sql = sql & N & "   ,YSEISANSU "        '翌月生産計画数
            sql = sql & N & "   ,YSEISANRYOU "      '翌月生産計画量
            sql = sql & N & "   ,YHANBAISU "        '翌月販売計画数
            sql = sql & N & "   ,YHANBAIRYOU "      '翌月販売計画量
            sql = sql & N & "   ,YZAIKOSU "         '翌月末在庫数
            sql = sql & N & "   ,YZAIKORYOU "       '翌月末在庫量
            sql = sql & N & "   ,YZAIKOTUKISU "     '翌月在庫月数
            sql = sql & N & "   ,YYHANBAISU "       '翌々月販売計画数
            sql = sql & N & "   ,YYHANBAIRYOU "     '翌々月販売計画量
            sql = sql & N & "   ,KTUKISU "          '基準月数
            sql = sql & N & "   ,FZAIKOSU "         '復旧用在庫数
            sql = sql & N & "   ,FZAIKORYOU "       '復旧用在庫量
            sql = sql & N & "   ,AZAIKOSU "         '安全在庫数
            sql = sql & N & "   ,AZAIKORYOU "       '安全在庫量
            sql = sql & N & "   ,METSUKE "          '目付
            sql = sql & N & "   ,UPDNAME "          '端末ID
            sql = sql & N & "   ,UPDDATE) "         '更新日時
            sql = sql & N & " SELECT "
            sql = sql & N & "    M.TT_JUYOUCD "     '需要先コード
            sql = sql & N & "   ,M.TT_KHINMEICD "   '計画品名コード
            sql = sql & N & "   ,M.TT_HINMEI "      '品名
            sql = sql & N & "   ,M.TT_HINSYUNM "    '品種名
            sql = sql & N & "   ,M.TT_H_SIYOU_CD "  '仕様コード
            sql = sql & N & "   ,M.TT_H_HIN_CD "    '品種コード
            sql = sql & N & "   ,M.TT_H_SENSIN_CD " '線心数コード
            sql = sql & N & "   ,M.TT_H_SIZE_CD "   'サイズコード
            sql = sql & N & "   ,M.TT_H_COLOR_CD "  '色コード
            sql = sql & N & "   ,M.TT_LOT / 1000 "  '標準ロット長(メートル単位に変換)
            sql = sql & N & "   ,M.TT_ABCKBN "      'ABC区分
            sql = sql & N & "   ,K.ZZZAIKOSU "      '前々月末在庫数
            sql = sql & N & "   ,K.ZZZAIKORYOU "    '前々月末在庫量
            sql = sql & N & "   ,K.ZSEISANSU "      '前月生産実績数
            sql = sql & N & "   ,K.ZSEISANRYOU "    '前月生産実績量
            sql = sql & N & "   ,K.ZHANBAISU "      '前月販売実績数
            sql = sql & N & "   ,K.ZHANBAIRYOU "    '前月販売実績量
            sql = sql & N & "   ,K.ZZAIKOSU "       '前月末在庫数
            sql = sql & N & "   ,K.ZZAIKORYOU "     '前月末在庫量
            sql = sql & N & "   ,K.TSEISANSU "      '当月生産計画数
            sql = sql & N & "   ,K.TSEISANRYOU "    '当月生産計画量
            sql = sql & N & "   ,K.THANBAISU "      '当月販売計画数
            sql = sql & N & "   ,K.THANBAIRYOU "    '当月販売計画量
            sql = sql & N & "   ,K.TZAIKOSU "       '当月末在庫数
            sql = sql & N & "   ,K.TZAIKORYOU "     '当月末在庫量
            sql = sql & N & "   ,K.KURIKOSISU "     '繰越数
            sql = sql & N & "   ,K.KURIKOSIRYOU "   '繰越量
            sql = sql & N & "   ,K.IKATULOTOSU "    '一括算出ロット数
            sql = sql & N & "   ,K.LOTOSU "         'ロット数
            sql = sql & N & "   ,K.YSEISANSU "      '翌月生産計画数
            sql = sql & N & "   ,K.YSEISANRYOU "    '翌月生産計画量
            sql = sql & N & "   ,K.YHANBAISU "      '翌月販売計画数
            sql = sql & N & "   ,K.YHANBAIRYOU "    '翌月販売計画量
            sql = sql & N & "   ,K.YZAIKOSU "       '翌月末在庫数
            sql = sql & N & "   ,K.YZAIKORYOU "     '翌月末在庫量
            sql = sql & N & "   ,K.YZAIKOTUKISU  "  '翌月在庫月数
            sql = sql & N & "   ,K.YYHANBAISU "     '翌々月販売計画数
            sql = sql & N & "   ,K.YYHANBAIRYOU "   '翌々月販売計画量
            sql = sql & N & "   ,K.KTUKISU "        '基準月数
            sql = sql & N & "   ,K.FZAIKOSU "       '復旧用在庫数
            sql = sql & N & "   ,K.FZAIKORYOU "     '復旧用在庫量
            sql = sql & N & "   ,K.AZAIKOSU "       '安全在庫数
            sql = sql & N & "   ,K.AZAIKORYOU "     '安全在庫量
            sql = sql & N & "   ,K.METSUKE "        '目付
            sql = sql & N & "   ,'" & _tanmatuID & "'"          '端末ID
            sql = sql & N & "   ,TO_DATE('" & updateDate & "', 'YYYY/MM/DD HH24:MI:SS') "       '更新日時
            sql = sql & N & "    FROM T41SEISANK K INNER JOIN "
            sql = sql & N & "       (SELECT * FROM M11KEIKAKUHIN "
            If Not "".Equals(prmSql) Then
                sql = sql & N & " WHERE TT_JUYOUCD = '" & prmSql & "'"
            End If
            sql = sql & N & "       ) M ON "
            sql = sql & N & "       K.KHINMEICD = M.TT_KHINMEICD "
            _db.executeDB(sql)

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　ワークテーブル作成
    '　(処理概要)ワークテーブルを作成(update)
    '------------------------------------------------------------------------------------------------------
    Private Sub createWK10()
        Try

            'M01汎用マスタ
            Dim sql As String = ""
            sql = sql & N & " UPDATE ZG530E_W10 ZG SET ( "
            sql = sql & N & "    JUYOUNM "
            sql = sql & N & "   ,JUYOSORT) = ("
            sql = sql & N & "       SELECT  "
            sql = sql & N & "           NAME2, "
            sql = sql & N & "           SORT"
            sql = sql & N & "           FROM M01HANYO M "
            sql = sql & N & "           WHERE ZG.JUYOUCD = M.KAHENKEY "
            sql = sql & N & "           AND M.KOTEIKEY = '" & KOTEI_JUYOU & "') "
            sql = sql & N & "   WHERE (JUYOUCD) = (SELECT"
            sql = sql & N & "           KAHENKEY "
            sql = sql & N & "           FROM M01HANYO M "
            sql = sql & N & "           WHERE ZG.JUYOUCD = M.KAHENKEY "
            sql = sql & N & "           AND M.KOTEIKEY = '" & KOTEI_JUYOU & "'"
            sql = sql & N & "           AND ZG.UPDNAME = '" & _tanmatuID & "')"

            _db.executeDB(sql)

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　ワークテーブルデータの一覧表示
    '　(処理概要)ワークテーブルのデータを一覧に表示する
    '　　I　：　prmSort     ソート順(画面起動時は何も受け取らない)
    '-------------------------------------------------------------------------------
    Private Sub dispWK10(Optional ByVal prmSort As String = "")
        Try

            Dim sql As String = ""
            'ワークのデータを一覧に表示
            sql = sql & N & " SELECT "
            sql = sql & N & "    JUYOUCD " & COLDT_JUYOUCD              '需要先
            sql = sql & N & "   ,JUYOUNM " & COLDT_JUYOUSAKI            '需要先名
            sql = sql & N & "   ,KHINMEICD " & COLDT_HINMEICD           '品名コード
            sql = sql & N & "   ,HINMEI " & COLDT_HINMEI                '品名
            sql = sql & N & "   ,LOT " & COLDT_LOTTYO                   'ロット長
            sql = sql & N & "   ,ABCKBN " & COLDT_ABC                   'ABC
            sql = sql & N & "   ,ZZAIKOSU " & COLDT_ZZAIKOS             '前月末在庫数
            sql = sql & N & "   ,ZZAIKORYOU " & COLDT_ZZAIKOR           '前月末在庫量
            sql = sql & N & "   ,TSEISANSU " & COLDT_TSEISANS           '当月生産計画数
            sql = sql & N & "   ,TSEISANRYOU " & COLDT_TSEISANR         '当月生産計画量
            sql = sql & N & "   ,THANBAISU " & COLDT_THANBAIS           '当月販売計画数
            sql = sql & N & "   ,THANBAIRYOU " & COLDT_THANBAIR         '当月販売計画量
            sql = sql & N & "   ,TZAIKOSU " & COLDT_TZAIKOS             '当月末在庫数
            sql = sql & N & "   ,TZAIKORYOU " & COLDT_TZAIKOR           '当月末在庫量
            sql = sql & N & "   ,KURIKOSISU " & COLDT_KURIKOSIS         '繰越数
            sql = sql & N & "   ,KURIKOSIRYOU " & COLDT_KURIKOSIR       '繰越量
            sql = sql & N & "   ,LOTOSU " & COLDT_LOTSU                 'ロット数
            sql = sql & N & "   ,YSEISANSU " & COLDT_YSEISANS           '翌月生産計画数
            sql = sql & N & "   ,YSEISANRYOU " & COLDT_YSEISANR         '翌月生産計画量
            sql = sql & N & "   ,YHANBAISU " & COLDT_YHANBAIS           '翌月販売計画数
            sql = sql & N & "   ,YHANBAIRYOU " & COLDT_YHANBAIR         '翌月販売計画量
            sql = sql & N & "   ,YZAIKOSU " & COLDT_YZAIKOS             '翌月末在庫数
            sql = sql & N & "   ,YZAIKORYOU " & COLDT_YZAIKOR           '翌月末在庫量
            sql = sql & N & "   ,YZAIKOTUKISU " & COLDT_ZAIKOTUKISU     '在庫月数       
            sql = sql & N & "   ,YYHANBAISU " & COLDT_YYHANBAIS         '翌々月販売計画数
            sql = sql & N & "   ,YYHANBAIRYOU " & COLDT_YYHANBAIR       '翌々月販売計画量
            sql = sql & N & "   ,KTUKISU " & COLDT_KTUKISU              '基準月数
            sql = sql & N & "   ,FZAIKOSU " & COLDT_FZAIKOS             '復旧用在庫数
            sql = sql & N & "   ,FZAIKORYOU " & COLDT_FZAIKOR           '復旧用在庫量
            sql = sql & N & "   ,AZAIKOSU " & COLDT_AZAIKOS             '安全在庫数
            sql = sql & N & "   ,AZAIKORYOU " & COLDT_AZAIKOR           '安全在庫量
            sql = sql & N & "   ,METSUKE " & COLDT_METUKE               '目付
            sql = sql & N & " FROM ZG530E_W10 "
            sql = sql & N & "   WHERE UPDNAME = '" & _tanmatuID & "'"
            sql = sql & N & " ORDER BY "
            If "".Equals(prmSort) Then
                '' 2010/12/17 upd start sugano
                'sql = sql & N & " JUYOSORT, HINSYUCD, SENSINCD, SIZECD, SIYOUCD, COLORCD "
                sql = sql & N & " HINSYUCD, SENSINCD, SIZECD, SIYOUCD, COLORCD "
                '' 2010/12/17 upd end sugano
            Else
                sql = sql & N & " " & prmSort
            End If

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                '一覧のクリア
                Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanSyuusei)
                gh.clearRow()

                '件数のクリア
                lblKensu.Text = "0件"

                '登録・再計算・EXCEL出力ボタンおよび単位オプションボタン使用不可
                btnTouroku.Enabled = False
                btnSaikeisan.Enabled = False
                btnSeisanPrint.Enabled = False
                btnHinsyuPrint.Enabled = False
                optTanni.Enabled = False

                Throw New UsrDefException("対象データがありません。", _msgHd.getMSG("NonData"))
            Else
                '抽出データがある場合、登録ボタン・EXCELボタンを有効にする
                btnTouroku.Enabled = _updFlg
                btnSaikeisan.Enabled = True
                btnSeisanPrint.Enabled = True
                btnHinsyuPrint.Enabled = True
                optTanni.Enabled = True
            End If

            '抽出データを一覧に表示する
            dgvSeisanSyuusei.DataSource = ds
            dgvSeisanSyuusei.DataMember = RS

            '一覧の件数を表示する
            lblKensu.Text = CStr(iRecCnt) & "件"

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　処理年月、計画年月表示
    '　(処理概要)処理年月、計画年月を表示する
    '-------------------------------------------------------------------------------
    Private Sub dispDate()
        Try
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & " SNENGETU " & "SYORI"          '処理年月
            sql = sql & N & " ,KNENGETU " & "KEIKAKU"       '計画年月
            sql = sql & N & " FROM T01KEIKANRI "

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                lblSyori.Text = ""
                lblKeikaku.Text = ""
                lblTogetu.Text = ""
                lblYokugetu.Text = ""
                lblYygetu.Text = ""
                lblZengetu.Text = ""
            Else
                Dim syoriDate As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("SYORI"))
                Dim keikakuDate As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("KEIKAKU"))

                '「YYYY/MM」形式で表示
                lblSyori.Text = syoriDate.Substring(0, 4) & "/" & syoriDate.Substring(4, 2)
                lblKeikaku.Text = keikakuDate.Substring(0, 4) & "/" & keikakuDate.Substring(4, 2)

                '一覧ヘッダー表示
                If CInt(syoriDate.Substring(4, 2)) < 10 Then
                    lblTogetu.Text = syoriDate.Substring(5, 1) & MONTH
                Else
                    lblTogetu.Text = syoriDate.Substring(4, 2) & MONTH
                End If

                If CInt(keikakuDate.Substring(4, 2)) < 10 Then
                    lblYokugetu.Text = keikakuDate.Substring(5, 1) & MONTH
                Else
                    lblYokugetu.Text = keikakuDate.Substring(4, 2) & MONTH
                End If
                '翌々月の日付を作成
                Dim yyhanbai As String = keikakuDate & "01"     '日付に変換するために日を付け足す
                Dim yyDate As DateTime = Date.ParseExact(yyhanbai, "yyyyMMdd", Nothing)
                yyDate = yyDate.AddMonths(1)        '1ヶ月足す

                If CInt(CStr(yyDate).Substring(5, 2)) < 10 Then
                    '月が1桁の場合
                    lblYygetu.Text = CStr(yyDate).Substring(6, 1) & MONTH
                Else
                    lblYygetu.Text = CStr(yyDate).Substring(5, 2) & MONTH
                End If

                '前月の日付を作成
                Dim zDate As DateTime = yyDate.AddMonths(-3)
                'If CInt(CStr(zDate).Substring(5, 2)) Then
                If CInt(CStr(zDate).Substring(5, 2)) < 10 Then
                    '月が1桁の場合
                    lblZengetu.Text = CStr(zDate).Substring(6, 1) & MONTH
                Else
                    lblZengetu.Text = CStr(zDate).Substring(5, 2) & MONTH
                End If
            End If


        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　需要先コンボボックスのセット
    '　(処理概要)M01汎用マスタから需要先名を抽出して表示する。
    '-------------------------------------------------------------------------------
    Private Sub setCbo()
        Try

            'コンボボックス
            Dim sql = ""
            sql = sql & N & " SELECT KAHENKEY KAHEN, "
            sql = sql & N & " NAME2 JUYOUSAKI "
            sql = sql & N & " FROM M01HANYO "
            sql = sql & N & " WHERE KOTEIKEY = '" & KOTEI_JUYOU & "' "
            sql = sql & N & " ORDER BY KAHENKEY "


            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                btnTouroku.Enabled = False
                btnSaikeisan.Enabled = False
                btnSeisanPrint.Enabled = False
                btnHinsyuPrint.Enabled = False
                optTanni.Enabled = False
                Throw New UsrDefException("汎用マスタの値の取得に失敗しました。", _msgHd.getMSG("noHanyouMst"))
            End If

            'コンボボックスクリア
            Me.cboJuyou.Items.Clear()
            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(cboJuyou)

            '先頭に空行
            ch.addItem(New UtilCboVO(CBONULLCODE, ""))

            'ループさせてコンボボックスにセット
            For i As Integer = 0 To iRecCnt - 1
                ch.addItem(New UtilCboVO(_db.rmNullStr(ds.Tables(RS).Rows(i)("KAHEN")), _db.rmNullStr(ds.Tables(RS).Rows(i)("JUYOUSAKI"))))
            Next

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　ワークテーブルデータの更新
    '　(処理概要)一覧に表示されているデータをワークテーブルに更新する
    '-------------------------------------------------------------------------------
    Private Sub updateWK10()
        Try

            Dim sql As String = ""
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanSyuusei)

            'トランザクション開始
            _db.beginTran()

            '行数分だけループ
            For i As Integer = 0 To gh.getMaxRow - 1
                sql = ""
                sql = sql & N & " UPDATE ZG530E_W10 SET "
                sql = sql & N & " LOTOSU = " & NS(_db.rmNullStr(dgvSeisanSyuusei(COLNO_LOTSUU, i).Value))     'ロット数
                sql = sql & N & "   WHERE UPDNAME = '" & _tanmatuID & "'"
                sql = sql & N & "   AND KHINMEICD = '" & dgvSeisanSyuusei(COLNO_HINMEICD, i).Value & "'"
                _db.executeDB(sql)
            Next

            'トランザクション終了
            _db.commitTran()

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        Finally
            If _db.isTransactionOpen = True Then
                _db.rollbackTran()                          'ロールバック
            End If
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

    '-------------------------------------------------------------------------------
    '　再計算処理
    '　(処理概要)ロット数を元にワークテーブルの計画値を再計算する
    '-------------------------------------------------------------------------------
    Private Sub culcUpdateWK10()
        Try

            '翌月生産計画数
            Dim sql As String = ""
            sql = sql & N & " UPDATE ZG530E_W10 SET "
            sql = sql & N & " YSEISANSU = LOTOSU * LOT + KURIKOSISU "
            sql = sql & N & " WHERE UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql)

            '翌月末在庫数
            sql = ""
            sql = sql & N & " UPDATE ZG530E_W10 SET "
            sql = sql & N & " YZAIKOSU = TZAIKOSU + YSEISANSU - YHANBAISU "
            sql = sql & N & " WHERE UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql)

            '翌月在庫月数
            sql = ""
            sql = sql & N & " UPDATE ZG530E_W10 SET "
            sql = sql & N & " YZAIKOTUKISU = YZAIKOSU / YYHANBAISU "
            sql = sql & N & " WHERE UPDNAME = '" & _tanmatuID & "'"
            '-->2010.12.02 add by takagi 
            sql = sql & N & "  AND YYHANBAISU != 0"
            '<--2010.12.02 add by takagi 
            _db.executeDB(sql)

            '翌月生産計画量
            sql = ""
            sql = sql & N & " UPDATE ZG530E_W10 SET "
            sql = sql & N & " YSEISANRYOU = YSEISANSU * METSUKE / 1000 "
            sql = sql & N & " WHERE UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql)

            '翌月末在庫量
            sql = ""
            sql = sql & N & " UPDATE ZG530E_W10 SET "
            sql = sql & N & " YZAIKORYOU = YZAIKOSU * METSUKE / 1000 "
            sql = sql & N & " WHERE UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql)

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　DB更新
    '　(処理概要)ワークテーブルの値をT41に登録する
    '-------------------------------------------------------------------------------
    Private Sub registT41()
        Try

            'T41削除
            Dim delCnt As Integer = 0           '削除レコード数
            Dim sql As String = ""
            sql = " DELETE FROM T41SEISANK T41 "
            sql = sql & N & " WHERE EXISTS "
            sql = sql & N & "   (SELECT * FROM ZG530E_W10 WK "
            sql = sql & N & "       WHERE T41.KHINMEICD = WK.KHINMEICD "
            sql = sql & N & " AND UPDNAME = '" & _tanmatuID & "')"
            _db.executeDB(sql, delCnt)

            '更新開始日時を取得
            Dim updStartDate As Date = Now

            'ワークテーブルの値をT41に登録
            sql = ""
            sql = sql & N & " INSERT INTO T41SEISANK ( "
            sql = sql & N & "    KHINMEICD "            '計画品名コード
            sql = sql & N & "   ,ZZZAIKOSU "            '前々月末在庫数
            sql = sql & N & "   ,ZZZAIKORYOU "          '前々月末在庫量
            sql = sql & N & "   ,ZSEISANSU "            '前月生産実績数
            sql = sql & N & "   ,ZSEISANRYOU "          '前月生産実績量
            sql = sql & N & "   ,ZHANBAISU "            '前月販売実績数
            sql = sql & N & "   ,ZHANBAIRYOU "          '前月販売実績量
            sql = sql & N & "   ,ZZAIKOSU "             '前月末在庫数
            sql = sql & N & "   ,ZZAIKORYOU "           '前月末在庫量
            sql = sql & N & "   ,TSEISANSU "            '当月生産計画数
            sql = sql & N & "   ,TSEISANRYOU "          '当月生産計画量
            sql = sql & N & "   ,THANBAISU "            '当月販売計画数
            sql = sql & N & "   ,THANBAIRYOU "          '当月販売計画量
            sql = sql & N & "   ,TZAIKOSU "             '当月末在庫数
            sql = sql & N & "   ,TZAIKORYOU "           '当月末在庫量
            sql = sql & N & "   ,KURIKOSISU "           '繰越数
            sql = sql & N & "   ,KURIKOSIRYOU "         '繰越量
            sql = sql & N & "   ,IKATULOTOSU "          '一括算出ロット数
            sql = sql & N & "   ,LOTOSU "               'ロット数
            sql = sql & N & "   ,YSEISANSU "            '翌月生産計画数
            sql = sql & N & "   ,YSEISANRYOU "          '翌月生産計画量
            sql = sql & N & "   ,YHANBAISU "            '翌月販売計画数
            sql = sql & N & "   ,YHANBAIRYOU "          '翌月販売計画量
            sql = sql & N & "   ,YZAIKOSU "             '翌月末在庫数
            sql = sql & N & "   ,YZAIKORYOU "           '翌月末在庫量
            sql = sql & N & "   ,YZAIKOTUKISU  "        '翌月在庫月数
            sql = sql & N & "   ,YYHANBAISU "           '翌々月販売計画数
            sql = sql & N & "   ,YYHANBAIRYOU "         '翌々月販売計画量
            sql = sql & N & "   ,KTUKISU "              '基準月数
            sql = sql & N & "   ,FZAIKOSU "             '復旧用在庫数
            sql = sql & N & "   ,FZAIKORYOU "           '復旧用在庫量
            sql = sql & N & "   ,AZAIKOSU "             '安全在庫数
            sql = sql & N & "   ,AZAIKORYOU "           '安全在庫量
            sql = sql & N & "   ,METSUKE "              '目付
            sql = sql & N & "   ,UPDNAME "              '端末ID
            sql = sql & N & "   ,UPDDATE )"             '更新日時
            sql = sql & N & " SELECT "
            sql = sql & N & "    KHINMEICD "            '計画品名コード
            sql = sql & N & "   ,ZZZAIKOSU "            '前々月末在庫数
            sql = sql & N & "   ,ZZZAIKORYOU "          '前々月末在庫量
            sql = sql & N & "   ,ZSEISANSU "            '前月生産実績数
            sql = sql & N & "   ,ZSEISANRYOU "          '前月生産実績量
            sql = sql & N & "   ,ZHANBAISU "            '前月販売実績数
            sql = sql & N & "   ,ZHANBAIRYOU "          '前月販売実績量
            sql = sql & N & "   ,ZZAIKOSU "             '前月末在庫数
            sql = sql & N & "   ,ZZAIKORYOU "           '前月末在庫量
            sql = sql & N & "   ,TSEISANSU "            '当月生産計画数
            sql = sql & N & "   ,TSEISANRYOU "          '当月生産計画量
            sql = sql & N & "   ,THANBAISU "            '当月販売計画数
            sql = sql & N & "   ,THANBAIRYOU "          '当月販売計画量
            sql = sql & N & "   ,TZAIKOSU "             '当月末在庫数
            sql = sql & N & "   ,TZAIKORYOU "           '当月末在庫量
            sql = sql & N & "   ,KURIKOSISU "           '繰越数
            sql = sql & N & "   ,KURIKOSIRYOU "         '繰越量
            sql = sql & N & "   ,IKATULOTOSU "          '一括算出ロット数
            sql = sql & N & "   ,LOTOSU "               'ロット数
            sql = sql & N & "   ,YSEISANSU "            '翌月生産計画数
            sql = sql & N & "   ,YSEISANRYOU "          '翌月生産計画量
            sql = sql & N & "   ,YHANBAISU "            '翌月販売計画数
            sql = sql & N & "   ,YHANBAIRYOU "          '翌月販売計画量
            sql = sql & N & "   ,YZAIKOSU "             '翌月末在庫数
            sql = sql & N & "   ,YZAIKORYOU "           '翌月末在庫量
            sql = sql & N & "   ,YZAIKOTUKISU  "        '翌月在庫月数
            sql = sql & N & "   ,YYHANBAISU "           '翌々月販売計画数
            sql = sql & N & "   ,YYHANBAIRYOU "         '翌々月販売計画量
            sql = sql & N & "   ,KTUKISU "              '基準月数
            sql = sql & N & "   ,FZAIKOSU "             '復旧用在庫数
            sql = sql & N & "   ,FZAIKORYOU "           '復旧用在庫量
            sql = sql & N & "   ,AZAIKOSU "             '安全在庫数
            sql = sql & N & "   ,AZAIKORYOU "           '安全在庫量
            sql = sql & N & "   ,METSUKE "              '目付
            sql = sql & N & "   ,UPDNAME "              '端末ID
            sql = sql & N & "   ,TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS')"
            sql = sql & N & "   FROM ZG530E_W10 "
            sql = sql & N & "       WHERE UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql)

            '更新終了日時を取得
            Dim updFinDate As Date = Now

            '更新件数の取得
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanSyuusei)        'DGVハンドラの設定
            Dim updCnt As Integer = gh.getMaxRow

            '処理日時・計画日時取得
            Dim syoriDate As String = lblSyori.Text.Substring(0, 4) & lblSyori.Text.Substring(5, 2)
            Dim keikakuDate As String = lblKeikaku.Text.Substring(0, 4) & lblKeikaku.Text.Substring(5, 2)

            '実行履歴登録処理
            sql = ""
            sql = "INSERT INTO T91RIREKI ("
            sql = sql & N & "  PGID"                                                        '機能ID
            sql = sql & N & ", SNENGETU"                                                    '処理日時
            sql = sql & N & ", KNENGETU"                                                    '計画日時
            sql = sql & N & ", SDATESTART"                                                  '処理開始日時
            sql = sql & N & ", SDATEEND"                                                    '処理終了日時
            sql = sql & N & ", KENNSU1"                                                     '件数１（削除件数）
            sql = sql & N & ", KENNSU2"                                                     '件数２（登録件数）
            sql = sql & N & ", UPDNAME"                                                     '端末ID
            sql = sql & N & ", UPDDATE"                                                     '更新日時
            sql = sql & N & ") VALUES ("
            sql = sql & N & "  '" & PGID & "'"                                              '機能ID
            sql = sql & N & ", '" & syoriDate & "'"
            sql = sql & N & ", '" & keikakuDate & "'"
            sql = sql & N & ", TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "   '処理開始日時
            sql = sql & N & ", TO_DATE('" & updFinDate & "', 'YYYY/MM/DD HH24:MI:SS') "     '処理終了日時
            sql = sql & N & ", " & delCnt                                                   '件数１（削除件数）
            sql = sql & N & ", " & updCnt                                                   '件数２（登録件数）
            sql = sql & N & ", '" & _tanmatuID & "'"                                        '端末ID
            sql = sql & N & ", TO_DATE('" & updFinDate & "', 'YYYY/MM/DD HH24:MI:SS') "     '更新日時
            sql = sql & N & " )"
            _db.executeDB(sql)

            'T02処理制御テーブル更新
            _parentForm.updateSeigyoTbl(PGID, True, updStartDate, updFinDate)

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　品種別集計表出力用データ抽出
    '　(処理概要)品種別集計表出力のため、ワークテーブルのデータをデータセットに保持して返す。
    '   ●入力パラメタ  ：需要先コード
    '   ●出力パラメタ  ：検索結果のデータセット
    '                   ：データセットの件数
    '   ●メソッド戻り値：なし
    '-------------------------------------------------------------------------------
    Private Sub getDataForHinsyuXls(ByVal prmJuyoCD As String, ByRef prmDs As DataSet, ByRef prmRecCnt As Integer)
        Try

            Dim sql As String = ""
            'ワークのデータを一覧に表示
            sql = sql & N & " SELECT "
            sql = sql & N & "    HINSYUCD " & COLDT_HINSYUCD                '品種コード

            '' 2011/01/24 upd start sugano
            'sql = sql & N & "   ,HINSYUNM " & COLDT_HINSYUNM                '品種名
            sql = sql & N & "   ,MAX(HINSYUNM) " & COLDT_HINSYUNM           '品種名
            '' 2011/01/24 upd end sugano

            sql = sql & N & "   ,SUM(ZZZAIKORYOU) " & COLDT_ZZZAIKOR        '前々月末在庫量
            sql = sql & N & "   ,SUM(ZSEISANRYOU) " & COLDT_ZSEISANR        '前月生産実績量
            sql = sql & N & "   ,SUM(ZHANBAIRYOU) " & COLDT_ZHANBAIR        '前月販売実績量

            '' 2011/01/20 upd start sugano
            'sql = sql & N & "   ,SUM(ZZAIKORYOU) " & COLDT_ZZAIKOR          '前月末在庫量
            sql = sql & N & "   ,SUM(ZZAIKORYOU) " & COLDT_ZZAIKOR          '前月末在庫量
            '' 2011/01/20 upd end sugano

            sql = sql & N & "   ,SUM(TSEISANRYOU) " & COLDT_TSEISANR        '当月生産計画量
            sql = sql & N & "   ,SUM(THANBAIRYOU) " & COLDT_THANBAIR        '当月販売計画量

            '' 2011/01/20 upd start sugano
            'sql = sql & N & "   ,SUM(TZAIKORYOU) " & COLDT_TZAIKOR          '当月末在庫量
            sql = sql & N & "   ,SUM(ZZAIKORYOU+TSEISANRYOU-THANBAIRYOU) " & COLDT_TZAIKOR          '当月末在庫量
            '' 2011/01/20 upd end sugano

            sql = sql & N & "   ,SUM(KURIKOSIRYOU) " & COLDT_KURIKOSIR      '繰越量
            sql = sql & N & "   ,SUM(LOTOSU) " & COLDT_LOTSU                'ロット数
            sql = sql & N & "   ,SUM(YSEISANRYOU) " & COLDT_YSEISANR        '翌月生産計画量
            sql = sql & N & "   ,SUM(YHANBAIRYOU) " & COLDT_YHANBAIR        '翌月販売計画量

            '' 2011/01/20 upd start sugano
            'sql = sql & N & "   ,SUM(YZAIKORYOU) " & COLDT_YZAIKOR          '翌月末在庫量   
            sql = sql & N & "   ,SUM(ZZAIKORYOU+TSEISANRYOU-THANBAIRYOU+YSEISANRYOU-YHANBAIRYOU) " & COLDT_YZAIKOR          '翌月末在庫量   
            '' 2011/01/20 upd end sugano

            sql = sql & N & "   ,SUM(YYHANBAIRYOU) " & COLDT_YYHANBAIR      '翌々月販売計画量
            sql = sql & N & " FROM ZG530E_W10 "
            sql = sql & N & "   WHERE UPDNAME = '" & _tanmatuID & "' AND JUYOUCD = '" & prmJuyoCD & "'"

            '' 2011/01/24 upd start sugano
            'sql = sql & N & "   GROUP BY HINSYUCD, HINSYUNM "
            sql = sql & N & "   GROUP BY HINSYUCD "
            '' 2011/01/24 upd end sugano

            sql = sql & N & "   ORDER BY HINSYUCD "

            'SQL発行
            prmDs = _db.selectDB(sql, RS, prmRecCnt)

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　生産販売在庫計画出力用データ抽出
    '　(処理概要)生産販売在庫計画出力のため、ワークテーブルのデータをデータセットに保持して返す。
    '   ●入力パラメタ  ：需要先コード
    '   ●出力パラメタ  ：検索結果のデータセット
    '                   ：データセットの件数
    '   ●メソッド戻り値：なし
    '-------------------------------------------------------------------------------
    Private Sub getDataForSeisanXls(ByVal prmJuyoCD As String, ByRef prmDs As DataSet, ByRef prmRecCnt As Integer)
        Try

            Dim sql As String = ""
            'ワークのデータを一覧に表示
            sql = sql & N & " SELECT "
            sql = sql & N & "    HINSYUCD " & COLDT_HINSYUCD        '品種コード
            sql = sql & N & "   ,SIYOUCD " & COLDT_SIYOCD           '仕様コード
            sql = sql & N & "   ,HINMEI " & COLDT_HINMEI            '品名
            sql = sql & N & "   ,LOT " & COLDT_LOTTYO               '標準ロット長
            sql = sql & N & "   ,ABCKBN " & COLDT_ABC               'ABC区分
            sql = sql & N & "   ,ZZZAIKOSU " & COLDT_ZZZAIKOS       '前々月末在庫数
            sql = sql & N & "   ,ZSEISANSU " & COLDT_ZSEISANS       '前月生産実績数
            sql = sql & N & "   ,ZHANBAISU " & COLDT_ZHANBAIS       '前月販売実績数
            sql = sql & N & "   ,TSEISANSU " & COLDT_TSEISANS       '当月生産計画数
            sql = sql & N & "   ,TSEISANRYOU " & COLDT_TSEISANR     '当月生産計画量
            sql = sql & N & "   ,THANBAISU " & COLDT_THANBAIS       '当月販売計画数
            sql = sql & N & "   ,THANBAIRYOU " & COLDT_THANBAIR     '当月販売計画量
            sql = sql & N & "   ,ZZAIKORYOU " & COLDT_ZZAIKOR       '前月末在庫量
            sql = sql & N & "   ,KURIKOSISU " & COLDT_KURIKOSIS     '繰越数
            sql = sql & N & "   ,LOTOSU " & COLDT_LOTSU             'ロット数
            sql = sql & N & "   ,YSEISANSU " & COLDT_YSEISANS       '翌月生産計画数
            sql = sql & N & "   ,YSEISANRYOU " & COLDT_YSEISANR     '翌月生産計画量
            sql = sql & N & "   ,YHANBAISU " & COLDT_YHANBAIS       '翌月販売計画数
            sql = sql & N & "   ,YHANBAIRYOU " & COLDT_YHANBAIR     '翌月販売計画量
            sql = sql & N & "   ,YZAIKOTUKISU " & COLDT_ZAIKOTUKISU '翌月在庫月数
            sql = sql & N & "   ,YYHANBAISU " & COLDT_YYHANBAIS     '翌々月販売計画数
            sql = sql & N & "   ,YYHANBAIRYOU " & COLDT_YYHANBAIR   '翌々月販売計画量
            sql = sql & N & "   ,KTUKISU " & COLDT_KTUKISU          '基準月数
            '' 2011/01/20 upd start sugano
            'sql = sql & N & "   ,FZAIKOSU " & COLDT_FZAIKOS         '復旧用在庫数
            'sql = sql & N & "   ,AZAIKOSU " & COLDT_AZAIKOS         '安全在庫数
            sql = sql & N & "   ,DECODE(FZAIKOSU,0,'',FZAIKOSU) " & COLDT_FZAIKOS         '復旧用在庫数
            sql = sql & N & "   ,DECODE(AZAIKOSU,0,'',AZAIKOSU) " & COLDT_AZAIKOS         '安全在庫数
            '' 2011/01/20 upd end sugano
            sql = sql & N & "   ,METSUKE " & COLDT_METUKE           '目付
            sql = sql & N & " FROM ZG530E_W10 "
            sql = sql & N & "   WHERE UPDNAME = '" & _tanmatuID & "' AND JUYOUCD = '" & prmJuyoCD & "'"
            sql = sql & N & "   ORDER BY JUYOSORT, HINSYUCD, SENSINCD, SIZECD, SIYOUCD, COLORCD "


            'SQL発行
            prmDs = _db.selectDB(sql, RS, prmRecCnt)

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

#End Region

#Region "ユーザ定義関数:チェック処理"

    '------------------------------------------------------------------------------------------------------
    '  登録チェック
    '　(処理概要)必須項目チェックを行う
    '------------------------------------------------------------------------------------------------------
    Private Sub checkTouroku()
        Try

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanSyuusei)

            For i As Integer = 0 To gh.getMaxRow - 1

                '入力桁数チェック
                'ロット数
                Call checkKeta(COLDT_LOTSU, "ロット数", i, COLNO_LOTSUU)

            Next

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '  必須入力チェック
    '　(処理概要)セルが入力されているかチェックする
    '　　I　：　prmColName              チェックするセルの列名
    '　　I　：　prmColHeaderName        エラー時に表示する列名
    '　　I　：　prmCnt                  チェックするセルの行数
    '　　I　：　prmColNo                チェックするセルの列数
    '------------------------------------------------------------------------------------------------------
    Private Sub checkKeta(ByVal prmColName As String, ByVal prmColHeaderName As String, ByVal prmCnt As Integer, ByVal prmColNo As Integer)
        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanSyuusei)

            '必須入力チェック
            If "".Equals(gh.getCellData(prmColName, prmCnt).ToString) Then
                'フォーカスをあてる
                Call setForcusCol(prmColNo, prmCnt)
                'エラーメッセージの表示
                '-->2010.12.17 chg by takagi #13
                'Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【 '" & prmColHeaderName & "' ：" & prmCnt + 1 & "行目】"))
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput"))
                '<--2010.12.17 chg by takagi #13
            End If

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

#End Region

End Class