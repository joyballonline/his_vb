'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）負荷山積集計結果確認(工程別)
'    （フォームID）ZG730Q_FukaYamadumiKoutei
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   鈴木        2010/11/19                 新規              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.Combo
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory

Public Class ZG730Q_FukaYamadumiKoutei
    Inherits System.Windows.Forms.Form
    Implements IfRturnSeisanNouryoku

#Region "リテラル値定義"

    'PG制御文字
    Private Const RS As String = "RecSet"                       'データセットテーブル名
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const T As String = ControlChars.Tab                '区切文字

    '一覧データバインド列名
    Private Const COLDT_KOUTEI As String = "dtKoutei"                   '工程
    Private Const COLDT_MASHINENAME As String = "dtMashineName"         '機械略記号
    Private Const COLDT_SEISANNOURYOKU As String = "dtSeisanNouryoku"   'MCH生産能力
    Private Const COLDT_MCHGOUKEI As String = "dtMCHGoukei"             'MCH山積分
    Private Const COLDT_MCHHAKKOU As String = "dtMCHHakkou"             'MCH製作伝票発行分
    Private Const COLDT_MCHMIHAKKOU As String = "dtMCHMihakkou"         'MCH製作伝票未発行分
    Private Const COLDT_MCHGETUJIZAIKO As String = "dtMCHGetujiZaiko"   'MCH月次在庫分
    Private Const COLDT_OVERMCH As String = "dtOverMCH"                 'MCHオーバー分
    Private Const COLDT_MHGOUKEI As String = "dtMHGoukei"               'MH山積分
    Private Const COLDT_MHHAKKOU As String = "dtMHHakkou"               'MH製作伝票発行分
    Private Const COLDT_MHMIHAKKOU As String = "dtMHMihakkou"           'MH製作伝票未発行分
    Private Const COLDT_MHGETUJIZAIKO As String = "dtMHGetujiZaiko"     'MH月次在庫分

    '一覧グリッド列名
    Private Const COLCN_MEISAICHK As String = "cnMeisaiChk"             '明細ボタン
    Private Const COLCN_KOUTEI As String = "cnKoutei"                   '工程
    Private Const COLCN_MASHINENAME As String = "cnMashineName"         '機械略記号
    Private Const COLCN_SEISANNOURYOKU As String = "cnSeisanNouryoku"   'MCH生産能力
    Private Const COLCN_MCHGOUKEI As String = "cnMCHGoukei"             'MCH山積分
    Private Const COLCN_MCHHAKKOU As String = "cnMCHHakkou"             'MCH製作伝票発行分
    Private Const COLCN_MCHMIHAKKOU As String = "cnMCHMihakkou"         'MCH製作伝票未発行分
    Private Const COLCN_MCHGETUJIZAIKO As String = "cnMCHGetujiZaiko"   'MCH月次在庫分
    Private Const COLCN_OVERMCH As String = "cnOverMCH"                 'MCHオーバー分
    Private Const COLCN_MHGOUKEI As String = "cnMHGoukei"               'MH山積分
    Private Const COLCN_MHHAKKOU As String = "cnMHHakkou"               'MH製作伝票発行分
    Private Const COLCN_MIHAKKOU As String = "cnMihakkou"               'MH製作伝票未発行分
    Private Const COLCN_MHGETUJIZAIKO As String = "cnMHGetujiZaiko"     'MH月次在庫分

    Private Const KOUTEI As String = "12"                               '工程の固定キー
    Private Const MAXSORT As Integer = 99999999                         'ソート番号の最大値

    Private PGID As String = "ZG730Q"                                   'T02に登録するための機能ＩＤ

#End Region

