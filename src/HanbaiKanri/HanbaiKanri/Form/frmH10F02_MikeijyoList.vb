'===============================================================================
'
'　カネキ吉田商店様
'　　（システム名）販売管理
'　　（処理機能名）売上未計上一覧出力指示
'    （フォームID）H10F02
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   狩野        2018/02/28                 新規              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB

Public Class frmH10F02_MikeijyoList
    '-------------------------------------------------------------------------------
    '   定数定義
    '-------------------------------------------------------------------------------
    'PG制御文字 
    Private Const N As String = ControlChars.NewLine                    '改行文字
    Private Const RS As String = "RecSet"                               'レコードセットテーブル
    Private Const DATE_EMPTY As String = "    /  /"

    '-------------------------------------------------------------------------------
    '   変数定義
    '-------------------------------------------------------------------------------
    Private _msgHd As UtilMsgHandler
    Private _db As UtilDBIf
    Private _companyCd As String
    Private _selectId As String
    Private _gyomuId As String
    Private _userId As String
    Private Shared _shoriId As String
    Private Shared _printKbn As String

    Private _comLogc As CommonLogic                         '共通処理用
    Private _frmOpen As Boolean                             '画面起動済フラグ
    Private _parentForm As Form                             '親フォーム


    '-------------------------------------------------------------------------------
    'プロパティ宣言
    '-------------------------------------------------------------------------------
    Public Shared ReadOnly Property shoriId() As String
        Get
            Return _shoriId
        End Get
    End Property
    Public Shared ReadOnly Property printKbn() As String
        Get
            Return _printKbn
        End Get
    End Property

#Region "コンストラクタ"
    '-------------------------------------------------------------------------------
    'デフォルトコンストラクタ（隠蔽）
    '-------------------------------------------------------------------------------
    Private Sub New()
        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()
    End Sub

    '-------------------------------------------------------------------------------
    'コンストラクタ　メニューから呼ばれる
    '-------------------------------------------------------------------------------
    Public Sub New(ByRef prmRefMsgHd As UtilMsgHandler, ByRef prmRefDbHd As UtilDBIf, ByRef prmSelectID As String, ByRef prmKidoShoriID As String, ByRef prmParentForm As Form)
        Call Me.New()

        '初期処理
        _msgHd = prmRefMsgHd                                                'MSGハンドラの設定
        _db = prmRefDbHd                                                    'DBハンドラの設定
        StartPosition = FormStartPosition.CenterScreen                      '画面中央表示
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint   'フォームタイトル表示
        _companyCd = frmC01F10_Login.loginValue.BumonCD                     '会社コード
        _selectId = prmSelectID                                             '選択処理ID
        _shoriId = prmSelectID                                              '起動処理ID
        _gyomuId = prmSelectID.Substring(0, 3)                              '業務ID
        _userId = frmC01F10_Login.loginValue.TantoCD                        'ユーザＩＤ
        _parentForm = prmParentForm

        ' 共通処理使用の準備
        _comLogc = New CommonLogic(_db, _msgHd)                             ' 共通処理用
        _frmOpen = False

    End Sub
#End Region


#Region "フォームロード"
    '-------------------------------------------------------------------------------
    '   画面ロード処理
    '   （処理概要） 画面が起動したときの処理を行う。
    '-------------------------------------------------------------------------------
    Private Sub frmH10F02_MikeijyoList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            '操作履歴ログ作成
            Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, _shoriId, CommonConst.PROGRAM_START, CommonConst.STATUS_NORMAL,
                                                DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value,
                                                DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _userId)

            '画面初期化
            Call initForm()

            _frmOpen = True

        Catch ue As UsrDefException
            ue.dspMsg()
            cmdPreview.Enabled = False
            cmdPrint.Enabled = False
        Catch ex As Exception
            'システムエラー
            Call _msgHd.dspMSG("SystemErr", vbCrLf & "詳細：(" & Err.Number & ")" & vbCrLf & Err.Description & vbCrLf & "(Form_Load)")
        End Try

    End Sub
#End Region

#Region "戻るボタンクリック"
    '-------------------------------------------------------------------------------
    '　戻るボタン押下
    '-------------------------------------------------------------------------------
    Private Sub cmdBack_Click(sender As Object, e As EventArgs) Handles cmdBack.Click
        _parentForm.Show()                                              ' 前画面を表示
        _parentForm.Enabled = True                                      ' 前画面の使用を可能にする
        _parentForm.Activate()                                          ' 前画面をアクティブにする

        Me.Dispose()                                                    ' 自画面を閉じる
    End Sub
#End Region

