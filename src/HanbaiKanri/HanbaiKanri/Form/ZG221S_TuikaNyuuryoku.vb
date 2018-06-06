'===============================================================================
'
'　北日本電線株式会社
'　　（システム名）在庫計画システム
'　　（処理機能名）追加追加登録
'    （フォームID）ZG221S_TuikaNyuuryoku
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   橋本        2010/10/26                 新規              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB

Public Class ZG221S_TuikaNyuuryoku
    Inherits System.Windows.Forms.Form

#Region "リテラル値定義"
    'PG制御文字
    Private Const RS As String = "RecSet"                       'データセットテーブル名
    Private Const N As String = ControlChars.NewLine            '改行文字

    'プログラムID（T91実行履歴テーブル登録用）
    Private Const PGID As String = "ZG221S"
    '更新件数（T91実行履歴テーブル登録用）
    Private Const UPDATECOUNT As Integer = 1

    '生産見込テーブル更新内容固定値
    Private Const UPDATENYUKOFLG As String = "1"
    Private Const UPDATETAISYOFLG As String = "3"

#End Region

#Region "メンバ変数定義"

    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _parentForm As IfRturnUpDDate

    Private _Syori As String
    Private _Keikaku As String

    Private _changeFlg As Boolean = False           '変更フラグ
    Private _beforeChange As String = ""            '変更前のデータ

    '検索条件格納変数
    Private _Updatedata As Updatedata
    Private Structure Updatedata
        Private _TehaiKbn As String            '手配区分
        Private _JuyouCD As String             '需要先
        Private _Hinsyukbn As String           '品種

        Public Property TehaiKbn() As String
            Get
                Return _TehaiKbn
            End Get
            Set(ByVal Value As String)
                _TehaiKbn = Value
            End Set
        End Property
        Public Property JuyouCD() As String
            Get
                Return _JuyouCD
            End Get
            Set(ByVal Value As String)
                _JuyouCD = Value
            End Set
        End Property
        Public Property Hinsyukbn() As String
            Get
                Return _Hinsyukbn
            End Get
            Set(ByVal Value As String)
                _Hinsyukbn = Value
            End Set
        End Property
    End Structure

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
    'コンストラクタ　生産量補正画面から呼ばれる
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByVal prmForm As Form, ByVal prmSyori As String, ByVal prmKeikaku As String)
        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _parentForm = prmForm                                               '親フォーム
        _Syori = prmSyori                                                   '親から受け取った処理年月
        _Keikaku = prmKeikaku                                               '親から受け取った計画年月
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
    End Sub

#End Region

#Region "Formイベント"
    '-------------------------------------------------------------------------------
    '　処理開始イベント
    '-------------------------------------------------------------------------------
    Private Sub ZG221S_TuikaNyuuryoku_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
    '　戻るボタン押下
    '------------------------------------------------------------------------------------------------------
    Private Sub btnModoru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModoru.Click
        Try

            '警告メッセージ
            If _changeFlg Then
                Dim rtn As DialogResult = _msgHd.dspMSG("confirmDgvEdit")   '編集中の内容が破棄されます。よろしいですか？
                If rtn = Windows.Forms.DialogResult.Cancel Or rtn = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If

            '親フォーム表示
            _parentForm.myShow()
            _parentForm.myActivate()

            Me.Close()

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
                '処理開始時間と端末IDの取得
                Dim dStartSysdate As Date = Now()                           '処理開始日時
                Dim sPCName As String = UtilClass.getComputerName           '端末ID

                '登録内容取得
                Call getUpdateData()

                'トランザクション開始
                _db.beginTran()

                '生産見込テーブルの追加処理
                Call AddT21Seisanm(dStartSysdate, sPCName)

                '処理終了日時の取得
                Dim dFinishSysdate As Date = Now()

                '実行履歴テーブルの更新処理
                Call updT91Rireki(sPCName, dStartSysdate, dFinishSysdate)

                'トランザクション終了
                _db.commitTran()

                '親フォームに値渡し
                _parentForm.setUpDDate(dStartSysdate)

                '完了メッセージ
                _msgHd.dspMSG("completeInsert")

                '変更フラグを無効にする
                _changeFlg = False

                '親フォーム表示
                _parentForm.myShow()
                _parentForm.myActivate()

                Me.Close()

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

