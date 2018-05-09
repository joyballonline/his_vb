'===============================================================================
'　 （システム名）      カネキ吉田商店様向け　原価管理システム
'
'   （機能名）          商品マスタ保守
'   （クラス名）        frmM30F20_ShohinHosyu
'   （処理機能名）      
'   （本MDL使用前提）   
'   （備考）         
'
'===============================================================================
' 履歴  名前               日付       　　　 　内容
'-------------------------------------------------------------------------------
'  (1)  桜井               2018/03/13      　　新規
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.DB
Imports UtilMDL.MSG             'UtilMsgHandler用
Imports UtilMDL.DataGridView

Public Class frmM30F20_ShohinHosyu
#Region "宣言"

    '-------------------------------------------------------------------------------
    ' 定数宣言
    '-------------------------------------------------------------------------------
    ' PG制御文字
    Private Const N As String = ControlChars.NewLine                    ' 改行文字
    Private Const RS As String = "RecSet"                               ' レコードセットテーブル

    '汎用マスメン
    Private Const lM3020_DB_EXIST As Integer = 1                        'あり：登録済
    Private Const lM3020_DB_NONE As Integer = 0                         'なし：未登録

    'DataGridView-------------------------------------------------------------------
    'グリッド列№
    'dgvIchiran
    Private Const COLNO_NO = 2                                      '01:No.
    Private Const COLNO_SHOSAI = 3                                  '02:詳細情報
    Private Const COLNO_DISPNO = 4                                  '03:表示順
    Private Const COLNO_MEMO = 5                                    '04:メモ

    'グリッド列名
    'dgvIchiran
    Private Const CCOL_NO As String = "cnNo"                        '01:No.
    Private Const CCOL_SHOSAI As String = "cnShosai"                '02:詳細情報
    Private Const CCOL_DISPNO As String = "cnDispNo"                '03:表示順
    Private Const CCOL_MEMO As String = "cnMemo"                    '04:メモ

    'グリッドデータ名
    'dgvIchiran
    Private Const DTCOL_NO As String = "dtNo"                       '01:No.
    Private Const DTCOL_SHOSAI As String = "dtShosai"               '02:詳細情報
    Private Const DTCOL_DISPNO As String = "dtDispNo"               '03:表示順
    Private Const DTCOL_MEMO As String = "dtMemo"                   '04:メモ

    '更新者・更新日見出しラベル
    Private Const MIDASHI_KOUSINSYA As String = "更新者："
    Private Const MIDASHI_KOUSINBI As String = "更新日："

    '-------------------------------------------------------------------------------
    'メンバー変数宣言
    '-------------------------------------------------------------------------------
    Private _db As UtilDBIf
    Private _msgHd As UtilMsgHandler
    Private _parentForm As frmM30F10_ShohinList
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
    Private pCS3020_DBFlg As Long

    '商品コード格納用変数
    Private psShohinCode As String

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
                   ByVal prmParentForm As frmM30F10_ShohinList,
                   ByVal prmMode As Integer,
                   ByRef prmSelectID As String,
                   ByRef prmShohinCode As String)

        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _parentForm = prmParentForm
        _ShoriMode = prmMode
        psShohinCode = prmShohinCode
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
                cmdAddRow.Enabled = False
                cmdDelRow.Enabled = False
                cmdRowCopy.Enabled = False
                cmdKakutei.Enabled = False
            Case CommonConst.MODE_DELETE                                                                '削除
                cmdAddRow.Enabled = False
                cmdDelRow.Enabled = False
                cmdRowCopy.Enabled = False
                cmdKakutei.Enabled = True
            Case CommonConst.MODE_ADDNEW, CommonConst.MODE_ADDNEWCOPY, CommonConst.MODE_EditStatus      '新規,複写新規,変更
                cmdAddRow.Enabled = True
                cmdDelRow.Enabled = True
                cmdRowCopy.Enabled = True
                cmdKakutei.Enabled = True
        End Select
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

        '商品詳細データ取得
        getShosaiData()

    End Sub
#End Region

#Region "イベント"

