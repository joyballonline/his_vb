'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）負荷山積集計結果確認(明細)
'    （フォームID）ZG731Q_FukaYamadumiMeisai
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   鈴木        2010/11/29                 新規              
'　(2)   菅野        2011/01/25                 変更　項目変更（製作区分→手配区分）#91              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView
Imports UtilMDL.FileDirectory

Public Class ZG731Q_FukaYamadumiMeisai
    Inherits System.Windows.Forms.Form

#Region "リテラル値定義"

    'PG制御文字
    Private Const RS As String = "RecSet"                       'データセットテーブル名
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const T As String = ControlChars.Tab                '区切文字

    '一覧データバインド列名
    Private Const COLDT_FUKA As String = "dtFuka"                   '負荷区分
    Private Const COLDT_TEHAINO As String = "dtTehaiNo"             '手配No
    Private Const COLDT_SEIBAN As String = "dtSeiban"               '製番
    Private Const COLDT_HINMEI As String = "dtHinmei"               '品名
    '' 2011/01/25 CHG-S Sugano #91
    'Private Const COLDT_SEISAKUKUBUN As String = "dtSeisakuKbn"     '製作区分
    Private Const COLDT_TEHAIKUBUN As String = "dtTehaiKbn"         '手配区分
    '' 2011/01/25 CHG-E Sugano #91
    Private Const COLDT_KIBOUSYUTTAIBI As String = "dtKibou"        '希望出来日
    Private Const COLDT_KOUTEITYAKUSYUBI As String = "dtKoutei"      '工程着手日
    Private Const COLDT_MCH As String = "dtMCH"                     'MCH
    Private Const COLDT_MH As String = "dtMH"                       'MH
    Private Const COLDT_RUIKEIMCH As String = "dtRuikeiMCH"         '累計MCH
    Private Const COLDT_RUIKEIMH As String = "dtRuikeiMH"           '累計MH

    '一覧グリッド列名
    Private Const COLCN_FUKA As String = "cnFuka"                   '負荷区分
    Private Const COLCN_TEHAINO As String = "cnTehaiNo"             '手配No
    Private Const COLCN_SEIBAN As String = "cnSeiban"               '製番
    Private Const COLCN_HINMEI As String = "cnHinmei"               '品名
    '' 2011/01/25 CHG-S Sugano #91
    'Private Const COLCN_SEISAKUKUBUN As String = "cnSeisakuKbn"     '製作区分
    Private Const COLCN_TEHAIKUBUN As String = "cnTehaiKbn"         '手配区分
    '' 2011/01/25 CHG-E Sugano #91
    Private Const COLCN_KIBOUSYUTTAIBI As String = "cnKibou"        '希望出来日
    Private Const COLCN_KOUTEITYAKUSYUBI As String = "cnKoutei"      '工程着手日
    Private Const COLCN_MCH As String = "cnMCH"                     'MCH
    Private Const COLCN_MH As String = "cnMH"                       'MH
    Private Const COLCN_RUIKEIMCH As String = "cnRuikeiMCH"         '累計MCH
    Private Const COLCN_RUIKEIMH As String = "cnRuikeiMH"           '累計MH

    Private Const FUKAKBN As String = "14"                          '負荷区分の固定キー
    '' 2011/01/25 CHG-S Sugano #91
    'Private Const SEISANKBN As String = "03"                        '製作区分の固定キー
    Private Const TEHAIKBN As String = "02"                         '手配区分の固定キー
    '' 2011/01/25 CHG-E Sugano #91

#End Region

