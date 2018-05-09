'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）品種別販売計画入力
'    （フォームID）ZG310E_Hinsyubetu
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   中澤        2010/09/02                 新規              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory
Public Class ZG310E_Hinsyubetu
    Inherits System.Windows.Forms.Form

#Region "リテラル値定義"
    '-------------------------------------------------------------------------------
    '   定数定義
    '-------------------------------------------------------------------------------

    'PG制御文字 
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const RS As String = "RecSet"                       'レコードセットテーブル

    Private Const PGID As String = "ZG310E"                     'T91に登録するPGID

    '一覧データバインド列名
    Private Const COLDT_JUYOCD As String = "dtJuyousakiCD"
    Private Const COLDT_JUYONM As String = "dtJuyousaki"
    Private Const COLDT_HINKBN As String = "dtHinsyuKbn"
    Private Const COLDT_HINKBNNM As String = "dtHinsyuKbnNm"
    Private Const COLDT_THANBAI As String = "dtTougetu"
    Private Const COLDT_YHANBAI As String = "dtYokugetu"
    Private Const COLDT_YYHANBAI As String = "dtYyokugetu"
    Private Const COLDT_JUYOSORT As String = "dtJuyoSort"
    Private Const COLDT_UPDNM As String = "dtUpdNm"

    '一覧グリッド列名
    Private Const COLCN_JUYOCD As String = "cnJuyousakiCD"
    Private Const COLCN_JUYONM As String = "cnJuyousaki"
    Private Const COLCN_HINKBN As String = "cnHinsyuKbn"
    Private Const COLCN_HINKBNNM As String = "cnHinsyuKbnNm"
    Private Const COLCN_THANBAI As String = "cnTougetu"
    Private Const COLCN_YHANBAI As String = "cnYokugetu"
    Private Const COLCN_YYHANBAI As String = "cnYyokugetu"
    Private Const COLCN_JUYOSORT As String = "cnJuyoSort"
    Private Const COLCN_UPDNM As String = "cnUpdNm"

    '一覧列番号
    Private Const COLNO_JUYOCD As Integer = 0
    Private Const COLNO_JUYONM As Integer = 1
    Private Const COLNO_HINKBN As Integer = 2
    Private Const COLNO_HINKBNNM As Integer = 3
    Private Const COLNO_THANBAI As Integer = 4
    Private Const COLNO_YHANBAI As Integer = 5
    Private Const COLNO_YYHANBAI As Integer = 6

    'ラベル押下時並べ替え用リテラル
    Private Const LBL_JUYO As String = "需要先"
    Private Const LBL_HINSYU As String = "品種区分"
    Private Const LBL_SYOJUN As String = "▼"
    Private Const LBL_KOJUN As String = "▲"

    'EXCEL
    Private Const START_PRINT As Integer = 7        'EXCEL出力開始行数

    'EXCEL列番号
    Private Const XLSCOL_JUYOCD As Integer = 1      '需要コード
    Private Const XLSCOL_JUYONM As Integer = 2      '需要名
    Private Const XLSCOL_HINKBN As Integer = 3      '品種区分   
    Private Const XLSCOL_HINKBNNM As Integer = 4    '品種区分名
    Private Const XLSCOL_THANBAI As Integer = 5     '当月販売計画
    Private Const XLSCOL_YHANBAI As Integer = 6     '翌月販売計画
    Private Const XLSCOL_YYHANBAI As Integer = 7    '翌々月販売計画

#End Region