#Region "フォームロード"
    '-------------------------------------------------------------------------------
    '   画面ロード処理
    '   （処理概要） 画面が起動したときの処理を行う。
    '-------------------------------------------------------------------------------
    Private Sub frmM30F20_ShohinHosyu_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim BkCur As Cursor = Cursor.Current                                ' 現在のカーソルを取っておく
        Try
            Cursor.Current = Cursors.WaitCursor                             ' 砂時計カーソルに入れ替える

            _open = True                                                    '画面起動済フラグ

            '操作履歴ログ作成
            Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, CommonConst.MENU_M3002, CommonConst.PROGRAM_UPDATE, CommonConst.STATUS_NORMAL,
                                                Me.txtShohinCode.Text, DBNull.Value, lblShoriMode.Text, DBNull.Value, DBNull.Value,
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
                        sql = sql & N & " FROM m20_goods c "
                        sql = sql & N & " Where c.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and c.商品コード = '" & Me.txtShohinCode.Text & "'"
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
                    If M30SHOHIN_Add_Ctl() = False Then
                        Exit Sub
                    End If
                Case CommonConst.MODE_ADDNEWCOPY    '複写新規
                    If M30SHOHIN_Add_Ctl() = False Then
                        Exit Sub
                    End If
                Case CommonConst.MODE_EditStatus    '変更
                    If M30SHOHIN_Upd_Ctl() = False Then
                        Exit Sub
                    End If
                Case CommonConst.MODE_DELETE        '削除
                    If M30SHOHIN_Del_Ctl() = False Then
                        Exit Sub
                    End If
            End Select

            '操作履歴ログ作成 
            Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, CommonConst.MENU_M3002, CommonConst.PROGRAM_REPORT, CommonConst.STATUS_NORMAL,
                                                Me.txtShohinCode.Text, DBNull.Value, lblShoriMode.Text, DBNull.Value, DBNull.Value,
                                                DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _userId)

            '更新フラグ：登録済
            pCS3020_DBFlg = lM3020_DB_EXIST

            '商品コードを親フォームに返却（新規・複写新規は保守画面の入力値）
            '選択行の位置づけに使用
            _parentForm.shohinCode = Me.txtShohinCode.Text
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
    Private Function M30SHOHIN_Add_Ctl() As Boolean

        M30SHOHIN_Add_Ctl = False

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
            Call Insert_M30SHOHIN()

            '完了メッセージ表示
            Call _msgHd.dspMSG("completeInsert")                                      '登録が完了しました。

            'トランザクション終了
            _db.commitTran()

            M30SHOHIN_Add_Ctl = True

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
    Private Function M30SHOHIN_Upd_Ctl() As Boolean

        M30SHOHIN_Upd_Ctl = False

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
            Call Delete_M30SHOHIN()

            '追加処理
            Call Insert_M30SHOHIN()

            '完了メッセージ表示
            Call _msgHd.dspMSG("completeUpdate")                                      '登録が完了しました。

            'トランザクション終了
            _db.commitTran()

            M30SHOHIN_Upd_Ctl = True

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
    Private Function M30SHOHIN_Del_Ctl() As Boolean

        M30SHOHIN_Del_Ctl = False

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
            Call Delete_M30SHOHIN()

            '完了メッセージ表示
            Call _msgHd.dspMSG("completeDelete")                                      '削除が完了しました。

            'トランザクション終了
            _db.commitTran()

            M30SHOHIN_Del_Ctl = True

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
                Throw New UsrDefException("重複データが存在します。", _msgHd.getMSG("RegDupKeyData"), txtShohinCode)
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

        If "".Equals(txtShohinCode.Text) Then '商品コード
            Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【商品コード】"), txtShohinCode)
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
            sql = sql & N & " m20_goods"
            sql = sql & N & " WHERE"
            sql = sql & N & "       会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            sql = sql & N & "   AND 商品コード = '" & Me.txtShohinCode.Text & "'"

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
    Private Sub Insert_M30SHOHIN()

        Try
            '商品マスタ追加
            Dim sql As String = ""
            sql = "INSERT INTO"
            sql = sql & N & " m20_goods "
            sql = sql & N & "("
            sql = sql & N & "  会社コード"            '会社コード
            sql = sql & N & ", 商品コード"            '商品コード
            sql = sql & N & ", 商品名"                '商品名
            sql = sql & N & ", 商品名略称"            '商品名略称
            sql = sql & N & ", 商品名カナ"            '商品名カナ
            sql = sql & N & ", 販売仕入区分"          '販売仕入区分
            sql = sql & N & ", 大分類"                '大分類
            sql = sql & N & ", 課税区分"              '課税区分
            sql = sql & N & ", 冷凍区分"              '冷凍区分
            sql = sql & N & ", 入数"                  '入数
            sql = sql & N & ", 単位"                  '単位
            sql = sql & N & ", 産地"                  '産地
            sql = sql & N & ", 表示順"                '表示順
            sql = sql & N & ", メモ"                  'メモ
            sql = sql & N & ", 更新者"                '更新者
            sql = sql & N & ", 更新日"                '更新日
            sql = sql & N & ")"
            sql = sql & N & " VALUES"
            sql = sql & N & "("
            sql = sql & N & "  " & nullToStr(frmC01F10_Login.loginValue.BumonCD)
            sql = sql & N & ", " & nullToStr(Me.txtShohinCode.Text)
            sql = sql & N & ", " & nullToStr(Me.txtShohinName.Text)
            sql = sql & N & ", " & nullToStr(Me.txtShohinRyakuName.Text)
            sql = sql & N & ", " & nullToStr(Me.txtShohinNameKana.Text)
            sql = sql & N & ", " & nullToStr(Me.txtHanbaiSiireKbn.Text)
            sql = sql & N & ", " & nullToStr(Me.txtDaibunrui.Text)
            sql = sql & N & ", " & nullToStr(Me.txtKazeiKbn.Text)
            sql = sql & N & ", " & nullToStr(Me.txtReitoKbn.Text)
            sql = sql & N & ", " & nullToNum(Me.txtIrisu.Value)
            sql = sql & N & ", " & nullToStr(Me.txtTaniName.Text)
            sql = sql & N & ", " & nullToStr(Me.txtSanti.Text)
            sql = sql & N & ", " & nullToNum(Me.txtDispNo.Text)
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

            '商品詳細
            Dim intCnt As Integer = 0
            For index As Integer = 0 To dgvIchiran.RowCount - 1

                '詳細情報・表示順・メモがすべて未入力の行は追加しない
                If Not ((nullToStr(dgvIchiran.Rows(index).Cells(COLNO_SHOSAI).Value) = "NULL" Or nullToStr(dgvIchiran.Rows(index).Cells(COLNO_SHOSAI).Value) = "''") _
                        And (nullToNum(dgvIchiran.Rows(index).Cells(COLNO_DISPNO).Value) = "NULL" Or nullToNum(dgvIchiran.Rows(index).Cells(COLNO_DISPNO).Value) = "''") _
                        And (nullToStr(dgvIchiran.Rows(index).Cells(COLNO_MEMO).Value) = "NULL" Or nullToStr(dgvIchiran.Rows(index).Cells(COLNO_MEMO).Value) = "''")) Then

                    intCnt += 1
                    sql = ""
                    sql = sql & "INSERT INTO M21_GOODSDTL ( "
                    sql = sql & N & "    会社コード "
                    sql = sql & N & "  , 商品コード "
                    sql = sql & N & "  , 連番 "
                    sql = sql & N & "  , 詳細情報 "
                    sql = sql & N & "  , 表示順 "
                    sql = sql & N & "  , メモ "

                    sql = sql & N & "  , 更新者 "
                    sql = sql & N & "  , 更新日 "
                    sql = sql & N & ") VALUES ( "
                    sql = sql & N & "    '" & _db.rmSQ(frmC01F10_Login.loginValue.BumonCD) & "' "                           '会社コード
                    sql = sql & N & "  , '" & Me.txtShohinCode.Text & "' "                                                  '商品コード
                    sql = sql & N & "  , " & intCnt                                                                         '連番(有効行のみ採番)
                    sql = sql & N & "  , '" & dgvIchiran.Rows(index).Cells(COLNO_SHOSAI).Value & "' "                       '詳細情報
                    sql = sql & N & "  , " & nullToNum(dgvIchiran.Rows(index).Cells(COLNO_DISPNO).Value)                    '表示順
                    sql = sql & N & "  , '" & dgvIchiran.Rows(index).Cells(COLNO_MEMO).Value & "' "                         'メモ

                    sql = sql & N & "  , '" & frmC01F10_Login.loginValue.TantoCD & "' "         '更新者
                    sql = sql & N & "  , current_timestamp "                                    '更新日
                    sql = sql & N & ") "

                    Try
                        'sql発行
                        _db.executeDB(sql)
                    Catch ex As Exception
                        _dbErr = True
                        Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
                    End Try
                End If

            Next

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
    Private Sub Delete_M30SHOHIN()

        Try
            '商品マスタ更新
            Dim sql As String = ""
            sql = "DELETE FROM m20_goods"
            sql = sql & N & " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"                   '会社コード
            sql = sql & N & "   AND 商品コード = '" & Me.txtShohinCode.Text & "' "                               '商品コード

            Try
                'sql発行
                _db.executeDB(sql)
            Catch ex As Exception
                _dbErr = True
                Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
            End Try

            '商品詳細マスタ更新
            sql = ""
            sql = "DELETE FROM m21_goodsdtl"
            sql = sql & N & " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"                   '会社コード
            sql = sql & N & "   AND 商品コード = '" & Me.txtShohinCode.Text & "' "                               '商品コード

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
        Dim shohinCode As String = psShohinCode

        Dim sql As String = ""

        Try
            sql = "SELECT "
            sql = sql & N & "  g.商品コード, g.商品名, g.商品名略称, g.商品名カナ, g.販売仕入区分, g.大分類, g.課税区分, g.冷凍区分, g.単位, g.入数, g.産地, g.表示順, g.メモ, g.更新者, g.更新日 "
            sql = sql & N & " ,h1.文字２ 販売仕入区分名称 "
            sql = sql & N & " ,h2.文字２ 大分類名称 "
            sql = sql & N & " ,h3.文字２ 課税区分名称 "
            sql = sql & N & " ,h4.文字２ 冷凍区分名称 "
            sql = sql & N & " ,h5.可変キー 単位区分 "
            sql = sql & N & ", u.氏名 更新者名 "
            sql = sql & N & " FROM m20_goods g "
            sql = sql & N & " left join M90_HANYO h1 on h1.会社コード = g.会社コード and h1.固定キー = '" & CommonConst.HANYO_HANBAISIIRE_KBN & "' and h1.可変キー = g.販売仕入区分 "
            sql = sql & N & " left join M90_HANYO h2 on h2.会社コード = g.会社コード and h2.固定キー = '" & CommonConst.HANYO_SHOHIN_BUNRUI & "' and h2.可変キー = g.大分類 "
            sql = sql & N & " left join M90_HANYO h3 on h3.会社コード = g.会社コード and h3.固定キー = '" & CommonConst.HANYO_KAZEI_KBN & "' and h3.可変キー = g.課税区分 "
            sql = sql & N & " left join M90_HANYO h4 on h4.会社コード = g.会社コード and h4.固定キー = '" & CommonConst.HANYO_REITOU_KBN & "' and h4.可変キー = g.冷凍区分 "
            sql = sql & N & " left join M90_HANYO h5 on h5.会社コード = g.会社コード and h5.固定キー = '" & CommonConst.HANYO_TANI & "' and h5.文字１ = g.単位 "
            sql = sql & N & " LEFT JOIN m02_user u on u.会社コード = g.会社コード AND u.ユーザＩＤ = g.更新者 "
            sql = sql & N & " Where g.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and g.商品コード = '" & shohinCode & "'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

            If _ShoriMode <> CommonConst.MODE_ADDNEWCOPY Then
                Me.txtShohinCode.Text = shohinCode
            End If

            Me.txtShohinName.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("商品名"))
            Me.txtShohinRyakuName.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("商品名略称"))
            Me.txtShohinNameKana.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("商品名カナ"))
            Me.txtHanbaiSiireKbn.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("販売仕入区分"))
            Me.txtHanbaiSiireKbnName.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("販売仕入区分名称"))
            Me.txtDaibunrui.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("大分類"))
            Me.txtDaibunruiName.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("大分類名称"))
            Me.txtKazeiKbn.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("課税区分"))
            Me.txtKazeiKbnName.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("課税区分名称"))
            Me.txtReitoKbn.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("冷凍区分"))
            Me.txtReitoKbnName.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("冷凍区分名称"))
            Me.txtIrisu.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("入数"))
            Me.txtTaniName.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("単位"))
            Me.txtSanti.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("産地"))
            Me.txtDispNo.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("表示順"))
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

    'マスタデータ取得
    Private Sub getShosaiData()

        'データ初期表示
        'キー項目を取得
        Dim shohinCode = psShohinCode

        Dim sql As String = ""

        Try
            sql = "SELECT "
            sql = sql & "  gd.連番              " & DTCOL_NO            ' 0：連番
            sql = sql & ", gd.詳細情報          " & DTCOL_SHOSAI        ' 1：詳細情報
            sql = sql & ", gd.表示順            " & DTCOL_DISPNO        ' 2：表示順
            sql = sql & ", gd.メモ              " & DTCOL_MEMO          ' 3：メモ
            sql = sql & " FROM m21_goodsdtl gd "
            sql = sql & " Where gd.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and gd.商品コード = '" & shohinCode & "'"
            sql = sql & " ORDER BY gd.連番 "

            Dim reccnt As Integer = 0

            '新規追加の場合は空白行を1行追加する
            If _ShoriMode = CommonConst.MODE_ADDNEW Then
                Dim dt As DataTable = New DataTable(RS)
                dt.Columns().Add(DTCOL_NO, Type.GetType("System.String"))
                dt.Columns().Add(DTCOL_SHOSAI, Type.GetType("System.String"))
                dt.Columns().Add(DTCOL_DISPNO, Type.GetType("System.Decimal"))
                dt.Columns().Add(DTCOL_MEMO, Type.GetType("System.String"))

                Dim dr As DataRow = dt.NewRow()
                dr.Item(DTCOL_NO) = 1
                dr.Item(DTCOL_SHOSAI) = ""
                dr.Item(DTCOL_DISPNO) = DBNull.Value
                dr.Item(DTCOL_MEMO) = ""

                dt.Rows.Add(dr)
                Dim ds As DataSet = New DataSet()
                ds.Tables.Add(dt)
                dgvIchiran.DataSource = ds
            Else
                Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)
                dgvIchiran.DataSource = ds
            End If

            dgvIchiran.DataMember = RS

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

        Me.txtShohinCode.Text = String.Empty

        Me.txtShohinName.Text = String.Empty
        Me.txtShohinRyakuName.Text = String.Empty
        Me.txtShohinNameKana.Text = String.Empty
        Me.txtHanbaiSiireKbn.Text = String.Empty
        Me.txtHanbaiSiireKbnName.Text = String.Empty
        Me.txtDaibunrui.Text = String.Empty
        Me.txtDaibunruiName.Text = String.Empty
        Me.txtKazeiKbn.Text = String.Empty
        Me.txtKazeiKbnName.Text = String.Empty
        Me.txtReitoKbn.Text = String.Empty
        Me.txtReitoKbnName.Text = String.Empty
        Me.txtIrisu.Text = String.Empty
        Me.txtTaniName.Text = String.Empty
        Me.txtSanti.Text = String.Empty
        Me.txtDispNo.Text = String.Empty
        Me.txtMemo.Text = String.Empty

        Me.lblKousinsya.Text = MIDASHI_KOUSINSYA
        Me.lblKousinbi.Text = MIDASHI_KOUSINBI

    End Sub

    '背景色セット
    Private Sub setBackColor()

        Select Case _ShoriMode
            Case CommonConst.MODE_EditStatus     '変更
                Me.txtShohinCode.BackColor = _readOnlyTextBackColor
            Case CommonConst.MODE_InquiryStatus, CommonConst.MODE_DELETE     '参照,削除
                Me.txtShohinCode.BackColor = _readOnlyTextBackColor
                Me.txtShohinName.BackColor = _readOnlyTextBackColor
                Me.txtShohinRyakuName.BackColor = _readOnlyTextBackColor
                Me.txtShohinNameKana.BackColor = _readOnlyTextBackColor
                Me.txtHanbaiSiireKbn.BackColor = _readOnlyTextBackColor
                Me.txtDaibunrui.BackColor = _readOnlyTextBackColor
                Me.txtKazeiKbn.BackColor = _readOnlyTextBackColor
                Me.txtReitoKbn.BackColor = _readOnlyTextBackColor
                Me.txtIrisu.BackColor = _readOnlyTextBackColor
                Me.txtSanti.BackColor = _readOnlyTextBackColor
                Me.txtDispNo.BackColor = _readOnlyTextBackColor
                Me.txtMemo.BackColor = _readOnlyTextBackColor
                Me.dgvIchiran.RowsDefaultCellStyle.BackColor = _readOnlyTextBackColor
        End Select

    End Sub

    '画面項目のプロパティセット
    Private Sub setProperty()

        Select Case _ShoriMode
            Case CommonConst.MODE_InquiryStatus, CommonConst.MODE_DELETE     '参照,削除
                Me.txtShohinCode.ReadOnly = True
                Me.txtShohinName.ReadOnly = True
                Me.txtShohinRyakuName.ReadOnly = True
                Me.txtShohinNameKana.ReadOnly = True
                Me.txtHanbaiSiireKbn.ReadOnly = True
                Me.txtDaibunrui.ReadOnly = True
                Me.txtKazeiKbn.ReadOnly = True
                Me.txtReitoKbn.ReadOnly = True
                Me.txtIrisu.ReadOnly = True
                Me.txtSanti.ReadOnly = True
                Me.txtDispNo.ReadOnly = True
                Me.txtMemo.ReadOnly = True
                Me.txtShohinCode.TabStop = False
                Me.txtShohinName.TabStop = False
                Me.txtShohinRyakuName.TabStop = False
                Me.txtShohinNameKana.TabStop = False
                Me.txtHanbaiSiireKbn.TabStop = False
                Me.txtDaibunrui.TabStop = False
                Me.txtKazeiKbn.TabStop = False
                Me.txtReitoKbn.TabStop = False
                Me.txtIrisu.TabStop = False
                Me.txtSanti.TabStop = False
                Me.txtDispNo.TabStop = False
                Me.txtMemo.TabStop = False
                Me.dgvIchiran.ReadOnly = True
            Case CommonConst.MODE_ADDNEW, CommonConst.MODE_ADDNEWCOPY     '新規追加,複写新規
                Me.txtShohinCode.ReadOnly = False
                Me.txtShohinName.ReadOnly = False
                Me.txtShohinRyakuName.ReadOnly = False
                Me.txtShohinNameKana.ReadOnly = False
                Me.txtHanbaiSiireKbn.ReadOnly = False
                Me.txtDaibunrui.ReadOnly = False
                Me.txtKazeiKbn.ReadOnly = False
                Me.txtReitoKbn.ReadOnly = False
                Me.txtIrisu.ReadOnly = False
                Me.txtSanti.ReadOnly = False
                Me.txtDispNo.ReadOnly = False
                Me.txtMemo.ReadOnly = False
                Me.dgvIchiran.ReadOnly = False
            Case CommonConst.MODE_EditStatus     '変更
                Me.txtShohinCode.ReadOnly = True
                Me.txtShohinName.ReadOnly = False
                Me.txtShohinRyakuName.ReadOnly = False
                Me.txtShohinNameKana.ReadOnly = False
                Me.txtHanbaiSiireKbn.ReadOnly = False
                Me.txtDaibunrui.ReadOnly = False
                Me.txtKazeiKbn.ReadOnly = False
                Me.txtReitoKbn.ReadOnly = False
                Me.txtIrisu.ReadOnly = False
                Me.txtSanti.ReadOnly = False
                Me.txtDispNo.ReadOnly = False
                Me.txtMemo.ReadOnly = False
                Me.dgvIchiran.ReadOnly = False
                Me.txtShohinCode.TabStop = False
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

    Private Sub txtHanbaiSiireKbn_DoubleClick(sender As Object, e As EventArgs) Handles txtHanbaiSiireKbn.DoubleClick
        Dim openForm As frmC10F90_Hanyo = New frmC10F90_Hanyo(_msgHd, _db, CommonConst.HANYO_HANBAISIIRE_KBN)      '画面遷移
        openForm.ShowDialog()                      '画面表示
        If openForm.Selected Then
            '選択されました
            Me.txtHanbaiSiireKbn.Text = openForm.GetValCD
            Me.txtHanbaiSiireKbnName.Text = openForm.GetValNM
        End If
        openForm = Nothing

    End Sub

    Private Sub txtHanbaiSiireKbn_Leave(sender As Object, e As EventArgs) Handles txtHanbaiSiireKbn.Leave

        Try
            '販売仕入区分フォーカスアウト時処理
            hanbaiSiireKbnLeave()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    Private Sub ctl_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtShohinCode.KeyPress,
                                                                                                    txtShohinName.KeyPress,
                                                                                                    txtShohinRyakuName.KeyPress,
                                                                                                    txtShohinNameKana.KeyPress,
                                                                                                    txtHanbaiSiireKbn.KeyPress,
                                                                                                    txtDaibunrui.KeyPress,
                                                                                                    txtKazeiKbn.KeyPress,
                                                                                                    txtReitoKbn.KeyPress,
                                                                                                    txtIrisu.KeyPress,
                                                                                                    txtSanti.KeyPress,
                                                                                                    txtDispNo.KeyPress,
                                                                                                    txtMemo.KeyPress,
                                                                                                    txtTaniName.KeyPress

        UtilMDL.UtilClass.moveNextFocus(Me, e)
    End Sub

    '-------------------------------------------------------------------------------
    '　フォーカス取得イベント
    '-------------------------------------------------------------------------------
    Private Sub ctl_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtShohinCode.GotFocus,
                                                                                          txtShohinName.GotFocus,
                                                                                          txtShohinRyakuName.GotFocus,
                                                                                          txtShohinNameKana.GotFocus,
                                                                                          txtHanbaiSiireKbn.GotFocus,
                                                                                          txtDaibunrui.GotFocus,
                                                                                          txtKazeiKbn.GotFocus,
                                                                                          txtReitoKbn.GotFocus,
                                                                                          txtIrisu.GotFocus,
                                                                                          txtSanti.GotFocus,
                                                                                          txtDispNo.GotFocus,
                                                                                          txtMemo.GotFocus,
                                                                                          txtTaniName.GotFocus

        'フォーカス取得時、入力パラメタのコントロールを全選択状態とする
        Call UtilClass.selAll(sender)

    End Sub

    '販売仕入区分フォーカスアウト時処理
    Private Sub hanbaiSiireKbnLeave()

        '販売仕入区分に入力がある場合
        If (Not ("".Equals(Me.txtHanbaiSiireKbn.Text))) Then
            '販売仕入区分名称取得処理
            Me.txtHanbaiSiireKbnName.Text = getHanyoMstName(CommonConst.HANYO_HANBAISIIRE_KBN, Me.txtHanbaiSiireKbn.Text)
        Else
            '販売仕入区分名称クリア
            Me.txtHanbaiSiireKbnName.Clear()
        End If

    End Sub

    Private Sub cmdAddRow_Click(sender As Object, e As EventArgs) Handles cmdAddRow.Click

        If Me.dgvIchiran.RowCount = 0 Then
            Dim dt As DataTable = New DataTable(RS)
            dt.Columns().Add(DTCOL_NO, Type.GetType("System.String"))
            dt.Columns().Add(DTCOL_SHOSAI, Type.GetType("System.String"))
            dt.Columns().Add(DTCOL_DISPNO, Type.GetType("System.Decimal"))
            dt.Columns().Add(DTCOL_MEMO, Type.GetType("System.String"))

            Dim dr As DataRow = dt.NewRow()
            dr.Item(DTCOL_NO) = 1
            dr.Item(DTCOL_SHOSAI) = ""
            dr.Item(DTCOL_DISPNO) = DBNull.Value
            dr.Item(DTCOL_MEMO) = ""

            dt.Rows.Add(dr)
            Dim ds As DataSet = New DataSet()
            ds.Tables.Add(dt)

            dgvIchiran.DataSource = ds
            dgvIchiran.DataMember = RS
        Else
            Dim ds As DataSet = TryCast(Me.dgvIchiran.DataSource, DataSet)
            ds.Tables("RecSet").Rows.InsertAt(ds.Tables("RecSet").NewRow, Me.dgvIchiran.CurrentCell.RowIndex + 1)
        End If

        '行番号降り直し
        For i As Integer = 0 To dgvIchiran.RowCount - 1
            dgvIchiran.Rows(i).Cells(CCOL_NO).Value = i + 1
        Next i

    End Sub

    Private Sub cmdDelRow_Click(sender As Object, e As EventArgs) Handles cmdDelRow.Click

        'グリッドに１明細しかないときは削除しない
        If dgvIchiran.RowCount <= 1 Then
            Exit Sub
        End If

        Dim gh As UtilDataGridViewHandler = New UtilDataGridViewHandler(dgvIchiran)
        If gh.getMaxRow > 0 Then
            If TypeOf Me.dgvIchiran.DataSource Is DataSet Then
                Dim ds As DataSet = TryCast(Me.dgvIchiran.DataSource, DataSet)
                ds.Tables("RecSet").Rows.RemoveAt(Me.dgvIchiran.CurrentCell.RowIndex)
            ElseIf TypeOf Me.dgvIchiran.DataSource Is DataTable Then
                Dim dt As DataTable = TryCast(Me.dgvIchiran.DataSource, DataTable)
                dt.Rows.RemoveAt(Me.dgvIchiran.CurrentCell.RowIndex)
            End If
        End If

        '行番号降り直し
        For i As Integer = 0 To dgvIchiran.RowCount - 1
            dgvIchiran.Rows(i).Cells(COLNO_NO).Value = i + 1
        Next i

    End Sub

    Private Sub cmdRowCopy_Click(sender As Object, e As EventArgs) Handles cmdRowCopy.Click

        Dim ds As DataSet = TryCast(Me.dgvIchiran.DataSource, DataSet)
        ds.Tables("RecSet").Rows.InsertAt(ds.Tables("RecSet").NewRow, Me.dgvIchiran.CurrentCell.RowIndex + 1)
        ds.Tables("RecSet").Rows(Me.dgvIchiran.CurrentCell.RowIndex + 1).ItemArray = ds.Tables("RecSet").Rows(Me.dgvIchiran.CurrentCell.RowIndex).ItemArray

        '行番号降り直し
        For i As Integer = 0 To dgvIchiran.RowCount - 1
            dgvIchiran.Rows(i).Cells(COLNO_NO).Value = i + 1
        Next i

    End Sub

    Private Sub dgvIchiran_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgvIchiran.CellEnter

        '列ごとにIMEModeを制御する
        Select Case e.ColumnIndex
            Case COLNO_SHOSAI, COLNO_MEMO
                dgvIchiran.ImeMode = Windows.Forms.ImeMode.Hiragana
            Case COLNO_DISPNO
                dgvIchiran.ImeMode = Windows.Forms.ImeMode.Off
        End Select

    End Sub

    Private Sub txtDaibunrui_DoubleClick(sender As Object, e As EventArgs) Handles txtDaibunrui.DoubleClick
        Dim openForm As frmC10F90_Hanyo = New frmC10F90_Hanyo(_msgHd, _db, CommonConst.HANYO_SHOHIN_BUNRUI)      '画面遷移
        openForm.ShowDialog()                      '画面表示
        If openForm.Selected Then
            '選択されました
            Me.txtDaibunrui.Text = openForm.GetValCD
            Me.txtDaibunruiName.Text = openForm.GetValNM
        End If
        openForm = Nothing
    End Sub

    Private Sub txtDaibunrui_Leave(sender As Object, e As EventArgs) Handles txtDaibunrui.Leave

        Try
            '大分類フォーカスアウト時処理
            daibunruiLeave()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '大分類フォーカスアウト時処理
    Private Sub daibunruiLeave()

        '大分類に入力がある場合
        If (Not ("".Equals(Me.txtDaibunrui.Text))) Then
            '大分類名称取得処理
            Me.txtDaibunruiName.Text = getHanyoMstName(CommonConst.HANYO_SHOHIN_BUNRUI, Me.txtDaibunrui.Text)
        Else
            '大分類名称クリア
            Me.txtDaibunruiName.Clear()
        End If

    End Sub

    Private Sub txtKazeiKbn_Leave(sender As Object, e As EventArgs) Handles txtKazeiKbn.Leave

        Try
            '課税区分フォーカスアウト時処理
            kazeiKbnLeave()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    Private Sub txtKazeiKbn_DoubleClick(sender As Object, e As EventArgs) Handles txtKazeiKbn.DoubleClick
        Dim openForm As frmC10F90_Hanyo = New frmC10F90_Hanyo(_msgHd, _db, CommonConst.HANYO_KAZEI_KBN)      '画面遷移
        openForm.ShowDialog()                      '画面表示
        If openForm.Selected Then
            '選択されました
            Me.txtKazeiKbn.Text = openForm.GetValCD
            Me.txtKazeiKbnName.Text = openForm.GetValNM
        End If
        openForm = Nothing
    End Sub

    '課税区分フォーカスアウト時処理
    Private Sub kazeiKbnLeave()

        '課税区分に入力がある場合
        If (Not ("".Equals(Me.txtKazeiKbn.Text))) Then
            '課税区分名称取得処理
            Me.txtKazeiKbnName.Text = getHanyoMstName(CommonConst.HANYO_KAZEI_KBN, Me.txtKazeiKbn.Text)
        Else
            '課税区分名称クリア
            Me.txtKazeiKbnName.Clear()
        End If

    End Sub

    Private Sub txtReitoKbn_Leave(sender As Object, e As EventArgs) Handles txtReitoKbn.Leave

        Try
            '冷凍区分フォーカスアウト時処理
            reitoKbnLeave()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    Private Sub txtReitoKbn_DoubleClick(sender As Object, e As EventArgs) Handles txtReitoKbn.DoubleClick
        Dim openForm As frmC10F90_Hanyo = New frmC10F90_Hanyo(_msgHd, _db, CommonConst.HANYO_REITOU_KBN)      '画面遷移
        openForm.ShowDialog()                      '画面表示
        If openForm.Selected Then
            '選択されました
            Me.txtReitoKbn.Text = openForm.GetValCD
            Me.txtReitoKbnName.Text = openForm.GetValNM
        End If
        openForm = Nothing
    End Sub

    '冷凍区分フォーカスアウト時処理
    Private Sub reitoKbnLeave()

        '冷凍区分に入力がある場合
        If (Not ("".Equals(Me.txtReitoKbn.Text))) Then
            '冷凍区分名称取得処理
            Me.txtReitoKbnName.Text = getHanyoMstName(CommonConst.HANYO_REITOU_KBN, Me.txtReitoKbn.Text)
        Else
            '冷凍区分名称クリア
            Me.txtReitoKbnName.Clear()
        End If

    End Sub

    Private Sub txtTaniName_DoubleClick(sender As Object, e As EventArgs) Handles txtTaniName.DoubleClick
        Dim openForm As frmC10F90_Hanyo = New frmC10F90_Hanyo(_msgHd, _db, CommonConst.HANYO_TANI)      '画面遷移
        openForm.ShowDialog()                      '画面表示
        If openForm.Selected Then
            '選択されました
            Me.txtTaniName.Text = openForm.GetValNM
        End If
        openForm = Nothing
    End Sub

    '汎用マスタ名称取得処理
    Private Function getHanyoMstName(koteikey As String, kahenKey As String) As String

        getHanyoMstName = String.Empty

        '住所データを取得
        Dim sql As String = ""

        sql = sql & "SELECT "
        sql = sql & N & "    h.文字２ 名称 "
        sql = sql & N & " FROM M90_HANYO h "
        sql = sql & N & " Where h.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and h.固定キー = '" & koteikey & "'"
        sql = sql & N & " and h.可変キー = '" + kahenKey + "' "

        Dim reccnt As Integer = 0

        Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

        'データカウント
        Dim dataCount As Integer = ds.Tables(RS).Rows.Count

        If dataCount <> 0 Then

            '取得データ
            Dim dataRow As DataRow = ds.Tables(RS).Rows(0)

            getHanyoMstName = _db.rmNullStr(dataRow("名称"))
        End If

    End Function

#End Region

End Class