#Region "プレビューボタンクリック"
    '-------------------------------------------------------------------------------
    '　プレビューボタン押下
    '-------------------------------------------------------------------------------
    Private Sub cmdPreview_Click(sender As Object, e As EventArgs) Handles cmdPreview.Click

        Dim piRtn As Integer

        Try

            '入力チェック
            If check_Input() = False Then
                Exit Sub
            End If

            'データ存在チェック
            Dim sSql As String = ""

            'SQL編集
            sSql = makeSql()
            Dim rc As Integer = 0
            Dim ds As DataSet = _db.selectDB(sSql, RS, rc)

            '対象件数チェック
            If rc <= 0 Then
                piRtn = _msgHd.dspMSG("noTargetData", "")       '該当データがありません。
                dtShukkabiFrom.Focus()
                Exit Sub
            End If
            '上限チェック 
            If rc >= StartUp._iniVal.PrintSelUpperCnt Then
                If _msgHd.dspMSG("MaxDataCnt", "") = vbCancel Then    '抽出したデータが上限を超えています。表示してよろしいですか？
                    dtShukkabiFrom.Focus()
                    Exit Sub
                End If
            End If

            _printKbn = CommonConst.REPORT_PREVIEW

            '注文明細表
            Dim rSect As RepSectionIF = New H10R02_MikeijyoList
            rSect.setData(ds, _db)
            rSect.runReport()

            '※操作履歴はレポートで出力

        Catch ue As UsrDefException
            'メッセージ表示
            ue.dspMsg()
        Catch ex As Exception
            'システムエラー
            Call _msgHd.dspMSG("SystemErr", vbCrLf & "詳細：(" & Err.Number & ")" & vbCrLf & Err.Description & vbCrLf & "(cmdPreview)")
        End Try
    End Sub
#End Region

#Region "印刷ボタンクリック"
    '-------------------------------------------------------------------------------
    '　印刷ボタン押下
    '-------------------------------------------------------------------------------
    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click

        Dim piRtn As Integer

        Try

            '入力チェック
            If check_Input() = False Then
                Exit Sub
            End If

            'データ存在チェック
            Dim sSql As String = ""

            'SQL編集
            sSql = makeSql()
            Dim rc As Integer = 0
            Dim ds As DataSet = _db.selectDB(sSql, RS, rc)

            '対象件数チェック
            If rc <= 0 Then
                piRtn = _msgHd.dspMSG("noTargetData", "")       '該当データがありません。
                dtShukkabiFrom.Focus()
                Exit Sub
            End If
            '上限チェック 
            If rc >= StartUp._iniVal.PrintSelUpperCnt Then
                If _msgHd.dspMSG("MaxDataCnt", "") = vbCancel Then    '抽出したデータが上限を超えています。表示してよろしいですか？
                    dtShukkabiFrom.Focus()
                    Exit Sub
                End If
            End If
            'ダイアログ表示（印刷時のみ）
            If _msgHd.dspMSG("confirmPrint") = vbNo Then        '印刷処理を実行します。よろしいですか？
                Exit Sub
            End If

            _printKbn = CommonConst.REPORT_DIRECT

            '注文明細表
            Dim rSect As RepSectionIF = New H10R02_MikeijyoList
            rSect.setData(ds, _db)
            rSect.runReport()

            '※操作履歴はレポートで出力

            'ダイアログ表示（印刷時のみ）
            piRtn = _msgHd.dspMSG("printOK")                    '対象データの印刷が完了しました。

        Catch ue As UsrDefException
            'メッセージ表示
            ue.dspMsg()
        Catch ex As Exception
            'システムエラー
            Call _msgHd.dspMSG("SystemErr", vbCrLf & "詳細：(" & Err.Number & ")" & vbCrLf & Err.Description & vbCrLf & "(cmdPreview)")
        End Try
    End Sub
#End Region

#Region "CloseUp時"
    '-------------------------------------------------------------------------------
    '　CloseUp時、曜日設定
    '-------------------------------------------------------------------------------
    Private Sub dtShukkabiFrom_CloseUp(sender As Object, e As EventArgs) Handles dtShukkabiFrom.CloseUp
        Dim dDate As Date = Date.Parse(dtShukkabiFrom.Text)
        lblShukkabiWeekFrom.Text = Format(dDate, "ddd")
    End Sub
    Private Sub dtShukkabiTo_CloseUp(sender As Object, e As EventArgs) Handles dtShukkabiTo.CloseUp
        Dim dDate As Date = Date.Parse(dtShukkabiTo.Text)
        lblShukkabiWeekTo.Text = Format(dDate, "ddd")
    End Sub
    '-------------------------------------------------------------------------------
    '　Leave時、曜日設定
    '-------------------------------------------------------------------------------
    Private Sub dtShukkabiFrom_Leave(sender As Object, e As EventArgs) Handles dtShukkabiFrom.Leave
        Dim dDate As Date = Date.Parse(dtShukkabiFrom.Text)
        lblShukkabiWeekFrom.Text = Format(dDate, "ddd")
    End Sub

    Private Sub dtShukkabiTo_Leave(sender As Object, e As EventArgs) Handles dtShukkabiTo.Leave
        Dim dDate As Date = Date.Parse(dtShukkabiTo.Text)
        lblShukkabiWeekTo.Text = Format(dDate, "ddd")
    End Sub

#End Region

