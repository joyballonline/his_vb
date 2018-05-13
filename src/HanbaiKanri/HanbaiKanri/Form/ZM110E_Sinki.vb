'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）計画対象品マスタメンテ新規登録画面
'    （フォームID）ZM110E_Sinki
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   中澤        2010/10/25                 新規
'　(2)   菅野        2011/01/13                 変更　科目コード桁数チェック追加
'　(3)   菅野        2014/06/04                 変更　材料票マスタ（MPESEKKEI）テーブル変更対応            
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.Combo
Imports UtilMDL.DataGridView

Public Class ZM110E_Sinki
    Implements IfRturnKahenKey

#Region "リテラル値定義"

    'PG制御文字 
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const RS As String = "RecSet"                       'レコードセットテーブル
    Private Const RS2 As String = "RecSet2"                     'レコードセットテーブル
    Private Const NUMBER As String = "999999D9"                 'SQLでカンマ区切りの数値文字列をカンマ抜きの数値に変換するための形式

    '-->2010.12.02 upd by takagi
    'Private Const PGID As String = "ZM110E1"                     'T91に登録するPGID
    Private _pgId As String = ""                     'T91に登録するPGID
    '<--2010.12.02 upd by takagi

    '画面モード
    Private Const SINKITOUROKU As String = "新規登録"
    Private Const SAKUJO As String = "削除"

    '各入力項目初期値
    Private Const SYOKI_SEISAKUKBN As String = "1"              '製作区分＝1(内作)
    Private Const SYOKI_ZAIKO As String = "1"                   '在庫・繰返＝1(在庫対象)
    Private Const SYOKI_TENKAIKBN As String = "1"               '展開区分＝1(全展開)
    Private Const SYOKI_HINSHITSUKBN As String = "0"            '品質試験区分＝0(余長不要)
    Private Const SYOKI_TACHIAIKBN As String = "1"              '立会有無＝1(ナシ)

    Private Const SYOKI_SYORIKBN As String = "1"                '処理区分＝1(新規)
    Private Const SYOKI_KAKOUKBN As String = "0"                '加工長計算区分＝0(計算不要)
    Private Const SYOKI_TANTYOKBN As String = "2"               '単長区分＝2(標準外)

    Private Const SYOKI_SEISEKI As String = "0"                '成績書
    Private Const SYOKI_MTANTYO As String = "0"                '持込余長単長毎
    Private Const SYOKI_MLOT As String = "0"                   '持込余長ロット毎
    Private Const SYOKI_TTANTYO As String = "0"                '立会余長単長毎
    Private Const SYOKI_TLOT As String = "0"                   '立会余長ロット毎
    Private Const SYOKI_STANTYO As String = "0"                '指定社検単長毎
    Private Const SYOKI_SLOT As String = "0"                   '指定社検ロット毎
    Private Const SYOKI_TOKKI As String = ""                    '特記事項
    Private Const SYOKI_BIKOU As String = ""                    '備考
    Private Const SYOKI_NYUUKO As String = ""                   '入庫本数
    Private Const SYOKI_TOUROKUFLG As String = ""               '登録フラグ
    Private Const SYOKI_HENKOU As String = ""                   '変更内容

    '製作区分コード
    Private Const SEISAKUKBNCD_NAISAKU As String = "1"
    Private Const SEISAKUKBNCD_GAITYU As String = "2"

    '在庫・繰返コード
    Private Const ZAIKOCD_ZAIKO As String = "1"
    Private Const ZAIKOCD_KURIKAESI As String = "2"

    '展開区分コード
    Private Const TENKAIKBNCD_ZEN As String = "1"
    Private Const TENKAIKBNCD_BUBUN As String = "2"

    '品質試験区分コード
    Private Const HINSITUCD_YOTYOUFUYO As String = "0"
    Private Const HINSITUCD_LOTKANRI As String = "2"

    '材料票DB検索用
    Private Const HINSYU As String = "HINSYU"
    Private Const LINE As String = "LINE"
    Private Const COLOR As String = "COLOR"

    'M11検索用リテラル
    Private Const DB_SEISAKU As String = "seisakukubn"      '製作区分
    Private Const DB_HINMEI As String = "hinmei"            '品名
    Private Const DB_LOTTYO As String = "lottyo"            '標準ロット長
    Private Const DB_TANTYO As String = "tantyo"            '単長
    Private Const DB_JOSU As String = "josu"                '条数
    Private Const DB_KND As String = "knd"                  '北日本分
    Private Const DB_SUMIDEN As String = "sumiden"          '住電日立分
    Private Const DB_ZAIKO As String = "zaiko"              '在庫・繰返
    Private Const DB_TYUMONSAKI As String = "tyumonsaki"    '注文先
    Private Const DB_JUYOSAKI As String = "juyousaki"       '需要先
    Private Const DB_ABC As String = "abc"                  'ABC
    Private Const DB_SIZETENKAI As String = "sizetenkai"    'サイズ展開
    Private Const DB_HINSYUKBN As String = "hinsyukbn"      '品種区分
    Private Const DB_KIJUNTUKI As String = "kijuntukisu"    '基準月数
    Private Const DB_SAIGAI As String = "saigai"            '災害復旧用在庫量
    Private Const DB_ANZEN As String = "anzen"              '安全在庫量
    Private Const DB_KAMOKU As String = "kamoku"            '科目コード
    Private Const DB_MAKIWAKU As String = "makiwaku"        '巻枠コード
    Private Const DB_HOUSOU As String = "housou"            '包装／表示区分
    Private Const DB_SIYOUSYO As String = "siyousyo"        '仕様書番号
    Private Const DB_SEIZOUBMN As String = "seizobumon"     '製造部門
    Private Const DB_TEHAIKBN As String = "tehaikbn"        '手配区分
    Private Const DB_TENKAIKBN As String = "tenkai"         '展開区分
    Private Const DB_BUBUNTENKAI As String = "bubuntenkai"  '部分展開工程
    Private Const DB_HINSITU As String = "hinsitu"          '品質試験区分
    Private Const DB_TATIAI As String = "tatiai"            '立会有無
    Private Const DB_SYORIKBN As String = "syorikbn"        '処理区分
    Private Const DB_KAKOU As String = "kakou"              '加工長計算区分
    Private Const DB_TANTYOKBN As String = "tantyokbn"      '単長区分
    Private Const DB_SEISEKI As String = "seiseki"          '成績書
    Private Const DB_MTANTYO As String = "mtantyo"          '持込余長単長毎
    Private Const DB_MLOT As String = "mlot"                '持込余長ロット毎
    Private Const DB_TTANTYO As String = "ttantyo"          '立会余長単長毎
    Private Const DB_TLOT As String = "tlot"                '立会余長ロット毎
    Private Const DB_STANTYO As String = "stantyo"          '指定社検単長毎
    Private Const DB_SLOT As String = "slot"                '指定社検ロット毎
    Private Const DB_TOKKI As String = "tokki"              '特記事項
    Private Const DB_BIKOU As String = "bikou"              '備考
    Private Const DB_NYUUKO As String = "nyuuko"            '入庫本数
    Private Const DB_TOUROKU As String = "touroku"          '登録フラグ
    Private Const DB_HENKOU As String = "henkou"            '変更内容

    'M12検索用リテラル
    Private Const COLDT_KHINMEICD As String = "dtSHinmeiCD"
    Private Const COLDT_KHINMEI As String = "dtSHinmei"
    Private Const COLDT_HINSYUNM As String = "dtHinsyuNM"
    Private Const COLDT_SIZENM As String = "dtSizeNM"
    Private Const COLDT_IRONM As String = "dtIroNM"

    '材料票検索用リテラル
    Private Const INT_SEQNO As Integer = 1

    '汎用マスタ固定キー
    Private Const HKOTEIKEY_JUYOSAKI As String = "01"               '需要先
    Private Const HKOTEIKEY_TEHAI_KBN As String = "02"              '手配区分
    Private Const HKOTEIKEY_SEISAKU_KBN As String = "03"            '製作区分
    Private Const HKOTEIKEY_TENKAI_KBN As String = "04"             '展開区分
    Private Const HKOTEIKEY_KAKOUCHO_KBN As String = "05"           '加工長計算区分
    Private Const HKOTEIKEY_TACHIAI_UM As String = "06"             '立会有無
    Private Const HKOTEIKEY_TANCHO_KBN As String = "07"             '単長区分
    Private Const HKOTEIKEY_HINSHITU_KBN As String = "08"           '品質試験区分
    Private Const HKOTEIKEY_SEIZO_BMN As String = "09"              '製造部門
    Private Const HKOTEIKEY_ZAIKO_KBN As String = "10"              '在庫・繰返
    Private Const HKOTEIKEY_SIZETENKAI_KBN As String = "11"         'サイズ展開
    Private Const HKOTEIKEY_SYORI_KBN As String = "19"              '処理区分

    '入力チェック用リテラル
    Private Const TENKAIKBN_BUBUN As String = "2"                   '展開区分=2(部分展開) 
    Private Const SEISAKUKBN_GAITYU As String = "2"                 '製作区分=2(外注)

    '集計対象品名コード一覧データバインド列名
    Private Const COLDT_SCODE As String = "dtSHinmeiCD"             '集計対象品名コード
    Private Const COLDT_SNAME As String = "dtSHinmei"               '集計対象品名

    Private Enum IniFormType As Short
        ''' <summary>
        ''' キー項目含
        ''' </summary>
        ''' <remarks></remarks>
        IncludeKey = 1
        ''' <summary>
        ''' キー項目除く
        ''' </summary>
        ''' <remarks></remarks>
        ExecludeKey = 0

    End Enum

#End Region

#Region "メンバー変数宣言"

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu
    Private _ZC910KahenKey As String                'ZC910S_CodeSentakuから受け取る汎用マスタ可変キー
    Private _ZC910Meisyo As String                  'ZC910S_CodeSentakuから受け取る汎用マスタ名称

    Private _oldRowIndex As Integer = -1            '選択行の背景色を変更するためのフラグを宣言
    Private _colorCtlFlg As Boolean = False         '選択行の背景色を変更するためのフラグを宣言

    Private _updFlg As Boolean = False              '更新可否
    Private _sinkiFlg As Boolean = False            'モード制御フラグ
    Private _itiranDispFirstFlg As Boolean = True   '一覧にデータが表示されているかどうかのフラグ

    Private _tanmatuID As String = ""               '端末ID
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
    'コンストラクタ　   メニューまたは修正画面から呼ばれる
    '-------------------------------------------------------------------------------
    '-->2010.12.02 add by takagi
    'Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmForm As ZC110M_Menu, _
    '                                         ByVal prmUpdFlg As Boolean, ByVal prmSinkiFlg As Boolean)
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmForm As ZC110M_Menu, _
                                     ByVal prmUpdFlg As Boolean, ByVal prmSinkiFlg As Boolean, ByVal prmPgId As String)
        '<--2010.12.02 add by takagi
        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _parentForm = prmForm                                               '親フォーム
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        _updFlg = prmUpdFlg
        _sinkiFlg = prmSinkiFlg
        '-->2010.12.02 add by takagi
        _pgId = prmPgId
        '<--2010.12.02 add by takagi

    End Sub
#End Region

#Region "Formイベント"
    '-------------------------------------------------------------------------------
    '　処理開始イベント
    '-------------------------------------------------------------------------------
    Private Sub ZM110E_Sinki_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            '画面表示
            Call initForm()

            If _sinkiFlg Then
                '集計対象品名一覧バインド用データテーブルの列定義
                Dim dt As DataTable = New DataTable(RS)
                dt.Columns.Add(COLDT_SCODE, Type.GetType("System.String")) 'String型
                dt.Columns.Add(COLDT_SNAME, Type.GetType("System.String")) 'String型

                'データセットにデータテーブルをセット
                Dim ds As DataSet = New DataSet
                ds.Tables.Add(dt)

                'グリッドにデータセットをバインド
                dgvSTaisyou.DataSource = ds
                dgvSTaisyou.DataMember = RS
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

#End Region

