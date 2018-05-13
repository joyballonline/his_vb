'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）汎用マスタメンテ画面
'    （フォームID）ZM310E_HanyouMstMente
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   中澤        2010/09/03                 新規              
'　(2)   菅野        2011/01/13                 修正　行追加時の不具合対応              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView

Public Class ZM310E_HanyouMstMente
    Inherits System.Windows.Forms.Form

#Region "リテラル値定義"

    'PG制御文字
    Private Const RS As String = "RecSet"                       'データセットテーブル名
    Private Const RS2 As String = "RecSet2"                     'データセットテーブル名
    Private Const N As String = ControlChars.NewLine            '改行文字

    'マスタ登録項目一覧表示用の固定キー検索条件
    Private Const KOTEIKEY As String = "00"

    'マスタ登録項目一覧(上)グリッドバインド列名
    Private Const COLDT_TOP_CD As String = "dtCD"
    Private Const COLDT_TOP_KAHENCD As String = "dtKahenCD"
    Private Const COLDT_TOP_MEISYOU As String = "dtMeisyou"
    Private Const COLDT_TOP_BIKO As String = "dtBiko"
    Private Const COLDT_TOP_UPDDATE As String = "dtUpdDate"

    'マスタ詳細一覧(下)グリッドバインド列名
    Private Const COLDT_UND_CHECK As String = "dtCheck"
    Private Const COLDT_UND_KOTEIKEY As String = "dtKoteiKey"
    Private Const COLDT_UND_KAHENKEY As String = "dtKahenKey"
    Private Const COLDT_UND_NAME1 As String = "dtMeisyou1"
    Private Const COLDT_UND_NAME2 As String = "dtMeisyou2"
    Private Const COLDT_UND_NAME3 As String = "dtMeisyou3"
    Private Const COLDT_UND_NAME4 As String = "dtMeisyou4"
    Private Const COLDT_UND_NAME5 As String = "dtMeisyou5"
    Private Const COLDT_UND_NUM1 As String = "dtSuuti1"
    Private Const COLDT_UND_NUM2 As String = "dtSuuti2"
    Private Const COLDT_UND_NUM3 As String = "dtSuuti3"
    Private Const COLDT_UND_NUM4 As String = "dtSuuti4"
    Private Const COLDT_UND_NUM5 As String = "dtSuuti5"
    Private Const COLDT_UND_SORT As String = "dtHyoujijun"

    'マスタ登録項目一覧(上)グリッド列名
    Private Const COLCN_TOP_KOTEIKEY As String = "cnCD"
    Private Const COLCN_TOP_MEISYOU As String = "cnMeisyou"
    Private Const COLCN_TOP_KAHENKEY As String = "cnKahenCD"

    'マスタ詳細一覧(下)グリッドバインド列名
    Private Const COLCN_UND_KAHENKEY As String = "cnKahenKey"

    'マスタ詳細一覧(下)グリッド列数
    Private Const COLNO_UND_CHECK As Integer = 0
    Private Const COLNO_UND_KAHEN As Integer = 2
    Private Const COLNO_UND_NAME1 As Integer = 3
    Private Const COLNO_UND_NAME2 As Integer = 4
    Private Const COLNO_UND_NAME3 As Integer = 5
    Private Const COLNO_UND_NAME4 As Integer = 6
    Private Const COLNO_UND_NAME5 As Integer = 7
    Private Const COLNO_UND_NUM1 As Integer = 8
    Private Const COLNO_UND_NUM2 As Integer = 9
    Private Const COLNO_UND_NUM3 As Integer = 10
    Private Const COLNO_UND_NUM4 As Integer = 11
    Private Const COLNO_UND_NUM5 As Integer = 12
    Private Const COLNO_UND_SORT As Integer = 13

    Private Const PGID As String = "ZM310E"             'DB登録時に使用する機能ID

#End Region

