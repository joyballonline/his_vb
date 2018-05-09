'===============================================================================
'　 （システム名）      カネキ吉田商店様向け　原価管理システム
'
'   （機能名）          消費税マスタ保守
'   （クラス名）        frmM80F20_ShohizeiHosyu
'   （処理機能名）      
'   （本MDL使用前提）   
'   （備考）         
'
'===============================================================================
' 履歴  名前               日付       　　　 　内容
'-------------------------------------------------------------------------------
'  (1)  桜井               2018/03/29      　　新規
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.DB
Imports UtilMDL.MSG             'UtilMsgHandler用

Public Class frmM80F20_ShohizeiHosyu
#Region "宣言"

    '-------------------------------------------------------------------------------
    ' 定数宣言
    '-------------------------------------------------------------------------------
    ' PG制御文字
    Private Const N As String = ControlChars.NewLine                    ' 改行文字
    Private Const RS As String = "RecSet"                               ' レコードセットテーブル

    '汎用マスメン
    Private Const lM8020_DB_EXIST As Integer = 1                        'あり：登録済
    Private Const lM8020_DB_NONE As Integer = 0                         'なし：未登録

    '更新者・更新日見出しラベル
    Private Const MIDASHI_KOUSINSYA As String = "更新者："
    Private Const MIDASHI_KOUSINBI As String = "更新日："

    '適用終了日最大値
    Private Const TEKIYOSHURYOBI_MAX As String = "9999/12/31"

    '-------------------------------------------------------------------------------
    'メンバー変数宣言
    '-------------------------------------------------------------------------------
    Private _db As UtilDBIf
    Private _msgHd As UtilMsgHandler
    Private _parentForm As frmM80F10_ShohizeiList
    Private _ShoriMode As Integer
    Private _comLogc As CommonLogic                         '共通処理用
    Private _open As Boolean = False                        '画面起動済フラグ
    Private _dbErr As Boolean = False                       'DB登録エラー判定用
    Private _companyCd As String
    Private _selectId As String
    Private _userId As String
    Private Shared _shoriId As String
    Private _UpdateTime As String
    Private _TekiyoToDT As String

    Private _readOnlyTextBackColor As Color

    'データDB更新フラグ(DB登録判定)
    Private pCS8020_DBFlg As Long

    '引数格納用変数
    Private psTekiyoFrDT As String

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
    'コンストラクタ　
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler,
                   ByRef prmRefDbHd As UtilDBIf,
                   ByVal prmParentForm As frmM80F10_ShohizeiList,
                   ByVal prmMode As Integer,
                   ByRef prmSelectID As String,
                   ByRef prmTekiyoFrDT As String)

        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _parentForm = prmParentForm
        _ShoriMode = prmMode
        psTekiyoFrDT = prmTekiyoFrDT
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint  'フォームタイトル表示
        _companyCd = frmC01F10_Login.loginValue.BumonCD                     '会社コード
        _selectId = prmSelectID                                             '選択処理ID
        _userId = frmC01F10_Login.loginValue.TantoCD                        'ユーザＩＤ

        _readOnlyTextBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))

        ' 共通処理使用の準備
        _comLogc = New CommonLogic(_db, _msgHd)                             ' 共通処理用

        '画面項目のクリア
        initializeDisplay()

        '処理状態の選択
        Select Case prmMode
            Case CommonConst.MODE_InquiryStatus     '参照
                lblShoriMode.Text = CommonConst.MODE_InquiryStatus_NAME
            Case CommonConst.MODE_ADDNEW            '新規
                lblShoriMode.Text = CommonConst.MODE_ADDNEW_NAME
            Case CommonConst.MODE_ADDNEWCOPY        '複写新規
                lblShoriMode.Text = CommonConst.MODE_ADDNEWCOPY_NAME
            Case CommonConst.MODE_EditStatus        '変更
                lblShoriMode.Text = CommonConst.MODE_EditStatus_NAME
            Case CommonConst.MODE_DELETE            '削除
                lblShoriMode.Text = CommonConst.MODE_DELETE_NAME
        End Select

        '参照時は確定ボタン使用不可
        If prmMode = CommonConst.MODE_InquiryStatus Then
            cmdKakutei.Enabled = False
        Else
            cmdKakutei.Enabled = True
        End If
        cmdModoru.Enabled = True

        '新規以外
        If prmMode <> CommonConst.MODE_ADDNEW Then
            'マスタデータ取得
            getMasterData()
        End If

        '画面項目のプロパティセット
        setProperty()

        '背景色セット
        setBackColor()

    End Sub
#End Region

#Region "イベント"

#Region "フォームロード"
    '-------------------------------------------------------------------------------
    '   画面ロード処理
    '   （処理概要） 画面が起動したときの処理を行う。
    '-------------------------------------------------------------------------------
    Private Sub frmM80F20ShohizeiHosyu_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim BkCur As Cursor = Cursor.Current                                ' 現在のカーソルを取っておく
        Try
            Cursor.Current = Cursors.WaitCursor                             ' 砂時計カーソルに入れ替える

            _open = True                                                    '画面起動済フラグ

            Dim tekiyoFrDT As Long = 0
            If IsDate(psTekiyoFrDT) Then
                tekiyoFrDT = Long.Parse(psTekiyoFrDT.Replace("/", ""))
            End If

            '操作履歴ログ作成
            Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, CommonConst.MENU_M8002, CommonConst.PROGRAM_UPDATE, CommonConst.STATUS_NORMAL,
                                                CommonConst.SHOUHIZEI_KBN, DBNull.Value, lblShoriMode.Text, DBNull.Value, DBNull.Value,
                                                tekiyoFrDT, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _userId)


        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        Finally
            Cursor.Current = BkCur                                          ' 取っておいたカーソルに戻す
        End Try

    End Sub
