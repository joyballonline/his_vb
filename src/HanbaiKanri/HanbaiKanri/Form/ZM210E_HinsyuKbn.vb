'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）品種区分マスタメンテ画面
'    （フォームID）ZM210E_HinsyuKbn
'
'===============================================================================
'　履歴　名前　         日　付          マーク      内容
'-------------------------------------------------------------------------------
'　(1)   小島           2010/09/22                  新規
'  (2)   中澤           2010/09/29                  子画面に渡すパラメータ追加
'  (3)   中澤           2010/11/01                  子画面で品種区分マスタを表示するロジック追加
'  (4)   中澤           2010/11/22                  品種区分子画面呼出ボタン削除、品種区分名入力可
'-------------------------------------------------------------------------------
Option Explicit On
Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView

Public Class ZM210E_HinsyuKbn
    Inherits System.Windows.Forms.Form
    Implements IfRturnKahenKey
#Region "リテラル値定義"

    'PG制御文字 
    Private Const N As String = ControlChars.NewLine                    '改行文字
    Private Const RS As String = "RecSet"                               'レコードセットテーブル

    'グリッド列番号
    Private Const COLNO_JUYOUSAKIBTN As Integer = 0
    '-->2010/11/22 del start nakazawa   
    Private Const COLNO_HINSYUKBNBTN As Integer = 1
    '<--2010/11/22 del end nakazawa   
    '-->2010/11/22 upd start nakazawa   
    'Private Const COLNO_SAKUJOCHK As Integer = 2
    'Private Const COLNO_JUYOUSAKIKBN As Integer = 3
    'Private Const COLNO_JUYOUSAKI As Integer = 4
    'Private Const COLNO_HINSYUCD As Integer = 5
    'Private Const COLNO_HINSYUKBN As Integer = 6
    Private Const COLNO_SAKUJOCHK As Integer = 1
    Private Const COLNO_JUYOUSAKIKBN As Integer = 2
    Private Const COLNO_JUYOUSAKI As Integer = 3
    Private Const COLNO_HINSYUCD As Integer = 4
    Private Const COLNO_HINSYUKBN As Integer = 5
    '<--2010/11/22 upd end nakazawa   

    'M01汎用マスタ固定ｷｰ
    Private Const KOTEIKEY_JUYOUCD As String = "01"                     '需要先コード
    
    'M02品種区分マスタ
    Private Const M02_JUYOUSAKI As String = ""
    Private Const M02_HINSYUKBN As String = ""
    Private Const M02_HINSYUKBNNM As String = ""


    '削除フラグ
    Private Const SAKUJO_FLG As String = "1"

    '変更フラグ
    Private Const HENKO_FLG As String = "1"

    'プログラムID（T91実行履歴テーブル登録用）
    Private Const PGID As String = "ZM210E"

    '一覧バインドDetaSet列名
    Private Const COLDT_SAKUJOCHK As String = "dtSakujoChk"             '削除チェック
    Private Const COLDT_JUYOUSAKI As String = "dtJuyousakiCD"           '需要先
    Private Const COLDT_JUYOUSAKINM As String = "dtJuyousaki"           '需要先名
    Private Const COLDT_HINSYUKBN As String = "dtCD"                    '品種区分
    Private Const COLDT_HINSYUKBNNM As String = "dtHinsyu"              '品種区分名
    Private Const COLDT_HENKOUFLG As String = "dtHenkouFlg"             '変更フラグ
    Private Const COLDT_HENKOMAEJUYOU As String = "dtHenkomaeJuyou"     '変更前需要先
    Private Const COLDT_HENKOMAEHINSYU As String = "dtHenkomaeHinsyu"   '変更前品種区分

    '-->2010/11/22 add start nakazawa   
    '一覧列名
    Private Const COLCN_JUYOUSAKI As String = "cnJuyousakiCD"           '需要先
    Private Const COLCN_HINSYUKBN As String = "cnCD"                    '品種区分
    '<--2010/11/22 add end nakazawa   

#End Region

