'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）製作手配ＤＢ登録
'    （フォームID）ZG640B_SeisakuTehaiDB
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
Public Class ZG640B_SeisakuTehaiDB
    Inherits System.Windows.Forms.Form

    '-------------------------------------------------------------------------------
    '   構造体定義
    '-------------------------------------------------------------------------------

    '-------------------------------------------------------------------------------
    '   定数定義
    '-------------------------------------------------------------------------------
    Private Const RS As String = "RecSet"                       'データセットテーブル名
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const PGID As String = "ZG640B"


    Private Const lZM05_SEISAKU_IRAI_NO As Integer = 0     '製作依頼№：０（ｼｽﾃﾑ固定）
    Private Const lMZ05_SENDFLG_OFF As Integer = 0         '送信済フラグ：未送信
    Private Const lMZ05_SENDFLG_ON As Integer = 1          '送信済フラグ：送信済
    Private Const lMZ05_KOUJYOK_NOTHING As Integer = 0     '工場区分：なし
    Private Const lMZ05_KOUJYOK_DENRYOKU  As Integer = 1    '工場区分：電力
    Private Const lMZ05_KOUJYOK_JYOUHOU As Integer = 2     '工場区分：情通
    Private Const lMZ05_SEKKEI_MST_EXIST As Integer = 0    '設計マスタ：有り（ｼｽﾃﾑ固定）
    Private Const lMZ05_ZAIRYO_MST_EXIST As Integer = 1    '材料マスタ：有り（ｼｽﾃﾑ固定）
    Private Const lMZ05_KOUTEI_MST_EXIST As Integer = 1    '工程マスタ：有り（ｼｽﾃﾑ固定）
    Private Const lMZ05_TOROKU_KBN_TOROKU As Integer = 1   '登録区分：登録（ｼｽﾃﾑ固定）
    Private Const sMZ05_TEHAI_HAKKOU_SYA As String = "在庫手配"   '手配発行者：在庫手配（ｼｽﾃﾑ固定）
    Private Const sMZ05_SEKKEI_KAKUNIN_SYA As String = "在庫手配" '設計票確認者：在庫手配（ｼｽﾃﾑ固定）
    Private Const sMZ05_ZAIRYO_KAKUNIN_SYA As String = "在庫手配" '材料票確認者：在庫手配（ｼｽﾃﾑ固定）
    Private Const sMZ05_KOUTEI_KAKUNIN_SYA As String = "在庫手配" '工程票確認者：在庫手配（ｼｽﾃﾑ固定）
    Private Const sMZ05_TEHAI_TOUROKU_SYA As String = "在庫手配"  '手配登録者：在庫手配（ｼｽﾃﾑ固定）

    '-------------------------------------------------------------------------------
    '   変数定義
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu
    Private _updFlg As Boolean = False  '更新可否

    '-------------------------------------------------------------------------------
    'コンストラクタ（Privateにして、外からは呼べないようにする）
    '-------------------------------------------------------------------------------
    Private Sub New()
        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

    End Sub

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
    Private Sub ZG640B_SeisakuTehaiDB_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
                lblJikkouDate.Text = ZC110M_Menu.NON_EXECUTE
                lblZTehaiData.Text = ""
                lblKihon.Text = ""
                lblTantyo.Text = ""
                lblDaitai.Text = ""
            Else
                '履歴あり
                lblJikkouDate.Text = _db.rmNullDate(ds.Tables(RS).Rows(0)("SDATEEND"))
                lblZTehaiData.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU1")), "#,##0")
                lblKihon.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU2")), "#,##0")
                lblTantyo.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU3")), "#,##0")
                lblDaitai.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU4")), "#,##0")
            End If

            '今回実行情報の表示
            '2011/01/28 chg start Sugawara #95
            'lblKTehaiData.Text = Format(_db.rmNullInt(_db.selectDB("select count(*) CNT from T51TEHAI ", RS).Tables(RS).Rows(0)("CNT")), "#,##0")
            lblKTehaiData.Text = Format(_db.rmNullInt(_db.selectDB("select count(*) CNT from T51TEHAI where GAI_FLG is null ", RS).Tables(RS).Rows(0)("CNT")), "#,##0")
            '2011/01/28 chg end Sugawara #95

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
                    pb.jobName = "製作手配データを作成しています。"
                    pb.oneStep = 1

                    pb.status = "作成中"
                    Dim outputCnt As Integer = 0
                    Dim kihonCnt As Integer = 0
                    Dim danchoCnt As Integer = 0
                    Dim daitaiCnt As Integer = 0

                    _db.beginTran()
                    Try
                        Call insertSeisakuDB(kihonCnt, danchoCnt, daitaiCnt, pb)

                        pb.status = "ステータス変更中・・・"
                        ed = Now                    '処理終了日時
                        _parentForm.updateSeigyoTbl(PGID, True, st, ed)

                        pb.status = "実行履歴作成"
                        Call insertRireki(kihonCnt, danchoCnt, daitaiCnt, st, ed)                                  '2-1 実行履歴格納
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
    Private Sub insertRireki(ByVal prmKihonCnt As Integer, ByVal prmTanchoCnt As Integer, ByVal prmDaitaiCnt As Integer, ByVal prmStDt As Date, ByVal prmEdDt As Date)

        Dim sql As String = ""
        sql = sql & N & "insert into T91RIREKI "
        sql = sql & N & "( "
        sql = sql & N & " SNENGETU "   '処理年月
        sql = sql & N & ",KNENGETU "   '計画年月
        sql = sql & N & ",PGID "       '機能ID
        sql = sql & N & ",SDATESTART " '処理開始日時
        sql = sql & N & ",SDATEEND "   '処理終了日時
        sql = sql & N & ",KENNSU1 "    '実行件数
        sql = sql & N & ",KENNSU2 "    '基本実行件数
        sql = sql & N & ",KENNSU3 "    '単長実行件数
        sql = sql & N & ",KENNSU4 "    '代替実行件数
        sql = sql & N & ",UPDNAME "    '最終更新者
        sql = sql & N & ",UPDDATE "    '最終更新日
        sql = sql & N & ")values( "
        sql = sql & N & "  '" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "' "                             '処理年月
        sql = sql & N & ", '" & _db.rmSQ(lblKeikakuDate.Text.Replace("/", "")) & "' "                           '計画年月
        sql = sql & N & ", '" & _db.rmSQ(PGID) & "' "                                                           '機能ID
        sql = sql & N & ",to_date('" & Format(prmStDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '処理開始日時
        sql = sql & N & ",to_date('" & Format(prmEdDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '処理終了日時
        sql = sql & N & ", " & CLng(lblKTehaiData.Text) & " "                                                  '入力件数
        sql = sql & N & ", " & prmKihonCnt & " "                                                  '入力件数
        sql = sql & N & ", " & prmTanchoCnt & " "                                                  '入力件数
        sql = sql & N & ", " & prmDaitaiCnt & " "                                                  '入力件数
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "                                     '最終更新者
        sql = sql & N & ",sysdate "                                                                             '最終更新日
        sql = sql & N & ") "
        _db.executeDB(sql)

    End Sub

    '-------------------------------------------------------------------------------
    '　 製作手配ＤＢ登録
    '   （処理概要）手配ワークＤＢのデータを製作手配ＤＢに登録する。
    '              [対象外フラグＯＮ(1)のデータは除く。]
    '-------------------------------------------------------------------------------
    Public Sub insertSeisakuDB(ByRef prmKihonCnt As Integer, ByRef prmTanchoCnt As Integer, ByRef prmDaitaiCnt As Integer, ByRef prmRefBar As UtilProgressBar)

        Dim SQL As String = ""
        SQL = SQL & N & "SELECT "
        SQL = SQL & N & " TEHAI_NO"         ' 0：手配№
        SQL = SQL & N & ",SYORI_YM"         ' 1：処理年月
        SQL = SQL & N & ",SYORI_KBN"        ' 2：処理区分
        SQL = SQL & N & ",KIBOU_DATE"       ' 3：希望年月日
        'SQL = SQL & N & ",NOUKI"            ' 4：納期
        SQL = SQL & N & ",TEHAI_KBN"        ' 5：手配区分
        SQL = SQL & N & ",SEISAKU_KBN"      ' 6：製作区分
        SQL = SQL & N & ",SEIZOU_BMN"       ' 7：製造部門
        SQL = SQL & N & ",DENPYOK"          ' 8：伝票区分
        SQL = SQL & N & ",TYUMONSAKI"       ' 9：注文先
        SQL = SQL & N & ",H_SIYOU_CD"       '10：（品名コード）仕様コード
        SQL = SQL & N & ",H_HIN_CD"         '11：（品名コード）品種コード
        SQL = SQL & N & ",H_SENSIN_CD"      '12：（品名コード）線心数コード
        SQL = SQL & N & ",H_SIZE_CD"        '13：（品名コード）サイズコード
        SQL = SQL & N & ",H_COLOR_CD"       '14：（品名コード）色コード
        SQL = SQL & N & ",FUKA_CD"          '15：設計付加記号
        SQL = SQL & N & ",HINMEI"           '16：品名
        SQL = SQL & N & ",TEHAI_SUU"        '17：手配数量
        SQL = SQL & N & ",TANCYO_KBN"       '18：単長区分
        SQL = SQL & N & ",TANCYO"           '19：製作単長
        SQL = SQL & N & ",JYOSU"            '20：条数
        SQL = SQL & N & ",MAKI_CD"          '21：巻枠コード
        SQL = SQL & N & ",HOSO_KBN"         '22：包装区分
        SQL = SQL & N & ",SIYOUSYO_NO"      '23：仕様書№
        SQL = SQL & N & ",TOKKI"            '24：特記事項
        SQL = SQL & N & ",BIKO"             '25：備考
        SQL = SQL & N & ",HENKO"            '26：変更内容
        SQL = SQL & N & ",TENKAI_KBN"       '27：展開区分
        SQL = SQL & N & ",BBNKOUTEI"        '28：指定工程
        SQL = SQL & N & ",HINSITU_KBN"      '29：品質試験区分
        SQL = SQL & N & ",KEISAN_KBN"       '30：加工長計算
        SQL = SQL & N & ",TATIAI_UM"        '31：立会有無
        SQL = SQL & N & ",TACIAIBI"         '32：立会予定日
        SQL = SQL & N & ",SEISEKI"          '33：成績書
        SQL = SQL & N & ",MYTANCYO"         '34：持込余長（単長毎）
        SQL = SQL & N & ",MYLOT"            '35：持込余長（ロット毎）
        SQL = SQL & N & ",TYTANCYO"         '36：立会余長（単長毎）
        SQL = SQL & N & ",TYLOT"            '37：立会余長（ロット毎）
        SQL = SQL & N & ",SYTANCYO"         '38：指定社検余長（単長毎）
        SQL = SQL & N & ",SYLOT"            '39：指定社検余長（ロット毎）
        'SQL = SQL & N & ",HYOJYUNC_1"       '40：標準工程コード_1
        'SQL = SQL & N & ",KOUTEIC_1"        '41：工程コード_1
        'SQL = SQL & N & ",KOUTEIJ_1"        '42：工程順位_1
        'SQL = SQL & N & ",TEIIN_1"          '43：定員_1
        'SQL = SQL & N & ",DANDORI_1"        '44：段取_1
        'SQL = SQL & N & ",KIJYUN_1"         '45：基準出来高_1
        'SQL = SQL & N & ",HYOJYUNC_2"       '46：標準工程コード_2
        'SQL = SQL & N & ",KOUTEIC_2"        '47：工程コード_2
        'SQL = SQL & N & ",KOUTEIJ_2"        '48：工程順位_2
        'SQL = SQL & N & ",TEIIN_2"          '49：定員_2
        'SQL = SQL & N & ",DANDORI_2"        '50：段取_2
        'SQL = SQL & N & ",KIJYUN_2"         '51：基準出来高_2
        'SQL = SQL & N & ",HYOJYUNC_3"       '52：標準工程コード_3
        'SQL = SQL & N & ",KOUTEIC_3"        '53：工程コード_3
        'SQL = SQL & N & ",KOUTEIJ_3"        '54：工程順位_3
        'SQL = SQL & N & ",TEIIN_3"          '55：定員_3
        'SQL = SQL & N & ",DANDORI_3"        '56：段取_3
        'SQL = SQL & N & ",KIJYUN_3"         '57：基準出来高_3
        'SQL = SQL & N & ",HYOJYUNC_4"       '58：標準工程コード_4
        'SQL = SQL & N & ",KOUTEIC_4"        '59：工程コード_4
        'SQL = SQL & N & ",KOUTEIJ_4"        '60：工程順位_4
        'SQL = SQL & N & ",TEIIN_4"          '61：定員_4
        'SQL = SQL & N & ",DANDORI_4"        '62：段取_4
        'SQL = SQL & N & ",KIJYUN_4"         '63：基準出来高_4
        'SQL = SQL & N & ",HYOJYUNC_5"       '64：標準工程コード_5
        'SQL = SQL & N & ",KOUTEIC_5"        '65：工程コード_5
        'SQL = SQL & N & ",KOUTEIJ_5"        '66：工程順位_5
        'SQL = SQL & N & ",TEIIN_5"          '67：定員_5
        'SQL = SQL & N & ",DANDORI_5"        '68：段取_5
        'SQL = SQL & N & ",KIJYUN_5"         '69：基準出来高_5
        SQL = SQL & N & ",HIN_DATA"         '70：品名データ
        SQL = SQL & N & ",SIZE_DATA"        '71：サイズデータ
        SQL = SQL & N & ",COLOR_DATA"       '72：色データ
        SQL = SQL & N & ",UPDDATE "         '73：更新日
        SQL = SQL & N & "FROM T51TEHAI "
        '2011/01/28 add start Sugawara #95
        SQL = SQL & N & "WHERE GAI_FLG IS NULL " '(初期値：NULL、対象外：1) 対象外データを除く。
        '2011/01/28 add end Sugawara #95
        SQL = SQL & N & "ORDER BY TEHAI_NO"
        Dim ds As DataSet = _db.selectDB(SQL, RS)

        For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1

            '製作手配ＤＢ（基本部）更新文字列作成
            Call EditSQL_Tehai1(ds.Tables(RS).Rows(i))
            '2011/01/28 add start Sugawara #96
            prmKihonCnt = prmKihonCnt + 1
            '2011/01/28 add end Sugawara #96

            '製作手配ＤＢ（単長部）更新文字列作成
            Call insertTehai2(ds.Tables(RS).Rows(i))
            '2011/01/28 add start Sugawara #96
            prmTanchoCnt = prmTanchoCnt + 1
            '2011/01/28 add end Sugawara #96

            'For lDCnt As Integer = 1 To 5
            '    If _db.rmNullInt(ds.Tables(RS).Rows(i)("KOUTEIJ_" & lDCnt)) <> 0 Then
            '        '製作手配ＤＢ（代替部）更新文字列作成
            '        Call insertTehai3(ds.Tables(RS).Rows(i), lDCnt)
            '        2011/01/28 add start Sugawara #96
            '        prmDaitaiCnt = prmDaitaiCnt + 1
            '        2011/01/28 add end Sugawara #96
            '    End If
            'Next
        Next

    End Sub

    '-------------------------------------------------------------------------------
    '　 製作手配ＤＢ追加ＳＱＬ文作成（基本部）
    '   （処理概要）製作手配ＤＢ（基本部）に追加するＳＱＬ文を編集する。
    '-------------------------------------------------------------------------------
    Private Sub EditSQL_Tehai1(ByVal prmRow As DataRow)

        '品名コード編集
        Dim sHinCD As String = ""
        sHinCD += UtilClass.setDataLen(_db.rmNullStr(prmRow("H_SIYOU_CD")), 2)
        sHinCD += UtilClass.setDataLen(_db.rmNullStr(prmRow("H_HIN_CD")), 3)
        sHinCD += UtilClass.setDataLen(_db.rmNullStr(prmRow("H_SENSIN_CD")), 3)
        sHinCD += UtilClass.setDataLen(_db.rmNullStr(prmRow("H_SIZE_CD")), 2)
        sHinCD += UtilClass.setDataLen(_db.rmNullStr(prmRow("H_COLOR_CD")), 3)

        '注文品名編集
        Dim sOdrHinmei As String = ""
        sOdrHinmei += UtilClass.setDataLen(_db.rmNullStr(prmRow("HIN_DATA")), 13)
        sOdrHinmei += UtilClass.setDataLen(_db.rmNullStr(prmRow("SIZE_DATA")), 8)
        sOdrHinmei += UtilClass.setDataLen(_db.rmNullStr(prmRow("COLOR_DATA")), 2)

        '製造部門→工場区分変換
        Dim lFctKBN As Long
        Select Case _db.rmNullInt(prmRow("SEIZOU_BMN"))
            Case 1 : lFctKBN = lMZ05_KOUJYOK_JYOUHOU '通信
            Case 3 : lFctKBN = lMZ05_KOUJYOK_DENRYOKU '電力
            Case Else : lFctKBN = lMZ05_KOUJYOK_NOTHING 'その他（エラー）
        End Select

        'システム日取得
        Dim sEntDate As String = CStr(Format(Now, "yyyyMMdd"))

        Dim SQL As String = ""
        SQL = SQL & N & "INSERT INTO IN_TEHAI1_TB"
        SQL = SQL & N & "("
        SQL = SQL & N & " IT1_TEHAINO,"     ' 1：手配№
        SQL = SQL & N & " IT1_IRAINO,"      ' 2：製作依頼№
        SQL = SQL & N & " IT1_SYORIK,"      ' 3：処理区分
        SQL = SQL & N & " IT1_TEHAIK,"      ' 4：手配区分
        SQL = SQL & N & " IT1_SEISAKK,"     ' 5：製作区分
        SQL = SQL & N & " IT1_KOUJYOK,"     ' 6：工場区分
        SQL = SQL & N & " IT1_DENPYOK,"     ' 7：伝票区分
        SQL = SQL & N & " IT1_DEKIBI,"      ' 8：希望出来日
        SQL = SQL & N & " IT1_NOUKI,"       ' 9：納期
        SQL = SQL & N & " IT1_KYAKSAKI,"    '10：注文先
        SQL = SQL & N & " IT1_HINMEIC,"     '11：品名コード
        SQL = SQL & N & " IT1_FUKAC,"       '12：設計付加記号
        SQL = SQL & N & " IT1_KNAME,"       '13：注文品名
        SQL = SQL & N & " IT1_SNAME,"       '14：設計票品名
        SQL = SQL & N & " IT1_ZNAME,"       '15：材料票品名
        SQL = SQL & N & " IT1_SIYOSYONO,"   '16：仕様書番号
        SQL = SQL & N & " IT1_SURYO,"       '17：手配数量
        SQL = SQL & N & " IT1_TANCYOK,"     '18：単長区分
        SQL = SQL & N & " IT1_TENKAIK,"     '19：展開区分
        SQL = SQL & N & " IT1_BBNKOTEI,"    '20：部分展開指定工程
        SQL = SQL & N & " IT1_HINSITUK,"    '21：品質試験区分
        SQL = SQL & N & " IT1_KEISANK,"     '22：加工長計算区分
        SQL = SQL & N & " IT1_TACIAIUM,"    '23：立会有無
        SQL = SQL & N & " IT1_TACIAIBI,"    '24：立会予定日
        SQL = SQL & N & " IT1_SEISEKI,"     '25：成績書
        SQL = SQL & N & " IT1_MYTANCYO,"    '26：持込余長（単長毎）
        SQL = SQL & N & " IT1_MYLOT,"       '27：持込余長（ロット毎）
        SQL = SQL & N & " IT1_TYTANCYO,"    '28：立会余長（単長毎）
        SQL = SQL & N & " IT1_TYLOT,"       '29：立会余長（ロット毎）
        SQL = SQL & N & " IT1_SYTANCYO,"    '30：指定社検長（単長毎）
        SQL = SQL & N & " IT1_SYLOT,"       '31：指定社検長（ロット毎）
        SQL = SQL & N & " IT1_TOKKI,"       '32：特記事項
        SQL = SQL & N & " IT1_BIKO,"        '33：備考
        SQL = SQL & N & " IT1_HENKO,"       '34：変更内容
        SQL = SQL & N & " IT1_SMASTUM,"     '35：設計マスタ有無
        SQL = SQL & N & " IT1_ZMASTUM,"     '36：材料マスタ有無
        SQL = SQL & N & " IT1_KMASTUM,"     '37：工程マスタ有無
        SQL = SQL & N & " IT1_TOROKK,"      '38：登録区分
        SQL = SQL & N & " IT1_HAKKOSYA,"    '39：手配発行者
        SQL = SQL & N & " IT1_HAKKOBI,"     '40：手配発行日
        SQL = SQL & N & " IT1_SKNSYA,"      '41：設計票確認者
        SQL = SQL & N & " IT1_SKNBI,"       '42：設計票確認日
        SQL = SQL & N & " IT1_ZKNSYA,"      '43：材料票確認者
        SQL = SQL & N & " IT1_ZKNBI,"       '44：材料票確認日
        SQL = SQL & N & " IT1_KKNSYA,"      '45：工程票確認者
        SQL = SQL & N & " IT1_KKNBI,"       '46：工程票確認日
        SQL = SQL & N & " IT1_TOROKSYA,"    '47：手配登録者
        SQL = SQL & N & " IT1_TOROKBI,"     '48：手配登録日
        SQL = SQL & N & " IT1_SENDFLG"      '49：送信済フラグ
        SQL = SQL & N & ")"
        SQL = SQL & N & " VALUES("
        SQL = SQL & N & _db.rmNullInt(prmRow("TEHAI_NO")) & ","                 ' 1：手配№
        SQL = SQL & N & lZM05_SEISAKU_IRAI_NO & ","                             ' 2：製作依頼№
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("SYORI_KBN"))) & ","   ' 3：処理区分
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("TEHAI_KBN"))) & ","   ' 4：手配区分
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("SEISAKU_KBN"))) & "," ' 5：製作区分
        SQL = SQL & N & lFctKBN & ","                                           ' 6：工場区分
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("DENPYOK"))) & ","     ' 7：伝票区分
        SQL = SQL & N & impDtForStr(_db.rmNullStr(prmRow("KIBOU_DATE"))) & ","  ' 8：希望出来日
        'SQL = SQL & N & impDtForStr(_db.rmNullStr(prmRow("NOUKI"))) & ","       ' 9：納期
        SQL = SQL & N & impDtForStr("") & ","       ' 9：納期
        SQL = SQL & N & impDtForStr(_db.rmNullStr(prmRow("TYUMONSAKI"))) & ","  '10：注文先
        SQL = SQL & N & impDtForStr(sHinCD) & ","                               '11：品名コード
        SQL = SQL & N & impDtForStr(_db.rmNullStr(prmRow("FUKA_CD"))) & ","     '12：設計付加記号
        SQL = SQL & N & impDtForStr(sOdrHinmei) & ","                           '13：注文品名
        SQL = SQL & N & impDtForStr(_db.rmNullStr(prmRow("HINMEI"))) & ","      '14：設計票品名
        SQL = SQL & N & impDtForStr(_db.rmNullStr(prmRow("HINMEI"))) & ","      '15：材料票品名
        SQL = SQL & N & impDtForStr(_db.rmNullStr(prmRow("SIYOUSYO_NO"))) & "," '16：仕様書番号
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("TEHAI_SUU"))) & ","   '17：手配数量
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("TANCYO_KBN"))) & ","  '18：単長区分
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("TENKAI_KBN"))) & ","  '19：展開区分
        SQL = SQL & N & impDtForStr(_db.rmNullStr(prmRow("BBNKOUTEI"))) & ","   '20：部分展開指定工程
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("HINSITU_KBN"))) & "," '21：品質試験区分
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("KEISAN_KBN"))) & ","  '22：加工長計算区分
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("TATIAI_UM"))) & ","   '23：立会有無
        SQL = SQL & N & "" & "NULL" & ","                                       '24：立会予定日
        SQL = SQL & N & "" & "NULL" & ","                                       '25：成績書
        SQL = SQL & N & "" & "NULL" & ","                                       '26：持込余長（単長毎）
        SQL = SQL & N & "" & "NULL" & ","                                       '27：持込余長（ロット毎）
        SQL = SQL & N & "" & "NULL" & ","                                       '28：立会余長（単長毎）
        SQL = SQL & N & "" & "NULL" & ","                                       '29：立会余長（ロット毎）
        SQL = SQL & N & "" & "NULL" & ","                                       '30：指定社検長（単長毎）
        SQL = SQL & N & "" & "NULL" & ","                                       '31：指定社検長（ロット毎）
        SQL = SQL & N & impDtForStr(_db.rmNullStr(prmRow("TOKKI"))) & ","       '32：特記事項
        SQL = SQL & N & impDtForStr(_db.rmNullStr(prmRow("BIKO"))) & ","        '33：備考
        SQL = SQL & N & impDtForStr(_db.rmNullStr(prmRow("HENKO"))) & ","       '34：変更内容
        SQL = SQL & N & "" & lMZ05_SEKKEI_MST_EXIST & ","                       '35：設計マスタ有無
        SQL = SQL & N & "" & lMZ05_ZAIRYO_MST_EXIST & ","                       '36：材料マスタ有無
        SQL = SQL & N & "" & lMZ05_KOUTEI_MST_EXIST & ","                       '37：工程マスタ有無
        SQL = SQL & N & "" & lMZ05_TOROKU_KBN_TOROKU & ","                      '38：登録区分
        SQL = SQL & N & "'" & sMZ05_TEHAI_HAKKOU_SYA & "',"                     '39：手配発行者
        SQL = SQL & N & "'" & sEntDate & "',"                                   '40：手配発行日
        SQL = SQL & N & "'" & sMZ05_SEKKEI_KAKUNIN_SYA & "',"                   '41：設計票確認者
        SQL = SQL & N & "'" & sEntDate & "',"                                   '42：設計票確認日
        SQL = SQL & N & "'" & sMZ05_ZAIRYO_KAKUNIN_SYA & "',"                   '43：材料票確認者
        SQL = SQL & N & "'" & sEntDate & "',"                                   '44：材料票確認日
        SQL = SQL & N & "'" & sMZ05_KOUTEI_KAKUNIN_SYA & "',"                   '45：工程票確認者
        SQL = SQL & N & "'" & sEntDate & "',"                                   '46：工程票確認日
        SQL = SQL & N & "'" & sMZ05_TEHAI_TOUROKU_SYA & "',"                    '47：手配登録者
        SQL = SQL & N & "'" & sEntDate & "',"                                   '48：手配登録日
        SQL = SQL & N & "" & lMZ05_SENDFLG_ON & ""                              '49：送信済フラグ
        SQL = SQL & N & ")"
        _db.executeDB(SQL)

    End Sub

    '-------------------------------------------------------------------------------
    '　 製作手配ＤＢ追加ＳＱＬ文作成（単長部）
    '   （処理概要）製作手配ＤＢ（単長）に追加するＳＱＬ文を編集する。
    '-------------------------------------------------------------------------------
    Private Function insertTehai2(ByVal prmRow As DataRow) As Boolean

        Dim SQL As String = ""
        SQL = SQL & N & "INSERT INTO IN_TEHAI2_TB"
        SQL = SQL & N & "("
        SQL = SQL & N & " IT2_TEHAINO,"     ' 1：手配№
        SQL = SQL & N & " IT2_RENBAN,"      ' 2：連番
        SQL = SQL & N & " IT2_TANCYO,"      ' 3：単長
        SQL = SQL & N & " IT2_JYOSU,"       ' 4：条数
        SQL = SQL & N & " IT2_MAKIWAKC,"    ' 5：巻枠コード
        SQL = SQL & N & " IT2_HOSOK"        ' 6：包装／表示区分
        SQL = SQL & N & ")"
        SQL = SQL & N & " VALUES("
        SQL = SQL & N & _db.rmNullInt(prmRow("TEHAI_NO")) & ","             ' 1：手配№
        SQL = SQL & N & " 1,"                                               ' 2：連番
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("TANCYO"))) & ","  ' 3：単長
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("JYOSU"))) & ","   ' 4：条数
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("MAKI_CD"))) & "," ' 5：巻枠コード
        SQL = SQL & N & impDtForStr(_db.rmNullStr(prmRow("HOSO_KBN")))      ' 6：包装／表示区分
        SQL = SQL & N & ")"
        _db.executeDB(SQL)

    End Function

    '-------------------------------------------------------------------------------
    '　 製作手配ＤＢ追加ＳＱＬ文作成（代替部）
    '   （処理概要）製作手配ＤＢ（代替部）に追加するＳＱＬ文を編集する。
    '-------------------------------------------------------------------------------
    Private Function insertTehai3(ByVal prmRow As DataRow, ByVal lPrmRen As Long) As Boolean
        Dim SQL As String = ""
        SQL = SQL & N & "INSERT INTO IN_TEHAI3_TB"
        SQL = SQL & N & "("
        SQL = SQL & N & " IT3_TEHAINO,"     ' 1：手配№
        SQL = SQL & N & " IT3_RENBAN,"      ' 2：連番
        SQL = SQL & N & " IT3_HYOJYNC,"     ' 3：標準工程コード
        SQL = SQL & N & " IT3_KOTEIC,"      ' 4：工程コード
        SQL = SQL & N & " IT3_KOTEIJ,"      ' 5：工程順位
        SQL = SQL & N & " IT3_TEIIN,"       ' 6：定員
        SQL = SQL & N & " IT3_DANDORI,"     ' 7：段取
        SQL = SQL & N & " IT3_KIJYN,"       ' 8：基準出来高
        SQL = SQL & N & " IT3_STARTS,"      ' 9：ｽﾀｰﾄﾘｰﾙ余長・ｽﾀｰﾄ
        SQL = SQL & N & " IT3_STARTL,"      '10：ｽﾀｰﾄﾘｰﾙ余長・ﾗｽﾄ
        SQL = SQL & N & " IT3_LASTS,"       '11：ﾗｽﾄﾘｰﾙ余長・ｽﾀｰﾄ
        SQL = SQL & N & " IT3_LASTL,"       '12：ﾗｽﾄﾘｰﾙ余長・ﾗｽﾄ
        SQL = SQL & N & " IT3_LENGTH,"      '13：最大巻取長
        SQL = SQL & N & " IT3_CONTROL"      '14：計算制御
        SQL = SQL & N & ")"
        SQL = SQL & N & " VALUES("
        SQL = SQL & N & _db.rmNullInt(prmRow("TEHAI_NO")) & ","                         ' 1：手配№
        SQL = SQL & N & lPrmRen & ","                                                   ' 2：連番
        SQL = SQL & N & impDtForStr(_db.rmNullStr(prmRow("HYOJYUNC_" & lPrmRen))) & "," ' 3：標準工程コード
        SQL = SQL & N & impDtForStr(_db.rmNullStr(prmRow("KOUTEIC_" & lPrmRen))) & ","  ' 4：工程コード
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("KOUTEIJ_" & lPrmRen))) & ","  ' 5：工程順位
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("TEIIN_" & lPrmRen))) & ","    ' 6：定員
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("DANDORI_" & lPrmRen))) & ","  ' 7：段取
        SQL = SQL & N & impDtForNum(_db.rmNullStr(prmRow("DKIJYUN_" & lPrmRen))) & ","  ' 8：基準出来高
        SQL = SQL & N & "NULL,"                                                         ' 9：ｽﾀｰﾄﾘｰﾙ余長・ｽﾀｰﾄ
        SQL = SQL & N & "NULL,"                                                         '10：ｽﾀｰﾄﾘｰﾙ余長・ﾗｽﾄ
        SQL = SQL & N & "NULL,"                                                         '11：ﾗｽﾄﾘｰﾙ余長・ｽﾀｰﾄ
        SQL = SQL & N & "NULL,"                                                         '12：ﾗｽﾄﾘｰﾙ余長・ﾗｽﾄ
        SQL = SQL & N & "NULL,"                                                         '13：最大巻取長
        SQL = SQL & N & "NULL"                                                          '14：計算制御
        SQL = SQL & N & ")"
        _db.executeDB(SQL)

    End Function

    'SQL文字列編集(文字用)
    Private Function impDtForStr(ByVal prmVal As String) As String
        If "".Equals(prmVal) Then
            Return "NULL"
        Else
            Return "'" & prmVal & "'"
        End If
    End Function

    'SQL文字列編集(数値用)
    Private Function impDtForNum(ByVal prmVal As String) As String
        If "".Equals(prmVal) Then
            Return "NULL"
        Else
            Return "" & prmVal & ""
        End If
    End Function

End Class