#End Region

#Region "確定ボタンクリック"
    '-------------------------------------------------------------------------------
    '   確定ボタンクリック時処理
    '   （処理概要） 確定ボタン押下時に、処理モードにより該当する処理を行う。
    '-------------------------------------------------------------------------------
    Private Sub cmdKautei_Click(sender As Object, e As EventArgs) Handles cmdKakutei.Click

        Dim sql As String = ""
        Try

            '排他チェック処理
            Select Case _ShoriMode
                Case CommonConst.MODE_ADDNEW   '登録
                '新規登録の場合は処理不要。

                Case CommonConst.MODE_EditStatus   '変更
                    Try
                        sql = ""
                        sql = "SELECT  "
                        sql = sql & N & "    ct.更新日,ct.更新者 "
                        sql = sql & N & " FROM m71_ctax ct "
                        sql = sql & N & " Where ct.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and ct.消費税区分 = '" & CommonConst.SHOUHIZEI_KBN & "' and ct.適用開始日 = '" & Me.dtpTekiyoFrDt.Value & "'"
                        Dim reccnt As Integer = 0
                        Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)
                        If reccnt = 0 Then
                            Exit Sub
                        End If
                        If _UpdateTime <> DateTime.Parse(ds.Tables(RS).Rows(0)("更新日")) Then
                            '登録終了メッセージ
                            Dim strMessage As String = ""
                            strMessage = "更新者：" & ds.Tables(RS).Rows(0)("更新者") & " 更新日時：" & ds.Tables(RS).Rows(0)("更新日").ToString
                            _msgHd.dspMSG("Exclusion", strMessage)
                            'Me.Close()
                            Exit Sub
                        End If
                    Catch
                    End Try
                    '変更の場合のみ処理を行う
            End Select

            '処理モードにより更新方法を切り替える
            Select Case _ShoriMode
                Case CommonConst.MODE_ADDNEW        '新規
                    If M80SHOHIZEI_Add_Ctl() = False Then
                        Exit Sub
                    End If
                Case CommonConst.MODE_ADDNEWCOPY    '複写新規
                    If M80SHOHIZEI_Add_Ctl() = False Then
                        Exit Sub
                    End If
                Case CommonConst.MODE_EditStatus    '変更
                    If M80SHOHIZEI_Upd_Ctl() = False Then
                        Exit Sub
                    End If
                Case CommonConst.MODE_DELETE        '削除
                    If M80SHOHIZEI_Del_Ctl() = False Then
                        Exit Sub
                    End If
            End Select

            Dim tekiyoFrDT As String = ""
            If _ShoriMode = CommonConst.MODE_ADDNEW Or _ShoriMode = CommonConst.MODE_ADDNEWCOPY Then
                If IsDate(Me.dtpTekiyoFrDt.Text) Then
                    tekiyoFrDT = Me.dtpTekiyoFrDt.Text.Replace("/", "")
                End If
            Else
                If IsDate(Me.txtTekiyoFrDt.Text) Then
                    tekiyoFrDT = Me.txtTekiyoFrDt.Text.Replace("/", "")
                End If
            End If

            '操作履歴ログ作成 
            Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, CommonConst.MENU_M8002, CommonConst.PROGRAM_REPORT, CommonConst.STATUS_NORMAL,
                                                CommonConst.SHOUHIZEI_KBN, DBNull.Value, lblShoriMode.Text, DBNull.Value, DBNull.Value,
                                                Long.Parse(tekiyoFrDT), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _userId)

            '更新フラグ：登録済
            pCS8020_DBFlg = lM8020_DB_EXIST

            'キー項目を親フォームに返却（新規・複写新規は保守画面の入力値）
            '選択行の位置づけに使用
            If _ShoriMode = CommonConst.MODE_ADDNEW Or _ShoriMode = CommonConst.MODE_ADDNEWCOPY Then
                _parentForm.tekiyoFrDT = Me.dtpTekiyoFrDt.Text
            Else
                _parentForm.tekiyoFrDT = Me.txtTekiyoFrDt.Text
            End If
            _parentForm.redisplay = True

            _parentForm.Show()                                              ' 前画面を表示
            _parentForm.Enabled = True                                      ' 前画面の使用を可能にする
            _parentForm.Activate()                                          ' 前画面をアクティブにする

            Me.Dispose()                                                    ' 自画面を閉じる

        Catch ue As UsrDefException
            ue.dspMsg()

            'データ取得失敗の場合、アプリケーション終了
            If _dbErr Then
                Me.Dispose()                    ' 自画面を閉じる
                _parentForm.Dispose()           ' 呼出元画面を閉じる
                Application.Exit()              ' アプリケーション終了
            End If
        Catch ex As Exception
            Dim te As UsrDefException = New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub
#End Region

