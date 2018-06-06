'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）計画対象品マスタメンテ新規登録画面
'    （フォームID）ZM110E_Sinki
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   中澤        2010/09/03                 新規
'　(2)   菅野        2014/06/04                 変更　材料票マスタ（MPESEKKEI）テーブル変更対応            
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.DataGridView

Public Class ZM121S_SyuukeiTouroku
    Inherits System.Windows.Forms.Form

#Region "リテラル値定義"
    '-------------------------------------------------------------------------------
    '   定数定義
    '-------------------------------------------------------------------------------

    'PG制御文字 
    Private Const N As String = ControlChars.NewLine            '改行文字
    Private Const RS As String = "RecSet"                       'レコードセットテーブル
    Private Const RS2 As String = "RecSet2"                     'レコードセットテーブル
    Private Const PGID As String = "ZM121S"                     'T91に登録するPGID

    'M12検索用リテラル
    Private Const COLDT_KHINMEICD As String = "dtSHinmeiCD"
    Private Const COLDT_KHINMEI As String = "dtSHinmei"

    '材料票検索用リテラル
    Private Const INT_SEQNO As Integer = 1

#End Region

#Region "メンバー変数宣言"

    '-------------------------------------------------------------------------------
    '   変数定義
    '-------------------------------------------------------------------------------

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As Form

    Private _oldRowIndex As Integer = -1            '選択行の背景色を変更するための変数
    Private _colorCtlFlg As Boolean = False         '選択行の背景色を変更するためのフラグ

    Private _kHinmeiCD As String = ""               '親画面から受け取った計画品名コード

    Private _tanmatuID As String = ""               '端末ID

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
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmForm As Form, ByVal prmKHinmeiCD As String)
        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _parentForm = prmForm                                               '親フォーム
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        _kHinmeiCD = prmKHinmeiCD

    End Sub
#End Region

#Region "Formイベント"

    '-------------------------------------------------------------------------------
    '　処理開始イベント
    '-------------------------------------------------------------------------------
    Private Sub ZM121S_SyuukeiTouroku_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            '描画関係の設定
            Me.SetStyle(ControlStyles.ResizeRedraw, True)           'サイズが変更されたときに、コントロールがコントロール自体を再描画するかどうかを示す値を設定
            Me.SetStyle(ControlStyles.DoubleBuffer, True)           '描画はバッファで実行され、完了後に、結果が画面に出力されるよう設定
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)  'コントロールは、画面に直接ではなく、まずバッファに描画されます。これにより、ちらつきを抑えることができます。
            Me.SetStyle(ControlStyles.UserPaint, True)              'コントロールは、オペレーティング システムによってではなく、独自に描画されるよう設定
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)   'コントロールはウィンドウ メッセージ WM_ERASEBKGND を無視するように設定

            Dim optionStr As String = ComBiz.getFormTitleOption(_db, _msgHd)
            If Not "".Equals(optionStr) Then Me.Text = Me.Text & " - " & optionStr 'タイトルオプション表示

            '端末IDの取得
            _tanmatuID = UtilClass.getComputerName

            '画面表示
            Call dispDGV()

            '一覧着色フラグ
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
    '   追加ボタン押下
    '   (処理概要)入力されたコードの存在チェックを行い、品名と共に一覧に追加する。
    '------------------------------------------------------------------------------------------------------
    Private Sub btnTuika_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTuika.Click

        Try
            '集計対象品名コード入力チェック
            Call checkInputSHinmeiCD()

            '集計対象品名コード重複チェック
            Call checkKHinmeiCDRepeat(txtSSiyo.Text, txtSHinsyu.Text, txtSSensin.Text, txtSSize.Text, txtSColor.Text)

            '重複キーチェック
            Call checkHinmeiCDRepeat()

            _colorCtlFlg = False

            '一覧追加表示
            Call insertDGV()

            _colorCtlFlg = True

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '------------------------------------------------------------------------------------------------------
    '   削除ボタン押下
    '   (処理概要)選択された行を一覧から削除する。
    '------------------------------------------------------------------------------------------------------
    Private Sub btnSakujo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSakujo.Click
        Try
            '削除確認ダイアログ表示
            Dim rtn As DialogResult = _msgHd.dspMSG("confDeleteSTaisyohin")   '選択行を一覧から削除します。よろしいですか？
            If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            '一覧選択行削除
            Call deleteRowDgv()

            '完了メッセージ
            Call _msgHd.dspMSG("completeDelete")          '削除が完了しました。

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　登録ボタン押下
    '　(処理概要)一覧に表示されているコードをM12に登録する。
    '-------------------------------------------------------------------------------
    Private Sub btnKakutei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKakutei.Click
        Try

            '重複キーチェック
            'Call checkHinmeiCDRepeat()

            '登録確認ダイアログ表示
            Dim rtn As DialogResult = _msgHd.dspMSG("confirmInsert")   '登録します。よろしいですか？
            If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            'マウスカーソル砂時計
            Me.Cursor = Cursors.WaitCursor

            'データ追加
            Call insertDB()

            'マウスカーソル矢印
            Me.Cursor = Cursors.Arrow

            '完了メッセージ
            Call _msgHd.dspMSG("completeInsert")          '登録が完了しました。

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

        'トランザクション有効なら解除する
        If _db.isTransactionOpen Then
            Call _db.rollbackTran()
        End If

        '■親フォーム表示
        _parentForm.Show()
        _parentForm.Activate()
        Me.Close()

    End Sub

