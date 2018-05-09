'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）販売実績集計展開指示
'    （フォームID）ZG340B_HJissekiTorikomi
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   高木        2010/10/26                 新規              
'　(2)   菅野        2011/01/26                 変更　個別計画入力未登録チェック追加              
'　(3)   菅野        2014/06/04                 変更　材料票マスタ（MPESEKKEI）テーブル変更対応            
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Public Class ZG340B_HJissekiTenkai
    Inherits System.Windows.Forms.Form

    '-------------------------------------------------------------------------------
    '   定数定義
    '-------------------------------------------------------------------------------
    Private Const RS As String = "RecSet"                       'データセットテーブル名
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const PGID As String = "ZG340B"
    Private Const IMP_LOG_NM As String = "販売実績集計展開処理出力情報.txt" '2010.12.27 add by takagi #59

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
            sql = sql & N & "    ,KENNSU2 "
            sql = sql & N & "    ,KENNSU3 "
            sql = sql & N & "    ,KENNSU4 "
            sql = sql & N & "    ,KENNSU5 "
            sql = sql & N & "    ,NAME1 "
            sql = sql & N & "    ,NAME2 "
            sql = sql & N & "    from T91RIREKI "
            sql = sql & N & "    where PGID     = '" & PGID & "' "
            sql = sql & N & ") "
            sql = sql & N & "where RNUM = 1 "
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)
            If iRecCnt < 1 Then
                '履歴なし
                lblZenkaiJikkouDate.Text = ZC110M_Menu.NON_EXECUTE
                lblZenkaiKensuPtn1.Text = ""
                lblZenkaiKensuPtn2.Text = ""
                lblZenkaiKensuPtn3.Text = ""
                lblZenkaiKensuPtn4.Text = ""
                lblZenkaiKensuPtn5.Text = ""
                lblZenkaiAnbunFrom.Text = ""
                lblZenkaiAnbunTo.Text = ""
            Else
                '履歴あり
                lblZenkaiJikkouDate.Text = _db.rmNullDate(ds.Tables(RS).Rows(0)("SDATEEND"))
                lblZenkaiKensuPtn1.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU1")), "#,##0")
                lblZenkaiKensuPtn2.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU2")), "#,##0")
                lblZenkaiKensuPtn3.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU3")), "#,##0")
                lblZenkaiKensuPtn4.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU4")), "#,##0")
                lblZenkaiKensuPtn5.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU5")), "#,##0")
                Dim wk As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("NAME1"))
                lblZenkaiAnbunFrom.Text = wk.Substring(0, 4) & "/" & wk.Substring(4, 2)
                wk = _db.rmNullStr(ds.Tables(RS).Rows(0)("NAME2"))
                lblZenkaiAnbunTo.Text = wk.Substring(0, 4) & "/" & wk.Substring(4, 2)
            End If



            '今回実行情報の表示
            dteKonkaiAnbunFrom.Text = Format(DateAdd(DateInterval.Month, -12, CDate(lblSyoriDate.Text & "/01")), "yyyy/MM")
            dteKonkaiAnbunTo.Text = Format(DateAdd(DateInterval.Month, -1, CDate(lblSyoriDate.Text & "/01")), "yyyy/MM")
            lblKonkaiKensuPtn1.Text = getCntAnbunTarget()
            lblKonkaiKensuPtn2.Text = _db.rmNullInt(_db.selectDB("SELECT COUNT(*) CNT FROM T12HINMEIHANK WHERE TENKAIPTN = '2'", RS, iRecCnt).Tables(RS).Rows(0)("CNT"))
            lblKonkaiKensuPtn3.Text = getCntTarget(-3, -1, "3")
            lblKonkaiKensuPtn4.Text = getCntTarget(-12, -1, "4")
            lblKonkaiKensuPtn5.Text = getCntTarget(-12, -10, "5")

            '実行ボタン使用可否
            btnJikkou.Enabled = _updFlg

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '展開パターン３，４，５の対象件数を取得する
    Private Function getCntTarget(ByVal prmFrom As Integer, ByVal prmTo As Integer, ByVal prmPtn As String) As String

        Dim prmFromYM As String = Format(DateAdd(DateInterval.Month, prmFrom, CDate(lblSyoriDate.Text & "/01")), "yyyyMM")
        Dim prmToYM As String = Format(DateAdd(DateInterval.Month, prmTo, CDate(lblSyoriDate.Text & "/01")), "yyyyMM")
        'W13
        Dim Sql As String = ""
        Sql = Sql & N & "SELECT COUNT(*) CNT FROM ( "
        Sql = Sql & N & "SELECT KHINMEICD "
        Sql = Sql & N & "FROM ( "
        Sql = Sql & N & "     SELECT "
        Sql = Sql & N & "      KHINMEICD "
        Sql = Sql & N & "     ,NENGETU "
        Sql = Sql & N & "     FROM ( "
        Sql = Sql & N & "          SELECT "
        Sql = Sql & N & "           M12.KHINMEICD "
        Sql = Sql & N & "          ,T10.NENGETU "
        Sql = Sql & N & "          FROM T10HINHANJ T10 "
        Sql = Sql & N & "          INNER JOIN M12SYUYAKU    M12 ON T10.HINMEICD  = M12.HINMEICD "
        Sql = Sql & N & "          INNER JOIN M11KEIKAKUHIN M11 ON M12.KHINMEICD = M11.TT_KHINMEICD "
        Sql = Sql & N & "          WHERE M11.TT_TENKAIPTN = '" & prmPtn & "' "
        Sql = Sql & N & "            AND T10.NENGETU >= '" & Format(DateAdd(DateInterval.Month, -12, CDate(lblSyoriDate.Text & "/01")), "yyyyMM") & "' "
        Sql = Sql & N & "            AND T10.NENGETU <= '" & Format(DateAdd(DateInterval.Month, -1, CDate(lblSyoriDate.Text & "/01")), "yyyyMM") & "' "
        Sql = Sql & N & "          ) "
        Sql = Sql & N & "     GROUP BY  KHINMEICD,NENGETU "
        Sql = Sql & N & "     ) "
        Sql = Sql & N & "WHERE NENGETU >= '" & prmFromYM & "' "
        Sql = Sql & N & "  AND NENGETU <= '" & prmToYM & "' "
        Sql = Sql & N & "GROUP BY KHINMEICD "
        Sql = Sql & N & ")"
        Return _db.rmNullInt(_db.selectDB(Sql, RS).Tables(RS).Rows(0)("CNT")).ToString

    End Function

    '展開パターン１の対象件数を取得するが、マスタとの突合結果によってはこれ以下になることもある
    Private Function getCntAnbunTarget() As String

        Dim Sql As String = ""
        Sql = Sql & N & "SELECT COUNT(*) CNT "
        Sql = Sql & N & "FROM ( "
        Sql = Sql & N & "     SELECT * "
        Sql = Sql & N & "     FROM ( "
        Sql = Sql & N & "          SELECT "
        Sql = Sql & N & "           KHINMEICD "
        Sql = Sql & N & "          ,NENGETU "
        Sql = Sql & N & "          FROM ( "
        Sql = Sql & N & "               SELECT "
        Sql = Sql & N & "                M12.KHINMEICD "
        Sql = Sql & N & "               ,T10.NENGETU "
        Sql = Sql & N & "               FROM T10HINHANJ T10 "
        Sql = Sql & N & "               INNER JOIN M12SYUYAKU    M12 ON T10.HINMEICD  = M12.HINMEICD "
        Sql = Sql & N & "               INNER JOIN M11KEIKAKUHIN M11 ON M12.KHINMEICD = M11.TT_KHINMEICD "
        Sql = Sql & N & "               WHERE M11.TT_TENKAIPTN = '1' "
        Sql = Sql & N & "                 AND T10.NENGETU >= '" & Format(DateAdd(DateInterval.Month, -12, CDate(lblSyoriDate.Text & "/01")), "yyyyMM") & "' "
        Sql = Sql & N & "                 AND T10.NENGETU <= '" & Format(DateAdd(DateInterval.Month, -1, CDate(lblSyoriDate.Text & "/01")), "yyyyMM") & "' "
        Sql = Sql & N & "               ) "
        Sql = Sql & N & "          GROUP BY  KHINMEICD,NENGETU "
        Sql = Sql & N & "          ) "
        Sql = Sql & N & "     WHERE NENGETU >= '" & dteKonkaiAnbunFrom.Text.Replace("/", "") & "' "
        Sql = Sql & N & "       AND NENGETU <= '" & dteKonkaiAnbunTo.Text.Replace("/", "") & "' "
        Sql = Sql & N & "     GROUP BY KHINMEICD "
        Sql = Sql & N & "     ) "
        Return _db.rmNullInt(_db.selectDB(Sql, RS).Tables(RS).Rows(0)("CNT")).ToString

    End Function

    '------------------------------------------------------------------------------------------------------
    '　戻るボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnModoru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModoru.Click

        '自画面を終了し、メニュー画面に戻る。
        _parentForm.Show()
        _parentForm.Activate()

        Me.Close()

    End Sub

    '-------------------------------------------------------------------------------
    '   実行ボタン押下イベント
    '-------------------------------------------------------------------------------
    Private Sub btnJikkou_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJikkou.Click
        Try

            '入力チェック
            Call checkInput()

            '' 2011/01/26 ADD-S Sugano #94
            '個別販売計画入力の登録済チェック
            '未登録のレコード件数を取得
            Dim sql = ""
            sql = sql & N & " SELECT COUNT(*) RECCNT FROM M11KEIKAKUHIN M11 "
            sql = sql & N & " LEFT JOIN T12HINMEIHANK T12 "
            sql = sql & N & " ON M11.TT_KHINMEICD = T12.KHINMEICD "
            sql = sql & N & " WHERE M11.TT_TENKAIPTN = '2' "
            sql = sql & N & " AND "
            sql = sql & N & " T12.KHINMEICD IS NULL "   'T12にレコードが存在しないもの

            'SQL発行
            Dim ds As DataSet = _db.selectDB(sql, RS)
            Dim reccnt As Integer = 0
            reccnt = _db.rmNullInt(ds.Tables(RS).Rows(0)("RECCNT"))
            If reccnt <> 0 Then
                Throw New UsrDefException("個別計画入力が未登録のデータがあります。", _msgHd.getMSG("kobetsuMitouroku"), btnModoru)
            End If
            '' 2011/01/26 ADD-E Sugano #94

            '実行確認（実行します。よろしいですか？）
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmRun")
            If rtn = Windows.Forms.DialogResult.No Or rtn = Windows.Forms.DialogResult.Cancel Then Exit Sub

            '-->2010.12.27 add by takagi #59
            Dim cntNullMetsuke As Integer = 0
            '<--2010.12.27 add by takagi #59

            'ポインタ変更
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor
            Try
                '【バッチ処理開始】
                Dim pb As UtilProgressBar = New UtilProgressBar(Me)                         'プログレスバー画面を表示
                pb.Show()
                Try
                    Dim st As Date = Now                                                    '処理開始日時
                    Dim ed As Date = Nothing    '処理終了日時

                    'プログレスバー設定
                    pb.status = "取込準備中" : pb.maxVal = 3
                    _db.executeDB("delete from ZG340B_W10 where UPDNAME = '" & UtilClass.getComputerName() & "'")
                    _db.executeDB("delete from ZG340B_W11 where UPDNAME = '" & UtilClass.getComputerName() & "'")
                    _db.executeDB("delete from ZG340B_W12 where UPDNAME = '" & UtilClass.getComputerName() & "'")
                    _db.executeDB("delete from ZG340B_W13 where UPDNAME = '" & UtilClass.getComputerName() & "'")
                    _db.executeDB("delete from ZG340B_W14 where UPDNAME = '" & UtilClass.getComputerName() & "'")

                    ' ''Call kobetsuInsert()                    '1-0 個別計画設定

                    Call createHanbaiJisseki()              '2-0 計画品名CD補完
                    Call createKeikakuhinHanbaiJisseki()    '2-1 計画品名毎集計
                    Call createAnbunWk()                    '2-2 按分期間集計(パターン１)
                    Call createHanbaiJissekiWkByPtn(Format(DateAdd(DateInterval.Month, -3, CDate(lblSyoriDate.Text & "/01")), "yyyyMM"), _
                                                    Format(DateAdd(DateInterval.Month, -1, CDate(lblSyoriDate.Text & "/01")), "yyyyMM"), _
                                                    "3")    '2-3 直近３カ月集計(パターン３)
                    Call createHanbaiJissekiWkByPtn(Format(DateAdd(DateInterval.Month, -12, CDate(lblSyoriDate.Text & "/01")), "yyyyMM"), _
                                                    Format(DateAdd(DateInterval.Month, -1, CDate(lblSyoriDate.Text & "/01")), "yyyyMM"), _
                                                    "4")    '2-4 前年集計(パターン４)
                    '-->2010.12.25 chg by takagi #49
                    'Call createHanbaiJissekiWkByPtn(Format(DateAdd(DateInterval.Month, -12, CDate(lblSyoriDate.Text & "/01")), "yyyyMM"), _
                    '                                Format(DateAdd(DateInterval.Month, -10, CDate(lblSyoriDate.Text & "/01")), "yyyyMM"), _
                    '                                "5")    '2-5 前年３カ月集計(パターン５)
                    Call createHanbaiJissekiWkByPtn(Format(DateAdd(DateInterval.Month, -13, CDate(lblSyoriDate.Text & "/01")), "yyyyMM"), _
                                                    Format(DateAdd(DateInterval.Month, -11, CDate(lblSyoriDate.Text & "/01")), "yyyyMM"), _
                                                    "5T")    '2-5 前年３カ月集計(パターン５-1)
                    Call createHanbaiJissekiWkByPtn(Format(DateAdd(DateInterval.Month, -12, CDate(lblSyoriDate.Text & "/01")), "yyyyMM"), _
                                                    Format(DateAdd(DateInterval.Month, -10, CDate(lblSyoriDate.Text & "/01")), "yyyyMM"), _
                                                    "5Y")    '2-5 前年３カ月集計(パターン５-2)
                    Call createHanbaiJissekiWkByPtn(Format(DateAdd(DateInterval.Month, -11, CDate(lblSyoriDate.Text & "/01")), "yyyyMM"), _
                                                    Format(DateAdd(DateInterval.Month, -9, CDate(lblSyoriDate.Text & "/01")), "yyyyMM"), _
                                                    "5YY")    '2-5 前年３カ月集計(パターン５-3)
                    '<--2010.12.25 chg by takagi #49
                    Call summaryHinshuValue()               '2-6 品種別合計生成
                    Call updateHinshuSum()                  '2-7 品種別合計補完
                    Call updateAnbunRate()                  '2-8 サイズ別按分率算出
                    _db.beginTran()
                    Try
                        _db.executeDB("delete from T13HANBAI ")
                        '-->2010.12.27 chg by takagi #59
                        'Call kobetsuInsert()                    '1-0 個別計画設定
                        Call kobetsuInsert(cntNullMetsuke)
                        '<--2010.12.27 chg by takagi #59

                        Call insertAnbunRec()                   '2-9 按分値計画設定
                        Call insertAvgRec()                     '2-10 平均値計画設定

                        pb.status = "ステータス変更中・・・"
                        ed = Now                    '処理終了日時
                        _parentForm.updateSeigyoTbl(PGID, True, st, ed) : pb.value += 1         '3-0

                        pb.status = "実行履歴作成"
                        insertRireki(st, ed) : pb.value += 1                                    '3-1 実行履歴格納
                    Catch ex As Exception
                        _db.rollbackTran()
                        Throw ex
                    Finally
                        If _db.isTransactionOpen Then _db.commitTran()
                    End Try

                Finally
                    pb.Close()                                                              'プログレスバー画面消去
                End Try
            Finally
                Me.Cursor = cur
            End Try


            '終了MSG
            '-->2010.12.27 chg by takagi #59
            'Call _msgHd.dspMSG("completeRun")
            Dim optionMsg As String = ""
            Dim wk As String = ""
            Dim outFilePath As String = ""
            Call UtilClass.dividePathAndFile(StartUp.iniValue.LogFilePath, outFilePath, wk)
            outFilePath = outFilePath & "\" & IMP_LOG_NM
            If cntNullMetsuke > 0 Then
                optionMsg = "-----------------------------------------------------------------" & N & _
                            "目付の取得が行えないデータが" & cntNullMetsuke & "件存在しました。" & N & _
                            "詳細な品名コードはログを確認してください。"

                optionMsg = optionMsg & N & N & outFilePath
            End If
            Call _msgHd.dspMSG("completeRun", optionMsg)
            If cntNullMetsuke > 0 Then
                'ログ表示
                Try
                    System.Diagnostics.Process.Start(outFilePath)   '関連付いたアプリで起動
                Catch ex As Exception
                End Try
            End If
            '<--2010.12.27 chg by takagi #59
            Call btnModoru_Click(Nothing, Nothing)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '2-10 平均値計画設定
    Private Sub insertAvgRec()
        Dim sql As String = ""
        sql = sql & N & "INSERT INTO T13HANBAI "
        sql = sql & N & "( "
        sql = sql & N & " KHINMEICD "
        sql = sql & N & ",TENKAIPTN "
        sql = sql & N & ",THANBAIRYOU "
        sql = sql & N & ",YHANBAIRYOU "
        sql = sql & N & ",YYHANBAIRYOU "
        sql = sql & N & ",THANBAIRYOUH "
        sql = sql & N & ",YHANBAIRYOUH "
        sql = sql & N & ",YYHANBAIRYOUH "
        sql = sql & N & ",THANBAIRYOUHK "
        sql = sql & N & ",YHANBAIRYOUHK "
        sql = sql & N & ",YYHANBAIRYOUHK "
        sql = sql & N & ",UPDNAME "
        sql = sql & N & ",UPDDATE "
        '-->2010.12.12 add by takagi
        sql = sql & N & ",METSUKE "
        sql = sql & N & ",THANBAISU "
        sql = sql & N & ",YHANBAISU "
        sql = sql & N & ",YYHANBAISU "
        sql = sql & N & ",THANBAISUH "
        sql = sql & N & ",YHANBAISUH "
        sql = sql & N & ",YYHANBAISUH "
        sql = sql & N & ",THANBAISUHK "
        sql = sql & N & ",YHANBAISUHK "
        sql = sql & N & ",YYHANBAISUHK "
        '<--2010.12.12 add by takagi
        sql = sql & N & ") "
        '-->2010.12.17 chg by takagi #15
        ''-->2010.12.12 chg by takagi
        ''sql = sql & N & "SELECT  "
        ''sql = sql & N & " KHINMEICD "
        ''sql = sql & N & ",TENKAIPTN "
        ''sql = sql & N & ",ROUND(HJISSEKIRYOU/DECODE(TENKAIPTN,'4',12,3),1) "
        ''sql = sql & N & ",ROUND(HJISSEKIRYOU/DECODE(TENKAIPTN,'4',12,3),1) "
        ''sql = sql & N & ",ROUND(HJISSEKIRYOU/DECODE(TENKAIPTN,'4',12,3),1) "
        ''sql = sql & N & ",NULL "
        ''sql = sql & N & ",NULL "
        ''sql = sql & N & ",NULL "
        ''sql = sql & N & ",ROUND(HJISSEKIRYOU/DECODE(TENKAIPTN,'4',12,3),1) "
        ''sql = sql & N & ",ROUND(HJISSEKIRYOU/DECODE(TENKAIPTN,'4',12,3),1) "
        ''sql = sql & N & ",ROUND(HJISSEKIRYOU/DECODE(TENKAIPTN,'4',12,3),1) "
        ''sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        ''sql = sql & N & ",SYSDATE "
        ''sql = sql & N & "FROM ZG340B_W13 WK "
        ''sql = sql & N & "WHERE UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        'sql = sql & N & "SELECT  "
        'sql = sql & N & " WK.KHINMEICD "
        'sql = sql & N & ",WK.TENKAIPTN "
        'sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3),1) "
        'sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3),1) "
        'sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3),1) "
        'sql = sql & N & ",NULL "
        'sql = sql & N & ",NULL "
        'sql = sql & N & ",NULL "
        'sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3),1) "
        'sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3),1) "
        'sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3),1) "
        'sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        'sql = sql & N & ",SYSDATE "
        'sql = sql & N & ",T10.MYMETSUKE "
        'sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12,1) ELSE NULL END  "
        'sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12,1) ELSE NULL END  "
        'sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12,1) ELSE NULL END  "
        'sql = sql & N & ",NULL "
        'sql = sql & N & ",NULL "
        'sql = sql & N & ",NULL "
        'sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12,1) ELSE NULL END  "
        'sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12,1) ELSE NULL END  "
        'sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12,1) ELSE NULL END  "
        sql = sql & N & "SELECT  "
        sql = sql & N & " WK.KHINMEICD "
        sql = sql & N & ",WK.TENKAIPTN "
        '-->2011.01.16 chg by takagi #82
        'sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3)/1000,1) " 'Kg→t
        'sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3)/1000,1) "
        'sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3)/1000,1) "
        sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3)/1000,4) " 'Kg→t
        sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3)/1000,4) "
        sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3)/1000,4) "
        '<--2011.01.16 chg by takagi #82
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        '-->2011.01.16 chg by takagi #82
        'sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3)/1000,1) "
        'sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3)/1000,1) "
        'sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3)/1000,1) "
        sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3)/1000,4) "
        sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3)/1000,4) "
        sql = sql & N & ",ROUND(WK.HJISSEKIRYOU/DECODE(WK.TENKAIPTN,'4',12,3)/1000,4) "
        '<--2011.01.16 chg by takagi #82
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & ",SYSDATE "
        sql = sql & N & ",T10.MYMETSUKE "
        '-->2011.01.16 chg by takagi #82
        'sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12/1000,1) ELSE NULL END  " 'm→Km
        'sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12/1000,1) ELSE NULL END  "
        'sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12/1000,1) ELSE NULL END  "
        sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12/1000,4) ELSE NULL END  " 'm→Km
        sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12/1000,4) ELSE NULL END  "
        sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12/1000,4) ELSE NULL END  "
        '<--2011.01.16 chg by takagi #82
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        '-->2011.01.16 chg by takagi #82
        'sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12/1000,1) ELSE NULL END  "
        'sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12/1000,1) ELSE NULL END  "
        'sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12/1000,1) ELSE NULL END  "
        sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12/1000,4) ELSE NULL END  "
        sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12/1000,4) ELSE NULL END  "
        sql = sql & N & ",CASE WHEN WK.TENKAIPTN='4' AND T10.MYMETSUKE=0 THEN ROUND(WK.HJISSEKISU/12/1000,4) ELSE NULL END  "
        '<--2011.01.16 chg by takagi #82
        ''<--2010.12.17 chg by takagi #15
        '-->2010.12.26 chg by takagi #49
        'sql = sql & N & "FROM ZG340B_W13 WK "
        sql = sql & N & "FROM (select * from ZG340B_W13 where tenkaiptn not in ('5T','5Y','5YY')) WK "
        '<--2010.12.26 chg by takagi #49
        sql = sql & N & "LEFT JOIN ( "
        sql = sql & N & "	SELECT "
        sql = sql & N & "	 M.KHINMEICD "
        sql = sql & N & "	,MIN(T.METSUKE) MYMETSUKE "
        sql = sql & N & "	FROM T10HINHANJ       T "
        sql = sql & N & "	INNER JOIN M12SYUYAKU M "
        sql = sql & N & "	ON T.HINMEICD = M.HINMEICD "
        sql = sql & N & "	WHERE T.METSUKE IS NOT NULL "
        sql = sql & N & "	GROUP BY M.KHINMEICD ) T10 ON WK.KHINMEICD = T10.KHINMEICD "
        '<--2010.12.12 chg by takagi
        sql = sql & N & "WHERE UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        _db.executeDB(sql)

        '-->2010.12.26 add by takagi #49
        sql = ""
        sql = sql & N & "INSERT INTO T13HANBAI "
        sql = sql & N & "( "
        sql = sql & N & " KHINMEICD "
        sql = sql & N & ",TENKAIPTN "
        sql = sql & N & ",THANBAIRYOU "
        sql = sql & N & ",YHANBAIRYOU "
        sql = sql & N & ",YYHANBAIRYOU "
        sql = sql & N & ",THANBAIRYOUH "
        sql = sql & N & ",YHANBAIRYOUH "
        sql = sql & N & ",YYHANBAIRYOUH "
        sql = sql & N & ",THANBAIRYOUHK "
        sql = sql & N & ",YHANBAIRYOUHK "
        sql = sql & N & ",YYHANBAIRYOUHK "
        sql = sql & N & ",UPDNAME "
        sql = sql & N & ",UPDDATE "
        sql = sql & N & ",METSUKE "
        sql = sql & N & ",THANBAISU "
        sql = sql & N & ",YHANBAISU "
        sql = sql & N & ",YYHANBAISU "
        sql = sql & N & ",THANBAISUH "
        sql = sql & N & ",YHANBAISUH "
        sql = sql & N & ",YYHANBAISUH "
        sql = sql & N & ",THANBAISUHK "
        sql = sql & N & ",YHANBAISUHK "
        sql = sql & N & ",YYHANBAISUHK "
        sql = sql & N & ") "
        sql = sql & N & "SELECT  "
        sql = sql & N & " WK.KHINMEICD "
        sql = sql & N & ",WK.MAXTENKAIPTN "
        '-->2011.01.16 chg by takagi #82
        'sql = sql & N & ",ROUND(WK.SUMHJISSEKIRYOU_T /3/1000,1) " 'Kg→t
        'sql = sql & N & ",ROUND(WK.SUMHJISSEKIRYOU_Y /3/1000,1) "
        'sql = sql & N & ",ROUND(WK.SUMHJISSEKIRYOU_YY/3/1000,1) "
        sql = sql & N & ",ROUND(WK.SUMHJISSEKIRYOU_T /3/1000,4) " 'Kg→t
        sql = sql & N & ",ROUND(WK.SUMHJISSEKIRYOU_Y /3/1000,4) "
        sql = sql & N & ",ROUND(WK.SUMHJISSEKIRYOU_YY/3/1000,4) "
        '<--2011.01.16 chg by takagi #82
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        '-->2011.01.16 chg by takagi #82
        'sql = sql & N & ",ROUND(WK.SUMHJISSEKIRYOU_T /3/1000,1) "
        'sql = sql & N & ",ROUND(WK.SUMHJISSEKIRYOU_Y /3/1000,1) "
        'sql = sql & N & ",ROUND(WK.SUMHJISSEKIRYOU_YY/3/1000,1) "
        sql = sql & N & ",ROUND(WK.SUMHJISSEKIRYOU_T /3/1000,4) "
        sql = sql & N & ",ROUND(WK.SUMHJISSEKIRYOU_Y /3/1000,4) "
        sql = sql & N & ",ROUND(WK.SUMHJISSEKIRYOU_YY/3/1000,4) "
        '<--2011.01.16 chg by takagi #82
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & ",SYSDATE "
        sql = sql & N & ",T10.MYMETSUKE "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & "FROM ( "
        sql = sql & N & "     SELECT "
        sql = sql & N & "       KHINMEICD "
        sql = sql & N & "      ,SUM(HJISSEKIRYOU_T)  SUMHJISSEKIRYOU_T "
        sql = sql & N & "      ,SUM(HJISSEKISU_T)    SUMHJISSEKISU_T "
        sql = sql & N & "      ,SUM(HJISSEKIRYOU_Y)  SUMHJISSEKIRYOU_Y"
        sql = sql & N & "      ,SUM(HJISSEKISU_Y)    SUMHJISSEKISU_Y "
        sql = sql & N & "      ,SUM(HJISSEKIRYOU_YY) SUMHJISSEKIRYOU_YY "
        sql = sql & N & "      ,SUM(HJISSEKISU_YY)   SUMHJISSEKISU_YY "
        sql = sql & N & "      ,'5'                  MAXTENKAIPTN"
        sql = sql & N & "      ,MAX(UPDNAME)         MAXUPDNAME"
        sql = sql & N & "      ,MAX(UPDDATE)         MAXUPDDATE "
        sql = sql & N & "     FROM ( "
        sql = sql & N & "          SELECT "
        sql = sql & N & "            KHINMEICD "
        sql = sql & N & "           ,HJISSEKIRYOU HJISSEKIRYOU_T "
        sql = sql & N & "           ,HJISSEKISU   HJISSEKISU_T "
        sql = sql & N & "           ,0            HJISSEKIRYOU_Y "
        sql = sql & N & "           ,0            HJISSEKISU_Y "
        sql = sql & N & "           ,0            HJISSEKIRYOU_YY "
        sql = sql & N & "           ,0            HJISSEKISU_YY "
        sql = sql & N & "           ,TENKAIPTN "
        sql = sql & N & "           ,UPDNAME "
        sql = sql & N & "           ,UPDDATE "
        sql = sql & N & "          FROM ZG340B_W13 "
        sql = sql & N & "          WHERE TENKAIPTN  = '5T' "
        sql = sql & N & "          UNION ALL "
        sql = sql & N & "          SELECT "
        sql = sql & N & "            KHINMEICD "
        sql = sql & N & "           ,0            HJISSEKIRYOU_T "
        sql = sql & N & "           ,0            HJISSEKISU_T "
        sql = sql & N & "           ,HJISSEKIRYOU HJISSEKIRYOU_Y "
        sql = sql & N & "           ,HJISSEKISU   HJISSEKISU_Y "
        sql = sql & N & "           ,0            HJISSEKIRYOU_YY "
        sql = sql & N & "           ,0            HJISSEKISU_YY "
        sql = sql & N & "           ,TENKAIPTN "
        sql = sql & N & "           ,UPDNAME "
        sql = sql & N & "           ,UPDDATE "
        sql = sql & N & "          FROM ZG340B_W13 "
        sql = sql & N & "          WHERE TENKAIPTN = '5Y' "
        sql = sql & N & "          UNION ALL "
        sql = sql & N & "          SELECT "
        sql = sql & N & "            KHINMEICD "
        sql = sql & N & "           ,0            HJISSEKIRYOU_T "
        sql = sql & N & "           ,0            HJISSEKISU_T "
        sql = sql & N & "           ,0            HJISSEKIRYOU_Y "
        sql = sql & N & "           ,0            HJISSEKISU_Y "
        sql = sql & N & "           ,HJISSEKIRYOU HJISSEKIRYOU_YY "
        sql = sql & N & "           ,HJISSEKISU   HJISSEKISU_YY "
        sql = sql & N & "           ,TENKAIPTN "
        sql = sql & N & "           ,UPDNAME "
        sql = sql & N & "           ,UPDDATE "
        sql = sql & N & "          FROM ZG340B_W13 "
        sql = sql & N & "          WHERE TENKAIPTN = '5YY' "
        sql = sql & N & "         ) "
        sql = sql & N & "     WHERE UPDNAME   = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & "     GROUP BY KHINMEICD "
        sql = sql & N & "     ) WK "
        sql = sql & N & "LEFT JOIN ( "
        sql = sql & N & "	SELECT "
        sql = sql & N & "	 M.KHINMEICD "
        sql = sql & N & "	,MIN(T.METSUKE) MYMETSUKE "
        sql = sql & N & "	FROM T10HINHANJ       T "
        sql = sql & N & "	INNER JOIN M12SYUYAKU M "
        sql = sql & N & "	ON T.HINMEICD = M.HINMEICD "
        sql = sql & N & "	WHERE T.METSUKE IS NOT NULL "
        sql = sql & N & "	GROUP BY M.KHINMEICD ) T10 ON WK.KHINMEICD = T10.KHINMEICD "
        _db.executeDB(sql)
        '<--2010.12.26 add by takagi #49

    End Sub

    '2-9 按分値計画設定
    Private Sub insertAnbunRec()

        Dim sql As String = ""
        sql = sql & N & "INSERT INTO T13HANBAI "
        Sql = Sql & N & "( "
        Sql = Sql & N & " KHINMEICD "
        Sql = Sql & N & ",TENKAIPTN "
        Sql = Sql & N & ",THANBAIRYOU "
        Sql = Sql & N & ",YHANBAIRYOU "
        Sql = Sql & N & ",YYHANBAIRYOU "
        Sql = Sql & N & ",THANBAIRYOUH "
        Sql = Sql & N & ",YHANBAIRYOUH "
        Sql = Sql & N & ",YYHANBAIRYOUH "
        Sql = Sql & N & ",THANBAIRYOUHK "
        Sql = Sql & N & ",YHANBAIRYOUHK "
        Sql = Sql & N & ",YYHANBAIRYOUHK "
        Sql = Sql & N & ",UPDNAME "
        Sql = Sql & N & ",UPDDATE "
        '-->2010.12.12 add by takagi
        sql = sql & N & ",METSUKE "
        sql = sql & N & ",THANBAISU "
        sql = sql & N & ",YHANBAISU "
        sql = sql & N & ",YYHANBAISU "
        sql = sql & N & ",THANBAISUH "
        sql = sql & N & ",YHANBAISUH "
        sql = sql & N & ",YYHANBAISUH "
        sql = sql & N & ",THANBAISUHK "
        sql = sql & N & ",YHANBAISUHK "
        sql = sql & N & ",YYHANBAISUHK "
        '<--2010.12.12 add by takagi
        sql = sql & N & ") "
        Sql = Sql & N & "SELECT  "
        Sql = Sql & N & " WK.KHINMEICD "
        Sql = Sql & N & ",WK.TENKAIPTN "
        '-->2011.01.16 chg by takagi #82
        'sql = sql & N & ",ROUND(SUB.THANBAIRYOU * WK.ALLOTMENTRATE/100,1) "
        'sql = sql & N & ",ROUND(SUB.YHANBAIRYOU * WK.ALLOTMENTRATE/100,1) "
        'sql = sql & N & ",ROUND(SUB.YYHANBAIRYOU * WK.ALLOTMENTRATE/100,1) "
        sql = sql & N & ",ROUND(SUB.THANBAIRYOU * WK.ALLOTMENTRATE/100,4) "
        sql = sql & N & ",ROUND(SUB.YHANBAIRYOU * WK.ALLOTMENTRATE/100,4) "
        sql = sql & N & ",ROUND(SUB.YYHANBAIRYOU * WK.ALLOTMENTRATE/100,4) "
        '<--2011.01.16 chg by takagi #82
        sql = sql & N & ",NULL "
        Sql = Sql & N & ",NULL "
        Sql = Sql & N & ",NULL "
        '-->2011.01.16 chg by takagi #82
        'sql = sql & N & ",ROUND(SUB.THANBAIRYOU * WK.ALLOTMENTRATE/100,1) "
        'sql = sql & N & ",ROUND(SUB.YHANBAIRYOU * WK.ALLOTMENTRATE/100,1) "
        'sql = sql & N & ",ROUND(SUB.YYHANBAIRYOU * WK.ALLOTMENTRATE/100,1) "
        sql = sql & N & ",ROUND(SUB.THANBAIRYOU * WK.ALLOTMENTRATE/100,4) "
        sql = sql & N & ",ROUND(SUB.YHANBAIRYOU * WK.ALLOTMENTRATE/100,4) "
        sql = sql & N & ",ROUND(SUB.YYHANBAIRYOU * WK.ALLOTMENTRATE/100,4) "
        '<--2011.01.16 chg by takagi #82
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        Sql = Sql & N & ",SYSDATE "
        '-->2010.12.12 add by takagi
        sql = sql & N & ",T10.MYMETSUKE "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        '<--2010.12.12 add by takagi
        sql = sql & N & "FROM ZG340B_W12 WK "
        Sql = Sql & N & "INNER JOIN ( "
        Sql = Sql & N & "	SELECT "
        sql = sql & N & "	 M11.TT_KHINMEICD "
        sql = sql & N & "	,T11.HINSYUKBN "
        sql = sql & N & "	,T11.JUYOUCD "
        sql = sql & N & "	,T11.THANBAIRYOU "
        sql = sql & N & "	,T11.YHANBAIRYOU "
        sql = sql & N & "	,T11.YYHANBAIRYOU "
        sql = sql & N & "	FROM T11HINSYUHANK T11 "
        sql = sql & N & "	INNER JOIN M11KEIKAKUHIN M11 ON T11.JUYOUCD = M11.TT_JUYOUCD AND T11.HINSYUKBN = M11.TT_HINSYUKBN "
        Sql = Sql & N & "	) SUB "
        sql = sql & N & "ON WK.KHINMEICD = SUB.TT_KHINMEICD "
        '-->2010.12.12 add by takagi
        sql = sql & N & "LEFT JOIN ( "
        sql = sql & N & "	SELECT "
        sql = sql & N & "	 M.KHINMEICD "
        sql = sql & N & "	,MIN(T.METSUKE) MYMETSUKE "
        sql = sql & N & "	FROM T10HINHANJ       T "
        sql = sql & N & "	INNER JOIN M12SYUYAKU M "
        sql = sql & N & "	ON T.HINMEICD = M.HINMEICD "
        sql = sql & N & "	WHERE T.METSUKE IS NOT NULL "
        sql = sql & N & "	GROUP BY M.KHINMEICD ) T10 ON WK.KHINMEICD = T10.KHINMEICD "
        '<--2010.12.12 add by takagi
        sql = sql & N & "WHERE UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        '-->2010.12.22 add by takagi #21
        sql = sql & N & " AND  TENKAIPTN = '1' "
        '<--2010.12.22 add by takagi #21
        _db.executeDB(sql)

    End Sub

    '2-8 サイズ別按分率算出
    Private Sub updateAnbunRate()

        Dim sql As String = ""
        sql = sql & N & "UPDATE ZG340B_W12 "
        sql = sql & N & "SET ALLOTMENTRATE = DECODE(HJISSEKIRYOU_SUM,0,0,ROUND((HJISSEKIRYOU/HJISSEKIRYOU_SUM)*100,3)) "
        sql = sql & N & "WHERE UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        _db.executeDB(sql)

    End Sub

    '2-7 品種別合計補完
    Private Sub updateHinshuSum()

        Dim sql As String = ""
        sql = sql & N & "UPDATE ZG340B_W12 "
        sql = sql & N & "SET (HJISSEKIRYOU_SUM,HJISSEKISU_SUM) = ( "
        sql = sql & N & "	SELECT W14.HJISSEKIRYOU_SUM,W14.HJISSEKISU_SUM "
        sql = sql & N & "	FROM ZG340B_W14 W14 "
        sql = sql & N & "	INNER JOIN M11KEIKAKUHIN M11 ON W14.HINSYUKBN = M11.TT_HINSYUKBN AND W14.JUYOUCD = M11.TT_JUYOUCD "
        sql = sql & N & "	WHERE ZG340B_W12.KHINMEICD = M11.TT_KHINMEICD "
        sql = sql & N & "	  AND W14.UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & "	) "
        sql = sql & N & "WHERE ZG340B_W12.KHINMEICD IN ( "
        sql = sql & N & "	SELECT M11.TT_KHINMEICD "
        sql = sql & N & "	FROM ZG340B_W14 W14 "
        sql = sql & N & "	INNER JOIN M11KEIKAKUHIN M11 ON W14.HINSYUKBN = M11.TT_HINSYUKBN AND W14.JUYOUCD = M11.TT_JUYOUCD "
        sql = sql & N & "	WHERE ZG340B_W12.KHINMEICD = M11.TT_KHINMEICD "
        sql = sql & N & "	  AND W14.UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & "	) "
        sql = sql & N & "  AND ZG340B_W12.UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        _db.executeDB(sql)

    End Sub

    '2-6 品種別合計生成
    Private Sub summaryHinshuValue()

        Dim sql As String = ""
        sql = sql & N & "INSERT INTO ZG340B_W14 "
        Sql = Sql & N & "( "
        sql = sql & N & " JUYOUCD "
        sql = sql & N & ",HINSYUKBN "
        sql = sql & N & ",HJISSEKIRYOU_SUM "
        Sql = Sql & N & ",HJISSEKISU_SUM "
        Sql = Sql & N & ",UPDNAME "
        Sql = Sql & N & ",UPDDATE "
        Sql = Sql & N & ") "
        Sql = Sql & N & "SELECT "
        sql = sql & N & " M.TT_JUYOUCD "
        sql = sql & N & ",M.TT_HINSYUKBN "
        sql = sql & N & ",SUM(W.HJISSEKIRYOU) "
        sql = sql & N & ",SUM(W.HJISSEKISU) "
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        Sql = Sql & N & ",SYSDATE "
        sql = sql & N & "FROM ZG340B_W12 W INNER JOIN M11KEIKAKUHIN M ON W.KHINMEICD = M.TT_KHINMEICD"
        sql = sql & N & "WHERE UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & "GROUP BY M.TT_JUYOUCD,M.TT_HINSYUKBN "
        _db.executeDB(sql)

    End Sub

    '-------------------------------------------------------------------------------
    '   入力チェック処理
    '   （処理概要）未入力チェック、大小チェック、妥当性チェック(1年以上前の日付)を行う
    '   ●入力パラメタ  ：なし
    '   ●出力パラメタ  ：なし
    '   ●メソッド戻り値：なし
    '-------------------------------------------------------------------------------
    Private Sub checkInput()
        '入力チェック
        If "".Equals(dteKonkaiAnbunFrom.Text.Replace("/", "").Trim) Then                           '未入力
            Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput"), dteKonkaiAnbunFrom)
        ElseIf "".Equals(dteKonkaiAnbunTo.Text.Replace("/", "").Trim) Then                         '未入力
            Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput"), dteKonkaiAnbunTo)
        ElseIf (dteKonkaiAnbunFrom.Text) > (dteKonkaiAnbunTo.Text) Then                            '大小不正
            Throw New UsrDefException("大小関係が不正です。", _msgHd.getMSG("ErrDaiSyoChk"), dteKonkaiAnbunTo)
        ElseIf DateAdd(DateInterval.Year, 1, CDate(dteKonkaiAnbunFrom.Text & "/01")) < CDate(lblSyoriDate.Text & "/01") Then
            Throw New UsrDefException("無効な日付が入力されています。", _msgHd.getMSG("ImputedInvalidDate"), dteKonkaiAnbunFrom)
        ElseIf DateAdd(DateInterval.Year, 1, CDate(dteKonkaiAnbunTo.Text & "/01")) < CDate(lblSyoriDate.Text & "/01") Then
            Throw New UsrDefException("無効な日付が入力されています。", _msgHd.getMSG("ImputedInvalidDate"), dteKonkaiAnbunTo)
        ElseIf CDate(dteKonkaiAnbunFrom.Text & "/01") >= CDate(lblSyoriDate.Text & "/01") Then
            Throw New UsrDefException("無効な日付が入力されています。", _msgHd.getMSG("ImputedInvalidDate"), dteKonkaiAnbunFrom)
        ElseIf CDate(dteKonkaiAnbunTo.Text & "/01") >= CDate(lblSyoriDate.Text & "/01") Then
            Throw New UsrDefException("無効な日付が入力されています。", _msgHd.getMSG("ImputedInvalidDate"), dteKonkaiAnbunTo)
        End If
    End Sub

    '-------------------------------------------------------------------------------
    '   按分期間集計
    '   （処理概要）展開パターンが１(品種別)のものだけを画面で指定された集計期間で抽出し、計画品名単位で集約する
    '   ●入力パラメタ  ：なし
    '   ●出力パラメタ  ：なし
    '   ●メソッド戻り値：なし
    '-------------------------------------------------------------------------------
    Private Sub createAnbunWk()

        Dim sql As String = ""
        sql = sql & N & "INSERT INTO ZG340B_W12 "
        sql = sql & N & "( "
        sql = sql & N & " KHINMEICD "
        sql = sql & N & ",HJISSEKIRYOU "
        sql = sql & N & ",HJISSEKISU "
        sql = sql & N & ",TENKAIPTN "
        sql = sql & N & ",UPDNAME "
        sql = sql & N & ",UPDDATE "
        sql = sql & N & ") "
        sql = sql & N & "SELECT "
        sql = sql & N & " KHINMEICD "
        sql = sql & N & ",SUM(HJISSEKIRYOU) "
        sql = sql & N & ",SUM(HJISSEKISU) "
        sql = sql & N & ",MAX(TENKAIPTN) "
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & ",SYSDATE "
        sql = sql & N & "FROM ZG340B_W11 "
        sql = sql & N & "WHERE UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & "  AND NENGETU >= '" & dteKonkaiAnbunFrom.Text.Replace("/", "") & "' "
        sql = sql & N & "  AND NENGETU <= '" & dteKonkaiAnbunTo.Text.Replace("/", "") & "' "
        '-->2010.12.22 del by takagi #21
        'sql = sql & N & "  AND TENKAIPTN = '1' "
        '<--2010.12.22 del by takagi #21
        sql = sql & N & "GROUP BY  KHINMEICD "
        _db.executeDB(sql)

    End Sub

    '-------------------------------------------------------------------------------
    '   指定期間集計
    '   （処理概要）引数で指定された集計期間のデータを集約する
    '   ●入力パラメタ  ：prmFromYM 集計開始年月
    '                   ：prmToYM   集計終了年月
    '                   ：prmPtn    展開パターン
    '   ●出力パラメタ  ：なし
    '   ●メソッド戻り値：なし
    '-------------------------------------------------------------------------------
    Private Sub createHanbaiJissekiWkByPtn(ByVal prmFromYM As String, ByVal prmToYM As String, ByVal prmPtn As String)

        Dim sql As String = ""
        sql = sql & N & "INSERT INTO ZG340B_W13 "
        sql = sql & N & "( "
        sql = sql & N & " KHINMEICD "
        sql = sql & N & ",HJISSEKIRYOU "
        sql = sql & N & ",HJISSEKISU "
        sql = sql & N & ",TENKAIPTN "
        sql = sql & N & ",UPDNAME "
        sql = sql & N & ",UPDDATE "
        sql = sql & N & ") "
        sql = sql & N & "SELECT "
        sql = sql & N & " KHINMEICD "
        sql = sql & N & ",SUM(HJISSEKIRYOU) "
        sql = sql & N & ",SUM(HJISSEKISU) "
        '-->2010.12.26 chg by takagi #49
        'sql = sql & N & ",MAX(TENKAIPTN) "
        sql = sql & N & ",CASE WHEN '" & prmPtn & "' = '5T'  THEN '5T' "
        sql = sql & N & "      WHEN '" & prmPtn & "' = '5Y'  THEN '5Y' "
        sql = sql & N & "      WHEN '" & prmPtn & "' = '5YY' THEN '5YY' "
        sql = sql & N & "      ELSE MAX(TENKAIPTN) END "
        '<--2010.12.26 chg by takagi #49
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & ",SYSDATE "
        sql = sql & N & "FROM ZG340B_W11 "
        sql = sql & N & "WHERE UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & "  AND NENGETU >= '" & prmFromYM & "' "
        sql = sql & N & "  AND NENGETU <= '" & prmToYM & "' "
        '-->2010.12.26 chg by takagi #49
        'sql = sql & N & "  AND TENKAIPTN = '" & prmPtn & "' "
        If "5T".Equals(prmPtn) OrElse "5Y".Equals(prmPtn) OrElse "5YY".Equals(prmPtn) Then
            sql = sql & N & "  AND TENKAIPTN = '5' "
        Else
            sql = sql & N & "  AND TENKAIPTN = '" & prmPtn & "' "
        End If
        '<--2010.12.26 chg by takagi #49
        sql = sql & N & "GROUP BY  KHINMEICD "
        _db.executeDB(sql)

    End Sub

    '-------------------------------------------------------------------------------
    '   計画品名毎集計
    '   （処理概要）計画品名と年月単位で販売実績を集計する
    '   ●入力パラメタ  ：なし
    '   ●出力パラメタ  ：なし
    '   ●メソッド戻り値：なし
    '-------------------------------------------------------------------------------
    Private Sub createKeikakuhinHanbaiJisseki()
        Dim sql As String = ""
        sql = sql & N & "INSERT INTO ZG340B_W11 "
        sql = sql & N & "( "
        sql = sql & N & " KHINMEICD "
        sql = sql & N & ",NENGETU "
        sql = sql & N & ",HJISSEKIRYOU "
        sql = sql & N & ",HJISSEKISU "
        sql = sql & N & ",TENKAIPTN "
        sql = sql & N & ",UPDNAME "
        sql = sql & N & ",UPDDATE "
        sql = sql & N & ") "
        sql = sql & N & "SELECT "
        sql = sql & N & " KHINMEICD "
        sql = sql & N & ",NENGETU "
        sql = sql & N & ",SUM(HJISSEKIRYOU) "
        sql = sql & N & ",SUM(HJISSEKISU) "
        sql = sql & N & ",MAX(TENKAIPTN) "
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & ",SYSDATE "
        sql = sql & N & "FROM ZG340B_W10 "
        sql = sql & N & "WHERE UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & "GROUP BY  KHINMEICD,NENGETU "
        _db.executeDB(sql)
    End Sub

    '-------------------------------------------------------------------------------
    '   計画品名CD補完
    '   （処理概要）処理年月-12～処理年月-1のデータを販売実績から抽出し、同時に展開パターンと計画品名CDを補完する
    '   ●入力パラメタ  ：なし
    '   ●出力パラメタ  ：なし
    '   ●メソッド戻り値：なし
    '-------------------------------------------------------------------------------
    Private Sub createHanbaiJisseki()

        Dim sql As String = ""
        sql = sql & N & "INSERT INTO ZG340B_W10 "
        sql = sql & N & "( "
        sql = sql & N & " HINMEICD "
        sql = sql & N & ",KHINMEICD "
        sql = sql & N & ",NENGETU "
        sql = sql & N & ",HJISSEKIRYOU "
        sql = sql & N & ",HJISSEKISU "
        sql = sql & N & ",TENKAIPTN "
        sql = sql & N & ",UPDNAME "
        sql = sql & N & ",UPDDATE "
        sql = sql & N & ") "
        sql = sql & N & "SELECT "
        sql = sql & N & " T10.HINMEICD "
        sql = sql & N & ",M12.KHINMEICD "
        sql = sql & N & ",T10.NENGETU "
        sql = sql & N & ",T10.HJISSEKIRYOU "
        sql = sql & N & ",T10.HJISSEKISU "
        sql = sql & N & ",M11.TT_TENKAIPTN "
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & ",SYSDATE "
        sql = sql & N & "FROM T10HINHANJ T10 "
        sql = sql & N & "INNER JOIN M12SYUYAKU    M12 ON T10.HINMEICD  = M12.HINMEICD "
        sql = sql & N & "INNER JOIN M11KEIKAKUHIN M11 ON M12.KHINMEICD = M11.TT_KHINMEICD "
        sql = sql & N & "  AND M11.TT_SYUBETU = 1 " '（1：在庫）'★★★2011.01.19 add by takagi
        '-->2010.12.22 chg by takagi #21
        'sql = sql & N & "WHERE M11.TT_TENKAIPTN IN ('1','3','4','5') "
        sql = sql & N & "WHERE M11.TT_TENKAIPTN IN ('1','2','3','4','5') "
        '<--2010.12.22 chg by takagi #21
        '-->2010.12.22 chg by takagi #49
        'sql = sql & N & "  AND T10.NENGETU >= '" & Format(DateAdd(DateInterval.Month, -12, CDate(lblSyoriDate.Text & "/01")), "yyyyMM") & "' "
        sql = sql & N & "  AND T10.NENGETU >= '" & Format(DateAdd(DateInterval.Month, -13, CDate(lblSyoriDate.Text & "/01")), "yyyyMM") & "' "
        '<--2010.12.22 chg by takagi #49
        sql = sql & N & "  AND T10.NENGETU <= '" & Format(DateAdd(DateInterval.Month, -1, CDate(lblSyoriDate.Text & "/01")), "yyyyMM") & "' "
        _db.executeDB(sql)

    End Sub

    '-------------------------------------------------------------------------------
    '   個別計画設定
    '   （処理概要）サイズ別展開パターン＝２のデータを個別計画(T12)から販売計画(T13)に投入する
    '   ●入力パラメタ  ：なし
    '   ●出力パラメタ  ：なし
    '   ●メソッド戻り値：なし
    '-------------------------------------------------------------------------------
    '-->2010.12.27 add by takagi #59
    'Private Sub kobetsuInsert()
    Private Sub kobetsuInsert(ByRef prmRefNullMetsukeCnt As Integer)
        '-->2010.12.27 add by takagi #59

        _db.executeDB("delete from T13HANBAI where KHINMEICD in (select KHINMEICD FROM T12HINMEIHANK WHERE TENKAIPTN = '2')")

        Dim sql As String = ""
        sql = sql & N & "INSERT INTO T13HANBAI "
        sql = sql & N & "( "
        sql = sql & N & " KHINMEICD "
        sql = sql & N & ",TENKAIPTN "
        sql = sql & N & ",THANBAIRYOU "
        sql = sql & N & ",YHANBAIRYOU "
        sql = sql & N & ",YYHANBAIRYOU "
        sql = sql & N & ",THANBAIRYOUH "
        sql = sql & N & ",YHANBAIRYOUH "
        sql = sql & N & ",YYHANBAIRYOUH "
        sql = sql & N & ",THANBAIRYOUHK "
        sql = sql & N & ",YHANBAIRYOUHK "
        sql = sql & N & ",YYHANBAIRYOUHK "
        sql = sql & N & ",UPDNAME "
        sql = sql & N & ",UPDDATE "
        '-->2010.12.27 add by takagi #59
        sql = sql & N & ",METSUKE "
        '<--2010.12.27 add by takagi #59
        sql = sql & N & ") "
        '-->2010.12.02 upd by takagi #TM11が変わっていることを考慮して、12の展開パターンは信用しない
        'sql = sql & N & "SELECT "
        'sql = sql & N & " KHINMEICD "
        'sql = sql & N & ",TENKAIPTN "
        'sql = sql & N & ",THANBAIRYOU "
        'sql = sql & N & ",YHANBAIRYOU "
        'sql = sql & N & ",YYHANBAIRYOU "
        'sql = sql & N & ",NULL "
        'sql = sql & N & ",NULL "
        'sql = sql & N & ",NULL "
        'sql = sql & N & ",THANBAIRYOU "
        'sql = sql & N & ",YHANBAIRYOU "
        'sql = sql & N & ",YYHANBAIRYOU "
        'sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        'sql = sql & N & ",SYSDATE "
        'sql = sql & N & "FROM T12HINMEIHANK "
        'sql = sql & N & "WHERE TENKAIPTN = '2' "
        sql = sql & N & "SELECT "
        sql = sql & N & " T12.KHINMEICD "
        sql = sql & N & ",M11.TT_TENKAIPTN  "
        sql = sql & N & ",T12.THANBAIRYOU "
        sql = sql & N & ",T12.YHANBAIRYOU "
        sql = sql & N & ",T12.YYHANBAIRYOU "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",NULL "
        sql = sql & N & ",T12.THANBAIRYOU "
        sql = sql & N & ",T12.YHANBAIRYOU "
        sql = sql & N & ",T12.YYHANBAIRYOU "
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "
        sql = sql & N & ",SYSDATE "
        '-->2010.12.27 add by takagi #59
        sql = sql & N & ",M.METSUKE "       '目付
        '<--2010.12.27 add by takagi #59
        sql = sql & N & "FROM T12HINMEIHANK T12 "
        sql = sql & N & "INNER JOIN M11KEIKAKUHIN M11 ON T12.KHINMEICD = M11.TT_KHINMEICD "
        '-->2010.12.27 add by takagi #59
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
        sql = sql & N & " ON T12.KHINMEICD = M.HINCD "
        '<--2010.12.27 add by takagi #59
        sql = sql & N & "WHERE M11.TT_TENKAIPTN = '2' "
        '<--2010.12.02 upd by takagi #TM11が変わっていることを考慮して、12の展開パターンは信用しない
        _db.executeDB(sql)

        '-->2010.12.27 add by takagi #59
        '目付取得不良の抽出
        sql = ""
        sql = sql & N & "SELECT KHINMEICD  "
        sql = sql & N & "FROM T13HANBAI "
        sql = sql & N & "WHERE TENKAIPTN = '2' "
        sql = sql & N & "  AND METSUKE IS NULL "
        sql = sql & N & "ORDER BY KHINMEICD "
        Dim ds As DataSet = _db.selectDB(sql, RS, prmRefNullMetsukeCnt)
        If prmRefNullMetsukeCnt > 0 Then

            Dim wk As String = ""
            Dim outFilePath As String = ""
            Call UtilClass.dividePathAndFile(StartUp.iniValue.LogFilePath, outFilePath, wk)
            outFilePath = outFilePath & "\" & IMP_LOG_NM

            Dim logBuf As System.Text.StringBuilder = New System.Text.StringBuilder
            logBuf.Append(Format(Now(), "yyyy/MM/dd HH:mm:ss") & "実行" & N)
            logBuf.Append("==========================================================" & N)
            logBuf.Append("■販売実績集計展開処理出力情報■" & N)
            logBuf.Append("  材料票マスタ未登録品名コード（目付の取得が行えなかった品名コード）" & N)
            logBuf.Append("----------------------------------------------------------" & N)
            For i As Integer = 0 To prmRefNullMetsukeCnt - 1
                logBuf.Append(_db.rmNullStr(ds.Tables(RS).Rows(i)("KHINMEICD")) & N)
            Next
            logBuf.Append("==========================================================")
            Dim tw As UtilMDL.Text.UtilTextWriter = New UtilMDL.Text.UtilTextWriter(outFilePath)
            tw.open(False)
            Try
                tw.writeLine(logBuf.ToString)
            Finally
                tw.close()
            End Try

        End If
        '<--2010.12.27 add by takagi #59

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
        sql = sql & N & ",KENNSU1 "    'パターン１件数
        sql = sql & N & ",KENNSU2 "    'パターン２件数
        sql = sql & N & ",KENNSU3 "    'パターン３件数
        sql = sql & N & ",KENNSU4 "    'パターン４件数
        sql = sql & N & ",KENNSU5 "    'パターン５件数
        sql = sql & N & ",NAME1 "      '期間FROM
        sql = sql & N & ",NAME2 "      '期間TO
        sql = sql & N & ",UPDNAME "    '最終更新者
        sql = sql & N & ",UPDDATE "    '最終更新日
        sql = sql & N & ")values( "
        sql = sql & N & "  '" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "' "                             '処理年月
        sql = sql & N & ", '" & _db.rmSQ(lblKeikakuDate.Text.Replace("/", "")) & "' "                           '計画年月
        sql = sql & N & ", '" & _db.rmSQ(PGID) & "' "                                                           '機能ID
        sql = sql & N & ",to_date('" & Format(prmStDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '処理開始日時
        sql = sql & N & ",to_date('" & Format(prmEdDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '処理終了日時
        sql = sql & N & ", (SELECT COUNT(*) FROM T13HANBAI WHERE TENKAIPTN = '1') "                             'Ptn1件数
        sql = sql & N & ", (SELECT COUNT(*) FROM T13HANBAI WHERE TENKAIPTN = '2') "                             'Ptn2件数
        sql = sql & N & ", (SELECT COUNT(*) FROM T13HANBAI WHERE TENKAIPTN = '3') "                             'Ptn3件数
        sql = sql & N & ", (SELECT COUNT(*) FROM T13HANBAI WHERE TENKAIPTN = '4') "                             'Ptn4件数
        sql = sql & N & ", (SELECT COUNT(*) FROM T13HANBAI WHERE TENKAIPTN = '5') "                             'Ptn5件数
        sql = sql & N & ", " & dteKonkaiAnbunFrom.Text.Replace("/", "") & " "                                   '期間FROM
        sql = sql & N & ", " & dteKonkaiAnbunTo.Text.Replace("/", "") & " "                                     '期間TO
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "                                     '最終更新者
        sql = sql & N & ",sysdate "                                                                             '最終更新日
        sql = sql & N & ") "
        _db.executeDB(sql)

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　フォーカス取得イベント
    '------------------------------------------------------------------------------------------------------
    Private Sub txt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dteKonkaiAnbunFrom.GotFocus, dteKonkaiAnbunTo.GotFocus
        Try
            UtilClass.selAll(sender)
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　キープレスイベント
    '------------------------------------------------------------------------------------------------------
    Private Sub txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dteKonkaiAnbunFrom.KeyPress, dteKonkaiAnbunTo.KeyPress
        Try
            UtilClass.moveNextFocus(Me, e)
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

End Class