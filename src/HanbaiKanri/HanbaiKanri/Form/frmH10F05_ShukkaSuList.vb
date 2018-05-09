'===============================================================================
'
'　カネキ吉田商店様
'　　（システム名）販売管理
'　　（処理機能名）出荷数一覧表出力指示
'    （フォームID）H10F05
'
'===============================================================================
'　履歴　名前　　　　　日　付       マーク      内容
'-------------------------------------------------------------------------------
'　(1)   狩野        2018/03/29                 新規              
'-------------------------------------------------------------------------------
Option Explicit On

Imports UtilMDL
Imports UtilMDL.MSG
Imports UtilMDL.DB
Imports UtilMDL.Combo               'UtilComboBoxHandler用
Imports UtilMDL.xls                 'UtilExcelHandler

Public Class frmH10F05_ShukkaSuList
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

    'EXCEL関連------------------------------------------------
    'EXCEL列定義
    Private Const XLS_COL_SHUKKABI As Integer = 2           '出荷日

    'EXCEL行定義
    Private Const XLS_ROW_HEAD As Integer = 1               'ヘッダ

    '-------------------------------------------------------------------------------
    '   構造体
    '-------------------------------------------------------------------------------
    '取得件数変数
    Private Shared lHanyoSu As Integer

    '汎用格納用変数
    Public Structure ptShukkasu_Data
        Dim KahenKey As String
        Dim Moji1 As String
        Dim Moji2 As String
        Dim Moji3 As String
    End Structure
    Private Shared uShukkasu_Data() As ptShukkasu_Data

    '取得件数変数
    Private Shared lPrmTblSu As Integer

    '出荷数一覧パラメタ格納用変数
    Public Structure ptPrmTbl_Data
        Dim ReportID As String          '帳票ＩＤ
        Dim ColNo As String             '列番号
        Dim RowNo As String             '行番号
        Dim DispName As String          '表示名
        Dim WhereSql As String          'where条件
        Dim MeishoCell As String        '名称セル
        Dim SuryoCell As String         '数量セル
        Dim Kosu As Integer             '個数
    End Structure
    Private Shared uPrmTbl_Data() As ptPrmTbl_Data


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
    Private Sub frmH10F05_ShukkaSuList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

        'Dim piRtn As Integer

        Dim BkCur As Cursor = Cursor.Current                        ' 現在のカーソルを取っておく
        Try

            '入力チェック
            If check_Input() = False Then
                Exit Sub
            End If

            Cursor.Current = Cursors.WaitCursor                     ' 砂時計カーソルに入れ替える

            '帳票ID取得
            Dim sReportId As String = ""
            sReportId = uShukkasu_Data(Me.cmbChohyo.SelectedIndex).KahenKey

            'データ存在チェック
            Dim sSql As String = ""

            '出荷数一覧パラメタテーブル読込み
            Call Read_M40_SKLISTPRM(sReportId)

            'パラメタテーブル分ループ
            Dim iIdx As Integer = 0
            For iIdx = 0 To UBound(uPrmTbl_Data) - 1
                '個数算出（SQLエラーは処理中断）
                Call Get_T10_EditKosu(iIdx)
            Next

            '雛形パス
            Dim sHinaPath As String = ""
            sHinaPath = StartUp._iniVal.BaseXlsPath

            '雛形ファイル名
            Dim sHinaFile As String = ""
            sHinaFile = sHinaPath & "\" & uShukkasu_Data(Me.cmbChohyo.SelectedIndex).Moji2

            '出力先パス
            Dim sOutPath As String = ""
            sOutPath = StartUp._iniVal.OutXlsPath

            '出力ファイル名
            Dim sOutFile As String = ""
            sOutFile = sOutPath & "\" & uShukkasu_Data(Me.cmbChohyo.SelectedIndex).Moji3

            'EXCEL帳票出力処理
            Call Out_Excel_Report(sHinaFile, sOutFile, CommonConst.REPORT_PREVIEW)

            _printKbn = CommonConst.REPORT_PREVIEW
            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(Me.cmbChohyo)

            'ログ出力
            '操作履歴ログ作成
            Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, _shoriId, CommonConst.PROGRAM_REPORT, CommonConst.STATUS_NORMAL,
                                                dtShukkabi.Text, ch.getCode(), DBNull.Value, _printKbn, DBNull.Value,
                                                UBound(uPrmTbl_Data), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _userId)

        Catch ue As UsrDefException
            'メッセージ表示
            ue.dspMsg()
        Catch ex As Exception
            'システムエラー
            Call _msgHd.dspMSG("SystemErr", vbCrLf & "詳細：(" & Err.Number & ")" & vbCrLf & Err.Description & vbCrLf & "(cmdPreview)")
        Finally
            Cursor.Current = BkCur                                  ' 取っておいたカーソルに戻す
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

            'ダイアログ表示（印刷時のみ）
            If _msgHd.dspMSG("confirmPrint") = vbNo Then        '印刷処理を実行します。よろしいですか？
                Exit Sub
            End If

            Cursor.Current = Cursors.WaitCursor                     ' 砂時計カーソルに入れ替える

            '帳票ID取得
            Dim sReportId As String = ""
            sReportId = uShukkasu_Data(Me.cmbChohyo.SelectedIndex).KahenKey

            'データ存在チェック
            Dim sSql As String = ""

            '出荷数一覧パラメタテーブル読込み
            Call Read_M40_SKLISTPRM(sReportId)

            'パラメタテーブル分ループ
            Dim iIdx As Integer = 0
            For iIdx = 0 To UBound(uPrmTbl_Data) - 1
                '個数算出（SQLエラーは処理中断）
                Call Get_T10_EditKosu(iIdx)
            Next

            '雛形パス
            Dim sHinaPath As String = ""
            sHinaPath = StartUp._iniVal.BaseXlsPath

            '雛形ファイル名
            Dim sHinaFile As String = ""
            sHinaFile = sHinaPath & "\" & uShukkasu_Data(Me.cmbChohyo.SelectedIndex).Moji2

            '出力先パス
            Dim sOutPath As String = ""
            sOutPath = StartUp._iniVal.OutXlsPath

            '出力ファイル名
            Dim sOutFile As String = ""
            sOutFile = sOutPath & "\" & uShukkasu_Data(Me.cmbChohyo.SelectedIndex).Moji3

            'EXCEL帳票出力処理
            Call Out_Excel_Report(sHinaFile, sOutFile, CommonConst.REPORT_DIRECT)

            _printKbn = CommonConst.REPORT_DIRECT
            Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(Me.cmbChohyo)

            'ログ出力
            '操作履歴ログ作成
            Call _comLogc.Insert_L01_ProcLog(_companyCd, DBNull.Value, _shoriId, CommonConst.PROGRAM_REPORT, CommonConst.STATUS_NORMAL,
                                                dtShukkabi.Text, ch.getCode(), DBNull.Value, _printKbn, DBNull.Value,
                                                UBound(uPrmTbl_Data), DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _userId)


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
    Private Sub dtShukkabiFrom_CloseUp(sender As Object, e As EventArgs) Handles dtShukkabi.CloseUp
        Dim dDate As Date = Date.Parse(dtShukkabi.Text)
        lblShukkabiWeekFrom.Text = Format(dDate, "ddd")
    End Sub
    '-------------------------------------------------------------------------------
    '　Leave時、曜日設定
    '-------------------------------------------------------------------------------
    Private Sub dtShukkabiFrom_Leave(sender As Object, e As EventArgs) Handles dtShukkabi.Leave
        Dim dDate As Date = Date.Parse(dtShukkabi.Text)
        lblShukkabiWeekFrom.Text = Format(dDate, "ddd")
    End Sub
