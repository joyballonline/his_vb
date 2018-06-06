'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）ABC分析実行指示
'    （フォームID）ZM410B_ABCBunseki
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   高木        2010/10/25                 新規              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Public Class ZM410B_ABCBunseki
    Inherits System.Windows.Forms.Form

    '-------------------------------------------------------------------------------
    '   定数定義
    '-------------------------------------------------------------------------------
    Private Const RS As String = "RecSet"                       'データセットテーブル名
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const PGID As String = "ZM410B"

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
        _updFlg = prmUpdFlg


    End Sub

    '-------------------------------------------------------------------------------
    '　フォームロードイベント
    '-------------------------------------------------------------------------------
    Private Sub ZM410B_ABCBunseki_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            '前回実行情報の表示
            Dim sql As String = ""
            Dim iRecCnt As Integer = 0
            sql = sql & N & "select *  "
            sql = sql & N & "from ( "
            sql = sql & N & "    select "
            sql = sql & N & "     ROW_NUMBER() OVER (ORDER BY RECORDID desc) RNUM "
            sql = sql & N & "    ,SDATEEND "
            sql = sql & N & "    ,KENNSU1 T10TARGET_CNT "
            sql = sql & N & "    ,KENNSU2 TARGET_ITEM_CNT "
            sql = sql & N & "    ,KENNSU3 A_CNT "
            sql = sql & N & "    ,KENNSU4 B_CNT "
            sql = sql & N & "    ,KENNSU5 C_CNT "
            sql = sql & N & "    ,KENNSU6 S_CNT "
            sql = sql & N & "    ,NAME1   YYYYMM_FROM "
            sql = sql & N & "    ,NAME2   YYYYMM_TO "
            sql = sql & N & "    from T91RIREKI "
            sql = sql & N & "    where PGID     = '" & PGID & "' "
            sql = sql & N & ") "
            sql = sql & N & "where RNUM = 1 "
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)
            If iRecCnt < 1 Then
                '履歴なし
                lblZenkaiJikkouDate.Text = ZC110M_Menu.NON_EXECUTE
                lblZenkaiJissekiFrom.Text = ""
                lblZenkaiJissekiTo.Text = ""
                lblSum.Text = ""
                lblS.Text = ""
                lblA.Text = ""
                lblB.Text = ""
                lblC.Text = ""
            Else
                '履歴あり
                lblZenkaiJikkouDate.Text = _db.rmNullDate(ds.Tables(RS).Rows(0)("SDATEEND"))
                Dim wk As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("YYYYMM_FROM"))
                lblZenkaiJissekiFrom.Text = wk.Substring(0, 4) & "/" & wk.Substring(4, 2)
                wk = _db.rmNullStr(ds.Tables(RS).Rows(0)("YYYYMM_TO"))
                lblZenkaiJissekiTo.Text = wk.Substring(0, 4) & "/" & wk.Substring(4, 2)
                lblSum.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("TARGET_ITEM_CNT")), "#,##0")
                lblS.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("S_CNT")), "#,##0")
                lblA.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("A_CNT")), "#,##0")
                lblB.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("B_CNT")), "#,##0")
                lblC.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("C_CNT")), "#,##0")
            End If

            '今回実行情報の表示
            sql = ""
            sql = sql & N & "SELECT "
            sql = sql & N & " DECODE(MAX(NENGETU),NULL,'', "
            sql = sql & N & "     TO_CHAR( "
            sql = sql & N & "         ADD_MONTHS( "
            sql = sql & N & "             TO_DATE( "
            sql = sql & N & "                 SUBSTR(MAX(NENGETU),1,4) || '/' || SUBSTR(MAX(NENGETU),5,2) || '/01' "
            sql = sql & N & "                ,'YYYY/MM/DD' "
            sql = sql & N & "                ) "
            sql = sql & N & "            ,-11 "
            sql = sql & N & "            ) "
            sql = sql & N & "        ,'YYYY/MM' "
            sql = sql & N & "        ) "
            sql = sql & N & "     )                       FROM_DT "
            sql = sql & N & ",DECODE(MAX(NENGETU),NULL,'', "
            sql = sql & N & "     TO_CHAR( "
            sql = sql & N & "         TO_DATE( "
            sql = sql & N & "             SUBSTR(MAX(NENGETU),1,4) || '/' || SUBSTR(MAX(NENGETU),5,2) || '/01' "
            sql = sql & N & "            ,'YYYY/MM/DD' "
            sql = sql & N & "            ) "
            sql = sql & N & "        ,'YYYY/MM' "
            sql = sql & N & "        ) "
            sql = sql & N & "     )                       TO_DT "
            sql = sql & N & "FROM T10HINHANJ "
            ds = _db.selectDB(sql, RS)
            dteKonkaiJissekiFrom.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("FROM_DT"))
            dteKonkaiJissekiTo.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("TO_DT"))

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
    '　テキストフォーカス取得イベント
    '------------------------------------------------------------------------------------------------------
    Private Sub txt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dteKonkaiJissekiFrom.GotFocus, dteKonkaiJissekiTo.GotFocus
        UtilClass.selAll(sender)
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　テキストキー押下イベント
    '------------------------------------------------------------------------------------------------------
    Private Sub dteKonkaiJissekiFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dteKonkaiJissekiFrom.KeyPress, dteKonkaiJissekiTo.KeyPress
        UtilClass.moveNextFocus(Me, e)
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　実行ボタンク押下イベント
    '------------------------------------------------------------------------------------------------------
    Private Sub btnJikkou_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJikkou.Click
        Try
            '入力チェック
            Call checkInit()                                                                    '実績参照期間


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

                    pb.status = "分析準備中" '0-0 WK初期化
                    _db.executeDB("delete from ZM410B_W10 where UPDNAME = '" & UtilClass.getComputerName() & "'")
                    _db.executeDB("delete from ZM410B_W11 where UPDNAME = '" & UtilClass.getComputerName() & "'")

                    Const CALC_VAL As String = "HJISSEKISU"

                    '1-0 対象品名抽出
                    pb.status = "対象品名抽出"
                    Dim sql As String = ""
                    sql = sql & N & "INSERT INTO ZM410B_W10 "
                    sql = sql & N & "( "
                    sql = sql & N & " HINMEICD "        '実品名コード
                    sql = sql & N & ",KHINMEICD "       '計画品名コード
                    sql = sql & N & ",NENGETU "         '年月
                    sql = sql & N & ",HJISSEKIRYOU "    '販売実績量
                    sql = sql & N & ",HJISSEKISU "      '販売実績数
                    sql = sql & N & ",UPDNAME "         '端末ID
                    sql = sql & N & ",UPDDATE "         '更新日時
                    sql = sql & N & ") "
                    sql = sql & N & "SELECT "
                    sql = sql & N & " T10.HINMEICD "                          '実品名コード
                    sql = sql & N & ",M12.KHINMEICD "                         '計画品名コード
                    sql = sql & N & ",T10.NENGETU "                           '年月
                    sql = sql & N & ",T10.HJISSEKIRYOU "                      '販売実績量
                    sql = sql & N & ",T10.HJISSEKISU "                        '販売実績数
                    sql = sql & N & ",'" & UtilClass.getComputerName() & "' " '端末ID
                    sql = sql & N & ",SYSDATE "                               '更新日時
                    sql = sql & N & "FROM T10HINHANJ       T10  "
                    sql = sql & N & "INNER JOIN M12SYUYAKU M12 "
                    sql = sql & N & " ON T10.HINMEICD = M12.HINMEICD "
                    sql = sql & N & "INNER JOIN (SELECT * FROM M11KEIKAKUHIN WHERE (TT_JUYOUCD IS NULL OR TT_JUYOUCD != '1') AND TT_SYUBETU = 1) M11 " '需要先CD≠1かつ在庫・繰返＝1
                    sql = sql & N & " ON M12.KHINMEICD = M11.TT_KHINMEICD "
                    sql = sql & N & "WHERE NENGETU >= '" & dteKonkaiJissekiFrom.Text.Replace("/", "") & "'"
                    sql = sql & N & "  AND NENGETU <= '" & dteKonkaiJissekiTo.Text.Replace("/", "") & "'"
                    _db.executeDB(sql)

                    '2-1 総販売数編集
                    pb.status = "総販売数編集"
                    sql = ""
                    sql = sql & N & "INSERT INTO ZM410B_W11 "
                    sql = sql & N & "( "
                    sql = sql & N & " KHINMEICD "
                    sql = sql & N & ",HJISSEKIRYOU "
                    sql = sql & N & ",HJISSEKISU "
                    sql = sql & N & ",UPDNAME "
                    sql = sql & N & ",UPDDATE "
                    sql = sql & N & ")SELECT "
                    sql = sql & N & " KHINMEICD "
                    sql = sql & N & ",SUM(HJISSEKIRYOU) "
                    sql = sql & N & ",SUM(HJISSEKISU) "
                    sql = sql & N & ",'" & UtilClass.getComputerName() & "' "
                    sql = sql & N & ",SYSDATE "
                    sql = sql & N & "FROM ZM410B_W10 "
                    sql = sql & N & "GROUP BY KHINMEICD "
                    _db.executeDB(sql)

                    '2-2 構成比率算出
                    pb.status = "構成比率算出"
                    sql = ""
                    sql = sql & N & "UPDATE ZM410B_W11 "
                    sql = sql & N & "SET CONSISTSRATE = "
                    sql = sql & N & "( "
                    sql = sql & N & "SELECT ROUND((" & CALC_VAL & "/(SELECT SUM(" & CALC_VAL & ") FROM ZM410B_W11 WHERE UPDNAME='" & UtilClass.getComputerName() & "'))*100,5) "
                    sql = sql & N & "FROM ZM410B_W11 SUB  "
                    sql = sql & N & "WHERE ZM410B_W11.KHINMEICD = SUB.KHINMEICD "
                    sql = sql & N & "AND   SUB.UPDNAME='" & UtilClass.getComputerName() & "' "
                    sql = sql & N & ") "
                    sql = sql & N & "WHERE UPDNAME='" & UtilClass.getComputerName() & "' "
                    _db.executeDB(sql)

                    '2-3 累積構成率積上
                    pb.status = "累積構成率積上げ"
                    sql = ""
                    sql = sql & N & "SELECT "
                    sql = sql & N & " ROWID "
                    sql = sql & N & ",HJISSEKISU "
                    sql = sql & N & ",CONSISTSRATE "
                    sql = sql & N & "FROM ZM410B_W11 "
                    sql = sql & N & "WHERE UPDNAME = '" & UtilClass.getComputerName() & "' "
                    sql = sql & N & "ORDER BY CONSISTSRATE DESC ," & CALC_VAL & " DESC "
                    Dim ds As DataSet = _db.selectDB(sql, RS, pb.maxVal)
                    Dim sumValue As Decimal = 0.0
                    Dim sumRate As Decimal = 0.0
                    For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1
                        sumValue += _db.rmNullDouble(ds.Tables(RS).Rows(i)("HJISSEKISU"))
                        sumRate += _db.rmNullDouble(ds.Tables(RS).Rows(i)("CONSISTSRATE"))
                        sql = ""
                        sql = sql & N & "UPDATE ZM410B_W11"
                        sql = sql & N & "SET  RANK        = " & i + 1 & " "
                        sql = sql & N & "    ,AMOUNTVALUE = " & sumValue & " "
                        sql = sql & N & "    ,AMOUNTRATE  = " & sumRate & " "
                        sql = sql & N & "WHERE ROWID = '" & ds.Tables(RS).Rows(i)("ROWID") & "'"
                        _db.executeDB(sql)
                        pb.value = i + 1
                    Next

                    '3-0 ABC区分設定
                    pb.status = "ABC区分設定"
                    pb.value = 0
                    sql = ""
                    sql = sql & N & "UPDATE ZM410B_W11"
                    sql = sql & N & "SET ABC = "
                    sql = sql & N & " CASE WHEN AMOUNTRATE < (SELECT NUM1 FROM M01HANYO WHERE KOTEIKEY = '13' AND KAHENKEY = 'A') THEN 'A' "
                    sql = sql & N & "      WHEN AMOUNTRATE < (SELECT NUM1 FROM M01HANYO WHERE KOTEIKEY = '13' AND KAHENKEY = 'B') THEN 'B' "
                    sql = sql & N & "                                                                                             ELSE 'C' END "
                    sql = sql & N & "WHERE UPDNAME = '" & UtilClass.getComputerName() & "' "
                    _db.executeDB(sql)

                    _db.beginTran()
                    Try
                        '4-0 ABC区分更新
                        pb.status = "ABC区分更新"
                        pb.maxVal = 3
                        sql = ""
                        sql = sql & N & "UPDATE M11KEIKAKUHIN "
                        sql = sql & N & "SET TT_UPDNAME = '" & UtilClass.getComputerName() & "' "
                        sql = sql & N & "  , TT_DATE    = SYSDATE "
                        sql = sql & N & "  , TT_ABCKBN  =  "
                        sql = sql & N & "	( "
                        sql = sql & N & "	SELECT WK.ABC "
                        sql = sql & N & "	FROM (SELECT ABC,KHINMEICD FROM ZM410B_W11 WHERE  UPDNAME = '" & UtilClass.getComputerName() & "') WK "
                        sql = sql & N & "	WHERE M11KEIKAKUHIN.TT_KHINMEICD = WK.KHINMEICD "
                        sql = sql & N & "	) "
                        _db.executeDB(sql)
                        pb.value += 1
                        sql = ""
                        sql = sql & N & "UPDATE M11KEIKAKUHIN "
                        sql = sql & N & "SET TT_ABCKBN = 'C' "
                        sql = sql & N & "  , TT_UPDNAME = '" & UtilClass.getComputerName() & "' "
                        sql = sql & N & "  , TT_DATE    = SYSDATE "
                        sql = sql & N & "WHERE TT_ABCKBN IS NULL "
                        _db.executeDB(sql)
                        pb.value += 1
                        sql = ""
                        sql = sql & N & "UPDATE M11KEIKAKUHIN "
                        sql = sql & N & "SET TT_ABCKBN = 'S' "
                        sql = sql & N & "  , TT_UPDNAME = '" & UtilClass.getComputerName() & "' "
                        sql = sql & N & "  , TT_DATE    = SYSDATE "
                        sql = sql & N & "WHERE TT_JUYOUCD = '1' "
                        _db.executeDB(sql)
                        pb.value += 1


                        pb.status = "ステータス変更中・・・"
                        pb.value = 0
                        ed = Now                    '処理終了日時
                        _parentForm.updateSeigyoTbl(PGID, True, st, ed)                             '5-0 生産量確定/解除

                        pb.status = "実行履歴作成"
                        '5-1
                        sql = ""
                        sql = sql & N & "SELECT "
                        sql = sql & N & " SUM(ITEMCNT)   SUM_ITEMCNT "
                        sql = sql & N & ",SUM(A_CNT)     SUM_A_CNT "
                        sql = sql & N & ",SUM(B_CNT)     SUM_B_CNT "
                        sql = sql & N & ",SUM(C_CNT)     SUM_C_CNT "
                        sql = sql & N & ",SUM(S_CNT)     SUM_S_CNT  "
                        sql = sql & N & ",SUM(TARGETCNT) SUM_TARGETCNT "
                        sql = sql & N & "FROM "
                        sql = sql & N & "( "
                        sql = sql & N & "SELECT COUNT(*) ITEMCNT,0 A_CNT,0 B_CNT,0 C_CNT,0 S_CNT,0 TARGETCNT FROM M11KEIKAKUHIN  "
                        sql = sql & N & "UNION ALL "
                        sql = sql & N & "SELECT 0 ITEMCNT,COUNT(*) A_CNT,0 B_CNT,0 C_CNT,0 S_CNT,0 TARGETCNT FROM M11KEIKAKUHIN WHERE TT_ABCKBN = 'A' "
                        sql = sql & N & "UNION ALL "
                        sql = sql & N & "SELECT 0 ITEMCNT,0 A_CNT,COUNT(*) B_CNT,0 C_CNT,0 S_CNT,0 TARGETCNT FROM M11KEIKAKUHIN WHERE TT_ABCKBN = 'B' "
                        sql = sql & N & "UNION ALL "
                        sql = sql & N & "SELECT 0 ITEMCNT,0 A_CNT,0 B_CNT,COUNT(*) C_CNT,0 S_CNT,0 TARGETCNT FROM M11KEIKAKUHIN WHERE TT_ABCKBN = 'C' "
                        sql = sql & N & "UNION ALL "
                        sql = sql & N & "SELECT 0 ITEMCNT,0 A_CNT,0 B_CNT,0 C_CNT,COUNT(*) S_CNT,0 TARGETCNT FROM M11KEIKAKUHIN WHERE TT_ABCKBN = 'S' "
                        sql = sql & N & "UNION ALL "
                        sql = sql & N & "SELECT 0 ITEMCNT,0 A_CNT,0 B_CNT,0 C_CNT,0 S_CNT,COUNT(*) TARGETCNT FROM ZM410B_W10 WHERE UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
                        sql = sql & N & ") "
                        With _db.selectDB(sql, RS).Tables(RS)
                            insertRireki(_db.rmNullInt(.Rows(0)("SUM_TARGETCNT")), _
                                         _db.rmNullInt(.Rows(0)("SUM_ITEMCNT")), _
                                         _db.rmNullInt(.Rows(0)("SUM_A_CNT")), _
                                         _db.rmNullInt(.Rows(0)("SUM_B_CNT")), _
                                         _db.rmNullInt(.Rows(0)("SUM_C_CNT")), _
                                         _db.rmNullInt(.Rows(0)("SUM_S_CNT")), _
                                         st, ed)                                 '5-2 実行履歴格納
                        End With


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
            Call initForm()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   入力チェック
    '   （処理概要）実績参照期間の必須/大小/過去日チェックを行う
    '   ●入力パラメタ  ：なし
    '   ●出力パラメタ  ：なし
    '   ●メソッド戻り値：なし
    '-------------------------------------------------------------------------------
    Private Sub checkInit()
        '入力チェック
        If "/".Equals(dteKonkaiJissekiFrom.Text.Trim) Then                                                 '未入力
            Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput"), dteKonkaiJissekiFrom)
        ElseIf "/".Equals(dteKonkaiJissekiTo.Text.Trim) Then                                           '未入力
            Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput"), dteKonkaiJissekiTo)
        ElseIf CDate(dteKonkaiJissekiFrom.Text & "/01") >= CDate(dteKonkaiJissekiTo.Text & "/01") Then
            Throw New UsrDefException("大小関係が不正です。", _msgHd.getMSG("ErrDaiSyoChk"), dteKonkaiJissekiTo)
        ElseIf Format(CDate(dteKonkaiJissekiFrom.Text & "/01"), "yyyy/MM") >= Format(Now, "yyyy/MM") Then
            Throw New UsrDefException("将来の日付は入力できません。", _msgHd.getMSG("FutureDay"), dteKonkaiJissekiFrom)
        ElseIf Format(CDate(dteKonkaiJissekiTo.Text & "/01"), "yyyy/MM") >= Format(Now, "yyyy/MM") Then
            Throw New UsrDefException("将来の日付は入力できません。", _msgHd.getMSG("FutureDay"), dteKonkaiJissekiTo)
        End If
    End Sub

    '-------------------------------------------------------------------------------
    '   実行履歴作成
    '   （処理概要）確定処理用の実行履歴を作成する
    '   ●入力パラメタ  ：prmCntTarget  T10対象件数
    '                     prmCntItem    対象品名数(Item数)
    '                     prmCntA       A区分件数
    '                     prmCntB       B区分件数
    '                     prmCntC       C区分件数
    '                     prmCntS       S区分件数
    '                     prmStDt       処理開始日時
    '                     prmEdDt       処理終了日時
    '   ●出力パラメタ  ：なし
    '   ●メソッド戻り値：なし
    '-------------------------------------------------------------------------------
    Private Sub insertRireki(ByVal prmCntTarget As Integer, _
                             ByVal prmCntItem As Integer, _
                             ByVal prmCntA As Integer, _
                             ByVal prmCntB As Integer, _
                             ByVal prmCntC As Integer, _
                             ByVal prmCntS As Integer, ByVal prmStDt As Date, ByVal prmEdDt As Date)

        Dim d As DataSet = _db.selectDB("select SNENGETU,KNENGETU from T01KEIKANRI", RS)
        Dim syoriDate As String = _db.rmNullStr(d.Tables(RS).Rows(0)("SNENGETU"))
        Dim keikakuDate As String = _db.rmNullStr(d.Tables(RS).Rows(0)("KNENGETU"))

        Dim sql As String = ""
        sql = sql & N & "insert into T91RIREKI "
        sql = sql & N & "( "
        sql = sql & N & " SNENGETU "   '処理年月
        sql = sql & N & ",KNENGETU "   '計画年月
        sql = sql & N & ",PGID "       '機能ID
        sql = sql & N & ",SDATESTART " '処理開始日時
        sql = sql & N & ",SDATEEND "   '処理終了日時
        sql = sql & N & ",KENNSU1 "    'T10対象件数
        sql = sql & N & ",KENNSU2 "    '対象品名数(Item数)
        sql = sql & N & ",KENNSU3 "    'A区分件数
        sql = sql & N & ",KENNSU4 "    'B区分件数
        sql = sql & N & ",KENNSU5 "    'C区分件数
        sql = sql & N & ",KENNSU6 "    'S区分件数
        sql = sql & N & ",NAME1 "      '実績参照期間FROM
        sql = sql & N & ",NAME2 "      '実績参照期間TO
        sql = sql & N & ",UPDNAME "    '最終更新者
        sql = sql & N & ",UPDDATE "    '最終更新日
        sql = sql & N & ")values( "
        sql = sql & N & "  '" & _db.rmSQ(syoriDate) & "' "                                                      '処理年月
        sql = sql & N & ", '" & _db.rmSQ(keikakuDate) & "' "                                                    '計画年月
        sql = sql & N & ", '" & _db.rmSQ(PGID) & "' "                                                           '機能ID
        sql = sql & N & ",to_date('" & Format(prmStDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '処理開始日時
        sql = sql & N & ",to_date('" & Format(prmEdDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '処理終了日時
        sql = sql & N & "," & prmCntTarget & " "                                                                'T10対象件数
        sql = sql & N & "," & prmCntItem & " "                                                                  '対象品名数(Item数)
        sql = sql & N & "," & prmCntA & " "                                                                     'A区分件数
        sql = sql & N & "," & prmCntB & " "                                                                     'B区分件数
        sql = sql & N & "," & prmCntC & " "                                                                     'C区分件数
        sql = sql & N & "," & prmCntS & " "                                                                     'S区分件数
        sql = sql & N & ",'" & dteKonkaiJissekiFrom.Text.Replace("/", "") & "' "                                '実績参照期間FROM
        sql = sql & N & ",'" & dteKonkaiJissekiTo.Text.Replace("/", "") & "' "                                  '実績参照期間TO
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "                                     '最終更新者
        sql = sql & N & ",sysdate "                                                                             '最終更新日
        sql = sql & N & ") "
        _db.executeDB(sql)

    End Sub

End Class