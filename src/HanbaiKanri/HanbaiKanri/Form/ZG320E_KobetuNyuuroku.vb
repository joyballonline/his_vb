'======================================================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）個別販売計画入力
'    （フォームID）ZG320E_KobetuNyuuroku
'
'======================================================================================================
'　履歴　名前　　　　　日　付      マーク       内容
'------------------------------------------------------------------------------------------------------
'　(1)   中澤        2010/09/02                 新規
'　(2)   菅野        2011/01/25                 修正　未登録データ件数表示対応　#94
'------------------------------------------------------------------------------------------------------
Option Explicit On
Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.Combo
Imports UtilMDL.FileDirectory
Imports UtilMDL.xls

Public Class ZG320E_KobetuNyuuroku
    Inherits System.Windows.Forms.Form

#Region "リテラル値定義"
    '-------------------------------------------------------------------------------
    '   定数定義
    '-------------------------------------------------------------------------------

    'PG制御文字 
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const RS As String = "RecSet"                       'レコードセットテーブル

    Private Const PGID As String = "ZG320E"                     'T02に登録するPGID

    '一覧データバインド名
    Private Const COLDT_STENKAINM As String = "dtSTenkai"           'サイズ展開
    Private Const COLDT_JUYOUCD As String = "dtJuyouCD"             '需要先
    Private Const COLDT_JUYOUNAME As String = "dtJuyouName"         '需要先名
    Private Const COLDT_HINSYUKBN As String = "dtHinsyuKbn"         '品種区分
    Private Const COLDT_HINSYUKBNNM As String = "dtHinsyuKbnName"   '品種区分名
    Private Const COLDT_HINMEICD As String = "dtHinmeiCD"           '品名コード
    Private Const COLDT_HINMEI As String = "dtHinmei"               '品名
    Private Const COLDT_TANCYO As String = "dtTancyo"               '単長
    Private Const COLDT_THANBAI As String = "dtTHanbai"             '当月販売計画   
    Private Const COLDT_YHANBAI As String = "dtYHanbai"             '翌月販売計画
    Private Const COLDT_YYHANBAI As String = "dtYYHanbai"           '翌々月販売計画
    Private Const COLDT_JUYOUSORT As String = "dtJuyouSort"         '需要先表示順
    Private Const COLDT_STENKAISORT As String = "dtSTenkaiSort"     'サイズ展開表示順
    Private Const COLDT_UPDNAME As String = "dtUpdName"             '端末ID

    '一覧グリッド名
    Private Const COLCN_STENKAINM As String = "cnSTenkai"           'サイズ展開
    Private Const COLCN_JUYOUCD As String = "cnJuyouCD"             '需要先
    Private Const COLCN_JUYOUNAME As String = "cnJuyouName"         '需要先名
    Private Const COLCN_HINSYUKBN As String = "cnHinsyuKbn"         '品種区分
    Private Const COLCN_HINSYUKBNNM As String = "cnHinsyuKbnName"   '品種区分名
    '' 2010/12/27 upd start sugano
    'Private Const COLCN_HINMEICD As String = "cnHinmei"             '品名コード
    Private Const COLCN_HINMEICD As String = "cnHinmeiCD"           '品名コード
    '' 2010/12/27 upd end sugano
    Private Const COLCN_HINMEI As String = "cnHinmei"               '品名
    Private Const COLCN_TANCYO As String = "cnTancyo"               '単長
    Private Const COLCN_THANBAI As String = "cnTHanbai"             '当月販売計画   
    Private Const COLCN_YHANBAI As String = "cnYHanbai"             '翌月販売計画
    Private Const COLCN_YYHANBAI As String = "cnYYHanbai"           '翌々月販売計画
    Private Const COLCN_JUYOUSORT As String = "cnJuyouSort"         '需要先表示順
    Private Const COLCN_STENKAISORT As String = "cnSTenkaiSort"     'サイズ展開表示順
    Private Const COLCN_UPDNAME As String = "cnUpdName"             '端末ID

    '一覧列番号
    Private Const COLNO_STENKAINM As Integer = 0                    'サイズ展開
    Private Const COLNO_JUYOUCD As Integer = 1                      '需要先
    Private Const COLNO_JUYOUNAME As Integer = 2                    '需要先名
    Private Const COLNO_HINSYUKBN As Integer = 3                    '品種区分
    Private Const COLNO_HINSYUKBNNM As Integer = 4                  '品種区分名
    Private Const COLNO_HINMEICD As Integer = 5                     '品名コード
    Private Const COLNO_HINMEI As Integer = 6                       '品名
    Private Const COLNO_TANCYO As Integer = 7                       '単長
    Private Const COLNO_THANBAI As Integer = 8                      '当月販売計画   
    Private Const COLNO_YHANBAI As Integer = 9                      '翌月販売計画
    Private Const COLNO_YYHANBAI As Integer = 10                    '翌々月販売計画
    Private Const COLNO_JUYOUSORT As Integer = 11                   '需要先表示順
    Private Const COLNO_STENKAISORT As Integer = 12                 'サイズ展開表示順
    Private Const COLNO_UPDNAME As Integer = 13                     '端末ID

    'ラベル押下時並べ替え用リテラル
    Private Const LBL_JUYO As String = "需要先"
    Private Const LBL_HINSYU As String = "品種区分"
    Private Const LBL_HINMEICD As String = "品名コード"
    Private Const LBL_HINMEI As String = "品名"
    Private Const LBL_SYOJUN As String = "▼"
    Private Const LBL_KOJUN As String = "▲"

    '固定キー
    Private Const COTEI_JUYOU As String = "01"                      '需要先
    Private Const COTEI_STENKAI As String = "11"                    'サイズ展開パターン

    'EXCEL
    Private Const START_PRINT As Integer = 7                        'EXCEL出力開始行数

    Private Const XLSCOL_STENKAI As Integer = 1
    Private Const XLSCOL_JUYOUCD As Integer = 2
    Private Const XLSCOL_JUYOUNM As Integer = 3
    Private Const XLSCOL_HINSYUKBN As Integer = 4
    Private Const XLSCOL_HINSYUKBNNM As Integer = 5
    Private Const XLSCOL_HINMEICD As Integer = 6
    Private Const XLSCOL_HINMEI As Integer = 7
    Private Const XLSCOL_TANCHO As Integer = 8
    Private Const XLSCOL_THANBAI As Integer = 9
    Private Const XLSCOL_YHANBAI As Integer = 10
    Private Const XLSCOL_YYHANBAI As Integer = 11

#End Region

