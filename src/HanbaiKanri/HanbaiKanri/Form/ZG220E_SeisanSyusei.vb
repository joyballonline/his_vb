'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）生産量データ修正
'    （フォームID）ZG220E_SeisanSyusei
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   橋本        2010/10/26                 新規              
'　(2)   菅野        2011/01/13                 変更　処理制御テーブルの更新タイミングを変更              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.Combo
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory

Imports System.Runtime.InteropServices

Public Class ZG220E_SeisanSyusei
    Inherits System.Windows.Forms.Form
    Implements IfRturnUpDDate

#Region "リテラル値定義"
    '------------------------------------------------------------------------------------------------------
    'メンバー定数宣言
    '------------------------------------------------------------------------------------------------------

    'PG制御文字 
    Private Const N As String = ControlChars.NewLine                    '改行文字
    Private Const RS As String = "RecSet"                               'レコードセットテーブル

    '一覧データバインド列名
    Private Const COLDT_TAISYOU As String = "dtTaisyou"         '対象
    Private Const COLDT_TAISYOUCOPY As String = "dtTaisyouCopy" '対象コピー
    Private Const COLDT_KUBUN As String = "dtKubun"             '区分
    Private Const COLDT_UCHIIRE As String = "dtUchiire"         '内入
    Private Const COLDT_TEHAIKBN As String = "dtTehaiKbn"       '手配
    Private Const COLDT_KIBOU As String = "dtKibou"             '希望
    Private Const COLDT_YOTEI As String = "dtYotei"             '予定
    Private Const COLDT_TEHAINO As String = "dtTehaino"         '手配№
    Private Const COLDT_SEIBAN As String = "dtSeiban"           '製番
    Private Const COLDT_HINMEI As String = "dtHinmei"           '品名
    Private Const COLDT_TEHAISU As String = "dtTehaisu"         '手配数
    Private Const COLDT_TANCYO As String = "dtTancyo"           '単長
    Private Const COLDT_JOSU As String = "dtJosu"               '条数
    Private Const COLDT_SEISAN As String = "dtSeisan"           '生産見込
    Private Const COLDT_TUKI As String = "dtTuki"               '年月
    Private Const COLDT_HINCD As String = "dtHincd"             '品名コード
    Private Const COLDT_OYACD As String = "dtOyacd"             '親品名コード
    Private Const COLDT_JUYOSAKI As String = "dtJuyosaki"       '需要先
    Private Const COLDT_HINSYU As String = "dtHinsyu"           '品種区分
    Private Const COLDT_RECORDID As String = "dtRecordid"       'レコードID    (隠し列)
    Private Const COLDT_JUYOSORT As String = "dtJuyoSort"       '需要先表示順  (隠し列)
    Private Const COLDT_SAKUSEISORT As String = "dtSakuseiSort" '作成区分表示順(隠し列)
    Private Const COLDT_HENKOUFLG As String = "dtHenkouflg"     '変更フラグ    (隠し列)

    '一覧グリッド列名
    Private Const COLCN_TAISYOU As String = "cnTaisyou"         '対象
    Private Const COLCN_TAISYOUCOPY As String = "cnTaisyouCopy" '対象コピー
    Private Const COLCN_KUBUN As String = "cnKubun"             '区分
    Private Const COLCN_UCHIIRE As String = "cnUchiire"         '内入
    Private Const COLCN_TEHAIKBN As String = "cnTehaiKbn"       '手配
    Private Const COLCN_KIBOU As String = "cnKibou"             '希望
    Private Const COLCN_YOTEI As String = "cnYotei"             '予定
    Private Const COLCN_TEHAINO As String = "cnTehaino"         '手配№
    Private Const COLCN_SEIBAN As String = "cnSeiban"           '製番
    Private Const COLCN_HINMEI As String = "cnHinmei"           '品名
    Private Const COLCN_TEHAISU As String = "cnTehaisu"         '手配数
    Private Const COLCN_TANCYO As String = "cnTancyo"           '単長
    Private Const COLCN_JOSU As String = "cnJosu"               '条数
    Private Const COLCN_SEISAN As String = "cnSeisan"           '生産見込
    Private Const COLCN_TUKI As String = "cnTuki"               '年月
    Private Const COLCN_HINCD As String = "cnHincd"             '品名コード
    Private Const COLCN_OYACD As String = "cnOyacd"             '親品名コード
    Private Const COLCN_JUYOSAKI As String = "cnJuyosaki"       '需要先
    Private Const COLCN_HINSYU As String = "cnHinsyu"           '品種区分
    Private Const COLCN_RECORDID As String = "cnRecordid"       'レコードID　　(隠し列)
    Private Const COLCN_JUYOSORT As String = "cnJuyoSort"       '需要先表示順　(隠し列)
    Private Const COLCN_SAKUSEISORT As String = "cnSakuseiSort" '作成区分表示順(隠し列)
    Private Const COLCN_HENKOUFLG As String = "cnHenkouflg"     '変更フラグ　　(隠し列)

    '一覧列番号
    Private Const COLNO_TAISYOU As Integer = 0        '対象
    Private Const COLNO_TAISYOUCOPY As Integer = 1    '対象コピー
    Private Const COLNO_KUBUN As Integer = 2          '区分
    Private Const COLNO_UCHIIRE As Integer = 3        '内入
    Private Const COLNO_TEHAIKBN As Integer = 4       '手配
    Private Const COLNO_KIBOU As Integer = 5          '希望
    Private Const COLNO_YOTEI As Integer = 6          '予定
    Private Const COLNO_TEHAINO As Integer = 7        '手配№
    Private Const COLNO_SEIBAN As Integer = 8         '製番
    Private Const COLNO_HINMEI As Integer = 9         '品名
    Private Const COLNO_TEHAISU As Integer = 10       '手配数
    Private Const COLNO_TANCYO As Integer = 11        '単長
    Private Const COLNO_JOSU As Integer = 12          '条数
    Private Const COLNO_SEISAN As Integer = 13        '生産見込
    Private Const COLNO_TUKI As Integer = 14          '年月
    Private Const COLNO_HINCD As Integer = 15         '品名コード
    Private Const COLNO_OYACD As Integer = 16         '親品名コード
    Private Const COLNO_JUYOSAKI As Integer = 17      '需要先
    Private Const COLNO_HINSYU As Integer = 18        '品種区分
    Private Const COLNO_RECORDID As Integer = 19      'レコードID　　(隠し列)
    Private Const COLNO_JUYOSORT As Integer = 20      '需要先表示順　(隠し列)
    Private Const COLNO_SAKUSEISORT As Integer = 21   '作成区分表示順(隠し列)
    Private Const COLNO_HENKOUFLG As Integer = 22     '変更フラグ　　(隠し列)

    'M01汎用マスタ固定ｷｰ
    Private Const COTEI_JUYOU As String = "01"                      '需要先名
    Private Const COTEI_TEHAI As String = "02"                      '手配区分
    Private Const COTEI_SAKUSEI As String = "15"                    '作成区分
    Private Const COTEI_NYUKO As String = "18"                      '内入フラグ

    'EXCEL
    Private Const START_PRINT As Integer = 7        'EXCEL出力開始行数

    'EXCEL列番号
    Private Const XLSCOL_TAISYOU As Integer = 1        '対象
    Private Const XLSCOL_KUBUN As Integer = 2          '区分
    Private Const XLSCOL_UCHIIRE As Integer = 3        '内入
    Private Const XLSCOL_TEHAIKBN As Integer = 4       '手配
    Private Const XLSCOL_KIBOU As Integer = 5          '希望
    Private Const XLSCOL_YOTEI As Integer = 6          '予定
    Private Const XLSCOL_TEHAINO As Integer = 7        '手配№
    Private Const XLSCOL_SEIBAN As Integer = 8         '製番
    Private Const XLSCOL_HINMEI As Integer = 9         '品名
    Private Const XLSCOL_TEHAISU As Integer = 10       '手配数
    Private Const XLSCOL_TANCYO As Integer = 11        '単長
    Private Const XLSCOL_JOSU As Integer = 12          '条数
    Private Const XLSCOL_SEISAN As Integer = 13        '生産見込
    Private Const XLSCOL_TUKI As Integer = 14          '年月
    Private Const XLSCOL_HINCD As Integer = 15         '品名コード
    Private Const XLSCOL_OYACD As Integer = 16         '親品名コード
    Private Const XLSCOL_JUYOSAKI As Integer = 17      '需要先
    Private Const XLSCOL_HINSYU As Integer = 18        '品種区分

    'EXCEL固定文字
    Private Const HEADER_TAISYOU As String = "対象のみ表示"
    Private Const LIST_TAISYOU As String = "●"

    '変更フラグ
    Private Const HENKO_FLG As String = "1"

    'プログラムID（T91実行履歴テーブル登録用）
    Private Const PGID As String = "ZG220E"

    '実行履歴テーブル登録用
    Private Const TOUROKU = "2"

    '生産見込テーブル更新用
    Private Const TAISYO_ARI = "1"
    Private Const TAISYO_GAI = ""

    '対象フラグ
    Private Const TAISYO As String = "1"
    '-->2010/12/12 chg by takagi #デザイナで論理値は設定できない
    'Private Const CHECK As String = "True"
    'Private Const NON_CHECK As String = "False"
    Private Const CHECK As String = "1"
    Private Const NON_CHECK As String = "0"
    '<--2010/12/12 chg by takagi #デザイナで論理値は設定できない

