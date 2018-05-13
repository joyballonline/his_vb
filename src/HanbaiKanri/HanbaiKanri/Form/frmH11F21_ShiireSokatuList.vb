'===============================================================================
'
'　カネキ吉田商店様
'　　（システム名）販売管理
'　　（処理機能名）仕入総括表出力指示
'    （フォームID）H11F21
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   狩野        2018/03/22                 新規              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Public Class frmH11F21_ShiireSokatuList
    '-------------------------------------------------------------------------------
    '   定数定義
    '-------------------------------------------------------------------------------
    'PG制御文字 
    Private Const N As String = ControlChars.NewLine                    '改行文字
    Private Const RS As String = "RecSet"                               'レコードセットテーブル
    Private Const DATE_EMPTY As String = ""

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
        Me.Text = Me.Text & "[" & frmC01F10_Login.loginValue.BumonNM & "][" & frmC01F10_Login.loginValue.TantoNM & "]" & StartUp.BackUpServerPrint  'フォームタイトル表示
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
    Private Sub frmH11F21_ShiireSokatuList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
                txtNendo.Focus()
                Exit Sub
            End If
            '上限チェック 
            If rc >= StartUp._iniVal.PrintSelUpperCnt Then
                If _msgHd.dspMSG("MaxDataCnt", "") = vbCancel Then    '抽出したデータが上限を超えています。表示してよろしいですか？
                    txtNendo.Focus()
                    Exit Sub
                End If
            End If

            _printKbn = CommonConst.REPORT_PREVIEW

            '仕入総括表
            Dim rSect As RepSectionIF = New H11R21_ShiireSokatuList
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
                txtNendo.Focus()
                Exit Sub
            End If
            '上限チェック 
            If rc >= StartUp._iniVal.PrintSelUpperCnt Then
                If _msgHd.dspMSG("MaxDataCnt", "") = vbCancel Then    '抽出したデータが上限を超えています。表示してよろしいですか？
                    txtNendo.Focus()
                    Exit Sub
                End If
            End If
            'ダイアログ表示（印刷時のみ）
            If _msgHd.dspMSG("confirmPrint") = vbNo Then        '印刷処理を実行します。よろしいですか？
                Exit Sub
            End If

            _printKbn = CommonConst.REPORT_DIRECT

            '仕入総括表
            Dim rSect As RepSectionIF = New H11R21_ShiireSokatuList
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

#Region "Enter時"
    '-------------------------------------------------------------------------------
    '　Enter時、全選択状態にする
    '-------------------------------------------------------------------------------
    Private Sub txtDenNoFrom_Enter(sender As Object, e As EventArgs) Handles txtNendo.Enter
        txtNendo.Focus()
        txtNendo.SelectAll()
    End Sub
#End Region

#Region "KeyPress時"
    '-------------------------------------------------------------------------------
    '　KeyPress時、次コントロール移動
    '-------------------------------------------------------------------------------
    Private Sub rbUriage_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNendo.KeyPress

        '押下キーがEnterの場合、次のコントロールへフォーカス移動
        Call UtilClass.moveNextFocus(Me, e)
    End Sub
