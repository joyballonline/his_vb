'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）生産能力マスタメンテ画面
'    （フォームID）ZM610E_SeisanMstMente
'
'===============================================================================
'　履歴     名前        日　付          マーク      内容
'-------------------------------------------------------------------------------
'　(1)      中澤        2010/09/30                  新規
'　(2)      中澤        2010/11/22                  キー変更による入力チェックの変更対応
'　(3)      菅野        2011/01/13                  (2)対応時に発生した不具合の修正
'-------------------------------------------------------------------------------
Option Explicit On
Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory

Public Class ZM610E_SeisanMstMente
    Inherits System.Windows.Forms.Form
    Implements IfRturnKahenKey

#Region "リテラル値定義"

    'PG制御文字 
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const RS As String = "RecSet"                       'レコードセットテーブル

    '一覧バインドDetaSet列名
    Private Const COLDT_SAKUJOCHK As String = "dtSakujoChk"     '削除チェックボックス
    Private Const COLDT_KOUTEI As String = "dtKoutei"           '工程名コード
    Private Const COLDT_KOUTEIBTN As String = "dtKouteiBtn"     '工程ボタン
    Private Const COLDT_KIKAINAME As String = "dtKikaiName"     '機械略記号
    Private Const COLDT_TJIKAN As String = "dtTjikan"           '通常稼動時間
    Private Const COLDT_DJIKAN As String = "dtDjikan"           '土曜稼動時間
    Private Const COLDT_NJIKAN As String = "dtNjikan"           '日曜稼動時間
    Private Const COLDT_FLG As String = "dtFlg"                 '変更フラグ
    Private Const COLDT_BKOUTEI As String = "dtBKoutei"         '変更前工程
    Private Const COLDT_BKIKAINAME As String = "dtBKikaiName"   '変更前機械名

    '一覧列名
    Private Const COLCN_SAKUJOCHK As String = "cnSakujoChk"     '削除チェックボックス
    Private Const COLCN_KOUTEI As String = "cnKoutei"           '工程名コード
    Private Const COLCN_KOUTEIBTN As String = "cnKouteiBtn"     '工程ボタン
    Private Const COLCN_KIKAINAME As String = "cnKikaiName"     '機械略記号
    Private Const COLCN_TJIKAN As String = "cnTjikan"           '通常稼動時間
    Private Const COLCN_DJIKAN As String = "cnDjikan"           '土曜稼動時間
    Private Const COLCN_NJIKAN As String = "cnNjikan"           '日曜稼動時間
    Private Const COLCN_FLG As String = "cnFlg"                 '変更フラグ
    Private Const COLCN_BKOUTEI As String = "cnBKoutei"         '変更前工程
    Private Const COLCN_BKIKAINAME As String = "cnBKikaiName"   '変更前機械名

    'グリッド列番号
    Private Const COLNO_SAKUJOCHK As Integer = 0                '削除チェックボックス
    Private Const COLNO_KOUTEI As Integer = 1                   '工程名コード
    Private Const COLNO_KOUTEIBTN As Integer = 2                '工程ボタン
    Private Const COLNO_KIKAINAME As Integer = 3                '機械略記号
    Private Const COLNO_TJIKAN As Integer = 4                   '通常稼動時間
    Private Const COLNO_DJIKAN As Integer = 5                   '土曜稼動時間
    Private Const COLNO_NJIKAN As Integer = 6                   '日曜稼動時間

    'M01汎用マスタ固定ｷｰ
    Private Const KOTEIKEY_KOUTEI As String = "12"              '工程

    '削除フラグ
    Private Const SAKUJO_FLG As String = "1"

    '変更フラグ
    Private Const HENKO_FLG As String = "1"

    'プログラムID（T91実行履歴テーブル登録用）
    Private Const PGID As String = "ZM610E"

    'EXCEL拡張子
    Private Const EXT_XLS As String = ".xls"

#End Region