#Region "メンバー変数宣言"

    '-------------------------------------------------------------------------------
    '   変数定義
    '-------------------------------------------------------------------------------

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As ZC110M_Menu

    Private _oldRowIndex As Integer = -1            '選択行の背景色を変更するための変数
    Private _colorCtlFlg As Boolean = False         '選択行の背景色を変更するためのフラグ

    Private _tanmatuID As String = ""               '端末ID

    Private _changeFlg As Boolean = False           '一覧データ変更フラグ
    Private _beforeChange As Double = 0             '一覧変更前のデータ

    Private _chkCellVO As UtilDgvChkCellVO          '一覧の入力制限用

    Private _errSet As UtilDataGridViewHandler.dgvErrSet    'エラー発生時にフォーカスするセル位置
    Private _nyuuryokuErrFlg As Boolean = False             '入力エラー有無フラグ

    '検索条件格納変数
    Private _serchJuyo As String = ""               '需要先
    Private _serchCdJuyo As String = ""             '需要先コンボボックスのコード
    Private _serchHinsyuKbn As String = ""          '品種区分
    Private _serchSiyoCd As String = ""             '仕様コード
    Private _serchHinsyuCd As String = ""           '品種コード
    Private _serchSensinCd As String = ""           '線心数コード
    Private _serchSizeCd As String = ""             'サイズコード
    Private _serchColorCd As String = ""            '色コード

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
    Private Sub ZG320E_KobetuNyuuroku_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            '描画関係の設定
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           'サイズが変更されたときに、コントロールがコントロール自体を再描画するかどうかを示す値を設定
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '描画はバッファで実行され、完了後に、結果が画面に出力されるよう設定
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  'コントロールは、画面に直接ではなく、まずバッファに描画されます。これにより、ちらつきを抑えることができます。
            Me.SetStyle(ControlStyles.UserPaint, True)              'コントロールは、オペレーティング システムによってではなく、独自に描画されるよう設定
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   'コントロールはウィンドウ メッセージ WM_ERASEBKGND を無視するように設定

            Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr 'タイトルオプション表示

            '画面表示
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
    '　戻るボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnModoru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModoru.Click

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

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　検索ボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnKensaku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKensaku.Click
        Try
            '警告メッセージ
            If _changeFlg Then
                Dim rtn As DialogResult = _msgHd.dspMSG("confirmSrcEdit")   '編集中の内容が破棄されます。よろしいですか？
                If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If

            '変更フラグをリセットする
            _changeFlg = False

            'マウスカーソル砂時計
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor
            Try

                '検索条件の作成
                Dim sqlWhere As String = ""
                sqlWhere = createSerchStr()

                'トランザクション開始
                _db.beginTran()

                'ワークテーブルの作成
                Call delInsWK01(sqlWhere)
                Call createWK01()

                'トランザクション終了
                _db.commitTran()

                '一覧行着色フラグを無効にする
                _colorCtlFlg = False

                '一覧表示
                Call dispWK01()

                '一覧行着色フラグを有効にする
                _colorCtlFlg = True

                '一覧の最初の入力可能セルへフォーカスする
                setForcusCol(COLNO_THANBAI, 0)

                '並べ替えラベルの表示初期化
                Call initLabel()

                '一覧下の合計表示
                Call dispTotal()

                '処理年月、計画年月表示
                Call dispDate()

            Finally
                'マウスカーソル矢印
                Me.Cursor = cur
            End Try

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

            '変更フラグをリセットする
            _changeFlg = False

            'マウスカーソル砂時計
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor
            Try

                '一覧のデータをワークテーブルに更新
                Call updateWK01()

                'トランザクション開始
                _db.beginTran()

                'T12へ登録
                Call registT12()

                'トランザクション終了
                _db.commitTran()

                'マウスカーソル矢印
                Me.Cursor = Cursors.Arrow

                '並べ替えラベルの表示初期化
                Call initLabel()

                '' 2011/01/25 CHG-S Sugano #94
                ''完了メッセージ
                '_msgHd.dspMSG("completeInsert")

                '未登録データの件数を取得
                Dim mireccnt As Integer '未登録データ件数
                Call getMitourokuCount(mireccnt)

                Dim msg As String
                If mireccnt = 0 Then
                    msg = ""
                Else
                    msg = "★　未登録データは" & mireccnt & "件です　★　"
                End If
                '完了メッセージ
                _msgHd.dspMSG("completeInsert", msg)
                '' 2011/01/25 CHG-E Sugano #94

                '一覧の変更フラグを無効にする
                _changeFlg = False

            Finally
                'マウスカーソル矢印
                Me.Cursor = cur
            End Try

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

        'マウスカーソル砂時計
        Me.Cursor = Cursors.WaitCursor
        '-->2010.01.13 add by takagi #64
        Me.Enabled = False
        Try
            '<--2010.01.13 add by takagi #64

            'EXCEL出力
            Call printExcel()

            '-->2010.01.13 add by takagi #64
        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        Finally
            Me.Enabled = True
            btnExcel.Focus()
            '<--2010.01.13 add by takagi #64

            'マウスカーソル元に戻す
            Me.Cursor = Cursors.Arrow

            '<--2010.01.13 add by takagi #64
        End Try
        '-->2010.01.13 add by takagi #64

    End Sub

#End Region