#Region "メンバー変数宣言"

    '-------------------------------------------------------------------------------
    '   変数定義
    '-------------------------------------------------------------------------------

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu

    Private _oldRowIndex As Integer = -1                    '選択行の背景色を変更するための変数
    Private _colorCtlFlg As Boolean = False                 '選択行の背景色を変更するためのフラグ

    Private _tanmatuID As String = ""                       '端末ID

    Private _chkCellVO As UtilDgvChkCellVO                  '一覧の入力制限用

    Private _errSet As UtilDataGridViewHandler.dgvErrSet    'エラー発生時にフォーカスするセル位置
    Private _nyuuryokuErrFlg As Boolean = False             '入力エラー有無フラグ

    Private _updFlg As Boolean = False                      '更新可否

    Private _formOpenFlg As Boolean = True                  '画面起動時フラグ
    Private _dgvChangeFlg As Boolean = False                '一覧変更フラグ

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
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _parentForm = prmForm                                               '親フォーム
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        _updFlg = prmUpdFlg                                                 '更新可否

    End Sub
#End Region

#Region "Formイベント"

    '-------------------------------------------------------------------------------
    '　処理開始イベント
    '-------------------------------------------------------------------------------
    Private Sub ZG310E_Hinsyubetu_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

            '画面起動時フラグ有効
            _formOpenFlg = True

            '画面表示
            Call initForm()

            '画面起動時フラグ無効
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
    '　登録ボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnTouroku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTouroku.Click
        Try

            '必須入力チェック
            Call checkTouroku()

            '登録確認メッセージ
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmInsert")    '登録します。
            If rtn <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If

            'マウスカーソル砂時計
            Me.Cursor = Cursors.WaitCursor

            '一覧のデータをワークテーブルに更新
            Call updateWK01()

            'トランザクション開始
            _db.beginTran()

            'T11へ登録
            Call registT11()

            'トランザクション終了
            _db.commitTran()

            'マウスカーソル矢印
            Me.Cursor = Cursors.Arrow

            '完了メッセージ
            _msgHd.dspMSG("completeInsert")

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
            'マウスカーソル矢印
            Me.Cursor = Cursors.Arrow
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　EXCELボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHinsyu)

            '一覧の件数が0件なら、処理を中止する
            If gh.getMaxRow < 0 Then
                Throw New UsrDefException("該当データがありません。", _msgHd.getMSG("noTargetData"))
            End If

            'マウスカーソル砂時計
            Me.Cursor = Cursors.WaitCursor

            'EXCEL出力
            Call printExcel()

            'マウスカーソル元に戻す
            Me.Cursor = Cursors.Arrow

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

    '-------------------------------------------------------------------------------
    '　画面起動時
    '-------------------------------------------------------------------------------
    Private Sub initForm()
        Try
            '端末IDの取得
            _tanmatuID = UtilClass.getComputerName

            '一覧表示
            Call dispdgv()

            '一覧行着色フラグを有効にする
            _colorCtlFlg = True

            '処理年月、計画年月表示
            Call dispDate()

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　並べ替えラベル押下時
    '　(処理概要)一覧を並び替える
    '-------------------------------------------------------------------------------
    Private Sub lbl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblJuyo.Click, _
                                                                                            lblHinsyu.Click
        Try

            '一覧行着色フラグを無効にする
            _colorCtlFlg = False

            '一覧のデータをワークテーブルに更新
            Call updateWK01()

            '一覧ヘッダーラベル編集
            If sender.Equals(lblJuyo) Then
                '需要先
                If (LBL_JUYO & N & LBL_SYOJUN).Equals(lblJuyo.Text) Then
                    '既に昇順で並べ替えられている場合
                    dispWK01("JUYOSORT DESC")
                    lblJuyo.Text = LBL_JUYO & N & LBL_KOJUN
                Else
                    '初回押下時または既に降順で並べ替えられている場合
                    dispWK01("JUYOSORT")
                    lblJuyo.Text = LBL_JUYO & N & LBL_SYOJUN
                End If
                '品種区分のラベルを元に戻す
                lblHinsyu.Text = LBL_HINSYU
            Else
                '品種区分
                If (LBL_HINSYU & N & LBL_SYOJUN).Equals(lblHinsyu.Text) Then
                    '既に昇順で並べ替えられている場合
                    dispWK01("HINSYUKBN DESC")
                    lblHinsyu.Text = LBL_HINSYU & N & LBL_KOJUN
                Else
                    '初回押下時または既に降順で並べ替えられている場合
                    dispWK01("HINSYUKBN")
                    lblHinsyu.Text = LBL_HINSYU & N & LBL_SYOJUN
                End If
                '需要先のラベルを元に戻す
                lblJuyo.Text = LBL_JUYO
            End If

            '背景色の設定
            Call setBackcolor(0, 0)

            '一覧行着色フラグを有効にする
            _colorCtlFlg = True

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

