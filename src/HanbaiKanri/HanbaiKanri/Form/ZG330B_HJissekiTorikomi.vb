'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）販売実績取込指示
'    （フォームID）ZG330B_HJissekiTorikomi
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   高木        2010/10/19                 新規              
'　(2)   菅野        2014/06/04                 変更　材料票マスタ（MPESEKKEI）テーブル変更対応            
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Public Class ZG330B_HJissekiTorikomi
    Inherits System.Windows.Forms.Form

    '-------------------------------------------------------------------------------
    '   定数定義
    '-------------------------------------------------------------------------------
    Private Const RS As String = "RecSet"                       'データセットテーブル名
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const PGID As String = "ZG330B"
    Private Const IMP_LOG_NM As String = "販売実績取込処理出力情報.txt"

    '-------------------------------------------------------------------------------
    '   変数定義
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu
    Private _updFlg As Boolean = False  '更新可否
    Private _nouhinshoDb As UtilDBIf

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

        '納品書データ用コネクションの生成
        Dim iniWk As StartUp.iniType = StartUp.iniValue
        _nouhinshoDb = New UtilOleDBDebugger(iniWk.UdlFilePath_Nouhinsho, iniWk.LogFilePath, StartUp.DebugMode)

    End Sub

    '-------------------------------------------------------------------------------
    '　フォームクローズイベント
    '-------------------------------------------------------------------------------
    Private Sub ZG330B_HJissekiTorikomi_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            '納品書データ用コネクションの破棄
            _nouhinshoDb.close()
        Catch ex As Exception
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　フォームロードイベント
    '-------------------------------------------------------------------------------
    Private Sub ZG330B_HJissekiTorikomi_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            Dim targetYYYY As String = lblKonkaiYM.Text.Substring(0, 4)
            If 1 <= CInt(lblKonkaiYM.Text.Substring(5, 2)) And CInt(lblKonkaiYM.Text.Substring(5, 2)) <= 3 Then
                targetYYYY = CInt(targetYYYY) - 1
            End If
            sql = ""
            sql = sql & N & "select count(*) CNT from T_納品書データ_" & targetYYYY & "累計 "
            sql = sql & N & "where  substring(convert(varchar,[出荷日]),1,6)='" & lblKonkaiYM.Text.Replace("/", "") & "'"
            lblKonkaiKensu.Text = Format(_db.rmNullInt(_nouhinshoDb.selectDB(sql, RS).Tables(RS).Rows(0)("CNT")), "#,##0")

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

            Dim cntNullMetsuke As Integer = 0

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
                    _db.executeDB("delete from ZG330B_W10 where UPDNAME = '" & UtilClass.getComputerName() & "'")
                    pb.value += 1                                                               '0-0 WK初期化


                    pb.status = "納品書データ取得中"
                    pb.value = 0 : pb.maxVal = CInt(lblKonkaiKensu.Text)
                    Call importNohinsho(pb)                                                     '1-1 実績転送

                    _db.beginTran()
                    Try
                        pb.status = "該当年月データ削除中・・・"
                        _db.executeDB("delete from T10HINHANJ where NENGETU = '" & lblKonkaiYM.Text.Replace("/", "") & "'")
                        _db.executeDB("delete from T71HANBAIS where SUBSTR(TO_CHAR(SYUKKABI),1,6) = '" & lblKonkaiYM.Text.Replace("/", "") & "'")

                        pb.status = "販売実績データ構築中・・・"
                        Call insertHanbaiJisseki(cntNullMetsuke)                                    '1-2 販売実績取込
                        pb.status = "販売実績照会データ構築中・・・"
                        Call insertHanbaiJissekiDB()                                                '1-3 販売実績取込

                        pb.status = "ステータス変更中・・・"
                        ed = Now                    '処理終了日時
                        _parentForm.updateSeigyoTbl(PGID, True, st, ed)                             '2-0 生産量確定/解除

                        pb.status = "実行履歴作成"
                        insertRireki(st, ed)                                                        '2-1 実行履歴格納
                    Catch ex As Exception
                        _db.rollbackTran()
                        Throw ex
                    Finally
                        If _db.isTransactionOpen Then _db.commitTran()
                    End Try


                    '★★★2011.01.19 del by takagi
                    ''-->2011.01.16 add by takagi #未取込実績再取得
                    ''-->2011.01.18 del by takagi #82
                    ''_db.executeDB("delete from ZG330B_W10 where UPDNAME = '" & UtilClass.getComputerName() & "'")
                    ''<--2011.01.18 del by takagi #82

                    'pb.status = "新規追加計画対象品の過去納品書データ取得中"
                    'pb.value = 0
                    'Dim wkKey As String = getNonImportHinmei()
                    'If Not "".Equals(wkKey) Then
                    '    '-->2011.01.18 add by takagi #82
                    '    _db.executeDB("delete from ZG330B_W10 where UPDNAME = '" & UtilClass.getComputerName() & "'")
                    '    '<--2011.01.18 add by takagi #82
                    '    Call importNohinsho(pb, wkKey)                                                     '1-1 実績転送
                    '    _db.beginTran()
                    '    Try

                    '        pb.status = "過去分販売実績データ構築中・・・"
                    '        Dim wkCnt As Integer
                    '        '-->2011.01.18 chg by takagi #82
                    '        'Call insertHanbaiJisseki(wkCnt)                                    '1-2 販売実績取込
                    '        Call insertHanbaiJisseki(wkCnt, True)                                    '1-2 販売実績取込
                    '        cntNullMetsuke = cntNullMetsuke + wkCnt
                    '        '<--2011.01.18 chg by takagi #82
                    '        pb.status = "過去分販売実績照会データ構築中・・・"
                    '        Call insertHanbaiJissekiDB()                                                '1-3 販売実績取込

                    '    Catch ex As Exception
                    '        _db.rollbackTran()
                    '        Throw ex
                    '    Finally
                    '        If _db.isTransactionOpen Then _db.commitTran()
                    '    End Try
                    'End If
                    ''<--2011,01.16 add by takagi #未取込実績再取得
                    ''★★★2011.01.19 del by takagi

                Finally
                    pb.Close()                                                                  'プログレスバー画面消去
                End Try

            Finally
                Me.Cursor = cur
            End Try

            '終了MSG
            Dim optionMsg As String = ""
            '-->2010.12.02 add by takagi 
            Dim wk As String = ""
            Dim outFilePath As String = ""
            Call UtilClass.dividePathAndFile(StartUp.iniValue.LogFilePath, outFilePath, wk)
            outFilePath = outFilePath & "\" & IMP_LOG_NM
            '<--2010.12.02 add by takagi
            If cntNullMetsuke > 0 Then
                optionMsg = "-----------------------------------------------------------------" & N & _
                            "目付の取得が行えないデータが" & cntNullMetsuke & "件存在しました。" & N & _
                            "詳細な品名コードはログを確認してください。"

                '-->2010.12.02 add by takagi 
                optionMsg = optionMsg & N & N & outFilePath
                '<--2010.12.02 add by takagi
            End If
            Call _msgHd.dspMSG("completeRun", optionMsg)
            If cntNullMetsuke > 0 Then
                'ログ表示
                Try
                    '-->2010.12.02 add by takagi 
                    'System.Diagnostics.Process.Start(UtilClass.getAppPath(StartUp.assembly) & "\" & IMP_LOG_NM)   '関連付いたアプリで起動
                    System.Diagnostics.Process.Start(outFilePath)   '関連付いたアプリで起動
                    '<--2010.12.02 add by takagi
                Catch ex As Exception
                End Try
            End If
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
        sql = sql & N & ", " & CInt(lblKonkaiKensu.Text) & " "                                                  '実行件数
        sql = sql & N & ", '" & lblKonkaiYM.Text.Replace("/", "") & "' "                                        '対象年月
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "                                     '最終更新者
        sql = sql & N & ",sysdate "                                                                             '最終更新日
        sql = sql & N & ") "
        _db.executeDB(sql)

    End Sub

    '-------------------------------------------------------------------------------
    '   販売実績作成
    '   （処理概要）ワークより販売実績へデータ投入する
    '   ●入力パラメタ  ：なし
    '   ●出力パラメタ  ：目付取得不良件数
    '   ●メソッド戻り値：なし
    '-------------------------------------------------------------------------------
    '-->2011.01.18 chg by takagi #82
    'Private Sub insertHanbaiJisseki(ByRef prmRefNullMetsukeCnt As Integer)
    Private Sub insertHanbaiJisseki(ByRef prmRefNullMetsukeCnt As Integer, Optional ByVal prmAppendFlg As Boolean = False)
        '<--2011.01.18 chg by takagi #82

        Dim sql As String = ""
        sql = sql & N & "INSERT INTO T10HINHANJ "
        sql = sql & N & "( "
        sql = sql & N & " HINMEICD "        '実品名コード
        sql = sql & N & ",NENGETU "         '年月
        sql = sql & N & ",HJISSEKIRYOU "    '販売実績量
        sql = sql & N & ",HJISSEKISU "      '販売実績数
        sql = sql & N & ",METSUKE "         '目付
        sql = sql & N & ",UPDNAME "         '端末ID
        sql = sql & N & ",UPDDATE "         '更新日時
        sql = sql & N & ") "
        sql = sql & N & "SELECT "
        sql = sql & N & " SUB.HINCD "       '実品名コード
        sql = sql & N & ",SUB.YM "          '年月
        sql = sql & N & ",SUB.SUMD "        '販売実績量
        sql = sql & N & ",SUB.SUMS "        '販売実績数
        sql = sql & N & ",M.METSUKE "       '目付
        sql = sql & N & ",SUB.PCID "        '端末ID
        sql = sql & N & ",SUB.DT "          '更新日時
        sql = sql & N & "FROM  "
        sql = sql & N & "    ( "
        sql = sql & N & "    SELECT  "
        sql = sql & N & "        RPAD(SIYO    ,2,' ') "
        sql = sql & N & "     || LPAD(HINSYU  ,3,'0') "
        sql = sql & N & "     || LPAD(SENSHIN ,3,'0') "
        sql = sql & N & "     || LPAD(SIZECD  ,2,'0') "
        sql = sql & N & "     || LPAD(IRO     ,3,'0')                      HINCD "    '実品名コード
        '-->2011.01.16 chg by takagi #未取込実績再取得
        'sql = sql & N & "    ,'" & lblKonkaiYM.Text.Replace("/", "") & "'  YM    "    '年月
        sql = sql & N & "    ,substr(SYUKKABI,1,6)  YM    "    '年月
        '-->2011.01.16 chg by takagi #未取込実績再取得
        sql = sql & N & "    ,SUM(DOURYOU)                                 SUMD  "    '販売実績量
        sql = sql & N & "    ,SUM(SYUKANUM)                                SUMS  "    '販売実績数
        sql = sql & N & "    ,'" & UtilClass.getComputerName() & "'        PCID  "    '端末ID
        sql = sql & N & "    ,SYSDATE                                      DT    "    '更新日時
        sql = sql & N & "    FROM ZG330B_W10  "                  '販売実績WK
        sql = sql & N & "    WHERE  UPDNAME = '" & UtilClass.getComputerName() & "' "
        sql = sql & N & "      AND (TO_CHAR(TORIHIKIKBN) LIKE '11%' or TO_CHAR(TORIHIKIKBN) LIKE '12%')  "  '取引データ区分
        sql = sql & N & "      AND SYOHINKBN = 1  "                                                         '商品区分
        sql = sql & N & "      AND TYOUBOKBN = 1  "                                                         '帳簿区分
        sql = sql & N & "      AND (HINMEIKBN2 LIKE '01%' or HINMEIKBN2 LIKE '02%') "                       '品名区分2
        sql = sql & N & "    GROUP BY    RPAD(SIYO    ,2,' ') "                       '実品名コード
        sql = sql & N & "             || LPAD(HINSYU  ,3,'0') "
        sql = sql & N & "             || LPAD(SENSHIN ,3,'0') "
        sql = sql & N & "             || LPAD(SIZECD  ,2,'0') "
        sql = sql & N & "             || LPAD(IRO     ,3,'0') "
        '-->2011.01.16 add by takagi #未取込実績再取得
        sql = sql & N & "            ,substr(SYUKKABI,1,6) "    '年月
        '-->2011.01.16 add by takagi #未取込実績再取得
        sql = sql & N & "    ) SUB "
        '★★★2011.01.19 del by takagi
        ''sql = sql & N & "INNER JOIN M12SYUYAKU M12"
        ''sql = sql & N & " ON   SUB.HINCD = M12.HINMEICD "
        ''sql = sql & N & "INNER JOIN M11KEIKAKUHIN M11"
        ''sql = sql & N & " ON   M12.KHINMEICD = M11.TT_KHINMEICD "
        ''sql = sql & N & "  AND M11.TT_SYUBETU = 1 " '（1：在庫）
        '★★★2011.01.19 del by takagi
        '2014/06/04 UPD-S Sugano
        'sql = sql & N & "LEFT JOIN "
        'sql = sql & N & "    ( "
        'sql = sql & N & "    SELECT "
        'sql = sql & N & "     (  SHIYO                         "
        'sql = sql & N & "     || LPAD(TO_CHAR(HINSYU)  ,3,'0') "
        'sql = sql & N & "     || LPAD(TO_CHAR(SENSHIN) ,3,'0') "
        'sql = sql & N & "     || LPAD(TO_CHAR(SAIZU)   ,2,'0') "
        'sql = sql & N & "     || LPAD(TO_CHAR(IRO)     ,3,'0'))            HINCD   "  '実品名コード
        'sql = sql & N & "    ,SEISAN_KANSAN                                METSUKE "  '目付
        'sql = sql & N & "    FROM MPESEKKEI "                    '材料票マスタ
        'sql = sql & N & "    WHERE SEKKEI_HUKA = 'A' "
        'sql = sql & N & "      AND SEQ_NO      = 1 "
        'sql = sql & N & "    ) M "
        sql = sql & N & " LEFT JOIN (SELECT "
        sql = sql & N & "             (M1.SHIYO || M1.HINSYU || M1.SENSHIN || M1.SAIZU || M1.IRO) HINCD "
        sql = sql & N & "             ,M1.SEISAN_KANSAN METSUKE "
        sql = sql & N & "             FROM MPESEKKEI1 M1 "
        sql = sql & N & "             INNER JOIN (SELECT "
        sql = sql & N & "                               SHIYO,HINSYU,SENSHIN,SAIZU,IRO,SEKKEI_FUKA,MAX(SEKKEI_KAITEI) KAITEI"
        sql = sql & N & "                         FROM  MPESEKKEI1 "
        sql = sql & N & "                         WHERE SEKKEI_FUKA = 'A' "
        sql = sql & N & "                         GROUP BY SHIYO,HINSYU,SENSHIN,SAIZU,IRO,SEKKEI_FUKA) M2 "
        sql = sql & N & "             ON  M1.SHIYO = M2.SHIYO "
        sql = sql & N & "             AND M1.HINSYU = M2.HINSYU "
        sql = sql & N & "             AND M1.SENSHIN = M2.SENSHIN "
        sql = sql & N & "             AND M1.SAIZU = M2.SAIZU "
        sql = sql & N & "             AND M1.IRO = M2.IRO "
        sql = sql & N & "             AND M1.SEKKEI_FUKA = M2.SEKKEI_FUKA "
        sql = sql & N & "             AND M1.SEKKEI_KAITEI = M2.KAITEI ) M"
        '2014/06/04 UPD-E Sugano
        sql = sql & N & " ON SUB.HINCD = M.HINCD "
        sql = sql & N & "ORDER BY SUB.HINCD "
        Try
            _db.executeDB(sql)
        Catch ex As Exception
            If InStr(ex.Message, "ORA-00001") > 0 Then
                '一意制約違反
                Throw New UsrDefException("材料票マスタ(MPESEKKEI)に重複する品名コードが登録されています。継続処理できないため、実行を中止します。", _msgHd.getMSG("NonUniqueMPESEKKEI", "重複を解消し、再度処理を実行してください。"))
            End If
            Throw ex
        End Try

        sql = ""
        '★★★2011.01.19 chg by takagi
        ''sql = sql & N & "SELECT HINMEICD  "
        ''sql = sql & N & "FROM T10HINHANJ "
        ''sql = sql & N & "WHERE NENGETU = '" & lblKonkaiYM.Text.Replace("/", "") & "' "
        ''sql = sql & N & "  AND METSUKE IS NULL "
        ''sql = sql & N & "ORDER BY HINMEICD "
        sql = sql & N & "SELECT T10.HINMEICD  "
        sql = sql & N & "FROM T10HINHANJ T10 "
        sql = sql & N & "INNER JOIN M12SYUYAKU M12"
        sql = sql & N & " ON   T10.HINMEICD = M12.HINMEICD "
        sql = sql & N & "INNER JOIN M11KEIKAKUHIN M11"
        sql = sql & N & " ON   M12.KHINMEICD = M11.TT_KHINMEICD "
        sql = sql & N & "  AND M11.TT_SYUBETU = 1 " '（1：在庫）
        sql = sql & N & "WHERE T10.NENGETU = '" & lblKonkaiYM.Text.Replace("/", "") & "' "
        sql = sql & N & "  AND T10.METSUKE IS NULL "
        sql = sql & N & "ORDER BY T10.HINMEICD "
        '★★★2011.01.19 chg by takagi
        Dim ds As DataSet = _db.selectDB(sql, RS, prmRefNullMetsukeCnt)
        If prmRefNullMetsukeCnt > 0 Then

            '-->2010.12.02 add by takagi 
            Dim wk As String = ""
            Dim outFilePath As String = ""
            Call UtilClass.dividePathAndFile(StartUp.iniValue.LogFilePath, outFilePath, wk)
            outFilePath = outFilePath & "\" & IMP_LOG_NM
            '<--2010.12.02 add by takagi

            Dim logBuf As System.Text.StringBuilder = New System.Text.StringBuilder
            logBuf.Append(Format(Now(), "yyyy/MM/dd HH:mm:ss") & "実行" & N)
            logBuf.Append("==========================================================" & N)
            logBuf.Append("■販売実績取込処理出力情報■" & N)
            logBuf.Append("  材料票マスタ未登録品名コード（目付の取得が行えなかった品名コード）" & N)
            logBuf.Append("----------------------------------------------------------" & N)
            For i As Integer = 0 To prmRefNullMetsukeCnt - 1
                logBuf.Append(_db.rmNullStr(ds.Tables(RS).Rows(i)("HINMEICD")) & N)
            Next
            logBuf.Append("==========================================================")
            '-->2010.12.02 upd by takagi 
            'Dim tw As UtilMDL.Text.UtilTextWriter = New UtilMDL.Text.UtilTextWriter(UtilClass.getAppPath(StartUp.assembly) & "\" & IMP_LOG_NM)
            Dim tw As UtilMDL.Text.UtilTextWriter = New UtilMDL.Text.UtilTextWriter(outFilePath)
            '<--2010.12.02 upd by takagi
            '-->2011.01.18 chg by takagi #82
            'tw.open(False)
            tw.open(prmAppendFlg)
            '<--2011.01.18 chg by takagi #82
            Try
                tw.writeLine(logBuf.ToString)
            Finally
                tw.close()
            End Try

        End If

    End Sub


    '-------------------------------------------------------------------------------
    '   販売実績DB作成
    '   （処理概要）ワークより販売実績DB(T71)へデータ投入する
    '   ●入力パラメタ  ：なし
    '   ●出力パラメタ  ：なし
    '   ●メソッド戻り値：なし
    '-------------------------------------------------------------------------------
    Private Sub insertHanbaiJissekiDB()

        Dim sqlBuff As System.Text.StringBuilder = New System.Text.StringBuilder
        sqlBufF.Append(N & "INSERT INTO T71HANBAIS ")
        sqlBufF.Append(N & "( ")
        sqlBufF.Append(N & " HINMEIKBN1  ")                           '品名区分１
        sqlBufF.Append(N & ",HINMEIKBN2  ")                           '品名区分２
        sqlBufF.Append(N & ",HINMEIKBN3  ")                           '品名区分３
        sqlBufF.Append(N & ",HINMEIKBN4  ")                           '品名区分４
        sqlBufF.Append(N & ",HINMEIKBN5  ")                           '品名区分５
        sqlBufF.Append(N & ",HINMEIKBN6  ")                           '品名区分６
        sqlBufF.Append(N & ",HINMEIKBN7  ")                           '品名区分７
        sqlBufF.Append(N & ",HINMEIKBN8  ")                           '品名区分８
        sqlBufF.Append(N & ",TORIHIKISAKIKBN1  ")                     '取引先区分１
        sqlBufF.Append(N & ",TORIHIKISAKIKBN2  ")                     '取引先区分２
        sqlBufF.Append(N & ",TORIHIKISAKIKBN3  ")                     '取引先区分３
        sqlBufF.Append(N & ",TORIHIKISAKIKBN4  ")                     '取引先区分４
        sqlBufF.Append(N & ",TORIHIKISAKIKBN5  ")                     '取引先区分５
        sqlBufF.Append(N & ",TORIHIKISAKIKBN6  ")                     '取引先区分６
        sqlBufF.Append(N & ",TO_SONEKI_KBN  ")                        '得意先別損益算出用抽出区分
        sqlBufF.Append(N & ",TORIHIKIKBN  ")                          '取引データ区分
        sqlBufF.Append(N & ",NYUKO  ")                                '入庫倉庫
        sqlBufF.Append(N & ",SYUKO  ")                                '出庫倉庫
        sqlBufF.Append(N & ",SYOHINKBN  ")                            '商品区分
        sqlBufF.Append(N & ",SIYO  ")                                 '仕様
        sqlBufF.Append(N & ",HINSYU  ")                               '品種
        sqlBufF.Append(N & ",SENSHIN  ")                              '線心数
        sqlBufF.Append(N & ",SIZECD  ")                               'サイズ
        sqlBufF.Append(N & ",IRO  ")                                  '色
        sqlBufF.Append(N & ",YOBI1  ")                                '予備１
        sqlBufF.Append(N & ",HINSYUMEI  ")                            '品種名
        sqlBufF.Append(N & ",SIZEMEI  ")                              'サイズ名
        sqlBufF.Append(N & ",IROMEI  ")                               '色支持線名
        sqlBufF.Append(N & ",SYUKANUM  ")                             '出荷数
        sqlBufF.Append(N & ",UNIT  ")                                 '単位
        sqlBufF.Append(N & ",DOUTAIKBN  ")                            '導体区分
        sqlBufF.Append(N & ",TANKA  ")                                '単価
        sqlBufF.Append(N & ",KINGAKU  ")                              '金額
        sqlBufF.Append(N & ",TORIHIKISAKIKBN  ")                      '取引先区分
        sqlBufF.Append(N & ",TORIHIKISAKI  ")                         '取引先
        sqlBufF.Append(N & ",SHITENCD  ")                             '支店コード
        sqlBufF.Append(N & ",TORIHIKISAKIMEI  ")                      '取引先名称
        sqlBufF.Append(N & ",NOUSHOKBN  ")                            '納所区分
        sqlBufF.Append(N & ",NOUSHO  ")                               '納所
        sqlBufF.Append(N & ",NOUHINCD  ")                             '納所コード
        sqlBufF.Append(N & ",NOUSYOMEI  ")                            '納所名称
        sqlBufF.Append(N & ",DNPYOUNO  ")                             '伝票ＮＯ
        sqlBufF.Append(N & ",DENPYOUGYONO  ")                         '伝票行ＮＯ
        sqlBufF.Append(N & ",SYORIBI  ")                              '処理日
        sqlBufF.Append(N & ",SETSUDANMOTO  ")                         '切断条長元
        sqlBufF.Append(N & ",JYOCHO  ")                               '条長
        sqlBufF.Append(N & ",KOSU  ")                                 '個数
        sqlBufF.Append(N & ",TYOUBOKBN  ")                            '帳簿区分
        sqlBufF.Append(N & ",ZAIMUGENKAKBN  ")                        '財務原価区分
        sqlBufF.Append(N & ",ZAIKOKANRIKBN  ")                        '在庫管理区分
        sqlBufF.Append(N & ",CHOTATUKBN  ")                           '調達区分
        sqlBufF.Append(N & ",KANRYOKBN  ")                            '完了区分
        sqlBufF.Append(N & ",TEIHAKBN  ")                             '定端区分
        sqlBufF.Append(N & ",SYUKKABI  ")                             '出荷日
        sqlBufF.Append(N & ",KONPOUTYPE  ")                           '梱包タイプコード
        sqlBufF.Append(N & ",KONPONUM  ")                             '梱包数
        sqlBufF.Append(N & ",UNSOKBN  ")                              '運送区分
        sqlBufF.Append(N & ",UNSOKAISHAKBN  ")                        '運送会社コード
        sqlBufF.Append(N & ",UNCHINKBN  ")                            '運賃区分
        sqlBufF.Append(N & ",KARIKENSHUKINGAKU  ")                    '仮検収金額
        sqlBufF.Append(N & ",BUMON  ")                                '部門
        sqlBufF.Append(N & ",JYUCHUDENPYOUNO  ")                      '受注伝票ＮＯ
        sqlBufF.Append(N & ",JYUCHGYONO  ")                           '受注行ＮＯ
        sqlBufF.Append(N & ",JYUCHUGAPPI  ")                          '受注月日
        sqlBufF.Append(N & ",NOUKI  ")                                '納期
        sqlBufF.Append(N & ",HIKIATEKBN  ")                           '引当区分
        sqlBufF.Append(N & ",KENMEI  ")                               '件名
        sqlBufF.Append(N & ",SHUYOTORIHIKISAKI  ")                    '主要取引先コード
        sqlBufF.Append(N & ",JYUYOBUMON  ")                           '需要部門
        sqlBufF.Append(N & ",DOUBASE  ")                              '銅ベース
        sqlBufF.Append(N & ",RIEKI  ")                                '利益額
        sqlBufF.Append(N & ",CHUMONNO  ")                             '注文ＮＯ
        sqlBufF.Append(N & ",KEIYAKUKBN  ")                           '契約区分
        sqlBufF.Append(N & ",KEIYAKUTSUKI  ")                         '契約月
        sqlBufF.Append(N & ",SHIHARAIKBN  ")                          '支払区分
        sqlBufF.Append(N & ",HANBAITESURYORITSU  ")                   '販売手数料率
        sqlBufF.Append(N & ",MOME  ")                                 '社外メモ欄
        sqlBufF.Append(N & ",WAKUNO  ")                               '枠ＮＯ
        sqlBufF.Append(N & ",HIKIATENO  ")                            '引当ＮＯ
        sqlBufF.Append(N & ",GAISANJYURYO  ")                         '概算重量
        sqlBufF.Append(N & ",KADOUSAIN  ")                            '移動サイン
        sqlBufF.Append(N & ",TEIREIRINJIKBN  ")                       '定例臨時区分
        sqlBufF.Append(N & ",HAKKOUTANTO  ")                          '発行担当
        sqlBufF.Append(N & ",TANMATSUNO  ")                           '端末機ＮＯ
        sqlBufF.Append(N & ",JIGYOKBN  ")                             '事業部区分
        sqlBufF.Append(N & ",KAMOKUKBN  ")                            '科目区分
        sqlBufF.Append(N & ",YOBI2  ")                                '予備２
        sqlBufF.Append(N & ",CUBASE_ST  ")                            'ＣＵベース標準
        sqlBufF.Append(N & ",CUBASE_TEKIYO  ")                        'ＣＵベース適用
        sqlBufF.Append(N & ",SEIZOCOST  ")                            '製造コスト計
        sqlBufF.Append(N & ",SYUZAIRYOHI  ")                          '主材料費
        sqlBufF.Append(N & ",HUKUZAIRYOHI  ")                         '副資材費
        sqlBufF.Append(N & ",KAKOUHI  ")                              '加工費
        sqlBufF.Append(N & ",BASESAGAKU  ")                           'ベース差額
        sqlBufF.Append(N & ",HUKUSHIZAIHOSEI_RATE  ")                 '副資材補正係数
        sqlBufF.Append(N & ",HUKUSHIZAIHOSEI_GAKU  ")                 '副資材補正額
        sqlBufF.Append(N & ",DOURYOU  ")                              '銅量
        sqlBufF.Append(N & ",HIKARI_CORE  ")                          '光コア長
        sqlBufF.Append(N & ",HIKARI_CONECTOR  ")                      '光コネクタ数
        sqlBufF.Append(N & ",SHITENMEI  ")                            '支店名
        sqlBufF.Append(N & ",KITANIHONKINGAKU  ")                     '北日本金額
        sqlBufF.Append(N & ",KITANIHONTANKA  ")                       '北日本単価
        sqlBufF.Append(N & ",KITANIHONRIEKI  ")                       '北日本利益
        sqlBufF.Append(N & ",KITANIHONKOUNYUTANKA  ")                 '北日本購入単価
        sqlBufF.Append(N & ",HENDOUHANBAIHIRITSU  ")                  '変動販売費率
        sqlBufF.Append(N & ",HENDOUHANBAIHISAGAKU  ")                 '変動販売費額
        sqlBufF.Append(N & ",BUMONCD  ")                              '部門コード
        sqlBufF.Append(N & ",TOHIKISAKICD  ")                         '取引先コード
        sqlBufF.Append(N & ",TOUKEICD  ")                             '統計コード
        sqlBufF.Append(N & ",SDCRIEKI  ")                             'ＳＤＣ利益
        sqlBufF.Append(N & ",COSTSURAIDO  ")                          'コストスライド額
        sqlBufF.Append(N & ",SDCTANKA  ")                             'ＳＤＣ購入単価
        sqlBufF.Append(N & ",YOBI3  ")                                '予備３
        sqlBufF.Append(N & ",TORIHIKIKBN7  ")                         '取引先区分７
        sqlBufF.Append(N & ",TORIHIKIKBN8  ")                         '取引先区分８
        sqlBufF.Append(N & ",UPDNAME   ")                             '端末ID
        sqlBufF.Append(N & ",UPDDATE  ")                              '更新日時
        sqlBufF.Append(N & ") ")
        sqlBufF.Append(N & "SELECT ")
        sqlBuff.Append(N & " HINMEIKBN1  ")                           '品名区分１
        sqlBuff.Append(N & ",HINMEIKBN2  ")                           '品名区分２
        sqlBuff.Append(N & ",HINMEIKBN3  ")                           '品名区分３
        sqlBuff.Append(N & ",HINMEIKBN4  ")                           '品名区分４
        sqlBuff.Append(N & ",HINMEIKBN5  ")                           '品名区分５
        sqlBuff.Append(N & ",HINMEIKBN6  ")                           '品名区分６
        sqlBuff.Append(N & ",HINMEIKBN7  ")                           '品名区分７
        sqlBuff.Append(N & ",HINMEIKBN8  ")                           '品名区分８
        sqlBuff.Append(N & ",TORIHIKISAKIKBN1  ")                     '取引先区分１
        sqlBuff.Append(N & ",TORIHIKISAKIKBN2  ")                     '取引先区分２
        sqlBuff.Append(N & ",TORIHIKISAKIKBN3  ")                     '取引先区分３
        sqlBuff.Append(N & ",TORIHIKISAKIKBN4  ")                     '取引先区分４
        sqlBuff.Append(N & ",TORIHIKISAKIKBN5  ")                     '取引先区分５
        sqlBuff.Append(N & ",TORIHIKISAKIKBN6  ")                     '取引先区分６
        sqlBuff.Append(N & ",TO_SONEKI_KBN  ")                        '得意先別損益算出用抽出区分
        sqlBuff.Append(N & ",TORIHIKIKBN  ")                          '取引データ区分
        sqlBuff.Append(N & ",NYUKO  ")                                '入庫倉庫
        sqlBuff.Append(N & ",SYUKO  ")                                '出庫倉庫
        sqlBuff.Append(N & ",SYOHINKBN  ")                            '商品区分
        sqlBuff.Append(N & ",SIYO  ")                                 '仕様
        sqlBuff.Append(N & ",HINSYU  ")                               '品種
        sqlBuff.Append(N & ",SENSHIN  ")                              '線心数
        sqlBuff.Append(N & ",SIZECD  ")                               'サイズ
        sqlBuff.Append(N & ",IRO  ")                                  '色
        sqlBuff.Append(N & ",YOBI1  ")                                '予備１
        sqlBuff.Append(N & ",HINSYUMEI  ")                            '品種名
        sqlBuff.Append(N & ",SIZEMEI  ")                              'サイズ名
        sqlBuff.Append(N & ",IROMEI  ")                               '色支持線名
        sqlBuff.Append(N & ",SYUKANUM  ")                             '出荷数
        sqlBuff.Append(N & ",UNIT  ")                                 '単位
        sqlBuff.Append(N & ",DOUTAIKBN  ")                            '導体区分
        sqlBuff.Append(N & ",TANKA  ")                                '単価
        sqlBuff.Append(N & ",KINGAKU  ")                              '金額
        sqlBuff.Append(N & ",TORIHIKISAKIKBN  ")                      '取引先区分
        sqlBuff.Append(N & ",TORIHIKISAKI  ")                         '取引先
        sqlBuff.Append(N & ",SHITENCD  ")                             '支店コード
        sqlBuff.Append(N & ",TORIHIKISAKIMEI  ")                      '取引先名称
        sqlBuff.Append(N & ",NOUSHOKBN  ")                            '納所区分
        sqlBuff.Append(N & ",NOUSHO  ")                               '納所
        sqlBuff.Append(N & ",NOUHINCD  ")                             '納所コード
        sqlBuff.Append(N & ",NOUSYOMEI  ")                            '納所名称
        sqlBuff.Append(N & ",DNPYOUNO  ")                             '伝票ＮＯ
        sqlBuff.Append(N & ",DENPYOUGYONO  ")                         '伝票行ＮＯ
        sqlBuff.Append(N & ",SYORIBI  ")                              '処理日
        sqlBuff.Append(N & ",SETSUDANMOTO  ")                         '切断条長元
        sqlBuff.Append(N & ",JYOCHO  ")                               '条長
        sqlBuff.Append(N & ",KOSU  ")                                 '個数
        sqlBuff.Append(N & ",TYOUBOKBN  ")                            '帳簿区分
        sqlBuff.Append(N & ",ZAIMUGENKAKBN  ")                        '財務原価区分
        sqlBuff.Append(N & ",ZAIKOKANRIKBN  ")                        '在庫管理区分
        sqlBuff.Append(N & ",CHOTATUKBN  ")                           '調達区分
        sqlBuff.Append(N & ",KANRYOKBN  ")                            '完了区分
        sqlBuff.Append(N & ",TEIHAKBN  ")                             '定端区分
        sqlBuff.Append(N & ",SYUKKABI  ")                             '出荷日
        sqlBuff.Append(N & ",KONPOUTYPE  ")                           '梱包タイプコード
        sqlBuff.Append(N & ",KONPONUM  ")                             '梱包数
        sqlBuff.Append(N & ",UNSOKBN  ")                              '運送区分
        sqlBuff.Append(N & ",UNSOKAISHAKBN  ")                        '運送会社コード
        sqlBuff.Append(N & ",UNCHINKBN  ")                            '運賃区分
        sqlBuff.Append(N & ",KARIKENSHUKINGAKU  ")                    '仮検収金額
        sqlBuff.Append(N & ",BUMON  ")                                '部門
        sqlBuff.Append(N & ",JYUCHUDENPYOUNO  ")                      '受注伝票ＮＯ
        sqlBuff.Append(N & ",JYUCHGYONO  ")                           '受注行ＮＯ
        sqlBuff.Append(N & ",JYUCHUGAPPI  ")                          '受注月日
        sqlBuff.Append(N & ",NOUKI  ")                                '納期
        sqlBuff.Append(N & ",HIKIATEKBN  ")                           '引当区分
        sqlBuff.Append(N & ",KENMEI  ")                               '件名
        sqlBuff.Append(N & ",SHUYOTORIHIKISAKI  ")                    '主要取引先コード
        sqlBuff.Append(N & ",JYUYOBUMON  ")                           '需要部門
        sqlBuff.Append(N & ",DOUBASE  ")                              '銅ベース
        sqlBuff.Append(N & ",RIEKI  ")                                '利益額
        sqlBuff.Append(N & ",CHUMONNO  ")                             '注文ＮＯ
        sqlBuff.Append(N & ",KEIYAKUKBN  ")                           '契約区分
        sqlBuff.Append(N & ",KEIYAKUTSUKI  ")                         '契約月
        sqlBuff.Append(N & ",SHIHARAIKBN  ")                          '支払区分
        sqlBuff.Append(N & ",HANBAITESURYORITSU  ")                   '販売手数料率
        sqlBuff.Append(N & ",MOME  ")                                 '社外メモ欄
        sqlBuff.Append(N & ",WAKUNO  ")                               '枠ＮＯ
        sqlBuff.Append(N & ",HIKIATENO  ")                            '引当ＮＯ
        sqlBuff.Append(N & ",GAISANJYURYO  ")                         '概算重量
        sqlBuff.Append(N & ",KADOUSAIN  ")                            '移動サイン
        sqlBuff.Append(N & ",TEIREIRINJIKBN  ")                       '定例臨時区分
        sqlBuff.Append(N & ",HAKKOUTANTO  ")                          '発行担当
        sqlBuff.Append(N & ",TANMATSUNO  ")                           '端末機ＮＯ
        sqlBuff.Append(N & ",JIGYOKBN  ")                             '事業部区分
        sqlBuff.Append(N & ",KAMOKUKBN  ")                            '科目区分
        sqlBuff.Append(N & ",YOBI2  ")                                '予備２
        sqlBuff.Append(N & ",CUBASE_ST  ")                            'ＣＵベース標準
        sqlBuff.Append(N & ",CUBASE_TEKIYO  ")                        'ＣＵベース適用
        sqlBuff.Append(N & ",SEIZOCOST  ")                            '製造コスト計
        sqlBuff.Append(N & ",SYUZAIRYOHI  ")                          '主材料費
        sqlBuff.Append(N & ",HUKUZAIRYOHI  ")                         '副資材費
        sqlBuff.Append(N & ",KAKOUHI  ")                              '加工費
        sqlBuff.Append(N & ",BASESAGAKU  ")                           'ベース差額
        sqlBuff.Append(N & ",HUKUSHIZAIHOSEI_RATE  ")                 '副資材補正係数
        sqlBuff.Append(N & ",HUKUSHIZAIHOSEI_GAKU  ")                 '副資材補正額
        sqlBuff.Append(N & ",DOURYOU  ")                              '銅量
        sqlBuff.Append(N & ",HIKARI_CORE  ")                          '光コア長
        sqlBuff.Append(N & ",HIKARI_CONECTOR  ")                      '光コネクタ数
        sqlBuff.Append(N & ",SHITENMEI  ")                            '支店名
        sqlBuff.Append(N & ",KITANIHONKINGAKU  ")                     '北日本金額
        sqlBuff.Append(N & ",KITANIHONTANKA  ")                       '北日本単価
        sqlBuff.Append(N & ",KITANIHONRIEKI  ")                       '北日本利益
        sqlBuff.Append(N & ",KITANIHONKOUNYUTANKA  ")                 '北日本購入単価
        sqlBuff.Append(N & ",HENDOUHANBAIHIRITSU  ")                  '変動販売費率
        sqlBuff.Append(N & ",HENDOUHANBAIHISAGAKU  ")                 '変動販売費額
        sqlBuff.Append(N & ",BUMONCD  ")                              '部門コード
        sqlBuff.Append(N & ",TOHIKISAKICD  ")                         '取引先コード
        sqlBuff.Append(N & ",TOUKEICD  ")                             '統計コード
        sqlBuff.Append(N & ",SDCRIEKI  ")                             'ＳＤＣ利益
        sqlBuff.Append(N & ",COSTSURAIDO  ")                          'コストスライド額
        sqlBuff.Append(N & ",SDCTANKA  ")                             'ＳＤＣ購入単価
        sqlBuff.Append(N & ",YOBI3  ")                                '予備３
        sqlBuff.Append(N & ",TORIHIKIKBN7  ")                         '取引先区分７
        sqlBuff.Append(N & ",TORIHIKIKBN8  ")                         '取引先区分８
        sqlBuff.Append(N & ",'" & UtilClass.getComputerName() & "'   ") '端末ID
        sqlBuff.Append(N & ",SYSDATE  ")                              '更新日時
        sqlBuff.Append(N & "FROM  ZG330B_W10  ")                  '販売実績WK
        sqlBuff.Append(N & "WHERE UPDNAME = '" & UtilClass.getComputerName() & "'")
        sqlBuff.Append(N & "  AND (TO_CHAR(TORIHIKIKBN) LIKE '11%' or TO_CHAR(TORIHIKIKBN) LIKE '12%')  ")  '取引データ区分
        sqlBuff.Append(N & "  AND SYOHINKBN = 1  ")                                                         '商品区分
        sqlBuff.Append(N & "  AND TYOUBOKBN = 1  ")                                                         '帳簿区分
        sqlBuff.Append(N & "  AND (HINMEIKBN2 LIKE '01%' or HINMEIKBN2 LIKE '02%') ")                       '品名区分2
        _db.executeDB(sqlBuff.ToString)

    End Sub

    '-->2011.01.16 add by takagi #新規計画品の販売実績再取込対応
    '-------------------------------------------------------------------------------
    '   納品書データ未取込品名コード抽出
    '-------------------------------------------------------------------------------
    Private Function getNonImportHinmei() As String
        Dim buf As System.Text.StringBuilder = New System.Text.StringBuilder

        Dim sql As String = ""
        Dim iRecCnt As Integer = 0
        sql = ""
        sql = sql & N & "SELECT "
        sql = sql & N & " SUB.HINMEICD"
        sql = sql & N & "FROM ("
        sql = sql & N & "	SELECT DISTINCT M12.HINMEICD"
        sql = sql & N & "	FROM M11KEIKAKUHIN M11 "
        sql = sql & N & "	INNER JOIN M12SYUYAKU M12 "
        sql = sql & N & "	ON M11.TT_KHINMEICD = M12.KHINMEICD"
        sql = sql & N & "	) SUB "
        sql = sql & N & "LEFT JOIN ("
        sql = sql & N & "	SELECT DISTINCT HINMEICD "
        sql = sql & N & "	FROM T10HINHANJ"
        sql = sql & N & "	) T10"
        sql = sql & N & "ON SUB.HINMEICD = T10.HINMEICD"
        sql = sql & N & "WHERE T10.HINMEICD IS NULL"
        sql = sql & N & "ORDER BY SUB.HINMEICD"
        Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)     '計画対象品でありながら、販売実績が存在しない品名CDを抽出
        With ds.Tables(RS)
            For i As Integer = 0 To iRecCnt - 1
                If buf.Length > 0 Then buf.Append(",")
                buf.Append(convNullStr(.Rows(i)("HINMEICD")))
            Next
        End With
        Return buf.tostring()

    End Function
    '<--2011.01.16 add by takagi #新規計画品の販売実績再取込対応

    '-------------------------------------------------------------------------------
    '   納品書データ取込
    '   （処理概要）SQLSV(納品書データ)からOracleWKへデータを取り込む
    '   ●入力パラメタ  ：プログレスバー
    '   ●出力パラメタ  ：なし
    '   ●メソッド戻り値：なし
    '-------------------------------------------------------------------------------
    '-->2011.01.16 add by takagi #未取込実績再取得
    'Private Sub importNohinsho(ByRef prmRefPb As UtilProgressBar)
    Private Sub importNohinsho(ByRef prmRefPb As UtilProgressBar, Optional ByVal prmNonImportsHinmeiCDs As String = "")
        '<--2011.01.16 add by takagi #未取込実績再取得

        Dim sysdate As String = "to_date('" & Format(Now, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS')"

        Dim targetYYYY As String = lblKonkaiYM.Text.Substring(0, 4)
        If 1 <= CInt(lblKonkaiYM.Text.Substring(5, 2)) And CInt(lblKonkaiYM.Text.Substring(5, 2)) <= 3 Then
            targetYYYY = CInt(targetYYYY) - 1
        End If

        'SQLSVより抽出
        Dim sql As String = ""
        Dim iRecCnt As Integer = 0
        sql = ""
        sql = sql & N & "select "
        sql = sql & N & " [品名区分１] "
        sql = sql & N & ",[品名区分２] "
        sql = sql & N & ",[品名区分３] "
        sql = sql & N & ",[品名区分４] "
        sql = sql & N & ",[品名区分５] "
        sql = sql & N & ",[品名区分６] "
        sql = sql & N & ",[品名区分７] "
        sql = sql & N & ",[品名区分８] "
        sql = sql & N & ",[取引先区分１] "
        sql = sql & N & ",[取引先区分２] "
        sql = sql & N & ",[取引先区分３] "
        sql = sql & N & ",[取引先区分４] "
        sql = sql & N & ",[取引先区分５] "
        sql = sql & N & ",[取引先区分６] "
        sql = sql & N & ",[得意先別損益算出用抽出区分] "
        sql = sql & N & ",[取引データ区分] "
        sql = sql & N & ",[入庫倉庫] "
        sql = sql & N & ",[出庫倉庫] "
        sql = sql & N & ",[商品区分] "
        sql = sql & N & ",[仕様] "
        sql = sql & N & ",[品種] "
        sql = sql & N & ",[線心数] "
        sql = sql & N & ",[サイズ] "
        sql = sql & N & ",[色] "
        sql = sql & N & ",[予備１] "
        sql = sql & N & ",[品種名] "
        sql = sql & N & ",[サイズ名] "
        sql = sql & N & ",[色支持線名] "
        sql = sql & N & ",[出荷数] "
        sql = sql & N & ",[単位] "
        sql = sql & N & ",[導体区分] "
        sql = sql & N & ",[単価] "
        sql = sql & N & ",[金額] "
        sql = sql & N & ",[取引先区分] "
        sql = sql & N & ",[取引先] "
        sql = sql & N & ",[支店コード] "
        sql = sql & N & ",[取引先名称] "
        sql = sql & N & ",[納所区分] "
        sql = sql & N & ",[納所] "
        sql = sql & N & ",[納所コード] "
        sql = sql & N & ",[納所名称] "
        sql = sql & N & ",[伝票ＮＯ] "
        sql = sql & N & ",[伝票行ＮＯ] "
        sql = sql & N & ",[処理日] "
        sql = sql & N & ",[切断条長元] "
        sql = sql & N & ",[条長] "
        sql = sql & N & ",[個数] "
        sql = sql & N & ",[帳簿区分] "
        sql = sql & N & ",[財務原価区分] "
        sql = sql & N & ",[在庫管理区分] "
        sql = sql & N & ",[調達区分] "
        sql = sql & N & ",[完了区分] "
        sql = sql & N & ",[定端区分] "
        sql = sql & N & ",[出荷日] "
        sql = sql & N & ",[梱包タイプコード] "
        sql = sql & N & ",[梱包数] "
        sql = sql & N & ",[運送区分] "
        sql = sql & N & ",[運送会社コード] "
        sql = sql & N & ",[運賃区分] "
        sql = sql & N & ",[仮検収金額] "
        sql = sql & N & ",[部門] "
        sql = sql & N & ",[受注伝票ＮＯ] "
        sql = sql & N & ",[受注行ＮＯ] "
        sql = sql & N & ",[受注月日] "
        sql = sql & N & ",[納期] "
        sql = sql & N & ",[引当区分] "
        sql = sql & N & ",[件名] "
        sql = sql & N & ",[主要取引先コード] "
        sql = sql & N & ",[需要部門] "
        sql = sql & N & ",[銅ベース] "
        sql = sql & N & ",[利益額] "
        sql = sql & N & ",[注文ＮＯ] "
        sql = sql & N & ",[契約区分] "
        sql = sql & N & ",[契約月] "
        sql = sql & N & ",[支払区分] "
        sql = sql & N & ",[販売手数料率] "
        sql = sql & N & ",[社外メモ欄] "
        sql = sql & N & ",[枠ＮＯ] "
        sql = sql & N & ",[引当ＮＯ] "
        sql = sql & N & ",[概算重量] "
        sql = sql & N & ",[移動サイン] "
        sql = sql & N & ",[定例臨時区分] "
        sql = sql & N & ",[発行担当] "
        sql = sql & N & ",[端末機ＮＯ] "
        sql = sql & N & ",[事業部区分] "
        sql = sql & N & ",[科目区分] "
        sql = sql & N & ",[予備２] "
        sql = sql & N & ",[ＣＵベース標準] "
        sql = sql & N & ",[ＣＵベース適用] "
        sql = sql & N & ",[製造コスト計] "
        sql = sql & N & ",[主材料費] "
        sql = sql & N & ",[副資材費] "
        sql = sql & N & ",[加工費] "
        sql = sql & N & ",[ベース差額] "
        sql = sql & N & ",[副資材補正係数] "
        sql = sql & N & ",[副資材補正額] "
        sql = sql & N & ",[銅量] "
        sql = sql & N & ",[光コア長] "
        sql = sql & N & ",[光コネクタ数] "
        sql = sql & N & ",[支店名] "
        sql = sql & N & ",[北日本金額] "
        sql = sql & N & ",[北日本単価] "
        sql = sql & N & ",[北日本利益] "
        sql = sql & N & ",[北日本購入単価] "
        sql = sql & N & ",[変動販売費率] "
        sql = sql & N & ",[変動販売費額] "
        sql = sql & N & ",[部門コード] "
        sql = sql & N & ",[取引先コード] "
        sql = sql & N & ",[統計コード] "
        sql = sql & N & ",[ＳＤＣ利益] "
        sql = sql & N & ",[コストスライド額] "
        sql = sql & N & ",[ＳＤＣ購入単価] "
        sql = sql & N & ",[予備３] "
        sql = sql & N & ",[取引先区分７] "
        sql = sql & N & ",[取引先区分８] "
        '-->2011.01.16 add by takagi #未取込実績再取得
        'sql = sql & N & "from T_納品書データ_" & targetYYYY & "累計 "
        'sql = sql & N & "where substring(convert(varchar,[出荷日]),1,6)='" & lblKonkaiYM.Text.Replace("/", "") & "' "
        If "".Equals(prmNonImportsHinmeiCDs) Then
            sql = sql & N & "from T_納品書データ_" & targetYYYY & "累計 "
            sql = sql & N & "where substring(convert(varchar,[出荷日]),1,6)='" & lblKonkaiYM.Text.Replace("/", "") & "' "
        Else
            sql = sql & N & "from (select * from T_納品書データ_" & targetYYYY & "累計 union all select * from T_納品書データ_" & (CInt(targetYYYY) - 1) & "累計) as uniTbl "
            sql = sql & N & "where (substring((仕様+' '),1,2) + 品種 + 線心数 + サイズ + 色) in (" & prmNonImportsHinmeiCDs & ")"
        End If
        '<--2011.01.16 add by takagi #未取込実績再取得
        Dim ds As DataSet = _nouhinshoDb.selectDB(sql, RS, iRecCnt)     '納品書データ用コネクションへSelect発行

        '-->2011.01.16 add by takagi #未取込実績再取得
        If Not "".Equals(prmNonImportsHinmeiCDs) Then
            prmRefPb.maxVal = iRecCnt
        End If
        '<--2011.01.16 add by takagi #未取込実績再取得

        'ORACLEへインサート
        Dim sqlBuf As System.Text.StringBuilder = New System.Text.StringBuilder
        Dim sqlBufF As System.Text.StringBuilder = New System.Text.StringBuilder
        sqlBufF.Append(N & "insert into ZG330B_W10 ")
        sqlBufF.Append(N & "( ")
        sqlBufF.Append(N & " HINMEIKBN1  ")                           '品名区分１
        sqlBufF.Append(N & ",HINMEIKBN2  ")                           '品名区分２
        sqlBufF.Append(N & ",HINMEIKBN3  ")                           '品名区分３
        sqlBufF.Append(N & ",HINMEIKBN4  ")                           '品名区分４
        sqlBufF.Append(N & ",HINMEIKBN5  ")                           '品名区分５
        sqlBufF.Append(N & ",HINMEIKBN6  ")                           '品名区分６
        sqlBufF.Append(N & ",HINMEIKBN7  ")                           '品名区分７
        sqlBufF.Append(N & ",HINMEIKBN8  ")                           '品名区分８
        sqlBufF.Append(N & ",TORIHIKISAKIKBN1  ")                     '取引先区分１
        sqlBufF.Append(N & ",TORIHIKISAKIKBN2  ")                     '取引先区分２
        sqlBufF.Append(N & ",TORIHIKISAKIKBN3  ")                     '取引先区分３
        sqlBufF.Append(N & ",TORIHIKISAKIKBN4  ")                     '取引先区分４
        sqlBufF.Append(N & ",TORIHIKISAKIKBN5  ")                     '取引先区分５
        sqlBufF.Append(N & ",TORIHIKISAKIKBN6  ")                     '取引先区分６
        sqlBufF.Append(N & ",TO_SONEKI_KBN  ")                        '得意先別損益算出用抽出区分
        sqlBufF.Append(N & ",TORIHIKIKBN  ")                          '取引データ区分
        sqlBufF.Append(N & ",NYUKO  ")                                '入庫倉庫
        sqlBufF.Append(N & ",SYUKO  ")                                '出庫倉庫
        sqlBufF.Append(N & ",SYOHINKBN  ")                            '商品区分
        sqlBufF.Append(N & ",SIYO  ")                                 '仕様
        sqlBufF.Append(N & ",HINSYU  ")                               '品種
        sqlBufF.Append(N & ",SENSHIN  ")                              '線心数
        sqlBufF.Append(N & ",SIZECD  ")                               'サイズ
        sqlBufF.Append(N & ",IRO  ")                                  '色
        sqlBufF.Append(N & ",YOBI1  ")                                '予備１
        sqlBufF.Append(N & ",HINSYUMEI  ")                            '品種名
        sqlBufF.Append(N & ",SIZEMEI  ")                              'サイズ名
        sqlBufF.Append(N & ",IROMEI  ")                               '色支持線名
        sqlBufF.Append(N & ",SYUKANUM  ")                             '出荷数
        sqlBufF.Append(N & ",UNIT  ")                                 '単位
        sqlBufF.Append(N & ",DOUTAIKBN  ")                            '導体区分
        sqlBufF.Append(N & ",TANKA  ")                                '単価
        sqlBufF.Append(N & ",KINGAKU  ")                              '金額
        sqlBufF.Append(N & ",TORIHIKISAKIKBN  ")                      '取引先区分
        sqlBufF.Append(N & ",TORIHIKISAKI  ")                         '取引先
        sqlBufF.Append(N & ",SHITENCD  ")                             '支店コード
        sqlBufF.Append(N & ",TORIHIKISAKIMEI  ")                      '取引先名称
        sqlBufF.Append(N & ",NOUSHOKBN  ")                            '納所区分
        sqlBufF.Append(N & ",NOUSHO  ")                               '納所
        sqlBufF.Append(N & ",NOUHINCD  ")                             '納所コード
        sqlBufF.Append(N & ",NOUSYOMEI  ")                            '納所名称
        sqlBufF.Append(N & ",DNPYOUNO  ")                             '伝票ＮＯ
        sqlBufF.Append(N & ",DENPYOUGYONO  ")                         '伝票行ＮＯ
        sqlBufF.Append(N & ",SYORIBI  ")                              '処理日
        sqlBufF.Append(N & ",SETSUDANMOTO  ")                         '切断条長元
        sqlBufF.Append(N & ",JYOCHO  ")                               '条長
        sqlBufF.Append(N & ",KOSU  ")                                 '個数
        sqlBufF.Append(N & ",TYOUBOKBN  ")                            '帳簿区分
        sqlBufF.Append(N & ",ZAIMUGENKAKBN  ")                        '財務原価区分
        sqlBufF.Append(N & ",ZAIKOKANRIKBN  ")                        '在庫管理区分
        sqlBufF.Append(N & ",CHOTATUKBN  ")                           '調達区分
        sqlBufF.Append(N & ",KANRYOKBN  ")                            '完了区分
        sqlBufF.Append(N & ",TEIHAKBN  ")                             '定端区分
        sqlBufF.Append(N & ",SYUKKABI  ")                             '出荷日
        sqlBufF.Append(N & ",KONPOUTYPE  ")                           '梱包タイプコード
        sqlBufF.Append(N & ",KONPONUM  ")                             '梱包数
        sqlBufF.Append(N & ",UNSOKBN  ")                              '運送区分
        sqlBufF.Append(N & ",UNSOKAISHAKBN  ")                        '運送会社コード
        sqlBufF.Append(N & ",UNCHINKBN  ")                            '運賃区分
        sqlBufF.Append(N & ",KARIKENSHUKINGAKU  ")                    '仮検収金額
        sqlBufF.Append(N & ",BUMON  ")                                '部門
        sqlBufF.Append(N & ",JYUCHUDENPYOUNO  ")                      '受注伝票ＮＯ
        sqlBufF.Append(N & ",JYUCHGYONO  ")                           '受注行ＮＯ
        sqlBufF.Append(N & ",JYUCHUGAPPI  ")                          '受注月日
        sqlBufF.Append(N & ",NOUKI  ")                                '納期
        sqlBufF.Append(N & ",HIKIATEKBN  ")                           '引当区分
        sqlBufF.Append(N & ",KENMEI  ")                               '件名
        sqlBufF.Append(N & ",SHUYOTORIHIKISAKI  ")                    '主要取引先コード
        sqlBufF.Append(N & ",JYUYOBUMON  ")                           '需要部門
        sqlBufF.Append(N & ",DOUBASE  ")                              '銅ベース
        sqlBufF.Append(N & ",RIEKI  ")                                '利益額
        sqlBufF.Append(N & ",CHUMONNO  ")                             '注文ＮＯ
        sqlBufF.Append(N & ",KEIYAKUKBN  ")                           '契約区分
        sqlBufF.Append(N & ",KEIYAKUTSUKI  ")                         '契約月
        sqlBufF.Append(N & ",SHIHARAIKBN  ")                          '支払区分
        sqlBufF.Append(N & ",HANBAITESURYORITSU  ")                   '販売手数料率
        sqlBufF.Append(N & ",MOME  ")                                 '社外メモ欄
        sqlBufF.Append(N & ",WAKUNO  ")                               '枠ＮＯ
        sqlBufF.Append(N & ",HIKIATENO  ")                            '引当ＮＯ
        sqlBufF.Append(N & ",GAISANJYURYO  ")                         '概算重量
        sqlBufF.Append(N & ",KADOUSAIN  ")                            '移動サイン
        sqlBufF.Append(N & ",TEIREIRINJIKBN  ")                       '定例臨時区分
        sqlBufF.Append(N & ",HAKKOUTANTO  ")                          '発行担当
        sqlBufF.Append(N & ",TANMATSUNO  ")                           '端末機ＮＯ
        sqlBufF.Append(N & ",JIGYOKBN  ")                             '事業部区分
        sqlBufF.Append(N & ",KAMOKUKBN  ")                            '科目区分
        sqlBufF.Append(N & ",YOBI2  ")                                '予備２
        sqlBufF.Append(N & ",CUBASE_ST  ")                            'ＣＵベース標準
        sqlBufF.Append(N & ",CUBASE_TEKIYO  ")                        'ＣＵベース適用
        sqlBufF.Append(N & ",SEIZOCOST  ")                            '製造コスト計
        sqlBufF.Append(N & ",SYUZAIRYOHI  ")                          '主材料費
        sqlBufF.Append(N & ",HUKUZAIRYOHI  ")                         '副資材費
        sqlBufF.Append(N & ",KAKOUHI  ")                              '加工費
        sqlBufF.Append(N & ",BASESAGAKU  ")                           'ベース差額
        sqlBufF.Append(N & ",HUKUSHIZAIHOSEI_RATE  ")                 '副資材補正係数
        sqlBufF.Append(N & ",HUKUSHIZAIHOSEI_GAKU  ")                 '副資材補正額
        sqlBufF.Append(N & ",DOURYOU  ")                              '銅量
        sqlBufF.Append(N & ",HIKARI_CORE  ")                          '光コア長
        sqlBufF.Append(N & ",HIKARI_CONECTOR  ")                      '光コネクタ数
        sqlBufF.Append(N & ",SHITENMEI  ")                            '支店名
        sqlBufF.Append(N & ",KITANIHONKINGAKU  ")                     '北日本金額
        sqlBufF.Append(N & ",KITANIHONTANKA  ")                       '北日本単価
        sqlBufF.Append(N & ",KITANIHONRIEKI  ")                       '北日本利益
        sqlBufF.Append(N & ",KITANIHONKOUNYUTANKA  ")                 '北日本購入単価
        sqlBufF.Append(N & ",HENDOUHANBAIHIRITSU  ")                  '変動販売費率
        sqlBufF.Append(N & ",HENDOUHANBAIHISAGAKU  ")                 '変動販売費額
        sqlBufF.Append(N & ",BUMONCD  ")                              '部門コード
        sqlBufF.Append(N & ",TOHIKISAKICD  ")                         '取引先コード
        sqlBufF.Append(N & ",TOUKEICD  ")                             '統計コード
        sqlBufF.Append(N & ",SDCRIEKI  ")                             'ＳＤＣ利益
        sqlBufF.Append(N & ",COSTSURAIDO  ")                          'コストスライド額
        sqlBufF.Append(N & ",SDCTANKA  ")                             'ＳＤＣ購入単価
        sqlBufF.Append(N & ",YOBI3  ")                                '予備３
        sqlBufF.Append(N & ",TORIHIKIKBN7  ")                         '取引先区分７
        sqlBufF.Append(N & ",TORIHIKIKBN8  ")                         '取引先区分８
        sqlBufF.Append(N & ",UPDNAME   ")                             '端末ID
        sqlBufF.Append(N & ",UPDDATE  ")                              '更新日時
        sqlBufF.Append(N & ")values( ")
        With ds.Tables(RS)
            For i As Integer = 0 To iRecCnt - 1
                sqlBuf.Remove(0, sqlBuf.Length)
                sqlBuf.Append(N & "  " & convNullStr(.Rows(i)("品名区分１")) & " ")                  'HINMEIKBN1 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("品名区分２")) & " ")                  'HINMEIKBN2 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("品名区分３")) & " ")                  'HINMEIKBN3 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("品名区分４")) & " ")                  'HINMEIKBN4 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("品名区分５")) & " ")                  'HINMEIKBN5 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("品名区分６")) & " ")                  'HINMEIKBN6 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("品名区分７")) & " ")                  'HINMEIKBN7 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("品名区分８")) & " ")                  'HINMEIKBN8 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("取引先区分１")) & " ")                'TORIHIKISAKIKBN1 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("取引先区分２")) & " ")                'TORIHIKISAKIKBN2 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("取引先区分３")) & " ")                'TORIHIKISAKIKBN3 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("取引先区分４")) & " ")                'TORIHIKISAKIKBN4 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("取引先区分５")) & " ")                'TORIHIKISAKIKBN5 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("取引先区分６")) & " ")                'TORIHIKISAKIKBN6 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("得意先別損益算出用抽出区分")) & " ")  'TO_SONEKI_KBN 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("取引データ区分")) & " ")              'TORIHIKIKBN 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("入庫倉庫")) & " ")                    'NYUKO 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("出庫倉庫")) & " ")                    'SYUKO 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("商品区分")) & " ")                    'SYOHINKBN 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("仕様")) & " ")                        'SIYO 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("品種")) & " ")                        'HINSYU 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("線心数")) & " ")                      'SENSHIN 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("サイズ")) & " ")                      'SIZECD 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("色")) & " ")                          'IRO 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("予備１")) & " ")                      'YOBI1 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("品種名")) & " ")                      'HINSYUMEI 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("サイズ名")) & " ")                    'SIZEMEI 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("色支持線名")) & " ")                  'IROMEI 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("出荷数")) & " ")                      'SYUKANUM 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("単位")) & " ")                        'UNIT 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("導体区分")) & " ")                    'DOUTAIKBN 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("単価")) & " ")                        'TANKA 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("金額")) & " ")                        'KINGAKU 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("取引先区分")) & " ")                  'TORIHIKISAKIKBN 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("取引先")) & " ")                      'TORIHIKISAKI 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("支店コード")) & " ")                  'SHITENCD 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("取引先名称")) & " ")                  'TORIHIKISAKIMEI 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("納所区分")) & " ")                    'NOUSHOKBN 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("納所")) & " ")                        'NOUSHO 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("納所コード")) & " ")                  'NOUHINCD 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("納所名称")) & " ")                    'NOUSYOMEI 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("伝票ＮＯ")) & " ")                    'DNPYOUNO 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("伝票行ＮＯ")) & " ")                  'DENPYOUGYONO 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("処理日")) & " ")                      'SYORIBI 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("切断条長元")) & " ")                  'SETSUDANMOTO 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("条長")) & " ")                        'JYOCHO 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("個数")) & " ")                        'KOSU 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("帳簿区分")) & " ")                    'TYOUBOKBN 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("財務原価区分")) & " ")                'ZAIMUGENKAKBN 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("在庫管理区分")) & " ")                'ZAIKOKANRIKBN 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("調達区分")) & " ")                    'CHOTATUKBN 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("完了区分")) & " ")                    'KANRYOKBN 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("定端区分")) & " ")                    'TEIHAKBN 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("出荷日")) & " ")                      'SYUKKABI 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("梱包タイプコード")) & " ")            'KONPOUTYPE 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("梱包数")) & " ")                      'KONPONUM 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("運送区分")) & " ")                    'UNSOKBN 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("運送会社コード")) & " ")              'UNSOKAISHAKBN 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("運賃区分")) & " ")                    'UNCHINKBN 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("仮検収金額")) & " ")                  'KARIKENSHUKINGAKU 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("部門")) & " ")                        'BUMON 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("受注伝票ＮＯ")) & " ")                'JYUCHUDENPYOUNO 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("受注行ＮＯ")) & " ")                  'JYUCHGYONO 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("受注月日")) & " ")                    'JYUCHUGAPPI 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("納期")) & " ")                        'NOUKI 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("引当区分")) & " ")                    'HIKIATEKBN 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("件名")) & " ")                        'KENMEI 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("主要取引先コード")) & " ")            'SHUYOTORIHIKISAKI 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("需要部門")) & " ")                    'JYUYOBUMON 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("銅ベース")) & " ")                    'DOUBASE 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("利益額")) & " ")                      'RIEKI 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("注文ＮＯ")) & " ")                    'CHUMONNO 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("契約区分")) & " ")                    'KEIYAKUKBN 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("契約月")) & " ")                      'KEIYAKUTSUKI 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("支払区分")) & " ")                    'SHIHARAIKBN 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("販売手数料率")) & " ")                'HANBAITESURYORITSU 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("社外メモ欄")) & " ")                  'MOME 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("枠ＮＯ")) & " ")                      'WAKUNO 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("引当ＮＯ")) & " ")                    'HIKIATENO 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("概算重量")) & " ")                    'GAISANJYURYO 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("移動サイン")) & " ")                  'KADOUSAIN 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("定例臨時区分")) & " ")                'TEIREIRINJIKBN 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("発行担当")) & " ")                    'HAKKOUTANTO 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("端末機ＮＯ")) & " ")                  'TANMATSUNO 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("事業部区分")) & " ")                  'JIGYOKBN 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("科目区分")) & " ")                    'KAMOKUKBN 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("予備２")) & " ")                      'YOBI2 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("ＣＵベース標準")) & " ")              'CUBASE_ST 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("ＣＵベース適用")) & " ")              'CUBASE_TEKIYO 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("製造コスト計")) & " ")                'SEIZOCOST 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("主材料費")) & " ")                    'SYUZAIRYOHI 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("副資材費")) & " ")                    'HUKUZAIRYOHI 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("加工費")) & " ")                      'KAKOUHI 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("ベース差額")) & " ")                  'BASESAGAKU 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("副資材補正係数")) & " ")              'HUKUSHIZAIHOSEI_RATE 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("副資材補正額")) & " ")                'HUKUSHIZAIHOSEI_GAKU 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("銅量")) & " ")                        'DOURYOU 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("光コア長")) & " ")                    'HIKARI_CORE 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("光コネクタ数")) & " ")                'HIKARI_CONECTOR 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("支店名")) & " ")                      'SHITENMEI 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("北日本金額")) & " ")                  'KITANIHONKINGAKU 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("北日本単価")) & " ")                  'KITANIHONTANKA 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("北日本利益")) & " ")                  'KITANIHONRIEKI 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("北日本購入単価")) & " ")              'KITANIHONKOUNYUTANKA 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("変動販売費率")) & " ")                'HENDOUHANBAIHIRITSU 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("変動販売費額")) & " ")                'HENDOUHANBAIHISAGAKU 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("部門コード")) & " ")                  'BUMONCD 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("取引先コード")) & " ")                'TOHIKISAKICD 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("統計コード")) & " ")                  'TOUKEICD 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("ＳＤＣ利益")) & " ")                  'SDCRIEKI 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("コストスライド額")) & " ")            'COSTSURAIDO 
                sqlBuf.Append(N & ", " & convNullNUm(.Rows(i)("ＳＤＣ購入単価")) & " ")              'SDCTANKA 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("予備３")) & " ")                      'YOBI3 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("取引先区分７")) & " ")                'TORIHIKIKBN7 
                sqlBuf.Append(N & ", " & convNullStr(.Rows(i)("取引先区分８")) & " ")                'TORIHIKIKBN8 
                sqlBuf.Append(N & ", " & convNullStr(UtilClass.getComputerName()) & " ")             'UPDNAME  
                sqlBuf.Append(N & ", " & sysdate & " ")                                              'UPDDATE 
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