#End Region

#Region "ユーザ定義関数:画面制御"

    '-------------------------------------------------------------------------------
    ' 　フォーカス移動
    '　（処理概要）検索項目のテキストボックスでエンターキー押下時は次のコントロールへ移動する。
    '-------------------------------------------------------------------------------
    Private Sub txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSSiyo.KeyPress, _
                                                                                                    txtSHinsyu.KeyPress, _
                                                                                                    txtSSensin.KeyPress, _
                                                                                                    txtSSize.KeyPress, _
                                                                                                    txtSColor.KeyPress

        UtilClass.moveNextFocus(Me, e)  '次のコントロールへ移動する

    End Sub

    '-------------------------------------------------------------------------------
    '　コントロール全選択
    '　(処理概要)コントロール移動時に全選択状態にする
    '-------------------------------------------------------------------------------
    Private Sub serch_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSSiyo.GotFocus, _
                                                                                            txtSHinsyu.GotFocus, _
                                                                                            txtSSensin.GotFocus, _
                                                                                            txtSSize.GotFocus, _
                                                                                            txtSColor.GotFocus
        UtilClass.selAll(sender)

    End Sub

#End Region

#Region "ユーザ定義関数:DGV関連"

    '------------------------------------------------------------------------------------------------------
    '選択行に着色する処理
    '------------------------------------------------------------------------------------------------------
    Private Sub dgvSHinmei_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvSHinmei.SelectionChanged
        If _colorCtlFlg Then
            Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSHinmei)
            gh.setSelectionRowColor(dgvSHinmei.CurrentCellAddress.Y, _oldRowIndex, StartUp.lCOLOR_BLUE)
        End If
        _oldRowIndex = dgvSHinmei.CurrentCellAddress.Y
    End Sub

#End Region