#Region "ボタンイベント"

    '-------------------------------------------------------------------------------
    '　検索ボタン押下イベント
    '　(処理概要)入力値された品名コードをもとにデータを画面表示する。
    '　　　　　　検索完了後は、キャンセルボタン押下まで検索不可とする。
    '-------------------------------------------------------------------------------
    Private Sub btnKensaku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensaku.Click
        Try

            '検索条件のチェック
            '品名コードが全て入力されている場合は検索を行う。
            Call checkInputHinmeiCD()

            '画面のクリア
            clearCtrDelMode()

            'データを検索して画面表示
            Call dispData()

            '各入力項目をもとにラベルを表示する。
            Call txtSeisaku_Leave(Nothing, Nothing)                                     '製作区分
            Call txtZaiko_Leave(Nothing, Nothing)                                       '在庫･繰返
            Call txtJuyousaki_Leave(Nothing, Nothing)                                   '需要先
            Call txtInput_Leave(txtSizeTenkai, Nothing)                                 'サイズ展開
            Call txtInput_Leave(txtSeizouBmn, Nothing)                                  '製造部門
            Call txtTenkaiKbn_Leave(Nothing, Nothing)                                   '展開区分
            Call txtInput_Leave(txtHinsitu, Nothing)                                    '品質試験区分
            Call txtInput_Leave(txtTatiai, Nothing)                                     '立会有無
            Call dispMakiwakuLabel()                                                    '巻枠コード
            Call dispHousouLabel()                                                      '包装／表示区分
            lblTehaiKbnNM.Text = serchHanyoMst(HKOTEIKEY_TEHAI_KBN, lblTehaiKbn.Text)   '手配区分
            lblSyoriKbnNM.Text = serchHanyoMst(HKOTEIKEY_SYORI_KBN, lblSyoriKbn.Text)   '処理区分
            lblKakoNM.Text = serchHanyoMst(HKOTEIKEY_KAKOUCHO_KBN, lblKako.Text)        '加工長計算区分
            lblTantyoNM.Text = serchHanyoMst(HKOTEIKEY_TANCHO_KBN, lblTantyo.Text)      '単長区分
            Call dispCboLabel()                                                         '品種区分

            btnKensaku.Enabled = False      '検索ボタン使用不可
            btnTouroku.Enabled = True       '削除ボタン使用可
            btnCancel.Enabled = True        'キャンセルボタン使用可

            'キャンセルボタン押下まで再検索不可
            txtSiyo.ReadOnly = True                     '仕様コード
            txtSiyo.BackColor = StartUp.lCOLOR_YELLOW
            txtHinsyu.ReadOnly = True                   '品種コード
            txtHinsyu.BackColor = StartUp.lCOLOR_YELLOW
            txtSensin.ReadOnly = True                   '線心数コード
            txtSensin.BackColor = StartUp.lCOLOR_YELLOW
            txtSize.ReadOnly = True                     'サイズコード
            txtSize.BackColor = StartUp.lCOLOR_YELLOW
            txtColor.ReadOnly = True                    '色コード
            txtColor.BackColor = StartUp.lCOLOR_YELLOW

            'キャンセルボタンにフォーカス
            btnCancel.Focus()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　キャンセルボタン押下イベント
    '　(処理概要)画面をクリアし、検索ボタンを使用可とする。
    '-------------------------------------------------------------------------------
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            '画面クリア
            Call clearCtrDelMode()

            '品名コード入力可・テキストボックスの色を戻す
            txtSiyo.Focus()                             '仕様コード
            txtSiyo.Text = ""
            txtSiyo.BackColor = StartUp.lCOLOR_WHITE
            txtSiyo.ReadOnly = False
            txtSiyo.Enabled = True
            txtHinsyu.Text = ""                         '品種コード
            txtHinsyu.BackColor = StartUp.lCOLOR_WHITE
            txtHinsyu.ReadOnly = False
            txtHinsyu.Enabled = True
            txtSensin.Text = ""                         '線心数コード
            txtSensin.BackColor = StartUp.lCOLOR_WHITE
            txtSensin.ReadOnly = False
            txtSensin.Enabled = True
            txtSize.Text = ""                           'サイズコード
            txtSize.BackColor = StartUp.lCOLOR_WHITE
            txtSize.ReadOnly = False
            txtSize.Enabled = True
            txtColor.Text = ""                          '色コード
            txtColor.BackColor = StartUp.lCOLOR_WHITE
            txtColor.ReadOnly = False
            txtColor.Enabled = True

            'ボタン制御
            btnKensaku.Enabled = True       '検索ボタン使用可
            btnTouroku.Enabled = False      '削除ボタン使用不可
            btnCancel.Enabled = False       'キャンセルボタン使用不可
            btnHinmeiHyouji.Enabled = True

            '' 2010/12/27 add start sugano #キャンセル時、各項目に初期値が設定されない不具合を修正
            Call initForm()
            '' 2010/12/27 add end sugano

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　品名表示ボタン押下イベント
    '　(処理概要)入力値された品名コードをもとに品名をラベルに表示する。
    '-------------------------------------------------------------------------------
    Private Sub btnHinmeiHyouji_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHinmeiHyouji.Click
        Try

            '検索条件のチェック
            '品名コードが全て入力されている場合は検索を行う。
            Call checkInputHinmeiCD()

            '品名を検索して表示
            Call dispHinmei()

            '登録ボタン使用可
            btnTouroku.Enabled = True

            'キャンセルボタン使用可
            btnCancel.Enabled = True
            btnCancel.Visible = True

            '品名コード入力不可・品名表示不可
            txtSiyo.ReadOnly = True                     '仕様コード
            txtSiyo.BackColor = StartUp.lCOLOR_YELLOW
            txtHinsyu.ReadOnly = True                   '品種コード
            txtHinsyu.BackColor = StartUp.lCOLOR_YELLOW
            txtSensin.ReadOnly = True                   '線心数コード
            txtSensin.BackColor = StartUp.lCOLOR_YELLOW
            txtSize.ReadOnly = True                     'サイズコード
            txtSize.BackColor = StartUp.lCOLOR_YELLOW
            txtColor.ReadOnly = True                    '色コード
            txtColor.BackColor = StartUp.lCOLOR_YELLOW
            btnHinmeiHyouji.Enabled = False             '品名表示ボタン

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　子画面起動各ボタン押下イベント
    '　(処理概要)子画面を起動し、子画面で選択されたデータを親画面に表示する
    '-------------------------------------------------------------------------------
    Private Sub clickSubBtn(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeisaku.Click, _
                                                                                                btnJuyousaki.Click, _
                                                                                                btnZaiko.Click, _
                                                                                                btnSizeTenkai.Click, _
                                                                                                btnSeizouBmn.Click, _
                                                                                                btnTenkaiKbn.Click, _
                                                                                                btnHinsitu.Click, _
                                                                                                btnTatiai.Click



        Dim koteiKey As String = ""     '子画面に渡す固定キー
        Dim targetTxtBox As TextBox     '押下したボタンに対応するテキストボックス
        Dim targetLbl As Label

        Try

            Dim buttonName As String = ""
            buttonName = CType(sender, Button).Name

            Select Case True
                Case btnSeisaku.Name.Equals(buttonName)
                    '「製作区分」ボタン押下イベント
                    koteiKey = HKOTEIKEY_SEISAKU_KBN
                    targetTxtBox = txtSeisaku
                    targetLbl = lblSeisaku
                Case btnJuyousaki.Name.Equals(buttonName)
                    '「需要先」ボタン押下イベント
                    koteiKey = HKOTEIKEY_JUYOSAKI
                    targetTxtBox = txtJuyousaki
                    targetLbl = lblJuyousaki
                Case btnZaiko.Name.Equals(buttonName)
                    '「在庫・繰返」ボタン押下イベント
                    koteiKey = HKOTEIKEY_ZAIKO_KBN
                    targetTxtBox = txtZaiko
                    targetLbl = lblZaiko
                Case btnSizeTenkai.Name.Equals(buttonName)
                    '「サイズ展開」ボタン押下イベント
                    koteiKey = HKOTEIKEY_SIZETENKAI_KBN
                    targetTxtBox = txtSizeTenkai
                    targetLbl = lblSizeTenkai
                Case btnSeizouBmn.Name.Equals(buttonName)
                    '「製造部門」ボタン押下イベント
                    koteiKey = HKOTEIKEY_SEIZO_BMN
                    targetTxtBox = txtSeizouBmn
                    targetLbl = lblSeizoBmn
                Case btnTenkaiKbn.Name.Equals(buttonName)
                    '「展開区分」ボタン押下イベント
                    koteiKey = HKOTEIKEY_TENKAI_KBN
                    targetTxtBox = txtTenkaiKbn
                    targetLbl = lblTenkaiKbn
                Case btnHinsitu.Name.Equals(buttonName)
                    '「品質試験区分」ボタン押下イベント
                    koteiKey = HKOTEIKEY_HINSHITU_KBN
                    targetTxtBox = txtHinsitu
                    targetLbl = lblHinmei
                Case btnTatiai.Name.Equals(buttonName)
                    '「立会有無」ボタン押下イベント
                    koteiKey = HKOTEIKEY_TACHIAI_UM
                    targetTxtBox = txtTatiai
                    targetLbl = lblTatiai
                Case Else
                    Exit Sub
            End Select

            Dim kahenKey As String = ""
            If Not "".Equals(targetTxtBox.Text) Then
                kahenKey = targetTxtBox.Text
            End If

            '子画面の起動
            Dim openForm As ZC910S_CodeSentaku
            If targetTxtBox.Equals(txtJuyousaki) Then
                '需要先の場合は名称2を返す
                openForm = New ZC910S_CodeSentaku(_msgHd, _db, Me, koteiKey, kahenKey, StartUp.HANYO_BACK_NAME2)      'パラメタを遷移先画面へ渡す
            Else
                'それ以外は名称1を返す
                openForm = New ZC910S_CodeSentaku(_msgHd, _db, Me, koteiKey, kahenKey, StartUp.HANYO_BACK_NAME1)      'パラメタを遷移先画面へ渡す
            End If
            openForm.ShowDialog(Me)                                                             '画面表示
            openForm.Dispose()
            If Not "".Equals(_ZC910KahenKey) Then                    'getKahenKeyメソッドで子画面から可変キーを受け取っている
                '押下されたボタンに対応するテキストボックス・ラベルに表示
                targetTxtBox.Text = _ZC910KahenKey
                targetLbl.Text = _ZC910Meisyo

                '自動表示
                If targetTxtBox.Equals(txtSeisaku) Then
                    '展開区分・品質試験区分自動表示、科目コードの使用可否設定
                    Call txtSeisakuChange()
                ElseIf targetTxtBox.Equals(txtZaiko) Then
                    '手配区分・注文先自動表示
                    Call txtZaikoChange()
                ElseIf targetTxtBox.Equals(txtJuyousaki) Then
                    '品種区分コンボの再作成とラベルの初期化
                    Call createCbo()
                ElseIf targetTxtBox.Equals(txtTenkaiKbn) Then
                    '部分展開指定工程自動設定
                    Call txtTenkaiKbnChange()
                End If
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　集計対象品名コード追加ボタン押下イベント
    '　(処理概要)入力された集計対象品名コードと品名を一覧に表示する。
    '-------------------------------------------------------------------------------
    Private Sub btnTuika_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTuika.Click
        Try
            '集計対象品名コード入力チェック
            Call checkInputSHinmeiCD()

            '集計対象品名コード重複チェック
            Call checkKHinmeiCDRepeat(txtSSiyo.Text, txtSHinsyu.Text, txtSSensin.Text, txtSSize.Text, txtSColor.Text)

            '一覧表示
            Call dispDgv()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　集計対象品名コード削除ボタン押下イベント
    '　(処理概要)一覧で選択された行を削除する。
    '-------------------------------------------------------------------------------
    Private Sub btnSakujo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSakujo.Click
        Try
            '削除確認ダイアログ表示
            Dim rtn As DialogResult = _msgHd.dspMSG("confDeleteSTaisyohin")   '選択行を一覧から削除します。よろしいですか？
            If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            '一覧選択行削除
            Call deleteRowDgv()

            '完了メッセージ
            Call _msgHd.dspMSG("completeDelete")          '削除が完了しました。

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　登録ボタン(削除ボタン)押下イベント
    '　(処理概要)新規モードの場合は登録、削除モードの場合は削除を行う。
    '-------------------------------------------------------------------------------
    Private Sub btnTouroku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTouroku.Click
        Try

            If _sinkiFlg Then
                '新規登録
                Try

                    '品名コード入力チェック
                    Call checkInputHinmeiCD()

                    '重複キーチェック
                    Call checkHinmeiCDRepeat()

                    'キー項目入力チェック
                    Call checkInputKey()

                Catch ue As UsrDefException
                    ue.dspMsg()
                    Exit Sub
                End Try

                '登録確認ダイアログ表示
                Dim rtn As DialogResult = _msgHd.dspMSG("confirmInsert")   '登録します。よろしいですか？
                If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If

                'マウスカーソル砂時計
                Dim cur As Cursor = Me.Cursor
                Me.Cursor = Cursors.WaitCursor
                Try

                    'データ追加
                    Call insertDB()

                Finally
                    'マウスカーソル矢印
                    Me.Cursor = cur
                End Try

                '完了メッセージ
                Call _msgHd.dspMSG("completeInsert")          '登録が完了しました。

            Else
                '削除
                Try
                    '品名コード入力チェック
                    Call checkInputHinmeiCD()
                Catch ue As UsrDefException
                    ue.dspMsg()
                    Exit Sub
                End Try

                '削除確認ダイアログ表示
                Dim rtn As DialogResult = _msgHd.dspMSG("confirmDelete")   '削除します。よろしいですか？
                If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If

                '-->2010.12.02 del by takagi
                ''マウスカーソル砂時計
                'Me.Cursor = Cursors.WaitCursor
                '
                ''データ削除
                'Call deleteDB()
                '<--2010.12.02 del by takagi

                'マウスカーソル矢印
                Dim cur As Cursor = Me.Cursor
                Me.Cursor = Cursors.WaitCursor
                Try

                    '-->2010.12.02 add by takagi
                    'データ削除
                    Call deleteDB()
                    '<--2010.12.02 add by takagi

                    '完了メッセージ
                    Call _msgHd.dspMSG("completeDelete")          '削除が完了しました。

                    '画面初期化
                    Call clearCtrDelMode()

                    '検索条件入力コントロール初期化・使用可
                    txtSiyo.Focus()             '仕様コード           
                    txtSiyo.Text = ""
                    txtSiyo.BackColor = StartUp.lCOLOR_WHITE
                    txtSiyo.ReadOnly = False
                    txtHinsyu.Text = ""         '品種コード
                    txtHinsyu.BackColor = StartUp.lCOLOR_WHITE
                    txtHinsyu.ReadOnly = False
                    txtSensin.Text = ""         '線心数コード
                    txtSensin.BackColor = StartUp.lCOLOR_WHITE
                    txtSensin.ReadOnly = False
                    txtSize.Text = ""           'サイズコード
                    txtSize.BackColor = StartUp.lCOLOR_WHITE
                    txtSize.ReadOnly = False
                    txtColor.Text = ""          '色コード
                    txtColor.ReadOnly = False
                    txtColor.BackColor = StartUp.lCOLOR_WHITE

                    btnKensaku.Enabled = True   '検索ボタン使用可
                    btnCancel.Enabled = False   'キャンセルボタン使用不可
                    btnTouroku.Enabled = False  '削除ボタン使用不可

                Finally
                    'マウスカーソル矢印
                    Me.Cursor = cur
                End Try

            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　戻るボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnModoru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModoru.Click

        '■親フォーム表示
        _parentForm.Show()
        _parentForm.Activate()
        Me.Close()

    End Sub

#End Region

#Region "ユーザ定義関数:画面制御"

    '-------------------------------------------------------------------------------
    '　画面初期設定
    '-------------------------------------------------------------------------------
    Private Sub initForm()
        Try

            If _sinkiFlg Then
                '新規登録の場合の処理

                '初期値を設定する
                '' 2010/12/27 upd start sugano
                'lblSeisaku.Text = SYOKI_SEISEKI         '成績書
                lblSeiseki.Text = SYOKI_SEISEKI         '成績書
                '' 2010/12/27 upd end sugano
                lblMTantyo.Text = SYOKI_MTANTYO         '持込余長単長毎
                lblMLot.Text = SYOKI_MLOT               '持込余長ロット毎
                lblTTantyo.Text = SYOKI_TTANTYO         '立会余長単長毎
                lblTLot.Text = SYOKI_TLOT               '立会余長ロット毎
                lblSTantyo.Text = SYOKI_STANTYO         '指定社検余長単長毎
                lblSLot.Text = SYOKI_SLOT               '指定社検余長ロット毎
                lblTokki.Text = SYOKI_TOKKI             '特記事項
                lblBikou.Text = SYOKI_BIKOU             '備考
                lblNyuko.Text = SYOKI_NYUUKO            '入庫本数
                lblTourokuFlg.Text = SYOKI_TOUROKUFLG   '登録フラグ
                lblHenko.Text = SYOKI_HENKOU            '変更内容

                '子画面を呼べる項目の初期値の表示と、それに伴う自動表示の設定
                Call setSyokiti()

                '新規登録モード時は検索ボタン非表示
                btnKensaku.Visible = False

                'キャンセルボタン・登録ボタンは品名表示するまで使用不可
                btnCancel.Enabled = False
                btnTouroku.Enabled = False

                '画面モード表示
                lblJoutai.Text = SINKITOUROKU       '新規登録

                'コンボボックス作成
                Call createCbo()

            Else
                '削除の場合の処理

                '「新規ボタン」の名前を「削除ボタン」に変更する。
                btnTouroku.Text = "削除(&E)"

                '削除モード時は品名表示ボタン使用不可
                btnHinmeiHyouji.Visible = False

                '検索・戻るボタン以外の全ての機能を使用不可とする。
                Call deleteMode()

            End If

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　削除モードの画面表示
    '　(処理概要)検索用コントロール以外を使用不可にする。テキストボックスは黄色に着色。
    '-------------------------------------------------------------------------------
    Private Sub deleteMode()

        btnHinmeiHyouji.Enabled = False     '品名表示ボタン
        txtSeisaku.ReadOnly = True          '製作区分
        txtSeisaku.BackColor = StartUp.lCOLOR_YELLOW
        btnSeisaku.Enabled = False          '製作区分ボタン
        txtHyoujunLot.ReadOnly = True       '標準ロット長
        txtHyoujunLot.BackColor = StartUp.lCOLOR_YELLOW
        txtTantyou.ReadOnly = True          '単長
        txtTantyou.BackColor = StartUp.lCOLOR_YELLOW
        txtSumiHonsu.ReadOnly = True        '住電日立分
        txtSumiHonsu.BackColor = StartUp.lCOLOR_YELLOW
        txtZaiko.ReadOnly = True            '在庫・繰返
        txtZaiko.BackColor = StartUp.lCOLOR_YELLOW
        btnZaiko.Enabled = False            '在庫・繰返ボタン
        txtChumonsaki.Enabled = True        '注文先
        txtChumonsaki.ReadOnly = True
        txtJuyousaki.ReadOnly = True        '需要先
        txtJuyousaki.BackColor = StartUp.lCOLOR_YELLOW
        btnJuyousaki.Enabled = False        '需要先ボタン
        txtSizeTenkai.ReadOnly = True       'サイズ展開
        txtSizeTenkai.BackColor = StartUp.lCOLOR_YELLOW
        btnSizeTenkai.Enabled = False       'サイズ展開ボタン
        cboHinsyuKbn.Enabled = False        '品種区分
        cboHinsyuKbn.BackColor = StartUp.lCOLOR_YELLOW
        txtKijunTuki.ReadOnly = True        '基準在庫月数
        txtKijunTuki.BackColor = StartUp.lCOLOR_YELLOW
        txtSaigai.ReadOnly = True           '災害復旧用在庫量
        txtSaigai.BackColor = StartUp.lCOLOR_YELLOW
        txtAnzenZ.ReadOnly = True           '安全在庫量
        txtAnzenZ.BackColor = StartUp.lCOLOR_YELLOW
        txtSSiyo.ReadOnly = True            '集計対象品　仕様
        txtSSiyo.BackColor = StartUp.lCOLOR_YELLOW
        txtSHinsyu.ReadOnly = True          '集計対象品　品種
        txtSHinsyu.BackColor = StartUp.lCOLOR_YELLOW
        txtSSensin.ReadOnly = True          '集計対象品　線心数
        txtSSensin.BackColor = StartUp.lCOLOR_YELLOW
        txtSSize.ReadOnly = True            '集計対象品　サイズ
        txtSSize.BackColor = StartUp.lCOLOR_YELLOW
        txtSColor.ReadOnly = True           '集計対象品　色
        txtSColor.BackColor = StartUp.lCOLOR_YELLOW
        btnTuika.Enabled = False            '集計対象品名追加ボタン
        btnSakujo.Enabled = False           '集計対象品名削除ボタン
        dgvSTaisyou.ReadOnly = True         '一覧
        dgvSTaisyou.Enabled = False
        txtKamoku.Enabled = False           '科目コード
        txtKamoku.BackColor = StartUp.lCOLOR_YELLOW
        txtMakiwaku.ReadOnly = True         '巻枠コード
        txtMakiwaku.BackColor = StartUp.lCOLOR_YELLOW
        txtHousou.ReadOnly = True           '包装／表示区分
        txtHousou.BackColor = StartUp.lCOLOR_YELLOW
        txtSiyousyo.ReadOnly = True         '仕様書番号
        txtSiyousyo.BackColor = StartUp.lCOLOR_YELLOW
        txtSeizouBmn.ReadOnly = True        '製造部門
        txtSeizouBmn.BackColor = StartUp.lCOLOR_YELLOW
        btnSeizouBmn.Enabled = False        '製造部門ボタン
        txtTenkaiKbn.ReadOnly = True        '展開区分
        txtTenkaiKbn.BackColor = StartUp.lCOLOR_YELLOW
        btnTenkaiKbn.Enabled = False        '展開区分ボタン
        txtBTenkai.ReadOnly = True          '部分展開工程
        txtBTenkai.Enabled = True
        txtBTenkai.BackColor = StartUp.lCOLOR_YELLOW
        btnTenkaiKbn.Enabled = False        '部分展開工程ボタン
        txtHinsitu.ReadOnly = True          '品質試験区分
        txtHinsitu.BackColor = StartUp.lCOLOR_YELLOW
        btnHinsitu.Enabled = False          '品質試験区分ボタン
        txtTatiai.ReadOnly = True           '立会有無
        txtTatiai.BackColor = StartUp.lCOLOR_YELLOW
        btnTatiai.Enabled = False           '立会有無ボタン
        btnTouroku.Enabled = False          '削除ボタン
        btnCancel.Visible = True            'キャンセルボタン
        btnCancel.Enabled = False

        '画面モード表示
        lblJoutai.Text = SAKUJO             '削除

    End Sub

    '-------------------------------------------------------------------------------
    '　各コントロールの値クリア
    '　(処理概要)検索に失敗・検索をやり直す場合に、各コントロールの値をクリアする。
    '-------------------------------------------------------------------------------
    Private Sub clearCtrDelMode()
        Try
            txtSeisaku.Text = ""        '製作区分
            lblSeisaku.Text = ""        '製作区分ラベル
            lblHinmei.Text = ""         '品名
            txtHyoujunLot.Text = ""     '標準ロット長
            txtTantyou.Text = ""        '単長
            lblJosu.Text = ""           '条数
            lblKNDHonsu.Text = ""       '北日本分
            txtSumiHonsu.Text = ""      '住電日立分
            txtZaiko.Text = ""          '在庫・繰返
            lblZaiko.Text = ""          '在庫・繰返ラベル
            txtChumonsaki.Text = ""     '注文先ラベル
            txtJuyousaki.Text = ""      '需要先
            lblJuyousaki.Text = ""      '需要先ラベル
            lblABC.Text = ""            'ABCラベル
            txtSizeTenkai.Text = ""     'サイズ展開
            lblSizeTenkai.Text = ""     'サイズ展開ラベル
            cboHinsyuKbn.Text = ""      '品種区分
            lblHinsyuKbn.Text = ""      '品種区分ラベル
            txtKijunTuki.Text = ""      '基準在庫月数
            txtSaigai.Text = ""         '災害復旧用在庫量
            txtAnzenZ.Text = ""         '安全在庫量
            txtKamoku.Text = ""         '科目コード
            txtSSiyo.Text = ""          '集計対象品仕様コード
            txtSHinsyu.Text = ""        '集計対象品品種コード
            txtSSensin.Text = ""        '集計対象品線心数コード
            txtSSize.Text = ""          '集計対象品サイズコード
            txtSColor.Text = ""         '集計対象品色コード
            txtMakiwaku.Text = ""       '巻枠コード
            lblMakiwaku.Text = ""       '巻枠コードラベル
            txtHousou.Text = ""         '包装／表示区分
            lblHousou.Text = ""         '包装／表示区分ラベル
            txtSiyousyo.Text = ""       '仕様書番号
            txtSeizouBmn.Text = ""      '製造部門
            lblSeizoBmn.Text = ""       '製造部門ラベル
            lblTehaiKbn.Text = ""       '手配区分
            lblTehaiKbnNM.Text = ""     '手配区分ラベル
            txtTenkaiKbn.Text = ""      '展開区分
            lblTenkaiKbn.Text = ""      '展開区分ラベル
            txtBTenkai.Text = ""        '部分展開工程ラベル
            txtHinsitu.Text = ""        '品質試験区分
            lblHinsitu.Text = ""        '品質試験区分ラベル
            txtTatiai.Text = ""         '立会有無
            lblTatiai.Text = ""         '立会有無ラベル
            lblSyoriKbn.Text = ""       '処理区分
            lblSyoriKbnNM.Text = ""     '処理区分名
            lblKako.Text = ""           '加工長計算区分
            lblKakoNM.Text = ""         '加工長計算区分名
            lblTantyo.Text = ""         '単長区分
            lblTantyoNM.Text = ""       '単長区分名
            lblSeiseki.Text = ""        '成績書
            lblMTantyo.Text = ""        '持込余長単長毎
            lblMLot.Text = ""           '持込余長ロット毎
            lblTTantyo.Text = ""        '立会余長単長毎
            lblTLot.Text = ""           '立会余長ロット毎
            lblSTantyo.Text = ""        '指定社検余長単長毎
            lblSLot.Text = ""           '指定社検余長ロット毎
            lblTokki.Text = ""          '特記事項
            lblBikou.Text = ""          '備考
            lblNyuko.Text = ""          '入庫本数
            lblTourokuFlg.Text = ""     '登録フラグ
            lblHenko.Text = ""          '変更内容

            'コンボボックスクリア
            Me.cboHinsyuKbn.Items.Clear()

            '一覧のクリア
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSTaisyou)
            gh.clearRow()

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　製作区分入力時
    '　(処理概要)入力値に対応する名称をラベルに表示し、展開区分・品質試験区分の値を自動表示する。
    '-------------------------------------------------------------------------------
    Private Sub txtSeisaku_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSeisaku.Leave
        Try

            If "".Equals(txtSeisaku.Text) Then
                lblSeisaku.Text = ""
                Exit Sub
            End If

            'ラベル表示
            Dim lblStr As String = serchName(txtSeisaku.Text, HKOTEIKEY_SEISAKU_KBN, lblSeisaku)
            If "".Equals(lblStr) Then
                If _sinkiFlg Then txtSeisaku.Focus()
                '-->2010.12.22 chg by takagi #38
                'Throw New UsrDefException("汎用マスタに登録されていない値です。", _msgHd.getMSG("noExistHanyouMst", "【製作区分】"))
                If _sinkiFlg Then Throw New UsrDefException("汎用マスタに登録されていない値です。", _msgHd.getMSG("noExistHanyouMst", "【製作区分】"))
                '<--2010.12.22 chg by takagi #38
            Else
                lblSeisaku.Text = lblStr
            End If

            If _sinkiFlg Then
                '展開区分・品質試験区分自動表示、科目コードの使用可否設定
                Call txtSeisakuChange()
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　展開区分・品質試験区分の自動表示、科目コードの使用可否設定
    '　(処理概要)入力値をもとに展開区分・品質試験区分の値を自動表示する。
    '            科目コードの使用可否設定を行う。
    '-------------------------------------------------------------------------------
    Private Sub txtSeisakuChange()
        Try
            '製作区分の入力内容によって以下のコントロールを制御
            If SEISAKUKBNCD_NAISAKU.Equals(txtSeisaku.Text) Then
                '展開区分
                txtTenkaiKbn.Text = TENKAIKBNCD_ZEN
                '品質試験区分
                txtHinsitu.Text = HINSITUCD_LOTKANRI
                '科目コード　使用不可
                txtKamoku.Text = ""
                txtKamoku.ReadOnly = True
                txtKamoku.Enabled = False
                txtKamoku.BackColor = StartUp.lCOLOR_YELLOW
            ElseIf SEISAKUKBNCD_GAITYU.Equals(txtSeisaku.Text) Then
                '展開区分
                txtTenkaiKbn.Text = TENKAIKBNCD_BUBUN
                '品質試験区分
                txtHinsitu.Text = HINSITUCD_YOTYOUFUYO
                '科目コード　使用可
                txtKamoku.ReadOnly = False
                txtKamoku.Enabled = True
                txtKamoku.BackColor = StartUp.lCOLOR_WHITE
            Else
                Exit Sub
            End If

            '部分展開指定工程自動設定
            Call txtTenkaiKbn_Leave(txtTenkaiKbn, Nothing)

            '品質試験区分自動設定
            Call txtInput_Leave(txtHinsitu, Nothing)

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　在庫・繰返入力時
    '　(処理概要)入力値に対応する名称をラベルに表示し、手配区分の値を自動表示する。
    '-------------------------------------------------------------------------------
    Private Sub txtZaiko_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtZaiko.Leave
        Try

            If "".Equals(txtZaiko.Text) Then
                lblZaiko.Text = ""
                Exit Sub
            End If

            'ラベル表示
            Dim lblStr As String = serchName(txtZaiko.Text, HKOTEIKEY_ZAIKO_KBN, lblZaiko)
            If "".Equals(lblStr) Then
                If _sinkiFlg Then txtZaiko.Focus()
                '-->2010.12.22 chg by takagi #38
                'Throw New UsrDefException("汎用マスタに登録されていない値です。", _msgHd.getMSG("noExistHanyouMst", "【在庫・繰返】"))
                If _sinkiFlg Then Throw New UsrDefException("汎用マスタに登録されていない値です。", _msgHd.getMSG("noExistHanyouMst", "【在庫・繰返】"))
                '<--2010.12.22 chg by takagi #38
            Else
                lblZaiko.Text = lblStr
            End If

            If ZAIKOCD_ZAIKO.Equals(txtZaiko.Text) Then
                '計画情報入力可
                Call notEnableKeikakujouhou(True)
            Else
                '計画情報入力不可
                Call notEnableKeikakujouhou(False)
            End If
            If _sinkiFlg Then
                '手配区分・注文先自動表示
                Call txtZaikoChange()
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　計画情報入力不可
    '　(処理概要)在庫・繰返が「1」以外の場合は、計画情報を入力不可とする
    '-------------------------------------------------------------------------------
    Private Sub notEnableKeikakujouhou(ByVal prmZaikoInputFlg As Boolean)
        Try

            If _sinkiFlg Then
                Dim zaikoEnabled As Boolean = False         'Enabledフラグ
                Dim zaikoReadOnly As Boolean = False        'ReadOnlyフラグ
                Dim backColor As Color                      '背景色

                If prmZaikoInputFlg Then                    '計画情報入力可
                    zaikoEnabled = True
                    zaikoReadOnly = False
                    backColor = StartUp.lCOLOR_WHITE
                Else                                        '計画情報入力不可
                    zaikoEnabled = False
                    zaikoReadOnly = True
                    backColor = StartUp.lCOLOR_YELLOW
                End If

                txtJuyousaki.Enabled = zaikoEnabled         '需要コード
                txtJuyousaki.ReadOnly = zaikoReadOnly
                txtJuyousaki.BackColor = backColor
                btnJuyousaki.Enabled = zaikoEnabled         '需要コードボタン
                txtSizeTenkai.Enabled = zaikoEnabled        'サイズ展開
                txtSizeTenkai.ReadOnly = zaikoReadOnly
                txtSizeTenkai.BackColor = backColor
                btnSizeTenkai.Enabled = zaikoEnabled        'サイズ展開ボタン
                cboHinsyuKbn.Enabled = zaikoEnabled         '品種区分コンボ
                cboHinsyuKbn.BackColor = backColor
                txtKijunTuki.Enabled = zaikoEnabled         '基準月数
                txtKijunTuki.ReadOnly = zaikoReadOnly
                txtKijunTuki.BackColor = backColor
                txtSaigai.Enabled = zaikoEnabled            '災害復旧用在庫
                txtSaigai.ReadOnly = zaikoReadOnly
                txtSaigai.BackColor = backColor
                txtAnzenZ.Enabled = zaikoEnabled            '安全在庫量
                txtAnzenZ.ReadOnly = zaikoReadOnly
                txtAnzenZ.BackColor = backColor
                txtSSiyo.Enabled = zaikoEnabled             '集計対象品仕様コード
                txtSSiyo.ReadOnly = zaikoReadOnly
                txtSSiyo.BackColor = backColor
                txtSHinsyu.Enabled = zaikoEnabled           '集計対象品品種コード
                txtSHinsyu.ReadOnly = zaikoReadOnly
                txtSHinsyu.BackColor = backColor
                txtSSensin.Enabled = zaikoEnabled           '集計対象品線心数コード
                txtSSensin.ReadOnly = zaikoReadOnly
                txtSSensin.BackColor = backColor
                txtSSize.Enabled = zaikoEnabled             '集計対象品サイズコード
                txtSSize.ReadOnly = zaikoReadOnly
                txtSSize.BackColor = backColor
                txtSColor.Enabled = zaikoEnabled            '集計対象品色コード
                txtSColor.ReadOnly = zaikoReadOnly
                txtSColor.BackColor = backColor
                btnTuika.Enabled = zaikoEnabled             '一覧追加ボタン
                btnSakujo.Enabled = zaikoEnabled            '一覧削除ボタン
                dgvSTaisyou.Enabled = zaikoEnabled          '集計対象品一覧
                dgvSTaisyou.ReadOnly = zaikoReadOnly
                lblKensuu.Text = "0件"                      '集計対象品一覧件数

                '入力不可の場合は入力値をクリア
                If Not prmZaikoInputFlg Then
                    txtJuyousaki.Text = ""              '需要コード
                    lblJuyousaki.Text = ""              '需要コードラベル
                    lblABC.Text = ""                    'ABC区分
                    txtSizeTenkai.Text = ""             'サイズ展開
                    lblSizeTenkai.Text = ""             'サイズ展開ラベル
                    Me.cboHinsyuKbn.Items.Clear()       '品種区分コンボ
                    lblHinsyuKbn.Text = ""              '品種区分ラベル
                    txtKijunTuki.Text = ""              '基準月数
                    txtSaigai.Text = ""                 '災害復旧用在庫
                    txtAnzenZ.Text = ""                 '安全在庫量
                    txtSSiyo.Text = ""                  '集計対象品仕様コード
                    txtSHinsyu.Text = ""                '集計対象品品種コード
                    txtSSensin.Text = ""                '集計対象品線心数コード
                    txtSSize.Text = ""                  '集計対象品サイズコード
                    txtSColor.Text = ""                 '集計対象品色コード
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
    '　手配区分・注文先の自動設定
    '　(処理概要)入力値をもとに手配区分・注文先を自動設定する。
    '-------------------------------------------------------------------------------
    Private Sub txtZaikoChange()
        Try

            If ZAIKOCD_ZAIKO.Equals(txtZaiko.Text) Then
                '手配区分
                lblTehaiKbn.Text = "1"
                lblTehaiKbnNM.Text = "在庫"

                '注文先
                txtChumonsaki.Text = ""
                txtChumonsaki.BackColor = StartUp.lCOLOR_YELLOW
                txtChumonsaki.ReadOnly = True
                txtChumonsaki.Enabled = False

                '計画情報入力可
                Call notEnableKeikakujouhou(True)

            ElseIf ZAIKOCD_KURIKAESI.Equals(txtZaiko.Text) Then
                '手配区分
                lblTehaiKbn.Text = "2"
                lblTehaiKbnNM.Text = "受注"

                '注文先
                txtChumonsaki.BackColor = StartUp.lCOLOR_WHITE
                txtChumonsaki.ReadOnly = False
                txtChumonsaki.Enabled = True

                '計画情報入力不可
                Call notEnableKeikakujouhou(False)
            Else
                Exit Sub
            End If

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　需要先入力時
    '　(処理概要)入力値に対応する名称をラベルに表示し、品種区分のコンボボックスを再作成する。
    '-------------------------------------------------------------------------------
    Private Sub txtJuyousaki_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtJuyousaki.Leave
        Try

            If "".Equals(txtJuyousaki.Text) Then
                lblJuyousaki.Text = ""
                Exit Sub
            End If

            'ラベル表示
            Dim lblStr As String = serchName(txtJuyousaki.Text, HKOTEIKEY_JUYOSAKI, lblJuyousaki)
            If "".Equals(lblStr) Then
                If _sinkiFlg Then txtJuyousaki.Focus()
                '-->2010.12.22 chg by takagi
                'Throw New UsrDefException("汎用マスタに登録されていない値です。", _msgHd.getMSG("noExistHanyouMst", "【需要先】"))
                If _sinkiFlg Then Throw New UsrDefException("汎用マスタに登録されていない値です。", _msgHd.getMSG("noExistHanyouMst", "【需要先】"))
                '<--2010.12.22 chg by takagi
            Else
                lblJuyousaki.Text = lblStr
            End If

            If _sinkiFlg Then
                '品種区分コンボの再作成とラベルの初期化
                Call createCbo()
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　展開区分入力時
    '　(処理概要)入力値に対応する名称をラベルに表示し、部分展開区分を自動設定する。
    '-------------------------------------------------------------------------------
    Private Sub txtTenkaiKbn_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTenkaiKbn.Leave
        Try

            '空の場合はラベルをクリアし、部分展開区分を使用不可とする。
            If "".Equals(txtTenkaiKbn.Text) Then
                lblTenkaiKbn.Text = ""
                txtBTenkai.Text = ""
                txtBTenkai.Enabled = False
                txtBTenkai.BackColor = StartUp.lCOLOR_YELLOW
                Exit Sub
            End If

            'ラベル表示
            Dim lblStr As String = serchName(txtTenkaiKbn.Text, HKOTEIKEY_TENKAI_KBN, lblTenkaiKbn)
            If "".Equals(lblStr) Then

                If _sinkiFlg Then txtTenkaiKbn.Focus()
                '-->2010.12.22 chg by takagi
                'Throw New UsrDefException("汎用マスタに登録されていない値です。", _msgHd.getMSG("noExistHanyouMst", "【展開区分】"))
                If _sinkiFlg Then Throw New UsrDefException("汎用マスタに登録されていない値です。", _msgHd.getMSG("noExistHanyouMst", "【展開区分】"))
                '<--2010.12.22 chg by takagi

            Else
                lblTenkaiKbn.Text = lblStr
            End If

            If _sinkiFlg Then
                '部分展開指定工程自動設定
                Call txtTenkaiKbnChange()
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　部分展開指定工程の自動設定
    '　(処理概要)入力値をもとに部分展開指定工程を自動設定する。
    '-------------------------------------------------------------------------------
    Private Sub txtTenkaiKbnChange()
        Try
            If TENKAIKBNCD_ZEN.Equals(txtTenkaiKbn.Text) Then
                txtBTenkai.ReadOnly = True
                txtBTenkai.Enabled = False
                txtBTenkai.Text = ""
                txtBTenkai.BackColor = StartUp.lCOLOR_YELLOW
            ElseIf TENKAIKBNCD_BUBUN.Equals(txtTenkaiKbn.Text) Then
                txtBTenkai.ReadOnly = False
                txtBTenkai.BackColor = StartUp.lCOLOR_WHITE
                txtBTenkai.Enabled = True
            Else
                Exit Sub
            End If

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　ラベル自動表示(新規モード用)
    '　(処理概要)サイズ展開・製造部門・品質試験区分・立会区分のテキストボックス入力値に対応する
    '　　　　　　名称をラベルに自動表示する。
    '-------------------------------------------------------------------------------
    Private Sub txtInput_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSizeTenkai.Leave, _
                                                                                            txtSeizouBmn.Leave, _
                                                                                            txtHinsitu.Leave, _
                                                                                            txtTatiai.Leave
        Try

            '入力されていない場合は、対応するラベルをクリアして処理を抜ける
            If "".Equals(sender.Text) Then
                Select Case True
                    Case txtSizeTenkai.Equals(sender)
                        'サイズ展開
                        lblSizeTenkai.Text = ""
                    Case txtSeizouBmn.Equals(sender)
                        '製造部門
                        lblSeizoBmn.Text = ""
                    Case txtHinsitu.Equals(sender)
                        '品質試験区分
                        lblHinsitu.Text = ""
                    Case txtTatiai.Equals(sender)
                        '立会有無
                        lblTatiai.Text = ""
                End Select

                Exit Sub
            End If

            Dim targetTextBox As TextBox = sender
            Dim targetText As String = ""
            Dim koteikey As String = ""
            Dim targetLabel As Label = Nothing
            Dim errorMsg As String = ""

            'ラベル表示処理に渡す変数を設定
            Select Case True
                Case txtSizeTenkai.Equals(sender)
                    'サイズ展開
                    targetText = txtSizeTenkai.Text
                    koteikey = HKOTEIKEY_SIZETENKAI_KBN
                    targetLabel = lblSizeTenkai
                    errorMsg = "サイズ展開"

                    '製造部門
                Case txtSeizouBmn.Equals(sender)
                    targetText = txtSeizouBmn.Text
                    koteikey = HKOTEIKEY_SEIZO_BMN
                    targetLabel = lblSeizoBmn
                    errorMsg = "製造部門"

                    '品質試験区分
                Case txtHinsitu.Equals(sender)
                    targetText = txtHinsitu.Text
                    koteikey = HKOTEIKEY_HINSHITU_KBN
                    targetLabel = lblHinsitu
                    errorMsg = "品質試験区分"

                    '立会有無
                Case txtTatiai.Equals(sender)
                    targetText = txtTatiai.Text
                    koteikey = HKOTEIKEY_TACHIAI_UM
                    targetLabel = lblTatiai
                    errorMsg = "立会有無"
            End Select

            'ラベル表示
            Dim lblStr As String = serchName(targetText, koteikey, targetLabel)
            If "".Equals(lblStr) Then
                If _sinkiFlg Then targetTextBox.Focus()
                '-->2010.12.22 chg by takagi
                'Throw New UsrDefException("汎用マスタに登録されていない値です。", _msgHd.getMSG("noExistHanyouMst", "【" & errorMsg & "】"))
                If _sinkiFlg Then Throw New UsrDefException("汎用マスタに登録されていない値です。", _msgHd.getMSG("noExistHanyouMst", "【" & errorMsg & "】"))
                '<--2010.12.22 chg by takagi
            Else
                targetLabel.Text = lblStr
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　品種区分入力時
    '　(処理概要)入力値に対応する名称をラベルに表示する。
    '-------------------------------------------------------------------------------
    Private Sub cboHinsyuKbn_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboHinsyuKbn.Leave
        Try
            'ラベル表示
            Call dispCboLabel()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　巻枠コード入力時
    '　(処理概要)入力値に対応する名称をラベルに表示する。
    '-------------------------------------------------------------------------------
    Private Sub txtMakiwaku_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMakiwaku.Leave
        Try

            'ラベル表示
            Call dispMakiwakuLabel()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　包装／表示区分入力時
    '　(処理概要)入力値に対応する名称をラベルに表示する。
    '-------------------------------------------------------------------------------
    Private Sub txtHousou_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtHousou.Leave
        Try

            'ラベル表示
            Call dispHousouLabel()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    ' 　入庫本数 住電日立分変更    
    '　(処理概要)住電日立分に入力された値を元に、北日本分を再計算して表示する。
    '-------------------------------------------------------------------------------
    Private Sub txtSumiHonsu_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSumiHonsu.LostFocus
        Try

            '条数がある場合
            If Not "".Equals(lblJosu.Text) Then
                '住電日立分が条数より少ない場合
                If _db.rmNullInt(txtSumiHonsu.Text) <= _db.rmNullInt(lblJosu.Text) Then
                    '北日本分を自動計算
                    lblKNDHonsu.Text = _db.rmNullInt(lblJosu.Text) - _db.rmNullInt(txtSumiHonsu.Text)
                End If

                '住電日立分が未入力の場合、0を表示
                If "".Equals(txtSumiHonsu.Text) Then
                    txtSumiHonsu.Text = "0"
                End If
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   条数の自動計算
    ' 　(処理概要)標準ロット長または単長が変更された場合、条数の自動計算を行う。
    '-------------------------------------------------------------------------------
    Private Sub numLotLen_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHyoujunLot.LostFocus, _
                                                                                                        txtTantyou.LostFocus
        Try

            '標準ロット長及び単長の両方が入力されている場合のみ処理を行う。
            If "".Equals(txtHyoujunLot.Text) Or "".Equals(txtTantyou.Text) Then
                Exit Sub
            End If

            '条数の計算
            If CInt(txtHyoujunLot.Text) Mod CInt(txtTantyou.Text) <> 0 Then
                '割り切れない場合は「0」にしておく
                lblJosu.Text = "0"
                Exit Sub
            End If
            lblJosu.Text = Format(CInt(txtHyoujunLot.Text) / CInt(txtTantyou.Text), "#,##0")

            '入庫本数に条数の値をセットする
            lblNyuko.Text = lblJosu.Text

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    ' 　汎用マスタ可変キーの受け取り
    '-------------------------------------------------------------------------------
    Sub setKahenKey(ByVal prmKahenKey As String, ByVal prmMeisyo As String) Implements IfRturnKahenKey.setKahenKey
        Try

            _ZC910KahenKey = prmKahenKey
            _ZC910Meisyo = prmMeisyo

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

    '-------------------------------------------------------------------------------
    '　コントロールキー押下イベント
    '　(処理概要)エンターボタン押下時に次のコントロールに移る
    '-------------------------------------------------------------------------------
    Private Sub txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSiyo.KeyPress, _
                                                                                            txtHinsyu.KeyPress, _
                                                                                            txtSensin.KeyPress, _
                                                                                            txtSize.KeyPress, _
                                                                                            txtColor.KeyPress, _
                                                                                            txtSeisaku.KeyPress, _
                                                                                            txtHyoujunLot.KeyPress, _
                                                                                            txtTantyou.KeyPress, _
                                                                                            txtSumiHonsu.KeyPress, _
                                                                                            txtZaiko.KeyPress, _
                                                                                            txtJuyousaki.KeyPress, _
                                                                                            txtSizeTenkai.KeyPress, _
                                                                                            txtKijunTuki.KeyPress, _
                                                                                            txtSaigai.KeyPress, _
                                                                                            txtAnzenZ.KeyPress, _
                                                                                            txtSSiyo.KeyPress, _
                                                                                            txtSHinsyu.KeyPress, _
                                                                                            txtSSensin.KeyPress, _
                                                                                            txtSSize.KeyPress, _
                                                                                            txtSColor.KeyPress, _
                                                                                            txtKamoku.KeyPress, _
                                                                                            txtMakiwaku.KeyPress, _
                                                                                            txtHousou.KeyPress, _
                                                                                            txtSiyousyo.KeyPress, _
                                                                                            txtSeizouBmn.KeyPress, _
                                                                                            txtTenkaiKbn.KeyPress, _
                                                                                            txtBTenkai.KeyPress, _
                                                                                            txtHinsitu.KeyPress, _
                                                                                            txtTatiai.KeyPress, _
                                                                                            txtChumonsaki.KeyPress, _
                                                                                            cboHinsyuKbn.KeyPress

        UtilClass.moveNextFocus(Me, e) '次のコントロールへ移動する 

    End Sub

    '-------------------------------------------------------------------------------
    '　コントロール全選択
    '　(処理概要)コントロール移動時に全選択状態にする
    '-------------------------------------------------------------------------------
    Private Sub serch_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSiyo.GotFocus, _
                                                                                            txtHinsyu.GotFocus, _
                                                                                            txtSensin.GotFocus, _
                                                                                            txtSize.GotFocus, _
                                                                                            txtColor.GotFocus, _
                                                                                            txtSeisaku.GotFocus, _
                                                                                            txtHyoujunLot.GotFocus, _
                                                                                            txtTantyou.GotFocus, _
                                                                                            txtSumiHonsu.GotFocus, _
                                                                                            txtZaiko.GotFocus, _
                                                                                            txtJuyousaki.GotFocus, _
                                                                                            txtSizeTenkai.GotFocus, _
                                                                                            txtKijunTuki.GotFocus, _
                                                                                            txtSaigai.GotFocus, _
                                                                                            txtAnzenZ.GotFocus, _
                                                                                            txtSSiyo.GotFocus, _
                                                                                            txtSHinsyu.GotFocus, _
                                                                                            txtSSensin.GotFocus, _
                                                                                            txtSSize.GotFocus, _
                                                                                            txtSColor.GotFocus, _
                                                                                            txtKamoku.GotFocus, _
                                                                                            txtMakiwaku.GotFocus, _
                                                                                            txtHousou.GotFocus, _
                                                                                            txtSiyousyo.GotFocus, _
                                                                                            txtSeizouBmn.GotFocus, _
                                                                                            txtTenkaiKbn.GotFocus, _
                                                                                            txtBTenkai.GotFocus, _
                                                                                            txtHinsitu.GotFocus, _
                                                                                            txtTatiai.GotFocus, _
                                                                                            cboHinsyuKbn.GotFocus


        UtilClass.selAll(sender)

    End Sub


