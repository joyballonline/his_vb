'===============================================================================
'　 （システム名）      カネキ吉田商店様向け　原価管理システム
'
'   （機能名）          取引先マスタ保守
'   （クラス名）        frmM20F20_TorihikisakiHosyu
'   （処理機能名）      
'   （本MDL使用前提）   
'   （備考）         
'
'===============================================================================
' 履歴  名前               日付       　　　 　内容
'-------------------------------------------------------------------------------
'  (1)  桜井               2018/03/08      　　新規
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.DB
Imports UtilMDL.MSG             'UtilMsgHandler用
Imports System.Text.RegularExpressions

Public Class frmM20F20_TorihikisakiHosyu
#Region "宣言"

    '-------------------------------------------------------------------------------
    ' 定数宣言
    '-------------------------------------------------------------------------------
    ' PG制御文字
    Private Const N As String = ControlChars.NewLine                    ' 改行文字
    Private Const RS As String = "RecSet"                               ' レコードセットテーブル

    '汎用マスメン
    Private Const lM2020_DB_EXIST As Integer = 1                        'あり：登録済
    Private Const lM2020_DB_NONE As Integer = 0                         'なし：未登録

    '更新者・更新日見出しラベル
    Private Const MIDASHI_KOUSINSYA As String = "更新者："
    Private Const MIDASHI_KOUSINBI As String = "更新日："

    '印刷有無
    Private Const PRINT_NASI As Integer = 0                             '印刷なし
    Private Const PRINT_ARI As Integer = 1                              '印刷あり

    '口座種別
    Private Const KOUZA_FUTUU As Integer = 1                            '普通
    Private Const KOUZA_TOUZA As Integer = 2                            '当座

    '端数区分
    Private Const HASU_ROUNDDOWN As Integer = 1                         '切捨
    Private Const HASU_ROUNDOFF As Integer = 2                          '四捨五入
    Private Const HASU_ROUNDUP As Integer = 3                           '切上

    '税算出区分
    Private Const ZEISANSYUTU_DENPYO As Integer = 1                     '伝票
    Private Const ZEISANSYUTU_MEISAI As Integer = 2                     '明細
    Private Const ZEISANSYUTU_SEIKYU As Integer = 3                     '請求時一括

    '該当／非該当
    Private Const GAITO_HIGAITO As Integer = 0                          '非該当
    Private Const GAITO_GAITO As Integer = 1                            '該当

    '-------------------------------------------------------------------------------
    'メンバー変数宣言
    '-------------------------------------------------------------------------------
    Private _db As UtilDBIf
    Private _msgHd As UtilMsgHandler
    Private _parentForm As frmM20F10_TorihikisakiList
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
    Private pCS2020_DBFlg As Long

    '取引先コード格納用変数
    Private psToriCode As String

    Private txtArr As TextBox()
    Private rboArr As RadioButton()

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
                   ByVal prmParentForm As frmM20F10_TorihikisakiList,
                   ByVal prmMode As Integer,
                   ByRef prmSelectID As String,
                   ByRef prmToriCode As String)

        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        _parentForm = prmParentForm
        _ShoriMode = prmMode
        psToriCode = prmToriCode
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint  'フォームタイトル表示
        _companyCd = frmC01F10_Login.loginValue.BumonCD                     '会社コード
        _selectId = prmSelectID                                             '選択処理ID
        _userId = frmC01F10_Login.loginValue.TantoCD                        'ユーザＩＤ

        _readOnlyTextBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))

        ' 共通処理使用の準備
        _comLogc = New CommonLogic(_db, _msgHd)                             ' 共通処理用

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
    Private Sub frmM20F20_TorihikisakiHosyu_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim BkCur As Cursor = Cursor.Current                                ' 現在のカーソルを取っておく
        Try
            Cursor.Current = Cursors.WaitCursor                             ' 砂時計カーソルに入れ替える

            _open = True                                                    '画面起動済フラグ

            txtArr = New TextBox() {
                    txtToriCode,
                    txtToriName,
                    txtToriRyakuName,
                    txtToriNameKana,
                    txtPostalCd1,
                    txtPostalCd2,
                    txtAddress1,
                    txtAddress2,
                    txtAddress3,
                    txtTelNo,
                    txtFaxNo,
                    txtTantoSya,
                    txtIrainusi,
                    txtJikanSitei,
                    txtHaisoNissu,
                    txtUnsobinCode,
                    txtSimebi,
                    txtBankName,
                    txtSitenName,
                    txtKouzaNo,
                    txtMeiginin,
                    txtSyukkasakiGCode,
                    txtSeikyusakiCode,
                    txtSiharaisakiCode,
                    txtMemo
                    }
            rboArr = New RadioButton() {
                    rboOkurijoNasi,
                    rboOkurijoAri,
                    rboNifudaNasi,
                    rboNifudaAri,
                    rboNouhinDenpyoNasi,
                    rboNouhinDenpyoAri,
                    rboSeikyuDenpyoNasi,
                    rboSeikyuDenpyoAri,
                    rboLespritNasi,
                    rboLespritAri,
                    rboKouzaSyubetuFutuu,
                    rboKouzaSyubetuTouza,
                    rboKinHKbnRoundDown,
                    rboKinHKbnRoundOff,
                    rboKinHKbnRoundUp,
                    rboZeiSKbnDenpyo,
                    rboZeiSKbnMeisai,
                    rboZeiSKbnSeikyu,
                    rboZeiHKbnRoundDown,
                    rboZeiHKbnRoundOff,
                    rboZeiHKbnRoundUp,
                    rboSeikyuHigaito,
                    rboSeikyuGaito,
                    rboSyukkaHigaito,
                    rboSyukkaGaito,
                    rboSyukkaGHigaito,
                    rboSyukkaGGaito,
                    rboSiireHigaito,
                    rboSiireGaito,
                    rboSiharaiHigaito,
                    rboSiharaiGaito,
                    rboSyukkaItaku,
                    rboSyukkaUriage
                    }

            Dim i As Integer = 0
            Dim j As Integer = 0
            For i = 0 To txtArr.Length - 1
                AddHandler txtArr(i).Click, AddressOf ctl_GotFocus
                AddHandler txtArr(i).KeyPress, AddressOf ctl_KeyPress
            Next
            For j = 0 To rboArr.Length - 1
                AddHandler rboArr(j).Click, AddressOf ctl_GotFocus
                AddHandler rboArr(j).KeyPress, AddressOf ctl_KeyPress
            Next

            '画面項目のクリア
            initializeDisplay()

            '新規以外
            If _ShoriMode <> CommonConst.MODE_ADDNEW Then
                'マスタデータ取得
                getMasterData()
            End If

            '操作履歴ログ作成
            Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, CommonConst.MENU_M2002, CommonConst.PROGRAM_UPDATE, CommonConst.STATUS_NORMAL,
                                                Me.txtToriCode.Text, DBNull.Value, lblShoriMode.Text, DBNull.Value, DBNull.Value,
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
                        sql = sql & N & " FROM M10_CUSTOMER c "
                        sql = sql & N & " Where c.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and c.取引先コード = '" & Me.txtToriCode.Text & "'"
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
                    If M20TORIHIKISAKI_Add_Ctl() = False Then
                        Exit Sub
                    End If
                Case CommonConst.MODE_ADDNEWCOPY    '複写新規
                    If M20TORIHIKISAKI_Add_Ctl() = False Then
                        Exit Sub
                    End If
                Case CommonConst.MODE_EditStatus    '変更
                    If M20TORIHIKISAKI_Upd_Ctl() = False Then
                        Exit Sub
                    End If
                Case CommonConst.MODE_DELETE        '削除
                    If M20TORIHIKISAKI_Del_Ctl() = False Then
                        Exit Sub
                    End If
            End Select

            '操作履歴ログ作成 
            Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, CommonConst.MENU_M2002, CommonConst.PROGRAM_REPORT, CommonConst.STATUS_NORMAL,
                                                Me.txtToriCode.Text, DBNull.Value, lblShoriMode.Text, DBNull.Value, DBNull.Value,
                                                DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _userId)

            '更新フラグ：登録済
            pCS2020_DBFlg = lM2020_DB_EXIST

            '取引先コードを親フォームに返却（新規・複写新規は保守画面の入力値）
            '選択行の位置づけに使用
            _parentForm.toriCode = Me.txtToriCode.Text
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
    Private Function M20TORIHIKISAKI_Add_Ctl() As Boolean

        M20TORIHIKISAKI_Add_Ctl = False

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
            Call Insert_M20TORIHIKISAKI()

            '完了メッセージ表示
            Call _msgHd.dspMSG("completeInsert")                                      '登録が完了しました。

            'トランザクション終了
            _db.commitTran()

            M20TORIHIKISAKI_Add_Ctl = True

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
    Private Function M20TORIHIKISAKI_Upd_Ctl() As Boolean

        M20TORIHIKISAKI_Upd_Ctl = False

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
            Call Delete_M20TORIHIKISAKI()

            '追加処理
            Call Insert_M20TORIHIKISAKI()

            '完了メッセージ表示
            Call _msgHd.dspMSG("completeUpdate")                                      '登録が完了しました。

            'トランザクション終了
            _db.commitTran()

            M20TORIHIKISAKI_Upd_Ctl = True

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
    Private Function M20TORIHIKISAKI_Del_Ctl() As Boolean

        M20TORIHIKISAKI_Del_Ctl = False

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
            Call Delete_M20TORIHIKISAKI()

            '完了メッセージ表示
            Call _msgHd.dspMSG("completeDelete")                                      '削除が完了しました。

            'トランザクション終了
            _db.commitTran()

            M20TORIHIKISAKI_Del_Ctl = True

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

        If "".Equals(txtToriCode.Text) Then '取引先コード
            Throw New UsrDefException("必須入力項目です。", _msgHd.getMSG("requiredImput", "【取引先コード】"), txtToriCode)
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
            sql = sql & N & " m10_customer"
            sql = sql & N & " WHERE"
            sql = sql & N & "       会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
            sql = sql & N & "   AND 取引先コード = '" & Me.txtToriCode.Text & "'"

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
    Private Sub Insert_M20TORIHIKISAKI()

        Try
            '取引先マスタ追加
            Dim sql As String = ""
            sql = "INSERT INTO"
            sql = sql & N & " m10_customer "
            sql = sql & N & "("
            sql = sql & N & "  会社コード"          '会社コード
            sql = sql & N & ", 取引先コード"        '取引先コード
            sql = sql & N & ", 取引先名"            '取引先名
            sql = sql & N & ", 取引先名略称"        '取引先名略称
            sql = sql & N & ", 取引先名カナ"        '取引先名カナ
            sql = sql & N & ", 郵便番号"            '郵便番号
            sql = sql & N & ", 住所１"              '住所１
            sql = sql & N & ", 住所２"              '住所２
            sql = sql & N & ", 住所３"              '住所３
            sql = sql & N & ", 電話番号"            '電話番号
            sql = sql & N & ", 電話番号検索用"      '電話番号検索用
            sql = sql & N & ", ＦＡＸ番号"          'ＦＡＸ番号
            sql = sql & N & ", 担当者名"            '担当者名
            sql = sql & N & ", 依頼主等"            '依頼主等
            sql = sql & N & ", 時間指定"            '時間指定
            sql = sql & N & ", 配送日数"            '配送日数
            sql = sql & N & ", 運送便コード"        '運送便コード
            sql = sql & N & ", 送り状印刷有無"      '送り状印刷有無
            sql = sql & N & ", 荷札印刷有無"        '荷札印刷有無
            sql = sql & N & ", 納品伝票印刷有無"    '納品伝票印刷有無
            sql = sql & N & ", 請求伝票印刷有無"    '請求伝票印刷有無
            sql = sql & N & ", レスプリ印刷有無"    'レスプリ印刷有無 
            sql = sql & N & ", 締日"                '締日
            sql = sql & N & ", 銀行名"              '銀行名
            sql = sql & N & ", 支店名"              '支店名
            sql = sql & N & ", 口座種別"            '口座種別
            sql = sql & N & ", 口座番号"            '口座番号
            sql = sql & N & ", 名義人名"            '名義人名
            sql = sql & N & ", 金額端数区分"        '金額端数区分
            sql = sql & N & ", 税算出区分"          '税算出区分
            sql = sql & N & ", 税端数区分"          '税端数区分
            sql = sql & N & ", 請求先該当"          '請求先該当
            sql = sql & N & ", 出荷先該当"          '出荷先該当
            sql = sql & N & ", 出荷先Ｇ該当"        '出荷先Ｇ該当
            sql = sql & N & ", 仕入先該当"          '仕入先該当
            sql = sql & N & ", 支払先該当"          '支払先該当
            sql = sql & N & ", 出荷先分類"          '出荷先分類
            sql = sql & N & ", 出荷先Ｇコード"      '出荷先Ｇコード
            sql = sql & N & ", 請求先コード"        '請求先コード
            sql = sql & N & ", 支払先コード"        '支払先コード
            sql = sql & N & ", メモ"                'メモ
            sql = sql & N & ", 更新者"              '更新者
            sql = sql & N & ", 更新日"              '更新日
            sql = sql & N & ")"
            sql = sql & N & " VALUES"
            sql = sql & N & "("
            sql = sql & N & "  " & nullToStr(frmC01F10_Login.loginValue.BumonCD)
            sql = sql & N & ", " & nullToStr(Me.txtToriCode.Text)
            sql = sql & N & ", " & nullToStr(Me.txtToriName.Text)
            sql = sql & N & ", " & nullToStr(Me.txtToriRyakuName.Text)
            sql = sql & N & ", " & nullToStr(Me.txtToriNameKana.Text)
            sql = sql & N & ", '" & Me.txtPostalCd1.Text & Me.txtPostalCd2.Text & "' "
            sql = sql & N & ", " & nullToStr(Me.txtAddress1.Text)
            sql = sql & N & ", " & nullToStr(Me.txtAddress2.Text)
            sql = sql & N & ", " & nullToStr(Me.txtAddress3.Text)
            sql = sql & N & ", " & nullToStr(Me.txtTelNo.Text)
            Dim reg As New Regex("[^\d]")
            Dim strDes As String = reg.Replace(Me.txtTelNo.Text, "")
            sql = sql & N & ", '" & strDes & "' "
            sql = sql & N & ", " & nullToStr(Me.txtFaxNo.Text)
            sql = sql & N & ", " & nullToStr(Me.txtTantoSya.Text)
            sql = sql & N & ", " & nullToStr(Me.txtIrainusi.Text)
            sql = sql & N & ", " & nullToStr(Me.txtJikanSitei.Text)
            sql = sql & N & ", " & nullToNum(Me.txtHaisoNissu.Text)
            sql = sql & N & ", " & nullToStr(Me.txtUnsobinCode.Text)
            '送り状印刷有無
            If Me.rboOkurijoNasi.Checked Then
                sql = sql & N & ", " & PRINT_NASI                   '印刷なし
            End If
            If Me.rboOkurijoAri.Checked Then
                sql = sql & N & ", " & PRINT_ARI                    '印刷あり
            End If
            If Me.rboOkurijoNasi.Checked = False And Me.rboOkurijoAri.Checked = False Then
                sql = sql & N & ", NULL"
            End If
            '荷札印刷有無
            If Me.rboNifudaNasi.Checked Then
                sql = sql & N & ", " & PRINT_NASI                   '印刷なし
            End If
            If Me.rboNifudaAri.Checked Then
                sql = sql & N & ", " & PRINT_ARI                    '印刷あり
            End If
            If Me.rboNifudaNasi.Checked = False And Me.rboNifudaAri.Checked = False Then
                sql = sql & N & ", NULL"
            End If
            '納品伝票印刷有無
            If Me.rboNouhinDenpyoNasi.Checked Then
                sql = sql & N & ", " & PRINT_NASI                   '印刷なし
            End If
            If Me.rboNouhinDenpyoAri.Checked Then
                sql = sql & N & ", " & PRINT_ARI                    '印刷あり
            End If
            If Me.rboNouhinDenpyoNasi.Checked = False And Me.rboNouhinDenpyoAri.Checked = False Then
                sql = sql & N & ", NULL"
            End If
            '請求伝票印刷有無
            If Me.rboSeikyuDenpyoNasi.Checked Then
                sql = sql & N & ", " & PRINT_NASI                   '印刷なし
            End If
            If Me.rboSeikyuDenpyoAri.Checked Then
                sql = sql & N & ", " & PRINT_ARI                    '印刷あり
            End If
            If Me.rboSeikyuDenpyoNasi.Checked = False And Me.rboSeikyuDenpyoAri.Checked = False Then
                sql = sql & N & ", NULL"
            End If
            'レスプリ印刷有無
            If Me.rboLespritNasi.Checked Then
                sql = sql & N & ", " & PRINT_NASI                   '印刷なし
            End If
            If Me.rboLespritAri.Checked Then
                sql = sql & N & ", " & PRINT_ARI                    '印刷あり
            End If
            If Me.rboLespritNasi.Checked = False And Me.rboLespritAri.Checked = False Then
                sql = sql & N & ", NULL"
            End If
            sql = sql & N & ", " & nullToNum(Me.txtSimebi.Text)
            sql = sql & N & ", " & nullToStr(Me.txtBankName.Text)
            sql = sql & N & ", " & nullToStr(Me.txtSitenName.Text)
            '口座種別
            If Me.rboKouzaSyubetuFutuu.Checked Then
                sql = sql & N & ", " & KOUZA_FUTUU                  '普通
            End If
            If Me.rboKouzaSyubetuTouza.Checked Then
                sql = sql & N & ", " & KOUZA_TOUZA                  '当座
            End If
            If Me.rboKouzaSyubetuFutuu.Checked = False And Me.rboKouzaSyubetuTouza.Checked = False Then
                sql = sql & N & ", NULL"
            End If
            sql = sql & N & ", " & nullToStr(Me.txtKouzaNo.Text)    '口座番号
            sql = sql & N & ", " & nullToStr(Me.txtMeiginin.Text)   '名義人名
            '金額端数区分
            If Me.rboKinHKbnRoundDown.Checked Then
                sql = sql & N & ", " & HASU_ROUNDDOWN               '切捨
            End If
            If Me.rboKinHKbnRoundOff.Checked Then
                sql = sql & N & ", " & HASU_ROUNDOFF                '四捨五入
            End If
            If Me.rboKinHKbnRoundUp.Checked Then
                sql = sql & N & ", " & HASU_ROUNDUP                 '切上
            End If
            If Me.rboKinHKbnRoundDown.Checked = False And Me.rboKinHKbnRoundOff.Checked = False And Me.rboKinHKbnRoundUp.Checked = False Then
                sql = sql & N & ", NULL"
            End If
            '税算出区分
            If Me.rboZeiSKbnDenpyo.Checked Then
                sql = sql & N & ", " & ZEISANSYUTU_DENPYO           '伝票
            End If
            If Me.rboZeiSKbnMeisai.Checked Then
                sql = sql & N & ", " & ZEISANSYUTU_MEISAI           '明細
            End If
            If Me.rboZeiSKbnSeikyu.Checked Then
                sql = sql & N & ", " & ZEISANSYUTU_SEIKYU           '請求時一括
            End If
            If Me.rboZeiSKbnDenpyo.Checked = False And Me.rboZeiSKbnMeisai.Checked = False And Me.rboZeiSKbnSeikyu.Checked = False Then
                sql = sql & N & ", NULL"
            End If
            '税端数区分
            If Me.rboZeiHKbnRoundDown.Checked Then
                sql = sql & N & ", " & HASU_ROUNDDOWN               '切捨
            End If
            If Me.rboZeiHKbnRoundOff.Checked Then
                sql = sql & N & ", " & HASU_ROUNDOFF                '四捨五入
            End If
            If Me.rboZeiHKbnRoundUp.Checked Then
                sql = sql & N & ", " & HASU_ROUNDUP                 '切上
            End If
            If Me.rboZeiHKbnRoundDown.Checked = False And Me.rboZeiHKbnRoundOff.Checked = False And Me.rboZeiHKbnRoundUp.Checked = False Then
                sql = sql & N & ", NULL"
            End If
            '請求先該当
            If Me.rboSeikyuHigaito.Checked Then
                sql = sql & N & ", " & GAITO_HIGAITO                '非該当
            End If
            If Me.rboSeikyuGaito.Checked Then
                sql = sql & N & ", " & GAITO_GAITO                  '該当
            End If
            If Me.rboSeikyuHigaito.Checked = False And Me.rboSeikyuGaito.Checked = False Then
                sql = sql & N & ", NULL"
            End If
            '出荷先該当
            If Me.rboSyukkaHigaito.Checked Then
                sql = sql & N & ", " & GAITO_HIGAITO                '非該当
            End If
            If Me.rboSyukkaGaito.Checked Then
                sql = sql & N & ", " & GAITO_GAITO                  '該当
            End If
            If Me.rboSyukkaHigaito.Checked = False And Me.rboSyukkaGaito.Checked = False Then
                sql = sql & N & ", NULL"
            End If
            '出荷先Ｇ該当
            If Me.rboSyukkaGHigaito.Checked Then
                sql = sql & N & ", " & GAITO_HIGAITO                '非該当
            End If
            If Me.rboSyukkaGGaito.Checked Then
                sql = sql & N & ", " & GAITO_GAITO                  '該当
            End If
            If Me.rboSyukkaGHigaito.Checked = False And Me.rboSyukkaGGaito.Checked = False Then
                sql = sql & N & ", NULL"
            End If
            '仕入先該当
            If Me.rboSiireHigaito.Checked Then
                sql = sql & N & ", " & GAITO_HIGAITO                '非該当
            End If
            If Me.rboSiireGaito.Checked Then
                sql = sql & N & ", " & GAITO_GAITO                  '該当
            End If
            If Me.rboSiireHigaito.Checked = False And Me.rboSiireGaito.Checked = False Then
                sql = sql & N & ", NULL"
            End If
            '支払先該当
            If Me.rboSiharaiHigaito.Checked Then
                sql = sql & N & ", " & GAITO_HIGAITO                '非該当
            End If
            If Me.rboSiharaiGaito.Checked Then
                sql = sql & N & ", " & GAITO_GAITO                  '該当
            End If
            If Me.rboSiharaiHigaito.Checked = False And Me.rboSiharaiGaito.Checked = False Then
                sql = sql & N & ", NULL"
            End If
            '出荷先分類
            If Me.rboSyukkaItaku.Checked Then
                sql = sql & N & ", " & CommonConst.SKBUNRUI_ITAKU                '委託
            End If
            If Me.rboSyukkaUriage.Checked Then
                sql = sql & N & ", " & CommonConst.SKBUNRUI_URIAGE               '売上
            End If
            If Me.rboSyukkaItaku.Checked = False And Me.rboSyukkaUriage.Checked = False Then
                sql = sql & N & ", NULL"
            End If
            sql = sql & N & ", " & nullToStr(Me.txtSyukkasakiGCode.Text)
            sql = sql & N & ", " & nullToStr(Me.txtSeikyusakiCode.Text)
            sql = sql & N & ", " & nullToStr(Me.txtSiharaisakiCode.Text)
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
    Private Sub Delete_M20TORIHIKISAKI()

        Try
            '取引先マスタ更新
            Dim sql As String = ""
            sql = "DELETE FROM m10_customer"
            sql = sql & N & " WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"      '会社コード
            sql = sql & N & "   AND 取引先コード = '" & Me.txtToriCode.Text & "' "                               '取引先コード

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
        Dim toriCode As String = psToriCode

        Dim sql As String = ""

        Try
            sql = "SELECT "
            sql = sql & N & "  c.取引先コード, c.取引先名, c.取引先名略称, c.取引先名カナ, c.郵便番号, c.住所１, c.住所２, c.住所３, c.電話番号, c.電話番号検索用, c.ＦＡＸ番号, c.担当者名, c.依頼主等, c.時間指定, c.配送日数, c.運送便コード, c.送り状印刷有無, c.荷札印刷有無, c.納品伝票印刷有無, c.請求伝票印刷有無, c.レスプリ印刷有無, c.締日, c.銀行名, c.支店名, c.口座種別, c.口座番号, c.名義人名, c.金額端数区分, c.税算出区分, c.税端数区分, c.請求先該当, c.出荷先該当, c.出荷先Ｇ該当, c.仕入先該当, c.支払先該当, c.出荷先分類, c.出荷先Ｇコード, c.請求先コード, c.支払先コード, c.メモ, c.更新者, c.更新日 "
            sql = sql & N & " ,h.文字１ 運送便名 "
            sql = sql & N & ", u.氏名 更新者名 "
            sql = sql & N & ", c1.取引先名 出荷先Ｇ名 "
            sql = sql & N & ", c2.取引先名 請求先名 "
            sql = sql & N & ", c3.取引先名 支払先名 "
            sql = sql & N & " FROM m10_customer c "
            sql = sql & N & " left join M90_HANYO h on h.会社コード = c.会社コード and h.固定キー = '" & CommonConst.HANYO_KOTEI_UNSOUBIN & "' and h.可変キー = c.運送便コード "
            sql = sql & N & " LEFT JOIN m02_user u on u.会社コード = c.会社コード AND u.ユーザＩＤ = c.更新者 "
            sql = sql & N & " LEFT JOIN m10_customer c1 on c.会社コード = c1.会社コード AND c.出荷先Ｇコード = c1.取引先コード "
            sql = sql & N & " LEFT JOIN m10_customer c2 on c.会社コード = c2.会社コード AND c.請求先コード = c2.取引先コード "
            sql = sql & N & " LEFT JOIN m10_customer c3 on c.会社コード = c3.会社コード AND c.支払先コード = c3.取引先コード "
            sql = sql & N & " Where c.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and c.取引先コード = '" & toriCode & "'"

            Dim reccnt As Integer = 0
            Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

            If _ShoriMode <> CommonConst.MODE_ADDNEWCOPY Then
                Me.txtToriCode.Text = toriCode
            End If

            Me.txtToriName.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("取引先名"))
            Me.txtToriRyakuName.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("取引先名略称"))
            Me.txtToriNameKana.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("取引先名カナ"))
            '郵便番号
            Dim postalCd1 As String
            Dim postalCd2 As String
            If String.IsNullOrEmpty(_db.rmNullStr(ds.Tables(RS).Rows(0)("郵便番号"))) Then
                postalCd1 = ""
                postalCd2 = ""
            Else
                postalCd1 = (_db.rmNullStr(ds.Tables(RS).Rows(0)("郵便番号"))).Substring(0, 3)
                postalCd2 = (_db.rmNullStr(ds.Tables(RS).Rows(0)("郵便番号"))).Substring(3, 4)
            End If
            Me.txtPostalCd1.Text = _db.rmNullStr(postalCd1)
            Me.txtPostalCd2.Text = _db.rmNullStr(postalCd2)
            Me.txtAddress1.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("住所１"))
            Me.txtAddress2.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("住所２"))
            Me.txtAddress3.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("住所３"))
            Me.txtTelNo.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("電話番号"))
            Me.txtFaxNo.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("ＦＡＸ番号"))
            Me.txtTantoSya.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("担当者名"))
            Me.txtIrainusi.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("依頼主等"))
            Me.txtJikanSitei.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("時間指定"))
            Me.txtHaisoNissu.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("配送日数"))
            Me.txtUnsobinCode.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("運送便コード"))
            Me.txtUnsoBinName.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("運送便名"))
            If Not String.IsNullOrEmpty(_db.rmNullStr(ds.Tables(RS).Rows(0)("送り状印刷有無"))) Then
                Select Case _db.rmNullStr(ds.Tables(RS).Rows(0)("送り状印刷有無"))
                    Case "0"
                        Me.rboOkurijoNasi.Checked = True
                    Case "1"
                        Me.rboOkurijoAri.Checked = True
                End Select
            End If
            If Not String.IsNullOrEmpty(_db.rmNullStr(ds.Tables(RS).Rows(0)("荷札印刷有無"))) Then
                Select Case _db.rmNullStr(ds.Tables(RS).Rows(0)("荷札印刷有無"))
                    Case "0"
                        Me.rboNifudaNasi.Checked = True
                    Case "1"
                        Me.rboNifudaAri.Checked = True
                End Select
            End If
            If Not String.IsNullOrEmpty(_db.rmNullStr(ds.Tables(RS).Rows(0)("納品伝票印刷有無"))) Then
                Select Case _db.rmNullStr(ds.Tables(RS).Rows(0)("納品伝票印刷有無"))
                    Case "0"
                        Me.rboNouhinDenpyoNasi.Checked = True
                    Case "1"
                        Me.rboNouhinDenpyoAri.Checked = True
                End Select
            End If
            If Not String.IsNullOrEmpty(_db.rmNullStr(ds.Tables(RS).Rows(0)("請求伝票印刷有無"))) Then
                Select Case _db.rmNullStr(ds.Tables(RS).Rows(0)("請求伝票印刷有無"))
                    Case "0"
                        Me.rboSeikyuDenpyoNasi.Checked = True
                    Case "1"
                        Me.rboSeikyuDenpyoAri.Checked = True
                End Select
            End If
            If Not String.IsNullOrEmpty(_db.rmNullStr(ds.Tables(RS).Rows(0)("レスプリ印刷有無"))) Then
                Select Case _db.rmNullStr(ds.Tables(RS).Rows(0)("レスプリ印刷有無"))
                    Case "0"
                        Me.rboLespritNasi.Checked = True
                    Case "1"
                        Me.rboLespritAri.Checked = True
                End Select
            End If
            Me.txtSimebi.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("締日"))
            Me.txtBankName.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("銀行名"))
            Me.txtSitenName.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("支店名"))
            If Not String.IsNullOrEmpty(_db.rmNullStr(ds.Tables(RS).Rows(0)("口座種別"))) Then
                Select Case _db.rmNullStr(ds.Tables(RS).Rows(0)("口座種別"))
                    Case "1"
                        Me.rboKouzaSyubetuFutuu.Checked = True
                    Case "2"
                        Me.rboKouzaSyubetuTouza.Checked = True
                End Select
            End If
            Me.txtKouzaNo.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("口座番号"))
            Me.txtMeiginin.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("名義人名"))
            If Not String.IsNullOrEmpty(_db.rmNullStr(ds.Tables(RS).Rows(0)("金額端数区分"))) Then
                Select Case _db.rmNullStr(ds.Tables(RS).Rows(0)("金額端数区分"))
                    Case "1"
                        Me.rboKinHKbnRoundDown.Checked = True
                    Case "2"
                        Me.rboKinHKbnRoundOff.Checked = True
                    Case "3"
                        Me.rboKinHKbnRoundUp.Checked = True
                End Select
            End If
            If Not String.IsNullOrEmpty(_db.rmNullStr(ds.Tables(RS).Rows(0)("税算出区分"))) Then
                Select Case _db.rmNullStr(ds.Tables(RS).Rows(0)("税算出区分"))
                    Case "1"
                        Me.rboZeiSKbnDenpyo.Checked = True
                    Case "2"
                        Me.rboZeiSKbnMeisai.Checked = True
                    Case "3"
                        Me.rboZeiSKbnSeikyu.Checked = True
                End Select
            End If
            If Not String.IsNullOrEmpty(_db.rmNullStr(ds.Tables(RS).Rows(0)("税端数区分"))) Then
                Select Case _db.rmNullStr(ds.Tables(RS).Rows(0)("税端数区分"))
                    Case "1"
                        Me.rboZeiHKbnRoundDown.Checked = True
                    Case "2"
                        Me.rboZeiHKbnRoundOff.Checked = True
                    Case "3"
                        Me.rboZeiHKbnRoundUp.Checked = True
                End Select
            End If
            If Not String.IsNullOrEmpty(_db.rmNullStr(ds.Tables(RS).Rows(0)("請求先該当"))) Then
                Select Case _db.rmNullStr(ds.Tables(RS).Rows(0)("請求先該当"))
                    Case "0"
                        Me.rboSeikyuHigaito.Checked = True
                    Case "1"
                        Me.rboSeikyuGaito.Checked = True
                End Select
            End If
            If Not String.IsNullOrEmpty(_db.rmNullStr(ds.Tables(RS).Rows(0)("出荷先該当"))) Then
                Select Case _db.rmNullStr(ds.Tables(RS).Rows(0)("出荷先該当"))
                    Case "0"
                        Me.rboSyukkaHigaito.Checked = True
                    Case "1"
                        Me.rboSyukkaGaito.Checked = True
                End Select
            End If
            If Not String.IsNullOrEmpty(_db.rmNullStr(ds.Tables(RS).Rows(0)("出荷先Ｇ該当"))) Then
                Select Case _db.rmNullStr(ds.Tables(RS).Rows(0)("出荷先Ｇ該当"))
                    Case "0"
                        Me.rboSyukkaGHigaito.Checked = True
                    Case "1"
                        Me.rboSyukkaGGaito.Checked = True
                End Select
            End If
            If Not String.IsNullOrEmpty(_db.rmNullStr(ds.Tables(RS).Rows(0)("仕入先該当"))) Then
                Select Case _db.rmNullStr(ds.Tables(RS).Rows(0)("仕入先該当"))
                    Case "0"
                        Me.rboSiireHigaito.Checked = True
                    Case "1"
                        Me.rboSiireGaito.Checked = True
                End Select
            End If
            If Not String.IsNullOrEmpty(_db.rmNullStr(ds.Tables(RS).Rows(0)("支払先該当"))) Then
                Select Case _db.rmNullStr(ds.Tables(RS).Rows(0)("支払先該当"))
                    Case "0"
                        Me.rboSiharaiHigaito.Checked = True
                    Case "1"
                        Me.rboSiharaiGaito.Checked = True
                End Select
            End If
            If Not String.IsNullOrEmpty(_db.rmNullStr(ds.Tables(RS).Rows(0)("出荷先分類"))) Then
                Select Case _db.rmNullStr(ds.Tables(RS).Rows(0)("出荷先分類"))
                    Case "1"
                        Me.rboSyukkaItaku.Checked = True
                    Case "2"
                        Me.rboSyukkaUriage.Checked = True
                End Select
            End If
            Me.txtSyukkasakiGCode.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("出荷先Ｇコード"))
            Me.txtSyukkasakiGName.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("出荷先Ｇ名"))
            Me.txtSeikyusakiCode.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("請求先コード"))
            Me.txtSeikyusakiName.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("請求先名"))
            Me.txtSiharaisakiCode.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("支払先コード"))
            Me.txtSiharaisakiName.Text = _db.rmNullStr(ds.Tables(RS).Rows(0)("支払先名"))
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

        Dim i As Integer = 0
        Dim j As Integer = 0
        For i = 0 To txtArr.Length - 1
            txtArr(i).Text = String.Empty
        Next
        For j = 0 To rboArr.Length - 1
            rboArr(j).Checked = True
        Next

        Me.lblKousinsya.Text = MIDASHI_KOUSINSYA
        Me.lblKousinbi.Text = MIDASHI_KOUSINBI

    End Sub

    '背景色セット
    Private Sub setBackColor()

        Select Case _ShoriMode
            Case CommonConst.MODE_EditStatus     '変更
                Me.txtToriCode.BackColor = _readOnlyTextBackColor
            Case CommonConst.MODE_InquiryStatus, CommonConst.MODE_DELETE     '参照,削除
                Me.txtToriCode.BackColor = _readOnlyTextBackColor
                Me.txtToriName.BackColor = _readOnlyTextBackColor
                Me.txtToriRyakuName.BackColor = _readOnlyTextBackColor
                Me.txtToriNameKana.BackColor = _readOnlyTextBackColor
                Me.txtPostalCd1.BackColor = _readOnlyTextBackColor
                Me.txtPostalCd2.BackColor = _readOnlyTextBackColor
                Me.txtAddress1.BackColor = _readOnlyTextBackColor
                Me.txtAddress2.BackColor = _readOnlyTextBackColor
                Me.txtAddress3.BackColor = _readOnlyTextBackColor
                Me.txtTelNo.BackColor = _readOnlyTextBackColor
                Me.txtFaxNo.BackColor = _readOnlyTextBackColor
                Me.txtTantoSya.BackColor = _readOnlyTextBackColor
                Me.txtIrainusi.BackColor = _readOnlyTextBackColor
                Me.txtJikanSitei.BackColor = _readOnlyTextBackColor
                Me.txtHaisoNissu.BackColor = _readOnlyTextBackColor
                Me.txtUnsobinCode.BackColor = _readOnlyTextBackColor
                Me.txtSimebi.BackColor = _readOnlyTextBackColor
                Me.txtBankName.BackColor = _readOnlyTextBackColor
                Me.txtSitenName.BackColor = _readOnlyTextBackColor
                Me.txtKouzaNo.BackColor = _readOnlyTextBackColor
                Me.txtMeiginin.BackColor = _readOnlyTextBackColor
                Me.txtSyukkasakiGCode.BackColor = _readOnlyTextBackColor
                Me.txtSeikyusakiCode.BackColor = _readOnlyTextBackColor
                Me.txtSiharaisakiCode.BackColor = _readOnlyTextBackColor
                Me.txtMemo.BackColor = _readOnlyTextBackColor
        End Select

    End Sub

    '画面項目のプロパティセット
    Private Sub setProperty()

        Select Case _ShoriMode
            Case CommonConst.MODE_InquiryStatus, CommonConst.MODE_DELETE     '参照,削除
                Me.txtToriCode.ReadOnly = True
                Me.txtToriName.ReadOnly = True
                Me.txtToriRyakuName.ReadOnly = True
                Me.txtToriNameKana.ReadOnly = True
                Me.txtPostalCd1.ReadOnly = True
                Me.txtPostalCd2.ReadOnly = True
                Me.txtAddress1.ReadOnly = True
                Me.txtAddress2.ReadOnly = True
                Me.txtAddress3.ReadOnly = True
                Me.txtTelNo.ReadOnly = True
                Me.txtFaxNo.ReadOnly = True
                Me.txtTantoSya.ReadOnly = True
                Me.txtIrainusi.ReadOnly = True
                Me.txtJikanSitei.ReadOnly = True
                Me.txtHaisoNissu.ReadOnly = True
                Me.txtUnsobinCode.ReadOnly = True
                Me.rboOkurijoNasi.Enabled = False
                Me.rboOkurijoAri.Enabled = False
                Me.rboNifudaNasi.Enabled = False
                Me.rboNifudaAri.Enabled = False
                Me.rboNouhinDenpyoNasi.Enabled = False
                Me.rboNouhinDenpyoAri.Enabled = False
                Me.rboSeikyuDenpyoNasi.Enabled = False
                Me.rboSeikyuDenpyoAri.Enabled = False
                Me.rboLespritNasi.Enabled = False
                Me.rboLespritAri.Enabled = False
                Me.txtSimebi.ReadOnly = True
                Me.txtBankName.ReadOnly = True
                Me.txtSitenName.ReadOnly = True
                Me.rboKouzaSyubetuFutuu.Enabled = False
                Me.rboKouzaSyubetuTouza.Enabled = False
                Me.txtKouzaNo.ReadOnly = True
                Me.txtMeiginin.ReadOnly = True
                Me.rboKinHKbnRoundDown.Enabled = False
                Me.rboKinHKbnRoundOff.Enabled = False
                Me.rboKinHKbnRoundUp.Enabled = False
                Me.rboZeiSKbnDenpyo.Enabled = False
                Me.rboZeiSKbnMeisai.Enabled = False
                Me.rboZeiSKbnSeikyu.Enabled = False
                Me.rboZeiHKbnRoundDown.Enabled = False
                Me.rboZeiHKbnRoundOff.Enabled = False
                Me.rboZeiHKbnRoundUp.Enabled = False
                Me.rboSeikyuHigaito.Enabled = False
                Me.rboSeikyuGaito.Enabled = False
                Me.rboSyukkaHigaito.Enabled = False
                Me.rboSyukkaGaito.Enabled = False
                Me.rboSyukkaGHigaito.Enabled = False
                Me.rboSyukkaGGaito.Enabled = False
                Me.rboSiireHigaito.Enabled = False
                Me.rboSiireGaito.Enabled = False
                Me.rboSiharaiHigaito.Enabled = False
                Me.rboSiharaiGaito.Enabled = False
                Me.rboSyukkaItaku.Enabled = False
                Me.rboSyukkaUriage.Enabled = False
                Me.txtSyukkasakiGCode.ReadOnly = True
                Me.txtSeikyusakiCode.ReadOnly = True
                Me.txtSiharaisakiCode.ReadOnly = True
                Me.txtMemo.ReadOnly = True
                Me.txtToriCode.TabStop = False
                Me.txtToriName.TabStop = False
                Me.txtToriRyakuName.TabStop = False
                Me.txtToriNameKana.TabStop = False
                Me.txtPostalCd1.TabStop = False
                Me.txtPostalCd2.TabStop = False
                Me.txtAddress1.TabStop = False
                Me.txtAddress2.TabStop = False
                Me.txtAddress3.TabStop = False
                Me.txtTelNo.TabStop = False
                Me.txtFaxNo.TabStop = False
                Me.txtTantoSya.TabStop = False
                Me.txtIrainusi.TabStop = False
                Me.txtJikanSitei.TabStop = False
                Me.txtHaisoNissu.TabStop = False
                Me.txtUnsobinCode.TabStop = False
                Me.txtSimebi.TabStop = False
                Me.txtBankName.TabStop = False
                Me.txtSitenName.TabStop = False
                Me.txtKouzaNo.TabStop = False
                Me.txtMeiginin.TabStop = False
                Me.txtSyukkasakiGCode.TabStop = False
                Me.txtSeikyusakiCode.TabStop = False
                Me.txtSiharaisakiCode.TabStop = False
                Me.txtMemo.TabStop = False
            Case CommonConst.MODE_ADDNEW, CommonConst.MODE_ADDNEWCOPY     '新規追加,複写新規
                Me.txtToriCode.ReadOnly = False
                Me.txtToriName.ReadOnly = False
                Me.txtToriRyakuName.ReadOnly = False
                Me.txtToriNameKana.ReadOnly = False
                Me.txtPostalCd1.ReadOnly = False
                Me.txtPostalCd2.ReadOnly = False
                Me.txtAddress1.ReadOnly = False
                Me.txtAddress2.ReadOnly = False
                Me.txtAddress3.ReadOnly = False
                Me.txtTelNo.ReadOnly = False
                Me.txtFaxNo.ReadOnly = False
                Me.txtTantoSya.ReadOnly = False
                Me.txtIrainusi.ReadOnly = False
                Me.txtJikanSitei.ReadOnly = False
                Me.txtHaisoNissu.ReadOnly = False
                Me.txtUnsobinCode.ReadOnly = False
                Me.txtSimebi.ReadOnly = False
                Me.txtBankName.ReadOnly = False
                Me.txtSitenName.ReadOnly = False
                Me.txtKouzaNo.ReadOnly = False
                Me.txtMeiginin.ReadOnly = False
                Me.txtSyukkasakiGCode.ReadOnly = False
                Me.txtSeikyusakiCode.ReadOnly = False
                Me.txtSiharaisakiCode.ReadOnly = False
                Me.txtMemo.ReadOnly = False
            Case CommonConst.MODE_EditStatus     '変更
                Me.txtToriCode.ReadOnly = True
                Me.txtToriName.ReadOnly = False
                Me.txtToriRyakuName.ReadOnly = False
                Me.txtToriNameKana.ReadOnly = False
                Me.txtPostalCd1.ReadOnly = False
                Me.txtPostalCd2.ReadOnly = False
                Me.txtAddress1.ReadOnly = False
                Me.txtAddress2.ReadOnly = False
                Me.txtAddress3.ReadOnly = False
                Me.txtTelNo.ReadOnly = False
                Me.txtFaxNo.ReadOnly = False
                Me.txtTantoSya.ReadOnly = False
                Me.txtIrainusi.ReadOnly = False
                Me.txtJikanSitei.ReadOnly = False
                Me.txtHaisoNissu.ReadOnly = False
                Me.txtUnsobinCode.ReadOnly = False
                Me.txtSimebi.ReadOnly = False
                Me.txtBankName.ReadOnly = False
                Me.txtSitenName.ReadOnly = False
                Me.txtKouzaNo.ReadOnly = False
                Me.txtMeiginin.ReadOnly = False
                Me.txtSyukkasakiGCode.ReadOnly = False
                Me.txtSeikyusakiCode.ReadOnly = False
                Me.txtSiharaisakiCode.ReadOnly = False
                Me.txtMemo.ReadOnly = False
                Me.txtToriCode.TabStop = False
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

    '郵便番号検索ウインドウオープン処理
    Private Sub postalCdSelectWindowOpen()

        '入力郵便番号
        Dim inputPostalCd1 As String = Me.txtPostalCd1.Text  '郵便番号１
        Dim inputPostalCd2 As String = Me.txtPostalCd2.Text  '郵便番号２

        Dim openForm As frmC10F30_Postal = New frmC10F30_Postal(_msgHd, _db, inputPostalCd1, inputPostalCd2)      '画面遷移
        openForm.ShowDialog()                      '画面表示

        '選択されている場合
        If openForm.Selected Then
            '画面に値をセット

            '郵便番号１
            Me.txtPostalCd1.Text = openForm.GetValPostalCd1
            '郵便番号２
            Me.txtPostalCd2.Text = openForm.GetValPostalCd2
            '住所１
            Me.txtAddress1.Text = openForm.GetValAddress
        End If

        openForm = Nothing

    End Sub

    Private Sub txtPostalCd_DoubleClick(sender As Object, e As EventArgs) Handles txtPostalCd1.DoubleClick,
                                                                                  txtPostalCd2.DoubleClick
        Try
            '郵便番号検索ウインドウオープン
            postalCdSelectWindowOpen()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    Private Sub txtUnsobinCode_DoubleClick(sender As Object, e As EventArgs) Handles txtUnsobinCode.DoubleClick
        Dim openForm As frmC10F90_Hanyo = New frmC10F90_Hanyo(_msgHd, _db, CommonConst.HANYO_KOTEI_UNSOUBIN)      '画面遷移
        openForm.ShowDialog()                      '画面表示
        If openForm.Selected Then
            '選択されました
            Me.txtUnsobinCode.Text = openForm.GetValCD
            Me.txtUnsoBinName.Text = openForm.GetValNM
        End If
        openForm = Nothing
    End Sub

    Private Sub ctl_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs)

        UtilMDL.UtilClass.moveNextFocus(Me, e)
    End Sub

    '-------------------------------------------------------------------------------
    '　フォーカス取得イベント
    '-------------------------------------------------------------------------------
    Private Sub ctl_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)

        'フォーカス取得時、入力パラメタのコントロールを全選択状態とする
        Call UtilClass.selAll(sender)

    End Sub

    Private Sub txtPostalCd_Leave(sender As Object, e As EventArgs) Handles txtPostalCd1.Leave,
                                                                            txtPostalCd2.Leave

        Try
            '郵便番号フォーカスアウト時処理
            postalCdLeave()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '郵便番号フォーカスアウト時処理
    Private Sub postalCdLeave()

        '郵便番号１、２とも入力がある場合
        If (Not ("".Equals(Me.txtPostalCd1.Text))) AndAlso (Not ("".Equals(Me.txtPostalCd2.Text))) Then
            '住所取得処理
            Call getAddress()
        Else
            '住所１クリア
            Me.txtAddress1.Clear()
        End If

    End Sub

    '運送便コードフォーカスアウト時処理
    Private Sub unsobinCodeLeave()

        '運送便コードに入力がある場合
        If (Not ("".Equals(Me.txtUnsobinCode.Text))) Then
            '運送便名取得処理
            Call getUnsoBinName()
        Else
            '運送便名クリア
            Me.txtUnsoBinName.Clear()
        End If

    End Sub

    '住所取得処理
    Private Sub getAddress()

        '入力郵便番号
        Dim inputPostalCd1 As String = Me.txtPostalCd1.Text  '郵便番号１
        Dim inputPostalCd2 As String = Me.txtPostalCd2.Text  '郵便番号２

        '住所データを取得
        Dim ds As DataSet = _comLogc.getAddress(inputPostalCd1 & inputPostalCd2)

        'データカウント
        Dim dataCount As Integer = ds.Tables(RS).Rows.Count

        If dataCount = 0 Then
            'データ0件

            '住所１クリア
            Me.txtAddress1.Clear()

        ElseIf dataCount = 1 Then
            'データ1件

            '取得データ
            Dim dataRow As DataRow = ds.Tables(RS).Rows(0)

            '住所１に取得データをセット
            Me.txtAddress1.Text = _db.rmNullStr(dataRow("都道府県名")) &
                                  _db.rmNullStr(dataRow("市区町村名")) &
                                  _db.rmNullStr(dataRow("町域名"))

        Else
            'データ2件以上

            '郵便番号検索ウインドウオープン
            postalCdSelectWindowOpen()
        End If

    End Sub

    '運送便名取得処理
    Private Sub getUnsoBinName()

        '運送便名データを取得
        Dim sql As String = ""

        sql = sql & "SELECT "
        sql = sql & N & "    h.文字２ 運送便名 "
        sql = sql & N & " FROM M90_HANYO h "
        sql = sql & N & " Where h.会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' and h.固定キー = '" & CommonConst.HANYO_KOTEI_UNSOUBIN & "'"
        sql = sql & N & " and h.可変キー = '" + _db.rmSQ(Me.txtUnsobinCode.Text) + "' "

        Dim reccnt As Integer = 0

        Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

        'データカウント
        Dim dataCount As Integer = ds.Tables(RS).Rows.Count

        If dataCount = 0 Then
            'データ0件

            '運送便名クリア
            Me.txtUnsoBinName.Clear()

        Else

            '取得データ
            Dim dataRow As DataRow = ds.Tables(RS).Rows(0)

            '運送便名に取得データをセット
            Me.txtUnsoBinName.Text = _db.rmNullStr(dataRow("運送便名"))
        End If

    End Sub

    Private Sub txtUnsobinCode_Leave(sender As Object, e As EventArgs) Handles txtUnsobinCode.Leave

        Try
            '運送便コードフォーカスアウト時処理
            unsobinCodeLeave()

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    Private Sub txtSyukkasakiGCode_DoubleClick(sender As Object, e As EventArgs) Handles txtSyukkasakiGCode.DoubleClick
        Dim openForm As frmC10F20_Torihikisaki = New frmC10F20_Torihikisaki(_msgHd, _db, CommonConst.TORIHIKISAKI_TARGET_KBN_SHUKKAG)      '画面遷移
        openForm.ShowDialog()                      '画面表示
        If openForm.Selected Then
            '選択されました
            Me.txtSyukkasakiGCode.Text = openForm.GetValTorihikisakiCd
            Me.txtSyukkasakiGName.Text = openForm.GetValTorihikisakiName
        End If
        openForm = Nothing

    End Sub

    Private Sub txtSyukkasakiGCode_Leave(sender As Object, e As EventArgs) Handles txtSyukkasakiGCode.Leave

        Try
            '出荷先Gコードフォーカスアウト時処理
            Me.txtSyukkasakiGName.Text = getToriName(Me.txtSyukkasakiGCode.Text)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    '取引先名取得処理
    Private Function getToriName(toriCode As String) As String

        '取引先名データを取得
        Dim sql As String = ""

        sql = sql & "SELECT "
        sql = sql & N & "    取引先名 "
        sql = sql & N & " FROM m10_customer "
        sql = sql & N & " Where 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "'"
        sql = sql & N & " and 取引先コード = '" + _db.rmSQ(toriCode) + "' "

        Dim reccnt As Integer = 0

        Dim ds As DataSet = _db.selectDB(sql, RS, reccnt)

        'データカウント
        Dim dataCount As Integer = ds.Tables(RS).Rows.Count

        If dataCount = 0 Then
            'データ0件

            '取引先名クリア
            getToriName = String.Empty

        Else

            '取得データ
            Dim dataRow As DataRow = ds.Tables(RS).Rows(0)

            '取引先名に取得データをセット
            getToriName = _db.rmNullStr(dataRow("取引先名"))
        End If

    End Function

    Private Sub txtSeikyusakiCode_DoubleClick(sender As Object, e As EventArgs) Handles txtSeikyusakiCode.DoubleClick
        Dim openForm As frmC10F20_Torihikisaki = New frmC10F20_Torihikisaki(_msgHd, _db, CommonConst.TORIHIKISAKI_TARGET_KBN_SEIKYU)      '画面遷移
        openForm.ShowDialog()                      '画面表示
        If openForm.Selected Then
            '選択されました
            Me.txtSeikyusakiCode.Text = openForm.GetValTorihikisakiCd
            Me.txtSeikyusakiName.Text = openForm.GetValTorihikisakiName
        End If
        openForm = Nothing

    End Sub

    Private Sub txtSiharaisakiCode_DoubleClick(sender As Object, e As EventArgs) Handles txtSiharaisakiCode.DoubleClick
        Dim openForm As frmC10F20_Torihikisaki = New frmC10F20_Torihikisaki(_msgHd, _db, CommonConst.TORIHIKISAKI_TARGET_KBN_SHIHARAI)      '画面遷移
        openForm.ShowDialog()                      '画面表示
        If openForm.Selected Then
            '選択されました
            Me.txtSiharaisakiCode.Text = openForm.GetValTorihikisakiCd
            Me.txtSiharaisakiName.Text = openForm.GetValTorihikisakiName
        End If
        openForm = Nothing

    End Sub

    Private Sub txtSeikyusakiCode_Leave(sender As Object, e As EventArgs) Handles txtSeikyusakiCode.Leave

        Try
            '請求先コードフォーカスアウト時処理
            Me.txtSeikyusakiName.Text = getToriName(Me.txtSeikyusakiCode.Text)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

    Private Sub txtSiharaisakiCode_Leave(sender As Object, e As EventArgs) Handles txtSiharaisakiCode.Leave

        Try
            '支払先コードフォーカスアウト時処理
            Me.txtSiharaisakiName.Text = getToriName(Me.txtSiharaisakiCode.Text)

        Catch ue As UsrDefException
            ue.dspMsg()
        Catch ex As Exception
            'キャッチした例外をユーザー定義例外に移し変えシステムエラーMSG出力後スロー
            Throw New UsrDefException(ex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ex)))
        End Try

    End Sub

#End Region

End Class