#End Region

#Region "メンバ変数定義"
    '------------------------------------------------------------------------------------------------------
    'メンバー変数宣言
    '------------------------------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler                       'MSGハンドラ
    Private _db As UtilDBIf                                'DBハンドラ
    Private _parentForm As ZC110M_Menu

    Private _oldRowIndex As Integer = -1                   '選択行の背景色を変更するためのフラグ
    Private _colorCtlFlg As Boolean = False                '選択行の背景色を変更するためのフラグ

    Private _errSet As UtilDataGridViewHandler.dgvErrSet   'エラー発生時にフォーカスするセル位置
    Private _nyuuryokuErrFlg As Boolean = False            '入力エラー有無フラグ

    Private _changeFlg As Boolean = False                  '一覧データ変更フラグ
    Private _beforeChange As String = ""                   '一覧変更前のデータ

    Private _chkCellVO As UtilDgvChkCellVO                 '一覧の入力制限用

    Private _updFlg As Boolean = False

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

    '------------------------------------------------------------------------------------------------------
    'コンストラクタ
    '------------------------------------------------------------------------------------------------------
    Private Sub New()
        InitializeComponent()
    End Sub

    '------------------------------------------------------------------------------------------------------
    '   コンストラクタ
    '   （処理概要）　メニュー画面から呼ばれる。
    '   ●入力パラメタ   ：prmRefMsgHd      MSGハンドラ
    '                      prmRefDbHd       DBハンドラ
    '                      prmBumonCd       部門コード
    '                   　 prmTantoSign     担当サイン
    '                      prmDataShubetsu  データ種別(受注データ・テンプレート)
    '                      prmDataKbn       データ区分(作成中・送信済)
    '                      prmShohinBunrui  商品検索分類(全て・電線・付属品)
    '                      prmDefaultHyoji  デフォルト表示（True・false）	
    '   ●メソッド戻り値 ：インスタンス
    '------------------------------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmForm As ZC110M_Menu, ByVal prmUpdFlg As Boolean)
        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _parentForm = prmForm                                               '親フォーム
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        _updFlg = prmUpdFlg
    End Sub

#End Region

#Region "Formイベント"

    '------------------------------------------------------------------------------------------------------
    'フォームロードイベント
    '------------------------------------------------------------------------------------------------------
    Private Sub frmSH51_ChumonList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            '描画関係の設定
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           'サイズが変更されたときに、コントロールがコントロール自体を再描画するかどうかを示す値を設定
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '描画はバッファで実行され、完了後に、結果が画面に出力されるよう設定
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  'コントロールは、画面に直接ではなく、まずバッファに描画されます。これにより、ちらつきを抑えることができます。
            Me.SetStyle(ControlStyles.UserPaint, True)              'コントロールは、オペレーティング システムによってではなく、独自に描画されるよう設定
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   'コントロールはウィンドウ メッセージ WM_ERASEBKGND を無視するように設定

            'タイトルオプション表示
            Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr

            '初期化
            Call initForm()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

#End Region

