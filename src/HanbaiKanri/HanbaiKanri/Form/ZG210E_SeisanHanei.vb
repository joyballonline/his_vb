'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）生産量データ取込
'    （フォームID）ZG210E_SeisanHanei
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   橋本        2010/10/26                新規              
'　(2)   菅野        2011/01/13                変更　処理制御テーブルの更新タイミングを変更           
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.Combo
Imports UtilMDL.DataGridView

Imports System.Runtime.InteropServices

Public Class ZG210E_SeisanHanei
    Inherits System.Windows.Forms.Form

#Region "リテラル値定義"
    '------------------------------------------------------------------------------------------------------
    'メンバー定数宣言
    '------------------------------------------------------------------------------------------------------
    'PG制御文字 
    Private Const N As String = ControlChars.NewLine                    '改行文字
    Private Const RS As String = "RecSet"                               'レコードセットテーブル
    
    '一覧データバインド名
    Private Const COLDT_TAISYOGAI As String = "dtTaisyou"           '対象
    Private Const COLDT_TAISYOGAICOPY As String = "dtTaisyouCopy"   '対象コピー
    Private Const COLDT_UCHIIRE As String = "dtUchiire"             '内入
    Private Const COLDT_TEHAIKBN As String = "dtTehaiKbn"           '手配
    Private Const COLDT_SYUTTAIBI As String = "dtKibou"             '希望出来日
    Private Const COLDT_YOTEI As String = "dtYotei"                 '予定出来日
    Private Const COLDT_TEHAINO As String = "dtTehaiNo"             '手配№
    Private Const COLDT_SEIBAN As String = "dtSeiban"               '製番
    Private Const COLDT_HINMEI As String = "dtHinmei"               '品名
    Private Const COLDT_TEHAISU As String = "dtTehaisu"             '手配数
    Private Const COLDT_TANTYOU As String = "dtTancyo"              '単長
    Private Const COLDT_JOUSUU As String = "dtJosu"                 '条数
    Private Const COLDT_SEISAN As String = "dtSeisan"               '生産見込
    Private Const COLDT_TUKI As String = "dtTuki"                   '年月
    Private Const COLDT_HINCD As String = "dtHincd"                 '実名品コード
    Private Const COLDT_OYACD As String = "dtOyacd"                 '計画品名コード
    Private Const COLDT_JUYOSAKI As String = "dtJuyosaki"           '需要先名
    Private Const COLDT_HINSYU As String = "dtHinsyu"               '品種区分
    Private Const COLDT_NEN As String = "dtNen"                     '年　　　　(隠し列)
    Private Const COLDT_RENBAN As String = "dtRenban"               '連番　　　(隠し列)
    Private Const COLDT_HENKOUFLG As String = "dtHenkouFlg"         '変更フラグ(隠し列)

    '一覧グリッド名
    Private Const COLCN_TAISYOGAI As String = "cnTaisyou"           '対象
    Private Const COLCN_TAISYOGAICOPY As String = "cnTaisyouCopy"   '対象コピー
    Private Const COLCN_UCHIIRE As String = "cnUchiire"             '内入
    Private Const COLCN_TEHAIKBN As String = "cnTehaiKbn"           '手配
    Private Const COLCN_SYUTTAIBI As String = "cnKibou"             '希望出来日
    Private Const COLCN_YOTEI As String = "cnYotei"                 '予定出来日
    Private Const COLCN_TEHAINO As String = "cnTehaiNo"             '手配№
    Private Const COLCN_SEIBAN As String = "cnSeiban"               '製番
    Private Const COLCN_HINMEI As String = "cnHinmei"               '品名
    Private Const COLCN_TEHAISU As String = "cnTehaisu"             '手配数
    Private Const COLCN_TANTYOU As String = "cnTancyo"              '単長
    Private Const COLCN_JOUSUU As String = "cnJosu"                 '条数
    Private Const COLCN_SEISAN As String = "cnSeisan"               '生産見込
    Private Const COLCN_TUKI As String = "cnTuki"                   '年月
    Private Const COLCN_HINCD As String = "cnHincd"                 '実名品コード
    Private Const COLCN_OYACD As String = "cnOyacd"                 '計画品名コード
    Private Const COLCN_JUYOSAKI As String = "cnJuyosaki"           '需要先名
    Private Const COLCN_HINSYU As String = "cnHinsyu"               '品種区分
    Private Const COLCN_NEN As String = "cnNen"                     '年　　　　(隠し列)
    Private Const COLCN_RENBAN As String = "cnRenban"               '連番　　　(隠し列)
    Private Const COLCN_HENKOUFLG As String = "cnHenkouFlg"         '変更フラグ(隠し列)

    '一覧グリッド名
    Private Const COLNO_TAISYOGAI As Integer = 0                    '対象
    Private Const COLNO_TAISYOGAICOPY As Integer = 1                '対象コピー
    Private Const COLNO_UCHIIRE As Integer = 2                      '内入
    Private Const COLNO_TEHAIKBN As Integer = 3                     '手配
    Private Const COLNO_SYUTTAIBI As Integer = 4                    '希望出来日
    Private Const COLNO_YOTEI As Integer = 5                        '予定出来日
    Private Const COLNO_TEHAINO As Integer = 6                      '手配№
    Private Const COLNO_SEIBAN As Integer = 7                       '製番
    Private Const COLNO_HINMEI As Integer = 8                       '品名
    Private Const COLNO_TEHAISU As Integer = 9                      '手配数
    Private Const COLNO_TANTYOU As Integer = 10                     '単長
    Private Const COLNO_JOUSUU As Integer = 11                      '条数
    Private Const COLNO_SEISAN As Integer = 12                      '生産見込
    Private Const COLNO_TUKI As Integer = 13                        '年月
    Private Const COLNO_HINCD As Integer = 14                       '実名品コード
    Private Const COLNO_OYACD As Integer = 15                       '計画品名コード
    Private Const COLNO_JUYOSAKI As Integer = 16                    '需要先名
    Private Const COLNO_HINSYU As Integer = 17                      '品種区分
    Private Const COLNO_NEN As Integer = 18                         '年　　　　(隠し列)
    Private Const COLNO_RENBAN As Integer = 19                      '連番　　　(隠し列)
    Private Const COLNO_HENKOUFLG As Integer = 20                   '変更フラグ(隠し列)

    '作成区分
    Public Const TEHAI As String = "0"                                 '手配済みデータ
    Public Const NYUKO As String = "1"                                 '入庫済みデータ

    'ワークテーブル用
    Private Const RENBAN As Integer = 1                                 '連番
    Private Const NYUKOFLG As String = "1"                              '内入フラグ
    Private Const SAKUSEIKBN_TEHAI As String = "1"                      '作成区分(手配済みデータ)
    Private Const SAKUSEIKBN_NYUKO As String = "2"                      '作成区分(入庫済みデータ)
    Private Const TAISYOFLG As String = "1"                             '対象フラグ
    Private Const MINOUZAN As Integer = 0                               '未納残

    'M01汎用マスタ固定ｷｰ
    Private Const COTEI_JUYOU As String = "01"                          '需要先名
    Private Const COTEI_TEHAI As String = "02"                          '手配区分名
    Private Const COTEI_NYUKO As String = "18"                          '内入フラグ名

    '対象フラグ
    Private Const TAISYO As String = "1"
    Private Const TAISYO_ARI As String = "True"
    Private Const TAISYO_GAI As String = "False"

    'プログラムID（T91実行履歴テーブル登録用）
    Private Const PGID1 As String = "ZG210E1"
    Private Const PGID2 As String = "ZG210E2"

    '変更フラグ
    Private Const HENKO_FLG As String = "0"