#Region "メンバ変数定義"

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu

    Dim _dgv As UtilDataGridViewHandler         'グリッドハンドラー

    Private _oldRowIndex As Integer = -1            '選択行の背景色を変更するための変数
    Private _colorCtlFlg As Boolean = False         '選択行の背景色を変更するためのフラグ

    Private _formOpenFlg As Boolean                 '画面起動時であるかを判別するためのフラグ

    Private _errSet As UtilDataGridViewHandler.dgvErrSet
    Private _nyuuryokuErrFlg As Boolean = False

    Private _ZC910KahenKey As String                'ZC910S_CodeSentakuから受け取る汎用マスタ可変キー
    Private _ZC910Meisyo1 As String                 'ZC910S_CodeSentakuから受け取る汎用マスタ名称

    Private _chkCellVO As UtilDgvChkCellVO          '一覧の入力制限用

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
    Private Sub ZM610E_SeisanMstMente_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
            _formOpenFlg = True

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
        Try

            Dim dt As DataTable = CType(dgvSeisanMst.DataSource, DataSet).Tables(RS)
            Dim newRow As DataRow = dt.NewRow

            '既存DataTableの最終行にVOを挿入
            dt.Rows.InsertAt(newRow, dt.Rows.Count)

            _dgv = New UtilDataGridViewHandler(dgvSeisanMst)                       'DGVハンドラの設定
            '追加した行の変更フラグを立てる。
            '※変更フラグが立っているデータが登録対象になるため
            Dim lRowCnt As Long = dt.Rows.Count
            _dgv.setCellData(COLDT_FLG, lRowCnt - 1, HENKO_FLG)

            '追加した行の削除チェックを初期化する（Falseに設定する）
            _dgv.setCellData(COLDT_SAKUJOCHK, lRowCnt - 1, False)

            '件数の表示
            lblKensuu.Text = CStr(lRowCnt) & "件"

            'ボタンの使用可否設定（使用可）
            Call initBotton(True)

            'フォーカスの設定
            Call setForcusCol(COLNO_KOUTEI, CInt(lRowCnt - 1))

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

            '一覧のデータ数の取得
            _dgv = New UtilDataGridViewHandler(dgvSeisanMst)
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

                Call registDB()

                'トランザクション終了
                _db.commitTran()

            Finally
                'マウスカーソル矢印
                Me.Cursor = cur
            End Try

            '完了メッセージ
            _msgHd.dspMSG("completeInsert")

            _colorCtlFlg = False

            '画面再表示時の判別用フラグの初期化
            _formOpenFlg = True

            '一覧の再表示
            Call dispDGV()

            '画面再表示時の判別用フラグの設定
            _formOpenFlg = False

            _dgvChangeFlg = False                '一覧変更フラグ

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
    '　EXCELボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Try
            _dgv = New UtilDataGridViewHandler(dgvSeisanMst)

            '一覧の件数が0件なら、処理を中止する
            If _dgv.getMaxRow < 0 Then
                Throw New UsrDefException("該当データがありません。", _msgHd.getMSG("noTargetData"))
            End If

            'マウスカーソル砂時計
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor
            Try

                Call printExcel()

            Finally
                'マウスカーソル元に戻す
                Me.Cursor = cur
            End Try

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        Finally
            'マウスカーソル元に戻す
            Me.Cursor = Cursors.Arrow
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
    '   (処理概要)子画面で選択された可変キーと名称を受け取る
    '　　I　：　prmKahenKey     可変キー
    '　　I　：　prmMeisyo1      名称
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