#End Region

#Region "ユーザ定義関数:画面制御"
    '------------------------------------------------------------------------------------------------------
    '   フォーム初期化
    '   （処理概要）　各コントロールの初期設定を行う
    '   ●入力パラメタ   ：なし
    '   ●メソッド戻り値 ：なし
    '------------------------------------------------------------------------------------------------------
    Private Sub initForm()
        Try
            '各コントロールの初期化
            txtKibou.Text = ""
            txtHinmeiCD.Text = ""
            lblHinmei.Text = ""
            txtSeiban.Text = ""
            txtSuuryou.Text = ""
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
    Private Sub txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtKibou.KeyPress, _
                                                                                                                txtHinmeiCD.KeyPress, _
                                                                                                                txtSeiban.KeyPress, _
                                                                                                                txtSuuryou.KeyPress
        Try

            UtilClass.moveNextFocus(Me, e) '次のコントロールへ移動する

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　コントロールキーフォーカス取得イベント
    '　(処理概要)フォーカス取得時、全選択状態にする
    '-------------------------------------------------------------------------------
    Private Sub txt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtKibou.GotFocus, _
                                                                                          txtHinmeiCD.GotFocus, _
                                                                                          txtSeiban.GotFocus, _
                                                                                          txtSuuryou.GotFocus
        Try
            '全選択状態にする
            UtilClass.selAll(sender)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　品名表示
    '　(処理年月)品名を表示する
    '-------------------------------------------------------------------------------
    Private Sub dispHinmei()
        Try
            '品名取得
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & "  M11.TT_HINMEI " & "HINMEI"          '品名
            sql = sql & N & " ,M12.KHINMEICD " & "KHINMEICD"          '計画品名コード
            sql = sql & N & " FROM M11KEIKAKUHIN M11 "
            sql = sql & N & "   LEFT OUTER JOIN M12SYUYAKU M12 ON "
            sql = sql & N & "   M11.TT_KHINMEICD = M12.KHINMEICD "
            sql = sql & N & " WHERE M12.HINMEICD = '" & txtHinmeiCD.Text & "'"

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                Throw New UsrDefException("品名コードが計画対象品マスタに登録されていません。", _msgHd.getMSG("NonKeikakuhinMst"), txtHinmeiCD)
            End If

            '品名表示
            lblHinmei.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("HINMEI"))
            lblKhinmeicd.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("KHINMEICD"))

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　テキストボックスコントロールのフォーカス取得時イベント
    '　(処理概要)テキストボックスコントロールのフォーカス取得時の処理
    '-------------------------------------------------------------------------------
    Private Sub txt_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKibou.Enter, _
                                                                                          txtHinmeiCD.Enter, _
                                                                                          txtSeiban.Enter, _
                                                                                          txtSuuryou.Enter
        Try
            '編集前の内容を保持
            If sender.GetType Is GetType(TextBox) Then
                Dim txt As TextBox = DirectCast(sender, TextBox)
                _beforeChange = txt.Text
            ElseIf sender.GetType Is GetType(CustomControl.TextBoxNum) Then
                Dim txtnum As CustomControl.TextBoxNum = DirectCast(sender, CustomControl.TextBoxNum)
                _beforeChange = txtnum.Text
            ElseIf sender.GetType Is GetType(CustomControl.TextBoxDate) Then
                Dim txtdate As CustomControl.TextBoxDate = DirectCast(sender, CustomControl.TextBoxDate)
                _beforeChange = txtdate.Text
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　テキストボックスコントロールのフォーカス消失時イベント
    '　(処理概要)テキストコントロールのフォーカス消失時の処理
    '-------------------------------------------------------------------------------
    Private Sub txt_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKibou.Leave, _
                                                                                              txtHinmeiCD.Leave, _
                                                                                              txtSeiban.Leave, _
                                                                                              txtSuuryou.Leave
        Try

            '編集前と値が変わっていた場合、フラグを立てる
            If sender.GetType Is GetType(TextBox) Then
                Dim txt As TextBox = DirectCast(sender, TextBox)
                If Not _beforeChange.Equals(txt.Text) Then
                    _changeFlg = True
                End If
                Select Case txt.Name
                    Case txtHinmeiCD.Name
                        If "".Equals(txtHinmeiCD.Text) Then
                            lblHinmei.Text = ""
                        Else
                            '入力チェック
                            Call checkHinCD()
                            '品名表示
                            Call dispHinmei()
                        End If
                End Select
            ElseIf sender.GetType Is GetType(CustomControl.TextBoxNum) Then
                Dim txtnum As CustomControl.TextBoxNum = DirectCast(sender, CustomControl.TextBoxNum)
                If Not _beforeChange.Equals(txtnum.Text) Then
                    _changeFlg = True
                End If
            ElseIf sender.GetType Is GetType(CustomControl.TextBoxDate) Then
                Dim txtdate As CustomControl.TextBoxDate = DirectCast(sender, CustomControl.TextBoxDate)
                If Not _beforeChange.Equals(txtdate.Text) Then
                    _changeFlg = True
                End If
            End If

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub
#End Region