#Region "ユーザ定義関数:EXCEL関連"

    '------------------------------------------------------------------------------------------------------
    '　EXCEL出力処理
    '------------------------------------------------------------------------------------------------------
    Private Sub printExcel()
        Try

            '雛形ファイル
            Dim openFilePath As String = StartUp.iniValue.BaseXlsPath & "\" & StartUp.iniValue.ExcelZG320R1_Base
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
            Dim wkEditFile As String = StartUp.iniValue.OutXlsPath & "\" & StartUp.iniValue.ExcelZG320R1_Out     'コピー先ファイル

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
                eh.open()
                Try
                    Dim startPrintRow As Integer = START_PRINT          '出力開始行数
                    Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvKHanbai)        'DGVハンドラの設定
                    Dim rowCnt As Integer = gh.getMaxRow
                    Dim i As Integer
                    For i = 0 To rowCnt - 1

                        '列を1行追加
                        eh.copyRow(startPrintRow + i)
                        eh.insertPasteRow(startPrintRow + i)
                        '一覧データ出力
                        eh.setValue(_db.rmNullStr(dgvKHanbai(COLCN_STENKAINM, i).Value), startPrintRow + i, XLSCOL_STENKAI)         'サイズ展開パターン
                        eh.setValue(_db.rmNullStr(dgvKHanbai(COLCN_JUYOUCD, i).Value), startPrintRow + i, XLSCOL_JUYOUCD)           '需要先コード
                        eh.setValue(_db.rmNullStr(dgvKHanbai(COLCN_JUYOUNAME, i).Value), startPrintRow + i, XLSCOL_JUYOUNM)         '需要先名
                        eh.setValue(_db.rmNullStr(dgvKHanbai(COLCN_HINSYUKBN, i).Value), startPrintRow + i, XLSCOL_HINSYUKBN)       '品種区分
                        eh.setValue(_db.rmNullStr(dgvKHanbai(COLCN_HINSYUKBNNM, i).Value), startPrintRow + i, XLSCOL_HINSYUKBNNM)   '品種区分名
                        eh.setValue(_db.rmNullStr(dgvKHanbai(COLCN_HINMEICD, i).Value), startPrintRow + i, XLSCOL_HINMEICD)         '品名コード
                        eh.setValue(_db.rmNullStr(dgvKHanbai(COLCN_HINMEI, i).Value), startPrintRow + i, XLSCOL_HINMEI)             '品名   
                        eh.setValue(_db.rmNullStr(dgvKHanbai(COLCN_TANCYO, i).Value), startPrintRow + i, XLSCOL_TANCHO)             '単長
                        eh.setValue(_db.rmNullStr(dgvKHanbai(COLCN_THANBAI, i).Value), startPrintRow + i, XLSCOL_THANBAI)           '当月販売計画
                        eh.setValue(_db.rmNullStr(dgvKHanbai(COLCN_YHANBAI, i).Value), startPrintRow + i, XLSCOL_YHANBAI)           '翌月販売計画
                        eh.setValue(_db.rmNullStr(dgvKHanbai(COLCN_YYHANBAI, i).Value), startPrintRow + i, XLSCOL_YYHANBAI)         '翌々月販売計画

                    Next

                    '合計行表示
                    eh.setValue("合計", startPrintRow + i, 1)
                    eh.setValue("-", startPrintRow + i, 2)
                    eh.setValue("-", startPrintRow + i, 3)
                    eh.setValue("-", startPrintRow + i, 4)
                    eh.setValue("-", startPrintRow + i, 5)
                    eh.setValue("-", startPrintRow + i, 6)
                    eh.setValue("-", startPrintRow + i, 7)
                    eh.setValue("-", startPrintRow + i, 8)
                    eh.setValue(lblTKei.Text, startPrintRow + i, 9)         '当月販売計画
                    eh.setValue(lblYKei.Text, startPrintRow + i, 10)        '翌月販売計画
                    eh.setValue(lblYYKei.Text, startPrintRow + i, 11)       '翌々月販売計画

                    '罫線を再設定
                    Dim lineV As LineVO = New LineVO()
                    lineV.Bottom = LineVO.LineType.NomalL
                    eh.drawRuledLine(lineV, startPrintRow + i, 1, , 11)

                    '作成日時編集
                    Dim printDate As String = Now.ToString("yyyy/MM/dd HH:mm")
                    eh.setValue("作成日時 ： " & printDate, 1, 11)   'K1

                    '処理年月、計画年月編集
                    eh.setValue("処理年月：" & lblSyori.Text & "　　計画年月：" & lblKeikaku.Text, 1, 6)    'F1

                    '件数編集
                    eh.setValue(rowCnt & "件", 3, 11)    'K3

                    '検索条件
                    eh.setValue("サイズ別展開ﾊﾟﾀｰﾝ：" & lblSTenkai.Text, 3, 1)      'A3
                    eh.setValue("品種区分：" & _serchHinsyuKbn, 3, 7)               'G3
                    eh.setValue("品名コード：" & createHinmeiCd(), 3, 8)            'H3

                    '需要先の取得と表示
                    If Not "".Equals(_serchJuyo) Then
                        Dim sql As String = ""
                        sql = sql & N & " SELECT NAME1  FROM M01HANYO WHERE KOTEIKEY = '01' "
                        sql = sql & N & "   AND KAHENKEY = '" & _serchCdJuyo & "'"
                        'SQL発行
                        Dim iRecCnt As Integer          'データセットの行数
                        Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)
                        If iRecCnt = 1 Then
                            eh.setValue("需要先：" & ds.Tables(RS).Rows(0)(0), 3, 5)    'E3
                        Else
                            eh.setValue("需要先：", 3, 5)       'E3
                        End If
                    Else
                        eh.setValue("需要先：", 3, 5)           'E3
                    End If

                        'ヘッダーの年月編集
                    eh.setValue(lblTHanbai.Text, 6, 9)          'I6
                    eh.setValue(lblYHanbai.Text, 6, 10)         'J6
                    eh.setValue(lblYYHanbai.Text, 6, 11)        'K6

                        '左上のセルにフォーカス当てる
                    eh.selectCell(7, 1)     'A7

                    Clipboard.Clear()       'クリップボードの初期化

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
    '　品名コード作成
    '　(処理概要)EXCELに出力する品名コードを編集して返す。
    '　　I　：　なし
    '　　R　：　createHinmeiCd      '編集した品名コード
    '-------------------------------------------------------------------------------
    Private Function createHinmeiCd() As String
        Try
            createHinmeiCd = ""

            '仕様コード
            If _serchSiyoCd.Length = 2 Then
                createHinmeiCd = _serchSiyoCd & "-"
            ElseIf _serchSiyoCd.Length = 1 Then
                createHinmeiCd = _serchSiyoCd.Substring(0, 1) & "*-"
            Else
                createHinmeiCd = "**-"
            End If

            '品種コード
            If _serchHinsyuCd.Length = 3 Then
                createHinmeiCd = createHinmeiCd & _serchHinsyuCd & "-"
            ElseIf _serchHinsyuCd.Length = 2 Then
                createHinmeiCd = createHinmeiCd & _serchHinsyuCd.Substring(0, 2) & "*-"
            ElseIf _serchHinsyuCd.Length = 1 Then
                createHinmeiCd = createHinmeiCd & _serchHinsyuCd.Substring(0, 1) & "**-"
            Else
                createHinmeiCd = createHinmeiCd & "***-"
            End If

            '線心数コード
            If _serchSensinCd.Length = 3 Then
                createHinmeiCd = createHinmeiCd & _serchSensinCd & "-"
            ElseIf _serchSensinCd.Length = 2 Then
                createHinmeiCd = createHinmeiCd & _serchSensinCd.Substring(0, 2) & "*-"
            ElseIf _serchSensinCd.Length = 1 Then
                createHinmeiCd = createHinmeiCd & _serchSensinCd.Substring(0, 1) & "**-"
            Else
                createHinmeiCd = createHinmeiCd & "***-"
            End If

            'サイズコード
            If _serchSizeCd.Length = 2 Then
                createHinmeiCd = createHinmeiCd & _serchSizeCd & "-"
            ElseIf _serchSizeCd.Length = 1 Then
                createHinmeiCd = createHinmeiCd & _serchSizeCd.Substring(0, 1) & "*-"
            Else
                createHinmeiCd = createHinmeiCd & "**-"
            End If

            '色コード
            If _serchColorCd.Length = 3 Then
                createHinmeiCd = createHinmeiCd & _serchColorCd
            ElseIf _serchColorCd.Length = 2 Then
                createHinmeiCd = createHinmeiCd & _serchColorCd.Substring(0, 2) & "*"
            ElseIf _serchColorCd.Length = 1 Then
                createHinmeiCd = createHinmeiCd & _serchColorCd.Substring(0, 1) & "**"
            Else
                createHinmeiCd = createHinmeiCd & "***"
            End If

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Function


#End Region