#End Region

#Region "ユーザ定義関数:EXCEL関連"

    '------------------------------------------------------------------------------------------------------
    '　EXCEL出力処理
    '------------------------------------------------------------------------------------------------------
    Private Sub printExcel()
        Try

            '雛形ファイル
            Dim openFilePath As String = StartUp.iniValue.BaseXlsPath & "\" & StartUp.iniValue.ExcelZG310R1_Base
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
            Dim wkEditFile As String = StartUp.iniValue.OutXlsPath & "\" & StartUp.iniValue.ExcelZG310R1_Out     'コピー先ファイル

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
            Try
                'コピー先ファイル開く
                eh.open()
                Try
                    Dim startPrintRow As Integer = START_PRINT          '出力開始行数
                    Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHinsyu)        'DGVハンドラの設定
                    Dim rowCnt As Integer = gh.getMaxRow
                    Dim i As Integer
                    For i = 0 To rowCnt - 1

                        '列を1行追加
                        eh.copyRow(startPrintRow + i)
                        eh.insertPasteRow(startPrintRow + i)
                        '一覧データ出力
                        eh.setValue(_db.rmNullStr(dgvHinsyu(COLCN_JUYOCD, i).Value), startPrintRow + i, XLSCOL_JUYOCD)
                        eh.setValue(_db.rmNullStr(dgvHinsyu(COLCN_JUYONM, i).Value), startPrintRow + i, XLSCOL_JUYONM)
                        eh.setValue(_db.rmNullStr(dgvHinsyu(COLCN_HINKBN, i).Value), startPrintRow + i, XLSCOL_HINKBN)
                        eh.setValue(_db.rmNullStr(dgvHinsyu(COLCN_HINKBNNM, i).Value), startPrintRow + i, XLSCOL_HINKBNNM)
                        eh.setValue(_db.rmNullStr(dgvHinsyu(COLCN_THANBAI, i).Value), startPrintRow + i, XLSCOL_THANBAI)
                        eh.setValue(_db.rmNullStr(dgvHinsyu(COLCN_YHANBAI, i).Value), startPrintRow + i, XLSCOL_YHANBAI)
                        eh.setValue(_db.rmNullStr(dgvHinsyu(COLCN_YYHANBAI, i).Value), startPrintRow + i, XLSCOL_YYHANBAI)

                    Next

                    '余分な空行を削除
                    eh.deleteRow(startPrintRow + i)

                    '作成日時編集
                    Dim printDate As String = Now.ToString("yyyy/MM/dd HH:mm")
                    eh.setValue("作成日時 ： " & printDate, 1, 7)   'G1

                    '処理年月、計画年月編集
                    eh.setValue("処理年月：" & lblSyori.Text & "　　計画年月：" & lblKeikaku.Text, 1, 4)    'D1

                    '件数編集
                    eh.setValue(rowCnt & "件", 3, 7)    'G3

                    'ヘッダーの年月編集
                    eh.setValue(lblTHanbai.Text, 6, 5)  'E6
                    eh.setValue(lblYHanbai.Text, 6, 6)  'F6
                    eh.setValue(lblYYHanbai.Text, 6, 7) 'G6

                    '左上のセルにフォーカス当てる
                    eh.selectCell(7, 1)     'G1

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
    '   一覧のセル値変更時
    '   （処理概要）変更があった行の変更フラグを立てる
    '-------------------------------------------------------------------------------
    Private Sub dgvHinsyu_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvHinsyu.CellValueChanged
        Try

            '画面起動時は処理を行わない
            If _formOpenFlg Then
                Exit Sub
            End If

            '一覧変更フラグ
            _dgvChangeFlg = True

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
    Private Sub dgvHinsyu_EditingControlShowing(ByVal sender As Object, _
                                              ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs _
                                              ) Handles dgvHinsyu.EditingControlShowing

        Try
            Dim _dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHinsyu)        'DGVハンドラの設定
            '■当月販売計画、翌月販売計画、翌々月販売計画の場合
            If dgvHinsyu.CurrentCell.ColumnIndex = COLNO_THANBAI Or _
                            dgvHinsyu.CurrentCell.ColumnIndex = COLNO_YHANBAI Or _
                            dgvHinsyu.CurrentCell.ColumnIndex = COLNO_YYHANBAI Then

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
    Private Sub dgvHinsyu_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvHinsyu.DataError

        Try
            e.Cancel = False                                   '編集モード終了

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHinsyu)
            '■当月販売計画、翌月販売計画、翌々月販売計画の場合、グリッドには数値入力モード(0～9)の制限をかけているので、制限の解除
            If dgvHinsyu.CurrentCell.ColumnIndex = COLNO_THANBAI Or _
                            dgvHinsyu.CurrentCell.ColumnIndex = COLNO_YHANBAI Or _
                            dgvHinsyu.CurrentCell.ColumnIndex = COLNO_YYHANBAI Then

                gh.AfterchkCell(_chkCellVO)
            End If

            '入力エラーフラグを立てる
            _nyuuryokuErrFlg = True

            Dim colName As String = ""
            Select Case e.ColumnIndex
                Case COLNO_THANBAI
                    colName = COLDT_THANBAI
                Case COLNO_YHANBAI
                    colName = COLDT_YHANBAI
                Case COLNO_YYHANBAI
                    colName = COLDT_YYHANBAI
                Case Else
                    colName = COLDT_THANBAI
            End Select

            '文字入力されたらセルを空にする
            gh.setCellData(colName, e.RowIndex, System.DBNull.Value)

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
    Private Sub dgvHinsyu_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvHinsyu.SelectionChanged
        Try

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHinsyu)

            '入力エラーがあった場合
            If _nyuuryokuErrFlg Then
                _nyuuryokuErrFlg = False
                gh.setCurrentCell(_errSet)
            End If

            If _colorCtlFlg Then
                '背景色の設定
                Call setBackcolor(dgvHinsyu.CurrentCellAddress.Y, _oldRowIndex)

            End If
            _oldRowIndex = dgvHinsyu.CurrentCellAddress.Y

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

        Dim _dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHinsyu)

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
        dgvHinsyu.Focus()
        dgvHinsyu.CurrentCell = dgvHinsyu(prmColIndex, prmRowIndex)

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
                lblSyori.Text = ""
                lblKeikaku.Text = ""
                lblTHanbai.Text = ""
                lblYHanbai.Text = ""
                lblYYHanbai.Text = ""
            Else
                Dim syoriDate As String = ds.Tables(RS).Rows(0)("SYORI")
                Dim keikakuDate As String = ds.Tables(RS).Rows(0)("KEIKAKU")

                '「YYYY/MM」形式で表示
                lblSyori.Text = syoriDate.Substring(0, 4) & "/" & syoriDate.Substring(4, 2)
                lblKeikaku.Text = keikakuDate.Substring(0, 4) & "/" & keikakuDate.Substring(4, 2)

                '一覧ヘッダー表示
                lblTHanbai.Text = syoriDate.Substring(0, 4) & "/" & syoriDate.Substring(4, 2)
                lblYHanbai.Text = keikakuDate.Substring(0, 4) & "/" & keikakuDate.Substring(4, 2)

                '翌々月の日付を作成
                Dim yyhanbai As String = keikakuDate & "01"     '日付に変換するために日を付け足す
                Dim yyDate As DateTime = Date.ParseExact(yyhanbai, "yyyyMMdd", Nothing)
                yyDate = yyDate.AddMonths(1)        '1ヶ月足す

                '「YYYY/MM」形式で表示
                lblYYHanbai.Text = CStr(yyDate).Substring(0, 7)
            End If

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
            sql = " DELETE FROM ZG310E_W10 WHERE UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql)

            '更新日時を取得
            Dim updateDate As Date = Now

            'M02品種区分マスタ
            sql = ""
            sql = "INSERT INTO ZG310E_W10 ("
            sql = sql & N & " JUYOUCD "                     '需要先
            sql = sql & N & " ,HINSYUKBN "                  '品種区分
            sql = sql & N & " ,HINSYUKBNNM "                '品種区分名
            sql = sql & N & " ,UPDNAME "                    '端末ID
            sql = sql & N & " ,UPDDATE) "                   '処理開始年月日
            sql = sql & N & " SELECT "
            sql = sql & N & "   JUYOUCD "
            sql = sql & N & "   , HINSYUKBN "
            sql = sql & N & "   , HINSYUKBNNM "
            sql = sql & N & "   , '" & _tanmatuID & "'"
            sql = sql & N & "   , TO_DATE('" & updateDate & "', 'YYYY/MM/DD HH24:MI:SS')"
            sql = sql & N & " FROM  M02HINSYUKBN "
            _db.executeDB(sql)

            'M10汎用マスタ
            sql = ""
            sql = sql & N & "UPDATE ZG310E_W10 "
            sql = sql & N & "SET (JUYOUNM, JUYOSORT) = ("
            sql = sql & N & " SELECT M.NAME1, M.SORT FROM M01HANYO M "
            sql = sql & N & " WHERE M.KAHENKEY = ZG310E_W10.JUYOUCD "
            sql = sql & N & " AND M.KOTEIKEY = '01')"
            sql = sql & N & "WHERE ZG310E_W10.JUYOUCD = ("
            sql = sql & N & " SELECT M.KAHENKEY FROM M01HANYO M"
            sql = sql & N & " WHERE M.KAHENKEY = ZG310E_W10.JUYOUCD"
            sql = sql & N & " AND M.KOTEIKEY = '01')"
            _db.executeDB(sql)

            'T11品種別販売計画
            sql = ""
            sql = sql & N & "UPDATE ZG310E_W10 "
            sql = sql & N & "SET ("
            sql = sql & N & "THANBAIRYOU, "
            sql = sql & N & "YHANBAIRYOU, "
            sql = sql & N & "YYHANBAIRYOU) = ("
            sql = sql & N & " SELECT T.THANBAIRYOU,"
            sql = sql & N & " T.YHANBAIRYOU, "
            sql = sql & N & " T.YYHANBAIRYOU "
            sql = sql & N & " FROM T11HINSYUHANK T"
            sql = sql & N & " WHERE ZG310E_W10.JUYOUCD = T.JUYOUCD"
            sql = sql & N & "  AND ZG310E_W10.HINSYUKBN = T.HINSYUKBN)"
            sql = sql & N & "WHERE "
            sql = sql & N & "(JUYOUCD, "
            sql = sql & N & "HINSYUKBN) IN ("
            sql = sql & N & " SELECT T.JUYOUCD,"
            sql = sql & N & "  T.HINSYUKBN"
            sql = sql & N & " FROM T11HINSYUHANK T"
            sql = sql & N & " WHERE ZG310E_W10.JUYOUCD = T.JUYOUCD"
            sql = sql & N & " AND ZG310E_W10.HINSYUKBN = T.HINSYUKBN)"
            _db.executeDB(sql)

            'トランザクション終了
            _db.commitTran()

            '一覧表示
            Call dispWK01()

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
    '　　I　：　prmSort     ソート順(画面起動時は何も受け取らない)
    '-------------------------------------------------------------------------------
    Private Sub dispWK01(Optional ByVal prmSort As String = "")
        Try

            Dim sql As String = ""
            'ワークのデータを一覧に表示
            sql = sql & N & " SELECT "
            sql = sql & N & "  JUYOUCD " & COLDT_JUYOCD             '需要先
            sql = sql & N & " ,JUYOUNM " & COLDT_JUYONM             '需要先名
            sql = sql & N & " ,HINSYUKBN " & COLDT_HINKBN           '品種区分
            sql = sql & N & " ,HINSYUKBNNM " & COLDT_HINKBNNM       '品種区分名
            sql = sql & N & " ,THANBAIRYOU " & COLDT_THANBAI        '当月計画販売量
            sql = sql & N & " ,YHANBAIRYOU " & COLDT_YHANBAI        '翌月販売反映量
            sql = sql & N & " ,YYHANBAIRYOU " & COLDT_YYHANBAI      '翌々月販売反映量
            sql = sql & N & " ,JUYOSORT " & COLDT_JUYOSORT          '更新IOd
            sql = sql & N & " ,UPDNAME " & COLDT_UPDNM              '更新日時
            sql = sql & N & " FROM ZG310E_W10 "
            sql = sql & N & "   WHERE UPDNAME = '" & _tanmatuID & "'"
            sql = sql & N & " ORDER BY "
            If "".Equals(prmSort) Then
                sql = sql & N & " JUYOSORT, HINSYUKBN"
            Else
                sql = sql & N & " " & prmSort
            End If

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                btnTouroku.Enabled = False
                Throw New UsrDefException("対象データがありません。", _msgHd.getMSG("NonData"))
            Else                                    '抽出データがある場合、登録ボタン有効
                btnTouroku.Enabled = _updFlg
            End If

            '抽出データを一覧に表示する
            dgvHinsyu.DataSource = ds
            dgvHinsyu.DataMember = RS

            '一覧の件数を表示する
            lblKensu.Text = CStr(iRecCnt) & "件"

            '背景色の設定
            Call setBackcolor(0, 0)

            '一覧の最初の入力可能セルへフォーカスする
            setForcusCol(COLNO_THANBAI, 0)

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
    Private Sub updateWK01()
        Try

            Dim sql As String = ""
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHinsyu)

            'トランザクション開始
            _db.beginTran()

            '行数分だけループ
            For i As Integer = 0 To gh.getMaxRow - 1
                sql = ""
                sql = sql & N & " UPDATE ZG310E_W10 SET "
                '-->2010.12.17 chg by takagi #5
                'sql = sql & N & " THANBAIRYOU = TO_NUMBER('" & _db.rmNullStr(dgvHinsyu(COLNO_THANBAI, i).Value) & "') "
                'sql = sql & N & " ,YHANBAIRYOU = TO_NUMBER('" & _db.rmNullStr(dgvHinsyu(COLNO_YHANBAI, i).Value) & "') "
                'sql = sql & N & " ,YYHANBAIRYOU = TO_NUMBER('" & _db.rmNullStr(dgvHinsyu(COLNO_YYHANBAI, i).Value) & "') "
                sql = sql & N & " THANBAIRYOU = TO_NUMBER('" & _db.rmNullDouble(dgvHinsyu(COLNO_THANBAI, i).Value) & "') "
                sql = sql & N & " ,YHANBAIRYOU = TO_NUMBER('" & _db.rmNullDouble(dgvHinsyu(COLNO_YHANBAI, i).Value) & "') "
                sql = sql & N & " ,YYHANBAIRYOU = TO_NUMBER('" & _db.rmNullDouble(dgvHinsyu(COLNO_YYHANBAI, i).Value) & "') "
                '<--2010.12.17 chg by takagi #5
                sql = sql & N & "   WHERE UPDNAME = '" & _tanmatuID & "'"
                sql = sql & N & "   AND JUYOUCD = '" & dgvHinsyu(COLNO_JUYOCD, i).Value & "'"
                sql = sql & N & "   AND HINSYUKBN = '" & dgvHinsyu(COLNO_HINKBN, i).Value & "'"
                _db.executeDB(sql)

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

    '-------------------------------------------------------------------------------
    '　DB更新
    '　(処理概要)ワークテーブルの値をT11に登録する
    '-------------------------------------------------------------------------------
    Private Sub registT11()
        Try

            'T11削除
            Dim delCnt As Integer = 0           '削除レコード数
            Dim sql As String = ""
            sql = sql & N & " DELETE FROM T11HINSYUHANK "
            sql = sql & N & " WHERE UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql, delCnt)

            '更新日時を取得
            Dim updStartDate As Date = Now

            'ワークテーブルの値をT11に登録
            sql = ""
            sql = sql & N & " INSERT INTO T11HINSYUHANK ( "
            sql = sql & N & "       JUYOUCD "           '需要先
            sql = sql & N & "      ,HINSYUKBN "         '品種区分
            sql = sql & N & "      ,THANBAIRYOU "       '当月販売計画
            sql = sql & N & "      ,YHANBAIRYOU "       '翌月販売計画
            sql = sql & N & "      ,YYHANBAIRYOU "      '翌々月販売計画
            sql = sql & N & "      ,UPDNAME "           '端末ID
            sql = sql & N & "      ,UPDDATE )"          '更新日時
            sql = sql & N & " SELECT "
            sql = sql & N & "       JUYOUCD "           '需要先
            sql = sql & N & "      ,HINSYUKBN "         '品種区分
            sql = sql & N & "      ,THANBAIRYOU "       '当月販売計画
            sql = sql & N & "      ,YHANBAIRYOU "       '翌月販売計画
            sql = sql & N & "      ,YYHANBAIRYOU "      '翌々月販売計画
            sql = sql & N & "      ,UPDNAME "           '端末ID
            sql = sql & N & "      ,TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS')"
            sql = sql & N & "   FROM ZG310E_W10 "
            sql = sql & N & "       WHERE UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql)


            '更新終了日時を取得
            Dim updFinDate As Date = Now

            '更新件数の取得
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHinsyu)        'DGVハンドラの設定
            Dim updCnt As Integer = gh.getMaxRow

            '処理日時・計画日時取得
            Dim syoriDate As String = lblSyori.Text.Substring(0, 4) & lblSyori.Text.Substring(5, 2)
            Dim keikakuDate As String = lblKeikaku.Text.Substring(0, 4) & lblKeikaku.Text.Substring(5, 2)

            'T91実行履歴登録処理
            sql = ""
            sql = "INSERT INTO T91RIREKI ("
            sql = sql & N & "  PGID"                                                        '機能ID
            sql = sql & N & ", SNENGETU"                                                    '処理日時
            sql = sql & N & ", KNENGETU"                                                    '計画日時
            sql = sql & N & ", SDATESTART"                                                  '処理開始日時
            sql = sql & N & ", SDATEEND"                                                    '処理終了日時
            sql = sql & N & ", KENNSU1"                                                     '件数１（削除件数）
            sql = sql & N & ", KENNSU2"                                                     '件数２（登録件数）
            sql = sql & N & ", UPDNAME"                                                     '端末ID
            sql = sql & N & ", UPDDATE"                                                     '更新日時
            sql = sql & N & ") VALUES ("
            sql = sql & N & "  '" & PGID & "'"                                              '機能ID
            sql = sql & N & ", '" & syoriDate & "'"
            sql = sql & N & ", '" & keikakuDate & "'"
            sql = sql & N & ", TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "   '処理開始日時
            sql = sql & N & ", TO_DATE('" & updFinDate & "', 'YYYY/MM/DD HH24:MI:SS') "     '処理終了日時
            sql = sql & N & ", " & delCnt                                                   '件数１（削除件数）
            sql = sql & N & ", " & updCnt                                                   '件数２（登録件数）
            sql = sql & N & ", '" & _tanmatuID & "'"                                        '端末ID
            sql = sql & N & ", TO_DATE('" & updFinDate & "', 'YYYY/MM/DD HH24:MI:SS') "     '更新日時
            sql = sql & N & " )"
            _db.executeDB(sql)

            'T02処理制御テーブル更新
            _parentForm.updateSeigyoTbl(PGID, True, updStartDate, updFinDate)

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