#Region "メンバ変数定義"

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As Form

    Private _oldRowIndex As Integer = -1            '選択行の背景色を変更するためのフラグを宣言
    Private _colorCtlFlg As Boolean = False         '選択行の背景色を変更するためのフラグを宣言

    Private _syoriDate As String                    '処理年月
    Private _keikakuDate As String                  '計画年月

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
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmForm As Form, _
                    ByVal prmKikaimei As String, ByVal prmSeisanMch As String, ByVal prmYamadumiMch As String, ByVal prmOverMch As String, ByVal prmYamadumiMh As String, _
                    ByVal prmSyoriDate As String, ByVal prmKeikakuDate As String)

        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _parentForm = prmForm                                               '親フォーム
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示

        '前画面からの受け渡し値をラベルにセット
        lblMachine.Text = prmKikaimei
        If String.Empty.Equals(prmSeisanMch) Then
            lblSNouryoku.Text = String.Empty
        Else
            lblSNouryoku.Text = Format(CDec(prmSeisanMch), "#,##0.0")
        End If
        If String.Empty.Equals(prmYamadumiMch) Then
            lblYamadumiMCH.Text = String.Empty
        Else
            lblYamadumiMCH.Text = Format(CDec(prmYamadumiMch), "#,##0.0")
        End If
        If String.Empty.Equals(prmOverMch) Then
            lblOverMCH.Text = String.Empty
        Else
            lblOverMCH.Text = Format(CDec(prmOverMch), "#,##0.0")
        End If
        If String.Empty.Equals(prmYamadumiMh) Then
            lblYamadumiMH.Text = String.Empty
        Else
            lblYamadumiMH.Text = Format(CDec(prmYamadumiMh), "#,##0.0")
        End If

        '前画面の処理年月、計画年月をメンバ変数にセット
        _syoriDate = prmSyoriDate
        _keikakuDate = prmKeikakuDate

    End Sub

#End Region

#Region "Formイベント"


    '-------------------------------------------------------------------------------
    '　処理開始イベント
    '-------------------------------------------------------------------------------
    Private Sub ZG731Q_FukaYamadumiMeisai_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            '描画関係の設定
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           'サイズが変更されたときに、コントロールがコントロール自体を再描画するかどうかを示す値を設定
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '描画はバッファで実行され、完了後に、結果が画面に出力されるよう設定
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  'コントロールは、画面に直接ではなく、まずバッファに描画されます。これにより、ちらつきを抑えることができます。
            Me.SetStyle(ControlStyles.UserPaint, True)              'コントロールは、オペレーティング システムによってではなく、独自に描画されるよう設定
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   'コントロールはウィンドウ メッセージ WM_ERASEBKGND を無視するように設定

            Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr 'タイトルオプション表示

            '初期値設定
            Call dispDGV()
            _colorCtlFlg = True

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

        '自画面を終了し、メニュー画面に戻る。
        _parentForm.Show()
        _parentForm.Activate()

        Me.Close()

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