#Region "メンバ変数定義"

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu
    Private _updFlg As Boolean = False

    Private _selectedCbKoutei As String         '検索時の工程名コード
    Private _selectedCbKikai As String          '検索時の機械略記号
    Private _selectedCbCdKouitei As String      '工程名コンボボックスのコード
    Private _selectedCbCdKikai As String        '機械略記号コンボボックスのコード

    Private _kensuu As Integer                  '一覧に表示されている件数

    Private _oldRowIndex As Integer = -1        '選択行の背景色を変更するためのフラグを宣言
    Private _colorCtlFlg As Boolean = False     '選択行の背景色を変更するためのフラグを宣言

    Private _startDate As Date                  'T02に登録する処理開始日時
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
        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

    End Sub

    '-------------------------------------------------------------------------------
    'コンストラクタ　メニューから呼ばれる
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmForm As ZC110M_Menu, ByVal prmUpdFlg As Boolean)

        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                    'MSGハンドラの設定
        _db = prmRefDbHd                                        'DBハンドラの設定
        _parentForm = prmForm                                   '親フォーム
        _kensuu = 0
        lblKensuu.Text = "0件"
        StartPosition = FormStartPosition.CenterScreen          '画面中央表示
        _updFlg = prmUpdFlg
        _startDate = Now                                        '処理開始時刻保持

    End Sub

#End Region

#Region "Formイベント"


    '-------------------------------------------------------------------------------
    '　処理開始イベント
    '-------------------------------------------------------------------------------
    Private Sub ZG730Q_FukaYamadumiKoutei_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            '描画関係の設定
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           'サイズが変更されたときに、コントロールがコントロール自体を再描画するかどうかを示す値を設定
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '描画はバッファで実行され、完了後に、結果が画面に出力されるよう設定
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  'コントロールは、画面に直接ではなく、まずバッファに描画されます。これにより、ちらつきを抑えることができます。
            Me.SetStyle(ControlStyles.UserPaint, True)              'コントロールは、オペレーティング システムによってではなく、独自に描画されるよう設定
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   'コントロールはウィンドウ メッセージ WM_ERASEBKGND を無視するように設定

            Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr 'タイトルオプション表示

            '初期化
            Call initForm()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

#End Region

#Region "ボタンイベント"

    '------------------------------------------------------------------------------------------------------
    '　検索ボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnKensaku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensaku.Click

        Try
            '列着色フラグ無効
            _colorCtlFlg = False

            Call dispDGV()     '検索処理へ

            '列着色フラグ有効
            _colorCtlFlg = True

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　戻るボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnModoru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModoru.Click

        If "設定あり".Equals(lblSeisanSettei.Text) Then
            'T02処理制御テーブル更新
            _parentForm.updateSeigyoTbl(PGID, True, _startDate, Now)
        End If

        '自画面を終了し、メニュー画面に戻る。
        _parentForm.Show()
        _parentForm.Activate()

        Me.Close()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　生産能力設定ボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnSeisan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeisan.Click

        Dim openForm As ZG710E_SeisanNouryoku = New ZG710E_SeisanNouryoku(_msgHd, _db, Me, _kensuu)      '画面遷移
        openForm.Show()    '画面表示
        Me.Hide()

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　EXCELボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click

        Try
            ' 元のWaitカーソルを保持
            Dim cur As Cursor = Me.Cursor
            ' カーソルを待機カーソルに変更
            Me.Cursor = Cursors.WaitCursor

            Try
                'EXCEL出力
                Call printExcel()

            Finally
                ' カーソルを元に戻す
                Me.Cursor = cur
            End Try

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

#End Region