#Region "メンバ変数定義"

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu

    Private _dgv As UtilDataGridViewHandler         'グリッドハンドラー

    Private _oldRowIndex As Integer = -1            '選択行の背景色を変更するための変数
    Private _colorCtlFlg As Boolean = False         '選択行の背景色を変更するためのフラグ

    Private _formOpenFlg As Boolean                 '画面起動時であるかを判別するためのフラグ

    Private _ZC910KahenKey As String                'ZC910S_CodeSentakuから受け取る汎用マスタ可変キー
    Private _ZC910Meisyo1 As String                 'ZC910S_CodeSentakuから受け取る汎用マスタ名称１

    Private _updFlg As Boolean = False              '更新可否
    Private _dgvChangeFlg As Boolean = False        '一覧変更フラグ
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
    'コンストラクタ（Privateにして、外からは呼べないようにする）
    '------------------------------------------------------------------------------------------------------
    Private Sub New()

        '画面起動時の判別用フラグを初期化
        _formOpenFlg = True

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

    End Sub

    '------------------------------------------------------------------------------------------------------
    'コンストラクタ　メニューから呼ばれる
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

    '-------------------------------------------------------------------------------
    '　処理開始イベント
    '-------------------------------------------------------------------------------
    Private Sub ZM210E_HinsyuKbn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            '描画関係の設定
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           'サイズが変更されたときに、コントロールがコントロール自体を再描画するかどうかを示す値を設定
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '描画はバッファで実行され、完了後に、結果が画面に出力されるよう設定
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  'コントロールは、画面に直接ではなく、まずバッファに描画されます。これにより、ちらつきを抑えることができます。
            Me.SetStyle(ControlStyles.UserPaint, True)              'コントロールは、オペレーティング システムによってではなく、独自に描画されるよう設定
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   'コントロールはウィンドウ メッセージ WM_ERASEBKGND を無視するように設定

            Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr 'タイトルオプション表示

            '一覧表示
            Call dispDGV()

            '画面起動時の判別用フラグの設定
            _formOpenFlg = False

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

#End Region

#Region "ボタンイベント"

    '------------------------------------------------------------------------------------------------------
    '　新規追加ボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnSinki_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSinki.Click

        Dim dt As DataTable = CType(dgvHinsyuMst.DataSource, DataSet).Tables(RS)
        Dim newRow As DataRow = dt.NewRow

        '既存DataTableの最終行にVOを挿入
        dt.Rows.InsertAt(newRow, dt.Rows.Count)

        '追加した行の変更フラグを立てる。
        '※変更フラグが立っているデータが登録対象になるため
        Dim lRowCnt As Long = dt.Rows.Count
        _dgv.setCellData(COLDT_HENKOUFLG, lRowCnt - 1, HENKO_FLG)

        '追加した行の削除チェックを初期化する（Falseに設定する）
        _dgv.setCellData(COLDT_SAKUJOCHK, lRowCnt - 1, False)

        '件数の表示
        lblKensuu.Text = CStr(lRowCnt) & "件"

        'ボタンの使用可否設定（使用可）
        Call initBotton(_updFlg)

        'フォーカスの設定
        Call setForcusCol(COLNO_JUYOUSAKIKBN, CInt(lRowCnt - 1))

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　登録ボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnTouroku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTouroku.Click
        Try
            '一覧のデータ数の取得
            _dgv = New UtilDataGridViewHandler(dgvHinsyuMst)
            Dim lMaxCnt As Long = _dgv.getMaxRow

            '登録チェック
            Try
                Call checkTouroku(lMaxCnt)
            Catch lex As UsrDefException
                lex.dspMsg()
                Exit Sub
            End Try

            '登録確認メッセージ
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmInsert")    '登録します。
            If rtn <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If

            'マウスカーソル砂時計
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor
            Try

                '処理開始時間と端末IDの取得
                Dim dStartSysdate As Date = Now()                           '処理開始日時
                Dim sPCName As String = UtilClass.getComputerName           '端末ID

                'トランザクション開始
                _db.beginTran()

                '品種区分マスタの登録処理
                Dim lCntIns As Long = 0
                Dim lCntDel As Long = 0
                Call torokuM02HinsyuKbnMst(lMaxCnt, dStartSysdate, sPCName, lCntIns, lCntDel)

                '処理終了日時の取得
                Dim dFinishSysdate As Date = Now()

                '実行履歴テーブル、処理制御テーブルの更新処理
                Call updRirekiAndSeigyo(lCntIns, lCntDel, sPCName, dStartSysdate, dFinishSysdate)

                'トランザクション終了
                _db.commitTran()

            Finally
                'マウスカーソル元に戻す
                Me.Cursor = cur
            End Try

            '完了メッセージ
            _msgHd.dspMSG("completeInsert")

            '画面起動時の判別用フラグの初期化
            _formOpenFlg = True

            '一覧の再表示
            Call dispDGV()

            '画面起動時の判別用フラグの設定
            _formOpenFlg = False

            '一覧変更フラグ
            _dgvChangeFlg = False

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        Finally
            If _db.isTransactionOpen = True Then
                _db.rollbackTran()                          'ロールバック
            End If
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　戻るボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnModoru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModoru.Click

        '警告メッセージ
        If _dgvChangeFlg Then
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