#End Region

#Region "ユーザ定義関数:チェック処理"

    '------------------------------------------------------------------------------------------------------
    '  登録チェック
    '　(処理概要)各項目の必須・入力桁数チェックを行う
    '------------------------------------------------------------------------------------------------------
    Private Sub checkTouroku()
        Try

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHinsyu)

            For i As Integer = 0 To gh.getMaxRow - 1

                '-->2010.12.17 add by takagi #5
                If (Not "".Equals(gh.getCellData(COLDT_THANBAI, i))) OrElse _
                   (Not "".Equals(gh.getCellData(COLDT_YHANBAI, i))) OrElse _
                   (Not "".Equals(gh.getCellData(COLDT_YYHANBAI, i))) Then
                    '<--2010.12.17 add by takagi #5

                    '必須入力チェック
                    '工程コード
                    Call chekuHissuKeta(COLDT_THANBAI, "当月販売計画", i, COLNO_THANBAI)
                    '機械名
                    Call chekuHissuKeta(COLDT_YHANBAI, "翌月販売計画", i, COLNO_YHANBAI)
                    '通常稼働時間
                    Call chekuHissuKeta(COLDT_YYHANBAI, "翌々月販売計画", i, COLNO_YYHANBAI)

                    '-->2010.12.17 add by takagi #4
                End If
                '<--2010.12.17 add by takagi #4
                
            Next

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '  必須・入力桁数チェック
    '　(処理概要)セルが入力されているか・整数部が4桁までかチェクする
    '　　I　：　prmColName              チェックするセルの列名
    '　　I　：　prmColHeaderName        エラー時に表示する列名
    '　　I　：　prmCnt                  チェックするセルの行数
    '　　I　：　prmColNo                チェックするセルの列数
    '------------------------------------------------------------------------------------------------------
    Private Sub chekuHissuKeta(ByVal prmColName As String, ByVal prmColHeaderName As String, ByVal prmCnt As Integer, ByVal prmColNo As Integer)
        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvHinsyu)

            If "".Equals(gh.getCellData(prmColName, prmCnt).ToString) Then
                'フォーカスをあてる
                Call setForcusCol(prmColNo, prmCnt)
                'エラーメッセージの表示
                '-->2010.12.17 chg by takagi #13
                'Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【 '" & prmColHeaderName & "' ：" & prmCnt + 1 & "行目】"))
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput"))
                '<--2010.12.17 chg by takagi #13
            End If

            '桁数チェックチェック
            If CInt(gh.getCellData(prmColName, prmCnt).ToString) >= 10000 Then
                'フォーカスをあてる
                Call setForcusCol(prmColNo, prmCnt)
                'エラーメッセージの表示
                '-->2010.12.17 chg by takagi #13
                'Throw New UsrDefException("整数部は４桁以内で入力して下さい。", _msgHd.getMSG("over4Keta", "【 '" & prmColHeaderName & "' ：" & prmCnt + 1 & "行目】"))
                Throw New UsrDefException("整数部は４桁以内で入力して下さい。", _msgHd.getMSG("over4Keta"))
                '<--2010.12.17 chg by takagi #13
            End If

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

#End Region

End Class