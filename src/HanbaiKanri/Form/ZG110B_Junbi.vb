'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）準備処理
'    （フォームID）ZG110B_Junbi
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   高木        2010/10/15                 新規              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB

Public Class ZG110B_Junbi
    Inherits System.Windows.Forms.Form

    '-------------------------------------------------------------------------------
    '   定数定義
    '-------------------------------------------------------------------------------
    Private Const RS As String = "RecSet"                       'データセットテーブル名
    Private Const N As String = ControlChars.NewLine            '改行文字

    Private Const PGID As String = "ZG110B"                     '+モード(1:準備処理/2:更新処理)
    Public Const BOOTMODE_INIT As String = "1"                  '1:準備処理
    Public Const BOOTMODE_UPD As String = "2"                   '2:更新処理

    '-------------------------------------------------------------------------------
    '   変数定義
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu
    Private _bootMode As String = ""    '起動モード
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
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, _
                   ByRef prmRefDbHd As UtilDBIf, _
                   ByRef prmRefForm As ZC110M_Menu, _
                   ByVal prmBootMode As String, _
                   ByVal prmUpdFlg As Boolean)
        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _parentForm = prmRefForm                                            '親フォーム
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        If BOOTMODE_INIT.Equals(prmBootMode) OrElse _
           BOOTMODE_UPD.Equals(prmBootMode) Then                            '起動モード(準備処理/更新処理)
            _bootMode = prmBootMode
        Else
            Throw New UsrDefException("起動モードに誤りがあります。(" & BOOTMODE_INIT & ":準備処理/" & BOOTMODE_UPD & ":更新処理)")
        End If
        _updFlg = prmUpdFlg                                                 '更新可否
    End Sub

    '-------------------------------------------------------------------------------
    '　フォームロードイベント
    '-------------------------------------------------------------------------------
    Private Sub ZG110B_Junbi_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            '描画関係の設定
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           'サイズが変更されたときに、コントロールがコントロール自体を再描画するかどうかを示す値を設定
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '描画はバッファで実行され、完了後に、結果が画面に出力されるよう設定
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  'コントロールは、画面に直接ではなく、まずバッファに描画されます。これにより、ちらつきを抑えることができます。
            Me.SetStyle(ControlStyles.UserPaint, True)              'コントロールは、オペレーティング システムによってではなく、独自に描画されるよう設定
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   'コントロールはウィンドウ メッセージ WM_ERASEBKGND を無視するように設定

            '画面タイトル設定
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
    '   （処理概要）画面の各項目を初期化する
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
            sql = sql & N & "     RECORDID "
            sql = sql & N & "    ,ROW_NUMBER() OVER (ORDER BY RECORDID desc) RNUM "
            sql = sql & N & "    ,SNENGETU "
            sql = sql & N & "    ,KNENGETU "
            sql = sql & N & "    ,PGID "
            sql = sql & N & "    ,SDATESTART "
            sql = sql & N & "    ,SDATEEND "
            sql = sql & N & "    from T91RIREKI "
            sql = sql & N & "    where PGID = '" & PGID & BOOTMODE_INIT & "' "
            sql = sql & N & ") "
            sql = sql & N & "where RNUM = 1 "
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)
            If iRecCnt < 1 Then
                '履歴なし
                lblPreviousRunDt.Text = ZC110M_Menu.NON_EXECUTE                                                     '前回実行日時
                lblPreviousRun_SyoriYM.Text = ""                                                                    '前回実行時処理年月
                lblPreviousRun_KeikakuYM.Text = ""                                                                  '前回実行時計画年月
                dteSyoriDate.Text = Format(Now, "yyyy/MM")                                                          '処理年月
                dteKeikakuDate.Text = Format(DateAdd(DateInterval.Month, 1, Now), "yyyy/MM")                        '計画年月
            Else
                '履歴あり
                lblPreviousRunDt.Text = _db.rmNullDate(ds.Tables(RS).Rows(0)("SDATEEND"))                           '前回実行日時
                lblPreviousRun_SyoriYM.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("SNENGETU")).Substring(0, 4) & "/" & _
                                              _db.rmNullStr(ds.Tables(RS).Rows(0)("SNENGETU")).Substring(4, 2)      '前回実行時処理年月
                lblPreviousRun_KeikakuYM.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("KNENGETU")).Substring(0, 4) & "/" & _
                                                _db.rmNullStr(ds.Tables(RS).Rows(0)("KNENGETU")).Substring(4, 2)    '前回実行時計画年月
                If _bootMode.Equals(BOOTMODE_INIT) Then
                    '準備処理
                    dteSyoriDate.Text = Format(DateAdd(DateInterval.Month, 1, CDate(lblPreviousRun_SyoriYM.Text & "/01")), "yyyy/MM")   '処理年月
                    dteKeikakuDate.Text = Format(DateAdd(DateInterval.Month, 1, CDate(dteSyoriDate.Text & "/01")), "yyyy/MM")           '計画年月
                ElseIf _bootMode.Equals(BOOTMODE_UPD) Then
                    '更新処理
                    dteSyoriDate.Text = lblPreviousRun_SyoriYM.Text                                                                     '処理年月
                    dteKeikakuDate.Text = Format(DateAdd(DateInterval.Month, 1, CDate(dteSyoriDate.Text & "/01")), "yyyy/MM")           '計画年月
                End If
            End If

            '起動モード分岐
            If _bootMode.Equals(BOOTMODE_INIT) Then
                '準備処理
                numSyuttaibi1.Text = ""                                                                         '希望出来日１
                numSyuttaibi2.Text = ""                                                                         '希望出来日２
                numSyuttaibi3.Text = ""                                                                         '希望出来日３
                numSyuttaibi4.Text = ""                                                                         '希望出来日４
                numSyuttaibi5.Text = ""                                                                         '希望出来日５
                numSyuttaibi6.Text = ""                                                                         '希望出来日６
                lblSyuttaibiUpdDt.Text = ZC110M_Menu.NON_EXECUTE                                                '前回更新日時
                btnKousin.Enabled = False                                                                       '更新ボタン使用不可
                btnJikkou.Enabled = _updFlg                                                                     '実行ボタンは更新可否フラグに依存
            ElseIf _bootMode.Equals(BOOTMODE_UPD) Then
                '更新処理
                Dim d As DataSet = _db.selectDB("select KIBOU1,KIBOU2,KIBOU3,KIBOU4,KIBOU5,KIBOU6,UPDDATE from T01KEIKANRI", RS, iRecCnt)
                If iRecCnt <> 1 Then Throw New UsrDefException("計画管理TBLのレコード構成が不正です。")
                With d.Tables(RS).Rows(0)
                    numSyuttaibi1.Text = _db.rmNullStr(.Item("KIBOU1")).PadLeft(8, " "c).Substring(6, 2).Trim   '希望出来日１
                    numSyuttaibi2.Text = _db.rmNullStr(.Item("KIBOU2")).PadLeft(8, " "c).Substring(6, 2).Trim   '希望出来日２
                    numSyuttaibi3.Text = _db.rmNullStr(.Item("KIBOU3")).PadLeft(8, " "c).Substring(6, 2).Trim   '希望出来日３
                    numSyuttaibi4.Text = _db.rmNullStr(.Item("KIBOU4")).PadLeft(8, " "c).Substring(6, 2).Trim   '希望出来日４
                    numSyuttaibi5.Text = _db.rmNullStr(.Item("KIBOU5")).PadLeft(8, " "c).Substring(6, 2).Trim   '希望出来日５
                    numSyuttaibi6.Text = _db.rmNullStr(.Item("KIBOU6")).PadLeft(8, " "c).Substring(6, 2).Trim   '希望出来日６
                    lblSyuttaibiUpdDt.Text = _db.rmNullDate(.Item("UPDDATE"))                                   '前回更新日時
                End With
                btnJikkou.Enabled = False                                                                       '実行ボタン使用不可
                btnKousin.Enabled = _updFlg                                                                     '更新ボタンは更新可否フラグに依存
                dteSyoriDate.Enabled = False                                                                    '処理年月使用不可
                dteKeikakuDate.Enabled = False                                                                  '計画年月使用不可
            Else
                Throw New UsrDefException("起動モードの整合性が取れません。現在値：" & _bootMode)
            End If

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
    '　フォーカス取得イベント
    '------------------------------------------------------------------------------------------------------
    Private Sub txt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dteSyoriDate.GotFocus, dteKeikakuDate.GotFocus, numSyuttaibi1.GotFocus, numSyuttaibi2.GotFocus, numSyuttaibi3.GotFocus, numSyuttaibi4.GotFocus, numSyuttaibi5.GotFocus, numSyuttaibi6.GotFocus
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
    Private Sub txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dteSyoriDate.KeyPress, dteKeikakuDate.KeyPress, numSyuttaibi1.KeyPress, numSyuttaibi2.KeyPress, numSyuttaibi3.KeyPress, numSyuttaibi4.KeyPress, numSyuttaibi5.KeyPress, numSyuttaibi6.KeyPress
        Try
            UtilClass.moveNextFocus(Me, e)
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　実行ボタン押下イベント
    '------------------------------------------------------------------------------------------------------
    Private Sub btnJikkou_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJikkou.Click
        Try
            '入力チェック
            Call checkInit()                                                                    '処理年月/計画年月
            Call checkUpdate()                                                                  '希望出来日

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
                    pb.jobName = "準備処理を実行しています。"
                    pb.oneStep = 1
                    pb.maxVal = 29 '(バックアップ/クリア対象12TBL＋処理数4)

                    _db.beginTran()
                    Try
                        pb.status = "計画管理TBL更新"
                        Call updateKeikakuKanri() : pb.value += 1                                   '1-0 計画管理TBL更新

                        pb.status = "データバックアップ中・・・"
                        Call backUpTrnTbl(pb)                                                       '2-0 TRNバックアップ

                        pb.status = "前月データ消去中・・・"
                        Call deleteTrnTbl(pb)                                                       '3-0 TRN初期化

                        pb.status = "ステータス変更中・・・"
                        Call updateSeigyoInit() : pb.value += 1                                     '4-0 処理制御初期化
                        ed = Now                                                                    '処理終了日時
                        _parentForm.updateSeigyoTbl(PGID & _bootMode, True, st, ed) : pb.value += 1 '4-1 計画情報設定

                        pb.status = "実行履歴作成"
                        insertRireki(st, ed) : pb.value += 1                                        '5-0 実行履歴格納

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

    '------------------------------------------------------------------------------------------------------
    '　更新ボタン押下イベント
    '------------------------------------------------------------------------------------------------------
    Private Sub btnKousin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKousin.Click
        Try
            '入力チェック
            Call checkUpdate()                                                                  '希望出来日

            '実行確認（更新します。よろしいですか？）
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmUpdate")
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
                    pb.jobName = "更新処理を実行しています。"
                    pb.oneStep = 1
                    pb.maxVal = 3 '(処理数3)

                    _db.beginTran()
                    Try
                        pb.status = "計画管理TBL更新"
                        Call updateKeikakuKanri(True) : pb.value += 1                               '1-0 計画管理TBL更新

                        pb.status = "ステータス変更中・・・"
                        ed = Now                                                                    '処理終了日時
                        _parentForm.updateSeigyoTbl(PGID & _bootMode, True, st, ed) : pb.value += 1 '4-1 計画情報設定

                        pb.status = "実行履歴作成"
                        insertRireki(st, ed) : pb.value += 1                                        '5-0 実行履歴格納

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
    '   準備処理用入力チェック
    '   （処理概要）処理年月/計画年月の必須/大小チェックを行う
    '   ●入力パラメタ  ：なし
    '   ●出力パラメタ  ：なし
    '   ●メソッド戻り値：なし
    '-------------------------------------------------------------------------------
    Private Sub checkInit()
        '入力チェック
        If "/".Equals(dteSyoriDate.Text.Trim) Then                                                 '未入力
            Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput"), dteSyoriDate)
        ElseIf "/".Equals(dteKeikakuDate.Text.Trim) Then                                           '未入力
            Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput"), dteKeikakuDate)
        ElseIf CDate(dteSyoriDate.Text & "/01") >= CDate(dteKeikakuDate.Text & "/01") Then
            Throw New UsrDefException("大小関係が不正です。", _msgHd.getMSG("ErrDaiSyoChk"), dteKeikakuDate)
        ElseIf DateAdd(DateInterval.Month, 1, CDate(dteSyoriDate.Text & "/01")) <> CDate(dteKeikakuDate.Text & "/01") Then
            Throw New UsrDefException("無効な日付が入力されています。", _msgHd.getMSG("ImputedInvalidDate"), dteKeikakuDate)
        End If
    End Sub

    '-------------------------------------------------------------------------------
    '   更新処理用入力チェック
    '   （処理概要）出来日１～６の未入力/暦上日/大小チェックを行う
    '   ●入力パラメタ  ：なし
    '   ●出力パラメタ  ：なし
    '   ●メソッド戻り値：なし
    '-------------------------------------------------------------------------------
    Private Sub checkUpdate()

        '入力チェック
        If "".Equals(numSyuttaibi1.Text) Then                                                       '未入力
            Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput"), numSyuttaibi1)
        ElseIf "".Equals(numSyuttaibi2.Text) Then                                                   '未入力
            Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput"), numSyuttaibi2)
        ElseIf "".Equals(numSyuttaibi3.Text) Then                                                   '未入力
            Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput"), numSyuttaibi3)
        ElseIf "".Equals(numSyuttaibi4.Text) Then                                                   '未入力
            Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput"), numSyuttaibi4)
        ElseIf "".Equals(numSyuttaibi5.Text) Then                                                   '未入力
            Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput"), numSyuttaibi5)
        ElseIf "".Equals(numSyuttaibi6.Text) Then                                                   '未入力
            Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput"), numSyuttaibi6)
        ElseIf IsDate(dteKeikakuDate.Text & "/" & numSyuttaibi1.Text.PadLeft(2, "0"c)) = False Then '暦上日不正
            Throw New UsrDefException("日付が不正です。", _msgHd.getMSG("ImputedInvalidDate"), numSyuttaibi1)
        ElseIf IsDate(dteKeikakuDate.Text & "/" & numSyuttaibi2.Text.PadLeft(2, "0"c)) = False Then '暦上日不正
            Throw New UsrDefException("日付が不正です。", _msgHd.getMSG("ImputedInvalidDate"), numSyuttaibi2)
        ElseIf IsDate(dteKeikakuDate.Text & "/" & numSyuttaibi3.Text.PadLeft(2, "0"c)) = False Then '暦上日不正
            Throw New UsrDefException("日付が不正です。", _msgHd.getMSG("ImputedInvalidDate"), numSyuttaibi3)
        ElseIf IsDate(dteKeikakuDate.Text & "/" & numSyuttaibi4.Text.PadLeft(2, "0"c)) = False Then '暦上日不正
            Throw New UsrDefException("日付が不正です。", _msgHd.getMSG("ImputedInvalidDate"), numSyuttaibi4)
        ElseIf IsDate(dteKeikakuDate.Text & "/" & numSyuttaibi5.Text.PadLeft(2, "0"c)) = False Then '暦上日不正
            Throw New UsrDefException("日付が不正です。", _msgHd.getMSG("ImputedInvalidDate"), numSyuttaibi5)
        ElseIf IsDate(dteKeikakuDate.Text & "/" & numSyuttaibi6.Text.PadLeft(2, "0"c)) = False Then '暦上日不正
            Throw New UsrDefException("日付が不正です。", _msgHd.getMSG("ImputedInvalidDate"), numSyuttaibi6)
        ElseIf CInt(numSyuttaibi1.Text) >= CInt(numSyuttaibi2.Text) Then                            '大小不正
            Throw New UsrDefException("大小関係が不正です。", _msgHd.getMSG("ErrDaiSyoChk"), numSyuttaibi2)
        ElseIf CInt(numSyuttaibi2.Text) >= CInt(numSyuttaibi3.Text) Then                            '大小不正
            Throw New UsrDefException("大小関係が不正です。", _msgHd.getMSG("ErrDaiSyoChk"), numSyuttaibi3)
        ElseIf CInt(numSyuttaibi3.Text) >= CInt(numSyuttaibi4.Text) Then                            '大小不正
            Throw New UsrDefException("大小関係が不正です。", _msgHd.getMSG("ErrDaiSyoChk"), numSyuttaibi4)
        ElseIf CInt(numSyuttaibi4.Text) >= CInt(numSyuttaibi5.Text) Then                            '大小不正
            Throw New UsrDefException("大小関係が不正です。", _msgHd.getMSG("ErrDaiSyoChk"), numSyuttaibi5)
        ElseIf CInt(numSyuttaibi5.Text) >= CInt(numSyuttaibi6.Text) Then                            '大小不正
            Throw New UsrDefException("大小関係が不正です。", _msgHd.getMSG("ErrDaiSyoChk"), numSyuttaibi6)
        End If

    End Sub

    '-------------------------------------------------------------------------------
    '   計画管理TBL更新
    '   （処理概要）処理年月/計画年月、ならびに希望出来日１～６を更新する
    '   ●入力パラメタ  ：[prmOnlyUpdate]   希望出来日のみの更新とするか否か
    '   ●出力パラメタ  ：なし
    '   ●メソッド戻り値：なし
    '-------------------------------------------------------------------------------
    Private Sub updateKeikakuKanri(Optional ByVal prmOnlySyuttaiUpdate As Boolean = False)

        Dim sql As String = ""
        sql = sql & N & "update T01KEIKANRI set "
        If prmOnlySyuttaiUpdate = False Then                                                        '出来日のみ更新か？
            sql = sql & N & " SNENGETU = '" & _db.rmSQ(dteSyoriDate.Text.Replace("/", "")) & "', "
            sql = sql & N & " KNENGETU = '" & _db.rmSQ(dteKeikakuDate.Text.Replace("/", "")) & "', "
        End If
        sql = sql & N & " KIBOU1   =  " & _db.rmSQ(dteKeikakuDate.Text.Replace("/", "") & numSyuttaibi1.Text.PadLeft(2, "0"c)) & ", "
        sql = sql & N & " KIBOU2   =  " & _db.rmSQ(dteKeikakuDate.Text.Replace("/", "") & numSyuttaibi2.Text.PadLeft(2, "0"c)) & ", "
        sql = sql & N & " KIBOU3   =  " & _db.rmSQ(dteKeikakuDate.Text.Replace("/", "") & numSyuttaibi3.Text.PadLeft(2, "0"c)) & ", "
        sql = sql & N & " KIBOU4   =  " & _db.rmSQ(dteKeikakuDate.Text.Replace("/", "") & numSyuttaibi4.Text.PadLeft(2, "0"c)) & ", "
        sql = sql & N & " KIBOU5   =  " & _db.rmSQ(dteKeikakuDate.Text.Replace("/", "") & numSyuttaibi5.Text.PadLeft(2, "0"c)) & ", "
        sql = sql & N & " KIBOU6   =  " & _db.rmSQ(dteKeikakuDate.Text.Replace("/", "") & numSyuttaibi6.Text.PadLeft(2, "0"c)) & ", "
        sql = sql & N & " UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "', "
        sql = sql & N & " UPDDATE = TO_DATE('" & Format(Now, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "
        _db.executeDB(sql)

    End Sub

    '-------------------------------------------------------------------------------
    '   実行履歴作成
    '   （処理概要）準備処理用の実行履歴を作成する
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
        sql = sql & N & ",SUUTI1 "     '希望出来日1
        sql = sql & N & ",SUUTI2 "     '希望出来日2
        sql = sql & N & ",SUUTI3 "     '希望出来日3
        sql = sql & N & ",SUUTI4 "     '希望出来日4
        sql = sql & N & ",SUUTI5 "     '希望出来日5
        sql = sql & N & ",SUUTI6 "     '希望出来日6
        sql = sql & N & ",UPDNAME "    '最終更新者
        sql = sql & N & ",UPDDATE "    '最終更新日
        sql = sql & N & ")values( "
        sql = sql & N & "  '" & _db.rmSQ(dteSyoriDate.Text.Replace("/", "")) & "' "                                        '処理年月
        sql = sql & N & ", '" & _db.rmSQ(dteKeikakuDate.Text.Replace("/", "")) & "' "                                      '計画年月
        sql = sql & N & ", '" & _db.rmSQ(PGID & _bootMode) & "' "                                                          '機能ID
        sql = sql & N & ",to_date('" & Format(prmStDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "              '処理開始日時
        sql = sql & N & ",to_date('" & Format(prmEdDt, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "              '処理終了日時
        sql = sql & N & ", " & _db.rmSQ(dteKeikakuDate.Text.Replace("/", "") & numSyuttaibi1.Text.PadLeft(2, "0"c)) & " "  '希望出来日1
        sql = sql & N & ", " & _db.rmSQ(dteKeikakuDate.Text.Replace("/", "") & numSyuttaibi2.Text.PadLeft(2, "0"c)) & " "  '希望出来日2
        sql = sql & N & ", " & _db.rmSQ(dteKeikakuDate.Text.Replace("/", "") & numSyuttaibi3.Text.PadLeft(2, "0"c)) & " "  '希望出来日3
        sql = sql & N & ", " & _db.rmSQ(dteKeikakuDate.Text.Replace("/", "") & numSyuttaibi4.Text.PadLeft(2, "0"c)) & " "  '希望出来日4
        sql = sql & N & ", " & _db.rmSQ(dteKeikakuDate.Text.Replace("/", "") & numSyuttaibi5.Text.PadLeft(2, "0"c)) & " "  '希望出来日5
        sql = sql & N & ", " & _db.rmSQ(dteKeikakuDate.Text.Replace("/", "") & numSyuttaibi6.Text.PadLeft(2, "0"c)) & " "  '希望出来日6
        sql = sql & N & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "' "                                                '最終更新者
        sql = sql & N & ",sysdate "                                                                                        '最終更新日
        sql = sql & N & ") "
        _db.executeDB(sql)

    End Sub

    '-------------------------------------------------------------------------------
    '   処理制御初期化
    '   （処理概要）マスメン系を除く各処理の処理日時を初期化し、次月の処理を開始する
    '   ●入力パラメタ  ：なし
    '   ●出力パラメタ  ：なし
    '   ●メソッド戻り値：なし
    '-------------------------------------------------------------------------------
    Private Sub updateSeigyoInit()

        Dim sql As String = ""
        sql = sql & N & "UPDATE T02SEIGYO SET "
        sql = sql & N & "SDATESTART = NULL, "
        sql = sql & N & "SDATEEND   = NULL, "
        sql = sql & N & "UPDNAME = '" & _db.rmSQ(UtilClass.getComputerName()) & "', "
        sql = sql & N & "UPDDATE = TO_DATE('" & Format(Now, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS') "
        sql = sql & N & "WHERE PGID in ('ZG110B1','ZG110B2','ZG210E1','ZG210E2','ZG220E','ZG230B','ZG310E','ZG320E','ZG330B','ZG340B','ZG350E','ZG360B','ZG410B','ZG510B','ZG530E','ZG540B','ZG610B','ZG620E','ZG630B','ZG640B','ZG720B','ZG730Q')  "
        _db.executeDB(sql)

    End Sub

    '-------------------------------------------------------------------------------
    '   トランザクションバックアップ
    '   （処理概要）各種トランザクションTBLをバックアップ用スキーマの退避TBLへバックアップする
    '   ●入力パラメタ  ：prmPb プログレスバーフォーム
    '   ●出力パラメタ  ：prmPb プログレスバーフォーム
    '   ●メソッド戻り値：なし
    '-------------------------------------------------------------------------------
    Private Sub backUpTrnTbl(ByRef prmPb As UtilProgressBar)

        Dim sql As String = ""
        Dim sysdate As String = "to_date('" & Format(Now, "yyyy/MM/dd HH:mm:ss") & "','YYYY/MM/DD HH24:MI:SS')"

        '品名別販売実績
        sql = ""
        sql = sql & N & "insert into B10HINHANJ  "
        sql = sql & N & "select " & sysdate & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "',T.* "
        sql = sql & N & "from T10HINHANJ T"
        _db.executeDB(sql) : prmPb.value += 1

        '品種別販売計画テーブル
        sql = ""
        sql = sql & N & "insert into B11HINSYUHANK  "
        sql = sql & N & "select " & sysdate & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "',T.* "
        sql = sql & N & "from T11HINSYUHANK T"
        _db.executeDB(sql) : prmPb.value += 1

        '品名別販売計画テーブル
        sql = ""
        sql = sql & N & "insert into B12HINMEIHANK  "
        sql = sql & N & "select " & sysdate & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "',T.* "
        sql = sql & N & "from T12HINMEIHANK T"
        _db.executeDB(sql) : prmPb.value += 1

        '販売計画テーブル
        sql = ""
        sql = sql & N & "insert into B13HANBAI  "
        sql = sql & N & "select " & sysdate & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "',T.* "
        sql = sql & N & "from T13HANBAI T"
        _db.executeDB(sql) : prmPb.value += 1

        '生産見込テーブル
        sql = ""
        sql = sql & N & "insert into B21SEISANM  "
        sql = sql & N & "select " & sysdate & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "',T.* "
        sql = sql & N & "from T21SEISANM T"
        _db.executeDB(sql) : prmPb.value += 1

        '在庫実績テーブル
        sql = ""
        sql = sql & N & "insert into B31ZAIKOJ  "
        sql = sql & N & "select " & sysdate & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "',T.* "
        sql = sql & N & "from T31ZAIKOJ T"
        _db.executeDB(sql) : prmPb.value += 1

        '生産計画テーブル
        sql = ""
        sql = sql & N & "insert into B41SEISANK  "
        sql = sql & N & "select " & sysdate & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "',T.* "
        sql = sql & N & "from T41SEISANK T"
        _db.executeDB(sql) : prmPb.value += 1

        '手配テーブル
        sql = ""
        sql = sql & N & "insert into B51TEHAI "
        sql = sql & N & "select " & sysdate & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "',T.* "
        sql = sql & N & "from T51TEHAI T"
        _db.executeDB(sql) : prmPb.value += 1

        '負荷山積テーブル
        sql = ""
        sql = sql & N & "insert into B61FUKA  "
        sql = sql & N & "select " & sysdate & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "',T.* "
        sql = sql & N & "from T61FUKA T"
        _db.executeDB(sql) : prmPb.value += 1

        '負荷山積明細テーブル
        sql = ""
        sql = sql & N & "insert into B62FUKAMEISAI "
        sql = sql & N & "select " & sysdate & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "',T.* "
        sql = sql & N & "from T62FUKAMEISAI T"
        _db.executeDB(sql) : prmPb.value += 1

        '稼働日数テーブル
        sql = ""
        sql = sql & N & "insert into B63KADOUBI  "
        sql = sql & N & "select " & sysdate & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "',T.* "
        sql = sql & N & "from T63KADOUBI T"
        _db.executeDB(sql) : prmPb.value += 1

        '調整MCHテーブル
        sql = ""
        sql = sql & N & "insert into B64MCH  "
        sql = sql & N & "select " & sysdate & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "',T.* "
        sql = sql & N & "from T64MCH T"
        _db.executeDB(sql) : prmPb.value += 1

        '-->2010.12.07 add by takagi 
        '販売実績照会テーブル
        sql = ""
        sql = sql & N & "insert into B71HANBAIS  "
        sql = sql & N & "select " & sysdate & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "',T.* "
        sql = sql & N & "from T71HANBAIS T"
        _db.executeDB(sql) : prmPb.value += 1
        '<--2010.12.07 add by takagi 

        '-->2010.12.09 add by takagi 
        '在庫実績照会テーブル
        sql = ""
        sql = sql & N & "insert into B72ZAIKOS  "
        sql = sql & N & "select " & sysdate & ",'" & _db.rmSQ(UtilClass.getComputerName()) & "',T.* "
        sql = sql & N & "from T72ZAIKOS T"
        _db.executeDB(sql) : prmPb.value += 1
        '<--2010.12.09 add by takagi 
    End Sub

    '-------------------------------------------------------------------------------
    '   トランザクション削除
    '   （処理概要）各種トランザクションTBLを初期化する
    '   ●入力パラメタ  ：prmPb プログレスバーフォーム
    '   ●出力パラメタ  ：prmPb プログレスバーフォーム
    '   ●メソッド戻り値：なし
    '-------------------------------------------------------------------------------
    Private Sub deleteTrnTbl(ByRef prmPb As UtilProgressBar)

        '品名別販売実績(T10HINHANJ)は削除しない

        _db.executeDB("delete from  T11HINSYUHANK ") : prmPb.value += 1     '品種別販売計画テーブル
        _db.executeDB("delete from  T12HINMEIHANK") : prmPb.value += 1      '品名別販売計画テーブル
        _db.executeDB("delete from  T13HANBAI ") : prmPb.value += 1         '販売計画テーブル
        _db.executeDB("delete from  T21SEISANM ") : prmPb.value += 1        '生産見込テーブル
        _db.executeDB("delete from  T31ZAIKOJ ") : prmPb.value += 1         '在庫実績テーブル
        _db.executeDB("delete from  T41SEISANK ") : prmPb.value += 1        '生産計画テーブル
        _db.executeDB("delete from  T51TEHAI ") : prmPb.value += 1          '手配テーブル
        _db.executeDB("delete from  T61FUKA ") : prmPb.value += 1           '負荷山積テーブル
        _db.executeDB("delete from  T62FUKAMEISAI ") : prmPb.value += 1     '負荷山積明細テーブル
        _db.executeDB("delete from  T63KADOUBI ") : prmPb.value += 1        '稼働日数テーブル
        _db.executeDB("delete from  T64MCH ") : prmPb.value += 1            '調整MCHテーブル

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　処理年月フォーカス喪失イベント
    '------------------------------------------------------------------------------------------------------
    Private Sub dteSyoriDate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dteSyoriDate.LostFocus
        Try
            If (Not "".Equals(dteSyoriDate.Text.Replace("/", "").Trim)) AndAlso "".Equals(dteKeikakuDate.Text.Replace("/", "").Trim) Then
                dteKeikakuDate.Text = Format(DateAdd(DateInterval.Month, 1, CDate(dteSyoriDate.Text & "/01")), "yyyy/MM")
            End If
        Catch ex As Exception

        End Try
    End Sub

End Class