#Region "ユーザ定義関数:EXCEL関連"

    '------------------------------------------------------------------------------------------------------
    '　EXCEL出力処理
    '------------------------------------------------------------------------------------------------------
    Private Sub printExcel()
        Try
            Dim openFilePath As String = StartUp.iniValue.BaseXlsPath & "\" & StartUp.iniValue.ExcelZM610R1_Base

            '雛形ファイルが開かれていないかチェック
            Dim fh As UtilFile = New UtilFile()
            Try
                fh.move(openFilePath, openFilePath & 1)
                fh.move(openFilePath & 1, openFilePath)
            Catch ioe As System.IO.IOException
                Throw New UsrDefException("ファイルが開かれています。ファイルを閉じてから再実行してください。", _
                                          _msgHd.getMSG("fileOpenErr", "【ファイル名】：" & openFilePath))
            End Try

            '編集用ファイルへコピー
            'ファイル名取得-----------------------------------------------------
            Dim wkEditFile As String = StartUp.iniValue.OutXlsPath & "\" & StartUp.iniValue.ExcelZM610R1_Out     'コピー先ファイル

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
                
                '雛型ファイルコピー
                FileCopy(openFilePath, wkEditFile)

            Catch ioe As System.IO.IOException
                Throw New UsrDefException(ioe, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ioe)))
            End Try

            Dim eh As xls.UtilExcelHandler = New xls.UtilExcelHandler(wkEditFile)
           
            Try
                'コピー先ファイル開く
                eh.open()
                Try
                    Dim startPrintRow As Integer = 5                    '出力開始行数
                    _dgv = New UtilDataGridViewHandler(dgvSeisanMst)
                    Dim rowCnt As Integer = _dgv.getMaxRow
                    Dim i As Integer
                    For i = 0 To rowCnt - 1

                        '列を1行追加
                        eh.copyRow(startPrintRow + i)
                        eh.insertPasteRow(startPrintRow + i)
                        '一覧データ出力
                        eh.setValue(_db.rmNullStr(dgvSeisanMst(COLCN_KOUTEI, i).Value), startPrintRow + i, 1)
                        eh.setValue(_db.rmNullStr(dgvSeisanMst(COLCN_KIKAINAME, i).Value), startPrintRow + i, 2)
                        eh.setValue(_db.rmNullStr(dgvSeisanMst(COLCN_TJIKAN, i).Value), startPrintRow + i, 3)
                        eh.setValue(_db.rmNullStr(dgvSeisanMst(COLCN_DJIKAN, i).Value), startPrintRow + i, 4)
                        eh.setValue(_db.rmNullStr(dgvSeisanMst(COLCN_NJIKAN, i).Value), startPrintRow + i, 5)

                    Next

                    '余分な空行を削除
                    eh.deleteRow(startPrintRow + i)

                    '作成日時編集
                    Dim printDate As String = Now.ToString("yyyy/MM/dd HH:mm")
                    eh.setValue("作成日時 ： " & printDate, 1, 5)
                    eh.selectCell(1, 1)

                    Clipboard.Clear()         'クリップボードの初期化

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

#End Region