#End Region

#Region "ユーザ定義関数:DGV関連"

    '------------------------------------------------------------------------------------------------------
    '選択行に着色する処理
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvSHinmei_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvSTaisyou.SelectionChanged
        If _colorCtlFlg Then
            Dim gh As UtilMDL.DataGridView.UtilDataGridViewHandler = New UtilMDL.DataGridView.UtilDataGridViewHandler(dgvSTaisyou)
            gh.setSelectionRowColor(dgvSTaisyou.CurrentCellAddress.Y, _oldRowIndex, StartUp.lCOLOR_BLUE)
        End If
        _oldRowIndex = dgvSTaisyou.CurrentCellAddress.Y
    End Sub

#End Region

#Region "ユーザ定義関数:DB関連"

    '-------------------------------------------------------------------------------
    '　 削除データ表示
    '   （処理概要）品名コードをもとにM11およびM12のデータを画面に表示する。
    '-------------------------------------------------------------------------------
    Private Sub dispData()
        Try

            '仕様コードが1桁の場合は、半角スペースを加えて2桁にする
            Dim siyoCd As String = _db.rmSQ(txtSiyo.Text)
            If _db.rmSQ(txtSiyo.Text).Length = 1 Then
                siyoCd = siyoCd & " "
            End If

            'M11
            Dim sql As String = ""
            sql = sql & N & " SELECT "
            sql = sql & N & "  TT_TANCYO " & DB_TANTYO              '単長
            sql = sql & N & " ,TT_HINMEI " & DB_HINMEI              '品名
            sql = sql & N & " ,TT_SYORI_KBN " & DB_SYORIKBN         '処理区分
            sql = sql & N & " ,TT_TEHAI_KBN " & DB_TEHAIKBN         '手配区分
            sql = sql & N & " ,TT_SEISAKU_KBN " & DB_SEISAKU        '製作区分
            sql = sql & N & " ,TT_KYAKSAKI " & DB_TYUMONSAKI        '注文先
            sql = sql & N & " ,TT_TENKAI_KBN " & DB_TENKAIKBN       '展開区分
            sql = sql & N & " ,TT_KOUTEI " & DB_BUBUNTENKAI         '部分展開工程
            sql = sql & N & " ,TT_KEISAN_KBN " & DB_KAKOU           '加工長計算部分
            sql = sql & N & " ,TT_TATIAI_UM " & DB_TATIAI           '立会有無
            sql = sql & N & " ,TT_TANCYO_KBN " & DB_TANTYOKBN       '単長区分
            sql = sql & N & " ,TT_MAKI_CD " & DB_MAKIWAKU           '巻枠
            sql = sql & N & " ,TT_HOSO_KBN " & DB_HOUSOU            '包装／表示区分
            sql = sql & N & " ,TT_HINSITU_KBN " & DB_HINSITU        '品質試験区分
            sql = sql & N & " ,TT_SIYOUSYO_NO " & DB_SIYOUSYO       '仕様書番号
            sql = sql & N & " ,TT_SEIZOU_BMN " & DB_SEIZOUBMN       '製造部門
            sql = sql & N & " ,TT_KAMOKU_CD " & DB_KAMOKU           '科目コード
            sql = sql & N & " ,TT_N_SO_SUU " & DB_NYUUKO            '入庫本数
            sql = sql & N & " ,TT_N_K_SUU " & DB_KND                '北日本分
            sql = sql & N & " ,TT_N_SH_SUU " & DB_SUMIDEN           '住電日立分
            sql = sql & N & " ,TT_SEISEKI " & DB_SEISEKI            '成績書
            sql = sql & N & " ,TT_MYTANCYO " & DB_MTANTYO           '持込余長単長毎
            sql = sql & N & " ,TT_MYLOT " & DB_MLOT                 '持込余長ロット毎
            sql = sql & N & " ,TT_TYTANCYO " & DB_TTANTYO           '立会余長単長毎
            sql = sql & N & " ,TT_TYLOT " & DB_TLOT                 '立会余長ロット毎
            sql = sql & N & " ,TT_SYTANCYO " & DB_STANTYO           '指定社検余長単長毎
            sql = sql & N & " ,TT_SYLOT " & DB_SLOT                 '指定社検余長ロット毎
            sql = sql & N & " ,TT_TOKKI " & DB_TOKKI                '特記事項
            sql = sql & N & " ,TT_BIKO " & DB_BIKOU                 '備考
            sql = sql & N & " ,TT_HENKO " & DB_HENKOU               '変更内容
            sql = sql & N & " ,TT_JYOSU " & DB_JOSU                 '条数
            sql = sql & N & " ,TT_INSFLG " & DB_TOUROKU             '登録フラグ
            sql = sql & N & " ,TT_SYUBETU " & DB_ZAIKO              '在庫・繰返
            sql = sql & N & " ,TT_LOT " & DB_LOTTYO                 '標準ロット長
            sql = sql & N & " ,TT_JUYOUCD " & DB_JUYOSAKI           '需要先
            sql = sql & N & " ,TT_ABCKBN " & DB_ABC                 'ABC
            sql = sql & N & " ,TT_HINSYUKBN " & DB_HINSYUKBN        '品種区分
            sql = sql & N & " ,TT_TENKAIPTN " & DB_SIZETENKAI       'サイズ展開
            sql = sql & N & " ,TT_KZAIKOTUKISU " & DB_KIJUNTUKI     '基準月数
            sql = sql & N & " ,TT_SFUKKYUU " & DB_SAIGAI            '災害復旧用在庫量
            sql = sql & N & " ,TT_ANNZENZAIKO " & DB_ANZEN          '安全在庫量
            sql = sql & N & " FROM M11KEIKAKUHIN "
            sql = sql & N & " WHERE "

            'sql = sql & N & "      TT_H_SIYOU_CD = '" & siyoCd & "' AND "
            sql = sql & N & "      TT_H_SIYOU_CD = '" & siyoCd.PadRight(2, " ") & "' AND "

            sql = sql & N & "      TT_H_HIN_CD = '" & _db.rmSQ(txtHinsyu.Text) & "' AND "
            sql = sql & N & "      TT_H_SENSIN_CD = '" & _db.rmSQ(txtSensin.Text) & "' AND "
            sql = sql & N & "      TT_H_SIZE_CD = '" & _db.rmSQ(txtSize.Text) & "' AND "
            sql = sql & N & "      TT_H_COLOR_CD = '" & _db.rmSQ(txtColor.Text) & "'"

            'SQL発行
            Dim iRecCntM11 As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCntM11)

            If iRecCntM11 <= 0 Then         '抽出レコードが１件もない場合
                txtSiyo.Focus()
                Call clearCtrDelMode()      '各コントロールのクリア
                Throw New UsrDefException("品名コードが計画対象品マスタに登録されていません。", _msgHd.getMSG("NonKTaisyohinMst"))
            End If

            'M12
            sql = ""
            sql = sql & N & "SELECT "
            sql = sql & N & " M12.HINMEICD " & COLDT_KHINMEICD
            sql = sql & N & ",(MPE.HINSYU_MEI "
            sql = sql & N & "		|| MPE.SAIZU_MEI"
            sql = sql & N & "		|| MPE.IRO_MEI) " & COLDT_KHINMEI
            sql = sql & N & " FROM M12SYUYAKU M12 "
            '2014/06/04 UPD-S Sugano
            'sql = sql & N & "	 INNER JOIN MPESEKKEI MPE ON"
            'sql = sql & N & "	 M12.KHINMEICD = "
            'sql = sql & N & "		(MPE.SHIYO "
            'sql = sql & N & "		|| LPAD(TO_CHAR(MPE.HINSYU)  ,3,'0')"
            'sql = sql & N & "		|| LPAD(TO_CHAR(MPE.SENSHIN)  ,3,'0')"
            'sql = sql & N & "		|| LPAD(TO_CHAR(MPE.SAIZU)  ,2,'0')"
            'sql = sql & N & "		|| LPAD(TO_CHAR(MPE.IRO)  ,3,'0'))"
            'sql = sql & N & " WHERE MPE.SHIYO = '" & siyoCd & "' AND "
            'sql = sql & N & "       MPE.HINSYU = '" & _db.rmSQ(txtHinsyu.Text) & "' AND "
            'sql = sql & N & "       MPE.SENSHIN = '" & _db.rmSQ(txtSensin.Text) & "' AND "
            'sql = sql & N & "       MPE.SAIZU = '" & _db.rmSQ(txtSize.Text) & "' AND "
            'sql = sql & N & "       MPE.IRO = '" & _db.rmSQ(txtColor.Text) & "' "
            'sql = sql & N & " AND MPE.SEQ_NO = " & INT_SEQNO
            'sql = sql & N & " AND MPE.SEKKEI_HUKA = "
            'sql = sql & N & "           (SELECT MAX(SEKKEI_HUKA) FROM MPESEKKEI "
            'sql = sql & N & "               WHERE SHIYO = '" & siyoCd & "' AND "
            'sql = sql & N & "                   HINSYU = '" & _db.rmSQ(txtHinsyu.Text) & "' AND "
            'sql = sql & N & "                   SENSHIN = '" & _db.rmSQ(txtSensin.Text) & "' AND "
            'sql = sql & N & "                   SAIZU = '" & _db.rmSQ(txtSize.Text) & "' AND "
            'sql = sql & N & "                   IRO = '" & _db.rmSQ(txtColor.Text) & "') AND "
            'sql = sql & N & " WHERE NOT M12.KHINMEICD = M12.HINMEICD "
            sql = sql & N & "	 INNER JOIN (SELECT M1.* FROM MPESEKKEI1 M1 "
            sql = sql & N & "	             INNER JOIN (SELECT SHIYO,HINSYU,SENSHIN,SAIZU,IRO,SEKKEI_FUKA,MAX(SEKKEI_KAITEI) KAITEI FROM MPESEKKEI1 WHERE SEKKEI_FUKA = 'A' "
            sql = sql & N & "	                         GROUP BY SHIYO,HINSYU,SENSHIN,SAIZU,IRO,SEKKEI_FUKA) M2 "
            sql = sql & N & "	             ON  M1.SHIYO = M2.SHIYO "
            sql = sql & N & "	             AND M1.HINSYU = M2.HINSYU "
            sql = sql & N & "	             AND M1.SENSHIN = M2.SENSHIN "
            sql = sql & N & "	             AND M1.SAIZU = M2.SAIZU "
            sql = sql & N & "	             AND M1.IRO = M2.IRO "
            sql = sql & N & "	             AND M1.SEKKEI_FUKA = M2.SEKKEI_FUKA "
            sql = sql & N & "	             AND M1.SEKKEI_KAITEI = M2.KAITEI ) MPE"
            sql = sql & N & " ON M12.HINMEICD = MPE.SHIYO || MPE.HINSYU || MPE.SENSHIN || MPE.SAIZU || MPE.IRO "
            sql = sql & N & " WHERE NOT M12.KHINMEICD = M12.HINMEICD "
            sql = sql & N & " AND M12.KHINMEICD = '" & siyoCd & _db.rmSQ(txtHinsyu.Text) & _db.rmSQ(txtSensin.Text) & _db.rmSQ(txtSize.Text) & _db.rmSQ(txtColor.Text) & "'"
            '2014/06/04 UPD-E Sugano

            'SQL発行
            Dim iRecCntM12 As Integer
            Dim dsM12 As DataSet = _db.selectDB(sql, RS, iRecCntM12)

            'M11のデータを各コントロールに表示
            txtSeisaku.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_SEISAKU))             '製作区分
            lblHinmei.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_HINMEI))               '品名
            txtHyoujunLot.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_LOTTYO))           '標準ロット長
            txtTantyou.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_TANTYO))              '単長
            lblJosu.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_JOSU))                   '条数
            lblKNDHonsu.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_KND))                '北日本分
            txtSumiHonsu.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_SUMIDEN))           '住電日立分
            txtZaiko.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_ZAIKO))                 '在庫・繰返
            txtChumonsaki.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_TYUMONSAKI))       '注文先
            txtJuyousaki.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_JUYOSAKI))          '需要先
            lblABC.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_ABC))                     'ABC
            txtSizeTenkai.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_SIZETENKAI))       'サイズ展開

            Me.cboHinsyuKbn.Items.Clear()                                                  '品種区分コンボ
            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(cboHinsyuKbn)

            'コンボボックスにセット
            ch.addItem(New UtilCboVO(0, _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_HINSYUKBN))))
            ch.selectItem(0)

            txtKijunTuki.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_KIJUNTUKI))         '基準月数
            txtSaigai.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_SAIGAI))               '災害復旧用在庫量
            txtAnzenZ.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_ANZEN))                '安全在庫量
            txtKamoku.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_KAMOKU))               '科目コード
            txtMakiwaku.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_MAKIWAKU))           '巻枠コード
            txtHousou.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_HOUSOU))               '包装／表示区分
            txtSiyousyo.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_SIYOUSYO))           '仕様書番号
            txtSeizouBmn.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_SEIZOUBMN))         '製造部門
            lblTehaiKbn.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_TEHAIKBN))           '手配区分
            txtBTenkai.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_BUBUNTENKAI))         '展開区分
            txtTenkaiKbn.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_TENKAIKBN))         '部分展開工程
            txtHinsitu.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_HINSITU))             '品質試験区分
            txtTatiai.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_TATIAI))               '立会有無
            lblSyoriKbn.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_SYORIKBN))           '処理区分
            lblKako.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_KAKOU))                  '加工長計算区分
            lblTantyo.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_TANTYOKBN))            '単長区分
            lblSeiseki.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_SEISEKI))             '成績書
            lblMTantyo.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_MTANTYO))             '持込余長単長毎
            lblMLot.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_MLOT))                   '持込余長ロット毎
            lblTTantyo.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_TTANTYO))             '立会余長単長毎
            lblTLot.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_TLOT))                   '立会余長ロット毎
            lblSTantyo.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_STANTYO))             '指定社検単長毎
            lblSLot.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_SLOT))                   '指定社検ロット毎
            lblTokki.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_TOKKI))                 '特記事項
            lblBikou.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_BIKOU))                 '備考
            lblNyuko.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_NYUUKO))                '入庫本数
            lblTourokuFlg.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_TOUROKU))          '登録フラグ
            lblHenko.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)(DB_HENKOU))                '変更内容

            '抽出データを一覧に表示する
            dgvSTaisyou.DataSource = dsM12
            dgvSTaisyou.DataMember = RS

            '一覧右上に件数を表示
            lblKensuu.Text = CStr(iRecCntM12) & "件"

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　 品名表示
    '   （処理概要）入力された品名コードをもとに材料票から品名を取得して画面に表示する。
    '-------------------------------------------------------------------------------
    Private Sub dispHinmei()
        Try

            '品名などを受け取るための変数
            Dim hinmei As String = ""
            Dim hinsyuNM As String = ""
            Dim sizeNM As String = ""
            Dim colorNM As String = ""
            Dim instance As ComBiz = New ComBiz(_db, _msgHd)
            '材料票から品名検索
            Call instance.getHinmeiFromZairyoMst(txtSiyo.Text, txtHinsyu.Text, txtSensin.Text, txtSize.Text, txtColor.Text, _
                                                                                       hinmei, hinsyuNM, sizeNM, colorNM)

            '品名が取れなかった場合
            If "".Equals(hinmei) Then
                If _sinkiFlg Then
                    txtSiyo.Focus()
                    lblHinmei.Text = ""     '品名のクリア
                End If
                Throw New UsrDefException("品名コードが材料票マスタに登録されていません。", _msgHd.getMSG("notExistZairyouMst"))
            End If

            'M11に登録されている品名コードと重複していないかチェック
            Call checkHinmeiCDRepeat()

            'M12に登録されている実品名コードと重複していないかチェック
            Call checkKHinmeiCDRepeat(txtSiyo.Text, txtHinsyu.Text, txtSensin.Text, txtSize.Text, txtColor.Text)

            '取得したデータを画面の隠しコントロールに格納
            lblHinsyuNMHide.Text = hinsyuNM
            lblSizeNMHide.Text = sizeNM
            lblIroNMHide.Text = colorNM

            '品名ラベルに品名を表示
            lblHinmei.Text = hinmei

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　 品種区分コンボボックス作成
    '   （処理概要）需要先に入力されている値をもとにコンボボックスを作成する。
    '-------------------------------------------------------------------------------
    Private Sub createCbo()
        Try

            '需要先が空なら処理を行わない。
            If "".Equals(txtJuyousaki.Text) Then
                Exit Sub
            End If

            Dim sql As String = ""
            sql = sql & N & " SELECT HINSYUKBN FROM M02HINSYUKBN "
            sql = sql & N & "   WHERE JUYOUCD = '" & _db.rmNullInt(txtJuyousaki.Text) & "'"

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                Exit Sub
            End If

            'コンボボックスクリア
            Me.cboHinsyuKbn.Items.Clear()
            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(cboHinsyuKbn)

            'ループさせてコンボボックスにセット
            For i As Integer = 0 To iRecCnt - 1
                ch.addItem(New UtilCboVO(i, _db.rmNullStr(ds.Tables(RS).Rows(i)("HINSYUKBN"))))
            Next

            'ラベルの初期化
            lblHinsyuKbn.Text = ""

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　ラベルの自動表示
    '　(処理概要)コード選択子画面対応のテキストボックスに値が入力された場合、
    '　　　　　　その値をもとにラベルに名称を表示する。
    '   ●入力パラメタ  ：prmKahenkey   汎用マスタ可変キー
    '                     prmKoteikey   汎用マスタ固定キー
    '                     prmLbl　　　　結果表示ラベル
    '   ●出力パラメタ  ：なし
    '   ●メソッド戻り値：なし
    '-------------------------------------------------------------------------------
    Private Function serchName(ByVal prmKahenkey As String, ByVal prmKoteikey As String, ByVal prmLbl As Label) As String

        Try

            Dim dtCol As String = "NAME"        'エイリアス
            Dim col As String = "NAME1"         '取得する列名
            If prmKoteikey.Equals(HKOTEIKEY_JUYOSAKI) Then
                col = "NAME2"                   '需要先のみ名称2を取得する
            End If

            Dim sql As String = ""
            sql = sql & N & " SELECT " & col & " " & dtCol
            sql = sql & N & " FROM M01HANYO "
            sql = sql & N & "   WHERE KOTEIKEY = '" & prmKoteikey & "'"
            sql = sql & N & "   AND KAHENKEY = '" & prmKahenkey & "'"

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                Return ""
            Else
                Return _db.rmNullStr(ds.Tables(RS).Rows(0)(dtCol))
            End If

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Function

    '-------------------------------------------------------------------------------
    '　新規登録モード初期値設定
    '　（処理概要）新規登録モードでの初期値を汎用マスタから取得し、各コントロールに表示する。
    '　　　　　　　対象コントロール ：製作区分(展開区分・品質試験区分も自動表示)
    '                               ：加工長計算区分
    '                               ：立会区分
    '                               ：単長区分
    '                               ：在庫区分(手配区分も自動表示)
    '                               ：処理区分
    '　　●入力パラメータ：なし
    '　　●関数戻り値　　：なし
    '-------------------------------------------------------------------------------
    Private Sub setSyokiti()
        Try
            '取得するレコードの固定キーをつなげて検索用文字列にする。
            Dim koteikey As String = ""
            koteikey = "('" & HKOTEIKEY_SEISAKU_KBN & "', '" & HKOTEIKEY_KAKOUCHO_KBN & "','" & HKOTEIKEY_TACHIAI_UM & "','" & _
                    HKOTEIKEY_TANCHO_KBN & "','" & HKOTEIKEY_ZAIKO_KBN & "','" & HKOTEIKEY_SYORI_KBN & "'"

            Dim sql As String = ""
            sql = sql & N & " SELECT "
            sql = sql & N & " KOTEIKEY, "
            sql = sql & N & " KAHENKEY, "
            sql = sql & N & " NAME1 "
            sql = sql & N & " FROM M01HANYO "
            sql = sql & N & " WHERE KOTEIKEY IN " & koteikey & ")"

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合

                Throw New UsrDefException("汎用マスタに登録されていない値です。", _msgHd.getMSG("noExistHanyouMst"))
            End If

            '各コントロールに初期値を設定していく
            For i As Integer = 0 To iRecCnt - 1
                '製作区分
                If HKOTEIKEY_SEISAKU_KBN.Equals(_db.rmNullStr(ds.Tables(RS).Rows(i)("KOTEIKEY"))) And _
                        SYOKI_SEISAKUKBN.Equals(_db.rmNullStr(ds.Tables(RS).Rows(i)("KAHENKEY"))) Then
                    txtSeisaku.Text = _db.rmNullStr(ds.Tables(RS).Rows(i)("KAHENKEY"))
                    lblSeisaku.Text = _db.rmNullStr(ds.Tables(RS).Rows(i)("NAME1"))
                    '展開区分・品質試験区分自動表示、科目コードの使用可否設定
                    Call txtSeisakuChange()

                    '加工長計算区分
                ElseIf HKOTEIKEY_KAKOUCHO_KBN.Equals(_db.rmNullStr(ds.Tables(RS).Rows(i)("KOTEIKEY"))) And _
                        SYOKI_KAKOUKBN.Equals(ds.Tables(RS).Rows(i)("KAHENKEY")) Then
                    lblKako.Text = ds.Tables(RS).Rows(i)("KAHENKEY")
                    lblKakoNM.Text = ds.Tables(RS).Rows(i)("NAME1")

                    '立会区分
                    ''2010/12/27 upd start sugano
                    'ElseIf HKOTEIKEY_TACHIAI_UM.Equals(_db.rmNullStr(_db.rmNullStr(ds.Tables(RS).Rows(i)("KOTEIKEY"))) And _
                    '        SYOKI_TACHIAIKBN.Equals(_db.rmNullStr(ds.Tables(RS).Rows(i)("KAHENKEY")))) Then
                ElseIf HKOTEIKEY_TACHIAI_UM.Equals(_db.rmNullStr(ds.Tables(RS).Rows(i)("KOTEIKEY"))) And _
                        SYOKI_TACHIAIKBN.Equals(_db.rmNullStr(ds.Tables(RS).Rows(i)("KAHENKEY"))) Then
                    ''2010/12/27 upd end sugano
                    txtTatiai.Text = ds.Tables(RS).Rows(i)("KAHENKEY")
                    lblTatiai.Text = ds.Tables(RS).Rows(i)("NAME1")

                    '単長区分
                ElseIf HKOTEIKEY_TANCHO_KBN.Equals(_db.rmNullStr(ds.Tables(RS).Rows(i)("KOTEIKEY"))) And _
                        SYOKI_TANTYOKBN.Equals(_db.rmNullStr(ds.Tables(RS).Rows(i)("KAHENKEY"))) Then
                    lblTantyo.Text = _db.rmNullStr(ds.Tables(RS).Rows(i)("KAHENKEY"))
                    lblTantyoNM.Text = _db.rmNullStr(ds.Tables(RS).Rows(i)("NAME1"))

                    '在庫区分
                ElseIf HKOTEIKEY_ZAIKO_KBN.Equals(_db.rmNullStr(_db.rmNullStr(ds.Tables(RS).Rows(i)("KOTEIKEY"))) And _
                        SYOKI_ZAIKO.Equals(_db.rmNullStr(ds.Tables(RS).Rows(i)("KAHENKEY")))) Then
                    txtZaiko.Text = _db.rmNullStr(ds.Tables(RS).Rows(i)("KAHENKEY"))
                    lblZaiko.Text = _db.rmNullStr(ds.Tables(RS).Rows(i)("NAME1"))
                    '手配区分・注文先自動表示
                    Call txtZaikoChange()

                    '処理区分
                ElseIf HKOTEIKEY_SYORI_KBN.Equals(_db.rmNullStr(ds.Tables(RS).Rows(i)("KOTEIKEY"))) And _
                        SYOKI_SYORIKBN.Equals(_db.rmNullStr(ds.Tables(RS).Rows(i)("KAHENKEY"))) Then
                    lblSyoriKbn.Text = _db.rmNullStr(ds.Tables(RS).Rows(i)("KAHENKEY"))
                    lblSyoriKbnNM.Text = _db.rmNullStr(ds.Tables(RS).Rows(i)("NAME1"))
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
    '　品種区分ラベル表示
    '　（処理概要）品種区分コンボで選択されたコードに対応する名称をラベルに表示する。
    '　　●入力パラメータ：なし
    '　　●関数戻り値　　：なし
    '-------------------------------------------------------------------------------
    Private Sub dispCboLabel()
        Try
            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(cboHinsyuKbn)

            '需要先およびコンボボックスの両方が入力されている場合のみ処理を行う。
            If "".Equals(ch.getName) Or "".Equals(txtJuyousaki.Text) Then Exit Sub

            Dim sql = ""
            sql = sql & N & " SELECT "
            sql = sql & N & " HINSYUKBNNM "
            sql = sql & N & " FROM M02HINSYUKBN "
            sql = sql & N & "   WHERE JUYOUCD = '" & txtJuyousaki.Text & "'"
            sql = sql & N & "   AND HINSYUKBN = '" & ch.getName & "'"

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                '-->2010.12.22 chg by takagi
                'Throw New UsrDefException("汎用マスタに登録されていない値です。", _msgHd.getMSG("noExistHanyouMst", "【品種区分】"))
                If _sinkiFlg Then
                    Throw New UsrDefException("汎用マスタに登録されていない値です。", _msgHd.getMSG("noExistHanyouMst", "【品種区分】"))
                Else
                    lblHinsyuKbn.Text = ""
                    Exit Sub
                End If
                '<--2010.12.22 chg by takagi

            End If

            'ラベル表示
            lblHinsyuKbn.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("HINSYUKBNNM"))

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　巻枠コードラベル表示
    '　（処理概要）入力された巻枠コードに対応する名称をラベルに表示する。
    '　　●入力パラメータ：なし
    '　　●関数戻り値　　：なし
    '-------------------------------------------------------------------------------
    Private Sub dispMakiwakuLabel()
        Try

            '-->2010.12.24 chg by takagi
            'If "".Equals(txtMakiwaku.Text) Then Exit Sub
            If "".Equals(txtMakiwaku.Text) Then
                lblMakiwaku.Text = ""
                Exit Sub
            End If
            '<--2010.12.24 chg by takagi

            '-->2010.12.24 add by takagi
            If "999999".Equals(txtMakiwaku.Text) Then
                lblMakiwaku.Text = ""
                Exit Sub
            End If
            '<--2010.12.24 add by takagi

            Dim sql = ""
            sql = sql & N & " SELECT "
            sql = sql & N & " ZE_NAME "
            sql = sql & N & " FROM ZEASYCODE_TB "
            sql = sql & N & "   WHERE ZE_CODE = '" & txtMakiwaku.Text & "'"

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then            '抽出レコードが１件もない場合
                If _sinkiFlg Then
                    lblMakiwaku.Text = ""       'ラベルクリア
                    txtMakiwaku.Focus()         'フォーカス設定
                End If
                '-->2010.12.22 chg by takagi
                'Throw New UsrDefException("巻枠名がマスタに登録されていません。", _msgHd.getMSG("noExistMakiwaku", "【巻枠コード】"))
                If _sinkiFlg Then
                    Throw New UsrDefException("巻枠名がマスタに登録されていません。", _msgHd.getMSG("noExistMakiwaku", "【巻枠コード】"))
                Else
                    lblMakiwaku.Text = ""
                    Exit Sub
                End If
                '<--2010.12.22 chg by takagi

            End If

            'ラベル表示
            lblMakiwaku.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("ZE_NAME"))

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　包装／表示区分ラベル表示
    '　（処理概要）入力された巻枠コードに対応する名称をラベルに表示する。
    '　　●入力パラメータ：なし
    '　　●関数戻り値　　：なし
    '-------------------------------------------------------------------------------
    Private Sub dispHousouLabel()
        Try

            '-->2010.12.24 chg by takagi
            'If "".Equals(txtHousou.Text) Then Exit Sub
            If "".Equals(txtHousou.Text) Then
                lblHousou.Text = ""
                Exit Sub
            End If
            '<--2010.12.24 chg by takagi

            Dim sql = ""
            sql = sql & N & " SELECT "
            sql = sql & N & " HN_NAME "
            sql = sql & N & " FROM HOSONAME_TB "
            sql = sql & N & "   WHERE HN_KUBUN = '" & txtHousou.Text & "'"

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then            '抽出レコードが１件もない場合
                If _sinkiFlg Then
                    lblHousou.Text = ""         'ラベルクリア
                    txtHousou.Focus()           'フォーカス設定
                End If
                '-->2010.12.22 chg by takagi
                'Throw New UsrDefException("包装／表示種類がマスタに登録されていません。", _msgHd.getMSG("noExistHousou", "【包装／表示種類】"))
                If _sinkiFlg Then
                    Throw New UsrDefException("包装／表示種類がマスタに登録されていません。", _msgHd.getMSG("noExistHousou", "【包装／表示種類】"))
                Else
                    lblHousou.Text = ""
                    Exit Sub
                End If
                '<--2010.12.22 chg by takagi

            End If

            'ラベル表示
            lblHousou.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("HN_NAME"))


        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　一覧表示
    '　（処理概要）入力された集計対象品名コードとそれに対応する名称を一覧に表示する。
    '　　●入力パラメータ：なし
    '　　●関数戻り値　　：なし
    '-------------------------------------------------------------------------------
    Private Sub dispDgv()
        Try

            '集計対象品名コードの作成
            Dim sSiyo As String = txtSSiyo.Text
            If sSiyo.Length = 1 Then
                sSiyo = sSiyo & " "
            End If

            Dim sHinmeiCD As String = ""
            sHinmeiCD = sSiyo & txtSHinsyu.Text & txtSSensin.Text & txtSSize.Text & txtSColor.Text

            Dim SQL As String = ""
            SQL = SQL & N & " SELECT "
            SQL = SQL & N & " '" & sHinmeiCD & "'" & COLDT_SCODE
            SQL = SQL & N & " ,HINSYU_MEI || SAIZU_MEI || IRO_MEI " & COLDT_SNAME
            '2014/06/04 UPD-S Sugano
            'SQL = SQL & N & " FROM MPESEKKEI "
            SQL = SQL & N & " FROM MPESEKKEI1 "
            '2014/06/04 UPD-E Sugano
            SQL = SQL & N & "   WHERE SHIYO = '" & _db.rmSQ(txtSSiyo.Text) & "'  "
            SQL = SQL & N & "   AND HINSYU = '" & _db.rmSQ(txtSHinsyu.Text) & "'  "
            SQL = SQL & N & "   AND SENSHIN = '" & _db.rmSQ(txtSSensin.Text) & "'  "
            SQL = SQL & N & "   AND SAIZU = '" & _db.rmSQ(txtSSize.Text) & "'  "
            SQL = SQL & N & "   AND IRO = '" & _db.rmSQ(txtSColor.Text) & "' "
            '2014/06/04 UPD-S Sugano
            'SQL = SQL & N & "   AND SEQ_NO = " & INT_SEQNO
            'SQL = SQL & N & "   AND SEKKEI_HUKA = "
            'SQL = SQL & N & "           (SELECT MAX(SEKKEI_HUKA) FROM MPESEKKEI "
            SQL = SQL & N & "   AND SEKKEI_FUKA = 'A'"
            SQL = SQL & N & "   AND SEKKEI_KAITEI = (SELECT MAX(SEKKEI_KAITEI) FROM MPESEKKEI1 "
            '2014/06/04 UPD-E Sugano
            SQL = SQL & N & "               WHERE SHIYO = '" & _db.rmSQ(txtSSiyo.Text) & "'  "
            SQL = SQL & N & "               AND HINSYU = '" & _db.rmSQ(txtSHinsyu.Text) & "'  "
            SQL = SQL & N & "               AND SENSHIN = '" & _db.rmSQ(txtSSensin.Text) & "'  "
            SQL = SQL & N & "               AND SAIZU = '" & _db.rmSQ(txtSSize.Text) & "'  "
            '2014/06/04 UPD-S Sugano
            'SQL = SQL & N & "               AND IRO = '" & _db.rmSQ(txtSColor.Text) & "')  "
            SQL = SQL & N & "               AND IRO = '" & _db.rmSQ(txtSColor.Text) & "'  "
            SQL = SQL & N & "               AND SEKKEI_FUKA = 'A')"
            '2014/06/04 UPD-E Sugano

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(SQL, RS2, iRecCnt)

            If iRecCnt <= 0 Then            '抽出レコードが１件もない場合
                txtSSiyo.Focus()            'フォーカス設定
                Throw New UsrDefException("集計対象品名コードが材料票マスタに登録されていません。", _
                                                                _msgHd.getMSG("notExistSyukeiZairyouMst"))
            End If

            '追加行生成
            Dim rowDt As Object() = {_db.rmNullStr(ds.Tables(RS2).Rows(0)(COLDT_SCODE)), _
                                            _db.rmNullStr(ds.Tables(RS2).Rows(0)(COLDT_SNAME))}

            '追加するコードと一覧に表示されているコードの重複チェック
            For i As Integer = 0 To dgvSTaisyou.RowCount - 1
                If _db.rmNullStr(ds.Tables(RS2).Rows(0)(COLDT_SCODE)).Equals(_db.rmNullStr(dgvSTaisyou(0, i).Value)) Then
                    txtSSiyo.Focus()
                    Throw New UsrDefException("すでに一覧に追加された集計対象品名コードです。", _
                                                            _msgHd.getMSG("alreadyAddSyukeiItiran"))
                End If
            Next

            'グリッドにバインドされたデータセット取得
            Dim wkDs As DataSet = dgvSTaisyou.DataSource

            'そのデータセットに行追加
            wkDs.Tables(RS).Rows.Add(rowDt)

            'ソート処理
            Dim dtTmp As DataTable = wkDs.Tables(RS).Clone()

            'ソートされたデータビューの作成
            Dim dv As DataView = New DataView(wkDs.Tables(RS))
            dv.Sort = COLDT_SCODE

            'ソートされたレコードのコピー
            For Each drv As DataRowView In dv
                dtTmp.ImportRow(drv.Row)
            Next

            'バインド用データセット生成
            Dim bindDs As DataSet = New DataSet
            bindDs.Tables.Add(dtTmp)

            '再バインド
            dgvSTaisyou.DataSource = bindDs
            dgvSTaisyou.DataMember = RS

            '一覧の件数を表示する
            lblKensuu.Text = CStr(dgvSTaisyou.RowCount) & "件"

            '追加した行にフォーカス
            For c As Integer = 0 To dgvSTaisyou.RowCount - 1
                If _db.rmNullStr(ds.Tables(RS2).Rows(0)(COLDT_SCODE)).Equals(_db.rmNullStr(dgvSTaisyou(0, c).Value)) Then
                    Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSTaisyou)
                    dgvSTaisyou.Focus()
                    dgvSTaisyou.CurrentCell = dgvSTaisyou(0, c)
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
    '　一覧行削除
    '　（処理概要）選択された行を一覧から削除する。
    '　　●入力パラメータ：なし
    '　　●関数戻り値　　：なし
    '-------------------------------------------------------------------------------
    Private Sub deleteRowDgv()
        Try

            'グリッドにバインドされたデータセット取得
            Dim wkDs As DataSet = dgvSTaisyou.DataSource

            '選択行削除
            wkDs.Tables(RS).Rows.RemoveAt(dgvSTaisyou.CurrentCellAddress.Y)

            '再バインド
            dgvSTaisyou.DataSource = wkDs
            dgvSTaisyou.DataMember = RS

            '一覧の件数を表示する
            lblKensuu.Text = CStr(dgvSTaisyou.RowCount) & "件"

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　登録処理
    '　（処理概要）入力された値をDBに登録する。
    '　　●入力パラメータ：なし
    '　　●関数戻り値　　：なし
    '-------------------------------------------------------------------------------
    Private Sub insertDB()
        Try
            '仕様コードが1桁の場合は、半角スペースを加える
            Dim siyoCD As String = ""
            If _db.rmSQ(txtSiyo.Text).Length = 1 Then
                siyoCD = siyoCD & _db.rmSQ(txtSiyo.Text) & " "
            Else
                siyoCD = _db.rmSQ(txtSiyo.Text)
            End If

            '計画品名コード作成
            Dim kHinmei As String = ""
            kHinmei = siyoCD & _db.rmSQ(txtHinsyu.Text) & _db.rmSQ(txtSensin.Text) & _db.rmSQ(txtSize.Text) & _db.rmSQ(txtColor.Text)

            '更新開始日時を取得
            Dim updStartDate As Date = Now

            'トランザクション開始
            _db.beginTran()

            Dim sql As String = ""
            sql = sql & N & " INSERT INTO M11KEIKAKUHIN ("
            sql = sql & N & " TT_H_SIYOU_CD "            '仕様コード
            sql = sql & N & " ,TT_H_HIN_CD "             '品種コード
            sql = sql & N & " ,TT_H_SENSIN_CD "          '線新数コード
            sql = sql & N & " ,TT_H_SIZE_CD "            'サイズコード
            sql = sql & N & " ,TT_H_COLOR_CD "           '色コード
            sql = sql & N & " ,TT_TANCYO "               '単長
            sql = sql & N & " ,TT_FUKA_CD "              '付加記号
            sql = sql & N & " ,TT_HINMEI "               '品名
            sql = sql & N & " ,TT_TEHAI_SUU "            '手配数量
            sql = sql & N & " ,TT_SYORI_KBN "            '処理区分
            sql = sql & N & " ,TT_TEHAI_KBN "            '手配区分
            sql = sql & N & " ,TT_SEISAKU_KBN "          '製作区分
            sql = sql & N & " ,TT_KYAKSAKI "             '注文先
            sql = sql & N & " ,TT_TENKAI_KBN "           '展開区分
            sql = sql & N & " ,TT_KOUTEI "               '部分展開指定工程
            sql = sql & N & " ,TT_KEISAN_KBN "           '加工長計算区分
            sql = sql & N & " ,TT_TATIAI_UM "            '立会有無
            sql = sql & N & " ,TT_TANCYO_KBN "           '単長区分
            sql = sql & N & " ,TT_MAKI_CD "              '巻枠コード
            sql = sql & N & " ,TT_HOSO_KBN "             '包装／表示区分
            sql = sql & N & " ,TT_HINSITU_KBN "          '品質試験区分
            sql = sql & N & " ,TT_SIYOUSYO_NO "          '仕様書番号
            sql = sql & N & " ,TT_SEIZOU_BMN "           '製造部門
            sql = sql & N & " ,TT_KAMOKU_CD "            '科目コード
            sql = sql & N & " ,TT_N_SO_SUU "             '入庫本数
            sql = sql & N & " ,TT_N_K_SUU "              '北日本本数
            sql = sql & N & " ,TT_N_SH_SUU "             '住電日立本数
            sql = sql & N & " ,TT_SEISEKI "              '成績書
            sql = sql & N & " ,TT_MYTANCYO "             '持込余長単長毎
            sql = sql & N & " ,TT_MYLOT "                '持込余長ロット毎
            sql = sql & N & " ,TT_TYTANCYO "             '立会余長単長毎
            sql = sql & N & " ,TT_TYLOT "                '立会余長ロット毎
            sql = sql & N & " ,TT_SYTANCYO "             '指定社検余長単長毎
            sql = sql & N & " ,TT_SYLOT "                '指定社検余長ロット毎
            sql = sql & N & " ,TT_TOKKI "                '特記事項
            sql = sql & N & " ,TT_BIKO "                 '備考
            sql = sql & N & " ,TT_HENKO "                '変更内容
            sql = sql & N & " ,TT_JYOSU "                '条数
            sql = sql & N & " ,TT_INSFLG "               '登録フラグ
            sql = sql & N & " ,TT_SYUBETU "              '在庫繰返
            sql = sql & N & " ,TT_KHINMEICD "            '計画品名コード
            sql = sql & N & " ,TT_HINSYUNM "             '品種名
            sql = sql & N & " ,TT_SIZENM "               'サイズ名
            sql = sql & N & " ,TT_IRONM "                '色支持線名
            sql = sql & N & " ,TT_LOT "                  '標準ロット長
            '在庫・繰返が「1」の場合のみ登録
            If txtZaiko.Text = ZAIKOCD_ZAIKO Then
                sql = sql & N & " ,TT_JUYOUCD "              '需要先
                sql = sql & N & " ,TT_ABCKBN "               'ＡＢＣ区分
                sql = sql & N & " ,TT_HINSYUKBN "            '品種区分
                sql = sql & N & " ,TT_TENKAIPTN "            'サイズ展開パターン
                sql = sql & N & " ,TT_KZAIKOTUKISU "         '基準在庫月数
                sql = sql & N & " ,TT_SFUKKYUU "             '災害復旧用在庫量
                sql = sql & N & " ,TT_ANNZENZAIKO "          '安全在庫量
            End If
            sql = sql & N & " ,TT_UPDNAME "              '端末ID
            sql = sql & N & " ,TT_DATE )"                '更新日時
            sql = sql & N & " VALUES ( "

            'sql = sql & N & " '" & siyoCD & "', "                                                   '仕様コード
            sql = sql & N & " '" & siyoCD.PadRight(2, " ") & "', "                                                   '仕様コード

            sql = sql & N & " '" & _db.rmSQ(txtHinsyu.Text) & "', "                                 '品種コード
            sql = sql & N & " '" & _db.rmSQ(txtSensin.Text) & "', "                                 '線新数コード
            sql = sql & N & " '" & _db.rmSQ(txtSize.Text) & "', "                                   'サイズコード
            sql = sql & N & " '" & _db.rmSQ(txtColor.Text) & "', "                                  '色コード
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(txtTantyou.Text) & "', '" & NUMBER & "'), "   '単長
            sql = sql & N & " '" & _db.rmSQ(lblFuka.Text) & "', "                                   '付加記号
            sql = sql & N & " '" & _db.rmSQ(lblHinmei.Text) & "', "                                 '品名
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(txtTantyou.Text) & "', '" & NUMBER & _
                        "') * TO_NUMBER('" & _db.rmSQ(lblJosu.Text) & "', '" & NUMBER & "'), "      '標準ロット長×単長
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(lblSyoriKbn.Text) & "'), "                    '処理区分
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(lblTehaiKbn.Text) & "'), "                    '手配区分
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(txtSeisaku.Text) & "'), "                     '製作区分
            sql = sql & N & " " & returnNull(txtChumonsaki) & ", "                                  '注文先
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(txtTenkaiKbn.Text) & "'), "                   '展開区分
            sql = sql & N & " " & returnNull(txtBTenkai) & ", "                                     '部分展開指定工程
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(lblKako.Text) & "'), "                        '加工長計算区分
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(txtTatiai.Text) & "'), "                      '立会有無
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(lblTantyo.Text) & "'), "                      '単長区分
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(txtMakiwaku.Text) & "'), "                    '巻枠コード
            sql = sql & N & " '" & _db.rmSQ(txtHousou.Text) & "', "                                 '包装／表示区分
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(txtHinsitu.Text) & "'), "                     '品質試験区分
            sql = sql & N & " '" & _db.rmSQ(txtSiyousyo.Text) & "', "                               '仕様書番号
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(txtSeizouBmn.Text) & "'), "                   '製造部門
            sql = sql & N & " TO_NUMBER(" & returnNull(txtKamoku) & "), "                           '科目コード
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(lblNyuko.Text) & "', '" & NUMBER & "'), "     '入庫本数
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(lblKNDHonsu.Text) & "', '" & NUMBER & "'), "  '北日本本数
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(txtSumiHonsu.Text) & "', '" & NUMBER & "'), " '住電日立本数
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(lblSeiseki.Text) & "'), "                     '成績書
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(lblMTantyo.Text) & "', '" & NUMBER & "'), "   '持込余長単長毎
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(lblMLot.Text) & "', '" & NUMBER & "'), "      '持込余長ロット毎
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(lblTTantyo.Text) & "', '" & NUMBER & "'), "   '立会余長単長毎
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(lblTLot.Text) & "', '" & NUMBER & "'), "      '立会余長ロット毎
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(lblSTantyo.Text) & "', '" & NUMBER & "'), "   '指定社検余長単長毎
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(lblSLot.Text) & "', '" & NUMBER & "'), "      '指定社検余長ロット毎
            sql = sql & N & " " & returnNull(lblTokki) & ", "                                       '特記事項
            sql = sql & N & " " & returnNull(lblBikou) & ", "                                       '備考
            sql = sql & N & " " & returnNull(lblHenko) & ", "                                       '変更内容
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(lblJosu.Text) & "'), "                        '条数
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(lblTourokuFlg.Text) & "'), "                  '登録フラグ
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(txtZaiko.Text) & "'), "                       '在庫繰返
            sql = sql & N & " '" & kHinmei & "', "                                                  '計画品名コード
            sql = sql & N & " '" & _db.rmSQ(lblHinsyuNMHide.Text) & "', "                           '品種名
            sql = sql & N & " '" & _db.rmSQ(lblSizeNMHide.Text) & "', "                             'サイズ名
            sql = sql & N & " '" & _db.rmSQ(lblIroNMHide.Text) & "', "                              '色支持線名
            sql = sql & N & " TO_NUMBER('" & _db.rmSQ(txtHyoujunLot.Text) & "','" & NUMBER & "'), " '標準ロット長
            '在庫・繰返が「1」の場合のみ登録
            If txtZaiko.Text = ZAIKOCD_ZAIKO Then
                sql = sql & N & " " & returnNull(txtJuyousaki) & ", "                                   '需要先
                sql = sql & N & " " & returnNull(lblABC) & ", "                                         'ＡＢＣ区分
                sql = sql & N & " '" & cboHinsyuKbn.Text & "', "                                        '品種区分
                sql = sql & N & " '" & txtSizeTenkai.Text & "', "                                       'サイズ展開パターン
                sql = sql & N & " TO_NUMBER('" & _db.rmSQ(txtKijunTuki.Text) & "', '" & NUMBER & "'), " '基準在庫月数
                sql = sql & N & " TO_NUMBER('" & _db.rmSQ(txtSaigai.Text) & "', '" & NUMBER & "'), "    '災害復旧用在庫量
                sql = sql & N & " TO_NUMBER('" & _db.rmSQ(txtAnzenZ.Text) & "', '" & NUMBER & "'), "    '安全在庫量            
            End If
            sql = sql & N & " '" & _tanmatuID & "', "                                               '端末ID
            sql = sql & N & " TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS')) "           '更新日時

            Dim updCnt As Integer = 0
            _db.executeDB(sql, updCnt)

            'M12 親コードの登録
            sql = ""
            sql = sql & N & " INSERT INTO M12SYUYAKU ("
            sql = sql & N & "  HINMEICD "
            sql = sql & N & " ,KHINMEICD "
            sql = sql & N & " ,UPDNAME "
            sql = sql & N & " ,UPDDATE) "
            sql = sql & N & " VALUES ( "
            sql = sql & N & "   '" & kHinmei & "', "
            sql = sql & N & "   '" & kHinmei & "', "
            sql = sql & N & " '" & _tanmatuID & "', "                                       '端末ID
            sql = sql & N & " TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS')) "   '更新日時
            _db.executeDB(sql)

            'M12 一覧に表示されている計画品名コードの登録
            For loopCnt As Integer = 0 To dgvSTaisyou.RowCount - 1

                sql = ""
                sql = sql & N & " INSERT INTO M12SYUYAKU ("
                sql = sql & N & "  HINMEICD "
                sql = sql & N & " ,KHINMEICD "
                sql = sql & N & " ,UPDNAME "
                sql = sql & N & " ,UPDDATE) "
                sql = sql & N & " VALUES ( "
                sql = sql & N & "   '" & _db.rmSQ(dgvSTaisyou(0, loopCnt).Value) & "', "
                sql = sql & N & "   '" & kHinmei & "', "
                sql = sql & N & " '" & _tanmatuID & "', "                                       '端末ID
                sql = sql & N & " TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS')) "   '更新日時
                _db.executeDB(sql)
            Next

            '更新終了日時を取得
            Dim updFinDate As Date = Now

            'T91実行履歴登録処理
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
            '-->2010.12.02 upd by takagi
            'sql = sql & N & "  '" & PGID & "'"                                              '機能ID
            sql = sql & N & "  '" & _pgId & "'"                                              '機能ID
            '<--2010.12.02 upd by takagi
            sql = sql & N & ", NULL "
            sql = sql & N & ", NULL "
            sql = sql & N & ", TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "   '処理開始日時
            sql = sql & N & ", TO_DATE('" & updFinDate & "', 'YYYY/MM/DD HH24:MI:SS') "     '処理終了日時
            sql = sql & N & ", 0 "                                                          '件数１（削除件数）"
            sql = sql & N & ", " & updCnt                                                   '件数２（登録件数）
            sql = sql & N & ", '" & _tanmatuID & "'"                                        '端末ID
            sql = sql & N & ", TO_DATE('" & updFinDate & "', 'YYYY/MM/DD HH24:MI:SS') "     '更新日時
            sql = sql & N & " )"
            _db.executeDB(sql)

            'T02処理制御テーブル更新
            '-->2010.12.02 upd by takagi
            '_parentForm.updateSeigyoTbl(PGID, True, updStartDate, updFinDate)
            _parentForm.updateSeigyoTbl(_pgId, True, updStartDate, updFinDate)
            '<--2010.12.02 upd by takagi

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

    '-------------------------------------------------------------------------------
    '　削除処理
    '　（処理概要）入力された品名コードをもとにDBのレコードを削除する。
    '　　●入力パラメータ：なし
    '　　●関数戻り値　　：なし
    '-------------------------------------------------------------------------------
    Private Sub deleteDB()
        Try

            '更新開始日時を取得
            Dim updStartDate As Date = Now

            '仕様コードが1桁の場合は、半角スペースを加えて2桁にする
            Dim siyoCd As String = _db.rmSQ(txtSiyo.Text)
            If _db.rmSQ(txtSiyo.Text).Length = 1 Then
                siyoCd = siyoCd & " "
            End If

            'トランザクション開始
            _db.beginTran()

            'M11
            Dim sql As String = ""
            sql = sql & N & " DELETE FROM M11KEIKAKUHIN "
            sql = sql & N & "   WHERE "

            'sql = sql & N & "       TT_H_SIYOU_CD = '" & _db.rmSQ(siyoCd) & "'"       '仕様コード
            sql = sql & N & "       TT_H_SIYOU_CD = '" & _db.rmSQ(siyoCd.PadRight(2, " ")) & "'"       '仕様コード

            sql = sql & N & "   AND TT_H_HIN_CD = '" & _db.rmSQ(txtHinsyu.Text) & "'"       '品種コード
            sql = sql & N & "   AND TT_H_SENSIN_CD = '" & _db.rmSQ(txtSensin.Text) & "'"    '線新数コード
            sql = sql & N & "   AND TT_H_SIZE_CD = '" & _db.rmSQ(txtSize.Text) & "'"        'サイズコード
            sql = sql & N & "   AND TT_H_COLOR_CD = '" & _db.rmSQ(txtColor.Text) & "'"      '色コード

            '削除件数保持変数
            Dim delCnt As Integer = 0
            _db.executeDB(sql, delCnt)

            '品名コードを結合
            Dim kHinmeiCD As String = ""
            kHinmeiCD = siyoCd & _db.rmSQ(txtHinsyu.Text) & _db.rmSQ(txtSensin.Text) & _
                                                _db.rmSQ(txtSize.Text) & _db.rmSQ(txtColor.Text)

            'M12
            sql = ""
            sql = sql & N & " DELETE FROM M12SYUYAKU "
            sql = sql & N & "   WHERE "
            sql = sql & N & "       KHINMEICD = '" & kHinmeiCD & "'"

            _db.executeDB(sql, delCnt)

            '更新終了日時を取得
            Dim updFinDate As Date = Now

            'T91実行履歴登録処理
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
            '-->2010.12.02 upd by takagi
            'sql = sql & N & "  '" & PGID & "'"                                              '機能ID
            sql = sql & N & "  '" & _pgId & "'"                                              '機能ID
            '<--2010.12.02 upd by takagi
            sql = sql & N & ", NULL "
            sql = sql & N & ", NULL "
            sql = sql & N & ", TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "   '処理開始日時
            sql = sql & N & ", TO_DATE('" & updFinDate & "', 'YYYY/MM/DD HH24:MI:SS') "     '処理終了日時
            sql = sql & N & ", " & delCnt                                                   '件数１（削除件数）"
            sql = sql & N & ", 0 "                                                          '件数２（登録件数）
            sql = sql & N & ", '" & _tanmatuID & "'"                                        '端末ID
            sql = sql & N & ", TO_DATE('" & updFinDate & "', 'YYYY/MM/DD HH24:MI:SS') "     '更新日時
            sql = sql & N & " )"
            _db.executeDB(sql)

            'T02処理制御テーブル更新
            '-->2010.12.02 upd by takagi
            '_parentForm.updateSeigyoTbl(PGID, True, updStartDate, updFinDate)
            _parentForm.updateSeigyoTbl(_pgId, True, updStartDate, updFinDate)
            '<--2010.12.02 upd by takagi

            'トランザクション終了
            _db.commitTran()

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　コントロール未入力判定
    '　（処理概要）渡されたコントロールが未入力かどうかを判定する。
    '　　●入力パラメータ：各コントロール
    '　　●関数戻り値　　：SQL文
    '-------------------------------------------------------------------------------
    Private Function returnNull(ByVal prmControl As Object) As String
        Try

            returnNull = " " & "NULL"

            If TypeOf prmControl Is TextBox Then
                'テキストボックス用
                If "".Equals(CType(prmControl, TextBox).Text) Then
                    returnNull = " " & "NULL"
                Else
                    returnNull = " '" & _db.rmSQ(prmControl.Text) & "' "
                End If
            ElseIf TypeOf prmControl Is Label Then
                'ラベル用
                If "".Equals(CType(prmControl, Label).Text) Then
                    returnNull = " " & "NULL"
                Else
                    returnNull = " '" & _db.rmSQ(prmControl.Text) & "' "
                End If
            End If

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Function

    '-------------------------------------------------------------------------------
    '　汎用マスタ名称取得(削除モード用)
    '　（処理概要）検索ボタン押下後、渡された値をもとに汎用マスタから名称1を取得する。
    '　　●入力パラメータ： prmKoteiKey　固定キー
    '                    ： prmKahenkey　可変キー
    '　　●関数戻り値　　： 名称1
    '-------------------------------------------------------------------------------
    Private Function serchHanyoMst(ByVal prmKoteiKey As String, ByVal prmKahenkey As String)
        Try

            Dim ret As String = ""

            Dim sql As String = ""
            sql = sql & N & " SELECT "
            sql = sql & N & " NAME1 "
            sql = sql & N & " FROM M01HANYO "
            sql = sql & N & " WHERE KOTEIKEY = '" & _db.rmSQ(prmKoteiKey) & "'"
            sql = sql & N & " AND KAHENKEY = '" & _db.rmSQ(prmKahenkey) & "'"

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then            '抽出レコードが１件もない場合
                Return ret
                Exit Function
            End If

            ret = _db.rmNullStr(ds.Tables(RS).Rows(0)("NAME1"))
            Return ret

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Function