#Region "戻るボタンクリック"
    Private Sub cmdModoru_Click(sender As Object, e As EventArgs) Handles cmdModoru.Click

        _parentForm.Show()                                              ' 前画面を表示
        _parentForm.Enabled = True                                      ' 前画面の使用を可能にする
        _parentForm.Activate()                                          ' 前画面をアクティブにする

        Me.Dispose()                                                    ' 自画面を閉じる

    End Sub
#End Region

#End Region

#Region "プロシージャ"
    '-------------------------------------------------------------------------------
    '　データ追加処理
    '-------------------------------------------------------------------------------
    Private Function M80SHOHIZEI_Add_Ctl() As Boolean

        M80SHOHIZEI_Add_Ctl = False

        'データチェック
        Call Check_Data_Ctl()

        '登録確認ダイアログ表示
        Dim piRtn As DialogResult = _msgHd.dspMSG("confirmInsert")                   '登録します。よろしいですか？
        If piRtn <> vbYes Then Exit Function

        Dim BkCur As Cursor = Cursor.Current                                ' 現在のカーソルを取っておく
        Try
            Cursor.Current = Cursors.WaitCursor                             ' 砂時計カーソルに入れ替える

            '登録実行
            'トランザクション開始
            If Not _db.isTransactionOpen Then
                _db.beginTran()
            End If

            '追加処理
            Call Insert_M80SHOHIZEI()

            '完了メッセージ表示
            Call _msgHd.dspMSG("completeInsert")                                      '登録が完了しました。

            'トランザクション終了
            _db.commitTran()

            M80SHOHIZEI_Add_Ctl = True

        Catch ue As UsrDefException
            Throw ue
        Catch ex As Exception
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        Finally
            If _db.isTransactionOpen Then
                'ロールバック
                _db.rollbackTran()
            End If
            Cursor.Current = BkCur                                          ' 取っておいたカーソルに戻す
        End Try

    End Function

    '-------------------------------------------------------------------------------
    '　データ更新処理
    '-------------------------------------------------------------------------------
    Private Function M80SHOHIZEI_Upd_Ctl() As Boolean

        M80SHOHIZEI_Upd_Ctl = False

        '登録確認ダイアログ表示
        Dim piRtn As DialogResult = _msgHd.dspMSG("confirmUpdate")                   '登録します。よろしいですか？
        If piRtn <> vbYes Then Exit Function

        Dim BkCur As Cursor = Cursor.Current                                ' 現在のカーソルを取っておく
        Try
            Cursor.Current = Cursors.WaitCursor                             ' 砂時計カーソルに入れ替える

            '登録実行
            'トランザクション開始
            If Not _db.isTransactionOpen Then
                _db.beginTran()
            End If

            '更新処理
            Call Update_M80SHOHIZEI()

            '完了メッセージ表示
            Call _msgHd.dspMSG("completeUpdate")                                      '登録が完了しました。

            'トランザクション終了
            _db.commitTran()

            M80SHOHIZEI_Upd_Ctl = True

        Catch ue As UsrDefException
            Throw ue
        Catch ex As Exception
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        Finally
            If _db.isTransactionOpen Then
                'ロールバック
                _db.rollbackTran()
            End If
            Cursor.Current = BkCur                                          ' 取っておいたカーソルに戻す
        End Try

    End Function

    '-------------------------------------------------------------------------------
    '　データ削除処理
    '-------------------------------------------------------------------------------
    Private Function M80SHOHIZEI_Del_Ctl() As Boolean

        M80SHOHIZEI_Del_Ctl = False

        '削除確認ダイアログ表示
        Dim piRtn As DialogResult = _msgHd.dspMSG("confirmDelete")                                       '選択されているデータの削除を行います。よろしいですか？
        If piRtn <> vbYes Then Exit Function

        Dim BkCur As Cursor = Cursor.Current                                ' 現在のカーソルを取っておく
        Try
            Cursor.Current = Cursors.WaitCursor                             ' 砂時計カーソルに入れ替える

            '登録実行
            'トランザクション開始
            If Not _db.isTransactionOpen Then
                _db.beginTran()
            End If

            '削除処理
            Call Delete_M80SHOHIZEI()

            '完了メッセージ表示
            Call _msgHd.dspMSG("completeDelete")                                      '削除が完了しました。

            'トランザクション終了
            _db.commitTran()

            M80SHOHIZEI_Del_Ctl = True

        Catch ue As UsrDefException
            Throw ue
        Catch ex As Exception
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))   'キャッチした例外をユーザー定義例外に移し変えスロー
        Finally
            If _db.isTransactionOpen Then
                'ロールバック
                _db.rollbackTran()
            End If
            Cursor.Current = BkCur                                          ' 取っておいたカーソルに戻す
        End Try

    End Function

    '-------------------------------------------------------------------------------
    '　データチェック制御
    '　（処理概要）「登録」時、必須項目入力チェック，キー重複チェックをする。
    '-------------------------------------------------------------------------------
    Private Sub Check_Data_Ctl()

        Dim iRowCnt As Integer = 0
        Dim lUpdFlg As Boolean = False

        Try
            Call Check_Text_Input()

            'キー重複チェック(DB上)
            Dim lRtn As Long = Check_Key()
            If lRtn > 0 Then                                        '抽出レコード
                Throw New UsrDefException("重複データが存在します。", _msgHd.getMSG("RegDupKeyData"))
            End If

        Catch lue As UsrDefException
            Throw lue
        Catch lex As Exception
            Throw New UsrDefException(lex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(lex)))       'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　データ入力チェック
    '   （処理概要）新規追加or変更時，必須項目入力チェックを行う
    '-------------------------------------------------------------------------------
    Private Sub Check_Text_Input()

        If dtpTekiyoFrDt.Value Is Nothing Then '適用開始日
            Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【適用開始日】"), dtpTekiyoFrDt)
        End If

    End Sub

    '-------------------------------------------------------------------------------
    '　キー重複チェック
    '　（処理概要）「登録」時、キーの重複をチェックする。
    '-------------------------------------------------------------------------------
    Private Function Check_Key() As Integer

        Check_Key = False

        Try
            'sql編集
            Dim sql As String = ""
            sql = "SELECT *"
            sql = sql & N & " FROM"
            sql = sql & N & " m71_ctax"
            sql = sql & N & " WHERE"
            sql = sql & N & "       会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            sql = sql & N & "   AND 消費税区分 = '" & CommonConst.SHOUHIZEI_KBN & "'"
            sql = sql & N & "   AND 適用開始日 = '" & Me.dtpTekiyoFrDt.Text & "'"

            Dim iRecCnt As Integer = 0
            Try
                'sql発行
                Dim oDataSet As DataSet = _db.selectDB(sql, RS, iRecCnt)    '抽出結果をDSへ格納
            Catch ex As Exception
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
            End Try

            Check_Key = iRecCnt

        Catch lue As UsrDefException
            Throw lue
        Catch lex As Exception
            Throw New UsrDefException(lex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(lex)))       'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Function

    '-------------------------------------------------------------------------------
    '　sql／Insert発行
    '　（処理概要）sql文を作成し，ＤＢへの登録を行う
    '-------------------------------------------------------------------------------
    Private Sub Insert_M80SHOHIZEI()

        Try
            '消費税マスタの最新レコード更新
            Dim sql As String = ""
            sql = "UPDATE m71_ctax"
            sql = sql & N & " SET"
            sql = sql & N & "  適用終了日 = '" & DateTime.Parse(Me.dtpTekiyoFrDt.Value).AddDays(-1).ToString("yyyy/MM/dd") & "'"     '適用終了日
            sql = sql & N & " ,更新者 = '" & _db.rmSQ(frmC01F10_Login.loginValue.TantoCD) & "'"                                           '更新者
            sql = sql & N & " ,更新日 = current_timestamp"                                                                           '更新日
            sql = sql & N & " WHERE 会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"                                       '会社コード
            sql = sql & N & "   AND 消費税区分 = '" & _db.rmSQ(CommonConst.SHOUHIZEI_KBN) & "' "                                               '消費税区分

            sql = sql & N & "   AND 適用終了日 >= '" & Me.dtpTekiyoFrDt.Text & "'"
            sql = sql & N & "   AND 適用開始日 <= '" & Me.dtpTekiyoFrDt.Text & "'"

            Try
                'sql発行
                _db.executeDB(sql)
            Catch ex As Exception
                _dbErr = True
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
            End Try

        Catch lue As UsrDefException
            Throw lue
        Catch lex As Exception
            Throw New UsrDefException(lex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(lex)))       'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

        Try

            Dim tekiyoShuryoDate As String = String.Empty
            Dim sql As String = ""
            sql = "SELECT  "
            sql = sql & N & "    min(適用開始日) 開始日 "
            sql = sql & N & " FROM m71_ctax "
            sql = sql & N & " Where 会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' and 消費税区分 = '" & _db.rmSQ(CommonConst.SHOUHIZEI_KBN) & "' and 適用開始日 >= '" & Me.dtpTekiyoFrDt.Value & "'"
            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)
            If reccnt = 0 Then
                tekiyoShuryoDate = TEKIYOSHURYOBI_MAX
            Else
                If _db.rmNullStr(ds.Tables(RS).Rows(0)("開始日")) = "" Then
                    tekiyoShuryoDate = TEKIYOSHURYOBI_MAX
                Else
                    tekiyoShuryoDate = DateTime.Parse(ds.Tables(RS).Rows(0)("開始日")).AddDays(-1).ToString("yyyy/MM/dd")
                End If
            End If

            '消費税マスタ追加
            sql = ""
            sql = "INSERT INTO"
            sql = sql & N & " m71_ctax "
            sql = sql & N & "("
            sql = sql & N & "  会社コード"          '会社コード
            sql = sql & N & ", 消費税区分"          '消費税区分
            sql = sql & N & ", 適用開始日"          '適用開始日
            sql = sql & N & ", 適用終了日"          '適用終了日
            sql = sql & N & ", 消費税率"            '消費税率
            sql = sql & N & ", 更新者"              '更新者
            sql = sql & N & ", 更新日"              '更新日
            sql = sql & N & ")"
            sql = sql & N & " VALUES"
            sql = sql & N & "("
            sql = sql & N & "  '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"
            sql = sql & N & ", '" & _db.rmSQ(CommonConst.SHOUHIZEI_KBN) & "'"
            If _ShoriMode = CommonConst.MODE_EditStatus Then
                sql = sql & N & ", '" & Me.dtpTekiyoFrDt.Value & "'"
            Else
                sql = sql & N & ", '" & Me.dtpTekiyoFrDt.Text & "'"
            End If
            sql = sql & N & ", '" & tekiyoShuryoDate & "'"
            sql = sql & N & ", " & nullToNum(Me.txtShohizeiRitsu.Value)
            sql = sql & N & ", '" & _db.rmSQ(frmC01F10_Login.loginValue.TantoCD) & "'"
            sql = sql & N & ", current_timestamp "
            sql = sql & N & ")"

            Try
                'sql発行
                _db.executeDB(sql)
            Catch ex As Exception
                _dbErr = True
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
            End Try

        Catch lue As UsrDefException
            Throw lue
        Catch lex As Exception
            Throw New UsrDefException(lex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(lex)))       'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try
    End Sub

    '-------------------------------------------------------------------------------
    '　sql／Delete発行
    '　（処理概要）sql文を作成し，ＤＢへの登録を行う
    '-------------------------------------------------------------------------------
    Private Sub Delete_M80SHOHIZEI()

        '削除対象のデータの適用開始日より前の日付の適用終了日を持つデータが存在するかどうか
        '存在した場合直近のデータを前歴データとするため適用終了日の降順でソートする
        Try
            Dim sql As String = ""
            'sql編集
            sql = ""
            sql = "SELECT "
            sql = sql & N & "    適用開始日, 適用終了日, 消費税率 "
            sql = sql & N & " FROM m71_ctax "
            sql = sql & N & " Where 会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' and 消費税区分 = '" & _db.rmSQ(CommonConst.SHOUHIZEI_KBN) & "' and 適用終了日 < '" & Me.txtTekiyoFrDt.Text & "'"
            sql = sql & N & " ORDER BY 適用終了日 DESC "

            Dim iRecCnt As Integer = 0
            Dim iRecCnt2 As Integer = 0
            Dim iRecCnt3 As Integer = 0
            Try
                'sql発行
                Dim oDataSet As DataSet = _db.selectDB(sql, RS, iRecCnt)    '抽出結果をDSへ格納
                Dim tekiyoKaisiDate As String = String.Empty
                Dim tekiyoShuryoDate As String = String.Empty
                Dim shohizeiRitsu As Double = 0
                Dim memo As String = String.Empty
                '存在しない場合
                '削除対象のデータの適用終了日より後の日付の適用開始日を持つデータが存在するかどうか
                If iRecCnt = 0 Then
                    sql = ""
                    sql = "SELECT "
                    sql = sql & N & "    min(適用開始日) 開始日 "
                    sql = sql & N & " FROM m71_ctax "
                    sql = sql & N & " Where 会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' and 消費税区分 = '" & _db.rmSQ(CommonConst.SHOUHIZEI_KBN) & "' and 適用開始日 > '" & Me.txtTekiyoToDt.Text & "'"
                    Dim oDataSet2 As DataSet = _db.selectDB(sql, RS, iRecCnt2)    '抽出結果をDSへ格納
                    '存在する場合は該当データの適用開始日を削除対象データの適用開始日で更新する
                    If nullToStr(oDataSet2.Tables(RS).Rows(0)("開始日")) <> "NULL" Then
                        '適用開始日はキー項目なのでDELETE／INSERTするためにDELETE前に該当データからキー項目以外の項目（消費税率・適用終了日）を取得しておく
                        tekiyoKaisiDate = DateTime.Parse(oDataSet2.Tables(RS).Rows(0)("開始日")).ToString("yyyy/MM/dd")
                        sql = ""
                        sql = "SELECT "
                        sql = sql & N & "    消費税率, 適用終了日 "
                        sql = sql & N & " FROM m71_ctax "
                        sql = sql & N & " Where 会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' and 消費税区分 = '" & _db.rmSQ(CommonConst.SHOUHIZEI_KBN) & "' and 適用開始日 = '" & tekiyoKaisiDate & "'"
                        Dim oDataSet3 As DataSet = _db.selectDB(sql, RS, iRecCnt3)    '抽出結果をDSへ格納
                        If iRecCnt3 <> 0 Then
                            shohizeiRitsu = _db.rmNullDouble(oDataSet3.Tables(RS).Rows(0)("消費税率"))
                            tekiyoShuryoDate = DateTime.Parse(oDataSet3.Tables(RS).Rows(0)("適用終了日")).ToString("yyyy/MM/dd")
                            memo = _db.rmNullStr(oDataSet3.Tables(RS).Rows(0)("メモ"))
                        End If


                        Try
                            '該当データ削除
                            Dim sql2 As String = ""
                            sql2 = "DELETE FROM m71_ctax"
                            sql2 = sql2 & N & " WHERE 会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"         '会社コード
                            sql2 = sql2 & N & "   AND 消費税区分 = '" & _db.rmSQ(CommonConst.SHOUHIZEI_KBN) & "' "                 '消費税区分
                            sql2 = sql2 & N & "   AND 適用開始日 = '" & Me.txtTekiyoFrDt.Text & "'"

                            Try
                                'sql発行
                                _db.executeDB(sql2)
                            Catch ex As Exception
                                _dbErr = True
                                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
                            End Try

                        Catch lue As UsrDefException
                            Throw lue
                        Catch lex As Exception
                            Throw New UsrDefException(lex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(lex)))       'キャッチした例外をユーザー定義例外に移し変えスロー
                        End Try

                        Try
                            'INSERTデータ削除
                            Dim sql3 As String = ""
                            sql3 = "DELETE FROM m71_ctax"
                            sql3 = sql3 & N & " WHERE 会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"         '会社コード
                            sql3 = sql3 & N & "   AND 消費税区分 = '" & _db.rmSQ(CommonConst.SHOUHIZEI_KBN) & "' "                 '消費税区分
                            sql3 = sql3 & N & "   AND 適用開始日 = '" & tekiyoKaisiDate & "'"

                            Try
                                'sql発行
                                _db.executeDB(sql3)
                            Catch ex As Exception
                                _dbErr = True
                                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
                            End Try

                        Catch lue As UsrDefException
                            Throw lue
                        Catch lex As Exception
                            Throw New UsrDefException(lex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(lex)))       'キャッチした例外をユーザー定義例外に移し変えスロー
                        End Try

                        Try

                            '追加
                            sql = ""
                            sql = "INSERT INTO"
                            sql = sql & N & " m71_ctax "
                            sql = sql & N & "("
                            sql = sql & N & "  会社コード"          '会社コード
                            sql = sql & N & ", 消費税区分"          '消費税区分
                            sql = sql & N & ", 適用開始日"          '適用開始日
                            sql = sql & N & ", 適用終了日"          '適用終了日
                            sql = sql & N & ", 消費税率"            '消費税率
                            sql = sql & N & ", 更新者"              '更新者
                            sql = sql & N & ", 更新日"              '更新日
                            sql = sql & N & ")"
                            sql = sql & N & " VALUES"
                            sql = sql & N & "("
                            sql = sql & N & "  '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "
                            sql = sql & N & ", '" & _db.rmSQ(CommonConst.SHOUHIZEI_KBN) & "' "
                            sql = sql & N & ", '" & Me.txtTekiyoFrDt.Text & "'"
                            sql = sql & N & ", '" & tekiyoShuryoDate & "'"
                            sql = sql & N & ", " & nullToNum(shohizeiRitsu)
                            sql = sql & N & ", '" & _db.rmSQ(frmC01F10_Login.loginValue.TantoCD) & "' "
                            sql = sql & N & ", current_timestamp "
                            sql = sql & N & ")"

                            Try
                                'sql発行
                                _db.executeDB(sql)
                            Catch ex As Exception
                                _dbErr = True
                                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
                            End Try

                        Catch lue As UsrDefException
                            Throw lue
                        Catch lex As Exception
                            Throw New UsrDefException(lex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(lex)))       'キャッチした例外をユーザー定義例外に移し変えスロー
                        End Try

                    Else
                        '存在しない場合は削除対象のデータしか存在しないので削除対象のデータを削除して終了する
                        Try
                            '消費税マスタ削除
                            sql = ""
                            sql = "DELETE FROM m71_ctax"
                            sql = sql & N & " WHERE 会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"           '会社コード
                            sql = sql & N & "   AND 消費税区分 = '" & _db.rmSQ(CommonConst.SHOUHIZEI_KBN) & "' "                   '消費税区分
                            sql = sql & N & "   AND 適用開始日 = '" & Me.txtTekiyoFrDt.Text & "'"

                            Try
                                'sql発行
                                _db.executeDB(sql)
                            Catch ex As Exception
                                _dbErr = True
                                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
                            End Try

                        Catch lue As UsrDefException
                            Throw lue
                        Catch lex As Exception
                            Throw New UsrDefException(lex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(lex)))       'キャッチした例外をユーザー定義例外に移し変えスロー
                        End Try
                    End If
                Else
                    tekiyoKaisiDate = _db.rmNullStr(oDataSet.Tables(RS).Rows(0)("適用開始日"))

                    Try
                        '消費税マスタの最新レコード更新
                        sql = ""
                        sql = "UPDATE m71_ctax"
                        sql = sql & N & " SET"
                        sql = sql & N & "  適用終了日 = '" & Me.txtTekiyoToDt.Text & "'"                             '適用終了日
                        sql = sql & N & " ,更新者 = '" & _db.rmSQ(frmC01F10_Login.loginValue.TantoCD) & "'"                '更新者
                        sql = sql & N & " ,更新日 = current_timestamp"                                               '更新日
                        sql = sql & N & " WHERE 会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"           '会社コード
                        sql = sql & N & "   AND 消費税区分 = '" & _db.rmSQ(CommonConst.SHOUHIZEI_KBN) & "' "                   '消費税区分
                        sql = sql & N & "   AND 適用開始日 = '" & tekiyoKaisiDate & "'"

                        Try
                            'sql発行
                            _db.executeDB(sql)
                        Catch ex As Exception
                            _dbErr = True
                            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
                        End Try

                    Catch lue As UsrDefException
                        Throw lue
                    Catch lex As Exception
                        Throw New UsrDefException(lex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(lex)))       'キャッチした例外をユーザー定義例外に移し変えスロー
                    End Try

                    Try
                        '消費税マスタ削除
                        sql = ""
                        sql = "DELETE FROM m71_ctax"
                        sql = sql & N & " WHERE 会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"      '会社コード
                        sql = sql & N & "   AND 消費税区分 = '" & _db.rmSQ(CommonConst.SHOUHIZEI_KBN) & "' "                               '消費税区分
                        sql = sql & N & "   AND 適用開始日 = '" & Me.txtTekiyoFrDt.Text & "'"

                        Try
                            'sql発行
                            _db.executeDB(sql)
                        Catch ex As Exception
                            _dbErr = True
                            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
                        End Try

                    Catch lue As UsrDefException
                        Throw lue
                    Catch lex As Exception
                        Throw New UsrDefException(lex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(lex)))       'キャッチした例外をユーザー定義例外に移し変えスロー
                    End Try

                End If
            Catch ex As Exception
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
            End Try

        Catch lue As UsrDefException
            Throw lue
        Catch lex As Exception
            Throw New UsrDefException(lex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(lex)))       'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　sql／Update発行
    '　（処理概要）sql文を作成し，ＤＢへの登録を行う
    '-------------------------------------------------------------------------------
    Private Sub Update_M80SHOHIZEI()

        Try
            '消費税マスタ削除
            Dim sql As String = ""
            sql = "DELETE FROM m71_ctax"
            sql = sql & N & " WHERE 会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"       '会社コード
            sql = sql & N & "   AND 消費税区分 = '" & _db.rmSQ(CommonConst.SHOUHIZEI_KBN) & "' "               '消費税区分
            sql = sql & N & "   AND 適用開始日 = '" & Me.txtTekiyoFrDt.Text & "'"

            Try
                'sql発行
                _db.executeDB(sql)
            Catch ex As Exception
                _dbErr = True
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
            End Try

        Catch lue As UsrDefException
            Throw lue
        Catch lex As Exception
            Throw New UsrDefException(lex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(lex)))       'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

        Try

            Dim tekiyoShuryoDate As String = String.Empty
            Dim sql As String = ""
            sql = "SELECT  "
            sql = sql & N & "    min(適用開始日) 開始日 "
            sql = sql & N & " FROM m71_ctax "
            sql = sql & N & " Where 会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' and 消費税区分 = '" & _db.rmSQ(CommonConst.SHOUHIZEI_KBN) & "' and 適用開始日 >= '" & Me.dtpTekiyoFrDt.Value & "'"
            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)
            If reccnt = 0 Then
                tekiyoShuryoDate = TEKIYOSHURYOBI_MAX
            Else
                If _db.rmNullStr(ds.Tables(RS).Rows(0)("開始日")) = "" Then
                    tekiyoShuryoDate = TEKIYOSHURYOBI_MAX
                Else
                    tekiyoShuryoDate = DateTime.Parse(ds.Tables(RS).Rows(0)("開始日")).AddDays(-1).ToString("yyyy/MM/dd")
                End If
            End If

            '消費税マスタ追加
            sql = ""
            sql = "INSERT INTO"
            sql = sql & N & " m71_ctax "
            sql = sql & N & "("
            sql = sql & N & "  会社コード"          '会社コード
            sql = sql & N & ", 消費税区分"          '消費税区分
            sql = sql & N & ", 適用開始日"          '適用開始日
            sql = sql & N & ", 適用終了日"          '適用終了日
            sql = sql & N & ", 消費税率"            '消費税率
            sql = sql & N & ", 更新者"              '更新者
            sql = sql & N & ", 更新日"              '更新日
            sql = sql & N & ")"
            sql = sql & N & " VALUES"
            sql = sql & N & "("
            sql = sql & N & "  '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"
            sql = sql & N & ", '" & _db.rmSQ(CommonConst.SHOUHIZEI_KBN) & "'"
            If _ShoriMode = CommonConst.MODE_EditStatus Then
                sql = sql & N & ", '" & Me.dtpTekiyoFrDt.Value & "'"
            Else
                sql = sql & N & ", '" & Me.dtpTekiyoFrDt.Text & "'"
            End If
            sql = sql & N & ", '" & tekiyoShuryoDate & "'"
            sql = sql & N & ", " & nullToNum(Me.txtShohizeiRitsu.Value)
            sql = sql & N & ", '" & _db.rmSQ(frmC01F10_Login.loginValue.TantoCD) & "'"
            sql = sql & N & ", current_timestamp "
            sql = sql & N & ")"

            Try
                'sql発行
                _db.executeDB(sql)
            Catch ex As Exception
                _dbErr = True
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
            End Try

        Catch lue As UsrDefException
            Throw lue
        Catch lex As Exception
            Throw New UsrDefException(lex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(lex)))       'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

    End Sub

    'マスタデータ取得
    Private Sub getMasterData()

        'データ初期表示
        'キー項目を取得
        Dim tekiyoFrDT As String = psTekiyoFrDT

        Dim sql As String = ""

        Try
            sql = "SELECT "
            sql = sql & N & "  ct.消費税区分, ct.適用開始日, ct.適用終了日, ct.消費税率, ct.更新者, ct.更新日 "
            sql = sql & N & ", u.氏名 更新者名 "
            sql = sql & N & " FROM m71_ctax ct "
            sql = sql & N & " LEFT JOIN m02_user u on u.会社コード = ct.会社コード AND u.ユーザＩＤ = ct.更新者 "
            sql = sql & N & " Where ct.会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' and ct.消費税区分 = '" & _db.rmSQ(CommonConst.SHOUHIZEI_KBN) & "' and ct.適用開始日 = '" & tekiyoFrDT & "'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

            If _ShoriMode <> CommonConst.MODE_ADDNEWCOPY Then
                Me.dtpTekiyoFrDt.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("適用開始日"))
                Me.txtTekiyoFrDt.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("適用開始日"))
                Me.dtpTekiyoToDt.Text = DateAdd("d", 0, ds.Tables(RS).Rows(0)("適用終了日")).ToString("yyyy/MM/dd")
                Me.txtTekiyoToDt.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("適用終了日"))
                Me.lblKousinsya.Text = MIDASHI_KOUSINSYA & _db.rmNullStr(ds.Tables(RS).Rows(0)("更新者名"))
                Me.lblKousinbi.Text = MIDASHI_KOUSINBI & _db.rmNullStr(ds.Tables(RS).Rows(0)("更新日"))
            End If
            Me.txtShohizeiRitsu.Text = _db.rmNullDouble(ds.Tables(RS).Rows(0)("消費税率"))

            _TekiyoToDT = _db.rmNullStr(ds.Tables(RS).Rows(0)("適用終了日"))

            _UpdateTime = _db.rmNullStr(ds.Tables(RS).Rows(0)("更新日"))

        Catch ue As UsrDefException
            ue.dspMsg()
            Throw ue
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '画面項目のクリア
    Private Sub initializeDisplay()

        Me.dtpTekiyoFrDt.Value = DateTime.Now.ToString()
        Me.dtpTekiyoFrDt.Text = String.Empty
        Me.txtTekiyoFrDt.Text = String.Empty
        Me.dtpTekiyoToDt.Value = DateTime.Now.ToString()
        Me.dtpTekiyoToDt.Text = String.Empty
        Me.txtTekiyoToDt.Text = String.Empty
        Me.txtShohizeiRitsu.Text = String.Empty

        Me.lblKousinsya.Text = MIDASHI_KOUSINSYA
        Me.lblKousinbi.Text = MIDASHI_KOUSINBI

    End Sub

    '背景色セット
    Private Sub setBackColor()

        Select Case _ShoriMode
            Case CommonConst.MODE_EditStatus     '変更
                Me.txtTekiyoFrDt.BackColor = _readOnlyTextBackColor
                Me.txtTekiyoToDt.BackColor = _readOnlyTextBackColor
            Case CommonConst.MODE_ADDNEW, CommonConst.MODE_ADDNEWCOPY     '新規追加,複写新規
                Me.txtTekiyoToDt.BackColor = _readOnlyTextBackColor
            Case CommonConst.MODE_InquiryStatus, CommonConst.MODE_DELETE     '参照,削除
                Me.txtTekiyoFrDt.BackColor = _readOnlyTextBackColor
                Me.txtTekiyoToDt.BackColor = _readOnlyTextBackColor
                Me.txtShohizeiRitsu.BackColor = _readOnlyTextBackColor
        End Select

    End Sub

    '画面項目のプロパティセット
    Private Sub setProperty()

        Select Case _ShoriMode
            Case CommonConst.MODE_InquiryStatus, CommonConst.MODE_DELETE     '参照,削除
                Me.dtpTekiyoFrDt.Visible = False
                Me.txtTekiyoFrDt.Visible = True
                Me.dtpTekiyoToDt.Visible = False
                Me.txtTekiyoToDt.Visible = True
                Me.txtShohizeiRitsu.ReadOnly = True
                Me.txtShohizeiRitsu.TabStop = False
            Case CommonConst.MODE_ADDNEW, CommonConst.MODE_ADDNEWCOPY     '新規追加,複写新規
                Me.dtpTekiyoFrDt.Visible = True
                Me.txtTekiyoFrDt.Visible = False
                Me.txtShohizeiRitsu.ReadOnly = False
                Me.dtpTekiyoToDt.Visible = False
                Me.txtTekiyoToDt.Visible = True
                Me.dtpTekiyoToDt.TabStop = False
            Case CommonConst.MODE_EditStatus     '変更
                Me.txtShohizeiRitsu.ReadOnly = False
                Me.dtpTekiyoFrDt.Visible = False
                Me.txtTekiyoFrDt.Visible = True
                Me.txtTekiyoFrDt.ReadOnly = True
                Me.txtShohizeiRitsu.TabStop = True
                Me.dtpTekiyoToDt.Visible = False
                Me.txtTekiyoToDt.Visible = True
                Me.dtpTekiyoToDt.TabStop = False
        End Select

    End Sub

    Private Function nullToStr(ByVal obj As Object) As String
        If (obj Is DBNull.Value) Then
            Return "NULL"
        Else
            Return "'" & obj & "'"
        End If
    End Function

    Private Function nullToNum(ByVal obj As Object) As String
        If (obj Is DBNull.Value Or obj.ToString() = "") Then
            Return "NULL"
        Else
            Return obj
        End If
    End Function

    Private Sub ctl_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles dtpTekiyoFrDt.KeyPress,
                                                                                                    dtpTekiyoToDt.KeyPress,
                                                                                                    txtShohizeiRitsu.KeyPress

        UtilMDL.UtilClass.moveNextFocus(Me, e)
    End Sub

    '-------------------------------------------------------------------------------
    '　フォーカス取得イベント
    '-------------------------------------------------------------------------------
    Private Sub ctl_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpTekiyoFrDt.GotFocus,
                                                                                          dtpTekiyoToDt.GotFocus,
                                                                                          txtShohizeiRitsu.GotFocus

        'フォーカス取得時、入力パラメタのコントロールを全選択状態とする
        Call UtilClass.selAll(sender)

    End Sub

#End Region

End Class