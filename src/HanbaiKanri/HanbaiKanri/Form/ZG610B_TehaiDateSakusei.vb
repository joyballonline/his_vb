'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）手配データ作成指示指示
'    （フォームID）ZG610B_TehaiDateSakusei
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   高木        2010/11/10                 新規              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Public Class ZG610B_TehaiDateSakusei
    Inherits System.Windows.Forms.Form

    '-------------------------------------------------------------------------------
    '   構造体定義
    '-------------------------------------------------------------------------------
    Private Structure shuttaiBiType
        Dim shuttaiBi1 As String
        Dim shuttaiBi2 As String
        Dim shuttaiBi3 As String
        Dim shuttaiBi4 As String
        Dim shuttaiBi5 As String
        Dim shuttaiBi6 As String
    End Structure
    Private Structure abcType
        Dim nomal As shuttaiBiType
        Dim chozoHin As shuttaiBiType
    End Structure

    '-------------------------------------------------------------------------------
    '   定数定義
    '-------------------------------------------------------------------------------
    Private Const RS As String = "RecSet"                       'データセットテーブル名
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const PGID As String = "ZG610B"

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
    'コンストラクタ（Privateにして、外からは呼べないようにする）
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
    Private Sub ZG610B_TehaiDateSakusei_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            sql = sql & N & "    from T91RIREKI "
            sql = sql & N & "    where PGID     = '" & PGID & "' "
            sql = sql & N & ") "
            sql = sql & N & "where RNUM = 1 "
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)
            If iRecCnt < 1 Then
                '履歴なし
                lblJikkouDate.Text = ZC110M_Menu.NON_EXECUTE
                lblZNyuuryokuKensu.Text = ""
                lblZSyuturyokuKensu.Text = ""
            Else
                '履歴あり
                lblJikkouDate.Text = _db.rmNullDate(ds.Tables(RS).Rows(0)("SDATEEND"))
                lblZNyuuryokuKensu.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU1")), "#,##0")
                lblZSyuturyokuKensu.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU2")), "#,##0")
            End If

            '今回実行情報の表示
            lblKonkaiKensu.Text = Format(_db.rmNullInt(_db.selectDB("select count(*) CNT from T41SEISANK ", RS).Tables(RS).Rows(0)("CNT")), "#,##0")

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
                    pb.jobName = "手配作成処理を実行しています。"
                    pb.oneStep = 1

                    pb.status = "準備中" : pb.maxVal = 1
                    _db.executeDB("delete from ZG610B_W10 where updname = '" & UtilClass.getComputerName() & "'")

                    pb.status = "ベースデータ作成中"
                    Dim outputCnt As Integer = 0
                    Call insertBaseRecord(outputCnt, pb)
                    pb.status = "手配データ構築中"
                    Call updateWkColumns(pb)

                    _db.beginTran()
                    Try
                        _db.executeDB("delete from T51TEHAI")

                        pb.status = "手配データ作成中"
                        Call insertTehaiRec(pb)

                        pb.status = "ステータス変更中・・・"
                        ed = Now                    '処理終了日時
                        _parentForm.updateSeigyoTbl(PGID, True, st, ed)

                        pb.status = "実行履歴作成"
                        Call insertRireki(outputCnt, st, ed)                                  '2-1 実行履歴格納

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
    Private Sub insertRireki(ByRef prmRefOutputCnt As Integer, ByVal prmStDt As Date, ByVal prmEdDt As Date)

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
        sql = sql & N & ",UPDNAME "    '最終更新者
        sql = sql & N & ",UPDDATE "    '最終更新日
        sql = sql & N & ")values( "
        sql = sql & N & "  '" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "' "                             '処理年月
        sql = sql & N & ", '" & _db.rmSQ(lblKeikakuDate.Text.Replace("/", "")) & "' "                           '計画年月
        sql = sql & N & ", '" & _db.rmSQ(PGID) & "' "                                                           '機能ID
        sql = sql & N & ",to_date('" & Format(prmStDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '処理開始日時
        sql = sql & N & ",to_date('" & Format(prmEdDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '処理終了日時
        sql = sql & N & ", " & CLng(lblKonkaiKensu.Text) & " "                                                  '入力件数
        sql = sql & N & ", " & prmRefOutputCnt & " "                                                            '出力件数
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "                                     '最終更新者
        sql = sql & N & ",sysdate "                                                                             '最終更新日
        sql = sql & N & ") "
        _db.executeDB(sql)

    End Sub

    'ベースデータ作成
    Private Sub insertBaseRecord(ByRef prmRefOutputCnt As Integer, ByRef prmRefBar As UtilProgressBar)

        '展開パターン設定のロード
        Dim iniFileName As String = UtilClass.getAppPath(StartUp.assembly)
        If Not iniFileName.EndsWith("\") Then iniFileName = iniFileName & "\"
        iniFileName = iniFileName & "..\Setting\" & StartUp.INI_FILE
        Dim ini As UtilMDL.API.UtilIniFileHandler = New UtilMDL.API.UtilIniFileHandler(iniFileName)
        Dim tenkaiPth As abcType
        tenkaiPth.chozoHin.shuttaiBi1 = ini.getIni("Lot Expand Rule", "S1")
        tenkaiPth.chozoHin.shuttaiBi2 = ini.getIni("Lot Expand Rule", "S2")
        tenkaiPth.chozoHin.shuttaiBi3 = ini.getIni("Lot Expand Rule", "S3")
        tenkaiPth.chozoHin.shuttaiBi4 = ini.getIni("Lot Expand Rule", "S4")
        tenkaiPth.chozoHin.shuttaiBi5 = ini.getIni("Lot Expand Rule", "S5")
        tenkaiPth.chozoHin.shuttaiBi6 = ini.getIni("Lot Expand Rule", "S6")
        tenkaiPth.nomal.shuttaiBi1 = ini.getIni("Lot Expand Rule", "ABC1")
        tenkaiPth.nomal.shuttaiBi2 = ini.getIni("Lot Expand Rule", "ABC2")
        tenkaiPth.nomal.shuttaiBi3 = ini.getIni("Lot Expand Rule", "ABC3")
        tenkaiPth.nomal.shuttaiBi4 = ini.getIni("Lot Expand Rule", "ABC4")
        tenkaiPth.nomal.shuttaiBi5 = ini.getIni("Lot Expand Rule", "ABC5")
        tenkaiPth.nomal.shuttaiBi6 = ini.getIni("Lot Expand Rule", "ABC6")

        '希望出来日の取得
        Dim recCnt As Integer = 0
        Dim sql As String = ""
        sql = sql & N & "SELECT 1 KBN,KIBOU1 KIBOU FROM T01KEIKANRI "
        sql = sql & N & "UNION ALL "
        sql = sql & N & "SELECT 2 KBN,KIBOU2 KIBOU FROM T01KEIKANRI "
        sql = sql & N & "UNION ALL "
        sql = sql & N & "SELECT 3 KBN,KIBOU3 KIBOU FROM T01KEIKANRI "
        sql = sql & N & "UNION ALL "
        sql = sql & N & "SELECT 4 KBN,KIBOU4 KIBOU FROM T01KEIKANRI "
        sql = sql & N & "UNION ALL "
        sql = sql & N & "SELECT 5 KBN,KIBOU5 KIBOU FROM T01KEIKANRI "
        sql = sql & N & "UNION ALL "
        sql = sql & N & "SELECT 6 KBN,KIBOU6 KIBOU FROM T01KEIKANRI "
        Dim kibouDs As DataSet = _db.selectDB(sql, RS, recCnt)
        Dim kibouHash As Hashtable = New Hashtable
        With kibouDs.Tables(RS)
            For i As Integer = 0 To recCnt - 1
                kibouHash.Add(_db.rmNullInt(.Rows(i)("KBN")), _db.rmNullStr(.Rows(i)("KIBOU")))
            Next
        End With

        'T41生産計画の取得
        sql = ""
        sql = sql & N & "SELECT "
        sql = sql & N & " T.KHINMEICD "                        '計画品名CD
        sql = sql & N & ",SUBSTR(T.KHINMEICD,1,2) SHIYOCD "    '計画品名CD 仕様
        sql = sql & N & ",SUBSTR(T.KHINMEICD,3,3) HINSHUCD "   '計画品名CD 品種
        sql = sql & N & ",SUBSTR(T.KHINMEICD,6,3) SENSHINCD "  '計画品名CD 線心数
        sql = sql & N & ",SUBSTR(T.KHINMEICD,9,2) SIZECD "     '計画品名CD サイズ
        sql = sql & N & ",SUBSTR(T.KHINMEICD,11,3) IROCD "     '計画品名CD 色
        sql = sql & N & ",M.TT_HINSYUNM "                      '品種名 
        sql = sql & N & ",M.TT_SIZENM "                        'サイズ名 
        sql = sql & N & ",M.TT_IRONM "                         '色名称 
        sql = sql & N & ",1 LOTNUM "                           'ロット数 
        sql = sql & N & ",M.TT_LOT "                           '標準ロット長 
        sql = sql & N & ",M.TT_TANCYO "                        '単長 
        sql = sql & N & ",(M.TT_LOT/M.TT_TANCYO) HONSU "       '本数 
        sql = sql & N & ",H.NAME3 CHUMONSAKI "                 '注文先 
        sql = sql & N & ",T.LOTOSU "                           'ロット数  （展開元情報）
        sql = sql & N & ",M.TT_ABCKBN "                        'ABC区分   （展開元情報）
        sql = sql & N & "FROM T41SEISANK T "
        sql = sql & N & "INNER JOIN M11KEIKAKUHIN                                  M ON T.KHINMEICD  = M.TT_KHINMEICD "
        sql = sql & N & "LEFT  JOIN (SELECT * FROM M01HANYO WHERE KOTEIKEY = '01') H ON M.TT_JUYOUCD = H.KAHENKEY "
        sql = sql & N & "WHERE T.LOTOSU > 0 "
        Dim Ds As DataSet = _db.selectDB(sql, RS, recCnt)
        prmRefBar.maxVal = Ds.Tables(RS).Rows.Count - 1
        With Ds.Tables(RS)

            Dim nowdt As String = Format(Now, "yyyy/MM/dd HH:mm:ss")
            For i As Integer = 0 To recCnt - 1
                prmRefBar.value = i
                '希望出来日毎にロット展開
                Dim shuttaibi() As String = getShuttaibi(tenkaiPth, kibouHash, _db.rmNullStr(.Rows(i)("TT_ABCKBN")), _db.rmNullInt(.Rows(i)("LOTOSU")))

                '希望出来日毎にWKへレコード挿入
                For j As Integer = 0 To shuttaibi.Length - 1
                    sql = ""
                    sql = sql & N & "INSERT INTO ZG610B_W10"
                    sql = sql & N & "("
                    sql = sql & N & " ZW_KHINMEICD"    '計画品名CD
                    sql = sql & N & ",ZW_H_SIYOU_CD"   '仕様コード
                    sql = sql & N & ",ZW_H_HIN_CD"     '品種コード
                    sql = sql & N & ",ZW_H_SENSIN_CD"  '線心数
                    sql = sql & N & ",ZW_H_SIZE_CD"    'サイズ
                    sql = sql & N & ",ZW_H_COLOR_CD"   '色
                    sql = sql & N & ",ZW_HIN_DATA"     '品名データ
                    sql = sql & N & ",ZW_SIZE_DATA"    'サイズデータ
                    sql = sql & N & ",ZW_COLOR_DATA"   '色データ
                    sql = sql & N & ",ZW_TEHAI_SUU"    '手配数量
                    sql = sql & N & ",ZW_TANCYO"       '製作単長
                    sql = sql & N & ",ZW_JYO_SUU"      '条数
                    sql = sql & N & ",ZW_KIBOU_DATE"   '希望年月日
                    sql = sql & N & ",ZW_TYUMONSAKI"   '注文先
                    sql = sql & N & ",ZW_NOUKI"        '納期
                    sql = sql & N & ",ZW_LOT_SUU"      'ロット数
                    sql = sql & N & ",ZW_LOT_CHO"      'ロット長
                    sql = sql & N & ",UPDNAME "        '更新者
                    sql = sql & N & ",UPDDATE "        '更新日
                    sql = sql & N & ")VALUES("
                    sql = sql & N & " " & impDtForStr(_db.rmNullStr(.Rows(i)("KHINMEICD")))   '計画品名コード
                    sql = sql & N & "," & impDtForStr(_db.rmNullStr(.Rows(i)("SHIYOCD")))     '仕様コード
                    sql = sql & N & "," & impDtForStr(_db.rmNullStr(.Rows(i)("HINSHUCD")))    '品種コード
                    sql = sql & N & "," & impDtForStr(_db.rmNullStr(.Rows(i)("SENSHINCD")))   '線心数
                    sql = sql & N & "," & impDtForStr(_db.rmNullStr(.Rows(i)("SIZECD")))      'サイズ
                    sql = sql & N & "," & impDtForStr(_db.rmNullStr(.Rows(i)("IROCD")))       '色
                    sql = sql & N & "," & impDtForStr(_db.rmNullStr(.Rows(i)("TT_HINSYUNM"))) '品名データ
                    sql = sql & N & "," & impDtForStr(_db.rmNullStr(.Rows(i)("TT_SIZENM")))   'サイズデータ
                    sql = sql & N & "," & impDtForStr(_db.rmNullStr(.Rows(i)("TT_IRONM")))    '色データ
                    sql = sql & N & "," & impDtForNum(_db.rmNullStr(.Rows(i)("TT_LOT")))      '手配数量
                    sql = sql & N & "," & impDtForNum(_db.rmNullStr(.Rows(i)("TT_TANCYO")))   '製作単長
                    sql = sql & N & "," & impDtForNum(_db.rmNullStr(.Rows(i)("HONSU")))       '条数
                    sql = sql & N & "," & impDtForStr(shuttaibi(j))                           '希望年月日
                    sql = sql & N & "," & impDtForStr(_db.rmNullStr(.Rows(i)("CHUMONSAKI")))  '注文先
                    sql = sql & N & "," & impDtForStr(shuttaibi(j))                           '納期
                    sql = sql & N & "," & impDtForNum(_db.rmNullStr(.Rows(i)("LOTNUM")))      'ロット数
                    sql = sql & N & "," & impDtForNum(_db.rmNullStr(.Rows(i)("TT_LOT")))      'ロット長
                    sql = sql & N & ",'" & UtilClass.getComputerName() & "'"                  '更新者
                    sql = sql & N & ",TO_DATE('" & nowdt & "','YYYY/MM/DD HH24:MI:SS') "      '更新日
                    sql = sql & N & ")"
                    _db.executeDB(sql)
                    '2011/02/01 add start Sugawara #97
                    prmRefOutputCnt = prmRefOutputCnt + 1
                    '2011/02/01 add end Sugawara #97
                Next

            Next

        End With

    End Sub

    '手配データ構築
    Public Sub updateWkColumns(ByRef prmRefBar As UtilProgressBar)

        Dim SQL As String = ""
        SQL = SQL & "SELECT "
        SQL = SQL & " TT_KHINMEICD,"
        SQL = SQL & " TT_H_SIYOU_CD,"       ' 0：（品名コード）仕様コード
        SQL = SQL & " TT_H_HIN_CD,"         ' 1：（品名コード）品種コード
        SQL = SQL & " TT_H_SENSIN_CD,"      ' 2：（品名コード）線心数コード
        SQL = SQL & " TT_H_SIZE_CD,"        ' 3：（品名コード）サイズコード
        SQL = SQL & " TT_H_COLOR_CD,"       ' 4：（品名コード）色コード
        SQL = SQL & " TT_TANCYO,"           ' 5：製作単長
        SQL = SQL & " TT_FUKA_CD,"          ' 6：付加記号
        SQL = SQL & " TT_HINMEI,"           ' 7：品名
        SQL = SQL & " TT_TEHAI_SUU,"        ' 8：手配数量
        SQL = SQL & " TT_SYORI_KBN,"        ' 9：処理区分
        SQL = SQL & " TT_TEHAI_KBN,"        '10：手配区分
        SQL = SQL & " TT_SEISAKU_KBN,"      '11：製作区分
        SQL = SQL & " TT_TENKAI_KBN,"       '12：展開区分
        SQL = SQL & " TT_KOUTEI,"           '13：指定工程
        SQL = SQL & " TT_KEISAN_KBN,"       '14：加工長計算
        SQL = SQL & " TT_TATIAI_UM,"        '15：立会有無
        SQL = SQL & " TT_TANCYO_KBN,"       '16：単長区分
        SQL = SQL & " TT_MAKI_CD,"          '17：巻枠コード
        SQL = SQL & " TT_HOSO_KBN,"         '18：包装区分
        SQL = SQL & " TT_HINSITU_KBN,"      '19：品質試験区分
        SQL = SQL & " TT_SIYOUSYO_NO,"      '20：仕様書№
        SQL = SQL & " TT_SEIZOU_BMN,"       '21：製造部門
        'SQL = SQL & " TT_KONYU_SIYOU,"      '22：購入品用仕様コード
        SQL = SQL & " TT_KAMOKU_CD,"        '23：科目コード
        SQL = SQL & " TT_N_SO_SUU,"         '24：入庫本数 全体
        SQL = SQL & " TT_N_K_SUU,"          '25：入庫本数 北日本本数
        SQL = SQL & " TT_N_SH_SUU"          '26：入庫本数 住電日立本数
        SQL = SQL & " FROM"
        SQL = SQL & " M11KEIKAKUHIN TT"
        SQL = SQL & " WHERE EXISTS (SELECT * "
        SQL = SQL & "               FROM ZG610B_W10 zw "
        SQL = SQL & "               WHERE TT.TT_KHINMEICD = zw.ZW_KHINMEICD "
        SQL = SQL & "                AND  zw.UPDNAME      = '" & UtilClass.getComputerName() & "')"
        Dim ds As DataSet = _db.selectDB(SQL, RS)
        prmRefBar.maxVal = ds.Tables(RS).Rows.Count - 1
        With ds.Tables(RS)
            Dim nowdt As String = Format(Now, "yyyy/MM/dd HH:mm:ss")
            For i As Integer = 0 To .Rows.Count - 1
                prmRefBar.value = i
                SQL = ""
                SQL = SQL & N & "UPDATE ZG610B_W10 "
                SQL = SQL & N & "SET "
                SQL = SQL & N & " ZW_FUKA_CD     = " & impDtForStr(_db.rmNullStr(.Rows(i)("TT_FUKA_CD")))       '付加記号
                SQL = SQL & N & ",ZW_HINMEI      = " & impDtForStr(_db.rmNullStr(.Rows(i)("TT_HINMEI")))        '品名
                SQL = SQL & N & ",ZW_TEHAI_SUU   = " & impDtForNum(_db.rmNullStr(.Rows(i)("TT_TEHAI_SUU")))     '手配数量
                SQL = SQL & N & ",ZW_SYORI_KBN   = " & impDtForNum(_db.rmNullStr(.Rows(i)("TT_SYORI_KBN")))     '処理区分
                SQL = SQL & N & ",ZW_TEHAI_KBN   = " & impDtForNum(_db.rmNullStr(.Rows(i)("TT_TEHAI_KBN")))     '手配区分
                SQL = SQL & N & ",ZW_SEISAKU_KBN = " & impDtForNum(_db.rmNullStr(.Rows(i)("TT_SEISAKU_KBN")))   '製作区分
                SQL = SQL & N & ",ZW_SEIZOU_BMN  = " & impDtForNum(_db.rmNullStr(.Rows(i)("TT_SEIZOU_BMN")))    '製造部門
                SQL = SQL & N & ",ZW_TANCYO_KBN  = " & impDtForNum(_db.rmNullStr(.Rows(i)("TT_TANCYO_KBN")))    '単長区分
                SQL = SQL & N & ",ZW_MAKI_CD     = " & impDtForNum(_db.rmNullStr(.Rows(i)("TT_MAKI_CD")))
                SQL = SQL & N & ",ZW_HOSO_KBN    = " & impDtForStr(_db.rmNullStr(.Rows(i)("TT_HOSO_KBN")))      '包装区分
                SQL = SQL & N & ",ZW_SIYOUSYO_NO = " & impDtForStr(_db.rmNullStr(.Rows(i)("TT_SIYOUSYO_NO")))   '仕様書番号
                SQL = SQL & N & ",ZW_TENKAI_KBN  = " & impDtForNum(_db.rmNullStr(.Rows(i)("TT_TENKAI_KBN")))    '展開区分
                SQL = SQL & N & ",ZW_BBNKOUTEI   = " & impDtForStr(_db.rmNullStr(.Rows(i)("TT_KOUTEI")))        '指定工程
                SQL = SQL & N & ",ZW_HINSITU_KBN = " & impDtForNum(_db.rmNullStr(.Rows(i)("TT_HINSITU_KBN")))   '品質試験区分
                SQL = SQL & N & ",ZW_KEISAN_KBN  = " & impDtForNum(_db.rmNullStr(.Rows(i)("TT_KEISAN_KBN")))    '加工長計算区分
                SQL = SQL & N & ",ZW_TATIAI_UM   = " & impDtForNum(_db.rmNullStr(.Rows(i)("TT_TATIAI_UM")))     '立会有無
                'SQL = SQL & N & ",ZW_KONYU_SIYOU = " & impDtForStr(_db.rmNullStr(.Rows(i)("TT_KONYU_SIYOU")))  '購入品用仕様コード
                SQL = SQL & N & ",ZW_KAMOKU_CD   = " & impDtForNum(_db.rmNullStr(.Rows(i)("TT_KAMOKU_CD")))
                SQL = SQL & N & ",ZW_N_SO_SUU    = " & impDtForNum(_db.rmNullStr(.Rows(i)("TT_N_SO_SUU")))      '入庫本数 全体
                SQL = SQL & N & ",ZW_N_K_SUU     = " & impDtForNum(_db.rmNullStr(.Rows(i)("TT_N_K_SUU")))       '入庫本数 北日本本数
                SQL = SQL & N & ",ZW_N_SH_SUU    = " & impDtForNum(_db.rmNullStr(.Rows(i)("TT_N_SH_SUU")))      '入庫本数 住電日立本数
                SQL = SQL & N & ",ZW_HENKAN_FLG  = 1 "                                                          '変換ＤＢ存在フラグ
                SQL = SQL & N & ",UPDDATE        = TO_DATE('" & nowdt & "','YYYY/MM/DD HH24:MI:SS') "                                '更新日
                SQL = SQL & N & "WHERE ZW_KHINMEICD = " & impDtForStr(_db.rmNullStr(.Rows(i)("TT_KHINMEICD")))  '計画品名コード
                SQL = SQL & N & " AND  UPDNAME      = '" & UtilClass.getComputerName() & "'"
                _db.executeDB(SQL)
            Next
        End With

    End Sub

    '手配データ作成
    Public Sub insertTehaiRec(ByRef prmRefBar As UtilProgressBar)

        Dim SQL As String = ""
        SQL = SQL & N & "SELECT"
        SQL = SQL & N & " ZW_KHINMEICD,"
        SQL = SQL & N & " ZW_H_SIYOU_CD,"   ' 0：（品名コード）仕様コード
        SQL = SQL & N & " ZW_H_HIN_CD,"     ' 1：（品名コード）品種コード
        SQL = SQL & N & " ZW_H_SENSIN_CD,"  ' 2：（品名コード）線心数コード
        SQL = SQL & N & " ZW_H_SIZE_CD,"    ' 3：（品名コード）サイズコード
        SQL = SQL & N & " ZW_H_COLOR_CD,"   ' 4：（品名コード）色コード
        SQL = SQL & N & " ZW_FUKA_CD,"      ' 5：設計付加記号
        SQL = SQL & N & " ZW_LOT_SUU,"      ' 6：ロット数
        SQL = SQL & N & " ZW_LOT_CHO,"      ' 7：ロット長
        SQL = SQL & N & " ZW_HIN_DATA,"     ' 8：品名データ
        SQL = SQL & N & " ZW_SIZE_DATA,"    ' 9：サイズデータ
        SQL = SQL & N & " ZW_COLOR_DATA,"   '10：色データ
        SQL = SQL & N & " ZW_KIBOU_DATE,"   '11：希望年月日
        SQL = SQL & N & " ZW_NOUKI,"        '12：納期
        SQL = SQL & N & " ZW_TYUMONSAKI,"   '13：注文先
        SQL = SQL & N & " ZW_HINMEI,"       '14：品名
        SQL = SQL & N & " ZW_TEHAI_SUU,"    '15：手配数量(手配変換ＤＢ)
        SQL = SQL & N & " ZW_TANCYO,"       '16：製作単長
        SQL = SQL & N & " ZW_JYO_SUU,"      '17：条数
        SQL = SQL & N & " ZW_SYORI_KBN,"    '18：処理区分
        SQL = SQL & N & " ZW_TEHAI_KBN,"    '19：手配区分
        SQL = SQL & N & " ZW_SEISAKU_KBN,"  '20：製作区分
        SQL = SQL & N & " ZW_SEIZOU_BMN,"   '21：製造部門
        SQL = SQL & N & " ZW_TANCYO_KBN,"   '22：単長区分
        SQL = SQL & N & " ZW_MAKI_CD,"      '23：巻枠コード
        SQL = SQL & N & " ZW_HOSO_KBN,"     '24：包装区分
        SQL = SQL & N & " ZW_SIYOUSYO_NO,"  '25：仕様書№
        SQL = SQL & N & " ZW_TENKAI_KBN,"   '26：展開区分
        SQL = SQL & N & " ZW_BBNKOUTEI,"    '27：指定工程
        SQL = SQL & N & " ZW_HINSITU_KBN,"  '28：品質試験区分
        SQL = SQL & N & " ZW_KEISAN_KBN,"   '29：加工長計算
        SQL = SQL & N & " ZW_TATIAI_UM,"    '30：立会有無
        SQL = SQL & N & " ZW_KONYU_SIYOU,"  '31：購入品用仕様コード
        SQL = SQL & N & " ZW_KAMOKU_CD,"    '32：科目コード
        SQL = SQL & N & " ZW_HENKAN_FLG,"   '33：手配変換ＤＢ存在フラグ
        SQL = SQL & N & " UPDDATE,"         '34：更新日
        SQL = SQL & N & " ZW_N_SO_SUU,"     '35:入庫本数 全体
        SQL = SQL & N & " ZW_N_K_SUU,"      '36:入庫本数 北日本本数
        SQL = SQL & N & " ZW_N_SH_SUU "     '37入庫本数 住電日立本数
        '-->2010.12.22 add by takagi 
        SQL = SQL & N & ",TT_JUYOUCD"
        '<--2010.12.22 add by takagi 
        SQL = SQL & N & "FROM ZG610B_W10 "
        '-->2010.12.22 add by takagi 
        SQL = SQL & N & "LEFT JOIN M11KEIKAKUHIN ON ZG610B_W10.ZW_KHINMEICD = M11KEIKAKUHIN.TT_KHINMEICD"
        '<--2010.12.22 add by takagi 
        SQL = SQL & N & "WHERE ZW_HENKAN_FLG = 1 AND updname = '" & UtilClass.getComputerName() & "' "
        SQL = SQL & N & "ORDER BY"
        SQL = SQL & N & "  ZW_SEIZOU_BMN DESC,"
        SQL = SQL & N & "  ZW_H_HIN_CD,"
        SQL = SQL & N & "  ZW_H_SENSIN_CD,"
        SQL = SQL & N & "  ZW_H_SIZE_CD,"
        SQL = SQL & N & "  ZW_H_SIYOU_CD,"
        SQL = SQL & N & "  ZW_H_COLOR_CD,"
        SQL = SQL & N & "  ZW_TANCYO,"
        SQL = SQL & N & "  ZW_KIBOU_DATE"
        Dim ds As DataSet = _db.selectDB(SQL, RS)
        prmRefBar.maxVal = ds.Tables(RS).Rows.Count - 1

        '-->2010.12.22 chg by takagi #24
        'Dim sYear As String = lblSyoriDate.Text.Replace("/", "").Substring(0, 4)
        'Dim sMonth As String = lblSyoriDate.Text.Replace("/", "").Substring(4, 2)
        Dim sYear As String = lblKeikakuDate.Text.Replace("/", "").Substring(0, 4)
        Dim sMonth As String = lblKeikakuDate.Text.Replace("/", "").Substring(4, 2)
        '<--2010.12.22 chg by takagi #24

        '手配Noカウンタ初期化
        '-->2010.12.22 chg by takagi #24
        'Dim lT_Cnt_K As Integer = lT_Cnt_K = 700      '購入品は700番台から使用する。
        'Dim lT_Cnt_O As Integer = lT_Cnt_O = 0        '以外は通常
        Dim lT_Cnt_K As Integer = 700      '購入品は700番台から使用する。
        Dim lT_Cnt_O As Integer = 0        '以外は通常
        '<--2010.12.22 chg by takagi #24

        'Const lH03_NAISAKU As Integer = 1      '製作区分：内作
        Const lH03_GAICHUU As Integer = 2      '製作区分：外注
        Const lH03_KOUNYUU As Integer = 3      '製作区分：購入
        Const lZM03_DENPYO_KBN_OCR As Integer = 1     '伝票区分：OCR（ｼｽﾃﾑ固定）

        Dim nowdt As String = Format(Now, "yyyy/MM/dd HH:mm:ss")

        With ds.Tables(RS)

            For i As Integer = 0 To .Rows.Count - 1
                prmRefBar.value = i

                '製作区分を保存
                Dim lSeiKbn As Integer = _db.rmNullInt(.Rows(i)("ZW_SEISAKU_KBN"))

                '手配No作成
                Dim sTehaiNO As String = ""
                If lSeiKbn = lH03_KOUNYUU Then
                    '購入品の場合
                    lT_Cnt_K = lT_Cnt_K + 1
                    sTehaiNO = sMonth & Format(lT_Cnt_K, "000")
                Else
                    '購入品以外の場合（内作・外注）
                    lT_Cnt_O = lT_Cnt_O + 1
                    sTehaiNO = sMonth & Format(lT_Cnt_O, "000")
                End If

                Dim sTokki1 As String = "ﾆｭｳｺﾌﾘﾜｹ "
                sTokki1 = sTokki1 & "K:" & _db.rmNullInt(.Rows(i)("ZW_N_K_SUU")) & " "
                sTokki1 = sTokki1 & "S:" & _db.rmNullInt(.Rows(i)("ZW_N_SH_SUU")) & " "
                sTokki1 = (sTokki1 & Space(22)).Substring(0, 22)

                '登録
                SQL = ""
                SQL = SQL & N & "INSERT INTO T51TEHAI"
                SQL = SQL & N & " (TEHAI_NO,"        '手配№
                SQL = SQL & N & " SYORI_YM,"         '処理年月
                SQL = SQL & N & " SYORI_KBN,"        '処理区分
                SQL = SQL & N & " KIBOU_DATE,"       '希望年月日
                'SQL = SQL & N & " NOUKI,"            '納期
                SQL = SQL & N & " TEHAI_KBN,"        '手配区分
                SQL = SQL & N & " SEISAKU_KBN,"      '製作区分
                SQL = SQL & N & " SEIZOU_BMN,"       '製造部門
                SQL = SQL & N & " DENPYOK,"          '伝票区分
                SQL = SQL & N & " TYUMONSAKI,"       '注文先
                SQL = SQL & N & " HINMEI_CD, "       '品名CD
                SQL = SQL & N & " H_SIYOU_CD,"       '（品名コード）仕様コード
                SQL = SQL & N & " H_HIN_CD,"         '（品名コード）品種コード
                SQL = SQL & N & " H_SENSIN_CD,"      '（品名コード）線心数コード
                SQL = SQL & N & " H_SIZE_CD,"        '（品名コード）サイズコード
                SQL = SQL & N & " H_COLOR_CD,"       '（品名コード）色コード
                SQL = SQL & N & " FUKA_CD,"          '設計付加記号
                SQL = SQL & N & " HINMEI,"           '品名
                SQL = SQL & N & " HIN_DATA,"         '品名データ
                SQL = SQL & N & " SIZE_DATA,"        'サイズデータ
                SQL = SQL & N & " COLOR_DATA,"       '色データ
                SQL = SQL & N & " TEHAI_SUU,"        '手配数量
                SQL = SQL & N & " TANCYO_KBN,"       '単長区分
                SQL = SQL & N & " TANCYO,"           '製作単長
                SQL = SQL & N & " JYOSU,"            '条数
                SQL = SQL & N & " MAKI_CD,"          '巻枠コード
                SQL = SQL & N & " HOSO_KBN,"         '包装区分
                SQL = SQL & N & " SIYOUSYO_NO,"      '仕様書№
                SQL = SQL & N & " TOKKI,"            '特記事項
                SQL = SQL & N & " TENKAI_KBN,"       '展開区分
                SQL = SQL & N & " BBNKOUTEI,"        '指定工程
                SQL = SQL & N & " HINSITU_KBN,"      '品質試験区分
                SQL = SQL & N & " KEISAN_KBN,"       '加工長計算
                SQL = SQL & N & " TATIAI_UM,"        '立会有無
                'SQL = SQL & N & " KONYU_SIYOU,"      '購入品用仕様コード
                SQL = SQL & N & " KAMOKU_CD,"        '科目コード
                SQL = SQL & N & " N_SO_SUU,"         '入庫本数
                SQL = SQL & N & " N_K_SUU,"          '北日本本数
                SQL = SQL & N & " N_SH_SUU,"         '住電日立本数
                '-->2010.12.22 add by takagi 
                SQL = SQL & N & " JUYOUCD,"
                '<--2010.12.22 add by takagi 
                SQL = SQL & N & " UPDNAME,"          '更新者
                SQL = SQL & N & " UPDDATE)"          '更新日
                SQL = SQL & N & " VALUES"
                SQL = SQL & N & " ("
                SQL = SQL & N & "'" & sTehaiNO & "',"                                           '手配№
                SQL = SQL & N & "'" & sYear & sMonth & "',"                                     '処理年月
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_SYORI_KBN"))) & ","      '処理区分
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_KIBOU_DATE"))) & ","     '希望年月日
                'SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_NOUKI"))) & ","          '納期
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_TEHAI_KBN"))) & ","      '手配区分
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_SEISAKU_KBN"))) & ","    '製作区分
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_SEIZOU_BMN"))) & ","     '製造部門
                SQL = SQL & N & lZM03_DENPYO_KBN_OCR & ","                                      '伝票区分
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_TYUMONSAKI"))) & ","     '注文先
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_KHINMEICD"))) & ","      '品名コード
                SQL = SQL & N & impDtForStr((_db.rmNullStr(.Rows(i)("ZW_H_SIYOU_CD")) & "  ").Substring(0, 2)) & ","     '（品名コード）仕様コード
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_H_HIN_CD"))) & ","       '（品名コード）品種コード
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_H_SENSIN_CD"))) & ","    '（品名コード）線心数コード
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_H_SIZE_CD"))) & ","      '（品名コード）サイズコード
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_H_COLOR_CD"))) & ","     '（品名コード）色コード
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_FUKA_CD"))) & ","        '設計付加記号
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_HINMEI"))) & ","         '品名
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_HIN_DATA"))) & ","       '品名データ
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_SIZE_DATA"))) & ","      'サイズデータ
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_COLOR_DATA"))) & ","     '色データ
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_LOT_CHO"))) & ","        '手配数量
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_TANCYO_KBN"))) & ","     '単長区分
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_TANCYO"))) & ","         '製作単長
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_JYO_SUU"))) & ","        '条数
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_MAKI_CD"))) & ","
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_HOSO_KBN"))) & ","       '包装区分
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_SIYOUSYO_NO"))) & ","    '仕様書№
                If (lSeiKbn = lH03_GAICHUU) Or (lSeiKbn = lH03_KOUNYUU) Then '製作区分：外注(2)購入(3)       '特記事項（※条件あり）
                    '外注・購入品の場合、科目コードを付加（３段目／45ﾊﾞｲﾄ～）
                    SQL = SQL & N & "'" & sTokki1 & Space(22) & Format(_db.rmNullStr(.Rows(i)("ZW_KAMOKU_CD")), "000000") & "',"
                Else
                    SQL = SQL & N & "'" & sTokki1 & "',"
                End If
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_TENKAI_KBN"))) & ","     '展開区分
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_BBNKOUTEI"))) & ","      '指定工程
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_HINSITU_KBN"))) & ","    '品質試験区分
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_KEISAN_KBN"))) & ","     '加工長計算
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_TATIAI_UM"))) & ","      '立会有無
                'SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("ZW_KONYU_SIYOU"))) & ","    '購入品用仕様コード
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_KAMOKU_CD"))) & ","
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_N_SO_SUU"))) & ","       '入庫本数全体
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_N_K_SUU"))) & ","        '北日本本数
                SQL = SQL & N & impDtForNum(_db.rmNullStr(.Rows(i)("ZW_N_SH_SUU"))) & ","       '住電日立本数
                '-->2010.12.22 add by takagi 
                SQL = SQL & N & impDtForStr(_db.rmNullStr(.Rows(i)("TT_JUYOUCD"))) & ","
                '<--2010.12.22 add by takagi 
                SQL = SQL & N & "'" & UtilClass.getComputerName() & "',"
                SQL = SQL & N & "TO_DATE('" & nowdt & "','YYYY/MM/DD HH24:MI:SS'))"             '更新日
                _db.executeDB(SQL)
            Next
        End With

    End Sub

    'insert文用文字列編集(文字用)
    Private Function impDtForStr(ByVal prmVal As String) As String
        If "".Equals(prmVal) Then
            Return "NULL"
        Else
            Return "'" & prmVal & "'"
        End If
    End Function

    'insert文用文字列編集(数値用)
    Private Function impDtForNum(ByVal prmVal As String) As String
        If "".Equals(prmVal) Then
            Return "NULL"
        Else
            Return "" & prmVal & ""
        End If
    End Function

    'パターンマトリクスに従った出来日の取得
    Private Function getShuttaibi(ByRef prmRefPattern As abcType, ByRef prmRefShuttaiHash As Hashtable, _
                                  ByRef prmRefABC As String, ByRef prmRefLotNum As Integer) As String()
        Dim ret() As String = {}
        Dim i As Integer = 0

        '１～６ロットは各パターンに従う。
        '６超の場合は６ロット単位に除算し、６ロット分はパターン６の要領で展開する
        'その余り分はパターン１～５に沿って展開する

        Do

            If "S".Equals(prmRefABC) Then
                '貯蔵品
                If (prmRefLotNum - (i * 6)) \ 6 > 0 Then
                    Call getShuttaibiAry(prmRefPattern.chozoHin.shuttaiBi6, prmRefShuttaiHash, ret)
                Else
                    Select Case (prmRefLotNum - (i * 6)) Mod 6
                        Case 1 : Call getShuttaibiAry(prmRefPattern.chozoHin.shuttaiBi1, prmRefShuttaiHash, ret)
                        Case 2 : Call getShuttaibiAry(prmRefPattern.chozoHin.shuttaiBi2, prmRefShuttaiHash, ret)
                        Case 3 : Call getShuttaibiAry(prmRefPattern.chozoHin.shuttaiBi3, prmRefShuttaiHash, ret)
                        Case 4 : Call getShuttaibiAry(prmRefPattern.chozoHin.shuttaiBi4, prmRefShuttaiHash, ret)
                        Case 5 : Call getShuttaibiAry(prmRefPattern.chozoHin.shuttaiBi5, prmRefShuttaiHash, ret)
                        Case 0 : Call getShuttaibiAry(prmRefPattern.chozoHin.shuttaiBi6, prmRefShuttaiHash, ret)
                    End Select
                End If
            Else
                '通常
                If (prmRefLotNum - (i * 6)) \ 6 > 0 Then
                    Call getShuttaibiAry(prmRefPattern.nomal.shuttaiBi6, prmRefShuttaiHash, ret)
                Else
                    Select Case (prmRefLotNum - (i * 6)) Mod 6
                        Case 1 : Call getShuttaibiAry(prmRefPattern.nomal.shuttaiBi1, prmRefShuttaiHash, ret)
                        Case 2 : Call getShuttaibiAry(prmRefPattern.nomal.shuttaiBi2, prmRefShuttaiHash, ret)
                        Case 3 : Call getShuttaibiAry(prmRefPattern.nomal.shuttaiBi3, prmRefShuttaiHash, ret)
                        Case 4 : Call getShuttaibiAry(prmRefPattern.nomal.shuttaiBi4, prmRefShuttaiHash, ret)
                        Case 5 : Call getShuttaibiAry(prmRefPattern.nomal.shuttaiBi5, prmRefShuttaiHash, ret)
                        Case 0 : Call getShuttaibiAry(prmRefPattern.nomal.shuttaiBi6, prmRefShuttaiHash, ret)
                    End Select
                End If
            End If
            i += 1

        Loop Until (prmRefLotNum <= 6) Or (i > IIf((prmRefLotNum Mod 6) <> 0, (prmRefLotNum \ 6), (prmRefLotNum \ 6) - 1))

        Return ret
    End Function

    '出来日配列の取得
    Private Sub getShuttaibiAry(ByVal prmBit As String, ByRef prmRefShuttaiHash As Hashtable, ByRef prmRefTarget() As String)

        For i As Integer = 0 To prmBit.Length - 1
            If "1".Equals(prmBit.Substring(i, 1)) Then
                ReDim Preserve prmRefTarget(UBound(prmRefTarget) + 1)
                prmRefTarget(UBound(prmRefTarget)) = prmRefShuttaiHash.Item(i + 1).ToString
            End If
        Next

    End Sub

End Class