#Region "メンバ変数定義"

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu

    Dim _dgv As UtilDataGridViewHandler         'グリッドハンドラー

    Private _oldRowIndexUnder As Integer = 0                'マスタ詳細一覧(下)選択行のカーソル移動前の行番号
    Private _oldColNameUnder As String = ""                 'マスタ詳細一覧(下)選択行のカーソル移動前の列名
    Private _oldRowIndexTop As Integer = -1                 'マスタ登録項目一覧(上)選択行のカーソル移動前の行番号
    Private _colorCtlFlgUnder As Boolean = False            'マスタ詳細一覧(下)選択行の背景色を変更するためのフラグを宣言
    
    Private _updateDgvUnderFlg As Boolean = False           'マスタ詳細一覧(下)セル内容編集フラグ
    Private _dispDgvUnderFirstFlg As Boolean = False        '画面起動時にマスタ詳細一覧(下)を表示させないためのフラグ

    Private _beforeCellData As String = ""                  'マスタ詳細一覧(下)の値の変更前値

    Private _chkCellVO As UtilDgvChkCellVO                  'マスタ詳細一覧(下)数値のみ入力のための変数

    Private _hankakuSuutiErrorFlg As Boolean = False        '半角数字チェックでエラーが起きた場合のフラグ
    Private _hankakuSuutiErrorRowNo As Integer              'マスタ詳細一覧(下)入力エラーを起こしたセルの行数
    Private _hankakuSuutiErrorColNo As Integer              'マスタ詳細一覧(下)入力エラーを起こしたセルの列数
    Private _kahenKeyErrorFlg As Boolean = True             '可変キーが未入力または重複した場合の行着色フラグ

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
    'コンストラクタ　frmMZ02_01Mから呼ばれる
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

#End Region

#Region "Formイベント"

    '-------------------------------------------------------------------------------
    '　処理開始イベント
    '-------------------------------------------------------------------------------
    Private Sub ZC910S_CodeSentaku_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            '描画関係の設定
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           'サイズが変更されたときに、コントロールがコントロール自体を再描画するかどうかを示す値を設定
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '描画はバッファで実行され、完了後に、結果が画面に出力されるよう設定
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  'コントロールは、画面に直接ではなく、まずバッファに描画されます。これにより、ちらつきを抑えることができます。
            Me.SetStyle(ControlStyles.UserPaint, True)              'コントロールは、オペレーティング システムによってではなく、独自に描画されるよう設定
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   'コントロールはウィンドウ メッセージ WM_ERASEBKGND を無視するように設定

            Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr 'タイトルオプション表示

            _dispDgvUnderFirstFlg = True
            'コントロール設定
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
    '　マスタ登録項目の編集ボタン押下
    '　(処理内容)マスタ登録項目の編集子画面を開く
    '------------------------------------------------------------------------------------------------------
    Private Sub btnHensyuu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHensyuu.Click
        Try

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvJuyousaki)
            Dim rowcnt As Integer = dgvJuyousaki.CurrentCellAddress.Y

            '現在カーソルがある行の可変キーを取得
            Dim kahenKey As String = gh.getCellData(COLDT_TOP_KAHENCD, rowcnt)
            '子画面表示
            Dim openForm As ZM311S_HanyouMstHensyuu = New ZM311S_HanyouMstHensyuu(_msgHd, _db, Me, kahenKey)      'パラメタを遷移先画面へ渡す
            openForm.ShowDialog(Me)                                                             '画面表示
            openForm.Dispose()

            'マスタ登録項目一覧(上)の再表示
            Call dispDgvTop()

            '備考、更新日時の取得
            Dim biko As String = ""
            Dim kousinDate As String = ""
            biko = gh.getCellData(COLDT_TOP_BIKO, rowcnt)
            kousinDate = CStr(gh.getCellData(COLDT_TOP_UPDDATE, rowcnt))

            '備考と更新日時ラベルの表示
            lblKoumokuSetumei.Text = biko

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　行追加ボタン押下
    '　(処理内容)詳細一覧(下)の最終行に空行を追加する
    '------------------------------------------------------------------------------------------------------
    Private Sub btnTuika_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTuika.Click
        Try

            Dim dt As DataTable = CType(dgvHanyouMst.DataSource, DataSet).Tables(RS2)

            Dim newRow As DataRow = dt.NewRow

            '既存DataTableの最終行にVOを挿入
            dt.Rows.InsertAt(newRow, dt.Rows.Count)

            '' 2011/01/13 add start sugano
            '固定キーの取得
            Dim rowcnt As Integer
            rowcnt = dgvJuyousaki.CurrentCellAddress.Y
            Dim gh_hd As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvJuyousaki)
            Dim koteikey As String = ""
            koteikey = gh_hd.getCellData(COLDT_TOP_KAHENCD, rowcnt)

            '追加行の非表示列に固定キーをセット
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHanyouMst)
            gh.setCellData(COLDT_UND_KOTEIKEY, dt.Rows.Count - 1, koteikey)
            '' 2011/01/13 add end sugano

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　登録ボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnTouroku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTouroku.Click
        Try

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHanyouMst)
            Dim rowcnt As Integer = gh.getMaxRow

            '入力チェック
            Call checkTourokuBtn(rowcnt)

            '登録確認ダイアログ表示
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmInsert")   '登録します。よろしいですか？
            If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            'マウスカーソル砂時計
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor
            Try

                Call registDB()

            Finally
                'マウスカーソル矢印
                Me.Cursor = cur
            End Try

            Call _msgHd.dspMSG("completeRun")        '処理が完了しました

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        Finally
            'マウスカーソル矢印
            Me.Cursor = Cursors.Arrow
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　戻るボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnModoru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModoru.Click

        '警告メッセージ
        If _updateDgvUnderFlg Then
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmDgvEdit")   '編集中の内容が破棄されます。よろしいですか？
            If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        '自画面を終了し、メニュー画面に戻る。
        _parentForm.Show()
        _parentForm.Activate()

        Me.Close()

    End Sub