#Region "データグリッド操作"
    '-->2010.12.27 add by takagi 
    Private Sub dgvFukaKakunin_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvFukaKakunin.DoubleClick
        If dgvFukaKakunin.CurrentCellAddress.Y < 0 Then Exit Sub
        Dim ee As System.Windows.Forms.DataGridViewCellEventArgs = New System.Windows.Forms.DataGridViewCellEventArgs(0, dgvFukaKakunin.CurrentCellAddress.Y)
        Call dgvFukaKakunin_CellContentClick(sender, ee) 'ボタン押下転送
    End Sub
    '<--2010.12.27 add by takagi 

    '------------------------------------------------------------------------------------------------------
    '　データグリッド押下
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvFukaKakunin_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvFukaKakunin.CellContentClick

        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvFukaKakunin)
            Dim clickMeisaiBtn As Integer        '明細ボタンが押下された行数
            Dim nowRow As Integer = e.RowIndex   '現在の行数

            '押下行を水色に着色
            dgvFukaKakunin.CurrentCell = dgvFukaKakunin(COLCN_MEISAICHK, nowRow)
            gh.setSelectionRowColor(nowRow, _oldRowIndex, StartUp.lCOLOR_BLUE)

            'ボタン列の色をシルバーに戻す
            dgvFukaKakunin(COLCN_MEISAICHK, nowRow).Style.BackColor = SystemColors.Control

            _oldRowIndex = nowRow

            'データグリッドのボタンが押下された場合、明細画面に遷移
            If gh.getClickBtn(e, clickMeisaiBtn) = True Then

                Dim openForm As ZG731Q_FukaYamadumiMeisai = New ZG731Q_FukaYamadumiMeisai(_msgHd, _db, Me, _
                                                                        _db.rmNullStr(gh.getCellData(COLDT_MASHINENAME, clickMeisaiBtn)), _
                                                                        _db.rmNullStr(gh.getCellData(COLDT_SEISANNOURYOKU, clickMeisaiBtn)), _
                                                                        _db.rmNullStr(gh.getCellData(COLDT_MCHGOUKEI, clickMeisaiBtn)), _
                                                                        _db.rmNullStr(gh.getCellData(COLDT_OVERMCH, clickMeisaiBtn)), _
                                                                        _db.rmNullStr(gh.getCellData(COLDT_MHGOUKEI, clickMeisaiBtn)), _
                                                                        lblSyori.Text, lblKeikaku.Text)      '画面遷移
                openForm.Show() '画面表示
                Me.Hide()

            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　　選択行に着色する処理
    '　　(処理概要）選択行に着色する。
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvFukaKakunin_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvFukaKakunin.CellEnter

        Try
            If _colorCtlFlg Then
                Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvFukaKakunin)
                '背景色の設定
                Call setBackcolor(dgvFukaKakunin.CurrentCellAddress.Y, _oldRowIndex)
            End If
            _oldRowIndex = dgvFukaKakunin.CurrentCellAddress.Y
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub


    '------------------------------------------------------------------------------------------------------
    '　背景色の設定処理
    '　(処理概要)行の背景列を青にし、ボタンの列を元に戻す。
    '　　I　：　prmRowIndex     現在フォーカスがある行数
    '　　I　：　prmOldRowIndex  現在の行に移る前の行数
    '------------------------------------------------------------------------------------------------------
    Private Sub setBackcolor(ByVal prmRowIndex As Integer, ByVal prmOldRowIndex As Integer)

        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvFukaKakunin)

            '指定した行の背景色を青にする
            gh.setSelectionRowColor(prmRowIndex, prmOldRowIndex, StartUp.lCOLOR_BLUE)

            'ボタン列の色も変わってしまうので、戻す処理
            dgvFukaKakunin(COLCN_MEISAICHK, prmRowIndex).Style.BackColor = SystemColors.Control

            _oldRowIndex = prmRowIndex

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

#End Region