#Region "ユーザ定義関数:DGV関連"

    '-------------------------------------------------------------------------------
    '   一覧　編集チェック（EditingControlShowingイベント）
    '   （処理概要）入力の制限をかける
    '-------------------------------------------------------------------------------
    Private Sub dgvkousin_EditingControlShowing(ByVal sender As Object, _
                                              ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs _
                                              ) Handles dgvSeisanMst.EditingControlShowing

        Try
            _dgv = New UtilDataGridViewHandler(dgvSeisanMst)                       'DGVハンドラの設定
            '■通常稼働時間、土曜稼働時間、日曜稼働時間の場合
            If dgvSeisanMst.CurrentCell.ColumnIndex = COLNO_TJIKAN Or _
                            dgvSeisanMst.CurrentCell.ColumnIndex = COLNO_DJIKAN Or _
                            dgvSeisanMst.CurrentCell.ColumnIndex = COLNO_NJIKAN Then

                '■グリッドに、数値入力モードの制限をかける
                _chkCellVO = _dgv.chkCell(sender, e, UtilDataGridViewHandler.chkType.Num)

            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '   サイズ別一覧　選択セル検証イベント（DataErrorイベント）
    '   （処理概要）数値入力欄に数値以外が入力された場合のエラー処理
    '-------------------------------------------------------------------------------
    Private Sub dgvSeisanMst_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvSeisanMst.DataError

        Try
            e.Cancel = False                                   '編集モード終了

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanMst)
            '■通常稼働時間、土曜稼働時間、日曜稼働時間の場合、グリッドには数値入力モード(0～9)の制限をかけているので、制限の解除
            If dgvSeisanMst.CurrentCell.ColumnIndex = COLNO_TJIKAN Or _
                            dgvSeisanMst.CurrentCell.ColumnIndex = COLNO_DJIKAN Or _
                            dgvSeisanMst.CurrentCell.ColumnIndex = COLNO_NJIKAN Then

                gh.AfterchkCell(_chkCellVO)
            End If

            '入力エラーフラグを立てる
            _nyuuryokuErrFlg = True

            Dim colName As String = ""
            Select Case e.ColumnIndex
                Case COLNO_TJIKAN
                    colName = COLDT_TJIKAN
                Case COLNO_DJIKAN
                    colName = COLDT_DJIKAN
                Case COLNO_NJIKAN
                    colName = COLDT_NJIKAN
                Case Else
                    colName = COLDT_TJIKAN
            End Select

            '文字入力されたらセルを空にする
            gh.setCellData(colname, e.RowIndex, System.DBNull.Value)

            'エラーメッセージ表示
            Throw New UsrDefException("半角数字のみ入力可能です。", _msgHd.getMSG("onlyAcceptNumeric"))

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   シートコマンドボタン押下
    '   (処理概要)一覧のボタンが押下された場合、子画面を立ち上げる
    '-------------------------------------------------------------------------------
    Private Sub dgvHinsyuMst_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSeisanMst.CellContentClick
        Try

            If e.ColumnIndex <> COLNO_KOUTEIBTN Then  '選択ボタン
                Exit Sub
            End If

            _dgv = New UtilDataGridViewHandler(dgvSeisanMst)                       'DGVハンドラの設定
            Dim kahenKey As String = _dgv.getCellData(COLDT_KOUTEI, e.RowIndex)

            'コード選択画面表示
            Dim openForm As ZC910S_CodeSentaku = New ZC910S_CodeSentaku(_msgHd, _db, Me, KOTEIKEY_KOUTEI, kahenKey)    '画面遷移
            openForm.ShowDialog(Me)                                                         '画面表示
            openForm.Dispose()

            '現在の値の保持
            Dim beforeKoutei As String = _dgv.getCellData(COLDT_KOUTEI, e.RowIndex)

            '選択した内容の表示
            If Not "".Equals(_ZC910KahenKey) Then
                _dgv.setCellData(COLDT_KOUTEI, e.RowIndex, _ZC910KahenKey)      '工程名コード
                If Not beforeKoutei.Equals(_ZC910KahenKey) Then
                    '対象行に変更フラグを立てる
                    _dgv.setCellData(COLDT_FLG, e.RowIndex, HENKO_FLG)
                End If
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '   一覧のセル値変更時
    '-------------------------------------------------------------------------------
    Private Sub dgvSeisanMst_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSeisanMst.CellEndEdit
        Try
            '画面起動時は処理を行わない
            If _formOpenFlg = True Then
                _formOpenFlg = False
                Exit Sub
            End If


            '一覧変更フラグ
            _dgvChangeFlg = True

            _dgv = New UtilDataGridViewHandler(dgvSeisanMst)                       'DGVハンドラの設定

            '数値入力モード(0～9)の制限がかかっている場合は、制限の解除
            If dgvSeisanMst.CurrentCell.ColumnIndex = COLNO_TJIKAN Or _
                            dgvSeisanMst.CurrentCell.ColumnIndex = COLNO_DJIKAN Or _
                            dgvSeisanMst.CurrentCell.ColumnIndex = COLNO_NJIKAN Then

                _dgv.AfterchkCell(_chkCellVO)
            End If

            '変更した行を取得する
            Dim RowNo As Integer = dgvSeisanMst.CurrentCell.RowIndex

            '対象行に変更フラグを立てる
            _dgv.setCellData(COLDT_FLG, RowNo, HENKO_FLG)

            'ボタンの使用可否設定（使用可）
            Call initBotton(True)

            '削除チェック
            If e.ColumnIndex = COLNO_SAKUJOCHK Then
                If IIf(("".Equals(_dgv.getCellData(COLDT_SAKUJOCHK, RowNo))), False, (_dgv.getCellData(COLDT_SAKUJOCHK, RowNo))) = True Then
                    '削除フラグを立てる
                    _dgv.setCellData(COLDT_SAKUJOCHK, RowNo, True)
                Else
                    '削除フラグを消す
                    _dgv.setCellData(COLDT_SAKUJOCHK, RowNo, False)
                End If
            End If

            '工程名コードの存在チェック
            If e.ColumnIndex = COLNO_KOUTEI Then

                If Not checkKoutei(_db.rmNullStr(dgvSeisanMst.CurrentCell.Value)) > 0 Then
                    dgvSeisanMst.CurrentCell.Value = _dgv.getCellData(COLDT_BKOUTEI, e.RowIndex)
                    'エラーが起きたセルの位置情報を渡す
                    _nyuuryokuErrFlg = True
                    _errSet = _dgv.readyForErrSet(e.RowIndex, COLCN_KOUTEI)
                    Throw New UsrDefException("登録されていない工程名コードです。", _msgHd.getMSG("NonKoutei"))
                End If
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　　グリッドフォーカス設定及
    '　　(処理概要）セル編集後にエラーになった場合に、エラーセルにフォーカスを戻す。
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvHinsyuMst_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvSeisanMst.SelectionChanged
        Try

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanMst)

            '入力エラーがあった場合
            If _nyuuryokuErrFlg Then
                _nyuuryokuErrFlg = False
                'フォーカスを入力エラーセルに移す
                gh.setCurrentCell(_errSet)
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
    Private Sub dgvSeisanMst_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvSeisanMst.CellEnter
        Try

            If _colorCtlFlg Then
                Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanMst)
                '背景色の設定
                Call setBackcolor(dgvSeisanMst.CurrentCellAddress.Y, _oldRowIndex)

            End If
            _oldRowIndex = dgvSeisanMst.CurrentCellAddress.Y

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

        Dim _dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanMst)

        '指定した行の背景色を青にする
        _dgv.setSelectionRowColor(prmRowIndex, prmOldRowIndex, StartUp.lCOLOR_BLUE)

        'ボタン列の色も変わってしまうので、戻す処理
        Call colBtnColorSilver(prmRowIndex)

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
        dgvSeisanMst.Focus()
        dgvSeisanMst.CurrentCell = dgvSeisanMst(prmColIndex, prmRowIndex)

        '背景色の設定
        Call setBackcolor(prmRowIndex, _oldRowIndex)

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　選択行のボタンの色を元に戻す処理
    '　(処理概要)ボタン列の背景色を元に戻す。
    '　　I　：　prmNewRowIndex      現在の行数
    '------------------------------------------------------------------------------------------------------
    Private Sub colBtnColorSilver(ByVal prmNewRowIdx As Integer)

        dgvSeisanMst(COLNO_KOUTEIBTN, prmNewRowIdx).Style.BackColor = SystemColors.Control

    End Sub