#End Region

#Region "ユーザ定義関数:画面制御"

    '-------------------------------------------------------------------------------
    '　コントロール初期設定
    '-------------------------------------------------------------------------------
    Private Sub initForm()

        '初期値設定
        Call dispDgvTop()

        'ボタン使用可否設定
        btnHensyuu.Enabled = True
        btnTouroku.Enabled = True
        btnTuika.Enabled = True

    End Sub

#End Region

#Region "ユーザ定義関数:DGV関連"

    '------------------------------------------------------------------------------------------------------
    '選択行に着色する処理(マスタ登録項目一覧・上)およびラベル・詳細一覧(下)の表示、ボタンの可否設定
    '------------------------------------------------------------------------------------------------------
    Private _NonSelectionChangedBySetCurrentCell As Boolean = False
    Private _NonSelectionChangedBydispDGV As Boolean = False            '子画面から戻ってきた場合に、マスタ詳細一覧(下)に再表示させないためのフラグ
    Private Sub dgvJuyousaki_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvJuyousaki.SelectionChanged
        If _NonSelectionChangedBySetCurrentCell Or _
           _NonSelectionChangedBydispDGV Then Exit Sub

        dgvJuyousaki.Columns(COLCN_TOP_KAHENKEY).DefaultCellStyle.SelectionBackColor = StartUp.lCOLOR_BLUE
        dgvJuyousaki.Columns(COLCN_TOP_MEISYOU).DefaultCellStyle.SelectionBackColor = StartUp.lCOLOR_BLUE

        Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvJuyousaki)

        Try
            '詳細一覧の内容が変更されている場合、内容を破棄してよいか確認する。
            If _updateDgvUnderFlg Then
                '登録確認ダイアログ表示
                Dim rtn As DialogResult = _msgHd.dspMSG("confirmDgvEdit")   '編集中の内容が破棄されます。よろしいですか？
                If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                    _updateDgvUnderFlg = False
                    gh.setSelectionRowColor(_oldRowIndexTop, dgvJuyousaki.CurrentCellAddress.Y, StartUp.lCOLOR_BLUE)
                    _dispDgvUnderFirstFlg = True

                    _NonSelectionChangedBySetCurrentCell = True
                    Try
                        gh.setCurrentCell(COLCN_TOP_MEISYOU, _oldRowIndexTop)
                    Finally
                        _NonSelectionChangedBySetCurrentCell = False
                    End Try
                    _updateDgvUnderFlg = True
                    Exit Sub
                End If
            End If
            _updateDgvUnderFlg = False
            _oldRowIndexTop = dgvJuyousaki.CurrentCellAddress.Y

            '現在の行番号を取得
            Dim rowcnt As Integer
            rowcnt = dgvJuyousaki.CurrentCellAddress.Y 'e.RowIndex

            '備考、更新日時の取得
            Dim biko As String = ""
            Dim kousinDate As String = ""
            biko = gh.getCellData(COLDT_TOP_BIKO, rowcnt)
            kousinDate = CStr(gh.getCellData(COLDT_TOP_UPDDATE, rowcnt))

            '備考と更新日時ラベルの表示
            lblKoumokuSetumei.Text = biko
            lblKousinDate.Text = kousinDate

            '可変キーの取得
            Dim kahen As String = ""
            kahen = gh.getCellData(COLDT_TOP_KAHENCD, rowcnt)
            '着色フラグを倒し、着色変数をリセットする
            _colorCtlFlgUnder = False
            _oldRowIndexUnder = 0
            'マスタ詳細一覧(下)の表示
            Call dispMstMeisai(kahen)
            _colorCtlFlgUnder = True
            dgvJuyousaki.Focus()

            'ボタンの使用可否設定
            btnHensyuu.Enabled = True
            btnTouroku.Enabled = True
            btnTuika.Enabled = True

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　マスタ登録項目一覧セル編集開始時処理
    '　(処理内容)　編集前の値を取得する
    '　　　　　　　特定の列に数値入力モードの制限をかける
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvHanyouMst_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvHanyouMst.EditingControlShowing
        '編集前の値を保持
        _beforeCellData = CStr(_db.rmNullStr(dgvHanyouMst.CurrentCell.Value))

        Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHanyouMst)

        '■数値１、数値２、数値３、表示順の場合、グリッドに数値入力モードの制限をかける
        If dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_NUM1 Or _
                dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_NUM2 Or _
                dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_NUM3 Or _
                dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_NUM4 Or _
                dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_NUM5 Or _
                dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_SORT Then

            _chkCellVO = gh.chkCell(sender, e, UtilDataGridViewHandler.chkType.Num)
        End If
    End Sub

    '-------------------------------------------------------------------------------
    '   サイズ別一覧　選択セル検証イベント（DataErrorイベント）
    '   （処理概要）数値入力欄に数値以外が入力された場合のエラー処理
    '-------------------------------------------------------------------------------
    Private Sub dgvHanyouMst_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvHanyouMst.DataError

        Try
            e.Cancel = False                                   '編集モード終了

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHanyouMst)
            '■数値１、数値２、数値３、表示順の場合、グリッドには数値入力モード(0～9)の制限をかけているので、制限の解除
            If dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_NUM1 Or _
                dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_NUM2 Or _
                dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_NUM3 Or _
                dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_NUM4 Or _
                dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_NUM5 Or _
                dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_SORT Then
                gh.AfterchkCell(_chkCellVO)
            End If

            'エラーフラグを立てる
            _hankakuSuutiErrorFlg = True

            Dim colName As String = ""
            Select Case e.ColumnIndex
                Case COLNO_UND_NUM1
                    colName = COLDT_UND_NUM1
                Case COLNO_UND_NUM2
                    colName = COLDT_UND_NUM2
                Case COLNO_UND_NUM3
                    colName = COLDT_UND_NUM3
                Case COLNO_UND_NUM4
                    colName = COLDT_UND_NUM4
                Case COLNO_UND_NUM5
                    colName = COLDT_UND_NUM5
                Case Else
                    colName = COLDT_UND_NUM1
            End Select

            '文字入力されたらセルを空にする
            gh.setCellData(colName, e.RowIndex, DBNull.Value)

            Throw New UsrDefException("半角数字のみ入力可能です。", _msgHd.getMSG("onlyAcceptNumeric"))

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　選択行に着色する処理(マスタ詳細・下)
    '-------------------------------------------------------------------------------
    Private Sub dgvHanyouMst_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvHanyouMst.CellEnter

        Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHanyouMst)

        '選択行着色
        '可変キーの未入力・重複チェックでエラーが合った場合は処理を行わない
        If _colorCtlFlgUnder And _kahenKeyErrorFlg Then
            gh.setSelectionRowColor(dgvHanyouMst.CurrentCellAddress.Y, _oldRowIndexUnder, StartUp.lCOLOR_BLUE)
        Else
            _kahenKeyErrorFlg = True
            Exit Sub
        End If
        _oldRowIndexUnder = dgvHanyouMst.CurrentCellAddress.Y
    End Sub

    '------------------------------------------------------------------------------------------------------
    'マスタ登録項目一覧セル編集終了時処理
    '(処理内容)　開始時と終了時にセルの内容が変わっていた場合、変更済みフラグを立てる
    '　　　　　　可変キーが変更された場合は入力チェックを行う
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvHanyouMst_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvHanyouMst.CellEndEdit
        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHanyouMst)

            '変更後の値の取得
            Dim afterCellData As String = ""
            afterCellData = CStr(_db.rmNullStr(dgvHanyouMst.CurrentCell.Value))

            '入力エラーが合った場合は処理を行わない
            If _hankakuSuutiErrorFlg Then
                '変更された場合はフラグを立てる
                If Not _beforeCellData.Equals(afterCellData) Then
                    _updateDgvUnderFlg = True
                Else
                    _updateDgvUnderFlg = False
                End If
                Exit Sub
            End If

            '■数値１、数値２、数値３、表示順の場合、グリッドには数値入力モード(0～9)の制限をかけているので、制限の解除
            If dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_NUM1 Or _
                dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_NUM2 Or _
                dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_NUM3 Or _
                dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_NUM4 Or _
                dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_NUM5 Or _
                dgvHanyouMst.CurrentCellAddress.X = COLNO_UND_SORT Then
                gh.AfterchkCell(_chkCellVO)
            End If

            '可変キーが変更された場合は入力チェックを呼び出す
            If e.ColumnIndex = COLNO_UND_KAHEN Then
                Call checkKahenKey(afterCellData)
            End If

            '変更された場合はフラグを立てる
            If Not _beforeCellData.Equals(afterCellData) Then
                _updateDgvUnderFlg = True
            Else
                _updateDgvUnderFlg = False
            End If

            _beforeCellData = ""

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