#End Region

#Region "ユーザ定義関数:チェック処理"

    '-------------------------------------------------------------------------------
    '　 品名コードチェック
    '   （処理概要）品名コードが入力されているかチェックする
    '-------------------------------------------------------------------------------
    Private Sub checkInputHinmeiCD()
        Try

            If "".Equals(txtSiyo.Text) Then
                txtSiyo.Focus()
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【仕様コード】"))
            ElseIf "".Equals(txtHinsyu.Text) Then
                txtHinsyu.Focus()
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【品種コード】"))
            ElseIf "".Equals(txtSensin.Text) Then
                txtSensin.Focus()
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【線心数コード】"))
            ElseIf "".Equals(txtSize.Text) Then
                txtSize.Focus()
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【サイズコード】"))
            ElseIf "".Equals(txtColor.Text) Then
                txtColor.Focus()
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【色コード】"))
            End If

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　 集計対象品名コードチェック
    '   （処理概要）集計対象品名コードが入力されているかチェックする
    '-------------------------------------------------------------------------------
    Private Sub checkInputSHinmeiCD()
        Try

            If "".Equals(txtSSiyo.Text) Then
                txtSSiyo.Focus()
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【集計対象品仕様コード】"))
            ElseIf "".Equals(txtSHinsyu.Text) Then
                txtSHinsyu.Focus()
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【集計対象品品種コード】"))
            ElseIf "".Equals(txtSSensin.Text) Then
                txtSSensin.Focus()
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【集計対象品線心数コード】"))
            ElseIf "".Equals(txtSSize.Text) Then
                txtSSize.Focus()
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【集計対象品サイズコード】"))
            ElseIf "".Equals(txtSColor.Text) Then
                txtSColor.Focus()
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【集計対象品色コード】"))
            End If

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　 品名重複チェック
    '   （処理概要）計画対象品マスタを検索し、品名コードの重複がないかチェックする
    '-------------------------------------------------------------------------------
    Private Sub checkHinmeiCDRepeat()
        Try

            '仕様コードが1桁の場合は後に半角スペースを加える
            Dim siyoCD As String = txtSiyo.Text
            If txtSiyo.Text.Length <= 1 Then
                siyoCD = siyoCD & " "
            End If

            'M11
            Dim sql As String = ""
            sql = ""
            sql = sql & N & " SELECT "
            sql = sql & N & " * "
            sql = sql & N & " FROM M11KEIKAKUHIN "

            'sql = sql & N & "   WHERE TT_H_SIYOU_CD = '" & _db.rmSQ(siyoCD) & "' "
            sql = sql & N & "   WHERE TT_H_SIYOU_CD = '" & _db.rmSQ(siyoCD.PadRight(2, " ")) & "' "

            sql = sql & N & "   AND TT_H_HIN_CD = '" & _db.rmSQ(txtHinsyu.Text) & "' "
            sql = sql & N & "   AND TT_H_SENSIN_CD = '" & _db.rmSQ(txtSensin.Text) & "' "
            sql = sql & N & "   AND TT_H_SIZE_CD = '" & _db.rmSQ(txtSize.Text) & "' "
            sql = sql & N & "   AND TT_H_COLOR_CD = '" & _db.rmSQ(txtColor.Text) & "' "

            Dim iRecCnt As Integer
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If Not iRecCnt = 0 Then     '抽出レコード件数が0件以外の場合
                txtSiyo.Focus()
                lblHinmei.Text = ""     '品名のクリア
                Throw New UsrDefException("品名コードは既に登録されています。", _msgHd.getMSG("alreadyHinmei"))
            End If


        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　 品名重複チェック
    '   （処理概要）集計品名マスタを検索し、品名コードの重複がないかチェックする
    '　　●入力パラメータ： prmSiyo　   仕様コードまたは集計対象品仕様コード
    '                    ： prmHinsyu   品種コードまたは集計対象品品種コード
    '                    ： prmSensin　 線心数コードまたは集計対象品線心数コード
    '                    ： prmSize　   サイズコードまたは集計対象品サイズコード
    '                    ： prmColor　  色コードまたは集計対象品色コード
    '　　●関数戻り値　　： なし
    '-------------------------------------------------------------------------------
    Private Sub checkKHinmeiCDRepeat(ByVal prmSiyo As String, ByVal prmHinsyu As String, ByVal prmSensin As String, _
                                                            ByVal prmSize As String, ByVal prmColor As String)
        Try
            '品名を検索用につなげる
            '仕様コードが1桁の場合は、半角スペースを加える
            Dim siyoCD As String = ""
            If _db.rmSQ(prmSiyo).Length = 1 Then
                siyoCD = siyoCD & _db.rmSQ(prmSiyo) & " "
            Else
                siyoCD = _db.rmSQ(prmSiyo)
            End If

            '計画品名コード作成
            Dim kHinmei As String = ""
            kHinmei = siyoCD & _db.rmSQ(prmHinsyu) & _db.rmSQ(prmSensin) & _db.rmSQ(prmSize) & _db.rmSQ(prmColor)

            'M12
            Dim sql As String = ""
            sql = ""
            sql = sql & N & " SELECT "
            sql = sql & N & " KHINMEICD "
            sql = sql & N & " FROM M12SYUYAKU "
            sql = sql & N & "   WHERE HINMEICD = '" & kHinmei & "' "

            Dim iRecCnt As Integer
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If Not iRecCnt = 0 Then     '抽出レコード件数が0件以外の場合
                If prmSiyo.Equals(txtSiyo.Text) Then
                    txtSiyo.Focus()
                Else
                    txtSSiyo.Focus()
                End If
                Throw New UsrDefException("入力されたコードは以下のコードの実品名コードとして登録されています。", _
                    _msgHd.getMSG("alreakyAddJituhinmeiCD", "計画品名コード　：　" & _db.rmNullStr(ds.Tables(RS).Rows(0)("KHINMEICD"))))
            End If

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　 キー項目入力チェック
    '   （処理概要）各項目の未入力チェック・入力値妥当性チェックを行なう
    '-------------------------------------------------------------------------------
    Private Sub checkInputKey()
        Try

            '品名表示チェック
            If "".Equals(lblHinmei.Text) Then
                Throw New UsrDefException("必須入力です。", _msgHd.getMSG("requiredImput", "【品名】"), txtSeisaku)
            End If

            '製作区分未入力チェック
            If "".Equals(txtSeisaku.Text) Then
                Throw New UsrDefException("必須入力です。", _msgHd.getMSG("requiredImput", "【製作区分】"), txtSeisaku)
            End If

            '標準ロット長未入力チェック
            If "".Equals(txtHyoujunLot.Text) Then
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【標準ロット長】"), txtHyoujunLot)
            End If

            '単長未入力チェック
            If "".Equals(txtTantyou.Text) Then
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【単長】"), txtTantyou)
            End If

            '標準ロット長・製作単長入力値チェック(標準ロット長÷製作単長=整数)
            If CInt(txtHyoujunLot.Text) Mod CInt(txtTantyou.Text) <> 0 Then
                Throw New UsrDefException("標準ロット長または製作単長が違います。", _msgHd.getMSG("failLotOrTantyou"), txtHyoujunLot)
            End If

            '単長×条数が「9999999」を超える場合
            If CInt(txtTantyou.Text) * CInt(lblJosu.Text) > 9999999 Then
                Throw New UsrDefException("単長×条数の値が7桁を超えています。", _msgHd.getMSG("over7Keta"), txtTantyou)
            End If

            '条数が4桁を超える場合
            If CInt(lblJosu.Text).ToString.Length > 4 Then
                Throw New UsrDefException("条数の値が4桁を超えています。", _msgHd.getMSG("over4KetaJosu"), txtHyoujunLot)
            End If

            '住電日立未入力チェック
            If "".Equals(txtSumiHonsu.Text) Then
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【住電日立分】"), txtSumiHonsu)
            End If

            '住電日立本数範囲チェック
            If (CInt(txtSumiHonsu.Text) < 0) Or (CInt(txtSumiHonsu.Text) > CInt(lblJosu.Text)) Then
                Throw New UsrDefException("範囲外の値が入力されました。", _msgHd.getMSG("errOutOfRange", "【住電日立分】"), txtSumiHonsu)
            End If

            '条数妥当性チェック
            If Not CInt(lblJosu.Text) = CInt(lblKNDHonsu.Text) + CInt(txtSumiHonsu.Text) Then
                Throw New UsrDefException("北日本分と住電日立分の合計が条数と一致していません。", _msgHd.getMSG("notJosuSumKndAndSumi"), txtSumiHonsu)
            End If

            '在庫・繰返
            If "".Equals(txtZaiko.Text) Then
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【在庫・繰返】"), txtZaiko)
            End If

            If txtZaiko.Text = ZAIKOCD_ZAIKO Then
                '需要先
                If "".Equals(txtJuyousaki.Text) Then
                    Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【需要先】"), txtJuyousaki)
                End If

                'サイズ展開
                If "".Equals(txtSizeTenkai.Text) Then
                    Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【サイズ展開】"), txtSizeTenkai)
                End If

                '品種区分
                If "".Equals(cboHinsyuKbn.Text) Then
                    Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【品種区分】"), cboHinsyuKbn)
                End If

                '基準月数
                If "".Equals(txtKijunTuki.Text) Then
                    Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【基準月数】"), txtKijunTuki)
                End If
            Else
                '注文先長さチェック
                If System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(txtChumonsaki.Text) > 20 Then
                    Throw New UsrDefException("20バイトまでで入力してください。", _msgHd.getMSG("over20bite", "【注文先】"), txtChumonsaki)
                End If
            End If

            '科目コード未入力チェック（※製作区分=2：外注 の場合のみ）
            If SEISAKUKBN_GAITYU.Equals(txtSeisaku.Text) And "".Equals(txtKamoku.Text) Then
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【科目コード】"), txtKamoku)
            End If
            '' 2011/01/13 add start sugano
            '科目コード桁数チェック（※製作区分=2：外注の場合のみ）
            If SEISAKUKBN_GAITYU.Equals(txtSeisaku.Text) And Len(Trim(txtKamoku.Text)) <> 5 Then
                Throw New UsrDefException("科目コードは５桁で入力して下さい。", _msgHd.getMSG("notKamokuCDKeta", "【科目コード】"), txtKamoku)
            End If
            '' 2011/01/13 add end sugano

            '巻枠コード未入力チェック
            If "".Equals(txtMakiwaku.Text) Then
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【巻枠コード】"), txtMakiwaku)
            End If

            '巻枠コード桁数チェック
            If Len(Trim(txtMakiwaku.Text)) <> 6 Then
                Throw New UsrDefException("巻枠コードは６桁で入力して下さい。", _msgHd.getMSG("notMakiwaku6Keta"), txtMakiwaku)
            End If

            '包装・表示区分未入力チェック
            If "".Equals(txtHousou.Text) Then
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【包装・表示区分】"), txtHousou)
            End If

            '仕様書番号未入力チェック
            If "".Equals(txtSiyousyo.Text) Then
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【仕様書番号】"), txtSiyousyo)
            End If

            '仕様書番号未長さチェック
            If System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(txtSiyousyo.Text) > 20 Then
                Throw New UsrDefException("20バイトまでで入力してください。", _msgHd.getMSG("over20bite", "【仕様書番号】"), txtSiyousyo)
            End If

            '製造部門未入力チェック
            If "".Equals(txtSeizouBmn.Text) Then
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【製造部門】"), txtSeizouBmn)
            End If

            '展開区分未入力チェック
            If "".Equals(txtTenkaiKbn.Text) Then
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【展開区分】"), txtTenkaiKbn)
            End If

            '展開区分内容チェック
            If SEISAKUKBN_GAITYU.Equals(txtSeisaku.Text) And Not TENKAIKBN_BUBUN.Equals(txtTenkaiKbn.Text) Then
                Throw New UsrDefException("製作区分「外注」時は展開区分「部分展開」以外選択できません。", _msgHd.getMSG("nonGaicyuSelect"), txtTenkaiKbn)
            End If

            '展開区分が部分展開の場合
            If TENKAIKBN_BUBUN.Equals(txtTenkaiKbn.Text) Then
                '部分展開工程未入力チェック
                If "".Equals(txtBTenkai.Text) Then
                    Throw New UsrDefException("展開区分「部分展開」時は部分展開指定工程は省略できません。", _msgHd.getMSG("nonBubunOmit"), txtBTenkai)
                End If

                '部分展開工程長さチェック
                If System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(txtBTenkai.Text) > 6 Then
                    Throw New UsrDefException("部分展開工程は6バイトまでで入力してください。", _msgHd.getMSG("over6biteBubunTenkai", "【部分展開工程】"), txtBTenkai)
                End If

                '部分展開工程入力内容チェック
                    If Not ("1".Equals(txtBTenkai.Text.Substring(0, 1)) Or "3".Equals(txtBTenkai.Text.Substring(0, 1))) Then
                    Throw New UsrDefException("1または3から始まる工程を入力してください。", _msgHd.getMSG("notBKouteiStart1Or3"), txtBTenkai)
                End If
            End If

            '品質試験区分未入力チェック
            If "".Equals(txtHinsitu.Text) Then
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【品質試験区分】"), txtHinsitu)
            End If

            '立会有無未入力チェック
            If "".Equals(txtTatiai.Text) Then
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【立会有無】"), txtTatiai)
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