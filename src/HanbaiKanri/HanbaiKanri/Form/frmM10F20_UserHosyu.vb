'===============================================================================
'　 （システム名）      株式会社 全備様向け　販売管理システム
'
'   （機能名）          ユーザマスタ保守
'   （クラス名）        frmM10F20_UserHosyu
'   （処理機能名）      
'   （本MDL使用前提）   
'   （備考）         
'
'===============================================================================
' 履歴  名前               日付       　　　 　内容
'-------------------------------------------------------------------------------
'  (1)  桜井               2018/03/15      　　新規
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.DB
Imports UtilMDL.MSG             'UtilMsgHandler用

Public Class frmM10F20_UserHosyu
#Region "宣言"

    '-------------------------------------------------------------------------------
    ' 定数宣言
    '-------------------------------------------------------------------------------
    ' PG制御文字
    Private Const N As String = ControlChars.NewLine                    ' 改行文字
    Private Const RS As String = "RecSet"                               ' レコードセットテーブル

    '汎用マスメン
    Private Const lM1020_DB_EXIST As Integer = 1                        'あり：登録済
    Private Const lM1020_DB_NONE As Integer = 0                         'なし：未登録

    '更新者・更新日見出しラベル
    Private Const MIDASHI_KOUSINSYA As String = "更新者："
    Private Const MIDASHI_KOUSINBI As String = "更新日："

    '無効フラグ
    Private Const MUKOFLG_YUKO As Integer = 0                           '有効
    Private Const MUKOFLG_MUKO As Integer = 1                           '無効

    'パスワードマスタ初期パスワード
    Private Const PASSWORD_ZANTEI As String = "xxxxxx"                  '初期パスワード　★暫定

    'パスワード変更方法
    Private Const PASSWORD_CHANGE_RESET As Integer = 2                  'リセット

    '-------------------------------------------------------------------------------
    'メンバー変数宣言
    '-------------------------------------------------------------------------------
    Private _db As UtilDBIf
    Private _msgHd As UtilMsgHandler
    Private _parentForm As frmM10F10_UserList
    Private _ShoriMode As Integer
    Private _comLogc As CommonLogic                         '共通処理用
    Private _open As Boolean = False                        '画面起動済フラグ
    Private _dbErr As Boolean = False                       'DB登録エラー判定用
    Private _companyCd As String
    Private _selectId As String
    Private _userId As String
    Private Shared _shoriId As String
    Private _UpdateTime As String

    Private _readOnlyTextBackColor As Color

    'データDB更新フラグ(DB登録判定)
    Private pCS1020_DBFlg As Long

    'ユーザＩＤ格納用変数
    Private psUserID As String

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
                   ByVal prmParentForm As frmM10F10_UserList,
                   ByVal prmMode As Integer,
                   ByRef prmSelectID As String,
                   ByRef prmUserID As String)

        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _parentForm = prmParentForm
        _ShoriMode = prmMode
        psUserID = prmUserID
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

        'ボタン使用可不可設定
        Select Case prmMode
            Case CommonConst.MODE_InquiryStatus                                                         '参照
                cmdKakutei.Enabled = False
            Case CommonConst.MODE_ADDNEW, CommonConst.MODE_ADDNEWCOPY, CommonConst.MODE_EditStatus, CommonConst.MODE_DELETE      '新規,複写新規,変更,削除
                cmdKakutei.Enabled = True
        End Select
        cmdModoru.Enabled = True

        '新規以外
        If prmMode <> CommonConst.MODE_ADDNEW Then
            'マスタデータ取得
            getMasterData()
        End If

        If prmMode = CommonConst.MODE_ADDNEW Or prmMode = CommonConst.MODE_ADDNEWCOPY Then
            Me.chkPWDInit.Checked = True
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
    Private Sub frmM10F20_UserHosyu_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim BkCur As Cursor = Cursor.Current                                ' 現在のカーソルを取っておく
        Try
            Cursor.Current = Cursors.WaitCursor                             ' 砂時計カーソルに入れ替える

            _open = True                                                    '画面起動済フラグ

            '操作履歴ログ作成
            Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, CommonConst.MENU_M1002, CommonConst.PROGRAM_UPDATE, CommonConst.STATUS_NORMAL,
                                                Me.txtUserID.Text, DBNull.Value, lblShoriMode.Text, DBNull.Value, DBNull.Value,
                                                DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _userId)


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

        Try

            '排他チェック処理
            Select Case _ShoriMode
                Case CommonConst.MODE_ADDNEW   '登録
                '新規登録の場合は処理不要。

                Case CommonConst.MODE_EditStatus   '変更
                    Try
                        Dim sql As String = ""
                        sql = "SELECT  "
                        sql = sql & "    c.更新日,c.更新者 "
                        sql = sql & " FROM m02_user c "
                        sql = sql & " Where c.会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' and c.ユーザＩＤ = '" & _db.rmSQ(Me.txtUserID.Text) & "'"
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
                    If M10USER_Add_Ctl() = False Then
                        Exit Sub
                    End If
                Case CommonConst.MODE_ADDNEWCOPY    '複写新規
                    If M10USER_Add_Ctl() = False Then
                        Exit Sub
                    End If
                Case CommonConst.MODE_EditStatus    '変更
                    If M10USER_Upd_Ctl() = False Then
                        Exit Sub
                    End If
                Case CommonConst.MODE_DELETE        '削除
                    If M10USER_Del_Ctl() = False Then
                        Exit Sub
                    End If
            End Select

            '操作履歴ログ作成 
            Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, CommonConst.MENU_M1002, CommonConst.PROGRAM_REPORT, CommonConst.STATUS_NORMAL,
                                                Me.txtUserID.Text, DBNull.Value, lblShoriMode.Text, DBNull.Value, DBNull.Value,
                                                DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _userId)

            '更新フラグ：登録済
            pCS1020_DBFlg = lM1020_DB_EXIST

            'ユーザＩＤを親フォームに返却（新規・複写新規は保守画面の入力値）
            '選択行の位置づけに使用
            _parentForm.userID = Me.txtUserID.Text
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
    Private Function M10USER_Add_Ctl() As Boolean

        M10USER_Add_Ctl = False

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
            Call Insert_M10USER()

            'パスワード初期化チェックボックスチェック時、パスワードマスタ更新処理実行
            If Me.chkPWDInit.Checked = True Then
                Call Update_M03PSWD()
            End If

            '完了メッセージ表示
            Call _msgHd.dspMSG("completeInsert")                                      '登録が完了しました。

            'トランザクション終了
            _db.commitTran()

            M10USER_Add_Ctl = True

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
    Private Function M10USER_Upd_Ctl() As Boolean

        M10USER_Upd_Ctl = False

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

            '削除処理
            Call Delete_M10USER()

            '追加処理
            Call Insert_M10USER()

            'パスワード初期化チェックボックスチェック時、パスワードマスタ更新処理実行
            If Me.chkPWDInit.Checked = True Then
                Call Update_M03PSWD()
            End If

            '完了メッセージ表示
            Call _msgHd.dspMSG("completeUpdate")                                      '登録が完了しました。

            'トランザクション終了
            _db.commitTran()

            M10USER_Upd_Ctl = True

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
    Private Function M10USER_Del_Ctl() As Boolean

        M10USER_Del_Ctl = False

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
            Call Delete_M10USER()

            '完了メッセージ表示
            Call _msgHd.dspMSG("completeDelete")                                      '削除が完了しました。

            'トランザクション終了
            _db.commitTran()

            M10USER_Del_Ctl = True

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
                Throw New UsrDefException("重複データが存在します。", _msgHd.getMSG("RegDupKeyData"), txtUserID)
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

        If "".Equals(txtUserID.Text) Then 'ユーザＩＤ
            Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【ユーザＩＤ】"), txtUserID)
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
            sql = sql & N & " m02_user"
            sql = sql & N & " WHERE"
            sql = sql & N & "       会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"
            sql = sql & N & "   AND ユーザＩＤ = '" & _db.rmSQ(Me.txtUserID.Text) & "'"

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
    Private Sub Insert_M10USER()

        Try
            '商品マスタ追加
            Dim sql As String = ""
            sql = "INSERT INTO"
            sql = sql & N & " m02_user "
            sql = sql & N & "("
            sql = sql & N & "  会社コード"            '会社コード
            sql = sql & N & ", ユーザＩＤ"            'ユーザＩＤ
            sql = sql & N & ", 氏名"                '氏名
            sql = sql & N & ", 略名"            '略名
            sql = sql & N & ", 備考"            '備考
            sql = sql & N & ", 無効フラグ"          '無効フラグ
            sql = sql & N & ", 更新者"                '更新者
            sql = sql & N & ", 更新日"                '更新日
            sql = sql & N & ")"
            sql = sql & N & " VALUES"
            sql = sql & N & "("
            sql = sql & N & "  '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"
            sql = sql & N & ", '" & _db.rmSQ(Me.txtUserID.Text) & "'"
            sql = sql & N & ", '" & _db.rmSQ(Me.txtSimei.Text) & "'"
            sql = sql & N & ", '" & _db.rmSQ(Me.txtRyakumei.Text) & "'"
            sql = sql & N & ", '" & _db.rmSQ(Me.txtBiko.Text) & "'"
            '無効フラグ
            If Me.chkMukoFlg.Checked Then
                sql = sql & N & ", " & MUKOFLG_MUKO                   '無効
            Else
                sql = sql & N & ", " & MUKOFLG_YUKO                   '有効
            End If
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
    Private Sub Delete_M10USER()

        Try
            'ユーザマスタ更新
            Dim sql As String = ""
            sql = "DELETE FROM m02_user"
            sql = sql & N & " WHERE 会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"                   '会社コード
            sql = sql & N & "   AND ユーザＩＤ = '" & _db.rmSQ(Me.txtUserID.Text) & "' "                               'ユーザＩＤ

            Try
                'sql発行
                _db.executeDB(sql)
            Catch ex As Exception
                _dbErr = True
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
            End Try

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
    '　sql／Insert発行
    '　（処理概要）sql文を作成し，ＤＢへの登録を行う
    '-------------------------------------------------------------------------------
    Private Sub Update_M03PSWD()

        Dim maxSedai As Integer = 0
        Dim todayCnt As Integer = 0
        Dim sql As String = ""

        '運用開始日=システム日付のデータ件数を取得
        Try
            'sql編集
            sql = ""
            sql = "SELECT count(*) 件数"
            sql = sql & N & " FROM"
            sql = sql & N & " m03_pswd"
            sql = sql & N & " WHERE"
            sql = sql & N & "       会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"
            sql = sql & N & "   AND ユーザＩＤ = '" & _db.rmSQ(Me.txtUserID.Text) & "'"
            sql = sql & N & "   AND 適用開始日 = current_date "

            Dim iRecCnt As Integer = 0
            Try
                'sql発行
                Dim oDataSet As DataSet = _db.selectDB(sql, RS, iRecCnt)    '抽出結果をDSへ格納
                todayCnt = _db.rmNullInt(oDataSet.Tables(RS).Rows(0)("件数"))
            Catch ex As Exception
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
            End Try

        Catch lue As UsrDefException
            Throw lue
        Catch lex As Exception
            Throw New UsrDefException(lex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(lex)))       'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

        '運用開始日=システム日付のデータ件数が0件
        If todayCnt = 0 Then
            Try
                'パスワードマスタ更新
                sql = ""
                sql = "UPDATE m03_pswd"
                sql = sql & N & " SET"
                sql = sql & N & "  適用終了日 = current_date - 1"                                      '適用終了日
                sql = sql & N & " ,更新者 = '" & _db.rmSQ(frmC01F10_Login.loginValue.TantoCD) & "'"          '更新者
                sql = sql & N & " ,更新日 = current_timestamp"                                         '更新日
                sql = sql & N & " WHERE 会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"     '会社コード
                sql = sql & N & "   AND ユーザＩＤ = '" & _db.rmSQ(Me.txtUserID.Text) & "'"                      'ユーザＩＤ
                sql = sql & N & "   AND 適用終了日 = (SELECT max(適用終了日) FROM m03_pswd "
                sql = sql & N & "                     WHERE 会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"     '会社コード
                sql = sql & N & "                     AND ユーザＩＤ = '" & _db.rmSQ(Me.txtUserID.Text) & "')"                      'ユーザＩＤ

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
            Try
                'パスワードマスタ更新
                sql = ""
                sql = "UPDATE m03_pswd"
                sql = sql & N & " SET"
                sql = sql & N & "  適用終了日 = current_date"                                          '適用終了日
                sql = sql & N & " ,更新者 = '" & _db.rmSQ(frmC01F10_Login.loginValue.TantoCD) & "'"          '更新者
                sql = sql & N & " ,更新日 = current_timestamp"                                         '更新日
                sql = sql & N & " WHERE 会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"     '会社コード
                sql = sql & N & "   AND ユーザＩＤ = '" & _db.rmSQ(Me.txtUserID.Text) & "'"                      'ユーザＩＤ
                sql = sql & N & "   AND 適用終了日 = (SELECT max(適用終了日) FROM m03_pswd "
                sql = sql & N & "                     WHERE 会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"     '会社コード
                sql = sql & N & "                     AND ユーザＩＤ = '" & _db.rmSQ(Me.txtUserID.Text) & "')"                      'ユーザＩＤ

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

        '最大の世代番号を取得
        Try
            'sql編集
            sql = ""
            sql = "SELECT MAX(世代番号) 最大世代番号"
            sql = sql & N & " FROM"
            sql = sql & N & " m03_pswd"
            sql = sql & N & " WHERE"
            sql = sql & N & "       会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "'"
            sql = sql & N & "   AND ユーザＩＤ = '" & _db.rmSQ(Me.txtUserID.Text) & "'"

            Dim iRecCnt As Integer = 0
            Try
                'sql発行
                Dim oDataSet As DataSet = _db.selectDB(sql, RS, iRecCnt)    '抽出結果をDSへ格納
                maxSedai = _db.rmNullInt(oDataSet.Tables(RS).Rows(0)("最大世代番号"))
            Catch ex As Exception
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
            End Try

        Catch lue As UsrDefException
            Throw lue
        Catch lex As Exception
            Throw New UsrDefException(lex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(lex)))       'キャッチした例外をユーザー定義例外に移し変えスロー
        End Try

        Try
            'パスワードマスタ追加
            sql = ""
            sql = "INSERT INTO"
            sql = sql & N & " m03_pswd "
            sql = sql & N & "("
            sql = sql & N & "  会社コード"           '会社コード
            sql = sql & N & ", ユーザＩＤ"           'ユーザＩＤ
            sql = sql & N & ", 適用開始日"           '適用開始日
            sql = sql & N & ", 適用終了日"           '適用終了日
            sql = sql & N & ", パスワード"           'パスワード
            sql = sql & N & ", パスワード変更方法"   'パスワード変更方法
            sql = sql & N & ", 世代番号"             '世代番号
            sql = sql & N & ", 有効期限"             '有効期限
            sql = sql & N & ", 更新者"               '更新者
            sql = sql & N & ", 更新日"               '更新日
            sql = sql & N & ")"
            sql = sql & N & " VALUES"
            sql = sql & N & "("
            sql = sql & N & "  '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "        '会社コード
            sql = sql & N & ", '" & _db.rmSQ(Me.txtUserID.Text) & "' "                         'ユーザＩＤ
            sql = sql & N & ", current_date "                                           '運用開始日
            sql = sql & N & ", '2099-12-31' "                                           '運用終了日
            sql = sql & N & ", '" & _db.rmSQ(PASSWORD_ZANTEI) & "' "                              '新パスワード
            sql = sql & N & ", " & PASSWORD_CHANGE_RESET                                'パスワード変更方法　固定値"2"（リセット）
            sql = sql & N & ", " & maxSedai + 1                                         '世代番号
            sql = sql & N & ", '2099-12-31' "                                           '有効期限
            sql = sql & N & ", '" & _db.rmSQ(frmC01F10_Login.loginValue.TantoCD) & "' "        '更新者
            sql = sql & N & ", current_timestamp "                                      '更新日
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
        Dim userID As String = psUserID

        Dim sql As String = ""

        Try
            sql = "SELECT "
            sql = sql & N & "  u.ユーザＩＤ, u.氏名, u.略名, u.備考, u.無効フラグ, u.更新者, u.更新日 "
            sql = sql & N & ", su.氏名 更新者名 "
            sql = sql & N & " FROM m02_user u "
            sql = sql & N & " LEFT JOIN m02_user su on su.会社コード = u.会社コード AND su.ユーザＩＤ = u.更新者 "
            sql = sql & N & " Where u.会社コード = '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' and u.ユーザＩＤ = '" & _db.rmSQ(userID) & "'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

            If _ShoriMode <> CommonConst.MODE_ADDNEWCOPY Then
                Me.txtUserID.Text = userID
            End If

            Me.txtSimei.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("氏名"))
            Me.txtRyakumei.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("略名"))
            Me.txtBiko.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("備考"))
            If Not String.IsNullOrEmpty(_db.rmNullStr(ds.Tables(RS).Rows(0)("無効フラグ"))) Then
                Select Case _db.rmNullStr(ds.Tables(RS).Rows(0)("無効フラグ"))
                    Case "0"
                        Me.chkMukoFlg.Checked = False
                    Case "1"
                        Me.chkMukoFlg.Checked = True
                End Select
            End If
            If _ShoriMode <> CommonConst.MODE_ADDNEWCOPY Then '複写新規以外
                Me.lblKousinsya.Text = MIDASHI_KOUSINSYA & _db.rmNullStr(ds.Tables(RS).Rows(0)("更新者名"))
                Me.lblKousinbi.Text = MIDASHI_KOUSINBI & _db.rmNullStr(ds.Tables(RS).Rows(0)("更新日"))
            End If

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

        Me.txtUserID.Text = String.Empty

        Me.txtSimei.Text = String.Empty
        Me.txtRyakumei.Text = String.Empty
        Me.txtBiko.Text = String.Empty
        Me.chkMukoFlg.Checked = False
        Me.chkPWDInit.Checked = False

        Me.lblKousinsya.Text = MIDASHI_KOUSINSYA
        Me.lblKousinbi.Text = MIDASHI_KOUSINBI

    End Sub

    '背景色セット
    Private Sub setBackColor()

        Select Case _ShoriMode
            Case CommonConst.MODE_EditStatus     '変更
                Me.txtUserID.BackColor = _readOnlyTextBackColor
            Case CommonConst.MODE_InquiryStatus, CommonConst.MODE_DELETE     '参照,削除
                Me.txtUserID.BackColor = _readOnlyTextBackColor
                Me.txtSimei.BackColor = _readOnlyTextBackColor
                Me.txtRyakumei.BackColor = _readOnlyTextBackColor
                Me.txtBiko.BackColor = _readOnlyTextBackColor
        End Select

    End Sub

    '画面項目のプロパティセット
    Private Sub setProperty()

        Select Case _ShoriMode
            Case CommonConst.MODE_InquiryStatus, CommonConst.MODE_DELETE     '参照,削除
                Me.txtUserID.ReadOnly = True
                Me.txtSimei.ReadOnly = True
                Me.txtRyakumei.ReadOnly = True
                Me.txtBiko.ReadOnly = True
                Me.chkMukoFlg.Enabled = False
                Me.chkPWDInit.Enabled = False
                Me.txtUserID.TabStop = False
                Me.txtSimei.TabStop = False
                Me.txtRyakumei.TabStop = False
                Me.txtBiko.TabStop = False
                Me.chkMukoFlg.TabStop = False
                Me.chkPWDInit.TabStop = False
            Case CommonConst.MODE_ADDNEW, CommonConst.MODE_ADDNEWCOPY     '新規追加,複写新規
                Me.txtUserID.ReadOnly = False
                Me.txtSimei.ReadOnly = False
                Me.txtRyakumei.ReadOnly = False
                Me.txtBiko.ReadOnly = False
                Me.chkMukoFlg.Enabled = False
                Me.chkPWDInit.Enabled = False
                Me.chkMukoFlg.TabStop = False
                Me.chkPWDInit.TabStop = False
            Case CommonConst.MODE_EditStatus     '変更
                Me.txtUserID.ReadOnly = True
                Me.txtSimei.ReadOnly = False
                Me.txtRyakumei.ReadOnly = False
                Me.txtBiko.ReadOnly = False
                Me.txtUserID.TabStop = False
        End Select

    End Sub

    Private Sub ctl_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtUserID.KeyPress,
                                                                                                    txtSimei.KeyPress,
                                                                                                    txtRyakumei.KeyPress,
                                                                                                    txtBiko.KeyPress

        UtilMDL.UtilClass.moveNextFocus(Me, e)
    End Sub

    '-------------------------------------------------------------------------------
    '　フォーカス取得イベント
    '-------------------------------------------------------------------------------
    Private Sub ctl_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserID.GotFocus,
                                                                                          txtSimei.GotFocus,
                                                                                          txtRyakumei.GotFocus,
                                                                                          txtBiko.GotFocus

        'フォーカス取得時、入力パラメタのコントロールを全選択状態とする
        Call UtilClass.selAll(sender)

    End Sub

#End Region

End Class