#Region "ユーザ定義関数:DGV関連"

    '-------------------------------------------------------------------------------
    '   シートコマンドボタン押下
    '-------------------------------------------------------------------------------
    Private Sub dgvHinsyuMst_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvHinsyuMst.CellContentClick
        Try
            _dgv = New UtilDataGridViewHandler(dgvHinsyuMst)        'DGVハンドラの設定

            If e.ColumnIndex <> COLNO_JUYOUSAKIBTN Then             '需要先選択ボタン
                Exit Sub
            End If

            '変更した行を取得する
            _dgv = New UtilDataGridViewHandler(dgvHinsyuMst)
            Dim RowNo As Integer = dgvHinsyuMst.CurrentCell.RowIndex

            '-->2010/11/22 del start nakazawa  品種区分ボタン削除
            'Select Case e.ColumnIndex
            'Case COLNO_JUYOUSAKIBTN     '①需要先ボタン
            '<--2010/11/22 del end nakazawa   品種区分ボタン削除

            '需要先コードの取得
            Dim sJuyosaki As String = _dgv.getCellData(COLDT_JUYOUSAKI, RowNo)

            '-->2010/09/29 upd start nakazawa
            'コード選択画面表示
            'Dim openForm As ZC910S_CodeSentaku = New ZC910S_CodeSentaku(_msgHd, _db, Me, KOTEIKEY_JUYOUCD)   '画面遷移
            Dim openForm As ZC910S_CodeSentaku = New ZC910S_CodeSentaku(_msgHd, _db, Me, KOTEIKEY_JUYOUCD, sJuyosaki)   '画面遷移
            '<--2010/09/29 upd end nakazawa
            openForm.ShowDialog(Me)                                 '画面表示
            openForm.Dispose()

            '選択した内容の表示
            If Not "".Equals(_ZC910KahenKey) Then
                '一覧の内容が変わった場合
                If Not _dgv.getCellData(COLDT_JUYOUSAKI, RowNo).Equals(_ZC910KahenKey) Then
                    _dgvChangeFlg = True        '一覧変更フラグ
                End If
                _dgv.setCellData(COLDT_JUYOUSAKI, RowNo, _ZC910KahenKey)    '需要先
                _dgv.setCellData(COLDT_JUYOUSAKINM, RowNo, _ZC910Meisyo1)   '需要先名
            End If

            '-->2010/11/22 del start nakazawa  品種区分ボタン削除
            'Case COLNO_HINSYUKBNBTN     '②品種区分ボタン
            ''-->2010/11/22 upd start nakazawa
            ''品種区分の取得
            'Dim sHinsyuKbn As String = _dgv.getCellData(COLDT_HINSYUKBN, RowNo)

            ''コード選択画面表示
            'Dim openForm As ZC910S_CodeSentaku = New ZC910S_CodeSentaku(_msgHd, _db, Me, _
            '        dgvHinsyuMst(COLNO_JUYOUSAKIKBN, e.RowIndex).Value, _
            '        dgvHinsyuMst(COLNO_HINSYUCD, e.RowIndex).Value, , True)   '画面遷移
            'openForm.ShowDialog(Me)                                 '画面表示
            'openForm.Dispose()

            ''選択した内容の表示
            'If Not "".Equals(_ZC910KahenKey) Then
            '    _dgv.setCellData(COLDT_HINSYUKBN, RowNo, _ZC910KahenKey)    '品種区分
            '    _dgv.setCellData(COLDT_HINSYUKBNNM, RowNo, _ZC910Meisyo1)   '品種区分名
            'End If
            ''<--2010/11/01 upd end nakazawa

            'Case Else
            'Exit Sub
            'End Select
            '<--2010/11/22 del end nakazawa   品種区分ボタン削除

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '   一覧のセル値変更時
    '-------------------------------------------------------------------------------
    Private Sub dgvHinsyuMst_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvHinsyuMst.CellValueChanged
        Try
            '画面起動時は処理を行わない
            If _formOpenFlg = True Then
                Exit Sub
            End If

            '一覧変更フラグ
            _dgvChangeFlg = True

            '変更した行を取得する
            _dgv = New UtilDataGridViewHandler(dgvHinsyuMst)
            Dim RowNo As Integer = dgvHinsyuMst.CurrentCell.RowIndex

            '対象行に変更フラグを立てる
            _dgv.setCellData(COLDT_HENKOUFLG, RowNo, HENKO_FLG)

            '①削除チェックの場合-----------------------------------------------------
            If e.ColumnIndex = COLNO_SAKUJOCHK Then
                If IIf((_dgv.getCellData(COLDT_SAKUJOCHK, RowNo) = ""), False, (_dgv.getCellData(COLDT_SAKUJOCHK, RowNo))) = True Then
                    '削除フラグを立てる
                    _dgv.setCellData(COLDT_SAKUJOCHK, RowNo, True)
                Else
                    '削除フラグを消す
                    _dgv.setCellData(COLDT_SAKUJOCHK, RowNo, False)
                End If
            End If

            '②需要先コードを変更した場合（需要先を表示する）-------------------------
            If e.ColumnIndex = COLNO_JUYOUSAKIKBN Then
                '需要先コードの取得
                Dim sJuyosaki As String = _dgv.getCellData(COLDT_JUYOUSAKI, RowNo)

                If Not "".Equals(sJuyosaki) Then
                    '需要先名の取得処理
                    Call getJuyousakiName(sJuyosaki, RowNo)
                Else
                    '需要先名の初期化（空欄にする）
                    _dgv.setCellData(COLDT_JUYOUSAKINM, RowNo, "")
                End If
            End If

            '③品種区分を変更した場合（品種区分名を表示する）-------------------------
            If e.ColumnIndex = COLNO_HINSYUCD Then
                '品種区分の取得
                Dim sHinsyuKbn As String = _dgv.getCellData(COLDT_HINSYUKBN, RowNo)

                If Not "".Equals(sHinsyuKbn) Then
                    '品種区分名の取得処理
                    Call getHinsyuKbnName(sHinsyuKbn, RowNo)
                Else
                    '品種区分名の初期化（空欄にする）
                    _dgv.setCellData(COLDT_HINSYUKBNNM, RowNo, "")
                End If
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '選択行に着色する処理
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvHinsyuMst_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvHinsyuMst.CellEnter

        If _colorCtlFlg Then
            '背景色の設定
            Call setBackcolor(dgvHinsyuMst.CurrentCellAddress.Y, _oldRowIndex)
        End If

        _oldRowIndex = dgvHinsyuMst.CurrentCellAddress.Y
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　背景色の設定処理
    '------------------------------------------------------------------------------------------------------
    Private Sub setBackcolor(ByVal prmRowIndex As Integer, ByVal prmOldRowIndex As Integer)

        Dim _dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHinsyuMst)

        '指定した行の背景色を青にする
        _dgv.setSelectionRowColor(prmRowIndex, prmOldRowIndex, StartUp.lCOLOR_BLUE)

        'ボタン列の色も変わってしまうので、戻す処理
        Call colBtnColorSilver(prmRowIndex)

        _oldRowIndex = prmRowIndex

    End Sub

    '------------------------------------------------------------------------------------------------------
    '選択行のボタンの色を元に戻す処理
    '------------------------------------------------------------------------------------------------------
    Private Sub colBtnColorSilver(ByVal prmNewRowIdx As Integer)

        dgvHinsyuMst(COLNO_JUYOUSAKIBTN, prmNewRowIdx).Style.BackColor = SystemColors.Control   '需要先ボタン
        '-->2010/11/22 del start nakazawa
        'dgvHinsyuMst(COLNO_HINSYUKBNBTN, prmNewRowIdx).Style.BackColor = SystemColors.Control   '品種区分ボタン
        '<--2010/11/22 del end nakazawa

    End Sub

    '------------------------------------------------------------------------------------------------------
    '   指定列へのフォーカス設定処理
    '------------------------------------------------------------------------------------------------------
    Private Sub setForcusCol(ByVal prmColIndex As Integer, ByVal prmRowIndex As Integer)

        'フォーカスをあてる
        dgvHinsyuMst.Focus()
        dgvHinsyuMst.CurrentCell = dgvHinsyuMst(prmColIndex, prmRowIndex)

        '背景色の設定
        Call setBackcolor(prmRowIndex, _oldRowIndex)

    End Sub

