'===============================================================================
'　 （システム名）      株式会社 全備様向け　販売管理システム
'
'   （機能名）          汎用マスタ保守
'   （クラス名）        frmM70F20_HanyoHosyu
'   （処理機能名）      
'   （本MDL使用前提）   
'   （備考）         
'
'===============================================================================
' 履歴  名前               日付       　　　 　内容
'-------------------------------------------------------------------------------
'  (1)  鴫原               2018/03/02      　　新規
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.DB
Imports UtilMDL.MSG             'UtilMsgHandler用

Public Class frmM70F20_HanyoHosyu
#Region "宣言"

    '-------------------------------------------------------------------------------
    ' 定数宣言
    '-------------------------------------------------------------------------------
    ' PG制御文字
    Private Const N As String = ControlChars.NewLine                    ' 改行文字
    Private Const RS As String = "RecSet"                               ' レコードセットテーブル

    '汎用マスメン
    Private Const lM7020_DB_EXIST As Integer = 1                        'あり：登録済
    Private Const lM7020_DB_NONE As Integer = 0                         'なし：未登録

    '更新者・更新日見出しラベル
    Private Const MIDASHI_KOUSINSYA As String = "更新者："
    Private Const MIDASHI_KOUSINBI As String = "更新日："

    '-------------------------------------------------------------------------------
    'メンバー変数宣言
    '-------------------------------------------------------------------------------
    Private _db As UtilDBIf
    Private _msgHd As UtilMsgHandler
    Private _parentForm As frmM70F10_HanyoList
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
    Private pCS7020_DBFlg As Long

    '固定キー格納用変数
    Private psKoteiKey As String
    Private psKahenKey As String

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
                   ByVal prmParentForm As frmM70F10_HanyoList,
                   ByVal prmMode As Integer,
                   ByRef prmSelectID As String,
                   ByRef prmKoteiKey As String,
                   ByRef prmKahenKey As String)

        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _parentForm = prmParentForm
        _ShoriMode = prmMode
        psKoteiKey = prmKoteiKey
        psKahenKey = prmKahenKey
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
            '汎用マスタ情報表示
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
    Private Sub frmM70F20_HanyoHosyu_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim BkCur As Cursor = Cursor.Current                                ' 現在のカーソルを取っておく
        Try
            Cursor.Current = Cursors.WaitCursor                             ' 砂時計カーソルに入れ替える

            _open = True                                                    '画面起動済フラグ

            '操作履歴ログ作成
            Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, CommonConst.MENU_M7002, CommonConst.PROGRAM_UPDATE, CommonConst.STATUS_NORMAL,
                                                psKoteiKey, Me.txtKahenKey.Text, lblShoriMode.Text, DBNull.Value, DBNull.Value,
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
                        sql = sql & N & "    c.更新日,c.更新者 "
                        sql = sql & N & " FROM M90_HANYO c "
                        sql = sql & N & " Where c.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and c.固定キー = '" & psKoteiKey & "' and c.可変キー = '" & Me.txtKahenKey.Text & "'"
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
                Case CommonConst.MODE_ADDNEW     '新規
                    If M90HANYO_Add_Ctl() = False Then
                        Exit Sub
                    End If
                Case CommonConst.MODE_ADDNEWCOPY     '複写新規
                    If M90HANYO_Add_Ctl() = False Then
                        Exit Sub
                    End If
                Case CommonConst.MODE_EditStatus     '変更
                    If M90HANYO_Upd_Ctl() = False Then
                        Exit Sub
                    End If
                Case CommonConst.MODE_DELETE     '削除
                    If M90HANYO_Del_Ctl() = False Then
                        Exit Sub
                    End If
            End Select

            '操作履歴ログ作成 
            Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, CommonConst.MENU_M7002, CommonConst.PROGRAM_REPORT, CommonConst.STATUS_NORMAL,
                                                psKoteiKey, Me.txtKahenKey.Text, lblShoriMode.Text, DBNull.Value, DBNull.Value,
                                                DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _userId)

            '更新フラグ：登録済
            pCS7020_DBFlg = lM7020_DB_EXIST

            '可変キーを親フォームに返却（新規・複写新規は保守画面の入力値）
            '選択行の位置づけに使用
            _parentForm.kahenKey = Me.txtKahenKey.Text
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
    Private Function M90HANYO_Add_Ctl() As Boolean

        M90HANYO_Add_Ctl = False

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
            Call Insert_M90HANYO()

            '完了メッセージ表示
            Call _msgHd.dspMSG("completeInsert")                                      '登録が完了しました。

            'トランザクション終了
            _db.commitTran()

            M90HANYO_Add_Ctl = True

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
    Private Function M90HANYO_Upd_Ctl() As Boolean

        M90HANYO_Upd_Ctl = False

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
            Call Delete_M90HANYO()

            '追加処理
            Call Insert_M90HANYO()

            '完了メッセージ表示
            Call _msgHd.dspMSG("completeUpdate")                                      '登録が完了しました。

            'トランザクション終了
            _db.commitTran()

            M90HANYO_Upd_Ctl = True

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
    Private Function M90HANYO_Del_Ctl() As Boolean

        M90HANYO_Del_Ctl = False

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
            Call Delete_M90HANYO()

            '完了メッセージ表示
            Call _msgHd.dspMSG("completeDelete")                                      '削除が完了しました。

            'トランザクション終了
            _db.commitTran()

            M90HANYO_Del_Ctl = True

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

        If "".Equals(txtKahenKey.Text) Then '可変キー
            Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【可変キー】"), txtKahenKey)
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
            sql = sql & N & " m90_hanyo"
            sql = sql & N & " WHERE"
            sql = sql & N & "       会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            sql = sql & N & "   AND 固定キー = '" & psKoteiKey & "'"
            sql = sql & N & "   AND 可変キー = '" & Me.txtKahenKey.Text & "'"

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
    Private Sub Insert_M90HANYO()

        Try
            '汎用マスタ追加
            Dim sql As String = ""
            sql = "INSERT INTO"
            sql = sql & N & " m90_hanyo "
            sql = sql & N & "("
            sql = sql & N & "  会社コード"          '会社コード
            sql = sql & N & ", 固定キー"            '固定キー
            sql = sql & N & ", 可変キー"            '可変キー
            sql = sql & N & ", 表示順"              '表示順
            sql = sql & N & ", 文字１"              '文字１
            sql = sql & N & ", 文字２"              '文字２
            sql = sql & N & ", 文字３"              '文字３
            sql = sql & N & ", 文字４"              '文字４
            sql = sql & N & ", 文字５"              '文字５
            sql = sql & N & ", 数値１"              '数値１
            sql = sql & N & ", 数値２"              '数値２
            sql = sql & N & ", 数値３"              '数値３
            sql = sql & N & ", 数値４"              '数値４
            sql = sql & N & ", 数値５"              '数値５
            sql = sql & N & ", メモ"                'メモ
            sql = sql & N & ", 更新者"              '更新者
            sql = sql & N & ", 更新日"              '更新日
            sql = sql & N & ")"
            sql = sql & N & " VALUES"
            sql = sql & N & "("
            sql = sql & N & "  " & nullToStr(frmC01F10_Login.loginValue.BumonCD)
            sql = sql & N & ", " & nullToStr(psKoteiKey)
            sql = sql & N & ", " & nullToStr(Me.txtKahenKey.Text)
            sql = sql & N & ", " & nullToNum(Me.txtHyoujiJun.Text)
            sql = sql & N & ", " & nullToStr(Me.txtMoji1.Text)
            sql = sql & N & ", " & nullToStr(Me.txtMoji2.Text)
            sql = sql & N & ", " & nullToStr(Me.txtMoji3.Text)
            sql = sql & N & ", " & nullToStr(Me.txtMoji4.Text)
            sql = sql & N & ", " & nullToStr(Me.txtMoji5.Text)
            sql = sql & N & ", " & nullToNum(Me.txtSuuti1.Text)
            sql = sql & N & ", " & nullToNum(Me.txtSuuti2.Text)
            sql = sql & N & ", " & nullToNum(Me.txtSuuti3.Text)
            sql = sql & N & ", " & nullToNum(Me.txtSuuti4.Text)
            sql = sql & N & ", " & nullToNum(Me.txtSuuti5.Text)
            sql = sql & N & ", " & nullToStr(Me.txtMemo.Text)
            sql = sql & N & ", " & nullToStr(frmC01F10_Login.loginValue.TantoCD)
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
    Private Sub Delete_M90HANYO()

        Try
            '汎用マスタ更新
            Dim sql As String = ""
            sql = "DELETE FROM m90_hanyo"
            sql = sql & N & " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"      '会社コード
            sql = sql & N & "   AND 固定キー = '" & psKoteiKey & "' "                               '固定キー
            sql = sql & N & "   AND 可変キー = '" & Me.txtKahenKey.Text & "' "                      '可変キー

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
        Dim koteiKey As String = psKoteiKey
        Dim kahenKey As String = psKahenKey

        Dim sql As String = ""

        Try
            sql = "SELECT "
            sql = sql & N & "    h.表示順,h.文字１, h.文字２, h.文字３, h.文字４, h.文字５, h.数値１, h.数値２, h.数値３ ,h.数値４ ,h.数値５, h.メモ, h.更新者, h.更新日 "
            sql = sql & N & ",   u.氏名 更新者名 "
            sql = sql & N & " FROM m90_hanyo h "
            sql = sql & N & " LEFT JOIN m02_user u on u.会社コード = h.会社コード AND u.ユーザＩＤ = h.更新者 "
            sql = sql & N & " Where h.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and h.固定キー = '" & koteiKey & "' and h.可変キー = '" & kahenKey & "'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

            If _ShoriMode <> CommonConst.MODE_ADDNEWCOPY Then
                Me.txtKahenKey.Text = kahenKey
            End If

            Me.txtHyoujiJun.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("表示順"))
            Me.txtMoji1.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("文字１"))
            Me.txtMoji2.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("文字２"))
            Me.txtMoji3.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("文字３"))
            Me.txtMoji4.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("文字４"))
            Me.txtMoji5.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("文字５"))
            Me.txtSuuti1.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("数値１"))
            Me.txtSuuti2.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("数値２"))
            Me.txtSuuti3.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("数値３"))
            Me.txtSuuti4.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("数値４"))
            Me.txtSuuti5.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("数値５"))
            Me.txtMemo.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("メモ"))
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

        Me.txtKoteiKey.Text = psKoteiKey
        Me.txtKahenKey.Text = String.Empty

        Me.txtHyoujiJun.Text = String.Empty
        Me.txtMoji1.Text = String.Empty
        Me.txtMoji2.Text = String.Empty
        Me.txtMoji3.Text = String.Empty
        Me.txtMoji4.Text = String.Empty
        Me.txtMoji5.Text = String.Empty
        Me.txtSuuti1.Text = String.Empty
        Me.txtSuuti2.Text = String.Empty
        Me.txtSuuti3.Text = String.Empty
        Me.txtSuuti4.Text = String.Empty
        Me.txtSuuti5.Text = String.Empty
        Me.txtMemo.Text = String.Empty

        Me.lblKousinsya.Text = MIDASHI_KOUSINSYA
        Me.lblKousinbi.Text = MIDASHI_KOUSINBI

    End Sub

    '背景色セット
    Private Sub setBackColor()

        Select Case _ShoriMode
            Case CommonConst.MODE_EditStatus     '変更
                Me.txtKahenKey.BackColor = _readOnlyTextBackColor
            Case CommonConst.MODE_InquiryStatus, CommonConst.MODE_DELETE    '参照,削除
                Me.txtKahenKey.BackColor = _readOnlyTextBackColor
                Me.txtHyoujiJun.BackColor = _readOnlyTextBackColor
                Me.txtMoji1.BackColor = _readOnlyTextBackColor
                Me.txtMoji2.BackColor = _readOnlyTextBackColor
                Me.txtMoji3.BackColor = _readOnlyTextBackColor
                Me.txtMoji4.BackColor = _readOnlyTextBackColor
                Me.txtMoji5.BackColor = _readOnlyTextBackColor
                Me.txtSuuti1.BackColor = _readOnlyTextBackColor
                Me.txtSuuti2.BackColor = _readOnlyTextBackColor
                Me.txtSuuti3.BackColor = _readOnlyTextBackColor
                Me.txtSuuti4.BackColor = _readOnlyTextBackColor
                Me.txtSuuti5.BackColor = _readOnlyTextBackColor
                Me.txtMemo.BackColor = _readOnlyTextBackColor
        End Select

    End Sub

    '画面項目のプロパティセット
    Private Sub setProperty()

        Select Case _ShoriMode
            Case CommonConst.MODE_InquiryStatus, CommonConst.MODE_DELETE     '参照,削除
                Me.txtKahenKey.ReadOnly = True
                Me.txtHyoujiJun.ReadOnly = True
                Me.txtMoji1.ReadOnly = True
                Me.txtMoji2.ReadOnly = True
                Me.txtMoji3.ReadOnly = True
                Me.txtMoji4.ReadOnly = True
                Me.txtMoji5.ReadOnly = True
                Me.txtSuuti1.ReadOnly = True
                Me.txtSuuti2.ReadOnly = True
                Me.txtSuuti3.ReadOnly = True
                Me.txtSuuti4.ReadOnly = True
                Me.txtSuuti5.ReadOnly = True
                Me.txtMemo.ReadOnly = True
                Me.txtKahenKey.TabStop = False
                Me.txtHyoujiJun.TabStop = False
                Me.txtMoji1.TabStop = False
                Me.txtMoji2.TabStop = False
                Me.txtMoji3.TabStop = False
                Me.txtMoji4.TabStop = False
                Me.txtMoji5.TabStop = False
                Me.txtSuuti1.TabStop = False
                Me.txtSuuti2.TabStop = False
                Me.txtSuuti3.TabStop = False
                Me.txtSuuti4.TabStop = False
                Me.txtSuuti5.TabStop = False
                Me.txtMemo.TabStop = False
            Case CommonConst.MODE_ADDNEW, CommonConst.MODE_ADDNEWCOPY     '新規追加,複写新規
                Me.txtKahenKey.ReadOnly = False
                Me.txtHyoujiJun.ReadOnly = False
                Me.txtMoji1.ReadOnly = False
                Me.txtMoji2.ReadOnly = False
                Me.txtMoji3.ReadOnly = False
                Me.txtMoji4.ReadOnly = False
                Me.txtMoji5.ReadOnly = False
                Me.txtSuuti1.ReadOnly = False
                Me.txtSuuti2.ReadOnly = False
                Me.txtSuuti3.ReadOnly = False
                Me.txtSuuti4.ReadOnly = False
                Me.txtSuuti5.ReadOnly = False
                Me.txtMemo.ReadOnly = False
            Case CommonConst.MODE_EditStatus     '変更
                Me.txtKahenKey.ReadOnly = True
                Me.txtHyoujiJun.ReadOnly = False
                Me.txtMoji1.ReadOnly = False
                Me.txtMoji2.ReadOnly = False
                Me.txtMoji3.ReadOnly = False
                Me.txtMoji4.ReadOnly = False
                Me.txtMoji5.ReadOnly = False
                Me.txtSuuti1.ReadOnly = False
                Me.txtSuuti2.ReadOnly = False
                Me.txtSuuti3.ReadOnly = False
                Me.txtSuuti4.ReadOnly = False
                Me.txtSuuti5.ReadOnly = False
                Me.txtMemo.ReadOnly = False
                Me.txtKahenKey.TabStop = False
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

    Private Sub ctl_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtKahenKey.KeyPress,
                                                                                                    txtHyoujiJun.KeyPress,
                                                                                                    txtMoji1.KeyPress,
                                                                                                    txtMoji2.KeyPress,
                                                                                                    txtMoji3.KeyPress,
                                                                                                    txtMoji4.KeyPress,
                                                                                                    txtMoji5.KeyPress,
                                                                                                    txtSuuti1.KeyPress,
                                                                                                    txtSuuti2.KeyPress,
                                                                                                    txtSuuti3.KeyPress,
                                                                                                    txtSuuti4.KeyPress,
                                                                                                    txtSuuti5.KeyPress,
                                                                                                    txtMemo.KeyPress

        UtilMDL.UtilClass.moveNextFocus(Me, e)
    End Sub

    '-------------------------------------------------------------------------------
    '　フォーカス取得イベント
    '-------------------------------------------------------------------------------
    Private Sub ctl_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtKahenKey.GotFocus,
                                                                                          txtHyoujiJun.GotFocus,
                                                                                          txtMoji1.GotFocus,
                                                                                          txtMoji2.GotFocus,
                                                                                          txtMoji3.GotFocus,
                                                                                          txtMoji4.GotFocus,
                                                                                          txtMoji5.GotFocus,
                                                                                          txtSuuti1.GotFocus,
                                                                                          txtSuuti2.GotFocus,
                                                                                          txtSuuti3.GotFocus,
                                                                                          txtSuuti4.GotFocus,
                                                                                          txtSuuti5.GotFocus,
                                                                                          txtMemo.GotFocus

        'フォーカス取得時、入力パラメタのコントロールを全選択状態とする
        Call UtilClass.selAll(sender)

    End Sub

#End Region

End Class