#End Region

#Region "ユーザ定義関数:DB関連"

    '-------------------------------------------------------------------------------
    '　一覧表示
    '-------------------------------------------------------------------------------
    Private Sub dispDGV()

        Try

            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & "  'False' " & COLDT_SAKUJOCHK
            sql = sql & N & " ,M21.KOUTEI " & COLDT_KOUTEI
            sql = sql & N & " ,'' " & COLDT_KOUTEIBTN
            sql = sql & N & " ,M21.KIKAIMEI " & COLDT_KIKAINAME
            sql = sql & N & " ,M21.TUUJOUH " & COLDT_TJIKAN
            sql = sql & N & " ,M21.DOYOUH " & COLDT_DJIKAN
            sql = sql & N & " ,M21.NITIYOUH " & COLDT_NJIKAN
            sql = sql & N & " ,'' " & COLDT_FLG
            sql = sql & N & " ,M21.KOUTEI " & COLDT_BKOUTEI
            sql = sql & N & " ,M21.KIKAIMEI " & COLDT_BKIKAINAME
            sql = sql & N & " FROM M21SEISAN M21 "
            sql = sql & N & "   INNER JOIN M01HANYO M01 ON "
            sql = sql & N & "   M21.KOUTEI = M01.KAHENKEY "
            sql = sql & N & " WHERE M01.KOTEIKEY = '" & KOTEIKEY_KOUTEI & "'"
            sql = sql & N & " ORDER BY M01.SORT, M21.KIKAIMEI "

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                Throw New UsrDefException("登録されていません。", _msgHd.getMSG("noData"))
            End If

            '抽出データを一覧に表示する
            dgvSeisanMst.DataSource = ds
            dgvSeisanMst.DataMember = RS

            lblKensuu.Text = CStr(iRecCnt) & "件"

            '抽出データがある場合、登録ボタン有効
            If iRecCnt > 0 Then
                initBotton(True)
            End If

            _colorCtlFlg = True

            '背景色の設定
            Call setBackcolor(0, 0)

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　工程名コード存在チェック
    '　(処理概要)画面に入力された工程名コードが汎用マスタに存在するかチェックする
    '　　I　：　prmKoutei       画面に入力された工程名コード
    '　　R　：  checkKoutei     SQL結果の件数（0ならエラー）
    '-------------------------------------------------------------------------------
    Private Function checkKoutei(ByVal prmKoutei As String) As Integer
        Try
            checkKoutei = 0

            Dim Sql As String = ""
            Sql = Sql & N & " SELECT KAHENKEY FROM M01HANYO "
            Sql = Sql & N & " WHERE KOTEIKEY = '" & KOTEIKEY_KOUTEI & "'"
            '-->2010/11/25 del start nakazawa
            'Sql = Sql & N & " AND KAHENKEY = '" & prmKoutei & "'"
            '<--2010/11/25 del end nakazawa
            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(Sql, RS, iRecCnt)

            checkKoutei = iRecCnt

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Function

    '-->2010/11/22 del start nakazawa
    '-------------------------------------------------------------------------------
    '　工程名コードと機械略記号の組合せチェック
    '　(処理概要)画面に入力された工程と機械名の組合せが生産能力マスタに存在するかチェックする
    '　　I　：　prmKoutei           工程
    '　　I　：　prmKikaiName        機械名
    '　　R　：　checkCombination    SQL結果の件数（0以外ならエラー）
    '-------------------------------------------------------------------------------
    'Private Function checkCombination(ByVal prmKoutei As String, ByVal prmKikaiName As String) As Integer
    '    Try

    '        checkCombination = 0

    '        Dim ds As DataSet
    '        Dim recCnt As Integer = 0

    '        Dim sql As String = ""
    '        sql = sql & N & " SELECT "
    '        sql = sql & N & " KOUTEI "
    '        sql = sql & N & " FROM M21SEISAN "
    '        sql = sql & N & " WHERE KOUTEI = '" & prmKoutei & "'"
    '        sql = sql & N & "   AND KIKAIMEI = '" & prmKikaiName & "'"
    '        ds = _db.selectDB(sql, RS, recCnt)

    '        checkCombination = recCnt

    '    Catch ue As UsrDefException         'ユーザー定義例外
    '        Call ue.dspMsg()
    '        Throw ue                        'キャッチした例外をそのままスロー
    '    Catch ex As Exception               'システム例外
    '        Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
    '    End Try

    'End Function
    '<--2010/11/22 del end nakazawa

    '------------------------------------------------------------------------------------------------------
    '　生産能力マスタの登録処理
    '　(処理概要)変更内容をDBに反映させる
    '------------------------------------------------------------------------------------------------------
    Private Sub registDB()
        Try
            _dgv = New UtilDataGridViewHandler(dgvSeisanMst)
            Dim sql As String = ""

            '処理開始時間と端末IDの取得
            Dim updStartDate As Date = Now()                           '処理開始日時
            Dim sPCName As String = UtilClass.getComputerName           '端末ID
            Dim lCntHenkoFlg As Long        '削除処理した件数（更新登録のための削除も含む）のカウント用

            '削除処理
            lCntHenkoFlg = 0
            For i As Integer = 0 To _dgv.getMaxRow - 1
                '変更フラグが立っているデータを削除する
                If HENKO_FLG.Equals(_dgv.getCellData(COLDT_FLG, i).ToString) Then

                    'SQL文発行
                    sql = ""
                    sql = "DELETE FROM M21SEISAN"
                    '' 2011/01/13 upd start sugano
                    'sql = sql & N & " WHERE KOUTEI = '" & _dgv.getCellData(COLDT_BKOUTEI, i).ToString & "'"
                    sql = sql & N & " WHERE KIKAIMEI = '" & _dgv.getCellData(COLDT_BKIKAINAME, i).ToString & "'"
                    '' 2011/01/13 upd end sugano
                    '-->2010/11/25 del start nakazawa
                    'sql = sql & N & "   AND KIKAIMEI = '" & _dgv.getCellData(COLDT_BKIKAINAME, i).ToString & "'"
                    '<--2010/11/25 del end nakazawa
                    _db.executeDB(sql)

                    '削除件数のカウントアップ
                    lCntHenkoFlg = lCntHenkoFlg + 1
                End If
            Next

            '登録処理した件数
            Dim rCntIns As Integer = 0
            '登録処理
            For i As Integer = 0 To _dgv.getMaxRow - 1
                '変更フラグが立っており、かつ削除チェックがないデータを登録する
                If HENKO_FLG.Equals(_dgv.getCellData(COLDT_FLG, i).ToString) _
                   And _dgv.getCellData(COLDT_SAKUJOCHK, i) = False Then
                    'SQL文発行
                    sql = ""
                    sql = "INSERT INTO M21SEISAN ("
                    sql = sql & N & "  KOUTEI "                                                     '工程名コード
                    sql = sql & N & ", KIKAIMEI "                                                   '機械略記号
                    sql = sql & N & ", TUUJOUH "                                                    '通常稼働時間
                    sql = sql & N & ", DOYOUH "                                                     '土曜稼働時間
                    sql = sql & N & ", NITIYOUH "                                                   '日曜稼働時間
                    sql = sql & N & ", UPDNAME "                                                    '端末ID
                    sql = sql & N & ", UPDDATE "                                                    '更新日時
                    sql = sql & N & ") VALUES ("
                    sql = sql & N & "  '" & _dgv.getCellData(COLDT_KOUTEI, i).ToString & "'"        '工程名コード
                    sql = sql & N & ", '" & _dgv.getCellData(COLDT_KIKAINAME, i).ToString & "'"     '機械略記号
                    sql = sql & N & ", '" & _dgv.getCellData(COLDT_TJIKAN, i).ToString & "'"        '通常稼働時間
                    sql = sql & N & ", '" & _dgv.getCellData(COLDT_DJIKAN, i).ToString & "'"        '土曜稼働時間
                    sql = sql & N & ", '" & _dgv.getCellData(COLDT_NJIKAN, i).ToString & "'"        '日曜稼働時間
                    sql = sql & N & ", '" & sPCName & "'"                                           '端末ID
                    sql = sql & N & ", TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "  '更新日時
                    sql = sql & N & " )"
                    _db.executeDB(sql)

                    '登録件数のカウントアップ
                    rCntIns = rCntIns + 1
                End If
            Next

            '処理終了日時を取得
            Dim updFinDate As Date
            updFinDate = Now

            '削除した件数の算出（削除チェックで削除した件数）
            lCntHenkoFlg = lCntHenkoFlg - rCntIns

            Dim cnt As Integer = 0

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
            sql = sql & N & "       " & NS(lCntHenkoFlg) & " , "
            sql = sql & N & "       " & NS(rCntIns) & " , "
            sql = sql & N & "       '" & KOTEIKEY_KOUTEI & "', "
            sql = sql & N & "   '" & sPCName & "' ,"
            sql = sql & N & "   TO_DATE('" & updFinDate & "', 'YYYY/MM/DD HH24:MI:SS')) "
            _db.executeDB(sql, cnt)

            'T02処理制御テーブル更新
            _parentForm.updateSeigyoTbl(PGID, True, updStartDate, updFinDate)

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　NULL判定
    '　(処理内容)セルの内容がNULLなら"NULL"を返す
    '　　I　：　prmStr      DBに登録するNUMBER型の値
    '　　R　：　NS          prmStrが空なら「NULL」、それ以外なら「prmStr」
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

