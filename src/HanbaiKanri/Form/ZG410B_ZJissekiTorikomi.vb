'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）在庫実績取込指示
'    （フォームID）ZG410B_ZJissekiTorikomi
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   高木        2010/10/19                 新規              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Public Class ZG410B_ZJissekiTorikomi
    Inherits System.Windows.Forms.Form

    '-------------------------------------------------------------------------------
    '   定数定義
    '-------------------------------------------------------------------------------
    Private Const RS As String = "RecSet"                       'データセットテーブル名
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const PGID As String = "ZG410B"
    Private Const IMP_LOG_NM As String = "在庫実績取込処理出力情報.txt"

    '-------------------------------------------------------------------------------
    '   変数定義
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu
    Private _updFlg As Boolean = False  '更新可否
    Private _ukeharaiDb As UtilDBIf     '電線在庫取得用コネクション

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

        '電線在庫マスタ用コネクションの生成
        Dim iniWk As StartUp.iniType = StartUp.iniValue
        _ukeharaiDb = New UtilOleDBDebugger(iniWk.UdlFilePath_Ukeharai, iniWk.LogFilePath, StartUp.DebugMode)
    End Sub

    '-------------------------------------------------------------------------------
    '　フォームクローズイベント
    '-------------------------------------------------------------------------------
    Private Sub ZG410B_ZJissekiTorikomi_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            '電線在庫マスタ用コネクションの破棄
            _ukeharaiDb.close()
        Catch ex As Exception
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　フォームロードイベント
    '-------------------------------------------------------------------------------
    Private Sub ZG410B_ZJissekiTorikomi_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            sql = sql & N & "    ,NAME1 "
            sql = sql & N & "    from T91RIREKI "
            sql = sql & N & "    where PGID     = '" & PGID & "' "
            sql = sql & N & ") "
            sql = sql & N & "where RNUM = 1 "
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)
            If iRecCnt < 1 Then
                '履歴なし
                lblZenkaiJikkouDate.Text = ZC110M_Menu.NON_EXECUTE
                lblZenkaiKensu.Text = ""
                lblZenkaiYM.Text = ""
            Else
                '履歴あり
                lblZenkaiJikkouDate.Text = _db.rmNullDate(ds.Tables(RS).Rows(0)("SDATEEND"))
                lblZenkaiKensu.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU1")), "#,##0")
                Dim wk As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("NAME1"))
                lblZenkaiYM.Text = wk.Substring(0, 4) & "/" & wk.Substring(4, 2)
            End If

            '今回実行情報の表示
            lblKonkaiYM.Text = Format(DateAdd(DateInterval.Month, -1, CDate(lblSyoriDate.Text & "/01")), "yyyy/MM")
            sql = ""
            sql = sql & N & "select count(*) CNT from 電線在庫マスタ "
            sql = sql & N & "where 年月 = '" & lblKonkaiYM.Text.Replace("/", "") & "'"
            lblKonkaiKensu.Text = Format(_db.rmNullInt(_ukeharaiDb.selectDB(sql, RS).Tables(RS).Rows(0)("CNT")), "#,##0")

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

            Dim cntNullKeikakuHinmei As Integer = 0

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

                    pb.status = "取込準備中" : pb.maxVal = 1
                    _db.executeDB("delete from ZG410B_W10 where UPDNAME = '" & UtilClass.getComputerName() & "'")
                    _db.executeDB("delete from ZG410B_W11 where UPDNAME = '" & UtilClass.getComputerName() & "'")
                    pb.value += 1                                                               '0-0 WK初期化


                    pb.status = "在庫データ取得中"
                    pb.value = 0 : pb.maxVal = CInt(lblKonkaiKensu.Text)
                    Call importZaikoMast(pb)                                                    '1-1 実績転送

                    pb.status = "在庫実績データ構築中・・・"
                    Call insertZaikoJisseki(cntNullKeikakuHinmei)                               '1-2 在庫実績取込
                    _db.beginTran()
                    Try
                        _db.executeDB("delete from T31ZAIKOJ")
                        _db.executeDB("delete from T72ZAIKOS where NENTETSU = '" & lblKonkaiYM.Text.Replace("/", "") & "'")

                        Dim insCnt As Integer
                        Call insertZaikoJisseki2(insCnt)                                        '1-2 在庫実績取込
                        Call insertZaikoJissekiDB()                                             '1-3 在庫実績取込

                        pb.status = "ステータス変更中・・・"
                        ed = Now                    '処理終了日時
                        _parentForm.updateSeigyoTbl(PGID, True, st, ed)                         '2-0 生産量確定/解除

                        pb.status = "実行履歴作成"
                        insertRireki(insCnt, st, ed)                                            '2-1 実行履歴格納
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
            Dim optionMsg As String = ""
            ''If cntNullKeikakuHinmei > 0 Then
            ''    optionMsg = "-----------------------------------------------------------------" & N & _
            ''                "計画品名コードを照合できない実品名コードが" & cntNullKeikakuHinmei & "件存在しました。" & N & _
            ''                "当該データは取込されていません。" & N & N & _
            ''                "詳細な実品名コードはログを確認してください。"
            ''End If
            Call _msgHd.dspMSG("completeRun", optionMsg)
            ''If cntNullKeikakuHinmei > 0 Then
            ''    'ログ表示
            ''    Try
            ''        System.Diagnostics.Process.Start(UtilClass.getAppPath(StartUp.assembly) & "\" & IMP_LOG_NM)   '関連付いたアプリで起動
            ''    Catch ex As Exception
            ''    End Try
            ''End If
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
    Private Sub insertRireki(ByVal prmInsCnt As Integer, ByVal prmStDt As Date, ByVal prmEdDt As Date)

        Dim sql As String = ""
        sql = sql & N & "insert into T91RIREKI "
        sql = sql & N & "( "
        sql = sql & N & " SNENGETU "   '処理年月
        sql = sql & N & ",KNENGETU "   '計画年月
        sql = sql & N & ",PGID "       '機能ID
        sql = sql & N & ",SDATESTART " '処理開始日時
        sql = sql & N & ",SDATEEND "   '処理終了日時
        sql = sql & N & ",KENNSU1 "    '実行件数
        sql = sql & N & ",NAME1 "      '対象年月
        sql = sql & N & ",UPDNAME "    '最終更新者
        sql = sql & N & ",UPDDATE "    '最終更新日
        sql = sql & N & ")values( "
        sql = sql & N & "  '" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "' "                             '処理年月
        sql = sql & N & ", '" & _db.rmSQ(lblKeikakuDate.Text.Replace("/", "")) & "' "                           '計画年月
        sql = sql & N & ", '" & _db.rmSQ(PGID) & "' "                                                           '機能ID
        sql = sql & N & ",to_date('" & Format(prmStDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '処理開始日時
        sql = sql & N & ",to_date('" & Format(prmEdDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '処理終了日時
        'sql = sql & N & ", " & prmInsCnt & " "                                                                  '実行件数
        sql = sql & N & ", " & lblKonkaiKensu.Text & " "                                                        '実行件数
        sql = sql & N & ", '" & lblKonkaiYM.Text.Replace("/", "") & "' "                                        '対象年月
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "                                     '最終更新者
        sql = sql & N & ",sysdate "                                                                             '最終更新日
        sql = sql & N & ") "
        _db.executeDB(sql)

    End Sub

    '-------------------------------------------------------------------------------
    '   在庫実績作成
    '   （処理概要）ワークより在庫実績へデータ投入する
    '   ●入力パラメタ  ：なし
    '   ●出力パラメタ  ：なし
    '   ●メソッド戻り値：なし
    '-------------------------------------------------------------------------------
    Private Sub insertZaikoJisseki(ByRef prmRefNullMetsukeCnt As Integer)

        Dim sql As String = ""
        sql = sql & N & "INSERT INTO ZG410B_W11 "
        sql = sql & N & "( "
        sql = sql & N & " HINMEICD         --実品名コード "
        sql = sql & N & ",KHINMEICD        --計画品名コード "
        sql = sql & N & ",ZZZAIKOSU        --前々月末在庫実績数 "
        sql = sql & N & ",ZZZAIKORYOU      --前々月末在庫実績量 "
        sql = sql & N & ",ZSEISANSU        --前月生産実績数 "
        sql = sql & N & ",ZSEISANRYOU      --前月生産実績量 "
        sql = sql & N & ",ZHANBAISU        --前月販売実績数 "
        sql = sql & N & ",ZHANBAIRYOU      --前月販売実績量 "
        sql = sql & N & ",ZZAIKOSU         --前月末在庫実績数 "
        sql = sql & N & ",ZZAIKORYOU       --前月末在庫実績量 "
        sql = sql & N & ",UPDNAME          --端末ID "
        sql = sql & N & ",UPDDATE          --更新日時 "
        sql = sql & N & ") "
        sql = sql & N & "select "
        sql = sql & N & " S.HINMEICD         --実品名コード "
        sql = sql & N & ",M.KHINMEICD        --計画品名コード "
        sql = sql & N & ",S.ZZZAIKOSU        --前々月末在庫実績数 "
        sql = sql & N & ",S.ZZZAIKORYOU      --前々月末在庫実績量 "
        sql = sql & N & ",S.ZSEISANSU        --前月生産実績数 "
        sql = sql & N & ",S.ZSEISANRYOU      --前月生産実績量 "
        sql = sql & N & ",S.ZHANBAISU        --前月販売実績数 "
        sql = sql & N & ",S.ZHANBAIRYOU      --前月販売実績量 "
        sql = sql & N & ",S.ZZAIKOSU         --前月末在庫実績数 "
        sql = sql & N & ",S.ZZAIKORYOU       --前月末在庫実績量 "
        sql = sql & N & ",S.UPDNAME          --端末ID "
        sql = sql & N & ",S.UPDDATE          --更新日時 "
        sql = sql & N & "FROM ( "
        sql = sql & N & "     SELECT "
        sql = sql & N & "      (W.SHIYO || W.HINSHU || W.SENSHIN || W.SAIZU || W.IRO) HINMEICD --実品名コード "
        sql = sql & N & "     ,SUM(NVL(W.KAGI_ZEN_SU        ,0) + NVL(W.FUNA_ZEN_SU        ,0) + NVL(W.SAPO_ZEN_SU        ,0) + NVL(W.HEN_ZEN_SU        ,0) + NVL(W.KARI_ZEN_SU  ,0) + NVL(W.YU_ZEN_SU   ,0)) ZZZAIKOSU    --前々月末在庫実績数 "
        sql = sql & N & "     ,SUM(NVL(W.KAGI_ZEN_DOU       ,0) + NVL(W.FUNA_ZEN_DOU       ,0) + NVL(W.SAPO_ZEN_DOU       ,0) + NVL(W.HEN_ZEN_DOU       ,0) + NVL(W.KARI_ZEN_DOU ,0) + NVL(W.YU_ZEN_DOU  ,0)) ZZZAIKORYOU  --前々月末在庫実績量 "
        sql = sql & N & "     ,SUM(NVL(W.KAGI_TOU_NYUKO_SU  ,0) + NVL(W.FUNA_TOU_NYUKO_SU  ,0) + NVL(W.SAPO_TOU_NYUKO_SU  ,0)                                                                               ) ZSEISANSU    --前月生産実績数 "
        sql = sql & N & "     ,SUM(NVL(W.KAGI_TOU_NYUKO_DOU ,0) + NVL(W.FUNA_TOU_NYUKO_DOU ,0) + NVL(W.SAPO_TOU_NYUKO_DOU ,0)                                                                               ) ZSEISANRYOU  --前月生産実績量 "
        sql = sql & N & "     ,SUM(NVL(W.KAGI_TOU_SHUKO_SU  ,0) + NVL(W.FUNA_TOU_SHUKO_SU  ,0) + NVL(W.SAPO_TOU_SHUKO_SU  ,0) + NVL(W.HEN_TOU_SHUKO_SU  ,0) + NVL(W.KARI_TOU_SU  ,0) + NVL(W.YU_TOU_SU   ,0)) ZHANBAISU    --前月販売実績数 "
        sql = sql & N & "     ,SUM(NVL(W.KAGI_TOU_DHUKO_DOU ,0) + NVL(W.FUNA_TOU_SHUKO_DOU ,0) + NVL(W.SAPO_TOU_SHUKO_DOU ,0) + NVL(W.HEN_TOU_SHUKO_DOU ,0) + NVL(W.KARI_TOU_DOU ,0) + NVL(W.YU_TOU_DOU  ,0)) ZHANBAIRYOU  --前月販売実績量 "
        sql = sql & N & "     ,SUM( "
        sql = sql & N & "        ( "
        sql = sql & N & "          NVL(W.KAGI_ZEN_SU        ,0) + NVL(W.FUNA_ZEN_SU        ,0) + NVL(W.SAPO_ZEN_SU        ,0) + NVL(W.HEN_ZEN_SU        ,0) + NVL(W.KARI_ZEN_SU  ,0) + NVL(W.YU_ZEN_SU   ,0) --(前々月末在庫実績数) "
        sql = sql & N & "          + "
        sql = sql & N & "          NVL(W.KAGI_TOU_NYUKO_SU  ,0) + NVL(W.FUNA_TOU_NYUKO_SU  ,0) + NVL(W.SAPO_TOU_NYUKO_SU  ,0)                                                                                --(前月生産実績数) "
        sql = sql & N & "        ) "
        sql = sql & N & "        - "
        sql = sql & N & "        ( "
        sql = sql & N & "          NVL(W.KAGI_TOU_SHUKO_SU  ,0) + NVL(W.FUNA_TOU_SHUKO_SU  ,0) + NVL(W.SAPO_TOU_SHUKO_SU  ,0) + NVL(W.HEN_TOU_SHUKO_SU  ,0) + NVL(W.KARI_TOU_SU  ,0) + NVL(W.YU_TOU_SU   ,0) --(前月販売実績数) "
        sql = sql & N & "        ) "
        sql = sql & N & "      )                                                                                                                                                                              ZZAIKOSU     --前月末在庫実績数 "
        sql = sql & N & "     ,SUM( "
        sql = sql & N & "        ( "
        sql = sql & N & "          NVL(W.KAGI_ZEN_DOU       ,0) + NVL(W.FUNA_ZEN_DOU       ,0) + NVL(W.SAPO_ZEN_DOU       ,0) + NVL(W.HEN_ZEN_DOU       ,0) + NVL(W.KARI_ZEN_DOU ,0) + NVL(W.YU_ZEN_DOU  ,0) --(前々月末在庫実績量) "
        sql = sql & N & "          + "
        sql = sql & N & "          NVL(W.KAGI_TOU_NYUKO_DOU ,0) + NVL(W.FUNA_TOU_NYUKO_DOU ,0) + NVL(W.SAPO_TOU_NYUKO_DOU ,0)                                                                                --(前月生産実績量) "
        sql = sql & N & "        ) "
        sql = sql & N & "        - "
        sql = sql & N & "        ( "
        sql = sql & N & "          NVL(W.KAGI_TOU_DHUKO_DOU ,0) + NVL(W.FUNA_TOU_SHUKO_DOU ,0) + NVL(W.SAPO_TOU_SHUKO_DOU ,0) + NVL(W.HEN_TOU_SHUKO_DOU ,0) + NVL(W.KARI_TOU_DOU ,0) + NVL(W.YU_TOU_DOU  ,0) --(前月販売実績量) "
        sql = sql & N & "        ) "
        sql = sql & N & "      )                                                                                                                                                                              ZZAIKORYOU   --前月末在庫実績量 "
        sql = sql & N & "     ,'" & UtilClass.getComputerName() & "'                                                                                                                                          UPDNAME      --端末ID "
        sql = sql & N & "     ,SYSDATE                                                                                                                                                                        UPDDATE      --更新日時 "
        sql = sql & N & "     FROM ZG410B_W10 W "
        sql = sql & N & "     WHERE UPDNAME = '" & UtilClass.getComputerName() & "' "
        sql = sql & N & "     GROUP BY (W.SHIYO || W.HINSHU || W.SENSHIN || W.SAIZU || W.IRO) "
        sql = sql & N & "     ) S "
        sql = sql & N & "LEFT JOIN M12SYUYAKU M ON S.HINMEICD = M.HINMEICD "
        _db.executeDB(sql)
        
        sql = ""
        sql = sql & N & "SELECT HINMEICD  "
        sql = sql & N & "FROM ZG410B_W11 "
        sql = sql & N & "WHERE UPDNAME = '" & UtilClass.getComputerName() & "' "
        sql = sql & N & "  AND KHINMEICD IS NULL "
        sql = sql & N & "ORDER BY HINMEICD "
        Dim ds As DataSet = _db.selectDB(sql, RS, prmRefNullMetsukeCnt)
        If prmRefNullMetsukeCnt > 0 Then

            Dim logBuf As System.Text.StringBuilder = New System.Text.StringBuilder
            logBuf.Append(Format(Now(), "yyyy/MM/dd HH:mm:ss") & "実行" & N)
            logBuf.Append("==========================================================" & N)
            logBuf.Append("■在庫実績取込処理出力情報■" & N)
            logBuf.Append("  集約品名マスタ(M12)未登録品名コード（計画品名コードに紐付けることが出来なかった実品名コード）" & N)
            logBuf.Append("----------------------------------------------------------" & N)
            For i As Integer = 0 To prmRefNullMetsukeCnt - 1
                logBuf.Append(_db.rmNullStr(ds.Tables(RS).Rows(i)("HINMEICD")) & N)
            Next
            logBuf.Append("==========================================================")
            Dim tw As UtilMDL.Text.UtilTextWriter = New UtilMDL.Text.UtilTextWriter(UtilClass.getAppPath(StartUp.assembly) & "\" & IMP_LOG_NM)
            tw.open(False)
            Try
                tw.writeLine(logBuf.ToString)
            Finally
                tw.close()
            End Try

        End If
    End Sub

    Private Sub insertZaikoJisseki2(ByRef prmRefInsCnt As Integer)

        Dim Sql As String = ""
        Sql = Sql & N & "INSERT INTO T31ZAIKOJ "
        Sql = Sql & N & "( "
        Sql = Sql & N & " KHINMEICD "
        Sql = Sql & N & ",ZZZAIKOSU "
        Sql = Sql & N & ",ZZZAIKORYOU "
        Sql = Sql & N & ",ZSEISANSU "
        Sql = Sql & N & ",ZSEISANRYOU "
        Sql = Sql & N & ",ZHANBAISU "
        Sql = Sql & N & ",ZHANBAIRYOU "
        Sql = Sql & N & ",ZZAIKOSU "
        Sql = Sql & N & ",ZZAIKORYOU "
        Sql = Sql & N & ",UPDNAME "
        Sql = Sql & N & ",UPDDATE "
        Sql = Sql & N & ") "
        Sql = Sql & N & "SELECT  "
        Sql = Sql & N & " SUB.KHINMEICD "
        Sql = Sql & N & ",SUB.SUM_ZZZAIKOSU "
        Sql = Sql & N & ",SUB.SUM_ZZZAIKORYOU "
        Sql = Sql & N & ",SUB.SUM_ZSEISANSU "
        Sql = Sql & N & ",SUB.SUM_ZSEISANRYOU "
        Sql = Sql & N & ",SUB.SUM_ZHANBAISU "
        Sql = Sql & N & ",SUB.SUM_ZHANBAIRYOU "
        Sql = Sql & N & ",SUB.SUM_ZZAIKOSU "
        Sql = Sql & N & ",SUB.SUM_ZZAIKORYOU "
        Sql = Sql & N & ",SUB.MAX_UPDNAME "
        Sql = Sql & N & ",SUB.SYSDT "
        Sql = Sql & N & "FROM ( "
        Sql = Sql & N & "     SELECT  "
        Sql = Sql & N & "      KHINMEICD        KHINMEICD "
        Sql = Sql & N & "     ,SUM(ZZZAIKOSU)   SUM_ZZZAIKOSU "
        Sql = Sql & N & "     ,SUM(ZZZAIKORYOU) SUM_ZZZAIKORYOU "
        Sql = Sql & N & "     ,SUM(ZSEISANSU)   SUM_ZSEISANSU "
        Sql = Sql & N & "     ,SUM(ZSEISANRYOU) SUM_ZSEISANRYOU "
        Sql = Sql & N & "     ,SUM(ZHANBAISU)   SUM_ZHANBAISU "
        Sql = Sql & N & "     ,SUM(ZHANBAIRYOU) SUM_ZHANBAIRYOU "
        Sql = Sql & N & "     ,SUM(ZZAIKOSU)    SUM_ZZAIKOSU "
        Sql = Sql & N & "     ,SUM(ZZAIKORYOU)  SUM_ZZAIKORYOU "
        Sql = Sql & N & "     ,MAX(UPDNAME)     MAX_UPDNAME "
        Sql = Sql & N & "     ,SYSDATE          SYSDT "
        Sql = Sql & N & "     FROM ZG410B_W11 "
        Sql = Sql & N & "     WHERE KHINMEICD IS NOT NULL "
        Sql = Sql & N & "       AND UPDNAME   = '" & UtilClass.getComputerName() & "' "
        Sql = Sql & N & "     GROUP BY KHINMEICD "
        Sql = Sql & N & "     ) SUB "
        Sql = Sql & N & "INNER JOIN M11KEIKAKUHIN M11"
        Sql = Sql & N & " ON   SUB.KHINMEICD = M11.TT_KHINMEICD "
        Sql = Sql & N & "  AND M11.TT_SYUBETU = 1 " '（1：在庫）
        _db.executeDB(Sql, prmRefInsCnt)

    End Sub

    '-------------------------------------------------------------------------------
    '   在庫実績DB作成
    '   （処理概要）ワークより在庫実績DB(T72)へデータ投入する
    '   ●入力パラメタ  ：なし
    '   ●出力パラメタ  ：なし
    '   ●メソッド戻り値：なし
    '-------------------------------------------------------------------------------
    Private Sub insertZaikoJissekiDB()

        Dim sql As String = ""
        Sql = Sql & N & "INSERT INTO T72ZAIKOS "
        Sql = Sql & N & "( "
        sql = sql & N & " NENTETSU                       --年月 "
        sql = sql & N & ",SHIYO                          --品名CD仕様 "
        sql = sql & N & ",HINSHU                         --品名CD品種 "
        sql = sql & N & ",SENSHIN                        --品名CD線心数 "
        sql = sql & N & ",SAIZU                          --品名CDサイズ "
        sql = sql & N & ",IRO                            --品名CD色 "
        sql = sql & N & ",FUKA                           --品名CD設計付加記号 "
        sql = sql & N & ",KAITEI                         --品名CD設計改訂記号 "
        '-->2010.12.10 add by takagi
        sql = sql & N & ",HINKBN2 "
        '<--2010.12.10 add by takagi
        sql = sql & N & ",HINSHUMEI                      --品種名 "
        sql = sql & N & ",SAIZUMEI                       --サイズ名 "
        sql = sql & N & ",IROMEI                         --色名 "
        sql = sql & N & ",TANI                           --単位 "
        sql = sql & N & ",KAGI_ZEN_SU                    --鈎取前月在庫数 "
        sql = sql & N & ",KAGI_ZEN_KINGAKU               --鈎取前月在庫金額 "
        sql = sql & N & ",KAGI_ZEN_DOU                   --鈎取前月在庫銅量 "
        sql = sql & N & ",KAGI_TOU_NYUKO_SU              --鈎取当月入庫数 "
        sql = sql & N & ",KAGI_TOU_NYUKO_KINGAKU         --鈎取当月入庫金額 "
        sql = sql & N & ",KAGI_TOU_NYUKO_DOU             --鈎取当月入庫銅量 "
        sql = sql & N & ",KAGI_TOU_SHUKO_SU              --鈎取当月出庫数 "
        sql = sql & N & ",KAGI_TOU_SHUKO_KINGAKU         --鈎取当月出庫金額 "
        sql = sql & N & ",KAGI_TOU_DHUKO_DOU             --鈎取当月出庫銅量 "
        sql = sql & N & ",FUNA_ZEN_SU                    --船岡前月在庫数 "
        sql = sql & N & ",FUNA_ZEN_KINGAKU               --船岡前月在庫金額 "
        sql = sql & N & ",FUNA_ZEN_DOU                   --船岡前月在庫銅量 "
        sql = sql & N & ",FUNA_TOU_NYUKO_SU              --船岡当月入庫数 "
        sql = sql & N & ",FUNA_TOU_NYUKO_KINGAKU         --船岡当月入庫金額 "
        sql = sql & N & ",FUNA_TOU_NYUKO_DOU             --船岡当月入庫銅量 "
        sql = sql & N & ",FUNA_TOU_SHUKO_SU              --船岡当月出庫数 "
        sql = sql & N & ",FUNA_TOU_SHUKO_KINGAKU         --船岡当月出庫金額 "
        sql = sql & N & ",FUNA_TOU_SHUKO_DOU             --船岡当月出庫銅量 "
        sql = sql & N & ",SAPO_ZEN_SU                    --札幌前月在庫数 "
        sql = sql & N & ",SAPO_ZEN_KINGAKU               --札幌前月在庫金額 "
        sql = sql & N & ",SAPO_ZEN_DOU                   --札幌前月在庫銅量 "
        sql = sql & N & ",SAPO_TOU_NYUKO_SU              --札幌当月入庫数 "
        sql = sql & N & ",SAPO_TOU_NYUKO_KINGAKU         --札幌当月入庫金額 "
        sql = sql & N & ",SAPO_TOU_NYUKO_DOU             --札幌当月入庫銅量 "
        sql = sql & N & ",SAPO_TOU_SHUKO_SU              --札幌当月出庫数 "
        sql = sql & N & ",SAPO_TOU_SHUKO_KINGAKU         --札幌当月出庫金額 "
        sql = sql & N & ",SAPO_TOU_SHUKO_DOU             --札幌当月出庫銅量 "
        sql = sql & N & ",HEN_ZEN_SU                     --返品前月在庫数 "
        sql = sql & N & ",HEN_ZEN_KINGAKU                --返品前月在庫金額 "
        sql = sql & N & ",HEN_ZEN_DOU                    --返品前月在庫銅量 "
        sql = sql & N & ",HEN_TOU_SHUKO_SU               --返品当月出庫数 "
        sql = sql & N & ",HEN_TOU_SHUKO_KINGAKU          --返品当月出庫金額 "
        sql = sql & N & ",HEN_TOU_SHUKO_DOU              --返品当月出庫銅量 "
        sql = sql & N & ",KARI_ZEN_SU                    --仮出荷前月在庫数 "
        sql = sql & N & ",KARI_ZEN_KINGAKU               --仮出荷前月在庫金額 "
        sql = sql & N & ",KARI_ZEN_DOU                   --仮出荷前月在庫銅量 "
        sql = sql & N & ",KARI_TOU_SU                    --仮出荷当月出庫数 "
        sql = sql & N & ",KARI_TOU_KINGAKU               --仮出荷当月出庫金額 "
        sql = sql & N & ",KARI_TOU_DOU                   --仮出荷当月出庫銅量 "
        sql = sql & N & ",COST                           --製造コスト "
        sql = sql & N & ",YU_ZEN_SU                      --輸送中前月在庫数 "
        sql = sql & N & ",YU_ZEN_KINGAKU                 --輸送中前月在庫金額 "
        sql = sql & N & ",YU_ZEN_DOU                     --輸送中前月在庫銅量 "
        sql = sql & N & ",YU_TOU_SU                      --輸送中当月出庫数 "
        sql = sql & N & ",YU_TOU_KNGAKU                  --輸送中当月出庫金額 "
        sql = sql & N & ",YU_TOU_DOU                     --輸送中当月出庫銅量 "
        sql = sql & N & ",CREATEDT                       --作成日時 "
        sql = sql & N & ",UPDDT                          --更新日時 "
        sql = sql & N & ") "
        Sql = Sql & N & "SELECT "
        sql = sql & N & " NENTETSU                       --年月 "
        sql = sql & N & ",SHIYO                          --品名CD仕様 "
        sql = sql & N & ",HINSHU                         --品名CD品種 "
        sql = sql & N & ",SENSHIN                        --品名CD線心数 "
        sql = sql & N & ",SAIZU                          --品名CDサイズ "
        sql = sql & N & ",IRO                            --品名CD色 "
        sql = sql & N & ",FUKA                           --品名CD設計付加記号 "
        sql = sql & N & ",KAITEI                         --品名CD設計改訂記号 "
        '-->2010.12.10 add by takagi
        sql = sql & N & ",HINKBN2 "
        '<--2010.12.10 add by takagi
        sql = sql & N & ",HINSHUMEI                      --品種名 "
        sql = sql & N & ",SAIZUMEI                       --サイズ名 "
        sql = sql & N & ",IROMEI                         --色名 "
        sql = sql & N & ",TANI                           --単位 "
        sql = sql & N & ",KAGI_ZEN_SU                    --鈎取前月在庫数 "
        sql = sql & N & ",KAGI_ZEN_KINGAKU               --鈎取前月在庫金額 "
        sql = sql & N & ",KAGI_ZEN_DOU                   --鈎取前月在庫銅量 "
        sql = sql & N & ",KAGI_TOU_NYUKO_SU              --鈎取当月入庫数 "
        sql = sql & N & ",KAGI_TOU_NYUKO_KINGAKU         --鈎取当月入庫金額 "
        sql = sql & N & ",KAGI_TOU_NYUKO_DOU             --鈎取当月入庫銅量 "
        sql = sql & N & ",KAGI_TOU_SHUKO_SU              --鈎取当月出庫数 "
        sql = sql & N & ",KAGI_TOU_SHUKO_KINGAKU         --鈎取当月出庫金額 "
        sql = sql & N & ",KAGI_TOU_DHUKO_DOU             --鈎取当月出庫銅量 "
        sql = sql & N & ",FUNA_ZEN_SU                    --船岡前月在庫数 "
        sql = sql & N & ",FUNA_ZEN_KINGAKU               --船岡前月在庫金額 "
        sql = sql & N & ",FUNA_ZEN_DOU                   --船岡前月在庫銅量 "
        sql = sql & N & ",FUNA_TOU_NYUKO_SU              --船岡当月入庫数 "
        sql = sql & N & ",FUNA_TOU_NYUKO_KINGAKU         --船岡当月入庫金額 "
        sql = sql & N & ",FUNA_TOU_NYUKO_DOU             --船岡当月入庫銅量 "
        sql = sql & N & ",FUNA_TOU_SHUKO_SU              --船岡当月出庫数 "
        sql = sql & N & ",FUNA_TOU_SHUKO_KINGAKU         --船岡当月出庫金額 "
        sql = sql & N & ",FUNA_TOU_SHUKO_DOU             --船岡当月出庫銅量 "
        sql = sql & N & ",SAPO_ZEN_SU                    --札幌前月在庫数 "
        sql = sql & N & ",SAPO_ZEN_KINGAKU               --札幌前月在庫金額 "
        sql = sql & N & ",SAPO_ZEN_DOU                   --札幌前月在庫銅量 "
        sql = sql & N & ",SAPO_TOU_NYUKO_SU              --札幌当月入庫数 "
        sql = sql & N & ",SAPO_TOU_NYUKO_KINGAKU         --札幌当月入庫金額 "
        sql = sql & N & ",SAPO_TOU_NYUKO_DOU             --札幌当月入庫銅量 "
        sql = sql & N & ",SAPO_TOU_SHUKO_SU              --札幌当月出庫数 "
        sql = sql & N & ",SAPO_TOU_SHUKO_KINGAKU         --札幌当月出庫金額 "
        sql = sql & N & ",SAPO_TOU_SHUKO_DOU             --札幌当月出庫銅量 "
        sql = sql & N & ",HEN_ZEN_SU                     --返品前月在庫数 "
        sql = sql & N & ",HEN_ZEN_KINGAKU                --返品前月在庫金額 "
        sql = sql & N & ",HEN_ZEN_DOU                    --返品前月在庫銅量 "
        sql = sql & N & ",HEN_TOU_SHUKO_SU               --返品当月出庫数 "
        sql = sql & N & ",HEN_TOU_SHUKO_KINGAKU          --返品当月出庫金額 "
        sql = sql & N & ",HEN_TOU_SHUKO_DOU              --返品当月出庫銅量 "
        sql = sql & N & ",KARI_ZEN_SU                    --仮出荷前月在庫数 "
        sql = sql & N & ",KARI_ZEN_KINGAKU               --仮出荷前月在庫金額 "
        sql = sql & N & ",KARI_ZEN_DOU                   --仮出荷前月在庫銅量 "
        sql = sql & N & ",KARI_TOU_SU                    --仮出荷当月出庫数 "
        sql = sql & N & ",KARI_TOU_KINGAKU               --仮出荷当月出庫金額 "
        sql = sql & N & ",KARI_TOU_DOU                   --仮出荷当月出庫銅量 "
        sql = sql & N & ",COST                           --製造コスト "
        sql = sql & N & ",YU_ZEN_SU                      --輸送中前月在庫数 "
        sql = sql & N & ",YU_ZEN_KINGAKU                 --輸送中前月在庫金額 "
        sql = sql & N & ",YU_ZEN_DOU                     --輸送中前月在庫銅量 "
        sql = sql & N & ",YU_TOU_SU                      --輸送中当月出庫数 "
        sql = sql & N & ",YU_TOU_KNGAKU                  --輸送中当月出庫金額 "
        sql = sql & N & ",YU_TOU_DOU                     --輸送中当月出庫銅量 "
        sql = sql & N & ",ORG_CREATEDT                   --オリジナル作成日時 "
        sql = sql & N & ",ORG_UPDDT                      --オリジナル更新日時 "
        sql = sql & N & "FROM  ZG410B_W10  "                  '在庫実績WK
        sql = sql & N & "WHERE UPDNAME = '" & UtilClass.getComputerName() & "' "
        _db.executeDB(Sql)

    End Sub

    '-------------------------------------------------------------------------------
    '   電線在庫データ取込
    '   （処理概要）SQLSV(電線在庫マスタ)からOracleWKへデータを取り込む
    '   ●入力パラメタ  ：プログレスバー
    '   ●出力パラメタ  ：なし
    '   ●メソッド戻り値：なし
    '-------------------------------------------------------------------------------
    Private Sub importZaikoMast(ByRef prmRefPb As UtilProgressBar)

        Dim sysdate As String = "to_date('" & Format(Now, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS')"

        'SQLSVより抽出
        Dim sql As String = ""
        Dim iRecCnt As Integer = 0
        sql = ""
        sql = sql & N & "SELECT "
        sql = sql & N & " [年月] "
        sql = sql & N & ",[品名CD仕様] "
        sql = sql & N & ",[品名CD品種] "
        sql = sql & N & ",[品名CD線心数] "
        sql = sql & N & ",[品名CDサイズ] "
        sql = sql & N & ",[品名CD色] "
        sql = sql & N & ",[品名CD設計付加記号] "
        sql = sql & N & ",[品名CD設計改訂記号] "
        sql = sql & N & ",[品種名] "
        sql = sql & N & ",[サイズ名] "
        sql = sql & N & ",[色名] "
        sql = sql & N & ",[単位] "
        sql = sql & N & ",[鈎取前月在庫数] "
        sql = sql & N & ",[鈎取前月在庫金額] "
        sql = sql & N & ",[鈎取前月在庫銅量] "
        sql = sql & N & ",[鈎取当月入庫数] "
        sql = sql & N & ",[鈎取当月入庫金額] "
        sql = sql & N & ",[鈎取当月入庫銅量] "
        sql = sql & N & ",[鈎取当月出庫数] "
        sql = sql & N & ",[鈎取当月出庫金額] "
        sql = sql & N & ",[鈎取当月出庫銅量] "
        sql = sql & N & ",[船岡前月在庫数] "
        sql = sql & N & ",[船岡前月在庫金額] "
        sql = sql & N & ",[船岡前月在庫銅量] "
        sql = sql & N & ",[船岡当月入庫数] "
        sql = sql & N & ",[船岡当月入庫金額] "
        sql = sql & N & ",[船岡当月入庫銅量] "
        sql = sql & N & ",[船岡当月出庫数] "
        sql = sql & N & ",[船岡当月出庫金額] "
        sql = sql & N & ",[船岡当月出庫銅量] "
        sql = sql & N & ",[札幌前月在庫数] "
        sql = sql & N & ",[札幌前月在庫金額] "
        sql = sql & N & ",[札幌前月在庫銅量] "
        sql = sql & N & ",[札幌当月入庫数] "
        sql = sql & N & ",[札幌当月入庫金額] "
        sql = sql & N & ",[札幌当月入庫銅量] "
        sql = sql & N & ",[札幌当月出庫数] "
        sql = sql & N & ",[札幌当月出庫金額] "
        sql = sql & N & ",[札幌当月出庫銅量] "
        sql = sql & N & ",[返品前月在庫数] "
        sql = sql & N & ",[返品前月在庫金額] "
        sql = sql & N & ",[返品前月在庫銅量] "
        sql = sql & N & ",[返品当月出庫数] "
        sql = sql & N & ",[返品当月出庫金額] "
        sql = sql & N & ",[返品当月出庫銅量] "
        sql = sql & N & ",[仮出荷前月在庫数] "
        sql = sql & N & ",[仮出荷前月在庫金額] "
        sql = sql & N & ",[仮出荷前月在庫銅量] "
        sql = sql & N & ",[仮出荷当月出庫数] "
        sql = sql & N & ",[仮出荷当月出庫金額] "
        sql = sql & N & ",[仮出荷当月出庫銅量] "
        sql = sql & N & ",[製造コスト] "
        sql = sql & N & ",[輸送中前月在庫数] "
        sql = sql & N & ",[輸送中前月在庫金額] "
        sql = sql & N & ",[輸送中前月在庫銅量] "
        sql = sql & N & ",[輸送中当月出庫数] "
        sql = sql & N & ",[輸送中当月出庫金額] "
        sql = sql & N & ",[輸送中当月出庫銅量] "
        sql = sql & N & ",[作成日時] "
        sql = sql & N & ",[更新日時] "
        '-->2010.12.10 add by takagi
        sql = sql & N & ",M.[品名区分２] "
        '<--2010.12.10 add by takagi
        sql = sql & N & "  FROM 電線在庫マスタ "
        '-->2010.12.10 add by takagi
        sql = sql & N & " INNER JOIN " & StartUp.iniValue.LinkSvForHanbai & ".販売." & StartUp.iniValue.TableOwner & ".[T_品名区分マスタ（電線製品）] M "
        sql = sql & N & " ON 電線在庫マスタ.[品名CD品種] = M.品種コード"
        '<--2010.12.10 add by takagi
        sql = sql & N & "where 年月 = '" & lblKonkaiYM.Text.Replace("/", "") & "'"
        Dim ds As DataSet = _ukeharaiDb.selectDB(sql, RS, iRecCnt)     '電線在庫データ用コネクションへSelect発行

        'ORACLEへインサート
        Dim sqlBuf As System.Text.StringBuilder = New System.Text.StringBuilder
        Dim sqlBufF As System.Text.StringBuilder = New System.Text.StringBuilder
        sqlBufF.Append(N & "insert into ZG410B_W10 ")
        sqlBufF.Append(N & "( ")
        sqlBufF.Append(N & " NENTETSU ")                    '年月
        sqlBufF.Append(N & ",SHIYO ")                       '品名CD仕様
        sqlBufF.Append(N & ",HINSHU ")                      '品名CD品種
        sqlBufF.Append(N & ",SENSHIN ")                     '品名CD線心数
        sqlBufF.Append(N & ",SAIZU ")                       '品名CDサイズ
        sqlBufF.Append(N & ",IRO ")                         '品名CD色
        sqlBufF.Append(N & ",FUKA ")                        '品名CD設計付加記号
        sqlBufF.Append(N & ",KAITEI ")                      '品名CD設計改訂記号
        sqlBufF.Append(N & ",HINSHUMEI ")                   '品種名
        sqlBufF.Append(N & ",SAIZUMEI ")                    'サイズ名
        sqlBufF.Append(N & ",IROMEI ")                      '色名
        sqlBufF.Append(N & ",TANI ")                        '単位
        sqlBufF.Append(N & ",KAGI_ZEN_SU ")                 '鈎取前月在庫数
        sqlBufF.Append(N & ",KAGI_ZEN_KINGAKU ")            '鈎取前月在庫金額
        sqlBufF.Append(N & ",KAGI_ZEN_DOU ")                '鈎取前月在庫銅量
        sqlBufF.Append(N & ",KAGI_TOU_NYUKO_SU ")           '鈎取当月入庫数
        sqlBufF.Append(N & ",KAGI_TOU_NYUKO_KINGAKU ")      '鈎取当月入庫金額
        sqlBufF.Append(N & ",KAGI_TOU_NYUKO_DOU ")          '鈎取当月入庫銅量
        sqlBufF.Append(N & ",KAGI_TOU_SHUKO_SU ")           '鈎取当月出庫数
        sqlBufF.Append(N & ",KAGI_TOU_SHUKO_KINGAKU ")      '鈎取当月出庫金額
        sqlBufF.Append(N & ",KAGI_TOU_DHUKO_DOU ")          '鈎取当月出庫銅量
        sqlBufF.Append(N & ",FUNA_ZEN_SU ")                 '船岡前月在庫数
        sqlBufF.Append(N & ",FUNA_ZEN_KINGAKU ")            '船岡前月在庫金額
        sqlBufF.Append(N & ",FUNA_ZEN_DOU ")                '船岡前月在庫銅量
        sqlBufF.Append(N & ",FUNA_TOU_NYUKO_SU ")           '船岡当月入庫数
        sqlBufF.Append(N & ",FUNA_TOU_NYUKO_KINGAKU ")      '船岡当月入庫金額
        sqlBufF.Append(N & ",FUNA_TOU_NYUKO_DOU ")          '船岡当月入庫銅量
        sqlBufF.Append(N & ",FUNA_TOU_SHUKO_SU ")           '船岡当月出庫数
        sqlBufF.Append(N & ",FUNA_TOU_SHUKO_KINGAKU ")      '船岡当月出庫金額
        sqlBufF.Append(N & ",FUNA_TOU_SHUKO_DOU ")          '船岡当月出庫銅量
        sqlBufF.Append(N & ",SAPO_ZEN_SU ")                 '札幌前月在庫数
        sqlBufF.Append(N & ",SAPO_ZEN_KINGAKU ")            '札幌前月在庫金額
        sqlBufF.Append(N & ",SAPO_ZEN_DOU ")                '札幌前月在庫銅量
        sqlBufF.Append(N & ",SAPO_TOU_NYUKO_SU ")           '札幌当月入庫数
        sqlBufF.Append(N & ",SAPO_TOU_NYUKO_KINGAKU ")      '札幌当月入庫金額
        sqlBufF.Append(N & ",SAPO_TOU_NYUKO_DOU ")          '札幌当月入庫銅量
        sqlBufF.Append(N & ",SAPO_TOU_SHUKO_SU ")           '札幌当月出庫数
        sqlBufF.Append(N & ",SAPO_TOU_SHUKO_KINGAKU ")      '札幌当月出庫金額
        sqlBufF.Append(N & ",SAPO_TOU_SHUKO_DOU ")          '札幌当月出庫銅量
        sqlBufF.Append(N & ",HEN_ZEN_SU ")                  '返品前月在庫数
        sqlBufF.Append(N & ",HEN_ZEN_KINGAKU ")             '返品前月在庫金額
        sqlBufF.Append(N & ",HEN_ZEN_DOU ")                 '返品前月在庫銅量
        sqlBufF.Append(N & ",HEN_TOU_SHUKO_SU ")            '返品当月出庫数
        sqlBufF.Append(N & ",HEN_TOU_SHUKO_KINGAKU ")       '返品当月出庫金額
        sqlBufF.Append(N & ",HEN_TOU_SHUKO_DOU ")           '返品当月出庫銅量
        sqlBufF.Append(N & ",KARI_ZEN_SU ")                 '仮出荷前月在庫数
        sqlBufF.Append(N & ",KARI_ZEN_KINGAKU ")            '仮出荷前月在庫金額
        sqlBufF.Append(N & ",KARI_ZEN_DOU ")                '仮出荷前月在庫銅量
        sqlBufF.Append(N & ",KARI_TOU_SU ")                 '仮出荷当月出庫数
        sqlBufF.Append(N & ",KARI_TOU_KINGAKU ")            '仮出荷当月出庫金額
        sqlBufF.Append(N & ",KARI_TOU_DOU ")                '仮出荷当月出庫銅量
        sqlBufF.Append(N & ",COST ")                        '製造コスト
        sqlBufF.Append(N & ",YU_ZEN_SU ")                   '輸送中前月在庫数
        sqlBufF.Append(N & ",YU_ZEN_KINGAKU ")              '輸送中前月在庫金額
        sqlBufF.Append(N & ",YU_ZEN_DOU ")                  '輸送中前月在庫銅量
        sqlBufF.Append(N & ",YU_TOU_SU ")                   '輸送中当月出庫数
        sqlBufF.Append(N & ",YU_TOU_KNGAKU ")               '輸送中当月出庫金額
        sqlBufF.Append(N & ",YU_TOU_DOU ")                  '輸送中当月出庫銅量
        sqlBufF.Append(N & ",ORG_CREATEDT ")                'オリジナル作成日時
        sqlBufF.Append(N & ",ORG_UPDDT ")                   'オリジナル更新日時
        sqlBufF.Append(N & ",UPDNAME ")                     '端末ID
        sqlBufF.Append(N & ",UPDDATE ")                     '更新日時
        '-->2010.12.10 add by takagi
        sqlBufF.Append(N & ",HINKBN2")
        '<--2010.12.10 add by takagi
        sqlBufF.Append(N & ")values( ")
        With ds.Tables(RS)
            For i As Integer = 0 To iRecCnt - 1
                sqlBuf.Remove(0, sqlBuf.Length)
                sqlBuf.Append(N & "  " & convNullStr(.Rows(i)("年月")))                                  'NENTETSU
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("品名CD仕様")))                            'SHIYO
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("品名CD品種")))                            'HINSHU
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("品名CD線心数")))                          'SENSHIN
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("品名CDサイズ")))                          'SAIZU
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("品名CD色")))                              'IRO
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("品名CD設計付加記号")))                    'FUKA
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("品名CD設計改訂記号")))                    'KAITEI
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("品種名")))                                'HINSHUMEI
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("サイズ名")))                              'SAIZUMEI
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("色名")))                                  'IROMEI
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("単位")))                                  'TANI
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("鈎取前月在庫数")))                        'KAGI_ZEN_SU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("鈎取前月在庫金額")))                      'KAGI_ZEN_KINGAKU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("鈎取前月在庫銅量")))                      'KAGI_ZEN_DOU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("鈎取当月入庫数")))                        'KAGI_TOU_NYUKO_SU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("鈎取当月入庫金額")))                      'KAGI_TOU_NYUKO_KINGAKU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("鈎取当月入庫銅量")))                      'KAGI_TOU_NYUKO_DOU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("鈎取当月出庫数")))                        'KAGI_TOU_SHUKO_SU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("鈎取当月出庫金額")))                      'KAGI_TOU_SHUKO_KINGAKU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("鈎取当月出庫銅量")))                      'KAGI_TOU_DHUKO_DOU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("船岡前月在庫数")))                        'FUNA_ZEN_SU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("船岡前月在庫金額")))                      'FUNA_ZEN_KINGAKU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("船岡前月在庫銅量")))                      'FUNA_ZEN_DOU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("船岡当月入庫数")))                        'FUNA_TOU_NYUKO_SU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("船岡当月入庫金額")))                      'FUNA_TOU_NYUKO_KINGAKU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("船岡当月入庫銅量")))                      'FUNA_TOU_NYUKO_DOU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("船岡当月出庫数")))                        'FUNA_TOU_SHUKO_SU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("船岡当月出庫金額")))                      'FUNA_TOU_SHUKO_KINGAKU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("船岡当月出庫銅量")))                      'FUNA_TOU_SHUKO_DOU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("札幌前月在庫数")))                        'SAPO_ZEN_SU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("札幌前月在庫金額")))                      'SAPO_ZEN_KINGAKU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("札幌前月在庫銅量")))                      'SAPO_ZEN_DOU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("札幌当月入庫数")))                        'SAPO_TOU_NYUKO_SU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("札幌当月入庫金額")))                      'SAPO_TOU_NYUKO_KINGAKU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("札幌当月入庫銅量")))                      'SAPO_TOU_NYUKO_DOU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("札幌当月出庫数")))                        'SAPO_TOU_SHUKO_SU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("札幌当月出庫金額")))                      'SAPO_TOU_SHUKO_KINGAKU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("札幌当月出庫銅量")))                      'SAPO_TOU_SHUKO_DOU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("返品前月在庫数")))                        'HEN_ZEN_SU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("返品前月在庫金額")))                      'HEN_ZEN_KINGAKU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("返品前月在庫銅量")))                      'HEN_ZEN_DOU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("返品当月出庫数")))                        'HEN_TOU_SHUKO_SU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("返品当月出庫金額")))                      'HEN_TOU_SHUKO_KINGAKU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("返品当月出庫銅量")))                      'HEN_TOU_SHUKO_DOU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("仮出荷前月在庫数")))                      'KARI_ZEN_SU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("仮出荷前月在庫金額")))                    'KARI_ZEN_KINGAKU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("仮出荷前月在庫銅量")))                    'KARI_ZEN_DOU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("仮出荷当月出庫数")))                      'KARI_TOU_SU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("仮出荷当月出庫金額")))                    'KARI_TOU_KINGAKU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("仮出荷当月出庫銅量")))                    'KARI_TOU_DOU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("製造コスト")))                            'COST
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("輸送中前月在庫数")))                      'YU_ZEN_SU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("輸送中前月在庫金額")))                    'YU_ZEN_KINGAKU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("輸送中前月在庫銅量")))                    'YU_ZEN_DOU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("輸送中当月出庫数")))                      'YU_TOU_SU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("輸送中当月出庫金額")))                    'YU_TOU_KNGAKU
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("輸送中当月出庫銅量")))                    'YU_TOU_DOU
                sqlBuf.Append(N & ", to_date(" & convNullStr(_db.rmNullDate(.Rows(i)("作成日時"))) & ",'YYYY/MM/DD HH24:MI:SS') ") 'ORG_CREATEDT
                sqlBuf.Append(N & ", to_date(" & convNullStr(_db.rmNullDate(.Rows(i)("更新日時"))) & ",'YYYY/MM/DD HH24:MI:SS') ") 'ORG_UPDDT
                sqlBuf.Append(N & ", " & convNullStr(UtilClass.getComputerName()) & " ")                 'UPDNAME  
                sqlBuf.Append(N & ", " & sysdate & " ")                                                  'UPDDATE 
                '-->2010.12.10 add by takagi
                sqlBuf.Append(N & "," & convNullStr(.Rows(i)("品名区分２")))
                '<--2010.12.10 add by takagi
                sqlBuf.Append(N & ") ")
                _db.executeDB(sqlBufF.ToString & sqlBuf.ToString)

                prmRefPb.value += 1
                If prmRefPb.value Mod 10 = 0 Then
                    prmRefPb.status = "納品書データ取込中... (" & prmRefPb.value & "/" & prmRefPb.maxVal & ")"
                End If
            Next
        End With

    End Sub

    '-------------------------------------------------------------------------------
    '   文字型列用　NULL→"NULL"置換／NOT NULL→'そのまま'
    '   （処理概要）SQL発行用の文字列置換を行う
    '   ●入力パラメタ  ：フィールドデータ(NULL可)
    '   ●出力パラメタ  ：なし
    '   ●メソッド戻り値：変換後文字列
    '-------------------------------------------------------------------------------
    Private Function convNullStr(ByVal prmField As Object) As String
        If IsDBNull(prmField) Then
            Return "NULL"
        Else
            Return "'" & _db.rmSQ(CStr(prmField)) & "'"
        End If
    End Function

    '-------------------------------------------------------------------------------
    '   数値型列用　NULL→"NULL"置換／NOT NULL→そのまま
    '   （処理概要）SQL発行用の文字列置換を行う
    '   ●入力パラメタ  ：フィールドデータ(NULL可)
    '   ●出力パラメタ  ：なし
    '   ●メソッド戻り値：変換後文字列
    '-------------------------------------------------------------------------------
    Private Function convNullNUm(ByVal prmField As Object) As String
        If IsDBNull(prmField) Then
            Return "NULL"
        Else
            Return CStr(prmField)
        End If
    End Function
End Class