#Region "ユーザ定義関数:DB関連"
    '------------------------------------------------------------------------------------------------------
    '　実行履歴テーブルの更新処理
    '  (処理概要)実行履歴テーブルにレコードを追加する
    '　　I　：　prmPCName      　　端末ID
    '　　I　：　prmStartDate       処理開始日時
    '　　I　：　prmFinishDate      処理終了日時
    '------------------------------------------------------------------------------------------------------
    Private Sub updT91Rireki(ByVal prmPCName As String, ByVal prmStartDate As Date, ByVal prmFinishDate As Date)
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
            sql = sql & N & ", UPDNAME"                                                     '端末ID
            sql = sql & N & ", UPDDATE"                                                     '更新日時
            sql = sql & N & ") VALUES ("
            sql = sql & N & "  '" & _Syori & "'"                                            '処理年月
            sql = sql & N & ", '" & _Keikaku & "'"                                          '計画年月
            sql = sql & N & ", '" & PGID & "'"                                              '機能ID
            sql = sql & N & ", TO_DATE('" & prmStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "   '処理開始日時
            sql = sql & N & ", TO_DATE('" & prmFinishDate & "', 'YYYY/MM/DD HH24:MI:SS') "  '処理終了日時
            sql = sql & N & ", " & UPDATECOUNT                                              '件数１（更新件数）
            sql = sql & N & ", '" & prmPCName & "'"                                         '端末ID
            sql = sql & N & ", TO_DATE('" & prmStartDate & "', 'YYYY/MM/DD HH24:MI:SS') "   '更新日時
            sql = sql & N & " )"
            _db.executeDB(sql)

        Catch ex As Exception
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　登録内容の取得処理
    '  (処理概要)生産見込テーブルに設定する内容を取得
    '------------------------------------------------------------------------------------------------------
    Private Sub getUpdatedata()
        Try
            Dim sql As String = ""
            sql = "SELECT "
            sql = sql & N & "  TT_TEHAI_KBN " & "TEHAIKBN"       '手配区分
            sql = sql & N & " ,TT_JUYOUCD " & "JUYOUCD"          '需要先
            sql = sql & N & " ,TT_HINSYUKBN " & "HINSYUKBN"      '品種
            sql = sql & N & " FROM M11KEIKAKUHIN "
            sql = sql & N & " WHERE TT_KHINMEICD = '" & lblKhinmeicd.Text & "'"

            'SQL発行
            Dim iRecCnt As Integer          'データセットの行数
            Dim ds As DataSet = _db.selectDB(sql, RS, iRecCnt)

            If iRecCnt <= 0 Then                    '抽出レコードが１件もない場合
                Throw New UsrDefException("登録されていません。", _msgHd.getMSG("noData"))
            End If

            '登録内容編集
            _Updatedata.TehaiKbn = _db.rmNullStr(ds.Tables(RS).Rows(0)("TEHAIKBN"))
            _Updatedata.JuyouCD = _db.rmNullStr(ds.Tables(RS).Rows(0)("JUYOUCD"))
            _Updatedata.Hinsyukbn = _db.rmNullStr(ds.Tables(RS).Rows(0)("HINSYUKBN"))

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '　生産見込テーブル追加処理
    '------------------------------------------------------------------------------------------------------
    Private Sub AddT21Seisanm(ByVal prmSysdate As Date, ByVal prmPCName As String)
        Try
            '追加処理
            Dim sql As String = ""
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
            sql = sql & N & " VALUES ("
            '-->2010.12.12 chg by takagi
            'sql = sql & N & "  '" & txtHinmeiCD.Text & "'"                                         '実品名コード
            'sql = sql & N & ", '" & txtHinmeiCD.Text.Substring(0, 2) & "'"                         '仕様コード
            'sql = sql & N & ", '" & txtHinmeiCD.Text.Substring(2, 3) & "'"                         '品種コード
            'sql = sql & N & ", '" & txtHinmeiCD.Text.Substring(5, 3) & "'"                         '線心数コード
            'sql = sql & N & ", '" & txtHinmeiCD.Text.Substring(8, 2) & "'"                         'サイズコード
            'sql = sql & N & ", '" & txtHinmeiCD.Text.Substring(10, 3) & "'"                        '色コード
            'sql = sql & N & ", '" & lblHinmei.Text & "'"                                           '品名
            'sql = sql & N & ", '" & lblKhinmeicd.Text & "'"                                        '計画品名コード
            'sql = sql & N & ", ''"                                                                 '年
            'sql = sql & N & ", ''"                                                                 '手配№
            'sql = sql & N & ", ''"                                                                 '連番
            'sql = sql & N & ", '" & txtSeiban.Text & "'"                                           '製番
            'sql = sql & N & ", '" & _Updatedata.TehaiKbn & "'"                                     '手配区分
            'sql = sql & N & ", 0"                                                                  '単長
            'sql = sql & N & ", 0"                                                                  '条数
            'sql = sql & N & ", 0"                                                                  '手配数
            'sql = sql & N & ", '" & Trim(Replace(txtKibou.Text, "/", "")) & "'"                    '希望出来日
            'sql = sql & N & ", ''"                                                                 '予定出来日
            'sql = sql & N & ", 0"                                                                  '入庫数
            'sql = sql & N & ", 0"                                                                  '未納残
            'sql = sql & N & ", '" & txtSuuryou.Text & "'"                                          '生産見込数
            'sql = sql & N & ", '" & Trim(Replace(txtKibou.Text, "/", "")).Substring(0, 6) & "'"    '年月
            'sql = sql & N & ", '" & _Updatedata.JuyouCD & "'"                                      '需要先
            'sql = sql & N & ", '" & _Updatedata.Hinsyukbn & "'"                                     '品種区分
            'sql = sql & N & ", ''"                                                                 '内入フラグ
            'sql = sql & N & ", '" & UPDATENYUKOFLG & "'"                                           '対象フラグ"
            'sql = sql & N & ", '" & UPDATETAISYOFLG & "'"                                          '作成区分
            'sql = sql & N & ", ''"                                                                 '入庫日
            'sql = sql & N & ", '" & prmPCName & "'"                                                '端末ID
            'sql = sql & N & ", TO_DATE('" & prmSysdate & "', 'YYYY/MM/DD HH24:MI:SS') "            '更新日時
            sql = sql & N & "  '" & _db.rmSQ(txtHinmeiCD.Text) & "'"                                         '実品名コード
            sql = sql & N & ", '" & _db.rmSQ(txtHinmeiCD.Text.Substring(0, 2)) & "'"                         '仕様コード
            sql = sql & N & ", '" & _db.rmSQ(txtHinmeiCD.Text.Substring(2, 3)) & "'"                         '品種コード
            sql = sql & N & ", '" & _db.rmSQ(txtHinmeiCD.Text.Substring(5, 3)) & "'"                         '線心数コード
            sql = sql & N & ", '" & _db.rmSQ(txtHinmeiCD.Text.Substring(8, 2)) & "'"                         'サイズコード
            sql = sql & N & ", '" & _db.rmSQ(txtHinmeiCD.Text.Substring(10, 3)) & "'"                        '色コード
            sql = sql & N & ", '" & _db.rmSQ(lblHinmei.Text) & "'"                                           '品名
            sql = sql & N & ", '" & _db.rmSQ(lblKhinmeicd.Text) & "'"                                        '計画品名コード
            sql = sql & N & ", ''"                                                                 '年
            sql = sql & N & ", ''"                                                                 '手配№
            sql = sql & N & ", ''"                                                                 '連番
            sql = sql & N & ", '" & _db.rmSQ(txtSeiban.Text) & "'"                                           '製番
            sql = sql & N & ", '" & _db.rmSQ(_Updatedata.TehaiKbn) & "'"                                     '手配区分
            sql = sql & N & ", 0"                                                                  '単長
            sql = sql & N & ", 0"                                                                  '条数
            sql = sql & N & ", 0"                                                                  '手配数
            sql = sql & N & ", '" & Trim(Replace(txtKibou.Text, "/", "")) & "'"                    '希望出来日
            sql = sql & N & ", ''"                                                                 '予定出来日
            sql = sql & N & ", 0"                                                                  '入庫数
            sql = sql & N & ", 0"                                                                  '未納残
            sql = sql & N & ", '" & _db.rmSQ(CInt(txtSuuryou.Text)) & "'"                                          '生産見込数
            sql = sql & N & ", '" & Trim(Replace(txtKibou.Text, "/", "")).Substring(0, 6) & "'"    '年月
            sql = sql & N & ", '" & _db.rmSQ(_Updatedata.JuyouCD) & "'"                                      '需要先
            sql = sql & N & ", '" & _db.rmSQ(_Updatedata.Hinsyukbn) & "'"                                     '品種区分
            sql = sql & N & ", ''"                                                                 '内入フラグ
            sql = sql & N & ", '" & _db.rmSQ(UPDATENYUKOFLG) & "'"                                           '対象フラグ"
            sql = sql & N & ", '" & _db.rmSQ(UPDATETAISYOFLG) & "'"                                          '作成区分
            sql = sql & N & ", ''"                                                                 '入庫日
            sql = sql & N & ", '" & _db.rmSQ(prmPCName) & "'"                                                '端末ID
            sql = sql & N & ", TO_DATE('" & prmSysdate & "', 'YYYY/MM/DD HH24:MI:SS') "            '更新日時
            '<--2010.12.12 chg by takagi
            sql = sql & N & ")"
            _db.executeDB(sql)
        Catch ex As Exception
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try
    End Sub