#Region "ユーザ定義関数:画面制御"

    '-------------------------------------------------------------------------------
    '　画面起動時
    '-------------------------------------------------------------------------------
    Private Sub initForm()
        Try

            '登録ボタン・EXCELボタン使用不可
            btnTouroku.Enabled = False
            btnExcel.Enabled = False

            '端末IDの取得
            _tanmatuID = UtilClass.getComputerName

            'マウスカーソル砂時計
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor
            Try

                '処理年月、計画年月表示
                Call dispDate()

                '需要先コンボボックス・サイズ別展開のセット
                Call setJuyouCbo()

                'トランザクション開始
                _db.beginTran()

                '一覧データ作成
                Call delInsWK01()
                Call createWK01()

                'トランザクション終了
                _db.commitTran()

                '一覧表示
                Call dispWK01()

                '一覧行着色フラグを有効にする
                _colorCtlFlg = True

                '一覧下の合計表示
                Call dispTotal()

                '背景色の設定
                Call setBackcolor(0, 0)

            Finally
                'マウスカーソル矢印
                Me.Cursor = cur
            End Try

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        Finally
            If _db.isTransactionOpen = True Then
                _db.rollbackTran()                          'ロールバック
            End If
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　並べ替えラベル押下時
    '　(処理概要)一覧を並び替える
    '-------------------------------------------------------------------------------
    Private Sub lblJuyoSort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblJuyoSort.Click, _
                                                                                                    lblHinmeiCDSort.Click, _
                                                                                                    lblHinsyuSort.Click, _
                                                                                                    lblHinmeiSort.Click
        Try
            'マウスカーソル砂時計
            Dim cur As Cursor = Me.Cursor
            Me.Cursor = Cursors.WaitCursor
            Try

                '一覧行着色フラグを無効にする
                _colorCtlFlg = False

                '一覧のデータをワークテーブルに更新
                Call updateWK01()

                '一覧ヘッダーラベル編集
                If sender.Equals(lblJuyoSort) Then
                    '需要先
                    If (LBL_JUYO & N & LBL_SYOJUN).Equals(lblJuyoSort.Text) Then
                        '既に昇順で並べ替えられている場合
                        dispWK01("JUYOSORT DESC")
                        lblJuyoSort.Text = LBL_JUYO & N & LBL_KOJUN
                    Else
                        '初回押下時または既に降順で並べ替えられている場合
                        dispWK01("JUYOSORT")
                        lblJuyoSort.Text = LBL_JUYO & N & LBL_SYOJUN
                    End If
                    '品種区分・品名コード・品名のラベルを元に戻す
                    lblHinsyuSort.Text = LBL_HINSYU
                    lblHinmeiCDSort.Text = LBL_HINMEICD
                    lblHinmeiSort.Text = LBL_HINMEI
                ElseIf sender.Equals(lblHinsyuSort) Then
                    '品種区分
                    If (LBL_HINSYU & N & LBL_SYOJUN).Equals(lblHinsyuSort.Text) Then
                        '既に昇順で並べ替えられている場合
                        dispWK01("HINSYUKBN DESC")
                        lblHinsyuSort.Text = LBL_HINSYU & N & LBL_KOJUN
                    Else
                        '初回押下時または既に降順で並べ替えられている場合
                        dispWK01("HINSYUKBN")
                        lblHinsyuSort.Text = LBL_HINSYU & N & LBL_SYOJUN
                    End If
                    '需要先・品名コード・品名のラベルを元に戻す
                    lblJuyoSort.Text = LBL_JUYO
                    lblHinmeiCDSort.Text = LBL_HINMEICD
                    lblHinmeiSort.Text = LBL_HINMEI
                ElseIf sender.Equals(lblHinmeiCDSort) Then
                    '品名コード
                    If (LBL_HINMEICD & N & LBL_SYOJUN).Equals(lblHinmeiCDSort.Text) Then
                        '既に昇順で並べ替えられている場合
                        dispWK01("KHINMEICD DESC")
                        lblHinmeiCDSort.Text = LBL_HINMEICD & N & LBL_KOJUN
                    Else
                        '初回押下時または既に降順で並べ替えられている場合
                        dispWK01("KHINMEICD")
                        lblHinmeiCDSort.Text = LBL_HINMEICD & N & LBL_SYOJUN
                    End If
                    '需要先・品種区分・品名のラベルを元に戻す
                    lblJuyoSort.Text = LBL_JUYO
                    lblHinsyuSort.Text = LBL_HINSYU
                    lblHinmeiSort.Text = LBL_HINMEI
                ElseIf sender.Equals(lblHinmeiSort) Then
                    '品名
                    If (LBL_HINMEI & N & LBL_SYOJUN).Equals(lblHinmeiSort.Text) Then
                        '既に昇順で並べ替えられている場合
                        dispWK01("HINMEI DESC")
                        lblHinmeiSort.Text = LBL_HINMEI & N & LBL_KOJUN
                    Else
                        '初回押下時または既に降順で並べ替えられている場合
                        dispWK01("HINMEI")
                        lblHinmeiSort.Text = LBL_HINMEI & N & LBL_SYOJUN
                    End If
                    '需要先・品種区分・品種コードのラベルを元に戻す
                    lblJuyoSort.Text = LBL_JUYO
                    lblHinsyuSort.Text = LBL_HINSYU
                    lblHinmeiCDSort.Text = LBL_HINMEI

                Else
                    Exit Sub
                End If

                '背景色の設定
                Call setBackcolor(0, 0)

                '一覧行着色フラグを有効にする
                _colorCtlFlg = True

            Finally
                'マウスカーソル元に戻す
                Me.Cursor = cur
            End Try

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　検索条件SQL作成
    '　(処理概要)画面に入力された検索条件をSQL文にする
    '-------------------------------------------------------------------------------
    Private Function createSerchStr() As String
        Try
            createSerchStr = ""
            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(cboJuyou)

            '検索条件の保持
            _serchJuyo = _db.rmNullStr(ch.getName)
            _serchCdJuyo = _db.rmNullStr(ch.getCode)
            _serchHinsyuKbn = _db.rmSQ(txtHinsyuKbn.Text)
            _serchSiyoCd = txtSiyouCD.Text
            _serchHinsyuCd = txtHinsyuCD.Text
            _serchSensinCd = txtSensinsuu.Text
            _serchSizeCd = txtSize.Text
            _serchColorCd = txtColor.Text

            '需要先
            If Not "".Equals(_serchJuyo) Then
                createSerchStr = createSerchStr & N & " TT_JUYOUCD = '" & _serchCdJuyo & "'"
            End If

            '品種区分
            If Not "".Equals(_serchHinsyuKbn) Then
                If Not "".Equals(createSerchStr) Then
                    createSerchStr = createSerchStr & N & " AND "
                End If
                createSerchStr = createSerchStr & " TT_HINSYUKBN LIKE '" & _serchHinsyuKbn & "%'"
            End If

            '品名コード仕様
            If Not "".Equals(_serchSiyoCd) Then
                If Not "".Equals(createSerchStr) Then
                    createSerchStr = createSerchStr & N & " AND "
                End If
                createSerchStr = createSerchStr & " TT_H_SIYOU_CD LIKE '" & _serchSiyoCd & "%'"
            End If

            '品名コード品種
            If Not "".Equals(_serchHinsyuCd) Then
                If Not "".Equals(createSerchStr) Then
                    createSerchStr = createSerchStr & N & " AND "
                End If
                createSerchStr = createSerchStr & " TT_H_HIN_CD LIKE '" & _serchHinsyuCd & "%'"
            End If

            '品名コード線心数
            If Not "".Equals(_serchSensinCd) Then
                If Not "".Equals(createSerchStr) Then
                    createSerchStr = createSerchStr & N & " AND "
                End If
                createSerchStr = createSerchStr & " TT_H_SENSIN_CD LIKE '" & _serchSensinCd & "%'"
            End If

            '品名コードサイズ
            If Not "".Equals(_serchSizeCd) Then
                If Not "".Equals(createSerchStr) Then
                    createSerchStr = createSerchStr & N & " AND "
                End If
                createSerchStr = createSerchStr & " TT_H_SIZE_CD LIKE '" & _serchSizeCd & "%'"
            End If

            '品名コード品種
            If Not "".Equals(_serchColorCd) Then
                If Not "".Equals(createSerchStr) Then
                    createSerchStr = createSerchStr & N & " AND "
                End If
                createSerchStr = createSerchStr & " TT_H_COLOR_CD LIKE '" & _serchColorCd & "%'"
            End If
            If Not "".Equals(createSerchStr) Then
                createSerchStr = " AND " & createSerchStr
            End If
        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Function

    '-------------------------------------------------------------------------------
    '　コントロールキー押下イベント
    '　(処理概要)エンターボタン押下時に次のコントロールに移る
    '-------------------------------------------------------------------------------
    Private Sub txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboJuyou.KeyPress, _
                                                                                                                txtHinsyuKbn.KeyPress, _
                                                                                                                txtHinsyuCD.KeyPress, _
                                                                                                                txtSiyouCD.KeyPress, _
                                                                                                                txtSensinsuu.KeyPress, _
                                                                                                                txtSize.KeyPress, _
                                                                                                                txtColor.KeyPress
        UtilClass.moveNextFocus(Me, e) '次のコントロールへ移動する 

    End Sub

    '-------------------------------------------------------------------------------
    '　コントロール全選択
    '　(処理概要)コントロール移動時に全選択状態にする
    '-------------------------------------------------------------------------------
    Private Sub serch_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtHinsyuKbn.GotFocus, _
                                                                                            txtHinsyuCD.GotFocus, _
                                                                                            txtSiyouCD.GotFocus, _
                                                                                            txtSensinsuu.GotFocus, _
                                                                                            txtSize.GotFocus, _
                                                                                            txtColor.GotFocus
        UtilClass.selAll(sender)

    End Sub

    '-------------------------------------------------------------------------------
    '　並べ替えラベルの表示初期化
    '　(処理概要)検索・登録ボタン押下時、並べ替えラベルの表示を初期化する
    '-------------------------------------------------------------------------------
    Private Sub initLabel()

        lblJuyoSort.Text = LBL_JUYO
        lblHinsyuSort.Text = LBL_HINSYU
        lblHinmeiCDSort.Text = LBL_HINMEICD
        lblHinmeiSort.Text = LBL_HINMEI

    End Sub