#Region "ユーザ定義関数:チェック処理"

    '------------------------------------------------------------------------------------------------------
    '  登録チェック
    '　(処理概要)各項目の必須項目チェック、工程名コードと機械略記号の組み合わせ重複チェック
    '------------------------------------------------------------------------------------------------------
    Private Sub checkTouroku(ByRef prmMaxCnt As Long)
        Try

            _dgv = New UtilDataGridViewHandler(dgvSeisanMst)

            For i As Integer = 0 To prmMaxCnt - 1
                If _dgv.getCellData(COLDT_SAKUJOCHK, i) Then
                    '削除にチェックが入っている場合は入力チェックは行わない
                Else
                    '必須入力チェック
                    '工程名コード
                    Call chekuHissu(COLDT_KOUTEI, "工程名コード", i, COLNO_KOUTEI)
                    '機械略記号
                    Call chekuHissu(COLDT_KIKAINAME, "機械略記号", i, COLNO_KIKAINAME)
                    '通常稼働時間
                    Call chekuHissu(COLDT_TJIKAN, "通常稼動時間", i, COLNO_TJIKAN)
                    '土曜稼働時間
                    Call chekuHissu(COLDT_DJIKAN, "土曜稼動時間", i, COLNO_DJIKAN)
                    '日曜稼働時間
                    Call chekuHissu(COLDT_NJIKAN, "日曜稼動時間", i, COLNO_NJIKAN)

                    '-->2010/11/22 upd start nakazawa
                    '工程と機械名の組み合わせ重複チェック
                    '※工程または機械名が変更前と変わっている場合にチェックする。
                    'If Not _dgv.getCellData(COLDT_KOUTEI, i).Equals(_dgv.getCellData(COLDT_BKOUTEI, i)) _
                    ' Or Not _dgv.getCellData(COLDT_KIKAINAME, i).Equals(_dgv.getCellData(COLDT_BKIKAINAME, i)) Then

                    '    If checkCombination(_dgv.getCellData(COLDT_KOUTEI, i).ToString, _dgv.getCellData(COLDT_KIKAINAME, i).ToString) > 0 Then

                    '        'フォーカスをあてる
                    '        Call setForcusCol(COLNO_KOUTEI, i)
                    '        'エラーメッセージの表示
                    '        Throw New UsrDefException("キーが重複しています。", _msgHd.getMSG("NotUniqueKey", "【" & i + 1 & "行目】"))
                    '    End If
                    'End If
                    '機械略記号の重複チェック
                    If _dgv.getCellData(COLDT_KIKAINAME, i).Equals(_dgv.getCellData(COLDT_BKIKAINAME, i)) Then

                        For j As Integer = i + 1 To prmMaxCnt - 1
                            If _dgv.getCellData(COLDT_KIKAINAME, i).Equals(_dgv.getCellData(COLDT_KIKAINAME, j)) Then

                                'フォーカスをあてる
                                Call setForcusCol(COLNO_KIKAINAME, j)
                                '-->2010.12.17 chg by takagi #13
                                'Throw New UsrDefException("機械略記号が重複しています。", _
                                '        _msgHd.getMSG("NotUniqueKikaiRyaku", "【" & j + 1 & "行目】"))
                                Throw New UsrDefException("機械略記号が重複しています。", _
                                        _msgHd.getMSG("NotUniqueKikaiRyaku"))
                                '<--2010.12.17 chg by takagi #13
                            End If
                        Next
                    End If
                    '工程名コードと機械略記号の1桁目チェック
                    If Not _dgv.getCellData(COLDT_KOUTEI, i).Equals(_dgv.getCellData(COLDT_BKOUTEI, i)) _
                        Or Not _dgv.getCellData(COLDT_KIKAINAME, i).Equals(_dgv.getCellData(COLDT_BKIKAINAME, i)) Then

                        If Not _dgv.getCellData(COLDT_KOUTEI, i).Substring(0, 1).Equals(_dgv.getCellData(COLDT_KIKAINAME, i).Substring(0, 1)) Then

                            'フォーカスをあてる
                            Call setForcusCol(COLNO_KIKAINAME, i)
                            '-->2010.12.17 chg by takagi #13
                            'Throw New UsrDefException("工程名コードの1桁目と、機械略記号の1桁目が一致していません。", _
                            '        _msgHd.getMSG("notEqualKouteiKikaiRyaku", "【" & i + 1 & "行目】"))
                            Throw New UsrDefException("工程名コードの1桁目と、機械略記号の1桁目が一致していません。", _
                                    _msgHd.getMSG("notEqualKouteiKikaiRyaku"))
                            '<--2010.12.17 chg by takagi #13
                        End If
                    End If
                    '<--2010/11/22 upd end nakazawa

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
    '　　I　：　prmColName              チェックするセルの列名
    '　　I　：　prmColHeaderName        エラー時に表示する列名
    '　　I　：　prmCnt                  チェックするセルの行数
    '　　I　：　prmColNo                チェックするセルの列数
    '------------------------------------------------------------------------------------------------------
    Private Sub chekuHissu(ByVal prmColName As String, ByVal prmColHeaderName As String, ByVal prmCnt As Integer, ByVal prmColNo As Integer)

        Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSeisanMst)

        If "".Equals(gh.getCellData(prmColName, prmCnt).ToString) Then
            'フォーカスをあてる
            Call setForcusCol(prmColNo, prmCnt)
            'エラーメッセージの表示
            '-->2010.12.17 chg by takagi #13
            'Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【 '" & prmColHeaderName & "' ：" & prmCnt + 1 & "行目】"))
            Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput"))
            '<--2010.12.17 chg by takagi #13
        End If

    End Sub

#End Region

End Class

