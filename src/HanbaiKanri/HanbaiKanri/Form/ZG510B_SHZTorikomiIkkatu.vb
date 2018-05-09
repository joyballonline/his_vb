'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）生産販売在庫取込・一括算出
'    （フォームID）ZG510B_SHZTorikomiIkkatu
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   高木        2010/11/09                 新規              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Public Class ZG510B_SHZTorikomiIkkatu
    Inherits System.Windows.Forms.Form

    '-------------------------------------------------------------------------------
    '   定数定義
    '-------------------------------------------------------------------------------
    Private Const RS As String = "RecSet"                       'データセットテーブル名
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const PGID As String = "ZG510B"

    '-------------------------------------------------------------------------------
    '   変数定義
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu
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

    '-------------------------------------------------------------------------------
    'デフォルトコンストラクタ（隠蔽）
    '-------------------------------------------------------------------------------
    Private Sub New()
        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

    End Sub

    '-------------------------------------------------------------------------------
    'コンストラクタ　メニュー画面から呼ばれる
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

    '-------------------------------------------------------------------------------
    '　フォームロードイベント
    '-------------------------------------------------------------------------------
    Private Sub ZG510B_SHZTorikomiIkkatu_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            '描画関係の設定
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           'サイズが変更されたときに、コントロールがコントロール自体を再描画するかどうかを示す値を設定
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '描画はバッファで実行され、完了後に、結果が画面に出力されるよう設定
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  'コントロールは、画面に直接ではなく、まずバッファに描画されます。これにより、ちらつきを抑えることができます。
            Me.SetStyle(ControlStyles.UserPaint, True)              'コントロールは、オペレーティング システムによってではなく、独自に描画されるよう設定
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   'コントロールはウィンドウ メッセージ WM_ERASEBKGND を無視するように設定

            Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr 'タイトルオプション表示

            '画面初期化
            Call initForm()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   画面初期化
    '   （処理概要）画面内の各項目を初期表示する
    '   ●入力パラメタ  ：なし
    '   ●出力パラメタ  ：なし
    '   ●メソッド戻り値：なし
    '-------------------------------------------------------------------------------
    Private Sub initForm()
        Try

            '処理年月/計画年月の表示
            Dim d As DataSet = _db.selectDB("select SNENGETU,KNENGETU from T01KEIKANRI", RS)
            Dim syoriDate As String = _db.rmNullStr(d.Tables(RS).Rows(0)("SNENGETU"))
            Dim keikakuDate As String = _db.rmNullStr(d.Tables(RS).Rows(0)("KNENGETU"))
            lblSyoriDate.Text = syoriDate.Substring(0, 4) & "/" & syoriDate.Substring(4)
            lblKeikakuDate.Text = keikakuDate.Substring(0, 4) & "/" & keikakuDate.Substring(4)

            '前回実行情報の表示
            Dim sql As String = ""
            Dim iRecCnt As Integer = 0
            sql = sql & N & "select *  "
            sql = sql & N & "from ( "
            sql = sql & N & "    select "
            sql = sql & N & "     ROW_NUMBER() OVER (ORDER BY RECORDID desc) RNUM "
            sql = sql & N & "    ,SDATEEND "
            sql = sql & N & "    ,KENNSU1 "
            sql = sql & N & "    ,KENNSU2 "
            sql = sql & N & "    ,KENNSU3 "
            sql = sql & N & "    ,KENNSU4 "
            sql = sql & N & "    from T91RIREKI "
            sql = sql & N & "    where PGID     = '" & PGID & "' "
            sql = sql & N & ") "
            sql = sql & N & "where RNUM = 1 "
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)
            If iRecCnt < 1 Then
                '履歴なし
                lblZenkaiJikkouDate.Text = ZC110M_Menu.NON_EXECUTE
                lblZenkaiKensu.Text = ""
                lblZSeisanryo.Text = ""
                lblZHanbai.Text = ""
                lblZZaiko.Text = ""
            Else
                '履歴あり
                lblZenkaiJikkouDate.Text = _db.rmNullDate(ds.Tables(RS).Rows(0)("SDATEEND"))
                lblZenkaiKensu.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU4")), "#,##0")
                lblZSeisanryo.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU1")), "#,##0")
                lblZHanbai.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU2")), "#,##0")
                lblZZaiko.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU3")), "#,##0")
            End If

            '今回実行情報の表示
            lblKSeisanryo.Text = Format(_db.rmNullInt(_db.selectDB("select count(*) CNT from T21SEISANM ", RS).Tables(RS).Rows(0)("CNT")), "#,##0")
            lblKHanbai.Text = Format(_db.rmNullInt(_db.selectDB("select count(*) CNT from T13HANBAI ", RS).Tables(RS).Rows(0)("CNT")), "#,##0")
            lblKZaiko.Text = Format(_db.rmNullInt(_db.selectDB("select count(*) CNT from T31ZAIKOJ ", RS).Tables(RS).Rows(0)("CNT")), "#,##0")

            '実行ボタン使用可否
            btnJikkou.Enabled = _updFlg

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　戻るボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnModoru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModoru.Click

        '自画面を終了し、メニュー画面に戻る。
        _parentForm.Show()
        _parentForm.Activate()

        Me.Close()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　実行ボタン押下イベント
    '------------------------------------------------------------------------------------------------------
    Private Sub btnJikkou_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJikkou.Click

        Try
            '実行確認（実行します。よろしいですか？）
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmRun")
            If rtn = Windows.Forms.DialogResult.No Or rtn = Windows.Forms.DialogResult.Cancel Then Exit Sub

            'ポインタ変更
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor
            Try
                '【バッチ処理開始】
                Dim pb As UtilProgressBar = New UtilProgressBar(Me)                             'プログレスバー画面を表示
                pb.Show()
                Try
                    Dim st As Date = Now                                                        '処理開始日時
                    Dim ed As Date = Nothing                                                    '処理終了日時

                    'プログレスバー設定
                    pb.jobName = "取込処理を実行しています。"
                    pb.oneStep = 1

                    _db.beginTran()
                    Try
                        pb.status = "取込準備中" : pb.maxVal = 1
                        _db.executeDB("delete from T41SEISANK")

                        pb.status = "生産・販売・在庫データ取得中"
                        Call insertBaseRecord()                                                    '1-1 生産販売在庫取込


                        pb.status = "ステータス変更中・・・"
                        ed = Now                    '処理終了日時
                        _parentForm.updateSeigyoTbl(PGID, True, st, ed)                             '2-0 生産量確定/解除

                        pb.status = "実行履歴作成"
                        insertRireki(st, ed)                                  '2-1 実行履歴格納
                    Catch ex As Exception
                        _db.rollbackTran()
                        Throw ex
                    Finally
                        If _db.isTransactionOpen Then _db.commitTran()
                    End Try

                Finally
                    pb.Close()                                                                  'プログレスバー画面消去
                End Try

                Finally
                    Me.Cursor = cur
                End Try

            '終了MSG
            Call _msgHd.dspMSG("completeRun")
            Call btnModoru_Click(Nothing, Nothing)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '   実行履歴作成
    '   （処理概要）確定処理用の実行履歴を作成する
    '   ●入力パラメタ  ：prmStDt   処理開始日時
    '                     prmEdDt   処理終了日時
    '   ●出力パラメタ  ：なし
    '   ●メソッド戻り値：なし
    '-------------------------------------------------------------------------------
    Private Sub insertRireki(ByVal prmStDt As Date, ByVal prmEdDt As Date)

        Dim sql As String = ""
        sql = sql & N & "insert into T91RIREKI "
        sql = sql & N & "( "
        sql = sql & N & " SNENGETU "   '処理年月
        sql = sql & N & ",KNENGETU "   '計画年月
        sql = sql & N & ",PGID "       '機能ID
        sql = sql & N & ",SDATESTART " '処理開始日時
        sql = sql & N & ",SDATEEND "   '処理終了日時
        sql = sql & N & ",KENNSU1 "    'T21実行件数
        sql = sql & N & ",KENNSU2 "    'T13実行件数
        sql = sql & N & ",KENNSU3 "    'T31実行件数
        sql = sql & N & ",KENNSU4 "    'T41実行件数
        sql = sql & N & ",UPDNAME "    '最終更新者
        sql = sql & N & ",UPDDATE "    '最終更新日
        sql = sql & N & ")values( "
        sql = sql & N & "  '" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "' "                             '処理年月
        sql = sql & N & ", '" & _db.rmSQ(lblKeikakuDate.Text.Replace("/", "")) & "' "                           '計画年月
        sql = sql & N & ", '" & _db.rmSQ(PGID) & "' "                                                           '機能ID
        sql = sql & N & ",to_date('" & Format(prmStDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '処理開始日時
        sql = sql & N & ",to_date('" & Format(prmEdDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '処理終了日時
        sql = sql & N & ", " & lblKSeisanryo.Text & " "                                                         'T21実行件数
        sql = sql & N & ", " & lblKHanbai.Text & " "                                                            'T13実行件数
        sql = sql & N & ", " & lblKZaiko.Text & " "                                                             'T31実行件数
        sql = sql & N & ", " & _db.rmNullInt(_db.selectDB("select count(*) CNT from T41SEISANK ", RS).Tables(RS).Rows(0)("CNT")) & " "                        '実行件数
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "                                     '最終更新者
        sql = sql & N & ",sysdate "                                                                             '最終更新日
        sql = sql & N & ") "
        _db.executeDB(sql)

    End Sub

    '-------------------------------------------------------------------------------
    '   生産販売在庫取込
    '   （処理概要）T21生産見込、T13販売計画、T31在庫実績からT41生産計画へデータ投入する
    '   ●入力パラメタ  ：なし
    '   ●出力パラメタ  ：なし
    '   ●メソッド戻り値：なし
    '-------------------------------------------------------------------------------
    Private Sub insertBaseRecord()

        Dim sql As String = ""
        '通常目付分
        sql = sql & N & "INSERT INTO T41SEISANK "
        sql = sql & N & "( "
        sql = sql & N & " KHINMEICD        "  '計画品名コード 
        sql = sql & N & ",ZZZAIKOSU        "  '前々月末在庫数 
        sql = sql & N & ",ZZZAIKORYOU      "  '前々月末在庫量 
        sql = sql & N & ",ZSEISANSU        "  '前月生産実績数 
        sql = sql & N & ",ZSEISANRYOU      "  '前月生産実績量 
        sql = sql & N & ",ZHANBAISU        "  '前月販売実績数 
        sql = sql & N & ",ZHANBAIRYOU      "  '前月販売実績量 
        sql = sql & N & ",ZZAIKOSU         "  '前月末在庫数 
        sql = sql & N & ",ZZAIKORYOU       "  '前月末在庫量 
        sql = sql & N & ",TSEISANSU        "  '当月生産計画数 
        sql = sql & N & ",TSEISANRYOU      "  '当月生産計画量 
        sql = sql & N & ",THANBAISU        "  '当月販売計画数 
        sql = sql & N & ",THANBAIRYOU      "  '当月販売計画量 
        sql = sql & N & ",TZAIKOSU         "  '当月末在庫数 
        sql = sql & N & ",TZAIKORYOU       "  '当月末在庫量 
        sql = sql & N & ",KURIKOSISU       "  '繰越数 
        sql = sql & N & ",KURIKOSIRYOU     "  '繰越量 
        sql = sql & N & ",IKATULOTOSU      "  '一括算出ロット数 
        sql = sql & N & ",LOTOSU           "  'ロット数 
        sql = sql & N & ",YSEISANSU        "  '翌月生産計画数 
        sql = sql & N & ",YSEISANRYOU      "  '翌月生産計画量 
        sql = sql & N & ",YHANBAISU        "  '翌月販売計画数 
        sql = sql & N & ",YHANBAIRYOU      "  '翌月販売計画量 
        sql = sql & N & ",YZAIKOSU         "  '翌月末在庫数 
        sql = sql & N & ",YZAIKORYOU       "  '翌月末在庫量 
        sql = sql & N & ",YZAIKOTUKISU     "  '翌月在庫月数 
        sql = sql & N & ",YYHANBAISU       "  '翌々月販売計画数 
        sql = sql & N & ",YYHANBAIRYOU     "  '翌々月販売計画量 
        sql = sql & N & ",KTUKISU          "  '基準月数 
        sql = sql & N & ",FZAIKOSU         "  '復旧用在庫数 
        sql = sql & N & ",FZAIKORYOU       "  '復旧用在庫量 
        sql = sql & N & ",AZAIKOSU         "  '安全在庫数 
        sql = sql & N & ",AZAIKORYOU       "  '安全在庫量 
        sql = sql & N & ",METSUKE          "  '目付 
        sql = sql & N & ",UPDNAME          "  '端末ID 
        sql = sql & N & ",UPDDATE          "  '更新日時 
        sql = sql & N & ") "

        ''sql = sql & N & "SELECT "
        ''sql = sql & N & " T13.KHINMEICD "                                                                                                                                                                                                                                 '計画品名コード 
        ''sql = sql & N & ",ROUND(NVL(T31.ZZZAIKOSU ,0) * 1000 ,1) "                                                                                                                                                                                                        '前々月末在庫数 
        ''sql = sql & N & ",ROUND(NVL(T31.ZZZAIKORYOU,0) * 1000 ,1) "                                                                                                                                                                                                       '前々月末在庫量 
        ''sql = sql & N & ",ROUND(NVL(T31.ZSEISANSU ,0) * 1000 ,1) "                                                                                                                                                                                                        '前月生産実績数 
        ''sql = sql & N & ",ROUND(NVL(T31.ZSEISANRYOU,0) * 1000 ,1) "                                                                                                                                                                                                       '前月生産実績量 
        ''sql = sql & N & ",ROUND(NVL(T31.ZHANBAISU ,0) * 1000 ,1) "                                                                                                                                                                                                        '前月販売実績数 
        ''sql = sql & N & ",ROUND(NVL(T31.ZHANBAIRYOU,0) * 1000 ,1) "                                                                                                                                                                                                       '前月販売実績量 
        ''sql = sql & N & ",ROUND(NVL(T31.ZZAIKOSU ,0) * 1000 ,1) "                                                                                                                                                                                                         '前月末在庫数 
        ''sql = sql & N & ",ROUND(NVL(T31.ZZAIKORYOU ,0) * 1000 ,1) "                                                                                                                                                                                                       '前月末在庫量 
        ''sql = sql & N & ",CASE WHEN T21.NENGETSU <= '" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "' THEN ROUND(NVL(T21.MYSMIKOMISU ,0) * 1000 ,1) ELSE NULL END "                                                                                                  '当月生産計画数 
        ''sql = sql & N & ",CASE WHEN T21.NENGETSU <= '" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "' THEN ROUND(NVL(T21.MYSMIKOMISU ,0) * 1000 * NVL(T10.MYMETSUKE,0) / 1000 ,1) ELSE NULL END "                                                                    '当月生産計画量 
        ''sql = sql & N & ",ROUND(NVL(T13.THANBAIRYOUHK,0) * 1000 / T10.MYMETSUKE ,1) "                                                                                                                                                                                     '当月販売計画数 
        ''sql = sql & N & ",ROUND(NVL(T13.THANBAIRYOUHK,0) ,1) "                                                                                                                                                                                                            '当月販売計画量 
        ''sql = sql & N & ",ROUND((NVL(T31.ZZAIKOSU  ,0) * 1000) + ( NVL(T21.MYSMIKOMISU,0) * 1000                               ) - ((T13.THANBAIRYOUHK * 1000) / T10.MYMETSUKE) ,1) "                                                                                     '当月末在庫数 
        ''sql = sql & N & ",ROUND((NVL(T31.ZZAIKORYOU,0) * 1000) + ((NVL(T21.MYSMIKOMISU,0) * 1000 * NVL(T10.MYMETSUKE,0)) / 1000) -   T13.THANBAIRYOUHK                          ,1) "                                                                                     '当月末在庫量 
        ''sql = sql & N & ",CASE WHEN T21.NENGETSU > '" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "' THEN ROUND(NVL(T21.MYSMIKOMISU ,0) * 1000,1) ELSE NULL END "                                                                                                    '繰越数 
        ''sql = sql & N & ",CASE WHEN T21.NENGETSU > '" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "' THEN ROUND((NVL(T21.MYSMIKOMISU ,0) * 1000 * NVL(T10.MYMETSUKE,0)) / 1000,1) ELSE NULL END "                                                                    '繰越量 
        ''sql = sql & N & ",0 "                                                                                                                                                                                                                                             '一括算出ロット数 
        ''sql = sql & N & ",0 "                                                                                                                                                                                                                                             'ロット数 
        ''sql = sql & N & ",ROUND(NVL(T21.MYSMIKOMISU,0) * 1000,1) "                                                                                                                                                                                                        '翌月生産計画数 
        ''sql = sql & N & ",ROUND((NVL(T21.MYSMIKOMISU,0) * 1000 * NVL(T10.MYMETSUKE,0)) / 1000,1) "                                                                                                                                                                        '翌月生産計画量 
        ''sql = sql & N & ",ROUND((T13.YHANBAIRYOUHK * 1000) / T10.MYMETSUKE,1) "                                                                                                                                                                                           '翌月販売計画数 
        ''sql = sql & N & ",ROUND(T13.YHANBAIRYOUHK,1) "                                                                                                                                                                                                                    '翌月販売計画量 
        ''sql = sql & N & ",ROUND(((NVL(T31.ZZAIKOSU,0) * 1000) + (NVL(T21.MYSMIKOMISU,0) * 1000) - ((T13.THANBAIRYOUHK * 1000) / T10.MYMETSUKE)) + (NVL(T21.MYSMIKOMISU,0) * 1000) - ((T13.YHANBAIRYOUHK * 1000) / T10.MYMETSUKE),1) "                                     '翌月末在庫数 
        ''sql = sql & N & ",ROUND(((NVL(T31.ZZAIKORYOU,0) * 1000) + ((NVL(T21.MYSMIKOMISU,0) * 1000 * NVL(T10.MYMETSUKE,0)) / 1000) - T13.THANBAIRYOUHK) + ((NVL(T21.MYSMIKOMISU,0) * 1000 * NVL(T10.MYMETSUKE,0)) / 1000) - T13.YHANBAIRYOUHK,1) "                         '翌月末在庫量 
        ''sql = sql & N & ",ROUND((((NVL(T31.ZZAIKORYOU,0) * 1000) + ((NVL(T21.MYSMIKOMISU,0) * 1000 * NVL(T10.MYMETSUKE,0)) / 1000) - T13.THANBAIRYOUHK) + ((NVL(T21.MYSMIKOMISU,0) * 1000 * NVL(T10.MYMETSUKE,0)) / 1000) - T13.YHANBAIRYOUHK) / T13.YYHANBAIRYOUHK,1) "  '翌月在庫月数 
        ''sql = sql & N & ",ROUND((T13.YYHANBAIRYOUHK * 1000) / T10.MYMETSUKE,1) "                                                                                                                                                                                          '翌々月販売計画数 
        ''sql = sql & N & ",ROUND(T13.YYHANBAIRYOUHK ,1) "                                                                                                                                                                                                                  '翌々月販売計画量 
        ''sql = sql & N & ",ROUND(NVL(M11.TT_KZAIKOTUKISU,0),1) "                                                                                                                                                                                                           '基準月数 
        ''sql = sql & N & ",ROUND(NVL(M11.TT_SFUKKYUU,0) * 1000,1) "                                                                                                                                                                                                        '復旧用在庫数 
        ''sql = sql & N & ",ROUND(NVL(M11.TT_SFUKKYUU,0) * NVL(T10.MYMETSUKE,0),1) "                                                                                                                                                                                        '復旧用在庫量 
        ''sql = sql & N & ",ROUND(NVL(M11.TT_ANNZENZAIKO,0) * 1000,1) "                                                                                                                                                                                                     '安全在庫数 
        ''sql = sql & N & ",ROUND(NVL(M11.TT_ANNZENZAIKO,0) * NVL(T10.MYMETSUKE,0),1) "                                                                                                                                                                                     '安全在庫量 
        ''sql = sql & N & ",ROUND(NVL(T10.MYMETSUKE,0),3) "                                                                                                                                                                                                                 '目付 
        ''sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "                                                                                                                                                                                               '端末ID 
        ''sql = sql & N & ",SYSDATE "                                                                                                                                                                                                                                       '更新日時 
        ''sql = sql & N & "FROM T13HANBAI             T13 "
        ''sql = sql & N & "LEFT JOIN ( "
        ''sql = sql & N & "	SELECT "
        ''sql = sql & N & "	 KHINMEICD,NENGETSU "
        ''sql = sql & N & "	,MAX(SMIKOMISU) MYSMIKOMISU "
        ''sql = sql & N & "	FROM T21SEISANM "
        ''sql = sql & N & "   WHERE TAISYO_FLG = '1' "
        ''sql = sql & N & "	GROUP BY KHINMEICD,NENGETSU "
        ''sql = sql & N & "	)                       T21 ON T13.KHINMEICD = T21.KHINMEICD "

        '-->2010.12.02 chg by takagi
        'sql = sql & N & "SELECT "
        'sql = sql & N & " T13.KHINMEICD "                                                                                                                                                                                                                                 '計画品名コード 
        'sql = sql & N & ",ROUND(NVL(T31.ZZZAIKOSU ,0) / 1000 ,1) "                                                                                                                                                                                                        '前々月末在庫数 
        'sql = sql & N & ",ROUND(NVL(T31.ZZZAIKORYOU,0) / 1000 ,1) "                                                                                                                                                                                                       '前々月末在庫量 
        'sql = sql & N & ",ROUND(NVL(T31.ZSEISANSU ,0) / 1000 ,1) "                                                                                                                                                                                                        '前月生産実績数 
        'sql = sql & N & ",ROUND(NVL(T31.ZSEISANRYOU,0) / 1000 ,1) "                                                                                                                                                                                                       '前月生産実績量 
        'sql = sql & N & ",ROUND(NVL(T31.ZHANBAISU ,0) / 1000 ,1) "                                                                                                                                                                                                        '前月販売実績数 
        'sql = sql & N & ",ROUND(NVL(T31.ZHANBAIRYOU,0) / 1000 ,1) "                                                                                                                                                                                                       '前月販売実績量 
        'sql = sql & N & ",ROUND(NVL(T31.ZZAIKOSU ,0) / 1000 ,1) "                                                                                                                                                                                                         '前月末在庫数 
        'sql = sql & N & ",ROUND(NVL(T31.ZZAIKORYOU ,0) / 1000 ,1) "                                                                                                                                                                                                       '前月末在庫量 
        'sql = sql & N & ",ROUND(NVL(T21L.MYSMIKOMISU ,0) / 1000 ,1) "                                                                                                  '当月生産計画数 
        'sql = sql & N & ",ROUND(NVL(T21L.MYSMIKOMISU ,0) / 1000 * NVL(T10.MYMETSUKE,0) / 1000 ,1) "                                                                    '当月生産計画量 
        'sql = sql & N & ",ROUND(NVL(T13.THANBAIRYOUHK,0) * 1000 / T10.MYMETSUKE ,1) "                                                                                                                                                                                     '当月販売計画数 
        'sql = sql & N & ",ROUND(NVL(T13.THANBAIRYOUHK,0) ,1) "                                                                                                                                                                                                            '当月販売計画量 
        'sql = sql & N & ",ROUND((NVL(T31.ZZAIKOSU  ,0) / 1000) + ( NVL(T21L.MYSMIKOMISU,0) / 1000                               ) - ((T13.THANBAIRYOUHK * 1000) / T10.MYMETSUKE) ,1) "                                                                                     '当月末在庫数 
        'sql = sql & N & ",ROUND((NVL(T31.ZZAIKORYOU,0) / 1000) + ((NVL(T21L.MYSMIKOMISU,0) / 1000 * NVL(T10.MYMETSUKE,0)) / 1000) -   T13.THANBAIRYOUHK                          ,1) "                                                                                     '当月末在庫量 
        'sql = sql & N & ",ROUND(NVL(T21U.MYSMIKOMISU ,0) / 1000,1) "                                                                                                    '繰越数 
        'sql = sql & N & ",ROUND((NVL(T21U.MYSMIKOMISU ,0) / 1000 * NVL(T10.MYMETSUKE,0)) / 1000,1) "                                                                    '繰越量 
        'sql = sql & N & ",0 "                                                                                                                                                                                                                                             '一括算出ロット数 
        'sql = sql & N & ",0 "                                                                                                                                                                                                                                             'ロット数 
        'sql = sql & N & ",ROUND(NVL(T21U.MYSMIKOMISU,0) / 1000,1) "                                                                                                                                                                                                        '翌月生産計画数 
        'sql = sql & N & ",ROUND((NVL(T21U.MYSMIKOMISU,0) / 1000 * NVL(T10.MYMETSUKE,0)) / 1000,1) "                                                                                                                                                                        '翌月生産計画量 
        'sql = sql & N & ",ROUND((T13.YHANBAIRYOUHK * 1000) / T10.MYMETSUKE,1) "                                                                                                                                                                                           '翌月販売計画数 
        'sql = sql & N & ",ROUND(T13.YHANBAIRYOUHK,1) "                                                                                                                                                                                                                    '翌月販売計画量 
        'sql = sql & N & ",ROUND(((NVL(T31.ZZAIKOSU,0) / 1000) + (NVL(T21L.MYSMIKOMISU,0) / 1000) - ((T13.THANBAIRYOUHK * 1000) / T10.MYMETSUKE)) + (NVL(T21U.MYSMIKOMISU,0) / 1000) - ((T13.YHANBAIRYOUHK * 1000) / T10.MYMETSUKE),1) "                                     '翌月末在庫数 
        'sql = sql & N & ",ROUND(((NVL(T31.ZZAIKORYOU,0) / 1000) + ((NVL(T21L.MYSMIKOMISU,0) / 1000 * NVL(T10.MYMETSUKE,0)) / 1000) - T13.THANBAIRYOUHK) + ((NVL(T21U.MYSMIKOMISU,0) / 1000 * NVL(T10.MYMETSUKE,0)) / 1000) - T13.YHANBAIRYOUHK,1) "                         '翌月末在庫量 
        ''-->2010.12.02 upd by takagi
        ''sql = sql & N & ",ROUND((((NVL(T31.ZZAIKORYOU,0) / 1000) + ((NVL(T21L.MYSMIKOMISU,0) / 1000 * NVL(T10.MYMETSUKE,0)) / 1000) - T13.THANBAIRYOUHK) + ((NVL(T21U.MYSMIKOMISU,0) / 1000 * NVL(T10.MYMETSUKE,0)) / 1000) - T13.YHANBAIRYOUHK) / T13.YYHANBAIRYOUHK,1) "  '翌月在庫月数 
        'sql = sql & N & ",DECODE(T13.YYHANBAIRYOUHK,0,NULL,ROUND((((NVL(T31.ZZAIKORYOU,0) / 1000) + ((NVL(T21L.MYSMIKOMISU,0) / 1000 * NVL(T10.MYMETSUKE,0)) / 1000) - T13.THANBAIRYOUHK) + ((NVL(T21U.MYSMIKOMISU,0) / 1000 * NVL(T10.MYMETSUKE,0)) / 1000) - T13.YHANBAIRYOUHK) / T13.YYHANBAIRYOUHK,1)) "  '翌月在庫月数 
        ''<--2010.12.02 upd by takagi
        'sql = sql & N & ",ROUND((T13.YYHANBAIRYOUHK * 1000) / T10.MYMETSUKE,1) "                                                                                                                                                                                          '翌々月販売計画数 
        'sql = sql & N & ",ROUND(T13.YYHANBAIRYOUHK ,1) "                                                                                                                                                                                                                  '翌々月販売計画量 
        'sql = sql & N & ",ROUND(NVL(M11.TT_KZAIKOTUKISU,0),1) "                                                                                                                                                                                                           '基準月数 
        'sql = sql & N & ",ROUND(NVL(M11.TT_SFUKKYUU,0) / 1000,1) "                                                                                                                                                                                                        '復旧用在庫数 
        'sql = sql & N & ",ROUND((NVL(M11.TT_SFUKKYUU,0) / 1000) * NVL(T10.MYMETSUKE,0) / 1000 ,1) "                                                                                                                                                                       '復旧用在庫量 
        'sql = sql & N & ",ROUND(NVL(M11.TT_ANNZENZAIKO,0) / 1000,1) "                                                                                                                                                                                                     '安全在庫数 
        'sql = sql & N & ",ROUND((NVL(M11.TT_ANNZENZAIKO,0) / 1000) * NVL(T10.MYMETSUKE,0) / 1000 ,1) "                                                                                                                                                                    '安全在庫量 
        'sql = sql & N & ",ROUND(NVL(T10.MYMETSUKE,0),3) "                                                                                                                                                                                                                 '目付 
        'sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "                                                                                                                                                                                               '端末ID 
        'sql = sql & N & ",SYSDATE "                                                                                                                                                                                                                                       '更新日時 
        sql = sql & N & "SELECT "
        sql = sql & N & " T13.KHINMEICD "                                                                   '計画品名コード 
        sql = sql & N & ",ROUND(NVL(T31.ZZZAIKOSU    ,0) / 1000 ,1) "                                       '前々月末在庫数 
        sql = sql & N & ",ROUND(NVL(T31.ZZZAIKORYOU  ,0) / 1000 ,1) "                                       '前々月末在庫量 
        sql = sql & N & ",ROUND(NVL(T31.ZSEISANSU    ,0) / 1000 ,1) "                                       '前月生産実績数 
        sql = sql & N & ",ROUND(NVL(T31.ZSEISANRYOU  ,0) / 1000 ,1) "                                       '前月生産実績量 
        sql = sql & N & ",ROUND(NVL(T31.ZHANBAISU    ,0) / 1000 ,1) "                                       '前月販売実績数 
        sql = sql & N & ",ROUND(NVL(T31.ZHANBAIRYOU  ,0) / 1000 ,1) "                                       '前月販売実績量 
        sql = sql & N & ",ROUND(NVL(T31.ZZAIKOSU     ,0) / 1000 ,1) "                                       '前月末在庫数 
        sql = sql & N & ",ROUND(NVL(T31.ZZAIKORYOU   ,0) / 1000 ,1) "                                       '前月末在庫量 
        '-->2011.01.16 chg by takagi #82
        'sql = sql & N & ",ROUND(NVL(T21L.MYSMIKOMISU ,0) / 1000 ,1) "                                       '当月生産計画数 
        'sql = sql & N & ",ROUND(NVL(T21L.MYSMIKOMISU ,0) / 1000 * NVL(T13.METSUKE,0) / 1000 ,1) "           '当月生産計画量 
        'sql = sql & N & ",ROUND(NVL(T13.THANBAIRYOUHK,0) * 1000 / T13.METSUKE ,1) "                         '当月販売計画数 
        'sql = sql & N & ",ROUND(NVL(T13.THANBAIRYOUHK,0) ,1) "                                              '当月販売計画量 
        'sql = sql & N & ",ROUND(   ( NVL(T31.ZZAIKOSU    ,0)   / 1000        ) "
        'sql = sql & N & "        + ( NVL(T21L.MYSMIKOMISU,0)   / 1000        ) "
        'sql = sql & N & "        - ((T13.THANBAIRYOUHK * 1000) / T13.METSUKE )  , 1 ) "                     '当月末在庫数 
        'sql = sql & N & ",ROUND(   ( NVL(T31.ZZAIKORYOU  ,0) / 1000                             ) "
        'sql = sql & N & "        + ((NVL(T21L.MYSMIKOMISU,0) / 1000 * NVL(T13.METSUKE,0)) / 1000) "
        'sql = sql & N & "        - ( T13.THANBAIRYOUHK                                          )  ,1) "    '当月末在庫量 
        sql = sql & N & ",ROUND(NVL(T21L.MYSMIKOMISU ,0) / 1000 ,4) "                                       '当月生産計画数 
        sql = sql & N & ",ROUND(NVL(T21L.MYSMIKOMISU ,0) / 1000 * NVL(T13.METSUKE,0) / 1000 ,4) "           '当月生産計画量 
        sql = sql & N & ",ROUND(NVL(T13.THANBAIRYOUHK,0) * 1000 / T13.METSUKE ,4) "                         '当月販売計画数 
        sql = sql & N & ",ROUND(NVL(T13.THANBAIRYOUHK,0) ,4) "                                              '当月販売計画量 
        sql = sql & N & ",ROUND(   ( NVL(T31.ZZAIKOSU    ,0)   / 1000        ) "
        sql = sql & N & "        + ( NVL(T21L.MYSMIKOMISU,0)   / 1000        ) "
        sql = sql & N & "        - ((T13.THANBAIRYOUHK * 1000) / T13.METSUKE )  , 4 ) "                     '当月末在庫数 
        sql = sql & N & ",ROUND(   ( NVL(T31.ZZAIKORYOU  ,0) / 1000                             ) "
        sql = sql & N & "        + ((NVL(T21L.MYSMIKOMISU,0) / 1000 * NVL(T13.METSUKE,0)) / 1000) "
        sql = sql & N & "        - ( T13.THANBAIRYOUHK                                          )  ,4) "    '当月末在庫量 
        '<--2011.01.16 chg by takagi #82
        sql = sql & N & ",ROUND(NVL(T21U.MYSMIKOMISU ,0) / 1000,1) "                                        '繰越数 
        sql = sql & N & ",ROUND((NVL(T21U.MYSMIKOMISU ,0) / 1000 * NVL(T13.METSUKE,0)) / 1000,1) "          '繰越量 
        sql = sql & N & ",0 "                                                                               '一括算出ロット数 
        sql = sql & N & ",0 "                                                                               'ロット数 
        '-->2011.01.16 chg by takagi #82
        'sql = sql & N & ",ROUND(NVL(T21U.MYSMIKOMISU,0) / 1000,1) "                                         '翌月生産計画数 
        'sql = sql & N & ",ROUND((NVL(T21U.MYSMIKOMISU,0) / 1000 * NVL(T13.METSUKE,0)) / 1000,1) "           '翌月生産計画量 
        'sql = sql & N & ",ROUND((T13.YHANBAIRYOUHK * 1000) / T13.METSUKE,1) "                               '翌月販売計画数 
        'sql = sql & N & ",ROUND(T13.YHANBAIRYOUHK,1) "                                                      '翌月販売計画量 
        'sql = sql & N & ",ROUND(    (  ( NVL(T31.ZZAIKOSU    ,0)    / 1000       ) "
        'sql = sql & N & "            + ( NVL(T21L.MYSMIKOMISU,0)    / 1000       ) "
        'sql = sql & N & "            - ( (T13.THANBAIRYOUHK * 1000) / T13.METSUKE) "
        'sql = sql & N & "           ) "
        'sql = sql & N & "         + ( NVL(T21U.MYSMIKOMISU,0   ) / 1000          ) "
        'sql = sql & N & "         - ( (T13.YHANBAIRYOUHK * 1000) / T13.METSUKE   )      ,1) "               '翌月末在庫数 
        'sql = sql & N & ",ROUND(   (  (NVL(T31.ZZAIKORYOU  ,0) / 1000                            ) "
        'sql = sql & N & "           + (NVL(T21L.MYSMIKOMISU,0) / 1000 * NVL(T13.METSUKE,0) / 1000) "
        'sql = sql & N & "           -  T13.THANBAIRYOUHK "
        'sql = sql & N & "          ) "
        'sql = sql & N & "        + (NVL(T21U.MYSMIKOMISU,0) / 1000 * NVL(T13.METSUKE,0) / 1000) "
        'sql = sql & N & "        - T13.YHANBAIRYOUHK                                    ,1) "               '翌月末在庫量 
        sql = sql & N & ",ROUND(NVL(T21U.MYSMIKOMISU,0) / 1000,4) "                                         '翌月生産計画数 
        sql = sql & N & ",ROUND((NVL(T21U.MYSMIKOMISU,0) / 1000 * NVL(T13.METSUKE,0)) / 1000,4) "           '翌月生産計画量 
        sql = sql & N & ",ROUND((T13.YHANBAIRYOUHK * 1000) / T13.METSUKE,4) "                               '翌月販売計画数 
        sql = sql & N & ",ROUND(T13.YHANBAIRYOUHK,4) "                                                      '翌月販売計画量 
        sql = sql & N & ",ROUND(    (  ( NVL(T31.ZZAIKOSU    ,0)    / 1000       ) "
        sql = sql & N & "            + ( NVL(T21L.MYSMIKOMISU,0)    / 1000       ) "
        sql = sql & N & "            - ( (T13.THANBAIRYOUHK * 1000) / T13.METSUKE) "
        sql = sql & N & "           ) "
        sql = sql & N & "         + ( NVL(T21U.MYSMIKOMISU,0   ) / 1000          ) "
        sql = sql & N & "         - ( (T13.YHANBAIRYOUHK * 1000) / T13.METSUKE   )      ,4) "               '翌月末在庫数 
        sql = sql & N & ",ROUND(   (  (NVL(T31.ZZAIKORYOU  ,0) / 1000                            ) "
        sql = sql & N & "           + (NVL(T21L.MYSMIKOMISU,0) / 1000 * NVL(T13.METSUKE,0) / 1000) "
        sql = sql & N & "           -  T13.THANBAIRYOUHK "
        sql = sql & N & "          ) "
        sql = sql & N & "        + (NVL(T21U.MYSMIKOMISU,0) / 1000 * NVL(T13.METSUKE,0) / 1000) "
        sql = sql & N & "        - T13.YHANBAIRYOUHK                                    ,4) "               '翌月末在庫量 
        '<--2011.01.16 chg by takagi #82

        '-->2011.01.18 chg by takagi #82
        ''sql = sql & N & ",DECODE( T13.YYHANBAIRYOUHK ,0 ,NULL "
        ''sql = sql & N & "        ,ROUND( (  (  (NVL(T31.ZZAIKORYOU  ,0) / 1000                            ) "
        ''sql = sql & N & "                    + (NVL(T21L.MYSMIKOMISU,0) / 1000 * NVL(T13.METSUKE,0) / 1000) "
        ''sql = sql & N & "                    - T13.THANBAIRYOUHK "
        ''sql = sql & N & "                   ) "
        ''sql = sql & N & "                 + (  (NVL(T21U.MYSMIKOMISU,0) / 1000 * NVL(T13.METSUKE,0)       ) / 1000) "
        ''sql = sql & N & "                 - T13.YHANBAIRYOUHK) / T13.YYHANBAIRYOUHK     ,1)) "              '翌月在庫月数 
        sql = sql & N & ",DECODE(T13.YYHANBAIRYOUHK, 0, NULL, ROUND( "
        sql = sql & N & "     (   (  (NVL(T31.ZZAIKORYOU  ,0) / 1000                            ) "
        sql = sql & N & "               + (NVL(T21L.MYSMIKOMISU,0) / 1000 * NVL(T13.METSUKE,0) / 1000) "
        sql = sql & N & "               -  T13.THANBAIRYOUHK "
        sql = sql & N & "              ) "
        sql = sql & N & "            + (NVL(T21U.MYSMIKOMISU,0) / 1000 * NVL(T13.METSUKE,0) / 1000) "
        sql = sql & N & "            - T13.YHANBAIRYOUHK                                    ) "             '翌月末在庫量 
        sql = sql & N & "     / "
        sql = sql & N & "     (T13.YYHANBAIRYOUHK ) "                                                       '翌々月販売計画量 
        sql = sql & N & "     ,4)) "
        '<--2011.01.18 chg by takagi #82

        '-->2011.01.16 chg by takagi #82
        'sql = sql & N & ",ROUND((T13.YYHANBAIRYOUHK * 1000) / T13.METSUKE,1) "                             '翌々月販売計画数 
        'sql = sql & N & ",ROUND(T13.YYHANBAIRYOUHK ,1) "                                                   '翌々月販売計画量 
        sql = sql & N & ",ROUND((T13.YYHANBAIRYOUHK * 1000) / T13.METSUKE,4) "                              '翌々月販売計画数 
        sql = sql & N & ",ROUND(T13.YYHANBAIRYOUHK ,4) "                                                    '翌々月販売計画量 
        '<--2011.01.16 chg by takagi #82
        sql = sql & N & ",ROUND(NVL(M11.TT_KZAIKOTUKISU,0),1) "                                             '基準月数 
        sql = sql & N & ",ROUND(NVL(M11.TT_SFUKKYUU,0) / 1000,1) "                                          '復旧用在庫数 
        sql = sql & N & ",ROUND((NVL(M11.TT_SFUKKYUU,0) / 1000) * NVL(T13.METSUKE,0) / 1000 ,1) "           '復旧用在庫量 
        sql = sql & N & ",ROUND(NVL(M11.TT_ANNZENZAIKO,0) / 1000,1) "                                       '安全在庫数 
        sql = sql & N & ",ROUND((NVL(M11.TT_ANNZENZAIKO,0) / 1000) * NVL(T13.METSUKE,0) / 1000 ,1) "        '安全在庫量 
        sql = sql & N & ",ROUND(NVL(T13.METSUKE,0),3) "                                                     '目付 
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "                                 '端末ID 
        sql = sql & N & ",SYSDATE "                                                                         '更新日時 
        '<--2010.12.02 chg by takagi
        sql = sql & N & "FROM T13HANBAI             T13 "
        sql = sql & N & "LEFT JOIN ( "
        sql = sql & N & "	SELECT "
        sql = sql & N & "	 KHINMEICD "
        sql = sql & N & "	,SUM(SMIKOMISU) MYSMIKOMISU "
        sql = sql & N & "	FROM T21SEISANM "
        sql = sql & N & "   WHERE TAISYO_FLG = '1' "
        sql = sql & N & "    AND  NENGETSU <= '" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "'"
        sql = sql & N & "	GROUP BY KHINMEICD "
        sql = sql & N & "	)                       T21L ON T13.KHINMEICD = T21L.KHINMEICD "
        sql = sql & N & "LEFT JOIN ( "
        sql = sql & N & "	SELECT "
        sql = sql & N & "	 KHINMEICD "
        sql = sql & N & "	,SUM(SMIKOMISU) MYSMIKOMISU "
        sql = sql & N & "	FROM T21SEISANM "
        sql = sql & N & "   WHERE TAISYO_FLG = '1' "
        sql = sql & N & "    AND  NENGETSU > '" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "'"
        sql = sql & N & "	GROUP BY KHINMEICD "
        sql = sql & N & "	)                       T21U ON T13.KHINMEICD = T21U.KHINMEICD "

        sql = sql & N & "LEFT JOIN T31ZAIKOJ        T31 ON T13.KHINMEICD = T31.KHINMEICD "
        sql = sql & N & "LEFT JOIN M11KEIKAKUHIN    M11 ON T13.KHINMEICD = M11.TT_KHINMEICD "
        '-->2010.12.12 chg by takagi
        ''-->2010.12.02 upd by takagi
        ''sql = sql & N & "LEFT JOIN ( "
        'sql = sql & N & "INNER JOIN ( "
        ''<--2010.12.02 upd by takagi
        'sql = sql & N & "	SELECT "
        'sql = sql & N & "	 M.KHINMEICD "
        'sql = sql & N & "	,MIN(T.METSUKE) MYMETSUKE "
        'sql = sql & N & "	FROM T10HINHANJ       T "
        'sql = sql & N & "	INNER JOIN M12SYUYAKU M "
        'sql = sql & N & "	ON T.HINMEICD = M.HINMEICD "
        ''-->2010.12.02 add by takagi
        'sql = sql & N & "	WHERE T.METSUKE IS NOT NULL "
        ''<--2010.12.02 add by takagi
        'sql = sql & N & "	GROUP BY M.KHINMEICD ) T10 ON T13.KHINMEICD = T10.KHINMEICD "
        ''-->2010.12.02 add by takagi
        'sql = sql & N & "	                           AND T10.MYMETSUKE != 0 "
        ''<--2010.12.02 add by takagi
        sql = sql & N & "WHERE T13.METSUKE IS NOT NULL AND T13.METSUKE != 0 "
        '<--2010.12.12 chg by takagi
        _db.executeDB(sql)

        '-->2010.12.12 add by takagi
        '目付０分
        sql = ""
        sql = sql & N & "INSERT INTO T41SEISANK "
        sql = sql & N & "( "
        sql = sql & N & " KHINMEICD        "  '計画品名コード 
        sql = sql & N & ",ZZZAIKOSU        "  '前々月末在庫数 
        sql = sql & N & ",ZZZAIKORYOU      "  '前々月末在庫量 
        sql = sql & N & ",ZSEISANSU        "  '前月生産実績数 
        sql = sql & N & ",ZSEISANRYOU      "  '前月生産実績量 
        sql = sql & N & ",ZHANBAISU        "  '前月販売実績数 
        sql = sql & N & ",ZHANBAIRYOU      "  '前月販売実績量 
        sql = sql & N & ",ZZAIKOSU         "  '前月末在庫数 
        sql = sql & N & ",ZZAIKORYOU       "  '前月末在庫量 
        sql = sql & N & ",TSEISANSU        "  '当月生産計画数 
        sql = sql & N & ",TSEISANRYOU      "  '当月生産計画量 
        sql = sql & N & ",THANBAISU        "  '当月販売計画数 
        sql = sql & N & ",THANBAIRYOU      "  '当月販売計画量 
        sql = sql & N & ",TZAIKOSU         "  '当月末在庫数 
        sql = sql & N & ",TZAIKORYOU       "  '当月末在庫量 
        sql = sql & N & ",KURIKOSISU       "  '繰越数 
        sql = sql & N & ",KURIKOSIRYOU     "  '繰越量 
        sql = sql & N & ",IKATULOTOSU      "  '一括算出ロット数 
        sql = sql & N & ",LOTOSU           "  'ロット数 
        sql = sql & N & ",YSEISANSU        "  '翌月生産計画数 
        sql = sql & N & ",YSEISANRYOU      "  '翌月生産計画量 
        sql = sql & N & ",YHANBAISU        "  '翌月販売計画数 
        sql = sql & N & ",YHANBAIRYOU      "  '翌月販売計画量 
        sql = sql & N & ",YZAIKOSU         "  '翌月末在庫数 
        sql = sql & N & ",YZAIKORYOU       "  '翌月末在庫量 
        sql = sql & N & ",YZAIKOTUKISU     "  '翌月在庫月数 
        sql = sql & N & ",YYHANBAISU       "  '翌々月販売計画数 
        sql = sql & N & ",YYHANBAIRYOU     "  '翌々月販売計画量 
        sql = sql & N & ",KTUKISU          "  '基準月数 
        sql = sql & N & ",FZAIKOSU         "  '復旧用在庫数 
        sql = sql & N & ",FZAIKORYOU       "  '復旧用在庫量 
        sql = sql & N & ",AZAIKOSU         "  '安全在庫数 
        sql = sql & N & ",AZAIKORYOU       "  '安全在庫量 
        sql = sql & N & ",METSUKE          "  '目付 
        sql = sql & N & ",UPDNAME          "  '端末ID 
        sql = sql & N & ",UPDDATE          "  '更新日時 
        sql = sql & N & ") "
        sql = sql & N & "SELECT "
        sql = sql & N & " T13.KHINMEICD "                                                                   '計画品名コード 
        sql = sql & N & ",ROUND(NVL(T31.ZZZAIKOSU ,0) / 1000 ,1) "                                          '前々月末在庫数 
        sql = sql & N & ",ROUND(NVL(T31.ZZZAIKORYOU,0) / 1000 ,1) "                                         '前々月末在庫量 
        sql = sql & N & ",ROUND(NVL(T31.ZSEISANSU ,0) / 1000 ,1) "                                          '前月生産実績数 
        sql = sql & N & ",ROUND(NVL(T31.ZSEISANRYOU,0) / 1000 ,1) "                                         '前月生産実績量 
        sql = sql & N & ",ROUND(NVL(T31.ZHANBAISU ,0) / 1000 ,1) "                                          '前月販売実績数 
        sql = sql & N & ",ROUND(NVL(T31.ZHANBAIRYOU,0) / 1000 ,1) "                                         '前月販売実績量 
        sql = sql & N & ",ROUND(NVL(T31.ZZAIKOSU ,0) / 1000 ,1) "                                           '前月末在庫数 
        sql = sql & N & ",ROUND(NVL(T31.ZZAIKORYOU ,0) / 1000 ,1) "                                         '前月末在庫量 
        '-->2011.01.16 chg by takagi #82
        'sql = sql & N & ",ROUND(NVL(T21L.MYSMIKOMISU ,0) / 1000 ,1) "                                       '当月生産計画数 
        'sql = sql & N & ",ROUND(NVL(T21L.MYSMIKOMISU ,0) / 1000 * NVL(T13.METSUKE,0) / 1000 ,1) "           '当月生産計画量 
        'sql = sql & N & ",ROUND(NVL(T13.THANBAISUHK,0) ,1) "                                                '当月販売計画数 
        'sql = sql & N & ",0 "                                                                               '当月販売計画量 
        'sql = sql & N & ",ROUND(  (NVL(T31.ZZAIKOSU    ,0) / 1000) "
        'sql = sql & N & "       + (NVL(T21L.MYSMIKOMISU,0) / 1000) "
        'sql = sql & N & "       - T13.THANBAISUHK                      ,1) "                                '当月末在庫数 
        'sql = sql & N & ",0 "                                                                               '当月末在庫量 
        sql = sql & N & ",ROUND(NVL(T21L.MYSMIKOMISU ,0) / 1000 ,4) "                                       '当月生産計画数 
        sql = sql & N & ",ROUND(NVL(T21L.MYSMIKOMISU ,0) / 1000 * NVL(T13.METSUKE,0) / 1000 ,4) "           '当月生産計画量 
        sql = sql & N & ",ROUND(NVL(T13.THANBAISUHK,0) ,4) "                                                '当月販売計画数 
        sql = sql & N & ",0 "                                                                               '当月販売計画量 
        sql = sql & N & ",ROUND(  (NVL(T31.ZZAIKOSU    ,0) / 1000) "
        sql = sql & N & "       + (NVL(T21L.MYSMIKOMISU,0) / 1000) "
        sql = sql & N & "       - T13.THANBAISUHK                      ,4) "                                '当月末在庫数 
        sql = sql & N & ",0 "                                                                               '当月末在庫量 
        '<--2011.01.16 chg by takagi #82
        sql = sql & N & ",ROUND(NVL(T21U.MYSMIKOMISU ,0) / 1000,1) "                                        '繰越数 
        sql = sql & N & ",0 "                                                                               '繰越量 
        sql = sql & N & ",0 "                                                                               '一括算出ロット数 
        sql = sql & N & ",0 "                                                                               'ロット数 
        '-->2011.01.16 chg by takagi #82
        'sql = sql & N & ",ROUND(NVL(T21U.MYSMIKOMISU,0) / 1000,1) "                                         '翌月生産計画数 
        'sql = sql & N & ",ROUND((NVL(T21U.MYSMIKOMISU,0) / 1000 * NVL(T13.METSUKE,0)) / 1000,1) "           '翌月生産計画量 
        'sql = sql & N & ",ROUND(T13.YHANBAISUHK,1) "                                                        '翌月販売計画数 
        'sql = sql & N & ",0 "                                                                               '翌月販売計画量 
        'sql = sql & N & ",ROUND( ( (NVL(T31.ZZAIKOSU    ,0) / 1000) "
        'sql = sql & N & "         +(NVL(T21L.MYSMIKOMISU,0) / 1000) "
        'sql = sql & N & "         - T13.THANBAISUHK"
        'sql = sql & N & "         ) "
        'sql = sql & N & "       + (NVL(T21U.MYSMIKOMISU,0) / 1000) "
        'sql = sql & N & "       - T13.YHANBAISUHK                      ,1) "                                '翌月末在庫数 
        'sql = sql & N & ",0 "                                                                               '翌月末在庫量 
        sql = sql & N & ",ROUND(NVL(T21U.MYSMIKOMISU,0) / 1000,4) "                                         '翌月生産計画数 
        sql = sql & N & ",ROUND((NVL(T21U.MYSMIKOMISU,0) / 1000 * NVL(T13.METSUKE,0)) / 1000,4) "           '翌月生産計画量 
        sql = sql & N & ",ROUND(T13.YHANBAISUHK,4) "                                                        '翌月販売計画数 
        sql = sql & N & ",0 "                                                                               '翌月販売計画量 
        sql = sql & N & ",ROUND( ( (NVL(T31.ZZAIKOSU    ,0) / 1000) "
        sql = sql & N & "         +(NVL(T21L.MYSMIKOMISU,0) / 1000) "
        sql = sql & N & "         - T13.THANBAISUHK"
        sql = sql & N & "         ) "
        sql = sql & N & "       + (NVL(T21U.MYSMIKOMISU,0) / 1000) "
        sql = sql & N & "       - T13.YHANBAISUHK                      ,4) "                                '翌月末在庫数 
        sql = sql & N & ",0 "                                                                               '翌月末在庫量 
        '<--2011.01.16 chg by takagi #82

        '-->2011.01.18 chg by takagi #82
        ''sql = sql & N & ",DECODE(T13.YYHANBAISUHK ,0 ,NULL, "
        ''sql = sql & N & "        ROUND( ( ( (NVL(T31.ZZAIKOSU    ,0) / 1000) "
        ''sql = sql & N & "                 + (NVL(T21L.MYSMIKOMISU,0) / 1000) "
        ''sql = sql & N & "                 - T13.THANBAISUHK "
        ''sql = sql & N & "                 ) "
        ''sql = sql & N & "               + (NVL(T21U.MYSMIKOMISU  ,0) / 1000) "
        ''sql = sql & N & "               - T13.YHANBAISUHK "
        ''sql = sql & N & "               ) / T13.YYHANBAISUHK           ,1)) "                               '翌月在庫月数 
        sql = sql & N & ",DECODE(T13.YYHANBAISUHK ,0 ,NULL , "
        sql = sql & N & "        ROUND( "
        sql = sql & N & "             ( ( (NVL(T31.ZZAIKOSU    ,0) / 1000) "
        sql = sql & N & "                +(NVL(T21L.MYSMIKOMISU,0) / 1000) "
        sql = sql & N & "                - T13.THANBAISUHK"
        sql = sql & N & "                ) "
        sql = sql & N & "              + (NVL(T21U.MYSMIKOMISU,0) / 1000) "
        sql = sql & N & "              - T13.YHANBAISUHK                        ) "                                '翌月末在庫数 
        sql = sql & N & "             / "
        sql = sql & N & "             (T13.YYHANBAISUHK  ) "                                                       '翌々月販売計画数 
        sql = sql & N & "                              ,4)) "
        ''<--2011.01.18 chg by takagi #82

        '-->2011.01.16 chg by takagi #82
        'sql = sql & N & ",ROUND(T13.YYHANBAISUHK,1) "                                                       '翌々月販売計画数 
        'sql = sql & N & ",0 "                                                                               '翌々月販売計画量 
        sql = sql & N & ",ROUND(T13.YYHANBAISUHK,4) "                                                       '翌々月販売計画数 
        sql = sql & N & ",0 "                                                                               '翌々月販売計画量 
        '<--2011.01.16 chg by takagi #82
        sql = sql & N & ",ROUND(NVL(M11.TT_KZAIKOTUKISU,0),1) "                                             '基準月数 
        sql = sql & N & ",ROUND(NVL(M11.TT_SFUKKYUU,0) / 1000,1) "                                          '復旧用在庫数 
        sql = sql & N & ",ROUND((NVL(M11.TT_SFUKKYUU,0) / 1000) * NVL(T13.METSUKE,0) / 1000 ,1) "           '復旧用在庫量 
        sql = sql & N & ",ROUND(NVL(M11.TT_ANNZENZAIKO,0) / 1000,1) "                                       '安全在庫数 
        sql = sql & N & ",ROUND((NVL(M11.TT_ANNZENZAIKO,0) / 1000) * NVL(T13.METSUKE,0) / 1000 ,1) "        '安全在庫量 
        sql = sql & N & ",ROUND(NVL(T13.METSUKE,0),3) "                                                     '目付 
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "                                 '端末ID 
        sql = sql & N & ",SYSDATE "                                                                         '更新日時 
        sql = sql & N & "FROM T13HANBAI             T13 "
        sql = sql & N & "LEFT JOIN ( "
        sql = sql & N & "	SELECT "
        sql = sql & N & "	 KHINMEICD "
        sql = sql & N & "	,SUM(SMIKOMISU) MYSMIKOMISU "
        sql = sql & N & "	FROM T21SEISANM "
        sql = sql & N & "   WHERE TAISYO_FLG = '1' "
        sql = sql & N & "    AND  NENGETSU <= '" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "'"
        sql = sql & N & "	GROUP BY KHINMEICD "
        sql = sql & N & "	)                       T21L ON T13.KHINMEICD = T21L.KHINMEICD "
        sql = sql & N & "LEFT JOIN ( "
        sql = sql & N & "	SELECT "
        sql = sql & N & "	 KHINMEICD "
        sql = sql & N & "	,SUM(SMIKOMISU) MYSMIKOMISU "
        sql = sql & N & "	FROM T21SEISANM "
        sql = sql & N & "   WHERE TAISYO_FLG = '1' "
        sql = sql & N & "    AND  NENGETSU > '" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "'"
        sql = sql & N & "	GROUP BY KHINMEICD "
        sql = sql & N & "	)                       T21U ON T13.KHINMEICD = T21U.KHINMEICD "
        sql = sql & N & "LEFT JOIN T31ZAIKOJ        T31 ON T13.KHINMEICD = T31.KHINMEICD "
        sql = sql & N & "LEFT JOIN M11KEIKAKUHIN    M11 ON T13.KHINMEICD = M11.TT_KHINMEICD "
        sql = sql & N & "WHERE T13.METSUKE IS NOT NULL AND T13.METSUKE = 0 "
        _db.executeDB(sql)
        '<--2010.12.12 add by takagi

        sql = ""
        sql = sql & N & "UPDATE T41SEISANK "
        '-->2011.01.16 chg by takagi #72
        ''sql = sql & N & "SET IKATULOTOSU = CEIL(((YZAIKOTUKISU - KTUKISU) * YYHANBAIRYOU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) "
        ''sql = sql & N & " ,  LOTOSU      = CEIL(((YZAIKOTUKISU - KTUKISU) * YYHANBAIRYOU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) "
        ''sql = sql & N & " ,  YSEISANSU   = (CEIL(((YZAIKOTUKISU - KTUKISU) * YYHANBAIRYOU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSISU "
        ''sql = sql & N & " ,  YSEISANRYOU = (CEIL(((YZAIKOTUKISU - KTUKISU) * YYHANBAIRYOU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSIRYOU "
        ''sql = sql & N & " ,  YZAIKOSU    = TZAIKOSU   - YHANBAISU    + ((CEIL(((YZAIKOTUKISU - KTUKISU) * YYHANBAIRYOU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSISU) "
        ''sql = sql & N & " ,  YZAIKORYOU  = ZZAIKORYOU - YHANBAIRYOU  + ((CEIL(((YZAIKOTUKISU - KTUKISU) * YYHANBAIRYOU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSIRYOU) "
        ''sql = sql & N & " ,  YZAIKOTUKISU= (ZZAIKORYOU - YHANBAIRYOU  + ((CEIL(((YZAIKOTUKISU - KTUKISU) * YYHANBAIRYOU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSIRYOU)) / YYHANBAIRYOU "
        ''sql = sql & N & "SET IKATULOTOSU = CEIL(((KTUKISU - YZAIKOTUKISU) * YYHANBAIRYOU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) "
        'sql = sql & N & " ,  LOTOSU      = CEIL(((KTUKISU - YZAIKOTUKISU) * YYHANBAIRYOU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) "
        'sql = sql & N & " ,  YSEISANSU   = (CEIL(((KTUKISU - YZAIKOTUKISU) * YYHANBAIRYOU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSISU "
        'sql = sql & N & " ,  YSEISANRYOU = (CEIL(((KTUKISU - YZAIKOTUKISU) * YYHANBAIRYOU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) / 1000 * METSUKE / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSIRYOU "
        'sql = sql & N & " ,  YZAIKOSU    = TZAIKOSU   - YHANBAISU    + ((CEIL(((KTUKISU - YZAIKOTUKISU) * YYHANBAIRYOU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSISU) "
        'sql = sql & N & " ,  YZAIKORYOU  = TZAIKORYOU - YHANBAIRYOU  + ((CEIL(((KTUKISU - YZAIKOTUKISU) * YYHANBAIRYOU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) / 1000 * METSUKE / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSIRYOU) "
        'sql = sql & N & " ,  YZAIKOTUKISU= DECODE(YYHANBAIRYOU,0,0, (ZZAIKORYOU - YHANBAIRYOU  + ((CEIL(((KTUKISU - YZAIKOTUKISU) * YYHANBAIRYOU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) / 1000 * METSUKE / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSIRYOU)) / YYHANBAIRYOU ) "
        sql = sql & N & "SET IKATULOTOSU = CEIL( (YYHANBAIRYOU * KTUKISU - TZAIKORYOU + YHANBAIRYOU + KURIKOSIRYOU ) / ((SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD ) / 1000 * METSUKE / 1000) ) "
        sql = sql & N & " ,  LOTOSU      = CEIL( (YYHANBAIRYOU * KTUKISU - TZAIKORYOU + YHANBAIRYOU + KURIKOSIRYOU ) / ((SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD ) / 1000 * METSUKE / 1000) ) "
        sql = sql & N & " ,  YSEISANSU   = (CEIL( (YYHANBAIRYOU * KTUKISU - TZAIKORYOU + YHANBAIRYOU + KURIKOSIRYOU ) / ((SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD ) / 1000 * METSUKE / 1000) ) / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSISU "
        sql = sql & N & " ,  YSEISANRYOU = (CEIL( (YYHANBAIRYOU * KTUKISU - TZAIKORYOU + YHANBAIRYOU + KURIKOSIRYOU ) / ((SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD ) / 1000 * METSUKE / 1000) ) / 1000 * METSUKE / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSIRYOU "
        sql = sql & N & " ,  YZAIKOSU    = TZAIKOSU   - YHANBAISU    + ((CEIL( (YYHANBAIRYOU * KTUKISU - TZAIKORYOU + YHANBAIRYOU + KURIKOSIRYOU ) / ((SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD ) / 1000 * METSUKE / 1000) ) / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSISU) "
        sql = sql & N & " ,  YZAIKORYOU  = TZAIKORYOU - YHANBAIRYOU  + ((CEIL( (YYHANBAIRYOU * KTUKISU - TZAIKORYOU + YHANBAIRYOU + KURIKOSIRYOU ) / ((SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD ) / 1000 * METSUKE / 1000) ) / 1000 * METSUKE / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSIRYOU) "
        sql = sql & N & " ,  YZAIKOTUKISU= DECODE(YYHANBAIRYOU,0,0, (TZAIKORYOU - YHANBAIRYOU  + ((CEIL( (YYHANBAIRYOU * KTUKISU - TZAIKORYOU + YHANBAIRYOU + KURIKOSIRYOU ) / ((SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD ) / 1000 * METSUKE / 1000) ) / 1000 * METSUKE / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSIRYOU)) / YYHANBAIRYOU) "
        '<--2011.01.16 chg by takagi #72
        sql = sql & N & " ,  UPDNAME     = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & " ,  UPDDATE     = SYSDATE "
        sql = sql & N & "WHERE YZAIKOTUKISU < KTUKISU "
        '-->2010.12.12 add by takagi
        sql = sql & N & " AND METSUKE IS NOT NULL AND METSUKE != 0 "
        '<--2010.12.12 add by takagi
        _db.executeDB(sql)

        '-->2010.12.12 add by takagi
        sql = ""
        sql = sql & N & "UPDATE T41SEISANK "
        '-->2011.01.16 chg by takagi #72
        'sql = sql & N & "SET IKATULOTOSU = CEIL(((KTUKISU - YZAIKOTUKISU) * YYHANBAISU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) "
        'sql = sql & N & " ,  LOTOSU      = CEIL(((KTUKISU - YZAIKOTUKISU) * YYHANBAISU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) "
        'sql = sql & N & " ,  YSEISANSU   = (CEIL(((KTUKISU - YZAIKOTUKISU) * YYHANBAISU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSISU "
        'sql = sql & N & " ,  YSEISANRYOU = 0 "
        'sql = sql & N & " ,  YZAIKOSU    = TZAIKOSU   - YHANBAISU    + ((CEIL(((KTUKISU - YZAIKOTUKISU) * YYHANBAISU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSISU) "
        'sql = sql & N & " ,  YZAIKORYOU  = 0 "
        'sql = sql & N & " ,  YZAIKOTUKISU= DECODE(YYHANBAISU,0,0, (ZZAIKOSU - YHANBAISU  + ((CEIL(((KTUKISU - YZAIKOTUKISU) * YYHANBAISU) / (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSISU)) / YYHANBAISU ) "
        sql = sql & N & "SET IKATULOTOSU = CEIL( (YYHANBAISU*KTUKISU-TZAIKOSU+YHANBAISU+KURIKOSISU) /( (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD ) / 1000 ) ) "
        sql = sql & N & " ,  LOTOSU      = CEIL( (YYHANBAISU*KTUKISU-TZAIKOSU+YHANBAISU+KURIKOSISU) /( (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD ) / 1000 ) ) "
        sql = sql & N & " ,  YSEISANSU   = (CEIL( (YYHANBAISU*KTUKISU-TZAIKOSU+YHANBAISU+KURIKOSISU) /( (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD ) / 1000 ) ) / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSISU "
        sql = sql & N & " ,  YSEISANRYOU = 0 "
        sql = sql & N & " ,  YZAIKOSU    = TZAIKOSU   - YHANBAISU    + ((CEIL( (YYHANBAISU*KTUKISU-TZAIKOSU+YHANBAISU+KURIKOSISU) /( (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD ) / 1000 ) ) / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSISU) "
        sql = sql & N & " ,  YZAIKORYOU  = 0 "
        sql = sql & N & " ,  YZAIKOTUKISU= DECODE(YYHANBAISU,0,0, (TZAIKOSU   - YHANBAISU    + ((CEIL( (YYHANBAISU*KTUKISU-TZAIKOSU+YHANBAISU+KURIKOSISU) /( (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD ) / 1000 ) ) / 1000 * (SELECT TT_LOT FROM M11KEIKAKUHIN WHERE TT_KHINMEICD = KHINMEICD )) + KURIKOSISU)) / YYHANBAISU ) "
        '<--2011.01.16 chg by takagi #72
        sql = sql & N & " ,  UPDNAME     = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & " ,  UPDDATE     = SYSDATE "
        sql = sql & N & "WHERE YZAIKOTUKISU < KTUKISU "
        sql = sql & N & " AND METSUKE IS NOT NULL AND METSUKE = 0 "
        _db.executeDB(sql)
        '<--2010.12.12 add by takagi

    End Sub

End Class