#Region "ユーザ定義関数"

    '-------------------------------------------------------------------------------
    '　画面起動時
    '-------------------------------------------------------------------------------
    Private Sub initForm()
        Try
            '処理年月、計画年月表示
            Call dispDate()

            'コンボボックス作成
            Call setCbo()

            '生産能力設定ラベル作成
            Call dispSeisan()

            'エクセルボタンの使用不可
            btnExcel.Enabled = False

            '生産能力設定ボタン使用可否
            btnSeisan.Enabled = _updFlg

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

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

            Dim syoriDate As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("SYORI"))     '処理年月
            Dim keikakuDate As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("KEIKAKU")) '計画年月

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
    '　工程名コンボボックス・機械略記号のセット
    '　(処理概要)M01汎用マスタから工程名・機械略記号を抽出して表示する。
    '-------------------------------------------------------------------------------
    Private Sub setCbo()

        Try
            '工程コンボボックス作成用SQL
            Dim sql = ""
            sql = sql & N & " SELECT DISTINCT "
            sql = sql & N & " M21.KOUTEI KOUTEI, M01.KAHENKEY KAHEN, M01.SORT "
            sql = sql & N & " FROM M21SEISAN M21 "
            sql = sql & N & " LEFT OUTER JOIN (SELECT KAHENKEY,SORT FROM M01HANYO WHERE KOTEIKEY = '" & KOUTEI & "') M01 "
            sql = sql & N & " ON M01.KAHENKEY = M21.KOUTEI  "
            sql = sql & N & " ORDER BY NVL(M01.SORT, " & MAXSORT & ")"

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            Dim ckoutei As UtilComboBoxHandler = New UtilComboBoxHandler(cboKoutei) 'コンボボックスハンドラ

            '検索条件解除用リストを追加
            ckoutei.addItem(New UtilCboVO("", ""))

            'ループさせてコンボボックスにセット
            For i As Integer = 0 To iRecCnt - 1
                ckoutei.addItem(New UtilCboVO(_db.rmNullStr(ds.Tables(RS).Rows(i)("KAHEN")), _db.rmNullStr(ds.Tables(RS).Rows(i)("KOUTEI"))))
            Next

            '機械略記号コンボボックス作成用SQL
            sql = ""
            sql = sql & N & " SELECT "
            sql = sql & N & " M21.KIKAIMEI KIKAIMEI, M01.SORT "
            sql = sql & N & " FROM M21SEISAN M21 "
            sql = sql & N & " LEFT OUTER JOIN (SELECT KAHENKEY,SORT FROM M01HANYO WHERE KOTEIKEY = '" & KOUTEI & "') M01 "
            sql = sql & N & " ON M01.KAHENKEY = M21.KOUTEI "
            sql = sql & N & " ORDER BY NVL(M01.SORT, " & MAXSORT & "), M21.KIKAIMEI "

            'SQL発行
            Dim ds2 As DataSet = _db.selectDB(sql, RS, iRecCnt)

            Dim ckikai As UtilComboBoxHandler = New UtilComboBoxHandler(cboKikai) 'コンボボックスハンドラ

            'ループさせてコンボボックスにセット
            ckikai.addItem(New UtilCboVO("", ""))
            For i As Integer = 0 To iRecCnt - 1
                ckikai.addItem(New UtilCboVO(_db.rmNullStr(ds2.Tables(RS).Rows(i)("KIKAIMEI")), _db.rmNullStr(ds2.Tables(RS).Rows(i)("KIKAIMEI"))))
            Next

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　生産能力設定ラベル表示
    '　(処理概要)生産能力設定ラベルを表示する
    '-------------------------------------------------------------------------------
    Private Sub dispSeisan()

        Try
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & " TO_CHAR(MAX(UPDDATE), 'YYYY/MM/DD HH24:MI:SS') " & "UPDDATE "          '更新年月
            sql = sql & N & " FROM T64MCH "

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)
            Dim seisanDate As String = _db.rmNullStr(ds.Tables(RS).Rows(0)("UPDDATE"))  '生産能力設定日時

            If String.Empty.Equals(seisanDate) Then      '抽出レコードが空の場合
                lblSeisanSettei.Text = "設定なし"
                lblSeisanDate.Text = "- - -"
                Exit Sub
            End If

            lblSeisanSettei.Text = "設定あり"
            lblSeisanDate.Text = seisanDate.Substring(0, 16)        'yyyy/mm/dd hh:mm形式で表示

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '検索処理
    '   （処理概要）　検索処理を行ない、一覧にデータを表示する。
    '------------------------------------------------------------------------------------------------------
    Private Sub dispDGV()

        Dim c As Cursor = Me.Cursor
        Me.Cursor = Cursors.WaitCursor

        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvFukaKakunin)
            Dim ckoutei As UtilComboBoxHandler = New UtilComboBoxHandler(cboKoutei)
            Dim ckikai As UtilComboBoxHandler = New UtilComboBoxHandler(cboKikai)

            '一覧の初期化
            gh.clearRow()
            dgvFukaKakunin.Enabled = False
            lblKensuu.Text = "0件"

            '検索条件の取得
            _selectedCbKoutei = _db.rmNullStr(ckoutei.getName)
            _selectedCbKikai = _db.rmNullStr(ckikai.getName)
            _selectedCbCdKouitei = _db.rmNullStr(ckoutei.getCode)
            _selectedCbCdKikai = _db.rmNullStr(ckikai.getCode)

            'SQL
            'T61をメインとして、T61、T64からデータ取得
            'ソート順についてはM01を参照(工程名 = NULLも考慮)
            Dim sql As String = ""
            Dim sqlAdd As String = ""
            sql = "SELECT "
            sql = sql & N & " T61.KOUTEI " & COLDT_KOUTEI
            sql = sql & N & " ,T61.KIKAIMEI " & COLDT_MASHINENAME
            sql = sql & N & " ,NVL(T64.MSTMCH,'') " & COLDT_SEISANNOURYOKU
            sql = sql & N & " ,T61.YAMADUMIMCH " & COLDT_MCHGOUKEI
            sql = sql & N & " ,T61.DHAKKOUMCH " & COLDT_MCHHAKKOU
            sql = sql & N & " ,T61.DMIHAKKOUMCH " & COLDT_MCHMIHAKKOU
            sql = sql & N & " ,T61.GZAIKOMCH " & COLDT_MCHGETUJIZAIKO
            sql = sql & N & " ,NVL(T64.MSTMCH,0) - T61.YAMADUMIMCH " & COLDT_OVERMCH
            sql = sql & N & " ,T61.YAMADUMIMH " & COLDT_MHGOUKEI
            sql = sql & N & " ,T61.DHAKKOUMH " & COLDT_MHHAKKOU
            sql = sql & N & " ,T61.DMIHAKKOUMH " & COLDT_MHMIHAKKOU
            sql = sql & N & " ,T61.GZAIKOMH " & COLDT_MHGETUJIZAIKO
            sql = sql & N & " FROM T61FUKA T61 "
            sql = sql & N & " LEFT OUTER JOIN T64MCH T64 ON T64.NAME = T61.KIKAIMEI "
            sql = sql & N & " LEFT OUTER JOIN (SELECT KAHENKEY,SORT FROM M01HANYO WHERE KOTEIKEY = '" & KOUTEI & "') M01 ON M01.KAHENKEY = T61.KOUTEI "
            'コンボボックスの内容が設定されている場合、WHERE句を結合
            If (String.Empty.Equals(_selectedCbKoutei) And String.Empty.Equals(_selectedCbKikai)) = False Then
                sql = sql & N & " WHERE "
                If String.Empty.Equals(_selectedCbKoutei) = False Then   '工程名コンボに選択値が存在する場合、検索条件に付加
                    sql = sql & N & " T61.KOUTEI = '" & _selectedCbCdKouitei & "'"
                End If
                If String.Empty.Equals(_selectedCbKikai) = False Then   '機械名コンボに選択値が存在する場合、検索条件に付加
                    '工程名コンボが設定されていた場合、AND句を結合
                    If String.Empty.Equals(_selectedCbKoutei) = False Then
                        sql = sql & N & " AND "
                    End If
                    sql = sql & N & " T61.KIKAIMEI = '" & _selectedCbCdKikai & "'"
                End If
            End If
            sql = sql & N & " ORDER BY NVL(M01.SORT," & MAXSORT & "), T61.KIKAIMEI "

            'SQL発行
            Dim iRecCnt As Integer                  'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then            '抽出レコードが１件もない場合
                Throw New UsrDefException("対象データがありません。", _msgHd.getMSG("NonData"))
            End If

            '抽出データを一覧に表示する
            dgvFukaKakunin.DataSource = ds
            dgvFukaKakunin.DataMember = RS

            '件数を表示
            lblKensuu.Text = CStr(iRecCnt) & "件"
            _kensuu = iRecCnt

            'ボタン制御
            If dgvFukaKakunin.RowCount <= 0 Then
                dgvFukaKakunin.Enabled = False  '一覧の使用不可
                btnExcel.Enabled = False        'エクセルボタンの使用不可
            Else
                dgvFukaKakunin.Enabled = True   '一覧の使用不可
                btnExcel.Enabled = True         'エクセルボタンの使用可
                '一覧先頭行選択
                dgvFukaKakunin.Focus()
                gh.setSelectionRowColor(0, 0, StartUp.lCOLOR_BLUE)
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

    '------------------------------------------------------------------------------------------------------
    '　EXCEL出力処理
    '   （処理概要）　最終検索結果から機械別負荷山積集計表を出力する。
    '------------------------------------------------------------------------------------------------------
    Private Sub printExcel()

        Try
            '雛形ファイル
            Dim openFilePath As String = StartUp.iniValue.BaseXlsPath & "\" & StartUp.iniValue.ExcelZG730R1_Base
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
            'ファイル名取得
            Dim wkEditFile As String = StartUp.iniValue.OutXlsPath & "\" & StartUp.iniValue.ExcelZG730R1_Out     'コピー先ファイル

            'コピー先ファイルが存在する場合、コピー先ファイルを削除
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
            Try
                'コピー先ファイル開く
                eh.open()
                Try
                    Dim startPrintRow As Integer = 9        '出力開始行数
                    Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvFukaKakunin)        'DGVハンドラの設定
                    Dim rowCnt As Integer = gh.getMaxRow    'データグリッドの最大行
                    Dim xlsi As Integer = 0                 'エクセルファイルの書き込み行
                    Dim sLine As Integer = startPrintRow    '集計開始行
                    Dim eLine As Integer = startPrintRow    '集計終了行
                    Dim syuukeiFlg As Boolean = False   '集計判定を行うフラグ
                    Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder

                    For i As Integer = 0 To rowCnt - 1

                        If i = rowCnt - 1 Then
                            '最終行の場合集計フラグON
                            syuukeiFlg = True
                        ElseIf _db.rmNullStr(dgvFukaKakunin(COLCN_KOUTEI, i).Value).Equals(_db.rmNullStr(dgvFukaKakunin(COLCN_KOUTEI, i + 1).Value)) = False Then
                            '現工程と次工程が異なる場合も集計フラグON
                            syuukeiFlg = True
                        End If

                        '一覧データ出力
                        sb.Append(_db.rmNullStr(dgvFukaKakunin(COLCN_KOUTEI, i).Value) & T)          '工程名コード
                        sb.Append(_db.rmNullStr(dgvFukaKakunin(COLCN_MASHINENAME, i).Value) & T)     '機械略記号
                        sb.Append(_db.rmNullStr(dgvFukaKakunin(COLCN_SEISANNOURYOKU, i).Value) & T)  '（MCH）生産能力
                        sb.Append(_db.rmNullStr(dgvFukaKakunin(COLCN_MCHGOUKEI, i).Value) & T)       '（MCH）山積分
                        sb.Append(_db.rmNullStr(dgvFukaKakunin(COLCN_MCHHAKKOU, i).Value) & T)       '（MCH）製作伝票発行分
                        sb.Append(_db.rmNullStr(dgvFukaKakunin(COLCN_MCHMIHAKKOU, i).Value) & T)     '（MCH）製作伝票未発行分
                        sb.Append(_db.rmNullStr(dgvFukaKakunin(COLCN_MCHGETUJIZAIKO, i).Value) & T)  '（MCH）月次在庫分
                        sb.Append(_db.rmNullStr(dgvFukaKakunin(COLCN_OVERMCH, i).Value) & T)         '（MCH）オーバー分
                        sb.Append(_db.rmNullStr(dgvFukaKakunin(COLCN_MHGOUKEI, i).Value) & T)        '（MH）山積分
                        sb.Append(_db.rmNullStr(dgvFukaKakunin(COLCN_MHHAKKOU, i).Value) & T)        '（MH）製作伝票発行分
                        sb.Append(_db.rmNullStr(dgvFukaKakunin(COLCN_MIHAKKOU, i).Value) & T)        '（MH）製作伝票未発行分
                        sb.Append(_db.rmNullStr(dgvFukaKakunin(COLCN_MHGETUJIZAIKO, i).Value) & N)   '（MH）月次在庫分

                        If syuukeiFlg = True Then
                            '集計終了行の設定
                            eLine = startPrintRow + xlsi

                            '行を1行追加
                            xlsi += 1

                            '集計行の挿入
                            sb.Append(_db.rmNullStr(dgvFukaKakunin(COLCN_KOUTEI, i).Value) & T)  '工程名コード
                            sb.Append("★計" & T)                                                '機械略記号
                            sb.Append("=if(C" & eLine & "="""","""",subtotal(9,C" & sLine & ":C" & eLine & "))" & T) '（MCH）生産能力
                            sb.Append("=subtotal(9,D" & sLine & ":D" & eLine & ")" & T)                 '（MCH）山積分
                            sb.Append("=subtotal(9,E" & sLine & ":E" & eLine & ")" & T)                 '（MCH）製作伝票発行分
                            sb.Append("=subtotal(9,F" & sLine & ":F" & eLine & ")" & T)                 '（MCH）製作伝票未発行分
                            sb.Append("=subtotal(9,G" & sLine & ":G" & eLine & ")" & T)                 '（MCH）月次在庫分
                            sb.Append("=subtotal(9,H" & sLine & ":H" & eLine & ")" & T)                 '（MCH）オーバー分
                            sb.Append("=subtotal(9,I" & sLine & ":I" & eLine & ")" & T)                 '（MH）山積分
                            sb.Append("=subtotal(9,J" & sLine & ":J" & eLine & ")" & T)                 '（MH）製作伝票発行分
                            sb.Append("=subtotal(9,K" & sLine & ":K" & eLine & ")" & T)                 '（MH）製作伝票未発行分
                            sb.Append("=subtotal(9,L" & sLine & ":L" & eLine & ")" & N)                 '（MH）月次在庫分

                            '行を1行追加
                            xlsi += 1
                            sb.Append(N)
                            syuukeiFlg = False

                            '集計開始行の設定
                            sLine = startPrintRow + xlsi + 1
                        End If

                        xlsi += 1

                    Next

                    '行を追加
                    eh.copyRow(startPrintRow)
                    eh.insertPasteRow(startPrintRow, startPrintRow + xlsi)

                    '雛形に一括貼り付け
                    Clipboard.SetText(sb.ToString)
                    eh.paste(startPrintRow, 1)

                    '余分な空行を削除
                    eh.deleteRow(startPrintRow + xlsi - 1, startPrintRow + xlsi + 1)

                    '作成日時編集
                    Dim printDate As String = Now.ToString("yyyy/MM/dd HH:mm")
                    eh.setValue("作成日時 ： " & printDate, 1, 12)   'L1:

                    '処理年月、計画年月編集
                    eh.setValue("処理年月：" & lblSyori.Text & "　　計画年月：" & lblKeikaku.Text, 1, 6)    'F1

                    'ヘッダーの検索条件編集
                    eh.setValue("工程名コード：" & _selectedCbKoutei, 3, 1) 'A3
                    eh.setValue("機械略記号：" & _selectedCbKikai, 3, 4)    'D3

                    '左上のセルにフォーカス当てる
                    eh.selectCell(9, 1)     'A7

                    'クリップボードの初期化
                    Clipboard.Clear()

                Finally
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
    Private Sub txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboKoutei.KeyPress, _
                                                                                                                cboKikai.KeyPress
        Try
            '次のコントロールへ移動する
            UtilClass.moveNextFocus(Me, e)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

