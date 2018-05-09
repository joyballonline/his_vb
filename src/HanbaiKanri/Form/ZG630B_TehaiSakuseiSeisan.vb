'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）手配データ作成指示(生産管理)
'    （フォームID）ZG630B_TehaiSakuseiSeisan
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
Public Class ZG630B_TehaiSakuseiSeisan
    Inherits System.Windows.Forms.Form

    '-------------------------------------------------------------------------------
    '   構造体定義
    '-------------------------------------------------------------------------------
    Private Structure fixedStringType
        Dim var As String
        Dim size As Integer
    End Structure
    'OCR手配用データ(行種1)定義　(REC SIZE=82)
    Private Structure OCRrec1_def
        Dim T001 As fixedStringType '* 5       '伝送制御コード
        Dim T006 As fixedStringType '* 1       '行種
        Dim T007 As fixedStringType '* 1       '処理区分
        Dim T008 As fixedStringType '* 5       '手配№
        Dim T013 As fixedStringType '* 1       '手配区分
        Dim T014 As fixedStringType '* 1       '製作区分
        Dim T015 As fixedStringType '* 13      '品名コード
        Dim T028 As fixedStringType '* 1       '設計付加記号
        Dim T029 As fixedStringType '* 4       '希望出来日
        Dim T033 As fixedStringType '* 1       '展開区分
        Dim T034 As fixedStringType '* 6       '部分展開指定工程
        Dim T040 As fixedStringType '* 1       '加工長計算区分
        Dim T041 As fixedStringType '* 1       '立会有無
        Dim T042 As fixedStringType '* 4       '立会予定日
        Dim T046 As fixedStringType '* 1       '成績書
        Dim T047 As fixedStringType '* 1       '単長区分
        Dim T048 As fixedStringType '* 7       '手配数量
        Dim T055 As fixedStringType '* 6       '納期
        Dim T061 As fixedStringType '* 1       '品質試験区分
        Dim T062 As fixedStringType '* 2       '持込余長（単長）
        Dim T064 As fixedStringType '* 3       '持込余長（ロット）
        Dim T067 As fixedStringType '* 2       '立会余長（単長）
        Dim T069 As fixedStringType '* 3       '立会余長（ロット）
        Dim T072 As fixedStringType '* 2       '指定社検余長（単長）
        Dim T074 As fixedStringType '* 3       '指定社検余長（ロット長）
        Dim T077 As fixedStringType '* 4       '空白
        Dim T081 As fixedStringType '* 2       'CRLF
        Overrides Function ToString() As String
            Return T001.var & T006.var & T007.var & T008.var & T013.var & T014.var & T015.var & T028.var _
                    & T029.var & T033.var & T034.var & T040.var & T041.var & T042.var & T046.var & T047.var _
                    & T048.var & T055.var & T061.var & T062.var & T064.var & T067.var & T069.var & T072.var _
                    & T074.var & T077.var & T081.var
        End Function
    End Structure

    'OCR手配用データ(行種2)定義　(REC SIZE=82)
    Private Structure OCRrec2_def
        Dim T001 As fixedStringType '* 5       '伝送制御コード
        Dim T006 As fixedStringType '* 1       '行種
        Dim T007 As fixedStringType '* 20      '仕様書番号
        Dim T027 As fixedStringType '* 20      '注文先
        Dim T047 As fixedStringType '* 34      '空白
        Dim T081 As fixedStringType '* 2       'CRLF
        Overrides Function ToString() As String
            Return T001.var & T006.var & T007.var & T027.var & T047.var & T081.var
        End Function
    End Structure

    'OCR手配用データ(行種3)定義　(REC SIZE=82)
    Private Structure OCRrec3_def
        Dim T001 As fixedStringType '* 5       '伝送制御コード
        Dim T006 As fixedStringType '* 1       '行種
        Dim T007 As fixedStringType '* 66      '特記事項
        Dim T073 As fixedStringType '* 8       '空白
        Dim T081 As fixedStringType '* 2       'CRLF
        Overrides Function ToString() As String
            Return T001.var & T006.var & T007.var & T073.var & T081.var
        End Function
    End Structure

    'OCR手配用データ(行種4)定義　(REC SIZE=82)
    Private Structure OCRrec4_def
        Dim T001 As fixedStringType '* 5       '伝送制御コード
        Dim T006 As fixedStringType '* 1       '行種
        Dim T007 As fixedStringType '* 5       '単長_1
        Dim T012 As fixedStringType '* 4       '条数_1
        Dim T016 As fixedStringType '* 6       '巻枠コード_1
        Dim T022 As fixedStringType '* 2       '包装区分_1
        Dim T024 As fixedStringType '* 5       '単長_2
        Dim T029 As fixedStringType '* 4       '条数_2
        Dim T033 As fixedStringType '* 6       '巻枠コード_2
        Dim T039 As fixedStringType '* 2       '包装区分_2
        Dim T041 As fixedStringType '* 5       '単長_3
        Dim T046 As fixedStringType '* 4       '条数_3
        Dim T050 As fixedStringType '* 6       '巻枠コード_3
        Dim T056 As fixedStringType '* 2       '包装区分_3
        Dim T058 As fixedStringType '* 5       '単長_4
        Dim T063 As fixedStringType '* 4       '条数_4
        Dim T067 As fixedStringType '* 6       '巻枠コード_4
        Dim T073 As fixedStringType '* 2       '包装区分_4
        Dim T075 As fixedStringType '* 6       '空白
        Dim T081 As fixedStringType '* 2       'CRLF
        Overrides Function ToString() As String
            Return T001.var & T006.var & T007.var & T012.var & T016.var & T022.var & T024.var & T029.var _
                    & T033.var & T039.var & T041.var & T046.var & T050.var & T056.var & T058.var & T063.var _
                    & T067.var & T073.var & T075.var & T081.var
        End Function
    End Structure

    'OCR手配用データ(行種5)定義　(REC SIZE=82)
    Private Structure OCRrec5_def
        Dim T001 As fixedStringType '* 5       '伝送制御コード
        Dim T006 As fixedStringType '* 1       '行種
        Dim T007 As fixedStringType '* 5       '単長_5
        Dim T012 As fixedStringType '* 4       '条数_5
        Dim T016 As fixedStringType '* 6       '巻枠コード_5
        Dim T022 As fixedStringType '* 2       '包装区分_5
        Dim T024 As fixedStringType '* 5       '単長_6
        Dim T029 As fixedStringType '* 4       '条数_6
        Dim T033 As fixedStringType '* 6       '巻枠コード_6
        Dim T039 As fixedStringType '* 2       '包装区分_6
        Dim T041 As fixedStringType '* 5       '単長_7
        Dim T046 As fixedStringType '* 4       '条数_7
        Dim T050 As fixedStringType '* 6       '巻枠コード_7
        Dim T056 As fixedStringType '* 2       '包装区分_7
        Dim T058 As fixedStringType '* 5       '単長_8
        Dim T063 As fixedStringType '* 4       '条数_8
        Dim T067 As fixedStringType '* 6       '巻枠コード_8
        Dim T073 As fixedStringType '* 2       '包装区分_8
        Dim T075 As fixedStringType '* 6       '空白
        Dim T081 As fixedStringType '* 2       'CRLF
        Overrides Function ToString() As String
            Return T001.var & T006.var & T007.var & T012.var & T016.var & T022.var & T024.var & T029.var _
                    & T033.var & T039.var & T041.var & T046.var & T050.var & T056.var & T058.var & T063.var _
                    & T067.var & T073.var & T075.var & T081.var
        End Function
    End Structure


    'OCR手配用データ(行種6)定義　(REC SIZE=82)
    Private Structure OCRrec6_def
        Dim T001 As fixedStringType '* 5       '伝送制御コード
        Dim T006 As fixedStringType '* 1       '行種
        Dim T007 As fixedStringType '* 6       '標準工程コード
        Dim T013 As fixedStringType '* 6       '（代替）工程コード
        Dim T019 As fixedStringType '* 3       '（代替）工程順位
        Dim T022 As fixedStringType '* 2       '（代替）定員
        Dim T024 As fixedStringType '* 2       '（代替）段取
        Dim T026 As fixedStringType '* 6       '（代替）基準出来高
        Dim T032 As fixedStringType '* 4       'ｽﾀｰﾄﾘｰﾙ余長・ｽﾀｰﾄ
        Dim T036 As fixedStringType '* 4       'ｽﾀｰﾄﾘｰﾙ余長・ﾗｽﾄ
        Dim T040 As fixedStringType '* 4       'ﾗｽﾄﾘｰﾙ余長・ｽﾀｰﾄ
        Dim T044 As fixedStringType '* 4       'ﾗｽﾄﾘｰﾙ余長・ﾗｽﾄ
        Dim T048 As fixedStringType '* 5       '最大巻取長
        Dim T053 As fixedStringType '* 1       '計算制御
        Dim T054 As fixedStringType '* 27      '空白
        Dim T081 As fixedStringType '* 2       'CRLF
        Overrides Function ToString() As String
            Return T001.var & T006.var & T007.var & T013.var & T019.var & T022.var & T024.var & T026.var _
                    & T032.var & T036.var & T040.var & T044.var & T048.var & T053.var & T054.var & T081.var
        End Function
    End Structure

    '-------------------------------------------------------------------------------
    '   定数定義
    '-------------------------------------------------------------------------------
    Private Const RS As String = "RecSet"                       'データセットテーブル名
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const PGID As String = "ZG630B"

    '-------------------- << OCR手配用データ情報 >> ----------------------
    Private Const sSEND_CTRL_CD = "PB110"   '伝送制御コード
    Private Const sGYO_1 = "1"              '行種：1
    Private Const sGYO_2 = "2"              '行種：2
    Private Const sGYO_3 = "3"              '行種：3
    Private Const sGYO_4 = "4"              '行種：4
    Private Const sGYO_5 = "5"              '行種：5
    Private Const sGYO_6 = "6"              '行種：6

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
    Private Sub ZG630B_TehaiSakuseiSeisan_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            sql = sql & N & "    ,(NAME1 || NAME2 || NAME3 || NAME4) mypath "
            sql = sql & N & "    from T91RIREKI "
            sql = sql & N & "    where PGID     = '" & PGID & "' "
            sql = sql & N & ") "
            sql = sql & N & "where RNUM = 1 "
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)
            If iRecCnt < 1 Then
                '履歴なし
                lblJikkouDate.Text = ZC110M_Menu.NON_EXECUTE
                lblZenkaiKensu.Text = ""
                txtPastPass.Text = ""
            Else
                '履歴あり
                lblJikkouDate.Text = _db.rmNullDate(ds.Tables(RS).Rows(0)("SDATEEND"))
                lblZenkaiKensu.Text = Format(_db.rmNullInt(ds.Tables(RS).Rows(0)("KENNSU1")), "#,##0")
                txtPastPass.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("mypath")).Trim
            End If

            '今回実行情報の表示
            '2011/01/28 chg start Sugawara #95
            'lblKonkaiKensu.Text = Format(_db.rmNullInt(_db.selectDB("select count(*) CNT from T51TEHAI ", RS).Tables(RS).Rows(0)("CNT")), "#,##0")
            lblKonkaiKensu.Text = Format(_db.rmNullInt(_db.selectDB("select count(*) CNT from T51TEHAI where GAI_FLG is null ", RS).Tables(RS).Rows(0)("CNT")), "#,##0")
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
            'Dim rtn As DialogResult = _msgHd.dspMSG("confirmRun")
            'If rtn = Windows.Forms.DialogResult.No Or rtn = Windows.Forms.DialogResult.Cancel Then Exit Sub

            Dim myPath As String = ""
            Dim myFile As String = ""
            Call UtilClass.dividePathAndFile(txtPastPass.Text, myPath, myFile)
            Dim executePath As String = UtilMDL.CommonDialog.UtilCmnDlgHandler.saveFileDialog(myPath, "DPB12000", , "OCR手配データ作成先")
            If String.IsNullOrEmpty(executePath) Then Exit Sub

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
                    pb.jobName = "ＯＣＲ手配データ(DPB12000)を作成しています。"
                    pb.oneStep = 1

                    pb.status = "作成中"
                    Dim outputCnt As Integer = 0
                    Call outputOcrTehaiDate(executePath, outputCnt, pb)

                    _db.beginTran()
                    Try
                        pb.status = "ステータス変更中・・・"
                        ed = Now                    '処理終了日時
                        _parentForm.updateSeigyoTbl(PGID, True, st, ed)

                        pb.status = "実行履歴作成"
                        Call insertRireki(executePath, st, ed)                                  '2-1 実行履歴格納
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
    Private Sub insertRireki(ByVal executePath As String, ByVal prmStDt As Date, ByVal prmEdDt As Date)

        Dim path1 As String = ""
        Dim path2 As String = ""
        Dim path3 As String = ""
        Dim path4 As String = ""
        Dim targetPath As String = executePath

        If targetPath.Length < 128 Then
            path1 = targetPath
        Else
            path1 = targetPath.Substring(0, 128)
            targetPath = targetPath.Substring(128)

            If targetPath.Length < 128 Then
                path2 = targetPath
            Else
                path2 = targetPath.Substring(0, 128)
                targetPath = targetPath.Substring(128)

                If targetPath.Length < 128 Then
                    path3 = targetPath
                Else
                    path3 = targetPath.Substring(0, 128)
                    targetPath = targetPath.Substring(128)

                    If targetPath.Length < 128 Then
                        path4 = targetPath
                    Else
                        path4 = targetPath.Substring(0, 128)
                        targetPath = targetPath.Substring(128)
                    End If
                End If
            End If
        End If

        Dim sql As String = ""
        sql = sql & N & "insert into T91RIREKI "
        sql = sql & N & "( "
        sql = sql & N & " SNENGETU "   '処理年月
        sql = sql & N & ",KNENGETU "   '計画年月
        sql = sql & N & ",PGID "       '機能ID
        sql = sql & N & ",SDATESTART " '処理開始日時
        sql = sql & N & ",SDATEEND "   '処理終了日時
        sql = sql & N & ",KENNSU1 "    '実行件数
        sql = sql & N & ",NAME1 "      'パス
        sql = sql & N & ",NAME2 "      'パス
        sql = sql & N & ",NAME3 "      'パス
        sql = sql & N & ",NAME4 "      'パス
        sql = sql & N & ",UPDNAME "    '最終更新者
        sql = sql & N & ",UPDDATE "    '最終更新日
        sql = sql & N & ")values( "
        sql = sql & N & "  '" & _db.rmSQ(lblSyoriDate.Text.Replace("/", "")) & "' "                             '処理年月
        sql = sql & N & ", '" & _db.rmSQ(lblKeikakuDate.Text.Replace("/", "")) & "' "                           '計画年月
        sql = sql & N & ", '" & _db.rmSQ(PGID) & "' "                                                           '機能ID
        sql = sql & N & ",to_date('" & Format(prmStDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '処理開始日時
        sql = sql & N & ",to_date('" & Format(prmEdDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "   '処理終了日時
        sql = sql & N & ", " & CLng(lblKonkaiKensu.Text) & " "                                                  '入力件数
        sql = sql & N & ", " & impDtForStr(path1) & " "                                                            'パス
        sql = sql & N & ", " & impDtForStr(path2) & " "                                                            'パス
        sql = sql & N & ", " & impDtForStr(path3) & " "                                                            'パス
        sql = sql & N & ", " & impDtForStr(path4) & " "                                                            'パス
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "                                     '最終更新者
        sql = sql & N & ",sysdate "                                                                             '最終更新日
        sql = sql & N & ") "
        _db.executeDB(sql)

    End Sub

    Private Function impDtForStr(ByVal prmVal As String) As String
        If "".Equals(prmVal) Then
            Return "NULL"
        Else
            Return "'" & prmVal & "'"
        End If
    End Function

    Private Function initialyzerOCRrec1() As OCRrec1_def
        Dim ret As OCRrec1_def = New OCRrec1_def
        ret.T001.size = 5       '伝送制御コード
        ret.T006.size = 1       '行種
        ret.T007.size = 1       '処理区分
        ret.T008.size = 5       '手配№
        ret.T013.size = 1       '手配区分
        ret.T014.size = 1       '製作区分
        ret.T015.size = 13      '品名コード
        ret.T028.size = 1       '設計付加記号
        ret.T029.size = 4       '希望出来日
        ret.T033.size = 1       '展開区分
        ret.T034.size = 6       '部分展開指定工程
        ret.T040.size = 1       '加工長計算区分
        ret.T041.size = 1       '立会有無
        ret.T042.size = 4       '立会予定日
        ret.T046.size = 1       '成績書
        ret.T047.size = 1       '単長区分
        ret.T048.size = 7       '手配数量
        ret.T055.size = 6       '納期
        ret.T061.size = 1       '品質試験区分
        ret.T062.size = 2       '持込余長（単長）
        ret.T064.size = 3       '持込余長（ロット）
        ret.T067.size = 2       '立会余長（単長）
        ret.T069.size = 3       '立会余長（ロット）
        ret.T072.size = 2       '指定社検余長（単長）
        ret.T074.size = 3       '指定社検余長（ロット長）
        ret.T077.size = 4       '空白
        ret.T081.size = 2       'CRLF
        Return ret
    End Function

    Private Function initialyzerOCRrec2() As OCRrec2_def
        Dim ret As OCRrec2_def = New OCRrec2_def
        ret.T001.size = 5       '伝送制御コード
        ret.T006.size = 1       '行種
        ret.T007.size = 20      '仕様書番号
        ret.T027.size = 20      '注文先
        ret.T047.size = 34      '空白
        ret.T081.size = 2       'CRLF
        Return ret
    End Function

    Private Function initialyzerOCRrec3() As OCRrec3_def
        Dim ret As OCRrec3_def = New OCRrec3_def
        ret.T001.size = 5       '伝送制御コード
        ret.T006.size = 1       '行種
        ret.T007.size = 66      '特記事項
        ret.T073.size = 8       '空白
        ret.T081.size = 2       'CRLF
        Return ret
    End Function

    Private Function initialyzerOCRrec4() As OCRrec4_def
        Dim ret As OCRrec4_def = New OCRrec4_def
        ret.T001.size = 5       '伝送制御コード
        ret.T006.size = 1       '行種
        ret.T007.size = 5       '単長_1
        ret.T012.size = 4       '条数_1
        ret.T016.size = 6       '巻枠コード_1
        ret.T022.size = 2       '包装区分_1
        ret.T024.size = 5       '単長_2
        ret.T029.size = 4       '条数_2
        ret.T033.size = 6       '巻枠コード_2
        ret.T039.size = 2       '包装区分_2
        ret.T041.size = 5       '単長_3
        ret.T046.size = 4       '条数_3
        ret.T050.size = 6       '巻枠コード_3
        ret.T056.size = 2       '包装区分_3
        ret.T058.size = 5       '単長_4
        ret.T063.size = 4       '条数_4
        ret.T067.size = 6       '巻枠コード_4
        ret.T073.size = 2       '包装区分_4
        ret.T075.size = 6       '空白
        ret.T081.size = 2       'CRLF
        Return ret
    End Function

    Private Function initialyzerOCRrec5() As OCRrec5_def
        Dim ret As OCRrec5_def = New OCRrec5_def
        ret.T001.size = 5       '伝送制御コード
        ret.T006.size = 1       '行種
        ret.T007.size = 5       '単長_5
        ret.T012.size = 4       '条数_5
        ret.T016.size = 6       '巻枠コード_5
        ret.T022.size = 2       '包装区分_5
        ret.T024.size = 5       '単長_6
        ret.T029.size = 4       '条数_6
        ret.T033.size = 6       '巻枠コード_6
        ret.T039.size = 2       '包装区分_6
        ret.T041.size = 5       '単長_7
        ret.T046.size = 4       '条数_7
        ret.T050.size = 6       '巻枠コード_7
        ret.T056.size = 2       '包装区分_7
        ret.T058.size = 5       '単長_8
        ret.T063.size = 4       '条数_8
        ret.T067.size = 6       '巻枠コード_8
        ret.T073.size = 2       '包装区分_8
        ret.T075.size = 6       '空白
        ret.T081.size = 2       'CRLF
        Return ret
    End Function

    Private Function initialyzerOCRrec6() As OCRrec6_def
        Dim ret As OCRrec6_def = New OCRrec6_def
        ret.T001.size = 5       '伝送制御コード
        ret.T006.size = 1       '行種
        ret.T007.size = 6       '標準工程コード
        ret.T013.size = 6       '（代替）工程コード
        ret.T019.size = 3       '（代替）工程順位
        ret.T022.size = 2       '（代替）定員
        ret.T024.size = 2       '（代替）段取
        ret.T026.size = 6       '（代替）基準出来高
        ret.T032.size = 4       'ｽﾀｰﾄﾘｰﾙ余長・ｽﾀｰﾄ
        ret.T036.size = 4       'ｽﾀｰﾄﾘｰﾙ余長・ﾗｽﾄ
        ret.T040.size = 4       'ﾗｽﾄﾘｰﾙ余長・ｽﾀｰﾄ
        ret.T044.size = 4       'ﾗｽﾄﾘｰﾙ余長・ﾗｽﾄ
        ret.T048.size = 5       '最大巻取長
        ret.T053.size = 1       '計算制御
        ret.T054.size = 27      '空白
        ret.T081.size = 2       'CRLF
        Return ret
    End Function
    Private Sub setOcrRecValue(ByRef prmRefTarget As fixedStringType, ByVal prmVal As String)
        prmRefTarget.var = LSet(prmVal, prmRefTarget.size)
    End Sub
    '-------------------------------------------------------------------------------
    '　 OCR手配用データ(TEXT)作成
    '   （処理概要）手配ワークＤＢよりOCR手配用データを作成する。
    '-------------------------------------------------------------------------------
    Private Sub outputOcrTehaiDate(ByVal prmExecutePath As String, ByRef prmRefOutputCnt As Integer, ByRef prmRefProgress As UtilProgressBar)

        Dim SQL As String = ""
        SQL = SQL & N & "SELECT "
        SQL = SQL & N & "  TEHAI_NO"         ' 0：手配№
        SQL = SQL & N & " ,SYORI_YM"         ' 1：処理年月
        SQL = SQL & N & " ,SYORI_KBN"        ' 2：処理区分
        SQL = SQL & N & " ,KIBOU_DATE"       ' 3：希望年月日
        'SQL = SQL & N & " ,NOUKI"            ' 4：納期
        SQL = SQL & N & " ,TEHAI_KBN"        ' 5：手配区分
        SQL = SQL & N & " ,SEISAKU_KBN"      ' 6：製作区分
        SQL = SQL & N & " ,SEIZOU_BMN"       ' 7：製造部門
        SQL = SQL & N & " ,DENPYOK"          ' 8：伝票区分
        SQL = SQL & N & " ,TYUMONSAKI"       ' 9：注文先
        SQL = SQL & N & " ,H_SIYOU_CD"       '10：（品名コード）仕様コード
        SQL = SQL & N & " ,H_HIN_CD"         '11：（品名コード）品種コード
        SQL = SQL & N & " ,H_SENSIN_CD"      '12：（品名コード）線心数コード
        SQL = SQL & N & " ,H_SIZE_CD"        '13：（品名コード）サイズコード
        SQL = SQL & N & " ,H_COLOR_CD"       '14：（品名コード）色コード
        SQL = SQL & N & " ,FUKA_CD"          '15：設計付加記号
        SQL = SQL & N & " ,HINMEI"           '16：品名
        SQL = SQL & N & " ,TEHAI_SUU"        '17：手配数量
        SQL = SQL & N & " ,TANCYO_KBN"       '18：単長区分
        SQL = SQL & N & " ,TANCYO"           '19：製作単長
        SQL = SQL & N & " ,JYOSU"            '20：条数
        SQL = SQL & N & " ,MAKI_CD"          '21：巻枠コード
        SQL = SQL & N & " ,HOSO_KBN"         '22：包装区分
        SQL = SQL & N & " ,SIYOUSYO_NO"      '23：仕様書№
        SQL = SQL & N & " ,TOKKI"            '24：特記事項
        SQL = SQL & N & " ,BIKO"             '25：備考
        SQL = SQL & N & " ,HENKO"            '26：変更内容
        SQL = SQL & N & " ,TENKAI_KBN"       '27：展開区分
        SQL = SQL & N & " ,BBNKOUTEI"        '28：指定工程
        SQL = SQL & N & " ,HINSITU_KBN"      '29：品質試験区分
        SQL = SQL & N & " ,KEISAN_KBN"       '30：加工長計算
        SQL = SQL & N & " ,TATIAI_UM"        '31：立会有無
        SQL = SQL & N & " ,TACIAIBI"         '32：立会予定日
        SQL = SQL & N & " ,SEISEKI"          '33：成績書
        SQL = SQL & N & " ,MYTANCYO"         '34：持込余長（単長毎）
        SQL = SQL & N & " ,MYLOT"            '35：持込余長（ロット毎）
        SQL = SQL & N & " ,TYTANCYO"         '36：立会余長（単長毎）
        SQL = SQL & N & " ,TYLOT"            '37：立会余長（ロット毎）
        SQL = SQL & N & " ,SYTANCYO"         '38：指定社検余長（単長毎）
        SQL = SQL & N & " ,SYLOT"            '39：指定社検余長（ロット毎）
        'SQL = SQL & n & " ,HYOJYUNC_1"       '40：標準工程コード_1
        'SQL = SQL & n & " ,KOUTEIC_1"        '41：工程コード_1
        'SQL = SQL & n & " ,KOUTEIJ_1"        '42：工程順位_1
        'SQL = SQL & n & " ,TEIIN_1"          '43：定員_1
        'SQL = SQL & n & " ,DANDORI_1"        '44：段取_1
        'SQL = SQL & n & " ,KIJYUN_1"         '45：基準出来高_1
        'SQL = SQL & n & " ,HYOJYUNC_2"       '46：標準工程コード_2
        'SQL = SQL & n & " ,KOUTEIC_2"        '47：工程コード_2
        'SQL = SQL & n & " ,KOUTEIJ_2"        '48：工程順位_2
        'SQL = SQL & n & " ,TEIIN_2"          '49：定員_2
        'SQL = SQL & n & " ,DANDORI_2"        '50：段取_2
        'SQL = SQL & n & " ,KIJYUN_2"         '51：基準出来高_2
        'SQL = SQL & n & " ,HYOJYUNC_3"       '52：標準工程コード_3
        'SQL = SQL & n & " ,KOUTEIC_3"        '53：工程コード_3
        'SQL = SQL & n & " ,KOUTEIJ_3"        '54：工程順位_3
        'SQL = SQL & n & " ,TEIIN_3"          '55：定員_3
        'SQL = SQL & n & " ,DANDORI_3"        '56：段取_3
        'SQL = SQL & n & " ,KIJYUN_3"         '57：基準出来高_3
        'SQL = SQL & n & " ,HYOJYUNC_4"       '58：標準工程コード_4
        'SQL = SQL & n & " ,KOUTEIC_4"        '59：工程コード_4
        'SQL = SQL & n & " ,KOUTEIJ_4"        '60：工程順位_4
        'SQL = SQL & n & " ,TEIIN_4"          '61：定員_4
        'SQL = SQL & n & " ,DANDORI_4"        '62：段取_4
        'SQL = SQL & n & " ,KIJYUN_4"         '63：基準出来高_4
        'SQL = SQL & n & " ,HYOJYUNC_5"       '64：標準工程コード_5
        'SQL = SQL & n & " ,KOUTEIC_5"        '65：工程コード_5
        'SQL = SQL & n & " ,KOUTEIJ_5"        '66：工程順位_5
        'SQL = SQL & n & " ,TEIIN_5"          '67：定員_5
        'SQL = SQL & n & " ,DANDORI_5"        '68：段取_5
        'SQL = SQL & n & " ,KIJYUN_5"         '69：基準出来高_5
        SQL = SQL & N & " ,UPDDATE"          '70：更新日
        SQL = SQL & N & " FROM T51TEHAI"
        '2011/01/28 add start Sugawara #95
        SQL = SQL & N & " WHERE GAI_FLG IS NULL"   '(初期値：NULL、対象外：1) 対象外データを除く。
        '2011/01/28 add end Sugawara #95
        SQL = SQL & N & " ORDER BY TEHAI_NO"
        Dim ds As DataSet = _db.selectDB(SQL, RS)

        Dim tw As UtilMDL.Text.UtilTextWriter = New UtilMDL.Text.UtilTextWriter(prmExecutePath)
        '-->2011.02.15 upd start by takagi #104 追記モード→上書きモード
        'tw.open()
        Const NO_APPEND As Boolean = False
        tw.open(NO_APPEND)                                                  '追記ではなく、上書き
        '<--2011.02.15 upd end by takagi #104 追記モード→上書きモード
        Try
            For i As Integer = 0 To ds.Tables(RS).Rows.Count - 1

                Dim OCRrec1 As OCRrec1_def = initialyzerOCRrec1()           'OCR手配用データ・行種１
                Call Edit_OCRrec1(ds.Tables(RS).Rows(i), OCRrec1)
                tw.write(OCRrec1)

                Dim OCRrec2 As OCRrec2_def = initialyzerOCRrec2()           'OCR手配用データ・行種２
                Call Edit_OCRrec2(ds.Tables(RS).Rows(i), OCRrec2)
                tw.write(OCRrec2)

                Dim OCRrec3 As OCRrec3_def = initialyzerOCRrec3()           'OCR手配用データ・行種３
                Call Edit_OCRrec3(ds.Tables(RS).Rows(i), OCRrec3)
                tw.write(OCRrec3)

                Dim OCRrec4 As OCRrec4_def = initialyzerOCRrec4()           'OCR手配用データ・行種４
                Call Edit_OCRrec4(ds.Tables(RS).Rows(i), OCRrec4)
                tw.write(OCRrec4)

                Dim OCRrec5 As OCRrec5_def = initialyzerOCRrec5()           'OCR手配用データ・行種５
                Call Edit_OCRrec5(ds.Tables(RS).Rows(i), OCRrec5)
                tw.write(OCRrec5)

                For j As Integer = 0 To 4
                    Dim OCRrec6 As OCRrec6_def = initialyzerOCRrec6()       'OCR手配用データ・行種６
                    Call Edit_OCRrec6(ds.Tables(RS).Rows(i), OCRrec6, j)
                    If CLng(OCRrec6.T019.var) <> 0 Then
                        '工程順位が０以外の場合、代替指定レコードを出力
                        tw.write(OCRrec6)
                    End If
                Next

            Next

        Finally
            tw.close()
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　 OCR手配用データ・行種１編集
    '   （処理概要）OCR手配用データ・行種１を編集する。
    '-------------------------------------------------------------------------------
    Private Sub Edit_OCRrec1(ByVal prmRow As DataRow, ByRef prmRefOcrRec1 As OCRrec1_def)

        Dim sHinCD As String = ""
        sHinCD += UtilClass.setDataLen(_db.rmNullStr(prmRow("H_SIYOU_CD")), 2)
        sHinCD += UtilClass.setDataLen(_db.rmNullStr(prmRow("H_HIN_CD")), 3)
        sHinCD += UtilClass.setDataLen(_db.rmNullStr(prmRow("H_SENSIN_CD")), 3)
        sHinCD += UtilClass.setDataLen(_db.rmNullStr(prmRow("H_SIZE_CD")), 2)
        sHinCD += UtilClass.setDataLen(_db.rmNullStr(prmRow("H_COLOR_CD")), 3)

        'OCR手配用データ・行種１編集
        With prmRefOcrRec1
            setOcrRecValue(.T001, sSEND_CTRL_CD)                                            '伝送制御コード
            setOcrRecValue(.T006, sGYO_1)                                                   '行種
            setOcrRecValue(.T007, _db.rmNullInt(prmRow("SYORI_KBN")))                       '処理区分
            setOcrRecValue(.T008, Format(_db.rmNullInt(prmRow("TEHAI_NO")), "00000"))       '手配№
            setOcrRecValue(.T013, _db.rmNullInt(prmRow("TEHAI_KBN")))                       '手配区分
            setOcrRecValue(.T014, _db.rmNullInt(prmRow("SEISAKU_KBN")))                     '製作区分
            setOcrRecValue(.T015, sHinCD)                                                   '品名コード
            setOcrRecValue(.T028, _db.rmNullStr(prmRow("FUKA_CD")))                         '設計付加記号
            setOcrRecValue(.T029, _db.rmNullStr(prmRow("KIBOU_DATE")).PadLeft(8, "0").Substring(4)) '希望出来日
            setOcrRecValue(.T033, _db.rmNullInt(prmRow("TENKAI_KBN")))                      '展開区分
            setOcrRecValue(.T034, _db.rmNullStr(prmRow("BBNKOUTEI")))                       '部分展開指定工程
            setOcrRecValue(.T040, _db.rmNullInt(prmRow("KEISAN_KBN")))                      '加工計算区分
            setOcrRecValue(.T041, _db.rmNullInt(prmRow("TATIAI_UM")))                       '立会有無
            setOcrRecValue(.T042, _db.rmNullStr(prmRow("TACIAIBI")).PadLeft(8, "0").Substring(4)) '立会予定日
            setOcrRecValue(.T046, _db.rmNullInt(prmRow("SEISEKI")))                         '成績書
            setOcrRecValue(.T047, _db.rmNullInt(prmRow("TANCYO_KBN")))                      '単長区分
            setOcrRecValue(.T048, Format(_db.rmNullInt(prmRow("TEHAI_SUU")), "0000000"))    '手配数量
            'setOcrRecValue(.T055, _db.rmNullStr(prmRow("NOUKI")).PadLeft(8, "0").Substring(2)) '納期
            setOcrRecValue(.T055, "") '納期
            setOcrRecValue(.T061, _db.rmNullInt(prmRow("HINSITU_KBN")))                     '品質試験区分
            setOcrRecValue(.T062, Format(_db.rmNullDouble(prmRow("MYTANCYO")) * 10, "00"))  '持込余長（単長）
            setOcrRecValue(.T064, Format(_db.rmNullDouble(prmRow("MYLOT")) * 100, "0000"))  '持込余長（ロット）
            setOcrRecValue(.T067, Format(_db.rmNullDouble(prmRow("TYTANCYO")) * 10, "00"))  '立会余長（単長）
            setOcrRecValue(.T069, Format(_db.rmNullDouble(prmRow("TYLOT")) * 100, "0000"))  '立会余長（ロット）
            setOcrRecValue(.T072, Format(_db.rmNullDouble(prmRow("SYTANCYO")) * 10, "00"))  '指定社検（単長）
            setOcrRecValue(.T074, Format(_db.rmNullDouble(prmRow("SYLOT")) * 100, "0000"))  '指定社検（ロット）
            setOcrRecValue(.T077, Space(4))                                                 '空白
            setOcrRecValue(.T081, ControlChars.CrLf)                                        'CRLF
        End With

    End Sub

    '-------------------------------------------------------------------------------
    '　 OCR手配用データ・行種２編集
    '   （処理概要）OCR手配用データ・行種２を編集する。
    '-------------------------------------------------------------------------------
    Private Sub Edit_OCRrec2(ByVal prmRow As DataRow, ByRef prmRefOcrRec2 As OCRrec2_def)

        'OCR手配用データ・行種２編集
        With prmRefOcrRec2
            setOcrRecValue(.T001, sSEND_CTRL_CD)                                        '伝送制御コード
            setOcrRecValue(.T006, sGYO_2)                                               '行種
            setOcrRecValue(.T007, _db.rmNullStr(prmRow("SIYOUSYO_NO")))                 '仕様書番号
            setOcrRecValue(.T027, _db.rmNullStr(prmRow("TYUMONSAKI")))                  '注文先
            setOcrRecValue(.T047, Space(34))                                            '空白
            setOcrRecValue(.T081, ControlChars.CrLf)                                    'CRLF
        End With

    End Sub

    '-------------------------------------------------------------------------------
    '　 OCR手配用データ・行種３編集
    '   （処理概要）OCR手配用データ・行種３を編集する。
    '-------------------------------------------------------------------------------
    Private Sub Edit_OCRrec3(ByVal prmRow As DataRow, ByRef prmRefOcrRec3 As OCRrec3_def)

        'OCR手配用データ・行種３編集
        With prmRefOcrRec3
            setOcrRecValue(.T001, sSEND_CTRL_CD)                                        '伝送制御コード
            setOcrRecValue(.T006, sGYO_3)                                               '行種
            setOcrRecValue(.T007, _db.rmNullStr(prmRow("TOKKI")))                       '特記事項
            setOcrRecValue(.T073, Space(8))                                             '空白
            setOcrRecValue(.T081, ControlChars.CrLf)                                    'CRLF
        End With

    End Sub

    '-------------------------------------------------------------------------------
    '　 OCR手配用データ・行種４編集
    '   （処理概要）OCR手配用データ・行種４を編集する。
    '-------------------------------------------------------------------------------
    Private Sub Edit_OCRrec4(ByVal prmRow As DataRow, ByRef prmRefOcrRec4 As OCRrec4_def)

        'OCR手配用データ・行種４編集
        With prmRefOcrRec4
            setOcrRecValue(.T001, sSEND_CTRL_CD)                                        '伝送制御コード
            setOcrRecValue(.T006, sGYO_4)                                               '行種
            setOcrRecValue(.T007, Format(_db.rmNullInt(prmRow("TANCYO")), "00000"))     '単長_1
            setOcrRecValue(.T012, Format(_db.rmNullInt(prmRow("JYOSU")), "0000"))       '条数_1
            setOcrRecValue(.T016, Format(_db.rmNullInt(prmRow("MAKI_CD")), "000000"))   '巻枠コード_1
            setOcrRecValue(.T022, _db.rmNullStr(prmRow("HOSO_KBN")))                    '包装区分_1
            setOcrRecValue(.T024, "00000")                                              '単長_2
            setOcrRecValue(.T029, "0000")                                               '条数_2
            setOcrRecValue(.T033, "000000")                                             '巻枠コード_2
            setOcrRecValue(.T039, Space(2))                                             '包装区分_2
            setOcrRecValue(.T041, "00000")                                              '単長_3
            setOcrRecValue(.T046, "0000")                                               '条数_3
            setOcrRecValue(.T050, "000000")                                             '巻枠コード_3
            setOcrRecValue(.T056, Space(2))                                             '包装区分_3
            setOcrRecValue(.T058, "00000")                                              '単長_4
            setOcrRecValue(.T063, "0000")                                               '条数_4
            setOcrRecValue(.T067, "000000")                                             '巻枠コード_4
            setOcrRecValue(.T073, Space(2))                                             '包装区分_4
            setOcrRecValue(.T075, Space(6))                                             '空白
            setOcrRecValue(.T081, ControlChars.CrLf)                                    'CRLF
        End With

    End Sub


    '-------------------------------------------------------------------------------
    '　 OCR手配用データ・行種５編集
    '   （処理概要）OCR手配用データ・行種５を編集する。
    '-------------------------------------------------------------------------------
    Private Sub Edit_OCRrec5(ByVal prmRow As DataRow, ByRef prmRefOcrRec5 As OCRrec5_def)

        'OCR手配用データ・行種５編集
        With prmRefOcrRec5
            setOcrRecValue(.T001, sSEND_CTRL_CD)    '伝送制御コード
            setOcrRecValue(.T006, sGYO_5)           '行種
            setOcrRecValue(.T007, "00000")          '単長_5
            setOcrRecValue(.T012, "0000")           '条数_5
            setOcrRecValue(.T016, "000000")         '巻枠コード_5
            setOcrRecValue(.T022, Space(2))         '包装区分_5
            setOcrRecValue(.T024, "00000")          '単長_6
            setOcrRecValue(.T029, "0000")           '条数_6
            setOcrRecValue(.T033, "000000")         '巻枠コード_6
            setOcrRecValue(.T039, Space(2))         '包装区分_6
            setOcrRecValue(.T041, "00000")          '単長_7
            setOcrRecValue(.T046, "0000")           '条数_7
            setOcrRecValue(.T050, "000000")         '巻枠コード_7
            setOcrRecValue(.T056, Space(2))         '包装区分_7
            setOcrRecValue(.T058, "00000")          '単長_8
            setOcrRecValue(.T063, "0000")           '条数_8
            setOcrRecValue(.T067, "000000")         '巻枠コード_8
            setOcrRecValue(.T073, Space(2))         '包装区分_8
            setOcrRecValue(.T075, Space(6))         '空白
            setOcrRecValue(.T081, ControlChars.CrLf) 'CRLF
        End With

    End Sub


    '-------------------------------------------------------------------------------
    '　 OCR手配用データ・行種６編集
    '   （処理概要）OCR手配用データ・行種６を編集する。
    '-------------------------------------------------------------------------------
    Private Sub Edit_OCRrec6(ByVal prmRow As DataRow, ByRef prmRefOcrRec6 As OCRrec6_def, ByVal prmRowCnt As Long)
        With prmRefOcrRec6

            'OCR手配用データ・行種６編集
            setOcrRecValue(.T001, sSEND_CTRL_CD)                '伝送制御コード
            setOcrRecValue(.T006, sGYO_6)                       '行種

            ''setOcrRecValue(.T007, _db.rmNullStr(prmRow("HYOJYUNC_" & CStr(prmRowCnt + 1))))                      '標準工程コード
            ''setOcrRecValue(.T013, _db.rmNullStr(prmRow("KOUTEIC_" & CStr(prmRowCnt + 1))))                       '（代替）工程コード
            ''setOcrRecValue(.T019, Format(_db.rmNullInt(prmRow("KOUTEIJ_" & CStr(prmRowCnt + 1))), "000"))        '（代替）工程順位
            ''setOcrRecValue(.T022, Format(_db.rmNullDouble(prmRow("TEIIN_" & CStr(prmRowCnt + 1))) * 10, "00"))   '（代替）定員
            ''setOcrRecValue(.T024, Format(_db.rmNullDouble(prmRow("DANDORI_" & CStr(prmRowCnt + 1))) * 10, "00")) '（代替）段取
            ''setOcrRecValue(.T026, Format(_db.rmNullInt(prmRow("KIJYUN_" & CStr(prmRowCnt + 1))), "000000"))      '（代替）基準出来高
            setOcrRecValue(.T007, "")                           '標準工程コード
            setOcrRecValue(.T013, "")                           '（代替）工程コード
            setOcrRecValue(.T019, Format(0, "000"))             '（代替）工程順位
            setOcrRecValue(.T022, Format(0, "00"))              '（代替）定員
            setOcrRecValue(.T024, Format(0, "00"))              '（代替）段取
            setOcrRecValue(.T026, Format(0, "000000"))          '（代替）基準出来高

            setOcrRecValue(.T032, "0000")                       'ｽﾀｰﾄﾘｰﾙ余長・ｽﾀｰﾄ
            setOcrRecValue(.T036, "0000")                       'ｽﾀｰﾄﾘｰﾙ余長・ﾗｽﾄ
            setOcrRecValue(.T040, "0000")                       'ﾗｽﾄﾘｰﾙ余長・ｽﾀｰﾄ
            setOcrRecValue(.T044, "0000")                       'ﾗｽﾄﾘｰﾙ余長・ﾗｽﾄ
            setOcrRecValue(.T048, "00000")                      '最大巻取長
            setOcrRecValue(.T053, "0")                          '計算制御
            setOcrRecValue(.T054, Space(27))                    '空白
            setOcrRecValue(.T081, ControlChars.CrLf)            'CRLF
        End With
    End Sub
End Class