#Region "ボタンイベント"
    '------------------------------------------------------------------------------------------------------
    '戻るボタン押下時イベント
    '------------------------------------------------------------------------------------------------------
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            'チェックボックスの内容変更確認
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)
            For i As Integer = gh.getMaxRow - 1 To 0 Step -1
                If Not gh.getCellData(COLDT_TAISYOU, i).Equals(gh.getCellData(COLDT_TAISYOUCOPY, i)) Then
                    _changeFlg = True
                    Exit For
                End If
            Next

            '警告メッセージ
            If _changeFlg Then
                Dim rtn As DialogResult = _msgHd.dspMSG("confirmDgvEdit")   '編集中の内容が破棄されます。よろしいですか？
                If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If

            '自画面を終了し、メニュー画面に戻る。
            _parentForm.Show()
            _parentForm.Activate()

            Me.Close()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '検索ボタン押下時イベント
    '------------------------------------------------------------------------------------------------------
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            'チェックボックスの内容変更確認
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)
            For i As Integer = gh.getMaxRow - 1 To 0 Step -1
                If Not gh.getCellData(COLDT_TAISYOU, i).Equals(gh.getCellData(COLDT_TAISYOUCOPY, i)) Then
                    _changeFlg = True
                    Exit For
                End If
            Next

            '警告メッセージ
            If _changeFlg Then
                Dim rtn As DialogResult = _msgHd.dspMSG("DellDgvData")   '編集中の内容が破棄されます。よろしいですか？
                If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If

            Call dispDGV(True, False)     '検索処理へ

            '変更フラグを無効にする
            _changeFlg = False

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '登録ボタン押下時イベント
    '------------------------------------------------------------------------------------------------------
    Private Sub btnEntry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEntry.Click
        Try
            '必須入力チェック
            Call checkTouroku()

            '登録確認メッセージ
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmInsert")    '登録します。
            If rtn <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If

            ' 元のWaitカーソルを保持
            Dim preCursor As Cursor = Me.Cursor
            ' カーソルを待機カーソルに変更
            Me.Cursor = Cursors.WaitCursor

            Try
                Dim dStartSysdate As Date = Now()                           '処理開始日時
                Dim sPCName As String = UtilClass.getComputerName           '端末ID
                Dim lCntIns As Long = 0

                'トランザクション開始
                _db.beginTran()

                '生産見込テーブル更新
                Call UpdateT21Seisanm(sPCName, dStartSysdate, lCntIns)

                '処理終了日時の取得
                Dim dFinishSysdate As Date = Now()

                '実行履歴テーブルの更新処理
                Call updT91Rireki(lCntIns, sPCName, dStartSysdate, dFinishSysdate)

                _parentForm.updateSeigyoTbl(PGID, True, dStartSysdate, dFinishSysdate)

                'トランザクション終了
                _db.commitTran()

                'チェックボックスを更新する
                Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)
                For i As Integer = gh.getMaxRow - 1 To 0 Step -1
                    If Not gh.getCellData(COLDT_TAISYOU, i).Equals(gh.getCellData(COLDT_TAISYOUCOPY, i)) Then
                        gh.setCellData(COLDT_TAISYOUCOPY, i, gh.getCellData(COLDT_TAISYOU, i))
                    End If
                Next
                
                '変更フラグを無効にする
                _changeFlg = False

                '完了メッセージ
                _msgHd.dspMSG("completeInsert")


            Finally
                If _db.isTransactionOpen = True Then
                    _db.rollbackTran()                          'ロールバック
                End If
                ' カーソルを元に戻す
                Me.Cursor = preCursor
            End Try
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    'Excelボタン押下時イベント
    '------------------------------------------------------------------------------------------------------
    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Try
            ' 元のWaitカーソルを保持
            Dim preCursor As Cursor = Me.Cursor
            ' カーソルを待機カーソルに変更
            Me.Cursor = Cursors.WaitCursor
            Try

                'EXCEL出力
                Call printExcel()

            Finally
                ' カーソルを元に戻す
                Me.Cursor = preCursor
            End Try
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '追加手配登録ボタン押下時イベント
    '------------------------------------------------------------------------------------------------------
    Private Sub btnTsuika_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTsuika.Click
        Try

            Dim syori As String = Trim(Replace(lblSyori.Text, "/", ""))       '処理年月
            Dim keikaku As String = Trim(Replace(lblKeikaku.Text, "/", ""))   '計画年月

            Dim openForm As ZG221S_TuikaNyuuryoku = New ZG221S_TuikaNyuuryoku(_msgHd, _db, Me, syori, keikaku)      '画面遷移
            openForm.ShowDialog(Me)                                                                                 '画面表示
            openForm.Dispose()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '全選択ボタン押下時イベント
    '------------------------------------------------------------------------------------------------------
    Private Sub btnZenSentaku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZenSentaku.Click
        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)

            '項目をすべてチェックさせる
            For i As Integer = gh.getMaxRow - 1 To 0 Step -1
                gh.setCellData(COLDT_TAISYOU, i, True)
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '全解除ボタン押下時イベント
    '------------------------------------------------------------------------------------------------------
    Private Sub btnZenKaijo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZenKaijo.Click
        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)

            'チェックされている項目をすべて解除する
            For i As Integer = gh.getMaxRow - 1 To 0 Step -1
                gh.setCellData(COLDT_TAISYOU, i, False)
            Next

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

#End Region

#Region "ユーザ定義関数:画面制御"

    '-------------------------------------------------------------------------------
    '　画面起動時
    '-------------------------------------------------------------------------------
    Private Sub initForm()
        Try
            '処理年月、計画年月表示
            Call dispDate()

            'コンボボックス
            Call setCbo(cboKubun, COTEI_SAKUSEI) '作成区分
            'チェックボックス
            chkTaisyo.Checked = True

            Call dispDGV(False, False)               '検索処理

            '一覧行着色フラグを有効にする
            _colorCtlFlg = True

            btnEntry.Enabled = _updFlg

            '' 2011/01/13 del start sugano #処理制御テーブルは登録ボタン押下時のみ更新する
            ' '' 2010/12/22 add start sugano
            ''登録ボタン使用可の場合（確定済）の場合、処理制御テーブルは更新しない
            'If btnEntry.Enabled Then
            '    '' 2010/12/22 add  end  sugano
            '    '登録ボタンを押さないケースを考慮し、この時点で処理制御テーブルを更新する
            '    _parentForm.updateSeigyoTbl(PGID, True, Now, Now)
            '    '' 2010/12/22 add start sugano
            'End If
            ' '' 2010/12/22 add  end  sugano
            '' 2011/01/13 del end sugano

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    ' 　更新データの受け取り
    '   (処理概要)子画面で更新されたデータを受け取る
    '　　I　：　prmUpDDate     　　 更新日時
    '-------------------------------------------------------------------------------
    Sub setUpDDate(ByVal prmUpDDate As Date) Implements IfRturnUpDDate.setUpDDate
        Try
            '一覧表示
            Call dispDGV(False, True, prmUpDDate)

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
    Public Sub myShow() Implements IfRturnUpDDate.myShow
        Me.Show()
    End Sub

    '-------------------------------------------------------------------------------
    '   myActivateメソッド
    '-------------------------------------------------------------------------------
    Public Sub myActivate() Implements IfRturnUpDDate.myActivate
        Me.Activate()
    End Sub

    '-------------------------------------------------------------------------------
    '　コンボボックスのセット
    '　(処理年月)M01汎用マスタから対象レコードを抽出して表示する。
    '-------------------------------------------------------------------------------
    Private Sub setCbo(ByVal prmsender As Object, ByVal prmWhere As String)
        Try
            'コンボボックス
            Dim sql = ""
            sql = sql & N & " SELECT KAHENKEY "
            sql = sql & N & " ,NAME1 "
            sql = sql & N & " FROM M01HANYO "
            sql = sql & N & " WHERE KOTEIKEY = '" & prmWhere & "' "
            sql = sql & N & " ORDER BY KAHENKEY "

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                Throw New UsrDefException("登録されていません。", _msgHd.getMSG("noData"))
            End If

            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(prmsender)

            '検索条件解除用リストを追加
            ch.addItem(New UtilCboVO("", ""))

            '検索結果をコンボボックスに設定
            For i As Integer = 0 To iRecCnt - 1
                ch.addItem(New UtilCboVO(ds.Tables(RS).Rows(i)(0).ToString, ds.Tables(RS).Rows(i)(1).ToString))
            Next

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　コントロールキー押下イベント
    '　(処理概要)エンターボタン押下時に次のコントロールに移る
    '-------------------------------------------------------------------------------
    Private Sub ctl_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboKubun.KeyPress, _
                                                                                                                chkTaisyo.KeyPress, _
                                                                                                                btnSearch.KeyPress, _
                                                                                                                btnZenKaijo.KeyPress, _
                                                                                                                btnZenSentaku.KeyPress, _
                                                                                                                btnTsuika.KeyPress, _
                                                                                                                btnExcel.KeyPress, _
                                                                                                                btnEntry.KeyPress, _
                                                                                                                btnBack.KeyPress
        '押下キーがEnterの場合、次のコントロールへフォーカス移動
        Call UtilClass.moveNextFocus(Me, e)
    End Sub