#End Region

#Region "ユーザ定義関数:DB関連"

    '-------------------------------------------------------------------------------
    '　マスタ登録項目一覧表示(上の一覧)
    '-------------------------------------------------------------------------------
    Private Sub dispDgvTop()

        Try

            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & " KOTEIKEY " & COLDT_TOP_CD
            sql = sql & N & " ,NAME1 " & COLDT_TOP_MEISYOU
            sql = sql & N & " ,KAHENKEY " & COLDT_TOP_KAHENCD
            sql = sql & N & " ,BIKO " & COLDT_TOP_BIKO
            sql = sql & N & " ,UPDDATE " & COLDT_TOP_UPDDATE
            sql = sql & N & " FROM M01HANYO "
            sql = sql & N & " WHERE KOTEIKEY = " & KOTEIKEY
            sql = sql & N & " ORDER BY SORT "

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                Throw New UsrDefException("登録されていません。", _msgHd.getMSG("noData"))
            End If


            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(Me.dgvJuyousaki)
            Dim saveKeyType As String = ""
            '子画面から戻ってきた時に、マスタ項目一覧(上)フォーカスを元の位置に戻すため可変キーを保持
            If (dgvJuyousaki.DataSource IsNot Nothing) AndAlso (gh.getMaxRow > 0) Then
                saveKeyType = gh.getCellData(COLDT_TOP_KAHENCD, dgvJuyousaki.CurrentCellAddress.Y)
            End If

            '抽出データを一覧に表示する
            If _dispDgvUnderFirstFlg Then       '画面起動時は一覧(下)を表示する。
                _dispDgvUnderFirstFlg = False
            Else
                _NonSelectionChangedBydispDGV = True        '子画面から戻ってきた場合に、マスタ詳細一覧(下)に再表示させない
            End If
            Try
                dgvJuyousaki.DataSource = ds
                dgvJuyousaki.DataMember = RS
            Finally
                _NonSelectionChangedBydispDGV = False
            End Try

            'マスタ項目一覧(上)のフォーカスを子画面呼出前に戻す
            If Not "".Equals(saveKeyType) Then
                For i As Integer = 0 To gh.getMaxRow - 1
                    If saveKeyType.Equals(gh.getCellData(COLDT_TOP_KAHENCD, i)) Then
                        _NonSelectionChangedBydispDGV = True
                        Try
                            Call gh.setCurrentCell(COLCN_TOP_KAHENKEY, i)
                        Finally
                            _NonSelectionChangedBydispDGV = False
                        End Try
                        Exit For
                    End If
                Next
            End If

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    'マスタ詳細一覧表示(下の一覧)
    '------------------------------------------------------------------------------------------------------
    Private Sub dispMstMeisai(ByVal prmKahenKey As String)
        Try

            'マスタ詳細を一覧表示
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & " KOTEIKEY " & COLDT_UND_KOTEIKEY
            sql = sql & N & " ,KAHENKEY " & COLDT_UND_KAHENKEY
            sql = sql & N & " ,NAME1 " & COLDT_UND_NAME1
            sql = sql & N & " ,NAME2 " & COLDT_UND_NAME2
            sql = sql & N & " ,NAME3 " & COLDT_UND_NAME3
            sql = sql & N & " ,NAME4 " & COLDT_UND_NAME4
            sql = sql & N & " ,NAME5 " & COLDT_UND_NAME5
            sql = sql & N & " ,NUM1 " & COLDT_UND_NUM1
            sql = sql & N & " ,NUM2 " & COLDT_UND_NUM2
            sql = sql & N & " ,NUM3 " & COLDT_UND_NUM3
            sql = sql & N & " ,NUM4 " & COLDT_UND_NUM4
            sql = sql & N & " ,NUM5 " & COLDT_UND_NUM5
            sql = sql & N & " ,SORT " & COLDT_UND_SORT
            sql = sql & N & " FROM M01HANYO"
            sql = sql & N & " WHERE KOTEIKEY = '" & _db.rmSQ(prmKahenKey) & "'"
            sql = sql & N & " ORDER BY SORT "

            'SQL発行
            Dim recCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS2, recCnt)

            If recCnt <= 0 Then                    '抽出レコードが１件もない場合
                Throw New UsrDefException("登録されていません。", _msgHd.getMSG("noData"))
            End If

            '抽出データを一覧に表示する
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(Me.dgvHanyouMst)
            gh.clearRow()
            dgvHanyouMst.DataSource = ds
            dgvHanyouMst.DataMember = RS2

            '件数表示
            lblKensuu.Text = CStr(recCnt) & "件"

            'マスタ詳細一覧(下)の先頭行を着色
            gh.setSelectionRowColor(0, 0, StartUp.lCOLOR_BLUE)

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　DB登録
    '　(処理内容)登録ボタン押下時にDB登録を行う
    '------------------------------------------------------------------------------------------------------
    Private Sub registDB()
        Try
            Dim updStartDate As Date = Now      '処理開始日時

            Dim sql As String = ""
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(Me.dgvHanyouMst)

            _db.beginTran()
            Dim koteiKey As String = gh.getCellData(COLDT_UND_KOTEIKEY, 0)

            Dim cnt As Integer = 0

            sql = " DELETE FROM "
            sql = sql & N & " M01HANYO "
            sql = sql & N & " WHERE KOTEIKEY = '" & koteiKey & "'"
            _db.executeDB(sql, cnt)


            Dim machineId As String = SystemInformation.ComputerName    '端末Id
            Dim updDate As Date = Now                                   '更新日時および処理終了日時
            Dim insertCount As Integer = 0                              'インサート件数

            '汎用マスタ更新
            Dim rowCnt As Integer = gh.getMaxRow
            For i As Integer = 0 To rowCnt - 1
                Dim checkBoxValue As Object = dgvHanyouMst(COLNO_UND_CHECK, i).Value
                If Not checkBoxValue Then
                    sql = ""
                    sql = "INSERT INTO M01HANYO ("
                    sql = sql & N & " KOTEIKEY, "
                    sql = sql & N & " KAHENKEY, "
                    sql = sql & N & " NAME1, "
                    sql = sql & N & " NAME2, "
                    sql = sql & N & " NAME3, "
                    sql = sql & N & " NAME4, "
                    sql = sql & N & " NAME5, "
                    sql = sql & N & " NUM1, "
                    sql = sql & N & " NUM2, "
                    sql = sql & N & " NUM3, "
                    sql = sql & N & " NUM4, "
                    sql = sql & N & " NUM5, "
                    sql = sql & N & " SORT, "
                    sql = sql & N & " BIKO, "
                    sql = sql & N & " UPDNAME, "
                    sql = sql & N & " UPDDATE) "
                    sql = sql & N & "   VALUES ("
                    sql = sql & N & "       '" & _db.rmNullStr(gh.getCellData(COLDT_UND_KOTEIKEY, i)) & "', "
                    sql = sql & N & "       '" & _db.rmNullStr(_db.rmSQ(gh.getCellData(COLDT_UND_KAHENKEY, i))) & "', "
                    sql = sql & N & "       '" & _db.rmNullStr(_db.rmSQ(gh.getCellData(COLDT_UND_NAME1, i))) & "', "
                    sql = sql & N & "       '" & _db.rmNullStr(_db.rmSQ(gh.getCellData(COLDT_UND_NAME2, i))) & "', "
                    sql = sql & N & "       '" & _db.rmNullStr(_db.rmSQ(gh.getCellData(COLDT_UND_NAME3, i))) & "', "
                    sql = sql & N & "       '" & _db.rmNullStr(_db.rmSQ(gh.getCellData(COLDT_UND_NAME4, i))) & "', "
                    sql = sql & N & "       '" & _db.rmNullStr(_db.rmSQ(gh.getCellData(COLDT_UND_NAME5, i))) & "', "
                    sql = sql & N & "       " & NS(gh.getCellData(COLDT_UND_NUM1, i)) & " , "
                    sql = sql & N & "       " & NS(gh.getCellData(COLDT_UND_NUM2, i)) & " , "
                    sql = sql & N & "       " & NS(gh.getCellData(COLDT_UND_NUM3, i)) & " , "
                    sql = sql & N & "       " & NS(gh.getCellData(COLDT_UND_NUM4, i)) & " , "
                    sql = sql & N & "       " & NS(gh.getCellData(COLDT_UND_NUM5, i)) & " , "
                    sql = sql & N & "       " & NS(gh.getCellData(COLDT_UND_SORT, i)) & " , "
                    sql = sql & N & "       '', "
                    sql = sql & N & "       '" & machineId & "', "
                    sql = sql & N & "       TO_DATE('" & updDate & "', 'YYYY/MM/DD HH24:MI:SS')) "

                    _db.executeDB(sql, cnt)
                    insertCount = insertCount + 1
                End If
            Next

            Dim deleteCount As Integer = rowCnt - insertCount     'DELETE件数

            '更新終了日時を取得
            Dim updFinDate As Date = Now

            '計画日時取得
            Dim keikakuDate As String = lblKousinDate.Text.Substring(0, 4) & lblKousinDate.Text.Substring(5, 2)

            '実行履歴テーブル更新
            sql = ""
            sql = sql & N & " INSERT INTO T91RIREKI ("
            sql = sql & N & " PGID, "
            sql = sql & N & " SDATESTART, "
            sql = sql & N & " SDATEEND, "
            sql = sql & N & " KENNSU1, "
            sql = sql & N & " KENNSU2, "
            sql = sql & N & " NAME1, "
            sql = sql & N & " UPDNAME, "
            sql = sql & N & " UPDDATE) "
            sql = sql & N & " VALUEs ("
            sql = sql & N & "   '" & PGID & "', "
            sql = sql & N & "       TO_DATE('" & updStartDate & "',  'YYYY/MM/DD HH24:MI:SS'), "
            sql = sql & N & "       TO_DATE('" & updFinDate & "',  'YYYY/MM/DD HH24:MI:SS'), "
            sql = sql & N & "       " & NS(deleteCount) & " , "
            sql = sql & N & "       " & NS(insertCount) & " , "
            sql = sql & N & "       '" & _db.rmNullStr(gh.getCellData(COLDT_UND_KOTEIKEY, 0)) & "', "
            sql = sql & N & "   '" & machineId & "' ,"
            sql = sql & N & "   TO_DATE('" & updFinDate & "', 'YYYY/MM/DD HH24:MI:SS')) "
            _db.executeDB(sql, cnt)

            'T02処理制御テーブル更新
            _parentForm.updateSeigyoTbl(PGID, True, updStartDate, updFinDate)

            _db.commitTran()

            _updateDgvUnderFlg = False
            lblKousinDate.Text = updDate.ToString("yyyy/MM/dd HH:mm:ss")

            _colorCtlFlgUnder = False
            Call dispMstMeisai(gh.getCellData(COLDT_UND_KOTEIKEY, 0))
            _colorCtlFlgUnder = True

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        Finally
            If _db.isTransactionOpen Then
                Call _db.rollbackTran()
            End If
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　NULL判定
    '　(処理内容)セルの内容がNULLなら"NULL"を返す
    '------------------------------------------------------------------------------------------------------
    Private Function NS(ByVal prmStr As String) As String
        Dim ret As String = ""
        If "".Equals(prmStr) Then
            ret = "NULL"
        Else
            ret = prmStr
        End If
        Return ret
    End Function
