'===============================================================================
'　 （システム名）      カネキ吉田商店様向け　原価管理システム
'
'   （機能名）          販売単価マスタ保守
'   （クラス名）        frmM40F20_HanbaiTankaHosyu
'   （処理機能名）      
'   （本MDL使用前提）   
'   （備考）         
'
'===============================================================================
' 履歴  名前               日付       　　　 　内容
'-------------------------------------------------------------------------------
'  (1)  桜井               2018/03/23      　　新規
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.DB
Imports UtilMDL.MSG             'UtilMsgHandler用

Public Class frmM40F20_HanbaiTankaHosyu
#Region "宣言"

    '-------------------------------------------------------------------------------
    ' 定数宣言
    '-------------------------------------------------------------------------------
    ' PG制御文字
    Private Const N As String = ControlChars.NewLine                    ' 改行文字
    Private Const RS As String = "RecSet"                               ' レコードセットテーブル

    '汎用マスメン
    Private Const lM4020_DB_EXIST As Integer = 1                        'あり：登録済
    Private Const lM4020_DB_NONE As Integer = 0                         'なし：未登録

    '更新者・更新日見出しラベル
    Private Const MIDASHI_KOUSINSYA As String = "更新者："
    Private Const MIDASHI_KOUSINBI As String = "更新日："

    '特売区分
    Private Const TOKUBAIKBN_TOKUBAI As Integer = 1                                 '区分(1:特売)
    Private Const TOKUBAIKBN_IGAI As Integer = 0                                    '区分(0:特売以外)

    '取引先コード指定なし
    Private Const TORICODE_ALL As String = "ALL"                                    '指定なし:ALL

    '適用終了日最大値
    Private Const TEKIYOSHURYOBI_MAX As String = "9999/12/31"

    '-------------------------------------------------------------------------------
    'メンバー変数宣言
    '-------------------------------------------------------------------------------
    Private _db As UtilDBIf
    Private _msgHd As UtilMsgHandler
    Private _parentForm As frmM40F10_HanbaiTankaList
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
    Private pCS4020_DBFlg As Long

    '引数格納用変数
    Private psShohinCode As String
    Private psTokubaikbn As Integer
    Private psToriCode As String
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
                   ByVal prmParentForm As frmM40F10_HanbaiTankaList,
                   ByVal prmMode As Integer,
                   ByRef prmSelectID As String,
                   ByRef prmShohinCode As String,
                   ByRef prmTokubaiKbn As Integer,
                   ByRef prmToriCode As String,
                   ByRef prmTekiyoFrDT As String)

        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _parentForm = prmParentForm
        _ShoriMode = prmMode
        psShohinCode = prmShohinCode
        psTokubaikbn = prmTokubaiKbn
        psToriCode = prmToriCode
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
    Private Sub frmM40F20_HanbaiTankaHosyu_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim BkCur As Cursor = Cursor.Current                                ' 現在のカーソルを取っておく
        Try
            Cursor.Current = Cursors.WaitCursor                             ' 砂時計カーソルに入れ替える

            _open = True                                                    '画面起動済フラグ

            Dim tekiyoFrDT As Long = 0
            If IsDate(psTekiyoFrDT) Then
                tekiyoFrDT = Long.Parse(psTekiyoFrDT.Replace("/", ""))
            End If

            '操作履歴ログ作成
            Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, CommonConst.MENU_M4002, CommonConst.PROGRAM_UPDATE, CommonConst.STATUS_NORMAL,
                                                Me.txtShohinCode.Text, Me.txtToriCode.Text, lblShoriMode.Text, DBNull.Value, DBNull.Value,
                                                psTokubaikbn, tekiyoFrDT, DBNull.Value, DBNull.Value, DBNull.Value, _userId)

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
                        sql = sql & N & "    sp.更新日,sp.更新者 "
                        sql = sql & N & " FROM m25_slprice sp "
                        sql = sql & N & " Where sp.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and sp.商品コード = '" & Me.txtShohinCode.Text & "' and sp.特売区分 = " & If(Me.chkTokubaiKbn.Checked = True, 1, 0) & " and sp.取引先コード = '" & If(Me.txtToriCode.Text = String.Empty, TORICODE_ALL, Me.txtToriCode.Text) & "' and sp.適用開始日 = '" & Me.dtpTekiyoFrDt.Value & "'"
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

            '特売の場合
            If Me.chkTokubaiKbn.Checked = True Then

                Dim chkCNt As Integer = 0
                '新規の場合
                If _ShoriMode = CommonConst.MODE_ADDNEW Or _ShoriMode = CommonConst.MODE_ADDNEWCOPY Or (_ShoriMode = CommonConst.MODE_EditStatus And _TekiyoToDT <> Me.dtpTekiyoToDt.Text) Then
                    '既存のデータに適用開始日・適用終了日をまたぐデータが存在するかどうかのチェック
                    '画面の適用開始日が適用開始日～適用終了日の範囲に含まれるデータが存在するかどうか
                    Try
                        sql = ""
                        sql = "SELECT  "
                        sql = sql & N & "    count(*) as 件数 "
                        sql = sql & N & " FROM m25_slprice sp "
                        sql = sql & N & " Where sp.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and sp.商品コード = '" & Me.txtShohinCode.Text & "' and sp.特売区分 = " & If(Me.chkTokubaiKbn.Checked = True, 1, 0) & " and sp.取引先コード = '" & If(Me.txtToriCode.Text = String.Empty, TORICODE_ALL, Me.txtToriCode.Text) & "' and sp.適用開始日 <= '" & Me.dtpTekiyoToDt.Value & "' and sp.適用終了日 >= '" & Me.dtpTekiyoFrDt.Value & "'"
                        Dim reccnt As Integer = 0
                        Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)
                        If _db.rmNullInt(ds.Tables(RS).Rows(0)("件数")) > 0 Then
                            chkCNt += 1
                        End If
                    Catch
                    End Try
                    'ElseIf _ShoriMode = CommonConst.MODE_EditStatus Then
                    '    If _TekiyoToDT <> Me.dtpTekiyoToDt.Text Then
                    '        chkCNt += 1
                    '    End If
                End If
                '画面の適用開始日が適用開始日～適用終了日の範囲に、画面の適用終了日が適用開始日～適用終了日の範囲に含まれるデータがどちらかでも存在した場合
                '適用開始日・適用終了日をまたぐデータを登録しようとしているのでエラーとする
                If chkCNt > 0 Then
                    Throw New UsrDefException("登録済みのデータが存在します。", _msgHd.getMSG("existRegistData", "【適用開始日】【適用終了日】"), dtpTekiyoToDt)
                    Exit Sub
                End If
            End If

            '処理モードにより更新方法を切り替える
            Select Case _ShoriMode
                Case CommonConst.MODE_ADDNEW        '新規
                    If M40HANBAITANKA_Add_Ctl() = False Then
                        Exit Sub
                    End If
                Case CommonConst.MODE_ADDNEWCOPY    '複写新規
                    If M40HANBAITANKA_Add_Ctl() = False Then
                        Exit Sub
                    End If
                Case CommonConst.MODE_EditStatus    '変更
                    If M40HANBAITANKA_Upd_Ctl() = False Then
                        Exit Sub
                    End If
                Case CommonConst.MODE_DELETE        '削除
                    If M40HANBAITANKA_Del_Ctl() = False Then
                        Exit Sub
                    End If
            End Select

            Dim tokubaiKbn As Integer = 0
            If Me.chkTokubaiKbn.Checked = True Then
                tokubaiKbn = TOKUBAIKBN_TOKUBAI
            Else
                tokubaiKbn = TOKUBAIKBN_IGAI
            End If
            Dim toriCode As String = ""
            If Me.txtToriCode.Text = String.Empty Then
                toriCode = TORICODE_ALL
            Else
                toriCode = Me.txtToriCode.Text
            End If
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
            Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, CommonConst.MENU_M4002, CommonConst.PROGRAM_REPORT, CommonConst.STATUS_NORMAL,
                                                Me.txtShohinCode.Text, toriCode, lblShoriMode.Text, DBNull.Value, DBNull.Value,
                                                tokubaiKbn, Long.Parse(tekiyoFrDT), DBNull.Value, DBNull.Value, DBNull.Value, _userId)

            '更新フラグ：登録済
            pCS4020_DBFlg = lM4020_DB_EXIST

            'キー項目を親フォームに返却（新規・複写新規は保守画面の入力値）
            '選択行の位置づけに使用
            _parentForm.shohinCode = Me.txtShohinCode.Text
            If Me.chkTokubaiKbn.Checked = True Then
                _parentForm.tokubaiKbn = TOKUBAIKBN_TOKUBAI
            Else
                _parentForm.tokubaiKbn = TOKUBAIKBN_IGAI
            End If
            If Me.txtToriCode.Text = String.Empty Then
                _parentForm.toriCode = TORICODE_ALL
            Else
                _parentForm.toriCode = Me.txtToriCode.Text
            End If
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
    Private Function M40HANBAITANKA_Add_Ctl() As Boolean

        M40HANBAITANKA_Add_Ctl = False

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
            Call Insert_M40HANBAITANKA()

            '完了メッセージ表示
            Call _msgHd.dspMSG("completeInsert")                                      '登録が完了しました。

            'トランザクション終了
            _db.commitTran()

            M40HANBAITANKA_Add_Ctl = True

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
    Private Function M40HANBAITANKA_Upd_Ctl() As Boolean

        M40HANBAITANKA_Upd_Ctl = False

        If Me.chkTokubaiKbn.Checked = True Then
            If dtpTekiyoToDt.Value Is Nothing Then '適用終了日
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【適用終了日】"), dtpTekiyoToDt)
            End If
            If dtpTekiyoToDt.Value < dtpTekiyoFrDt.Value Then
                Throw New UsrDefException("大小関係が正しくありません。", _msgHd.getMSG("ErrDaiSyoChk", "【適用開始日】【適用終了日】"), dtpTekiyoFrDt)
            End If
        End If

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
            Call Update_M40HANBAITANKA()

            '完了メッセージ表示
            Call _msgHd.dspMSG("completeUpdate")                                      '登録が完了しました。

            'トランザクション終了
            _db.commitTran()

            M40HANBAITANKA_Upd_Ctl = True

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
    Private Function M40HANBAITANKA_Del_Ctl() As Boolean

        M40HANBAITANKA_Del_Ctl = False

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
            Call Delete_M40HANBAITANKA()

            '完了メッセージ表示
            Call _msgHd.dspMSG("completeDelete")                                      '削除が完了しました。

            'トランザクション終了
            _db.commitTran()

            M40HANBAITANKA_Del_Ctl = True

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

        If "".Equals(txtShohinCode.Text) Then '商品コード
            Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【商品コード】"), txtShohinCode)
        End If

        '特売の場合は取引先コード・適用終了日必須
        If chkTokubaiKbn.Checked = True Then
            If "".Equals(txtToriCode.Text) Then '取引先コード
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【取引先コード】"), txtToriCode)
            End If
            If dtpTekiyoToDt.Value Is Nothing Then '適用終了日
                Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【適用終了日】"), dtpTekiyoToDt)
            End If
            If dtpTekiyoToDt.Value < dtpTekiyoFrDt.Value Then
                Throw New UsrDefException("大小関係が正しくありません。", _msgHd.getMSG("ErrDaiSyoChk", "【適用開始日】【適用終了日】"), dtpTekiyoFrDt)
            End If
        End If

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
            sql = sql & N & " m25_slprice"
            sql = sql & N & " WHERE"
            sql = sql & N & "       会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            sql = sql & N & "   AND 商品コード = '" & Me.txtShohinCode.Text & "'"
            sql = sql & N & "   AND 特売区分 = " & If(Me.chkTokubaiKbn.Checked = True, TOKUBAIKBN_TOKUBAI, TOKUBAIKBN_IGAI)
            sql = sql & N & "   AND 取引先コード = '" & If(Me.txtToriCode.Text = String.Empty, TORICODE_ALL, Me.txtToriCode.Text) & "'"
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
    Private Sub Insert_M40HANBAITANKA()

        Try
            '販売単価マスタの最新レコード更新
            Dim sql As String = ""
            sql = "UPDATE m25_slprice"
            sql = sql & N & " SET"
            sql = sql & N & "  適用終了日 = '" & DateTime.Parse(Me.dtpTekiyoFrDt.Value).AddDays(-1).ToString("yyyy/MM/dd") & "'"                                 '適用終了日
            sql = sql & N & " ,更新者 = " & nullToStr(frmC01F10_Login.loginValue.TantoCD)          '更新者
            sql = sql & N & " ,更新日 = current_timestamp"                                         '更新日
            sql = sql & N & " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"     '会社コード
            sql = sql & N & "   AND 商品コード = '" & Me.txtShohinCode.Text & "' "                               '商品コード
            If Me.chkTokubaiKbn.Checked = True Then
                sql = sql & N & "   AND 特売区分 = " & TOKUBAIKBN_TOKUBAI
            Else
                sql = sql & N & "   AND 特売区分 = " & TOKUBAIKBN_IGAI
            End If
            If Me.txtToriCode.Text = "" Then
                sql = sql & N & "   AND 取引先コード = '" & TORICODE_ALL & "' "                               '取引先コード
            Else
                sql = sql & N & "   AND 取引先コード = '" & Me.txtToriCode.Text & "' "                               '取引先コード
            End If

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
            sql = sql & N & " FROM m25_slprice "
            sql = sql & N & " Where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and 商品コード = '" & Me.txtShohinCode.Text & "' and 特売区分 = " & If(Me.chkTokubaiKbn.Checked = True, 1, 0) & " and 取引先コード = '" & If(Me.txtToriCode.Text = String.Empty, TORICODE_ALL, Me.txtToriCode.Text) & "' and 適用開始日 >= '" & Me.dtpTekiyoFrDt.Value & "'"
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

            '販売単価マスタ追加
            sql = ""
            sql = "INSERT INTO"
            sql = sql & N & " m25_slprice "
            sql = sql & N & "("
            sql = sql & N & "  会社コード"          '会社コード
            sql = sql & N & ", 商品コード"          '商品コード
            sql = sql & N & ", 特売区分"            '特売区分
            sql = sql & N & ", 取引先コード"        '取引先コード
            sql = sql & N & ", 適用開始日"          '適用開始日
            sql = sql & N & ", 適用終了日"          '適用終了日
            sql = sql & N & ", 販売単価"            '販売単価
            sql = sql & N & ", メモ"                'メモ
            sql = sql & N & ", 更新者"              '更新者
            sql = sql & N & ", 更新日"              '更新日
            sql = sql & N & ")"
            sql = sql & N & " VALUES"
            sql = sql & N & "("
            sql = sql & N & "  " & nullToStr(frmC01F10_Login.loginValue.BumonCD)
            sql = sql & N & ", " & nullToStr(Me.txtShohinCode.Text)
            If Me.chkTokubaiKbn.Checked = True Then
                sql = sql & N & ", " & TOKUBAIKBN_TOKUBAI
            Else
                sql = sql & N & ", " & TOKUBAIKBN_IGAI
            End If
            If Me.txtToriCode.Text = "" Then
                sql = sql & N & ", '" & TORICODE_ALL & "'"
            Else
                sql = sql & N & ", " & nullToStr(Me.txtToriCode.Text)
            End If
            If _ShoriMode = CommonConst.MODE_EditStatus Then
                sql = sql & N & ", '" & Me.dtpTekiyoFrDt.Value & "'"
            Else
                sql = sql & N & ", '" & Me.dtpTekiyoFrDt.Text & "'"
            End If
            If Me.chkTokubaiKbn.Checked = True Then
                sql = sql & N & ", '" & Me.dtpTekiyoToDt.Text & "'"
            Else
                sql = sql & N & ", '" & tekiyoShuryoDate & "'"
            End If
            sql = sql & N & ", " & nullToNum(Me.txtHanbaiTanka.Value)
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
    Private Sub Delete_M40HANBAITANKA()

        '特売以外の場合
        If Me.chkTokubaiKbn.Checked = False Then
            '削除対象のデータの適用開始日より前の日付の適用終了日を持つデータが存在するかどうか
            '存在した場合直近のデータを前歴データとするため適用終了日の降順でソートする
            Try
                Dim sql As String = ""
                'sql編集
                sql = ""
                sql = "SELECT "
                sql = sql & N & "    適用開始日, 適用終了日, 販売単価, メモ "
                sql = sql & N & " FROM m25_slprice "
                sql = sql & N & " Where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and 商品コード = '" & Me.txtShohinCode.Text & "' and 特売区分 = " & If(Me.chkTokubaiKbn.Checked = True, 1, 0) & " and 取引先コード = '" & If(Me.txtToriCode.Text = String.Empty, TORICODE_ALL, Me.txtToriCode.Text) & "' and 適用終了日 < '" & Me.txtTekiyoFrDt.Text & "'"
                sql = sql & N & " ORDER BY 適用終了日 DESC "

                Dim iRecCnt As Integer = 0
                Dim iRecCnt2 As Integer = 0
                Dim iRecCnt3 As Integer = 0
                Try
                    'sql発行
                    Dim oDataSet As DataSet = _db.selectDB(sql, RS, iRecCnt)    '抽出結果をDSへ格納
                    Dim tekiyoKaisiDate As String = String.Empty
                    Dim tekiyoShuryoDate As String = String.Empty
                    Dim hanbaiTanka As Double = 0
                    Dim memo As String = String.Empty
                    '存在しない場合
                    '削除対象のデータの適用終了日より後の日付の適用開始日を持つデータが存在するかどうか
                    If iRecCnt = 0 Then
                        sql = ""
                        sql = "SELECT "
                        sql = sql & N & "    min(適用開始日) 開始日 "
                        sql = sql & N & " FROM m25_slprice "
                        sql = sql & N & " Where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and 商品コード = '" & Me.txtShohinCode.Text & "' and 特売区分 = " & If(Me.chkTokubaiKbn.Checked = True, 1, 0) & " and 取引先コード = '" & If(Me.txtToriCode.Text = String.Empty, TORICODE_ALL, Me.txtToriCode.Text) & "' and 適用開始日 > '" & Me.txtTekiyoToDt.Text & "'"
                        Dim oDataSet2 As DataSet = _db.selectDB(sql, RS, iRecCnt2)    '抽出結果をDSへ格納
                        '存在する場合は該当データの適用開始日を削除対象データの適用開始日で更新する
                        If nullToStr(oDataSet2.Tables(RS).Rows(0)("開始日")) <> "NULL" Then
                            '適用開始日はキー項目なのでDELETE／INSERTするためにDELETE前に該当データからキー項目以外の項目（販売単価・適用終了日・メモ）を取得しておく
                            tekiyoKaisiDate = DateTime.Parse(oDataSet2.Tables(RS).Rows(0)("開始日")).ToString("yyyy/MM/dd")
                            sql = ""
                            sql = "SELECT "
                            sql = sql & N & "    販売単価, 適用終了日, メモ "
                            sql = sql & N & " FROM m25_slprice "
                            sql = sql & N & " Where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and 商品コード = '" & Me.txtShohinCode.Text & "' and 特売区分 = " & If(Me.chkTokubaiKbn.Checked = True, 1, 0) & " and 取引先コード = '" & If(Me.txtToriCode.Text = String.Empty, TORICODE_ALL, Me.txtToriCode.Text) & "' and 適用開始日 = '" & tekiyoKaisiDate & "'"
                            Dim oDataSet3 As DataSet = _db.selectDB(sql, RS, iRecCnt3)    '抽出結果をDSへ格納
                            If iRecCnt3 <> 0 Then
                                hanbaiTanka = _db.rmNullDouble(oDataSet3.Tables(RS).Rows(0)("販売単価"))
                                tekiyoShuryoDate = DateTime.Parse(oDataSet3.Tables(RS).Rows(0)("適用終了日")).ToString("yyyy/MM/dd")
                                memo = _db.rmNullStr(oDataSet3.Tables(RS).Rows(0)("メモ"))
                            End If


                            Try
                                '該当データ削除
                                Dim sql2 As String = ""
                                sql2 = "DELETE FROM m25_slprice"
                                sql2 = sql2 & N & " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"      '会社コード
                                sql2 = sql2 & N & "   AND 商品コード = '" & Me.txtShohinCode.Text & "' "                               '商品コード
                                If Me.chkTokubaiKbn.Checked = True Then
                                    sql2 = sql2 & N & "   AND 特売区分 = " & TOKUBAIKBN_TOKUBAI
                                Else
                                    sql2 = sql2 & N & "   AND 特売区分 = " & TOKUBAIKBN_IGAI
                                End If
                                If Me.txtToriCode.Text = "" Then
                                    sql2 = sql2 & N & "   AND 取引先コード = '" & TORICODE_ALL & "' "                               '取引先コード
                                Else
                                    sql2 = sql2 & N & "   AND 取引先コード = '" & Me.txtToriCode.Text & "' "                               '取引先コード
                                End If
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
                                sql3 = "DELETE FROM m25_slprice"
                                sql3 = sql3 & N & " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"      '会社コード
                                sql3 = sql3 & N & "   AND 商品コード = '" & Me.txtShohinCode.Text & "' "                               '商品コード
                                If Me.chkTokubaiKbn.Checked = True Then
                                    sql3 = sql3 & N & "   AND 特売区分 = " & TOKUBAIKBN_TOKUBAI
                                Else
                                    sql3 = sql3 & N & "   AND 特売区分 = " & TOKUBAIKBN_IGAI
                                End If
                                If Me.txtToriCode.Text = "" Then
                                    sql3 = sql3 & N & "   AND 取引先コード = '" & TORICODE_ALL & "' "                               '取引先コード
                                Else
                                    sql3 = sql3 & N & "   AND 取引先コード = '" & Me.txtToriCode.Text & "' "                               '取引先コード
                                End If
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
                                sql = sql & N & " m25_slprice "
                                sql = sql & N & "("
                                sql = sql & N & "  会社コード"          '会社コード
                                sql = sql & N & ", 商品コード"          '商品コード
                                sql = sql & N & ", 特売区分"            '特売区分
                                sql = sql & N & ", 取引先コード"        '取引先コード
                                sql = sql & N & ", 適用開始日"          '適用開始日
                                sql = sql & N & ", 適用終了日"          '適用終了日
                                sql = sql & N & ", 販売単価"            '販売単価
                                sql = sql & N & ", メモ"                'メモ
                                sql = sql & N & ", 更新者"              '更新者
                                sql = sql & N & ", 更新日"              '更新日
                                sql = sql & N & ")"
                                sql = sql & N & " VALUES"
                                sql = sql & N & "("
                                sql = sql & N & "  " & nullToStr(frmC01F10_Login.loginValue.BumonCD)
                                sql = sql & N & ", " & nullToStr(Me.txtShohinCode.Text)
                                If Me.chkTokubaiKbn.Checked = True Then
                                    sql = sql & N & ", " & TOKUBAIKBN_TOKUBAI
                                Else
                                    sql = sql & N & ", " & TOKUBAIKBN_IGAI
                                End If
                                If Me.txtToriCode.Text = "" Then
                                    sql = sql & N & ", '" & TORICODE_ALL & "'"
                                Else
                                    sql = sql & N & ", " & nullToStr(Me.txtToriCode.Text)
                                End If
                                sql = sql & N & ", '" & Me.txtTekiyoFrDt.Text & "'"
                                sql = sql & N & ", '" & tekiyoShuryoDate & "'"
                                sql = sql & N & ", " & nullToNum(hanbaiTanka)
                                sql = sql & N & ", " & nullToStr(memo)
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

                        Else
                            '存在しない場合は削除対象のデータしか存在しないので削除対象のデータを削除して終了する
                            Try
                                '販売単価マスタ削除
                                sql = ""
                                sql = "DELETE FROM m25_slprice"
                                sql = sql & N & " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"      '会社コード
                                sql = sql & N & "   AND 商品コード = '" & Me.txtShohinCode.Text & "' "                               '商品コード
                                If Me.chkTokubaiKbn.Checked = True Then
                                    sql = sql & N & "   AND 特売区分 = " & TOKUBAIKBN_TOKUBAI
                                Else
                                    sql = sql & N & "   AND 特売区分 = " & TOKUBAIKBN_IGAI
                                End If
                                If Me.txtToriCode.Text = "" Then
                                    sql = sql & N & "   AND 取引先コード = '" & TORICODE_ALL & "' "                               '取引先コード
                                Else
                                    sql = sql & N & "   AND 取引先コード = '" & Me.txtToriCode.Text & "' "                               '取引先コード
                                End If
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
                            '販売単価マスタの最新レコード更新
                            sql = ""
                            sql = "UPDATE m25_slprice"
                            sql = sql & N & " SET"
                            sql = sql & N & "  適用終了日 = '" & Me.txtTekiyoToDt.Text & "'"                                 '適用終了日
                            sql = sql & N & " ,更新者 = " & nullToStr(frmC01F10_Login.loginValue.TantoCD)          '更新者
                            sql = sql & N & " ,更新日 = current_timestamp"                                         '更新日
                            sql = sql & N & " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"     '会社コード
                            sql = sql & N & "   AND 商品コード = '" & Me.txtShohinCode.Text & "' "                               '商品コード
                            If Me.chkTokubaiKbn.Checked = True Then
                                sql = sql & N & "   AND 特売区分 = " & TOKUBAIKBN_TOKUBAI
                            Else
                                sql = sql & N & "   AND 特売区分 = " & TOKUBAIKBN_IGAI
                            End If
                            If Me.txtToriCode.Text = "" Then
                                sql = sql & N & "   AND 取引先コード = '" & TORICODE_ALL & "' "                               '取引先コード
                            Else
                                sql = sql & N & "   AND 取引先コード = '" & Me.txtToriCode.Text & "' "                               '取引先コード
                            End If
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
                            '販売単価マスタ削除
                            sql = ""
                            sql = "DELETE FROM m25_slprice"
                            sql = sql & N & " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"      '会社コード
                            sql = sql & N & "   AND 商品コード = '" & Me.txtShohinCode.Text & "' "                               '商品コード
                            If Me.chkTokubaiKbn.Checked = True Then
                                sql = sql & N & "   AND 特売区分 = " & TOKUBAIKBN_TOKUBAI
                            Else
                                sql = sql & N & "   AND 特売区分 = " & TOKUBAIKBN_IGAI
                            End If
                            If Me.txtToriCode.Text = "" Then
                                sql = sql & N & "   AND 取引先コード = '" & TORICODE_ALL & "' "                               '取引先コード
                            Else
                                sql = sql & N & "   AND 取引先コード = '" & Me.txtToriCode.Text & "' "                               '取引先コード
                            End If
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
        Else

            Try
                '該当データ削除
                Dim sql2 As String = ""
                sql2 = "DELETE FROM m25_slprice"
                sql2 = sql2 & N & " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"      '会社コード
                sql2 = sql2 & N & "   AND 商品コード = '" & Me.txtShohinCode.Text & "' "                               '商品コード
                If Me.chkTokubaiKbn.Checked = True Then
                    sql2 = sql2 & N & "   AND 特売区分 = " & TOKUBAIKBN_TOKUBAI
                Else
                    sql2 = sql2 & N & "   AND 特売区分 = " & TOKUBAIKBN_IGAI
                End If
                If Me.txtToriCode.Text = "" Then
                    sql2 = sql2 & N & "   AND 取引先コード = '" & TORICODE_ALL & "' "                               '取引先コード
                Else
                    sql2 = sql2 & N & "   AND 取引先コード = '" & Me.txtToriCode.Text & "' "                               '取引先コード
                End If
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

        End If

    End Sub

    '-------------------------------------------------------------------------------
    '　sql／Update発行
    '　（処理概要）sql文を作成し，ＤＢへの登録を行う
    '-------------------------------------------------------------------------------
    Private Sub Update_M40HANBAITANKA()

        Try
            '販売単価マスタ削除
            Dim sql As String = ""
            sql = "DELETE FROM m25_slprice"
            sql = sql & N & " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"      '会社コード
            sql = sql & N & "   AND 商品コード = '" & Me.txtShohinCode.Text & "' "                               '商品コード
            If Me.chkTokubaiKbn.Checked = True Then
                sql = sql & N & "   AND 特売区分 = " & TOKUBAIKBN_TOKUBAI
            Else
                sql = sql & N & "   AND 特売区分 = " & TOKUBAIKBN_IGAI
            End If
            If Me.txtToriCode.Text = "" Then
                sql = sql & N & "   AND 取引先コード = '" & TORICODE_ALL & "' "                               '取引先コード
            Else
                sql = sql & N & "   AND 取引先コード = '" & Me.txtToriCode.Text & "' "                               '取引先コード
            End If
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
            sql = sql & N & " FROM m25_slprice "
            sql = sql & N & " Where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and 商品コード = '" & Me.txtShohinCode.Text & "' and 特売区分 = " & If(Me.chkTokubaiKbn.Checked = True, 1, 0) & " and 取引先コード = '" & If(Me.txtToriCode.Text = String.Empty, TORICODE_ALL, Me.txtToriCode.Text) & "' and 適用開始日 >= '" & Me.dtpTekiyoFrDt.Value & "'"
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

            '販売単価マスタ追加
            sql = ""
            sql = "INSERT INTO"
            sql = sql & N & " m25_slprice "
            sql = sql & N & "("
            sql = sql & N & "  会社コード"          '会社コード
            sql = sql & N & ", 商品コード"          '商品コード
            sql = sql & N & ", 特売区分"            '特売区分
            sql = sql & N & ", 取引先コード"        '取引先コード
            sql = sql & N & ", 適用開始日"          '適用開始日
            sql = sql & N & ", 適用終了日"          '適用終了日
            sql = sql & N & ", 販売単価"            '販売単価
            sql = sql & N & ", メモ"                'メモ
            sql = sql & N & ", 更新者"              '更新者
            sql = sql & N & ", 更新日"              '更新日
            sql = sql & N & ")"
            sql = sql & N & " VALUES"
            sql = sql & N & "("
            sql = sql & N & "  " & nullToStr(frmC01F10_Login.loginValue.BumonCD)
            sql = sql & N & ", " & nullToStr(Me.txtShohinCode.Text)
            If Me.chkTokubaiKbn.Checked = True Then
                sql = sql & N & ", " & TOKUBAIKBN_TOKUBAI
            Else
                sql = sql & N & ", " & TOKUBAIKBN_IGAI
            End If
            If Me.txtToriCode.Text = "" Then
                sql = sql & N & ", '" & TORICODE_ALL & "'"
            Else
                sql = sql & N & ", " & nullToStr(Me.txtToriCode.Text)
            End If
            If _ShoriMode = CommonConst.MODE_EditStatus Then
                sql = sql & N & ", '" & Me.dtpTekiyoFrDt.Value & "'"
            Else
                sql = sql & N & ", '" & Me.dtpTekiyoFrDt.Text & "'"
            End If
            If Me.chkTokubaiKbn.Checked = True Then
                sql = sql & N & ", '" & Me.dtpTekiyoToDt.Text & "'"
            Else
                sql = sql & N & ", '" & tekiyoShuryoDate & "'"
            End If
            sql = sql & N & ", " & nullToNum(Me.txtHanbaiTanka.Value)
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

    'マスタデータ取得
    Private Sub getMasterData()

        'データ初期表示
        'キー項目を取得
        Dim shohinCode As String = psShohinCode
        Dim tokubaikbn As Integer = psTokubaikbn
        Dim toriCode As String = psToriCode
        Dim tekiyoFrDT As String = psTekiyoFrDT

        Dim sql As String = ""

        Try
            sql = "SELECT "
            sql = sql & N & "  sp.商品コード, sp.特売区分, sp.取引先コード, sp.適用開始日, sp.適用終了日, sp.販売単価, sp.メモ, sp.更新者, sp.更新日 "
            sql = sql & N & " ,g.商品名 商品名 "
            sql = sql & N & " ,c.取引先名 取引先名 "
            sql = sql & N & ", u.氏名 更新者名 "
            sql = sql & N & " FROM m25_slprice sp "
            sql = sql & N & " left join m20_goods g on g.会社コード = sp.会社コード and g.商品コード = sp.商品コード "
            sql = sql & N & " left join m10_customer c on c.会社コード = sp.会社コード and c.取引先コード = sp.取引先コード "
            sql = sql & N & " LEFT JOIN m02_user u on u.会社コード = sp.会社コード AND u.ユーザＩＤ = sp.更新者 "
            sql = sql & N & " Where sp.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and sp.商品コード = '" & shohinCode & "' and sp.特売区分 = " & tokubaikbn & " and sp.取引先コード = '" & toriCode & "' and sp.適用開始日 = '" & tekiyoFrDT & "'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

            If _ShoriMode <> CommonConst.MODE_ADDNEWCOPY Then
                Me.txtShohinCode.Text = shohinCode
                Me.txtShohinName.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("商品名"))

                Select Case _db.rmNullStr(ds.Tables(RS).Rows(0)("特売区分"))
                    Case "0"
                        Me.chkTokubaiKbn.Checked = False
                    Case "1"
                        Me.chkTokubaiKbn.Checked = True
                End Select
                Me.txtToriCode.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("取引先コード"))
                Me.txtToriName.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("取引先名"))
                Me.dtpTekiyoFrDt.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("適用開始日"))
                Me.txtTekiyoFrDt.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("適用開始日"))
                Me.dtpTekiyoToDt.Text = DateAdd("d", 0, ds.Tables(RS).Rows(0)("適用終了日")).ToString("yyyy/MM/dd")
                Me.txtTekiyoToDt.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("適用終了日"))
                Me.lblKousinsya.Text = MIDASHI_KOUSINSYA & _db.rmNullStr(ds.Tables(RS).Rows(0)("更新者名"))
                Me.lblKousinbi.Text = MIDASHI_KOUSINBI & _db.rmNullStr(ds.Tables(RS).Rows(0)("更新日"))
            End If
            Me.txtHanbaiTanka.Text = _db.rmNullDouble(ds.Tables(RS).Rows(0)("販売単価"))
            Me.txtMemo.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("メモ"))

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

        Me.txtShohinCode.Text = String.Empty
        Me.txtShohinName.Text = String.Empty
        Me.chkTokubaiKbn.Checked = False
        Me.txtToriCode.Text = String.Empty
        Me.txtToriName.Text = String.Empty
        Me.dtpTekiyoFrDt.Value = DateTime.Now.ToString()
        Me.dtpTekiyoFrDt.Text = String.Empty
        Me.txtTekiyoFrDt.Text = String.Empty
        Me.dtpTekiyoToDt.Value = DateTime.Now.ToString()
        Me.dtpTekiyoToDt.Text = String.Empty
        Me.txtTekiyoToDt.Text = String.Empty
        Me.txtHanbaiTanka.Text = String.Empty
        Me.txtMemo.Text = String.Empty

        Me.lblKousinsya.Text = MIDASHI_KOUSINSYA
        Me.lblKousinbi.Text = MIDASHI_KOUSINBI

    End Sub

    '背景色セット
    Private Sub setBackColor()

        Select Case _ShoriMode
            Case CommonConst.MODE_EditStatus     '変更
                Me.txtShohinCode.BackColor = _readOnlyTextBackColor
                Me.txtToriCode.BackColor = _readOnlyTextBackColor
                Me.txtTekiyoFrDt.BackColor = _readOnlyTextBackColor
                If chkTokubaiKbn.Checked = False Then
                    Me.txtTekiyoToDt.BackColor = _readOnlyTextBackColor
                End If
            Case CommonConst.MODE_ADDNEW, CommonConst.MODE_ADDNEWCOPY     '新規追加,複写新規
                If chkTokubaiKbn.Checked = False Then
                    Me.txtTekiyoToDt.BackColor = _readOnlyTextBackColor
                End If
            Case CommonConst.MODE_InquiryStatus, CommonConst.MODE_DELETE     '参照,削除
                Me.txtShohinCode.BackColor = _readOnlyTextBackColor
                Me.txtToriCode.BackColor = _readOnlyTextBackColor
                Me.txtTekiyoFrDt.BackColor = _readOnlyTextBackColor
                Me.txtTekiyoToDt.BackColor = _readOnlyTextBackColor
                Me.txtHanbaiTanka.BackColor = _readOnlyTextBackColor
                Me.txtMemo.BackColor = _readOnlyTextBackColor
        End Select

    End Sub

    '画面項目のプロパティセット
    Private Sub setProperty()

        Select Case _ShoriMode
            Case CommonConst.MODE_InquiryStatus, CommonConst.MODE_DELETE     '参照,削除
                Me.txtShohinCode.ReadOnly = True
                Me.chkTokubaiKbn.Enabled = False
                Me.txtToriCode.ReadOnly = True
                Me.dtpTekiyoFrDt.Visible = False
                Me.txtTekiyoFrDt.Visible = True
                Me.dtpTekiyoToDt.Visible = False
                Me.txtTekiyoToDt.Visible = True
                Me.txtHanbaiTanka.ReadOnly = True
                Me.txtMemo.ReadOnly = True
                Me.txtShohinCode.TabStop = False
                Me.chkTokubaiKbn.TabStop = False
                Me.txtToriCode.TabStop = False
                Me.txtHanbaiTanka.TabStop = False
                Me.txtMemo.TabStop = False
            Case CommonConst.MODE_ADDNEW, CommonConst.MODE_ADDNEWCOPY     '新規追加,複写新規
                Me.txtShohinCode.ReadOnly = False
                Me.chkTokubaiKbn.Enabled = True
                Me.txtToriCode.ReadOnly = False
                Me.dtpTekiyoFrDt.Visible = True
                Me.txtTekiyoFrDt.Visible = False
                Me.txtHanbaiTanka.ReadOnly = False
                Me.txtMemo.ReadOnly = False
                If chkTokubaiKbn.Checked = True Then
                    Me.dtpTekiyoToDt.Visible = True
                    Me.txtTekiyoToDt.Visible = False
                    Me.dtpTekiyoToDt.TabStop = True
                Else
                    Me.dtpTekiyoToDt.Visible = False
                    Me.txtTekiyoToDt.Visible = True
                    Me.dtpTekiyoToDt.TabStop = False
                End If
            Case CommonConst.MODE_EditStatus     '変更
                Me.txtShohinCode.ReadOnly = True
                Me.chkTokubaiKbn.Enabled = False
                Me.txtToriCode.ReadOnly = True
                Me.txtHanbaiTanka.ReadOnly = False
                Me.txtMemo.ReadOnly = False
                Me.txtShohinCode.TabStop = False
                Me.chkTokubaiKbn.TabStop = False
                Me.txtToriCode.TabStop = False
                Me.dtpTekiyoFrDt.Visible = False
                Me.txtTekiyoFrDt.Visible = True
                Me.txtTekiyoFrDt.ReadOnly = True
                Me.txtHanbaiTanka.TabStop = True
                Me.txtMemo.TabStop = True
                If chkTokubaiKbn.Checked = True Then
                    Me.dtpTekiyoToDt.Visible = True
                    Me.txtTekiyoToDt.Visible = False
                    Me.dtpTekiyoToDt.TabStop = True
                Else
                    Me.dtpTekiyoToDt.Visible = False
                    Me.txtTekiyoToDt.Visible = True
                    Me.dtpTekiyoToDt.TabStop = False
                End If
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

    Private Sub txtShohinCode_DoubleClick(sender As Object, e As EventArgs) Handles txtShohinCode.DoubleClick
        Dim openForm As frmC10F10_Shohin = New frmC10F10_Shohin(_msgHd, _db, CommonConst.TORIHIKISAKI_TARGET_KBN_SHUKKA)      '画面遷移
        openForm.ShowDialog()                      '画面表示
        If openForm.Selected Then
            '選択されました
            Me.txtShohinCode.Text = openForm.GettShohinCD
            Me.txtShohinName.Text = openForm.GetShohinNM
        End If
        openForm = Nothing
    End Sub

    Private Sub txtToriCode_DoubleClick(sender As Object, e As EventArgs) Handles txtToriCode.DoubleClick
        Dim openForm As frmC10F20_Torihikisaki = New frmC10F20_Torihikisaki(_msgHd, _db, CommonConst.TORIHIKISAKI_TARGET_KBN_SHUKKA)      '画面遷移
        openForm.ShowDialog()                      '画面表示
        If openForm.Selected Then
            '選択されました
            Me.txtToriCode.Text = openForm.GetValTorihikisakiCd
            Me.txtToriName.Text = openForm.GetValTorihikisakiName
        End If
        openForm = Nothing

    End Sub

    Private Sub ctl_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtShohinCode.KeyPress,
                                                                                                    txtToriCode.KeyPress,
                                                                                                    dtpTekiyoFrDt.KeyPress,
                                                                                                    dtpTekiyoToDt.KeyPress,
                                                                                                    txtHanbaiTanka.KeyPress,
                                                                                                    txtMemo.KeyPress

        UtilMDL.UtilClass.moveNextFocus(Me, e)
    End Sub

    '-------------------------------------------------------------------------------
    '　フォーカス取得イベント
    '-------------------------------------------------------------------------------
    Private Sub ctl_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtShohinCode.GotFocus,
                                                                                          txtToriCode.GotFocus,
                                                                                          dtpTekiyoFrDt.GotFocus,
                                                                                          dtpTekiyoToDt.GotFocus,
                                                                                          txtHanbaiTanka.GotFocus,
                                                                                          txtMemo.GotFocus

        'フォーカス取得時、入力パラメタのコントロールを全選択状態とする
        Call UtilClass.selAll(sender)

    End Sub

    Private Sub chkTokubaiKbn_CheckedChanged(sender As Object, e As EventArgs) Handles chkTokubaiKbn.CheckedChanged
        If chkTokubaiKbn.Checked = True Then
            Me.dtpTekiyoToDt.Visible = True
            Me.txtTekiyoToDt.Visible = False
            Me.dtpTekiyoToDt.TabStop = True
            Me.txtTekiyoToDt.TabStop = False
        Else
            Me.dtpTekiyoToDt.Visible = False
            Me.txtTekiyoToDt.Visible = True
            Me.dtpTekiyoToDt.TabStop = False
            Me.txtTekiyoToDt.TabStop = True
        End If

    End Sub

    '商品名取得処理
    Private Sub getShohinName()

        '商品名データを取得
        Dim sql As String = ""

        sql = sql & "SELECT "
        sql = sql & N & "    商品名 "
        sql = sql & N & " FROM m20_goods "
        sql = sql & N & " Where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        sql = sql & N & " and 商品コード = '" + _db.rmSQ(Me.txtShohinCode.Text) + "' "

        Dim reccnt As Integer = 0

        Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

        'データカウント
        Dim dataCount As Integer = ds.Tables(RS).Rows.Count

        If dataCount = 0 Then
            'データ0件

            '商品名クリア
            Me.txtShohinName.Clear()

        Else

            '取得データ
            Dim dataRow As DataRow = ds.Tables(RS).Rows(0)

            '商品名に取得データをセット
            Me.txtShohinName.Text = _db.rmNullStr(dataRow("商品名"))
        End If

    End Sub

    Private Sub txtShohinCode_Leave(sender As Object, e As EventArgs) Handles txtShohinCode.Leave

        Try
            '商品コードフォーカスアウト時処理
            getShohinName()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '取引先名取得処理
    Private Sub getToriName()

        '取引先名データを取得
        Dim sql As String = ""

        sql = sql & "SELECT "
        sql = sql & N & "    取引先名 "
        sql = sql & N & " FROM m10_customer "
        sql = sql & N & " Where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        sql = sql & N & " and 取引先コード = '" + _db.rmSQ(Me.txtToriCode.Text) + "' "

        Dim reccnt As Integer = 0

        Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

        'データカウント
        Dim dataCount As Integer = ds.Tables(RS).Rows.Count

        If dataCount = 0 Then
            'データ0件

            '取引先名クリア
            Me.txtToriName.Clear()

        Else

            '取得データ
            Dim dataRow As DataRow = ds.Tables(RS).Rows(0)

            '取引先名に取得データをセット
            Me.txtToriName.Text = _db.rmNullStr(dataRow("取引先名"))
        End If

    End Sub

    Private Sub txtToriCode_Leave(sender As Object, e As EventArgs) Handles txtToriCode.Leave

        Try
            '取引先コードフォーカスアウト時処理
            getToriName()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

#End Region

End Class