#End Region

#Region "ユーザ定義関数:EXCEL関連"
    '------------------------------------------------------------------------------------------------------
    '　EXCEL出力処理
    '------------------------------------------------------------------------------------------------------
    Private Sub printExcel()
        Try
            'プログレスバー表示
            Dim pb As UtilProgressBar = New UtilProgressBar(Me)
            pb.Show()
            Try

                'プログレスバー設定
                pb.jobName = "出力を準備しています。"
                pb.status = "初期化中．．．"

                '雛形ファイル
                Dim openFilePath As String = StartUp.iniValue.BaseXlsPath & "\" & StartUp.iniValue.ExcelZG220R1_Base
                '雛形ファイルが開かれていないかチェック
                Dim fh As UtilFile = New UtilFile()
                Try
                    fh.move(openFilePath, openFilePath & 1)
                    fh.move(openFilePath & 1, openFilePath)
                Catch ioe As System.IO.IOException
                    Throw New UsrDefException("ファイルが開かれています。ファイルを閉じてから再実行してください。", _
                                              _msgHd.getMSG("fileOpenErr", "【ファイル名】：" & openFilePath))
                End Try

                '出力用ファイル
                'ファイル名取得-----------------------------------------------------
                Dim wkEditFile As String = StartUp.iniValue.OutXlsPath & "\" & StartUp.iniValue.ExcelZG220R1_Out     'コピー先ファイル

                'コピー先ファイルが存在する場合、コピー先ファイルを削除----------------
                If UtilClass.isFileExists(wkEditFile) Then
                    Try
                        fh.delete(wkEditFile)
                    Catch ioe As System.IO.IOException
                        Throw New UsrDefException("ファイルが開かれています。ファイルを閉じてから再実行してください。", _
                                                  _msgHd.getMSG("fileOpenErr", "【ファイル名】：" & wkEditFile))
                    End Try
                End If

                Try
                    '出力用ファイルへ雛型ファイルコピー
                    FileCopy(openFilePath, wkEditFile)
                Catch ioe As System.IO.IOException
                    Throw New UsrDefException(ioe, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ioe)))
                End Try

                Dim eh As xls.UtilExcelHandler = New xls.UtilExcelHandler(wkEditFile)
                Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder
                Try
                    'コピー先ファイル開く
                    eh.open()

                    'プログレスバー設定
                    pb.jobName = "出力中．．．"
                    pb.status = ""

                    Try
                        Dim startPrintRow As Integer = START_PRINT          '出力開始行数
                        Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)        'DGVハンドラの設定
                        Dim rowCnt As Integer = gh.getMaxRow

                        'プログレスバー　最大値設定
                        pb.maxVal = gh.getMaxRow

                        '明細行を行数分コピーする
                        eh.copyRow(startPrintRow)
                        eh.insertPasteRow(startPrintRow + 1, startPrintRow + gh.getMaxRow)

                        '-->2010.12.12 chg by takagi #チェックOFF時に必ずエラーになる(UtilDataGridViewHandlerを使用していないため/併せて効率かも実施)
                        'Dim i As Integer
                        'For i = 0 To rowCnt - 1

                        '    'プログレスバーカウントアップ
                        '    pb.status = (i) & "/" & gh.getMaxRow & "件"
                        '    pb.oneStep = 10
                        '    pb.value = i

                        '    '一覧データ出力
                        '    If dgvList(COLCN_TAISYOU, i).Value Then
                        '        sb.Append(LIST_TAISYOU & ControlChars.Tab)
                        '    Else
                        '        sb.Append("" & ControlChars.Tab)
                        '    End If
                        '    sb.Append(dgvList(COLCN_KUBUN, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_UCHIIRE, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_TEHAIKBN, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_KIBOU, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_YOTEI, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_TEHAINO, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_SEIBAN, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_HINMEI, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_TEHAISU, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_TANCYO, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_JOSU, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_SEISAN, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_TUKI, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_HINCD, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_OYACD, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_JUYOSAKI, i).Value & ControlChars.Tab)
                        '    sb.Append(dgvList(COLCN_HINSYU, i).Value & ControlChars.Tab)
                        '    sb.Append(ControlChars.CrLf)

                        'Next
                        Dim i As Integer = 0
                        With sb
                            For i = 0 To rowCnt - 1

                                'プログレスバーカウントアップ
                                pb.status = (i) & "/" & gh.getMaxRow & "件"
                                pb.oneStep = 10
                                pb.value = i

                                '一覧データ出力
                                If CHECK.Equals(gh.getCellData(COLDT_TAISYOU, i)) Then
                                    sb.Append(LIST_TAISYOU & ControlChars.Tab)
                                Else
                                    sb.Append("" & ControlChars.Tab)
                                End If
                                .Append(gh.getCellData(COLDT_KUBUN, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_UCHIIRE, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_TEHAIKBN, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_KIBOU, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_YOTEI, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_TEHAINO, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_SEIBAN, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_HINMEI, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_TEHAISU, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_TANCYO, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_JOSU, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_SEISAN, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_TUKI, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_HINCD, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_OYACD, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_JUYOSAKI, i) & ControlChars.Tab)
                                .Append(gh.getCellData(COLDT_HINSYU, i) & ControlChars.Tab)
                                .Append(ControlChars.CrLf)

                            Next
                        End With
                        '<--2010.12.12 chg by takagi #チェックOFF時に必ずエラーになる(UtilDataGridViewHandlerを使用していないため/併せて効率かも実施)

                        Clipboard.SetText(sb.ToString)
                        eh.paste(startPrintRow, XLSCOL_TAISYOU) '一括貼り付け

                        '余分な空行を削除
                        eh.deleteRow(startPrintRow + i) '雛形のコピー元となる行
                        ''2011/01/20 add start sugano
                        eh.deleteRow(startPrintRow + i) '雛型の印刷範囲に含まれる空行
                        ''2011/01/20 add end sugano

                        '作成日時編集
                        Dim printDate As String = Now.ToString("yyyy/MM/dd HH:mm")
                        eh.setValue("作成日時 ： " & printDate, 1, 18)   'R1

                        '処理年月、計画年月編集
                        eh.setValue("処理年月：" & lblSyori.Text & "　　計画年月：" & lblKeikaku.Text, 1, 8)    'H1

                        '件数編集
                        eh.setValue(rowCnt & "件", 3, 18)    'R3

                        'ヘッダーの検索条件編集
                        eh.setValue("区分：" & cboKubun.Text, 3, 1)  'A3
                        If chkTaisyo.Checked Then
                            eh.setValue(HEADER_TAISYOU, 3, 5)  'E3
                        Else
                            eh.setValue("", 3, 5)
                        End If

                        '左上のセルにフォーカス当てる
                        eh.selectCell(7, 1)     'A7

                        Clipboard.Clear()         'クリップボードの初期化

                    Finally
                        'EXCELを閉じる
                        eh.close()
                    End Try

                    'EXCELファイル開く
                    eh.display()

                Catch ue As UsrDefException
                    ue.dspMsg()
                    Throw ue
                Catch ex As Exception
                    'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
                    Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)), , StartUp.iniValue.LogFilePath)
                Finally
                    eh.endUse()
                    eh = Nothing
                End Try

                '-->2010.12.12 プログレスバーが確実に閉じられないので、Try～Finallyを新設し、再設定
            Finally
                'プログレスバー画面消去
                pb.Close()
            End Try
            '<--2010.12.12 プログレスバーが確実に閉じられないので、Try～Finallyを新設し、再設定

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub
#End Region