#End Region

#Region "ユーザ定義関数:画面制御"

    '------------------------------------------------------------------------------------------------------
    '　ボタンの使用可否設定
    '------------------------------------------------------------------------------------------------------
    Private Sub initBotton(ByVal prmEnable As Boolean)

        '登録ボタン
        btnTouroku.Enabled = prmEnable

    End Sub

    '-------------------------------------------------------------------------------
    ' 　汎用マスタ可変キーの受け取り
    '-------------------------------------------------------------------------------
    Sub setKahenKey(ByVal prmKahenKey As String, ByVal prmMeisyo1 As String) Implements IfRturnKahenKey.setKahenKey
        Try

            _ZC910KahenKey = prmKahenKey
            _ZC910Meisyo1 = prmMeisyo1

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
    Public Sub myShow() Implements IfRturnKahenKey.myShow
        Me.Show()
    End Sub

    '-------------------------------------------------------------------------------
    '   myActivateメソッド
    '-------------------------------------------------------------------------------
    Public Sub myActivate() Implements IfRturnKahenKey.myActivate
        Me.Activate()
    End Sub

#End Region

#Region "ユーザ定義関数:DB関連"

    '-------------------------------------------------------------------------------
    '　処理開始イベント
    '-------------------------------------------------------------------------------
    Private Sub dispDGV()

        Try
            '一覧・件数クリア
            _colorCtlFlg = False
            _dgv = New UtilDataGridViewHandler(dgvHinsyuMst)
            _dgv.clearRow()
            dgvHinsyuMst.Enabled = False
            lblKensuu.Text = "0件"

            'M02品種区分マスタの表示
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & "   'False' " & COLDT_SAKUJOCHK
            sql = sql & N & " , M02.JUYOUCD " & COLDT_JUYOUSAKI
            sql = sql & N & " , M01.NAME1 " & COLDT_JUYOUSAKINM
            sql = sql & N & " , M02.HINSYUKBN " & COLDT_HINSYUKBN
            sql = sql & N & " , M02.HINSYUKBNNM " & COLDT_HINSYUKBNNM
            sql = sql & N & " , NULL " & COLDT_HENKOUFLG
            sql = sql & N & " , M02.JUYOUCD " & COLDT_HENKOMAEJUYOU
            sql = sql & N & " , M02.HINSYUKBN " & COLDT_HENKOMAEHINSYU
            sql = sql & N & " FROM M02HINSYUKBN M02"
            sql = sql & N & "   LEFT JOIN M01HANYO M01 ON M02.JUYOUCD = M01.KAHENKEY"
            sql = sql & N & " WHERE M01.KOTEIKEY = '" & KOTEIKEY_JUYOUCD & "'"
            sql = sql & N & " ORDER BY JUYOUCD, HINSYUKBN "

            'SQL発行
            Dim iRecCnt As Integer                  'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                'ボタンの使用可否設定（使用不可）
                Call initBotton(False)
                'メッセージの表示
                Throw New UsrDefException("登録されていません。", _msgHd.getMSG("noData"))
            Else
                'ボタンの使用可否設定（使用可）
                Call initBotton(_updFlg)
            End If

            '抽出データを一覧に表示する
            dgvHinsyuMst.DataSource = ds
            dgvHinsyuMst.DataMember = RS

            '件数を表示する
            lblKensuu.Text = CStr(iRecCnt) & "件"

            _colorCtlFlg = True
            dgvHinsyuMst.Enabled = True

            '一覧先頭行選択
            dgvHinsyuMst.Focus()
            '背景色の設定
            Call setBackcolor(0, 0)

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　需要先名の取得処理
    '------------------------------------------------------------------------------------------------------
    Private Sub getJuyousakiName(ByVal prmJuyousaki As String, ByVal prmRowNo As Long)
        Try
            'M01汎用マスタから該当する需要先名を取得する
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & "  NAME1"
            sql = sql & N & " FROM M01HANYO"
            sql = sql & N & " WHERE KOTEIKEY = '" & KOTEIKEY_JUYOUCD & "'"
            sql = sql & N & "   AND KAHENKEY = '" & prmJuyousaki & "'"

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then
                '需要先名を空欄にする
                _dgv.setCellData(COLDT_JUYOUSAKINM, prmRowNo, "")
                'メッセージの表示
                Throw New UsrDefException("登録されていない需要先コードです。", _msgHd.getMSG("NoJuyousakiCD"), dgvHinsyuMst, COLCN_JUYOUSAKI, prmRowNo)
            Else
                '需要先名を表示する
                _dgv.setCellData(COLDT_JUYOUSAKINM, prmRowNo, _db.rmNullStr(ds.Tables(RS).Rows(0)("NAME1")))
            End If

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　品種区分名の取得処理
    '------------------------------------------------------------------------------------------------------
    Private Sub getHinsyuKbnName(ByVal prmHinsyuKbn As String, ByVal prmRowNo As Long)
        Try

            ' ''M01汎用マスタから該当する品種区分名を取得する
            ''Dim sql As String = ""
            ''sql = "SELECT "
            ''sql = sql & N & "  NAME1"
            ''sql = sql & N & " FROM M01HANYO"
            ''sql = sql & N & " WHERE KOTEIKEY = '" & KOTEIKEY_JUYOUCD & "'"
            ''sql = sql & N & "   AND KAHENKEY = '" & prmHinsyuKbn & "'"

            ' ''SQL発行
            ''Dim iRecCnt As Integer			'データセットの行数
            ''Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            ''If iRecCnt <= 0 Then
            ''	'品種区分名を空欄にする
            ''	_dgv.setCellData(COLDT_HINSYUKBNNM, prmRowNo, "")
            ''	'メッセージの表示
            ''	Throw New UsrDefException("登録されていない品種区分です。", _msgHd.getMSG("NoJuyousakiCD"))
            ''Else
            ''	'品種区分名を表示する
            ''	_dgv.setCellData(COLDT_HINSYUKBNNM, prmRowNo, _db.rmNullStr(ds.Tables(RS).Rows(0)("NAME1")))
            ''End If

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　品種区分マスタの登録処理
    '------------------------------------------------------------------------------------------------------
    Private Sub torokuM02HinsyuKbnMst(ByVal prmMaxCnt As Long, ByVal prmSysdate As Date, ByVal prmPCName As String, _
       ByRef rCntIns As Long, ByRef rCntDel As Long)
        Try
            Dim lCntHenkoFlg As Long        '削除処理した件数（更新登録のための削除も含む）のカウント用

            '削除処理
            lCntHenkoFlg = 0
            For i As Integer = 0 To prmMaxCnt - 1
                '変更フラグが立っているデータを削除する
                If HENKO_FLG.Equals(_dgv.getCellData(COLDT_HENKOUFLG, i).ToString) Then
                    'SQL文発行
                    Dim sql As String = ""
                    sql = "DELETE FROM M02HINSYUKBN"
                    sql = sql & N & " WHERE JUYOUCD = '" & _dgv.getCellData(COLDT_HENKOMAEJUYOU, i).ToString & "'"
                    sql = sql & N & "   AND HINSYUKBN = '" & _dgv.getCellData(COLDT_HENKOMAEHINSYU, i).ToString & "'"
                    _db.executeDB(sql)

                    '削除件数のカウントアップ
                    lCntHenkoFlg = lCntHenkoFlg + 1
                End If
            Next

            '登録処理
            For i As Integer = 0 To prmMaxCnt - 1
                '変更フラグが立っており、かつ削除チェックがないデータを登録する
                If HENKO_FLG.Equals(_dgv.getCellData(COLDT_HENKOUFLG, i).ToString) _
                   And _dgv.getCellData(COLDT_SAKUJOCHK, i) = False Then
                    'SQL文発行
                    Dim sql As String = ""
                    sql = "INSERT INTO M02HINSYUKBN ("
                    sql = sql & N & "  JUYOUCD"                                                     '需要先コード
                    sql = sql & N & ", HINSYUKBN"                                                   '品種区分コード
                    sql = sql & N & ", HINSYUKBNNM"                                                 '品種区分名
                    sql = sql & N & ", UPDNAME"                                                     '端末ID
                    sql = sql & N & ", UPDDATE"                                                     '更新日時
                    sql = sql & N & ") VALUES ("
                    sql = sql & N & "  '" & _dgv.getCellData(COLDT_JUYOUSAKI, i).ToString & "'"     '需要先コード
                    sql = sql & N & ", '" & _dgv.getCellData(COLDT_HINSYUKBN, i).ToString & "'"     '品種区分コード
                    sql = sql & N & ", '" & _dgv.getCellData(COLDT_HINSYUKBNNM, i).ToString & "'"   '品種区分名
                    sql = sql & N & ", '" & prmPCName & "'"                                         '端末ID
                    sql = sql & N & ", TO_DATE('" & prmSysdate & "', 'YYYY/MM/DD HH24:MI:SS') "     '更新日時
                    sql = sql & N & " )"
                    _db.executeDB(sql)

                    '登録件数のカウントアップ
                    rCntIns = rCntIns + 1
                End If
            Next

            '削除した件数の算出（削除チェックで削除した件数）
            rCntDel = lCntHenkoFlg - rCntIns

        Catch ex As Exception
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　実行履歴テーブル、処理制御テーブルの更新処理
    '　　●入力パラメータ： prmCntIns       登録件数
    '　　　　　　　　　　： prmCntDel       削除件数
    '　　　　　　　　　　： prmPCName       端末名
    '　　　　　　　　　　： prmStartDate    処理開始日時
    '　　　　　　　　　　： prmFinishDate   処理終了日時
    '　　●関数戻り値　　：なし
    '------------------------------------------------------------------------------------------------------
    Private Sub updRirekiAndSeigyo(ByVal prmCntIns As Long, ByVal prmCntDel As Long, ByVal prmPCName As String, _
       ByVal prmStartDate As Date, ByVal prmFinishDate As Date)
        Try
            '登録処理
            Dim sql As String = ""
            sql = "INSERT INTO T91RIREKI ("
            sql = sql & N & "  PGID"                                                        '機能ID
            sql = sql & N & ", SDATESTART"                                                  '処理開始日時
            sql = sql & N & ", SDATEEND"                                                    '処理終了日時
            sql = sql & N & ", KENNSU1"                                                     '件数１（削除件数）
            sql = sql & N & ", KENNSU2"                                                     '件数２（登録件数）
            sql = sql & N & ", UPDNAME"                                                     '端末ID
            sql = sql & N & ", UPDDATE"                                                     '更新日時
            sql = sql & N & ") VALUES ("
            sql = sql & N & "  '" & PGID & "'"                                              '機能ID
            sql = sql & N & ", TO_DATE('" & prmStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "   '処理開始日時
            sql = sql & N & ", TO_DATE('" & prmFinishDate & "', 'YYYY/MM/DD HH24:MI:SS') "  '処理終了日時
            sql = sql & N & ", " & prmCntDel                                                '件数１（削除件数）
            sql = sql & N & ", " & prmCntIns                                                '件数２（登録件数）
            sql = sql & N & ", '" & prmPCName & "'"                                         '端末ID
            sql = sql & N & ", TO_DATE('" & prmFinishDate & "', 'YYYY/MM/DD HH24:MI:SS') "  '更新日時
            sql = sql & N & " )"
            _db.executeDB(sql)

            'T02処理制御テーブル更新
            _parentForm.updateSeigyoTbl(PGID, True, prmStartDate, prmFinishDate)

        Catch ex As Exception
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