#Region "ユーザ定義イベント"

    '------------------------------------------------------------------------------------------------------
    '検索処理
    '   （処理概要）　検索処理を行ない、一覧にデータを表示する。
    '------------------------------------------------------------------------------------------------------
    Private Sub dispDGV()

        Dim c As Cursor = Me.Cursor
        Me.Cursor = Cursors.WaitCursor
        lblKensuu.Text = "0件"
        btnExcel.Enabled = False

        Try
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & "  M01F.NAME1 " & COLDT_FUKA
            sql = sql & N & " ,T62.TEHAINO " & COLDT_TEHAINO
            sql = sql & N & " ,T62.SEIBAN " & COLDT_SEIBAN
            sql = sql & N & " ,T62.HINMEI " & COLDT_HINMEI
            '' 2011/01/25 CHG-S Sugano #91
            'sql = sql & N & " ,M01S.NAME1 " & COLDT_SEISAKUKUBUN
            sql = sql & N & " ,M01S.NAME1 " & COLDT_TEHAIKUBUN
            '' 2011/01/25 CHG-E Sugano #91
            sql = sql & N & " ,DECODE(T62.SYUTTAIBI,NULL,NULL,SUBSTR(T62.SYUTTAIBI,3,2) || '/' || SUBSTR(T62.SYUTTAIBI,5,2) || '/' || SUBSTR(T62.SYUTTAIBI,7,2)) " & COLDT_KIBOUSYUTTAIBI
            sql = sql & N & " ,DECODE(T62.TYAKUSYUBI,NULL,NULL,SUBSTR(T62.TYAKUSYUBI,3,2) || '/' || SUBSTR(T62.TYAKUSYUBI,5,2) || '/' || SUBSTR(T62.TYAKUSYUBI,7,2)) " & COLDT_KOUTEITYAKUSYUBI
            sql = sql & N & " ,T62.MCH " & COLDT_MCH
            sql = sql & N & " ,T62.MH " & COLDT_MH
            sql = sql & N & " ,0.0 " & COLDT_RUIKEIMCH
            sql = sql & N & " ,0.0 " & COLDT_RUIKEIMH
            sql = sql & N & " FROM T62FUKAMEISAI T62 "
            sql = sql & N & " LEFT OUTER JOIN (SELECT NAME1,KAHENKEY FROM M01HANYO WHERE KOTEIKEY = '" & FUKAKBN & "') M01F "
            sql = sql & N & " ON M01F.KAHENKEY = T62.FUKAKBN "
            '' 2011/01/25 CHG-S Sugano #91
            'sql = sql & N & " LEFT OUTER JOIN (SELECT NAME1,KAHENKEY FROM M01HANYO WHERE KOTEIKEY = '" & SEISANKBN & "') M01S "
            'sql = sql & N & " ON M01S.KAHENKEY = T62.SEISAKUKBN "
            sql = sql & N & " LEFT OUTER JOIN (SELECT NAME1,KAHENKEY FROM M01HANYO WHERE KOTEIKEY = '" & TEHAIKBN & "') M01S "
            sql = sql & N & " ON M01S.KAHENKEY = T62.TEHAIKBN "
            '' 2011/01/25 CHG-E Sugano #91

            '' 2011/01/25 CHG-S Sugano #91
            'sql = sql & N & " WHERE T62.NAME ='" & _db.rmSQ(_db.rmNullStr(lblMachine.Text)) & "' "
            sql = sql & N & " WHERE T62.KIKAIMEI ='" & _db.rmSQ(_db.rmNullStr(lblMachine.Text)) & "' "
            '' 2011/01/25 CHG-E Sugano #91
            sql = sql & N & " ORDER BY T62.FUKAKBN,T62.SYUTTAIBI,T62.TEHAINO "

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then            '抽出レコードが１件もない場合
                Throw New UsrDefException("対象データがありません。", _msgHd.getMSG("NonData"))
            End If

            '0件より大きい場合はEXCELボタン押下可
            btnExcel.Enabled = True

            '累計値をセットする
            Dim rukeiMCH As Decimal = 0.0 '累計MCH
            Dim rukeiMH As Decimal = 0.0  '累計MH
            For i As Integer = 0 To iRecCnt - 1
                rukeiMCH = rukeiMCH + CDec(_db.rmNullStr(ds.Tables(RS).Rows(i)(COLDT_MCH)))
                rukeiMH = rukeiMH + CDec(_db.rmNullStr(ds.Tables(RS).Rows(i)(COLDT_MH)))
                ds.Tables(RS).Rows(i).Item(COLDT_RUIKEIMCH) = rukeiMCH
                ds.Tables(RS).Rows(i).Item(COLDT_RUIKEIMH) = rukeiMH
            Next

            '抽出データを一覧に表示する
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvFukaMeisai)
            gh.clearRow()
            dgvFukaMeisai.DataSource = ds
            dgvFukaMeisai.DataMember = RS

            lblKensuu.Text = CStr(iRecCnt) & "件"

            '一覧先頭行選択
            dgvFukaMeisai.Focus()
            gh.setSelectionRowColor(0, 0, StartUp.lCOLOR_BLUE)

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        Finally
            Me.Cursor = c
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '選択行に着色する処理
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvFukaMeisai_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvFukaMeisai.SelectionChanged
        If _colorCtlFlg Then
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvFukaMeisai)
            gh.setSelectionRowColor(dgvFukaMeisai.CurrentCellAddress.Y, _oldRowIndex, StartUp.lCOLOR_BLUE)
        End If
        _oldRowIndex = dgvFukaMeisai.CurrentCellAddress.Y
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　EXCEL出力処理
    '   （処理概要）　画面表示情報を機械別品名別負荷山積集計表を出力する。
    '------------------------------------------------------------------------------------------------------
    Private Sub printExcel()

        Try
            '雛形ファイル
            Dim openFilePath As String = StartUp.iniValue.BaseXlsPath & "\" & StartUp.iniValue.ExcelZG731R1_Base
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
            Dim wkEditFile As String = StartUp.iniValue.OutXlsPath & "\" & StartUp.iniValue.ExcelZG731R1_Out     'コピー先ファイル

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
                    Dim startPrintRow As Integer = 7    '出力開始行数
                    Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvFukaMeisai)        'DGVハンドラの設定
                    Dim rowCnt As Integer = gh.getMaxRow    'データグリッド最終行
                    Dim xlsi As Integer = 0                 'エクセルファイルの書き込み行
                    Dim sLine As Integer = startPrintRow    '集計開始行
                    Dim eLine As Integer = startPrintRow    '集計終了行
                    Dim syuukeiFlg As Boolean = False       '集計判定を行うフラグ
                    Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder

                    For i As Integer = 0 To rowCnt - 1

                        If i = rowCnt - 1 Then
                            '最終行の場合集計フラグON
                            syuukeiFlg = True
                        ElseIf _db.rmNullStr(dgvFukaMeisai(COLCN_FUKA, i).Value).Equals(_db.rmNullStr(dgvFukaMeisai(COLCN_FUKA, i + 1).Value)) = False Then
                            '現工程と次工程が異なる場合も集計フラグON
                            syuukeiFlg = True
                        End If

                        '一覧データ出力
                        sb.Append(_db.rmNullStr(dgvFukaMeisai(COLCN_FUKA, i).Value) & T)              '負荷区分
                        sb.Append(_db.rmNullStr(dgvFukaMeisai(COLCN_TEHAINO, i).Value) & T)           '手配No
                        sb.Append(_db.rmNullStr(dgvFukaMeisai(COLCN_SEIBAN, i).Value) & T)            '製番
                        sb.Append(_db.rmNullStr(dgvFukaMeisai(COLCN_HINMEI, i).Value) & T & T & T)    '品名
                        '' 2011/01/25 CHG-S Sugano #91
                        'sb.Append(_db.rmNullStr(dgvFukaMeisai(COLCN_SEISAKUKUBUN, i).Value) & T)      '製作区分
                        sb.Append(_db.rmNullStr(dgvFukaMeisai(COLCN_TEHAIKUBUN, i).Value) & T)        '手配区分
                        '' 2011/01/25 CHG-E Sugano #91
                        sb.Append(_db.rmNullStr(dgvFukaMeisai(COLCN_KIBOUSYUTTAIBI, i).Value) & T)    '希望出来日
                        sb.Append(_db.rmNullStr(dgvFukaMeisai(COLCN_KOUTEITYAKUSYUBI, i).Value) & T)  '工程着手日
                        sb.Append(_db.rmNullStr(dgvFukaMeisai(COLCN_MCH, i).Value) & T)               'MCH
                        sb.Append(_db.rmNullStr(dgvFukaMeisai(COLCN_MH, i).Value) & T)                'MH
                        sb.Append(_db.rmNullStr(dgvFukaMeisai(COLCN_RUIKEIMCH, i).Value) & T)         '累計MCH
                        sb.Append(_db.rmNullStr(dgvFukaMeisai(COLCN_RUIKEIMH, i).Value) & N)          '累計MH

                        If syuukeiFlg = True Then
                            '集計終了行の設定
                            eLine = startPrintRow + xlsi

                            '行を1行追加
                            xlsi += 1

                            '集計行の挿入
                            sb.Append("★小計" & T)            '負荷区分
                            sb.Append("-" & T)                 '手配No
                            sb.Append("-" & T)                 '製番
                            sb.Append("-" & T & T & T)         '品名
                            sb.Append("-" & T)                 '製作区分→手配区分 (2011/01/25 CHG Sugano #91)
                            sb.Append("-" & T)                 '希望出来日
                            sb.Append("-" & T)                 '工程着手日
                            sb.Append("=subtotal(9,J" & sLine & ":J" & eLine & ")" & T) 'MCH
                            sb.Append("=subtotal(9,K" & sLine & ":K" & eLine & ")" & T) 'MH
                            sb.Append("-" & T)                 '累計MCH
                            sb.Append("-" & N)                 '累計MH

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
                    eh.setValue("作成日時 ： " & printDate, 1, 11)   'K1:

                    '処理年月、計画年月編集
                    eh.setValue("処理年月：" & _syoriDate & "　　計画年月：" & _keikakuDate, 1, 5)    'E1

                    'ヘッダを表示する
                    eh.setValue(_db.rmNullStr(lblMachine.Text), 4, 2)       'B4
                    eh.setValue(_db.rmNullStr(lblSNouryoku.Text), 4, 4)     'D4
                    eh.setValue(_db.rmNullStr(lblYamadumiMCH.Text), 4, 6)   'F4
                    eh.setValue(_db.rmNullStr(lblOverMCH.Text), 4, 9)       'I4
                    eh.setValue(_db.rmNullStr(lblYamadumiMH.Text), 4, 12)   'L4

                    '左上のセルにフォーカス当てる
                    eh.selectCell(startPrintRow, 1)     'A7

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

#End Region

End Class