#End Region

#Region "KeyPress時"
    '-------------------------------------------------------------------------------
    '　KeyPress時、次コントロール移動
    '-------------------------------------------------------------------------------
    Private Sub rbUriage_KeyPress(sender As Object, e As KeyPressEventArgs) Handles dtShukkabi.KeyPress,
                                                                                    cmbChohyo.KeyPress

        '押下キーがEnterの場合、次のコントロールへフォーカス移動
        Call UtilClass.moveNextFocus(Me, e)
    End Sub
#End Region

    '-------------------------------------------------------------------------------
    '　画面初期化
    '-------------------------------------------------------------------------------
    Private Sub initForm()

        '出荷日
        Dim sSysDate As String = _comLogc.getSysDdate()     'システム日付
        Dim dDate As DateTime = Date.Parse(sSysDate)
        dtShukkabi.Text = dDate.ToString("yyyy/MM/dd")
        Dim sWeek As String = _comLogc.getSysWeek()         'システム日付の曜日取得
        lblShukkabiWeekFrom.Text = dDate.ToString("ddd")

        'コンボボックス設定
        cmbChohyo.Items.Clear()
        Call GetHanyo_ShukkasuIchiran()
        Call SetComboShukkasu(False)
        cmbChohyo.SelectedIndex = 0                         '先頭

    End Sub

    '-------------------------------------------------------------------------------
    '　汎用マスタ(1013)よりデータを取得し、内部に保持
    '-------------------------------------------------------------------------------
    Private Sub GetHanyo_ShukkasuIchiran()

        Dim sSql As String = ""
        Dim oDataSet As DataSet             ' データセット型

        sSql = sSql & " SELECT"
        sSql = sSql & "    可変キー "
        sSql = sSql & "   ,文字１ "
        sSql = sSql & "   ,文字２ "
        sSql = sSql & "   ,文字３ "
        sSql = sSql & " FROM m90_hanyo "
        sSql = sSql & "   WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' "
        sSql = sSql & "     AND 固定キー   = '" & CommonConst.HANYO_SHUKKASU_ICHIRAN & "' "         '1013出荷数一覧表
        sSql = sSql & " ORDER BY 表示順 "

        'SQL発行
        Dim reccnt As Integer = 0
        oDataSet = _db.selectDB(sSql, RS, reccnt)                           '抽出結果をDSへ格納

        If reccnt <= 0 Then
            Throw New UsrDefException("対象データがありません。", _msgHd.getMSG("NonData"))
        End If

        lHanyoSu = reccnt
        ReDim uShukkasu_Data(reccnt)

        Dim lCnt As Integer = 0
        For lCnt = 0 To reccnt - 1
            uShukkasu_Data(lCnt).KahenKey = _db.rmNullStr(oDataSet.Tables(RS).Rows(lCnt)("可変キー"))
            uShukkasu_Data(lCnt).Moji1 = _db.rmNullStr(oDataSet.Tables(RS).Rows(lCnt)("文字１"))
            uShukkasu_Data(lCnt).Moji2 = _db.rmNullStr(oDataSet.Tables(RS).Rows(lCnt)("文字２"))
            uShukkasu_Data(lCnt).Moji3 = _db.rmNullStr(oDataSet.Tables(RS).Rows(lCnt)("文字３"))
        Next

    End Sub

    '-------------------------------------------------------------------------------
    '   汎用マスタ(1013)よりデータを取得し、コンボボックスにセットする
    '-------------------------------------------------------------------------------
    Private Sub SetComboShukkasu(ByVal prmKara As Boolean)

        Dim ch As UtilComboBoxHandler = New UtilComboBoxHandler(Me.cmbChohyo)

        '先頭空
        If prmKara = True Then
            Call ch.addItem(New UtilCboVO("", ""))
        End If

        Dim iIdx As Integer = 0
        For iIdx = 0 To UBound(uShukkasu_Data) - 1
            Call ch.addItem(New UtilCboVO(uShukkasu_Data(iIdx).KahenKey, uShukkasu_Data(iIdx).Moji1))
        Next

    End Sub

    '-------------------------------------------------------------------------------
    '　入力チェック
    '-------------------------------------------------------------------------------
    Private Function check_Input() As Boolean

        '出荷日はクリアできないので、必須チェック不要

        check_Input = True

    End Function

    '-------------------------------------------------------------------------------
    '　出荷数一覧パラメタよりデータを取得し、内部に保持
    '-------------------------------------------------------------------------------
    Private Sub Read_M40_SKLISTPRM(ByVal prmReportId As String)

        Dim sSql As String = ""
        Dim oDataSet As DataSet             ' データセット型

        sSql = sSql & " SELECT"
        sSql = sSql & "    会社コード "
        sSql = sSql & "   ,帳票ＩＤ "
        sSql = sSql & "   ,列番号 "
        sSql = sSql & "   ,行番号 "
        sSql = sSql & "   ,表示名 "
        sSql = sSql & "   ,対象条件 "
        sSql = sSql & "   ,名称セル "
        sSql = sSql & "   ,数量セル "
        sSql = sSql & "   ,メモ "
        sSql = sSql & " FROM m40_sklistprm "
        sSql = sSql & "   WHERE 会社コード = '" & frmC01F10_Login.loginValue.BumonCD & "' "
        sSql = sSql & "     AND 帳票ＩＤ   = '" & prmReportId & "' "
        sSql = sSql & " ORDER BY 列番号, 行番号  "

        'SQL発行
        Dim reccnt As Integer = 0
        oDataSet = _db.selectDB(sSql, RS, reccnt)                           '抽出結果をDSへ格納

        If reccnt <= 0 Then
            Throw New UsrDefException("対象データがありません。", _msgHd.getMSG("NonData"))
        End If

        lHanyoSu = reccnt
        ReDim uPrmTbl_Data(reccnt)

        Dim lCnt As Integer = 0
        For lCnt = 0 To reccnt - 1
            uPrmTbl_Data(lCnt).ReportID = _db.rmNullStr(oDataSet.Tables(RS).Rows(lCnt)("帳票ＩＤ"))
            uPrmTbl_Data(lCnt).ColNo = _db.rmNullStr(oDataSet.Tables(RS).Rows(lCnt)("列番号"))
            uPrmTbl_Data(lCnt).RowNo = _db.rmNullStr(oDataSet.Tables(RS).Rows(lCnt)("行番号"))
            uPrmTbl_Data(lCnt).DispName = _db.rmNullStr(oDataSet.Tables(RS).Rows(lCnt)("表示名"))
            uPrmTbl_Data(lCnt).WhereSql = _db.rmNullStr(oDataSet.Tables(RS).Rows(lCnt)("対象条件"))
            uPrmTbl_Data(lCnt).MeishoCell = _db.rmNullStr(oDataSet.Tables(RS).Rows(lCnt)("名称セル"))
            uPrmTbl_Data(lCnt).SuryoCell = _db.rmNullStr(oDataSet.Tables(RS).Rows(lCnt)("数量セル"))
            uPrmTbl_Data(lCnt).Kosu = 0
        Next

    End Sub

    '-------------------------------------------------------------------------------
    '　注文基本、注文明細から対象データを抽出し、個数を編集する
    '-------------------------------------------------------------------------------
    Private Sub Get_T10_EditKosu(ByVal pIdx As Integer)

        Dim sSql As String = ""
        Dim oDataSet As DataSet             ' データセット型

        Dim sDispName = "(" & uPrmTbl_Data(pIdx).DispName & ")"

        Try
            sSql = sSql & " SELECT" & vbCrLf
            sSql = sSql & "     SUM(t11.個数) 個数 " & vbCrLf
            sSql = sSql & " FROM t10_cymnhd t10" & vbCrLf
            sSql = sSql & "      LEFT JOIN t11_cymndt AS t11    ON t11.会社コード   = t10.会社コード" & vbCrLf
            sSql = sSql & "                                    AND t11.注文伝番     = t10.注文伝番" & vbCrLf
            sSql = sSql & " WHERE t10.会社コード = '" & _companyCd & "'" & vbCrLf
            sSql = sSql & "   AND t10.取消区分   = 0" & vbCrLf
            '出荷日
            sSql = sSql & "   AND t10.出荷日     = TO_DATE('" & dtShukkabi.Text & "','yyyy/MM/dd')" & vbCrLf
            '可変条件
            If Not uPrmTbl_Data(pIdx).WhereSql.Equals("") Then
                sSql = sSql & "   AND " & uPrmTbl_Data(pIdx).WhereSql & vbCrLf
            End If

            'SQL発行
            Dim reccnt As Integer = 0
            oDataSet = _db.selectDB(sSql, RS, reccnt)                           '抽出結果をDSへ格納

            '集計した個数を保存する
            If _db.rmNullStr(oDataSet.Tables(RS).Rows(0)("個数")).Equals("") Then
                uPrmTbl_Data(pIdx).Kosu = 0
            Else
                uPrmTbl_Data(pIdx).Kosu = _db.rmNullStr(oDataSet.Tables(RS).Rows(0)("個数"))
            End If

        Catch ex As Exception
            Throw New UsrDefException("出荷数一覧パラメタの対象条件に誤りがあります。", _msgHd.getMSG("paramTblError",
                                                                               sDispName & ControlChars.NewLine & sSql))     '表示名を表示
        End Try

    End Sub

    '-------------------------------------------------------------------------------
    '　EXCEL帳票出力処理
    '-------------------------------------------------------------------------------
    Private Sub Out_Excel_Report(ByRef prmHinaFile As String, ByRef prmOutFile As String, ByVal prmPreview As String)

        'コピー先ファイルが存在する場合、コピー先ファイルを削除
        If UtilClass.isFileExists(prmOutFile) Then
            Try
                System.IO.File.Delete(prmOutFile)
            Catch ioe As System.IO.IOException
                Throw New UsrDefException("出力先ファイルが開かれたままになっています。", _msgHd.getMSG("outFileOpen"))
            End Try
        End If

        '雛形ファイルを出力先にコピー
        Try
            System.IO.File.Copy(prmHinaFile, prmOutFile)
        Catch ioe As System.IO.IOException
            Throw New UsrDefException(ioe, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(ioe)))
        End Try

        'Excelハンドラ
        Dim xh As UtilExcelHandler = New UtilExcelHandler(prmOutFile, False)

        'コピー先ファイルオープン-------------------------------
        xh.open()

        Try

            'Excel出力
            Call Put_Excel_Sheet(xh)

        Catch lue As UsrDefException
            Throw lue
        Catch lex As Exception
            Throw New UsrDefException(lex, _msgHd.getMSG("SystemErr", UtilClass.getErrDetail(lex)))
        Finally
            'ファイルクローズ
            xh.close()
        End Try

        If prmPreview.Equals(CommonConst.REPORT_PREVIEW) Then
            'Excel表示
            xh.display()
        Else
            'Excel出力
            xh.printOut()
            'xh.printPreview()

        End If

    End Sub

    '-------------------------------------------------------------------------------
    '   内部テーブルからＥＸＣＥＬシートに出力
    '-------------------------------------------------------------------------------
    Private Sub Put_Excel_Sheet(ByRef prmRefXh As UtilExcelHandler)

        Dim x As UtilExcelHandler = prmRefXh            'Excelハンドラ

        'シート(雛形)を複製保存----------------------------------------------
        Dim editName As String = x.targetSheet          '出力先シート
        Dim orgName As String = "TEMPLATE"              'シート名
        x.copySheet(orgName)
        x.selectSheet(editName)                         '操作対象を元に戻す

        '出荷日
        Dim sShukkabi As String = ""
        Dim dDate As DateTime = Date.Parse(dtShukkabi.Text)
        sShukkabi = dDate.ToString("yyyy年MM月dd日（ddd）　出荷分")
        x.setValue(sShukkabi, XLS_ROW_HEAD, XLS_COL_SHUKKABI)


        Dim iIdx As Integer = 0
        For iIdx = 0 To UBound(uPrmTbl_Data) - 1

            Dim sMeisho As String = uPrmTbl_Data(iIdx).MeishoCell       '名称セル（Ａ1形式）
            Dim sSuryo As String = uPrmTbl_Data(iIdx).SuryoCell         '数量セル（Ａ1形式）
            Dim sDispName As String = uPrmTbl_Data(iIdx).DispName
            Dim dcKosu As Decimal = uPrmTbl_Data(iIdx).Kosu

            x.setValueA1(sDispName, sMeisho)            '名称セル←表示名
            x.setValueA1(dcKosu, sSuryo)                '数量セル←個数

        Next

        '明細先頭行にフォーカス
        x.selectCell(1, 1)

        System.Windows.Forms.Application.DoEvents()

        '後処理-------------------------------------------------------------
        x.deleteSheet(orgName)  '保存していた雛形シートを削除

    End Sub


End Class