#End Region

#Region "子画面から実行される関数"

    '-------------------------------------------------------------------------------
    ' 　更新データの受け取り
    '   (処理概要)子画面での登録処理を受けて検索を行う
    '　　I　：　prmRegist     　　 登録した場合True
    '-------------------------------------------------------------------------------
    Sub setRegist(ByVal prmRegist As Boolean) Implements IfRturnSeisanNouryoku.setRegist

        Try
            If prmRegist = True Then
                Call dispSeisan() '子画面で登録処理を行った場合生産能力設定ラベルを更新

                '親画面の一覧表示件数が0件より大きい場合は再検索を行う
                If _kensuu > 0 Then
                    _colorCtlFlg = False
                    Call dispDGV()     '検索処理へ
                    _colorCtlFlg = True
                    'スクロールバーの不活性対応
                    dgvFukaKakunin.VirtualMode = True
                    dgvFukaKakunin.VirtualMode = False
                End If
            End If

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
    Public Sub myShow() Implements IfRturnSeisanNouryoku.myShow
        Me.Show()
    End Sub

    '-------------------------------------------------------------------------------
    '   myActivateメソッド
    '-------------------------------------------------------------------------------
    Public Sub myActivate() Implements IfRturnSeisanNouryoku.myActivate
        Me.Activate()
    End Sub

#End Region

End Class