#End Region

    '-------------------------------------------------------------------------------
    '　画面初期化
    '-------------------------------------------------------------------------------
    Private Sub initForm()

        '仕入日
        Dim sSysDate As String = _comLogc.getSysDdate()     'システム日付
        'txtSiireNendo.Text = sSysDate.Substring(0, 4)
        txtNendo.Text = sSysDate
        txtNendo.Text = sSysDate.Substring(0, 4)

    End Sub

    '-------------------------------------------------------------------------------
    '　入力チェック
    '-------------------------------------------------------------------------------
    Private Function check_Input() As Boolean

        Dim piRtn As Integer

        check_Input = False

        '必須チェック
        If txtNendo.Text = DATE_EMPTY Then
            piRtn = _msgHd.dspMSG("requiredImput", "")                               '必須入力項目です。
            txtNendo.Focus()
            Exit Function
        End If

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
        sSql = sSql & "  '" & CommonConst.SHIIRE_KBN_NM_SHIIRE & "' 条件_区分"
        sSql = sSql & "     ,'" & txtNendo.Text & "年度" & "' 条件_仕入年度"
        '明細
        sSql = sSql & "     ,AA.商品分類 "
        sSql = sSql & "     ,AA.仕入先コード "
        sSql = sSql & "     ,AA.仕入先名 "
        sSql = sSql & "     ,AA.２月件数 "
        sSql = sSql & "     ,AA.２月仕入数量 "
        sSql = sSql & "     ,AA.２月仕入金額 "
        sSql = sSql & "     ,AA.２月仕入単価 "
        sSql = sSql & "     ,AA.３月件数 "
        sSql = sSql & "     ,AA.３月仕入数量 "
        sSql = sSql & "     ,AA.３月仕入金額 "
        sSql = sSql & "     ,AA.３月仕入単価 "
        sSql = sSql & "     ,AA.４月件数 "
        sSql = sSql & "     ,AA.４月仕入数量 "
        sSql = sSql & "     ,AA.４月仕入金額 "
        sSql = sSql & "     ,AA.４月仕入単価 "
        sSql = sSql & "     ,AA.５月件数 "
        sSql = sSql & "     ,AA.５月仕入数量 "
        sSql = sSql & "     ,AA.５月仕入金額 "
        sSql = sSql & "     ,AA.５月仕入単価 "
        'ページフッタ
        sSql = sSql & "     ,AA.会社名"
        'ヘッダ（なし）
        '明細
        sSql = sSql & " FROM ("
        ' 2月
        sSql = sSql & "       SELECT "
        sSql = sSql & "              '1'                商品分類 "
        sSql = sSql & "             ,t40.仕入先コード   仕入先コード "
        sSql = sSql & "             ,MAX(t40.仕入先名)  仕入先名 "
        sSql = sSql & "             ,COUNT(*)           ２月件数 "
        sSql = sSql & "             ,SUM(t41.仕入数量)  ２月仕入数量 "
        sSql = sSql & "             ,SUM(t41.仕入金額)  ２月仕入金額 "
        sSql = sSql & "             ,SUM(t41.仕入単価)  ２月仕入単価 "
        sSql = sSql & "             ,0  ３月件数 "
        sSql = sSql & "             ,0  ３月仕入数量 "
        sSql = sSql & "             ,0  ３月仕入金額 "
        sSql = sSql & "             ,0  ３月仕入単価 "
        sSql = sSql & "             ,0  ４月件数 "
        sSql = sSql & "             ,0  ４月仕入数量 "
        sSql = sSql & "             ,0  ４月仕入金額 "
        sSql = sSql & "             ,0  ４月仕入単価 "
        sSql = sSql & "             ,0  ５月件数 "
        sSql = sSql & "             ,0  ５月仕入数量 "
        sSql = sSql & "             ,0  ５月仕入金額 "
        sSql = sSql & "             ,0  ５月仕入単価 "
        sSql = sSql & "             ,MAX(m01.会社名) 会社名 "
        sSql = sSql & "       FROM t40_sirehd t40 "     '仕入基本
        sSql = sSql & "            LEFT JOIN t41_siredt As t41    On t41.会社コード   = t40.会社コード "
        sSql = sSql & "                                          AND t41.仕入伝番     = t40.仕入伝番 "
        sSql = sSql & "       LEFT JOIN m01_company As m01        On m01.会社コード   = t40.会社コード"
        sSql = sSql & "       WHERE t40.会社コード = '" & _companyCd & "'"
        sSql = sSql & "         AND TO_CHAR(T40.仕入日,'YYYY/MM') = '" & txtNendo.Text & "/02" & "'"
        sSql = sSql & "       GROUP BY t40.仕入先コード  "
        ' 3月
        sSql = sSql & "       UNION ALL "
        sSql = sSql & "       SELECT "
        sSql = sSql & "              '1' 商品分類 "
        sSql = sSql & "             ,t40.仕入先コード   仕入先コード "
        sSql = sSql & "             ,MAX(t40.仕入先名)  仕入先名 "
        sSql = sSql & "             ,0  ２月件数 "
        sSql = sSql & "             ,0  ２月仕入数量 "
        sSql = sSql & "             ,0  ２月仕入金額 "
        sSql = sSql & "             ,0  ２月仕入単価 "
        sSql = sSql & "             ,COUNT(*)           ３月件数 "
        sSql = sSql & "             ,SUM(t41.仕入数量)  ３月仕入数量 "
        sSql = sSql & "             ,SUM(t41.仕入金額)  ３月仕入金額 "
        sSql = sSql & "             ,SUM(t41.仕入単価)  ３月仕入単価 "
        sSql = sSql & "             ,0  ４月件数 "
        sSql = sSql & "             ,0  ４月仕入数量 "
        sSql = sSql & "             ,0  ４月仕入金額 "
        sSql = sSql & "             ,0  ４月仕入単価 "
        sSql = sSql & "             ,0  ５月件数 "
        sSql = sSql & "             ,0  ５月仕入数量 "
        sSql = sSql & "             ,0  ５月仕入金額 "
        sSql = sSql & "             ,0  ５月仕入単価 "
        sSql = sSql & "             ,MAX(m01.会社名) 会社名 "
        sSql = sSql & "       FROM t40_sirehd t40 "     '仕入基本
        sSql = sSql & "            LEFT JOIN t41_siredt As t41    On t41.会社コード   = t40.会社コード "
        sSql = sSql & "                                          AND t41.仕入伝番     = t40.仕入伝番 "
        sSql = sSql & "       LEFT JOIN m01_company As m01        On m01.会社コード   = t40.会社コード"
        sSql = sSql & "       WHERE t40.会社コード = '" & _companyCd & "'"
        sSql = sSql & "         AND TO_CHAR(T40.仕入日,'YYYY/MM') = '" & txtNendo.Text & "/03" & "'"
        sSql = sSql & "       GROUP BY t40.仕入先コード  "
        ' 4月
        sSql = sSql & "       UNION ALL "
        sSql = sSql & "       SELECT "
        sSql = sSql & "              '1' 商品分類 "
        sSql = sSql & "             ,t40.仕入先コード   仕入先コード "
        sSql = sSql & "             ,MAX(t40.仕入先名)  仕入先名 "
        sSql = sSql & "             ,0  ２月件数 "
        sSql = sSql & "             ,0  ２月仕入数量 "
        sSql = sSql & "             ,0  ２月仕入金額 "
        sSql = sSql & "             ,0  ２月仕入単価 "
        sSql = sSql & "             ,0  ３月件数 "
        sSql = sSql & "             ,0  ３月仕入数量 "
        sSql = sSql & "             ,0  ３月仕入金額 "
        sSql = sSql & "             ,0  ３月仕入単価 "
        sSql = sSql & "             ,COUNT(*)           ４月件数 "
        sSql = sSql & "             ,SUM(t41.仕入数量)  ４月仕入数量 "
        sSql = sSql & "             ,SUM(t41.仕入金額)  ４月仕入金額 "
        sSql = sSql & "             ,SUM(t41.仕入単価)  ４月仕入単価 "
        sSql = sSql & "             ,0  ５月件数 "
        sSql = sSql & "             ,0  ５月仕入数量 "
        sSql = sSql & "             ,0  ５月仕入金額 "
        sSql = sSql & "             ,0  ５月仕入単価 "
        sSql = sSql & "             ,MAX(m01.会社名) 会社名 "
        sSql = sSql & "       FROM t40_sirehd t40 "     '仕入基本
        sSql = sSql & "            LEFT JOIN t41_siredt As t41    On t41.会社コード   = t40.会社コード "
        sSql = sSql & "                                          AND t41.仕入伝番     = t40.仕入伝番 "
        sSql = sSql & "       LEFT JOIN m01_company As m01        On m01.会社コード   = t40.会社コード"
        sSql = sSql & "       WHERE t40.会社コード = '" & _companyCd & "'"
        sSql = sSql & "         AND TO_CHAR(T40.仕入日,'YYYY/MM') = '" & txtNendo.Text & "/04" & "'"
        sSql = sSql & "       GROUP BY t40.仕入先コード  "
        ' 5月
        sSql = sSql & "       UNION ALL "
        sSql = sSql & "       SELECT "
        sSql = sSql & "              '1' 商品分類 "
        sSql = sSql & "             ,t40.仕入先コード   仕入先コード "
        sSql = sSql & "             ,MAX(t40.仕入先名)  仕入先名 "
        sSql = sSql & "             ,0  ２月件数 "
        sSql = sSql & "             ,0  ２月仕入数量 "
        sSql = sSql & "             ,0  ２月仕入金額 "
        sSql = sSql & "             ,0  ２月仕入単価 "
        sSql = sSql & "             ,0  ３月件数 "
        sSql = sSql & "             ,0  ３月仕入数量 "
        sSql = sSql & "             ,0  ３月仕入金額 "
        sSql = sSql & "             ,0  ３月仕入単価 "
        sSql = sSql & "             ,0  ４月件数 "
        sSql = sSql & "             ,0  ４月仕入数量 "
        sSql = sSql & "             ,0  ４月仕入金額 "
        sSql = sSql & "             ,0  ４月仕入単価 "
        sSql = sSql & "             ,COUNT(*)           ５月件数 "
        sSql = sSql & "             ,SUM(t41.仕入数量)  ５月仕入数量 "
        sSql = sSql & "             ,SUM(t41.仕入金額)  ５月仕入金額 "
        sSql = sSql & "             ,SUM(t41.仕入単価)  ５月仕入単価 "
        sSql = sSql & "             ,MAX(m01.会社名) 会社名 "
        sSql = sSql & "       FROM t40_sirehd t40 "     '仕入基本
        sSql = sSql & "            LEFT JOIN t41_siredt As t41    On t41.会社コード   = t40.会社コード "
        sSql = sSql & "                                          AND t41.仕入伝番     = t40.仕入伝番 "
        sSql = sSql & "       LEFT JOIN m01_company As m01        On m01.会社コード   = t40.会社コード"
        sSql = sSql & "       WHERE t40.会社コード = '" & _companyCd & "'"
        sSql = sSql & "         AND TO_CHAR(T40.仕入日,'YYYY/MM') = '" & txtNendo.Text & "/05" & "'"
        sSql = sSql & "       GROUP BY t40.仕入先コード  "
        sSql = sSql & "      ) AS  AA "
        sSql = sSql & " ORDER BY AA.商品分類, AA.仕入先コード "

        Return sSql

    End Function

End Class