#End Region

#Region "ユーザ定義関数:DGV関連"

    '------------------------------------------------------------------------------------------------------
    '　一覧データ編集前
    '　(処理概要)一覧のデータが変更される前の値を保持する
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvKHanbai_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles dgvKHanbai.CellBeginEdit

        '既に変更フラグが立っている場合は何も行わない
        If _changeFlg = False Then
            _beforeChange = _db.rmNullDouble(dgvKHanbai(e.ColumnIndex, e.RowIndex).Value)
        End If
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　一覧データ編集後
    '　(処理概要)一覧のデータが変更された場合、変更フラグを立て、合計の値を再表示する
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvKHanbai_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvKHanbai.CellEndEdit
        Try
            If _changeFlg = False Then
                '編集前と値が変わっていた場合、フラグを立てる
                If Not _beforeChange.Equals(_db.rmNullDouble(dgvKHanbai(e.ColumnIndex, e.RowIndex).Value)) Then
                    _changeFlg = True
                Else
                    Exit Sub
                End If
            End If

            '合計の再計算
            Call dispTotal()

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
    Private Sub dgvKHanbai_EditingControlShowing(ByVal sender As Object, _
                                              ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs _
                                              ) Handles dgvKHanbai.EditingControlShowing

        Try
            Dim _dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvKHanbai)        'DGVハンドラの設定
            '■当月販売計画、翌月販売計画、翌々月販売計画の場合
            If dgvKHanbai.CurrentCell.ColumnIndex = COLNO_THANBAI Or _
                            dgvKHanbai.CurrentCell.ColumnIndex = COLNO_YHANBAI Or _
                            dgvKHanbai.CurrentCell.ColumnIndex = COLNO_YYHANBAI Then

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
    '   選択セル検証イベント（DataErrorイベント）
    '   （処理概要）数値入力欄に数値以外が入力された場合のエラー処理
    '-------------------------------------------------------------------------------
    Private Sub dgvKHanbai_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvKHanbai.DataError

        Try
            e.Cancel = False                                   '編集モード終了

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvKHanbai)
            '■当月販売計画、翌月販売計画、翌々月販売計画の場合、グリッドには数値入力モード(0～9)の制限をかけているので、制限の解除
            If dgvKHanbai.CurrentCell.ColumnIndex = COLNO_THANBAI Or _
                            dgvKHanbai.CurrentCell.ColumnIndex = COLNO_YHANBAI Or _
                            dgvKHanbai.CurrentCell.ColumnIndex = COLNO_YYHANBAI Then

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
    Private Sub dgvKHanbai_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvKHanbai.SelectionChanged
        Try

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvKHanbai)

            '入力エラーがあった場合
            If _nyuuryokuErrFlg Then
                _nyuuryokuErrFlg = False
                gh.setCurrentCell(_errSet)
            End If

            If _colorCtlFlg Then
                '背景色の設定
                Call setBackcolor(dgvKHanbai.CurrentCellAddress.Y, _oldRowIndex)

            End If
            _oldRowIndex = dgvKHanbai.CurrentCellAddress.Y

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　選択行に着色する処理
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvKHanbai_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvKHanbai.CellEnter
        If _colorCtlFlg Then
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvKHanbai)
            gh.setSelectionRowColor(dgvKHanbai.CurrentCellAddress.Y, _oldRowIndex, StartUp.lCOLOR_BLUE)
        End If
        _oldRowIndex = dgvKHanbai.CurrentCellAddress.Y
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　背景色の設定処理
    '　(処理概要)行の背景色を青に着色する。
    '　　I　：　prmRowIndex     現在フォーカスがある行数
    '　　I　：　prmOldRowIndex  現在の行に移る前の行数
    '------------------------------------------------------------------------------------------------------
    Private Sub setBackcolor(ByVal prmRowIndex As Integer, ByVal prmOldRowIndex As Integer)

        Dim _dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvKHanbai)

        '指定した行の背景色を青にする
        _dgv.setSelectionRowColor(prmRowIndex, prmOldRowIndex, StartUp.lCOLOR_BLUE)

        _oldRowIndex = prmRowIndex

    End Sub

    '-------------------------------------------------------------------------------
    '　一覧下合計表示
    '　(処理概要)一覧の下の合計を表示する。
    '-------------------------------------------------------------------------------
    Private Sub dispTotal()
        Try

            Dim tTotal As Double = 0       '当月販売計画の合計
            Dim yTotal As Double = 0       '翌月販売計画の合計
            Dim yyTotal As Double = 0      '翌々販売計画の合計

            Dim _dgv As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvKHanbai)

            '列ごとに合計を算出する
            For i As Integer = 0 To _dgv.getMaxRow - 1
                tTotal = tTotal + _db.rmNullDouble(dgvKHanbai(COLNO_THANBAI, i).Value)
                yTotal = yTotal + _db.rmNullDouble(dgvKHanbai(COLNO_YHANBAI, i).Value)
                yyTotal = yyTotal + _db.rmNullDouble(dgvKHanbai(COLNO_YYHANBAI, i).Value)
            Next

            'カンマ編集して表示
            lblTKei.Text = CStr(tTotal.ToString("N1"))
            lblYKei.Text = CStr(yTotal.ToString("N1"))
            lblYYKei.Text = CStr(yyTotal.ToString("N1"))

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　指定列へのフォーカス設定処理
    '　(処理概要)指定されたセルにフォーカスする。
    '　　I　：　prmCoIndex      フォーカスさせるセルの列数
    '　　I　：　prmRowIndex     フォーカスさせるセルの行数
    '------------------------------------------------------------------------------------------------------
    Private Sub setForcusCol(ByVal prmColIndex As Integer, ByVal prmRowIndex As Integer)

        'フォーカスをあてる
        dgvKHanbai.Focus()
        dgvKHanbai.CurrentCell = dgvKHanbai(prmColIndex, prmRowIndex)

        '背景色の設定
        Call setBackcolor(prmRowIndex, _oldRowIndex)

    End Sub

#End Region