#Region "ユーザ定義関数:DGV関連"
    '------------------------------------------------------------------------------------------------------
    '　一覧データ編集前
    '　(処理概要)一覧のデータが変更される前の値を保持する
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvList_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles dgvList.CellBeginEdit

        '編集前の値を保存
        _beforeChange = _db.rmNullStr(dgvList(e.ColumnIndex, e.RowIndex).Value.ToString)

        '背景色の設定
        'グリッドの先頭列にフォーカスが移動した場合のみSelectionChangedイベントでフォーカスがあるセルの行を取得
        '出来ない為、ここで再度背景色を変更
        Call setBackcolor(e.RowIndex, _oldRowIndex)

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　一覧データ編集後
    '　(処理概要)一覧のデータが変更された場合、変更フラグを立て、合計の値を再表示する
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvList_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvList.CellEndEdit
        Try
            Dim _dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)        'DGVハンドラの設定
            Dim RowNo As Integer = dgvList.CurrentCell.RowIndex

            '編集前と値が変わっていた場合、フラグを立てる
            If Not _beforeChange.Equals(_db.rmNullStr(dgvList(e.ColumnIndex, e.RowIndex).Value.ToString)) Then
                '対象行に変更フラグを立てる
                dgvList(COLNO_HENKOUFLG, RowNo).Value = HENKO_FLG
                _changeFlg = True
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   一覧　編集チェック（EditingControlShowingイベント）
    '   （処理概要）入力の制限をかける
    '-------------------------------------------------------------------------------
    Private Sub dgvList_EditingControlShowing(ByVal sender As Object, _
                                              ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs _
                                              ) Handles dgvList.EditingControlShowing

        Try
            Dim _dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)        'DGVハンドラの設定
            '■生産見込の場合
            If dgvList.CurrentCell.ColumnIndex = COLNO_SEISAN Then
                '■グリッドに、数値入力モードの制限をかける
                _chkCellVO = _dgv.chkCell(sender, e, UtilDataGridViewHandler.chkType.Num)
            Else
                If Not _chkCellVO Is Nothing Then
                    _dgv.AfterchkCell(_chkCellVO)
                End If
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '   選択セル検証イベント（DataErrorイベント）
    '   （処理概要）数値入力欄に数値以外が入力された場合のエラー処理
    '-------------------------------------------------------------------------------
    Private Sub dgvList_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvList.DataError

        Try
            e.Cancel = False                                   '編集モード終了

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)
            '■生産見込の場合、グリッドには数値入力モード(0～9)の制限をかけているので、制限の解除
            If dgvList.CurrentCell.ColumnIndex = COLNO_SEISAN Then
                gh.AfterchkCell(_chkCellVO)
            End If

            '入力エラーフラグを立てる
            _nyuuryokuErrFlg = True

            Dim colName As String = ""
            colName = COLCN_SEISAN


            'エラーセルにフォーカスをあてる
            _errSet = gh.readyForErrSet(e.RowIndex, colName)

            'エラーメッセージ表示
            Throw New UsrDefException("半角数字のみ入力可能です。", _msgHd.getMSG("onlyAcceptNumeric"))

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　　グリッドフォーカス設定及び選択行に着色する処理
    '　　(処理概要）セル編集後にエラーになった場合に、エラーセルにフォーカスを戻す。
    '               選択行に着色する。
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvList_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvList.SelectionChanged
        Try

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)

            '入力エラーがあった場合
            If _nyuuryokuErrFlg Then
                _nyuuryokuErrFlg = False
                gh.setCurrentCell(_errSet)
            End If

            If _colorCtlFlg Then
                '背景色の設定
                Call setBackcolor(dgvList.CurrentCellAddress.Y, _oldRowIndex)

            End If
            _oldRowIndex = dgvList.CurrentCellAddress.Y

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　背景色の設定処理
    '　(処理概要)行の背景色を青に着色する。
    '　　I　：　prmRowIndex     現在フォーカスがある行数
    '　　I　：　prmOldRowIndex  現在の行に移る前の行数
    '------------------------------------------------------------------------------------------------------
    Private Sub setBackcolor(ByVal prmRowIndex As Integer, ByVal prmOldRowIndex As Integer)

        Dim _dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)

        '指定した行の背景色を青にする
        _dgv.setSelectionRowColor(prmRowIndex, prmOldRowIndex, StartUp.lCOLOR_BLUE)

        _oldRowIndex = prmRowIndex

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　指定列へのフォーカス設定処理
    '　(処理概要)指定されたセルにフォーカスする。
    '　　I　：　prmCoIndex      フォーカスさせるセルの列数
    '　　I　：　prmRowIndex     フォーカスさせるセルの行数
    '------------------------------------------------------------------------------------------------------
    Private Sub setForcusCol(ByVal prmColIndex As Integer, ByVal prmRowIndex As Integer)

        'フォーカスをあてる
        dgvList.Focus()
        dgvList.CurrentCell = dgvList(prmColIndex, prmRowIndex)

        '背景色の設定
        Call setBackcolor(prmRowIndex, _oldRowIndex)

    End Sub
#End Region