#End Region

#Region "ユーザ定義関数:チェック処理"

	'------------------------------------------------------------------------------------------------------
	'   登録チェック
	'------------------------------------------------------------------------------------------------------
	Private Sub checkTouroku(ByRef prmMaxCnt As Long)
        Try

            Dim reccnt As Integer = 0

            For i As Integer = 0 To prmMaxCnt - 1

                '需要先コード必須チェック
                If "".Equals(_db.rmNullStr(_dgv.getCellData(COLDT_JUYOUSAKI, i)).ToString) Then
                    'フォーカスをあてる
                    Call setForcusCol(COLNO_JUYOUSAKIKBN, i)
                    'エラーメッセージの表示
                    '-->2010.12.17 chg by takagi #13
                    'Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【需要先：" & i + 1 & "行目】"))
                    Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput"))
                    '<--2010.12.17 chg by takagi #13
                End If

                '需要先コード存在チェック
                Call getJuyousakiName(_db.rmNullStr(_dgv.getCellData(COLDT_JUYOUSAKI, i)).ToString, i)

                '品種区分必須チェック
                If "".Equals(_db.rmNullStr(_dgv.getCellData(COLDT_HINSYUKBN, i)).ToString) Then
                    'フォーカスをあてる
                    Call setForcusCol(COLNO_HINSYUCD, i)
                    'エラーメッセージの表示
                    '-->2010.12.17 chg by takagi #13
                    'Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【品種区分：" & i + 1 & "行目】"))
                    Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput"))
                    '<--2010.12.17 chg by takagi #13
                End If

                '需要先コードと品種区分の組み合わせ重複チェック
                '※需要先または品種区分が変更前と変わっている場合にチェックする。
                If Not _dgv.getCellData(COLDT_JUYOUSAKI, i).ToString.Equals(_dgv.getCellData(COLDT_HENKOMAEJUYOU, i)) _
                 Or Not _dgv.getCellData(COLDT_HINSYUKBN, i).ToString.Equals(_dgv.getCellData(COLDT_HENKOMAEHINSYU, i)) Then

                    '-->2010/11/22 del start nakazawa
                    'Dim sql As String = ""
                    'sql = sql & N & "SELECT "
                    'sql = sql & N & "   * "
                    'sql = sql & N & " FROM M02HINSYUKBN "
                    'sql = sql & N & " WHERE JUYOUCD = '" & _dgv.getCellData(COLDT_JUYOUSAKI, i).ToString & "'"
                    'sql = sql & N & "   AND HINSYUKBN = '" & _dgv.getCellData(COLDT_HINSYUKBN, i).ToString & "'"
                    '            ds = _db.selectDB(sql, RS, reccnt)

                    'If reccnt <> 0 Then
                    '    'フォーカスをあてる
                    '    Call setForcusCol(COLNO_HINSYUCD, i)
                    '    'エラーメッセージの表示
                    '    Throw New UsrDefException("入力された品種区分は登録済です。", _msgHd.getMSG("RepeatHinsyuCD", "【" & i + 1 & "行目】"))
                    'End If
                    '<--2010/11/22 del end nakazawa

                    '-->2010/11/22 add start nakazawa
                    For j As Integer = 0 To prmMaxCnt - 1
                        If Not j = i Then
                            If _dgv.getCellData(COLDT_HINSYUKBN, i).ToString.Equals(_dgv.getCellData(COLDT_HINSYUKBN, j).ToString) Then
                                If _dgv.getCellData(COLDT_JUYOUSAKI, i).ToString.Equals(_dgv.getCellData(COLDT_JUYOUSAKI, j).ToString) Then
                                    'フォーカスをあてる
                                    Call setForcusCol(COLNO_HINSYUCD, i)
                                    '-->2010.12.17 chg by takagi #13
                                    'Throw New UsrDefException("品種区分が重複してます。", _
                                    '        _msgHd.getMSG("RepeatHinsyuCD", "【" & i + 1 & "行目】"))
                                    Throw New UsrDefException("品種区分が重複してます。", _
                                            _msgHd.getMSG("RepeatHinsyuCD"))
                                    '<--2010.12.17 chg by takagi #13
                                End If
                            End If
                        End If
                    Next
                    '<--2010/11/22 add end nakazawa
                End If

                ' ''品種区分名
                ''If "".Equals(_dgv.getCellData(COLDT_HINSYUKBNNM, i).ToString) Then
                ''	'フォーカスをあてる
                ''	Call setForcusCol(COLNO_HINSYUKBN, i)
                ''	'エラーメッセージの表示
                ''	Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【品種区分名：" & i + 1 & "行目】"))
                ''End If

            Next

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

	End Sub

#End Region

End Class