#Region "ユーザ定義関数:DB関連"

    '-------------------------------------------------------------------------------
    '　ワークテーブルデータの作成
    '　(処理概要)ワークテーブルのデータを作成(delete & insert)
    '　　I　：　prmSql     検索条件(画面起動時は何も受け取らない)
    '-------------------------------------------------------------------------------
    Private Sub delInsWK01(Optional ByVal prmSql As String = "")
        Try
            Dim sql As String = ""
            sql = " DELETE FROM ZG320E_W10 WHERE UPDNAME = '" & _tanmatuID & "'"
            _db.executeDB(sql)

            '更新日時を取得
            Dim updateDate As Date = Now

            'M01計画対象品
            sql = ""
            sql = sql & N & " INSERT INTO ZG320E_W10 ("
            sql = sql & N & "    STENKAIPTN "       'サイズ展開パターン
            sql = sql & N & "   ,JUYOUCD "          '需要先コード
            sql = sql & N & "   ,HINSYUKBN "        '品種区分
            sql = sql & N & "   ,KHINMEICD "        '計画品名コード
            sql = sql & N & "   ,HINMEI "           '品名
            sql = sql & N & "   ,TANCYO"            '単長
            sql = sql & N & "   ,SIYOUCD "          '仕様コード
            sql = sql & N & "   ,HINSYUCD "         '品種コード
            sql = sql & N & "   ,SENSINCD "         '線心数コード
            sql = sql & N & "   ,SIZECD "           'サイズコード
            sql = sql & N & "   ,COLORCD "          '色コード
            sql = sql & N & "   ,UPDNAME "          '端末ID
            sql = sql & N & "   ,UPDDATE) "         '更新日時
            sql = sql & N & " SELECT "
            sql = sql & N & "    TT_TENKAIPTN "
            sql = sql & N & "   ,TT_JUYOUCD "
            sql = sql & N & "   ,TT_HINSYUKBN "
            sql = sql & N & "   ,TT_KHINMEICD "
            sql = sql & N & "   ,TT_HINMEI "
            sql = sql & N & "   ,TT_TANCYO "
            sql = sql & N & "   ,TT_H_SIYOU_CD "
            sql = sql & N & "   ,TT_H_HIN_CD "
            sql = sql & N & "   ,TT_H_SENSIN_CD "
            sql = sql & N & "   ,TT_H_SIZE_CD "
            sql = sql & N & "   ,TT_H_COLOR_CD "
            sql = sql & N & "   ,'" & _tanmatuID & "'"
            sql = sql & N & "   ,TO_DATE('" & updateDate & "', 'YYYY/MM/DD HH24:MI:SS') "
            sql = sql & N & "    FROM M11KEIKAKUHIN"
            sql = sql & N & " WHERE M11KEIKAKUHIN.TT_TENKAIPTN = '2'"
            If Not "".Equals(prmSql) Then
                sql = sql & N & prmSql
            End If
            _db.executeDB(sql)

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '　ワークテーブル作成
    '　(処理概要)ワークテーブルを作成(update)。
    '------------------------------------------------------------------------------------------------------
    Private Sub createWK01()
        Try

            'M01汎用マスタ
            Dim sql As String = ""
            sql = sql & N & " UPDATE ZG320E_W10 ZG SET ( "
            sql = sql & N & "    JUYOUNM "
            sql = sql & N & "   ,JUYOSORT) = ("
            sql = sql & N & "       SELECT  "
            sql = sql & N & "           NAME2, "
            sql = sql & N & "           SORT"
            sql = sql & N & "           FROM M01HANYO M "
            sql = sql & N & "           WHERE ZG.JUYOUCD = M.KAHENKEY "
            sql = sql & N & "           AND M.KOTEIKEY = '" & COTEI_JUYOU & "') "
            sql = sql & N & "   WHERE (JUYOUCD) = (SELECT"
            sql = sql & N & "           KAHENKEY "
            sql = sql & N & "           FROM M01HANYO M "
            sql = sql & N & "           WHERE ZG.JUYOUCD = M.KAHENKEY "
            sql = sql & N & "           AND M.KOTEIKEY = '" & COTEI_JUYOU & "'"
            sql = sql & N & "           AND ZG.UPDNAME = '" & _tanmatuID & "')"
            _db.executeDB(sql)

            'M01汎用マスタ
            sql = ""
            sql = sql & N & " UPDATE ZG320E_W10 ZG SET ( "
            sql = sql & N & "    STENKAINM "
            sql = sql & N & "   ,STENKAISORT) = ( "
            sql = sql & N & "       SELECT "
            sql = sql & N & "           NAME1, "
            sql = sql & N & "           SORT "
            sql = sql & N & "           FROM M01HANYO M "
            sql = sql & N & "           WHERE ZG.STENKAIPTN = M.KAHENKEY "
            sql = sql & N & "           AND M.KOTEIKEY = '" & COTEI_STENKAI & "') "
            sql = sql & N & "   WHERE STENKAIPTN = (SELECT "
            sql = sql & N & "           KAHENKEY "
            sql = sql & N & "           FROM M01HANYO M "
            sql = sql & N & "           WHERE ZG.STENKAIPTN = M.KAHENKEY "
            sql = sql & N & "           AND M.KOTEIKEY = '" & COTEI_STENKAI & "'"
            sql = sql & N & "           AND ZG.UPDNAME = '" & _tanmatuID & "')"
            _db.executeDB(sql)

            'M02品種区分マスタ
            sql = ""
            sql = sql & N & " UPDATE ZG320E_W10 ZG SET "
            sql = sql & N & "   ZG.HINSYUKBNNM = ( "
            sql = sql & N & "       SELECT "
            sql = sql & N & "           M.HINSYUKBNNM "
            sql = sql & N & "           FROM M02HINSYUKBN M "
            sql = sql & N & "           WHERE ZG.HINSYUKBN = M.HINSYUKBN "
            sql = sql & N & "           AND ZG.JUYOUCD = M.JUYOUCD) "
            sql = sql & N & "   WHERE (ZG.HINSYUKBN, ZG.JUYOUCD) = (SELECT "
            sql = sql & N & "           HINSYUKBN,"
            sql = sql & N & "           JUYOUCD "
            sql = sql & N & "           FROM M02HINSYUKBN M "
            sql = sql & N & "           WHERE ZG.HINSYUKBN = M.HINSYUKBN "
            sql = sql & N & "           AND ZG.JUYOUCD = M.JUYOUCD "
            sql = sql & N & "           AND ZG.UPDNAME = '" & _tanmatuID & "')"
            _db.executeDB(sql)

            'T12品名別販売計画テーブル
            sql = ""
            sql = sql & N & " UPDATE ZG320E_W10 ZG SET ("
            sql = sql & N & "   ZG.THANBAIRYOU, "
            sql = sql & N & "   ZG.YHANBAIRYOU, "
            sql = sql & N & "   ZG.YYHANBAIRYOU ) = ("
            sql = sql & N & "       SELECT "
            sql = sql & N & "           T.THANBAIRYOU, "
            sql = sql & N & "           T.YHANBAIRYOU, "
            sql = sql & N & "           T.YYHANBAIRYOU"
            sql = sql & N & "           FROM T12HINMEIHANK T "
            sql = sql & N & "           WHERE ZG.KHINMEICD = T.KHINMEICD) "
            sql = sql & N & "   WHERE ZG.KHINMEICD = ( SELECT "
            sql = sql & N & "           T.KHINMEICD "
            sql = sql & N & "           FROM T12HINMEIHANK T "
            sql = sql & N & "           WHERE ZG.KHINMEICD = T.KHINMEICD "
            sql = sql & N & "           AND ZG.UPDNAME = '" & _tanmatuID & "')"
            _db.executeDB(sql)

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
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
            sql = sql & N & "    STENKAINM " & COLDT_STENKAINM          'サイズ展開
            sql = sql & N & "   ,JUYOUCD " & COLDT_JUYOUCD              '需要先
            sql = sql & N & "   ,JUYOUNM " & COLDT_JUYOUNAME            '需要先名
            sql = sql & N & "   ,HINSYUKBN " & COLDT_HINSYUKBN          '品種区分
            sql = sql & N & "   ,HINSYUKBNNM " & COLDT_HINSYUKBNNM      '品種区分名
            sql = sql & N & "   ,KHINMEICD " & COLDT_HINMEICD           '品名コード
            sql = sql & N & "   ,HINMEI " & COLDT_HINMEI                '品名
            sql = sql & N & "   ,TANCYO " & COLDT_TANCYO                '単長
            sql = sql & N & "   ,THANBAIRYOU " & COLDT_THANBAI          '当月販売計画   
            sql = sql & N & "   ,YHANBAIRYOU " & COLDT_YHANBAI          '翌月販売計画
            sql = sql & N & "   ,YYHANBAIRYOU " & COLDT_YYHANBAI        '翌々月販売計画
            sql = sql & N & "   ,JUYOSORT " & COLDT_JUYOUSORT           '需要先表示順
            sql = sql & N & "   ,STENKAISORT " & COLDT_STENKAISORT      'サイズ展開表示順
            sql = sql & N & "   ,UPDNAME " & COLDT_UPDNAME              '端末ID
            sql = sql & N & " FROM ZG320E_W10 "
            sql = sql & N & "   WHERE UPDNAME = '" & _tanmatuID & "'"
            sql = sql & N & " ORDER BY "
            If "".Equals(prmSort) Then
                '-->2010.12.25 chg by takagi #47
                'sql = sql & N & " STENKAISORT , JUYOSORT, HINSYUKBN, HINSYUCD, SENSINCD, SIZECD, COLORCD, SIYOUCD "
                sql = sql & N & " HINSYUCD, SENSINCD, SIZECD, SIYOUCD, COLORCD "
                '<--2010.12.25 chg by takagi #47
            Else
                sql = sql & N & " " & prmSort
            End If

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                '一覧のクリア
                Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvKHanbai)
                gh.clearRow()

                '件数のクリア
                lblKensu.Text = 0 & "件"

                '合計のクリア
                lblTKei.Text = ""
                lblYKei.Text = ""
                lblYYKei.Text = ""

                '登録・EXCELボタン使用不可
                btnTouroku.Enabled = False
                btnExcel.Enabled = False
                Throw New UsrDefException("対象データがありません。", _msgHd.getMSG("NonData"))
            Else
                '抽出データがある場合、登録ボタン・EXCELボタンを有効にする
                btnTouroku.Enabled = _updFlg
                '-->2010.12.22 chg by takagi
                'btnExcel.Enabled = _updFlg
                btnExcel.Enabled = True
                '<--2010.12.22 chg by takagi
            End If

            '抽出データを一覧に表示する
            dgvKHanbai.DataSource = ds
            dgvKHanbai.DataMember = RS

            '一覧の件数を表示する
            lblKensu.Text = CStr(iRecCnt) & "件"

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
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
    '　需要先コンボボックス・サイズ別展開のセット
    '　(処理概要)M01汎用マスタから需要先を抽出して表示する。
    '-------------------------------------------------------------------------------
    Private Sub setJuyouCbo()
        Try

            'コンボボックス
            Dim sql = ""
            sql = sql & N & " SELECT KAHENKEY KAHEN, NAME2 JUYOUSAKI "
            sql = sql & N & " FROM M01HANYO "
            sql = sql & N & " WHERE KOTEIKEY = '" & COTEI_JUYOU & "' "
            sql = sql & N & " ORDER BY KAHENKEY "

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                btnTouroku.Enabled = False
                btnExcel.Enabled = False
                Throw New UsrDefException("汎用マスタの値の取得に失敗しました。", _msgHd.getMSG("noHanyouMst"))
            End If

            'コンボボックスクリア
            Me.cboJuyou.Items.Clear()
            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(cboJuyou)

            '先頭に空行
            ch.addItem(New UtilCboVO("", ""))

            'ループさせてコンボボックスにセット
            For i As Integer = 0 To iRecCnt - 1
                ch.addItem(New UtilCboVO(ds.Tables(RS).Rows(i)("KAHEN").ToString, ds.Tables(RS).Rows(i)("JUYOUSAKI").ToString))
            Next

            'サイズ別展開
            sql = ""
            sql = sql & N & " SELECT NAME1 NAME FROM M01HANYO WHERE KOTEIKEY = '" & COTEI_STENKAI & "' AND KAHENKEY = '2'"
            Dim ds2 As DataSet = _db.selectDB(sql, RS, iRecCnt)
            If iRecCnt > 0 Then
                lblSTenkai.Text = ds2.Tables(RS).Rows(0)("NAME").ToString
            End If

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
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvKHanbai)

            'トランザクション開始
            _db.beginTran()

            '行数分だけループ
            For i As Integer = 0 To gh.getMaxRow - 1
                sql = ""
                sql = sql & N & " UPDATE ZG320E_W10 SET "

                ''2011/01/25 CHG-S Sugano #94
                'sql = sql & N & " THANBAIRYOU = TO_NUMBER('" & _db.rmNullDouble(dgvKHanbai(COLNO_THANBAI, i).Value) & "') "     '当月販売計画
                'sql = sql & N & " ,YHANBAIRYOU = TO_NUMBER('" & _db.rmNullDouble(dgvKHanbai(COLNO_YHANBAI, i).Value) & "') "    '翌月販売計画
                'sql = sql & N & " ,YYHANBAIRYOU = TO_NUMBER('" & _db.rmNullDouble(dgvKHanbai(COLNO_YYHANBAI, i).Value) & "') "  '翌々月販売計画
                If _db.rmNullStr(dgvKHanbai(COLNO_THANBAI, i).Value).Equals("") Then
                    sql = sql & N & " THANBAIRYOU = NULL "     '当月販売計画
                Else
                    sql = sql & N & " THANBAIRYOU = TO_NUMBER('" & _db.rmNullDouble(dgvKHanbai(COLNO_THANBAI, i).Value) & "') "     '当月販売計画
                End If
                If _db.rmNullStr(dgvKHanbai(COLNO_THANBAI, i).Value).Equals("") Then
                    sql = sql & N & ",YHANBAIRYOU = NULL "     '翌月販売計画
                Else
                    sql = sql & N & ",YHANBAIRYOU = TO_NUMBER('" & _db.rmNullDouble(dgvKHanbai(COLNO_YHANBAI, i).Value) & "') "     '翌月販売計画
                End If
                If _db.rmNullStr(dgvKHanbai(COLNO_THANBAI, i).Value).Equals("") Then
                    sql = sql & N & ",YYHANBAIRYOU = NULL "     '翌々月販売計画
                Else
                    sql = sql & N & ",YYHANBAIRYOU = TO_NUMBER('" & _db.rmNullDouble(dgvKHanbai(COLNO_YYHANBAI, i).Value) & "') "     '翌々月販売計画
                End If
                ''2011/01/25 CHG-E Sugano #94
                sql = sql & N & "   WHERE UPDNAME = '" & _tanmatuID & "'"
                sql = sql & N & "   AND KHINMEICD = '" & dgvKHanbai(COLNO_HINMEICD, i).Value & "'"
                _db.executeDB(sql)
            Next

            'トランザクション終了
            _db.commitTran()

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        Finally
            If _db.isTransactionOpen = True Then
                _db.rollbackTran()                          'ロールバック
            End If
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　DB更新
    '　(処理概要)ワークテーブルの値をT12に登録する
    '-------------------------------------------------------------------------------
    Private Sub registT12()
        Try

            'T12削除
            Dim delCnt As Integer = 0           '削除レコード数
            Dim sql As String = ""
            sql = " DELETE FROM T12HINMEIHANK T12 "
            sql = sql & N & " WHERE EXISTS "
            sql = sql & N & "   (SELECT * FROM ZG320E_W10 WK "
            sql = sql & N & "       WHERE T12.KHINMEICD = WK.KHINMEICD "
            sql = sql & N & " AND UPDNAME = '" & _tanmatuID & "')"
            _db.executeDB(sql, delCnt)

            '更新日時を取得
            Dim updStartDate As Date = Now
            ''2011/01/25 ADD-S Sugano #94
            Dim updCnt As Integer = 0
            ''2011/01/25 ADD-E Sugano #94

            'ワークテーブルの値をT12に登録
            sql = ""
            sql = sql & N & " INSERT INTO T12HINMEIHANK ( "
            sql = sql & N & "       KHINMEICD "         '品名コード
            sql = sql & N & "      ,TENKAIPTN "         'サイズ展開パターン
            sql = sql & N & "      ,THANBAIRYOU "       '当月販売計画
            sql = sql & N & "      ,YHANBAIRYOU "       '翌月販売計画
            sql = sql & N & "      ,YYHANBAIRYOU "      '翌々月販売計画
            sql = sql & N & "      ,UPDNAME "           '端末ID
            sql = sql & N & "      ,UPDDATE )"          '更新日時
            sql = sql & N & " SELECT "
            sql = sql & N & "       KHINMEICD "         '品名コード
            sql = sql & N & "      ,STENKAIPTN "        'サイズ展開パターン
            sql = sql & N & "      ,THANBAIRYOU "       '当月販売計画
            sql = sql & N & "      ,YHANBAIRYOU "       '翌月販売計画
            sql = sql & N & "      ,YYHANBAIRYOU "      '翌々月販売計画
            sql = sql & N & "      ,UPDNAME "           '端末ID
            sql = sql & N & "      ,TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS')"
            sql = sql & N & "   FROM ZG320E_W10 "
            sql = sql & N & "       WHERE UPDNAME = '" & _tanmatuID & "'"
            ''2011/01/25 ADD-S Sugano #94
            'NULLのデータは追加しない
            sql = sql & N & "       AND THANBAIRYOU IS NOT NULL"
            sql = sql & N & "       AND YHANBAIRYOU IS NOT NULL"
            sql = sql & N & "       AND YYHANBAIRYOU IS NOT NULL"
            ''2011/01/25 ADD-E Sugano #94

            ''2011/01/25 ADD-S Sugano #94
            '_db.executeDB(sql)
            _db.executeDB(sql, updCnt)
            ''2011/01/25 ADD-E Sugano #94

            '更新終了日時を取得
            Dim updFinDate As Date = Now

            '更新件数の取得
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvKHanbai)        'DGVハンドラの設定
            ''2011/01/25 DEL-S Sugano #94
            'Dim updCnt As Integer = gh.getMaxRow
            ''2011/01/25 DEL-E Sugano #94

            '処理日時・計画日時取得
            Dim syoriDate As String = lblSyori.Text.Substring(0, 4) & lblSyori.Text.Substring(5, 2)
            Dim keikakuDate As String = lblKeikaku.Text.Substring(0, 4) & lblKeikaku.Text.Substring(5, 2)

            '実行履歴登録処理
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

    '' 2011/01/25 ADD-S Sugano #94
    '-------------------------------------------------------------------------------
    '　未登録データ件数取得処理
    '　(処理概要)サイズ展開パターンが「個別」の品名のうち、個別販売計画入力が
    '　　　　　　未登録のレコード件数を取得する。
    '　　O　：　prmRefRecCount     未登録データのレコード件数
    '-------------------------------------------------------------------------------
    Private Sub getMitourokuCount(ByRef prmRefRecCount As Integer)

        Try
            prmRefRecCount = 0

            '未登録データのレコード件数を取得する。
            Dim sql = ""
            sql = sql & N & " SELECT COUNT(*) RECCNT FROM M11KEIKAKUHIN M11 "
            sql = sql & N & " LEFT JOIN T12HINMEIHANK T12 "
            sql = sql & N & " ON M11.TT_KHINMEICD = T12.KHINMEICD "
            sql = sql & N & " WHERE M11.TT_TENKAIPTN = '2' "
            sql = sql & N & " AND "
            sql = sql & N & " T12.KHINMEICD IS NULL "   'T12にレコードが存在しないもの

            'SQL発行
            Dim ds As DataSet = _db.selectDB(sql, RS)

            prmRefRecCount = _db.rmNullInt(ds.Tables(RS).Rows(0)("RECCNT"))

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub
    '' 2011/01/25 ADD-E Sugano #94