#Region "ユーザ定義関数:DB関連"
    '-------------------------------------------------------------------------------
    '　処理年月、計画年月表示
    '　(処理概要)処理年月、計画年月を表示する
    '-------------------------------------------------------------------------------
    Private Sub dispDate()
        Try
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & " SNENGETU " & "SYORI"          '処理年月
            sql = sql & N & " ,KNENGETU " & "KEIKAKU"       '計画年月
            sql = sql & N & " FROM T01KEIKANRI "

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                Throw New UsrDefException("登録されていません。", _msgHd.getMSG("noData"))
            End If

            Dim syoriDate As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("SYORI"))
            Dim keikakuDate As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("KEIKAKU"))

            '「YYYY/MM」形式で表示
            lblSyori.Text = syoriDate.Substring(0, 4) & "/" & syoriDate.Substring(4, 2)
            lblKeikaku.Text = keikakuDate.Substring(0, 4) & "/" & keikakuDate.Substring(4, 2)

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　実行履歴テーブルの追加処理
    '  (処理概要)実行履歴テーブルにレコードを追加する
    '　　I　：　prmCntIns       　 登録件数
    '　　I　：　prmPCName      　　端末ID
    '　　I　：　prmStartDate       処理開始日時
    '　　I　：　prmFinishDate      処理終了日時
    '------------------------------------------------------------------------------------------------------
    Private Sub updT91Rireki(ByVal prmCntIns As Long, ByVal prmPCName As String, ByVal prmStartDate As Date, ByVal prmFinishDate As Date)
        Try
            '登録処理
            Dim sql As String = ""
            sql = "INSERT INTO T91RIREKI ("
            sql = sql & N & "  SNENGETU"                                                    '処理年月
            sql = sql & N & ", KNENGETU"                                                    '計画年月
            sql = sql & N & ", PGID"                                                        '機能ID
            sql = sql & N & ", SDATESTART"                                                  '処理開始日時
            sql = sql & N & ", SDATEEND"                                                    '処理終了日時
            sql = sql & N & ", KENNSU1"                                                     '件数１（更新件数）
            sql = sql & N & ", NAME1"                                                       '名称１
            sql = sql & N & ", UPDNAME"                                                     '端末ID
            sql = sql & N & ", UPDDATE"                                                     '更新日時
            sql = sql & N & ") VALUES ("
            sql = sql & N & "  '" & Trim(Replace(lblSyori.Text, "/", "")) & "'"             '処理年月
            sql = sql & N & ", '" & Trim(Replace(lblKeikaku.Text, "/", "")) & "'"           '計画年月
            sql = sql & N & ", '" & PGID & "'"                                              '機能ID
            sql = sql & N & ", TO_DATE('" & prmStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "   '処理開始日時
            sql = sql & N & ", TO_DATE('" & prmFinishDate & "', 'YYYY/MM/DD HH24:MI:SS') "  '処理終了日時
            sql = sql & N & ", " & prmCntIns                                                '件数１（更新件数）
            sql = sql & N & ", " & TOUROKU                                                  '名称１
            sql = sql & N & ", '" & prmPCName & "'"                                         '端末ID
            sql = sql & N & ", TO_DATE('" & Now() & "', 'YYYY/MM/DD HH24:MI:SS') "          '更新日時
            sql = sql & N & " )"
            _db.executeDB(sql)

        Catch ex As Exception
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　生産見込テーブルの更新処理
    '  (処理概要)生産見込テーブルを変更内容にて更新する
    '　　I　：　prmPCName      　　端末ID
    '　　I　：　prmStartDate       処理開始日時
    '　　R　：　rCntUp       　　　更新件数
    '------------------------------------------------------------------------------------------------------
    Private Sub UpdateT21Seisanm(ByVal prmPCName As String, ByVal prmStartDate As Date, ByRef rCntUp As Long)
        Try

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)

            For i As Integer = 0 To gh.getMaxRow - 1
                '変更フラグが立っているデータを対象とする
                If HENKO_FLG.Equals(gh.getCellData(COLDT_HENKOUFLG, i).ToString) Or Not gh.getCellData(COLDT_TAISYOU, i).Equals(gh.getCellData(COLDT_TAISYOUCOPY, i)) Then
                    '変更データの更新処理
                    Dim sql As String = ""
                    sql = ""
                    sql = sql & N & " UPDATE T21SEISANM SET "
                    If Not "".Equals(gh.getCellData(COLDT_SEISAN, i).ToString) Then
                        sql = sql & N & "   SMIKOMISU = " & gh.getCellData(COLDT_SEISAN, i).ToString
                    Else
                        sql = sql & N & "   SMIKOMISU = '' "
                    End If
                    If Not "".Equals(gh.getCellData(COLDT_TUKI, i).ToString) Then
                        sql = sql & N & ", NENGETSU = '" & Trim(Replace(gh.getCellData(COLDT_TUKI, i).ToString, "/", "")) & "'"
                    Else
                        sql = sql & N & ", NENGETSU = ''"
                    End If
                    If CHECK.Equals(gh.getCellData(COLDT_TAISYOU, i)) Then
                        sql = sql & N & ", TAISYO_FLG = '" & TAISYO_ARI & "'"
                    Else
                        sql = sql & N & ", TAISYO_FLG = '" & TAISYO_GAI & "'"
                    End If
                    sql = sql & N & ", UPDNAME = '" & prmPCName & "'"
                    sql = sql & N & ", UPDDATE = " & " TO_DATE('" & prmStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "
                    sql = sql & N & " WHERE RECORDID = '" & gh.getCellData(COLDT_RECORDID, i).ToString & "'"
                    _db.executeDB(sql)

                    '更新件数のカウントアップ
                    rCntUp = rCntUp + 1
                End If
            Next

        Catch ex As Exception
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '検索処理
    '   （処理概要）　検索処理を行ない、一覧にデータを表示する。
    '   ●入力パラメタ   ：prmActSearchBtnFlg           検索ボタン押下時 呼出判定フラグ
    '                                                   True = 検索ボタン押下時、False = 検索ボタン押下時以外
    '                    ：prmActCheck_InsertDataFlg    データ追加実行判定フラグ
    '                                                   True = 実行する、False = 実行しない
    '                    ：prmUpDDate　　　　　　　　　 更新日時
    '   ●メソッド戻り値 ：なし
    '------------------------------------------------------------------------------------------------------
    Private Sub dispDGV(Optional ByVal prmActSearchBtnFlg As Boolean = False, Optional ByVal prmActCheck_InsertDataFlg As Boolean = False, Optional ByVal prmUpDDate As Date = Nothing)
        Dim c As Cursor = Me.Cursor
        Me.Cursor = Cursors.WaitCursor
        Try
            '一覧・件数クリア
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)
            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(cboKubun)

            'データ追加処理以外の場合、一覧の初期化
            If Not prmActCheck_InsertDataFlg Then
                gh.clearRow()
                dgvList.Enabled = False
                txtCnt.Text = "0件"
            End If

            Dim sql As String = ""
            Dim sqlAdd As String = ""
            sql = "SELECT "
            sql = sql & N & "  CASE T21.TAISYO_FLG WHEN '" & TAISYO & "' THEN '" & CHECK & "' ELSE '" & NON_CHECK & "' END " & COLDT_TAISYOU
            sql = sql & N & " ,CASE T21.TAISYO_FLG WHEN '" & TAISYO & "' THEN '" & CHECK & "' ELSE '" & NON_CHECK & "' END " & COLDT_TAISYOUCOPY
            sql = sql & N & " ,M11.NAME1 " & COLDT_KUBUN
            sql = sql & N & " ,M12.NAME1 " & COLDT_UCHIIRE
            sql = sql & N & " ,M13.NAME1 " & COLDT_TEHAIKBN
            sql = sql & N & " ,TO_CHAR(TO_DATE(T21.KYUTTAIBI,'YYYYMMDD'),'yy/mm/dd') " & COLDT_KIBOU
            sql = sql & N & " ,TO_CHAR(TO_DATE(T21.YSYUTTAIBI,'YYYYMMDD'),'yy/mm/dd') " & COLDT_YOTEI
            sql = sql & N & " ,T21.TEHAINO " & COLDT_TEHAINO
            sql = sql & N & " ,T21.SEIBAN " & COLDT_SEIBAN
            sql = sql & N & " ,T21.HINMEI " & COLDT_HINMEI
            sql = sql & N & " ,T21.TEHAISU " & COLDT_TEHAISU
            sql = sql & N & " ,T21.TANCYO " & COLDT_TANCYO
            sql = sql & N & " ,T21.JYOSU " & COLDT_JOSU
            sql = sql & N & " ,T21.SMIKOMISU " & COLDT_SEISAN
            sql = sql & N & " ,CASE WHEN T21.NENGETSU IS NULL THEN '' ELSE TO_CHAR(TO_DATE(T21.NENGETSU ,'YYYYMM'),'yyyy/mm') END " & COLDT_TUKI
            sql = sql & N & " ,T21.HINMEICD " & COLDT_HINCD
            sql = sql & N & " ,CASE T21.HINMEICD WHEN T21.KHINMEICD THEN '' ELSE T21.KHINMEICD END " & COLDT_OYACD
            sql = sql & N & " ,M14.NAME2 " & COLDT_JUYOSAKI
            sql = sql & N & " ,T21.HINSYU_KBN " & COLDT_HINSYU
            sql = sql & N & " ,T21.RECORDID " & COLDT_RECORDID
            sql = sql & N & " ,M14.SORT " & COLDT_JUYOSORT
            sql = sql & N & " ,M11.SORT " & COLDT_SAKUSEISORT
            sql = sql & N & " ,NULL " & COLDT_HENKOUFLG
            sql = sql & N & " FROM T21SEISANM T21 "
            sql = sql & N & "   LEFT OUTER JOIN M01HANYO M11 ON "
            sql = sql & N & "   T21.SAKUSEI_KBN = M11.KAHENKEY "
            sql = sql & N & "   AND M11.KOTEIKEY = '" & COTEI_SAKUSEI & "'"
            sql = sql & N & "   LEFT OUTER JOIN M01HANYO M12 ON "
            sql = sql & N & "   T21.NYUKO_FLG = M12.KAHENKEY "
            sql = sql & N & "   AND M12.KOTEIKEY = '" & COTEI_NYUKO & "'"
            sql = sql & N & "   LEFT OUTER JOIN M01HANYO M13 ON "
            sql = sql & N & "   T21.TEHAI_KBN = M13.KAHENKEY "
            sql = sql & N & "   AND M13.KOTEIKEY = '" & COTEI_TEHAI & "'"
            sql = sql & N & "   LEFT OUTER JOIN M01HANYO M14 ON "
            sql = sql & N & "   T21.JUYOU_CD = M14.KAHENKEY "
            sql = sql & N & "   AND M14.KOTEIKEY = '" & COTEI_JUYOU & "'"
            If prmActCheck_InsertDataFlg Then
                sql = sql & N & " WHERE T21.UPDNAME = '" & UtilClass.getComputerName & "'"
                sql = sql & N & " AND T21.UPDDATE = TO_DATE('" & prmUpDDate & "', 'YYYY/MM/DD HH24:MI:SS') "
            ElseIf prmActSearchBtnFlg Then
                If Not "".Equals(ch.getCode()) Then
                    If Not "".Equals(sqlAdd) Then
                        sqlAdd = sqlAdd & N & " AND "
                    End If
                    sqlAdd = sqlAdd & "T21.SAKUSEI_KBN = '" & ch.getCode & "'"
                End If
                If chkTaisyo.Checked Then
                    If Not "".Equals(sqlAdd) Then
                        sqlAdd = sqlAdd & N & " AND "
                    End If
                    sqlAdd = sqlAdd & "T21.TAISYO_FLG = '" & TAISYO & "'"
                End If
                If Not "".Equals(sqlAdd) Then
                    sql = sql & N & " WHERE " & sqlAdd
                End If
            Else
                sql = sql & N & " WHERE T21.TAISYO_FLG = '" & TAISYO & "'"
            End If
            '-->2010.12.25 chg by takagi #46
            'sql = sql & N & " ORDER BY M14.SORT ,HINSYU_KBN ,HIN_CD ,SENSIN_CD ,SIZE_CD ,SIYOU_CD ,COLOR_CD ,KYUTTAIBI ,TEHAINO ,M11.SORT"
            sql = sql & N & " ORDER BY HIN_CD ,SENSIN_CD ,SIZE_CD ,SIYOU_CD ,COLOR_CD ,KYUTTAIBI ,TEHAINO ,M11.SORT"
            '<--2010.12.25 chg by takagi #46

            'SQL発行
            Dim iRecCnt As Integer                  'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If prmActCheck_InsertDataFlg Then
                Dim dt As DataTable = CType(dgvList.DataSource, DataSet).Tables(RS)
                Dim newRow As DataRow = dt.NewRow

                '既存DataTableの最終行にVOを挿入
                dt.Rows.InsertAt(newRow, dt.Rows.Count)

                Dim lRowCnt As Long = dt.Rows.Count
                gh.setCellData(COLDT_TAISYOU, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TAISYOU)))        '対象
                gh.setCellData(COLDT_KUBUN, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_KUBUN)))             '区分
                gh.setCellData(COLDT_UCHIIRE, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_UCHIIRE)))         '内入
                gh.setCellData(COLDT_TEHAIKBN, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TEHAIKBN)))       '手配
                gh.setCellData(COLDT_KIBOU, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_KIBOU)))             '希望
                gh.setCellData(COLDT_YOTEI, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_YOTEI)))             '予定
                gh.setCellData(COLDT_TEHAINO, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TEHAINO)))         '手配№
                gh.setCellData(COLDT_SEIBAN, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_SEIBAN)))           '製番
                gh.setCellData(COLDT_HINMEI, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_HINMEI)))           '品名
                gh.setCellData(COLDT_TEHAISU, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TEHAISU)))         '手配数
                gh.setCellData(COLDT_TANCYO, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TANCYO)))           '単長
                gh.setCellData(COLDT_JOSU, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_JOSU)))               '条数
                gh.setCellData(COLDT_SEISAN, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_SEISAN)))           '生産見込
                gh.setCellData(COLDT_TUKI, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_TUKI)))               '年月
                gh.setCellData(COLDT_HINCD, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_HINCD)))             '品名コード
                gh.setCellData(COLDT_OYACD, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_OYACD)))             '親品名コード
                gh.setCellData(COLDT_JUYOSAKI, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_JUYOSAKI)))       '需要先
                gh.setCellData(COLDT_HINSYU, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_HINSYU)))           '品種区分
                gh.setCellData(COLDT_RECORDID, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_RECORDID)))       'レコードID    (隠し列)
                gh.setCellData(COLDT_JUYOSORT, lRowCnt - 1, _db.rmNullInt(ds.Tables(RS).Rows(0)(COLDT_JUYOSORT)))       '需要先表示順  (隠し列)
                gh.setCellData(COLDT_SAKUSEISORT, lRowCnt - 1, _db.rmNullStr(ds.Tables(RS).Rows(0)(COLDT_SAKUSEISORT))) '作成区分表示順(隠し列)

                '追加した行の変更フラグを立てる。
                '※変更フラグが立っているデータが登録対象になるため
                gh.setCellData(COLDT_HENKOUFLG, lRowCnt - 1, HENKO_FLG)

                '件数の表示
                txtCnt.Text = CStr(lRowCnt) & "件"
            Else
                '抽出データを一覧に表示する
                dgvList.DataSource = ds
                dgvList.DataMember = RS

                '一覧件数表示
                txtCnt.Text = dgvList.RowCount.ToString & "件"

                'ボタン制御------------------------------------------------------------
                If dgvList.RowCount <= 0 Then
                    dgvList.Enabled = False         '一覧の使用不可
                    btnEntry.Enabled = False        '登録ボタンの使用不可
                    btnExcel.Enabled = False        'エクセルボタンの使用不可
                    btnZenSentaku.Enabled = False   '全選択ボタンの使用不可
                    btnZenKaijo.Enabled = False     '全解除ボタンの使用不可
                    'メッセージの表示
                    Throw New UsrDefException("対象データがありません。", _msgHd.getMSG("NonData"))
                Else
                    dgvList.Enabled = True          '一覧の使用不可
                    btnEntry.Enabled = _updFlg      '登録ボタンの使用可
                    btnExcel.Enabled = True         'エクセルボタンの使用可
                    btnZenSentaku.Enabled = True    '全選択ボタンの使用可
                    btnZenKaijo.Enabled = True      '全解除ボタンの使用可
                    '一覧先頭行選択--------------------------------------------------------------
                    dgvList.Focus()
                    gh.setSelectionRowColor(0, 0, StartUp.lCOLOR_BLUE)
                End If
            End If


        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        Finally
            Me.Cursor = c
        End Try
    End Sub