#End Region

#Region "ユーザ定義関数:入力チェック"
    '------------------------------------------------------------------------------------------------------
    '　登録ボタン押下時入力チェック
    '　(処理内容)数値、表示順の入力チェックを行う
    '------------------------------------------------------------------------------------------------------
    Private Sub checkTourokuBtn(ByVal prmRowCnt As Integer)
        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHanyouMst)

            '可変キーの必須チェック
            For rowCntForHissu As Integer = 0 To prmRowCnt - 1
                If "".Equals(_db.rmNullStr(gh.getCellData(COLDT_UND_KAHENKEY, rowCntForHissu))) Then
                    Dim checkBoxValue As Object = dgvHanyouMst(COLNO_UND_CHECK, rowCntForHissu).Value
                    If Not checkBoxValue Then
                        '現在の行の着色を戻し、エラーのあった行を着色
                        dgvHanyouMst.Rows(dgvHanyouMst.CurrentCellAddress.Y).DefaultCellStyle.BackColor = StartUp.lCOLOR_WHITE
                        dgvHanyouMst.Rows(rowCntForHissu).DefaultCellStyle.BackColor = StartUp.lCOLOR_BLUE
                        _oldRowIndexUnder = rowCntForHissu
                        _kahenKeyErrorFlg = False

                        'メッセージを表示し、エラーセルにフォーカスする
                        Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput"), dgvHanyouMst, COLCN_UND_KAHENKEY, rowCntForHissu)
                    End If
                End If
            Next

            '可変キーの重複チェック
            For rowCntForTyofuku As Integer = 0 To prmRowCnt - 1

                Dim checkedKahenKey As String = gh.getCellData(COLDT_UND_KAHENKEY, rowCntForTyofuku)    'チェックする可変キー
                If Not "".Equals(checkedKahenKey) Then
                    Dim tyofukuErrorFlg As Boolean = False         '可変キー重複判定フラグ
                    Dim errorRowNo As Integer = 0

                    For i As Integer = 0 To prmRowCnt - 1
                        Dim loopKahenKey As String = gh.getCellData(COLDT_UND_KAHENKEY, i)                  '全ての可変キー
                        If checkedKahenKey.Equals(loopKahenKey) Then
                            '重複が2回あった場合エラー
                            If tyofukuErrorFlg = True Then

                                '現在の行の着色を戻し、エラーのあった行を着色
                                dgvHanyouMst.Rows(dgvHanyouMst.CurrentCellAddress.Y).DefaultCellStyle.BackColor = StartUp.lCOLOR_WHITE
                                dgvHanyouMst.Rows(errorRowNo).DefaultCellStyle.BackColor = StartUp.lCOLOR_BLUE
                                _oldRowIndexUnder = errorRowNo
                                _kahenKeyErrorFlg = False

                                'メッセージを表示し、エラーセルにフォーカスする
                                Throw New UsrDefException("可変キーが重複しています。", _msgHd.getMSG("NotUniqueKahenKey"), dgvHanyouMst, COLCN_UND_KAHENKEY, errorRowNo)
                            Else
                                '重複があった行番号を保持し、エラーフラグを立てる
                                errorRowNo = i
                                tyofukuErrorFlg = True
                            End If
                        End If
                    Next

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
    '　半角数値チェック
    '　(処理内容)一覧の入力内容の半角数値チェック
    '------------------------------------------------------------------------------------------------------
    Private Sub checkHankakuEisu(ByVal prmRowCnt As Integer, ByVal prmColDataName As String, ByVal prmGh As UtilDataGridViewHandler)
        Try
            For i As Integer = 0 To prmRowCnt
                Dim checkCellData As String = ""
                checkCellData = prmGh.getCellData(prmColDataName, i)
                '半角チェック
                If UtilClass.isOnlyNStr(checkCellData) = False Then
                    Throw New UsrDefException("半角数字のみ入力可能です。", _msgHd.getMSG("onlyAcceptNumeric"))
                End If
                '数値チェック
                If Not IsNumeric(checkCellData) Then
                    Throw New UsrDefException("半角数字のみ入力可能です。", _msgHd.getMSG("onlyAcceptNumeric"))
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
    '　可変キー入力チェック
    '　(処理内容)可変キーの半角チェック、重複チェックを行う
    '------------------------------------------------------------------------------------------------------
    Private Sub checkKahenKey(ByVal prmAfterCellData As String)
        Try

            '半角チェック
            If UtilClass.isOnlyNStr(prmAfterCellData) = False Then
                Throw New UsrDefException("半角英数のみ入力可能です。", _msgHd.getMSG("onlyAcceptHankakuEisu"))
            End If

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

#End Region

    Private Sub dgvJuyousaki_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvJuyousaki.CellContentClick

    End Sub
End Class