#Region "ユーザ定義関数:DB関連"

    '-------------------------------------------------------------------------------
    '　画面表示
    '-------------------------------------------------------------------------------
    Private Sub dispDGV()

        Try

            'M12
            Dim Sql As String = ""
            Sql = Sql & N & "SELECT "
            Sql = Sql & N & " M12.HINMEICD " & COLDT_KHINMEICD
            Sql = Sql & N & ",(MPE.HINSYU_MEI "
            Sql = Sql & N & "		|| MPE.SAIZU_MEI"
            Sql = Sql & N & "		|| MPE.IRO_MEI) " & COLDT_KHINMEI
            Sql = Sql & N & " FROM M12SYUYAKU M12 "
            '2014/06/04 UPD-S Sugano
            'Sql = Sql & N & "	 INNER JOIN MPESEKKEI MPE ON"
            'Sql = Sql & N & "	 M12.KHINMEICD = '" & _db.rmNullStr(_kHinmeiCD) & "'"
            'Sql = Sql & N & " WHERE MPE.SHIYO || "
            'Sql = Sql & N & "       LPAD(TO_CHAR(MPE.HINSYU)  ,3,'0') || "
            'Sql = Sql & N & "       LPAD(TO_CHAR(MPE.SENSHIN)  ,3,'0') || "
            'Sql = Sql & N & "       LPAD(TO_CHAR(MPE.SAIZU)  ,2,'0')  || "
            'Sql = Sql & N & "       LPAD(TO_CHAR(MPE.IRO)  ,3,'0')  = '" & _db.rmNullStr(_kHinmeiCD) & "'"
            'Sql = Sql & N & " AND MPE.SEQ_NO = " & INT_SEQNO
            'Sql = Sql & N & " AND MPE.SEKKEI_HUKA = "
            'Sql = Sql & N & "           (SELECT MAX(SEKKEI_HUKA) FROM MPESEKKEI MPE"
            'Sql = Sql & N & "               WHERE SHIYO || "
            'Sql = Sql & N & "                   LPAD(TO_CHAR(MPE.HINSYU)  ,3,'0') || "
            'Sql = Sql & N & "                   LPAD(TO_CHAR(MPE.SENSHIN)  ,3,'0') || "
            'Sql = Sql & N & "                   LPAD(TO_CHAR(MPE.SAIZU)  ,2,'0')  || "
            'Sql = Sql & N & "                   LPAD(TO_CHAR(MPE.IRO)  ,3,'0')  = '" & _db.rmNullStr(_kHinmeiCD) & "') "
            'Sql = Sql & N & " AND NOT M12.KHINMEICD = M12.HINMEICD "
            Sql = Sql & N & "	 INNER JOIN (SELECT M1.* FROM MPESEKKEI1 M1 "
            Sql = Sql & N & "	             INNER JOIN (SELECT SHIYO,HINSYU,SENSHIN,SAIZU,IRO,SEKKEI_FUKA,MAX(SEKKEI_KAITEI) KAITEI FROM MPESEKKEI1 WHERE SEKKEI_FUKA = 'A' "
            Sql = Sql & N & "	                         GROUP BY SHIYO,HINSYU,SENSHIN,SAIZU,IRO,SEKKEI_FUKA) M2 "
            Sql = Sql & N & "	             ON  M1.SHIYO = M2.SHIYO "
            Sql = Sql & N & "	             AND M1.HINSYU = M2.HINSYU "
            Sql = Sql & N & "	             AND M1.SENSHIN = M2.SENSHIN "
            Sql = Sql & N & "	             AND M1.SAIZU = M2.SAIZU "
            Sql = Sql & N & "	             AND M1.IRO = M2.IRO "
            Sql = Sql & N & "	             AND M1.SEKKEI_FUKA = M2.SEKKEI_FUKA "
            Sql = Sql & N & "	             AND M1.SEKKEI_KAITEI = M2.KAITEI ) MPE"
            Sql = Sql & N & " ON M12.HINMEICD = MPE.SHIYO || MPE.HINSYU || MPE.SENSHIN || MPE.SAIZU || MPE.IRO "
            Sql = Sql & N & " WHERE NOT M12.KHINMEICD = M12.HINMEICD "
            Sql = Sql & N & " AND M12.KHINMEICD = '" & _db.rmNullStr(_kHinmeiCD) & "'"
            '2014/06/04 UPD-E Sugano

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            'If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
            '    Throw New UsrDefException("登録されていません。", _msgHd.getMSG("noData"))
            'End If

            '抽出データを一覧に表示する
            Dim gh As UtilMDL.DataGridView.UtilDataGridViewHandler = New UtilMDL.DataGridView.UtilDataGridViewHandler(Me.dgvSHinmei)
            gh.clearRow()
            dgvSHinmei.DataSource = ds
            dgvSHinmei.DataMember = RS

            '件数表示
            lblKensuu.Text = CStr(iRecCnt) & "件"

            _colorCtlFlg = True

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　一覧行追加
    '　処理概要）入力された集計対象品名コードとそれに対応する名称を一覧に表示する。
    '　　●入力パラメータ：なし
    '　　●関数戻り値　　：なし
    '-------------------------------------------------------------------------------
    Private Sub insertDGV()
        Try
            '集計対象品名コードの作成
            Dim sSiyo As String = txtSSiyo.Text
            If sSiyo.Length = 1 Then
                sSiyo = sSiyo & " "
            End If

            Dim sHinmeiCD As String = ""
            sHinmeiCD = sSiyo & txtSHinsyu.Text & txtSSensin.Text & txtSSize.Text & txtSColor.Text

            Dim SQL As String = ""
            SQL = SQL & N & " SELECT "
            SQL = SQL & N & " '" & sHinmeiCD & "'" & COLDT_KHINMEICD
            SQL = SQL & N & " ,HINSYU_MEI || SAIZU_MEI || IRO_MEI " & COLDT_KHINMEI
            '2014/06/04 UPD-S Sugano
            'SQL = SQL & N & " FROM MPESEKKEI "
            'SQL = SQL & N & "   WHERE SHIYO = '" & _db.rmSQ(txtSSiyo.Text) & "'  "
            SQL = SQL & N & " FROM MPESEKKEI1 "
            SQL = SQL & N & "   WHERE SHIYO = '" & sSiyo & "'  "
            '2014/06/04 UPD-E Sugano
            SQL = SQL & N & "   AND HINSYU = '" & _db.rmSQ(txtSHinsyu.Text) & "'  "
            SQL = SQL & N & "   AND SENSHIN = '" & _db.rmSQ(txtSSensin.Text) & "'  "
            SQL = SQL & N & "   AND SAIZU = '" & _db.rmSQ(txtSSize.Text) & "'  "
            SQL = SQL & N & "   AND IRO = '" & _db.rmSQ(txtSColor.Text) & "' "
            '2014/06/04 UPD-S Sugano
            'SQL = SQL & N & "   AND SEQ_NO = " & INT_SEQNO
            'SQL = SQL & N & "   AND SEKKEI_HUKA = "
            'SQL = SQL & N & "           (SELECT MAX(SEKKEI_HUKA) FROM MPESEKKEI "
            'SQL = SQL & N & "               WHERE SHIYO = '" & _db.rmSQ(txtSSiyo.Text) & "'  "
            SQL = SQL & N & "   AND SEKKEI_FUKA = 'A'"
            SQL = SQL & N & "   AND SEKKEI_KAITEI = (SELECT MAX(SEKKEI_KAITEI) FROM MPESEKKEI1 "
            SQL = SQL & N & "               WHERE SHIYO = '" & sSiyo & "'  "
            '2014/06/04 UPD-E Sugano
            SQL = SQL & N & "               AND HINSYU = '" & _db.rmSQ(txtSHinsyu.Text) & "'  "
            SQL = SQL & N & "               AND SENSHIN = '" & _db.rmSQ(txtSSensin.Text) & "'  "
            SQL = SQL & N & "               AND SAIZU = '" & _db.rmSQ(txtSSize.Text) & "'  "
            '2014/06/04 UPD-S Sugano
            'SQL = SQL & N & "               AND IRO = '" & _db.rmSQ(txtSColor.Text) & "')"
            SQL = SQL & N & "               AND IRO = '" & _db.rmSQ(txtSColor.Text) & "'"
            SQL = SQL & N & "               AND SEKKEI_FUKA = 'A')"
            '2014/06/04 UPD-E Sugano

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(SQL, RS2, iRecCnt)

            If iRecCnt <= 0 Then            '抽出レコードが１件もない場合
                txtSSiyo.Focus()            'フォーカス設定
                Throw New UsrDefException("集計対象品名コードが材料票マスタに登録されていません。", _
                                                                _msgHd.getMSG("notExistSyukeiZairyouMst"))
            End If

            '追加行生成
            Dim rowDt As Object() = {_db.rmNullStr(ds.Tables(RS2).Rows(0)(COLDT_KHINMEICD)), _
                                            _db.rmNullStr(ds.Tables(RS2).Rows(0)(COLDT_KHINMEI))}

            '追加するコードと一覧に表示されているコードの重複チェック
            For i As Integer = 0 To dgvSHinmei.RowCount - 1
                If _db.rmNullStr(ds.Tables(RS2).Rows(0)(COLDT_KHINMEICD)).Equals(_db.rmNullStr(dgvSHinmei(0, i).Value)) Then
                    txtSSiyo.Focus()
                    Throw New UsrDefException("すでに一覧に追加された集計対象品名コードです。", _
                                                            _msgHd.getMSG("alreadyAddSyukeiItiran"))
                End If
            Next

            'グリッドにバインドされたデータセット取得
            Dim wkDs As DataSet = dgvSHinmei.DataSource

            'そのデータセットに行追加
            wkDs.Tables(RS).Rows.Add(rowDt)

            'ソート処理
            Dim dtTmp As DataTable = wkDs.Tables(RS).Clone()

            'ソートされたデータビューの作成
            Dim dv As DataView = New DataView(wkDs.Tables(RS))
            dv.Sort = COLDT_KHINMEICD

            'ソートされたレコードのコピー
            For Each drv As DataRowView In dv
                dtTmp.ImportRow(drv.Row)
            Next

            'バインド用データセット生成
            Dim bindDs As DataSet = New DataSet
            bindDs.Tables.Add(dtTmp)

            '再バインド
            dgvSHinmei.DataSource = bindDs
            dgvSHinmei.DataMember = RS

            '一覧の件数を表示する
            lblKensuu.Text = CStr(dgvSHinmei.RowCount) & "件"

            '追加した行にフォーカス
            For c As Integer = 0 To dgvSHinmei.RowCount - 1
                If _db.rmNullStr(ds.Tables(RS2).Rows(0)(COLDT_KHINMEICD)).Equals(_db.rmNullStr(dgvSHinmei(0, c).Value)) Then
                    Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvSHinmei)
                    dgvSHinmei.Focus()
                    dgvSHinmei.CurrentCell = dgvSHinmei(0, c)
                End If
            Next

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　一覧行削除
    '　（処理概要）選択された行を一覧から削除する。
    '　　●入力パラメータ：なし
    '　　●関数戻り値　　：なし
    '-------------------------------------------------------------------------------
    Private Sub deleteRowDgv()
        Try

            'グリッドにバインドされたデータセット取得
            Dim wkDs As DataSet = dgvSHinmei.DataSource

            '選択行削除
            wkDs.Tables(RS).Rows.RemoveAt(dgvSHinmei.CurrentCellAddress.Y)

            '再バインド
            dgvSHinmei.DataSource = wkDs
            dgvSHinmei.DataMember = RS

            '一覧の件数を表示する
            lblKensuu.Text = CStr(dgvSHinmei.RowCount) & "件"

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　登録処理
    '　（処理概要）入力された値をDBに登録する。
    '　　●入力パラメータ：なし
    '　　●関数戻り値　　：なし
    '-------------------------------------------------------------------------------
    Private Sub insertDB()
        Try
            '仕様コードが1桁の場合は、半角スペースを加える
            Dim siyoCD As String = ""
            If _db.rmSQ(txtSSiyo.Text).Length = 1 Then
                siyoCD = siyoCD & _db.rmSQ(txtSSiyo.Text) & " "
            Else
                siyoCD = _db.rmSQ(txtSSiyo.Text)
            End If

            '計画品名コード作成
            Dim kHinmei As String = ""
            kHinmei = siyoCD & _db.rmSQ(txtSHinsyu.Text) & _db.rmSQ(txtSSensin.Text) & _db.rmSQ(txtSSize.Text) & _db.rmSQ(txtSColor.Text)

            '更新開始日時を取得
            Dim updStartDate As Date = Now

            'トランザクション開始
            _db.beginTran()

            'M12の削除
            Dim sql As String = ""
            sql = sql & N & " DELETE FROM M12SYUYAKU "
            sql = sql & N & "   WHERE KHINMEICD = '" & _kHinmeiCD & "'"
            _db.executeDB(sql)

            'M12 親コードの登録
            sql = ""
            sql = sql & N & " INSERT INTO M12SYUYAKU ("
            sql = sql & N & "  HINMEICD "
            sql = sql & N & " ,KHINMEICD "
            sql = sql & N & " ,UPDNAME "
            sql = sql & N & " ,UPDDATE) "
            sql = sql & N & " VALUES ( "
            sql = sql & N & "   '" & _kHinmeiCD & "', "
            sql = sql & N & "   '" & _kHinmeiCD & "', "
            sql = sql & N & " '" & _tanmatuID & "', "                                       '端末ID
            sql = sql & N & " TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS')) "   '更新日時
            _db.executeDB(sql)

            'M12 一覧に表示されている計画品名コードの登録
            For loopCnt As Integer = 0 To dgvSHinmei.RowCount - 1

                sql = ""
                sql = sql & N & " INSERT INTO M12SYUYAKU ("
                sql = sql & N & "  HINMEICD "
                sql = sql & N & " ,KHINMEICD "
                sql = sql & N & " ,UPDNAME "
                sql = sql & N & " ,UPDDATE) "
                sql = sql & N & " VALUES ( "
                sql = sql & N & "   '" & _db.rmSQ(dgvSHinmei(0, loopCnt).Value) & "', "
                sql = sql & N & "   '" & _kHinmeiCD & "', "
                sql = sql & N & " '" & _tanmatuID & "', "                                       '端末ID
                sql = Sql & N & " TO_DATE('" & updStartDate & "', 'YYYY/MM/DD HH24:MI:SS')) "   '更新日時
                _db.executeDB(Sql)
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