#End Region

#Region "ユーザ定義関数:チェック処理"

    '------------------------------------------------------------------------------------------------------
    '  登録チェック
    '　(処理概要)各項目の必須項目チェックを行う
    '------------------------------------------------------------------------------------------------------
    Private Sub checkTouroku()
        Try

            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvKHanbai)

            For i As Integer = 0 To gh.getMaxRow - 1

                '-->2010.12.17 add by takagi #4
                If (Not "".Equals(gh.getCellData(COLDT_THANBAI, i))) OrElse _
                   (Not "".Equals(gh.getCellData(COLDT_YHANBAI, i))) OrElse _
                   (Not "".Equals(gh.getCellData(COLDT_YYHANBAI, i))) Then
                    '<--2010.12.17 add by takagi #4

                    '必須入力チェック
                    '工程コード
                    Call checkHissuKeta(COLDT_THANBAI, "当月販売計画", i, COLNO_THANBAI)
                    '機械名
                    Call checkHissuKeta(COLDT_YHANBAI, "翌月販売計画", i, COLNO_YHANBAI)
                    '通常稼働時間
                    Call checkHissuKeta(COLDT_YYHANBAI, "翌々月販売計画", i, COLNO_YYHANBAI)

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
    '  必須入力・入力桁チェック
    '　(処理概要)セルが入力されているか・整数部が4桁までかチェックする
    '　　I　：　prmColName              チェックするセルの列名
    '　　I　：　prmColHeaderName        エラー時に表示する列名
    '　　I　：　prmCnt                  チェックするセルの行数
    '　　I　：　prmColNo                チェックするセルの列数
    '------------------------------------------------------------------------------------------------------
    Private Sub checkHissuKeta(ByVal prmColName As String, ByVal prmColHeaderName As String, ByVal prmCnt As Integer, ByVal prmColNo As Integer)
        Try
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvKHanbai)

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

            '桁数チェックチェック
            If CInt(gh.getCellData(prmColName, prmCnt).ToString) >= 10000 Then  '整数部は4桁まで登録可
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