#Region "KeyPress時"
    '-------------------------------------------------------------------------------
    '　KeyPress時、次コントロール移動
    '-------------------------------------------------------------------------------
    Private Sub rbUriage_KeyPress(sender As Object, e As KeyPressEventArgs) Handles rbItaku.KeyPress,
                                                                                    dtShukkabiFrom.KeyPress,
                                                                                    dtShukkabiTo.KeyPress

        '押下キーがEnterの場合、次のコントロールへフォーカス移動
        Call UtilClass.moveNextFocus(Me, e)
    End Sub
#End Region

    '-------------------------------------------------------------------------------
    '　画面初期化
    '-------------------------------------------------------------------------------
    Private Sub initForm()

        '売上／委託ラジオボタン
        rbItaku.Checked = True
        '出荷日
        Dim sSysDate As String = _comLogc.getSysDdate()     'システム日付
        Dim dDate As DateTime = Date.Parse(sSysDate)
        dDate = dDate.AddMonths(-1)
        dtShukkabiFrom.Text = dDate.ToString("yyyy/MM/dd")  '前月
        dtShukkabiTo.Text = sSysDate                        'システム日付
        Dim sWeek As String = _comLogc.getSysWeek()         'システム日付の曜日取得
        lblShukkabiWeekFrom.Text = dDate.ToString("ddd")
        lblShukkabiWeekTo.Text = sWeek

    End Sub


    '-------------------------------------------------------------------------------
    '　入力チェック
    '-------------------------------------------------------------------------------
    Private Function check_Input() As Boolean

        Dim piRtn As Integer

        check_Input = False

        '大小チェック
        Select Case String.CompareOrdinal(dtShukkabiFrom.Text, dtShukkabiTo.Text)
            Case 0
                '同じ
            Case Is > 0
                '>
                piRtn = _msgHd.dspMSG("ErrDaiSyoChk", "")                                   '大小関係が正しくありません。
                dtShukkabiFrom.Focus()
                Exit Function
            Case Is < 0
                '<
        End Select

        check_Input = True

    End Function

    '-------------------------------------------------------------------------------
    '　ＳＱＬ編集
    '-------------------------------------------------------------------------------
    Private Function makeSql() As String
        makeSql = ""

        Dim sSql As String = ""
        sSql = sSql & " SELECT"
        '検索条件
        If rbItaku.Checked = True Then
            sSql = sSql & "  '" & rbItaku.Text.Substring(0, 2) & "' 条件_区分"
        Else
            sSql = sSql & "  '" & rbItaku.Text.Substring(0, 2) & "' 条件_区分"
        End If
        sSql = sSql & "     ,'" & dtShukkabiFrom.Text & " ～ " & dtShukkabiTo.Text & "' 条件_出荷日"
        'ヘッダ
        sSql = sSql & "     ,t15.委託伝番"
        sSql = sSql & "     ,t15.出荷日"
        sSql = sSql & "     ,t15.出荷先名 出荷先名"
        sSql = sSql & "     ,CASE WHEN t15.出荷先コード = t15.請求先コード"
        sSql = sSql & "           THEN ''"
        sSql = sSql & "           ELSE '[請求先]'"
        sSql = sSql & "      END 請求先ID"
        sSql = sSql & "     ,CASE WHEN t15.出荷先コード = t15.請求先コード"
        sSql = sSql & "           THEN ''"
        sSql = sSql & "           ELSE t15.請求先名"
        sSql = sSql & "      END 請求先名"
        sSql = sSql & "     ,t15.着日"
        '明細
        sSql = sSql & "     ,t16.商品名 || COALESCE(t16.荷姿形状,'') 商品名称"   'NVL
        sSql = sSql & "     ,t16.入数"
        sSql = sSql & "     ,t16.個数"
        sSql = sSql & "     ,t16.委託数量"
        sSql = sSql & "     ,t16.単位"
        sSql = sSql & "     ,t16.売上数量計"
        sSql = sSql & "     ,t16.目切数量計"
        sSql = sSql & "     ,t16.委託残数"
        sSql = sSql & "     ,t16.仮単価"
        sSql = sSql & "     ,t16.委託残金額"
        'ページフッタ
        sSql = sSql & "     ,m01.会社名"
        sSql = sSql & " FROM t15_itakhd t15"
        sSql = sSql & "      LEFT JOIN t16_itakdt AS t16     ON t16.会社コード    = t15.会社コード"
        sSql = sSql & "                                     AND t16.委託伝番      = t15.委託伝番"
        sSql = sSql & "      LEFT JOIN m01_company AS  m01   ON m01.会社コード    = t15.会社コード"
        sSql = sSql & " WHERE t15.会社コード = '" & _companyCd & "'"
        sSql = sSql & "   AND t15.取消区分   = 0"
        '出荷日
        sSql = sSql & "   AND t15.出荷日 BETWEEN '" & dtShukkabiFrom.Text & "'" & " AND '" & dtShukkabiTo.Text & "'"
        sSql = sSql & "   AND t16.委託残金額   <> 0"

        sSql = sSql & " ORDER BY t15.出荷日, t15.委託伝番, t16.行番"

        Return sSql

    End Function





End Class