#End Region

#Region "ユーザ定義関数:チェック処理"

    '-------------------------------------------------------------------------------
    '　 集計対象品名コードチェック
    '   （処理概要）集計対象品名コードが入力されているかチェックする
    '-------------------------------------------------------------------------------
    Private Sub checkInputSHinmeiCD()
        Try

            If "".Equals(txtSSiyo.Text) Then
                txtSSiyo.Focus()
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【集計対象品仕様コード】"))
            ElseIf "".Equals(txtSHinsyu.Text) Then
                txtSHinsyu.Focus()
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【集計対象品品種コード】"))
            ElseIf "".Equals(txtSSensin.Text) Then
                txtSSensin.Focus()
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【集計対象品線心数コード】"))
            ElseIf "".Equals(txtSSize.Text) Then
                txtSSize.Focus()
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【集計対象品サイズコード】"))
            ElseIf "".Equals(txtSColor.Text) Then
                txtSColor.Focus()
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【集計対象品色コード】"))
            End If

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　 品名重複チェック
    '   （処理概要）①親画面で渡されてきた計画品名コードと重複していないかチェックする
    '               ②集計品名マスタを検索し、品名コードの重複がないかチェックする
    '　　●入力パラメータ： prmSiyo　   仕様コードまたは集計対象品仕様コード
    '                    ： prmHinsyu   品種コードまたは集計対象品品種コード
    '                    ： prmSensin　 線心数コードまたは集計対象品線心数コード
    '                    ： prmSize　   サイズコードまたは集計対象品サイズコード
    '                    ： prmColor　  色コードまたは集計対象品色コード
    '　　●関数戻り値　　： なし
    '-------------------------------------------------------------------------------
    Private Sub checkKHinmeiCDRepeat(ByVal prmSiyo As String, ByVal prmHinsyu As String, ByVal prmSensin As String, _
                                                            ByVal prmSize As String, ByVal prmColor As String)
        Try
            '品名を検索用につなげる
            '仕様コードが1桁の場合は、半角スペースを加える
            Dim siyoCD As String = ""
            If _db.rmSQ(prmSiyo).Length = 1 Then
                siyoCD = siyoCD & _db.rmSQ(prmSiyo) & " "
            Else
                siyoCD = _db.rmSQ(prmSiyo)
            End If

            '計画品名コード作成
            Dim kHinmei As String = ""
            kHinmei = siyoCD & _db.rmSQ(prmHinsyu) & _db.rmSQ(prmSensin) & _db.rmSQ(prmSize) & _db.rmSQ(prmColor)

            '計画品名コードとの重複チェック
            If _kHinmeiCD.Equals(kHinmei) Then
                txtSSiyo.Focus()
                Throw New UsrDefException("入力されたコードは計画品名コードと同じです。", _
                                    _msgHd.getMSG("equalsKHinmeiCD", "計画品名コード　：　" & _kHinmeiCD))
            End If

            'M12
            Dim sql As String = ""
            sql = ""
            sql = sql & N & " SELECT "
            sql = sql & N & " KHINMEICD "
            sql = sql & N & " FROM M12SYUYAKU "
            sql = sql & N & "   WHERE HINMEICD = '" & kHinmei & "' "

            Dim iRecCnt As Integer
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If Not iRecCnt = 0 Then     '抽出レコード件数が0件以外の場合
                txtSSiyo.Focus()
                Throw New UsrDefException("入力されたコードは以下のコードの実品名コードとして登録されています。", _
                    _msgHd.getMSG("alreakyAddJituhinmeiCD", "計画品名コード　：　" & _db.rmNullStr(ds.Tables(RS).Rows(0)("KHINMEICD"))))
            End If

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　 品名コード重複チェック
    '   （処理概要）計画対象品マスタを検索し、品名コードの重複がないかチェックする
    '-------------------------------------------------------------------------------
    Private Sub checkHinmeiCDRepeat()
        Try

            Dim sSiyo As String = _db.rmSQ(txtSSiyo.Text)
            If _db.rmSQ(txtSSiyo.Text).Length = 1 Then
                sSiyo = sSiyo & " "
            End If


            'M11
            Dim sql As String = ""
            sql = ""
            sql = sql & N & " SELECT "
            sql = sql & N & " * "
            sql = sql & N & " FROM M11KEIKAKUHIN "
            sql = sql & N & "   WHERE TT_H_SIYOU_CD = '" & sSiyo & "' "
            sql = sql & N & "   AND TT_H_HIN_CD = '" & _db.rmSQ(txtSHinsyu.Text) & "' "
            sql = sql & N & "   AND TT_H_SENSIN_CD = '" & _db.rmSQ(txtSSensin.Text) & "' "
            sql = sql & N & "   AND TT_H_SIZE_CD = '" & _db.rmSQ(txtSSize.Text) & "' "
            sql = sql & N & "   AND TT_H_COLOR_CD = '" & _db.rmSQ(txtSColor.Text) & "' "

            Dim iRecCnt As Integer
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If Not iRecCnt = 0 Then     '抽出レコード件数が0件以外の場合
                txtSSiyo.Focus()
                Throw New UsrDefException("品名コードは既に登録されています。", _msgHd.getMSG("alreadyHinmei"))
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