#End Region

#Region "ユーザ定義関数:チェック処理"
    '------------------------------------------------------------------------------------------------------
    '  登録チェック
    '　(処理概要)各項目の必須項目チェックを行う
    '------------------------------------------------------------------------------------------------------
    Private Sub checkTouroku()
        Try

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)

            For i As Integer = 0 To gh.getMaxRow - 1
                If CHECK.Equals(gh.getCellData(COLDT_TAISYOU, i)) Then
                    '対象チェックがチェックありの場合、必須入力チェック
                    '生産見込
                    Call checkHissu(COLDT_SEISAN, "生産見込", i, COLNO_SEISAN)
                    '年月
                    Call checkHissu(COLDT_TUKI, "年月", i, COLNO_TUKI)
                End If

                '年月が空白以外の場合
                If Not "".Equals(gh.getCellData(COLDT_TUKI, i)) Then
                    '半角文字チェック
                    If UtilClass.isOnlyNStr(gh.getCellData(COLDT_TUKI, i)) = False Then
                        Throw New UsrDefException("半角英数のみ入力可能です。", _msgHd.getMSG("onlyAcceptHankakuEisu", "【 年月 】"))
                    End If
                    '形式チェック
                    Call checkFormat(COLDT_TUKI, "年月", i, COLNO_TUKI)
                End If
            Next

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub
    '------------------------------------------------------------------------------------------------------
    '  必須入力チェック
    '　(処理概要)セルが入力されているか
    '　　I　：　prmColName              チェックするセルの列名
    '　　I　：　prmColHeaderName        エラー時に表示する列名
    '　　I　：　prmCnt                  チェックするセルの行数
    '　　I　：　prmColNo                チェックするセルの列数
    '------------------------------------------------------------------------------------------------------
    Private Sub checkHissu(ByVal prmColName As String, ByVal prmColHeaderName As String, ByVal prmCnt As Integer, ByVal prmColNo As Integer)
        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)

            '必須入力チェック
            If "".Equals(gh.getCellData(prmColName, prmCnt).ToString) Then
                'フォーカスをあてる
                Call setForcusCol(prmColNo, prmCnt)
                'エラーメッセージの表示
                '-->2010.12.17 chg by takagi #13
                'Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【 '" & prmColHeaderName & "' ：" & prmCnt + 1 & "行目】"))
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput"))
                '<--2010.12.17 chg by takagi #13
            End If

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '  形式チェック
    '　(処理概要)セルに入力された値の形式が正しいか
    '　　I　：　prmColName              チェックするセルの列名
    '　　I　：　prmColHeaderName        エラー時に表示する列名
    '　　I　：　prmCnt                  チェックするセルの行数
    '　　I　：　prmColNo                チェックするセルの列数
    '------------------------------------------------------------------------------------------------------
    Private Sub checkFormat(ByVal prmColName As String, ByVal prmColHeaderName As String, ByVal prmCnt As Integer, ByVal prmColNo As Integer)
        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)

            '形式チェック
            If Not IsDigit(gh.getCellData(prmColName, prmCnt).ToString) Then
                'フォーカスをあてる
                Call setForcusCol(prmColNo, prmCnt)
                'エラーメッセージの表示
                '-->2010.12.17 chg by takagi #13
                'Throw New UsrDefException("年月はYYYY/MM形式で入力してください。", _msgHd.getMSG("ErrTukiFormat", "【 '" & prmColHeaderName & "' ：" & prmCnt + 1 & "行目】"))
                Throw New UsrDefException("年月はYYYY/MM形式で入力してください。", _msgHd.getMSG("ErrTukiFormat"))
                '<--2010.12.17 chg by takagi #13
            End If

            '日付チェック
            If Not IsDate(gh.getCellData(prmColName, prmCnt).ToString & "/01") Then
                'フォーカスをあてる
                Call setForcusCol(prmColNo, prmCnt)
                'エラーメッセージの表示
                '-->2010.12.17 chg by takagi #13
                'Throw New UsrDefException("無効な日付が入力されています。", _msgHd.getMSG("ImputedInvalidDate", "【 '" & prmColHeaderName & "' ：" & prmCnt + 1 & "行目】"))
                Throw New UsrDefException("無効な日付が入力されています。", _msgHd.getMSG("ImputedInvalidDate"))
                '<--2010.12.17 chg by takagi #13
            End If


        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '  形式チェック
    '　(処理概要)セルに入力された値の形式が正しいか
    '　　I　：　prmColName              チェックするセルの内容
    '------------------------------------------------------------------------------------------------------
    Private Function IsDigit(ByVal Value As String) As Boolean
        Dim K As Long

        If Len(Value) = 0 Then
            IsDigit = False
            Exit Function
        End If

        If Not Len(Value) = 7 Then
            IsDigit = False
            Exit Function
        End If

        For K = 1 To Len(Value)
            If K = 5 Then
                If Not Mid(Value, K, 1) Like "/" Then Exit Function
            Else
                If Not Mid(Value, K, 1) Like "[0-9]" Then Exit Function
            End If
        Next K

        IsDigit = True

    End Function
#End Region

End Class