#End Region

#Region "ユーザ定義関数:チェック処理"

    '------------------------------------------------------------------------------------------------------
    '  入力チェック
    '　(処理概要)品名コードに入力された内容のチェックを行う
    '------------------------------------------------------------------------------------------------------
    Private Sub checkHinCD()
        Try
            '品名コードチェック
            If Not txtHinmeiCD.TextLength.Equals(13) Then
                'エラーメッセージの表示
                Throw New UsrDefException("品名コードの桁数が違います。", _msgHd.getMSG("HinmeiLengthNG", "【 品名コード 】"), txtHinmeiCD)
            End If

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '  登録チェック
    '　(処理概要)各項目の必須項目チェックを行う
    '------------------------------------------------------------------------------------------------------
    Private Sub checkTouroku()
        Try
            Call chekuHissu(txtKibou, Trim(Replace(txtKibou.Text, "/", "")), "希望出来日")
            Call chekuHissu(txtHinmeiCD, txtHinmeiCD.Text, "品名コード")
            Call chekuHissu(txtSeiban, txtSeiban.Text, "製量")
            Call chekuHissu(txtSuuryou, txtSuuryou.Text, "数量")

            '空欄チェック
            If "".Equals(lblHinmei.Text) Then
                'エラーメッセージの表示
                Throw New UsrDefException("品名コードが計画対象品マスタに登録されていません。", _msgHd.getMSG("NonKeikakuhinMst"), txtHinmeiCD)
            End If

        Catch ue As UsrDefException         'ユーザー定義例外
            Call ue.dspMsg()
            Throw ue                        'キャッチした例外をそのままスロー
        Catch ex As Exception               'システム例外
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------
    '  必須入力チェック
    '　(処理概要)テキストが入力されているかチェックする
    '　　I　：　prmSender              チェックするオブジェクト
    '　　I　：　prmControlName         チェックするオブジェクト名
    '------------------------------------------------------------------------------------------------------
    Private Sub chekuHissu(ByVal prmSender As System.Object, ByVal prmChktxt As String, ByVal prmControlName As String)
        Try
            '必須入力チェック
            If "".Equals(prmChktxt) Then
                'エラーメッセージの表示
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【 " & prmControlName & "】"), prmSender)
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