#End Region

#Region "メンバ変数定義"
    '------------------------------------------------------------------------------------------------------
    'メンバー変数宣言
    '------------------------------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler                                    'MSGハンドラ
    Private _db As UtilDBIf                                             'DBハンドラ
    Private _parentForm As ZC110M_Menu

    Private _oldRowIndex As Integer = -1                                '選択行の背景色を変更するためのフラグ
    Private _colorCtlFlg As Boolean = False                             '選択行の背景色を変更するためのフラグ

    Private _SakuseiCD As String = ""                                   '作成区分
    Private _tanmatuID As String = ""                                   '端末ID
    Private _updateDate As Date = Now                                   '更新日時

    Private _addcount As Long = 0                                       '更新件数

    Private _errSet As UtilDataGridViewHandler.dgvErrSet                'エラー発生時にフォーカスするセル位置
    Private _nyuuryokuErrFlg As Boolean = False                         '入力エラー有無フラグ

    Private _changeFlg As Boolean = False                               '一覧データ変更フラグ
    Private _beforeChange As String = ""                                '一覧変更前のデータ
   
    Private _chkCellVO As UtilDgvChkCellVO                              '一覧の入力制限用
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

    '-------------------------------------------------------------------------------
    'コンストラクタ（Privateにして、外からは呼べないようにする）
    '-------------------------------------------------------------------------------
    Private Sub New()
        InitializeComponent()

    End Sub

    '-------------------------------------------------------------------------------
    'コンストラクタ　メニューから呼ばれる
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmForm As ZC110M_Menu, ByVal prmSakuseiCD As String, ByVal prmUpdFlg As Boolean)
        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _parentForm = prmForm                                               '親フォーム
        _SakuseiCD = prmSakuseiCD                                           'メニュー画面で選択されたボタン
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

            If gh.getMaxRow > 0 Then

                For i As Integer = gh.getMaxRow - 1 To 0 Step -1
                    If Not gh.getCellData(COLDT_TAISYOGAI, i).Equals(gh.getCellData(COLDT_TAISYOGAICOPY, i)) Then
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
            Else
                '' 2011/01/13 del start sugano #処理制御テーブルは登録ボタン押下時のみ更新する
                ''対象データが０件の場合、処理制御テーブルを更新する
                'Dim PGID As String = IIf(TEHAI.Equals(_SakuseiCD), PGID1, PGID2)
                '_parentForm.updateSeigyoTbl(PGID, True, Now(), Now())
                '' 2011/01/13 del end sugano
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
    '登録ボタン押下時イベント
    '------------------------------------------------------------------------------------------------------
    Private Sub btnEntry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEntry.Click
        Try
            Dim lCntIns As Long = 0
            Dim lCntDel As Long = 0

            '登録チェック
            Call checkTouroku()

            '登録確認メッセージ
            Dim rtn As DialogResult
            Dim sql As String = ""
            sql = "SELECT COUNT(*)"
            sql = sql & N & " FROM T21SEISANM "
            If TEHAI.Equals(_SakuseiCD) Then
                sql = sql & N & " WHERE SAKUSEI_KBN = '" & SAKUSEIKBN_TEHAI & "'"
            Else
                sql = sql & N & " WHERE SAKUSEI_KBN = '" & SAKUSEIKBN_NYUKO & "'"
            End If

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If _db.rmNullStr(ds.Tables(RS).Rows(0)(0)) = 0 Then
                rtn = _msgHd.dspMSG("confirmInsert")         '登録します。
            Else
                rtn = _msgHd.dspMSG("confirmNotTorikomi")    '取込済データ破棄

                '削除件数編集
                lCntDel = CInt(_db.rmNullStr(ds.Tables(RS).Rows(0)(0)))
            End If
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


                '一覧のデータをワークテーブルに更新
                Call updateWK10()

                'トランザクション開始
                _db.beginTran()

                '生産見込テーブル登録
                Call torokuT21SEUSANM(dStartSysdate, sPCName)

                '処理終了日時の取得
                Dim dFinishSysdate As Date = Now()

                '追加件数編集
                lCntIns = _addcount

                '実行履歴テーブルの更新処理
                Call updT91Rireki(lCntIns, lCntDel, sPCName, dStartSysdate, dFinishSysdate)

                Dim PGID As String = IIf(TEHAI.Equals(_SakuseiCD), PGID1, PGID2)
                _parentForm.updateSeigyoTbl(PGID, True, dStartSysdate, dFinishSysdate)

                'トランザクション終了
                _db.commitTran()

                
                'チェックボックスを更新する
                Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)
                For i As Integer = gh.getMaxRow - 1 To 0 Step -1
                    If Not gh.getCellData(COLDT_TAISYOGAI, i).Equals(gh.getCellData(COLDT_TAISYOGAICOPY, i)) Then
                        gh.setCellData(COLDT_TAISYOGAICOPY, i, gh.getCellData(COLDT_TAISYOGAI, i))
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
    '全選択ボタン押下時イベント
    '------------------------------------------------------------------------------------------------------
    Private Sub btnZenSentaku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZenSentaku.Click
        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)

            '項目をすべてチェックさせる
            For i As Integer = gh.getMaxRow - 1 To 0 Step -1
                gh.setCellData(COLDT_TAISYOGAI, i, True)
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
                gh.setCellData(COLDT_TAISYOGAI, i, False)
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
            '作成区分の編集
            If TEHAI.Equals(_SakuseiCD) Then
                '手配済データ取込の場合
                rdoKubun1.Checked = True
                rdoKubun2.Enabled = False
            Else
                '入庫済データ取込の場合
                rdoKubun2.Checked = True
                rdoKubun1.Enabled = False
            End If

            '処理年月、計画年月表示
            Call dispDate()

            '端末IDの取得
            _tanmatuID = UtilClass.getComputerName

            '一覧表示
            Call dispdgv()

            '一覧行着色フラグを有効にする
            _colorCtlFlg = True

            '' 2011/01/13 del start sugano
            '#登録ボタンの制御タイミングを変更
            'btnEntry.Enabled = _updFlg
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
    '　コントロールキー押下イベント
    '　(処理概要)エンターボタン押下時に次のコントロールに移る
    '-------------------------------------------------------------------------------
    Private Sub ctl_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles rdoKubun1.KeyPress, _
                                                                                                                rdoKubun2.KeyPress, _
                                                                                                                btnZenKaijo.KeyPress, _
                                                                                                                btnZenSentaku.KeyPress, _
                                                                                                                btnEntry.KeyPress, _
                                                                                                                btnBack.KeyPress
        '押下キーがEnterの場合、次のコントロールへフォーカス移動
        Call UtilClass.moveNextFocus(Me, e)
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　検索条件SQL作成
    '　(処理概要)SQLを作成する
    '　　I　：　prmSakuseiCD　　　'作成区分
    '　　R　：　createSerch       '検索条件
    '------------------------------------------------------------------------------------------------------
    Private Function createSerch(ByVal prmSakuseiCD As String) As String
        Try

            createSerch = ""
            createSerch = "INSERT INTO ZG210E_W10 ("
            createSerch = createSerch & N & " HINMEICD "        '実品名コード
            createSerch = createSerch & N & " ,SIYOU_CD "       '仕様コード
            createSerch = createSerch & N & " ,HIN_CD "         '品種コード
            createSerch = createSerch & N & " ,SENSIN_CD "      '線心数コード
            createSerch = createSerch & N & " ,SIZE_CD "        'サイズコード
            createSerch = createSerch & N & " ,COLOR_CD "       '色コード
            createSerch = createSerch & N & " ,HINMEI "         '品名
            createSerch = createSerch & N & " ,KHINMEICD "      '計画品名コード
            createSerch = createSerch & N & " ,NEN "            '年
            createSerch = createSerch & N & " ,TEHAINO "        '手配№
            createSerch = createSerch & N & " ,RENBAN "         '連番
            createSerch = createSerch & N & " ,SEIBAN "         '製番
            createSerch = createSerch & N & " ,TEHAI_KBN "      '手配区分
            createSerch = createSerch & N & " ,TANCYO "         '単長
            createSerch = createSerch & N & " ,JYOSU "          '条数
            createSerch = createSerch & N & " ,TEHAISU "        '手配数
            createSerch = createSerch & N & " ,KYUTTAIBI "      '希望出来日
            createSerch = createSerch & N & " ,YSYUTTAIBI "     '予定出来日
            createSerch = createSerch & N & " ,NYUUKOSU "       '入庫数
            createSerch = createSerch & N & " ,MINOUZAN "       '未納残
            createSerch = createSerch & N & " ,SMIKOMISU "      '生産見込
            createSerch = createSerch & N & " ,NENGETSU "       '年月
            createSerch = createSerch & N & " ,NYUKO_FLG "      '内入フラグ
            If NYUKO.Equals(prmSakuseiCD) Then
                createSerch = createSerch & N & " ,NYUKOBI "      '入庫日
            End If
            createSerch = createSerch & N & " ,SAKUSEI_KBN "    '作成区分
            createSerch = createSerch & N & " ,TAISYO_FLG "     '対象フラグ
            createSerch = createSerch & N & " ,UPDNAME "        '端末ID
            createSerch = createSerch & N & " ,UPDDATE) "       '更新日時
            createSerch = createSerch & N & " SELECT "
            createSerch = createSerch & N & "   RPAD(T03.SHIYOCD, 2) || T03.HINSYUCD || T03.SENSINSUCD || T03.SIZECD || T03.IROCD "
            createSerch = createSerch & N & "   , T03.SHIYOCD "
            createSerch = createSerch & N & "   , T03.HINSYUCD "
            createSerch = createSerch & N & "   , T03.SENSINSUCD "
            createSerch = createSerch & N & "   , T03.SIZECD "
            createSerch = createSerch & N & "   , T03.IROCD "
            createSerch = createSerch & N & "   , T03.HINNM "
            createSerch = createSerch & N & "   , M12.KHINMEICD "

            If NYUKO.Equals(prmSakuseiCD) Then
                createSerch = createSerch & N & "   , T04.NENDO "
                createSerch = createSerch & N & "   , T04.TEHAINO "
                createSerch = createSerch & N & "   , T04.NYUKONO "
            Else
                createSerch = createSerch & N & "   , T03.NENDO "
                createSerch = createSerch & N & "   , T03.TEHAINO "
                createSerch = createSerch & N & "   , " & RENBAN & " "
            End If

            createSerch = createSerch & N & "   , T03.SEIBAN "
            createSerch = createSerch & N & "   , T03.TEHAIKBN "
            createSerch = createSerch & N & "   , T03.TANTYO "
            createSerch = createSerch & N & "   , T03.INSU "
            createSerch = createSerch & N & "   , T03.TEHAISU "
            createSerch = createSerch & N & "   , T03.KIBOUDATE "
            createSerch = createSerch & N & "   , T03.YOTEIDATE "

            If NYUKO.Equals(prmSakuseiCD) Then
                createSerch = createSerch & N & "   , T04.NYUKOSUU "
                createSerch = createSerch & N & "   , " & MINOUZAN & " "
                createSerch = createSerch & N & "   , T04.NYUKOSUU "
            Else
                createSerch = createSerch & N & "   , T03.NYUKOSUM "
                createSerch = createSerch & N & "   , T03.MINOUZAN "
                createSerch = createSerch & N & "   , T03.MINOUZAN "
            End If

            createSerch = createSerch & N & "   , TO_CHAR(TO_DATE(T03.KIBOUDATE,'YYYYMMDD'),'yyyymm') "

            If NYUKO.Equals(prmSakuseiCD) Then
                createSerch = createSerch & N & "   , CASE WHEN T04.NYUKOSUU > 0 THEN '" & NYUKOFLG & "' END"
                createSerch = createSerch & N & "   , T04.NYUKODATE "
                createSerch = createSerch & N & "   , '" & SAKUSEIKBN_NYUKO & "'"
            Else
                createSerch = createSerch & N & "   , CASE WHEN T03.NYUKOSUM > 0 THEN '" & NYUKOFLG & "' END"
                createSerch = createSerch & N & "   , '" & SAKUSEIKBN_TEHAI & "'"
            End If

            createSerch = createSerch & N & "   , '" & TAISYOFLG & "'"
            createSerch = createSerch & N & "   , '" & _tanmatuID & "'"
            createSerch = createSerch & N & "   , TO_DATE('" & _updateDate & "', 'YYYY/MM/DD HH24:MI:SS')"
            createSerch = createSerch & N & " FROM  T03MINOU T03 "
            createSerch = createSerch & N & "   INNER JOIN M12SYUYAKU M12 "
            createSerch = createSerch & N & "   ON SUBSTR(T03.COSTCD,0,13) = M12.HINMEICD "
            createSerch = createSerch & N & "   INNER JOIN M11KEIKAKUHIN M11 "
            createSerch = createSerch & N & "   ON M11.TT_KHINMEICD = M12.KHINMEICD "

            If NYUKO.Equals(prmSakuseiCD) Then
                createSerch = createSerch & N & "   INNER JOIN T04NYUKO T04 "
                createSerch = createSerch & N & "   ON T03.NENDO = T04.NENDO "
                createSerch = createSerch & N & "   AND T03.TEHAINO = T04.TEHAINO "
                createSerch = createSerch & N & " WHERE TO_CHAR(TO_DATE(T04.NYUKODATE,'YYYYMMDD'),'yyyy/mm') = '" & lblSyori.Text & "'"
                createSerch = createSerch & N & " AND M11.TT_SYUBETU = '1'" '在庫
            Else
                createSerch = createSerch & N & " WHERE T03.DELFLG IS NULL"
                createSerch = createSerch & N & " AND T03.NYUFLG = '0'"
                createSerch = createSerch & N & " AND M11.TT_SYUBETU = '1'" '在庫
            End If

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Function

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

    '-------------------------------------------------------------------------------
    '　一覧表示
    '　(処理概要)一覧表示データをWK01に保持し、一覧に表示する
    '-------------------------------------------------------------------------------
    Private Sub dispdgv()
        Try

            'トランザクション開始
            _db.beginTran()

            Dim sql As String = ""
            sql = " DELETE FROM ZG210E_W10 WHERE UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql)

            '更新日時を取得
            _updateDate = Now

            If TEHAI.Equals(_SakuseiCD) Then
                'T03未納残テーブル、M12集約商品マスタ
                sql = createSerch(_SakuseiCD)
            Else
                'T03未納残テーブル、T04入庫状況テーブル
                sql = createSerch(_SakuseiCD)
            End If
            _db.executeDB(sql)


            'M11計画対象品マスタ
            sql = ""
            sql = sql & N & "UPDATE ZG210E_W10 W10"
            sql = sql & N & "SET (JUYOU_CD, HINSYU_KBN) = ("
            sql = sql & N & " SELECT M11.TT_JUYOUCD, M11.TT_HINSYUKBN FROM M11KEIKAKUHIN M11 "
            sql = sql & N & " WHERE M11.TT_KHINMEICD = W10.KHINMEICD) "
            sql = sql & N & "WHERE W10.UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql)

            'M10汎用マスタ
            '内入フラグ名
            sql = ""
            sql = sql & N & "UPDATE ZG210E_W10 W10"
            sql = sql & N & "SET (NYUKO_FLGNM) = ("
            sql = sql & N & " SELECT M01.NAME1 FROM M01HANYO M01 "
            sql = sql & N & " WHERE M01.KOTEIKEY = '" & COTEI_NYUKO & "' "
            sql = sql & N & " AND M01.KAHENKEY = W10.NYUKO_FLG) "
            sql = sql & N & "WHERE W10.UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql)

            '手配区分名
            sql = ""
            sql = sql & N & "UPDATE ZG210E_W10 W10"
            sql = sql & N & "SET (TEHAI_KBNNM) = ("
            sql = sql & N & " SELECT M01.NAME1 FROM M01HANYO M01 "
            sql = sql & N & " WHERE M01.KOTEIKEY = '" & COTEI_TEHAI & "' "
            sql = sql & N & " AND M01.KAHENKEY = W10.TEHAI_KBN) "
            sql = sql & N & "WHERE W10.UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql)

            '需要先名、需要先表示順
            sql = ""
            sql = sql & N & "UPDATE ZG210E_W10 W10"
            sql = sql & N & "SET (JUYOU_NM,JUYOU_SORT) = ("
            sql = sql & N & " SELECT M01.NAME2,M01.SORT FROM M01HANYO M01 "
            sql = sql & N & " WHERE M01.KOTEIKEY = '" & COTEI_JUYOU & "' "
            sql = sql & N & " AND M01.KAHENKEY = W10.JUYOU_CD) "
            sql = sql & N & "WHERE W10.UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql)

            'トランザクション終了
            _db.commitTran()

            '一覧表示
            Call dispWK10()

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        Finally
            If _db.isTransactionOpen = True Then
                _db.rollbackTran()                          'ロールバック
            End If
            'マウスカーソル矢印
            Me.Cursor = Cursors.Arrow
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　ワークテーブルデータの一覧表示
    '　(処理概要)ワークテーブルのデータを一覧に表示する
    '-------------------------------------------------------------------------------
    Private Sub dispWK10()
        Try
            'ワークのデータを一覧に表示
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)

            gh.clearRow()
            dgvList.Enabled = False

            Dim sql As String = ""
            sql = sql & N & " SELECT "
            sql = sql & N & "  CASE TAISYO_FLG WHEN '" & TAISYO & "' THEN '" & TAISYO_ARI & "' ELSE '" & TAISYO_GAI & "' END " & COLDT_TAISYOGAI       '対象
            sql = sql & N & " ,CASE TAISYO_FLG WHEN '" & TAISYO & "' THEN '" & TAISYO_ARI & "' ELSE '" & TAISYO_GAI & "' END " & COLDT_TAISYOGAICOPY   '対象コピー
            sql = sql & N & " ,NYUKO_FLGNM " & COLDT_UCHIIRE      '内入
            sql = sql & N & " ,TEHAI_KBNNM " & COLDT_TEHAIKBN     '手配
            sql = sql & N & " ,TO_CHAR(TO_DATE(KYUTTAIBI,'YYYYMMDD'),'yy/mm/dd') " & COLDT_SYUTTAIBI      '希望出来日
            sql = sql & N & " ,TO_CHAR(TO_DATE(YSYUTTAIBI,'YYYYMMDD'),'yy/mm/dd') " & COLDT_YOTEI         '予定出来日
            sql = sql & N & " ,TEHAINO " & COLDT_TEHAINO          '手配№
            sql = sql & N & " ,SEIBAN " & COLDT_SEIBAN            '製番
            sql = sql & N & " ,HINMEI " & COLDT_HINMEI            '品名
            sql = sql & N & " ,TEHAISU " & COLDT_TEHAISU          '手配数
            sql = sql & N & " ,TANCYO " & COLDT_TANTYOU           '単長
            sql = sql & N & " ,JYOSU " & COLDT_JOUSUU             '条数
            sql = sql & N & " ,SMIKOMISU " & COLDT_SEISAN         '生産見込
            sql = sql & N & " ,CASE WHEN NENGETSU IS NULL THEN '' ELSE TO_CHAR(TO_DATE(NENGETSU,'YYYYMM'),'yyyy/mm') END " & COLDT_TUKI            '年月
            sql = sql & N & " ,HINMEICD " & COLDT_HINCD           '実名品コード
            sql = sql & N & " ,CASE KHINMEICD WHEN HINMEICD THEN '' ELSE KHINMEICD END " & COLDT_OYACD          '計画品名コード
            sql = sql & N & " ,JUYOU_NM " & COLDT_JUYOSAKI        '需要先名
            sql = sql & N & " ,HINSYU_KBN " & COLDT_HINSYU        '品種区分
            sql = sql & N & " ,NEN " & COLDT_NEN                  '年　　　　(隠し列)
            sql = sql & N & " ,RENBAN " & COLDT_RENBAN            '連番　　　(隠し列)
            sql = sql & N & " ,NULL " & COLDT_HENKOUFLG           '変更フラグ(隠し列)
            sql = sql & N & " FROM ZG210E_W10 "
            sql = sql & N & "   WHERE UPDNAME = '" & _tanmatuID & "'"
            '-->2010.12.25 chg by takagi #44
            'ソートキー変更（品種→線心数→サイズ→仕様→色）
            'sql = sql & N & " ORDER BY JUYOU_SORT, HINSYU_KBN, HIN_CD, SENSIN_CD, SIZE_CD, SIYOU_CD, COLOR_CD, TEHAINO"
            sql = sql & N & " ORDER BY HIN_CD, SENSIN_CD, SIZE_CD, SIYOU_CD, COLOR_CD, TEHAINO"
            '<--2010.12.25 chg by takagi #44

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            '' 2011/01/13 add start sugano
            '登録ボタンは一覧の件数にかかわらず制御する
            btnEntry.Enabled = _updFlg
            '' 2011/01/13 add end sugano

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                dgvList.Enabled = False         '一覧の使用不可
                '' 2011/01/13 del start sugano
                'btnEntry.Enabled = False
                '' 2011/01/13 del end sugano
                btnZenSentaku.Enabled = False
                btnZenKaijo.Enabled = False

                '一覧の件数を表示する
                txtCnt.Text = CStr(iRecCnt) & "件"
                '表示件数の保存
                _addcount = CLng(iRecCnt)

                Throw New UsrDefException("対象データがありません。", _msgHd.getMSG("NonData"))
            Else                                    '抽出データがある場合、登録ボタン有効
                dgvList.Enabled = True          '一覧の使用不可
                '' 2011/01/13 del start sugano
                'btnEntry.Enabled = _updFlg
                '' 2011/01/13 del end sugano
                btnZenSentaku.Enabled = True
                btnZenKaijo.Enabled = True
            End If

            '抽出データを一覧に表示する
            dgvList.DataSource = ds
            dgvList.DataMember = RS

            '一覧の件数を表示する
            txtCnt.Text = CStr(iRecCnt) & "件"
            '表示件数の保存
            _addcount = CLng(iRecCnt)

            '一覧先頭行選択--------------------------------------------------------------
            gh.setSelectionRowColor(0, 0, StartUp.lCOLOR_BLUE)

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　ワークテーブルデータの更新
    '　(処理概要)一覧に表示されているデータをワークテーブルに更新する
    '-------------------------------------------------------------------------------
    Private Sub updateWK10()
        Try

            Dim sql As String = ""
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)

            'トランザクション開始
            _db.beginTran()

            '行数分だけループ
            For i As Integer = 0 To gh.getMaxRow - 1
                If HENKO_FLG.Equals(dgvList(COLNO_HENKOUFLG, i).Value.ToString) Or Not gh.getCellData(COLDT_TAISYOGAI, i).Equals(gh.getCellData(COLDT_TAISYOGAICOPY, i)) Then
                    sql = ""
                    sql = sql & N & " UPDATE ZG210E_W10 SET "
                    sql = sql & N & " SMIKOMISU = TO_NUMBER('" & _db.rmNullStr(dgvList(COLNO_SEISAN, i).Value) & "') "
                    sql = sql & N & " ,NENGETSU = '" & Trim(Replace(_db.rmNullStr(dgvList(COLNO_TUKI, i).Value), "/", "")) & "' "
                    If TAISYO_ARI.Equals(gh.getCellData(COLDT_TAISYOGAI, i)) Then
                        sql = sql & N & " ,TAISYO_FLG = '" & TAISYO & "' "
                    Else
                        sql = sql & N & " ,TAISYO_FLG = ''"
                    End If
                    sql = sql & N & "   WHERE UPDNAME = '" & _tanmatuID & "'"
                    sql = sql & N & "   AND NEN = '" & dgvList(COLNO_NEN, i).Value & "'"
                    sql = sql & N & "   AND TEHAINO = '" & dgvList(COLNO_TEHAINO, i).Value & "'"
                    sql = sql & N & "   AND RENBAN = '" & dgvList(COLNO_RENBAN, i).Value & "'"
                    _db.executeDB(sql)
                End If
            Next

            'トランザクション終了
            _db.commitTran()

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
    '　　I　：　prmCntDel        　削除件数
    '　　I　：　prmPCName      　　端末ID
    '　　I　：　prmStartDate       処理開始日時
    '　　I　：　prmFinishDate      処理終了日時
    '------------------------------------------------------------------------------------------------------
    Private Sub updT91Rireki(ByVal prmCntIns As Long, ByVal prmCntDel As Long, ByVal prmPCName As String, _
                             ByVal prmStartDate As Date, ByVal prmFinishDate As Date)
        Try
            '登録処理
            Dim sql As String = ""
            sql = "INSERT INTO T91RIREKI ("
            sql = sql & N & "  SNENGETU"                                                    '処理年月
            sql = sql & N & ", KNENGETU"                                                    '計画年月
            sql = sql & N & ", PGID"                                                        '機能ID
            sql = sql & N & ", SDATESTART"                                                  '処理開始日時
            sql = sql & N & ", SDATEEND"                                                    '処理終了日時
            sql = sql & N & ", KENNSU1"                                                     '件数１（削除件数）
            sql = sql & N & ", KENNSU2"                                                     '件数２（登録件数）
            sql = sql & N & ", UPDNAME"                                                     '端末ID
            sql = sql & N & ", UPDDATE"                                                     '更新日時
            sql = sql & N & ") VALUES ("

            sql = sql & N & "  '" & Trim(Replace(lblSyori.Text, "/", "")) & "'"             '処理年月
            sql = sql & N & ", '" & Trim(Replace(lblKeikaku.Text, "/", "")) & "'"           '計画年月
            If TEHAI.Equals(_SakuseiCD) Then
                sql = sql & N & ",  '" & PGID1 & "'"                                        '機能ID(手配済データ)
            Else
                sql = sql & N & ",  '" & PGID2 & "'"                                        '機能ID(入庫済データ)
            End If

            sql = sql & N & ", TO_DATE('" & prmStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "   '処理開始日時
            sql = sql & N & ", TO_DATE('" & prmFinishDate & "', 'YYYY/MM/DD HH24:MI:SS') "  '処理終了日時
            sql = sql & N & ", " & prmCntDel                                                '件数１（削除件数）
            sql = sql & N & ", " & prmCntIns                                                '件数２（登録件数）
            sql = sql & N & ", '" & prmPCName & "'"                                         '端末ID
            sql = sql & N & ", TO_DATE('" & prmFinishDate & "', 'YYYY/MM/DD HH24:MI:SS') "  '更新日時
            sql = sql & N & " )"
            _db.executeDB(sql)

        Catch ex As Exception
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　生産見込テーブルの登録処理
    '　　I　：　prmSysdate       処理開始日時
    '　　I　：　prmPCName      　端末ID
    '------------------------------------------------------------------------------------------------------
    Private Sub torokuT21SEUSANM(ByVal prmSysdate As Date, ByVal prmPCName As String)
        Try
            '削除処理
            'SQL文発行
            Dim sql As String = ""
            sql = "DELETE FROM T21SEISANM"
            If TEHAI.Equals(_SakuseiCD) Then
                sql = sql & N & " WHERE SAKUSEI_KBN = '" & SAKUSEIKBN_TEHAI & "'"
            Else
                sql = sql & N & " WHERE SAKUSEI_KBN = '" & SAKUSEIKBN_NYUKO & "'"
            End If
            _db.executeDB(sql)

            '登録処理
            'SQL文発行
            sql = ""
            sql = "INSERT INTO T21SEISANM ("
            sql = sql & N & "  HINMEICD"                                                  '実品名コード
            sql = sql & N & ", SIYOU_CD"                                                  '仕様コード
            sql = sql & N & ", HIN_CD"                                                    '品種コード
            sql = sql & N & ", SENSIN_CD"                                                 '線心数コード
            sql = sql & N & ", SIZE_CD"                                                   'サイズコード
            sql = sql & N & ", COLOR_CD"                                                  '色コード
            sql = sql & N & ", HINMEI"                                                    '品名
            sql = sql & N & ", KHINMEICD"                                                 '計画品名コード
            sql = sql & N & ", NEN"                                                       '年
            sql = sql & N & ", TEHAINO"                                                   '手配№
            sql = sql & N & ", RENBAN"                                                    '連番
            sql = sql & N & ", SEIBAN"                                                    '製番
            sql = sql & N & ", TEHAI_KBN"                                                 '手配区分
            sql = sql & N & ", TANCYO"                                                    '単長
            sql = sql & N & ", JYOSU"                                                     '条数
            sql = sql & N & ", TEHAISU"                                                   '手配数
            sql = sql & N & ", KYUTTAIBI"                                                 '希望出来日
            sql = sql & N & ", YSYUTTAIBI"                                                '予定出来日
            sql = sql & N & ", NYUUKOSU"                                                  '入庫数
            sql = sql & N & ", MINOUZAN"                                                  '未納残
            sql = sql & N & ", SMIKOMISU"                                                 '生産見込数
            sql = sql & N & ", NENGETSU"                                                  '年月
            sql = sql & N & ", JUYOU_CD"                                                  '需要先
            sql = sql & N & ", HINSYU_KBN"                                                '品種区分
            sql = sql & N & ", NYUKO_FLG"                                                 '内入フラグ
            sql = sql & N & ", TAISYO_FLG"                                                '対象フラグ
            sql = sql & N & ", SAKUSEI_KBN"                                               '作成区分
            sql = sql & N & ", NYUKOBI"                                                   '入庫日
            sql = sql & N & ", UPDNAME"                                                   '端末ID
            sql = sql & N & ", UPDDATE )"                                                 '更新日時
            sql = sql & N & " SELECT "
            sql = sql & N & "  HINMEICD"                                                  '実品名コード
            sql = sql & N & ", SIYOU_CD"                                                  '仕様コード
            sql = sql & N & ", HIN_CD"                                                    '品種コード
            sql = sql & N & ", SENSIN_CD"                                                 '線心数コード
            sql = sql & N & ", SIZE_CD"                                                   'サイズコード
            sql = sql & N & ", COLOR_CD"                                                  '色コード
            sql = sql & N & ", HINMEI"                                                    '品名
            sql = sql & N & ", KHINMEICD"                                                 '計画品名コード
            sql = sql & N & ", NEN"                                                       '年
            sql = sql & N & ", TEHAINO"                                                   '手配№
            sql = sql & N & ", RENBAN"                                                    '連番
            sql = sql & N & ", SEIBAN"                                                    '製番
            sql = sql & N & ", TEHAI_KBN"                                                 '手配区分
            sql = sql & N & ", TANCYO"                                                    '単長
            sql = sql & N & ", JYOSU"                                                     '条数
            sql = sql & N & ", TEHAISU"                                                   '手配数
            sql = sql & N & ", KYUTTAIBI"                                                 '希望出来日
            sql = sql & N & ", YSYUTTAIBI"                                                '予定出来日
            sql = sql & N & ", NYUUKOSU"                                                  '入庫数
            sql = sql & N & ", MINOUZAN"                                                  '未納残
            sql = sql & N & ", SMIKOMISU"                                                 '生産見込数
            sql = sql & N & ", NENGETSU"                                                  '年月
            sql = sql & N & ", JUYOU_CD"                                                  '需要先
            sql = sql & N & ", HINSYU_KBN"                                                '品種区分
            sql = sql & N & ", NYUKO_FLG"                                                 '内入フラグ
            sql = sql & N & ", TAISYO_FLG"                                                '対象フラグ
            sql = sql & N & ", SAKUSEI_KBN"                                               '作成区分
            sql = sql & N & ", NYUKOBI"                                                   '入庫日
            sql = sql & N & ", UPDNAME "                                                  '端末ID
            sql = sql & N & ", TO_DATE('" & prmSysdate & "', 'YYYY/MM/DD HH24:MI:SS')"
            sql = sql & N & " FROM ZG210E_W10 "
            sql = sql & N & " WHERE UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql)


        Catch ex As Exception
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub
#End Region

#Region "ユーザ定義関数:チェック処理"
    '------------------------------------------------------------------------------------------------------
    '  登録チェック
    '　(処理概要)各項目の必須項目、形式チェックを行う
    '------------------------------------------------------------------------------------------------------
    Private Sub checkTouroku()
        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvList)

            For i As Integer = 0 To gh.getMaxRow - 1
                If TAISYO_ARI.Equals(gh.getCellData(COLDT_TAISYOGAI, i)) Then
                    '必須入力チェック
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
